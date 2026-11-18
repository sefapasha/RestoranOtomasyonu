using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace RestoranOtomasyonu.UI
{
    public partial class frmPaketRapor : Form
    {
        string connectionString = SqlBaglantisi.Adres;

        private Timer timerCanliYenile = new Timer();

        public frmPaketRapor()
        {
            InitializeComponent();
        }

        private void frmPaketRapor_Load(object sender, EventArgs e)
        {
            VerileriVeTabloyuYukle();
            DataGridTasarimiDuzenle();

            // 10 Saniyede Bir Canlı Yenileme
            timerCanliYenile.Interval = 10000; // 10 Saniye
            timerCanliYenile.Tick += TimerCanliYenile_Tick;
            timerCanliYenile.Start();
        }

        private void frmPaketRapor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (timerCanliYenile != null)
            {
                timerCanliYenile.Stop();
                timerCanliYenile.Dispose();
            }
        }

        private void TimerCanliYenile_Tick(object sender, EventArgs e)
        {
            VerileriVeTabloyuYukle();
        }

        private void VerileriVeTabloyuYukle()
        {
            ZamanAsiminiVeKuryeyiGuncelle();
            OzetVerileriYukle();
            SiparisTablosunuYukle();
        }

        private void ZamanAsiminiVeKuryeyiGuncelle()
        {
            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();

                // a) Bekleyen yeni siparişlerden 5 dk (300 sn) geçenleri Süresi Doldu olarak İptal Et
                string queryOtoIptal = @"UPDATE PaketSiparisler 
                                         SET Durum = 4, IptalNedeni = 'Süresi doldu' 
                                         WHERE Durum = 1 
                                           AND DATEDIFF(second, SiparisZamani, GETDATE()) >= 300";
                SqlCommand cmdOtoIptal = new SqlCommand(queryOtoIptal, baglanti);
                cmdOtoIptal.ExecuteNonQuery();

                // b) Kuryede kalan (Durum = 2) ve 1 dakikadan (60 sn) fazla yolda olan siparişleri Otomatik Teslim Edildi yap
                string queryOtoTeslim = @"UPDATE PaketSiparisler 
                                          SET Durum = 3, TeslimZamani = GETDATE() 
                                          WHERE Durum = 2 
                                            AND DATEDIFF(second, SiparisZamani, GETDATE()) >= 60";
                SqlCommand cmdOtoTeslim = new SqlCommand(queryOtoTeslim, baglanti);
                cmdOtoTeslim.ExecuteNonQuery();
            }
        }

        // ÜST PANEL CİRO VE TOPLAM TESLİM/İPTAL SAYILARI
        private void OzetVerileriYukle()
        {
            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();

                SqlCommand cmdCiro = new SqlCommand("SELECT ISNULL(SUM(Tutar), 0) FROM PaketSiparisler WHERE Durum = 3", baglanti);
                decimal toplamCiro = Convert.ToDecimal(cmdCiro.ExecuteScalar());
                lblToplamCiro.Text = toplamCiro.ToString("C2");

                SqlCommand cmdTeslim = new SqlCommand("SELECT COUNT(*) FROM PaketSiparisler WHERE Durum = 3", baglanti);
                int teslimAdet = Convert.ToInt32(cmdTeslim.ExecuteScalar());
                lblTeslimSayisi.Text = teslimAdet.ToString();

                SqlCommand cmdIptal = new SqlCommand("SELECT COUNT(*) FROM PaketSiparisler WHERE Durum = 4", baglanti);
                int iptalAdet = Convert.ToInt32(cmdIptal.ExecuteScalar());
                lblIptalSayisi.Text = iptalAdet.ToString();
            }
        }

        // VERİTABANINDAN SİPARİŞ GEÇMİŞİNİ TABLOYA ÇEKME
        private void SiparisTablosunuYukle()
        {
            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();

                string sorgu = @"SELECT 
                                    PaketID AS [Sipariş No],
                                    MusteriAdi AS [Müşteri Adı],
                                    SiparisIcerigi AS [Sipariş İçeriği],
                                    Tutar AS [Tutar],
                                    CASE 
                                        WHEN Durum = 1 THEN 'Yeni Sipariş'
                                        WHEN Durum = 2 THEN 'Kuryede'
                                        WHEN Durum = 3 THEN 'Teslim Edildi'
                                        WHEN Durum = 4 THEN 'İptal Edildi'
                                        ELSE 'Bilinmiyor'
                                    END AS [Durum],
                                    SiparisZamani AS [Tarih],
                                    ISNULL(IptalNedeni, '-') AS [İptal Gerekçesi]
                                 FROM PaketSiparisler 
                                 ORDER BY PaketID DESC";

                SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);

                int oncekiSecim = dgvPaketGecmisi.FirstDisplayedScrollingRowIndex;
                dgvPaketGecmisi.DataSource = dt;
                if (oncekiSecim >= 0 && oncekiSecim < dgvPaketGecmisi.Rows.Count)
                {
                    dgvPaketGecmisi.FirstDisplayedScrollingRowIndex = oncekiSecim;
                }
            }
        }

        private void DataGridTasarimiDuzenle()
        {
            dgvPaketGecmisi.ReadOnly = true;
            dgvPaketGecmisi.AllowUserToAddRows = false;
            dgvPaketGecmisi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPaketGecmisi.RowHeadersVisible = false;
            dgvPaketGecmisi.BorderStyle = BorderStyle.None;
            dgvPaketGecmisi.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvPaketGecmisi.GridColor = Color.FromArgb(230, 230, 230);
            dgvPaketGecmisi.RowTemplate.Height = 36;

            dgvPaketGecmisi.EnableHeadersVisualStyles = false;
            dgvPaketGecmisi.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvPaketGecmisi.ColumnHeadersHeight = 45;
            dgvPaketGecmisi.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 0, 110);
            dgvPaketGecmisi.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPaketGecmisi.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dgvPaketGecmisi.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvPaketGecmisi.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);

            // Sütun Oranları ve Genişlikleri
            dgvPaketGecmisi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvPaketGecmisi.Columns["Sipariş No"] != null)
                dgvPaketGecmisi.Columns["Sipariş No"].FillWeight = 12;

            if (dgvPaketGecmisi.Columns["Müşteri Adı"] != null)
                dgvPaketGecmisi.Columns["Müşteri Adı"].FillWeight = 18;

            if (dgvPaketGecmisi.Columns["Sipariş İçeriği"] != null)
            {
                dgvPaketGecmisi.Columns["Sipariş İçeriği"].FillWeight = 48;
                dgvPaketGecmisi.Columns["Sipariş İçeriği"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            }

            if (dgvPaketGecmisi.Columns["Tutar"] != null)
            {
                dgvPaketGecmisi.Columns["Tutar"].FillWeight = 15;
                dgvPaketGecmisi.Columns["Tutar"].DefaultCellStyle.Format = "C2";
                dgvPaketGecmisi.Columns["Tutar"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPaketGecmisi.Columns["Tutar"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            }

            if (dgvPaketGecmisi.Columns["Durum"] != null)
            {
                dgvPaketGecmisi.Columns["Durum"].FillWeight = 16;
                dgvPaketGecmisi.Columns["Durum"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvPaketGecmisi.Columns["Tarih"] != null)
            {
                dgvPaketGecmisi.Columns["Tarih"].FillWeight = 22;
                dgvPaketGecmisi.Columns["Tarih"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
                dgvPaketGecmisi.Columns["Tarih"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvPaketGecmisi.Columns["İptal Gerekçesi"] != null)
                dgvPaketGecmisi.Columns["İptal Gerekçesi"].FillWeight = 25;
        }


        private void dgvPaketGecmisi_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvPaketGecmisi.Columns[e.ColumnIndex].Name == "Durum" && e.Value != null)
            {
                string durum = e.Value.ToString();
                DataGridViewRow row = dgvPaketGecmisi.Rows[e.RowIndex];

                if (durum == "Teslim Edildi")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(220, 248, 220);
                    row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(170, 225, 170);
                    row.DefaultCellStyle.SelectionForeColor = Color.Black;
                }
                else if (durum == "İptal Edildi")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 222, 222);
                    row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 180, 180);
                    row.DefaultCellStyle.SelectionForeColor = Color.Black;
                }
                else if (durum == "Kuryede")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 250, 205);
                    row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 230, 160);
                    row.DefaultCellStyle.SelectionForeColor = Color.Black;
                }
                else if (durum == "Yeni Sipariş")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(220, 235, 252);
                    row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(185, 215, 245);
                    row.DefaultCellStyle.SelectionForeColor = Color.Black;
                }
            }
        }
    }
}