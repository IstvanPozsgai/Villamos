using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{ 
    public partial class Ablak_épülettakarítás_alap : Form
    {
         // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components!=  null)
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_épülettakarítás_alap));
            this.Panel4 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Holtart = new System.Windows.Forms.ProgressBar();
            this.LapFülek = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Sorszám = new System.Windows.Forms.TextBox();
            this.Label14 = new System.Windows.Forms.Label();
            this.Osztálynév = new System.Windows.Forms.TextBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.E3ár = new System.Windows.Forms.TextBox();
            this.E1ár = new System.Windows.Forms.TextBox();
            this.E2ár = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Oszály_Excel = new System.Windows.Forms.Button();
            this.Osztály_feljebb = new System.Windows.Forms.Button();
            this.Adatok_beolvasása = new System.Windows.Forms.Button();
            this.Beviteli_táblakészítés = new System.Windows.Forms.Button();
            this.Osztály_rögzít = new System.Windows.Forms.Button();
            this.Osztály_Új = new System.Windows.Forms.Button();
            this.Osztály_törlés = new System.Windows.Forms.Button();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.Helység_excel = new System.Windows.Forms.Button();
            this.Helység_feljebb = new System.Windows.Forms.Button();
            this.Helység_frissít = new System.Windows.Forms.Button();
            this.Tábla2 = new System.Windows.Forms.DataGridView();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.Részletes_feljebb = new System.Windows.Forms.Button();
            this.Combo1 = new System.Windows.Forms.ComboBox();
            this.Check1 = new System.Windows.Forms.CheckBox();
            this.Label15 = new System.Windows.Forms.Label();
            this.Kapcsolthelység = new System.Windows.Forms.TextBox();
            this.Label16 = new System.Windows.Forms.Label();
            this.Hellenőrtelefon = new System.Windows.Forms.TextBox();
            this.Label17 = new System.Windows.Forms.Label();
            this.Hellenőremail = new System.Windows.Forms.TextBox();
            this.Label18 = new System.Windows.Forms.Label();
            this.Hellenőrneve = new System.Windows.Forms.TextBox();
            this.Label19 = new System.Windows.Forms.Label();
            this.Hvégez = new System.Windows.Forms.TextBox();
            this.Label20 = new System.Windows.Forms.Label();
            this.Hkezd = new System.Windows.Forms.TextBox();
            this.Label10 = new System.Windows.Forms.Label();
            this.He3évdb = new System.Windows.Forms.TextBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.He2évdb = new System.Windows.Forms.TextBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.He1évdb = new System.Windows.Forms.TextBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Hhelyiségkód = new System.Windows.Forms.TextBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.Hméret = new System.Windows.Forms.TextBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Hmegnevezés = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Hsorszám = new System.Windows.Forms.TextBox();
            this.Részletes_Kuka = new System.Windows.Forms.Button();
            this.Részletes_Új = new System.Windows.Forms.Button();
            this.Részletes_rögzít = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.Opció_Id = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.Opció_Frissít = new System.Windows.Forms.Button();
            this.Opció_Tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.Opció_Új = new System.Windows.Forms.Button();
            this.Opció_Excel = new System.Windows.Forms.Button();
            this.Opció_OK = new System.Windows.Forms.Button();
            this.Opció_Vég = new System.Windows.Forms.DateTimePicker();
            this.label25 = new System.Windows.Forms.Label();
            this.Opció_Ár = new System.Windows.Forms.TextBox();
            this.Opció_Mennyisége = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.Opció_Kezdet = new System.Windows.Forms.DateTimePicker();
            this.Opció_Megnevezés = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.Btn_súgó = new System.Windows.Forms.Button();
            this.Tábla1 = new Zuby.ADGV.AdvancedDataGridView();
            this.Panel4.SuspendLayout();
            this.LapFülek.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla2)).BeginInit();
            this.TabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Opció_Tábla)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla1)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel4
            // 
            this.Panel4.Controls.Add(this.Cmbtelephely);
            this.Panel4.Controls.Add(this.Label5);
            this.Panel4.Location = new System.Drawing.Point(5, 5);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(373, 33);
            this.Panel4.TabIndex = 144;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(175, 2);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(12, 5);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(145, 20);
            this.Label5.TabIndex = 17;
            this.Label5.Text = "Telephelyi beállítás:";
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(391, 5);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(759, 28);
            this.Holtart.TabIndex = 143;
            this.Holtart.Visible = false;
            // 
            // LapFülek
            // 
            this.LapFülek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LapFülek.Controls.Add(this.TabPage1);
            this.LapFülek.Controls.Add(this.TabPage2);
            this.LapFülek.Controls.Add(this.TabPage3);
            this.LapFülek.Controls.Add(this.tabPage4);
            this.LapFülek.Location = new System.Drawing.Point(5, 50);
            this.LapFülek.Name = "LapFülek";
            this.LapFülek.Padding = new System.Drawing.Point(16, 3);
            this.LapFülek.SelectedIndex = 0;
            this.LapFülek.Size = new System.Drawing.Size(1191, 565);
            this.LapFülek.TabIndex = 145;
            this.LapFülek.SelectedIndexChanged += new System.EventHandler(this.Fülek_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.BurlyWood;
            this.TabPage1.Controls.Add(this.Tábla1);
            this.TabPage1.Controls.Add(this.tableLayoutPanel1);
            this.TabPage1.Controls.Add(this.Oszály_Excel);
            this.TabPage1.Controls.Add(this.Osztály_feljebb);
            this.TabPage1.Controls.Add(this.Adatok_beolvasása);
            this.TabPage1.Controls.Add(this.Beviteli_táblakészítés);
            this.TabPage1.Controls.Add(this.Osztály_rögzít);
            this.TabPage1.Controls.Add(this.Osztály_Új);
            this.TabPage1.Controls.Add(this.Osztály_törlés);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1183, 532);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Takarítási osztályok";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.Sorszám, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.Label14, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Osztálynév, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.Label9, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Label2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.E3ár, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.E1ár, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.E2ár, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.Label3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.Label1, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(869, 168);
            this.tableLayoutPanel1.TabIndex = 204;
            // 
            // Sorszám
            // 
            this.Sorszám.Enabled = false;
            this.Sorszám.Location = new System.Drawing.Point(143, 3);
            this.Sorszám.Name = "Sorszám";
            this.Sorszám.Size = new System.Drawing.Size(203, 26);
            this.Sorszám.TabIndex = 199;
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.BackColor = System.Drawing.Color.Silver;
            this.Label14.Location = new System.Drawing.Point(3, 0);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(76, 20);
            this.Label14.TabIndex = 200;
            this.Label14.Text = "Sorszám:";
            // 
            // Osztálynév
            // 
            this.Osztálynév.Location = new System.Drawing.Point(143, 35);
            this.Osztálynév.MaxLength = 50;
            this.Osztálynév.Name = "Osztálynév";
            this.Osztálynév.Size = new System.Drawing.Size(515, 26);
            this.Osztálynév.TabIndex = 189;
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.BackColor = System.Drawing.Color.Silver;
            this.Label9.Location = new System.Drawing.Point(3, 32);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(134, 20);
            this.Label9.TabIndex = 190;
            this.Label9.Text = "Takarítási osztály:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Silver;
            this.Label2.Location = new System.Drawing.Point(3, 96);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(116, 20);
            this.Label2.TabIndex = 194;
            this.Label2.Text = "E2 takarítás ár:";
            // 
            // E3ár
            // 
            this.E3ár.Location = new System.Drawing.Point(143, 131);
            this.E3ár.MaxLength = 50;
            this.E3ár.Name = "E3ár";
            this.E3ár.Size = new System.Drawing.Size(203, 26);
            this.E3ár.TabIndex = 195;
            // 
            // E1ár
            // 
            this.E1ár.Location = new System.Drawing.Point(143, 67);
            this.E1ár.MaxLength = 50;
            this.E1ár.Name = "E1ár";
            this.E1ár.Size = new System.Drawing.Size(203, 26);
            this.E1ár.TabIndex = 191;
            // 
            // E2ár
            // 
            this.E2ár.Location = new System.Drawing.Point(143, 99);
            this.E2ár.MaxLength = 50;
            this.E2ár.Name = "E2ár";
            this.E2ár.Size = new System.Drawing.Size(203, 26);
            this.E2ár.TabIndex = 193;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.Silver;
            this.Label3.Location = new System.Drawing.Point(3, 128);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(116, 20);
            this.Label3.TabIndex = 196;
            this.Label3.Text = "E3 takarítás ár:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Silver;
            this.Label1.Location = new System.Drawing.Point(3, 64);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(116, 20);
            this.Label1.TabIndex = 192;
            this.Label1.Text = "E1 takarítás ár:";
            // 
            // Oszály_Excel
            // 
            this.Oszály_Excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Oszály_Excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Oszály_Excel.Location = new System.Drawing.Point(884, 129);
            this.Oszály_Excel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Oszály_Excel.Name = "Oszály_Excel";
            this.Oszály_Excel.Size = new System.Drawing.Size(45, 45);
            this.Oszály_Excel.TabIndex = 201;
            this.Oszály_Excel.UseVisualStyleBackColor = true;
            this.Oszály_Excel.Click += new System.EventHandler(this.Oszály_Excel_Click);
            // 
            // Osztály_feljebb
            // 
            this.Osztály_feljebb.BackgroundImage = global::Villamos.Properties.Resources.Button_Upload_01;
            this.Osztály_feljebb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Osztály_feljebb.Location = new System.Drawing.Point(1037, 129);
            this.Osztály_feljebb.Name = "Osztály_feljebb";
            this.Osztály_feljebb.Size = new System.Drawing.Size(45, 45);
            this.Osztály_feljebb.TabIndex = 198;
            this.Osztály_feljebb.UseVisualStyleBackColor = true;
            this.Osztály_feljebb.Click += new System.EventHandler(this.Felljebb_Click);
            // 
            // Adatok_beolvasása
            // 
            this.Adatok_beolvasása.BackgroundImage = global::Villamos.Properties.Resources.Custom_Icon_Design_Flatastic_1_Import;
            this.Adatok_beolvasása.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Adatok_beolvasása.Location = new System.Drawing.Point(934, 73);
            this.Adatok_beolvasása.Name = "Adatok_beolvasása";
            this.Adatok_beolvasása.Size = new System.Drawing.Size(45, 45);
            this.Adatok_beolvasása.TabIndex = 202;
            this.Adatok_beolvasása.UseVisualStyleBackColor = true;
            this.Adatok_beolvasása.Click += new System.EventHandler(this.Adatok_beolvasása_Click);
            // 
            // Beviteli_táblakészítés
            // 
            this.Beviteli_táblakészítés.BackgroundImage = global::Villamos.Properties.Resources.Custom_Icon_Design_Flatastic_1_Export;
            this.Beviteli_táblakészítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Beviteli_táblakészítés.Location = new System.Drawing.Point(883, 73);
            this.Beviteli_táblakészítés.Name = "Beviteli_táblakészítés";
            this.Beviteli_táblakészítés.Size = new System.Drawing.Size(45, 45);
            this.Beviteli_táblakészítés.TabIndex = 203;
            this.Beviteli_táblakészítés.UseVisualStyleBackColor = true;
            this.Beviteli_táblakészítés.Click += new System.EventHandler(this.Beviteli_táblakészítés_Click);
            // 
            // Osztály_rögzít
            // 
            this.Osztály_rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Osztály_rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Osztály_rögzít.Location = new System.Drawing.Point(883, 9);
            this.Osztály_rögzít.Name = "Osztály_rögzít";
            this.Osztály_rögzít.Size = new System.Drawing.Size(45, 45);
            this.Osztály_rögzít.TabIndex = 187;
            this.Osztály_rögzít.UseVisualStyleBackColor = true;
            this.Osztály_rögzít.Click += new System.EventHandler(this.Osztály_rögzít_Click);
            // 
            // Osztály_Új
            // 
            this.Osztály_Új.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Osztály_Új.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Osztály_Új.Location = new System.Drawing.Point(986, 129);
            this.Osztály_Új.Name = "Osztály_Új";
            this.Osztály_Új.Size = new System.Drawing.Size(45, 45);
            this.Osztály_Új.TabIndex = 188;
            this.Osztály_Új.UseVisualStyleBackColor = true;
            this.Osztály_Új.Click += new System.EventHandler(this.Osztály_Új_Click);
            // 
            // Osztály_törlés
            // 
            this.Osztály_törlés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Osztály_törlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Osztály_törlés.Location = new System.Drawing.Point(935, 129);
            this.Osztály_törlés.Name = "Osztály_törlés";
            this.Osztály_törlés.Size = new System.Drawing.Size(45, 45);
            this.Osztály_törlés.TabIndex = 197;
            this.Osztály_törlés.UseVisualStyleBackColor = true;
            this.Osztály_törlés.Click += new System.EventHandler(this.Osztálytörlés_Click);
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.SandyBrown;
            this.TabPage2.Controls.Add(this.Helység_excel);
            this.TabPage2.Controls.Add(this.Helység_feljebb);
            this.TabPage2.Controls.Add(this.Helység_frissít);
            this.TabPage2.Controls.Add(this.Tábla2);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(1183, 532);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Helység adatok listázása";
            // 
            // Helység_excel
            // 
            this.Helység_excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Helység_excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Helység_excel.Location = new System.Drawing.Point(112, 10);
            this.Helység_excel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Helység_excel.Name = "Helység_excel";
            this.Helység_excel.Size = new System.Drawing.Size(45, 45);
            this.Helység_excel.TabIndex = 201;
            this.Helység_excel.UseVisualStyleBackColor = true;
            this.Helység_excel.Click += new System.EventHandler(this.Helység_excel_Click);
            // 
            // Helység_feljebb
            // 
            this.Helység_feljebb.BackgroundImage = global::Villamos.Properties.Resources.Button_Upload_01;
            this.Helység_feljebb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Helység_feljebb.Location = new System.Drawing.Point(61, 10);
            this.Helység_feljebb.Name = "Helység_feljebb";
            this.Helység_feljebb.Size = new System.Drawing.Size(45, 45);
            this.Helység_feljebb.TabIndex = 200;
            this.Helység_feljebb.UseVisualStyleBackColor = true;
            this.Helység_feljebb.Click += new System.EventHandler(this.Helység_feljebb_Click);
            // 
            // Helység_frissít
            // 
            this.Helység_frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Helység_frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Helység_frissít.Location = new System.Drawing.Point(10, 10);
            this.Helység_frissít.Name = "Helység_frissít";
            this.Helység_frissít.Size = new System.Drawing.Size(45, 45);
            this.Helység_frissít.TabIndex = 199;
            this.Helység_frissít.UseVisualStyleBackColor = true;
            this.Helység_frissít.Click += new System.EventHandler(this.Helység_frissít_Click);
            // 
            // Tábla2
            // 
            this.Tábla2.AllowUserToAddRows = false;
            this.Tábla2.AllowUserToDeleteRows = false;
            this.Tábla2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla2.Location = new System.Drawing.Point(6, 61);
            this.Tábla2.Name = "Tábla2";
            this.Tábla2.RowHeadersVisible = false;
            this.Tábla2.Size = new System.Drawing.Size(1171, 465);
            this.Tábla2.TabIndex = 187;
            this.Tábla2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla2_CellClick);
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.Teal;
            this.TabPage3.Controls.Add(this.Részletes_feljebb);
            this.TabPage3.Controls.Add(this.Combo1);
            this.TabPage3.Controls.Add(this.Check1);
            this.TabPage3.Controls.Add(this.Label15);
            this.TabPage3.Controls.Add(this.Kapcsolthelység);
            this.TabPage3.Controls.Add(this.Label16);
            this.TabPage3.Controls.Add(this.Hellenőrtelefon);
            this.TabPage3.Controls.Add(this.Label17);
            this.TabPage3.Controls.Add(this.Hellenőremail);
            this.TabPage3.Controls.Add(this.Label18);
            this.TabPage3.Controls.Add(this.Hellenőrneve);
            this.TabPage3.Controls.Add(this.Label19);
            this.TabPage3.Controls.Add(this.Hvégez);
            this.TabPage3.Controls.Add(this.Label20);
            this.TabPage3.Controls.Add(this.Hkezd);
            this.TabPage3.Controls.Add(this.Label10);
            this.TabPage3.Controls.Add(this.He3évdb);
            this.TabPage3.Controls.Add(this.Label11);
            this.TabPage3.Controls.Add(this.He2évdb);
            this.TabPage3.Controls.Add(this.Label12);
            this.TabPage3.Controls.Add(this.He1évdb);
            this.TabPage3.Controls.Add(this.Label13);
            this.TabPage3.Controls.Add(this.Hhelyiségkód);
            this.TabPage3.Controls.Add(this.Label8);
            this.TabPage3.Controls.Add(this.Hméret);
            this.TabPage3.Controls.Add(this.Label7);
            this.TabPage3.Controls.Add(this.Label6);
            this.TabPage3.Controls.Add(this.Hmegnevezés);
            this.TabPage3.Controls.Add(this.Label4);
            this.TabPage3.Controls.Add(this.Hsorszám);
            this.TabPage3.Controls.Add(this.Részletes_Kuka);
            this.TabPage3.Controls.Add(this.Részletes_Új);
            this.TabPage3.Controls.Add(this.Részletes_rögzít);
            this.TabPage3.Location = new System.Drawing.Point(4, 29);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(1183, 532);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Helység adatok módosítása";
            // 
            // Részletes_feljebb
            // 
            this.Részletes_feljebb.BackgroundImage = global::Villamos.Properties.Resources.Button_Upload_01;
            this.Részletes_feljebb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Részletes_feljebb.Location = new System.Drawing.Point(835, 135);
            this.Részletes_feljebb.Name = "Részletes_feljebb";
            this.Részletes_feljebb.Size = new System.Drawing.Size(45, 45);
            this.Részletes_feljebb.TabIndex = 232;
            this.Részletes_feljebb.UseVisualStyleBackColor = true;
            this.Részletes_feljebb.Click += new System.EventHandler(this.Részletes_feljebb_Click);
            // 
            // Combo1
            // 
            this.Combo1.FormattingEnabled = true;
            this.Combo1.Location = new System.Drawing.Point(170, 85);
            this.Combo1.Name = "Combo1";
            this.Combo1.Size = new System.Drawing.Size(560, 28);
            this.Combo1.TabIndex = 231;
            // 
            // Check1
            // 
            this.Check1.AutoSize = true;
            this.Check1.BackColor = System.Drawing.Color.PaleTurquoise;
            this.Check1.Location = new System.Drawing.Point(170, 505);
            this.Check1.Name = "Check1";
            this.Check1.Size = new System.Drawing.Size(100, 24);
            this.Check1.TabIndex = 229;
            this.Check1.Text = "Szemetes";
            this.Check1.UseVisualStyleBackColor = false;
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.BackColor = System.Drawing.Color.Silver;
            this.Label15.Location = new System.Drawing.Point(6, 476);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(158, 20);
            this.Label15.TabIndex = 228;
            this.Label15.Text = "Helység összevonás:";
            // 
            // Kapcsolthelység
            // 
            this.Kapcsolthelység.Location = new System.Drawing.Point(170, 470);
            this.Kapcsolthelység.MaxLength = 50;
            this.Kapcsolthelység.Name = "Kapcsolthelység";
            this.Kapcsolthelység.Size = new System.Drawing.Size(393, 26);
            this.Kapcsolthelység.TabIndex = 227;
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.BackColor = System.Drawing.Color.Silver;
            this.Label16.Location = new System.Drawing.Point(6, 441);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(153, 20);
            this.Label16.TabIndex = 226;
            this.Label16.Text = "Ellenőr telefonszám:";
            // 
            // Hellenőrtelefon
            // 
            this.Hellenőrtelefon.Location = new System.Drawing.Point(170, 435);
            this.Hellenőrtelefon.MaxLength = 50;
            this.Hellenőrtelefon.Name = "Hellenőrtelefon";
            this.Hellenőrtelefon.Size = new System.Drawing.Size(393, 26);
            this.Hellenőrtelefon.TabIndex = 225;
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.BackColor = System.Drawing.Color.Silver;
            this.Label17.Location = new System.Drawing.Point(6, 406);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(108, 20);
            this.Label17.TabIndex = 224;
            this.Label17.Text = "Ellenőr e-mail:";
            // 
            // Hellenőremail
            // 
            this.Hellenőremail.Location = new System.Drawing.Point(170, 400);
            this.Hellenőremail.MaxLength = 50;
            this.Hellenőremail.Name = "Hellenőremail";
            this.Hellenőremail.Size = new System.Drawing.Size(393, 26);
            this.Hellenőremail.TabIndex = 223;
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.BackColor = System.Drawing.Color.Silver;
            this.Label18.Location = new System.Drawing.Point(6, 371);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(100, 20);
            this.Label18.TabIndex = 222;
            this.Label18.Text = "Ellenőr neve:";
            // 
            // Hellenőrneve
            // 
            this.Hellenőrneve.Location = new System.Drawing.Point(170, 365);
            this.Hellenőrneve.MaxLength = 50;
            this.Hellenőrneve.Name = "Hellenőrneve";
            this.Hellenőrneve.Size = new System.Drawing.Size(393, 26);
            this.Hellenőrneve.TabIndex = 221;
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.BackColor = System.Drawing.Color.Silver;
            this.Label19.Location = new System.Drawing.Point(6, 336);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(59, 20);
            this.Label19.TabIndex = 220;
            this.Label19.Text = "Végez:";
            // 
            // Hvégez
            // 
            this.Hvégez.Location = new System.Drawing.Point(170, 330);
            this.Hvégez.MaxLength = 50;
            this.Hvégez.Name = "Hvégez";
            this.Hvégez.Size = new System.Drawing.Size(393, 26);
            this.Hvégez.TabIndex = 219;
            // 
            // Label20
            // 
            this.Label20.AutoSize = true;
            this.Label20.BackColor = System.Drawing.Color.Silver;
            this.Label20.Location = new System.Drawing.Point(6, 301);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(49, 20);
            this.Label20.TabIndex = 218;
            this.Label20.Text = "Kezd:";
            // 
            // Hkezd
            // 
            this.Hkezd.Location = new System.Drawing.Point(170, 295);
            this.Hkezd.MaxLength = 50;
            this.Hkezd.Name = "Hkezd";
            this.Hkezd.Size = new System.Drawing.Size(393, 26);
            this.Hkezd.TabIndex = 217;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.BackColor = System.Drawing.Color.Silver;
            this.Label10.Location = new System.Drawing.Point(6, 266);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(150, 20);
            this.Label10.TabIndex = 216;
            this.Label10.Text = "E3 éves mennyiség:";
            // 
            // He3évdb
            // 
            this.He3évdb.Location = new System.Drawing.Point(170, 260);
            this.He3évdb.MaxLength = 50;
            this.He3évdb.Name = "He3évdb";
            this.He3évdb.Size = new System.Drawing.Size(393, 26);
            this.He3évdb.TabIndex = 215;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.BackColor = System.Drawing.Color.Silver;
            this.Label11.Location = new System.Drawing.Point(8, 230);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(150, 20);
            this.Label11.TabIndex = 214;
            this.Label11.Text = "E2 éves mennyiség:";
            // 
            // He2évdb
            // 
            this.He2évdb.Location = new System.Drawing.Point(170, 225);
            this.He2évdb.MaxLength = 50;
            this.He2évdb.Name = "He2évdb";
            this.He2évdb.Size = new System.Drawing.Size(393, 26);
            this.He2évdb.TabIndex = 213;
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.BackColor = System.Drawing.Color.Silver;
            this.Label12.Location = new System.Drawing.Point(8, 195);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(150, 20);
            this.Label12.TabIndex = 212;
            this.Label12.Text = "E1 éves mennyiség:";
            // 
            // He1évdb
            // 
            this.He1évdb.Location = new System.Drawing.Point(170, 190);
            this.He1évdb.MaxLength = 50;
            this.He1évdb.Name = "He1évdb";
            this.He1évdb.Size = new System.Drawing.Size(393, 26);
            this.He1évdb.TabIndex = 211;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.BackColor = System.Drawing.Color.Silver;
            this.Label13.Location = new System.Drawing.Point(8, 160);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(99, 20);
            this.Label13.TabIndex = 210;
            this.Label13.Text = "Helyiségkód:";
            // 
            // Hhelyiségkód
            // 
            this.Hhelyiségkód.Location = new System.Drawing.Point(170, 155);
            this.Hhelyiségkód.MaxLength = 50;
            this.Hhelyiségkód.Name = "Hhelyiségkód";
            this.Hhelyiségkód.Size = new System.Drawing.Size(393, 26);
            this.Hhelyiségkód.TabIndex = 209;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.BackColor = System.Drawing.Color.Silver;
            this.Label8.Location = new System.Drawing.Point(8, 128);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(54, 20);
            this.Label8.TabIndex = 208;
            this.Label8.Text = "Méret:";
            // 
            // Hméret
            // 
            this.Hméret.Location = new System.Drawing.Point(170, 120);
            this.Hméret.MaxLength = 50;
            this.Hméret.Name = "Hméret";
            this.Hméret.Size = new System.Drawing.Size(393, 26);
            this.Hméret.TabIndex = 207;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.BackColor = System.Drawing.Color.Silver;
            this.Label7.Location = new System.Drawing.Point(8, 96);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(65, 20);
            this.Label7.TabIndex = 206;
            this.Label7.Text = "Osztály:";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.BackColor = System.Drawing.Color.Silver;
            this.Label6.Location = new System.Drawing.Point(8, 61);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(103, 20);
            this.Label6.TabIndex = 204;
            this.Label6.Text = "Megnevezés:";
            // 
            // Hmegnevezés
            // 
            this.Hmegnevezés.Location = new System.Drawing.Point(170, 50);
            this.Hmegnevezés.MaxLength = 50;
            this.Hmegnevezés.Name = "Hmegnevezés";
            this.Hmegnevezés.Size = new System.Drawing.Size(393, 26);
            this.Hmegnevezés.TabIndex = 203;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.BackColor = System.Drawing.Color.Silver;
            this.Label4.Location = new System.Drawing.Point(8, 26);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(76, 20);
            this.Label4.TabIndex = 192;
            this.Label4.Text = "Sorszám:";
            // 
            // Hsorszám
            // 
            this.Hsorszám.Location = new System.Drawing.Point(171, 15);
            this.Hsorszám.MaxLength = 50;
            this.Hsorszám.Name = "Hsorszám";
            this.Hsorszám.Size = new System.Drawing.Size(393, 26);
            this.Hsorszám.TabIndex = 191;
            // 
            // Részletes_Kuka
            // 
            this.Részletes_Kuka.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Részletes_Kuka.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Részletes_Kuka.Location = new System.Drawing.Point(733, 136);
            this.Részletes_Kuka.Name = "Részletes_Kuka";
            this.Részletes_Kuka.Size = new System.Drawing.Size(45, 45);
            this.Részletes_Kuka.TabIndex = 201;
            this.Részletes_Kuka.UseVisualStyleBackColor = true;
            this.Részletes_Kuka.Click += new System.EventHandler(this.Részletes_Kuka_Click);
            // 
            // Részletes_Új
            // 
            this.Részletes_Új.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Részletes_Új.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Részletes_Új.Location = new System.Drawing.Point(784, 136);
            this.Részletes_Új.Name = "Részletes_Új";
            this.Részletes_Új.Size = new System.Drawing.Size(45, 45);
            this.Részletes_Új.TabIndex = 200;
            this.Részletes_Új.UseVisualStyleBackColor = true;
            this.Részletes_Új.Click += new System.EventHandler(this.Részletes_Új_Click);
            // 
            // Részletes_rögzít
            // 
            this.Részletes_rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Részletes_rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Részletes_rögzít.Location = new System.Drawing.Point(733, 14);
            this.Részletes_rögzít.Name = "Részletes_rögzít";
            this.Részletes_rögzít.Size = new System.Drawing.Size(45, 45);
            this.Részletes_rögzít.TabIndex = 199;
            this.Részletes_rögzít.UseVisualStyleBackColor = true;
            this.Részletes_rögzít.Click += new System.EventHandler(this.Részletes_rögzít_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.Teal;
            this.tabPage4.Controls.Add(this.Opció_Id);
            this.tabPage4.Controls.Add(this.label26);
            this.tabPage4.Controls.Add(this.Opció_Frissít);
            this.tabPage4.Controls.Add(this.Opció_Tábla);
            this.tabPage4.Controls.Add(this.Opció_Új);
            this.tabPage4.Controls.Add(this.Opció_Excel);
            this.tabPage4.Controls.Add(this.Opció_OK);
            this.tabPage4.Controls.Add(this.Opció_Vég);
            this.tabPage4.Controls.Add(this.label25);
            this.tabPage4.Controls.Add(this.Opció_Ár);
            this.tabPage4.Controls.Add(this.Opció_Mennyisége);
            this.tabPage4.Controls.Add(this.label24);
            this.tabPage4.Controls.Add(this.label23);
            this.tabPage4.Controls.Add(this.label22);
            this.tabPage4.Controls.Add(this.Opció_Kezdet);
            this.tabPage4.Controls.Add(this.Opció_Megnevezés);
            this.tabPage4.Controls.Add(this.label21);
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1183, 532);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Opcionális";
            // 
            // Opció_Id
            // 
            this.Opció_Id.Enabled = false;
            this.Opció_Id.Location = new System.Drawing.Point(194, 8);
            this.Opció_Id.Name = "Opció_Id";
            this.Opció_Id.Size = new System.Drawing.Size(104, 26);
            this.Opció_Id.TabIndex = 0;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(8, 8);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(72, 20);
            this.label26.TabIndex = 206;
            this.label26.Text = "Sorszám";
            // 
            // Opció_Frissít
            // 
            this.Opció_Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Opció_Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Opció_Frissít.Location = new System.Drawing.Point(911, 84);
            this.Opció_Frissít.Name = "Opció_Frissít";
            this.Opció_Frissít.Size = new System.Drawing.Size(45, 45);
            this.Opció_Frissít.TabIndex = 8;
            this.Opció_Frissít.UseVisualStyleBackColor = true;
            this.Opció_Frissít.Click += new System.EventHandler(this.Opció_Frissít_Click);
            // 
            // Opció_Tábla
            // 
            this.Opció_Tábla.AllowUserToAddRows = false;
            this.Opció_Tábla.AllowUserToDeleteRows = false;
            this.Opció_Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Opció_Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Opció_Tábla.FilterAndSortEnabled = true;
            this.Opció_Tábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Opció_Tábla.Location = new System.Drawing.Point(6, 211);
            this.Opció_Tábla.MaxFilterButtonImageHeight = 23;
            this.Opció_Tábla.Name = "Opció_Tábla";
            this.Opció_Tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Opció_Tábla.RowHeadersVisible = false;
            this.Opció_Tábla.Size = new System.Drawing.Size(1171, 318);
            this.Opció_Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Opció_Tábla.TabIndex = 204;
            this.Opció_Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Opció_Tábla_CellClick);
            // 
            // Opció_Új
            // 
            this.Opció_Új.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Opció_Új.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Opció_Új.Location = new System.Drawing.Point(962, 83);
            this.Opció_Új.Name = "Opció_Új";
            this.Opció_Új.Size = new System.Drawing.Size(45, 45);
            this.Opció_Új.TabIndex = 7;
            this.Opció_Új.UseVisualStyleBackColor = true;
            this.Opció_Új.Click += new System.EventHandler(this.Opció_Új_Click);
            // 
            // Opció_Excel
            // 
            this.Opció_Excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Opció_Excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Opció_Excel.Location = new System.Drawing.Point(1013, 83);
            this.Opció_Excel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Opció_Excel.Name = "Opció_Excel";
            this.Opció_Excel.Size = new System.Drawing.Size(45, 45);
            this.Opció_Excel.TabIndex = 9;
            this.Opció_Excel.UseVisualStyleBackColor = true;
            this.Opció_Excel.Click += new System.EventHandler(this.Opció_Excel_Click);
            // 
            // Opció_OK
            // 
            this.Opció_OK.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Opció_OK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Opció_OK.Location = new System.Drawing.Point(962, 20);
            this.Opció_OK.Name = "Opció_OK";
            this.Opció_OK.Size = new System.Drawing.Size(45, 45);
            this.Opció_OK.TabIndex = 6;
            this.Opció_OK.UseVisualStyleBackColor = true;
            this.Opció_OK.Click += new System.EventHandler(this.Opció_OK_Click);
            // 
            // Opció_Vég
            // 
            this.Opció_Vég.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Opció_Vég.Location = new System.Drawing.Point(194, 179);
            this.Opció_Vég.Name = "Opció_Vég";
            this.Opció_Vég.Size = new System.Drawing.Size(104, 26);
            this.Opció_Vég.TabIndex = 5;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(8, 179);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(142, 20);
            this.label25.TabIndex = 9;
            this.label25.Text = "Érvényesség vége:";
            // 
            // Opció_Ár
            // 
            this.Opció_Ár.Location = new System.Drawing.Point(194, 104);
            this.Opció_Ár.Name = "Opció_Ár";
            this.Opció_Ár.Size = new System.Drawing.Size(104, 26);
            this.Opció_Ár.TabIndex = 3;
            // 
            // Opció_Mennyisége
            // 
            this.Opció_Mennyisége.Location = new System.Drawing.Point(194, 72);
            this.Opció_Mennyisége.MaxLength = 10;
            this.Opció_Mennyisége.Name = "Opció_Mennyisége";
            this.Opció_Mennyisége.Size = new System.Drawing.Size(104, 26);
            this.Opció_Mennyisége.TabIndex = 2;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(8, 83);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(144, 20);
            this.label24.TabIndex = 6;
            this.label24.Text = "Mennyiség egység:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(8, 115);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(84, 20);
            this.label23.TabIndex = 5;
            this.label23.Text = "Egység ár:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(8, 144);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(165, 20);
            this.label22.TabIndex = 4;
            this.label22.Text = "Érvényesség kezdete:";
            // 
            // Opció_Kezdet
            // 
            this.Opció_Kezdet.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Opció_Kezdet.Location = new System.Drawing.Point(194, 139);
            this.Opció_Kezdet.Name = "Opció_Kezdet";
            this.Opció_Kezdet.Size = new System.Drawing.Size(104, 26);
            this.Opció_Kezdet.TabIndex = 4;
            // 
            // Opció_Megnevezés
            // 
            this.Opció_Megnevezés.Location = new System.Drawing.Point(194, 40);
            this.Opció_Megnevezés.MaxLength = 250;
            this.Opció_Megnevezés.Name = "Opció_Megnevezés";
            this.Opció_Megnevezés.Size = new System.Drawing.Size(754, 26);
            this.Opció_Megnevezés.TabIndex = 1;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(8, 46);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(103, 20);
            this.label21.TabIndex = 0;
            this.label21.Text = "Megnevezés:";
            // 
            // Btn_súgó
            // 
            this.Btn_súgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_súgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.Btn_súgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_súgó.Location = new System.Drawing.Point(1156, 5);
            this.Btn_súgó.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_súgó.Name = "Btn_súgó";
            this.Btn_súgó.Size = new System.Drawing.Size(40, 40);
            this.Btn_súgó.TabIndex = 142;
            this.Btn_súgó.UseVisualStyleBackColor = true;
            this.Btn_súgó.Click += new System.EventHandler(this.Btn_súgó_Click);
            // 
            // Tábla1
            // 
            this.Tábla1.AllowUserToAddRows = false;
            this.Tábla1.AllowUserToDeleteRows = false;
            this.Tábla1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla1.FilterAndSortEnabled = true;
            this.Tábla1.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla1.Location = new System.Drawing.Point(8, 180);
            this.Tábla1.MaxFilterButtonImageHeight = 23;
            this.Tábla1.Name = "Tábla1";
            this.Tábla1.ReadOnly = true;
            this.Tábla1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tábla1.Size = new System.Drawing.Size(1169, 346);
            this.Tábla1.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla1.TabIndex = 221;
            this.Tábla1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla1_CellClick);
            // 
            // Ablak_épülettakarítás_alap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SandyBrown;
            this.ClientSize = new System.Drawing.Size(1200, 627);
            this.Controls.Add(this.LapFülek);
            this.Controls.Add(this.Panel4);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Btn_súgó);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_épülettakarítás_alap";
            this.Text = "Épület takarítás törzsadatok";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_épülettakarítás_alap_Load);
            this.Panel4.ResumeLayout(false);
            this.Panel4.PerformLayout();
            this.LapFülek.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.TabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla2)).EndInit();
            this.TabPage3.ResumeLayout(false);
            this.TabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Opció_Tábla)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla1)).EndInit();
            this.ResumeLayout(false);

        }

        internal Panel Panel4;
        internal ComboBox Cmbtelephely;
        internal Label Label5;
        internal ProgressBar Holtart;
        internal Button Btn_súgó;
        internal TabControl LapFülek;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal TabPage TabPage3;
        internal Button Osztály_törlés;
        internal Label Label3;
        internal TextBox E3ár;
        internal Label Label2;
        internal TextBox E2ár;
        internal Label Label1;
        internal TextBox E1ár;
        internal Label Label9;
        internal TextBox Osztálynév;
        internal Button Osztály_rögzít;
        internal Button Helység_feljebb;
        internal Button Helység_frissít;
        internal DataGridView Tábla2;
        internal Label Label15;
        internal TextBox Kapcsolthelység;
        internal Label Label16;
        internal TextBox Hellenőrtelefon;
        internal Label Label17;
        internal TextBox Hellenőremail;
        internal Label Label18;
        internal TextBox Hellenőrneve;
        internal Label Label19;
        internal TextBox Hvégez;
        internal Label Label20;
        internal TextBox Hkezd;
        internal Label Label10;
        internal TextBox He3évdb;
        internal Label Label11;
        internal TextBox He2évdb;
        internal Label Label12;
        internal TextBox He1évdb;
        internal Label Label13;
        internal TextBox Hhelyiségkód;
        internal Label Label8;
        internal TextBox Hméret;
        internal Label Label7;
        internal Label Label6;
        internal TextBox Hmegnevezés;
        internal Button Részletes_Kuka;
        internal Button Részletes_Új;
        internal Button Részletes_rögzít;
        internal Label Label4;
        internal TextBox Hsorszám;
        internal CheckBox Check1;
        internal ComboBox Combo1;
        internal Label Label14;
        internal TextBox Sorszám;
        internal Button Oszály_Excel;
        internal Button Helység_excel;
        internal Button Részletes_feljebb;
        internal Button Adatok_beolvasása;
        internal Button Beviteli_táblakészítés;
        private TableLayoutPanel tableLayoutPanel1;
        internal Button Osztály_feljebb;
        internal Button Osztály_Új;
        private TabPage tabPage4;
        private Label label25;
        private TextBox Opció_Ár;
        private TextBox Opció_Mennyisége;
        private Label label24;
        private Label label23;
        private Label label22;
        private DateTimePicker Opció_Kezdet;
        private TextBox Opció_Megnevezés;
        private Label label21;
        internal Button Opció_Új;
        internal Button Opció_Excel;
        internal Button Opció_OK;
        private DateTimePicker Opció_Vég;
        private Zuby.ADGV.AdvancedDataGridView Opció_Tábla;
        internal Button Opció_Frissít;
        private TextBox Opció_Id;
        private Label label26;
        private Zuby.ADGV.AdvancedDataGridView Tábla1;
    }
}