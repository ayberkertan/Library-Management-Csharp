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
    public partial class KullaniciListeForm : Form
    {
        public KullaniciListeForm()
        {
            InitializeComponent();
        }

        private void KullaniciListeForm_Load(object sender, EventArgs e)
        {
            // 1. baglantı dizesi
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Kutuphane.mdf;Integrated Security=True;";

            // 2. SQL sorgusu yazdık
            string sorgu = "SELECT * FROM Kullanicilar";

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                try
                {
                    // 3. Verileri çekmek için köprü kuruyoruz
                    SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);

                    // 4. Verileri geçici olarak tutacak tablo (DataTable) oluşturuyoruz
                    DataTable dt = new DataTable();

                    // 5. Köprüyü kullanarak verileri SQL'den çekip DataTable a dolduruyoruz
                    da.Fill(dt);

                    // 6. Son olarak bu dolan tabloyu ekrandaki DataGridView'e bağlıyoruz
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata oluştu: " + ex.Message);
                }

            }
        }
    }
}
