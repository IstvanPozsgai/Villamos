namespace Villamos.V_Ablakok._4_Nyilvántartások.Nóta
{
    partial class Ablak_Fődarab
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Holtart = new System.Windows.Forms.ProgressBar();
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.Táblalista = new Zuby.ADGV.AdvancedDataGridView();
            this.Frissíti_táblalistát = new System.Windows.Forms.Button();
            this.Excel_gomb = new System.Windows.Forms.Button();
            this.BtnSAP = new System.Windows.Forms.Button();
            this.Módosítás = new System.Windows.Forms.Button();
            this.Aktív = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.Táblalista)).BeginInit();
            this.SuspendLayout();
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(12, 13);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(825, 27);
            this.Holtart.TabIndex = 172;
            this.Holtart.Visible = false;
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(856, 3);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 171;
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // Táblalista
            // 
            this.Táblalista.AllowUserToAddRows = false;
            this.Táblalista.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Táblalista.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.Táblalista.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Táblalista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Táblalista.FilterAndSortEnabled = true;
            this.Táblalista.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Táblalista.Location = new System.Drawing.Point(12, 102);
            this.Táblalista.MaxFilterButtonImageHeight = 23;
            this.Táblalista.Name = "Táblalista";
            this.Táblalista.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Táblalista.Size = new System.Drawing.Size(886, 140);
            this.Táblalista.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Táblalista.TabIndex = 185;
            this.Táblalista.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Táblalista_CellClick);
            // 
            // Frissíti_táblalistát
            // 
            this.Frissíti_táblalistát.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Frissíti_táblalistát.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Frissíti_táblalistát.Location = new System.Drawing.Point(164, 51);
            this.Frissíti_táblalistát.Name = "Frissíti_táblalistát";
            this.Frissíti_táblalistát.Size = new System.Drawing.Size(45, 45);
            this.Frissíti_táblalistát.TabIndex = 184;
            this.Frissíti_táblalistát.UseVisualStyleBackColor = true;
            this.Frissíti_táblalistát.Click += new System.EventHandler(this.Frissíti_táblalistát_Click);
            // 
            // Excel_gomb
            // 
            this.Excel_gomb.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excel_gomb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excel_gomb.Location = new System.Drawing.Point(215, 51);
            this.Excel_gomb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Excel_gomb.Name = "Excel_gomb";
            this.Excel_gomb.Size = new System.Drawing.Size(45, 45);
            this.Excel_gomb.TabIndex = 183;
            this.Excel_gomb.UseVisualStyleBackColor = true;
            this.Excel_gomb.Click += new System.EventHandler(this.Excel_gomb_Click);
            // 
            // BtnSAP
            // 
            this.BtnSAP.BackgroundImage = global::Villamos.Properties.Resources.SAP;
            this.BtnSAP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSAP.Location = new System.Drawing.Point(302, 51);
            this.BtnSAP.Name = "BtnSAP";
            this.BtnSAP.Size = new System.Drawing.Size(45, 45);
            this.BtnSAP.TabIndex = 186;
            this.BtnSAP.UseVisualStyleBackColor = true;
            this.BtnSAP.Click += new System.EventHandler(this.BtnSAP_Click);
            // 
            // Módosítás
            // 
            this.Módosítás.BackgroundImage = global::Villamos.Properties.Resources.Action_configure;
            this.Módosítás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Módosítás.Location = new System.Drawing.Point(353, 51);
            this.Módosítás.Name = "Módosítás";
            this.Módosítás.Size = new System.Drawing.Size(45, 45);
            this.Módosítás.TabIndex = 187;
            this.Módosítás.UseVisualStyleBackColor = true;
            this.Módosítás.Click += new System.EventHandler(this.Módosítás_Click);
            // 
            // Aktív
            // 
            this.Aktív.AutoSize = true;
            this.Aktív.Location = new System.Drawing.Point(12, 62);
            this.Aktív.Name = "Aktív";
            this.Aktív.Size = new System.Drawing.Size(146, 24);
            this.Aktív.TabIndex = 188;
            this.Aktív.Text = "Történeti elemek";
            this.Aktív.UseVisualStyleBackColor = true;
            // 
            // Ablak_Fődarab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Tan;
            this.ClientSize = new System.Drawing.Size(906, 252);
            this.Controls.Add(this.Aktív);
            this.Controls.Add(this.Módosítás);
            this.Controls.Add(this.BtnSAP);
            this.Controls.Add(this.Táblalista);
            this.Controls.Add(this.Frissíti_táblalistát);
            this.Controls.Add(this.Excel_gomb);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.BtnSúgó);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Fődarab";
            this.Text = "Fődarab";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_Fődarab_FormClosed);
            this.Load += new System.EventHandler(this.Ablak_Fődarab_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Táblalista)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ProgressBar Holtart;
        internal System.Windows.Forms.Button BtnSúgó;
        private Zuby.ADGV.AdvancedDataGridView Táblalista;
        internal System.Windows.Forms.Button Frissíti_táblalistát;
        internal System.Windows.Forms.Button Excel_gomb;
        internal System.Windows.Forms.Button BtnSAP;
        internal System.Windows.Forms.Button Módosítás;
        private System.Windows.Forms.CheckBox Aktív;
    }
}