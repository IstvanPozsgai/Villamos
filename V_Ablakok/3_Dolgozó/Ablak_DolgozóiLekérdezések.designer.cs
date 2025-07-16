using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
     public partial class Ablak_DolgozóiLekérdezések : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_DolgozóiLekérdezések));
            this.Label13 = new System.Windows.Forms.Label();
            this.Fülek = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Command3 = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.BtnExcelkimenet = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.Váltmódosítás = new System.Windows.Forms.Button();
            this.Új_adat = new System.Windows.Forms.Button();
            this.Tábla_Frissít = new System.Windows.Forms.Button();
            this.Vált_Törlés = new System.Windows.Forms.Button();
            this.Sorszám = new System.Windows.Forms.TextBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.Excelbe = new System.Windows.Forms.Button();
            this.Változattörlés = new System.Windows.Forms.Button();
            this.Újváltozat = new System.Windows.Forms.Button();
            this.Szélessége = new System.Windows.Forms.TextBox();
            this.Sora = new System.Windows.Forms.TextBox();
            this.Oszlopa = new System.Windows.Forms.TextBox();
            this.Csoportlista = new System.Windows.Forms.ComboBox();
            this.Változatoklist = new System.Windows.Forms.ComboBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.Panel5 = new System.Windows.Forms.Panel();
            this.BtnKijelölésátjelöl = new System.Windows.Forms.Button();
            this.TáblaDolgozónévsor = new System.Windows.Forms.DataGridView();
            this.BtnDolgozóÜres = new System.Windows.Forms.Button();
            this.BtnDolgozóMind = new System.Windows.Forms.Button();
            this.BtndolgozóLE = new System.Windows.Forms.Button();
            this.Label11 = new System.Windows.Forms.Label();
            this.BtndolgozóFEL = new System.Windows.Forms.Button();
            this.Label9 = new System.Windows.Forms.Label();
            this.BtnTáblafrissítés = new System.Windows.Forms.Button();
            this.Jog_Excel = new System.Windows.Forms.Button();
            this.Dátumig = new System.Windows.Forms.DateTimePicker();
            this.Dátumtól = new System.Windows.Forms.DateTimePicker();
            this.JogTábla = new System.Windows.Forms.DataGridView();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.Munkakör_excel = new System.Windows.Forms.Button();
            this.Button3 = new System.Windows.Forms.Button();
            this.RadioButton3 = new System.Windows.Forms.RadioButton();
            this.RadioButton2 = new System.Windows.Forms.RadioButton();
            this.RadioButton1 = new System.Windows.Forms.RadioButton();
            this.Label40 = new System.Windows.Forms.Label();
            this.PDFMunkakör = new System.Windows.Forms.ComboBox();
            this.Munkakörtábla = new System.Windows.Forms.DataGridView();
            this.TabPage5 = new System.Windows.Forms.TabPage();
            this.PDF_néző = new PdfiumViewer.PdfViewer();
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.Holtart = new V_MindenEgyéb.MyProgressbar();
            this.Főholtart = new V_MindenEgyéb.MyProgressbar();
            this.Alholtart = new V_MindenEgyéb.MyProgressbar();
            this.Cmbtelephely = new System.Windows.Forms.CheckedListBox();
            this.BtnLe = new System.Windows.Forms.Button();
            this.BtnFel = new System.Windows.Forms.Button();
            this.BtnTelepÜres = new System.Windows.Forms.Button();
            this.BtnTelepMind = new System.Windows.Forms.Button();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Fülek.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.TabPage2.SuspendLayout();
            this.Panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.TabPage3.SuspendLayout();
            this.Panel4.SuspendLayout();
            this.Panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TáblaDolgozónévsor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.JogTábla)).BeginInit();
            this.TabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Munkakörtábla)).BeginInit();
            this.TabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(12, 12);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(145, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
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
            this.Fülek.Controls.Add(this.TabPage5);
            this.Fülek.Location = new System.Drawing.Point(2, 55);
            this.Fülek.Name = "Fülek";
            this.Fülek.Padding = new System.Drawing.Point(16, 3);
            this.Fülek.SelectedIndex = 0;
            this.Fülek.Size = new System.Drawing.Size(1197, 346);
            this.Fülek.TabIndex = 57;
            this.Fülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Fülek_DrawItem);
            this.Fülek.SelectedIndexChanged += new System.EventHandler(this.Fülek_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.Controls.Add(this.Panel2);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1189, 313);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Létszám adatok";
            this.TabPage1.UseVisualStyleBackColor = true;
            // 
            // Panel2
            // 
            this.Panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel2.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Panel2.Controls.Add(this.Command3);
            this.Panel2.Controls.Add(this.Label2);
            this.Panel2.Controls.Add(this.BtnExcelkimenet);
            this.Panel2.Controls.Add(this.Label1);
            this.Panel2.Location = new System.Drawing.Point(0, 0);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(1189, 306);
            this.Panel2.TabIndex = 114;
            // 
            // Command3
            // 
            this.Command3.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Command3.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Command3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command3.Location = new System.Drawing.Point(504, 74);
            this.Command3.Name = "Command3";
            this.Command3.Size = new System.Drawing.Size(45, 45);
            this.Command3.TabIndex = 115;
            this.ToolTip1.SetToolTip(this.Command3, "Excel tábla készítés");
            this.Command3.UseVisualStyleBackColor = false;
            this.Command3.Click += new System.EventHandler(this.Command3_Click);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(15, 86);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(210, 20);
            this.Label2.TabIndex = 114;
            this.Label2.Text = "Csoportonkénti megjelenítés";
            // 
            // BtnExcelkimenet
            // 
            this.BtnExcelkimenet.BackColor = System.Drawing.Color.LightSeaGreen;
            this.BtnExcelkimenet.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.BtnExcelkimenet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnExcelkimenet.Location = new System.Drawing.Point(504, 20);
            this.BtnExcelkimenet.Name = "BtnExcelkimenet";
            this.BtnExcelkimenet.Size = new System.Drawing.Size(45, 45);
            this.BtnExcelkimenet.TabIndex = 111;
            this.ToolTip1.SetToolTip(this.BtnExcelkimenet, "Excel tábla készítés");
            this.BtnExcelkimenet.UseVisualStyleBackColor = false;
            this.BtnExcelkimenet.Click += new System.EventHandler(this.BtnExcelkimenet_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(15, 29);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(483, 20);
            this.Label1.TabIndex = 112;
            this.Label1.Text = "Üzemenként egy táblázatban került összefoglalásra az adathalmaz";
            // 
            // TabPage2
            // 
            this.TabPage2.Controls.Add(this.Panel3);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(1189, 313);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Saját elrendezésű felépítés";
            this.TabPage2.UseVisualStyleBackColor = true;
            // 
            // Panel3
            // 
            this.Panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel3.BackColor = System.Drawing.Color.Coral;
            this.Panel3.Controls.Add(this.Váltmódosítás);
            this.Panel3.Controls.Add(this.Új_adat);
            this.Panel3.Controls.Add(this.Tábla_Frissít);
            this.Panel3.Controls.Add(this.Vált_Törlés);
            this.Panel3.Controls.Add(this.Sorszám);
            this.Panel3.Controls.Add(this.Label8);
            this.Panel3.Controls.Add(this.Tábla);
            this.Panel3.Controls.Add(this.Excelbe);
            this.Panel3.Controls.Add(this.Változattörlés);
            this.Panel3.Controls.Add(this.Újváltozat);
            this.Panel3.Controls.Add(this.Szélessége);
            this.Panel3.Controls.Add(this.Sora);
            this.Panel3.Controls.Add(this.Oszlopa);
            this.Panel3.Controls.Add(this.Csoportlista);
            this.Panel3.Controls.Add(this.Változatoklist);
            this.Panel3.Controls.Add(this.Label7);
            this.Panel3.Controls.Add(this.Label6);
            this.Panel3.Controls.Add(this.Label5);
            this.Panel3.Controls.Add(this.Label4);
            this.Panel3.Controls.Add(this.Label3);
            this.Panel3.Location = new System.Drawing.Point(0, 0);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(1189, 313);
            this.Panel3.TabIndex = 0;
            // 
            // Váltmódosítás
            // 
            this.Váltmódosítás.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Váltmódosítás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Váltmódosítás.Location = new System.Drawing.Point(287, 21);
            this.Váltmódosítás.Name = "Váltmódosítás";
            this.Váltmódosítás.Size = new System.Drawing.Size(45, 45);
            this.Váltmódosítás.TabIndex = 124;
            this.ToolTip1.SetToolTip(this.Váltmódosítás, "Adatsor rögzítése");
            this.Váltmódosítás.UseVisualStyleBackColor = true;
            this.Váltmódosítás.Click += new System.EventHandler(this.Váltmódosítás_Click);
            // 
            // Új_adat
            // 
            this.Új_adat.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Új_adat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Új_adat.Location = new System.Drawing.Point(885, 104);
            this.Új_adat.Name = "Új_adat";
            this.Új_adat.Size = new System.Drawing.Size(45, 45);
            this.Új_adat.TabIndex = 123;
            this.ToolTip1.SetToolTip(this.Új_adat, "Új változat");
            this.Új_adat.UseVisualStyleBackColor = true;
            this.Új_adat.Click += new System.EventHandler(this.Új_adat_Click);
            // 
            // Tábla_Frissít
            // 
            this.Tábla_Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Tábla_Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Tábla_Frissít.Location = new System.Drawing.Point(834, 104);
            this.Tábla_Frissít.Name = "Tábla_Frissít";
            this.Tábla_Frissít.Size = new System.Drawing.Size(45, 45);
            this.Tábla_Frissít.TabIndex = 122;
            this.ToolTip1.SetToolTip(this.Tábla_Frissít, "Frissíti a táblázat tartalmát");
            this.Tábla_Frissít.UseVisualStyleBackColor = true;
            this.Tábla_Frissít.Click += new System.EventHandler(this.Tábla_Frissít_Click);
            // 
            // Vált_Törlés
            // 
            this.Vált_Törlés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Vált_Törlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Vált_Törlés.Location = new System.Drawing.Point(15, 21);
            this.Vált_Törlés.Name = "Vált_Törlés";
            this.Vált_Törlés.Size = new System.Drawing.Size(45, 45);
            this.Vált_Törlés.TabIndex = 121;
            this.ToolTip1.SetToolTip(this.Vált_Törlés, "Változat törlése");
            this.Vált_Törlés.UseVisualStyleBackColor = true;
            this.Vált_Törlés.Click += new System.EventHandler(this.Vált_Törlés_Click);
            // 
            // Sorszám
            // 
            this.Sorszám.Enabled = false;
            this.Sorszám.Location = new System.Drawing.Point(66, 123);
            this.Sorszám.Name = "Sorszám";
            this.Sorszám.Size = new System.Drawing.Size(100, 26);
            this.Sorszám.TabIndex = 120;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(62, 91);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(72, 20);
            this.Label8.TabIndex = 119;
            this.Label8.Text = "Sorszám";
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            this.Tábla.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.EnableHeadersVisualStyles = false;
            this.Tábla.Location = new System.Drawing.Point(10, 160);
            this.Tábla.Name = "Tábla";
            this.Tábla.RowHeadersVisible = false;
            this.Tábla.RowHeadersWidth = 20;
            this.Tábla.Size = new System.Drawing.Size(1172, 143);
            this.Tábla.TabIndex = 118;
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            // 
            // Excelbe
            // 
            this.Excelbe.BackColor = System.Drawing.Color.SlateGray;
            this.Excelbe.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excelbe.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excelbe.Location = new System.Drawing.Point(783, 21);
            this.Excelbe.Name = "Excelbe";
            this.Excelbe.Size = new System.Drawing.Size(45, 45);
            this.Excelbe.TabIndex = 116;
            this.ToolTip1.SetToolTip(this.Excelbe, "Excel tábla készítés");
            this.Excelbe.UseVisualStyleBackColor = false;
            this.Excelbe.Click += new System.EventHandler(this.Excelbe_Click);
            // 
            // Változattörlés
            // 
            this.Változattörlés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Változattörlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Változattörlés.Location = new System.Drawing.Point(15, 104);
            this.Változattörlés.Name = "Változattörlés";
            this.Változattörlés.Size = new System.Drawing.Size(45, 45);
            this.Változattörlés.TabIndex = 88;
            this.ToolTip1.SetToolTip(this.Változattörlés, "Adat sor törlése");
            this.Változattörlés.UseVisualStyleBackColor = true;
            this.Változattörlés.Click += new System.EventHandler(this.Változattörlés_Click);
            // 
            // Újváltozat
            // 
            this.Újváltozat.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Újváltozat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Újváltozat.Location = new System.Drawing.Point(783, 104);
            this.Újváltozat.Name = "Újváltozat";
            this.Újváltozat.Size = new System.Drawing.Size(45, 45);
            this.Újváltozat.TabIndex = 39;
            this.ToolTip1.SetToolTip(this.Újváltozat, "Adatsor rögzítése");
            this.Újváltozat.UseVisualStyleBackColor = true;
            this.Újváltozat.Click += new System.EventHandler(this.Újváltozat_Click);
            // 
            // Szélessége
            // 
            this.Szélessége.Location = new System.Drawing.Point(677, 123);
            this.Szélessége.Name = "Szélessége";
            this.Szélessége.Size = new System.Drawing.Size(100, 26);
            this.Szélessége.TabIndex = 9;
            // 
            // Sora
            // 
            this.Sora.Location = new System.Drawing.Point(571, 123);
            this.Sora.Name = "Sora";
            this.Sora.Size = new System.Drawing.Size(100, 26);
            this.Sora.TabIndex = 8;
            // 
            // Oszlopa
            // 
            this.Oszlopa.Location = new System.Drawing.Point(465, 123);
            this.Oszlopa.MaxLength = 2;
            this.Oszlopa.Name = "Oszlopa";
            this.Oszlopa.Size = new System.Drawing.Size(100, 26);
            this.Oszlopa.TabIndex = 7;
            // 
            // Csoportlista
            // 
            this.Csoportlista.FormattingEnabled = true;
            this.Csoportlista.Location = new System.Drawing.Point(172, 121);
            this.Csoportlista.Name = "Csoportlista";
            this.Csoportlista.Size = new System.Drawing.Size(287, 28);
            this.Csoportlista.TabIndex = 6;
            // 
            // Változatoklist
            // 
            this.Változatoklist.FormattingEnabled = true;
            this.Változatoklist.Location = new System.Drawing.Point(66, 38);
            this.Változatoklist.Name = "Változatoklist";
            this.Változatoklist.Size = new System.Drawing.Size(215, 28);
            this.Változatoklist.TabIndex = 5;
            this.Változatoklist.SelectedIndexChanged += new System.EventHandler(this.Változatoklist_SelectedIndexChanged);
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(673, 91);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(83, 20);
            this.Label7.TabIndex = 4;
            this.Label7.Text = "Szélesség";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(567, 91);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(34, 20);
            this.Label6.TabIndex = 3;
            this.Label6.Text = "Sor";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(461, 91);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(58, 20);
            this.Label5.TabIndex = 2;
            this.Label5.Text = "Oszlop";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(168, 91);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(82, 20);
            this.Label4.TabIndex = 1;
            this.Label4.Text = "Csoportok";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(62, 15);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(85, 20);
            this.Label3.TabIndex = 0;
            this.Label3.Text = "Változatok";
            // 
            // TabPage3
            // 
            this.TabPage3.Controls.Add(this.Panel4);
            this.TabPage3.Location = new System.Drawing.Point(4, 29);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(1189, 313);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Jogosítvány adatok lekérdezése";
            this.TabPage3.UseVisualStyleBackColor = true;
            // 
            // Panel4
            // 
            this.Panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel4.BackColor = System.Drawing.Color.Silver;
            this.Panel4.Controls.Add(this.Panel5);
            this.Panel4.Location = new System.Drawing.Point(0, 0);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(1189, 313);
            this.Panel4.TabIndex = 0;
            // 
            // Panel5
            // 
            this.Panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel5.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Panel5.Controls.Add(this.BtnKijelölésátjelöl);
            this.Panel5.Controls.Add(this.TáblaDolgozónévsor);
            this.Panel5.Controls.Add(this.BtnDolgozóÜres);
            this.Panel5.Controls.Add(this.BtnDolgozóMind);
            this.Panel5.Controls.Add(this.BtndolgozóLE);
            this.Panel5.Controls.Add(this.Label11);
            this.Panel5.Controls.Add(this.BtndolgozóFEL);
            this.Panel5.Controls.Add(this.Label9);
            this.Panel5.Controls.Add(this.BtnTáblafrissítés);
            this.Panel5.Controls.Add(this.Jog_Excel);
            this.Panel5.Controls.Add(this.Dátumig);
            this.Panel5.Controls.Add(this.Dátumtól);
            this.Panel5.Controls.Add(this.JogTábla);
            this.Panel5.Location = new System.Drawing.Point(0, 0);
            this.Panel5.Name = "Panel5";
            this.Panel5.Size = new System.Drawing.Size(1189, 313);
            this.Panel5.TabIndex = 0;
            // 
            // BtnKijelölésátjelöl
            // 
            this.BtnKijelölésátjelöl.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.BtnKijelölésátjelöl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnKijelölésátjelöl.Location = new System.Drawing.Point(722, 3);
            this.BtnKijelölésátjelöl.Name = "BtnKijelölésátjelöl";
            this.BtnKijelölésátjelöl.Size = new System.Drawing.Size(40, 40);
            this.BtnKijelölésátjelöl.TabIndex = 134;
            this.ToolTip1.SetToolTip(this.BtnKijelölésátjelöl, "Dolgozói táblázat adatainak frissítése");
            this.BtnKijelölésátjelöl.UseVisualStyleBackColor = true;
            this.BtnKijelölésátjelöl.Click += new System.EventHandler(this.BtnKijelölésátjelöl_Click);
            // 
            // TáblaDolgozónévsor
            // 
            this.TáblaDolgozónévsor.AllowUserToAddRows = false;
            this.TáblaDolgozónévsor.AllowUserToDeleteRows = false;
            this.TáblaDolgozónévsor.AllowUserToResizeRows = false;
            this.TáblaDolgozónévsor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TáblaDolgozónévsor.Location = new System.Drawing.Point(92, 5);
            this.TáblaDolgozónévsor.Name = "TáblaDolgozónévsor";
            this.TáblaDolgozónévsor.Size = new System.Drawing.Size(580, 155);
            this.TáblaDolgozónévsor.TabIndex = 132;
            this.TáblaDolgozónévsor.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TáblaDolgozónévsor_CellDoubleClick);
            // 
            // BtnDolgozóÜres
            // 
            this.BtnDolgozóÜres.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.BtnDolgozóÜres.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnDolgozóÜres.Location = new System.Drawing.Point(814, 3);
            this.BtnDolgozóÜres.Name = "BtnDolgozóÜres";
            this.BtnDolgozóÜres.Size = new System.Drawing.Size(40, 40);
            this.BtnDolgozóÜres.TabIndex = 131;
            this.ToolTip1.SetToolTip(this.BtnDolgozóÜres, "Minden kijelölés törlése");
            this.BtnDolgozóÜres.UseVisualStyleBackColor = true;
            this.BtnDolgozóÜres.Click += new System.EventHandler(this.BtnDolgozóÜres_Click);
            // 
            // BtnDolgozóMind
            // 
            this.BtnDolgozóMind.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.BtnDolgozóMind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnDolgozóMind.Location = new System.Drawing.Point(768, 3);
            this.BtnDolgozóMind.Name = "BtnDolgozóMind";
            this.BtnDolgozóMind.Size = new System.Drawing.Size(40, 40);
            this.BtnDolgozóMind.TabIndex = 130;
            this.ToolTip1.SetToolTip(this.BtnDolgozóMind, "Minden dolgozó kijelölése");
            this.BtnDolgozóMind.UseVisualStyleBackColor = true;
            this.BtnDolgozóMind.Click += new System.EventHandler(this.BtnDolgozóMind_Click);
            // 
            // BtndolgozóLE
            // 
            this.BtndolgozóLE.BackgroundImage = global::Villamos.Properties.Resources.le;
            this.BtndolgozóLE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtndolgozóLE.Location = new System.Drawing.Point(676, 3);
            this.BtndolgozóLE.Name = "BtndolgozóLE";
            this.BtndolgozóLE.Size = new System.Drawing.Size(40, 40);
            this.BtndolgozóLE.TabIndex = 62;
            this.ToolTip1.SetToolTip(this.BtndolgozóLE, "Dolgozói tábla nagyobb méretre nyitása");
            this.BtndolgozóLE.UseVisualStyleBackColor = true;
            this.BtndolgozóLE.Click += new System.EventHandler(this.BtndolgozóLE_Click);
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(6, 21);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(80, 20);
            this.Label11.TabIndex = 129;
            this.Label11.Text = "Dolgozók:";
            // 
            // BtndolgozóFEL
            // 
            this.BtndolgozóFEL.BackgroundImage = global::Villamos.Properties.Resources.fel;
            this.BtndolgozóFEL.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtndolgozóFEL.Location = new System.Drawing.Point(676, 3);
            this.BtndolgozóFEL.Name = "BtndolgozóFEL";
            this.BtndolgozóFEL.Size = new System.Drawing.Size(40, 40);
            this.BtndolgozóFEL.TabIndex = 63;
            this.ToolTip1.SetToolTip(this.BtndolgozóFEL, "Dolgozói tábla kisebb méretre állítása");
            this.BtndolgozóFEL.UseVisualStyleBackColor = true;
            this.BtndolgozóFEL.Visible = false;
            this.BtndolgozóFEL.Click += new System.EventHandler(this.BtndolgozóFEL_Click);
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(681, 134);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(127, 20);
            this.Label9.TabIndex = 124;
            this.Label9.Text = "Vizsgált időszak:";
            // 
            // BtnTáblafrissítés
            // 
            this.BtnTáblafrissítés.BackColor = System.Drawing.Color.SlateGray;
            this.BtnTáblafrissítés.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.BtnTáblafrissítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnTáblafrissítés.Location = new System.Drawing.Point(1086, 118);
            this.BtnTáblafrissítés.Name = "BtnTáblafrissítés";
            this.BtnTáblafrissítés.Size = new System.Drawing.Size(45, 45);
            this.BtnTáblafrissítés.TabIndex = 123;
            this.ToolTip1.SetToolTip(this.BtnTáblafrissítés, "A feltételeknek megfelelően elkészíti az eredmény táblát");
            this.BtnTáblafrissítés.UseVisualStyleBackColor = false;
            this.BtnTáblafrissítés.Click += new System.EventHandler(this.BtnTáblafrissítés_Click);
            // 
            // Jog_Excel
            // 
            this.Jog_Excel.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Jog_Excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Jog_Excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Jog_Excel.Location = new System.Drawing.Point(1137, 118);
            this.Jog_Excel.Name = "Jog_Excel";
            this.Jog_Excel.Size = new System.Drawing.Size(45, 45);
            this.Jog_Excel.TabIndex = 122;
            this.ToolTip1.SetToolTip(this.Jog_Excel, "Eredmény táblát excelbe menti");
            this.Jog_Excel.UseVisualStyleBackColor = false;
            this.Jog_Excel.Click += new System.EventHandler(this.Jog_Excel_Click);
            // 
            // Dátumig
            // 
            this.Dátumig.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátumig.Location = new System.Drawing.Point(958, 128);
            this.Dátumig.Name = "Dátumig";
            this.Dátumig.Size = new System.Drawing.Size(124, 26);
            this.Dátumig.TabIndex = 121;
            this.ToolTip1.SetToolTip(this.Dátumig, "Dátumig");
            // 
            // Dátumtól
            // 
            this.Dátumtól.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátumtól.Location = new System.Drawing.Point(828, 128);
            this.Dátumtól.Name = "Dátumtól";
            this.Dátumtól.Size = new System.Drawing.Size(124, 26);
            this.Dátumtól.TabIndex = 120;
            this.ToolTip1.SetToolTip(this.Dátumtól, "Dátumtól");
            // 
            // JogTábla
            // 
            this.JogTábla.AllowUserToAddRows = false;
            this.JogTábla.AllowUserToDeleteRows = false;
            this.JogTábla.AllowUserToResizeRows = false;
            this.JogTábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.JogTábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.JogTábla.Location = new System.Drawing.Point(10, 172);
            this.JogTábla.Name = "JogTábla";
            this.JogTábla.RowHeadersWidth = 20;
            this.JogTábla.Size = new System.Drawing.Size(1172, 131);
            this.JogTábla.TabIndex = 119;
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.Coral;
            this.TabPage4.Controls.Add(this.Munkakör_excel);
            this.TabPage4.Controls.Add(this.Button3);
            this.TabPage4.Controls.Add(this.RadioButton3);
            this.TabPage4.Controls.Add(this.RadioButton2);
            this.TabPage4.Controls.Add(this.RadioButton1);
            this.TabPage4.Controls.Add(this.Label40);
            this.TabPage4.Controls.Add(this.PDFMunkakör);
            this.TabPage4.Controls.Add(this.Munkakörtábla);
            this.TabPage4.Location = new System.Drawing.Point(4, 29);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Size = new System.Drawing.Size(1189, 313);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Munkakör és Kiegészítő tevékenységek";
            // 
            // Munkakör_excel
            // 
            this.Munkakör_excel.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Munkakör_excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Munkakör_excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Munkakör_excel.Location = new System.Drawing.Point(6, 8);
            this.Munkakör_excel.Name = "Munkakör_excel";
            this.Munkakör_excel.Size = new System.Drawing.Size(45, 45);
            this.Munkakör_excel.TabIndex = 136;
            this.ToolTip1.SetToolTip(this.Munkakör_excel, "Eredmény táblát excelbe menti");
            this.Munkakör_excel.UseVisualStyleBackColor = false;
            this.Munkakör_excel.Click += new System.EventHandler(this.Munkakör_excel_Click);
            // 
            // Button3
            // 
            this.Button3.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button3.Location = new System.Drawing.Point(1031, 10);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(40, 40);
            this.Button3.TabIndex = 135;
            this.ToolTip1.SetToolTip(this.Button3, "Dolgozói táblázat adatainak frissítése");
            this.Button3.UseVisualStyleBackColor = true;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // RadioButton3
            // 
            this.RadioButton3.AutoSize = true;
            this.RadioButton3.BackColor = System.Drawing.Color.Khaki;
            this.RadioButton3.Location = new System.Drawing.Point(755, 21);
            this.RadioButton3.Name = "RadioButton3";
            this.RadioButton3.Size = new System.Drawing.Size(100, 24);
            this.RadioButton3.TabIndex = 125;
            this.RadioButton3.Text = "Kiegészítő";
            this.RadioButton3.UseVisualStyleBackColor = false;
            this.RadioButton3.Click += new System.EventHandler(this.RadioButton3_Click);
            // 
            // RadioButton2
            // 
            this.RadioButton2.AutoSize = true;
            this.RadioButton2.BackColor = System.Drawing.Color.Khaki;
            this.RadioButton2.Location = new System.Drawing.Point(861, 21);
            this.RadioButton2.Name = "RadioButton2";
            this.RadioButton2.Size = new System.Drawing.Size(134, 24);
            this.RadioButton2.TabIndex = 124;
            this.RadioButton2.Text = "Részmunkakör";
            this.RadioButton2.UseVisualStyleBackColor = false;
            this.RadioButton2.Click += new System.EventHandler(this.RadioButton2_Click);
            // 
            // RadioButton1
            // 
            this.RadioButton1.AutoSize = true;
            this.RadioButton1.BackColor = System.Drawing.Color.Khaki;
            this.RadioButton1.Checked = true;
            this.RadioButton1.Location = new System.Drawing.Point(652, 21);
            this.RadioButton1.Name = "RadioButton1";
            this.RadioButton1.Size = new System.Drawing.Size(97, 24);
            this.RadioButton1.TabIndex = 123;
            this.RadioButton1.TabStop = true;
            this.RadioButton1.Text = "Munkakör";
            this.RadioButton1.UseVisualStyleBackColor = false;
            this.RadioButton1.Click += new System.EventHandler(this.RadioButton1_Click);
            // 
            // Label40
            // 
            this.Label40.AutoSize = true;
            this.Label40.BackColor = System.Drawing.Color.Khaki;
            this.Label40.Location = new System.Drawing.Point(70, 25);
            this.Label40.Name = "Label40";
            this.Label40.Size = new System.Drawing.Size(81, 20);
            this.Label40.TabIndex = 122;
            this.Label40.Text = "Kategória:";
            // 
            // PDFMunkakör
            // 
            this.PDFMunkakör.FormattingEnabled = true;
            this.PDFMunkakör.Location = new System.Drawing.Point(179, 17);
            this.PDFMunkakör.Name = "PDFMunkakör";
            this.PDFMunkakör.Size = new System.Drawing.Size(443, 28);
            this.PDFMunkakör.TabIndex = 121;
            // 
            // Munkakörtábla
            // 
            this.Munkakörtábla.AllowUserToAddRows = false;
            this.Munkakörtábla.AllowUserToDeleteRows = false;
            this.Munkakörtábla.AllowUserToResizeRows = false;
            this.Munkakörtábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Munkakörtábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Munkakörtábla.Location = new System.Drawing.Point(6, 58);
            this.Munkakörtábla.Name = "Munkakörtábla";
            this.Munkakörtábla.RowHeadersWidth = 20;
            this.Munkakörtábla.Size = new System.Drawing.Size(1176, 245);
            this.Munkakörtábla.TabIndex = 120;
            this.Munkakörtábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Munkakörtábla_CellClick);
            this.Munkakörtábla.SelectionChanged += new System.EventHandler(this.Munkakörtábla_SelectionChanged);
            // 
            // TabPage5
            // 
            this.TabPage5.BackColor = System.Drawing.Color.LightSeaGreen;
            this.TabPage5.Controls.Add(this.PDF_néző);
            this.TabPage5.Location = new System.Drawing.Point(4, 29);
            this.TabPage5.Name = "TabPage5";
            this.TabPage5.Size = new System.Drawing.Size(1189, 313);
            this.TabPage5.TabIndex = 4;
            this.TabPage5.Text = "PDF";
            // 
            // PDF_néző
            // 
            this.PDF_néző.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PDF_néző.Location = new System.Drawing.Point(6, 8);
            this.PDF_néző.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.PDF_néző.Name = "PDF_néző";
            this.PDF_néző.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.PDF_néző.Size = new System.Drawing.Size(1173, 297);
            this.PDF_néző.TabIndex = 241;
            this.PDF_néző.ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitWidth;
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1150, 4);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 58;
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.ForeColor = System.Drawing.Color.Coral;
            this.Holtart.Location = new System.Drawing.Point(545, 36);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(599, 12);
            this.Holtart.TabIndex = 61;
            this.Holtart.Visible = false;
            // 
            // Főholtart
            // 
            this.Főholtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Főholtart.ForeColor = System.Drawing.Color.Red;
            this.Főholtart.Location = new System.Drawing.Point(545, 4);
            this.Főholtart.Name = "Főholtart";
            this.Főholtart.Size = new System.Drawing.Size(599, 12);
            this.Főholtart.TabIndex = 60;
            this.Főholtart.Visible = false;
            // 
            // Alholtart
            // 
            this.Alholtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Alholtart.ForeColor = System.Drawing.Color.Tomato;
            this.Alholtart.Location = new System.Drawing.Point(545, 20);
            this.Alholtart.Name = "Alholtart";
            this.Alholtart.Size = new System.Drawing.Size(599, 12);
            this.Alholtart.TabIndex = 59;
            this.Alholtart.Visible = false;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.CheckOnClick = true;
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(163, 12);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(235, 25);
            this.Cmbtelephely.TabIndex = 18;
            // 
            // BtnLe
            // 
            this.BtnLe.BackgroundImage = global::Villamos.Properties.Resources.le;
            this.BtnLe.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnLe.Location = new System.Drawing.Point(404, 6);
            this.BtnLe.Name = "BtnLe";
            this.BtnLe.Size = new System.Drawing.Size(40, 40);
            this.BtnLe.TabIndex = 59;
            this.ToolTip1.SetToolTip(this.BtnLe, "Telephelyi lista kibontása");
            this.BtnLe.UseVisualStyleBackColor = true;
            this.BtnLe.Click += new System.EventHandler(this.BtnLe_Click);
            // 
            // BtnFel
            // 
            this.BtnFel.BackgroundImage = global::Villamos.Properties.Resources.fel;
            this.BtnFel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnFel.Location = new System.Drawing.Point(404, 4);
            this.BtnFel.Name = "BtnFel";
            this.BtnFel.Size = new System.Drawing.Size(40, 40);
            this.BtnFel.TabIndex = 60;
            this.ToolTip1.SetToolTip(this.BtnFel, "Telephely lista elrejtése");
            this.BtnFel.UseVisualStyleBackColor = true;
            this.BtnFel.Visible = false;
            this.BtnFel.Click += new System.EventHandler(this.BtnFel_Click);
            // 
            // BtnTelepÜres
            // 
            this.BtnTelepÜres.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.BtnTelepÜres.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnTelepÜres.Location = new System.Drawing.Point(496, 4);
            this.BtnTelepÜres.Name = "BtnTelepÜres";
            this.BtnTelepÜres.Size = new System.Drawing.Size(40, 40);
            this.BtnTelepÜres.TabIndex = 133;
            this.ToolTip1.SetToolTip(this.BtnTelepÜres, "Minden kijelölt telephely kijelölésének törlése");
            this.BtnTelepÜres.UseVisualStyleBackColor = true;
            this.BtnTelepÜres.Click += new System.EventHandler(this.BtnTelepÜres_Click);
            // 
            // BtnTelepMind
            // 
            this.BtnTelepMind.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.BtnTelepMind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnTelepMind.Location = new System.Drawing.Point(450, 4);
            this.BtnTelepMind.Name = "BtnTelepMind";
            this.BtnTelepMind.Size = new System.Drawing.Size(40, 40);
            this.BtnTelepMind.TabIndex = 132;
            this.ToolTip1.SetToolTip(this.BtnTelepMind, "Minden telephely kijelölése");
            this.BtnTelepMind.UseVisualStyleBackColor = true;
            this.BtnTelepMind.Click += new System.EventHandler(this.BtnTelepMind_Click);
            // 
            // Ablak_DolgozóiLekérdezések
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 413);
            this.Controls.Add(this.BtnTelepÜres);
            this.Controls.Add(this.BtnLe);
            this.Controls.Add(this.BtnTelepMind);
            this.Controls.Add(this.BtnFel);
            this.Controls.Add(this.Cmbtelephely);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Főholtart);
            this.Controls.Add(this.Alholtart);
            this.Controls.Add(this.Label13);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.Fülek);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_DolgozóiLekérdezések";
            this.Text = "Dolgozói Lekérdezések";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AblakDolgozóiLekérdezések_Load);
            this.Fülek.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.TabPage2.ResumeLayout(false);
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.TabPage3.ResumeLayout(false);
            this.Panel4.ResumeLayout(false);
            this.Panel5.ResumeLayout(false);
            this.Panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TáblaDolgozónévsor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.JogTábla)).EndInit();
            this.TabPage4.ResumeLayout(false);
            this.TabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Munkakörtábla)).EndInit();
            this.TabPage5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal Label Label13;
        internal TabControl Fülek;
        internal TabPage TabPage1;
        internal Label Label1;
        internal Button BtnExcelkimenet;
        internal TabPage TabPage2;
        internal Panel Panel2;
        internal Button BtnSúgó;
        internal Button Command3;
        internal Label Label2;
        internal Panel Panel3;
        internal TextBox Szélessége;
        internal TextBox Sora;
        internal TextBox Oszlopa;
        internal ComboBox Csoportlista;
        internal ComboBox Változatoklist;
        internal Label Label7;
        internal Label Label6;
        internal Label Label5;
        internal Label Label4;
        internal Label Label3;
        internal TabPage TabPage3;
        internal Panel Panel4;
        internal Button Újváltozat;
        internal Button Változattörlés;
        internal Button Excelbe;
        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal V_MindenEgyéb.MyProgressbar Főholtart;
        internal V_MindenEgyéb.MyProgressbar Alholtart;
        internal DataGridView Tábla;
        internal TextBox Sorszám;
        internal Label Label8;
        internal Button Vált_Törlés;
        internal Panel Panel5;
        internal Button Jog_Excel;
        internal DateTimePicker Dátumig;
        internal DateTimePicker Dátumtól;
        internal DataGridView JogTábla;
        internal Button BtnTáblafrissítés;
        internal Label Label9;
        internal CheckedListBox Cmbtelephely;
        internal Button BtnFel;
        internal Button BtnLe;
        internal Button BtndolgozóLE;
        internal Label Label11;
        internal Button BtndolgozóFEL;
        internal Button BtnDolgozóÜres;
        internal Button BtnDolgozóMind;
        internal Button BtnTelepÜres;
        internal Button BtnTelepMind;
        internal Button BtnKijelölésátjelöl;
        internal DataGridView TáblaDolgozónévsor;
        internal ToolTip ToolTip1;
        internal TabPage TabPage4;
        internal DataGridView Munkakörtábla;
        internal Button Munkakör_excel;
        internal Button Button3;
        internal RadioButton RadioButton3;
        internal RadioButton RadioButton2;
        internal RadioButton RadioButton1;
        internal Label Label40;
        internal ComboBox PDFMunkakör;
        internal TabPage TabPage5;
        private PdfiumViewer.PdfViewer PDF_néző;
        internal Button Tábla_Frissít;
        internal Button Új_adat;
        internal Button Váltmódosítás;
    }
}