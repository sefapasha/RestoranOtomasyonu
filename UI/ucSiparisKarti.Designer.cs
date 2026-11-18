namespace RestoranOtomasyonu.UI
{
    partial class ucSiparisKarti
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblMusteriAdi = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSiparisIcerigi = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTutar = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblAdres = new System.Windows.Forms.Label();
            this.lblSure = new System.Windows.Forms.Label();
            this.tmrSure = new System.Windows.Forms.Timer(this.components);
            this.btnDurum = new System.Windows.Forms.Button();
            this.btnIptal = new System.Windows.Forms.Button();
            this.prgSure = new System.Windows.Forms.ProgressBar();
            this.lblSiparisID = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblMusteriAdi
            // 
            this.lblMusteriAdi.AutoSize = true;
            this.lblMusteriAdi.Location = new System.Drawing.Point(137, 32);
            this.lblMusteriAdi.Name = "lblMusteriAdi";
            this.lblMusteriAdi.Size = new System.Drawing.Size(72, 16);
            this.lblMusteriAdi.TabIndex = 0;
            this.lblMusteriAdi.Text = "Müşteri adi";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "👤 Müşteri:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(19, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "🍔 Sipariş İçeriği : ";
            // 
            // lblSiparisIcerigi
            // 
            this.lblSiparisIcerigi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblSiparisIcerigi.Location = new System.Drawing.Point(137, 130);
            this.lblSiparisIcerigi.Name = "lblSiparisIcerigi";
            this.lblSiparisIcerigi.Size = new System.Drawing.Size(182, 76);
            this.lblSiparisIcerigi.TabIndex = 3;
            this.lblSiparisIcerigi.Text = "sipariş iceriği";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Green;
            this.label3.Location = new System.Drawing.Point(24, 206);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "💵 Tutar :";
            // 
            // lblTutar
            // 
            this.lblTutar.AutoSize = true;
            this.lblTutar.ForeColor = System.Drawing.Color.Green;
            this.lblTutar.Location = new System.Drawing.Point(89, 206);
            this.lblTutar.Name = "lblTutar";
            this.lblTutar.Size = new System.Drawing.Size(32, 16);
            this.lblTutar.TabIndex = 5;
            this.lblTutar.Text = "tutar";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label4.Location = new System.Drawing.Point(19, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "📍 Adres:";
            // 
            // lblAdres
            // 
            this.lblAdres.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblAdres.Location = new System.Drawing.Point(137, 64);
            this.lblAdres.Name = "lblAdres";
            this.lblAdres.Size = new System.Drawing.Size(182, 53);
            this.lblAdres.TabIndex = 7;
            this.lblAdres.Text = "adreş";
            // 
            // lblSure
            // 
            this.lblSure.AutoSize = true;
            this.lblSure.BackColor = System.Drawing.Color.Transparent;
            this.lblSure.Location = new System.Drawing.Point(158, 248);
            this.lblSure.Name = "lblSure";
            this.lblSure.Size = new System.Drawing.Size(38, 16);
            this.lblSure.TabIndex = 8;
            this.lblSure.Text = "00:00";
            // 
            // tmrSure
            // 
            this.tmrSure.Interval = 1000;
            this.tmrSure.Tick += new System.EventHandler(this.tmrSure_Tick);
            // 
            // btnDurum
            // 
            this.btnDurum.BackColor = System.Drawing.Color.Green;
            this.btnDurum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDurum.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnDurum.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDurum.Location = new System.Drawing.Point(22, 285);
            this.btnDurum.Name = "btnDurum";
            this.btnDurum.Size = new System.Drawing.Size(94, 31);
            this.btnDurum.TabIndex = 9;
            this.btnDurum.Text = "İleri";
            this.btnDurum.UseVisualStyleBackColor = false;
            this.btnDurum.Click += new System.EventHandler(this.btnDurum_Click);
            // 
            // btnIptal
            // 
            this.btnIptal.BackColor = System.Drawing.Color.Red;
            this.btnIptal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIptal.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnIptal.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnIptal.Location = new System.Drawing.Point(238, 285);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(94, 31);
            this.btnIptal.TabIndex = 10;
            this.btnIptal.Text = "İptal";
            this.btnIptal.UseVisualStyleBackColor = false;
            this.btnIptal.Click += new System.EventHandler(this.btnIptal_Click);
            // 
            // prgSure
            // 
            this.prgSure.Location = new System.Drawing.Point(22, 244);
            this.prgSure.Name = "prgSure";
            this.prgSure.Size = new System.Drawing.Size(310, 23);
            this.prgSure.TabIndex = 11;
            // 
            // lblSiparisID
            // 
            this.lblSiparisID.AutoSize = true;
            this.lblSiparisID.Location = new System.Drawing.Point(19, 12);
            this.lblSiparisID.Name = "lblSiparisID";
            this.lblSiparisID.Size = new System.Drawing.Size(68, 16);
            this.lblSiparisID.TabIndex = 12;
            this.lblSiparisID.Text = "Sipariş ID:";
            // 
            // ucSiparisKarti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblSiparisID);
            this.Controls.Add(this.lblSure);
            this.Controls.Add(this.prgSure);
            this.Controls.Add(this.btnIptal);
            this.Controls.Add(this.btnDurum);
            this.Controls.Add(this.lblAdres);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblTutar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblSiparisIcerigi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMusteriAdi);
            this.Name = "ucSiparisKarti";
            this.Size = new System.Drawing.Size(355, 335);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMusteriAdi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSiparisIcerigi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTutar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblAdres;
        private System.Windows.Forms.Label lblSure;
        private System.Windows.Forms.Timer tmrSure;
        private System.Windows.Forms.Button btnDurum;
        private System.Windows.Forms.Button btnIptal;
        private System.Windows.Forms.ProgressBar prgSure;
        private System.Windows.Forms.Label lblSiparisID;
    }
}
