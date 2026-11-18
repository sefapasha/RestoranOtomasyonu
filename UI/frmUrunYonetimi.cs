using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace RestoranOtomasyonu.UI
{
    public partial class frmUrunYonetimi : Form
    {
        string connectionString = SqlBaglantisi.Adres;

        public string AktifKullanici { get; set; }
        public string AktifRol { get; set; }

        public frmUrunYonetimi()
        {
            InitializeComponent();
        }

        private void frmUrunYonetimi_Load(object sender, EventArgs e)
        {
            // Sol üstteki Label'lara değerleri atıyoruz 
            lblKullaniciAdi.Text = AktifKullanici;
            lblKullaniciRol.Text = AktifRol;

            KategorileriDoldur();
            UrunleriGetir();
        }

        // KATEGORİLERİ COMBOBOX'A ÇEK
        private void KategorileriDoldur()
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(connectionString))
                {
                    // Kategoriler tablosundan KategoriID ve KategoriAdi'ni çekiyoruz
                    string sorgu = "SELECT KategoriID, KategoriAdi FROM Kategoriler";
                    SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Arama/Filtreleme ComboBox'ı için "Tümü" seçeneği ekliyoruz
                    DataRow row = dt.NewRow();
                    row["KategoriID"] = 0;
                    row["KategoriAdi"] = "Tümü";
                    dt.Rows.InsertAt(row, 0);

                    // ComboBox'a verileri bağlıyoruz
                    cmbFiltreKategori.DataSource = dt;
                    cmbFiltreKategori.DisplayMember = "KategoriAdi"; // Ekranda görünecek olan
                    cmbFiltreKategori.ValueMember = "KategoriID";    // Arka planda tutulacak ID değeri
                    cmbFiltreKategori.SelectedIndex = 0; // Varsayılan olarak "Tümü" seçili gelsin
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kategoriler yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ÜRÜNLERİ TABLOYA ÇEKME
        private void UrunleriGetir()
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(connectionString))
                {
                    // Urunler ve Kategoriler tablolarını JOIN ile birleştirip getiriyoruz ki KategoriID yerine KategoriAdi yazsın
                    string sorgu = @"SELECT 
                                        u.UrunID, 
                                        u.UrunAdi AS [Ürün Adı], 
                                        k.KategoriAdi AS [Kategori], 
                                        u.Fiyat 
                                     FROM Urunler u
                                     INNER JOIN Kategoriler k ON u.KategoriID = k.KategoriID";

                    SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvUrunler.DataSource = dt;

                    dgvUrunler.AllowUserToAddRows = false;
                    dgvUrunler.ReadOnly = true;
                    dgvUrunler.RowHeadersVisible = false;
                    dgvUrunler.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvUrunler.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                    dgvUrunler.EnableHeadersVisualStyles = false;
                    dgvUrunler.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                    dgvUrunler.ColumnHeadersDefaultCellStyle.BackColor = Color.MediumPurple;
                    dgvUrunler.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    dgvUrunler.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.MediumPurple;
                    dgvUrunler.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                    dgvUrunler.ColumnHeadersHeight = 45;

                    dgvUrunler.DefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
                    dgvUrunler.DefaultCellStyle.SelectionBackColor = Color.MediumPurple;
                    dgvUrunler.DefaultCellStyle.SelectionForeColor = Color.White;
                    dgvUrunler.GridColor = Color.LightGray;

                    dgvUrunler.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ürünler yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUygula_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(connectionString))
                {
                    string sorgu = @"SELECT 
                                u.UrunID, 
                                u.UrunAdi AS [Ürün Adı], 
                                k.KategoriAdi AS [Kategori], 
                                u.Fiyat 
                             FROM Urunler u
                             INNER JOIN Kategoriler k ON u.KategoriID = k.KategoriID
                             WHERE 1=1 ";

                    SqlCommand cmd = new SqlCommand();

                    // 1. ŞART: Eğer Kategori "Tümü" (KategoriID = 0) değilse, kategori filtresini ekle
                    if (Convert.ToInt32(cmbFiltreKategori.SelectedValue) != 0)
                    {
                        sorgu += " AND u.KategoriID = @KatID";
                        cmd.Parameters.AddWithValue("@KatID", cmbFiltreKategori.SelectedValue);
                    }

                    // 2. ŞART: Eğer Arama kutusu boş değilse, kelimeye göre arama filtresini ekle
                    if (!string.IsNullOrWhiteSpace(txtUrunAra.Text))
                    {
                        sorgu += " AND u.UrunAdi LIKE @AramaKelimesi";
                        cmd.Parameters.AddWithValue("@AramaKelimesi", "%" + txtUrunAra.Text.Trim() + "%");
                    }

                    cmd.CommandText = sorgu;
                    cmd.Connection = baglanti;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Filtrelenmiş sonuçları tabloya yansıtma
                    dgvUrunler.DataSource = dt;
                    dgvUrunler.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filtreleme sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUrunEkle_Click(object sender, EventArgs e)
        {
            frmUrunIslem islemFormu = new frmUrunIslem();
            islemFormu.GelenUrunID = 0; // 0 demek yeni kayıt işlemi demek

            // Popup formundan 'DialogResult.OK' dönerse (işlem başarılıysa) tabloyu yenile
            if (islemFormu.ShowDialog() == DialogResult.OK)
            {
                UrunleriGetir();
            }
        }

        private void btnUrunDuzenle_Click(object sender, EventArgs e)
        {
            // Önce tablodan bir şey seçilmiş mi kontrol et
            if (dgvUrunler.CurrentRow == null)
            {
                MessageBox.Show("Lütfen düzenlemek istediğiniz ürünü tablodan seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            frmUrunIslem islemFormu = new frmUrunIslem();

            // Seçili satırdaki 0. hücre (UrunID) bilgisini alıp gönderiyoruz
            islemFormu.GelenUrunID = Convert.ToInt32(dgvUrunler.CurrentRow.Cells["UrunID"].Value);

            if (islemFormu.ShowDialog() == DialogResult.OK)
            {
                UrunleriGetir();
            }
        }

        private void btnUrunSil_Click(object sender, EventArgs e)
        {
            if (dgvUrunler.CurrentRow == null)
            {
                MessageBox.Show("Lütfen silmek istediğiniz ürünü tablodan seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Seçili ID'yi ve Ürün Adını alıyoruz
            int seciliID = Convert.ToInt32(dgvUrunler.CurrentRow.Cells["UrunID"].Value);
            string seciliUrunAdi = dgvUrunler.CurrentRow.Cells["Ürün Adı"].Value.ToString();

            DialogResult cevap = MessageBox.Show($"'{seciliUrunAdi}' adlı ürünü menüden tamamen silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (cevap == DialogResult.Yes)
            {
                using (SqlConnection baglanti = new SqlConnection(connectionString))
                {
                    string sorgu = "DELETE FROM Urunler WHERE UrunID = @ID";
                    SqlCommand cmd = new SqlCommand(sorgu, baglanti);
                    cmd.Parameters.AddWithValue("@ID", seciliID);

                    baglanti.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Ürün başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UrunleriGetir();
            }
        }

        private void btnKategoriEkle_Click(object sender, EventArgs e)
        {
            frmKategoriIslem kategoriFormu = new frmKategoriIslem();

            if (kategoriFormu.ShowDialog() == DialogResult.OK)
            {
                KategorileriDoldur();
                MessageBox.Show("Kategori listesi güncellendi. Artık ürün eklerken yeni kategoriyi seçebilirsiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}