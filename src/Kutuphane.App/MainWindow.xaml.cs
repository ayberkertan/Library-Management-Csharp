using System.Windows;
using Kutuphane.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Kutuphane.App;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = App.Services.GetRequiredService<ShellViewModel>();
    }
}
