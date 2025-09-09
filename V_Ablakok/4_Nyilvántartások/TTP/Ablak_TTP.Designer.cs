namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.TTP
{
    partial class Ablak_TTP
    {

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
        internal void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.DtGvw_Naptár = new System.Windows.Forms.DataGridView();
            this.Dátum = new System.Windows.Forms.DateTimePicker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ChkKötelezett = new System.Windows.Forms.CheckBox();
            this.BtnJavítva = new System.Windows.Forms.Button();
            this.BtnKészJav = new System.Windows.Forms.Button();
            this.BtnTTPKész = new System.Windows.Forms.Button();
            this.BtnKuka = new System.Windows.Forms.Button();
            this.BtnTörténet = new System.Windows.Forms.Button();
            this.Btn_Ütemez = new System.Windows.Forms.Button();
            this.BtnNaptár = new System.Windows.Forms.Button();
            this.Btn_TTP_Év = new System.Windows.Forms.Button();
            this.BtnAlapadat = new System.Windows.Forms.Button();
            this.Frissítés_gomb = new System.Windows.Forms.Button();
            this.BtnExcel = new System.Windows.Forms.Button();
            this.Tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.Súgó_gomb = new System.Windows.Forms.Button();
            this.ChkSzűrés = new System.Windows.Forms.CheckBox();
            this.ChkRendezés = new System.Windows.Forms.CheckBox();
            this.KötésiOsztály = new System.Windows.Forms.BindingSource(this.components);
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DtGvw_Naptár)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KötésiOsztály)).BeginInit();
            this.SuspendLayout();
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(175, 2);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
            this.Cmbtelephely.SelectionChangeCommitted += new System.EventHandler(this.Cmbtelephely_SelectionChangeCommitted);
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(12, 5);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(145, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.Cmbtelephely);
            this.Panel1.Controls.Add(this.Label13);
            this.Panel1.Location = new System.Drawing.Point(11, 7);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(373, 33);
            this.Panel1.TabIndex = 67;
            // 
            // DtGvw_Naptár
            // 
            this.DtGvw_Naptár.AllowUserToAddRows = false;
            this.DtGvw_Naptár.AllowUserToDeleteRows = false;
            this.DtGvw_Naptár.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DtGvw_Naptár.Location = new System.Drawing.Point(14, 116);
            this.DtGvw_Naptár.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.DtGvw_Naptár.Name = "DtGvw_Naptár";
            this.DtGvw_Naptár.ReadOnly = true;
            this.DtGvw_Naptár.RowHeadersVisible = false;
            this.DtGvw_Naptár.RowHeadersWidth = 62;
            this.DtGvw_Naptár.RowTemplate.Height = 28;
            this.DtGvw_Naptár.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DtGvw_Naptár.Size = new System.Drawing.Size(1189, 84);
            this.DtGvw_Naptár.TabIndex = 74;
            // 
            // Dátum
            // 
            this.Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum.Location = new System.Drawing.Point(14, 83);
            this.Dátum.Name = "Dátum";
            this.Dátum.Size = new System.Drawing.Size(131, 26);
            this.Dátum.TabIndex = 75;
            // 
            // ChkKötelezett
            // 
            this.ChkKötelezett.AutoSize = true;
            this.ChkKötelezett.Checked = true;
            this.ChkKötelezett.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkKötelezett.Location = new System.Drawing.Point(151, 83);
            this.ChkKötelezett.Name = "ChkKötelezett";
            this.ChkKötelezett.Size = new System.Drawing.Size(56, 24);
            this.ChkKötelezett.TabIndex = 185;
            this.ChkKötelezett.Text = "TTP";
            this.toolTip1.SetToolTip(this.ChkKötelezett, "Csak a TTP-re kötelezett járműveket listázza");
            this.ChkKötelezett.UseVisualStyleBackColor = true;
            // 
            // BtnJavítva
            // 
            this.BtnJavítva.BackgroundImage = global::Villamos.Properties.Resources.App_network_connection_manager;
            this.BtnJavítva.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnJavítva.Location = new System.Drawing.Point(116, 208);
            this.BtnJavítva.Name = "BtnJavítva";
            this.BtnJavítva.Size = new System.Drawing.Size(45, 45);
            this.BtnJavítva.TabIndex = 188;
            this.toolTip1.SetToolTip(this.BtnJavítva, "Javítás elkészült és lezárható");
            this.BtnJavítva.UseVisualStyleBackColor = true;
            this.BtnJavítva.Click += new System.EventHandler(this.BtnJavítva_Click);
            // 
            // BtnKészJav
            // 
            this.BtnKészJav.BackgroundImage = global::Villamos.Properties.Resources.Action_configure;
            this.BtnKészJav.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnKészJav.Location = new System.Drawing.Point(65, 208);
            this.BtnKészJav.Name = "BtnKészJav";
            this.BtnKészJav.Size = new System.Drawing.Size(45, 45);
            this.BtnKészJav.TabIndex = 187;
            this.toolTip1.SetToolTip(this.BtnKészJav, "TTP vizsgálat elkészült, de javítani szükséges.");
            this.BtnKészJav.UseVisualStyleBackColor = true;
            this.BtnKészJav.Click += new System.EventHandler(this.BtnKészJav_Click);
            // 
            // BtnTTPKész
            // 
            this.BtnTTPKész.BackgroundImage = global::Villamos.Properties.Resources.Document_write;
            this.BtnTTPKész.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnTTPKész.Location = new System.Drawing.Point(14, 208);
            this.BtnTTPKész.Name = "BtnTTPKész";
            this.BtnTTPKész.Size = new System.Drawing.Size(45, 45);
            this.BtnTTPKész.TabIndex = 186;
            this.toolTip1.SetToolTip(this.BtnTTPKész, "TTP vizsgálat elkészült és nem kell javítani");
            this.BtnTTPKész.UseVisualStyleBackColor = true;
            this.BtnTTPKész.Click += new System.EventHandler(this.BtnTTPKész_Click);
            // 
            // BtnKuka
            // 
            this.BtnKuka.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.BtnKuka.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnKuka.Location = new System.Drawing.Point(488, 62);
            this.BtnKuka.Name = "BtnKuka";
            this.BtnKuka.Size = new System.Drawing.Size(45, 45);
            this.BtnKuka.TabIndex = 184;
            this.toolTip1.SetToolTip(this.BtnKuka, "A kiválasztott napra ütemezett kocsikat törli.\r\n\r\n ");
            this.BtnKuka.UseVisualStyleBackColor = true;
            this.BtnKuka.Click += new System.EventHandler(this.BtnKuka_Click);
            // 
            // BtnTörténet
            // 
            this.BtnTörténet.BackgroundImage = global::Villamos.Properties.Resources.App_dict;
            this.BtnTörténet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnTörténet.Location = new System.Drawing.Point(1041, 62);
            this.BtnTörténet.Name = "BtnTörténet";
            this.BtnTörténet.Size = new System.Drawing.Size(45, 45);
            this.BtnTörténet.TabIndex = 77;
            this.toolTip1.SetToolTip(this.BtnTörténet, "Jármű történeti adatok megjelenítése");
            this.BtnTörténet.UseVisualStyleBackColor = true;
            this.BtnTörténet.Click += new System.EventHandler(this.BtnTörténet_Click);
            // 
            // Btn_Ütemez
            // 
            this.Btn_Ütemez.BackgroundImage = global::Villamos.Properties.Resources.leadott;
            this.Btn_Ütemez.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Btn_Ütemez.Location = new System.Drawing.Point(437, 62);
            this.Btn_Ütemez.Name = "Btn_Ütemez";
            this.Btn_Ütemez.Size = new System.Drawing.Size(45, 45);
            this.Btn_Ütemez.TabIndex = 76;
            this.toolTip1.SetToolTip(this.Btn_Ütemez, "A kiválasztott napra ütemezi az alsó táblázat kiválasztott sora(i)ban lévő kocsik" +
        "at.\r\n ");
            this.Btn_Ütemez.UseVisualStyleBackColor = true;
            this.Btn_Ütemez.Click += new System.EventHandler(this.Btn_Ütemez_Click);
            // 
            // BtnNaptár
            // 
            this.BtnNaptár.BackgroundImage = global::Villamos.Properties.Resources.Calendar;
            this.BtnNaptár.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnNaptár.Location = new System.Drawing.Point(939, 62);
            this.BtnNaptár.Name = "BtnNaptár";
            this.BtnNaptár.Size = new System.Drawing.Size(45, 45);
            this.BtnNaptár.TabIndex = 73;
            this.toolTip1.SetToolTip(this.BtnNaptár, "Naptár adatok beállítása");
            this.BtnNaptár.UseVisualStyleBackColor = true;
            this.BtnNaptár.Click += new System.EventHandler(this.BtnNaptár_Click);
            // 
            // Btn_TTP_Év
            // 
            this.Btn_TTP_Év.BackgroundImage = global::Villamos.Properties.Resources.CALENDR1;
            this.Btn_TTP_Év.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Btn_TTP_Év.Location = new System.Drawing.Point(888, 62);
            this.Btn_TTP_Év.Name = "Btn_TTP_Év";
            this.Btn_TTP_Év.Size = new System.Drawing.Size(45, 45);
            this.Btn_TTP_Év.TabIndex = 72;
            this.toolTip1.SetToolTip(this.Btn_TTP_Év, "Beállítja, hogy mennyi évente kell elvégezni a TTP-t");
            this.Btn_TTP_Év.UseVisualStyleBackColor = true;
            this.Btn_TTP_Év.Click += new System.EventHandler(this.Btn_TTP_Év_Click);
            // 
            // BtnAlapadat
            // 
            this.BtnAlapadat.BackgroundImage = global::Villamos.Properties.Resources.App_ark;
            this.BtnAlapadat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnAlapadat.Location = new System.Drawing.Point(990, 62);
            this.BtnAlapadat.Name = "BtnAlapadat";
            this.BtnAlapadat.Size = new System.Drawing.Size(45, 45);
            this.BtnAlapadat.TabIndex = 70;
            this.toolTip1.SetToolTip(this.BtnAlapadat, "Jármű alapadatok beállítása");
            this.BtnAlapadat.UseVisualStyleBackColor = true;
            this.BtnAlapadat.Click += new System.EventHandler(this.BtnAlapadat_Click);
            // 
            // Frissítés_gomb
            // 
            this.Frissítés_gomb.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Frissítés_gomb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Frissítés_gomb.Location = new System.Drawing.Point(366, 62);
            this.Frissítés_gomb.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Frissítés_gomb.Name = "Frissítés_gomb";
            this.Frissítés_gomb.Size = new System.Drawing.Size(45, 45);
            this.Frissítés_gomb.TabIndex = 1;
            this.toolTip1.SetToolTip(this.Frissítés_gomb, "Frissíti a táblázat adatait");
            this.Frissítés_gomb.UseVisualStyleBackColor = true;
            this.Frissítés_gomb.Click += new System.EventHandler(this.Frissítés_gomb_Click);
            // 
            // BtnExcel
            // 
            this.BtnExcel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.BtnExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnExcel.Location = new System.Drawing.Point(888, 208);
            this.BtnExcel.Name = "BtnExcel";
            this.BtnExcel.Size = new System.Drawing.Size(45, 45);
            this.BtnExcel.TabIndex = 191;
            this.toolTip1.SetToolTip(this.BtnExcel, "Táblázat adatait Excel fájlba menti");
            this.BtnExcel.UseVisualStyleBackColor = true;
            this.BtnExcel.Click += new System.EventHandler(this.BtnExcel_Click);
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
            this.Tábla.Location = new System.Drawing.Point(14, 259);
            this.Tábla.MaxFilterButtonImageHeight = 23;
            this.Tábla.Name = "Tábla";
            this.Tábla.ReadOnly = true;
            this.Tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tábla.RowHeadersWidth = 30;
            this.Tábla.Size = new System.Drawing.Size(1189, 224);
            this.Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.TabIndex = 183;
            this.Tábla.SortStringChanged += new System.EventHandler<Zuby.ADGV.AdvancedDataGridView.SortEventArgs>(this.Tábla_SortStringChanged);
            this.Tábla.FilterStringChanged += new System.EventHandler<Zuby.ADGV.AdvancedDataGridView.FilterEventArgs>(this.Tábla_FilterStringChanged);
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            // 
            // Súgó_gomb
            // 
            this.Súgó_gomb.Image = global::Villamos.Properties.Resources.Help_Support;
            this.Súgó_gomb.Location = new System.Drawing.Point(1158, 7);
            this.Súgó_gomb.Name = "Súgó_gomb";
            this.Súgó_gomb.Size = new System.Drawing.Size(45, 45);
            this.Súgó_gomb.TabIndex = 68;
            this.Súgó_gomb.UseVisualStyleBackColor = true;
            this.Súgó_gomb.Click += new System.EventHandler(this.Súgó_gomb_Click);
            // 
            // ChkSzűrés
            // 
            this.ChkSzűrés.AutoSize = true;
            this.ChkSzűrés.Location = new System.Drawing.Point(213, 87);
            this.ChkSzűrés.Name = "ChkSzűrés";
            this.ChkSzűrés.Size = new System.Drawing.Size(78, 24);
            this.ChkSzűrés.TabIndex = 189;
            this.ChkSzűrés.Text = "Szűrés";
            this.ChkSzűrés.UseVisualStyleBackColor = true;
            // 
            // ChkRendezés
            // 
            this.ChkRendezés.AutoSize = true;
            this.ChkRendezés.Location = new System.Drawing.Point(213, 57);
            this.ChkRendezés.Name = "ChkRendezés";
            this.ChkRendezés.Size = new System.Drawing.Size(145, 24);
            this.ChkRendezés.TabIndex = 190;
            this.ChkRendezés.Text = "Sorba rendezés ";
            this.ChkRendezés.UseVisualStyleBackColor = true;
            // 
            // KötésiOsztály
            // 
            this.KötésiOsztály.ListChanged += new System.ComponentModel.ListChangedEventHandler(this.KötésiOsztály_ListChanged);
            // 
            // Ablak_TTP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1212, 495);
            this.Controls.Add(this.BtnExcel);
            this.Controls.Add(this.ChkRendezés);
            this.Controls.Add(this.ChkSzűrés);
            this.Controls.Add(this.BtnJavítva);
            this.Controls.Add(this.BtnKészJav);
            this.Controls.Add(this.BtnTTPKész);
            this.Controls.Add(this.ChkKötelezett);
            this.Controls.Add(this.BtnKuka);
            this.Controls.Add(this.Tábla);
            this.Controls.Add(this.BtnTörténet);
            this.Controls.Add(this.Btn_Ütemez);
            this.Controls.Add(this.Dátum);
            this.Controls.Add(this.DtGvw_Naptár);
            this.Controls.Add(this.BtnNaptár);
            this.Controls.Add(this.Btn_TTP_Év);
            this.Controls.Add(this.BtnAlapadat);
            this.Controls.Add(this.Súgó_gomb);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Frissítés_gomb);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "Ablak_TTP";
            this.Text = "TTP Nyilvántartás";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_TTP_FormClosed);
            this.Load += new System.EventHandler(this.Ablak_TTP_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DtGvw_Naptár)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KötésiOsztály)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.Button Frissítés_gomb;
        internal System.Windows.Forms.ComboBox Cmbtelephely;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.Button Súgó_gomb;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Button BtnAlapadat;
        internal System.Windows.Forms.Button Btn_TTP_Év;
        internal System.Windows.Forms.Button BtnNaptár;
        internal System.Windows.Forms.DataGridView DtGvw_Naptár;
        internal System.Windows.Forms.DateTimePicker Dátum;
        internal System.Windows.Forms.Button Btn_Ütemez;
        internal System.Windows.Forms.ToolTip toolTip1;
        internal System.Windows.Forms.Button BtnTörténet;
        internal Zuby.ADGV.AdvancedDataGridView Tábla;
        internal System.Windows.Forms.Button BtnKuka;
        internal System.Windows.Forms.CheckBox ChkKötelezett;
        internal System.Windows.Forms.Button BtnTTPKész;
        internal System.Windows.Forms.Button BtnKészJav;
        internal System.Windows.Forms.Button BtnJavítva;
        internal System.Windows.Forms.CheckBox ChkSzűrés;
        internal System.Windows.Forms.CheckBox ChkRendezés;
        internal System.Windows.Forms.BindingSource KötésiOsztály;
        internal System.Windows.Forms.Button BtnExcel;
        private System.ComponentModel.IContainer components;
    }
}