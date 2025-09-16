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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Vételezés));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.CmbTelephely = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.TáblaFelső = new Zuby.ADGV.AdvancedDataGridView();
            this.Kereső = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Összesen = new System.Windows.Forms.Label();
            this.Előjeletvált = new System.Windows.Forms.Button();
            this.SorTörlés = new System.Windows.Forms.Button();
            this.FelsőÜrítés = new System.Windows.Forms.Button();
            this.MásikTáblázatba = new System.Windows.Forms.Button();
            this.Másol = new System.Windows.Forms.Button();
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.Excel_gomb = new System.Windows.Forms.Button();
            this.AnyagMódosítás = new System.Windows.Forms.Button();
            this.BtnSAP = new System.Windows.Forms.Button();
            this.Képnéző = new System.Windows.Forms.Button();
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TáblaFelső)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
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
            this.Tábla.Location = new System.Drawing.Point(5, 326);
            this.Tábla.MaxFilterButtonImageHeight = 23;
            this.Tábla.Name = "Tábla";
            this.Tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tábla.Size = new System.Drawing.Size(1132, 250);
            this.Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.TabIndex = 191;
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
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
            this.TáblaFelső.Size = new System.Drawing.Size(1132, 225);
            this.TáblaFelső.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.TáblaFelső.TabIndex = 192;
            this.TáblaFelső.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TáblaFelső_CellClick);
            this.TáblaFelső.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.TáblaFelső_CellValueChanged);
            // 
            // Kereső
            // 
            this.Kereső.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Kereső.Location = new System.Drawing.Point(5, 294);
            this.Kereső.Name = "Kereső";
            this.Kereső.Size = new System.Drawing.Size(926, 26);
            this.Kereső.TabIndex = 193;
            this.Kereső.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Kereső_KeyDown);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 13;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 183F));
            this.tableLayoutPanel1.Controls.Add(this.Előjeletvált, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.SorTörlés, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.FelsőÜrítés, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.MásikTáblázatba, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Másol, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.BtnSúgó, 12, 0);
            this.tableLayoutPanel1.Controls.Add(this.Excel_gomb, 11, 0);
            this.tableLayoutPanel1.Controls.Add(this.AnyagMódosítás, 9, 0);
            this.tableLayoutPanel1.Controls.Add(this.BtnSAP, 8, 0);
            this.tableLayoutPanel1.Controls.Add(this.Képnéző, 6, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(354, 7);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(783, 55);
            this.tableLayoutPanel1.TabIndex = 198;
            // 
            // Összesen
            // 
            this.Összesen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Összesen.AutoSize = true;
            this.Összesen.BackColor = System.Drawing.Color.LightSalmon;
            this.Összesen.Location = new System.Drawing.Point(937, 294);
            this.Összesen.Name = "Összesen";
            this.Összesen.Size = new System.Drawing.Size(120, 20);
            this.Összesen.TabIndex = 199;
            this.Összesen.Text = "Összeg: << - >>";
            // 
            // Előjeletvált
            // 
            this.Előjeletvált.BackgroundImage = global::Villamos.Properties.Resources.PlusMinus;
            this.Előjeletvált.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Előjeletvált.Location = new System.Drawing.Point(203, 3);
            this.Előjeletvált.Name = "Előjeletvált";
            this.Előjeletvált.Size = new System.Drawing.Size(44, 45);
            this.Előjeletvált.TabIndex = 200;
            this.toolTip1.SetToolTip(this.Előjeletvált, "A mennyiség adatokat megfordítja (+/-)");
            this.Előjeletvált.UseVisualStyleBackColor = true;
            this.Előjeletvált.Click += new System.EventHandler(this.Előjeletvált_Click);
            // 
            // SorTörlés
            // 
            this.SorTörlés.BackgroundImage = global::Villamos.Properties.Resources.Fatcow_Farm_Fresh_Table_row_delete_32;
            this.SorTörlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SorTörlés.Location = new System.Drawing.Point(153, 3);
            this.SorTörlés.Name = "SorTörlés";
            this.SorTörlés.Size = new System.Drawing.Size(44, 45);
            this.SorTörlés.TabIndex = 199;
            this.toolTip1.SetToolTip(this.SorTörlés, "A felső táblázat kijelöl elemeit törli");
            this.SorTörlés.UseVisualStyleBackColor = true;
            this.SorTörlés.Click += new System.EventHandler(this.SorTörlés_Click);
            // 
            // FelsőÜrítés
            // 
            this.FelsőÜrítés.BackgroundImage = global::Villamos.Properties.Resources.New_32_piros;
            this.FelsőÜrítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FelsőÜrítés.Location = new System.Drawing.Point(103, 3);
            this.FelsőÜrítés.Name = "FelsőÜrítés";
            this.FelsőÜrítés.Size = new System.Drawing.Size(44, 45);
            this.FelsőÜrítés.TabIndex = 198;
            this.toolTip1.SetToolTip(this.FelsőÜrítés, "A feslő táblázat tartalmát kiüríti");
            this.FelsőÜrítés.UseVisualStyleBackColor = true;
            this.FelsőÜrítés.Click += new System.EventHandler(this.FelsőÜrítés_Click);
            // 
            // MásikTáblázatba
            // 
            this.MásikTáblázatba.BackgroundImage = global::Villamos.Properties.Resources.Button_Upload_01;
            this.MásikTáblázatba.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MásikTáblázatba.Location = new System.Drawing.Point(3, 3);
            this.MásikTáblázatba.Name = "MásikTáblázatba";
            this.MásikTáblázatba.Size = new System.Drawing.Size(44, 45);
            this.MásikTáblázatba.TabIndex = 195;
            this.toolTip1.SetToolTip(this.MásikTáblázatba, "Alsó táblából a felső táblába\r\n átmásolja a tételt");
            this.MásikTáblázatba.UseVisualStyleBackColor = true;
            this.MásikTáblázatba.Click += new System.EventHandler(this.MásikTáblázatba_Click);
            // 
            // Másol
            // 
            this.Másol.BackgroundImage = global::Villamos.Properties.Resources.Document_Copy_01;
            this.Másol.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Másol.Location = new System.Drawing.Point(53, 3);
            this.Másol.Name = "Másol";
            this.Másol.Size = new System.Drawing.Size(44, 45);
            this.Másol.TabIndex = 196;
            this.toolTip1.SetToolTip(this.Másol, "Adatokat másolja a vágólapra");
            this.Másol.UseVisualStyleBackColor = true;
            this.Másol.Click += new System.EventHandler(this.Másol_Click);
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(735, 3);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 188;
            this.toolTip1.SetToolTip(this.BtnSúgó, "Súgó");
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // Excel_gomb
            // 
            this.Excel_gomb.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excel_gomb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excel_gomb.Location = new System.Drawing.Point(553, 2);
            this.Excel_gomb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Excel_gomb.Name = "Excel_gomb";
            this.Excel_gomb.Size = new System.Drawing.Size(43, 45);
            this.Excel_gomb.TabIndex = 197;
            this.toolTip1.SetToolTip(this.Excel_gomb, "Excel kimenetet készít a Felső \r\ntáblázat adatai alapján");
            this.Excel_gomb.UseVisualStyleBackColor = true;
            this.Excel_gomb.Click += new System.EventHandler(this.Excel_gomb_Click);
            // 
            // AnyagMódosítás
            // 
            this.AnyagMódosítás.BackgroundImage = global::Villamos.Properties.Resources.Document_preferences;
            this.AnyagMódosítás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AnyagMódosítás.Location = new System.Drawing.Point(453, 3);
            this.AnyagMódosítás.Name = "AnyagMódosítás";
            this.AnyagMódosítás.Size = new System.Drawing.Size(44, 45);
            this.AnyagMódosítás.TabIndex = 190;
            this.toolTip1.SetToolTip(this.AnyagMódosítás, "Anyag adatok módosítása");
            this.AnyagMódosítás.UseVisualStyleBackColor = true;
            this.AnyagMódosítás.Click += new System.EventHandler(this.AnyagMódosítás_Click);
            // 
            // BtnSAP
            // 
            this.BtnSAP.BackgroundImage = global::Villamos.Properties.Resources.SAP;
            this.BtnSAP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSAP.Location = new System.Drawing.Point(403, 3);
            this.BtnSAP.Name = "BtnSAP";
            this.BtnSAP.Size = new System.Drawing.Size(44, 45);
            this.BtnSAP.TabIndex = 187;
            this.toolTip1.SetToolTip(this.BtnSAP, "Raktárkészlet frissítés");
            this.BtnSAP.UseVisualStyleBackColor = true;
            this.BtnSAP.Click += new System.EventHandler(this.BtnSAP_Click);
            // 
            // Képnéző
            // 
            this.Képnéző.BackgroundImage = global::Villamos.Properties.Resources.App_photo;
            this.Képnéző.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Képnéző.Location = new System.Drawing.Point(303, 3);
            this.Képnéző.Name = "Képnéző";
            this.Képnéző.Size = new System.Drawing.Size(44, 45);
            this.Képnéző.TabIndex = 201;
            this.toolTip1.SetToolTip(this.Képnéző, "A mennyiség adatokat megfordítja (+/-)");
            this.Képnéző.UseVisualStyleBackColor = true;
            this.Képnéző.Click += new System.EventHandler(this.Képnéző_Click);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(14, 251);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(1109, 28);
            this.Holtart.TabIndex = 200;
            this.Holtart.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Ablak_Vételezés
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Coral;
            this.ClientSize = new System.Drawing.Size(1149, 584);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Összesen);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Kereső);
            this.Controls.Add(this.TáblaFelső);
            this.Controls.Add(this.Tábla);
            this.Controls.Add(this.Panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Vételezés";
            this.Text = "Vételezés segéd";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_Vételezés_FormClosed);
            this.Load += new System.EventHandler(this.Ablak_Vételezés_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TáblaFelső)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
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
        internal System.Windows.Forms.Button MásikTáblázatba;
        internal System.Windows.Forms.Button Másol;
        internal System.Windows.Forms.Button Excel_gomb;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label Összesen;
        internal System.Windows.Forms.Button FelsőÜrítés;
        internal System.Windows.Forms.Button SorTörlés;
        internal System.Windows.Forms.Button Előjeletvált;
        internal System.Windows.Forms.Button Képnéző;
        internal V_MindenEgyéb.MyProgressbar Holtart;
        private System.Windows.Forms.Timer timer1;
    }
}