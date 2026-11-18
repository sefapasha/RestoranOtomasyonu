using System;
using System.Windows.Forms;
using RestoranOtomasyonu.UI;

namespace RestoranOtomasyonu
{
    public partial class frmAnaSayfa : Form
    {
        public string GirenRol { get; set; }
        public string GirenKullaniciAdi { get; set; }

        public frmAnaSayfa()
        {
            InitializeComponent();
            this.FormClosed += (s, e) => Application.Exit();
        }

        private void frmAnaSayfa_Load(object sender, EventArgs e)
        {
            string rol = GirenRol.ToLower().Trim();

            // GARSON YETKİLERİ
            if (rol == "garson")
            {
                btnPaketRapor.Visible = false;
                btnMenu.Visible = false;
                btnIstatistik.Visible = false;
                btnPersonel.Visible = false;
            }
            // KASİYER YETKİLERİ
            else if (rol == "kasiyer")
            {
                btnMenu.Visible = false;
                btnIstatistik.Visible = false;
                btnPersonel.Visible = false;
                btnPaketRapor.Visible = false;
            }
        }


        private void btnMasalar_Click(object sender, EventArgs e)
        {
            frmMasalar masalarFormu = new frmMasalar();
            masalarFormu.ShowDialog();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            DialogResult sonuc = MessageBox.Show("Sistemden çıkmak istediğinize emin misiniz?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (sonuc == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnIstatistik_Click(object sender, EventArgs e)
        {
            frmRaporlar raporFormu = new frmRaporlar();
            raporFormu.ShowDialog();
        }

        private void btnPersonel_Click(object sender, EventArgs e)
        {
            frmPersonelYonetimi personelFormu = new frmPersonelYonetimi();
            personelFormu.ShowDialog();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            frmUrunYonetimi urunYonetimi = new frmUrunYonetimi();

            urunYonetimi.AktifKullanici = this.GirenKullaniciAdi;
            urunYonetimi.AktifRol = this.GirenRol;

            urunYonetimi.ShowDialog();
        }

        private void btnPaketServis_Click(object sender, EventArgs e)
        {
            frmPaketServis paketForm = new frmPaketServis();

            paketForm.AktifKullanici = this.GirenKullaniciAdi; 
            paketForm.AktifRol = this.GirenRol;

            paketForm.Show();
        }

        private void btnPaketRapor_Click(object sender, EventArgs e)
        {
            frmPaketRapor frm = new frmPaketRapor();
            frm.ShowDialog();
        }

        private void btnMusteriYorumlari_Click(object sender, EventArgs e)
        {
            frmMusteriYorumlari frm = new frmMusteriYorumlari();
            frm.ShowDialog();
        }
    }
}
