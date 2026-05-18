using System.Windows;
using Application = System.Windows.Application;
using Kutuphane.App.ViewModels;
using Kutuphane.App.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Kutuphane.App.Windows;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
        var vm = App.Services.GetRequiredService<LoginViewModel>();
        vm.LoginSucceeded += OnLoginSucceeded;
        LoginContent.Content = new LoginView { DataContext = vm };
    }

    private void OnLoginSucceeded()
    {
        var main = App.Services.GetRequiredService<MainWindow>();
        Application.Current.MainWindow = main;
        main.Show();
        Close();
    }
}
