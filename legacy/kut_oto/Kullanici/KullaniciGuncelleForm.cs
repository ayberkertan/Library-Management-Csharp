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
    public partial class KullaniciGüncelleForm : Form
    {
        public KullaniciGüncelleForm()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // 1. Hangi ID li kullanıcıyı güncelleyeceğimizi seçili satırdan alıyoruz
            int seciliId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["kullanici_ıd"].Value);

            //sql baglantı
            string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Kutuphane.mdf;Integrated Security=True;";

            // 2. update sorgusu yapıyoruz
            string sorgu = "UPDATE Kullanicilar SET kullanici_ad=@ad, kullanici_soyad=@soyad, kullanici_tc=@tc, " +
                           "kullanici_tel=@tel, kullanici_mail=@mail, kullanici_cinsiyet=@cinsiyet, kullanici_ceza=@ceza " +
                           "WHERE kullanici_ıd=@id";

            using (SqlConnection baglanti = new SqlConnection(conString))
            {
                try
                {
                    SqlCommand komut = new SqlCommand(sorgu, baglanti);
                    komut.Parameters.AddWithValue("@id", seciliId);
                    komut.Parameters.AddWithValue("@ad", kullaniciAdtxt.Text);
                    komut.Parameters.AddWithValue("@soyad", kullaniciSoyadtxt.Text);
                    komut.Parameters.AddWithValue("@tc", kullaniciTctxt.Text);
                    komut.Parameters.AddWithValue("@tel", kullaniciTeltxt.Text);
                    komut.Parameters.AddWithValue("@mail", kullaniciMailtxt.Text);
                    komut.Parameters.AddWithValue("@ceza", Convert.ToDouble(kullaniciCezatxt.Text));
                    komut.Parameters.AddWithValue("@cinsiyet", radioE.Checked ? "E" : "K");

                    baglanti.Open();
                    komut.ExecuteNonQuery();

                    MessageBox.Show("Kullanıcı bilgileri başarıyla güncellendi!");

                    VerileriListele(); // Listeleme metodunu buraya da ekliyoruz ki tablo tekrar yenilensin güncellemeden sonra
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Güncelleme hatası: " + ex.Message);
                }
            }

        }
        private void VerileriListele()
        {
            string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Kutuphane.mdf;Integrated Security=True;";
            using (SqlConnection baglanti = new SqlConnection(conString))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Kullanicilar", baglanti);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Liste güncellenirken hata oluştu: " + ex.Message);
                }
            }
        }

        private void KullaniciGüncelleForm_Load(object sender, EventArgs e)
        {
            VerileriListele();
        }



        //bu metodun amacı; kullanıcının tablodan (DataGridView) bir satıra tıkladığında, o satırdaki bilgilerin otomatik olarak yandaki kutucuklara (TextBox) dolmasını sağlamaktır. Bu sayede kullanıcı, güncelleme veya silme yaparken bilgileri tek tek elle yazmak zorunda kalmıyor
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                

                //içndeki değeriyle birlikle çağırıyor
                kullaniciAdtxt.Text = row.Cells["kullanici_ad"].Value.ToString();
                kullaniciSoyadtxt.Text = row.Cells["kullanici_soyad"].Value.ToString();
                kullaniciTctxt.Text = row.Cells["kullanici_tc"].Value.ToString();
                kullaniciTeltxt.Text = row.Cells["kullanici_tel"].Value.ToString();
                kullaniciMailtxt.Text = row.Cells["kullanici_mail"].Value.ToString();

                // CEZA AYARI 
                kullaniciCezatxt.Text = row.Cells["kullanici_ceza"].Value.ToString();

                string cinsiyet = row.Cells["kullanici_cinsiyet"].Value.ToString();
                if (cinsiyet == "E") radioE.Checked = true; else radioK.Checked = true;
            }
        }
    }
}
