namespace RestoranOtomasyonu.UI
{
    partial class frmPaketServis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPaketServis));
            this.pnlKullaniciBilgi = new System.Windows.Forms.Panel();
            this.lblKullaniciRol = new System.Windows.Forms.Label();
            this.lblKullaniciAdi = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnTestKurye = new System.Windows.Forms.Button();
            this.btnTestSiparis = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flpIptalEdilenler = new System.Windows.Forms.FlowLayoutPanel();
            this.flpYeniSiparisler = new System.Windows.Forms.FlowLayoutPanel();
            this.flpYolda = new System.Windows.Forms.FlowLayoutPanel();
            this.flpTeslimEdilenler = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlKullaniciBilgi.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlKullaniciBilgi
            // 
            this.pnlKullaniciBilgi.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnlKullaniciBilgi.Controls.Add(this.lblKullaniciRol);
            this.pnlKullaniciBilgi.Controls.Add(this.lblKullaniciAdi);
            this.pnlKullaniciBilgi.Controls.Add(this.label5);
            this.pnlKullaniciBilgi.Controls.Add(this.label4);
            this.pnlKullaniciBilgi.Controls.Add(this.label3);
            this.pnlKullaniciBilgi.Location = new System.Drawing.Point(12, 21);
            this.pnlKullaniciBilgi.Name = "pnlKullaniciBilgi";
            this.pnlKullaniciBilgi.Size = new System.Drawing.Size(229, 90);
            this.pnlKullaniciBilgi.TabIndex = 1;
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
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Indigo;
            this.panel5.Controls.Add(this.btnTestKurye);
            this.panel5.Controls.Add(this.btnTestSiparis);
            this.panel5.Controls.Add(this.pnlKullaniciBilgi);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.tableLayoutPanel2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1472, 193);
            this.panel5.TabIndex = 3;
            // 
            // btnTestKurye
            // 
            this.btnTestKurye.Location = new System.Drawing.Point(371, 38);
            this.btnTestKurye.Name = "btnTestKurye";
            this.btnTestKurye.Size = new System.Drawing.Size(109, 67);
            this.btnTestKurye.TabIndex = 12;
            this.btnTestKurye.Text = "İlerlet";
            this.btnTestKurye.UseVisualStyleBackColor = true;
            this.btnTestKurye.Click += new System.EventHandler(this.btnTestKurye_Click);
            // 
            // btnTestSiparis
            // 
            this.btnTestSiparis.Location = new System.Drawing.Point(247, 39);
            this.btnTestSiparis.Name = "btnTestSiparis";
            this.btnTestSiparis.Size = new System.Drawing.Size(118, 66);
            this.btnTestSiparis.TabIndex = 8;
            this.btnTestSiparis.Text = "Test Sipariş";
            this.btnTestSiparis.UseVisualStyleBackColor = true;
            this.btnTestSiparis.Click += new System.EventHandler(this.btnTestSiparis_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label1.Location = new System.Drawing.Point(539, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(394, 28);
            this.label1.TabIndex = 1;
            this.label1.Text = "🛵 ONLINE PAKET SERVİS TAKİP MERKEZİ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label7, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label9, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label11, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 130);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 63F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1472, 63);
            this.tableLayoutPanel2.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(85, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(197, 28);
            this.label2.TabIndex = 2;
            this.label2.Text = "🔴 YENİ SİPARİŞLER";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Yellow;
            this.label7.Location = new System.Drawing.Point(445, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(214, 28);
            this.label7.TabIndex = 5;
            this.label7.Text = "🔴 YOLDA / KURYEDE";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label9.Location = new System.Drawing.Point(812, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(215, 28);
            this.label9.TabIndex = 7;
            this.label9.Text = "🔴 TESLİM EDİLENLER";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(1188, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(200, 28);
            this.label11.TabIndex = 10;
            this.label11.Text = "🔴 İPTAL EDİLENLER";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Silver;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.99813F));
            this.tableLayoutPanel1.Controls.Add(this.flpIptalEdilenler, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.flpYeniSiparisler, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flpYolda, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.flpTeslimEdilenler, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 193);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1472, 549);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // flpIptalEdilenler
            // 
            this.flpIptalEdilenler.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpIptalEdilenler.AutoScroll = true;
            this.flpIptalEdilenler.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpIptalEdilenler.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.flpIptalEdilenler.Location = new System.Drawing.Point(1107, 3);
            this.flpIptalEdilenler.Name = "flpIptalEdilenler";
            this.flpIptalEdilenler.Size = new System.Drawing.Size(362, 543);
            this.flpIptalEdilenler.TabIndex = 3;
            // 
            // flpYeniSiparisler
            // 
            this.flpYeniSiparisler.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpYeniSiparisler.AutoScroll = true;
            this.flpYeniSiparisler.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpYeniSiparisler.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpYeniSiparisler.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.flpYeniSiparisler.Location = new System.Drawing.Point(3, 3);
            this.flpYeniSiparisler.Name = "flpYeniSiparisler";
            this.flpYeniSiparisler.Size = new System.Drawing.Size(362, 543);
            this.flpYeniSiparisler.TabIndex = 0;
            this.flpYeniSiparisler.WrapContents = false;
            // 
            // flpYolda
            // 
            this.flpYolda.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpYolda.AutoScroll = true;
            this.flpYolda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpYolda.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.flpYolda.Location = new System.Drawing.Point(371, 3);
            this.flpYolda.Name = "flpYolda";
            this.flpYolda.Size = new System.Drawing.Size(362, 543);
            this.flpYolda.TabIndex = 1;
            // 
            // flpTeslimEdilenler
            // 
            this.flpTeslimEdilenler.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpTeslimEdilenler.AutoScroll = true;
            this.flpTeslimEdilenler.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpTeslimEdilenler.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.flpTeslimEdilenler.Location = new System.Drawing.Point(739, 3);
            this.flpTeslimEdilenler.Name = "flpTeslimEdilenler";
            this.flpTeslimEdilenler.Size = new System.Drawing.Size(362, 543);
            this.flpTeslimEdilenler.TabIndex = 2;
            // 
            // frmPaketServis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1472, 742);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel5);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmPaketServis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Online Siparişler";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPaketServis_FormClosing);
            this.Load += new System.EventHandler(this.frmPaketServis_Load);
            this.pnlKullaniciBilgi.ResumeLayout(false);
            this.pnlKullaniciBilgi.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlKullaniciBilgi;
        private System.Windows.Forms.Label lblKullaniciRol;
        private System.Windows.Forms.Label lblKullaniciAdi;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flpYeniSiparisler;
        private System.Windows.Forms.FlowLayoutPanel flpYolda;
        private System.Windows.Forms.FlowLayoutPanel flpTeslimEdilenler;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTestSiparis;
        private System.Windows.Forms.FlowLayoutPanel flpIptalEdilenler;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnTestKurye;
    }
}