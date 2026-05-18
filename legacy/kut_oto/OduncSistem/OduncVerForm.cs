using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kut_oto
{
    public partial class OduncVerForm : Form
    {
        public OduncVerForm()
        {
            InitializeComponent();
        }
        private void KayitlariListele()
        {
            string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Kutuphane.mdf;Integrated Security=True;";

            // JOIN kullanarak ID yerine isimleri getiriyoruz
            string sorgu = @"SELECT k.kayit_id, u.kullanici_ad + ' ' + u.kullanici_soyad AS [Kullanıcı], 
                     kn.kaynak_ad AS [Kitap], k.alis_tarihi AS [Alış Tarihi], 
                     k.son_tarih AS [İade Tarihi], k.durum AS [Durum]
                     FROM kayitlar k
                     JOIN Kullanicilar u ON k.kullanici_id = u.kullanici_ıd
                     JOIN Kaynaklar kn ON k.kitap_id = kn.kaynak_id";

            using (SqlConnection baglanti = new SqlConnection(conString))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kayıtlar listelenirken hata: " + ex.Message);
                }
            }
        }
        private void OduncVerForm_Load(object sender, EventArgs e)
        {
            KayitlariListele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Kutuphane.mdf;Integrated Security=True;";

            string sorgu = "SELECT kullanici_ad, kullanici_soyad FROM Kullanicilar WHERE kullanici_tc = @tc";

            using (SqlConnection baglanti = new SqlConnection(conString))
            {
                try
                {
                    SqlCommand komut = new SqlCommand(sorgu, baglanti);

                    komut.Parameters.AddWithValue("@tc", textBox1.Text);

                    baglanti.Open(); // Bağlantıyı açıyoruz

                    // 6. SqlDataReader ile veritabanından satır satır okuma yapıyoruz
                    SqlDataReader dr = komut.ExecuteReader();

                    // 7. Eğer bir sonuç bulunduysa (Yani bu TC ye sahip biri varsa)
                    if (dr.Read())
                    {
                        // Bulunan Ad ve Soyadı label2'ye yazdırıyoruz
                        label2.Text = "Bulunan Kullanıcı: " + dr["kullanici_ad"].ToString() + " " + dr["kullanici_soyad"].ToString();
                        label2.ForeColor = Color.Green; // Başarılıysa yazısı yazar yazıyı yeşil yapaar
                    }
                    else
                    {
                        // 8. Eğer dr.Read() false dönerse eşleşen kimse yok demektir
                        label2.Text = "Böyle bir kullanıcı bulunamadı!";
                        label2.ForeColor = Color.Red; // Hata durumunda yazıyı yazar yazıyı kırmızı yapaar
                    }
                }
                catch (Exception ex)
                {
                    //Olası bir bağlantı hatasında kullanıcıya bilgi veriyor
                    MessageBox.Show("Veritabanı hatası: " + ex.Message);
                }
            }
        }
        // Bu metot, dışarıdan gelen bir kelimeye göre kitapları filtreler
        private void KitapAra(string arananKelime)
        {
            // Veritabanı bağlantı adresi
            string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Kutuphane.mdf;Integrated Security=True;";

            //SQL LIKE sorgusu: Kitap adının içinde aranan kelime geçenleri getirir
            string sorgu = "SELECT * FROM Kaynaklar WHERE kaynak_ad LIKE @kelime";

            using (SqlConnection baglanti = new SqlConnection(conString))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
                    //Parametreyi SQL'e gönderiyoruz
                    da.SelectCommand.Parameters.AddWithValue("@kelime", "%" + arananKelime + "%");

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    //Filtrelenmiş sonuçları sağdaki DataGridView e basıyoruz
                    dataGridView2.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Arama sırasında hata: " + ex.Message);
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Kullanıcı her harf değiştirdiğinde yazdığı metni alıp metodumuza gönderiyoruz
            KitapAra(textBox2.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
 
            // TC den bulduğumuz kullanıcının ID sini ve DataGridView den seçtiğimiz kitabın ID sini kullanacağız
 
            // önce o TC ye ait kullanıcının ID sini veritabanından çekmemiz gerekir

            string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Kutuphane.mdf;Integrated Security=True;";

            using (SqlConnection baglanti = new SqlConnection(conString))
            {
                try
                {
                    baglanti.Open();

                    //Önce TC'den Kullanıcı ID sini öğreniyoruz
                    string kullaniciSorgu = "SELECT kullanici_ıd FROM Kullanicilar WHERE kullanici_tc = @tc";
                    SqlCommand kulKomut = new SqlCommand(kullaniciSorgu, baglanti);
                    kulKomut.Parameters.AddWithValue("@tc", textBox1.Text);
                    int kullaniciId = Convert.ToInt32(kulKomut.ExecuteScalar());

                    // DataGridView'den (Sağdaki tablo) seçili kitabın IDsini alalıyor
                    int kitapId = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value);

                    //Şimdi 'kayitlar' tablosuna ekleme yapalım
                    //Alış tarihi bugün, iade tarihi ise 15 gün sonrası yapıyorum
                    string ekleSorgu = @"INSERT INTO kayitlar (kullanici_id, kitap_id, alis_tarihi, son_tarih, durum) 
                     VALUES (@kulId, @kitapId, @alis, @son, @durum)";

                    SqlCommand ekleKomut = new SqlCommand(ekleSorgu, baglanti);
                    ekleKomut.Parameters.AddWithValue("@kulId", kullaniciId);
                    ekleKomut.Parameters.AddWithValue("@kitapId", kitapId);
                    ekleKomut.Parameters.AddWithValue("@alis", DateTime.Now); // Bugünün tarihi atıyor
                    ekleKomut.Parameters.AddWithValue("@son", DateTime.Now.AddDays(15)); // 15 gün süre veriyoruz
                    ekleKomut.Parameters.AddWithValue("@durum", true); // true = Kitap şu an kullanıcıda demek

                    ekleKomut.ExecuteNonQuery();

                    MessageBox.Show("Ödünç verme işlemi başarıyla kaydedildi! \nİade Tarihi: " + DateTime.Now.AddDays(15).ToShortDateString());

                    // 3. Alt taraftaki (büyük) DataGridView'i güncellemek için listeleme metodunu çağır
                    KayitlariListele();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("İşlem sırasında bir hata oluştu: " + ex.Message);
                }
            }
        }
    }
}
