using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Data;

namespace kut_oto
{
    public partial class KullaniciSilFormu : Form
    {
        public KullaniciSilFormu()
        {
            InitializeComponent();
        }
        private void Silbtn_Click(object sender, EventArgs e)
        {
            // DataGridView den seçili satırın ID sini alıyoruz
            // Tablonda ID hangi sütundaysa [0] yerine onun indeksini yazıyoruz 
            int seciliId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

            string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Kutuphane.mdf;Integrated Security=True;";

            // 2. Silme Sorgusu
            string sorgu = "DELETE FROM Kullanicilar WHERE kullanici_ıd = @id";

            using (SqlConnection baglanti = new SqlConnection(conString))
            {
                try
                {
                    SqlCommand komut = new SqlCommand(sorgu, baglanti);
                    komut.Parameters.AddWithValue("@id", seciliId);

                    baglanti.Open();
                    int sonuc = komut.ExecuteNonQuery();

                    if (sonuc > 0)
                    {
                        MessageBox.Show("Kullanıcı silindi!");
                        VerileriListele(); // Tabloyu güncellemek için daha önce yazdığımız metodu çağır
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Silme hatası: " + ex.Message);
                }
            }
        }
            private void VerileriListele()
        {
            string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Kutuphane.mdf;Integrated Security=True;";
            using (SqlConnection baglanti = new SqlConnection(conString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Kullanicilar", baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            }

            // Form yüklendiğinde listeleme yapması için
            private void KullaniciSilFormu_Load(object sender, EventArgs e)
            {
            VerileriListele();
            }
    }
    }

