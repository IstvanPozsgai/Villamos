using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Villamos
{
    
    public partial class Ablak_Rezsi : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Rezsi));
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Dátumig = new System.Windows.Forms.DateTimePicker();
            this.Dátumtól = new System.Windows.Forms.DateTimePicker();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Azonosító_napló = new System.Windows.Forms.ComboBox();
            this.Megnevezés_napló = new System.Windows.Forms.TextBox();
            this.Lapfülek = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.Vezér = new System.Windows.Forms.TextBox();
            this.Törzs_excel = new System.Windows.Forms.Button();
            this.Törzs_Új_adat = new System.Windows.Forms.Button();
            this.Törzs_Frissít = new System.Windows.Forms.Button();
            this.Törzs_tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.CsoportCombo = new System.Windows.Forms.ComboBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.Méret = new System.Windows.Forms.TextBox();
            this.Megnevezés = new System.Windows.Forms.TextBox();
            this.Label34 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Azonosító = new System.Windows.Forms.ComboBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.Aktív = new System.Windows.Forms.CheckBox();
            this.Rögzítteljes = new System.Windows.Forms.Button();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.Tár_tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.Állvány = new System.Windows.Forms.TextBox();
            this.Polc = new System.Windows.Forms.TextBox();
            this.Megjegyzés = new System.Windows.Forms.TextBox();
            this.Helyiség = new System.Windows.Forms.TextBox();
            this.Label16 = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.TárMegnevezés = new System.Windows.Forms.TextBox();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.TárAzonosító = new System.Windows.Forms.ComboBox();
            this.Tár_excel = new System.Windows.Forms.Button();
            this.Tár_frissít = new System.Windows.Forms.Button();
            this.Tárolásihelyrögzítés = new System.Windows.Forms.Button();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.KépTörlés = new System.Windows.Forms.Button();
            this.FényképLista = new System.Windows.Forms.ListBox();
            this.KépHozzáad = new System.Windows.Forms.Button();
            this.FénySorszám = new System.Windows.Forms.TextBox();
            this.FényMegnevezés = new System.Windows.Forms.TextBox();
            this.Label17 = new System.Windows.Forms.Label();
            this.Label18 = new System.Windows.Forms.Label();
            this.Fényazonosító = new System.Windows.Forms.ComboBox();
            this.Label19 = new System.Windows.Forms.Label();
            this.KépKeret = new System.Windows.Forms.PictureBox();
            this.Fényképfrissítés = new System.Windows.Forms.Button();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.Lista_megnevezés_szűrő = new System.Windows.Forms.TextBox();
            this.ListaCsoportCombo = new System.Windows.Forms.ComboBox();
            this.Tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.Excel = new System.Windows.Forms.Button();
            this.Command20 = new System.Windows.Forms.Button();
            this.TabPage5 = new System.Windows.Forms.TabPage();
            this.BehovaRaktár = new System.Windows.Forms.TextBox();
            this.BeMegnevezés = new System.Windows.Forms.TextBox();
            this.Label24 = new System.Windows.Forms.Label();
            this.Label22 = new System.Windows.Forms.Label();
            this.Label23 = new System.Windows.Forms.Label();
            this.BeAzonosító = new System.Windows.Forms.ComboBox();
            this.BeMennyiség = new System.Windows.Forms.TextBox();
            this.Bekészlet = new System.Windows.Forms.TextBox();
            this.Label21 = new System.Windows.Forms.Label();
            this.Label20 = new System.Windows.Forms.Label();
            this.BeHonnanraktár = new System.Windows.Forms.ComboBox();
            this.Label25 = new System.Windows.Forms.Label();
            this.BeRögzít = new System.Windows.Forms.Button();
            this.TabPage6 = new System.Windows.Forms.TabPage();
            this.Label36 = new System.Windows.Forms.Label();
            this.KiFelhasználás = new System.Windows.Forms.TextBox();
            this.KiHonnanRaktár = new System.Windows.Forms.TextBox();
            this.KiMegnevezés = new System.Windows.Forms.TextBox();
            this.Label26 = new System.Windows.Forms.Label();
            this.Label27 = new System.Windows.Forms.Label();
            this.Label28 = new System.Windows.Forms.Label();
            this.Kiazonosító = new System.Windows.Forms.ComboBox();
            this.KiMennyiség = new System.Windows.Forms.TextBox();
            this.KiKészlet = new System.Windows.Forms.TextBox();
            this.Label32 = new System.Windows.Forms.Label();
            this.Label33 = new System.Windows.Forms.Label();
            this.KiHovaRaktár = new System.Windows.Forms.ComboBox();
            this.Label35 = new System.Windows.Forms.Label();
            this.Kirögzít = new System.Windows.Forms.Button();
            this.TabPage7 = new System.Windows.Forms.TabPage();
            this.Napló_tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.Listáz = new System.Windows.Forms.Button();
            this.Excelclick = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.Panel2.SuspendLayout();
            this.Lapfülek.SuspendLayout();
            this.TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Törzs_tábla)).BeginInit();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tár_tábla)).BeginInit();
            this.TabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KépKeret)).BeginInit();
            this.TabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.TabPage5.SuspendLayout();
            this.TabPage6.SuspendLayout();
            this.TabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Napló_tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(353, 12);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(668, 28);
            this.Holtart.TabIndex = 175;
            this.Holtart.Visible = false;
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.Cmbtelephely);
            this.Panel2.Controls.Add(this.Label13);
            this.Panel2.Location = new System.Drawing.Point(12, 12);
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
            this.Cmbtelephely.SelectedIndexChanged += new System.EventHandler(this.Cmbtelephely_SelectedIndexChanged);
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
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(6, 12);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(78, 20);
            this.Label1.TabIndex = 176;
            this.Label1.Text = "Dátumtól:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(130, 12);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(73, 20);
            this.Label2.TabIndex = 177;
            this.Label2.Text = "Dátumig:";
            // 
            // Dátumig
            // 
            this.Dátumig.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátumig.Location = new System.Drawing.Point(134, 35);
            this.Dátumig.Name = "Dátumig";
            this.Dátumig.Size = new System.Drawing.Size(118, 26);
            this.Dátumig.TabIndex = 178;
            // 
            // Dátumtól
            // 
            this.Dátumtól.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátumtól.Location = new System.Drawing.Point(10, 35);
            this.Dátumtól.Name = "Dátumtól";
            this.Dátumtól.Size = new System.Drawing.Size(118, 26);
            this.Dátumtól.TabIndex = 179;
            this.Dátumtól.ValueChanged += new System.EventHandler(this.Dátumtól_ValueChanged);
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(487, 12);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(103, 20);
            this.Label5.TabIndex = 183;
            this.Label5.Text = "Megnevezés:";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(267, 12);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(84, 20);
            this.Label6.TabIndex = 184;
            this.Label6.Text = "Azonosító:";
            // 
            // Azonosító_napló
            // 
            this.Azonosító_napló.FormattingEnabled = true;
            this.Azonosító_napló.Location = new System.Drawing.Point(271, 35);
            this.Azonosító_napló.Name = "Azonosító_napló";
            this.Azonosító_napló.Size = new System.Drawing.Size(204, 28);
            this.Azonosító_napló.TabIndex = 185;
            this.Azonosító_napló.SelectedIndexChanged += new System.EventHandler(this.Azonosító_napló_SelectedIndexChanged);
            // 
            // Megnevezés_napló
            // 
            this.Megnevezés_napló.Location = new System.Drawing.Point(491, 37);
            this.Megnevezés_napló.Name = "Megnevezés_napló";
            this.Megnevezés_napló.Size = new System.Drawing.Size(389, 26);
            this.Megnevezés_napló.TabIndex = 186;
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
            this.Lapfülek.Location = new System.Drawing.Point(3, 63);
            this.Lapfülek.Name = "Lapfülek";
            this.Lapfülek.Padding = new System.Drawing.Point(16, 3);
            this.Lapfülek.SelectedIndex = 0;
            this.Lapfülek.Size = new System.Drawing.Size(1068, 461);
            this.Lapfülek.TabIndex = 204;
            this.Lapfülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LapFülek_DrawItem);
            this.Lapfülek.SelectedIndexChanged += new System.EventHandler(this.LapFülek_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.Green;
            this.TabPage1.Controls.Add(this.Vezér);
            this.TabPage1.Controls.Add(this.Törzs_excel);
            this.TabPage1.Controls.Add(this.Törzs_Új_adat);
            this.TabPage1.Controls.Add(this.Törzs_Frissít);
            this.TabPage1.Controls.Add(this.Törzs_tábla);
            this.TabPage1.Controls.Add(this.CsoportCombo);
            this.TabPage1.Controls.Add(this.Label9);
            this.TabPage1.Controls.Add(this.Méret);
            this.TabPage1.Controls.Add(this.Megnevezés);
            this.TabPage1.Controls.Add(this.Label34);
            this.TabPage1.Controls.Add(this.Label7);
            this.TabPage1.Controls.Add(this.Azonosító);
            this.TabPage1.Controls.Add(this.Label8);
            this.TabPage1.Controls.Add(this.Aktív);
            this.TabPage1.Controls.Add(this.Rögzítteljes);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1060, 428);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Törzs karbantartás";
            // 
            // Vezér
            // 
            this.Vezér.Location = new System.Drawing.Point(746, 68);
            this.Vezér.MaxLength = 15;
            this.Vezér.Name = "Vezér";
            this.Vezér.Size = new System.Drawing.Size(40, 26);
            this.Vezér.TabIndex = 204;
            this.Vezér.Visible = false;
            // 
            // Törzs_excel
            // 
            this.Törzs_excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Törzs_excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Törzs_excel.Location = new System.Drawing.Point(848, 8);
            this.Törzs_excel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Törzs_excel.Name = "Törzs_excel";
            this.Törzs_excel.Size = new System.Drawing.Size(45, 45);
            this.Törzs_excel.TabIndex = 8;
            this.toolTip1.SetToolTip(this.Törzs_excel, "A táblázatot Excelbe exportálja.");
            this.Törzs_excel.UseVisualStyleBackColor = true;
            this.Törzs_excel.Click += new System.EventHandler(this.Törzs_excel_Click);
            // 
            // Törzs_Új_adat
            // 
            this.Törzs_Új_adat.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Törzs_Új_adat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Törzs_Új_adat.Location = new System.Drawing.Point(797, 8);
            this.Törzs_Új_adat.Name = "Törzs_Új_adat";
            this.Törzs_Új_adat.Size = new System.Drawing.Size(45, 45);
            this.Törzs_Új_adat.TabIndex = 6;
            this.toolTip1.SetToolTip(this.Törzs_Új_adat, "Új adatot hoz létre.");
            this.Törzs_Új_adat.UseVisualStyleBackColor = true;
            this.Törzs_Új_adat.Click += new System.EventHandler(this.Törzs_Új_adat_Click);
            // 
            // Törzs_Frissít
            // 
            this.Törzs_Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Törzs_Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Törzs_Frissít.Location = new System.Drawing.Point(746, 8);
            this.Törzs_Frissít.Name = "Törzs_Frissít";
            this.Törzs_Frissít.Size = new System.Drawing.Size(45, 45);
            this.Törzs_Frissít.TabIndex = 7;
            this.toolTip1.SetToolTip(this.Törzs_Frissít, "Frissíti a táblázatot.");
            this.Törzs_Frissít.UseVisualStyleBackColor = true;
            this.Törzs_Frissít.Click += new System.EventHandler(this.Törzs_Frissít_Click);
            // 
            // Törzs_tábla
            // 
            this.Törzs_tábla.AllowUserToAddRows = false;
            this.Törzs_tábla.AllowUserToDeleteRows = false;
            this.Törzs_tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Törzs_tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Törzs_tábla.FilterAndSortEnabled = true;
            this.Törzs_tábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Törzs_tábla.Location = new System.Drawing.Point(6, 184);
            this.Törzs_tábla.MaxFilterButtonImageHeight = 23;
            this.Törzs_tábla.Name = "Törzs_tábla";
            this.Törzs_tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Törzs_tábla.RowHeadersVisible = false;
            this.Törzs_tábla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Törzs_tábla.Size = new System.Drawing.Size(1048, 238);
            this.Törzs_tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Törzs_tábla.TabIndex = 203;
            this.Törzs_tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Törzs_tábla_CellClick);
            this.Törzs_tábla.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Törzs_tábla_CellFormatting);
            // 
            // CsoportCombo
            // 
            this.CsoportCombo.FormattingEnabled = true;
            this.CsoportCombo.Location = new System.Drawing.Point(174, 120);
            this.CsoportCombo.MaxLength = 20;
            this.CsoportCombo.Name = "CsoportCombo";
            this.CsoportCombo.Size = new System.Drawing.Size(306, 28);
            this.CsoportCombo.TabIndex = 3;
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.BackColor = System.Drawing.Color.Silver;
            this.Label9.Location = new System.Drawing.Point(9, 128);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(111, 20);
            this.Label9.TabIndex = 114;
            this.Label9.Text = "Anyagcsoport:";
            // 
            // Méret
            // 
            this.Méret.Location = new System.Drawing.Point(174, 87);
            this.Méret.MaxLength = 20;
            this.Méret.Name = "Méret";
            this.Méret.Size = new System.Drawing.Size(180, 26);
            this.Méret.TabIndex = 2;
            // 
            // Megnevezés
            // 
            this.Megnevezés.Location = new System.Drawing.Point(174, 52);
            this.Megnevezés.MaxLength = 50;
            this.Megnevezés.Name = "Megnevezés";
            this.Megnevezés.Size = new System.Drawing.Size(550, 26);
            this.Megnevezés.TabIndex = 1;
            // 
            // Label34
            // 
            this.Label34.AutoSize = true;
            this.Label34.BackColor = System.Drawing.Color.Silver;
            this.Label34.Location = new System.Drawing.Point(9, 20);
            this.Label34.Name = "Label34";
            this.Label34.Size = new System.Drawing.Size(84, 20);
            this.Label34.TabIndex = 106;
            this.Label34.Text = "Azonosító:";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.BackColor = System.Drawing.Color.Silver;
            this.Label7.Location = new System.Drawing.Point(9, 57);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(103, 20);
            this.Label7.TabIndex = 108;
            this.Label7.Text = "Megnevezés:";
            // 
            // Azonosító
            // 
            this.Azonosító.FormattingEnabled = true;
            this.Azonosító.Location = new System.Drawing.Point(174, 15);
            this.Azonosító.MaxLength = 18;
            this.Azonosító.Name = "Azonosító";
            this.Azonosító.Size = new System.Drawing.Size(180, 28);
            this.Azonosító.TabIndex = 0;
            this.Azonosító.SelectedIndexChanged += new System.EventHandler(this.Azonosító_SelectedIndexChanged);
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.BackColor = System.Drawing.Color.Silver;
            this.Label8.Location = new System.Drawing.Point(9, 92);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(54, 20);
            this.Label8.TabIndex = 109;
            this.Label8.Text = "Méret:";
            // 
            // Aktív
            // 
            this.Aktív.AutoSize = true;
            this.Aktív.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.Aktív.Location = new System.Drawing.Point(174, 154);
            this.Aktív.Name = "Aktív";
            this.Aktív.Size = new System.Drawing.Size(68, 24);
            this.Aktív.TabIndex = 4;
            this.Aktív.Text = "Törölt";
            this.Aktív.UseVisualStyleBackColor = false;
            // 
            // Rögzítteljes
            // 
            this.Rögzítteljes.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Rögzítteljes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Rögzítteljes.Location = new System.Drawing.Point(899, 8);
            this.Rögzítteljes.Name = "Rögzítteljes";
            this.Rögzítteljes.Size = new System.Drawing.Size(45, 45);
            this.Rögzítteljes.TabIndex = 5;
            this.toolTip1.SetToolTip(this.Rögzítteljes, "Rögzíti a beviteli mezők adatait.");
            this.Rögzítteljes.UseVisualStyleBackColor = true;
            this.Rögzítteljes.Click += new System.EventHandler(this.Rögzítteljes_Click);
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.PaleGreen;
            this.TabPage2.Controls.Add(this.Tár_tábla);
            this.TabPage2.Controls.Add(this.Állvány);
            this.TabPage2.Controls.Add(this.Polc);
            this.TabPage2.Controls.Add(this.Megjegyzés);
            this.TabPage2.Controls.Add(this.Helyiség);
            this.TabPage2.Controls.Add(this.Label16);
            this.TabPage2.Controls.Add(this.Label15);
            this.TabPage2.Controls.Add(this.Label14);
            this.TabPage2.Controls.Add(this.Label12);
            this.TabPage2.Controls.Add(this.TárMegnevezés);
            this.TabPage2.Controls.Add(this.Label10);
            this.TabPage2.Controls.Add(this.Label11);
            this.TabPage2.Controls.Add(this.TárAzonosító);
            this.TabPage2.Controls.Add(this.Tár_excel);
            this.TabPage2.Controls.Add(this.Tár_frissít);
            this.TabPage2.Controls.Add(this.Tárolásihelyrögzítés);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(1060, 428);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Tárolási hely";
            // 
            // Tár_tábla
            // 
            this.Tár_tábla.AllowUserToAddRows = false;
            this.Tár_tábla.AllowUserToDeleteRows = false;
            this.Tár_tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tár_tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tár_tábla.FilterAndSortEnabled = true;
            this.Tár_tábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tár_tábla.Location = new System.Drawing.Point(7, 182);
            this.Tár_tábla.MaxFilterButtonImageHeight = 23;
            this.Tár_tábla.Name = "Tár_tábla";
            this.Tár_tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tár_tábla.RowHeadersVisible = false;
            this.Tár_tábla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Tár_tábla.Size = new System.Drawing.Size(1048, 238);
            this.Tár_tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tár_tábla.TabIndex = 204;
            this.Tár_tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tár_tábla_CellClick);
            // 
            // Állvány
            // 
            this.Állvány.Location = new System.Drawing.Point(414, 80);
            this.Állvány.MaxLength = 15;
            this.Állvány.Name = "Állvány";
            this.Állvány.Size = new System.Drawing.Size(196, 26);
            this.Állvány.TabIndex = 3;
            // 
            // Polc
            // 
            this.Polc.Location = new System.Drawing.Point(665, 80);
            this.Polc.MaxLength = 15;
            this.Polc.Name = "Polc";
            this.Polc.Size = new System.Drawing.Size(196, 26);
            this.Polc.TabIndex = 4;
            // 
            // Megjegyzés
            // 
            this.Megjegyzés.Location = new System.Drawing.Point(125, 112);
            this.Megjegyzés.MaxLength = 254;
            this.Megjegyzés.Multiline = true;
            this.Megjegyzés.Name = "Megjegyzés";
            this.Megjegyzés.Size = new System.Drawing.Size(826, 64);
            this.Megjegyzés.TabIndex = 5;
            // 
            // Helyiség
            // 
            this.Helyiség.Location = new System.Drawing.Point(125, 80);
            this.Helyiség.MaxLength = 15;
            this.Helyiség.Name = "Helyiség";
            this.Helyiség.Size = new System.Drawing.Size(215, 26);
            this.Helyiség.TabIndex = 2;
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.BackColor = System.Drawing.Color.Silver;
            this.Label16.Location = new System.Drawing.Point(10, 115);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(97, 20);
            this.Label16.TabIndex = 121;
            this.Label16.Text = "Megjegyzés:";
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.BackColor = System.Drawing.Color.Silver;
            this.Label15.Location = new System.Drawing.Point(616, 86);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(43, 20);
            this.Label15.TabIndex = 120;
            this.Label15.Text = "Polc:";
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.BackColor = System.Drawing.Color.Silver;
            this.Label14.Location = new System.Drawing.Point(346, 86);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(62, 20);
            this.Label14.TabIndex = 119;
            this.Label14.Text = "Állvány:";
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.BackColor = System.Drawing.Color.Silver;
            this.Label12.Location = new System.Drawing.Point(10, 86);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(106, 20);
            this.Label12.TabIndex = 118;
            this.Label12.Text = "Tárolás helye:";
            // 
            // TárMegnevezés
            // 
            this.TárMegnevezés.Location = new System.Drawing.Point(125, 48);
            this.TárMegnevezés.MaxLength = 50;
            this.TárMegnevezés.Name = "TárMegnevezés";
            this.TárMegnevezés.Size = new System.Drawing.Size(550, 26);
            this.TárMegnevezés.TabIndex = 1;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.BackColor = System.Drawing.Color.Silver;
            this.Label10.Location = new System.Drawing.Point(10, 17);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(84, 20);
            this.Label10.TabIndex = 114;
            this.Label10.Text = "Azonosító:";
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.BackColor = System.Drawing.Color.Silver;
            this.Label11.Location = new System.Drawing.Point(10, 51);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(103, 20);
            this.Label11.TabIndex = 116;
            this.Label11.Text = "Megnevezés:";
            // 
            // TárAzonosító
            // 
            this.TárAzonosító.FormattingEnabled = true;
            this.TárAzonosító.Location = new System.Drawing.Point(125, 14);
            this.TárAzonosító.MaxLength = 18;
            this.TárAzonosító.Name = "TárAzonosító";
            this.TárAzonosító.Size = new System.Drawing.Size(180, 28);
            this.TárAzonosító.TabIndex = 0;
            this.TárAzonosító.SelectedIndexChanged += new System.EventHandler(this.TárAzonosító_SelectedIndexChanged);
            // 
            // Tár_excel
            // 
            this.Tár_excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Tár_excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Tár_excel.Location = new System.Drawing.Point(943, 14);
            this.Tár_excel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Tár_excel.Name = "Tár_excel";
            this.Tár_excel.Size = new System.Drawing.Size(45, 45);
            this.Tár_excel.TabIndex = 8;
            this.toolTip1.SetToolTip(this.Tár_excel, "A táblázatot Excelbe exportálja.");
            this.Tár_excel.UseVisualStyleBackColor = true;
            this.Tár_excel.Click += new System.EventHandler(this.Tár_excel_Click);
            // 
            // Tár_frissít
            // 
            this.Tár_frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Tár_frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Tár_frissít.Location = new System.Drawing.Point(892, 14);
            this.Tár_frissít.Name = "Tár_frissít";
            this.Tár_frissít.Size = new System.Drawing.Size(45, 45);
            this.Tár_frissít.TabIndex = 7;
            this.toolTip1.SetToolTip(this.Tár_frissít, "Frissíti a táblázatot.");
            this.Tár_frissít.UseVisualStyleBackColor = true;
            this.Tár_frissít.Click += new System.EventHandler(this.Tár_frissít_Click);
            // 
            // Tárolásihelyrögzítés
            // 
            this.Tárolásihelyrögzítés.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Tárolásihelyrögzítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Tárolásihelyrögzítés.Location = new System.Drawing.Point(994, 14);
            this.Tárolásihelyrögzítés.Name = "Tárolásihelyrögzítés";
            this.Tárolásihelyrögzítés.Size = new System.Drawing.Size(45, 45);
            this.Tárolásihelyrögzítés.TabIndex = 6;
            this.toolTip1.SetToolTip(this.Tárolásihelyrögzítés, "Rögzíti a beviteli mezők adatait.");
            this.Tárolásihelyrögzítés.UseVisualStyleBackColor = true;
            this.Tárolásihelyrögzítés.Click += new System.EventHandler(this.Tárolásihelyrögzítés_Click);
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.TabPage3.Controls.Add(this.KépTörlés);
            this.TabPage3.Controls.Add(this.FényképLista);
            this.TabPage3.Controls.Add(this.KépHozzáad);
            this.TabPage3.Controls.Add(this.FénySorszám);
            this.TabPage3.Controls.Add(this.FényMegnevezés);
            this.TabPage3.Controls.Add(this.Label17);
            this.TabPage3.Controls.Add(this.Label18);
            this.TabPage3.Controls.Add(this.Fényazonosító);
            this.TabPage3.Controls.Add(this.Label19);
            this.TabPage3.Controls.Add(this.KépKeret);
            this.TabPage3.Controls.Add(this.Fényképfrissítés);
            this.TabPage3.Location = new System.Drawing.Point(4, 29);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage3.Size = new System.Drawing.Size(1060, 428);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Fényképek";
            // 
            // KépTörlés
            // 
            this.KépTörlés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.KépTörlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.KépTörlés.Location = new System.Drawing.Point(835, 14);
            this.KépTörlés.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.KépTörlés.Name = "KépTörlés";
            this.KépTörlés.Size = new System.Drawing.Size(45, 45);
            this.KépTörlés.TabIndex = 272;
            this.toolTip1.SetToolTip(this.KépTörlés, "Kép törlése.");
            this.KépTörlés.UseVisualStyleBackColor = true;
            this.KépTörlés.Click += new System.EventHandler(this.KépTörlés_Click);
            // 
            // FényképLista
            // 
            this.FényképLista.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.FényképLista.FormattingEnabled = true;
            this.FényképLista.ItemHeight = 20;
            this.FényképLista.Location = new System.Drawing.Point(6, 81);
            this.FényképLista.Name = "FényképLista";
            this.FényképLista.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.FényképLista.Size = new System.Drawing.Size(203, 344);
            this.FényképLista.Sorted = true;
            this.FényképLista.TabIndex = 242;
            this.FényképLista.SelectedIndexChanged += new System.EventHandler(this.FényképLista_SelectedIndexChanged);
            // 
            // KépHozzáad
            // 
            this.KépHozzáad.BackgroundImage = global::Villamos.Properties.Resources.image_add32;
            this.KépHozzáad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.KépHozzáad.Location = new System.Drawing.Point(782, 14);
            this.KépHozzáad.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.KépHozzáad.Name = "KépHozzáad";
            this.KépHozzáad.Size = new System.Drawing.Size(45, 45);
            this.KépHozzáad.TabIndex = 240;
            this.toolTip1.SetToolTip(this.KépHozzáad, "Új fénykép hozzáadása.");
            this.KépHozzáad.UseVisualStyleBackColor = true;
            this.KépHozzáad.Click += new System.EventHandler(this.KépHozzáad_Click);
            // 
            // FénySorszám
            // 
            this.FénySorszám.Location = new System.Drawing.Point(585, 14);
            this.FénySorszám.MaxLength = 15;
            this.FénySorszám.Name = "FénySorszám";
            this.FénySorszám.Size = new System.Drawing.Size(180, 26);
            this.FénySorszám.TabIndex = 120;
            // 
            // FényMegnevezés
            // 
            this.FényMegnevezés.Location = new System.Drawing.Point(215, 49);
            this.FényMegnevezés.MaxLength = 50;
            this.FényMegnevezés.Name = "FényMegnevezés";
            this.FényMegnevezés.Size = new System.Drawing.Size(550, 26);
            this.FényMegnevezés.TabIndex = 119;
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.BackColor = System.Drawing.Color.Silver;
            this.Label17.Location = new System.Drawing.Point(10, 15);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(84, 20);
            this.Label17.TabIndex = 115;
            this.Label17.Text = "Azonosító:";
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.BackColor = System.Drawing.Color.Silver;
            this.Label18.Location = new System.Drawing.Point(10, 52);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(103, 20);
            this.Label18.TabIndex = 117;
            this.Label18.Text = "Megnevezés:";
            // 
            // Fényazonosító
            // 
            this.Fényazonosító.FormattingEnabled = true;
            this.Fényazonosító.Location = new System.Drawing.Point(215, 12);
            this.Fényazonosító.MaxLength = 20;
            this.Fényazonosító.Name = "Fényazonosító";
            this.Fényazonosító.Size = new System.Drawing.Size(180, 28);
            this.Fényazonosító.TabIndex = 116;
            this.Fényazonosító.SelectedIndexChanged += new System.EventHandler(this.Fényazonosító_SelectedIndexChanged);
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.BackColor = System.Drawing.Color.Silver;
            this.Label19.Location = new System.Drawing.Point(434, 20);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(142, 20);
            this.Label19.TabIndex = 118;
            this.Label19.Text = "Fényképek száma:";
            // 
            // KépKeret
            // 
            this.KépKeret.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.KépKeret.Location = new System.Drawing.Point(215, 81);
            this.KépKeret.Name = "KépKeret";
            this.KépKeret.Size = new System.Drawing.Size(839, 341);
            this.KépKeret.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.KépKeret.TabIndex = 241;
            this.KépKeret.TabStop = false;
            // 
            // Fényképfrissítés
            // 
            this.Fényképfrissítés.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Fényképfrissítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Fényképfrissítés.Location = new System.Drawing.Point(887, 14);
            this.Fényképfrissítés.Name = "Fényképfrissítés";
            this.Fényképfrissítés.Size = new System.Drawing.Size(45, 45);
            this.Fényképfrissítés.TabIndex = 273;
            this.toolTip1.SetToolTip(this.Fényképfrissítés, "Frissíti a fényképet.");
            this.Fényképfrissítés.UseVisualStyleBackColor = true;
            this.Fényképfrissítés.Click += new System.EventHandler(this.Fényképfrissítés_Click);
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.LightSalmon;
            this.TabPage4.Controls.Add(this.Lista_megnevezés_szűrő);
            this.TabPage4.Controls.Add(this.ListaCsoportCombo);
            this.TabPage4.Controls.Add(this.Tábla);
            this.TabPage4.Controls.Add(this.Excel);
            this.TabPage4.Controls.Add(this.Command20);
            this.TabPage4.Location = new System.Drawing.Point(4, 29);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage4.Size = new System.Drawing.Size(1060, 428);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Lista";
            // 
            // Lista_megnevezés_szűrő
            // 
            this.Lista_megnevezés_szűrő.Location = new System.Drawing.Point(254, 13);
            this.Lista_megnevezés_szűrő.Name = "Lista_megnevezés_szűrő";
            this.Lista_megnevezés_szűrő.Size = new System.Drawing.Size(224, 26);
            this.Lista_megnevezés_szűrő.TabIndex = 212;
            // 
            // ListaCsoportCombo
            // 
            this.ListaCsoportCombo.FormattingEnabled = true;
            this.ListaCsoportCombo.Location = new System.Drawing.Point(5, 11);
            this.ListaCsoportCombo.Name = "ListaCsoportCombo";
            this.ListaCsoportCombo.Size = new System.Drawing.Size(243, 28);
            this.ListaCsoportCombo.TabIndex = 116;
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
            this.Tábla.Location = new System.Drawing.Point(6, 57);
            this.Tábla.MaxFilterButtonImageHeight = 23;
            this.Tábla.Name = "Tábla";
            this.Tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tábla.RowHeadersVisible = false;
            this.Tábla.Size = new System.Drawing.Size(1048, 365);
            this.Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.TabIndex = 202;
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            // 
            // Excel
            // 
            this.Excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excel.Location = new System.Drawing.Point(586, 6);
            this.Excel.Name = "Excel";
            this.Excel.Size = new System.Drawing.Size(40, 40);
            this.Excel.TabIndex = 197;
            this.toolTip1.SetToolTip(this.Excel, "A táblázatot Excelbe exportálja.");
            this.Excel.UseVisualStyleBackColor = true;
            this.Excel.Click += new System.EventHandler(this.Excel_Click);
            // 
            // Command20
            // 
            this.Command20.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Command20.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command20.Location = new System.Drawing.Point(484, 6);
            this.Command20.Name = "Command20";
            this.Command20.Size = new System.Drawing.Size(40, 40);
            this.Command20.TabIndex = 193;
            this.toolTip1.SetToolTip(this.Command20, "Frissíti a táblázatot.");
            this.Command20.UseVisualStyleBackColor = true;
            this.Command20.Click += new System.EventHandler(this.Command20_Click);
            // 
            // TabPage5
            // 
            this.TabPage5.BackColor = System.Drawing.Color.Cyan;
            this.TabPage5.Controls.Add(this.BehovaRaktár);
            this.TabPage5.Controls.Add(this.BeMegnevezés);
            this.TabPage5.Controls.Add(this.Label24);
            this.TabPage5.Controls.Add(this.Label22);
            this.TabPage5.Controls.Add(this.Label23);
            this.TabPage5.Controls.Add(this.BeAzonosító);
            this.TabPage5.Controls.Add(this.BeMennyiség);
            this.TabPage5.Controls.Add(this.Bekészlet);
            this.TabPage5.Controls.Add(this.Label21);
            this.TabPage5.Controls.Add(this.Label20);
            this.TabPage5.Controls.Add(this.BeHonnanraktár);
            this.TabPage5.Controls.Add(this.Label25);
            this.TabPage5.Controls.Add(this.BeRögzít);
            this.TabPage5.Location = new System.Drawing.Point(4, 29);
            this.TabPage5.Name = "TabPage5";
            this.TabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage5.Size = new System.Drawing.Size(1060, 428);
            this.TabPage5.TabIndex = 4;
            this.TabPage5.Text = "Beraktározás";
            // 
            // BehovaRaktár
            // 
            this.BehovaRaktár.Enabled = false;
            this.BehovaRaktár.Location = new System.Drawing.Point(160, 63);
            this.BehovaRaktár.Name = "BehovaRaktár";
            this.BehovaRaktár.Size = new System.Drawing.Size(180, 26);
            this.BehovaRaktár.TabIndex = 212;
            // 
            // BeMegnevezés
            // 
            this.BeMegnevezés.Location = new System.Drawing.Point(160, 191);
            this.BeMegnevezés.MaxLength = 50;
            this.BeMegnevezés.Name = "BeMegnevezés";
            this.BeMegnevezés.Size = new System.Drawing.Size(550, 26);
            this.BeMegnevezés.TabIndex = 211;
            // 
            // Label24
            // 
            this.Label24.AutoSize = true;
            this.Label24.BackColor = System.Drawing.Color.Silver;
            this.Label24.Location = new System.Drawing.Point(32, 239);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(85, 20);
            this.Label24.TabIndex = 209;
            this.Label24.Text = "Mennyiség";
            // 
            // Label22
            // 
            this.Label22.AutoSize = true;
            this.Label22.BackColor = System.Drawing.Color.Silver;
            this.Label22.Location = new System.Drawing.Point(32, 113);
            this.Label22.Name = "Label22";
            this.Label22.Size = new System.Drawing.Size(84, 20);
            this.Label22.TabIndex = 205;
            this.Label22.Text = "Azonosító:";
            // 
            // Label23
            // 
            this.Label23.AutoSize = true;
            this.Label23.BackColor = System.Drawing.Color.Silver;
            this.Label23.Location = new System.Drawing.Point(32, 197);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(103, 20);
            this.Label23.TabIndex = 207;
            this.Label23.Text = "Megnevezés:";
            // 
            // BeAzonosító
            // 
            this.BeAzonosító.FormattingEnabled = true;
            this.BeAzonosító.Location = new System.Drawing.Point(160, 105);
            this.BeAzonosító.MaxLength = 20;
            this.BeAzonosító.Name = "BeAzonosító";
            this.BeAzonosító.Size = new System.Drawing.Size(180, 28);
            this.BeAzonosító.TabIndex = 206;
            this.BeAzonosító.SelectedIndexChanged += new System.EventHandler(this.BeAzonosító_SelectedIndexChanged);
            // 
            // BeMennyiség
            // 
            this.BeMennyiség.Location = new System.Drawing.Point(160, 233);
            this.BeMennyiség.Name = "BeMennyiség";
            this.BeMennyiség.Size = new System.Drawing.Size(180, 26);
            this.BeMennyiség.TabIndex = 204;
            // 
            // Bekészlet
            // 
            this.Bekészlet.Enabled = false;
            this.Bekészlet.Location = new System.Drawing.Point(160, 149);
            this.Bekészlet.Name = "Bekészlet";
            this.Bekészlet.Size = new System.Drawing.Size(180, 26);
            this.Bekészlet.TabIndex = 203;
            // 
            // Label21
            // 
            this.Label21.AutoSize = true;
            this.Label21.BackColor = System.Drawing.Color.Silver;
            this.Label21.Location = new System.Drawing.Point(32, 152);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(65, 20);
            this.Label21.TabIndex = 202;
            this.Label21.Text = "Készlet:";
            // 
            // Label20
            // 
            this.Label20.AutoSize = true;
            this.Label20.BackColor = System.Drawing.Color.Silver;
            this.Label20.Location = new System.Drawing.Point(32, 69);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(50, 20);
            this.Label20.TabIndex = 200;
            this.Label20.Text = "Hova:";
            // 
            // BeHonnanraktár
            // 
            this.BeHonnanraktár.FormattingEnabled = true;
            this.BeHonnanraktár.Location = new System.Drawing.Point(160, 19);
            this.BeHonnanraktár.MaxLength = 20;
            this.BeHonnanraktár.Name = "BeHonnanraktár";
            this.BeHonnanraktár.Size = new System.Drawing.Size(180, 28);
            this.BeHonnanraktár.Sorted = true;
            this.BeHonnanraktár.TabIndex = 199;
            this.BeHonnanraktár.SelectedIndexChanged += new System.EventHandler(this.BeHonnanraktár_SelectedIndexChanged);
            // 
            // Label25
            // 
            this.Label25.AutoSize = true;
            this.Label25.BackColor = System.Drawing.Color.Silver;
            this.Label25.Location = new System.Drawing.Point(32, 27);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(70, 20);
            this.Label25.TabIndex = 198;
            this.Label25.Text = "Honnan:";
            // 
            // BeRögzít
            // 
            this.BeRögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.BeRögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BeRögzít.Location = new System.Drawing.Point(767, 214);
            this.BeRögzít.Name = "BeRögzít";
            this.BeRögzít.Size = new System.Drawing.Size(45, 45);
            this.BeRögzít.TabIndex = 210;
            this.toolTip1.SetToolTip(this.BeRögzít, "Rögzíti a beviteli mezők adatait.");
            this.BeRögzít.UseVisualStyleBackColor = true;
            this.BeRögzít.Click += new System.EventHandler(this.BeRögzít_Click);
            // 
            // TabPage6
            // 
            this.TabPage6.BackColor = System.Drawing.Color.Salmon;
            this.TabPage6.Controls.Add(this.Label36);
            this.TabPage6.Controls.Add(this.KiFelhasználás);
            this.TabPage6.Controls.Add(this.KiHonnanRaktár);
            this.TabPage6.Controls.Add(this.KiMegnevezés);
            this.TabPage6.Controls.Add(this.Label26);
            this.TabPage6.Controls.Add(this.Label27);
            this.TabPage6.Controls.Add(this.Label28);
            this.TabPage6.Controls.Add(this.Kiazonosító);
            this.TabPage6.Controls.Add(this.KiMennyiség);
            this.TabPage6.Controls.Add(this.KiKészlet);
            this.TabPage6.Controls.Add(this.Label32);
            this.TabPage6.Controls.Add(this.Label33);
            this.TabPage6.Controls.Add(this.KiHovaRaktár);
            this.TabPage6.Controls.Add(this.Label35);
            this.TabPage6.Controls.Add(this.Kirögzít);
            this.TabPage6.Location = new System.Drawing.Point(4, 29);
            this.TabPage6.Name = "TabPage6";
            this.TabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage6.Size = new System.Drawing.Size(1060, 428);
            this.TabPage6.TabIndex = 5;
            this.TabPage6.Text = "Anyagkiadás";
            // 
            // Label36
            // 
            this.Label36.AutoSize = true;
            this.Label36.BackColor = System.Drawing.Color.Silver;
            this.Label36.Location = new System.Drawing.Point(23, 293);
            this.Label36.Name = "Label36";
            this.Label36.Size = new System.Drawing.Size(172, 20);
            this.Label36.TabIndex = 227;
            this.Label36.Text = "Ki vitte el/ Mire használ:";
            // 
            // KiFelhasználás
            // 
            this.KiFelhasználás.Location = new System.Drawing.Point(201, 287);
            this.KiFelhasználás.Name = "KiFelhasználás";
            this.KiFelhasználás.Size = new System.Drawing.Size(450, 26);
            this.KiFelhasználás.TabIndex = 226;
            // 
            // KiHonnanRaktár
            // 
            this.KiHonnanRaktár.Enabled = false;
            this.KiHonnanRaktár.Location = new System.Drawing.Point(201, 34);
            this.KiHonnanRaktár.Name = "KiHonnanRaktár";
            this.KiHonnanRaktár.Size = new System.Drawing.Size(180, 26);
            this.KiHonnanRaktár.TabIndex = 225;
            // 
            // KiMegnevezés
            // 
            this.KiMegnevezés.Location = new System.Drawing.Point(201, 201);
            this.KiMegnevezés.MaxLength = 50;
            this.KiMegnevezés.Name = "KiMegnevezés";
            this.KiMegnevezés.Size = new System.Drawing.Size(550, 26);
            this.KiMegnevezés.TabIndex = 224;
            // 
            // Label26
            // 
            this.Label26.AutoSize = true;
            this.Label26.BackColor = System.Drawing.Color.Silver;
            this.Label26.Location = new System.Drawing.Point(23, 249);
            this.Label26.Name = "Label26";
            this.Label26.Size = new System.Drawing.Size(85, 20);
            this.Label26.TabIndex = 222;
            this.Label26.Text = "Mennyiség";
            // 
            // Label27
            // 
            this.Label27.AutoSize = true;
            this.Label27.BackColor = System.Drawing.Color.Silver;
            this.Label27.Location = new System.Drawing.Point(23, 123);
            this.Label27.Name = "Label27";
            this.Label27.Size = new System.Drawing.Size(84, 20);
            this.Label27.TabIndex = 219;
            this.Label27.Text = "Azonosító:";
            // 
            // Label28
            // 
            this.Label28.AutoSize = true;
            this.Label28.BackColor = System.Drawing.Color.Silver;
            this.Label28.Location = new System.Drawing.Point(23, 207);
            this.Label28.Name = "Label28";
            this.Label28.Size = new System.Drawing.Size(103, 20);
            this.Label28.TabIndex = 221;
            this.Label28.Text = "Megnevezés:";
            // 
            // Kiazonosító
            // 
            this.Kiazonosító.FormattingEnabled = true;
            this.Kiazonosító.Location = new System.Drawing.Point(201, 115);
            this.Kiazonosító.MaxLength = 20;
            this.Kiazonosító.Name = "Kiazonosító";
            this.Kiazonosító.Size = new System.Drawing.Size(180, 28);
            this.Kiazonosító.TabIndex = 220;
            this.Kiazonosító.SelectedIndexChanged += new System.EventHandler(this.Kiazonosító_SelectedIndexChanged);
            // 
            // KiMennyiség
            // 
            this.KiMennyiség.Location = new System.Drawing.Point(201, 243);
            this.KiMennyiség.Name = "KiMennyiség";
            this.KiMennyiség.Size = new System.Drawing.Size(180, 26);
            this.KiMennyiség.TabIndex = 218;
            // 
            // KiKészlet
            // 
            this.KiKészlet.Enabled = false;
            this.KiKészlet.Location = new System.Drawing.Point(201, 159);
            this.KiKészlet.Name = "KiKészlet";
            this.KiKészlet.Size = new System.Drawing.Size(180, 26);
            this.KiKészlet.TabIndex = 217;
            // 
            // Label32
            // 
            this.Label32.AutoSize = true;
            this.Label32.BackColor = System.Drawing.Color.Silver;
            this.Label32.Location = new System.Drawing.Point(23, 162);
            this.Label32.Name = "Label32";
            this.Label32.Size = new System.Drawing.Size(65, 20);
            this.Label32.TabIndex = 216;
            this.Label32.Text = "Készlet:";
            // 
            // Label33
            // 
            this.Label33.AutoSize = true;
            this.Label33.BackColor = System.Drawing.Color.Silver;
            this.Label33.Location = new System.Drawing.Point(23, 79);
            this.Label33.Name = "Label33";
            this.Label33.Size = new System.Drawing.Size(50, 20);
            this.Label33.TabIndex = 215;
            this.Label33.Text = "Hova:";
            // 
            // KiHovaRaktár
            // 
            this.KiHovaRaktár.FormattingEnabled = true;
            this.KiHovaRaktár.Location = new System.Drawing.Point(201, 76);
            this.KiHovaRaktár.MaxLength = 20;
            this.KiHovaRaktár.Name = "KiHovaRaktár";
            this.KiHovaRaktár.Size = new System.Drawing.Size(180, 28);
            this.KiHovaRaktár.Sorted = true;
            this.KiHovaRaktár.TabIndex = 214;
            // 
            // Label35
            // 
            this.Label35.AutoSize = true;
            this.Label35.BackColor = System.Drawing.Color.Silver;
            this.Label35.Location = new System.Drawing.Point(23, 37);
            this.Label35.Name = "Label35";
            this.Label35.Size = new System.Drawing.Size(70, 20);
            this.Label35.TabIndex = 213;
            this.Label35.Text = "Honnan:";
            // 
            // Kirögzít
            // 
            this.Kirögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Kirögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Kirögzít.Location = new System.Drawing.Point(759, 268);
            this.Kirögzít.Name = "Kirögzít";
            this.Kirögzít.Size = new System.Drawing.Size(45, 45);
            this.Kirögzít.TabIndex = 223;
            this.toolTip1.SetToolTip(this.Kirögzít, "Rögzíti a beviteli mezők adatait.");
            this.Kirögzít.UseVisualStyleBackColor = true;
            this.Kirögzít.Click += new System.EventHandler(this.Kirögzít_Click);
            // 
            // TabPage7
            // 
            this.TabPage7.Controls.Add(this.Napló_tábla);
            this.TabPage7.Controls.Add(this.Label1);
            this.TabPage7.Controls.Add(this.Label2);
            this.TabPage7.Controls.Add(this.Dátumig);
            this.TabPage7.Controls.Add(this.Dátumtól);
            this.TabPage7.Controls.Add(this.Label5);
            this.TabPage7.Controls.Add(this.Megnevezés_napló);
            this.TabPage7.Controls.Add(this.Label6);
            this.TabPage7.Controls.Add(this.Azonosító_napló);
            this.TabPage7.Controls.Add(this.Listáz);
            this.TabPage7.Controls.Add(this.Excelclick);
            this.TabPage7.Location = new System.Drawing.Point(4, 29);
            this.TabPage7.Name = "TabPage7";
            this.TabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage7.Size = new System.Drawing.Size(1060, 428);
            this.TabPage7.TabIndex = 6;
            this.TabPage7.Text = "Rezsi Napló";
            this.TabPage7.UseVisualStyleBackColor = true;
            // 
            // Napló_tábla
            // 
            this.Napló_tábla.AllowUserToAddRows = false;
            this.Napló_tábla.AllowUserToDeleteRows = false;
            this.Napló_tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Napló_tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Napló_tábla.FilterAndSortEnabled = true;
            this.Napló_tábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Napló_tábla.Location = new System.Drawing.Point(6, 69);
            this.Napló_tábla.MaxFilterButtonImageHeight = 23;
            this.Napló_tábla.Name = "Napló_tábla";
            this.Napló_tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Napló_tábla.RowHeadersVisible = false;
            this.Napló_tábla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Napló_tábla.Size = new System.Drawing.Size(1048, 353);
            this.Napló_tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Napló_tábla.TabIndex = 204;
            // 
            // Listáz
            // 
            this.Listáz.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Listáz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Listáz.Location = new System.Drawing.Point(926, 12);
            this.Listáz.Name = "Listáz";
            this.Listáz.Size = new System.Drawing.Size(45, 45);
            this.Listáz.TabIndex = 191;
            this.toolTip1.SetToolTip(this.Listáz, "Frissíti a táblázatot.");
            this.Listáz.UseVisualStyleBackColor = true;
            this.Listáz.Click += new System.EventHandler(this.Listáz_Click);
            // 
            // Excelclick
            // 
            this.Excelclick.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excelclick.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excelclick.Location = new System.Drawing.Point(977, 12);
            this.Excelclick.Name = "Excelclick";
            this.Excelclick.Size = new System.Drawing.Size(45, 45);
            this.Excelclick.TabIndex = 190;
            this.toolTip1.SetToolTip(this.Excelclick, "A táblázatot Excelbe exportálja.");
            this.Excelclick.UseVisualStyleBackColor = true;
            this.Excelclick.Click += new System.EventHandler(this.Excelclick_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1027, 12);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 174;
            this.toolTip1.SetToolTip(this.BtnSúgó, "Súgó");
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // Ablak_Rezsi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(1074, 527);
            this.Controls.Add(this.Lapfülek);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.Panel2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Rezsi";
            this.Text = "Rezsi anyagok";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_Rezsi_FormClosed);
            this.Load += new System.EventHandler(this.Ablak_Rezsi_könyvelés_Load);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.Lapfülek.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Törzs_tábla)).EndInit();
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tár_tábla)).EndInit();
            this.TabPage3.ResumeLayout(false);
            this.TabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KépKeret)).EndInit();
            this.TabPage4.ResumeLayout(false);
            this.TabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.TabPage5.ResumeLayout(false);
            this.TabPage5.PerformLayout();
            this.TabPage6.ResumeLayout(false);
            this.TabPage6.PerformLayout();
            this.TabPage7.ResumeLayout(false);
            this.TabPage7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Napló_tábla)).EndInit();
            this.ResumeLayout(false);

        }

        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal Button BtnSúgó;
        internal Panel Panel2;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal Label Label1;
        internal Label Label2;
        internal DateTimePicker Dátumig;
        internal DateTimePicker Dátumtól;
        internal Label Label5;
        internal Label Label6;
        internal ComboBox Azonosító_napló;
        internal TextBox Megnevezés_napló;
        internal Button Excelclick;
        internal Button Listáz;
        internal TabControl Lapfülek;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal TabPage TabPage3;
        internal TabPage TabPage4;
        internal TabPage TabPage5;
        internal TabPage TabPage6;
        internal TabPage TabPage7;
        internal ComboBox CsoportCombo;
        internal Label Label9;
        internal TextBox Méret;
        internal TextBox Megnevezés;
        internal Label Label34;
        internal Label Label7;
        internal ComboBox Azonosító;
        internal Label Label8;
        internal CheckBox Aktív;
        internal Button Rögzítteljes;
        internal TextBox Állvány;
        internal TextBox Polc;
        internal TextBox Megjegyzés;
        internal TextBox Helyiség;
        internal Label Label16;
        internal Label Label15;
        internal Label Label14;
        internal Label Label12;
        internal TextBox TárMegnevezés;
        internal Label Label10;
        internal Label Label11;
        internal ComboBox TárAzonosító;
        internal Button Tárolásihelyrögzítés;
        internal TextBox FénySorszám;
        internal TextBox FényMegnevezés;
        internal Label Label17;
        internal Label Label18;
        internal ComboBox Fényazonosító;
        internal Label Label19;
        internal PictureBox KépKeret;
        internal Button KépHozzáad;
        internal ListBox FényképLista;
        internal ComboBox ListaCsoportCombo;
        internal Zuby.ADGV.AdvancedDataGridView Tábla;
        internal Button Excel;
        internal Button Command20;
        internal TextBox BehovaRaktár;
        internal TextBox BeMegnevezés;
        internal Button BeRögzít;
        internal Label Label24;
        internal Label Label22;
        internal Label Label23;
        internal ComboBox BeAzonosító;
        internal TextBox BeMennyiség;
        internal TextBox Bekészlet;
        internal Label Label21;
        internal Label Label20;
        internal ComboBox BeHonnanraktár;
        internal Label Label25;
        internal Label Label36;
        internal TextBox KiFelhasználás;
        internal TextBox KiHonnanRaktár;
        internal TextBox KiMegnevezés;
        internal Button Kirögzít;
        internal Label Label26;
        internal Label Label27;
        internal Label Label28;
        internal ComboBox Kiazonosító;
        internal TextBox KiMennyiség;
        internal TextBox KiKészlet;
        internal Label Label32;
        internal Label Label33;
        internal ComboBox KiHovaRaktár;
        internal Label Label35;
        internal Zuby.ADGV.AdvancedDataGridView Törzs_tábla;
        internal Button Törzs_excel;
        internal Button Törzs_Új_adat;
        internal Button Törzs_Frissít;
        internal Zuby.ADGV.AdvancedDataGridView Tár_tábla;
        internal Button Tár_excel;
        internal Button Tár_frissít;
        internal Button KépTörlés;
        internal TextBox Vezér;
        internal TextBox Lista_megnevezés_szűrő;
        internal Button Fényképfrissítés;
        internal ToolTip toolTip1;
        internal Zuby.ADGV.AdvancedDataGridView Napló_tábla;
     }
}