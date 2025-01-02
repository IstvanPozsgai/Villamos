using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos.Ablakok
{
    
    public partial class Ablak_szerelvény : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_szerelvény));
            this.Fülek = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.TényKeres = new System.Windows.Forms.Button();
            this.Újszerelvény = new System.Windows.Forms.Button();
            this.Szerelvénytörlés = new System.Windows.Forms.Button();
            this.Egyszerelvényminusz = new System.Windows.Forms.Button();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Szerelvénylista_gomb = new System.Windows.Forms.Button();
            this.Hozzáad = new System.Windows.Forms.Button();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Pályaszám = new System.Windows.Forms.ComboBox();
            this.Combo1 = new System.Windows.Forms.ComboBox();
            this.Excel_gomb = new System.Windows.Forms.Button();
            this.Szerelvénylista = new System.Windows.Forms.DataGridView();
            this.HibásTábla = new System.Windows.Forms.DataGridView();
            this.Szerelvénytáblasor = new System.Windows.Forms.DataGridView();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.E2_panel = new System.Windows.Forms.Panel();
            this.E2_Törlés = new System.Windows.Forms.Button();
            this.E2_rögzítés = new System.Windows.Forms.Button();
            this.E2_3 = new System.Windows.Forms.RadioButton();
            this.E2_2 = new System.Windows.Forms.RadioButton();
            this.E2_1 = new System.Windows.Forms.RadioButton();
            this.E2_0 = new System.Windows.Forms.RadioButton();
            this.Előírt_Keresés = new System.Windows.Forms.Button();
            this.Előírt_Egyszerelvényminusz = new System.Windows.Forms.Button();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Előírt_pályaszám = new System.Windows.Forms.ComboBox();
            this.Előírt_Combo1 = new System.Windows.Forms.ComboBox();
            this.Előírt_Szerelvénylista = new System.Windows.Forms.DataGridView();
            this.Előírt_Szerelvénytáblasor = new System.Windows.Forms.DataGridView();
            this.Előírt_Frissít = new System.Windows.Forms.Button();
            this.Előírt_Újszerelvény = new System.Windows.Forms.Button();
            this.Előírt_hozzáad = new System.Windows.Forms.Button();
            this.Előírt_szerelvénytörlés = new System.Windows.Forms.Button();
            this.Előírt_Excel = new System.Windows.Forms.Button();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.Napló_Excel = new System.Windows.Forms.Button();
            this.DátumNapló = new System.Windows.Forms.DateTimePicker();
            this.Tábla_napló = new Zuby.ADGV.AdvancedDataGridView();
            this.Napló_Frissít = new System.Windows.Forms.Button();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.Txtírásimező = new System.Windows.Forms.RichTextBox();
            this.Btnrögzítés = new System.Windows.Forms.Button();
            this.Utasítás_tervezet = new System.Windows.Forms.Button();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.Holtart = new V_MindenEgyéb.MyProgressbar();
            this.Fülek.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Szerelvénylista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HibásTábla)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Szerelvénytáblasor)).BeginInit();
            this.TabPage2.SuspendLayout();
            this.E2_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Előírt_Szerelvénylista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Előírt_Szerelvénytáblasor)).BeginInit();
            this.TabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_napló)).BeginInit();
            this.TabPage4.SuspendLayout();
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
            this.Fülek.Controls.Add(this.TabPage3);
            this.Fülek.Controls.Add(this.TabPage4);
            this.Fülek.Location = new System.Drawing.Point(5, 55);
            this.Fülek.Name = "Fülek";
            this.Fülek.Padding = new System.Drawing.Point(16, 3);
            this.Fülek.SelectedIndex = 0;
            this.Fülek.Size = new System.Drawing.Size(1190, 405);
            this.Fülek.TabIndex = 0;
            this.Fülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Fülek_DrawItem);
            this.Fülek.SelectedIndexChanged += new System.EventHandler(this.Fülek_SelectedIndexChanged);
            this.Fülek.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Fülek_KeyDown);
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.TabPage1.Controls.Add(this.TényKeres);
            this.TabPage1.Controls.Add(this.Újszerelvény);
            this.TabPage1.Controls.Add(this.Szerelvénytörlés);
            this.TabPage1.Controls.Add(this.Egyszerelvényminusz);
            this.TabPage1.Controls.Add(this.Panel2);
            this.TabPage1.Controls.Add(this.Szerelvénylista_gomb);
            this.TabPage1.Controls.Add(this.Hozzáad);
            this.TabPage1.Controls.Add(this.Label3);
            this.TabPage1.Controls.Add(this.Label2);
            this.TabPage1.Controls.Add(this.Label1);
            this.TabPage1.Controls.Add(this.Pályaszám);
            this.TabPage1.Controls.Add(this.Combo1);
            this.TabPage1.Controls.Add(this.Excel_gomb);
            this.TabPage1.Controls.Add(this.Szerelvénylista);
            this.TabPage1.Controls.Add(this.HibásTábla);
            this.TabPage1.Controls.Add(this.Szerelvénytáblasor);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1182, 372);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Tényleges összeállítás";
            // 
            // TényKeres
            // 
            this.TényKeres.BackgroundImage = global::Villamos.Properties.Resources.Nagyító;
            this.TényKeres.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TényKeres.Location = new System.Drawing.Point(339, 155);
            this.TényKeres.Name = "TényKeres";
            this.TényKeres.Size = new System.Drawing.Size(45, 45);
            this.TényKeres.TabIndex = 178;
            this.ToolTip1.SetToolTip(this.TényKeres, "Keresés a szerelvényekben");
            this.TényKeres.UseVisualStyleBackColor = true;
            this.TényKeres.Click += new System.EventHandler(this.TényKeres_Click);
            // 
            // Újszerelvény
            // 
            this.Újszerelvény.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Újszerelvény.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Újszerelvény.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Újszerelvény.Location = new System.Drawing.Point(97, 160);
            this.Újszerelvény.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Újszerelvény.Name = "Újszerelvény";
            this.Újszerelvény.Size = new System.Drawing.Size(40, 40);
            this.Újszerelvény.TabIndex = 175;
            this.ToolTip1.SetToolTip(this.Újszerelvény, "Új szerelvényyt készít");
            this.Újszerelvény.UseVisualStyleBackColor = true;
            this.Újszerelvény.Click += new System.EventHandler(this.Újszerelvény_Click);
            // 
            // Szerelvénytörlés
            // 
            this.Szerelvénytörlés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Szerelvénytörlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Szerelvénytörlés.Location = new System.Drawing.Point(51, 160);
            this.Szerelvénytörlés.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Szerelvénytörlés.Name = "Szerelvénytörlés";
            this.Szerelvénytörlés.Size = new System.Drawing.Size(40, 40);
            this.Szerelvénytörlés.TabIndex = 165;
            this.ToolTip1.SetToolTip(this.Szerelvénytörlés, "Egész szerelvényt törli");
            this.Szerelvénytörlés.UseVisualStyleBackColor = true;
            this.Szerelvénytörlés.Click += new System.EventHandler(this.Szerelvénytörlés_Click);
            // 
            // Egyszerelvényminusz
            // 
            this.Egyszerelvényminusz.BackgroundImage = global::Villamos.Properties.Resources.New_32_piros;
            this.Egyszerelvényminusz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Egyszerelvényminusz.Location = new System.Drawing.Point(5, 160);
            this.Egyszerelvényminusz.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Egyszerelvényminusz.Name = "Egyszerelvényminusz";
            this.Egyszerelvényminusz.Size = new System.Drawing.Size(40, 40);
            this.Egyszerelvényminusz.TabIndex = 174;
            this.ToolTip1.SetToolTip(this.Egyszerelvényminusz, "Kijelölt kocsit törli a szerelvényből");
            this.Egyszerelvényminusz.UseVisualStyleBackColor = true;
            this.Egyszerelvényminusz.Click += new System.EventHandler(this.Egyszerelvényminusz_Click);
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.Color.MediumTurquoise;
            this.Panel2.Controls.Add(this.Label5);
            this.Panel2.Controls.Add(this.Label4);
            this.Panel2.Location = new System.Drawing.Point(484, 65);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(418, 213);
            this.Panel2.TabIndex = 182;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Label5.Location = new System.Drawing.Point(38, 106);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(359, 31);
            this.Label5.TabIndex = 1;
            this.Label5.Text = "Az adatok ellenőrzése folyik.";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Label4.Location = new System.Drawing.Point(151, 35);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(121, 31);
            this.Label4.TabIndex = 0;
            this.Label4.Text = "Türelem!";
            // 
            // Szerelvénylista_gomb
            // 
            this.Szerelvénylista_gomb.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Szerelvénylista_gomb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Szerelvénylista_gomb.Location = new System.Drawing.Point(390, 155);
            this.Szerelvénylista_gomb.Name = "Szerelvénylista_gomb";
            this.Szerelvénylista_gomb.Size = new System.Drawing.Size(45, 45);
            this.Szerelvénylista_gomb.TabIndex = 176;
            this.ToolTip1.SetToolTip(this.Szerelvénylista_gomb, "Frissíti a képernyőt");
            this.Szerelvénylista_gomb.UseVisualStyleBackColor = true;
            this.Szerelvénylista_gomb.Click += new System.EventHandler(this.Szerelvénylista_gomb_Click);
            // 
            // Hozzáad
            // 
            this.Hozzáad.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Hozzáad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Hozzáad.Location = new System.Drawing.Point(395, 18);
            this.Hozzáad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Hozzáad.Name = "Hozzáad";
            this.Hozzáad.Size = new System.Drawing.Size(40, 40);
            this.Hozzáad.TabIndex = 171;
            this.ToolTip1.SetToolTip(this.Hozzáad, "A kiválasztott kocsit hozzáadja a szerelvényhez");
            this.Hozzáad.UseVisualStyleBackColor = true;
            this.Hozzáad.Click += new System.EventHandler(this.Hozzáad_Click);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(6, 201);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(134, 20);
            this.Label3.TabIndex = 170;
            this.Label3.Text = "Hibás csatolások:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(160, 5);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(106, 20);
            this.Label2.TabIndex = 169;
            this.Label2.Text = "Pályaszámok:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(5, 5);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(112, 20);
            this.Label1.TabIndex = 168;
            this.Label1.Text = "Jármű típusok:";
            // 
            // Pályaszám
            // 
            this.Pályaszám.FormattingEnabled = true;
            this.Pályaszám.Location = new System.Drawing.Point(160, 30);
            this.Pályaszám.Name = "Pályaszám";
            this.Pályaszám.Size = new System.Drawing.Size(146, 28);
            this.Pályaszám.TabIndex = 167;
            // 
            // Combo1
            // 
            this.Combo1.FormattingEnabled = true;
            this.Combo1.Location = new System.Drawing.Point(5, 30);
            this.Combo1.Name = "Combo1";
            this.Combo1.Size = new System.Drawing.Size(146, 28);
            this.Combo1.TabIndex = 166;
            this.Combo1.SelectedIndexChanged += new System.EventHandler(this.Combo1_SelectedIndexChanged);
            // 
            // Excel_gomb
            // 
            this.Excel_gomb.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excel_gomb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excel_gomb.Location = new System.Drawing.Point(143, 160);
            this.Excel_gomb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Excel_gomb.Name = "Excel_gomb";
            this.Excel_gomb.Size = new System.Drawing.Size(40, 40);
            this.Excel_gomb.TabIndex = 164;
            this.ToolTip1.SetToolTip(this.Excel_gomb, "A szerelvényeket Excelbe menti");
            this.Excel_gomb.UseVisualStyleBackColor = true;
            this.Excel_gomb.Click += new System.EventHandler(this.Excel_gomb_Click);
            // 
            // Szerelvénylista
            // 
            this.Szerelvénylista.AllowUserToAddRows = false;
            this.Szerelvénylista.AllowUserToDeleteRows = false;
            this.Szerelvénylista.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Szerelvénylista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Szerelvénylista.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.Szerelvénylista.Location = new System.Drawing.Point(446, 5);
            this.Szerelvénylista.Name = "Szerelvénylista";
            this.Szerelvénylista.RowHeadersVisible = false;
            this.Szerelvénylista.RowHeadersWidth = 62;
            this.Szerelvénylista.Size = new System.Drawing.Size(730, 360);
            this.Szerelvénylista.TabIndex = 3;
            this.Szerelvénylista.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Szerelvénylista_CellClick);
            // 
            // HibásTábla
            // 
            this.HibásTábla.AllowUserToAddRows = false;
            this.HibásTábla.AllowUserToDeleteRows = false;
            this.HibásTábla.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.HibásTábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.HibásTábla.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.HibásTábla.Location = new System.Drawing.Point(6, 224);
            this.HibásTábla.Name = "HibásTábla";
            this.HibásTábla.RowHeadersVisible = false;
            this.HibásTábla.RowHeadersWidth = 62;
            this.HibásTábla.Size = new System.Drawing.Size(430, 142);
            this.HibásTábla.TabIndex = 1;
            // 
            // Szerelvénytáblasor
            // 
            this.Szerelvénytáblasor.AllowUserToAddRows = false;
            this.Szerelvénytáblasor.AllowUserToDeleteRows = false;
            this.Szerelvénytáblasor.AllowUserToResizeColumns = false;
            this.Szerelvénytáblasor.AllowUserToResizeRows = false;
            this.Szerelvénytáblasor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Szerelvénytáblasor.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.Szerelvénytáblasor.Location = new System.Drawing.Point(5, 65);
            this.Szerelvénytáblasor.Name = "Szerelvénytáblasor";
            this.Szerelvénytáblasor.RowHeadersVisible = false;
            this.Szerelvénytáblasor.RowHeadersWidth = 62;
            this.Szerelvénytáblasor.Size = new System.Drawing.Size(430, 87);
            this.Szerelvénytáblasor.TabIndex = 0;
            this.Szerelvénytáblasor.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Szerelvénytáblasor_CellClick);
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.DarkOrange;
            this.TabPage2.Controls.Add(this.E2_panel);
            this.TabPage2.Controls.Add(this.Előírt_Keresés);
            this.TabPage2.Controls.Add(this.Előírt_Egyszerelvényminusz);
            this.TabPage2.Controls.Add(this.Label6);
            this.TabPage2.Controls.Add(this.Label7);
            this.TabPage2.Controls.Add(this.Előírt_pályaszám);
            this.TabPage2.Controls.Add(this.Előírt_Combo1);
            this.TabPage2.Controls.Add(this.Előírt_Szerelvénylista);
            this.TabPage2.Controls.Add(this.Előírt_Szerelvénytáblasor);
            this.TabPage2.Controls.Add(this.Előírt_Frissít);
            this.TabPage2.Controls.Add(this.Előírt_Újszerelvény);
            this.TabPage2.Controls.Add(this.Előírt_hozzáad);
            this.TabPage2.Controls.Add(this.Előírt_szerelvénytörlés);
            this.TabPage2.Controls.Add(this.Előírt_Excel);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(1182, 372);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Előírt összeállítás";
            // 
            // E2_panel
            // 
            this.E2_panel.BackColor = System.Drawing.Color.Salmon;
            this.E2_panel.Controls.Add(this.E2_Törlés);
            this.E2_panel.Controls.Add(this.E2_rögzítés);
            this.E2_panel.Controls.Add(this.E2_3);
            this.E2_panel.Controls.Add(this.E2_2);
            this.E2_panel.Controls.Add(this.E2_1);
            this.E2_panel.Controls.Add(this.E2_0);
            this.E2_panel.Location = new System.Drawing.Point(158, 207);
            this.E2_panel.Name = "E2_panel";
            this.E2_panel.Size = new System.Drawing.Size(226, 142);
            this.E2_panel.TabIndex = 198;
            // 
            // E2_Törlés
            // 
            this.E2_Törlés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.E2_Törlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.E2_Törlés.Location = new System.Drawing.Point(181, 93);
            this.E2_Törlés.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.E2_Törlés.Name = "E2_Törlés";
            this.E2_Törlés.Size = new System.Drawing.Size(40, 40);
            this.E2_Törlés.TabIndex = 192;
            this.ToolTip1.SetToolTip(this.E2_Törlés, "Alaphelyzetbe állítja a napokat\r\nminden szerelvény esetén");
            this.E2_Törlés.UseVisualStyleBackColor = true;
            this.E2_Törlés.Click += new System.EventHandler(this.E2_Törlés_Click);
            // 
            // E2_rögzítés
            // 
            this.E2_rögzítés.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.E2_rögzítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.E2_rögzítés.Location = new System.Drawing.Point(181, 5);
            this.E2_rögzítés.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.E2_rögzítés.Name = "E2_rögzítés";
            this.E2_rögzítés.Size = new System.Drawing.Size(40, 40);
            this.E2_rögzítés.TabIndex = 191;
            this.ToolTip1.SetToolTip(this.E2_rögzítés, "Rögzíti az E2 vizsgálat napján szerelvényhez.");
            this.E2_rögzítés.UseVisualStyleBackColor = true;
            this.E2_rögzítés.Click += new System.EventHandler(this.E2_rögzítés_Click);
            // 
            // E2_3
            // 
            this.E2_3.AutoSize = true;
            this.E2_3.Location = new System.Drawing.Point(14, 109);
            this.E2_3.Name = "E2_3";
            this.E2_3.Size = new System.Drawing.Size(151, 24);
            this.E2_3.TabIndex = 3;
            this.E2_3.TabStop = true;
            this.E2_3.Text = "Szerda- Szombat";
            this.E2_3.UseVisualStyleBackColor = true;
            // 
            // E2_2
            // 
            this.E2_2.AutoSize = true;
            this.E2_2.Location = new System.Drawing.Point(14, 77);
            this.E2_2.Name = "E2_2";
            this.E2_2.Size = new System.Drawing.Size(123, 24);
            this.E2_2.TabIndex = 2;
            this.E2_2.TabStop = true;
            this.E2_2.Text = "Kedd- Péntek";
            this.E2_2.UseVisualStyleBackColor = true;
            // 
            // E2_1
            // 
            this.E2_1.AutoSize = true;
            this.E2_1.Location = new System.Drawing.Point(14, 45);
            this.E2_1.Name = "E2_1";
            this.E2_1.Size = new System.Drawing.Size(145, 24);
            this.E2_1.TabIndex = 1;
            this.E2_1.TabStop = true;
            this.E2_1.Text = "Hétfő- Csütörtök";
            this.E2_1.UseVisualStyleBackColor = true;
            // 
            // E2_0
            // 
            this.E2_0.AutoSize = true;
            this.E2_0.Location = new System.Drawing.Point(14, 13);
            this.E2_0.Name = "E2_0";
            this.E2_0.Size = new System.Drawing.Size(127, 24);
            this.E2_0.TabIndex = 0;
            this.E2_0.TabStop = true;
            this.E2_0.Text = "Nincs beállítva";
            this.E2_0.UseVisualStyleBackColor = true;
            // 
            // Előírt_Keresés
            // 
            this.Előírt_Keresés.BackgroundImage = global::Villamos.Properties.Resources.Nagyító;
            this.Előírt_Keresés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Előírt_Keresés.Location = new System.Drawing.Point(288, 156);
            this.Előírt_Keresés.Name = "Előírt_Keresés";
            this.Előírt_Keresés.Size = new System.Drawing.Size(45, 45);
            this.Előírt_Keresés.TabIndex = 194;
            this.ToolTip1.SetToolTip(this.Előírt_Keresés, "Keresési segédablak megjelenítése\r\nhogy a táblázat adatai között lehessen keresni" +
        ".\r\n");
            this.Előírt_Keresés.UseVisualStyleBackColor = true;
            this.Előírt_Keresés.Click += new System.EventHandler(this.Előírt_Keresés_Click);
            // 
            // Előírt_Egyszerelvényminusz
            // 
            this.Előírt_Egyszerelvényminusz.BackgroundImage = global::Villamos.Properties.Resources.New_32_piros;
            this.Előírt_Egyszerelvényminusz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Előírt_Egyszerelvényminusz.Location = new System.Drawing.Point(3, 155);
            this.Előírt_Egyszerelvényminusz.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Előírt_Egyszerelvényminusz.Name = "Előírt_Egyszerelvényminusz";
            this.Előírt_Egyszerelvényminusz.Size = new System.Drawing.Size(40, 40);
            this.Előírt_Egyszerelvényminusz.TabIndex = 191;
            this.ToolTip1.SetToolTip(this.Előírt_Egyszerelvényminusz, "Törli a szerelvény kijelölt elemét.");
            this.Előírt_Egyszerelvényminusz.UseVisualStyleBackColor = true;
            this.Előírt_Egyszerelvényminusz.Click += new System.EventHandler(this.Előírt_Egyszerelvénymnusz_Click);
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(158, 3);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(106, 20);
            this.Label6.TabIndex = 189;
            this.Label6.Text = "Pályaszámok:";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(3, 3);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(112, 20);
            this.Label7.TabIndex = 188;
            this.Label7.Text = "Jármű típusok:";
            // 
            // Előírt_pályaszám
            // 
            this.Előírt_pályaszám.FormattingEnabled = true;
            this.Előírt_pályaszám.Location = new System.Drawing.Point(158, 28);
            this.Előírt_pályaszám.Name = "Előírt_pályaszám";
            this.Előírt_pályaszám.Size = new System.Drawing.Size(146, 28);
            this.Előírt_pályaszám.TabIndex = 187;
            this.Előírt_pályaszám.SelectedIndexChanged += new System.EventHandler(this.Előírt_pályaszám_SelectedIndexChanged);
            this.Előírt_pályaszám.TextUpdate += new System.EventHandler(this.Előírt_pályaszám_TextUpdate);
            this.Előírt_pályaszám.MouseEnter += new System.EventHandler(this.Előírt_pályaszám_MouseEnter);
            // 
            // Előírt_Combo1
            // 
            this.Előírt_Combo1.FormattingEnabled = true;
            this.Előírt_Combo1.Location = new System.Drawing.Point(3, 28);
            this.Előírt_Combo1.Name = "Előírt_Combo1";
            this.Előírt_Combo1.Size = new System.Drawing.Size(146, 28);
            this.Előírt_Combo1.TabIndex = 186;
            this.Előírt_Combo1.SelectedIndexChanged += new System.EventHandler(this.Előírt_Combo1_SelectedIndexChanged);
            // 
            // Előírt_Szerelvénylista
            // 
            this.Előírt_Szerelvénylista.AllowUserToAddRows = false;
            this.Előírt_Szerelvénylista.AllowUserToDeleteRows = false;
            this.Előírt_Szerelvénylista.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Előírt_Szerelvénylista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Előírt_Szerelvénylista.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.Előírt_Szerelvénylista.Location = new System.Drawing.Point(403, 16);
            this.Előírt_Szerelvénylista.Name = "Előírt_Szerelvénylista";
            this.Előírt_Szerelvénylista.RowHeadersVisible = false;
            this.Előírt_Szerelvénylista.RowHeadersWidth = 62;
            this.Előírt_Szerelvénylista.Size = new System.Drawing.Size(773, 349);
            this.Előírt_Szerelvénylista.TabIndex = 183;
            this.Előírt_Szerelvénylista.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Előírt_Szerelvénylista_CellClick);
            // 
            // Előírt_Szerelvénytáblasor
            // 
            this.Előírt_Szerelvénytáblasor.AllowUserToAddRows = false;
            this.Előírt_Szerelvénytáblasor.AllowUserToDeleteRows = false;
            this.Előírt_Szerelvénytáblasor.AllowUserToResizeColumns = false;
            this.Előírt_Szerelvénytáblasor.AllowUserToResizeRows = false;
            this.Előírt_Szerelvénytáblasor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Előírt_Szerelvénytáblasor.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.Előírt_Szerelvénytáblasor.Location = new System.Drawing.Point(3, 63);
            this.Előírt_Szerelvénytáblasor.Name = "Előírt_Szerelvénytáblasor";
            this.Előírt_Szerelvénytáblasor.RowHeadersVisible = false;
            this.Előírt_Szerelvénytáblasor.RowHeadersWidth = 62;
            this.Előírt_Szerelvénytáblasor.Size = new System.Drawing.Size(381, 87);
            this.Előírt_Szerelvénytáblasor.TabIndex = 182;
            this.Előírt_Szerelvénytáblasor.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Előírt_Szerelvénytáblasor_CellClick);
            // 
            // Előírt_Frissít
            // 
            this.Előírt_Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Előírt_Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Előírt_Frissít.Location = new System.Drawing.Point(339, 156);
            this.Előírt_Frissít.Name = "Előírt_Frissít";
            this.Előírt_Frissít.Size = new System.Drawing.Size(45, 45);
            this.Előírt_Frissít.TabIndex = 193;
            this.ToolTip1.SetToolTip(this.Előírt_Frissít, "Frissíti a táblázat adatait");
            this.Előírt_Frissít.UseVisualStyleBackColor = true;
            this.Előírt_Frissít.Click += new System.EventHandler(this.Button4_Click);
            // 
            // Előírt_Újszerelvény
            // 
            this.Előírt_Újszerelvény.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Előírt_Újszerelvény.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Előírt_Újszerelvény.Location = new System.Drawing.Point(95, 155);
            this.Előírt_Újszerelvény.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Előírt_Újszerelvény.Name = "Előírt_Újszerelvény";
            this.Előírt_Újszerelvény.Size = new System.Drawing.Size(40, 40);
            this.Előírt_Újszerelvény.TabIndex = 192;
            this.ToolTip1.SetToolTip(this.Előírt_Újszerelvény, "Új szerelvény létrehozás");
            this.Előírt_Újszerelvény.UseVisualStyleBackColor = true;
            this.Előírt_Újszerelvény.Click += new System.EventHandler(this.Előírt_Újszerelvény_Click);
            // 
            // Előírt_hozzáad
            // 
            this.Előírt_hozzáad.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Előírt_hozzáad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Előírt_hozzáad.Location = new System.Drawing.Point(344, 16);
            this.Előírt_hozzáad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Előírt_hozzáad.Name = "Előírt_hozzáad";
            this.Előírt_hozzáad.Size = new System.Drawing.Size(40, 40);
            this.Előírt_hozzáad.TabIndex = 190;
            this.ToolTip1.SetToolTip(this.Előírt_hozzáad, "Szerelvényhez adja a pályaszám mezőbe \r\nbeírt pályaszámot.\r\n");
            this.Előírt_hozzáad.UseVisualStyleBackColor = true;
            this.Előírt_hozzáad.Click += new System.EventHandler(this.Előírt_hozzáad_Click);
            // 
            // Előírt_szerelvénytörlés
            // 
            this.Előírt_szerelvénytörlés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Előírt_szerelvénytörlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Előírt_szerelvénytörlés.Location = new System.Drawing.Point(49, 155);
            this.Előírt_szerelvénytörlés.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Előírt_szerelvénytörlés.Name = "Előírt_szerelvénytörlés";
            this.Előírt_szerelvénytörlés.Size = new System.Drawing.Size(40, 40);
            this.Előírt_szerelvénytörlés.TabIndex = 185;
            this.ToolTip1.SetToolTip(this.Előírt_szerelvénytörlés, "Törli a szerelvényt");
            this.Előírt_szerelvénytörlés.UseVisualStyleBackColor = true;
            this.Előírt_szerelvénytörlés.Click += new System.EventHandler(this.Előírt_szerelvénytörlés_Click);
            // 
            // Előírt_Excel
            // 
            this.Előírt_Excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Előírt_Excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Előírt_Excel.Location = new System.Drawing.Point(141, 155);
            this.Előírt_Excel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Előírt_Excel.Name = "Előírt_Excel";
            this.Előírt_Excel.Size = new System.Drawing.Size(40, 40);
            this.Előírt_Excel.TabIndex = 184;
            this.ToolTip1.SetToolTip(this.Előírt_Excel, "Táblázat adatait excelbe menti.");
            this.Előírt_Excel.UseVisualStyleBackColor = true;
            this.Előírt_Excel.Click += new System.EventHandler(this.Előírt_Excel_Click);
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.TabPage3.Controls.Add(this.Napló_Excel);
            this.TabPage3.Controls.Add(this.DátumNapló);
            this.TabPage3.Controls.Add(this.Tábla_napló);
            this.TabPage3.Controls.Add(this.Napló_Frissít);
            this.TabPage3.Location = new System.Drawing.Point(4, 29);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(1182, 372);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Napló";
            // 
            // Napló_Excel
            // 
            this.Napló_Excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Napló_Excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Napló_Excel.Location = new System.Drawing.Point(202, 10);
            this.Napló_Excel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Napló_Excel.Name = "Napló_Excel";
            this.Napló_Excel.Size = new System.Drawing.Size(45, 45);
            this.Napló_Excel.TabIndex = 185;
            this.ToolTip1.SetToolTip(this.Napló_Excel, "Táblázat adatait excelbe menti.");
            this.Napló_Excel.UseVisualStyleBackColor = true;
            this.Napló_Excel.Click += new System.EventHandler(this.Napló_Excel_Click);
            // 
            // DátumNapló
            // 
            this.DátumNapló.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DátumNapló.Location = new System.Drawing.Point(7, 19);
            this.DátumNapló.Name = "DátumNapló";
            this.DátumNapló.Size = new System.Drawing.Size(124, 26);
            this.DátumNapló.TabIndex = 178;
            this.DátumNapló.ValueChanged += new System.EventHandler(this.DátumNapló_ValueChanged);
            // 
            // Tábla_napló
            // 
            this.Tábla_napló.AllowUserToAddRows = false;
            this.Tábla_napló.AllowUserToDeleteRows = false;
            this.Tábla_napló.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla_napló.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla_napló.FilterAndSortEnabled = true;
            this.Tábla_napló.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla_napló.Location = new System.Drawing.Point(3, 63);
            this.Tábla_napló.MaxFilterButtonImageHeight = 23;
            this.Tábla_napló.Name = "Tábla_napló";
            this.Tábla_napló.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tábla_napló.RowHeadersVisible = false;
            this.Tábla_napló.RowHeadersWidth = 62;
            this.Tábla_napló.Size = new System.Drawing.Size(1173, 306);
            this.Tábla_napló.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla_napló.TabIndex = 4;
            // 
            // Napló_Frissít
            // 
            this.Napló_Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Napló_Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Napló_Frissít.Location = new System.Drawing.Point(151, 10);
            this.Napló_Frissít.Name = "Napló_Frissít";
            this.Napló_Frissít.Size = new System.Drawing.Size(45, 45);
            this.Napló_Frissít.TabIndex = 177;
            this.ToolTip1.SetToolTip(this.Napló_Frissít, "Listázza a naplózást.");
            this.Napló_Frissít.UseVisualStyleBackColor = true;
            this.Napló_Frissít.Click += new System.EventHandler(this.Napló_Frissít_Click);
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.Green;
            this.TabPage4.Controls.Add(this.Txtírásimező);
            this.TabPage4.Controls.Add(this.Btnrögzítés);
            this.TabPage4.Controls.Add(this.Utasítás_tervezet);
            this.TabPage4.Location = new System.Drawing.Point(4, 29);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Size = new System.Drawing.Size(1182, 372);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Utasítás írás";
            // 
            // Txtírásimező
            // 
            this.Txtírásimező.AcceptsTab = true;
            this.Txtírásimező.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Txtírásimező.Location = new System.Drawing.Point(5, 60);
            this.Txtírásimező.Name = "Txtírásimező";
            this.Txtírásimező.Size = new System.Drawing.Size(1168, 304);
            this.Txtírásimező.TabIndex = 78;
            this.Txtírásimező.Text = "";
            // 
            // Btnrögzítés
            // 
            this.Btnrögzítés.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btnrögzítés.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Btnrögzítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btnrögzítés.Location = new System.Drawing.Point(56, 10);
            this.Btnrögzítés.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btnrögzítés.Name = "Btnrögzítés";
            this.Btnrögzítés.Size = new System.Drawing.Size(40, 40);
            this.Btnrögzítés.TabIndex = 76;
            this.ToolTip1.SetToolTip(this.Btnrögzítés, "Utasításokba berögzíti az előírást");
            this.Btnrögzítés.UseVisualStyleBackColor = true;
            this.Btnrögzítés.Click += new System.EventHandler(this.Btnrögzítés_Click);
            // 
            // Utasítás_tervezet
            // 
            this.Utasítás_tervezet.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Utasítás_tervezet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Utasítás_tervezet.Location = new System.Drawing.Point(11, 10);
            this.Utasítás_tervezet.Name = "Utasítás_tervezet";
            this.Utasítás_tervezet.Size = new System.Drawing.Size(40, 40);
            this.Utasítás_tervezet.TabIndex = 74;
            this.ToolTip1.SetToolTip(this.Utasítás_tervezet, "Elkészíti az előtervet");
            this.Utasítás_tervezet.UseVisualStyleBackColor = true;
            this.Utasítás_tervezet.Click += new System.EventHandler(this.Utasítás_tervezet_Click);
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.Cmbtelephely);
            this.Panel1.Controls.Add(this.Label13);
            this.Panel1.Location = new System.Drawing.Point(12, 6);
            this.Panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(374, 37);
            this.Panel1.TabIndex = 58;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(176, 4);
            this.Cmbtelephely.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
            this.Cmbtelephely.SelectedIndexChanged += new System.EventHandler(this.Cmbtelephely_SelectedIndexChanged);
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(12, 8);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(145, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // ToolTip1
            // 
            this.ToolTip1.IsBalloon = true;
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1148, 6);
            this.BtnSúgó.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(40, 40);
            this.BtnSúgó.TabIndex = 62;
            this.ToolTip1.SetToolTip(this.BtnSúgó, "Súgó");
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.BackColor = System.Drawing.Color.Teal;
            this.Holtart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Holtart.Location = new System.Drawing.Point(405, 16);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(725, 20);
            this.Holtart.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.Holtart.TabIndex = 96;
            this.Holtart.Visible = false;
            // 
            // Ablak_szerelvény
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1200, 463);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Fülek);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_szerelvény";
            this.Text = "Szerelvények összeállítása";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_szerelvény_FormClosed);
            this.Load += new System.EventHandler(this.Ablak_szerelvény_Load);
            this.Shown += new System.EventHandler(this.Ablak_szerelvény_Shown);
            this.Fülek.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Szerelvénylista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HibásTábla)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Szerelvénytáblasor)).EndInit();
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            this.E2_panel.ResumeLayout(false);
            this.E2_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Előírt_Szerelvénylista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Előírt_Szerelvénytáblasor)).EndInit();
            this.TabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_napló)).EndInit();
            this.TabPage4.ResumeLayout(false);
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
        internal Button BtnSúgó;
        internal DataGridView Szerelvénylista;
        internal DataGridView HibásTábla;
        internal DataGridView Szerelvénytáblasor;
        internal Button Egyszerelvényminusz;
        internal Button Hozzáad;
        internal Label Label3;
        internal Label Label2;
        internal Label Label1;
        internal ComboBox Pályaszám;
        internal ComboBox Combo1;
        internal Button Szerelvénytörlés;
        internal Button Excel_gomb;
        internal Button Újszerelvény;
        internal TabPage TabPage3;
        internal Button Szerelvénylista_gomb;
        internal Button TényKeres;
        internal DateTimePicker DátumNapló;
        internal Button Napló_Frissít;
        internal Zuby.ADGV.AdvancedDataGridView Tábla_napló;
        internal Panel Panel2;
        internal Label Label5;
        internal Label Label4;
        internal Button Előírt_Keresés;
        internal Button Előírt_Frissít;
        internal Button Előírt_Újszerelvény;
        internal Button Előírt_Egyszerelvényminusz;
        internal Button Előírt_hozzáad;
        internal Label Label6;
        internal Label Label7;
        internal ComboBox Előírt_pályaszám;
        internal ComboBox Előírt_Combo1;
        internal Button Előírt_szerelvénytörlés;
        internal Button Előírt_Excel;
        internal DataGridView Előírt_Szerelvénylista;
        internal DataGridView Előírt_Szerelvénytáblasor;
        internal Panel E2_panel;
        internal RadioButton E2_3;
        internal RadioButton E2_2;
        internal RadioButton E2_1;
        internal RadioButton E2_0;
        internal Button E2_rögzítés;
        internal TabPage TabPage4;
        internal Button Btnrögzítés;
        internal Button Utasítás_tervezet;
        internal Button E2_Törlés;
        internal ToolTip ToolTip1;
        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal RichTextBox Txtírásimező;
        internal Button Napló_Excel;
    }
}