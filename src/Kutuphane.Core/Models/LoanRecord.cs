namespace Kutuphane.Core.Models;

public class LoanRecord
{
    public int KayitId { get; set; }
    public int KullaniciId { get; set; }
    public int KitapId { get; set; }
    public string KullaniciAdi { get; set; } = string.Empty;
    public string KitapAdi { get; set; } = string.Empty;
    public DateTime AlisTarihi { get; set; }
    public DateTime SonTarih { get; set; }
    public bool Durum { get; set; }
}
