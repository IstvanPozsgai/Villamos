using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
    public partial class Ablak_Digitális_Főkönyv : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components!= null)
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Digitális_Főkönyv));
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Label5 = new System.Windows.Forms.Label();
            this.Délután = new System.Windows.Forms.RadioButton();
            this.Délelőtt = new System.Windows.Forms.RadioButton();
            this.Dátum = new System.Windows.Forms.DateTimePicker();
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.Tábla2 = new System.Windows.Forms.DataGridView();
            this.Tábla1 = new System.Windows.Forms.DataGridView();
            this.Tábla3 = new System.Windows.Forms.DataGridView();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.Option1 = new System.Windows.Forms.RadioButton();
            this.Option4 = new System.Windows.Forms.RadioButton();
            this.Option2 = new System.Windows.Forms.RadioButton();
            this.Option3 = new System.Windows.Forms.RadioButton();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Command4 = new System.Windows.Forms.Button();
            this.Dátum_Melyik = new System.Windows.Forms.DateTimePicker();
            this.Becsukja = new System.Windows.Forms.Button();
            this.BtnKeres_command2 = new System.Windows.Forms.Button();
            this.Excel_Melyik = new System.Windows.Forms.Button();
            this.Keresés = new System.Windows.Forms.Button();
            this.Command3 = new System.Windows.Forms.Button();
            this.CsoportkijelölMind = new System.Windows.Forms.Button();
            this.CsoportVissza = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.Járgomb = new System.Windows.Forms.Button();
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.GombTároló = new System.Windows.Forms.Panel();
            this.Választott_Nap = new System.Windows.Forms.TextBox();
            this.Választott_napszak = new System.Windows.Forms.TextBox();
            this.Választott_Telephely = new System.Windows.Forms.TextBox();
            this.Panel5 = new System.Windows.Forms.Panel();
            this.Panel7 = new System.Windows.Forms.Panel();
            this.RadioButton1 = new System.Windows.Forms.RadioButton();
            this.RadioButton2 = new System.Windows.Forms.RadioButton();
            this.Panel6 = new System.Windows.Forms.Panel();
            this.RadioButton3 = new System.Windows.Forms.RadioButton();
            this.RadioButton4 = new System.Windows.Forms.RadioButton();
            this.Típuslista = new System.Windows.Forms.CheckedListBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Telephelykönyvtár = new System.Windows.Forms.ComboBox();
            this.Kereső = new System.Windows.Forms.GroupBox();
            this.Chk_CTRL = new System.Windows.Forms.CheckBox();
            this.Keresőnév = new System.Windows.Forms.Label();
            this.TextKeres_Text = new System.Windows.Forms.TextBox();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla3)).BeginInit();
            this.Panel3.SuspendLayout();
            this.Panel5.SuspendLayout();
            this.Panel7.SuspendLayout();
            this.Panel6.SuspendLayout();
            this.Kereső.SuspendLayout();
            this.SuspendLayout();
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.BackColor = System.Drawing.Color.LightGreen;
            this.Holtart.ForeColor = System.Drawing.Color.Green;
            this.Holtart.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Holtart.Location = new System.Drawing.Point(114, 188);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(815, 23);
            this.Holtart.TabIndex = 81;
            this.Holtart.Visible = false;
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.Goldenrod;
            this.Panel1.Controls.Add(this.Label5);
            this.Panel1.Controls.Add(this.Délután);
            this.Panel1.Controls.Add(this.Délelőtt);
            this.Panel1.Controls.Add(this.Dátum);
            this.Panel1.Location = new System.Drawing.Point(10, 63);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(242, 130);
            this.Panel1.TabIndex = 85;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(5, 5);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(142, 20);
            this.Label5.TabIndex = 3;
            this.Label5.Text = "Dátum és napszak";
            // 
            // Délután
            // 
            this.Délután.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.Délután.Location = new System.Drawing.Point(14, 97);
            this.Délután.Name = "Délután";
            this.Délután.Size = new System.Drawing.Size(121, 24);
            this.Délután.TabIndex = 2;
            this.Délután.TabStop = true;
            this.Délután.Text = "Délután";
            this.Délután.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Délután.UseVisualStyleBackColor = false;
            this.Délután.CheckedChanged += new System.EventHandler(this.Délután_CheckedChanged);
            // 
            // Délelőtt
            // 
            this.Délelőtt.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.Délelőtt.Checked = true;
            this.Délelőtt.Location = new System.Drawing.Point(15, 67);
            this.Délelőtt.Name = "Délelőtt";
            this.Délelőtt.Size = new System.Drawing.Size(120, 24);
            this.Délelőtt.TabIndex = 1;
            this.Délelőtt.TabStop = true;
            this.Délelőtt.Text = "Délelőtt";
            this.Délelőtt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Délelőtt.UseVisualStyleBackColor = false;
            this.Délelőtt.CheckedChanged += new System.EventHandler(this.Délelőtt_CheckedChanged);
            // 
            // Dátum
            // 
            this.Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum.Location = new System.Drawing.Point(14, 35);
            this.Dátum.Name = "Dátum";
            this.Dátum.Size = new System.Drawing.Size(120, 26);
            this.Dátum.TabIndex = 0;
            this.Dátum.Value = new System.DateTime(2020, 2, 17, 17, 13, 0, 0);
            this.Dátum.ValueChanged += new System.EventHandler(this.Dátum_ValueChanged);
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla.BackgroundColor = System.Drawing.Color.Aqua;
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.Location = new System.Drawing.Point(258, 60);
            this.Tábla.Name = "Tábla";
            this.Tábla.RowHeadersVisible = false;
            this.Tábla.Size = new System.Drawing.Size(740, 442);
            this.Tábla.TabIndex = 86;
            this.Tábla.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Tábla_CellFormatting);
            // 
            // Tábla2
            // 
            this.Tábla2.AllowUserToAddRows = false;
            this.Tábla2.AllowUserToDeleteRows = false;
            this.Tábla2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla2.Location = new System.Drawing.Point(258, 60);
            this.Tábla2.Name = "Tábla2";
            this.Tábla2.RowHeadersVisible = false;
            this.Tábla2.Size = new System.Drawing.Size(740, 442);
            this.Tábla2.TabIndex = 87;
            this.Tábla2.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Tábla2_CellFormatting);
            // 
            // Tábla1
            // 
            this.Tábla1.AllowUserToAddRows = false;
            this.Tábla1.AllowUserToDeleteRows = false;
            this.Tábla1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla1.BackgroundColor = System.Drawing.SystemColors.HotTrack;
            this.Tábla1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla1.Location = new System.Drawing.Point(258, 60);
            this.Tábla1.Name = "Tábla1";
            this.Tábla1.RowHeadersVisible = false;
            this.Tábla1.Size = new System.Drawing.Size(740, 442);
            this.Tábla1.TabIndex = 88;
            // 
            // Tábla3
            // 
            this.Tábla3.AllowUserToAddRows = false;
            this.Tábla3.AllowUserToDeleteRows = false;
            this.Tábla3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla3.BackgroundColor = System.Drawing.Color.MediumSpringGreen;
            this.Tábla3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla3.Location = new System.Drawing.Point(258, 61);
            this.Tábla3.Name = "Tábla3";
            this.Tábla3.RowHeadersVisible = false;
            this.Tábla3.Size = new System.Drawing.Size(740, 442);
            this.Tábla3.TabIndex = 89;
            // 
            // Panel3
            // 
            this.Panel3.BackColor = System.Drawing.Color.Goldenrod;
            this.Panel3.Controls.Add(this.Option1);
            this.Panel3.Controls.Add(this.Option4);
            this.Panel3.Controls.Add(this.Option2);
            this.Panel3.Controls.Add(this.Option3);
            this.Panel3.Location = new System.Drawing.Point(589, 9);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(401, 40);
            this.Panel3.TabIndex = 91;
            // 
            // Option1
            // 
            this.Option1.AutoSize = true;
            this.Option1.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.Option1.Checked = true;
            this.Option1.Location = new System.Drawing.Point(16, 9);
            this.Option1.Name = "Option1";
            this.Option1.Size = new System.Drawing.Size(75, 24);
            this.Option1.TabIndex = 5;
            this.Option1.TabStop = true;
            this.Option1.Text = "Kiadás";
            this.Option1.UseVisualStyleBackColor = false;
            this.Option1.CheckedChanged += new System.EventHandler(this.Option1_CheckedChanged);
            // 
            // Option4
            // 
            this.Option4.AutoSize = true;
            this.Option4.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.Option4.Location = new System.Drawing.Point(97, 9);
            this.Option4.Name = "Option4";
            this.Option4.Size = new System.Drawing.Size(84, 24);
            this.Option4.TabIndex = 4;
            this.Option4.Text = "Tartalék";
            this.Option4.UseVisualStyleBackColor = false;
            this.Option4.CheckedChanged += new System.EventHandler(this.Option4_CheckedChanged);
            // 
            // Option2
            // 
            this.Option2.AutoSize = true;
            this.Option2.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.Option2.Location = new System.Drawing.Point(187, 9);
            this.Option2.Name = "Option2";
            this.Option2.Size = new System.Drawing.Size(95, 24);
            this.Option2.TabIndex = 3;
            this.Option2.TabStop = true;
            this.Option2.Text = "Javítandó";
            this.Option2.UseVisualStyleBackColor = false;
            this.Option2.CheckedChanged += new System.EventHandler(this.Option2_CheckedChanged);
            // 
            // Option3
            // 
            this.Option3.AutoSize = true;
            this.Option3.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.Option3.Location = new System.Drawing.Point(288, 9);
            this.Option3.Name = "Option3";
            this.Option3.Size = new System.Drawing.Size(97, 24);
            this.Option3.TabIndex = 2;
            this.Option3.TabStop = true;
            this.Option3.Text = "Összesítő";
            this.Option3.UseVisualStyleBackColor = false;
            this.Option3.CheckedChanged += new System.EventHandler(this.Option3_CheckedChanged);
            // 
            // ToolTip1
            // 
            this.ToolTip1.IsBalloon = true;
            // 
            // Command4
            // 
            this.Command4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Command4.BackgroundImage = global::Villamos.Properties.Resources.Document_write;
            this.Command4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command4.Location = new System.Drawing.Point(192, 355);
            this.Command4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Command4.Name = "Command4";
            this.Command4.Size = new System.Drawing.Size(40, 40);
            this.Command4.TabIndex = 178;
            this.ToolTip1.SetToolTip(this.Command4, "Adott nap kiválasztott típus járműveinek státusa");
            this.Command4.UseVisualStyleBackColor = true;
            this.Command4.Click += new System.EventHandler(this.Command4_Click);
            // 
            // Dátum_Melyik
            // 
            this.Dátum_Melyik.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Dátum_Melyik.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum_Melyik.Location = new System.Drawing.Point(9, 365);
            this.Dátum_Melyik.Name = "Dátum_Melyik";
            this.Dátum_Melyik.Size = new System.Drawing.Size(109, 26);
            this.Dátum_Melyik.TabIndex = 174;
            this.ToolTip1.SetToolTip(this.Dátum_Melyik, "Dátum választó");
            // 
            // Becsukja
            // 
            this.Becsukja.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Becsukja.BackgroundImage = global::Villamos.Properties.Resources.bezár;
            this.Becsukja.Location = new System.Drawing.Point(206, 10);
            this.Becsukja.Margin = new System.Windows.Forms.Padding(4);
            this.Becsukja.Name = "Becsukja";
            this.Becsukja.Size = new System.Drawing.Size(35, 34);
            this.Becsukja.TabIndex = 57;
            this.ToolTip1.SetToolTip(this.Becsukja, "Bezárja a kereső ablakot");
            this.Becsukja.UseVisualStyleBackColor = true;
            this.Becsukja.Click += new System.EventHandler(this.Becsukja_Click);
            // 
            // BtnKeres_command2
            // 
            this.BtnKeres_command2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnKeres_command2.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.BtnKeres_command2.Location = new System.Drawing.Point(155, 29);
            this.BtnKeres_command2.Margin = new System.Windows.Forms.Padding(4);
            this.BtnKeres_command2.Name = "BtnKeres_command2";
            this.BtnKeres_command2.Size = new System.Drawing.Size(40, 40);
            this.BtnKeres_command2.TabIndex = 56;
            this.ToolTip1.SetToolTip(this.BtnKeres_command2, "Megkeresi a beírt pályaszámot");
            this.BtnKeres_command2.UseVisualStyleBackColor = true;
            this.BtnKeres_command2.Click += new System.EventHandler(this.BtnKeres_command2_Click);
            // 
            // Excel_Melyik
            // 
            this.Excel_Melyik.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Excel_Melyik.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excel_Melyik.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excel_Melyik.Location = new System.Drawing.Point(192, 244);
            this.Excel_Melyik.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Excel_Melyik.Name = "Excel_Melyik";
            this.Excel_Melyik.Size = new System.Drawing.Size(40, 40);
            this.Excel_Melyik.TabIndex = 180;
            this.ToolTip1.SetToolTip(this.Excel_Melyik, "Excel táblázatot készít a táblázat adataiból");
            this.Excel_Melyik.UseVisualStyleBackColor = true;
            this.Excel_Melyik.Click += new System.EventHandler(this.Excel_Melyik_Click);
            // 
            // Keresés
            // 
            this.Keresés.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Keresés.BackgroundImage = global::Villamos.Properties.Resources.Nagyító;
            this.Keresés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Keresés.Location = new System.Drawing.Point(192, 192);
            this.Keresés.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Keresés.Name = "Keresés";
            this.Keresés.Size = new System.Drawing.Size(40, 40);
            this.Keresés.TabIndex = 179;
            this.ToolTip1.SetToolTip(this.Keresés, "Frissíti a listákat");
            this.Keresés.UseVisualStyleBackColor = true;
            // 
            // Command3
            // 
            this.Command3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Command3.BackgroundImage = global::Villamos.Properties.Resources.Treetog_Junior_Document_scroll;
            this.Command3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command3.Location = new System.Drawing.Point(192, 290);
            this.Command3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Command3.Name = "Command3";
            this.Command3.Size = new System.Drawing.Size(40, 40);
            this.Command3.TabIndex = 177;
            this.ToolTip1.SetToolTip(this.Command3, "Adott napi forgalmi és műszaki adatok");
            this.Command3.UseVisualStyleBackColor = true;
            this.Command3.Click += new System.EventHandler(this.Command3_Click);
            // 
            // CsoportkijelölMind
            // 
            this.CsoportkijelölMind.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.CsoportkijelölMind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CsoportkijelölMind.Location = new System.Drawing.Point(192, 28);
            this.CsoportkijelölMind.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CsoportkijelölMind.Name = "CsoportkijelölMind";
            this.CsoportkijelölMind.Size = new System.Drawing.Size(40, 40);
            this.CsoportkijelölMind.TabIndex = 171;
            this.ToolTip1.SetToolTip(this.CsoportkijelölMind, "Mindent kijelöl");
            this.CsoportkijelölMind.UseVisualStyleBackColor = true;
            this.CsoportkijelölMind.Click += new System.EventHandler(this.CsoportkijelölMind_Click);
            // 
            // CsoportVissza
            // 
            this.CsoportVissza.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.CsoportVissza.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CsoportVissza.Location = new System.Drawing.Point(192, 78);
            this.CsoportVissza.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CsoportVissza.Name = "CsoportVissza";
            this.CsoportVissza.Size = new System.Drawing.Size(40, 40);
            this.CsoportVissza.TabIndex = 172;
            this.ToolTip1.SetToolTip(this.CsoportVissza, "Minden kijelölést töröl");
            this.CsoportVissza.UseVisualStyleBackColor = true;
            this.CsoportVissza.Click += new System.EventHandler(this.CsoportVissza_Click);
            // 
            // Button1
            // 
            this.Button1.BackgroundImage = global::Villamos.Properties.Resources.page_swap_32;
            this.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button1.Location = new System.Drawing.Point(114, 12);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(45, 45);
            this.Button1.TabIndex = 97;
            this.ToolTip1.SetToolTip(this.Button1, "Váltás a digitális főkönyv és típus szerinti lekérdezés között");
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Járgomb
            // 
            this.Járgomb.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Járgomb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Járgomb.Location = new System.Drawing.Point(63, 12);
            this.Járgomb.Name = "Járgomb";
            this.Járgomb.Size = new System.Drawing.Size(45, 45);
            this.Járgomb.TabIndex = 92;
            this.ToolTip1.SetToolTip(this.Járgomb, "Frissíti a táblázat adatait");
            this.Járgomb.UseVisualStyleBackColor = true;
            this.Járgomb.Click += new System.EventHandler(this.Járgomb_Click);
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(12, 12);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 80;
            this.ToolTip1.SetToolTip(this.BtnSúgó, "Súgó");
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // GombTároló
            // 
            this.GombTároló.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.GombTároló.BackColor = System.Drawing.Color.Goldenrod;
            this.GombTároló.Location = new System.Drawing.Point(10, 199);
            this.GombTároló.Name = "GombTároló";
            this.GombTároló.Size = new System.Drawing.Size(242, 304);
            this.GombTároló.TabIndex = 93;
            // 
            // Választott_Nap
            // 
            this.Választott_Nap.BackColor = System.Drawing.Color.Gold;
            this.Választott_Nap.Enabled = false;
            this.Választott_Nap.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Választott_Nap.Location = new System.Drawing.Point(180, 18);
            this.Választott_Nap.Name = "Választott_Nap";
            this.Választott_Nap.Size = new System.Drawing.Size(122, 31);
            this.Választott_Nap.TabIndex = 94;
            // 
            // Választott_napszak
            // 
            this.Választott_napszak.BackColor = System.Drawing.Color.Gold;
            this.Választott_napszak.Enabled = false;
            this.Választott_napszak.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Választott_napszak.Location = new System.Drawing.Point(308, 18);
            this.Választott_napszak.Name = "Választott_napszak";
            this.Választott_napszak.Size = new System.Drawing.Size(91, 31);
            this.Választott_napszak.TabIndex = 95;
            // 
            // Választott_Telephely
            // 
            this.Választott_Telephely.BackColor = System.Drawing.Color.Gold;
            this.Választott_Telephely.Enabled = false;
            this.Választott_Telephely.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Választott_Telephely.Location = new System.Drawing.Point(405, 17);
            this.Választott_Telephely.Name = "Választott_Telephely";
            this.Választott_Telephely.Size = new System.Drawing.Size(151, 31);
            this.Választott_Telephely.TabIndex = 96;
            // 
            // Panel5
            // 
            this.Panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Panel5.BackColor = System.Drawing.Color.Goldenrod;
            this.Panel5.Controls.Add(this.Excel_Melyik);
            this.Panel5.Controls.Add(this.Keresés);
            this.Panel5.Controls.Add(this.Command4);
            this.Panel5.Controls.Add(this.Command3);
            this.Panel5.Controls.Add(this.Panel7);
            this.Panel5.Controls.Add(this.Panel6);
            this.Panel5.Controls.Add(this.Dátum_Melyik);
            this.Panel5.Controls.Add(this.CsoportkijelölMind);
            this.Panel5.Controls.Add(this.CsoportVissza);
            this.Panel5.Controls.Add(this.Típuslista);
            this.Panel5.Controls.Add(this.Label6);
            this.Panel5.Controls.Add(this.Telephelykönyvtár);
            this.Panel5.Location = new System.Drawing.Point(10, 63);
            this.Panel5.Name = "Panel5";
            this.Panel5.Size = new System.Drawing.Size(242, 437);
            this.Panel5.TabIndex = 98;
            // 
            // Panel7
            // 
            this.Panel7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Panel7.Controls.Add(this.RadioButton1);
            this.Panel7.Controls.Add(this.RadioButton2);
            this.Panel7.Location = new System.Drawing.Point(5, 400);
            this.Panel7.Name = "Panel7";
            this.Panel7.Size = new System.Drawing.Size(227, 34);
            this.Panel7.TabIndex = 176;
            // 
            // RadioButton1
            // 
            this.RadioButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RadioButton1.AutoSize = true;
            this.RadioButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.RadioButton1.Location = new System.Drawing.Point(109, 5);
            this.RadioButton1.Name = "RadioButton1";
            this.RadioButton1.Size = new System.Drawing.Size(98, 24);
            this.RadioButton1.TabIndex = 1;
            this.RadioButton1.Text = "Részletes";
            this.RadioButton1.UseVisualStyleBackColor = false;
            // 
            // RadioButton2
            // 
            this.RadioButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RadioButton2.AutoSize = true;
            this.RadioButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.RadioButton2.Checked = true;
            this.RadioButton2.Location = new System.Drawing.Point(16, 5);
            this.RadioButton2.Name = "RadioButton2";
            this.RadioButton2.Size = new System.Drawing.Size(61, 24);
            this.RadioButton2.TabIndex = 0;
            this.RadioButton2.TabStop = true;
            this.RadioButton2.Text = "Lista";
            this.RadioButton2.UseVisualStyleBackColor = false;
            this.RadioButton2.CheckedChanged += new System.EventHandler(this.RadioButton2_CheckedChanged);
            // 
            // Panel6
            // 
            this.Panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Panel6.Controls.Add(this.RadioButton3);
            this.Panel6.Controls.Add(this.RadioButton4);
            this.Panel6.Location = new System.Drawing.Point(5, 290);
            this.Panel6.Name = "Panel6";
            this.Panel6.Size = new System.Drawing.Size(122, 65);
            this.Panel6.TabIndex = 175;
            // 
            // RadioButton3
            // 
            this.RadioButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RadioButton3.AutoSize = true;
            this.RadioButton3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.RadioButton3.Location = new System.Drawing.Point(5, 35);
            this.RadioButton3.Name = "RadioButton3";
            this.RadioButton3.Size = new System.Drawing.Size(117, 24);
            this.RadioButton3.TabIndex = 1;
            this.RadioButton3.TabStop = true;
            this.RadioButton3.Text = "Teljes dátum";
            this.RadioButton3.UseVisualStyleBackColor = false;
            // 
            // RadioButton4
            // 
            this.RadioButton4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RadioButton4.AutoSize = true;
            this.RadioButton4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.RadioButton4.Checked = true;
            this.RadioButton4.Location = new System.Drawing.Point(5, 5);
            this.RadioButton4.Name = "RadioButton4";
            this.RadioButton4.Size = new System.Drawing.Size(88, 24);
            this.RadioButton4.TabIndex = 0;
            this.RadioButton4.TabStop = true;
            this.RadioButton4.Text = "Óra:perc";
            this.RadioButton4.UseVisualStyleBackColor = false;
            // 
            // Típuslista
            // 
            this.Típuslista.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Típuslista.CheckOnClick = true;
            this.Típuslista.FormattingEnabled = true;
            this.Típuslista.Location = new System.Drawing.Point(3, 28);
            this.Típuslista.Name = "Típuslista";
            this.Típuslista.Size = new System.Drawing.Size(182, 256);
            this.Típuslista.TabIndex = 1;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(5, 5);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(109, 20);
            this.Label6.TabIndex = 0;
            this.Label6.Text = "Típus választó";
            // 
            // Telephelykönyvtár
            // 
            this.Telephelykönyvtár.FormattingEnabled = true;
            this.Telephelykönyvtár.Location = new System.Drawing.Point(21, 331);
            this.Telephelykönyvtár.Name = "Telephelykönyvtár";
            this.Telephelykönyvtár.Size = new System.Drawing.Size(211, 28);
            this.Telephelykönyvtár.TabIndex = 181;
            this.Telephelykönyvtár.Visible = false;
            // 
            // Kereső
            // 
            this.Kereső.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Kereső.BackColor = System.Drawing.Color.Blue;
            this.Kereső.Controls.Add(this.Chk_CTRL);
            this.Kereső.Controls.Add(this.Keresőnév);
            this.Kereső.Controls.Add(this.Becsukja);
            this.Kereső.Controls.Add(this.BtnKeres_command2);
            this.Kereső.Controls.Add(this.TextKeres_Text);
            this.Kereső.Location = new System.Drawing.Point(381, 217);
            this.Kereső.Name = "Kereső";
            this.Kereső.Size = new System.Drawing.Size(241, 78);
            this.Kereső.TabIndex = 99;
            this.Kereső.TabStop = false;
            this.Kereső.Visible = false;
            // 
            // Chk_CTRL
            // 
            this.Chk_CTRL.AutoSize = true;
            this.Chk_CTRL.Location = new System.Drawing.Point(6, 10);
            this.Chk_CTRL.Name = "Chk_CTRL";
            this.Chk_CTRL.Size = new System.Drawing.Size(127, 24);
            this.Chk_CTRL.TabIndex = 93;
            this.Chk_CTRL.Text = "CTRL nyomva";
            this.Chk_CTRL.UseVisualStyleBackColor = true;
            this.Chk_CTRL.Visible = false;
            // 
            // Keresőnév
            // 
            this.Keresőnév.AutoSize = true;
            this.Keresőnév.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Keresőnév.Location = new System.Drawing.Point(0, 0);
            this.Keresőnév.Name = "Keresőnév";
            this.Keresőnév.Size = new System.Drawing.Size(59, 20);
            this.Keresőnév.TabIndex = 86;
            this.Keresőnév.Text = "Kereső";
            this.Keresőnév.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Keresőnév_MouseMove);
            // 
            // TextKeres_Text
            // 
            this.TextKeres_Text.Location = new System.Drawing.Point(6, 43);
            this.TextKeres_Text.Name = "TextKeres_Text";
            this.TextKeres_Text.Size = new System.Drawing.Size(142, 26);
            this.TextKeres_Text.TabIndex = 55;
            // 
            // Ablak_Digitális_Főkönyv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.ClientSize = new System.Drawing.Size(1002, 513);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Tábla1);
            this.Controls.Add(this.Kereső);
            this.Controls.Add(this.Panel5);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.Választott_Telephely);
            this.Controls.Add(this.Választott_napszak);
            this.Controls.Add(this.Választott_Nap);
            this.Controls.Add(this.GombTároló);
            this.Controls.Add(this.Járgomb);
            this.Controls.Add(this.Panel3);
            this.Controls.Add(this.Tábla3);
            this.Controls.Add(this.Tábla2);
            this.Controls.Add(this.Tábla);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.BtnSúgó);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Digitális_Főkönyv";
            this.Text = "Digitális Főkönyv";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_Digitális_Főkönyv_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla3)).EndInit();
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            this.Panel5.ResumeLayout(false);
            this.Panel5.PerformLayout();
            this.Panel7.ResumeLayout(false);
            this.Panel7.PerformLayout();
            this.Panel6.ResumeLayout(false);
            this.Panel6.PerformLayout();
            this.Kereső.ResumeLayout(false);
            this.Kereső.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal Button BtnSúgó;
        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal Panel Panel1;
        internal Label Label5;
        internal RadioButton Délután;
        internal RadioButton Délelőtt;
        internal DateTimePicker Dátum;
        internal DataGridView Tábla;
        internal DataGridView Tábla2;
        internal DataGridView Tábla1;
        internal DataGridView Tábla3;
        internal Panel Panel3;
        internal RadioButton Option1;
        internal RadioButton Option4;
        internal RadioButton Option2;
        internal RadioButton Option3;
        internal ToolTip ToolTip1;
        internal Panel GombTároló;
        internal TextBox Választott_Nap;
        internal TextBox Választott_napszak;
        internal TextBox Választott_Telephely;
        internal Button Járgomb;
        internal Button Button1;
        internal Panel Panel5;
        internal Button Excel_Melyik;
        internal Button Keresés;
        internal Button Command4;
        internal Button Command3;
        internal Panel Panel7;
        internal RadioButton RadioButton1;
        internal RadioButton RadioButton2;
        internal Panel Panel6;
        internal RadioButton RadioButton3;
        internal RadioButton RadioButton4;
        internal DateTimePicker Dátum_Melyik;
        internal Button CsoportkijelölMind;
        internal Button CsoportVissza;
        internal CheckedListBox Típuslista;
        internal Label Label6;
        internal ComboBox Telephelykönyvtár;
        internal GroupBox Kereső;
        internal CheckBox Chk_CTRL;
        internal Label Keresőnév;
        internal Button Becsukja;
        internal Button BtnKeres_command2;
        internal TextBox TextKeres_Text;
    }
}