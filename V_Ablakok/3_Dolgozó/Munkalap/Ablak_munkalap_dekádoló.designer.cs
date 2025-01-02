using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{

    public partial class Ablak_munkalap_dekádoló : Form
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_munkalap_dekádoló));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Fülek = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.FelhasználtText = new System.Windows.Forms.Label();
            this.RendelkezésText = new System.Windows.Forms.Label();
            this.Benn_Lévő = new System.Windows.Forms.Button();
            this.Command24 = new System.Windows.Forms.Button();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Command7 = new System.Windows.Forms.Button();
            this.Command11 = new System.Windows.Forms.Button();
            this.Command18 = new System.Windows.Forms.Button();
            this.Dátum = new System.Windows.Forms.DateTimePicker();
            this.Command6 = new System.Windows.Forms.Button();
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.Command5 = new System.Windows.Forms.Button();
            this.Tábla1 = new System.Windows.Forms.DataGridView();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.Button2 = new System.Windows.Forms.Button();
            this.Option4 = new System.Windows.Forms.RadioButton();
            this.Option3 = new System.Windows.Forms.RadioButton();
            this.Option2 = new System.Windows.Forms.RadioButton();
            this.Option1 = new System.Windows.Forms.RadioButton();
            this.Command12 = new System.Windows.Forms.Button();
            this.Tábla3 = new System.Windows.Forms.DataGridView();
            this.DekádDátum = new System.Windows.Forms.DateTimePicker();
            this.Command25 = new System.Windows.Forms.Button();
            this.Excel = new System.Windows.Forms.Button();
            this.Command14 = new System.Windows.Forms.Button();
            this.Command10 = new System.Windows.Forms.Button();
            this.Command13 = new System.Windows.Forms.Button();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.Feljebb = new System.Windows.Forms.Button();
            this.Változattörlés = new System.Windows.Forms.Button();
            this.Command4 = new System.Windows.Forms.Button();
            this.List1 = new System.Windows.Forms.ListBox();
            this.Text1 = new System.Windows.Forms.TextBox();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.Napi_id = new System.Windows.Forms.TextBox();
            this.Tábla2 = new System.Windows.Forms.DataGridView();
            this.TextPályaszám = new System.Windows.Forms.TextBox();
            this.TextMegnevezés = new System.Windows.Forms.TextBox();
            this.TextMűvelet = new System.Windows.Forms.TextBox();
            this.TextRendelés = new System.Windows.Forms.TextBox();
            this.Label14 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Command3 = new System.Windows.Forms.Button();
            this.Command = new System.Windows.Forms.Button();
            this.Command2 = new System.Windows.Forms.Button();
            this.Holtart = new System.Windows.Forms.ProgressBar();
            this.SaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.Panel1.SuspendLayout();
            this.Fülek.SuspendLayout();
            this.TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla1)).BeginInit();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla3)).BeginInit();
            this.TabPage3.SuspendLayout();
            this.TabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla2)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.Cmbtelephely);
            this.Panel1.Controls.Add(this.Label13);
            this.Panel1.Location = new System.Drawing.Point(5, 5);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(373, 33);
            this.Panel1.TabIndex = 55;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(175, 2);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
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
            // Fülek
            // 
            this.Fülek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fülek.Controls.Add(this.TabPage1);
            this.Fülek.Controls.Add(this.TabPage2);
            this.Fülek.Controls.Add(this.TabPage3);
            this.Fülek.Controls.Add(this.TabPage4);
            this.Fülek.Location = new System.Drawing.Point(5, 50);
            this.Fülek.Name = "Fülek";
            this.Fülek.Padding = new System.Drawing.Point(16, 3);
            this.Fülek.SelectedIndex = 0;
            this.Fülek.Size = new System.Drawing.Size(1190, 565);
            this.Fülek.TabIndex = 60;
            this.Fülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Fülek_DrawItem);
            this.Fülek.SelectedIndexChanged += new System.EventHandler(this.Fülek_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.Thistle;
            this.TabPage1.Controls.Add(this.FelhasználtText);
            this.TabPage1.Controls.Add(this.RendelkezésText);
            this.TabPage1.Controls.Add(this.Benn_Lévő);
            this.TabPage1.Controls.Add(this.Command24);
            this.TabPage1.Controls.Add(this.Label9);
            this.TabPage1.Controls.Add(this.Label8);
            this.TabPage1.Controls.Add(this.Command7);
            this.TabPage1.Controls.Add(this.Command11);
            this.TabPage1.Controls.Add(this.Command18);
            this.TabPage1.Controls.Add(this.Dátum);
            this.TabPage1.Controls.Add(this.Command6);
            this.TabPage1.Controls.Add(this.Tábla);
            this.TabPage1.Controls.Add(this.Command5);
            this.TabPage1.Controls.Add(this.Tábla1);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1182, 532);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Napi Összesítés";
            // 
            // FelhasználtText
            // 
            this.FelhasználtText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FelhasználtText.AutoSize = true;
            this.FelhasználtText.BackColor = System.Drawing.Color.MediumOrchid;
            this.FelhasználtText.Location = new System.Drawing.Point(957, 489);
            this.FelhasználtText.Name = "FelhasználtText";
            this.FelhasználtText.Size = new System.Drawing.Size(13, 20);
            this.FelhasználtText.TabIndex = 101;
            this.FelhasználtText.Text = " ";
            // 
            // RendelkezésText
            // 
            this.RendelkezésText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RendelkezésText.AutoSize = true;
            this.RendelkezésText.BackColor = System.Drawing.Color.MediumOrchid;
            this.RendelkezésText.Location = new System.Drawing.Point(957, 406);
            this.RendelkezésText.Name = "RendelkezésText";
            this.RendelkezésText.Size = new System.Drawing.Size(13, 20);
            this.RendelkezésText.TabIndex = 100;
            this.RendelkezésText.Text = " ";
            // 
            // Benn_Lévő
            // 
            this.Benn_Lévő.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Benn_Lévő.BackgroundImage = global::Villamos.Properties.Resources.felhasználók32;
            this.Benn_Lévő.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Benn_Lévő.Location = new System.Drawing.Point(957, 335);
            this.Benn_Lévő.Name = "Benn_Lévő";
            this.Benn_Lévő.Size = new System.Drawing.Size(45, 45);
            this.Benn_Lévő.TabIndex = 99;
            this.ToolTip1.SetToolTip(this.Benn_Lévő, "Csoportok benn levő létszám segédablakát nyitja meg.");
            this.Benn_Lévő.UseVisualStyleBackColor = true;
            this.Benn_Lévő.Click += new System.EventHandler(this.Benn_Lévő_Click);
            // 
            // Command24
            // 
            this.Command24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Command24.BackgroundImage = global::Villamos.Properties.Resources.Fatcow_Farm_Fresh_Table_row_insert;
            this.Command24.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command24.Location = new System.Drawing.Point(1007, 335);
            this.Command24.Name = "Command24";
            this.Command24.Size = new System.Drawing.Size(45, 45);
            this.Command24.TabIndex = 98;
            this.ToolTip1.SetToolTip(this.Command24, "Beszúr egy üres sort a táblázatba.");
            this.Command24.UseVisualStyleBackColor = true;
            this.Command24.Click += new System.EventHandler(this.Command24_Click);
            // 
            // Label9
            // 
            this.Label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label9.AutoSize = true;
            this.Label9.BackColor = System.Drawing.Color.MediumOrchid;
            this.Label9.Location = new System.Drawing.Point(957, 465);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(120, 20);
            this.Label9.TabIndex = 96;
            this.Label9.Text = "Felhasznált idő:";
            // 
            // Label8
            // 
            this.Label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label8.AutoSize = true;
            this.Label8.BackColor = System.Drawing.Color.MediumOrchid;
            this.Label8.Location = new System.Drawing.Point(957, 383);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(173, 20);
            this.Label8.TabIndex = 94;
            this.Label8.Text = "Rendelkezésre álló idő:";
            // 
            // Command7
            // 
            this.Command7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Command7.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Command7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command7.Location = new System.Drawing.Point(995, 6);
            this.Command7.Name = "Command7";
            this.Command7.Size = new System.Drawing.Size(45, 45);
            this.Command7.TabIndex = 93;
            this.ToolTip1.SetToolTip(this.Command7, "Új oszlop Beszúrásához a segédablakot megnyitja");
            this.Command7.UseVisualStyleBackColor = true;
            this.Command7.Click += new System.EventHandler(this.Command7_Click);
            // 
            // Command11
            // 
            this.Command11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Command11.BackgroundImage = global::Villamos.Properties.Resources.Calc;
            this.Command11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command11.Location = new System.Drawing.Point(1131, 406);
            this.Command11.Name = "Command11";
            this.Command11.Size = new System.Drawing.Size(45, 45);
            this.Command11.TabIndex = 89;
            this.ToolTip1.SetToolTip(this.Command11, "Összesíti a két táblát adatait.");
            this.Command11.UseVisualStyleBackColor = true;
            this.Command11.Click += new System.EventHandler(this.Command11_Click);
            // 
            // Command18
            // 
            this.Command18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Command18.BackgroundImage = global::Villamos.Properties.Resources.Control_Panel_32;
            this.Command18.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command18.Location = new System.Drawing.Point(945, 6);
            this.Command18.Name = "Command18";
            this.Command18.Size = new System.Drawing.Size(45, 45);
            this.Command18.TabIndex = 88;
            this.ToolTip1.SetToolTip(this.Command18, "Előző napok adataiból választhatunk elemet.");
            this.Command18.UseVisualStyleBackColor = true;
            this.Command18.Click += new System.EventHandler(this.Command18_Click);
            // 
            // Dátum
            // 
            this.Dátum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum.Location = new System.Drawing.Point(945, 58);
            this.Dátum.Name = "Dátum";
            this.Dátum.Size = new System.Drawing.Size(130, 26);
            this.Dátum.TabIndex = 87;
            this.Dátum.ValueChanged += new System.EventHandler(this.Dátum_ValueChanged);
            // 
            // Command6
            // 
            this.Command6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Command6.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Command6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command6.Location = new System.Drawing.Point(1131, 46);
            this.Command6.Name = "Command6";
            this.Command6.Size = new System.Drawing.Size(45, 45);
            this.Command6.TabIndex = 86;
            this.ToolTip1.SetToolTip(this.Command6, "Előkészíti az adott naphoz a képernyőt.");
            this.Command6.UseVisualStyleBackColor = true;
            this.Command6.Click += new System.EventHandler(this.Command6_Click);
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.Location = new System.Drawing.Point(931, 97);
            this.Tábla.Name = "Tábla";
            this.Tábla.RowHeadersVisible = false;
            this.Tábla.Size = new System.Drawing.Size(245, 232);
            this.Tábla.TabIndex = 85;
            // 
            // Command5
            // 
            this.Command5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Command5.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Command5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command5.Location = new System.Drawing.Point(1131, 477);
            this.Command5.Name = "Command5";
            this.Command5.Size = new System.Drawing.Size(45, 45);
            this.Command5.TabIndex = 84;
            this.ToolTip1.SetToolTip(this.Command5, "Rögzíti az adatokat.");
            this.Command5.UseVisualStyleBackColor = true;
            this.Command5.Visible = false;
            this.Command5.Click += new System.EventHandler(this.Command5_Click);
            // 
            // Tábla1
            // 
            this.Tábla1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Tábla1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla1.EnableHeadersVisualStyles = false;
            this.Tábla1.Location = new System.Drawing.Point(5, 5);
            this.Tábla1.Name = "Tábla1";
            this.Tábla1.RowHeadersVisible = false;
            this.Tábla1.Size = new System.Drawing.Size(920, 517);
            this.Tábla1.TabIndex = 0;
            this.Tábla1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Tábla1_CellFormatting);
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.YellowGreen;
            this.TabPage2.Controls.Add(this.Button2);
            this.TabPage2.Controls.Add(this.Option4);
            this.TabPage2.Controls.Add(this.Option3);
            this.TabPage2.Controls.Add(this.Option2);
            this.TabPage2.Controls.Add(this.Option1);
            this.TabPage2.Controls.Add(this.Command12);
            this.TabPage2.Controls.Add(this.Tábla3);
            this.TabPage2.Controls.Add(this.DekádDátum);
            this.TabPage2.Controls.Add(this.Command25);
            this.TabPage2.Controls.Add(this.Excel);
            this.TabPage2.Controls.Add(this.Command14);
            this.TabPage2.Controls.Add(this.Command10);
            this.TabPage2.Controls.Add(this.Command13);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(1182, 532);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Dekád adatok";
            // 
            // Button2
            // 
            this.Button2.BackgroundImage = global::Villamos.Properties.Resources.Document_write;
            this.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button2.Location = new System.Drawing.Point(339, 6);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(45, 45);
            this.Button2.TabIndex = 100;
            this.ToolTip1.SetToolTip(this.Button2, "Rendelés adatokat módosítja.");
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Option4
            // 
            this.Option4.AutoSize = true;
            this.Option4.BackColor = System.Drawing.Color.LightGreen;
            this.Option4.Location = new System.Drawing.Point(711, 15);
            this.Option4.Name = "Option4";
            this.Option4.Size = new System.Drawing.Size(111, 24);
            this.Option4.TabIndex = 99;
            this.Option4.TabStop = true;
            this.Option4.Text = "Havi adatok";
            this.Option4.UseVisualStyleBackColor = false;
            // 
            // Option3
            // 
            this.Option3.AutoSize = true;
            this.Option3.BackColor = System.Drawing.Color.LightGreen;
            this.Option3.Location = new System.Drawing.Point(621, 15);
            this.Option3.Name = "Option3";
            this.Option3.Size = new System.Drawing.Size(84, 24);
            this.Option3.TabIndex = 98;
            this.Option3.TabStop = true;
            this.Option3.Text = "3 dekád";
            this.Option3.UseVisualStyleBackColor = false;
            // 
            // Option2
            // 
            this.Option2.AutoSize = true;
            this.Option2.BackColor = System.Drawing.Color.LightGreen;
            this.Option2.Location = new System.Drawing.Point(531, 15);
            this.Option2.Name = "Option2";
            this.Option2.Size = new System.Drawing.Size(84, 24);
            this.Option2.TabIndex = 97;
            this.Option2.TabStop = true;
            this.Option2.Text = "2 dekád";
            this.Option2.UseVisualStyleBackColor = false;
            // 
            // Option1
            // 
            this.Option1.AutoSize = true;
            this.Option1.BackColor = System.Drawing.Color.LightGreen;
            this.Option1.Location = new System.Drawing.Point(441, 15);
            this.Option1.Name = "Option1";
            this.Option1.Size = new System.Drawing.Size(84, 24);
            this.Option1.TabIndex = 96;
            this.Option1.TabStop = true;
            this.Option1.Text = "1 dekád";
            this.Option1.UseVisualStyleBackColor = false;
            // 
            // Command12
            // 
            this.Command12.BackgroundImage = global::Villamos.Properties.Resources.Fatcow_Farm_Fresh_Sum;
            this.Command12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command12.Location = new System.Drawing.Point(207, 6);
            this.Command12.Name = "Command12";
            this.Command12.Size = new System.Drawing.Size(45, 45);
            this.Command12.TabIndex = 95;
            this.ToolTip1.SetToolTip(this.Command12, "Napi összesített adatokat listázza.");
            this.Command12.UseVisualStyleBackColor = true;
            this.Command12.Click += new System.EventHandler(this.Command12_Click);
            // 
            // Tábla3
            // 
            this.Tábla3.AllowUserToAddRows = false;
            this.Tábla3.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Tábla3.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.Tábla3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Tábla3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla3.EnableHeadersVisualStyles = false;
            this.Tábla3.Location = new System.Drawing.Point(5, 60);
            this.Tábla3.Name = "Tábla3";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla3.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.Tábla3.Size = new System.Drawing.Size(1170, 465);
            this.Tábla3.TabIndex = 91;
            this.Tábla3.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla3_CellClick);
            this.Tábla3.SelectionChanged += new System.EventHandler(this.Tábla3_SelectionChanged);
            // 
            // DekádDátum
            // 
            this.DekádDátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DekádDátum.Location = new System.Drawing.Point(6, 23);
            this.DekádDátum.Name = "DekádDátum";
            this.DekádDátum.Size = new System.Drawing.Size(130, 26);
            this.DekádDátum.TabIndex = 89;
            // 
            // Command25
            // 
            this.Command25.BackgroundImage = global::Villamos.Properties.Resources.CARDFIL3;
            this.Command25.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command25.Location = new System.Drawing.Point(996, 6);
            this.Command25.Name = "Command25";
            this.Command25.Size = new System.Drawing.Size(45, 45);
            this.Command25.TabIndex = 94;
            this.ToolTip1.SetToolTip(this.Command25, "Havi rögzített adatokat listázza.");
            this.Command25.UseVisualStyleBackColor = true;
            this.Command25.Click += new System.EventHandler(this.Command25_Click);
            // 
            // Excel
            // 
            this.Excel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excel.Location = new System.Drawing.Point(1134, 6);
            this.Excel.Name = "Excel";
            this.Excel.Size = new System.Drawing.Size(45, 45);
            this.Excel.TabIndex = 93;
            this.ToolTip1.SetToolTip(this.Excel, "Excel táblába menti a táblázatos részt.");
            this.Excel.UseVisualStyleBackColor = true;
            this.Excel.Click += new System.EventHandler(this.Excel_Click);
            // 
            // Command14
            // 
            this.Command14.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Command14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command14.Location = new System.Drawing.Point(390, 6);
            this.Command14.Name = "Command14";
            this.Command14.Size = new System.Drawing.Size(45, 45);
            this.Command14.TabIndex = 92;
            this.ToolTip1.SetToolTip(this.Command14, "Törli a napi adatokat.");
            this.Command14.UseVisualStyleBackColor = true;
            this.Command14.Click += new System.EventHandler(this.Command14_Click);
            // 
            // Command10
            // 
            this.Command10.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Command10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command10.Location = new System.Drawing.Point(156, 6);
            this.Command10.Name = "Command10";
            this.Command10.Size = new System.Drawing.Size(45, 45);
            this.Command10.TabIndex = 90;
            this.ToolTip1.SetToolTip(this.Command10, "A napnak megfelelő adatokat listázza.");
            this.Command10.UseVisualStyleBackColor = true;
            this.Command10.Click += new System.EventHandler(this.Command10_Click);
            // 
            // Command13
            // 
            this.Command13.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Command13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command13.Location = new System.Drawing.Point(828, 6);
            this.Command13.Name = "Command13";
            this.Command13.Size = new System.Drawing.Size(45, 45);
            this.Command13.TabIndex = 88;
            this.ToolTip1.SetToolTip(this.Command13, "Dekádnak megfelelő összesített adatokat listázza.");
            this.Command13.UseVisualStyleBackColor = true;
            this.Command13.Click += new System.EventHandler(this.Command13_Click);
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.Chocolate;
            this.TabPage3.Controls.Add(this.Feljebb);
            this.TabPage3.Controls.Add(this.Változattörlés);
            this.TabPage3.Controls.Add(this.Command4);
            this.TabPage3.Controls.Add(this.List1);
            this.TabPage3.Controls.Add(this.Text1);
            this.TabPage3.Location = new System.Drawing.Point(4, 29);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(1182, 532);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Napi munkaidő adatok";
            // 
            // Feljebb
            // 
            this.Feljebb.BackgroundImage = global::Villamos.Properties.Resources.Button_Upload_01;
            this.Feljebb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Feljebb.Location = new System.Drawing.Point(361, 116);
            this.Feljebb.Name = "Feljebb";
            this.Feljebb.Size = new System.Drawing.Size(45, 45);
            this.Feljebb.TabIndex = 94;
            this.ToolTip1.SetToolTip(this.Feljebb, "Feljebb visszük a kiválasztott tételt.");
            this.Feljebb.UseVisualStyleBackColor = true;
            this.Feljebb.Click += new System.EventHandler(this.Feljebb_Click);
            // 
            // Változattörlés
            // 
            this.Változattörlés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Változattörlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Változattörlés.Location = new System.Drawing.Point(361, 65);
            this.Változattörlés.Name = "Változattörlés";
            this.Változattörlés.Size = new System.Drawing.Size(45, 45);
            this.Változattörlés.TabIndex = 93;
            this.ToolTip1.SetToolTip(this.Változattörlés, "Töröljük a kiválasztott tételt.");
            this.Változattörlés.UseVisualStyleBackColor = true;
            this.Változattörlés.Click += new System.EventHandler(this.Változattörlés_Click);
            // 
            // Command4
            // 
            this.Command4.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Command4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command4.Location = new System.Drawing.Point(361, 10);
            this.Command4.Name = "Command4";
            this.Command4.Size = new System.Drawing.Size(45, 45);
            this.Command4.TabIndex = 85;
            this.ToolTip1.SetToolTip(this.Command4, "Rögzíti az adatokat.");
            this.Command4.UseVisualStyleBackColor = true;
            this.Command4.Click += new System.EventHandler(this.Command4_Click);
            // 
            // List1
            // 
            this.List1.FormattingEnabled = true;
            this.List1.ItemHeight = 20;
            this.List1.Location = new System.Drawing.Point(179, 65);
            this.List1.Name = "List1";
            this.List1.Size = new System.Drawing.Size(153, 324);
            this.List1.TabIndex = 1;
            // 
            // Text1
            // 
            this.Text1.Location = new System.Drawing.Point(180, 29);
            this.Text1.Name = "Text1";
            this.Text1.Size = new System.Drawing.Size(153, 26);
            this.Text1.TabIndex = 0;
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.Coral;
            this.TabPage4.Controls.Add(this.Napi_id);
            this.TabPage4.Controls.Add(this.Tábla2);
            this.TabPage4.Controls.Add(this.TextPályaszám);
            this.TabPage4.Controls.Add(this.TextMegnevezés);
            this.TabPage4.Controls.Add(this.TextMűvelet);
            this.TabPage4.Controls.Add(this.TextRendelés);
            this.TabPage4.Controls.Add(this.Label14);
            this.TabPage4.Controls.Add(this.Label12);
            this.TabPage4.Controls.Add(this.Label11);
            this.TabPage4.Controls.Add(this.Label10);
            this.TabPage4.Controls.Add(this.Command3);
            this.TabPage4.Controls.Add(this.Command);
            this.TabPage4.Controls.Add(this.Command2);
            this.TabPage4.Location = new System.Drawing.Point(4, 29);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Size = new System.Drawing.Size(1182, 532);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Napi munkaidő lista";
            // 
            // Napi_id
            // 
            this.Napi_id.Location = new System.Drawing.Point(1023, 15);
            this.Napi_id.Name = "Napi_id";
            this.Napi_id.Size = new System.Drawing.Size(147, 26);
            this.Napi_id.TabIndex = 99;
            this.Napi_id.Visible = false;
            // 
            // Tábla2
            // 
            this.Tábla2.AllowUserToAddRows = false;
            this.Tábla2.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Tábla2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.Tábla2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.Tábla2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla2.EnableHeadersVisualStyles = false;
            this.Tábla2.Location = new System.Drawing.Point(5, 5);
            this.Tábla2.Name = "Tábla2";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla2.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            this.Tábla2.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.Tábla2.Size = new System.Drawing.Size(1017, 520);
            this.Tábla2.TabIndex = 98;
            this.Tábla2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla2_CellClick);
            this.Tábla2.SelectionChanged += new System.EventHandler(this.Tábla2_SelectionChanged);
            // 
            // TextPályaszám
            // 
            this.TextPályaszám.Location = new System.Drawing.Point(1027, 241);
            this.TextPályaszám.Name = "TextPályaszám";
            this.TextPályaszám.Size = new System.Drawing.Size(147, 26);
            this.TextPályaszám.TabIndex = 7;
            // 
            // TextMegnevezés
            // 
            this.TextMegnevezés.Location = new System.Drawing.Point(1027, 180);
            this.TextMegnevezés.Name = "TextMegnevezés";
            this.TextMegnevezés.Size = new System.Drawing.Size(147, 26);
            this.TextMegnevezés.TabIndex = 6;
            // 
            // TextMűvelet
            // 
            this.TextMűvelet.Location = new System.Drawing.Point(1027, 128);
            this.TextMűvelet.Name = "TextMűvelet";
            this.TextMűvelet.Size = new System.Drawing.Size(147, 26);
            this.TextMűvelet.TabIndex = 5;
            // 
            // TextRendelés
            // 
            this.TextRendelés.Location = new System.Drawing.Point(1027, 76);
            this.TextRendelés.Name = "TextRendelés";
            this.TextRendelés.Size = new System.Drawing.Size(147, 26);
            this.TextRendelés.TabIndex = 4;
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(1023, 218);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(112, 20);
            this.Label14.TabIndex = 3;
            this.Label14.Text = "Munka és Psz:";
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(1023, 157);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(51, 20);
            this.Label12.TabIndex = 2;
            this.Label12.Text = "Típus:";
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(1023, 105);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(106, 20);
            this.Label11.TabIndex = 1;
            this.Label11.Text = "Műveletszám:";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(1023, 53);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(126, 20);
            this.Label10.TabIndex = 0;
            this.Label10.Text = "Rendelési szám:";
            // 
            // Command3
            // 
            this.Command3.BackgroundImage = global::Villamos.Properties.Resources.Button_Upload_01;
            this.Command3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command3.Location = new System.Drawing.Point(1129, 273);
            this.Command3.Name = "Command3";
            this.Command3.Size = new System.Drawing.Size(45, 45);
            this.Command3.TabIndex = 97;
            this.ToolTip1.SetToolTip(this.Command3, "Feljebb visszük egy sorral a tételt.");
            this.Command3.UseVisualStyleBackColor = true;
            this.Command3.Click += new System.EventHandler(this.Command3_Click);
            // 
            // Command
            // 
            this.Command.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Command.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command.Location = new System.Drawing.Point(1078, 273);
            this.Command.Name = "Command";
            this.Command.Size = new System.Drawing.Size(45, 45);
            this.Command.TabIndex = 96;
            this.ToolTip1.SetToolTip(this.Command, "Törli a kiválasztott adatokat.");
            this.Command.UseVisualStyleBackColor = true;
            this.Command.Click += new System.EventHandler(this.Command_Click);
            // 
            // Command2
            // 
            this.Command2.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Command2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command2.Location = new System.Drawing.Point(1027, 273);
            this.Command2.Name = "Command2";
            this.Command2.Size = new System.Drawing.Size(45, 45);
            this.Command2.TabIndex = 95;
            this.ToolTip1.SetToolTip(this.Command2, "Rögzíti az adatokat.");
            this.Command2.UseVisualStyleBackColor = true;
            this.Command2.Click += new System.EventHandler(this.Command2_Click);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(394, 4);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(708, 28);
            this.Holtart.TabIndex = 67;
            this.Holtart.Visible = false;
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1145, 5);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 59;
            this.ToolTip1.SetToolTip(this.BtnSúgó, "Súgó");
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // Ablak_munkalap_dekádoló
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 616);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Fülek);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_munkalap_dekádoló";
            this.Text = "Munkalap elszámolás";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_munkalap_dekádoló_FormClosed);
            this.Load += new System.EventHandler(this.Ablak_munkalap_dekádoló_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.Fülek.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla1)).EndInit();
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla3)).EndInit();
            this.TabPage3.ResumeLayout(false);
            this.TabPage3.PerformLayout();
            this.TabPage4.ResumeLayout(false);
            this.TabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla2)).EndInit();
            this.ResumeLayout(false);

        }

        internal Panel Panel1;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal Button BtnSúgó;
        internal TabControl Fülek;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal Button Command6;
        internal DataGridView Tábla;
        internal Button Command5;
        internal DataGridView Tábla1;
        internal TabPage TabPage3;
        internal ProgressBar Holtart;
        internal Button Command11;
        internal Button Command18;
        internal DateTimePicker Dátum;
        internal Button Command7;
        internal Button Benn_Lévő;
        internal Button Command24;
        internal Label Label9;
        internal Label Label8;
        internal Button Command14;
        internal DataGridView Tábla3;
        internal Button Command10;
        internal DateTimePicker DekádDátum;
        internal Button Command13;
        internal Button Command25;
        internal Button Excel;
        internal RadioButton Option4;
        internal RadioButton Option3;
        internal RadioButton Option2;
        internal RadioButton Option1;
        internal Button Command12;
        internal TabPage TabPage4;
        internal Button Feljebb;
        internal Button Változattörlés;
        internal Button Command4;
        internal ListBox List1;
        internal TextBox Text1;
        internal DataGridView Tábla2;
        internal Button Command3;
        internal Button Command;
        internal Button Command2;
        internal TextBox TextPályaszám;
        internal TextBox TextMegnevezés;
        internal TextBox TextMűvelet;
        internal TextBox TextRendelés;
        internal Label Label14;
        internal Label Label12;
        internal Label Label11;
        internal Label Label10;
        internal Label FelhasználtText;
        internal Label RendelkezésText;
        internal SaveFileDialog SaveFileDialog1;
        internal Button Button2;
        internal ToolTip ToolTip1;
        internal TextBox Napi_id;
    }
}