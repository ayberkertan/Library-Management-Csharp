using System.IO;
using System.Windows;
using Kutuphane.App.Services;
using Kutuphane.App.ViewModels;
using Kutuphane.App.Windows;
using Kutuphane.Infrastructure;
using Kutuphane.Infrastructure.Configuration;
using Kutuphane.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kutuphane.App;

public partial class App : Application
{
    public static IServiceProvider Services { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var appDir = AppContext.BaseDirectory;
        AppDomain.CurrentDomain.SetData("DataDirectory", appDir);

        var configuration = new ConfigurationBuilder()
            .SetBasePath(appDir)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionOptions = new ConnectionOptions();
        configuration.GetSection(ConnectionOptions.SectionName).Bind(connectionOptions);

        var services = new ServiceCollection();
        services.AddSingleton(configuration);
        services.AddInfrastructure(connectionOptions);
        services.AddSingleton<NavigationService>();
        services.AddTransient<LoginViewModel>();
        services.AddTransient<ShellViewModel>();
        services.AddTransient<UsersViewModel>();
        services.AddTransient<BooksViewModel>();
        services.AddTransient<LendViewModel>();
        services.AddTransient<ReturnViewModel>();
        services.AddTransient<LoginWindow>();
        services.AddTransient<MainWindow>();

        Services = services.BuildServiceProvider();

        try
        {
            Services.GetRequiredService<DatabaseInitializer>().EnsureDatabaseExists();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Veritabanı oluşturulamadı:\n{ex.Message}\n\n" +
                "Eski proje kaydı kalmış olabilir. Çözüm:\n" +
                "1) scripts\\TemizleLocalDB.sql dosyasını LocalDB üzerinde çalıştırın\n" +
                "2) Uygulamayı yeniden başlatın\n\n" +
                "SQL Server LocalDB kurulu olmalıdır.",
                "Veritabanı Hatası",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            Shutdown();
            return;
        }

        var login = Services.GetRequiredService<LoginWindow>();
        login.Show();
    }
}
