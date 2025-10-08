using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{

    public partial class Ablak_alap_program_egyéb : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (components !=null))
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_alap_program_egyéb));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Fülek = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.FejlécBeolvasása = new System.Windows.Forms.Button();
            this.Label69 = new System.Windows.Forms.Label();
            this.SAPCsoport = new System.Windows.Forms.ComboBox();
            this.Label68 = new System.Windows.Forms.Label();
            this.Label67 = new System.Windows.Forms.Label();
            this.SAPOSzlopszám = new System.Windows.Forms.TextBox();
            this.Label60 = new System.Windows.Forms.Label();
            this.SAPFejléc = new System.Windows.Forms.TextBox();
            this.Változónév = new System.Windows.Forms.TextBox();
            this.SAPRögzít = new System.Windows.Forms.Button();
            this.SAPTöröl = new System.Windows.Forms.Button();
            this.SAPExcel = new System.Windows.Forms.Button();
            this.SAPFrissít = new System.Windows.Forms.Button();
            this.SAPTábla = new System.Windows.Forms.DataGridView();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.Osztály_Új = new System.Windows.Forms.Button();
            this.Osztályfrissít = new System.Windows.Forms.Button();
            this.OsztályRögzít = new System.Windows.Forms.Button();
            this.OsztályExcel = new System.Windows.Forms.Button();
            this.Használatban = new System.Windows.Forms.CheckBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Osztálymező = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Osztálynév = new System.Windows.Forms.TextBox();
            this.ID = new System.Windows.Forms.TextBox();
            this.TáblaOsztály = new System.Windows.Forms.DataGridView();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.Bizt_frissít = new System.Windows.Forms.Button();
            this.Label4 = new System.Windows.Forms.Label();
            this.Hova = new System.Windows.Forms.TextBox();
            this.Honnan = new System.Windows.Forms.TextBox();
            this.Honnan_rögzít = new System.Windows.Forms.Button();
            this.Hova_rögzít = new System.Windows.Forms.Button();
            this.Dátumtól = new System.Windows.Forms.DateTimePicker();
            this.Dátumig = new System.Windows.Forms.DateTimePicker();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.Mátrix_igazság = new System.Windows.Forms.ComboBox();
            this.Mátrix_fajtamásik = new System.Windows.Forms.ComboBox();
            this.Mátrix_tábla = new System.Windows.Forms.DataGridView();
            this.Mátrix_frissít = new System.Windows.Forms.Button();
            this.Mátrix_rögzít = new System.Windows.Forms.Button();
            this.Label17 = new System.Windows.Forms.Label();
            this.Label18 = new System.Windows.Forms.Label();
            this.Label19 = new System.Windows.Forms.Label();
            this.Mátrix_fajta = new System.Windows.Forms.ComboBox();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.Kötbér_tábla = new System.Windows.Forms.DataGridView();
            this.Kötbér_Frissít = new System.Windows.Forms.Button();
            this.Kötbér_Nem = new System.Windows.Forms.TextBox();
            this.Button4 = new System.Windows.Forms.Button();
            this.Label16 = new System.Windows.Forms.Label();
            this.Kötbér_pót = new System.Windows.Forms.TextBox();
            this.Label14 = new System.Windows.Forms.Label();
            this.LLabel1 = new System.Windows.Forms.Label();
            this.Kötbér_takarítási_fajta = new System.Windows.Forms.ComboBox();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.Tak_Ár_frissít = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.Szűr_Fajta = new System.Windows.Forms.ComboBox();
            this.Szűr_Típus = new System.Windows.Forms.ComboBox();
            this.Szűr_Napszak = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.Szűr_Érvényes = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Adatok_beolvasása = new System.Windows.Forms.Button();
            this.Beviteli_táblakészítés = new System.Windows.Forms.Button();
            this.Excel_tak = new System.Windows.Forms.Button();
            this.Tak_Ár_rögzítés = new System.Windows.Forms.Button();
            this.Tak_Új = new System.Windows.Forms.Button();
            this.VégeÁrRögzítés = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Label15 = new System.Windows.Forms.Label();
            this.Tak_id = new System.Windows.Forms.TextBox();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Tak_J_típus = new System.Windows.Forms.ComboBox();
            this.Tak_Napszak = new System.Windows.Forms.ComboBox();
            this.Tak_Érv_k = new System.Windows.Forms.DateTimePicker();
            this.Tak_érv_V = new System.Windows.Forms.DateTimePicker();
            this.Tak_Ár = new System.Windows.Forms.TextBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Tak_J_takarítási_fajta = new System.Windows.Forms.ComboBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Tak_Ár_Tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.email_tabla = new Zuby.ADGV.AdvancedDataGridView();
            this.Button13 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.Panel1.SuspendLayout();
            this.Fülek.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SAPTábla)).BeginInit();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TáblaOsztály)).BeginInit();
            this.TabPage3.SuspendLayout();
            this.TabPage4.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Mátrix_tábla)).BeginInit();
            this.GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Kötbér_tábla)).BeginInit();
            this.GroupBox1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tak_Ár_Tábla)).BeginInit();
            this.tabPage5.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.email_tabla)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.Cmbtelephely);
            this.Panel1.Controls.Add(this.Label13);
            this.Panel1.Location = new System.Drawing.Point(2, 2);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(373, 33);
            this.Panel1.TabIndex = 47;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(175, 2);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 33);
            this.Cmbtelephely.TabIndex = 18;
            this.Cmbtelephely.SelectionChangeCommitted += new System.EventHandler(this.Cmbtelephely_SelectionChangeCommitted);
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(3, 4);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(184, 25);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // Fülek
            // 
            this.Fülek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fülek.Controls.Add(this.TabPage1);
            this.Fülek.Controls.Add(this.TabPage2);
            this.Fülek.Controls.Add(this.TabPage3);
            this.Fülek.Controls.Add(this.TabPage4);
            this.Fülek.Controls.Add(this.tabPage5);
            this.Fülek.Location = new System.Drawing.Point(2, 60);
            this.Fülek.Name = "Fülek";
            this.Fülek.Padding = new System.Drawing.Point(16, 3);
            this.Fülek.SelectedIndex = 0;
            this.Fülek.Size = new System.Drawing.Size(1492, 522);
            this.Fülek.TabIndex = 54;
            this.Fülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Fülek_DrawItem);
            this.Fülek.SelectedIndexChanged += new System.EventHandler(this.Fülek_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.SandyBrown;
            this.TabPage1.Controls.Add(this.tableLayoutPanel4);
            this.TabPage1.Controls.Add(this.SAPTábla);
            this.TabPage1.Location = new System.Drawing.Point(4, 34);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1484, 484);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "SAP- FORTE beolvasás";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 6;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.FejlécBeolvasása, 5, 4);
            this.tableLayoutPanel4.Controls.Add(this.Label69, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.SAPCsoport, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.Label68, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.Label67, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.SAPOSzlopszám, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.Label60, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.SAPFejléc, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.Változónév, 1, 3);
            this.tableLayoutPanel4.Controls.Add(this.SAPRögzít, 4, 3);
            this.tableLayoutPanel4.Controls.Add(this.SAPTöröl, 2, 4);
            this.tableLayoutPanel4.Controls.Add(this.SAPExcel, 3, 4);
            this.tableLayoutPanel4.Controls.Add(this.SAPFrissít, 4, 4);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(6, 6);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 5;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(823, 211);
            this.tableLayoutPanel4.TabIndex = 113;
            // 
            // FejlécBeolvasása
            // 
            this.FejlécBeolvasása.BackColor = System.Drawing.SystemColors.Control;
            this.FejlécBeolvasása.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FejlécBeolvasása.Image = global::Villamos.Properties.Resources.Document_Microsoft_Excel_01;
            this.FejlécBeolvasása.Location = new System.Drawing.Point(809, 159);
            this.FejlécBeolvasása.Name = "FejlécBeolvasása";
            this.FejlécBeolvasása.Size = new System.Drawing.Size(45, 45);
            this.FejlécBeolvasása.TabIndex = 56;
            this.toolTip1.SetToolTip(this.FejlécBeolvasása, "Excel tábla alapján beolvassa a fejlécet");
            this.FejlécBeolvasása.UseVisualStyleBackColor = false;
            this.FejlécBeolvasása.Click += new System.EventHandler(this.FejlécBeolvasása_Click);
            // 
            // Label69
            // 
            this.Label69.AutoSize = true;
            this.Label69.Location = new System.Drawing.Point(3, 0);
            this.Label69.Name = "Label69";
            this.Label69.Size = new System.Drawing.Size(182, 25);
            this.Label69.TabIndex = 48;
            this.Label69.Text = "Beolvasási csoport:";
            // 
            // SAPCsoport
            // 
            this.SAPCsoport.FormattingEnabled = true;
            this.SAPCsoport.Location = new System.Drawing.Point(191, 3);
            this.SAPCsoport.MaxLength = 10;
            this.SAPCsoport.Name = "SAPCsoport";
            this.SAPCsoport.Size = new System.Drawing.Size(187, 33);
            this.SAPCsoport.TabIndex = 0;
            this.SAPCsoport.SelectedIndexChanged += new System.EventHandler(this.Csoport_SelectedIndexChanged);
            // 
            // Label68
            // 
            this.Label68.AutoSize = true;
            this.Label68.Location = new System.Drawing.Point(3, 35);
            this.Label68.Name = "Label68";
            this.Label68.Size = new System.Drawing.Size(143, 25);
            this.Label68.TabIndex = 53;
            this.Label68.Text = "Oszlop száma:";
            // 
            // Label67
            // 
            this.Label67.AutoSize = true;
            this.Label67.Location = new System.Drawing.Point(3, 105);
            this.Label67.Name = "Label67";
            this.Label67.Size = new System.Drawing.Size(116, 25);
            this.Label67.TabIndex = 55;
            this.Label67.Text = "Változónév:";
            // 
            // SAPOSzlopszám
            // 
            this.SAPOSzlopszám.Location = new System.Drawing.Point(191, 38);
            this.SAPOSzlopszám.Name = "SAPOSzlopszám";
            this.SAPOSzlopszám.Size = new System.Drawing.Size(187, 30);
            this.SAPOSzlopszám.TabIndex = 1;
            // 
            // Label60
            // 
            this.Label60.AutoSize = true;
            this.Label60.Location = new System.Drawing.Point(3, 70);
            this.Label60.Name = "Label60";
            this.Label60.Size = new System.Drawing.Size(149, 25);
            this.Label60.TabIndex = 54;
            this.Label60.Text = "Fejléc szövege:";
            // 
            // SAPFejléc
            // 
            this.SAPFejléc.Location = new System.Drawing.Point(191, 73);
            this.SAPFejléc.MaxLength = 255;
            this.SAPFejléc.Name = "SAPFejléc";
            this.SAPFejléc.Size = new System.Drawing.Size(459, 30);
            this.SAPFejléc.TabIndex = 2;
            // 
            // Változónév
            // 
            this.Változónév.Location = new System.Drawing.Point(191, 108);
            this.Változónév.MaxLength = 50;
            this.Változónév.Name = "Változónév";
            this.Változónév.Size = new System.Drawing.Size(187, 30);
            this.Változónév.TabIndex = 3;
            // 
            // SAPRögzít
            // 
            this.SAPRögzít.BackColor = System.Drawing.SystemColors.Control;
            this.SAPRögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SAPRögzít.Image = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.SAPRögzít.Location = new System.Drawing.Point(758, 108);
            this.SAPRögzít.Name = "SAPRögzít";
            this.SAPRögzít.Size = new System.Drawing.Size(45, 45);
            this.SAPRögzít.TabIndex = 4;
            this.toolTip1.SetToolTip(this.SAPRögzít, "Rögzíti az adatokat");
            this.SAPRögzít.UseVisualStyleBackColor = false;
            this.SAPRögzít.Click += new System.EventHandler(this.Rögzít_Click);
            // 
            // SAPTöröl
            // 
            this.SAPTöröl.BackColor = System.Drawing.SystemColors.Control;
            this.SAPTöröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SAPTöröl.Image = global::Villamos.Properties.Resources.Kuka;
            this.SAPTöröl.Location = new System.Drawing.Point(656, 159);
            this.SAPTöröl.Name = "SAPTöröl";
            this.SAPTöröl.Size = new System.Drawing.Size(45, 45);
            this.SAPTöröl.TabIndex = 5;
            this.toolTip1.SetToolTip(this.SAPTöröl, "Törli a megjelenített értékeket");
            this.SAPTöröl.UseVisualStyleBackColor = false;
            this.SAPTöröl.Click += new System.EventHandler(this.Töröl_Click);
            // 
            // SAPExcel
            // 
            this.SAPExcel.BackColor = System.Drawing.SystemColors.Control;
            this.SAPExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SAPExcel.Image = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.SAPExcel.Location = new System.Drawing.Point(707, 159);
            this.SAPExcel.Name = "SAPExcel";
            this.SAPExcel.Size = new System.Drawing.Size(45, 45);
            this.SAPExcel.TabIndex = 7;
            this.toolTip1.SetToolTip(this.SAPExcel, "Excel kimetetet készít");
            this.SAPExcel.UseVisualStyleBackColor = false;
            this.SAPExcel.Click += new System.EventHandler(this.Excel_Click);
            // 
            // SAPFrissít
            // 
            this.SAPFrissít.BackColor = System.Drawing.SystemColors.Control;
            this.SAPFrissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SAPFrissít.Image = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.SAPFrissít.Location = new System.Drawing.Point(758, 159);
            this.SAPFrissít.Name = "SAPFrissít";
            this.SAPFrissít.Size = new System.Drawing.Size(45, 45);
            this.SAPFrissít.TabIndex = 6;
            this.toolTip1.SetToolTip(this.SAPFrissít, "Táblázar adatait frissíti");
            this.SAPFrissít.UseVisualStyleBackColor = false;
            this.SAPFrissít.Click += new System.EventHandler(this.Command1_Click);
            // 
            // SAPTábla
            // 
            this.SAPTábla.AllowUserToAddRows = false;
            this.SAPTábla.AllowUserToDeleteRows = false;
            this.SAPTábla.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.SAPTábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.SAPTábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SAPTábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.SAPTábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SAPTábla.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.SAPTábla.EnableHeadersVisualStyles = false;
            this.SAPTábla.Location = new System.Drawing.Point(6, 229);
            this.SAPTábla.Name = "SAPTábla";
            this.SAPTábla.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SAPTábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.SAPTábla.RowHeadersWidth = 20;
            this.SAPTábla.Size = new System.Drawing.Size(1472, 254);
            this.SAPTábla.TabIndex = 112;
            this.SAPTábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SAPTábla_CellClick);
            this.SAPTábla.SelectionChanged += new System.EventHandler(this.Tábla_SelectionChanged);
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.SandyBrown;
            this.TabPage2.Controls.Add(this.Osztály_Új);
            this.TabPage2.Controls.Add(this.Osztályfrissít);
            this.TabPage2.Controls.Add(this.OsztályRögzít);
            this.TabPage2.Controls.Add(this.OsztályExcel);
            this.TabPage2.Controls.Add(this.Használatban);
            this.TabPage2.Controls.Add(this.Label1);
            this.TabPage2.Controls.Add(this.Label2);
            this.TabPage2.Controls.Add(this.Osztálymező);
            this.TabPage2.Controls.Add(this.Label3);
            this.TabPage2.Controls.Add(this.Osztálynév);
            this.TabPage2.Controls.Add(this.ID);
            this.TabPage2.Controls.Add(this.TáblaOsztály);
            this.TabPage2.Location = new System.Drawing.Point(4, 34);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(1484, 484);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Osztály elemek kezelése";
            // 
            // Osztály_Új
            // 
            this.Osztály_Új.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Osztály_Új.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Osztály_Új.Location = new System.Drawing.Point(762, 75);
            this.Osztály_Új.Name = "Osztály_Új";
            this.Osztály_Új.Size = new System.Drawing.Size(45, 45);
            this.Osztály_Új.TabIndex = 124;
            this.toolTip1.SetToolTip(this.Osztály_Új, "Új adathoz a beviteli mezőket törli");
            this.Osztály_Új.UseVisualStyleBackColor = true;
            this.Osztály_Új.Click += new System.EventHandler(this.Osztály_Új_Click);
            // 
            // Osztályfrissít
            // 
            this.Osztályfrissít.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Osztályfrissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Osztályfrissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Osztályfrissít.Location = new System.Drawing.Point(813, 75);
            this.Osztályfrissít.Name = "Osztályfrissít";
            this.Osztályfrissít.Size = new System.Drawing.Size(45, 45);
            this.Osztályfrissít.TabIndex = 123;
            this.toolTip1.SetToolTip(this.Osztályfrissít, "Frissíti a táblázatot");
            this.Osztályfrissít.UseVisualStyleBackColor = false;
            this.Osztályfrissít.Click += new System.EventHandler(this.Osztályfrissít_Click);
            // 
            // OsztályRögzít
            // 
            this.OsztályRögzít.BackColor = System.Drawing.Color.LightSeaGreen;
            this.OsztályRögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.OsztályRögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.OsztályRögzít.Location = new System.Drawing.Point(762, 12);
            this.OsztályRögzít.Name = "OsztályRögzít";
            this.OsztályRögzít.Size = new System.Drawing.Size(45, 45);
            this.OsztályRögzít.TabIndex = 121;
            this.toolTip1.SetToolTip(this.OsztályRögzít, "Rögzíti/módosítja az adatokat");
            this.OsztályRögzít.UseVisualStyleBackColor = false;
            this.OsztályRögzít.Click += new System.EventHandler(this.OsztályRögzít_Click);
            // 
            // OsztályExcel
            // 
            this.OsztályExcel.BackColor = System.Drawing.Color.LightSeaGreen;
            this.OsztályExcel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.OsztályExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.OsztályExcel.Location = new System.Drawing.Point(864, 75);
            this.OsztályExcel.Name = "OsztályExcel";
            this.OsztályExcel.Size = new System.Drawing.Size(45, 45);
            this.OsztályExcel.TabIndex = 120;
            this.toolTip1.SetToolTip(this.OsztályExcel, "Excel táblázatot készít a táblázatból");
            this.OsztályExcel.UseVisualStyleBackColor = false;
            this.OsztályExcel.Click += new System.EventHandler(this.OsztályExcel_Click);
            // 
            // Használatban
            // 
            this.Használatban.AutoSize = true;
            this.Használatban.Location = new System.Drawing.Point(152, 107);
            this.Használatban.Name = "Használatban";
            this.Használatban.Size = new System.Drawing.Size(154, 29);
            this.Használatban.TabIndex = 119;
            this.Használatban.Text = "Használatban";
            this.Használatban.UseVisualStyleBackColor = true;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(8, 79);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(126, 25);
            this.Label1.TabIndex = 118;
            this.Label1.Text = "Osztálymező";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(8, 12);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(174, 25);
            this.Label2.TabIndex = 116;
            this.Label2.Text = "Osztály sorszáma:";
            // 
            // Osztálymező
            // 
            this.Osztálymező.Enabled = false;
            this.Osztálymező.Location = new System.Drawing.Point(152, 75);
            this.Osztálymező.MaxLength = 50;
            this.Osztálymező.Name = "Osztálymező";
            this.Osztálymező.Size = new System.Drawing.Size(187, 30);
            this.Osztálymező.TabIndex = 115;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(8, 47);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(110, 25);
            this.Label3.TabIndex = 117;
            this.Label3.Text = "Osztálynév";
            // 
            // Osztálynév
            // 
            this.Osztálynév.Location = new System.Drawing.Point(152, 43);
            this.Osztálynév.MaxLength = 50;
            this.Osztálynév.Name = "Osztálynév";
            this.Osztálynév.Size = new System.Drawing.Size(459, 30);
            this.Osztálynév.TabIndex = 114;
            // 
            // ID
            // 
            this.ID.Enabled = false;
            this.ID.Location = new System.Drawing.Point(152, 12);
            this.ID.Name = "ID";
            this.ID.Size = new System.Drawing.Size(187, 30);
            this.ID.TabIndex = 113;
            // 
            // TáblaOsztály
            // 
            this.TáblaOsztály.AllowUserToAddRows = false;
            this.TáblaOsztály.AllowUserToDeleteRows = false;
            this.TáblaOsztály.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.TáblaOsztály.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.TáblaOsztály.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TáblaOsztály.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.TáblaOsztály.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TáblaOsztály.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.TáblaOsztály.EnableHeadersVisualStyles = false;
            this.TáblaOsztály.Location = new System.Drawing.Point(6, 144);
            this.TáblaOsztály.Name = "TáblaOsztály";
            this.TáblaOsztály.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TáblaOsztály.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.TáblaOsztály.RowHeadersWidth = 20;
            this.TáblaOsztály.Size = new System.Drawing.Size(1472, 339);
            this.TáblaOsztály.TabIndex = 112;
            this.TáblaOsztály.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TáblaOsztály_CellClick);
            this.TáblaOsztály.SelectionChanged += new System.EventHandler(this.TáblaOsztály_SelectionChanged);
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.LightSalmon;
            this.TabPage3.Controls.Add(this.Bizt_frissít);
            this.TabPage3.Controls.Add(this.Label4);
            this.TabPage3.Controls.Add(this.Hova);
            this.TabPage3.Controls.Add(this.Honnan);
            this.TabPage3.Controls.Add(this.Honnan_rögzít);
            this.TabPage3.Controls.Add(this.Hova_rögzít);
            this.TabPage3.Controls.Add(this.Dátumtól);
            this.TabPage3.Controls.Add(this.Dátumig);
            this.TabPage3.Controls.Add(this.Label8);
            this.TabPage3.Controls.Add(this.Label7);
            this.TabPage3.Location = new System.Drawing.Point(4, 34);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage3.Size = new System.Drawing.Size(1484, 484);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Biztonsági másolat készítés";
            // 
            // Bizt_frissít
            // 
            this.Bizt_frissít.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Bizt_frissít.BackgroundImage = global::Villamos.Properties.Resources.Designcontest_Ecommerce_Business_Save;
            this.Bizt_frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Bizt_frissít.Location = new System.Drawing.Point(1008, 111);
            this.Bizt_frissít.Name = "Bizt_frissít";
            this.Bizt_frissít.Size = new System.Drawing.Size(40, 40);
            this.Bizt_frissít.TabIndex = 124;
            this.toolTip1.SetToolTip(this.Bizt_frissít, "Mentést készít");
            this.Bizt_frissít.UseVisualStyleBackColor = false;
            this.Bizt_frissít.Click += new System.EventHandler(this.Bizt_frissít_Click);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Label4.Location = new System.Drawing.Point(8, 82);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(81, 25);
            this.Label4.TabIndex = 93;
            this.Label4.Text = "Honnan";
            // 
            // Hova
            // 
            this.Hova.Location = new System.Drawing.Point(137, 125);
            this.Hova.Name = "Hova";
            this.Hova.Size = new System.Drawing.Size(819, 30);
            this.Hova.TabIndex = 83;
            // 
            // Honnan
            // 
            this.Honnan.Location = new System.Drawing.Point(137, 79);
            this.Honnan.Name = "Honnan";
            this.Honnan.Size = new System.Drawing.Size(819, 30);
            this.Honnan.TabIndex = 82;
            // 
            // Honnan_rögzít
            // 
            this.Honnan_rögzít.BackgroundImage = global::Villamos.Properties.Resources.Folder_;
            this.Honnan_rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Honnan_rögzít.Location = new System.Drawing.Point(962, 65);
            this.Honnan_rögzít.Name = "Honnan_rögzít";
            this.Honnan_rögzít.Size = new System.Drawing.Size(40, 40);
            this.Honnan_rögzít.TabIndex = 81;
            this.toolTip1.SetToolTip(this.Honnan_rögzít, "Tallózza a fájl helyét");
            this.Honnan_rögzít.UseVisualStyleBackColor = true;
            this.Honnan_rögzít.Click += new System.EventHandler(this.Honnan_rögzít_Click);
            // 
            // Hova_rögzít
            // 
            this.Hova_rögzít.BackgroundImage = global::Villamos.Properties.Resources.Folder_;
            this.Hova_rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Hova_rögzít.Location = new System.Drawing.Point(962, 111);
            this.Hova_rögzít.Name = "Hova_rögzít";
            this.Hova_rögzít.Size = new System.Drawing.Size(40, 40);
            this.Hova_rögzít.TabIndex = 80;
            this.toolTip1.SetToolTip(this.Hova_rögzít, "Tallózza a fájl helyét");
            this.Hova_rögzít.UseVisualStyleBackColor = true;
            this.Hova_rögzít.Click += new System.EventHandler(this.Hova_rögzít_Click);
            // 
            // Dátumtól
            // 
            this.Dátumtól.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátumtól.Location = new System.Drawing.Point(137, 31);
            this.Dátumtól.Name = "Dátumtól";
            this.Dátumtól.Size = new System.Drawing.Size(114, 30);
            this.Dátumtól.TabIndex = 78;
            // 
            // Dátumig
            // 
            this.Dátumig.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátumig.Location = new System.Drawing.Point(257, 31);
            this.Dátumig.Name = "Dátumig";
            this.Dátumig.Size = new System.Drawing.Size(114, 30);
            this.Dátumig.TabIndex = 77;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Label8.Location = new System.Drawing.Point(8, 131);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(64, 25);
            this.Label8.TabIndex = 4;
            this.Label8.Text = "Hova:";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Label7.Location = new System.Drawing.Point(8, 37);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(142, 25);
            this.Label7.TabIndex = 3;
            this.Label7.Text = "Mettől-meddig:";
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.SystemColors.HotTrack;
            this.TabPage4.Controls.Add(this.GroupBox3);
            this.TabPage4.Controls.Add(this.GroupBox2);
            this.TabPage4.Controls.Add(this.GroupBox1);
            this.TabPage4.Location = new System.Drawing.Point(4, 34);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Size = new System.Drawing.Size(1484, 484);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Jármű Takarítás";
            // 
            // GroupBox3
            // 
            this.GroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.GroupBox3.BackColor = System.Drawing.SystemColors.Highlight;
            this.GroupBox3.Controls.Add(this.Mátrix_igazság);
            this.GroupBox3.Controls.Add(this.Mátrix_fajtamásik);
            this.GroupBox3.Controls.Add(this.Mátrix_tábla);
            this.GroupBox3.Controls.Add(this.Mátrix_frissít);
            this.GroupBox3.Controls.Add(this.Mátrix_rögzít);
            this.GroupBox3.Controls.Add(this.Label17);
            this.GroupBox3.Controls.Add(this.Label18);
            this.GroupBox3.Controls.Add(this.Label19);
            this.GroupBox3.Controls.Add(this.Mátrix_fajta);
            this.GroupBox3.Location = new System.Drawing.Point(1126, 5);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(350, 345);
            this.GroupBox3.TabIndex = 115;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "Járműtakarítás együtt takarítás";
            // 
            // Mátrix_igazság
            // 
            this.Mátrix_igazság.FormattingEnabled = true;
            this.Mátrix_igazság.Location = new System.Drawing.Point(169, 88);
            this.Mátrix_igazság.Name = "Mátrix_igazság";
            this.Mátrix_igazság.Size = new System.Drawing.Size(174, 33);
            this.Mátrix_igazság.TabIndex = 116;
            // 
            // Mátrix_fajtamásik
            // 
            this.Mátrix_fajtamásik.FormattingEnabled = true;
            this.Mátrix_fajtamásik.Location = new System.Drawing.Point(169, 56);
            this.Mátrix_fajtamásik.Name = "Mátrix_fajtamásik";
            this.Mátrix_fajtamásik.Size = new System.Drawing.Size(174, 33);
            this.Mátrix_fajtamásik.TabIndex = 115;
            // 
            // Mátrix_tábla
            // 
            this.Mátrix_tábla.AllowUserToAddRows = false;
            this.Mátrix_tábla.AllowUserToDeleteRows = false;
            this.Mátrix_tábla.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Mátrix_tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.Mátrix_tábla.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Mátrix_tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.Mátrix_tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Mátrix_tábla.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.Mátrix_tábla.EnableHeadersVisualStyles = false;
            this.Mátrix_tábla.Location = new System.Drawing.Point(6, 177);
            this.Mátrix_tábla.Name = "Mátrix_tábla";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Mátrix_tábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.Mátrix_tábla.RowHeadersVisible = false;
            this.Mátrix_tábla.RowHeadersWidth = 20;
            this.Mátrix_tábla.Size = new System.Drawing.Size(337, 158);
            this.Mátrix_tábla.TabIndex = 113;
            this.Mátrix_tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Mátrix_tábla_CellClick);
            // 
            // Mátrix_frissít
            // 
            this.Mátrix_frissít.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Mátrix_frissít.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Mátrix_frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Mátrix_frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Mátrix_frissít.Location = new System.Drawing.Point(10, 126);
            this.Mátrix_frissít.Name = "Mátrix_frissít";
            this.Mátrix_frissít.Size = new System.Drawing.Size(45, 45);
            this.Mátrix_frissít.TabIndex = 114;
            this.toolTip1.SetToolTip(this.Mátrix_frissít, "Frissíti a táblázatot");
            this.Mátrix_frissít.UseVisualStyleBackColor = true;
            this.Mátrix_frissít.Click += new System.EventHandler(this.Mátrix_frissít_Click);
            // 
            // Mátrix_rögzít
            // 
            this.Mátrix_rögzít.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Mátrix_rögzít.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Mátrix_rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Mátrix_rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Mátrix_rögzít.Location = new System.Drawing.Point(298, 126);
            this.Mátrix_rögzít.Name = "Mátrix_rögzít";
            this.Mátrix_rögzít.Size = new System.Drawing.Size(45, 45);
            this.Mátrix_rögzít.TabIndex = 113;
            this.toolTip1.SetToolTip(this.Mátrix_rögzít, "Rögzíti/módosítja az adatokat");
            this.Mátrix_rögzít.UseVisualStyleBackColor = true;
            this.Mátrix_rögzít.Click += new System.EventHandler(this.Mátrix_rögzít_Click);
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.Location = new System.Drawing.Point(6, 61);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(200, 25);
            this.Label17.TabIndex = 14;
            this.Label17.Text = "Takarítási fajta másik:";
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.Location = new System.Drawing.Point(6, 32);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(144, 25);
            this.Label18.TabIndex = 12;
            this.Label18.Text = "Takarítási fajta:";
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.Location = new System.Drawing.Point(6, 93);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(87, 25);
            this.Label19.TabIndex = 11;
            this.Label19.Text = "Igazság:";
            // 
            // Mátrix_fajta
            // 
            this.Mátrix_fajta.FormattingEnabled = true;
            this.Mátrix_fajta.Location = new System.Drawing.Point(169, 25);
            this.Mátrix_fajta.Name = "Mátrix_fajta";
            this.Mátrix_fajta.Size = new System.Drawing.Size(174, 33);
            this.Mátrix_fajta.TabIndex = 10;
            // 
            // GroupBox2
            // 
            this.GroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.GroupBox2.BackColor = System.Drawing.SystemColors.Highlight;
            this.GroupBox2.Controls.Add(this.Kötbér_tábla);
            this.GroupBox2.Controls.Add(this.Kötbér_Frissít);
            this.GroupBox2.Controls.Add(this.Kötbér_Nem);
            this.GroupBox2.Controls.Add(this.Button4);
            this.GroupBox2.Controls.Add(this.Label16);
            this.GroupBox2.Controls.Add(this.Kötbér_pót);
            this.GroupBox2.Controls.Add(this.Label14);
            this.GroupBox2.Controls.Add(this.LLabel1);
            this.GroupBox2.Controls.Add(this.Kötbér_takarítási_fajta);
            this.GroupBox2.Location = new System.Drawing.Point(795, 5);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(324, 345);
            this.GroupBox2.TabIndex = 1;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Járműtakarítás Kötbér";
            // 
            // Kötbér_tábla
            // 
            this.Kötbér_tábla.AllowUserToAddRows = false;
            this.Kötbér_tábla.AllowUserToDeleteRows = false;
            this.Kötbér_tábla.AllowUserToResizeRows = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Kötbér_tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.Kötbér_tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Kötbér_tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.Kötbér_tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Kötbér_tábla.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.Kötbér_tábla.EnableHeadersVisualStyles = false;
            this.Kötbér_tábla.Location = new System.Drawing.Point(4, 177);
            this.Kötbér_tábla.Name = "Kötbér_tábla";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Kötbér_tábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.Kötbér_tábla.RowHeadersVisible = false;
            this.Kötbér_tábla.RowHeadersWidth = 20;
            this.Kötbér_tábla.Size = new System.Drawing.Size(314, 158);
            this.Kötbér_tábla.TabIndex = 113;
            this.Kötbér_tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Kötbér_tábla_CellClick);
            // 
            // Kötbér_Frissít
            // 
            this.Kötbér_Frissít.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Kötbér_Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Kötbér_Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Kötbér_Frissít.Location = new System.Drawing.Point(8, 126);
            this.Kötbér_Frissít.Name = "Kötbér_Frissít";
            this.Kötbér_Frissít.Size = new System.Drawing.Size(45, 45);
            this.Kötbér_Frissít.TabIndex = 114;
            this.toolTip1.SetToolTip(this.Kötbér_Frissít, "Frissíti a táblázatot");
            this.Kötbér_Frissít.UseVisualStyleBackColor = true;
            this.Kötbér_Frissít.Click += new System.EventHandler(this.Kötbér_Frissít_Click);
            // 
            // Kötbér_Nem
            // 
            this.Kötbér_Nem.Location = new System.Drawing.Point(167, 58);
            this.Kötbér_Nem.Name = "Kötbér_Nem";
            this.Kötbér_Nem.Size = new System.Drawing.Size(151, 30);
            this.Kötbér_Nem.TabIndex = 15;
            // 
            // Button4
            // 
            this.Button4.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Button4.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button4.Location = new System.Drawing.Point(273, 126);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(45, 45);
            this.Button4.TabIndex = 113;
            this.toolTip1.SetToolTip(this.Button4, "Rögzíti/módosítja az adatokat");
            this.Button4.UseVisualStyleBackColor = true;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Location = new System.Drawing.Point(4, 61);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(205, 25);
            this.Label16.TabIndex = 14;
            this.Label16.Text = "Nem megfelelő szorzó";
            // 
            // Kötbér_pót
            // 
            this.Kötbér_pót.Location = new System.Drawing.Point(167, 90);
            this.Kötbér_pót.Name = "Kötbér_pót";
            this.Kötbér_pót.Size = new System.Drawing.Size(151, 30);
            this.Kötbér_pót.TabIndex = 13;
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(4, 32);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(144, 25);
            this.Label14.TabIndex = 12;
            this.Label14.Text = "Takarítási fajta:";
            // 
            // LLabel1
            // 
            this.LLabel1.AutoSize = true;
            this.LLabel1.Location = new System.Drawing.Point(4, 95);
            this.LLabel1.Name = "LLabel1";
            this.LLabel1.Size = new System.Drawing.Size(179, 25);
            this.LLabel1.TabIndex = 11;
            this.LLabel1.Text = "Pót határidő szorzó";
            // 
            // Kötbér_takarítási_fajta
            // 
            this.Kötbér_takarítási_fajta.FormattingEnabled = true;
            this.Kötbér_takarítási_fajta.Location = new System.Drawing.Point(167, 24);
            this.Kötbér_takarítási_fajta.Name = "Kötbér_takarítási_fajta";
            this.Kötbér_takarítási_fajta.Size = new System.Drawing.Size(151, 33);
            this.Kötbér_takarítási_fajta.TabIndex = 10;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.GroupBox1.BackColor = System.Drawing.SystemColors.Highlight;
            this.GroupBox1.Controls.Add(this.Tak_Ár_frissít);
            this.GroupBox1.Controls.Add(this.tableLayoutPanel3);
            this.GroupBox1.Controls.Add(this.tableLayoutPanel2);
            this.GroupBox1.Controls.Add(this.tableLayoutPanel1);
            this.GroupBox1.Controls.Add(this.Tak_Ár_Tábla);
            this.GroupBox1.Location = new System.Drawing.Point(2, 5);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(787, 345);
            this.GroupBox1.TabIndex = 0;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Takarítási árak";
            // 
            // Tak_Ár_frissít
            // 
            this.Tak_Ár_frissít.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Tak_Ár_frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Tak_Ár_frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Tak_Ár_frissít.Location = new System.Drawing.Point(530, 186);
            this.Tak_Ár_frissít.Name = "Tak_Ár_frissít";
            this.Tak_Ár_frissít.Size = new System.Drawing.Size(45, 45);
            this.Tak_Ár_frissít.TabIndex = 13;
            this.toolTip1.SetToolTip(this.Tak_Ár_frissít, "Frissíti a táblázatot");
            this.Tak_Ár_frissít.UseVisualStyleBackColor = true;
            this.Tak_Ár_frissít.Click += new System.EventHandler(this.Tak_Ár_frissít_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.Szűr_Fajta, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.Szűr_Típus, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.Szűr_Napszak, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label22, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label21, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label20, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.Szűr_Érvényes, 3, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(6, 177);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(518, 56);
            this.tableLayoutPanel3.TabIndex = 208;
            // 
            // Szűr_Fajta
            // 
            this.Szűr_Fajta.FormattingEnabled = true;
            this.Szűr_Fajta.Location = new System.Drawing.Point(3, 28);
            this.Szűr_Fajta.Name = "Szűr_Fajta";
            this.Szűr_Fajta.Size = new System.Drawing.Size(121, 33);
            this.Szűr_Fajta.TabIndex = 14;
            // 
            // Szűr_Típus
            // 
            this.Szűr_Típus.FormattingEnabled = true;
            this.Szűr_Típus.Location = new System.Drawing.Point(153, 28);
            this.Szűr_Típus.Name = "Szűr_Típus";
            this.Szűr_Típus.Size = new System.Drawing.Size(121, 33);
            this.Szűr_Típus.TabIndex = 13;
            // 
            // Szűr_Napszak
            // 
            this.Szűr_Napszak.FormattingEnabled = true;
            this.Szűr_Napszak.Location = new System.Drawing.Point(280, 28);
            this.Szűr_Napszak.Name = "Szűr_Napszak";
            this.Szűr_Napszak.Size = new System.Drawing.Size(121, 33);
            this.Szűr_Napszak.TabIndex = 12;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(280, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(95, 25);
            this.label22.TabIndex = 7;
            this.label22.Text = "Napszak:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(3, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(144, 25);
            this.label21.TabIndex = 6;
            this.label21.Text = "Takarítási fajta:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(153, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(119, 25);
            this.label20.TabIndex = 2;
            this.label20.Text = "Jármű típus:";
            // 
            // Szűr_Érvényes
            // 
            this.Szűr_Érvényes.AutoSize = true;
            this.Szűr_Érvényes.Checked = true;
            this.Szűr_Érvényes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Szűr_Érvényes.Location = new System.Drawing.Point(407, 28);
            this.Szűr_Érvényes.Name = "Szűr_Érvényes";
            this.Szűr_Érvényes.Size = new System.Drawing.Size(159, 29);
            this.Szűr_Érvényes.TabIndex = 15;
            this.Szűr_Érvényes.Text = "Érvényes árak";
            this.Szűr_Érvényes.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 12;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel2.Controls.Add(this.Adatok_beolvasása, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.Beviteli_táblakészítés, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.Excel_tak, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.Tak_Ár_rögzítés, 11, 0);
            this.tableLayoutPanel2.Controls.Add(this.Tak_Új, 10, 0);
            this.tableLayoutPanel2.Controls.Add(this.VégeÁrRögzítés, 7, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 123);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(661, 53);
            this.tableLayoutPanel2.TabIndex = 207;
            // 
            // Adatok_beolvasása
            // 
            this.Adatok_beolvasása.BackgroundImage = global::Villamos.Properties.Resources.Custom_Icon_Design_Flatastic_1_Import;
            this.Adatok_beolvasása.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Adatok_beolvasása.Location = new System.Drawing.Point(256, 3);
            this.Adatok_beolvasása.Name = "Adatok_beolvasása";
            this.Adatok_beolvasása.Size = new System.Drawing.Size(44, 45);
            this.Adatok_beolvasása.TabIndex = 204;
            this.toolTip1.SetToolTip(this.Adatok_beolvasása, "Beilleszti az elkészült Excel fájlt");
            this.Adatok_beolvasása.UseVisualStyleBackColor = true;
            this.Adatok_beolvasása.Click += new System.EventHandler(this.Adatok_beolvasása_Click);
            // 
            // Beviteli_táblakészítés
            // 
            this.Beviteli_táblakészítés.BackgroundImage = global::Villamos.Properties.Resources.Custom_Icon_Design_Flatastic_1_Export;
            this.Beviteli_táblakészítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Beviteli_táblakészítés.Location = new System.Drawing.Point(205, 3);
            this.Beviteli_táblakészítés.Name = "Beviteli_táblakészítés";
            this.Beviteli_táblakészítés.Size = new System.Drawing.Size(45, 45);
            this.Beviteli_táblakészítés.TabIndex = 205;
            this.toolTip1.SetToolTip(this.Beviteli_táblakészítés, "Beolvasás Excel táblába");
            this.Beviteli_táblakészítés.UseVisualStyleBackColor = true;
            this.Beviteli_táblakészítés.Click += new System.EventHandler(this.Beviteli_táblakészítés_Click);
            // 
            // Excel_tak
            // 
            this.Excel_tak.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Excel_tak.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excel_tak.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excel_tak.Location = new System.Drawing.Point(3, 3);
            this.Excel_tak.Name = "Excel_tak";
            this.Excel_tak.Size = new System.Drawing.Size(45, 45);
            this.Excel_tak.TabIndex = 115;
            this.toolTip1.SetToolTip(this.Excel_tak, "Excel táblázatot készít a táblázatból");
            this.Excel_tak.UseVisualStyleBackColor = true;
            this.Excel_tak.Click += new System.EventHandler(this.Excel_tak_Click);
            // 
            // Tak_Ár_rögzítés
            // 
            this.Tak_Ár_rögzítés.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Tak_Ár_rögzítés.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Tak_Ár_rögzítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Tak_Ár_rögzítés.Location = new System.Drawing.Point(613, 3);
            this.Tak_Ár_rögzítés.Name = "Tak_Ár_rögzítés";
            this.Tak_Ár_rögzítés.Size = new System.Drawing.Size(45, 45);
            this.Tak_Ár_rögzítés.TabIndex = 12;
            this.toolTip1.SetToolTip(this.Tak_Ár_rögzítés, "Rögzíti/módosítja az adatokat");
            this.Tak_Ár_rögzítés.UseVisualStyleBackColor = true;
            this.Tak_Ár_rögzítés.Click += new System.EventHandler(this.Tak_Ár_rögzítés_Click);
            // 
            // Tak_Új
            // 
            this.Tak_Új.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Tak_Új.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Tak_Új.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Tak_Új.Location = new System.Drawing.Point(562, 3);
            this.Tak_Új.Name = "Tak_Új";
            this.Tak_Új.Size = new System.Drawing.Size(45, 45);
            this.Tak_Új.TabIndex = 116;
            this.toolTip1.SetToolTip(this.Tak_Új, "Új adathoz a beviteli mezőket törli");
            this.Tak_Új.UseVisualStyleBackColor = true;
            this.Tak_Új.Click += new System.EventHandler(this.Tak_Új_Click);
            // 
            // VégeÁrRögzítés
            // 
            this.VégeÁrRögzítés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.VégeÁrRögzítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VégeÁrRögzítés.Location = new System.Drawing.Point(409, 3);
            this.VégeÁrRögzítés.Name = "VégeÁrRögzítés";
            this.VégeÁrRögzítés.Size = new System.Drawing.Size(44, 45);
            this.VégeÁrRögzítés.TabIndex = 206;
            this.toolTip1.SetToolTip(this.VégeÁrRögzítés, "Beállítja az érvényes ár végét a kijelölt tételeknél.");
            this.VégeÁrRögzítés.UseVisualStyleBackColor = true;
            this.VégeÁrRögzítés.Click += new System.EventHandler(this.VégeÁrRögzítés_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.Label15, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Tak_id, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Label10, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.Label6, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.Tak_J_típus, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.Tak_Napszak, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.Tak_Érv_k, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.Tak_érv_V, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.Tak_Ár, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.Label11, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.Label9, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.Tak_J_takarítási_fajta, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.Label12, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.Label5, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 21);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(658, 102);
            this.tableLayoutPanel1.TabIndex = 206;
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Location = new System.Drawing.Point(3, 0);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(96, 25);
            this.Label15.TabIndex = 114;
            this.Label15.Text = "Sorszám:";
            // 
            // Tak_id
            // 
            this.Tak_id.Enabled = false;
            this.Tak_id.Location = new System.Drawing.Point(3, 42);
            this.Tak_id.Name = "Tak_id";
            this.Tak_id.Size = new System.Drawing.Size(121, 30);
            this.Tak_id.TabIndex = 113;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(130, 39);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(95, 25);
            this.Label10.TabIndex = 4;
            this.Label10.Text = "Napszak:";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(130, 78);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(174, 25);
            this.Label6.TabIndex = 2;
            this.Label6.Text = "Érvényes kezdete:";
            // 
            // Tak_J_típus
            // 
            this.Tak_J_típus.FormattingEnabled = true;
            this.Tak_J_típus.Location = new System.Drawing.Point(310, 3);
            this.Tak_J_típus.Name = "Tak_J_típus";
            this.Tak_J_típus.Size = new System.Drawing.Size(121, 33);
            this.Tak_J_típus.TabIndex = 11;
            // 
            // Tak_Napszak
            // 
            this.Tak_Napszak.FormattingEnabled = true;
            this.Tak_Napszak.Location = new System.Drawing.Point(310, 42);
            this.Tak_Napszak.Name = "Tak_Napszak";
            this.Tak_Napszak.Size = new System.Drawing.Size(121, 33);
            this.Tak_Napszak.TabIndex = 10;
            // 
            // Tak_Érv_k
            // 
            this.Tak_Érv_k.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Tak_Érv_k.Location = new System.Drawing.Point(310, 81);
            this.Tak_Érv_k.Name = "Tak_Érv_k";
            this.Tak_Érv_k.Size = new System.Drawing.Size(121, 30);
            this.Tak_Érv_k.TabIndex = 7;
            // 
            // Tak_érv_V
            // 
            this.Tak_érv_V.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Tak_érv_V.Location = new System.Drawing.Point(591, 81);
            this.Tak_érv_V.Name = "Tak_érv_V";
            this.Tak_érv_V.Size = new System.Drawing.Size(121, 30);
            this.Tak_érv_V.TabIndex = 8;
            // 
            // Tak_Ár
            // 
            this.Tak_Ár.Location = new System.Drawing.Point(591, 42);
            this.Tak_Ár.Name = "Tak_Ár";
            this.Tak_Ár.Size = new System.Drawing.Size(121, 30);
            this.Tak_Ár.TabIndex = 9;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(437, 0);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(144, 25);
            this.Label11.TabIndex = 5;
            this.Label11.Text = "Takarítási fajta:";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(437, 39);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(38, 25);
            this.Label9.TabIndex = 3;
            this.Label9.Text = "Ár:";
            // 
            // Tak_J_takarítási_fajta
            // 
            this.Tak_J_takarítási_fajta.FormattingEnabled = true;
            this.Tak_J_takarítási_fajta.Location = new System.Drawing.Point(591, 3);
            this.Tak_J_takarítási_fajta.Name = "Tak_J_takarítási_fajta";
            this.Tak_J_takarítási_fajta.Size = new System.Drawing.Size(121, 33);
            this.Tak_J_takarítási_fajta.TabIndex = 0;
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(437, 78);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(148, 25);
            this.Label12.TabIndex = 6;
            this.Label12.Text = "Érvényes vége:";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(130, 0);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(119, 25);
            this.Label5.TabIndex = 1;
            this.Label5.Text = "Jármű típus:";
            // 
            // Tak_Ár_Tábla
            // 
            this.Tak_Ár_Tábla.AllowUserToAddRows = false;
            this.Tak_Ár_Tábla.AllowUserToDeleteRows = false;
            this.Tak_Ár_Tábla.AllowUserToResizeRows = false;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Tak_Ár_Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.Tak_Ár_Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tak_Ár_Tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.Tak_Ár_Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tak_Ár_Tábla.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.Tak_Ár_Tábla.EnableHeadersVisualStyles = false;
            this.Tak_Ár_Tábla.FilterAndSortEnabled = true;
            this.Tak_Ár_Tábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tak_Ár_Tábla.Location = new System.Drawing.Point(4, 236);
            this.Tak_Ár_Tábla.MaxFilterButtonImageHeight = 23;
            this.Tak_Ár_Tábla.Name = "Tak_Ár_Tábla";
            this.Tak_Ár_Tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tak_Ár_Tábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.Tak_Ár_Tábla.RowHeadersWidth = 30;
            this.Tak_Ár_Tábla.Size = new System.Drawing.Size(777, 103);
            this.Tak_Ár_Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tak_Ár_Tábla.TabIndex = 112;
            this.Tak_Ár_Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tak_Ár_Tábla_CellClick);
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.Color.SeaShell;
            this.tabPage5.Controls.Add(this.tableLayoutPanel5);
            this.tabPage5.Location = new System.Drawing.Point(4, 34);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(1484, 484);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "E-mail címek";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Controls.Add(this.email_tabla, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1478, 478);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // email_tabla
            // 
            this.email_tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.email_tabla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.email_tabla.FilterAndSortEnabled = true;
            this.email_tabla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.email_tabla.Location = new System.Drawing.Point(3, 3);
            this.email_tabla.MaxFilterButtonImageHeight = 23;
            this.email_tabla.Name = "email_tabla";
            this.email_tabla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.email_tabla.RowHeadersWidth = 51;
            this.email_tabla.RowTemplate.Height = 24;
            this.email_tabla.Size = new System.Drawing.Size(1472, 472);
            this.email_tabla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.email_tabla.TabIndex = 0;
            // 
            // Button13
            // 
            this.Button13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button13.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.Button13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button13.Location = new System.Drawing.Point(1442, 6);
            this.Button13.Name = "Button13";
            this.Button13.Size = new System.Drawing.Size(45, 45);
            this.Button13.TabIndex = 53;
            this.toolTip1.SetToolTip(this.Button13, "Súgó");
            this.Button13.UseVisualStyleBackColor = true;
            this.Button13.Click += new System.EventHandler(this.Button13_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(390, 10);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(1040, 25);
            this.Holtart.TabIndex = 1;
            this.Holtart.Visible = false;
            // 
            // Ablak_alap_program_egyéb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(1499, 584);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Fülek);
            this.Controls.Add(this.Button13);
            this.Controls.Add(this.Panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_alap_program_egyéb";
            this.Text = "Program Alapadatok Egyéb";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AblakProgramegyéb_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.Fülek.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SAPTábla)).EndInit();
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TáblaOsztály)).EndInit();
            this.TabPage3.ResumeLayout(false);
            this.TabPage3.PerformLayout();
            this.TabPage4.ResumeLayout(false);
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Mátrix_tábla)).EndInit();
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Kötbér_tábla)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tak_Ár_Tábla)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.email_tabla)).EndInit();
            this.ResumeLayout(false);

        }

        internal Panel Panel1;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal Button Button13;
        internal TabControl Fülek;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal Button Osztályfrissít;
        internal Button OsztályRögzít;
        internal Button OsztályExcel;
        internal CheckBox Használatban;
        internal Label Label1;
        internal Label Label2;
        internal TextBox Osztálymező;
        internal Label Label3;
        internal TextBox Osztálynév;
        internal TextBox ID;
        internal DataGridView TáblaOsztály;
        internal TabPage TabPage3;
        internal Label Label8;
        internal Label Label7;
        internal TextBox Hova;
        internal TextBox Honnan;
        internal Button Honnan_rögzít;
        internal Button Hova_rögzít;
        internal DateTimePicker Dátumtól;
        internal DateTimePicker Dátumig;
        internal Label Label4;
        internal TabPage TabPage4;
        internal GroupBox GroupBox2;
        internal DataGridView Kötbér_tábla;
        internal Button Kötbér_Frissít;
        internal TextBox Kötbér_Nem;
        internal Button Button4;
        internal Label Label16;
        internal TextBox Kötbér_pót;
        internal Label Label14;
        internal Label LLabel1;
        internal ComboBox Kötbér_takarítási_fajta;
        internal GroupBox GroupBox1;
        internal Zuby.ADGV.AdvancedDataGridView Tak_Ár_Tábla;
        internal Button Tak_Ár_frissít;
        internal Button Tak_Ár_rögzítés;
        internal ComboBox Tak_J_típus;
        internal ComboBox Tak_Napszak;
        internal TextBox Tak_Ár;
        internal DateTimePicker Tak_érv_V;
        internal DateTimePicker Tak_Érv_k;
        internal Label Label12;
        internal Label Label11;
        internal Label Label10;
        internal Label Label9;
        internal Label Label6;
        internal Label Label5;
        internal ComboBox Tak_J_takarítási_fajta;
        internal TextBox Tak_id;
        internal Label Label15;
        internal Button Excel_tak;
        internal Button Tak_Új;
        internal GroupBox GroupBox3;
        internal ComboBox Mátrix_igazság;
        internal ComboBox Mátrix_fajtamásik;
        internal DataGridView Mátrix_tábla;
        internal Button Mátrix_frissít;
        internal Button Mátrix_rögzít;
        internal Label Label17;
        internal Label Label18;
        internal Label Label19;
        internal ComboBox Mátrix_fajta;
        internal Button Bizt_frissít;
        internal ToolTip toolTip1;
        internal Button Adatok_beolvasása;
        internal Button Beviteli_táblakészítés;
        internal TableLayoutPanel tableLayoutPanel1;
        internal TableLayoutPanel tableLayoutPanel2;
        internal TableLayoutPanel tableLayoutPanel3;
        internal ComboBox Szűr_Fajta;
        internal ComboBox Szűr_Típus;
        internal ComboBox Szűr_Napszak;
        internal Label label22;
        internal Label label21;
        internal Label label20;
        internal CheckBox Szűr_Érvényes;
        internal Button VégeÁrRögzítés;
        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal Button Osztály_Új;
        internal DataGridView SAPTábla;
        private TableLayoutPanel tableLayoutPanel4;
        internal Button FejlécBeolvasása;
        internal Label Label69;
        internal ComboBox SAPCsoport;
        internal Label Label68;
        internal Label Label67;
        internal TextBox SAPOSzlopszám;
        internal Label Label60;
        internal TextBox SAPFejléc;
        internal TextBox Változónév;
        internal Button SAPRögzít;
        internal Button SAPTöröl;
        internal Button SAPExcel;
        internal Button SAPFrissít;
        internal TabPage tabPage5;
        private TableLayoutPanel tableLayoutPanel5;
        private Zuby.ADGV.AdvancedDataGridView email_tabla;
    }                    
}