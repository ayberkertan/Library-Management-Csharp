namespace Kutuphane.Core.Models;

public class Book
{
    public int Id { get; set; }
    public string Ad { get; set; } = string.Empty;
    public string Yazar { get; set; } = string.Empty;
    public string Yayinci { get; set; } = string.Empty;
    public int SayfaSayisi { get; set; }
    public DateTime BasimTarihi { get; set; }
}
