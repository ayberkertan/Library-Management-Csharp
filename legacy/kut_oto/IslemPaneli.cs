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
using kut_oto.Kaynaklar;

namespace kut_oto
{
    public partial class IslemPaneli : Form
    {
        public IslemPaneli()
        {
            InitializeComponent();
        }

        private void IslemPaneli_Load(object sender, EventArgs e)
        {
            // Kullanıcı butonu girişte kapalıdır
            ekleKullanicibtn.Visible = false;
            güncelleKullanicibtn.Visible = false;
            silKullanicibtn.Visible = false;

            // Kaynak butonu girişte kapalıdır
            ekleKaynakbtn.Visible = false;
            güncelleKaynakbtn.Visible = false;
            silKaynakbtn.Visible = false;
        }

        private KullaniciListeForm klisteForm; //nesneyi metodun dışında gloval tanımlıyoruz
        private void button1_Click(object sender, EventArgs e)
        {
            // eger :kllanıcı ekle butonu şu an gizliyse (yani menü kapalıysa)
            if (ekleKullanicibtn.Visible == false)
            {
                // Alt butonları (Ekle, Güncelle, Sil) ekranda görünür ya
                ekleKullanicibtn.Visible = true;
                güncelleKullanicibtn.Visible = true;
                silKullanicibtn.Visible = true;
                klisteForm = new KullaniciListeForm();
                klisteForm.MdiParent = this;
                klisteForm.Show();
            }
            else
            {
                // değilse menü zaten açıksa (butonlar görünürse)
                ekleKullanicibtn.Visible = false;
                güncelleKullanicibtn.Visible = false;
                silKullanicibtn.Visible = false;
                klisteForm.Close();
            }
        }
        // Formun kendisini ve açık olup olmadığını tutan değişkenler
        KullaniciEkleForm ekleForm;
        bool ekleFormAcikMi = false;
        private void ekleKullanicibtn_Click(object sender, EventArgs e)
        {
            // 1. Eğer form şu an kapalıysa (false ise)
            if (ekleFormAcikMi == false)
            {
                ekleForm = new KullaniciEkleForm(); // Yeni formu oluştur
                ekleForm.MdiParent = this; // Ana panelin içinde kalmasını sağla
                ekleForm.Show(); // Formu göster
                ekleFormAcikMi = true; // Artık açık olduğunu kaydet
            }
            // 2. Eğer form zaten açıksa (true ise)
            else
            {
                ekleForm.Close(); // Formu kapat
                ekleFormAcikMi = false; // Kapalı olduğunu kaydet
            }

        }


        KullaniciSilFormu silForm;
        bool silAcik = false;
        private void silKullanicibtn_Click(object sender, EventArgs e)
        {
            if (silAcik == false)
            {
                silForm = new KullaniciSilFormu();
                silForm.MdiParent = this; 
                silForm.Show();
                silAcik = true;
            }
            else
            {
                silForm.Close();
                silAcik = false;
            }
        }


        KullaniciGüncelleForm guncelleForm;
        bool guncelleAcik = false;

        private void güncelleKullanicibtn_Click(object sender, EventArgs e)
        {
            if (guncelleAcik == false)
            {

                guncelleForm = new KullaniciGüncelleForm();
                guncelleForm.MdiParent = this; 
                guncelleForm.Show();
                guncelleAcik = true; 
            }

            else
            {
                guncelleForm.Close();
                guncelleAcik = false;
            }
        }

        private KaynakListeForm klisteKaynakForm;
        private void button2_Click(object sender, EventArgs e)
        {
            if (ekleKaynakbtn.Visible == false)
            {
                ekleKaynakbtn.Visible = true;
                güncelleKaynakbtn.Visible = true;
                silKaynakbtn.Visible = true;
                klisteKaynakForm = new KaynakListeForm();
                klisteKaynakForm.MdiParent = this;
                klisteKaynakForm.Show();
            }
            else
            {
                ekleKaynakbtn.Visible = false;
                güncelleKaynakbtn.Visible = false;
                silKaynakbtn.Visible = false;
                if (klisteKaynakForm != null && !klisteKaynakForm.IsDisposed)
                    klisteKaynakForm.Close();
            }
        }

        KaynakEkleForm ekleKaynakForm;
        bool ekleKaynakAcik = false;
        private void ekleKaynakbtn_Click(object sender, EventArgs e)
        {
            if (ekleKaynakAcik == false)
            {
                ekleKaynakForm = new KaynakEkleForm();
                ekleKaynakForm.MdiParent = this;
                ekleKaynakForm.FormClosed += (s, args) => ekleKaynakAcik = false;
                ekleKaynakForm.Show();
                ekleKaynakAcik = true;
            }
            else
            {
                ekleKaynakForm.Close();
                ekleKaynakAcik = false;
            }
        }

        KaynakSilForm silKaynakForm;
        bool silKaynakAcik = false;
        private void silKaynakbtn_Click(object sender, EventArgs e)
        {
            if (silKaynakAcik == false)
            {
                silKaynakForm = new KaynakSilForm();
                silKaynakForm.MdiParent = this;
                silKaynakForm.FormClosed += (s, args) => silKaynakAcik = false;
                silKaynakForm.Show();
                silKaynakAcik = true;
            }
            else
            {
                silKaynakForm.Close();
                silKaynakAcik = false;
            }
        }

        KaynakGüncelleForm guncelleKaynakForm;
        bool guncelleKaynakAcik = false;
        private void güncelleKaynakbtn_Click(object sender, EventArgs e)
        {
            if (guncelleKaynakAcik == false)
            {
                guncelleKaynakForm = new KaynakGüncelleForm();
                guncelleKaynakForm.MdiParent = this;
                guncelleKaynakForm.FormClosed += (s, args) => guncelleKaynakAcik = false;
                guncelleKaynakForm.Show();
                guncelleKaynakAcik = true;
            }
            else
            {
                guncelleKaynakForm.Close();
                guncelleKaynakAcik = false;
            }
        }

        OduncVerForm oduncVerForm;
        bool oduncVerAcik = false;
        private void button3_Click(object sender, EventArgs e)
        {
            if (oduncVerAcik == false)
            {
                oduncVerForm = new OduncVerForm();
                oduncVerForm.MdiParent = this;
                oduncVerForm.FormClosed += (s, args) => oduncVerAcik = false;
                oduncVerForm.Show();
                oduncVerAcik = true;
            }
            else
            {
                oduncVerForm.Close();
                oduncVerAcik = false;
            }
        }

        GeriAlForm geriAlForm;
        bool geriAlAcik = false;
        private void button4_Click(object sender, EventArgs e)
        {
            if (geriAlAcik == false)
            {
                geriAlForm = new GeriAlForm();
                geriAlForm.MdiParent = this;
                geriAlForm.FormClosed += (s, args) => geriAlAcik = false;
                geriAlForm.Show();
                geriAlAcik = true;
            }
            else
            {
                geriAlForm.Close();
                geriAlAcik = false;
            }
        }
    }
}

