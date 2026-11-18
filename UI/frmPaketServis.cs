using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections.Generic;

namespace RestoranOtomasyonu.UI
{
    public partial class frmPaketServis : Form
    {
        string connectionString = SqlBaglantisi.Adres;

        public string AktifKullanici { get; set; }
        public string AktifRol { get; set; }

        private Timer timerOtoSiparis = new Timer();
        private Timer timerOtoIlerleme = new Timer();
        private Random rnd = new Random();

        private List<string> iptalNedenleri = new List<string>();

        public frmPaketServis()
        {
            InitializeComponent();

            IptalNedenleriniYukle();

            // Form kapanınca timerların durmaı için
            this.FormClosing += frmPaketServis_FormClosing;

            // 30 Saniyede bir otomatik sipariş üretici
            timerOtoSiparis.Interval = 30000;
            timerOtoSiparis.Tick += TimerOtoSiparis_Tick;
            timerOtoSiparis.Start();

            // 20 Saniyede bir kuryedeki siparişi otomatik ilerletme
            timerOtoIlerleme.Interval = 20000;
            timerOtoIlerleme.Tick += TimerOtoIlerleme_Tick;
            timerOtoIlerleme.Start();
        }

        private void IptalNedenleriniYukle()
        {
            iptalNedenleri.Clear();

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("SELECT NedenAciklamasi FROM IptalNedenleri", baglanti);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    iptalNedenleri.Add(dr["NedenAciklamasi"].ToString());
                }
                dr.Close();
            }

