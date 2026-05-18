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
    public partial class KaynakGüncelleForm : Form
    {
        public KaynakGüncelleForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Seçili satırdaki ID yi al
            int seciliId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Kutuphane.mdf;Integrated Security=True;";


            string sorgu = "UPDATE Kaynaklar SET kaynak_ad=@ad, kaynak_yazar=@yazar, kaynak_yayıncı=@yayinci, " +
                           "kaynak_sayfasayısı=@sayfa, kaynak_basımtarihi=@tarih WHERE kaynak_id=@id";

            using (SqlConnection baglanti = new SqlConnection(conString))
            {
                try
                {
                    SqlCommand komut = new SqlCommand(sorgu, baglanti);

                    // Parametreleri nesnelerden alıyoruz
                    komut.Parameters.AddWithValue("@id", seciliId);
                    komut.Parameters.AddWithValue("@ad", kaynakAdtxt.Text);
                    komut.Parameters.AddWithValue("@yazar", kaynakYazartxt.Text);
                    komut.Parameters.AddWithValue("@yayinci", kaynakYayincitxt.Text);

                    // NumericUpDown ve DateTimePicker verilerini alıyoruz
                    komut.Parameters.AddWithValue("@sayfa", numericUpDown1.Value);
                    komut.Parameters.AddWithValue("@tarih", dateTimePicker1.Value);

                    baglanti.Open();
                    komut.ExecuteNonQuery();

                    MessageBox.Show("Kitap başarıyla güncellendi!");

                    // Tabloyu yeniler
                    KaynaklariListele(); 
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
        private void KaynaklariListele()
        {
            // Bağlantı cümlesi
            string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Kutuphane.mdf;Integrated Security=True;";

            using (SqlConnection baglanti = new SqlConnection(conString))
            {
                try
                {
                    // Kaynaklar tablosundaki her şeyi seç
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Kaynaklar", baglanti);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Tablodaki verileri DataGridView e aktar
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Liste yenilenirken bir hata oluştu: " + ex.Message);
                }
            }
        }

        private void KaynakGüncelleForm_Load(object sender, EventArgs e)
        {
            KaynaklariListele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Başlık satırına tıklarsan hata vermemesi için
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                    // Kutuları dolduruyoruz
                    kaynakAdtxt.Text = row.Cells["kaynak_ad"].Value.ToString();
                    kaynakYazartxt.Text = row.Cells["kaynak_yazar"].Value.ToString();
                    kaynakYayincitxt.Text = row.Cells["kaynak_yayıncı"].Value.ToString();
                    numericUpDown1.Value = Convert.ToDecimal(row.Cells["kaynak_sayfasayısı"].Value);
                    dateTimePicker1.Value = Convert.ToDateTime(row.Cells["kaynak_basımtarihi"].Value);
                }
            }
            catch (Exception ex)
            {
                // Eğer bir sütun ismi yanlışsa burada sana "Hata budur" diyecek
                MessageBox.Show("Veri çekme hatası: " + ex.Message);
            }
        }
    }
}
