using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
    
    public partial class Ablak_T5C5_Vizsgálat_ütemező : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components != null)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_T5C5_Vizsgálat_ütemező));
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.EszközökToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SzínválasztóToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SzínezToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.KeresésCtrlFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.BeosztásAdatokTörléseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ListázásToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AktuálisSzerelvénySzerintiListaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ElőírtSzerelvénySzerintiListaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ExcelKimenetKészítéseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.AktuálisSzerelvénySzerintVizsgálatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Fülek = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.Vonal_red = new System.Windows.Forms.TextBox();
            this.Vonal_green = new System.Windows.Forms.TextBox();
            this.Vonal_blue = new System.Windows.Forms.TextBox();
            this.Vonal_Mennyiség = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Vonal_Vonal = new System.Windows.Forms.TextBox();
            this.Vonal_Id = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Vonal_tábla = new System.Windows.Forms.DataGridView();
            this.Vonal_fel = new System.Windows.Forms.Button();
            this.Command7_Rögzítés = new System.Windows.Forms.Button();
            this.Command8_Új = new System.Windows.Forms.Button();
            this.Command11_frissít = new System.Windows.Forms.Button();
            this.Command10_Listát_töröl = new System.Windows.Forms.Button();
            this.Command9_színkereső = new System.Windows.Forms.Button();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.Utasítás_törlés = new System.Windows.Forms.Button();
            this.Txtírásimező = new System.Windows.Forms.RichTextBox();
            this.Btnrögzítés = new System.Windows.Forms.Button();
            this.Utasítás_tervezet = new System.Windows.Forms.Button();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.Felmentés_Id = new System.Windows.Forms.TextBox();
            this.CiklusTípus = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Keréktábla = new System.Windows.Forms.DataGridView();
            this.Vizs_tábla = new System.Windows.Forms.DataGridView();
            this.Kért_vizsgálat = new System.Windows.Forms.TextBox();
            this.Label39 = new System.Windows.Forms.Label();
            this.Következő_vizsgálat = new System.Windows.Forms.TextBox();
            this.Label38 = new System.Windows.Forms.Label();
            this.Ciklus_Mentés = new System.Windows.Forms.Button();
            this.J_tőlFutott = new System.Windows.Forms.TextBox();
            this.Label37 = new System.Windows.Forms.Label();
            this.Befejezés = new System.Windows.Forms.TextBox();
            this.Tárgyalás = new System.Windows.Forms.TextBox();
            this.Bevezetés = new System.Windows.Forms.TextBox();
            this.Label34 = new System.Windows.Forms.Label();
            this.Label35 = new System.Windows.Forms.Label();
            this.Label36 = new System.Windows.Forms.Label();
            this.Tárgy = new System.Windows.Forms.TextBox();
            this.Másolat = new System.Windows.Forms.TextBox();
            this.Címzett = new System.Windows.Forms.TextBox();
            this.Label33 = new System.Windows.Forms.Label();
            this.Label32 = new System.Windows.Forms.Label();
            this.Label31 = new System.Windows.Forms.Label();
            this.Label30 = new System.Windows.Forms.Label();
            this.Ciklus_Pályaszám = new System.Windows.Forms.TextBox();
            this.Email = new System.Windows.Forms.Button();
            this.CiklusFrissít = new System.Windows.Forms.Button();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.Panel2.SuspendLayout();
            this.MenuStrip1.SuspendLayout();
            this.Fülek.SuspendLayout();
            this.TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Vonal_tábla)).BeginInit();
            this.TabPage3.SuspendLayout();
            this.TabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Keréktábla)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Vizs_tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.Cmbtelephely);
            this.Panel2.Controls.Add(this.Label13);
            this.Panel2.Location = new System.Drawing.Point(4, 27);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(335, 33);
            this.Panel2.TabIndex = 57;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(149, 3);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(3, 7);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(145, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // MenuStrip1
            // 
            this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EszközökToolStripMenuItem,
            this.ListázásToolStripMenuItem});
            this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip1.Name = "MenuStrip1";
            this.MenuStrip1.Size = new System.Drawing.Size(1218, 24);
            this.MenuStrip1.TabIndex = 64;
            this.MenuStrip1.Text = "MenuStrip1";
            // 
            // EszközökToolStripMenuItem
            // 
            this.EszközökToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SzínválasztóToolStripMenuItem,
            this.SzínezToolStripMenuItem,
            this.ToolStripSeparator1,
            this.KeresésCtrlFToolStripMenuItem,
            this.ToolStripSeparator3,
            this.BeosztásAdatokTörléseToolStripMenuItem});
            this.EszközökToolStripMenuItem.Name = "EszközökToolStripMenuItem";
            this.EszközökToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.EszközökToolStripMenuItem.Text = "Eszközök";
            // 
            // SzínválasztóToolStripMenuItem
            // 
            this.SzínválasztóToolStripMenuItem.Name = "SzínválasztóToolStripMenuItem";
            this.SzínválasztóToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.SzínválasztóToolStripMenuItem.Text = "Színválasztó";
            this.SzínválasztóToolStripMenuItem.Click += new System.EventHandler(this.SzínválasztóToolStripMenuItem_Click);
            // 
            // SzínezToolStripMenuItem
            // 
            this.SzínezToolStripMenuItem.Checked = true;
            this.SzínezToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SzínezToolStripMenuItem.Name = "SzínezToolStripMenuItem";
            this.SzínezToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.SzínezToolStripMenuItem.Text = "Színez";
            this.SzínezToolStripMenuItem.Click += new System.EventHandler(this.SzínezToolStripMenuItem_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(193, 6);
            // 
            // KeresésCtrlFToolStripMenuItem
            // 
            this.KeresésCtrlFToolStripMenuItem.Name = "KeresésCtrlFToolStripMenuItem";
            this.KeresésCtrlFToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.KeresésCtrlFToolStripMenuItem.Text = "Keresés Ctrl+F";
            this.KeresésCtrlFToolStripMenuItem.Click += new System.EventHandler(this.KeresésCtrlFToolStripMenuItem_Click);
            // 
            // ToolStripSeparator3
            // 
            this.ToolStripSeparator3.Name = "ToolStripSeparator3";
            this.ToolStripSeparator3.Size = new System.Drawing.Size(193, 6);
            // 
            // BeosztásAdatokTörléseToolStripMenuItem
            // 
            this.BeosztásAdatokTörléseToolStripMenuItem.Name = "BeosztásAdatokTörléseToolStripMenuItem";
            this.BeosztásAdatokTörléseToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.BeosztásAdatokTörléseToolStripMenuItem.Text = "Beosztás adatok törlése";
            this.BeosztásAdatokTörléseToolStripMenuItem.Click += new System.EventHandler(this.BeosztásAdatokTörléseToolStripMenuItem_Click);
            // 
            // ListázásToolStripMenuItem
            // 
            this.ListázásToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AktuálisSzerelvénySzerintiListaToolStripMenuItem,
            this.ElőírtSzerelvénySzerintiListaToolStripMenuItem,
            this.ToolStripSeparator2,
            this.ExcelKimenetKészítéseToolStripMenuItem,
            this.ToolStripSeparator4,
            this.AktuálisSzerelvénySzerintVizsgálatToolStripMenuItem});
            this.ListázásToolStripMenuItem.Name = "ListázásToolStripMenuItem";
            this.ListázásToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.ListázásToolStripMenuItem.Text = "Listázás";
            // 
            // AktuálisSzerelvénySzerintiListaToolStripMenuItem
            // 
            this.AktuálisSzerelvénySzerintiListaToolStripMenuItem.Name = "AktuálisSzerelvénySzerintiListaToolStripMenuItem";
            this.AktuálisSzerelvénySzerintiListaToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.AktuálisSzerelvénySzerintiListaToolStripMenuItem.Text = "Aktuális Szerelvény szerinti Lista";
            this.AktuálisSzerelvénySzerintiListaToolStripMenuItem.Click += new System.EventHandler(this.AktuálisSzerelvénySzerintiListaToolStripMenuItem_Click);
            // 
            // ElőírtSzerelvénySzerintiListaToolStripMenuItem
            // 
            this.ElőírtSzerelvénySzerintiListaToolStripMenuItem.Name = "ElőírtSzerelvénySzerintiListaToolStripMenuItem";
            this.ElőírtSzerelvénySzerintiListaToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.ElőírtSzerelvénySzerintiListaToolStripMenuItem.Text = "Előírt Szerelvény szerinti Lista";
            this.ElőírtSzerelvénySzerintiListaToolStripMenuItem.Click += new System.EventHandler(this.ElőírtSzerelvénySzerintiListaToolStripMenuItem_Click);
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(259, 6);
            // 
            // ExcelKimenetKészítéseToolStripMenuItem
            // 
            this.ExcelKimenetKészítéseToolStripMenuItem.Name = "ExcelKimenetKészítéseToolStripMenuItem";
            this.ExcelKimenetKészítéseToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.ExcelKimenetKészítéseToolStripMenuItem.Text = "Excel kimenet készítése";
            this.ExcelKimenetKészítéseToolStripMenuItem.Click += new System.EventHandler(this.ExcelKimenetKészítéseToolStripMenuItem_Click);
            // 
            // ToolStripSeparator4
            // 
            this.ToolStripSeparator4.Name = "ToolStripSeparator4";
            this.ToolStripSeparator4.Size = new System.Drawing.Size(259, 6);
            // 
            // AktuálisSzerelvénySzerintVizsgálatToolStripMenuItem
            // 
            this.AktuálisSzerelvénySzerintVizsgálatToolStripMenuItem.Name = "AktuálisSzerelvénySzerintVizsgálatToolStripMenuItem";
            this.AktuálisSzerelvénySzerintVizsgálatToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.AktuálisSzerelvénySzerintVizsgálatToolStripMenuItem.Text = "Aktuális szerelvény szerint Vizsgálat ";
            this.AktuálisSzerelvénySzerintVizsgálatToolStripMenuItem.Click += new System.EventHandler(this.AktuálisSzerelvénySzerintVizsgálatToolStripMenuItem_Click);
            // 
            // Fülek
            // 
            this.Fülek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fülek.Controls.Add(this.TabPage1);
            this.Fülek.Controls.Add(this.TabPage2);
            this.Fülek.Controls.Add(this.TabPage3);
            this.Fülek.Controls.Add(this.TabPage4);
            this.Fülek.Location = new System.Drawing.Point(10, 67);
            this.Fülek.Name = "Fülek";
            this.Fülek.Padding = new System.Drawing.Point(16, 3);
            this.Fülek.SelectedIndex = 0;
            this.Fülek.Size = new System.Drawing.Size(1200, 590);
            this.Fülek.TabIndex = 66;
            this.Fülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Fülek_DrawItem);
            this.Fülek.SelectedIndexChanged += new System.EventHandler(this.Fülek_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.Controls.Add(this.Tábla);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1192, 557);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Vizsgálat Ütemező";
            this.TabPage1.UseVisualStyleBackColor = true;
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla.BackgroundColor = System.Drawing.Color.Silver;
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.Location = new System.Drawing.Point(3, 6);
            this.Tábla.Name = "Tábla";
            this.Tábla.Size = new System.Drawing.Size(1185, 545);
            this.Tábla.TabIndex = 66;
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            this.Tábla.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Tábla_CellFormatting);
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.NavajoWhite;
            this.TabPage2.Controls.Add(this.Vonal_red);
            this.TabPage2.Controls.Add(this.Vonal_green);
            this.TabPage2.Controls.Add(this.Vonal_blue);
            this.TabPage2.Controls.Add(this.Vonal_Mennyiség);
            this.TabPage2.Controls.Add(this.Label4);
            this.TabPage2.Controls.Add(this.Vonal_Vonal);
            this.TabPage2.Controls.Add(this.Vonal_Id);
            this.TabPage2.Controls.Add(this.Label3);
            this.TabPage2.Controls.Add(this.Label2);
            this.TabPage2.Controls.Add(this.Label1);
            this.TabPage2.Controls.Add(this.Vonal_tábla);
            this.TabPage2.Controls.Add(this.Vonal_fel);
            this.TabPage2.Controls.Add(this.Command7_Rögzítés);
            this.TabPage2.Controls.Add(this.Command8_Új);
            this.TabPage2.Controls.Add(this.Command11_frissít);
            this.TabPage2.Controls.Add(this.Command10_Listát_töröl);
            this.TabPage2.Controls.Add(this.Command9_színkereső);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(1192, 557);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Vonalak";
            // 
            // Vonal_red
            // 
            this.Vonal_red.BackColor = System.Drawing.Color.White;
            this.Vonal_red.Enabled = false;
            this.Vonal_red.Location = new System.Drawing.Point(350, 36);
            this.Vonal_red.Name = "Vonal_red";
            this.Vonal_red.Size = new System.Drawing.Size(62, 26);
            this.Vonal_red.TabIndex = 77;
            // 
            // Vonal_green
            // 
            this.Vonal_green.BackColor = System.Drawing.Color.White;
            this.Vonal_green.Enabled = false;
            this.Vonal_green.Location = new System.Drawing.Point(418, 36);
            this.Vonal_green.Name = "Vonal_green";
            this.Vonal_green.Size = new System.Drawing.Size(62, 26);
            this.Vonal_green.TabIndex = 76;
            // 
            // Vonal_blue
            // 
            this.Vonal_blue.BackColor = System.Drawing.Color.White;
            this.Vonal_blue.Enabled = false;
            this.Vonal_blue.Location = new System.Drawing.Point(486, 36);
            this.Vonal_blue.Name = "Vonal_blue";
            this.Vonal_blue.Size = new System.Drawing.Size(62, 26);
            this.Vonal_blue.TabIndex = 75;
            // 
            // Vonal_Mennyiség
            // 
            this.Vonal_Mennyiség.BackColor = System.Drawing.Color.White;
            this.Vonal_Mennyiség.Location = new System.Drawing.Point(255, 36);
            this.Vonal_Mennyiség.Name = "Vonal_Mennyiség";
            this.Vonal_Mennyiség.Size = new System.Drawing.Size(85, 26);
            this.Vonal_Mennyiség.TabIndex = 73;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(251, 13);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(89, 20);
            this.Label4.TabIndex = 72;
            this.Label4.Text = "Mennyiség:";
            // 
            // Vonal_Vonal
            // 
            this.Vonal_Vonal.BackColor = System.Drawing.Color.White;
            this.Vonal_Vonal.Location = new System.Drawing.Point(90, 36);
            this.Vonal_Vonal.Name = "Vonal_Vonal";
            this.Vonal_Vonal.Size = new System.Drawing.Size(159, 26);
            this.Vonal_Vonal.TabIndex = 71;
            // 
            // Vonal_Id
            // 
            this.Vonal_Id.BackColor = System.Drawing.Color.White;
            this.Vonal_Id.Enabled = false;
            this.Vonal_Id.Location = new System.Drawing.Point(12, 36);
            this.Vonal_Id.Name = "Vonal_Id";
            this.Vonal_Id.Size = new System.Drawing.Size(72, 26);
            this.Vonal_Id.TabIndex = 69;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(346, 13);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(44, 20);
            this.Label3.TabIndex = 3;
            this.Label3.Text = "Szín:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(86, 13);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(54, 20);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "Vonal:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(8, 13);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(76, 20);
            this.Label1.TabIndex = 1;
            this.Label1.Text = "Sorszám:";
            // 
            // Vonal_tábla
            // 
            this.Vonal_tábla.AllowUserToAddRows = false;
            this.Vonal_tábla.AllowUserToDeleteRows = false;
            this.Vonal_tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Vonal_tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Vonal_tábla.Location = new System.Drawing.Point(5, 68);
            this.Vonal_tábla.Name = "Vonal_tábla";
            this.Vonal_tábla.RowHeadersVisible = false;
            this.Vonal_tábla.Size = new System.Drawing.Size(1179, 480);
            this.Vonal_tábla.TabIndex = 0;
            this.Vonal_tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Vonal_tábla_CellClick);
            // 
            // Vonal_fel
            // 
            this.Vonal_fel.BackgroundImage = global::Villamos.Properties.Resources.Button_Upload_01;
            this.Vonal_fel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Vonal_fel.Location = new System.Drawing.Point(882, 17);
            this.Vonal_fel.Name = "Vonal_fel";
            this.Vonal_fel.Size = new System.Drawing.Size(45, 45);
            this.Vonal_fel.TabIndex = 74;
            this.ToolTip1.SetToolTip(this.Vonal_fel, "Adatsort egy sorral feljebb helyezi");
            this.Vonal_fel.UseVisualStyleBackColor = true;
            this.Vonal_fel.Click += new System.EventHandler(this.Vonal_fel_Click);
            // 
            // Command7_Rögzítés
            // 
            this.Command7_Rögzítés.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Command7_Rögzítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command7_Rögzítés.Location = new System.Drawing.Point(729, 17);
            this.Command7_Rögzítés.Name = "Command7_Rögzítés";
            this.Command7_Rögzítés.Size = new System.Drawing.Size(45, 45);
            this.Command7_Rögzítés.TabIndex = 68;
            this.ToolTip1.SetToolTip(this.Command7_Rögzítés, "Rögzíti a vonal adatait");
            this.Command7_Rögzítés.UseVisualStyleBackColor = true;
            this.Command7_Rögzítés.Click += new System.EventHandler(this.Command7_Rögzítés_Click);
            // 
            // Command8_Új
            // 
            this.Command8_Új.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Command8_Új.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command8_Új.Location = new System.Drawing.Point(780, 17);
            this.Command8_Új.Name = "Command8_Új";
            this.Command8_Új.Size = new System.Drawing.Size(45, 45);
            this.Command8_Új.TabIndex = 67;
            this.ToolTip1.SetToolTip(this.Command8_Új, "Új adatsornak készíti elő a beviteli mezőt.");
            this.Command8_Új.UseVisualStyleBackColor = true;
            this.Command8_Új.Click += new System.EventHandler(this.Command8_Új_Click);
            // 
            // Command11_frissít
            // 
            this.Command11_frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Command11_frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command11_frissít.Location = new System.Drawing.Point(831, 17);
            this.Command11_frissít.Name = "Command11_frissít";
            this.Command11_frissít.Size = new System.Drawing.Size(45, 45);
            this.Command11_frissít.TabIndex = 66;
            this.ToolTip1.SetToolTip(this.Command11_frissít, "Frissítit a táblázatot");
            this.Command11_frissít.UseVisualStyleBackColor = true;
            this.Command11_frissít.Click += new System.EventHandler(this.Command11_frissít_Click);
            // 
            // Command10_Listát_töröl
            // 
            this.Command10_Listát_töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Command10_Listát_töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command10_Listát_töröl.Location = new System.Drawing.Point(933, 17);
            this.Command10_Listát_töröl.Name = "Command10_Listát_töröl";
            this.Command10_Listát_töröl.Size = new System.Drawing.Size(45, 45);
            this.Command10_Listát_töröl.TabIndex = 65;
            this.ToolTip1.SetToolTip(this.Command10_Listát_töröl, "Törli a vonal adatait.");
            this.Command10_Listát_töröl.UseVisualStyleBackColor = true;
            this.Command10_Listát_töröl.Click += new System.EventHandler(this.Command10_Listát_töröl_Click);
            // 
            // Command9_színkereső
            // 
            this.Command9_színkereső.BackgroundImage = global::Villamos.Properties.Resources.Yellow_Glass_Folders_Icon_25;
            this.Command9_színkereső.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command9_színkereső.Location = new System.Drawing.Point(554, 17);
            this.Command9_színkereső.Name = "Command9_színkereső";
            this.Command9_színkereső.Size = new System.Drawing.Size(45, 45);
            this.Command9_színkereső.TabIndex = 64;
            this.ToolTip1.SetToolTip(this.Command9_színkereső, "Színválasztó");
            this.Command9_színkereső.UseVisualStyleBackColor = true;
            this.Command9_színkereső.Click += new System.EventHandler(this.Command9_színkereső_Click);
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.LightSeaGreen;
            this.TabPage3.Controls.Add(this.Utasítás_törlés);
            this.TabPage3.Controls.Add(this.Txtírásimező);
            this.TabPage3.Controls.Add(this.Btnrögzítés);
            this.TabPage3.Controls.Add(this.Utasítás_tervezet);
            this.TabPage3.Location = new System.Drawing.Point(4, 29);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(1192, 557);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Előírás utasítás";
            // 
            // Utasítás_törlés
            // 
            this.Utasítás_törlés.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Utasítás_törlés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Utasítás_törlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Utasítás_törlés.Location = new System.Drawing.Point(200, 11);
            this.Utasítás_törlés.Name = "Utasítás_törlés";
            this.Utasítás_törlés.Size = new System.Drawing.Size(48, 48);
            this.Utasítás_törlés.TabIndex = 80;
            this.ToolTip1.SetToolTip(this.Utasítás_törlés, "Törli az utasítást");
            this.Utasítás_törlés.UseVisualStyleBackColor = true;
            this.Utasítás_törlés.Click += new System.EventHandler(this.Utasítás_törlés_Click);
            // 
            // Txtírásimező
            // 
            this.Txtírásimező.AcceptsTab = true;
            this.Txtírásimező.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Txtírásimező.Location = new System.Drawing.Point(3, 64);
            this.Txtírásimező.Name = "Txtírásimező";
            this.Txtírásimező.Size = new System.Drawing.Size(1186, 490);
            this.Txtírásimező.TabIndex = 71;
            this.Txtírásimező.Text = "";
            // 
            // Btnrögzítés
            // 
            this.Btnrögzítés.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btnrögzítés.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Btnrögzítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btnrögzítés.Location = new System.Drawing.Point(255, 11);
            this.Btnrögzítés.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btnrögzítés.Name = "Btnrögzítés";
            this.Btnrögzítés.Size = new System.Drawing.Size(48, 48);
            this.Btnrögzítés.TabIndex = 72;
            this.ToolTip1.SetToolTip(this.Btnrögzítés, "Utasítás rögzítése");
            this.Btnrögzítés.UseVisualStyleBackColor = true;
            this.Btnrögzítés.Click += new System.EventHandler(this.Btnrögzítés_Click);
            // 
            // Utasítás_tervezet
            // 
            this.Utasítás_tervezet.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Utasítás_tervezet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Utasítás_tervezet.Location = new System.Drawing.Point(8, 13);
            this.Utasítás_tervezet.Name = "Utasítás_tervezet";
            this.Utasítás_tervezet.Size = new System.Drawing.Size(45, 45);
            this.Utasítás_tervezet.TabIndex = 70;
            this.ToolTip1.SetToolTip(this.Utasítás_tervezet, "Frissíti az utasítás tervezetet.");
            this.Utasítás_tervezet.UseVisualStyleBackColor = true;
            this.Utasítás_tervezet.Click += new System.EventHandler(this.Utasítás_tervezet_Click);
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.LightSalmon;
            this.TabPage4.Controls.Add(this.Felmentés_Id);
            this.TabPage4.Controls.Add(this.CiklusTípus);
            this.TabPage4.Controls.Add(this.label5);
            this.TabPage4.Controls.Add(this.Keréktábla);
            this.TabPage4.Controls.Add(this.Vizs_tábla);
            this.TabPage4.Controls.Add(this.Kért_vizsgálat);
            this.TabPage4.Controls.Add(this.Label39);
            this.TabPage4.Controls.Add(this.Következő_vizsgálat);
            this.TabPage4.Controls.Add(this.Label38);
            this.TabPage4.Controls.Add(this.Ciklus_Mentés);
            this.TabPage4.Controls.Add(this.J_tőlFutott);
            this.TabPage4.Controls.Add(this.Label37);
            this.TabPage4.Controls.Add(this.Befejezés);
            this.TabPage4.Controls.Add(this.Tárgyalás);
            this.TabPage4.Controls.Add(this.Bevezetés);
            this.TabPage4.Controls.Add(this.Label34);
            this.TabPage4.Controls.Add(this.Label35);
            this.TabPage4.Controls.Add(this.Label36);
            this.TabPage4.Controls.Add(this.Tárgy);
            this.TabPage4.Controls.Add(this.Másolat);
            this.TabPage4.Controls.Add(this.Címzett);
            this.TabPage4.Controls.Add(this.Label33);
            this.TabPage4.Controls.Add(this.Label32);
            this.TabPage4.Controls.Add(this.Label31);
            this.TabPage4.Controls.Add(this.Label30);
            this.TabPage4.Controls.Add(this.Ciklus_Pályaszám);
            this.TabPage4.Controls.Add(this.Email);
            this.TabPage4.Controls.Add(this.CiklusFrissít);
            this.TabPage4.Location = new System.Drawing.Point(4, 29);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage4.Size = new System.Drawing.Size(1192, 557);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Ciklus eltolás";
            // 
            // Felmentés_Id
            // 
            this.Felmentés_Id.Location = new System.Drawing.Point(7, 525);
            this.Felmentés_Id.Name = "Felmentés_Id";
            this.Felmentés_Id.Size = new System.Drawing.Size(122, 26);
            this.Felmentés_Id.TabIndex = 93;
            // 
            // CiklusTípus
            // 
            this.CiklusTípus.Enabled = false;
            this.CiklusTípus.Location = new System.Drawing.Point(144, 38);
            this.CiklusTípus.Name = "CiklusTípus";
            this.CiklusTípus.Size = new System.Drawing.Size(181, 26);
            this.CiklusTípus.TabIndex = 92;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 20);
            this.label5.TabIndex = 91;
            this.label5.Text = "Ciklus típus:";
            // 
            // Keréktábla
            // 
            this.Keréktábla.AllowUserToAddRows = false;
            this.Keréktábla.AllowUserToDeleteRows = false;
            this.Keréktábla.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Keréktábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Keréktábla.Location = new System.Drawing.Point(607, 226);
            this.Keréktábla.Name = "Keréktábla";
            this.Keréktábla.Size = new System.Drawing.Size(579, 325);
            this.Keréktábla.TabIndex = 90;
            // 
            // Vizs_tábla
            // 
            this.Vizs_tábla.AllowUserToAddRows = false;
            this.Vizs_tábla.AllowUserToDeleteRows = false;
            this.Vizs_tábla.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Vizs_tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Vizs_tábla.Location = new System.Drawing.Point(607, 10);
            this.Vizs_tábla.Name = "Vizs_tábla";
            this.Vizs_tábla.Size = new System.Drawing.Size(579, 210);
            this.Vizs_tábla.TabIndex = 89;
            // 
            // Kért_vizsgálat
            // 
            this.Kért_vizsgálat.Location = new System.Drawing.Point(425, 166);
            this.Kért_vizsgálat.Name = "Kért_vizsgálat";
            this.Kért_vizsgálat.Size = new System.Drawing.Size(122, 26);
            this.Kért_vizsgálat.TabIndex = 88;
            // 
            // Label39
            // 
            this.Label39.AutoSize = true;
            this.Label39.Location = new System.Drawing.Point(272, 172);
            this.Label39.Name = "Label39";
            this.Label39.Size = new System.Drawing.Size(125, 20);
            this.Label39.TabIndex = 87;
            this.Label39.Text = "Kért vizsgálat:(łł)";
            // 
            // Következő_vizsgálat
            // 
            this.Következő_vizsgálat.Location = new System.Drawing.Point(144, 166);
            this.Következő_vizsgálat.Name = "Következő_vizsgálat";
            this.Következő_vizsgálat.Size = new System.Drawing.Size(122, 26);
            this.Következő_vizsgálat.TabIndex = 86;
            // 
            // Label38
            // 
            this.Label38.AutoSize = true;
            this.Label38.Location = new System.Drawing.Point(8, 166);
            this.Label38.Name = "Label38";
            this.Label38.Size = new System.Drawing.Size(118, 20);
            this.Label38.TabIndex = 85;
            this.Label38.Text = "Köv. vizsg.: (ŁŁ)";
            // 
            // Ciklus_Mentés
            // 
            this.Ciklus_Mentés.BackgroundImage = global::Villamos.Properties.Resources.mentés32;
            this.Ciklus_Mentés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Ciklus_Mentés.Location = new System.Drawing.Point(556, 102);
            this.Ciklus_Mentés.Name = "Ciklus_Mentés";
            this.Ciklus_Mentés.Size = new System.Drawing.Size(45, 45);
            this.Ciklus_Mentés.TabIndex = 84;
            this.ToolTip1.SetToolTip(this.Ciklus_Mentés, "Frissítit a táblázatot");
            this.Ciklus_Mentés.UseVisualStyleBackColor = true;
            this.Ciklus_Mentés.Click += new System.EventHandler(this.Ciklus_Mentés_Click);
            // 
            // J_tőlFutott
            // 
            this.J_tőlFutott.Location = new System.Drawing.Point(425, 6);
            this.J_tőlFutott.Name = "J_tőlFutott";
            this.J_tőlFutott.Size = new System.Drawing.Size(122, 26);
            this.J_tőlFutott.TabIndex = 83;
            // 
            // Label37
            // 
            this.Label37.AutoSize = true;
            this.Label37.Location = new System.Drawing.Point(272, 12);
            this.Label37.Name = "Label37";
            this.Label37.Size = new System.Drawing.Size(124, 20);
            this.Label37.TabIndex = 82;
            this.Label37.Text = "J-től Futott: (ßß)";
            // 
            // Befejezés
            // 
            this.Befejezés.Location = new System.Drawing.Point(144, 438);
            this.Befejezés.Multiline = true;
            this.Befejezés.Name = "Befejezés";
            this.Befejezés.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Befejezés.Size = new System.Drawing.Size(457, 114);
            this.Befejezés.TabIndex = 81;
            // 
            // Tárgyalás
            // 
            this.Tárgyalás.Location = new System.Drawing.Point(144, 318);
            this.Tárgyalás.Multiline = true;
            this.Tárgyalás.Name = "Tárgyalás";
            this.Tárgyalás.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Tárgyalás.Size = new System.Drawing.Size(457, 114);
            this.Tárgyalás.TabIndex = 80;
            // 
            // Bevezetés
            // 
            this.Bevezetés.Location = new System.Drawing.Point(144, 198);
            this.Bevezetés.Multiline = true;
            this.Bevezetés.Name = "Bevezetés";
            this.Bevezetés.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Bevezetés.Size = new System.Drawing.Size(457, 114);
            this.Bevezetés.TabIndex = 79;
            // 
            // Label34
            // 
            this.Label34.AutoSize = true;
            this.Label34.Location = new System.Drawing.Point(17, 438);
            this.Label34.Name = "Label34";
            this.Label34.Size = new System.Drawing.Size(80, 20);
            this.Label34.TabIndex = 78;
            this.Label34.Text = "Befejezés";
            // 
            // Label35
            // 
            this.Label35.AutoSize = true;
            this.Label35.Location = new System.Drawing.Point(8, 318);
            this.Label35.Name = "Label35";
            this.Label35.Size = new System.Drawing.Size(81, 20);
            this.Label35.TabIndex = 77;
            this.Label35.Text = "Tárgyalás:";
            // 
            // Label36
            // 
            this.Label36.AutoSize = true;
            this.Label36.Location = new System.Drawing.Point(8, 201);
            this.Label36.Name = "Label36";
            this.Label36.Size = new System.Drawing.Size(88, 20);
            this.Label36.TabIndex = 76;
            this.Label36.Text = "Bevezetés:";
            // 
            // Tárgy
            // 
            this.Tárgy.Location = new System.Drawing.Point(144, 134);
            this.Tárgy.Name = "Tárgy";
            this.Tárgy.Size = new System.Drawing.Size(403, 26);
            this.Tárgy.TabIndex = 75;
            this.Tárgy.Text = "$$ V3 vizsgálatának eltolás engelyezése";
            // 
            // Másolat
            // 
            this.Másolat.Location = new System.Drawing.Point(144, 102);
            this.Másolat.Name = "Másolat";
            this.Másolat.Size = new System.Drawing.Size(403, 26);
            this.Másolat.TabIndex = 74;
            // 
            // Címzett
            // 
            this.Címzett.Location = new System.Drawing.Point(144, 70);
            this.Címzett.Name = "Címzett";
            this.Címzett.Size = new System.Drawing.Size(403, 26);
            this.Címzett.TabIndex = 73;
            // 
            // Label33
            // 
            this.Label33.AutoSize = true;
            this.Label33.Location = new System.Drawing.Point(8, 140);
            this.Label33.Name = "Label33";
            this.Label33.Size = new System.Drawing.Size(52, 20);
            this.Label33.TabIndex = 72;
            this.Label33.Text = "Tárgy:";
            // 
            // Label32
            // 
            this.Label32.AutoSize = true;
            this.Label32.Location = new System.Drawing.Point(8, 105);
            this.Label32.Name = "Label32";
            this.Label32.Size = new System.Drawing.Size(69, 20);
            this.Label32.TabIndex = 71;
            this.Label32.Text = "Másolat:";
            // 
            // Label31
            // 
            this.Label31.AutoSize = true;
            this.Label31.Location = new System.Drawing.Point(8, 73);
            this.Label31.Name = "Label31";
            this.Label31.Size = new System.Drawing.Size(67, 20);
            this.Label31.TabIndex = 70;
            this.Label31.Text = "Címzett:";
            // 
            // Label30
            // 
            this.Label30.AutoSize = true;
            this.Label30.Location = new System.Drawing.Point(8, 12);
            this.Label30.Name = "Label30";
            this.Label30.Size = new System.Drawing.Size(121, 20);
            this.Label30.TabIndex = 69;
            this.Label30.Text = "Pályaszám: ($$)";
            // 
            // Ciklus_Pályaszám
            // 
            this.Ciklus_Pályaszám.Location = new System.Drawing.Point(144, 6);
            this.Ciklus_Pályaszám.Name = "Ciklus_Pályaszám";
            this.Ciklus_Pályaszám.Size = new System.Drawing.Size(122, 26);
            this.Ciklus_Pályaszám.TabIndex = 0;
            // 
            // Email
            // 
            this.Email.BackgroundImage = global::Villamos.Properties.Resources.email;
            this.Email.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Email.Location = new System.Drawing.Point(556, 3);
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(45, 45);
            this.Email.TabIndex = 68;
            this.ToolTip1.SetToolTip(this.Email, "A címzetteknek elküldi a levelet");
            this.Email.UseVisualStyleBackColor = true;
            this.Email.Click += new System.EventHandler(this.Email_Click);
            // 
            // CiklusFrissít
            // 
            this.CiklusFrissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.CiklusFrissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CiklusFrissít.Location = new System.Drawing.Point(556, 51);
            this.CiklusFrissít.Name = "CiklusFrissít";
            this.CiklusFrissít.Size = new System.Drawing.Size(45, 45);
            this.CiklusFrissít.TabIndex = 67;
            this.ToolTip1.SetToolTip(this.CiklusFrissít, "Frissítit a táblázatot");
            this.CiklusFrissít.UseVisualStyleBackColor = true;
            this.CiklusFrissít.Click += new System.EventHandler(this.CiklusFrissít_Click);
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1172, 27);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(40, 40);
            this.BtnSúgó.TabIndex = 62;
            this.ToolTip1.SetToolTip(this.BtnSúgó, "Súgó");
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(350, 36);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(816, 21);
            this.Holtart.TabIndex = 67;
            this.Holtart.Visible = false;
            // 
            // Ablak_T5C5_Vizsgálat_ütemező
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Tan;
            this.ClientSize = new System.Drawing.Size(1218, 666);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.MenuStrip1);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.Fülek);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.MenuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_T5C5_Vizsgálat_ütemező";
            this.Text = "T5C5 Km alapú vezénylése és Hétvégi kiadás";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_T5C5_Vizsgálat_ütemező_FormClosed);
            this.Load += new System.EventHandler(this.Ablak_Vizsgálat_ütemező_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ablak_Vizsgálat_ütemező_KeyDown);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.MenuStrip1.ResumeLayout(false);
            this.MenuStrip1.PerformLayout();
            this.Fülek.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Vonal_tábla)).EndInit();
            this.TabPage3.ResumeLayout(false);
            this.TabPage4.ResumeLayout(false);
            this.TabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Keréktábla)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Vizs_tábla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal Panel Panel2;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal MenuStrip MenuStrip1;
        internal ToolStripMenuItem EszközökToolStripMenuItem;
        internal ToolStripMenuItem SzínválasztóToolStripMenuItem;
        internal ToolStripMenuItem SzínezToolStripMenuItem;
        internal ToolStripSeparator ToolStripSeparator1;
        internal ToolStripMenuItem KeresésCtrlFToolStripMenuItem;
        internal ToolStripMenuItem ListázásToolStripMenuItem;
        internal ToolStripMenuItem AktuálisSzerelvénySzerintiListaToolStripMenuItem;
        internal ToolStripMenuItem ElőírtSzerelvénySzerintiListaToolStripMenuItem;
        internal ToolStripSeparator ToolStripSeparator2;
        internal ToolStripMenuItem ExcelKimenetKészítéseToolStripMenuItem;
        internal TabControl Fülek;
        internal TabPage TabPage1;
        internal DataGridView Tábla;
        internal TabPage TabPage2;
        internal Label Label3;
        internal Label Label2;
        internal Label Label1;
        internal DataGridView Vonal_tábla;
        internal Button Command7_Rögzítés;
        internal Button Command8_Új;
        internal Button Command11_frissít;
        internal Button Command10_Listát_töröl;
        internal Button Command9_színkereső;
        internal TextBox Vonal_Vonal;
        internal TextBox Vonal_Id;
        internal TextBox Vonal_Mennyiség;
        internal Label Label4;
        internal Button Vonal_fel;
        internal TextBox Vonal_red;
        internal TextBox Vonal_green;
        internal TextBox Vonal_blue;
        internal ToolStripSeparator ToolStripSeparator3;
        internal ToolStripMenuItem BeosztásAdatokTörléseToolStripMenuItem;
        internal ToolStripSeparator ToolStripSeparator4;
        internal ToolStripMenuItem AktuálisSzerelvénySzerintVizsgálatToolStripMenuItem;
        internal TabPage TabPage3;
        internal Button Utasítás_tervezet;
        internal RichTextBox Txtírásimező;
        internal Button Btnrögzítés;
        internal ToolTip ToolTip1;
        internal Button Utasítás_törlés;
        internal TabPage TabPage4;
        internal Button CiklusFrissít;
        internal TextBox Ciklus_Pályaszám;
        internal TextBox Tárgy;
        internal TextBox Másolat;
        internal TextBox Címzett;
        internal Label Label33;
        internal Label Label32;
        internal Label Label31;
        internal Label Label30;
        internal Button Email;
        internal TextBox Befejezés;
        internal TextBox Tárgyalás;
        internal TextBox Bevezetés;
        internal Label Label34;
        internal Label Label35;
        internal Label Label36;
        internal Button Ciklus_Mentés;
        internal TextBox J_tőlFutott;
        internal Label Label37;
        internal TextBox Következő_vizsgálat;
        internal Label Label38;
        internal TextBox Kért_vizsgálat;
        internal Label Label39;
        internal DataGridView Keréktábla;
        internal DataGridView Vizs_tábla;
        internal Button BtnSúgó;
        internal TextBox CiklusTípus;
        internal Label label5;
        private V_MindenEgyéb.MyProgressbar Holtart;
        internal TextBox Felmentés_Id;
    }
}