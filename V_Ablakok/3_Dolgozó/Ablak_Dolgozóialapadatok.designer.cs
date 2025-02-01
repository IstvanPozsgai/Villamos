using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Zuby.ADGV;

namespace Villamos
{
    
    public partial class Ablak_Dolgozóialapadatok : Form
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Dolgozóialapadatok));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.ChkDolgozónév = new System.Windows.Forms.ComboBox();
            this.Kilépettjel = new System.Windows.Forms.CheckBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Dolgozószám = new System.Windows.Forms.TextBox();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.TabPage10 = new System.Windows.Forms.TabPage();
            this.Panel6 = new System.Windows.Forms.Panel();
            this.Button2 = new System.Windows.Forms.Button();
            this.CheckBox1 = new System.Windows.Forms.CheckBox();
            this.Túlórakiró = new System.Windows.Forms.Label();
            this.TabPage9 = new System.Windows.Forms.TabPage();
            this.Panel5 = new System.Windows.Forms.Panel();
            this.Bérrögzítés = new System.Windows.Forms.Button();
            this.Label34 = new System.Windows.Forms.Label();
            this.Órabér = new System.Windows.Forms.TextBox();
            this.TabPage5 = new System.Windows.Forms.TabPage();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.Label25 = new System.Windows.Forms.Label();
            this.Label26 = new System.Windows.Forms.Label();
            this.Label27 = new System.Windows.Forms.Label();
            this.Label28 = new System.Windows.Forms.Label();
            this.Label29 = new System.Windows.Forms.Label();
            this.Label30 = new System.Windows.Forms.Label();
            this.Label31 = new System.Windows.Forms.Label();
            this.Label32 = new System.Windows.Forms.Label();
            this.Label33 = new System.Windows.Forms.Label();
            this.Leánykori = new System.Windows.Forms.TextBox();
            this.Anyja = new System.Windows.Forms.TextBox();
            this.Születésihely = new System.Windows.Forms.TextBox();
            this.Lakcím = new System.Windows.Forms.TextBox();
            this.Ideiglenescím = new System.Windows.Forms.TextBox();
            this.Telefonszám1 = new System.Windows.Forms.TextBox();
            this.Telefonszám2 = new System.Windows.Forms.TextBox();
            this.Telefonszám3 = new System.Windows.Forms.TextBox();
            this.Születésiidő = new System.Windows.Forms.DateTimePicker();
            this.Személyesmódosítás = new System.Windows.Forms.Button();
            this.TabPage8 = new System.Windows.Forms.TabPage();
            this.PDF_néző = new PdfiumViewer.PdfViewer();
            this.TabPage7 = new System.Windows.Forms.TabPage();
            this.TáblaOktatás = new System.Windows.Forms.DataGridView();
            this.Btnfrissít = new System.Windows.Forms.Button();
            this.Label9 = new System.Windows.Forms.Label();
            this.Cmboktatásrögz = new System.Windows.Forms.ComboBox();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.Munkakörtábla = new System.Windows.Forms.DataGridView();
            this.Panel7 = new System.Windows.Forms.Panel();
            this.Label23 = new System.Windows.Forms.Label();
            this.Munkakör = new System.Windows.Forms.ComboBox();
            this.Feorszám = new System.Windows.Forms.TextBox();
            this.Label24 = new System.Windows.Forms.Label();
            this.Munkakörmódosít = new System.Windows.Forms.Button();
            this.Panel8 = new System.Windows.Forms.Panel();
            this.Label39 = new System.Windows.Forms.Label();
            this.PDFMunkakör = new System.Windows.Forms.ComboBox();
            this.Label41 = new System.Windows.Forms.Label();
            this.TxtPDFfájl = new System.Windows.Forms.TextBox();
            this.Label40 = new System.Windows.Forms.Label();
            this.Munkakör_Megnyit = new System.Windows.Forms.Button();
            this.Munkakör_Töröl = new System.Windows.Forms.Button();
            this.BtnPDFsave = new System.Windows.Forms.Button();
            this.MunkaCsoport = new System.Windows.Forms.ComboBox();
            this.label43 = new System.Windows.Forms.Label();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.Jogvonalmegszerzés = new System.Windows.Forms.DateTimePicker();
            this.Label19 = new System.Windows.Forms.Label();
            this.Vonalszám = new System.Windows.Forms.ComboBox();
            this.Tábla1 = new Zuby.ADGV.AdvancedDataGridView();
            this.Jogterületrögzítés = new System.Windows.Forms.Button();
            this.Jogterülettörlés = new System.Windows.Forms.Button();
            this.Label20 = new System.Windows.Forms.Label();
            this.Label21 = new System.Windows.Forms.Label();
            this.Label22 = new System.Windows.Forms.Label();
            this.Jogvonalérv = new System.Windows.Forms.DateTimePicker();
            this.Vonalmegnevezés = new System.Windows.Forms.ComboBox();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Label3 = new System.Windows.Forms.Label();
            this.Jogosítványszám = new System.Windows.Forms.TextBox();
            this.Jogorvosi = new System.Windows.Forms.DateTimePicker();
            this.Jogosítványidő = new System.Windows.Forms.DateTimePicker();
            this.Jogtanusítvány = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.Jogosítványmódosít = new System.Windows.Forms.Button();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.Jogtípusmegszerzés = new System.Windows.Forms.DateTimePicker();
            this.Label16 = new System.Windows.Forms.Label();
            this.Jogtípusérvényes = new System.Windows.Forms.DateTimePicker();
            this.Label17 = new System.Windows.Forms.Label();
            this.Label18 = new System.Windows.Forms.Label();
            this.Jogtípus = new System.Windows.Forms.ComboBox();
            this.Tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.Típusrögzítés = new System.Windows.Forms.Button();
            this.Típustörlés = new System.Windows.Forms.Button();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.Csopvez = new System.Windows.Forms.CheckBox();
            this.Eltérőmunkarend = new System.Windows.Forms.CheckBox();
            this.Szünidős = new System.Windows.Forms.CheckBox();
            this.Állományonkívül = new System.Windows.Forms.CheckBox();
            this.Nyugdíjas = new System.Windows.Forms.CheckBox();
            this.Részmunkaidős = new System.Windows.Forms.CheckBox();
            this.Vezényelve = new System.Windows.Forms.CheckBox();
            this.Vezényelt = new System.Windows.Forms.CheckBox();
            this.Passzív = new System.Windows.Forms.CheckBox();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.Óra8 = new System.Windows.Forms.RadioButton();
            this.Óra12 = new System.Windows.Forms.RadioButton();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.Alkalmazott = new System.Windows.Forms.RadioButton();
            this.Fizikai = new System.Windows.Forms.RadioButton();
            this.Hovavez = new System.Windows.Forms.Label();
            this.Honnanvez = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Csoport = new System.Windows.Forms.ComboBox();
            this.Váltóscsoport = new System.Windows.Forms.ComboBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.Részmunkaidőperc = new System.Windows.Forms.TextBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Felhasználóinév = new System.Windows.Forms.ComboBox();
            this.Főkönyvititulus = new System.Windows.Forms.TextBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.Belépésiidő = new System.Windows.Forms.DateTimePicker();
            this.Kilépésiidő = new System.Windows.Forms.DateTimePicker();
            this.Button4 = new System.Windows.Forms.Button();
            this.Fülek = new System.Windows.Forms.TabControl();
            this.Panel1.SuspendLayout();
            this.TabPage10.SuspendLayout();
            this.Panel6.SuspendLayout();
            this.TabPage9.SuspendLayout();
            this.Panel5.SuspendLayout();
            this.TabPage5.SuspendLayout();
            this.Panel4.SuspendLayout();
            this.TabPage8.SuspendLayout();
            this.TabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TáblaOktatás)).BeginInit();
            this.TabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Munkakörtábla)).BeginInit();
            this.Panel7.SuspendLayout();
            this.Panel8.SuspendLayout();
            this.TabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla1)).BeginInit();
            this.TabPage2.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.Panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.TabPage1.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.Fülek.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.Cmbtelephely);
            this.Panel1.Controls.Add(this.Label13);
            this.Panel1.Location = new System.Drawing.Point(5, 5);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(335, 33);
            this.Panel1.TabIndex = 54;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(143, 0);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
            this.Cmbtelephely.SelectedIndexChanged += new System.EventHandler(this.Cmbtelephely_SelectedIndexChanged);
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(-4, 8);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(145, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // ChkDolgozónév
            // 
            this.ChkDolgozónév.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ChkDolgozónév.FormattingEnabled = true;
            this.ChkDolgozónév.Location = new System.Drawing.Point(148, 46);
            this.ChkDolgozónév.MaxDropDownItems = 15;
            this.ChkDolgozónév.Name = "ChkDolgozónév";
            this.ChkDolgozónév.Size = new System.Drawing.Size(374, 28);
            this.ChkDolgozónév.TabIndex = 56;
            this.ChkDolgozónév.SelectedIndexChanged += new System.EventHandler(this.ChkDolgozónév_SelectedIndexChanged);
            // 
            // Kilépettjel
            // 
            this.Kilépettjel.AutoSize = true;
            this.Kilépettjel.BackColor = System.Drawing.Color.Khaki;
            this.Kilépettjel.Location = new System.Drawing.Point(548, 50);
            this.Kilépettjel.Name = "Kilépettjel";
            this.Kilépettjel.Size = new System.Drawing.Size(169, 24);
            this.Kilépettjel.TabIndex = 55;
            this.Kilépettjel.Text = "Kilépett dolgozókkal";
            this.Kilépettjel.UseVisualStyleBackColor = false;
            this.Kilépettjel.Click += new System.EventHandler(this.Kilépettjel_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(8, 54);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(110, 20);
            this.Label1.TabIndex = 57;
            this.Label1.Text = "Dolgozó neve:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(8, 88);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(114, 20);
            this.Label2.TabIndex = 58;
            this.Label2.Text = "Dolgozó szám:";
            // 
            // Dolgozószám
            // 
            this.Dolgozószám.Location = new System.Drawing.Point(148, 82);
            this.Dolgozószám.Name = "Dolgozószám";
            this.Dolgozószám.Size = new System.Drawing.Size(170, 26);
            this.Dolgozószám.TabIndex = 59;
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1277, 13);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 60;
            this.ToolTip1.SetToolTip(this.BtnSúgó, "Súgó");
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // TabPage10
            // 
            this.TabPage10.BackColor = System.Drawing.Color.IndianRed;
            this.TabPage10.Controls.Add(this.Panel6);
            this.TabPage10.Location = new System.Drawing.Point(4, 29);
            this.TabPage10.Name = "TabPage10";
            this.TabPage10.Size = new System.Drawing.Size(1318, 392);
            this.TabPage10.TabIndex = 9;
            this.TabPage10.Text = "Túlóra engedély";
            // 
            // Panel6
            // 
            this.Panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel6.BackColor = System.Drawing.Color.MistyRose;
            this.Panel6.Controls.Add(this.Túlórakiró);
            this.Panel6.Controls.Add(this.CheckBox1);
            this.Panel6.Controls.Add(this.Button2);
            this.Panel6.Location = new System.Drawing.Point(10, 10);
            this.Panel6.Name = "Panel6";
            this.Panel6.Size = new System.Drawing.Size(1291, 375);
            this.Panel6.TabIndex = 2;
            // 
            // Button2
            // 
            this.Button2.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button2.Location = new System.Drawing.Point(203, 22);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(45, 45);
            this.Button2.TabIndex = 38;
            this.ToolTip1.SetToolTip(this.Button2, "Rögzíti az adatokat");
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // CheckBox1
            // 
            this.CheckBox1.AutoSize = true;
            this.CheckBox1.Location = new System.Drawing.Point(18, 22);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(140, 24);
            this.CheckBox1.TabIndex = 39;
            this.CheckBox1.Text = "Túlóra engedély";
            this.CheckBox1.UseVisualStyleBackColor = true;
            // 
            // Túlórakiró
            // 
            this.Túlórakiró.AutoSize = true;
            this.Túlórakiró.Location = new System.Drawing.Point(14, 110);
            this.Túlórakiró.Name = "Túlórakiró";
            this.Túlórakiró.Size = new System.Drawing.Size(199, 20);
            this.Túlórakiró.TabIndex = 40;
            this.Túlórakiró.Text = "Tárgy évi túlóra mennyiség:";
            // 
            // TabPage9
            // 
            this.TabPage9.BackColor = System.Drawing.Color.IndianRed;
            this.TabPage9.Controls.Add(this.Panel5);
            this.TabPage9.Location = new System.Drawing.Point(4, 29);
            this.TabPage9.Name = "TabPage9";
            this.TabPage9.Size = new System.Drawing.Size(1318, 392);
            this.TabPage9.TabIndex = 8;
            this.TabPage9.Text = "Bér adatok";
            // 
            // Panel5
            // 
            this.Panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel5.BackColor = System.Drawing.Color.MistyRose;
            this.Panel5.Controls.Add(this.Órabér);
            this.Panel5.Controls.Add(this.Label34);
            this.Panel5.Controls.Add(this.Bérrögzítés);
            this.Panel5.Location = new System.Drawing.Point(10, 10);
            this.Panel5.Name = "Panel5";
            this.Panel5.Size = new System.Drawing.Size(1296, 372);
            this.Panel5.TabIndex = 0;
            // 
            // Bérrögzítés
            // 
            this.Bérrögzítés.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Bérrögzítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Bérrögzítés.Location = new System.Drawing.Point(365, 5);
            this.Bérrögzítés.Name = "Bérrögzítés";
            this.Bérrögzítés.Size = new System.Drawing.Size(45, 45);
            this.Bérrögzítés.TabIndex = 38;
            this.ToolTip1.SetToolTip(this.Bérrögzítés, "Rögzíti az adatokat");
            this.Bérrögzítés.UseVisualStyleBackColor = true;
            this.Bérrögzítés.Click += new System.EventHandler(this.Bérrögzítés_Click);
            // 
            // Label34
            // 
            this.Label34.AutoSize = true;
            this.Label34.Location = new System.Drawing.Point(22, 30);
            this.Label34.Name = "Label34";
            this.Label34.Size = new System.Drawing.Size(38, 20);
            this.Label34.TabIndex = 39;
            this.Label34.Text = "Bér:";
            // 
            // Órabér
            // 
            this.Órabér.Location = new System.Drawing.Point(92, 24);
            this.Órabér.MaxLength = 50;
            this.Órabér.Name = "Órabér";
            this.Órabér.Size = new System.Drawing.Size(152, 26);
            this.Órabér.TabIndex = 40;
            // 
            // TabPage5
            // 
            this.TabPage5.BackColor = System.Drawing.Color.IndianRed;
            this.TabPage5.Controls.Add(this.Panel4);
            this.TabPage5.Location = new System.Drawing.Point(4, 29);
            this.TabPage5.Name = "TabPage5";
            this.TabPage5.Size = new System.Drawing.Size(1318, 392);
            this.TabPage5.TabIndex = 4;
            this.TabPage5.Text = "Személyes adatok";
            // 
            // Panel4
            // 
            this.Panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel4.BackColor = System.Drawing.Color.MistyRose;
            this.Panel4.Controls.Add(this.Személyesmódosítás);
            this.Panel4.Controls.Add(this.Születésiidő);
            this.Panel4.Controls.Add(this.Telefonszám3);
            this.Panel4.Controls.Add(this.Telefonszám2);
            this.Panel4.Controls.Add(this.Telefonszám1);
            this.Panel4.Controls.Add(this.Ideiglenescím);
            this.Panel4.Controls.Add(this.Lakcím);
            this.Panel4.Controls.Add(this.Születésihely);
            this.Panel4.Controls.Add(this.Anyja);
            this.Panel4.Controls.Add(this.Leánykori);
            this.Panel4.Controls.Add(this.Label33);
            this.Panel4.Controls.Add(this.Label32);
            this.Panel4.Controls.Add(this.Label31);
            this.Panel4.Controls.Add(this.Label30);
            this.Panel4.Controls.Add(this.Label29);
            this.Panel4.Controls.Add(this.Label28);
            this.Panel4.Controls.Add(this.Label27);
            this.Panel4.Controls.Add(this.Label26);
            this.Panel4.Controls.Add(this.Label25);
            this.Panel4.Location = new System.Drawing.Point(8, 9);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(1306, 376);
            this.Panel4.TabIndex = 0;
            // 
            // Label25
            // 
            this.Label25.AutoSize = true;
            this.Label25.Location = new System.Drawing.Point(14, 19);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(110, 20);
            this.Label25.TabIndex = 0;
            this.Label25.Text = "Leánykori név:";
            // 
            // Label26
            // 
            this.Label26.AutoSize = true;
            this.Label26.Location = new System.Drawing.Point(14, 65);
            this.Label26.Name = "Label26";
            this.Label26.Size = new System.Drawing.Size(90, 20);
            this.Label26.TabIndex = 1;
            this.Label26.Text = "Anyja neve:";
            // 
            // Label27
            // 
            this.Label27.AutoSize = true;
            this.Label27.Location = new System.Drawing.Point(14, 101);
            this.Label27.Name = "Label27";
            this.Label27.Size = new System.Drawing.Size(103, 20);
            this.Label27.TabIndex = 2;
            this.Label27.Text = "Születési idő:";
            // 
            // Label28
            // 
            this.Label28.AutoSize = true;
            this.Label28.Location = new System.Drawing.Point(14, 137);
            this.Label28.Name = "Label28";
            this.Label28.Size = new System.Drawing.Size(110, 20);
            this.Label28.TabIndex = 3;
            this.Label28.Text = "Születési hely:";
            // 
            // Label29
            // 
            this.Label29.AutoSize = true;
            this.Label29.Location = new System.Drawing.Point(14, 187);
            this.Label29.Name = "Label29";
            this.Label29.Size = new System.Drawing.Size(63, 20);
            this.Label29.TabIndex = 4;
            this.Label29.Text = "Lakcím:";
            // 
            // Label30
            // 
            this.Label30.AutoSize = true;
            this.Label30.Location = new System.Drawing.Point(14, 223);
            this.Label30.Name = "Label30";
            this.Label30.Size = new System.Drawing.Size(136, 20);
            this.Label30.TabIndex = 5;
            this.Label30.Text = "Ideiglenes Lakcím";
            // 
            // Label31
            // 
            this.Label31.AutoSize = true;
            this.Label31.Location = new System.Drawing.Point(14, 270);
            this.Label31.Name = "Label31";
            this.Label31.Size = new System.Drawing.Size(104, 20);
            this.Label31.TabIndex = 6;
            this.Label31.Text = "Telefonszám:";
            // 
            // Label32
            // 
            this.Label32.AutoSize = true;
            this.Label32.Location = new System.Drawing.Point(14, 306);
            this.Label32.Name = "Label32";
            this.Label32.Size = new System.Drawing.Size(141, 20);
            this.Label32.TabIndex = 7;
            this.Label32.Text = "Mobil telefonszám:";
            // 
            // Label33
            // 
            this.Label33.AutoSize = true;
            this.Label33.Location = new System.Drawing.Point(14, 342);
            this.Label33.Name = "Label33";
            this.Label33.Size = new System.Drawing.Size(141, 20);
            this.Label33.TabIndex = 8;
            this.Label33.Text = "Mobil telefonszám:";
            // 
            // Leánykori
            // 
            this.Leánykori.Location = new System.Drawing.Point(173, 16);
            this.Leánykori.MaxLength = 50;
            this.Leánykori.Name = "Leánykori";
            this.Leánykori.Size = new System.Drawing.Size(320, 26);
            this.Leánykori.TabIndex = 9;
            // 
            // Anyja
            // 
            this.Anyja.Location = new System.Drawing.Point(173, 62);
            this.Anyja.MaxLength = 50;
            this.Anyja.Name = "Anyja";
            this.Anyja.Size = new System.Drawing.Size(320, 26);
            this.Anyja.TabIndex = 10;
            // 
            // Születésihely
            // 
            this.Születésihely.Location = new System.Drawing.Point(173, 134);
            this.Születésihely.MaxLength = 20;
            this.Születésihely.Name = "Születésihely";
            this.Születésihely.Size = new System.Drawing.Size(320, 26);
            this.Születésihely.TabIndex = 11;
            // 
            // Lakcím
            // 
            this.Lakcím.Location = new System.Drawing.Point(173, 184);
            this.Lakcím.MaxLength = 50;
            this.Lakcím.Name = "Lakcím";
            this.Lakcím.Size = new System.Drawing.Size(635, 26);
            this.Lakcím.TabIndex = 12;
            // 
            // Ideiglenescím
            // 
            this.Ideiglenescím.Location = new System.Drawing.Point(173, 220);
            this.Ideiglenescím.MaxLength = 50;
            this.Ideiglenescím.Name = "Ideiglenescím";
            this.Ideiglenescím.Size = new System.Drawing.Size(635, 26);
            this.Ideiglenescím.TabIndex = 13;
            // 
            // Telefonszám1
            // 
            this.Telefonszám1.Location = new System.Drawing.Point(173, 267);
            this.Telefonszám1.MaxLength = 13;
            this.Telefonszám1.Name = "Telefonszám1";
            this.Telefonszám1.Size = new System.Drawing.Size(165, 26);
            this.Telefonszám1.TabIndex = 14;
            // 
            // Telefonszám2
            // 
            this.Telefonszám2.Location = new System.Drawing.Point(173, 303);
            this.Telefonszám2.MaxLength = 13;
            this.Telefonszám2.Name = "Telefonszám2";
            this.Telefonszám2.Size = new System.Drawing.Size(165, 26);
            this.Telefonszám2.TabIndex = 15;
            // 
            // Telefonszám3
            // 
            this.Telefonszám3.Location = new System.Drawing.Point(173, 339);
            this.Telefonszám3.MaxLength = 13;
            this.Telefonszám3.Name = "Telefonszám3";
            this.Telefonszám3.Size = new System.Drawing.Size(165, 26);
            this.Telefonszám3.TabIndex = 16;
            // 
            // Születésiidő
            // 
            this.Születésiidő.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Születésiidő.Location = new System.Drawing.Point(173, 96);
            this.Születésiidő.Name = "Születésiidő";
            this.Születésiidő.Size = new System.Drawing.Size(110, 26);
            this.Születésiidő.TabIndex = 28;
            // 
            // Személyesmódosítás
            // 
            this.Személyesmódosítás.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Személyesmódosítás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Személyesmódosítás.Location = new System.Drawing.Point(763, 19);
            this.Személyesmódosítás.Name = "Személyesmódosítás";
            this.Személyesmódosítás.Size = new System.Drawing.Size(45, 45);
            this.Személyesmódosítás.TabIndex = 38;
            this.ToolTip1.SetToolTip(this.Személyesmódosítás, "Rögzíti az adatokat");
            this.Személyesmódosítás.UseVisualStyleBackColor = true;
            this.Személyesmódosítás.Click += new System.EventHandler(this.Személyesmódosítás_Click);
            // 
            // TabPage8
            // 
            this.TabPage8.Controls.Add(this.PDF_néző);
            this.TabPage8.Location = new System.Drawing.Point(4, 29);
            this.TabPage8.Name = "TabPage8";
            this.TabPage8.Size = new System.Drawing.Size(1318, 392);
            this.TabPage8.TabIndex = 7;
            this.TabPage8.Text = "PDF ";
            this.TabPage8.UseVisualStyleBackColor = true;
            // 
            // PDF_néző
            // 
            this.PDF_néző.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PDF_néző.Location = new System.Drawing.Point(0, 0);
            this.PDF_néző.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.PDF_néző.Name = "PDF_néző";
            this.PDF_néző.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.PDF_néző.ShowToolbar = false;
            this.PDF_néző.Size = new System.Drawing.Size(1318, 392);
            this.PDF_néző.TabIndex = 241;
            this.PDF_néző.ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitWidth;
            // 
            // TabPage7
            // 
            this.TabPage7.BackColor = System.Drawing.Color.DarkSalmon;
            this.TabPage7.Controls.Add(this.Cmboktatásrögz);
            this.TabPage7.Controls.Add(this.Label9);
            this.TabPage7.Controls.Add(this.Btnfrissít);
            this.TabPage7.Controls.Add(this.TáblaOktatás);
            this.TabPage7.Location = new System.Drawing.Point(4, 29);
            this.TabPage7.Name = "TabPage7";
            this.TabPage7.Size = new System.Drawing.Size(1318, 392);
            this.TabPage7.TabIndex = 6;
            this.TabPage7.Text = "Oktatások";
            // 
            // TáblaOktatás
            // 
            this.TáblaOktatás.AllowUserToAddRows = false;
            this.TáblaOktatás.AllowUserToDeleteRows = false;
            this.TáblaOktatás.AllowUserToResizeRows = false;
            this.TáblaOktatás.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TáblaOktatás.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.TáblaOktatás.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TáblaOktatás.Location = new System.Drawing.Point(3, 54);
            this.TáblaOktatás.Name = "TáblaOktatás";
            this.TáblaOktatás.RowHeadersWidth = 25;
            this.TáblaOktatás.Size = new System.Drawing.Size(1311, 335);
            this.TáblaOktatás.TabIndex = 63;
            this.TáblaOktatás.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TáblaOktatás_CellClick);
            this.TáblaOktatás.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.TáblaOktatás_CellFormatting);
            this.TáblaOktatás.SelectionChanged += new System.EventHandler(this.TáblaOktatás_SelectionChanged);
            // 
            // Btnfrissít
            // 
            this.Btnfrissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Btnfrissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btnfrissít.Location = new System.Drawing.Point(733, 3);
            this.Btnfrissít.Name = "Btnfrissít";
            this.Btnfrissít.Size = new System.Drawing.Size(45, 45);
            this.Btnfrissít.TabIndex = 69;
            this.Btnfrissít.UseVisualStyleBackColor = true;
            this.Btnfrissít.Click += new System.EventHandler(this.Btnfrissít_Click);
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(8, 25);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(160, 20);
            this.Label9.TabIndex = 86;
            this.Label9.Text = "Oktatás tárgya szűrő:";
            // 
            // Cmboktatásrögz
            // 
            this.Cmboktatásrögz.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmboktatásrögz.FormattingEnabled = true;
            this.Cmboktatásrögz.Location = new System.Drawing.Point(170, 17);
            this.Cmboktatásrögz.Name = "Cmboktatásrögz";
            this.Cmboktatásrögz.Size = new System.Drawing.Size(548, 28);
            this.Cmboktatásrögz.TabIndex = 87;
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.DarkOrange;
            this.TabPage4.Controls.Add(this.Panel8);
            this.TabPage4.Controls.Add(this.Panel7);
            this.TabPage4.Controls.Add(this.Munkakörtábla);
            this.TabPage4.Location = new System.Drawing.Point(4, 29);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Size = new System.Drawing.Size(1318, 392);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Munkaköri adatok";
            // 
            // Munkakörtábla
            // 
            this.Munkakörtábla.AllowUserToAddRows = false;
            this.Munkakörtábla.AllowUserToDeleteRows = false;
            this.Munkakörtábla.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Goldenrod;
            this.Munkakörtábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Munkakörtábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Munkakörtábla.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Goldenrod;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Munkakörtábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Munkakörtábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Munkakörtábla.EnableHeadersVisualStyles = false;
            this.Munkakörtábla.Location = new System.Drawing.Point(3, 213);
            this.Munkakörtábla.Name = "Munkakörtábla";
            this.Munkakörtábla.RowHeadersWidth = 25;
            this.Munkakörtábla.Size = new System.Drawing.Size(1311, 176);
            this.Munkakörtábla.TabIndex = 70;
            this.Munkakörtábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Munkakörtábla_CellClick);
            this.Munkakörtábla.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Munkakörtábla_CellFormatting);
            this.Munkakörtábla.SelectionChanged += new System.EventHandler(this.Munkakörtábla_SelectionChanged);
            // 
            // Panel7
            // 
            this.Panel7.BackColor = System.Drawing.Color.Orange;
            this.Panel7.Controls.Add(this.Munkakörmódosít);
            this.Panel7.Controls.Add(this.Label24);
            this.Panel7.Controls.Add(this.Feorszám);
            this.Panel7.Controls.Add(this.Munkakör);
            this.Panel7.Controls.Add(this.Label23);
            this.Panel7.Location = new System.Drawing.Point(8, 3);
            this.Panel7.Name = "Panel7";
            this.Panel7.Size = new System.Drawing.Size(912, 64);
            this.Panel7.TabIndex = 89;
            // 
            // Label23
            // 
            this.Label23.BackColor = System.Drawing.Color.Khaki;
            this.Label23.Location = new System.Drawing.Point(12, 33);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(83, 20);
            this.Label23.TabIndex = 60;
            this.Label23.Text = "Munkakör:";
            // 
            // Munkakör
            // 
            this.Munkakör.FormattingEnabled = true;
            this.Munkakör.Location = new System.Drawing.Point(105, 25);
            this.Munkakör.Name = "Munkakör";
            this.Munkakör.Size = new System.Drawing.Size(468, 28);
            this.Munkakör.TabIndex = 61;
            this.Munkakör.SelectedIndexChanged += new System.EventHandler(this.Munkakör_SelectedIndexChanged);
            // 
            // Feorszám
            // 
            this.Feorszám.Location = new System.Drawing.Point(681, 27);
            this.Feorszám.Name = "Feorszám";
            this.Feorszám.Size = new System.Drawing.Size(170, 26);
            this.Feorszám.TabIndex = 62;
            // 
            // Label24
            // 
            this.Label24.BackColor = System.Drawing.Color.Khaki;
            this.Label24.Location = new System.Drawing.Point(582, 33);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(93, 20);
            this.Label24.TabIndex = 63;
            this.Label24.Text = "Feorszám:";
            // 
            // Munkakörmódosít
            // 
            this.Munkakörmódosít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Munkakörmódosít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Munkakörmódosít.Location = new System.Drawing.Point(857, 8);
            this.Munkakörmódosít.Name = "Munkakörmódosít";
            this.Munkakörmódosít.Size = new System.Drawing.Size(45, 45);
            this.Munkakörmódosít.TabIndex = 64;
            this.ToolTip1.SetToolTip(this.Munkakörmódosít, "Rögzíti az adatokat");
            this.Munkakörmódosít.UseVisualStyleBackColor = true;
            this.Munkakörmódosít.Click += new System.EventHandler(this.Munkakörmódosít_Click);
            // 
            // Panel8
            // 
            this.Panel8.BackColor = System.Drawing.Color.Orange;
            this.Panel8.Controls.Add(this.label43);
            this.Panel8.Controls.Add(this.MunkaCsoport);
            this.Panel8.Controls.Add(this.BtnPDFsave);
            this.Panel8.Controls.Add(this.Munkakör_Töröl);
            this.Panel8.Controls.Add(this.Munkakör_Megnyit);
            this.Panel8.Controls.Add(this.Label40);
            this.Panel8.Controls.Add(this.TxtPDFfájl);
            this.Panel8.Controls.Add(this.Label41);
            this.Panel8.Controls.Add(this.PDFMunkakör);
            this.Panel8.Controls.Add(this.Label39);
            this.Panel8.Location = new System.Drawing.Point(8, 73);
            this.Panel8.Name = "Panel8";
            this.Panel8.Size = new System.Drawing.Size(912, 134);
            this.Panel8.TabIndex = 90;
            // 
            // Label39
            // 
            this.Label39.BackColor = System.Drawing.Color.Khaki;
            this.Label39.Location = new System.Drawing.Point(8, 8);
            this.Label39.Name = "Label39";
            this.Label39.Size = new System.Drawing.Size(197, 20);
            this.Label39.TabIndex = 71;
            this.Label39.Text = "Dokumentumok feltöltése";
            // 
            // PDFMunkakör
            // 
            this.PDFMunkakör.FormattingEnabled = true;
            this.PDFMunkakör.Location = new System.Drawing.Point(120, 65);
            this.PDFMunkakör.Name = "PDFMunkakör";
            this.PDFMunkakör.Size = new System.Drawing.Size(443, 28);
            this.PDFMunkakör.TabIndex = 74;
            // 
            // Label41
            // 
            this.Label41.AutoSize = true;
            this.Label41.BackColor = System.Drawing.Color.Khaki;
            this.Label41.Location = new System.Drawing.Point(11, 105);
            this.Label41.Name = "Label41";
            this.Label41.Size = new System.Drawing.Size(103, 20);
            this.Label41.TabIndex = 77;
            this.Label41.Text = "PDF fájlneve:";
            // 
            // TxtPDFfájl
            // 
            this.TxtPDFfájl.Enabled = false;
            this.TxtPDFfájl.Location = new System.Drawing.Point(120, 99);
            this.TxtPDFfájl.Name = "TxtPDFfájl";
            this.TxtPDFfájl.Size = new System.Drawing.Size(443, 26);
            this.TxtPDFfájl.TabIndex = 76;
            // 
            // Label40
            // 
            this.Label40.AutoSize = true;
            this.Label40.BackColor = System.Drawing.Color.Khaki;
            this.Label40.Location = new System.Drawing.Point(11, 73);
            this.Label40.Name = "Label40";
            this.Label40.Size = new System.Drawing.Size(81, 20);
            this.Label40.TabIndex = 78;
            this.Label40.Text = "Kategória:";
            // 
            // Munkakör_Megnyit
            // 
            this.Munkakör_Megnyit.BackgroundImage = global::Villamos.Properties.Resources.Folder_;
            this.Munkakör_Megnyit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Munkakör_Megnyit.Location = new System.Drawing.Point(586, 80);
            this.Munkakör_Megnyit.Name = "Munkakör_Megnyit";
            this.Munkakör_Megnyit.Size = new System.Drawing.Size(45, 45);
            this.Munkakör_Megnyit.TabIndex = 75;
            this.ToolTip1.SetToolTip(this.Munkakör_Megnyit, "PDF fájl kiválasztása");
            this.Munkakör_Megnyit.UseVisualStyleBackColor = true;
            this.Munkakör_Megnyit.Click += new System.EventHandler(this.Munkakör_Megnyit_Click);
            // 
            // Munkakör_Töröl
            // 
            this.Munkakör_Töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Munkakör_Töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Munkakör_Töröl.Location = new System.Drawing.Point(857, 59);
            this.Munkakör_Töröl.Name = "Munkakör_Töröl";
            this.Munkakör_Töröl.Size = new System.Drawing.Size(45, 45);
            this.Munkakör_Töröl.TabIndex = 88;
            this.ToolTip1.SetToolTip(this.Munkakör_Töröl, "Kijelölt elemek törlése");
            this.Munkakör_Töröl.UseVisualStyleBackColor = true;
            this.Munkakör_Töröl.Click += new System.EventHandler(this.Munkakör_Töröl_Click);
            // 
            // BtnPDFsave
            // 
            this.BtnPDFsave.BackgroundImage = global::Villamos.Properties.Resources.Designcontest_Ecommerce_Business_Save;
            this.BtnPDFsave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnPDFsave.Location = new System.Drawing.Point(857, 8);
            this.BtnPDFsave.Name = "BtnPDFsave";
            this.BtnPDFsave.Size = new System.Drawing.Size(45, 45);
            this.BtnPDFsave.TabIndex = 82;
            this.ToolTip1.SetToolTip(this.BtnPDFsave, "Menti a PDF adatokat");
            this.BtnPDFsave.UseVisualStyleBackColor = true;
            this.BtnPDFsave.Click += new System.EventHandler(this.BtnPDFsave_Click);
            // 
            // MunkaCsoport
            // 
            this.MunkaCsoport.FormattingEnabled = true;
            this.MunkaCsoport.Location = new System.Drawing.Point(120, 31);
            this.MunkaCsoport.Name = "MunkaCsoport";
            this.MunkaCsoport.Size = new System.Drawing.Size(443, 28);
            this.MunkaCsoport.TabIndex = 89;
            this.MunkaCsoport.SelectedIndexChanged += new System.EventHandler(this.MunkakCsoport_SelectedIndexChanged);
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.BackColor = System.Drawing.Color.Khaki;
            this.label43.Location = new System.Drawing.Point(12, 39);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(69, 20);
            this.label43.TabIndex = 90;
            this.label43.Text = "Csoport:";
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.YellowGreen;
            this.TabPage3.Controls.Add(this.Vonalmegnevezés);
            this.TabPage3.Controls.Add(this.Jogvonalérv);
            this.TabPage3.Controls.Add(this.Label22);
            this.TabPage3.Controls.Add(this.Label21);
            this.TabPage3.Controls.Add(this.Label20);
            this.TabPage3.Controls.Add(this.Jogterülettörlés);
            this.TabPage3.Controls.Add(this.Jogterületrögzítés);
            this.TabPage3.Controls.Add(this.Tábla1);
            this.TabPage3.Controls.Add(this.Vonalszám);
            this.TabPage3.Controls.Add(this.Label19);
            this.TabPage3.Controls.Add(this.Jogvonalmegszerzés);
            this.TabPage3.Location = new System.Drawing.Point(4, 29);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(1318, 392);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Jogosítvány és vonal";
            // 
            // Jogvonalmegszerzés
            // 
            this.Jogvonalmegszerzés.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Jogvonalmegszerzés.Location = new System.Drawing.Point(234, 8);
            this.Jogvonalmegszerzés.Name = "Jogvonalmegszerzés";
            this.Jogvonalmegszerzés.Size = new System.Drawing.Size(110, 26);
            this.Jogvonalmegszerzés.TabIndex = 88;
            // 
            // Label19
            // 
            this.Label19.BackColor = System.Drawing.Color.YellowGreen;
            this.Label19.Location = new System.Drawing.Point(8, 14);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(173, 20);
            this.Label19.TabIndex = 89;
            this.Label19.Text = "Megszerzés dátuma:";
            // 
            // Vonalszám
            // 
            this.Vonalszám.FormattingEnabled = true;
            this.Vonalszám.Location = new System.Drawing.Point(234, 39);
            this.Vonalszám.Name = "Vonalszám";
            this.Vonalszám.Size = new System.Drawing.Size(236, 28);
            this.Vonalszám.TabIndex = 90;
            this.Vonalszám.SelectionChangeCommitted += new System.EventHandler(this.Vonalszám_SelectionChangeCommitted);
            // 
            // Tábla1
            // 
            this.Tábla1.AllowUserToAddRows = false;
            this.Tábla1.AllowUserToDeleteRows = false;
            this.Tábla1.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.OliveDrab;
            this.Tábla1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.Tábla1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.OliveDrab;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.Tábla1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla1.EnableHeadersVisualStyles = false;
            this.Tábla1.FilterAndSortEnabled = true;
            this.Tábla1.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla1.Location = new System.Drawing.Point(3, 139);
            this.Tábla1.MaxFilterButtonImageHeight = 23;
            this.Tábla1.Name = "Tábla1";
            this.Tábla1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tábla1.RowHeadersVisible = false;
            this.Tábla1.RowHeadersWidth = 25;
            this.Tábla1.Size = new System.Drawing.Size(1312, 250);
            this.Tábla1.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla1.TabIndex = 91;
            this.Tábla1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla1_CellClick);
            // 
            // Jogterületrögzítés
            // 
            this.Jogterületrögzítés.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Jogterületrögzítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Jogterületrögzítés.Location = new System.Drawing.Point(1150, 12);
            this.Jogterületrögzítés.Name = "Jogterületrögzítés";
            this.Jogterületrögzítés.Size = new System.Drawing.Size(45, 45);
            this.Jogterületrögzítés.TabIndex = 92;
            this.ToolTip1.SetToolTip(this.Jogterületrögzítés, "Rögzíti az adatokat");
            this.Jogterületrögzítés.UseVisualStyleBackColor = true;
            this.Jogterületrögzítés.Click += new System.EventHandler(this.Jogterületrögzítés_Click);
            // 
            // Jogterülettörlés
            // 
            this.Jogterülettörlés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Jogterülettörlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Jogterülettörlés.Location = new System.Drawing.Point(1150, 88);
            this.Jogterülettörlés.Name = "Jogterülettörlés";
            this.Jogterülettörlés.Size = new System.Drawing.Size(45, 45);
            this.Jogterülettörlés.TabIndex = 93;
            this.ToolTip1.SetToolTip(this.Jogterülettörlés, "Kijelölt elemek törlése");
            this.Jogterülettörlés.UseVisualStyleBackColor = true;
            this.Jogterülettörlés.Click += new System.EventHandler(this.Jogterülettörlés_Click);
            // 
            // Label20
            // 
            this.Label20.BackColor = System.Drawing.Color.YellowGreen;
            this.Label20.Location = new System.Drawing.Point(8, 112);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(220, 20);
            this.Label20.TabIndex = 94;
            this.Label20.Text = "Érvényes:";
            // 
            // Label21
            // 
            this.Label21.BackColor = System.Drawing.Color.YellowGreen;
            this.Label21.Location = new System.Drawing.Point(8, 81);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(220, 20);
            this.Label21.TabIndex = 95;
            this.Label21.Text = "Terület megnevezés:";
            // 
            // Label22
            // 
            this.Label22.BackColor = System.Drawing.Color.YellowGreen;
            this.Label22.Location = new System.Drawing.Point(8, 47);
            this.Label22.Name = "Label22";
            this.Label22.Size = new System.Drawing.Size(220, 20);
            this.Label22.TabIndex = 96;
            this.Label22.Text = "Terület szám:";
            // 
            // Jogvonalérv
            // 
            this.Jogvonalérv.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Jogvonalérv.Location = new System.Drawing.Point(234, 107);
            this.Jogvonalérv.Name = "Jogvonalérv";
            this.Jogvonalérv.Size = new System.Drawing.Size(110, 26);
            this.Jogvonalérv.TabIndex = 98;
            // 
            // Vonalmegnevezés
            // 
            this.Vonalmegnevezés.FormattingEnabled = true;
            this.Vonalmegnevezés.Location = new System.Drawing.Point(234, 73);
            this.Vonalmegnevezés.Name = "Vonalmegnevezés";
            this.Vonalmegnevezés.Size = new System.Drawing.Size(894, 28);
            this.Vonalmegnevezés.TabIndex = 99;
            this.Vonalmegnevezés.SelectionChangeCommitted += new System.EventHandler(this.Vonalmegnevezés_SelectionChangeCommitted);
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.YellowGreen;
            this.TabPage2.Controls.Add(this.Panel3);
            this.TabPage2.Controls.Add(this.Panel2);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(1318, 392);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Jogosítvány és típus";
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.Panel2.Controls.Add(this.Jogosítványmódosít);
            this.Panel2.Controls.Add(this.Label15);
            this.Panel2.Controls.Add(this.Label14);
            this.Panel2.Controls.Add(this.Label4);
            this.Panel2.Controls.Add(this.Jogtanusítvány);
            this.Panel2.Controls.Add(this.Jogosítványidő);
            this.Panel2.Controls.Add(this.Jogorvosi);
            this.Panel2.Controls.Add(this.Jogosítványszám);
            this.Panel2.Controls.Add(this.Label3);
            this.Panel2.Location = new System.Drawing.Point(8, 10);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(509, 230);
            this.Panel2.TabIndex = 0;
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.Color.YellowGreen;
            this.Label3.Location = new System.Drawing.Point(13, 9);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(220, 20);
            this.Label3.TabIndex = 14;
            this.Label3.Text = "Jogosítvány száma:";
            // 
            // Jogosítványszám
            // 
            this.Jogosítványszám.Location = new System.Drawing.Point(256, 6);
            this.Jogosítványszám.MaxLength = 20;
            this.Jogosítványszám.Name = "Jogosítványszám";
            this.Jogosítványszám.Size = new System.Drawing.Size(236, 26);
            this.Jogosítványszám.TabIndex = 19;
            // 
            // Jogorvosi
            // 
            this.Jogorvosi.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Jogorvosi.Location = new System.Drawing.Point(256, 93);
            this.Jogorvosi.Name = "Jogorvosi";
            this.Jogorvosi.Size = new System.Drawing.Size(110, 26);
            this.Jogorvosi.TabIndex = 26;
            // 
            // Jogosítványidő
            // 
            this.Jogosítványidő.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Jogosítványidő.Location = new System.Drawing.Point(256, 50);
            this.Jogosítványidő.Name = "Jogosítványidő";
            this.Jogosítványidő.Size = new System.Drawing.Size(110, 26);
            this.Jogosítványidő.TabIndex = 27;
            // 
            // Jogtanusítvány
            // 
            this.Jogtanusítvány.Location = new System.Drawing.Point(256, 138);
            this.Jogtanusítvány.MaxLength = 20;
            this.Jogtanusítvány.Name = "Jogtanusítvány";
            this.Jogtanusítvány.Size = new System.Drawing.Size(236, 26);
            this.Jogtanusítvány.TabIndex = 28;
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.YellowGreen;
            this.Label4.Location = new System.Drawing.Point(13, 144);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(220, 20);
            this.Label4.TabIndex = 29;
            this.Label4.Text = "Tanusítvány száma:";
            // 
            // Label14
            // 
            this.Label14.BackColor = System.Drawing.Color.YellowGreen;
            this.Label14.Location = new System.Drawing.Point(13, 99);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(220, 20);
            this.Label14.TabIndex = 30;
            this.Label14.Text = "Jogosítvány orvosi idő:";
            // 
            // Label15
            // 
            this.Label15.BackColor = System.Drawing.Color.YellowGreen;
            this.Label15.Location = new System.Drawing.Point(13, 56);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(220, 20);
            this.Label15.TabIndex = 31;
            this.Label15.Text = "Jogosítvány érvényességi idő:";
            // 
            // Jogosítványmódosít
            // 
            this.Jogosítványmódosít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Jogosítványmódosít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Jogosítványmódosít.Location = new System.Drawing.Point(256, 177);
            this.Jogosítványmódosít.Name = "Jogosítványmódosít";
            this.Jogosítványmódosít.Size = new System.Drawing.Size(45, 45);
            this.Jogosítványmódosít.TabIndex = 38;
            this.ToolTip1.SetToolTip(this.Jogosítványmódosít, "Rögzíti az adatokat");
            this.Jogosítványmódosít.UseVisualStyleBackColor = true;
            this.Jogosítványmódosít.Click += new System.EventHandler(this.Jogosítványmódosít_Click);
            // 
            // Panel3
            // 
            this.Panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel3.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.Panel3.Controls.Add(this.Típustörlés);
            this.Panel3.Controls.Add(this.Típusrögzítés);
            this.Panel3.Controls.Add(this.Tábla);
            this.Panel3.Controls.Add(this.Jogtípus);
            this.Panel3.Controls.Add(this.Label18);
            this.Panel3.Controls.Add(this.Label17);
            this.Panel3.Controls.Add(this.Jogtípusérvényes);
            this.Panel3.Controls.Add(this.Label16);
            this.Panel3.Controls.Add(this.Jogtípusmegszerzés);
            this.Panel3.Location = new System.Drawing.Point(523, 10);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(789, 382);
            this.Panel3.TabIndex = 1;
            // 
            // Jogtípusmegszerzés
            // 
            this.Jogtípusmegszerzés.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Jogtípusmegszerzés.Location = new System.Drawing.Point(246, 3);
            this.Jogtípusmegszerzés.Name = "Jogtípusmegszerzés";
            this.Jogtípusmegszerzés.Size = new System.Drawing.Size(110, 26);
            this.Jogtípusmegszerzés.TabIndex = 32;
            // 
            // Label16
            // 
            this.Label16.BackColor = System.Drawing.Color.YellowGreen;
            this.Label16.Location = new System.Drawing.Point(3, 9);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(220, 20);
            this.Label16.TabIndex = 33;
            this.Label16.Text = "Megszerzés dátuma:";
            // 
            // Jogtípusérvényes
            // 
            this.Jogtípusérvényes.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Jogtípusérvényes.Location = new System.Drawing.Point(246, 81);
            this.Jogtípusérvényes.Name = "Jogtípusérvényes";
            this.Jogtípusérvényes.Size = new System.Drawing.Size(110, 26);
            this.Jogtípusérvényes.TabIndex = 34;
            // 
            // Label17
            // 
            this.Label17.BackColor = System.Drawing.Color.YellowGreen;
            this.Label17.Location = new System.Drawing.Point(3, 85);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(220, 20);
            this.Label17.TabIndex = 35;
            this.Label17.Text = "Érvényes:";
            // 
            // Label18
            // 
            this.Label18.BackColor = System.Drawing.Color.YellowGreen;
            this.Label18.Location = new System.Drawing.Point(3, 49);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(220, 20);
            this.Label18.TabIndex = 36;
            this.Label18.Text = "Típus:";
            // 
            // Jogtípus
            // 
            this.Jogtípus.FormattingEnabled = true;
            this.Jogtípus.Location = new System.Drawing.Point(246, 41);
            this.Jogtípus.Name = "Jogtípus";
            this.Jogtípus.Size = new System.Drawing.Size(453, 28);
            this.Jogtípus.TabIndex = 37;
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            this.Tábla.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.OliveDrab;
            this.Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.OliveDrab;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.EnableHeadersVisualStyles = false;
            this.Tábla.FilterAndSortEnabled = true;
            this.Tábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.Location = new System.Drawing.Point(3, 120);
            this.Tábla.MaxFilterButtonImageHeight = 23;
            this.Tábla.Name = "Tábla";
            this.Tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tábla.RowHeadersVisible = false;
            this.Tábla.RowHeadersWidth = 25;
            this.Tábla.Size = new System.Drawing.Size(783, 256);
            this.Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.TabIndex = 64;
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            // 
            // Típusrögzítés
            // 
            this.Típusrögzítés.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Típusrögzítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Típusrögzítés.Location = new System.Drawing.Point(730, 9);
            this.Típusrögzítés.Name = "Típusrögzítés";
            this.Típusrögzítés.Size = new System.Drawing.Size(45, 45);
            this.Típusrögzítés.TabIndex = 65;
            this.ToolTip1.SetToolTip(this.Típusrögzítés, "Rögzíti az adatokat");
            this.Típusrögzítés.UseVisualStyleBackColor = true;
            this.Típusrögzítés.Click += new System.EventHandler(this.Típusrögzítés_Click);
            // 
            // Típustörlés
            // 
            this.Típustörlés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Típustörlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Típustörlés.Location = new System.Drawing.Point(730, 60);
            this.Típustörlés.Name = "Típustörlés";
            this.Típustörlés.Size = new System.Drawing.Size(45, 45);
            this.Típustörlés.TabIndex = 87;
            this.ToolTip1.SetToolTip(this.Típustörlés, "Kijelölt elemek törlése");
            this.Típustörlés.UseVisualStyleBackColor = true;
            this.Típustörlés.Click += new System.EventHandler(this.Típustörlés_Click);
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.Orange;
            this.TabPage1.Controls.Add(this.Button4);
            this.TabPage1.Controls.Add(this.Kilépésiidő);
            this.TabPage1.Controls.Add(this.Belépésiidő);
            this.TabPage1.Controls.Add(this.Label12);
            this.TabPage1.Controls.Add(this.Label11);
            this.TabPage1.Controls.Add(this.Főkönyvititulus);
            this.TabPage1.Controls.Add(this.Részmunkaidőperc);
            this.TabPage1.Controls.Add(this.Felhasználóinév);
            this.TabPage1.Controls.Add(this.Label10);
            this.TabPage1.Controls.Add(this.Label8);
            this.TabPage1.Controls.Add(this.Label7);
            this.TabPage1.Controls.Add(this.Váltóscsoport);
            this.TabPage1.Controls.Add(this.Csoport);
            this.TabPage1.Controls.Add(this.Label6);
            this.TabPage1.Controls.Add(this.Label5);
            this.TabPage1.Controls.Add(this.Honnanvez);
            this.TabPage1.Controls.Add(this.Hovavez);
            this.TabPage1.Controls.Add(this.GroupBox2);
            this.TabPage1.Controls.Add(this.GroupBox1);
            this.TabPage1.Controls.Add(this.Passzív);
            this.TabPage1.Controls.Add(this.Vezényelt);
            this.TabPage1.Controls.Add(this.Vezényelve);
            this.TabPage1.Controls.Add(this.Részmunkaidős);
            this.TabPage1.Controls.Add(this.Nyugdíjas);
            this.TabPage1.Controls.Add(this.Állományonkívül);
            this.TabPage1.Controls.Add(this.Szünidős);
            this.TabPage1.Controls.Add(this.Eltérőmunkarend);
            this.TabPage1.Controls.Add(this.Csopvez);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1318, 392);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Csoport adatok";
            // 
            // Csopvez
            // 
            this.Csopvez.BackColor = System.Drawing.Color.Khaki;
            this.Csopvez.Location = new System.Drawing.Point(8, 6);
            this.Csopvez.Name = "Csopvez";
            this.Csopvez.Size = new System.Drawing.Size(147, 24);
            this.Csopvez.TabIndex = 0;
            this.Csopvez.Text = "Csoportvezető";
            this.Csopvez.UseVisualStyleBackColor = false;
            // 
            // Eltérőmunkarend
            // 
            this.Eltérőmunkarend.BackColor = System.Drawing.Color.Khaki;
            this.Eltérőmunkarend.Location = new System.Drawing.Point(180, 282);
            this.Eltérőmunkarend.Name = "Eltérőmunkarend";
            this.Eltérőmunkarend.Size = new System.Drawing.Size(236, 24);
            this.Eltérőmunkarend.TabIndex = 1;
            this.Eltérőmunkarend.Text = "Eltérő munkarend";
            this.Eltérőmunkarend.UseVisualStyleBackColor = false;
            // 
            // Szünidős
            // 
            this.Szünidős.BackColor = System.Drawing.Color.Khaki;
            this.Szünidős.Location = new System.Drawing.Point(8, 282);
            this.Szünidős.Name = "Szünidős";
            this.Szünidős.Size = new System.Drawing.Size(147, 24);
            this.Szünidős.TabIndex = 2;
            this.Szünidős.Text = "Nyári szünidős";
            this.Szünidős.UseVisualStyleBackColor = false;
            // 
            // Állományonkívül
            // 
            this.Állományonkívül.BackColor = System.Drawing.Color.Khaki;
            this.Állományonkívül.Location = new System.Drawing.Point(8, 242);
            this.Állományonkívül.Name = "Állományonkívül";
            this.Állományonkívül.Size = new System.Drawing.Size(147, 24);
            this.Állományonkívül.TabIndex = 3;
            this.Állományonkívül.Text = "Állományon kívüli";
            this.Állományonkívül.UseVisualStyleBackColor = false;
            // 
            // Nyugdíjas
            // 
            this.Nyugdíjas.BackColor = System.Drawing.Color.Khaki;
            this.Nyugdíjas.Location = new System.Drawing.Point(8, 202);
            this.Nyugdíjas.Name = "Nyugdíjas";
            this.Nyugdíjas.Size = new System.Drawing.Size(147, 24);
            this.Nyugdíjas.TabIndex = 4;
            this.Nyugdíjas.Text = "Nyugdíjas";
            this.Nyugdíjas.UseVisualStyleBackColor = false;
            // 
            // Részmunkaidős
            // 
            this.Részmunkaidős.BackColor = System.Drawing.Color.Khaki;
            this.Részmunkaidős.Location = new System.Drawing.Point(8, 162);
            this.Részmunkaidős.Name = "Részmunkaidős";
            this.Részmunkaidős.Size = new System.Drawing.Size(147, 24);
            this.Részmunkaidős.TabIndex = 5;
            this.Részmunkaidős.Text = "Rész munkaidős";
            this.Részmunkaidős.UseVisualStyleBackColor = false;
            // 
            // Vezényelve
            // 
            this.Vezényelve.BackColor = System.Drawing.Color.Khaki;
            this.Vezényelve.Enabled = false;
            this.Vezényelve.Location = new System.Drawing.Point(544, 122);
            this.Vezényelve.Name = "Vezényelve";
            this.Vezényelve.Size = new System.Drawing.Size(181, 24);
            this.Vezényelve.TabIndex = 6;
            this.Vezényelve.Text = "Vezényelve";
            this.Vezényelve.UseVisualStyleBackColor = false;
            // 
            // Vezényelt
            // 
            this.Vezényelt.BackColor = System.Drawing.Color.Khaki;
            this.Vezényelt.Enabled = false;
            this.Vezényelt.Location = new System.Drawing.Point(8, 122);
            this.Vezényelt.Name = "Vezényelt";
            this.Vezényelt.Size = new System.Drawing.Size(147, 24);
            this.Vezényelt.TabIndex = 7;
            this.Vezényelt.Text = "Vezényelt";
            this.Vezényelt.UseVisualStyleBackColor = false;
            // 
            // Passzív
            // 
            this.Passzív.BackColor = System.Drawing.Color.Khaki;
            this.Passzív.Location = new System.Drawing.Point(8, 82);
            this.Passzív.Name = "Passzív";
            this.Passzív.Size = new System.Drawing.Size(147, 24);
            this.Passzív.TabIndex = 8;
            this.Passzív.Text = "Passzív";
            this.Passzív.UseVisualStyleBackColor = false;
            // 
            // GroupBox1
            // 
            this.GroupBox1.BackColor = System.Drawing.Color.Khaki;
            this.GroupBox1.Controls.Add(this.Óra12);
            this.GroupBox1.Controls.Add(this.Óra8);
            this.GroupBox1.Location = new System.Drawing.Point(181, 182);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(235, 84);
            this.GroupBox1.TabIndex = 9;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Napi munkaidő";
            // 
            // Óra8
            // 
            this.Óra8.AutoSize = true;
            this.Óra8.Location = new System.Drawing.Point(6, 25);
            this.Óra8.Name = "Óra8";
            this.Óra8.Size = new System.Drawing.Size(71, 24);
            this.Óra8.TabIndex = 0;
            this.Óra8.TabStop = true;
            this.Óra8.Text = "8 órás";
            this.Óra8.UseVisualStyleBackColor = true;
            // 
            // Óra12
            // 
            this.Óra12.AutoSize = true;
            this.Óra12.Location = new System.Drawing.Point(6, 55);
            this.Óra12.Name = "Óra12";
            this.Óra12.Size = new System.Drawing.Size(80, 24);
            this.Óra12.TabIndex = 1;
            this.Óra12.TabStop = true;
            this.Óra12.Text = "12 órás";
            this.Óra12.UseVisualStyleBackColor = true;
            // 
            // GroupBox2
            // 
            this.GroupBox2.BackColor = System.Drawing.Color.Khaki;
            this.GroupBox2.Controls.Add(this.Fizikai);
            this.GroupBox2.Controls.Add(this.Alkalmazott);
            this.GroupBox2.Location = new System.Drawing.Point(544, 182);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(181, 84);
            this.GroupBox2.TabIndex = 10;
            this.GroupBox2.TabStop = false;
            // 
            // Alkalmazott
            // 
            this.Alkalmazott.AutoSize = true;
            this.Alkalmazott.Location = new System.Drawing.Point(6, 20);
            this.Alkalmazott.Name = "Alkalmazott";
            this.Alkalmazott.Size = new System.Drawing.Size(110, 24);
            this.Alkalmazott.TabIndex = 0;
            this.Alkalmazott.TabStop = true;
            this.Alkalmazott.Text = "Alkalmazott";
            this.Alkalmazott.UseVisualStyleBackColor = true;
            // 
            // Fizikai
            // 
            this.Fizikai.AutoSize = true;
            this.Fizikai.Location = new System.Drawing.Point(6, 50);
            this.Fizikai.Name = "Fizikai";
            this.Fizikai.Size = new System.Drawing.Size(71, 24);
            this.Fizikai.TabIndex = 1;
            this.Fizikai.TabStop = true;
            this.Fizikai.Text = "Fizikai";
            this.Fizikai.UseVisualStyleBackColor = true;
            // 
            // Hovavez
            // 
            this.Hovavez.BackColor = System.Drawing.Color.Khaki;
            this.Hovavez.Location = new System.Drawing.Point(177, 126);
            this.Hovavez.Name = "Hovavez";
            this.Hovavez.Size = new System.Drawing.Size(240, 20);
            this.Hovavez.TabIndex = 11;
            // 
            // Honnanvez
            // 
            this.Honnanvez.BackColor = System.Drawing.Color.Khaki;
            this.Honnanvez.Location = new System.Drawing.Point(740, 122);
            this.Honnanvez.Name = "Honnanvez";
            this.Honnanvez.Size = new System.Drawing.Size(239, 20);
            this.Honnanvez.TabIndex = 12;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.Khaki;
            this.Label5.Location = new System.Drawing.Point(8, 46);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(147, 20);
            this.Label5.TabIndex = 13;
            this.Label5.Text = "Csoport beosztás:";
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.Khaki;
            this.Label6.Location = new System.Drawing.Point(540, 46);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(185, 20);
            this.Label6.TabIndex = 14;
            this.Label6.Text = "Váltós csoportkód:";
            // 
            // Csoport
            // 
            this.Csoport.FormattingEnabled = true;
            this.Csoport.Location = new System.Drawing.Point(180, 38);
            this.Csoport.Name = "Csoport";
            this.Csoport.Size = new System.Drawing.Size(236, 28);
            this.Csoport.TabIndex = 15;
            // 
            // Váltóscsoport
            // 
            this.Váltóscsoport.FormattingEnabled = true;
            this.Váltóscsoport.Location = new System.Drawing.Point(743, 43);
            this.Váltóscsoport.Name = "Váltóscsoport";
            this.Váltóscsoport.Size = new System.Drawing.Size(121, 28);
            this.Váltóscsoport.TabIndex = 16;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.BackColor = System.Drawing.Color.Khaki;
            this.Label7.Location = new System.Drawing.Point(540, 286);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(185, 20);
            this.Label7.TabIndex = 17;
            this.Label7.Text = "Rész munkaidő percben:";
            // 
            // Részmunkaidőperc
            // 
            this.Részmunkaidőperc.Location = new System.Drawing.Point(743, 280);
            this.Részmunkaidőperc.Name = "Részmunkaidőperc";
            this.Részmunkaidőperc.Size = new System.Drawing.Size(236, 26);
            this.Részmunkaidőperc.TabIndex = 18;
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.Khaki;
            this.Label8.Location = new System.Drawing.Point(8, 322);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(147, 20);
            this.Label8.TabIndex = 19;
            this.Label8.Text = "Felhaszálónév:";
            // 
            // Label10
            // 
            this.Label10.BackColor = System.Drawing.Color.Khaki;
            this.Label10.Location = new System.Drawing.Point(8, 358);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(147, 20);
            this.Label10.TabIndex = 20;
            this.Label10.Text = "Főkönyvi titulus:";
            // 
            // Felhasználóinév
            // 
            this.Felhasználóinév.FormattingEnabled = true;
            this.Felhasználóinév.Location = new System.Drawing.Point(180, 314);
            this.Felhasználóinév.Name = "Felhasználóinév";
            this.Felhasználóinév.Size = new System.Drawing.Size(236, 28);
            this.Felhasználóinév.TabIndex = 21;
            // 
            // Főkönyvititulus
            // 
            this.Főkönyvititulus.Location = new System.Drawing.Point(180, 352);
            this.Főkönyvititulus.Name = "Főkönyvititulus";
            this.Főkönyvititulus.Size = new System.Drawing.Size(354, 26);
            this.Főkönyvititulus.TabIndex = 22;
            // 
            // Label11
            // 
            this.Label11.BackColor = System.Drawing.Color.Khaki;
            this.Label11.Location = new System.Drawing.Point(540, 322);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(185, 20);
            this.Label11.TabIndex = 23;
            this.Label11.Text = "Belépési idő:";
            // 
            // Label12
            // 
            this.Label12.BackColor = System.Drawing.Color.Khaki;
            this.Label12.Location = new System.Drawing.Point(540, 358);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(185, 20);
            this.Label12.TabIndex = 24;
            this.Label12.Text = "Kilépési idő:";
            // 
            // Belépésiidő
            // 
            this.Belépésiidő.Enabled = false;
            this.Belépésiidő.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Belépésiidő.Location = new System.Drawing.Point(743, 322);
            this.Belépésiidő.Name = "Belépésiidő";
            this.Belépésiidő.Size = new System.Drawing.Size(110, 26);
            this.Belépésiidő.TabIndex = 25;
            // 
            // Kilépésiidő
            // 
            this.Kilépésiidő.Enabled = false;
            this.Kilépésiidő.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Kilépésiidő.Location = new System.Drawing.Point(743, 352);
            this.Kilépésiidő.Name = "Kilépésiidő";
            this.Kilépésiidő.Size = new System.Drawing.Size(110, 26);
            this.Kilépésiidő.TabIndex = 26;
            // 
            // Button4
            // 
            this.Button4.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button4.Location = new System.Drawing.Point(1101, 21);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(45, 45);
            this.Button4.TabIndex = 37;
            this.ToolTip1.SetToolTip(this.Button4, "Rögzíti az adatokat");
            this.Button4.UseVisualStyleBackColor = true;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
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
            this.Fülek.Controls.Add(this.TabPage7);
            this.Fülek.Controls.Add(this.TabPage8);
            this.Fülek.Controls.Add(this.TabPage5);
            this.Fülek.Controls.Add(this.TabPage9);
            this.Fülek.Controls.Add(this.TabPage10);
            this.Fülek.Location = new System.Drawing.Point(0, 125);
            this.Fülek.Name = "Fülek";
            this.Fülek.Padding = new System.Drawing.Point(16, 3);
            this.Fülek.SelectedIndex = 0;
            this.Fülek.Size = new System.Drawing.Size(1326, 425);
            this.Fülek.TabIndex = 61;
            this.Fülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Fülek_DrawItem);
            this.Fülek.SelectedIndexChanged += new System.EventHandler(this.Fülek_SelectedIndexChanged);
            // 
            // Ablak_Dolgozóialapadatok
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkKhaki;
            this.ClientSize = new System.Drawing.Size(1330, 551);
            this.Controls.Add(this.Fülek);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.Dolgozószám);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.ChkDolgozónév);
            this.Controls.Add(this.Kilépettjel);
            this.Controls.Add(this.Panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Dolgozóialapadatok";
            this.Text = "Dolgozói alapadatok";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AblakDolgozóialapadatok_Load);
            this.Shown += new System.EventHandler(this.Ablak_Dolgozóialapadatok_Shown);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.TabPage10.ResumeLayout(false);
            this.Panel6.ResumeLayout(false);
            this.Panel6.PerformLayout();
            this.TabPage9.ResumeLayout(false);
            this.Panel5.ResumeLayout(false);
            this.Panel5.PerformLayout();
            this.TabPage5.ResumeLayout(false);
            this.Panel4.ResumeLayout(false);
            this.Panel4.PerformLayout();
            this.TabPage8.ResumeLayout(false);
            this.TabPage7.ResumeLayout(false);
            this.TabPage7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TáblaOktatás)).EndInit();
            this.TabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Munkakörtábla)).EndInit();
            this.Panel7.ResumeLayout(false);
            this.Panel7.PerformLayout();
            this.Panel8.ResumeLayout(false);
            this.Panel8.PerformLayout();
            this.TabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla1)).EndInit();
            this.TabPage2.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.Panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.Fülek.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal Panel Panel1;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal ComboBox ChkDolgozónév;
        internal CheckBox Kilépettjel;
        internal Label Label1;
        internal Label Label2;
        internal TextBox Dolgozószám;
        internal Button BtnSúgó;
        internal ToolTip ToolTip1;
        internal TabPage TabPage10;
        internal Panel Panel6;
        internal Label Túlórakiró;
        internal CheckBox CheckBox1;
        internal Button Button2;
        internal TabPage TabPage9;
        internal Panel Panel5;
        internal TextBox Órabér;
        internal Label Label34;
        internal Button Bérrögzítés;
        internal TabPage TabPage5;
        internal Panel Panel4;
        internal Button Személyesmódosítás;
        internal DateTimePicker Születésiidő;
        internal TextBox Telefonszám3;
        internal TextBox Telefonszám2;
        internal TextBox Telefonszám1;
        internal TextBox Ideiglenescím;
        internal TextBox Lakcím;
        internal TextBox Születésihely;
        internal TextBox Anyja;
        internal TextBox Leánykori;
        internal Label Label33;
        internal Label Label32;
        internal Label Label31;
        internal Label Label30;
        internal Label Label29;
        internal Label Label28;
        internal Label Label27;
        internal Label Label26;
        internal Label Label25;
        internal TabPage TabPage8;
        private PdfiumViewer.PdfViewer PDF_néző;
        internal TabPage TabPage7;
        internal ComboBox Cmboktatásrögz;
        internal Label Label9;
        internal Button Btnfrissít;
        internal DataGridView TáblaOktatás;
        internal TabPage TabPage4;
        internal Panel Panel8;
        internal Label label43;
        internal ComboBox MunkaCsoport;
        internal Button BtnPDFsave;
        internal Button Munkakör_Töröl;
        internal Button Munkakör_Megnyit;
        internal Label Label40;
        internal TextBox TxtPDFfájl;
        internal Label Label41;
        internal ComboBox PDFMunkakör;
        internal Label Label39;
        internal Panel Panel7;
        internal Button Munkakörmódosít;
        internal Label Label24;
        internal TextBox Feorszám;
        internal ComboBox Munkakör;
        internal Label Label23;
        internal DataGridView Munkakörtábla;
        internal TabPage TabPage3;
        internal ComboBox Vonalmegnevezés;
        internal DateTimePicker Jogvonalérv;
        internal Label Label22;
        internal Label Label21;
        internal Label Label20;
        internal Button Jogterülettörlés;
        internal Button Jogterületrögzítés;
        internal AdvancedDataGridView Tábla1;
        internal ComboBox Vonalszám;
        internal Label Label19;
        internal DateTimePicker Jogvonalmegszerzés;
        internal TabPage TabPage2;
        internal Panel Panel3;
        internal Button Típustörlés;
        internal Button Típusrögzítés;
        internal AdvancedDataGridView Tábla;
        internal ComboBox Jogtípus;
        internal Label Label18;
        internal Label Label17;
        internal DateTimePicker Jogtípusérvényes;
        internal Label Label16;
        internal DateTimePicker Jogtípusmegszerzés;
        internal Panel Panel2;
        internal Button Jogosítványmódosít;
        internal Label Label15;
        internal Label Label14;
        internal Label Label4;
        internal TextBox Jogtanusítvány;
        internal DateTimePicker Jogosítványidő;
        internal DateTimePicker Jogorvosi;
        internal TextBox Jogosítványszám;
        internal Label Label3;
        internal TabPage TabPage1;
        internal Button Button4;
        internal DateTimePicker Kilépésiidő;
        internal DateTimePicker Belépésiidő;
        internal Label Label12;
        internal Label Label11;
        internal TextBox Főkönyvititulus;
        internal TextBox Részmunkaidőperc;
        internal ComboBox Felhasználóinév;
        internal Label Label10;
        internal Label Label8;
        internal Label Label7;
        internal ComboBox Váltóscsoport;
        internal ComboBox Csoport;
        internal Label Label6;
        internal Label Label5;
        internal Label Honnanvez;
        internal Label Hovavez;
        internal GroupBox GroupBox2;
        internal RadioButton Fizikai;
        internal RadioButton Alkalmazott;
        internal GroupBox GroupBox1;
        internal RadioButton Óra12;
        internal RadioButton Óra8;
        internal CheckBox Passzív;
        internal CheckBox Vezényelt;
        internal CheckBox Vezényelve;
        internal CheckBox Részmunkaidős;
        internal CheckBox Nyugdíjas;
        internal CheckBox Állományonkívül;
        internal CheckBox Szünidős;
        internal CheckBox Eltérőmunkarend;
        internal CheckBox Csopvez;
        internal TabControl Fülek;
    }
}