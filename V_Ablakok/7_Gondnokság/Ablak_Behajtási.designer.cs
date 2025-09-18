using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
    public partial class Ablak_Behajtási : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Behajtási));
            this.Fülek = new System.Windows.Forms.TabControl();
            this.Engedélyek = new System.Windows.Forms.TabPage();
            this.Nézet_Egyszerű = new System.Windows.Forms.CheckBox();
            this.TxtRendszámszűrő = new System.Windows.Forms.TextBox();
            this.LblEngedélyRendszám = new System.Windows.Forms.Label();
            this.PanelEngedély = new System.Windows.Forms.Panel();
            this.BtnEngedélyListaSzakEmail = new System.Windows.Forms.Button();
            this.BtnEngedélyListaTörlés = new System.Windows.Forms.Button();
            this.BtnEngedélyListaÁtvételMegtörtént = new System.Windows.Forms.Button();
            this.BtnEngedélyListaÁtvételKüld = new System.Windows.Forms.Button();
            this.BtnEngedélyListaÁtvételNyomtat = new System.Windows.Forms.Button();
            this.BtnEngedélyListaGondnokEmail = new System.Windows.Forms.Button();
            this.BtnEngedélyListaEngedélyNyomtat = new System.Windows.Forms.Button();
            this.Txtnévszűrő = new System.Windows.Forms.TextBox();
            this.LblEngedélyDolgozóNév = new System.Windows.Forms.Label();
            this.CmbEngedélylistaszűrő = new System.Windows.Forms.ComboBox();
            this.LblEngedélyEngedélyStátus = new System.Windows.Forms.Label();
            this.BtnExcelkimenet = new System.Windows.Forms.Button();
            this.BtnEngedélyListaFrissít = new System.Windows.Forms.Button();
            this.TáblaLista = new System.Windows.Forms.DataGridView();
            this.Kérelem = new System.Windows.Forms.TabPage();
            this.KérelemTábla = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DatÉrvényes = new System.Windows.Forms.DateTimePicker();
            this.LblKérelemÉrvényességVége = new System.Windows.Forms.Label();
            this.CMBkérelemStátus = new System.Windows.Forms.ComboBox();
            this.LblKérelemEngedélyStátus = new System.Windows.Forms.Label();
            this.CmbKérelemTípus = new System.Windows.Forms.ComboBox();
            this.LblKérelemJogosultságTípus = new System.Windows.Forms.Label();
            this.TxtKérelemMegjegyzés = new System.Windows.Forms.TextBox();
            this.LblKérelemMegjegyzés = new System.Windows.Forms.Label();
            this.TxtKérelemautó = new System.Windows.Forms.TextBox();
            this.LblKérelemAutókSzáma = new System.Windows.Forms.Label();
            this.KérelemDátuma = new System.Windows.Forms.DateTimePicker();
            this.LblKérelemIgénylésDátum = new System.Windows.Forms.Label();
            this.TxtKérrelemPDF = new System.Windows.Forms.TextBox();
            this.LblKérelemPDFneve = new System.Windows.Forms.Label();
            this.TxtKérelemID = new System.Windows.Forms.TextBox();
            this.LblKérelemEngedélySzám = new System.Windows.Forms.Label();
            this.CmbkérelemOka = new System.Windows.Forms.ComboBox();
            this.LblKérelemKérelemOka = new System.Windows.Forms.Label();
            this.CmbKérelemSzolgálati = new System.Windows.Forms.ComboBox();
            this.LblKérelemDolgozóSzolgálatiHely = new System.Windows.Forms.Label();
            this.TxtKérelemFrsz = new System.Windows.Forms.TextBox();
            this.Txtkérelemnév = new System.Windows.Forms.TextBox();
            this.LblKérelemRendszám = new System.Windows.Forms.Label();
            this.TxtkérelemHR = new System.Windows.Forms.TextBox();
            this.LblKérelemDolgozóNév = new System.Windows.Forms.Label();
            this.LblKérelemDolgozóHR = new System.Windows.Forms.Label();
            this.Btnkilelöltörlés = new System.Windows.Forms.Button();
            this.BtnKijelölcsop = new System.Windows.Forms.Button();
            this.Btn3szak = new System.Windows.Forms.Button();
            this.Btn2szak = new System.Windows.Forms.Button();
            this.Btn1szak = new System.Windows.Forms.Button();
            this.BtnKérelemPDF = new System.Windows.Forms.Button();
            this.BtnÖsszSzabiLista = new System.Windows.Forms.Button();
            this.BtnOktatásÚj = new System.Windows.Forms.Button();
            this.BtnkérelemRögzítés = new System.Windows.Forms.Button();
            this.PDF = new System.Windows.Forms.TabPage();
            this.PDF_néző = new PdfiumViewer.PdfViewer();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.BtnGondnokSave = new System.Windows.Forms.Button();
            this.LblGondnokEngedélyezés = new System.Windows.Forms.Label();
            this.LblGondnokIndoklás = new System.Windows.Forms.Label();
            this.TxtGondnokMegjegyzés = new System.Windows.Forms.TextBox();
            this.CmbGondnokEngedély = new System.Windows.Forms.ComboBox();
            this.Táblagondnok = new System.Windows.Forms.DataGridView();
            this.BtnGondnokFrissít = new System.Windows.Forms.Button();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.Elutasít_gomb = new System.Windows.Forms.Button();
            this.LblSzakszGondnokiFelülbírálás = new System.Windows.Forms.Label();
            this.LblSzakszEngedély = new System.Windows.Forms.Label();
            this.CmbSzakszlista = new System.Windows.Forms.ComboBox();
            this.Táblaszaksz = new System.Windows.Forms.DataGridView();
            this.BtnEngedélySzakBírál = new System.Windows.Forms.Button();
            this.BtnSzakszeng = new System.Windows.Forms.Button();
            this.BtnEngedélySzakFrissít = new System.Windows.Forms.Button();
            this.Adminisztátor = new System.Windows.Forms.TabPage();
            this.PanelAdminAlap = new System.Windows.Forms.Panel();
            this.BtnAdminÚjEngedély = new System.Windows.Forms.Button();
            this.DataAdminAlap = new System.Windows.Forms.DataGridView();
            this.LblAdminAktuálisAB = new System.Windows.Forms.Label();
            this.BtnAdminRögz = new System.Windows.Forms.Button();
            this.DatadminÉrvényes = new System.Windows.Forms.DateTimePicker();
            this.LblAdminABNév = new System.Windows.Forms.Label();
            this.LblAdminÉrvényesség = new System.Windows.Forms.Label();
            this.TxtAmindFájl = new System.Windows.Forms.TextBox();
            this.TxtAdminaktuális = new System.Windows.Forms.TextBox();
            this.LblAdminSorszámBetűjel = new System.Windows.Forms.Label();
            this.TxtadminBetű = new System.Windows.Forms.TextBox();
            this.TxtAdminkönyvtár = new System.Windows.Forms.TextBox();
            this.LblAdminSorszKezdete = new System.Windows.Forms.Label();
            this.LblAdminKönyvtár = new System.Windows.Forms.Label();
            this.TxtAdminSorszám = new System.Windows.Forms.TextBox();
            this.PanelAdminKérelemOka = new System.Windows.Forms.Panel();
            this.BtnAdminOkfel = new System.Windows.Forms.Button();
            this.TxtAdminOk = new System.Windows.Forms.TextBox();
            this.LstAdminokok = new System.Windows.Forms.ListBox();
            this.LblAdminKérelemOka = new System.Windows.Forms.Label();
            this.BtnAdminOkTöröl = new System.Windows.Forms.Button();
            this.BtnAdminOkrögzítés = new System.Windows.Forms.Button();
            this.BtnDolgozóilsta = new System.Windows.Forms.Button();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.BtnNaplóExcel = new System.Windows.Forms.Button();
            this.TextNaplósorszám = new System.Windows.Forms.TextBox();
            this.LblNaplóEngedélySorsz = new System.Windows.Forms.Label();
            this.DataNapló = new System.Windows.Forms.DataGridView();
            this.BtnNaplóLista = new System.Windows.Forms.Button();
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.PanelTelephely = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.LblTelephelyBeállítás = new System.Windows.Forms.Label();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.Aktuálissor = new System.Windows.Forms.CheckBox();
            this.Fülek.SuspendLayout();
            this.Engedélyek.SuspendLayout();
            this.PanelEngedély.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TáblaLista)).BeginInit();
            this.Kérelem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KérelemTábla)).BeginInit();
            this.PDF.SuspendLayout();
            this.TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Táblagondnok)).BeginInit();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Táblaszaksz)).BeginInit();
            this.Adminisztátor.SuspendLayout();
            this.PanelAdminAlap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataAdminAlap)).BeginInit();
            this.PanelAdminKérelemOka.SuspendLayout();
            this.TabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataNapló)).BeginInit();
            this.PanelTelephely.SuspendLayout();
            this.SuspendLayout();
            // 
            // Fülek
            // 
            this.Fülek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fülek.Controls.Add(this.Engedélyek);
            this.Fülek.Controls.Add(this.Kérelem);
            this.Fülek.Controls.Add(this.PDF);
            this.Fülek.Controls.Add(this.TabPage1);
            this.Fülek.Controls.Add(this.TabPage2);
            this.Fülek.Controls.Add(this.Adminisztátor);
            this.Fülek.Controls.Add(this.TabPage3);
            this.Fülek.Location = new System.Drawing.Point(4, 53);
            this.Fülek.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Fülek.Name = "Fülek";
            this.Fülek.Padding = new System.Drawing.Point(16, 3);
            this.Fülek.SelectedIndex = 0;
            this.Fülek.Size = new System.Drawing.Size(1271, 590);
            this.Fülek.TabIndex = 0;
            this.Fülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Fülek_DrawItem);
            this.Fülek.SelectedIndexChanged += new System.EventHandler(this.Fülek_SelectedIndexChanged);
            // 
            // Engedélyek
            // 
            this.Engedélyek.BackColor = System.Drawing.Color.LightSalmon;
            this.Engedélyek.Controls.Add(this.Nézet_Egyszerű);
            this.Engedélyek.Controls.Add(this.TxtRendszámszűrő);
            this.Engedélyek.Controls.Add(this.LblEngedélyRendszám);
            this.Engedélyek.Controls.Add(this.PanelEngedély);
            this.Engedélyek.Controls.Add(this.Txtnévszűrő);
            this.Engedélyek.Controls.Add(this.LblEngedélyDolgozóNév);
            this.Engedélyek.Controls.Add(this.CmbEngedélylistaszűrő);
            this.Engedélyek.Controls.Add(this.LblEngedélyEngedélyStátus);
            this.Engedélyek.Controls.Add(this.BtnExcelkimenet);
            this.Engedélyek.Controls.Add(this.BtnEngedélyListaFrissít);
            this.Engedélyek.Controls.Add(this.TáblaLista);
            this.Engedélyek.Location = new System.Drawing.Point(4, 29);
            this.Engedélyek.Name = "Engedélyek";
            this.Engedélyek.Size = new System.Drawing.Size(1263, 557);
            this.Engedélyek.TabIndex = 6;
            this.Engedélyek.Text = "Engedélyek listája";
            // 
            // Nézet_Egyszerű
            // 
            this.Nézet_Egyszerű.AutoSize = true;
            this.Nézet_Egyszerű.Checked = true;
            this.Nézet_Egyszerű.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Nézet_Egyszerű.Location = new System.Drawing.Point(639, 9);
            this.Nézet_Egyszerű.Name = "Nézet_Egyszerű";
            this.Nézet_Egyszerű.Size = new System.Drawing.Size(126, 24);
            this.Nézet_Egyszerű.TabIndex = 117;
            this.Nézet_Egyszerű.Text = "Egyszerű lista";
            this.Nézet_Egyszerű.UseVisualStyleBackColor = true;
            // 
            // TxtRendszámszűrő
            // 
            this.TxtRendszámszűrő.Location = new System.Drawing.Point(520, 35);
            this.TxtRendszámszűrő.Name = "TxtRendszámszűrő";
            this.TxtRendszámszűrő.Size = new System.Drawing.Size(115, 26);
            this.TxtRendszámszűrő.TabIndex = 116;
            // 
            // LblEngedélyRendszám
            // 
            this.LblEngedélyRendszám.AutoSize = true;
            this.LblEngedélyRendszám.Location = new System.Drawing.Point(520, 5);
            this.LblEngedélyRendszám.Name = "LblEngedélyRendszám";
            this.LblEngedélyRendszám.Size = new System.Drawing.Size(90, 20);
            this.LblEngedélyRendszám.TabIndex = 115;
            this.LblEngedélyRendszám.Text = "Rendszám:";
            // 
            // PanelEngedély
            // 
            this.PanelEngedély.Controls.Add(this.BtnEngedélyListaSzakEmail);
            this.PanelEngedély.Controls.Add(this.BtnEngedélyListaTörlés);
            this.PanelEngedély.Controls.Add(this.BtnEngedélyListaÁtvételMegtörtént);
            this.PanelEngedély.Controls.Add(this.BtnEngedélyListaÁtvételKüld);
            this.PanelEngedély.Controls.Add(this.BtnEngedélyListaÁtvételNyomtat);
            this.PanelEngedély.Controls.Add(this.BtnEngedélyListaGondnokEmail);
            this.PanelEngedély.Controls.Add(this.BtnEngedélyListaEngedélyNyomtat);
            this.PanelEngedély.Location = new System.Drawing.Point(873, 9);
            this.PanelEngedély.Name = "PanelEngedély";
            this.PanelEngedély.Size = new System.Drawing.Size(362, 58);
            this.PanelEngedély.TabIndex = 56;
            // 
            // BtnEngedélyListaSzakEmail
            // 
            this.BtnEngedélyListaSzakEmail.BackColor = System.Drawing.Color.Fuchsia;
            this.BtnEngedélyListaSzakEmail.BackgroundImage = global::Villamos.Properties.Resources.email;
            this.BtnEngedélyListaSzakEmail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnEngedélyListaSzakEmail.Location = new System.Drawing.Point(54, 5);
            this.BtnEngedélyListaSzakEmail.Name = "BtnEngedélyListaSzakEmail";
            this.BtnEngedélyListaSzakEmail.Size = new System.Drawing.Size(45, 45);
            this.BtnEngedélyListaSzakEmail.TabIndex = 121;
            this.ToolTip1.SetToolTip(this.BtnEngedélyListaSzakEmail, "Elküldi e-mailben Szakszolgálat- vezetőnek, hogy kinek milyen engedélyezési felad" +
        "ata van");
            this.BtnEngedélyListaSzakEmail.UseVisualStyleBackColor = false;
            this.BtnEngedélyListaSzakEmail.Click += new System.EventHandler(this.BtnEngedélyListaSzakEmail_Click);
            // 
            // BtnEngedélyListaTörlés
            // 
            this.BtnEngedélyListaTörlés.BackColor = System.Drawing.Color.LightSeaGreen;
            this.BtnEngedélyListaTörlés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.BtnEngedélyListaTörlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnEngedélyListaTörlés.Location = new System.Drawing.Point(309, 5);
            this.BtnEngedélyListaTörlés.Name = "BtnEngedélyListaTörlés";
            this.BtnEngedélyListaTörlés.Size = new System.Drawing.Size(45, 45);
            this.BtnEngedélyListaTörlés.TabIndex = 120;
            this.ToolTip1.SetToolTip(this.BtnEngedélyListaTörlés, "Engedély törlése");
            this.BtnEngedélyListaTörlés.UseVisualStyleBackColor = false;
            this.BtnEngedélyListaTörlés.Click += new System.EventHandler(this.BtnEngedélyListaTörlés_Click);
            // 
            // BtnEngedélyListaÁtvételMegtörtént
            // 
            this.BtnEngedélyListaÁtvételMegtörtént.BackColor = System.Drawing.Color.LightSeaGreen;
            this.BtnEngedélyListaÁtvételMegtörtént.BackgroundImage = global::Villamos.Properties.Resources.mail_accept_32;
            this.BtnEngedélyListaÁtvételMegtörtént.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnEngedélyListaÁtvételMegtörtént.Location = new System.Drawing.Point(258, 5);
            this.BtnEngedélyListaÁtvételMegtörtént.Name = "BtnEngedélyListaÁtvételMegtörtént";
            this.BtnEngedélyListaÁtvételMegtörtént.Size = new System.Drawing.Size(45, 45);
            this.BtnEngedélyListaÁtvételMegtörtént.TabIndex = 119;
            this.ToolTip1.SetToolTip(this.BtnEngedélyListaÁtvételMegtörtént, "Átvétel megtörtént");
            this.BtnEngedélyListaÁtvételMegtörtént.UseVisualStyleBackColor = false;
            this.BtnEngedélyListaÁtvételMegtörtént.Click += new System.EventHandler(this.BtnEngedélyListaÁtvételMegtörtént_Click);
            // 
            // BtnEngedélyListaÁtvételKüld
            // 
            this.BtnEngedélyListaÁtvételKüld.BackColor = System.Drawing.Color.LightSeaGreen;
            this.BtnEngedélyListaÁtvételKüld.BackgroundImage = global::Villamos.Properties.Resources.mail_next_32;
            this.BtnEngedélyListaÁtvételKüld.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnEngedélyListaÁtvételKüld.Location = new System.Drawing.Point(207, 5);
            this.BtnEngedélyListaÁtvételKüld.Name = "BtnEngedélyListaÁtvételKüld";
            this.BtnEngedélyListaÁtvételKüld.Size = new System.Drawing.Size(45, 45);
            this.BtnEngedélyListaÁtvételKüld.TabIndex = 118;
            this.ToolTip1.SetToolTip(this.BtnEngedélyListaÁtvételKüld, "Átvételre elküldve");
            this.BtnEngedélyListaÁtvételKüld.UseVisualStyleBackColor = false;
            this.BtnEngedélyListaÁtvételKüld.Click += new System.EventHandler(this.BtnEngedélyListaÁtvételKüld_Click);
            // 
            // BtnEngedélyListaÁtvételNyomtat
            // 
            this.BtnEngedélyListaÁtvételNyomtat.BackColor = System.Drawing.Color.LightSeaGreen;
            this.BtnEngedélyListaÁtvételNyomtat.BackgroundImage = global::Villamos.Properties.Resources.Document_write;
            this.BtnEngedélyListaÁtvételNyomtat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnEngedélyListaÁtvételNyomtat.Location = new System.Drawing.Point(156, 5);
            this.BtnEngedélyListaÁtvételNyomtat.Name = "BtnEngedélyListaÁtvételNyomtat";
            this.BtnEngedélyListaÁtvételNyomtat.Size = new System.Drawing.Size(45, 45);
            this.BtnEngedélyListaÁtvételNyomtat.TabIndex = 117;
            this.ToolTip1.SetToolTip(this.BtnEngedélyListaÁtvételNyomtat, "Átvételi elismervény nyomtatása");
            this.BtnEngedélyListaÁtvételNyomtat.UseVisualStyleBackColor = false;
            this.BtnEngedélyListaÁtvételNyomtat.Click += new System.EventHandler(this.BtnEngedélyListaÁtvételNyomtat_Click);
            // 
            // BtnEngedélyListaGondnokEmail
            // 
            this.BtnEngedélyListaGondnokEmail.BackColor = System.Drawing.Color.LightSeaGreen;
            this.BtnEngedélyListaGondnokEmail.BackgroundImage = global::Villamos.Properties.Resources.email;
            this.BtnEngedélyListaGondnokEmail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnEngedélyListaGondnokEmail.Location = new System.Drawing.Point(3, 5);
            this.BtnEngedélyListaGondnokEmail.Name = "BtnEngedélyListaGondnokEmail";
            this.BtnEngedélyListaGondnokEmail.Size = new System.Drawing.Size(45, 45);
            this.BtnEngedélyListaGondnokEmail.TabIndex = 116;
            this.ToolTip1.SetToolTip(this.BtnEngedélyListaGondnokEmail, "Elküldi e-mailben a Gondnokoknak, hogy kinek milyen engedélyezési feladata van");
            this.BtnEngedélyListaGondnokEmail.UseVisualStyleBackColor = false;
            this.BtnEngedélyListaGondnokEmail.Click += new System.EventHandler(this.BtnEngedélyListaGondnokEmail_Click);
            // 
            // BtnEngedélyListaEngedélyNyomtat
            // 
            this.BtnEngedélyListaEngedélyNyomtat.BackColor = System.Drawing.Color.LightSeaGreen;
            this.BtnEngedélyListaEngedélyNyomtat.BackgroundImage = global::Villamos.Properties.Resources.Yellow_Glass_Folders_Icon_28;
            this.BtnEngedélyListaEngedélyNyomtat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnEngedélyListaEngedélyNyomtat.Location = new System.Drawing.Point(105, 5);
            this.BtnEngedélyListaEngedélyNyomtat.Name = "BtnEngedélyListaEngedélyNyomtat";
            this.BtnEngedélyListaEngedélyNyomtat.Size = new System.Drawing.Size(45, 45);
            this.BtnEngedélyListaEngedélyNyomtat.TabIndex = 115;
            this.ToolTip1.SetToolTip(this.BtnEngedélyListaEngedélyNyomtat, "Engedélyek nyomtatása");
            this.BtnEngedélyListaEngedélyNyomtat.UseVisualStyleBackColor = false;
            this.BtnEngedélyListaEngedélyNyomtat.Click += new System.EventHandler(this.BtnEngedélyListaEngedélyNyomtat_Click);
            // 
            // Txtnévszűrő
            // 
            this.Txtnévszűrő.Location = new System.Drawing.Point(235, 35);
            this.Txtnévszűrő.Name = "Txtnévszűrő";
            this.Txtnévszűrő.Size = new System.Drawing.Size(281, 26);
            this.Txtnévszűrő.TabIndex = 114;
            // 
            // LblEngedélyDolgozóNév
            // 
            this.LblEngedélyDolgozóNév.AutoSize = true;
            this.LblEngedélyDolgozóNév.Location = new System.Drawing.Point(235, 5);
            this.LblEngedélyDolgozóNév.Name = "LblEngedélyDolgozóNév";
            this.LblEngedélyDolgozóNév.Size = new System.Drawing.Size(110, 20);
            this.LblEngedélyDolgozóNév.TabIndex = 113;
            this.LblEngedélyDolgozóNév.Text = "Dolgozó neve:";
            // 
            // CmbEngedélylistaszűrő
            // 
            this.CmbEngedélylistaszűrő.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbEngedélylistaszűrő.FormattingEnabled = true;
            this.CmbEngedélylistaszűrő.Location = new System.Drawing.Point(5, 35);
            this.CmbEngedélylistaszűrő.Name = "CmbEngedélylistaszűrő";
            this.CmbEngedélylistaszűrő.Size = new System.Drawing.Size(223, 28);
            this.CmbEngedélylistaszűrő.TabIndex = 112;
            this.CmbEngedélylistaszűrő.SelectedIndexChanged += new System.EventHandler(this.CmbEngedélylistaszűrő_SelectedIndexChanged);
            // 
            // LblEngedélyEngedélyStátus
            // 
            this.LblEngedélyEngedélyStátus.AutoSize = true;
            this.LblEngedélyEngedélyStátus.Location = new System.Drawing.Point(5, 5);
            this.LblEngedélyEngedélyStátus.Name = "LblEngedélyEngedélyStátus";
            this.LblEngedélyEngedélyStátus.Size = new System.Drawing.Size(139, 20);
            this.LblEngedélyEngedélyStátus.TabIndex = 111;
            this.LblEngedélyEngedélyStátus.Text = "Engedély Státusa:";
            // 
            // BtnExcelkimenet
            // 
            this.BtnExcelkimenet.BackColor = System.Drawing.Color.LightSeaGreen;
            this.BtnExcelkimenet.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.BtnExcelkimenet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnExcelkimenet.Location = new System.Drawing.Point(771, 16);
            this.BtnExcelkimenet.Name = "BtnExcelkimenet";
            this.BtnExcelkimenet.Size = new System.Drawing.Size(45, 45);
            this.BtnExcelkimenet.TabIndex = 110;
            this.ToolTip1.SetToolTip(this.BtnExcelkimenet, "A táblázat tartalmát Excelbe menti ki.");
            this.BtnExcelkimenet.UseVisualStyleBackColor = false;
            this.BtnExcelkimenet.Click += new System.EventHandler(this.BtnExcelkimenet_Click);
            // 
            // BtnEngedélyListaFrissít
            // 
            this.BtnEngedélyListaFrissít.BackColor = System.Drawing.Color.LightSeaGreen;
            this.BtnEngedélyListaFrissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.BtnEngedélyListaFrissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnEngedélyListaFrissít.Location = new System.Drawing.Point(822, 16);
            this.BtnEngedélyListaFrissít.Name = "BtnEngedélyListaFrissít";
            this.BtnEngedélyListaFrissít.Size = new System.Drawing.Size(45, 45);
            this.BtnEngedélyListaFrissít.TabIndex = 109;
            this.ToolTip1.SetToolTip(this.BtnEngedélyListaFrissít, "Frissíti a táblázatot");
            this.BtnEngedélyListaFrissít.UseVisualStyleBackColor = false;
            this.BtnEngedélyListaFrissít.Click += new System.EventHandler(this.BtnEngedélyListaFrissít_Click);
            // 
            // TáblaLista
            // 
            this.TáblaLista.AllowUserToAddRows = false;
            this.TáblaLista.AllowUserToDeleteRows = false;
            this.TáblaLista.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TáblaLista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TáblaLista.Location = new System.Drawing.Point(5, 69);
            this.TáblaLista.Name = "TáblaLista";
            this.TáblaLista.RowHeadersWidth = 20;
            this.TáblaLista.Size = new System.Drawing.Size(1255, 484);
            this.TáblaLista.TabIndex = 0;
            this.TáblaLista.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TáblaLista_CellClick);
            this.TáblaLista.SelectionChanged += new System.EventHandler(this.TáblaLista_SelectionChanged);
            // 
            // Kérelem
            // 
            this.Kérelem.BackColor = System.Drawing.Color.LightSalmon;
            this.Kérelem.Controls.Add(this.KérelemTábla);
            this.Kérelem.Controls.Add(this.DatÉrvényes);
            this.Kérelem.Controls.Add(this.LblKérelemÉrvényességVége);
            this.Kérelem.Controls.Add(this.CMBkérelemStátus);
            this.Kérelem.Controls.Add(this.LblKérelemEngedélyStátus);
            this.Kérelem.Controls.Add(this.CmbKérelemTípus);
            this.Kérelem.Controls.Add(this.LblKérelemJogosultságTípus);
            this.Kérelem.Controls.Add(this.TxtKérelemMegjegyzés);
            this.Kérelem.Controls.Add(this.LblKérelemMegjegyzés);
            this.Kérelem.Controls.Add(this.TxtKérelemautó);
            this.Kérelem.Controls.Add(this.LblKérelemAutókSzáma);
            this.Kérelem.Controls.Add(this.KérelemDátuma);
            this.Kérelem.Controls.Add(this.LblKérelemIgénylésDátum);
            this.Kérelem.Controls.Add(this.TxtKérrelemPDF);
            this.Kérelem.Controls.Add(this.LblKérelemPDFneve);
            this.Kérelem.Controls.Add(this.TxtKérelemID);
            this.Kérelem.Controls.Add(this.LblKérelemEngedélySzám);
            this.Kérelem.Controls.Add(this.CmbkérelemOka);
            this.Kérelem.Controls.Add(this.LblKérelemKérelemOka);
            this.Kérelem.Controls.Add(this.CmbKérelemSzolgálati);
            this.Kérelem.Controls.Add(this.LblKérelemDolgozóSzolgálatiHely);
            this.Kérelem.Controls.Add(this.TxtKérelemFrsz);
            this.Kérelem.Controls.Add(this.Txtkérelemnév);
            this.Kérelem.Controls.Add(this.LblKérelemRendszám);
            this.Kérelem.Controls.Add(this.TxtkérelemHR);
            this.Kérelem.Controls.Add(this.LblKérelemDolgozóNév);
            this.Kérelem.Controls.Add(this.LblKérelemDolgozóHR);
            this.Kérelem.Controls.Add(this.Btnkilelöltörlés);
            this.Kérelem.Controls.Add(this.BtnKijelölcsop);
            this.Kérelem.Controls.Add(this.Btn3szak);
            this.Kérelem.Controls.Add(this.Btn2szak);
            this.Kérelem.Controls.Add(this.Btn1szak);
            this.Kérelem.Controls.Add(this.BtnKérelemPDF);
            this.Kérelem.Controls.Add(this.BtnÖsszSzabiLista);
            this.Kérelem.Controls.Add(this.BtnOktatásÚj);
            this.Kérelem.Controls.Add(this.BtnkérelemRögzítés);
            this.Kérelem.Location = new System.Drawing.Point(4, 29);
            this.Kérelem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Kérelem.Name = "Kérelem";
            this.Kérelem.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Kérelem.Size = new System.Drawing.Size(1263, 557);
            this.Kérelem.TabIndex = 1;
            this.Kérelem.Text = "Kérelem";
            // 
            // KérelemTábla
            // 
            this.KérelemTábla.AllowDrop = true;
            this.KérelemTábla.AllowUserToAddRows = false;
            this.KérelemTábla.AllowUserToDeleteRows = false;
            this.KérelemTábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.KérelemTábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.KérelemTábla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.KérelemTábla.Location = new System.Drawing.Point(3, 401);
            this.KérelemTábla.Name = "KérelemTábla";
            this.KérelemTábla.RowHeadersVisible = false;
            this.KérelemTábla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.KérelemTábla.Size = new System.Drawing.Size(1253, 153);
            this.KérelemTábla.TabIndex = 211;
            this.KérelemTábla.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.KérelemTábla_CellValueChanged);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Telephely";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Állapota";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Állapota";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Megjegyzés";
            this.Column5.Name = "Column5";
            // 
            // DatÉrvényes
            // 
            this.DatÉrvényes.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DatÉrvényes.Location = new System.Drawing.Point(199, 373);
            this.DatÉrvényes.Name = "DatÉrvényes";
            this.DatÉrvényes.Size = new System.Drawing.Size(127, 26);
            this.DatÉrvényes.TabIndex = 111;
            // 
            // LblKérelemÉrvényességVége
            // 
            this.LblKérelemÉrvényességVége.AutoSize = true;
            this.LblKérelemÉrvényességVége.Location = new System.Drawing.Point(7, 378);
            this.LblKérelemÉrvényességVége.Name = "LblKérelemÉrvényességVége";
            this.LblKérelemÉrvényességVége.Size = new System.Drawing.Size(142, 20);
            this.LblKérelemÉrvényességVége.TabIndex = 110;
            this.LblKérelemÉrvényességVége.Text = "Érvényesség vége:";
            // 
            // CMBkérelemStátus
            // 
            this.CMBkérelemStátus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CMBkérelemStátus.FormattingEnabled = true;
            this.CMBkérelemStátus.Location = new System.Drawing.Point(926, 10);
            this.CMBkérelemStátus.Name = "CMBkérelemStátus";
            this.CMBkérelemStátus.Size = new System.Drawing.Size(223, 28);
            this.CMBkérelemStátus.TabIndex = 109;
            // 
            // LblKérelemEngedélyStátus
            // 
            this.LblKérelemEngedélyStátus.AutoSize = true;
            this.LblKérelemEngedélyStátus.Location = new System.Drawing.Point(781, 18);
            this.LblKérelemEngedélyStátus.Name = "LblKérelemEngedélyStátus";
            this.LblKérelemEngedélyStátus.Size = new System.Drawing.Size(139, 20);
            this.LblKérelemEngedélyStátus.TabIndex = 108;
            this.LblKérelemEngedélyStátus.Text = "Engedély Státusa:";
            // 
            // CmbKérelemTípus
            // 
            this.CmbKérelemTípus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbKérelemTípus.FormattingEnabled = true;
            this.CmbKérelemTípus.Location = new System.Drawing.Point(199, 209);
            this.CmbKérelemTípus.Name = "CmbKérelemTípus";
            this.CmbKérelemTípus.Size = new System.Drawing.Size(246, 28);
            this.CmbKérelemTípus.TabIndex = 107;
            // 
            // LblKérelemJogosultságTípus
            // 
            this.LblKérelemJogosultságTípus.AutoSize = true;
            this.LblKérelemJogosultságTípus.Location = new System.Drawing.Point(7, 217);
            this.LblKérelemJogosultságTípus.Name = "LblKérelemJogosultságTípus";
            this.LblKérelemJogosultságTípus.Size = new System.Drawing.Size(146, 20);
            this.LblKérelemJogosultságTípus.TabIndex = 106;
            this.LblKérelemJogosultságTípus.Text = "Jogosultság típusa:";
            // 
            // TxtKérelemMegjegyzés
            // 
            this.TxtKérelemMegjegyzés.Location = new System.Drawing.Point(199, 310);
            this.TxtKérelemMegjegyzés.Multiline = true;
            this.TxtKérelemMegjegyzés.Name = "TxtKérelemMegjegyzés";
            this.TxtKérelemMegjegyzés.Size = new System.Drawing.Size(700, 57);
            this.TxtKérelemMegjegyzés.TabIndex = 105;
            // 
            // LblKérelemMegjegyzés
            // 
            this.LblKérelemMegjegyzés.AutoSize = true;
            this.LblKérelemMegjegyzés.Location = new System.Drawing.Point(7, 328);
            this.LblKérelemMegjegyzés.Name = "LblKérelemMegjegyzés";
            this.LblKérelemMegjegyzés.Size = new System.Drawing.Size(97, 20);
            this.LblKérelemMegjegyzés.TabIndex = 104;
            this.LblKérelemMegjegyzés.Text = "Megjegyzés:";
            // 
            // TxtKérelemautó
            // 
            this.TxtKérelemautó.Location = new System.Drawing.Point(510, 177);
            this.TxtKérelemautó.Name = "TxtKérelemautó";
            this.TxtKérelemautó.Size = new System.Drawing.Size(80, 26);
            this.TxtKérelemautó.TabIndex = 98;
            // 
            // LblKérelemAutókSzáma
            // 
            this.LblKérelemAutókSzáma.AutoSize = true;
            this.LblKérelemAutókSzáma.Location = new System.Drawing.Point(386, 180);
            this.LblKérelemAutókSzáma.Name = "LblKérelemAutókSzáma";
            this.LblKérelemAutókSzáma.Size = new System.Drawing.Size(106, 20);
            this.LblKérelemAutókSzáma.TabIndex = 97;
            this.LblKérelemAutókSzáma.Text = "Autók száma:";
            // 
            // KérelemDátuma
            // 
            this.KérelemDátuma.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.KérelemDátuma.Location = new System.Drawing.Point(199, 141);
            this.KérelemDátuma.Name = "KérelemDátuma";
            this.KérelemDátuma.Size = new System.Drawing.Size(127, 26);
            this.KérelemDátuma.TabIndex = 84;
            // 
            // LblKérelemIgénylésDátum
            // 
            this.LblKérelemIgénylésDátum.AutoSize = true;
            this.LblKérelemIgénylésDátum.Location = new System.Drawing.Point(7, 147);
            this.LblKérelemIgénylésDátum.Name = "LblKérelemIgénylésDátum";
            this.LblKérelemIgénylésDátum.Size = new System.Drawing.Size(130, 20);
            this.LblKérelemIgénylésDátum.TabIndex = 83;
            this.LblKérelemIgénylésDátum.Text = "Igénylés dátuma:";
            // 
            // TxtKérrelemPDF
            // 
            this.TxtKérrelemPDF.Location = new System.Drawing.Point(199, 278);
            this.TxtKérrelemPDF.Name = "TxtKérrelemPDF";
            this.TxtKérrelemPDF.Size = new System.Drawing.Size(619, 26);
            this.TxtKérrelemPDF.TabIndex = 81;
            // 
            // LblKérelemPDFneve
            // 
            this.LblKérelemPDFneve.AutoSize = true;
            this.LblKérelemPDFneve.Location = new System.Drawing.Point(7, 284);
            this.LblKérelemPDFneve.Name = "LblKérelemPDFneve";
            this.LblKérelemPDFneve.Size = new System.Drawing.Size(99, 20);
            this.LblKérelemPDFneve.TabIndex = 80;
            this.LblKérelemPDFneve.Text = "Pdf fájl neve:";
            // 
            // TxtKérelemID
            // 
            this.TxtKérelemID.Enabled = false;
            this.TxtKérelemID.Location = new System.Drawing.Point(199, 12);
            this.TxtKérelemID.Name = "TxtKérelemID";
            this.TxtKérelemID.Size = new System.Drawing.Size(173, 26);
            this.TxtKérelemID.TabIndex = 77;
            // 
            // LblKérelemEngedélySzám
            // 
            this.LblKérelemEngedélySzám.AutoSize = true;
            this.LblKérelemEngedélySzám.Location = new System.Drawing.Point(7, 18);
            this.LblKérelemEngedélySzám.Name = "LblKérelemEngedélySzám";
            this.LblKérelemEngedélySzám.Size = new System.Drawing.Size(130, 20);
            this.LblKérelemEngedélySzám.TabIndex = 76;
            this.LblKérelemEngedélySzám.Text = "Engedély száma:";
            // 
            // CmbkérelemOka
            // 
            this.CmbkérelemOka.FormattingEnabled = true;
            this.CmbkérelemOka.Location = new System.Drawing.Point(199, 244);
            this.CmbkérelemOka.Name = "CmbkérelemOka";
            this.CmbkérelemOka.Size = new System.Drawing.Size(526, 28);
            this.CmbkérelemOka.TabIndex = 21;
            // 
            // LblKérelemKérelemOka
            // 
            this.LblKérelemKérelemOka.AutoSize = true;
            this.LblKérelemKérelemOka.Location = new System.Drawing.Point(7, 252);
            this.LblKérelemKérelemOka.Name = "LblKérelemKérelemOka";
            this.LblKérelemKérelemOka.Size = new System.Drawing.Size(101, 20);
            this.LblKérelemKérelemOka.TabIndex = 20;
            this.LblKérelemKérelemOka.Text = "Kérelem oka:";
            // 
            // CmbKérelemSzolgálati
            // 
            this.CmbKérelemSzolgálati.FormattingEnabled = true;
            this.CmbKérelemSzolgálati.Location = new System.Drawing.Point(199, 107);
            this.CmbKérelemSzolgálati.Name = "CmbKérelemSzolgálati";
            this.CmbKérelemSzolgálati.Size = new System.Drawing.Size(526, 28);
            this.CmbKérelemSzolgálati.TabIndex = 19;
            // 
            // LblKérelemDolgozóSzolgálatiHely
            // 
            this.LblKérelemDolgozóSzolgálatiHely.AutoSize = true;
            this.LblKérelemDolgozóSzolgálatiHely.Location = new System.Drawing.Point(7, 115);
            this.LblKérelemDolgozóSzolgálatiHely.Name = "LblKérelemDolgozóSzolgálatiHely";
            this.LblKérelemDolgozóSzolgálatiHely.Size = new System.Drawing.Size(186, 20);
            this.LblKérelemDolgozóSzolgálatiHely.TabIndex = 18;
            this.LblKérelemDolgozóSzolgálatiHely.Text = "Dolgozó Szolgálati helye:";
            // 
            // TxtKérelemFrsz
            // 
            this.TxtKérelemFrsz.Location = new System.Drawing.Point(199, 177);
            this.TxtKérelemFrsz.Name = "TxtKérelemFrsz";
            this.TxtKérelemFrsz.Size = new System.Drawing.Size(173, 26);
            this.TxtKérelemFrsz.TabIndex = 17;
            // 
            // Txtkérelemnév
            // 
            this.Txtkérelemnév.Location = new System.Drawing.Point(199, 75);
            this.Txtkérelemnév.Name = "Txtkérelemnév";
            this.Txtkérelemnév.Size = new System.Drawing.Size(526, 26);
            this.Txtkérelemnév.TabIndex = 16;
            // 
            // LblKérelemRendszám
            // 
            this.LblKérelemRendszám.AutoSize = true;
            this.LblKérelemRendszám.Location = new System.Drawing.Point(7, 180);
            this.LblKérelemRendszám.Name = "LblKérelemRendszám";
            this.LblKérelemRendszám.Size = new System.Drawing.Size(190, 20);
            this.LblKérelemRendszám.TabIndex = 13;
            this.LblKérelemRendszám.Text = "Autó forgalmi rendszáma:";
            // 
            // TxtkérelemHR
            // 
            this.TxtkérelemHR.Location = new System.Drawing.Point(199, 43);
            this.TxtkérelemHR.Name = "TxtkérelemHR";
            this.TxtkérelemHR.Size = new System.Drawing.Size(173, 26);
            this.TxtkérelemHR.TabIndex = 12;
            // 
            // LblKérelemDolgozóNév
            // 
            this.LblKérelemDolgozóNév.AutoSize = true;
            this.LblKérelemDolgozóNév.Location = new System.Drawing.Point(7, 81);
            this.LblKérelemDolgozóNév.Name = "LblKérelemDolgozóNév";
            this.LblKérelemDolgozóNév.Size = new System.Drawing.Size(110, 20);
            this.LblKérelemDolgozóNév.TabIndex = 11;
            this.LblKérelemDolgozóNév.Text = "Dolgozó neve:";
            // 
            // LblKérelemDolgozóHR
            // 
            this.LblKérelemDolgozóHR.AutoSize = true;
            this.LblKérelemDolgozóHR.Location = new System.Drawing.Point(7, 49);
            this.LblKérelemDolgozóHR.Name = "LblKérelemDolgozóHR";
            this.LblKérelemDolgozóHR.Size = new System.Drawing.Size(185, 20);
            this.LblKérelemDolgozóHR.TabIndex = 10;
            this.LblKérelemDolgozóHR.Text = "Dolgozó HR azonosítója:";
            // 
            // Btnkilelöltörlés
            // 
            this.Btnkilelöltörlés.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.Btnkilelöltörlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btnkilelöltörlés.Location = new System.Drawing.Point(1128, 353);
            this.Btnkilelöltörlés.Name = "Btnkilelöltörlés";
            this.Btnkilelöltörlés.Size = new System.Drawing.Size(45, 45);
            this.Btnkilelöltörlés.TabIndex = 103;
            this.ToolTip1.SetToolTip(this.Btnkilelöltörlés, "Mindent kijelölést töröl");
            this.Btnkilelöltörlés.UseVisualStyleBackColor = true;
            this.Btnkilelöltörlés.Click += new System.EventHandler(this.Btnkilelöltörlés_Click);
            // 
            // BtnKijelölcsop
            // 
            this.BtnKijelölcsop.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.BtnKijelölcsop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnKijelölcsop.Location = new System.Drawing.Point(1078, 353);
            this.BtnKijelölcsop.Name = "BtnKijelölcsop";
            this.BtnKijelölcsop.Size = new System.Drawing.Size(45, 45);
            this.BtnKijelölcsop.TabIndex = 102;
            this.ToolTip1.SetToolTip(this.BtnKijelölcsop, "Mindent kijelöl");
            this.BtnKijelölcsop.UseVisualStyleBackColor = true;
            this.BtnKijelölcsop.Click += new System.EventHandler(this.BtnKijelölcsop_Click);
            // 
            // Btn3szak
            // 
            this.Btn3szak.BackgroundImage = global::Villamos.Properties.Resources._3B;
            this.Btn3szak.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn3szak.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Btn3szak.Location = new System.Drawing.Point(1028, 353);
            this.Btn3szak.Name = "Btn3szak";
            this.Btn3szak.Size = new System.Drawing.Size(45, 45);
            this.Btn3szak.TabIndex = 101;
            this.ToolTip1.SetToolTip(this.Btn3szak, "III Szakszolgálat telepeit jelöli ki");
            this.Btn3szak.UseVisualStyleBackColor = true;
            this.Btn3szak.Click += new System.EventHandler(this.Btn3szak_Click);
            // 
            // Btn2szak
            // 
            this.Btn2szak.BackgroundImage = global::Villamos.Properties.Resources._2B;
            this.Btn2szak.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn2szak.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Btn2szak.Location = new System.Drawing.Point(978, 353);
            this.Btn2szak.Name = "Btn2szak";
            this.Btn2szak.Size = new System.Drawing.Size(45, 45);
            this.Btn2szak.TabIndex = 100;
            this.ToolTip1.SetToolTip(this.Btn2szak, "II Szakszolgálat telepeit jelöli ki");
            this.Btn2szak.UseVisualStyleBackColor = true;
            this.Btn2szak.Click += new System.EventHandler(this.Btn2szak_Click);
            // 
            // Btn1szak
            // 
            this.Btn1szak.BackgroundImage = global::Villamos.Properties.Resources._1B;
            this.Btn1szak.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn1szak.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Btn1szak.Location = new System.Drawing.Point(926, 353);
            this.Btn1szak.Name = "Btn1szak";
            this.Btn1szak.Size = new System.Drawing.Size(45, 45);
            this.Btn1szak.TabIndex = 99;
            this.ToolTip1.SetToolTip(this.Btn1szak, "I Szakszolgálat telepeit jelöli ki");
            this.Btn1szak.UseVisualStyleBackColor = true;
            this.Btn1szak.Click += new System.EventHandler(this.Btn1szak_Click);
            // 
            // BtnKérelemPDF
            // 
            this.BtnKérelemPDF.BackgroundImage = global::Villamos.Properties.Resources.Folder_;
            this.BtnKérelemPDF.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnKérelemPDF.Location = new System.Drawing.Point(854, 259);
            this.BtnKérelemPDF.Name = "BtnKérelemPDF";
            this.BtnKérelemPDF.Size = new System.Drawing.Size(45, 45);
            this.BtnKérelemPDF.TabIndex = 82;
            this.ToolTip1.SetToolTip(this.BtnKérelemPDF, "PDF fájl kiválasztása");
            this.BtnKérelemPDF.UseVisualStyleBackColor = true;
            this.BtnKérelemPDF.Click += new System.EventHandler(this.BtnKérelemPDF_Click);
            // 
            // BtnÖsszSzabiLista
            // 
            this.BtnÖsszSzabiLista.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.BtnÖsszSzabiLista.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnÖsszSzabiLista.Location = new System.Drawing.Point(731, 49);
            this.BtnÖsszSzabiLista.Name = "BtnÖsszSzabiLista";
            this.BtnÖsszSzabiLista.Size = new System.Drawing.Size(45, 45);
            this.BtnÖsszSzabiLista.TabIndex = 79;
            this.ToolTip1.SetToolTip(this.BtnÖsszSzabiLista, "Ha létezik SAP adatokban akkor megkeresi a dolgozót.");
            this.BtnÖsszSzabiLista.UseVisualStyleBackColor = true;
            this.BtnÖsszSzabiLista.Click += new System.EventHandler(this.BtnÖsszSzabiLista_Click);
            // 
            // BtnOktatásÚj
            // 
            this.BtnOktatásÚj.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.BtnOktatásÚj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnOktatásÚj.Location = new System.Drawing.Point(390, 12);
            this.BtnOktatásÚj.Name = "BtnOktatásÚj";
            this.BtnOktatásÚj.Size = new System.Drawing.Size(45, 45);
            this.BtnOktatásÚj.TabIndex = 78;
            this.ToolTip1.SetToolTip(this.BtnOktatásÚj, "Új engedély készítés");
            this.BtnOktatásÚj.UseVisualStyleBackColor = true;
            this.BtnOktatásÚj.Click += new System.EventHandler(this.BtnOktatásÚj_Click);
            // 
            // BtnkérelemRögzítés
            // 
            this.BtnkérelemRögzítés.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.BtnkérelemRögzítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnkérelemRögzítés.Location = new System.Drawing.Point(1175, 6);
            this.BtnkérelemRögzítés.Name = "BtnkérelemRögzítés";
            this.BtnkérelemRögzítés.Size = new System.Drawing.Size(45, 45);
            this.BtnkérelemRögzítés.TabIndex = 65;
            this.ToolTip1.SetToolTip(this.BtnkérelemRögzítés, "Rögzíti az adatokat");
            this.BtnkérelemRögzítés.UseVisualStyleBackColor = true;
            this.BtnkérelemRögzítés.Click += new System.EventHandler(this.BtnkérelemRögzítés_Click);
            // 
            // PDF
            // 
            this.PDF.Controls.Add(this.PDF_néző);
            this.PDF.Location = new System.Drawing.Point(4, 29);
            this.PDF.Name = "PDF";
            this.PDF.Size = new System.Drawing.Size(1263, 557);
            this.PDF.TabIndex = 5;
            this.PDF.Text = "PDF";
            this.PDF.UseVisualStyleBackColor = true;
            // 
            // PDF_néző
            // 
            this.PDF_néző.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PDF_néző.Location = new System.Drawing.Point(6, 8);
            this.PDF_néző.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.PDF_néző.Name = "PDF_néző";
            this.PDF_néző.ShowToolbar = false;
            this.PDF_néző.Size = new System.Drawing.Size(1251, 541);
            this.PDF_néző.TabIndex = 68;
            this.PDF_néző.ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitWidth;
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.SteelBlue;
            this.TabPage1.Controls.Add(this.BtnGondnokSave);
            this.TabPage1.Controls.Add(this.LblGondnokEngedélyezés);
            this.TabPage1.Controls.Add(this.LblGondnokIndoklás);
            this.TabPage1.Controls.Add(this.TxtGondnokMegjegyzés);
            this.TabPage1.Controls.Add(this.CmbGondnokEngedély);
            this.TabPage1.Controls.Add(this.Táblagondnok);
            this.TabPage1.Controls.Add(this.BtnGondnokFrissít);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Size = new System.Drawing.Size(1263, 557);
            this.TabPage1.TabIndex = 2;
            this.TabPage1.Text = "Engedélyezés Gondnok";
            // 
            // BtnGondnokSave
            // 
            this.BtnGondnokSave.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.BtnGondnokSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnGondnokSave.Location = new System.Drawing.Point(1155, 6);
            this.BtnGondnokSave.Name = "BtnGondnokSave";
            this.BtnGondnokSave.Size = new System.Drawing.Size(45, 45);
            this.BtnGondnokSave.TabIndex = 115;
            this.ToolTip1.SetToolTip(this.BtnGondnokSave, "Menti az engedélyezést");
            this.BtnGondnokSave.UseVisualStyleBackColor = true;
            this.BtnGondnokSave.Click += new System.EventHandler(this.BtnGondnokSave_Click);
            // 
            // LblGondnokEngedélyezés
            // 
            this.LblGondnokEngedélyezés.AutoSize = true;
            this.LblGondnokEngedélyezés.Location = new System.Drawing.Point(70, 31);
            this.LblGondnokEngedélyezés.Name = "LblGondnokEngedélyezés";
            this.LblGondnokEngedélyezés.Size = new System.Drawing.Size(113, 20);
            this.LblGondnokEngedélyezés.TabIndex = 114;
            this.LblGondnokEngedélyezés.Text = "Engedélyezés:";
            // 
            // LblGondnokIndoklás
            // 
            this.LblGondnokIndoklás.AutoSize = true;
            this.LblGondnokIndoklás.Location = new System.Drawing.Point(375, 30);
            this.LblGondnokIndoklás.Name = "LblGondnokIndoklás";
            this.LblGondnokIndoklás.Size = new System.Drawing.Size(161, 20);
            this.LblGondnokIndoklás.TabIndex = 113;
            this.LblGondnokIndoklás.Text = "Indoklás/Megjegyzés:";
            // 
            // TxtGondnokMegjegyzés
            // 
            this.TxtGondnokMegjegyzés.Location = new System.Drawing.Point(542, 24);
            this.TxtGondnokMegjegyzés.Name = "TxtGondnokMegjegyzés";
            this.TxtGondnokMegjegyzés.Size = new System.Drawing.Size(595, 26);
            this.TxtGondnokMegjegyzés.TabIndex = 112;
            this.ToolTip1.SetToolTip(this.TxtGondnokMegjegyzés, "Elutasítás alkalmával ki kell tölteni.");
            // 
            // CmbGondnokEngedély
            // 
            this.CmbGondnokEngedély.FormattingEnabled = true;
            this.CmbGondnokEngedély.Location = new System.Drawing.Point(189, 23);
            this.CmbGondnokEngedély.Name = "CmbGondnokEngedély";
            this.CmbGondnokEngedély.Size = new System.Drawing.Size(161, 28);
            this.CmbGondnokEngedély.TabIndex = 111;
            // 
            // Táblagondnok
            // 
            this.Táblagondnok.AllowUserToAddRows = false;
            this.Táblagondnok.AllowUserToDeleteRows = false;
            this.Táblagondnok.AllowUserToResizeRows = false;
            this.Táblagondnok.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Táblagondnok.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Táblagondnok.Location = new System.Drawing.Point(3, 66);
            this.Táblagondnok.Name = "Táblagondnok";
            this.Táblagondnok.RowHeadersWidth = 20;
            this.Táblagondnok.Size = new System.Drawing.Size(1255, 487);
            this.Táblagondnok.TabIndex = 1;
            this.Táblagondnok.SelectionChanged += new System.EventHandler(this.Táblagondnok_SelectionChanged);
            // 
            // BtnGondnokFrissít
            // 
            this.BtnGondnokFrissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.BtnGondnokFrissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnGondnokFrissít.Location = new System.Drawing.Point(5, 5);
            this.BtnGondnokFrissít.Name = "BtnGondnokFrissít";
            this.BtnGondnokFrissít.Size = new System.Drawing.Size(45, 45);
            this.BtnGondnokFrissít.TabIndex = 110;
            this.ToolTip1.SetToolTip(this.BtnGondnokFrissít, "Frissíti az engedélyezési táblázatot");
            this.BtnGondnokFrissít.UseVisualStyleBackColor = true;
            this.BtnGondnokFrissít.Click += new System.EventHandler(this.BtnGondnokFrissít_Click);
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.TabPage2.Controls.Add(this.Elutasít_gomb);
            this.TabPage2.Controls.Add(this.LblSzakszGondnokiFelülbírálás);
            this.TabPage2.Controls.Add(this.LblSzakszEngedély);
            this.TabPage2.Controls.Add(this.CmbSzakszlista);
            this.TabPage2.Controls.Add(this.Táblaszaksz);
            this.TabPage2.Controls.Add(this.BtnEngedélySzakBírál);
            this.TabPage2.Controls.Add(this.BtnSzakszeng);
            this.TabPage2.Controls.Add(this.BtnEngedélySzakFrissít);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Size = new System.Drawing.Size(1263, 557);
            this.TabPage2.TabIndex = 3;
            this.TabPage2.Text = "Engedélyezés Szakszolgálat";
            // 
            // Elutasít_gomb
            // 
            this.Elutasít_gomb.BackgroundImage = global::Villamos.Properties.Resources.bezár;
            this.Elutasít_gomb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Elutasít_gomb.Location = new System.Drawing.Point(226, 11);
            this.Elutasít_gomb.Name = "Elutasít_gomb";
            this.Elutasít_gomb.Size = new System.Drawing.Size(45, 45);
            this.Elutasít_gomb.TabIndex = 123;
            this.ToolTip1.SetToolTip(this.Elutasít_gomb, "Elutasítja a kérelmeket");
            this.Elutasít_gomb.UseVisualStyleBackColor = true;
            this.Elutasít_gomb.Click += new System.EventHandler(this.Elutasít_gomb_Click);
            // 
            // LblSzakszGondnokiFelülbírálás
            // 
            this.LblSzakszGondnokiFelülbírálás.AutoSize = true;
            this.LblSzakszGondnokiFelülbírálás.Location = new System.Drawing.Point(528, 11);
            this.LblSzakszGondnokiFelülbírálás.Name = "LblSzakszGondnokiFelülbírálás";
            this.LblSzakszGondnokiFelülbírálás.Size = new System.Drawing.Size(684, 40);
            this.LblSzakszGondnokiFelülbírálás.TabIndex = 122;
            this.LblSzakszGondnokiFelülbírálás.Text = "Gondoki engedélyezést felül lehet bírálni a szakszolgálat-vezetői engedélyezést m" +
    "egelőzően.\r\nTelephelyen beírt 2,3 státust át lehet írni a táblázatban, majd a fe" +
    "lülbírálás gombbal kell rögzíteni.";
            // 
            // LblSzakszEngedély
            // 
            this.LblSzakszEngedély.AutoSize = true;
            this.LblSzakszEngedély.Location = new System.Drawing.Point(56, 35);
            this.LblSzakszEngedély.Name = "LblSzakszEngedély";
            this.LblSzakszEngedély.Size = new System.Drawing.Size(113, 20);
            this.LblSzakszEngedély.TabIndex = 119;
            this.LblSzakszEngedély.Text = "Engedélyezés:";
            // 
            // CmbSzakszlista
            // 
            this.CmbSzakszlista.FormattingEnabled = true;
            this.CmbSzakszlista.Location = new System.Drawing.Point(277, 23);
            this.CmbSzakszlista.Name = "CmbSzakszlista";
            this.CmbSzakszlista.Size = new System.Drawing.Size(161, 28);
            this.CmbSzakszlista.TabIndex = 118;
            this.CmbSzakszlista.Visible = false;
            // 
            // Táblaszaksz
            // 
            this.Táblaszaksz.AllowUserToAddRows = false;
            this.Táblaszaksz.AllowUserToDeleteRows = false;
            this.Táblaszaksz.AllowUserToResizeRows = false;
            this.Táblaszaksz.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Táblaszaksz.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Táblaszaksz.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Táblaszaksz.Location = new System.Drawing.Point(5, 71);
            this.Táblaszaksz.Name = "Táblaszaksz";
            this.Táblaszaksz.RowHeadersWidth = 20;
            this.Táblaszaksz.Size = new System.Drawing.Size(1255, 482);
            this.Táblaszaksz.TabIndex = 116;
            this.Táblaszaksz.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.Táblaszaksz_CellBeginEdit);
            this.Táblaszaksz.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.Táblaszaksz_CellEndEdit);
            this.Táblaszaksz.SelectionChanged += new System.EventHandler(this.Táblaszaksz_SelectionChanged);
            // 
            // BtnEngedélySzakBírál
            // 
            this.BtnEngedélySzakBírál.BackgroundImage = global::Villamos.Properties.Resources.Iconarchive_Red_Orb_Alphabet_Exclamation_mark;
            this.BtnEngedélySzakBírál.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnEngedélySzakBírál.Location = new System.Drawing.Point(1215, 10);
            this.BtnEngedélySzakBírál.Name = "BtnEngedélySzakBírál";
            this.BtnEngedélySzakBírál.Size = new System.Drawing.Size(45, 45);
            this.BtnEngedélySzakBírál.TabIndex = 121;
            this.ToolTip1.SetToolTip(this.BtnEngedélySzakBírál, "Felülbírálja a gondoki engedélyezést.");
            this.BtnEngedélySzakBírál.UseVisualStyleBackColor = true;
            this.BtnEngedélySzakBírál.Click += new System.EventHandler(this.BtnEngedélySzakBírál_Click);
            // 
            // BtnSzakszeng
            // 
            this.BtnSzakszeng.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.BtnSzakszeng.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSzakszeng.Location = new System.Drawing.Point(175, 11);
            this.BtnSzakszeng.Name = "BtnSzakszeng";
            this.BtnSzakszeng.Size = new System.Drawing.Size(45, 45);
            this.BtnSzakszeng.TabIndex = 120;
            this.ToolTip1.SetToolTip(this.BtnSzakszeng, "Engedélyezi a kérelmeket.");
            this.BtnSzakszeng.UseVisualStyleBackColor = true;
            this.BtnSzakszeng.Click += new System.EventHandler(this.BtnSzakszeng_Click);
            // 
            // BtnEngedélySzakFrissít
            // 
            this.BtnEngedélySzakFrissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.BtnEngedélySzakFrissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnEngedélySzakFrissít.Location = new System.Drawing.Point(5, 10);
            this.BtnEngedélySzakFrissít.Name = "BtnEngedélySzakFrissít";
            this.BtnEngedélySzakFrissít.Size = new System.Drawing.Size(45, 45);
            this.BtnEngedélySzakFrissít.TabIndex = 117;
            this.ToolTip1.SetToolTip(this.BtnEngedélySzakFrissít, "Frissíti az engedélyezési táblázatot");
            this.BtnEngedélySzakFrissít.UseVisualStyleBackColor = true;
            this.BtnEngedélySzakFrissít.Click += new System.EventHandler(this.BtnEngedélySzakFrissít_Click);
            // 
            // Adminisztátor
            // 
            this.Adminisztátor.BackColor = System.Drawing.SystemColors.Highlight;
            this.Adminisztátor.Controls.Add(this.PanelAdminAlap);
            this.Adminisztátor.Controls.Add(this.PanelAdminKérelemOka);
            this.Adminisztátor.Controls.Add(this.BtnDolgozóilsta);
            this.Adminisztátor.Location = new System.Drawing.Point(4, 29);
            this.Adminisztátor.Name = "Adminisztátor";
            this.Adminisztátor.Size = new System.Drawing.Size(1263, 557);
            this.Adminisztátor.TabIndex = 4;
            this.Adminisztátor.Text = "Adminisztrátori beállítások";
            // 
            // PanelAdminAlap
            // 
            this.PanelAdminAlap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelAdminAlap.BackColor = System.Drawing.Color.OliveDrab;
            this.PanelAdminAlap.Controls.Add(this.Aktuálissor);
            this.PanelAdminAlap.Controls.Add(this.BtnAdminÚjEngedély);
            this.PanelAdminAlap.Controls.Add(this.DataAdminAlap);
            this.PanelAdminAlap.Controls.Add(this.LblAdminAktuálisAB);
            this.PanelAdminAlap.Controls.Add(this.BtnAdminRögz);
            this.PanelAdminAlap.Controls.Add(this.DatadminÉrvényes);
            this.PanelAdminAlap.Controls.Add(this.LblAdminABNév);
            this.PanelAdminAlap.Controls.Add(this.LblAdminÉrvényesség);
            this.PanelAdminAlap.Controls.Add(this.TxtAmindFájl);
            this.PanelAdminAlap.Controls.Add(this.TxtAdminaktuális);
            this.PanelAdminAlap.Controls.Add(this.LblAdminSorszámBetűjel);
            this.PanelAdminAlap.Controls.Add(this.TxtadminBetű);
            this.PanelAdminAlap.Controls.Add(this.TxtAdminkönyvtár);
            this.PanelAdminAlap.Controls.Add(this.LblAdminSorszKezdete);
            this.PanelAdminAlap.Controls.Add(this.LblAdminKönyvtár);
            this.PanelAdminAlap.Controls.Add(this.TxtAdminSorszám);
            this.PanelAdminAlap.Location = new System.Drawing.Point(5, 222);
            this.PanelAdminAlap.Name = "PanelAdminAlap";
            this.PanelAdminAlap.Size = new System.Drawing.Size(1255, 328);
            this.PanelAdminAlap.TabIndex = 114;
            // 
            // BtnAdminÚjEngedély
            // 
            this.BtnAdminÚjEngedély.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.BtnAdminÚjEngedély.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnAdminÚjEngedély.Location = new System.Drawing.Point(687, 92);
            this.BtnAdminÚjEngedély.Name = "BtnAdminÚjEngedély";
            this.BtnAdminÚjEngedély.Size = new System.Drawing.Size(45, 45);
            this.BtnAdminÚjEngedély.TabIndex = 116;
            this.ToolTip1.SetToolTip(this.BtnAdminÚjEngedély, "Új engedély készítés");
            this.BtnAdminÚjEngedély.UseVisualStyleBackColor = true;
            this.BtnAdminÚjEngedély.Click += new System.EventHandler(this.BtnAdminÚjEngedély_Click);
            // 
            // DataAdminAlap
            // 
            this.DataAdminAlap.AllowUserToAddRows = false;
            this.DataAdminAlap.AllowUserToDeleteRows = false;
            this.DataAdminAlap.AllowUserToResizeRows = false;
            this.DataAdminAlap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataAdminAlap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataAdminAlap.Location = new System.Drawing.Point(3, 150);
            this.DataAdminAlap.Name = "DataAdminAlap";
            this.DataAdminAlap.RowHeadersVisible = false;
            this.DataAdminAlap.RowHeadersWidth = 20;
            this.DataAdminAlap.Size = new System.Drawing.Size(1249, 175);
            this.DataAdminAlap.TabIndex = 113;
            this.DataAdminAlap.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataAdminAlap_CellClick);
            // 
            // LblAdminAktuálisAB
            // 
            this.LblAdminAktuálisAB.AutoSize = true;
            this.LblAdminAktuálisAB.Location = new System.Drawing.Point(8, 14);
            this.LblAdminAktuálisAB.Name = "LblAdminAktuálisAB";
            this.LblAdminAktuálisAB.Size = new System.Drawing.Size(138, 20);
            this.LblAdminAktuálisAB.TabIndex = 76;
            this.LblAdminAktuálisAB.Text = "Aktuális adatbázis";
            // 
            // BtnAdminRögz
            // 
            this.BtnAdminRögz.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.BtnAdminRögz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnAdminRögz.Location = new System.Drawing.Point(687, 8);
            this.BtnAdminRögz.Name = "BtnAdminRögz";
            this.BtnAdminRögz.Size = new System.Drawing.Size(45, 45);
            this.BtnAdminRögz.TabIndex = 66;
            this.ToolTip1.SetToolTip(this.BtnAdminRögz, "Rögzíti az adatokat");
            this.BtnAdminRögz.UseVisualStyleBackColor = true;
            this.BtnAdminRögz.Click += new System.EventHandler(this.BtnAdminRögz_Click);
            // 
            // DatadminÉrvényes
            // 
            this.DatadminÉrvényes.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DatadminÉrvényes.Location = new System.Drawing.Point(491, 72);
            this.DatadminÉrvényes.Name = "DatadminÉrvényes";
            this.DatadminÉrvényes.Size = new System.Drawing.Size(127, 26);
            this.DatadminÉrvényes.TabIndex = 112;
            // 
            // LblAdminABNév
            // 
            this.LblAdminABNév.AutoSize = true;
            this.LblAdminABNév.Location = new System.Drawing.Point(8, 50);
            this.LblAdminABNév.Name = "LblAdminABNév";
            this.LblAdminABNév.Size = new System.Drawing.Size(113, 20);
            this.LblAdminABNév.TabIndex = 67;
            this.LblAdminABNév.Text = "Adatbázis név:";
            // 
            // LblAdminÉrvényesség
            // 
            this.LblAdminÉrvényesség.AutoSize = true;
            this.LblAdminÉrvényesség.Location = new System.Drawing.Point(349, 81);
            this.LblAdminÉrvényesség.Name = "LblAdminÉrvényesség";
            this.LblAdminÉrvényesség.Size = new System.Drawing.Size(104, 20);
            this.LblAdminÉrvényesség.TabIndex = 80;
            this.LblAdminÉrvényesség.Text = "Érvényesség:";
            // 
            // TxtAmindFájl
            // 
            this.TxtAmindFájl.Location = new System.Drawing.Point(170, 44);
            this.TxtAmindFájl.Name = "TxtAmindFájl";
            this.TxtAmindFájl.Size = new System.Drawing.Size(173, 26);
            this.TxtAmindFájl.TabIndex = 68;
            // 
            // TxtAdminaktuális
            // 
            this.TxtAdminaktuális.Location = new System.Drawing.Point(170, 8);
            this.TxtAdminaktuális.Name = "TxtAdminaktuális";
            this.TxtAdminaktuális.Size = new System.Drawing.Size(173, 26);
            this.TxtAdminaktuális.TabIndex = 77;
            // 
            // LblAdminSorszámBetűjel
            // 
            this.LblAdminSorszámBetűjel.AutoSize = true;
            this.LblAdminSorszámBetűjel.Location = new System.Drawing.Point(8, 82);
            this.LblAdminSorszámBetűjel.Name = "LblAdminSorszámBetűjel";
            this.LblAdminSorszámBetűjel.Size = new System.Drawing.Size(136, 20);
            this.LblAdminSorszámBetűjel.TabIndex = 69;
            this.LblAdminSorszámBetűjel.Text = "Sorszám betűjele:";
            // 
            // TxtadminBetű
            // 
            this.TxtadminBetű.Location = new System.Drawing.Point(170, 76);
            this.TxtadminBetű.Name = "TxtadminBetű";
            this.TxtadminBetű.Size = new System.Drawing.Size(173, 26);
            this.TxtadminBetű.TabIndex = 70;
            // 
            // TxtAdminkönyvtár
            // 
            this.TxtAdminkönyvtár.Location = new System.Drawing.Point(491, 8);
            this.TxtAdminkönyvtár.Name = "TxtAdminkönyvtár";
            this.TxtAdminkönyvtár.Size = new System.Drawing.Size(173, 26);
            this.TxtAdminkönyvtár.TabIndex = 75;
            // 
            // LblAdminSorszKezdete
            // 
            this.LblAdminSorszKezdete.AutoSize = true;
            this.LblAdminSorszKezdete.Location = new System.Drawing.Point(349, 46);
            this.LblAdminSorszKezdete.Name = "LblAdminSorszKezdete";
            this.LblAdminSorszKezdete.Size = new System.Drawing.Size(137, 20);
            this.LblAdminSorszKezdete.TabIndex = 71;
            this.LblAdminSorszKezdete.Text = "Sorszám kezdete:";
            // 
            // LblAdminKönyvtár
            // 
            this.LblAdminKönyvtár.AutoSize = true;
            this.LblAdminKönyvtár.Location = new System.Drawing.Point(349, 14);
            this.LblAdminKönyvtár.Name = "LblAdminKönyvtár";
            this.LblAdminKönyvtár.Size = new System.Drawing.Size(70, 20);
            this.LblAdminKönyvtár.TabIndex = 74;
            this.LblAdminKönyvtár.Text = "Könyvtár";
            // 
            // TxtAdminSorszám
            // 
            this.TxtAdminSorszám.Location = new System.Drawing.Point(491, 40);
            this.TxtAdminSorszám.Name = "TxtAdminSorszám";
            this.TxtAdminSorszám.Size = new System.Drawing.Size(173, 26);
            this.TxtAdminSorszám.TabIndex = 72;
            // 
            // PanelAdminKérelemOka
            // 
            this.PanelAdminKérelemOka.BackColor = System.Drawing.Color.OliveDrab;
            this.PanelAdminKérelemOka.Controls.Add(this.BtnAdminOkfel);
            this.PanelAdminKérelemOka.Controls.Add(this.TxtAdminOk);
            this.PanelAdminKérelemOka.Controls.Add(this.LstAdminokok);
            this.PanelAdminKérelemOka.Controls.Add(this.LblAdminKérelemOka);
            this.PanelAdminKérelemOka.Controls.Add(this.BtnAdminOkTöröl);
            this.PanelAdminKérelemOka.Controls.Add(this.BtnAdminOkrögzítés);
            this.PanelAdminKérelemOka.Location = new System.Drawing.Point(5, 5);
            this.PanelAdminKérelemOka.Name = "PanelAdminKérelemOka";
            this.PanelAdminKérelemOka.Size = new System.Drawing.Size(506, 211);
            this.PanelAdminKérelemOka.TabIndex = 73;
            // 
            // BtnAdminOkfel
            // 
            this.BtnAdminOkfel.BackgroundImage = global::Villamos.Properties.Resources.Button_Upload_01;
            this.BtnAdminOkfel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnAdminOkfel.Location = new System.Drawing.Point(446, 99);
            this.BtnAdminOkfel.Name = "BtnAdminOkfel";
            this.BtnAdminOkfel.Size = new System.Drawing.Size(45, 45);
            this.BtnAdminOkfel.TabIndex = 2;
            this.ToolTip1.SetToolTip(this.BtnAdminOkfel, "Feljebb viszi a sorban az adatot");
            this.BtnAdminOkfel.UseVisualStyleBackColor = true;
            this.BtnAdminOkfel.Click += new System.EventHandler(this.BtnAdminOkfel_Click);
            // 
            // TxtAdminOk
            // 
            this.TxtAdminOk.Location = new System.Drawing.Point(3, 26);
            this.TxtAdminOk.MaxLength = 20;
            this.TxtAdminOk.Name = "TxtAdminOk";
            this.TxtAdminOk.Size = new System.Drawing.Size(432, 26);
            this.TxtAdminOk.TabIndex = 0;
            // 
            // LstAdminokok
            // 
            this.LstAdminokok.FormattingEnabled = true;
            this.LstAdminokok.ItemHeight = 20;
            this.LstAdminokok.Location = new System.Drawing.Point(3, 58);
            this.LstAdminokok.Name = "LstAdminokok";
            this.LstAdminokok.Size = new System.Drawing.Size(432, 144);
            this.LstAdminokok.TabIndex = 21;
            this.LstAdminokok.SelectedIndexChanged += new System.EventHandler(this.LstAdminokok_SelectedIndexChanged);
            // 
            // LblAdminKérelemOka
            // 
            this.LblAdminKérelemOka.AutoSize = true;
            this.LblAdminKérelemOka.Location = new System.Drawing.Point(3, 3);
            this.LblAdminKérelemOka.Name = "LblAdminKérelemOka";
            this.LblAdminKérelemOka.Size = new System.Drawing.Size(101, 20);
            this.LblAdminKérelemOka.TabIndex = 20;
            this.LblAdminKérelemOka.Text = "Kérelem oka:";
            // 
            // BtnAdminOkTöröl
            // 
            this.BtnAdminOkTöröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.BtnAdminOkTöröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnAdminOkTöröl.Location = new System.Drawing.Point(446, 150);
            this.BtnAdminOkTöröl.Name = "BtnAdminOkTöröl";
            this.BtnAdminOkTöröl.Size = new System.Drawing.Size(45, 45);
            this.BtnAdminOkTöröl.TabIndex = 3;
            this.ToolTip1.SetToolTip(this.BtnAdminOkTöröl, "Törli a kijelölt adatot");
            this.BtnAdminOkTöröl.UseVisualStyleBackColor = true;
            this.BtnAdminOkTöröl.Click += new System.EventHandler(this.BtnAdminOkTöröl_Click);
            // 
            // BtnAdminOkrögzítés
            // 
            this.BtnAdminOkrögzítés.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.BtnAdminOkrögzítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnAdminOkrögzítés.Location = new System.Drawing.Point(446, 26);
            this.BtnAdminOkrögzítés.Name = "BtnAdminOkrögzítés";
            this.BtnAdminOkrögzítés.Size = new System.Drawing.Size(45, 45);
            this.BtnAdminOkrögzítés.TabIndex = 1;
            this.ToolTip1.SetToolTip(this.BtnAdminOkrögzítés, "Rögzíti a kérelem okát");
            this.BtnAdminOkrögzítés.UseVisualStyleBackColor = true;
            this.BtnAdminOkrögzítés.Click += new System.EventHandler(this.BtnTíputlétOK_Click);
            // 
            // BtnDolgozóilsta
            // 
            this.BtnDolgozóilsta.BackgroundImage = global::Villamos.Properties.Resources.felhasználók32;
            this.BtnDolgozóilsta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnDolgozóilsta.Location = new System.Drawing.Point(517, 5);
            this.BtnDolgozóilsta.Name = "BtnDolgozóilsta";
            this.BtnDolgozóilsta.Size = new System.Drawing.Size(50, 50);
            this.BtnDolgozóilsta.TabIndex = 78;
            this.ToolTip1.SetToolTip(this.BtnDolgozóilsta, "Frissíti a dolgozói listát");
            this.BtnDolgozóilsta.UseVisualStyleBackColor = true;
            this.BtnDolgozóilsta.Click += new System.EventHandler(this.BtnDolgozóilsta_Click);
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.BurlyWood;
            this.TabPage3.Controls.Add(this.BtnNaplóExcel);
            this.TabPage3.Controls.Add(this.TextNaplósorszám);
            this.TabPage3.Controls.Add(this.LblNaplóEngedélySorsz);
            this.TabPage3.Controls.Add(this.DataNapló);
            this.TabPage3.Controls.Add(this.BtnNaplóLista);
            this.TabPage3.Location = new System.Drawing.Point(4, 29);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(1263, 557);
            this.TabPage3.TabIndex = 7;
            this.TabPage3.Text = "Naplózások";
            // 
            // BtnNaplóExcel
            // 
            this.BtnNaplóExcel.BackColor = System.Drawing.Color.LightSeaGreen;
            this.BtnNaplóExcel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.BtnNaplóExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnNaplóExcel.Location = new System.Drawing.Point(420, 6);
            this.BtnNaplóExcel.Name = "BtnNaplóExcel";
            this.BtnNaplóExcel.Size = new System.Drawing.Size(45, 45);
            this.BtnNaplóExcel.TabIndex = 119;
            this.ToolTip1.SetToolTip(this.BtnNaplóExcel, "A táblázat tartalmát Excelbe menti ki.");
            this.BtnNaplóExcel.UseVisualStyleBackColor = false;
            this.BtnNaplóExcel.Click += new System.EventHandler(this.BtnNaplóExcel_Click);
            // 
            // TextNaplósorszám
            // 
            this.TextNaplósorszám.Location = new System.Drawing.Point(166, 20);
            this.TextNaplósorszám.Name = "TextNaplósorszám";
            this.TextNaplósorszám.Size = new System.Drawing.Size(174, 26);
            this.TextNaplósorszám.TabIndex = 118;
            // 
            // LblNaplóEngedélySorsz
            // 
            this.LblNaplóEngedélySorsz.AutoSize = true;
            this.LblNaplóEngedélySorsz.Location = new System.Drawing.Point(8, 26);
            this.LblNaplóEngedélySorsz.Name = "LblNaplóEngedélySorsz";
            this.LblNaplóEngedélySorsz.Size = new System.Drawing.Size(152, 20);
            this.LblNaplóEngedélySorsz.TabIndex = 117;
            this.LblNaplóEngedélySorsz.Text = "Engedély sorszáma:";
            // 
            // DataNapló
            // 
            this.DataNapló.AllowUserToAddRows = false;
            this.DataNapló.AllowUserToDeleteRows = false;
            this.DataNapló.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DataNapló.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.DataNapló.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataNapló.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataNapló.EnableHeadersVisualStyles = false;
            this.DataNapló.Location = new System.Drawing.Point(4, 57);
            this.DataNapló.Name = "DataNapló";
            this.DataNapló.RowHeadersWidth = 20;
            this.DataNapló.Size = new System.Drawing.Size(1253, 496);
            this.DataNapló.TabIndex = 115;
            // 
            // BtnNaplóLista
            // 
            this.BtnNaplóLista.BackColor = System.Drawing.Color.LightSeaGreen;
            this.BtnNaplóLista.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.BtnNaplóLista.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnNaplóLista.Location = new System.Drawing.Point(369, 6);
            this.BtnNaplóLista.Name = "BtnNaplóLista";
            this.BtnNaplóLista.Size = new System.Drawing.Size(45, 45);
            this.BtnNaplóLista.TabIndex = 116;
            this.ToolTip1.SetToolTip(this.BtnNaplóLista, "Frissíti a táblázatot");
            this.BtnNaplóLista.UseVisualStyleBackColor = false;
            this.BtnNaplóLista.Click += new System.EventHandler(this.BtnNaplóLista_Click);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.BackColor = System.Drawing.Color.ForestGreen;
            this.Holtart.ForeColor = System.Drawing.Color.SpringGreen;
            this.Holtart.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Holtart.Location = new System.Drawing.Point(350, 17);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(870, 23);
            this.Holtart.TabIndex = 79;
            this.Holtart.Visible = false;
            // 
            // PanelTelephely
            // 
            this.PanelTelephely.Controls.Add(this.Cmbtelephely);
            this.PanelTelephely.Controls.Add(this.LblTelephelyBeállítás);
            this.PanelTelephely.Location = new System.Drawing.Point(9, 12);
            this.PanelTelephely.Name = "PanelTelephely";
            this.PanelTelephely.Size = new System.Drawing.Size(335, 36);
            this.PanelTelephely.TabIndex = 54;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(145, 4);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
            this.Cmbtelephely.SelectedIndexChanged += new System.EventHandler(this.Cmbtelephely_SelectedIndexChanged);
            this.Cmbtelephely.SelectionChangeCommitted += new System.EventHandler(this.Cmbtelephely_SelectionChangeCommitted);
            // 
            // LblTelephelyBeállítás
            // 
            this.LblTelephelyBeállítás.AutoSize = true;
            this.LblTelephelyBeállítás.Location = new System.Drawing.Point(3, 6);
            this.LblTelephelyBeállítás.Name = "LblTelephelyBeállítás";
            this.LblTelephelyBeállítás.Size = new System.Drawing.Size(145, 20);
            this.LblTelephelyBeállítás.TabIndex = 17;
            this.LblTelephelyBeállítás.Text = "Telephelyi beállítás:";
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1225, 3);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 55;
            this.ToolTip1.SetToolTip(this.BtnSúgó, "Súgó");
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // Aktuálissor
            // 
            this.Aktuálissor.AutoSize = true;
            this.Aktuálissor.Location = new System.Drawing.Point(170, 116);
            this.Aktuálissor.Name = "Aktuálissor";
            this.Aktuálissor.Size = new System.Drawing.Size(84, 24);
            this.Aktuálissor.TabIndex = 118;
            this.Aktuálissor.Text = "Aktuális";
            this.Aktuálissor.UseVisualStyleBackColor = true;
            // 
            // Ablak_Behajtási
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.ClientSize = new System.Drawing.Size(1284, 647);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.PanelTelephely);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Fülek);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Behajtási";
            this.Text = "AblakBehajtási";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AblakBehajtási_Load);
            this.Fülek.ResumeLayout(false);
            this.Engedélyek.ResumeLayout(false);
            this.Engedélyek.PerformLayout();
            this.PanelEngedély.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TáblaLista)).EndInit();
            this.Kérelem.ResumeLayout(false);
            this.Kérelem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KérelemTábla)).EndInit();
            this.PDF.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Táblagondnok)).EndInit();
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Táblaszaksz)).EndInit();
            this.Adminisztátor.ResumeLayout(false);
            this.PanelAdminAlap.ResumeLayout(false);
            this.PanelAdminAlap.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataAdminAlap)).EndInit();
            this.PanelAdminKérelemOka.ResumeLayout(false);
            this.PanelAdminKérelemOka.PerformLayout();
            this.TabPage3.ResumeLayout(false);
            this.TabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataNapló)).EndInit();
            this.PanelTelephely.ResumeLayout(false);
            this.PanelTelephely.PerformLayout();
            this.ResumeLayout(false);

        }

        internal TabControl Fülek;
        internal TabPage Kérelem;
        internal ComboBox CmbkérelemOka;
        internal Label LblKérelemKérelemOka;
        internal ComboBox CmbKérelemSzolgálati;
        internal Label LblKérelemDolgozóSzolgálatiHely;
        internal TextBox TxtKérelemFrsz;
        internal TextBox Txtkérelemnév;
        internal Label LblKérelemRendszám;
        internal TextBox TxtkérelemHR;
        internal Label LblKérelemDolgozóNév;
        internal Label LblKérelemDolgozóHR;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal Panel PanelTelephely;
        internal ComboBox Cmbtelephely;
        internal Label LblTelephelyBeállítás;
        internal Button BtnSúgó;
        internal Button BtnkérelemRögzítés;
        internal ToolTip ToolTip1;
        internal TextBox TxtKérelemID;
        internal Label LblKérelemEngedélySzám;
        internal Button BtnOktatásÚj;
        internal TabPage Adminisztátor;
        internal TextBox TxtAdminSorszám;
        internal Label LblAdminSorszKezdete;
        internal TextBox TxtadminBetű;
        internal Label LblAdminSorszámBetűjel;
        internal TextBox TxtAmindFájl;
        internal Label LblAdminABNév;
        internal Button BtnAdminRögz;
        internal Panel PanelAdminKérelemOka;
        internal Button BtnAdminOkfel;
        internal TextBox TxtAdminOk;
        internal ListBox LstAdminokok;
        internal Label LblAdminKérelemOka;
        internal Button BtnAdminOkTöröl;
        internal Button BtnAdminOkrögzítés;
        internal Button BtnÖsszSzabiLista;
        internal TextBox TxtKérrelemPDF;
        internal Label LblKérelemPDFneve;
        internal Button BtnKérelemPDF;

        internal DateTimePicker KérelemDátuma;
        internal Label LblKérelemIgénylésDátum;
        internal TextBox TxtKérelemautó;
        internal Label LblKérelemAutókSzáma;
        internal Button Btn3szak;
        internal Button Btn2szak;
        internal Button Btn1szak;
        internal Button Btnkilelöltörlés;
        internal Button BtnKijelölcsop;
        internal TextBox TxtKérelemMegjegyzés;
        internal Label LblKérelemMegjegyzés;
        internal Label LblKérelemJogosultságTípus;
        internal ComboBox CmbKérelemTípus;
        internal TextBox TxtAdminkönyvtár;
        internal Label LblAdminKönyvtár;
        internal TextBox TxtAdminaktuális;
        internal Label LblAdminAktuálisAB;
        internal TabPage Engedélyek;
        internal Button BtnEngedélyListaFrissít;
        internal DataGridView Táblagondnok;
        internal Button BtnGondnokFrissít;
        internal Button BtnExcelkimenet;
        internal Label LblGondnokEngedélyezés;
        internal Label LblGondnokIndoklás;
        internal TextBox TxtGondnokMegjegyzés;
        internal ComboBox CmbGondnokEngedély;
        internal Button BtnGondnokSave;
        internal ComboBox CMBkérelemStátus;
        internal Label LblKérelemEngedélyStátus;
        internal Button BtnSzakszeng;
        internal Label LblSzakszEngedély;
        internal ComboBox CmbSzakszlista;
        internal Button BtnEngedélySzakFrissít;
        internal DataGridView Táblaszaksz;
        internal ComboBox CmbEngedélylistaszűrő;
        internal Label LblEngedélyEngedélyStátus;
        internal TextBox Txtnévszűrő;
        internal Label LblEngedélyDolgozóNév;
        internal Button BtnDolgozóilsta;
        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal Button BtnEngedélyListaEngedélyNyomtat;
        internal DateTimePicker DatÉrvényes;
        internal Label LblKérelemÉrvényességVége;
        internal DateTimePicker DatadminÉrvényes;
        internal Label LblAdminÉrvényesség;
        internal Panel PanelEngedély;
        internal Button BtnEngedélyListaGondnokEmail;
        internal Button BtnEngedélyListaÁtvételNyomtat;
        internal Button BtnEngedélyListaÁtvételKüld;
        internal Button BtnEngedélyListaÁtvételMegtörtént;
        internal Button BtnEngedélyListaTörlés;
        internal TabPage TabPage3;
        internal TextBox TextNaplósorszám;
        internal Label LblNaplóEngedélySorsz;
        internal Button BtnNaplóLista;
        internal DataGridView DataNapló;
        internal Button BtnNaplóExcel;
        internal Panel PanelAdminAlap;
        internal DataGridView DataAdminAlap;
        internal Button BtnAdminÚjEngedély;
        internal TextBox TxtRendszámszűrő;
        internal Label LblEngedélyRendszám;
        internal Button BtnEngedélyListaSzakEmail;
        internal Button BtnEngedélySzakBírál;
        internal Label LblSzakszGondnokiFelülbírálás;
        internal CheckBox Nézet_Egyszerű;
        internal TabPage PDF;
        internal PdfiumViewer.PdfViewer PDF_néző;
        internal DataGridView KérelemTábla;
        internal DataGridView TáblaLista;
        internal DataGridViewCheckBoxColumn Column1;
        internal DataGridViewTextBoxColumn Column2;
        internal DataGridViewTextBoxColumn Column3;
        internal DataGridViewTextBoxColumn Column4;
        internal DataGridViewTextBoxColumn Column5;
        internal Button Elutasít_gomb;
        private CheckBox Aktuálissor;
    }
}