            // Tablo boş kalırsa varsayılan olarak müşteri siparişten vazgeçti ekliyoruz
            if (iptalNedenleri.Count == 0)
            {
                iptalNedenleri.Add("Müşteri siparişten vazgeçti");
            }
        }

        private void frmPaketServis_Load(object sender, EventArgs e)
        {
            lblKullaniciAdi.Text = AktifKullanici;
            lblKullaniciRol.Text = AktifRol;

            SiparisleriGetir();
        }

        // ŞİMDİLİK BÖYLE KALIN ONRA DÜELTECEM(yukarıda)**************************************************-*-*-*-*---***
        private void frmPaketServis_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (timerOtoSiparis != null)
            {
                timerOtoSiparis.Stop();
                timerOtoSiparis.Dispose();
            }
            if (timerOtoIlerleme != null)
            {
                timerOtoIlerleme.Stop();
                timerOtoIlerleme.Dispose();
            }
        }
        ///-*-*-***************************************************************-----------------

        // 30 Saniyede bir otomatik çalışan sipariş üretici
        private void TimerOtoSiparis_Tick(object sender, EventArgs e)
        {
            timerOtoSiparis.Stop();
            YeniSiparisUretVeEkle();
            timerOtoSiparis.Start();
        }

        // 20 Saniyede bir kuryedeki siparişi otomatik ilerleten mekanizma
        private void TimerOtoIlerleme_Tick(object sender, EventArgs e)
        {
            if (flpYolda.Controls.Count == 0) return;

            timerOtoIlerleme.Stop();

            ucSiparisKarti yoldakiKart = (ucSiparisKarti)flpYolda.Controls[0];
            int ihtimal = rnd.Next(1, 100);

            if (ihtimal <= 20)
            {
                string rastgeleNeden = iptalNedenleri[rnd.Next(iptalNedenleri.Count)];
                SiparisiIptalEt(yoldakiKart, rastgeleNeden);
            }
            else
            {
                SiparisiIlerlet(yoldakiKart);
            }

            timerOtoIlerleme.Start();
        }

        // =========================================================================
        // SİPARİŞ ÜRETİMİ VE EKRANA EKLEME
        // =========================================================================
        private void YeniSiparisUretVeEkle()
        {
            string musteri = "Müşteri " + rnd.Next(1000, 9999).ToString();
            string adres = "Merkez Mah. No:" + rnd.Next(1, 100).ToString() + " D:" + rnd.Next(1, 20).ToString();

            string icerik = "";
            decimal toplamTutar = 0;
            int urunCesitSayisi = rnd.Next(1, 4);

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();
                string sorgu = $"SELECT TOP {urunCesitSayisi} UrunAdi, Fiyat FROM Urunler ORDER BY NEWID()";
                SqlCommand cmdUrun = new SqlCommand(sorgu, baglanti);
                SqlDataReader dr = cmdUrun.ExecuteReader();

                while (dr.Read())
                {
                    string uAdi = dr["UrunAdi"].ToString();
                    decimal birimFiyat = Convert.ToDecimal(dr["Fiyat"]);
                    int adet = rnd.Next(1, 4); // 1 ile 3 arasında rastgele adet

                    if (icerik != "") icerik += ", ";
                    icerik += $"{adet}x {uAdi}";
                    toplamTutar += (adet * birimFiyat);
                }
                dr.Close();
            }

            YENISiparisEkleSQLVeEkran(musteri, adres, icerik, toplamTutar);
        }

        private void YENISiparisEkleSQLVeEkran(string musteri, string adres, string icerik, decimal tutar)
        {
            int yeniPaketID = 0;
            DateTime siparisZamani = DateTime.Now;

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();
                string insertQuery = @"INSERT INTO PaketSiparisler (MusteriAdi, Adres, SiparisIcerigi, Tutar, Durum, SiparisZamani) 
                                       VALUES (@musteri, @adres, @icerik, @tutar, 1, @zamani); 
                                       SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(insertQuery, baglanti);
                cmd.Parameters.AddWithValue("@musteri", musteri);
                cmd.Parameters.AddWithValue("@adres", adres);
                cmd.Parameters.AddWithValue("@icerik", icerik);
                cmd.Parameters.AddWithValue("@tutar", tutar);
                cmd.Parameters.AddWithValue("@zamani", siparisZamani);

                yeniPaketID = Convert.ToInt32(cmd.ExecuteScalar());
            }

            ucSiparisKarti yeniKart = new ucSiparisKarti();
            yeniKart.KartiDoldur(yeniPaketID, musteri, adres, icerik, tutar, siparisZamani);

            yeniKart.IleriTiklandi += Kart_IleriTiklandi;
            yeniKart.IptalTiklandi += Kart_IptalTiklandi;
            yeniKart.SuresiDoldu += Kart_SuresiDoldu;

            flpYeniSiparisler.Controls.Add(yeniKart);
            flpYeniSiparisler.Controls.SetChildIndex(yeniKart, 0);
            flpYeniSiparisler.Refresh();
            Application.DoEvents();

            MessageBox.Show($"Yeni Paket Siparişi Geldi!\n\nSipariş No: #{yeniPaketID}\n{musteri}\nTutar: ₺{tutar:0.00}", "Yeni Sipariş Alındı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // =========================================================================
        // SİPARİŞ YÜKLEME VE İLERLETME
        // =========================================================================
        private void SiparisleriGetir()
        {
            flpYeniSiparisler.Controls.Clear();
            flpYolda.Controls.Clear();
            flpTeslimEdilenler.Controls.Clear();
            if (flpIptalEdilenler != null) flpIptalEdilenler.Controls.Clear();

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();

                string otoIptalQuery = @"UPDATE PaketSiparisler 
                                         SET Durum = 4, IptalNedeni = 'Süresi doldu' 
                                         WHERE Durum IN (1, 2) 
                                           AND DATEDIFF(second, SiparisZamani, GETDATE()) >= 300";
                SqlCommand cmdOtoIptal = new SqlCommand(otoIptalQuery, baglanti);
                cmdOtoIptal.ExecuteNonQuery();

                string sorgu = @"SELECT * FROM PaketSiparisler 
                                 WHERE Durum IN (1, 2) 
                                    OR (Durum = 3 AND TeslimZamani >= DATEADD(minute, -2, GETDATE()))
                                    OR (Durum = 4 AND SiparisZamani >= DATEADD(minute, -2, GETDATE()))
                                 ORDER BY PaketID DESC";

                SqlCommand komut = new SqlCommand(sorgu, baglanti);
                SqlDataReader dr = komut.ExecuteReader();

                while (dr.Read())
                {
                    int paketID = Convert.ToInt32(dr["PaketID"]);
                    string musteriAdi = dr["MusteriAdi"].ToString();
                    string adres = dr["Adres"].ToString();
                    string icerik = dr["SiparisIcerigi"].ToString();
                    decimal tutar = Convert.ToDecimal(dr["Tutar"]);
                    int durum = Convert.ToInt32(dr["Durum"]);
                    DateTime siparisZamani = Convert.ToDateTime(dr["SiparisZamani"]);
                    string iptalNedeni = dr["IptalNedeni"] != DBNull.Value ? dr["IptalNedeni"].ToString() : "";

                    ucSiparisKarti kart = new ucSiparisKarti();
                    kart.KartiDoldur(paketID, musteriAdi, adres, icerik, tutar, siparisZamani);

                    kart.IleriTiklandi += Kart_IleriTiklandi;
                    kart.IptalTiklandi += Kart_IptalTiklandi;
                    kart.SuresiDoldu += Kart_SuresiDoldu;

                    if (durum == 1) // Yeni siparişler paneline ekle
                    {
                        flpYeniSiparisler.Controls.Add(kart);
                    }
                    else if (durum == 2) // Yolda paneline ekle
                    {
                        kart.YoldaModunaGec();
                        flpYolda.Controls.Add(kart);
                    }
                    else if (durum == 3) // Teslim edilenler paneline ekle
                    {
                        kart.TeslimEdildiYap();
                        flpTeslimEdilenler.Controls.Add(kart);
                        OtomatikTemizleEkle(flpTeslimEdilenler, kart);
                    }
                    else if (durum == 4 && flpIptalEdilenler != null) // İptal edilenler paneline ekle
                    {
                        kart.IptalEdildiYap(iptalNedeni);
                        flpIptalEdilenler.Controls.Add(kart);
                        OtomatikTemizleEkle(flpIptalEdilenler, kart);
                    }
                }
                dr.Close();
            }
        }

        // Kartın ileri butonuna tıklandığında siparişi ilerletmek için çağrılan event
        private void Kart_IleriTiklandi(object sender, EventArgs e)
        {
            ucSiparisKarti tiklananKart = (ucSiparisKarti)sender;
            SiparisiIlerlet(tiklananKart);
        }

        private void SiparisiIlerlet(ucSiparisKarti kart)
        {
            int paketID = kart.SiparisID;

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();

                if (flpYeniSiparisler.Controls.Contains(kart))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE PaketSiparisler SET Durum = 2 WHERE PaketID = @id", baglanti);
                    cmd.Parameters.AddWithValue("@id", paketID);
                    cmd.ExecuteNonQuery();

                    flpYeniSiparisler.Controls.Remove(kart); // Yeni siparişler panelinden kaldırma
                    kart.YoldaModunaGec();                   // Kartın durumunu yolda olarak değiştir
                    flpYolda.Controls.Add(kart);             // Yolda paneline ekleme flowlaayoutpanel
                }
                else if (flpYolda.Controls.Contains(kart))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE PaketSiparisler SET Durum = 3, TeslimZamani = GETDATE() WHERE PaketID = @id", baglanti);
                    cmd.Parameters.AddWithValue("@id", paketID);
                    cmd.ExecuteNonQuery();

                    // kurye teslim ettiğinde otomatik olarak yorum ekleme
                    OtomatikYorumEkle(paketID);

                    flpYolda.Controls.Remove(kart);         // Yolda panelinden kaldırma
                    flpTeslimEdilenler.Controls.Add(kart);  // Teslim edilenler paneline ekleme

                    kart.TeslimEdildiYap();                 // Kartın durumunu teslim edildi yap ve yeşil yap
                    MessageBox.Show($"Sipariş #{paketID} BAŞARIYLA TESLİM EDİLDİ!", "Teslimat Tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    OtomatikTemizleEkle(flpTeslimEdilenler, kart);  // 2 dakika sonra teslim edilenler panelinden kaldır
                }
            }
        }

        // 5 dakika içinde ilerletmediğimiz siparişleri otomatik iptal etme
        private void Kart_SuresiDoldu(object sender, EventArgs e)
        {
            ucSiparisKarti kart = (ucSiparisKarti)sender;
            if (flpYeniSiparisler.Controls.Contains(kart) || flpYolda.Controls.Contains(kart))
            {
                SiparisiIptalEt(kart, "Süresi doldu");
            }
        }

        // Manuel iptal edince iptal nedeni sorup iptal etme
        private void Kart_IptalTiklandi(object sender, EventArgs e)
        {
            ucSiparisKarti tiklananKart = (ucSiparisKarti)sender;
            string iptalNedeni = Microsoft.VisualBasic.Interaction.InputBox("Lütfen sipariş iptal nedenini giriniz:", "Sipariş İptal Et", "Müşteri vazgeçti");

            if (!string.IsNullOrWhiteSpace(iptalNedeni))
            {
                SiparisiIptalEt(tiklananKart, iptalNedeni);
            }
        }

        private void SiparisiIptalEt(ucSiparisKarti kart, string iptalNedeni)
        {
            int paketID = kart.SiparisID;

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();
                string updateQuery = "UPDATE PaketSiparisler SET Durum = 4, IptalNedeni = @neden WHERE PaketID = @id";
                SqlCommand cmd = new SqlCommand(updateQuery, baglanti);
                cmd.Parameters.AddWithValue("@neden", iptalNedeni);
                cmd.Parameters.AddWithValue("@id", paketID);
                cmd.ExecuteNonQuery();
            }

            if (flpYeniSiparisler.Controls.Contains(kart)) flpYeniSiparisler.Controls.Remove(kart);
            if (flpYolda.Controls.Contains(kart)) flpYolda.Controls.Remove(kart);

            kart.IptalEdildiYap(iptalNedeni);
            if (flpIptalEdilenler != null) flpIptalEdilenler.Controls.Add(kart);

            MessageBox.Show($"Sipariş #{paketID} İPTAL EDİLDİ!\n\nNedeni: {iptalNedeni}", "Sipariş İptal Edildi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            OtomatikTemizleEkle(flpIptalEdilenler, kart);
        }

        // otomatik olarak teslim edilen veya iptal edilen siparişleri 2 dakika sonra panelden kaldırmak için
        private void OtomatikTemizleEkle(FlowLayoutPanel panel, ucSiparisKarti kart)
        {
            Timer timerSil = new Timer();
            timerSil.Interval = 60000;
            timerSil.Tick += (s, ev) =>
            {
                if (panel != null && panel.Controls.Contains(kart))
                {
                    panel.Controls.Remove(kart);
                    kart.Dispose();
                }
                timerSil.Stop();
                timerSil.Dispose();
            };
            timerSil.Start();
        }

        // test butonları ile sipariş üretme
        private void btnTestSiparis_Click(object sender, EventArgs e)
        {
            YeniSiparisUretVeEkle();
        }

        // test butonları ile kuryedeki siparişi ilerletme
        private void btnTestKurye_Click(object sender, EventArgs e)
        {
            if (flpYolda.Controls.Count > 0)
            {
                TimerOtoIlerleme_Tick(null, null);
            }
            else
            {
                MessageBox.Show("Şu an yolda/kuryede bekleyen herhangi bir sipariş bulunmuyor.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void OtomatikYorumEkle(int paketID)
        {
            int zar = rnd.Next(1, 101);
            int puan;

            if (zar <= 60)
                puan = 5;              // %60 ihtimalle 5 Yıldız
            else if (zar <= 88)
                puan = 4;              // %28 ihtimalle 4 Yıldız
            else if (zar <= 96)
                puan = 3;              // %8 ihtimalle 3 Yıldız
            else
                puan = rnd.Next(1, 3); // %4 ihtimalle 1 veya 2 Yıldız

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();
                string query = @"IF NOT EXISTS (SELECT 1 FROM MusteriYorumlari WHERE PaketID = @pId)
                         BEGIN
                             INSERT INTO MusteriYorumlari (PaketID, MusteriAdi, Puan, YorumMetni, Tarih)
                             SELECT 
                                 PaketID, 
                                 MusteriAdi, 
                                 @puan, 
                                 ISNULL(
                                     (SELECT TOP 1 YorumMetni FROM HazirYorumlar WHERE @puan BETWEEN MinPuan AND MaxPuan ORDER BY NEWID()), 
                                     N'Teşekkürler, elinize sağlık.'
                                 ), 
                                 GETDATE()
                             FROM PaketSiparisler
                             WHERE PaketID = @pId
                         END";

                SqlCommand cmd = new SqlCommand(query, baglanti);
                cmd.Parameters.AddWithValue("@pId", paketID);
                cmd.Parameters.AddWithValue("@puan", puan);
                cmd.ExecuteNonQuery();
            }
        }
    }
}