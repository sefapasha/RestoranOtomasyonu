using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace RestoranOtomasyonu.UI
{
    public partial class frmMusteriYorumlari : Form
    {
        string connectionString = SqlBaglantisi.Adres;
        private Timer timerCanliYenile = new Timer();

        public frmMusteriYorumlari()
        {
            InitializeComponent();

            dgvYorumlar.CellFormatting += dgvYorumlar_CellFormatting;
            this.FormClosing += frmMusteriYorumlari_FormClosing;
        }

        private void frmMusteriYorumlari_Load(object sender, EventArgs e)
        {
            
            VerileriYukle();
            DataGridTasarimiDuzenle();

            // 10 Saniyede Bir tabloyu yeniler
            timerCanliYenile.Interval = 10000;
            timerCanliYenile.Tick += (s, ev) => VerileriYukle();
            timerCanliYenile.Start();
        }

        private void frmMusteriYorumlari_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (timerCanliYenile != null)
            {
                timerCanliYenile.Stop();
                timerCanliYenile.Dispose();
            }
        }

        private void VerileriYukle()
        {
            EksikYorumlariTamamla(); // Teslim edilip yorumu olmayan siparişlere otomatik yorum ekler (telim edilen tüm iparişlerde yorum olur)
            OzetIstatistikleriGetir();
            YorumlariGetir();
        }

        // =========================================================================
        // 1. TESLİM EDİLMİŞ (DURUM = 3) eksik yorumlu TÜM SİPARİŞLERE OTOMATİK YORUM ÜRETME
        // =========================================================================
        private void EksikYorumlariTamamla()
        {
            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();

                string bulSorgu = @"SELECT P.PaketID, P.MusteriAdi 
                            FROM PaketSiparisler P
                            LEFT JOIN MusteriYorumlari Y ON P.PaketID = Y.PaketID
                            WHERE P.Durum = 3 AND Y.YorumID IS NULL";

                SqlDataAdapter da = new SqlDataAdapter(bulSorgu, baglanti);
                DataTable dtEksikler = new DataTable();
                da.Fill(dtEksikler);

                if (dtEksikler.Rows.Count == 0) return;

                Random rnd = new Random();

                foreach (DataRow row in dtEksikler.Rows)
                {
                    int pId = Convert.ToInt32(row["PaketID"]);
                    string mAdi = row["MusteriAdi"].ToString();

                    int zar = rnd.Next(1, 101); // 1-100 arası yüzdelik zar
                    int puan;

                    if (zar <= 60)
                        puan = 5;              // %60 ihtimalle 5 Yıldız
                    else if (zar <= 88)
                        puan = 4;              // %28 ihtimalle 4 Yıldız
                    else if (zar <= 96)
                        puan = 3;              // %8 ihtimalle 3 Yıldız
                    else
                        puan = rnd.Next(1, 3); // %4 ihtimalle 1 veya 2 Yıldız

                    string ekleSorgu = @"INSERT INTO MusteriYorumlari (PaketID, MusteriAdi, Puan, YorumMetni, Tarih)
                                 VALUES (
                                     @pId, 
                                     @mAdi, 
                                     @puan, 
                                     ISNULL(
                                         (SELECT TOP 1 YorumMetni FROM HazirYorumlar WHERE @puan BETWEEN MinPuan AND MaxPuan ORDER BY NEWID()), 
                                         N'Teşekkürler, elinize sağlık.'
                                     ), 
                                     GETDATE()
                                 )";

                    SqlCommand cmd = new SqlCommand(ekleSorgu, baglanti);
                    cmd.Parameters.AddWithValue("@pId", pId);
                    cmd.Parameters.AddWithValue("@mAdi", mAdi);
                    cmd.Parameters.AddWithValue("@puan", puan);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        

        // =========================================================================
        // 3. ÜST PANEL KPI HESAPLAMALARI
        // =========================================================================
        private void OzetIstatistikleriGetir()
        {
            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();

                SqlCommand cmdOrt = new SqlCommand("SELECT ISNULL(AVG(CAST(Puan AS DECIMAL(3,1))), 0) FROM MusteriYorumlari", baglanti);
                decimal ortalama = Convert.ToDecimal(cmdOrt.ExecuteScalar());
                lblOrtalamaPuan.Text = $"★ {ortalama:0.0} / 5.0";

                SqlCommand cmdSayi = new SqlCommand("SELECT COUNT(*) FROM MusteriYorumlari", baglanti);
                int toplam = Convert.ToInt32(cmdSayi.ExecuteScalar());
                lblToplamYorum.Text = $"{toplam} Değerlendirme";

                // Ortalama Teslimat Süresi 
                string hizSorgu = @"SELECT ISNULL(AVG(DATEDIFF(SECOND, SiparisZamani, TeslimZamani)), 22) 
                            FROM PaketSiparisler 
                            WHERE Durum = 3 AND TeslimZamani IS NOT NULL";
                SqlCommand cmdHiz = new SqlCommand(hizSorgu, baglanti);
                int hamSaniye = Convert.ToInt32(cmdHiz.ExecuteScalar());

                int ortDakika = hamSaniye;
                if (ortDakika < 19) ortDakika = 19 + (hamSaniye % 5);
                else if (ortDakika > 28) ortDakika = 21 + (hamSaniye % 6);

                lblOrtalamaSure.Text = $"{ortDakika} Dk";
            }
        }

        // =========================================================================
        // 4. YORUMLARI ÇEKME VE YILDIZLARI OLUŞTURMA
        // =========================================================================
        private void YorumlariGetir()
        {
            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();

                string sorgu = @"SELECT 
                                    Y.YorumID AS [ID],
                                    Y.PaketID AS [Sipariş No],
                                    Y.MusteriAdi AS [Müşteri Adı],
                                    ISNULL(P.SiparisIcerigi, '-') AS [Sipariş İçeriği],
                                    Y.Puan AS [Puan],
                                    '' AS [Değerlendirme], 
                                    Y.YorumMetni AS [Müşteri Yorumu],
                                    Y.Tarih AS [Tarih]
                                 FROM MusteriYorumlari Y
                                 LEFT JOIN PaketSiparisler P ON Y.PaketID = P.PaketID
                                 ORDER BY Y.YorumID DESC";

                SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    int p = Convert.ToInt32(row["Puan"]);
                    string yildizStr = "";
                    for (int i = 0; i < p; i++) yildizStr += "★ ";
                    row["Değerlendirme"] = yildizStr.Trim();
                }

                int ilkGorunenSatir = dgvYorumlar.FirstDisplayedScrollingRowIndex;
                dgvYorumlar.DataSource = dt;
                if (ilkGorunenSatir >= 0 && ilkGorunenSatir < dgvYorumlar.Rows.Count)
                {
                    dgvYorumlar.FirstDisplayedScrollingRowIndex = ilkGorunenSatir;
                }
            }
        }

        // =========================================================================
        // 5. DATAGRIDVIEW TASARIMI VE SÜTUN ORANLARI
        // =========================================================================
        private void DataGridTasarimiDuzenle()
        {
            dgvYorumlar.ReadOnly = true;
            dgvYorumlar.AllowUserToAddRows = false;
            dgvYorumlar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvYorumlar.RowHeadersVisible = false;
            dgvYorumlar.BorderStyle = BorderStyle.None;
            dgvYorumlar.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvYorumlar.GridColor = Color.FromArgb(230, 230, 230);
            dgvYorumlar.RowTemplate.Height = 38;
            dgvYorumlar.ShowCellToolTips = true;

            dgvYorumlar.EnableHeadersVisualStyles = false;
            dgvYorumlar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvYorumlar.ColumnHeadersHeight = 45;
            dgvYorumlar.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 0, 110);
            dgvYorumlar.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvYorumlar.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dgvYorumlar.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvYorumlar.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dgvYorumlar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvYorumlar.Columns["ID"] != null) dgvYorumlar.Columns["ID"].Visible = false;
            if (dgvYorumlar.Columns["Puan"] != null) dgvYorumlar.Columns["Puan"].Visible = false;

            if (dgvYorumlar.Columns["Sipariş No"] != null)
            {
                dgvYorumlar.Columns["Sipariş No"].FillWeight = 11;
                dgvYorumlar.Columns["Sipariş No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvYorumlar.Columns["Müşteri Adı"] != null)
                dgvYorumlar.Columns["Müşteri Adı"].FillWeight = 16;

            if (dgvYorumlar.Columns["Sipariş İçeriği"] != null)
                dgvYorumlar.Columns["Sipariş İçeriği"].FillWeight = 28;

            if (dgvYorumlar.Columns["Değerlendirme"] != null)
            {
                dgvYorumlar.Columns["Değerlendirme"].FillWeight = 16;
                dgvYorumlar.Columns["Değerlendirme"].DefaultCellStyle.ForeColor = Color.DarkOrange;
                dgvYorumlar.Columns["Değerlendirme"].DefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
                dgvYorumlar.Columns["Değerlendirme"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvYorumlar.Columns["Müşteri Yorumu"] != null)
            {
                dgvYorumlar.Columns["Müşteri Yorumu"].FillWeight = 50;
                dgvYorumlar.Columns["Müşteri Yorumu"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Italic);
            }

            if (dgvYorumlar.Columns["Tarih"] != null)
            {
                dgvYorumlar.Columns["Tarih"].FillWeight = 19;
                dgvYorumlar.Columns["Tarih"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
                dgvYorumlar.Columns["Tarih"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        // =========================================================================
        // 6. PUANA GÖRE SATIR RENKLENDİRME
        // =========================================================================
        private void dgvYorumlar_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvYorumlar.Columns[e.ColumnIndex].Name == "Değerlendirme" && e.Value != null)
            {
                DataGridViewRow row = dgvYorumlar.Rows[e.RowIndex];
                if (row.Cells["Puan"].Value != null && row.Cells["Puan"].Value != DBNull.Value)
                {
                    int puan = Convert.ToInt32(row.Cells["Puan"].Value);
                    if (puan >= 4)
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(228, 250, 228);
                        row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(180, 230, 180);
                        row.DefaultCellStyle.SelectionForeColor = Color.Black;
                    }
                    else if (puan == 3)
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 252, 225);
                        row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 235, 175);
                        row.DefaultCellStyle.SelectionForeColor = Color.Black;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 225, 225);
                        row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 185, 185);
                        row.DefaultCellStyle.SelectionForeColor = Color.Black;
                    }
                }
            }
        }
    }
}