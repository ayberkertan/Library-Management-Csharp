using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kut_oto
{
    public partial class KullaniciEkleForm : Form
    {
        public KullaniciEkleForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Kutuphane.mdf;Integrated Security=True;";

            // 2. Cinsiyet seçimi kontrolü
            string cinsiyet = "";
            if (radioE.Checked) cinsiyet = "E";
            else if (radioK.Checked) cinsiyet = "K";

            // 3. SQL INSERT Sorgusu
            string sorgu = "INSERT INTO Kullanicilar (kullanici_ad, kullanici_soyad, kullanici_tc, kullanici_tel, kullanici_mail, kullanici_cinsiyet, kullanici_ceza) " +
                           "VALUES (@ad, @soyad, @tc, @tel, @mail, @cinsiyet, @ceza)";

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand komut = new SqlCommand(sorgu, baglanti);

                    // Görseldeki TextBox isimlerine göre parametreler:
                    komut.Parameters.AddWithValue("@ad", kullaniciAdtxt.Text);
                    komut.Parameters.AddWithValue("@soyad", kullaniciSoyadtxt.Text);
                    komut.Parameters.AddWithValue("@tc", kullaniciTctxt.Text);
                    komut.Parameters.AddWithValue("@tel", kullaniciTeltxt.Text);
                    komut.Parameters.AddWithValue("@mail", kullaniciMailtxt.Text);
                    komut.Parameters.AddWithValue("@cinsiyet", cinsiyet);
                    komut.Parameters.AddWithValue("@ceza", 0); // Başlangıç cezası 0

                    baglanti.Open();
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Başarıyla Kaydedildi!");

                    VerileriYenile();

                    MessageBox.Show("Yeni kullanıcı başarıyla eklendi!", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Formu temizlemek istersen:
                    kullaniciAdtxt.Clear();
                    kullaniciSoyadtxt.Clear();
                    kullaniciTctxt.Clear();
                    kullaniciTeltxt.Clear();
                    kullaniciMailtxt.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        // Bu metot çağrıldığında DataGridView ı günceller
        private void VerileriYenile()
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

        private void KullaniciEkleForm_Load(object sender, EventArgs e)
        {
            VerileriYenile();
        }

    }
}

