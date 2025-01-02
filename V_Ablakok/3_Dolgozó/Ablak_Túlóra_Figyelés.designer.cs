using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
     public partial class Ablak_Túlóra_Figyelés : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Túlóra_Figyelés));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Dátum = new System.Windows.Forms.DateTimePicker();
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.Fülek = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Csoportlista = new System.Windows.Forms.CheckedListBox();
            this.Kilépettjel = new System.Windows.Forms.CheckBox();
            this.Dolgozóvissza = new System.Windows.Forms.Button();
            this.Dolgozókijelölmind = new System.Windows.Forms.Button();
            this.NyitDolgozó = new System.Windows.Forms.Button();
            this.CsukDolgozó = new System.Windows.Forms.Button();
            this.Dolgozónév = new System.Windows.Forms.CheckedListBox();
            this.CsoportFrissít = new System.Windows.Forms.Button();
            this.Tábla3 = new System.Windows.Forms.DataGridView();
            this.Csoportvissza = new System.Windows.Forms.Button();
            this.Label8 = new System.Windows.Forms.Label();
            this.Excel = new System.Windows.Forms.Button();
            this.Csoportkijelölmind = new System.Windows.Forms.Button();
            this.Ellenőrzés = new System.Windows.Forms.Button();
            this.Munkaév = new System.Windows.Forms.TextBox();
            this.NyitCsoport = new System.Windows.Forms.Button();
            this.Label3 = new System.Windows.Forms.Label();
            this.Munka2fél = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Munka1fél = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.CsukCsoport = new System.Windows.Forms.Button();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.Excel_Keret = new System.Windows.Forms.Button();
            this.Tábla2 = new System.Windows.Forms.DataGridView();
            this.Label19 = new System.Windows.Forms.Label();
            this.Command20 = new System.Windows.Forms.Button();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.Excel_Elvont = new System.Windows.Forms.Button();
            this.Rögzítés = new System.Windows.Forms.Button();
            this.Táblakiírás = new System.Windows.Forms.Button();
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.Label9 = new System.Windows.Forms.Label();
            this.Év = new System.Windows.Forms.ComboBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.Csoport = new System.Windows.Forms.ComboBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.TelephelyiVáltozat = new System.Windows.Forms.ComboBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Félév = new System.Windows.Forms.ComboBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Holtart = new V_MindenEgyéb.MyProgressbar();
            this.Panel1.SuspendLayout();
            this.Fülek.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla3)).BeginInit();
            this.TabPage2.SuspendLayout();
            this.Panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla2)).BeginInit();
            this.TabPage3.SuspendLayout();
            this.Panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.Cmbtelephely);
            this.Panel1.Controls.Add(this.Label13);
            this.Panel1.Location = new System.Drawing.Point(8, 8);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(335, 33);
            this.Panel1.TabIndex = 54;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(143, 2);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(3, 6);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(145, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // Dátum
            // 
            this.Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum.Location = new System.Drawing.Point(361, 11);
            this.Dátum.Name = "Dátum";
            this.Dátum.Size = new System.Drawing.Size(127, 26);
            this.Dátum.TabIndex = 55;
            this.Dátum.ValueChanged += new System.EventHandler(this.Dátum_ValueChanged);
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1236, 4);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 57;
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // Fülek
            // 
            this.Fülek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fülek.Controls.Add(this.TabPage1);
            this.Fülek.Controls.Add(this.TabPage2);
            this.Fülek.Controls.Add(this.TabPage3);
            this.Fülek.Location = new System.Drawing.Point(0, 55);
            this.Fülek.Name = "Fülek";
            this.Fülek.Padding = new System.Drawing.Point(16, 3);
            this.Fülek.SelectedIndex = 0;
            this.Fülek.Size = new System.Drawing.Size(1284, 423);
            this.Fülek.TabIndex = 58;
            this.Fülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Fülek_DrawItem);
            this.Fülek.SelectedIndexChanged += new System.EventHandler(this.Fülek_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.Controls.Add(this.Panel2);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1276, 390);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Munkaidő keret ellenőrzés";
            this.TabPage1.UseVisualStyleBackColor = true;
            // 
            // Panel2
            // 
            this.Panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel2.BackColor = System.Drawing.Color.LightGreen;
            this.Panel2.Controls.Add(this.Csoportlista);
            this.Panel2.Controls.Add(this.Kilépettjel);
            this.Panel2.Controls.Add(this.Dolgozóvissza);
            this.Panel2.Controls.Add(this.Dolgozókijelölmind);
            this.Panel2.Controls.Add(this.NyitDolgozó);
            this.Panel2.Controls.Add(this.CsukDolgozó);
            this.Panel2.Controls.Add(this.Dolgozónév);
            this.Panel2.Controls.Add(this.CsoportFrissít);
            this.Panel2.Controls.Add(this.Tábla3);
            this.Panel2.Controls.Add(this.Csoportvissza);
            this.Panel2.Controls.Add(this.Label8);
            this.Panel2.Controls.Add(this.Excel);
            this.Panel2.Controls.Add(this.Csoportkijelölmind);
            this.Panel2.Controls.Add(this.Ellenőrzés);
            this.Panel2.Controls.Add(this.Munkaév);
            this.Panel2.Controls.Add(this.NyitCsoport);
            this.Panel2.Controls.Add(this.Label3);
            this.Panel2.Controls.Add(this.Munka2fél);
            this.Panel2.Controls.Add(this.Label2);
            this.Panel2.Controls.Add(this.Munka1fél);
            this.Panel2.Controls.Add(this.Label1);
            this.Panel2.Controls.Add(this.CsukCsoport);
            this.Panel2.Location = new System.Drawing.Point(0, 0);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(1276, 383);
            this.Panel2.TabIndex = 0;
            // 
            // Csoportlista
            // 
            this.Csoportlista.CheckOnClick = true;
            this.Csoportlista.FormattingEnabled = true;
            this.Csoportlista.Location = new System.Drawing.Point(6, 46);
            this.Csoportlista.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Csoportlista.Name = "Csoportlista";
            this.Csoportlista.Size = new System.Drawing.Size(340, 25);
            this.Csoportlista.TabIndex = 130;
            // 
            // Kilépettjel
            // 
            this.Kilépettjel.AutoSize = true;
            this.Kilépettjel.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.Kilépettjel.Location = new System.Drawing.Point(489, 92);
            this.Kilépettjel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Kilépettjel.Name = "Kilépettjel";
            this.Kilépettjel.Size = new System.Drawing.Size(169, 24);
            this.Kilépettjel.TabIndex = 142;
            this.Kilépettjel.Text = "Kilépett dolgozókkal";
            this.Kilépettjel.UseVisualStyleBackColor = false;
            this.Kilépettjel.CheckedChanged += new System.EventHandler(this.Kilépettjel_CheckedChanged);
            // 
            // Dolgozóvissza
            // 
            this.Dolgozóvissza.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.Dolgozóvissza.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Dolgozóvissza.Location = new System.Drawing.Point(443, 85);
            this.Dolgozóvissza.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Dolgozóvissza.Name = "Dolgozóvissza";
            this.Dolgozóvissza.Size = new System.Drawing.Size(40, 40);
            this.Dolgozóvissza.TabIndex = 136;
            this.Dolgozóvissza.UseVisualStyleBackColor = true;
            this.Dolgozóvissza.Click += new System.EventHandler(this.Dolgozóvissza_Click);
            // 
            // Dolgozókijelölmind
            // 
            this.Dolgozókijelölmind.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.Dolgozókijelölmind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Dolgozókijelölmind.Location = new System.Drawing.Point(397, 85);
            this.Dolgozókijelölmind.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Dolgozókijelölmind.Name = "Dolgozókijelölmind";
            this.Dolgozókijelölmind.Size = new System.Drawing.Size(40, 40);
            this.Dolgozókijelölmind.TabIndex = 135;
            this.Dolgozókijelölmind.UseVisualStyleBackColor = true;
            this.Dolgozókijelölmind.Click += new System.EventHandler(this.Dolgozókijelölmind_Click);
            // 
            // NyitDolgozó
            // 
            this.NyitDolgozó.BackgroundImage = global::Villamos.Properties.Resources.le;
            this.NyitDolgozó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.NyitDolgozó.Location = new System.Drawing.Point(351, 85);
            this.NyitDolgozó.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NyitDolgozó.Name = "NyitDolgozó";
            this.NyitDolgozó.Size = new System.Drawing.Size(40, 40);
            this.NyitDolgozó.TabIndex = 134;
            this.NyitDolgozó.UseVisualStyleBackColor = true;
            this.NyitDolgozó.Click += new System.EventHandler(this.Nyitdolgozó_Click);
            // 
            // CsukDolgozó
            // 
            this.CsukDolgozó.BackgroundImage = global::Villamos.Properties.Resources.fel;
            this.CsukDolgozó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CsukDolgozó.Location = new System.Drawing.Point(351, 85);
            this.CsukDolgozó.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CsukDolgozó.Name = "CsukDolgozó";
            this.CsukDolgozó.Size = new System.Drawing.Size(40, 40);
            this.CsukDolgozó.TabIndex = 140;
            this.CsukDolgozó.UseVisualStyleBackColor = true;
            this.CsukDolgozó.Visible = false;
            this.CsukDolgozó.Click += new System.EventHandler(this.Csukdolgozó_Click);
            // 
            // Dolgozónév
            // 
            this.Dolgozónév.CheckOnClick = true;
            this.Dolgozónév.FormattingEnabled = true;
            this.Dolgozónév.Location = new System.Drawing.Point(6, 92);
            this.Dolgozónév.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Dolgozónév.Name = "Dolgozónév";
            this.Dolgozónév.Size = new System.Drawing.Size(340, 25);
            this.Dolgozónév.TabIndex = 138;
            // 
            // CsoportFrissít
            // 
            this.CsoportFrissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.CsoportFrissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CsoportFrissít.Location = new System.Drawing.Point(489, 39);
            this.CsoportFrissít.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CsoportFrissít.Name = "CsoportFrissít";
            this.CsoportFrissít.Size = new System.Drawing.Size(40, 40);
            this.CsoportFrissít.TabIndex = 141;
            this.CsoportFrissít.UseVisualStyleBackColor = true;
            this.CsoportFrissít.Click += new System.EventHandler(this.CsoportFrissít_Click_1);
            // 
            // Tábla3
            // 
            this.Tábla3.AllowUserToAddRows = false;
            this.Tábla3.AllowUserToDeleteRows = false;
            this.Tábla3.AllowUserToResizeRows = false;
            this.Tábla3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla3.EnableHeadersVisualStyles = false;
            this.Tábla3.Location = new System.Drawing.Point(6, 134);
            this.Tábla3.Name = "Tábla3";
            this.Tábla3.RowHeadersVisible = false;
            this.Tábla3.RowHeadersWidth = 15;
            this.Tábla3.Size = new System.Drawing.Size(1264, 237);
            this.Tábla3.TabIndex = 72;
            // 
            // Csoportvissza
            // 
            this.Csoportvissza.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.Csoportvissza.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Csoportvissza.Location = new System.Drawing.Point(443, 39);
            this.Csoportvissza.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Csoportvissza.Name = "Csoportvissza";
            this.Csoportvissza.Size = new System.Drawing.Size(40, 40);
            this.Csoportvissza.TabIndex = 133;
            this.Csoportvissza.UseVisualStyleBackColor = true;
            this.Csoportvissza.Click += new System.EventHandler(this.Csoportvissza_Click);
            // 
            // Label8
            // 
            this.Label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label8.AutoSize = true;
            this.Label8.BackColor = System.Drawing.Color.LimeGreen;
            this.Label8.Location = new System.Drawing.Point(833, 6);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(434, 80);
            this.Label8.TabIndex = 71;
            this.Label8.Text = "Színmagyarázat:\r\nSárga háttér - az előírányzott értéknél kevesebb a tény érték\r\nZ" +
    "öld háttér - az előírányzott értékkel megegyező a tény érték\r\nPiros háttér - az " +
    "előírányzott értéknél több a tény érték";
            // 
            // Excel
            // 
            this.Excel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excel.Location = new System.Drawing.Point(782, 6);
            this.Excel.Name = "Excel";
            this.Excel.Size = new System.Drawing.Size(45, 45);
            this.Excel.TabIndex = 70;
            this.Excel.UseVisualStyleBackColor = true;
            this.Excel.Click += new System.EventHandler(this.Excel_Click);
            // 
            // Csoportkijelölmind
            // 
            this.Csoportkijelölmind.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.Csoportkijelölmind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Csoportkijelölmind.Location = new System.Drawing.Point(397, 39);
            this.Csoportkijelölmind.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Csoportkijelölmind.Name = "Csoportkijelölmind";
            this.Csoportkijelölmind.Size = new System.Drawing.Size(40, 40);
            this.Csoportkijelölmind.TabIndex = 132;
            this.Csoportkijelölmind.UseVisualStyleBackColor = true;
            this.Csoportkijelölmind.Click += new System.EventHandler(this.Csoportkijelölmind_Click);
            // 
            // Ellenőrzés
            // 
            this.Ellenőrzés.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Ellenőrzés.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Ellenőrzés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Ellenőrzés.Location = new System.Drawing.Point(731, 6);
            this.Ellenőrzés.Name = "Ellenőrzés";
            this.Ellenőrzés.Size = new System.Drawing.Size(45, 45);
            this.Ellenőrzés.TabIndex = 69;
            this.Ellenőrzés.UseVisualStyleBackColor = true;
            this.Ellenőrzés.Click += new System.EventHandler(this.Ellenőrzés_Click);
            // 
            // Munkaév
            // 
            this.Munkaév.Location = new System.Drawing.Point(597, 7);
            this.Munkaév.Name = "Munkaév";
            this.Munkaév.Size = new System.Drawing.Size(128, 26);
            this.Munkaév.TabIndex = 5;
            // 
            // NyitCsoport
            // 
            this.NyitCsoport.BackgroundImage = global::Villamos.Properties.Resources.le;
            this.NyitCsoport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.NyitCsoport.Location = new System.Drawing.Point(351, 39);
            this.NyitCsoport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NyitCsoport.Name = "NyitCsoport";
            this.NyitCsoport.Size = new System.Drawing.Size(40, 40);
            this.NyitCsoport.TabIndex = 131;
            this.NyitCsoport.UseVisualStyleBackColor = true;
            this.NyitCsoport.Click += new System.EventHandler(this.NyitCsoport_Click);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.LimeGreen;
            this.Label3.Location = new System.Drawing.Point(507, 10);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(84, 20);
            this.Label3.TabIndex = 4;
            this.Label3.Text = "Éves keret";
            // 
            // Munka2fél
            // 
            this.Munka2fél.Location = new System.Drawing.Point(373, 6);
            this.Munka2fél.Name = "Munka2fél";
            this.Munka2fél.Size = new System.Drawing.Size(128, 26);
            this.Munka2fél.TabIndex = 3;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.LimeGreen;
            this.Label2.Location = new System.Drawing.Point(257, 12);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(112, 20);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "2 féléves keret";
            // 
            // Munka1fél
            // 
            this.Munka1fél.Location = new System.Drawing.Point(123, 6);
            this.Munka1fél.Name = "Munka1fél";
            this.Munka1fél.Size = new System.Drawing.Size(128, 26);
            this.Munka1fél.TabIndex = 1;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.LimeGreen;
            this.Label1.Location = new System.Drawing.Point(6, 12);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(112, 20);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "1 féléves keret";
            // 
            // CsukCsoport
            // 
            this.CsukCsoport.BackgroundImage = global::Villamos.Properties.Resources.fel;
            this.CsukCsoport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CsukCsoport.Location = new System.Drawing.Point(351, 39);
            this.CsukCsoport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CsukCsoport.Name = "CsukCsoport";
            this.CsukCsoport.Size = new System.Drawing.Size(40, 40);
            this.CsukCsoport.TabIndex = 139;
            this.CsukCsoport.UseVisualStyleBackColor = true;
            this.CsukCsoport.Visible = false;
            this.CsukCsoport.Click += new System.EventHandler(this.CsukCsoport_Click);
            // 
            // TabPage2
            // 
            this.TabPage2.Controls.Add(this.Panel3);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(1276, 390);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Túlóra keret ellenőrzés";
            this.TabPage2.UseVisualStyleBackColor = true;
            // 
            // Panel3
            // 
            this.Panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel3.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.Panel3.Controls.Add(this.Excel_Keret);
            this.Panel3.Controls.Add(this.Tábla2);
            this.Panel3.Controls.Add(this.Label19);
            this.Panel3.Controls.Add(this.Command20);
            this.Panel3.Location = new System.Drawing.Point(0, 0);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(1273, 404);
            this.Panel3.TabIndex = 0;
            // 
            // Excel_Keret
            // 
            this.Excel_Keret.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Excel_Keret.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excel_Keret.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excel_Keret.Location = new System.Drawing.Point(828, 6);
            this.Excel_Keret.Name = "Excel_Keret";
            this.Excel_Keret.Size = new System.Drawing.Size(45, 45);
            this.Excel_Keret.TabIndex = 75;
            this.Excel_Keret.UseVisualStyleBackColor = true;
            this.Excel_Keret.Click += new System.EventHandler(this.Excel_Keret_Click);
            // 
            // Tábla2
            // 
            this.Tábla2.AllowUserToAddRows = false;
            this.Tábla2.AllowUserToDeleteRows = false;
            this.Tábla2.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LimeGreen;
            this.Tábla2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Tábla2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla2.EnableHeadersVisualStyles = false;
            this.Tábla2.Location = new System.Drawing.Point(1, 63);
            this.Tábla2.Name = "Tábla2";
            this.Tábla2.RowHeadersVisible = false;
            this.Tábla2.RowHeadersWidth = 51;
            this.Tábla2.Size = new System.Drawing.Size(1271, 321);
            this.Tábla2.TabIndex = 74;
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.BackColor = System.Drawing.Color.LimeGreen;
            this.Label19.Location = new System.Drawing.Point(74, 11);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(549, 40);
            this.Label19.TabIndex = 73;
            this.Label19.Text = "Színmagyarázat:\r\nPiros háttérszínnel az időarányosnál nagyobb túlóra felhasználás" +
    " van jelezve.";
            // 
            // Command20
            // 
            this.Command20.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Command20.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command20.Location = new System.Drawing.Point(6, 6);
            this.Command20.Name = "Command20";
            this.Command20.Size = new System.Drawing.Size(45, 45);
            this.Command20.TabIndex = 72;
            this.Command20.UseVisualStyleBackColor = true;
            this.Command20.Click += new System.EventHandler(this.Command20_Click);
            // 
            // TabPage3
            // 
            this.TabPage3.Controls.Add(this.Panel4);
            this.TabPage3.Location = new System.Drawing.Point(4, 29);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(1276, 390);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Váltóműszak beosztásból származó elvont pihenő feladás";
            this.TabPage3.UseVisualStyleBackColor = true;
            // 
            // Panel4
            // 
            this.Panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel4.BackColor = System.Drawing.Color.PapayaWhip;
            this.Panel4.Controls.Add(this.Excel_Elvont);
            this.Panel4.Controls.Add(this.Rögzítés);
            this.Panel4.Controls.Add(this.Táblakiírás);
            this.Panel4.Controls.Add(this.Tábla);
            this.Panel4.Controls.Add(this.Label9);
            this.Panel4.Controls.Add(this.Év);
            this.Panel4.Controls.Add(this.Label7);
            this.Panel4.Controls.Add(this.Csoport);
            this.Panel4.Controls.Add(this.Label6);
            this.Panel4.Controls.Add(this.TelephelyiVáltozat);
            this.Panel4.Controls.Add(this.Label5);
            this.Panel4.Controls.Add(this.Félév);
            this.Panel4.Controls.Add(this.Label4);
            this.Panel4.Location = new System.Drawing.Point(0, 0);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(1277, 387);
            this.Panel4.TabIndex = 0;
            // 
            // Excel_Elvont
            // 
            this.Excel_Elvont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Excel_Elvont.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excel_Elvont.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excel_Elvont.Location = new System.Drawing.Point(967, 16);
            this.Excel_Elvont.Name = "Excel_Elvont";
            this.Excel_Elvont.Size = new System.Drawing.Size(45, 45);
            this.Excel_Elvont.TabIndex = 78;
            this.Excel_Elvont.UseVisualStyleBackColor = true;
            this.Excel_Elvont.Click += new System.EventHandler(this.Excel_Elvont_Click);
            // 
            // Rögzítés
            // 
            this.Rögzítés.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Rögzítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Rögzítés.Location = new System.Drawing.Point(719, 16);
            this.Rögzítés.Name = "Rögzítés";
            this.Rögzítés.Size = new System.Drawing.Size(45, 45);
            this.Rögzítés.TabIndex = 77;
            this.Rögzítés.UseVisualStyleBackColor = true;
            this.Rögzítés.Click += new System.EventHandler(this.Rögzítés_Click);
            // 
            // Táblakiírás
            // 
            this.Táblakiírás.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Táblakiírás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Táblakiírás.Location = new System.Drawing.Point(654, 16);
            this.Táblakiírás.Name = "Táblakiírás";
            this.Táblakiírás.Size = new System.Drawing.Size(45, 45);
            this.Táblakiírás.TabIndex = 76;
            this.Táblakiírás.UseVisualStyleBackColor = true;
            this.Táblakiírás.Click += new System.EventHandler(this.Táblakiírás_Click);
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            this.Tábla.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.EnableHeadersVisualStyles = false;
            this.Tábla.Location = new System.Drawing.Point(4, 99);
            this.Tábla.Name = "Tábla";
            this.Tábla.RowHeadersWidth = 51;
            this.Tábla.Size = new System.Drawing.Size(1268, 285);
            this.Tábla.TabIndex = 75;
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.BackColor = System.Drawing.Color.Khaki;
            this.Label9.Location = new System.Drawing.Point(8, 76);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(133, 20);
            this.Label9.TabIndex = 8;
            this.Label9.Text = "Érintett dolgozók:";
            // 
            // Év
            // 
            this.Év.FormattingEnabled = true;
            this.Év.Location = new System.Drawing.Point(8, 33);
            this.Év.Name = "Év";
            this.Év.Size = new System.Drawing.Size(121, 28);
            this.Év.TabIndex = 7;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.BackColor = System.Drawing.Color.Khaki;
            this.Label7.Location = new System.Drawing.Point(262, 10);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(142, 20);
            this.Label7.TabIndex = 6;
            this.Label7.Text = "Telephelyi változat:";
            // 
            // Csoport
            // 
            this.Csoport.FormattingEnabled = true;
            this.Csoport.Location = new System.Drawing.Point(490, 33);
            this.Csoport.Name = "Csoport";
            this.Csoport.Size = new System.Drawing.Size(121, 28);
            this.Csoport.TabIndex = 5;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.BackColor = System.Drawing.Color.Khaki;
            this.Label6.Location = new System.Drawing.Point(490, 10);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(69, 20);
            this.Label6.TabIndex = 4;
            this.Label6.Text = "Csoport:";
            // 
            // TelephelyiVáltozat
            // 
            this.TelephelyiVáltozat.FormattingEnabled = true;
            this.TelephelyiVáltozat.Location = new System.Drawing.Point(262, 33);
            this.TelephelyiVáltozat.Name = "TelephelyiVáltozat";
            this.TelephelyiVáltozat.Size = new System.Drawing.Size(222, 28);
            this.TelephelyiVáltozat.TabIndex = 3;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.BackColor = System.Drawing.Color.Khaki;
            this.Label5.Location = new System.Drawing.Point(135, 10);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(51, 20);
            this.Label5.TabIndex = 2;
            this.Label5.Text = "Félév:";
            // 
            // Félév
            // 
            this.Félév.FormattingEnabled = true;
            this.Félév.Location = new System.Drawing.Point(135, 33);
            this.Félév.Name = "Félév";
            this.Félév.Size = new System.Drawing.Size(121, 28);
            this.Félév.TabIndex = 1;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.BackColor = System.Drawing.Color.Khaki;
            this.Label4.Location = new System.Drawing.Point(8, 10);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(31, 20);
            this.Label4.TabIndex = 0;
            this.Label4.Text = "Év:";
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(500, 10);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(730, 30);
            this.Holtart.TabIndex = 1;
            this.Holtart.Visible = false;
            // 
            // Ablak_Túlóra_Figyelés
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1284, 490);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Fülek);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.Dátum);
            this.Controls.Add(this.Panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Túlóra_Figyelés";
            this.Text = "Munkaidő keret és túlóra ellenőrzés";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_Túlóra_Figyelés_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.Fülek.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla3)).EndInit();
            this.TabPage2.ResumeLayout(false);
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla2)).EndInit();
            this.TabPage3.ResumeLayout(false);
            this.Panel4.ResumeLayout(false);
            this.Panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.ResumeLayout(false);

        }

        internal Panel Panel1;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal DateTimePicker Dátum;
        internal Button BtnSúgó;
        internal TabControl Fülek;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal TabPage TabPage3;
        internal Panel Panel2;
        internal TextBox Munkaév;
        internal Label Label3;
        internal TextBox Munka2fél;
        internal Label Label2;
        internal TextBox Munka1fél;
        internal Label Label1;
        internal Button Ellenőrzés;
        internal DataGridView Tábla3;
        internal Label Label8;
        internal Button Excel;
        internal Panel Panel3;
        internal DataGridView Tábla2;
        internal Label Label19;
        internal Button Command20;
        internal Panel Panel4;
        internal Button Táblakiírás;
        internal DataGridView Tábla;
        internal Label Label9;
        internal ComboBox Év;
        internal Label Label7;
        internal ComboBox Csoport;
        internal Label Label6;
        internal ComboBox TelephelyiVáltozat;
        internal Label Label5;
        internal ComboBox Félév;
        internal Label Label4;
        internal Button Rögzítés;
        internal Button Excel_Keret;
        internal Button Excel_Elvont;
        internal CheckedListBox Dolgozónév;
        internal Button Dolgozóvissza;
        internal Button Dolgozókijelölmind;
        internal Button NyitDolgozó;
        internal Button Csoportvissza;
        internal Button Csoportkijelölmind;
        internal Button NyitCsoport;
        internal CheckedListBox Csoportlista;
        internal Button CsukDolgozó;
        internal Button CsukCsoport;
        internal Button CsoportFrissít;
        internal CheckBox Kilépettjel;
        private V_MindenEgyéb.MyProgressbar Holtart;
    }
}