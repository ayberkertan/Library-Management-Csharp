# Ayberk Kütüphane Otomasyonu

Modern WPF arayüzlü kütüphane yönetim uygulaması. Kullanıcı ve kaynak (kitap) CRUD, ödünç verme / iade, gecikme cezası ve personel girişi içerir.

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4)
![WPF](https://img.shields.io/badge/UI-WPF%20%2B%20Material%20Design-6200EA)
![License](https://img.shields.io/badge/License-MIT-green)

## Özellikler

- Personel girişi (BCrypt hash; eski düz metin DB ile uyumlu)
- Kullanıcı yönetimi (ekle, güncelle, sil, liste)
- Kaynak / kitap yönetimi
- TC ile kullanıcı arama, kitap adına göre filtreleme
- Ödünç verme (15 gün süre, çift ödünç engeli)
- İade alma ve otomatik gecikme cezası (gün × 5 TL)
- SQL Server LocalDB — ilk çalıştırmada veritabanı otomatik oluşur

## Ekran görüntüleri

Ekran görüntülerini `docs/screenshots/` klasörüne ekleyebilirsiniz (ör. `login.png`, `dashboard.png`).

## Gereksinimler

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server Express LocalDB](https://learn.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb)

## Kurulum ve çalıştırma

```powershell
git clone https://github.com/KULLANICI_ADINIZ/Ayberk-Kutuphane-Otomasyonu.git
cd Ayberk-Kutuphane-Otomasyonu
dotnet restore Kutuphane.sln
dotnet run --project src/Kutuphane.App/Kutuphane.App.csproj
```

**Varsayılan giriş (yalnızca geliştirme / ilk kurulum):**

| Alan | Değer |
|------|--------|
| Kullanıcı | `admin` |
| Şifre | `1234` |

> Üretimde şifreyi mutlaka değiştirin. `Kutuphane.mdf` dosyası çalışma klasöründe oluşur ve `.gitignore` ile repoya eklenmez.

## Mimari

```
src/
├── Kutuphane.App/           # WPF + MVVM + Material Design
├── Kutuphane.Core/          # Modeller, arayüzler, validasyon
└── Kutuphane.Infrastructure/ # SQL LocalDB, repository'ler
legacy/
└── kut_oto/                 # Eski WinForms sürümü (referans)
```

- **MVVM:** ViewModel'ler `CommunityToolkit.Mvvm` kullanır
- **Veri erişimi:** `IUserRepository`, `IBookRepository`, `ILoanRepository`, `IAuthService`
- **Yapılandırma:** `appsettings.json` → `ConnectionStrings:LibraryDb`

