using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
    public partial class Ablak_külső : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_külső));
            this.LapFülek = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.Cégek_engedélyezésre = new System.Windows.Forms.Button();
            this.Cég_engedély_státus = new System.Windows.Forms.ComboBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Cég_excel = new System.Windows.Forms.Button();
            this.Alap_Frissít = new System.Windows.Forms.Button();
            this.Cég_tábla = new System.Windows.Forms.DataGridView();
            this.Cég_mikor = new System.Windows.Forms.ComboBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Cég_Aktív = new System.Windows.Forms.CheckBox();
            this.Cég_Érv_vég = new System.Windows.Forms.DateTimePicker();
            this.Cég_Érv_kezdet = new System.Windows.Forms.DateTimePicker();
            this.Cég_felelős_telefon = new System.Windows.Forms.TextBox();
            this.Cég_felelős_személy = new System.Windows.Forms.TextBox();
            this.Cég_Munkaleírás = new System.Windows.Forms.TextBox();
            this.Cég_email = new System.Windows.Forms.TextBox();
            this.Cég_címe = new System.Windows.Forms.TextBox();
            this.Cég_cég = new System.Windows.Forms.TextBox();
            this.Cég_sorszám = new System.Windows.Forms.TextBox();
            this.Label16 = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Alap_Új_adat = new System.Windows.Forms.Button();
            this.Alap_Rögzít = new System.Windows.Forms.Button();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.Dolgozó_töröl = new System.Windows.Forms.Button();
            this.Dolgozó_beolvas = new System.Windows.Forms.Button();
            this.Dolgozó_kivitel = new System.Windows.Forms.Button();
            this.Dolg_frissít = new System.Windows.Forms.Button();
            this.Dolg_új = new System.Windows.Forms.Button();
            this.Dolg_Rögzít = new System.Windows.Forms.Button();
            this.Dolg_Státus = new System.Windows.Forms.ComboBox();
            this.Label20 = new System.Windows.Forms.Label();
            this.Dolg_Személyi = new System.Windows.Forms.TextBox();
            this.Dolg_Dolgozónév = new System.Windows.Forms.TextBox();
            this.Label22 = new System.Windows.Forms.Label();
            this.Label24 = new System.Windows.Forms.Label();
            this.Label17 = new System.Windows.Forms.Label();
            this.Dolg_munka = new System.Windows.Forms.Label();
            this.Dolg_cégid = new System.Windows.Forms.Label();
            this.Dolg_cégneve = new System.Windows.Forms.Label();
            this.Label21 = new System.Windows.Forms.Label();
            this.Dolg_tábla = new System.Windows.Forms.DataGridView();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.Autó_töröl = new System.Windows.Forms.Button();
            this.Autó_beolvas = new System.Windows.Forms.Button();
            this.Autó_beviteli = new System.Windows.Forms.Button();
            this.Label6 = new System.Windows.Forms.Label();
            this.Autó_munka = new System.Windows.Forms.Label();
            this.Autó_Cégid = new System.Windows.Forms.Label();
            this.Autó_cégnév = new System.Windows.Forms.Label();
            this.Autó_FRSZ = new System.Windows.Forms.TextBox();
            this.Autó_státus = new System.Windows.Forms.ComboBox();
            this.Tábla_autó = new System.Windows.Forms.DataGridView();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Autó_Frissít = new System.Windows.Forms.Button();
            this.Autó_Új = new System.Windows.Forms.Button();
            this.Autó_ok = new System.Windows.Forms.Button();
            this.TabPage6 = new System.Windows.Forms.TabPage();
            this.Telephely_Cégid = new System.Windows.Forms.Label();
            this.Label25 = new System.Windows.Forms.Label();
            this.Telephely_Munka = new System.Windows.Forms.Label();
            this.Telephely_Cégnév = new System.Windows.Forms.Label();
            this.Label28 = new System.Windows.Forms.Label();
            this.Telephely_Tábla = new System.Windows.Forms.DataGridView();
            this.Telephely = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Button2 = new System.Windows.Forms.Button();
            this.Telephely_rögzít = new System.Windows.Forms.Button();
            this.Btnkilelöltörlés = new System.Windows.Forms.Button();
            this.BtnKijelölcsop = new System.Windows.Forms.Button();
            this.Btn3szak = new System.Windows.Forms.Button();
            this.Btn2szak = new System.Windows.Forms.Button();
            this.Btn1szak = new System.Windows.Forms.Button();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.Vezér = new System.Windows.Forms.CheckBox();
            this.Engedély_sorszámok = new System.Windows.Forms.TextBox();
            this.Engedély_tábla = new System.Windows.Forms.DataGridView();
            this.Engedély_teljes_lista = new System.Windows.Forms.Button();
            this.Engedély_frissít = new System.Windows.Forms.Button();
            this.Engedély_visszavonás = new System.Windows.Forms.Button();
            this.Engedély_elutasítás = new System.Windows.Forms.Button();
            this.BtnSzakszeng = new System.Windows.Forms.Button();
            this.TabPage5 = new System.Windows.Forms.TabPage();
            this.Lekérd_dolgozó_lista = new System.Windows.Forms.Button();
            this.Lekérd_autó_Lista = new System.Windows.Forms.Button();
            this.Lekérd_Excel = new System.Windows.Forms.Button();
            this.Lekérd_autó = new System.Windows.Forms.Button();
            this.Lekérd_dolgozó = new System.Windows.Forms.Button();
            this.Lekérdezés_tábla = new System.Windows.Forms.DataGridView();
            this.TabPage7 = new System.Windows.Forms.TabPage();
            this.WebBrowser1 = new System.Windows.Forms.WebBrowser();
            this.Email_Aláírás = new System.Windows.Forms.TextBox();
            this.Label27 = new System.Windows.Forms.Label();
            this.Email_másolat = new System.Windows.Forms.TextBox();
            this.Label26 = new System.Windows.Forms.Label();
            this.Email_frissít = new System.Windows.Forms.Button();
            this.Email_rögzít = new System.Windows.Forms.Button();
            this.TabPage8 = new System.Windows.Forms.TabPage();
            this.PDF_néző = new PdfiumViewer.PdfViewer();
            this.Doksik = new System.Windows.Forms.TextBox();
            this.Könyvtár = new System.Windows.Forms.TextBox();
            this.TxtKérrelemPDF = new System.Windows.Forms.TextBox();
            this.PDF_lista_frissít = new System.Windows.Forms.Button();
            this.PDF_lista = new System.Windows.Forms.ListBox();
            this.Label29 = new System.Windows.Forms.Label();
            this.PDF_munka = new System.Windows.Forms.Label();
            this.PDF_cégid = new System.Windows.Forms.Label();
            this.PDF_cégneve = new System.Windows.Forms.Label();
            this.Label33 = new System.Windows.Forms.Label();
            this.PDF_törlés = new System.Windows.Forms.Button();
            this.PDF_rögzít = new System.Windows.Forms.Button();
            this.PDF_feltöltés = new System.Windows.Forms.Button();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Btn_Súgó = new System.Windows.Forms.Button();
            this.LapFülek.SuspendLayout();
            this.TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Cég_tábla)).BeginInit();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dolg_tábla)).BeginInit();
            this.TabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_autó)).BeginInit();
            this.TabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Telephely_Tábla)).BeginInit();
            this.TabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Engedély_tábla)).BeginInit();
            this.TabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Lekérdezés_tábla)).BeginInit();
            this.TabPage7.SuspendLayout();
            this.TabPage8.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // LapFülek
            // 
            this.LapFülek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LapFülek.Controls.Add(this.TabPage1);
            this.LapFülek.Controls.Add(this.TabPage2);
            this.LapFülek.Controls.Add(this.TabPage3);
            this.LapFülek.Controls.Add(this.TabPage6);
            this.LapFülek.Controls.Add(this.TabPage4);
            this.LapFülek.Controls.Add(this.TabPage5);
            this.LapFülek.Controls.Add(this.TabPage7);
            this.LapFülek.Controls.Add(this.TabPage8);
            this.LapFülek.Location = new System.Drawing.Point(4, 46);
            this.LapFülek.Name = "LapFülek";
            this.LapFülek.Padding = new System.Drawing.Point(16, 3);
            this.LapFülek.SelectedIndex = 0;
            this.LapFülek.Size = new System.Drawing.Size(1041, 476);
            this.LapFülek.TabIndex = 63;
            this.LapFülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LapFülek_DrawItem);
            this.LapFülek.SelectedIndexChanged += new System.EventHandler(this.Fülek_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.TabPage1.Controls.Add(this.Cégek_engedélyezésre);
            this.TabPage1.Controls.Add(this.Cég_engedély_státus);
            this.TabPage1.Controls.Add(this.Label5);
            this.TabPage1.Controls.Add(this.Cég_excel);
            this.TabPage1.Controls.Add(this.Alap_Frissít);
            this.TabPage1.Controls.Add(this.Cég_tábla);
            this.TabPage1.Controls.Add(this.Cég_mikor);
            this.TabPage1.Controls.Add(this.Label4);
            this.TabPage1.Controls.Add(this.Cég_Aktív);
            this.TabPage1.Controls.Add(this.Cég_Érv_vég);
            this.TabPage1.Controls.Add(this.Cég_Érv_kezdet);
            this.TabPage1.Controls.Add(this.Cég_felelős_telefon);
            this.TabPage1.Controls.Add(this.Cég_felelős_személy);
            this.TabPage1.Controls.Add(this.Cég_Munkaleírás);
            this.TabPage1.Controls.Add(this.Cég_email);
            this.TabPage1.Controls.Add(this.Cég_címe);
            this.TabPage1.Controls.Add(this.Cég_cég);
            this.TabPage1.Controls.Add(this.Cég_sorszám);
            this.TabPage1.Controls.Add(this.Label16);
            this.TabPage1.Controls.Add(this.Label15);
            this.TabPage1.Controls.Add(this.Label14);
            this.TabPage1.Controls.Add(this.Label12);
            this.TabPage1.Controls.Add(this.Label11);
            this.TabPage1.Controls.Add(this.Label10);
            this.TabPage1.Controls.Add(this.Label9);
            this.TabPage1.Controls.Add(this.Label8);
            this.TabPage1.Controls.Add(this.Label7);
            this.TabPage1.Controls.Add(this.Alap_Új_adat);
            this.TabPage1.Controls.Add(this.Alap_Rögzít);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1033, 443);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Cégek ";
            // 
            // Cégek_engedélyezésre
            // 
            this.Cégek_engedélyezésre.BackgroundImage = global::Villamos.Properties.Resources.Document_write;
            this.Cégek_engedélyezésre.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cégek_engedélyezésre.Location = new System.Drawing.Point(982, 62);
            this.Cégek_engedélyezésre.Name = "Cégek_engedélyezésre";
            this.Cégek_engedélyezésre.Size = new System.Drawing.Size(45, 45);
            this.Cégek_engedélyezésre.TabIndex = 191;
            this.ToolTip1.SetToolTip(this.Cégek_engedélyezésre, "Engedélyezésre továbbít");
            this.Cégek_engedélyezésre.UseVisualStyleBackColor = true;
            this.Cégek_engedélyezésre.Click += new System.EventHandler(this.Cégek_engedélyezésre_Click);
            // 
            // Cég_engedély_státus
            // 
            this.Cég_engedély_státus.FormattingEnabled = true;
            this.Cég_engedély_státus.Location = new System.Drawing.Point(628, 5);
            this.Cég_engedély_státus.Name = "Cég_engedély_státus";
            this.Cég_engedély_státus.Size = new System.Drawing.Size(238, 28);
            this.Cég_engedély_státus.TabIndex = 190;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(486, 13);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(136, 20);
            this.Label5.TabIndex = 189;
            this.Label5.Text = "Engedély státusa:";
            // 
            // Cég_excel
            // 
            this.Cég_excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Cég_excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cég_excel.Location = new System.Drawing.Point(932, 116);
            this.Cég_excel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Cég_excel.Name = "Cég_excel";
            this.Cég_excel.Size = new System.Drawing.Size(45, 45);
            this.Cég_excel.TabIndex = 14;
            this.ToolTip1.SetToolTip(this.Cég_excel, "Listázott adatok excelbe töltése");
            this.Cég_excel.UseVisualStyleBackColor = true;
            this.Cég_excel.Click += new System.EventHandler(this.Cég_excel_Click);
            // 
            // Alap_Frissít
            // 
            this.Alap_Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Alap_Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Alap_Frissít.Location = new System.Drawing.Point(983, 116);
            this.Alap_Frissít.Name = "Alap_Frissít";
            this.Alap_Frissít.Size = new System.Drawing.Size(45, 45);
            this.Alap_Frissít.TabIndex = 12;
            this.ToolTip1.SetToolTip(this.Alap_Frissít, "Frissíti az adatokat");
            this.Alap_Frissít.UseVisualStyleBackColor = true;
            this.Alap_Frissít.Click += new System.EventHandler(this.Alap_Frissít_Click);
            // 
            // Cég_tábla
            // 
            this.Cég_tábla.AllowUserToAddRows = false;
            this.Cég_tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.Cég_tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Cég_tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Cég_tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Cég_tábla.EnableHeadersVisualStyles = false;
            this.Cég_tábla.Location = new System.Drawing.Point(5, 236);
            this.Cég_tábla.Name = "Cég_tábla";
            this.Cég_tábla.RowHeadersWidth = 25;
            this.Cég_tábla.Size = new System.Drawing.Size(1022, 201);
            this.Cég_tábla.TabIndex = 105;
            this.Cég_tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Cég_tábla_CellClick);
            // 
            // Cég_mikor
            // 
            this.Cég_mikor.FormattingEnabled = true;
            this.Cég_mikor.Location = new System.Drawing.Point(697, 167);
            this.Cég_mikor.Name = "Cég_mikor";
            this.Cég_mikor.Size = new System.Drawing.Size(331, 28);
            this.Cég_mikor.TabIndex = 7;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(527, 175);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(148, 20);
            this.Label4.TabIndex = 103;
            this.Label4.Text = "Munkavégzés ideje:";
            // 
            // Cég_Aktív
            // 
            this.Cég_Aktív.AutoSize = true;
            this.Cég_Aktív.BackColor = System.Drawing.Color.RoyalBlue;
            this.Cég_Aktív.Location = new System.Drawing.Point(960, 203);
            this.Cég_Aktív.Name = "Cég_Aktív";
            this.Cég_Aktív.Size = new System.Drawing.Size(68, 24);
            this.Cég_Aktív.TabIndex = 10;
            this.Cég_Aktív.Text = "Törölt";
            this.Cég_Aktív.UseVisualStyleBackColor = false;
            // 
            // Cég_Érv_vég
            // 
            this.Cég_Érv_vég.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Cég_Érv_vég.Location = new System.Drawing.Point(402, 169);
            this.Cég_Érv_vég.Name = "Cég_Érv_vég";
            this.Cég_Érv_vég.Size = new System.Drawing.Size(119, 26);
            this.Cég_Érv_vég.TabIndex = 6;
            // 
            // Cég_Érv_kezdet
            // 
            this.Cég_Érv_kezdet.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Cég_Érv_kezdet.Location = new System.Drawing.Point(172, 167);
            this.Cég_Érv_kezdet.Name = "Cég_Érv_kezdet";
            this.Cég_Érv_kezdet.Size = new System.Drawing.Size(119, 26);
            this.Cég_Érv_kezdet.TabIndex = 5;
            // 
            // Cég_felelős_telefon
            // 
            this.Cég_felelős_telefon.Location = new System.Drawing.Point(697, 201);
            this.Cég_felelős_telefon.Name = "Cég_felelős_telefon";
            this.Cég_felelős_telefon.Size = new System.Drawing.Size(195, 26);
            this.Cég_felelős_telefon.TabIndex = 9;
            // 
            // Cég_felelős_személy
            // 
            this.Cég_felelős_személy.Location = new System.Drawing.Point(172, 204);
            this.Cég_felelős_személy.Name = "Cég_felelős_személy";
            this.Cég_felelős_személy.Size = new System.Drawing.Size(331, 26);
            this.Cég_felelős_személy.TabIndex = 8;
            // 
            // Cég_Munkaleírás
            // 
            this.Cég_Munkaleírás.Location = new System.Drawing.Point(172, 135);
            this.Cég_Munkaleírás.Name = "Cég_Munkaleírás";
            this.Cég_Munkaleírás.Size = new System.Drawing.Size(694, 26);
            this.Cég_Munkaleírás.TabIndex = 4;
            // 
            // Cég_email
            // 
            this.Cég_email.Location = new System.Drawing.Point(172, 103);
            this.Cég_email.Name = "Cég_email";
            this.Cég_email.Size = new System.Drawing.Size(331, 26);
            this.Cég_email.TabIndex = 3;
            // 
            // Cég_címe
            // 
            this.Cég_címe.Location = new System.Drawing.Point(172, 71);
            this.Cég_címe.Name = "Cég_címe";
            this.Cég_címe.Size = new System.Drawing.Size(694, 26);
            this.Cég_címe.TabIndex = 2;
            // 
            // Cég_cég
            // 
            this.Cég_cég.Location = new System.Drawing.Point(172, 39);
            this.Cég_cég.Name = "Cég_cég";
            this.Cég_cég.Size = new System.Drawing.Size(694, 26);
            this.Cég_cég.TabIndex = 1;
            // 
            // Cég_sorszám
            // 
            this.Cég_sorszám.Enabled = false;
            this.Cég_sorszám.Location = new System.Drawing.Point(172, 7);
            this.Cég_sorszám.Name = "Cég_sorszám";
            this.Cég_sorszám.Size = new System.Drawing.Size(100, 26);
            this.Cég_sorszám.TabIndex = 0;
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Location = new System.Drawing.Point(10, 13);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(76, 20);
            this.Label16.TabIndex = 81;
            this.Label16.Text = "Sorszám:";
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Location = new System.Drawing.Point(10, 45);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(80, 20);
            this.Label15.TabIndex = 80;
            this.Label15.Text = "Cég neve:";
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(10, 77);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(79, 20);
            this.Label14.TabIndex = 79;
            this.Label14.Text = "Cég címe:";
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(10, 109);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(88, 20);
            this.Label12.TabIndex = 78;
            this.Label12.Text = "Cég e-mail:";
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(10, 141);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(98, 20);
            this.Label11.TabIndex = 77;
            this.Label11.Text = "Munkaleírás:";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(10, 173);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(122, 20);
            this.Label10.TabIndex = 76;
            this.Label10.Text = "Munka kezdete:";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(297, 174);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(99, 20);
            this.Label9.TabIndex = 75;
            this.Label9.Text = "Munka vége:";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(10, 210);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(125, 20);
            this.Label8.TabIndex = 74;
            this.Label8.Text = "Felelős személy:";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(527, 210);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(164, 20);
            this.Label7.TabIndex = 73;
            this.Label7.Text = "Felelős telefonszáma:";
            // 
            // Alap_Új_adat
            // 
            this.Alap_Új_adat.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Alap_Új_adat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Alap_Új_adat.Location = new System.Drawing.Point(931, 8);
            this.Alap_Új_adat.Name = "Alap_Új_adat";
            this.Alap_Új_adat.Size = new System.Drawing.Size(45, 45);
            this.Alap_Új_adat.TabIndex = 13;
            this.ToolTip1.SetToolTip(this.Alap_Új_adat, "Új adat");
            this.Alap_Új_adat.UseVisualStyleBackColor = true;
            this.Alap_Új_adat.Click += new System.EventHandler(this.Alap_Új_adat_Click);
            // 
            // Alap_Rögzít
            // 
            this.Alap_Rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Alap_Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Alap_Rögzít.Location = new System.Drawing.Point(982, 7);
            this.Alap_Rögzít.Name = "Alap_Rögzít";
            this.Alap_Rögzít.Size = new System.Drawing.Size(45, 45);
            this.Alap_Rögzít.TabIndex = 11;
            this.ToolTip1.SetToolTip(this.Alap_Rögzít, "Rögzít/Módosít");
            this.Alap_Rögzít.UseVisualStyleBackColor = true;
            this.Alap_Rögzít.Click += new System.EventHandler(this.Alap_Rögzít_Click);
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.TabPage2.Controls.Add(this.Dolgozó_töröl);
            this.TabPage2.Controls.Add(this.Dolgozó_beolvas);
            this.TabPage2.Controls.Add(this.Dolgozó_kivitel);
            this.TabPage2.Controls.Add(this.Dolg_frissít);
            this.TabPage2.Controls.Add(this.Dolg_új);
            this.TabPage2.Controls.Add(this.Dolg_Rögzít);
            this.TabPage2.Controls.Add(this.Dolg_Státus);
            this.TabPage2.Controls.Add(this.Label20);
            this.TabPage2.Controls.Add(this.Dolg_Személyi);
            this.TabPage2.Controls.Add(this.Dolg_Dolgozónév);
            this.TabPage2.Controls.Add(this.Label22);
            this.TabPage2.Controls.Add(this.Label24);
            this.TabPage2.Controls.Add(this.Label17);
            this.TabPage2.Controls.Add(this.Dolg_munka);
            this.TabPage2.Controls.Add(this.Dolg_cégid);
            this.TabPage2.Controls.Add(this.Dolg_cégneve);
            this.TabPage2.Controls.Add(this.Label21);
            this.TabPage2.Controls.Add(this.Dolg_tábla);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(1033, 443);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Dolgozók";
            // 
            // Dolgozó_töröl
            // 
            this.Dolgozó_töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Dolgozó_töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Dolgozó_töröl.Location = new System.Drawing.Point(755, 114);
            this.Dolgozó_töröl.Name = "Dolgozó_töröl";
            this.Dolgozó_töröl.Size = new System.Drawing.Size(45, 45);
            this.Dolgozó_töröl.TabIndex = 129;
            this.ToolTip1.SetToolTip(this.Dolgozó_töröl, "Törlés");
            this.Dolgozó_töröl.UseVisualStyleBackColor = true;
            this.Dolgozó_töröl.Click += new System.EventHandler(this.Dolgozó_töröl_Click);
            // 
            // Dolgozó_beolvas
            // 
            this.Dolgozó_beolvas.BackColor = System.Drawing.Color.DarkTurquoise;
            this.Dolgozó_beolvas.BackgroundImage = global::Villamos.Properties.Resources.Custom_Icon_Design_Flatastic_1_Import;
            this.Dolgozó_beolvas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Dolgozó_beolvas.Location = new System.Drawing.Point(857, 114);
            this.Dolgozó_beolvas.Name = "Dolgozó_beolvas";
            this.Dolgozó_beolvas.Size = new System.Drawing.Size(45, 45);
            this.Dolgozó_beolvas.TabIndex = 128;
            this.ToolTip1.SetToolTip(this.Dolgozó_beolvas, "Adatok feltöltése");
            this.Dolgozó_beolvas.UseVisualStyleBackColor = false;
            this.Dolgozó_beolvas.Click += new System.EventHandler(this.Dolgozó_beolvas_Click);
            // 
            // Dolgozó_kivitel
            // 
            this.Dolgozó_kivitel.BackColor = System.Drawing.Color.DarkTurquoise;
            this.Dolgozó_kivitel.BackgroundImage = global::Villamos.Properties.Resources.Custom_Icon_Design_Flatastic_1_Export;
            this.Dolgozó_kivitel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Dolgozó_kivitel.Location = new System.Drawing.Point(806, 114);
            this.Dolgozó_kivitel.Name = "Dolgozó_kivitel";
            this.Dolgozó_kivitel.Size = new System.Drawing.Size(45, 45);
            this.Dolgozó_kivitel.TabIndex = 127;
            this.ToolTip1.SetToolTip(this.Dolgozó_kivitel, "Feltöltő tábla készítés");
            this.Dolgozó_kivitel.UseVisualStyleBackColor = false;
            this.Dolgozó_kivitel.Click += new System.EventHandler(this.Dolgozó_kivitel_Click);
            // 
            // Dolg_frissít
            // 
            this.Dolg_frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Dolg_frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Dolg_frissít.Location = new System.Drawing.Point(970, 114);
            this.Dolg_frissít.Name = "Dolg_frissít";
            this.Dolg_frissít.Size = new System.Drawing.Size(45, 45);
            this.Dolg_frissít.TabIndex = 126;
            this.ToolTip1.SetToolTip(this.Dolg_frissít, "Frissíti az adatokat");
            this.Dolg_frissít.UseVisualStyleBackColor = true;
            this.Dolg_frissít.Click += new System.EventHandler(this.Dolg_frissít_Click);
            // 
            // Dolg_új
            // 
            this.Dolg_új.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Dolg_új.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Dolg_új.Location = new System.Drawing.Point(919, 114);
            this.Dolg_új.Name = "Dolg_új";
            this.Dolg_új.Size = new System.Drawing.Size(45, 45);
            this.Dolg_új.TabIndex = 125;
            this.ToolTip1.SetToolTip(this.Dolg_új, "Új adat");
            this.Dolg_új.UseVisualStyleBackColor = true;
            this.Dolg_új.Click += new System.EventHandler(this.Dolg_új_Click);
            // 
            // Dolg_Rögzít
            // 
            this.Dolg_Rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Dolg_Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Dolg_Rögzít.Location = new System.Drawing.Point(919, 63);
            this.Dolg_Rögzít.Name = "Dolg_Rögzít";
            this.Dolg_Rögzít.Size = new System.Drawing.Size(45, 45);
            this.Dolg_Rögzít.TabIndex = 124;
            this.ToolTip1.SetToolTip(this.Dolg_Rögzít, "Rögzít/Módosít");
            this.Dolg_Rögzít.UseVisualStyleBackColor = true;
            this.Dolg_Rögzít.Click += new System.EventHandler(this.Dolg_Rögzít_Click);
            // 
            // Dolg_Státus
            // 
            this.Dolg_Státus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Dolg_Státus.FormattingEnabled = true;
            this.Dolg_Státus.Location = new System.Drawing.Point(206, 133);
            this.Dolg_Státus.Name = "Dolg_Státus";
            this.Dolg_Státus.Size = new System.Drawing.Size(159, 28);
            this.Dolg_Státus.TabIndex = 123;
            // 
            // Label20
            // 
            this.Label20.AutoSize = true;
            this.Label20.Location = new System.Drawing.Point(7, 141);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(60, 20);
            this.Label20.TabIndex = 122;
            this.Label20.Text = "Státus:";
            // 
            // Dolg_Személyi
            // 
            this.Dolg_Személyi.Location = new System.Drawing.Point(206, 101);
            this.Dolg_Személyi.Name = "Dolg_Személyi";
            this.Dolg_Személyi.Size = new System.Drawing.Size(218, 26);
            this.Dolg_Személyi.TabIndex = 114;
            // 
            // Dolg_Dolgozónév
            // 
            this.Dolg_Dolgozónév.Location = new System.Drawing.Point(206, 69);
            this.Dolg_Dolgozónév.Name = "Dolg_Dolgozónév";
            this.Dolg_Dolgozónév.Size = new System.Drawing.Size(694, 26);
            this.Dolg_Dolgozónév.TabIndex = 112;
            // 
            // Label22
            // 
            this.Label22.AutoSize = true;
            this.Label22.Location = new System.Drawing.Point(7, 75);
            this.Label22.Name = "Label22";
            this.Label22.Size = new System.Drawing.Size(110, 20);
            this.Label22.TabIndex = 117;
            this.Label22.Text = "Dolgozó neve:";
            // 
            // Label24
            // 
            this.Label24.AutoSize = true;
            this.Label24.Location = new System.Drawing.Point(7, 107);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(195, 20);
            this.Label24.TabIndex = 115;
            this.Label24.Text = "Személyi igazolvány szám:";
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.Location = new System.Drawing.Point(7, 43);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(111, 20);
            this.Label17.TabIndex = 111;
            this.Label17.Text = "Munka leírása:";
            // 
            // Dolg_munka
            // 
            this.Dolg_munka.AutoSize = true;
            this.Dolg_munka.Location = new System.Drawing.Point(202, 43);
            this.Dolg_munka.Name = "Dolg_munka";
            this.Dolg_munka.Size = new System.Drawing.Size(66, 20);
            this.Dolg_munka.TabIndex = 110;
            this.Dolg_munka.Text = "Label18";
            // 
            // Dolg_cégid
            // 
            this.Dolg_cégid.AutoSize = true;
            this.Dolg_cégid.Location = new System.Drawing.Point(977, 13);
            this.Dolg_cégid.Name = "Dolg_cégid";
            this.Dolg_cégid.Size = new System.Drawing.Size(50, 20);
            this.Dolg_cégid.TabIndex = 109;
            this.Dolg_cégid.Text = "Cégid";
            this.Dolg_cégid.Visible = false;
            // 
            // Dolg_cégneve
            // 
            this.Dolg_cégneve.AutoSize = true;
            this.Dolg_cégneve.Location = new System.Drawing.Point(202, 13);
            this.Dolg_cégneve.Name = "Dolg_cégneve";
            this.Dolg_cégneve.Size = new System.Drawing.Size(66, 20);
            this.Dolg_cégneve.TabIndex = 108;
            this.Dolg_cégneve.Text = "Label20";
            // 
            // Label21
            // 
            this.Label21.AutoSize = true;
            this.Label21.Location = new System.Drawing.Point(7, 14);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(80, 20);
            this.Label21.TabIndex = 107;
            this.Label21.Text = "Cég neve:";
            // 
            // Dolg_tábla
            // 
            this.Dolg_tábla.AllowUserToAddRows = false;
            this.Dolg_tábla.AllowUserToDeleteRows = false;
            this.Dolg_tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Dolg_tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dolg_tábla.EnableHeadersVisualStyles = false;
            this.Dolg_tábla.Location = new System.Drawing.Point(6, 167);
            this.Dolg_tábla.Name = "Dolg_tábla";
            this.Dolg_tábla.Size = new System.Drawing.Size(1022, 270);
            this.Dolg_tábla.TabIndex = 106;
            this.Dolg_tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dolg_tábla_CellClick);
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.TabPage3.Controls.Add(this.Autó_töröl);
            this.TabPage3.Controls.Add(this.Autó_beolvas);
            this.TabPage3.Controls.Add(this.Autó_beviteli);
            this.TabPage3.Controls.Add(this.Label6);
            this.TabPage3.Controls.Add(this.Autó_munka);
            this.TabPage3.Controls.Add(this.Autó_Cégid);
            this.TabPage3.Controls.Add(this.Autó_cégnév);
            this.TabPage3.Controls.Add(this.Autó_FRSZ);
            this.TabPage3.Controls.Add(this.Autó_státus);
            this.TabPage3.Controls.Add(this.Tábla_autó);
            this.TabPage3.Controls.Add(this.Label3);
            this.TabPage3.Controls.Add(this.Label2);
            this.TabPage3.Controls.Add(this.Label1);
            this.TabPage3.Controls.Add(this.Autó_Frissít);
            this.TabPage3.Controls.Add(this.Autó_Új);
            this.TabPage3.Controls.Add(this.Autó_ok);
            this.TabPage3.Location = new System.Drawing.Point(4, 29);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(1033, 443);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Autók";
            // 
            // Autó_töröl
            // 
            this.Autó_töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Autó_töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Autó_töröl.Location = new System.Drawing.Point(597, 76);
            this.Autó_töröl.Name = "Autó_töröl";
            this.Autó_töröl.Size = new System.Drawing.Size(45, 45);
            this.Autó_töröl.TabIndex = 99;
            this.ToolTip1.SetToolTip(this.Autó_töröl, "Törlés");
            this.Autó_töröl.UseVisualStyleBackColor = true;
            this.Autó_töröl.Click += new System.EventHandler(this.Autó_töröl_Click);
            // 
            // Autó_beolvas
            // 
            this.Autó_beolvas.BackColor = System.Drawing.Color.DarkTurquoise;
            this.Autó_beolvas.BackgroundImage = global::Villamos.Properties.Resources.Custom_Icon_Design_Flatastic_1_Import;
            this.Autó_beolvas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Autó_beolvas.Location = new System.Drawing.Point(699, 76);
            this.Autó_beolvas.Name = "Autó_beolvas";
            this.Autó_beolvas.Size = new System.Drawing.Size(45, 45);
            this.Autó_beolvas.TabIndex = 98;
            this.ToolTip1.SetToolTip(this.Autó_beolvas, "Adatok feltöltése");
            this.Autó_beolvas.UseVisualStyleBackColor = false;
            this.Autó_beolvas.Click += new System.EventHandler(this.Autó_beolvas_Click);
            // 
            // Autó_beviteli
            // 
            this.Autó_beviteli.BackColor = System.Drawing.Color.DarkTurquoise;
            this.Autó_beviteli.BackgroundImage = global::Villamos.Properties.Resources.Custom_Icon_Design_Flatastic_1_Export;
            this.Autó_beviteli.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Autó_beviteli.Location = new System.Drawing.Point(648, 76);
            this.Autó_beviteli.Name = "Autó_beviteli";
            this.Autó_beviteli.Size = new System.Drawing.Size(45, 45);
            this.Autó_beviteli.TabIndex = 97;
            this.ToolTip1.SetToolTip(this.Autó_beviteli, "Feltöltő tábla készítés");
            this.Autó_beviteli.UseVisualStyleBackColor = false;
            this.Autó_beviteli.Click += new System.EventHandler(this.Autó_beviteli_Click);
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(8, 45);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(111, 20);
            this.Label6.TabIndex = 96;
            this.Label6.Text = "Munka leírása:";
            // 
            // Autó_munka
            // 
            this.Autó_munka.AutoSize = true;
            this.Autó_munka.Location = new System.Drawing.Point(164, 45);
            this.Autó_munka.Name = "Autó_munka";
            this.Autó_munka.Size = new System.Drawing.Size(100, 20);
            this.Autó_munka.TabIndex = 95;
            this.Autó_munka.Text = "Autó_munka";
            // 
            // Autó_Cégid
            // 
            this.Autó_Cégid.AutoSize = true;
            this.Autó_Cégid.Location = new System.Drawing.Point(612, 15);
            this.Autó_Cégid.Name = "Autó_Cégid";
            this.Autó_Cégid.Size = new System.Drawing.Size(50, 20);
            this.Autó_Cégid.TabIndex = 94;
            this.Autó_Cégid.Text = "Cégid";
            this.Autó_Cégid.Visible = false;
            // 
            // Autó_cégnév
            // 
            this.Autó_cégnév.AutoSize = true;
            this.Autó_cégnév.Location = new System.Drawing.Point(164, 15);
            this.Autó_cégnév.Name = "Autó_cégnév";
            this.Autó_cégnév.Size = new System.Drawing.Size(103, 20);
            this.Autó_cégnév.TabIndex = 93;
            this.Autó_cégnév.Text = "Autó_cégnév";
            // 
            // Autó_FRSZ
            // 
            this.Autó_FRSZ.Location = new System.Drawing.Point(164, 76);
            this.Autó_FRSZ.MaxLength = 20;
            this.Autó_FRSZ.Name = "Autó_FRSZ";
            this.Autó_FRSZ.Size = new System.Drawing.Size(158, 26);
            this.Autó_FRSZ.TabIndex = 89;
            // 
            // Autó_státus
            // 
            this.Autó_státus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Autó_státus.FormattingEnabled = true;
            this.Autó_státus.Location = new System.Drawing.Point(164, 108);
            this.Autó_státus.Name = "Autó_státus";
            this.Autó_státus.Size = new System.Drawing.Size(159, 28);
            this.Autó_státus.TabIndex = 88;
            // 
            // Tábla_autó
            // 
            this.Tábla_autó.AllowUserToAddRows = false;
            this.Tábla_autó.AllowUserToDeleteRows = false;
            this.Tábla_autó.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla_autó.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.Tábla_autó.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla_autó.EnableHeadersVisualStyles = false;
            this.Tábla_autó.Location = new System.Drawing.Point(5, 142);
            this.Tábla_autó.Name = "Tábla_autó";
            this.Tábla_autó.Size = new System.Drawing.Size(1022, 298);
            this.Tábla_autó.TabIndex = 87;
            this.Tábla_autó.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_autó_CellClick);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(8, 114);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(60, 20);
            this.Label3.TabIndex = 86;
            this.Label3.Text = "Státus:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(8, 82);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(148, 20);
            this.Label2.TabIndex = 85;
            this.Label2.Text = "Forgalmi rendszám:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(8, 16);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(80, 20);
            this.Label1.TabIndex = 84;
            this.Label1.Text = "Cég neve:";
            // 
            // Autó_Frissít
            // 
            this.Autó_Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Autó_Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Autó_Frissít.Location = new System.Drawing.Point(500, 76);
            this.Autó_Frissít.Name = "Autó_Frissít";
            this.Autó_Frissít.Size = new System.Drawing.Size(45, 45);
            this.Autó_Frissít.TabIndex = 83;
            this.ToolTip1.SetToolTip(this.Autó_Frissít, "Frissíti az adatokat");
            this.Autó_Frissít.UseVisualStyleBackColor = true;
            this.Autó_Frissít.Click += new System.EventHandler(this.Autó_Frissít_Click);
            // 
            // Autó_Új
            // 
            this.Autó_Új.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Autó_Új.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Autó_Új.Location = new System.Drawing.Point(449, 76);
            this.Autó_Új.Name = "Autó_Új";
            this.Autó_Új.Size = new System.Drawing.Size(45, 45);
            this.Autó_Új.TabIndex = 62;
            this.ToolTip1.SetToolTip(this.Autó_Új, "Új adat");
            this.Autó_Új.UseVisualStyleBackColor = true;
            this.Autó_Új.Click += new System.EventHandler(this.Autó_Új_Click);
            // 
            // Autó_ok
            // 
            this.Autó_ok.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Autó_ok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Autó_ok.Location = new System.Drawing.Point(353, 76);
            this.Autó_ok.Name = "Autó_ok";
            this.Autó_ok.Size = new System.Drawing.Size(45, 45);
            this.Autó_ok.TabIndex = 61;
            this.ToolTip1.SetToolTip(this.Autó_ok, "Rögzít/Módosít");
            this.Autó_ok.UseVisualStyleBackColor = true;
            this.Autó_ok.Click += new System.EventHandler(this.Autó_ok_Click);
            // 
            // TabPage6
            // 
            this.TabPage6.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.TabPage6.Controls.Add(this.Telephely_Cégid);
            this.TabPage6.Controls.Add(this.Label25);
            this.TabPage6.Controls.Add(this.Telephely_Munka);
            this.TabPage6.Controls.Add(this.Telephely_Cégnév);
            this.TabPage6.Controls.Add(this.Label28);
            this.TabPage6.Controls.Add(this.Telephely_Tábla);
            this.TabPage6.Controls.Add(this.Button2);
            this.TabPage6.Controls.Add(this.Telephely_rögzít);
            this.TabPage6.Controls.Add(this.Btnkilelöltörlés);
            this.TabPage6.Controls.Add(this.BtnKijelölcsop);
            this.TabPage6.Controls.Add(this.Btn3szak);
            this.TabPage6.Controls.Add(this.Btn2szak);
            this.TabPage6.Controls.Add(this.Btn1szak);
            this.TabPage6.Location = new System.Drawing.Point(4, 29);
            this.TabPage6.Name = "TabPage6";
            this.TabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage6.Size = new System.Drawing.Size(1033, 443);
            this.TabPage6.TabIndex = 5;
            this.TabPage6.Text = "Telephely";
            // 
            // Telephely_Cégid
            // 
            this.Telephely_Cégid.AutoSize = true;
            this.Telephely_Cégid.Location = new System.Drawing.Point(772, 13);
            this.Telephely_Cégid.Name = "Telephely_Cégid";
            this.Telephely_Cégid.Size = new System.Drawing.Size(50, 20);
            this.Telephely_Cégid.TabIndex = 117;
            this.Telephely_Cégid.Text = "Cégid";
            this.Telephely_Cégid.Visible = false;
            // 
            // Label25
            // 
            this.Label25.AutoSize = true;
            this.Label25.Location = new System.Drawing.Point(6, 42);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(111, 20);
            this.Label25.TabIndex = 115;
            this.Label25.Text = "Munka leírása:";
            // 
            // Telephely_Munka
            // 
            this.Telephely_Munka.AutoSize = true;
            this.Telephely_Munka.Location = new System.Drawing.Point(165, 43);
            this.Telephely_Munka.Name = "Telephely_Munka";
            this.Telephely_Munka.Size = new System.Drawing.Size(133, 20);
            this.Telephely_Munka.TabIndex = 114;
            this.Telephely_Munka.Text = "Telephely_Munka";
            // 
            // Telephely_Cégnév
            // 
            this.Telephely_Cégnév.AutoSize = true;
            this.Telephely_Cégnév.Location = new System.Drawing.Point(165, 13);
            this.Telephely_Cégnév.Name = "Telephely_Cégnév";
            this.Telephely_Cégnév.Size = new System.Drawing.Size(139, 20);
            this.Telephely_Cégnév.TabIndex = 113;
            this.Telephely_Cégnév.Text = "Telephely_Cégnév";
            // 
            // Label28
            // 
            this.Label28.AutoSize = true;
            this.Label28.Location = new System.Drawing.Point(6, 13);
            this.Label28.Name = "Label28";
            this.Label28.Size = new System.Drawing.Size(80, 20);
            this.Label28.TabIndex = 112;
            this.Label28.Text = "Cég neve:";
            // 
            // Telephely_Tábla
            // 
            this.Telephely_Tábla.AllowUserToAddRows = false;
            this.Telephely_Tábla.AllowUserToDeleteRows = false;
            this.Telephely_Tábla.AllowUserToOrderColumns = true;
            this.Telephely_Tábla.AllowUserToResizeColumns = false;
            this.Telephely_Tábla.AllowUserToResizeRows = false;
            this.Telephely_Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LimeGreen;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Telephely_Tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Telephely_Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Telephely_Tábla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Telephely,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.Telephely_Tábla.EnableHeadersVisualStyles = false;
            this.Telephely_Tábla.Location = new System.Drawing.Point(9, 130);
            this.Telephely_Tábla.Name = "Telephely_Tábla";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGreen;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Telephely_Tábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Telephely_Tábla.RowHeadersVisible = false;
            this.Telephely_Tábla.RowHeadersWidth = 51;
            this.Telephely_Tábla.Size = new System.Drawing.Size(1018, 307);
            this.Telephely_Tábla.TabIndex = 97;
            // 
            // Telephely
            // 
            this.Telephely.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Telephely.HeaderText = "";
            this.Telephely.MinimumWidth = 6;
            this.Telephely.Name = "Telephely";
            this.Telephely.ToolTipText = "Engedélyezés";
            this.Telephely.Width = 6;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Telephely";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 150;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Név";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 200;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Beosztás";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 125;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "E-mail cím";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 200;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Telefonszám";
            this.Column5.Name = "Column5";
            this.Column5.Width = 200;
            // 
            // Button2
            // 
            this.Button2.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button2.Location = new System.Drawing.Point(263, 79);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(45, 45);
            this.Button2.TabIndex = 116;
            this.ToolTip1.SetToolTip(this.Button2, "Frissiti a táblázat adatit");
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Telephely_rögzít
            // 
            this.Telephely_rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Telephely_rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Telephely_rögzít.Location = new System.Drawing.Point(441, 79);
            this.Telephely_rögzít.Name = "Telephely_rögzít";
            this.Telephely_rögzít.Size = new System.Drawing.Size(45, 45);
            this.Telephely_rögzít.TabIndex = 109;
            this.ToolTip1.SetToolTip(this.Telephely_rögzít, "Adatok rögzítése");
            this.Telephely_rögzít.UseVisualStyleBackColor = true;
            this.Telephely_rögzít.Click += new System.EventHandler(this.Telephely_rögzít_Click);
            // 
            // Btnkilelöltörlés
            // 
            this.Btnkilelöltörlés.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.Btnkilelöltörlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btnkilelöltörlés.Location = new System.Drawing.Point(212, 79);
            this.Btnkilelöltörlés.Name = "Btnkilelöltörlés";
            this.Btnkilelöltörlés.Size = new System.Drawing.Size(45, 45);
            this.Btnkilelöltörlés.TabIndex = 108;
            this.ToolTip1.SetToolTip(this.Btnkilelöltörlés, "Minden kijelöléstávolít");
            this.Btnkilelöltörlés.UseVisualStyleBackColor = true;
            this.Btnkilelöltörlés.Click += new System.EventHandler(this.Btnkilelöltörlés_Click);
            // 
            // BtnKijelölcsop
            // 
            this.BtnKijelölcsop.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.BtnKijelölcsop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnKijelölcsop.Location = new System.Drawing.Point(162, 79);
            this.BtnKijelölcsop.Name = "BtnKijelölcsop";
            this.BtnKijelölcsop.Size = new System.Drawing.Size(45, 45);
            this.BtnKijelölcsop.TabIndex = 107;
            this.ToolTip1.SetToolTip(this.BtnKijelölcsop, "Minden adatot kijelöl");
            this.BtnKijelölcsop.UseVisualStyleBackColor = true;
            this.BtnKijelölcsop.Click += new System.EventHandler(this.BtnKijelölcsop_Click);
            // 
            // Btn3szak
            // 
            this.Btn3szak.BackgroundImage = global::Villamos.Properties.Resources._3B;
            this.Btn3szak.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn3szak.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Btn3szak.Location = new System.Drawing.Point(112, 79);
            this.Btn3szak.Name = "Btn3szak";
            this.Btn3szak.Size = new System.Drawing.Size(45, 45);
            this.Btn3szak.TabIndex = 106;
            this.ToolTip1.SetToolTip(this.Btn3szak, "Szakszolgálatnak megfelelő kijelölés");
            this.Btn3szak.UseVisualStyleBackColor = true;
            this.Btn3szak.Click += new System.EventHandler(this.Btn3szak_Click);
            // 
            // Btn2szak
            // 
            this.Btn2szak.BackgroundImage = global::Villamos.Properties.Resources._2B;
            this.Btn2szak.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn2szak.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Btn2szak.Location = new System.Drawing.Point(62, 79);
            this.Btn2szak.Name = "Btn2szak";
            this.Btn2szak.Size = new System.Drawing.Size(45, 45);
            this.Btn2szak.TabIndex = 105;
            this.ToolTip1.SetToolTip(this.Btn2szak, "Szakszolgálatnak megfelelő kijelölés");
            this.Btn2szak.UseVisualStyleBackColor = true;
            this.Btn2szak.Click += new System.EventHandler(this.Btn2szak_Click);
            // 
            // Btn1szak
            // 
            this.Btn1szak.BackgroundImage = global::Villamos.Properties.Resources._1B;
            this.Btn1szak.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn1szak.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Btn1szak.Location = new System.Drawing.Point(10, 79);
            this.Btn1szak.Name = "Btn1szak";
            this.Btn1szak.Size = new System.Drawing.Size(45, 45);
            this.Btn1szak.TabIndex = 104;
            this.ToolTip1.SetToolTip(this.Btn1szak, "Szakszolgálatnak megfelelő kijelölés");
            this.Btn1szak.UseVisualStyleBackColor = true;
            this.Btn1szak.Click += new System.EventHandler(this.Btn1szak_Click);
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.SteelBlue;
            this.TabPage4.Controls.Add(this.Vezér);
            this.TabPage4.Controls.Add(this.Engedély_sorszámok);
            this.TabPage4.Controls.Add(this.Engedély_tábla);
            this.TabPage4.Controls.Add(this.Engedély_teljes_lista);
            this.TabPage4.Controls.Add(this.Engedély_frissít);
            this.TabPage4.Controls.Add(this.Engedély_visszavonás);
            this.TabPage4.Controls.Add(this.Engedély_elutasítás);
            this.TabPage4.Controls.Add(this.BtnSzakszeng);
            this.TabPage4.Location = new System.Drawing.Point(4, 29);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Size = new System.Drawing.Size(1033, 443);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Engedélyezés";
            // 
            // Vezér
            // 
            this.Vezér.AutoSize = true;
            this.Vezér.Location = new System.Drawing.Point(882, 11);
            this.Vezér.Name = "Vezér";
            this.Vezér.Size = new System.Drawing.Size(109, 24);
            this.Vezér.TabIndex = 129;
            this.Vezér.Text = "CheckBox1";
            this.Vezér.UseVisualStyleBackColor = true;
            this.Vezér.Visible = false;
            // 
            // Engedély_sorszámok
            // 
            this.Engedély_sorszámok.Location = new System.Drawing.Point(759, 11);
            this.Engedély_sorszámok.Name = "Engedély_sorszámok";
            this.Engedély_sorszámok.Size = new System.Drawing.Size(100, 26);
            this.Engedély_sorszámok.TabIndex = 128;
            this.Engedély_sorszámok.Visible = false;
            // 
            // Engedély_tábla
            // 
            this.Engedély_tábla.AllowUserToAddRows = false;
            this.Engedély_tábla.AllowUserToDeleteRows = false;
            this.Engedély_tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Engedély_tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Engedély_tábla.Location = new System.Drawing.Point(5, 68);
            this.Engedély_tábla.Name = "Engedély_tábla";
            this.Engedély_tábla.Size = new System.Drawing.Size(1022, 372);
            this.Engedély_tábla.TabIndex = 124;
            this.Engedély_tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Engedély_tábla_CellClick);
            // 
            // Engedély_teljes_lista
            // 
            this.Engedély_teljes_lista.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Engedély_teljes_lista.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Engedély_teljes_lista.Location = new System.Drawing.Point(523, 17);
            this.Engedély_teljes_lista.Name = "Engedély_teljes_lista";
            this.Engedély_teljes_lista.Size = new System.Drawing.Size(45, 45);
            this.Engedély_teljes_lista.TabIndex = 127;
            this.ToolTip1.SetToolTip(this.Engedély_teljes_lista, "Érvényes engedéllyel rendelkezők listázása");
            this.Engedély_teljes_lista.UseVisualStyleBackColor = true;
            this.Engedély_teljes_lista.Click += new System.EventHandler(this.Engedély_teljes_lista_Click);
            // 
            // Engedély_frissít
            // 
            this.Engedély_frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Engedély_frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Engedély_frissít.Location = new System.Drawing.Point(107, 17);
            this.Engedély_frissít.Name = "Engedély_frissít";
            this.Engedély_frissít.Size = new System.Drawing.Size(45, 45);
            this.Engedély_frissít.TabIndex = 126;
            this.ToolTip1.SetToolTip(this.Engedély_frissít, "Engedélyezésre váró lista frissítése");
            this.Engedély_frissít.UseVisualStyleBackColor = true;
            this.Engedély_frissít.Click += new System.EventHandler(this.Engedély_frissít_Click);
            // 
            // Engedély_visszavonás
            // 
            this.Engedély_visszavonás.BackgroundImage = global::Villamos.Properties.Resources.Iconarchive_Red_Orb_Alphabet_Exclamation_mark;
            this.Engedély_visszavonás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Engedély_visszavonás.Location = new System.Drawing.Point(574, 17);
            this.Engedély_visszavonás.Name = "Engedély_visszavonás";
            this.Engedély_visszavonás.Size = new System.Drawing.Size(45, 45);
            this.Engedély_visszavonás.TabIndex = 125;
            this.ToolTip1.SetToolTip(this.Engedély_visszavonás, "Engedély visszavonás");
            this.Engedély_visszavonás.UseVisualStyleBackColor = true;
            this.Engedély_visszavonás.Click += new System.EventHandler(this.Engedély_visszavonás_Click);
            // 
            // Engedély_elutasítás
            // 
            this.Engedély_elutasítás.BackgroundImage = global::Villamos.Properties.Resources.Iconarchive_Red_Orb_Alphabet_Exclamation_mark;
            this.Engedély_elutasítás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Engedély_elutasítás.Location = new System.Drawing.Point(56, 17);
            this.Engedély_elutasítás.Name = "Engedély_elutasítás";
            this.Engedély_elutasítás.Size = new System.Drawing.Size(45, 45);
            this.Engedély_elutasítás.TabIndex = 123;
            this.ToolTip1.SetToolTip(this.Engedély_elutasítás, "Elutasítás");
            this.Engedély_elutasítás.UseVisualStyleBackColor = true;
            this.Engedély_elutasítás.Click += new System.EventHandler(this.Engedély_elutasítás_Click);
            // 
            // BtnSzakszeng
            // 
            this.BtnSzakszeng.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.BtnSzakszeng.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSzakszeng.Location = new System.Drawing.Point(5, 17);
            this.BtnSzakszeng.Name = "BtnSzakszeng";
            this.BtnSzakszeng.Size = new System.Drawing.Size(45, 45);
            this.BtnSzakszeng.TabIndex = 122;
            this.ToolTip1.SetToolTip(this.BtnSzakszeng, "Engedélyezés");
            this.BtnSzakszeng.UseVisualStyleBackColor = true;
            this.BtnSzakszeng.Click += new System.EventHandler(this.BtnSzakszeng_Click);
            // 
            // TabPage5
            // 
            this.TabPage5.BackColor = System.Drawing.Color.Gold;
            this.TabPage5.Controls.Add(this.Lekérd_dolgozó_lista);
            this.TabPage5.Controls.Add(this.Lekérd_autó_Lista);
            this.TabPage5.Controls.Add(this.Lekérd_Excel);
            this.TabPage5.Controls.Add(this.Lekérd_autó);
            this.TabPage5.Controls.Add(this.Lekérd_dolgozó);
            this.TabPage5.Controls.Add(this.Lekérdezés_tábla);
            this.TabPage5.Location = new System.Drawing.Point(4, 29);
            this.TabPage5.Name = "TabPage5";
            this.TabPage5.Size = new System.Drawing.Size(1033, 443);
            this.TabPage5.TabIndex = 4;
            this.TabPage5.Text = "Lekérdezés";
            // 
            // Lekérd_dolgozó_lista
            // 
            this.Lekérd_dolgozó_lista.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Lekérd_dolgozó_lista.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lekérd_dolgozó_lista.Location = new System.Drawing.Point(168, 12);
            this.Lekérd_dolgozó_lista.Name = "Lekérd_dolgozó_lista";
            this.Lekérd_dolgozó_lista.Size = new System.Drawing.Size(45, 45);
            this.Lekérd_dolgozó_lista.TabIndex = 131;
            this.ToolTip1.SetToolTip(this.Lekérd_dolgozó_lista, "Személyek listázása");
            this.Lekérd_dolgozó_lista.UseVisualStyleBackColor = true;
            this.Lekérd_dolgozó_lista.Click += new System.EventHandler(this.Lekérd_dolgozó_lista_Click);
            // 
            // Lekérd_autó_Lista
            // 
            this.Lekérd_autó_Lista.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Lekérd_autó_Lista.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lekérd_autó_Lista.Location = new System.Drawing.Point(6, 12);
            this.Lekérd_autó_Lista.Name = "Lekérd_autó_Lista";
            this.Lekérd_autó_Lista.Size = new System.Drawing.Size(45, 45);
            this.Lekérd_autó_Lista.TabIndex = 130;
            this.ToolTip1.SetToolTip(this.Lekérd_autó_Lista, "Autók listázása");
            this.Lekérd_autó_Lista.UseVisualStyleBackColor = true;
            this.Lekérd_autó_Lista.Click += new System.EventHandler(this.Lekérd_autó_Lista_Click);
            // 
            // Lekérd_Excel
            // 
            this.Lekérd_Excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Lekérd_Excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lekérd_Excel.Location = new System.Drawing.Point(560, 12);
            this.Lekérd_Excel.Name = "Lekérd_Excel";
            this.Lekérd_Excel.Size = new System.Drawing.Size(45, 45);
            this.Lekérd_Excel.TabIndex = 129;
            this.ToolTip1.SetToolTip(this.Lekérd_Excel, "Táblázat adatait excelbe menti");
            this.Lekérd_Excel.UseVisualStyleBackColor = true;
            this.Lekérd_Excel.Click += new System.EventHandler(this.Lekérd_Excel_Click);
            // 
            // Lekérd_autó
            // 
            this.Lekérd_autó.BackgroundImage = global::Villamos.Properties.Resources.CAR5;
            this.Lekérd_autó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lekérd_autó.Location = new System.Drawing.Point(57, 12);
            this.Lekérd_autó.Name = "Lekérd_autó";
            this.Lekérd_autó.Size = new System.Drawing.Size(45, 45);
            this.Lekérd_autó.TabIndex = 128;
            this.ToolTip1.SetToolTip(this.Lekérd_autó, "Excel táblázat az autók listájáról");
            this.Lekérd_autó.UseVisualStyleBackColor = true;
            this.Lekérd_autó.Click += new System.EventHandler(this.Lekérd_autó_Click);
            // 
            // Lekérd_dolgozó
            // 
            this.Lekérd_dolgozó.BackgroundImage = global::Villamos.Properties.Resources.felhasználók32;
            this.Lekérd_dolgozó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lekérd_dolgozó.Location = new System.Drawing.Point(219, 12);
            this.Lekérd_dolgozó.Name = "Lekérd_dolgozó";
            this.Lekérd_dolgozó.Size = new System.Drawing.Size(45, 45);
            this.Lekérd_dolgozó.TabIndex = 127;
            this.ToolTip1.SetToolTip(this.Lekérd_dolgozó, "Excel táblázat a dolgozók listájáról");
            this.Lekérd_dolgozó.UseVisualStyleBackColor = true;
            this.Lekérd_dolgozó.Click += new System.EventHandler(this.Lekérd_dolgozó_Click);
            // 
            // Lekérdezés_tábla
            // 
            this.Lekérdezés_tábla.AllowUserToAddRows = false;
            this.Lekérdezés_tábla.AllowUserToDeleteRows = false;
            this.Lekérdezés_tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Lekérdezés_tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Lekérdezés_tábla.Location = new System.Drawing.Point(5, 63);
            this.Lekérdezés_tábla.Name = "Lekérdezés_tábla";
            this.Lekérdezés_tábla.RowHeadersVisible = false;
            this.Lekérdezés_tábla.Size = new System.Drawing.Size(1022, 377);
            this.Lekérdezés_tábla.TabIndex = 107;
            // 
            // TabPage7
            // 
            this.TabPage7.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.TabPage7.Controls.Add(this.WebBrowser1);
            this.TabPage7.Controls.Add(this.Email_Aláírás);
            this.TabPage7.Controls.Add(this.Label27);
            this.TabPage7.Controls.Add(this.Email_másolat);
            this.TabPage7.Controls.Add(this.Label26);
            this.TabPage7.Controls.Add(this.Email_frissít);
            this.TabPage7.Controls.Add(this.Email_rögzít);
            this.TabPage7.Location = new System.Drawing.Point(4, 29);
            this.TabPage7.Name = "TabPage7";
            this.TabPage7.Size = new System.Drawing.Size(1033, 443);
            this.TabPage7.TabIndex = 6;
            this.TabPage7.Text = "Email adatok";
            // 
            // WebBrowser1
            // 
            this.WebBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WebBrowser1.Location = new System.Drawing.Point(128, 121);
            this.WebBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.WebBrowser1.Name = "WebBrowser1";
            this.WebBrowser1.Size = new System.Drawing.Size(838, 160);
            this.WebBrowser1.TabIndex = 113;
            // 
            // Email_Aláírás
            // 
            this.Email_Aláírás.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Email_Aláírás.Location = new System.Drawing.Point(128, 287);
            this.Email_Aláírás.Multiline = true;
            this.Email_Aláírás.Name = "Email_Aláírás";
            this.Email_Aláírás.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Email_Aláírás.Size = new System.Drawing.Size(838, 153);
            this.Email_Aláírás.TabIndex = 111;
            // 
            // Label27
            // 
            this.Label27.AutoSize = true;
            this.Label27.Location = new System.Drawing.Point(9, 121);
            this.Label27.Name = "Label27";
            this.Label27.Size = new System.Drawing.Size(57, 20);
            this.Label27.TabIndex = 110;
            this.Label27.Text = "Aláírás";
            // 
            // Email_másolat
            // 
            this.Email_másolat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Email_másolat.Location = new System.Drawing.Point(128, 17);
            this.Email_másolat.Multiline = true;
            this.Email_másolat.Name = "Email_másolat";
            this.Email_másolat.Size = new System.Drawing.Size(838, 95);
            this.Email_másolat.TabIndex = 109;
            // 
            // Label26
            // 
            this.Label26.AutoSize = true;
            this.Label26.Location = new System.Drawing.Point(9, 17);
            this.Label26.Name = "Label26";
            this.Label26.Size = new System.Drawing.Size(113, 20);
            this.Label26.TabIndex = 108;
            this.Label26.Text = "Másolatot kap:";
            // 
            // Email_frissít
            // 
            this.Email_frissít.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Email_frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Email_frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Email_frissít.Location = new System.Drawing.Point(972, 121);
            this.Email_frissít.Name = "Email_frissít";
            this.Email_frissít.Size = new System.Drawing.Size(45, 45);
            this.Email_frissít.TabIndex = 14;
            this.ToolTip1.SetToolTip(this.Email_frissít, "Frissíti az adatokat");
            this.Email_frissít.UseVisualStyleBackColor = true;
            this.Email_frissít.Click += new System.EventHandler(this.Email_frissít_Click);
            // 
            // Email_rögzít
            // 
            this.Email_rögzít.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Email_rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Email_rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Email_rögzít.Location = new System.Drawing.Point(972, 17);
            this.Email_rögzít.Name = "Email_rögzít";
            this.Email_rögzít.Size = new System.Drawing.Size(45, 45);
            this.Email_rögzít.TabIndex = 13;
            this.ToolTip1.SetToolTip(this.Email_rögzít, "Rögzít/Módosít");
            this.Email_rögzít.UseVisualStyleBackColor = true;
            this.Email_rögzít.Click += new System.EventHandler(this.Email_rögzít_Click);
            // 
            // TabPage8
            // 
            this.TabPage8.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.TabPage8.Controls.Add(this.PDF_néző);
            this.TabPage8.Controls.Add(this.Doksik);
            this.TabPage8.Controls.Add(this.Könyvtár);
            this.TabPage8.Controls.Add(this.TxtKérrelemPDF);
            this.TabPage8.Controls.Add(this.PDF_lista_frissít);
            this.TabPage8.Controls.Add(this.PDF_lista);
            this.TabPage8.Controls.Add(this.Label29);
            this.TabPage8.Controls.Add(this.PDF_munka);
            this.TabPage8.Controls.Add(this.PDF_cégid);
            this.TabPage8.Controls.Add(this.PDF_cégneve);
            this.TabPage8.Controls.Add(this.Label33);
            this.TabPage8.Controls.Add(this.PDF_törlés);
            this.TabPage8.Controls.Add(this.PDF_rögzít);
            this.TabPage8.Controls.Add(this.PDF_feltöltés);
            this.TabPage8.Location = new System.Drawing.Point(4, 29);
            this.TabPage8.Name = "TabPage8";
            this.TabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage8.Size = new System.Drawing.Size(1033, 443);
            this.TabPage8.TabIndex = 7;
            this.TabPage8.Text = "PDF";
            // 
            // PDF_néző
            // 
            this.PDF_néző.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PDF_néző.Location = new System.Drawing.Point(240, 70);
            this.PDF_néző.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.PDF_néző.Name = "PDF_néző";
            this.PDF_néző.ShowToolbar = false;
            this.PDF_néző.Size = new System.Drawing.Size(786, 367);
            this.PDF_néző.TabIndex = 241;
            this.PDF_néző.ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitWidth;
            // 
            // Doksik
            // 
            this.Doksik.Location = new System.Drawing.Point(694, 38);
            this.Doksik.Name = "Doksik";
            this.Doksik.Size = new System.Drawing.Size(103, 26);
            this.Doksik.TabIndex = 135;
            this.Doksik.Visible = false;
            // 
            // Könyvtár
            // 
            this.Könyvtár.Location = new System.Drawing.Point(814, 38);
            this.Könyvtár.Name = "Könyvtár";
            this.Könyvtár.Size = new System.Drawing.Size(103, 26);
            this.Könyvtár.TabIndex = 134;
            this.Könyvtár.Visible = false;
            // 
            // TxtKérrelemPDF
            // 
            this.TxtKérrelemPDF.Location = new System.Drawing.Point(923, 38);
            this.TxtKérrelemPDF.Name = "TxtKérrelemPDF";
            this.TxtKérrelemPDF.Size = new System.Drawing.Size(103, 26);
            this.TxtKérrelemPDF.TabIndex = 132;
            this.TxtKérrelemPDF.Visible = false;
            // 
            // PDF_lista_frissít
            // 
            this.PDF_lista_frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.PDF_lista_frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PDF_lista_frissít.Location = new System.Drawing.Point(186, 121);
            this.PDF_lista_frissít.Name = "PDF_lista_frissít";
            this.PDF_lista_frissít.Size = new System.Drawing.Size(45, 45);
            this.PDF_lista_frissít.TabIndex = 133;
            this.ToolTip1.SetToolTip(this.PDF_lista_frissít, "Frissíti az adatokat");
            this.PDF_lista_frissít.UseVisualStyleBackColor = true;
            this.PDF_lista_frissít.Click += new System.EventHandler(this.PDF_lista_frissít_Click);
            // 
            // PDF_lista
            // 
            this.PDF_lista.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PDF_lista.FormattingEnabled = true;
            this.PDF_lista.ItemHeight = 20;
            this.PDF_lista.Location = new System.Drawing.Point(4, 173);
            this.PDF_lista.Name = "PDF_lista";
            this.PDF_lista.Size = new System.Drawing.Size(227, 264);
            this.PDF_lista.TabIndex = 131;
            this.PDF_lista.SelectedIndexChanged += new System.EventHandler(this.PDF_lista_SelectedIndexChanged);
            // 
            // Label29
            // 
            this.Label29.AutoSize = true;
            this.Label29.Location = new System.Drawing.Point(6, 47);
            this.Label29.Name = "Label29";
            this.Label29.Size = new System.Drawing.Size(111, 20);
            this.Label29.TabIndex = 116;
            this.Label29.Text = "Munka leírása:";
            // 
            // PDF_munka
            // 
            this.PDF_munka.AutoSize = true;
            this.PDF_munka.Location = new System.Drawing.Point(201, 47);
            this.PDF_munka.Name = "PDF_munka";
            this.PDF_munka.Size = new System.Drawing.Size(66, 20);
            this.PDF_munka.TabIndex = 115;
            this.PDF_munka.Text = "Label18";
            // 
            // PDF_cégid
            // 
            this.PDF_cégid.AutoSize = true;
            this.PDF_cégid.Location = new System.Drawing.Point(976, 17);
            this.PDF_cégid.Name = "PDF_cégid";
            this.PDF_cégid.Size = new System.Drawing.Size(50, 20);
            this.PDF_cégid.TabIndex = 114;
            this.PDF_cégid.Text = "Cégid";
            this.PDF_cégid.Visible = false;
            // 
            // PDF_cégneve
            // 
            this.PDF_cégneve.AutoSize = true;
            this.PDF_cégneve.Location = new System.Drawing.Point(201, 17);
            this.PDF_cégneve.Name = "PDF_cégneve";
            this.PDF_cégneve.Size = new System.Drawing.Size(66, 20);
            this.PDF_cégneve.TabIndex = 113;
            this.PDF_cégneve.Text = "Label20";
            // 
            // Label33
            // 
            this.Label33.AutoSize = true;
            this.Label33.Location = new System.Drawing.Point(6, 18);
            this.Label33.Name = "Label33";
            this.Label33.Size = new System.Drawing.Size(80, 20);
            this.Label33.TabIndex = 112;
            this.Label33.Text = "Cég neve:";
            // 
            // PDF_törlés
            // 
            this.PDF_törlés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.PDF_törlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PDF_törlés.Location = new System.Drawing.Point(60, 70);
            this.PDF_törlés.Name = "PDF_törlés";
            this.PDF_törlés.Size = new System.Drawing.Size(45, 45);
            this.PDF_törlés.TabIndex = 130;
            this.ToolTip1.SetToolTip(this.PDF_törlés, "Törlés");
            this.PDF_törlés.UseVisualStyleBackColor = true;
            this.PDF_törlés.Visible = false;
            this.PDF_törlés.Click += new System.EventHandler(this.PDF_törlés_Click);
            // 
            // PDF_rögzít
            // 
            this.PDF_rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.PDF_rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PDF_rögzít.Location = new System.Drawing.Point(186, 70);
            this.PDF_rögzít.Name = "PDF_rögzít";
            this.PDF_rögzít.Size = new System.Drawing.Size(45, 45);
            this.PDF_rögzít.TabIndex = 84;
            this.ToolTip1.SetToolTip(this.PDF_rögzít, "Rögzít/Módosít");
            this.PDF_rögzít.UseVisualStyleBackColor = true;
            this.PDF_rögzít.Click += new System.EventHandler(this.PDF_rögzít_Click);
            // 
            // PDF_feltöltés
            // 
            this.PDF_feltöltés.BackgroundImage = global::Villamos.Properties.Resources.Folder_;
            this.PDF_feltöltés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PDF_feltöltés.Location = new System.Drawing.Point(6, 70);
            this.PDF_feltöltés.Name = "PDF_feltöltés";
            this.PDF_feltöltés.Size = new System.Drawing.Size(45, 45);
            this.PDF_feltöltés.TabIndex = 83;
            this.ToolTip1.SetToolTip(this.PDF_feltöltés, "PDF fájl kiválasztása");
            this.PDF_feltöltés.UseVisualStyleBackColor = true;
            this.PDF_feltöltés.Click += new System.EventHandler(this.PDF_feltöltés_Click);
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.Cmbtelephely);
            this.Panel2.Controls.Add(this.Label13);
            this.Panel2.Location = new System.Drawing.Point(4, 7);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(335, 33);
            this.Panel2.TabIndex = 170;
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
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Holtart.Location = new System.Drawing.Point(345, 7);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(654, 28);
            this.Holtart.TabIndex = 173;
            this.Holtart.Visible = false;
            // 
            // Btn_Súgó
            // 
            this.Btn_Súgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Súgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.Btn_Súgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Súgó.Location = new System.Drawing.Point(1005, 1);
            this.Btn_Súgó.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Súgó.Name = "Btn_Súgó";
            this.Btn_Súgó.Size = new System.Drawing.Size(40, 40);
            this.Btn_Súgó.TabIndex = 62;
            this.Btn_Súgó.UseVisualStyleBackColor = true;
            this.Btn_Súgó.Click += new System.EventHandler(this.Btn_Súgó_Click);
            // 
            // Ablak_külső
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.ClientSize = new System.Drawing.Size(1047, 527);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.LapFülek);
            this.Controls.Add(this.Btn_Súgó);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_külső";
            this.Text = "Külsős Munkavállalók belépése és behajtása";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_külső_Load);
            this.LapFülek.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Cég_tábla)).EndInit();
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dolg_tábla)).EndInit();
            this.TabPage3.ResumeLayout(false);
            this.TabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_autó)).EndInit();
            this.TabPage6.ResumeLayout(false);
            this.TabPage6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Telephely_Tábla)).EndInit();
            this.TabPage4.ResumeLayout(false);
            this.TabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Engedély_tábla)).EndInit();
            this.TabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Lekérdezés_tábla)).EndInit();
            this.TabPage7.ResumeLayout(false);
            this.TabPage7.PerformLayout();
            this.TabPage8.ResumeLayout(false);
            this.TabPage8.PerformLayout();
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        internal Button Btn_Súgó;
        internal TabControl LapFülek;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal TabPage TabPage3;
        internal TabPage TabPage4;
        internal TabPage TabPage5;
        internal Button Autó_Új;
        internal Button Autó_ok;
        internal Label Label3;
        internal Label Label2;
        internal Label Label1;
        internal Button Autó_Frissít;
        internal DataGridView Tábla_autó;
        internal TextBox Autó_FRSZ;
        internal ComboBox Autó_státus;
        internal Panel Panel2;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal DateTimePicker Cég_Érv_vég;
        internal DateTimePicker Cég_Érv_kezdet;
        internal TextBox Cég_felelős_telefon;
        internal TextBox Cég_felelős_személy;
        internal TextBox Cég_Munkaleírás;
        internal TextBox Cég_email;
        internal TextBox Cég_címe;
        internal TextBox Cég_cég;
        internal TextBox Cég_sorszám;
        internal Label Label16;
        internal Label Label15;
        internal Label Label14;
        internal Label Label12;
        internal Label Label11;
        internal Label Label10;
        internal Label Label9;
        internal Label Label8;
        internal Label Label7;
        internal Button Alap_Új_adat;
        internal Button Alap_Rögzít;
        internal CheckBox Cég_Aktív;
        internal ComboBox Cég_mikor;
        internal Label Label4;
        internal DataGridView Cég_tábla;
        internal Button Cég_excel;
        internal Button Alap_Frissít;
        internal ComboBox Cég_engedély_státus;
        internal Label Label5;
        internal Label Autó_munka;
        internal Label Autó_Cégid;
        internal Label Autó_cégnév;
        internal Label Label6;
        internal TextBox Dolg_Személyi;
        internal TextBox Dolg_Dolgozónév;
        internal Label Label22;
        internal Label Label24;
        internal Label Label17;
        internal Label Dolg_munka;
        internal Label Dolg_cégid;
        internal Label Dolg_cégneve;
        internal Label Label21;
        internal DataGridView Dolg_tábla;
        internal Button Dolg_frissít;
        internal Button Dolg_új;
        internal Button Dolg_Rögzít;
        internal ComboBox Dolg_Státus;
        internal Label Label20;
        internal TabPage TabPage6;
        private DataGridView Telephely_Tábla;
        internal Button Btnkilelöltörlés;
        internal Button BtnKijelölcsop;
        internal Button Btn3szak;
        internal Button Btn2szak;
        internal Button Btn1szak;
        internal Label Label25;
        internal Label Telephely_Munka;
        internal Label Telephely_Cégnév;
        internal Label Label28;
        internal Button Telephely_rögzít;
        internal Label Telephely_Cégid;
        internal Button Button2;
        internal DataGridViewCheckBoxColumn Telephely;
        internal DataGridViewTextBoxColumn Column1;
        internal DataGridViewTextBoxColumn Column2;
        internal DataGridViewTextBoxColumn Column3;
        internal DataGridViewTextBoxColumn Column4;
        internal DataGridViewTextBoxColumn Column5;
        internal Button Cégek_engedélyezésre;
        internal Button Engedély_elutasítás;
        internal Button BtnSzakszeng;
        internal DataGridView Engedély_tábla;
        internal Button Engedély_visszavonás;
        internal Button Engedély_frissít;
        internal Button Engedély_teljes_lista;
        internal TextBox Engedély_sorszámok;
        internal Button Lekérd_Excel;
        internal Button Lekérd_autó;
        internal Button Lekérd_dolgozó;
        internal DataGridView Lekérdezés_tábla;
        internal ToolTip ToolTip1;
        internal Button Autó_beolvas;
        internal Button Autó_beviteli;
        internal Button Dolgozó_beolvas;
        internal Button Dolgozó_kivitel;
        internal Button Lekérd_dolgozó_lista;
        internal Button Lekérd_autó_Lista;
        internal TabPage TabPage7;
        internal Button Email_frissít;
        internal Button Email_rögzít;
        internal TextBox Email_Aláírás;
        internal Label Label27;
        internal TextBox Email_másolat;
        internal Label Label26;
        internal Button Dolgozó_töröl;
        internal Button Autó_töröl;
        internal TabPage TabPage8;

        internal Label Label29;
        internal Label PDF_munka;
        internal Label PDF_cégid;
        internal Label PDF_cégneve;
        internal Label Label33;
        internal Button PDF_rögzít;
        internal Button PDF_feltöltés;
        internal Button PDF_törlés;
        internal ListBox PDF_lista;
        internal TextBox TxtKérrelemPDF;
        internal Button PDF_lista_frissít;
        internal TextBox Könyvtár;
        internal TextBox Doksik;
        internal WebBrowser WebBrowser1;
        internal CheckBox Vezér;
        private PdfiumViewer.PdfViewer PDF_néző;
    }
}