using System.Diagnostics;
using System.Windows.Forms;

namespace Villamos
{

    public partial class Ablak_Oktatások : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Oktatások));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.ChkDolgozónév = new System.Windows.Forms.CheckedListBox();
            this.ChkCsoport = new System.Windows.Forms.CheckedListBox();
            this.TáblaOktatás = new System.Windows.Forms.DataGridView();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Btnfrissít = new System.Windows.Forms.Button();
            this.BtnElrendelés = new System.Windows.Forms.Button();
            this.Kötelezésmód = new System.Windows.Forms.Button();
            this.TörölKötelezés = new System.Windows.Forms.Button();
            this.BtnOktatásFrissít = new System.Windows.Forms.Button();
            this.BtnAdminMentés = new System.Windows.Forms.Button();
            this.BtnJelenléti = new System.Windows.Forms.Button();
            this.Button10 = new System.Windows.Forms.Button();
            this.Button9 = new System.Windows.Forms.Button();
            this.BtnOktatásEredményTöröl = new System.Windows.Forms.Button();
            this.BtnPdfÚjHasznál = new System.Windows.Forms.Button();
            this.BtnPDFsave = new System.Windows.Forms.Button();
            this.BtnPdfMegnyitás = new System.Windows.Forms.Button();
            this.BtnNaplózásEredményTöröl = new System.Windows.Forms.Button();
            this.BtnRögzítFrissít = new System.Windows.Forms.Button();
            this.BtnPdfNyit = new System.Windows.Forms.Button();
            this.Button3 = new System.Windows.Forms.Button();
            this.BtnKijelölésátjelöl = new System.Windows.Forms.Button();
            this.Btnkilelöltörlés = new System.Windows.Forms.Button();
            this.Btnkijelöléstöröl = new System.Windows.Forms.Button();
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.BtnEmailKüldés = new System.Windows.Forms.Button();
            this.Txtemail = new System.Windows.Forms.TextBox();
            this.Btnmindkijelöl = new System.Windows.Forms.Button();
            this.BtnKijelölcsop = new System.Windows.Forms.Button();
            this.BtnExcelkimenet = new System.Windows.Forms.Button();
            this.Fülek = new System.Windows.Forms.TabControl();
            this.TPElrendelés = new System.Windows.Forms.TabPage();
            this.Chkelrendelés = new System.Windows.Forms.CheckBox();
            this.CMBStátus = new System.Windows.Forms.ComboBox();
            this.CmbGyakoriság = new System.Windows.Forms.ComboBox();
            this.CmbKategória = new System.Windows.Forms.ComboBox();
            this.OktDátum = new System.Windows.Forms.DateTimePicker();
            this.Label3 = new System.Windows.Forms.Label();
            this.TPOktatandó = new System.Windows.Forms.TabPage();
            this.Oktatás_Panel = new System.Windows.Forms.Panel();
            this.Label2 = new System.Windows.Forms.Label();
            this.Lejáródátum = new System.Windows.Forms.DateTimePicker();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Átütemezés = new System.Windows.Forms.DateTimePicker();
            this.Oktataandó_Választó = new System.Windows.Forms.CheckBox();
            this.CMBoktatástárgya = new System.Windows.Forms.ComboBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.TPAdmin = new System.Windows.Forms.TabPage();
            this.Label22 = new System.Windows.Forms.Label();
            this.TextBox2 = new System.Windows.Forms.TextBox();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.Egyébszöveg = new System.Windows.Forms.RichTextBox();
            this.Label21 = new System.Windows.Forms.Label();
            this.Adminhelyszín = new System.Windows.Forms.TextBox();
            this.Label20 = new System.Windows.Forms.Label();
            this.Admintematika = new System.Windows.Forms.RichTextBox();
            this.Label19 = new System.Windows.Forms.Label();
            this.Admintartam = new System.Windows.Forms.TextBox();
            this.Adminoktatástárgya = new System.Windows.Forms.ComboBox();
            this.Label18 = new System.Windows.Forms.Label();
            this.AdminOktatómunkaköre = new System.Windows.Forms.TextBox();
            this.Label17 = new System.Windows.Forms.Label();
            this.Label16 = new System.Windows.Forms.Label();
            this.AdminOktatásoka = new System.Windows.Forms.TextBox();
            this.Label15 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.AdminOktató = new System.Windows.Forms.ComboBox();
            this.Adminoktatásdátuma = new System.Windows.Forms.DateTimePicker();
            this.Label14 = new System.Windows.Forms.Label();
            this.TPOktatásRögz = new System.Windows.Forms.TabPage();
            this.Megjegyzés = new System.Windows.Forms.TextBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.CHKpdfvan = new System.Windows.Forms.CheckBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.CMBszámon = new System.Windows.Forms.ComboBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.LSToktató = new System.Windows.Forms.ComboBox();
            this.BizDátum = new System.Windows.Forms.DateTimePicker();
            this.Label1 = new System.Windows.Forms.Label();
            this.Chkoktat = new System.Windows.Forms.CheckBox();
            this.Txtmegnyitott = new System.Windows.Forms.TextBox();
            this.Txtmentett = new System.Windows.Forms.TextBox();
            this.TPRögzítésekNaplóz = new System.Windows.Forms.TabPage();
            this.CHkNapló = new System.Windows.Forms.CheckBox();
            this.Cmboktatásrögz = new System.Windows.Forms.ComboBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.Dátumig = new System.Windows.Forms.DateTimePicker();
            this.Dátumtól = new System.Windows.Forms.DateTimePicker();
            this.Label8 = new System.Windows.Forms.Label();
            this.TabPage6 = new System.Windows.Forms.TabPage();
            this.PDF_néző = new PdfiumViewer.PdfViewer();
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TáblaOktatás)).BeginInit();
            this.Fülek.SuspendLayout();
            this.TPElrendelés.SuspendLayout();
            this.TPOktatandó.SuspendLayout();
            this.Oktatás_Panel.SuspendLayout();
            this.TPAdmin.SuspendLayout();
            this.TPOktatásRögz.SuspendLayout();
            this.TPRögzítésekNaplóz.SuspendLayout();
            this.TabPage6.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.Cmbtelephely);
            this.Panel1.Controls.Add(this.Label13);
            this.Panel1.Location = new System.Drawing.Point(6, 12);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(335, 33);
            this.Panel1.TabIndex = 53;
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
            this.Label13.Location = new System.Drawing.Point(3, 4);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(145, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // ChkDolgozónév
            // 
            this.ChkDolgozónév.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ChkDolgozónév.CheckOnClick = true;
            this.ChkDolgozónév.FormattingEnabled = true;
            this.ChkDolgozónév.Location = new System.Drawing.Point(6, 295);
            this.ChkDolgozónév.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ChkDolgozónév.Name = "ChkDolgozónév";
            this.ChkDolgozónév.Size = new System.Drawing.Size(335, 235);
            this.ChkDolgozónév.TabIndex = 56;
            // 
            // ChkCsoport
            // 
            this.ChkCsoport.CheckOnClick = true;
            this.ChkCsoport.FormattingEnabled = true;
            this.ChkCsoport.Location = new System.Drawing.Point(6, 104);
            this.ChkCsoport.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ChkCsoport.Name = "ChkCsoport";
            this.ChkCsoport.Size = new System.Drawing.Size(335, 109);
            this.ChkCsoport.TabIndex = 55;
            // 
            // TáblaOktatás
            // 
            this.TáblaOktatás.AllowUserToAddRows = false;
            this.TáblaOktatás.AllowUserToDeleteRows = false;
            this.TáblaOktatás.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.TáblaOktatás.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.TáblaOktatás.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TáblaOktatás.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TáblaOktatás.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.TáblaOktatás.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TáblaOktatás.EnableHeadersVisualStyles = false;
            this.TáblaOktatás.Location = new System.Drawing.Point(348, 10);
            this.TáblaOktatás.Name = "TáblaOktatás";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TáblaOktatás.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.TáblaOktatás.RowHeadersWidth = 25;
            this.TáblaOktatás.Size = new System.Drawing.Size(935, 338);
            this.TáblaOktatás.TabIndex = 62;
            this.TáblaOktatás.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TáblaOktatás_CellClick);
            this.TáblaOktatás.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.TáblaOktatás_CellFormatting);
            this.TáblaOktatás.SelectionChanged += new System.EventHandler(this.TáblaOktatás_SelectionChanged);
            // 
            // Btnfrissít
            // 
            this.Btnfrissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Btnfrissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btnfrissít.Location = new System.Drawing.Point(740, 0);
            this.Btnfrissít.Name = "Btnfrissít";
            this.Btnfrissít.Size = new System.Drawing.Size(45, 45);
            this.Btnfrissít.TabIndex = 68;
            this.ToolTip1.SetToolTip(this.Btnfrissít, "A feltételeknek megfelelően listázza az oktatásokat.");
            this.Btnfrissít.UseVisualStyleBackColor = true;
            this.Btnfrissít.Click += new System.EventHandler(this.Btnfrissít_Click_1);
            // 
            // BtnElrendelés
            // 
            this.BtnElrendelés.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.BtnElrendelés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnElrendelés.Location = new System.Drawing.Point(237, 65);
            this.BtnElrendelés.Name = "BtnElrendelés";
            this.BtnElrendelés.Size = new System.Drawing.Size(45, 45);
            this.BtnElrendelés.TabIndex = 43;
            this.ToolTip1.SetToolTip(this.BtnElrendelés, "Rögzíti az adatokat");
            this.BtnElrendelés.UseVisualStyleBackColor = true;
            this.BtnElrendelés.Click += new System.EventHandler(this.BtnElrendelés_Click);
            // 
            // Kötelezésmód
            // 
            this.Kötelezésmód.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Kötelezésmód.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Kötelezésmód.Location = new System.Drawing.Point(281, 73);
            this.Kötelezésmód.Name = "Kötelezésmód";
            this.Kötelezésmód.Size = new System.Drawing.Size(45, 45);
            this.Kötelezésmód.TabIndex = 91;
            this.ToolTip1.SetToolTip(this.Kötelezésmód, "Módosítja a dátumot");
            this.Kötelezésmód.UseVisualStyleBackColor = true;
            this.Kötelezésmód.Click += new System.EventHandler(this.Kötelezésmód_Click);
            // 
            // TörölKötelezés
            // 
            this.TörölKötelezés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.TörölKötelezés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TörölKötelezés.Location = new System.Drawing.Point(513, 72);
            this.TörölKötelezés.Name = "TörölKötelezés";
            this.TörölKötelezés.Size = new System.Drawing.Size(45, 45);
            this.TörölKötelezés.TabIndex = 87;
            this.ToolTip1.SetToolTip(this.TörölKötelezés, "Kijelölt elemek törlése");
            this.TörölKötelezés.UseVisualStyleBackColor = true;
            this.TörölKötelezés.Click += new System.EventHandler(this.TörölKötelezés_Click);
            // 
            // BtnOktatásFrissít
            // 
            this.BtnOktatásFrissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.BtnOktatásFrissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnOktatásFrissít.Location = new System.Drawing.Point(847, 10);
            this.BtnOktatásFrissít.Name = "BtnOktatásFrissít";
            this.BtnOktatásFrissít.Size = new System.Drawing.Size(45, 45);
            this.BtnOktatásFrissít.TabIndex = 62;
            this.ToolTip1.SetToolTip(this.BtnOktatásFrissít, "A feltételeknek megfelelően listázza az oktatandókat.");
            this.BtnOktatásFrissít.UseVisualStyleBackColor = true;
            this.BtnOktatásFrissít.Click += new System.EventHandler(this.BtnOktatásFrissít_Click);
            // 
            // BtnAdminMentés
            // 
            this.BtnAdminMentés.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAdminMentés.BackgroundImage = global::Villamos.Properties.Resources.Designcontest_Ecommerce_Business_Save;
            this.BtnAdminMentés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnAdminMentés.Location = new System.Drawing.Point(732, 6);
            this.BtnAdminMentés.Name = "BtnAdminMentés";
            this.BtnAdminMentés.Size = new System.Drawing.Size(45, 45);
            this.BtnAdminMentés.TabIndex = 94;
            this.ToolTip1.SetToolTip(this.BtnAdminMentés, "Menti az adatokat későbbi mintának");
            this.BtnAdminMentés.UseVisualStyleBackColor = true;
            this.BtnAdminMentés.Click += new System.EventHandler(this.BtnAdminMentés_Click);
            // 
            // BtnJelenléti
            // 
            this.BtnJelenléti.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnJelenléti.BackgroundImage = global::Villamos.Properties.Resources.App_edit;
            this.BtnJelenléti.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnJelenléti.Location = new System.Drawing.Point(875, 60);
            this.BtnJelenléti.Name = "BtnJelenléti";
            this.BtnJelenléti.Size = new System.Drawing.Size(45, 45);
            this.BtnJelenléti.TabIndex = 92;
            this.ToolTip1.SetToolTip(this.BtnJelenléti, "Elkészíti az oktatás jelenléti ívét Excelben");
            this.BtnJelenléti.UseVisualStyleBackColor = true;
            this.BtnJelenléti.Click += new System.EventHandler(this.BtnJelenléti_Click);
            // 
            // Button10
            // 
            this.Button10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button10.BackgroundImage = global::Villamos.Properties.Resources.fel;
            this.Button10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button10.Location = new System.Drawing.Point(875, 0);
            this.Button10.Name = "Button10";
            this.Button10.Size = new System.Drawing.Size(45, 45);
            this.Button10.TabIndex = 67;
            this.ToolTip1.SetToolTip(this.Button10, "Frissíti a névsort");
            this.Button10.UseVisualStyleBackColor = true;
            this.Button10.Click += new System.EventHandler(this.BtnLapFül_Click);
            // 
            // Button9
            // 
            this.Button9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button9.BackgroundImage = global::Villamos.Properties.Resources.le;
            this.Button9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button9.Location = new System.Drawing.Point(875, 0);
            this.Button9.Name = "Button9";
            this.Button9.Size = new System.Drawing.Size(45, 45);
            this.Button9.TabIndex = 68;
            this.ToolTip1.SetToolTip(this.Button9, "Frissíti a névsort");
            this.Button9.UseVisualStyleBackColor = true;
            this.Button9.Click += new System.EventHandler(this.Button10_Click);
            // 
            // BtnOktatásEredményTöröl
            // 
            this.BtnOktatásEredményTöröl.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.BtnOktatásEredményTöröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnOktatásEredményTöröl.Location = new System.Drawing.Point(13, 115);
            this.BtnOktatásEredményTöröl.Name = "BtnOktatásEredményTöröl";
            this.BtnOktatásEredményTöröl.Size = new System.Drawing.Size(45, 45);
            this.BtnOktatásEredményTöröl.TabIndex = 83;
            this.ToolTip1.SetToolTip(this.BtnOktatásEredményTöröl, "Táblázatban a kijelöleölést törli");
            this.BtnOktatásEredményTöröl.UseVisualStyleBackColor = true;
            this.BtnOktatásEredményTöröl.Click += new System.EventHandler(this.BtnOktatásEredményTöröl_Click);
            // 
            // BtnPdfÚjHasznál
            // 
            this.BtnPdfÚjHasznál.BackgroundImage = global::Villamos.Properties.Resources.BeCardStack;
            this.BtnPdfÚjHasznál.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnPdfÚjHasznál.Location = new System.Drawing.Point(13, 64);
            this.BtnPdfÚjHasznál.Name = "BtnPdfÚjHasznál";
            this.BtnPdfÚjHasznál.Size = new System.Drawing.Size(45, 45);
            this.BtnPdfÚjHasznál.TabIndex = 81;
            this.ToolTip1.SetToolTip(this.BtnPdfÚjHasznál, "Feltöltött PDF fájlok csatolása");
            this.BtnPdfÚjHasznál.UseVisualStyleBackColor = true;
            this.BtnPdfÚjHasznál.Click += new System.EventHandler(this.BtnPdfÚjHasználClick);
            // 
            // BtnPDFsave
            // 
            this.BtnPDFsave.BackgroundImage = global::Villamos.Properties.Resources.Designcontest_Ecommerce_Business_Save;
            this.BtnPDFsave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnPDFsave.Location = new System.Drawing.Point(878, 11);
            this.BtnPDFsave.Name = "BtnPDFsave";
            this.BtnPDFsave.Size = new System.Drawing.Size(45, 45);
            this.BtnPDFsave.TabIndex = 71;
            this.ToolTip1.SetToolTip(this.BtnPDFsave, "Menti az oktatásokat");
            this.BtnPDFsave.UseVisualStyleBackColor = true;
            this.BtnPDFsave.Click += new System.EventHandler(this.BtnPDFsave_Click);
            // 
            // BtnPdfMegnyitás
            // 
            this.BtnPdfMegnyitás.BackgroundImage = global::Villamos.Properties.Resources.Folder_;
            this.BtnPdfMegnyitás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnPdfMegnyitás.Location = new System.Drawing.Point(13, 13);
            this.BtnPdfMegnyitás.Name = "BtnPdfMegnyitás";
            this.BtnPdfMegnyitás.Size = new System.Drawing.Size(45, 45);
            this.BtnPdfMegnyitás.TabIndex = 70;
            this.ToolTip1.SetToolTip(this.BtnPdfMegnyitás, "PDF fájl kiválasztása");
            this.BtnPdfMegnyitás.UseVisualStyleBackColor = true;
            this.BtnPdfMegnyitás.Click += new System.EventHandler(this.BtnPdfMegnyitás_Click);
            // 
            // BtnNaplózásEredményTöröl
            // 
            this.BtnNaplózásEredményTöröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.BtnNaplózásEredményTöröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnNaplózásEredményTöröl.Location = new System.Drawing.Point(16, 126);
            this.BtnNaplózásEredményTöröl.Name = "BtnNaplózásEredményTöröl";
            this.BtnNaplózásEredményTöröl.Size = new System.Drawing.Size(45, 45);
            this.BtnNaplózásEredményTöröl.TabIndex = 86;
            this.ToolTip1.SetToolTip(this.BtnNaplózásEredményTöröl, "Kijelölt elemek törlése");
            this.BtnNaplózásEredményTöröl.UseVisualStyleBackColor = true;
            this.BtnNaplózásEredményTöröl.Click += new System.EventHandler(this.BtnNaplózásEredményTöröl_Click);
            // 
            // BtnRögzítFrissít
            // 
            this.BtnRögzítFrissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.BtnRögzítFrissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnRögzítFrissít.Location = new System.Drawing.Point(530, 3);
            this.BtnRögzítFrissít.Name = "BtnRögzítFrissít";
            this.BtnRögzítFrissít.Size = new System.Drawing.Size(45, 45);
            this.BtnRögzítFrissít.TabIndex = 63;
            this.ToolTip1.SetToolTip(this.BtnRögzítFrissít, "A feltételeknek megfelelően listázza az oktatandókat.");
            this.BtnRögzítFrissít.UseVisualStyleBackColor = true;
            this.BtnRögzítFrissít.Click += new System.EventHandler(this.BtnRögzítFrissít_Click);
            // 
            // BtnPdfNyit
            // 
            this.BtnPdfNyit.BackgroundImage = global::Villamos.Properties.Resources.fel;
            this.BtnPdfNyit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnPdfNyit.Location = new System.Drawing.Point(3, 3);
            this.BtnPdfNyit.Name = "BtnPdfNyit";
            this.BtnPdfNyit.Size = new System.Drawing.Size(45, 45);
            this.BtnPdfNyit.TabIndex = 65;
            this.ToolTip1.SetToolTip(this.BtnPdfNyit, "Frissíti a névsort");
            this.BtnPdfNyit.UseVisualStyleBackColor = true;
            this.BtnPdfNyit.Click += new System.EventHandler(this.BtnPdfNyit_Click);
            // 
            // Button3
            // 
            this.Button3.BackgroundImage = global::Villamos.Properties.Resources.le;
            this.Button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button3.Location = new System.Drawing.Point(3, 3);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(45, 45);
            this.Button3.TabIndex = 66;
            this.ToolTip1.SetToolTip(this.Button3, "Frissíti a névsort");
            this.Button3.UseVisualStyleBackColor = true;
            this.Button3.Click += new System.EventHandler(this.BtnPdfCsuk_Click);
            // 
            // BtnKijelölésátjelöl
            // 
            this.BtnKijelölésátjelöl.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.BtnKijelölésátjelöl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnKijelölésátjelöl.Location = new System.Drawing.Point(108, 51);
            this.BtnKijelölésátjelöl.Name = "BtnKijelölésátjelöl";
            this.BtnKijelölésátjelöl.Size = new System.Drawing.Size(45, 45);
            this.BtnKijelölésátjelöl.TabIndex = 61;
            this.ToolTip1.SetToolTip(this.BtnKijelölésátjelöl, "Frissíti a névsort");
            this.BtnKijelölésátjelöl.UseVisualStyleBackColor = true;
            this.BtnKijelölésátjelöl.Click += new System.EventHandler(this.BtnKijelölésátjelöl_Click);
            // 
            // Btnkilelöltörlés
            // 
            this.Btnkilelöltörlés.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.Btnkilelöltörlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btnkilelöltörlés.Location = new System.Drawing.Point(57, 51);
            this.Btnkilelöltörlés.Name = "Btnkilelöltörlés";
            this.Btnkilelöltörlés.Size = new System.Drawing.Size(45, 45);
            this.Btnkilelöltörlés.TabIndex = 60;
            this.ToolTip1.SetToolTip(this.Btnkilelöltörlés, "Mindent kijelölést töröl");
            this.Btnkilelöltörlés.UseVisualStyleBackColor = true;
            this.Btnkilelöltörlés.Click += new System.EventHandler(this.Btnkijelöltörlés_Click);
            // 
            // Btnkijelöléstöröl
            // 
            this.Btnkijelöléstöröl.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.Btnkijelöléstöröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btnkijelöléstöröl.Location = new System.Drawing.Point(57, 242);
            this.Btnkijelöléstöröl.Name = "Btnkijelöléstöröl";
            this.Btnkijelöléstöröl.Size = new System.Drawing.Size(45, 45);
            this.Btnkijelöléstöröl.TabIndex = 58;
            this.ToolTip1.SetToolTip(this.Btnkijelöléstöröl, "Mindent kijelölést töröl");
            this.Btnkijelöléstöröl.UseVisualStyleBackColor = true;
            this.Btnkijelöléstöröl.Click += new System.EventHandler(this.Btnkijelöléstöröl_Click);
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(296, 51);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 54;
            this.ToolTip1.SetToolTip(this.BtnSúgó, "Súgó");
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // BtnEmailKüldés
            // 
            this.BtnEmailKüldés.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnEmailKüldés.BackgroundImage = global::Villamos.Properties.Resources.email;
            this.BtnEmailKüldés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnEmailKüldés.Location = new System.Drawing.Point(875, 160);
            this.BtnEmailKüldés.Name = "BtnEmailKüldés";
            this.BtnEmailKüldés.Size = new System.Drawing.Size(45, 45);
            this.BtnEmailKüldés.TabIndex = 103;
            this.ToolTip1.SetToolTip(this.BtnEmailKüldés, "Elküldi e-mailben FAR-hoz az adatokat.");
            this.BtnEmailKüldés.UseVisualStyleBackColor = true;
            this.BtnEmailKüldés.Visible = false;
            this.BtnEmailKüldés.Click += new System.EventHandler(this.BtnEmailKüldés_Click);
            // 
            // Txtemail
            // 
            this.Txtemail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Txtemail.Location = new System.Drawing.Point(226, 177);
            this.Txtemail.Multiline = true;
            this.Txtemail.Name = "Txtemail";
            this.Txtemail.Size = new System.Drawing.Size(628, 28);
            this.Txtemail.TabIndex = 106;
            this.ToolTip1.SetToolTip(this.Txtemail, "Az egyes e-mail címeket pontosvesszővel kell elválasztani.");
            // 
            // Btnmindkijelöl
            // 
            this.Btnmindkijelöl.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.Btnmindkijelöl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btnmindkijelöl.Location = new System.Drawing.Point(6, 242);
            this.Btnmindkijelöl.Name = "Btnmindkijelöl";
            this.Btnmindkijelöl.Size = new System.Drawing.Size(45, 45);
            this.Btnmindkijelöl.TabIndex = 59;
            this.ToolTip1.SetToolTip(this.Btnmindkijelöl, "Mindent kijelöl");
            this.Btnmindkijelöl.UseVisualStyleBackColor = true;
            this.Btnmindkijelöl.Click += new System.EventHandler(this.Btnmindkijelöl_Click);
            // 
            // BtnKijelölcsop
            // 
            this.BtnKijelölcsop.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.BtnKijelölcsop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnKijelölcsop.Location = new System.Drawing.Point(6, 51);
            this.BtnKijelölcsop.Name = "BtnKijelölcsop";
            this.BtnKijelölcsop.Size = new System.Drawing.Size(45, 45);
            this.BtnKijelölcsop.TabIndex = 57;
            this.ToolTip1.SetToolTip(this.BtnKijelölcsop, "Mindent kijelöl");
            this.BtnKijelölcsop.UseVisualStyleBackColor = true;
            this.BtnKijelölcsop.Click += new System.EventHandler(this.BtnKijelölcsop_Click);
            // 
            // BtnExcelkimenet
            // 
            this.BtnExcelkimenet.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.BtnExcelkimenet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnExcelkimenet.Location = new System.Drawing.Point(245, 51);
            this.BtnExcelkimenet.Name = "BtnExcelkimenet";
            this.BtnExcelkimenet.Size = new System.Drawing.Size(45, 45);
            this.BtnExcelkimenet.TabIndex = 69;
            this.ToolTip1.SetToolTip(this.BtnExcelkimenet, "Excel létrehozása a táblázatból");
            this.BtnExcelkimenet.UseVisualStyleBackColor = true;
            this.BtnExcelkimenet.Click += new System.EventHandler(this.BtnExcelkimenet_Click);
            // 
            // Fülek
            // 
            this.Fülek.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fülek.Controls.Add(this.TPElrendelés);
            this.Fülek.Controls.Add(this.TPOktatandó);
            this.Fülek.Controls.Add(this.TPAdmin);
            this.Fülek.Controls.Add(this.TPOktatásRögz);
            this.Fülek.Controls.Add(this.TPRögzítésekNaplóz);
            this.Fülek.Controls.Add(this.TabPage6);
            this.Fülek.Location = new System.Drawing.Point(348, 354);
            this.Fülek.Name = "Fülek";
            this.Fülek.Padding = new System.Drawing.Point(16, 3);
            this.Fülek.SelectedIndex = 0;
            this.Fülek.Size = new System.Drawing.Size(934, 215);
            this.Fülek.TabIndex = 67;
            this.Fülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Fülek_DrawItem);
            this.Fülek.SelectedIndexChanged += new System.EventHandler(this.Fülek_SelectedIndexChanged);
            // 
            // TPElrendelés
            // 
            this.TPElrendelés.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.TPElrendelés.Controls.Add(this.Chkelrendelés);
            this.TPElrendelés.Controls.Add(this.CMBStátus);
            this.TPElrendelés.Controls.Add(this.CmbGyakoriság);
            this.TPElrendelés.Controls.Add(this.Btnfrissít);
            this.TPElrendelés.Controls.Add(this.CmbKategória);
            this.TPElrendelés.Controls.Add(this.OktDátum);
            this.TPElrendelés.Controls.Add(this.Label3);
            this.TPElrendelés.Controls.Add(this.BtnElrendelés);
            this.TPElrendelés.Location = new System.Drawing.Point(4, 29);
            this.TPElrendelés.Name = "TPElrendelés";
            this.TPElrendelés.Padding = new System.Windows.Forms.Padding(3);
            this.TPElrendelés.Size = new System.Drawing.Size(926, 182);
            this.TPElrendelés.TabIndex = 0;
            this.TPElrendelés.Text = "Elrendelés";
            // 
            // Chkelrendelés
            // 
            this.Chkelrendelés.AutoSize = true;
            this.Chkelrendelés.Location = new System.Drawing.Point(50, 146);
            this.Chkelrendelés.Name = "Chkelrendelés";
            this.Chkelrendelés.Size = new System.Drawing.Size(103, 24);
            this.Chkelrendelés.TabIndex = 75;
            this.Chkelrendelés.Text = "Elrendelés";
            this.Chkelrendelés.UseVisualStyleBackColor = true;
            this.Chkelrendelés.Visible = false;
            // 
            // CMBStátus
            // 
            this.CMBStátus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CMBStátus.FormattingEnabled = true;
            this.CMBStátus.Location = new System.Drawing.Point(522, 18);
            this.CMBStátus.Name = "CMBStátus";
            this.CMBStátus.Size = new System.Drawing.Size(212, 28);
            this.CMBStátus.TabIndex = 70;
            // 
            // CmbGyakoriság
            // 
            this.CmbGyakoriság.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbGyakoriság.FormattingEnabled = true;
            this.CmbGyakoriság.Location = new System.Drawing.Point(232, 18);
            this.CmbGyakoriság.Name = "CmbGyakoriság";
            this.CmbGyakoriság.Size = new System.Drawing.Size(284, 28);
            this.CmbGyakoriság.TabIndex = 69;
            // 
            // CmbKategória
            // 
            this.CmbKategória.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbKategória.FormattingEnabled = true;
            this.CmbKategória.Location = new System.Drawing.Point(6, 18);
            this.CmbKategória.Name = "CmbKategória";
            this.CmbKategória.Size = new System.Drawing.Size(220, 28);
            this.CmbKategória.TabIndex = 67;
            // 
            // OktDátum
            // 
            this.OktDátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.OktDátum.Location = new System.Drawing.Point(124, 84);
            this.OktDátum.Name = "OktDátum";
            this.OktDátum.Size = new System.Drawing.Size(107, 26);
            this.OktDátum.TabIndex = 50;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(11, 90);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(107, 20);
            this.Label3.TabIndex = 49;
            this.Label3.Text = "Kezdő dátum:";
            // 
            // TPOktatandó
            // 
            this.TPOktatandó.BackColor = System.Drawing.Color.LightSeaGreen;
            this.TPOktatandó.Controls.Add(this.Oktatás_Panel);
            this.TPOktatandó.Controls.Add(this.Oktataandó_Választó);
            this.TPOktatandó.Controls.Add(this.CMBoktatástárgya);
            this.TPOktatandó.Controls.Add(this.Label6);
            this.TPOktatandó.Controls.Add(this.BtnOktatásFrissít);
            this.TPOktatandó.Location = new System.Drawing.Point(4, 29);
            this.TPOktatandó.Name = "TPOktatandó";
            this.TPOktatandó.Size = new System.Drawing.Size(926, 182);
            this.TPOktatandó.TabIndex = 2;
            this.TPOktatandó.Text = "Oktatandó";
            // 
            // Oktatás_Panel
            // 
            this.Oktatás_Panel.Controls.Add(this.Label2);
            this.Oktatás_Panel.Controls.Add(this.Lejáródátum);
            this.Oktatás_Panel.Controls.Add(this.Label10);
            this.Oktatás_Panel.Controls.Add(this.Kötelezésmód);
            this.Oktatás_Panel.Controls.Add(this.TörölKötelezés);
            this.Oktatás_Panel.Controls.Add(this.Label7);
            this.Oktatás_Panel.Controls.Add(this.Átütemezés);
            this.Oktatás_Panel.Location = new System.Drawing.Point(8, 53);
            this.Oktatás_Panel.Name = "Oktatás_Panel";
            this.Oktatás_Panel.Size = new System.Drawing.Size(566, 126);
            this.Oktatás_Panel.TabIndex = 93;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(6, 11);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(131, 20);
            this.Label2.TabIndex = 63;
            this.Label2.Text = "Lejáró oktatások:";
            // 
            // Lejáródátum
            // 
            this.Lejáródátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Lejáródátum.Location = new System.Drawing.Point(168, 6);
            this.Lejáródátum.Name = "Lejáródátum";
            this.Lejáródátum.Size = new System.Drawing.Size(107, 26);
            this.Lejáródátum.TabIndex = 64;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(372, 97);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(135, 20);
            this.Label10.TabIndex = 90;
            this.Label10.Text = "Kötelezés törlése:";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(6, 98);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(82, 20);
            this.Label7.TabIndex = 89;
            this.Label7.Text = "Átütemez:";
            // 
            // Átütemezés
            // 
            this.Átütemezés.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Átütemezés.Location = new System.Drawing.Point(168, 92);
            this.Átütemezés.Name = "Átütemezés";
            this.Átütemezés.Size = new System.Drawing.Size(107, 26);
            this.Átütemezés.TabIndex = 88;
            // 
            // Oktataandó_Választó
            // 
            this.Oktataandó_Választó.AutoSize = true;
            this.Oktataandó_Választó.Location = new System.Drawing.Point(589, 64);
            this.Oktataandó_Választó.Name = "Oktataandó_Választó";
            this.Oktataandó_Választó.Size = new System.Drawing.Size(250, 24);
            this.Oktataandó_Választó.TabIndex = 92;
            this.Oktataandó_Választó.Text = "Oktatás szervezés/ Lekérdezés";
            this.Oktataandó_Választó.UseVisualStyleBackColor = true;
            this.Oktataandó_Választó.CheckedChanged += new System.EventHandler(this.Oktataandó_Választó_CheckedChanged);
            // 
            // CMBoktatástárgya
            // 
            this.CMBoktatástárgya.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CMBoktatástárgya.FormattingEnabled = true;
            this.CMBoktatástárgya.Location = new System.Drawing.Point(176, 19);
            this.CMBoktatástárgya.Name = "CMBoktatástárgya";
            this.CMBoktatástárgya.Size = new System.Drawing.Size(619, 28);
            this.CMBoktatástárgya.TabIndex = 70;
            this.CMBoktatástárgya.SelectedIndexChanged += new System.EventHandler(this.CMBoktatástárgya_SelectedIndexChanged);
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(14, 22);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(156, 20);
            this.Label6.TabIndex = 65;
            this.Label6.Text = "Oktatás tárgya szűrő";
            // 
            // TPAdmin
            // 
            this.TPAdmin.BackColor = System.Drawing.Color.PaleTurquoise;
            this.TPAdmin.Controls.Add(this.Label22);
            this.TPAdmin.Controls.Add(this.Txtemail);
            this.TPAdmin.Controls.Add(this.TextBox2);
            this.TPAdmin.Controls.Add(this.TextBox1);
            this.TPAdmin.Controls.Add(this.BtnEmailKüldés);
            this.TPAdmin.Controls.Add(this.Egyébszöveg);
            this.TPAdmin.Controls.Add(this.Label21);
            this.TPAdmin.Controls.Add(this.Adminhelyszín);
            this.TPAdmin.Controls.Add(this.Label20);
            this.TPAdmin.Controls.Add(this.BtnAdminMentés);
            this.TPAdmin.Controls.Add(this.Admintematika);
            this.TPAdmin.Controls.Add(this.BtnJelenléti);
            this.TPAdmin.Controls.Add(this.Label19);
            this.TPAdmin.Controls.Add(this.Admintartam);
            this.TPAdmin.Controls.Add(this.Adminoktatástárgya);
            this.TPAdmin.Controls.Add(this.Label18);
            this.TPAdmin.Controls.Add(this.AdminOktatómunkaköre);
            this.TPAdmin.Controls.Add(this.Label17);
            this.TPAdmin.Controls.Add(this.Label16);
            this.TPAdmin.Controls.Add(this.AdminOktatásoka);
            this.TPAdmin.Controls.Add(this.Label15);
            this.TPAdmin.Controls.Add(this.Label12);
            this.TPAdmin.Controls.Add(this.AdminOktató);
            this.TPAdmin.Controls.Add(this.Adminoktatásdátuma);
            this.TPAdmin.Controls.Add(this.Label14);
            this.TPAdmin.Controls.Add(this.Button10);
            this.TPAdmin.Controls.Add(this.Button9);
            this.TPAdmin.Location = new System.Drawing.Point(4, 29);
            this.TPAdmin.Name = "TPAdmin";
            this.TPAdmin.Padding = new System.Windows.Forms.Padding(3);
            this.TPAdmin.Size = new System.Drawing.Size(926, 182);
            this.TPAdmin.TabIndex = 1;
            this.TPAdmin.Text = "Adminisztráció";
            // 
            // Label22
            // 
            this.Label22.AutoSize = true;
            this.Label22.Location = new System.Drawing.Point(3, 185);
            this.Label22.Name = "Label22";
            this.Label22.Size = new System.Drawing.Size(217, 20);
            this.Label22.TabIndex = 109;
            this.Label22.Text = "FAR továbbítási e-mail címek:";
            // 
            // TextBox2
            // 
            this.TextBox2.Location = new System.Drawing.Point(618, 76);
            this.TextBox2.Multiline = true;
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.Size = new System.Drawing.Size(30, 21);
            this.TextBox2.TabIndex = 105;
            this.TextBox2.Visible = false;
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(702, 76);
            this.TextBox1.Multiline = true;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(28, 21);
            this.TextBox1.TabIndex = 104;
            this.TextBox1.Visible = false;
            // 
            // Egyébszöveg
            // 
            this.Egyébszöveg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Egyébszöveg.Location = new System.Drawing.Point(0, 239);
            this.Egyébszöveg.Name = "Egyébszöveg";
            this.Egyébszöveg.Size = new System.Drawing.Size(923, 68);
            this.Egyébszöveg.TabIndex = 102;
            this.Egyébszöveg.Text = "";
            // 
            // Label21
            // 
            this.Label21.AutoSize = true;
            this.Label21.Location = new System.Drawing.Point(3, 216);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(183, 20);
            this.Label21.TabIndex = 101;
            this.Label21.Text = "Egyéb kiegészítő szöveg";
            // 
            // Adminhelyszín
            // 
            this.Adminhelyszín.Location = new System.Drawing.Point(143, 145);
            this.Adminhelyszín.Name = "Adminhelyszín";
            this.Adminhelyszín.Size = new System.Drawing.Size(487, 26);
            this.Adminhelyszín.TabIndex = 100;
            // 
            // Label20
            // 
            this.Label20.AutoSize = true;
            this.Label20.Location = new System.Drawing.Point(3, 148);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(126, 20);
            this.Label20.TabIndex = 99;
            this.Label20.Text = "Helye, helyszíne:";
            // 
            // Admintematika
            // 
            this.Admintematika.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Admintematika.Location = new System.Drawing.Point(2, 332);
            this.Admintematika.Name = "Admintematika";
            this.Admintematika.Size = new System.Drawing.Size(923, 0);
            this.Admintematika.TabIndex = 93;
            this.Admintematika.Text = "";
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.Location = new System.Drawing.Point(3, 309);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(137, 20);
            this.Label19.TabIndex = 91;
            this.Label19.Text = "Oktatási tematika:";
            // 
            // Admintartam
            // 
            this.Admintartam.Location = new System.Drawing.Point(439, 76);
            this.Admintartam.Name = "Admintartam";
            this.Admintartam.Size = new System.Drawing.Size(116, 26);
            this.Admintartam.TabIndex = 90;
            // 
            // Adminoktatástárgya
            // 
            this.Adminoktatástárgya.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Adminoktatástárgya.FormattingEnabled = true;
            this.Adminoktatástárgya.Location = new System.Drawing.Point(143, 6);
            this.Adminoktatástárgya.Name = "Adminoktatástárgya";
            this.Adminoktatástárgya.Size = new System.Drawing.Size(401, 28);
            this.Adminoktatástárgya.TabIndex = 89;
            this.Adminoktatástárgya.SelectedIndexChanged += new System.EventHandler(this.Adminoktatástárgya_SelectedIndexChanged);
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.Location = new System.Drawing.Point(3, 12);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(52, 20);
            this.Label18.TabIndex = 88;
            this.Label18.Text = "Tárgy:";
            // 
            // AdminOktatómunkaköre
            // 
            this.AdminOktatómunkaköre.Location = new System.Drawing.Point(582, 111);
            this.AdminOktatómunkaköre.Name = "AdminOktatómunkaköre";
            this.AdminOktatómunkaköre.Size = new System.Drawing.Size(338, 26);
            this.AdminOktatómunkaköre.TabIndex = 87;
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.Location = new System.Drawing.Point(284, 83);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(149, 20);
            this.Label17.TabIndex = 86;
            this.Label17.Text = "Oktatás időtartama:\t";
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Location = new System.Drawing.Point(478, 114);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(92, 20);
            this.Label16.TabIndex = 85;
            this.Label16.Text = "Munkaköre:";
            // 
            // AdminOktatásoka
            // 
            this.AdminOktatásoka.Location = new System.Drawing.Point(143, 40);
            this.AdminOktatásoka.Name = "AdminOktatásoka";
            this.AdminOktatásoka.Size = new System.Drawing.Size(401, 26);
            this.AdminOktatásoka.TabIndex = 84;
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Location = new System.Drawing.Point(3, 43);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(99, 20);
            this.Label15.TabIndex = 83;
            this.Label15.Text = "Oktatás oka:";
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(3, 114);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(57, 20);
            this.Label12.TabIndex = 82;
            this.Label12.Text = "Oktató";
            // 
            // AdminOktató
            // 
            this.AdminOktató.FormattingEnabled = true;
            this.AdminOktató.Location = new System.Drawing.Point(143, 111);
            this.AdminOktató.Name = "AdminOktató";
            this.AdminOktató.Size = new System.Drawing.Size(329, 28);
            this.AdminOktató.TabIndex = 81;
            // 
            // Adminoktatásdátuma
            // 
            this.Adminoktatásdátuma.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Adminoktatásdátuma.Location = new System.Drawing.Point(143, 77);
            this.Adminoktatásdátuma.Name = "Adminoktatásdátuma";
            this.Adminoktatásdátuma.Size = new System.Drawing.Size(107, 26);
            this.Adminoktatásdátuma.TabIndex = 80;
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(3, 82);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(130, 20);
            this.Label14.TabIndex = 79;
            this.Label14.Text = "Oktatás Dátuma:";
            // 
            // TPOktatásRögz
            // 
            this.TPOktatásRögz.BackColor = System.Drawing.Color.LightSalmon;
            this.TPOktatásRögz.Controls.Add(this.Megjegyzés);
            this.TPOktatásRögz.Controls.Add(this.Label11);
            this.TPOktatásRögz.Controls.Add(this.CHKpdfvan);
            this.TPOktatásRögz.Controls.Add(this.Label5);
            this.TPOktatásRögz.Controls.Add(this.CMBszámon);
            this.TPOktatásRögz.Controls.Add(this.Label4);
            this.TPOktatásRögz.Controls.Add(this.LSToktató);
            this.TPOktatásRögz.Controls.Add(this.BizDátum);
            this.TPOktatásRögz.Controls.Add(this.Label1);
            this.TPOktatásRögz.Controls.Add(this.Chkoktat);
            this.TPOktatásRögz.Controls.Add(this.Txtmegnyitott);
            this.TPOktatásRögz.Controls.Add(this.Txtmentett);
            this.TPOktatásRögz.Controls.Add(this.BtnOktatásEredményTöröl);
            this.TPOktatásRögz.Controls.Add(this.BtnPdfÚjHasznál);
            this.TPOktatásRögz.Controls.Add(this.BtnPDFsave);
            this.TPOktatásRögz.Controls.Add(this.BtnPdfMegnyitás);
            this.TPOktatásRögz.Location = new System.Drawing.Point(4, 29);
            this.TPOktatásRögz.Name = "TPOktatásRögz";
            this.TPOktatásRögz.Size = new System.Drawing.Size(926, 182);
            this.TPOktatásRögz.TabIndex = 3;
            this.TPOktatásRögz.Text = "Oktatás Rögzítése";
            // 
            // Megjegyzés
            // 
            this.Megjegyzés.Location = new System.Drawing.Point(393, 145);
            this.Megjegyzés.Name = "Megjegyzés";
            this.Megjegyzés.Size = new System.Drawing.Size(511, 26);
            this.Megjegyzés.TabIndex = 85;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(389, 115);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(188, 20);
            this.Label11.TabIndex = 84;
            this.Label11.Text = "Megjegyzés/ Tárolási hely";
            // 
            // CHKpdfvan
            // 
            this.CHKpdfvan.AutoSize = true;
            this.CHKpdfvan.Enabled = false;
            this.CHKpdfvan.Location = new System.Drawing.Point(64, 89);
            this.CHKpdfvan.Name = "CHKpdfvan";
            this.CHKpdfvan.Size = new System.Drawing.Size(89, 24);
            this.CHKpdfvan.TabIndex = 82;
            this.CHKpdfvan.Text = "PDF van";
            this.CHKpdfvan.UseVisualStyleBackColor = true;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(389, 78);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(107, 20);
            this.Label5.TabIndex = 80;
            this.Label5.Text = "Számonkérés";
            // 
            // CMBszámon
            // 
            this.CMBszámon.BackColor = System.Drawing.Color.White;
            this.CMBszámon.FormattingEnabled = true;
            this.CMBszámon.Location = new System.Drawing.Point(525, 75);
            this.CMBszámon.Name = "CMBszámon";
            this.CMBszámon.Size = new System.Drawing.Size(185, 28);
            this.CMBszámon.TabIndex = 79;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(389, 46);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(57, 20);
            this.Label4.TabIndex = 78;
            this.Label4.Text = "Oktató";
            // 
            // LSToktató
            // 
            this.LSToktató.BackColor = System.Drawing.Color.White;
            this.LSToktató.FormattingEnabled = true;
            this.LSToktató.Location = new System.Drawing.Point(525, 41);
            this.LSToktató.Name = "LSToktató";
            this.LSToktató.Size = new System.Drawing.Size(329, 28);
            this.LSToktató.TabIndex = 77;
            // 
            // BizDátum
            // 
            this.BizDátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.BizDátum.Location = new System.Drawing.Point(525, 11);
            this.BizDátum.Name = "BizDátum";
            this.BizDátum.Size = new System.Drawing.Size(107, 26);
            this.BizDátum.TabIndex = 76;
            this.BizDátum.ValueChanged += new System.EventHandler(this.BizDátum_ValueChanged);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(389, 11);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(130, 20);
            this.Label1.TabIndex = 75;
            this.Label1.Text = "Oktatás Dátuma:";
            // 
            // Chkoktat
            // 
            this.Chkoktat.AutoSize = true;
            this.Chkoktat.Location = new System.Drawing.Point(178, 89);
            this.Chkoktat.Name = "Chkoktat";
            this.Chkoktat.Size = new System.Drawing.Size(103, 24);
            this.Chkoktat.TabIndex = 74;
            this.Chkoktat.Text = "Oktatandó";
            this.Chkoktat.UseVisualStyleBackColor = true;
            this.Chkoktat.Visible = false;
            // 
            // Txtmegnyitott
            // 
            this.Txtmegnyitott.Enabled = false;
            this.Txtmegnyitott.Location = new System.Drawing.Point(64, 54);
            this.Txtmegnyitott.Name = "Txtmegnyitott";
            this.Txtmegnyitott.Size = new System.Drawing.Size(281, 26);
            this.Txtmegnyitott.TabIndex = 73;
            // 
            // Txtmentett
            // 
            this.Txtmentett.Enabled = false;
            this.Txtmentett.Location = new System.Drawing.Point(64, 22);
            this.Txtmentett.Name = "Txtmentett";
            this.Txtmentett.Size = new System.Drawing.Size(281, 26);
            this.Txtmentett.TabIndex = 72;
            // 
            // TPRögzítésekNaplóz
            // 
            this.TPRögzítésekNaplóz.BackColor = System.Drawing.Color.LightSteelBlue;
            this.TPRögzítésekNaplóz.Controls.Add(this.CHkNapló);
            this.TPRögzítésekNaplóz.Controls.Add(this.Cmboktatásrögz);
            this.TPRögzítésekNaplóz.Controls.Add(this.Label9);
            this.TPRögzítésekNaplóz.Controls.Add(this.Dátumig);
            this.TPRögzítésekNaplóz.Controls.Add(this.Dátumtól);
            this.TPRögzítésekNaplóz.Controls.Add(this.Label8);
            this.TPRögzítésekNaplóz.Controls.Add(this.BtnNaplózásEredményTöröl);
            this.TPRögzítésekNaplóz.Controls.Add(this.BtnRögzítFrissít);
            this.TPRögzítésekNaplóz.Location = new System.Drawing.Point(4, 29);
            this.TPRögzítésekNaplóz.Name = "TPRögzítésekNaplóz";
            this.TPRögzítésekNaplóz.Size = new System.Drawing.Size(926, 182);
            this.TPRögzítésekNaplóz.TabIndex = 4;
            this.TPRögzítésekNaplóz.Text = "Rögzítések naplózása";
            // 
            // CHkNapló
            // 
            this.CHkNapló.AutoSize = true;
            this.CHkNapló.Location = new System.Drawing.Point(16, 103);
            this.CHkNapló.Name = "CHkNapló";
            this.CHkNapló.Size = new System.Drawing.Size(69, 24);
            this.CHkNapló.TabIndex = 87;
            this.CHkNapló.Text = "Napló";
            this.CHkNapló.UseVisualStyleBackColor = true;
            this.CHkNapló.Visible = false;
            // 
            // Cmboktatásrögz
            // 
            this.Cmboktatásrögz.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmboktatásrögz.FormattingEnabled = true;
            this.Cmboktatásrögz.Location = new System.Drawing.Point(174, 54);
            this.Cmboktatásrögz.Name = "Cmboktatásrögz";
            this.Cmboktatásrögz.Size = new System.Drawing.Size(401, 28);
            this.Cmboktatásrögz.TabIndex = 85;
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(12, 62);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(156, 20);
            this.Label9.TabIndex = 84;
            this.Label9.Text = "Oktatás tárgya szűrő";
            // 
            // Dátumig
            // 
            this.Dátumig.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátumig.Location = new System.Drawing.Point(302, 7);
            this.Dátumig.Name = "Dátumig";
            this.Dátumig.Size = new System.Drawing.Size(107, 26);
            this.Dátumig.TabIndex = 69;
            // 
            // Dátumtól
            // 
            this.Dátumtól.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátumtól.Location = new System.Drawing.Point(174, 7);
            this.Dátumtól.MinDate = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.Dátumtól.Name = "Dátumtól";
            this.Dátumtól.Size = new System.Drawing.Size(107, 26);
            this.Dátumtól.TabIndex = 68;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(12, 12);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(134, 20);
            this.Label8.TabIndex = 67;
            this.Label8.Text = "Rögzítés dátuma:";
            // 
            // TabPage6
            // 
            this.TabPage6.BackColor = System.Drawing.Color.LightGreen;
            this.TabPage6.Controls.Add(this.PDF_néző);
            this.TabPage6.Controls.Add(this.BtnPdfNyit);
            this.TabPage6.Controls.Add(this.Button3);
            this.TabPage6.Location = new System.Drawing.Point(4, 29);
            this.TabPage6.Name = "TabPage6";
            this.TabPage6.Size = new System.Drawing.Size(926, 182);
            this.TabPage6.TabIndex = 5;
            this.TabPage6.Text = "PDF";
            // 
            // PDF_néző
            // 
            this.PDF_néző.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PDF_néző.Location = new System.Drawing.Point(0, 47);
            this.PDF_néző.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PDF_néző.Name = "PDF_néző";
            this.PDF_néző.ShowToolbar = false;
            this.PDF_néző.Size = new System.Drawing.Size(926, 435);
            this.PDF_néző.TabIndex = 67;
            this.PDF_néző.ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitWidth;
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(30, 200);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(1245, 20);
            this.Holtart.TabIndex = 70;
            // 
            // Ablak_Oktatások
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.ForestGreen;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1295, 579);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.BtnExcelkimenet);
            this.Controls.Add(this.Fülek);
            this.Controls.Add(this.TáblaOktatás);
            this.Controls.Add(this.BtnKijelölésátjelöl);
            this.Controls.Add(this.Btnkilelöltörlés);
            this.Controls.Add(this.Btnmindkijelöl);
            this.Controls.Add(this.Btnkijelöléstöröl);
            this.Controls.Add(this.BtnKijelölcsop);
            this.Controls.Add(this.ChkDolgozónév);
            this.Controls.Add(this.ChkCsoport);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.Panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Oktatások";
            this.Text = "Oktatások Nyilvántartása";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AblakOktatások_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TáblaOktatás)).EndInit();
            this.Fülek.ResumeLayout(false);
            this.TPElrendelés.ResumeLayout(false);
            this.TPElrendelés.PerformLayout();
            this.TPOktatandó.ResumeLayout(false);
            this.TPOktatandó.PerformLayout();
            this.Oktatás_Panel.ResumeLayout(false);
            this.Oktatás_Panel.PerformLayout();
            this.TPAdmin.ResumeLayout(false);
            this.TPAdmin.PerformLayout();
            this.TPOktatásRögz.ResumeLayout(false);
            this.TPOktatásRögz.PerformLayout();
            this.TPRögzítésekNaplóz.ResumeLayout(false);
            this.TPRögzítésekNaplóz.PerformLayout();
            this.TabPage6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        internal Button BtnSúgó;
        internal Panel Panel1;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal Button BtnKijelölésátjelöl;
        internal Button Btnkilelöltörlés;
        internal Button Btnmindkijelöl;
        internal Button Btnkijelöléstöröl;
        internal Button BtnKijelölcsop;
        internal CheckedListBox ChkDolgozónév;
        internal CheckedListBox ChkCsoport;
        internal DataGridView TáblaOktatás;
        internal ToolTip ToolTip1;
        internal TabControl Fülek;
        internal TabPage TPElrendelés;
        internal TabPage TPAdmin;
        internal TabPage TPOktatandó;
        internal TabPage TPOktatásRögz;
        internal Button BtnElrendelés;
        internal DateTimePicker OktDátum;
        internal Label Label3;
        internal Button BtnOktatásFrissít;
        internal ComboBox CMBStátus;
        internal ComboBox CmbGyakoriság;
        internal Button Btnfrissít;
        internal ComboBox CmbKategória;
        internal Button BtnPDFsave;
        internal Button BtnPdfMegnyitás;
        internal TextBox Txtmegnyitott;
        internal TextBox Txtmentett;
        internal CheckBox Chkoktat;
        internal DateTimePicker BizDátum;
        internal Label Label1;
        internal DateTimePicker Lejáródátum;
        internal Label Label2;
        internal Label Label4;
        internal ComboBox LSToktató;
        internal Label Label5;
        internal ComboBox CMBszámon;
        internal Label Label6;
        internal ComboBox CMBoktatástárgya;
        internal Button BtnPdfÚjHasznál;
        internal CheckBox CHKpdfvan;
        internal TabPage TPRögzítésekNaplóz;
        internal DateTimePicker Dátumig;
        internal DateTimePicker Dátumtól;
        internal Label Label8;
        internal Button BtnRögzítFrissít;
        internal ComboBox Cmboktatásrögz;
        internal Label Label9;
        internal Button BtnExcelkimenet;
        internal Button BtnNaplózásEredményTöröl;
        internal CheckBox CHkNapló;
        internal CheckBox Chkelrendelés;
        internal TabPage TabPage6;

        internal Button BtnPdfNyit;
        internal Button Button3;
        internal Label Label7;
        internal DateTimePicker Átütemezés;
        internal Button TörölKötelezés;
        internal Button Kötelezésmód;
        internal Label Label10;
        internal Button BtnOktatásEredményTöröl;
        internal Label Label11;
        internal TextBox Megjegyzés;
        internal Button Button10;
        internal Button Button9;
        internal TextBox Admintartam;
        internal ComboBox Adminoktatástárgya;
        internal Label Label18;
        internal TextBox AdminOktatómunkaköre;
        internal Label Label17;
        internal Label Label16;
        internal TextBox AdminOktatásoka;
        internal Label Label15;
        internal Label Label12;
        internal ComboBox AdminOktató;
        internal DateTimePicker Adminoktatásdátuma;
        internal Label Label14;
        internal Button BtnJelenléti;
        internal Label Label19;
        internal Button BtnAdminMentés;
        internal RichTextBox Admintematika;
        internal TextBox Adminhelyszín;
        internal Label Label20;
        internal RichTextBox Egyébszöveg;
        internal Label Label21;
        internal Button BtnEmailKüldés;
        internal TextBox Txtemail;
        internal Label Label22;
        internal TextBox TextBox2;
        internal TextBox TextBox1;
        private PdfiumViewer.PdfViewer PDF_néző;
        private CheckBox Oktataandó_Választó;
        private Panel Oktatás_Panel;
        internal V_MindenEgyéb.MyProgressbar Holtart;
    }
}