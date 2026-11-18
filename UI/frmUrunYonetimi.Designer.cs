namespace RestoranOtomasyonu.UI
{
    partial class frmUrunYonetimi
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUrunYonetimi));
            this.label1 = new System.Windows.Forms.Label();
            this.pnlSol = new System.Windows.Forms.Panel();
            this.btnKategoriEkle = new System.Windows.Forms.Button();
            this.btnUrunSil = new System.Windows.Forms.Button();
            this.btnUrunDuzenle = new System.Windows.Forms.Button();
            this.btnUrunEkle = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlKullaniciBilgi = new System.Windows.Forms.Panel();
            this.lblKullaniciRol = new System.Windows.Forms.Label();
            this.lblKullaniciAdi = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnUygula = new System.Windows.Forms.Button();
            this.txtUrunAra = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbFiltreKategori = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvUrunler = new System.Windows.Forms.DataGridView();
            this.pnlSol.SuspendLayout();
            this.pnlKullaniciBilgi.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUrunler)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(16, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Menü ve Ürün Yönetimi";
            // 
            // pnlSol
            // 
            this.pnlSol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSol.Controls.Add(this.btnKategoriEkle);
            this.pnlSol.Controls.Add(this.btnUrunSil);
            this.pnlSol.Controls.Add(this.btnUrunDuzenle);
            this.pnlSol.Controls.Add(this.btnUrunEkle);
            this.pnlSol.Controls.Add(this.label2);
            this.pnlSol.Controls.Add(this.pnlKullaniciBilgi);
            this.pnlSol.Location = new System.Drawing.Point(17, 50);
            this.pnlSol.Name = "pnlSol";
            this.pnlSol.Size = new System.Drawing.Size(238, 629);
            this.pnlSol.TabIndex = 2;
            // 
            // btnKategoriEkle
            // 
            this.btnKategoriEkle.BackColor = System.Drawing.Color.Green;
            this.btnKategoriEkle.FlatAppearance.BorderSize = 0;
            this.btnKategoriEkle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKategoriEkle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnKategoriEkle.ForeColor = System.Drawing.SystemColors.Control;
            this.btnKategoriEkle.Location = new System.Drawing.Point(3, 344);
            this.btnKategoriEkle.Name = "btnKategoriEkle";
            this.btnKategoriEkle.Size = new System.Drawing.Size(230, 67);
            this.btnKategoriEkle.TabIndex = 5;
            this.btnKategoriEkle.Text = "Kategori Ekle";
            this.btnKategoriEkle.UseVisualStyleBackColor = false;
            this.btnKategoriEkle.Click += new System.EventHandler(this.btnKategoriEkle_Click);
            // 
            // btnUrunSil
            // 
            this.btnUrunSil.BackColor = System.Drawing.Color.DarkRed;
            this.btnUrunSil.FlatAppearance.BorderSize = 0;
            this.btnUrunSil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUrunSil.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnUrunSil.ForeColor = System.Drawing.SystemColors.Control;
            this.btnUrunSil.Location = new System.Drawing.Point(3, 271);
            this.btnUrunSil.Name = "btnUrunSil";
            this.btnUrunSil.Size = new System.Drawing.Size(230, 67);
            this.btnUrunSil.TabIndex = 4;
            this.btnUrunSil.Text = "Ürün Sil";
            this.btnUrunSil.UseVisualStyleBackColor = false;
            this.btnUrunSil.Click += new System.EventHandler(this.btnUrunSil_Click);
            // 
            // btnUrunDuzenle
            // 
            this.btnUrunDuzenle.BackColor = System.Drawing.Color.DarkOrange;
            this.btnUrunDuzenle.FlatAppearance.BorderSize = 0;
            this.btnUrunDuzenle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUrunDuzenle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnUrunDuzenle.ForeColor = System.Drawing.SystemColors.Control;
            this.btnUrunDuzenle.Location = new System.Drawing.Point(3, 198);
            this.btnUrunDuzenle.Name = "btnUrunDuzenle";
            this.btnUrunDuzenle.Size = new System.Drawing.Size(230, 67);
            this.btnUrunDuzenle.TabIndex = 3;
            this.btnUrunDuzenle.Text = "Ürün Düzenle";
            this.btnUrunDuzenle.UseVisualStyleBackColor = false;
            this.btnUrunDuzenle.Click += new System.EventHandler(this.btnUrunDuzenle_Click);
            // 
            // btnUrunEkle
            // 
            this.btnUrunEkle.BackColor = System.Drawing.Color.Green;
            this.btnUrunEkle.FlatAppearance.BorderSize = 0;
            this.btnUrunEkle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUrunEkle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnUrunEkle.ForeColor = System.Drawing.SystemColors.Control;
            this.btnUrunEkle.Location = new System.Drawing.Point(3, 125);
            this.btnUrunEkle.Name = "btnUrunEkle";
            this.btnUrunEkle.Size = new System.Drawing.Size(230, 67);
            this.btnUrunEkle.TabIndex = 2;
            this.btnUrunEkle.Text = "Ürün Ekle";
            this.btnUrunEkle.UseVisualStyleBackColor = false;
            this.btnUrunEkle.Click += new System.EventHandler(this.btnUrunEkle_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "İşlemler";
            // 
            // pnlKullaniciBilgi
            // 
            this.pnlKullaniciBilgi.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnlKullaniciBilgi.Controls.Add(this.lblKullaniciRol);
            this.pnlKullaniciBilgi.Controls.Add(this.lblKullaniciAdi);
            this.pnlKullaniciBilgi.Controls.Add(this.label5);
            this.pnlKullaniciBilgi.Controls.Add(this.label4);
            this.pnlKullaniciBilgi.Controls.Add(this.label3);
            this.pnlKullaniciBilgi.Location = new System.Drawing.Point(4, 29);
            this.pnlKullaniciBilgi.Name = "pnlKullaniciBilgi";
            this.pnlKullaniciBilgi.Size = new System.Drawing.Size(229, 90);
            this.pnlKullaniciBilgi.TabIndex = 0;
            // 
            // lblKullaniciRol
            // 
            this.lblKullaniciRol.AutoSize = true;
            this.lblKullaniciRol.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblKullaniciRol.Location = new System.Drawing.Point(50, 62);
            this.lblKullaniciRol.Name = "lblKullaniciRol";
            this.lblKullaniciRol.Size = new System.Drawing.Size(28, 17);
            this.lblKullaniciRol.TabIndex = 4;
            this.lblKullaniciRol.Text = "Rol";
            // 
            // lblKullaniciAdi
            // 
            this.lblKullaniciAdi.AutoSize = true;
            this.lblKullaniciAdi.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblKullaniciAdi.Location = new System.Drawing.Point(94, 30);
            this.lblKullaniciAdi.Name = "lblKullaniciAdi";
            this.lblKullaniciAdi.Size = new System.Drawing.Size(29, 17);
            this.lblKullaniciAdi.TabIndex = 3;
            this.lblKullaniciAdi.Text = "Adı";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(3, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Rolü :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(3, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "Kullanıcı Adı :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Aktif Kullanıcı Bilgileri";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnUygula);
            this.panel1.Controls.Add(this.txtUrunAra);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.cmbFiltreKategori);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Location = new System.Drawing.Point(261, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1209, 100);
            this.panel1.TabIndex = 3;
            // 
            // btnUygula
            // 
            this.btnUygula.BackColor = System.Drawing.Color.Green;
            this.btnUygula.FlatAppearance.BorderSize = 0;
            this.btnUygula.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUygula.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnUygula.ForeColor = System.Drawing.SystemColors.Control;
            this.btnUygula.Location = new System.Drawing.Point(354, 59);
            this.btnUygula.Name = "btnUygula";
            this.btnUygula.Size = new System.Drawing.Size(121, 30);
            this.btnUygula.TabIndex = 8;
            this.btnUygula.Text = "Uygula";
            this.btnUygula.UseVisualStyleBackColor = false;
            this.btnUygula.Click += new System.EventHandler(this.btnUygula_Click);
            // 
            // txtUrunAra
            // 
            this.txtUrunAra.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtUrunAra.Location = new System.Drawing.Point(179, 59);
            this.txtUrunAra.Name = "txtUrunAra";
            this.txtUrunAra.Size = new System.Drawing.Size(121, 30);
            this.txtUrunAra.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(176, 38);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(28, 17);
            this.label8.TabIndex = 10;
            this.label8.Text = "Ara";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(7, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 17);
            this.label7.TabIndex = 9;
            this.label7.Text = "Kategori Adı";
            // 
            // cmbFiltreKategori
            // 
            this.cmbFiltreKategori.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cmbFiltreKategori.FormattingEnabled = true;
            this.cmbFiltreKategori.Location = new System.Drawing.Point(7, 58);
            this.cmbFiltreKategori.Name = "cmbFiltreKategori";
            this.cmbFiltreKategori.Size = new System.Drawing.Size(121, 31);
            this.cmbFiltreKategori.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(3, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 23);
            this.label6.TabIndex = 8;
            this.label6.Text = "Filtreleme";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.dgvUrunler);
            this.panel2.Location = new System.Drawing.Point(261, 156);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1209, 523);
            this.panel2.TabIndex = 4;
            // 
            // dgvUrunler
            // 
            this.dgvUrunler.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvUrunler.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUrunler.Location = new System.Drawing.Point(3, 3);
            this.dgvUrunler.Name = "dgvUrunler";
            this.dgvUrunler.RowHeadersWidth = 51;
            this.dgvUrunler.RowTemplate.Height = 24;
            this.dgvUrunler.Size = new System.Drawing.Size(1201, 515);
            this.dgvUrunler.TabIndex = 0;
            // 
            // frmUrunYonetimi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Indigo;
            this.ClientSize = new System.Drawing.Size(1482, 694);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlSol);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmUrunYonetimi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ürün Yönetimi";
            this.Load += new System.EventHandler(this.frmUrunYonetimi_Load);
            this.pnlSol.ResumeLayout(false);
            this.pnlSol.PerformLayout();
            this.pnlKullaniciBilgi.ResumeLayout(false);
            this.pnlKullaniciBilgi.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUrunler)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlSol;
        private System.Windows.Forms.Panel pnlKullaniciBilgi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblKullaniciRol;
        private System.Windows.Forms.Label lblKullaniciAdi;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnUrunSil;
        private System.Windows.Forms.Button btnUrunDuzenle;
        private System.Windows.Forms.Button btnUrunEkle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnKategoriEkle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbFiltreKategori;
        private System.Windows.Forms.Button btnUygula;
        private System.Windows.Forms.TextBox txtUrunAra;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgvUrunler;
    }
}