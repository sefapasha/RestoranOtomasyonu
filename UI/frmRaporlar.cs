using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace RestoranOtomasyonu.UI
{
    public partial class frmRaporlar : Form
    {
        public frmRaporlar()
        {
            InitializeComponent();
        }

        private void frmRaporlar_Load(object sender, EventArgs e)
        {
            GunlukOzetiGetir();
            UrunIstatistikleriniGetir();
            GecmisAdisyonlariGetir();
            

            // Form açıldığında ilk olarak Günlük Özet tablosu önde gelsin
            tlpGunlukOzet.BringToFront();
        }

        private void GunlukOzetiGetir()
        {
            string connectionString = SqlBaglantisi.Adres;

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();

                // 1. BUGÜNKÜ CİRO (Sadece bugünün tarihi ve Durum = 0 olan kapanmış masalar)
                // CAST(AcilisZamani AS DATE) kodu saatleri çöpe atıp sadece gün-ay-yıl kısmını bugünün tarihiyle kıyaslar
                SqlCommand cmdCiro = new SqlCommand("SELECT SUM(ToplamTutar) FROM Siparisler WHERE Durum = 0 AND CAST(AcilisZamani AS DATE) = CAST(GETDATE() AS DATE)", baglanti);
                object ciroSonuc = cmdCiro.ExecuteScalar();

                // Eğer bugün hiç satış yoksa SQL 'Null' döndürür, program çökmesin diye kontrol ediyoruz
                decimal gunlukCiro = (ciroSonuc != DBNull.Value) ? Convert.ToDecimal(ciroSonuc) : 0;
                lblGunlukCiro.Text = gunlukCiro.ToString("C2", new CultureInfo("tr-TR"));

                // 2. KAPANAN MASA SAYISI (Durum= 0 olan Bugün hesabı kesilen sipariş adedi)
                SqlCommand cmdMasaSayisi = new SqlCommand("SELECT COUNT(SiparisID) FROM Siparisler WHERE Durum = 0 AND CAST(AcilisZamani AS DATE) = CAST(GETDATE() AS DATE)", baglanti);
                object masaSonuc = cmdMasaSayisi.ExecuteScalar();
                int kapananMasa = (masaSonuc != DBNull.Value) ? Convert.ToInt32(masaSonuc) : 0;
                lblKapananMasa.Text = kapananMasa.ToString();  

                // 4. AÇIK MASA SAYISI (Durum = 1 olan aktif siparişler)
                SqlCommand cmdAcikMasaSayisi = new SqlCommand("SELECT COUNT(SiparisID) FROM Siparisler WHERE Durum = 1", baglanti);
                object acikMasaSonuc = cmdAcikMasaSayisi.ExecuteScalar();
                int acikMasaSayisi = (acikMasaSonuc != DBNull.Value) ? Convert.ToInt32(acikMasaSonuc) : 0;
                lblAcikMasa.Text = acikMasaSayisi.ToString();

                // 4. AÇIK HESAPLAR (Şu an Durum = 1 olan tüm masaların içerideki toplam sipariş tutarı)
                SqlCommand cmdAcikHesap = new SqlCommand(@"
                    SELECT SUM(SD.Adet * SD.Fiyat) 
                    FROM Siparisler S 
                    INNER JOIN SiparisDetay SD ON S.SiparisID = SD.SiparisID 
                    WHERE S.Durum = 1", baglanti);

                object acikSonuc = cmdAcikHesap.ExecuteScalar();
                decimal acikHesap = (acikSonuc != DBNull.Value) ? Convert.ToDecimal(acikSonuc) : 0;
                lblAcikHesap.Text = acikHesap.ToString("C2", new CultureInfo("tr-TR"));
            }
        }
        // === 2. KANAL: EN ÇOK SATAN ÜRÜNLERİ TABLOYA ÇEKEN METOT ===
        private void UrunIstatistikleriniGetir()
        {
            string baglantiAdresi = @"Data Source=DESKTOP-K29O5PP;Initial Catalog=RestoranDB;Integrated Security=True;Encrypt=False";

            using (SqlConnection baglanti = new SqlConnection(baglantiAdresi))
            {
                string sorgu = @"
                    SELECT 
                        U.UrunAdi AS [Ürün Adı], 
                        SUM(SD.Adet) AS [Toplam Adet], 
                        SUM(SD.Adet * SD.Fiyat) AS [Toplam Kazanç]
                    FROM Siparisler S
                    INNER JOIN SiparisDetay SD ON S.SiparisID = SD.SiparisID
                    INNER JOIN Urunler U ON SD.UrunID = U.UrunID
                    WHERE S.Durum = 0 
                    AND CAST(S.AcilisZamani AS DATE) BETWEEN @Baslangic AND @Bitis
                    GROUP BY U.UrunAdi
                    ORDER BY U.UrunAdi ASC";

                SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
                da.SelectCommand.Parameters.AddWithValue("@Baslangic", dtpBaslangic.Value.Date);
                da.SelectCommand.Parameters.AddWithValue("@Bitis", dtpBitis.Value.Date);

                DataTable dt = new DataTable();
                da.Fill(dt);

                // ALT TOPLAMI HESAPLAYIP TABLOYA EKLEME
                if (dt.Rows.Count > 0)
                {
                    object genelAdet = dt.Compute("SUM([Toplam Adet])", "");
                    object genelKazanc = dt.Compute("SUM([Toplam Kazanç])", "");

                    DataRow toplamSatiri = dt.NewRow();
                    toplamSatiri["Ürün Adı"] = ">>> GENEL TOPLAM CİRO";
                    toplamSatiri["Toplam Adet"] = genelAdet;
                    toplamSatiri["Toplam Kazanç"] = genelKazanc;

                    dt.Rows.Add(toplamSatiri);
                }

                // TEMEL TABLO AYARLARI
                dgvIstatistikler.AllowUserToAddRows = false; // En alttaki ekstra boş satırı kalıcı olarak yok et
                dgvIstatistikler.DataSource = dt; // Hazırlanan veriyi ekrana bas

                //TABLO TASARIMI
                dgvIstatistikler.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvIstatistikler.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvIstatistikler.GridColor = Color.LightGray;
                dgvIstatistikler.EnableHeadersVisualStyles = false;

                // Başlıkların Tasarımı
                dgvIstatistikler.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
                dgvIstatistikler.ColumnHeadersDefaultCellStyle.BackColor = Color.Purple;
                dgvIstatistikler.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvIstatistikler.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Purple;
                dgvIstatistikler.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dgvIstatistikler.ColumnHeadersHeight = 45;

                // İçerideki Satırların Tasarımı
                dgvIstatistikler.DefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
                dgvIstatistikler.DefaultCellStyle.SelectionBackColor = Color.DarkOrchid;
                dgvIstatistikler.DefaultCellStyle.SelectionForeColor = Color.White;

                if (dgvIstatistikler.Rows.Count > 0)
                {
                    // GENEL TOPLAM CİRO araya karışmasın diye sıralamayı iptal ediyoruz
                    foreach (DataGridViewColumn sutun in dgvIstatistikler.Columns)
                    {
                        sutun.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }

                    // En son satırı (GENEL TOPLAM CİRO) bul ve Koyu Yeşil yap
                    int sonSatir = dgvIstatistikler.Rows.Count - 1;
                    dgvIstatistikler.Rows[sonSatir].DefaultCellStyle.BackColor = Color.DarkGreen;
                    dgvIstatistikler.Rows[sonSatir].DefaultCellStyle.ForeColor = Color.White;
                    dgvIstatistikler.Rows[sonSatir].DefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                    dgvIstatistikler.Rows[sonSatir].DefaultCellStyle.SelectionBackColor = Color.DarkGreen;
                }

                // Ekranda ilk açılışta hiçbir şey seçili olmasın
                dgvIstatistikler.ClearSelection();
            }
        }

        // GEÇMİŞ ADİSYONLARI (TARİH FİLTRELİ) GETİREN METOt
        private void GecmisAdisyonlariGetir()
        {
            string connectionString = SqlBaglantisi.Adres;

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                // seçilen tarih aralığında kapanan masaları (Durum = 0) saat sırasına göre listele
                string sorgu = @"
                SELECT 
                    CAST(SiparisID AS VARCHAR) AS [Adisyon No], -- Sadece buraya CAST ekledim ki içine yazı yazabilelim
                    MasaID AS [Masa No],
                    AcilisZamani AS [Açılış Saati],
                    ToplamTutar AS [Ödenen Hesap (₺)]
                FROM Siparisler
                WHERE Durum = 0 
                AND CAST(AcilisZamani AS DATE) BETWEEN @Baslangic AND @Bitis
                ORDER BY SiparisID ASC";

                SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);

                da.SelectCommand.Parameters.AddWithValue("@Baslangic", dateTimePicker1.Value.Date);
                da.SelectCommand.Parameters.AddWithValue("@Bitis", dateTimePicker2.Value.Date);
                
                DataTable dt = new DataTable();
                da.Fill(dt);

                // ALT TOPLAMI HESAPLAYIP TABLOYA EKLEME 
                if (dt.Rows.Count > 0)
                {
                    object genelKazanc = dt.Compute("SUM([Ödenen Hesap (₺)])", "");

                    DataRow toplamSatiri = dt.NewRow();
                    toplamSatiri["Adisyon No"] = ">>> GENEL TOPLAM CİRO";
                    toplamSatiri["Ödenen Hesap (₺)"] = genelKazanc;

                    dt.Rows.Add(toplamSatiri);
                }

                dgvGecmisAdisyonlar.AllowUserToAddRows = false;
                dgvGecmisAdisyonlar.DataSource = dt;

                dgvGecmisAdisyonlar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvGecmisAdisyonlar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvGecmisAdisyonlar.EnableHeadersVisualStyles = false;
                dgvGecmisAdisyonlar.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                dgvGecmisAdisyonlar.ColumnHeadersDefaultCellStyle.BackColor = Color.Purple;
                dgvGecmisAdisyonlar.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvGecmisAdisyonlar.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Purple;
                dgvGecmisAdisyonlar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dgvGecmisAdisyonlar.ColumnHeadersHeight = 45;
                dgvGecmisAdisyonlar.DefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
                dgvGecmisAdisyonlar.DefaultCellStyle.SelectionBackColor = Color.DarkOrchid;
                dgvGecmisAdisyonlar.DefaultCellStyle.SelectionForeColor = Color.White;
                dgvGecmisAdisyonlar.GridColor = Color.LightGray;

                if (dgvGecmisAdisyonlar.Rows.Count > 0)
                {
                    foreach (DataGridViewColumn sutun in dgvGecmisAdisyonlar.Columns)
                    {
                        sutun.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }

                    int sonSatir = dgvGecmisAdisyonlar.Rows.Count - 1;
                    dgvGecmisAdisyonlar.Rows[sonSatir].DefaultCellStyle.BackColor = Color.DarkGreen;
                    dgvGecmisAdisyonlar.Rows[sonSatir].DefaultCellStyle.ForeColor = Color.White;
                    dgvGecmisAdisyonlar.Rows[sonSatir].DefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                    dgvGecmisAdisyonlar.Rows[sonSatir].DefaultCellStyle.SelectionBackColor = Color.DarkGreen;
                }

                // Form açıldığında otomatik seçili gelen ilk satırın seçimini kaldır
                dgvGecmisAdisyonlar.ClearSelection();
            }
        }
        // GEÇMİŞ ADİSYON TABLOSUNDAKİ SATIRA TIKLAYINCA FİŞ DETAYINI GÖSTERME
        private void dgvGecmisAdisyonlar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Tıklanan satırdaki Adisyon numarasını alıyoruz
            string siparisIdStr = dgvGecmisAdisyonlar.Rows[e.RowIndex].Cells["Adisyon No"].Value.ToString();

            // Eğer yanlışlıkla en alttaki yeşil "GENEL TOPLAM CİRO" satırına çift tıklarsa programın hata vermemei için
            if (siparisIdStr.Contains("TOPLAM")) return;

            int siparisId = Convert.ToInt32(siparisIdStr);
            string fisDetayi = $"--- ADİSYON NO: {siparisId} ---\n\n";
            decimal toplamTutar = 0;

            string connectionString = SqlBaglantisi.Adres;

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                // Tıklanan siparişin içindeki ürünleri ve fiyatları Sqlden çekiyoruz
                string sorgu = @"
                    SELECT U.UrunAdi, SD.Adet, SD.Fiyat
                    FROM SiparisDetay SD
                    INNER JOIN Urunler U ON SD.UrunID = U.UrunID
                    WHERE SD.SiparisID = @SiparisID";

                SqlCommand cmd = new SqlCommand(sorgu, baglanti);
                cmd.Parameters.AddWithValue("@SiparisID", siparisId);

                baglanti.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                // Fişin içindeki tüm ürünleri tek tek okuyup mesaja ekliyoruz
                while (dr.Read())
                {
                    string urunAdi = dr["UrunAdi"].ToString();
                    int adet = Convert.ToInt32(dr["Adet"]);
                    decimal fiyat = Convert.ToDecimal(dr["Fiyat"]);
                    decimal araToplam = adet * fiyat;

                    fisDetayi += $"{adet}x {urunAdi} - {araToplam} ₺\n";
                    toplamTutar += araToplam;
                }
            }

            fisDetayi += "\n---------------------------\n";
            fisDetayi += $"ÖDENEN TOPLAM: {toplamTutar} ₺";

            MessageBox.Show(fisDetayi, "Masa Sipariş Detayı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGunlukOzet_Click(object sender, EventArgs e)
        {
            tlpGunlukOzet.BringToFront();
        }

        private void btnIstatistikler_Click(object sender, EventArgs e)
        {
            pnlUrunIstatistik.BringToFront();
        }
        private void btnGecmisAdisyon_Click(object sender, EventArgs e)
        {
            pnlGecmisAdisyonlar.BringToFront();
        }
        private void btnTarihselRapor_Click(object sender, EventArgs e)
        {
            pnlDetayliAnaliz.BringToFront();
        }   
        private void btnFiltrele_Click(object sender, EventArgs e)
        {
            UrunIstatistikleriniGetir();
        }

        private void btnAdisyonFiltrele_Click(object sender, EventArgs e)
        {
            GecmisAdisyonlariGetir();
        }

        private void DetayliAnalizGetir()
        {
            // Eğer ComboBox'tan henüz bir şey seçilmediyse işlemi durdur
            if (cmbAnalizTuru.SelectedIndex == -1) return;

            // Tarihleri al (Bitiş tarihinin saatini o günün 23:59:59'u yapıyoruz ki o günkü tüm verileri kapsasın)
            DateTime baslangic = dateTimePicker3.Value.Date;
            DateTime bitis = dateTimePicker4.Value.Date.AddDays(1).AddSeconds(-1);

            string secilenAnaliz = cmbAnalizTuru.SelectedItem.ToString();

            if (secilenAnaliz == "Saatlik Yoğunluk İstatistiği")
            {
                SaatlikYogunlukGetir(baslangic, bitis);
            }
            else if (secilenAnaliz == "Masa Performans ve Ciro Raporu")
            {
                MasaPerformansGetir(baslangic, bitis);
            }
            else if (secilenAnaliz == "Birlikte Satılan Ürünler Analizi")
            {
                BirlikteSatilanUrunlerGetir(baslangic, bitis);
            }
            else if (secilenAnaliz == "Kategori Bazlı Satışlar")
            {
                KategoriBazliSatislarGetir(baslangic, bitis);
            }
        }

        private void SaatlikYogunlukGetir(DateTime baslangic, DateTime bitis)
        {
            string connectionString = SqlBaglantisi.Adres;

            string query = @"
            SELECT 
                RIGHT('0' + CAST(DATEPART(HOUR, AcilisZamani) AS VARCHAR(2)), 2) + ':00 - ' + 
                RIGHT('0' + CAST(DATEPART(HOUR, AcilisZamani) + 1 AS VARCHAR(2)), 2) + ':00' AS [Saat Aralığı],
                COUNT(SiparisID) AS [Kesilen Fiş Sayısı],
                SUM(ToplamTutar) AS [Toplam Ciro (₺)]
            FROM Siparisler
            WHERE AcilisZamani >= @Baslangic AND AcilisZamani <= @Bitis AND Durum = 0
            GROUP BY DATEPART(HOUR, AcilisZamani)
            ORDER BY DATEPART(HOUR, AcilisZamani)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Baslangic", baslangic);
                    cmd.Parameters.AddWithValue("@Bitis", bitis);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    try
                    {
                        conn.Open();
                        da.Fill(dt);

                        dgvDetayliAnaliz.DataSource = dt;

                        // dataGridView tasarımı
                        dgvDetayliAnaliz.AllowUserToAddRows = false;
                        dgvDetayliAnaliz.ReadOnly = true;
                        dgvDetayliAnaliz.RowHeadersVisible = false;
                        dgvDetayliAnaliz.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dgvDetayliAnaliz.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dgvDetayliAnaliz.EnableHeadersVisualStyles = false;
                        dgvDetayliAnaliz.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                        dgvDetayliAnaliz.ColumnHeadersDefaultCellStyle.BackColor = Color.Purple;
                        dgvDetayliAnaliz.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                        dgvDetayliAnaliz.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Purple;
                        dgvDetayliAnaliz.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                        dgvDetayliAnaliz.ColumnHeadersHeight = 45;
                        dgvDetayliAnaliz.DefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
                        dgvDetayliAnaliz.DefaultCellStyle.SelectionBackColor = Color.DarkOrchid;
                        dgvDetayliAnaliz.DefaultCellStyle.SelectionForeColor = Color.White;
                        dgvDetayliAnaliz.GridColor = Color.LightGray;

                        dgvDetayliAnaliz.ClearSelection();

                        decimal genelCiro = 0;
                        int genelFis = 0;

                        foreach (DataRow row in dt.Rows)
                        {
                            genelFis += Convert.ToInt32(row["Kesilen Fiş Sayısı"]);
                            genelCiro += Convert.ToDecimal(row["Toplam Ciro (₺)"]);
                        }

                        decimal ortalama = genelFis > 0 ? (genelCiro / genelFis) : 0;

                        lblToplamCiro.Text = $"Toplam Ciro: {genelCiro:C2}";
                        lblToplamFis.Text = $"Toplam Fiş: {genelFis}";
                        lblMasaOrtalama.Text = $"Masa Ortalaması: {ortalama:C2}";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Veri çekilirken hata oluştu: " + ex.Message);
                    }
                }
            }
        }
        private void MasaPerformansGetir(DateTime baslangic, DateTime bitis)
        {
            string connectionString = SqlBaglantisi.Adres;

            string query = @"
            SELECT 
                MasaID AS [Masa Numarası],
                COUNT(SiparisID) AS [Kesilen Fiş Sayısı],
                SUM(ToplamTutar) AS [Toplam Ciro (₺)]
            FROM Siparisler
            WHERE AcilisZamani >= @Baslangic AND AcilisZamani <= @Bitis AND Durum = 0
            GROUP BY MasaID
            ORDER BY SUM(ToplamTutar) DESC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Baslangic", baslangic);
                    cmd.Parameters.AddWithValue("@Bitis", bitis);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    try
                    {
                        conn.Open();
                        da.Fill(dt);

                        dgvDetayliAnaliz.DataSource = dt;

                        // dataGridView tasarımı
                        dgvDetayliAnaliz.AllowUserToAddRows = false;
                        dgvDetayliAnaliz.ReadOnly = true;
                        dgvDetayliAnaliz.RowHeadersVisible = false;
                        dgvDetayliAnaliz.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dgvDetayliAnaliz.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dgvDetayliAnaliz.EnableHeadersVisualStyles = false;
                        dgvDetayliAnaliz.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                        dgvDetayliAnaliz.ColumnHeadersDefaultCellStyle.BackColor = Color.Purple;
                        dgvDetayliAnaliz.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                        dgvDetayliAnaliz.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Purple;
                        dgvDetayliAnaliz.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                        dgvDetayliAnaliz.ColumnHeadersHeight = 45;
                        dgvDetayliAnaliz.DefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
                        dgvDetayliAnaliz.DefaultCellStyle.SelectionBackColor = Color.DarkOrchid;
                        dgvDetayliAnaliz.DefaultCellStyle.SelectionForeColor = Color.White;
                        dgvDetayliAnaliz.GridColor = Color.LightGray;

                        dgvDetayliAnaliz.ClearSelection();

                        // Özet Hesaplamaları
                        decimal genelCiro = 0;
                        int genelFis = 0;

                        foreach (DataRow row in dt.Rows)
                        {
                            genelFis += Convert.ToInt32(row["Kesilen Fiş Sayısı"]);
                            genelCiro += Convert.ToDecimal(row["Toplam Ciro (₺)"]);
                        }

                        decimal ortalama = genelFis > 0 ? (genelCiro / genelFis) : 0;

                        lblToplamCiro.Text = $"Toplam Ciro: {genelCiro:C2}";
                        lblToplamFis.Text = $"Toplam Fiş: {genelFis}";
                        lblMasaOrtalama.Text = $"Ortalama Adisyon: {ortalama:C2}";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Masa performansı çekilirken hata: " + ex.Message);
                    }
                }
            }
        }
        private void BirlikteSatilanUrunlerGetir(DateTime baslangic, DateTime bitis)
        {
            string connectionString = SqlBaglantisi.Adres;

            // SiparisDetay ve Urunler tablolarını UrunID üzerinden bağlıyoruz
            string query = @"
            SELECT TOP 20
                U1.UrunAdi AS [1. Ürün],
                U2.UrunAdi AS [2. Ürün],
                COUNT(*) AS [Beraber Sipariş Edilme Sayısı]
            FROM SiparisDetay SD1
            JOIN SiparisDetay SD2 ON SD1.SiparisID = SD2.SiparisID AND SD1.UrunID < SD2.UrunID
            JOIN Siparisler S ON SD1.SiparisID = S.SiparisID
            JOIN Urunler U1 ON SD1.UrunID = U1.UrunID
            JOIN Urunler U2 ON SD2.UrunID = U2.UrunID
            WHERE S.AcilisZamani >= @Baslangic AND S.AcilisZamani <= @Bitis AND S.Durum = 0
            GROUP BY U1.UrunAdi, U2.UrunAdi
            ORDER BY COUNT(*) DESC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Baslangic", baslangic);
                    cmd.Parameters.AddWithValue("@Bitis", bitis);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    try
                    {
                        conn.Open();
                        da.Fill(dt);
                        dgvDetayliAnaliz.DataSource = dt;

                        // datagrivview Tablo Tasarımı
                        dgvDetayliAnaliz.AllowUserToAddRows = false;
                        dgvDetayliAnaliz.ReadOnly = true;
                        dgvDetayliAnaliz.RowHeadersVisible = false;
                        dgvDetayliAnaliz.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dgvDetayliAnaliz.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dgvDetayliAnaliz.EnableHeadersVisualStyles = false;
                        dgvDetayliAnaliz.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                        dgvDetayliAnaliz.ColumnHeadersDefaultCellStyle.BackColor = Color.Purple;
                        dgvDetayliAnaliz.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                        dgvDetayliAnaliz.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Purple;
                        dgvDetayliAnaliz.ColumnHeadersHeight = 45;
                        dgvDetayliAnaliz.DefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
                        dgvDetayliAnaliz.DefaultCellStyle.SelectionBackColor = Color.DarkOrchid;
                        dgvDetayliAnaliz.DefaultCellStyle.SelectionForeColor = Color.White;
                        dgvDetayliAnaliz.GridColor = Color.LightGray;

                        if (dgvDetayliAnaliz.Rows.Count > 0)
                        {
                            foreach (DataGridViewColumn sutun in dgvDetayliAnaliz.Columns)
                            {
                                sutun.SortMode = DataGridViewColumnSortMode.NotSortable;
                            }
                        }
                        dgvDetayliAnaliz.ClearSelection();

                        lblToplamCiro.Text = "Toplam Ciro: -";
                        lblToplamFis.Text = "Toplam Fiş: -";
                        lblMasaOrtalama.Text = "Ortalama Adisyon: -";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Birlikte satılan ürünler çekilirken hata oluştu: " + ex.Message);
                    }
                }
            }
        }
        private void KategoriBazliSatislarGetir(DateTime baslangic, DateTime bitis)
        {
            string connectionString = SqlBaglantisi.Adres;

            string query = @"
            SELECT 
                CASE U.KategoriID
                    WHEN 1 THEN 'Çorbalar'
                    WHEN 2 THEN 'Kebaplar ve Izgaralar'
                    WHEN 3 THEN 'İçecekler'
                    WHEN 4 THEN 'Tatlılar'
                    ELSE 'Diğer Kategoriler'
                END AS [Kategori Adı],
                SUM(SD.Adet) AS [Satılan Toplam Adet], 
                SUM(SD.Adet * U.Fiyat) AS [Kategori Cirosu (₺)] 
            FROM SiparisDetay SD
            JOIN Urunler U ON SD.UrunID = U.UrunID
            JOIN Siparisler S ON SD.SiparisID = S.SiparisID
            WHERE S.AcilisZamani >= @Baslangic AND S.AcilisZamani <= @Bitis AND S.Durum = 0
            GROUP BY U.KategoriID
            ORDER BY [Kategori Cirosu (₺)] DESC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Baslangic", baslangic);
                    cmd.Parameters.AddWithValue("@Bitis", bitis);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    try
                    {
                        conn.Open();
                        da.Fill(dt);
                        dgvDetayliAnaliz.DataSource = dt;

                        dgvDetayliAnaliz.AllowUserToAddRows = false;
                        dgvDetayliAnaliz.ReadOnly = true;
                        dgvDetayliAnaliz.RowHeadersVisible = false;
                        dgvDetayliAnaliz.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dgvDetayliAnaliz.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dgvDetayliAnaliz.EnableHeadersVisualStyles = false;
                        dgvDetayliAnaliz.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                        dgvDetayliAnaliz.ColumnHeadersDefaultCellStyle.BackColor = Color.Purple;
                        dgvDetayliAnaliz.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                        dgvDetayliAnaliz.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Purple;
                        dgvDetayliAnaliz.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                        dgvDetayliAnaliz.ColumnHeadersHeight = 45;
                        dgvDetayliAnaliz.DefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
                        dgvDetayliAnaliz.DefaultCellStyle.SelectionBackColor = Color.DarkOrchid;
                        dgvDetayliAnaliz.DefaultCellStyle.SelectionForeColor = Color.White;
                        dgvDetayliAnaliz.GridColor = Color.LightGray;

                        if (dgvDetayliAnaliz.Rows.Count > 0)
                        {
                            foreach (DataGridViewColumn sutun in dgvDetayliAnaliz.Columns)
                            {
                                sutun.SortMode = DataGridViewColumnSortMode.NotSortable;
                            }
                        }
                        dgvDetayliAnaliz.ClearSelection();

                        decimal genelCiro = 0;
                        int genelAdet = 0;

                        foreach (DataRow row in dt.Rows)
                        {
                            if (row["Satılan Toplam Adet"] != DBNull.Value)
                                genelAdet += Convert.ToInt32(row["Satılan Toplam Adet"]);

                            if (row["Kategori Cirosu (₺)"] != DBNull.Value)
                                genelCiro += Convert.ToDecimal(row["Kategori Cirosu (₺)"]);
                        }

                        lblToplamCiro.Text = $"Toplam Ciro: {genelCiro:C2}";
                        lblToplamFis.Text = $"Satılan Ürün: {genelAdet} Adet";
                        lblMasaOrtalama.Text = "Ortalama Adisyon: -";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Kategori satışları çekilirken hata oluştu: " + ex.Message);
                    }
                }
            }
        }
        private void cmbAnalizTuru_SelectedIndexChanged(object sender, EventArgs e)
        {
            DetayliAnalizGetir();
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            DetayliAnalizGetir();
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            DetayliAnalizGetir();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1) return;

            DateTime baslangic = dateTimePicker5.Value.Date;
            DateTime bitis = dateTimePicker6.Value.Date.AddDays(1).AddSeconds(-1);

            string secim = comboBox1.SelectedItem.ToString();

            if (secim == "Günlük Ciro Grafiği")
            {
                GunlukCiroGrafigiCiz(baslangic, bitis);
            }
            else if (secim == "Kategori Satış Dağılımı")
            {
                KategoriSatisGrafigiCiz(baslangic, bitis);
            }
        }

        private void GunlukCiroGrafigiCiz(DateTime baslangic, DateTime bitis)
        {
            string connectionString = SqlBaglantisi.Adres;

            string query = @"
            SELECT 
                CAST(AcilisZamani AS DATE) AS [Tarih],
                SUM(ToplamTutar) AS [GunlukCiro]
            FROM Siparisler
            WHERE AcilisZamani >= @Baslangic AND AcilisZamani <= @Bitis AND Durum = 0
            GROUP BY CAST(AcilisZamani AS DATE)
            ORDER BY CAST(AcilisZamani AS DATE) ASC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Baslangic", baslangic);
                    cmd.Parameters.AddWithValue("@Bitis", bitis);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    conn.Open();
                    da.Fill(dt);

                    chart1.Series.Clear();
                    chart1.Titles.Clear();
                    chart1.Legends.Clear();

                    Title baslik = chart1.Titles.Add("Günlük Ciro Analizi (₺)");
                    baslik.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
                    baslik.ForeColor = Color.Indigo;

                    Series seri = chart1.Series.Add("Ciro");
                    seri.ChartType = SeriesChartType.Column;
                    seri.IsValueShownAsLabel = true;
                    seri.LabelFormat = "C0";
                    seri.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
                    seri.LabelForeColor = Color.Black;

                    seri.Color = Color.MediumPurple; // Üst renk
                    seri.BackGradientStyle = GradientStyle.TopBottom; // gradyant yukarıdan aşağıya geçiş
                    seri.BackSecondaryColor = Color.Indigo; // Alt renk
                    seri.BorderWidth = 0;

                    seri["PixelPointWidth"] = "80";

                    foreach (DataRow row in dt.Rows)
                    {
                        string tarih = Convert.ToDateTime(row["Tarih"]).ToString("dd.MM");
                        decimal ciro = Convert.ToDecimal(row["GunlukCiro"]);
                        seri.Points.AddXY(tarih, ciro);
                    }

                    ChartArea area = chart1.ChartAreas[0];
                    area.BackColor = Color.White;
                    area.AxisX.MajorGrid.LineColor = Color.FromArgb(240, 240, 240);
                    area.AxisY.MajorGrid.LineColor = Color.FromArgb(240, 240, 240);
                    area.AxisX.LabelStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                    area.AxisY.LabelStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                    area.AxisX.LineColor = Color.Indigo;
                    area.AxisY.LineColor = Color.Indigo;
                }
            }
        }

        private void KategoriSatisGrafigiCiz(DateTime baslangic, DateTime bitis)
        {
            string connectionString = SqlBaglantisi.Adres;

            string query = @"
            SELECT 
                CASE U.KategoriID
                    WHEN 1 THEN 'Çorbalar'
                    WHEN 2 THEN 'Ana Yemekler'
                    WHEN 3 THEN 'İçecekler'
                    WHEN 4 THEN 'Tatlılar'
                    WHEN 5 THEN 'Salatalar'
                    ELSE 'Diğer Kategoriler'
                END AS [Kategori Adı],
                SUM(SD.Adet * U.Fiyat) AS [Ciro]
            FROM SiparisDetay SD
            JOIN Urunler U ON SD.UrunID = U.UrunID
            JOIN Siparisler S ON SD.SiparisID = S.SiparisID
            WHERE S.AcilisZamani >= @Baslangic AND S.AcilisZamani <= @Bitis AND S.Durum = 0
            GROUP BY U.KategoriID
            ORDER BY [Ciro] DESC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Baslangic", baslangic);
                    cmd.Parameters.AddWithValue("@Bitis", bitis);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    conn.Open();
                    da.Fill(dt);

                    chart1.Series.Clear();
                    chart1.Titles.Clear();
                    chart1.Legends.Clear();

                    Title baslik = chart1.Titles.Add("Kategori Ciro Dağılımı");
                    baslik.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
                    baslik.ForeColor = Color.Indigo;

                    Legend lejant = chart1.Legends.Add("Kategoriler");
                    lejant.Docking = Docking.Bottom;
                    lejant.Alignment = StringAlignment.Center;
                    lejant.Font = new Font("Segoe UI", 10F);

                    Series seri = chart1.Series.Add("Kategoriler");
                    seri.ChartType = SeriesChartType.Doughnut;
                    seri["DoughnutRadius"] = "55";

                    seri.IsValueShownAsLabel = true;

                    seri.Label = "#PERCENT{P0}";
                    seri["PieLabelStyle"] = "Inside";
                    seri.LabelForeColor = Color.White;
                    seri.Font = new Font("Segoe UI", 11F, FontStyle.Bold);

                    seri.LegendText = "#VALX\n#VAL{C0}\n(#PERCENT{P0})";

                    chart1.Palette = ChartColorPalette.None;
                    chart1.PaletteCustomColors = new Color[] {
                Color.Indigo, Color.MediumSlateBlue, Color.DarkOrchid, Color.Plum, Color.DarkMagenta
            };

                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["Kategori Adı"] != DBNull.Value && row["Ciro"] != DBNull.Value)
                        {
                            string kategori = row["Kategori Adı"].ToString();
                            decimal ciro = Convert.ToDecimal(row["Ciro"]);
                            seri.Points.AddXY(kategori, ciro);
                        }
                    }
                }
            }
        }
        private void btnGrafikler_Click(object sender, EventArgs e)
        {
            pnlGrafikler.BringToFront();
        }

        // "Bugün" Butonu
        private void btnBugun_Click(object sender, EventArgs e)
        {
            dateTimePicker5.Value = DateTime.Today;
            dateTimePicker6.Value = DateTime.Today;

            // Grafiği anında yenilemek için combobox eventini tetikliyoruz
            comboBox1_SelectedIndexChanged(null, null);
        }

        // "Dün" Butonu
        private void btnDun_Click(object sender, EventArgs e)
        {
            dateTimePicker5.Value = DateTime.Today.AddDays(-1);
            dateTimePicker6.Value = DateTime.Today.AddDays(-1);

            comboBox1_SelectedIndexChanged(null, null);
        }

        // "Bu Hafta" Butonu (Pazartesi'den bugüne)
        private void btnBuHafta_Click(object sender, EventArgs e)
        {
            // Pazartesiyi haftanın başı kabul ederek hesaplar
            int fark = (7 + (DateTime.Today.DayOfWeek - DayOfWeek.Monday)) % 7;
            dateTimePicker5.Value = DateTime.Today.AddDays(-1 * fark);
            dateTimePicker6.Value = DateTime.Today;

            comboBox1_SelectedIndexChanged(null, null);
        }

        // "Bu Ay" Butonu (Ayın 1'inden bugüne)
        private void btnBuAy_Click(object sender, EventArgs e)
        {
            dateTimePicker5.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dateTimePicker6.Value = DateTime.Today;

            comboBox1_SelectedIndexChanged(null, null);
        }
    }
}
