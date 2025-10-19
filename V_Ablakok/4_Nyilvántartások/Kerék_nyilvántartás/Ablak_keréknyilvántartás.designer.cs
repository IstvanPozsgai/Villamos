using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{

    public partial class Ablak_keréknyilvántartás : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_keréknyilvántartás));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.LapFülek = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.Kiadta = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Csakkerék = new System.Windows.Forms.CheckBox();
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.Command7 = new System.Windows.Forms.Button();
            this.Command8 = new System.Windows.Forms.Button();
            this.Command10 = new System.Windows.Forms.Button();
            this.Erőtámvan = new System.Windows.Forms.Label();
            this.SAPPályaszám = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.BtnListáz = new System.Windows.Forms.Button();
            this.BtnSAP = new System.Windows.Forms.Button();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.Button1 = new System.Windows.Forms.Button();
            this.RögzítPályaszám = new System.Windows.Forms.ComboBox();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.KMU_old = new System.Windows.Forms.Label();
            this.KMU_új = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.Eszterga = new System.Windows.Forms.DateTimePicker();
            this.Label16 = new System.Windows.Forms.Label();
            this.EsztergaDátum = new System.Windows.Forms.Label();
            this.Command6 = new System.Windows.Forms.Button();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.ChkErőtám = new System.Windows.Forms.CheckBox();
            this.Command3 = new System.Windows.Forms.Button();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Új_Gyári = new System.Windows.Forms.TextBox();
            this.Új_Pozíció = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.GyűjtőRögzítés = new System.Windows.Forms.Button();
            this.Oka = new System.Windows.Forms.Label();
            this.Méret = new System.Windows.Forms.Label();
            this.Állapot = new System.Windows.Forms.Label();
            this.Gyártási = new System.Windows.Forms.Label();
            this.Berendezés = new System.Windows.Forms.Label();
            this.Megnevezés = new System.Windows.Forms.Label();
            this.Rögzítrögzít = new System.Windows.Forms.Button();
            this.RögzítOka = new System.Windows.Forms.TextBox();
            this.RögzítMéret = new System.Windows.Forms.TextBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.RögzítÁllapot = new System.Windows.Forms.ComboBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Rögzítpozíció = new System.Windows.Forms.ComboBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.Jegyzettömb = new System.Windows.Forms.RichTextBox();
            this.Panel5 = new System.Windows.Forms.Panel();
            this.Command5Főm = new System.Windows.Forms.Button();
            this.Command3Főm = new System.Windows.Forms.Button();
            this.Command7Főm = new System.Windows.Forms.Button();
            this.Command4Főm = new System.Windows.Forms.Button();
            this.SAPba = new System.Windows.Forms.CheckBox();
            this.Tábla1 = new System.Windows.Forms.DataGridView();
            this.PályaszámCombo2 = new System.Windows.Forms.ComboBox();
            this.Dátumtól = new System.Windows.Forms.DateTimePicker();
            this.Dátumig = new System.Windows.Forms.DateTimePicker();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Command9 = new System.Windows.Forms.Button();
            this.Command4 = new System.Windows.Forms.Button();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.Típus_Szűrő = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.Kerék_Ütemez = new System.Windows.Forms.Button();
            this.Tábla2 = new System.Windows.Forms.DataGridView();
            this.ExcelKöltség = new System.Windows.Forms.Button();
            this.Command5 = new System.Windows.Forms.Button();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Button13 = new System.Windows.Forms.Button();
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Panel1.SuspendLayout();
            this.LapFülek.SuspendLayout();
            this.TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.TabPage2.SuspendLayout();
            this.Panel4.SuspendLayout();
            this.Panel3.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.TabPage3.SuspendLayout();
            this.Panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla1)).BeginInit();
            this.TabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla2)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.Cmbtelephely);
            this.Panel1.Controls.Add(this.Label13);
            this.Panel1.Location = new System.Drawing.Point(3, 3);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(373, 38);
            this.Panel1.TabIndex = 57;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.DropDownHeight = 300;
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.IntegralHeight = false;
            this.Cmbtelephely.Location = new System.Drawing.Point(173, 7);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
            this.Cmbtelephely.SelectedIndexChanged += new System.EventHandler(this.Cmbtelephely_SelectedIndexChanged);
            this.Cmbtelephely.SelectionChangeCommitted += new System.EventHandler(this.Cmbtelephely_SelectionChangeCommitted);
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(9, 10);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(145, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // LapFülek
            // 
            this.LapFülek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LapFülek.Controls.Add(this.TabPage1);
            this.LapFülek.Controls.Add(this.TabPage2);
            this.LapFülek.Controls.Add(this.TabPage3);
            this.LapFülek.Controls.Add(this.TabPage4);
            this.LapFülek.Location = new System.Drawing.Point(3, 47);
            this.LapFülek.Name = "LapFülek";
            this.LapFülek.Padding = new System.Drawing.Point(16, 3);
            this.LapFülek.SelectedIndex = 0;
            this.LapFülek.Size = new System.Drawing.Size(1188, 524);
            this.LapFülek.TabIndex = 62;
            this.LapFülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Lapfülek_DrawItem);
            this.LapFülek.SelectedIndexChanged += new System.EventHandler(this.LAPFülek_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.Orange;
            this.TabPage1.Controls.Add(this.Kiadta);
            this.TabPage1.Controls.Add(this.Label2);
            this.TabPage1.Controls.Add(this.Csakkerék);
            this.TabPage1.Controls.Add(this.Tábla);
            this.TabPage1.Controls.Add(this.Command7);
            this.TabPage1.Controls.Add(this.Command8);
            this.TabPage1.Controls.Add(this.Command10);
            this.TabPage1.Controls.Add(this.Erőtámvan);
            this.TabPage1.Controls.Add(this.SAPPályaszám);
            this.TabPage1.Controls.Add(this.Label1);
            this.TabPage1.Controls.Add(this.BtnListáz);
            this.TabPage1.Controls.Add(this.BtnSAP);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1180, 491);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "SAP adatok";
            // 
            // Kiadta
            // 
            this.Kiadta.DropDownHeight = 300;
            this.Kiadta.FormattingEnabled = true;
            this.Kiadta.IntegralHeight = false;
            this.Kiadta.Location = new System.Drawing.Point(803, 29);
            this.Kiadta.Name = "Kiadta";
            this.Kiadta.Size = new System.Drawing.Size(368, 28);
            this.Kiadta.TabIndex = 116;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(803, 6);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(155, 20);
            this.Label2.TabIndex = 115;
            this.Label2.Text = "Ellenőr, vagy igénylő:";
            // 
            // Csakkerék
            // 
            this.Csakkerék.AutoSize = true;
            this.Csakkerék.Checked = true;
            this.Csakkerék.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Csakkerék.Location = new System.Drawing.Point(650, 6);
            this.Csakkerék.Name = "Csakkerék";
            this.Csakkerék.Size = new System.Drawing.Size(147, 24);
            this.Csakkerék.TabIndex = 114;
            this.Csakkerék.Text = "Csak kerékpárok";
            this.Csakkerék.UseVisualStyleBackColor = true;
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.EnableHeadersVisualStyles = false;
            this.Tábla.Location = new System.Drawing.Point(5, 59);
            this.Tábla.Name = "Tábla";
            this.Tábla.RowHeadersVisible = false;
            this.Tábla.RowHeadersWidth = 51;
            this.Tábla.Size = new System.Drawing.Size(1166, 426);
            this.Tábla.TabIndex = 113;
            // 
            // Command7
            // 
            this.Command7.BackgroundImage = global::Villamos.Properties.Resources.view_list_tree_3;
            this.Command7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command7.Location = new System.Drawing.Point(594, 6);
            this.Command7.Name = "Command7";
            this.Command7.Size = new System.Drawing.Size(50, 50);
            this.Command7.TabIndex = 112;
            this.ToolTip1.SetToolTip(this.Command7, "SAP-s Berendezés Listát készít");
            this.Command7.UseVisualStyleBackColor = true;
            this.Command7.Click += new System.EventHandler(this.Command7_Click);
            // 
            // Command8
            // 
            this.Command8.BackgroundImage = global::Villamos.Properties.Resources.kerékeszterga;
            this.Command8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command8.Location = new System.Drawing.Point(538, 6);
            this.Command8.Name = "Command8";
            this.Command8.Size = new System.Drawing.Size(50, 50);
            this.Command8.TabIndex = 111;
            this.ToolTip1.SetToolTip(this.Command8, "Munkafelvételi lap esztergáláshoz");
            this.Command8.UseVisualStyleBackColor = true;
            this.Command8.Click += new System.EventHandler(this.Command8_Click);
            // 
            // Command10
            // 
            this.Command10.BackgroundImage = global::Villamos.Properties.Resources.Document_write;
            this.Command10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command10.Location = new System.Drawing.Point(482, 6);
            this.Command10.Name = "Command10";
            this.Command10.Size = new System.Drawing.Size(50, 50);
            this.Command10.TabIndex = 110;
            this.ToolTip1.SetToolTip(this.Command10, "Kerékmérési nyomtatványt készít");
            this.Command10.UseVisualStyleBackColor = true;
            this.Command10.Click += new System.EventHandler(this.Command10_Click);
            // 
            // Erőtámvan
            // 
            this.Erőtámvan.AutoSize = true;
            this.Erőtámvan.BackColor = System.Drawing.Color.DarkOrange;
            this.Erőtámvan.Location = new System.Drawing.Point(386, 18);
            this.Erőtámvan.Name = "Erőtámvan";
            this.Erőtámvan.Size = new System.Drawing.Size(90, 20);
            this.Erőtámvan.TabIndex = 109;
            this.Erőtámvan.Text = "Erőtám van";
            // 
            // SAPPályaszám
            // 
            this.SAPPályaszám.DropDownHeight = 300;
            this.SAPPályaszám.FormattingEnabled = true;
            this.SAPPályaszám.IntegralHeight = false;
            this.SAPPályaszám.Location = new System.Drawing.Point(220, 10);
            this.SAPPályaszám.Name = "SAPPályaszám";
            this.SAPPályaszám.Size = new System.Drawing.Size(104, 28);
            this.SAPPályaszám.TabIndex = 108;
            this.SAPPályaszám.TextUpdate += new System.EventHandler(this.SAPPályaszám_TextUpdate);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.DarkOrange;
            this.Label1.Location = new System.Drawing.Point(115, 18);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(89, 20);
            this.Label1.TabIndex = 107;
            this.Label1.Text = "Pályaszám:";
            // 
            // BtnListáz
            // 
            this.BtnListáz.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.BtnListáz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnListáz.Location = new System.Drawing.Point(330, 3);
            this.BtnListáz.Name = "BtnListáz";
            this.BtnListáz.Size = new System.Drawing.Size(50, 50);
            this.BtnListáz.TabIndex = 106;
            this.ToolTip1.SetToolTip(this.BtnListáz, "Frissíti a kiválasztott pályaszámnak megfelelően a táblázatot.\r\n");
            this.BtnListáz.UseVisualStyleBackColor = true;
            this.BtnListáz.Click += new System.EventHandler(this.BtnListáz_Click);
            // 
            // BtnSAP
            // 
            this.BtnSAP.BackgroundImage = global::Villamos.Properties.Resources.SAP;
            this.BtnSAP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSAP.Location = new System.Drawing.Point(3, 3);
            this.BtnSAP.Name = "BtnSAP";
            this.BtnSAP.Size = new System.Drawing.Size(50, 50);
            this.BtnSAP.TabIndex = 12;
            this.ToolTip1.SetToolTip(this.BtnSAP, "SAP-s adatokat frissíti");
            this.BtnSAP.UseVisualStyleBackColor = true;
            this.BtnSAP.Click += new System.EventHandler(this.BtnSAP_Click);
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.Gold;
            this.TabPage2.Controls.Add(this.Button1);
            this.TabPage2.Controls.Add(this.RögzítPályaszám);
            this.TabPage2.Controls.Add(this.Panel4);
            this.TabPage2.Controls.Add(this.Panel3);
            this.TabPage2.Controls.Add(this.Panel2);
            this.TabPage2.Controls.Add(this.Label3);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(1180, 491);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Mérési adatok rögzítése";
            // 
            // Button1
            // 
            this.Button1.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button1.Location = new System.Drawing.Point(245, 6);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(45, 45);
            this.Button1.TabIndex = 110;
            this.ToolTip1.SetToolTip(this.Button1, "Frissíti a kiválasztott pályaszámnak megfelelően az adatokat");
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // RögzítPályaszám
            // 
            this.RögzítPályaszám.DropDownHeight = 300;
            this.RögzítPályaszám.DropDownWidth = 110;
            this.RögzítPályaszám.FormattingEnabled = true;
            this.RögzítPályaszám.IntegralHeight = false;
            this.RögzítPályaszám.Location = new System.Drawing.Point(131, 11);
            this.RögzítPályaszám.Name = "RögzítPályaszám";
            this.RögzítPályaszám.Size = new System.Drawing.Size(110, 28);
            this.RögzítPályaszám.TabIndex = 109;
            this.RögzítPályaszám.SelectedIndexChanged += new System.EventHandler(this.RögzítPályaszám_SelectedIndexChanged);
            // 
            // Panel4
            // 
            this.Panel4.BackColor = System.Drawing.Color.Khaki;
            this.Panel4.Controls.Add(this.KMU_old);
            this.Panel4.Controls.Add(this.KMU_új);
            this.Panel4.Controls.Add(this.label11);
            this.Panel4.Controls.Add(this.Eszterga);
            this.Panel4.Controls.Add(this.Label16);
            this.Panel4.Controls.Add(this.EsztergaDátum);
            this.Panel4.Controls.Add(this.Command6);
            this.Panel4.Location = new System.Drawing.Point(9, 89);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(605, 108);
            this.Panel4.TabIndex = 3;
            // 
            // KMU_old
            // 
            this.KMU_old.AutoSize = true;
            this.KMU_old.BackColor = System.Drawing.Color.DarkOrange;
            this.KMU_old.Location = new System.Drawing.Point(332, 72);
            this.KMU_old.Name = "KMU_old";
            this.KMU_old.Size = new System.Drawing.Size(14, 20);
            this.KMU_old.TabIndex = 120;
            this.KMU_old.Text = "-";
            // 
            // KMU_új
            // 
            this.KMU_új.Location = new System.Drawing.Point(169, 66);
            this.KMU_új.Name = "KMU_új";
            this.KMU_új.Size = new System.Drawing.Size(143, 26);
            this.KMU_új.TabIndex = 119;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.DarkOrange;
            this.label11.Location = new System.Drawing.Point(8, 72);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(97, 20);
            this.label11.TabIndex = 118;
            this.label11.Text = "KMU értéke:";
            // 
            // Eszterga
            // 
            this.Eszterga.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Eszterga.Location = new System.Drawing.Point(169, 27);
            this.Eszterga.Name = "Eszterga";
            this.Eszterga.Size = new System.Drawing.Size(105, 26);
            this.Eszterga.TabIndex = 117;
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.BackColor = System.Drawing.Color.DarkOrange;
            this.Label16.Location = new System.Drawing.Point(8, 33);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(155, 20);
            this.Label16.TabIndex = 116;
            this.Label16.Text = "Esztergálás dátuma:";
            // 
            // EsztergaDátum
            // 
            this.EsztergaDátum.AutoSize = true;
            this.EsztergaDátum.BackColor = System.Drawing.Color.DarkOrange;
            this.EsztergaDátum.Location = new System.Drawing.Point(332, 32);
            this.EsztergaDátum.Name = "EsztergaDátum";
            this.EsztergaDátum.Size = new System.Drawing.Size(14, 20);
            this.EsztergaDátum.TabIndex = 115;
            this.EsztergaDátum.Text = "-";
            // 
            // Command6
            // 
            this.Command6.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Command6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command6.Location = new System.Drawing.Point(540, 33);
            this.Command6.Name = "Command6";
            this.Command6.Size = new System.Drawing.Size(50, 50);
            this.Command6.TabIndex = 10;
            this.ToolTip1.SetToolTip(this.Command6, "Rögzíti az adatokat");
            this.Command6.UseVisualStyleBackColor = true;
            this.Command6.Click += new System.EventHandler(this.Command6_Click);
            // 
            // Panel3
            // 
            this.Panel3.BackColor = System.Drawing.Color.Khaki;
            this.Panel3.Controls.Add(this.ChkErőtám);
            this.Panel3.Controls.Add(this.Command3);
            this.Panel3.Location = new System.Drawing.Point(492, 14);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(324, 69);
            this.Panel3.TabIndex = 2;
            // 
            // ChkErőtám
            // 
            this.ChkErőtám.AutoSize = true;
            this.ChkErőtám.BackColor = System.Drawing.Color.DarkOrange;
            this.ChkErőtám.Location = new System.Drawing.Point(21, 21);
            this.ChkErőtám.Name = "ChkErőtám";
            this.ChkErőtám.Size = new System.Drawing.Size(109, 24);
            this.ChkErőtám.TabIndex = 11;
            this.ChkErőtám.Text = "Erőtám van";
            this.ChkErőtám.UseVisualStyleBackColor = false;
            // 
            // Command3
            // 
            this.Command3.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Command3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command3.Location = new System.Drawing.Point(262, 7);
            this.Command3.Name = "Command3";
            this.Command3.Size = new System.Drawing.Size(50, 50);
            this.Command3.TabIndex = 10;
            this.ToolTip1.SetToolTip(this.Command3, "Rögzíti az adatokat");
            this.Command3.UseVisualStyleBackColor = true;
            this.Command3.Click += new System.EventHandler(this.Command3_Click);
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.Color.Khaki;
            this.Panel2.Controls.Add(this.Új_Gyári);
            this.Panel2.Controls.Add(this.Új_Pozíció);
            this.Panel2.Controls.Add(this.label17);
            this.Panel2.Controls.Add(this.label15);
            this.Panel2.Controls.Add(this.label14);
            this.Panel2.Controls.Add(this.GyűjtőRögzítés);
            this.Panel2.Controls.Add(this.Oka);
            this.Panel2.Controls.Add(this.Méret);
            this.Panel2.Controls.Add(this.Állapot);
            this.Panel2.Controls.Add(this.Gyártási);
            this.Panel2.Controls.Add(this.Berendezés);
            this.Panel2.Controls.Add(this.Megnevezés);
            this.Panel2.Controls.Add(this.Rögzítrögzít);
            this.Panel2.Controls.Add(this.RögzítOka);
            this.Panel2.Controls.Add(this.RögzítMéret);
            this.Panel2.Controls.Add(this.Label7);
            this.Panel2.Controls.Add(this.Label6);
            this.Panel2.Controls.Add(this.RögzítÁllapot);
            this.Panel2.Controls.Add(this.Label5);
            this.Panel2.Controls.Add(this.Rögzítpozíció);
            this.Panel2.Controls.Add(this.Label4);
            this.Panel2.Location = new System.Drawing.Point(9, 203);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(807, 276);
            this.Panel2.TabIndex = 1;
            // 
            // Új_Gyári
            // 
            this.Új_Gyári.BackColor = System.Drawing.SystemColors.Window;
            this.Új_Gyári.Location = new System.Drawing.Point(366, 65);
            this.Új_Gyári.MaxLength = 20;
            this.Új_Gyári.Name = "Új_Gyári";
            this.Új_Gyári.Size = new System.Drawing.Size(240, 26);
            this.Új_Gyári.TabIndex = 130;
            this.Új_Gyári.Visible = false;
            // 
            // Új_Pozíció
            // 
            this.Új_Pozíció.BackgroundImage = global::Villamos.Properties.Resources.Gear_01;
            this.Új_Pozíció.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Új_Pozíció.Location = new System.Drawing.Point(743, 78);
            this.Új_Pozíció.Name = "Új_Pozíció";
            this.Új_Pozíció.Size = new System.Drawing.Size(50, 50);
            this.Új_Pozíció.TabIndex = 129;
            this.ToolTip1.SetToolTip(this.Új_Pozíció, "Típusra jellemző Pozíciók");
            this.Új_Pozíció.UseVisualStyleBackColor = true;
            this.Új_Pozíció.Click += new System.EventHandler(this.Új_Pozíció_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.DarkOrange;
            this.label17.Location = new System.Drawing.Point(117, 127);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(103, 20);
            this.label17.TabIndex = 128;
            this.label17.Text = "Megnevezés:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.DarkOrange;
            this.label15.Location = new System.Drawing.Point(117, 97);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(175, 20);
            this.label15.TabIndex = 127;
            this.label15.Text = "SAP berendezés szám:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.DarkOrange;
            this.label14.Location = new System.Drawing.Point(117, 68);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(114, 20);
            this.label14.TabIndex = 126;
            this.label14.Text = "Gyártási szám:";
            // 
            // GyűjtőRögzítés
            // 
            this.GyűjtőRögzítés.BackgroundImage = global::Villamos.Properties.Resources.Fatcow_Farm_Fresh_Table_row_insert;
            this.GyűjtőRögzítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.GyűjtőRögzítés.Location = new System.Drawing.Point(743, 11);
            this.GyűjtőRögzítés.Name = "GyűjtőRögzítés";
            this.GyűjtőRögzítés.Size = new System.Drawing.Size(50, 50);
            this.GyűjtőRögzítés.TabIndex = 125;
            this.ToolTip1.SetToolTip(this.GyűjtőRögzítés, "Csoportos Rögzítés");
            this.GyűjtőRögzítés.UseVisualStyleBackColor = true;
            this.GyűjtőRögzítés.Click += new System.EventHandler(this.GyűjtőRögzítés_Click);
            // 
            // Oka
            // 
            this.Oka.AutoSize = true;
            this.Oka.BackColor = System.Drawing.Color.DarkOrange;
            this.Oka.Location = new System.Drawing.Point(475, 230);
            this.Oka.Name = "Oka";
            this.Oka.Size = new System.Drawing.Size(18, 20);
            this.Oka.TabIndex = 124;
            this.Oka.Text = "_";
            // 
            // Méret
            // 
            this.Méret.AutoSize = true;
            this.Méret.BackColor = System.Drawing.Color.DarkOrange;
            this.Méret.Location = new System.Drawing.Point(475, 197);
            this.Méret.Name = "Méret";
            this.Méret.Size = new System.Drawing.Size(18, 20);
            this.Méret.TabIndex = 123;
            this.Méret.Text = "_";
            // 
            // Állapot
            // 
            this.Állapot.AutoSize = true;
            this.Állapot.BackColor = System.Drawing.Color.DarkOrange;
            this.Állapot.Location = new System.Drawing.Point(475, 162);
            this.Állapot.Name = "Állapot";
            this.Állapot.Size = new System.Drawing.Size(18, 20);
            this.Állapot.TabIndex = 122;
            this.Állapot.Text = "_";
            // 
            // Gyártási
            // 
            this.Gyártási.AutoSize = true;
            this.Gyártási.BackColor = System.Drawing.Color.DarkOrange;
            this.Gyártási.Location = new System.Drawing.Point(321, 68);
            this.Gyártási.Name = "Gyártási";
            this.Gyártási.Size = new System.Drawing.Size(18, 20);
            this.Gyártási.TabIndex = 121;
            this.Gyártási.Text = "_";
            // 
            // Berendezés
            // 
            this.Berendezés.AutoSize = true;
            this.Berendezés.BackColor = System.Drawing.Color.DarkOrange;
            this.Berendezés.Location = new System.Drawing.Point(321, 97);
            this.Berendezés.Name = "Berendezés";
            this.Berendezés.Size = new System.Drawing.Size(18, 20);
            this.Berendezés.TabIndex = 120;
            this.Berendezés.Text = "_";
            // 
            // Megnevezés
            // 
            this.Megnevezés.AutoSize = true;
            this.Megnevezés.BackColor = System.Drawing.Color.DarkOrange;
            this.Megnevezés.Location = new System.Drawing.Point(321, 127);
            this.Megnevezés.Name = "Megnevezés";
            this.Megnevezés.Size = new System.Drawing.Size(18, 20);
            this.Megnevezés.TabIndex = 119;
            this.Megnevezés.Text = "_";
            // 
            // Rögzítrögzít
            // 
            this.Rögzítrögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Rögzítrögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Rögzítrögzít.Location = new System.Drawing.Point(743, 215);
            this.Rögzítrögzít.Name = "Rögzítrögzít";
            this.Rögzítrögzít.Size = new System.Drawing.Size(50, 50);
            this.Rögzítrögzít.TabIndex = 118;
            this.ToolTip1.SetToolTip(this.Rögzítrögzít, "Rögzíti az adatokat");
            this.Rögzítrögzít.UseVisualStyleBackColor = true;
            this.Rögzítrögzít.Click += new System.EventHandler(this.Rögzítrögzít_Click);
            // 
            // RögzítOka
            // 
            this.RögzítOka.Location = new System.Drawing.Point(121, 228);
            this.RögzítOka.MaxLength = 20;
            this.RögzítOka.Name = "RögzítOka";
            this.RögzítOka.Size = new System.Drawing.Size(190, 26);
            this.RögzítOka.TabIndex = 117;
            // 
            // RögzítMéret
            // 
            this.RögzítMéret.Location = new System.Drawing.Point(121, 195);
            this.RögzítMéret.Name = "RögzítMéret";
            this.RögzítMéret.Size = new System.Drawing.Size(190, 26);
            this.RögzítMéret.TabIndex = 116;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.BackColor = System.Drawing.Color.DarkOrange;
            this.Label7.Location = new System.Drawing.Point(14, 201);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(54, 20);
            this.Label7.TabIndex = 115;
            this.Label7.Text = "Méret:";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.BackColor = System.Drawing.Color.DarkOrange;
            this.Label6.Location = new System.Drawing.Point(14, 234);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(87, 20);
            this.Label6.TabIndex = 114;
            this.Label6.Text = "Mérés oka:";
            // 
            // RögzítÁllapot
            // 
            this.RögzítÁllapot.DropDownHeight = 200;
            this.RögzítÁllapot.FormattingEnabled = true;
            this.RögzítÁllapot.IntegralHeight = false;
            this.RögzítÁllapot.Location = new System.Drawing.Point(121, 160);
            this.RögzítÁllapot.Name = "RögzítÁllapot";
            this.RögzítÁllapot.Size = new System.Drawing.Size(348, 28);
            this.RögzítÁllapot.TabIndex = 113;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.BackColor = System.Drawing.Color.DarkOrange;
            this.Label5.Location = new System.Drawing.Point(14, 168);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(62, 20);
            this.Label5.TabIndex = 112;
            this.Label5.Text = "Állapot:";
            // 
            // Rögzítpozíció
            // 
            this.Rögzítpozíció.DropDownHeight = 200;
            this.Rögzítpozíció.FormattingEnabled = true;
            this.Rögzítpozíció.IntegralHeight = false;
            this.Rögzítpozíció.Location = new System.Drawing.Point(121, 16);
            this.Rögzítpozíció.Name = "Rögzítpozíció";
            this.Rögzítpozíció.Size = new System.Drawing.Size(190, 28);
            this.Rögzítpozíció.TabIndex = 111;
            this.Rögzítpozíció.SelectedIndexChanged += new System.EventHandler(this.Rögzítpozíció_SelectedIndexChanged);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.BackColor = System.Drawing.Color.DarkOrange;
            this.Label4.Location = new System.Drawing.Point(14, 19);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(63, 20);
            this.Label4.TabIndex = 110;
            this.Label4.Text = "Pozíció:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.DarkOrange;
            this.Label3.Location = new System.Drawing.Point(24, 14);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(89, 20);
            this.Label3.TabIndex = 0;
            this.Label3.Text = "Pályaszám:";
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.Orange;
            this.TabPage3.Controls.Add(this.Jegyzettömb);
            this.TabPage3.Controls.Add(this.Panel5);
            this.TabPage3.Controls.Add(this.Tábla1);
            this.TabPage3.Controls.Add(this.PályaszámCombo2);
            this.TabPage3.Controls.Add(this.Dátumtól);
            this.TabPage3.Controls.Add(this.Dátumig);
            this.TabPage3.Controls.Add(this.Label10);
            this.TabPage3.Controls.Add(this.Label9);
            this.TabPage3.Controls.Add(this.Label8);
            this.TabPage3.Controls.Add(this.Command9);
            this.TabPage3.Controls.Add(this.Command4);
            this.TabPage3.Location = new System.Drawing.Point(4, 29);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(1180, 491);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Rögzítések listázása";
            // 
            // Jegyzettömb
            // 
            this.Jegyzettömb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Jegyzettömb.Location = new System.Drawing.Point(4, 66);
            this.Jegyzettömb.Name = "Jegyzettömb";
            this.Jegyzettömb.Size = new System.Drawing.Size(1164, 419);
            this.Jegyzettömb.TabIndex = 125;
            this.Jegyzettömb.Text = "";
            // 
            // Panel5
            // 
            this.Panel5.BackColor = System.Drawing.Color.Khaki;
            this.Panel5.Controls.Add(this.Command5Főm);
            this.Panel5.Controls.Add(this.Command3Főm);
            this.Panel5.Controls.Add(this.Command7Főm);
            this.Panel5.Controls.Add(this.Command4Főm);
            this.Panel5.Controls.Add(this.SAPba);
            this.Panel5.Location = new System.Drawing.Point(499, 6);
            this.Panel5.Name = "Panel5";
            this.Panel5.Size = new System.Drawing.Size(430, 53);
            this.Panel5.TabIndex = 124;
            // 
            // Command5Főm
            // 
            this.Command5Főm.BackgroundImage = global::Villamos.Properties.Resources.váltó;
            this.Command5Főm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command5Főm.Location = new System.Drawing.Point(222, 3);
            this.Command5Főm.Name = "Command5Főm";
            this.Command5Főm.Size = new System.Drawing.Size(45, 45);
            this.Command5Főm.TabIndex = 125;
            this.ToolTip1.SetToolTip(this.Command5Főm, "Váltókapcsoló jegyzettömb és táblázat között");
            this.Command5Főm.UseVisualStyleBackColor = true;
            this.Command5Főm.Click += new System.EventHandler(this.Command5Főm_Click);
            // 
            // Command3Főm
            // 
            this.Command3Főm.BackgroundImage = global::Villamos.Properties.Resources.SAPba_32;
            this.Command3Főm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command3Főm.Location = new System.Drawing.Point(273, 3);
            this.Command3Főm.Name = "Command3Főm";
            this.Command3Főm.Size = new System.Drawing.Size(45, 45);
            this.Command3Főm.TabIndex = 124;
            this.ToolTip1.SetToolTip(this.Command3Főm, "SAP-s feltöltéshez elkészíti az adatokat");
            this.Command3Főm.UseVisualStyleBackColor = true;
            this.Command3Főm.Click += new System.EventHandler(this.Command3Főm_Click);
            // 
            // Command7Főm
            // 
            this.Command7Főm.BackgroundImage = global::Villamos.Properties.Resources.SAP32_jó;
            this.Command7Főm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command7Főm.Location = new System.Drawing.Point(375, 4);
            this.Command7Főm.Name = "Command7Főm";
            this.Command7Főm.Size = new System.Drawing.Size(45, 45);
            this.Command7Főm.TabIndex = 123;
            this.ToolTip1.SetToolTip(this.Command7Főm, "Sikeres feltöltést követően státus állítás");
            this.Command7Főm.UseVisualStyleBackColor = true;
            this.Command7Főm.Click += new System.EventHandler(this.Command7Főm_Click);
            // 
            // Command4Főm
            // 
            this.Command4Főm.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Command4Főm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command4Főm.Location = new System.Drawing.Point(160, 3);
            this.Command4Főm.Name = "Command4Főm";
            this.Command4Főm.Size = new System.Drawing.Size(45, 45);
            this.Command4Főm.TabIndex = 122;
            this.ToolTip1.SetToolTip(this.Command4Főm, "SAP-s adatokat frissíti");
            this.Command4Főm.UseVisualStyleBackColor = true;
            this.Command4Főm.Click += new System.EventHandler(this.Command4Főm_Click);
            // 
            // SAPba
            // 
            this.SAPba.AutoSize = true;
            this.SAPba.BackColor = System.Drawing.Color.DarkOrange;
            this.SAPba.Location = new System.Drawing.Point(12, 18);
            this.SAPba.Name = "SAPba";
            this.SAPba.Size = new System.Drawing.Size(142, 24);
            this.SAPba.TabIndex = 12;
            this.SAPba.Text = "SAP-ba feltöltve";
            this.SAPba.UseVisualStyleBackColor = false;
            // 
            // Tábla1
            // 
            this.Tábla1.AllowUserToAddRows = false;
            this.Tábla1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Tábla1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.Tábla1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Tábla1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla1.EnableHeadersVisualStyles = false;
            this.Tábla1.Location = new System.Drawing.Point(5, 66);
            this.Tábla1.Name = "Tábla1";
            this.Tábla1.RowHeadersVisible = false;
            this.Tábla1.RowHeadersWidth = 51;
            this.Tábla1.Size = new System.Drawing.Size(1163, 419);
            this.Tábla1.TabIndex = 123;
            // 
            // PályaszámCombo2
            // 
            this.PályaszámCombo2.DropDownHeight = 200;
            this.PályaszámCombo2.FormattingEnabled = true;
            this.PályaszámCombo2.IntegralHeight = false;
            this.PályaszámCombo2.Location = new System.Drawing.Point(235, 33);
            this.PályaszámCombo2.Name = "PályaszámCombo2";
            this.PályaszámCombo2.Size = new System.Drawing.Size(104, 28);
            this.PályaszámCombo2.TabIndex = 120;
            // 
            // Dátumtól
            // 
            this.Dátumtól.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátumtól.Location = new System.Drawing.Point(10, 35);
            this.Dátumtól.Name = "Dátumtól";
            this.Dátumtól.Size = new System.Drawing.Size(105, 26);
            this.Dátumtól.TabIndex = 119;
            // 
            // Dátumig
            // 
            this.Dátumig.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátumig.Location = new System.Drawing.Point(121, 35);
            this.Dátumig.Name = "Dátumig";
            this.Dátumig.Size = new System.Drawing.Size(105, 26);
            this.Dátumig.TabIndex = 118;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.BackColor = System.Drawing.Color.DarkOrange;
            this.Label10.Location = new System.Drawing.Point(121, 10);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(69, 20);
            this.Label10.TabIndex = 3;
            this.Label10.Text = "Dátumig";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.BackColor = System.Drawing.Color.DarkOrange;
            this.Label9.Location = new System.Drawing.Point(235, 10);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(89, 20);
            this.Label9.TabIndex = 2;
            this.Label9.Text = "Pályaszám:";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.BackColor = System.Drawing.Color.DarkOrange;
            this.Label8.Location = new System.Drawing.Point(10, 10);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(78, 20);
            this.Label8.TabIndex = 1;
            this.Label8.Text = "Dátumtól:";
            // 
            // Command9
            // 
            this.Command9.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Command9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command9.Location = new System.Drawing.Point(436, 10);
            this.Command9.Name = "Command9";
            this.Command9.Size = new System.Drawing.Size(50, 50);
            this.Command9.TabIndex = 122;
            this.Command9.UseVisualStyleBackColor = true;
            this.Command9.Click += new System.EventHandler(this.Command9_Click);
            // 
            // Command4
            // 
            this.Command4.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Command4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command4.Location = new System.Drawing.Point(380, 10);
            this.Command4.Name = "Command4";
            this.Command4.Size = new System.Drawing.Size(50, 50);
            this.Command4.TabIndex = 121;
            this.ToolTip1.SetToolTip(this.Command4, "SAP-s adatokat frissíti");
            this.Command4.UseVisualStyleBackColor = true;
            this.Command4.Click += new System.EventHandler(this.Command4_Click);
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.Orange;
            this.TabPage4.Controls.Add(this.Típus_Szűrő);
            this.TabPage4.Controls.Add(this.label12);
            this.TabPage4.Controls.Add(this.Kerék_Ütemez);
            this.TabPage4.Controls.Add(this.Tábla2);
            this.TabPage4.Controls.Add(this.ExcelKöltség);
            this.TabPage4.Controls.Add(this.Command5);
            this.TabPage4.Location = new System.Drawing.Point(4, 29);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Size = new System.Drawing.Size(1180, 491);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Eredmények listázása";
            // 
            // Típus_Szűrő
            // 
            this.Típus_Szűrő.DropDownHeight = 200;
            this.Típus_Szűrő.FormattingEnabled = true;
            this.Típus_Szűrő.IntegralHeight = false;
            this.Típus_Szűrő.Location = new System.Drawing.Point(64, 30);
            this.Típus_Szűrő.Name = "Típus_Szűrő";
            this.Típus_Szűrő.Size = new System.Drawing.Size(176, 28);
            this.Típus_Szűrő.TabIndex = 129;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.DarkOrange;
            this.label12.Location = new System.Drawing.Point(7, 38);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(51, 20);
            this.label12.TabIndex = 128;
            this.label12.Text = "Típus:";
            // 
            // Kerék_Ütemez
            // 
            this.Kerék_Ütemez.BackgroundImage = global::Villamos.Properties.Resources.kerékeszterga;
            this.Kerék_Ütemez.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Kerék_Ütemez.Location = new System.Drawing.Point(496, 8);
            this.Kerék_Ütemez.Name = "Kerék_Ütemez";
            this.Kerék_Ütemez.Size = new System.Drawing.Size(50, 50);
            this.Kerék_Ütemez.TabIndex = 127;
            this.ToolTip1.SetToolTip(this.Kerék_Ütemez, "Kerékesztergálásra ütemez");
            this.Kerék_Ütemez.UseVisualStyleBackColor = true;
            this.Kerék_Ütemez.Click += new System.EventHandler(this.Kerék_Ütemez_Click);
            // 
            // Tábla2
            // 
            this.Tábla2.AllowUserToAddRows = false;
            this.Tábla2.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Tábla2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.Tábla2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.Tábla2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla2.EnableHeadersVisualStyles = false;
            this.Tábla2.Location = new System.Drawing.Point(5, 64);
            this.Tábla2.Name = "Tábla2";
            this.Tábla2.RowHeadersWidth = 51;
            this.Tábla2.Size = new System.Drawing.Size(1172, 421);
            this.Tábla2.TabIndex = 126;
            // 
            // ExcelKöltség
            // 
            this.ExcelKöltség.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.ExcelKöltség.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ExcelKöltség.Location = new System.Drawing.Point(306, 8);
            this.ExcelKöltség.Name = "ExcelKöltség";
            this.ExcelKöltség.Size = new System.Drawing.Size(50, 50);
            this.ExcelKöltség.TabIndex = 125;
            this.ExcelKöltség.UseVisualStyleBackColor = true;
            this.ExcelKöltség.Click += new System.EventHandler(this.ExcelKöltség_Click);
            // 
            // Command5
            // 
            this.Command5.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Command5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command5.Location = new System.Drawing.Point(250, 8);
            this.Command5.Name = "Command5";
            this.Command5.Size = new System.Drawing.Size(50, 50);
            this.Command5.TabIndex = 124;
            this.ToolTip1.SetToolTip(this.Command5, "SAP-s adatokat frissíti");
            this.Command5.UseVisualStyleBackColor = true;
            this.Command5.Click += new System.EventHandler(this.Command5_Click);
            // 
            // Button13
            // 
            this.Button13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button13.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.Button13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button13.Location = new System.Drawing.Point(1151, 1);
            this.Button13.Name = "Button13";
            this.Button13.Size = new System.Drawing.Size(45, 45);
            this.Button13.TabIndex = 61;
            this.ToolTip1.SetToolTip(this.Button13, "Súgó");
            this.Button13.UseVisualStyleBackColor = true;
            this.Button13.Click += new System.EventHandler(this.Button13_Click);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(380, 10);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(745, 30);
            this.Holtart.TabIndex = 117;
            this.Holtart.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Ablak_keréknyilvántartás
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Orange;
            this.ClientSize = new System.Drawing.Size(1198, 573);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.LapFülek);
            this.Controls.Add(this.Button13);
            this.Controls.Add(this.Panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_keréknyilvántartás";
            this.Text = "Kerék méret nyilvántartás";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_keréknyilvántartás_FormClosed);
            this.Load += new System.EventHandler(this.Ablak_keréknyilvántartás_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.LapFülek.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            this.Panel4.ResumeLayout(false);
            this.Panel4.PerformLayout();
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.TabPage3.ResumeLayout(false);
            this.TabPage3.PerformLayout();
            this.Panel5.ResumeLayout(false);
            this.Panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla1)).EndInit();
            this.TabPage4.ResumeLayout(false);
            this.TabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla2)).EndInit();
            this.ResumeLayout(false);

        }

        internal Panel Panel1;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal Button Button13;
        internal TabControl LapFülek;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal TabPage TabPage3;
        internal TabPage TabPage4;
        internal Button BtnSAP;
        internal Button Command7;
        internal Button Command8;
        internal Button Command10;
        internal Label Erőtámvan;
        internal ComboBox SAPPályaszám;
        internal Label Label1;
        internal Button BtnListáz;
        internal DataGridView Tábla;
        internal ToolTip ToolTip1;
        internal Panel Panel4;
        internal Panel Panel3;
        internal Panel Panel2;
        internal Label Label3;
        internal ComboBox RögzítPályaszám;
        internal TextBox RögzítOka;
        internal TextBox RögzítMéret;
        internal Label Label7;
        internal Label Label6;
        internal ComboBox RögzítÁllapot;
        internal Label Label5;
        internal ComboBox Rögzítpozíció;
        internal Label Label4;
        internal Label Label16;
        internal Label EsztergaDátum;
        internal Button Command6;
        internal CheckBox ChkErőtám;
        internal Button Command3;
        internal Label Oka;
        internal Label Méret;
        internal Label Állapot;
        internal Label Gyártási;
        internal Label Berendezés;
        internal Label Megnevezés;
        internal Button Rögzítrögzít;
        internal DateTimePicker Eszterga;
        internal Button Command4;
        internal ComboBox PályaszámCombo2;
        internal DateTimePicker Dátumtól;
        internal DateTimePicker Dátumig;
        internal Label Label10;
        internal Label Label9;
        internal Label Label8;
        internal Button Command9;
        internal DataGridView Tábla1;
        internal Panel Panel5;
        internal DataGridView Tábla2;
        internal Button ExcelKöltség;
        internal Button Command5;
        internal Button Command5Főm;
        internal Button Command3Főm;
        internal Button Command7Főm;
        internal Button Command4Főm;
        internal CheckBox SAPba;
        internal RichTextBox Jegyzettömb;
        internal CheckBox Csakkerék;
        internal Label Label2;
        internal ComboBox Kiadta;
        internal Button Button1;
        internal Button Kerék_Ütemez;
        internal TextBox KMU_új;
        internal Label label11;
        internal Button GyűjtőRögzítés;
        internal ComboBox Típus_Szűrő;
        internal Label label12;
        internal Label KMU_old;
        internal Label label17;
        internal Label label15;
        internal Label label14;
        internal Button Új_Pozíció;
        internal TextBox Új_Gyári;
        internal V_MindenEgyéb.MyProgressbar Holtart;
        private Timer timer1;
    }
}