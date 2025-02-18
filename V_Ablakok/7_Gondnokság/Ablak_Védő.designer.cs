using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
    public partial class Ablak_védő : Form
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_védő));
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.Lapfülek = new System.Windows.Forms.TabControl();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.Más_dátum = new System.Windows.Forms.CheckBox();
            this.Könyvelési_dátum = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.Könyv_SzűrőTXT = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Könyvelés_Szűrés = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Label20 = new System.Windows.Forms.Label();
            this.Label21 = new System.Windows.Forms.Label();
            this.HonnanMennyiség = new System.Windows.Forms.Label();
            this.HováMennyiség = new System.Windows.Forms.Label();
            this.SzerszámAzonosító = new System.Windows.Forms.ComboBox();
            this.Label23 = new System.Windows.Forms.Label();
            this.Megnevezés = new System.Windows.Forms.TextBox();
            this.Label24 = new System.Windows.Forms.Label();
            this.Mennyiség = new System.Windows.Forms.TextBox();
            this.Label25 = new System.Windows.Forms.Label();
            this.Gyáriszám = new System.Windows.Forms.TextBox();
            this.Rögzít = new System.Windows.Forms.Button();
            this.HováNév = new System.Windows.Forms.ComboBox();
            this.Hova = new System.Windows.Forms.ComboBox();
            this.Label18 = new System.Windows.Forms.Label();
            this.HonnanNév = new System.Windows.Forms.ComboBox();
            this.Label19 = new System.Windows.Forms.Label();
            this.Tábla_Könyv = new Zuby.ADGV.AdvancedDataGridView();
            this.Honnan = new System.Windows.Forms.ComboBox();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.Alap_Szint = new System.Windows.Forms.TextBox();
            this.Label30 = new System.Windows.Forms.Label();
            this.Alap_védelem = new System.Windows.Forms.ComboBox();
            this.Alap_szabvány = new System.Windows.Forms.TextBox();
            this.Label29 = new System.Windows.Forms.Label();
            this.Alap_kockázat = new System.Windows.Forms.TextBox();
            this.Label28 = new System.Windows.Forms.Label();
            this.Label27 = new System.Windows.Forms.Label();
            this.Alap_Munk_Megnevezés = new System.Windows.Forms.TextBox();
            this.Label26 = new System.Windows.Forms.Label();
            this.Alap_tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.Alap_Költséghely = new System.Windows.Forms.TextBox();
            this.Alap_Méret = new System.Windows.Forms.TextBox();
            this.Alap_Megnevezés = new System.Windows.Forms.TextBox();
            this.Label34 = new System.Windows.Forms.Label();
            this.Alap_Töröltek = new System.Windows.Forms.CheckBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Alap_Azonosító = new System.Windows.Forms.ComboBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Alap_Aktív = new System.Windows.Forms.CheckBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Alap_excel = new System.Windows.Forms.Button();
            this.Alap_Új_adat = new System.Windows.Forms.Button();
            this.Alap_Rögzít = new System.Windows.Forms.Button();
            this.Alap_Frissít = new System.Windows.Forms.Button();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.IDM_dolgozó = new System.Windows.Forms.Button();
            this.Könyv_excel = new System.Windows.Forms.Button();
            this.Könyv_tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.Könyv_Felelős1 = new System.Windows.Forms.ComboBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.Könyv_Töröltek = new System.Windows.Forms.CheckBox();
            this.Könyv_megnevezés = new System.Windows.Forms.TextBox();
            this.Könyv_szám = new System.Windows.Forms.ComboBox();
            this.Könyv_Törlés = new System.Windows.Forms.CheckBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Könyv_új = new System.Windows.Forms.Button();
            this.Könyv_Rögzít = new System.Windows.Forms.Button();
            this.Frissít = new System.Windows.Forms.Button();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.Lekérd_Munkáltatói = new System.Windows.Forms.Button();
            this.Lekérd_Szerszámkönyvszám = new System.Windows.Forms.CheckedListBox();
            this.Lekérd_Tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.Lekérd_Szerszámazonosító = new System.Windows.Forms.ComboBox();
            this.Lekérd_Megnevezés = new System.Windows.Forms.ComboBox();
            this.Lekérd_Töröltek = new System.Windows.Forms.CheckBox();
            this.Label17 = new System.Windows.Forms.Label();
            this.Label16 = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.Lekérd_Felelős1 = new System.Windows.Forms.ComboBox();
            this.Lekérd_Command1 = new System.Windows.Forms.Button();
            this.Lekérd_Excelclick = new System.Windows.Forms.Button();
            this.Lekérd_Nevekkiválasztása = new System.Windows.Forms.Button();
            this.Lekérd_Visszacsuk = new System.Windows.Forms.Button();
            this.Lekérd_Jelöltszersz = new System.Windows.Forms.Button();
            this.Lekérd_Mindtöröl = new System.Windows.Forms.Button();
            this.Lekérd_Összeskijelöl = new System.Windows.Forms.Button();
            this.Lekérd_Lenyit = new System.Windows.Forms.Button();
            this.Lekérd_Anyagkiíró = new System.Windows.Forms.Button();
            this.TabPage5 = new System.Windows.Forms.TabPage();
            this.Napló_hely = new System.Windows.Forms.TextBox();
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
            this.PDF_néző = new PdfiumViewer.PdfViewer();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.Panel2.SuspendLayout();
            this.Lapfülek.SuspendLayout();
            this.TabPage3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_Könyv)).BeginInit();
            this.TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Alap_tábla)).BeginInit();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Könyv_tábla)).BeginInit();
            this.TabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Lekérd_Tábla)).BeginInit();
            this.TabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Napló_Tábla)).BeginInit();
            this.TabPage6.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.Cmbtelephely);
            this.Panel2.Controls.Add(this.Label13);
            this.Panel2.Location = new System.Drawing.Point(5, 5);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(335, 33);
            this.Panel2.TabIndex = 169;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.DropDownHeight = 300;
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.IntegralHeight = false;
            this.Cmbtelephely.Location = new System.Drawing.Point(143, 0);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
            this.Cmbtelephely.SelectedIndexChanged += new System.EventHandler(this.Cmbtelephely_SelectedIndexChanged);
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
            this.Holtart.Location = new System.Drawing.Point(346, 7);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(703, 28);
            this.Holtart.TabIndex = 172;
            this.Holtart.Visible = false;
            // 
            // Lapfülek
            // 
            this.Lapfülek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Lapfülek.Controls.Add(this.TabPage3);
            this.Lapfülek.Controls.Add(this.TabPage1);
            this.Lapfülek.Controls.Add(this.TabPage2);
            this.Lapfülek.Controls.Add(this.TabPage4);
            this.Lapfülek.Controls.Add(this.TabPage5);
            this.Lapfülek.Controls.Add(this.TabPage6);
            this.Lapfülek.Location = new System.Drawing.Point(5, 56);
            this.Lapfülek.Name = "Lapfülek";
            this.Lapfülek.Padding = new System.Drawing.Point(16, 3);
            this.Lapfülek.SelectedIndex = 0;
            this.Lapfülek.Size = new System.Drawing.Size(1215, 400);
            this.Lapfülek.TabIndex = 173;
            this.Lapfülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LapFülek_DrawItem);
            this.Lapfülek.SelectedIndexChanged += new System.EventHandler(this.LapFülek_SelectedIndexChanged);
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.SteelBlue;
            this.TabPage3.Controls.Add(this.Más_dátum);
            this.TabPage3.Controls.Add(this.Könyvelési_dátum);
            this.TabPage3.Controls.Add(this.panel1);
            this.TabPage3.Controls.Add(this.label1);
            this.TabPage3.Controls.Add(this.Label20);
            this.TabPage3.Controls.Add(this.Label21);
            this.TabPage3.Controls.Add(this.HonnanMennyiség);
            this.TabPage3.Controls.Add(this.HováMennyiség);
            this.TabPage3.Controls.Add(this.SzerszámAzonosító);
            this.TabPage3.Controls.Add(this.Label23);
            this.TabPage3.Controls.Add(this.Megnevezés);
            this.TabPage3.Controls.Add(this.Label24);
            this.TabPage3.Controls.Add(this.Mennyiség);
            this.TabPage3.Controls.Add(this.Label25);
            this.TabPage3.Controls.Add(this.Gyáriszám);
            this.TabPage3.Controls.Add(this.Rögzít);
            this.TabPage3.Controls.Add(this.HováNév);
            this.TabPage3.Controls.Add(this.Hova);
            this.TabPage3.Controls.Add(this.Label18);
            this.TabPage3.Controls.Add(this.HonnanNév);
            this.TabPage3.Controls.Add(this.Label19);
            this.TabPage3.Controls.Add(this.Tábla_Könyv);
            this.TabPage3.Controls.Add(this.Honnan);
            this.TabPage3.Location = new System.Drawing.Point(4, 29);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage3.Size = new System.Drawing.Size(1207, 367);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Könyvelés";
            // 
            // Más_dátum
            // 
            this.Más_dátum.AutoSize = true;
            this.Más_dátum.BackColor = System.Drawing.Color.Silver;
            this.Más_dátum.Location = new System.Drawing.Point(699, 146);
            this.Más_dátum.Name = "Más_dátum";
            this.Más_dátum.Size = new System.Drawing.Size(165, 24);
            this.Más_dátum.TabIndex = 217;
            this.Más_dátum.Text = "Utólagos könyvelés";
            this.Más_dátum.UseVisualStyleBackColor = false;
            this.Más_dátum.CheckedChanged += new System.EventHandler(this.Más_dátum_CheckedChanged);
            // 
            // Könyvelési_dátum
            // 
            this.Könyvelési_dátum.Enabled = false;
            this.Könyvelési_dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Könyvelési_dátum.Location = new System.Drawing.Point(874, 144);
            this.Könyvelési_dátum.Name = "Könyvelési_dátum";
            this.Könyvelési_dátum.Size = new System.Drawing.Size(118, 26);
            this.Könyvelési_dátum.TabIndex = 216;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.Könyv_SzűrőTXT);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.Könyvelés_Szűrés);
            this.panel1.Location = new System.Drawing.Point(6, 183);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(580, 60);
            this.panel1.TabIndex = 215;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Silver;
            this.label8.Location = new System.Drawing.Point(14, 31);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 20);
            this.label8.TabIndex = 217;
            this.label8.Text = "Megnevezés:";
            // 
            // Könyv_SzűrőTXT
            // 
            this.Könyv_SzűrőTXT.Location = new System.Drawing.Point(127, 25);
            this.Könyv_SzűrőTXT.Name = "Könyv_SzűrőTXT";
            this.Könyv_SzűrőTXT.Size = new System.Drawing.Size(361, 26);
            this.Könyv_SzűrőTXT.TabIndex = 216;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Silver;
            this.label5.Location = new System.Drawing.Point(2, 2);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 20);
            this.label5.TabIndex = 214;
            this.label5.Text = "Szűrő:";
            // 
            // Könyvelés_Szűrés
            // 
            this.Könyvelés_Szűrés.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Könyvelés_Szűrés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Könyvelés_Szűrés.Location = new System.Drawing.Point(524, 6);
            this.Könyvelés_Szűrés.Name = "Könyvelés_Szűrés";
            this.Könyvelés_Szűrés.Size = new System.Drawing.Size(45, 45);
            this.Könyvelés_Szűrés.TabIndex = 213;
            this.ToolTip1.SetToolTip(this.Könyvelés_Szűrés, "Frissíti a listát");
            this.Könyvelés_Szűrés.UseVisualStyleBackColor = true;
            this.Könyvelés_Szűrés.Click += new System.EventHandler(this.Könyvelés_Szűrés_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(9, 114);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 20);
            this.label1.TabIndex = 214;
            this.label1.Text = "Azonosító:";
            // 
            // Label20
            // 
            this.Label20.AutoSize = true;
            this.Label20.BackColor = System.Drawing.Color.Silver;
            this.Label20.Location = new System.Drawing.Point(493, 77);
            this.Label20.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(65, 20);
            this.Label20.TabIndex = 205;
            this.Label20.Text = "Készlet:";
            // 
            // Label21
            // 
            this.Label21.AutoSize = true;
            this.Label21.BackColor = System.Drawing.Color.Silver;
            this.Label21.Location = new System.Drawing.Point(9, 77);
            this.Label21.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(65, 20);
            this.Label21.TabIndex = 206;
            this.Label21.Text = "Készlet:";
            // 
            // HonnanMennyiség
            // 
            this.HonnanMennyiség.AutoSize = true;
            this.HonnanMennyiség.BackColor = System.Drawing.Color.Silver;
            this.HonnanMennyiség.Location = new System.Drawing.Point(133, 77);
            this.HonnanMennyiség.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.HonnanMennyiség.Name = "HonnanMennyiség";
            this.HonnanMennyiség.Size = new System.Drawing.Size(37, 20);
            this.HonnanMennyiség.TabIndex = 211;
            this.HonnanMennyiség.Text = "<-->";
            // 
            // HováMennyiség
            // 
            this.HováMennyiség.AutoSize = true;
            this.HováMennyiség.BackColor = System.Drawing.Color.Silver;
            this.HováMennyiség.Location = new System.Drawing.Point(603, 77);
            this.HováMennyiség.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.HováMennyiség.Name = "HováMennyiség";
            this.HováMennyiség.Size = new System.Drawing.Size(37, 20);
            this.HováMennyiség.TabIndex = 212;
            this.HováMennyiség.Text = "<-->";
            // 
            // SzerszámAzonosító
            // 
            this.SzerszámAzonosító.DropDownHeight = 300;
            this.SzerszámAzonosító.FormattingEnabled = true;
            this.SzerszámAzonosító.IntegralHeight = false;
            this.SzerszámAzonosító.Location = new System.Drawing.Point(133, 106);
            this.SzerszámAzonosító.MaxLength = 20;
            this.SzerszámAzonosító.Name = "SzerszámAzonosító";
            this.SzerszámAzonosító.Size = new System.Drawing.Size(220, 28);
            this.SzerszámAzonosító.TabIndex = 200;
            this.SzerszámAzonosító.SelectedIndexChanged += new System.EventHandler(this.SzerszámAzonosító_SelectedIndexChanged_1);
            this.SzerszámAzonosító.DropDownClosed += new System.EventHandler(this.SzerszámAzonosító_DropDownClosed);
            // 
            // Label23
            // 
            this.Label23.AutoSize = true;
            this.Label23.BackColor = System.Drawing.Color.Silver;
            this.Label23.Location = new System.Drawing.Point(9, 150);
            this.Label23.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(103, 20);
            this.Label23.TabIndex = 208;
            this.Label23.Text = "Megnevezés:";
            // 
            // Megnevezés
            // 
            this.Megnevezés.Location = new System.Drawing.Point(133, 144);
            this.Megnevezés.Name = "Megnevezés";
            this.Megnevezés.Size = new System.Drawing.Size(431, 26);
            this.Megnevezés.TabIndex = 201;
            // 
            // Label24
            // 
            this.Label24.AutoSize = true;
            this.Label24.BackColor = System.Drawing.Color.Silver;
            this.Label24.Location = new System.Drawing.Point(493, 114);
            this.Label24.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(85, 20);
            this.Label24.TabIndex = 209;
            this.Label24.Text = "Mennyiség";
            // 
            // Mennyiség
            // 
            this.Mennyiség.Location = new System.Drawing.Point(603, 108);
            this.Mennyiség.Name = "Mennyiség";
            this.Mennyiség.Size = new System.Drawing.Size(128, 26);
            this.Mennyiség.TabIndex = 202;
            // 
            // Label25
            // 
            this.Label25.AutoSize = true;
            this.Label25.BackColor = System.Drawing.Color.Silver;
            this.Label25.Location = new System.Drawing.Point(749, 114);
            this.Label25.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(115, 20);
            this.Label25.TabIndex = 210;
            this.Label25.Text = "Bizonylatszám:";
            // 
            // Gyáriszám
            // 
            this.Gyáriszám.Location = new System.Drawing.Point(874, 108);
            this.Gyáriszám.Name = "Gyáriszám";
            this.Gyáriszám.Size = new System.Drawing.Size(301, 26);
            this.Gyáriszám.TabIndex = 203;
            // 
            // Rögzít
            // 
            this.Rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Rögzít.Location = new System.Drawing.Point(1130, 52);
            this.Rögzít.Name = "Rögzít";
            this.Rögzít.Size = new System.Drawing.Size(45, 45);
            this.Rögzít.TabIndex = 204;
            this.ToolTip1.SetToolTip(this.Rögzít, "Rögzíti az adatokat");
            this.Rögzít.UseVisualStyleBackColor = true;
            this.Rögzít.Click += new System.EventHandler(this.Rögzít_Click_2);
            // 
            // HováNév
            // 
            this.HováNév.DropDownHeight = 300;
            this.HováNév.FormattingEnabled = true;
            this.HováNév.IntegralHeight = false;
            this.HováNév.Location = new System.Drawing.Point(603, 40);
            this.HováNév.MaxLength = 20;
            this.HováNév.Name = "HováNév";
            this.HováNév.Size = new System.Drawing.Size(350, 28);
            this.HováNév.Sorted = true;
            this.HováNév.TabIndex = 184;
            this.HováNév.SelectedIndexChanged += new System.EventHandler(this.HováNév_SelectedIndexChanged_1);
            // 
            // Hova
            // 
            this.Hova.DropDownHeight = 300;
            this.Hova.FormattingEnabled = true;
            this.Hova.IntegralHeight = false;
            this.Hova.Location = new System.Drawing.Point(603, 6);
            this.Hova.MaxLength = 20;
            this.Hova.Name = "Hova";
            this.Hova.Size = new System.Drawing.Size(220, 28);
            this.Hova.Sorted = true;
            this.Hova.TabIndex = 1;
            this.Hova.SelectedIndexChanged += new System.EventHandler(this.Hova_SelectedIndexChanged_1);
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.BackColor = System.Drawing.Color.Silver;
            this.Label18.Location = new System.Drawing.Point(493, 14);
            this.Label18.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(50, 20);
            this.Label18.TabIndex = 182;
            this.Label18.Text = "Hova:";
            // 
            // HonnanNév
            // 
            this.HonnanNév.DropDownHeight = 300;
            this.HonnanNév.FormattingEnabled = true;
            this.HonnanNév.IntegralHeight = false;
            this.HonnanNév.Location = new System.Drawing.Point(133, 40);
            this.HonnanNév.MaxLength = 20;
            this.HonnanNév.Name = "HonnanNév";
            this.HonnanNév.Size = new System.Drawing.Size(350, 28);
            this.HonnanNév.Sorted = true;
            this.HonnanNév.TabIndex = 181;
            this.HonnanNév.SelectedIndexChanged += new System.EventHandler(this.HonnanNév_SelectedIndexChanged_1);
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.BackColor = System.Drawing.Color.Silver;
            this.Label19.Location = new System.Drawing.Point(9, 14);
            this.Label19.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(70, 20);
            this.Label19.TabIndex = 179;
            this.Label19.Text = "Honnan:";
            // 
            // Tábla_Könyv
            // 
            this.Tábla_Könyv.AllowUserToAddRows = false;
            this.Tábla_Könyv.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Tábla_Könyv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Tábla_Könyv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla_Könyv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Tábla_Könyv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla_Könyv.EnableHeadersVisualStyles = false;
            this.Tábla_Könyv.FilterAndSortEnabled = true;
            this.Tábla_Könyv.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla_Könyv.Location = new System.Drawing.Point(6, 248);
            this.Tábla_Könyv.MaxFilterButtonImageHeight = 23;
            this.Tábla_Könyv.Name = "Tábla_Könyv";
            this.Tábla_Könyv.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tábla_Könyv.RowHeadersVisible = false;
            this.Tábla_Könyv.RowHeadersWidth = 62;
            this.Tábla_Könyv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Tábla_Könyv.Size = new System.Drawing.Size(1196, 113);
            this.Tábla_Könyv.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla_Könyv.TabIndex = 198;
            this.Tábla_Könyv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_Könyv_CellClick);
            // 
            // Honnan
            // 
            this.Honnan.DropDownHeight = 300;
            this.Honnan.FormattingEnabled = true;
            this.Honnan.IntegralHeight = false;
            this.Honnan.Location = new System.Drawing.Point(133, 6);
            this.Honnan.MaxLength = 20;
            this.Honnan.Name = "Honnan";
            this.Honnan.Size = new System.Drawing.Size(220, 28);
            this.Honnan.Sorted = true;
            this.Honnan.TabIndex = 0;
            this.Honnan.SelectedIndexChanged += new System.EventHandler(this.Honnan_SelectedIndexChanged_1);
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.SeaGreen;
            this.TabPage1.Controls.Add(this.Alap_Szint);
            this.TabPage1.Controls.Add(this.Label30);
            this.TabPage1.Controls.Add(this.Alap_védelem);
            this.TabPage1.Controls.Add(this.Alap_szabvány);
            this.TabPage1.Controls.Add(this.Label29);
            this.TabPage1.Controls.Add(this.Alap_kockázat);
            this.TabPage1.Controls.Add(this.Label28);
            this.TabPage1.Controls.Add(this.Label27);
            this.TabPage1.Controls.Add(this.Alap_Munk_Megnevezés);
            this.TabPage1.Controls.Add(this.Label26);
            this.TabPage1.Controls.Add(this.Alap_tábla);
            this.TabPage1.Controls.Add(this.Alap_Költséghely);
            this.TabPage1.Controls.Add(this.Alap_Méret);
            this.TabPage1.Controls.Add(this.Alap_Megnevezés);
            this.TabPage1.Controls.Add(this.Label34);
            this.TabPage1.Controls.Add(this.Alap_Töröltek);
            this.TabPage1.Controls.Add(this.Label2);
            this.TabPage1.Controls.Add(this.Alap_Azonosító);
            this.TabPage1.Controls.Add(this.Label3);
            this.TabPage1.Controls.Add(this.Alap_Aktív);
            this.TabPage1.Controls.Add(this.Label4);
            this.TabPage1.Controls.Add(this.Alap_excel);
            this.TabPage1.Controls.Add(this.Alap_Új_adat);
            this.TabPage1.Controls.Add(this.Alap_Rögzít);
            this.TabPage1.Controls.Add(this.Alap_Frissít);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1207, 367);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Védőeszköz Törzs";
            // 
            // Alap_Szint
            // 
            this.Alap_Szint.Location = new System.Drawing.Point(592, 175);
            this.Alap_Szint.MaxLength = 50;
            this.Alap_Szint.Name = "Alap_Szint";
            this.Alap_Szint.Size = new System.Drawing.Size(348, 26);
            this.Alap_Szint.TabIndex = 9;
            // 
            // Label30
            // 
            this.Label30.AutoSize = true;
            this.Label30.BackColor = System.Drawing.Color.Silver;
            this.Label30.Location = new System.Drawing.Point(473, 178);
            this.Label30.Name = "Label30";
            this.Label30.Size = new System.Drawing.Size(103, 20);
            this.Label30.TabIndex = 195;
            this.Label30.Text = "Védelmi szint";
            // 
            // Alap_védelem
            // 
            this.Alap_védelem.DropDownHeight = 300;
            this.Alap_védelem.FormattingEnabled = true;
            this.Alap_védelem.IntegralHeight = false;
            this.Alap_védelem.Location = new System.Drawing.Point(114, 175);
            this.Alap_védelem.MaxLength = 20;
            this.Alap_védelem.Name = "Alap_védelem";
            this.Alap_védelem.Size = new System.Drawing.Size(180, 28);
            this.Alap_védelem.TabIndex = 8;
            // 
            // Alap_szabvány
            // 
            this.Alap_szabvány.Location = new System.Drawing.Point(592, 143);
            this.Alap_szabvány.MaxLength = 100;
            this.Alap_szabvány.Name = "Alap_szabvány";
            this.Alap_szabvány.Size = new System.Drawing.Size(348, 26);
            this.Alap_szabvány.TabIndex = 7;
            // 
            // Label29
            // 
            this.Label29.AutoSize = true;
            this.Label29.BackColor = System.Drawing.Color.Silver;
            this.Label29.Location = new System.Drawing.Point(484, 149);
            this.Label29.Name = "Label29";
            this.Label29.Size = new System.Drawing.Size(82, 20);
            this.Label29.TabIndex = 192;
            this.Label29.Text = "Szabvány:";
            // 
            // Alap_kockázat
            // 
            this.Alap_kockázat.Location = new System.Drawing.Point(114, 143);
            this.Alap_kockázat.MaxLength = 100;
            this.Alap_kockázat.Name = "Alap_kockázat";
            this.Alap_kockázat.Size = new System.Drawing.Size(348, 26);
            this.Alap_kockázat.TabIndex = 6;
            // 
            // Label28
            // 
            this.Label28.AutoSize = true;
            this.Label28.BackColor = System.Drawing.Color.Silver;
            this.Label28.Location = new System.Drawing.Point(6, 183);
            this.Label28.Name = "Label28";
            this.Label28.Size = new System.Drawing.Size(67, 20);
            this.Label28.TabIndex = 190;
            this.Label28.Text = "Mit Véd:";
            // 
            // Label27
            // 
            this.Label27.AutoSize = true;
            this.Label27.BackColor = System.Drawing.Color.Silver;
            this.Label27.Location = new System.Drawing.Point(6, 149);
            this.Label27.Name = "Label27";
            this.Label27.Size = new System.Drawing.Size(96, 20);
            this.Label27.TabIndex = 189;
            this.Label27.Text = "Kockázatok:";
            // 
            // Alap_Munk_Megnevezés
            // 
            this.Alap_Munk_Megnevezés.Location = new System.Drawing.Point(220, 111);
            this.Alap_Munk_Megnevezés.MaxLength = 150;
            this.Alap_Munk_Megnevezés.Name = "Alap_Munk_Megnevezés";
            this.Alap_Munk_Megnevezés.Size = new System.Drawing.Size(550, 26);
            this.Alap_Munk_Megnevezés.TabIndex = 5;
            // 
            // Label26
            // 
            this.Label26.AutoSize = true;
            this.Label26.BackColor = System.Drawing.Color.Silver;
            this.Label26.Location = new System.Drawing.Point(6, 117);
            this.Label26.Name = "Label26";
            this.Label26.Size = new System.Drawing.Size(208, 20);
            this.Label26.TabIndex = 187;
            this.Label26.Text = "Munkavédelmi megnevezés:";
            // 
            // Alap_tábla
            // 
            this.Alap_tábla.AllowUserToAddRows = false;
            this.Alap_tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.Alap_tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.Alap_tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.MediumSeaGreen;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Alap_tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.Alap_tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Alap_tábla.EnableHeadersVisualStyles = false;
            this.Alap_tábla.FilterAndSortEnabled = true;
            this.Alap_tábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Alap_tábla.Location = new System.Drawing.Point(5, 209);
            this.Alap_tábla.MaxFilterButtonImageHeight = 23;
            this.Alap_tábla.Name = "Alap_tábla";
            this.Alap_tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Alap_tábla.RowHeadersVisible = false;
            this.Alap_tábla.RowHeadersWidth = 62;
            this.Alap_tábla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Alap_tábla.Size = new System.Drawing.Size(1196, 156);
            this.Alap_tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Alap_tábla.TabIndex = 168;
            this.Alap_tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Alap_tábla_CellClick);
            // 
            // Alap_Költséghely
            // 
            this.Alap_Költséghely.Location = new System.Drawing.Point(114, 79);
            this.Alap_Költséghely.MaxLength = 6;
            this.Alap_Költséghely.Name = "Alap_Költséghely";
            this.Alap_Költséghely.Size = new System.Drawing.Size(115, 26);
            this.Alap_Költséghely.TabIndex = 2;
            // 
            // Alap_Méret
            // 
            this.Alap_Méret.Location = new System.Drawing.Point(300, 78);
            this.Alap_Méret.MaxLength = 15;
            this.Alap_Méret.Name = "Alap_Méret";
            this.Alap_Méret.Size = new System.Drawing.Size(180, 26);
            this.Alap_Méret.TabIndex = 3;
            // 
            // Alap_Megnevezés
            // 
            this.Alap_Megnevezés.Location = new System.Drawing.Point(114, 47);
            this.Alap_Megnevezés.MaxLength = 50;
            this.Alap_Megnevezés.Name = "Alap_Megnevezés";
            this.Alap_Megnevezés.Size = new System.Drawing.Size(550, 26);
            this.Alap_Megnevezés.TabIndex = 1;
            // 
            // Label34
            // 
            this.Label34.AutoSize = true;
            this.Label34.BackColor = System.Drawing.Color.Silver;
            this.Label34.Location = new System.Drawing.Point(5, 15);
            this.Label34.Name = "Label34";
            this.Label34.Size = new System.Drawing.Size(84, 20);
            this.Label34.TabIndex = 95;
            this.Label34.Text = "Azonosító:";
            // 
            // Alap_Töröltek
            // 
            this.Alap_Töröltek.AutoSize = true;
            this.Alap_Töröltek.BackColor = System.Drawing.Color.Gold;
            this.Alap_Töröltek.Location = new System.Drawing.Point(300, 14);
            this.Alap_Töröltek.Name = "Alap_Töröltek";
            this.Alap_Töröltek.Size = new System.Drawing.Size(149, 24);
            this.Alap_Töröltek.TabIndex = 11;
            this.Alap_Töröltek.Text = "Törölt azonosítók";
            this.Alap_Töröltek.UseVisualStyleBackColor = false;
            this.Alap_Töröltek.CheckedChanged += new System.EventHandler(this.Töröltek_CheckedChanged);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Silver;
            this.Label2.Location = new System.Drawing.Point(3, 47);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(103, 20);
            this.Label2.TabIndex = 97;
            this.Label2.Text = "Megnevezés:";
            // 
            // Alap_Azonosító
            // 
            this.Alap_Azonosító.DropDownHeight = 300;
            this.Alap_Azonosító.FormattingEnabled = true;
            this.Alap_Azonosító.IntegralHeight = false;
            this.Alap_Azonosító.Location = new System.Drawing.Point(114, 10);
            this.Alap_Azonosító.MaxLength = 20;
            this.Alap_Azonosító.Name = "Alap_Azonosító";
            this.Alap_Azonosító.Size = new System.Drawing.Size(180, 28);
            this.Alap_Azonosító.TabIndex = 0;
            this.Alap_Azonosító.SelectedIndexChanged += new System.EventHandler(this.Azonosító_SelectedIndexChanged);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.Silver;
            this.Label3.Location = new System.Drawing.Point(240, 84);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(54, 20);
            this.Label3.TabIndex = 98;
            this.Label3.Text = "Méret:";
            // 
            // Alap_Aktív
            // 
            this.Alap_Aktív.AutoSize = true;
            this.Alap_Aktív.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.Alap_Aktív.Location = new System.Drawing.Point(596, 78);
            this.Alap_Aktív.Name = "Alap_Aktív";
            this.Alap_Aktív.Size = new System.Drawing.Size(68, 24);
            this.Alap_Aktív.TabIndex = 4;
            this.Alap_Aktív.Text = "Törölt";
            this.Alap_Aktív.UseVisualStyleBackColor = false;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.BackColor = System.Drawing.Color.Silver;
            this.Label4.Location = new System.Drawing.Point(5, 84);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(94, 20);
            this.Label4.TabIndex = 99;
            this.Label4.Text = "Költséghely:";
            // 
            // Alap_excel
            // 
            this.Alap_excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Alap_excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Alap_excel.Location = new System.Drawing.Point(865, 6);
            this.Alap_excel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Alap_excel.Name = "Alap_excel";
            this.Alap_excel.Size = new System.Drawing.Size(45, 45);
            this.Alap_excel.TabIndex = 14;
            this.ToolTip1.SetToolTip(this.Alap_excel, "Táblázat adatait excelbe menti");
            this.Alap_excel.UseVisualStyleBackColor = true;
            this.Alap_excel.Click += new System.EventHandler(this.Alap_excel_Click);
            // 
            // Alap_Új_adat
            // 
            this.Alap_Új_adat.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Alap_Új_adat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Alap_Új_adat.Location = new System.Drawing.Point(814, 6);
            this.Alap_Új_adat.Name = "Alap_Új_adat";
            this.Alap_Új_adat.Size = new System.Drawing.Size(45, 45);
            this.Alap_Új_adat.TabIndex = 13;
            this.ToolTip1.SetToolTip(this.Alap_Új_adat, "Új elemnek előkészíti a beviteli mezőket");
            this.Alap_Új_adat.UseVisualStyleBackColor = true;
            this.Alap_Új_adat.Click += new System.EventHandler(this.Új_adat_Click);
            // 
            // Alap_Rögzít
            // 
            this.Alap_Rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Alap_Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Alap_Rögzít.Location = new System.Drawing.Point(944, 6);
            this.Alap_Rögzít.Name = "Alap_Rögzít";
            this.Alap_Rögzít.Size = new System.Drawing.Size(45, 45);
            this.Alap_Rögzít.TabIndex = 10;
            this.ToolTip1.SetToolTip(this.Alap_Rögzít, "Rögzíti az adatokat");
            this.Alap_Rögzít.UseVisualStyleBackColor = true;
            this.Alap_Rögzít.Click += new System.EventHandler(this.Rögzít_Click);
            // 
            // Alap_Frissít
            // 
            this.Alap_Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Alap_Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Alap_Frissít.Location = new System.Drawing.Point(763, 6);
            this.Alap_Frissít.Name = "Alap_Frissít";
            this.Alap_Frissít.Size = new System.Drawing.Size(45, 45);
            this.Alap_Frissít.TabIndex = 12;
            this.ToolTip1.SetToolTip(this.Alap_Frissít, "Frissíti a listát");
            this.Alap_Frissít.UseVisualStyleBackColor = true;
            this.Alap_Frissít.Click += new System.EventHandler(this.Frissít_Click);
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.DarkTurquoise;
            this.TabPage2.Controls.Add(this.IDM_dolgozó);
            this.TabPage2.Controls.Add(this.Könyv_excel);
            this.TabPage2.Controls.Add(this.Könyv_tábla);
            this.TabPage2.Controls.Add(this.Könyv_Felelős1);
            this.TabPage2.Controls.Add(this.Label9);
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
            this.TabPage2.Size = new System.Drawing.Size(1207, 367);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Könyvek";
            // 
            // IDM_dolgozó
            // 
            this.IDM_dolgozó.BackgroundImage = global::Villamos.Properties.Resources.felhasználók32;
            this.IDM_dolgozó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.IDM_dolgozó.Location = new System.Drawing.Point(593, 61);
            this.IDM_dolgozó.Name = "IDM_dolgozó";
            this.IDM_dolgozó.Size = new System.Drawing.Size(45, 45);
            this.IDM_dolgozó.TabIndex = 9;
            this.ToolTip1.SetToolTip(this.IDM_dolgozó, "IDM adatokkal frissíti a dolgozókat");
            this.IDM_dolgozó.UseVisualStyleBackColor = true;
            this.IDM_dolgozó.Click += new System.EventHandler(this.IDM_dolgozó_Click);
            // 
            // Könyv_excel
            // 
            this.Könyv_excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Könyv_excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Könyv_excel.Location = new System.Drawing.Point(695, 8);
            this.Könyv_excel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Könyv_excel.Name = "Könyv_excel";
            this.Könyv_excel.Size = new System.Drawing.Size(45, 45);
            this.Könyv_excel.TabIndex = 8;
            this.ToolTip1.SetToolTip(this.Könyv_excel, "Táblázat adatait excelbe menti");
            this.Könyv_excel.UseVisualStyleBackColor = true;
            this.Könyv_excel.Click += new System.EventHandler(this.Könyv_excel_Click);
            // 
            // Könyv_tábla
            // 
            this.Könyv_tábla.AllowUserToAddRows = false;
            this.Könyv_tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Cyan;
            this.Könyv_tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.Könyv_tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Cyan;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Könyv_tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.Könyv_tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Könyv_tábla.EnableHeadersVisualStyles = false;
            this.Könyv_tábla.FilterAndSortEnabled = true;
            this.Könyv_tábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Könyv_tábla.Location = new System.Drawing.Point(5, 155);
            this.Könyv_tábla.MaxFilterButtonImageHeight = 23;
            this.Könyv_tábla.Name = "Könyv_tábla";
            this.Könyv_tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Könyv_tábla.RowHeadersVisible = false;
            this.Könyv_tábla.RowHeadersWidth = 62;
            this.Könyv_tábla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Könyv_tábla.Size = new System.Drawing.Size(1196, 200);
            this.Könyv_tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Könyv_tábla.TabIndex = 169;
            this.Könyv_tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Könyv_tábla_CellClick);
            // 
            // Könyv_Felelős1
            // 
            this.Könyv_Felelős1.DropDownHeight = 300;
            this.Könyv_Felelős1.FormattingEnabled = true;
            this.Könyv_Felelős1.IntegralHeight = false;
            this.Könyv_Felelős1.Location = new System.Drawing.Point(175, 90);
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
            // Könyv_Töröltek
            // 
            this.Könyv_Töröltek.AutoSize = true;
            this.Könyv_Töröltek.BackColor = System.Drawing.Color.Gold;
            this.Könyv_Töröltek.Location = new System.Drawing.Point(419, 24);
            this.Könyv_Töröltek.Name = "Könyv_Töröltek";
            this.Könyv_Töröltek.Size = new System.Drawing.Size(149, 24);
            this.Könyv_Töröltek.TabIndex = 5;
            this.Könyv_Töröltek.Text = "Törölt azonosítók";
            this.Könyv_Töröltek.UseVisualStyleBackColor = false;
            this.Könyv_Töröltek.CheckedChanged += new System.EventHandler(this.Töröltek_CheckedChanged_1);
            // 
            // Könyv_megnevezés
            // 
            this.Könyv_megnevezés.Location = new System.Drawing.Point(175, 55);
            this.Könyv_megnevezés.MaxLength = 50;
            this.Könyv_megnevezés.Name = "Könyv_megnevezés";
            this.Könyv_megnevezés.Size = new System.Drawing.Size(393, 26);
            this.Könyv_megnevezés.TabIndex = 1;
            // 
            // Könyv_szám
            // 
            this.Könyv_szám.DropDownHeight = 300;
            this.Könyv_szám.FormattingEnabled = true;
            this.Könyv_szám.IntegralHeight = false;
            this.Könyv_szám.Location = new System.Drawing.Point(175, 20);
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
            this.Könyv_Törlés.Location = new System.Drawing.Point(175, 125);
            this.Könyv_Törlés.Name = "Könyv_Törlés";
            this.Könyv_Törlés.Size = new System.Drawing.Size(68, 24);
            this.Könyv_Törlés.TabIndex = 3;
            this.Könyv_Törlés.Text = "Törölt";
            this.Könyv_Törlés.UseVisualStyleBackColor = false;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.BackColor = System.Drawing.Color.Silver;
            this.Label6.Location = new System.Drawing.Point(10, 20);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(142, 20);
            this.Label6.TabIndex = 98;
            this.Label6.Text = "Védőkönyv száma:";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.BackColor = System.Drawing.Color.Silver;
            this.Label7.Location = new System.Drawing.Point(10, 98);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(64, 20);
            this.Label7.TabIndex = 99;
            this.Label7.Text = "Felelős:";
            // 
            // Könyv_új
            // 
            this.Könyv_új.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Könyv_új.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Könyv_új.Location = new System.Drawing.Point(644, 8);
            this.Könyv_új.Name = "Könyv_új";
            this.Könyv_új.Size = new System.Drawing.Size(45, 45);
            this.Könyv_új.TabIndex = 7;
            this.ToolTip1.SetToolTip(this.Könyv_új, "Új elemnek előkészíti a beviteli mezőket");
            this.Könyv_új.UseVisualStyleBackColor = true;
            this.Könyv_új.Click += new System.EventHandler(this.Könyv_új_Click);
            // 
            // Könyv_Rögzít
            // 
            this.Könyv_Rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Könyv_Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Könyv_Rögzít.Location = new System.Drawing.Point(766, 8);
            this.Könyv_Rögzít.Name = "Könyv_Rögzít";
            this.Könyv_Rögzít.Size = new System.Drawing.Size(45, 45);
            this.Könyv_Rögzít.TabIndex = 4;
            this.ToolTip1.SetToolTip(this.Könyv_Rögzít, "Rögzíti az adatokat");
            this.Könyv_Rögzít.UseVisualStyleBackColor = true;
            this.Könyv_Rögzít.Click += new System.EventHandler(this.Rögzít_Click_1);
            // 
            // Frissít
            // 
            this.Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Frissít.Location = new System.Drawing.Point(593, 8);
            this.Frissít.Name = "Frissít";
            this.Frissít.Size = new System.Drawing.Size(45, 45);
            this.Frissít.TabIndex = 6;
            this.ToolTip1.SetToolTip(this.Frissít, "Frissíti a listát");
            this.Frissít.UseVisualStyleBackColor = true;
            this.Frissít.Click += new System.EventHandler(this.Frissít_Click1);
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.DarkTurquoise;
            this.TabPage4.Controls.Add(this.Lekérd_Munkáltatói);
            this.TabPage4.Controls.Add(this.Lekérd_Szerszámkönyvszám);
            this.TabPage4.Controls.Add(this.Lekérd_Tábla);
            this.TabPage4.Controls.Add(this.Lekérd_Szerszámazonosító);
            this.TabPage4.Controls.Add(this.Lekérd_Megnevezés);
            this.TabPage4.Controls.Add(this.Lekérd_Töröltek);
            this.TabPage4.Controls.Add(this.Label17);
            this.TabPage4.Controls.Add(this.Label16);
            this.TabPage4.Controls.Add(this.Label15);
            this.TabPage4.Controls.Add(this.Lekérd_Felelős1);
            this.TabPage4.Controls.Add(this.Lekérd_Command1);
            this.TabPage4.Controls.Add(this.Lekérd_Excelclick);
            this.TabPage4.Controls.Add(this.Lekérd_Nevekkiválasztása);
            this.TabPage4.Controls.Add(this.Lekérd_Visszacsuk);
            this.TabPage4.Controls.Add(this.Lekérd_Jelöltszersz);
            this.TabPage4.Controls.Add(this.Lekérd_Mindtöröl);
            this.TabPage4.Controls.Add(this.Lekérd_Összeskijelöl);
            this.TabPage4.Controls.Add(this.Lekérd_Lenyit);
            this.TabPage4.Controls.Add(this.Lekérd_Anyagkiíró);
            this.TabPage4.Location = new System.Drawing.Point(4, 29);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage4.Size = new System.Drawing.Size(1207, 367);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Lekérdezés";
            // 
            // Lekérd_Munkáltatói
            // 
            this.Lekérd_Munkáltatói.BackgroundImage = global::Villamos.Properties.Resources.Dolgozó_32;
            this.Lekérd_Munkáltatói.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lekérd_Munkáltatói.Location = new System.Drawing.Point(995, 6);
            this.Lekérd_Munkáltatói.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Lekérd_Munkáltatói.Name = "Lekérd_Munkáltatói";
            this.Lekérd_Munkáltatói.Size = new System.Drawing.Size(40, 40);
            this.Lekérd_Munkáltatói.TabIndex = 190;
            this.ToolTip1.SetToolTip(this.Lekérd_Munkáltatói, "Védőkönyvnek megfelelően elkészíti a védőeszköz meghatározást.\r\n");
            this.Lekérd_Munkáltatói.UseVisualStyleBackColor = true;
            this.Lekérd_Munkáltatói.Click += new System.EventHandler(this.Lekérd_Munkáltatói_Click);
            // 
            // Lekérd_Szerszámkönyvszám
            // 
            this.Lekérd_Szerszámkönyvszám.CheckOnClick = true;
            this.Lekérd_Szerszámkönyvszám.FormattingEnabled = true;
            this.Lekérd_Szerszámkönyvszám.Location = new System.Drawing.Point(162, 21);
            this.Lekérd_Szerszámkönyvszám.Name = "Lekérd_Szerszámkönyvszám";
            this.Lekérd_Szerszámkönyvszám.Size = new System.Drawing.Size(412, 25);
            this.Lekérd_Szerszámkönyvszám.TabIndex = 102;
            // 
            // Lekérd_Tábla
            // 
            this.Lekérd_Tábla.AllowUserToAddRows = false;
            this.Lekérd_Tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.DarkTurquoise;
            this.Lekérd_Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.Lekérd_Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.DarkTurquoise;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Lekérd_Tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.Lekérd_Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Lekérd_Tábla.EnableHeadersVisualStyles = false;
            this.Lekérd_Tábla.FilterAndSortEnabled = true;
            this.Lekérd_Tábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Lekérd_Tábla.Location = new System.Drawing.Point(5, 127);
            this.Lekérd_Tábla.MaxFilterButtonImageHeight = 23;
            this.Lekérd_Tábla.Name = "Lekérd_Tábla";
            this.Lekérd_Tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Lekérd_Tábla.RowHeadersVisible = false;
            this.Lekérd_Tábla.RowHeadersWidth = 62;
            this.Lekérd_Tábla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Lekérd_Tábla.Size = new System.Drawing.Size(1196, 234);
            this.Lekérd_Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Lekérd_Tábla.TabIndex = 189;
            this.Lekérd_Tábla.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Lekérd_Tábla_CellDoubleClick);
            // 
            // Lekérd_Szerszámazonosító
            // 
            this.Lekérd_Szerszámazonosító.DropDownHeight = 300;
            this.Lekérd_Szerszámazonosító.FormattingEnabled = true;
            this.Lekérd_Szerszámazonosító.IntegralHeight = false;
            this.Lekérd_Szerszámazonosító.Location = new System.Drawing.Point(162, 93);
            this.Lekérd_Szerszámazonosító.MaxLength = 20;
            this.Lekérd_Szerszámazonosító.Name = "Lekérd_Szerszámazonosító";
            this.Lekérd_Szerszámazonosító.Size = new System.Drawing.Size(223, 28);
            this.Lekérd_Szerszámazonosító.Sorted = true;
            this.Lekérd_Szerszámazonosító.TabIndex = 130;
            this.Lekérd_Szerszámazonosító.SelectedIndexChanged += new System.EventHandler(this.Szerszámazonosító_SelectedIndexChanged);
            // 
            // Lekérd_Megnevezés
            // 
            this.Lekérd_Megnevezés.DropDownHeight = 300;
            this.Lekérd_Megnevezés.FormattingEnabled = true;
            this.Lekérd_Megnevezés.IntegralHeight = false;
            this.Lekérd_Megnevezés.Location = new System.Drawing.Point(410, 93);
            this.Lekérd_Megnevezés.MaxLength = 20;
            this.Lekérd_Megnevezés.Name = "Lekérd_Megnevezés";
            this.Lekérd_Megnevezés.Size = new System.Drawing.Size(451, 28);
            this.Lekérd_Megnevezés.Sorted = true;
            this.Lekérd_Megnevezés.TabIndex = 129;
            this.Lekérd_Megnevezés.SelectedIndexChanged += new System.EventHandler(this.Megnevezés_SelectedIndexChanged);
            // 
            // Lekérd_Töröltek
            // 
            this.Lekérd_Töröltek.AutoSize = true;
            this.Lekérd_Töröltek.BackColor = System.Drawing.Color.Gold;
            this.Lekérd_Töröltek.Location = new System.Drawing.Point(812, 22);
            this.Lekérd_Töröltek.Name = "Lekérd_Töröltek";
            this.Lekérd_Töröltek.Size = new System.Drawing.Size(85, 24);
            this.Lekérd_Töröltek.TabIndex = 126;
            this.Lekérd_Töröltek.Text = "Töröltek";
            this.ToolTip1.SetToolTip(this.Lekérd_Töröltek, "Törölt Védőkönyveket listázza");
            this.Lekérd_Töröltek.UseVisualStyleBackColor = false;
            this.Lekérd_Töröltek.CheckedChanged += new System.EventHandler(this.Töröltek_CheckedChanged_2);
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.BackColor = System.Drawing.Color.Silver;
            this.Label17.Location = new System.Drawing.Point(10, 26);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(133, 20);
            this.Label17.TabIndex = 101;
            this.Label17.Text = "Védőkönyv szám:";
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.BackColor = System.Drawing.Color.Silver;
            this.Label16.Location = new System.Drawing.Point(10, 64);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(64, 20);
            this.Label16.TabIndex = 100;
            this.Label16.Text = "Felelős:";
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.BackColor = System.Drawing.Color.Silver;
            this.Label15.Location = new System.Drawing.Point(10, 101);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(134, 20);
            this.Label15.TabIndex = 98;
            this.Label15.Text = "Eszköz azonosító";
            // 
            // Lekérd_Felelős1
            // 
            this.Lekérd_Felelős1.DropDownHeight = 300;
            this.Lekérd_Felelős1.FormattingEnabled = true;
            this.Lekérd_Felelős1.IntegralHeight = false;
            this.Lekérd_Felelős1.Location = new System.Drawing.Point(162, 56);
            this.Lekérd_Felelős1.MaxLength = 20;
            this.Lekérd_Felelős1.Name = "Lekérd_Felelős1";
            this.Lekérd_Felelős1.Size = new System.Drawing.Size(412, 28);
            this.Lekérd_Felelős1.Sorted = true;
            this.Lekérd_Felelős1.TabIndex = 99;
            // 
            // Lekérd_Command1
            // 
            this.Lekérd_Command1.BackgroundImage = global::Villamos.Properties.Resources.App_dict;
            this.Lekérd_Command1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lekérd_Command1.Location = new System.Drawing.Point(949, 6);
            this.Lekérd_Command1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Lekérd_Command1.Name = "Lekérd_Command1";
            this.Lekérd_Command1.Size = new System.Drawing.Size(40, 40);
            this.Lekérd_Command1.TabIndex = 185;
            this.ToolTip1.SetToolTip(this.Lekérd_Command1, "Védőkönyvön lévő védőeszközöket nyomtatvány forrmájában listázza.\r\n ");
            this.Lekérd_Command1.UseVisualStyleBackColor = true;
            this.Lekérd_Command1.Click += new System.EventHandler(this.Lekérd_Command1_Click);
            // 
            // Lekérd_Excelclick
            // 
            this.Lekérd_Excelclick.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Lekérd_Excelclick.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lekérd_Excelclick.Location = new System.Drawing.Point(903, 6);
            this.Lekérd_Excelclick.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Lekérd_Excelclick.Name = "Lekérd_Excelclick";
            this.Lekérd_Excelclick.Size = new System.Drawing.Size(40, 40);
            this.Lekérd_Excelclick.TabIndex = 184;
            this.ToolTip1.SetToolTip(this.Lekérd_Excelclick, "Táblázat adatait excelbe menti");
            this.Lekérd_Excelclick.UseVisualStyleBackColor = true;
            this.Lekérd_Excelclick.Click += new System.EventHandler(this.Excelclick_Click);
            // 
            // Lekérd_Nevekkiválasztása
            // 
            this.Lekérd_Nevekkiválasztása.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Lekérd_Nevekkiválasztása.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lekérd_Nevekkiválasztása.Location = new System.Drawing.Point(580, 50);
            this.Lekérd_Nevekkiválasztása.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Lekérd_Nevekkiválasztása.Name = "Lekérd_Nevekkiválasztása";
            this.Lekérd_Nevekkiválasztása.Size = new System.Drawing.Size(40, 40);
            this.Lekérd_Nevekkiválasztása.TabIndex = 128;
            this.ToolTip1.SetToolTip(this.Lekérd_Nevekkiválasztása, "Dolgozónévhez rendelt könyvet megkeresi és listázza a tartalmát\r\n");
            this.Lekérd_Nevekkiválasztása.UseVisualStyleBackColor = true;
            this.Lekérd_Nevekkiválasztása.Click += new System.EventHandler(this.Nevekkiválasztása_Click);
            // 
            // Lekérd_Visszacsuk
            // 
            this.Lekérd_Visszacsuk.BackgroundImage = global::Villamos.Properties.Resources.fel;
            this.Lekérd_Visszacsuk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lekérd_Visszacsuk.Location = new System.Drawing.Point(626, 6);
            this.Lekérd_Visszacsuk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Lekérd_Visszacsuk.Name = "Lekérd_Visszacsuk";
            this.Lekérd_Visszacsuk.Size = new System.Drawing.Size(40, 40);
            this.Lekérd_Visszacsuk.TabIndex = 125;
            this.ToolTip1.SetToolTip(this.Lekérd_Visszacsuk, "Felcsukja a Védőkönyv lista mezőt");
            this.Lekérd_Visszacsuk.UseVisualStyleBackColor = true;
            this.Lekérd_Visszacsuk.Click += new System.EventHandler(this.Visszacsuk_Click);
            // 
            // Lekérd_Jelöltszersz
            // 
            this.Lekérd_Jelöltszersz.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Lekérd_Jelöltszersz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lekérd_Jelöltszersz.Location = new System.Drawing.Point(764, 6);
            this.Lekérd_Jelöltszersz.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Lekérd_Jelöltszersz.Name = "Lekérd_Jelöltszersz";
            this.Lekérd_Jelöltszersz.Size = new System.Drawing.Size(40, 40);
            this.Lekérd_Jelöltszersz.TabIndex = 124;
            this.ToolTip1.SetToolTip(this.Lekérd_Jelöltszersz, "Listázza a védőkönyvek listájában kijelölt elemeket");
            this.Lekérd_Jelöltszersz.UseVisualStyleBackColor = true;
            this.Lekérd_Jelöltszersz.Click += new System.EventHandler(this.Jelöltszersz_Click);
            // 
            // Lekérd_Mindtöröl
            // 
            this.Lekérd_Mindtöröl.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.Lekérd_Mindtöröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lekérd_Mindtöröl.Location = new System.Drawing.Point(718, 6);
            this.Lekérd_Mindtöröl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Lekérd_Mindtöröl.Name = "Lekérd_Mindtöröl";
            this.Lekérd_Mindtöröl.Size = new System.Drawing.Size(40, 40);
            this.Lekérd_Mindtöröl.TabIndex = 123;
            this.ToolTip1.SetToolTip(this.Lekérd_Mindtöröl, "Minden kijelölést töröl");
            this.Lekérd_Mindtöröl.UseVisualStyleBackColor = true;
            this.Lekérd_Mindtöröl.Click += new System.EventHandler(this.Mindtöröl_Click);
            // 
            // Lekérd_Összeskijelöl
            // 
            this.Lekérd_Összeskijelöl.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.Lekérd_Összeskijelöl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lekérd_Összeskijelöl.Location = new System.Drawing.Point(672, 6);
            this.Lekérd_Összeskijelöl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Lekérd_Összeskijelöl.Name = "Lekérd_Összeskijelöl";
            this.Lekérd_Összeskijelöl.Size = new System.Drawing.Size(40, 40);
            this.Lekérd_Összeskijelöl.TabIndex = 122;
            this.ToolTip1.SetToolTip(this.Lekérd_Összeskijelöl, "Minden elemet kijelöl");
            this.Lekérd_Összeskijelöl.UseVisualStyleBackColor = true;
            this.Lekérd_Összeskijelöl.Click += new System.EventHandler(this.Összeskijelöl_Click);
            // 
            // Lekérd_Lenyit
            // 
            this.Lekérd_Lenyit.BackgroundImage = global::Villamos.Properties.Resources.le;
            this.Lekérd_Lenyit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lekérd_Lenyit.Location = new System.Drawing.Point(580, 5);
            this.Lekérd_Lenyit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Lekérd_Lenyit.Name = "Lekérd_Lenyit";
            this.Lekérd_Lenyit.Size = new System.Drawing.Size(40, 40);
            this.Lekérd_Lenyit.TabIndex = 121;
            this.ToolTip1.SetToolTip(this.Lekérd_Lenyit, "Lenyitja a védőkönyv számok listáját");
            this.Lekérd_Lenyit.UseVisualStyleBackColor = true;
            this.Lekérd_Lenyit.Click += new System.EventHandler(this.Lenyit_Click);
            // 
            // Lekérd_Anyagkiíró
            // 
            this.Lekérd_Anyagkiíró.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Lekérd_Anyagkiíró.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lekérd_Anyagkiíró.Location = new System.Drawing.Point(867, 76);
            this.Lekérd_Anyagkiíró.Name = "Lekérd_Anyagkiíró";
            this.Lekérd_Anyagkiíró.Size = new System.Drawing.Size(45, 45);
            this.Lekérd_Anyagkiíró.TabIndex = 97;
            this.ToolTip1.SetToolTip(this.Lekérd_Anyagkiíró, "Védőeszközönként listázza, hogy melyik könyvön, hány darab van.\r\n");
            this.Lekérd_Anyagkiíró.UseVisualStyleBackColor = true;
            this.Lekérd_Anyagkiíró.Click += new System.EventHandler(this.Anyagkiíró_Click);
            // 
            // TabPage5
            // 
            this.TabPage5.BackColor = System.Drawing.Color.SeaGreen;
            this.TabPage5.Controls.Add(this.Napló_hely);
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
            this.TabPage5.Size = new System.Drawing.Size(1207, 367);
            this.TabPage5.TabIndex = 4;
            this.TabPage5.Text = "Naplózás";
            // 
            // Napló_hely
            // 
            this.Napló_hely.Location = new System.Drawing.Point(766, 12);
            this.Napló_hely.Name = "Napló_hely";
            this.Napló_hely.Size = new System.Drawing.Size(43, 26);
            this.Napló_hely.TabIndex = 185;
            this.Napló_hely.Visible = false;
            // 
            // Napló_Fájltöröl
            // 
            this.Napló_Fájltöröl.AutoSize = true;
            this.Napló_Fájltöröl.BackColor = System.Drawing.Color.Gold;
            this.Napló_Fájltöröl.Location = new System.Drawing.Point(514, 42);
            this.Napló_Fájltöröl.Name = "Napló_Fájltöröl";
            this.Napló_Fájltöröl.Size = new System.Drawing.Size(159, 24);
            this.Napló_Fájltöröl.TabIndex = 179;
            this.Napló_Fájltöröl.Text = "Bizonylati fájlt töröl";
            this.ToolTip1.SetToolTip(this.Napló_Fájltöröl, "Az Excel fájlt nyomtatás után törli.\r\n");
            this.Napló_Fájltöröl.UseVisualStyleBackColor = false;
            // 
            // Napló_Hovánév
            // 
            this.Napló_Hovánév.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Napló_Hovánév.FormattingEnabled = true;
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
            this.Napló_Hova.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Napló_Hova.FormattingEnabled = true;
            this.Napló_Hova.Location = new System.Drawing.Point(830, 72);
            this.Napló_Hova.MaxLength = 20;
            this.Napló_Hova.Name = "Napló_Hova";
            this.Napló_Hova.Size = new System.Drawing.Size(180, 28);
            this.Napló_Hova.Sorted = true;
            this.Napló_Hova.TabIndex = 177;
            this.Napló_Hova.SelectedIndexChanged += new System.EventHandler(this.Hova_SelectedIndexChanged);
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
            this.Napló_Honnannév.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Napló_Honnannév.FormattingEnabled = true;
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
            this.Napló_Honnan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Napló_Honnan.FormattingEnabled = true;
            this.Napló_Honnan.Location = new System.Drawing.Point(329, 72);
            this.Napló_Honnan.MaxLength = 20;
            this.Napló_Honnan.Name = "Napló_Honnan";
            this.Napló_Honnan.Size = new System.Drawing.Size(180, 28);
            this.Napló_Honnan.Sorted = true;
            this.Napló_Honnan.TabIndex = 174;
            this.Napló_Honnan.SelectedIndexChanged += new System.EventHandler(this.Honnan_SelectedIndexChanged);
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
            this.Napló_Dátumig.TabIndex = 172;
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
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.Napló_Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.Napló_Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.MediumSeaGreen;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Napló_Tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.Napló_Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Napló_Tábla.EnableHeadersVisualStyles = false;
            this.Napló_Tábla.FilterAndSortEnabled = true;
            this.Napló_Tábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Napló_Tábla.Location = new System.Drawing.Point(6, 140);
            this.Napló_Tábla.MaxFilterButtonImageHeight = 23;
            this.Napló_Tábla.Name = "Napló_Tábla";
            this.Napló_Tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.MediumSeaGreen;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Napló_Tábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.Napló_Tábla.RowHeadersWidth = 30;
            this.Napló_Tábla.Size = new System.Drawing.Size(1195, 221);
            this.Napló_Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Napló_Tábla.TabIndex = 170;
            this.Napló_Tábla.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Napló_Tábla_CellDoubleClick);
            // 
            // Napló_Dátumtól
            // 
            this.Napló_Dátumtól.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Napló_Dátumtól.Location = new System.Drawing.Point(98, 8);
            this.Napló_Dátumtól.Name = "Napló_Dátumtól";
            this.Napló_Dátumtól.Size = new System.Drawing.Size(118, 26);
            this.Napló_Dátumtól.TabIndex = 103;
            this.Napló_Dátumtól.ValueChanged += new System.EventHandler(this.Dátumtól_ValueChanged);
            // 
            // Napló_Nyomtat
            // 
            this.Napló_Nyomtat.AutoSize = true;
            this.Napló_Nyomtat.BackColor = System.Drawing.Color.Gold;
            this.Napló_Nyomtat.Location = new System.Drawing.Point(514, 9);
            this.Napló_Nyomtat.Name = "Napló_Nyomtat";
            this.Napló_Nyomtat.Size = new System.Drawing.Size(154, 24);
            this.Napló_Nyomtat.TabIndex = 105;
            this.Napló_Nyomtat.Text = "Nyomtatást készít";
            this.ToolTip1.SetToolTip(this.Napló_Nyomtat, "Kijelölése esetén 2 pld-ban kinyomtatja a ki/be adási nyomtatványt.");
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
            this.Napló_Nyomtatvány.Location = new System.Drawing.Point(691, 27);
            this.Napló_Nyomtatvány.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Napló_Nyomtatvány.Name = "Napló_Nyomtatvány";
            this.Napló_Nyomtatvány.Size = new System.Drawing.Size(40, 40);
            this.Napló_Nyomtatvány.TabIndex = 184;
            this.ToolTip1.SetToolTip(this.Napló_Nyomtatvány, "Elkészíti a védőeszköz le- felvételi Excel táblát.");
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
            this.Napló_Excel_gomb.TabIndex = 183;
            this.ToolTip1.SetToolTip(this.Napló_Excel_gomb, "Táblázat adatait excelbe menti");
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
            this.Napló_Listáz.TabIndex = 182;
            this.ToolTip1.SetToolTip(this.Napló_Listáz, "Az időintervallumnak és a Honnan-hovának \r\nmegfelelően listázza a naplózásokat.\r\n" +
        "");
            this.Napló_Listáz.UseVisualStyleBackColor = true;
            this.Napló_Listáz.Click += new System.EventHandler(this.Listáz_Click);
            // 
            // TabPage6
            // 
            this.TabPage6.BackColor = System.Drawing.Color.LightGreen;
            this.TabPage6.Controls.Add(this.PDF_néző);
            this.TabPage6.Location = new System.Drawing.Point(4, 29);
            this.TabPage6.Name = "TabPage6";
            this.TabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage6.Size = new System.Drawing.Size(1207, 367);
            this.TabPage6.TabIndex = 5;
            this.TabPage6.Text = "Pdf lap";
            // 
            // PDF_néző
            // 
            this.PDF_néző.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PDF_néző.Location = new System.Drawing.Point(9, 8);
            this.PDF_néző.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.PDF_néző.Name = "PDF_néző";
            this.PDF_néző.ShowToolbar = false;
            this.PDF_néző.Size = new System.Drawing.Size(1189, 348);
            this.PDF_néző.TabIndex = 241;
            this.PDF_néző.ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitWidth;
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1169, 5);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 171;
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // Ablak_védő
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.ClientSize = new System.Drawing.Size(1226, 463);
            this.Controls.Add(this.Lapfülek);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.Panel2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_védő";
            this.Text = "Védőeszköz Nyilvántartás";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_védő_Load);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.Lapfülek.ResumeLayout(false);
            this.TabPage3.ResumeLayout(false);
            this.TabPage3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_Könyv)).EndInit();
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Alap_tábla)).EndInit();
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Könyv_tábla)).EndInit();
            this.TabPage4.ResumeLayout(false);
            this.TabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Lekérd_Tábla)).EndInit();
            this.TabPage5.ResumeLayout(false);
            this.TabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Napló_Tábla)).EndInit();
            this.TabPage6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        internal Panel Panel2;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal Button BtnSúgó;
        internal TabControl Lapfülek;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal Button Alap_Frissít;
        internal Button Alap_Új_adat;
        internal Button Alap_Rögzít;
        internal ComboBox Alap_Azonosító;
        internal Label Label34;
        internal TextBox Alap_Költséghely;
        internal TextBox Alap_Méret;
        internal TextBox Alap_Megnevezés;
        internal CheckBox Alap_Töröltek;
        internal CheckBox Alap_Aktív;
        internal Label Label4;
        internal Label Label3;
        internal Label Label2;
        internal Zuby.ADGV.AdvancedDataGridView Alap_tábla;
        internal Button Könyv_új;
        internal Button Könyv_Rögzít;
        internal Button Frissít;
        internal CheckBox Könyv_Töröltek;
        internal TextBox Könyv_megnevezés;
        internal ComboBox Könyv_szám;
        internal CheckBox Könyv_Törlés;
        internal Label Label6;
        internal Label Label7;
        internal Zuby.ADGV.AdvancedDataGridView Könyv_tábla;
        internal ComboBox Könyv_Felelős1;
        internal Label Label9;
        internal TabPage TabPage3;
        internal TabPage TabPage4;
        internal TabPage TabPage5;
        internal DateTimePicker Napló_Dátumtól;
        internal CheckBox Napló_Nyomtat;
        internal Label Label10;
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
        internal Button Napló_Listáz;
        internal Button Napló_Excel_gomb;
        internal Button Napló_Nyomtatvány;
        internal TextBox Napló_hely;
        internal Label Label17;
        internal Label Label16;
        internal Label Label15;
        internal ComboBox Lekérd_Felelős1;
        internal Button Lekérd_Anyagkiíró;
        internal CheckedListBox Lekérd_Szerszámkönyvszám;
        internal Button Lekérd_Command1;
        internal Button Lekérd_Excelclick;
        internal ComboBox Lekérd_Szerszámazonosító;
        internal ComboBox Lekérd_Megnevezés;
        internal Button Lekérd_Nevekkiválasztása;
        internal CheckBox Lekérd_Töröltek;
        internal Button Lekérd_Visszacsuk;
        internal Button Lekérd_Jelöltszersz;
        internal Button Lekérd_Mindtöröl;
        internal Button Lekérd_Összeskijelöl;
        internal Button Lekérd_Lenyit;
        internal Zuby.ADGV.AdvancedDataGridView Lekérd_Tábla;
        internal ComboBox HováNév;
        internal ComboBox Hova;
        internal Label Label18;
        internal ComboBox HonnanNév;
        internal ComboBox Honnan;
        internal Label Label19;
        internal Button Könyv_excel;
        internal Button Alap_excel;
        internal TabPage TabPage6;
        internal TextBox Alap_Szint;
        internal Label Label30;
        internal ComboBox Alap_védelem;
        internal TextBox Alap_szabvány;
        internal Label Label29;
        internal TextBox Alap_kockázat;
        internal Label Label28;
        internal Label Label27;
        internal TextBox Alap_Munk_Megnevezés;
        internal Label Label26;
        internal Button Lekérd_Munkáltatói;
        internal ToolTip ToolTip1;
        internal Button IDM_dolgozó;
        internal Zuby.ADGV.AdvancedDataGridView Tábla_Könyv;
        internal Label Label20;
        internal Label Label21;
        internal Label HonnanMennyiség;
        internal Label HováMennyiség;
        internal ComboBox SzerszámAzonosító;
        internal Label Label23;
        internal TextBox Megnevezés;
        internal Label Label24;
        internal TextBox Mennyiség;
        internal Label Label25;
        internal TextBox Gyáriszám;
        internal Button Rögzít;
        internal Button Könyvelés_Szűrés;
        internal Label label1;
        internal Panel panel1;
        internal Label label8;
        internal TextBox Könyv_SzűrőTXT;
        internal Label label5;
        internal PdfiumViewer.PdfViewer PDF_néző;
        internal CheckBox Más_dátum;
        internal DateTimePicker Könyvelési_dátum;
    }
}