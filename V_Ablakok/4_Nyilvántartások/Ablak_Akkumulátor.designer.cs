using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
       public partial class Ablak_Akkumulátor : Form
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Akkumulátor));
            this.Fülek = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Textgyáriszám = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.Telephely_alap = new System.Windows.Forms.TextBox();
            this.Kapacitás_Alap = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Combogyártó = new System.Windows.Forms.ComboBox();
            this.Label16 = new System.Windows.Forms.Label();
            this.Combofajta = new System.Windows.Forms.ComboBox();
            this.Label15 = new System.Windows.Forms.Label();
            this.Combotípus = new System.Windows.Forms.ComboBox();
            this.Label14 = new System.Windows.Forms.Label();
            this.Label13 = new System.Windows.Forms.Label();
            this.Dgarancia = new System.Windows.Forms.DateTimePicker();
            this.Dgyártásiidő = new System.Windows.Forms.DateTimePicker();
            this.Label12 = new System.Windows.Forms.Label();
            this.Dbeépítésdátum = new System.Windows.Forms.DateTimePicker();
            this.Label19 = new System.Windows.Forms.Label();
            this.Textbeépítve = new System.Windows.Forms.TextBox();
            this.Label20 = new System.Windows.Forms.Label();
            this.Státus_alap = new System.Windows.Forms.TextBox();
            this.Label18 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.TextMegjegyzés = new System.Windows.Forms.TextBox();
            this.btnAkufriss = new System.Windows.Forms.Button();
            this.Btnakurögzít = new System.Windows.Forms.Button();
            this.Btnakuúj = new System.Windows.Forms.Button();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.Résztörlés = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Label22 = new System.Windows.Forms.Label();
            this.Textmérkapacitás = new System.Windows.Forms.TextBox();
            this.Check1 = new System.Windows.Forms.CheckBox();
            this.Textmérvégfesz = new System.Windows.Forms.TextBox();
            this.Textmérkezdetifesz = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Textmérkisütésiáram = new System.Windows.Forms.TextBox();
            this.Textgyárimérés = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.mérdátum = new System.Windows.Forms.DateTimePicker();
            this.Label6 = new System.Windows.Forms.Label();
            this.MérésDátuma = new System.Windows.Forms.DateTimePicker();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.TextMérmegjegyzés = new System.Windows.Forms.TextBox();
            this.btnrögzítés = new System.Windows.Forms.Button();
            this.btnmérúj = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.label26 = new System.Windows.Forms.Label();
            this.Beép_Gyári = new System.Windows.Forms.TextBox();
            this.Használt = new System.Windows.Forms.Button();
            this.Törölt = new System.Windows.Forms.Button();
            this.Kiépít = new System.Windows.Forms.Button();
            this.Leselejtezett = new System.Windows.Forms.Button();
            this.KIBE_Panel = new System.Windows.Forms.Panel();
            this.label29 = new System.Windows.Forms.Label();
            this.BePSz = new System.Windows.Forms.TextBox();
            this.Beépít = new System.Windows.Forms.Button();
            this.Pályaszám_Szűrő = new System.Windows.Forms.Panel();
            this.label28 = new System.Windows.Forms.Label();
            this.Beép_Psz = new System.Windows.Forms.ComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.Beép_Frissít = new System.Windows.Forms.Button();
            this.Tábla_Beép = new System.Windows.Forms.DataGridView();
            this.SelejtElő = new System.Windows.Forms.Button();
            this.Beép_Státus = new System.Windows.Forms.ComboBox();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.TelephelyEllenőr = new System.Windows.Forms.Button();
            this.Mérés = new System.Windows.Forms.Button();
            this.Teljesség = new System.Windows.Forms.Button();
            this.Telephely_Szűrő = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.ExcelAlapLista = new System.Windows.Forms.Button();
            this.Label21 = new System.Windows.Forms.Label();
            this.txtgyáriszám = new System.Windows.Forms.TextBox();
            this.Tábla2 = new System.Windows.Forms.DataGridView();
            this.Akku_Tábla_Listázás = new System.Windows.Forms.Button();
            this.Label9 = new System.Windows.Forms.Label();
            this.ComboStátuslek = new System.Windows.Forms.ComboBox();
            this.TextPszlek = new System.Windows.Forms.TextBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.label30 = new System.Windows.Forms.Label();
            this.MérésLekGyári = new System.Windows.Forms.TextBox();
            this.dátumig = new System.Windows.Forms.DateTimePicker();
            this.Dátumtól = new System.Windows.Forms.DateTimePicker();
            this.Tábla4 = new System.Windows.Forms.DataGridView();
            this.R_törlés = new System.Windows.Forms.Button();
            this.Button2 = new System.Windows.Forms.Button();
            this.BtnMéréslista = new System.Windows.Forms.Button();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.CmbTelephely = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.Súgó = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Holtart = new V_MindenEgyéb.MyProgressbar();
            this.Fülek.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.TabPage3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.KIBE_Panel.SuspendLayout();
            this.Pályaszám_Szűrő.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_Beép)).BeginInit();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla2)).BeginInit();
            this.TabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla4)).BeginInit();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Fülek
            // 
            this.Fülek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fülek.Controls.Add(this.TabPage1);
            this.Fülek.Controls.Add(this.TabPage3);
            this.Fülek.Controls.Add(this.tabPage5);
            this.Fülek.Controls.Add(this.TabPage2);
            this.Fülek.Controls.Add(this.TabPage4);
            this.Fülek.Location = new System.Drawing.Point(12, 55);
            this.Fülek.Name = "Fülek";
            this.Fülek.Padding = new System.Drawing.Point(16, 3);
            this.Fülek.SelectedIndex = 0;
            this.Fülek.Size = new System.Drawing.Size(1424, 521);
            this.Fülek.TabIndex = 2;
            this.Fülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Fülek_DrawItem);
            this.Fülek.SelectedIndexChanged += new System.EventHandler(this.Fülek_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.PaleGreen;
            this.TabPage1.Controls.Add(this.tableLayoutPanel1);
            this.TabPage1.Controls.Add(this.Label11);
            this.TabPage1.Controls.Add(this.TextMegjegyzés);
            this.TabPage1.Controls.Add(this.btnAkufriss);
            this.TabPage1.Controls.Add(this.Btnakurögzít);
            this.TabPage1.Controls.Add(this.Btnakuúj);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1416, 488);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Alapadatok Rögzítése";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.Textgyáriszám, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label17, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.Telephely_alap, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.Kapacitás_Alap, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label25, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.Label10, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Combogyártó, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.Label16, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Combofajta, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.Label15, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.Combotípus, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.Label14, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.Label13, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.Dgarancia, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.Dgyártásiidő, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.Label12, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.Dbeépítésdátum, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.Label19, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.Textbeépítve, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.Label20, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.Státus_alap, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.Label18, 0, 7);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 11;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(453, 390);
            this.tableLayoutPanel1.TabIndex = 90;
            // 
            // Textgyáriszám
            // 
            this.Textgyáriszám.Location = new System.Drawing.Point(229, 3);
            this.Textgyáriszám.MaxLength = 30;
            this.Textgyáriszám.Name = "Textgyáriszám";
            this.Textgyáriszám.Size = new System.Drawing.Size(221, 26);
            this.Textgyáriszám.TabIndex = 0;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(3, 350);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(80, 20);
            this.label17.TabIndex = 86;
            this.label17.Text = "Telephely:";
            // 
            // Telephely_alap
            // 
            this.Telephely_alap.BackColor = System.Drawing.Color.PaleGreen;
            this.Telephely_alap.Enabled = false;
            this.Telephely_alap.Location = new System.Drawing.Point(229, 353);
            this.Telephely_alap.Name = "Telephely_alap";
            this.Telephely_alap.Size = new System.Drawing.Size(221, 26);
            this.Telephely_alap.TabIndex = 88;
            // 
            // Kapacitás_Alap
            // 
            this.Kapacitás_Alap.Location = new System.Drawing.Point(229, 213);
            this.Kapacitás_Alap.Name = "Kapacitás_Alap";
            this.Kapacitás_Alap.Size = new System.Drawing.Size(221, 26);
            this.Kapacitás_Alap.TabIndex = 89;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(3, 210);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(83, 20);
            this.label25.TabIndex = 87;
            this.label25.Text = "Kapacitás:";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(3, 0);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(88, 20);
            this.Label10.TabIndex = 62;
            this.Label10.Text = "Gyáriszám:";
            // 
            // Combogyártó
            // 
            this.Combogyártó.FormattingEnabled = true;
            this.Combogyártó.Location = new System.Drawing.Point(229, 38);
            this.Combogyártó.MaxLength = 30;
            this.Combogyártó.Name = "Combogyártó";
            this.Combogyártó.Size = new System.Drawing.Size(221, 28);
            this.Combogyártó.TabIndex = 1;
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Location = new System.Drawing.Point(3, 35);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(61, 20);
            this.Label16.TabIndex = 74;
            this.Label16.Text = "Gyártó:";
            // 
            // Combofajta
            // 
            this.Combofajta.FormattingEnabled = true;
            this.Combofajta.Location = new System.Drawing.Point(229, 73);
            this.Combofajta.MaxLength = 10;
            this.Combofajta.Name = "Combofajta";
            this.Combofajta.Size = new System.Drawing.Size(221, 28);
            this.Combofajta.TabIndex = 2;
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Location = new System.Drawing.Point(3, 70);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(49, 20);
            this.Label15.TabIndex = 73;
            this.Label15.Text = "Fajta:";
            // 
            // Combotípus
            // 
            this.Combotípus.FormattingEnabled = true;
            this.Combotípus.Location = new System.Drawing.Point(229, 108);
            this.Combotípus.MaxLength = 30;
            this.Combotípus.Name = "Combotípus";
            this.Combotípus.Size = new System.Drawing.Size(221, 28);
            this.Combotípus.TabIndex = 3;
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(3, 105);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(51, 20);
            this.Label14.TabIndex = 72;
            this.Label14.Text = "Típus:";
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(3, 140);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(97, 20);
            this.Label13.TabIndex = 71;
            this.Label13.Text = "Gyártási idő:";
            // 
            // Dgarancia
            // 
            this.Dgarancia.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dgarancia.Location = new System.Drawing.Point(229, 178);
            this.Dgarancia.Name = "Dgarancia";
            this.Dgarancia.Size = new System.Drawing.Size(109, 26);
            this.Dgarancia.TabIndex = 5;
            // 
            // Dgyártásiidő
            // 
            this.Dgyártásiidő.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dgyártásiidő.Location = new System.Drawing.Point(229, 143);
            this.Dgyártásiidő.Name = "Dgyártásiidő";
            this.Dgyártásiidő.Size = new System.Drawing.Size(109, 26);
            this.Dgyártásiidő.TabIndex = 4;
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(3, 175);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(116, 20);
            this.Label12.TabIndex = 70;
            this.Label12.Text = "Garancia vége:";
            // 
            // Dbeépítésdátum
            // 
            this.Dbeépítésdátum.CalendarMonthBackground = System.Drawing.Color.PaleGreen;
            this.Dbeépítésdátum.Enabled = false;
            this.Dbeépítésdátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dbeépítésdátum.Location = new System.Drawing.Point(229, 318);
            this.Dbeépítésdátum.Name = "Dbeépítésdátum";
            this.Dbeépítésdátum.Size = new System.Drawing.Size(109, 26);
            this.Dbeépítésdátum.TabIndex = 85;
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.Location = new System.Drawing.Point(3, 315);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(144, 20);
            this.Label19.TabIndex = 83;
            this.Label19.Text = "Módosítás dátuma:";
            // 
            // Textbeépítve
            // 
            this.Textbeépítve.BackColor = System.Drawing.Color.PaleGreen;
            this.Textbeépítve.Enabled = false;
            this.Textbeépítve.Location = new System.Drawing.Point(229, 283);
            this.Textbeépítve.Name = "Textbeépítve";
            this.Textbeépítve.Size = new System.Drawing.Size(221, 26);
            this.Textbeépítve.TabIndex = 7;
            // 
            // Label20
            // 
            this.Label20.AutoSize = true;
            this.Label20.Location = new System.Drawing.Point(3, 280);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(130, 20);
            this.Label20.TabIndex = 84;
            this.Label20.Text = "Jármű azonosító:";
            // 
            // Státus_alap
            // 
            this.Státus_alap.BackColor = System.Drawing.Color.PaleGreen;
            this.Státus_alap.Enabled = false;
            this.Státus_alap.Location = new System.Drawing.Point(229, 248);
            this.Státus_alap.Name = "Státus_alap";
            this.Státus_alap.Size = new System.Drawing.Size(221, 26);
            this.Státus_alap.TabIndex = 89;
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.Location = new System.Drawing.Point(3, 245);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(60, 20);
            this.Label18.TabIndex = 76;
            this.Label18.Text = "Státus:";
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(8, 405);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(97, 20);
            this.Label11.TabIndex = 68;
            this.Label11.Text = "Megjegyzés:";
            // 
            // TextMegjegyzés
            // 
            this.TextMegjegyzés.Location = new System.Drawing.Point(235, 402);
            this.TextMegjegyzés.MaxLength = 250;
            this.TextMegjegyzés.Multiline = true;
            this.TextMegjegyzés.Name = "TextMegjegyzés";
            this.TextMegjegyzés.Size = new System.Drawing.Size(754, 65);
            this.TextMegjegyzés.TabIndex = 67;
            // 
            // btnAkufriss
            // 
            this.btnAkufriss.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.btnAkufriss.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAkufriss.Location = new System.Drawing.Point(466, 146);
            this.btnAkufriss.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAkufriss.Name = "btnAkufriss";
            this.btnAkufriss.Size = new System.Drawing.Size(48, 48);
            this.btnAkufriss.TabIndex = 66;
            this.toolTip1.SetToolTip(this.btnAkufriss, "Frissíti az adatokat");
            this.btnAkufriss.UseVisualStyleBackColor = true;
            this.btnAkufriss.Click += new System.EventHandler(this.BtnAkufriss_Click);
            // 
            // Btnakurögzít
            // 
            this.Btnakurögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Btnakurögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btnakurögzít.Location = new System.Drawing.Point(466, 5);
            this.Btnakurögzít.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btnakurögzít.Name = "Btnakurögzít";
            this.Btnakurögzít.Size = new System.Drawing.Size(48, 48);
            this.Btnakurögzít.TabIndex = 65;
            this.toolTip1.SetToolTip(this.Btnakurögzít, "Rögzíti/módosítja az adatokat");
            this.Btnakurögzít.UseVisualStyleBackColor = true;
            this.Btnakurögzít.Click += new System.EventHandler(this.Btnakurögzít_Click);
            // 
            // Btnakuúj
            // 
            this.Btnakuúj.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Btnakuúj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btnakuúj.Location = new System.Drawing.Point(466, 76);
            this.Btnakuúj.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btnakuúj.Name = "Btnakuúj";
            this.Btnakuúj.Size = new System.Drawing.Size(48, 48);
            this.Btnakuúj.TabIndex = 64;
            this.toolTip1.SetToolTip(this.Btnakuúj, "Új adatnak előkészíti a beviteli mezőt");
            this.Btnakuúj.UseVisualStyleBackColor = true;
            this.Btnakuúj.Click += new System.EventHandler(this.Btnakuúj_Click);
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.Turquoise;
            this.TabPage3.Controls.Add(this.Résztörlés);
            this.TabPage3.Controls.Add(this.tableLayoutPanel2);
            this.TabPage3.Controls.Add(this.Label7);
            this.TabPage3.Controls.Add(this.TextMérmegjegyzés);
            this.TabPage3.Controls.Add(this.btnrögzítés);
            this.TabPage3.Controls.Add(this.btnmérúj);
            this.TabPage3.Location = new System.Drawing.Point(4, 29);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(1416, 488);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Akkumulátor mérés rögzítésre";
            // 
            // Résztörlés
            // 
            this.Résztörlés.BackgroundImage = global::Villamos.Properties.Resources.részjelöl;
            this.Résztörlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Résztörlés.Location = new System.Drawing.Point(459, 101);
            this.Résztörlés.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Résztörlés.Name = "Résztörlés";
            this.Résztörlés.Size = new System.Drawing.Size(48, 48);
            this.Résztörlés.TabIndex = 57;
            this.toolTip1.SetToolTip(this.Résztörlés, "Új adatnak előkészíti a beviteli mezőt Nem minden");
            this.Résztörlés.UseVisualStyleBackColor = true;
            this.Résztörlés.Click += new System.EventHandler(this.Résztörlés_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.Label22, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.Textmérkapacitás, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.Check1, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.Textmérvégfesz, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.Textmérkezdetifesz, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.Label2, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.Textmérkisütésiáram, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.Textgyárimérés, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.Label4, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.Label5, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.Label3, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.mérdátum, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.Label6, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.MérésDátuma, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.Label1, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 15);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 8;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(376, 281);
            this.tableLayoutPanel2.TabIndex = 56;
            // 
            // Label22
            // 
            this.Label22.AutoSize = true;
            this.Label22.Location = new System.Drawing.Point(3, 0);
            this.Label22.Name = "Label22";
            this.Label22.Size = new System.Drawing.Size(118, 20);
            this.Label22.TabIndex = 55;
            this.Label22.Text = "Mérés Dátuma:";
            // 
            // Textmérkapacitás
            // 
            this.Textmérkapacitás.Location = new System.Drawing.Point(191, 213);
            this.Textmérkapacitás.Name = "Textmérkapacitás";
            this.Textmérkapacitás.Size = new System.Drawing.Size(181, 26);
            this.Textmérkapacitás.TabIndex = 6;
            // 
            // Check1
            // 
            this.Check1.AutoSize = true;
            this.Check1.Location = new System.Drawing.Point(191, 248);
            this.Check1.Name = "Check1";
            this.Check1.Size = new System.Drawing.Size(159, 24);
            this.Check1.TabIndex = 7;
            this.Check1.Text = "Párban mérve 24V";
            this.Check1.UseVisualStyleBackColor = true;
            // 
            // Textmérvégfesz
            // 
            this.Textmérvégfesz.Location = new System.Drawing.Point(191, 178);
            this.Textmérvégfesz.Name = "Textmérvégfesz";
            this.Textmérvégfesz.Size = new System.Drawing.Size(181, 26);
            this.Textmérvégfesz.TabIndex = 5;
            // 
            // Textmérkezdetifesz
            // 
            this.Textmérkezdetifesz.Location = new System.Drawing.Point(191, 108);
            this.Textmérkezdetifesz.Name = "Textmérkezdetifesz";
            this.Textmérkezdetifesz.Size = new System.Drawing.Size(181, 26);
            this.Textmérkezdetifesz.TabIndex = 3;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(3, 210);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(83, 20);
            this.Label2.TabIndex = 48;
            this.Label2.Text = "Kapacitás:";
            // 
            // Textmérkisütésiáram
            // 
            this.Textmérkisütésiáram.Location = new System.Drawing.Point(191, 73);
            this.Textmérkisütésiáram.Name = "Textmérkisütésiáram";
            this.Textmérkisütésiáram.Size = new System.Drawing.Size(181, 26);
            this.Textmérkisütésiáram.TabIndex = 2;
            // 
            // Textgyárimérés
            // 
            this.Textgyárimérés.Location = new System.Drawing.Point(191, 38);
            this.Textgyárimérés.MaxLength = 30;
            this.Textgyárimérés.Name = "Textgyárimérés";
            this.Textgyárimérés.Size = new System.Drawing.Size(181, 26);
            this.Textgyárimérés.TabIndex = 1;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(3, 175);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(119, 20);
            this.Label4.TabIndex = 50;
            this.Label4.Text = "Vég feszültség:";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(3, 105);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(143, 20);
            this.Label5.TabIndex = 51;
            this.Label5.Text = "Kezdeti feszültség:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(3, 140);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(93, 20);
            this.Label3.TabIndex = 49;
            this.Label3.Text = "Kisütési idő:";
            // 
            // mérdátum
            // 
            this.mérdátum.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.mérdátum.Location = new System.Drawing.Point(191, 143);
            this.mérdátum.Name = "mérdátum";
            this.mérdátum.Size = new System.Drawing.Size(109, 26);
            this.mérdátum.TabIndex = 4;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(3, 70);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(108, 20);
            this.Label6.TabIndex = 52;
            this.Label6.Text = "Kisütési áram:";
            // 
            // MérésDátuma
            // 
            this.MérésDátuma.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.MérésDátuma.Location = new System.Drawing.Point(191, 3);
            this.MérésDátuma.Name = "MérésDátuma";
            this.MérésDátuma.Size = new System.Drawing.Size(106, 26);
            this.MérésDátuma.TabIndex = 0;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(3, 35);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(88, 20);
            this.Label1.TabIndex = 1;
            this.Label1.Text = "Gyáriszám:";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(15, 312);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(97, 20);
            this.Label7.TabIndex = 53;
            this.Label7.Text = "Megjegyzés:";
            // 
            // TextMérmegjegyzés
            // 
            this.TextMérmegjegyzés.Location = new System.Drawing.Point(203, 309);
            this.TextMérmegjegyzés.MaxLength = 250;
            this.TextMérmegjegyzés.Multiline = true;
            this.TextMérmegjegyzés.Name = "TextMérmegjegyzés";
            this.TextMérmegjegyzés.Size = new System.Drawing.Size(754, 65);
            this.TextMérmegjegyzés.TabIndex = 8;
            // 
            // btnrögzítés
            // 
            this.btnrögzítés.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.btnrögzítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnrögzítés.Location = new System.Drawing.Point(403, 15);
            this.btnrögzítés.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnrögzítés.Name = "btnrögzítés";
            this.btnrögzítés.Size = new System.Drawing.Size(48, 48);
            this.btnrögzítés.TabIndex = 9;
            this.toolTip1.SetToolTip(this.btnrögzítés, "Rögzíti/módosítja az adatokat");
            this.btnrögzítés.UseVisualStyleBackColor = true;
            this.btnrögzítés.Click += new System.EventHandler(this.Btnrögzítés_Click);
            // 
            // btnmérúj
            // 
            this.btnmérúj.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.btnmérúj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnmérúj.Location = new System.Drawing.Point(403, 99);
            this.btnmérúj.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnmérúj.Name = "btnmérúj";
            this.btnmérúj.Size = new System.Drawing.Size(48, 48);
            this.btnmérúj.TabIndex = 10;
            this.toolTip1.SetToolTip(this.btnmérúj, "Új adatnak előkészíti a beviteli mezőt Mindent ");
            this.btnmérúj.UseVisualStyleBackColor = true;
            this.btnmérúj.Click += new System.EventHandler(this.Btnmérúj_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.Color.LimeGreen;
            this.tabPage5.Controls.Add(this.label26);
            this.tabPage5.Controls.Add(this.Beép_Gyári);
            this.tabPage5.Controls.Add(this.Használt);
            this.tabPage5.Controls.Add(this.Törölt);
            this.tabPage5.Controls.Add(this.Kiépít);
            this.tabPage5.Controls.Add(this.Leselejtezett);
            this.tabPage5.Controls.Add(this.KIBE_Panel);
            this.tabPage5.Controls.Add(this.Pályaszám_Szűrő);
            this.tabPage5.Controls.Add(this.label27);
            this.tabPage5.Controls.Add(this.Beép_Frissít);
            this.tabPage5.Controls.Add(this.Tábla_Beép);
            this.tabPage5.Controls.Add(this.SelejtElő);
            this.tabPage5.Controls.Add(this.Beép_Státus);
            this.tabPage5.Location = new System.Drawing.Point(4, 29);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(1416, 488);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Beépítés/Státus Módosítás";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(270, 33);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(88, 20);
            this.label26.TabIndex = 86;
            this.label26.Text = "Gyáriszám:";
            // 
            // Beép_Gyári
            // 
            this.Beép_Gyári.Location = new System.Drawing.Point(364, 27);
            this.Beép_Gyári.Name = "Beép_Gyári";
            this.Beép_Gyári.Size = new System.Drawing.Size(181, 26);
            this.Beép_Gyári.TabIndex = 85;
            // 
            // Használt
            // 
            this.Használt.BackgroundImage = global::Villamos.Properties.Resources.App_ark;
            this.Használt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Használt.Location = new System.Drawing.Point(1185, 8);
            this.Használt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Használt.Name = "Használt";
            this.Használt.Size = new System.Drawing.Size(48, 48);
            this.Használt.TabIndex = 84;
            this.toolTip1.SetToolTip(this.Használt, "Kijelölt elemek Használttá tétele");
            this.Használt.UseVisualStyleBackColor = true;
            this.Használt.Click += new System.EventHandler(this.Használt_Click);
            // 
            // Törölt
            // 
            this.Törölt.BackgroundImage = global::Villamos.Properties.Resources.bezár;
            this.Törölt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Törölt.Location = new System.Drawing.Point(1353, 8);
            this.Törölt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Törölt.Name = "Törölt";
            this.Törölt.Size = new System.Drawing.Size(48, 48);
            this.Törölt.TabIndex = 83;
            this.toolTip1.SetToolTip(this.Törölt, "Kijelölt elemeket Töröltre állítja.");
            this.Törölt.UseVisualStyleBackColor = true;
            this.Törölt.Click += new System.EventHandler(this.Törölt_Click);
            // 
            // Kiépít
            // 
            this.Kiépít.BackgroundImage = global::Villamos.Properties.Resources.Fatcow_Farm_Fresh_Table_row_delete_32;
            this.Kiépít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Kiépít.Location = new System.Drawing.Point(1129, 8);
            this.Kiépít.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Kiépít.Name = "Kiépít";
            this.Kiépít.Size = new System.Drawing.Size(48, 48);
            this.Kiépít.TabIndex = 78;
            this.toolTip1.SetToolTip(this.Kiépít, "Kiépíti a kijelölt akkumulátorokat");
            this.Kiépít.UseVisualStyleBackColor = true;
            this.Kiépít.Click += new System.EventHandler(this.Kiépít_Click);
            // 
            // Leselejtezett
            // 
            this.Leselejtezett.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Leselejtezett.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Leselejtezett.Location = new System.Drawing.Point(1297, 8);
            this.Leselejtezett.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Leselejtezett.Name = "Leselejtezett";
            this.Leselejtezett.Size = new System.Drawing.Size(48, 48);
            this.Leselejtezett.TabIndex = 82;
            this.toolTip1.SetToolTip(this.Leselejtezett, "Kijelölt elemek Selejtezése");
            this.Leselejtezett.UseVisualStyleBackColor = true;
            this.Leselejtezett.Click += new System.EventHandler(this.Leselejtezett_Click);
            // 
            // KIBE_Panel
            // 
            this.KIBE_Panel.Controls.Add(this.label29);
            this.KIBE_Panel.Controls.Add(this.BePSz);
            this.KIBE_Panel.Controls.Add(this.Beépít);
            this.KIBE_Panel.Location = new System.Drawing.Point(847, 3);
            this.KIBE_Panel.Name = "KIBE_Panel";
            this.KIBE_Panel.Size = new System.Drawing.Size(271, 59);
            this.KIBE_Panel.TabIndex = 81;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(3, 30);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(89, 20);
            this.label29.TabIndex = 79;
            this.label29.Text = "Pályaszám:";
            // 
            // BePSz
            // 
            this.BePSz.Location = new System.Drawing.Point(98, 24);
            this.BePSz.MaxLength = 30;
            this.BePSz.Name = "BePSz";
            this.BePSz.Size = new System.Drawing.Size(110, 26);
            this.BePSz.TabIndex = 63;
            // 
            // Beépít
            // 
            this.Beépít.BackgroundImage = global::Villamos.Properties.Resources.Fatcow_Farm_Fresh_Table_row_insert;
            this.Beépít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Beépít.Location = new System.Drawing.Point(215, 5);
            this.Beépít.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Beépít.Name = "Beépít";
            this.Beépít.Size = new System.Drawing.Size(48, 48);
            this.Beépít.TabIndex = 67;
            this.toolTip1.SetToolTip(this.Beépít, "Beépíti a kijelölt akkumulátorokat");
            this.Beépít.UseVisualStyleBackColor = true;
            this.Beépít.Click += new System.EventHandler(this.Beépít_Click);
            // 
            // Pályaszám_Szűrő
            // 
            this.Pályaszám_Szűrő.Controls.Add(this.label28);
            this.Pályaszám_Szűrő.Controls.Add(this.Beép_Psz);
            this.Pályaszám_Szűrő.Location = new System.Drawing.Point(553, 18);
            this.Pályaszám_Szűrő.Name = "Pályaszám_Szűrő";
            this.Pályaszám_Szűrő.Size = new System.Drawing.Size(227, 39);
            this.Pályaszám_Szűrő.TabIndex = 80;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(3, 15);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(89, 20);
            this.label28.TabIndex = 73;
            this.label28.Text = "Pályaszám:";
            // 
            // Beép_Psz
            // 
            this.Beép_Psz.FormattingEnabled = true;
            this.Beép_Psz.Location = new System.Drawing.Point(98, 7);
            this.Beép_Psz.Name = "Beép_Psz";
            this.Beép_Psz.Size = new System.Drawing.Size(124, 28);
            this.Beép_Psz.TabIndex = 74;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(9, 33);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(60, 20);
            this.label27.TabIndex = 69;
            this.label27.Text = "Státus:";
            // 
            // Beép_Frissít
            // 
            this.Beép_Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Beép_Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Beép_Frissít.Location = new System.Drawing.Point(790, 7);
            this.Beép_Frissít.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Beép_Frissít.Name = "Beép_Frissít";
            this.Beép_Frissít.Size = new System.Drawing.Size(48, 48);
            this.Beép_Frissít.TabIndex = 68;
            this.toolTip1.SetToolTip(this.Beép_Frissít, "Frissíti az adatokat");
            this.Beép_Frissít.UseVisualStyleBackColor = true;
            this.Beép_Frissít.Click += new System.EventHandler(this.Beép_Frissít_Click);
            // 
            // Tábla_Beép
            // 
            this.Tábla_Beép.AllowUserToAddRows = false;
            this.Tábla_Beép.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Khaki;
            this.Tábla_Beép.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Tábla_Beép.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla_Beép.BackgroundColor = System.Drawing.Color.DarkKhaki;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Moccasin;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla_Beép.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Tábla_Beép.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla_Beép.EnableHeadersVisualStyles = false;
            this.Tábla_Beép.Location = new System.Drawing.Point(4, 65);
            this.Tábla_Beép.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Tábla_Beép.Name = "Tábla_Beép";
            this.Tábla_Beép.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla_Beép.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Tábla_Beép.RowHeadersWidth = 42;
            this.Tábla_Beép.RowTemplate.Height = 30;
            this.Tábla_Beép.Size = new System.Drawing.Size(1408, 418);
            this.Tábla_Beép.TabIndex = 75;
            // 
            // SelejtElő
            // 
            this.SelejtElő.BackgroundImage = global::Villamos.Properties.Resources.App_spreadsheet;
            this.SelejtElő.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SelejtElő.Location = new System.Drawing.Point(1241, 8);
            this.SelejtElő.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SelejtElő.Name = "SelejtElő";
            this.SelejtElő.Size = new System.Drawing.Size(48, 48);
            this.SelejtElő.TabIndex = 71;
            this.toolTip1.SetToolTip(this.SelejtElő, "Kijelölt elemek Selejtelőkészítése");
            this.SelejtElő.UseVisualStyleBackColor = true;
            this.SelejtElő.Click += new System.EventHandler(this.SelejtElő_Click);
            // 
            // Beép_Státus
            // 
            this.Beép_Státus.FormattingEnabled = true;
            this.Beép_Státus.Location = new System.Drawing.Point(75, 25);
            this.Beép_Státus.Name = "Beép_Státus";
            this.Beép_Státus.Size = new System.Drawing.Size(189, 28);
            this.Beép_Státus.TabIndex = 70;
            this.Beép_Státus.SelectedIndexChanged += new System.EventHandler(this.Beép_Státus_SelectedIndexChanged);
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.PaleGreen;
            this.TabPage2.Controls.Add(this.TelephelyEllenőr);
            this.TabPage2.Controls.Add(this.Mérés);
            this.TabPage2.Controls.Add(this.Teljesség);
            this.TabPage2.Controls.Add(this.Telephely_Szűrő);
            this.TabPage2.Controls.Add(this.label24);
            this.TabPage2.Controls.Add(this.ExcelAlapLista);
            this.TabPage2.Controls.Add(this.Label21);
            this.TabPage2.Controls.Add(this.txtgyáriszám);
            this.TabPage2.Controls.Add(this.Tábla2);
            this.TabPage2.Controls.Add(this.Akku_Tábla_Listázás);
            this.TabPage2.Controls.Add(this.Label9);
            this.TabPage2.Controls.Add(this.ComboStátuslek);
            this.TabPage2.Controls.Add(this.TextPszlek);
            this.TabPage2.Controls.Add(this.Label8);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(1416, 488);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Akkumulátorok listázása";
            // 
            // TelephelyEllenőr
            // 
            this.TelephelyEllenőr.BackgroundImage = global::Villamos.Properties.Resources.process_accept;
            this.TelephelyEllenőr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TelephelyEllenőr.Location = new System.Drawing.Point(1312, 10);
            this.TelephelyEllenőr.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TelephelyEllenőr.Name = "TelephelyEllenőr";
            this.TelephelyEllenőr.Size = new System.Drawing.Size(45, 45);
            this.TelephelyEllenőr.TabIndex = 74;
            this.toolTip1.SetToolTip(this.TelephelyEllenőr, "Telephely ellenőrzés.\r\nA kocsi telephelyét átveszi az akkumulátor alapadata.\r\n");
            this.TelephelyEllenőr.UseVisualStyleBackColor = true;
            this.TelephelyEllenőr.Click += new System.EventHandler(this.TelephelyEllenőr_Click);
            // 
            // Mérés
            // 
            this.Mérés.BackgroundImage = global::Villamos.Properties.Resources.Aha_Soft_Large_Seo_SEO;
            this.Mérés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Mérés.Location = new System.Drawing.Point(1261, 10);
            this.Mérés.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Mérés.Name = "Mérés";
            this.Mérés.Size = new System.Drawing.Size(45, 45);
            this.Mérés.TabIndex = 73;
            this.toolTip1.SetToolTip(this.Mérés, "Utolsó mérési adatok");
            this.Mérés.UseVisualStyleBackColor = true;
            this.Mérés.Click += new System.EventHandler(this.Mérés_Click);
            // 
            // Teljesség
            // 
            this.Teljesség.BackgroundImage = global::Villamos.Properties.Resources.CARDFIL3;
            this.Teljesség.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Teljesség.Location = new System.Drawing.Point(1208, 10);
            this.Teljesség.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Teljesség.Name = "Teljesség";
            this.Teljesség.Size = new System.Drawing.Size(45, 45);
            this.Teljesség.TabIndex = 72;
            this.toolTip1.SetToolTip(this.Teljesség, "Teljesség vizsgálat");
            this.Teljesség.UseVisualStyleBackColor = true;
            this.Teljesség.Click += new System.EventHandler(this.Teljesség_Click);
            // 
            // Telephely_Szűrő
            // 
            this.Telephely_Szűrő.FormattingEnabled = true;
            this.Telephely_Szűrő.Location = new System.Drawing.Point(918, 27);
            this.Telephely_Szűrő.Name = "Telephely_Szűrő";
            this.Telephely_Szűrő.Size = new System.Drawing.Size(189, 28);
            this.Telephely_Szűrő.TabIndex = 71;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(832, 35);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(80, 20);
            this.label24.TabIndex = 70;
            this.label24.Text = "Telephely:";
            // 
            // ExcelAlapLista
            // 
            this.ExcelAlapLista.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.ExcelAlapLista.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ExcelAlapLista.Location = new System.Drawing.Point(1364, 10);
            this.ExcelAlapLista.Name = "ExcelAlapLista";
            this.ExcelAlapLista.Size = new System.Drawing.Size(45, 45);
            this.ExcelAlapLista.TabIndex = 69;
            this.toolTip1.SetToolTip(this.ExcelAlapLista, "Excel táblázatot készít a táblázat adataiból");
            this.ExcelAlapLista.UseVisualStyleBackColor = true;
            this.ExcelAlapLista.Click += new System.EventHandler(this.ExcelAlapLista_Click);
            // 
            // Label21
            // 
            this.Label21.AutoSize = true;
            this.Label21.Location = new System.Drawing.Point(551, 35);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(88, 20);
            this.Label21.TabIndex = 68;
            this.Label21.Text = "Gyáriszám:";
            // 
            // txtgyáriszám
            // 
            this.txtgyáriszám.Location = new System.Drawing.Point(645, 29);
            this.txtgyáriszám.Name = "txtgyáriszám";
            this.txtgyáriszám.Size = new System.Drawing.Size(181, 26);
            this.txtgyáriszám.TabIndex = 67;
            // 
            // tábla2
            // 
            this.Tábla2.AllowUserToAddRows = false;
            this.Tábla2.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Khaki;
            this.Tábla2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.Tábla2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla2.BackgroundColor = System.Drawing.Color.DarkKhaki;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Moccasin;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.Tábla2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla2.EnableHeadersVisualStyles = false;
            this.Tábla2.Location = new System.Drawing.Point(7, 63);
            this.Tábla2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Tábla2.Name = "tábla2";
            this.Tábla2.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla2.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.Tábla2.RowHeadersVisible = false;
            this.Tábla2.RowHeadersWidth = 42;
            this.Tábla2.RowTemplate.Height = 30;
            this.Tábla2.Size = new System.Drawing.Size(1402, 414);
            this.Tábla2.TabIndex = 64;
            this.Tábla2.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla2_CellDoubleClick);
            // 
            // Akku_Tábla_Listázás
            // 
            this.Akku_Tábla_Listázás.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Akku_Tábla_Listázás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Akku_Tábla_Listázás.Location = new System.Drawing.Point(1114, 10);
            this.Akku_Tábla_Listázás.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Akku_Tábla_Listázás.Name = "Akku_Tábla_Listázás";
            this.Akku_Tábla_Listázás.Size = new System.Drawing.Size(45, 45);
            this.Akku_Tábla_Listázás.TabIndex = 63;
            this.toolTip1.SetToolTip(this.Akku_Tábla_Listázás, "Frissíti a táblázatot");
            this.Akku_Tábla_Listázás.UseVisualStyleBackColor = true;
            this.Akku_Tábla_Listázás.Click += new System.EventHandler(this.Akku_Tábla_Listázás_Click);
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(269, 35);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(89, 20);
            this.Label9.TabIndex = 62;
            this.Label9.Text = "Pályaszám:";
            // 
            // ComboStátuslek
            // 
            this.ComboStátuslek.FormattingEnabled = true;
            this.ComboStátuslek.Location = new System.Drawing.Point(74, 27);
            this.ComboStátuslek.Name = "ComboStátuslek";
            this.ComboStátuslek.Size = new System.Drawing.Size(189, 28);
            this.ComboStátuslek.TabIndex = 61;
            // 
            // TextPszlek
            // 
            this.TextPszlek.Location = new System.Drawing.Point(364, 29);
            this.TextPszlek.Name = "TextPszlek";
            this.TextPszlek.Size = new System.Drawing.Size(181, 26);
            this.TextPszlek.TabIndex = 60;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(8, 35);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(60, 20);
            this.Label8.TabIndex = 59;
            this.Label8.Text = "Státus:";
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.Turquoise;
            this.TabPage4.Controls.Add(this.label30);
            this.TabPage4.Controls.Add(this.MérésLekGyári);
            this.TabPage4.Controls.Add(this.dátumig);
            this.TabPage4.Controls.Add(this.Dátumtól);
            this.TabPage4.Controls.Add(this.Tábla4);
            this.TabPage4.Controls.Add(this.R_törlés);
            this.TabPage4.Controls.Add(this.Button2);
            this.TabPage4.Controls.Add(this.BtnMéréslista);
            this.TabPage4.Location = new System.Drawing.Point(4, 29);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Size = new System.Drawing.Size(1416, 488);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Mérések listázása";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(241, 35);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(88, 20);
            this.label30.TabIndex = 100;
            this.label30.Text = "Gyáriszám:";
            // 
            // MérésLekGyári
            // 
            this.MérésLekGyári.Location = new System.Drawing.Point(335, 29);
            this.MérésLekGyári.Name = "MérésLekGyári";
            this.MérésLekGyári.Size = new System.Drawing.Size(181, 26);
            this.MérésLekGyári.TabIndex = 99;
            // 
            // dátumig
            // 
            this.dátumig.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dátumig.Location = new System.Drawing.Point(128, 29);
            this.dátumig.Name = "dátumig";
            this.dátumig.Size = new System.Drawing.Size(107, 26);
            this.dátumig.TabIndex = 39;
            // 
            // Dátumtól
            // 
            this.Dátumtól.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátumtól.Location = new System.Drawing.Point(8, 29);
            this.Dátumtól.Name = "Dátumtól";
            this.Dátumtól.Size = new System.Drawing.Size(107, 26);
            this.Dátumtól.TabIndex = 38;
            // 
            // Tábla4
            // 
            this.Tábla4.AllowUserToAddRows = false;
            this.Tábla4.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Khaki;
            this.Tábla4.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.Tábla4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla4.BackgroundColor = System.Drawing.Color.DarkKhaki;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.Moccasin;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla4.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.Tábla4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla4.EnableHeadersVisualStyles = false;
            this.Tábla4.Location = new System.Drawing.Point(8, 64);
            this.Tábla4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Tábla4.Name = "Tábla4";
            this.Tábla4.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla4.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.Tábla4.RowHeadersWidth = 42;
            this.Tábla4.RowTemplate.Height = 30;
            this.Tábla4.Size = new System.Drawing.Size(1404, 417);
            this.Tábla4.TabIndex = 36;
            // 
            // R_törlés
            // 
            this.R_törlés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.R_törlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.R_törlés.Location = new System.Drawing.Point(855, 14);
            this.R_törlés.Name = "R_törlés";
            this.R_törlés.Size = new System.Drawing.Size(40, 40);
            this.R_törlés.TabIndex = 98;
            this.toolTip1.SetToolTip(this.R_törlés, "Törli az adatokat");
            this.R_törlés.UseVisualStyleBackColor = true;
            this.R_törlés.Click += new System.EventHandler(this.R_törlés_Click);
            // 
            // Button2
            // 
            this.Button2.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button2.Location = new System.Drawing.Point(809, 14);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(40, 40);
            this.Button2.TabIndex = 57;
            this.toolTip1.SetToolTip(this.Button2, "Excel táblázatot készít a táblázat adataiból");
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // BtnMéréslista
            // 
            this.BtnMéréslista.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.BtnMéréslista.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnMéréslista.Location = new System.Drawing.Point(542, 15);
            this.BtnMéréslista.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnMéréslista.Name = "BtnMéréslista";
            this.BtnMéréslista.Size = new System.Drawing.Size(40, 40);
            this.BtnMéréslista.TabIndex = 37;
            this.toolTip1.SetToolTip(this.BtnMéréslista, "Frissíti a táblázatot");
            this.BtnMéréslista.UseVisualStyleBackColor = true;
            this.BtnMéréslista.Click += new System.EventHandler(this.BtnMéréslista_Click);
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.CmbTelephely);
            this.Panel1.Controls.Add(this.label23);
            this.Panel1.Location = new System.Drawing.Point(12, 4);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(373, 45);
            this.Panel1.TabIndex = 54;
            // 
            // CmbTelephely
            // 
            this.CmbTelephely.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbTelephely.FormattingEnabled = true;
            this.CmbTelephely.Location = new System.Drawing.Point(184, 9);
            this.CmbTelephely.Name = "CmbTelephely";
            this.CmbTelephely.Size = new System.Drawing.Size(186, 28);
            this.CmbTelephely.TabIndex = 18;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(12, 11);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(145, 20);
            this.label23.TabIndex = 17;
            this.label23.Text = "Telephelyi beállítás:";
            // 
            // Súgó
            // 
            this.Súgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Súgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.Súgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Súgó.Location = new System.Drawing.Point(1391, 4);
            this.Súgó.Name = "Súgó";
            this.Súgó.Size = new System.Drawing.Size(45, 45);
            this.Súgó.TabIndex = 55;
            this.toolTip1.SetToolTip(this.Súgó, "Súgó");
            this.Súgó.UseVisualStyleBackColor = true;
            this.Súgó.Click += new System.EventHandler(this.Súgó_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(410, 15);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(975, 30);
            this.Holtart.TabIndex = 56;
            this.Holtart.Visible = false;
            // 
            // Ablak_Akkumulátor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Peru;
            this.ClientSize = new System.Drawing.Size(1448, 579);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Súgó);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Fülek);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Akkumulátor";
            this.Text = "Akkumulátor nyilvántartás";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AblakAkkumulátor_Load);
            this.Fülek.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.TabPage3.ResumeLayout(false);
            this.TabPage3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.KIBE_Panel.ResumeLayout(false);
            this.KIBE_Panel.PerformLayout();
            this.Pályaszám_Szűrő.ResumeLayout(false);
            this.Pályaszám_Szűrő.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_Beép)).EndInit();
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla2)).EndInit();
            this.TabPage4.ResumeLayout(false);
            this.TabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla4)).EndInit();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.ResumeLayout(false);

        }
        internal TabControl Fülek;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal TabPage TabPage3;
        internal TabPage TabPage4;
        internal DateTimePicker dátumig;
        internal DateTimePicker Dátumtól;
        internal Button BtnMéréslista;
        internal DataGridView Tábla4;
        internal DateTimePicker mérdátum;
        internal CheckBox Check1;
        internal Label Label1;
        internal TextBox TextMérmegjegyzés;
        internal TextBox Textgyárimérés;
        internal TextBox Textmérkisütésiáram;
        internal TextBox Textmérkezdetifesz;
        internal TextBox Textmérvégfesz;
        internal TextBox Textmérkapacitás;
        internal Label Label7;
        internal Label Label6;
        internal Label Label5;
        internal Label Label4;
        internal Label Label3;
        internal Label Label2;
        internal Button btnrögzítés;
        internal Button btnmérúj;
        internal Label Label9;
        internal ComboBox ComboStátuslek;
        internal TextBox TextPszlek;
        internal Label Label8;
        internal DataGridView Tábla2;
        internal Button Akku_Tábla_Listázás;
        internal ComboBox Combofajta;
        internal ComboBox Combotípus;
        internal TextBox Textgyáriszám;
        internal Label Label18;
        internal Label Label16;
        internal Label Label15;
        internal Label Label14;
        internal Label Label13;
        internal Label Label12;
        internal DateTimePicker Dgyártásiidő;
        internal Label Label11;
        internal TextBox TextMegjegyzés;
        internal Button btnAkufriss;
        internal Button Btnakurögzít;
        internal Button Btnakuúj;
        internal ComboBox Combogyártó;
        internal Label Label10;
        internal TextBox Textbeépítve;
        internal DateTimePicker Dbeépítésdátum;
        internal Label Label20;
        internal Label Label19;
        internal DateTimePicker Dgarancia;
        internal Label Label21;
        internal TextBox txtgyáriszám;
        internal Button ExcelAlapLista;
        internal Button Button2;
        internal Label Label22;
        internal DateTimePicker MérésDátuma;
        internal Button R_törlés;
        internal Button Súgó;
        internal Panel Panel1;
        internal ComboBox CmbTelephely;
        internal Label label23;
        private ToolTip toolTip1;
        internal ComboBox Telephely_Szűrő;
        internal Label label24;
        private TabPage tabPage5;
        private TableLayoutPanel tableLayoutPanel1;
        internal TextBox Kapacitás_Alap;
        internal TextBox Telephely_alap;
        internal Label label25;
        internal Label label17;
        internal TextBox Státus_alap;
        internal ComboBox Beép_Státus;
        internal Label label27;
        internal Button Beép_Frissít;
        internal Button Beépít;
        internal TextBox BePSz;
        internal Button Kiépít;
        internal DataGridView Tábla_Beép;
        internal ComboBox Beép_Psz;
        internal Label label28;
        internal Button SelejtElő;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel Pályaszám_Szűrő;
        internal Label label29;
        private Panel KIBE_Panel;
        internal Button Törölt;
        internal Button Leselejtezett;
        internal Button Használt;
        internal Button Résztörlés;
        internal Label label26;
        internal TextBox Beép_Gyári;
        internal Button Teljesség;
        internal Button Mérés;
        internal Label label30;
        internal TextBox MérésLekGyári;
        internal Button TelephelyEllenőr;
        private V_MindenEgyéb.MyProgressbar Holtart;
    }
}