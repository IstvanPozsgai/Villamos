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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Holtart = new System.Windows.Forms.ProgressBar();
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.Táblalista = new Zuby.ADGV.AdvancedDataGridView();
            this.Frissíti_táblalistát = new System.Windows.Forms.Button();
            this.Excel_gomb = new System.Windows.Forms.Button();
            this.BtnSAP = new System.Windows.Forms.Button();
            this.Módosítás = new System.Windows.Forms.Button();
            this.Aktív = new System.Windows.Forms.CheckBox();
            this.Összesítés = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ChkRendezés = new System.Windows.Forms.CheckBox();
            this.ChkSzűrés = new System.Windows.Forms.CheckBox();
            this.KötésiOsztály = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Táblalista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KötésiOsztály)).BeginInit();
            this.SuspendLayout();
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(12, 12);
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
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Táblalista.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
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
            this.Frissíti_táblalistát.Location = new System.Drawing.Point(309, 51);
            this.Frissíti_táblalistát.Name = "Frissíti_táblalistát";
            this.Frissíti_táblalistát.Size = new System.Drawing.Size(45, 45);
            this.Frissíti_táblalistát.TabIndex = 184;
            this.toolTip1.SetToolTip(this.Frissíti_táblalistát, "Frissíti a táblázat elemeit");
            this.Frissíti_táblalistát.UseVisualStyleBackColor = true;
            this.Frissíti_táblalistát.Click += new System.EventHandler(this.Frissíti_táblalistát_Click);
            // 
            // Excel_gomb
            // 
            this.Excel_gomb.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excel_gomb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excel_gomb.Location = new System.Drawing.Point(360, 51);
            this.Excel_gomb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Excel_gomb.Name = "Excel_gomb";
            this.Excel_gomb.Size = new System.Drawing.Size(45, 45);
            this.Excel_gomb.TabIndex = 183;
            this.toolTip1.SetToolTip(this.Excel_gomb, "Excel kimenetet készít a táblázat adatai alapján");
            this.Excel_gomb.UseVisualStyleBackColor = true;
            this.Excel_gomb.Click += new System.EventHandler(this.Excel_gomb_Click);
            // 
            // BtnSAP
            // 
            this.BtnSAP.BackgroundImage = global::Villamos.Properties.Resources.SAP;
            this.BtnSAP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSAP.Location = new System.Drawing.Point(643, 51);
            this.BtnSAP.Name = "BtnSAP";
            this.BtnSAP.Size = new System.Drawing.Size(45, 45);
            this.BtnSAP.TabIndex = 186;
            this.toolTip1.SetToolTip(this.BtnSAP, "Sap adatokkal frissíti a táblázatot");
            this.BtnSAP.UseVisualStyleBackColor = true;
            this.BtnSAP.Click += new System.EventHandler(this.BtnSAP_Click);
            // 
            // Módosítás
            // 
            this.Módosítás.BackgroundImage = global::Villamos.Properties.Resources.Action_configure;
            this.Módosítás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Módosítás.Location = new System.Drawing.Point(694, 51);
            this.Módosítás.Name = "Módosítás";
            this.Módosítás.Size = new System.Drawing.Size(45, 45);
            this.Módosítás.TabIndex = 187;
            this.toolTip1.SetToolTip(this.Módosítás, "A kijelölt elem adatainak módosítása");
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
            // Összesítés
            // 
            this.Összesítés.BackgroundImage = global::Villamos.Properties.Resources.justice_297629_1280;
            this.Összesítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Összesítés.Location = new System.Drawing.Point(592, 51);
            this.Összesítés.Name = "Összesítés";
            this.Összesítés.Size = new System.Drawing.Size(45, 45);
            this.Összesítés.TabIndex = 189;
            this.toolTip1.SetToolTip(this.Összesítés, "Összesítési ablak");
            this.Összesítés.UseVisualStyleBackColor = true;
            this.Összesítés.Click += new System.EventHandler(this.Összesítés_Click);
            // 
            // ChkRendezés
            // 
            this.ChkRendezés.AutoSize = true;
            this.ChkRendezés.Location = new System.Drawing.Point(164, 42);
            this.ChkRendezés.Name = "ChkRendezés";
            this.ChkRendezés.Size = new System.Drawing.Size(145, 24);
            this.ChkRendezés.TabIndex = 192;
            this.ChkRendezés.Text = "Sorba rendezés ";
            this.ChkRendezés.UseVisualStyleBackColor = true;
            // 
            // ChkSzűrés
            // 
            this.ChkSzűrés.AutoSize = true;
            this.ChkSzűrés.Location = new System.Drawing.Point(164, 72);
            this.ChkSzűrés.Name = "ChkSzűrés";
            this.ChkSzűrés.Size = new System.Drawing.Size(78, 24);
            this.ChkSzűrés.TabIndex = 191;
            this.ChkSzűrés.Text = "Szűrés";
            this.ChkSzűrés.UseVisualStyleBackColor = true;
            // 
            // Ablak_Fődarab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Tan;
            this.ClientSize = new System.Drawing.Size(906, 252);
            this.Controls.Add(this.ChkRendezés);
            this.Controls.Add(this.ChkSzűrés);
            this.Controls.Add(this.Összesítés);
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
            ((System.ComponentModel.ISupportInitialize)(this.KötésiOsztály)).EndInit();
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
        internal System.Windows.Forms.Button Összesítés;
        private System.Windows.Forms.ToolTip toolTip1;
        internal System.Windows.Forms.CheckBox ChkRendezés;
        internal System.Windows.Forms.CheckBox ChkSzűrés;
        internal System.Windows.Forms.BindingSource KötésiOsztály;
    }
}