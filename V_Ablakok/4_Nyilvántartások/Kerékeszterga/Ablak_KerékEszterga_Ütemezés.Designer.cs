namespace Villamos.Villamos_Ablakok
{
    partial class Ablak_KerékEszterga_Ütemezés
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_KerékEszterga_Ütemezés));
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Holtart = new V_MindenEgyéb.MyProgressbar();
            this.Fülek = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.MiniBeosztás = new System.Windows.Forms.Button();
            this.Heti_jelentés = new System.Windows.Forms.Button();
            this.Munkaközi = new System.Windows.Forms.Button();
            this.Rögzítés = new System.Windows.Forms.Button();
            this.Sor_Beszúrása = new System.Windows.Forms.Button();
            this.Sor_törlése = new System.Windows.Forms.Button();
            this.Terjesztési = new System.Windows.Forms.Button();
            this.Heti_terv_küldés = new System.Windows.Forms.Button();
            this.Választék_Lista = new System.Windows.Forms.Button();
            this.Esztergályosok = new System.Windows.Forms.Button();
            this.BeosztásAdatok = new System.Windows.Forms.Button();
            this.Dátum = new System.Windows.Forms.DateTimePicker();
            this.Terv_Tábla = new System.Windows.Forms.DataGridView();
            this.Terv_Lista = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.Telephely = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Igény_Státus = new System.Windows.Forms.ComboBox();
            this.Igény_Típus = new System.Windows.Forms.ComboBox();
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.Visszaállítás = new System.Windows.Forms.Button();
            this.Excel_készítés = new System.Windows.Forms.Button();
            this.Elkészült = new System.Windows.Forms.Button();
            this.Törölt = new System.Windows.Forms.Button();
            this.Lista_Tábla = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.Panel2.SuspendLayout();
            this.Fülek.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Terv_Tábla)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.Cmbtelephely);
            this.Panel2.Controls.Add(this.Label13);
            this.Panel2.Location = new System.Drawing.Point(5, 9);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(335, 33);
            this.Panel2.TabIndex = 175;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(143, 0);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(5, 5);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(145, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(346, 13);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(846, 28);
            this.Holtart.TabIndex = 178;
            this.Holtart.Visible = false;
            // 
            // Fülek
            // 
            this.Fülek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fülek.Controls.Add(this.tabPage2);
            this.Fülek.Controls.Add(this.tabPage1);
            this.Fülek.Location = new System.Drawing.Point(7, 56);
            this.Fülek.Multiline = true;
            this.Fülek.Name = "Fülek";
            this.Fülek.SelectedIndex = 0;
            this.Fülek.Size = new System.Drawing.Size(1236, 330);
            this.Fülek.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.Fülek.TabIndex = 183;
            this.Fülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Fülek_DrawItem);
            this.Fülek.SelectedIndexChanged += new System.EventHandler(this.Fülek_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.tabPage2.Controls.Add(this.MiniBeosztás);
            this.tabPage2.Controls.Add(this.Heti_jelentés);
            this.tabPage2.Controls.Add(this.Munkaközi);
            this.tabPage2.Controls.Add(this.Rögzítés);
            this.tabPage2.Controls.Add(this.Sor_Beszúrása);
            this.tabPage2.Controls.Add(this.Sor_törlése);
            this.tabPage2.Controls.Add(this.Terjesztési);
            this.tabPage2.Controls.Add(this.Heti_terv_küldés);
            this.tabPage2.Controls.Add(this.Választék_Lista);
            this.tabPage2.Controls.Add(this.Esztergályosok);
            this.tabPage2.Controls.Add(this.BeosztásAdatok);
            this.tabPage2.Controls.Add(this.Dátum);
            this.tabPage2.Controls.Add(this.Terv_Tábla);
            this.tabPage2.Controls.Add(this.Terv_Lista);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1228, 297);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Esztergálási terv";
            // 
            // MiniBeosztás
            // 
            this.MiniBeosztás.BackgroundImage = global::Villamos.Properties.Resources.Dolgozó_32;
            this.MiniBeosztás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MiniBeosztás.Location = new System.Drawing.Point(762, 11);
            this.MiniBeosztás.Name = "MiniBeosztás";
            this.MiniBeosztás.Size = new System.Drawing.Size(45, 45);
            this.MiniBeosztás.TabIndex = 198;
            this.toolTip1.SetToolTip(this.MiniBeosztás, "Beosztás megjelenítés");
            this.MiniBeosztás.UseVisualStyleBackColor = true;
            this.MiniBeosztás.Click += new System.EventHandler(this.MiniBeosztás_Click);
            // 
            // Heti_jelentés
            // 
            this.Heti_jelentés.BackgroundImage = global::Villamos.Properties.Resources.App_xf_mail;
            this.Heti_jelentés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Heti_jelentés.Location = new System.Drawing.Point(711, 10);
            this.Heti_jelentés.Name = "Heti_jelentés";
            this.Heti_jelentés.Size = new System.Drawing.Size(45, 45);
            this.Heti_jelentés.TabIndex = 197;
            this.toolTip1.SetToolTip(this.Heti_jelentés, "Heti jelentés küldés e-mailben");
            this.Heti_jelentés.UseVisualStyleBackColor = true;
            this.Heti_jelentés.Click += new System.EventHandler(this.Heti_jelentés_Click);
            // 
            // Munkaközi
            // 
            this.Munkaközi.BackgroundImage = global::Villamos.Properties.Resources.Icons_Land_Points_Of_Interest_Restaurant_Blue;
            this.Munkaközi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Munkaközi.Location = new System.Drawing.Point(478, 10);
            this.Munkaközi.Name = "Munkaközi";
            this.Munkaközi.Size = new System.Drawing.Size(45, 45);
            this.Munkaközi.TabIndex = 196;
            this.toolTip1.SetToolTip(this.Munkaközi, "Munkaközi szünet gyorsgomb");
            this.Munkaközi.UseVisualStyleBackColor = true;
            this.Munkaközi.Click += new System.EventHandler(this.Munkaközi_Click);
            // 
            // Rögzítés
            // 
            this.Rögzítés.BackgroundImage = global::Villamos.Properties.Resources.Document_preferences;
            this.Rögzítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Rögzítés.Location = new System.Drawing.Point(325, 10);
            this.Rögzítés.Name = "Rögzítés";
            this.Rögzítés.Size = new System.Drawing.Size(45, 45);
            this.Rögzítés.TabIndex = 195;
            this.toolTip1.SetToolTip(this.Rögzítés, "Egy tevékenységet rögzítése");
            this.Rögzítés.UseVisualStyleBackColor = true;
            this.Rögzítés.Click += new System.EventHandler(this.Rögzítés_Click);
            // 
            // Sor_Beszúrása
            // 
            this.Sor_Beszúrása.BackgroundImage = global::Villamos.Properties.Resources.Fatcow_Farm_Fresh_Table_row_insert;
            this.Sor_Beszúrása.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Sor_Beszúrása.Location = new System.Drawing.Point(376, 11);
            this.Sor_Beszúrása.Name = "Sor_Beszúrása";
            this.Sor_Beszúrása.Size = new System.Drawing.Size(45, 45);
            this.Sor_Beszúrása.TabIndex = 194;
            this.toolTip1.SetToolTip(this.Sor_Beszúrása, "Beszúr egy tevékenységet a kiválasztott helyre");
            this.Sor_Beszúrása.UseVisualStyleBackColor = true;
            this.Sor_Beszúrása.Click += new System.EventHandler(this.Sor_Beszúrása_Click);
            // 
            // Sor_törlése
            // 
            this.Sor_törlése.BackgroundImage = global::Villamos.Properties.Resources.Fatcow_Farm_Fresh_Table_row_delete_32;
            this.Sor_törlése.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Sor_törlése.Location = new System.Drawing.Point(427, 10);
            this.Sor_törlése.Name = "Sor_törlése";
            this.Sor_törlése.Size = new System.Drawing.Size(45, 45);
            this.Sor_törlése.TabIndex = 193;
            this.toolTip1.SetToolTip(this.Sor_törlése, "Törli a kiválaszotott helyen lévő tervet és  az ütemezést ennek megfelelően módos" +
        "ítja");
            this.Sor_törlése.UseVisualStyleBackColor = true;
            this.Sor_törlése.Click += new System.EventHandler(this.Sor_törlése_Click);
            // 
            // Terjesztési
            // 
            this.Terjesztési.BackgroundImage = global::Villamos.Properties.Resources.mail_next_32;
            this.Terjesztési.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Terjesztési.Location = new System.Drawing.Point(1066, 10);
            this.Terjesztési.Name = "Terjesztési";
            this.Terjesztési.Size = new System.Drawing.Size(45, 45);
            this.Terjesztési.TabIndex = 192;
            this.toolTip1.SetToolTip(this.Terjesztési, "Terjesztési lista");
            this.Terjesztési.UseVisualStyleBackColor = true;
            this.Terjesztési.Click += new System.EventHandler(this.Terjesztési_Click);
            // 
            // Heti_terv_küldés
            // 
            this.Heti_terv_küldés.BackgroundImage = global::Villamos.Properties.Resources.email;
            this.Heti_terv_küldés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Heti_terv_küldés.Location = new System.Drawing.Point(660, 10);
            this.Heti_terv_küldés.Name = "Heti_terv_küldés";
            this.Heti_terv_küldés.Size = new System.Drawing.Size(45, 45);
            this.Heti_terv_küldés.TabIndex = 191;
            this.toolTip1.SetToolTip(this.Heti_terv_küldés, "Heti tervet elküld");
            this.Heti_terv_küldés.UseVisualStyleBackColor = true;
            this.Heti_terv_küldés.Click += new System.EventHandler(this.Heti_terv_küldés_Click);
            // 
            // Választék_Lista
            // 
            this.Választék_Lista.BackgroundImage = global::Villamos.Properties.Resources.Gear_01;
            this.Választék_Lista.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Választék_Lista.Location = new System.Drawing.Point(1168, 11);
            this.Választék_Lista.Name = "Választék_Lista";
            this.Választék_Lista.Size = new System.Drawing.Size(45, 45);
            this.Választék_Lista.TabIndex = 190;
            this.toolTip1.SetToolTip(this.Választék_Lista, "Beállítások");
            this.Választék_Lista.UseVisualStyleBackColor = true;
            this.Választék_Lista.Click += new System.EventHandler(this.Választék_Lista_Click);
            // 
            // Esztergályosok
            // 
            this.Esztergályosok.BackgroundImage = global::Villamos.Properties.Resources.felhasználók32;
            this.Esztergályosok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Esztergályosok.Location = new System.Drawing.Point(1117, 10);
            this.Esztergályosok.Name = "Esztergályosok";
            this.Esztergályosok.Size = new System.Drawing.Size(45, 45);
            this.Esztergályosok.TabIndex = 189;
            this.toolTip1.SetToolTip(this.Esztergályosok, "Esztergályosok kiválasztása");
            this.Esztergályosok.UseVisualStyleBackColor = true;
            this.Esztergályosok.Click += new System.EventHandler(this.Esztergályosok_Click);
            // 
            // BeosztásAdatok
            // 
            this.BeosztásAdatok.BackgroundImage = global::Villamos.Properties.Resources.App_network_connection_manager;
            this.BeosztásAdatok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BeosztásAdatok.Location = new System.Drawing.Point(192, 10);
            this.BeosztásAdatok.Name = "BeosztásAdatok";
            this.BeosztásAdatok.Size = new System.Drawing.Size(45, 45);
            this.BeosztásAdatok.TabIndex = 188;
            this.toolTip1.SetToolTip(this.BeosztásAdatok, "Beosztás adatainak megfelelően a rendelkezésre\r\nálló időt átemeli.");
            this.BeosztásAdatok.UseVisualStyleBackColor = true;
            this.BeosztásAdatok.Click += new System.EventHandler(this.BeosztásAdatok_Click);
            // 
            // Dátum
            // 
            this.Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum.Location = new System.Drawing.Point(8, 29);
            this.Dátum.Name = "Dátum";
            this.Dátum.Size = new System.Drawing.Size(127, 26);
            this.Dátum.TabIndex = 188;
            // 
            // Terv_Tábla
            // 
            this.Terv_Tábla.AllowUserToAddRows = false;
            this.Terv_Tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Terv_Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Terv_Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Terv_Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Terv_Tábla.EnableHeadersVisualStyles = false;
            this.Terv_Tábla.Location = new System.Drawing.Point(8, 62);
            this.Terv_Tábla.Name = "Terv_Tábla";
            this.Terv_Tábla.RowHeadersVisible = false;
            this.Terv_Tábla.Size = new System.Drawing.Size(1214, 222);
            this.Terv_Tábla.TabIndex = 187;
            this.Terv_Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Terv_Tábla_CellClick);
            // 
            // Terv_Lista
            // 
            this.Terv_Lista.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Terv_Lista.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Terv_Lista.Location = new System.Drawing.Point(141, 11);
            this.Terv_Lista.Name = "Terv_Lista";
            this.Terv_Lista.Size = new System.Drawing.Size(45, 45);
            this.Terv_Lista.TabIndex = 186;
            this.toolTip1.SetToolTip(this.Terv_Lista, "Frissíti a kiválasztott pályaszámnak megfelelően a táblázatot.\r\n");
            this.Terv_Lista.UseVisualStyleBackColor = true;
            this.Terv_Lista.Click += new System.EventHandler(this.Terv_Lista_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Goldenrod;
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.Telephely);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.Igény_Státus);
            this.tabPage1.Controls.Add(this.Igény_Típus);
            this.tabPage1.Controls.Add(this.Tábla);
            this.tabPage1.Controls.Add(this.Visszaállítás);
            this.tabPage1.Controls.Add(this.Excel_készítés);
            this.tabPage1.Controls.Add(this.Elkészült);
            this.tabPage1.Controls.Add(this.Törölt);
            this.tabPage1.Controls.Add(this.Lista_Tábla);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1228, 297);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Igények";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 20);
            this.label3.TabIndex = 194;
            this.label3.Text = "Telephely:";
            // 
            // Telephely
            // 
            this.Telephely.FormattingEnabled = true;
            this.Telephely.Location = new System.Drawing.Point(8, 37);
            this.Telephely.Name = "Telephely";
            this.Telephely.Size = new System.Drawing.Size(157, 28);
            this.Telephely.TabIndex = 193;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(340, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 20);
            this.label2.TabIndex = 192;
            this.label2.Text = "Státus:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(174, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 191;
            this.label1.Text = "Típus:";
            // 
            // Igény_Státus
            // 
            this.Igény_Státus.FormattingEnabled = true;
            this.Igény_Státus.Location = new System.Drawing.Point(338, 36);
            this.Igény_Státus.Name = "Igény_Státus";
            this.Igény_Státus.Size = new System.Drawing.Size(183, 28);
            this.Igény_Státus.TabIndex = 190;
            // 
            // Igény_Típus
            // 
            this.Igény_Típus.FormattingEnabled = true;
            this.Igény_Típus.Location = new System.Drawing.Point(174, 36);
            this.Igény_Típus.Name = "Igény_Típus";
            this.Igény_Típus.Size = new System.Drawing.Size(157, 28);
            this.Igény_Típus.TabIndex = 19;
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.EnableHeadersVisualStyles = false;
            this.Tábla.Location = new System.Drawing.Point(8, 70);
            this.Tábla.Name = "Tábla";
            this.Tábla.RowHeadersWidth = 25;
            this.Tábla.Size = new System.Drawing.Size(1214, 218);
            this.Tábla.TabIndex = 185;
            // 
            // Visszaállítás
            // 
            this.Visszaállítás.BackgroundImage = global::Villamos.Properties.Resources.visszavonás;
            this.Visszaállítás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Visszaállítás.Location = new System.Drawing.Point(639, 14);
            this.Visszaállítás.Name = "Visszaállítás";
            this.Visszaállítás.Size = new System.Drawing.Size(50, 50);
            this.Visszaállítás.TabIndex = 189;
            this.toolTip1.SetToolTip(this.Visszaállítás, "A kijelölt sorokat visszaállítja Ütemezettre státuszúra\r\n\r\n");
            this.Visszaállítás.UseVisualStyleBackColor = true;
            this.Visszaállítás.Click += new System.EventHandler(this.Visszaállítás_Click);
            // 
            // Excel_készítés
            // 
            this.Excel_készítés.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excel_készítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excel_készítés.Location = new System.Drawing.Point(751, 14);
            this.Excel_készítés.Name = "Excel_készítés";
            this.Excel_készítés.Size = new System.Drawing.Size(50, 50);
            this.Excel_készítés.TabIndex = 188;
            this.toolTip1.SetToolTip(this.Excel_készítés, "Táblázat adatait Excelbe exportálja");
            this.Excel_készítés.UseVisualStyleBackColor = true;
            this.Excel_készítés.Click += new System.EventHandler(this.Excel_készítés_Click);
            // 
            // Elkészült
            // 
            this.Elkészült.BackgroundImage = global::Villamos.Properties.Resources.process_accept;
            this.Elkészült.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Elkészült.Location = new System.Drawing.Point(583, 14);
            this.Elkészült.Name = "Elkészült";
            this.Elkészült.Size = new System.Drawing.Size(50, 50);
            this.Elkészült.TabIndex = 187;
            this.toolTip1.SetToolTip(this.Elkészült, "A kijelölt sorokat Készre állítja.");
            this.Elkészült.UseVisualStyleBackColor = true;
            this.Elkészült.Click += new System.EventHandler(this.Elkészült_Click);
            // 
            // Törölt
            // 
            this.Törölt.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Törölt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Törölt.Location = new System.Drawing.Point(695, 14);
            this.Törölt.Name = "Törölt";
            this.Törölt.Size = new System.Drawing.Size(50, 50);
            this.Törölt.TabIndex = 186;
            this.toolTip1.SetToolTip(this.Törölt, "A kijelölt soroknak törölt státuszúra állítja");
            this.Törölt.UseVisualStyleBackColor = true;
            this.Törölt.Click += new System.EventHandler(this.Törölt_Click);
            // 
            // Lista_Tábla
            // 
            this.Lista_Tábla.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Lista_Tábla.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lista_Tábla.Location = new System.Drawing.Point(527, 14);
            this.Lista_Tábla.Name = "Lista_Tábla";
            this.Lista_Tábla.Size = new System.Drawing.Size(50, 50);
            this.Lista_Tábla.TabIndex = 184;
            this.toolTip1.SetToolTip(this.Lista_Tábla, "Frissíti a kiválasztott pályaszámnak megfelelően a táblázatot.\r\n");
            this.Lista_Tábla.UseVisualStyleBackColor = true;
            this.Lista_Tábla.Click += new System.EventHandler(this.Lista_Tábla_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1198, 5);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 179;
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // Ablak_KerékEszterga_Ütemezés
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1250, 390);
            this.Controls.Add(this.Fülek);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Panel2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_KerékEszterga_Ütemezés";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ablak_KerékEszterga_Ütemezés";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_KerékEszterga_Ütemezés_FormClosed);
            this.Load += new System.EventHandler(this.Ablak_KerékEszterga_Ütemezés_Load);
            this.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.Ablak_KerékEszterga_Ütemezés_ControlAdded);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.Fülek.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Terv_Tábla)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.ComboBox Cmbtelephely;
        internal System.Windows.Forms.Label Label13;
        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal System.Windows.Forms.Button BtnSúgó;
        internal System.Windows.Forms.TabControl Fülek;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolTip toolTip1;
        internal System.Windows.Forms.DataGridView Tábla;
        internal System.Windows.Forms.Button Lista_Tábla;
        internal System.Windows.Forms.DataGridView Terv_Tábla;
        internal System.Windows.Forms.Button Terv_Lista;
        private System.Windows.Forms.DateTimePicker Dátum;
        internal System.Windows.Forms.Button BeosztásAdatok;
        internal System.Windows.Forms.Button Választék_Lista;
        internal System.Windows.Forms.Button Esztergályosok;
        internal System.Windows.Forms.ComboBox Igény_Típus;
        internal System.Windows.Forms.Button Excel_készítés;
        internal System.Windows.Forms.Button Elkészült;
        internal System.Windows.Forms.Button Törölt;
        internal System.Windows.Forms.Button Visszaállítás;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.ComboBox Igény_Státus;
        internal System.Windows.Forms.Button Heti_terv_küldés;
        internal System.Windows.Forms.Button Terjesztési;
        internal System.Windows.Forms.Button Sor_Beszúrása;
        internal System.Windows.Forms.Button Sor_törlése;
        internal System.Windows.Forms.Button Rögzítés;
        internal System.Windows.Forms.Button Munkaközi;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.ComboBox Telephely;
        internal System.Windows.Forms.Button Heti_jelentés;
        internal System.Windows.Forms.Button MiniBeosztás;
    }
}