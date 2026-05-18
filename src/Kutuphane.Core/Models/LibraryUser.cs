namespace Kutuphane.Core.Models;

public class LibraryUser
{
    public int Id { get; set; }
    public string Ad { get; set; } = string.Empty;
    public string Soyad { get; set; } = string.Empty;
    public string Tc { get; set; } = string.Empty;
    public string Tel { get; set; } = string.Empty;
    public string Mail { get; set; } = string.Empty;
    public string Cinsiyet { get; set; } = string.Empty;
    public double Ceza { get; set; }

    public string TamAd => $"{Ad} {Soyad}".Trim();
}
