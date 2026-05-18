using Kutuphane.Infrastructure.Configuration;
using Microsoft.Data.SqlClient;

namespace Kutuphane.Infrastructure.Data;

public class SqlConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(ConnectionOptions options)
    {
        var raw = options.LibraryDb;
        var dataDir = AppDomain.CurrentDomain.GetData("DataDirectory") as string
                      ?? AppContext.BaseDirectory;
        _connectionString = raw.Replace("|DataDirectory|", dataDir.TrimEnd('\\', '/'));
    }

    public SqlConnection Create() => new(_connectionString);
}
