namespace Kutuphane.App.Services;

public class NavigationService
{
    public event Action<Type>? NavigateRequested;

    public void NavigateTo<TViewModel>() where TViewModel : class
    {
        NavigateRequested?.Invoke(typeof(TViewModel));
    }
}
