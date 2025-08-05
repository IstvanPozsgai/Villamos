using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos.Villamos_Ablakok
{
    partial class Ablak_Nosztalgia : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Nosztalgia));
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Pályaszám = new System.Windows.Forms.ComboBox();
            this.Pályaszámkereső = new System.Windows.Forms.Button();
            this.Label15 = new System.Windows.Forms.Label();
            this.Holtart = new System.Windows.Forms.ProgressBar();
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.Mentés = new System.Windows.Forms.Button();
            this.Kép_szűrés = new System.Windows.Forms.ListBox();
            this.KépTörlés = new System.Windows.Forms.Button();
            this.Kép_megnevezés = new System.Windows.Forms.TextBox();
            this.Kép_Feltöltendő = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.Kép_btn = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.Kép_listbox = new System.Windows.Forms.ListBox();
            this.Kép_Listázás = new System.Windows.Forms.Button();
            this.Kép_rögzít = new System.Windows.Forms.Button();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.Dátum_ütem = new System.Windows.Forms.DateTimePicker();
            this.button2 = new System.Windows.Forms.Button();
            this.Dátum = new System.Windows.Forms.DateTimePicker();
            this.Tábla_lekérdezés = new Zuby.ADGV.AdvancedDataGridView();
            this.Futásnaptábla_Rögzítés = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Nap_azonosító = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.Nap_Dátum = new System.Windows.Forms.DateTimePicker();
            this.Nap_törlés = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Nap_Telephely = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.Napi_Adatok_rögzítése = new System.Windows.Forms.Button();
            this.SAP_Beolv = new System.Windows.Forms.Button();
            this.Lekérdezés_lekérdezés = new System.Windows.Forms.Button();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.Cmb_KmCiklus_V2_Cnév = new System.Windows.Forms.ComboBox();
            this.Txt_V2_dátum = new System.Windows.Forms.DateTimePicker();
            this.Cmb_KmCiklus_V2 = new System.Windows.Forms.ComboBox();
            this.Txt_V2_Kmu = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Txt_V2_Kmv = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Txt_V2_sorszám = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.V_Idő_CiklusRögzít_gom = new System.Windows.Forms.Button();
            this.label45 = new System.Windows.Forms.Label();
            this.Km_group = new System.Windows.Forms.GroupBox();
            this.KM_Alap = new System.Windows.Forms.Panel();
            this.Cmb_KmCiklus_V1_Cnév = new System.Windows.Forms.ComboBox();
            this.Cmb_KmCiklus_V1 = new System.Windows.Forms.ComboBox();
            this.Txt_V1_Kmu = new System.Windows.Forms.TextBox();
            this.Txt_V1_dátum = new System.Windows.Forms.DateTimePicker();
            this.label59 = new System.Windows.Forms.Label();
            this.Txt_V1_Kmv = new System.Windows.Forms.TextBox();
            this.label60 = new System.Windows.Forms.Label();
            this.Txt_V1_sorszám = new System.Windows.Forms.TextBox();
            this.label61 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.V_Km_CiklusRögzít_gomb = new System.Windows.Forms.Button();
            this.label64 = new System.Windows.Forms.Label();
            this.Idő_group = new System.Windows.Forms.GroupBox();
            this.Idő_Alap = new System.Windows.Forms.Panel();
            this.Cmb_FutCiklusE_Cnév = new System.Windows.Forms.ComboBox();
            this.Fut_dátum = new System.Windows.Forms.DateTimePicker();
            this.Cmb_FutCiklusE = new System.Windows.Forms.ComboBox();
            this.Fut_sorszám = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.E_CiklusRögzít_gomb = new System.Windows.Forms.Button();
            this.label51 = new System.Windows.Forms.Label();
            this.Alap_group = new System.Windows.Forms.GroupBox();
            this.Alap = new System.Windows.Forms.Panel();
            this.Fut_nap_text = new System.Windows.Forms.TextBox();
            this.ut_forg_text = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TárH_text = new System.Windows.Forms.TextBox();
            this.TárH_label = new System.Windows.Forms.Label();
            this.LeltSz_text = new System.Windows.Forms.TextBox();
            this.LeltSz_label = new System.Windows.Forms.Label();
            this.EszkSz_text = new System.Windows.Forms.TextBox();
            this.EszkSz_label = new System.Windows.Forms.Label();
            this.Év_text = new System.Windows.Forms.TextBox();
            this.Év_label = new System.Windows.Forms.Label();
            this.Gyártó_text = new System.Windows.Forms.TextBox();
            this.Gyártó_label = new System.Windows.Forms.Label();
            this.Járműtípus_text = new System.Windows.Forms.TextBox();
            this.Főmérnökség_text = new System.Windows.Forms.TextBox();
            this.Takarítás_text = new System.Windows.Forms.TextBox();
            this.Miótaáll_text = new System.Windows.Forms.TextBox();
            this.Státus_text = new System.Windows.Forms.TextBox();
            this.Típus_text = new System.Windows.Forms.TextBox();
            this.alapadatRögzít = new System.Windows.Forms.Button();
            this.Típus_label = new System.Windows.Forms.Label();
            this.Státus_label = new System.Windows.Forms.Label();
            this.Miótaáll_label = new System.Windows.Forms.Label();
            this.Takarítás_label = new System.Windows.Forms.Label();
            this.Főmérnökség_label = new System.Windows.Forms.Label();
            this.Járműtípus_label = new System.Windows.Forms.Label();
            this.Fülek = new System.Windows.Forms.TabControl();
            this.TabPage7 = new System.Windows.Forms.TabPage();
            this.Szűrés = new System.Windows.Forms.ListBox();
            this.PDF_néző = new PdfiumViewer.PdfViewer();
            this.PDF_törlés = new System.Windows.Forms.Button();
            this.PDF_megnevezés = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.Feltöltendő = new System.Windows.Forms.TextBox();
            this.BtnPDF = new System.Windows.Forms.Button();
            this.label43 = new System.Windows.Forms.Label();
            this.Pdf_listbox = new System.Windows.Forms.ListBox();
            this.PDF_Frissít = new System.Windows.Forms.Button();
            this.PDF_rögzít = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.FolderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.saveFileDialog3 = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialog4 = new System.Windows.Forms.SaveFileDialog();
            this.Panel2.SuspendLayout();
            this.tabPage8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.TabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_lekérdezés)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel8.SuspendLayout();
            this.Km_group.SuspendLayout();
            this.KM_Alap.SuspendLayout();
            this.Idő_group.SuspendLayout();
            this.Idő_Alap.SuspendLayout();
            this.Alap_group.SuspendLayout();
            this.Alap.SuspendLayout();
            this.Fülek.SuspendLayout();
            this.TabPage7.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.Cmbtelephely);
            this.Panel2.Controls.Add(this.Label13);
            this.Panel2.Location = new System.Drawing.Point(5, 15);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(335, 33);
            this.Panel2.TabIndex = 168;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(143, 0);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 30);
            this.Cmbtelephely.TabIndex = 18;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(3, 4);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(169, 22);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // Pályaszám
            // 
            this.Pályaszám.FormattingEnabled = true;
            this.Pályaszám.Location = new System.Drawing.Point(442, 15);
            this.Pályaszám.MaxDropDownItems = 5;
            this.Pályaszám.Name = "Pályaszám";
            this.Pályaszám.Size = new System.Drawing.Size(124, 30);
            this.Pályaszám.TabIndex = 166;
            this.Pályaszám.SelectedIndexChanged += new System.EventHandler(this.Pályaszám_SelectedIndexChanged);
            // 
            // Pályaszámkereső
            // 
            this.Pályaszámkereső.Image = ((System.Drawing.Image)(resources.GetObject("Pályaszámkereső.Image")));
            this.Pályaszámkereső.Location = new System.Drawing.Point(572, 5);
            this.Pályaszámkereső.Name = "Pályaszámkereső";
            this.Pályaszámkereső.Size = new System.Drawing.Size(45, 45);
            this.Pályaszámkereső.TabIndex = 178;
            this.Pályaszámkereső.UseVisualStyleBackColor = true;
            this.Pályaszámkereső.Click += new System.EventHandler(this.Pályaszámkereső_Click);
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Location = new System.Drawing.Point(347, 23);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(101, 22);
            this.Label15.TabIndex = 167;
            this.Label15.Text = "Pályaszám:";
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.BackColor = System.Drawing.Color.Aquamarine;
            this.Holtart.Location = new System.Drawing.Point(623, 15);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(660, 28);
            this.Holtart.TabIndex = 170;
            this.Holtart.Visible = false;
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.Image = ((System.Drawing.Image)(resources.GetObject("BtnSúgó.Image")));
            this.BtnSúgó.Location = new System.Drawing.Point(1300, 5);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 169;
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // tabPage8
            // 
            this.tabPage8.BackColor = System.Drawing.Color.DarkTurquoise;
            this.tabPage8.Controls.Add(this.Mentés);
            this.tabPage8.Controls.Add(this.Kép_szűrés);
            this.tabPage8.Controls.Add(this.KépTörlés);
            this.tabPage8.Controls.Add(this.Kép_megnevezés);
            this.tabPage8.Controls.Add(this.Kép_Feltöltendő);
            this.tabPage8.Controls.Add(this.label4);
            this.tabPage8.Controls.Add(this.label12);
            this.tabPage8.Controls.Add(this.Kép_btn);
            this.tabPage8.Controls.Add(this.label14);
            this.tabPage8.Controls.Add(this.PictureBox1);
            this.tabPage8.Controls.Add(this.Kép_listbox);
            this.tabPage8.Controls.Add(this.Kép_Listázás);
            this.tabPage8.Controls.Add(this.Kép_rögzít);
            this.tabPage8.Location = new System.Drawing.Point(4, 31);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(1320, 536);
            this.tabPage8.TabIndex = 9;
            this.tabPage8.Text = "Képek";
            // 
            // Mentés
            // 
            this.Mentés.Image = ((System.Drawing.Image)(resources.GetObject("Mentés.Image")));
            this.Mentés.Location = new System.Drawing.Point(109, 1);
            this.Mentés.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Mentés.Name = "Mentés";
            this.Mentés.Size = new System.Drawing.Size(45, 45);
            this.Mentés.TabIndex = 2;
            this.Mentés.UseVisualStyleBackColor = true;
            this.Mentés.Click += new System.EventHandler(this.Mentés_Click);
            // 
            // Kép_szűrés
            // 
            this.Kép_szűrés.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Kép_szűrés.FormattingEnabled = true;
            this.Kép_szűrés.ItemHeight = 22;
            this.Kép_szűrés.Location = new System.Drawing.Point(445, 74);
            this.Kép_szűrés.Name = "Kép_szűrés";
            this.Kép_szűrés.Size = new System.Drawing.Size(163, 268);
            this.Kép_szűrés.TabIndex = 9;
            this.Kép_szűrés.Visible = false;
            // 
            // KépTörlés
            // 
            this.KépTörlés.Image = ((System.Drawing.Image)(resources.GetObject("KépTörlés.Image")));
            this.KépTörlés.Location = new System.Drawing.Point(269, 74);
            this.KépTörlés.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.KépTörlés.Name = "KépTörlés";
            this.KépTörlés.Size = new System.Drawing.Size(45, 45);
            this.KépTörlés.TabIndex = 5;
            this.KépTörlés.UseVisualStyleBackColor = true;
            this.KépTörlés.Click += new System.EventHandler(this.KépTörlés_Click);
            // 
            // Kép_megnevezés
            // 
            this.Kép_megnevezés.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Kép_megnevezés.Location = new System.Drawing.Point(449, 6);
            this.Kép_megnevezés.MaxLength = 50;
            this.Kép_megnevezés.Name = "Kép_megnevezés";
            this.Kép_megnevezés.Size = new System.Drawing.Size(554, 27);
            this.Kép_megnevezés.TabIndex = 7;
            // 
            // Kép_Feltöltendő
            // 
            this.Kép_Feltöltendő.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Kép_Feltöltendő.Location = new System.Drawing.Point(449, 38);
            this.Kép_Feltöltendő.Name = "Kép_Feltöltendő";
            this.Kép_Feltöltendő.Size = new System.Drawing.Size(554, 27);
            this.Kép_Feltöltendő.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Silver;
            this.label4.Location = new System.Drawing.Point(322, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 22);
            this.label4.TabIndex = 190;
            this.label4.Text = "Megnevezés:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Silver;
            this.label12.Location = new System.Drawing.Point(322, 42);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(137, 22);
            this.label12.TabIndex = 186;
            this.label12.Text = "Feltöltendő fájl :";
            // 
            // Kép_btn
            // 
            this.Kép_btn.Image = ((System.Drawing.Image)(resources.GetObject("Kép_btn.Image")));
            this.Kép_btn.Location = new System.Drawing.Point(213, 4);
            this.Kép_btn.Name = "Kép_btn";
            this.Kép_btn.Size = new System.Drawing.Size(45, 45);
            this.Kép_btn.TabIndex = 3;
            this.Kép_btn.UseVisualStyleBackColor = true;
            this.Kép_btn.Click += new System.EventHandler(this.Kép_btn_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(2, 51);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(137, 22);
            this.label14.TabIndex = 183;
            this.label14.Text = "Feltöltött képek:";
            // 
            // PictureBox1
            // 
            this.PictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureBox1.Location = new System.Drawing.Point(322, 70);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(799, 365);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBox1.TabIndex = 189;
            this.PictureBox1.TabStop = false;
            // 
            // Kép_listbox
            // 
            this.Kép_listbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Kép_listbox.FormattingEnabled = true;
            this.Kép_listbox.ItemHeight = 22;
            this.Kép_listbox.Location = new System.Drawing.Point(3, 74);
            this.Kép_listbox.Name = "Kép_listbox";
            this.Kép_listbox.Size = new System.Drawing.Size(259, 290);
            this.Kép_listbox.TabIndex = 6;
            this.Kép_listbox.SelectedIndexChanged += new System.EventHandler(this.Kép_listbox_SelectedIndexChanged);
            // 
            // Kép_Listázás
            // 
            this.Kép_Listázás.Image = ((System.Drawing.Image)(resources.GetObject("Kép_Listázás.Image")));
            this.Kép_Listázás.Location = new System.Drawing.Point(6, 4);
            this.Kép_Listázás.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Kép_Listázás.Name = "Kép_Listázás";
            this.Kép_Listázás.Size = new System.Drawing.Size(45, 45);
            this.Kép_Listázás.TabIndex = 1;
            this.Kép_Listázás.UseVisualStyleBackColor = true;
            this.Kép_Listázás.Click += new System.EventHandler(this.Kép_Listázás_Click);
            // 
            // Kép_rögzít
            // 
            this.Kép_rögzít.Image = ((System.Drawing.Image)(resources.GetObject("Kép_rögzít.Image")));
            this.Kép_rögzít.Location = new System.Drawing.Point(270, 4);
            this.Kép_rögzít.Name = "Kép_rögzít";
            this.Kép_rögzít.Size = new System.Drawing.Size(45, 45);
            this.Kép_rögzít.TabIndex = 4;
            this.Kép_rögzít.UseVisualStyleBackColor = true;
            this.Kép_rögzít.Click += new System.EventHandler(this.Kép_rögzít_Click);
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.YellowGreen;
            this.TabPage4.Controls.Add(this.Dátum_ütem);
            this.TabPage4.Controls.Add(this.button2);
            this.TabPage4.Controls.Add(this.Dátum);
            this.TabPage4.Controls.Add(this.Tábla_lekérdezés);
            this.TabPage4.Controls.Add(this.Futásnaptábla_Rögzítés);
            this.TabPage4.Controls.Add(this.groupBox1);
            this.TabPage4.Controls.Add(this.SAP_Beolv);
            this.TabPage4.Controls.Add(this.Lekérdezés_lekérdezés);
            this.TabPage4.Location = new System.Drawing.Point(4, 31);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Size = new System.Drawing.Size(1320, 536);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Lekérdezések";
            // 
            // Dátum_ütem
            // 
            this.Dátum_ütem.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum_ütem.Location = new System.Drawing.Point(286, 27);
            this.Dátum_ütem.Name = "Dátum_ütem";
            this.Dátum_ütem.Size = new System.Drawing.Size(109, 27);
            this.Dátum_ütem.TabIndex = 223;
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(138, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(50, 50);
            this.button2.TabIndex = 222;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Dátum
            // 
            this.Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum.Location = new System.Drawing.Point(123, 63);
            this.Dátum.Name = "Dátum";
            this.Dátum.Size = new System.Drawing.Size(109, 27);
            this.Dátum.TabIndex = 221;
            // 
            // Tábla_lekérdezés
            // 
            this.Tábla_lekérdezés.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla_lekérdezés.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla_lekérdezés.FilterAndSortEnabled = true;
            this.Tábla_lekérdezés.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla_lekérdezés.Location = new System.Drawing.Point(4, 95);
            this.Tábla_lekérdezés.MaxFilterButtonImageHeight = 23;
            this.Tábla_lekérdezés.Name = "Tábla_lekérdezés";
            this.Tábla_lekérdezés.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tábla_lekérdezés.RowHeadersWidth = 45;
            this.Tábla_lekérdezés.Size = new System.Drawing.Size(1128, 416);
            this.Tábla_lekérdezés.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla_lekérdezés.TabIndex = 213;
            this.Tábla_lekérdezés.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_lekérdezés_CellClick);
            // 
            // Futásnaptábla_Rögzítés
            // 
            this.Futásnaptábla_Rögzítés.Enabled = false;
            this.Futásnaptábla_Rögzítés.Location = new System.Drawing.Point(4, 59);
            this.Futásnaptábla_Rögzítés.Name = "Futásnaptábla_Rögzítés";
            this.Futásnaptábla_Rögzítés.Size = new System.Drawing.Size(113, 30);
            this.Futásnaptábla_Rögzítés.TabIndex = 212;
            this.Futásnaptábla_Rögzítés.Text = "Futásnap";
            this.Futásnaptábla_Rögzítés.UseVisualStyleBackColor = true;
            this.Futásnaptábla_Rögzítés.Click += new System.EventHandler(this.Futásnaptábla_Rögzítés_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Controls.Add(this.Napi_Adatok_rögzítése);
            this.groupBox1.Location = new System.Drawing.Point(477, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(655, 86);
            this.groupBox1.TabIndex = 211;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Módosítás";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.Nap_azonosító, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label9, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.Nap_Dátum, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.Nap_törlés, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this.label8, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.Nap_Telephely, 5, 1);
            this.tableLayoutPanel2.Controls.Add(this.label11, 5, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 20);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(561, 60);
            this.tableLayoutPanel2.TabIndex = 200;
            // 
            // Nap_azonosító
            // 
            this.Nap_azonosító.Location = new System.Drawing.Point(3, 25);
            this.Nap_azonosító.Name = "Nap_azonosító";
            this.Nap_azonosító.Size = new System.Drawing.Size(128, 27);
            this.Nap_azonosító.TabIndex = 219;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 22);
            this.label7.TabIndex = 214;
            this.label7.Text = "Azonosító";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(137, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 22);
            this.label9.TabIndex = 216;
            this.label9.Text = "Indulás";
            // 
            // Nap_Dátum
            // 
            this.Nap_Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Nap_Dátum.Location = new System.Drawing.Point(137, 25);
            this.Nap_Dátum.Name = "Nap_Dátum";
            this.Nap_Dátum.Size = new System.Drawing.Size(109, 27);
            this.Nap_Dátum.TabIndex = 220;
            // 
            // Nap_törlés
            // 
            this.Nap_törlés.AutoSize = true;
            this.Nap_törlés.Location = new System.Drawing.Point(252, 25);
            this.Nap_törlés.Name = "Nap_törlés";
            this.Nap_törlés.Size = new System.Drawing.Size(76, 26);
            this.Nap_törlés.TabIndex = 213;
            this.Nap_törlés.Text = "Törölt";
            this.Nap_törlés.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(252, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 22);
            this.label8.TabIndex = 194;
            this.label8.Text = "Törölt";
            // 
            // Nap_Telephely
            // 
            this.Nap_Telephely.Location = new System.Drawing.Point(334, 25);
            this.Nap_Telephely.Name = "Nap_Telephely";
            this.Nap_Telephely.Size = new System.Drawing.Size(100, 27);
            this.Nap_Telephely.TabIndex = 223;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(334, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 22);
            this.label11.TabIndex = 224;
            this.label11.Text = "Telephely";
            // 
            // Napi_Adatok_rögzítése
            // 
            this.Napi_Adatok_rögzítése.Image = ((System.Drawing.Image)(resources.GetObject("Napi_Adatok_rögzítése.Image")));
            this.Napi_Adatok_rögzítése.Location = new System.Drawing.Point(588, 26);
            this.Napi_Adatok_rögzítése.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Napi_Adatok_rögzítése.Name = "Napi_Adatok_rögzítése";
            this.Napi_Adatok_rögzítése.Size = new System.Drawing.Size(50, 50);
            this.Napi_Adatok_rögzítése.TabIndex = 212;
            this.Napi_Adatok_rögzítése.UseVisualStyleBackColor = true;
            this.Napi_Adatok_rögzítése.Click += new System.EventHandler(this.Napi_Adatok_rögzítése_Click);
            // 
            // SAP_Beolv
            // 
            this.SAP_Beolv.Image = ((System.Drawing.Image)(resources.GetObject("SAP_Beolv.Image")));
            this.SAP_Beolv.Location = new System.Drawing.Point(67, 3);
            this.SAP_Beolv.Name = "SAP_Beolv";
            this.SAP_Beolv.Size = new System.Drawing.Size(50, 50);
            this.SAP_Beolv.TabIndex = 171;
            this.SAP_Beolv.UseVisualStyleBackColor = true;
            this.SAP_Beolv.Click += new System.EventHandler(this.SAP_Beolv_Click);
            // 
            // Lekérdezés_lekérdezés
            // 
            this.Lekérdezés_lekérdezés.Image = ((System.Drawing.Image)(resources.GetObject("Lekérdezés_lekérdezés.Image")));
            this.Lekérdezés_lekérdezés.Location = new System.Drawing.Point(3, 3);
            this.Lekérdezés_lekérdezés.Name = "Lekérdezés_lekérdezés";
            this.Lekérdezés_lekérdezés.Size = new System.Drawing.Size(50, 50);
            this.Lekérdezés_lekérdezés.TabIndex = 64;
            this.Lekérdezés_lekérdezés.UseVisualStyleBackColor = true;
            this.Lekérdezés_lekérdezés.Click += new System.EventHandler(this.Lekérdezés_lekérdezés_Click);
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.LimeGreen;
            this.TabPage1.Controls.Add(this.button3);
            this.TabPage1.Controls.Add(this.groupBox2);
            this.TabPage1.Controls.Add(this.Km_group);
            this.TabPage1.Controls.Add(this.Idő_group);
            this.TabPage1.Controls.Add(this.Alap_group);
            this.TabPage1.Location = new System.Drawing.Point(4, 31);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1320, 536);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Alapadatok";
            // 
            // button3
            // 
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(868, 239);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(50, 50);
            this.button3.TabIndex = 260;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel8);
            this.groupBox2.Location = new System.Drawing.Point(868, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(433, 230);
            this.groupBox2.TabIndex = 257;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "V - Ciklus - Idő";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.Cmb_KmCiklus_V2_Cnév);
            this.panel8.Controls.Add(this.Txt_V2_dátum);
            this.panel8.Controls.Add(this.Cmb_KmCiklus_V2);
            this.panel8.Controls.Add(this.Txt_V2_Kmu);
            this.panel8.Controls.Add(this.label1);
            this.panel8.Controls.Add(this.Txt_V2_Kmv);
            this.panel8.Controls.Add(this.label2);
            this.panel8.Controls.Add(this.Txt_V2_sorszám);
            this.panel8.Controls.Add(this.label10);
            this.panel8.Controls.Add(this.label41);
            this.panel8.Controls.Add(this.label44);
            this.panel8.Controls.Add(this.V_Idő_CiklusRögzít_gom);
            this.panel8.Controls.Add(this.label45);
            this.panel8.Location = new System.Drawing.Point(11, 29);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(416, 190);
            this.panel8.TabIndex = 255;
            // 
            // Cmb_KmCiklus_V2_Cnév
            // 
            this.Cmb_KmCiklus_V2_Cnév.FormattingEnabled = true;
            this.Cmb_KmCiklus_V2_Cnév.Location = new System.Drawing.Point(165, 2);
            this.Cmb_KmCiklus_V2_Cnév.Name = "Cmb_KmCiklus_V2_Cnév";
            this.Cmb_KmCiklus_V2_Cnév.Size = new System.Drawing.Size(185, 30);
            this.Cmb_KmCiklus_V2_Cnév.TabIndex = 264;
            // 
            // Txt_V2_dátum
            // 
            this.Txt_V2_dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Txt_V2_dátum.Location = new System.Drawing.Point(165, 33);
            this.Txt_V2_dátum.Name = "Txt_V2_dátum";
            this.Txt_V2_dátum.Size = new System.Drawing.Size(185, 27);
            this.Txt_V2_dátum.TabIndex = 263;
            // 
            // Cmb_KmCiklus_V2
            // 
            this.Cmb_KmCiklus_V2.FormattingEnabled = true;
            this.Cmb_KmCiklus_V2.Location = new System.Drawing.Point(165, 62);
            this.Cmb_KmCiklus_V2.Name = "Cmb_KmCiklus_V2";
            this.Cmb_KmCiklus_V2.Size = new System.Drawing.Size(185, 30);
            this.Cmb_KmCiklus_V2.TabIndex = 260;
            // 
            // Txt_V2_Kmu
            // 
            this.Txt_V2_Kmu.BackColor = System.Drawing.Color.LightGreen;
            this.Txt_V2_Kmu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_V2_Kmu.Location = new System.Drawing.Point(165, 153);
            this.Txt_V2_Kmu.Name = "Txt_V2_Kmu";
            this.Txt_V2_Kmu.Size = new System.Drawing.Size(185, 27);
            this.Txt_V2_Kmu.TabIndex = 221;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.LightGreen;
            this.label1.Location = new System.Drawing.Point(4, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 22);
            this.label1.TabIndex = 220;
            this.label1.Text = "Kmu:";
            // 
            // Txt_V2_Kmv
            // 
            this.Txt_V2_Kmv.BackColor = System.Drawing.Color.LightGreen;
            this.Txt_V2_Kmv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_V2_Kmv.Location = new System.Drawing.Point(165, 123);
            this.Txt_V2_Kmv.Name = "Txt_V2_Kmv";
            this.Txt_V2_Kmv.Size = new System.Drawing.Size(185, 27);
            this.Txt_V2_Kmv.TabIndex = 219;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.LightGreen;
            this.label2.Location = new System.Drawing.Point(4, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 22);
            this.label2.TabIndex = 218;
            this.label2.Text = "Km vizsgálatnál:";
            // 
            // Txt_V2_sorszám
            // 
            this.Txt_V2_sorszám.BackColor = System.Drawing.Color.LightGreen;
            this.Txt_V2_sorszám.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_V2_sorszám.Location = new System.Drawing.Point(165, 93);
            this.Txt_V2_sorszám.Name = "Txt_V2_sorszám";
            this.Txt_V2_sorszám.Size = new System.Drawing.Size(185, 27);
            this.Txt_V2_sorszám.TabIndex = 217;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.LightGreen;
            this.label10.Location = new System.Drawing.Point(4, 95);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(168, 22);
            this.label10.TabIndex = 216;
            this.label10.Text = "Vizsgálat sorszáma:";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.BackColor = System.Drawing.Color.LightGreen;
            this.label41.Location = new System.Drawing.Point(4, 65);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(159, 22);
            this.label41.TabIndex = 214;
            this.label41.Text = "Viszgálat fokozata:";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.BackColor = System.Drawing.Color.LightGreen;
            this.label44.Location = new System.Drawing.Point(4, 35);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(151, 22);
            this.label44.TabIndex = 212;
            this.label44.Text = "Vizsgálat dátuma:";
            // 
            // V_Idő_CiklusRögzít_gom
            // 
            this.V_Idő_CiklusRögzít_gom.Image = ((System.Drawing.Image)(resources.GetObject("V_Idő_CiklusRögzít_gom.Image")));
            this.V_Idő_CiklusRögzít_gom.Location = new System.Drawing.Point(358, 5);
            this.V_Idő_CiklusRögzít_gom.Name = "V_Idő_CiklusRögzít_gom";
            this.V_Idő_CiklusRögzít_gom.Size = new System.Drawing.Size(50, 50);
            this.V_Idő_CiklusRögzít_gom.TabIndex = 205;
            this.V_Idő_CiklusRögzít_gom.UseVisualStyleBackColor = true;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.BackColor = System.Drawing.Color.LightGreen;
            this.label45.Location = new System.Drawing.Point(4, 5);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(64, 22);
            this.label45.TabIndex = 204;
            this.label45.Text = "Ciklus:";
            // 
            // Km_group
            // 
            this.Km_group.Controls.Add(this.KM_Alap);
            this.Km_group.Location = new System.Drawing.Point(430, 178);
            this.Km_group.Name = "Km_group";
            this.Km_group.Size = new System.Drawing.Size(432, 227);
            this.Km_group.TabIndex = 257;
            this.Km_group.TabStop = false;
            this.Km_group.Text = "V - Ciklus - Km";
            // 
            // KM_Alap
            // 
            this.KM_Alap.Controls.Add(this.Cmb_KmCiklus_V1_Cnév);
            this.KM_Alap.Controls.Add(this.Cmb_KmCiklus_V1);
            this.KM_Alap.Controls.Add(this.Txt_V1_Kmu);
            this.KM_Alap.Controls.Add(this.Txt_V1_dátum);
            this.KM_Alap.Controls.Add(this.label59);
            this.KM_Alap.Controls.Add(this.Txt_V1_Kmv);
            this.KM_Alap.Controls.Add(this.label60);
            this.KM_Alap.Controls.Add(this.Txt_V1_sorszám);
            this.KM_Alap.Controls.Add(this.label61);
            this.KM_Alap.Controls.Add(this.label62);
            this.KM_Alap.Controls.Add(this.label63);
            this.KM_Alap.Controls.Add(this.V_Km_CiklusRögzít_gomb);
            this.KM_Alap.Controls.Add(this.label64);
            this.KM_Alap.Location = new System.Drawing.Point(6, 30);
            this.KM_Alap.Name = "KM_Alap";
            this.KM_Alap.Size = new System.Drawing.Size(416, 190);
            this.KM_Alap.TabIndex = 254;
            // 
            // Cmb_KmCiklus_V1_Cnév
            // 
            this.Cmb_KmCiklus_V1_Cnév.FormattingEnabled = true;
            this.Cmb_KmCiklus_V1_Cnév.Location = new System.Drawing.Point(164, 2);
            this.Cmb_KmCiklus_V1_Cnév.Name = "Cmb_KmCiklus_V1_Cnév";
            this.Cmb_KmCiklus_V1_Cnév.Size = new System.Drawing.Size(185, 30);
            this.Cmb_KmCiklus_V1_Cnév.TabIndex = 262;
            // 
            // Cmb_KmCiklus_V1
            // 
            this.Cmb_KmCiklus_V1.FormattingEnabled = true;
            this.Cmb_KmCiklus_V1.Location = new System.Drawing.Point(165, 61);
            this.Cmb_KmCiklus_V1.Name = "Cmb_KmCiklus_V1";
            this.Cmb_KmCiklus_V1.Size = new System.Drawing.Size(185, 30);
            this.Cmb_KmCiklus_V1.TabIndex = 259;
            // 
            // Txt_V1_Kmu
            // 
            this.Txt_V1_Kmu.BackColor = System.Drawing.Color.LightGreen;
            this.Txt_V1_Kmu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_V1_Kmu.Location = new System.Drawing.Point(165, 153);
            this.Txt_V1_Kmu.Name = "Txt_V1_Kmu";
            this.Txt_V1_Kmu.Size = new System.Drawing.Size(185, 27);
            this.Txt_V1_Kmu.TabIndex = 221;
            // 
            // Txt_V1_dátum
            // 
            this.Txt_V1_dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Txt_V1_dátum.Location = new System.Drawing.Point(165, 33);
            this.Txt_V1_dátum.Name = "Txt_V1_dátum";
            this.Txt_V1_dátum.Size = new System.Drawing.Size(185, 27);
            this.Txt_V1_dátum.TabIndex = 261;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.BackColor = System.Drawing.Color.LightGreen;
            this.label59.Location = new System.Drawing.Point(4, 155);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(51, 22);
            this.label59.TabIndex = 220;
            this.label59.Text = "Kmu:";
            // 
            // Txt_V1_Kmv
            // 
            this.Txt_V1_Kmv.BackColor = System.Drawing.Color.LightGreen;
            this.Txt_V1_Kmv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_V1_Kmv.Location = new System.Drawing.Point(165, 123);
            this.Txt_V1_Kmv.Name = "Txt_V1_Kmv";
            this.Txt_V1_Kmv.Size = new System.Drawing.Size(185, 27);
            this.Txt_V1_Kmv.TabIndex = 219;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.BackColor = System.Drawing.Color.LightGreen;
            this.label60.Location = new System.Drawing.Point(4, 125);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(139, 22);
            this.label60.TabIndex = 218;
            this.label60.Text = "Km vizsgálatnál:";
            // 
            // Txt_V1_sorszám
            // 
            this.Txt_V1_sorszám.BackColor = System.Drawing.Color.LightGreen;
            this.Txt_V1_sorszám.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_V1_sorszám.Location = new System.Drawing.Point(165, 93);
            this.Txt_V1_sorszám.Name = "Txt_V1_sorszám";
            this.Txt_V1_sorszám.Size = new System.Drawing.Size(185, 27);
            this.Txt_V1_sorszám.TabIndex = 217;
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.BackColor = System.Drawing.Color.LightGreen;
            this.label61.Location = new System.Drawing.Point(4, 95);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(168, 22);
            this.label61.TabIndex = 216;
            this.label61.Text = "Vizsgálat sorszáma:";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.BackColor = System.Drawing.Color.LightGreen;
            this.label62.Location = new System.Drawing.Point(4, 65);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(159, 22);
            this.label62.TabIndex = 214;
            this.label62.Text = "Viszgálat fokozata:";
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.BackColor = System.Drawing.Color.LightGreen;
            this.label63.Location = new System.Drawing.Point(4, 35);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(151, 22);
            this.label63.TabIndex = 212;
            this.label63.Text = "Vizsgálat dátuma:";
            // 
            // V_Km_CiklusRögzít_gomb
            // 
            this.V_Km_CiklusRögzít_gomb.Image = ((System.Drawing.Image)(resources.GetObject("V_Km_CiklusRögzít_gomb.Image")));
            this.V_Km_CiklusRögzít_gomb.Location = new System.Drawing.Point(358, 5);
            this.V_Km_CiklusRögzít_gomb.Name = "V_Km_CiklusRögzít_gomb";
            this.V_Km_CiklusRögzít_gomb.Size = new System.Drawing.Size(50, 50);
            this.V_Km_CiklusRögzít_gomb.TabIndex = 205;
            this.V_Km_CiklusRögzít_gomb.UseVisualStyleBackColor = true;
            this.V_Km_CiklusRögzít_gomb.Click += new System.EventHandler(this.Km_Rögzít_Click);
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.BackColor = System.Drawing.Color.LightGreen;
            this.label64.Location = new System.Drawing.Point(4, 5);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(64, 22);
            this.label64.TabIndex = 204;
            this.label64.Text = "Ciklus:";
            // 
            // Idő_group
            // 
            this.Idő_group.Controls.Add(this.Idő_Alap);
            this.Idő_group.Location = new System.Drawing.Point(429, 3);
            this.Idő_group.Name = "Idő_group";
            this.Idő_group.Size = new System.Drawing.Size(433, 176);
            this.Idő_group.TabIndex = 256;
            this.Idő_group.TabStop = false;
            this.Idő_group.Text = "E - Ciklus";
            // 
            // Idő_Alap
            // 
            this.Idő_Alap.Controls.Add(this.Cmb_FutCiklusE_Cnév);
            this.Idő_Alap.Controls.Add(this.Fut_dátum);
            this.Idő_Alap.Controls.Add(this.Cmb_FutCiklusE);
            this.Idő_Alap.Controls.Add(this.Fut_sorszám);
            this.Idő_Alap.Controls.Add(this.label48);
            this.Idő_Alap.Controls.Add(this.label49);
            this.Idő_Alap.Controls.Add(this.label50);
            this.Idő_Alap.Controls.Add(this.E_CiklusRögzít_gomb);
            this.Idő_Alap.Controls.Add(this.label51);
            this.Idő_Alap.Location = new System.Drawing.Point(6, 29);
            this.Idő_Alap.Name = "Idő_Alap";
            this.Idő_Alap.Size = new System.Drawing.Size(416, 133);
            this.Idő_Alap.TabIndex = 254;
            // 
            // Cmb_FutCiklusE_Cnév
            // 
            this.Cmb_FutCiklusE_Cnév.FormattingEnabled = true;
            this.Cmb_FutCiklusE_Cnév.Location = new System.Drawing.Point(165, 2);
            this.Cmb_FutCiklusE_Cnév.Name = "Cmb_FutCiklusE_Cnév";
            this.Cmb_FutCiklusE_Cnév.Size = new System.Drawing.Size(185, 30);
            this.Cmb_FutCiklusE_Cnév.TabIndex = 263;
            // 
            // Fut_dátum
            // 
            this.Fut_dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Fut_dátum.Location = new System.Drawing.Point(165, 33);
            this.Fut_dátum.Name = "Fut_dátum";
            this.Fut_dátum.Size = new System.Drawing.Size(185, 27);
            this.Fut_dátum.TabIndex = 262;
            // 
            // Cmb_FutCiklusE
            // 
            this.Cmb_FutCiklusE.FormattingEnabled = true;
            this.Cmb_FutCiklusE.Location = new System.Drawing.Point(165, 62);
            this.Cmb_FutCiklusE.Name = "Cmb_FutCiklusE";
            this.Cmb_FutCiklusE.Size = new System.Drawing.Size(185, 30);
            this.Cmb_FutCiklusE.TabIndex = 258;
            // 
            // Fut_sorszám
            // 
            this.Fut_sorszám.BackColor = System.Drawing.Color.LightGreen;
            this.Fut_sorszám.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Fut_sorszám.Location = new System.Drawing.Point(165, 93);
            this.Fut_sorszám.Name = "Fut_sorszám";
            this.Fut_sorszám.Size = new System.Drawing.Size(185, 27);
            this.Fut_sorszám.TabIndex = 217;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.BackColor = System.Drawing.Color.LightGreen;
            this.label48.Location = new System.Drawing.Point(4, 95);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(168, 22);
            this.label48.TabIndex = 216;
            this.label48.Text = "Vizsgálat sorszáma:";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.BackColor = System.Drawing.Color.LightGreen;
            this.label49.Location = new System.Drawing.Point(4, 65);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(159, 22);
            this.label49.TabIndex = 214;
            this.label49.Text = "Vizsgálat fokozata:";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.BackColor = System.Drawing.Color.LightGreen;
            this.label50.Location = new System.Drawing.Point(4, 35);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(151, 22);
            this.label50.TabIndex = 212;
            this.label50.Text = "Vizsgálat dátuma:";
            // 
            // E_CiklusRögzít_gomb
            // 
            this.E_CiklusRögzít_gomb.Image = ((System.Drawing.Image)(resources.GetObject("E_CiklusRögzít_gomb.Image")));
            this.E_CiklusRögzít_gomb.Location = new System.Drawing.Point(358, 5);
            this.E_CiklusRögzít_gomb.Name = "E_CiklusRögzít_gomb";
            this.E_CiklusRögzít_gomb.Size = new System.Drawing.Size(50, 50);
            this.E_CiklusRögzít_gomb.TabIndex = 205;
            this.E_CiklusRögzít_gomb.UseVisualStyleBackColor = true;
            this.E_CiklusRögzít_gomb.Click += new System.EventHandler(this.Futás_Rögzít_Click);
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.BackColor = System.Drawing.Color.LightGreen;
            this.label51.Location = new System.Drawing.Point(4, 5);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(64, 22);
            this.label51.TabIndex = 204;
            this.label51.Text = "Ciklus:";
            // 
            // Alap_group
            // 
            this.Alap_group.Controls.Add(this.Alap);
            this.Alap_group.Location = new System.Drawing.Point(2, 3);
            this.Alap_group.Name = "Alap_group";
            this.Alap_group.Size = new System.Drawing.Size(422, 430);
            this.Alap_group.TabIndex = 255;
            this.Alap_group.TabStop = false;
            this.Alap_group.Text = "Alapadatok";
            // 
            // Alap
            // 
            this.Alap.Controls.Add(this.Fut_nap_text);
            this.Alap.Controls.Add(this.ut_forg_text);
            this.Alap.Controls.Add(this.label3);
            this.Alap.Controls.Add(this.label6);
            this.Alap.Controls.Add(this.TárH_text);
            this.Alap.Controls.Add(this.TárH_label);
            this.Alap.Controls.Add(this.LeltSz_text);
            this.Alap.Controls.Add(this.LeltSz_label);
            this.Alap.Controls.Add(this.EszkSz_text);
            this.Alap.Controls.Add(this.EszkSz_label);
            this.Alap.Controls.Add(this.Év_text);
            this.Alap.Controls.Add(this.Év_label);
            this.Alap.Controls.Add(this.Gyártó_text);
            this.Alap.Controls.Add(this.Gyártó_label);
            this.Alap.Controls.Add(this.Járműtípus_text);
            this.Alap.Controls.Add(this.Főmérnökség_text);
            this.Alap.Controls.Add(this.Takarítás_text);
            this.Alap.Controls.Add(this.Miótaáll_text);
            this.Alap.Controls.Add(this.Státus_text);
            this.Alap.Controls.Add(this.Típus_text);
            this.Alap.Controls.Add(this.alapadatRögzít);
            this.Alap.Controls.Add(this.Típus_label);
            this.Alap.Controls.Add(this.Státus_label);
            this.Alap.Controls.Add(this.Miótaáll_label);
            this.Alap.Controls.Add(this.Takarítás_label);
            this.Alap.Controls.Add(this.Főmérnökség_label);
            this.Alap.Controls.Add(this.Járműtípus_label);
            this.Alap.Location = new System.Drawing.Point(1, 25);
            this.Alap.Name = "Alap";
            this.Alap.Size = new System.Drawing.Size(416, 399);
            this.Alap.TabIndex = 253;
            // 
            // Fut_nap_text
            // 
            this.Fut_nap_text.BackColor = System.Drawing.Color.LightGreen;
            this.Fut_nap_text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Fut_nap_text.Location = new System.Drawing.Point(165, 363);
            this.Fut_nap_text.Name = "Fut_nap_text";
            this.Fut_nap_text.Size = new System.Drawing.Size(187, 27);
            this.Fut_nap_text.TabIndex = 225;
            // 
            // ut_forg_text
            // 
            this.ut_forg_text.BackColor = System.Drawing.Color.LightGreen;
            this.ut_forg_text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ut_forg_text.Location = new System.Drawing.Point(165, 333);
            this.ut_forg_text.Name = "ut_forg_text";
            this.ut_forg_text.Size = new System.Drawing.Size(187, 27);
            this.ut_forg_text.TabIndex = 224;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.LightGreen;
            this.label3.Location = new System.Drawing.Point(4, 335);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(169, 22);
            this.label3.TabIndex = 223;
            this.label3.Text = "Utolsó forgalmi nap:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.LightGreen;
            this.label6.Location = new System.Drawing.Point(4, 365);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(165, 22);
            this.label6.TabIndex = 222;
            this.label6.Text = "Futásnapok száma:";
            // 
            // TárH_text
            // 
            this.TárH_text.BackColor = System.Drawing.Color.LightGreen;
            this.TárH_text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TárH_text.Location = new System.Drawing.Point(165, 153);
            this.TárH_text.Name = "TárH_text";
            this.TárH_text.Size = new System.Drawing.Size(187, 27);
            this.TárH_text.TabIndex = 221;
            // 
            // TárH_label
            // 
            this.TárH_label.AutoSize = true;
            this.TárH_label.BackColor = System.Drawing.Color.LightGreen;
            this.TárH_label.Location = new System.Drawing.Point(4, 155);
            this.TárH_label.Name = "TárH_label";
            this.TárH_label.Size = new System.Drawing.Size(118, 22);
            this.TárH_label.TabIndex = 220;
            this.TárH_label.Text = "Tárolási hely:";
            // 
            // LeltSz_text
            // 
            this.LeltSz_text.BackColor = System.Drawing.Color.LightGreen;
            this.LeltSz_text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LeltSz_text.Location = new System.Drawing.Point(165, 123);
            this.LeltSz_text.Name = "LeltSz_text";
            this.LeltSz_text.Size = new System.Drawing.Size(187, 27);
            this.LeltSz_text.TabIndex = 219;
            // 
            // LeltSz_label
            // 
            this.LeltSz_label.AutoSize = true;
            this.LeltSz_label.BackColor = System.Drawing.Color.LightGreen;
            this.LeltSz_label.Location = new System.Drawing.Point(4, 125);
            this.LeltSz_label.Name = "LeltSz_label";
            this.LeltSz_label.Size = new System.Drawing.Size(110, 22);
            this.LeltSz_label.TabIndex = 218;
            this.LeltSz_label.Text = "Leltári szám:";
            // 
            // EszkSz_text
            // 
            this.EszkSz_text.BackColor = System.Drawing.Color.LightGreen;
            this.EszkSz_text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EszkSz_text.Location = new System.Drawing.Point(165, 93);
            this.EszkSz_text.Name = "EszkSz_text";
            this.EszkSz_text.Size = new System.Drawing.Size(187, 27);
            this.EszkSz_text.TabIndex = 217;
            // 
            // EszkSz_label
            // 
            this.EszkSz_label.AutoSize = true;
            this.EszkSz_label.BackColor = System.Drawing.Color.LightGreen;
            this.EszkSz_label.Location = new System.Drawing.Point(4, 95);
            this.EszkSz_label.Name = "EszkSz_label";
            this.EszkSz_label.Size = new System.Drawing.Size(112, 22);
            this.EszkSz_label.TabIndex = 216;
            this.EszkSz_label.Text = "Eszközszám:";
            // 
            // Év_text
            // 
            this.Év_text.BackColor = System.Drawing.Color.LightGreen;
            this.Év_text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Év_text.Location = new System.Drawing.Point(165, 63);
            this.Év_text.Name = "Év_text";
            this.Év_text.Size = new System.Drawing.Size(187, 27);
            this.Év_text.TabIndex = 215;
            // 
            // Év_label
            // 
            this.Év_label.AutoSize = true;
            this.Év_label.BackColor = System.Drawing.Color.LightGreen;
            this.Év_label.Location = new System.Drawing.Point(4, 65);
            this.Év_label.Name = "Év_label";
            this.Év_label.Size = new System.Drawing.Size(36, 22);
            this.Év_label.TabIndex = 214;
            this.Év_label.Text = "Év:";
            // 
            // Gyártó_text
            // 
            this.Gyártó_text.BackColor = System.Drawing.Color.LightGreen;
            this.Gyártó_text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Gyártó_text.Location = new System.Drawing.Point(165, 33);
            this.Gyártó_text.Name = "Gyártó_text";
            this.Gyártó_text.Size = new System.Drawing.Size(187, 27);
            this.Gyártó_text.TabIndex = 213;
            // 
            // Gyártó_label
            // 
            this.Gyártó_label.AutoSize = true;
            this.Gyártó_label.BackColor = System.Drawing.Color.LightGreen;
            this.Gyártó_label.Location = new System.Drawing.Point(4, 35);
            this.Gyártó_label.Name = "Gyártó_label";
            this.Gyártó_label.Size = new System.Drawing.Size(69, 22);
            this.Gyártó_label.TabIndex = 212;
            this.Gyártó_label.Text = "Gyártó:";
            // 
            // Járműtípus_text
            // 
            this.Járműtípus_text.BackColor = System.Drawing.Color.LightGreen;
            this.Járműtípus_text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Járműtípus_text.Location = new System.Drawing.Point(165, 303);
            this.Járműtípus_text.Name = "Járműtípus_text";
            this.Járműtípus_text.Size = new System.Drawing.Size(187, 27);
            this.Járműtípus_text.TabIndex = 211;
            // 
            // Főmérnökség_text
            // 
            this.Főmérnökség_text.BackColor = System.Drawing.Color.LightGreen;
            this.Főmérnökség_text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Főmérnökség_text.Location = new System.Drawing.Point(165, 273);
            this.Főmérnökség_text.Name = "Főmérnökség_text";
            this.Főmérnökség_text.Size = new System.Drawing.Size(187, 27);
            this.Főmérnökség_text.TabIndex = 210;
            // 
            // Takarítás_text
            // 
            this.Takarítás_text.BackColor = System.Drawing.Color.LightGreen;
            this.Takarítás_text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Takarítás_text.Location = new System.Drawing.Point(165, 243);
            this.Takarítás_text.Name = "Takarítás_text";
            this.Takarítás_text.Size = new System.Drawing.Size(187, 27);
            this.Takarítás_text.TabIndex = 209;
            // 
            // Miótaáll_text
            // 
            this.Miótaáll_text.BackColor = System.Drawing.Color.LightGreen;
            this.Miótaáll_text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Miótaáll_text.Location = new System.Drawing.Point(165, 213);
            this.Miótaáll_text.Name = "Miótaáll_text";
            this.Miótaáll_text.Size = new System.Drawing.Size(187, 27);
            this.Miótaáll_text.TabIndex = 208;
            // 
            // Státus_text
            // 
            this.Státus_text.BackColor = System.Drawing.Color.LightGreen;
            this.Státus_text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Státus_text.Location = new System.Drawing.Point(165, 183);
            this.Státus_text.Name = "Státus_text";
            this.Státus_text.Size = new System.Drawing.Size(187, 27);
            this.Státus_text.TabIndex = 207;
            // 
            // Típus_text
            // 
            this.Típus_text.BackColor = System.Drawing.Color.LightGreen;
            this.Típus_text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Típus_text.Location = new System.Drawing.Point(165, 3);
            this.Típus_text.Name = "Típus_text";
            this.Típus_text.Size = new System.Drawing.Size(187, 27);
            this.Típus_text.TabIndex = 206;
            // 
            // alapadatRögzít
            // 
            this.alapadatRögzít.Image = ((System.Drawing.Image)(resources.GetObject("alapadatRögzít.Image")));
            this.alapadatRögzít.Location = new System.Drawing.Point(358, 5);
            this.alapadatRögzít.Name = "alapadatRögzít";
            this.alapadatRögzít.Size = new System.Drawing.Size(50, 50);
            this.alapadatRögzít.TabIndex = 205;
            this.alapadatRögzít.UseVisualStyleBackColor = true;
            this.alapadatRögzít.Click += new System.EventHandler(this.AlapadatRögzít_Click);
            // 
            // Típus_label
            // 
            this.Típus_label.AutoSize = true;
            this.Típus_label.BackColor = System.Drawing.Color.LightGreen;
            this.Típus_label.Location = new System.Drawing.Point(4, 5);
            this.Típus_label.Name = "Típus_label";
            this.Típus_label.Size = new System.Drawing.Size(60, 22);
            this.Típus_label.TabIndex = 204;
            this.Típus_label.Text = "Típus:";
            // 
            // Státus_label
            // 
            this.Státus_label.AutoSize = true;
            this.Státus_label.BackColor = System.Drawing.Color.LightGreen;
            this.Státus_label.Location = new System.Drawing.Point(4, 185);
            this.Státus_label.Name = "Státus_label";
            this.Státus_label.Size = new System.Drawing.Size(66, 22);
            this.Státus_label.TabIndex = 203;
            this.Státus_label.Text = "Státus:";
            // 
            // Miótaáll_label
            // 
            this.Miótaáll_label.AutoSize = true;
            this.Miótaáll_label.BackColor = System.Drawing.Color.LightGreen;
            this.Miótaáll_label.Location = new System.Drawing.Point(4, 215);
            this.Miótaáll_label.Name = "Miótaáll_label";
            this.Miótaáll_label.Size = new System.Drawing.Size(81, 22);
            this.Miótaáll_label.TabIndex = 202;
            this.Miótaáll_label.Text = "Mióta áll:";
            // 
            // Takarítás_label
            // 
            this.Takarítás_label.AutoSize = true;
            this.Takarítás_label.BackColor = System.Drawing.Color.LightGreen;
            this.Takarítás_label.Location = new System.Drawing.Point(4, 245);
            this.Takarítás_label.Name = "Takarítás_label";
            this.Takarítás_label.Size = new System.Drawing.Size(139, 22);
            this.Takarítás_label.TabIndex = 201;
            this.Takarítás_label.Text = "Utolsó takarítás:";
            // 
            // Főmérnökség_label
            // 
            this.Főmérnökség_label.AutoSize = true;
            this.Főmérnökség_label.BackColor = System.Drawing.Color.LightGreen;
            this.Főmérnökség_label.Location = new System.Drawing.Point(4, 275);
            this.Főmérnökség_label.Name = "Főmérnökség_label";
            this.Főmérnökség_label.Size = new System.Drawing.Size(171, 22);
            this.Főmérnökség_label.TabIndex = 200;
            this.Főmérnökség_label.Text = "Főmérnökségi típus:";
            // 
            // Járműtípus_label
            // 
            this.Járműtípus_label.AutoSize = true;
            this.Járműtípus_label.BackColor = System.Drawing.Color.LightGreen;
            this.Járműtípus_label.Location = new System.Drawing.Point(4, 305);
            this.Járműtípus_label.Name = "Járműtípus_label";
            this.Járműtípus_label.Size = new System.Drawing.Size(107, 22);
            this.Járműtípus_label.TabIndex = 199;
            this.Járműtípus_label.Text = "Jármű típus:";
            // 
            // Fülek
            // 
            this.Fülek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fülek.Controls.Add(this.TabPage1);
            this.Fülek.Controls.Add(this.TabPage4);
            this.Fülek.Controls.Add(this.tabPage8);
            this.Fülek.Controls.Add(this.TabPage7);
            this.Fülek.Location = new System.Drawing.Point(6, 55);
            this.Fülek.Name = "Fülek";
            this.Fülek.Padding = new System.Drawing.Point(16, 3);
            this.Fülek.SelectedIndex = 0;
            this.Fülek.Size = new System.Drawing.Size(1328, 571);
            this.Fülek.TabIndex = 171;
            this.Fülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Fülek_DrawItem);
            this.Fülek.SelectedIndexChanged += new System.EventHandler(this.Fülek_SelectedIndexChanged);
            // 
            // TabPage7
            // 
            this.TabPage7.BackColor = System.Drawing.Color.DarkTurquoise;
            this.TabPage7.Controls.Add(this.Szűrés);
            this.TabPage7.Controls.Add(this.PDF_néző);
            this.TabPage7.Controls.Add(this.PDF_törlés);
            this.TabPage7.Controls.Add(this.PDF_megnevezés);
            this.TabPage7.Controls.Add(this.label40);
            this.TabPage7.Controls.Add(this.label42);
            this.TabPage7.Controls.Add(this.Feltöltendő);
            this.TabPage7.Controls.Add(this.BtnPDF);
            this.TabPage7.Controls.Add(this.label43);
            this.TabPage7.Controls.Add(this.Pdf_listbox);
            this.TabPage7.Controls.Add(this.PDF_Frissít);
            this.TabPage7.Controls.Add(this.PDF_rögzít);
            this.TabPage7.Location = new System.Drawing.Point(4, 31);
            this.TabPage7.Name = "TabPage7";
            this.TabPage7.Size = new System.Drawing.Size(1320, 536);
            this.TabPage7.TabIndex = 10;
            this.TabPage7.Text = "PDF";
            // 
            // Szűrés
            // 
            this.Szűrés.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Szűrés.FormattingEnabled = true;
            this.Szűrés.ItemHeight = 22;
            this.Szűrés.Location = new System.Drawing.Point(380, 100);
            this.Szűrés.Name = "Szűrés";
            this.Szűrés.Size = new System.Drawing.Size(163, 268);
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
            this.PDF_néző.ShowToolbar = false;
            this.PDF_néző.Size = new System.Drawing.Size(825, 353);
            this.PDF_néző.TabIndex = 240;
            this.PDF_néző.ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitWidth;
            // 
            // PDF_törlés
            // 
            this.PDF_törlés.Image = ((System.Drawing.Image)(resources.GetObject("PDF_törlés.Image")));
            this.PDF_törlés.Location = new System.Drawing.Point(253, 80);
            this.PDF_törlés.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PDF_törlés.Name = "PDF_törlés";
            this.PDF_törlés.Size = new System.Drawing.Size(45, 45);
            this.PDF_törlés.TabIndex = 5;
            this.PDF_törlés.UseVisualStyleBackColor = true;
            this.PDF_törlés.Click += new System.EventHandler(this.PDF_törlés_Click);
            // 
            // PDF_megnevezés
            // 
            this.PDF_megnevezés.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PDF_megnevezés.Location = new System.Drawing.Point(430, 6);
            this.PDF_megnevezés.MaxLength = 50;
            this.PDF_megnevezés.Name = "PDF_megnevezés";
            this.PDF_megnevezés.Size = new System.Drawing.Size(536, 27);
            this.PDF_megnevezés.TabIndex = 6;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.BackColor = System.Drawing.Color.Silver;
            this.label40.Location = new System.Drawing.Point(303, 12);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(115, 22);
            this.label40.TabIndex = 192;
            this.label40.Text = "Megnevezés:";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(303, 55);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(137, 22);
            this.label42.TabIndex = 174;
            this.label42.Text = "Feltöltendő fájl :";
            // 
            // Feltöltendő
            // 
            this.Feltöltendő.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Feltöltendő.Location = new System.Drawing.Point(430, 49);
            this.Feltöltendő.Name = "Feltöltendő";
            this.Feltöltendő.Size = new System.Drawing.Size(536, 27);
            this.Feltöltendő.TabIndex = 7;
            // 
            // BtnPDF
            // 
            this.BtnPDF.Image = ((System.Drawing.Image)(resources.GetObject("BtnPDF.Image")));
            this.BtnPDF.Location = new System.Drawing.Point(200, 7);
            this.BtnPDF.Name = "BtnPDF";
            this.BtnPDF.Size = new System.Drawing.Size(45, 45);
            this.BtnPDF.TabIndex = 2;
            this.BtnPDF.UseVisualStyleBackColor = true;
            this.BtnPDF.Click += new System.EventHandler(this.BtnPDF_Click);
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(2, 57);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(210, 22);
            this.label43.TabIndex = 171;
            this.label43.Text = "Feltöltött dokumentumok:";
            // 
            // Pdf_listbox
            // 
            this.Pdf_listbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Pdf_listbox.FormattingEnabled = true;
            this.Pdf_listbox.ItemHeight = 22;
            this.Pdf_listbox.Location = new System.Drawing.Point(3, 80);
            this.Pdf_listbox.Name = "Pdf_listbox";
            this.Pdf_listbox.Size = new System.Drawing.Size(242, 290);
            this.Pdf_listbox.TabIndex = 4;
            this.Pdf_listbox.SelectedIndexChanged += new System.EventHandler(this.Pdf_listbox_SelectedIndexChanged);
            // 
            // PDF_Frissít
            // 
            this.PDF_Frissít.Image = ((System.Drawing.Image)(resources.GetObject("PDF_Frissít.Image")));
            this.PDF_Frissít.Location = new System.Drawing.Point(6, 9);
            this.PDF_Frissít.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PDF_Frissít.Name = "PDF_Frissít";
            this.PDF_Frissít.Size = new System.Drawing.Size(45, 45);
            this.PDF_Frissít.TabIndex = 1;
            this.PDF_Frissít.UseVisualStyleBackColor = true;
            this.PDF_Frissít.Click += new System.EventHandler(this.PDF_Frissít_Click);
            // 
            // PDF_rögzít
            // 
            this.PDF_rögzít.Image = ((System.Drawing.Image)(resources.GetObject("PDF_rögzít.Image")));
            this.PDF_rögzít.Location = new System.Drawing.Point(251, 7);
            this.PDF_rögzít.Name = "PDF_rögzít";
            this.PDF_rögzít.Size = new System.Drawing.Size(45, 45);
            this.PDF_rögzít.TabIndex = 3;
            this.PDF_rögzít.UseVisualStyleBackColor = true;
            this.PDF_rögzít.Click += new System.EventHandler(this.PDF_rögzít_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Ablak_Nosztalgia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(1350, 633);
            this.Controls.Add(this.Fülek);
            this.Controls.Add(this.Pályaszám);
            this.Controls.Add(this.Pályaszámkereső);
            this.Controls.Add(this.Label15);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.Panel2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Nosztalgia";
            this.Text = "Nosztalgia futás km adatok";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_Nosztalgia_Load);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.tabPage8.ResumeLayout(false);
            this.tabPage8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.TabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_lekérdezés)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.TabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.Km_group.ResumeLayout(false);
            this.KM_Alap.ResumeLayout(false);
            this.KM_Alap.PerformLayout();
            this.Idő_group.ResumeLayout(false);
            this.Idő_Alap.ResumeLayout(false);
            this.Idő_Alap.PerformLayout();
            this.Alap_group.ResumeLayout(false);
            this.Alap.ResumeLayout(false);
            this.Alap.PerformLayout();
            this.Fülek.ResumeLayout(false);
            this.TabPage7.ResumeLayout(false);
            this.TabPage7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal Panel Panel2;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal ComboBox Pályaszám;
        internal Button Pályaszámkereső;
        internal Label Label15;
        internal ProgressBar Holtart;
        internal Button BtnSúgó;
        internal TabPage tabPage8;
        internal Button Mentés;
        internal ListBox Kép_szűrés;
        internal Button KépTörlés;
        internal TextBox Kép_megnevezés;
        internal TextBox Kép_Feltöltendő;
        internal Label label4;
        internal Label label12;
        internal Button Kép_btn;
        internal Label label14;
        internal PictureBox PictureBox1;
        internal ListBox Kép_listbox;
        internal Button Kép_Listázás;
        internal Button Kép_rögzít;
        internal TabPage TabPage4;
        internal Button Lekérdezés_lekérdezés;
        internal TabPage TabPage1;
        internal TabControl Fülek;
        internal TabPage TabPage7;
        internal ListBox Szűrés;
        internal PdfiumViewer.PdfViewer PDF_néző;
        internal Button PDF_törlés;
        internal TextBox PDF_megnevezés;
        internal Label label40;
        internal Label label42;
        internal TextBox Feltöltendő;
        internal Button BtnPDF;
        internal Label label43;
        internal ListBox Pdf_listbox;
        internal Button PDF_Frissít;
        internal Button PDF_rögzít;
        internal FolderBrowserDialog FolderBrowserDialog1;
        internal SaveFileDialog SaveFileDialog1;
        internal SaveFileDialog saveFileDialog2;
        internal SaveFileDialog saveFileDialog3;
        internal OpenFileDialog openFileDialog1;
        internal FolderBrowserDialog folderBrowserDialog2;
        internal SaveFileDialog saveFileDialog4;
        internal Button SAP_Beolv;
        internal GroupBox groupBox1;
        internal TableLayoutPanel tableLayoutPanel2;
        internal Label label8;
        internal Button Napi_Adatok_rögzítése;
        internal Label label9;
        internal Label label7;
        internal TextBox Nap_azonosító;
        internal CheckBox Nap_törlés;
        internal DateTimePicker Nap_Dátum;
        internal Label label11;
        internal TextBox Nap_Telephely;
        internal Button Futásnaptábla_Rögzítés;
        internal Panel KM_Alap;
        internal TextBox Txt_V1_Kmu;
        internal Label label59;
        internal TextBox Txt_V1_Kmv;
        internal Label label60;
        internal TextBox Txt_V1_sorszám;
        internal Label label61;
        internal Label label62;
        internal Label label63;
        internal Button V_Km_CiklusRögzít_gomb;
        internal Label label64;
        internal Panel Idő_Alap;
        internal TextBox Fut_sorszám;
        internal Label label48;
        internal Label label49;
        internal Label label50;
        internal Button E_CiklusRögzít_gomb;
        internal Label label51;
        internal Panel Alap;
        internal TextBox Fut_nap_text;
        internal TextBox ut_forg_text;
        internal Label label3;
        internal Label label6;
        internal TextBox TárH_text;
        internal Label TárH_label;
        internal TextBox LeltSz_text;
        internal Label LeltSz_label;
        internal TextBox EszkSz_text;
        internal Label EszkSz_label;
        internal TextBox Év_text;
        internal Label Év_label;
        internal TextBox Gyártó_text;
        internal Label Gyártó_label;
        internal TextBox Járműtípus_text;
        internal TextBox Főmérnökség_text;
        internal TextBox Takarítás_text;
        internal TextBox Miótaáll_text;
        internal TextBox Státus_text;
        internal TextBox Típus_text;
        internal Button alapadatRögzít;
        internal Label Típus_label;
        internal Label Státus_label;
        internal Label Miótaáll_label;
        internal Label Takarítás_label;
        internal Label Főmérnökség_label;
        internal Label Járműtípus_label;
        internal GroupBox Idő_group;
        internal GroupBox Alap_group;
        internal GroupBox Km_group;
        internal Zuby.ADGV.AdvancedDataGridView Tábla_lekérdezés;
        internal DateTimePicker Dátum;
        internal GroupBox groupBox2;
        internal Panel panel8;
        internal TextBox Txt_V2_Kmu;
        internal Label label1;
        internal TextBox Txt_V2_Kmv;
        internal Label label2;
        internal TextBox Txt_V2_sorszám;
        internal Label label10;
        internal Label label41;
        internal Label label44;
        internal Button V_Idő_CiklusRögzít_gom;
        internal Label label45;
        internal ComboBox Cmb_KmCiklus_V2;
        internal ComboBox Cmb_KmCiklus_V1;
        internal ComboBox Cmb_FutCiklusE;
        internal Button button2;
        internal Button button3;
        internal DateTimePicker Txt_V1_dátum;
        internal DateTimePicker Fut_dátum;
        internal DateTimePicker Txt_V2_dátum;
        internal ComboBox Cmb_KmCiklus_V2_Cnév;
        internal ComboBox Cmb_KmCiklus_V1_Cnév;
        internal ComboBox Cmb_FutCiklusE_Cnév;
        internal DateTimePicker Dátum_ütem;
    }
}