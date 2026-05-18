using Kutuphane.Core.Interfaces;
using Kutuphane.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace Kutuphane.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly SqlConnectionFactory _connectionFactory;

    public AuthService(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public bool TryLogin(string username, string password)
    {
        const string query =
            "SELECT personeller_sifre FROM Personeller WHERE personeller_kullaniciAd = @ad";

        using var con = _connectionFactory.Create();
        using var cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@ad", username);
        con.Open();

        var result = cmd.ExecuteScalar();
        if (result is not string storedHash)
            return false;

        if (storedHash.StartsWith("$2", StringComparison.Ordinal))
            return BCrypt.Net.BCrypt.Verify(password, storedHash);

        return storedHash == password;
    }
}
