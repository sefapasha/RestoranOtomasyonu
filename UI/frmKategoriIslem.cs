using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RestoranOtomasyonu.UI
{
    public partial class frmKategoriIslem : Form
    {
        string connectionString = SqlBaglantisi.Adres;

        public frmKategoriIslem()
        {
            InitializeComponent();
        }

        private void btnKategoriKaydet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKategoriAdi.Text))
            {
                MessageBox.Show("Lütfen kategori adını boş bırakmayın!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection baglanti = new SqlConnection(connectionString))
                {
                    string sorgu = "INSERT INTO Kategoriler (KategoriAdi) VALUES (@Ad)";
                    SqlCommand cmd = new SqlCommand(sorgu, baglanti);
                    cmd.Parameters.AddWithValue("@Ad", txtKategoriAdi.Text.Trim());

                    baglanti.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Yeni kategori başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kategori eklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmKategoriIslem_Load(object sender, EventArgs e)
        {

        }
    }
}