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
            this.TxtBxMűvelet = new System.Windows.Forms.TextBox();
            this.TxtBxMennyiNap = new System.Windows.Forms.TextBox();
            this.TxtBxMennyiÓra = new System.Windows.Forms.TextBox();
            this.TxtBxUtolsóÜzemóraÁllás = new System.Windows.Forms.TextBox();
            this.LblSorsz = new System.Windows.Forms.Label();
            this.LblMűvelet = new System.Windows.Forms.Label();
            this.LblEgység = new System.Windows.Forms.Label();
            this.LblNap = new System.Windows.Forms.Label();
            this.LblÓra = new System.Windows.Forms.Label();
            this.LblStát = new System.Windows.Forms.Label();
            this.LblUtolsóDát = new System.Windows.Forms.Label();
            this.LblUtolsoÜzemÓ = new System.Windows.Forms.Label();
            this.CmbxEgység = new System.Windows.Forms.ComboBox();
            this.DtmPckrUtolsóDátum = new System.Windows.Forms.DateTimePicker();
            this.ChckBxStátus = new System.Windows.Forms.CheckBox();
            this.TablaMuvelet = new Zuby.ADGV.AdvancedDataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Btn_Módosít = new System.Windows.Forms.Button();
            this.Btn_Excel = new System.Windows.Forms.Button();
            this.Btn_ÚjFelvétel = new System.Windows.Forms.Button();
            this.Btn_Csere = new System.Windows.Forms.Button();
            this.Btn_Sorrend = new System.Windows.Forms.Button();
            this.Btn_Törlés = new System.Windows.Forms.Button();
            this.Üzemóra_Oldal = new System.Windows.Forms.Button();
            this.BttnUtolag_Modosit = new System.Windows.Forms.Button();
            this.GrpBxMűveletek = new System.Windows.Forms.GroupBox();
            this.GrpBxUtolagos = new System.Windows.Forms.GroupBox();
            this.TxtBxUtolagMegjegyzes = new System.Windows.Forms.TextBox();
            this.LblMegjegyzes = new System.Windows.Forms.Label();
            this.LblUtolagMuvelet = new System.Windows.Forms.Label();
            this.LblUtolagNaplozasTabla = new System.Windows.Forms.Label();
            this.LblAznapiUzemora = new System.Windows.Forms.Label();
            this.TxtBxUtolagUzemora = new System.Windows.Forms.TextBox();
            this.TablaUtolagMuvelet = new Zuby.ADGV.AdvancedDataGridView();
            this.LblDatumUtolag = new System.Windows.Forms.Label();
            this.TablaNaplo = new Zuby.ADGV.AdvancedDataGridView();
            this.DtmPckrUtolagos = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.TablaMuvelet)).BeginInit();
            this.GrpBxMűveletek.SuspendLayout();
            this.GrpBxUtolagos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TablaUtolagMuvelet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TablaNaplo)).BeginInit();
            this.SuspendLayout();
            // 
            // TxtBxId
            // 
            this.TxtBxId.Location = new System.Drawing.Point(105, 276);
            this.TxtBxId.Name = "TxtBxId";
            this.TxtBxId.Size = new System.Drawing.Size(92, 26);
            this.TxtBxId.TabIndex = 6;
            // 
            // TxtBxMűvelet
            // 
            this.TxtBxMűvelet.Location = new System.Drawing.Point(277, 273);
            this.TxtBxMűvelet.Multiline = true;
            this.TxtBxMűvelet.Name = "TxtBxMűvelet";
            this.TxtBxMűvelet.Size = new System.Drawing.Size(589, 110);
            this.TxtBxMűvelet.TabIndex = 7;
            // 
            // TxtBxMennyiNap
            // 
            this.TxtBxMennyiNap.Location = new System.Drawing.Point(105, 314);
            this.TxtBxMennyiNap.Name = "TxtBxMennyiNap";
            this.TxtBxMennyiNap.Size = new System.Drawing.Size(92, 26);
            this.TxtBxMennyiNap.TabIndex = 9;
            // 
            // TxtBxMennyiÓra
            // 
            this.TxtBxMennyiÓra.Location = new System.Drawing.Point(105, 354);
            this.TxtBxMennyiÓra.Name = "TxtBxMennyiÓra";
            this.TxtBxMennyiÓra.Size = new System.Drawing.Size(92, 26);
            this.TxtBxMennyiÓra.TabIndex = 10;
            // 
            // TxtBxUtolsóÜzemóraÁllás
            // 
            this.TxtBxUtolsóÜzemóraÁllás.Location = new System.Drawing.Point(874, 296);
            this.TxtBxUtolsóÜzemóraÁllás.Name = "TxtBxUtolsóÜzemóraÁllás";
            this.TxtBxUtolsóÜzemóraÁllás.Size = new System.Drawing.Size(162, 26);
            this.TxtBxUtolsóÜzemóraÁllás.TabIndex = 13;
            this.TxtBxUtolsóÜzemóraÁllás.TextChanged += new System.EventHandler(this.TxtBxUtolsóÜzemóraÁllás_TextChanged);
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
            // LblMűvelet
            // 
            this.LblMűvelet.AutoSize = true;
            this.LblMűvelet.Location = new System.Drawing.Point(203, 276);
            this.LblMűvelet.Name = "LblMűvelet";
            this.LblMűvelet.Size = new System.Drawing.Size(68, 20);
            this.LblMűvelet.TabIndex = 15;
            this.LblMűvelet.Text = "Művelet:";
            // 
            // LblEgység
            // 
            this.LblEgység.AutoSize = true;
            this.LblEgység.Location = new System.Drawing.Point(1026, 328);
            this.LblEgység.Name = "LblEgység";
            this.LblEgység.Size = new System.Drawing.Size(66, 20);
            this.LblEgység.TabIndex = 16;
            this.LblEgység.Text = "Egység:";
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
            // LblÓra
            // 
            this.LblÓra.AutoSize = true;
            this.LblÓra.Location = new System.Drawing.Point(6, 354);
            this.LblÓra.Name = "LblÓra";
            this.LblÓra.Size = new System.Drawing.Size(93, 20);
            this.LblÓra.TabIndex = 18;
            this.LblÓra.Text = "Mennyi Óra:";
            // 
            // LblStát
            // 
            this.LblStát.AutoSize = true;
            this.LblStát.Location = new System.Drawing.Point(1060, 273);
            this.LblStát.Name = "LblStát";
            this.LblStát.Size = new System.Drawing.Size(68, 20);
            this.LblStát.TabIndex = 19;
            this.LblStát.Text = "Státusz:";
            // 
            // LblUtolsóDát
            // 
            this.LblUtolsóDát.AutoSize = true;
            this.LblUtolsóDát.Location = new System.Drawing.Point(870, 325);
            this.LblUtolsóDát.Name = "LblUtolsóDát";
            this.LblUtolsóDát.Size = new System.Drawing.Size(111, 20);
            this.LblUtolsóDát.TabIndex = 20;
            this.LblUtolsóDát.Text = "Utolsó Dátum:";
            // 
            // LblUtolsoÜzemÓ
            // 
            this.LblUtolsoÜzemÓ.AutoSize = true;
            this.LblUtolsoÜzemÓ.Location = new System.Drawing.Point(870, 273);
            this.LblUtolsoÜzemÓ.Name = "LblUtolsoÜzemÓ";
            this.LblUtolsoÜzemÓ.Size = new System.Drawing.Size(166, 20);
            this.LblUtolsoÜzemÓ.TabIndex = 21;
            this.LblUtolsoÜzemÓ.Text = "Utolsó Üzemóra Állás:";
            // 
            // CmbxEgység
            // 
            this.CmbxEgység.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbxEgység.FormattingEnabled = true;
            this.CmbxEgység.Location = new System.Drawing.Point(1030, 351);
            this.CmbxEgység.Name = "CmbxEgység";
            this.CmbxEgység.Size = new System.Drawing.Size(130, 28);
            this.CmbxEgység.TabIndex = 23;
            this.CmbxEgység.SelectedIndexChanged += new System.EventHandler(this.CmbxEgység_SelectedIndexChanged);
            // 
            // DtmPckrUtolsóDátum
            // 
            this.DtmPckrUtolsóDátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtmPckrUtolsóDátum.Location = new System.Drawing.Point(874, 348);
            this.DtmPckrUtolsóDátum.Name = "DtmPckrUtolsóDátum";
            this.DtmPckrUtolsóDátum.Size = new System.Drawing.Size(107, 26);
            this.DtmPckrUtolsóDátum.TabIndex = 25;
            this.DtmPckrUtolsóDátum.ValueChanged += new System.EventHandler(this.DtmPckrUtolsóDátum_ValueChanged);
            // 
            // ChckBxStátus
            // 
            this.ChckBxStátus.AutoSize = true;
            this.ChckBxStátus.Location = new System.Drawing.Point(1064, 296);
            this.ChckBxStátus.Name = "ChckBxStátus";
            this.ChckBxStátus.Size = new System.Drawing.Size(79, 24);
            this.ChckBxStátus.TabIndex = 26;
            this.ChckBxStátus.Text = "Törölve";
            this.ChckBxStátus.UseVisualStyleBackColor = true;
            // 
            // TáblaMűvelet
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
            this.TablaMuvelet.Name = "TáblaMűvelet";
            this.TablaMuvelet.ReadOnly = true;
            this.TablaMuvelet.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TablaMuvelet.RowHeadersVisible = false;
            this.TablaMuvelet.RowHeadersWidth = 62;
            this.TablaMuvelet.RowTemplate.Height = 28;
            this.TablaMuvelet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TablaMuvelet.Size = new System.Drawing.Size(1146, 249);
            this.TablaMuvelet.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.TablaMuvelet.TabIndex = 28;
            this.TablaMuvelet.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            this.TablaMuvelet.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.TáblaMűvelet_CellFormatting);
            this.TablaMuvelet.SelectionChanged += new System.EventHandler(this.Tábla_SelectionChanged);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // Btn_Módosít
            // 
            this.Btn_Módosít.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Módosít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Módosít.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(200)))), ((int)(((byte)(184)))));
            this.Btn_Módosít.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Módosít.Image = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Btn_Módosít.Location = new System.Drawing.Point(885, 405);
            this.Btn_Módosít.Name = "Btn_Módosít";
            this.Btn_Módosít.Size = new System.Drawing.Size(40, 40);
            this.Btn_Módosít.TabIndex = 45;
            this.toolTip1.SetToolTip(this.Btn_Módosít, "Művelet módosítása");
            this.Btn_Módosít.UseVisualStyleBackColor = true;
            this.Btn_Módosít.Click += new System.EventHandler(this.Btn_Módosít_Click);
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
            // Btn_ÚjFelvétel
            // 
            this.Btn_ÚjFelvétel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_ÚjFelvétel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_ÚjFelvétel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(200)))), ((int)(((byte)(184)))));
            this.Btn_ÚjFelvétel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_ÚjFelvétel.Image = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Btn_ÚjFelvétel.Location = new System.Drawing.Point(974, 405);
            this.Btn_ÚjFelvétel.Name = "Btn_ÚjFelvétel";
            this.Btn_ÚjFelvétel.Size = new System.Drawing.Size(40, 40);
            this.Btn_ÚjFelvétel.TabIndex = 27;
            this.toolTip1.SetToolTip(this.Btn_ÚjFelvétel, "Új Művelet felvétele");
            this.Btn_ÚjFelvétel.UseVisualStyleBackColor = true;
            this.Btn_ÚjFelvétel.Click += new System.EventHandler(this.Btn_ÚjFelvétel_Click);
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
            // Btn_Törlés
            // 
            this.Btn_Törlés.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Törlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Törlés.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(200)))), ((int)(((byte)(184)))));
            this.Btn_Törlés.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Törlés.Image = global::Villamos.Properties.Resources.Kuka;
            this.Btn_Törlés.Location = new System.Drawing.Point(928, 405);
            this.Btn_Törlés.Name = "Btn_Törlés";
            this.Btn_Törlés.Size = new System.Drawing.Size(40, 40);
            this.Btn_Törlés.TabIndex = 35;
            this.toolTip1.SetToolTip(this.Btn_Törlés, "Művelet törlése");
            this.Btn_Törlés.UseVisualStyleBackColor = true;
            this.Btn_Törlés.Click += new System.EventHandler(this.Btn_Törlés_Click);
            // 
            // Üzemóra_Oldal
            // 
            this.Üzemóra_Oldal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Üzemóra_Oldal.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(200)))), ((int)(((byte)(184)))));
            this.Üzemóra_Oldal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Üzemóra_Oldal.Image = global::Villamos.Properties.Resources.Action_configure;
            this.Üzemóra_Oldal.Location = new System.Drawing.Point(10, 405);
            this.Üzemóra_Oldal.Name = "Üzemóra_Oldal";
            this.Üzemóra_Oldal.Size = new System.Drawing.Size(40, 40);
            this.Üzemóra_Oldal.TabIndex = 249;
            this.toolTip1.SetToolTip(this.Üzemóra_Oldal, "Üzemóra állítása");
            this.Üzemóra_Oldal.UseVisualStyleBackColor = true;
            this.Üzemóra_Oldal.Click += new System.EventHandler(this.Üzemóra_Oldal_Click);
            // 
            // BttnUtolag_Modosit
            // 
            this.BttnUtolag_Modosit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BttnUtolag_Modosit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BttnUtolag_Modosit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(200)))), ((int)(((byte)(184)))));
            this.BttnUtolag_Modosit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BttnUtolag_Modosit.Image = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.BttnUtolag_Modosit.Location = new System.Drawing.Point(1121, 404);
            this.BttnUtolag_Modosit.Name = "BttnUtolag_Modosit";
            this.BttnUtolag_Modosit.Size = new System.Drawing.Size(40, 40);
            this.BttnUtolag_Modosit.TabIndex = 46;
            this.toolTip1.SetToolTip(this.BttnUtolag_Modosit, "Művelet módosítása");
            this.BttnUtolag_Modosit.UseVisualStyleBackColor = true;
            this.BttnUtolag_Modosit.Click += new System.EventHandler(this.BttnUtolag_Modosit_Click);
            // 
            // GrpBxMűveletek
            // 
            this.GrpBxMűveletek.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GrpBxMűveletek.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(200)))), ((int)(((byte)(184)))));
            this.GrpBxMűveletek.Controls.Add(this.Üzemóra_Oldal);
            this.GrpBxMűveletek.Controls.Add(this.Btn_Módosít);
            this.GrpBxMűveletek.Controls.Add(this.Btn_Excel);
            this.GrpBxMűveletek.Controls.Add(this.TablaMuvelet);
            this.GrpBxMűveletek.Controls.Add(this.Btn_ÚjFelvétel);
            this.GrpBxMűveletek.Controls.Add(this.ChckBxStátus);
            this.GrpBxMűveletek.Controls.Add(this.Btn_Csere);
            this.GrpBxMűveletek.Controls.Add(this.DtmPckrUtolsóDátum);
            this.GrpBxMűveletek.Controls.Add(this.Btn_Sorrend);
            this.GrpBxMűveletek.Controls.Add(this.CmbxEgység);
            this.GrpBxMűveletek.Controls.Add(this.Btn_Törlés);
            this.GrpBxMűveletek.Controls.Add(this.LblUtolsoÜzemÓ);
            this.GrpBxMűveletek.Controls.Add(this.LblUtolsóDát);
            this.GrpBxMűveletek.Controls.Add(this.TxtBxMűvelet);
            this.GrpBxMűveletek.Controls.Add(this.LblStát);
            this.GrpBxMűveletek.Controls.Add(this.TxtBxId);
            this.GrpBxMűveletek.Controls.Add(this.LblÓra);
            this.GrpBxMűveletek.Controls.Add(this.TxtBxMennyiNap);
            this.GrpBxMűveletek.Controls.Add(this.LblNap);
            this.GrpBxMűveletek.Controls.Add(this.TxtBxMennyiÓra);
            this.GrpBxMűveletek.Controls.Add(this.LblEgység);
            this.GrpBxMűveletek.Controls.Add(this.TxtBxUtolsóÜzemóraÁllás);
            this.GrpBxMűveletek.Controls.Add(this.LblMűvelet);
            this.GrpBxMűveletek.Controls.Add(this.LblSorsz);
            this.GrpBxMűveletek.Location = new System.Drawing.Point(12, 12);
            this.GrpBxMűveletek.Name = "GrpBxMűveletek";
            this.GrpBxMűveletek.Size = new System.Drawing.Size(1167, 451);
            this.GrpBxMűveletek.TabIndex = 249;
            this.GrpBxMűveletek.TabStop = false;
            // 
            // GrpBxUtolagos
            // 
            this.GrpBxUtolagos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GrpBxUtolagos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(200)))), ((int)(((byte)(184)))));
            this.GrpBxUtolagos.Controls.Add(this.BttnUtolag_Modosit);
            this.GrpBxUtolagos.Controls.Add(this.TxtBxUtolagMegjegyzes);
            this.GrpBxUtolagos.Controls.Add(this.LblMegjegyzes);
            this.GrpBxUtolagos.Controls.Add(this.LblUtolagMuvelet);
            this.GrpBxUtolagos.Controls.Add(this.LblUtolagNaplozasTabla);
            this.GrpBxUtolagos.Controls.Add(this.LblAznapiUzemora);
            this.GrpBxUtolagos.Controls.Add(this.TxtBxUtolagUzemora);
            this.GrpBxUtolagos.Controls.Add(this.TablaUtolagMuvelet);
            this.GrpBxUtolagos.Controls.Add(this.LblDatumUtolag);
            this.GrpBxUtolagos.Controls.Add(this.TablaNaplo);
            this.GrpBxUtolagos.Controls.Add(this.DtmPckrUtolagos);
            this.GrpBxUtolagos.Location = new System.Drawing.Point(12, 469);
            this.GrpBxUtolagos.Name = "GrpBxUtolagos";
            this.GrpBxUtolagos.Size = new System.Drawing.Size(1167, 451);
            this.GrpBxUtolagos.TabIndex = 250;
            this.GrpBxUtolagos.TabStop = false;
            // 
            // TxtBxUtolagMegjegyzes
            // 
            this.TxtBxUtolagMegjegyzes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtBxUtolagMegjegyzes.Location = new System.Drawing.Point(791, 87);
            this.TxtBxUtolagMegjegyzes.Multiline = true;
            this.TxtBxUtolagMegjegyzes.Name = "TxtBxUtolagMegjegyzes";
            this.TxtBxUtolagMegjegyzes.Size = new System.Drawing.Size(369, 137);
            this.TxtBxUtolagMegjegyzes.TabIndex = 53;
            // 
            // LblMegjegyzes
            // 
            this.LblMegjegyzes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblMegjegyzes.AutoSize = true;
            this.LblMegjegyzes.Location = new System.Drawing.Point(787, 64);
            this.LblMegjegyzes.Name = "LblMegjegyzes";
            this.LblMegjegyzes.Size = new System.Drawing.Size(97, 20);
            this.LblMegjegyzes.TabIndex = 52;
            this.LblMegjegyzes.Text = "Megjegyzés:";
            // 
            // LblUtolagMuvelet
            // 
            this.LblUtolagMuvelet.AutoSize = true;
            this.LblUtolagMuvelet.Location = new System.Drawing.Point(10, 20);
            this.LblUtolagMuvelet.Name = "LblUtolagMuvelet";
            this.LblUtolagMuvelet.Size = new System.Drawing.Size(107, 20);
            this.LblUtolagMuvelet.TabIndex = 51;
            this.LblUtolagMuvelet.Text = "Művelet tábla:";
            // 
            // LblUtolagNaplozasTabla
            // 
            this.LblUtolagNaplozasTabla.AutoSize = true;
            this.LblUtolagNaplozasTabla.Location = new System.Drawing.Point(0, 232);
            this.LblUtolagNaplozasTabla.Name = "LblUtolagNaplozasTabla";
            this.LblUtolagNaplozasTabla.Size = new System.Drawing.Size(121, 20);
            this.LblUtolagNaplozasTabla.TabIndex = 50;
            this.LblUtolagNaplozasTabla.Text = "Naplózási tábla:";
            // 
            // LblAznapiUzemora
            // 
            this.LblAznapiUzemora.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblAznapiUzemora.AutoSize = true;
            this.LblAznapiUzemora.Location = new System.Drawing.Point(979, 20);
            this.LblAznapiUzemora.Name = "LblAznapiUzemora";
            this.LblAznapiUzemora.Size = new System.Drawing.Size(164, 20);
            this.LblAznapiUzemora.TabIndex = 49;
            this.LblAznapiUzemora.Text = "Aznapi üzemóra állás:";
            // 
            // TxtBxUtolagUzemora
            // 
            this.TxtBxUtolagUzemora.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtBxUtolagUzemora.Location = new System.Drawing.Point(981, 43);
            this.TxtBxUtolagUzemora.Name = "TxtBxUtolagUzemora";
            this.TxtBxUtolagUzemora.Size = new System.Drawing.Size(179, 26);
            this.TxtBxUtolagUzemora.TabIndex = 48;
            // 
            // TáblaUtólagMűvelet
            // 
            this.TablaUtolagMuvelet.AllowUserToAddRows = false;
            this.TablaUtolagMuvelet.AllowUserToDeleteRows = false;
            this.TablaUtolagMuvelet.AllowUserToResizeRows = false;
            this.TablaUtolagMuvelet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TablaUtolagMuvelet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TablaUtolagMuvelet.FilterAndSortEnabled = true;
            this.TablaUtolagMuvelet.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.TablaUtolagMuvelet.Location = new System.Drawing.Point(125, 20);
            this.TablaUtolagMuvelet.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.TablaUtolagMuvelet.MaxFilterButtonImageHeight = 23;
            this.TablaUtolagMuvelet.Name = "TáblaUtólagMűvelet";
            this.TablaUtolagMuvelet.ReadOnly = true;
            this.TablaUtolagMuvelet.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TablaUtolagMuvelet.RowHeadersVisible = false;
            this.TablaUtolagMuvelet.RowHeadersWidth = 62;
            this.TablaUtolagMuvelet.RowTemplate.Height = 28;
            this.TablaUtolagMuvelet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TablaUtolagMuvelet.Size = new System.Drawing.Size(656, 204);
            this.TablaUtolagMuvelet.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.TablaUtolagMuvelet.TabIndex = 47;
            this.TablaUtolagMuvelet.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.TáblaUtólagMűvelet_CellFormatting);
            this.TablaUtolagMuvelet.SelectionChanged += new System.EventHandler(this.TáblaUtólagMűvelet_SelectionChanged);
            // 
            // LblDatumUtolag
            // 
            this.LblDatumUtolag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblDatumUtolag.AutoSize = true;
            this.LblDatumUtolag.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.LblDatumUtolag.Location = new System.Drawing.Point(788, 23);
            this.LblDatumUtolag.Name = "LblDatumUtolag";
            this.LblDatumUtolag.Size = new System.Drawing.Size(61, 20);
            this.LblDatumUtolag.TabIndex = 30;
            this.LblDatumUtolag.Text = "Dátum:";
            // 
            // TáblaNapló
            // 
            this.TablaNaplo.AllowUserToAddRows = false;
            this.TablaNaplo.AllowUserToDeleteRows = false;
            this.TablaNaplo.AllowUserToResizeRows = false;
            this.TablaNaplo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TablaNaplo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TablaNaplo.FilterAndSortEnabled = true;
            this.TablaNaplo.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.TablaNaplo.Location = new System.Drawing.Point(125, 232);
            this.TablaNaplo.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.TablaNaplo.MaxFilterButtonImageHeight = 23;
            this.TablaNaplo.Name = "TáblaNapló";
            this.TablaNaplo.ReadOnly = true;
            this.TablaNaplo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TablaNaplo.RowHeadersVisible = false;
            this.TablaNaplo.RowHeadersWidth = 62;
            this.TablaNaplo.RowTemplate.Height = 28;
            this.TablaNaplo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TablaNaplo.Size = new System.Drawing.Size(656, 212);
            this.TablaNaplo.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.TablaNaplo.TabIndex = 29;
            this.TablaNaplo.SelectionChanged += new System.EventHandler(this.TáblaNapló_SelectionChanged);
            // 
            // DtmPckrUtolagos
            // 
            this.DtmPckrUtolagos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DtmPckrUtolagos.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtmPckrUtolagos.Location = new System.Drawing.Point(855, 20);
            this.DtmPckrUtolagos.Name = "DtmPckrUtolagos";
            this.DtmPckrUtolagos.Size = new System.Drawing.Size(107, 26);
            this.DtmPckrUtolagos.TabIndex = 26;
            this.DtmPckrUtolagos.ValueChanged += new System.EventHandler(this.DtmPckrUtolagos_ValueChanged);
            // 
            // Ablak_Eszterga_Karbantartás_Módosít
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Tan;
            this.ClientSize = new System.Drawing.Size(1196, 927);
            this.Controls.Add(this.GrpBxUtolagos);
            this.Controls.Add(this.GrpBxMűveletek);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Eszterga_Karbantartás_Módosít";
            this.Text = "Kerékeszterga műveletek módosítása";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Új_ablak_EsztergaMódosít_Closed);
            this.Load += new System.EventHandler(this.Ablak_Eszterga_Karbantartás_Módosít_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TablaMuvelet)).EndInit();
            this.GrpBxMűveletek.ResumeLayout(false);
            this.GrpBxMűveletek.PerformLayout();
            this.GrpBxUtolagos.ResumeLayout(false);
            this.GrpBxUtolagos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TablaUtolagMuvelet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TablaNaplo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.TextBox TxtBxId;
        internal System.Windows.Forms.TextBox TxtBxMűvelet;
        internal System.Windows.Forms.TextBox TxtBxMennyiNap;
        internal System.Windows.Forms.TextBox TxtBxMennyiÓra;
        internal System.Windows.Forms.TextBox TxtBxUtolsóÜzemóraÁllás;
        internal System.Windows.Forms.Label LblSorsz;
        internal System.Windows.Forms.Label LblMűvelet;
        internal System.Windows.Forms.Label LblEgység;
        internal System.Windows.Forms.Label LblNap;
        internal System.Windows.Forms.Label LblÓra;
        internal System.Windows.Forms.Label LblStát;
        internal System.Windows.Forms.Label LblUtolsóDát;
        internal System.Windows.Forms.Label LblUtolsoÜzemÓ;
        internal System.Windows.Forms.ComboBox CmbxEgység;
        internal System.Windows.Forms.DateTimePicker DtmPckrUtolsóDátum;
        internal System.Windows.Forms.CheckBox ChckBxStátus;
        internal System.Windows.Forms.Button Btn_ÚjFelvétel;
        internal Zuby.ADGV.AdvancedDataGridView TablaMuvelet;
        internal System.Windows.Forms.Button Btn_Csere;
        internal System.Windows.Forms.Button Btn_Sorrend;
        internal System.Windows.Forms.Button Btn_Törlés;
        internal System.Windows.Forms.ToolTip toolTip1;
        internal System.Windows.Forms.Button Btn_Módosít;
        internal System.Windows.Forms.Button Btn_Excel;
        internal System.Windows.Forms.GroupBox GrpBxMűveletek;
        internal System.Windows.Forms.Button Üzemóra_Oldal;
        private System.Windows.Forms.GroupBox GrpBxUtolagos;
        internal Zuby.ADGV.AdvancedDataGridView TablaNaplo;
        internal System.Windows.Forms.DateTimePicker DtmPckrUtolagos;
        internal Zuby.ADGV.AdvancedDataGridView TablaUtolagMuvelet;
        internal System.Windows.Forms.Button BttnUtolag_Modosit;
        internal System.Windows.Forms.Label LblDatumUtolag;
        internal System.Windows.Forms.Label LblAznapiUzemora;
        internal System.Windows.Forms.TextBox TxtBxUtolagUzemora;
        internal System.Windows.Forms.Label LblUtolagMuvelet;
        internal System.Windows.Forms.Label LblUtolagNaplozasTabla;
        internal System.Windows.Forms.Label LblMegjegyzes;
        internal System.Windows.Forms.TextBox TxtBxUtolagMegjegyzes;
    }
}