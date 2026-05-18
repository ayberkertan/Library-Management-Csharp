namespace Kutuphane.Infrastructure.Configuration;

public class ConnectionOptions
{
    public const string SectionName = "ConnectionStrings";
    public string LibraryDb { get; set; } = string.Empty;
}
