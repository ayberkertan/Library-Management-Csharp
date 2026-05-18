using Kutuphane.Core.Constants;
using Microsoft.Data.SqlClient;

namespace Kutuphane.Infrastructure.Data;

public class DatabaseInitializer
{
    private const string DatabaseName = "KutuphaneDB";
    private const string MdfFileName = "Kutuphane.mdf";
    private const string LdfFileName = "Kutuphane_log.ldf";
    private const string MasterConnStr =
        @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;";

    public void EnsureDatabaseExists()
    {
        var appDir = AppDomain.CurrentDomain.GetData("DataDirectory") as string
                     ?? AppContext.BaseDirectory;
        var mdfPath = Path.GetFullPath(Path.Combine(appDir, MdfFileName));
        var ldfPath = Path.GetFullPath(Path.Combine(appDir, LdfFileName));

        if (!File.Exists(mdfPath))
        {
            CreateFreshDatabase(mdfPath, ldfPath);
        }

        SampleDataSeeder.SeedIfEmpty(mdfPath);
    }

    private void CreateFreshDatabase(string mdfPath, string ldfPath)
    {
        using var master = new SqlConnection(MasterConnStr);
        master.Open();

        if (DatabaseExists(master))
            DropOrphanDatabaseRegistration(master);

        if (DatabaseExists(master))
            throw new InvalidOperationException(
                $"LocalDB'deki eski '{DatabaseName}' kaydı temizlenemedi. " +
                $"Lütfen scripts\\TemizleLocalDB.sql dosyasını SQL Server Management Studio veya " +
                $"Visual Studio SQL Server Object Explorer ile çalıştırın.");

        CreateDatabaseFiles(master, mdfPath, ldfPath);
        InitializeSchema(mdfPath);
    }

    private static bool DatabaseExists(SqlConnection master)
    {
        const string sql = "SELECT 1 FROM sys.databases WHERE name = @name";
        using var cmd = new SqlCommand(sql, master);
        cmd.Parameters.AddWithValue("@name", DatabaseName);
        return cmd.ExecuteScalar() != null;
    }

    /// <summary>
    /// Eski proje yolundaki (.mdf silinmiş) yetim veritabanı kaydını kaldırır.
    /// </summary>
    private static void DropOrphanDatabaseRegistration(SqlConnection master)
    {
        var strategies = new[]
        {
            $@"
                IF EXISTS (SELECT 1 FROM sys.databases WHERE name = N'{DatabaseName}')
                BEGIN
                    ALTER DATABASE [{DatabaseName}] SET EMERGENCY;
                    ALTER DATABASE [{DatabaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                    DROP DATABASE [{DatabaseName}];
                END",
            $@"
                IF EXISTS (SELECT 1 FROM sys.databases WHERE name = N'{DatabaseName}')
                BEGIN
                    ALTER DATABASE [{DatabaseName}] SET OFFLINE WITH ROLLBACK IMMEDIATE;
                    DROP DATABASE [{DatabaseName}];
                END",
            $@"
                IF EXISTS (SELECT 1 FROM sys.databases WHERE name = N'{DatabaseName}')
                    EXEC sp_detach_db @dbname = N'{DatabaseName}', @skipchecks = N'true';"
        };

        foreach (var sql in strategies)
        {
            try
            {
                new SqlCommand(sql, master) { CommandTimeout = 60 }.ExecuteNonQuery();
                if (!DatabaseExists(master))
                    return;
            }
            catch
            {
                // Sonraki yöntemi dene
            }
        }
    }

    private static void CreateDatabaseFiles(SqlConnection master, string mdfPath, string ldfPath)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(mdfPath)!);

        var mdfSql = mdfPath.Replace("'", "''");
        var ldfSql = ldfPath.Replace("'", "''");

        var createDb = $@"
            CREATE DATABASE [{DatabaseName}]
            ON PRIMARY (NAME=N'{DatabaseName}', FILENAME=N'{mdfSql}')
            LOG ON (NAME=N'{DatabaseName}_log', FILENAME=N'{ldfSql}');";

        new SqlCommand(createDb, master).ExecuteNonQuery();
    }

    private static void InitializeSchema(string mdfPath)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(LibrarySettings.DefaultAdminPassword);

        var tableScripts = new[]
        {
            @"CREATE TABLE Personeller (
                personeller_id INT IDENTITY(1,1) PRIMARY KEY,
                personeller_kullaniciAd NVARCHAR(50) NOT NULL,
                personeller_sifre NVARCHAR(200) NOT NULL
              );",
            @"CREATE TABLE Kullanicilar (
                kullanici_ıd INT IDENTITY(1,1) PRIMARY KEY,
                kullanici_ad NVARCHAR(50),
                kullanici_soyad NVARCHAR(50),
                kullanici_tc NVARCHAR(11),
                kullanici_tel NVARCHAR(20),
                kullanici_mail NVARCHAR(100),
                kullanici_cinsiyet NCHAR(1),
                kullanici_ceza FLOAT DEFAULT 0
              );",
            @"CREATE TABLE Kaynaklar (
                kaynak_id INT IDENTITY(1,1) PRIMARY KEY,
                kaynak_ad NVARCHAR(200),
                kaynak_yazar NVARCHAR(100),
                kaynak_yayıncı NVARCHAR(100),
                kaynak_sayfasayısı INT,
                kaynak_basımtarihi DATE
              );",
            @"CREATE TABLE kayitlar (
                kayit_id INT IDENTITY(1,1) PRIMARY KEY,
                kullanici_id INT NOT NULL,
                kitap_id INT NOT NULL,
                alis_tarihi DATETIME,
                son_tarih DATETIME,
                durum BIT DEFAULT 1,
                FOREIGN KEY (kullanici_id) REFERENCES Kullanicilar(kullanici_ıd),
                FOREIGN KEY (kitap_id) REFERENCES Kaynaklar(kaynak_id)
              );",
            $@"INSERT INTO Personeller (personeller_kullaniciAd, personeller_sifre)
               VALUES (N'{LibrarySettings.DefaultAdminUsername}', N'{hashedPassword.Replace("'", "''")}');"
        };

        var dbConnStr = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={mdfPath};Integrated Security=True;";
        using var dbCon = new SqlConnection(dbConnStr);
        dbCon.Open();
        foreach (var script in tableScripts)
            new SqlCommand(script, dbCon).ExecuteNonQuery();
    }
}
