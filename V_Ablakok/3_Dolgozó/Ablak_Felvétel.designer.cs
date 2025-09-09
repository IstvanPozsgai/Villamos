using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
    
    public partial class Ablak_Felvétel : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Felvétel));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Fülek = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.BtnÚj = new System.Windows.Forms.Button();
            this.Belépésiidő = new System.Windows.Forms.DateTimePicker();
            this.Dolgozónévúj = new System.Windows.Forms.TextBox();
            this.Belépésibér = new System.Windows.Forms.TextBox();
            this.Státusid = new System.Windows.Forms.TextBox();
            this.Dolgozószámúj = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.Telephely = new System.Windows.Forms.TextBox();
            this.Bér = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Kilépésiidő = new System.Windows.Forms.DateTimePicker();
            this.KilépDolgozószám = new System.Windows.Forms.TextBox();
            this.KilépDolgozónév = new System.Windows.Forms.ComboBox();
            this.KilépTelephely = new System.Windows.Forms.ComboBox();
            this.Command7 = new System.Windows.Forms.Button();
            this.Csoportmódosítás = new System.Windows.Forms.Button();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.Command2 = new System.Windows.Forms.Button();
            this.Dolgozóba = new System.Windows.Forms.ComboBox();
            this.Dolgozószámba = new System.Windows.Forms.TextBox();
            this.Hovába = new System.Windows.Forms.TextBox();
            this.Honnanba = new System.Windows.Forms.TextBox();
            this.Label15 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.Panel5 = new System.Windows.Forms.Panel();
            this.Button1 = new System.Windows.Forms.Button();
            this.DolgozószámKi = new System.Windows.Forms.TextBox();
            this.HováKi = new System.Windows.Forms.TextBox();
            this.HonnanKi = new System.Windows.Forms.TextBox();
            this.DolgozóKi = new System.Windows.Forms.ComboBox();
            this.Label19 = new System.Windows.Forms.Label();
            this.Label18 = new System.Windows.Forms.Label();
            this.Label17 = new System.Windows.Forms.Label();
            this.Label16 = new System.Windows.Forms.Label();
            this.TabPage5 = new System.Windows.Forms.TabPage();
            this.Panel6 = new System.Windows.Forms.Panel();
            this.Command4 = new System.Windows.Forms.Button();
            this.Dolgozószámvezénylés = new System.Windows.Forms.TextBox();
            this.Dolgozóvez = new System.Windows.Forms.ComboBox();
            this.Telephová = new System.Windows.Forms.ComboBox();
            this.Telephonnan = new System.Windows.Forms.ComboBox();
            this.Label23 = new System.Windows.Forms.Label();
            this.Label22 = new System.Windows.Forms.Label();
            this.Label21 = new System.Windows.Forms.Label();
            this.Label20 = new System.Windows.Forms.Label();
            this.TabPage6 = new System.Windows.Forms.TabPage();
            this.Panel7 = new System.Windows.Forms.Panel();
            this.Command5 = new System.Windows.Forms.Button();
            this.Label25 = new System.Windows.Forms.Label();
            this.Label24 = new System.Windows.Forms.Label();
            this.Veztörlésdolgozónév = new System.Windows.Forms.ComboBox();
            this.Label22text = new System.Windows.Forms.TextBox();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.Panel1.SuspendLayout();
            this.Fülek.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.TabPage2.SuspendLayout();
            this.Panel3.SuspendLayout();
            this.TabPage3.SuspendLayout();
            this.Panel4.SuspendLayout();
            this.TabPage4.SuspendLayout();
            this.Panel5.SuspendLayout();
            this.TabPage5.SuspendLayout();
            this.Panel6.SuspendLayout();
            this.TabPage6.SuspendLayout();
            this.Panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.Cmbtelephely);
            this.Panel1.Controls.Add(this.Label13);
            this.Panel1.Location = new System.Drawing.Point(5, 5);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(349, 45);
            this.Panel1.TabIndex = 54;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(160, 6);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
            this.Cmbtelephely.SelectionChangeCommitted += new System.EventHandler(this.Cmbtelephely_SelectionChangeCommitted);
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(7, 14);
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
            this.Fülek.Controls.Add(this.TabPage5);
            this.Fülek.Controls.Add(this.TabPage6);
            this.Fülek.Location = new System.Drawing.Point(5, 65);
            this.Fülek.Name = "Fülek";
            this.Fülek.Padding = new System.Drawing.Point(16, 3);
            this.Fülek.SelectedIndex = 0;
            this.Fülek.Size = new System.Drawing.Size(1170, 345);
            this.Fülek.TabIndex = 55;
            this.Fülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Fülek_DrawItem);
            this.Fülek.SelectedIndexChanged += new System.EventHandler(this.Fülek_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.Controls.Add(this.Panel2);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1162, 312);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Dolgozó Felvétel";
            this.TabPage1.UseVisualStyleBackColor = true;
            // 
            // Panel2
            // 
            this.Panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel2.BackColor = System.Drawing.Color.LightGreen;
            this.Panel2.Controls.Add(this.BtnÚj);
            this.Panel2.Controls.Add(this.Belépésiidő);
            this.Panel2.Controls.Add(this.Dolgozónévúj);
            this.Panel2.Controls.Add(this.Belépésibér);
            this.Panel2.Controls.Add(this.Státusid);
            this.Panel2.Controls.Add(this.Dolgozószámúj);
            this.Panel2.Controls.Add(this.Label5);
            this.Panel2.Controls.Add(this.Label4);
            this.Panel2.Controls.Add(this.Label3);
            this.Panel2.Controls.Add(this.Label2);
            this.Panel2.Controls.Add(this.Label1);
            this.Panel2.Location = new System.Drawing.Point(3, 1);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(1156, 308);
            this.Panel2.TabIndex = 0;
            // 
            // BtnÚj
            // 
            this.BtnÚj.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.BtnÚj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnÚj.Location = new System.Drawing.Point(594, 8);
            this.BtnÚj.Name = "BtnÚj";
            this.BtnÚj.Size = new System.Drawing.Size(45, 45);
            this.BtnÚj.TabIndex = 5;
            this.ToolTip1.SetToolTip(this.BtnÚj, "Rögzíti az adatokat");
            this.BtnÚj.UseVisualStyleBackColor = true;
            this.BtnÚj.Click += new System.EventHandler(this.BtnÚj_Click);
            // 
            // Belépésiidő
            // 
            this.Belépésiidő.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Belépésiidő.Location = new System.Drawing.Point(188, 115);
            this.Belépésiidő.Name = "Belépésiidő";
            this.Belépésiidő.Size = new System.Drawing.Size(107, 26);
            this.Belépésiidő.TabIndex = 2;
            // 
            // Dolgozónévúj
            // 
            this.Dolgozónévúj.Location = new System.Drawing.Point(188, 27);
            this.Dolgozónévúj.MaxLength = 50;
            this.Dolgozónévúj.Name = "Dolgozónévúj";
            this.Dolgozónévúj.Size = new System.Drawing.Size(378, 26);
            this.Dolgozónévúj.TabIndex = 0;
            this.ToolTip1.SetToolTip(this.Dolgozónévúj, "Kitöltése kötelező");
            // 
            // Belépésibér
            // 
            this.Belépésibér.Location = new System.Drawing.Point(188, 203);
            this.Belépésibér.Name = "Belépésibér";
            this.Belépésibér.Size = new System.Drawing.Size(149, 26);
            this.Belépésibér.TabIndex = 4;
            this.ToolTip1.SetToolTip(this.Belépésibér, "Kitöltése kötelező! Ha nem ismert a bér, akkor 0.");
            // 
            // Státusid
            // 
            this.Státusid.Location = new System.Drawing.Point(188, 159);
            this.Státusid.Name = "Státusid";
            this.Státusid.Size = new System.Drawing.Size(149, 26);
            this.Státusid.TabIndex = 3;
            this.ToolTip1.SetToolTip(this.Státusid, "Kitöltése kötelező! Ha nincs státus figyelés akkor \"n\" betű irandó");
            // 
            // Dolgozószámúj
            // 
            this.Dolgozószámúj.Location = new System.Drawing.Point(188, 71);
            this.Dolgozószámúj.Name = "Dolgozószámúj";
            this.Dolgozószámúj.Size = new System.Drawing.Size(149, 26);
            this.Dolgozószámúj.TabIndex = 1;
            this.ToolTip1.SetToolTip(this.Dolgozószámúj, "Kitöltése kötelező");
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(13, 209);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(101, 20);
            this.Label5.TabIndex = 4;
            this.Label5.Text = "Belépési bér:";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(13, 165);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(133, 20);
            this.Label4.TabIndex = 3;
            this.Label4.Text = "Státus sorszáma:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(13, 121);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(101, 20);
            this.Label3.TabIndex = 2;
            this.Label3.Text = "Belépési Idő:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(13, 77);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(106, 20);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "HR azonosító";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(13, 33);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(110, 20);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Dolgozó neve:";
            // 
            // TabPage2
            // 
            this.TabPage2.Controls.Add(this.Panel3);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(1162, 312);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Dolgozó Kilépés";
            this.TabPage2.UseVisualStyleBackColor = true;
            // 
            // Panel3
            // 
            this.Panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel3.BackColor = System.Drawing.Color.LightGreen;
            this.Panel3.Controls.Add(this.Telephely);
            this.Panel3.Controls.Add(this.Bér);
            this.Panel3.Controls.Add(this.Label6);
            this.Panel3.Controls.Add(this.Kilépésiidő);
            this.Panel3.Controls.Add(this.KilépDolgozószám);
            this.Panel3.Controls.Add(this.KilépDolgozónév);
            this.Panel3.Controls.Add(this.KilépTelephely);
            this.Panel3.Controls.Add(this.Command7);
            this.Panel3.Controls.Add(this.Csoportmódosítás);
            this.Panel3.Controls.Add(this.Label7);
            this.Panel3.Controls.Add(this.Label8);
            this.Panel3.Controls.Add(this.Label9);
            this.Panel3.Controls.Add(this.Label10);
            this.Panel3.Location = new System.Drawing.Point(0, 0);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(1166, 312);
            this.Panel3.TabIndex = 0;
            // 
            // Telephely
            // 
            this.Telephely.Location = new System.Drawing.Point(128, 194);
            this.Telephely.Name = "Telephely";
            this.Telephely.Size = new System.Drawing.Size(143, 26);
            this.Telephely.TabIndex = 30;
            this.Telephely.Visible = false;
            // 
            // Bér
            // 
            this.Bér.Location = new System.Drawing.Point(128, 226);
            this.Bér.Name = "Bér";
            this.Bér.Size = new System.Drawing.Size(143, 26);
            this.Bér.TabIndex = 29;
            this.Bér.Visible = false;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.BackColor = System.Drawing.Color.Transparent;
            this.Label6.Location = new System.Drawing.Point(124, 17);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(79, 20);
            this.Label6.TabIndex = 28;
            this.Label6.Text = "Kiléptetés";
            // 
            // Kilépésiidő
            // 
            this.Kilépésiidő.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Kilépésiidő.Location = new System.Drawing.Point(128, 150);
            this.Kilépésiidő.Name = "Kilépésiidő";
            this.Kilépésiidő.Size = new System.Drawing.Size(130, 26);
            this.Kilépésiidő.TabIndex = 27;
            // 
            // KilépDolgozószám
            // 
            this.KilépDolgozószám.Enabled = false;
            this.KilépDolgozószám.Location = new System.Drawing.Point(128, 118);
            this.KilépDolgozószám.Name = "KilépDolgozószám";
            this.KilépDolgozószám.Size = new System.Drawing.Size(152, 26);
            this.KilépDolgozószám.TabIndex = 26;
            // 
            // KilépDolgozónév
            // 
            this.KilépDolgozónév.DropDownHeight = 300;
            this.KilépDolgozónév.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.KilépDolgozónév.FormattingEnabled = true;
            this.KilépDolgozónév.IntegralHeight = false;
            this.KilépDolgozónév.Location = new System.Drawing.Point(128, 84);
            this.KilépDolgozónév.Name = "KilépDolgozónév";
            this.KilépDolgozónév.Size = new System.Drawing.Size(431, 28);
            this.KilépDolgozónév.TabIndex = 25;
            this.KilépDolgozónév.SelectedIndexChanged += new System.EventHandler(this.KilépDolgozónév_SelectedIndexChanged);
            // 
            // KilépTelephely
            // 
            this.KilépTelephely.DropDownHeight = 300;
            this.KilépTelephely.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.KilépTelephely.FormattingEnabled = true;
            this.KilépTelephely.IntegralHeight = false;
            this.KilépTelephely.Location = new System.Drawing.Point(128, 50);
            this.KilépTelephely.Name = "KilépTelephely";
            this.KilépTelephely.Size = new System.Drawing.Size(267, 28);
            this.KilépTelephely.TabIndex = 24;
            this.KilépTelephely.Visible = false;
            this.KilépTelephely.SelectedIndexChanged += new System.EventHandler(this.KilépTelephely_SelectedIndexChanged);
            // 
            // Command7
            // 
            this.Command7.BackgroundImage = global::Villamos.Properties.Resources.CALENDR4;
            this.Command7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command7.Location = new System.Drawing.Point(561, 33);
            this.Command7.Name = "Command7";
            this.Command7.Size = new System.Drawing.Size(45, 45);
            this.Command7.TabIndex = 23;
            this.ToolTip1.SetToolTip(this.Command7, "Előzetes kiléptetés/ aktuális kiléptetés váltó");
            this.Command7.UseVisualStyleBackColor = true;
            this.Command7.Click += new System.EventHandler(this.Command7_Click);
            // 
            // Csoportmódosítás
            // 
            this.Csoportmódosítás.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Csoportmódosítás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Csoportmódosítás.Location = new System.Drawing.Point(612, 33);
            this.Csoportmódosítás.Name = "Csoportmódosítás";
            this.Csoportmódosítás.Size = new System.Drawing.Size(45, 45);
            this.Csoportmódosítás.TabIndex = 22;
            this.ToolTip1.SetToolTip(this.Csoportmódosítás, "Rögzíti az adatokat");
            this.Csoportmódosítás.UseVisualStyleBackColor = true;
            this.Csoportmódosítás.Click += new System.EventHandler(this.Csoportmódosítás_Click);
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(10, 156);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(92, 20);
            this.Label7.TabIndex = 19;
            this.Label7.Text = "Kilépési idő:";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(10, 124);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(106, 20);
            this.Label8.TabIndex = 18;
            this.Label8.Text = "HR azonosító";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(10, 92);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(110, 20);
            this.Label9.TabIndex = 17;
            this.Label9.Text = "Dolgozó neve:";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(10, 58);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(80, 20);
            this.Label10.TabIndex = 16;
            this.Label10.Text = "Telephely:";
            this.Label10.Visible = false;
            // 
            // TabPage3
            // 
            this.TabPage3.Controls.Add(this.Panel4);
            this.TabPage3.Location = new System.Drawing.Point(4, 29);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(1162, 312);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Áthelyezés telephelyi állományba";
            this.TabPage3.UseVisualStyleBackColor = true;
            // 
            // Panel4
            // 
            this.Panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel4.BackColor = System.Drawing.Color.LightSkyBlue;
            this.Panel4.Controls.Add(this.Command2);
            this.Panel4.Controls.Add(this.Dolgozóba);
            this.Panel4.Controls.Add(this.Dolgozószámba);
            this.Panel4.Controls.Add(this.Hovába);
            this.Panel4.Controls.Add(this.Honnanba);
            this.Panel4.Controls.Add(this.Label15);
            this.Panel4.Controls.Add(this.Label11);
            this.Panel4.Controls.Add(this.Label14);
            this.Panel4.Controls.Add(this.Label12);
            this.Panel4.Location = new System.Drawing.Point(0, 0);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(1166, 312);
            this.Panel4.TabIndex = 0;
            // 
            // Command2
            // 
            this.Command2.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Command2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command2.Location = new System.Drawing.Point(599, 9);
            this.Command2.Name = "Command2";
            this.Command2.Size = new System.Drawing.Size(45, 45);
            this.Command2.TabIndex = 23;
            this.ToolTip1.SetToolTip(this.Command2, "Rögzíti az adatokat");
            this.Command2.UseVisualStyleBackColor = true;
            this.Command2.Click += new System.EventHandler(this.Command2_Click);
            // 
            // Dolgozóba
            // 
            this.Dolgozóba.DropDownHeight = 300;
            this.Dolgozóba.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Dolgozóba.FormattingEnabled = true;
            this.Dolgozóba.IntegralHeight = false;
            this.Dolgozóba.Location = new System.Drawing.Point(139, 83);
            this.Dolgozóba.Name = "Dolgozóba";
            this.Dolgozóba.Size = new System.Drawing.Size(459, 28);
            this.Dolgozóba.TabIndex = 8;
            this.Dolgozóba.SelectedIndexChanged += new System.EventHandler(this.Dolgozóba_SelectedIndexChanged);
            // 
            // Dolgozószámba
            // 
            this.Dolgozószámba.Enabled = false;
            this.Dolgozószámba.Location = new System.Drawing.Point(139, 119);
            this.Dolgozószámba.Name = "Dolgozószámba";
            this.Dolgozószámba.Size = new System.Drawing.Size(245, 26);
            this.Dolgozószámba.TabIndex = 7;
            // 
            // Hovába
            // 
            this.Hovába.Enabled = false;
            this.Hovába.Location = new System.Drawing.Point(139, 49);
            this.Hovába.Name = "Hovába";
            this.Hovába.Size = new System.Drawing.Size(245, 26);
            this.Hovába.TabIndex = 6;
            // 
            // Honnanba
            // 
            this.Honnanba.Enabled = false;
            this.Honnanba.Location = new System.Drawing.Point(139, 15);
            this.Honnanba.Name = "Honnanba";
            this.Honnanba.Size = new System.Drawing.Size(245, 26);
            this.Honnanba.TabIndex = 5;
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Location = new System.Drawing.Point(15, 129);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(110, 20);
            this.Label15.TabIndex = 4;
            this.Label15.Text = "HR azonosító:";
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(15, 21);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(70, 20);
            this.Label11.TabIndex = 1;
            this.Label11.Text = "Honnan:";
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(15, 93);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(110, 20);
            this.Label14.TabIndex = 3;
            this.Label14.Text = "Dolgozó neve:";
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(15, 57);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(50, 20);
            this.Label12.TabIndex = 2;
            this.Label12.Text = "Hová:";
            // 
            // TabPage4
            // 
            this.TabPage4.Controls.Add(this.Panel5);
            this.TabPage4.Location = new System.Drawing.Point(4, 29);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Size = new System.Drawing.Size(1162, 312);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Telephelyi állományból törlés";
            this.TabPage4.UseVisualStyleBackColor = true;
            // 
            // Panel5
            // 
            this.Panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel5.BackColor = System.Drawing.Color.LightSkyBlue;
            this.Panel5.Controls.Add(this.Button1);
            this.Panel5.Controls.Add(this.DolgozószámKi);
            this.Panel5.Controls.Add(this.HováKi);
            this.Panel5.Controls.Add(this.HonnanKi);
            this.Panel5.Controls.Add(this.DolgozóKi);
            this.Panel5.Controls.Add(this.Label19);
            this.Panel5.Controls.Add(this.Label18);
            this.Panel5.Controls.Add(this.Label17);
            this.Panel5.Controls.Add(this.Label16);
            this.Panel5.Location = new System.Drawing.Point(0, 0);
            this.Panel5.Name = "Panel5";
            this.Panel5.Size = new System.Drawing.Size(1166, 316);
            this.Panel5.TabIndex = 0;
            // 
            // Button1
            // 
            this.Button1.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button1.Location = new System.Drawing.Point(548, 17);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(45, 45);
            this.Button1.TabIndex = 24;
            this.ToolTip1.SetToolTip(this.Button1, "Rögzíti az adatokat");
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // DolgozószámKi
            // 
            this.DolgozószámKi.Enabled = false;
            this.DolgozószámKi.Location = new System.Drawing.Point(139, 121);
            this.DolgozószámKi.Name = "DolgozószámKi";
            this.DolgozószámKi.Size = new System.Drawing.Size(211, 26);
            this.DolgozószámKi.TabIndex = 7;
            // 
            // HováKi
            // 
            this.HováKi.Enabled = false;
            this.HováKi.Location = new System.Drawing.Point(139, 51);
            this.HováKi.Name = "HováKi";
            this.HováKi.Size = new System.Drawing.Size(211, 26);
            this.HováKi.TabIndex = 6;
            // 
            // HonnanKi
            // 
            this.HonnanKi.Enabled = false;
            this.HonnanKi.Location = new System.Drawing.Point(139, 17);
            this.HonnanKi.Name = "HonnanKi";
            this.HonnanKi.Size = new System.Drawing.Size(211, 26);
            this.HonnanKi.TabIndex = 5;
            // 
            // DolgozóKi
            // 
            this.DolgozóKi.DropDownHeight = 300;
            this.DolgozóKi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DolgozóKi.FormattingEnabled = true;
            this.DolgozóKi.IntegralHeight = false;
            this.DolgozóKi.Location = new System.Drawing.Point(139, 85);
            this.DolgozóKi.Name = "DolgozóKi";
            this.DolgozóKi.Size = new System.Drawing.Size(408, 28);
            this.DolgozóKi.TabIndex = 4;
            this.DolgozóKi.SelectedIndexChanged += new System.EventHandler(this.DolgozóKi_SelectedIndexChanged);
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.Location = new System.Drawing.Point(14, 127);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(110, 20);
            this.Label19.TabIndex = 3;
            this.Label19.Text = "HR azonosító:";
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.Location = new System.Drawing.Point(14, 93);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(110, 20);
            this.Label18.TabIndex = 2;
            this.Label18.Text = "Dolgozó neve:";
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.Location = new System.Drawing.Point(14, 57);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(50, 20);
            this.Label17.TabIndex = 1;
            this.Label17.Text = "Hová:";
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Location = new System.Drawing.Point(14, 23);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(70, 20);
            this.Label16.TabIndex = 0;
            this.Label16.Text = "Honnan:";
            // 
            // TabPage5
            // 
            this.TabPage5.Controls.Add(this.Panel6);
            this.TabPage5.Location = new System.Drawing.Point(4, 29);
            this.TabPage5.Name = "TabPage5";
            this.TabPage5.Size = new System.Drawing.Size(1162, 312);
            this.TabPage5.TabIndex = 4;
            this.TabPage5.Text = "Vezénylés létrehozás";
            this.TabPage5.UseVisualStyleBackColor = true;
            // 
            // Panel6
            // 
            this.Panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel6.BackColor = System.Drawing.Color.LightGray;
            this.Panel6.Controls.Add(this.Command4);
            this.Panel6.Controls.Add(this.Dolgozószámvezénylés);
            this.Panel6.Controls.Add(this.Dolgozóvez);
            this.Panel6.Controls.Add(this.Telephová);
            this.Panel6.Controls.Add(this.Telephonnan);
            this.Panel6.Controls.Add(this.Label23);
            this.Panel6.Controls.Add(this.Label22);
            this.Panel6.Controls.Add(this.Label21);
            this.Panel6.Controls.Add(this.Label20);
            this.Panel6.Location = new System.Drawing.Point(0, 0);
            this.Panel6.Name = "Panel6";
            this.Panel6.Size = new System.Drawing.Size(1166, 312);
            this.Panel6.TabIndex = 0;
            // 
            // Command4
            // 
            this.Command4.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Command4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command4.Location = new System.Drawing.Point(727, 10);
            this.Command4.Name = "Command4";
            this.Command4.Size = new System.Drawing.Size(45, 45);
            this.Command4.TabIndex = 25;
            this.ToolTip1.SetToolTip(this.Command4, "Rögzíti az adatokat");
            this.Command4.UseVisualStyleBackColor = true;
            this.Command4.Click += new System.EventHandler(this.Command4_Click);
            // 
            // Dolgozószámvezénylés
            // 
            this.Dolgozószámvezénylés.Location = new System.Drawing.Point(144, 142);
            this.Dolgozószámvezénylés.Name = "Dolgozószámvezénylés";
            this.Dolgozószámvezénylés.Size = new System.Drawing.Size(130, 26);
            this.Dolgozószámvezénylés.TabIndex = 7;
            // 
            // Dolgozóvez
            // 
            this.Dolgozóvez.DropDownHeight = 300;
            this.Dolgozóvez.FormattingEnabled = true;
            this.Dolgozóvez.IntegralHeight = false;
            this.Dolgozóvez.Location = new System.Drawing.Point(144, 98);
            this.Dolgozóvez.Name = "Dolgozóvez";
            this.Dolgozóvez.Size = new System.Drawing.Size(561, 28);
            this.Dolgozóvez.TabIndex = 6;
            this.Dolgozóvez.SelectedIndexChanged += new System.EventHandler(this.Dolgozóvez_SelectedIndexChanged);
            // 
            // Telephová
            // 
            this.Telephová.FormattingEnabled = true;
            this.Telephová.Location = new System.Drawing.Point(144, 54);
            this.Telephová.Name = "Telephová";
            this.Telephová.Size = new System.Drawing.Size(221, 28);
            this.Telephová.TabIndex = 5;
            // 
            // Telephonnan
            // 
            this.Telephonnan.FormattingEnabled = true;
            this.Telephonnan.Location = new System.Drawing.Point(144, 10);
            this.Telephonnan.Name = "Telephonnan";
            this.Telephonnan.Size = new System.Drawing.Size(221, 28);
            this.Telephonnan.TabIndex = 4;
            this.Telephonnan.SelectedIndexChanged += new System.EventHandler(this.Telephonnan_SelectedIndexChanged);
            // 
            // Label23
            // 
            this.Label23.AutoSize = true;
            this.Label23.Location = new System.Drawing.Point(8, 145);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(106, 20);
            this.Label23.TabIndex = 3;
            this.Label23.Text = "HR azonosító";
            // 
            // Label22
            // 
            this.Label22.AutoSize = true;
            this.Label22.Location = new System.Drawing.Point(8, 62);
            this.Label22.Name = "Label22";
            this.Label22.Size = new System.Drawing.Size(50, 20);
            this.Label22.TabIndex = 2;
            this.Label22.Text = "Hová:";
            // 
            // Label21
            // 
            this.Label21.AutoSize = true;
            this.Label21.Location = new System.Drawing.Point(8, 106);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(110, 20);
            this.Label21.TabIndex = 1;
            this.Label21.Text = "Dolgozó neve:";
            // 
            // Label20
            // 
            this.Label20.AutoSize = true;
            this.Label20.Location = new System.Drawing.Point(8, 18);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(70, 20);
            this.Label20.TabIndex = 0;
            this.Label20.Text = "Honnan:";
            // 
            // TabPage6
            // 
            this.TabPage6.Controls.Add(this.Panel7);
            this.TabPage6.Location = new System.Drawing.Point(4, 29);
            this.TabPage6.Name = "TabPage6";
            this.TabPage6.Size = new System.Drawing.Size(1162, 312);
            this.TabPage6.TabIndex = 5;
            this.TabPage6.Text = "Vezénylés törlés";
            this.TabPage6.UseVisualStyleBackColor = true;
            // 
            // Panel7
            // 
            this.Panel7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel7.BackColor = System.Drawing.Color.LightGray;
            this.Panel7.Controls.Add(this.Command5);
            this.Panel7.Controls.Add(this.Label25);
            this.Panel7.Controls.Add(this.Label24);
            this.Panel7.Controls.Add(this.Veztörlésdolgozónév);
            this.Panel7.Controls.Add(this.Label22text);
            this.Panel7.Location = new System.Drawing.Point(0, 0);
            this.Panel7.Name = "Panel7";
            this.Panel7.Size = new System.Drawing.Size(1166, 316);
            this.Panel7.TabIndex = 0;
            // 
            // Command5
            // 
            this.Command5.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Command5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command5.Location = new System.Drawing.Point(770, 7);
            this.Command5.Name = "Command5";
            this.Command5.Size = new System.Drawing.Size(45, 45);
            this.Command5.TabIndex = 26;
            this.ToolTip1.SetToolTip(this.Command5, "Rögzíti az adatokat");
            this.Command5.UseVisualStyleBackColor = true;
            this.Command5.Click += new System.EventHandler(this.Command5_Click);
            // 
            // Label25
            // 
            this.Label25.AutoSize = true;
            this.Label25.Location = new System.Drawing.Point(8, 24);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(110, 20);
            this.Label25.TabIndex = 3;
            this.Label25.Text = "Dolgozó neve:";
            // 
            // Label24
            // 
            this.Label24.AutoSize = true;
            this.Label24.Location = new System.Drawing.Point(8, 66);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(110, 20);
            this.Label24.TabIndex = 2;
            this.Label24.Text = "HR azonosító:";
            // 
            // Veztörlésdolgozónév
            // 
            this.Veztörlésdolgozónév.DropDownHeight = 300;
            this.Veztörlésdolgozónév.FormattingEnabled = true;
            this.Veztörlésdolgozónév.IntegralHeight = false;
            this.Veztörlésdolgozónév.Location = new System.Drawing.Point(198, 16);
            this.Veztörlésdolgozónév.Name = "Veztörlésdolgozónév";
            this.Veztörlésdolgozónév.Size = new System.Drawing.Size(498, 28);
            this.Veztörlésdolgozónév.TabIndex = 1;
            this.Veztörlésdolgozónév.SelectedIndexChanged += new System.EventHandler(this.Veztörlésdolgozónév_SelectedIndexChanged);
            // 
            // Label22text
            // 
            this.Label22text.Enabled = false;
            this.Label22text.Location = new System.Drawing.Point(198, 60);
            this.Label22text.Name = "Label22text";
            this.Label22text.Size = new System.Drawing.Size(164, 26);
            this.Label22text.TabIndex = 0;
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1135, 5);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 56;
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // Ablak_Felvétel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkOrange;
            this.ClientSize = new System.Drawing.Size(1183, 414);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.Fülek);
            this.Controls.Add(this.Panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Felvétel";
            this.Text = "Dolgozó felvétel-átvétel- belépés";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AblakFelvétel_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.Fülek.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.TabPage2.ResumeLayout(false);
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            this.TabPage3.ResumeLayout(false);
            this.Panel4.ResumeLayout(false);
            this.Panel4.PerformLayout();
            this.TabPage4.ResumeLayout(false);
            this.Panel5.ResumeLayout(false);
            this.Panel5.PerformLayout();
            this.TabPage5.ResumeLayout(false);
            this.Panel6.ResumeLayout(false);
            this.Panel6.PerformLayout();
            this.TabPage6.ResumeLayout(false);
            this.Panel7.ResumeLayout(false);
            this.Panel7.PerformLayout();
            this.ResumeLayout(false);

        }

        internal Panel Panel1;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal TabControl Fülek;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal Panel Panel2;
        internal TextBox Dolgozónévúj;
        internal TextBox Belépésibér;
        internal TextBox Státusid;
        internal TextBox Dolgozószámúj;
        internal Label Label5;
        internal Label Label4;
        internal Label Label3;
        internal Label Label2;
        internal Label Label1;
        internal TabPage TabPage3;
        internal TabPage TabPage4;
        internal TabPage TabPage5;
        internal TabPage TabPage6;
        internal Button BtnSúgó;
        internal DateTimePicker Belépésiidő;
        internal Button BtnÚj;
        internal ToolTip ToolTip1;
        internal Panel Panel3;
        internal Button Command7;
        internal Button Csoportmódosítás;
        internal Label Label7;
        internal Label Label8;
        internal Label Label9;
        internal Label Label10;
        internal Label Label6;
        internal DateTimePicker Kilépésiidő;
        internal TextBox KilépDolgozószám;
        internal ComboBox KilépDolgozónév;
        internal ComboBox KilépTelephely;
        internal TextBox Telephely;
        internal TextBox Bér;
        internal Panel Panel4;
        internal Button Command2;
        internal ComboBox Dolgozóba;
        internal TextBox Dolgozószámba;
        internal TextBox Hovába;
        internal TextBox Honnanba;
        internal Label Label15;
        internal Label Label11;
        internal Label Label14;
        internal Label Label12;
        internal Panel Panel5;
        internal TextBox DolgozószámKi;
        internal TextBox HováKi;
        internal TextBox HonnanKi;
        internal ComboBox DolgozóKi;
        internal Label Label19;
        internal Label Label18;
        internal Label Label17;
        internal Label Label16;
        internal Button Button1;
        internal Panel Panel6;
        internal Button Command4;
        internal TextBox Dolgozószámvezénylés;
        internal ComboBox Dolgozóvez;
        internal ComboBox Telephová;
        internal ComboBox Telephonnan;
        internal Label Label23;
        internal Label Label22;
        internal Label Label21;
        internal Label Label20;
        internal Panel Panel7;
        internal Button Command5;
        internal Label Label25;
        internal Label Label24;
        internal ComboBox Veztörlésdolgozónév;
        internal TextBox Label22text;
    }
}