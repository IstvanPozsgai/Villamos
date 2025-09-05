using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Villamos.V_MindenEgyéb;

namespace Villamos
{

    public partial class Ablak_alap_program_személy : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components  !=null)
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_alap_program_személy));
            this.Fülek = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.CsoportTábla = new System.Windows.Forms.DataGridView();
            this.CsoportTípus = new System.Windows.Forms.TextBox();
            this.Label27 = new System.Windows.Forms.Label();
            this.CsoportFel = new System.Windows.Forms.Button();
            this.CsoportTörlés = new System.Windows.Forms.Button();
            this.CsoportOK = new System.Windows.Forms.Button();
            this.CsoportNév = new System.Windows.Forms.TextBox();
            this.Label22 = new System.Windows.Forms.Label();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.JelenlétiText5 = new System.Windows.Forms.TextBox();
            this.label49 = new System.Windows.Forms.Label();
            this.Eszközhöz = new System.Windows.Forms.Button();
            this.Label39 = new System.Windows.Forms.Label();
            this.txtbeosztás2 = new System.Windows.Forms.TextBox();
            this.txtnév3 = new System.Windows.Forms.TextBox();
            this.txtbeosztás3 = new System.Windows.Forms.TextBox();
            this.Label35 = new System.Windows.Forms.Label();
            this.Label36 = new System.Windows.Forms.Label();
            this.Label37 = new System.Windows.Forms.Label();
            this.txtnév2 = new System.Windows.Forms.TextBox();
            this.Label38 = new System.Windows.Forms.Label();
            this.Btnfőkönyv = new System.Windows.Forms.Button();
            this.JelenlétiText4 = new System.Windows.Forms.TextBox();
            this.JelenlétiText3 = new System.Windows.Forms.TextBox();
            this.JelenlétiText2 = new System.Windows.Forms.TextBox();
            this.JelenlétiText1 = new System.Windows.Forms.TextBox();
            this.Label26 = new System.Windows.Forms.Label();
            this.Label25 = new System.Windows.Forms.Label();
            this.Label24 = new System.Windows.Forms.Label();
            this.Label23 = new System.Windows.Forms.Label();
            this.JelenlétiÜzem = new System.Windows.Forms.Button();
            this.JelenlétiFőmér = new System.Windows.Forms.Button();
            this.JelenlétiIgaz = new System.Windows.Forms.Button();
            this.JelenlétiSzerv = new System.Windows.Forms.Button();
            this.TabPage10 = new System.Windows.Forms.TabPage();
            this.BeoIdővége = new System.Windows.Forms.DateTimePicker();
            this.BeoIdőKezdete = new System.Windows.Forms.DateTimePicker();
            this.BeosztásTábla = new Zuby.ADGV.AdvancedDataGridView();
            this.BeoSzámoló = new System.Windows.Forms.CheckBox();
            this.BeoÉjszakás = new System.Windows.Forms.CheckBox();
            this.BeoKód = new System.Windows.Forms.TextBox();
            this.BeoMunkaidő = new System.Windows.Forms.TextBox();
            this.BEOMunkarend = new System.Windows.Forms.TextBox();
            this.BEOMagyarázat = new System.Windows.Forms.TextBox();
            this.BeoSorszám = new System.Windows.Forms.TextBox();
            this.Label34 = new System.Windows.Forms.Label();
            this.Label33 = new System.Windows.Forms.Label();
            this.Label32 = new System.Windows.Forms.Label();
            this.Label31 = new System.Windows.Forms.Label();
            this.Label30 = new System.Windows.Forms.Label();
            this.Label29 = new System.Windows.Forms.Label();
            this.Label28 = new System.Windows.Forms.Label();
            this.BeoFrissít = new System.Windows.Forms.Button();
            this.BeoExcel = new System.Windows.Forms.Button();
            this.BeoÚj = new System.Windows.Forms.Button();
            this.BeoTöröl = new System.Windows.Forms.Button();
            this.BeoOk = new System.Windows.Forms.Button();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.TxtPDFfájlteljes = new System.Windows.Forms.TextBox();
            this.TxtPDFfájl = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.IDoktatáselőző = new System.Windows.Forms.TextBox();
            this.TxtOktatássorszám = new System.Windows.Forms.TextBox();
            this.TxtOktatásRow = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Ismétlődés = new System.Windows.Forms.TextBox();
            this.OktDátum = new System.Windows.Forms.DateTimePicker();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.CMBStátus = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.CmbGyakoriság = new System.Windows.Forms.ComboBox();
            this.TáblaOktatás = new Zuby.ADGV.AdvancedDataGridView();
            this.CmbKategória = new System.Windows.Forms.ComboBox();
            this.Téma = new System.Windows.Forms.TextBox();
            this.TxtSorrend = new System.Windows.Forms.TextBox();
            this.Label57 = new System.Windows.Forms.Label();
            this.Label58 = new System.Windows.Forms.Label();
            this.Label59 = new System.Windows.Forms.Label();
            this.IDoktatás = new System.Windows.Forms.TextBox();
            this.Label60 = new System.Windows.Forms.Label();
            this.Button5 = new System.Windows.Forms.Button();
            this.Button12 = new System.Windows.Forms.Button();
            this.BtnOktatásÚj = new System.Windows.Forms.Button();
            this.BtnOktatásFel = new System.Windows.Forms.Button();
            this.BtnOktatásOK = new System.Windows.Forms.Button();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.PDF_néző = new PdfiumViewer.PdfViewer();
            this.TabPage5 = new System.Windows.Forms.TabPage();
            this.FeorStátus = new System.Windows.Forms.CheckBox();
            this.Feortörlés = new System.Windows.Forms.Button();
            this.FrissítMunkakör = new System.Windows.Forms.Button();
            this.FeorTábla = new Zuby.ADGV.AdvancedDataGridView();
            this.FeorFeormegnevezés = new System.Windows.Forms.TextBox();
            this.FeorFeorszám = new System.Windows.Forms.TextBox();
            this.Feorsorszám = new System.Windows.Forms.TextBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Command1 = new System.Windows.Forms.Button();
            this.Feljebb = new System.Windows.Forms.Button();
            this.Command4 = new System.Windows.Forms.Button();
            this.TabPage6 = new System.Windows.Forms.TabPage();
            this.Button2 = new System.Windows.Forms.Button();
            this.Tábla2 = new Zuby.ADGV.AdvancedDataGridView();
            this.Text4 = new System.Windows.Forms.TextBox();
            this.Text2 = new System.Windows.Forms.TextBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Command6 = new System.Windows.Forms.Button();
            this.Command5 = new System.Windows.Forms.Button();
            this.TabPage7 = new System.Windows.Forms.TabPage();
            this.Button3 = new System.Windows.Forms.Button();
            this.Label14 = new System.Windows.Forms.Label();
            this.Vonalszám = new System.Windows.Forms.TextBox();
            this.Tábla1 = new Zuby.ADGV.AdvancedDataGridView();
            this.Megnevezés = new System.Windows.Forms.TextBox();
            this.Text1 = new System.Windows.Forms.TextBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.Command2 = new System.Windows.Forms.Button();
            this.Command3 = new System.Windows.Forms.Button();
            this.TabPage8 = new System.Windows.Forms.TabPage();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.Label21 = new System.Windows.Forms.Label();
            this.Vezér2 = new System.Windows.Forms.CheckBox();
            this.Vezér1 = new System.Windows.Forms.CheckBox();
            this.Sorrend1 = new System.Windows.Forms.TextBox();
            this.Sorrend2 = new System.Windows.Forms.TextBox();
            this.Label15 = new System.Windows.Forms.Label();
            this.Csoport1 = new System.Windows.Forms.TextBox();
            this.Tábla3 = new System.Windows.Forms.DataGridView();
            this.Csoport2 = new System.Windows.Forms.TextBox();
            this.Könyvtár = new System.Windows.Forms.TextBox();
            this.Label16 = new System.Windows.Forms.Label();
            this.Label17 = new System.Windows.Forms.Label();
            this.Button4 = new System.Windows.Forms.Button();
            this.Command7 = new System.Windows.Forms.Button();
            this.Command9 = new System.Windows.Forms.Button();
            this.TabPage9 = new System.Windows.Forms.TabPage();
            this.Munka_Kategória = new System.Windows.Forms.ComboBox();
            this.label50 = new System.Windows.Forms.Label();
            this.Munka_Id = new System.Windows.Forms.TextBox();
            this.Munka_Státus = new System.Windows.Forms.CheckBox();
            this.Munka_Frissít = new System.Windows.Forms.Button();
            this.Munka_Tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.Munka_Megnevezés = new System.Windows.Forms.TextBox();
            this.Label19 = new System.Windows.Forms.Label();
            this.Label20 = new System.Windows.Forms.Label();
            this.Munka_Új = new System.Windows.Forms.Button();
            this.Munka_Rögzít = new System.Windows.Forms.Button();
            this.TabPage11 = new System.Windows.Forms.TabPage();
            this.Védő_frissít = new System.Windows.Forms.Button();
            this.Védő_tábla = new System.Windows.Forms.DataGridView();
            this.Védő_Megnevezés = new System.Windows.Forms.TextBox();
            this.Védő_id = new System.Windows.Forms.TextBox();
            this.Label40 = new System.Windows.Forms.Label();
            this.Label41 = new System.Windows.Forms.Label();
            this.Védő_új = new System.Windows.Forms.Button();
            this.Védő_rögzít = new System.Windows.Forms.Button();
            this.TabPage12 = new System.Windows.Forms.TabPage();
            this.Gondnok_Fel = new System.Windows.Forms.Button();
            this.Gond_szakszolg_szöv = new System.Windows.Forms.TextBox();
            this.Label48 = new System.Windows.Forms.Label();
            this.Gond_töröl = new System.Windows.Forms.Button();
            this.Gond_új = new System.Windows.Forms.Button();
            this.Gond_rögzít = new System.Windows.Forms.Button();
            this.Gond_Szak = new System.Windows.Forms.CheckBox();
            this.Gond_Gondnok = new System.Windows.Forms.CheckBox();
            this.Gond_beosztás = new System.Windows.Forms.TextBox();
            this.Gond_telefon = new System.Windows.Forms.TextBox();
            this.Gond_email = new System.Windows.Forms.TextBox();
            this.Gond_Név = new System.Windows.Forms.TextBox();
            this.Label47 = new System.Windows.Forms.Label();
            this.Label46 = new System.Windows.Forms.Label();
            this.Label45 = new System.Windows.Forms.Label();
            this.Label44 = new System.Windows.Forms.Label();
            this.Gond_telephely = new System.Windows.Forms.TextBox();
            this.Gond_sorszám = new System.Windows.Forms.TextBox();
            this.Label42 = new System.Windows.Forms.Label();
            this.Label43 = new System.Windows.Forms.Label();
            this.Gondnok_frissít = new System.Windows.Forms.Button();
            this.Gondnok_tábla = new System.Windows.Forms.DataGridView();
            this.tabPage13 = new System.Windows.Forms.TabPage();
            this.Eszköz_Frissít = new System.Windows.Forms.Button();
            this.Eszköz_Tábla = new System.Windows.Forms.DataGridView();
            this.Eszköz_Típus = new System.Windows.Forms.TextBox();
            this.label52 = new System.Windows.Forms.Label();
            this.LábJobb = new System.Windows.Forms.TextBox();
            this.LábKözép = new System.Windows.Forms.TextBox();
            this.LábBal = new System.Windows.Forms.TextBox();
            this.FejJobb = new System.Windows.Forms.TextBox();
            this.FejKözép = new System.Windows.Forms.TextBox();
            this.Szerszám_OK = new System.Windows.Forms.Button();
            this.label51 = new System.Windows.Forms.Label();
            this.FejBal = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Button13 = new System.Windows.Forms.Button();
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.Fülek.SuspendLayout();
            this.TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CsoportTábla)).BeginInit();
            this.TabPage2.SuspendLayout();
            this.TabPage10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BeosztásTábla)).BeginInit();
            this.TabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TáblaOktatás)).BeginInit();
            this.TabPage4.SuspendLayout();
            this.TabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FeorTábla)).BeginInit();
            this.TabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla2)).BeginInit();
            this.TabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla1)).BeginInit();
            this.TabPage8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla3)).BeginInit();
            this.TabPage9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Munka_Tábla)).BeginInit();
            this.TabPage11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Védő_tábla)).BeginInit();
            this.TabPage12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gondnok_tábla)).BeginInit();
            this.tabPage13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Eszköz_Tábla)).BeginInit();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Fülek
            // 
            this.Fülek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fülek.Controls.Add(this.TabPage1);
            this.Fülek.Controls.Add(this.TabPage2);
            this.Fülek.Controls.Add(this.TabPage10);
            this.Fülek.Controls.Add(this.TabPage3);
            this.Fülek.Controls.Add(this.TabPage4);
            this.Fülek.Controls.Add(this.TabPage5);
            this.Fülek.Controls.Add(this.TabPage6);
            this.Fülek.Controls.Add(this.TabPage7);
            this.Fülek.Controls.Add(this.TabPage8);
            this.Fülek.Controls.Add(this.TabPage9);
            this.Fülek.Controls.Add(this.TabPage11);
            this.Fülek.Controls.Add(this.TabPage12);
            this.Fülek.Controls.Add(this.tabPage13);
            this.Fülek.HotTrack = true;
            this.Fülek.Location = new System.Drawing.Point(0, 65);
            this.Fülek.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Fülek.Multiline = true;
            this.Fülek.Name = "Fülek";
            this.Fülek.Padding = new System.Drawing.Point(16, 3);
            this.Fülek.SelectedIndex = 0;
            this.Fülek.Size = new System.Drawing.Size(1196, 520);
            this.Fülek.TabIndex = 0;
            this.Fülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Fülek_DrawItem);
            this.Fülek.SelectedIndexChanged += new System.EventHandler(this.Fülek_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.RoyalBlue;
            this.TabPage1.Controls.Add(this.CsoportTábla);
            this.TabPage1.Controls.Add(this.CsoportTípus);
            this.TabPage1.Controls.Add(this.Label27);
            this.TabPage1.Controls.Add(this.CsoportFel);
            this.TabPage1.Controls.Add(this.CsoportTörlés);
            this.TabPage1.Controls.Add(this.CsoportOK);
            this.TabPage1.Controls.Add(this.CsoportNév);
            this.TabPage1.Controls.Add(this.Label22);
            this.TabPage1.Location = new System.Drawing.Point(4, 54);
            this.TabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TabPage1.Size = new System.Drawing.Size(1188, 462);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Csoportok";
            // 
            // CsoportTábla
            // 
            this.CsoportTábla.AllowUserToAddRows = false;
            this.CsoportTábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.CsoportTábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.CsoportTábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CsoportTábla.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CsoportTábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.CsoportTábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CsoportTábla.EnableHeadersVisualStyles = false;
            this.CsoportTábla.Location = new System.Drawing.Point(7, 107);
            this.CsoportTábla.Name = "CsoportTábla";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CsoportTábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.CsoportTábla.RowHeadersWidth = 51;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.CsoportTábla.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.CsoportTábla.Size = new System.Drawing.Size(1174, 342);
            this.CsoportTábla.TabIndex = 86;
            this.CsoportTábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CsoportTábla_CellClick);
            this.CsoportTábla.SelectionChanged += new System.EventHandler(this.CsoportTábla_SelectionChanged);
            // 
            // CsoportTípus
            // 
            this.CsoportTípus.Location = new System.Drawing.Point(137, 78);
            this.CsoportTípus.Name = "CsoportTípus";
            this.CsoportTípus.Size = new System.Drawing.Size(244, 26);
            this.CsoportTípus.TabIndex = 85;
            // 
            // Label27
            // 
            this.Label27.AutoSize = true;
            this.Label27.Location = new System.Drawing.Point(4, 81);
            this.Label27.Name = "Label27";
            this.Label27.Size = new System.Drawing.Size(107, 20);
            this.Label27.TabIndex = 84;
            this.Label27.Text = "Csoport típus:";
            // 
            // CsoportFel
            // 
            this.CsoportFel.BackgroundImage = global::Villamos.Properties.Resources.Up_gyűjtemény;
            this.CsoportFel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CsoportFel.Location = new System.Drawing.Point(438, 59);
            this.CsoportFel.Name = "CsoportFel";
            this.CsoportFel.Size = new System.Drawing.Size(45, 45);
            this.CsoportFel.TabIndex = 83;
            this.ToolTip1.SetToolTip(this.CsoportFel, "Feljebb viszi a sorban az adatot");
            this.CsoportFel.UseVisualStyleBackColor = true;
            this.CsoportFel.Click += new System.EventHandler(this.CsoportFel_Click);
            // 
            // CsoportTörlés
            // 
            this.CsoportTörlés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.CsoportTörlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CsoportTörlés.Location = new System.Drawing.Point(387, 59);
            this.CsoportTörlés.Name = "CsoportTörlés";
            this.CsoportTörlés.Size = new System.Drawing.Size(45, 45);
            this.CsoportTörlés.TabIndex = 81;
            this.ToolTip1.SetToolTip(this.CsoportTörlés, "Törli az adatokat");
            this.CsoportTörlés.UseVisualStyleBackColor = true;
            this.CsoportTörlés.Click += new System.EventHandler(this.CsoportTörlés_Click);
            // 
            // CsoportOK
            // 
            this.CsoportOK.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.CsoportOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CsoportOK.Location = new System.Drawing.Point(387, 10);
            this.CsoportOK.Name = "CsoportOK";
            this.CsoportOK.Size = new System.Drawing.Size(45, 45);
            this.CsoportOK.TabIndex = 53;
            this.ToolTip1.SetToolTip(this.CsoportOK, "Rögzíti/módosítja az adatokat");
            this.CsoportOK.UseVisualStyleBackColor = true;
            this.CsoportOK.Click += new System.EventHandler(this.CsoportOK_Click);
            // 
            // CsoportNév
            // 
            this.CsoportNév.Location = new System.Drawing.Point(137, 29);
            this.CsoportNév.Name = "CsoportNév";
            this.CsoportNév.Size = new System.Drawing.Size(244, 26);
            this.CsoportNév.TabIndex = 1;
            // 
            // Label22
            // 
            this.Label22.AutoSize = true;
            this.Label22.Location = new System.Drawing.Point(4, 35);
            this.Label22.Name = "Label22";
            this.Label22.Size = new System.Drawing.Size(98, 20);
            this.Label22.TabIndex = 0;
            this.Label22.Text = "Csoport név:";
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.RoyalBlue;
            this.TabPage2.Controls.Add(this.JelenlétiText5);
            this.TabPage2.Controls.Add(this.label49);
            this.TabPage2.Controls.Add(this.Eszközhöz);
            this.TabPage2.Controls.Add(this.Label39);
            this.TabPage2.Controls.Add(this.txtbeosztás2);
            this.TabPage2.Controls.Add(this.txtnév3);
            this.TabPage2.Controls.Add(this.txtbeosztás3);
            this.TabPage2.Controls.Add(this.Label35);
            this.TabPage2.Controls.Add(this.Label36);
            this.TabPage2.Controls.Add(this.Label37);
            this.TabPage2.Controls.Add(this.txtnév2);
            this.TabPage2.Controls.Add(this.Label38);
            this.TabPage2.Controls.Add(this.Btnfőkönyv);
            this.TabPage2.Controls.Add(this.JelenlétiText4);
            this.TabPage2.Controls.Add(this.JelenlétiText3);
            this.TabPage2.Controls.Add(this.JelenlétiText2);
            this.TabPage2.Controls.Add(this.JelenlétiText1);
            this.TabPage2.Controls.Add(this.Label26);
            this.TabPage2.Controls.Add(this.Label25);
            this.TabPage2.Controls.Add(this.Label24);
            this.TabPage2.Controls.Add(this.Label23);
            this.TabPage2.Controls.Add(this.JelenlétiÜzem);
            this.TabPage2.Controls.Add(this.JelenlétiFőmér);
            this.TabPage2.Controls.Add(this.JelenlétiIgaz);
            this.TabPage2.Controls.Add(this.JelenlétiSzerv);
            this.TabPage2.Location = new System.Drawing.Point(4, 54);
            this.TabPage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TabPage2.Size = new System.Drawing.Size(1188, 462);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Jelenléti ív";
            // 
            // JelenlétiText5
            // 
            this.JelenlétiText5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.JelenlétiText5.Location = new System.Drawing.Point(183, 226);
            this.JelenlétiText5.Name = "JelenlétiText5";
            this.JelenlétiText5.Size = new System.Drawing.Size(931, 26);
            this.JelenlétiText5.TabIndex = 74;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(11, 232);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(163, 20);
            this.label49.TabIndex = 72;
            this.label49.Text = "Eszközhöz szervezet:";
            // 
            // Eszközhöz
            // 
            this.Eszközhöz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Eszközhöz.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Eszközhöz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Eszközhöz.Location = new System.Drawing.Point(1132, 217);
            this.Eszközhöz.Name = "Eszközhöz";
            this.Eszközhöz.Size = new System.Drawing.Size(45, 45);
            this.Eszközhöz.TabIndex = 73;
            this.ToolTip1.SetToolTip(this.Eszközhöz, "Rögzíti/módosítja az adatokat");
            this.Eszközhöz.UseVisualStyleBackColor = true;
            this.Eszközhöz.Click += new System.EventHandler(this.Eszközhöz_Click);
            // 
            // Label39
            // 
            this.Label39.AutoSize = true;
            this.Label39.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Label39.Location = new System.Drawing.Point(11, 295);
            this.Label39.Name = "Label39";
            this.Label39.Size = new System.Drawing.Size(538, 20);
            this.Label39.TabIndex = 71;
            this.Label39.Text = "Jelenléti ív jóváhagyók (az adattartalom megegyezik a főkönyvi aláírásokkal)";
            // 
            // txtbeosztás2
            // 
            this.txtbeosztás2.Location = new System.Drawing.Point(499, 365);
            this.txtbeosztás2.Name = "txtbeosztás2";
            this.txtbeosztás2.Size = new System.Drawing.Size(300, 26);
            this.txtbeosztás2.TabIndex = 63;
            // 
            // txtnév3
            // 
            this.txtnév3.Location = new System.Drawing.Point(183, 415);
            this.txtnév3.Name = "txtnév3";
            this.txtnév3.Size = new System.Drawing.Size(300, 26);
            this.txtnév3.TabIndex = 65;
            // 
            // txtbeosztás3
            // 
            this.txtbeosztás3.Location = new System.Drawing.Point(499, 415);
            this.txtbeosztás3.Name = "txtbeosztás3";
            this.txtbeosztás3.Size = new System.Drawing.Size(300, 26);
            this.txtbeosztás3.TabIndex = 66;
            // 
            // Label35
            // 
            this.Label35.AutoSize = true;
            this.Label35.Location = new System.Drawing.Point(180, 328);
            this.Label35.Name = "Label35";
            this.Label35.Size = new System.Drawing.Size(40, 20);
            this.Label35.TabIndex = 70;
            this.Label35.Text = "Név:";
            // 
            // Label36
            // 
            this.Label36.AutoSize = true;
            this.Label36.Location = new System.Drawing.Point(496, 328);
            this.Label36.Name = "Label36";
            this.Label36.Size = new System.Drawing.Size(80, 20);
            this.Label36.TabIndex = 68;
            this.Label36.Text = "Beosztás:";
            // 
            // Label37
            // 
            this.Label37.AutoSize = true;
            this.Label37.Location = new System.Drawing.Point(56, 421);
            this.Label37.Name = "Label37";
            this.Label37.Size = new System.Drawing.Size(55, 20);
            this.Label37.TabIndex = 67;
            this.Label37.Text = "2 szint";
            // 
            // txtnév2
            // 
            this.txtnév2.Location = new System.Drawing.Point(183, 365);
            this.txtnév2.Name = "txtnév2";
            this.txtnév2.Size = new System.Drawing.Size(300, 26);
            this.txtnév2.TabIndex = 62;
            // 
            // Label38
            // 
            this.Label38.AutoSize = true;
            this.Label38.Location = new System.Drawing.Point(56, 371);
            this.Label38.Name = "Label38";
            this.Label38.Size = new System.Drawing.Size(55, 20);
            this.Label38.TabIndex = 64;
            this.Label38.Text = "1 szint";
            // 
            // Btnfőkönyv
            // 
            this.Btnfőkönyv.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Btnfőkönyv.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btnfőkönyv.Location = new System.Drawing.Point(824, 365);
            this.Btnfőkönyv.Name = "Btnfőkönyv";
            this.Btnfőkönyv.Size = new System.Drawing.Size(45, 45);
            this.Btnfőkönyv.TabIndex = 69;
            this.ToolTip1.SetToolTip(this.Btnfőkönyv, "Rögzíti/módosítja az adatokat");
            this.Btnfőkönyv.UseVisualStyleBackColor = true;
            this.Btnfőkönyv.Click += new System.EventHandler(this.Btnfőkönyv_Click);
            // 
            // JelenlétiText4
            // 
            this.JelenlétiText4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.JelenlétiText4.Location = new System.Drawing.Point(183, 176);
            this.JelenlétiText4.Name = "JelenlétiText4";
            this.JelenlétiText4.Size = new System.Drawing.Size(931, 26);
            this.JelenlétiText4.TabIndex = 61;
            // 
            // JelenlétiText3
            // 
            this.JelenlétiText3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.JelenlétiText3.Location = new System.Drawing.Point(183, 126);
            this.JelenlétiText3.Name = "JelenlétiText3";
            this.JelenlétiText3.Size = new System.Drawing.Size(931, 26);
            this.JelenlétiText3.TabIndex = 60;
            // 
            // JelenlétiText2
            // 
            this.JelenlétiText2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.JelenlétiText2.Location = new System.Drawing.Point(183, 76);
            this.JelenlétiText2.Name = "JelenlétiText2";
            this.JelenlétiText2.Size = new System.Drawing.Size(931, 26);
            this.JelenlétiText2.TabIndex = 59;
            // 
            // JelenlétiText1
            // 
            this.JelenlétiText1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.JelenlétiText1.Location = new System.Drawing.Point(183, 26);
            this.JelenlétiText1.Name = "JelenlétiText1";
            this.JelenlétiText1.Size = new System.Drawing.Size(931, 26);
            this.JelenlétiText1.TabIndex = 58;
            // 
            // Label26
            // 
            this.Label26.AutoSize = true;
            this.Label26.Location = new System.Drawing.Point(11, 182);
            this.Label26.Name = "Label26";
            this.Label26.Size = new System.Drawing.Size(158, 20);
            this.Label26.TabIndex = 3;
            this.Label26.Text = "Szakszolgálat, Üzem";
            // 
            // Label25
            // 
            this.Label25.AutoSize = true;
            this.Label25.Location = new System.Drawing.Point(11, 129);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(111, 20);
            this.Label25.TabIndex = 2;
            this.Label25.Text = "Főmérnökség:";
            // 
            // Label24
            // 
            this.Label24.AutoSize = true;
            this.Label24.Location = new System.Drawing.Point(8, 82);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(98, 20);
            this.Label24.TabIndex = 1;
            this.Label24.Text = "Igazgatóság";
            // 
            // Label23
            // 
            this.Label23.AutoSize = true;
            this.Label23.Location = new System.Drawing.Point(11, 32);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(138, 20);
            this.Label23.TabIndex = 0;
            this.Label23.Text = "Szervezeti egység";
            // 
            // JelenlétiÜzem
            // 
            this.JelenlétiÜzem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.JelenlétiÜzem.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.JelenlétiÜzem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.JelenlétiÜzem.Location = new System.Drawing.Point(1132, 167);
            this.JelenlétiÜzem.Name = "JelenlétiÜzem";
            this.JelenlétiÜzem.Size = new System.Drawing.Size(45, 45);
            this.JelenlétiÜzem.TabIndex = 57;
            this.ToolTip1.SetToolTip(this.JelenlétiÜzem, "Rögzíti/módosítja az adatokat");
            this.JelenlétiÜzem.UseVisualStyleBackColor = true;
            this.JelenlétiÜzem.Click += new System.EventHandler(this.JelenlétiÜzem_Click);
            // 
            // JelenlétiFőmér
            // 
            this.JelenlétiFőmér.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.JelenlétiFőmér.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.JelenlétiFőmér.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.JelenlétiFőmér.Location = new System.Drawing.Point(1132, 117);
            this.JelenlétiFőmér.Name = "JelenlétiFőmér";
            this.JelenlétiFőmér.Size = new System.Drawing.Size(45, 45);
            this.JelenlétiFőmér.TabIndex = 56;
            this.ToolTip1.SetToolTip(this.JelenlétiFőmér, "Rögzíti/módosítja az adatokat");
            this.JelenlétiFőmér.UseVisualStyleBackColor = true;
            this.JelenlétiFőmér.Click += new System.EventHandler(this.JelenlétiFőmér_Click);
            // 
            // JelenlétiIgaz
            // 
            this.JelenlétiIgaz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.JelenlétiIgaz.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.JelenlétiIgaz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.JelenlétiIgaz.Location = new System.Drawing.Point(1132, 67);
            this.JelenlétiIgaz.Name = "JelenlétiIgaz";
            this.JelenlétiIgaz.Size = new System.Drawing.Size(45, 45);
            this.JelenlétiIgaz.TabIndex = 55;
            this.ToolTip1.SetToolTip(this.JelenlétiIgaz, "Rögzíti/módosítja az adatokat");
            this.JelenlétiIgaz.UseVisualStyleBackColor = true;
            this.JelenlétiIgaz.Click += new System.EventHandler(this.JelenlétiIgaz_Click);
            // 
            // JelenlétiSzerv
            // 
            this.JelenlétiSzerv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.JelenlétiSzerv.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.JelenlétiSzerv.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.JelenlétiSzerv.Location = new System.Drawing.Point(1132, 17);
            this.JelenlétiSzerv.Name = "JelenlétiSzerv";
            this.JelenlétiSzerv.Size = new System.Drawing.Size(45, 45);
            this.JelenlétiSzerv.TabIndex = 54;
            this.ToolTip1.SetToolTip(this.JelenlétiSzerv, "Rögzíti/módosítja az adatokat");
            this.JelenlétiSzerv.UseVisualStyleBackColor = true;
            this.JelenlétiSzerv.Click += new System.EventHandler(this.JelenlétiSzerv_Click);
            // 
            // TabPage10
            // 
            this.TabPage10.BackColor = System.Drawing.Color.RoyalBlue;
            this.TabPage10.Controls.Add(this.BeoIdővége);
            this.TabPage10.Controls.Add(this.BeoIdőKezdete);
            this.TabPage10.Controls.Add(this.BeosztásTábla);
            this.TabPage10.Controls.Add(this.BeoSzámoló);
            this.TabPage10.Controls.Add(this.BeoÉjszakás);
            this.TabPage10.Controls.Add(this.BeoKód);
            this.TabPage10.Controls.Add(this.BeoMunkaidő);
            this.TabPage10.Controls.Add(this.BEOMunkarend);
            this.TabPage10.Controls.Add(this.BEOMagyarázat);
            this.TabPage10.Controls.Add(this.BeoSorszám);
            this.TabPage10.Controls.Add(this.Label34);
            this.TabPage10.Controls.Add(this.Label33);
            this.TabPage10.Controls.Add(this.Label32);
            this.TabPage10.Controls.Add(this.Label31);
            this.TabPage10.Controls.Add(this.Label30);
            this.TabPage10.Controls.Add(this.Label29);
            this.TabPage10.Controls.Add(this.Label28);
            this.TabPage10.Controls.Add(this.BeoFrissít);
            this.TabPage10.Controls.Add(this.BeoExcel);
            this.TabPage10.Controls.Add(this.BeoÚj);
            this.TabPage10.Controls.Add(this.BeoTöröl);
            this.TabPage10.Controls.Add(this.BeoOk);
            this.TabPage10.Location = new System.Drawing.Point(4, 54);
            this.TabPage10.Name = "TabPage10";
            this.TabPage10.Size = new System.Drawing.Size(1188, 462);
            this.TabPage10.TabIndex = 9;
            this.TabPage10.Text = "Beosztás kódok";
            // 
            // BeoIdővége
            // 
            this.BeoIdővége.CustomFormat = "";
            this.BeoIdővége.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.BeoIdővége.Location = new System.Drawing.Point(351, 33);
            this.BeoIdővége.Name = "BeoIdővége";
            this.BeoIdővége.Size = new System.Drawing.Size(105, 26);
            this.BeoIdővége.TabIndex = 94;
            this.BeoIdővége.Value = new System.DateTime(2022, 11, 19, 6, 0, 0, 0);
            // 
            // BeoIdőKezdete
            // 
            this.BeoIdőKezdete.CustomFormat = "";
            this.BeoIdőKezdete.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.BeoIdőKezdete.Location = new System.Drawing.Point(202, 33);
            this.BeoIdőKezdete.Name = "BeoIdőKezdete";
            this.BeoIdőKezdete.Size = new System.Drawing.Size(105, 26);
            this.BeoIdőKezdete.TabIndex = 93;
            this.BeoIdőKezdete.Value = new System.DateTime(2022, 11, 19, 6, 0, 0, 0);
            // 
            // BeosztásTábla
            // 
            this.BeosztásTábla.AllowUserToAddRows = false;
            this.BeosztásTábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.BeosztásTábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.BeosztásTábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BeosztásTábla.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.BeosztásTábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.BeosztásTábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BeosztásTábla.EnableHeadersVisualStyles = false;
            this.BeosztásTábla.FilterAndSortEnabled = true;
            this.BeosztásTábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.BeosztásTábla.Location = new System.Drawing.Point(3, 101);
            this.BeosztásTábla.MaxFilterButtonImageHeight = 23;
            this.BeosztásTábla.Name = "BeosztásTábla";
            this.BeosztásTábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.BeosztásTábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.BeosztásTábla.RowHeadersWidth = 51;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            this.BeosztásTábla.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.BeosztásTábla.Size = new System.Drawing.Size(1181, 344);
            this.BeosztásTábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.BeosztásTábla.TabIndex = 87;
            this.BeosztásTábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.BeosztásTábla_CellClick);
            this.BeosztásTábla.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.BeosztásTábla_CellDoubleClick);
            this.BeosztásTábla.SelectionChanged += new System.EventHandler(this.BeosztásTábla_SelectionChanged);
            // 
            // BeoSzámoló
            // 
            this.BeoSzámoló.AutoSize = true;
            this.BeoSzámoló.BackColor = System.Drawing.Color.CornflowerBlue;
            this.BeoSzámoló.Location = new System.Drawing.Point(664, 71);
            this.BeoSzámoló.Name = "BeoSzámoló";
            this.BeoSzámoló.Size = new System.Drawing.Size(160, 24);
            this.BeoSzámoló.TabIndex = 13;
            this.BeoSzámoló.Text = "Munkalap számoló";
            this.BeoSzámoló.UseVisualStyleBackColor = false;
            // 
            // BeoÉjszakás
            // 
            this.BeoÉjszakás.AutoSize = true;
            this.BeoÉjszakás.BackColor = System.Drawing.Color.CornflowerBlue;
            this.BeoÉjszakás.Location = new System.Drawing.Point(664, 35);
            this.BeoÉjszakás.Name = "BeoÉjszakás";
            this.BeoÉjszakás.Size = new System.Drawing.Size(92, 24);
            this.BeoÉjszakás.TabIndex = 12;
            this.BeoÉjszakás.Text = "Éjszakás";
            this.BeoÉjszakás.UseVisualStyleBackColor = false;
            // 
            // BeoKód
            // 
            this.BeoKód.Location = new System.Drawing.Point(90, 35);
            this.BeoKód.MaxLength = 3;
            this.BeoKód.Name = "BeoKód";
            this.BeoKód.Size = new System.Drawing.Size(106, 26);
            this.BeoKód.TabIndex = 11;
            // 
            // BeoMunkaidő
            // 
            this.BeoMunkaidő.Location = new System.Drawing.Point(477, 35);
            this.BeoMunkaidő.MaxLength = 4;
            this.BeoMunkaidő.Name = "BeoMunkaidő";
            this.BeoMunkaidő.Size = new System.Drawing.Size(81, 26);
            this.BeoMunkaidő.TabIndex = 10;
            // 
            // BEOMunkarend
            // 
            this.BEOMunkarend.Location = new System.Drawing.Point(564, 35);
            this.BEOMunkarend.MaxLength = 3;
            this.BEOMunkarend.Name = "BEOMunkarend";
            this.BEOMunkarend.Size = new System.Drawing.Size(94, 26);
            this.BEOMunkarend.TabIndex = 9;
            // 
            // BEOMagyarázat
            // 
            this.BEOMagyarázat.Location = new System.Drawing.Point(115, 69);
            this.BEOMagyarázat.Name = "BEOMagyarázat";
            this.BEOMagyarázat.Size = new System.Drawing.Size(543, 26);
            this.BEOMagyarázat.TabIndex = 8;
            // 
            // BeoSorszám
            // 
            this.BeoSorszám.Location = new System.Drawing.Point(8, 35);
            this.BeoSorszám.Name = "BeoSorszám";
            this.BeoSorszám.Size = new System.Drawing.Size(76, 26);
            this.BeoSorszám.TabIndex = 7;
            // 
            // Label34
            // 
            this.Label34.AutoSize = true;
            this.Label34.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Label34.Location = new System.Drawing.Point(8, 75);
            this.Label34.Name = "Label34";
            this.Label34.Size = new System.Drawing.Size(96, 20);
            this.Label34.TabIndex = 6;
            this.Label34.Text = "Magyarázat:";
            // 
            // Label33
            // 
            this.Label33.AutoSize = true;
            this.Label33.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Label33.Location = new System.Drawing.Point(565, 12);
            this.Label33.Name = "Label33";
            this.Label33.Size = new System.Drawing.Size(93, 20);
            this.Label33.TabIndex = 5;
            this.Label33.Text = "Munkarend:";
            // 
            // Label32
            // 
            this.Label32.AutoSize = true;
            this.Label32.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Label32.Location = new System.Drawing.Point(477, 12);
            this.Label32.Name = "Label32";
            this.Label32.Size = new System.Drawing.Size(82, 20);
            this.Label32.TabIndex = 4;
            this.Label32.Text = "Munkaidő:";
            // 
            // Label31
            // 
            this.Label31.AutoSize = true;
            this.Label31.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Label31.Location = new System.Drawing.Point(351, 12);
            this.Label31.Name = "Label31";
            this.Label31.Size = new System.Drawing.Size(120, 20);
            this.Label31.TabIndex = 3;
            this.Label31.Text = "Munkaidő vége:";
            // 
            // Label30
            // 
            this.Label30.AutoSize = true;
            this.Label30.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Label30.Location = new System.Drawing.Point(202, 12);
            this.Label30.Name = "Label30";
            this.Label30.Size = new System.Drawing.Size(143, 20);
            this.Label30.TabIndex = 2;
            this.Label30.Text = "Munkaidő kezdete:";
            // 
            // Label29
            // 
            this.Label29.AutoSize = true;
            this.Label29.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Label29.Location = new System.Drawing.Point(90, 12);
            this.Label29.Name = "Label29";
            this.Label29.Size = new System.Drawing.Size(106, 20);
            this.Label29.TabIndex = 1;
            this.Label29.Text = "Beosztáskód:";
            // 
            // Label28
            // 
            this.Label28.AutoSize = true;
            this.Label28.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Label28.Location = new System.Drawing.Point(8, 12);
            this.Label28.Name = "Label28";
            this.Label28.Size = new System.Drawing.Size(76, 20);
            this.Label28.TabIndex = 0;
            this.Label28.Text = "Sorszám:";
            // 
            // BeoFrissít
            // 
            this.BeoFrissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.BeoFrissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BeoFrissít.Location = new System.Drawing.Point(953, 50);
            this.BeoFrissít.Name = "BeoFrissít";
            this.BeoFrissít.Size = new System.Drawing.Size(45, 45);
            this.BeoFrissít.TabIndex = 92;
            this.ToolTip1.SetToolTip(this.BeoFrissít, "Frissíti a táblázatot");
            this.BeoFrissít.UseVisualStyleBackColor = true;
            this.BeoFrissít.Click += new System.EventHandler(this.BeoFrissít_Click);
            // 
            // BeoExcel
            // 
            this.BeoExcel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.BeoExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BeoExcel.Location = new System.Drawing.Point(1031, 50);
            this.BeoExcel.Name = "BeoExcel";
            this.BeoExcel.Size = new System.Drawing.Size(45, 45);
            this.BeoExcel.TabIndex = 91;
            this.ToolTip1.SetToolTip(this.BeoExcel, "Excel táblázatot készít a táblázatból");
            this.BeoExcel.UseVisualStyleBackColor = true;
            this.BeoExcel.Click += new System.EventHandler(this.BeoExcel_Click);
            // 
            // BeoÚj
            // 
            this.BeoÚj.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.BeoÚj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BeoÚj.Location = new System.Drawing.Point(902, 50);
            this.BeoÚj.Name = "BeoÚj";
            this.BeoÚj.Size = new System.Drawing.Size(45, 45);
            this.BeoÚj.TabIndex = 90;
            this.ToolTip1.SetToolTip(this.BeoÚj, "Új adatnak előkészíti a beviteli mezőt");
            this.BeoÚj.UseVisualStyleBackColor = true;
            this.BeoÚj.Click += new System.EventHandler(this.BeoÚj_Click);
            // 
            // BeoTöröl
            // 
            this.BeoTöröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.BeoTöröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BeoTöröl.Location = new System.Drawing.Point(851, 50);
            this.BeoTöröl.Name = "BeoTöröl";
            this.BeoTöröl.Size = new System.Drawing.Size(45, 45);
            this.BeoTöröl.TabIndex = 89;
            this.ToolTip1.SetToolTip(this.BeoTöröl, "Törli az adatokat");
            this.BeoTöröl.UseVisualStyleBackColor = true;
            this.BeoTöröl.Click += new System.EventHandler(this.BeoTöröl_Click);
            // 
            // BeoOk
            // 
            this.BeoOk.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.BeoOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BeoOk.Location = new System.Drawing.Point(851, 3);
            this.BeoOk.Name = "BeoOk";
            this.BeoOk.Size = new System.Drawing.Size(45, 45);
            this.BeoOk.TabIndex = 88;
            this.ToolTip1.SetToolTip(this.BeoOk, "Rögzíti/módosítja az adatokat");
            this.BeoOk.UseVisualStyleBackColor = true;
            this.BeoOk.Click += new System.EventHandler(this.BeoOk_Click);
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.YellowGreen;
            this.TabPage3.Controls.Add(this.TxtPDFfájlteljes);
            this.TabPage3.Controls.Add(this.TxtPDFfájl);
            this.TabPage3.Controls.Add(this.Label5);
            this.TabPage3.Controls.Add(this.IDoktatáselőző);
            this.TabPage3.Controls.Add(this.TxtOktatássorszám);
            this.TabPage3.Controls.Add(this.TxtOktatásRow);
            this.TabPage3.Controls.Add(this.Label4);
            this.TabPage3.Controls.Add(this.Ismétlődés);
            this.TabPage3.Controls.Add(this.OktDátum);
            this.TabPage3.Controls.Add(this.Label3);
            this.TabPage3.Controls.Add(this.Label2);
            this.TabPage3.Controls.Add(this.CMBStátus);
            this.TabPage3.Controls.Add(this.Label1);
            this.TabPage3.Controls.Add(this.CmbGyakoriság);
            this.TabPage3.Controls.Add(this.TáblaOktatás);
            this.TabPage3.Controls.Add(this.CmbKategória);
            this.TabPage3.Controls.Add(this.Téma);
            this.TabPage3.Controls.Add(this.TxtSorrend);
            this.TabPage3.Controls.Add(this.Label57);
            this.TabPage3.Controls.Add(this.Label58);
            this.TabPage3.Controls.Add(this.Label59);
            this.TabPage3.Controls.Add(this.IDoktatás);
            this.TabPage3.Controls.Add(this.Label60);
            this.TabPage3.Controls.Add(this.Button5);
            this.TabPage3.Controls.Add(this.Button12);
            this.TabPage3.Controls.Add(this.BtnOktatásÚj);
            this.TabPage3.Controls.Add(this.BtnOktatásFel);
            this.TabPage3.Controls.Add(this.BtnOktatásOK);
            this.TabPage3.Location = new System.Drawing.Point(4, 54);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(1188, 462);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Oktatások";
            // 
            // TxtPDFfájlteljes
            // 
            this.TxtPDFfájlteljes.Location = new System.Drawing.Point(891, 226);
            this.TxtPDFfájlteljes.Name = "TxtPDFfájlteljes";
            this.TxtPDFfájlteljes.Size = new System.Drawing.Size(61, 26);
            this.TxtPDFfájlteljes.TabIndex = 72;
            this.TxtPDFfájlteljes.Visible = false;
            // 
            // TxtPDFfájl
            // 
            this.TxtPDFfájl.Enabled = false;
            this.TxtPDFfájl.Location = new System.Drawing.Point(612, 152);
            this.TxtPDFfájl.Name = "TxtPDFfájl";
            this.TxtPDFfájl.Size = new System.Drawing.Size(443, 26);
            this.TxtPDFfájl.TabIndex = 57;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(468, 160);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(103, 20);
            this.Label5.TabIndex = 58;
            this.Label5.Text = "PDF fájlneve:";
            // 
            // IDoktatáselőző
            // 
            this.IDoktatáselőző.Enabled = false;
            this.IDoktatáselőző.Location = new System.Drawing.Point(705, 226);
            this.IDoktatáselőző.Name = "IDoktatáselőző";
            this.IDoktatáselőző.Size = new System.Drawing.Size(87, 26);
            this.IDoktatáselőző.TabIndex = 55;
            this.IDoktatáselőző.Visible = false;
            // 
            // TxtOktatássorszám
            // 
            this.TxtOktatássorszám.Enabled = false;
            this.TxtOktatássorszám.Location = new System.Drawing.Point(798, 226);
            this.TxtOktatássorszám.Name = "TxtOktatássorszám";
            this.TxtOktatássorszám.Size = new System.Drawing.Size(87, 26);
            this.TxtOktatássorszám.TabIndex = 54;
            this.TxtOktatássorszám.Visible = false;
            // 
            // TxtOktatásRow
            // 
            this.TxtOktatásRow.Enabled = false;
            this.TxtOktatásRow.Location = new System.Drawing.Point(612, 226);
            this.TxtOktatásRow.Name = "TxtOktatásRow";
            this.TxtOktatásRow.Size = new System.Drawing.Size(87, 26);
            this.TxtOktatásRow.TabIndex = 53;
            this.TxtOktatásRow.Visible = false;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(468, 124);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(138, 20);
            this.Label4.TabIndex = 52;
            this.Label4.Text = "Gyakoriság hónap";
            // 
            // Ismétlődés
            // 
            this.Ismétlődés.Location = new System.Drawing.Point(612, 120);
            this.Ismétlődés.Name = "Ismétlődés";
            this.Ismétlődés.Size = new System.Drawing.Size(87, 26);
            this.Ismétlődés.TabIndex = 51;
            // 
            // OktDátum
            // 
            this.OktDátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.OktDátum.Location = new System.Drawing.Point(157, 191);
            this.OktDátum.Name = "OktDátum";
            this.OktDátum.Size = new System.Drawing.Size(107, 26);
            this.OktDátum.TabIndex = 48;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(11, 196);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(57, 20);
            this.Label3.TabIndex = 47;
            this.Label3.Text = "Dátum";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(11, 160);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(56, 20);
            this.Label2.TabIndex = 46;
            this.Label2.Text = "Státus";
            // 
            // CMBStátus
            // 
            this.CMBStátus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CMBStátus.FormattingEnabled = true;
            this.CMBStátus.Location = new System.Drawing.Point(157, 152);
            this.CMBStátus.Name = "CMBStátus";
            this.CMBStátus.Size = new System.Drawing.Size(284, 28);
            this.CMBStátus.TabIndex = 45;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(11, 124);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(89, 20);
            this.Label1.TabIndex = 44;
            this.Label1.Text = "Gyakoriság";
            // 
            // CmbGyakoriság
            // 
            this.CmbGyakoriság.FormattingEnabled = true;
            this.CmbGyakoriság.Location = new System.Drawing.Point(157, 116);
            this.CmbGyakoriság.Name = "CmbGyakoriság";
            this.CmbGyakoriság.Size = new System.Drawing.Size(284, 28);
            this.CmbGyakoriság.TabIndex = 43;
            // 
            // TáblaOktatás
            // 
            this.TáblaOktatás.AllowUserToAddRows = false;
            this.TáblaOktatás.AllowUserToDeleteRows = false;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TáblaOktatás.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.TáblaOktatás.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TáblaOktatás.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TáblaOktatás.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.TáblaOktatás.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TáblaOktatás.EnableHeadersVisualStyles = false;
            this.TáblaOktatás.FilterAndSortEnabled = true;
            this.TáblaOktatás.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.TáblaOktatás.Location = new System.Drawing.Point(3, 268);
            this.TáblaOktatás.MaxFilterButtonImageHeight = 23;
            this.TáblaOktatás.Name = "TáblaOktatás";
            this.TáblaOktatás.RightToLeft = System.Windows.Forms.RightToLeft.No;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TáblaOktatás.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.TáblaOktatás.RowHeadersWidth = 51;
            this.TáblaOktatás.Size = new System.Drawing.Size(1181, 177);
            this.TáblaOktatás.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.TáblaOktatás.TabIndex = 41;
            this.TáblaOktatás.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TáblaOktatás_CellClick);
            this.TáblaOktatás.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TáblaOktatás_CellDoubleClick);
            this.TáblaOktatás.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.TáblaOktatás_CellFormatting);
            // 
            // CmbKategória
            // 
            this.CmbKategória.FormattingEnabled = true;
            this.CmbKategória.Location = new System.Drawing.Point(157, 80);
            this.CmbKategória.Name = "CmbKategória";
            this.CmbKategória.Size = new System.Drawing.Size(284, 28);
            this.CmbKategória.TabIndex = 34;
            // 
            // Téma
            // 
            this.Téma.Location = new System.Drawing.Point(157, 46);
            this.Téma.Name = "Téma";
            this.Téma.Size = new System.Drawing.Size(775, 26);
            this.Téma.TabIndex = 35;
            // 
            // TxtSorrend
            // 
            this.TxtSorrend.Enabled = false;
            this.TxtSorrend.Location = new System.Drawing.Point(157, 226);
            this.TxtSorrend.Name = "TxtSorrend";
            this.TxtSorrend.Size = new System.Drawing.Size(187, 26);
            this.TxtSorrend.TabIndex = 36;
            // 
            // Label57
            // 
            this.Label57.AutoSize = true;
            this.Label57.Location = new System.Drawing.Point(11, 88);
            this.Label57.Name = "Label57";
            this.Label57.Size = new System.Drawing.Size(81, 20);
            this.Label57.TabIndex = 40;
            this.Label57.Text = "Kategória:";
            // 
            // Label58
            // 
            this.Label58.AutoSize = true;
            this.Label58.Location = new System.Drawing.Point(11, 52);
            this.Label58.Name = "Label58";
            this.Label58.Size = new System.Drawing.Size(117, 20);
            this.Label58.TabIndex = 39;
            this.Label58.Text = "Oktatás témája";
            // 
            // Label59
            // 
            this.Label59.AutoSize = true;
            this.Label59.Location = new System.Drawing.Point(11, 232);
            this.Label59.Name = "Label59";
            this.Label59.Size = new System.Drawing.Size(129, 20);
            this.Label59.TabIndex = 38;
            this.Label59.Text = "Listázási sorrend";
            // 
            // IDoktatás
            // 
            this.IDoktatás.Enabled = false;
            this.IDoktatás.Location = new System.Drawing.Point(157, 10);
            this.IDoktatás.Name = "IDoktatás";
            this.IDoktatás.Size = new System.Drawing.Size(87, 26);
            this.IDoktatás.TabIndex = 33;
            // 
            // Label60
            // 
            this.Label60.AutoSize = true;
            this.Label60.Location = new System.Drawing.Point(11, 16);
            this.Label60.Name = "Label60";
            this.Label60.Size = new System.Drawing.Size(76, 20);
            this.Label60.TabIndex = 37;
            this.Label60.Text = "Sorszám:";
            // 
            // Button5
            // 
            this.Button5.BackgroundImage = global::Villamos.Properties.Resources.Folder_;
            this.Button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button5.Location = new System.Drawing.Point(1073, 135);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(45, 45);
            this.Button5.TabIndex = 71;
            this.ToolTip1.SetToolTip(this.Button5, "PDF fájl kiválasztása");
            this.Button5.UseVisualStyleBackColor = true;
            this.Button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // Button12
            // 
            this.Button12.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Button12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button12.Location = new System.Drawing.Point(1132, 191);
            this.Button12.Name = "Button12";
            this.Button12.Size = new System.Drawing.Size(45, 45);
            this.Button12.TabIndex = 56;
            this.ToolTip1.SetToolTip(this.Button12, "Excel táblába kiírja az adatokat");
            this.Button12.UseVisualStyleBackColor = true;
            this.Button12.Click += new System.EventHandler(this.Button12_Click);
            // 
            // BtnOktatásÚj
            // 
            this.BtnOktatásÚj.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.BtnOktatásÚj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnOktatásÚj.Location = new System.Drawing.Point(1132, 80);
            this.BtnOktatásÚj.Name = "BtnOktatásÚj";
            this.BtnOktatásÚj.Size = new System.Drawing.Size(45, 45);
            this.BtnOktatásÚj.TabIndex = 50;
            this.ToolTip1.SetToolTip(this.BtnOktatásÚj, "Új adatnak előkészíti a beviteli mezőt");
            this.BtnOktatásÚj.UseVisualStyleBackColor = true;
            this.BtnOktatásÚj.Click += new System.EventHandler(this.BtnOktatásÚj_Click);
            // 
            // BtnOktatásFel
            // 
            this.BtnOktatásFel.BackgroundImage = global::Villamos.Properties.Resources.Up_gyűjtemény;
            this.BtnOktatásFel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnOktatásFel.Location = new System.Drawing.Point(447, 207);
            this.BtnOktatásFel.Name = "BtnOktatásFel";
            this.BtnOktatásFel.Size = new System.Drawing.Size(45, 45);
            this.BtnOktatásFel.TabIndex = 49;
            this.ToolTip1.SetToolTip(this.BtnOktatásFel, "Feljebb viszi a sorban az adatot");
            this.BtnOktatásFel.UseVisualStyleBackColor = true;
            this.BtnOktatásFel.Click += new System.EventHandler(this.BtnOktatásFel_Click);
            // 
            // BtnOktatásOK
            // 
            this.BtnOktatásOK.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.BtnOktatásOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnOktatásOK.Location = new System.Drawing.Point(1132, 16);
            this.BtnOktatásOK.Name = "BtnOktatásOK";
            this.BtnOktatásOK.Size = new System.Drawing.Size(45, 45);
            this.BtnOktatásOK.TabIndex = 42;
            this.ToolTip1.SetToolTip(this.BtnOktatásOK, "Rögzíti/módosítja az adatokat");
            this.BtnOktatásOK.UseVisualStyleBackColor = true;
            this.BtnOktatásOK.Click += new System.EventHandler(this.BtnOktatásOK_Click);
            // 
            // TabPage4
            // 
            this.TabPage4.Controls.Add(this.PDF_néző);
            this.TabPage4.Location = new System.Drawing.Point(4, 54);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Size = new System.Drawing.Size(1188, 462);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "PDF";
            this.TabPage4.UseVisualStyleBackColor = true;
            // 
            // PDF_néző
            // 
            this.PDF_néző.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PDF_néző.Location = new System.Drawing.Point(4, 5);
            this.PDF_néző.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PDF_néző.Name = "PDF_néző";
            this.PDF_néző.ShowToolbar = false;
            this.PDF_néző.Size = new System.Drawing.Size(1179, 448);
            this.PDF_néző.TabIndex = 0;
            this.PDF_néző.ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitWidth;
            // 
            // TabPage5
            // 
            this.TabPage5.BackColor = System.Drawing.Color.SandyBrown;
            this.TabPage5.Controls.Add(this.FeorStátus);
            this.TabPage5.Controls.Add(this.Feortörlés);
            this.TabPage5.Controls.Add(this.FrissítMunkakör);
            this.TabPage5.Controls.Add(this.FeorTábla);
            this.TabPage5.Controls.Add(this.FeorFeormegnevezés);
            this.TabPage5.Controls.Add(this.FeorFeorszám);
            this.TabPage5.Controls.Add(this.Feorsorszám);
            this.TabPage5.Controls.Add(this.Label8);
            this.TabPage5.Controls.Add(this.Label7);
            this.TabPage5.Controls.Add(this.Label6);
            this.TabPage5.Controls.Add(this.Command1);
            this.TabPage5.Controls.Add(this.Feljebb);
            this.TabPage5.Controls.Add(this.Command4);
            this.TabPage5.Location = new System.Drawing.Point(4, 54);
            this.TabPage5.Name = "TabPage5";
            this.TabPage5.Size = new System.Drawing.Size(1188, 462);
            this.TabPage5.TabIndex = 4;
            this.TabPage5.Text = "Munkakörök";
            // 
            // FeorStátus
            // 
            this.FeorStátus.AutoSize = true;
            this.FeorStátus.Location = new System.Drawing.Point(781, 50);
            this.FeorStátus.Name = "FeorStátus";
            this.FeorStátus.Size = new System.Drawing.Size(68, 24);
            this.FeorStátus.TabIndex = 82;
            this.FeorStátus.Text = "Törölt";
            this.FeorStátus.UseVisualStyleBackColor = true;
            // 
            // Feortörlés
            // 
            this.Feortörlés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Feortörlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Feortörlés.Location = new System.Drawing.Point(1015, 67);
            this.Feortörlés.Name = "Feortörlés";
            this.Feortörlés.Size = new System.Drawing.Size(45, 45);
            this.Feortörlés.TabIndex = 81;
            this.ToolTip1.SetToolTip(this.Feortörlés, "Törli az adatokat");
            this.Feortörlés.UseVisualStyleBackColor = true;
            this.Feortörlés.Click += new System.EventHandler(this.Feortörlés_Click);
            // 
            // FrissítMunkakör
            // 
            this.FrissítMunkakör.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.FrissítMunkakör.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FrissítMunkakör.Location = new System.Drawing.Point(1066, 67);
            this.FrissítMunkakör.Name = "FrissítMunkakör";
            this.FrissítMunkakör.Size = new System.Drawing.Size(45, 45);
            this.FrissítMunkakör.TabIndex = 55;
            this.ToolTip1.SetToolTip(this.FrissítMunkakör, "Frissíti a táblázatot");
            this.FrissítMunkakör.UseVisualStyleBackColor = true;
            this.FrissítMunkakör.Click += new System.EventHandler(this.FrissítMunkakör_Click);
            // 
            // FeorTábla
            // 
            this.FeorTábla.AllowUserToAddRows = false;
            this.FeorTábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.FeorTábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle12;
            this.FeorTábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FeorTábla.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.FeorTábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.FeorTábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FeorTábla.EnableHeadersVisualStyles = false;
            this.FeorTábla.FilterAndSortEnabled = true;
            this.FeorTábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.FeorTábla.Location = new System.Drawing.Point(8, 118);
            this.FeorTábla.MaxFilterButtonImageHeight = 23;
            this.FeorTábla.Name = "FeorTábla";
            this.FeorTábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.FeorTábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.FeorTábla.RowHeadersWidth = 51;
            this.FeorTábla.Size = new System.Drawing.Size(1176, 327);
            this.FeorTábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.FeorTábla.TabIndex = 54;
            this.FeorTábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.FeorTábla_CellClick);
            this.FeorTábla.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.FeorTábla_CellDoubleClick);
            this.FeorTábla.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.FeorTábla_CellFormatting);
            this.FeorTábla.SelectionChanged += new System.EventHandler(this.FeorTábla_SelectionChanged);
            // 
            // FeorFeormegnevezés
            // 
            this.FeorFeormegnevezés.Location = new System.Drawing.Point(169, 81);
            this.FeorFeormegnevezés.MaxLength = 50;
            this.FeorFeormegnevezés.Name = "FeorFeormegnevezés";
            this.FeorFeormegnevezés.Size = new System.Drawing.Size(680, 26);
            this.FeorFeormegnevezés.TabIndex = 5;
            // 
            // FeorFeorszám
            // 
            this.FeorFeorszám.Location = new System.Drawing.Point(169, 48);
            this.FeorFeorszám.MaxLength = 10;
            this.FeorFeorszám.Name = "FeorFeorszám";
            this.FeorFeorszám.Size = new System.Drawing.Size(139, 26);
            this.FeorFeorszám.TabIndex = 4;
            // 
            // Feorsorszám
            // 
            this.Feorsorszám.Enabled = false;
            this.Feorsorszám.Location = new System.Drawing.Point(169, 12);
            this.Feorsorszám.Name = "Feorsorszám";
            this.Feorsorszám.Size = new System.Drawing.Size(97, 26);
            this.Feorsorszám.TabIndex = 3;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(11, 87);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(140, 20);
            this.Label8.TabIndex = 2;
            this.Label8.Text = "Feor megnevezés:";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(11, 51);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(84, 20);
            this.Label7.TabIndex = 1;
            this.Label7.Text = "Feorszám:";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(11, 15);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(76, 20);
            this.Label6.TabIndex = 0;
            this.Label6.Text = "Sorszám:";
            // 
            // Command1
            // 
            this.Command1.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Command1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command1.Location = new System.Drawing.Point(913, 67);
            this.Command1.Name = "Command1";
            this.Command1.Size = new System.Drawing.Size(45, 45);
            this.Command1.TabIndex = 53;
            this.ToolTip1.SetToolTip(this.Command1, "Új adatnak előkészíti a beviteli mezőt");
            this.Command1.UseVisualStyleBackColor = true;
            this.Command1.Click += new System.EventHandler(this.Command1_Click);
            // 
            // Feljebb
            // 
            this.Feljebb.BackgroundImage = global::Villamos.Properties.Resources.Up_gyűjtemény;
            this.Feljebb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Feljebb.Location = new System.Drawing.Point(964, 67);
            this.Feljebb.Name = "Feljebb";
            this.Feljebb.Size = new System.Drawing.Size(45, 45);
            this.Feljebb.TabIndex = 52;
            this.ToolTip1.SetToolTip(this.Feljebb, "Feljebb viszi a sorban az adatot");
            this.Feljebb.UseVisualStyleBackColor = true;
            this.Feljebb.Click += new System.EventHandler(this.Feljebb_Click);
            // 
            // Command4
            // 
            this.Command4.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Command4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command4.Location = new System.Drawing.Point(913, 12);
            this.Command4.Name = "Command4";
            this.Command4.Size = new System.Drawing.Size(45, 45);
            this.Command4.TabIndex = 51;
            this.ToolTip1.SetToolTip(this.Command4, "Rögzíti/módosítja az adatokat");
            this.Command4.UseVisualStyleBackColor = true;
            this.Command4.Click += new System.EventHandler(this.Command4_Click);
            // 
            // TabPage6
            // 
            this.TabPage6.BackColor = System.Drawing.Color.SandyBrown;
            this.TabPage6.Controls.Add(this.Button2);
            this.TabPage6.Controls.Add(this.Tábla2);
            this.TabPage6.Controls.Add(this.Text4);
            this.TabPage6.Controls.Add(this.Text2);
            this.TabPage6.Controls.Add(this.Label9);
            this.TabPage6.Controls.Add(this.Label10);
            this.TabPage6.Controls.Add(this.Command6);
            this.TabPage6.Controls.Add(this.Command5);
            this.TabPage6.Location = new System.Drawing.Point(4, 54);
            this.TabPage6.Name = "TabPage6";
            this.TabPage6.Size = new System.Drawing.Size(1188, 462);
            this.TabPage6.TabIndex = 5;
            this.TabPage6.Text = "Jogosítvány Típus";
            // 
            // Button2
            // 
            this.Button2.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button2.Location = new System.Drawing.Point(591, 53);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(45, 45);
            this.Button2.TabIndex = 59;
            this.ToolTip1.SetToolTip(this.Button2, "Frissíti a táblázatot");
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Tábla2
            // 
            this.Tábla2.AllowUserToAddRows = false;
            this.Tábla2.AllowUserToDeleteRows = false;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Tábla2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle15;
            this.Tábla2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.Tábla2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla2.EnableHeadersVisualStyles = false;
            this.Tábla2.FilterAndSortEnabled = true;
            this.Tábla2.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla2.Location = new System.Drawing.Point(8, 104);
            this.Tábla2.MaxFilterButtonImageHeight = 23;
            this.Tábla2.Name = "Tábla2";
            this.Tábla2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla2.RowHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this.Tábla2.RowHeadersWidth = 51;
            this.Tábla2.Size = new System.Drawing.Size(1176, 341);
            this.Tábla2.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla2.TabIndex = 58;
            this.Tábla2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla2_CellClick);
            this.Tábla2.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla2_CellDoubleClick);
            this.Tábla2.SelectionChanged += new System.EventHandler(this.Tábla2_SelectionChanged);
            // 
            // Text4
            // 
            this.Text4.Location = new System.Drawing.Point(134, 59);
            this.Text4.MaxLength = 50;
            this.Text4.Name = "Text4";
            this.Text4.Size = new System.Drawing.Size(354, 26);
            this.Text4.TabIndex = 57;
            // 
            // Text2
            // 
            this.Text2.Enabled = false;
            this.Text2.Location = new System.Drawing.Point(134, 23);
            this.Text2.Name = "Text2";
            this.Text2.Size = new System.Drawing.Size(97, 26);
            this.Text2.TabIndex = 56;
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(11, 65);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(103, 20);
            this.Label9.TabIndex = 55;
            this.Label9.Text = "Megnevezés:";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(11, 29);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(76, 20);
            this.Label10.TabIndex = 54;
            this.Label10.Text = "Sorszám:";
            // 
            // Command6
            // 
            this.Command6.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Command6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command6.Location = new System.Drawing.Point(540, 53);
            this.Command6.Name = "Command6";
            this.Command6.Size = new System.Drawing.Size(45, 45);
            this.Command6.TabIndex = 53;
            this.ToolTip1.SetToolTip(this.Command6, "Új adatnak előkészíti a beviteli mezőt");
            this.Command6.UseVisualStyleBackColor = true;
            this.Command6.Click += new System.EventHandler(this.Command6_Click);
            // 
            // Command5
            // 
            this.Command5.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Command5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command5.Location = new System.Drawing.Point(592, 4);
            this.Command5.Name = "Command5";
            this.Command5.Size = new System.Drawing.Size(45, 45);
            this.Command5.TabIndex = 52;
            this.ToolTip1.SetToolTip(this.Command5, "Rögzíti/módosítja az adatokat");
            this.Command5.UseVisualStyleBackColor = true;
            this.Command5.Click += new System.EventHandler(this.Command5_Click);
            // 
            // TabPage7
            // 
            this.TabPage7.BackColor = System.Drawing.Color.SandyBrown;
            this.TabPage7.Controls.Add(this.Button3);
            this.TabPage7.Controls.Add(this.Label14);
            this.TabPage7.Controls.Add(this.Vonalszám);
            this.TabPage7.Controls.Add(this.Tábla1);
            this.TabPage7.Controls.Add(this.Megnevezés);
            this.TabPage7.Controls.Add(this.Text1);
            this.TabPage7.Controls.Add(this.Label11);
            this.TabPage7.Controls.Add(this.Label12);
            this.TabPage7.Controls.Add(this.Command2);
            this.TabPage7.Controls.Add(this.Command3);
            this.TabPage7.Location = new System.Drawing.Point(4, 54);
            this.TabPage7.Name = "TabPage7";
            this.TabPage7.Size = new System.Drawing.Size(1188, 462);
            this.TabPage7.TabIndex = 6;
            this.TabPage7.Text = "Jogosítvány Vonalismeret";
            // 
            // Button3
            // 
            this.Button3.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button3.Location = new System.Drawing.Point(992, 65);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(45, 45);
            this.Button3.TabIndex = 68;
            this.ToolTip1.SetToolTip(this.Button3, "Frissíti a táblázatot");
            this.Button3.UseVisualStyleBackColor = true;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(11, 58);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(92, 20);
            this.Label14.TabIndex = 67;
            this.Label14.Text = "Vonalszám:";
            // 
            // Vonalszám
            // 
            this.Vonalszám.Location = new System.Drawing.Point(134, 52);
            this.Vonalszám.MaxLength = 10;
            this.Vonalszám.Name = "Vonalszám";
            this.Vonalszám.Size = new System.Drawing.Size(218, 26);
            this.Vonalszám.TabIndex = 66;
            // 
            // Tábla1
            // 
            this.Tábla1.AllowUserToAddRows = false;
            this.Tábla1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Tábla1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle18;
            this.Tábla1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle19;
            this.Tábla1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla1.EnableHeadersVisualStyles = false;
            this.Tábla1.FilterAndSortEnabled = true;
            this.Tábla1.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla1.Location = new System.Drawing.Point(4, 116);
            this.Tábla1.MaxFilterButtonImageHeight = 23;
            this.Tábla1.Name = "Tábla1";
            this.Tábla1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla1.RowHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.Tábla1.RowHeadersWidth = 51;
            this.Tábla1.Size = new System.Drawing.Size(1180, 354);
            this.Tábla1.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla1.TabIndex = 65;
            this.Tábla1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla1_CellClick);
            this.Tábla1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla1_CellDoubleClick);
            this.Tábla1.SelectionChanged += new System.EventHandler(this.Tábla1_SelectionChanged);
            // 
            // Megnevezés
            // 
            this.Megnevezés.Location = new System.Drawing.Point(134, 84);
            this.Megnevezés.MaxLength = 255;
            this.Megnevezés.Name = "Megnevezés";
            this.Megnevezés.Size = new System.Drawing.Size(801, 26);
            this.Megnevezés.TabIndex = 64;
            // 
            // Text1
            // 
            this.Text1.Enabled = false;
            this.Text1.Location = new System.Drawing.Point(134, 16);
            this.Text1.Name = "Text1";
            this.Text1.Size = new System.Drawing.Size(97, 26);
            this.Text1.TabIndex = 63;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(8, 87);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(103, 20);
            this.Label11.TabIndex = 62;
            this.Label11.Text = "Megnevezés:";
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(11, 19);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(76, 20);
            this.Label12.TabIndex = 61;
            this.Label12.Text = "Sorszám:";
            // 
            // Command2
            // 
            this.Command2.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Command2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command2.Location = new System.Drawing.Point(941, 65);
            this.Command2.Name = "Command2";
            this.Command2.Size = new System.Drawing.Size(45, 45);
            this.Command2.TabIndex = 60;
            this.ToolTip1.SetToolTip(this.Command2, "Új adatnak előkészíti a beviteli mezőt");
            this.Command2.UseVisualStyleBackColor = true;
            this.Command2.Click += new System.EventHandler(this.Command2_Click);
            // 
            // Command3
            // 
            this.Command3.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Command3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command3.Location = new System.Drawing.Point(992, 16);
            this.Command3.Name = "Command3";
            this.Command3.Size = new System.Drawing.Size(45, 45);
            this.Command3.TabIndex = 59;
            this.ToolTip1.SetToolTip(this.Command3, "Rögzíti/módosítja az adatokat");
            this.Command3.UseVisualStyleBackColor = true;
            this.Command3.Click += new System.EventHandler(this.Command3_Click);
            // 
            // TabPage8
            // 
            this.TabPage8.BackColor = System.Drawing.Color.SandyBrown;
            this.TabPage8.Controls.Add(this.TextBox1);
            this.TabPage8.Controls.Add(this.Label21);
            this.TabPage8.Controls.Add(this.Vezér2);
            this.TabPage8.Controls.Add(this.Vezér1);
            this.TabPage8.Controls.Add(this.Sorrend1);
            this.TabPage8.Controls.Add(this.Sorrend2);
            this.TabPage8.Controls.Add(this.Label15);
            this.TabPage8.Controls.Add(this.Csoport1);
            this.TabPage8.Controls.Add(this.Tábla3);
            this.TabPage8.Controls.Add(this.Csoport2);
            this.TabPage8.Controls.Add(this.Könyvtár);
            this.TabPage8.Controls.Add(this.Label16);
            this.TabPage8.Controls.Add(this.Label17);
            this.TabPage8.Controls.Add(this.Button4);
            this.TabPage8.Controls.Add(this.Command7);
            this.TabPage8.Controls.Add(this.Command9);
            this.TabPage8.Location = new System.Drawing.Point(4, 54);
            this.TabPage8.Name = "TabPage8";
            this.TabPage8.Size = new System.Drawing.Size(1188, 462);
            this.TabPage8.TabIndex = 7;
            this.TabPage8.Text = "Szervezeti könyvtár";
            // 
            // TextBox1
            // 
            this.TextBox1.Enabled = false;
            this.TextBox1.Location = new System.Drawing.Point(163, 22);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(103, 26);
            this.TextBox1.TabIndex = 85;
            // 
            // Label21
            // 
            this.Label21.AutoSize = true;
            this.Label21.Location = new System.Drawing.Point(11, 28);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(76, 20);
            this.Label21.TabIndex = 84;
            this.Label21.Text = "Sorszám:";
            // 
            // Vezér2
            // 
            this.Vezér2.AutoSize = true;
            this.Vezér2.Location = new System.Drawing.Point(279, 91);
            this.Vezér2.Name = "Vezér2";
            this.Vezér2.Size = new System.Drawing.Size(138, 24);
            this.Vezér2.TabIndex = 82;
            this.Vezér2.Text = "Vezérkönyvtár2";
            this.Vezér2.UseVisualStyleBackColor = true;
            // 
            // Vezér1
            // 
            this.Vezér1.AutoSize = true;
            this.Vezér1.Location = new System.Drawing.Point(279, 61);
            this.Vezér1.Name = "Vezér1";
            this.Vezér1.Size = new System.Drawing.Size(138, 24);
            this.Vezér1.TabIndex = 81;
            this.Vezér1.Text = "Vezérkönyvtár1";
            this.Vezér1.UseVisualStyleBackColor = true;
            // 
            // Sorrend1
            // 
            this.Sorrend1.Location = new System.Drawing.Point(423, 61);
            this.Sorrend1.Name = "Sorrend1";
            this.Sorrend1.Size = new System.Drawing.Size(103, 26);
            this.Sorrend1.TabIndex = 78;
            // 
            // Sorrend2
            // 
            this.Sorrend2.Location = new System.Drawing.Point(423, 93);
            this.Sorrend2.Name = "Sorrend2";
            this.Sorrend2.Size = new System.Drawing.Size(103, 26);
            this.Sorrend2.TabIndex = 77;
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Location = new System.Drawing.Point(8, 64);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(146, 20);
            this.Label15.TabIndex = 76;
            this.Label15.Text = "Csoport sorszám 1:";
            // 
            // Csoport1
            // 
            this.Csoport1.Location = new System.Drawing.Point(163, 58);
            this.Csoport1.Name = "Csoport1";
            this.Csoport1.Size = new System.Drawing.Size(103, 26);
            this.Csoport1.TabIndex = 75;
            // 
            // Tábla3
            // 
            this.Tábla3.AllowUserToAddRows = false;
            this.Tábla3.AllowUserToDeleteRows = false;
            dataGridViewCellStyle21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Tábla3.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle21;
            this.Tábla3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla3.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle22;
            this.Tábla3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla3.EnableHeadersVisualStyles = false;
            this.Tábla3.Location = new System.Drawing.Point(8, 125);
            this.Tábla3.Name = "Tábla3";
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla3.RowHeadersDefaultCellStyle = dataGridViewCellStyle23;
            this.Tábla3.RowHeadersWidth = 51;
            this.Tábla3.Size = new System.Drawing.Size(1176, 345);
            this.Tábla3.TabIndex = 74;
            this.Tábla3.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla3_CellClick);
            this.Tábla3.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla3_CellDoubleClick);
            this.Tábla3.SelectionChanged += new System.EventHandler(this.Tábla3_SelectionChanged);
            // 
            // Csoport2
            // 
            this.Csoport2.Location = new System.Drawing.Point(163, 90);
            this.Csoport2.Name = "Csoport2";
            this.Csoport2.Size = new System.Drawing.Size(103, 26);
            this.Csoport2.TabIndex = 73;
            // 
            // Könyvtár
            // 
            this.Könyvtár.Location = new System.Drawing.Point(423, 22);
            this.Könyvtár.Name = "Könyvtár";
            this.Könyvtár.Size = new System.Drawing.Size(282, 26);
            this.Könyvtár.TabIndex = 72;
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Location = new System.Drawing.Point(8, 93);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(146, 20);
            this.Label16.TabIndex = 71;
            this.Label16.Text = "Csoport sorszám 2:";
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.Location = new System.Drawing.Point(275, 28);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(74, 20);
            this.Label17.TabIndex = 70;
            this.Label17.Text = "Könyvtár:";
            // 
            // Button4
            // 
            this.Button4.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button4.Location = new System.Drawing.Point(790, 73);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(45, 45);
            this.Button4.TabIndex = 83;
            this.ToolTip1.SetToolTip(this.Button4, "Frissíti a táblázatot");
            this.Button4.UseVisualStyleBackColor = true;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // Command7
            // 
            this.Command7.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Command7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command7.Location = new System.Drawing.Point(739, 73);
            this.Command7.Name = "Command7";
            this.Command7.Size = new System.Drawing.Size(45, 45);
            this.Command7.TabIndex = 69;
            this.ToolTip1.SetToolTip(this.Command7, "Új adatnak előkészíti a beviteli mezőt");
            this.Command7.UseVisualStyleBackColor = true;
            this.Command7.Click += new System.EventHandler(this.Command7_Click);
            // 
            // Command9
            // 
            this.Command9.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Command9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command9.Location = new System.Drawing.Point(790, 22);
            this.Command9.Name = "Command9";
            this.Command9.Size = new System.Drawing.Size(45, 45);
            this.Command9.TabIndex = 68;
            this.ToolTip1.SetToolTip(this.Command9, "Rögzíti/módosítja az adatokat");
            this.Command9.UseVisualStyleBackColor = true;
            this.Command9.Click += new System.EventHandler(this.Command9_Click);
            // 
            // TabPage9
            // 
            this.TabPage9.BackColor = System.Drawing.Color.SandyBrown;
            this.TabPage9.Controls.Add(this.Munka_Kategória);
            this.TabPage9.Controls.Add(this.label50);
            this.TabPage9.Controls.Add(this.Munka_Id);
            this.TabPage9.Controls.Add(this.Munka_Státus);
            this.TabPage9.Controls.Add(this.Munka_Frissít);
            this.TabPage9.Controls.Add(this.Munka_Tábla);
            this.TabPage9.Controls.Add(this.Munka_Megnevezés);
            this.TabPage9.Controls.Add(this.Label19);
            this.TabPage9.Controls.Add(this.Label20);
            this.TabPage9.Controls.Add(this.Munka_Új);
            this.TabPage9.Controls.Add(this.Munka_Rögzít);
            this.TabPage9.Location = new System.Drawing.Point(4, 54);
            this.TabPage9.Name = "TabPage9";
            this.TabPage9.Size = new System.Drawing.Size(1188, 462);
            this.TabPage9.TabIndex = 8;
            this.TabPage9.Text = "Feltölthető dokumentumok";
            // 
            // Munka_Kategória
            // 
            this.Munka_Kategória.FormattingEnabled = true;
            this.Munka_Kategória.Location = new System.Drawing.Point(163, 87);
            this.Munka_Kategória.Name = "Munka_Kategória";
            this.Munka_Kategória.Size = new System.Drawing.Size(321, 28);
            this.Munka_Kategória.TabIndex = 96;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(11, 95);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(81, 20);
            this.label50.TabIndex = 95;
            this.label50.Text = "Kategória:";
            // 
            // Munka_Id
            // 
            this.Munka_Id.Enabled = false;
            this.Munka_Id.Location = new System.Drawing.Point(163, 19);
            this.Munka_Id.Name = "Munka_Id";
            this.Munka_Id.Size = new System.Drawing.Size(80, 26);
            this.Munka_Id.TabIndex = 94;
            // 
            // Munka_Státus
            // 
            this.Munka_Státus.AutoSize = true;
            this.Munka_Státus.Location = new System.Drawing.Point(163, 121);
            this.Munka_Státus.Name = "Munka_Státus";
            this.Munka_Státus.Size = new System.Drawing.Size(68, 24);
            this.Munka_Státus.TabIndex = 93;
            this.Munka_Státus.Text = "Törölt";
            this.Munka_Státus.UseVisualStyleBackColor = true;
            // 
            // Munka_Frissít
            // 
            this.Munka_Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Munka_Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Munka_Frissít.Location = new System.Drawing.Point(674, 70);
            this.Munka_Frissít.Name = "Munka_Frissít";
            this.Munka_Frissít.Size = new System.Drawing.Size(45, 45);
            this.Munka_Frissít.TabIndex = 92;
            this.ToolTip1.SetToolTip(this.Munka_Frissít, "Frissíti a táblázatot");
            this.Munka_Frissít.UseVisualStyleBackColor = true;
            this.Munka_Frissít.Click += new System.EventHandler(this.Munka_Frissít_Click);
            // 
            // Munka_Tábla
            // 
            this.Munka_Tábla.AllowUserToAddRows = false;
            this.Munka_Tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Munka_Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle24;
            this.Munka_Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Munka_Tábla.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle25.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle25.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle25.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle25.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Munka_Tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle25;
            this.Munka_Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Munka_Tábla.EnableHeadersVisualStyles = false;
            this.Munka_Tábla.FilterAndSortEnabled = true;
            this.Munka_Tábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Munka_Tábla.Location = new System.Drawing.Point(3, 151);
            this.Munka_Tábla.MaxFilterButtonImageHeight = 23;
            this.Munka_Tábla.Name = "Munka_Tábla";
            this.Munka_Tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle26.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle26.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle26.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle26.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Munka_Tábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle26;
            this.Munka_Tábla.RowHeadersWidth = 51;
            this.Munka_Tábla.Size = new System.Drawing.Size(1181, 304);
            this.Munka_Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Munka_Tábla.TabIndex = 86;
            this.Munka_Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Munka_Tábla_CellClick);
            this.Munka_Tábla.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Munka_Tábla_CellDoubleClick);
            this.Munka_Tábla.SelectionChanged += new System.EventHandler(this.Munka_Tábla_SelectionChanged);
            // 
            // Munka_Megnevezés
            // 
            this.Munka_Megnevezés.Location = new System.Drawing.Point(163, 55);
            this.Munka_Megnevezés.Name = "Munka_Megnevezés";
            this.Munka_Megnevezés.Size = new System.Drawing.Size(454, 26);
            this.Munka_Megnevezés.TabIndex = 85;
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.Location = new System.Drawing.Point(11, 19);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(76, 20);
            this.Label19.TabIndex = 84;
            this.Label19.Text = "Sorszám:";
            // 
            // Label20
            // 
            this.Label20.AutoSize = true;
            this.Label20.Location = new System.Drawing.Point(11, 58);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(103, 20);
            this.Label20.TabIndex = 83;
            this.Label20.Text = "Megnevezés:";
            // 
            // Munka_Új
            // 
            this.Munka_Új.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Munka_Új.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Munka_Új.Location = new System.Drawing.Point(623, 70);
            this.Munka_Új.Name = "Munka_Új";
            this.Munka_Új.Size = new System.Drawing.Size(45, 45);
            this.Munka_Új.TabIndex = 82;
            this.ToolTip1.SetToolTip(this.Munka_Új, "Új adatnak előkészíti a beviteli mezőt");
            this.Munka_Új.UseVisualStyleBackColor = true;
            this.Munka_Új.Click += new System.EventHandler(this.Munka_Új_Click);
            // 
            // Munka_Rögzít
            // 
            this.Munka_Rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Munka_Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Munka_Rögzít.Location = new System.Drawing.Point(674, 7);
            this.Munka_Rögzít.Name = "Munka_Rögzít";
            this.Munka_Rögzít.Size = new System.Drawing.Size(45, 45);
            this.Munka_Rögzít.TabIndex = 81;
            this.ToolTip1.SetToolTip(this.Munka_Rögzít, "Rögzíti/módosítja az adatokat");
            this.Munka_Rögzít.UseVisualStyleBackColor = true;
            this.Munka_Rögzít.Click += new System.EventHandler(this.Munka_Rögzít_Click);
            // 
            // TabPage11
            // 
            this.TabPage11.BackColor = System.Drawing.Color.SandyBrown;
            this.TabPage11.Controls.Add(this.Védő_frissít);
            this.TabPage11.Controls.Add(this.Védő_tábla);
            this.TabPage11.Controls.Add(this.Védő_Megnevezés);
            this.TabPage11.Controls.Add(this.Védő_id);
            this.TabPage11.Controls.Add(this.Label40);
            this.TabPage11.Controls.Add(this.Label41);
            this.TabPage11.Controls.Add(this.Védő_új);
            this.TabPage11.Controls.Add(this.Védő_rögzít);
            this.TabPage11.Location = new System.Drawing.Point(4, 54);
            this.TabPage11.Name = "TabPage11";
            this.TabPage11.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage11.Size = new System.Drawing.Size(1188, 462);
            this.TabPage11.TabIndex = 10;
            this.TabPage11.Text = "Védőeszköz";
            // 
            // Védő_frissít
            // 
            this.Védő_frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Védő_frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Védő_frissít.Location = new System.Drawing.Point(692, 58);
            this.Védő_frissít.Name = "Védő_frissít";
            this.Védő_frissít.Size = new System.Drawing.Size(45, 45);
            this.Védő_frissít.TabIndex = 67;
            this.ToolTip1.SetToolTip(this.Védő_frissít, "Frissíti a táblázatot");
            this.Védő_frissít.UseVisualStyleBackColor = true;
            this.Védő_frissít.Click += new System.EventHandler(this.Védő_frissít_Click);
            // 
            // Védő_tábla
            // 
            this.Védő_tábla.AllowUserToAddRows = false;
            this.Védő_tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Védő_tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle27;
            this.Védő_tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Védő_tábla.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle28.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle28.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle28.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle28.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle28.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Védő_tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle28;
            this.Védő_tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Védő_tábla.EnableHeadersVisualStyles = false;
            this.Védő_tábla.Location = new System.Drawing.Point(6, 114);
            this.Védő_tábla.Name = "Védő_tábla";
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle29.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle29.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle29.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle29.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle29.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Védő_tábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle29;
            this.Védő_tábla.RowHeadersWidth = 51;
            this.Védő_tábla.Size = new System.Drawing.Size(1176, 331);
            this.Védő_tábla.TabIndex = 66;
            this.Védő_tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Védő_tábla_CellClick);
            // 
            // Védő_Megnevezés
            // 
            this.Védő_Megnevezés.Location = new System.Drawing.Point(134, 64);
            this.Védő_Megnevezés.MaxLength = 20;
            this.Védő_Megnevezés.Name = "Védő_Megnevezés";
            this.Védő_Megnevezés.Size = new System.Drawing.Size(465, 26);
            this.Védő_Megnevezés.TabIndex = 65;
            // 
            // Védő_id
            // 
            this.Védő_id.Location = new System.Drawing.Point(134, 28);
            this.Védő_id.Name = "Védő_id";
            this.Védő_id.Size = new System.Drawing.Size(97, 26);
            this.Védő_id.TabIndex = 64;
            // 
            // Label40
            // 
            this.Label40.AutoSize = true;
            this.Label40.Location = new System.Drawing.Point(4, 70);
            this.Label40.Name = "Label40";
            this.Label40.Size = new System.Drawing.Size(103, 20);
            this.Label40.TabIndex = 63;
            this.Label40.Text = "Megnevezés:";
            // 
            // Label41
            // 
            this.Label41.AutoSize = true;
            this.Label41.Location = new System.Drawing.Point(6, 34);
            this.Label41.Name = "Label41";
            this.Label41.Size = new System.Drawing.Size(76, 20);
            this.Label41.TabIndex = 62;
            this.Label41.Text = "Sorszám:";
            // 
            // Védő_új
            // 
            this.Védő_új.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Védő_új.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Védő_új.Location = new System.Drawing.Point(641, 58);
            this.Védő_új.Name = "Védő_új";
            this.Védő_új.Size = new System.Drawing.Size(45, 45);
            this.Védő_új.TabIndex = 61;
            this.ToolTip1.SetToolTip(this.Védő_új, "Új adatnak előkészíti a beviteli mezőt");
            this.Védő_új.UseVisualStyleBackColor = true;
            this.Védő_új.Click += new System.EventHandler(this.Védő_új_Click);
            // 
            // Védő_rögzít
            // 
            this.Védő_rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Védő_rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Védő_rögzít.Location = new System.Drawing.Point(692, 9);
            this.Védő_rögzít.Name = "Védő_rögzít";
            this.Védő_rögzít.Size = new System.Drawing.Size(45, 45);
            this.Védő_rögzít.TabIndex = 60;
            this.ToolTip1.SetToolTip(this.Védő_rögzít, "Rögzíti/módosítja az adatokat");
            this.Védő_rögzít.UseVisualStyleBackColor = true;
            this.Védő_rögzít.Click += new System.EventHandler(this.Védő_rögzít_Click);
            // 
            // TabPage12
            // 
            this.TabPage12.BackColor = System.Drawing.Color.SandyBrown;
            this.TabPage12.Controls.Add(this.Gondnok_Fel);
            this.TabPage12.Controls.Add(this.Gond_szakszolg_szöv);
            this.TabPage12.Controls.Add(this.Label48);
            this.TabPage12.Controls.Add(this.Gond_töröl);
            this.TabPage12.Controls.Add(this.Gond_új);
            this.TabPage12.Controls.Add(this.Gond_rögzít);
            this.TabPage12.Controls.Add(this.Gond_Szak);
            this.TabPage12.Controls.Add(this.Gond_Gondnok);
            this.TabPage12.Controls.Add(this.Gond_beosztás);
            this.TabPage12.Controls.Add(this.Gond_telefon);
            this.TabPage12.Controls.Add(this.Gond_email);
            this.TabPage12.Controls.Add(this.Gond_Név);
            this.TabPage12.Controls.Add(this.Label47);
            this.TabPage12.Controls.Add(this.Label46);
            this.TabPage12.Controls.Add(this.Label45);
            this.TabPage12.Controls.Add(this.Label44);
            this.TabPage12.Controls.Add(this.Gond_telephely);
            this.TabPage12.Controls.Add(this.Gond_sorszám);
            this.TabPage12.Controls.Add(this.Label42);
            this.TabPage12.Controls.Add(this.Label43);
            this.TabPage12.Controls.Add(this.Gondnok_frissít);
            this.TabPage12.Controls.Add(this.Gondnok_tábla);
            this.TabPage12.Location = new System.Drawing.Point(4, 54);
            this.TabPage12.Name = "TabPage12";
            this.TabPage12.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage12.Size = new System.Drawing.Size(1188, 462);
            this.TabPage12.TabIndex = 11;
            this.TabPage12.Text = "Gondnokok";
            // 
            // Gondnok_Fel
            // 
            this.Gondnok_Fel.BackgroundImage = global::Villamos.Properties.Resources.Up_gyűjtemény;
            this.Gondnok_Fel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Gondnok_Fel.Location = new System.Drawing.Point(976, 77);
            this.Gondnok_Fel.Name = "Gondnok_Fel";
            this.Gondnok_Fel.Size = new System.Drawing.Size(45, 45);
            this.Gondnok_Fel.TabIndex = 94;
            this.ToolTip1.SetToolTip(this.Gondnok_Fel, "Feljebb viszi a sorban az adatot");
            this.Gondnok_Fel.UseVisualStyleBackColor = true;
            this.Gondnok_Fel.Click += new System.EventHandler(this.Gondnok_Fel_Click);
            // 
            // Gond_szakszolg_szöv
            // 
            this.Gond_szakszolg_szöv.Location = new System.Drawing.Point(566, 56);
            this.Gond_szakszolg_szöv.Name = "Gond_szakszolg_szöv";
            this.Gond_szakszolg_szöv.Size = new System.Drawing.Size(277, 26);
            this.Gond_szakszolg_szöv.TabIndex = 93;
            // 
            // Label48
            // 
            this.Label48.AutoSize = true;
            this.Label48.Location = new System.Drawing.Point(438, 62);
            this.Label48.Name = "Label48";
            this.Label48.Size = new System.Drawing.Size(112, 20);
            this.Label48.TabIndex = 92;
            this.Label48.Text = "Szakszolgálat:";
            // 
            // Gond_töröl
            // 
            this.Gond_töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Gond_töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Gond_töröl.Location = new System.Drawing.Point(1129, 77);
            this.Gond_töröl.Name = "Gond_töröl";
            this.Gond_töröl.Size = new System.Drawing.Size(45, 45);
            this.Gond_töröl.TabIndex = 90;
            this.ToolTip1.SetToolTip(this.Gond_töröl, "Törli az adatokat");
            this.Gond_töröl.UseVisualStyleBackColor = true;
            this.Gond_töröl.Click += new System.EventHandler(this.Gond_töröl_Click);
            // 
            // Gond_új
            // 
            this.Gond_új.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Gond_új.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Gond_új.Location = new System.Drawing.Point(1078, 77);
            this.Gond_új.Name = "Gond_új";
            this.Gond_új.Size = new System.Drawing.Size(45, 45);
            this.Gond_új.TabIndex = 84;
            this.ToolTip1.SetToolTip(this.Gond_új, "Új adatnak előkészíti a beviteli mezőt");
            this.Gond_új.UseVisualStyleBackColor = true;
            this.Gond_új.Click += new System.EventHandler(this.Gond_új_Click);
            // 
            // Gond_rögzít
            // 
            this.Gond_rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Gond_rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Gond_rögzít.Location = new System.Drawing.Point(1128, 15);
            this.Gond_rögzít.Name = "Gond_rögzít";
            this.Gond_rögzít.Size = new System.Drawing.Size(45, 45);
            this.Gond_rögzít.TabIndex = 83;
            this.ToolTip1.SetToolTip(this.Gond_rögzít, "Rögzíti/módosítja az adatokat");
            this.Gond_rögzít.UseVisualStyleBackColor = true;
            this.Gond_rögzít.Click += new System.EventHandler(this.Gond_rögzít_Click);
            // 
            // Gond_Szak
            // 
            this.Gond_Szak.AutoSize = true;
            this.Gond_Szak.Location = new System.Drawing.Point(849, 120);
            this.Gond_Szak.Name = "Gond_Szak";
            this.Gond_Szak.Size = new System.Drawing.Size(178, 24);
            this.Gond_Szak.TabIndex = 82;
            this.Gond_Szak.Text = "Szakszolgálat vezető";
            this.Gond_Szak.UseVisualStyleBackColor = true;
            // 
            // Gond_Gondnok
            // 
            this.Gond_Gondnok.AutoSize = true;
            this.Gond_Gondnok.Location = new System.Drawing.Point(849, 88);
            this.Gond_Gondnok.Name = "Gond_Gondnok";
            this.Gond_Gondnok.Size = new System.Drawing.Size(94, 24);
            this.Gond_Gondnok.TabIndex = 81;
            this.Gond_Gondnok.Text = "Gondnok";
            this.Gond_Gondnok.UseVisualStyleBackColor = true;
            // 
            // Gond_beosztás
            // 
            this.Gond_beosztás.Location = new System.Drawing.Point(139, 120);
            this.Gond_beosztás.Name = "Gond_beosztás";
            this.Gond_beosztás.Size = new System.Drawing.Size(277, 26);
            this.Gond_beosztás.TabIndex = 80;
            // 
            // Gond_telefon
            // 
            this.Gond_telefon.Location = new System.Drawing.Point(566, 120);
            this.Gond_telefon.Name = "Gond_telefon";
            this.Gond_telefon.Size = new System.Drawing.Size(277, 26);
            this.Gond_telefon.TabIndex = 79;
            // 
            // Gond_email
            // 
            this.Gond_email.Location = new System.Drawing.Point(566, 88);
            this.Gond_email.Name = "Gond_email";
            this.Gond_email.Size = new System.Drawing.Size(277, 26);
            this.Gond_email.TabIndex = 78;
            // 
            // Gond_Név
            // 
            this.Gond_Név.Location = new System.Drawing.Point(139, 88);
            this.Gond_Név.Name = "Gond_Név";
            this.Gond_Név.Size = new System.Drawing.Size(277, 26);
            this.Gond_Név.TabIndex = 77;
            // 
            // Label47
            // 
            this.Label47.AutoSize = true;
            this.Label47.Location = new System.Drawing.Point(11, 94);
            this.Label47.Name = "Label47";
            this.Label47.Size = new System.Drawing.Size(40, 20);
            this.Label47.TabIndex = 76;
            this.Label47.Text = "Név:";
            // 
            // Label46
            // 
            this.Label46.AutoSize = true;
            this.Label46.Location = new System.Drawing.Point(438, 94);
            this.Label46.Name = "Label46";
            this.Label46.Size = new System.Drawing.Size(85, 20);
            this.Label46.TabIndex = 75;
            this.Label46.Text = "E-mail cím:";
            // 
            // Label45
            // 
            this.Label45.AutoSize = true;
            this.Label45.Location = new System.Drawing.Point(438, 126);
            this.Label45.Name = "Label45";
            this.Label45.Size = new System.Drawing.Size(104, 20);
            this.Label45.TabIndex = 74;
            this.Label45.Text = "Telefonszám:";
            // 
            // Label44
            // 
            this.Label44.AutoSize = true;
            this.Label44.Location = new System.Drawing.Point(11, 126);
            this.Label44.Name = "Label44";
            this.Label44.Size = new System.Drawing.Size(80, 20);
            this.Label44.TabIndex = 73;
            this.Label44.Text = "Beosztás:";
            // 
            // Gond_telephely
            // 
            this.Gond_telephely.Location = new System.Drawing.Point(139, 56);
            this.Gond_telephely.Name = "Gond_telephely";
            this.Gond_telephely.Size = new System.Drawing.Size(277, 26);
            this.Gond_telephely.TabIndex = 72;
            // 
            // Gond_sorszám
            // 
            this.Gond_sorszám.Enabled = false;
            this.Gond_sorszám.Location = new System.Drawing.Point(139, 24);
            this.Gond_sorszám.Name = "Gond_sorszám";
            this.Gond_sorszám.Size = new System.Drawing.Size(97, 26);
            this.Gond_sorszám.TabIndex = 71;
            // 
            // Label42
            // 
            this.Label42.AutoSize = true;
            this.Label42.Location = new System.Drawing.Point(11, 62);
            this.Label42.Name = "Label42";
            this.Label42.Size = new System.Drawing.Size(80, 20);
            this.Label42.TabIndex = 70;
            this.Label42.Text = "Telephely:";
            // 
            // Label43
            // 
            this.Label43.AutoSize = true;
            this.Label43.Location = new System.Drawing.Point(11, 30);
            this.Label43.Name = "Label43";
            this.Label43.Size = new System.Drawing.Size(76, 20);
            this.Label43.TabIndex = 69;
            this.Label43.Text = "Sorszám:";
            // 
            // Gondnok_frissít
            // 
            this.Gondnok_frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Gondnok_frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Gondnok_frissít.Location = new System.Drawing.Point(1027, 77);
            this.Gondnok_frissít.Name = "Gondnok_frissít";
            this.Gondnok_frissít.Size = new System.Drawing.Size(45, 45);
            this.Gondnok_frissít.TabIndex = 68;
            this.ToolTip1.SetToolTip(this.Gondnok_frissít, "Frissíti a táblázatot");
            this.Gondnok_frissít.UseVisualStyleBackColor = true;
            this.Gondnok_frissít.Click += new System.EventHandler(this.Gondnok_frissít_Click);
            // 
            // Gondnok_tábla
            // 
            this.Gondnok_tábla.AllowUserToAddRows = false;
            this.Gondnok_tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Gondnok_tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle30;
            this.Gondnok_tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Gondnok_tábla.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle31.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle31.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle31.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle31.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle31.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Gondnok_tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle31;
            this.Gondnok_tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Gondnok_tábla.EnableHeadersVisualStyles = false;
            this.Gondnok_tábla.Location = new System.Drawing.Point(6, 152);
            this.Gondnok_tábla.Name = "Gondnok_tábla";
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle32.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle32.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle32.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle32.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle32.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Gondnok_tábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle32;
            this.Gondnok_tábla.RowHeadersVisible = false;
            this.Gondnok_tábla.RowHeadersWidth = 51;
            this.Gondnok_tábla.Size = new System.Drawing.Size(1176, 294);
            this.Gondnok_tábla.TabIndex = 67;
            this.Gondnok_tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Gondnok_tábla_CellClick);
            // 
            // tabPage13
            // 
            this.tabPage13.BackColor = System.Drawing.Color.SandyBrown;
            this.tabPage13.Controls.Add(this.Eszköz_Frissít);
            this.tabPage13.Controls.Add(this.Eszköz_Tábla);
            this.tabPage13.Controls.Add(this.Eszköz_Típus);
            this.tabPage13.Controls.Add(this.label52);
            this.tabPage13.Controls.Add(this.LábJobb);
            this.tabPage13.Controls.Add(this.LábKözép);
            this.tabPage13.Controls.Add(this.LábBal);
            this.tabPage13.Controls.Add(this.FejJobb);
            this.tabPage13.Controls.Add(this.FejKözép);
            this.tabPage13.Controls.Add(this.Szerszám_OK);
            this.tabPage13.Controls.Add(this.label51);
            this.tabPage13.Controls.Add(this.FejBal);
            this.tabPage13.Controls.Add(this.label18);
            this.tabPage13.Location = new System.Drawing.Point(4, 54);
            this.tabPage13.Name = "tabPage13";
            this.tabPage13.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage13.Size = new System.Drawing.Size(1188, 462);
            this.tabPage13.TabIndex = 12;
            this.tabPage13.Text = "Eszköz";
            // 
            // Eszköz_Frissít
            // 
            this.Eszköz_Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Eszköz_Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Eszköz_Frissít.Location = new System.Drawing.Point(243, 6);
            this.Eszköz_Frissít.Name = "Eszköz_Frissít";
            this.Eszköz_Frissít.Size = new System.Drawing.Size(45, 45);
            this.Eszköz_Frissít.TabIndex = 93;
            this.ToolTip1.SetToolTip(this.Eszköz_Frissít, "Frissíti a táblázatot");
            this.Eszköz_Frissít.UseVisualStyleBackColor = true;
            this.Eszköz_Frissít.Click += new System.EventHandler(this.Eszköz_Frissít_Click);
            // 
            // Eszköz_Tábla
            // 
            this.Eszköz_Tábla.AllowUserToAddRows = false;
            this.Eszköz_Tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Eszköz_Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle33;
            this.Eszköz_Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Eszköz_Tábla.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle34.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle34.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle34.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle34.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle34.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Eszköz_Tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle34;
            this.Eszköz_Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Eszköz_Tábla.EnableHeadersVisualStyles = false;
            this.Eszköz_Tábla.Location = new System.Drawing.Point(15, 316);
            this.Eszköz_Tábla.Name = "Eszköz_Tábla";
            dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle35.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle35.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle35.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle35.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle35.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Eszköz_Tábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle35;
            this.Eszköz_Tábla.RowHeadersVisible = false;
            this.Eszköz_Tábla.RowHeadersWidth = 51;
            this.Eszköz_Tábla.Size = new System.Drawing.Size(1164, 139);
            this.Eszköz_Tábla.TabIndex = 92;
            this.Eszköz_Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Eszköz_Tábla_CellClick);
            // 
            // Eszköz_Típus
            // 
            this.Eszköz_Típus.Location = new System.Drawing.Point(132, 25);
            this.Eszköz_Típus.Name = "Eszköz_Típus";
            this.Eszköz_Típus.Size = new System.Drawing.Size(105, 26);
            this.Eszköz_Típus.TabIndex = 91;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(23, 31);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(100, 20);
            this.label52.TabIndex = 90;
            this.label52.Text = "Nyomtatvány";
            // 
            // LábJobb
            // 
            this.LábJobb.Location = new System.Drawing.Point(805, 195);
            this.LábJobb.Multiline = true;
            this.LábJobb.Name = "LábJobb";
            this.LábJobb.Size = new System.Drawing.Size(374, 115);
            this.LábJobb.TabIndex = 89;
            // 
            // LábKözép
            // 
            this.LábKözép.Location = new System.Drawing.Point(414, 195);
            this.LábKözép.Multiline = true;
            this.LábKözép.Name = "LábKözép";
            this.LábKözép.Size = new System.Drawing.Size(374, 115);
            this.LábKözép.TabIndex = 88;
            // 
            // LábBal
            // 
            this.LábBal.Location = new System.Drawing.Point(15, 195);
            this.LábBal.Multiline = true;
            this.LábBal.Name = "LábBal";
            this.LábBal.Size = new System.Drawing.Size(374, 115);
            this.LábBal.TabIndex = 87;
            // 
            // FejJobb
            // 
            this.FejJobb.Location = new System.Drawing.Point(805, 57);
            this.FejJobb.Multiline = true;
            this.FejJobb.Name = "FejJobb";
            this.FejJobb.Size = new System.Drawing.Size(374, 112);
            this.FejJobb.TabIndex = 86;
            // 
            // FejKözép
            // 
            this.FejKözép.Location = new System.Drawing.Point(414, 57);
            this.FejKözép.Multiline = true;
            this.FejKözép.Name = "FejKözép";
            this.FejKözép.Size = new System.Drawing.Size(374, 112);
            this.FejKözép.TabIndex = 85;
            // 
            // Szerszám_OK
            // 
            this.Szerszám_OK.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Szerszám_OK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Szerszám_OK.Location = new System.Drawing.Point(1116, 6);
            this.Szerszám_OK.Name = "Szerszám_OK";
            this.Szerszám_OK.Size = new System.Drawing.Size(45, 45);
            this.Szerszám_OK.TabIndex = 84;
            this.ToolTip1.SetToolTip(this.Szerszám_OK, "Rögzíti/módosítja az adatokat");
            this.Szerszám_OK.UseVisualStyleBackColor = true;
            this.Szerszám_OK.Click += new System.EventHandler(this.Szerszám_OK_Click);
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(564, 172);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(103, 20);
            this.label51.TabIndex = 74;
            this.label51.Text = "Lábléc felirat:";
            // 
            // FejBal
            // 
            this.FejBal.Location = new System.Drawing.Point(15, 57);
            this.FejBal.Multiline = true;
            this.FejBal.Name = "FejBal";
            this.FejBal.Size = new System.Drawing.Size(374, 112);
            this.FejBal.TabIndex = 73;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(569, 31);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(98, 20);
            this.label18.TabIndex = 72;
            this.label18.Text = "Fejléc felirat:";
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.Cmbtelephely);
            this.Panel1.Controls.Add(this.Label13);
            this.Panel1.Location = new System.Drawing.Point(12, 12);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(373, 33);
            this.Panel1.TabIndex = 46;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(175, 2);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
            this.Cmbtelephely.SelectedIndexChanged += new System.EventHandler(this.Cmbtelephely_SelectedIndexChanged);
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(3, 4);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(145, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // ToolTip1
            // 
            this.ToolTip1.IsBalloon = true;
            // 
            // Button13
            // 
            this.Button13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button13.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.Button13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button13.Location = new System.Drawing.Point(1143, 12);
            this.Button13.Name = "Button13";
            this.Button13.Size = new System.Drawing.Size(45, 45);
            this.Button13.TabIndex = 52;
            this.ToolTip1.SetToolTip(this.Button13, "Súgó");
            this.Button13.UseVisualStyleBackColor = true;
            this.Button13.Click += new System.EventHandler(this.Button13_Click);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(409, 18);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(724, 17);
            this.Holtart.TabIndex = 53;
            this.Holtart.Visible = false;
            // 
            // Ablak_alap_program_személy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 586);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Button13);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Fülek);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_alap_program_személy";
            this.Text = "Program Adatok Személy";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AblakProgramadatokszemély_Load);
            this.Fülek.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CsoportTábla)).EndInit();
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            this.TabPage10.ResumeLayout(false);
            this.TabPage10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BeosztásTábla)).EndInit();
            this.TabPage3.ResumeLayout(false);
            this.TabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TáblaOktatás)).EndInit();
            this.TabPage4.ResumeLayout(false);
            this.TabPage5.ResumeLayout(false);
            this.TabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FeorTábla)).EndInit();
            this.TabPage6.ResumeLayout(false);
            this.TabPage6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla2)).EndInit();
            this.TabPage7.ResumeLayout(false);
            this.TabPage7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla1)).EndInit();
            this.TabPage8.ResumeLayout(false);
            this.TabPage8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla3)).EndInit();
            this.TabPage9.ResumeLayout(false);
            this.TabPage9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Munka_Tábla)).EndInit();
            this.TabPage11.ResumeLayout(false);
            this.TabPage11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Védő_tábla)).EndInit();
            this.TabPage12.ResumeLayout(false);
            this.TabPage12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gondnok_tábla)).EndInit();
            this.tabPage13.ResumeLayout(false);
            this.tabPage13.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Eszköz_Tábla)).EndInit();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        internal TabControl Fülek;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal Panel Panel1;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal Button Button13;
        internal TabPage TabPage3;
        internal DateTimePicker OktDátum;
        internal Label Label3;
        internal Label Label2;
        internal ComboBox CMBStátus;
        internal Label Label1;
        internal ComboBox CmbGyakoriság;
        internal Button BtnOktatásOK;
        internal Zuby.ADGV.AdvancedDataGridView TáblaOktatás;
        internal ComboBox CmbKategória;
        internal TextBox Téma;
        internal TextBox TxtSorrend;
        internal Label Label57;
        internal Label Label58;
        internal Label Label59;
        internal TextBox IDoktatás;
        internal Label Label60;
        internal Button BtnOktatásFel;
        internal Button BtnOktatásÚj;
        internal Label Label4;
        internal TextBox Ismétlődés;
        internal TextBox TxtOktatásRow;
        internal TextBox IDoktatáselőző;
        internal TextBox TxtOktatássorszám;
        internal Button Button12;
        internal ToolTip ToolTip1;
        internal TextBox TxtPDFfájl;
        internal Label Label5;
        internal TabPage TabPage4;
        internal Button Button5;
        internal TextBox TxtPDFfájlteljes;
        internal TabPage TabPage5;
        internal TabPage TabPage6;
        internal TabPage TabPage7;
        internal TextBox FeorFeormegnevezés;
        internal TextBox FeorFeorszám;
        internal TextBox Feorsorszám;
        internal Label Label8;
        internal Label Label7;
        internal Label Label6;
        internal Zuby.ADGV.AdvancedDataGridView FeorTábla;
        internal Button Command1;
        internal Button Feljebb;
        internal Button Command4;
        internal Zuby.ADGV.AdvancedDataGridView Tábla2;
        internal TextBox Text4;
        internal TextBox Text2;
        internal Label Label9;
        internal Label Label10;
        internal Button Command6;
        internal Button Command5;
        internal Label Label14;
        internal TextBox Vonalszám;
        internal Zuby.ADGV.AdvancedDataGridView Tábla1;
        internal TextBox Megnevezés;
        internal TextBox Text1;
        internal Label Label11;
        internal Label Label12;
        internal Button Command2;
        internal Button Command3;
        internal TabPage TabPage8;
        internal TextBox Sorrend1;
        internal TextBox Sorrend2;
        internal Label Label15;
        internal TextBox Csoport1;
        internal DataGridView Tábla3;
        internal TextBox Csoport2;
        internal TextBox Könyvtár;
        internal Label Label16;
        internal Label Label17;
        internal Button Command7;
        internal Button Command9;
        internal TabPage TabPage9;
        internal CheckBox Vezér2;
        internal CheckBox Vezér1;
        internal Zuby.ADGV.AdvancedDataGridView Munka_Tábla;
        internal TextBox Munka_Megnevezés;
        internal Label Label19;
        internal Label Label20;
        internal Button Munka_Új;
        internal Button Munka_Rögzít;
        internal Button FrissítMunkakör;
        internal Button Button2;
        internal Button Button3;
        internal Button Button4;
        internal Button Munka_Frissít;
        internal Button Feortörlés;
        internal TextBox TextBox1;
        internal Label Label21;
        internal Button CsoportFel;
        internal Button CsoportTörlés;
        internal Button CsoportOK;
        internal TextBox CsoportNév;
        internal Label Label22;
        internal TextBox JelenlétiText4;
        internal TextBox JelenlétiText3;
        internal TextBox JelenlétiText2;
        internal TextBox JelenlétiText1;
        internal Button JelenlétiÜzem;
        internal Button JelenlétiFőmér;
        internal Button JelenlétiIgaz;
        internal Button JelenlétiSzerv;
        internal Label Label26;
        internal Label Label25;
        internal Label Label24;
        internal Label Label23;
        internal DataGridView CsoportTábla;
        internal TextBox CsoportTípus;
        internal Label Label27;
        internal TabPage TabPage10;
        internal DateTimePicker BeoIdővége;
        internal DateTimePicker BeoIdőKezdete;
        internal Button BeoFrissít;
        internal Button BeoExcel;
        internal Button BeoÚj;
        internal Button BeoTöröl;
        internal Button BeoOk;
        internal Zuby.ADGV.AdvancedDataGridView BeosztásTábla;
        internal CheckBox BeoSzámoló;
        internal CheckBox BeoÉjszakás;
        internal TextBox BeoKód;
        internal TextBox BeoMunkaidő;
        internal TextBox BEOMunkarend;
        internal TextBox BEOMagyarázat;
        internal TextBox BeoSorszám;
        internal Label Label34;
        internal Label Label33;
        internal Label Label32;
        internal Label Label31;
        internal Label Label30;
        internal Label Label29;
        internal Label Label28;
        internal TextBox txtbeosztás2;
        internal TextBox txtnév3;
        internal TextBox txtbeosztás3;
        internal Label Label35;
        internal Label Label36;
        internal Label Label37;
        internal TextBox txtnév2;
        internal Label Label38;
        internal Button Btnfőkönyv;
        internal Label Label39;
        internal TabPage TabPage11;
        internal Button Védő_frissít;
        internal DataGridView Védő_tábla;
        internal TextBox Védő_Megnevezés;
        internal TextBox Védő_id;
        internal Label Label40;
        internal Label Label41;
        internal Button Védő_új;
        internal Button Védő_rögzít;
        internal TabPage TabPage12;
        internal Button Gondnok_frissít;
        internal DataGridView Gondnok_tábla;
        internal CheckBox Gond_Szak;
        internal CheckBox Gond_Gondnok;
        internal TextBox Gond_beosztás;
        internal TextBox Gond_telefon;
        internal TextBox Gond_email;
        internal TextBox Gond_Név;
        internal Label Label47;
        internal Label Label46;
        internal Label Label45;
        internal Label Label44;
        internal TextBox Gond_telephely;
        internal TextBox Gond_sorszám;
        internal Label Label42;
        internal Label Label43;
        internal Button Gond_új;
        internal Button Gond_rögzít;
        internal Button Gond_töröl;
        internal TextBox Gond_szakszolg_szöv;
        internal Label Label48;
        private PdfiumViewer.PdfViewer PDF_néző;
        internal TextBox JelenlétiText5;
        internal Label label49;
        internal Button Eszközhöz;
        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal Button Gondnok_Fel;
        private CheckBox Munka_Státus;
        internal Label label50;
        internal TextBox Munka_Id;
        private ComboBox Munka_Kategória;
        private CheckBox FeorStátus;
        private TabPage tabPage13;
        internal Button Szerszám_OK;
        internal Label label51;
        internal TextBox FejBal;
        internal Label label18;
        internal TextBox LábJobb;
        internal TextBox LábKözép;
        internal TextBox LábBal;
        internal TextBox FejJobb;
        internal TextBox FejKözép;
        internal TextBox Eszköz_Típus;
        internal Label label52;
        internal Button Eszköz_Frissít;
        internal DataGridView Eszköz_Tábla;
    }
}