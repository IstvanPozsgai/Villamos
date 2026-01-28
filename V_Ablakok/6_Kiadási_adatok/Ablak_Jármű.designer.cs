using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
    
    public partial class Ablak_Jármű : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Jármű));
            this.Fülek = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.MÓD_járműtípus = new System.Windows.Forms.ComboBox();
            this.MÓD_Főmérnökségitípus = new System.Windows.Forms.ComboBox();
            this.Mód_pályaszám = new System.Windows.Forms.ComboBox();
            this.Mód_üzembehelyezésdátuma = new System.Windows.Forms.DateTimePicker();
            this.MÓD_típustext = new System.Windows.Forms.TextBox();
            this.Mód_telephely = new System.Windows.Forms.TextBox();
            this.Label19 = new System.Windows.Forms.Label();
            this.Label18 = new System.Windows.Forms.Label();
            this.MÓD_pályaszámkereső = new System.Windows.Forms.Button();
            this.MÓD_SAP_adatok = new System.Windows.Forms.Button();
            this.Label17 = new System.Windows.Forms.Label();
            this.MÓD_rögzít = new System.Windows.Forms.Button();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.Label16 = new System.Windows.Forms.Label();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.TÖR_töröltek = new System.Windows.Forms.CheckBox();
            this.TÖR_List1 = new System.Windows.Forms.ListBox();
            this.TÖR_töröl = new System.Windows.Forms.Button();
            this.TÖR_Text1 = new System.Windows.Forms.TextBox();
            this.Label15 = new System.Windows.Forms.Label();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.LÉT_hozzáad = new System.Windows.Forms.Button();
            this.LÉT_járműtípus = new System.Windows.Forms.ComboBox();
            this.LÉT_Főmérnökségitípus = new System.Windows.Forms.ComboBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.LÉT_Pályaszám = new System.Windows.Forms.TextBox();
            this.Label10 = new System.Windows.Forms.Label();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Telephelyi_típus = new System.Windows.Forms.ComboBox();
            this.Lektelephely = new System.Windows.Forms.ComboBox();
            this.Közös_járművek = new System.Windows.Forms.ListBox();
            this.Command6 = new System.Windows.Forms.Button();
            this.Saját_járművek = new System.Windows.Forms.ListBox();
            this.Állkirak = new System.Windows.Forms.Button();
            this.Állvesz = new System.Windows.Forms.Button();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.Pdf_csere = new System.Windows.Forms.Button();
            this.PDF_néző = new PdfiumViewer.PdfViewer();
            this.Label38 = new System.Windows.Forms.Label();
            this.Feltöltendő = new System.Windows.Forms.TextBox();
            this.BtnPDF = new System.Windows.Forms.Button();
            this.Label9 = new System.Windows.Forms.Label();
            this.Pdf_listbox = new System.Windows.Forms.ListBox();
            this.Kiegészítő = new System.Windows.Forms.ComboBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.PDF_Frissít = new System.Windows.Forms.Button();
            this.PDF_rögzít = new System.Windows.Forms.Button();
            this.PDF_pályaszám = new System.Windows.Forms.ComboBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.TabPage5 = new System.Windows.Forms.TabPage();
            this.Tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.Mozg_lista = new System.Windows.Forms.Button();
            this.Mozg_havilista = new System.Windows.Forms.Button();
            this.Mozg_Excel = new System.Windows.Forms.Button();
            this.Mozg_Dátum = new System.Windows.Forms.DateTimePicker();
            this.TabPage6 = new System.Windows.Forms.TabPage();
            this.Panel5 = new System.Windows.Forms.Panel();
            this.Excel_Melyik = new System.Windows.Forms.Button();
            this.Keresés = new System.Windows.Forms.Button();
            this.Telephely_Frissít = new System.Windows.Forms.Button();
            this.CsoportkijelölMind = new System.Windows.Forms.Button();
            this.CsoportVissza = new System.Windows.Forms.Button();
            this.Típuslista_melyik = new System.Windows.Forms.CheckedListBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Tábla_telephely = new System.Windows.Forms.DataGridView();
            this.Btn_súgó = new System.Windows.Forms.Button();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Fülek.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.Panel3.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.TabPage4.SuspendLayout();
            this.TabPage2.SuspendLayout();
            this.TabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.TabPage6.SuspendLayout();
            this.Panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_telephely)).BeginInit();
            this.Panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // Fülek
            // 
            this.Fülek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fülek.Controls.Add(this.TabPage1);
            this.Fülek.Controls.Add(this.TabPage4);
            this.Fülek.Controls.Add(this.TabPage2);
            this.Fülek.Controls.Add(this.TabPage5);
            this.Fülek.Controls.Add(this.TabPage6);
            this.Fülek.Location = new System.Drawing.Point(5, 50);
            this.Fülek.Name = "Fülek";
            this.Fülek.Padding = new System.Drawing.Point(16, 3);
            this.Fülek.SelectedIndex = 0;
            this.Fülek.Size = new System.Drawing.Size(1188, 480);
            this.Fülek.TabIndex = 129;
            this.Fülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Fülek_DrawItem);
            this.Fülek.SelectedIndexChanged += new System.EventHandler(this.Fülek_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.Silver;
            this.TabPage1.Controls.Add(this.Panel3);
            this.TabPage1.Controls.Add(this.Panel2);
            this.TabPage1.Controls.Add(this.Panel1);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1180, 447);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Jármű létrehozás-törlés-módosítás";
            // 
            // Panel3
            // 
            this.Panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel3.BackColor = System.Drawing.Color.DodgerBlue;
            this.Panel3.Controls.Add(this.MÓD_járműtípus);
            this.Panel3.Controls.Add(this.MÓD_Főmérnökségitípus);
            this.Panel3.Controls.Add(this.Mód_pályaszám);
            this.Panel3.Controls.Add(this.Mód_üzembehelyezésdátuma);
            this.Panel3.Controls.Add(this.MÓD_típustext);
            this.Panel3.Controls.Add(this.Mód_telephely);
            this.Panel3.Controls.Add(this.Label19);
            this.Panel3.Controls.Add(this.Label18);
            this.Panel3.Controls.Add(this.MÓD_pályaszámkereső);
            this.Panel3.Controls.Add(this.MÓD_SAP_adatok);
            this.Panel3.Controls.Add(this.Label17);
            this.Panel3.Controls.Add(this.MÓD_rögzít);
            this.Panel3.Controls.Add(this.Label13);
            this.Panel3.Controls.Add(this.Label14);
            this.Panel3.Controls.Add(this.Label16);
            this.Panel3.Location = new System.Drawing.Point(715, 9);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(459, 428);
            this.Panel3.TabIndex = 2;
            // 
            // MÓD_járműtípus
            // 
            this.MÓD_járműtípus.DropDownHeight = 400;
            this.MÓD_járműtípus.FormattingEnabled = true;
            this.MÓD_járműtípus.IntegralHeight = false;
            this.MÓD_járműtípus.Location = new System.Drawing.Point(208, 77);
            this.MÓD_járműtípus.Name = "MÓD_járműtípus";
            this.MÓD_járműtípus.Size = new System.Drawing.Size(152, 28);
            this.MÓD_járműtípus.TabIndex = 152;
            // 
            // MÓD_Főmérnökségitípus
            // 
            this.MÓD_Főmérnökségitípus.DropDownHeight = 400;
            this.MÓD_Főmérnökségitípus.FormattingEnabled = true;
            this.MÓD_Főmérnökségitípus.IntegralHeight = false;
            this.MÓD_Főmérnökségitípus.Location = new System.Drawing.Point(208, 45);
            this.MÓD_Főmérnökségitípus.Name = "MÓD_Főmérnökségitípus";
            this.MÓD_Főmérnökségitípus.Size = new System.Drawing.Size(152, 28);
            this.MÓD_Főmérnökségitípus.TabIndex = 151;
            // 
            // Mód_pályaszám
            // 
            this.Mód_pályaszám.DropDownHeight = 400;
            this.Mód_pályaszám.FormattingEnabled = true;
            this.Mód_pályaszám.IntegralHeight = false;
            this.Mód_pályaszám.Location = new System.Drawing.Point(208, 12);
            this.Mód_pályaszám.Name = "Mód_pályaszám";
            this.Mód_pályaszám.Size = new System.Drawing.Size(152, 28);
            this.Mód_pályaszám.TabIndex = 150;
            this.Mód_pályaszám.SelectedIndexChanged += new System.EventHandler(this.Mód_pályaszám_SelectedIndexChanged);
            // 
            // Mód_üzembehelyezésdátuma
            // 
            this.Mód_üzembehelyezésdátuma.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Mód_üzembehelyezésdátuma.Location = new System.Drawing.Point(208, 109);
            this.Mód_üzembehelyezésdátuma.Name = "Mód_üzembehelyezésdátuma";
            this.Mód_üzembehelyezésdátuma.Size = new System.Drawing.Size(118, 26);
            this.Mód_üzembehelyezésdátuma.TabIndex = 148;
            // 
            // MÓD_típustext
            // 
            this.MÓD_típustext.Enabled = false;
            this.MÓD_típustext.Location = new System.Drawing.Point(208, 250);
            this.MÓD_típustext.Name = "MÓD_típustext";
            this.MÓD_típustext.Size = new System.Drawing.Size(190, 26);
            this.MÓD_típustext.TabIndex = 145;
            // 
            // Mód_telephely
            // 
            this.Mód_telephely.Enabled = false;
            this.Mód_telephely.Location = new System.Drawing.Point(208, 211);
            this.Mód_telephely.Name = "Mód_telephely";
            this.Mód_telephely.Size = new System.Drawing.Size(190, 26);
            this.Mód_telephely.TabIndex = 144;
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.Location = new System.Drawing.Point(12, 214);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(80, 20);
            this.Label19.TabIndex = 143;
            this.Label19.Text = "Telephely:";
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.Location = new System.Drawing.Point(12, 253);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(121, 20);
            this.Label18.TabIndex = 142;
            this.Label18.Text = "Telephelyi típus:";
            // 
            // MÓD_pályaszámkereső
            // 
            this.MÓD_pályaszámkereső.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.MÓD_pályaszámkereső.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MÓD_pályaszámkereső.Location = new System.Drawing.Point(376, 13);
            this.MÓD_pályaszámkereső.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MÓD_pályaszámkereső.Name = "MÓD_pályaszámkereső";
            this.MÓD_pályaszámkereső.Size = new System.Drawing.Size(50, 50);
            this.MÓD_pályaszámkereső.TabIndex = 141;
            this.ToolTip1.SetToolTip(this.MÓD_pályaszámkereső, "Frissíti az adatokat");
            this.MÓD_pályaszámkereső.UseVisualStyleBackColor = true;
            this.MÓD_pályaszámkereső.Click += new System.EventHandler(this.MÓD_pályaszámkereső_Click);
            // 
            // MÓD_SAP_adatok
            // 
            this.MÓD_SAP_adatok.BackgroundImage = global::Villamos.Properties.Resources.SAP;
            this.MÓD_SAP_adatok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MÓD_SAP_adatok.Location = new System.Drawing.Point(376, 68);
            this.MÓD_SAP_adatok.Name = "MÓD_SAP_adatok";
            this.MÓD_SAP_adatok.Size = new System.Drawing.Size(50, 50);
            this.MÓD_SAP_adatok.TabIndex = 18;
            this.ToolTip1.SetToolTip(this.MÓD_SAP_adatok, "SAP-s adatokkal frissít");
            this.MÓD_SAP_adatok.UseVisualStyleBackColor = true;
            this.MÓD_SAP_adatok.Click += new System.EventHandler(this.MÓD_SAP_adatok_Click);
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.Location = new System.Drawing.Point(12, 114);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(193, 20);
            this.Label17.TabIndex = 17;
            this.Label17.Text = "Üzembehelyezés dátuma:";
            // 
            // MÓD_rögzít
            // 
            this.MÓD_rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.MÓD_rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MÓD_rögzít.Location = new System.Drawing.Point(376, 121);
            this.MÓD_rögzít.Name = "MÓD_rögzít";
            this.MÓD_rögzít.Size = new System.Drawing.Size(50, 50);
            this.MÓD_rögzít.TabIndex = 16;
            this.ToolTip1.SetToolTip(this.MÓD_rögzít, "Módosítja a típusokat");
            this.MÓD_rögzít.UseVisualStyleBackColor = true;
            this.MÓD_rögzít.Click += new System.EventHandler(this.MÓD_rögzít_Click);
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(12, 48);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(152, 20);
            this.Label13.TabIndex = 3;
            this.Label13.Text = "Főmérnökségi típus:";
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(12, 80);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(95, 20);
            this.Label14.TabIndex = 2;
            this.Label14.Text = "Jármű típus:";
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Location = new System.Drawing.Point(12, 20);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(89, 20);
            this.Label16.TabIndex = 0;
            this.Label16.Text = "Pályaszám:";
            // 
            // Panel2
            // 
            this.Panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Panel2.BackColor = System.Drawing.Color.Coral;
            this.Panel2.Controls.Add(this.TÖR_töröltek);
            this.Panel2.Controls.Add(this.TÖR_List1);
            this.Panel2.Controls.Add(this.TÖR_töröl);
            this.Panel2.Controls.Add(this.TÖR_Text1);
            this.Panel2.Controls.Add(this.Label15);
            this.Panel2.Location = new System.Drawing.Point(356, 9);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(353, 428);
            this.Panel2.TabIndex = 1;
            // 
            // TÖR_töröltek
            // 
            this.TÖR_töröltek.AutoSize = true;
            this.TÖR_töröltek.Location = new System.Drawing.Point(16, 57);
            this.TÖR_töröltek.Name = "TÖR_töröltek";
            this.TÖR_töröltek.Size = new System.Drawing.Size(149, 24);
            this.TÖR_töröltek.TabIndex = 19;
            this.TÖR_töröltek.Text = "Törölt azonosítók";
            this.TÖR_töröltek.UseVisualStyleBackColor = true;
            this.TÖR_töröltek.CheckedChanged += new System.EventHandler(this.TÖR_töröltek_CheckedChanged);
            // 
            // TÖR_List1
            // 
            this.TÖR_List1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TÖR_List1.FormattingEnabled = true;
            this.TÖR_List1.ItemHeight = 20;
            this.TÖR_List1.Location = new System.Drawing.Point(120, 87);
            this.TÖR_List1.Name = "TÖR_List1";
            this.TÖR_List1.Size = new System.Drawing.Size(152, 324);
            this.TÖR_List1.TabIndex = 18;
            this.TÖR_List1.SelectedIndexChanged += new System.EventHandler(this.TÖR_List1_SelectedIndexChanged);
            // 
            // TÖR_töröl
            // 
            this.TÖR_töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.TÖR_töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TÖR_töröl.Location = new System.Drawing.Point(278, 13);
            this.TÖR_töröl.Name = "TÖR_töröl";
            this.TÖR_töröl.Size = new System.Drawing.Size(50, 50);
            this.TÖR_töröl.TabIndex = 17;
            this.ToolTip1.SetToolTip(this.TÖR_töröl, "Jármű törlése/ törlés visszaállítása");
            this.TÖR_töröl.UseVisualStyleBackColor = true;
            this.TÖR_töröl.Click += new System.EventHandler(this.TÖR_töröl_Click);
            // 
            // TÖR_Text1
            // 
            this.TÖR_Text1.Enabled = false;
            this.TÖR_Text1.Location = new System.Drawing.Point(120, 13);
            this.TÖR_Text1.Name = "TÖR_Text1";
            this.TÖR_Text1.Size = new System.Drawing.Size(152, 26);
            this.TÖR_Text1.TabIndex = 1;
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Location = new System.Drawing.Point(12, 20);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(89, 20);
            this.Label15.TabIndex = 0;
            this.Label15.Text = "Pályaszám:";
            // 
            // Panel1
            // 
            this.Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Panel1.BackColor = System.Drawing.Color.YellowGreen;
            this.Panel1.Controls.Add(this.LÉT_hozzáad);
            this.Panel1.Controls.Add(this.LÉT_járműtípus);
            this.Panel1.Controls.Add(this.LÉT_Főmérnökségitípus);
            this.Panel1.Controls.Add(this.Label12);
            this.Panel1.Controls.Add(this.Label11);
            this.Panel1.Controls.Add(this.LÉT_Pályaszám);
            this.Panel1.Controls.Add(this.Label10);
            this.Panel1.Location = new System.Drawing.Point(7, 8);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(343, 428);
            this.Panel1.TabIndex = 0;
            // 
            // LÉT_hozzáad
            // 
            this.LÉT_hozzáad.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.LÉT_hozzáad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LÉT_hozzáad.Location = new System.Drawing.Point(279, 128);
            this.LÉT_hozzáad.Name = "LÉT_hozzáad";
            this.LÉT_hozzáad.Size = new System.Drawing.Size(50, 50);
            this.LÉT_hozzáad.TabIndex = 16;
            this.ToolTip1.SetToolTip(this.LÉT_hozzáad, "Létrehozza a villamost");
            this.LÉT_hozzáad.UseVisualStyleBackColor = true;
            this.LÉT_hozzáad.Click += new System.EventHandler(this.LÉT_hozzáad_Click);
            // 
            // LÉT_járműtípus
            // 
            this.LÉT_járműtípus.DropDownHeight = 400;
            this.LÉT_járműtípus.FormattingEnabled = true;
            this.LÉT_járműtípus.IntegralHeight = false;
            this.LÉT_járműtípus.Location = new System.Drawing.Point(177, 80);
            this.LÉT_járműtípus.Name = "LÉT_járműtípus";
            this.LÉT_járműtípus.Size = new System.Drawing.Size(152, 28);
            this.LÉT_járműtípus.TabIndex = 5;
            // 
            // LÉT_Főmérnökségitípus
            // 
            this.LÉT_Főmérnökségitípus.DropDownHeight = 400;
            this.LÉT_Főmérnökségitípus.FormattingEnabled = true;
            this.LÉT_Főmérnökségitípus.IntegralHeight = false;
            this.LÉT_Főmérnökségitípus.Location = new System.Drawing.Point(177, 46);
            this.LÉT_Főmérnökségitípus.Name = "LÉT_Főmérnökségitípus";
            this.LÉT_Főmérnökségitípus.Size = new System.Drawing.Size(152, 28);
            this.LÉT_Főmérnökségitípus.TabIndex = 4;
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(12, 54);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(152, 20);
            this.Label12.TabIndex = 3;
            this.Label12.Text = "Főmérnökségi típus:";
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(12, 88);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(95, 20);
            this.Label11.TabIndex = 2;
            this.Label11.Text = "Jármű típus:";
            // 
            // LÉT_Pályaszám
            // 
            this.LÉT_Pályaszám.Location = new System.Drawing.Point(177, 14);
            this.LÉT_Pályaszám.Name = "LÉT_Pályaszám";
            this.LÉT_Pályaszám.Size = new System.Drawing.Size(152, 26);
            this.LÉT_Pályaszám.TabIndex = 1;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(12, 20);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(89, 20);
            this.Label10.TabIndex = 0;
            this.Label10.Text = "Pályaszám:";
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.LightGray;
            this.TabPage4.Controls.Add(this.Label1);
            this.TabPage4.Controls.Add(this.Label2);
            this.TabPage4.Controls.Add(this.Label3);
            this.TabPage4.Controls.Add(this.Label4);
            this.TabPage4.Controls.Add(this.Telephelyi_típus);
            this.TabPage4.Controls.Add(this.Lektelephely);
            this.TabPage4.Controls.Add(this.Közös_járművek);
            this.TabPage4.Controls.Add(this.Command6);
            this.TabPage4.Controls.Add(this.Saját_járművek);
            this.TabPage4.Controls.Add(this.Állkirak);
            this.TabPage4.Controls.Add(this.Állvesz);
            this.TabPage4.Location = new System.Drawing.Point(4, 29);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Size = new System.Drawing.Size(1180, 447);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Jármű átadás-átvétel";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(10, 10);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(112, 20);
            this.Label1.TabIndex = 129;
            this.Label1.Text = "Jármű típusok:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(243, 70);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(199, 20);
            this.Label2.TabIndex = 130;
            this.Label2.Text = "Állományon kívüli járművek:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(10, 70);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(81, 20);
            this.Label3.TabIndex = 131;
            this.Label3.Text = "Járművek:";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(10, 350);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(99, 20);
            this.Label4.TabIndex = 132;
            this.Label4.Text = "Céltelephely:";
            // 
            // Telephelyi_típus
            // 
            this.Telephelyi_típus.FormattingEnabled = true;
            this.Telephelyi_típus.Location = new System.Drawing.Point(14, 33);
            this.Telephelyi_típus.Name = "Telephelyi_típus";
            this.Telephelyi_típus.Size = new System.Drawing.Size(197, 28);
            this.Telephelyi_típus.TabIndex = 133;
            this.Telephelyi_típus.SelectedIndexChanged += new System.EventHandler(this.Combo1_SelectedIndexChanged);
            // 
            // Lektelephely
            // 
            this.Lektelephely.FormattingEnabled = true;
            this.Lektelephely.Location = new System.Drawing.Point(14, 373);
            this.Lektelephely.Name = "Lektelephely";
            this.Lektelephely.Size = new System.Drawing.Size(197, 28);
            this.Lektelephely.TabIndex = 134;
            // 
            // Közös_járművek
            // 
            this.Közös_járművek.FormattingEnabled = true;
            this.Közös_járművek.ItemHeight = 20;
            this.Közös_járművek.Location = new System.Drawing.Point(247, 93);
            this.Közös_járművek.Name = "Közös_járművek";
            this.Közös_járművek.Size = new System.Drawing.Size(195, 244);
            this.Közös_járművek.TabIndex = 135;
            // 
            // Command6
            // 
            this.Command6.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Command6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command6.Location = new System.Drawing.Point(402, 26);
            this.Command6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Command6.Name = "Command6";
            this.Command6.Size = new System.Drawing.Size(40, 40);
            this.Command6.TabIndex = 140;
            this.ToolTip1.SetToolTip(this.Command6, "Frissíti a listákat");
            this.Command6.UseVisualStyleBackColor = true;
            this.Command6.Click += new System.EventHandler(this.Command6_Click);
            // 
            // Saját_járművek
            // 
            this.Saját_járművek.FormattingEnabled = true;
            this.Saját_járművek.ItemHeight = 20;
            this.Saját_járművek.Location = new System.Drawing.Point(16, 93);
            this.Saját_járművek.Name = "Saját_járművek";
            this.Saját_járművek.Size = new System.Drawing.Size(195, 244);
            this.Saját_járművek.TabIndex = 136;
            // 
            // Állkirak
            // 
            this.Állkirak.BackColor = System.Drawing.Color.Red;
            this.Állkirak.ForeColor = System.Drawing.Color.White;
            this.Állkirak.Location = new System.Drawing.Point(16, 407);
            this.Állkirak.Name = "Állkirak";
            this.Állkirak.Size = new System.Drawing.Size(195, 35);
            this.Állkirak.TabIndex = 137;
            this.Állkirak.Text = "Állomámyból kirak";
            this.Állkirak.UseVisualStyleBackColor = false;
            this.Állkirak.Click += new System.EventHandler(this.Állkirak_Click);
            // 
            // Állvesz
            // 
            this.Állvesz.BackColor = System.Drawing.Color.Green;
            this.Állvesz.Location = new System.Drawing.Point(247, 350);
            this.Állvesz.Name = "Állvesz";
            this.Állvesz.Size = new System.Drawing.Size(195, 35);
            this.Állvesz.TabIndex = 138;
            this.Állvesz.Text = "Állományba Vesz";
            this.Állvesz.UseVisualStyleBackColor = false;
            this.Állvesz.Click += new System.EventHandler(this.Állvesz_Click);
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.YellowGreen;
            this.TabPage2.Controls.Add(this.Pdf_csere);
            this.TabPage2.Controls.Add(this.PDF_néző);
            this.TabPage2.Controls.Add(this.Label38);
            this.TabPage2.Controls.Add(this.Feltöltendő);
            this.TabPage2.Controls.Add(this.BtnPDF);
            this.TabPage2.Controls.Add(this.Label9);
            this.TabPage2.Controls.Add(this.Pdf_listbox);
            this.TabPage2.Controls.Add(this.Kiegészítő);
            this.TabPage2.Controls.Add(this.Label8);
            this.TabPage2.Controls.Add(this.PDF_Frissít);
            this.TabPage2.Controls.Add(this.PDF_rögzít);
            this.TabPage2.Controls.Add(this.PDF_pályaszám);
            this.TabPage2.Controls.Add(this.Label7);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Size = new System.Drawing.Size(1180, 447);
            this.TabPage2.TabIndex = 6;
            this.TabPage2.Text = "Jármű dokumentáció";
            // 
            // Pdf_csere
            // 
            this.Pdf_csere.BackgroundImage = global::Villamos.Properties.Resources.Mimetype_recycled;
            this.Pdf_csere.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Pdf_csere.Location = new System.Drawing.Point(110, 3);
            this.Pdf_csere.Name = "Pdf_csere";
            this.Pdf_csere.Size = new System.Drawing.Size(45, 45);
            this.Pdf_csere.TabIndex = 242;
            this.ToolTip1.SetToolTip(this.Pdf_csere, "A feltöltött két PDF fájl sorrendjét megcseréli");
            this.Pdf_csere.UseVisualStyleBackColor = true;
            this.Pdf_csere.Click += new System.EventHandler(this.Pdf_csere_Click);
            // 
            // PDF_néző
            // 
            this.PDF_néző.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PDF_néző.Location = new System.Drawing.Point(271, 54);
            this.PDF_néző.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.PDF_néző.Name = "PDF_néző";
            this.PDF_néző.Size = new System.Drawing.Size(896, 383);
            this.PDF_néző.TabIndex = 241;
            this.PDF_néző.ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitWidth;
            // 
            // Label38
            // 
            this.Label38.AutoSize = true;
            this.Label38.Location = new System.Drawing.Point(319, 25);
            this.Label38.Name = "Label38";
            this.Label38.Size = new System.Drawing.Size(121, 20);
            this.Label38.TabIndex = 161;
            this.Label38.Text = "Feltöltendő fájl :";
            // 
            // Feltöltendő
            // 
            this.Feltöltendő.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Feltöltendő.Location = new System.Drawing.Point(446, 19);
            this.Feltöltendő.Name = "Feltöltendő";
            this.Feltöltendő.Size = new System.Drawing.Size(721, 26);
            this.Feltöltendő.TabIndex = 160;
            // 
            // BtnPDF
            // 
            this.BtnPDF.BackgroundImage = global::Villamos.Properties.Resources.Folder_;
            this.BtnPDF.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnPDF.Location = new System.Drawing.Point(268, 3);
            this.BtnPDF.Name = "BtnPDF";
            this.BtnPDF.Size = new System.Drawing.Size(45, 45);
            this.BtnPDF.TabIndex = 159;
            this.ToolTip1.SetToolTip(this.BtnPDF, "PDF fájl tallózása");
            this.BtnPDF.UseVisualStyleBackColor = true;
            this.BtnPDF.Click += new System.EventHandler(this.BtnPDF_Click);
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(12, 128);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(190, 20);
            this.Label9.TabIndex = 158;
            this.Label9.Text = "Feltöltött dokumentumok:";
            // 
            // Pdf_listbox
            // 
            this.Pdf_listbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Pdf_listbox.FormattingEnabled = true;
            this.Pdf_listbox.ItemHeight = 20;
            this.Pdf_listbox.Location = new System.Drawing.Point(3, 160);
            this.Pdf_listbox.Name = "Pdf_listbox";
            this.Pdf_listbox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.Pdf_listbox.Size = new System.Drawing.Size(259, 284);
            this.Pdf_listbox.TabIndex = 157;
            this.Pdf_listbox.SelectedIndexChanged += new System.EventHandler(this.Filelistbox_SelectedIndexChanged);
            // 
            // Kiegészítő
            // 
            this.Kiegészítő.DropDownHeight = 300;
            this.Kiegészítő.FormattingEnabled = true;
            this.Kiegészítő.IntegralHeight = false;
            this.Kiegészítő.Location = new System.Drawing.Point(110, 88);
            this.Kiegészítő.Name = "Kiegészítő";
            this.Kiegészítő.Size = new System.Drawing.Size(152, 28);
            this.Kiegészítő.TabIndex = 156;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(12, 96);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(86, 20);
            this.Label8.TabIndex = 155;
            this.Label8.Text = "Kiegészítő:";
            // 
            // PDF_Frissít
            // 
            this.PDF_Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.PDF_Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PDF_Frissít.Location = new System.Drawing.Point(3, 3);
            this.PDF_Frissít.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PDF_Frissít.Name = "PDF_Frissít";
            this.PDF_Frissít.Size = new System.Drawing.Size(45, 45);
            this.PDF_Frissít.TabIndex = 154;
            this.ToolTip1.SetToolTip(this.PDF_Frissít, "Frissíti az adatokat");
            this.PDF_Frissít.UseVisualStyleBackColor = true;
            this.PDF_Frissít.Click += new System.EventHandler(this.PDF_Frissít_Click);
            // 
            // PDF_rögzít
            // 
            this.PDF_rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.PDF_rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PDF_rögzít.Location = new System.Drawing.Point(217, 3);
            this.PDF_rögzít.Name = "PDF_rögzít";
            this.PDF_rögzít.Size = new System.Drawing.Size(45, 45);
            this.PDF_rögzít.TabIndex = 153;
            this.ToolTip1.SetToolTip(this.PDF_rögzít, "Feltölti a pdf fájlt");
            this.PDF_rögzít.UseVisualStyleBackColor = true;
            this.PDF_rögzít.Click += new System.EventHandler(this.PDF_rögzít_Click);
            // 
            // PDF_pályaszám
            // 
            this.PDF_pályaszám.DropDownHeight = 300;
            this.PDF_pályaszám.FormattingEnabled = true;
            this.PDF_pályaszám.IntegralHeight = false;
            this.PDF_pályaszám.Location = new System.Drawing.Point(110, 54);
            this.PDF_pályaszám.Name = "PDF_pályaszám";
            this.PDF_pályaszám.Size = new System.Drawing.Size(152, 28);
            this.PDF_pályaszám.TabIndex = 152;
            this.PDF_pályaszám.SelectedIndexChanged += new System.EventHandler(this.PDF_Frissít_Click);
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(12, 62);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(89, 20);
            this.Label7.TabIndex = 151;
            this.Label7.Text = "Pályaszám:";
            // 
            // TabPage5
            // 
            this.TabPage5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.TabPage5.Controls.Add(this.Tábla);
            this.TabPage5.Controls.Add(this.Mozg_lista);
            this.TabPage5.Controls.Add(this.Mozg_havilista);
            this.TabPage5.Controls.Add(this.Mozg_Excel);
            this.TabPage5.Controls.Add(this.Mozg_Dátum);
            this.TabPage5.Location = new System.Drawing.Point(4, 29);
            this.TabPage5.Name = "TabPage5";
            this.TabPage5.Size = new System.Drawing.Size(1180, 447);
            this.TabPage5.TabIndex = 4;
            this.TabPage5.Text = "Járművek telephelyek közötti mozgása";
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.FilterAndSortEnabled = true;
            this.Tábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.Location = new System.Drawing.Point(154, 2);
            this.Tábla.MaxFilterButtonImageHeight = 23;
            this.Tábla.Name = "Tábla";
            this.Tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tábla.RowHeadersVisible = false;
            this.Tábla.Size = new System.Drawing.Size(1023, 442);
            this.Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.TabIndex = 170;
            // 
            // Mozg_lista
            // 
            this.Mozg_lista.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Mozg_lista.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Mozg_lista.Location = new System.Drawing.Point(12, 93);
            this.Mozg_lista.Name = "Mozg_lista";
            this.Mozg_lista.Size = new System.Drawing.Size(134, 53);
            this.Mozg_lista.TabIndex = 169;
            this.Mozg_lista.Text = "Napi kocsimozgások";
            this.ToolTip1.SetToolTip(this.Mozg_lista, "Napi kocsimozgások listázása");
            this.Mozg_lista.UseVisualStyleBackColor = false;
            this.Mozg_lista.Click += new System.EventHandler(this.Mozg_lista_Click);
            // 
            // Mozg_havilista
            // 
            this.Mozg_havilista.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Mozg_havilista.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Mozg_havilista.Location = new System.Drawing.Point(12, 152);
            this.Mozg_havilista.Name = "Mozg_havilista";
            this.Mozg_havilista.Size = new System.Drawing.Size(134, 53);
            this.Mozg_havilista.TabIndex = 168;
            this.Mozg_havilista.Text = "Havi kocsimozgások";
            this.ToolTip1.SetToolTip(this.Mozg_havilista, "Havi kocsimozgások listázása");
            this.Mozg_havilista.UseVisualStyleBackColor = false;
            this.Mozg_havilista.Click += new System.EventHandler(this.Mozg_havilista_Click);
            // 
            // Mozg_Excel
            // 
            this.Mozg_Excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Mozg_Excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Mozg_Excel.Location = new System.Drawing.Point(12, 43);
            this.Mozg_Excel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Mozg_Excel.Name = "Mozg_Excel";
            this.Mozg_Excel.Size = new System.Drawing.Size(45, 45);
            this.Mozg_Excel.TabIndex = 166;
            this.ToolTip1.SetToolTip(this.Mozg_Excel, "A táblázat adatit Excel táblába menti");
            this.Mozg_Excel.UseVisualStyleBackColor = true;
            this.Mozg_Excel.Click += new System.EventHandler(this.Mozg_Excel_Click);
            // 
            // Mozg_Dátum
            // 
            this.Mozg_Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Mozg_Dátum.Location = new System.Drawing.Point(12, 12);
            this.Mozg_Dátum.Name = "Mozg_Dátum";
            this.Mozg_Dátum.Size = new System.Drawing.Size(118, 26);
            this.Mozg_Dátum.TabIndex = 149;
            this.ToolTip1.SetToolTip(this.Mozg_Dátum, "Dátum választó");
            this.Mozg_Dátum.ValueChanged += new System.EventHandler(this.Mozg_Dátum_ValueChanged);
            // 
            // TabPage6
            // 
            this.TabPage6.BackColor = System.Drawing.Color.Olive;
            this.TabPage6.Controls.Add(this.Panel5);
            this.TabPage6.Controls.Add(this.Tábla_telephely);
            this.TabPage6.Location = new System.Drawing.Point(4, 29);
            this.TabPage6.Name = "TabPage6";
            this.TabPage6.Size = new System.Drawing.Size(1180, 447);
            this.TabPage6.TabIndex = 5;
            this.TabPage6.Text = "Jármű melyik telephelyen van";
            // 
            // Panel5
            // 
            this.Panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Panel5.BackColor = System.Drawing.Color.Goldenrod;
            this.Panel5.Controls.Add(this.Excel_Melyik);
            this.Panel5.Controls.Add(this.Keresés);
            this.Panel5.Controls.Add(this.Telephely_Frissít);
            this.Panel5.Controls.Add(this.CsoportkijelölMind);
            this.Panel5.Controls.Add(this.CsoportVissza);
            this.Panel5.Controls.Add(this.Típuslista_melyik);
            this.Panel5.Controls.Add(this.Label6);
            this.Panel5.Location = new System.Drawing.Point(5, 5);
            this.Panel5.Name = "Panel5";
            this.Panel5.Size = new System.Drawing.Size(239, 437);
            this.Panel5.TabIndex = 0;
            // 
            // Excel_Melyik
            // 
            this.Excel_Melyik.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Excel_Melyik.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excel_Melyik.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excel_Melyik.Location = new System.Drawing.Point(191, 391);
            this.Excel_Melyik.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Excel_Melyik.Name = "Excel_Melyik";
            this.Excel_Melyik.Size = new System.Drawing.Size(40, 40);
            this.Excel_Melyik.TabIndex = 180;
            this.ToolTip1.SetToolTip(this.Excel_Melyik, "A táblázat adatit Excel táblába menti");
            this.Excel_Melyik.UseVisualStyleBackColor = true;
            this.Excel_Melyik.Click += new System.EventHandler(this.Excel_Melyik_Click);
            // 
            // Keresés
            // 
            this.Keresés.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Keresés.BackgroundImage = global::Villamos.Properties.Resources.Nagyító;
            this.Keresés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Keresés.Location = new System.Drawing.Point(191, 347);
            this.Keresés.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Keresés.Name = "Keresés";
            this.Keresés.Size = new System.Drawing.Size(40, 40);
            this.Keresés.TabIndex = 179;
            this.ToolTip1.SetToolTip(this.Keresés, "Frissíti a listákat");
            this.Keresés.UseVisualStyleBackColor = true;
            this.Keresés.Click += new System.EventHandler(this.Keresés_Click);
            // 
            // Telephely_Frissít
            // 
            this.Telephely_Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Telephely_Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Telephely_Frissít.Location = new System.Drawing.Point(191, 188);
            this.Telephely_Frissít.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Telephely_Frissít.Name = "Telephely_Frissít";
            this.Telephely_Frissít.Size = new System.Drawing.Size(40, 40);
            this.Telephely_Frissít.TabIndex = 173;
            this.ToolTip1.SetToolTip(this.Telephely_Frissít, "Frissíti a listákat");
            this.Telephely_Frissít.UseVisualStyleBackColor = true;
            this.Telephely_Frissít.Click += new System.EventHandler(this.Telephely_Frissít_Click);
            // 
            // CsoportkijelölMind
            // 
            this.CsoportkijelölMind.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.CsoportkijelölMind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CsoportkijelölMind.Location = new System.Drawing.Point(192, 28);
            this.CsoportkijelölMind.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CsoportkijelölMind.Name = "CsoportkijelölMind";
            this.CsoportkijelölMind.Size = new System.Drawing.Size(40, 40);
            this.CsoportkijelölMind.TabIndex = 171;
            this.ToolTip1.SetToolTip(this.CsoportkijelölMind, "Mindent kijelöl");
            this.CsoportkijelölMind.UseVisualStyleBackColor = true;
            this.CsoportkijelölMind.Click += new System.EventHandler(this.CsoportkijelölMind_Click);
            // 
            // CsoportVissza
            // 
            this.CsoportVissza.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.CsoportVissza.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CsoportVissza.Location = new System.Drawing.Point(191, 72);
            this.CsoportVissza.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CsoportVissza.Name = "CsoportVissza";
            this.CsoportVissza.Size = new System.Drawing.Size(40, 40);
            this.CsoportVissza.TabIndex = 172;
            this.ToolTip1.SetToolTip(this.CsoportVissza, "Minden kijelölés törlése");
            this.CsoportVissza.UseVisualStyleBackColor = true;
            this.CsoportVissza.Click += new System.EventHandler(this.CsoportVissza_Click);
            // 
            // Típuslista_melyik
            // 
            this.Típuslista_melyik.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Típuslista_melyik.CheckOnClick = true;
            this.Típuslista_melyik.FormattingEnabled = true;
            this.Típuslista_melyik.Location = new System.Drawing.Point(3, 28);
            this.Típuslista_melyik.Name = "Típuslista_melyik";
            this.Típuslista_melyik.Size = new System.Drawing.Size(182, 403);
            this.Típuslista_melyik.TabIndex = 1;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(5, 5);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(109, 20);
            this.Label6.TabIndex = 0;
            this.Label6.Text = "Típus választó";
            // 
            // Tábla_telephely
            // 
            this.Tábla_telephely.AllowUserToAddRows = false;
            this.Tábla_telephely.AllowUserToDeleteRows = false;
            this.Tábla_telephely.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla_telephely.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Tábla_telephely.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla_telephely.Location = new System.Drawing.Point(250, 5);
            this.Tábla_telephely.Name = "Tábla_telephely";
            this.Tábla_telephely.RowHeadersVisible = false;
            this.Tábla_telephely.Size = new System.Drawing.Size(927, 437);
            this.Tábla_telephely.TabIndex = 1;
            this.Tábla_telephely.Visible = false;
            // 
            // Btn_súgó
            // 
            this.Btn_súgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_súgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.Btn_súgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_súgó.Location = new System.Drawing.Point(1153, 5);
            this.Btn_súgó.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_súgó.Name = "Btn_súgó";
            this.Btn_súgó.Size = new System.Drawing.Size(40, 40);
            this.Btn_súgó.TabIndex = 139;
            this.Btn_súgó.UseVisualStyleBackColor = true;
            this.Btn_súgó.Click += new System.EventHandler(this.Btn_súgó_Click);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(388, 5);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(759, 28);
            this.Holtart.TabIndex = 140;
            this.Holtart.Visible = false;
            // 
            // Panel4
            // 
            this.Panel4.Controls.Add(this.Cmbtelephely);
            this.Panel4.Controls.Add(this.Label5);
            this.Panel4.Location = new System.Drawing.Point(9, 5);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(373, 33);
            this.Panel4.TabIndex = 141;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(175, 2);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
            this.Cmbtelephely.SelectedIndexChanged += new System.EventHandler(this.Cmbtelephely_SelectedIndexChanged);
            this.Cmbtelephely.SelectionChangeCommitted += new System.EventHandler(this.Cmbtelephely_SelectionChangeCommitted);
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
            // Ablak_Jármű
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(1199, 533);
            this.Controls.Add(this.Panel4);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Fülek);
            this.Controls.Add(this.Btn_súgó);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Jármű";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Jármű létrehozás, törlés, áthelyezés, lekérdezés";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_Átadás_átvétel_Load);
            this.Shown += new System.EventHandler(this.Ablak_Jármű_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ablak_Jármű_KeyDown);
            this.Fülek.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.TabPage4.ResumeLayout(false);
            this.TabPage4.PerformLayout();
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            this.TabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.TabPage6.ResumeLayout(false);
            this.Panel5.ResumeLayout(false);
            this.Panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_telephely)).EndInit();
            this.Panel4.ResumeLayout(false);
            this.Panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        internal TabControl Fülek;
        internal TabPage TabPage1;
        internal TabPage TabPage4;
        internal Label Label1;
       
        internal Label Label2;
    
        internal Label Label3;
      
        internal Label Label4;
        internal ComboBox Telephelyi_típus;
        internal ComboBox Lektelephely;
      
        internal ListBox Közös_járművek;
        internal Button Command6;
        internal ListBox Saját_járművek;
        internal Button Állkirak;
        internal Button Állvesz;
        internal TabPage TabPage5;
        internal TabPage TabPage6;
        internal Button Btn_súgó;
        internal Panel Panel1;
        internal ComboBox LÉT_járműtípus;
        internal ComboBox LÉT_Főmérnökségitípus;
        internal Label Label12;
        internal Label Label11;
        internal TextBox LÉT_Pályaszám;
        internal Label Label10;
        internal Panel Panel2;
        internal Button TÖR_töröl;
        internal TextBox TÖR_Text1;
        internal Label Label15;
        internal Button LÉT_hozzáad;
        internal Panel Panel3;
        internal Label Label17;
        internal Button MÓD_rögzít;
        internal Label Label13;
        internal Label Label14;
        internal Label Label16;
        internal CheckBox TÖR_töröltek;
        internal ListBox TÖR_List1;
        internal TextBox MÓD_típustext;
        internal TextBox Mód_telephely;
        internal Label Label19;
        internal Label Label18;
        internal Button MÓD_pályaszámkereső;
        internal Button MÓD_SAP_adatok;
        internal DateTimePicker Mód_üzembehelyezésdátuma;
        internal ToolTip ToolTip1;
        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal DateTimePicker Mozg_Dátum;
        internal Button Mozg_Excel;
        internal Zuby.ADGV.AdvancedDataGridView Tábla;
        internal Button Mozg_lista;
        internal Button Mozg_havilista;
        internal ComboBox Mód_pályaszám;
        internal ComboBox MÓD_járműtípus;
        internal ComboBox MÓD_Főmérnökségitípus;
        internal Panel Panel4;
        internal ComboBox Cmbtelephely;
        internal Label Label5;
        internal Panel Panel5;
        internal CheckedListBox Típuslista_melyik;
        internal Label Label6;
        internal Button Excel_Melyik;
        internal Button Keresés;
        internal Button Telephely_Frissít;
        internal Button CsoportkijelölMind;
        internal Button CsoportVissza;
        internal DataGridView Tábla_telephely;
        internal TabPage TabPage2;
        internal ComboBox Kiegészítő;
        internal Label Label8;
        internal Button PDF_Frissít;
        internal Button PDF_rögzít;
        internal ComboBox PDF_pályaszám;
        internal Label Label7;
        
        internal Label Label9;
        internal ListBox Pdf_listbox;
        internal Button BtnPDF;
        internal TextBox Feltöltendő;
        internal Label Label38;
        private PdfiumViewer.PdfViewer PDF_néző;
        internal Button Pdf_csere;
    }
}