using Kutuphane.Core.Interfaces;
using Kutuphane.Infrastructure.Configuration;
using Kutuphane.Infrastructure.Data;
using Kutuphane.Infrastructure.Repositories;
using Kutuphane.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Kutuphane.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConnectionOptions options)
    {
        services.AddSingleton(options);
        services.AddSingleton<SqlConnectionFactory>();
        services.AddSingleton<DatabaseInitializer>();
        services.AddSingleton<IAuthService, AuthService>();
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IBookRepository, BookRepository>();
        services.AddSingleton<ILoanRepository, LoanRepository>();
        return services;
    }
}
