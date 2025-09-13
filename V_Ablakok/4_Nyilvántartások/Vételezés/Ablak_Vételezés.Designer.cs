namespace Villamos.V_Ablakok._4_Nyilvántartások.Vételezés
{
    partial class Ablak_Vételezés
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
            this.BtnSAP = new System.Windows.Forms.Button();
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.CmbTelephely = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.AnyagMódosítás = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Frissíti_táblalistát = new System.Windows.Forms.Button();
            this.MásikTáblázatba = new System.Windows.Forms.Button();
            this.Tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.TáblaFelső = new Zuby.ADGV.AdvancedDataGridView();
            this.Kereső = new System.Windows.Forms.TextBox();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TáblaFelső)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnSAP
            // 
            this.BtnSAP.BackgroundImage = global::Villamos.Properties.Resources.SAP;
            this.BtnSAP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSAP.Location = new System.Drawing.Point(525, 12);
            this.BtnSAP.Name = "BtnSAP";
            this.BtnSAP.Size = new System.Drawing.Size(45, 45);
            this.BtnSAP.TabIndex = 187;
            this.toolTip1.SetToolTip(this.BtnSAP, "Raktárkészlet frissítés");
            this.BtnSAP.UseVisualStyleBackColor = true;
            this.BtnSAP.Click += new System.EventHandler(this.BtnSAP_Click);
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(856, 12);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 188;
            this.toolTip1.SetToolTip(this.BtnSúgó, "Súgó");
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.CmbTelephely);
            this.Panel1.Controls.Add(this.label23);
            this.Panel1.Location = new System.Drawing.Point(5, 7);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(343, 35);
            this.Panel1.TabIndex = 189;
            // 
            // CmbTelephely
            // 
            this.CmbTelephely.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbTelephely.FormattingEnabled = true;
            this.CmbTelephely.Location = new System.Drawing.Point(150, 4);
            this.CmbTelephely.Name = "CmbTelephely";
            this.CmbTelephely.Size = new System.Drawing.Size(186, 28);
            this.CmbTelephely.TabIndex = 18;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(5, 5);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(145, 20);
            this.label23.TabIndex = 17;
            this.label23.Text = "Telephelyi beállítás:";
            // 
            // AnyagMódosítás
            // 
            this.AnyagMódosítás.BackgroundImage = global::Villamos.Properties.Resources.Document_preferences;
            this.AnyagMódosítás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AnyagMódosítás.Location = new System.Drawing.Point(590, 12);
            this.AnyagMódosítás.Name = "AnyagMódosítás";
            this.AnyagMódosítás.Size = new System.Drawing.Size(45, 45);
            this.AnyagMódosítás.TabIndex = 190;
            this.toolTip1.SetToolTip(this.AnyagMódosítás, "Anyag adatok módosítása");
            this.AnyagMódosítás.UseVisualStyleBackColor = true;
            this.AnyagMódosítás.Click += new System.EventHandler(this.AnyagMódosítás_Click);
            // 
            // Frissíti_táblalistát
            // 
            this.Frissíti_táblalistát.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Frissíti_táblalistát.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Frissíti_táblalistát.Location = new System.Drawing.Point(421, 13);
            this.Frissíti_táblalistát.Name = "Frissíti_táblalistát";
            this.Frissíti_táblalistát.Size = new System.Drawing.Size(44, 44);
            this.Frissíti_táblalistát.TabIndex = 194;
            this.toolTip1.SetToolTip(this.Frissíti_táblalistát, "Frissíti a táblázat adatait");
            this.Frissíti_táblalistát.UseVisualStyleBackColor = true;
            this.Frissíti_táblalistát.Click += new System.EventHandler(this.Frissíti_táblalistát_Click);
            // 
            // MásikTáblázatba
            // 
            this.MásikTáblázatba.BackgroundImage = global::Villamos.Properties.Resources.Button_Upload_01;
            this.MásikTáblázatba.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MásikTáblázatba.Location = new System.Drawing.Point(371, 13);
            this.MásikTáblázatba.Name = "MásikTáblázatba";
            this.MásikTáblázatba.Size = new System.Drawing.Size(44, 44);
            this.MásikTáblázatba.TabIndex = 195;
            this.toolTip1.SetToolTip(this.MásikTáblázatba, "Frissíti a táblázat adatait");
            this.MásikTáblázatba.UseVisualStyleBackColor = true;
            this.MásikTáblázatba.Click += new System.EventHandler(this.MásikTáblázatba_Click);
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.FilterAndSortEnabled = true;
            this.Tábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.Location = new System.Drawing.Point(5, 318);
            this.Tábla.MaxFilterButtonImageHeight = 23;
            this.Tábla.Name = "Tábla";
            this.Tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tábla.Size = new System.Drawing.Size(896, 258);
            this.Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.TabIndex = 191;
            // 
            // TáblaFelső
            // 
            this.TáblaFelső.AllowUserToAddRows = false;
            this.TáblaFelső.AllowUserToDeleteRows = false;
            this.TáblaFelső.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TáblaFelső.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TáblaFelső.FilterAndSortEnabled = true;
            this.TáblaFelső.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.TáblaFelső.Location = new System.Drawing.Point(5, 63);
            this.TáblaFelső.MaxFilterButtonImageHeight = 23;
            this.TáblaFelső.Name = "TáblaFelső";
            this.TáblaFelső.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TáblaFelső.Size = new System.Drawing.Size(896, 231);
            this.TáblaFelső.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.TáblaFelső.TabIndex = 192;
            this.TáblaFelső.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.TáblaFelső_CellValueChanged);
            // 
            // Kereső
            // 
            this.Kereső.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Kereső.Location = new System.Drawing.Point(5, 294);
            this.Kereső.Name = "Kereső";
            this.Kereső.Size = new System.Drawing.Size(896, 26);
            this.Kereső.TabIndex = 193;
            this.Kereső.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Kereső_KeyDown);
            // 
            // Ablak_Vételezés
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 584);
            this.Controls.Add(this.MásikTáblázatba);
            this.Controls.Add(this.Frissíti_táblalistát);
            this.Controls.Add(this.Kereső);
            this.Controls.Add(this.TáblaFelső);
            this.Controls.Add(this.Tábla);
            this.Controls.Add(this.AnyagMódosítás);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.BtnSAP);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Vételezés";
            this.Text = "Ablak_Vételezés";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_Vételezés_FormClosed);
            this.Load += new System.EventHandler(this.Ablak_Vételezés_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TáblaFelső)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button BtnSAP;
        internal System.Windows.Forms.Button BtnSúgó;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.ComboBox CmbTelephely;
        internal System.Windows.Forms.Label label23;
        internal System.Windows.Forms.Button AnyagMódosítás;
        private System.Windows.Forms.ToolTip toolTip1;
        private Zuby.ADGV.AdvancedDataGridView Tábla;
        private Zuby.ADGV.AdvancedDataGridView TáblaFelső;
        private System.Windows.Forms.TextBox Kereső;
        internal System.Windows.Forms.Button Frissíti_táblalistát;
        internal System.Windows.Forms.Button MásikTáblázatba;
    }
}