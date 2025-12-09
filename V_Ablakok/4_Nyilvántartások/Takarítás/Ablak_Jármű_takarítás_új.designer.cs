using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
  
    public partial class Ablak_Jármű_takarítás_új : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components != null)
                components.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Jármű_takarítás_új));
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Lapfülek = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.GroupBox6 = new System.Windows.Forms.GroupBox();
            this.Excell_Ütem_tábla = new System.Windows.Forms.Button();
            this.Ütem_Tábla = new System.Windows.Forms.DataGridView();
            this.Ütem_Rögzítés = new System.Windows.Forms.Button();
            this.Ütem_frissít = new System.Windows.Forms.Button();
            this.Ütem_növekmény = new System.Windows.Forms.TextBox();
            this.Ütem_mérték = new System.Windows.Forms.ComboBox();
            this.Label18 = new System.Windows.Forms.Label();
            this.Label17 = new System.Windows.Forms.Label();
            this.Ütem_telephely = new System.Windows.Forms.ComboBox();
            this.Ütem_státus = new System.Windows.Forms.CheckBox();
            this.Ütem_takarítási_fajta = new System.Windows.Forms.ComboBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.Label16 = new System.Windows.Forms.Label();
            this.Ütem_kezdődátum = new System.Windows.Forms.DateTimePicker();
            this.Ütem_azonosító = new System.Windows.Forms.ComboBox();
            this.GroupBox5 = new System.Windows.Forms.GroupBox();
            this.PályaszámTakarításai = new System.Windows.Forms.Button();
            this.Excel_Takarítás = new System.Windows.Forms.Button();
            this.Utolsó_telephely = new System.Windows.Forms.ComboBox();
            this.Utolsó_státus = new System.Windows.Forms.CheckBox();
            this.Utolsó_történet = new System.Windows.Forms.Button();
            this.Utolsó_takarítási_fajta = new System.Windows.Forms.ComboBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Utolsó_dátum = new System.Windows.Forms.DateTimePicker();
            this.Utolsó_pályaszám = new System.Windows.Forms.ComboBox();
            this.Utolsó_módosít = new System.Windows.Forms.Button();
            this.Utolsó_frissít = new System.Windows.Forms.Button();
            this.Tábla_utolsó = new Zuby.ADGV.AdvancedDataGridView();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.Btn_vezénylésexcel = new System.Windows.Forms.Button();
            this.Kereső_hívó = new System.Windows.Forms.Button();
            this.Alap_Lista = new System.Windows.Forms.Button();
            this.Ütemezés_lista = new System.Windows.Forms.ListBox();
            this.Btn_Vezénylésbeírás = new System.Windows.Forms.Button();
            this.BtnExcelkimenet = new System.Windows.Forms.Button();
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.Dátum = new System.Windows.Forms.DateTimePicker();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.Opció_tábla = new System.Windows.Forms.DataGridView();
            this.Label21 = new System.Windows.Forms.Label();
            this.Label20 = new System.Windows.Forms.Label();
            this.Label19 = new System.Windows.Forms.Label();
            this.Opció_Töröl = new System.Windows.Forms.Button();
            this.Opció_terület = new System.Windows.Forms.TextBox();
            this.Button2 = new System.Windows.Forms.Button();
            this.Opció_lista = new System.Windows.Forms.ComboBox();
            this.Opció_psz = new System.Windows.Forms.TextBox();
            this.Opció_mentés = new System.Windows.Forms.Button();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.Lét_Előírt = new System.Windows.Forms.TextBox();
            this.Lét_Viselt = new System.Windows.Forms.TextBox();
            this.Lét_Megjelent = new System.Windows.Forms.TextBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.LétszámMentés = new System.Windows.Forms.Button();
            this.GroupBox4 = new System.Windows.Forms.GroupBox();
            this.Label22 = new System.Windows.Forms.Label();
            this.Label23 = new System.Windows.Forms.Label();
            this.JK_törlés = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.JK_pót = new System.Windows.Forms.CheckBox();
            this.JK_Kategória = new System.Windows.Forms.ComboBox();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.JK_megfelel2 = new System.Windows.Forms.RadioButton();
            this.JK_Nem2 = new System.Windows.Forms.RadioButton();
            this.JK_List = new System.Windows.Forms.ListBox();
            this.Panel5 = new System.Windows.Forms.Panel();
            this.JK_Törölt = new System.Windows.Forms.RadioButton();
            this.JK_Megfelel1 = new System.Windows.Forms.RadioButton();
            this.JK_Nem1 = new System.Windows.Forms.RadioButton();
            this.JK_Azonosító = new System.Windows.Forms.TextBox();
            this.JK_Mentés = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.J1NemMegfelelő = new System.Windows.Forms.TextBox();
            this.J1Megfelelő = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.J1Típus = new System.Windows.Forms.ComboBox();
            this.J1Mentés = new System.Windows.Forms.Button();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.RadioButton7 = new System.Windows.Forms.RadioButton();
            this.Jnappal = new System.Windows.Forms.RadioButton();
            this.JDátum = new System.Windows.Forms.DateTimePicker();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.BMR = new System.Windows.Forms.Button();
            this.TIG_Készítés = new System.Windows.Forms.Button();
            this.Button5 = new System.Windows.Forms.Button();
            this.Button4 = new System.Windows.Forms.Button();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.Lekérdezés_Kategória = new System.Windows.Forms.ComboBox();
            this.RadioButton6 = new System.Windows.Forms.RadioButton();
            this.RadioButton3 = new System.Windows.Forms.RadioButton();
            this.RadioButton4 = new System.Windows.Forms.RadioButton();
            this.ListaTábla = new System.Windows.Forms.DataGridView();
            this.Havi_Lekérdezés = new System.Windows.Forms.Button();
            this.Lek_excel = new System.Windows.Forms.Button();
            this.ListaDátum = new System.Windows.Forms.DateTimePicker();
            this.Panel6 = new System.Windows.Forms.Panel();
            this.RadioButton2 = new System.Windows.Forms.RadioButton();
            this.RadioButton1 = new System.Windows.Forms.RadioButton();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.Gépi_Tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.CmbGépiTíp = new System.Windows.Forms.ComboBox();
            this.Tel_TB = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.Pály_TB = new System.Windows.Forms.TextBox();
            this.Gepi_frissit = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.Rögzítések = new System.Windows.Forms.CheckBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label27 = new System.Windows.Forms.Label();
            this.Gepi_torolt = new System.Windows.Forms.CheckBox();
            this.Gepi_rogzit = new System.Windows.Forms.Button();
            this.Gepi_datum = new System.Windows.Forms.DateTimePicker();
            this.Gepi_pályaszám = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.Gepi_excel = new System.Windows.Forms.Button();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Panel2.SuspendLayout();
            this.Lapfülek.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.GroupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Ütem_Tábla)).BeginInit();
            this.GroupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_utolsó)).BeginInit();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.TabPage3.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Opció_tábla)).BeginInit();
            this.GroupBox2.SuspendLayout();
            this.GroupBox4.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.Panel5.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.Panel3.SuspendLayout();
            this.TabPage4.SuspendLayout();
            this.Panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListaTábla)).BeginInit();
            this.Panel6.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gépi_Tábla)).BeginInit();
            this.groupBox8.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Holtart.Location = new System.Drawing.Point(353, 12);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(910, 28);
            this.Holtart.Step = 1;
            this.Holtart.TabIndex = 0;
            this.Holtart.Visible = false;
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.Cmbtelephely);
            this.Panel2.Controls.Add(this.Label13);
            this.Panel2.Location = new System.Drawing.Point(9, 6);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(335, 40);
            this.Panel2.TabIndex = 175;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(145, 5);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 0;
            this.Cmbtelephely.SelectedIndexChanged += new System.EventHandler(this.Cmbtelephely_SelectedIndexChanged);
            this.Cmbtelephely.SelectionChangeCommitted += new System.EventHandler(this.Cmbtelephely_SelectionChangeCommitted);
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(5, 10);
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
            this.Lapfülek.Controls.Add(this.tabPage5);
            this.Lapfülek.Location = new System.Drawing.Point(5, 51);
            this.Lapfülek.Name = "Lapfülek";
            this.Lapfülek.Padding = new System.Drawing.Point(16, 3);
            this.Lapfülek.SelectedIndex = 0;
            this.Lapfülek.Size = new System.Drawing.Size(1317, 508);
            this.Lapfülek.TabIndex = 177;
            this.Lapfülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LapFülek_DrawItem);
            this.Lapfülek.SelectedIndexChanged += new System.EventHandler(this.LapFülek_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.TabPage1.Controls.Add(this.GroupBox6);
            this.TabPage1.Controls.Add(this.GroupBox5);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1309, 475);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Alapadatok";
            // 
            // GroupBox6
            // 
            this.GroupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox6.BackColor = System.Drawing.Color.RoyalBlue;
            this.GroupBox6.Controls.Add(this.Excell_Ütem_tábla);
            this.GroupBox6.Controls.Add(this.Ütem_Tábla);
            this.GroupBox6.Controls.Add(this.Ütem_Rögzítés);
            this.GroupBox6.Controls.Add(this.Ütem_frissít);
            this.GroupBox6.Controls.Add(this.Ütem_növekmény);
            this.GroupBox6.Controls.Add(this.Ütem_mérték);
            this.GroupBox6.Controls.Add(this.Label18);
            this.GroupBox6.Controls.Add(this.Label17);
            this.GroupBox6.Controls.Add(this.Ütem_telephely);
            this.GroupBox6.Controls.Add(this.Ütem_státus);
            this.GroupBox6.Controls.Add(this.Ütem_takarítási_fajta);
            this.GroupBox6.Controls.Add(this.Label12);
            this.GroupBox6.Controls.Add(this.Label14);
            this.GroupBox6.Controls.Add(this.Label15);
            this.GroupBox6.Controls.Add(this.Label16);
            this.GroupBox6.Controls.Add(this.Ütem_kezdődátum);
            this.GroupBox6.Controls.Add(this.Ütem_azonosító);
            this.GroupBox6.Location = new System.Drawing.Point(670, 6);
            this.GroupBox6.Name = "GroupBox6";
            this.GroupBox6.Size = new System.Drawing.Size(634, 463);
            this.GroupBox6.TabIndex = 1;
            this.GroupBox6.TabStop = false;
            this.GroupBox6.Text = "Ütemezés beállítás";
            // 
            // Excell_Ütem_tábla
            // 
            this.Excell_Ütem_tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Excell_Ütem_tábla.Image = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excell_Ütem_tábla.Location = new System.Drawing.Point(587, 71);
            this.Excell_Ütem_tábla.Name = "Excell_Ütem_tábla";
            this.Excell_Ütem_tábla.Size = new System.Drawing.Size(40, 40);
            this.Excell_Ütem_tábla.TabIndex = 206;
            this.ToolTip1.SetToolTip(this.Excell_Ütem_tábla, "Excel táblázatot készít a táblázat adataiból");
            this.Excell_Ütem_tábla.UseVisualStyleBackColor = true;
            this.Excell_Ütem_tábla.Click += new System.EventHandler(this.Excell_Ütem_tábla_Click);
            // 
            // Ütem_Tábla
            // 
            this.Ütem_Tábla.AllowUserToAddRows = false;
            this.Ütem_Tábla.AllowUserToDeleteRows = false;
            this.Ütem_Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Ütem_Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Ütem_Tábla.Location = new System.Drawing.Point(6, 226);
            this.Ütem_Tábla.Name = "Ütem_Tábla";
            this.Ütem_Tábla.RowHeadersVisible = false;
            this.Ütem_Tábla.RowHeadersWidth = 62;
            this.Ütem_Tábla.Size = new System.Drawing.Size(625, 231);
            this.Ütem_Tábla.TabIndex = 191;
            this.Ütem_Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Ütem_Tábla_CellClick);
            this.Ütem_Tábla.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Ütem_Tábla_CellFormatting);
            // 
            // Ütem_Rögzítés
            // 
            this.Ütem_Rögzítés.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Ütem_Rögzítés.Image = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Ütem_Rögzítés.Location = new System.Drawing.Point(587, 24);
            this.Ütem_Rögzítés.Margin = new System.Windows.Forms.Padding(4);
            this.Ütem_Rögzítés.Name = "Ütem_Rögzítés";
            this.Ütem_Rögzítés.Size = new System.Drawing.Size(40, 40);
            this.Ütem_Rögzítés.TabIndex = 205;
            this.ToolTip1.SetToolTip(this.Ütem_Rögzítés, "Rögzíti/módosítja az adatokat");
            this.Ütem_Rögzítés.UseVisualStyleBackColor = true;
            this.Ütem_Rögzítés.Click += new System.EventHandler(this.Ütem_Rögzítés_Click);
            // 
            // Ütem_frissít
            // 
            this.Ütem_frissít.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Ütem_frissít.Image = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Ütem_frissít.Location = new System.Drawing.Point(587, 116);
            this.Ütem_frissít.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Ütem_frissít.Name = "Ütem_frissít";
            this.Ütem_frissít.Size = new System.Drawing.Size(40, 40);
            this.Ütem_frissít.TabIndex = 204;
            this.ToolTip1.SetToolTip(this.Ütem_frissít, "Frissíti a táblázatot");
            this.Ütem_frissít.UseVisualStyleBackColor = true;
            this.Ütem_frissít.Click += new System.EventHandler(this.Ütem_frissít_Click);
            // 
            // Ütem_növekmény
            // 
            this.Ütem_növekmény.Location = new System.Drawing.Point(141, 156);
            this.Ütem_növekmény.Name = "Ütem_növekmény";
            this.Ütem_növekmény.Size = new System.Drawing.Size(100, 26);
            this.Ütem_növekmény.TabIndex = 203;
            // 
            // Ütem_mérték
            // 
            this.Ütem_mérték.FormattingEnabled = true;
            this.Ütem_mérték.Location = new System.Drawing.Point(141, 188);
            this.Ütem_mérték.Name = "Ütem_mérték";
            this.Ütem_mérték.Size = new System.Drawing.Size(145, 28);
            this.Ütem_mérték.TabIndex = 202;
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.Location = new System.Drawing.Point(6, 191);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(99, 20);
            this.Label18.TabIndex = 201;
            this.Label18.Text = "Ütem lépték:";
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.Location = new System.Drawing.Point(6, 162);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(125, 20);
            this.Label17.TabIndex = 200;
            this.Label17.Text = "Ütem nagysága:";
            // 
            // Ütem_telephely
            // 
            this.Ütem_telephely.FormattingEnabled = true;
            this.Ütem_telephely.Location = new System.Drawing.Point(141, 122);
            this.Ütem_telephely.Name = "Ütem_telephely";
            this.Ütem_telephely.Size = new System.Drawing.Size(145, 28);
            this.Ütem_telephely.TabIndex = 199;
            // 
            // Ütem_státus
            // 
            this.Ütem_státus.AutoSize = true;
            this.Ütem_státus.Location = new System.Drawing.Point(313, 192);
            this.Ütem_státus.Name = "Ütem_státus";
            this.Ütem_státus.Size = new System.Drawing.Size(68, 24);
            this.Ütem_státus.TabIndex = 198;
            this.Ütem_státus.Text = "Törölt";
            this.Ütem_státus.UseVisualStyleBackColor = true;
            // 
            // Ütem_takarítási_fajta
            // 
            this.Ütem_takarítási_fajta.FormattingEnabled = true;
            this.Ütem_takarítási_fajta.Location = new System.Drawing.Point(141, 90);
            this.Ütem_takarítási_fajta.Name = "Ütem_takarítási_fajta";
            this.Ütem_takarítási_fajta.Size = new System.Drawing.Size(145, 28);
            this.Ütem_takarítási_fajta.TabIndex = 197;
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(6, 98);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(116, 20);
            this.Label12.TabIndex = 196;
            this.Label12.Text = "Takarítási fajta:";
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(6, 130);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(80, 20);
            this.Label14.TabIndex = 195;
            this.Label14.Text = "Telephely:";
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Location = new System.Drawing.Point(6, 64);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(107, 20);
            this.Label15.TabIndex = 194;
            this.Label15.Text = "Kezdő dátum:";
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Location = new System.Drawing.Point(6, 34);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(84, 20);
            this.Label16.TabIndex = 193;
            this.Label16.Text = "Azonosító:";
            // 
            // Ütem_kezdődátum
            // 
            this.Ütem_kezdődátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Ütem_kezdődátum.Location = new System.Drawing.Point(141, 58);
            this.Ütem_kezdődátum.Name = "Ütem_kezdődátum";
            this.Ütem_kezdődátum.Size = new System.Drawing.Size(115, 26);
            this.Ütem_kezdődátum.TabIndex = 192;
            // 
            // Ütem_azonosító
            // 
            this.Ütem_azonosító.DropDownHeight = 400;
            this.Ütem_azonosító.FormattingEnabled = true;
            this.Ütem_azonosító.IntegralHeight = false;
            this.Ütem_azonosító.Location = new System.Drawing.Point(141, 24);
            this.Ütem_azonosító.Name = "Ütem_azonosító";
            this.Ütem_azonosító.Size = new System.Drawing.Size(145, 28);
            this.Ütem_azonosító.TabIndex = 191;
            // 
            // GroupBox5
            // 
            this.GroupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.GroupBox5.BackColor = System.Drawing.Color.RoyalBlue;
            this.GroupBox5.Controls.Add(this.PályaszámTakarításai);
            this.GroupBox5.Controls.Add(this.Excel_Takarítás);
            this.GroupBox5.Controls.Add(this.Utolsó_telephely);
            this.GroupBox5.Controls.Add(this.Utolsó_státus);
            this.GroupBox5.Controls.Add(this.Utolsó_történet);
            this.GroupBox5.Controls.Add(this.Utolsó_takarítási_fajta);
            this.GroupBox5.Controls.Add(this.Label11);
            this.GroupBox5.Controls.Add(this.Label10);
            this.GroupBox5.Controls.Add(this.Label5);
            this.GroupBox5.Controls.Add(this.Label2);
            this.GroupBox5.Controls.Add(this.Utolsó_dátum);
            this.GroupBox5.Controls.Add(this.Utolsó_pályaszám);
            this.GroupBox5.Controls.Add(this.Utolsó_módosít);
            this.GroupBox5.Controls.Add(this.Utolsó_frissít);
            this.GroupBox5.Controls.Add(this.Tábla_utolsó);
            this.GroupBox5.Location = new System.Drawing.Point(5, 5);
            this.GroupBox5.Name = "GroupBox5";
            this.GroupBox5.Size = new System.Drawing.Size(659, 464);
            this.GroupBox5.TabIndex = 0;
            this.GroupBox5.TabStop = false;
            this.GroupBox5.Text = "Utolsó takarítások";
            // 
            // PályaszámTakarításai
            // 
            this.PályaszámTakarításai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PályaszámTakarításai.Image = global::Villamos.Properties.Resources.App_spreadsheet1;
            this.PályaszámTakarításai.Location = new System.Drawing.Point(566, 120);
            this.PályaszámTakarításai.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PályaszámTakarításai.Name = "PályaszámTakarításai";
            this.PályaszámTakarításai.Size = new System.Drawing.Size(40, 40);
            this.PályaszámTakarításai.TabIndex = 192;
            this.ToolTip1.SetToolTip(this.PályaszámTakarításai, "Utolsó takarítások");
            this.PályaszámTakarításai.UseVisualStyleBackColor = true;
            this.PályaszámTakarításai.Click += new System.EventHandler(this.PályaszámTakarításai_Click);
            // 
            // Excel_Takarítás
            // 
            this.Excel_Takarítás.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Excel_Takarítás.Image = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excel_Takarítás.Location = new System.Drawing.Point(612, 73);
            this.Excel_Takarítás.Name = "Excel_Takarítás";
            this.Excel_Takarítás.Size = new System.Drawing.Size(40, 40);
            this.Excel_Takarítás.TabIndex = 191;
            this.ToolTip1.SetToolTip(this.Excel_Takarítás, "Excel táblázatot készít a táblázat adataiból");
            this.Excel_Takarítás.UseVisualStyleBackColor = true;
            this.Excel_Takarítás.Click += new System.EventHandler(this.Excel_Takarítás_Click);
            // 
            // Utolsó_telephely
            // 
            this.Utolsó_telephely.FormattingEnabled = true;
            this.Utolsó_telephely.Location = new System.Drawing.Point(141, 121);
            this.Utolsó_telephely.Name = "Utolsó_telephely";
            this.Utolsó_telephely.Size = new System.Drawing.Size(145, 28);
            this.Utolsó_telephely.TabIndex = 190;
            // 
            // Utolsó_státus
            // 
            this.Utolsó_státus.AutoSize = true;
            this.Utolsó_státus.Location = new System.Drawing.Point(141, 155);
            this.Utolsó_státus.Name = "Utolsó_státus";
            this.Utolsó_státus.Size = new System.Drawing.Size(68, 24);
            this.Utolsó_státus.TabIndex = 189;
            this.Utolsó_státus.Text = "Törölt";
            this.Utolsó_státus.UseVisualStyleBackColor = true;
            // 
            // Utolsó_történet
            // 
            this.Utolsó_történet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Utolsó_történet.Image = global::Villamos.Properties.Resources.BeCardStack;
            this.Utolsó_történet.Location = new System.Drawing.Point(520, 121);
            this.Utolsó_történet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Utolsó_történet.Name = "Utolsó_történet";
            this.Utolsó_történet.Size = new System.Drawing.Size(40, 40);
            this.Utolsó_történet.TabIndex = 188;
            this.ToolTip1.SetToolTip(this.Utolsó_történet, "Pályaszámhoz tartozó takarítás történet");
            this.Utolsó_történet.UseVisualStyleBackColor = true;
            this.Utolsó_történet.Click += new System.EventHandler(this.Utolsó_történet_Click);
            // 
            // Utolsó_takarítási_fajta
            // 
            this.Utolsó_takarítási_fajta.FormattingEnabled = true;
            this.Utolsó_takarítási_fajta.Location = new System.Drawing.Point(141, 89);
            this.Utolsó_takarítási_fajta.Name = "Utolsó_takarítási_fajta";
            this.Utolsó_takarítási_fajta.Size = new System.Drawing.Size(145, 28);
            this.Utolsó_takarítási_fajta.TabIndex = 187;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(6, 97);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(116, 20);
            this.Label11.TabIndex = 184;
            this.Label11.Text = "Takarítási fajta:";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(6, 129);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(80, 20);
            this.Label10.TabIndex = 183;
            this.Label10.Text = "Telephely:";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(6, 65);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(61, 20);
            this.Label5.TabIndex = 182;
            this.Label5.Text = "Dátum:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(6, 35);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(84, 20);
            this.Label2.TabIndex = 181;
            this.Label2.Text = "Azonosító:";
            // 
            // Utolsó_dátum
            // 
            this.Utolsó_dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Utolsó_dátum.Location = new System.Drawing.Point(141, 59);
            this.Utolsó_dátum.Name = "Utolsó_dátum";
            this.Utolsó_dátum.Size = new System.Drawing.Size(115, 26);
            this.Utolsó_dátum.TabIndex = 180;
            // 
            // Utolsó_pályaszám
            // 
            this.Utolsó_pályaszám.DropDownHeight = 400;
            this.Utolsó_pályaszám.FormattingEnabled = true;
            this.Utolsó_pályaszám.IntegralHeight = false;
            this.Utolsó_pályaszám.Location = new System.Drawing.Point(141, 25);
            this.Utolsó_pályaszám.Name = "Utolsó_pályaszám";
            this.Utolsó_pályaszám.Size = new System.Drawing.Size(145, 28);
            this.Utolsó_pályaszám.TabIndex = 179;
            // 
            // Utolsó_módosít
            // 
            this.Utolsó_módosít.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Utolsó_módosít.Image = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Utolsó_módosít.Location = new System.Drawing.Point(612, 26);
            this.Utolsó_módosít.Margin = new System.Windows.Forms.Padding(4);
            this.Utolsó_módosít.Name = "Utolsó_módosít";
            this.Utolsó_módosít.Size = new System.Drawing.Size(40, 40);
            this.Utolsó_módosít.TabIndex = 178;
            this.ToolTip1.SetToolTip(this.Utolsó_módosít, "Rögzíti/módosítja az adatokat");
            this.Utolsó_módosít.UseVisualStyleBackColor = true;
            this.Utolsó_módosít.Click += new System.EventHandler(this.Utolsó_módosít_Click);
            // 
            // Utolsó_frissít
            // 
            this.Utolsó_frissít.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Utolsó_frissít.Image = global::Villamos.Properties.Resources.leadott;
            this.Utolsó_frissít.Location = new System.Drawing.Point(612, 120);
            this.Utolsó_frissít.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Utolsó_frissít.Name = "Utolsó_frissít";
            this.Utolsó_frissít.Size = new System.Drawing.Size(40, 40);
            this.Utolsó_frissít.TabIndex = 177;
            this.ToolTip1.SetToolTip(this.Utolsó_frissít, "Ütemezéshez pályaszám felvétele/Törlése");
            this.Utolsó_frissít.UseVisualStyleBackColor = true;
            this.Utolsó_frissít.Click += new System.EventHandler(this.Utolsó_frissít_Click);
            // 
            // Tábla_utolsó
            // 
            this.Tábla_utolsó.AllowUserToAddRows = false;
            this.Tábla_utolsó.AllowUserToDeleteRows = false;
            this.Tábla_utolsó.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Tábla_utolsó.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla_utolsó.FilterAndSortEnabled = true;
            this.Tábla_utolsó.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla_utolsó.Location = new System.Drawing.Point(6, 184);
            this.Tábla_utolsó.MaxFilterButtonImageHeight = 23;
            this.Tábla_utolsó.Name = "Tábla_utolsó";
            this.Tábla_utolsó.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tábla_utolsó.RowHeadersVisible = false;
            this.Tábla_utolsó.RowHeadersWidth = 62;
            this.Tábla_utolsó.Size = new System.Drawing.Size(647, 274);
            this.Tábla_utolsó.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla_utolsó.TabIndex = 0;
            this.Tábla_utolsó.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_utolsó_CellClick);
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.Blue;
            this.TabPage2.Controls.Add(this.Btn_vezénylésexcel);
            this.TabPage2.Controls.Add(this.Kereső_hívó);
            this.TabPage2.Controls.Add(this.Alap_Lista);
            this.TabPage2.Controls.Add(this.Ütemezés_lista);
            this.TabPage2.Controls.Add(this.Btn_Vezénylésbeírás);
            this.TabPage2.Controls.Add(this.BtnExcelkimenet);
            this.TabPage2.Controls.Add(this.Tábla);
            this.TabPage2.Controls.Add(this.Dátum);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(1309, 475);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Ütemezés";
            // 
            // Btn_vezénylésexcel
            // 
            this.Btn_vezénylésexcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_vezénylésexcel.Image = global::Villamos.Properties.Resources.CALC1;
            this.Btn_vezénylésexcel.Location = new System.Drawing.Point(1205, 51);
            this.Btn_vezénylésexcel.Name = "Btn_vezénylésexcel";
            this.Btn_vezénylésexcel.Size = new System.Drawing.Size(40, 40);
            this.Btn_vezénylésexcel.TabIndex = 96;
            this.ToolTip1.SetToolTip(this.Btn_vezénylésexcel, "Feladatterv készítés");
            this.Btn_vezénylésexcel.UseVisualStyleBackColor = true;
            this.Btn_vezénylésexcel.Click += new System.EventHandler(this.Btn_vezénylésexcel_Click);
            // 
            // Kereső_hívó
            // 
            this.Kereső_hívó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Kereső_hívó.Image = global::Villamos.Properties.Resources.Nagyító;
            this.Kereső_hívó.Location = new System.Drawing.Point(1113, 6);
            this.Kereső_hívó.Name = "Kereső_hívó";
            this.Kereső_hívó.Size = new System.Drawing.Size(40, 40);
            this.Kereső_hívó.TabIndex = 3;
            this.ToolTip1.SetToolTip(this.Kereső_hívó, "Keresés a táblázatban");
            this.Kereső_hívó.UseVisualStyleBackColor = true;
            this.Kereső_hívó.Click += new System.EventHandler(this.Kereső_hívó_Click);
            // 
            // Alap_Lista
            // 
            this.Alap_Lista.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Alap_Lista.Image = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Alap_Lista.Location = new System.Drawing.Point(1113, 51);
            this.Alap_Lista.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Alap_Lista.Name = "Alap_Lista";
            this.Alap_Lista.Size = new System.Drawing.Size(40, 40);
            this.Alap_Lista.TabIndex = 1;
            this.ToolTip1.SetToolTip(this.Alap_Lista, "Frissíti a képernyő adatait");
            this.Alap_Lista.UseVisualStyleBackColor = true;
            this.Alap_Lista.Click += new System.EventHandler(this.Alap_Lista_Click);
            // 
            // Ütemezés_lista
            // 
            this.Ütemezés_lista.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Ütemezés_lista.FormattingEnabled = true;
            this.Ütemezés_lista.ItemHeight = 20;
            this.Ütemezés_lista.Location = new System.Drawing.Point(1113, 97);
            this.Ütemezés_lista.Name = "Ütemezés_lista";
            this.Ütemezés_lista.Size = new System.Drawing.Size(190, 364);
            this.Ütemezés_lista.TabIndex = 83;
            this.Ütemezés_lista.SelectedIndexChanged += new System.EventHandler(this.Ütemezés_lista_SelectedIndexChanged);
            // 
            // Btn_Vezénylésbeírás
            // 
            this.Btn_Vezénylésbeírás.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Vezénylésbeírás.Image = global::Villamos.Properties.Resources.leadott;
            this.Btn_Vezénylésbeírás.Location = new System.Drawing.Point(1251, 51);
            this.Btn_Vezénylésbeírás.Name = "Btn_Vezénylésbeírás";
            this.Btn_Vezénylésbeírás.Size = new System.Drawing.Size(40, 40);
            this.Btn_Vezénylésbeírás.TabIndex = 4;
            this.ToolTip1.SetToolTip(this.Btn_Vezénylésbeírás, "Jármű karbantartási adatok közé írja");
            this.Btn_Vezénylésbeírás.UseVisualStyleBackColor = true;
            this.Btn_Vezénylésbeírás.Click += new System.EventHandler(this.Btn_Vezénylésbeírás_Click);
            // 
            // BtnExcelkimenet
            // 
            this.BtnExcelkimenet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnExcelkimenet.Image = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.BtnExcelkimenet.Location = new System.Drawing.Point(1159, 51);
            this.BtnExcelkimenet.Name = "BtnExcelkimenet";
            this.BtnExcelkimenet.Size = new System.Drawing.Size(40, 40);
            this.BtnExcelkimenet.TabIndex = 2;
            this.ToolTip1.SetToolTip(this.BtnExcelkimenet, "Táblázat Excelbe exportálása");
            this.BtnExcelkimenet.UseVisualStyleBackColor = true;
            this.BtnExcelkimenet.Click += new System.EventHandler(this.BtnExcelkimenet_Click);
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            this.Tábla.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.Location = new System.Drawing.Point(5, 5);
            this.Tábla.Name = "Tábla";
            this.Tábla.RowHeadersWidth = 25;
            this.Tábla.Size = new System.Drawing.Size(1102, 464);
            this.Tábla.TabIndex = 65;
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            // 
            // Dátum
            // 
            this.Dátum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum.Location = new System.Drawing.Point(1168, 6);
            this.Dátum.Name = "Dátum";
            this.Dátum.Size = new System.Drawing.Size(123, 26);
            this.Dátum.TabIndex = 0;
            this.Dátum.ValueChanged += new System.EventHandler(this.Dátum_ValueChanged);
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.Blue;
            this.TabPage3.Controls.Add(this.GroupBox3);
            this.TabPage3.Controls.Add(this.GroupBox2);
            this.TabPage3.Controls.Add(this.GroupBox4);
            this.TabPage3.Controls.Add(this.GroupBox1);
            this.TabPage3.Controls.Add(this.Panel3);
            this.TabPage3.Location = new System.Drawing.Point(4, 29);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(1309, 475);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Elkészült takarítás Rögzítés";
            // 
            // GroupBox3
            // 
            this.GroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.GroupBox3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.GroupBox3.BackColor = System.Drawing.Color.Blue;
            this.GroupBox3.Controls.Add(this.Opció_tábla);
            this.GroupBox3.Controls.Add(this.Label21);
            this.GroupBox3.Controls.Add(this.Label20);
            this.GroupBox3.Controls.Add(this.Label19);
            this.GroupBox3.Controls.Add(this.Opció_Töröl);
            this.GroupBox3.Controls.Add(this.Opció_terület);
            this.GroupBox3.Controls.Add(this.Button2);
            this.GroupBox3.Controls.Add(this.Opció_lista);
            this.GroupBox3.Controls.Add(this.Opció_psz);
            this.GroupBox3.Controls.Add(this.Opció_mentés);
            this.GroupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.GroupBox3.ForeColor = System.Drawing.Color.White;
            this.GroupBox3.Location = new System.Drawing.Point(3, 272);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(558, 198);
            this.GroupBox3.TabIndex = 7;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "Eseti és Graffiti ";
            // 
            // Opció_tábla
            // 
            this.Opció_tábla.AllowUserToAddRows = false;
            this.Opció_tábla.AllowUserToDeleteRows = false;
            this.Opció_tábla.AllowUserToResizeColumns = false;
            this.Opció_tábla.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Opció_tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.Opció_tábla.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Opció_tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Opció_tábla.EnableHeadersVisualStyles = false;
            this.Opció_tábla.Location = new System.Drawing.Point(161, 89);
            this.Opció_tábla.Name = "Opció_tábla";
            this.Opció_tábla.RowHeadersVisible = false;
            this.Opció_tábla.RowHeadersWidth = 62;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.Opció_tábla.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.Opció_tábla.Size = new System.Drawing.Size(235, 103);
            this.Opció_tábla.TabIndex = 149;
            this.Opció_tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Opció_tábla_CellClick);
            // 
            // Label21
            // 
            this.Label21.AutoSize = true;
            this.Label21.Location = new System.Drawing.Point(157, 23);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(94, 20);
            this.Label21.TabIndex = 148;
            this.Label21.Text = "Pályaszám";
            // 
            // Label20
            // 
            this.Label20.AutoSize = true;
            this.Label20.Location = new System.Drawing.Point(296, 23);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(88, 20);
            this.Label20.TabIndex = 147;
            this.Label20.Text = "felület m2";
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.Location = new System.Drawing.Point(5, 23);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(101, 20);
            this.Label19.TabIndex = 146;
            this.Label19.Text = "Opció fajta:";
            // 
            // Opció_Töröl
            // 
            this.Opció_Töröl.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Opció_Töröl.Image = global::Villamos.Properties.Resources.Kuka;
            this.Opció_Töröl.Location = new System.Drawing.Point(505, 113);
            this.Opció_Töröl.Name = "Opció_Töröl";
            this.Opció_Töröl.Size = new System.Drawing.Size(40, 40);
            this.Opció_Töröl.TabIndex = 5;
            this.ToolTip1.SetToolTip(this.Opció_Töröl, "Listában kijelöltek törlése");
            this.Opció_Töröl.UseVisualStyleBackColor = false;
            this.Opció_Töröl.Click += new System.EventHandler(this.Opció_Töröl_Click);
            // 
            // Opció_terület
            // 
            this.Opció_terület.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Opció_terület.Location = new System.Drawing.Point(299, 56);
            this.Opció_terület.Name = "Opció_terület";
            this.Opció_terület.Size = new System.Drawing.Size(97, 26);
            this.Opció_terület.TabIndex = 2;
            // 
            // Button2
            // 
            this.Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Button2.Image = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Button2.Location = new System.Drawing.Point(505, 68);
            this.Button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(40, 40);
            this.Button2.TabIndex = 4;
            this.ToolTip1.SetToolTip(this.Button2, "Frissít");
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Opció_lista
            // 
            this.Opció_lista.FormattingEnabled = true;
            this.Opció_lista.Location = new System.Drawing.Point(5, 54);
            this.Opció_lista.Name = "Opció_lista";
            this.Opció_lista.Size = new System.Drawing.Size(150, 28);
            this.Opció_lista.TabIndex = 1;
            this.Opció_lista.SelectedIndexChanged += new System.EventHandler(this.Opció_lista_SelectedIndexChanged);
            this.Opció_lista.Click += new System.EventHandler(this.Opció_lista_Click);
            // 
            // Opció_psz
            // 
            this.Opció_psz.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Opció_psz.Location = new System.Drawing.Point(161, 56);
            this.Opció_psz.Name = "Opció_psz";
            this.Opció_psz.Size = new System.Drawing.Size(130, 26);
            this.Opció_psz.TabIndex = 1;
            // 
            // Opció_mentés
            // 
            this.Opció_mentés.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Opció_mentés.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Opció_mentés.Image = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Opció_mentés.Location = new System.Drawing.Point(505, 23);
            this.Opció_mentés.Name = "Opció_mentés";
            this.Opció_mentés.Size = new System.Drawing.Size(40, 40);
            this.Opció_mentés.TabIndex = 3;
            this.ToolTip1.SetToolTip(this.Opció_mentés, "Rögzít");
            this.Opció_mentés.UseVisualStyleBackColor = false;
            this.Opció_mentés.Click += new System.EventHandler(this.Opció_mentés_Click);
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.Lét_Előírt);
            this.GroupBox2.Controls.Add(this.Lét_Viselt);
            this.GroupBox2.Controls.Add(this.Lét_Megjelent);
            this.GroupBox2.Controls.Add(this.Label7);
            this.GroupBox2.Controls.Add(this.Label1);
            this.GroupBox2.Controls.Add(this.Label9);
            this.GroupBox2.Controls.Add(this.LétszámMentés);
            this.GroupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.GroupBox2.ForeColor = System.Drawing.Color.White;
            this.GroupBox2.Location = new System.Drawing.Point(3, 166);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(558, 100);
            this.GroupBox2.TabIndex = 6;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Létszám";
            // 
            // Lét_Előírt
            // 
            this.Lét_Előírt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Lét_Előírt.Location = new System.Drawing.Point(14, 60);
            this.Lét_Előírt.Name = "Lét_Előírt";
            this.Lét_Előírt.Size = new System.Drawing.Size(120, 26);
            this.Lét_Előírt.TabIndex = 0;
            this.Lét_Előírt.Click += new System.EventHandler(this.Lét_Előírt_Click);
            // 
            // Lét_Viselt
            // 
            this.Lét_Viselt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Lét_Viselt.Location = new System.Drawing.Point(266, 60);
            this.Lét_Viselt.Name = "Lét_Viselt";
            this.Lét_Viselt.Size = new System.Drawing.Size(120, 26);
            this.Lét_Viselt.TabIndex = 2;
            this.Lét_Viselt.Click += new System.EventHandler(this.Lét_Előírt_Click);
            // 
            // Lét_Megjelent
            // 
            this.Lét_Megjelent.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Lét_Megjelent.Location = new System.Drawing.Point(140, 60);
            this.Lét_Megjelent.Name = "Lét_Megjelent";
            this.Lét_Megjelent.Size = new System.Drawing.Size(120, 26);
            this.Lét_Megjelent.TabIndex = 1;
            this.Lét_Megjelent.Click += new System.EventHandler(this.Lét_Előírt_Click);
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(262, 30);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(218, 20);
            this.Label7.TabIndex = 147;
            this.Label7.Text = "Előírt ruházatot nem viselt";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(136, 30);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(87, 20);
            this.Label1.TabIndex = 146;
            this.Label1.Text = "Megjelent";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(10, 30);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(51, 20);
            this.Label9.TabIndex = 145;
            this.Label9.Text = "Előírt";
            // 
            // LétszámMentés
            // 
            this.LétszámMentés.BackColor = System.Drawing.Color.WhiteSmoke;
            this.LétszámMentés.Image = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.LétszámMentés.Location = new System.Drawing.Point(507, 41);
            this.LétszámMentés.Name = "LétszámMentés";
            this.LétszámMentés.Size = new System.Drawing.Size(45, 45);
            this.LétszámMentés.TabIndex = 3;
            this.ToolTip1.SetToolTip(this.LétszámMentés, "Rögzít");
            this.LétszámMentés.UseVisualStyleBackColor = false;
            this.LétszámMentés.Click += new System.EventHandler(this.LétszámMentés_Click);
            // 
            // GroupBox4
            // 
            this.GroupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.GroupBox4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.GroupBox4.BackColor = System.Drawing.Color.Blue;
            this.GroupBox4.Controls.Add(this.Label22);
            this.GroupBox4.Controls.Add(this.Label23);
            this.GroupBox4.Controls.Add(this.JK_törlés);
            this.GroupBox4.Controls.Add(this.Button1);
            this.GroupBox4.Controls.Add(this.JK_pót);
            this.GroupBox4.Controls.Add(this.JK_Kategória);
            this.GroupBox4.Controls.Add(this.Panel1);
            this.GroupBox4.Controls.Add(this.JK_List);
            this.GroupBox4.Controls.Add(this.Panel5);
            this.GroupBox4.Controls.Add(this.JK_Azonosító);
            this.GroupBox4.Controls.Add(this.JK_Mentés);
            this.GroupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.GroupBox4.ForeColor = System.Drawing.Color.White;
            this.GroupBox4.Location = new System.Drawing.Point(695, 3);
            this.GroupBox4.Name = "GroupBox4";
            this.GroupBox4.Size = new System.Drawing.Size(555, 467);
            this.GroupBox4.TabIndex = 5;
            this.GroupBox4.TabStop = false;
            this.GroupBox4.Text = "Jármű takarítások";
            // 
            // Label22
            // 
            this.Label22.AutoSize = true;
            this.Label22.Location = new System.Drawing.Point(158, 25);
            this.Label22.Name = "Label22";
            this.Label22.Size = new System.Drawing.Size(94, 20);
            this.Label22.TabIndex = 176;
            this.Label22.Text = "Pályaszám";
            // 
            // Label23
            // 
            this.Label23.AutoSize = true;
            this.Label23.Location = new System.Drawing.Point(6, 25);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(129, 20);
            this.Label23.TabIndex = 175;
            this.Label23.Text = "Takarítás fajta:";
            // 
            // JK_törlés
            // 
            this.JK_törlés.BackColor = System.Drawing.Color.WhiteSmoke;
            this.JK_törlés.Image = global::Villamos.Properties.Resources.Kuka;
            this.JK_törlés.Location = new System.Drawing.Point(502, 113);
            this.JK_törlés.Name = "JK_törlés";
            this.JK_törlés.Size = new System.Drawing.Size(40, 40);
            this.JK_törlés.TabIndex = 7;
            this.ToolTip1.SetToolTip(this.JK_törlés, "Listában kijelöltek törlése");
            this.JK_törlés.UseVisualStyleBackColor = false;
            this.JK_törlés.Click += new System.EventHandler(this.JK_törlés_Click);
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Button1.Image = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Button1.Location = new System.Drawing.Point(502, 68);
            this.Button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(40, 40);
            this.Button1.TabIndex = 3;
            this.ToolTip1.SetToolTip(this.Button1, "Frissít");
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // JK_pót
            // 
            this.JK_pót.AutoSize = true;
            this.JK_pót.Location = new System.Drawing.Point(332, 155);
            this.JK_pót.Name = "JK_pót";
            this.JK_pót.Size = new System.Drawing.Size(121, 24);
            this.JK_pót.TabIndex = 174;
            this.JK_pót.Text = "Póthatáridő";
            this.JK_pót.UseVisualStyleBackColor = true;
            // 
            // JK_Kategória
            // 
            this.JK_Kategória.FormattingEnabled = true;
            this.JK_Kategória.Location = new System.Drawing.Point(5, 48);
            this.JK_Kategória.Name = "JK_Kategória";
            this.JK_Kategória.Size = new System.Drawing.Size(150, 28);
            this.JK_Kategória.TabIndex = 0;
            this.JK_Kategória.SelectedIndexChanged += new System.EventHandler(this.JK_Kategória_SelectedIndexChanged);
            this.JK_Kategória.Click += new System.EventHandler(this.JK_Kategória_Click);
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.Teal;
            this.Panel1.Controls.Add(this.JK_megfelel2);
            this.Panel1.Controls.Add(this.JK_Nem2);
            this.Panel1.Location = new System.Drawing.Point(332, 207);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(164, 79);
            this.Panel1.TabIndex = 172;
            // 
            // JK_megfelel2
            // 
            this.JK_megfelel2.AutoSize = true;
            this.JK_megfelel2.BackColor = System.Drawing.Color.DarkTurquoise;
            this.JK_megfelel2.Checked = true;
            this.JK_megfelel2.Location = new System.Drawing.Point(10, 10);
            this.JK_megfelel2.Name = "JK_megfelel2";
            this.JK_megfelel2.Size = new System.Drawing.Size(105, 24);
            this.JK_megfelel2.TabIndex = 0;
            this.JK_megfelel2.TabStop = true;
            this.JK_megfelel2.Text = "Megfelelő";
            this.JK_megfelel2.UseVisualStyleBackColor = false;
            // 
            // JK_Nem2
            // 
            this.JK_Nem2.AutoSize = true;
            this.JK_Nem2.BackColor = System.Drawing.Color.DarkTurquoise;
            this.JK_Nem2.Location = new System.Drawing.Point(10, 45);
            this.JK_Nem2.Name = "JK_Nem2";
            this.JK_Nem2.Size = new System.Drawing.Size(146, 24);
            this.JK_Nem2.TabIndex = 1;
            this.JK_Nem2.Text = "Nem megfelelő";
            this.JK_Nem2.UseVisualStyleBackColor = false;
            // 
            // JK_List
            // 
            this.JK_List.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.JK_List.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.JK_List.FormattingEnabled = true;
            this.JK_List.ItemHeight = 20;
            this.JK_List.Location = new System.Drawing.Point(161, 77);
            this.JK_List.Name = "JK_List";
            this.JK_List.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.JK_List.Size = new System.Drawing.Size(164, 364);
            this.JK_List.TabIndex = 2;
            this.JK_List.SelectedIndexChanged += new System.EventHandler(this.JK_List_SelectedIndexChanged);
            // 
            // Panel5
            // 
            this.Panel5.BackColor = System.Drawing.Color.Teal;
            this.Panel5.Controls.Add(this.JK_Törölt);
            this.Panel5.Controls.Add(this.JK_Megfelel1);
            this.Panel5.Controls.Add(this.JK_Nem1);
            this.Panel5.Location = new System.Drawing.Point(332, 25);
            this.Panel5.Name = "Panel5";
            this.Panel5.Size = new System.Drawing.Size(164, 112);
            this.Panel5.TabIndex = 170;
            // 
            // JK_Törölt
            // 
            this.JK_Törölt.AutoSize = true;
            this.JK_Törölt.BackColor = System.Drawing.Color.DarkTurquoise;
            this.JK_Törölt.Location = new System.Drawing.Point(10, 80);
            this.JK_Törölt.Name = "JK_Törölt";
            this.JK_Törölt.Size = new System.Drawing.Size(73, 24);
            this.JK_Törölt.TabIndex = 2;
            this.JK_Törölt.Text = "Törölt";
            this.JK_Törölt.UseVisualStyleBackColor = false;
            this.JK_Törölt.Click += new System.EventHandler(this.J2_Megfelel_Click);
            // 
            // JK_Megfelel1
            // 
            this.JK_Megfelel1.AutoSize = true;
            this.JK_Megfelel1.BackColor = System.Drawing.Color.DarkTurquoise;
            this.JK_Megfelel1.Checked = true;
            this.JK_Megfelel1.Location = new System.Drawing.Point(10, 10);
            this.JK_Megfelel1.Name = "JK_Megfelel1";
            this.JK_Megfelel1.Size = new System.Drawing.Size(105, 24);
            this.JK_Megfelel1.TabIndex = 0;
            this.JK_Megfelel1.TabStop = true;
            this.JK_Megfelel1.Text = "Megfelelő";
            this.JK_Megfelel1.UseVisualStyleBackColor = false;
            this.JK_Megfelel1.Click += new System.EventHandler(this.J2_Megfelel_Click);
            // 
            // JK_Nem1
            // 
            this.JK_Nem1.AutoSize = true;
            this.JK_Nem1.BackColor = System.Drawing.Color.DarkTurquoise;
            this.JK_Nem1.Location = new System.Drawing.Point(10, 45);
            this.JK_Nem1.Name = "JK_Nem1";
            this.JK_Nem1.Size = new System.Drawing.Size(146, 24);
            this.JK_Nem1.TabIndex = 1;
            this.JK_Nem1.Text = "Nem megfelelő";
            this.JK_Nem1.UseVisualStyleBackColor = false;
            this.JK_Nem1.Click += new System.EventHandler(this.JK_Nem1_Click);
            // 
            // JK_Azonosító
            // 
            this.JK_Azonosító.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.JK_Azonosító.Location = new System.Drawing.Point(161, 48);
            this.JK_Azonosító.Name = "JK_Azonosító";
            this.JK_Azonosító.Size = new System.Drawing.Size(164, 26);
            this.JK_Azonosító.TabIndex = 1;
            // 
            // JK_Mentés
            // 
            this.JK_Mentés.BackColor = System.Drawing.Color.WhiteSmoke;
            this.JK_Mentés.Image = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.JK_Mentés.Location = new System.Drawing.Point(502, 23);
            this.JK_Mentés.Name = "JK_Mentés";
            this.JK_Mentés.Size = new System.Drawing.Size(40, 40);
            this.JK_Mentés.TabIndex = 2;
            this.ToolTip1.SetToolTip(this.JK_Mentés, "Rögzít");
            this.JK_Mentés.UseVisualStyleBackColor = false;
            this.JK_Mentés.Click += new System.EventHandler(this.JK_Mentés_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.BackColor = System.Drawing.Color.Blue;
            this.GroupBox1.Controls.Add(this.J1NemMegfelelő);
            this.GroupBox1.Controls.Add(this.J1Megfelelő);
            this.GroupBox1.Controls.Add(this.Label6);
            this.GroupBox1.Controls.Add(this.Label4);
            this.GroupBox1.Controls.Add(this.Label3);
            this.GroupBox1.Controls.Add(this.J1Típus);
            this.GroupBox1.Controls.Add(this.J1Mentés);
            this.GroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.GroupBox1.ForeColor = System.Drawing.Color.White;
            this.GroupBox1.Location = new System.Drawing.Point(3, 60);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(558, 100);
            this.GroupBox1.TabIndex = 2;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "J1 mennyiségek rögzítése \"Söprés\"";
            // 
            // J1NemMegfelelő
            // 
            this.J1NemMegfelelő.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.J1NemMegfelelő.Location = new System.Drawing.Point(300, 60);
            this.J1NemMegfelelő.Name = "J1NemMegfelelő";
            this.J1NemMegfelelő.Size = new System.Drawing.Size(120, 26);
            this.J1NemMegfelelő.TabIndex = 2;
            this.J1NemMegfelelő.Click += new System.EventHandler(this.J1Típus_Click);
            // 
            // J1Megfelelő
            // 
            this.J1Megfelelő.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.J1Megfelelő.Location = new System.Drawing.Point(172, 60);
            this.J1Megfelelő.Name = "J1Megfelelő";
            this.J1Megfelelő.Size = new System.Drawing.Size(120, 26);
            this.J1Megfelelő.TabIndex = 1;
            this.J1Megfelelő.Click += new System.EventHandler(this.J1Típus_Click);
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(291, 30);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(158, 20);
            this.Label6.TabIndex = 147;
            this.Label6.Text = "Nem megfelelő db:";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(168, 30);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(117, 20);
            this.Label4.TabIndex = 146;
            this.Label4.Text = "Megfelelő db:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(10, 30);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(57, 20);
            this.Label3.TabIndex = 145;
            this.Label3.Text = "Típus:";
            // 
            // J1Típus
            // 
            this.J1Típus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.J1Típus.FormattingEnabled = true;
            this.J1Típus.Location = new System.Drawing.Point(5, 60);
            this.J1Típus.Name = "J1Típus";
            this.J1Típus.Size = new System.Drawing.Size(160, 28);
            this.J1Típus.TabIndex = 0;
            this.ToolTip1.SetToolTip(this.J1Típus, "Típus");
            this.J1Típus.Click += new System.EventHandler(this.J1Típus_Click);
            // 
            // J1Mentés
            // 
            this.J1Mentés.BackColor = System.Drawing.Color.LimeGreen;
            this.J1Mentés.Image = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.J1Mentés.Location = new System.Drawing.Point(507, 41);
            this.J1Mentés.Name = "J1Mentés";
            this.J1Mentés.Size = new System.Drawing.Size(45, 45);
            this.J1Mentés.TabIndex = 3;
            this.ToolTip1.SetToolTip(this.J1Mentés, "Rögzít");
            this.J1Mentés.UseVisualStyleBackColor = false;
            this.J1Mentés.Click += new System.EventHandler(this.J1Mentés_Click);
            // 
            // Panel3
            // 
            this.Panel3.Controls.Add(this.RadioButton7);
            this.Panel3.Controls.Add(this.Jnappal);
            this.Panel3.Controls.Add(this.JDátum);
            this.Panel3.Location = new System.Drawing.Point(3, 13);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(318, 41);
            this.Panel3.TabIndex = 1;
            // 
            // RadioButton7
            // 
            this.RadioButton7.AutoSize = true;
            this.RadioButton7.BackColor = System.Drawing.Color.DarkTurquoise;
            this.RadioButton7.Location = new System.Drawing.Point(221, 7);
            this.RadioButton7.Name = "RadioButton7";
            this.RadioButton7.Size = new System.Drawing.Size(83, 24);
            this.RadioButton7.TabIndex = 2;
            this.RadioButton7.Text = "Éjszaka";
            this.RadioButton7.UseVisualStyleBackColor = false;
            // 
            // Jnappal
            // 
            this.Jnappal.AutoSize = true;
            this.Jnappal.BackColor = System.Drawing.Color.DarkTurquoise;
            this.Jnappal.Checked = true;
            this.Jnappal.Location = new System.Drawing.Point(138, 7);
            this.Jnappal.Name = "Jnappal";
            this.Jnappal.Size = new System.Drawing.Size(77, 24);
            this.Jnappal.TabIndex = 1;
            this.Jnappal.TabStop = true;
            this.Jnappal.Text = "Nappal";
            this.Jnappal.UseVisualStyleBackColor = false;
            this.Jnappal.CheckedChanged += new System.EventHandler(this.Jnappal_CheckedChanged);
            // 
            // JDátum
            // 
            this.JDátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.JDátum.Location = new System.Drawing.Point(5, 5);
            this.JDátum.Name = "JDátum";
            this.JDátum.Size = new System.Drawing.Size(118, 26);
            this.JDátum.TabIndex = 0;
            this.JDátum.ValueChanged += new System.EventHandler(this.JDátum_ValueChanged);
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.Turquoise;
            this.TabPage4.Controls.Add(this.BMR);
            this.TabPage4.Controls.Add(this.TIG_Készítés);
            this.TabPage4.Controls.Add(this.Button5);
            this.TabPage4.Controls.Add(this.Button4);
            this.TabPage4.Controls.Add(this.Panel4);
            this.TabPage4.Controls.Add(this.ListaTábla);
            this.TabPage4.Controls.Add(this.Havi_Lekérdezés);
            this.TabPage4.Controls.Add(this.Lek_excel);
            this.TabPage4.Controls.Add(this.ListaDátum);
            this.TabPage4.Controls.Add(this.Panel6);
            this.TabPage4.Location = new System.Drawing.Point(4, 29);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Size = new System.Drawing.Size(1309, 475);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Lekérdezések";
            // 
            // BMR
            // 
            this.BMR.BackgroundImage = global::Villamos.Properties.Resources.App_spreadsheet1;
            this.BMR.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BMR.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BMR.Location = new System.Drawing.Point(1255, 11);
            this.BMR.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BMR.Name = "BMR";
            this.BMR.Size = new System.Drawing.Size(45, 45);
            this.BMR.TabIndex = 196;
            this.BMR.Text = "BMR";
            this.BMR.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ToolTip1.SetToolTip(this.BMR, "Takarítási Terv-Tény-Eltérés");
            this.BMR.UseVisualStyleBackColor = true;
            this.BMR.Click += new System.EventHandler(this.BMR_Click);
            // 
            // TIG_Készítés
            // 
            this.TIG_Készítés.BackgroundImage = global::Villamos.Properties.Resources.Aha_Soft_Large_Seo_SEO;
            this.TIG_Készítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TIG_Készítés.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TIG_Készítés.ForeColor = System.Drawing.Color.Black;
            this.TIG_Készítés.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.TIG_Készítés.Location = new System.Drawing.Point(1188, 11);
            this.TIG_Készítés.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TIG_Készítés.Name = "TIG_Készítés";
            this.TIG_Készítés.Size = new System.Drawing.Size(45, 45);
            this.TIG_Készítés.TabIndex = 183;
            this.TIG_Készítés.Text = "TIG";
            this.TIG_Készítés.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ToolTip1.SetToolTip(this.TIG_Készítés, "TIG Készítés");
            this.TIG_Készítés.UseVisualStyleBackColor = true;
            this.TIG_Készítés.Click += new System.EventHandler(this.TIG_Készítés_Click);
            // 
            // Button5
            // 
            this.Button5.Image = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Button5.Location = new System.Drawing.Point(1086, 12);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(45, 45);
            this.Button5.TabIndex = 182;
            this.ToolTip1.SetToolTip(this.Button5, "Táblázat Excelbe exportálása");
            this.Button5.UseVisualStyleBackColor = true;
            this.Button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // Button4
            // 
            this.Button4.Image = global::Villamos.Properties.Resources.App_dict;
            this.Button4.Location = new System.Drawing.Point(1035, 11);
            this.Button4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(45, 45);
            this.Button4.TabIndex = 181;
            this.ToolTip1.SetToolTip(this.Button4, "Kiírja a két takarítás között eltelt időt.");
            this.Button4.UseVisualStyleBackColor = true;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // Panel4
            // 
            this.Panel4.BackColor = System.Drawing.Color.Teal;
            this.Panel4.Controls.Add(this.Lekérdezés_Kategória);
            this.Panel4.Controls.Add(this.RadioButton6);
            this.Panel4.Controls.Add(this.RadioButton3);
            this.Panel4.Controls.Add(this.RadioButton4);
            this.Panel4.Location = new System.Drawing.Point(318, 12);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(642, 45);
            this.Panel4.TabIndex = 180;
            // 
            // Lekérdezés_Kategória
            // 
            this.Lekérdezés_Kategória.FormattingEnabled = true;
            this.Lekérdezés_Kategória.Location = new System.Drawing.Point(219, 8);
            this.Lekérdezés_Kategória.Name = "Lekérdezés_Kategória";
            this.Lekérdezés_Kategória.Size = new System.Drawing.Size(139, 28);
            this.Lekérdezés_Kategória.TabIndex = 4;
            this.Lekérdezés_Kategória.Click += new System.EventHandler(this.Lekérdezés_Kategória_Click);
            // 
            // RadioButton6
            // 
            this.RadioButton6.AutoSize = true;
            this.RadioButton6.BackColor = System.Drawing.Color.DarkTurquoise;
            this.RadioButton6.Location = new System.Drawing.Point(125, 10);
            this.RadioButton6.Name = "RadioButton6";
            this.RadioButton6.Size = new System.Drawing.Size(88, 24);
            this.RadioButton6.TabIndex = 3;
            this.RadioButton6.Text = "Létszám";
            this.RadioButton6.UseVisualStyleBackColor = false;
            // 
            // RadioButton3
            // 
            this.RadioButton3.AutoSize = true;
            this.RadioButton3.BackColor = System.Drawing.Color.DarkTurquoise;
            this.RadioButton3.Checked = true;
            this.RadioButton3.Location = new System.Drawing.Point(10, 10);
            this.RadioButton3.Name = "RadioButton3";
            this.RadioButton3.Size = new System.Drawing.Size(109, 24);
            this.RadioButton3.TabIndex = 0;
            this.RadioButton3.TabStop = true;
            this.RadioButton3.Text = "J1 takarítás";
            this.RadioButton3.UseVisualStyleBackColor = false;
            // 
            // RadioButton4
            // 
            this.RadioButton4.AutoSize = true;
            this.RadioButton4.BackColor = System.Drawing.Color.DarkTurquoise;
            this.RadioButton4.Location = new System.Drawing.Point(364, 10);
            this.RadioButton4.Name = "RadioButton4";
            this.RadioButton4.Size = new System.Drawing.Size(267, 24);
            this.RadioButton4.TabIndex = 1;
            this.RadioButton4.Text = "J2-J3-J4-J5-J6-Graffiti-... takarítás";
            this.RadioButton4.UseVisualStyleBackColor = false;
            // 
            // ListaTábla
            // 
            this.ListaTábla.AllowUserToAddRows = false;
            this.ListaTábla.AllowUserToDeleteRows = false;
            this.ListaTábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListaTábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListaTábla.Location = new System.Drawing.Point(3, 62);
            this.ListaTábla.Name = "ListaTábla";
            this.ListaTábla.RowHeadersVisible = false;
            this.ListaTábla.RowHeadersWidth = 62;
            this.ListaTábla.Size = new System.Drawing.Size(1300, 410);
            this.ListaTábla.TabIndex = 179;
            // 
            // Havi_Lekérdezés
            // 
            this.Havi_Lekérdezés.Image = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Havi_Lekérdezés.Location = new System.Drawing.Point(966, 12);
            this.Havi_Lekérdezés.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Havi_Lekérdezés.Name = "Havi_Lekérdezés";
            this.Havi_Lekérdezés.Size = new System.Drawing.Size(45, 45);
            this.Havi_Lekérdezés.TabIndex = 176;
            this.ToolTip1.SetToolTip(this.Havi_Lekérdezés, "Frissiti a táblázat adatait");
            this.Havi_Lekérdezés.UseVisualStyleBackColor = true;
            this.Havi_Lekérdezés.Click += new System.EventHandler(this.Havi_Lekérdezés_Click);
            // 
            // Lek_excel
            // 
            this.Lek_excel.Image = global::Villamos.Properties.Resources.Aha_Soft_Large_Seo_SEO;
            this.Lek_excel.Location = new System.Drawing.Point(1137, 11);
            this.Lek_excel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Lek_excel.Name = "Lek_excel";
            this.Lek_excel.Size = new System.Drawing.Size(45, 45);
            this.Lek_excel.TabIndex = 177;
            this.ToolTip1.SetToolTip(this.Lek_excel, "Excel kimutatás készítés");
            this.Lek_excel.UseVisualStyleBackColor = true;
            this.Lek_excel.Click += new System.EventHandler(this.Lek_excel_Click);
            // 
            // ListaDátum
            // 
            this.ListaDátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ListaDátum.Location = new System.Drawing.Point(3, 12);
            this.ListaDátum.Name = "ListaDátum";
            this.ListaDátum.Size = new System.Drawing.Size(118, 26);
            this.ListaDátum.TabIndex = 174;
            // 
            // Panel6
            // 
            this.Panel6.BackColor = System.Drawing.Color.Teal;
            this.Panel6.Controls.Add(this.RadioButton2);
            this.Panel6.Controls.Add(this.RadioButton1);
            this.Panel6.Location = new System.Drawing.Point(127, 11);
            this.Panel6.Name = "Panel6";
            this.Panel6.Size = new System.Drawing.Size(185, 45);
            this.Panel6.TabIndex = 175;
            // 
            // RadioButton2
            // 
            this.RadioButton2.AutoSize = true;
            this.RadioButton2.BackColor = System.Drawing.Color.DarkTurquoise;
            this.RadioButton2.Location = new System.Drawing.Point(93, 10);
            this.RadioButton2.Name = "RadioButton2";
            this.RadioButton2.Size = new System.Drawing.Size(83, 24);
            this.RadioButton2.TabIndex = 1;
            this.RadioButton2.Text = "Éjszaka";
            this.RadioButton2.UseVisualStyleBackColor = false;
            // 
            // RadioButton1
            // 
            this.RadioButton1.AutoSize = true;
            this.RadioButton1.BackColor = System.Drawing.Color.DarkTurquoise;
            this.RadioButton1.Checked = true;
            this.RadioButton1.Location = new System.Drawing.Point(10, 10);
            this.RadioButton1.Name = "RadioButton1";
            this.RadioButton1.Size = new System.Drawing.Size(77, 24);
            this.RadioButton1.TabIndex = 0;
            this.RadioButton1.TabStop = true;
            this.RadioButton1.Text = "Nappal";
            this.RadioButton1.UseVisualStyleBackColor = false;
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.tabPage5.Controls.Add(this.Gépi_Tábla);
            this.tabPage5.Controls.Add(this.groupBox8);
            this.tabPage5.Controls.Add(this.groupBox9);
            this.tabPage5.Controls.Add(this.Gepi_excel);
            this.tabPage5.Location = new System.Drawing.Point(4, 29);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(1309, 475);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Gépi Mosás";
            // 
            // Gépi_Tábla
            // 
            this.Gépi_Tábla.AllowUserToAddRows = false;
            this.Gépi_Tábla.AllowUserToDeleteRows = false;
            this.Gépi_Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Gépi_Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Gépi_Tábla.FilterAndSortEnabled = true;
            this.Gépi_Tábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Gépi_Tábla.Location = new System.Drawing.Point(5, 110);
            this.Gépi_Tábla.MaxFilterButtonImageHeight = 23;
            this.Gépi_Tábla.Name = "Gépi_Tábla";
            this.Gépi_Tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Gépi_Tábla.RowHeadersVisible = false;
            this.Gépi_Tábla.RowHeadersWidth = 62;
            this.Gépi_Tábla.Size = new System.Drawing.Size(1295, 359);
            this.Gépi_Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Gépi_Tábla.TabIndex = 0;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.tableLayoutPanel1);
            this.groupBox8.Location = new System.Drawing.Point(429, 5);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(600, 99);
            this.groupBox8.TabIndex = 208;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Szűrés";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.CmbGépiTíp, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Tel_TB, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label25, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label28, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.Pály_TB, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.Gepi_frissit, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label24, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Rögzítések, 3, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(70, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(522, 77);
            this.tableLayoutPanel1.TabIndex = 200;
            // 
            // CmbGépiTíp
            // 
            this.CmbGépiTíp.DropDownHeight = 400;
            this.CmbGépiTíp.FormattingEnabled = true;
            this.CmbGépiTíp.IntegralHeight = false;
            this.CmbGépiTíp.Location = new System.Drawing.Point(3, 33);
            this.CmbGépiTíp.Name = "CmbGépiTíp";
            this.CmbGépiTíp.Size = new System.Drawing.Size(113, 28);
            this.CmbGépiTíp.TabIndex = 207;
            // 
            // Tel_TB
            // 
            this.Tel_TB.DropDownHeight = 200;
            this.Tel_TB.FormattingEnabled = true;
            this.Tel_TB.IntegralHeight = false;
            this.Tel_TB.Location = new System.Drawing.Point(256, 33);
            this.Tel_TB.Name = "Tel_TB";
            this.Tel_TB.Size = new System.Drawing.Size(128, 28);
            this.Tel_TB.TabIndex = 208;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(122, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(89, 20);
            this.label25.TabIndex = 193;
            this.label25.Text = "Pályaszám:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(256, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(80, 20);
            this.label28.TabIndex = 194;
            this.label28.Text = "Telephely:";
            // 
            // Pály_TB
            // 
            this.Pály_TB.Location = new System.Drawing.Point(122, 33);
            this.Pály_TB.Name = "Pály_TB";
            this.Pály_TB.Size = new System.Drawing.Size(128, 26);
            this.Pály_TB.TabIndex = 196;
            // 
            // Gepi_frissit
            // 
            this.Gepi_frissit.Image = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Gepi_frissit.Location = new System.Drawing.Point(390, 32);
            this.Gepi_frissit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Gepi_frissit.Name = "Gepi_frissit";
            this.Gepi_frissit.Size = new System.Drawing.Size(40, 40);
            this.Gepi_frissit.TabIndex = 199;
            this.ToolTip1.SetToolTip(this.Gepi_frissit, "Pályaszámhoz tartozó utolsó takarítások");
            this.Gepi_frissit.UseVisualStyleBackColor = true;
            this.Gepi_frissit.Click += new System.EventHandler(this.Gepi_lista);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(3, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(51, 20);
            this.label24.TabIndex = 192;
            this.label24.Text = "Típus:";
            // 
            // Rögzítések
            // 
            this.Rögzítések.AutoSize = true;
            this.Rögzítések.Location = new System.Drawing.Point(390, 3);
            this.Rögzítések.Name = "Rögzítések";
            this.Rögzítések.Size = new System.Drawing.Size(108, 24);
            this.Rögzítések.TabIndex = 209;
            this.Rögzítések.Text = "Rögzítések";
            this.Rögzítések.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            this.groupBox9.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.groupBox9.Controls.Add(this.tableLayoutPanel2);
            this.groupBox9.Location = new System.Drawing.Point(5, 5);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(418, 99);
            this.groupBox9.TabIndex = 209;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Rögzítés";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.label27, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.Gepi_torolt, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.Gepi_rogzit, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.Gepi_datum, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.Gepi_pályaszám, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label26, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 25);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(403, 68);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(3, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(84, 20);
            this.label27.TabIndex = 203;
            this.label27.Text = "Azonosító:";
            // 
            // Gepi_torolt
            // 
            this.Gepi_torolt.AutoSize = true;
            this.Gepi_torolt.Location = new System.Drawing.Point(273, 23);
            this.Gepi_torolt.Name = "Gepi_torolt";
            this.Gepi_torolt.Size = new System.Drawing.Size(68, 24);
            this.Gepi_torolt.TabIndex = 206;
            this.Gepi_torolt.Text = "Törölt";
            this.Gepi_torolt.UseVisualStyleBackColor = true;
            // 
            // Gepi_rogzit
            // 
            this.Gepi_rogzit.Image = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Gepi_rogzit.Location = new System.Drawing.Point(348, 24);
            this.Gepi_rogzit.Margin = new System.Windows.Forms.Padding(4);
            this.Gepi_rogzit.Name = "Gepi_rogzit";
            this.Gepi_rogzit.Size = new System.Drawing.Size(40, 40);
            this.Gepi_rogzit.TabIndex = 200;
            this.ToolTip1.SetToolTip(this.Gepi_rogzit, "Rögzítés");
            this.Gepi_rogzit.UseVisualStyleBackColor = true;
            this.Gepi_rogzit.Click += new System.EventHandler(this.Gepi_rogzit_Click);
            // 
            // Gepi_datum
            // 
            this.Gepi_datum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Gepi_datum.Location = new System.Drawing.Point(122, 23);
            this.Gepi_datum.Name = "Gepi_datum";
            this.Gepi_datum.Size = new System.Drawing.Size(145, 26);
            this.Gepi_datum.TabIndex = 202;
            // 
            // Gepi_pályaszám
            // 
            this.Gepi_pályaszám.DropDownHeight = 400;
            this.Gepi_pályaszám.FormattingEnabled = true;
            this.Gepi_pályaszám.IntegralHeight = false;
            this.Gepi_pályaszám.Location = new System.Drawing.Point(3, 23);
            this.Gepi_pályaszám.Name = "Gepi_pályaszám";
            this.Gepi_pályaszám.Size = new System.Drawing.Size(113, 28);
            this.Gepi_pályaszám.TabIndex = 201;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(122, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(61, 20);
            this.label26.TabIndex = 204;
            this.label26.Text = "Dátum:";
            // 
            // Gepi_excel
            // 
            this.Gepi_excel.Image = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Gepi_excel.Location = new System.Drawing.Point(1035, 53);
            this.Gepi_excel.Name = "Gepi_excel";
            this.Gepi_excel.Size = new System.Drawing.Size(40, 40);
            this.Gepi_excel.TabIndex = 207;
            this.ToolTip1.SetToolTip(this.Gepi_excel, "Táblázat Excelbe exportálása");
            this.Gepi_excel.UseVisualStyleBackColor = true;
            this.Gepi_excel.Click += new System.EventHandler(this.Gepi_excel_Click);
            // 
            // ToolTip1
            // 
            this.ToolTip1.IsBalloon = true;
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.Image = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.Location = new System.Drawing.Point(1269, 6);
            this.BtnSúgó.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(40, 40);
            this.BtnSúgó.TabIndex = 1;
            this.ToolTip1.SetToolTip(this.BtnSúgó, "Súgó");
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Ablak_Jármű_takarítás_új
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.ClientSize = new System.Drawing.Size(1324, 562);
            this.Controls.Add(this.Lapfülek);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.BtnSúgó);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Jármű_takarítás_új";
            this.Text = "Jármű Takarítás";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_Jármű_takarítás_új_FormClosed);
            this.Load += new System.EventHandler(this.Ablak_Jármű_takarítás_új_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ESC_KeyDown);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.Lapfülek.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.GroupBox6.ResumeLayout(false);
            this.GroupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Ütem_Tábla)).EndInit();
            this.GroupBox5.ResumeLayout(false);
            this.GroupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_utolsó)).EndInit();
            this.TabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.TabPage3.ResumeLayout(false);
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Opció_tábla)).EndInit();
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.GroupBox4.ResumeLayout(false);
            this.GroupBox4.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.Panel5.ResumeLayout(false);
            this.Panel5.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            this.TabPage4.ResumeLayout(false);
            this.Panel4.ResumeLayout(false);
            this.Panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListaTábla)).EndInit();
            this.Panel6.ResumeLayout(false);
            this.Panel6.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Gépi_Tábla)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }



        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal Panel Panel2;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal Button BtnSúgó;
        internal TabControl Lapfülek;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal TabPage TabPage3;
        internal TabPage TabPage4;
        internal DataGridView Tábla;
        internal DateTimePicker Dátum;
        internal Button Btn_Vezénylésbeírás;
        internal Button BtnExcelkimenet;
        internal ListBox Ütemezés_lista;
        internal Button Alap_Lista;
        internal Panel Panel3;
        internal RadioButton RadioButton7;
        internal RadioButton Jnappal;
        internal DateTimePicker JDátum;
        internal GroupBox GroupBox1;
        internal TextBox J1NemMegfelelő;
        internal TextBox J1Megfelelő;
        internal Label Label6;
        internal Label Label4;
        internal Label Label3;
        internal ComboBox J1Típus;
        internal Button J1Mentés;
        internal Button Kereső_hívó;
        internal GroupBox GroupBox4;
        internal CheckBox JK_pót;
        internal ComboBox JK_Kategória;
        internal Panel Panel1;
        internal RadioButton JK_megfelel2;
        internal RadioButton JK_Nem2;
        internal ListBox JK_List;
        internal Panel Panel5;
        internal RadioButton JK_Törölt;
        internal RadioButton JK_Megfelel1;
        internal RadioButton JK_Nem1;
        internal TextBox JK_Azonosító;
        internal Button JK_Mentés;
        internal Button Button1;
        internal GroupBox GroupBox2;
        internal TextBox Lét_Előírt;
        internal TextBox Lét_Viselt;
        internal TextBox Lét_Megjelent;
        internal Label Label7;
        internal Label Label1;
        internal Label Label9;
        internal Button LétszámMentés;
        internal GroupBox GroupBox3;
        internal TextBox Opció_terület;
        internal Button Button2;
        internal ComboBox Opció_lista;
        internal TextBox Opció_psz;
        internal Button Opció_mentés;
        internal Button Opció_Töröl;
        internal DataGridView ListaTábla;
        internal Button Havi_Lekérdezés;
        internal Button Lek_excel;
        internal DateTimePicker ListaDátum;
        internal Panel Panel6;
        internal RadioButton RadioButton2;
        internal RadioButton RadioButton1;
        internal Panel Panel4;
        internal ComboBox Lekérdezés_Kategória;
        internal RadioButton RadioButton6;
        internal RadioButton RadioButton3;
        internal RadioButton RadioButton4;
        internal GroupBox GroupBox6;
        internal GroupBox GroupBox5;
        internal Label Label11;
        internal Label Label10;
        internal Label Label5;
        internal Label Label2;
        internal DateTimePicker Utolsó_dátum;
        internal ComboBox Utolsó_pályaszám;
        internal Button Utolsó_módosít;
        internal Button Utolsó_frissít;
        internal Zuby.ADGV.AdvancedDataGridView Tábla_utolsó;
        internal ComboBox Utolsó_takarítási_fajta;
        internal Button Utolsó_történet;
        internal CheckBox Utolsó_státus;
        internal ComboBox Utolsó_telephely;
        internal Button Ütem_Rögzítés;
        internal Button Ütem_frissít;
        internal TextBox Ütem_növekmény;
        internal ComboBox Ütem_mérték;
        internal Label Label18;
        internal Label Label17;
        internal ComboBox Ütem_telephely;
        internal CheckBox Ütem_státus;
        internal ComboBox Ütem_takarítási_fajta;
        internal Label Label12;
        internal Label Label14;
        internal Label Label15;
        internal Label Label16;
        internal DateTimePicker Ütem_kezdődátum;
        internal ComboBox Ütem_azonosító;
        internal DataGridView Ütem_Tábla;
        internal Button JK_törlés;
        internal Label Label21;
        internal Label Label20;
        internal Label Label19;
        internal Label Label22;
        internal Label Label23;
        internal ToolTip ToolTip1;
        internal Button Excell_Ütem_tábla;
        internal Button Excel_Takarítás;
        internal Button Button5;
        internal Button Button4;
        internal Button Btn_vezénylésexcel;
        internal DataGridView Opció_tábla;
        internal TabPage tabPage5;
        internal Zuby.ADGV.AdvancedDataGridView Gépi_Tábla;
        internal GroupBox groupBox8;
        internal Label label24;
        internal Label label28;
        internal TextBox Pály_TB;
        internal Label label25;
        internal Button Gepi_excel;
        internal CheckBox Gepi_torolt;
        internal Label label26;
        internal Label label27;
        internal DateTimePicker Gepi_datum;
        internal ComboBox Gepi_pályaszám;
        internal Button Gepi_rogzit;
        internal Button Gepi_frissit;
        internal GroupBox groupBox9;
        internal ComboBox Tel_TB;
        internal TableLayoutPanel tableLayoutPanel1;
        internal TableLayoutPanel tableLayoutPanel2;
        internal CheckBox Rögzítések;
        internal ComboBox CmbGépiTíp;
        internal Button TIG_Készítés;
        internal Button BMR;
        private Timer timer1;
        internal Button PályaszámTakarításai;
    }
}