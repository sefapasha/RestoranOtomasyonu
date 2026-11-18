using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace RestoranOtomasyonu
{
    public partial class frmSiparis : Form
    {
        public string SecilenMasaAdi { get; set; }
        public int SecilenMasaID { get; set; } // Veritabanı işlemleri için masanın ID si
        public frmSiparis()
        {
            InitializeComponent();
        }

        private void frmSiparis_Load(object sender, EventArgs e)
        {
            // Adisyon listesinin ayarları ve sütunları
            dgvAdisyon.ColumnCount = 3;
            dgvAdisyon.Columns[0].Name = "Ürün Adı";
            dgvAdisyon.Columns[1].Name = "Adet";
            dgvAdisyon.Columns[2].Name = "Fiyat (₺)";

            // Kullanıcı listeye elle boş satır giremesin ve sadece okuyabilsin
            dgvAdisyon.AllowUserToAddRows = false;
            dgvAdisyon.ReadOnly = true;
            dgvAdisyon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Formun başlığı = masa adı - Adisyon Ekranı yazisı
            this.Text = SecilenMasaAdi + " - Adisyon Ekranı";

            // Sql baglanti adresi
            string connectionString = SqlBaglantisi.Adres;

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("SELECT * FROM Kategoriler", baglanti);
                SqlDataReader dr = komut.ExecuteReader();

                // Kategori butonlarını dinamik olarak oluşturuyoruz
                while (dr.Read())
                {
                    Button btnKategori = new Button();
                    btnKategori.Text = dr["KategoriAdi"].ToString();
                    btnKategori.Name = "kat_" + dr["KategoriID"].ToString();
                    btnKategori.Width = 140;
                    btnKategori.Height = 60;
                    btnKategori.FlatStyle = FlatStyle.Flat;
                    btnKategori.BackColor = Color.Indigo;
                    btnKategori.ForeColor = Color.White;
                    btnKategori.FlatAppearance.BorderSize = 0;
                    btnKategori.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                    btnKategori.Cursor = Cursors.Hand;

                    btnKategori.Click += BtnKategori_Click;

                    flpKategoriler.Controls.Add(btnKategori);
                }
                dr.Close(); // DataReader kapatıyoruz ki SQL hata vermesin
            }
            AdisyonuGetir();

            // Boş masaları ComboBOX'a ekliyoruz
            cmbBosMasalar.Items.Clear();
            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();
                // Sadece Durum = 0 yani Boş) olan masaları getiriyoruz
                SqlCommand cmdMasa = new SqlCommand("SELECT MasaAdi FROM Masalar WHERE Durum = 0", baglanti);
                SqlDataReader drMasa = cmdMasa.ExecuteReader();

                while (drMasa.Read())
                {
                    cmbBosMasalar.Items.Add(drMasa["MasaAdi"].ToString());
                }
                drMasa.Close();
            }
        }

        private void AdisyonuGetir()
        {
            dgvAdisyon.Rows.Clear();
            decimal genelToplam = 0;

            string connectionString = SqlBaglantisi.Adres;
            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand(@"
                SELECT U.UrunID, U.UrunAdi, SD.Adet, SD.Fiyat 
                FROM Siparisler S 
                INNER JOIN SiparisDetay SD ON S.SiparisID = SD.SiparisID 
                INNER JOIN Urunler U ON SD.UrunID = U.UrunID 
                WHERE S.MasaID = @masaID AND S.Durum = 1", baglanti);

                cmd.Parameters.AddWithValue("@masaID", SecilenMasaID);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    int uID = Convert.ToInt32(dr["UrunID"]);
                    string uAdi = dr["UrunAdi"].ToString();
                    int adet = Convert.ToInt32(dr["Adet"]);
                    decimal fiyat = Convert.ToDecimal(dr["Fiyat"]);

                    decimal satirToplami = adet * fiyat;
                    genelToplam += satirToplami;

                    int satir = dgvAdisyon.Rows.Add(uAdi, adet, satirToplami.ToString("0.00"));
                    dgvAdisyon.Rows[satir].Tag = uID;
                }
                dr.Close();
            }
            // toplam tutarı label'a yazdırıyoruz
            lblToplam.Text = genelToplam.ToString("0.00") + " ₺";
        }
        private void BtnKategori_Click(object sender, EventArgs e)
        {
            Button tiklananKategori = (Button)sender;

            int kategoriID = Convert.ToInt32(tiklananKategori.Name.Replace("kat_", ""));

            flpUrunler.Controls.Clear();

            string connectionString = SqlBaglantisi.Adres;

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("SELECT * FROM Urunler WHERE KategoriID = @katID", baglanti);
                komut.Parameters.AddWithValue("@katID", kategoriID);
                SqlDataReader dr = komut.ExecuteReader();

                while (dr.Read())
                {
                    // yeni bir Ürün butonu
                    Button btnUrun = new Button();

                    // Ürün adı ve fiyatını alt altayazdırıyoruz
                    btnUrun.Text = dr["UrunAdi"].ToString() + "\n" + dr["Fiyat"].ToString() + " ₺";
                    btnUrun.Name = "urun_" + dr["UrunID"].ToString();
                    btnUrun.Width = 120;
                    btnUrun.Height = 100;
                    btnUrun.Margin = new Padding(10); // Butonlar arası boşluk

                    btnUrun.FlatStyle = FlatStyle.Flat;
                    btnUrun.BackColor = Color.White;
                    btnUrun.ForeColor = Color.Black;
                    btnUrun.FlatAppearance.BorderColor = Color.Indigo; 
                    btnUrun.FlatAppearance.BorderSize = 2; 
                    btnUrun.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    btnUrun.Cursor = Cursors.Hand;

                    btnUrun.Click += BtnUrun_Click; 

                    flpUrunler.Controls.Add(btnUrun);
                }
            }
        }
        private void BtnUrun_Click(object sender, EventArgs e)
        {
            Button tiklananUrun = (Button)sender;
            int urunID = Convert.ToInt32(tiklananUrun.Name.Replace("urun_", ""));

            string[] urunBilgisi = tiklananUrun.Text.Split('\n');
            decimal birimFiyat = Convert.ToDecimal(urunBilgisi[1].Replace(" ₺", "").Trim());

            string connectionString = SqlBaglantisi.Adres;
            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();

                // 1 ---- Masanın açık siparişi var mı bak, yoksa YENİ SİPARİŞ aç
                SqlCommand cmdKontrol = new SqlCommand("SELECT SiparisID FROM Siparisler WHERE MasaID = @masaID AND Durum = 1", baglanti);
                cmdKontrol.Parameters.AddWithValue("@masaID", SecilenMasaID);
                object sonuc = cmdKontrol.ExecuteScalar();

                int siparisID = 0;
                if (sonuc == null)
                {
                    SqlCommand cmdYeniSiparis = new SqlCommand("INSERT INTO Siparisler (MasaID, ToplamTutar, Durum) VALUES (@masaID, 0, 1); SELECT SCOPE_IDENTITY();", baglanti);
                    cmdYeniSiparis.Parameters.AddWithValue("@masaID", SecilenMasaID);
                    siparisID = Convert.ToInt32(cmdYeniSiparis.ExecuteScalar());

                    // Masa durumunu Dolu yap
                    new SqlCommand($"UPDATE Masalar SET Durum = 1 WHERE MasaID = {SecilenMasaID}", baglanti).ExecuteNonQuery();
                }
                else
                {
                    siparisID = Convert.ToInt32(sonuc);
                }

                // 2 ---- Bu ürün zaten eklenmiş mi, Eklendiyse adet artır yoksa yeni satır ekle
                SqlCommand cmdDetayKontrol = new SqlCommand("SELECT Adet FROM SiparisDetay WHERE SiparisID = @sipID AND UrunID = @uID", baglanti);
                cmdDetayKontrol.Parameters.AddWithValue("@sipID", siparisID);
                cmdDetayKontrol.Parameters.AddWithValue("@uID", urunID);
                object detaySonuc = cmdDetayKontrol.ExecuteScalar();

                if (detaySonuc != null) // Zaten sepette var UPDATE at
                {
                    SqlCommand cmdUpdate = new SqlCommand("UPDATE SiparisDetay SET Adet = Adet + 1 WHERE SiparisID = @sipID AND UrunID = @uID", baglanti);
                    cmdUpdate.Parameters.AddWithValue("@sipID", siparisID);
                    cmdUpdate.Parameters.AddWithValue("@uID", urunID);
                    cmdUpdate.ExecuteNonQuery();
                }
                else // Sepette yok INSERT at.
                {
                    SqlCommand cmdInsert = new SqlCommand("INSERT INTO SiparisDetay (SiparisID, UrunID, Adet, Fiyat) VALUES (@sipID, @uID, 1, @fiyat)", baglanti);
                    cmdInsert.Parameters.AddWithValue("@sipID", siparisID);
                    cmdInsert.Parameters.AddWithValue("@uID", urunID);
                    cmdInsert.Parameters.AddWithValue("@fiyat", birimFiyat);
                    cmdInsert.ExecuteNonQuery();
                }
            }

            // db güncellendi ekranı yenıle
            AdisyonuGetir();
        }

        private void btnSiparisKaydet_Click(object sender, EventArgs e)
        {
            if (dgvAdisyon.Rows.Count == 0) return; // Sepet boşsa işlem yapma

            // === DETAYLI FİŞ METNİ===
            string fisDetayi = "--- SİPARİŞ ÖZETİ ---\n\n";

            foreach (DataGridViewRow row in dgvAdisyon.Rows)
            {
                string uAdi = row.Cells[0].Value.ToString();
                string adet = row.Cells[1].Value.ToString();
                string fiyat = row.Cells[2].Value.ToString();

                fisDetayi += $"{uAdi} (x{adet}) = {fiyat} ₺\n"; // Örn: Adana Kebap (x5) = 1000,00 ₺
            }

            fisDetayi += $"\nGENEL TOPLAM: {lblToplam.Text}\n\nHesap ödendi olarak işaretlenip masa kapatılacak. Onaylıyor musunuz?";

            // Fiş metnini MessageBox içinde gösterme ve kullanıcıdan onay alma veya iptal etme
            DialogResult secim = MessageBox.Show(fisDetayi, "Hesabı Öde", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (secim == DialogResult.Yes)
            {
                string connectionString = SqlBaglantisi.Adres;
                using (SqlConnection baglanti = new SqlConnection(connectionString))
                {
                    baglanti.Open();
                    // ÖNCE --- Son tutarı ana sipariş tablosuna yazalım
                    decimal odenecekTutar = Convert.ToDecimal(lblToplam.Text.Replace(" ₺", ""));
                    new SqlCommand($"UPDATE Siparisler SET ToplamTutar = {odenecekTutar.ToString().Replace(',', '.')}, Durum = 0 WHERE MasaID = {SecilenMasaID} AND Durum = 1", baglanti).ExecuteNonQuery();

                    // SONRA ---- Masayı boş yap yeşil renge çevir
                    new SqlCommand($"UPDATE Masalar SET Durum = 0 WHERE MasaID = {SecilenMasaID}", baglanti).ExecuteNonQuery();
                }
                MessageBox.Show("Hesap başarıyla kapatıldı!", "Ödeme Alındı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Masalara dön
            }
        }

        private void btnUrunSil_Click(object sender, EventArgs e)
        {
            if (dgvAdisyon.CurrentRow == null) return;
            int urunID = Convert.ToInt32(dgvAdisyon.CurrentRow.Tag);
            int mevcutAdet = Convert.ToInt32(dgvAdisyon.CurrentRow.Cells[1].Value);

            string connectionString = SqlBaglantisi.Adres;
            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();
                if (mevcutAdet > 1) // Adet 1'den büyükse sadece 1 düşür
                {
                    new SqlCommand($"UPDATE SiparisDetay SET Adet = Adet - 1 WHERE UrunID = {urunID} AND SiparisID = (SELECT SiparisID FROM Siparisler WHERE MasaID = {SecilenMasaID} AND Durum = 1)", baglanti).ExecuteNonQuery();
                }
                else // Adet 1 ise komple satırı sil
                {
                    new SqlCommand($"DELETE FROM SiparisDetay WHERE UrunID = {urunID} AND SiparisID = (SELECT SiparisID FROM Siparisler WHERE MasaID = {SecilenMasaID} AND Durum = 1)", baglanti).ExecuteNonQuery();
                }
            }
            int seciliIndex = dgvAdisyon.CurrentRow.Index;

            AdisyonuGetir(); // ekranı yenileme

            // Ekran yenilendiktn sonra seçimi geri eski yerine alıyoruz
            if (dgvAdisyon.Rows.Count > 0)
            {
                // Eğer sildiğimiz ürün listenin EN SONUNDAKİ ürünse ve artık yoksa, bir üsttekine geç
                if (seciliIndex >= dgvAdisyon.Rows.Count)
                {
                    seciliIndex = dgvAdisyon.Rows.Count - 1;
                }

                dgvAdisyon.Rows[seciliIndex].Selected = true;
                dgvAdisyon.CurrentCell = dgvAdisyon.Rows[seciliIndex].Cells[0];
            }
        }

        private void btnSatirSil_Click(object sender, EventArgs e)
        {
            if (dgvAdisyon.CurrentRow == null) return;
            int urunID = Convert.ToInt32(dgvAdisyon.CurrentRow.Tag);

            string connectionString = SqlBaglantisi.Adres;
            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();
                new SqlCommand($"DELETE FROM SiparisDetay WHERE UrunID = {urunID} AND SiparisID = (SELECT SiparisID FROM Siparisler WHERE MasaID = {SecilenMasaID} AND Durum = 1)", baglanti).ExecuteNonQuery();
            }
            int seciliIndex = dgvAdisyon.CurrentRow.Index;

            AdisyonuGetir();

            // Ekran tazelendikten sonra seçimi geri eski yerine alıyoruz
            if (dgvAdisyon.Rows.Count > 0)
            {
                // Eğer sildiğimiz ürün listenin EN SONUNDAKİ ürünse ve artık yoksa, bir üsttekine geç
                if (seciliIndex >= dgvAdisyon.Rows.Count)
                {
                    seciliIndex = dgvAdisyon.Rows.Count - 1;
                }

                dgvAdisyon.Rows[seciliIndex].Selected = true; 
                dgvAdisyon.CurrentCell = dgvAdisyon.Rows[seciliIndex].Cells[0]; 
            }
        }

        private void btnSiparisSil_Click(object sender, EventArgs e)
        {
            DialogResult secim = MessageBox.Show("Tüm siparişi iptal etmek istediğinize emin misiniz?", "Sipariş İptal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (secim == DialogResult.Yes)
            {
                string connectionString = SqlBaglantisi.Adres;
                using (SqlConnection baglanti = new SqlConnection(connectionString))
                {
                    baglanti.Open();
                    // Masanın ID ile ilişkili tüm sipariş detaylarını ve siparişleri sil
                    new SqlCommand($"DELETE FROM SiparisDetay WHERE SiparisID = (SELECT SiparisID FROM Siparisler WHERE MasaID = {SecilenMasaID} AND Durum = 1)", baglanti).ExecuteNonQuery();
                    new SqlCommand($"DELETE FROM Siparisler WHERE MasaID = {SecilenMasaID} AND Durum = 1", baglanti).ExecuteNonQuery();

                    // Masayı boşa (yeşile)çıkarıyoruz ama formda kalmaya devam ediyoruz this.Close() yapmadık çünkü
                    new SqlCommand($"UPDATE Masalar SET Durum = 0 WHERE MasaID = {SecilenMasaID}", baglanti).ExecuteNonQuery();
                }
                AdisyonuGetir();
                MessageBox.Show("Mevcut sipariş iptal edildi. Masaya yeni sipariş girebilirsiniz.", "Sipariş Temizlendi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnMasaTasi_Click(object sender, EventArgs e)
        {
            // listeden masa seçmeden butona basarsa hata verecek
            if (cmbBosMasalar.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen taşınacak boş bir masa seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string hedefMasaAdi = cmbBosMasalar.SelectedItem.ToString();

            // masa taşıma işlemi için onay alınıyor
            DialogResult secim = MessageBox.Show($"Tüm adisyon '{hedefMasaAdi}' adlı masaya taşınacak. Onaylıyor musunuz?", "Masa Taşıma", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (secim == DialogResult.Yes)
            {
                string connectionString = SqlBaglantisi.Adres;
                using (SqlConnection baglanti = new SqlConnection(connectionString))
                {
                    baglanti.Open();

                    // 1. Yeni masanın IDsini sqlden bul
                    SqlCommand cmdHedefID = new SqlCommand("SELECT MasaID FROM Masalar WHERE MasaAdi = @hedefAdi", baglanti);
                    cmdHedefID.Parameters.AddWithValue("@hedefAdi", hedefMasaAdi);
                    int hedefMasaID = Convert.ToInt32(cmdHedefID.ExecuteScalar());

                    // 2. Açık olan yani "Durum=1" siparişin MasaID sini YENİ MASA ile değiştir
                    SqlCommand cmdTasi = new SqlCommand("UPDATE Siparisler SET MasaID = @yeniMasaID WHERE MasaID = @eskiMasaID AND Durum = 1", baglanti);
                    cmdTasi.Parameters.AddWithValue("@yeniMasaID", hedefMasaID);
                    cmdTasi.Parameters.AddWithValue("@eskiMasaID", SecilenMasaID);
                    cmdTasi.ExecuteNonQuery();

                    // 3. Eski masayı BOŞ (0) yap
                    SqlCommand cmdEskiMasaBosalt = new SqlCommand("UPDATE Masalar SET Durum = 0 WHERE MasaID = @eskiMasaID", baglanti);
                    cmdEskiMasaBosalt.Parameters.AddWithValue("@eskiMasaID", SecilenMasaID);
                    cmdEskiMasaBosalt.ExecuteNonQuery();

                    // 4. yeni masayı DOLU (1) yap
                    SqlCommand cmdYeniMasaDoldur = new SqlCommand("UPDATE Masalar SET Durum = 1 WHERE MasaID = @yeniMasaID", baglanti);
                    cmdYeniMasaDoldur.Parameters.AddWithValue("@yeniMasaID", hedefMasaID);
                    cmdYeniMasaDoldur.ExecuteNonQuery();
                }

                MessageBox.Show("Masa başarıyla taşındı!", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
    }
}
