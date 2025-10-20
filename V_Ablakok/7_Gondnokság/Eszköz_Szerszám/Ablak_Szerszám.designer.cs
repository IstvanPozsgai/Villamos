using System.Diagnostics;
using System.Windows.Forms;

namespace Villamos
{

    public partial class Ablak_Szerszám : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components!= null)
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Szerszám));
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Lapfülek = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.Alap_Gyáriszám = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.Alap_Lekérdezés_Méret = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.Alap_lekérd_megnevezés = new System.Windows.Forms.TextBox();
            this.Radio_minden = new System.Windows.Forms.RadioButton();
            this.label36 = new System.Windows.Forms.Label();
            this.Radio_E = new System.Windows.Forms.RadioButton();
            this.Radio_A = new System.Windows.Forms.RadioButton();
            this.Alap_Töröltek = new System.Windows.Forms.CheckBox();
            this.Alap_Frissít = new System.Windows.Forms.Button();
            this.Alap_excel = new System.Windows.Forms.Button();
            this.Alap_tárolás = new System.Windows.Forms.TextBox();
            this.Label26 = new System.Windows.Forms.Label();
            this.Alap_tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.Alap_Költséghely = new System.Windows.Forms.TextBox();
            this.Alap_Méret = new System.Windows.Forms.TextBox();
            this.Alap_Beszerzési_dátum = new System.Windows.Forms.DateTimePicker();
            this.Alap_Megnevezés = new System.Windows.Forms.TextBox();
            this.Label34 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Alap_Leltáriszám = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Alap_Azonosító = new System.Windows.Forms.ComboBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Alap_Aktív = new System.Windows.Forms.CheckBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Alap_Új_adat = new System.Windows.Forms.Button();
            this.Alap_Rögzít = new System.Windows.Forms.Button();
            this.Label5 = new System.Windows.Forms.Label();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.Könyv_excel = new System.Windows.Forms.Button();
            this.Könyv_tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.Könyv_Felelős2 = new System.Windows.Forms.ComboBox();
            this.Könyv_Felelős1 = new System.Windows.Forms.ComboBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Könyv_Töröltek = new System.Windows.Forms.CheckBox();
            this.Könyv_megnevezés = new System.Windows.Forms.TextBox();
            this.Könyv_szám = new System.Windows.Forms.ComboBox();
            this.Könyv_Törlés = new System.Windows.Forms.CheckBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Könyv_új = new System.Windows.Forms.Button();
            this.Könyv_Rögzít = new System.Windows.Forms.Button();
            this.Frissít = new System.Windows.Forms.Button();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Könyvelés_szűrés_ürítés = new System.Windows.Forms.Button();
            this.Könyvelés_Szűr = new System.Windows.Forms.Button();
            this.Könyvelés_Méret = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.Könyvelés_megnevezés = new System.Windows.Forms.TextBox();
            this.Radio_könyv_Minden = new System.Windows.Forms.RadioButton();
            this.label40 = new System.Windows.Forms.Label();
            this.Radio_könyv_E = new System.Windows.Forms.RadioButton();
            this.Radio_könyv_A = new System.Windows.Forms.RadioButton();
            this.HováMennyiség = new System.Windows.Forms.Label();
            this.HonnanMennyiség = new System.Windows.Forms.Label();
            this.Könyvelés_tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.Megnevezés = new System.Windows.Forms.TextBox();
            this.Label24 = new System.Windows.Forms.Label();
            this.Label22 = new System.Windows.Forms.Label();
            this.Label23 = new System.Windows.Forms.Label();
            this.SzerszámAzonosító = new System.Windows.Forms.ComboBox();
            this.Mennyiség = new System.Windows.Forms.TextBox();
            this.Label21 = new System.Windows.Forms.Label();
            this.Label20 = new System.Windows.Forms.Label();
            this.HováNév = new System.Windows.Forms.ComboBox();
            this.Hova = new System.Windows.Forms.ComboBox();
            this.Label18 = new System.Windows.Forms.Label();
            this.HonnanNév = new System.Windows.Forms.ComboBox();
            this.Honnan = new System.Windows.Forms.ComboBox();
            this.Label19 = new System.Windows.Forms.Label();
            this.Rögzít = new System.Windows.Forms.Button();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.Lekérd_Szerszámkönyvszám = new System.Windows.Forms.CheckedListBox();
            this.Lekérd_Tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.Lekérd_Töröltek = new System.Windows.Forms.CheckBox();
            this.Label17 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Label16 = new System.Windows.Forms.Label();
            this.Lekérd_Felelős1 = new System.Windows.Forms.ComboBox();
            this.Lekérd_Nevekkiválasztása = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Lekérd_Méret = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.Lekérd_Megnevezés = new System.Windows.Forms.TextBox();
            this.Radio_lek_minden = new System.Windows.Forms.RadioButton();
            this.label41 = new System.Windows.Forms.Label();
            this.Radio_lek_E = new System.Windows.Forms.RadioButton();
            this.Radio_lek_A = new System.Windows.Forms.RadioButton();
            this.Lekérd_Töröltek1 = new System.Windows.Forms.CheckBox();
            this.Lekérd_Szerszámazonosító = new System.Windows.Forms.ComboBox();
            this.Label15 = new System.Windows.Forms.Label();
            this.Lekérd_Anyagkiíró = new System.Windows.Forms.Button();
            this.Lekérd_Command1 = new System.Windows.Forms.Button();
            this.Lekérd_Excelclick = new System.Windows.Forms.Button();
            this.Lekérd_Visszacsuk = new System.Windows.Forms.Button();
            this.Lekérd_Jelöltszersz = new System.Windows.Forms.Button();
            this.Lekérd_Mindtöröl = new System.Windows.Forms.Button();
            this.Lekérd_Összeskijelöl = new System.Windows.Forms.Button();
            this.Lekérd_Lenyit = new System.Windows.Forms.Button();
            this.TabPage5 = new System.Windows.Forms.TabPage();
            this.Nyomtatvány9A = new System.Windows.Forms.Button();
            this.Nyomtatvány9B = new System.Windows.Forms.Button();
            this.Napló_Fájltöröl = new System.Windows.Forms.CheckBox();
            this.Napló_Hovánév = new System.Windows.Forms.ComboBox();
            this.Napló_Hova = new System.Windows.Forms.ComboBox();
            this.Label14 = new System.Windows.Forms.Label();
            this.Napló_Honnannév = new System.Windows.Forms.ComboBox();
            this.Napló_Honnan = new System.Windows.Forms.ComboBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.Napló_Dátumig = new System.Windows.Forms.DateTimePicker();
            this.Label11 = new System.Windows.Forms.Label();
            this.Napló_Tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.Napló_Dátumtól = new System.Windows.Forms.DateTimePicker();
            this.Napló_Nyomtat = new System.Windows.Forms.CheckBox();
            this.Label10 = new System.Windows.Forms.Label();
            this.Napló_Nyomtatvány = new System.Windows.Forms.Button();
            this.Napló_Excel_gomb = new System.Windows.Forms.Button();
            this.Napló_Listáz = new System.Windows.Forms.Button();
            this.TabPage6 = new System.Windows.Forms.TabPage();
            this.Mentés = new System.Windows.Forms.Button();
            this.Kép_szűrés = new System.Windows.Forms.ListBox();
            this.Kép_megnevezés = new System.Windows.Forms.TextBox();
            this.Label35 = new System.Windows.Forms.Label();
            this.Label30 = new System.Windows.Forms.Label();
            this.Label32 = new System.Windows.Forms.Label();
            this.Kép_Feltöltendő = new System.Windows.Forms.TextBox();
            this.Label33 = new System.Windows.Forms.Label();
            this.KépTörlés = new System.Windows.Forms.Button();
            this.Kép_btn = new System.Windows.Forms.Button();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.Kép_listbox = new System.Windows.Forms.ListBox();
            this.Kép_Azonosító = new System.Windows.Forms.ComboBox();
            this.Kép_Listázás = new System.Windows.Forms.Button();
            this.Kép_rögzít = new System.Windows.Forms.Button();
            this.TabPage7 = new System.Windows.Forms.TabPage();
            this.Szűrés = new System.Windows.Forms.ListBox();
            this.PDF_néző = new PdfiumViewer.PdfViewer();
            this.PDF_megnevezés = new System.Windows.Forms.TextBox();
            this.Label29 = new System.Windows.Forms.Label();
            this.Label28 = new System.Windows.Forms.Label();
            this.Label38 = new System.Windows.Forms.Label();
            this.Feltöltendő = new System.Windows.Forms.TextBox();
            this.Label27 = new System.Windows.Forms.Label();
            this.PDF_törlés = new System.Windows.Forms.Button();
            this.BtnPDF = new System.Windows.Forms.Button();
            this.Pdf_listbox = new System.Windows.Forms.ListBox();
            this.PDF_Azonosító = new System.Windows.Forms.ComboBox();
            this.PDF_Frissít = new System.Windows.Forms.Button();
            this.PDF_rögzít = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.Panel2.SuspendLayout();
            this.Lapfülek.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Alap_tábla)).BeginInit();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Könyv_tábla)).BeginInit();
            this.TabPage3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Könyvelés_tábla)).BeginInit();
            this.TabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Lekérd_Tábla)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.TabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Napló_Tábla)).BeginInit();
            this.TabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.TabPage7.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.Cmbtelephely);
            this.Panel2.Controls.Add(this.Label13);
            this.Panel2.Location = new System.Drawing.Point(5, 5);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(335, 33);
            this.Panel2.TabIndex = 173;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(143, 0);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
            this.Cmbtelephely.SelectionChangeCommitted += new System.EventHandler(this.Cmbtelephely_SelectionChangeCommitted);
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
            // Lapfülek
            // 
            this.Lapfülek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Lapfülek.Controls.Add(this.TabPage1);
            this.Lapfülek.Controls.Add(this.TabPage2);
            this.Lapfülek.Controls.Add(this.TabPage3);
            this.Lapfülek.Controls.Add(this.TabPage4);
            this.Lapfülek.Controls.Add(this.TabPage5);
            this.Lapfülek.Controls.Add(this.TabPage6);
            this.Lapfülek.Controls.Add(this.TabPage7);
            this.Lapfülek.Location = new System.Drawing.Point(5, 56);
            this.Lapfülek.Name = "Lapfülek";
            this.Lapfülek.Padding = new System.Drawing.Point(16, 3);
            this.Lapfülek.SelectedIndex = 0;
            this.Lapfülek.Size = new System.Drawing.Size(1227, 425);
            this.Lapfülek.TabIndex = 176;
            this.Lapfülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LapFülek_DrawItem);
            this.Lapfülek.SelectedIndexChanged += new System.EventHandler(this.LapFülek_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.DodgerBlue;
            this.TabPage1.Controls.Add(this.Alap_Gyáriszám);
            this.TabPage1.Controls.Add(this.label31);
            this.TabPage1.Controls.Add(this.GroupBox1);
            this.TabPage1.Controls.Add(this.Alap_excel);
            this.TabPage1.Controls.Add(this.Alap_tárolás);
            this.TabPage1.Controls.Add(this.Label26);
            this.TabPage1.Controls.Add(this.Alap_tábla);
            this.TabPage1.Controls.Add(this.Alap_Költséghely);
            this.TabPage1.Controls.Add(this.Alap_Méret);
            this.TabPage1.Controls.Add(this.Alap_Beszerzési_dátum);
            this.TabPage1.Controls.Add(this.Alap_Megnevezés);
            this.TabPage1.Controls.Add(this.Label34);
            this.TabPage1.Controls.Add(this.Label1);
            this.TabPage1.Controls.Add(this.Alap_Leltáriszám);
            this.TabPage1.Controls.Add(this.Label2);
            this.TabPage1.Controls.Add(this.Alap_Azonosító);
            this.TabPage1.Controls.Add(this.Label3);
            this.TabPage1.Controls.Add(this.Alap_Aktív);
            this.TabPage1.Controls.Add(this.Label4);
            this.TabPage1.Controls.Add(this.Alap_Új_adat);
            this.TabPage1.Controls.Add(this.Alap_Rögzít);
            this.TabPage1.Controls.Add(this.Label5);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1219, 392);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Szerszám Törzs";
            // 
            // Alap_Gyáriszám
            // 
            this.Alap_Gyáriszám.Location = new System.Drawing.Point(120, 41);
            this.Alap_Gyáriszám.MaxLength = 15;
            this.Alap_Gyáriszám.Name = "Alap_Gyáriszám";
            this.Alap_Gyáriszám.Size = new System.Drawing.Size(180, 26);
            this.Alap_Gyáriszám.TabIndex = 3;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.BackColor = System.Drawing.Color.Silver;
            this.label31.Location = new System.Drawing.Point(5, 47);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(88, 20);
            this.label31.TabIndex = 191;
            this.label31.Text = "Gyáriszám:";
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.Alap_Lekérdezés_Méret);
            this.GroupBox1.Controls.Add(this.label37);
            this.GroupBox1.Controls.Add(this.Alap_lekérd_megnevezés);
            this.GroupBox1.Controls.Add(this.Radio_minden);
            this.GroupBox1.Controls.Add(this.label36);
            this.GroupBox1.Controls.Add(this.Radio_E);
            this.GroupBox1.Controls.Add(this.Radio_A);
            this.GroupBox1.Controls.Add(this.Alap_Töröltek);
            this.GroupBox1.Controls.Add(this.Alap_Frissít);
            this.GroupBox1.Location = new System.Drawing.Point(5, 143);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(1208, 64);
            this.GroupBox1.TabIndex = 189;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Szűrés";
            // 
            // Alap_Lekérdezés_Méret
            // 
            this.Alap_Lekérdezés_Méret.Location = new System.Drawing.Point(783, 22);
            this.Alap_Lekérdezés_Méret.MaxLength = 15;
            this.Alap_Lekérdezés_Méret.Name = "Alap_Lekérdezés_Méret";
            this.Alap_Lekérdezés_Méret.Size = new System.Drawing.Size(180, 26);
            this.Alap_Lekérdezés_Méret.TabIndex = 4;
            this.Alap_Lekérdezés_Méret.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Alap_Lekérdezés_Méret_MouseClick);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.BackColor = System.Drawing.Color.Silver;
            this.label37.Location = new System.Drawing.Point(723, 26);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(54, 20);
            this.label37.TabIndex = 191;
            this.label37.Text = "Méret:";
            // 
            // Alap_lekérd_megnevezés
            // 
            this.Alap_lekérd_megnevezés.Location = new System.Drawing.Point(291, 23);
            this.Alap_lekérd_megnevezés.MaxLength = 50;
            this.Alap_lekérd_megnevezés.Name = "Alap_lekérd_megnevezés";
            this.Alap_lekérd_megnevezés.Size = new System.Drawing.Size(426, 26);
            this.Alap_lekérd_megnevezés.TabIndex = 3;
            this.Alap_lekérd_megnevezés.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Alap_lekérd_megnevezés_MouseClick);
            // 
            // Radio_minden
            // 
            this.Radio_minden.AutoSize = true;
            this.Radio_minden.BackColor = System.Drawing.Color.SkyBlue;
            this.Radio_minden.Checked = true;
            this.Radio_minden.Location = new System.Drawing.Point(6, 22);
            this.Radio_minden.Name = "Radio_minden";
            this.Radio_minden.Size = new System.Drawing.Size(79, 24);
            this.Radio_minden.TabIndex = 0;
            this.Radio_minden.TabStop = true;
            this.Radio_minden.Text = "Minden";
            this.Radio_minden.UseVisualStyleBackColor = false;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.BackColor = System.Drawing.Color.Silver;
            this.label36.Location = new System.Drawing.Point(183, 26);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(103, 20);
            this.label36.TabIndex = 189;
            this.label36.Text = "Megnevezés:";
            // 
            // Radio_E
            // 
            this.Radio_E.AutoSize = true;
            this.Radio_E.BackColor = System.Drawing.Color.SkyBlue;
            this.Radio_E.Location = new System.Drawing.Point(139, 22);
            this.Radio_E.Name = "Radio_E";
            this.Radio_E.Size = new System.Drawing.Size(38, 24);
            this.Radio_E.TabIndex = 2;
            this.Radio_E.Text = "E";
            this.Radio_E.UseVisualStyleBackColor = false;
            // 
            // Radio_A
            // 
            this.Radio_A.AutoSize = true;
            this.Radio_A.BackColor = System.Drawing.Color.SkyBlue;
            this.Radio_A.Location = new System.Drawing.Point(91, 22);
            this.Radio_A.Name = "Radio_A";
            this.Radio_A.Size = new System.Drawing.Size(42, 24);
            this.Radio_A.TabIndex = 1;
            this.Radio_A.Text = "A ";
            this.Radio_A.UseVisualStyleBackColor = false;
            // 
            // Alap_Töröltek
            // 
            this.Alap_Töröltek.AutoSize = true;
            this.Alap_Töröltek.BackColor = System.Drawing.Color.Gold;
            this.Alap_Töröltek.Location = new System.Drawing.Point(969, 24);
            this.Alap_Töröltek.Name = "Alap_Töröltek";
            this.Alap_Töröltek.Size = new System.Drawing.Size(169, 24);
            this.Alap_Töröltek.TabIndex = 5;
            this.Alap_Töröltek.Text = "Törölt azonosítókkal";
            this.Alap_Töröltek.UseVisualStyleBackColor = false;
            // 
            // Alap_Frissít
            // 
            this.Alap_Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Alap_Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Alap_Frissít.Location = new System.Drawing.Point(1153, 12);
            this.Alap_Frissít.Name = "Alap_Frissít";
            this.Alap_Frissít.Size = new System.Drawing.Size(45, 45);
            this.Alap_Frissít.TabIndex = 9;
            this.toolTip1.SetToolTip(this.Alap_Frissít, "Frissíti a képernyű adatait");
            this.Alap_Frissít.UseVisualStyleBackColor = true;
            this.Alap_Frissít.Click += new System.EventHandler(this.Frissít_Click);
            // 
            // Alap_excel
            // 
            this.Alap_excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Alap_excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Alap_excel.Location = new System.Drawing.Point(1117, 8);
            this.Alap_excel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Alap_excel.Name = "Alap_excel";
            this.Alap_excel.Size = new System.Drawing.Size(45, 45);
            this.Alap_excel.TabIndex = 10;
            this.toolTip1.SetToolTip(this.Alap_excel, "A táblázatos rész Excelbe való kiírása");
            this.Alap_excel.UseVisualStyleBackColor = true;
            this.Alap_excel.Click += new System.EventHandler(this.Alap_excel_Click);
            // 
            // Alap_tárolás
            // 
            this.Alap_tárolás.Location = new System.Drawing.Point(186, 106);
            this.Alap_tárolás.MaxLength = 50;
            this.Alap_tárolás.Name = "Alap_tárolás";
            this.Alap_tárolás.Size = new System.Drawing.Size(532, 26);
            this.Alap_tárolás.TabIndex = 7;
            // 
            // Label26
            // 
            this.Label26.AutoSize = true;
            this.Label26.BackColor = System.Drawing.Color.Silver;
            this.Label26.Location = new System.Drawing.Point(5, 112);
            this.Label26.Name = "Label26";
            this.Label26.Size = new System.Drawing.Size(174, 20);
            this.Label26.TabIndex = 169;
            this.Label26.Text = "Elhelyezés a raktárban:";
            // 
            // Alap_tábla
            // 
            this.Alap_tábla.AllowUserToAddRows = false;
            this.Alap_tábla.AllowUserToDeleteRows = false;
            this.Alap_tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Alap_tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Alap_tábla.FilterAndSortEnabled = true;
            this.Alap_tábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Alap_tábla.Location = new System.Drawing.Point(5, 213);
            this.Alap_tábla.MaxFilterButtonImageHeight = 23;
            this.Alap_tábla.Name = "Alap_tábla";
            this.Alap_tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Alap_tábla.RowHeadersVisible = false;
            this.Alap_tábla.RowHeadersWidth = 51;
            this.Alap_tábla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Alap_tábla.Size = new System.Drawing.Size(1208, 159);
            this.Alap_tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Alap_tábla.TabIndex = 168;
            this.Alap_tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Alap_tábla_CellClick);
            // 
            // Alap_Költséghely
            // 
            this.Alap_Költséghely.Location = new System.Drawing.Point(755, 8);
            this.Alap_Költséghely.MaxLength = 6;
            this.Alap_Költséghely.Name = "Alap_Költséghely";
            this.Alap_Költséghely.Size = new System.Drawing.Size(147, 26);
            this.Alap_Költséghely.TabIndex = 2;
            // 
            // Alap_Méret
            // 
            this.Alap_Méret.Location = new System.Drawing.Point(429, 41);
            this.Alap_Méret.MaxLength = 15;
            this.Alap_Méret.Name = "Alap_Méret";
            this.Alap_Méret.Size = new System.Drawing.Size(180, 26);
            this.Alap_Méret.TabIndex = 4;
            // 
            // Alap_Beszerzési_dátum
            // 
            this.Alap_Beszerzési_dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Alap_Beszerzési_dátum.Location = new System.Drawing.Point(784, 41);
            this.Alap_Beszerzési_dátum.Name = "Alap_Beszerzési_dátum";
            this.Alap_Beszerzési_dátum.Size = new System.Drawing.Size(118, 26);
            this.Alap_Beszerzési_dátum.TabIndex = 5;
            // 
            // Alap_Megnevezés
            // 
            this.Alap_Megnevezés.Location = new System.Drawing.Point(120, 73);
            this.Alap_Megnevezés.MaxLength = 50;
            this.Alap_Megnevezés.Name = "Alap_Megnevezés";
            this.Alap_Megnevezés.Size = new System.Drawing.Size(598, 26);
            this.Alap_Megnevezés.TabIndex = 6;
            // 
            // Label34
            // 
            this.Label34.AutoSize = true;
            this.Label34.BackColor = System.Drawing.Color.Silver;
            this.Label34.Location = new System.Drawing.Point(5, 14);
            this.Label34.Name = "Label34";
            this.Label34.Size = new System.Drawing.Size(84, 20);
            this.Label34.TabIndex = 95;
            this.Label34.Text = "Azonosító:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Silver;
            this.Label1.Location = new System.Drawing.Point(329, 14);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(94, 20);
            this.Label1.TabIndex = 96;
            this.Label1.Text = "Leltáriszám:";
            // 
            // Alap_Leltáriszám
            // 
            this.Alap_Leltáriszám.Location = new System.Drawing.Point(429, 8);
            this.Alap_Leltáriszám.MaxLength = 20;
            this.Alap_Leltáriszám.Name = "Alap_Leltáriszám";
            this.Alap_Leltáriszám.Size = new System.Drawing.Size(180, 26);
            this.Alap_Leltáriszám.TabIndex = 1;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Silver;
            this.Label2.Location = new System.Drawing.Point(5, 79);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(103, 20);
            this.Label2.TabIndex = 97;
            this.Label2.Text = "Megnevezés:";
            // 
            // Alap_Azonosító
            // 
            this.Alap_Azonosító.DropDownHeight = 350;
            this.Alap_Azonosító.FormattingEnabled = true;
            this.Alap_Azonosító.IntegralHeight = false;
            this.Alap_Azonosító.Location = new System.Drawing.Point(120, 6);
            this.Alap_Azonosító.MaxLength = 20;
            this.Alap_Azonosító.Name = "Alap_Azonosító";
            this.Alap_Azonosító.Size = new System.Drawing.Size(180, 28);
            this.Alap_Azonosító.TabIndex = 0;
            this.Alap_Azonosító.TextChanged += new System.EventHandler(this.Alap_Azonosító_TextChanged);
            this.Alap_Azonosító.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Alap_Azonosító_MouseClick);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.Silver;
            this.Label3.Location = new System.Drawing.Point(329, 47);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(54, 20);
            this.Label3.TabIndex = 98;
            this.Label3.Text = "Méret:";
            // 
            // Alap_Aktív
            // 
            this.Alap_Aktív.AutoSize = true;
            this.Alap_Aktív.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.Alap_Aktív.Location = new System.Drawing.Point(724, 108);
            this.Alap_Aktív.Name = "Alap_Aktív";
            this.Alap_Aktív.Size = new System.Drawing.Size(106, 24);
            this.Alap_Aktív.TabIndex = 101;
            this.Alap_Aktív.Text = "Aktív/Törölt";
            this.Alap_Aktív.UseVisualStyleBackColor = false;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.BackColor = System.Drawing.Color.Silver;
            this.Label4.Location = new System.Drawing.Point(632, 14);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(94, 20);
            this.Label4.TabIndex = 99;
            this.Label4.Text = "Költséghely:";
            // 
            // Alap_Új_adat
            // 
            this.Alap_Új_adat.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Alap_Új_adat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Alap_Új_adat.Location = new System.Drawing.Point(1066, 8);
            this.Alap_Új_adat.Name = "Alap_Új_adat";
            this.Alap_Új_adat.Size = new System.Drawing.Size(45, 45);
            this.Alap_Új_adat.TabIndex = 11;
            this.toolTip1.SetToolTip(this.Alap_Új_adat, "A rögzítési mezőket kiüríti");
            this.Alap_Új_adat.UseVisualStyleBackColor = true;
            this.Alap_Új_adat.Click += new System.EventHandler(this.Új_adat_Click);
            // 
            // Alap_Rögzít
            // 
            this.Alap_Rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Alap_Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Alap_Rögzít.Location = new System.Drawing.Point(1168, 8);
            this.Alap_Rögzít.Name = "Alap_Rögzít";
            this.Alap_Rögzít.Size = new System.Drawing.Size(45, 45);
            this.Alap_Rögzít.TabIndex = 8;
            this.toolTip1.SetToolTip(this.Alap_Rögzít, "Rögzíti/Módosítja az adatokat");
            this.Alap_Rögzít.UseVisualStyleBackColor = true;
            this.Alap_Rögzít.Click += new System.EventHandler(this.Rögzít_Click);
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.BackColor = System.Drawing.Color.Silver;
            this.Label5.Location = new System.Drawing.Point(632, 47);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(146, 20);
            this.Label5.TabIndex = 100;
            this.Label5.Text = "Beszerzés dátuma:";
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.DodgerBlue;
            this.TabPage2.Controls.Add(this.Könyv_excel);
            this.TabPage2.Controls.Add(this.Könyv_tábla);
            this.TabPage2.Controls.Add(this.Könyv_Felelős2);
            this.TabPage2.Controls.Add(this.Könyv_Felelős1);
            this.TabPage2.Controls.Add(this.Label9);
            this.TabPage2.Controls.Add(this.Label8);
            this.TabPage2.Controls.Add(this.Könyv_Töröltek);
            this.TabPage2.Controls.Add(this.Könyv_megnevezés);
            this.TabPage2.Controls.Add(this.Könyv_szám);
            this.TabPage2.Controls.Add(this.Könyv_Törlés);
            this.TabPage2.Controls.Add(this.Label6);
            this.TabPage2.Controls.Add(this.Label7);
            this.TabPage2.Controls.Add(this.Könyv_új);
            this.TabPage2.Controls.Add(this.Könyv_Rögzít);
            this.TabPage2.Controls.Add(this.Frissít);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(1219, 392);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Könyvek";
            // 
            // Könyv_excel
            // 
            this.Könyv_excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Könyv_excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Könyv_excel.Location = new System.Drawing.Point(826, 6);
            this.Könyv_excel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Könyv_excel.Name = "Könyv_excel";
            this.Könyv_excel.Size = new System.Drawing.Size(45, 45);
            this.Könyv_excel.TabIndex = 8;
            this.toolTip1.SetToolTip(this.Könyv_excel, "Excel táblázatot készít a táblázat adataiból");
            this.Könyv_excel.UseVisualStyleBackColor = true;
            this.Könyv_excel.Click += new System.EventHandler(this.Könyv_excel_Click);
            // 
            // Könyv_tábla
            // 
            this.Könyv_tábla.AllowUserToAddRows = false;
            this.Könyv_tábla.AllowUserToDeleteRows = false;
            this.Könyv_tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Könyv_tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Könyv_tábla.FilterAndSortEnabled = true;
            this.Könyv_tábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Könyv_tábla.Location = new System.Drawing.Point(5, 161);
            this.Könyv_tábla.MaxFilterButtonImageHeight = 23;
            this.Könyv_tábla.Name = "Könyv_tábla";
            this.Könyv_tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Könyv_tábla.RowHeadersVisible = false;
            this.Könyv_tábla.RowHeadersWidth = 51;
            this.Könyv_tábla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Könyv_tábla.Size = new System.Drawing.Size(1208, 225);
            this.Könyv_tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Könyv_tábla.TabIndex = 169;
            this.Könyv_tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Könyv_tábla_CellClick);
            // 
            // Könyv_Felelős2
            // 
            this.Könyv_Felelős2.DropDownHeight = 350;
            this.Könyv_Felelős2.FormattingEnabled = true;
            this.Könyv_Felelős2.IntegralHeight = false;
            this.Könyv_Felelős2.Location = new System.Drawing.Point(191, 126);
            this.Könyv_Felelős2.MaxLength = 50;
            this.Könyv_Felelős2.Name = "Könyv_Felelős2";
            this.Könyv_Felelős2.Size = new System.Drawing.Size(393, 28);
            this.Könyv_Felelős2.TabIndex = 3;
            // 
            // Könyv_Felelős1
            // 
            this.Könyv_Felelős1.DropDownHeight = 350;
            this.Könyv_Felelős1.FormattingEnabled = true;
            this.Könyv_Felelős1.IntegralHeight = false;
            this.Könyv_Felelős1.Location = new System.Drawing.Point(191, 90);
            this.Könyv_Felelős1.MaxLength = 50;
            this.Könyv_Felelős1.Name = "Könyv_Felelős1";
            this.Könyv_Felelős1.Size = new System.Drawing.Size(393, 28);
            this.Könyv_Felelős1.TabIndex = 2;
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.BackColor = System.Drawing.Color.Silver;
            this.Label9.Location = new System.Drawing.Point(10, 61);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(103, 20);
            this.Label9.TabIndex = 111;
            this.Label9.Text = "Megnevezés:";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.BackColor = System.Drawing.Color.Silver;
            this.Label8.Location = new System.Drawing.Point(10, 134);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(77, 20);
            this.Label8.TabIndex = 110;
            this.Label8.Text = "Felelős 2:";
            // 
            // Könyv_Töröltek
            // 
            this.Könyv_Töröltek.AutoSize = true;
            this.Könyv_Töröltek.BackColor = System.Drawing.Color.Gold;
            this.Könyv_Töröltek.Location = new System.Drawing.Point(755, 74);
            this.Könyv_Töröltek.Name = "Könyv_Töröltek";
            this.Könyv_Töröltek.Size = new System.Drawing.Size(131, 24);
            this.Könyv_Töröltek.TabIndex = 9;
            this.Könyv_Töröltek.Text = "Törölt Könyvek";
            this.Könyv_Töröltek.UseVisualStyleBackColor = false;
            this.Könyv_Töröltek.CheckedChanged += new System.EventHandler(this.Töröltek_CheckedChanged_1);
            // 
            // Könyv_megnevezés
            // 
            this.Könyv_megnevezés.Location = new System.Drawing.Point(191, 55);
            this.Könyv_megnevezés.MaxLength = 50;
            this.Könyv_megnevezés.Name = "Könyv_megnevezés";
            this.Könyv_megnevezés.Size = new System.Drawing.Size(393, 26);
            this.Könyv_megnevezés.TabIndex = 1;
            // 
            // Könyv_szám
            // 
            this.Könyv_szám.DropDownWidth = 350;
            this.Könyv_szám.FormattingEnabled = true;
            this.Könyv_szám.Location = new System.Drawing.Point(191, 20);
            this.Könyv_szám.MaxLength = 10;
            this.Könyv_szám.Name = "Könyv_szám";
            this.Könyv_szám.Size = new System.Drawing.Size(180, 28);
            this.Könyv_szám.TabIndex = 0;
            this.Könyv_szám.SelectedIndexChanged += new System.EventHandler(this.Szerszámkönyvszám_SelectedIndexChanged);
            // 
            // Könyv_Törlés
            // 
            this.Könyv_Törlés.AutoSize = true;
            this.Könyv_Törlés.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.Könyv_Törlés.Location = new System.Drawing.Point(637, 131);
            this.Könyv_Törlés.Name = "Könyv_Törlés";
            this.Könyv_Törlés.Size = new System.Drawing.Size(68, 24);
            this.Könyv_Törlés.TabIndex = 4;
            this.Könyv_Törlés.Text = "Törölt";
            this.Könyv_Törlés.UseVisualStyleBackColor = false;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.BackColor = System.Drawing.Color.Silver;
            this.Label6.Location = new System.Drawing.Point(10, 28);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(175, 20);
            this.Label6.TabIndex = 98;
            this.Label6.Text = "Szerszámkönyv száma:";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.BackColor = System.Drawing.Color.Silver;
            this.Label7.Location = new System.Drawing.Point(10, 98);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(77, 20);
            this.Label7.TabIndex = 99;
            this.Label7.Text = "Felelős 1:";
            // 
            // Könyv_új
            // 
            this.Könyv_új.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Könyv_új.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Könyv_új.Location = new System.Drawing.Point(775, 6);
            this.Könyv_új.Name = "Könyv_új";
            this.Könyv_új.Size = new System.Drawing.Size(45, 45);
            this.Könyv_új.TabIndex = 6;
            this.toolTip1.SetToolTip(this.Könyv_új, "Új adatnak előkészíti a beviteli mezőt");
            this.Könyv_új.UseVisualStyleBackColor = true;
            this.Könyv_új.Click += new System.EventHandler(this.Könyv_új_Click);
            // 
            // Könyv_Rögzít
            // 
            this.Könyv_Rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Könyv_Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Könyv_Rögzít.Location = new System.Drawing.Point(897, 6);
            this.Könyv_Rögzít.Name = "Könyv_Rögzít";
            this.Könyv_Rögzít.Size = new System.Drawing.Size(45, 45);
            this.Könyv_Rögzít.TabIndex = 5;
            this.toolTip1.SetToolTip(this.Könyv_Rögzít, "Rögzíti/módosítja az adatokat");
            this.Könyv_Rögzít.UseVisualStyleBackColor = true;
            this.Könyv_Rögzít.Click += new System.EventHandler(this.Rögzít_Click_1);
            // 
            // Frissít
            // 
            this.Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Frissít.Location = new System.Drawing.Point(897, 55);
            this.Frissít.Name = "Frissít";
            this.Frissít.Size = new System.Drawing.Size(45, 45);
            this.Frissít.TabIndex = 7;
            this.toolTip1.SetToolTip(this.Frissít, "Frissíti a táblázatot");
            this.Frissít.UseVisualStyleBackColor = true;
            this.Frissít.Click += new System.EventHandler(this.Frissít_Click1);
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.SteelBlue;
            this.TabPage3.Controls.Add(this.groupBox2);
            this.TabPage3.Controls.Add(this.HováMennyiség);
            this.TabPage3.Controls.Add(this.HonnanMennyiség);
            this.TabPage3.Controls.Add(this.Könyvelés_tábla);
            this.TabPage3.Controls.Add(this.Megnevezés);
            this.TabPage3.Controls.Add(this.Label24);
            this.TabPage3.Controls.Add(this.Label22);
            this.TabPage3.Controls.Add(this.Label23);
            this.TabPage3.Controls.Add(this.SzerszámAzonosító);
            this.TabPage3.Controls.Add(this.Mennyiség);
            this.TabPage3.Controls.Add(this.Label21);
            this.TabPage3.Controls.Add(this.Label20);
            this.TabPage3.Controls.Add(this.HováNév);
            this.TabPage3.Controls.Add(this.Hova);
            this.TabPage3.Controls.Add(this.Label18);
            this.TabPage3.Controls.Add(this.HonnanNév);
            this.TabPage3.Controls.Add(this.Honnan);
            this.TabPage3.Controls.Add(this.Label19);
            this.TabPage3.Controls.Add(this.Rögzít);
            this.TabPage3.Location = new System.Drawing.Point(4, 29);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage3.Size = new System.Drawing.Size(1219, 392);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Könyvelés";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Könyvelés_szűrés_ürítés);
            this.groupBox2.Controls.Add(this.Könyvelés_Szűr);
            this.groupBox2.Controls.Add(this.Könyvelés_Méret);
            this.groupBox2.Controls.Add(this.label39);
            this.groupBox2.Controls.Add(this.Könyvelés_megnevezés);
            this.groupBox2.Controls.Add(this.Radio_könyv_Minden);
            this.groupBox2.Controls.Add(this.label40);
            this.groupBox2.Controls.Add(this.Radio_könyv_E);
            this.groupBox2.Controls.Add(this.Radio_könyv_A);
            this.groupBox2.Location = new System.Drawing.Point(7, 160);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1086, 64);
            this.groupBox2.TabIndex = 203;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Szűrés";
            // 
            // Könyvelés_szűrés_ürítés
            // 
            this.Könyvelés_szűrés_ürítés.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Könyvelés_szűrés_ürítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Könyvelés_szűrés_ürítés.Location = new System.Drawing.Point(983, 14);
            this.Könyvelés_szűrés_ürítés.Name = "Könyvelés_szűrés_ürítés";
            this.Könyvelés_szűrés_ürítés.Size = new System.Drawing.Size(45, 45);
            this.Könyvelés_szűrés_ürítés.TabIndex = 193;
            this.toolTip1.SetToolTip(this.Könyvelés_szűrés_ürítés, "A rögzítési mezőket kiüríti");
            this.Könyvelés_szűrés_ürítés.UseVisualStyleBackColor = true;
            this.Könyvelés_szűrés_ürítés.Click += new System.EventHandler(this.Könyvelés_szűrés_ürítés_Click);
            // 
            // Könyvelés_Szűr
            // 
            this.Könyvelés_Szűr.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Könyvelés_Szűr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Könyvelés_Szűr.Location = new System.Drawing.Point(1034, 14);
            this.Könyvelés_Szűr.Name = "Könyvelés_Szűr";
            this.Könyvelés_Szűr.Size = new System.Drawing.Size(45, 45);
            this.Könyvelés_Szűr.TabIndex = 192;
            this.toolTip1.SetToolTip(this.Könyvelés_Szűr, "Frissíti a képernyű adatait");
            this.Könyvelés_Szűr.UseVisualStyleBackColor = true;
            this.Könyvelés_Szűr.Click += new System.EventHandler(this.Könyvelés_Szűr_Click);
            // 
            // Könyvelés_Méret
            // 
            this.Könyvelés_Méret.Location = new System.Drawing.Point(784, 22);
            this.Könyvelés_Méret.MaxLength = 15;
            this.Könyvelés_Méret.Name = "Könyvelés_Méret";
            this.Könyvelés_Méret.Size = new System.Drawing.Size(180, 26);
            this.Könyvelés_Méret.TabIndex = 4;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.BackColor = System.Drawing.Color.Silver;
            this.label39.Location = new System.Drawing.Point(724, 26);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(54, 20);
            this.label39.TabIndex = 191;
            this.label39.Text = "Méret:";
            // 
            // Könyvelés_megnevezés
            // 
            this.Könyvelés_megnevezés.Location = new System.Drawing.Point(292, 23);
            this.Könyvelés_megnevezés.MaxLength = 50;
            this.Könyvelés_megnevezés.Name = "Könyvelés_megnevezés";
            this.Könyvelés_megnevezés.Size = new System.Drawing.Size(426, 26);
            this.Könyvelés_megnevezés.TabIndex = 3;
            // 
            // Radio_könyv_Minden
            // 
            this.Radio_könyv_Minden.AutoSize = true;
            this.Radio_könyv_Minden.BackColor = System.Drawing.Color.SkyBlue;
            this.Radio_könyv_Minden.Checked = true;
            this.Radio_könyv_Minden.Location = new System.Drawing.Point(6, 22);
            this.Radio_könyv_Minden.Name = "Radio_könyv_Minden";
            this.Radio_könyv_Minden.Size = new System.Drawing.Size(79, 24);
            this.Radio_könyv_Minden.TabIndex = 0;
            this.Radio_könyv_Minden.TabStop = true;
            this.Radio_könyv_Minden.Text = "Minden";
            this.Radio_könyv_Minden.UseVisualStyleBackColor = false;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.BackColor = System.Drawing.Color.Silver;
            this.label40.Location = new System.Drawing.Point(183, 26);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(103, 20);
            this.label40.TabIndex = 189;
            this.label40.Text = "Megnevezés:";
            // 
            // Radio_könyv_E
            // 
            this.Radio_könyv_E.AutoSize = true;
            this.Radio_könyv_E.BackColor = System.Drawing.Color.SkyBlue;
            this.Radio_könyv_E.Location = new System.Drawing.Point(139, 22);
            this.Radio_könyv_E.Name = "Radio_könyv_E";
            this.Radio_könyv_E.Size = new System.Drawing.Size(38, 24);
            this.Radio_könyv_E.TabIndex = 2;
            this.Radio_könyv_E.Text = "E";
            this.Radio_könyv_E.UseVisualStyleBackColor = false;
            // 
            // Radio_könyv_A
            // 
            this.Radio_könyv_A.AutoSize = true;
            this.Radio_könyv_A.BackColor = System.Drawing.Color.SkyBlue;
            this.Radio_könyv_A.Location = new System.Drawing.Point(91, 22);
            this.Radio_könyv_A.Name = "Radio_könyv_A";
            this.Radio_könyv_A.Size = new System.Drawing.Size(42, 24);
            this.Radio_könyv_A.TabIndex = 1;
            this.Radio_könyv_A.Text = "A ";
            this.Radio_könyv_A.UseVisualStyleBackColor = false;
            // 
            // HováMennyiség
            // 
            this.HováMennyiség.AutoSize = true;
            this.HováMennyiség.BackColor = System.Drawing.Color.Azure;
            this.HováMennyiség.Location = new System.Drawing.Point(1005, 18);
            this.HováMennyiség.Name = "HováMennyiség";
            this.HováMennyiség.Size = new System.Drawing.Size(27, 20);
            this.HováMennyiség.TabIndex = 202;
            this.HováMennyiség.Text = "<>";
            // 
            // HonnanMennyiség
            // 
            this.HonnanMennyiség.AutoSize = true;
            this.HonnanMennyiség.BackColor = System.Drawing.Color.Azure;
            this.HonnanMennyiség.Location = new System.Drawing.Point(481, 18);
            this.HonnanMennyiség.Name = "HonnanMennyiség";
            this.HonnanMennyiség.Size = new System.Drawing.Size(27, 20);
            this.HonnanMennyiség.TabIndex = 201;
            this.HonnanMennyiség.Text = "<>";
            // 
            // Könyvelés_tábla
            // 
            this.Könyvelés_tábla.AllowUserToAddRows = false;
            this.Könyvelés_tábla.AllowUserToDeleteRows = false;
            this.Könyvelés_tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Könyvelés_tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Könyvelés_tábla.FilterAndSortEnabled = true;
            this.Könyvelés_tábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Könyvelés_tábla.Location = new System.Drawing.Point(5, 230);
            this.Könyvelés_tábla.MaxFilterButtonImageHeight = 23;
            this.Könyvelés_tábla.Name = "Könyvelés_tábla";
            this.Könyvelés_tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Könyvelés_tábla.RowHeadersVisible = false;
            this.Könyvelés_tábla.RowHeadersWidth = 51;
            this.Könyvelés_tábla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Könyvelés_tábla.Size = new System.Drawing.Size(1208, 156);
            this.Könyvelés_tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Könyvelés_tábla.TabIndex = 4;
            this.Könyvelés_tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Könyvelés_tábla_CellClick);
            // 
            // Megnevezés
            // 
            this.Megnevezés.Location = new System.Drawing.Point(408, 83);
            this.Megnevezés.Name = "Megnevezés";
            this.Megnevezés.Size = new System.Drawing.Size(625, 26);
            this.Megnevezés.TabIndex = 3;
            // 
            // Label24
            // 
            this.Label24.AutoSize = true;
            this.Label24.BackColor = System.Drawing.Color.Silver;
            this.Label24.Location = new System.Drawing.Point(15, 124);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(85, 20);
            this.Label24.TabIndex = 195;
            this.Label24.Text = "Mennyiség";
            // 
            // Label22
            // 
            this.Label22.AutoSize = true;
            this.Label22.BackColor = System.Drawing.Color.Silver;
            this.Label22.Location = new System.Drawing.Point(15, 89);
            this.Label22.Name = "Label22";
            this.Label22.Size = new System.Drawing.Size(84, 20);
            this.Label22.TabIndex = 191;
            this.Label22.Text = "Azonosító:";
            // 
            // Label23
            // 
            this.Label23.AutoSize = true;
            this.Label23.BackColor = System.Drawing.Color.Silver;
            this.Label23.Location = new System.Drawing.Point(299, 89);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(103, 20);
            this.Label23.TabIndex = 193;
            this.Label23.Text = "Megnevezés:";
            // 
            // SzerszámAzonosító
            // 
            this.SzerszámAzonosító.DropDownHeight = 350;
            this.SzerszámAzonosító.FormattingEnabled = true;
            this.SzerszámAzonosító.IntegralHeight = false;
            this.SzerszámAzonosító.Location = new System.Drawing.Point(113, 81);
            this.SzerszámAzonosító.MaxLength = 20;
            this.SzerszámAzonosító.Name = "SzerszámAzonosító";
            this.SzerszámAzonosító.Size = new System.Drawing.Size(180, 28);
            this.SzerszámAzonosító.TabIndex = 2;
            this.SzerszámAzonosító.SelectedIndexChanged += new System.EventHandler(this.SzerszámAzonosító_SelectedIndexChanged_1);
            this.SzerszámAzonosító.TextUpdate += new System.EventHandler(this.SzerszámAzonosító_TextUpdate);
            // 
            // Mennyiség
            // 
            this.Mennyiség.Location = new System.Drawing.Point(113, 118);
            this.Mennyiség.Name = "Mennyiség";
            this.Mennyiség.Size = new System.Drawing.Size(180, 26);
            this.Mennyiség.TabIndex = 5;
            // 
            // Label21
            // 
            this.Label21.AutoSize = true;
            this.Label21.BackColor = System.Drawing.Color.Silver;
            this.Label21.Location = new System.Drawing.Point(409, 18);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(65, 20);
            this.Label21.TabIndex = 186;
            this.Label21.Text = "Készlet:";
            // 
            // Label20
            // 
            this.Label20.AutoSize = true;
            this.Label20.BackColor = System.Drawing.Color.Silver;
            this.Label20.Location = new System.Drawing.Point(934, 18);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(65, 20);
            this.Label20.TabIndex = 185;
            this.Label20.Text = "Készlet:";
            // 
            // HováNév
            // 
            this.HováNév.DropDownHeight = 350;
            this.HováNév.FormattingEnabled = true;
            this.HováNév.IntegralHeight = false;
            this.HováNév.Location = new System.Drawing.Point(539, 44);
            this.HováNév.MaxLength = 20;
            this.HováNév.Name = "HováNév";
            this.HováNév.Size = new System.Drawing.Size(496, 28);
            this.HováNév.Sorted = true;
            this.HováNév.TabIndex = 184;
            this.HováNév.SelectedIndexChanged += new System.EventHandler(this.HováNév_SelectedIndexChanged_1);
            // 
            // Hova
            // 
            this.Hova.DropDownHeight = 350;
            this.Hova.FormattingEnabled = true;
            this.Hova.IntegralHeight = false;
            this.Hova.Location = new System.Drawing.Point(595, 10);
            this.Hova.MaxLength = 20;
            this.Hova.Name = "Hova";
            this.Hova.Size = new System.Drawing.Size(180, 28);
            this.Hova.Sorted = true;
            this.Hova.TabIndex = 1;
            this.Hova.SelectedIndexChanged += new System.EventHandler(this.Hova_SelectedIndexChanged_1);
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.BackColor = System.Drawing.Color.Silver;
            this.Label18.Location = new System.Drawing.Point(539, 18);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(50, 20);
            this.Label18.TabIndex = 182;
            this.Label18.Text = "Hova:";
            // 
            // HonnanNév
            // 
            this.HonnanNév.DropDownHeight = 350;
            this.HonnanNév.FormattingEnabled = true;
            this.HonnanNév.IntegralHeight = false;
            this.HonnanNév.Location = new System.Drawing.Point(14, 44);
            this.HonnanNév.MaxLength = 20;
            this.HonnanNév.Name = "HonnanNév";
            this.HonnanNév.Size = new System.Drawing.Size(496, 28);
            this.HonnanNév.Sorted = true;
            this.HonnanNév.TabIndex = 181;
            this.HonnanNév.SelectedIndexChanged += new System.EventHandler(this.HonnanNév_SelectedIndexChanged_1);
            // 
            // Honnan
            // 
            this.Honnan.DropDownHeight = 350;
            this.Honnan.FormattingEnabled = true;
            this.Honnan.IntegralHeight = false;
            this.Honnan.Location = new System.Drawing.Point(113, 10);
            this.Honnan.MaxLength = 20;
            this.Honnan.Name = "Honnan";
            this.Honnan.Size = new System.Drawing.Size(180, 28);
            this.Honnan.Sorted = true;
            this.Honnan.TabIndex = 0;
            this.Honnan.SelectedIndexChanged += new System.EventHandler(this.Honnan_SelectedIndexChanged_1);
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.BackColor = System.Drawing.Color.Silver;
            this.Label19.Location = new System.Drawing.Point(16, 18);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(70, 20);
            this.Label19.TabIndex = 179;
            this.Label19.Text = "Honnan:";
            // 
            // Rögzít
            // 
            this.Rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Rögzít.Location = new System.Drawing.Point(988, 118);
            this.Rögzít.Name = "Rögzít";
            this.Rögzít.Size = new System.Drawing.Size(45, 45);
            this.Rögzít.TabIndex = 6;
            this.toolTip1.SetToolTip(this.Rögzít, "Rögzíti/módosítja az adatokat");
            this.Rögzít.UseVisualStyleBackColor = true;
            this.Rögzít.Click += new System.EventHandler(this.Rögzít_Click_2);
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.DarkTurquoise;
            this.TabPage4.Controls.Add(this.Lekérd_Szerszámkönyvszám);
            this.TabPage4.Controls.Add(this.Lekérd_Tábla);
            this.TabPage4.Controls.Add(this.Lekérd_Töröltek);
            this.TabPage4.Controls.Add(this.Label17);
            this.TabPage4.Controls.Add(this.groupBox4);
            this.TabPage4.Controls.Add(this.groupBox3);
            this.TabPage4.Controls.Add(this.Lekérd_Command1);
            this.TabPage4.Controls.Add(this.Lekérd_Excelclick);
            this.TabPage4.Controls.Add(this.Lekérd_Visszacsuk);
            this.TabPage4.Controls.Add(this.Lekérd_Jelöltszersz);
            this.TabPage4.Controls.Add(this.Lekérd_Mindtöröl);
            this.TabPage4.Controls.Add(this.Lekérd_Összeskijelöl);
            this.TabPage4.Controls.Add(this.Lekérd_Lenyit);
            this.TabPage4.Location = new System.Drawing.Point(4, 29);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage4.Size = new System.Drawing.Size(1219, 392);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Lekérdezés";
            // 
            // Lekérd_Szerszámkönyvszám
            // 
            this.Lekérd_Szerszámkönyvszám.CheckOnClick = true;
            this.Lekérd_Szerszámkönyvszám.FormattingEnabled = true;
            this.Lekérd_Szerszámkönyvszám.Location = new System.Drawing.Point(182, 20);
            this.Lekérd_Szerszámkönyvszám.Name = "Lekérd_Szerszámkönyvszám";
            this.Lekérd_Szerszámkönyvszám.Size = new System.Drawing.Size(412, 25);
            this.Lekérd_Szerszámkönyvszám.TabIndex = 0;
            // 
            // Lekérd_Tábla
            // 
            this.Lekérd_Tábla.AllowUserToAddRows = false;
            this.Lekérd_Tábla.AllowUserToDeleteRows = false;
            this.Lekérd_Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Lekérd_Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Lekérd_Tábla.FilterAndSortEnabled = true;
            this.Lekérd_Tábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Lekérd_Tábla.Location = new System.Drawing.Point(5, 207);
            this.Lekérd_Tábla.MaxFilterButtonImageHeight = 23;
            this.Lekérd_Tábla.Name = "Lekérd_Tábla";
            this.Lekérd_Tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Lekérd_Tábla.RowHeadersVisible = false;
            this.Lekérd_Tábla.RowHeadersWidth = 51;
            this.Lekérd_Tábla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Lekérd_Tábla.Size = new System.Drawing.Size(1208, 179);
            this.Lekérd_Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Lekérd_Tábla.TabIndex = 189;
            this.Lekérd_Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Lekérd_Tábla_CellClick);
            // 
            // Lekérd_Töröltek
            // 
            this.Lekérd_Töröltek.AutoSize = true;
            this.Lekérd_Töröltek.BackColor = System.Drawing.Color.Gold;
            this.Lekérd_Töröltek.Location = new System.Drawing.Point(832, 21);
            this.Lekérd_Töröltek.Name = "Lekérd_Töröltek";
            this.Lekérd_Töröltek.Size = new System.Drawing.Size(85, 24);
            this.Lekérd_Töröltek.TabIndex = 6;
            this.Lekérd_Töröltek.Text = "Töröltek";
            this.toolTip1.SetToolTip(this.Lekérd_Töröltek, "Törölt könyveket listázza");
            this.Lekérd_Töröltek.UseVisualStyleBackColor = false;
            this.Lekérd_Töröltek.CheckedChanged += new System.EventHandler(this.Töröltek_CheckedChanged_2);
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.BackColor = System.Drawing.Color.Silver;
            this.Label17.Location = new System.Drawing.Point(10, 25);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(166, 20);
            this.Label17.TabIndex = 101;
            this.Label17.Text = "Szerszámkönyv szám:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Label16);
            this.groupBox4.Controls.Add(this.Lekérd_Felelős1);
            this.groupBox4.Controls.Add(this.Lekérd_Nevekkiválasztása);
            this.groupBox4.Location = new System.Drawing.Point(8, 51);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(544, 63);
            this.groupBox4.TabIndex = 193;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Dolgozók könyveinek keresése";
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.BackColor = System.Drawing.Color.Silver;
            this.Label16.Location = new System.Drawing.Point(6, 31);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(64, 20);
            this.Label16.TabIndex = 100;
            this.Label16.Text = "Felelős:";
            // 
            // Lekérd_Felelős1
            // 
            this.Lekérd_Felelős1.DropDownHeight = 350;
            this.Lekérd_Felelős1.FormattingEnabled = true;
            this.Lekérd_Felelős1.IntegralHeight = false;
            this.Lekérd_Felelős1.Location = new System.Drawing.Point(76, 27);
            this.Lekérd_Felelős1.MaxLength = 20;
            this.Lekérd_Felelős1.Name = "Lekérd_Felelős1";
            this.Lekérd_Felelős1.Size = new System.Drawing.Size(412, 28);
            this.Lekérd_Felelős1.Sorted = true;
            this.Lekérd_Felelős1.TabIndex = 9;
            // 
            // Lekérd_Nevekkiválasztása
            // 
            this.Lekérd_Nevekkiválasztása.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Lekérd_Nevekkiválasztása.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lekérd_Nevekkiválasztása.Location = new System.Drawing.Point(494, 19);
            this.Lekérd_Nevekkiválasztása.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Lekérd_Nevekkiválasztása.Name = "Lekérd_Nevekkiválasztása";
            this.Lekérd_Nevekkiválasztása.Size = new System.Drawing.Size(40, 40);
            this.Lekérd_Nevekkiválasztása.TabIndex = 10;
            this.toolTip1.SetToolTip(this.Lekérd_Nevekkiválasztása, "Frissiti a táblázat adatait");
            this.Lekérd_Nevekkiválasztása.UseVisualStyleBackColor = true;
            this.Lekérd_Nevekkiválasztása.Click += new System.EventHandler(this.Nevekkiválasztása_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Lekérd_Méret);
            this.groupBox3.Controls.Add(this.label25);
            this.groupBox3.Controls.Add(this.Lekérd_Megnevezés);
            this.groupBox3.Controls.Add(this.Radio_lek_minden);
            this.groupBox3.Controls.Add(this.label41);
            this.groupBox3.Controls.Add(this.Radio_lek_E);
            this.groupBox3.Controls.Add(this.Radio_lek_A);
            this.groupBox3.Controls.Add(this.Lekérd_Töröltek1);
            this.groupBox3.Controls.Add(this.Lekérd_Szerszámazonosító);
            this.groupBox3.Controls.Add(this.Label15);
            this.groupBox3.Controls.Add(this.Lekérd_Anyagkiíró);
            this.groupBox3.Location = new System.Drawing.Point(8, 118);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1181, 83);
            this.groupBox3.TabIndex = 192;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Anyag és Eszköz kereső";
            // 
            // Lekérd_Méret
            // 
            this.Lekérd_Méret.Location = new System.Drawing.Point(768, 49);
            this.Lekérd_Méret.MaxLength = 15;
            this.Lekérd_Méret.Name = "Lekérd_Méret";
            this.Lekérd_Méret.Size = new System.Drawing.Size(180, 26);
            this.Lekérd_Méret.TabIndex = 4;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.Silver;
            this.label25.Location = new System.Drawing.Point(771, 22);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(54, 20);
            this.label25.TabIndex = 191;
            this.label25.Text = "Méret:";
            // 
            // Lekérd_Megnevezés
            // 
            this.Lekérd_Megnevezés.Location = new System.Drawing.Point(336, 50);
            this.Lekérd_Megnevezés.MaxLength = 50;
            this.Lekérd_Megnevezés.Name = "Lekérd_Megnevezés";
            this.Lekérd_Megnevezés.Size = new System.Drawing.Size(426, 26);
            this.Lekérd_Megnevezés.TabIndex = 3;
            // 
            // Radio_lek_minden
            // 
            this.Radio_lek_minden.AutoSize = true;
            this.Radio_lek_minden.BackColor = System.Drawing.Color.SkyBlue;
            this.Radio_lek_minden.Checked = true;
            this.Radio_lek_minden.Location = new System.Drawing.Point(6, 22);
            this.Radio_lek_minden.Name = "Radio_lek_minden";
            this.Radio_lek_minden.Size = new System.Drawing.Size(79, 24);
            this.Radio_lek_minden.TabIndex = 0;
            this.Radio_lek_minden.TabStop = true;
            this.Radio_lek_minden.Text = "Minden";
            this.Radio_lek_minden.UseVisualStyleBackColor = false;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.BackColor = System.Drawing.Color.Silver;
            this.label41.Location = new System.Drawing.Point(339, 22);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(103, 20);
            this.label41.TabIndex = 189;
            this.label41.Text = "Megnevezés:";
            // 
            // Radio_lek_E
            // 
            this.Radio_lek_E.AutoSize = true;
            this.Radio_lek_E.BackColor = System.Drawing.Color.SkyBlue;
            this.Radio_lek_E.Location = new System.Drawing.Point(54, 52);
            this.Radio_lek_E.Name = "Radio_lek_E";
            this.Radio_lek_E.Size = new System.Drawing.Size(38, 24);
            this.Radio_lek_E.TabIndex = 2;
            this.Radio_lek_E.Text = "E";
            this.Radio_lek_E.UseVisualStyleBackColor = false;
            // 
            // Radio_lek_A
            // 
            this.Radio_lek_A.AutoSize = true;
            this.Radio_lek_A.BackColor = System.Drawing.Color.SkyBlue;
            this.Radio_lek_A.Location = new System.Drawing.Point(6, 52);
            this.Radio_lek_A.Name = "Radio_lek_A";
            this.Radio_lek_A.Size = new System.Drawing.Size(42, 24);
            this.Radio_lek_A.TabIndex = 1;
            this.Radio_lek_A.Text = "A ";
            this.Radio_lek_A.UseVisualStyleBackColor = false;
            // 
            // Lekérd_Töröltek1
            // 
            this.Lekérd_Töröltek1.AutoSize = true;
            this.Lekérd_Töröltek1.BackColor = System.Drawing.Color.Gold;
            this.Lekérd_Töröltek1.Location = new System.Drawing.Point(956, 49);
            this.Lekérd_Töröltek1.Name = "Lekérd_Töröltek1";
            this.Lekérd_Töröltek1.Size = new System.Drawing.Size(169, 24);
            this.Lekérd_Töröltek1.TabIndex = 5;
            this.Lekérd_Töröltek1.Text = "Törölt azonosítókkal";
            this.Lekérd_Töröltek1.UseVisualStyleBackColor = false;
            // 
            // Lekérd_Szerszámazonosító
            // 
            this.Lekérd_Szerszámazonosító.DropDownHeight = 350;
            this.Lekérd_Szerszámazonosító.FormattingEnabled = true;
            this.Lekérd_Szerszámazonosító.IntegralHeight = false;
            this.Lekérd_Szerszámazonosító.Location = new System.Drawing.Point(103, 49);
            this.Lekérd_Szerszámazonosító.MaxLength = 20;
            this.Lekérd_Szerszámazonosító.Name = "Lekérd_Szerszámazonosító";
            this.Lekérd_Szerszámazonosító.Size = new System.Drawing.Size(223, 28);
            this.Lekérd_Szerszámazonosító.Sorted = true;
            this.Lekérd_Szerszámazonosító.TabIndex = 11;
            this.Lekérd_Szerszámazonosító.SelectedIndexChanged += new System.EventHandler(this.Szerszámazonosító_SelectedIndexChanged);
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.BackColor = System.Drawing.Color.Silver;
            this.Label15.Location = new System.Drawing.Point(103, 22);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(134, 20);
            this.Label15.TabIndex = 98;
            this.Label15.Text = "Eszköz azonosító";
            // 
            // Lekérd_Anyagkiíró
            // 
            this.Lekérd_Anyagkiíró.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Lekérd_Anyagkiíró.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lekérd_Anyagkiíró.Location = new System.Drawing.Point(1131, 30);
            this.Lekérd_Anyagkiíró.Name = "Lekérd_Anyagkiíró";
            this.Lekérd_Anyagkiíró.Size = new System.Drawing.Size(45, 45);
            this.Lekérd_Anyagkiíró.TabIndex = 13;
            this.toolTip1.SetToolTip(this.Lekérd_Anyagkiíró, "Frissiti a táblázat adatait");
            this.Lekérd_Anyagkiíró.UseVisualStyleBackColor = true;
            this.Lekérd_Anyagkiíró.Click += new System.EventHandler(this.Anyagkiíró_Click);
            // 
            // Lekérd_Command1
            // 
            this.Lekérd_Command1.BackgroundImage = global::Villamos.Properties.Resources.App_dict;
            this.Lekérd_Command1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lekérd_Command1.Location = new System.Drawing.Point(969, 5);
            this.Lekérd_Command1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Lekérd_Command1.Name = "Lekérd_Command1";
            this.Lekérd_Command1.Size = new System.Drawing.Size(40, 40);
            this.Lekérd_Command1.TabIndex = 8;
            this.toolTip1.SetToolTip(this.Lekérd_Command1, "Leltári bizonylatot készít");
            this.Lekérd_Command1.UseVisualStyleBackColor = true;
            this.Lekérd_Command1.Click += new System.EventHandler(this.Lekérd_Command1_Click);
            // 
            // Lekérd_Excelclick
            // 
            this.Lekérd_Excelclick.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Lekérd_Excelclick.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lekérd_Excelclick.Location = new System.Drawing.Point(923, 5);
            this.Lekérd_Excelclick.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Lekérd_Excelclick.Name = "Lekérd_Excelclick";
            this.Lekérd_Excelclick.Size = new System.Drawing.Size(40, 40);
            this.Lekérd_Excelclick.TabIndex = 7;
            this.toolTip1.SetToolTip(this.Lekérd_Excelclick, "Táblázat tartalmát Excelbe menti");
            this.Lekérd_Excelclick.UseVisualStyleBackColor = true;
            this.Lekérd_Excelclick.Click += new System.EventHandler(this.Excelclick_Click);
            // 
            // Lekérd_Visszacsuk
            // 
            this.Lekérd_Visszacsuk.BackgroundImage = global::Villamos.Properties.Resources.le;
            this.Lekérd_Visszacsuk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lekérd_Visszacsuk.Location = new System.Drawing.Point(600, 5);
            this.Lekérd_Visszacsuk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Lekérd_Visszacsuk.Name = "Lekérd_Visszacsuk";
            this.Lekérd_Visszacsuk.Size = new System.Drawing.Size(40, 40);
            this.Lekérd_Visszacsuk.TabIndex = 2;
            this.toolTip1.SetToolTip(this.Lekérd_Visszacsuk, "Lenyitja a listát");
            this.Lekérd_Visszacsuk.UseVisualStyleBackColor = true;
            this.Lekérd_Visszacsuk.Click += new System.EventHandler(this.Lenyit_Click);
            // 
            // Lekérd_Jelöltszersz
            // 
            this.Lekérd_Jelöltszersz.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Lekérd_Jelöltszersz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lekérd_Jelöltszersz.Location = new System.Drawing.Point(784, 5);
            this.Lekérd_Jelöltszersz.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Lekérd_Jelöltszersz.Name = "Lekérd_Jelöltszersz";
            this.Lekérd_Jelöltszersz.Size = new System.Drawing.Size(40, 40);
            this.Lekérd_Jelöltszersz.TabIndex = 5;
            this.toolTip1.SetToolTip(this.Lekérd_Jelöltszersz, "Frissiti a táblázat adatait");
            this.Lekérd_Jelöltszersz.UseVisualStyleBackColor = true;
            this.Lekérd_Jelöltszersz.Click += new System.EventHandler(this.Jelöltszersz_Click);
            // 
            // Lekérd_Mindtöröl
            // 
            this.Lekérd_Mindtöröl.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.Lekérd_Mindtöröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lekérd_Mindtöröl.Location = new System.Drawing.Point(738, 5);
            this.Lekérd_Mindtöröl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Lekérd_Mindtöröl.Name = "Lekérd_Mindtöröl";
            this.Lekérd_Mindtöröl.Size = new System.Drawing.Size(40, 40);
            this.Lekérd_Mindtöröl.TabIndex = 4;
            this.toolTip1.SetToolTip(this.Lekérd_Mindtöröl, "Minden kijelölést töröl");
            this.Lekérd_Mindtöröl.UseVisualStyleBackColor = true;
            this.Lekérd_Mindtöröl.Click += new System.EventHandler(this.Mindtöröl_Click);
            // 
            // Lekérd_Összeskijelöl
            // 
            this.Lekérd_Összeskijelöl.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.Lekérd_Összeskijelöl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lekérd_Összeskijelöl.Location = new System.Drawing.Point(692, 5);
            this.Lekérd_Összeskijelöl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Lekérd_Összeskijelöl.Name = "Lekérd_Összeskijelöl";
            this.Lekérd_Összeskijelöl.Size = new System.Drawing.Size(40, 40);
            this.Lekérd_Összeskijelöl.TabIndex = 3;
            this.toolTip1.SetToolTip(this.Lekérd_Összeskijelöl, "Minden elemet kijelöl");
            this.Lekérd_Összeskijelöl.UseVisualStyleBackColor = true;
            this.Lekérd_Összeskijelöl.Click += new System.EventHandler(this.Összeskijelöl_Click);
            // 
            // Lekérd_Lenyit
            // 
            this.Lekérd_Lenyit.BackgroundImage = global::Villamos.Properties.Resources.fel;
            this.Lekérd_Lenyit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lekérd_Lenyit.Location = new System.Drawing.Point(646, 5);
            this.Lekérd_Lenyit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Lekérd_Lenyit.Name = "Lekérd_Lenyit";
            this.Lekérd_Lenyit.Size = new System.Drawing.Size(40, 40);
            this.Lekérd_Lenyit.TabIndex = 1;
            this.toolTip1.SetToolTip(this.Lekérd_Lenyit, "Visszacsukja a listát");
            this.Lekérd_Lenyit.UseVisualStyleBackColor = true;
            this.Lekérd_Lenyit.Click += new System.EventHandler(this.Visszacsuk_Click);
            // 
            // TabPage5
            // 
            this.TabPage5.BackColor = System.Drawing.Color.SeaGreen;
            this.TabPage5.Controls.Add(this.Nyomtatvány9A);
            this.TabPage5.Controls.Add(this.Nyomtatvány9B);
            this.TabPage5.Controls.Add(this.Napló_Fájltöröl);
            this.TabPage5.Controls.Add(this.Napló_Hovánév);
            this.TabPage5.Controls.Add(this.Napló_Hova);
            this.TabPage5.Controls.Add(this.Label14);
            this.TabPage5.Controls.Add(this.Napló_Honnannév);
            this.TabPage5.Controls.Add(this.Napló_Honnan);
            this.TabPage5.Controls.Add(this.Label12);
            this.TabPage5.Controls.Add(this.Napló_Dátumig);
            this.TabPage5.Controls.Add(this.Label11);
            this.TabPage5.Controls.Add(this.Napló_Tábla);
            this.TabPage5.Controls.Add(this.Napló_Dátumtól);
            this.TabPage5.Controls.Add(this.Napló_Nyomtat);
            this.TabPage5.Controls.Add(this.Label10);
            this.TabPage5.Controls.Add(this.Napló_Nyomtatvány);
            this.TabPage5.Controls.Add(this.Napló_Excel_gomb);
            this.TabPage5.Controls.Add(this.Napló_Listáz);
            this.TabPage5.Location = new System.Drawing.Point(4, 29);
            this.TabPage5.Name = "TabPage5";
            this.TabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage5.Size = new System.Drawing.Size(1219, 392);
            this.TabPage5.TabIndex = 4;
            this.TabPage5.Text = "Naplózás";
            // 
            // Nyomtatvány9A
            // 
            this.Nyomtatvány9A.BackgroundImage = global::Villamos.Properties.Resources._9_A;
            this.Nyomtatvány9A.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Nyomtatvány9A.Location = new System.Drawing.Point(783, 28);
            this.Nyomtatvány9A.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Nyomtatvány9A.Name = "Nyomtatvány9A";
            this.Nyomtatvány9A.Size = new System.Drawing.Size(40, 40);
            this.Nyomtatvány9A.TabIndex = 190;
            this.toolTip1.SetToolTip(this.Nyomtatvány9A, "9A bizonylatot készít\r\nGyűjtő táblázatos");
            this.Nyomtatvány9A.UseVisualStyleBackColor = true;
            this.Nyomtatvány9A.Click += new System.EventHandler(this.Nyomtatvány9A_Click);
            // 
            // Nyomtatvány9B
            // 
            this.Nyomtatvány9B.BackgroundImage = global::Villamos.Properties.Resources._9_B;
            this.Nyomtatvány9B.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Nyomtatvány9B.Location = new System.Drawing.Point(829, 28);
            this.Nyomtatvány9B.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Nyomtatvány9B.Name = "Nyomtatvány9B";
            this.Nyomtatvány9B.Size = new System.Drawing.Size(40, 40);
            this.Nyomtatvány9B.TabIndex = 189;
            this.toolTip1.SetToolTip(this.Nyomtatvány9B, "9B nyomtatványt készít\r\nSzemélyre felvett/leadott");
            this.Nyomtatvány9B.UseVisualStyleBackColor = true;
            this.Nyomtatvány9B.Click += new System.EventHandler(this.Nyomtatvány9B_Click);
            // 
            // Napló_Fájltöröl
            // 
            this.Napló_Fájltöröl.AutoSize = true;
            this.Napló_Fájltöröl.BackColor = System.Drawing.Color.Gold;
            this.Napló_Fájltöröl.Location = new System.Drawing.Point(514, 42);
            this.Napló_Fájltöröl.Name = "Napló_Fájltöröl";
            this.Napló_Fájltöröl.Size = new System.Drawing.Size(159, 24);
            this.Napló_Fájltöröl.TabIndex = 7;
            this.Napló_Fájltöröl.Text = "Bizonylati fájlt töröl";
            this.Napló_Fájltöröl.UseVisualStyleBackColor = false;
            // 
            // Napló_Hovánév
            // 
            this.Napló_Hovánév.DropDownHeight = 350;
            this.Napló_Hovánév.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Napló_Hovánév.FormattingEnabled = true;
            this.Napló_Hovánév.IntegralHeight = false;
            this.Napló_Hovánév.Location = new System.Drawing.Point(514, 106);
            this.Napló_Hovánév.MaxLength = 20;
            this.Napló_Hovánév.Name = "Napló_Hovánév";
            this.Napló_Hovánév.Size = new System.Drawing.Size(496, 28);
            this.Napló_Hovánév.Sorted = true;
            this.Napló_Hovánév.TabIndex = 178;
            this.Napló_Hovánév.SelectedIndexChanged += new System.EventHandler(this.Hovánév_SelectedIndexChanged);
            // 
            // Napló_Hova
            // 
            this.Napló_Hova.DropDownHeight = 350;
            this.Napló_Hova.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Napló_Hova.FormattingEnabled = true;
            this.Napló_Hova.IntegralHeight = false;
            this.Napló_Hova.Location = new System.Drawing.Point(830, 72);
            this.Napló_Hova.MaxLength = 20;
            this.Napló_Hova.Name = "Napló_Hova";
            this.Napló_Hova.Size = new System.Drawing.Size(180, 28);
            this.Napló_Hova.Sorted = true;
            this.Napló_Hova.TabIndex = 5;
            this.Napló_Hova.SelectionChangeCommitted += new System.EventHandler(this.Napló_Hova_SelectionChangeCommitted);
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.BackColor = System.Drawing.Color.Silver;
            this.Label14.Location = new System.Drawing.Point(515, 80);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(50, 20);
            this.Label14.TabIndex = 176;
            this.Label14.Text = "Hova:";
            // 
            // Napló_Honnannév
            // 
            this.Napló_Honnannév.DropDownHeight = 350;
            this.Napló_Honnannév.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Napló_Honnannév.FormattingEnabled = true;
            this.Napló_Honnannév.IntegralHeight = false;
            this.Napló_Honnannév.Location = new System.Drawing.Point(13, 106);
            this.Napló_Honnannév.MaxLength = 20;
            this.Napló_Honnannév.Name = "Napló_Honnannév";
            this.Napló_Honnannév.Size = new System.Drawing.Size(496, 28);
            this.Napló_Honnannév.Sorted = true;
            this.Napló_Honnannév.TabIndex = 175;
            this.Napló_Honnannév.SelectedIndexChanged += new System.EventHandler(this.Honnannév_SelectedIndexChanged);
            // 
            // Napló_Honnan
            // 
            this.Napló_Honnan.DropDownHeight = 350;
            this.Napló_Honnan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Napló_Honnan.FormattingEnabled = true;
            this.Napló_Honnan.IntegralHeight = false;
            this.Napló_Honnan.Location = new System.Drawing.Point(329, 72);
            this.Napló_Honnan.MaxLength = 20;
            this.Napló_Honnan.Name = "Napló_Honnan";
            this.Napló_Honnan.Size = new System.Drawing.Size(180, 28);
            this.Napló_Honnan.Sorted = true;
            this.Napló_Honnan.TabIndex = 4;
            this.Napló_Honnan.SelectionChangeCommitted += new System.EventHandler(this.Napló_Honnan_SelectionChangeCommitted);
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.BackColor = System.Drawing.Color.Silver;
            this.Label12.Location = new System.Drawing.Point(14, 80);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(70, 20);
            this.Label12.TabIndex = 173;
            this.Label12.Text = "Honnan:";
            // 
            // Napló_Dátumig
            // 
            this.Napló_Dátumig.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Napló_Dátumig.Location = new System.Drawing.Point(98, 41);
            this.Napló_Dátumig.Name = "Napló_Dátumig";
            this.Napló_Dátumig.Size = new System.Drawing.Size(118, 26);
            this.Napló_Dátumig.TabIndex = 1;
            this.Napló_Dátumig.ValueChanged += new System.EventHandler(this.Dátumig_ValueChanged);
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.BackColor = System.Drawing.Color.Silver;
            this.Label11.Location = new System.Drawing.Point(14, 14);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(78, 20);
            this.Label11.TabIndex = 171;
            this.Label11.Text = "Dátumtól:";
            // 
            // Napló_Tábla
            // 
            this.Napló_Tábla.AllowUserToAddRows = false;
            this.Napló_Tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.Napló_Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Napló_Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Napló_Tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Napló_Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Napló_Tábla.EnableHeadersVisualStyles = false;
            this.Napló_Tábla.FilterAndSortEnabled = true;
            this.Napló_Tábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Napló_Tábla.Location = new System.Drawing.Point(6, 140);
            this.Napló_Tábla.MaxFilterButtonImageHeight = 23;
            this.Napló_Tábla.Name = "Napló_Tábla";
            this.Napló_Tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Napló_Tábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Napló_Tábla.RowHeadersWidth = 51;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.Napló_Tábla.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.Napló_Tábla.Size = new System.Drawing.Size(1207, 246);
            this.Napló_Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Napló_Tábla.TabIndex = 170;
            // 
            // Napló_Dátumtól
            // 
            this.Napló_Dátumtól.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Napló_Dátumtól.Location = new System.Drawing.Point(98, 8);
            this.Napló_Dátumtól.Name = "Napló_Dátumtól";
            this.Napló_Dátumtól.Size = new System.Drawing.Size(118, 26);
            this.Napló_Dátumtól.TabIndex = 0;
            this.Napló_Dátumtól.ValueChanged += new System.EventHandler(this.Dátumtól_ValueChanged);
            // 
            // Napló_Nyomtat
            // 
            this.Napló_Nyomtat.AutoSize = true;
            this.Napló_Nyomtat.BackColor = System.Drawing.Color.Gold;
            this.Napló_Nyomtat.Location = new System.Drawing.Point(514, 9);
            this.Napló_Nyomtat.Name = "Napló_Nyomtat";
            this.Napló_Nyomtat.Size = new System.Drawing.Size(154, 24);
            this.Napló_Nyomtat.TabIndex = 6;
            this.Napló_Nyomtat.Text = "Nyomtatást készít";
            this.Napló_Nyomtat.UseVisualStyleBackColor = false;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.BackColor = System.Drawing.Color.Silver;
            this.Label10.Location = new System.Drawing.Point(14, 46);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(73, 20);
            this.Label10.TabIndex = 104;
            this.Label10.Text = "Dátumig:";
            // 
            // Napló_Nyomtatvány
            // 
            this.Napló_Nyomtatvány.BackgroundImage = global::Villamos.Properties.Resources.Document_write;
            this.Napló_Nyomtatvány.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Napló_Nyomtatvány.Location = new System.Drawing.Point(691, 28);
            this.Napló_Nyomtatvány.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Napló_Nyomtatvány.Name = "Napló_Nyomtatvány";
            this.Napló_Nyomtatvány.Size = new System.Drawing.Size(40, 40);
            this.Napló_Nyomtatvány.TabIndex = 8;
            this.toolTip1.SetToolTip(this.Napló_Nyomtatvány, "Szerszám felvételről elkészíti a bizonylatot.");
            this.Napló_Nyomtatvány.UseVisualStyleBackColor = true;
            this.Napló_Nyomtatvány.Click += new System.EventHandler(this.Nyomtatvány_Click);
            // 
            // Napló_Excel_gomb
            // 
            this.Napló_Excel_gomb.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Napló_Excel_gomb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Napló_Excel_gomb.Location = new System.Drawing.Point(273, 25);
            this.Napló_Excel_gomb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Napló_Excel_gomb.Name = "Napló_Excel_gomb";
            this.Napló_Excel_gomb.Size = new System.Drawing.Size(40, 40);
            this.Napló_Excel_gomb.TabIndex = 3;
            this.toolTip1.SetToolTip(this.Napló_Excel_gomb, "Excel táblázatot készít a táblázat adataiból");
            this.Napló_Excel_gomb.UseVisualStyleBackColor = true;
            this.Napló_Excel_gomb.Click += new System.EventHandler(this.Excel_gomb_Click);
            // 
            // Napló_Listáz
            // 
            this.Napló_Listáz.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Napló_Listáz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Napló_Listáz.Location = new System.Drawing.Point(227, 25);
            this.Napló_Listáz.Name = "Napló_Listáz";
            this.Napló_Listáz.Size = new System.Drawing.Size(40, 40);
            this.Napló_Listáz.TabIndex = 2;
            this.toolTip1.SetToolTip(this.Napló_Listáz, "Frissiti a képernyőt");
            this.Napló_Listáz.UseVisualStyleBackColor = true;
            this.Napló_Listáz.Click += new System.EventHandler(this.Listáz_Click);
            // 
            // TabPage6
            // 
            this.TabPage6.BackColor = System.Drawing.Color.DarkTurquoise;
            this.TabPage6.Controls.Add(this.Mentés);
            this.TabPage6.Controls.Add(this.Kép_szűrés);
            this.TabPage6.Controls.Add(this.Kép_megnevezés);
            this.TabPage6.Controls.Add(this.Label35);
            this.TabPage6.Controls.Add(this.Label30);
            this.TabPage6.Controls.Add(this.Label32);
            this.TabPage6.Controls.Add(this.Kép_Feltöltendő);
            this.TabPage6.Controls.Add(this.Label33);
            this.TabPage6.Controls.Add(this.KépTörlés);
            this.TabPage6.Controls.Add(this.Kép_btn);
            this.TabPage6.Controls.Add(this.PictureBox1);
            this.TabPage6.Controls.Add(this.Kép_listbox);
            this.TabPage6.Controls.Add(this.Kép_Azonosító);
            this.TabPage6.Controls.Add(this.Kép_Listázás);
            this.TabPage6.Controls.Add(this.Kép_rögzít);
            this.TabPage6.Location = new System.Drawing.Point(4, 29);
            this.TabPage6.Name = "TabPage6";
            this.TabPage6.Size = new System.Drawing.Size(1219, 392);
            this.TabPage6.TabIndex = 5;
            this.TabPage6.Text = "Képek";
            // 
            // Mentés
            // 
            this.Mentés.BackgroundImage = global::Villamos.Properties.Resources.Designcontest_Ecommerce_Business_Save;
            this.Mentés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Mentés.Location = new System.Drawing.Point(110, 35);
            this.Mentés.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Mentés.Name = "Mentés";
            this.Mentés.Size = new System.Drawing.Size(45, 45);
            this.Mentés.TabIndex = 2;
            this.toolTip1.SetToolTip(this.Mentés, "Menti a kiálasztott kép(ek)et");
            this.Mentés.UseVisualStyleBackColor = true;
            this.Mentés.Click += new System.EventHandler(this.Mentés_Click);
            // 
            // Kép_szűrés
            // 
            this.Kép_szűrés.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Kép_szűrés.FormattingEnabled = true;
            this.Kép_szűrés.ItemHeight = 20;
            this.Kép_szűrés.Location = new System.Drawing.Point(445, 74);
            this.Kép_szűrés.Name = "Kép_szűrés";
            this.Kép_szűrés.Size = new System.Drawing.Size(163, 224);
            this.Kép_szűrés.TabIndex = 9;
            this.Kép_szűrés.Visible = false;
            // 
            // Kép_megnevezés
            // 
            this.Kép_megnevezés.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Kép_megnevezés.Location = new System.Drawing.Point(449, 6);
            this.Kép_megnevezés.MaxLength = 50;
            this.Kép_megnevezés.Name = "Kép_megnevezés";
            this.Kép_megnevezés.Size = new System.Drawing.Size(637, 26);
            this.Kép_megnevezés.TabIndex = 7;
            // 
            // Label35
            // 
            this.Label35.AutoSize = true;
            this.Label35.BackColor = System.Drawing.Color.Silver;
            this.Label35.Location = new System.Drawing.Point(322, 12);
            this.Label35.Name = "Label35";
            this.Label35.Size = new System.Drawing.Size(103, 20);
            this.Label35.TabIndex = 190;
            this.Label35.Text = "Megnevezés:";
            // 
            // Label30
            // 
            this.Label30.AutoSize = true;
            this.Label30.BackColor = System.Drawing.Color.Silver;
            this.Label30.Location = new System.Drawing.Point(3, 12);
            this.Label30.Name = "Label30";
            this.Label30.Size = new System.Drawing.Size(84, 20);
            this.Label30.TabIndex = 187;
            this.Label30.Text = "Azonosító:";
            // 
            // Label32
            // 
            this.Label32.AutoSize = true;
            this.Label32.BackColor = System.Drawing.Color.Silver;
            this.Label32.Location = new System.Drawing.Point(322, 42);
            this.Label32.Name = "Label32";
            this.Label32.Size = new System.Drawing.Size(121, 20);
            this.Label32.TabIndex = 186;
            this.Label32.Text = "Feltöltendő fájl :";
            // 
            // Kép_Feltöltendő
            // 
            this.Kép_Feltöltendő.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Kép_Feltöltendő.Location = new System.Drawing.Point(449, 38);
            this.Kép_Feltöltendő.Name = "Kép_Feltöltendő";
            this.Kép_Feltöltendő.Size = new System.Drawing.Size(637, 26);
            this.Kép_Feltöltendő.TabIndex = 8;
            // 
            // Label33
            // 
            this.Label33.AutoSize = true;
            this.Label33.Location = new System.Drawing.Point(3, 85);
            this.Label33.Name = "Label33";
            this.Label33.Size = new System.Drawing.Size(123, 20);
            this.Label33.TabIndex = 183;
            this.Label33.Text = "Feltöltött képek:";
            // 
            // KépTörlés
            // 
            this.KépTörlés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.KépTörlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.KépTörlés.Location = new System.Drawing.Point(270, 108);
            this.KépTörlés.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.KépTörlés.Name = "KépTörlés";
            this.KépTörlés.Size = new System.Drawing.Size(45, 45);
            this.KépTörlés.TabIndex = 5;
            this.toolTip1.SetToolTip(this.KépTörlés, "Törli a kiválasztott képet");
            this.KépTörlés.UseVisualStyleBackColor = true;
            this.KépTörlés.Click += new System.EventHandler(this.KépTörlés_Click);
            // 
            // Kép_btn
            // 
            this.Kép_btn.BackgroundImage = global::Villamos.Properties.Resources.Folder_;
            this.Kép_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Kép_btn.Location = new System.Drawing.Point(214, 38);
            this.Kép_btn.Name = "Kép_btn";
            this.Kép_btn.Size = new System.Drawing.Size(45, 45);
            this.Kép_btn.TabIndex = 3;
            this.toolTip1.SetToolTip(this.Kép_btn, "Kép kiválasztása");
            this.Kép_btn.UseVisualStyleBackColor = true;
            this.Kép_btn.Click += new System.EventHandler(this.Kép_btn_Click);
            // 
            // PictureBox1
            // 
            this.PictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureBox1.Location = new System.Drawing.Point(322, 70);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(882, 311);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBox1.TabIndex = 189;
            this.PictureBox1.TabStop = false;
            // 
            // Kép_listbox
            // 
            this.Kép_listbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Kép_listbox.FormattingEnabled = true;
            this.Kép_listbox.ItemHeight = 20;
            this.Kép_listbox.Location = new System.Drawing.Point(4, 108);
            this.Kép_listbox.Name = "Kép_listbox";
            this.Kép_listbox.Size = new System.Drawing.Size(259, 244);
            this.Kép_listbox.TabIndex = 6;
            this.Kép_listbox.SelectedIndexChanged += new System.EventHandler(this.Kép_listbox_SelectedIndexChanged);
            // 
            // Kép_Azonosító
            // 
            this.Kép_Azonosító.DropDownHeight = 350;
            this.Kép_Azonosító.FormattingEnabled = true;
            this.Kép_Azonosító.IntegralHeight = false;
            this.Kép_Azonosító.Location = new System.Drawing.Point(110, 4);
            this.Kép_Azonosító.Name = "Kép_Azonosító";
            this.Kép_Azonosító.Size = new System.Drawing.Size(187, 28);
            this.Kép_Azonosító.TabIndex = 0;
            this.Kép_Azonosító.SelectedIndexChanged += new System.EventHandler(this.Képek_azonosító_SelectedIndexChanged);
            // 
            // Kép_Listázás
            // 
            this.Kép_Listázás.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Kép_Listázás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Kép_Listázás.Location = new System.Drawing.Point(7, 38);
            this.Kép_Listázás.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Kép_Listázás.Name = "Kép_Listázás";
            this.Kép_Listázás.Size = new System.Drawing.Size(45, 45);
            this.Kép_Listázás.TabIndex = 1;
            this.toolTip1.SetToolTip(this.Kép_Listázás, "Frissít");
            this.Kép_Listázás.UseVisualStyleBackColor = true;
            this.Kép_Listázás.Click += new System.EventHandler(this.Kép_Listázás_Click);
            // 
            // Kép_rögzít
            // 
            this.Kép_rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Kép_rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Kép_rögzít.Location = new System.Drawing.Point(271, 38);
            this.Kép_rögzít.Name = "Kép_rögzít";
            this.Kép_rögzít.Size = new System.Drawing.Size(45, 45);
            this.Kép_rögzít.TabIndex = 4;
            this.toolTip1.SetToolTip(this.Kép_rögzít, "Menti a képeket a hálózatra");
            this.Kép_rögzít.UseVisualStyleBackColor = true;
            this.Kép_rögzít.Click += new System.EventHandler(this.Kép_rögzít_Click);
            // 
            // TabPage7
            // 
            this.TabPage7.BackColor = System.Drawing.Color.DarkTurquoise;
            this.TabPage7.Controls.Add(this.Szűrés);
            this.TabPage7.Controls.Add(this.PDF_néző);
            this.TabPage7.Controls.Add(this.PDF_megnevezés);
            this.TabPage7.Controls.Add(this.Label29);
            this.TabPage7.Controls.Add(this.Label28);
            this.TabPage7.Controls.Add(this.Label38);
            this.TabPage7.Controls.Add(this.Feltöltendő);
            this.TabPage7.Controls.Add(this.Label27);
            this.TabPage7.Controls.Add(this.PDF_törlés);
            this.TabPage7.Controls.Add(this.BtnPDF);
            this.TabPage7.Controls.Add(this.Pdf_listbox);
            this.TabPage7.Controls.Add(this.PDF_Azonosító);
            this.TabPage7.Controls.Add(this.PDF_Frissít);
            this.TabPage7.Controls.Add(this.PDF_rögzít);
            this.TabPage7.Location = new System.Drawing.Point(4, 29);
            this.TabPage7.Name = "TabPage7";
            this.TabPage7.Size = new System.Drawing.Size(1219, 392);
            this.TabPage7.TabIndex = 6;
            this.TabPage7.Text = "PDF";
            // 
            // Szűrés
            // 
            this.Szűrés.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Szűrés.FormattingEnabled = true;
            this.Szűrés.ItemHeight = 20;
            this.Szűrés.Location = new System.Drawing.Point(380, 100);
            this.Szűrés.Name = "Szűrés";
            this.Szűrés.Size = new System.Drawing.Size(163, 224);
            this.Szűrés.TabIndex = 8;
            this.Szűrés.Visible = false;
            // 
            // PDF_néző
            // 
            this.PDF_néző.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PDF_néző.Location = new System.Drawing.Point(307, 88);
            this.PDF_néző.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PDF_néző.Name = "PDF_néző";
            this.PDF_néző.Size = new System.Drawing.Size(908, 299);
            this.PDF_néző.TabIndex = 240;
            this.PDF_néző.ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitWidth;
            // 
            // PDF_megnevezés
            // 
            this.PDF_megnevezés.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PDF_megnevezés.Location = new System.Drawing.Point(430, 6);
            this.PDF_megnevezés.MaxLength = 50;
            this.PDF_megnevezés.Name = "PDF_megnevezés";
            this.PDF_megnevezés.Size = new System.Drawing.Size(619, 26);
            this.PDF_megnevezés.TabIndex = 6;
            // 
            // Label29
            // 
            this.Label29.AutoSize = true;
            this.Label29.BackColor = System.Drawing.Color.Silver;
            this.Label29.Location = new System.Drawing.Point(303, 12);
            this.Label29.Name = "Label29";
            this.Label29.Size = new System.Drawing.Size(103, 20);
            this.Label29.TabIndex = 192;
            this.Label29.Text = "Megnevezés:";
            // 
            // Label28
            // 
            this.Label28.AutoSize = true;
            this.Label28.BackColor = System.Drawing.Color.Silver;
            this.Label28.Location = new System.Drawing.Point(3, 12);
            this.Label28.Name = "Label28";
            this.Label28.Size = new System.Drawing.Size(84, 20);
            this.Label28.TabIndex = 176;
            this.Label28.Text = "Azonosító:";
            // 
            // Label38
            // 
            this.Label38.AutoSize = true;
            this.Label38.Location = new System.Drawing.Point(303, 55);
            this.Label38.Name = "Label38";
            this.Label38.Size = new System.Drawing.Size(121, 20);
            this.Label38.TabIndex = 174;
            this.Label38.Text = "Feltöltendő fájl :";
            // 
            // Feltöltendő
            // 
            this.Feltöltendő.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Feltöltendő.Location = new System.Drawing.Point(430, 49);
            this.Feltöltendő.Name = "Feltöltendő";
            this.Feltöltendő.Size = new System.Drawing.Size(619, 26);
            this.Feltöltendő.TabIndex = 7;
            // 
            // Label27
            // 
            this.Label27.AutoSize = true;
            this.Label27.Location = new System.Drawing.Point(3, 88);
            this.Label27.Name = "Label27";
            this.Label27.Size = new System.Drawing.Size(190, 20);
            this.Label27.TabIndex = 171;
            this.Label27.Text = "Feltöltött dokumentumok:";
            // 
            // PDF_törlés
            // 
            this.PDF_törlés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.PDF_törlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PDF_törlés.Location = new System.Drawing.Point(254, 111);
            this.PDF_törlés.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PDF_törlés.Name = "PDF_törlés";
            this.PDF_törlés.Size = new System.Drawing.Size(45, 45);
            this.PDF_törlés.TabIndex = 5;
            this.toolTip1.SetToolTip(this.PDF_törlés, "Törli a kiválasztott fájlt.");
            this.PDF_törlés.UseVisualStyleBackColor = true;
            this.PDF_törlés.Click += new System.EventHandler(this.PDF_törlés_Click);
            // 
            // BtnPDF
            // 
            this.BtnPDF.BackgroundImage = global::Villamos.Properties.Resources.Folder_;
            this.BtnPDF.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnPDF.Location = new System.Drawing.Point(201, 38);
            this.BtnPDF.Name = "BtnPDF";
            this.BtnPDF.Size = new System.Drawing.Size(45, 45);
            this.BtnPDF.TabIndex = 2;
            this.toolTip1.SetToolTip(this.BtnPDF, "Megnyitja a feltölteni kívánt PDF fájlt.");
            this.BtnPDF.UseVisualStyleBackColor = true;
            this.BtnPDF.Click += new System.EventHandler(this.BtnPDF_Click);
            // 
            // Pdf_listbox
            // 
            this.Pdf_listbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Pdf_listbox.FormattingEnabled = true;
            this.Pdf_listbox.ItemHeight = 20;
            this.Pdf_listbox.Location = new System.Drawing.Point(4, 111);
            this.Pdf_listbox.Name = "Pdf_listbox";
            this.Pdf_listbox.Size = new System.Drawing.Size(242, 244);
            this.Pdf_listbox.TabIndex = 4;
            this.Pdf_listbox.SelectedIndexChanged += new System.EventHandler(this.Pdf_listbox_SelectedIndexChanged);
            // 
            // PDF_Azonosító
            // 
            this.PDF_Azonosító.DropDownHeight = 350;
            this.PDF_Azonosító.FormattingEnabled = true;
            this.PDF_Azonosító.IntegralHeight = false;
            this.PDF_Azonosító.Location = new System.Drawing.Point(93, 4);
            this.PDF_Azonosító.Name = "PDF_Azonosító";
            this.PDF_Azonosító.Size = new System.Drawing.Size(204, 28);
            this.PDF_Azonosító.TabIndex = 0;
            this.PDF_Azonosító.SelectedIndexChanged += new System.EventHandler(this.PDF_Azonosító_SelectedIndexChanged);
            // 
            // PDF_Frissít
            // 
            this.PDF_Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.PDF_Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PDF_Frissít.Location = new System.Drawing.Point(7, 40);
            this.PDF_Frissít.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PDF_Frissít.Name = "PDF_Frissít";
            this.PDF_Frissít.Size = new System.Drawing.Size(45, 45);
            this.PDF_Frissít.TabIndex = 1;
            this.toolTip1.SetToolTip(this.PDF_Frissít, "Frissiti a képernyőt");
            this.PDF_Frissít.UseVisualStyleBackColor = true;
            this.PDF_Frissít.Click += new System.EventHandler(this.PDF_Frissít_Click);
            // 
            // PDF_rögzít
            // 
            this.PDF_rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.PDF_rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PDF_rögzít.Location = new System.Drawing.Point(252, 38);
            this.PDF_rögzít.Name = "PDF_rögzít";
            this.PDF_rögzít.Size = new System.Drawing.Size(45, 45);
            this.PDF_rögzít.TabIndex = 3;
            this.toolTip1.SetToolTip(this.PDF_rögzít, "Rögzíti a kiválasztott fájlt.");
            this.PDF_rögzít.UseVisualStyleBackColor = true;
            this.PDF_rögzít.Click += new System.EventHandler(this.PDF_rögzít_Click);
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1189, 5);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 174;
            this.toolTip1.SetToolTip(this.BtnSúgó, "Sugó");
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(346, 5);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(835, 28);
            this.Holtart.TabIndex = 175;
            this.Holtart.Visible = false;
            // 
            // Ablak_Szerszám
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Chocolate;
            this.ClientSize = new System.Drawing.Size(1240, 493);
            this.Controls.Add(this.Lapfülek);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.Panel2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Szerszám";
            this.Text = "Szerszám Nyilvántartás";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_Szerszám_Load);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.Lapfülek.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Alap_tábla)).EndInit();
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Könyv_tábla)).EndInit();
            this.TabPage3.ResumeLayout(false);
            this.TabPage3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Könyvelés_tábla)).EndInit();
            this.TabPage4.ResumeLayout(false);
            this.TabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Lekérd_Tábla)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.TabPage5.ResumeLayout(false);
            this.TabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Napló_Tábla)).EndInit();
            this.TabPage6.ResumeLayout(false);
            this.TabPage6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.TabPage7.ResumeLayout(false);
            this.TabPage7.PerformLayout();
            this.ResumeLayout(false);

        }

        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal Button BtnSúgó;
        internal Panel Panel2;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal TabControl Lapfülek;
        internal TabPage TabPage1;
        internal Zuby.ADGV.AdvancedDataGridView Alap_tábla;
        internal TextBox Alap_Költséghely;
        internal TextBox Alap_Méret;
        internal DateTimePicker Alap_Beszerzési_dátum;
        internal TextBox Alap_Megnevezés;
        internal Label Label34;
        internal CheckBox Alap_Töröltek;
        internal Label Label1;
        internal TextBox Alap_Leltáriszám;
        internal Label Label2;
        internal ComboBox Alap_Azonosító;
        internal Label Label3;
        internal CheckBox Alap_Aktív;
        internal Label Label4;
        internal Button Alap_Új_adat;
        internal Button Alap_Rögzít;
        internal Label Label5;
        internal Button Alap_Frissít;
        internal TabPage TabPage2;
        internal Zuby.ADGV.AdvancedDataGridView Könyv_tábla;
        internal ComboBox Könyv_Felelős2;
        internal ComboBox Könyv_Felelős1;
        internal Label Label9;
        internal Label Label8;
        internal CheckBox Könyv_Töröltek;
        internal TextBox Könyv_megnevezés;
        internal ComboBox Könyv_szám;
        internal CheckBox Könyv_Törlés;
        internal Label Label6;
        internal Label Label7;
        internal Button Könyv_új;
        internal Button Könyv_Rögzít;
        internal Button Frissít;
        internal TabPage TabPage3;
        internal Button Rögzít;
        internal Label Label24;
        internal Label Label22;
        internal Label Label23;
        internal ComboBox SzerszámAzonosító;
        internal TextBox Mennyiség;
        internal Label Label21;
        internal Label Label20;
        internal ComboBox HováNév;
        internal ComboBox Hova;
        internal Label Label18;
        internal ComboBox HonnanNév;
        internal ComboBox Honnan;
        internal Label Label19;
        internal TabPage TabPage4;
        internal CheckedListBox Lekérd_Szerszámkönyvszám;
        internal Zuby.ADGV.AdvancedDataGridView Lekérd_Tábla;
        internal ComboBox Lekérd_Szerszámazonosító;
        internal CheckBox Lekérd_Töröltek;
        internal Label Label17;
        internal Label Label16;
        internal Label Label15;
        internal ComboBox Lekérd_Felelős1;
        internal Button Lekérd_Command1;
        internal Button Lekérd_Excelclick;
        internal Button Lekérd_Nevekkiválasztása;
        internal Button Lekérd_Visszacsuk;
        internal Button Lekérd_Jelöltszersz;
        internal Button Lekérd_Mindtöröl;
        internal Button Lekérd_Összeskijelöl;
        internal Button Lekérd_Lenyit;
        internal Button Lekérd_Anyagkiíró;
        internal TabPage TabPage5;
        internal CheckBox Napló_Fájltöröl;
        internal ComboBox Napló_Hovánév;
        internal ComboBox Napló_Hova;
        internal Label Label14;
        internal ComboBox Napló_Honnannév;
        internal ComboBox Napló_Honnan;
        internal Label Label12;
        internal DateTimePicker Napló_Dátumig;
        internal Label Label11;
        internal Zuby.ADGV.AdvancedDataGridView Napló_Tábla;
        internal DateTimePicker Napló_Dátumtól;
        internal CheckBox Napló_Nyomtat;
        internal Label Label10;
        internal Button Napló_Nyomtatvány;
        internal Button Napló_Excel_gomb;
        internal Button Napló_Listáz;
        internal TextBox Alap_tárolás;
        internal Label Label26;
        internal Button Alap_excel;
        internal Button Könyv_excel;
        internal TabPage TabPage6;
        internal TabPage TabPage7;
        internal PictureBox PictureBox1;
        internal Label Label30;
        internal Label Label32;
        internal TextBox Kép_Feltöltendő;
        internal Button Kép_btn;
        internal Label Label33;
        internal ListBox Kép_listbox;
        internal Button Kép_Listázás;
        internal Button Kép_rögzít;
        internal ComboBox Kép_Azonosító;
        internal Label Label28;
        internal ListBox Szűrés;
        internal Label Label38;
        internal TextBox Feltöltendő;
        internal Button BtnPDF;
        internal Label Label27;
        internal ListBox Pdf_listbox;
        internal Button PDF_Frissít;
        internal Button PDF_rögzít;
        internal ComboBox PDF_Azonosító;
        internal TextBox Kép_megnevezés;
        internal Label Label35;
        internal TextBox PDF_megnevezés;
        internal Label Label29;
        internal Button KépTörlés;
        internal Button PDF_törlés;
        internal ListBox Kép_szűrés;
        internal TextBox Megnevezés;
        internal Button Mentés;
        internal Zuby.ADGV.AdvancedDataGridView Könyvelés_tábla;
        internal GroupBox GroupBox1;
        internal RadioButton Radio_minden;
        internal RadioButton Radio_E;
        internal RadioButton Radio_A;
        internal PdfiumViewer.PdfViewer PDF_néző;
        internal TextBox Alap_Gyáriszám;
        internal Label label31;
        internal TextBox Alap_lekérd_megnevezés;
        internal Label label36;
        internal TextBox Alap_Lekérdezés_Méret;
        internal Label label37;
        internal Label HováMennyiség;
        internal Label HonnanMennyiség;
        internal GroupBox groupBox2;
        internal TextBox Könyvelés_Méret;
        internal Label label39;
        internal TextBox Könyvelés_megnevezés;
        internal RadioButton Radio_könyv_Minden;
        internal Label label40;
        internal RadioButton Radio_könyv_E;
        internal RadioButton Radio_könyv_A;
        internal ToolTip toolTip1;
        internal Button Könyvelés_Szűr;
        internal GroupBox groupBox3;
        internal TextBox Lekérd_Méret;
        internal Label label25;
        internal TextBox Lekérd_Megnevezés;
        internal RadioButton Radio_lek_minden;
        internal Label label41;
        internal RadioButton Radio_lek_E;
        internal RadioButton Radio_lek_A;
        internal CheckBox Lekérd_Töröltek1;
        internal GroupBox groupBox4;
        internal Button Könyvelés_szűrés_ürítés;
        internal Button Nyomtatvány9A;
        internal Button Nyomtatvány9B;
    }
}