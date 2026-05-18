-- KütüphaneOtomasyonu LocalDB Kurulum Betiği
-- Çalıştırma: Projeyi ilk kez açmadan önce bu betiği çalıştırın
-- VEYA uygulama içinden otomatik çalıştırılır (DatabaseHelper.cs varsa)

CREATE TABLE Personeller (
    personeller_id       INT IDENTITY(1,1) PRIMARY KEY,
    personeller_kullaniciAd NVARCHAR(50) NOT NULL,
    personeller_sifre    NVARCHAR(50) NOT NULL
);

CREATE TABLE Kullanicilar (
    kullanici_ıd         INT IDENTITY(1,1) PRIMARY KEY,
    kullanici_ad         NVARCHAR(50),
    kullanici_soyad      NVARCHAR(50),
    kullanici_tc         NVARCHAR(11),
    kullanici_tel        NVARCHAR(20),
    kullanici_mail       NVARCHAR(100),
    kullanici_cinsiyet   NCHAR(1),
    kullanici_ceza       FLOAT DEFAULT 0
);

CREATE TABLE Kaynaklar (
    kaynak_id            INT IDENTITY(1,1) PRIMARY KEY,
    kaynak_ad            NVARCHAR(200),
    kaynak_yazar         NVARCHAR(100),
    kaynak_yayıncı       NVARCHAR(100),
    kaynak_sayfasayısı   INT,
    kaynak_basımtarihi   DATE
);

CREATE TABLE kayitlar (
    kayit_id             INT IDENTITY(1,1) PRIMARY KEY,
    kullanici_id         INT NOT NULL,
    kitap_id             INT NOT NULL,
    alis_tarihi          DATETIME,
    son_tarih            DATETIME,
    durum                BIT DEFAULT 1,
    FOREIGN KEY (kullanici_id) REFERENCES Kullanicilar(kullanici_ıd),
    FOREIGN KEY (kitap_id)     REFERENCES Kaynaklar(kaynak_id)
);

-- Varsayılan admin hesabı (kullanıcı adı: admin, şifre: 1234)
INSERT INTO Personeller (personeller_kullaniciAd, personeller_sifre)
VALUES (N'admin', N'1234');
