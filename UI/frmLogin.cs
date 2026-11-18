using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RestoranOtomasyonu
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = SqlBaglantisi.Adres;

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();

                // Kullanıcı adı ve şifreyi kontrol eden parametreli sql sorgusu
                SqlCommand komut = new SqlCommand("SELECT * FROM Kullanicilar WHERE KullaniciAdi=@kAdi AND Sifre=@sifre", baglanti);
                komut.Parameters.AddWithValue("@kAdi", txtKullaniciAdi.Text);
                komut.Parameters.AddWithValue("@sifre", txtSifre.Text);

                SqlDataReader dr = komut.ExecuteReader();

                if (dr.Read())
                {
                    string rol = dr["Yetki"].ToString();

                    MessageBox.Show("Giriş Başarılı! Sisteme " + rol + " olarak giriş yapıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    frmAnaSayfa anaSayfa = new frmAnaSayfa();

                    anaSayfa.GirenRol = dr["Yetki"].ToString();
                    anaSayfa.GirenKullaniciAdi = dr["KullaniciAdi"].ToString();

                    anaSayfa.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
