using Microsoft.Data.SqlClient;

namespace Kutuphane.Infrastructure.Data;

internal static class SampleDataSeeder
{
    public static void SeedIfEmpty(string mdfPath)
    {
        var dbConnStr = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={mdfPath};Integrated Security=True;";
        using var con = new SqlConnection(dbConnStr);
        con.Open();

        if (TableHasRows(con, "Kullanicilar"))
            return;

        var scripts = new[]
        {
            @"INSERT INTO Kullanicilar (kullanici_ad, kullanici_soyad, kullanici_tc, kullanici_tel, kullanici_mail, kullanici_cinsiyet, kullanici_ceza)
              VALUES (N'Ahmet', N'Yılmaz', N'12345678901', N'05321234567', N'ahmet@mail.com', N'E', 0);",
            @"INSERT INTO Kullanicilar (kullanici_ad, kullanici_soyad, kullanici_tc, kullanici_tel, kullanici_mail, kullanici_cinsiyet, kullanici_ceza)
              VALUES (N'Ayşe', N'Kaya', N'23456789012', N'05329876543', N'ayse@mail.com', N'K', 15.5);",
            @"INSERT INTO Kullanicilar (kullanici_ad, kullanici_soyad, kullanici_tc, kullanici_tel, kullanici_mail, kullanici_cinsiyet, kullanici_ceza)
              VALUES (N'Mehmet', N'Demir', N'34567890123', N'05441112233', N'mehmet@mail.com', N'E', 0);",
            @"INSERT INTO Kullanicilar (kullanici_ad, kullanici_soyad, kullanici_tc, kullanici_tel, kullanici_mail, kullanici_cinsiyet, kullanici_ceza)
              VALUES (N'Zeynep', N'Çelik', N'45678901234', N'05552223344', N'zeynep@mail.com', N'K', 0);",
            @"INSERT INTO Kullanicilar (kullanici_ad, kullanici_soyad, kullanici_tc, kullanici_tel, kullanici_mail, kullanici_cinsiyet, kullanici_ceza)
              VALUES (N'Can', N'Öztürk', N'56789012345', N'05061234567', N'can@mail.com', N'E', 25);",

            @"INSERT INTO Kaynaklar (kaynak_ad, kaynak_yazar, kaynak_yayıncı, kaynak_sayfasayısı, kaynak_basımtarihi)
              VALUES (N'Suç ve Ceza', N'Fyodor Dostoyevski', N'İş Bankası Kültür', 687, '1866-01-01');",
            @"INSERT INTO Kaynaklar (kaynak_ad, kaynak_yazar, kaynak_yayıncı, kaynak_sayfasayısı, kaynak_basımtarihi)
              VALUES (N'Sefiller', N'Victor Hugo', N'Can Yayınları', 512, '1862-01-01');",
            @"INSERT INTO Kaynaklar (kaynak_ad, kaynak_yazar, kaynak_yayıncı, kaynak_sayfasayısı, kaynak_basımtarihi)
              VALUES (N'Simyacı', N'Paulo Coelho', N'Can Yayınları', 184, '1988-01-01');",
            @"INSERT INTO Kaynaklar (kaynak_ad, kaynak_yazar, kaynak_yayıncı, kaynak_sayfasayısı, kaynak_basımtarihi)
              VALUES (N'1984', N'George Orwell', N'Can Yayınları', 352, '1949-06-08');",
            @"INSERT INTO Kaynaklar (kaynak_ad, kaynak_yazar, kaynak_yayıncı, kaynak_sayfasayısı, kaynak_basımtarihi)
              VALUES (N'Küçük Prens', N'Antoine de Saint-Exupéry', N'İş Bankası Kültür', 96, '1943-04-06');",
            @"INSERT INTO Kaynaklar (kaynak_ad, kaynak_yazar, kaynak_yayıncı, kaynak_sayfasayısı, kaynak_basımtarihi)
              VALUES (N'Tutunamayanlar', N'Oğuz Atay', N'Oğlak Yayıncılık', 724, '1972-01-01');",

            // Örnek ödünç: Ahmet (id 1) — Simyacı (id 3), aktif
            @"INSERT INTO kayitlar (kullanici_id, kitap_id, alis_tarihi, son_tarih, durum)
              VALUES (1, 3, DATEADD(day, -5, GETDATE()), DATEADD(day, 10, GETDATE()), 1);",
            // Mehmet (id 3) — 1984 (id 4), iade edilmiş
            @"INSERT INTO kayitlar (kullanici_id, kitap_id, alis_tarihi, son_tarih, durum)
              VALUES (3, 4, DATEADD(day, -30, GETDATE()), DATEADD(day, -15, GETDATE()), 0);"
        };

        foreach (var script in scripts)
            new SqlCommand(script, con).ExecuteNonQuery();
    }

    private static bool TableHasRows(SqlConnection con, string tableName)
    {
        var sql = $"SELECT COUNT(*) FROM {tableName}";
        using var cmd = new SqlCommand(sql, con);
        return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
    }
}
