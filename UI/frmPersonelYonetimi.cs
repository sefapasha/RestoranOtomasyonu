using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace RestoranOtomasyonu.UI
{
    public partial class frmPersonelYonetimi : Form
    {
        string connectionString = SqlBaglantisi.Adres;

        public frmPersonelYonetimi()
        {
            InitializeComponent();
        }

        private void frmPersonelYonetimi_Load(object sender, EventArgs e)
        {
            KullanicilariGetir();
        }

        private void KullanicilariGetir()
        {
            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                string sorgu = "SELECT * FROM Kullanicilar";
                SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvPersoneller.DataSource = dt;

                dgvPersoneller.AllowUserToAddRows = false;
                dgvPersoneller.ReadOnly = true;
                dgvPersoneller.RowHeadersVisible = false;
                dgvPersoneller.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvPersoneller.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // renk ve stil ayarları
                dgvPersoneller.EnableHeadersVisualStyles = false;
                dgvPersoneller.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                dgvPersoneller.ColumnHeadersDefaultCellStyle.BackColor = Color.MediumPurple;
                dgvPersoneller.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvPersoneller.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.MediumPurple;
                dgvPersoneller.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dgvPersoneller.ColumnHeadersHeight = 45;

                // iç hücrelerin stilini ayarlama
                dgvPersoneller.DefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
                dgvPersoneller.DefaultCellStyle.SelectionBackColor = Color.MediumPurple;
                dgvPersoneller.DefaultCellStyle.SelectionForeColor = Color.White;
                dgvPersoneller.GridColor = Color.LightGray;

                // başlık isimlerini Türkçeleştirme
                if (dgvPersoneller.Columns.Contains("KullaniciID"))
                    dgvPersoneller.Columns["KullaniciID"].HeaderText = "Personel ID";

                if (dgvPersoneller.Columns.Contains("KullaniciAdi"))
                    dgvPersoneller.Columns["KullaniciAdi"].HeaderText = "Kullanıcı Adı";

                if (dgvPersoneller.Columns.Contains("Sifre"))
                    dgvPersoneller.Columns["Sifre"].HeaderText = "Giriş Şifresi";

                if (dgvPersoneller.Columns.Contains("Yetki"))
                    dgvPersoneller.Columns["Yetki"].HeaderText = "Sistem Yetkisi";

                // !!!!Form açıldığında otomatik seçili gelen ilk satırın seçimini kaldır
                dgvPersoneller.ClearSelection();
            }
        }

        private void dgvPersoneller_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPersoneller.Rows[e.RowIndex];

                txtKullaniciAdi.Tag = row.Cells[0].Value.ToString();
                txtKullaniciAdi.Text = row.Cells[1].Value.ToString().Trim();
                txtSifre.Text = row.Cells[2].Value.ToString().Trim();

                if (row.Cells[3].Value != DBNull.Value)
                {
                    cmbYetki.Text = row.Cells[3].Value.ToString().Trim();
                }
                else
                {
                    cmbYetki.SelectedIndex = -1;
                }
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKullaniciAdi.Text) || string.IsNullOrWhiteSpace(txtSifre.Text) || cmbYetki.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                // 1 ------AYNI KULLANICI ADI VAR MI KONTROL ET
                string kontrolSorgu = "SELECT COUNT(*) FROM Kullanicilar WHERE KullaniciAdi=@KullaniciAdi";
                SqlCommand cmdKontrol = new SqlCommand(kontrolSorgu, baglanti);
                cmdKontrol.Parameters.AddWithValue("@KullaniciAdi", txtKullaniciAdi.Text.Trim());

                baglanti.Open();
                int kayitSayisi = (int)cmdKontrol.ExecuteScalar();

                if (kayitSayisi > 0)
                {
                    MessageBox.Show("Bu kullanıcı adı zaten kullanılıyor! Lütfen farklı bir ad seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Aynı isim varsa işlemi bitir
                }

                // 2 ------ EĞER İSİM YOKSA YENİ KAYDI EKLE
                string sorgu = "INSERT INTO Kullanicilar (KullaniciAdi, Sifre, Yetki) VALUES (@KullaniciAdi, @Sifre, @Yetki)";
                SqlCommand cmd = new SqlCommand(sorgu, baglanti);

                cmd.Parameters.AddWithValue("@KullaniciAdi", txtKullaniciAdi.Text.Trim());
                cmd.Parameters.AddWithValue("@Sifre", txtSifre.Text.Trim());
                cmd.Parameters.AddWithValue("@Yetki", cmbYetki.SelectedItem.ToString());

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Yeni personel başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            KullanicilariGetir();
            FormuTemizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (txtKullaniciAdi.Tag == null) return;

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                string sorgu = "UPDATE Kullanicilar SET KullaniciAdi=@KullaniciAdi, Sifre=@Sifre, Yetki=@Yetki WHERE KullaniciID=@ID";
                SqlCommand cmd = new SqlCommand(sorgu, baglanti);
                cmd.Parameters.AddWithValue("@KullaniciAdi", txtKullaniciAdi.Text.Trim());
                cmd.Parameters.AddWithValue("@Sifre", txtSifre.Text.Trim());

                cmd.Parameters.AddWithValue("@Yetki", cmbYetki.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(txtKullaniciAdi.Tag));

                baglanti.Open();
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Başarıyla güncellendi.","Güncelleme",MessageBoxButtons.OK,MessageBoxIcon.Information);
            KullanicilariGetir();
            FormuTemizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (txtKullaniciAdi.Tag == null)
            {
                MessageBox.Show("Lütfen silinecek personeli tablodan seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult cevap = MessageBox.Show("Bu personeli silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {
                using (SqlConnection baglanti = new SqlConnection(connectionString))
                {
                    string sorgu = "DELETE FROM Kullanicilar WHERE KullaniciID=@ID";
                    SqlCommand cmd = new SqlCommand(sorgu, baglanti);
                    cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(txtKullaniciAdi.Tag));

                    baglanti.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Personel silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                KullanicilariGetir();
                FormuTemizle();
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            FormuTemizle();
        }

        private void FormuTemizle()
        {
            txtKullaniciAdi.Clear();
            txtSifre.Clear();
            cmbYetki.SelectedIndex = -1;
            txtKullaniciAdi.Tag = null;
            dgvPersoneller.ClearSelection();
        }
    }
}