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
    public partial class KaynakSilForm : Form
    {
        public KaynakSilForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 1. DataGridView den seçili satırın ID sini alıyoruz 
            int seciliId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

            string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Kutuphane.mdf;Integrated Security=True;";

            // 2. SQL Silme Sorgusu 
            string sorgu = "DELETE FROM Kaynaklar WHERE kaynak_id = @id";

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
                        MessageBox.Show("Kitap başarıyla silindi!");

                        // Listeyi anında tazeler
                        KaynaklariListele();
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Silme hatası: " + ex.Message);
                }
            }
        }

        //listeleme methodu
        private void KaynaklariListele()
        {
            string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Kutuphane.mdf;Integrated Security=True;";
            using (SqlConnection baglanti = new SqlConnection(conString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Kaynaklar", baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        // Form açıldığında liste dolsun diye:
        private void KaynakSilForm_Load_1(object sender, EventArgs e)
        {
            KaynaklariListele();
        }
    }

}

