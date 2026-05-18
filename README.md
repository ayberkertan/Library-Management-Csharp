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

## 📸 Ekran görüntüleri 

**Giriş Ekranı (Login)**

*<img width="402" height="465" alt="Image" src="https://github.com/user-attachments/assets/af37392a-34e8-4927-b17e-a179fb7b96ef" />*

**Kullanıcı Yönetimi (User Management)**
*<img width="1365" height="719" alt="Image" src="https://github.com/user-attachments/assets/b0921f9a-abd7-4fd4-87e6-ec3f25fdae3f" />*

**Kaynaklar (Resources)**
*<img width="1365" height="709" alt="Image" src="https://github.com/user-attachments/assets/322e9a4b-ae19-4ef8-a011-94aff15b5894" />*

**Ödünç Verme ve İade (Borrow & Return)**
*<img width="1365" height="717" alt="Image" src="https://github.com/user-attachments/assets/60e42fa2-6b29-4081-b1fa-54303a0b117c" />*

*<img width="1365" height="717" alt="Image" src="https://github.com/user-attachments/assets/9ef38cbd-8062-4016-98d3-6a5214cf6f77" />*

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

