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

namespace kut_oto.Kaynaklar
{
    public partial class KaynakEkleForm : Form
    {
        public KaynakEkleForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Kutuphane.mdf;Integrated Security=True;";

            // SQL Sorgusu 
            string sorgu = "INSERT INTO Kaynaklar (kaynak_ad, kaynak_yazar, kaynak_yayıncı, kaynak_sayfasayısı, kaynak_basımtarihi) " +
                           "VALUES (@ad, @yazar, @yayıncı, @sayfa, @tarih)";

            using (SqlConnection baglanti = new SqlConnection(conString))
            {
                try
                {
                    SqlCommand komut = new SqlCommand(sorgu, baglanti);

                    // Parametreleri formdaki nesnelerden alıyoruz
                    komut.Parameters.AddWithValue("@ad", kaynakAdtxt.Text);
                    komut.Parameters.AddWithValue("@yazar", kaynakYazartxt.Text);
                    komut.Parameters.AddWithValue("@yayıncı", kaynakYayincitxt.Text);

                    // NumericUpDown değeri için 
                    komut.Parameters.AddWithValue("@sayfa", numericUpDown1.Value);

                    // DateTimePicker değeri için
                    komut.Parameters.AddWithValue("@tarih", dateTimePicker1.Value);

                    baglanti.Open();
                    komut.ExecuteNonQuery();

                    MessageBox.Show("Kitap başarıyla eklendi!");

                    // Tabloyu anında güncellemek için metodumuzu çağırıyoruz
                    KaynaklariListele();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ekleme hatası: " + ex.Message);
                }
            }
        }
            private void KaynaklariListele()
            {
            string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Kutuphane.mdf;Integrated Security=True;";
            using (SqlConnection baglanti = new SqlConnection(conString))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Kaynaklar", baglanti);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void KaynakEkleForm_Load(object sender, EventArgs e)
        {
            KaynaklariListele();
        }
    }
}

