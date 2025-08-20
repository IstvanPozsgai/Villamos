using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
    public partial class Ablak_Főkönyv : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Főkönyv));
            this.Program_adatok = new System.Windows.Forms.Button();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Délutáni = new System.Windows.Forms.RadioButton();
            this.Délelőtt = new System.Windows.Forms.RadioButton();
            this.Zserbeolvasás = new System.Windows.Forms.Button();
            this.ZSERellenőrzés = new System.Windows.Forms.Button();
            this.Főkönyv = new System.Windows.Forms.Button();
            this.Haromnapos = new System.Windows.Forms.Button();
            this.Beállólista = new System.Windows.Forms.Button();
            this.Meghagyás = new System.Windows.Forms.Button();
            this.Dátum = new System.Windows.Forms.DateTimePicker();
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.Label1 = new System.Windows.Forms.Label();
            this.Fülek = new System.Windows.Forms.TabControl();
            this.TabPage6 = new System.Windows.Forms.TabPage();
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.ZSER_másol = new System.Windows.Forms.Button();
            this.ZSER_módosítás = new System.Windows.Forms.Button();
            this.Kereső_hívó = new System.Windows.Forms.Button();
            this.Szerelvénylista_gomb = new System.Windows.Forms.Button();
            this.BtnExcelkimenet = new System.Windows.Forms.Button();
            this.ZSER_tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.ZSER_tábla_idő = new Zuby.ADGV.AdvancedDataGridView();
            this.Időidő = new System.Windows.Forms.DateTimePicker();
            this.Idődátum = new System.Windows.Forms.DateTimePicker();
            this.Idő_frissítés = new System.Windows.Forms.Button();
            this.Kereső_hívó_idő = new System.Windows.Forms.Button();
            this.ZSER_időponti_lista = new System.Windows.Forms.Button();
            this.TabPage7 = new System.Windows.Forms.TabPage();
            this.Óráig = new System.Windows.Forms.DateTimePicker();
            this.Járműpanel_panel = new System.Windows.Forms.GroupBox();
            this.Járműpanel_név = new System.Windows.Forms.Label();
            this.Járműpanel_bezár = new System.Windows.Forms.Button();
            this.Járműpanel_OK = new System.Windows.Forms.Button();
            this.Járműpanel_Text = new System.Windows.Forms.TextBox();
            this.NapiTábla = new Zuby.ADGV.AdvancedDataGridView();
            this.Beálló_Kocsik_Hibái = new System.Windows.Forms.Button();
            this.Button3 = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.N_keres = new System.Windows.Forms.Button();
            this.Jármű_panel_be = new System.Windows.Forms.Button();
            this.Napi_adatok_listázása = new System.Windows.Forms.Button();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.R_frissít = new System.Windows.Forms.Button();
            this.Label19 = new System.Windows.Forms.Label();
            this.Label18 = new System.Windows.Forms.Label();
            this.R_miótaáll = new System.Windows.Forms.DateTimePicker();
            this.R_Státus = new System.Windows.Forms.ComboBox();
            this.R_napszak = new System.Windows.Forms.ComboBox();
            this.R_megjegyzés = new System.Windows.Forms.ComboBox();
            this.R_típus = new System.Windows.Forms.ComboBox();
            this.R_tervindulás = new System.Windows.Forms.DateTimePicker();
            this.R_tervérkezés = new System.Windows.Forms.DateTimePicker();
            this.R_tényérkezés = new System.Windows.Forms.DateTimePicker();
            this.R_tényindulás = new System.Windows.Forms.DateTimePicker();
            this.R_viszonylat = new System.Windows.Forms.TextBox();
            this.R_hibaleírása = new System.Windows.Forms.TextBox();
            this.R_kocsikszáma = new System.Windows.Forms.TextBox();
            this.R_forgalmiszám = new System.Windows.Forms.TextBox();
            this.R_szerelvény = new System.Windows.Forms.TextBox();
            this.R_azonosító = new System.Windows.Forms.Label();
            this.Label16 = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.R_törlés = new System.Windows.Forms.Button();
            this.R_rögzít = new System.Windows.Forms.Button();
            this.TabPage5 = new System.Windows.Forms.TabPage();
            this.GombokPanel = new System.Windows.Forms.Panel();
            this.Button4 = new System.Windows.Forms.Button();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.Reklám_Check = new System.Windows.Forms.CheckBox();
            this.RichtextBox1 = new System.Windows.Forms.RichTextBox();
            this.REklám_frissít = new System.Windows.Forms.Button();
            this.Vezénylésbeírás = new System.Windows.Forms.Button();
            this.TabPage8 = new System.Windows.Forms.TabPage();
            this.Km_tábla = new System.Windows.Forms.DataGridView();
            this.KM_pályaszám = new System.Windows.Forms.TextBox();
            this.KM_dátum_végez = new System.Windows.Forms.DateTimePicker();
            this.KM_dátum_kezd = new System.Windows.Forms.DateTimePicker();
            this.Napi_excel = new System.Windows.Forms.Button();
            this.Km_frissít = new System.Windows.Forms.Button();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Jegykezelő = new System.Windows.Forms.Button();
            this.Takarítás = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Papírméret = new System.Windows.Forms.ComboBox();
            this.PapírElrendezés = new System.Windows.Forms.ComboBox();
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.Panel1.SuspendLayout();
            this.Fülek.SuspendLayout();
            this.TabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ZSER_tábla)).BeginInit();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ZSER_tábla_idő)).BeginInit();
            this.TabPage7.SuspendLayout();
            this.Járműpanel_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NapiTábla)).BeginInit();
            this.TabPage3.SuspendLayout();
            this.TabPage5.SuspendLayout();
            this.TabPage4.SuspendLayout();
            this.TabPage8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Km_tábla)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Program_adatok
            // 
            this.Program_adatok.BackColor = System.Drawing.Color.Yellow;
            this.Program_adatok.Location = new System.Drawing.Point(4, 146);
            this.Program_adatok.Name = "Program_adatok";
            this.Program_adatok.Size = new System.Drawing.Size(210, 35);
            this.Program_adatok.TabIndex = 0;
            this.Program_adatok.Text = "Program adatok fordítása";
            this.Program_adatok.UseVisualStyleBackColor = false;
            this.Program_adatok.Click += new System.EventHandler(this.Program_adatok_Click);
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(3, 19);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(194, 28);
            this.Cmbtelephely.TabIndex = 18;
            this.Cmbtelephely.SelectedIndexChanged += new System.EventHandler(this.Cmbtelephely_SelectedIndexChanged);
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Panel1.Controls.Add(this.Délutáni);
            this.Panel1.Controls.Add(this.Délelőtt);
            this.Panel1.Location = new System.Drawing.Point(5, 110);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(208, 30);
            this.Panel1.TabIndex = 75;
            // 
            // Délutáni
            // 
            this.Délutáni.AutoSize = true;
            this.Délutáni.Location = new System.Drawing.Point(122, 3);
            this.Délutáni.Name = "Délutáni";
            this.Délutáni.Size = new System.Drawing.Size(83, 24);
            this.Délutáni.TabIndex = 1;
            this.Délutáni.TabStop = true;
            this.Délutáni.Text = "Délután";
            this.Délutáni.UseVisualStyleBackColor = true;
            this.Délutáni.Click += new System.EventHandler(this.Délutáni_Click);
            // 
            // Délelőtt
            // 
            this.Délelőtt.AutoSize = true;
            this.Délelőtt.Location = new System.Drawing.Point(8, 3);
            this.Délelőtt.Name = "Délelőtt";
            this.Délelőtt.Size = new System.Drawing.Size(82, 24);
            this.Délelőtt.TabIndex = 0;
            this.Délelőtt.TabStop = true;
            this.Délelőtt.Text = "Délelőtt";
            this.Délelőtt.UseVisualStyleBackColor = true;
            this.Délelőtt.Click += new System.EventHandler(this.Délelőtt_Click);
            // 
            // Zserbeolvasás
            // 
            this.Zserbeolvasás.BackColor = System.Drawing.Color.Yellow;
            this.Zserbeolvasás.Location = new System.Drawing.Point(3, 187);
            this.Zserbeolvasás.Name = "Zserbeolvasás";
            this.Zserbeolvasás.Size = new System.Drawing.Size(210, 35);
            this.Zserbeolvasás.TabIndex = 76;
            this.Zserbeolvasás.Text = "ZSER beolvasás";
            this.Zserbeolvasás.UseVisualStyleBackColor = false;
            this.Zserbeolvasás.Click += new System.EventHandler(this.Zserbeolvasás_Click);
            // 
            // ZSERellenőrzés
            // 
            this.ZSERellenőrzés.BackColor = System.Drawing.Color.Yellow;
            this.ZSERellenőrzés.Location = new System.Drawing.Point(2, 228);
            this.ZSERellenőrzés.Name = "ZSERellenőrzés";
            this.ZSERellenőrzés.Size = new System.Drawing.Size(210, 35);
            this.ZSERellenőrzés.TabIndex = 77;
            this.ZSERellenőrzés.Text = "ZSER adatok összevetése";
            this.ZSERellenőrzés.UseVisualStyleBackColor = false;
            this.ZSERellenőrzés.Click += new System.EventHandler(this.ZSERellenőrzés_Click);
            // 
            // Főkönyv
            // 
            this.Főkönyv.BackColor = System.Drawing.Color.Orange;
            this.Főkönyv.Location = new System.Drawing.Point(2, 269);
            this.Főkönyv.Name = "Főkönyv";
            this.Főkönyv.Size = new System.Drawing.Size(210, 35);
            this.Főkönyv.TabIndex = 78;
            this.Főkönyv.Text = "Főkönyv Excel";
            this.Főkönyv.UseVisualStyleBackColor = false;
            this.Főkönyv.Click += new System.EventHandler(this.Főkönyv_Click);
            // 
            // Haromnapos
            // 
            this.Haromnapos.BackColor = System.Drawing.Color.SpringGreen;
            this.Haromnapos.Location = new System.Drawing.Point(5, 342);
            this.Haromnapos.Name = "Haromnapos";
            this.Haromnapos.Size = new System.Drawing.Size(208, 35);
            this.Haromnapos.TabIndex = 79;
            this.Haromnapos.Text = "T5C5 E2 Nyomtatvány";
            this.Haromnapos.UseVisualStyleBackColor = false;
            this.Haromnapos.Click += new System.EventHandler(this.Haromnapos_Click);
            // 
            // Beállólista
            // 
            this.Beállólista.BackColor = System.Drawing.Color.SpringGreen;
            this.Beállólista.Location = new System.Drawing.Point(5, 424);
            this.Beállólista.Name = "Beállólista";
            this.Beállólista.Size = new System.Drawing.Size(208, 35);
            this.Beállólista.TabIndex = 83;
            this.Beállólista.Text = "Beálló lista";
            this.Beállólista.UseVisualStyleBackColor = false;
            this.Beállólista.Click += new System.EventHandler(this.Beállólista_Click);
            // 
            // Meghagyás
            // 
            this.Meghagyás.BackColor = System.Drawing.Color.SpringGreen;
            this.Meghagyás.Location = new System.Drawing.Point(5, 383);
            this.Meghagyás.Name = "Meghagyás";
            this.Meghagyás.Size = new System.Drawing.Size(208, 35);
            this.Meghagyás.TabIndex = 84;
            this.Meghagyás.Text = "Meghagyás";
            this.Meghagyás.UseVisualStyleBackColor = false;
            this.Meghagyás.Click += new System.EventHandler(this.Meghagyás_Click);
            // 
            // Dátum
            // 
            this.Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum.Location = new System.Drawing.Point(5, 69);
            this.Dátum.Name = "Dátum";
            this.Dátum.Size = new System.Drawing.Size(123, 26);
            this.Dátum.TabIndex = 10;
            this.Dátum.ValueChanged += new System.EventHandler(this.Dátum_ValueChanged);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.BackColor = System.Drawing.Color.Teal;
            this.Holtart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Holtart.Location = new System.Drawing.Point(8, 5);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(1137, 20);
            this.Holtart.Step = 1;
            this.Holtart.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.Holtart.TabIndex = 95;
            this.Holtart.Visible = false;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Red;
            this.Label1.ForeColor = System.Drawing.Color.White;
            this.Label1.Location = new System.Drawing.Point(3, 7);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(107, 20);
            this.Label1.TabIndex = 198;
            this.Label1.Text = "Hibás adatok:";
            // 
            // Fülek
            // 
            this.Fülek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fülek.Controls.Add(this.TabPage6);
            this.Fülek.Controls.Add(this.TabPage1);
            this.Fülek.Controls.Add(this.TabPage2);
            this.Fülek.Controls.Add(this.TabPage7);
            this.Fülek.Controls.Add(this.TabPage3);
            this.Fülek.Controls.Add(this.TabPage5);
            this.Fülek.Controls.Add(this.TabPage4);
            this.Fülek.Controls.Add(this.TabPage8);
            this.Fülek.Location = new System.Drawing.Point(218, 24);
            this.Fülek.Name = "Fülek";
            this.Fülek.Padding = new System.Drawing.Point(20, 3);
            this.Fülek.SelectedIndex = 0;
            this.Fülek.Size = new System.Drawing.Size(939, 580);
            this.Fülek.TabIndex = 200;
            this.Fülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Fülek_DrawItem);
            this.Fülek.SelectedIndexChanged += new System.EventHandler(this.Fülek_SelectedIndexChanged);
            // 
            // TabPage6
            // 
            this.TabPage6.BackColor = System.Drawing.Color.Lime;
            this.TabPage6.Controls.Add(this.Tábla);
            this.TabPage6.Controls.Add(this.Label1);
            this.TabPage6.Location = new System.Drawing.Point(4, 29);
            this.TabPage6.Name = "TabPage6";
            this.TabPage6.Size = new System.Drawing.Size(931, 547);
            this.TabPage6.TabIndex = 5;
            this.TabPage6.Text = "Hiba";
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.Location = new System.Drawing.Point(4, 30);
            this.Tábla.Name = "Tábla";
            this.Tábla.RowHeadersVisible = false;
            this.Tábla.Size = new System.Drawing.Size(923, 500);
            this.Tábla.TabIndex = 199;
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.RoyalBlue;
            this.TabPage1.Controls.Add(this.ZSER_másol);
            this.TabPage1.Controls.Add(this.ZSER_módosítás);
            this.TabPage1.Controls.Add(this.Kereső_hívó);
            this.TabPage1.Controls.Add(this.Szerelvénylista_gomb);
            this.TabPage1.Controls.Add(this.BtnExcelkimenet);
            this.TabPage1.Controls.Add(this.ZSER_tábla);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(931, 547);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Zser adatok";
            // 
            // ZSER_másol
            // 
            this.ZSER_másol.BackgroundImage = global::Villamos.Properties.Resources.Clipboard_Paste_01;
            this.ZSER_másol.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ZSER_másol.Location = new System.Drawing.Point(473, 6);
            this.ZSER_másol.Name = "ZSER_másol";
            this.ZSER_másol.Size = new System.Drawing.Size(40, 40);
            this.ZSER_másol.TabIndex = 215;
            this.ToolTip1.SetToolTip(this.ZSER_másol, "Adatok másolása");
            this.ZSER_másol.UseVisualStyleBackColor = true;
            this.ZSER_másol.Click += new System.EventHandler(this.ZSER_másol_Click);
            // 
            // ZSER_módosítás
            // 
            this.ZSER_módosítás.BackgroundImage = global::Villamos.Properties.Resources.Document_preferences;
            this.ZSER_módosítás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ZSER_módosítás.Location = new System.Drawing.Point(427, 6);
            this.ZSER_módosítás.Name = "ZSER_módosítás";
            this.ZSER_módosítás.Size = new System.Drawing.Size(40, 40);
            this.ZSER_módosítás.TabIndex = 214;
            this.ToolTip1.SetToolTip(this.ZSER_módosítás, "Zser adatok részletes megjelenítése");
            this.ZSER_módosítás.UseVisualStyleBackColor = true;
            this.ZSER_módosítás.Click += new System.EventHandler(this.ZSER_módosítás_Click);
            // 
            // Kereső_hívó
            // 
            this.Kereső_hívó.BackgroundImage = global::Villamos.Properties.Resources.Nagyító;
            this.Kereső_hívó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Kereső_hívó.Location = new System.Drawing.Point(49, 6);
            this.Kereső_hívó.Name = "Kereső_hívó";
            this.Kereső_hívó.Size = new System.Drawing.Size(40, 40);
            this.Kereső_hívó.TabIndex = 205;
            this.ToolTip1.SetToolTip(this.Kereső_hívó, "Keresés a táblázatban");
            this.Kereső_hívó.UseVisualStyleBackColor = true;
            this.Kereső_hívó.Click += new System.EventHandler(this.Kereső_hívó_Click);
            // 
            // Szerelvénylista_gomb
            // 
            this.Szerelvénylista_gomb.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Szerelvénylista_gomb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Szerelvénylista_gomb.Location = new System.Drawing.Point(3, 6);
            this.Szerelvénylista_gomb.Name = "Szerelvénylista_gomb";
            this.Szerelvénylista_gomb.Size = new System.Drawing.Size(40, 40);
            this.Szerelvénylista_gomb.TabIndex = 204;
            this.ToolTip1.SetToolTip(this.Szerelvénylista_gomb, "Listázza a táblázat adatait");
            this.Szerelvénylista_gomb.UseVisualStyleBackColor = true;
            this.Szerelvénylista_gomb.Click += new System.EventHandler(this.Szerelvénylista_gomb_Click);
            // 
            // BtnExcelkimenet
            // 
            this.BtnExcelkimenet.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.BtnExcelkimenet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnExcelkimenet.Location = new System.Drawing.Point(95, 6);
            this.BtnExcelkimenet.Name = "BtnExcelkimenet";
            this.BtnExcelkimenet.Size = new System.Drawing.Size(40, 40);
            this.BtnExcelkimenet.TabIndex = 203;
            this.ToolTip1.SetToolTip(this.BtnExcelkimenet, "A táblázat adatait Excelbe menti");
            this.BtnExcelkimenet.UseVisualStyleBackColor = true;
            this.BtnExcelkimenet.Click += new System.EventHandler(this.BtnExcelkimenet_Click);
            // 
            // ZSER_tábla
            // 
            this.ZSER_tábla.AllowUserToAddRows = false;
            this.ZSER_tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ZSER_tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ZSER_tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ZSER_tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ZSER_tábla.EnableHeadersVisualStyles = false;
            this.ZSER_tábla.FilterAndSortEnabled = true;
            this.ZSER_tábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.ZSER_tábla.Location = new System.Drawing.Point(6, 58);
            this.ZSER_tábla.MaxFilterButtonImageHeight = 23;
            this.ZSER_tábla.Name = "ZSER_tábla";
            this.ZSER_tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ZSER_tábla.RowHeadersVisible = false;
            this.ZSER_tábla.Size = new System.Drawing.Size(919, 483);
            this.ZSER_tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.ZSER_tábla.TabIndex = 218;
            this.ZSER_tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ZSER_tábla_CellClick);
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.RoyalBlue;
            this.TabPage2.Controls.Add(this.ZSER_tábla_idő);
            this.TabPage2.Controls.Add(this.Időidő);
            this.TabPage2.Controls.Add(this.Idődátum);
            this.TabPage2.Controls.Add(this.Idő_frissítés);
            this.TabPage2.Controls.Add(this.Kereső_hívó_idő);
            this.TabPage2.Controls.Add(this.ZSER_időponti_lista);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(931, 547);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "ZSER időponti lekérdezés";
            // 
            // ZSER_tábla_idő
            // 
            this.ZSER_tábla_idő.AllowUserToAddRows = false;
            this.ZSER_tábla_idő.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ZSER_tábla_idő.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.ZSER_tábla_idő.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ZSER_tábla_idő.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ZSER_tábla_idő.EnableHeadersVisualStyles = false;
            this.ZSER_tábla_idő.FilterAndSortEnabled = true;
            this.ZSER_tábla_idő.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.ZSER_tábla_idő.Location = new System.Drawing.Point(5, 49);
            this.ZSER_tábla_idő.MaxFilterButtonImageHeight = 23;
            this.ZSER_tábla_idő.Name = "ZSER_tábla_idő";
            this.ZSER_tábla_idő.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ZSER_tábla_idő.RowHeadersVisible = false;
            this.ZSER_tábla_idő.Size = new System.Drawing.Size(919, 492);
            this.ZSER_tábla_idő.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.ZSER_tábla_idő.TabIndex = 212;
            // 
            // Időidő
            // 
            this.Időidő.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.Időidő.Location = new System.Drawing.Point(117, 6);
            this.Időidő.Name = "Időidő";
            this.Időidő.Size = new System.Drawing.Size(105, 26);
            this.Időidő.TabIndex = 206;
            // 
            // Idődátum
            // 
            this.Idődátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Idődátum.Location = new System.Drawing.Point(6, 6);
            this.Idődátum.Name = "Idődátum";
            this.Idődátum.Size = new System.Drawing.Size(105, 26);
            this.Idődátum.TabIndex = 205;
            // 
            // Idő_frissítés
            // 
            this.Idő_frissítés.BackgroundImage = global::Villamos.Properties.Resources.clock;
            this.Idő_frissítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Idő_frissítés.Location = new System.Drawing.Point(228, 6);
            this.Idő_frissítés.Name = "Idő_frissítés";
            this.Idő_frissítés.Size = new System.Drawing.Size(40, 40);
            this.Idő_frissítés.TabIndex = 211;
            this.ToolTip1.SetToolTip(this.Idő_frissítés, "Dátum és idő aktualizálása");
            this.Idő_frissítés.UseVisualStyleBackColor = true;
            this.Idő_frissítés.Click += new System.EventHandler(this.Idő_frissítés_Click);
            // 
            // Kereső_hívó_idő
            // 
            this.Kereső_hívó_idő.BackgroundImage = global::Villamos.Properties.Resources.Nagyító;
            this.Kereső_hívó_idő.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Kereső_hívó_idő.Location = new System.Drawing.Point(320, 6);
            this.Kereső_hívó_idő.Name = "Kereső_hívó_idő";
            this.Kereső_hívó_idő.Size = new System.Drawing.Size(40, 40);
            this.Kereső_hívó_idő.TabIndex = 210;
            this.ToolTip1.SetToolTip(this.Kereső_hívó_idő, "Keresés a táblázatban");
            this.Kereső_hívó_idő.UseVisualStyleBackColor = true;
            this.Kereső_hívó_idő.Click += new System.EventHandler(this.Kereső_hívó_idő_Click);
            // 
            // ZSER_időponti_lista
            // 
            this.ZSER_időponti_lista.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.ZSER_időponti_lista.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ZSER_időponti_lista.Location = new System.Drawing.Point(274, 6);
            this.ZSER_időponti_lista.Name = "ZSER_időponti_lista";
            this.ZSER_időponti_lista.Size = new System.Drawing.Size(40, 40);
            this.ZSER_időponti_lista.TabIndex = 207;
            this.ToolTip1.SetToolTip(this.ZSER_időponti_lista, "Listázza a táblázat adatait");
            this.ZSER_időponti_lista.UseVisualStyleBackColor = true;
            this.ZSER_időponti_lista.Click += new System.EventHandler(this.ZSER_időponti_lista_Click);
            // 
            // TabPage7
            // 
            this.TabPage7.BackColor = System.Drawing.Color.DarkOrange;
            this.TabPage7.Controls.Add(this.Óráig);
            this.TabPage7.Controls.Add(this.Járműpanel_panel);
            this.TabPage7.Controls.Add(this.NapiTábla);
            this.TabPage7.Controls.Add(this.Beálló_Kocsik_Hibái);
            this.TabPage7.Controls.Add(this.Button3);
            this.TabPage7.Controls.Add(this.Button1);
            this.TabPage7.Controls.Add(this.N_keres);
            this.TabPage7.Controls.Add(this.Jármű_panel_be);
            this.TabPage7.Controls.Add(this.Napi_adatok_listázása);
            this.TabPage7.Location = new System.Drawing.Point(4, 29);
            this.TabPage7.Name = "TabPage7";
            this.TabPage7.Size = new System.Drawing.Size(931, 547);
            this.TabPage7.TabIndex = 6;
            this.TabPage7.Text = "Napi adatok listázása";
            // 
            // Óráig
            // 
            this.Óráig.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.Óráig.Location = new System.Drawing.Point(492, 24);
            this.Óráig.Name = "Óráig";
            this.Óráig.Size = new System.Drawing.Size(105, 26);
            this.Óráig.TabIndex = 218;
            // 
            // Járműpanel_panel
            // 
            this.Járműpanel_panel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Járműpanel_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Járműpanel_panel.Controls.Add(this.Járműpanel_név);
            this.Járműpanel_panel.Controls.Add(this.Járműpanel_bezár);
            this.Járműpanel_panel.Controls.Add(this.Járműpanel_OK);
            this.Járműpanel_panel.Controls.Add(this.Járműpanel_Text);
            this.Járműpanel_panel.Location = new System.Drawing.Point(666, 3);
            this.Járműpanel_panel.Name = "Járműpanel_panel";
            this.Járműpanel_panel.Size = new System.Drawing.Size(241, 54);
            this.Járműpanel_panel.TabIndex = 211;
            this.Járműpanel_panel.TabStop = false;
            this.Járműpanel_panel.Visible = false;
            // 
            // Járműpanel_név
            // 
            this.Járműpanel_név.AutoSize = true;
            this.Járműpanel_név.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Járműpanel_név.Location = new System.Drawing.Point(0, 0);
            this.Járműpanel_név.Name = "Járműpanel_név";
            this.Járműpanel_név.Size = new System.Drawing.Size(135, 20);
            this.Járműpanel_név.TabIndex = 86;
            this.Járműpanel_név.Text = "Jármű hozzáadás";
            // 
            // Járműpanel_bezár
            // 
            this.Járműpanel_bezár.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Járműpanel_bezár.BackgroundImage = global::Villamos.Properties.Resources.bezár;
            this.Járműpanel_bezár.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Járműpanel_bezár.Location = new System.Drawing.Point(206, 10);
            this.Járműpanel_bezár.Margin = new System.Windows.Forms.Padding(4);
            this.Járműpanel_bezár.Name = "Járműpanel_bezár";
            this.Járműpanel_bezár.Size = new System.Drawing.Size(35, 34);
            this.Járműpanel_bezár.TabIndex = 57;
            this.ToolTip1.SetToolTip(this.Járműpanel_bezár, "Bezárja a segédablakot");
            this.Járműpanel_bezár.UseVisualStyleBackColor = true;
            this.Járműpanel_bezár.Click += new System.EventHandler(this.Járműpanel_bezár_Click);
            // 
            // Járműpanel_OK
            // 
            this.Járműpanel_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Járműpanel_OK.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Járműpanel_OK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Járműpanel_OK.Location = new System.Drawing.Point(155, 10);
            this.Járműpanel_OK.Margin = new System.Windows.Forms.Padding(4);
            this.Járműpanel_OK.Name = "Járműpanel_OK";
            this.Járműpanel_OK.Size = new System.Drawing.Size(40, 40);
            this.Járműpanel_OK.TabIndex = 56;
            this.Járműpanel_OK.UseVisualStyleBackColor = true;
            this.Járműpanel_OK.Click += new System.EventHandler(this.Járműpanel_OK_Click);
            // 
            // Járműpanel_Text
            // 
            this.Járműpanel_Text.Location = new System.Drawing.Point(6, 23);
            this.Járműpanel_Text.Name = "Járműpanel_Text";
            this.Járműpanel_Text.Size = new System.Drawing.Size(142, 26);
            this.Járműpanel_Text.TabIndex = 55;
            // 
            // NapiTábla
            // 
            this.NapiTábla.AllowUserToAddRows = false;
            this.NapiTábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.NapiTábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.NapiTábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NapiTábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.NapiTábla.EnableHeadersVisualStyles = false;
            this.NapiTábla.FilterAndSortEnabled = true;
            this.NapiTábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.NapiTábla.Location = new System.Drawing.Point(5, 63);
            this.NapiTábla.MaxFilterButtonImageHeight = 23;
            this.NapiTábla.Name = "NapiTábla";
            this.NapiTábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.NapiTábla.RowHeadersVisible = false;
            this.NapiTábla.Size = new System.Drawing.Size(923, 481);
            this.NapiTábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.NapiTábla.TabIndex = 216;
            this.NapiTábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.NapiTábla_CellClick);
            // 
            // Beálló_Kocsik_Hibái
            // 
            this.Beálló_Kocsik_Hibái.BackgroundImage = global::Villamos.Properties.Resources.Aha_Soft_Large_Seo_SEO;
            this.Beálló_Kocsik_Hibái.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Beálló_Kocsik_Hibái.Location = new System.Drawing.Point(603, 11);
            this.Beálló_Kocsik_Hibái.Name = "Beálló_Kocsik_Hibái";
            this.Beálló_Kocsik_Hibái.Size = new System.Drawing.Size(40, 40);
            this.Beálló_Kocsik_Hibái.TabIndex = 217;
            this.ToolTip1.SetToolTip(this.Beálló_Kocsik_Hibái, "Beálló kocsik hibái");
            this.Beálló_Kocsik_Hibái.UseVisualStyleBackColor = true;
            this.Beálló_Kocsik_Hibái.Click += new System.EventHandler(this.Beálló_Kocsik_Hibái_Click);
            // 
            // Button3
            // 
            this.Button3.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button3.Location = new System.Drawing.Point(97, 11);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(40, 40);
            this.Button3.TabIndex = 215;
            this.ToolTip1.SetToolTip(this.Button3, "A táblázat adatait Excelbe menti");
            this.Button3.UseVisualStyleBackColor = true;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Button1
            // 
            this.Button1.BackgroundImage = global::Villamos.Properties.Resources.Clipboard_Paste_01;
            this.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button1.Location = new System.Drawing.Point(380, 11);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(40, 40);
            this.Button1.TabIndex = 213;
            this.ToolTip1.SetToolTip(this.Button1, "Adatok másolása");
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // N_keres
            // 
            this.N_keres.BackgroundImage = global::Villamos.Properties.Resources.Nagyító;
            this.N_keres.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.N_keres.Location = new System.Drawing.Point(51, 11);
            this.N_keres.Name = "N_keres";
            this.N_keres.Size = new System.Drawing.Size(40, 40);
            this.N_keres.TabIndex = 212;
            this.ToolTip1.SetToolTip(this.N_keres, "Keresés a táblázatban");
            this.N_keres.UseVisualStyleBackColor = true;
            this.N_keres.Click += new System.EventHandler(this.N_keres_Click);
            // 
            // Jármű_panel_be
            // 
            this.Jármű_panel_be.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Jármű_panel_be.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Jármű_panel_be.Location = new System.Drawing.Point(334, 11);
            this.Jármű_panel_be.Name = "Jármű_panel_be";
            this.Jármű_panel_be.Size = new System.Drawing.Size(40, 40);
            this.Jármű_panel_be.TabIndex = 206;
            this.ToolTip1.SetToolTip(this.Jármű_panel_be, "Jármű hozzáadása");
            this.Jármű_panel_be.UseVisualStyleBackColor = true;
            this.Jármű_panel_be.Click += new System.EventHandler(this.Jármű_panel_be_Click);
            // 
            // Napi_adatok_listázása
            // 
            this.Napi_adatok_listázása.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Napi_adatok_listázása.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Napi_adatok_listázása.Location = new System.Drawing.Point(5, 11);
            this.Napi_adatok_listázása.Name = "Napi_adatok_listázása";
            this.Napi_adatok_listázása.Size = new System.Drawing.Size(40, 40);
            this.Napi_adatok_listázása.TabIndex = 205;
            this.ToolTip1.SetToolTip(this.Napi_adatok_listázása, "Listázza a táblázat adatait");
            this.Napi_adatok_listázása.UseVisualStyleBackColor = true;
            this.Napi_adatok_listázása.Click += new System.EventHandler(this.Napi_adatok_listázása_Click);
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.DarkOrange;
            this.TabPage3.Controls.Add(this.R_frissít);
            this.TabPage3.Controls.Add(this.Label19);
            this.TabPage3.Controls.Add(this.Label18);
            this.TabPage3.Controls.Add(this.R_miótaáll);
            this.TabPage3.Controls.Add(this.R_Státus);
            this.TabPage3.Controls.Add(this.R_napszak);
            this.TabPage3.Controls.Add(this.R_megjegyzés);
            this.TabPage3.Controls.Add(this.R_típus);
            this.TabPage3.Controls.Add(this.R_tervindulás);
            this.TabPage3.Controls.Add(this.R_tervérkezés);
            this.TabPage3.Controls.Add(this.R_tényérkezés);
            this.TabPage3.Controls.Add(this.R_tényindulás);
            this.TabPage3.Controls.Add(this.R_viszonylat);
            this.TabPage3.Controls.Add(this.R_hibaleírása);
            this.TabPage3.Controls.Add(this.R_kocsikszáma);
            this.TabPage3.Controls.Add(this.R_forgalmiszám);
            this.TabPage3.Controls.Add(this.R_szerelvény);
            this.TabPage3.Controls.Add(this.R_azonosító);
            this.TabPage3.Controls.Add(this.Label16);
            this.TabPage3.Controls.Add(this.Label15);
            this.TabPage3.Controls.Add(this.Label14);
            this.TabPage3.Controls.Add(this.Label12);
            this.TabPage3.Controls.Add(this.Label11);
            this.TabPage3.Controls.Add(this.Label10);
            this.TabPage3.Controls.Add(this.Label9);
            this.TabPage3.Controls.Add(this.Label8);
            this.TabPage3.Controls.Add(this.Label7);
            this.TabPage3.Controls.Add(this.Label6);
            this.TabPage3.Controls.Add(this.Label5);
            this.TabPage3.Controls.Add(this.Label4);
            this.TabPage3.Controls.Add(this.Label3);
            this.TabPage3.Controls.Add(this.R_törlés);
            this.TabPage3.Controls.Add(this.R_rögzít);
            this.TabPage3.Location = new System.Drawing.Point(4, 29);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(931, 547);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Részletes adatok";
            // 
            // R_frissít
            // 
            this.R_frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.R_frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.R_frissít.Location = new System.Drawing.Point(500, 15);
            this.R_frissít.Name = "R_frissít";
            this.R_frissít.Size = new System.Drawing.Size(45, 45);
            this.R_frissít.TabIndex = 205;
            this.R_frissít.UseVisualStyleBackColor = true;
            this.R_frissít.Click += new System.EventHandler(this.R_frissít_Click);
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.Location = new System.Drawing.Point(15, 480);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(97, 20);
            this.Label19.TabIndex = 29;
            this.Label19.Text = "Megjegyzés:";
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.Location = new System.Drawing.Point(15, 430);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(75, 20);
            this.Label18.TabIndex = 28;
            this.Label18.Text = "Napszak:";
            // 
            // R_miótaáll
            // 
            this.R_miótaáll.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.R_miótaáll.Location = new System.Drawing.Point(150, 390);
            this.R_miótaáll.Name = "R_miótaáll";
            this.R_miótaáll.Size = new System.Drawing.Size(105, 26);
            this.R_miótaáll.TabIndex = 27;
            // 
            // R_Státus
            // 
            this.R_Státus.FormattingEnabled = true;
            this.R_Státus.Location = new System.Drawing.Point(150, 50);
            this.R_Státus.Name = "R_Státus";
            this.R_Státus.Size = new System.Drawing.Size(194, 28);
            this.R_Státus.TabIndex = 26;
            // 
            // R_napszak
            // 
            this.R_napszak.FormattingEnabled = true;
            this.R_napszak.Location = new System.Drawing.Point(150, 430);
            this.R_napszak.Name = "R_napszak";
            this.R_napszak.Size = new System.Drawing.Size(148, 28);
            this.R_napszak.TabIndex = 25;
            // 
            // R_megjegyzés
            // 
            this.R_megjegyzés.FormattingEnabled = true;
            this.R_megjegyzés.Location = new System.Drawing.Point(150, 480);
            this.R_megjegyzés.Name = "R_megjegyzés";
            this.R_megjegyzés.Size = new System.Drawing.Size(150, 28);
            this.R_megjegyzés.TabIndex = 24;
            // 
            // R_típus
            // 
            this.R_típus.FormattingEnabled = true;
            this.R_típus.Location = new System.Drawing.Point(150, 160);
            this.R_típus.Name = "R_típus";
            this.R_típus.Size = new System.Drawing.Size(127, 28);
            this.R_típus.TabIndex = 23;
            // 
            // R_tervindulás
            // 
            this.R_tervindulás.CustomFormat = "yyyy.MM.dd HH:mm:ss";
            this.R_tervindulás.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.R_tervindulás.Location = new System.Drawing.Point(15, 345);
            this.R_tervindulás.Name = "R_tervindulás";
            this.R_tervindulás.Size = new System.Drawing.Size(183, 26);
            this.R_tervindulás.TabIndex = 22;
            // 
            // R_tervérkezés
            // 
            this.R_tervérkezés.CustomFormat = "yyyy.MM.dd HH:mm:ss";
            this.R_tervérkezés.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.R_tervérkezés.Location = new System.Drawing.Point(440, 345);
            this.R_tervérkezés.Name = "R_tervérkezés";
            this.R_tervérkezés.Size = new System.Drawing.Size(183, 26);
            this.R_tervérkezés.TabIndex = 21;
            // 
            // R_tényérkezés
            // 
            this.R_tényérkezés.CustomFormat = "yyyy.MM.dd HH:mm:ss";
            this.R_tényérkezés.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.R_tényérkezés.Location = new System.Drawing.Point(650, 345);
            this.R_tényérkezés.Name = "R_tényérkezés";
            this.R_tényérkezés.Size = new System.Drawing.Size(183, 26);
            this.R_tényérkezés.TabIndex = 20;
            // 
            // R_tényindulás
            // 
            this.R_tényindulás.CustomFormat = "yyyy.MM.dd HH:mm:ss";
            this.R_tényindulás.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.R_tényindulás.Location = new System.Drawing.Point(230, 345);
            this.R_tényindulás.Name = "R_tényindulás";
            this.R_tényindulás.Size = new System.Drawing.Size(183, 26);
            this.R_tényindulás.TabIndex = 19;
            // 
            // R_viszonylat
            // 
            this.R_viszonylat.Location = new System.Drawing.Point(150, 280);
            this.R_viszonylat.Name = "R_viszonylat";
            this.R_viszonylat.Size = new System.Drawing.Size(100, 26);
            this.R_viszonylat.TabIndex = 18;
            // 
            // R_hibaleírása
            // 
            this.R_hibaleírása.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.R_hibaleírása.Location = new System.Drawing.Point(150, 90);
            this.R_hibaleírása.Multiline = true;
            this.R_hibaleírása.Name = "R_hibaleírása";
            this.R_hibaleírása.Size = new System.Drawing.Size(772, 58);
            this.R_hibaleírása.TabIndex = 18;
            // 
            // R_kocsikszáma
            // 
            this.R_kocsikszáma.Location = new System.Drawing.Point(150, 240);
            this.R_kocsikszáma.Name = "R_kocsikszáma";
            this.R_kocsikszáma.Size = new System.Drawing.Size(100, 26);
            this.R_kocsikszáma.TabIndex = 17;
            // 
            // R_forgalmiszám
            // 
            this.R_forgalmiszám.Location = new System.Drawing.Point(450, 280);
            this.R_forgalmiszám.Name = "R_forgalmiszám";
            this.R_forgalmiszám.Size = new System.Drawing.Size(100, 26);
            this.R_forgalmiszám.TabIndex = 16;
            // 
            // R_szerelvény
            // 
            this.R_szerelvény.Location = new System.Drawing.Point(150, 200);
            this.R_szerelvény.Name = "R_szerelvény";
            this.R_szerelvény.Size = new System.Drawing.Size(100, 26);
            this.R_szerelvény.TabIndex = 14;
            // 
            // R_azonosító
            // 
            this.R_azonosító.AutoSize = true;
            this.R_azonosító.Location = new System.Drawing.Point(150, 15);
            this.R_azonosító.Name = "R_azonosító";
            this.R_azonosító.Size = new System.Drawing.Size(99, 20);
            this.R_azonosító.TabIndex = 13;
            this.R_azonosító.Text = "R_azonosító";
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Location = new System.Drawing.Point(15, 50);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(105, 20);
            this.Label16.TabIndex = 12;
            this.Label16.Text = "Jármű státus:";
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Location = new System.Drawing.Point(15, 160);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(51, 20);
            this.Label15.TabIndex = 11;
            this.Label15.Text = "Típus:";
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(15, 200);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(128, 20);
            this.Label14.TabIndex = 10;
            this.Label14.Text = "Szerelvényszám:";
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(15, 280);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(85, 20);
            this.Label12.TabIndex = 9;
            this.Label12.Text = "Viszonylat:";
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(15, 320);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(99, 20);
            this.Label11.TabIndex = 8;
            this.Label11.Text = "Terv Indulás:";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(230, 320);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(101, 20);
            this.Label10.TabIndex = 7;
            this.Label10.Text = "Tény indulás:";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(15, 90);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(96, 20);
            this.Label9.TabIndex = 6;
            this.Label9.Text = "Hiba leírása:";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(15, 240);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(110, 20);
            this.Label8.TabIndex = 5;
            this.Label8.Text = "Kocsik száma:";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(320, 280);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(112, 20);
            this.Label7.TabIndex = 4;
            this.Label7.Text = "Forgalmiszám:";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(440, 320);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(103, 20);
            this.Label6.TabIndex = 3;
            this.Label6.Text = "Terv érkezés:";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(650, 320);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(107, 20);
            this.Label5.TabIndex = 2;
            this.Label5.Text = "Tény érkezés:";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(15, 390);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(71, 20);
            this.Label4.TabIndex = 1;
            this.Label4.Text = "Mióta áll:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(15, 15);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(89, 20);
            this.Label3.TabIndex = 0;
            this.Label3.Text = "Pályaszám:";
            // 
            // R_törlés
            // 
            this.R_törlés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.R_törlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.R_törlés.Location = new System.Drawing.Point(450, 15);
            this.R_törlés.Name = "R_törlés";
            this.R_törlés.Size = new System.Drawing.Size(45, 45);
            this.R_törlés.TabIndex = 97;
            this.R_törlés.UseVisualStyleBackColor = true;
            this.R_törlés.Click += new System.EventHandler(this.R_törlés_Click);
            // 
            // R_rögzít
            // 
            this.R_rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.R_rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.R_rögzít.Location = new System.Drawing.Point(550, 15);
            this.R_rögzít.Name = "R_rögzít";
            this.R_rögzít.Size = new System.Drawing.Size(45, 45);
            this.R_rögzít.TabIndex = 84;
            this.R_rögzít.UseVisualStyleBackColor = true;
            this.R_rögzít.Click += new System.EventHandler(this.R_rögzít_Click);
            // 
            // TabPage5
            // 
            this.TabPage5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.TabPage5.Controls.Add(this.GombokPanel);
            this.TabPage5.Controls.Add(this.Button4);
            this.TabPage5.Location = new System.Drawing.Point(4, 29);
            this.TabPage5.Name = "TabPage5";
            this.TabPage5.Size = new System.Drawing.Size(931, 547);
            this.TabPage5.TabIndex = 7;
            this.TabPage5.Text = "Gombok";
            // 
            // GombokPanel
            // 
            this.GombokPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GombokPanel.AutoScroll = true;
            this.GombokPanel.BackColor = System.Drawing.Color.Tomato;
            this.GombokPanel.Location = new System.Drawing.Point(5, 51);
            this.GombokPanel.Name = "GombokPanel";
            this.GombokPanel.Size = new System.Drawing.Size(921, 484);
            this.GombokPanel.TabIndex = 207;
            // 
            // Button4
            // 
            this.Button4.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button4.Location = new System.Drawing.Point(5, 5);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(40, 40);
            this.Button4.TabIndex = 206;
            this.ToolTip1.SetToolTip(this.Button4, "Listázza a táblázat adatait");
            this.Button4.UseVisualStyleBackColor = true;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.Silver;
            this.TabPage4.Controls.Add(this.Reklám_Check);
            this.TabPage4.Controls.Add(this.RichtextBox1);
            this.TabPage4.Controls.Add(this.REklám_frissít);
            this.TabPage4.Controls.Add(this.Vezénylésbeírás);
            this.TabPage4.Location = new System.Drawing.Point(4, 29);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Size = new System.Drawing.Size(931, 547);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Járműreklám";
            // 
            // Reklám_Check
            // 
            this.Reklám_Check.AutoSize = true;
            this.Reklám_Check.Location = new System.Drawing.Point(95, 9);
            this.Reklám_Check.Name = "Reklám_Check";
            this.Reklám_Check.Size = new System.Drawing.Size(151, 24);
            this.Reklám_Check.TabIndex = 202;
            this.Reklám_Check.Text = "Üzenetet generál";
            this.Reklám_Check.UseVisualStyleBackColor = true;
            // 
            // RichtextBox1
            // 
            this.RichtextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RichtextBox1.Location = new System.Drawing.Point(3, 49);
            this.RichtextBox1.Name = "RichtextBox1";
            this.RichtextBox1.Size = new System.Drawing.Size(923, 485);
            this.RichtextBox1.TabIndex = 201;
            this.RichtextBox1.Text = "";
            // 
            // REklám_frissít
            // 
            this.REklám_frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.REklám_frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.REklám_frissít.Location = new System.Drawing.Point(3, 3);
            this.REklám_frissít.Name = "REklám_frissít";
            this.REklám_frissít.Size = new System.Drawing.Size(40, 40);
            this.REklám_frissít.TabIndex = 200;
            this.REklám_frissít.UseVisualStyleBackColor = true;
            this.REklám_frissít.Click += new System.EventHandler(this.REklám_frissít_Click);
            // 
            // Vezénylésbeírás
            // 
            this.Vezénylésbeírás.BackgroundImage = global::Villamos.Properties.Resources.Document_write;
            this.Vezénylésbeírás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Vezénylésbeírás.Location = new System.Drawing.Point(49, 3);
            this.Vezénylésbeírás.Name = "Vezénylésbeírás";
            this.Vezénylésbeírás.Size = new System.Drawing.Size(40, 40);
            this.Vezénylésbeírás.TabIndex = 199;
            this.Vezénylésbeírás.UseVisualStyleBackColor = true;
            this.Vezénylésbeírás.Visible = false;
            this.Vezénylésbeírás.Click += new System.EventHandler(this.Vezénylésbeírás_Click);
            // 
            // TabPage8
            // 
            this.TabPage8.BackColor = System.Drawing.Color.SandyBrown;
            this.TabPage8.Controls.Add(this.Km_tábla);
            this.TabPage8.Controls.Add(this.KM_pályaszám);
            this.TabPage8.Controls.Add(this.KM_dátum_végez);
            this.TabPage8.Controls.Add(this.KM_dátum_kezd);
            this.TabPage8.Controls.Add(this.Napi_excel);
            this.TabPage8.Controls.Add(this.Km_frissít);
            this.TabPage8.Location = new System.Drawing.Point(4, 29);
            this.TabPage8.Name = "TabPage8";
            this.TabPage8.Size = new System.Drawing.Size(931, 547);
            this.TabPage8.TabIndex = 8;
            this.TabPage8.Text = "Napi km";
            // 
            // Km_tábla
            // 
            this.Km_tábla.AllowUserToAddRows = false;
            this.Km_tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Km_tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.Km_tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Km_tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Km_tábla.EnableHeadersVisualStyles = false;
            this.Km_tábla.Location = new System.Drawing.Point(3, 49);
            this.Km_tábla.Name = "Km_tábla";
            this.Km_tábla.RowHeadersVisible = false;
            this.Km_tábla.Size = new System.Drawing.Size(919, 492);
            this.Km_tábla.TabIndex = 217;
            // 
            // KM_pályaszám
            // 
            this.KM_pályaszám.Location = new System.Drawing.Point(228, 9);
            this.KM_pályaszám.Name = "KM_pályaszám";
            this.KM_pályaszám.Size = new System.Drawing.Size(114, 26);
            this.KM_pályaszám.TabIndex = 213;
            // 
            // KM_dátum_végez
            // 
            this.KM_dátum_végez.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.KM_dátum_végez.Location = new System.Drawing.Point(117, 9);
            this.KM_dátum_végez.Name = "KM_dátum_végez";
            this.KM_dátum_végez.Size = new System.Drawing.Size(105, 26);
            this.KM_dátum_végez.TabIndex = 212;
            // 
            // KM_dátum_kezd
            // 
            this.KM_dátum_kezd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.KM_dátum_kezd.Location = new System.Drawing.Point(6, 9);
            this.KM_dátum_kezd.Name = "KM_dátum_kezd";
            this.KM_dátum_kezd.Size = new System.Drawing.Size(105, 26);
            this.KM_dátum_kezd.TabIndex = 209;
            // 
            // Napi_excel
            // 
            this.Napi_excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Napi_excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Napi_excel.Location = new System.Drawing.Point(422, 3);
            this.Napi_excel.Name = "Napi_excel";
            this.Napi_excel.Size = new System.Drawing.Size(40, 40);
            this.Napi_excel.TabIndex = 216;
            this.ToolTip1.SetToolTip(this.Napi_excel, "A táblázat adatait Excelbe menti");
            this.Napi_excel.UseVisualStyleBackColor = true;
            this.Napi_excel.Click += new System.EventHandler(this.Napi_excel_Click);
            // 
            // Km_frissít
            // 
            this.Km_frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Km_frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Km_frissít.Location = new System.Drawing.Point(376, 4);
            this.Km_frissít.Name = "Km_frissít";
            this.Km_frissít.Size = new System.Drawing.Size(40, 40);
            this.Km_frissít.TabIndex = 210;
            this.ToolTip1.SetToolTip(this.Km_frissít, "Listázza a táblázat adatait");
            this.Km_frissít.UseVisualStyleBackColor = true;
            this.Km_frissít.Click += new System.EventHandler(this.Km_frissít_Click);
            // 
            // ToolTip1
            // 
            this.ToolTip1.IsBalloon = true;
            // 
            // Jegykezelő
            // 
            this.Jegykezelő.BackColor = System.Drawing.Color.SpringGreen;
            this.Jegykezelő.Location = new System.Drawing.Point(5, 506);
            this.Jegykezelő.Name = "Jegykezelő";
            this.Jegykezelő.Size = new System.Drawing.Size(208, 35);
            this.Jegykezelő.TabIndex = 201;
            this.Jegykezelő.Text = "Jegykezelő";
            this.Jegykezelő.UseVisualStyleBackColor = false;
            this.Jegykezelő.Click += new System.EventHandler(this.Jegykezelő_Click);
            // 
            // Takarítás
            // 
            this.Takarítás.BackColor = System.Drawing.Color.SpringGreen;
            this.Takarítás.Location = new System.Drawing.Point(5, 465);
            this.Takarítás.Name = "Takarítás";
            this.Takarítás.Size = new System.Drawing.Size(208, 35);
            this.Takarítás.TabIndex = 202;
            this.Takarítás.Text = "Takarítás";
            this.Takarítás.UseVisualStyleBackColor = false;
            this.Takarítás.Click += new System.EventHandler(this.Takarítás_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Cmbtelephely);
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(207, 54);
            this.groupBox1.TabIndex = 203;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Telephelyi beállítás:";
            // 
            // Papírméret
            // 
            this.Papírméret.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Papírméret.FormattingEnabled = true;
            this.Papírméret.Location = new System.Drawing.Point(5, 547);
            this.Papírméret.Name = "Papírméret";
            this.Papírméret.Size = new System.Drawing.Size(76, 28);
            this.Papírméret.TabIndex = 204;
            // 
            // PapírElrendezés
            // 
            this.PapírElrendezés.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PapírElrendezés.FormattingEnabled = true;
            this.PapírElrendezés.Location = new System.Drawing.Point(87, 547);
            this.PapírElrendezés.Name = "PapírElrendezés";
            this.PapírElrendezés.Size = new System.Drawing.Size(123, 28);
            this.PapírElrendezés.TabIndex = 205;
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(172, 64);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(40, 40);
            this.BtnSúgó.TabIndex = 74;
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // Ablak_Főkönyv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.ClientSize = new System.Drawing.Size(1161, 616);
            this.Controls.Add(this.PapírElrendezés);
            this.Controls.Add(this.Papírméret);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Takarítás);
            this.Controls.Add(this.Jegykezelő);
            this.Controls.Add(this.Fülek);
            this.Controls.Add(this.Dátum);
            this.Controls.Add(this.Meghagyás);
            this.Controls.Add(this.Beállólista);
            this.Controls.Add(this.Haromnapos);
            this.Controls.Add(this.Főkönyv);
            this.Controls.Add(this.ZSERellenőrzés);
            this.Controls.Add(this.Zserbeolvasás);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.Program_adatok);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Ablak_Főkönyv";
            this.Text = "Főkönyv készítés / Napi kiadási adatok létrehozása, módosítása";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_Főkönyv_FormClosed);
            this.Load += new System.EventHandler(this.Ablak_Főkönyv_Load);
            this.Shown += new System.EventHandler(this.Ablak_Főkönyv_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ablak_Jármű_állapotok_KeyDown);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.Fülek.ResumeLayout(false);
            this.TabPage6.ResumeLayout(false);
            this.TabPage6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.TabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ZSER_tábla)).EndInit();
            this.TabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ZSER_tábla_idő)).EndInit();
            this.TabPage7.ResumeLayout(false);
            this.Járműpanel_panel.ResumeLayout(false);
            this.Járműpanel_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NapiTábla)).EndInit();
            this.TabPage3.ResumeLayout(false);
            this.TabPage3.PerformLayout();
            this.TabPage5.ResumeLayout(false);
            this.TabPage4.ResumeLayout(false);
            this.TabPage4.PerformLayout();
            this.TabPage8.ResumeLayout(false);
            this.TabPage8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Km_tábla)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        internal Button Program_adatok;
        internal ComboBox Cmbtelephely;
        internal Button BtnSúgó;
        internal Panel Panel1;
        internal RadioButton Délutáni;
        internal RadioButton Délelőtt;
        internal Button Zserbeolvasás;
        internal Button ZSERellenőrzés;
        internal Button Főkönyv;
        internal Button Haromnapos;
        internal Button Beállólista;
        internal Button Meghagyás;
        internal DateTimePicker Dátum;
        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal Label Label1;
        internal TabControl Fülek;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal TabPage TabPage3;
        internal TabPage TabPage4;
        internal TabPage TabPage6;
        internal DateTimePicker Időidő;
        internal DateTimePicker Idődátum;
        internal Button BtnExcelkimenet;
        internal Button Szerelvénylista_gomb;
        internal Button Kereső_hívó;       
        internal Button ZSER_időponti_lista;
        internal Button Kereső_hívó_idő;
        internal Button Idő_frissítés;
        internal TabPage TabPage7;
        internal Button Napi_adatok_listázása;        
        internal Label Label19;
        internal Label Label18;
        internal DateTimePicker R_miótaáll;
        internal ComboBox R_Státus;
        internal ComboBox R_napszak;
        internal ComboBox R_megjegyzés;
        internal ComboBox R_típus;
        internal DateTimePicker R_tervindulás;
        internal DateTimePicker R_tervérkezés;
        internal DateTimePicker R_tényérkezés;
        internal DateTimePicker R_tényindulás;
        internal TextBox R_viszonylat;
        internal TextBox R_hibaleírása;
        internal TextBox R_kocsikszáma;
        internal TextBox R_forgalmiszám;
        internal TextBox R_szerelvény;
        internal Label R_azonosító;
        internal Label Label16;
        internal Label Label15;
        internal Label Label14;
        internal Label Label12;
        internal Label Label11;
        internal Label Label10;
        internal Label Label9;
        internal Label Label8;
        internal Label Label7;
        internal Label Label6;
        internal Label Label5;
        internal Label Label4;
        internal Label Label3;
        internal Button R_rögzít;
        internal GroupBox Járműpanel_panel;
        internal Label Járműpanel_név;
        internal Button Járműpanel_bezár;
        internal Button Járműpanel_OK;
        internal TextBox Járműpanel_Text;
        internal Button Jármű_panel_be;
        internal Button R_frissít;
        internal Button R_törlés;
        internal Button N_keres;
        internal ToolTip ToolTip1;
        internal RichTextBox RichtextBox1;
        internal Button REklám_frissít;
        internal Button Vezénylésbeírás;
        internal CheckBox Reklám_Check;
        internal Button Jegykezelő;
        internal Button Button1;
        internal Button Button3;
        internal TabPage TabPage5;
        internal Button Button4;
        internal Panel GombokPanel;
        internal Button ZSER_másol;
        internal Button ZSER_módosítás;
        internal Button Takarítás;
        internal TabPage TabPage8;    
        internal Button Km_frissít;
        internal DateTimePicker KM_dátum_kezd;
        internal DateTimePicker KM_dátum_végez;
        internal TextBox KM_pályaszám;
        internal Button Napi_excel;
        public DataGridView Tábla;
        internal Zuby.ADGV.AdvancedDataGridView ZSER_tábla;
        internal Zuby.ADGV.AdvancedDataGridView ZSER_tábla_idő;
        internal Zuby.ADGV.AdvancedDataGridView NapiTábla;
        public DataGridView Km_tábla;
        private Timer timer1;
        private GroupBox groupBox1;
        private ComboBox Papírméret;
        private ComboBox PapírElrendezés;
        internal Button Beálló_Kocsik_Hibái;
        internal DateTimePicker Óráig;
    }
}