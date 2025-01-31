using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
 
    public partial class Ablak_sérülés : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_sérülés));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Lapfülek = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.Label68 = new System.Windows.Forms.Label();
            this.Telephely1 = new System.Windows.Forms.TextBox();
            this.Költséghely = new System.Windows.Forms.TextBox();
            this.Leírás1 = new System.Windows.Forms.TextBox();
            this.Esemény = new System.Windows.Forms.TextBox();
            this.Leírás = new System.Windows.Forms.TextBox();
            this.Ütközött = new System.Windows.Forms.TextBox();
            this.Label61 = new System.Windows.Forms.Label();
            this.Label60 = new System.Windows.Forms.Label();
            this.Label59 = new System.Windows.Forms.Label();
            this.Label58 = new System.Windows.Forms.Label();
            this.Label57 = new System.Windows.Forms.Label();
            this.Biztosító = new System.Windows.Forms.TextBox();
            this.Helyszín = new System.Windows.Forms.TextBox();
            this.Label56 = new System.Windows.Forms.Label();
            this.Panel8 = new System.Windows.Forms.Panel();
            this.Hosszú = new System.Windows.Forms.RadioButton();
            this.Gyors = new System.Windows.Forms.RadioButton();
            this.Label55 = new System.Windows.Forms.Label();
            this.Rendelésszám = new System.Windows.Forms.TextBox();
            this.AnyagikárÁr = new System.Windows.Forms.TextBox();
            this.Label54 = new System.Windows.Forms.Label();
            this.Label53 = new System.Windows.Forms.Label();
            this.Személyi = new System.Windows.Forms.CheckBox();
            this.Anyagikár = new System.Windows.Forms.CheckBox();
            this.Műszakihiba = new System.Windows.Forms.CheckBox();
            this.Panel7 = new System.Windows.Forms.Panel();
            this.Személyhiba = new System.Windows.Forms.RadioButton();
            this.Egyébhiba = new System.Windows.Forms.RadioButton();
            this.Idegenhiba = new System.Windows.Forms.RadioButton();
            this.Sajáthiba = new System.Windows.Forms.RadioButton();
            this.Label52 = new System.Windows.Forms.Label();
            this.Doksik = new System.Windows.Forms.TextBox();
            this.Label51 = new System.Windows.Forms.Label();
            this.Fényképek = new System.Windows.Forms.TextBox();
            this.Label50 = new System.Windows.Forms.Label();
            this.Panel6 = new System.Windows.Forms.Panel();
            this.Opt_Törölt = new System.Windows.Forms.RadioButton();
            this.Opt_Elkészült = new System.Windows.Forms.RadioButton();
            this.Opt_Nyitott = new System.Windows.Forms.RadioButton();
            this.Label49 = new System.Windows.Forms.Label();
            this.Btn_Kép_Hozzáad = new System.Windows.Forms.Button();
            this.Btn_PDF_Hozzáad = new System.Windows.Forms.Button();
            this.Visszaállít = new System.Windows.Forms.Button();
            this.Újat = new System.Windows.Forms.Button();
            this.FékvizsgálatiExcel = new System.Windows.Forms.Button();
            this.CAFExcel = new System.Windows.Forms.Button();
            this.Rögzítjelentés = new System.Windows.Forms.Button();
            this.Viszonylat = new System.Windows.Forms.TextBox();
            this.Üzembehelyezés = new System.Windows.Forms.TextBox();
            this.Szerelvény = new System.Windows.Forms.TextBox();
            this.KmóraÁllás = new System.Windows.Forms.TextBox();
            this.Label48 = new System.Windows.Forms.Label();
            this.Label47 = new System.Windows.Forms.Label();
            this.Label46 = new System.Windows.Forms.Label();
            this.Label45 = new System.Windows.Forms.Label();
            this.Forgalmiakadály = new System.Windows.Forms.TextBox();
            this.Járművezető = new System.Windows.Forms.TextBox();
            this.Label44 = new System.Windows.Forms.Label();
            this.Label43 = new System.Windows.Forms.Label();
            this.Label42 = new System.Windows.Forms.Label();
            this.Label41 = new System.Windows.Forms.Label();
            this.Telephely = new System.Windows.Forms.TextBox();
            this.Típus = new System.Windows.Forms.TextBox();
            this.Pályaszám = new System.Windows.Forms.TextBox();
            this.Sorszám = new System.Windows.Forms.TextBox();
            this.Label40 = new System.Windows.Forms.Label();
            this.Label39 = new System.Windows.Forms.Label();
            this.Idő = new System.Windows.Forms.DateTimePicker();
            this.Dátum = new System.Windows.Forms.DateTimePicker();
            this.Label37 = new System.Windows.Forms.Label();
            this.Label38 = new System.Windows.Forms.Label();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.LekTelephely = new System.Windows.Forms.ComboBox();
            this.Lekrendszám = new System.Windows.Forms.TextBox();
            this.LekDátumig = new System.Windows.Forms.DateTimePicker();
            this.LekDátumtól = new System.Windows.Forms.DateTimePicker();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.LekKész = new System.Windows.Forms.RadioButton();
            this.LekTörölt = new System.Windows.Forms.RadioButton();
            this.LekNyitott = new System.Windows.Forms.RadioButton();
            this.Label5 = new System.Windows.Forms.Label();
            this.LekMind = new System.Windows.Forms.RadioButton();
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.LekExcel = new System.Windows.Forms.Button();
            this.LekLekérdezés = new System.Windows.Forms.Button();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.ChckBxDigitális = new System.Windows.Forms.CheckBox();
            this.Elkészült = new System.Windows.Forms.Button();
            this.Chck_Egyszerüsített = new System.Windows.Forms.CheckBox();
            this.KöltTelephely = new System.Windows.Forms.ComboBox();
            this.KöltRendszám = new System.Windows.Forms.TextBox();
            this.KöltDátumig = new System.Windows.Forms.DateTimePicker();
            this.KöltDátumtól = new System.Windows.Forms.DateTimePicker();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.KöltKész = new System.Windows.Forms.RadioButton();
            this.KöltNyitott = new System.Windows.Forms.RadioButton();
            this.Label10 = new System.Windows.Forms.Label();
            this.KöltMind = new System.Windows.Forms.RadioButton();
            this.Tábla2 = new System.Windows.Forms.DataGridView();
            this.NyomtatványKitöltés = new System.Windows.Forms.Button();
            this.ExcelNullás = new System.Windows.Forms.Button();
            this.CsoportkijelölMind = new System.Windows.Forms.Button();
            this.CsoportVissza = new System.Windows.Forms.Button();
            this.Nullás = new System.Windows.Forms.Button();
            this.ExcelKöltség = new System.Windows.Forms.Button();
            this.KöltLekérdezés = new System.Windows.Forms.Button();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.SapTelephely = new System.Windows.Forms.TextBox();
            this.Label67 = new System.Windows.Forms.Label();
            this.SapRendelés = new System.Windows.Forms.TextBox();
            this.Label63 = new System.Windows.Forms.Label();
            this.SapDátum = new System.Windows.Forms.DateTimePicker();
            this.Label64 = new System.Windows.Forms.Label();
            this.SapSorszám = new System.Windows.Forms.TextBox();
            this.Label65 = new System.Windows.Forms.Label();
            this.SapPályaszám = new System.Windows.Forms.TextBox();
            this.Label66 = new System.Windows.Forms.Label();
            this.Btn_SAP_Feltöltés_Excelből = new System.Windows.Forms.Button();
            this.Btn_SAP_Betöltés_Excelbe = new System.Windows.Forms.Button();
            this.RendelésAdatokSzolgáltatás = new System.Windows.Forms.Button();
            this.RendelésAdatokAnyag = new System.Windows.Forms.Button();
            this.RendelésAdatokIdő = new System.Windows.Forms.Button();
            this.SAPBeolvasó = new System.Windows.Forms.Button();
            this.Tábla1 = new System.Windows.Forms.DataGridView();
            this.TabPage5 = new System.Windows.Forms.TabPage();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label72 = new System.Windows.Forms.Label();
            this.TxtBxDigitalisAlairo1 = new System.Windows.Forms.TextBox();
            this.TxtBxBeosztas2 = new System.Windows.Forms.TextBox();
            this.label73 = new System.Windows.Forms.Label();
            this.Btn_Digitális_Aláírók = new System.Windows.Forms.Button();
            this.label69 = new System.Windows.Forms.Label();
            this.TxtBxBeosztas1 = new System.Windows.Forms.TextBox();
            this.label70 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.TxtBxDigitalisAlairo2 = new System.Windows.Forms.TextBox();
            this.Panel5 = new System.Windows.Forms.Panel();
            this.Label62 = new System.Windows.Forms.Label();
            this.Dátum_tarifa = new System.Windows.Forms.DateTimePicker();
            this.ÉvestarifaD03 = new System.Windows.Forms.TextBox();
            this.Btn_ÁllandóÉrt_Tarifa_Rögzít = new System.Windows.Forms.Button();
            this.Label36 = new System.Windows.Forms.Label();
            this.Label35 = new System.Windows.Forms.Label();
            this.Label34 = new System.Windows.Forms.Label();
            this.ÉvestarifaD60 = new System.Windows.Forms.TextBox();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.Text7 = new System.Windows.Forms.TextBox();
            this.Text6 = new System.Windows.Forms.TextBox();
            this.Text5 = new System.Windows.Forms.TextBox();
            this.Text4 = new System.Windows.Forms.TextBox();
            this.Text3 = new System.Windows.Forms.TextBox();
            this.Text2 = new System.Windows.Forms.TextBox();
            this.Text1 = new System.Windows.Forms.TextBox();
            this.Eszköz = new System.Windows.Forms.TextBox();
            this.Telefonszám = new System.Windows.Forms.TextBox();
            this.Kiállította = new System.Windows.Forms.TextBox();
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
            this.Btn_ÁllandóÉrt_Felépít_Rögzít = new System.Windows.Forms.Button();
            this.Label23 = new System.Windows.Forms.Label();
            this.Iktatószám = new System.Windows.Forms.TextBox();
            this.TabPage6 = new System.Windows.Forms.TabPage();
            this.KépTörlés = new System.Windows.Forms.Button();
            this.KépLementés = new System.Windows.Forms.Button();
            this.KépKeret = new System.Windows.Forms.PictureBox();
            this.FényIdő = new System.Windows.Forms.DateTimePicker();
            this.FényDátum = new System.Windows.Forms.DateTimePicker();
            this.Label15 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.FénySorszám = new System.Windows.Forms.TextBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.FényPályaszám = new System.Windows.Forms.TextBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.FileBox = new System.Windows.Forms.ListBox();
            this.TabPage7 = new System.Windows.Forms.TabPage();
            this.PDF_néző = new PdfiumViewer.PdfViewer();
            this.PdfIdő = new System.Windows.Forms.DateTimePicker();
            this.PdfDátum = new System.Windows.Forms.DateTimePicker();
            this.Label16 = new System.Windows.Forms.Label();
            this.Label17 = new System.Windows.Forms.Label();
            this.PdfSorszám = new System.Windows.Forms.TextBox();
            this.Label18 = new System.Windows.Forms.Label();
            this.PdfPályaszám = new System.Windows.Forms.TextBox();
            this.Label19 = new System.Windows.Forms.Label();
            this.PdfTörlés = new System.Windows.Forms.Button();
            this.FilePDF = new System.Windows.Forms.ListBox();
            this.TabPage8 = new System.Windows.Forms.TabPage();
            this.CafTábla = new System.Windows.Forms.DataGridView();
            this.Névtext = new System.Windows.Forms.TextBox();
            this.BeosztásText = new System.Windows.Forms.TextBox();
            this.Cégtext = new System.Windows.Forms.TextBox();
            this.Label22 = new System.Windows.Forms.Label();
            this.Label21 = new System.Windows.Forms.Label();
            this.Label20 = new System.Windows.Forms.Label();
            this.CafTöröl = new System.Windows.Forms.Button();
            this.Btn_CAF_Új = new System.Windows.Forms.Button();
            this.CAFRögzít = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Btn_Súgó = new System.Windows.Forms.Button();
            this.Holtart = new V_MindenEgyéb.MyProgressbar();
            this.Panel1.SuspendLayout();
            this.Lapfülek.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.Panel8.SuspendLayout();
            this.Panel7.SuspendLayout();
            this.Panel6.SuspendLayout();
            this.TabPage2.SuspendLayout();
            this.Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.TabPage3.SuspendLayout();
            this.Panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla2)).BeginInit();
            this.TabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla1)).BeginInit();
            this.TabPage5.SuspendLayout();
            this.panel9.SuspendLayout();
            this.Panel5.SuspendLayout();
            this.Panel4.SuspendLayout();
            this.TabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KépKeret)).BeginInit();
            this.TabPage7.SuspendLayout();
            this.TabPage8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CafTábla)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.Cmbtelephely);
            this.Panel1.Controls.Add(this.Label13);
            this.Panel1.Location = new System.Drawing.Point(0, 12);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(373, 33);
            this.Panel1.TabIndex = 64;
            // 
            // Cmbtelephely
            // 
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
            this.Label13.Location = new System.Drawing.Point(12, 5);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(145, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
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
            this.Lapfülek.Controls.Add(this.TabPage8);
            this.Lapfülek.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Lapfülek.Location = new System.Drawing.Point(5, 51);
            this.Lapfülek.Name = "Lapfülek";
            this.Lapfülek.Padding = new System.Drawing.Point(18, 3);
            this.Lapfülek.SelectedIndex = 0;
            this.Lapfülek.Size = new System.Drawing.Size(1248, 650);
            this.Lapfülek.TabIndex = 67;
            this.Lapfülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Lapfülek_DrawItem);
            this.Lapfülek.SelectedIndexChanged += new System.EventHandler(this.LAPFülek_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.AutoScroll = true;
            this.TabPage1.BackColor = System.Drawing.Color.LightSeaGreen;
            this.TabPage1.Controls.Add(this.Label68);
            this.TabPage1.Controls.Add(this.Telephely1);
            this.TabPage1.Controls.Add(this.Költséghely);
            this.TabPage1.Controls.Add(this.Leírás1);
            this.TabPage1.Controls.Add(this.Esemény);
            this.TabPage1.Controls.Add(this.Leírás);
            this.TabPage1.Controls.Add(this.Ütközött);
            this.TabPage1.Controls.Add(this.Label61);
            this.TabPage1.Controls.Add(this.Label60);
            this.TabPage1.Controls.Add(this.Label59);
            this.TabPage1.Controls.Add(this.Label58);
            this.TabPage1.Controls.Add(this.Label57);
            this.TabPage1.Controls.Add(this.Biztosító);
            this.TabPage1.Controls.Add(this.Helyszín);
            this.TabPage1.Controls.Add(this.Label56);
            this.TabPage1.Controls.Add(this.Panel8);
            this.TabPage1.Controls.Add(this.Rendelésszám);
            this.TabPage1.Controls.Add(this.AnyagikárÁr);
            this.TabPage1.Controls.Add(this.Label54);
            this.TabPage1.Controls.Add(this.Label53);
            this.TabPage1.Controls.Add(this.Személyi);
            this.TabPage1.Controls.Add(this.Anyagikár);
            this.TabPage1.Controls.Add(this.Műszakihiba);
            this.TabPage1.Controls.Add(this.Panel7);
            this.TabPage1.Controls.Add(this.Doksik);
            this.TabPage1.Controls.Add(this.Label51);
            this.TabPage1.Controls.Add(this.Fényképek);
            this.TabPage1.Controls.Add(this.Label50);
            this.TabPage1.Controls.Add(this.Panel6);
            this.TabPage1.Controls.Add(this.Btn_Kép_Hozzáad);
            this.TabPage1.Controls.Add(this.Btn_PDF_Hozzáad);
            this.TabPage1.Controls.Add(this.Visszaállít);
            this.TabPage1.Controls.Add(this.Újat);
            this.TabPage1.Controls.Add(this.FékvizsgálatiExcel);
            this.TabPage1.Controls.Add(this.CAFExcel);
            this.TabPage1.Controls.Add(this.Rögzítjelentés);
            this.TabPage1.Controls.Add(this.Viszonylat);
            this.TabPage1.Controls.Add(this.Üzembehelyezés);
            this.TabPage1.Controls.Add(this.Szerelvény);
            this.TabPage1.Controls.Add(this.KmóraÁllás);
            this.TabPage1.Controls.Add(this.Label48);
            this.TabPage1.Controls.Add(this.Label47);
            this.TabPage1.Controls.Add(this.Label46);
            this.TabPage1.Controls.Add(this.Label45);
            this.TabPage1.Controls.Add(this.Forgalmiakadály);
            this.TabPage1.Controls.Add(this.Járművezető);
            this.TabPage1.Controls.Add(this.Label44);
            this.TabPage1.Controls.Add(this.Label43);
            this.TabPage1.Controls.Add(this.Label42);
            this.TabPage1.Controls.Add(this.Label41);
            this.TabPage1.Controls.Add(this.Telephely);
            this.TabPage1.Controls.Add(this.Típus);
            this.TabPage1.Controls.Add(this.Pályaszám);
            this.TabPage1.Controls.Add(this.Sorszám);
            this.TabPage1.Controls.Add(this.Label40);
            this.TabPage1.Controls.Add(this.Label39);
            this.TabPage1.Controls.Add(this.Idő);
            this.TabPage1.Controls.Add(this.Dátum);
            this.TabPage1.Controls.Add(this.Label37);
            this.TabPage1.Controls.Add(this.Label38);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1240, 617);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Jelentés létrehozás / módosítás";
            // 
            // Label68
            // 
            this.Label68.AutoSize = true;
            this.Label68.BackColor = System.Drawing.Color.SeaGreen;
            this.Label68.Location = new System.Drawing.Point(559, 110);
            this.Label68.Name = "Label68";
            this.Label68.Size = new System.Drawing.Size(94, 20);
            this.Label68.TabIndex = 271;
            this.Label68.Text = "Költséghely:";
            // 
            // Telephely1
            // 
            this.Telephely1.Location = new System.Drawing.Point(849, 107);
            this.Telephely1.Name = "Telephely1";
            this.Telephely1.Size = new System.Drawing.Size(126, 26);
            this.Telephely1.TabIndex = 269;
            // 
            // Költséghely
            // 
            this.Költséghely.Location = new System.Drawing.Point(717, 107);
            this.Költséghely.Name = "Költséghely";
            this.Költséghely.Size = new System.Drawing.Size(126, 26);
            this.Költséghely.TabIndex = 268;
            // 
            // Leírás1
            // 
            this.Leírás1.Location = new System.Drawing.Point(619, 365);
            this.Leírás1.Multiline = true;
            this.Leírás1.Name = "Leírás1";
            this.Leírás1.Size = new System.Drawing.Size(600, 182);
            this.Leírás1.TabIndex = 12;
            // 
            // Esemény
            // 
            this.Esemény.Location = new System.Drawing.Point(619, 265);
            this.Esemény.MaxLength = 150;
            this.Esemény.Multiline = true;
            this.Esemény.Name = "Esemény";
            this.Esemény.Size = new System.Drawing.Size(600, 68);
            this.Esemény.TabIndex = 11;
            // 
            // Leírás
            // 
            this.Leírás.Location = new System.Drawing.Point(7, 459);
            this.Leírás.Multiline = true;
            this.Leírás.Name = "Leírás";
            this.Leírás.Size = new System.Drawing.Size(600, 88);
            this.Leírás.TabIndex = 10;
            // 
            // Ütközött
            // 
            this.Ütközött.Location = new System.Drawing.Point(7, 365);
            this.Ütközött.MaxLength = 150;
            this.Ütközött.Multiline = true;
            this.Ütközött.Name = "Ütközött";
            this.Ütközött.Size = new System.Drawing.Size(600, 68);
            this.Ütközött.TabIndex = 9;
            // 
            // Label61
            // 
            this.Label61.AutoSize = true;
            this.Label61.BackColor = System.Drawing.Color.SeaGreen;
            this.Label61.Location = new System.Drawing.Point(6, 242);
            this.Label61.Name = "Label61";
            this.Label61.Size = new System.Drawing.Size(127, 20);
            this.Label61.TabIndex = 263;
            this.Label61.Text = "Baleset helyszín:";
            // 
            // Label60
            // 
            this.Label60.AutoSize = true;
            this.Label60.BackColor = System.Drawing.Color.SeaGreen;
            this.Label60.Location = new System.Drawing.Point(7, 336);
            this.Label60.Name = "Label60";
            this.Label60.Size = new System.Drawing.Size(110, 20);
            this.Label60.TabIndex = 262;
            this.Label60.Text = "Mivel ütközött:";
            // 
            // Label59
            // 
            this.Label59.AutoSize = true;
            this.Label59.BackColor = System.Drawing.Color.SeaGreen;
            this.Label59.Location = new System.Drawing.Point(6, 436);
            this.Label59.Name = "Label59";
            this.Label59.Size = new System.Drawing.Size(197, 20);
            this.Label59.TabIndex = 261;
            this.Label59.Text = "Jármű sérülésének leírása:";
            // 
            // Label58
            // 
            this.Label58.AutoSize = true;
            this.Label58.BackColor = System.Drawing.Color.SeaGreen;
            this.Label58.Location = new System.Drawing.Point(615, 242);
            this.Label58.Name = "Label58";
            this.Label58.Size = new System.Drawing.Size(126, 20);
            this.Label58.TabIndex = 260;
            this.Label58.Text = "Egyéb esemény:";
            // 
            // Label57
            // 
            this.Label57.AutoSize = true;
            this.Label57.BackColor = System.Drawing.Color.SeaGreen;
            this.Label57.Location = new System.Drawing.Point(615, 336);
            this.Label57.Name = "Label57";
            this.Label57.Size = new System.Drawing.Size(166, 20);
            this.Label57.TabIndex = 259;
            this.Label57.Text = "Esemény rövid leírása:";
            // 
            // Biztosító
            // 
            this.Biztosító.Location = new System.Drawing.Point(841, 581);
            this.Biztosító.MaxLength = 20;
            this.Biztosító.Name = "Biztosító";
            this.Biztosító.Size = new System.Drawing.Size(198, 26);
            this.Biztosító.TabIndex = 17;
            // 
            // Helyszín
            // 
            this.Helyszín.Location = new System.Drawing.Point(7, 265);
            this.Helyszín.MaxLength = 150;
            this.Helyszín.Multiline = true;
            this.Helyszín.Name = "Helyszín";
            this.Helyszín.Size = new System.Drawing.Size(600, 68);
            this.Helyszín.TabIndex = 8;
            // 
            // Label56
            // 
            this.Label56.AutoSize = true;
            this.Label56.BackColor = System.Drawing.Color.SeaGreen;
            this.Label56.Location = new System.Drawing.Point(839, 555);
            this.Label56.Name = "Label56";
            this.Label56.Size = new System.Drawing.Size(119, 20);
            this.Label56.TabIndex = 252;
            this.Label56.Text = "Biztosítói szám:";
            // 
            // Panel8
            // 
            this.Panel8.BackColor = System.Drawing.Color.SeaGreen;
            this.Panel8.Controls.Add(this.Hosszú);
            this.Panel8.Controls.Add(this.Gyors);
            this.Panel8.Controls.Add(this.Label55);
            this.Panel8.Location = new System.Drawing.Point(1045, 554);
            this.Panel8.Name = "Panel8";
            this.Panel8.Size = new System.Drawing.Size(173, 55);
            this.Panel8.TabIndex = 251;
            // 
            // Hosszú
            // 
            this.Hosszú.AutoSize = true;
            this.Hosszú.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Hosszú.Checked = true;
            this.Hosszú.Location = new System.Drawing.Point(84, 24);
            this.Hosszú.Name = "Hosszú";
            this.Hosszú.Size = new System.Drawing.Size(80, 24);
            this.Hosszú.TabIndex = 216;
            this.Hosszú.TabStop = true;
            this.Hosszú.Text = "24 órás";
            this.Hosszú.UseVisualStyleBackColor = false;
            // 
            // Gyors
            // 
            this.Gyors.AutoSize = true;
            this.Gyors.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Gyors.Location = new System.Drawing.Point(7, 24);
            this.Gyors.Name = "Gyors";
            this.Gyors.Size = new System.Drawing.Size(71, 24);
            this.Gyors.TabIndex = 0;
            this.Gyors.Text = "4 órás";
            this.Gyors.UseVisualStyleBackColor = false;
            // 
            // Label55
            // 
            this.Label55.AutoSize = true;
            this.Label55.BackColor = System.Drawing.Color.SeaGreen;
            this.Label55.Location = new System.Drawing.Point(3, 1);
            this.Label55.Name = "Label55";
            this.Label55.Size = new System.Drawing.Size(102, 20);
            this.Label55.TabIndex = 214;
            this.Label55.Text = "Biztosítói idő:";
            // 
            // Rendelésszám
            // 
            this.Rendelésszám.Location = new System.Drawing.Point(357, 583);
            this.Rendelésszám.Name = "Rendelésszám";
            this.Rendelésszám.Size = new System.Drawing.Size(140, 26);
            this.Rendelésszám.TabIndex = 16;
            // 
            // AnyagikárÁr
            // 
            this.AnyagikárÁr.Location = new System.Drawing.Point(357, 551);
            this.AnyagikárÁr.Name = "AnyagikárÁr";
            this.AnyagikárÁr.Size = new System.Drawing.Size(140, 26);
            this.AnyagikárÁr.TabIndex = 15;
            // 
            // Label54
            // 
            this.Label54.AutoSize = true;
            this.Label54.BackColor = System.Drawing.Color.SeaGreen;
            this.Label54.Location = new System.Drawing.Point(211, 586);
            this.Label54.Name = "Label54";
            this.Label54.Size = new System.Drawing.Size(126, 20);
            this.Label54.TabIndex = 248;
            this.Label54.Text = "Rendelési szám:";
            // 
            // Label53
            // 
            this.Label53.AutoSize = true;
            this.Label53.BackColor = System.Drawing.Color.SeaGreen;
            this.Label53.Location = new System.Drawing.Point(211, 554);
            this.Label53.Name = "Label53";
            this.Label53.Size = new System.Drawing.Size(142, 20);
            this.Label53.TabIndex = 247;
            this.Label53.Text = "Becsült anyagi kár:";
            // 
            // Személyi
            // 
            this.Személyi.AutoSize = true;
            this.Személyi.BackColor = System.Drawing.Color.SeaGreen;
            this.Személyi.Location = new System.Drawing.Point(7, 553);
            this.Személyi.Name = "Személyi";
            this.Személyi.Size = new System.Drawing.Size(174, 24);
            this.Személyi.TabIndex = 13;
            this.Személyi.Text = "Személyi sérülés volt";
            this.Személyi.UseVisualStyleBackColor = false;
            // 
            // Anyagikár
            // 
            this.Anyagikár.AutoSize = true;
            this.Anyagikár.BackColor = System.Drawing.Color.SeaGreen;
            this.Anyagikár.Location = new System.Drawing.Point(7, 585);
            this.Anyagikár.Name = "Anyagikár";
            this.Anyagikár.Size = new System.Drawing.Size(184, 24);
            this.Anyagikár.TabIndex = 14;
            this.Anyagikár.Text = "Anyagi kár keletkezett";
            this.Anyagikár.UseVisualStyleBackColor = false;
            // 
            // Műszakihiba
            // 
            this.Műszakihiba.AutoSize = true;
            this.Műszakihiba.BackColor = System.Drawing.Color.SeaGreen;
            this.Műszakihiba.Location = new System.Drawing.Point(506, 206);
            this.Műszakihiba.Name = "Műszakihiba";
            this.Műszakihiba.Size = new System.Drawing.Size(215, 24);
            this.Műszakihiba.TabIndex = 219;
            this.Műszakihiba.Text = "Műszaki hibára hivatkozott";
            this.Műszakihiba.UseVisualStyleBackColor = false;
            // 
            // Panel7
            // 
            this.Panel7.BackColor = System.Drawing.Color.SeaGreen;
            this.Panel7.Controls.Add(this.Személyhiba);
            this.Panel7.Controls.Add(this.Egyébhiba);
            this.Panel7.Controls.Add(this.Idegenhiba);
            this.Panel7.Controls.Add(this.Sajáthiba);
            this.Panel7.Controls.Add(this.Label52);
            this.Panel7.Location = new System.Drawing.Point(3, 173);
            this.Panel7.Name = "Panel7";
            this.Panel7.Size = new System.Drawing.Size(494, 57);
            this.Panel7.TabIndex = 241;
            // 
            // Személyhiba
            // 
            this.Személyhiba.AutoSize = true;
            this.Személyhiba.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Személyhiba.Location = new System.Drawing.Point(248, 24);
            this.Személyhiba.Name = "Személyhiba";
            this.Személyhiba.Size = new System.Drawing.Size(157, 24);
            this.Személyhiba.TabIndex = 218;
            this.Személyhiba.Text = "Csak személyi sér.";
            this.Személyhiba.UseVisualStyleBackColor = false;
            // 
            // Egyébhiba
            // 
            this.Egyébhiba.AutoSize = true;
            this.Egyébhiba.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Egyébhiba.Location = new System.Drawing.Point(411, 24);
            this.Egyébhiba.Name = "Egyébhiba";
            this.Egyébhiba.Size = new System.Drawing.Size(72, 24);
            this.Egyébhiba.TabIndex = 217;
            this.Egyébhiba.Text = "Egyéb";
            this.Egyébhiba.UseVisualStyleBackColor = false;
            // 
            // Idegenhiba
            // 
            this.Idegenhiba.AutoSize = true;
            this.Idegenhiba.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Idegenhiba.Checked = true;
            this.Idegenhiba.Location = new System.Drawing.Point(121, 24);
            this.Idegenhiba.Name = "Idegenhiba";
            this.Idegenhiba.Size = new System.Drawing.Size(120, 24);
            this.Idegenhiba.TabIndex = 216;
            this.Idegenhiba.TabStop = true;
            this.Idegenhiba.Text = "Idegen jármű";
            this.Idegenhiba.UseVisualStyleBackColor = false;
            // 
            // Sajáthiba
            // 
            this.Sajáthiba.AutoSize = true;
            this.Sajáthiba.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Sajáthiba.Location = new System.Drawing.Point(7, 24);
            this.Sajáthiba.Name = "Sajáthiba";
            this.Sajáthiba.Size = new System.Drawing.Size(107, 24);
            this.Sajáthiba.TabIndex = 0;
            this.Sajáthiba.Text = "Saját jármű";
            this.Sajáthiba.UseVisualStyleBackColor = false;
            // 
            // Label52
            // 
            this.Label52.AutoSize = true;
            this.Label52.BackColor = System.Drawing.Color.SeaGreen;
            this.Label52.Location = new System.Drawing.Point(3, 1);
            this.Label52.Name = "Label52";
            this.Label52.Size = new System.Drawing.Size(79, 20);
            this.Label52.TabIndex = 214;
            this.Label52.Text = "Kimenetel";
            // 
            // Doksik
            // 
            this.Doksik.Location = new System.Drawing.Point(965, 156);
            this.Doksik.Name = "Doksik";
            this.Doksik.Size = new System.Drawing.Size(83, 26);
            this.Doksik.TabIndex = 244;
            // 
            // Label51
            // 
            this.Label51.AutoSize = true;
            this.Label51.BackColor = System.Drawing.Color.SeaGreen;
            this.Label51.Location = new System.Drawing.Point(817, 162);
            this.Label51.Name = "Label51";
            this.Label51.Size = new System.Drawing.Size(126, 20);
            this.Label51.TabIndex = 243;
            this.Label51.Text = "Dokumentumok:";
            // 
            // Fényképek
            // 
            this.Fényképek.Location = new System.Drawing.Point(965, 211);
            this.Fényképek.Name = "Fényképek";
            this.Fényképek.Size = new System.Drawing.Size(83, 26);
            this.Fényképek.TabIndex = 242;
            // 
            // Label50
            // 
            this.Label50.AutoSize = true;
            this.Label50.BackColor = System.Drawing.Color.SeaGreen;
            this.Label50.Location = new System.Drawing.Point(816, 217);
            this.Label50.Name = "Label50";
            this.Label50.Size = new System.Drawing.Size(142, 20);
            this.Label50.TabIndex = 241;
            this.Label50.Text = "Fényképek száma:";
            // 
            // Panel6
            // 
            this.Panel6.BackColor = System.Drawing.Color.SeaGreen;
            this.Panel6.Controls.Add(this.Opt_Törölt);
            this.Panel6.Controls.Add(this.Opt_Elkészült);
            this.Panel6.Controls.Add(this.Opt_Nyitott);
            this.Panel6.Controls.Add(this.Label49);
            this.Panel6.Location = new System.Drawing.Point(1077, 128);
            this.Panel6.Name = "Panel6";
            this.Panel6.Size = new System.Drawing.Size(141, 125);
            this.Panel6.TabIndex = 240;
            // 
            // Opt_Törölt
            // 
            this.Opt_Törölt.AutoSize = true;
            this.Opt_Törölt.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Opt_Törölt.Location = new System.Drawing.Point(7, 87);
            this.Opt_Törölt.Name = "Opt_Törölt";
            this.Opt_Törölt.Size = new System.Drawing.Size(67, 24);
            this.Opt_Törölt.TabIndex = 217;
            this.Opt_Törölt.Text = "Törölt";
            this.Opt_Törölt.UseVisualStyleBackColor = false;
            // 
            // Opt_Elkészült
            // 
            this.Opt_Elkészült.AutoSize = true;
            this.Opt_Elkészült.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Opt_Elkészült.Location = new System.Drawing.Point(7, 57);
            this.Opt_Elkészült.Name = "Opt_Elkészült";
            this.Opt_Elkészült.Size = new System.Drawing.Size(91, 24);
            this.Opt_Elkészült.TabIndex = 216;
            this.Opt_Elkészült.Text = "Elkészült";
            this.Opt_Elkészült.UseVisualStyleBackColor = false;
            // 
            // Opt_Nyitott
            // 
            this.Opt_Nyitott.AutoSize = true;
            this.Opt_Nyitott.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Opt_Nyitott.Checked = true;
            this.Opt_Nyitott.Location = new System.Drawing.Point(7, 24);
            this.Opt_Nyitott.Name = "Opt_Nyitott";
            this.Opt_Nyitott.Size = new System.Drawing.Size(72, 24);
            this.Opt_Nyitott.TabIndex = 215;
            this.Opt_Nyitott.TabStop = true;
            this.Opt_Nyitott.Text = "Nyitott";
            this.Opt_Nyitott.UseVisualStyleBackColor = false;
            // 
            // Label49
            // 
            this.Label49.AutoSize = true;
            this.Label49.BackColor = System.Drawing.Color.SeaGreen;
            this.Label49.Location = new System.Drawing.Point(3, 1);
            this.Label49.Name = "Label49";
            this.Label49.Size = new System.Drawing.Size(129, 20);
            this.Label49.TabIndex = 214;
            this.Label49.Text = "Munka Státusza:";
            // 
            // Btn_Kép_Hozzáad
            // 
            this.Btn_Kép_Hozzáad.BackgroundImage = global::Villamos.Properties.Resources.image_add32;
            this.Btn_Kép_Hozzáad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Kép_Hozzáad.Location = new System.Drawing.Point(764, 192);
            this.Btn_Kép_Hozzáad.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btn_Kép_Hozzáad.Name = "Btn_Kép_Hozzáad";
            this.Btn_Kép_Hozzáad.Size = new System.Drawing.Size(45, 45);
            this.Btn_Kép_Hozzáad.TabIndex = 239;
            this.toolTip1.SetToolTip(this.Btn_Kép_Hozzáad, "*.jpg* vagy *.jpeg* feltöltése");
            this.Btn_Kép_Hozzáad.UseVisualStyleBackColor = true;
            this.Btn_Kép_Hozzáad.Click += new System.EventHandler(this.Btn_Kép_Hozzáad_Click);
            // 
            // Btn_PDF_Hozzáad
            // 
            this.Btn_PDF_Hozzáad.BackgroundImage = global::Villamos.Properties.Resources.pdf_32;
            this.Btn_PDF_Hozzáad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_PDF_Hozzáad.Location = new System.Drawing.Point(765, 141);
            this.Btn_PDF_Hozzáad.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btn_PDF_Hozzáad.Name = "Btn_PDF_Hozzáad";
            this.Btn_PDF_Hozzáad.Size = new System.Drawing.Size(45, 45);
            this.Btn_PDF_Hozzáad.TabIndex = 238;
            this.toolTip1.SetToolTip(this.Btn_PDF_Hozzáad, "PDF formátumú fájlok feltöltése");
            this.Btn_PDF_Hozzáad.UseVisualStyleBackColor = true;
            this.Btn_PDF_Hozzáad.Click += new System.EventHandler(this.Btn_PDF_Hozzáad_Click);
            // 
            // Visszaállít
            // 
            this.Visszaállít.BackgroundImage = global::Villamos.Properties.Resources.visszavonás;
            this.Visszaállít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Visszaállít.Location = new System.Drawing.Point(1120, 9);
            this.Visszaállít.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Visszaállít.Name = "Visszaállít";
            this.Visszaállít.Size = new System.Drawing.Size(45, 45);
            this.Visszaállít.TabIndex = 19;
            this.toolTip1.SetToolTip(this.Visszaállít, "Lezárt munkastátusz visszanyitása");
            this.Visszaállít.UseVisualStyleBackColor = true;
            this.Visszaállít.Click += new System.EventHandler(this.Visszaállít_Click);
            // 
            // Újat
            // 
            this.Újat.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Újat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Újat.Location = new System.Drawing.Point(1067, 9);
            this.Újat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Újat.Name = "Újat";
            this.Újat.Size = new System.Drawing.Size(45, 45);
            this.Újat.TabIndex = 20;
            this.toolTip1.SetToolTip(this.Újat, "Új adatnak előkészíti a beviteli mezőt");
            this.Újat.UseVisualStyleBackColor = true;
            this.Újat.Click += new System.EventHandler(this.Újat_Click);
            // 
            // FékvizsgálatiExcel
            // 
            this.FékvizsgálatiExcel.BackgroundImage = global::Villamos.Properties.Resources.Document_write;
            this.FékvizsgálatiExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FékvizsgálatiExcel.Location = new System.Drawing.Point(1067, 64);
            this.FékvizsgálatiExcel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FékvizsgálatiExcel.Name = "FékvizsgálatiExcel";
            this.FékvizsgálatiExcel.Size = new System.Drawing.Size(45, 45);
            this.FékvizsgálatiExcel.TabIndex = 21;
            this.toolTip1.SetToolTip(this.FékvizsgálatiExcel, "Fékvizsgálati jelentést készít");
            this.FékvizsgálatiExcel.UseVisualStyleBackColor = true;
            this.FékvizsgálatiExcel.Click += new System.EventHandler(this.FékvizsgálatiExcel_Click);
            // 
            // CAFExcel
            // 
            this.CAFExcel.BackgroundImage = global::Villamos.Properties.Resources.CAF;
            this.CAFExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CAFExcel.Location = new System.Drawing.Point(1120, 64);
            this.CAFExcel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CAFExcel.Name = "CAFExcel";
            this.CAFExcel.Size = new System.Drawing.Size(45, 45);
            this.CAFExcel.TabIndex = 22;
            this.toolTip1.SetToolTip(this.CAFExcel, "CAF garanciális jegyzőkönyvet készít");
            this.CAFExcel.UseVisualStyleBackColor = true;
            this.CAFExcel.Click += new System.EventHandler(this.CAFExcel_Click);
            // 
            // Rögzítjelentés
            // 
            this.Rögzítjelentés.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Rögzítjelentés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Rögzítjelentés.Location = new System.Drawing.Point(1173, 9);
            this.Rögzítjelentés.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Rögzítjelentés.Name = "Rögzítjelentés";
            this.Rögzítjelentés.Size = new System.Drawing.Size(45, 45);
            this.Rögzítjelentés.TabIndex = 18;
            this.toolTip1.SetToolTip(this.Rögzítjelentés, "Rögzíti/módosítja az adatokat");
            this.Rögzítjelentés.UseVisualStyleBackColor = true;
            this.Rögzítjelentés.Click += new System.EventHandler(this.Rögzítjelentés_Click);
            // 
            // Viszonylat
            // 
            this.Viszonylat.Location = new System.Drawing.Point(650, 9);
            this.Viszonylat.MaxLength = 20;
            this.Viszonylat.Name = "Viszonylat";
            this.Viszonylat.Size = new System.Drawing.Size(126, 26);
            this.Viszonylat.TabIndex = 3;
            // 
            // Üzembehelyezés
            // 
            this.Üzembehelyezés.Location = new System.Drawing.Point(923, 9);
            this.Üzembehelyezés.Name = "Üzembehelyezés";
            this.Üzembehelyezés.Size = new System.Drawing.Size(126, 26);
            this.Üzembehelyezés.TabIndex = 231;
            // 
            // Szerelvény
            // 
            this.Szerelvény.Location = new System.Drawing.Point(717, 41);
            this.Szerelvény.MaxLength = 50;
            this.Szerelvény.Name = "Szerelvény";
            this.Szerelvény.Size = new System.Drawing.Size(332, 26);
            this.Szerelvény.TabIndex = 2;
            // 
            // KmóraÁllás
            // 
            this.KmóraÁllás.Location = new System.Drawing.Point(717, 75);
            this.KmóraÁllás.MaxLength = 20;
            this.KmóraÁllás.Name = "KmóraÁllás";
            this.KmóraÁllás.Size = new System.Drawing.Size(126, 26);
            this.KmóraÁllás.TabIndex = 229;
            // 
            // Label48
            // 
            this.Label48.AutoSize = true;
            this.Label48.BackColor = System.Drawing.Color.SeaGreen;
            this.Label48.Location = new System.Drawing.Point(559, 15);
            this.Label48.Name = "Label48";
            this.Label48.Size = new System.Drawing.Size(85, 20);
            this.Label48.TabIndex = 228;
            this.Label48.Text = "Viszonylat:";
            // 
            // Label47
            // 
            this.Label47.AutoSize = true;
            this.Label47.BackColor = System.Drawing.Color.SeaGreen;
            this.Label47.Location = new System.Drawing.Point(782, 15);
            this.Label47.Name = "Label47";
            this.Label47.Size = new System.Drawing.Size(135, 20);
            this.Label47.TabIndex = 227;
            this.Label47.Text = "Üzembehelyezés:";
            // 
            // Label46
            // 
            this.Label46.AutoSize = true;
            this.Label46.BackColor = System.Drawing.Color.SeaGreen;
            this.Label46.Location = new System.Drawing.Point(559, 47);
            this.Label46.Name = "Label46";
            this.Label46.Size = new System.Drawing.Size(152, 20);
            this.Label46.TabIndex = 226;
            this.Label46.Text = "Szerelvény járművei:";
            // 
            // Label45
            // 
            this.Label45.AutoSize = true;
            this.Label45.BackColor = System.Drawing.Color.SeaGreen;
            this.Label45.Location = new System.Drawing.Point(559, 78);
            this.Label45.Name = "Label45";
            this.Label45.Size = new System.Drawing.Size(108, 20);
            this.Label45.TabIndex = 225;
            this.Label45.Text = "Km óra állása:";
            // 
            // Forgalmiakadály
            // 
            this.Forgalmiakadály.Location = new System.Drawing.Point(186, 109);
            this.Forgalmiakadály.Name = "Forgalmiakadály";
            this.Forgalmiakadály.Size = new System.Drawing.Size(126, 26);
            this.Forgalmiakadály.TabIndex = 6;
            // 
            // Járművezető
            // 
            this.Járművezető.Location = new System.Drawing.Point(186, 141);
            this.Járművezető.MaxLength = 50;
            this.Járművezető.Name = "Járművezető";
            this.Járművezető.Size = new System.Drawing.Size(535, 26);
            this.Járművezető.TabIndex = 7;
            // 
            // Label44
            // 
            this.Label44.AutoSize = true;
            this.Label44.BackColor = System.Drawing.Color.SeaGreen;
            this.Label44.Location = new System.Drawing.Point(6, 115);
            this.Label44.Name = "Label44";
            this.Label44.Size = new System.Drawing.Size(169, 20);
            this.Label44.TabIndex = 222;
            this.Label44.Text = "Forgalmi akadály ideje:";
            // 
            // Label43
            // 
            this.Label43.AutoSize = true;
            this.Label43.BackColor = System.Drawing.Color.SeaGreen;
            this.Label43.Location = new System.Drawing.Point(6, 146);
            this.Label43.Name = "Label43";
            this.Label43.Size = new System.Drawing.Size(142, 20);
            this.Label43.TabIndex = 221;
            this.Label43.Text = "Járművezető neve:";
            // 
            // Label42
            // 
            this.Label42.AutoSize = true;
            this.Label42.BackColor = System.Drawing.Color.SeaGreen;
            this.Label42.Location = new System.Drawing.Point(272, 15);
            this.Label42.Name = "Label42";
            this.Label42.Size = new System.Drawing.Size(80, 20);
            this.Label42.TabIndex = 220;
            this.Label42.Text = "Telephely:";
            // 
            // Label41
            // 
            this.Label41.AutoSize = true;
            this.Label41.BackColor = System.Drawing.Color.SeaGreen;
            this.Label41.Location = new System.Drawing.Point(272, 44);
            this.Label41.Name = "Label41";
            this.Label41.Size = new System.Drawing.Size(51, 20);
            this.Label41.TabIndex = 219;
            this.Label41.Text = "Típus:";
            // 
            // Telephely
            // 
            this.Telephely.Location = new System.Drawing.Point(367, 9);
            this.Telephely.MaxLength = 15;
            this.Telephely.Name = "Telephely";
            this.Telephely.Size = new System.Drawing.Size(172, 26);
            this.Telephely.TabIndex = 218;
            // 
            // Típus
            // 
            this.Típus.Location = new System.Drawing.Point(367, 41);
            this.Típus.MaxLength = 50;
            this.Típus.Name = "Típus";
            this.Típus.Size = new System.Drawing.Size(172, 26);
            this.Típus.TabIndex = 1;
            // 
            // Pályaszám
            // 
            this.Pályaszám.Location = new System.Drawing.Point(112, 41);
            this.Pályaszám.MaxLength = 10;
            this.Pályaszám.Name = "Pályaszám";
            this.Pályaszám.Size = new System.Drawing.Size(109, 26);
            this.Pályaszám.TabIndex = 0;
            this.Pályaszám.LostFocus += new System.EventHandler(this.TextBox_LostFocus);
            // 
            // Sorszám
            // 
            this.Sorszám.Enabled = false;
            this.Sorszám.Location = new System.Drawing.Point(112, 9);
            this.Sorszám.Name = "Sorszám";
            this.Sorszám.Size = new System.Drawing.Size(109, 26);
            this.Sorszám.TabIndex = 215;
            // 
            // Label40
            // 
            this.Label40.AutoSize = true;
            this.Label40.BackColor = System.Drawing.Color.SeaGreen;
            this.Label40.Location = new System.Drawing.Point(6, 15);
            this.Label40.Name = "Label40";
            this.Label40.Size = new System.Drawing.Size(76, 20);
            this.Label40.TabIndex = 214;
            this.Label40.Text = "Sorszám:";
            // 
            // Label39
            // 
            this.Label39.AutoSize = true;
            this.Label39.BackColor = System.Drawing.Color.SeaGreen;
            this.Label39.Location = new System.Drawing.Point(6, 47);
            this.Label39.Name = "Label39";
            this.Label39.Size = new System.Drawing.Size(89, 20);
            this.Label39.TabIndex = 213;
            this.Label39.Text = "Pályaszám:";
            // 
            // Idő
            // 
            this.Idő.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.Idő.Location = new System.Drawing.Point(367, 73);
            this.Idő.Name = "Idő";
            this.Idő.Size = new System.Drawing.Size(109, 26);
            this.Idő.TabIndex = 5;
            // 
            // Dátum
            // 
            this.Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum.Location = new System.Drawing.Point(112, 74);
            this.Dátum.Name = "Dátum";
            this.Dátum.Size = new System.Drawing.Size(109, 26);
            this.Dátum.TabIndex = 4;
            // 
            // Label37
            // 
            this.Label37.AutoSize = true;
            this.Label37.BackColor = System.Drawing.Color.SeaGreen;
            this.Label37.Location = new System.Drawing.Point(272, 78);
            this.Label37.Name = "Label37";
            this.Label37.Size = new System.Drawing.Size(68, 20);
            this.Label37.TabIndex = 210;
            this.Label37.Text = "Időpont:";
            // 
            // Label38
            // 
            this.Label38.AutoSize = true;
            this.Label38.BackColor = System.Drawing.Color.SeaGreen;
            this.Label38.Location = new System.Drawing.Point(6, 80);
            this.Label38.Name = "Label38";
            this.Label38.Size = new System.Drawing.Size(61, 20);
            this.Label38.TabIndex = 209;
            this.Label38.Text = "Dátum:";
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.TabPage2.Controls.Add(this.LekTelephely);
            this.TabPage2.Controls.Add(this.Lekrendszám);
            this.TabPage2.Controls.Add(this.LekDátumig);
            this.TabPage2.Controls.Add(this.LekDátumtól);
            this.TabPage2.Controls.Add(this.Label4);
            this.TabPage2.Controls.Add(this.Label3);
            this.TabPage2.Controls.Add(this.Label2);
            this.TabPage2.Controls.Add(this.Label1);
            this.TabPage2.Controls.Add(this.Panel2);
            this.TabPage2.Controls.Add(this.Tábla);
            this.TabPage2.Controls.Add(this.LekExcel);
            this.TabPage2.Controls.Add(this.LekLekérdezés);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(1240, 617);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Sérülések lekérdezése";
            // 
            // LekTelephely
            // 
            this.LekTelephely.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LekTelephely.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LekTelephely.FormattingEnabled = true;
            this.LekTelephely.Location = new System.Drawing.Point(330, 36);
            this.LekTelephely.Name = "LekTelephely";
            this.LekTelephely.Size = new System.Drawing.Size(186, 28);
            this.LekTelephely.TabIndex = 191;
            // 
            // Lekrendszám
            // 
            this.Lekrendszám.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Lekrendszám.Location = new System.Drawing.Point(233, 38);
            this.Lekrendszám.Name = "Lekrendszám";
            this.Lekrendszám.Size = new System.Drawing.Size(91, 26);
            this.Lekrendszám.TabIndex = 190;
            // 
            // LekDátumig
            // 
            this.LekDátumig.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LekDátumig.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.LekDátumig.Location = new System.Drawing.Point(118, 38);
            this.LekDátumig.Name = "LekDátumig";
            this.LekDátumig.Size = new System.Drawing.Size(109, 26);
            this.LekDátumig.TabIndex = 189;
            // 
            // LekDátumtól
            // 
            this.LekDátumtól.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LekDátumtól.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.LekDátumtól.Location = new System.Drawing.Point(3, 38);
            this.LekDátumtól.Name = "LekDátumtól";
            this.LekDátumtól.Size = new System.Drawing.Size(109, 26);
            this.LekDátumtól.TabIndex = 188;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Label4.Location = new System.Drawing.Point(118, 15);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(78, 20);
            this.Label4.TabIndex = 187;
            this.Label4.Text = "Dátumtól:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Label3.Location = new System.Drawing.Point(233, 15);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(89, 20);
            this.Label3.TabIndex = 186;
            this.Label3.Text = "Pályaszám:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Label2.Location = new System.Drawing.Point(328, 15);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(80, 20);
            this.Label2.TabIndex = 185;
            this.Label2.Text = "Telephely:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Label1.Location = new System.Drawing.Point(6, 15);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(78, 20);
            this.Label1.TabIndex = 183;
            this.Label1.Text = "Dátumtól:";
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(100)))));
            this.Panel2.Controls.Add(this.LekKész);
            this.Panel2.Controls.Add(this.LekTörölt);
            this.Panel2.Controls.Add(this.LekNyitott);
            this.Panel2.Controls.Add(this.Label5);
            this.Panel2.Controls.Add(this.LekMind);
            this.Panel2.Location = new System.Drawing.Point(522, 8);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(366, 56);
            this.Panel2.TabIndex = 184;
            // 
            // LekKész
            // 
            this.LekKész.AutoSize = true;
            this.LekKész.BackColor = System.Drawing.Color.Cyan;
            this.LekKész.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LekKész.Location = new System.Drawing.Point(196, 25);
            this.LekKész.Name = "LekKész";
            this.LekKész.Size = new System.Drawing.Size(91, 24);
            this.LekKész.TabIndex = 6;
            this.LekKész.TabStop = true;
            this.LekKész.Text = "Elkészült";
            this.LekKész.UseVisualStyleBackColor = false;
            // 
            // LekTörölt
            // 
            this.LekTörölt.AutoSize = true;
            this.LekTörölt.BackColor = System.Drawing.Color.Cyan;
            this.LekTörölt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LekTörölt.Location = new System.Drawing.Point(293, 25);
            this.LekTörölt.Name = "LekTörölt";
            this.LekTörölt.Size = new System.Drawing.Size(67, 24);
            this.LekTörölt.TabIndex = 5;
            this.LekTörölt.TabStop = true;
            this.LekTörölt.Text = "Törölt";
            this.LekTörölt.UseVisualStyleBackColor = false;
            // 
            // LekNyitott
            // 
            this.LekNyitott.AutoSize = true;
            this.LekNyitott.BackColor = System.Drawing.Color.Cyan;
            this.LekNyitott.Checked = true;
            this.LekNyitott.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LekNyitott.Location = new System.Drawing.Point(118, 25);
            this.LekNyitott.Name = "LekNyitott";
            this.LekNyitott.Size = new System.Drawing.Size(72, 24);
            this.LekNyitott.TabIndex = 4;
            this.LekNyitott.TabStop = true;
            this.LekNyitott.Text = "Nyitott";
            this.LekNyitott.UseVisualStyleBackColor = false;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.BackColor = System.Drawing.Color.Cyan;
            this.Label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Label5.Location = new System.Drawing.Point(0, 0);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(112, 20);
            this.Label5.TabIndex = 3;
            this.Label5.Text = "Munka Státus:";
            // 
            // LekMind
            // 
            this.LekMind.AutoSize = true;
            this.LekMind.BackColor = System.Drawing.Color.Cyan;
            this.LekMind.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LekMind.Location = new System.Drawing.Point(5, 25);
            this.LekMind.Name = "LekMind";
            this.LekMind.Size = new System.Drawing.Size(61, 24);
            this.LekMind.TabIndex = 0;
            this.LekMind.TabStop = true;
            this.LekMind.Text = "Mind";
            this.LekMind.UseVisualStyleBackColor = false;
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.EnableHeadersVisualStyles = false;
            this.Tábla.Location = new System.Drawing.Point(5, 70);
            this.Tábla.Name = "Tábla";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Tábla.RowHeadersVisible = false;
            this.Tábla.RowHeadersWidth = 51;
            this.Tábla.Size = new System.Drawing.Size(1233, 541);
            this.Tábla.TabIndex = 182;
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            // 
            // LekExcel
            // 
            this.LekExcel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.LekExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LekExcel.Location = new System.Drawing.Point(940, 24);
            this.LekExcel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LekExcel.Name = "LekExcel";
            this.LekExcel.Size = new System.Drawing.Size(40, 40);
            this.LekExcel.TabIndex = 193;
            this.toolTip1.SetToolTip(this.LekExcel, "Excel táblázatot készít a táblázat adataiból");
            this.LekExcel.UseVisualStyleBackColor = true;
            this.LekExcel.Click += new System.EventHandler(this.LekExcel_Click);
            // 
            // LekLekérdezés
            // 
            this.LekLekérdezés.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.LekLekérdezés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LekLekérdezés.Location = new System.Drawing.Point(894, 24);
            this.LekLekérdezés.Name = "LekLekérdezés";
            this.LekLekérdezés.Size = new System.Drawing.Size(40, 40);
            this.LekLekérdezés.TabIndex = 192;
            this.toolTip1.SetToolTip(this.LekLekérdezés, "Frissíti a listát");
            this.LekLekérdezés.UseVisualStyleBackColor = true;
            this.LekLekérdezés.Click += new System.EventHandler(this.LekLekérdezés_Click);
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.TabPage3.Controls.Add(this.ChckBxDigitális);
            this.TabPage3.Controls.Add(this.Elkészült);
            this.TabPage3.Controls.Add(this.Chck_Egyszerüsített);
            this.TabPage3.Controls.Add(this.KöltTelephely);
            this.TabPage3.Controls.Add(this.KöltRendszám);
            this.TabPage3.Controls.Add(this.KöltDátumig);
            this.TabPage3.Controls.Add(this.KöltDátumtól);
            this.TabPage3.Controls.Add(this.Label6);
            this.TabPage3.Controls.Add(this.Label7);
            this.TabPage3.Controls.Add(this.Label8);
            this.TabPage3.Controls.Add(this.Label9);
            this.TabPage3.Controls.Add(this.Panel3);
            this.TabPage3.Controls.Add(this.Tábla2);
            this.TabPage3.Controls.Add(this.NyomtatványKitöltés);
            this.TabPage3.Controls.Add(this.ExcelNullás);
            this.TabPage3.Controls.Add(this.CsoportkijelölMind);
            this.TabPage3.Controls.Add(this.CsoportVissza);
            this.TabPage3.Controls.Add(this.Nullás);
            this.TabPage3.Controls.Add(this.ExcelKöltség);
            this.TabPage3.Controls.Add(this.KöltLekérdezés);
            this.TabPage3.Location = new System.Drawing.Point(4, 29);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage3.Size = new System.Drawing.Size(1240, 617);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Költségkimutatás készítés";
            // 
            // ChckBxDigitális
            // 
            this.ChckBxDigitális.AutoSize = true;
            this.ChckBxDigitális.Checked = true;
            this.ChckBxDigitális.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChckBxDigitális.Location = new System.Drawing.Point(911, 6);
            this.ChckBxDigitális.Name = "ChckBxDigitális";
            this.ChckBxDigitális.Size = new System.Drawing.Size(135, 24);
            this.ChckBxDigitális.TabIndex = 219;
            this.ChckBxDigitális.Text = "Digitális Aláírás";
            this.ChckBxDigitális.UseVisualStyleBackColor = true;
            this.ChckBxDigitális.Visible = false;
            // 
            // Elkészült
            // 
            this.Elkészült.BackgroundImage = global::Villamos.Properties.Resources.process_accept;
            this.Elkészült.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Elkészült.Location = new System.Drawing.Point(520, 30);
            this.Elkészült.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Elkészült.Name = "Elkészült";
            this.Elkészült.Size = new System.Drawing.Size(40, 40);
            this.Elkészült.TabIndex = 218;
            this.toolTip1.SetToolTip(this.Elkészült, "Az elkészült költségkimutatások státuszát lehet elkészültre állítani.");
            this.Elkészült.UseVisualStyleBackColor = true;
            this.Elkészült.Visible = false;
            this.Elkészült.Click += new System.EventHandler(this.Elkészült_Click);
            // 
            // Chck_Egyszerüsített
            // 
            this.Chck_Egyszerüsített.AutoSize = true;
            this.Chck_Egyszerüsített.Checked = true;
            this.Chck_Egyszerüsített.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Chck_Egyszerüsített.Location = new System.Drawing.Point(1052, 6);
            this.Chck_Egyszerüsített.Name = "Chck_Egyszerüsített";
            this.Chck_Egyszerüsített.Size = new System.Drawing.Size(134, 24);
            this.Chck_Egyszerüsített.TabIndex = 217;
            this.Chck_Egyszerüsített.Text = "Egyszerrűsített";
            this.Chck_Egyszerüsített.UseVisualStyleBackColor = true;
            this.Chck_Egyszerüsített.Visible = false;
            // 
            // KöltTelephely
            // 
            this.KöltTelephely.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.KöltTelephely.FormattingEnabled = true;
            this.KöltTelephely.Location = new System.Drawing.Point(329, 44);
            this.KöltTelephely.Name = "KöltTelephely";
            this.KöltTelephely.Size = new System.Drawing.Size(186, 28);
            this.KöltTelephely.TabIndex = 209;
            // 
            // KöltRendszám
            // 
            this.KöltRendszám.Location = new System.Drawing.Point(232, 46);
            this.KöltRendszám.Name = "KöltRendszám";
            this.KöltRendszám.Size = new System.Drawing.Size(91, 26);
            this.KöltRendszám.TabIndex = 208;
            // 
            // KöltDátumig
            // 
            this.KöltDátumig.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.KöltDátumig.Location = new System.Drawing.Point(117, 46);
            this.KöltDátumig.Name = "KöltDátumig";
            this.KöltDátumig.Size = new System.Drawing.Size(109, 26);
            this.KöltDátumig.TabIndex = 207;
            // 
            // KöltDátumtól
            // 
            this.KöltDátumtól.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.KöltDátumtól.Location = new System.Drawing.Point(2, 46);
            this.KöltDátumtól.Name = "KöltDátumtól";
            this.KöltDátumtól.Size = new System.Drawing.Size(109, 26);
            this.KöltDátumtól.TabIndex = 206;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(117, 23);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(78, 20);
            this.Label6.TabIndex = 205;
            this.Label6.Text = "Dátumtól:";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(232, 23);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(89, 20);
            this.Label7.TabIndex = 204;
            this.Label7.Text = "Pályaszám:";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(327, 23);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(80, 20);
            this.Label8.TabIndex = 203;
            this.Label8.Text = "Telephely:";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(5, 23);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(78, 20);
            this.Label9.TabIndex = 201;
            this.Label9.Text = "Dátumtól:";
            // 
            // Panel3
            // 
            this.Panel3.Controls.Add(this.KöltKész);
            this.Panel3.Controls.Add(this.KöltNyitott);
            this.Panel3.Controls.Add(this.Label10);
            this.Panel3.Controls.Add(this.KöltMind);
            this.Panel3.Location = new System.Drawing.Point(566, 16);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(249, 56);
            this.Panel3.TabIndex = 202;
            // 
            // KöltKész
            // 
            this.KöltKész.AutoSize = true;
            this.KöltKész.BackColor = System.Drawing.Color.MediumAquamarine;
            this.KöltKész.Location = new System.Drawing.Point(150, 25);
            this.KöltKész.Name = "KöltKész";
            this.KöltKész.Size = new System.Drawing.Size(91, 24);
            this.KöltKész.TabIndex = 6;
            this.KöltKész.TabStop = true;
            this.KöltKész.Text = "Elkészült";
            this.KöltKész.UseVisualStyleBackColor = false;
            // 
            // KöltNyitott
            // 
            this.KöltNyitott.AutoSize = true;
            this.KöltNyitott.BackColor = System.Drawing.Color.MediumAquamarine;
            this.KöltNyitott.Checked = true;
            this.KöltNyitott.Location = new System.Drawing.Point(72, 25);
            this.KöltNyitott.Name = "KöltNyitott";
            this.KöltNyitott.Size = new System.Drawing.Size(72, 24);
            this.KöltNyitott.TabIndex = 4;
            this.KöltNyitott.TabStop = true;
            this.KöltNyitott.Text = "Nyitott";
            this.KöltNyitott.UseVisualStyleBackColor = false;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Label10.Location = new System.Drawing.Point(0, 0);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(179, 20);
            this.Label10.TabIndex = 3;
            this.Label10.Text = "Költségkimutatás státus";
            // 
            // KöltMind
            // 
            this.KöltMind.AutoSize = true;
            this.KöltMind.BackColor = System.Drawing.Color.MediumAquamarine;
            this.KöltMind.Location = new System.Drawing.Point(5, 25);
            this.KöltMind.Name = "KöltMind";
            this.KöltMind.Size = new System.Drawing.Size(61, 24);
            this.KöltMind.TabIndex = 0;
            this.KöltMind.Text = "Mind";
            this.KöltMind.UseVisualStyleBackColor = false;
            // 
            // Tábla2
            // 
            this.Tábla2.AllowUserToAddRows = false;
            this.Tábla2.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Tábla2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.Tábla2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.Tábla2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla2.EnableHeadersVisualStyles = false;
            this.Tábla2.Location = new System.Drawing.Point(5, 78);
            this.Tábla2.Name = "Tábla2";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla2.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.Tábla2.RowHeadersWidth = 51;
            this.Tábla2.Size = new System.Drawing.Size(1233, 533);
            this.Tábla2.TabIndex = 200;
            this.Tábla2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla2_CellClick);
            // 
            // NyomtatványKitöltés
            // 
            this.NyomtatványKitöltés.BackgroundImage = global::Villamos.Properties.Resources.Aha_Soft_Large_Seo_SEO;
            this.NyomtatványKitöltés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.NyomtatványKitöltés.Location = new System.Drawing.Point(1052, 30);
            this.NyomtatványKitöltés.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.NyomtatványKitöltés.Name = "NyomtatványKitöltés";
            this.NyomtatványKitöltés.Size = new System.Drawing.Size(40, 40);
            this.NyomtatványKitöltés.TabIndex = 216;
            this.toolTip1.SetToolTip(this.NyomtatványKitöltés, "Költségkimutatást készít");
            this.NyomtatványKitöltés.UseVisualStyleBackColor = true;
            this.NyomtatványKitöltés.Visible = false;
            this.NyomtatványKitöltés.Click += new System.EventHandler(this.NyomtatványKitöltés_Click);
            // 
            // ExcelNullás
            // 
            this.ExcelNullás.BackgroundImage = global::Villamos.Properties.Resources.nullás_lista32;
            this.ExcelNullás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ExcelNullás.Location = new System.Drawing.Point(1005, 30);
            this.ExcelNullás.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ExcelNullás.Name = "ExcelNullás";
            this.ExcelNullás.Size = new System.Drawing.Size(40, 40);
            this.ExcelNullás.TabIndex = 215;
            this.toolTip1.SetToolTip(this.ExcelNullás, "„0” listát Excel táblába menti");
            this.ExcelNullás.UseVisualStyleBackColor = true;
            this.ExcelNullás.Click += new System.EventHandler(this.ExcelNullás_Click);
            // 
            // CsoportkijelölMind
            // 
            this.CsoportkijelölMind.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.CsoportkijelölMind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CsoportkijelölMind.Location = new System.Drawing.Point(909, 30);
            this.CsoportkijelölMind.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CsoportkijelölMind.Name = "CsoportkijelölMind";
            this.CsoportkijelölMind.Size = new System.Drawing.Size(40, 40);
            this.CsoportkijelölMind.TabIndex = 213;
            this.toolTip1.SetToolTip(this.CsoportkijelölMind, "Mindent kijelöl");
            this.CsoportkijelölMind.UseVisualStyleBackColor = true;
            this.CsoportkijelölMind.Click += new System.EventHandler(this.CsoportkijelölMind_Click);
            // 
            // CsoportVissza
            // 
            this.CsoportVissza.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.CsoportVissza.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CsoportVissza.Location = new System.Drawing.Point(957, 30);
            this.CsoportVissza.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CsoportVissza.Name = "CsoportVissza";
            this.CsoportVissza.Size = new System.Drawing.Size(40, 40);
            this.CsoportVissza.TabIndex = 214;
            this.toolTip1.SetToolTip(this.CsoportVissza, "Minden kijelölést töröl");
            this.CsoportVissza.UseVisualStyleBackColor = true;
            this.CsoportVissza.Click += new System.EventHandler(this.CsoportVissza_Click);
            // 
            // Nullás
            // 
            this.Nullás.BackgroundImage = global::Villamos.Properties.Resources._0;
            this.Nullás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Nullás.Location = new System.Drawing.Point(862, 30);
            this.Nullás.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Nullás.Name = "Nullás";
            this.Nullás.Size = new System.Drawing.Size(40, 40);
            this.Nullás.TabIndex = 212;
            this.toolTip1.SetToolTip(this.Nullás, "A szűrési feltételnek megfelelő „0”- jelentések listáját készíti elő");
            this.Nullás.UseVisualStyleBackColor = true;
            this.Nullás.Click += new System.EventHandler(this.Nullás_Click);
            // 
            // ExcelKöltség
            // 
            this.ExcelKöltség.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.ExcelKöltség.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ExcelKöltség.Location = new System.Drawing.Point(1192, 30);
            this.ExcelKöltség.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ExcelKöltség.Name = "ExcelKöltség";
            this.ExcelKöltség.Size = new System.Drawing.Size(40, 40);
            this.ExcelKöltség.TabIndex = 211;
            this.toolTip1.SetToolTip(this.ExcelKöltség, "Excel táblázatot készít a táblázat adataiból");
            this.ExcelKöltség.UseVisualStyleBackColor = true;
            this.ExcelKöltség.Click += new System.EventHandler(this.ExcelKöltség_Click);
            // 
            // KöltLekérdezés
            // 
            this.KöltLekérdezés.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.KöltLekérdezés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.KöltLekérdezés.Location = new System.Drawing.Point(816, 30);
            this.KöltLekérdezés.Name = "KöltLekérdezés";
            this.KöltLekérdezés.Size = new System.Drawing.Size(40, 40);
            this.KöltLekérdezés.TabIndex = 210;
            this.toolTip1.SetToolTip(this.KöltLekérdezés, "Frissíti a listát");
            this.KöltLekérdezés.UseVisualStyleBackColor = true;
            this.KöltLekérdezés.Click += new System.EventHandler(this.KöltLekérdezés_Click);
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.RoyalBlue;
            this.TabPage4.Controls.Add(this.SapTelephely);
            this.TabPage4.Controls.Add(this.Label67);
            this.TabPage4.Controls.Add(this.SapRendelés);
            this.TabPage4.Controls.Add(this.Label63);
            this.TabPage4.Controls.Add(this.SapDátum);
            this.TabPage4.Controls.Add(this.Label64);
            this.TabPage4.Controls.Add(this.SapSorszám);
            this.TabPage4.Controls.Add(this.Label65);
            this.TabPage4.Controls.Add(this.SapPályaszám);
            this.TabPage4.Controls.Add(this.Label66);
            this.TabPage4.Controls.Add(this.Btn_SAP_Feltöltés_Excelből);
            this.TabPage4.Controls.Add(this.Btn_SAP_Betöltés_Excelbe);
            this.TabPage4.Controls.Add(this.RendelésAdatokSzolgáltatás);
            this.TabPage4.Controls.Add(this.RendelésAdatokAnyag);
            this.TabPage4.Controls.Add(this.RendelésAdatokIdő);
            this.TabPage4.Controls.Add(this.SAPBeolvasó);
            this.TabPage4.Controls.Add(this.Tábla1);
            this.TabPage4.Location = new System.Drawing.Point(4, 29);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage4.Size = new System.Drawing.Size(1240, 617);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "SAP adatok feltöltése";
            // 
            // SapTelephely
            // 
            this.SapTelephely.Enabled = false;
            this.SapTelephely.Location = new System.Drawing.Point(946, 13);
            this.SapTelephely.Name = "SapTelephely";
            this.SapTelephely.Size = new System.Drawing.Size(133, 26);
            this.SapTelephely.TabIndex = 219;
            // 
            // Label67
            // 
            this.Label67.AutoSize = true;
            this.Label67.Location = new System.Drawing.Point(860, 19);
            this.Label67.Name = "Label67";
            this.Label67.Size = new System.Drawing.Size(80, 20);
            this.Label67.TabIndex = 218;
            this.Label67.Text = "Telephely:";
            // 
            // SapRendelés
            // 
            this.SapRendelés.Location = new System.Drawing.Point(705, 10);
            this.SapRendelés.Name = "SapRendelés";
            this.SapRendelés.Size = new System.Drawing.Size(149, 26);
            this.SapRendelés.TabIndex = 217;
            // 
            // Label63
            // 
            this.Label63.AutoSize = true;
            this.Label63.Location = new System.Drawing.Point(576, 16);
            this.Label63.Name = "Label63";
            this.Label63.Size = new System.Drawing.Size(123, 20);
            this.Label63.TabIndex = 216;
            this.Label63.Text = "Rendelés szám:";
            // 
            // SapDátum
            // 
            this.SapDátum.Enabled = false;
            this.SapDátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.SapDátum.Location = new System.Drawing.Point(461, 11);
            this.SapDátum.Name = "SapDátum";
            this.SapDátum.Size = new System.Drawing.Size(109, 26);
            this.SapDátum.TabIndex = 215;
            // 
            // Label64
            // 
            this.Label64.AutoSize = true;
            this.Label64.Location = new System.Drawing.Point(377, 16);
            this.Label64.Name = "Label64";
            this.Label64.Size = new System.Drawing.Size(61, 20);
            this.Label64.TabIndex = 213;
            this.Label64.Text = "Dátum:";
            // 
            // SapSorszám
            // 
            this.SapSorszám.Enabled = false;
            this.SapSorszám.Location = new System.Drawing.Point(88, 10);
            this.SapSorszám.Name = "SapSorszám";
            this.SapSorszám.Size = new System.Drawing.Size(91, 26);
            this.SapSorszám.TabIndex = 212;
            // 
            // Label65
            // 
            this.Label65.AutoSize = true;
            this.Label65.Location = new System.Drawing.Point(6, 16);
            this.Label65.Name = "Label65";
            this.Label65.Size = new System.Drawing.Size(76, 20);
            this.Label65.TabIndex = 211;
            this.Label65.Text = "Sorszám:";
            // 
            // SapPályaszám
            // 
            this.SapPályaszám.Enabled = false;
            this.SapPályaszám.Location = new System.Drawing.Point(280, 10);
            this.SapPályaszám.Name = "SapPályaszám";
            this.SapPályaszám.Size = new System.Drawing.Size(91, 26);
            this.SapPályaszám.TabIndex = 210;
            // 
            // Label66
            // 
            this.Label66.AutoSize = true;
            this.Label66.Location = new System.Drawing.Point(185, 16);
            this.Label66.Name = "Label66";
            this.Label66.Size = new System.Drawing.Size(89, 20);
            this.Label66.TabIndex = 209;
            this.Label66.Text = "Pályaszám:";
            // 
            // Btn_SAP_Feltöltés_Excelből
            // 
            this.Btn_SAP_Feltöltés_Excelből.BackColor = System.Drawing.Color.Silver;
            this.Btn_SAP_Feltöltés_Excelből.Location = new System.Drawing.Point(1082, 49);
            this.Btn_SAP_Feltöltés_Excelből.Name = "Btn_SAP_Feltöltés_Excelből";
            this.Btn_SAP_Feltöltés_Excelből.Size = new System.Drawing.Size(148, 49);
            this.Btn_SAP_Feltöltés_Excelből.TabIndex = 196;
            this.Btn_SAP_Feltöltés_Excelből.Text = "Anyagok feltöltése Excelből";
            this.Btn_SAP_Feltöltés_Excelből.UseVisualStyleBackColor = false;
            this.Btn_SAP_Feltöltés_Excelből.Click += new System.EventHandler(this.Btn_SAP_Feltöltés_Excelből_Click);
            // 
            // Btn_SAP_Betöltés_Excelbe
            // 
            this.Btn_SAP_Betöltés_Excelbe.BackColor = System.Drawing.Color.Silver;
            this.Btn_SAP_Betöltés_Excelbe.Location = new System.Drawing.Point(977, 49);
            this.Btn_SAP_Betöltés_Excelbe.Name = "Btn_SAP_Betöltés_Excelbe";
            this.Btn_SAP_Betöltés_Excelbe.Size = new System.Drawing.Size(99, 49);
            this.Btn_SAP_Betöltés_Excelbe.TabIndex = 195;
            this.Btn_SAP_Betöltés_Excelbe.Text = "Betöltési Exceltábla";
            this.Btn_SAP_Betöltés_Excelbe.UseVisualStyleBackColor = false;
            this.Btn_SAP_Betöltés_Excelbe.Click += new System.EventHandler(this.Btn_SAP_Betöltés_Excelbe_Click);
            // 
            // RendelésAdatokSzolgáltatás
            // 
            this.RendelésAdatokSzolgáltatás.BackColor = System.Drawing.Color.Silver;
            this.RendelésAdatokSzolgáltatás.Location = new System.Drawing.Point(329, 49);
            this.RendelésAdatokSzolgáltatás.Name = "RendelésAdatokSzolgáltatás";
            this.RendelésAdatokSzolgáltatás.Size = new System.Drawing.Size(106, 49);
            this.RendelésAdatokSzolgáltatás.TabIndex = 194;
            this.RendelésAdatokSzolgáltatás.Text = "Költség adatok";
            this.RendelésAdatokSzolgáltatás.UseVisualStyleBackColor = false;
            this.RendelésAdatokSzolgáltatás.Click += new System.EventHandler(this.RendelésAdatokSzolgáltatás_Click);
            // 
            // RendelésAdatokAnyag
            // 
            this.RendelésAdatokAnyag.BackColor = System.Drawing.Color.Silver;
            this.RendelésAdatokAnyag.Location = new System.Drawing.Point(132, 49);
            this.RendelésAdatokAnyag.Name = "RendelésAdatokAnyag";
            this.RendelésAdatokAnyag.Size = new System.Drawing.Size(92, 49);
            this.RendelésAdatokAnyag.TabIndex = 193;
            this.RendelésAdatokAnyag.Text = "Anyag adatok";
            this.RendelésAdatokAnyag.UseVisualStyleBackColor = false;
            this.RendelésAdatokAnyag.Click += new System.EventHandler(this.RendelésAdatokAnyag_Click);
            // 
            // RendelésAdatokIdő
            // 
            this.RendelésAdatokIdő.BackColor = System.Drawing.Color.Silver;
            this.RendelésAdatokIdő.Location = new System.Drawing.Point(230, 49);
            this.RendelésAdatokIdő.Name = "RendelésAdatokIdő";
            this.RendelésAdatokIdő.Size = new System.Drawing.Size(93, 49);
            this.RendelésAdatokIdő.TabIndex = 192;
            this.RendelésAdatokIdő.Text = "Munkaidő adatok";
            this.RendelésAdatokIdő.UseVisualStyleBackColor = false;
            this.RendelésAdatokIdő.Click += new System.EventHandler(this.RendelésAdatokIdő_Click);
            // 
            // SAPBeolvasó
            // 
            this.SAPBeolvasó.BackColor = System.Drawing.Color.Silver;
            this.SAPBeolvasó.Location = new System.Drawing.Point(6, 49);
            this.SAPBeolvasó.Name = "SAPBeolvasó";
            this.SAPBeolvasó.Size = new System.Drawing.Size(120, 49);
            this.SAPBeolvasó.TabIndex = 191;
            this.SAPBeolvasó.Text = "SAP adatok beolvasása";
            this.SAPBeolvasó.UseVisualStyleBackColor = false;
            this.SAPBeolvasó.Click += new System.EventHandler(this.SAPBeolvasó_Click);
            // 
            // Tábla1
            // 
            this.Tábla1.AllowUserToAddRows = false;
            this.Tábla1.AllowUserToDeleteRows = false;
            this.Tábla1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla1.EnableHeadersVisualStyles = false;
            this.Tábla1.Location = new System.Drawing.Point(2, 104);
            this.Tábla1.Name = "Tábla1";
            this.Tábla1.RowHeadersVisible = false;
            this.Tábla1.RowHeadersWidth = 51;
            this.Tábla1.Size = new System.Drawing.Size(1233, 507);
            this.Tábla1.TabIndex = 190;
            // 
            // TabPage5
            // 
            this.TabPage5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(180)))), ((int)(((byte)(200)))));
            this.TabPage5.Controls.Add(this.panel9);
            this.TabPage5.Controls.Add(this.Panel5);
            this.TabPage5.Controls.Add(this.Panel4);
            this.TabPage5.Location = new System.Drawing.Point(4, 29);
            this.TabPage5.Name = "TabPage5";
            this.TabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage5.Size = new System.Drawing.Size(1240, 617);
            this.TabPage5.TabIndex = 4;
            this.TabPage5.Text = "Állandó értékek";
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(180)))), ((int)(((byte)(200)))));
            this.panel9.Controls.Add(this.label72);
            this.panel9.Controls.Add(this.TxtBxDigitalisAlairo1);
            this.panel9.Controls.Add(this.TxtBxBeosztas2);
            this.panel9.Controls.Add(this.label73);
            this.panel9.Controls.Add(this.Btn_Digitális_Aláírók);
            this.panel9.Controls.Add(this.label69);
            this.panel9.Controls.Add(this.TxtBxBeosztas1);
            this.panel9.Controls.Add(this.label70);
            this.panel9.Controls.Add(this.label71);
            this.panel9.Controls.Add(this.TxtBxDigitalisAlairo2);
            this.panel9.Location = new System.Drawing.Point(8, 414);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(736, 190);
            this.panel9.TabIndex = 186;
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(3, 5);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(118, 20);
            this.label72.TabIndex = 212;
            this.label72.Text = "Digitális aláírók:";
            // 
            // TxtBxDigitalisAlairo1
            // 
            this.TxtBxDigitalisAlairo1.Location = new System.Drawing.Point(216, 57);
            this.TxtBxDigitalisAlairo1.Name = "TxtBxDigitalisAlairo1";
            this.TxtBxDigitalisAlairo1.Size = new System.Drawing.Size(503, 26);
            this.TxtBxDigitalisAlairo1.TabIndex = 211;
            // 
            // TxtBxBeosztas2
            // 
            this.TxtBxBeosztas2.Location = new System.Drawing.Point(216, 153);
            this.TxtBxBeosztas2.Name = "TxtBxBeosztas2";
            this.TxtBxBeosztas2.Size = new System.Drawing.Size(503, 26);
            this.TxtBxBeosztas2.TabIndex = 210;
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Location = new System.Drawing.Point(23, 159);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(93, 20);
            this.label73.TabIndex = 209;
            this.label73.Text = "Beosztás 2:";
            // 
            // Btn_Digitális_Aláírók
            // 
            this.Btn_Digitális_Aláírók.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Btn_Digitális_Aláírók.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Digitális_Aláírók.Location = new System.Drawing.Point(674, 5);
            this.Btn_Digitális_Aláírók.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btn_Digitális_Aláírók.Name = "Btn_Digitális_Aláírók";
            this.Btn_Digitális_Aláírók.Size = new System.Drawing.Size(45, 45);
            this.Btn_Digitális_Aláírók.TabIndex = 185;
            this.toolTip1.SetToolTip(this.Btn_Digitális_Aláírók, "Rögzíti/módosítja az adatokat");
            this.Btn_Digitális_Aláírók.UseVisualStyleBackColor = true;
            this.Btn_Digitális_Aláírók.Click += new System.EventHandler(this.Btn_Digitális_Aláírók_Click);
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(23, 63);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(123, 20);
            this.label69.TabIndex = 208;
            this.label69.Text = "Digitális aláíró 1:";
            // 
            // TxtBxBeosztas1
            // 
            this.TxtBxBeosztas1.Location = new System.Drawing.Point(216, 89);
            this.TxtBxBeosztas1.Name = "TxtBxBeosztas1";
            this.TxtBxBeosztas1.Size = new System.Drawing.Size(503, 26);
            this.TxtBxBeosztas1.TabIndex = 186;
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(23, 127);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(123, 20);
            this.label70.TabIndex = 184;
            this.label70.Text = "Digitális aláíró 2:";
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(23, 95);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(93, 20);
            this.label71.TabIndex = 183;
            this.label71.Text = "Beosztás 1:";
            // 
            // TxtBxDigitalisAlairo2
            // 
            this.TxtBxDigitalisAlairo2.Location = new System.Drawing.Point(216, 121);
            this.TxtBxDigitalisAlairo2.Name = "TxtBxDigitalisAlairo2";
            this.TxtBxDigitalisAlairo2.Size = new System.Drawing.Size(503, 26);
            this.TxtBxDigitalisAlairo2.TabIndex = 182;
            // 
            // Panel5
            // 
            this.Panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(180)))), ((int)(((byte)(200)))));
            this.Panel5.Controls.Add(this.Label62);
            this.Panel5.Controls.Add(this.Dátum_tarifa);
            this.Panel5.Controls.Add(this.ÉvestarifaD03);
            this.Panel5.Controls.Add(this.Btn_ÁllandóÉrt_Tarifa_Rögzít);
            this.Panel5.Controls.Add(this.Label36);
            this.Panel5.Controls.Add(this.Label35);
            this.Panel5.Controls.Add(this.Label34);
            this.Panel5.Controls.Add(this.ÉvestarifaD60);
            this.Panel5.Location = new System.Drawing.Point(750, 6);
            this.Panel5.Name = "Panel5";
            this.Panel5.Size = new System.Drawing.Size(484, 154);
            this.Panel5.TabIndex = 1;
            // 
            // Label62
            // 
            this.Label62.AutoSize = true;
            this.Label62.Location = new System.Drawing.Point(12, 50);
            this.Label62.Name = "Label62";
            this.Label62.Size = new System.Drawing.Size(82, 20);
            this.Label62.TabIndex = 208;
            this.Label62.Text = "Tarifa éve:";
            // 
            // Dátum_tarifa
            // 
            this.Dátum_tarifa.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum_tarifa.Location = new System.Drawing.Point(203, 42);
            this.Dátum_tarifa.Name = "Dátum_tarifa";
            this.Dátum_tarifa.Size = new System.Drawing.Size(109, 26);
            this.Dátum_tarifa.TabIndex = 207;
            this.Dátum_tarifa.ValueChanged += new System.EventHandler(this.Dátum_tarifa_ValueChanged);
            // 
            // ÉvestarifaD03
            // 
            this.ÉvestarifaD03.Location = new System.Drawing.Point(203, 111);
            this.ÉvestarifaD03.Name = "ÉvestarifaD03";
            this.ÉvestarifaD03.Size = new System.Drawing.Size(187, 26);
            this.ÉvestarifaD03.TabIndex = 186;
            // 
            // Btn_ÁllandóÉrt_Tarifa_Rögzít
            // 
            this.Btn_ÁllandóÉrt_Tarifa_Rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Btn_ÁllandóÉrt_Tarifa_Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_ÁllandóÉrt_Tarifa_Rögzít.Location = new System.Drawing.Point(426, 12);
            this.Btn_ÁllandóÉrt_Tarifa_Rögzít.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btn_ÁllandóÉrt_Tarifa_Rögzít.Name = "Btn_ÁllandóÉrt_Tarifa_Rögzít";
            this.Btn_ÁllandóÉrt_Tarifa_Rögzít.Size = new System.Drawing.Size(45, 45);
            this.Btn_ÁllandóÉrt_Tarifa_Rögzít.TabIndex = 185;
            this.toolTip1.SetToolTip(this.Btn_ÁllandóÉrt_Tarifa_Rögzít, "Rögzíti/módosítja az adatokat");
            this.Btn_ÁllandóÉrt_Tarifa_Rögzít.UseVisualStyleBackColor = true;
            this.Btn_ÁllandóÉrt_Tarifa_Rögzít.Click += new System.EventHandler(this.Btn_ÁllandóÉrt_Tarifa_Rögzít_Click);
            // 
            // Label36
            // 
            this.Label36.AutoSize = true;
            this.Label36.Location = new System.Drawing.Point(12, 82);
            this.Label36.Name = "Label36";
            this.Label36.Size = new System.Drawing.Size(169, 20);
            this.Label36.TabIndex = 184;
            this.Label36.Text = "Éves tarifa külső (D60)";
            // 
            // Label35
            // 
            this.Label35.AutoSize = true;
            this.Label35.Location = new System.Drawing.Point(12, 114);
            this.Label35.Name = "Label35";
            this.Label35.Size = new System.Drawing.Size(169, 20);
            this.Label35.TabIndex = 183;
            this.Label35.Text = "Éves tarifa külső (D03)";
            // 
            // Label34
            // 
            this.Label34.AutoSize = true;
            this.Label34.Location = new System.Drawing.Point(3, 2);
            this.Label34.Name = "Label34";
            this.Label34.Size = new System.Drawing.Size(120, 20);
            this.Label34.TabIndex = 181;
            this.Label34.Text = "Tarifa beállítása";
            // 
            // ÉvestarifaD60
            // 
            this.ÉvestarifaD60.Location = new System.Drawing.Point(203, 76);
            this.ÉvestarifaD60.Name = "ÉvestarifaD60";
            this.ÉvestarifaD60.Size = new System.Drawing.Size(187, 26);
            this.ÉvestarifaD60.TabIndex = 182;
            // 
            // Panel4
            // 
            this.Panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(180)))), ((int)(((byte)(200)))));
            this.Panel4.Controls.Add(this.Text7);
            this.Panel4.Controls.Add(this.Text6);
            this.Panel4.Controls.Add(this.Text5);
            this.Panel4.Controls.Add(this.Text4);
            this.Panel4.Controls.Add(this.Text3);
            this.Panel4.Controls.Add(this.Text2);
            this.Panel4.Controls.Add(this.Text1);
            this.Panel4.Controls.Add(this.Eszköz);
            this.Panel4.Controls.Add(this.Telefonszám);
            this.Panel4.Controls.Add(this.Kiállította);
            this.Panel4.Controls.Add(this.Label33);
            this.Panel4.Controls.Add(this.Label32);
            this.Panel4.Controls.Add(this.Label31);
            this.Panel4.Controls.Add(this.Label30);
            this.Panel4.Controls.Add(this.Label29);
            this.Panel4.Controls.Add(this.Label28);
            this.Panel4.Controls.Add(this.Label27);
            this.Panel4.Controls.Add(this.Label26);
            this.Panel4.Controls.Add(this.Label25);
            this.Panel4.Controls.Add(this.Label24);
            this.Panel4.Controls.Add(this.Btn_ÁllandóÉrt_Felépít_Rögzít);
            this.Panel4.Controls.Add(this.Label23);
            this.Panel4.Controls.Add(this.Iktatószám);
            this.Panel4.Location = new System.Drawing.Point(8, 8);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(736, 400);
            this.Panel4.TabIndex = 0;
            // 
            // Text7
            // 
            this.Text7.Location = new System.Drawing.Point(216, 364);
            this.Text7.Name = "Text7";
            this.Text7.Size = new System.Drawing.Size(503, 26);
            this.Text7.TabIndex = 202;
            // 
            // Text6
            // 
            this.Text6.Location = new System.Drawing.Point(216, 332);
            this.Text6.Name = "Text6";
            this.Text6.Size = new System.Drawing.Size(503, 26);
            this.Text6.TabIndex = 201;
            // 
            // Text5
            // 
            this.Text5.Location = new System.Drawing.Point(216, 284);
            this.Text5.Name = "Text5";
            this.Text5.Size = new System.Drawing.Size(503, 26);
            this.Text5.TabIndex = 200;
            // 
            // Text4
            // 
            this.Text4.Location = new System.Drawing.Point(216, 252);
            this.Text4.Name = "Text4";
            this.Text4.Size = new System.Drawing.Size(503, 26);
            this.Text4.TabIndex = 199;
            // 
            // Text3
            // 
            this.Text3.Location = new System.Drawing.Point(216, 220);
            this.Text3.Name = "Text3";
            this.Text3.Size = new System.Drawing.Size(503, 26);
            this.Text3.TabIndex = 198;
            // 
            // Text2
            // 
            this.Text2.Location = new System.Drawing.Point(216, 188);
            this.Text2.Name = "Text2";
            this.Text2.Size = new System.Drawing.Size(503, 26);
            this.Text2.TabIndex = 197;
            // 
            // Text1
            // 
            this.Text1.Location = new System.Drawing.Point(216, 156);
            this.Text1.Name = "Text1";
            this.Text1.Size = new System.Drawing.Size(503, 26);
            this.Text1.TabIndex = 196;
            // 
            // Eszköz
            // 
            this.Eszköz.Location = new System.Drawing.Point(216, 106);
            this.Eszköz.Name = "Eszköz";
            this.Eszköz.Size = new System.Drawing.Size(252, 26);
            this.Eszköz.TabIndex = 195;
            // 
            // Telefonszám
            // 
            this.Telefonszám.Location = new System.Drawing.Point(216, 74);
            this.Telefonszám.Name = "Telefonszám";
            this.Telefonszám.Size = new System.Drawing.Size(252, 26);
            this.Telefonszám.TabIndex = 194;
            // 
            // Kiállította
            // 
            this.Kiállította.Location = new System.Drawing.Point(216, 42);
            this.Kiállította.Name = "Kiállította";
            this.Kiállította.Size = new System.Drawing.Size(252, 26);
            this.Kiállította.TabIndex = 193;
            // 
            // Label33
            // 
            this.Label33.AutoSize = true;
            this.Label33.Location = new System.Drawing.Point(23, 370);
            this.Label33.Name = "Label33";
            this.Label33.Size = new System.Drawing.Size(131, 20);
            this.Label33.TabIndex = 191;
            this.Label33.Text = "Aláíró beosztása:";
            // 
            // Label32
            // 
            this.Label32.AutoSize = true;
            this.Label32.Location = new System.Drawing.Point(23, 338);
            this.Label32.Name = "Label32";
            this.Label32.Size = new System.Drawing.Size(91, 20);
            this.Label32.TabIndex = 190;
            this.Label32.Text = "Aláíró neve:";
            // 
            // Label31
            // 
            this.Label31.AutoSize = true;
            this.Label31.Location = new System.Drawing.Point(23, 290);
            this.Label31.Name = "Label31";
            this.Label31.Size = new System.Drawing.Size(128, 20);
            this.Label31.TabIndex = 189;
            this.Label31.Text = "Felépítés szint 5:";
            // 
            // Label30
            // 
            this.Label30.AutoSize = true;
            this.Label30.Location = new System.Drawing.Point(23, 258);
            this.Label30.Name = "Label30";
            this.Label30.Size = new System.Drawing.Size(128, 20);
            this.Label30.TabIndex = 188;
            this.Label30.Text = "Felépítés szint 4:";
            // 
            // Label29
            // 
            this.Label29.AutoSize = true;
            this.Label29.Location = new System.Drawing.Point(23, 226);
            this.Label29.Name = "Label29";
            this.Label29.Size = new System.Drawing.Size(128, 20);
            this.Label29.TabIndex = 187;
            this.Label29.Text = "Felépítés szint 3:";
            // 
            // Label28
            // 
            this.Label28.AutoSize = true;
            this.Label28.Location = new System.Drawing.Point(23, 194);
            this.Label28.Name = "Label28";
            this.Label28.Size = new System.Drawing.Size(128, 20);
            this.Label28.TabIndex = 186;
            this.Label28.Text = "Felépítés szint 2:";
            // 
            // Label27
            // 
            this.Label27.AutoSize = true;
            this.Label27.Location = new System.Drawing.Point(23, 162);
            this.Label27.Name = "Label27";
            this.Label27.Size = new System.Drawing.Size(128, 20);
            this.Label27.TabIndex = 185;
            this.Label27.Text = "Felépítés szint 1:";
            // 
            // Label26
            // 
            this.Label26.AutoSize = true;
            this.Label26.Location = new System.Drawing.Point(23, 112);
            this.Label26.Name = "Label26";
            this.Label26.Size = new System.Drawing.Size(65, 20);
            this.Label26.TabIndex = 184;
            this.Label26.Text = "Eszköz:";
            // 
            // Label25
            // 
            this.Label25.AutoSize = true;
            this.Label25.Location = new System.Drawing.Point(23, 80);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(104, 20);
            this.Label25.TabIndex = 183;
            this.Label25.Text = "Telefonszám:";
            // 
            // Label24
            // 
            this.Label24.AutoSize = true;
            this.Label24.Location = new System.Drawing.Point(23, 48);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(157, 20);
            this.Label24.TabIndex = 182;
            this.Label24.Text = "Bizonylatot kiállította:";
            // 
            // Btn_ÁllandóÉrt_Felépít_Rögzít
            // 
            this.Btn_ÁllandóÉrt_Felépít_Rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Btn_ÁllandóÉrt_Felépít_Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_ÁllandóÉrt_Felépít_Rögzít.Location = new System.Drawing.Point(674, 10);
            this.Btn_ÁllandóÉrt_Felépít_Rögzít.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btn_ÁllandóÉrt_Felépít_Rögzít.Name = "Btn_ÁllandóÉrt_Felépít_Rögzít";
            this.Btn_ÁllandóÉrt_Felépít_Rögzít.Size = new System.Drawing.Size(45, 45);
            this.Btn_ÁllandóÉrt_Felépít_Rögzít.TabIndex = 181;
            this.toolTip1.SetToolTip(this.Btn_ÁllandóÉrt_Felépít_Rögzít, "Rögzíti/módosítja az adatokat");
            this.Btn_ÁllandóÉrt_Felépít_Rögzít.UseVisualStyleBackColor = true;
            this.Btn_ÁllandóÉrt_Felépít_Rögzít.Click += new System.EventHandler(this.Btn_ÁllandóÉrt_Felépít_Rögzít_Click);
            // 
            // Label23
            // 
            this.Label23.AutoSize = true;
            this.Label23.Location = new System.Drawing.Point(23, 16);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(96, 20);
            this.Label23.TabIndex = 179;
            this.Label23.Text = "Iktató szám:";
            // 
            // Iktatószám
            // 
            this.Iktatószám.Location = new System.Drawing.Point(216, 10);
            this.Iktatószám.Name = "Iktatószám";
            this.Iktatószám.Size = new System.Drawing.Size(252, 26);
            this.Iktatószám.TabIndex = 180;
            // 
            // TabPage6
            // 
            this.TabPage6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.TabPage6.Controls.Add(this.KépTörlés);
            this.TabPage6.Controls.Add(this.KépLementés);
            this.TabPage6.Controls.Add(this.KépKeret);
            this.TabPage6.Controls.Add(this.FényIdő);
            this.TabPage6.Controls.Add(this.FényDátum);
            this.TabPage6.Controls.Add(this.Label15);
            this.TabPage6.Controls.Add(this.Label14);
            this.TabPage6.Controls.Add(this.FénySorszám);
            this.TabPage6.Controls.Add(this.Label12);
            this.TabPage6.Controls.Add(this.FényPályaszám);
            this.TabPage6.Controls.Add(this.Label11);
            this.TabPage6.Controls.Add(this.FileBox);
            this.TabPage6.Location = new System.Drawing.Point(4, 29);
            this.TabPage6.Name = "TabPage6";
            this.TabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage6.Size = new System.Drawing.Size(1240, 617);
            this.TabPage6.TabIndex = 5;
            this.TabPage6.Text = "Fényképek";
            // 
            // KépTörlés
            // 
            this.KépTörlés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.KépTörlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.KépTörlés.Location = new System.Drawing.Point(859, 7);
            this.KépTörlés.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.KépTörlés.Name = "KépTörlés";
            this.KépTörlés.Size = new System.Drawing.Size(45, 45);
            this.KépTörlés.TabIndex = 238;
            this.toolTip1.SetToolTip(this.KépTörlés, "Törli az adatokat");
            this.KépTörlés.UseVisualStyleBackColor = true;
            this.KépTörlés.Click += new System.EventHandler(this.KépTörlés_Click);
            // 
            // KépLementés
            // 
            this.KépLementés.BackgroundImage = global::Villamos.Properties.Resources.Designcontest_Ecommerce_Business_Save;
            this.KépLementés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.KépLementés.Location = new System.Drawing.Point(806, 8);
            this.KépLementés.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.KépLementés.Name = "KépLementés";
            this.KépLementés.Size = new System.Drawing.Size(45, 45);
            this.KépLementés.TabIndex = 237;
            this.toolTip1.SetToolTip(this.KépLementés, "Mentést készít");
            this.KépLementés.UseVisualStyleBackColor = true;
            this.KépLementés.Click += new System.EventHandler(this.KépLementés_Click);
            // 
            // KépKeret
            // 
            this.KépKeret.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.KépKeret.Location = new System.Drawing.Point(213, 61);
            this.KépKeret.Name = "KépKeret";
            this.KépKeret.Size = new System.Drawing.Size(1024, 550);
            this.KépKeret.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.KépKeret.TabIndex = 209;
            this.KépKeret.TabStop = false;
            // 
            // FényIdő
            // 
            this.FényIdő.Enabled = false;
            this.FényIdő.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.FényIdő.Location = new System.Drawing.Point(657, 7);
            this.FényIdő.Name = "FényIdő";
            this.FényIdő.Size = new System.Drawing.Size(109, 26);
            this.FényIdő.TabIndex = 208;
            // 
            // FényDátum
            // 
            this.FényDátum.Enabled = false;
            this.FényDátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.FényDátum.Location = new System.Drawing.Point(468, 7);
            this.FényDátum.Name = "FényDátum";
            this.FényDátum.Size = new System.Drawing.Size(109, 26);
            this.FényDátum.TabIndex = 207;
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Location = new System.Drawing.Point(583, 12);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(68, 20);
            this.Label15.TabIndex = 206;
            this.Label15.Text = "Időpont:";
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(384, 12);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(61, 20);
            this.Label14.TabIndex = 205;
            this.Label14.Text = "Dátum:";
            // 
            // FénySorszám
            // 
            this.FénySorszám.Enabled = false;
            this.FénySorszám.Location = new System.Drawing.Point(95, 6);
            this.FénySorszám.Name = "FénySorszám";
            this.FénySorszám.Size = new System.Drawing.Size(91, 26);
            this.FénySorszám.TabIndex = 204;
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(13, 12);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(76, 20);
            this.Label12.TabIndex = 203;
            this.Label12.Text = "Sorszám:";
            // 
            // FényPályaszám
            // 
            this.FényPályaszám.Enabled = false;
            this.FényPályaszám.Location = new System.Drawing.Point(287, 6);
            this.FényPályaszám.Name = "FényPályaszám";
            this.FényPályaszám.Size = new System.Drawing.Size(91, 26);
            this.FényPályaszám.TabIndex = 202;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(192, 12);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(89, 20);
            this.Label11.TabIndex = 201;
            this.Label11.Text = "Pályaszám:";
            // 
            // FileBox
            // 
            this.FileBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.FileBox.FormattingEnabled = true;
            this.FileBox.ItemHeight = 20;
            this.FileBox.Location = new System.Drawing.Point(4, 58);
            this.FileBox.Name = "FileBox";
            this.FileBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.FileBox.Size = new System.Drawing.Size(203, 524);
            this.FileBox.TabIndex = 200;
            this.FileBox.SelectedIndexChanged += new System.EventHandler(this.FileBox_SelectedIndexChanged);
            // 
            // TabPage7
            // 
            this.TabPage7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TabPage7.Controls.Add(this.PDF_néző);
            this.TabPage7.Controls.Add(this.PdfIdő);
            this.TabPage7.Controls.Add(this.PdfDátum);
            this.TabPage7.Controls.Add(this.Label16);
            this.TabPage7.Controls.Add(this.Label17);
            this.TabPage7.Controls.Add(this.PdfSorszám);
            this.TabPage7.Controls.Add(this.Label18);
            this.TabPage7.Controls.Add(this.PdfPályaszám);
            this.TabPage7.Controls.Add(this.Label19);
            this.TabPage7.Controls.Add(this.PdfTörlés);
            this.TabPage7.Controls.Add(this.FilePDF);
            this.TabPage7.Location = new System.Drawing.Point(4, 29);
            this.TabPage7.Name = "TabPage7";
            this.TabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage7.Size = new System.Drawing.Size(1240, 617);
            this.TabPage7.TabIndex = 6;
            this.TabPage7.Text = "Dokumentumok";
            // 
            // PDF_néző
            // 
            this.PDF_néző.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PDF_néző.Location = new System.Drawing.Point(220, 59);
            this.PDF_néző.Margin = new System.Windows.Forms.Padding(9, 12, 9, 12);
            this.PDF_néző.Name = "PDF_néző";
            this.PDF_néző.Size = new System.Drawing.Size(1008, 546);
            this.PDF_néző.TabIndex = 240;
            this.PDF_néző.ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitWidth;
            // 
            // PdfIdő
            // 
            this.PdfIdő.Enabled = false;
            this.PdfIdő.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.PdfIdő.Location = new System.Drawing.Point(658, 8);
            this.PdfIdő.Name = "PdfIdő";
            this.PdfIdő.Size = new System.Drawing.Size(109, 26);
            this.PdfIdő.TabIndex = 217;
            // 
            // PdfDátum
            // 
            this.PdfDátum.Enabled = false;
            this.PdfDátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.PdfDátum.Location = new System.Drawing.Point(469, 8);
            this.PdfDátum.Name = "PdfDátum";
            this.PdfDátum.Size = new System.Drawing.Size(109, 26);
            this.PdfDátum.TabIndex = 216;
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Location = new System.Drawing.Point(584, 13);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(68, 20);
            this.Label16.TabIndex = 215;
            this.Label16.Text = "Időpont:";
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.Location = new System.Drawing.Point(385, 13);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(61, 20);
            this.Label17.TabIndex = 214;
            this.Label17.Text = "Dátum:";
            // 
            // PdfSorszám
            // 
            this.PdfSorszám.Enabled = false;
            this.PdfSorszám.Location = new System.Drawing.Point(96, 7);
            this.PdfSorszám.Name = "PdfSorszám";
            this.PdfSorszám.Size = new System.Drawing.Size(91, 26);
            this.PdfSorszám.TabIndex = 213;
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.Location = new System.Drawing.Point(14, 13);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(76, 20);
            this.Label18.TabIndex = 212;
            this.Label18.Text = "Sorszám:";
            // 
            // PdfPályaszám
            // 
            this.PdfPályaszám.Enabled = false;
            this.PdfPályaszám.Location = new System.Drawing.Point(288, 7);
            this.PdfPályaszám.Name = "PdfPályaszám";
            this.PdfPályaszám.Size = new System.Drawing.Size(91, 26);
            this.PdfPályaszám.TabIndex = 211;
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.Location = new System.Drawing.Point(193, 13);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(89, 20);
            this.Label19.TabIndex = 210;
            this.Label19.Text = "Pályaszám:";
            // 
            // PdfTörlés
            // 
            this.PdfTörlés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.PdfTörlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PdfTörlés.Location = new System.Drawing.Point(785, 5);
            this.PdfTörlés.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PdfTörlés.Name = "PdfTörlés";
            this.PdfTörlés.Size = new System.Drawing.Size(45, 45);
            this.PdfTörlés.TabIndex = 239;
            this.toolTip1.SetToolTip(this.PdfTörlés, "Törli az adatokat");
            this.PdfTörlés.UseVisualStyleBackColor = true;
            this.PdfTörlés.Click += new System.EventHandler(this.PdfTörlés_Click);
            // 
            // FilePDF
            // 
            this.FilePDF.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.FilePDF.FormattingEnabled = true;
            this.FilePDF.ItemHeight = 20;
            this.FilePDF.Location = new System.Drawing.Point(5, 59);
            this.FilePDF.Name = "FilePDF";
            this.FilePDF.Size = new System.Drawing.Size(203, 524);
            this.FilePDF.TabIndex = 209;
            this.FilePDF.SelectedIndexChanged += new System.EventHandler(this.FilePDF_SelectedIndexChanged);
            // 
            // TabPage8
            // 
            this.TabPage8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.TabPage8.Controls.Add(this.CafTábla);
            this.TabPage8.Controls.Add(this.Névtext);
            this.TabPage8.Controls.Add(this.BeosztásText);
            this.TabPage8.Controls.Add(this.Cégtext);
            this.TabPage8.Controls.Add(this.Label22);
            this.TabPage8.Controls.Add(this.Label21);
            this.TabPage8.Controls.Add(this.Label20);
            this.TabPage8.Controls.Add(this.CafTöröl);
            this.TabPage8.Controls.Add(this.Btn_CAF_Új);
            this.TabPage8.Controls.Add(this.CAFRögzít);
            this.TabPage8.Location = new System.Drawing.Point(4, 29);
            this.TabPage8.Name = "TabPage8";
            this.TabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage8.Size = new System.Drawing.Size(1240, 617);
            this.TabPage8.TabIndex = 7;
            this.TabPage8.Text = "CAF";
            // 
            // CafTábla
            // 
            this.CafTábla.AllowUserToAddRows = false;
            this.CafTábla.AllowUserToDeleteRows = false;
            this.CafTábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CafTábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CafTábla.EnableHeadersVisualStyles = false;
            this.CafTábla.Location = new System.Drawing.Point(7, 109);
            this.CafTábla.Name = "CafTábla";
            this.CafTábla.RowHeadersVisible = false;
            this.CafTábla.RowHeadersWidth = 51;
            this.CafTábla.Size = new System.Drawing.Size(1211, 500);
            this.CafTábla.TabIndex = 191;
            this.CafTábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CafTábla_CellClick);
            // 
            // Névtext
            // 
            this.Névtext.Location = new System.Drawing.Point(90, 43);
            this.Névtext.Name = "Névtext";
            this.Névtext.Size = new System.Drawing.Size(252, 26);
            this.Névtext.TabIndex = 1;
            // 
            // BeosztásText
            // 
            this.BeosztásText.Location = new System.Drawing.Point(90, 77);
            this.BeosztásText.Name = "BeosztásText";
            this.BeosztásText.Size = new System.Drawing.Size(252, 26);
            this.BeosztásText.TabIndex = 2;
            // 
            // Cégtext
            // 
            this.Cégtext.Location = new System.Drawing.Point(90, 9);
            this.Cégtext.Name = "Cégtext";
            this.Cégtext.Size = new System.Drawing.Size(252, 26);
            this.Cégtext.TabIndex = 0;
            // 
            // Label22
            // 
            this.Label22.AutoSize = true;
            this.Label22.Location = new System.Drawing.Point(6, 49);
            this.Label22.Name = "Label22";
            this.Label22.Size = new System.Drawing.Size(40, 20);
            this.Label22.TabIndex = 2;
            this.Label22.Text = "Név:";
            // 
            // Label21
            // 
            this.Label21.AutoSize = true;
            this.Label21.Location = new System.Drawing.Point(6, 83);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(55, 20);
            this.Label21.TabIndex = 1;
            this.Label21.Text = "Titulus";
            // 
            // Label20
            // 
            this.Label20.AutoSize = true;
            this.Label20.Location = new System.Drawing.Point(6, 15);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(42, 20);
            this.Label20.TabIndex = 0;
            this.Label20.Text = "Cég:";
            // 
            // CafTöröl
            // 
            this.CafTöröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.CafTöröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CafTöröl.Location = new System.Drawing.Point(421, 6);
            this.CafTöröl.Name = "CafTöröl";
            this.CafTöröl.Size = new System.Drawing.Size(45, 45);
            this.CafTöröl.TabIndex = 4;
            this.toolTip1.SetToolTip(this.CafTöröl, "Törli az adatokat");
            this.CafTöröl.UseVisualStyleBackColor = true;
            this.CafTöröl.Click += new System.EventHandler(this.CafTöröl_Click);
            // 
            // Btn_CAF_Új
            // 
            this.Btn_CAF_Új.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Btn_CAF_Új.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_CAF_Új.Location = new System.Drawing.Point(370, 6);
            this.Btn_CAF_Új.Name = "Btn_CAF_Új";
            this.Btn_CAF_Új.Size = new System.Drawing.Size(45, 45);
            this.Btn_CAF_Új.TabIndex = 5;
            this.toolTip1.SetToolTip(this.Btn_CAF_Új, "Új adatnak előkészíti a beviteli mezőt");
            this.Btn_CAF_Új.UseVisualStyleBackColor = true;
            this.Btn_CAF_Új.Click += new System.EventHandler(this.Btn_CAF_Új_Click);
            // 
            // CAFRögzít
            // 
            this.CAFRögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.CAFRögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CAFRögzít.Location = new System.Drawing.Point(473, 6);
            this.CAFRögzít.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CAFRögzít.Name = "CAFRögzít";
            this.CAFRögzít.Size = new System.Drawing.Size(45, 45);
            this.CAFRögzít.TabIndex = 3;
            this.toolTip1.SetToolTip(this.CAFRögzít, "Rögzíti/módosítja az adatokat");
            this.CAFRögzít.UseVisualStyleBackColor = true;
            this.CAFRögzít.Click += new System.EventHandler(this.CAFRögzít_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // Btn_Súgó
            // 
            this.Btn_Súgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Súgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.Btn_Súgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Súgó.Location = new System.Drawing.Point(1213, 5);
            this.Btn_Súgó.Name = "Btn_Súgó";
            this.Btn_Súgó.Size = new System.Drawing.Size(45, 45);
            this.Btn_Súgó.TabIndex = 65;
            this.toolTip1.SetToolTip(this.Btn_Súgó, "Súgó");
            this.Btn_Súgó.UseVisualStyleBackColor = true;
            this.Btn_Súgó.Click += new System.EventHandler(this.Btn_Súgó_Click);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(380, 15);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(830, 30);
            this.Holtart.TabIndex = 272;
            this.Holtart.Visible = false;
            // 
            // Ablak_sérülés
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(1261, 701);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Lapfülek);
            this.Controls.Add(this.Btn_Súgó);
            this.Controls.Add(this.Panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_sérülés";
            this.Text = "Sérülések nyilvántartása";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_sérülés_FormClosed);
            this.Load += new System.EventHandler(this.Ablak_sérülés_Load);
            this.Shown += new System.EventHandler(this.Ablak_sérülés_Shown);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.Lapfülek.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            this.Panel8.ResumeLayout(false);
            this.Panel8.PerformLayout();
            this.Panel7.ResumeLayout(false);
            this.Panel7.PerformLayout();
            this.Panel6.ResumeLayout(false);
            this.Panel6.PerformLayout();
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.TabPage3.ResumeLayout(false);
            this.TabPage3.PerformLayout();
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla2)).EndInit();
            this.TabPage4.ResumeLayout(false);
            this.TabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla1)).EndInit();
            this.TabPage5.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.Panel5.ResumeLayout(false);
            this.Panel5.PerformLayout();
            this.Panel4.ResumeLayout(false);
            this.Panel4.PerformLayout();
            this.TabPage6.ResumeLayout(false);
            this.TabPage6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KépKeret)).EndInit();
            this.TabPage7.ResumeLayout(false);
            this.TabPage7.PerformLayout();
            this.TabPage8.ResumeLayout(false);
            this.TabPage8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CafTábla)).EndInit();
            this.ResumeLayout(false);

        }
        internal Button Btn_Súgó;
        internal Panel Panel1;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal TabControl Lapfülek;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal TabPage TabPage3;
        internal TabPage TabPage4;
        internal TabPage TabPage5;
        internal TabPage TabPage6;
        internal TabPage TabPage7;
        internal TabPage TabPage8;
        internal ComboBox LekTelephely;
        internal TextBox Lekrendszám;
        internal DateTimePicker LekDátumig;
        internal DateTimePicker LekDátumtól;
        internal Label Label4;
        internal Label Label3;
        internal Label Label2;
        internal Label Label1;
        internal Panel Panel2;
        internal RadioButton LekKész;
        internal RadioButton LekTörölt;
        internal RadioButton LekNyitott;
        internal Label Label5;
        internal RadioButton LekMind;
        internal DataGridView Tábla;
        internal Button LekExcel;
        internal Button LekLekérdezés;
        internal CheckBox Chck_Egyszerüsített;
        internal Button NyomtatványKitöltés;
        internal Button ExcelNullás;
        internal Button CsoportkijelölMind;
        internal Button CsoportVissza;
        internal Button Nullás;
        internal Button ExcelKöltség;
        internal Button KöltLekérdezés;
        internal ComboBox KöltTelephely;
        internal TextBox KöltRendszám;
        internal DateTimePicker KöltDátumig;
        internal DateTimePicker KöltDátumtól;
        internal Label Label6;
        internal Label Label7;
        internal Label Label8;
        internal Label Label9;
        internal Panel Panel3;
        internal RadioButton KöltKész;
        internal RadioButton KöltNyitott;
        internal Label Label10;
        internal RadioButton KöltMind;
        internal DataGridView Tábla2;
        internal Button Btn_SAP_Feltöltés_Excelből;
        internal Button Btn_SAP_Betöltés_Excelbe;
        internal Button RendelésAdatokSzolgáltatás;
        internal Button RendelésAdatokAnyag;
        internal Button RendelésAdatokIdő;
        internal Button SAPBeolvasó;
        internal DataGridView Tábla1;
        internal PictureBox KépKeret;
        internal DateTimePicker FényIdő;
        internal DateTimePicker FényDátum;
        internal Label Label15;
        internal Label Label14;
        internal TextBox FénySorszám;
        internal Label Label12;
        internal TextBox FényPályaszám;
        internal Label Label11;
        internal ListBox FileBox;
        internal DateTimePicker PdfIdő;
        internal DateTimePicker PdfDátum;
        internal Label Label16;
        internal Label Label17;
        internal TextBox PdfSorszám;
        internal Label Label18;
        internal TextBox PdfPályaszám;
        internal Label Label19;
        internal ListBox FilePDF;
        internal TextBox Névtext;
        internal TextBox BeosztásText;
        internal TextBox Cégtext;
        internal Label Label22;
        internal Label Label21;
        internal Label Label20;
        internal Button CAFRögzít;
        internal DataGridView CafTábla;
        internal Button CafTöröl;
        internal Button Btn_CAF_Új;
        internal Panel Panel5;
        internal Panel Panel4;
        internal TextBox Text7;
        internal TextBox Text6;
        internal TextBox Text5;
        internal TextBox Text4;
        internal TextBox Text3;
        internal TextBox Text2;
        internal TextBox Text1;
        internal TextBox Eszköz;
        internal TextBox Telefonszám;
        internal TextBox Kiállította;
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
        internal Button Btn_ÁllandóÉrt_Felépít_Rögzít;
        internal Label Label23;
        internal TextBox Iktatószám;
        internal TextBox ÉvestarifaD03;
        internal Button Btn_ÁllandóÉrt_Tarifa_Rögzít;
        internal Label Label36;
        internal Label Label35;
        internal Label Label34;
        internal TextBox ÉvestarifaD60;
        internal TextBox Forgalmiakadály;
        internal TextBox Járművezető;
        internal Label Label44;
        internal Label Label43;
        internal Label Label42;
        internal Label Label41;
        internal TextBox Telephely;
        internal TextBox Típus;
        internal TextBox Sorszám;
        internal Label Label40;
        internal Label Label39;
        internal DateTimePicker Idő;
        internal DateTimePicker Dátum;
        internal Label Label37;
        internal Label Label38;
        internal TextBox Viszonylat;
        internal TextBox Üzembehelyezés;
        internal TextBox Szerelvény;
        internal TextBox KmóraÁllás;
        internal Label Label48;
        internal Label Label47;
        internal Label Label46;
        internal Label Label45;
        internal Panel Panel6;
        internal RadioButton Opt_Törölt;
        internal RadioButton Opt_Elkészült;
        internal RadioButton Opt_Nyitott;
        internal Label Label49;
        internal Button Btn_Kép_Hozzáad;
        internal Button Btn_PDF_Hozzáad;
        internal Button Visszaállít;
        internal Button Újat;
        internal Button FékvizsgálatiExcel;
        internal Button CAFExcel;
        internal Button Rögzítjelentés;
        internal CheckBox Műszakihiba;
        internal Panel Panel7;
        internal RadioButton Személyhiba;
        internal RadioButton Egyébhiba;
        internal RadioButton Idegenhiba;
        internal RadioButton Sajáthiba;
        internal Label Label52;
        internal TextBox Doksik;
        internal Label Label51;
        internal TextBox Fényképek;
        internal Label Label50;
        internal TextBox Ütközött;
        internal Label Label61;
        internal Label Label60;
        internal Label Label59;
        internal Label Label58;
        internal Label Label57;
        internal TextBox Biztosító;
        internal TextBox Helyszín;
        internal Label Label56;
        internal Panel Panel8;
        internal RadioButton Hosszú;
        internal RadioButton Gyors;
        internal Label Label55;
        internal TextBox Rendelésszám;
        internal TextBox AnyagikárÁr;
        internal Label Label54;
        internal Label Label53;
        internal CheckBox Személyi;
        internal CheckBox Anyagikár;
        internal TextBox Leírás1;
        internal TextBox Esemény;
        internal TextBox Leírás;
        internal TextBox Telephely1;
        internal TextBox Költséghely;
        internal Label Label62;
        internal DateTimePicker Dátum_tarifa;
        internal Button Elkészült;
        internal Button KépTörlés;
        internal Button KépLementés;
        internal Button PdfTörlés;
        internal DateTimePicker SapDátum;
        internal Label Label64;
        internal TextBox SapSorszám;
        internal Label Label65;
        internal TextBox SapPályaszám;
        internal Label Label66;
        internal TextBox SapTelephely;
        internal Label Label67;
        internal TextBox SapRendelés;
        internal Label Label63;
        internal Label Label68;

        //private PdfiumViewer.PdfViewer PDF_néző;
        //private PdfiumViewer.PdfViewer Pdftöltő;
        private ToolTip toolTip1;
        public PdfiumViewer.PdfViewer PDF_néző;
        internal TextBox Pályaszám;
        internal Panel panel9;
        internal TextBox TxtBxDigitalisAlairo1;
        internal TextBox TxtBxBeosztas2;
        internal Label label73;
        internal Label label69;
        internal TextBox TxtBxBeosztas1;
        internal Button Btn_Digitális_Aláírók;
        internal Label label70;
        internal Label label71;
        internal TextBox TxtBxDigitalisAlairo2;
        private CheckBox ChckBxDigitális;
        internal Label label72;
        internal V_MindenEgyéb.MyProgressbar Holtart;
    }
}