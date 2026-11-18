namespace RestoranOtomasyonu
{
    partial class frmSiparis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSiparis));
            this.flpKategoriler = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblToplam = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSatirSil = new System.Windows.Forms.Button();
            this.btnUrunSil = new System.Windows.Forms.Button();
            this.btnSiparisSil = new System.Windows.Forms.Button();
            this.btnSiparisKaydet = new System.Windows.Forms.Button();
            this.dgvAdisyon = new System.Windows.Forms.DataGridView();
            this.flpUrunler = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbBosMasalar = new System.Windows.Forms.ComboBox();
            this.btnMasaTasi = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdisyon)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpKategoriler
            // 
            this.flpKategoriler.BackColor = System.Drawing.SystemColors.ControlLight;
            this.flpKategoriler.Dock = System.Windows.Forms.DockStyle.Left;
            this.flpKategoriler.Location = new System.Drawing.Point(0, 0);
            this.flpKategoriler.Name = "flpKategoriler";
            this.flpKategoriler.Size = new System.Drawing.Size(183, 953);
            this.flpKategoriler.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.dgvAdisyon);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(909, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(573, 953);
            this.panel1.TabIndex = 1;
            // 
            // lblToplam
            // 
            this.lblToplam.AutoSize = true;
            this.lblToplam.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblToplam.Location = new System.Drawing.Point(132, 117);
            this.lblToplam.Name = "lblToplam";
            this.lblToplam.Size = new System.Drawing.Size(65, 29);
            this.lblToplam.TabIndex = 6;
            this.lblToplam.Text = "0 TL";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(3, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 29);
            this.label1.TabIndex = 5;
            this.label1.Text = "Toplam : ";
            // 
            // btnSatirSil
            // 
            this.btnSatirSil.BackColor = System.Drawing.Color.DarkRed;
            this.btnSatirSil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSatirSil.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSatirSil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSatirSil.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSatirSil.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSatirSil.Location = new System.Drawing.Point(290, 30);
            this.btnSatirSil.Name = "btnSatirSil";
            this.btnSatirSil.Size = new System.Drawing.Size(283, 61);
            this.btnSatirSil.TabIndex = 4;
            this.btnSatirSil.Text = "Satır Sil";
            this.btnSatirSil.UseVisualStyleBackColor = false;
            this.btnSatirSil.Click += new System.EventHandler(this.btnSatirSil_Click);
            // 
            // btnUrunSil
            // 
            this.btnUrunSil.BackColor = System.Drawing.Color.DarkRed;
            this.btnUrunSil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUrunSil.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnUrunSil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUrunSil.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnUrunSil.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnUrunSil.Location = new System.Drawing.Point(2, 30);
            this.btnUrunSil.Name = "btnUrunSil";
            this.btnUrunSil.Size = new System.Drawing.Size(283, 61);
            this.btnUrunSil.TabIndex = 3;
            this.btnUrunSil.Text = "Ürün Sil";
            this.btnUrunSil.UseVisualStyleBackColor = false;
            this.btnUrunSil.Click += new System.EventHandler(this.btnUrunSil_Click);
            // 
            // btnSiparisSil
            // 
            this.btnSiparisSil.BackColor = System.Drawing.Color.Red;
            this.btnSiparisSil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSiparisSil.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSiparisSil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSiparisSil.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSiparisSil.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSiparisSil.Location = new System.Drawing.Point(2, 175);
            this.btnSiparisSil.Name = "btnSiparisSil";
            this.btnSiparisSil.Size = new System.Drawing.Size(283, 61);
            this.btnSiparisSil.TabIndex = 2;
            this.btnSiparisSil.Text = "Siparişi Sil";
            this.btnSiparisSil.UseVisualStyleBackColor = false;
            this.btnSiparisSil.Click += new System.EventHandler(this.btnSiparisSil_Click);
            // 
            // btnSiparisKaydet
            // 
            this.btnSiparisKaydet.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnSiparisKaydet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSiparisKaydet.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSiparisKaydet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSiparisKaydet.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSiparisKaydet.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSiparisKaydet.Location = new System.Drawing.Point(289, 175);
            this.btnSiparisKaydet.Name = "btnSiparisKaydet";
            this.btnSiparisKaydet.Size = new System.Drawing.Size(283, 61);
            this.btnSiparisKaydet.TabIndex = 1;
            this.btnSiparisKaydet.Text = "Siparişi Onayla";
            this.btnSiparisKaydet.UseVisualStyleBackColor = false;
            this.btnSiparisKaydet.Click += new System.EventHandler(this.btnSiparisKaydet_Click);
            // 
            // dgvAdisyon
            // 
            this.dgvAdisyon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAdisyon.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAdisyon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAdisyon.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvAdisyon.Location = new System.Drawing.Point(0, 0);
            this.dgvAdisyon.Name = "dgvAdisyon";
            this.dgvAdisyon.RowHeadersVisible = false;
            this.dgvAdisyon.RowHeadersWidth = 51;
            this.dgvAdisyon.RowTemplate.Height = 24;
            this.dgvAdisyon.Size = new System.Drawing.Size(573, 619);
            this.dgvAdisyon.TabIndex = 0;
            // 
            // flpUrunler
            // 
            this.flpUrunler.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.flpUrunler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpUrunler.Location = new System.Drawing.Point(183, 0);
            this.flpUrunler.Name = "flpUrunler";
            this.flpUrunler.Size = new System.Drawing.Size(726, 953);
            this.flpUrunler.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(16, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(190, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Taşınacak Masa : ";
            // 
            // cmbBosMasalar
            // 
            this.cmbBosMasalar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBosMasalar.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cmbBosMasalar.FormattingEnabled = true;
            this.cmbBosMasalar.Location = new System.Drawing.Point(212, 23);
            this.cmbBosMasalar.Name = "cmbBosMasalar";
            this.cmbBosMasalar.Size = new System.Drawing.Size(151, 37);
            this.cmbBosMasalar.TabIndex = 8;
            // 
            // btnMasaTasi
            // 
            this.btnMasaTasi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnMasaTasi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMasaTasi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnMasaTasi.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnMasaTasi.Location = new System.Drawing.Point(406, 23);
            this.btnMasaTasi.Name = "btnMasaTasi";
            this.btnMasaTasi.Size = new System.Drawing.Size(151, 37);
            this.btnMasaTasi.TabIndex = 9;
            this.btnMasaTasi.Text = "Masayı Taşı";
            this.btnMasaTasi.UseVisualStyleBackColor = false;
            this.btnMasaTasi.Click += new System.EventHandler(this.btnMasaTasi_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox1.Controls.Add(this.btnMasaTasi);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbBosMasalar);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox1.Location = new System.Drawing.Point(0, 619);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(573, 334);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Masa Taşıma İşlemleri";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.groupBox2.Controls.Add(this.btnSatirSil);
            this.groupBox2.Controls.Add(this.btnSiparisKaydet);
            this.groupBox2.Controls.Add(this.lblToplam);
            this.groupBox2.Controls.Add(this.btnSiparisSil);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnUrunSil);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox2.Location = new System.Drawing.Point(0, 695);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(573, 258);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sipariş İşlemleri";
            // 
            // frmSiparis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Indigo;
            this.ClientSize = new System.Drawing.Size(1482, 953);
            this.Controls.Add(this.flpUrunler);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flpKategoriler);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSiparis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sipariş Ekranı";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmSiparis_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdisyon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpKategoriler;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flpUrunler;
        private System.Windows.Forms.DataGridView dgvAdisyon;
        private System.Windows.Forms.Button btnSiparisKaydet;
        private System.Windows.Forms.Button btnSiparisSil;
        private System.Windows.Forms.Label lblToplam;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSatirSil;
        private System.Windows.Forms.Button btnUrunSil;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnMasaTasi;
        private System.Windows.Forms.ComboBox cmbBosMasalar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}