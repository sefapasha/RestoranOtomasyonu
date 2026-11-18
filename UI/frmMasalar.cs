using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace RestoranOtomasyonu
{
    public partial class frmMasalar : Form
    {
        public frmMasalar()
        {
            InitializeComponent();
        }

        private void MasalariYukle()
        {
            flowLayoutPanel1.Controls.Clear();

            string connectionString = SqlBaglantisi.Adres;

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("SELECT * FROM Masalar", baglanti);
                SqlDataReader dr = komut.ExecuteReader();

                while (dr.Read())
                {
                    Button btnMasa = new Button();
                    btnMasa.Name = "btn_" + dr["MasaID"].ToString();
                    btnMasa.Width = 120;
                    btnMasa.Height = 100;
                    btnMasa.Margin = new Padding(10);
                    btnMasa.FlatStyle = FlatStyle.Flat;
                    btnMasa.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    btnMasa.Cursor = Cursors.Hand;

                    // oluşturulan her butona bir tıklanma özelliği ekliyor
                    btnMasa.Click += BtnMasa_Click;

                    // Masanın durumuna göre renk ve yazı **(0: Boş, 1: Dolu)**
                    bool durum = Convert.ToBoolean(dr["Durum"]);

                    if (durum == false) // Eğer masa BOŞ ise
                    {
                        btnMasa.BackColor = Color.LimeGreen;
                        btnMasa.ForeColor = Color.White;
                        btnMasa.Text = dr["MasaAdi"].ToString() + "\n(Boş)";
                    }
                    else // Eğer masa DOLU ise
                    {
                        btnMasa.BackColor = Color.Crimson;
                        btnMasa.ForeColor = Color.White;
                        btnMasa.Text = dr["MasaAdi"].ToString() + "\n(Dolu)";
                    }

                    flowLayoutPanel1.Controls.Add(btnMasa);
                }
                dr.Close();
            }
        }

        private void frmMasalar_Load(object sender, EventArgs e)
        {
            MasalariYukle();
        }

        private void BtnMasa_Click(object sender, EventArgs e)
        {
            Button tiklananMasa = (Button)sender;

            // Butonun içindeki "Masa 1 \n (Boş)" yazısından sadece "Masa 1" kısmını alıyoruz ileride frmSiparis in title ı olacak
            string safMasaAdi = tiklananMasa.Text.Split('\n')[0];

            // Butonun ismindeki "btn_1" kısmından sadece "1" kısmını alıyoruz ve int e çeviriyoruz ileride frmSiparis e masaID olarak gönderilecek
            int masaID = Convert.ToInt32(tiklananMasa.Name.Replace("btn_", ""));

            frmSiparis siparisEkrani = new frmSiparis();
            siparisEkrani.SecilenMasaAdi = safMasaAdi;
            siparisEkrani.SecilenMasaID = masaID;

            siparisEkrani.ShowDialog();

            MasalariYukle();
        }
    }
}