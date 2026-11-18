using System;
using System.Drawing;
using System.Windows.Forms;

namespace RestoranOtomasyonu.UI
{
    public partial class ucSiparisKarti : UserControl
    {
        public event EventHandler IleriTiklandi;
        public event EventHandler IptalTiklandi;
        public event EventHandler SuresiDoldu;

        public int SiparisID { get; set; }
        private int maxSaniye = 300; // 5 Dakikalık varsayılan süre
        private int kalanSaniye;
        private ToolTip toolTipDetay = new ToolTip();

        public ucSiparisKarti()
        {
            InitializeComponent();
            this.Margin = new Padding(5);
        }

        public void KartiDoldur(int id, string musteri, string adres, string icerik, decimal tutar, DateTime? siparisZamani = null)
        {
            SiparisID = id;

            if (Controls.ContainsKey("lblSiparisID"))
            {
                Controls["lblSiparisID"].Text = "Sipariş ID: #" + id.ToString();
            }

            lblMusteriAdi.Text = musteri;
            lblAdres.Text = adres;
            lblSiparisIcerigi.Text = icerik;
            lblTutar.Text = tutar.ToString("C2");

            lblAdres.AutoEllipsis = true;
            lblSiparisIcerigi.AutoEllipsis = true;
            toolTipDetay.SetToolTip(lblAdres, adres);
            toolTipDetay.SetToolTip(lblSiparisIcerigi, icerik);

            if (btnDurum != null) btnDurum.Visible = true;
            if (btnIptal != null) btnIptal.Visible = true;

            if (siparisZamani.HasValue)
            {
                TimeSpan gecenSure = DateTime.Now - siparisZamani.Value;
                kalanSaniye = maxSaniye - (int)gecenSure.TotalSeconds;
            }
            else
            {
                kalanSaniye = maxSaniye;
            }

            if (kalanSaniye <= 0)
            {
                kalanSaniye = 0;
                prgSure.Value = 0;
                lblSure.Text = "00:00";
                this.BackColor = Color.LightGray;
            }
            else
            {
                prgSure.Maximum = maxSaniye;
                prgSure.Value = Math.Min(kalanSaniye, maxSaniye);

                TimeSpan zaman = TimeSpan.FromSeconds(kalanSaniye);
                lblSure.Text = zaman.ToString(@"mm\:ss");
                tmrSure.Start();
            }
        }

        private void btnDurum_Click(object sender, EventArgs e)
        {
            IleriTiklandi?.Invoke(this, e);
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            IptalTiklandi?.Invoke(this, e);
        }

        public void YoldaModunaGec()
        {
            if (btnDurum != null) btnDurum.Visible = false;
            if (btnIptal != null) btnIptal.Visible = false;
        }

        public void TeslimEdildiYap()
        {
            if (tmrSure != null) tmrSure.Stop();

            this.BackColor = Color.FromArgb(210, 245, 210);

            prgSure.Visible = false;
            lblSure.AutoSize = false;
            lblSure.Location = prgSure.Location;
            lblSure.Size = prgSure.Size;
            lblSure.TextAlign = ContentAlignment.MiddleCenter;

            lblSure.Text = "Teslim Edildi";
            lblSure.ForeColor = Color.White;
            lblSure.BackColor = Color.ForestGreen;
            lblSure.Font = new Font(lblSure.Font.FontFamily, 10, FontStyle.Bold);

            YoldaModunaGec();
        }

        public void IptalEdildiYap(string neden)
        {
            if (tmrSure != null) tmrSure.Stop();

            this.BackColor = Color.FromArgb(255, 210, 210);

            prgSure.Visible = false;
            lblSure.AutoSize = false;
            lblSure.Location = prgSure.Location;
            lblSure.Size = prgSure.Size;
            lblSure.TextAlign = ContentAlignment.MiddleCenter;

            lblSure.Text = "İPTAL: " + neden;
            lblSure.ForeColor = Color.White;
            lblSure.BackColor = Color.Firebrick;
            lblSure.Font = new Font(lblSure.Font.FontFamily, 9, FontStyle.Bold);

            YoldaModunaGec();
        }

        private void tmrSure_Tick(object sender, EventArgs e)
        {
            if (kalanSaniye > 0)
            {
                kalanSaniye--;
                prgSure.Value = Math.Max(0, Math.Min(kalanSaniye, maxSaniye));

                TimeSpan zaman = TimeSpan.FromSeconds(kalanSaniye);
                lblSure.Text = zaman.ToString(@"mm\:ss");

                if (kalanSaniye <= 60)
                {
                    this.BackColor = (this.BackColor == Color.White) ? Color.LightCoral : Color.White;
                }
                else if (kalanSaniye <= 150)
                {
                    this.BackColor = Color.LemonChiffon;
                }
            }
            else
            {
                tmrSure.Stop();
                lblSure.Text = "00:00";
                this.BackColor = Color.LightGray;

                // SÜRE BİTTİĞİ AN ANA FORMA BİLDİRİM TETİKLER
                SuresiDoldu?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}