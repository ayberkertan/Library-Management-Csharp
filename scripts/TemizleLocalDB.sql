-- Eski / bozuk KutuphaneDB kaydını LocalDB'den temizler.
-- Hata: "fixed_project\kut_oto\...\Kutuphane.mdf bulunamıyor" alıyorsanız bunu çalıştırın.
-- SSMS veya Visual Studio > View > SQL Server Object Explorer > (localdb)\MSSQLLocalDB > New Query

USE master;
GO

IF EXISTS (SELECT 1 FROM sys.databases WHERE name = N'KutuphaneDB')
BEGIN
    ALTER DATABASE [KutuphaneDB] SET EMERGENCY;
    ALTER DATABASE [KutuphaneDB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [KutuphaneDB];
    PRINT N'KutuphaneDB silindi.';
END
ELSE
    PRINT N'KutuphaneDB zaten yok.';
GO
