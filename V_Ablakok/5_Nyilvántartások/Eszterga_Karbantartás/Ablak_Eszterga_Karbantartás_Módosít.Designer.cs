namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.Kerékeszterga
{
    partial class Ablak_Eszterga_Karbantartás_Módosít
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Eszterga_Karbantartás_Módosít));
            this.TxtBxId = new System.Windows.Forms.TextBox();
            this.TxtBxMuvelet = new System.Windows.Forms.TextBox();
            this.TxtBxMennyiNap = new System.Windows.Forms.TextBox();
            this.TxtBxMennyiOra = new System.Windows.Forms.TextBox();
            this.TxtBxUtolsoUzemoraAllas = new System.Windows.Forms.TextBox();
            this.LblSorsz = new System.Windows.Forms.Label();
            this.LblMuvelet = new System.Windows.Forms.Label();
            this.LblEgyseg = new System.Windows.Forms.Label();
            this.LblNap = new System.Windows.Forms.Label();
            this.LblOra = new System.Windows.Forms.Label();
            this.LblStat = new System.Windows.Forms.Label();
            this.LblUtolsoDat = new System.Windows.Forms.Label();
            this.LblUtolsoUzemA = new System.Windows.Forms.Label();
            this.CmbxEgyseg = new System.Windows.Forms.ComboBox();
            this.DtmPckrUtolsoDatum = new System.Windows.Forms.DateTimePicker();
            this.ChckBxStatus = new System.Windows.Forms.CheckBox();
            this.TablaMuvelet = new Zuby.ADGV.AdvancedDataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Btn_Modosit = new System.Windows.Forms.Button();
            this.Btn_Excel = new System.Windows.Forms.Button();
            this.Btn_UjFelvetel = new System.Windows.Forms.Button();
            this.Btn_Csere = new System.Windows.Forms.Button();
            this.Btn_Sorrend = new System.Windows.Forms.Button();
            this.Btn_Torles = new System.Windows.Forms.Button();
            this.Btn_Uzemora_Oldal = new System.Windows.Forms.Button();
            this.Btn_Pdf = new System.Windows.Forms.Button();
            this.Btn_Naplo_Oldal = new System.Windows.Forms.Button();
            this.GrpBxMuveletek = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.TablaMuvelet)).BeginInit();
            this.GrpBxMuveletek.SuspendLayout();
            this.SuspendLayout();
            // 
            // TxtBxId
            // 
            this.TxtBxId.Location = new System.Drawing.Point(105, 276);
            this.TxtBxId.Name = "TxtBxId";
            this.TxtBxId.Size = new System.Drawing.Size(92, 26);
            this.TxtBxId.TabIndex = 6;
            // 
            // TxtBxMuvelet
            // 
            this.TxtBxMuvelet.Location = new System.Drawing.Point(277, 273);
            this.TxtBxMuvelet.Multiline = true;
            this.TxtBxMuvelet.Name = "TxtBxMuvelet";
            this.TxtBxMuvelet.Size = new System.Drawing.Size(589, 110);
            this.TxtBxMuvelet.TabIndex = 7;
            // 
            // TxtBxMennyiNap
            // 
            this.TxtBxMennyiNap.Location = new System.Drawing.Point(105, 314);
            this.TxtBxMennyiNap.Name = "TxtBxMennyiNap";
            this.TxtBxMennyiNap.Size = new System.Drawing.Size(92, 26);
            this.TxtBxMennyiNap.TabIndex = 9;
            // 
            // TxtBxMennyiOra
            // 
            this.TxtBxMennyiOra.Location = new System.Drawing.Point(105, 354);
            this.TxtBxMennyiOra.Name = "TxtBxMennyiOra";
            this.TxtBxMennyiOra.Size = new System.Drawing.Size(92, 26);
            this.TxtBxMennyiOra.TabIndex = 10;
            // 
            // TxtBxUtolsoUzemoraAllas
            // 
            this.TxtBxUtolsoUzemoraAllas.Location = new System.Drawing.Point(874, 296);
            this.TxtBxUtolsoUzemoraAllas.Name = "TxtBxUtolsoUzemoraAllas";
            this.TxtBxUtolsoUzemoraAllas.Size = new System.Drawing.Size(162, 26);
            this.TxtBxUtolsoUzemoraAllas.TabIndex = 13;
            this.TxtBxUtolsoUzemoraAllas.TextChanged += new System.EventHandler(this.TxtBxUtolsoUzemoraAllas_TextChanged);
            // 
            // LblSorsz
            // 
            this.LblSorsz.AutoSize = true;
            this.LblSorsz.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.LblSorsz.Location = new System.Drawing.Point(6, 276);
            this.LblSorsz.Name = "LblSorsz";
            this.LblSorsz.Size = new System.Drawing.Size(76, 20);
            this.LblSorsz.TabIndex = 14;
            this.LblSorsz.Text = "Sorszám:";
            // 
            // LblMuvelet
            // 
            this.LblMuvelet.AutoSize = true;
            this.LblMuvelet.Location = new System.Drawing.Point(203, 276);
            this.LblMuvelet.Name = "LblMuvelet";
            this.LblMuvelet.Size = new System.Drawing.Size(68, 20);
            this.LblMuvelet.TabIndex = 15;
            this.LblMuvelet.Text = "Művelet:";
            // 
            // LblEgyseg
            // 
            this.LblEgyseg.AutoSize = true;
            this.LblEgyseg.Location = new System.Drawing.Point(1026, 328);
            this.LblEgyseg.Name = "LblEgyseg";
            this.LblEgyseg.Size = new System.Drawing.Size(66, 20);
            this.LblEgyseg.TabIndex = 16;
            this.LblEgyseg.Text = "Egység:";
            // 
            // LblNap
            // 
            this.LblNap.AutoSize = true;
            this.LblNap.Location = new System.Drawing.Point(6, 314);
            this.LblNap.Name = "LblNap";
            this.LblNap.Size = new System.Drawing.Size(96, 20);
            this.LblNap.TabIndex = 17;
            this.LblNap.Text = "Mennyi Nap:";
            // 
            // LblOra
            // 
            this.LblOra.AutoSize = true;
            this.LblOra.Location = new System.Drawing.Point(6, 354);
            this.LblOra.Name = "LblOra";
            this.LblOra.Size = new System.Drawing.Size(93, 20);
            this.LblOra.TabIndex = 18;
            this.LblOra.Text = "Mennyi Óra:";
            // 
            // LblStat
            // 
            this.LblStat.AutoSize = true;
            this.LblStat.Location = new System.Drawing.Point(1060, 273);
            this.LblStat.Name = "LblStat";
            this.LblStat.Size = new System.Drawing.Size(68, 20);
            this.LblStat.TabIndex = 19;
            this.LblStat.Text = "Státusz:";
            // 
            // LblUtolsoDat
            // 
            this.LblUtolsoDat.AutoSize = true;
            this.LblUtolsoDat.Location = new System.Drawing.Point(870, 325);
            this.LblUtolsoDat.Name = "LblUtolsoDat";
            this.LblUtolsoDat.Size = new System.Drawing.Size(111, 20);
            this.LblUtolsoDat.TabIndex = 20;
            this.LblUtolsoDat.Text = "Utolsó Dátum:";
            // 
            // LblUtolsoUzemA
            // 
            this.LblUtolsoUzemA.AutoSize = true;
            this.LblUtolsoUzemA.Location = new System.Drawing.Point(870, 273);
            this.LblUtolsoUzemA.Name = "LblUtolsoUzemA";
            this.LblUtolsoUzemA.Size = new System.Drawing.Size(166, 20);
            this.LblUtolsoUzemA.TabIndex = 21;
            this.LblUtolsoUzemA.Text = "Utolsó Üzemóra Állás:";
            // 
            // CmbxEgyseg
            // 
            this.CmbxEgyseg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbxEgyseg.FormattingEnabled = true;
            this.CmbxEgyseg.Location = new System.Drawing.Point(1030, 351);
            this.CmbxEgyseg.Name = "CmbxEgyseg";
            this.CmbxEgyseg.Size = new System.Drawing.Size(130, 28);
            this.CmbxEgyseg.TabIndex = 23;
            this.CmbxEgyseg.SelectedIndexChanged += new System.EventHandler(this.CmbxEgyseg_SelectedIndexChanged);
            // 
            // DtmPckrUtolsoDatum
            // 
            this.DtmPckrUtolsoDatum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtmPckrUtolsoDatum.Location = new System.Drawing.Point(874, 348);
            this.DtmPckrUtolsoDatum.Name = "DtmPckrUtolsoDatum";
            this.DtmPckrUtolsoDatum.Size = new System.Drawing.Size(107, 26);
            this.DtmPckrUtolsoDatum.TabIndex = 25;
            this.DtmPckrUtolsoDatum.ValueChanged += new System.EventHandler(this.DtmPckrUtolsoDatum_ValueChanged);
            // 
            // ChckBxStatus
            // 
            this.ChckBxStatus.AutoSize = true;
            this.ChckBxStatus.Location = new System.Drawing.Point(1064, 296);
            this.ChckBxStatus.Name = "ChckBxStatus";
            this.ChckBxStatus.Size = new System.Drawing.Size(79, 24);
            this.ChckBxStatus.TabIndex = 26;
            this.ChckBxStatus.Text = "Törölve";
            this.ChckBxStatus.UseVisualStyleBackColor = true;
            // 
            // TablaMuvelet
            // 
            this.TablaMuvelet.AllowUserToAddRows = false;
            this.TablaMuvelet.AllowUserToDeleteRows = false;
            this.TablaMuvelet.AllowUserToResizeRows = false;
            this.TablaMuvelet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TablaMuvelet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TablaMuvelet.FilterAndSortEnabled = true;
            this.TablaMuvelet.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.TablaMuvelet.Location = new System.Drawing.Point(10, 9);
            this.TablaMuvelet.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.TablaMuvelet.MaxFilterButtonImageHeight = 23;
            this.TablaMuvelet.Name = "TablaMuvelet";
            this.TablaMuvelet.ReadOnly = true;
            this.TablaMuvelet.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TablaMuvelet.RowHeadersVisible = false;
            this.TablaMuvelet.RowHeadersWidth = 62;
            this.TablaMuvelet.RowTemplate.Height = 28;
            this.TablaMuvelet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TablaMuvelet.Size = new System.Drawing.Size(1146, 249);
            this.TablaMuvelet.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.TablaMuvelet.TabIndex = 28;
            this.TablaMuvelet.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tabla_CellClick);
            this.TablaMuvelet.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.TablaMuvelet_DataBindingComplete);
            this.TablaMuvelet.SelectionChanged += new System.EventHandler(this.Tabla_SelectionChanged);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // Btn_Modosit
            // 
            this.Btn_Modosit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Modosit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Modosit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(200)))), ((int)(((byte)(184)))));
            this.Btn_Modosit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Modosit.Image = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Btn_Modosit.Location = new System.Drawing.Point(836, 405);
            this.Btn_Modosit.Name = "Btn_Modosit";
            this.Btn_Modosit.Size = new System.Drawing.Size(40, 40);
            this.Btn_Modosit.TabIndex = 45;
            this.toolTip1.SetToolTip(this.Btn_Modosit, "Művelet módosítása");
            this.Btn_Modosit.UseVisualStyleBackColor = true;
            this.Btn_Modosit.Click += new System.EventHandler(this.Btn_Modosit_Click);
            // 
            // Btn_Excel
            // 
            this.Btn_Excel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Excel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(200)))), ((int)(((byte)(184)))));
            this.Btn_Excel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Excel.Image = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Btn_Excel.Location = new System.Drawing.Point(1020, 405);
            this.Btn_Excel.Name = "Btn_Excel";
            this.Btn_Excel.Size = new System.Drawing.Size(40, 40);
            this.Btn_Excel.TabIndex = 248;
            this.toolTip1.SetToolTip(this.Btn_Excel, "Excel táblázatot készít a táblázat adataiból");
            this.Btn_Excel.UseVisualStyleBackColor = true;
            this.Btn_Excel.Click += new System.EventHandler(this.Btn_Excel_Click);
            // 
            // Btn_UjFelvetel
            // 
            this.Btn_UjFelvetel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_UjFelvetel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_UjFelvetel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(200)))), ((int)(((byte)(184)))));
            this.Btn_UjFelvetel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_UjFelvetel.Image = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Btn_UjFelvetel.Location = new System.Drawing.Point(928, 405);
            this.Btn_UjFelvetel.Name = "Btn_UjFelvetel";
            this.Btn_UjFelvetel.Size = new System.Drawing.Size(40, 40);
            this.Btn_UjFelvetel.TabIndex = 27;
            this.toolTip1.SetToolTip(this.Btn_UjFelvetel, "Új Művelet felvétele");
            this.Btn_UjFelvetel.UseVisualStyleBackColor = true;
            this.Btn_UjFelvetel.Click += new System.EventHandler(this.Btn_UjFelvetel_Click);
            // 
            // Btn_Csere
            // 
            this.Btn_Csere.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Csere.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Csere.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(200)))), ((int)(((byte)(184)))));
            this.Btn_Csere.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Csere.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Csere.Image")));
            this.Btn_Csere.Location = new System.Drawing.Point(1066, 405);
            this.Btn_Csere.Name = "Btn_Csere";
            this.Btn_Csere.Size = new System.Drawing.Size(40, 40);
            this.Btn_Csere.TabIndex = 29;
            this.toolTip1.SetToolTip(this.Btn_Csere, "Műveletek cseréje");
            this.Btn_Csere.UseVisualStyleBackColor = true;
            this.Btn_Csere.Click += new System.EventHandler(this.Btn_Csere_Click);
            // 
            // Btn_Sorrend
            // 
            this.Btn_Sorrend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Sorrend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Sorrend.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(200)))), ((int)(((byte)(184)))));
            this.Btn_Sorrend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Sorrend.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Sorrend.Image")));
            this.Btn_Sorrend.Location = new System.Drawing.Point(1112, 405);
            this.Btn_Sorrend.Name = "Btn_Sorrend";
            this.Btn_Sorrend.Size = new System.Drawing.Size(40, 40);
            this.Btn_Sorrend.TabIndex = 33;
            this.toolTip1.SetToolTip(this.Btn_Sorrend, "Műveletek sorrend cseréje");
            this.Btn_Sorrend.UseVisualStyleBackColor = true;
            this.Btn_Sorrend.Click += new System.EventHandler(this.Btn_Sorrend_Click);
            // 
            // Btn_Torles
            // 
            this.Btn_Torles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Torles.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Torles.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(200)))), ((int)(((byte)(184)))));
            this.Btn_Torles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Torles.Image = global::Villamos.Properties.Resources.Kuka;
            this.Btn_Torles.Location = new System.Drawing.Point(882, 405);
            this.Btn_Torles.Name = "Btn_Torles";
            this.Btn_Torles.Size = new System.Drawing.Size(40, 40);
            this.Btn_Torles.TabIndex = 35;
            this.toolTip1.SetToolTip(this.Btn_Torles, "Művelet törlése");
            this.Btn_Torles.UseVisualStyleBackColor = true;
            this.Btn_Torles.Click += new System.EventHandler(this.Btn_Torles_Click);
            // 
            // Btn_Uzemora_Oldal
            // 
            this.Btn_Uzemora_Oldal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Uzemora_Oldal.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(200)))), ((int)(((byte)(184)))));
            this.Btn_Uzemora_Oldal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Uzemora_Oldal.Image = global::Villamos.Properties.Resources.Action_configure;
            this.Btn_Uzemora_Oldal.Location = new System.Drawing.Point(10, 405);
            this.Btn_Uzemora_Oldal.Name = "Btn_Uzemora_Oldal";
            this.Btn_Uzemora_Oldal.Size = new System.Drawing.Size(40, 40);
            this.Btn_Uzemora_Oldal.TabIndex = 249;
            this.toolTip1.SetToolTip(this.Btn_Uzemora_Oldal, "Üzemóra állítása");
            this.Btn_Uzemora_Oldal.UseVisualStyleBackColor = true;
            this.Btn_Uzemora_Oldal.Click += new System.EventHandler(this.Btn_Uzemora_Oldal_Click);
            // 
            // Btn_Pdf
            // 
            this.Btn_Pdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Pdf.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Pdf.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(200)))), ((int)(((byte)(184)))));
            this.Btn_Pdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Pdf.Image = global::Villamos.Properties.Resources.pdf_32;
            this.Btn_Pdf.Location = new System.Drawing.Point(974, 405);
            this.Btn_Pdf.Name = "Btn_Pdf";
            this.Btn_Pdf.Size = new System.Drawing.Size(40, 40);
            this.Btn_Pdf.TabIndex = 250;
            this.toolTip1.SetToolTip(this.Btn_Pdf, "PDF készítés a táblázat adataiból");
            this.Btn_Pdf.UseVisualStyleBackColor = true;
            this.Btn_Pdf.Click += new System.EventHandler(this.Btn_Pdf_Click);
            // 
            // Btn_Naplo_Oldal
            // 
            this.Btn_Naplo_Oldal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Naplo_Oldal.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(200)))), ((int)(((byte)(184)))));
            this.Btn_Naplo_Oldal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Naplo_Oldal.Image = global::Villamos.Properties.Resources.App_dict;
            this.Btn_Naplo_Oldal.Location = new System.Drawing.Point(56, 405);
            this.Btn_Naplo_Oldal.Name = "Btn_Naplo_Oldal";
            this.Btn_Naplo_Oldal.Size = new System.Drawing.Size(40, 40);
            this.Btn_Naplo_Oldal.TabIndex = 251;
            this.toolTip1.SetToolTip(this.Btn_Naplo_Oldal, "Napló műveletek");
            this.Btn_Naplo_Oldal.UseVisualStyleBackColor = true;
            this.Btn_Naplo_Oldal.Click += new System.EventHandler(this.Btn_Naplo_Oldal_Click);
            // 
            // GrpBxMuveletek
            // 
            this.GrpBxMuveletek.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GrpBxMuveletek.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(200)))), ((int)(((byte)(184)))));
            this.GrpBxMuveletek.Controls.Add(this.Btn_Naplo_Oldal);
            this.GrpBxMuveletek.Controls.Add(this.Btn_Pdf);
            this.GrpBxMuveletek.Controls.Add(this.Btn_Uzemora_Oldal);
            this.GrpBxMuveletek.Controls.Add(this.Btn_Modosit);
            this.GrpBxMuveletek.Controls.Add(this.Btn_Excel);
            this.GrpBxMuveletek.Controls.Add(this.TablaMuvelet);
            this.GrpBxMuveletek.Controls.Add(this.Btn_UjFelvetel);
            this.GrpBxMuveletek.Controls.Add(this.ChckBxStatus);
            this.GrpBxMuveletek.Controls.Add(this.Btn_Csere);
            this.GrpBxMuveletek.Controls.Add(this.DtmPckrUtolsoDatum);
            this.GrpBxMuveletek.Controls.Add(this.Btn_Sorrend);
            this.GrpBxMuveletek.Controls.Add(this.CmbxEgyseg);
            this.GrpBxMuveletek.Controls.Add(this.Btn_Torles);
            this.GrpBxMuveletek.Controls.Add(this.LblUtolsoUzemA);
            this.GrpBxMuveletek.Controls.Add(this.LblUtolsoDat);
            this.GrpBxMuveletek.Controls.Add(this.TxtBxMuvelet);
            this.GrpBxMuveletek.Controls.Add(this.LblStat);
            this.GrpBxMuveletek.Controls.Add(this.TxtBxId);
            this.GrpBxMuveletek.Controls.Add(this.LblOra);
            this.GrpBxMuveletek.Controls.Add(this.TxtBxMennyiNap);
            this.GrpBxMuveletek.Controls.Add(this.LblNap);
            this.GrpBxMuveletek.Controls.Add(this.TxtBxMennyiOra);
            this.GrpBxMuveletek.Controls.Add(this.LblEgyseg);
            this.GrpBxMuveletek.Controls.Add(this.TxtBxUtolsoUzemoraAllas);
            this.GrpBxMuveletek.Controls.Add(this.LblMuvelet);
            this.GrpBxMuveletek.Controls.Add(this.LblSorsz);
            this.GrpBxMuveletek.Location = new System.Drawing.Point(12, 12);
            this.GrpBxMuveletek.Name = "GrpBxMuveletek";
            this.GrpBxMuveletek.Size = new System.Drawing.Size(1167, 451);
            this.GrpBxMuveletek.TabIndex = 249;
            this.GrpBxMuveletek.TabStop = false;
            // 
            // Ablak_Eszterga_Karbantartás_Módosít
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Tan;
            this.ClientSize = new System.Drawing.Size(1196, 474);
            this.Controls.Add(this.GrpBxMuveletek);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Eszterga_Karbantartás_Módosít";
            this.Text = "Kerékeszterga műveletek módosítása";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Uj_ablak_EsztergaMódosít_Closed);
            this.Load += new System.EventHandler(this.Ablak_Eszterga_Karbantartás_Módosít_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TablaMuvelet)).EndInit();
            this.GrpBxMuveletek.ResumeLayout(false);
            this.GrpBxMuveletek.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.TextBox TxtBxId;
        internal System.Windows.Forms.TextBox TxtBxMuvelet;
        internal System.Windows.Forms.TextBox TxtBxMennyiNap;
        internal System.Windows.Forms.TextBox TxtBxMennyiOra;
        internal System.Windows.Forms.TextBox TxtBxUtolsoUzemoraAllas;
        internal System.Windows.Forms.Label LblSorsz;
        internal System.Windows.Forms.Label LblMuvelet;
        internal System.Windows.Forms.Label LblEgyseg;
        internal System.Windows.Forms.Label LblNap;
        internal System.Windows.Forms.Label LblOra;
        internal System.Windows.Forms.Label LblStat;
        internal System.Windows.Forms.Label LblUtolsoDat;
        internal System.Windows.Forms.Label LblUtolsoUzemA;
        internal System.Windows.Forms.ComboBox CmbxEgyseg;
        internal System.Windows.Forms.DateTimePicker DtmPckrUtolsoDatum;
        internal System.Windows.Forms.CheckBox ChckBxStatus;
        internal System.Windows.Forms.Button Btn_UjFelvetel;
        internal Zuby.ADGV.AdvancedDataGridView TablaMuvelet;
        internal System.Windows.Forms.Button Btn_Csere;
        internal System.Windows.Forms.Button Btn_Sorrend;
        internal System.Windows.Forms.Button Btn_Torles;
        internal System.Windows.Forms.ToolTip toolTip1;
        internal System.Windows.Forms.Button Btn_Modosit;
        internal System.Windows.Forms.Button Btn_Excel;
        internal System.Windows.Forms.GroupBox GrpBxMuveletek;
        internal System.Windows.Forms.Button Btn_Uzemora_Oldal;
        internal System.Windows.Forms.Button Btn_Pdf;
        internal System.Windows.Forms.Button Btn_Naplo_Oldal;
    }
}