namespace Kutuphane.Core.Interfaces;

public interface IAuthService
{
    bool TryLogin(string username, string password);
}
