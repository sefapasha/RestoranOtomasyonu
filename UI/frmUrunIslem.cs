using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RestoranOtomasyonu.UI
{
    public partial class frmUrunIslem : Form
    {
        string connectionString = SqlBaglantisi.Adres;

        // frmUrunYonetiminden gelecek Ürün ID (0 ise yeni ekleme, değilse güncelleme )
        public int GelenUrunID { get; set; }

        public frmUrunIslem()
        {
            InitializeComponent();
        }

        private void frmUrunIslem_Load(object sender, EventArgs e)
        {
            KategorileriDoldur(); // Önce ComboBox'ı kategorilerle doldur

            if (GelenUrunID == 0)
            {
                this.Text = "Yeni Ürün Ekle";
                btnKaydet.Text = "Ekle";
            }
            else
            {
                this.Text = "Ürün Düzenle";
                btnKaydet.Text = "Güncelle";
                UrunBilgileriniGetir(); // Güncellenecek ürünün mevcut bilgilerini kutulara doldur
            }
        }

        private void KategorileriDoldur()
        {
            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                string sorgu = "SELECT KategoriID, KategoriAdi FROM Kategoriler";
                SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbKategori.DataSource = dt;
                cmbKategori.DisplayMember = "KategoriAdi";
                cmbKategori.ValueMember = "KategoriID";
                cmbKategori.SelectedIndex = -1;
            }
        }

        private void UrunBilgileriniGetir()
        {
            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                string sorgu = "SELECT UrunAdi, KategoriID, Fiyat FROM Urunler WHERE UrunID = @ID";
                SqlCommand cmd = new SqlCommand(sorgu, baglanti);
                cmd.Parameters.AddWithValue("@ID", GelenUrunID);

                baglanti.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtUrunAdi.Text = dr["UrunAdi"].ToString();
                    cmbKategori.SelectedValue = dr["KategoriID"];
                    txtFiyat.Text = dr["Fiyat"].ToString();
                }
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUrunAdi.Text) || string.IsNullOrWhiteSpace(txtFiyat.Text) || cmbKategori.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Fiyatın sayısal bir değer olup olmadığını kontrol et
            decimal fiyat;
            if (!decimal.TryParse(txtFiyat.Text, out fiyat))
            {
                MessageBox.Show("Lütfen geçerli bir fiyat girin! (Örn: 150 veya 150,50)", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglanti;

                if (GelenUrunID == 0) // YENİ EKLEME
                {
                    cmd.CommandText = "INSERT INTO Urunler (UrunAdi, KategoriID, Fiyat) VALUES (@Ad, @Kategori, @Fiyat)";
                }
                else // GÜNCELLEME ////////////////////
                {
                    cmd.CommandText = "UPDATE Urunler SET UrunAdi=@Ad, KategoriID=@Kategori, Fiyat=@Fiyat WHERE UrunID=@ID";
                    cmd.Parameters.AddWithValue("@ID", GelenUrunID);
                }

                cmd.Parameters.AddWithValue("@Ad", txtUrunAdi.Text.Trim());
                cmd.Parameters.AddWithValue("@Kategori", cmbKategori.SelectedValue);
                cmd.Parameters.AddWithValue("@Fiyat", fiyat);

                baglanti.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("İşlem başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK; // Ana forma "işlem tamamlandı" mesajı gönderiyoruz
            this.Close(); // Küçük pencereyi kapat
        }
    }
}