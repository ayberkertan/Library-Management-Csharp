using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kutuphane.App.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Kutuphane.App.ViewModels;

public partial class ShellViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly NavigationService _navigationService;

    [ObservableProperty]
    private object? _currentViewModel;

    [ObservableProperty]
    private string _pageTitle = "Kullanıcılar";

    public ShellViewModel(IServiceProvider serviceProvider, NavigationService navigationService)
    {
        _serviceProvider = serviceProvider;
        _navigationService = navigationService;
        _navigationService.NavigateRequested += OnNavigateRequested;
        NavigateTo<UsersViewModel>();
    }

    [RelayCommand]
    private void ShowUsers() => NavigateTo<UsersViewModel>();

    [RelayCommand]
    private void ShowBooks() => NavigateTo<BooksViewModel>();

    [RelayCommand]
    private void ShowLend() => NavigateTo<LendViewModel>();

    [RelayCommand]
    private void ShowReturn() => NavigateTo<ReturnViewModel>();

    private void OnNavigateRequested(Type viewModelType)
    {
        CurrentViewModel = _serviceProvider.GetRequiredService(viewModelType);
        PageTitle = viewModelType.Name switch
        {
            nameof(UsersViewModel) => "Kullanıcılar",
            nameof(BooksViewModel) => "Kaynaklar",
            nameof(LendViewModel) => "Ödünç Ver",
            nameof(ReturnViewModel) => "Geri Al",
            _ => "Kütüphane"
        };
    }

    private void NavigateTo<T>() where T : class => _navigationService.NavigateTo<T>();
}
