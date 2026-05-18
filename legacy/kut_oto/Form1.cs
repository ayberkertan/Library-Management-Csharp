using Microsoft.Data.SqlClient;

namespace kut_oto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void personelGirisbtn_Click(object sender, EventArgs e)
        {
            string gelenAd = adGiristxt.Text;
            string gelenSifre = sifreGiristxt.Text;
            // 1. Bağlantı dizesini tanımla 
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Kutuphane.mdf;Integrated Security=True;";

            // 2. SQL Sorgusunu hazırla
            string query = "SELECT * FROM Personeller WHERE personeller_kullaniciAd=@ad AND personeller_sifre=@sifre";

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand komut = new SqlCommand(query, baglanti);

                    // Parametreleri ekele
                    komut.Parameters.AddWithValue("@ad", adGiristxt.Text);
                    komut.Parameters.AddWithValue("@sifre", sifreGiristxt.Text);

                    baglanti.Open();
                    SqlDataReader oku = komut.ExecuteReader();

                    if (oku.Read()) // Eğer veritabanında eşleşen bir kayıt varsa
                    {
                        MessageBox.Show("Giriş Başarılı! Hoş geldiniz.");                        
                        IslemPaneli panel = new IslemPaneli();
                        panel.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı adı veya şifre hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bağlantı hatası: " + ex.Message);
                }
            }
        }
    }
}
