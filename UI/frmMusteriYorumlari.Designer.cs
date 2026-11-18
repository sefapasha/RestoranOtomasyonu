namespace RestoranOtomasyonu.UI
{
    partial class frmMusteriYorumlari
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMusteriYorumlari));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblOrtalamaSure = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblToplamYorum = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblOrtalamaPuan = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvYorumlar = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvYorumlar)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Indigo;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1082, 167);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblOrtalamaSure);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(723, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(356, 161);
            this.panel3.TabIndex = 2;
            // 
            // lblOrtalamaSure
            // 
            this.lblOrtalamaSure.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOrtalamaSure.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblOrtalamaSure.ForeColor = System.Drawing.Color.White;
            this.lblOrtalamaSure.Location = new System.Drawing.Point(3, 74);
            this.lblOrtalamaSure.Name = "lblOrtalamaSure";
            this.lblOrtalamaSure.Size = new System.Drawing.Size(353, 87);
            this.lblOrtalamaSure.TabIndex = 1;
            this.lblOrtalamaSure.Text = "0";
            this.lblOrtalamaSure.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(26, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(304, 38);
            this.label6.TabIndex = 0;
            this.label6.Text = "⏱ Ort. Teslimat Süresi";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblToplamYorum);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(363, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(354, 161);
            this.panel2.TabIndex = 1;
            // 
            // lblToplamYorum
            // 
            this.lblToplamYorum.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblToplamYorum.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblToplamYorum.ForeColor = System.Drawing.Color.White;
            this.lblToplamYorum.Location = new System.Drawing.Point(0, 74);
            this.lblToplamYorum.Name = "lblToplamYorum";
            this.lblToplamYorum.Size = new System.Drawing.Size(354, 87);
            this.lblToplamYorum.TabIndex = 1;
            this.lblToplamYorum.Text = "0";
            this.lblToplamYorum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(54, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(247, 38);
            this.label4.TabIndex = 0;
            this.label4.Text = "✅ Toplam Yorum";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblOrtalamaPuan);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(354, 161);
            this.panel1.TabIndex = 0;
            // 
            // lblOrtalamaPuan
            // 
            this.lblOrtalamaPuan.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblOrtalamaPuan.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblOrtalamaPuan.ForeColor = System.Drawing.Color.White;
            this.lblOrtalamaPuan.Location = new System.Drawing.Point(-3, 74);
            this.lblOrtalamaPuan.Name = "lblOrtalamaPuan";
            this.lblOrtalamaPuan.Size = new System.Drawing.Size(357, 87);
            this.lblOrtalamaPuan.TabIndex = 1;
            this.lblOrtalamaPuan.Text = "0";
            this.lblOrtalamaPuan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(57, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "⭐ Ortalama Puan";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dgvYorumlar
            // 
            this.dgvYorumlar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvYorumlar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvYorumlar.Location = new System.Drawing.Point(0, 167);
            this.dgvYorumlar.Name = "dgvYorumlar";
            this.dgvYorumlar.RowHeadersWidth = 51;
            this.dgvYorumlar.RowTemplate.Height = 24;
            this.dgvYorumlar.Size = new System.Drawing.Size(1082, 486);
            this.dgvYorumlar.TabIndex = 2;
            this.dgvYorumlar.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvYorumlar_CellFormatting);
            // 
            // frmMusteriYorumlari
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1082, 653);
            this.Controls.Add(this.dgvYorumlar);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMusteriYorumlari";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Müşteri Yorumları";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMusteriYorumlari_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvYorumlar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblOrtalamaSure;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblToplamYorum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblOrtalamaPuan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvYorumlar;
    }
}