using Microsoft.Data.SqlClient;
using System;
using System.IO;
using System.Windows.Forms;

namespace kut_oto
{
    /// <summary>
    /// LocalDB veritabanını ilk çalıştırmada otomatik oluşturur.
    /// Tak-Çalıştır (Portable) destek için Program.cs'den çağrılır.
    /// </summary>
    internal static class DatabaseHelper
    {
        // MDF dosyasının adı (uygulama klasörüne yerleştirilir)
        private const string MdfFileName = "Kutuphane.mdf";

        // Master veritabanına bağlanarak yeni bir MDF oluşturur
        private const string MasterConnStr =
            @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;";

        public static void EnsureDatabaseExists()
        {
            string appDir = AppDomain.CurrentDomain.BaseDirectory;
            string mdfPath = Path.Combine(appDir, MdfFileName);
            string ldfPath = Path.Combine(appDir, "Kutuphane_log.ldf");

            // MDF dosyası zaten varsa işlem yapma
            if (File.Exists(mdfPath))
                return;

            try
            {
                using (SqlConnection con = new SqlConnection(MasterConnStr))
                {
                    con.Open();

                    // 1. MDF dosyasını oluştur (LocalDB'ye ekle)
                    string createDb = $@"
                        CREATE DATABASE [KutuphaneDB]
                        ON PRIMARY (NAME='KutuphaneDB', FILENAME='{mdfPath}')
                        LOG ON (NAME='KutuphaneDB_log', FILENAME='{ldfPath}');";
                    new SqlCommand(createDb, con).ExecuteNonQuery();

                    // 2. Tabloları oluştur
                    string[] tableScripts = {
                        @"CREATE TABLE Personeller (
                            personeller_id INT IDENTITY(1,1) PRIMARY KEY,
                            personeller_kullaniciAd NVARCHAR(50) NOT NULL,
                            personeller_sifre NVARCHAR(50) NOT NULL
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

                        // Varsayılan admin (kullanıcı: admin / şifre: 1234)
                        @"INSERT INTO Personeller (personeller_kullaniciAd, personeller_sifre)
                          VALUES (N'admin', N'1234');"
                    };

                    // Her tabloyu KutuphaneDB bağlamında çalıştır
                    string dbConnStr = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={mdfPath};Integrated Security=True;";
                    using (SqlConnection dbCon = new SqlConnection(dbConnStr))
                    {
                        dbCon.Open();
                        foreach (string script in tableScripts)
                            new SqlCommand(script, dbCon).ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Veritabanı oluşturulurken hata oluştu:\n" + ex.Message +
                    "\n\nLütfen SQL Server LocalDB'nin kurulu olduğundan emin olun.\n" +
                    "İndirme: https://aka.ms/sqllocaldb",
                    "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
