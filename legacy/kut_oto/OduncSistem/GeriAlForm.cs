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
    public partial class GeriAlForm : Form
    {
        public GeriAlForm()
        {
            InitializeComponent();
        }

        private void KayitlariListele()
        {
            // 1. Veritabanı bağlantı adresimiz
            string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Kutuphane.mdf;Integrated Security=True;";

            // 2. JOIN kullanarak ID'ler yerine isimleri getiriyoruz. 
            // WHERE durum = 1 diyerek sadece şu an dışarıda olan kitapları listeliyoruz.
            string sorgu = @"SELECT k.kayit_id, u.kullanici_ad + ' ' + u.kullanici_soyad AS [Kullanıcı], 
                     kn.kaynak_ad AS [Kitap], k.alis_tarihi AS [Alış Tarihi], 
                     k.son_tarih AS [İade Tarihi]
                     FROM kayitlar k
                     JOIN Kullanicilar u ON k.kullanici_id = u.kullanici_ıd
                     JOIN Kaynaklar kn ON k.kitap_id = kn.kaynak_id
                     WHERE k.durum = 1";

            using (SqlConnection baglanti = new SqlConnection(conString))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // 3. Verileri DataGridView'e aktar
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Listeleme hatası: " + ex.Message);
                }
            }
        }

        private void GeriAlForm_Load(object sender, EventArgs e)
        {
            KayitlariListele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Seçili satırdaki Kayıt ID sini alıyoruz
            int seciliKayitId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

            string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Kutuphane.mdf;Integrated Security=True;";

            // UPDATE sorgusu ile durum bilgisini 0 (false) yapıyoruz.
            // Bu, "Kitap kütüphaneye geri döndü" demektir.
            string sorgu = "UPDATE kayitlar SET durum = 0 WHERE kayit_id = @id";

            using (SqlConnection baglanti = new SqlConnection(conString))
            {
                try
                {
                    SqlCommand komut = new SqlCommand(sorgu, baglanti);
                    komut.Parameters.AddWithValue("@id", seciliKayitId);

                    baglanti.Open();
                    komut.ExecuteNonQuery();

                    MessageBox.Show("Kitap iade işlemi başarıyla tamamlandı!");

                    // Tabloyu yeniliyoruz (İade edilen kitap listeden düşer çünkü WHERE durum=1 yazdık)
                    KayitlariListele();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("İade hatası: " + ex.Message);
                }
            }
        }
    }
}
