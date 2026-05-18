# Katkı Rehberi

Bu projeye katkıda bulunmak istediğiniz için teşekkürler.

## Geliştirme ortamı

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server LocalDB](https://learn.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb)
- Visual Studio 2022 veya VS Code + C# Dev Kit

## Dal (branch) akışı

1. `main` dalından yeni bir dal oluşturun: `feat/kisa-aciklama` veya `fix/hata-aciklama`
2. Değişikliklerinizi yapın ve yerelde derleyin:
   ```powershell
   dotnet build Kutuphane.sln -c Release
   ```
3. Anlamlı commit mesajları kullanın (ör. `feat: kullanıcı arama filtresi`)
4. Pull request açın ve ne değiştiğini kısaca açıklayın

## Kod standartları

- MVVM: SQL ve iş mantığı ViewModel'de değil, Infrastructure katmanında
- Yeni özellikler için uygun repository arayüzü ekleyin
- Türkçe UI metinleri, İngilizce kod isimleri tercih edilir

## Pull request kontrol listesi

- [ ] `dotnet build` hatasız tamamlanıyor
- [ ] Yeni bağımlılık varsa README güncellendi
- [ ] Gizli bilgi (şifre, connection string) commit edilmedi
