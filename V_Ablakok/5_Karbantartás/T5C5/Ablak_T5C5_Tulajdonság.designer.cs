using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
    
    public partial class Ablak_T5C5_Tulajdonság : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_T5C5_Tulajdonság));
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.Fülek = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label41 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Járműtípus_text = new System.Windows.Forms.Label();
            this.Főmérnökség_text = new System.Windows.Forms.Label();
            this.Vizsgálati_text = new System.Windows.Forms.Label();
            this.Elő_Szerelvény_text = new System.Windows.Forms.Label();
            this.Típus_text = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Szerelvény_text = new System.Windows.Forms.Label();
            this.Miótaáll_text = new System.Windows.Forms.Label();
            this.Státus_text = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.ÜzembehelyezésiPDF = new System.Windows.Forms.Button();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.Futásnap = new System.Windows.Forms.TextBox();
            this.Utolsóvizsgálatszáma = new System.Windows.Forms.TextBox();
            this.Utolsóvizsgálatfokozata = new System.Windows.Forms.ComboBox();
            this.Utolsóvizsgálatdátuma = new System.Windows.Forms.DateTimePicker();
            this.Utolsóforgalminap = new System.Windows.Forms.DateTimePicker();
            this.Label14 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Rögzítnap = new System.Windows.Forms.Button();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.Teljes_adatbázis_excel = new System.Windows.Forms.Button();
            this.Tábla_lekérdezés = new System.Windows.Forms.DataGridView();
            this.Excellekérdezés = new System.Windows.Forms.Button();
            this.Lekérdezés_lekérdezés = new System.Windows.Forms.Button();
            this.TabPage5 = new System.Windows.Forms.TabPage();
            this.Töröl = new System.Windows.Forms.Button();
            this.KövV2km = new System.Windows.Forms.TextBox();
            this.Label35 = new System.Windows.Forms.Label();
            this.KövV2_Sorszám = new System.Windows.Forms.TextBox();
            this.KövV_Sorszám = new System.Windows.Forms.TextBox();
            this.KövV2_számláló = new System.Windows.Forms.TextBox();
            this.KövV2 = new System.Windows.Forms.TextBox();
            this.KövV1km = new System.Windows.Forms.TextBox();
            this.KövV = new System.Windows.Forms.TextBox();
            this.Sorszám = new System.Windows.Forms.TextBox();
            this.Label34 = new System.Windows.Forms.Label();
            this.Jjavszám = new System.Windows.Forms.TextBox();
            this.KMUkm = new System.Windows.Forms.TextBox();
            this.VizsgKm = new System.Windows.Forms.TextBox();
            this.Vizsgfok = new System.Windows.Forms.TextBox();
            this.HaviKm = new System.Windows.Forms.TextBox();
            this.TEljesKmText = new System.Windows.Forms.TextBox();
            this.CiklusrendCombo = new System.Windows.Forms.ComboBox();
            this.Üzemek = new System.Windows.Forms.ComboBox();
            this.Vizsgsorszám = new System.Windows.Forms.ComboBox();
            this.KMUdátum = new System.Windows.Forms.DateTimePicker();
            this.Utolsófelújításdátuma = new System.Windows.Forms.DateTimePicker();
            this.Vizsgdátumk = new System.Windows.Forms.DateTimePicker();
            this.Vizsgdátumv = new System.Windows.Forms.DateTimePicker();
            this.SAP_adatok = new System.Windows.Forms.Button();
            this.Új_adat = new System.Windows.Forms.Button();
            this.Utolsó_V_rögzítés = new System.Windows.Forms.Button();
            this.Label33 = new System.Windows.Forms.Label();
            this.Label32 = new System.Windows.Forms.Label();
            this.Label31 = new System.Windows.Forms.Label();
            this.Label30 = new System.Windows.Forms.Label();
            this.Label29 = new System.Windows.Forms.Label();
            this.Label28 = new System.Windows.Forms.Label();
            this.Label27 = new System.Windows.Forms.Label();
            this.Label26 = new System.Windows.Forms.Label();
            this.Label25 = new System.Windows.Forms.Label();
            this.Label24 = new System.Windows.Forms.Label();
            this.Label23 = new System.Windows.Forms.Label();
            this.Label22 = new System.Windows.Forms.Label();
            this.Label21 = new System.Windows.Forms.Label();
            this.Label20 = new System.Windows.Forms.Label();
            this.Label19 = new System.Windows.Forms.Label();
            this.Label18 = new System.Windows.Forms.Label();
            this.Label17 = new System.Windows.Forms.Label();
            this.TabPage6 = new System.Windows.Forms.TabPage();
            this.VizsgAdat_Excel = new System.Windows.Forms.Button();
            this.VizsgAdat_Frissít = new System.Windows.Forms.Button();
            this.Tábla1 = new System.Windows.Forms.DataGridView();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.Panel7 = new System.Windows.Forms.Panel();
            this.Kerékcsökkenés = new System.Windows.Forms.TextBox();
            this.Label39 = new System.Windows.Forms.Label();
            this.Hónapok = new System.Windows.Forms.TextBox();
            this.Havikmlabel = new System.Windows.Forms.TextBox();
            this.FőHoltart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.AlHoltart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.Command1 = new System.Windows.Forms.Button();
            this.Panel5 = new System.Windows.Forms.Panel();
            this.Text2 = new System.Windows.Forms.TextBox();
            this.Label38 = new System.Windows.Forms.Label();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.Option12 = new System.Windows.Forms.RadioButton();
            this.Option11 = new System.Windows.Forms.RadioButton();
            this.Option10 = new System.Windows.Forms.RadioButton();
            this.Label37 = new System.Windows.Forms.Label();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Text1 = new System.Windows.Forms.TextBox();
            this.Option8 = new System.Windows.Forms.RadioButton();
            this.Option9 = new System.Windows.Forms.RadioButton();
            this.Option7 = new System.Windows.Forms.RadioButton();
            this.Option6 = new System.Windows.Forms.RadioButton();
            this.Option5 = new System.Windows.Forms.RadioButton();
            this.Label36 = new System.Windows.Forms.Label();
            this.PszJelölő = new System.Windows.Forms.CheckedListBox();
            this.Mindentkijelöl = new System.Windows.Forms.Button();
            this.Kijelöléstörlése = new System.Windows.Forms.Button();
            this.Kimutatás_más = new System.Windows.Forms.Button();
            this.Panel6 = new System.Windows.Forms.Panel();
            this.Check1 = new System.Windows.Forms.CheckBox();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.Command2 = new System.Windows.Forms.Button();
            this.Telephely = new System.Windows.Forms.ComboBox();
            this.Label16 = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.Pályaszám = new System.Windows.Forms.ComboBox();
            this.Excel_gomb = new System.Windows.Forms.Button();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Pályaszámkereső = new System.Windows.Forms.Button();
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Panel2.SuspendLayout();
            this.Fülek.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.TabPage2.SuspendLayout();
            this.TabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_lekérdezés)).BeginInit();
            this.TabPage5.SuspendLayout();
            this.TabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla1)).BeginInit();
            this.TabPage3.SuspendLayout();
            this.Panel7.SuspendLayout();
            this.Panel5.SuspendLayout();
            this.Panel4.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.Panel6.SuspendLayout();
            this.Panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.Cmbtelephely);
            this.Panel2.Controls.Add(this.Label13);
            this.Panel2.Location = new System.Drawing.Point(10, 6);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(346, 39);
            this.Panel2.TabIndex = 56;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(149, 5);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
            this.Cmbtelephely.SelectedIndexChanged += new System.EventHandler(this.Cmbtelephely_SelectedIndexChanged);
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(5, 9);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(145, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(703, 10);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(411, 28);
            this.Holtart.TabIndex = 61;
            this.Holtart.Visible = false;
            // 
            // Fülek
            // 
            this.Fülek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fülek.Controls.Add(this.TabPage1);
            this.Fülek.Controls.Add(this.TabPage2);
            this.Fülek.Controls.Add(this.TabPage4);
            this.Fülek.Controls.Add(this.TabPage5);
            this.Fülek.Controls.Add(this.TabPage6);
            this.Fülek.Controls.Add(this.TabPage3);
            this.Fülek.Location = new System.Drawing.Point(10, 50);
            this.Fülek.Name = "Fülek";
            this.Fülek.Padding = new System.Drawing.Point(16, 3);
            this.Fülek.SelectedIndex = 0;
            this.Fülek.Size = new System.Drawing.Size(1144, 479);
            this.Fülek.TabIndex = 62;
            this.Fülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Fülek_DrawItem);
            this.Fülek.SelectedIndexChanged += new System.EventHandler(this.Fülek_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.LimeGreen;
            this.TabPage1.Controls.Add(this.tableLayoutPanel1);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1136, 446);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Alapadatok";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.Label8, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Vizsgálati_text, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.Elő_Szerelvény_text, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.Típus_text, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label40, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.Label7, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Label4, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.Szerelvény_text, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.Miótaáll_text, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.Státus_text, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.Label6, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.Label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.Főmérnökség_text, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.Járműtípus_text, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.ÜzembehelyezésiPDF, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.Label2, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.Label1, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label41, 0, 8);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(15, 14);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(697, 328);
            this.tableLayoutPanel1.TabIndex = 18;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.BackColor = System.Drawing.Color.LightGreen;
            this.label41.Location = new System.Drawing.Point(3, 240);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(227, 20);
            this.label41.TabIndex = 18;
            this.label41.Text = "Üzembehelyezési jegyzőkönyv:";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.BackColor = System.Drawing.Color.LightGreen;
            this.Label8.Location = new System.Drawing.Point(3, 0);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(51, 20);
            this.Label8.TabIndex = 7;
            this.Label8.Text = "Típus:";
            // 
            // Járműtípus_text
            // 
            this.Járműtípus_text.AutoSize = true;
            this.Járműtípus_text.BackColor = System.Drawing.Color.LightGreen;
            this.Járműtípus_text.Location = new System.Drawing.Point(253, 210);
            this.Járműtípus_text.Name = "Járműtípus_text";
            this.Járműtípus_text.Size = new System.Drawing.Size(66, 20);
            this.Járműtípus_text.TabIndex = 8;
            this.Járműtípus_text.Text = "Label17";
            // 
            // Főmérnökség_text
            // 
            this.Főmérnökség_text.AutoSize = true;
            this.Főmérnökség_text.BackColor = System.Drawing.Color.LightGreen;
            this.Főmérnökség_text.Location = new System.Drawing.Point(253, 180);
            this.Főmérnökség_text.Name = "Főmérnökség_text";
            this.Főmérnökség_text.Size = new System.Drawing.Size(66, 20);
            this.Főmérnökség_text.TabIndex = 9;
            this.Főmérnökség_text.Text = "Label16";
            // 
            // Vizsgálati_text
            // 
            this.Vizsgálati_text.AutoSize = true;
            this.Vizsgálati_text.BackColor = System.Drawing.Color.LightGreen;
            this.Vizsgálati_text.Location = new System.Drawing.Point(253, 150);
            this.Vizsgálati_text.Name = "Vizsgálati_text";
            this.Vizsgálati_text.Size = new System.Drawing.Size(66, 20);
            this.Vizsgálati_text.TabIndex = 11;
            this.Vizsgálati_text.Text = "Label14";
            // 
            // Elő_Szerelvény_text
            // 
            this.Elő_Szerelvény_text.AutoSize = true;
            this.Elő_Szerelvény_text.BackColor = System.Drawing.Color.LightGreen;
            this.Elő_Szerelvény_text.Location = new System.Drawing.Point(253, 120);
            this.Elő_Szerelvény_text.Name = "Elő_Szerelvény_text";
            this.Elő_Szerelvény_text.Size = new System.Drawing.Size(66, 20);
            this.Elő_Szerelvény_text.TabIndex = 17;
            this.Elő_Szerelvény_text.Text = "Label12";
            // 
            // Típus_text
            // 
            this.Típus_text.AutoSize = true;
            this.Típus_text.BackColor = System.Drawing.Color.LightGreen;
            this.Típus_text.Location = new System.Drawing.Point(253, 0);
            this.Típus_text.Name = "Típus_text";
            this.Típus_text.Size = new System.Drawing.Size(57, 20);
            this.Típus_text.TabIndex = 15;
            this.Típus_text.Text = "Label9";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.BackColor = System.Drawing.Color.LightGreen;
            this.label40.Location = new System.Drawing.Point(3, 120);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(185, 20);
            this.label40.TabIndex = 16;
            this.label40.Text = "Előírt Szerelvény kocsijai:";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.BackColor = System.Drawing.Color.LightGreen;
            this.Label7.Location = new System.Drawing.Point(3, 30);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(60, 20);
            this.Label7.TabIndex = 6;
            this.Label7.Text = "Státus:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.LightGreen;
            this.Label1.Location = new System.Drawing.Point(3, 210);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(95, 20);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Jármű típus:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.LightGreen;
            this.Label2.Location = new System.Drawing.Point(3, 180);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(152, 20);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "Főmérnökségi típus:";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.BackColor = System.Drawing.Color.LightGreen;
            this.Label4.Location = new System.Drawing.Point(3, 150);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(112, 20);
            this.Label4.TabIndex = 3;
            this.Label4.Text = "Vizsgálati nap:";
            // 
            // Szerelvény_text
            // 
            this.Szerelvény_text.AutoSize = true;
            this.Szerelvény_text.BackColor = System.Drawing.Color.LightGreen;
            this.Szerelvény_text.Location = new System.Drawing.Point(253, 90);
            this.Szerelvény_text.Name = "Szerelvény_text";
            this.Szerelvény_text.Size = new System.Drawing.Size(66, 20);
            this.Szerelvény_text.TabIndex = 12;
            this.Szerelvény_text.Text = "Label12";
            // 
            // Miótaáll_text
            // 
            this.Miótaáll_text.AutoSize = true;
            this.Miótaáll_text.BackColor = System.Drawing.Color.LightGreen;
            this.Miótaáll_text.Location = new System.Drawing.Point(253, 60);
            this.Miótaáll_text.Name = "Miótaáll_text";
            this.Miótaáll_text.Size = new System.Drawing.Size(66, 20);
            this.Miótaáll_text.TabIndex = 13;
            this.Miótaáll_text.Text = "Label11";
            // 
            // Státus_text
            // 
            this.Státus_text.AutoSize = true;
            this.Státus_text.BackColor = System.Drawing.Color.LightGreen;
            this.Státus_text.Location = new System.Drawing.Point(253, 30);
            this.Státus_text.Name = "Státus_text";
            this.Státus_text.Size = new System.Drawing.Size(66, 20);
            this.Státus_text.TabIndex = 14;
            this.Státus_text.Text = "Label10";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.BackColor = System.Drawing.Color.LightGreen;
            this.Label6.Location = new System.Drawing.Point(3, 60);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(71, 20);
            this.Label6.TabIndex = 5;
            this.Label6.Text = "Mióta áll:";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.BackColor = System.Drawing.Color.LightGreen;
            this.Label5.Location = new System.Drawing.Point(3, 90);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(145, 20);
            this.Label5.TabIndex = 4;
            this.Label5.Text = "Szerelvény kocsijai:";
            // 
            // ÜzembehelyezésiPDF
            // 
            this.ÜzembehelyezésiPDF.BackgroundImage = global::Villamos.Properties.Resources.pdf_32;
            this.ÜzembehelyezésiPDF.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ÜzembehelyezésiPDF.Location = new System.Drawing.Point(253, 243);
            this.ÜzembehelyezésiPDF.Name = "ÜzembehelyezésiPDF";
            this.ÜzembehelyezésiPDF.Size = new System.Drawing.Size(45, 45);
            this.ÜzembehelyezésiPDF.TabIndex = 64;
            this.ToolTip1.SetToolTip(this.ÜzembehelyezésiPDF, "Üzembehelyezési jegyzőkönyv(ek )");
            this.ÜzembehelyezésiPDF.UseVisualStyleBackColor = true;
            this.ÜzembehelyezésiPDF.Click += new System.EventHandler(this.ÜzembehelyezésiPDF_Click);
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.MediumTurquoise;
            this.TabPage2.Controls.Add(this.Futásnap);
            this.TabPage2.Controls.Add(this.Utolsóvizsgálatszáma);
            this.TabPage2.Controls.Add(this.Utolsóvizsgálatfokozata);
            this.TabPage2.Controls.Add(this.Utolsóvizsgálatdátuma);
            this.TabPage2.Controls.Add(this.Utolsóforgalminap);
            this.TabPage2.Controls.Add(this.Label14);
            this.TabPage2.Controls.Add(this.Label12);
            this.TabPage2.Controls.Add(this.Label11);
            this.TabPage2.Controls.Add(this.Label10);
            this.TabPage2.Controls.Add(this.Label9);
            this.TabPage2.Controls.Add(this.Rögzítnap);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(1136, 446);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Futás adatok";
            // 
            // Futásnap
            // 
            this.Futásnap.Location = new System.Drawing.Point(207, 125);
            this.Futásnap.Name = "Futásnap";
            this.Futásnap.Size = new System.Drawing.Size(124, 26);
            this.Futásnap.TabIndex = 14;
            // 
            // Utolsóvizsgálatszáma
            // 
            this.Utolsóvizsgálatszáma.Location = new System.Drawing.Point(207, 89);
            this.Utolsóvizsgálatszáma.Name = "Utolsóvizsgálatszáma";
            this.Utolsóvizsgálatszáma.Size = new System.Drawing.Size(124, 26);
            this.Utolsóvizsgálatszáma.TabIndex = 13;
            // 
            // Utolsóvizsgálatfokozata
            // 
            this.Utolsóvizsgálatfokozata.FormattingEnabled = true;
            this.Utolsóvizsgálatfokozata.Location = new System.Drawing.Point(207, 54);
            this.Utolsóvizsgálatfokozata.Name = "Utolsóvizsgálatfokozata";
            this.Utolsóvizsgálatfokozata.Size = new System.Drawing.Size(124, 28);
            this.Utolsóvizsgálatfokozata.TabIndex = 12;
            // 
            // Utolsóvizsgálatdátuma
            // 
            this.Utolsóvizsgálatdátuma.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Utolsóvizsgálatdátuma.Location = new System.Drawing.Point(208, 17);
            this.Utolsóvizsgálatdátuma.Name = "Utolsóvizsgálatdátuma";
            this.Utolsóvizsgálatdátuma.Size = new System.Drawing.Size(123, 26);
            this.Utolsóvizsgálatdátuma.TabIndex = 11;
            this.Utolsóvizsgálatdátuma.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // Utolsóforgalminap
            // 
            this.Utolsóforgalminap.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Utolsóforgalminap.Location = new System.Drawing.Point(208, 162);
            this.Utolsóforgalminap.Name = "Utolsóforgalminap";
            this.Utolsóforgalminap.Size = new System.Drawing.Size(123, 26);
            this.Utolsóforgalminap.TabIndex = 10;
            this.Utolsóforgalminap.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(16, 59);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(190, 20);
            this.Label14.TabIndex = 4;
            this.Label14.Text = "Utolsó vizsgálat fokozata:";
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(16, 95);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(175, 20);
            this.Label12.TabIndex = 3;
            this.Label12.Text = "Utolsó vizsgálat száma:";
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(16, 131);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(81, 20);
            this.Label11.TabIndex = 2;
            this.Label11.Text = "Futásnap:";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(16, 167);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(162, 20);
            this.Label10.TabIndex = 1;
            this.Label10.Text = "Utolsó forgalmi napja:";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(16, 23);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(182, 20);
            this.Label9.TabIndex = 0;
            this.Label9.Text = "Utolsó vizsgálat dátuma:";
            // 
            // Rögzítnap
            // 
            this.Rögzítnap.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Rögzítnap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Rögzítnap.Location = new System.Drawing.Point(361, 17);
            this.Rögzítnap.Name = "Rögzítnap";
            this.Rögzítnap.Size = new System.Drawing.Size(45, 45);
            this.Rögzítnap.TabIndex = 15;
            this.ToolTip1.SetToolTip(this.Rögzítnap, "Rögzíti a futásnapokat");
            this.Rögzítnap.UseVisualStyleBackColor = true;
            this.Rögzítnap.Click += new System.EventHandler(this.Rögzítnap_Click);
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.YellowGreen;
            this.TabPage4.Controls.Add(this.Teljes_adatbázis_excel);
            this.TabPage4.Controls.Add(this.Tábla_lekérdezés);
            this.TabPage4.Controls.Add(this.Excellekérdezés);
            this.TabPage4.Controls.Add(this.Lekérdezés_lekérdezés);
            this.TabPage4.Location = new System.Drawing.Point(4, 29);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Size = new System.Drawing.Size(1136, 446);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Lekérdezések";
            // 
            // Teljes_adatbázis_excel
            // 
            this.Teljes_adatbázis_excel.BackgroundImage = global::Villamos.Properties.Resources.Device_zip;
            this.Teljes_adatbázis_excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Teljes_adatbázis_excel.Location = new System.Drawing.Point(165, 4);
            this.Teljes_adatbázis_excel.Name = "Teljes_adatbázis_excel";
            this.Teljes_adatbázis_excel.Size = new System.Drawing.Size(45, 45);
            this.Teljes_adatbázis_excel.TabIndex = 168;
            this.ToolTip1.SetToolTip(this.Teljes_adatbázis_excel, "Teljes adatbázis kimentés");
            this.Teljes_adatbázis_excel.UseVisualStyleBackColor = true;
            this.Teljes_adatbázis_excel.Click += new System.EventHandler(this.Teljes_adatbázis_excel_Click);
            // 
            // Tábla_lekérdezés
            // 
            this.Tábla_lekérdezés.AllowUserToAddRows = false;
            this.Tábla_lekérdezés.AllowUserToDeleteRows = false;
            this.Tábla_lekérdezés.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla_lekérdezés.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla_lekérdezés.Location = new System.Drawing.Point(5, 55);
            this.Tábla_lekérdezés.Name = "Tábla_lekérdezés";
            this.Tábla_lekérdezés.RowHeadersVisible = false;
            this.Tábla_lekérdezés.Size = new System.Drawing.Size(1128, 388);
            this.Tábla_lekérdezés.TabIndex = 167;
            this.Tábla_lekérdezés.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_lekérdezés_CellClick);
            // 
            // Excellekérdezés
            // 
            this.Excellekérdezés.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excellekérdezés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excellekérdezés.Location = new System.Drawing.Point(54, 3);
            this.Excellekérdezés.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Excellekérdezés.Name = "Excellekérdezés";
            this.Excellekérdezés.Size = new System.Drawing.Size(45, 45);
            this.Excellekérdezés.TabIndex = 166;
            this.ToolTip1.SetToolTip(this.Excellekérdezés, "A táblázat adatait Excelbe menti");
            this.Excellekérdezés.UseVisualStyleBackColor = true;
            this.Excellekérdezés.Click += new System.EventHandler(this.Excellekérdezés_Click);
            // 
            // Lekérdezés_lekérdezés
            // 
            this.Lekérdezés_lekérdezés.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Lekérdezés_lekérdezés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lekérdezés_lekérdezés.Location = new System.Drawing.Point(3, 3);
            this.Lekérdezés_lekérdezés.Name = "Lekérdezés_lekérdezés";
            this.Lekérdezés_lekérdezés.Size = new System.Drawing.Size(45, 45);
            this.Lekérdezés_lekérdezés.TabIndex = 64;
            this.ToolTip1.SetToolTip(this.Lekérdezés_lekérdezés, "Frissíti a táblázat adatait");
            this.Lekérdezés_lekérdezés.UseVisualStyleBackColor = true;
            this.Lekérdezés_lekérdezés.Click += new System.EventHandler(this.Lekérdezés_lekérdezés_Click);
            // 
            // TabPage5
            // 
            this.TabPage5.BackColor = System.Drawing.Color.DarkOrange;
            this.TabPage5.Controls.Add(this.Töröl);
            this.TabPage5.Controls.Add(this.KövV2km);
            this.TabPage5.Controls.Add(this.Label35);
            this.TabPage5.Controls.Add(this.KövV2_Sorszám);
            this.TabPage5.Controls.Add(this.KövV_Sorszám);
            this.TabPage5.Controls.Add(this.KövV2_számláló);
            this.TabPage5.Controls.Add(this.KövV2);
            this.TabPage5.Controls.Add(this.KövV1km);
            this.TabPage5.Controls.Add(this.KövV);
            this.TabPage5.Controls.Add(this.Sorszám);
            this.TabPage5.Controls.Add(this.Label34);
            this.TabPage5.Controls.Add(this.Jjavszám);
            this.TabPage5.Controls.Add(this.KMUkm);
            this.TabPage5.Controls.Add(this.VizsgKm);
            this.TabPage5.Controls.Add(this.Vizsgfok);
            this.TabPage5.Controls.Add(this.HaviKm);
            this.TabPage5.Controls.Add(this.TEljesKmText);
            this.TabPage5.Controls.Add(this.CiklusrendCombo);
            this.TabPage5.Controls.Add(this.Üzemek);
            this.TabPage5.Controls.Add(this.Vizsgsorszám);
            this.TabPage5.Controls.Add(this.KMUdátum);
            this.TabPage5.Controls.Add(this.Utolsófelújításdátuma);
            this.TabPage5.Controls.Add(this.Vizsgdátumk);
            this.TabPage5.Controls.Add(this.Vizsgdátumv);
            this.TabPage5.Controls.Add(this.SAP_adatok);
            this.TabPage5.Controls.Add(this.Új_adat);
            this.TabPage5.Controls.Add(this.Utolsó_V_rögzítés);
            this.TabPage5.Controls.Add(this.Label33);
            this.TabPage5.Controls.Add(this.Label32);
            this.TabPage5.Controls.Add(this.Label31);
            this.TabPage5.Controls.Add(this.Label30);
            this.TabPage5.Controls.Add(this.Label29);
            this.TabPage5.Controls.Add(this.Label28);
            this.TabPage5.Controls.Add(this.Label27);
            this.TabPage5.Controls.Add(this.Label26);
            this.TabPage5.Controls.Add(this.Label25);
            this.TabPage5.Controls.Add(this.Label24);
            this.TabPage5.Controls.Add(this.Label23);
            this.TabPage5.Controls.Add(this.Label22);
            this.TabPage5.Controls.Add(this.Label21);
            this.TabPage5.Controls.Add(this.Label20);
            this.TabPage5.Controls.Add(this.Label19);
            this.TabPage5.Controls.Add(this.Label18);
            this.TabPage5.Controls.Add(this.Label17);
            this.TabPage5.Location = new System.Drawing.Point(4, 29);
            this.TabPage5.Name = "TabPage5";
            this.TabPage5.Size = new System.Drawing.Size(1136, 446);
            this.TabPage5.TabIndex = 4;
            this.TabPage5.Text = "Utolsó Vizsgálati adatok";
            // 
            // Töröl
            // 
            this.Töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Töröl.Location = new System.Drawing.Point(1055, 325);
            this.Töröl.Name = "Töröl";
            this.Töröl.Size = new System.Drawing.Size(45, 45);
            this.Töröl.TabIndex = 12;
            this.ToolTip1.SetToolTip(this.Töröl, "Törli az adatsort.");
            this.Töröl.UseVisualStyleBackColor = true;
            this.Töröl.Click += new System.EventHandler(this.Töröl_Click);
            // 
            // KövV2km
            // 
            this.KövV2km.Location = new System.Drawing.Point(670, 360);
            this.KövV2km.Name = "KövV2km";
            this.KövV2km.Size = new System.Drawing.Size(136, 26);
            this.KövV2km.TabIndex = 90;
            // 
            // Label35
            // 
            this.Label35.AutoSize = true;
            this.Label35.BackColor = System.Drawing.Color.OrangeRed;
            this.Label35.Location = new System.Drawing.Point(420, 360);
            this.Label35.Name = "Label35";
            this.Label35.Size = new System.Drawing.Size(147, 20);
            this.Label35.TabIndex = 89;
            this.Label35.Text = "V2-V3-tól futott km:";
            // 
            // KövV2_Sorszám
            // 
            this.KövV2_Sorszám.Location = new System.Drawing.Point(812, 290);
            this.KövV2_Sorszám.Name = "KövV2_Sorszám";
            this.KövV2_Sorszám.Size = new System.Drawing.Size(136, 26);
            this.KövV2_Sorszám.TabIndex = 88;
            // 
            // KövV_Sorszám
            // 
            this.KövV_Sorszám.Location = new System.Drawing.Point(811, 220);
            this.KövV_Sorszám.Name = "KövV_Sorszám";
            this.KövV_Sorszám.Size = new System.Drawing.Size(136, 26);
            this.KövV_Sorszám.TabIndex = 87;
            // 
            // KövV2_számláló
            // 
            this.KövV2_számláló.Location = new System.Drawing.Point(670, 325);
            this.KövV2_számláló.Name = "KövV2_számláló";
            this.KövV2_számláló.Size = new System.Drawing.Size(136, 26);
            this.KövV2_számláló.TabIndex = 8;
            // 
            // KövV2
            // 
            this.KövV2.Location = new System.Drawing.Point(670, 290);
            this.KövV2.Name = "KövV2";
            this.KövV2.Size = new System.Drawing.Size(136, 26);
            this.KövV2.TabIndex = 85;
            // 
            // KövV1km
            // 
            this.KövV1km.Location = new System.Drawing.Point(670, 255);
            this.KövV1km.Name = "KövV1km";
            this.KövV1km.Size = new System.Drawing.Size(136, 26);
            this.KövV1km.TabIndex = 84;
            // 
            // KövV
            // 
            this.KövV.Location = new System.Drawing.Point(670, 220);
            this.KövV.Name = "KövV";
            this.KövV.Size = new System.Drawing.Size(136, 26);
            this.KövV.TabIndex = 83;
            // 
            // Sorszám
            // 
            this.Sorszám.Enabled = false;
            this.Sorszám.Location = new System.Drawing.Point(230, 10);
            this.Sorszám.Name = "Sorszám";
            this.Sorszám.Size = new System.Drawing.Size(136, 26);
            this.Sorszám.TabIndex = 0;
            // 
            // Label34
            // 
            this.Label34.AutoSize = true;
            this.Label34.BackColor = System.Drawing.Color.Silver;
            this.Label34.Location = new System.Drawing.Point(10, 10);
            this.Label34.Name = "Label34";
            this.Label34.Size = new System.Drawing.Size(76, 20);
            this.Label34.TabIndex = 81;
            this.Label34.Text = "Sorszám:";
            // 
            // Jjavszám
            // 
            this.Jjavszám.Location = new System.Drawing.Point(230, 360);
            this.Jjavszám.Name = "Jjavszám";
            this.Jjavszám.Size = new System.Drawing.Size(136, 26);
            this.Jjavszám.TabIndex = 8;
            // 
            // KMUkm
            // 
            this.KMUkm.Location = new System.Drawing.Point(230, 325);
            this.KMUkm.Name = "KMUkm";
            this.KMUkm.Size = new System.Drawing.Size(136, 26);
            this.KMUkm.TabIndex = 7;
            // 
            // VizsgKm
            // 
            this.VizsgKm.Location = new System.Drawing.Point(230, 185);
            this.VizsgKm.Name = "VizsgKm";
            this.VizsgKm.Size = new System.Drawing.Size(136, 26);
            this.VizsgKm.TabIndex = 5;
            // 
            // Vizsgfok
            // 
            this.Vizsgfok.Location = new System.Drawing.Point(230, 42);
            this.Vizsgfok.Name = "Vizsgfok";
            this.Vizsgfok.Size = new System.Drawing.Size(136, 26);
            this.Vizsgfok.TabIndex = 2;
            // 
            // HaviKm
            // 
            this.HaviKm.Location = new System.Drawing.Point(670, 115);
            this.HaviKm.Name = "HaviKm";
            this.HaviKm.Size = new System.Drawing.Size(136, 26);
            this.HaviKm.TabIndex = 12;
            // 
            // TEljesKmText
            // 
            this.TEljesKmText.Location = new System.Drawing.Point(670, 10);
            this.TEljesKmText.Name = "TEljesKmText";
            this.TEljesKmText.Size = new System.Drawing.Size(136, 26);
            this.TEljesKmText.TabIndex = 10;
            // 
            // CiklusrendCombo
            // 
            this.CiklusrendCombo.FormattingEnabled = true;
            this.CiklusrendCombo.Location = new System.Drawing.Point(670, 45);
            this.CiklusrendCombo.Name = "CiklusrendCombo";
            this.CiklusrendCombo.Size = new System.Drawing.Size(136, 28);
            this.CiklusrendCombo.TabIndex = 11;
            this.CiklusrendCombo.SelectedIndexChanged += new System.EventHandler(this.CiklusrendCombo_SelectedIndexChanged);
            // 
            // Üzemek
            // 
            this.Üzemek.FormattingEnabled = true;
            this.Üzemek.Location = new System.Drawing.Point(230, 220);
            this.Üzemek.Name = "Üzemek";
            this.Üzemek.Size = new System.Drawing.Size(136, 28);
            this.Üzemek.TabIndex = 6;
            // 
            // Vizsgsorszám
            // 
            this.Vizsgsorszám.FormattingEnabled = true;
            this.Vizsgsorszám.Location = new System.Drawing.Point(230, 77);
            this.Vizsgsorszám.Name = "Vizsgsorszám";
            this.Vizsgsorszám.Size = new System.Drawing.Size(136, 28);
            this.Vizsgsorszám.TabIndex = 1;
            this.Vizsgsorszám.SelectedIndexChanged += new System.EventHandler(this.Vizsgsorszám_SelectedIndexChanged);
            // 
            // KMUdátum
            // 
            this.KMUdátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.KMUdátum.Location = new System.Drawing.Point(670, 150);
            this.KMUdátum.Name = "KMUdátum";
            this.KMUdátum.Size = new System.Drawing.Size(118, 26);
            this.KMUdátum.TabIndex = 13;
            // 
            // Utolsófelújításdátuma
            // 
            this.Utolsófelújításdátuma.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Utolsófelújításdátuma.Location = new System.Drawing.Point(230, 395);
            this.Utolsófelújításdátuma.Name = "Utolsófelújításdátuma";
            this.Utolsófelújításdátuma.Size = new System.Drawing.Size(118, 26);
            this.Utolsófelújításdátuma.TabIndex = 9;
            // 
            // Vizsgdátumk
            // 
            this.Vizsgdátumk.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Vizsgdátumk.Location = new System.Drawing.Point(230, 115);
            this.Vizsgdátumk.Name = "Vizsgdátumk";
            this.Vizsgdátumk.Size = new System.Drawing.Size(118, 26);
            this.Vizsgdátumk.TabIndex = 3;
            // 
            // Vizsgdátumv
            // 
            this.Vizsgdátumv.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Vizsgdátumv.Location = new System.Drawing.Point(230, 150);
            this.Vizsgdátumv.Name = "Vizsgdátumv";
            this.Vizsgdátumv.Size = new System.Drawing.Size(118, 26);
            this.Vizsgdátumv.TabIndex = 4;
            // 
            // SAP_adatok
            // 
            this.SAP_adatok.BackgroundImage = global::Villamos.Properties.Resources.SAP;
            this.SAP_adatok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SAP_adatok.Location = new System.Drawing.Point(1050, 173);
            this.SAP_adatok.Name = "SAP_adatok";
            this.SAP_adatok.Size = new System.Drawing.Size(50, 50);
            this.SAP_adatok.TabIndex = 11;
            this.ToolTip1.SetToolTip(this.SAP_adatok, "SAP km adatok frissítése");
            this.SAP_adatok.UseVisualStyleBackColor = true;
            this.SAP_adatok.Click += new System.EventHandler(this.SAP_adatok_Click);
            // 
            // Új_adat
            // 
            this.Új_adat.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Új_adat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Új_adat.Location = new System.Drawing.Point(1050, 115);
            this.Új_adat.Name = "Új_adat";
            this.Új_adat.Size = new System.Drawing.Size(50, 50);
            this.Új_adat.TabIndex = 10;
            this.ToolTip1.SetToolTip(this.Új_adat, "Új adatoknak előkészíti a beviteli mezzőket.");
            this.Új_adat.UseVisualStyleBackColor = true;
            this.Új_adat.Click += new System.EventHandler(this.Új_adat_Click);
            // 
            // Utolsó_V_rögzítés
            // 
            this.Utolsó_V_rögzítés.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Utolsó_V_rögzítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Utolsó_V_rögzítés.Location = new System.Drawing.Point(1050, 10);
            this.Utolsó_V_rögzítés.Name = "Utolsó_V_rögzítés";
            this.Utolsó_V_rögzítés.Size = new System.Drawing.Size(50, 50);
            this.Utolsó_V_rögzítés.TabIndex = 9;
            this.ToolTip1.SetToolTip(this.Utolsó_V_rögzítés, "Rögzíti az adatokat");
            this.Utolsó_V_rögzítés.UseVisualStyleBackColor = true;
            this.Utolsó_V_rögzítés.Click += new System.EventHandler(this.Utolsó_V_rögzítés_Click);
            // 
            // Label33
            // 
            this.Label33.AutoSize = true;
            this.Label33.BackColor = System.Drawing.Color.OrangeRed;
            this.Label33.Location = new System.Drawing.Point(420, 220);
            this.Label33.Name = "Label33";
            this.Label33.Size = new System.Drawing.Size(102, 20);
            this.Label33.TabIndex = 16;
            this.Label33.Text = "Következő V:";
            // 
            // Label32
            // 
            this.Label32.AutoSize = true;
            this.Label32.BackColor = System.Drawing.Color.OrangeRed;
            this.Label32.Location = new System.Drawing.Point(420, 290);
            this.Label32.Name = "Label32";
            this.Label32.Size = new System.Drawing.Size(132, 20);
            this.Label32.TabIndex = 15;
            this.Label32.Text = "Következő V2-V3";
            // 
            // Label31
            // 
            this.Label31.AutoSize = true;
            this.Label31.BackColor = System.Drawing.Color.OrangeRed;
            this.Label31.Location = new System.Drawing.Point(420, 255);
            this.Label31.Name = "Label31";
            this.Label31.Size = new System.Drawing.Size(163, 20);
            this.Label31.TabIndex = 14;
            this.Label31.Text = "Utolsó V-től futott km:";
            // 
            // Label30
            // 
            this.Label30.AutoSize = true;
            this.Label30.BackColor = System.Drawing.Color.OrangeRed;
            this.Label30.Location = new System.Drawing.Point(420, 325);
            this.Label30.Name = "Label30";
            this.Label30.Size = new System.Drawing.Size(160, 20);
            this.Label30.TabIndex = 13;
            this.Label30.Text = "V2-V3 számláló állás:";
            // 
            // Label29
            // 
            this.Label29.AutoSize = true;
            this.Label29.BackColor = System.Drawing.Color.Orange;
            this.Label29.Location = new System.Drawing.Point(420, 150);
            this.Label29.Name = "Label29";
            this.Label29.Size = new System.Drawing.Size(174, 20);
            this.Label29.TabIndex = 12;
            this.Label29.Text = "Adatok utolsófrissítése:";
            // 
            // Label28
            // 
            this.Label28.AutoSize = true;
            this.Label28.BackColor = System.Drawing.Color.Orange;
            this.Label28.Location = new System.Drawing.Point(420, 115);
            this.Label28.Name = "Label28";
            this.Label28.Size = new System.Drawing.Size(167, 20);
            this.Label28.TabIndex = 11;
            this.Label28.Text = "Havi futásteljesítmény:";
            // 
            // Label27
            // 
            this.Label27.AutoSize = true;
            this.Label27.BackColor = System.Drawing.Color.DarkKhaki;
            this.Label27.Location = new System.Drawing.Point(420, 45);
            this.Label27.Name = "Label27";
            this.Label27.Size = new System.Drawing.Size(133, 20);
            this.Label27.TabIndex = 10;
            this.Label27.Text = "Ütemezés típusa:";
            // 
            // Label26
            // 
            this.Label26.AutoSize = true;
            this.Label26.BackColor = System.Drawing.Color.DarkKhaki;
            this.Label26.Location = new System.Drawing.Point(420, 10);
            this.Label26.Name = "Label26";
            this.Label26.Size = new System.Drawing.Size(229, 20);
            this.Label26.TabIndex = 9;
            this.Label26.Text = "Üzembehelyezés óta futott km:";
            // 
            // Label25
            // 
            this.Label25.AutoSize = true;
            this.Label25.BackColor = System.Drawing.Color.Salmon;
            this.Label25.Location = new System.Drawing.Point(10, 395);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(180, 20);
            this.Label25.TabIndex = 8;
            this.Label25.Text = "Utolsó Felújítás dátuma:";
            // 
            // Label24
            // 
            this.Label24.AutoSize = true;
            this.Label24.BackColor = System.Drawing.Color.Salmon;
            this.Label24.Location = new System.Drawing.Point(10, 360);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(145, 20);
            this.Label24.TabIndex = 7;
            this.Label24.Text = "Felújítás sorszáma:";
            // 
            // Label23
            // 
            this.Label23.AutoSize = true;
            this.Label23.BackColor = System.Drawing.Color.Salmon;
            this.Label23.Location = new System.Drawing.Point(10, 325);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(211, 20);
            this.Label23.TabIndex = 6;
            this.Label23.Text = "Utolsó felújítás óta futott km:";
            // 
            // Label22
            // 
            this.Label22.AutoSize = true;
            this.Label22.BackColor = System.Drawing.Color.Silver;
            this.Label22.Location = new System.Drawing.Point(10, 45);
            this.Label22.Name = "Label22";
            this.Label22.Size = new System.Drawing.Size(113, 20);
            this.Label22.TabIndex = 5;
            this.Label22.Text = "Vizsgálat foka:";
            // 
            // Label21
            // 
            this.Label21.AutoSize = true;
            this.Label21.BackColor = System.Drawing.Color.Silver;
            this.Label21.Location = new System.Drawing.Point(10, 80);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(151, 20);
            this.Label21.TabIndex = 4;
            this.Label21.Text = "Vizsgálat sorszáma:";
            // 
            // Label20
            // 
            this.Label20.AutoSize = true;
            this.Label20.BackColor = System.Drawing.Color.Silver;
            this.Label20.Location = new System.Drawing.Point(10, 115);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(174, 20);
            this.Label20.TabIndex = 3;
            this.Label20.Text = "Vizsgálat kezdő dátum:";
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.BackColor = System.Drawing.Color.Silver;
            this.Label19.Location = new System.Drawing.Point(10, 150);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(173, 20);
            this.Label19.TabIndex = 2;
            this.Label19.Text = "Vizsgálat végző dátum:";
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.BackColor = System.Drawing.Color.Silver;
            this.Label18.Location = new System.Drawing.Point(10, 185);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(214, 20);
            this.Label18.TabIndex = 1;
            this.Label18.Text = "Vizsgálat km számláló állása:";
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.BackColor = System.Drawing.Color.Silver;
            this.Label17.Location = new System.Drawing.Point(10, 220);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(205, 20);
            this.Label17.TabIndex = 0;
            this.Label17.Text = "Vizsgálatot végző telephely:";
            // 
            // TabPage6
            // 
            this.TabPage6.BackColor = System.Drawing.Color.ForestGreen;
            this.TabPage6.Controls.Add(this.VizsgAdat_Excel);
            this.TabPage6.Controls.Add(this.VizsgAdat_Frissít);
            this.TabPage6.Controls.Add(this.Tábla1);
            this.TabPage6.Location = new System.Drawing.Point(4, 29);
            this.TabPage6.Name = "TabPage6";
            this.TabPage6.Size = new System.Drawing.Size(1136, 446);
            this.TabPage6.TabIndex = 5;
            this.TabPage6.Text = "Vizsgálati adatok";
            // 
            // VizsgAdat_Excel
            // 
            this.VizsgAdat_Excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.VizsgAdat_Excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VizsgAdat_Excel.Location = new System.Drawing.Point(57, 4);
            this.VizsgAdat_Excel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.VizsgAdat_Excel.Name = "VizsgAdat_Excel";
            this.VizsgAdat_Excel.Size = new System.Drawing.Size(45, 45);
            this.VizsgAdat_Excel.TabIndex = 167;
            this.ToolTip1.SetToolTip(this.VizsgAdat_Excel, "Állomány tábla készítése Excel táblába");
            this.VizsgAdat_Excel.UseVisualStyleBackColor = true;
            this.VizsgAdat_Excel.Click += new System.EventHandler(this.VizsgAdat_Excel_Click);
            // 
            // VizsgAdat_Frissít
            // 
            this.VizsgAdat_Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.VizsgAdat_Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VizsgAdat_Frissít.Location = new System.Drawing.Point(6, 4);
            this.VizsgAdat_Frissít.Name = "VizsgAdat_Frissít";
            this.VizsgAdat_Frissít.Size = new System.Drawing.Size(45, 45);
            this.VizsgAdat_Frissít.TabIndex = 166;
            this.ToolTip1.SetToolTip(this.VizsgAdat_Frissít, "Frissíti a lekérdezést");
            this.VizsgAdat_Frissít.UseVisualStyleBackColor = true;
            this.VizsgAdat_Frissít.Click += new System.EventHandler(this.VizsgAdat_Frissít_Click);
            // 
            // Tábla1
            // 
            this.Tábla1.AllowUserToAddRows = false;
            this.Tábla1.AllowUserToDeleteRows = false;
            this.Tábla1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla1.Location = new System.Drawing.Point(5, 54);
            this.Tábla1.Name = "Tábla1";
            this.Tábla1.RowHeadersVisible = false;
            this.Tábla1.Size = new System.Drawing.Size(1128, 389);
            this.Tábla1.TabIndex = 0;
            this.Tábla1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla1_CellClick);
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.TabPage3.Controls.Add(this.Panel7);
            this.TabPage3.Controls.Add(this.Hónapok);
            this.TabPage3.Controls.Add(this.Havikmlabel);
            this.TabPage3.Controls.Add(this.FőHoltart);
            this.TabPage3.Controls.Add(this.AlHoltart);
            this.TabPage3.Controls.Add(this.Command1);
            this.TabPage3.Controls.Add(this.Panel5);
            this.TabPage3.Controls.Add(this.Panel4);
            this.TabPage3.Controls.Add(this.Panel1);
            this.TabPage3.Controls.Add(this.PszJelölő);
            this.TabPage3.Controls.Add(this.Mindentkijelöl);
            this.TabPage3.Controls.Add(this.Kijelöléstörlése);
            this.TabPage3.Controls.Add(this.Kimutatás_más);
            this.TabPage3.Controls.Add(this.Panel6);
            this.TabPage3.Controls.Add(this.Panel3);
            this.TabPage3.Location = new System.Drawing.Point(4, 29);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(1136, 446);
            this.TabPage3.TabIndex = 6;
            this.TabPage3.Text = "Előtervező";
            // 
            // Panel7
            // 
            this.Panel7.BackColor = System.Drawing.Color.Tomato;
            this.Panel7.Controls.Add(this.Kerékcsökkenés);
            this.Panel7.Controls.Add(this.Label39);
            this.Panel7.Location = new System.Drawing.Point(3, 372);
            this.Panel7.Name = "Panel7";
            this.Panel7.Size = new System.Drawing.Size(233, 53);
            this.Panel7.TabIndex = 182;
            // 
            // Kerékcsökkenés
            // 
            this.Kerékcsökkenés.Location = new System.Drawing.Point(136, 23);
            this.Kerékcsökkenés.Name = "Kerékcsökkenés";
            this.Kerékcsökkenés.Size = new System.Drawing.Size(39, 26);
            this.Kerékcsökkenés.TabIndex = 96;
            this.Kerékcsökkenés.Text = "0,5";
            // 
            // Label39
            // 
            this.Label39.AutoSize = true;
            this.Label39.BackColor = System.Drawing.Color.Transparent;
            this.Label39.Location = new System.Drawing.Point(0, 0);
            this.Label39.Name = "Label39";
            this.Label39.Size = new System.Drawing.Size(159, 20);
            this.Label39.TabIndex = 89;
            this.Label39.Text = "Havi kerékcsökkenés";
            // 
            // Hónapok
            // 
            this.Hónapok.Location = new System.Drawing.Point(255, 284);
            this.Hónapok.Name = "Hónapok";
            this.Hónapok.Size = new System.Drawing.Size(47, 26);
            this.Hónapok.TabIndex = 180;
            this.Hónapok.Text = "24";
            this.Hónapok.Visible = false;
            // 
            // Havikmlabel
            // 
            this.Havikmlabel.Location = new System.Drawing.Point(255, 252);
            this.Havikmlabel.Name = "Havikmlabel";
            this.Havikmlabel.Size = new System.Drawing.Size(47, 26);
            this.Havikmlabel.TabIndex = 179;
            this.Havikmlabel.Text = "5000";
            this.Havikmlabel.Visible = false;
            // 
            // FőHoltart
            // 
            this.FőHoltart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FőHoltart.BackColor = System.Drawing.Color.Lime;
            this.FőHoltart.ForeColor = System.Drawing.Color.MediumBlue;
            this.FőHoltart.Location = new System.Drawing.Point(7, 133);
            this.FőHoltart.Name = "FőHoltart";
            this.FőHoltart.Size = new System.Drawing.Size(1120, 20);
            this.FőHoltart.TabIndex = 172;
            this.FőHoltart.Visible = false;
            // 
            // AlHoltart
            // 
            this.AlHoltart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AlHoltart.BackColor = System.Drawing.Color.Lime;
            this.AlHoltart.ForeColor = System.Drawing.Color.MediumBlue;
            this.AlHoltart.Location = new System.Drawing.Point(7, 176);
            this.AlHoltart.Name = "AlHoltart";
            this.AlHoltart.Size = new System.Drawing.Size(1121, 20);
            this.AlHoltart.TabIndex = 173;
            this.AlHoltart.Visible = false;
            // 
            // Command1
            // 
            this.Command1.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Command1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command1.Location = new System.Drawing.Point(639, 5);
            this.Command1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Command1.Name = "Command1";
            this.Command1.Size = new System.Drawing.Size(40, 40);
            this.Command1.TabIndex = 177;
            this.ToolTip1.SetToolTip(this.Command1, "Elkészíti a feltéleknek megfelelő előtervet");
            this.Command1.UseVisualStyleBackColor = true;
            this.Command1.Click += new System.EventHandler(this.Command1_Click);
            // 
            // Panel5
            // 
            this.Panel5.BackColor = System.Drawing.Color.Tomato;
            this.Panel5.Controls.Add(this.Text2);
            this.Panel5.Controls.Add(this.Label38);
            this.Panel5.Location = new System.Drawing.Point(3, 313);
            this.Panel5.Name = "Panel5";
            this.Panel5.Size = new System.Drawing.Size(233, 53);
            this.Panel5.TabIndex = 176;
            // 
            // Text2
            // 
            this.Text2.Location = new System.Drawing.Point(136, 23);
            this.Text2.Name = "Text2";
            this.Text2.Size = new System.Drawing.Size(39, 26);
            this.Text2.TabIndex = 96;
            this.Text2.Text = "24";
            this.Text2.Leave += new System.EventHandler(this.Text2_Leave);
            // 
            // Label38
            // 
            this.Label38.AutoSize = true;
            this.Label38.BackColor = System.Drawing.Color.Transparent;
            this.Label38.Location = new System.Drawing.Point(0, 0);
            this.Label38.Name = "Label38";
            this.Label38.Size = new System.Drawing.Size(199, 20);
            this.Label38.TabIndex = 89;
            this.Label38.Text = "Vizsgált időszak hónapban";
            // 
            // Panel4
            // 
            this.Panel4.BackColor = System.Drawing.Color.Tomato;
            this.Panel4.Controls.Add(this.Option12);
            this.Panel4.Controls.Add(this.Option11);
            this.Panel4.Controls.Add(this.Option10);
            this.Panel4.Controls.Add(this.Label37);
            this.Panel4.Location = new System.Drawing.Point(242, 4);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(233, 122);
            this.Panel4.TabIndex = 176;
            // 
            // Option12
            // 
            this.Option12.AutoSize = true;
            this.Option12.Checked = true;
            this.Option12.Location = new System.Drawing.Point(13, 83);
            this.Option12.Name = "Option12";
            this.Option12.Size = new System.Drawing.Size(207, 24);
            this.Option12.TabIndex = 93;
            this.Option12.TabStop = true;
            this.Option12.Text = "Felső határ átlépése előtt";
            this.Option12.UseVisualStyleBackColor = true;
            // 
            // Option11
            // 
            this.Option11.AutoSize = true;
            this.Option11.Location = new System.Drawing.Point(13, 53);
            this.Option11.Name = "Option11";
            this.Option11.Size = new System.Drawing.Size(200, 24);
            this.Option11.TabIndex = 92;
            this.Option11.Text = "Névleges érték átlépésig";
            this.Option11.UseVisualStyleBackColor = true;
            // 
            // Option10
            // 
            this.Option10.AutoSize = true;
            this.Option10.Location = new System.Drawing.Point(13, 23);
            this.Option10.Name = "Option10";
            this.Option10.Size = new System.Drawing.Size(167, 24);
            this.Option10.TabIndex = 91;
            this.Option10.Text = "Alsó határ átlépésig";
            this.Option10.UseVisualStyleBackColor = true;
            // 
            // Label37
            // 
            this.Label37.AutoSize = true;
            this.Label37.BackColor = System.Drawing.Color.Transparent;
            this.Label37.Location = new System.Drawing.Point(0, 0);
            this.Label37.Name = "Label37";
            this.Label37.Size = new System.Drawing.Size(124, 20);
            this.Label37.TabIndex = 89;
            this.Label37.Text = "Futatási szabály";
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.Tomato;
            this.Panel1.Controls.Add(this.Text1);
            this.Panel1.Controls.Add(this.Option8);
            this.Panel1.Controls.Add(this.Option9);
            this.Panel1.Controls.Add(this.Option7);
            this.Panel1.Controls.Add(this.Option6);
            this.Panel1.Controls.Add(this.Option5);
            this.Panel1.Controls.Add(this.Label36);
            this.Panel1.Location = new System.Drawing.Point(3, 133);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(233, 174);
            this.Panel1.TabIndex = 175;
            // 
            // Text1
            // 
            this.Text1.Location = new System.Drawing.Point(92, 141);
            this.Text1.Name = "Text1";
            this.Text1.Size = new System.Drawing.Size(81, 26);
            this.Text1.TabIndex = 95;
            this.Text1.Text = "5000";
            this.Text1.Leave += new System.EventHandler(this.Text1_Leave);
            // 
            // Option8
            // 
            this.Option8.AutoSize = true;
            this.Option8.Checked = true;
            this.Option8.Location = new System.Drawing.Point(3, 146);
            this.Option8.Name = "Option8";
            this.Option8.Size = new System.Drawing.Size(69, 24);
            this.Option8.TabIndex = 94;
            this.Option8.TabStop = true;
            this.Option8.Text = "Érték:";
            this.Option8.UseVisualStyleBackColor = true;
            this.Option8.Click += new System.EventHandler(this.Option8_Click);
            // 
            // Option9
            // 
            this.Option9.AutoSize = true;
            this.Option9.Location = new System.Drawing.Point(4, 116);
            this.Option9.Name = "Option9";
            this.Option9.Size = new System.Drawing.Size(137, 24);
            this.Option9.TabIndex = 93;
            this.Option9.Text = "Kijelöltek átlaga";
            this.Option9.UseVisualStyleBackColor = true;
            this.Option9.Click += new System.EventHandler(this.Option9_Click);
            // 
            // Option7
            // 
            this.Option7.AutoSize = true;
            this.Option7.Location = new System.Drawing.Point(4, 86);
            this.Option7.Name = "Option7";
            this.Option7.Size = new System.Drawing.Size(104, 24);
            this.Option7.TabIndex = 92;
            this.Option7.Text = "Típus átlag";
            this.Option7.UseVisualStyleBackColor = true;
            this.Option7.Click += new System.EventHandler(this.Option7_Click);
            // 
            // Option6
            // 
            this.Option6.AutoSize = true;
            this.Option6.Location = new System.Drawing.Point(4, 56);
            this.Option6.Name = "Option6";
            this.Option6.Size = new System.Drawing.Size(133, 24);
            this.Option6.TabIndex = 91;
            this.Option6.Text = "Telephely átlag";
            this.Option6.UseVisualStyleBackColor = true;
            this.Option6.Click += new System.EventHandler(this.Option6_Click);
            // 
            // Option5
            // 
            this.Option5.AutoSize = true;
            this.Option5.Location = new System.Drawing.Point(4, 26);
            this.Option5.Name = "Option5";
            this.Option5.Size = new System.Drawing.Size(122, 24);
            this.Option5.TabIndex = 90;
            this.Option5.Text = "Kocsi havi km";
            this.Option5.UseVisualStyleBackColor = true;
            this.Option5.Click += new System.EventHandler(this.Option5_Click);
            // 
            // Label36
            // 
            this.Label36.AutoSize = true;
            this.Label36.BackColor = System.Drawing.Color.Transparent;
            this.Label36.Location = new System.Drawing.Point(0, 0);
            this.Label36.Name = "Label36";
            this.Label36.Size = new System.Drawing.Size(119, 20);
            this.Label36.TabIndex = 89;
            this.Label36.Text = "Havi km beállító";
            // 
            // PszJelölő
            // 
            this.PszJelölő.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PszJelölő.CheckOnClick = true;
            this.PszJelölő.FormattingEnabled = true;
            this.PszJelölő.Location = new System.Drawing.Point(481, 4);
            this.PszJelölő.Name = "PszJelölő";
            this.PszJelölő.Size = new System.Drawing.Size(103, 403);
            this.PszJelölő.TabIndex = 174;
            // 
            // Mindentkijelöl
            // 
            this.Mindentkijelöl.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.Mindentkijelöl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Mindentkijelöl.Location = new System.Drawing.Point(591, 5);
            this.Mindentkijelöl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Mindentkijelöl.Name = "Mindentkijelöl";
            this.Mindentkijelöl.Size = new System.Drawing.Size(40, 40);
            this.Mindentkijelöl.TabIndex = 169;
            this.ToolTip1.SetToolTip(this.Mindentkijelöl, "Mindent kijelöl");
            this.Mindentkijelöl.UseVisualStyleBackColor = true;
            this.Mindentkijelöl.Click += new System.EventHandler(this.Mindentkijelöl_Click);
            // 
            // Kijelöléstörlése
            // 
            this.Kijelöléstörlése.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.Kijelöléstörlése.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Kijelöléstörlése.Location = new System.Drawing.Point(591, 55);
            this.Kijelöléstörlése.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Kijelöléstörlése.Name = "Kijelöléstörlése";
            this.Kijelöléstörlése.Size = new System.Drawing.Size(40, 40);
            this.Kijelöléstörlése.TabIndex = 170;
            this.ToolTip1.SetToolTip(this.Kijelöléstörlése, "Minden kijelölés törlése");
            this.Kijelöléstörlése.UseVisualStyleBackColor = true;
            this.Kijelöléstörlése.Click += new System.EventHandler(this.Kijelöléstörlése_Click);
            // 
            // Kimutatás_más
            // 
            this.Kimutatás_más.BackgroundImage = global::Villamos.Properties.Resources.Aha_Soft_Large_Seo_SEO;
            this.Kimutatás_más.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Kimutatás_más.Location = new System.Drawing.Point(687, 5);
            this.Kimutatás_más.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Kimutatás_más.Name = "Kimutatás_más";
            this.Kimutatás_más.Size = new System.Drawing.Size(40, 40);
            this.Kimutatás_más.TabIndex = 171;
            this.ToolTip1.SetToolTip(this.Kimutatás_más, "Adatbázis adatai alapján készít kimutatást.");
            this.Kimutatás_más.UseVisualStyleBackColor = true;
            this.Kimutatás_más.Click += new System.EventHandler(this.Kimutatás_más_Click);
            // 
            // Panel6
            // 
            this.Panel6.BackColor = System.Drawing.Color.Tomato;
            this.Panel6.Controls.Add(this.Check1);
            this.Panel6.Location = new System.Drawing.Point(3, 73);
            this.Panel6.Name = "Panel6";
            this.Panel6.Size = new System.Drawing.Size(233, 53);
            this.Panel6.TabIndex = 4;
            // 
            // Check1
            // 
            this.Check1.AutoSize = true;
            this.Check1.Location = new System.Drawing.Point(18, 15);
            this.Check1.Name = "Check1";
            this.Check1.Size = new System.Drawing.Size(180, 24);
            this.Check1.TabIndex = 0;
            this.Check1.Text = "Előző futatás marad?";
            this.Check1.UseVisualStyleBackColor = true;
            // 
            // Panel3
            // 
            this.Panel3.BackColor = System.Drawing.Color.Tomato;
            this.Panel3.Controls.Add(this.Command2);
            this.Panel3.Controls.Add(this.Telephely);
            this.Panel3.Controls.Add(this.Label16);
            this.Panel3.Location = new System.Drawing.Point(3, 3);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(233, 64);
            this.Panel3.TabIndex = 1;
            // 
            // Command2
            // 
            this.Command2.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Command2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command2.Location = new System.Drawing.Point(190, 14);
            this.Command2.Name = "Command2";
            this.Command2.Size = new System.Drawing.Size(40, 40);
            this.Command2.TabIndex = 1;
            this.ToolTip1.SetToolTip(this.Command2, "Listázza az előzményeket");
            this.Command2.UseVisualStyleBackColor = true;
            this.Command2.Click += new System.EventHandler(this.Command2_Click);
            // 
            // Telephely
            // 
            this.Telephely.FormattingEnabled = true;
            this.Telephely.Location = new System.Drawing.Point(4, 26);
            this.Telephely.Name = "Telephely";
            this.Telephely.Size = new System.Drawing.Size(151, 28);
            this.Telephely.TabIndex = 0;
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.BackColor = System.Drawing.Color.Transparent;
            this.Label16.Location = new System.Drawing.Point(0, 0);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(76, 20);
            this.Label16.TabIndex = 88;
            this.Label16.Text = "Telephely";
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Location = new System.Drawing.Point(351, 18);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(89, 20);
            this.Label15.TabIndex = 16;
            this.Label15.Text = "Pályaszám:";
            // 
            // Pályaszám
            // 
            this.Pályaszám.FormattingEnabled = true;
            this.Pályaszám.Location = new System.Drawing.Point(446, 16);
            this.Pályaszám.Name = "Pályaszám";
            this.Pályaszám.Size = new System.Drawing.Size(124, 28);
            this.Pályaszám.TabIndex = 16;
            this.Pályaszám.SelectedIndexChanged += new System.EventHandler(this.Pályaszám_SelectedIndexChanged);
            // 
            // Excel_gomb
            // 
            this.Excel_gomb.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excel_gomb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excel_gomb.Location = new System.Drawing.Point(627, 4);
            this.Excel_gomb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Excel_gomb.Name = "Excel_gomb";
            this.Excel_gomb.Size = new System.Drawing.Size(45, 45);
            this.Excel_gomb.TabIndex = 165;
            this.ToolTip1.SetToolTip(this.Excel_gomb, "Állomány tábla készítése Excel táblába");
            this.Excel_gomb.UseVisualStyleBackColor = true;
            this.Excel_gomb.Click += new System.EventHandler(this.Excel_gomb_Click);
            // 
            // Pályaszámkereső
            // 
            this.Pályaszámkereső.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Pályaszámkereső.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Pályaszámkereső.Location = new System.Drawing.Point(576, 4);
            this.Pályaszámkereső.Name = "Pályaszámkereső";
            this.Pályaszámkereső.Size = new System.Drawing.Size(45, 45);
            this.Pályaszámkereső.TabIndex = 63;
            this.ToolTip1.SetToolTip(this.Pályaszámkereső, "Frissíti a lekérdezést");
            this.Pályaszámkereső.UseVisualStyleBackColor = true;
            this.Pályaszámkereső.Click += new System.EventHandler(this.Pályaszámkereső_Click);
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1120, 1);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 60;
            this.ToolTip1.SetToolTip(this.BtnSúgó, "Súgó");
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Ablak_T5C5_Tulajdonság
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.ClientSize = new System.Drawing.Size(1166, 541);
            this.Controls.Add(this.Pályaszám);
            this.Controls.Add(this.Excel_gomb);
            this.Controls.Add(this.Pályaszámkereső);
            this.Controls.Add(this.Label15);
            this.Controls.Add(this.Fülek);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.Panel2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_T5C5_Tulajdonság";
            this.Text = "T5C5 járművek adatai";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_T5C5_Tulajdonság_FormClosed);
            this.Load += new System.EventHandler(this.Tulajdonság_T5C5_Load);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.Fülek.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            this.TabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_lekérdezés)).EndInit();
            this.TabPage5.ResumeLayout(false);
            this.TabPage5.PerformLayout();
            this.TabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla1)).EndInit();
            this.TabPage3.ResumeLayout(false);
            this.TabPage3.PerformLayout();
            this.Panel7.ResumeLayout(false);
            this.Panel7.PerformLayout();
            this.Panel5.ResumeLayout(false);
            this.Panel5.PerformLayout();
            this.Panel4.ResumeLayout(false);
            this.Panel4.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.Panel6.ResumeLayout(false);
            this.Panel6.PerformLayout();
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal Panel Panel2;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal Button BtnSúgó;
        internal TabControl Fülek;
        internal TabPage TabPage1;
        internal Label Típus_text;
        internal Label Státus_text;
        internal Label Miótaáll_text;
        internal Label Szerelvény_text;
        internal Label Vizsgálati_text;
        internal Label Főmérnökség_text;
        internal Label Járműtípus_text;
        internal Label Label8;
        internal Label Label7;
        internal Label Label6;
        internal Label Label5;
        internal Label Label4;
        internal Label Label2;
        internal Label Label1;
        internal TabPage TabPage2;
        internal Label Label14;
        internal Label Label12;
        internal Label Label11;
        internal Label Label10;
        internal Label Label9;
        internal TextBox Futásnap;
        internal TextBox Utolsóvizsgálatszáma;
        internal ComboBox Utolsóvizsgálatfokozata;
        internal DateTimePicker Utolsóvizsgálatdátuma;
        internal DateTimePicker Utolsóforgalminap;
        internal Button Rögzítnap;
        internal Label Label15;
        internal Button Pályaszámkereső;
        internal Button Excel_gomb;
        internal ComboBox Pályaszám;
        internal TabPage TabPage4;
        internal TabPage TabPage5;
        internal TabPage TabPage6;
        internal DataGridView Tábla_lekérdezés;
        internal Button Excellekérdezés;
        internal Button Lekérdezés_lekérdezés;
        internal DataGridView Tábla1;
        internal TextBox Jjavszám;
        internal TextBox KMUkm;
        internal TextBox VizsgKm;
        internal TextBox Vizsgfok;
        internal TextBox HaviKm;
        internal TextBox TEljesKmText;
        internal ComboBox CiklusrendCombo;
        internal ComboBox Üzemek;
        internal ComboBox Vizsgsorszám;
        internal DateTimePicker KMUdátum;
        internal DateTimePicker Utolsófelújításdátuma;
        internal DateTimePicker Vizsgdátumk;
        internal DateTimePicker Vizsgdátumv;
        internal Button SAP_adatok;
        internal Button Új_adat;
        internal Button Utolsó_V_rögzítés;
        internal Label Label33;
        internal Label Label32;
        internal Label Label31;
        internal Label Label30;
        internal Label Label29;
        internal Label Label28;
        internal Label Label27;
        internal Label Label26;
        internal Label Label25;
        internal Label Label24;
        internal Label Label23;
        internal Label Label22;
        internal Label Label21;
        internal Label Label20;
        internal Label Label19;
        internal Label Label18;
        internal Label Label17;
        internal Button Teljes_adatbázis_excel;
        internal ToolTip ToolTip1;
        internal TextBox KövV2;
        internal TextBox KövV1km;
        internal TextBox KövV;
        internal TextBox Sorszám;
        internal Label Label34;
        internal TextBox KövV2_számláló;
        internal TextBox KövV_Sorszám;
        internal TextBox KövV2_Sorszám;
        internal TextBox KövV2km;
        internal Label Label35;
        internal Button Töröl;
        internal TabPage TabPage3;
        internal Panel Panel3;
        internal Button Command2;
        internal ComboBox Telephely;
        internal Label Label16;
        internal Panel Panel6;
        internal CheckBox Check1;
        internal Panel Panel1;
        internal CheckedListBox PszJelölő;
        internal V_MindenEgyéb.MyProgressbar AlHoltart;
        internal V_MindenEgyéb.MyProgressbar FőHoltart;
        internal Button Mindentkijelöl;
        internal Button Kijelöléstörlése;
        internal Button Kimutatás_más;
        internal Button Command1;
        internal Panel Panel5;
        internal TextBox Text2;
        internal Label Label38;
        internal Panel Panel4;
        internal RadioButton Option12;
        internal RadioButton Option11;
        internal RadioButton Option10;
        internal Label Label37;
        internal TextBox Text1;
        internal RadioButton Option8;
        internal RadioButton Option9;
        internal RadioButton Option7;
        internal RadioButton Option6;
        internal RadioButton Option5;
        internal Label Label36;
        internal TextBox Hónapok;
        internal TextBox Havikmlabel;
        internal Panel Panel7;
        internal TextBox Kerékcsökkenés;
        internal Label Label39;
        internal Label Elő_Szerelvény_text;
        internal Label label40;
        internal TableLayoutPanel tableLayoutPanel1;
        internal Label label41;
        internal Button ÜzembehelyezésiPDF;
        internal Timer timer1;
        internal Button VizsgAdat_Excel;
        internal Button VizsgAdat_Frissít;
    }
}