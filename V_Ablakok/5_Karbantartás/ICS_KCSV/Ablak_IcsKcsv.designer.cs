using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{

    public partial class Ablak_IcsKcsv : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_IcsKcsv));
            this.Pályaszám = new System.Windows.Forms.ComboBox();
            this.Excel_gomb = new System.Windows.Forms.Button();
            this.Pályaszámkereső = new System.Windows.Forms.Button();
            this.Label15 = new System.Windows.Forms.Label();
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.Panel7 = new System.Windows.Forms.Panel();
            this.Kerékcsökkenés = new System.Windows.Forms.TextBox();
            this.Label39 = new System.Windows.Forms.Label();
            this.FőHoltart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.AlHoltart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.Command1 = new System.Windows.Forms.Button();
            this.Panel5 = new System.Windows.Forms.Panel();
            this.Text2 = new System.Windows.Forms.TextBox();
            this.Label38 = new System.Windows.Forms.Label();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.Option12 = new System.Windows.Forms.RadioButton();
            this.Option11 = new System.Windows.Forms.RadioButton();
            this.Option10 = new System.Windows.Forms.RadioButton();
            this.Label37 = new System.Windows.Forms.Label();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Text1 = new System.Windows.Forms.TextBox();
            this.Option8 = new System.Windows.Forms.RadioButton();
            this.Option9 = new System.Windows.Forms.RadioButton();
            this.Option7 = new System.Windows.Forms.RadioButton();
            this.Option6 = new System.Windows.Forms.RadioButton();
            this.Option5 = new System.Windows.Forms.RadioButton();
            this.Label36 = new System.Windows.Forms.Label();
            this.PszJelölő = new System.Windows.Forms.CheckedListBox();
            this.Mindentkijelöl = new System.Windows.Forms.Button();
            this.Kijelöléstörlése = new System.Windows.Forms.Button();
            this.Command3 = new System.Windows.Forms.Button();
            this.Panel6 = new System.Windows.Forms.Panel();
            this.Check1 = new System.Windows.Forms.CheckBox();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.Command2 = new System.Windows.Forms.Button();
            this.Telephely = new System.Windows.Forms.ComboBox();
            this.Label16 = new System.Windows.Forms.Label();
            this.TabPage6 = new System.Windows.Forms.TabPage();
            this.VizsAdat_Excel = new System.Windows.Forms.Button();
            this.Tábla1 = new System.Windows.Forms.DataGridView();
            this.VizsAdat_Frissít = new System.Windows.Forms.Button();
            this.TabPage5 = new System.Windows.Forms.TabPage();
            this.Töröl = new System.Windows.Forms.Button();
            this.KövV2km = new System.Windows.Forms.TextBox();
            this.KövV2_Sorszám = new System.Windows.Forms.TextBox();
            this.KövV_Sorszám = new System.Windows.Forms.TextBox();
            this.KövV2_számláló = new System.Windows.Forms.TextBox();
            this.KövV2 = new System.Windows.Forms.TextBox();
            this.KövV1km = new System.Windows.Forms.TextBox();
            this.KövV = new System.Windows.Forms.TextBox();
            this.Sorszám = new System.Windows.Forms.TextBox();
            this.Jjavszám = new System.Windows.Forms.TextBox();
            this.KMUkm = new System.Windows.Forms.TextBox();
            this.VizsgKm = new System.Windows.Forms.TextBox();
            this.Vizsgfok = new System.Windows.Forms.TextBox();
            this.HaviKm = new System.Windows.Forms.TextBox();
            this.TEljesKmText = new System.Windows.Forms.TextBox();
            this.Label35 = new System.Windows.Forms.Label();
            this.Label34 = new System.Windows.Forms.Label();
            this.CiklusrendCombo = new System.Windows.Forms.ComboBox();
            this.Üzemek = new System.Windows.Forms.ComboBox();
            this.Vizsgsorszám = new System.Windows.Forms.ComboBox();
            this.KMUdátum = new System.Windows.Forms.DateTimePicker();
            this.Utolsófelújításdátuma = new System.Windows.Forms.DateTimePicker();
            this.Vizsgdátumk = new System.Windows.Forms.DateTimePicker();
            this.Vizsgdátumv = new System.Windows.Forms.DateTimePicker();
            this.SAP_adatok = new System.Windows.Forms.Button();
            this.Új_adat = new System.Windows.Forms.Button();
            this.Utolsó_V_rögzítés = new System.Windows.Forms.Button();
            this.Label33 = new System.Windows.Forms.Label();
            this.Label32 = new System.Windows.Forms.Label();
            this.Label31 = new System.Windows.Forms.Label();
            this.Label30 = new System.Windows.Forms.Label();
            this.Label29 = new System.Windows.Forms.Label();
            this.Label28 = new System.Windows.Forms.Label();
            this.Label27 = new System.Windows.Forms.Label();
            this.Label26 = new System.Windows.Forms.Label();
            this.Label25 = new System.Windows.Forms.Label();
            this.Label24 = new System.Windows.Forms.Label();
            this.Label23 = new System.Windows.Forms.Label();
            this.Label22 = new System.Windows.Forms.Label();
            this.Label21 = new System.Windows.Forms.Label();
            this.Label20 = new System.Windows.Forms.Label();
            this.Label19 = new System.Windows.Forms.Label();
            this.Label18 = new System.Windows.Forms.Label();
            this.Label17 = new System.Windows.Forms.Label();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.Button1 = new System.Windows.Forms.Button();
            this.Teljes_adatbázis_excel = new System.Windows.Forms.Button();
            this.Tábla_lekérdezés = new System.Windows.Forms.DataGridView();
            this.Excellekérdezés = new System.Windows.Forms.Button();
            this.Lekérdezés_lekérdezés = new System.Windows.Forms.Button();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.Button3 = new System.Windows.Forms.Button();
            this.E_rögzít = new System.Windows.Forms.Button();
            this.Combo_E3 = new System.Windows.Forms.ComboBox();
            this.Combo_E2 = new System.Windows.Forms.ComboBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Típus_text = new System.Windows.Forms.Label();
            this.Státus_text = new System.Windows.Forms.Label();
            this.Miótaáll_text = new System.Windows.Forms.Label();
            this.Takarítás_text = new System.Windows.Forms.Label();
            this.Főmérnökség_text = new System.Windows.Forms.Label();
            this.Járműtípus_text = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Fülek = new System.Windows.Forms.TabControl();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.Button4 = new System.Windows.Forms.Button();
            this.Btn_Vezénylésbeírás = new System.Windows.Forms.Button();
            this.Tábla_vezénylés = new System.Windows.Forms.DataGridView();
            this.Dátum_ütem = new System.Windows.Forms.DateTimePicker();
            this.Ütem_frissít = new System.Windows.Forms.Button();
            this.Tábla_ütemező = new System.Windows.Forms.DataGridView();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Panel2.SuspendLayout();
            this.TabPage3.SuspendLayout();
            this.Panel7.SuspendLayout();
            this.Panel5.SuspendLayout();
            this.Panel4.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.Panel6.SuspendLayout();
            this.Panel3.SuspendLayout();
            this.TabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla1)).BeginInit();
            this.TabPage5.SuspendLayout();
            this.TabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_lekérdezés)).BeginInit();
            this.TabPage1.SuspendLayout();
            this.Fülek.SuspendLayout();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_vezénylés)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_ütemező)).BeginInit();
            this.SuspendLayout();
            // 
            // Pályaszám
            // 
            this.Pályaszám.FormattingEnabled = true;
            this.Pályaszám.Location = new System.Drawing.Point(448, 15);
            this.Pályaszám.Name = "Pályaszám";
            this.Pályaszám.Size = new System.Drawing.Size(124, 28);
            this.Pályaszám.TabIndex = 166;
            this.Pályaszám.SelectedIndexChanged += new System.EventHandler(this.Pályaszám_SelectedIndexChanged);
            // 
            // Excel_gomb
            // 
            this.Excel_gomb.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excel_gomb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excel_gomb.Location = new System.Drawing.Point(629, 5);
            this.Excel_gomb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Excel_gomb.Name = "Excel_gomb";
            this.Excel_gomb.Size = new System.Drawing.Size(45, 45);
            this.Excel_gomb.TabIndex = 173;
            this.ToolTip1.SetToolTip(this.Excel_gomb, "Állománytábla mentése Excelbe");
            this.Excel_gomb.UseVisualStyleBackColor = true;
            this.Excel_gomb.Click += new System.EventHandler(this.Excel_gomb_Click);
            // 
            // Pályaszámkereső
            // 
            this.Pályaszámkereső.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Pályaszámkereső.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Pályaszámkereső.Location = new System.Drawing.Point(578, 5);
            this.Pályaszámkereső.Name = "Pályaszámkereső";
            this.Pályaszámkereső.Size = new System.Drawing.Size(45, 45);
            this.Pályaszámkereső.TabIndex = 172;
            this.ToolTip1.SetToolTip(this.Pályaszámkereső, "Adatok frissítése");
            this.Pályaszámkereső.UseVisualStyleBackColor = true;
            this.Pályaszámkereső.Click += new System.EventHandler(this.Pályaszámkereső_Click);
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Location = new System.Drawing.Point(353, 23);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(89, 20);
            this.Label15.TabIndex = 167;
            this.Label15.Text = "Pályaszám:";
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.BackColor = System.Drawing.Color.Aquamarine;
            this.Holtart.Location = new System.Drawing.Point(706, 15);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(393, 28);
            this.Holtart.TabIndex = 170;
            this.Holtart.Visible = false;
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1116, 5);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 169;
            this.ToolTip1.SetToolTip(this.BtnSúgó, "Súgó");
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.Cmbtelephely);
            this.Panel2.Controls.Add(this.Label13);
            this.Panel2.Location = new System.Drawing.Point(5, 12);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(346, 36);
            this.Panel2.TabIndex = 168;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(148, 5);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
            this.Cmbtelephely.SelectedIndexChanged += new System.EventHandler(this.Cmbtelephely_SelectedIndexChanged);
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(3, 8);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(145, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.TabPage3.Controls.Add(this.Panel7);
            this.TabPage3.Controls.Add(this.FőHoltart);
            this.TabPage3.Controls.Add(this.AlHoltart);
            this.TabPage3.Controls.Add(this.Command1);
            this.TabPage3.Controls.Add(this.Panel5);
            this.TabPage3.Controls.Add(this.Panel4);
            this.TabPage3.Controls.Add(this.Panel1);
            this.TabPage3.Controls.Add(this.PszJelölő);
            this.TabPage3.Controls.Add(this.Mindentkijelöl);
            this.TabPage3.Controls.Add(this.Kijelöléstörlése);
            this.TabPage3.Controls.Add(this.Command3);
            this.TabPage3.Controls.Add(this.Panel6);
            this.TabPage3.Controls.Add(this.Panel3);
            this.TabPage3.Location = new System.Drawing.Point(4, 29);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(1136, 446);
            this.TabPage3.TabIndex = 6;
            this.TabPage3.Text = "Előtervező";
            // 
            // Panel7
            // 
            this.Panel7.BackColor = System.Drawing.Color.Tomato;
            this.Panel7.Controls.Add(this.Kerékcsökkenés);
            this.Panel7.Controls.Add(this.Label39);
            this.Panel7.Location = new System.Drawing.Point(3, 372);
            this.Panel7.Name = "Panel7";
            this.Panel7.Size = new System.Drawing.Size(233, 53);
            this.Panel7.TabIndex = 182;
            // 
            // Kerékcsökkenés
            // 
            this.Kerékcsökkenés.Location = new System.Drawing.Point(136, 23);
            this.Kerékcsökkenés.Name = "Kerékcsökkenés";
            this.Kerékcsökkenés.Size = new System.Drawing.Size(95, 26);
            this.Kerékcsökkenés.TabIndex = 96;
            this.Kerékcsökkenés.Text = "0,5";
            // 
            // Label39
            // 
            this.Label39.AutoSize = true;
            this.Label39.BackColor = System.Drawing.Color.Transparent;
            this.Label39.Location = new System.Drawing.Point(0, 0);
            this.Label39.Name = "Label39";
            this.Label39.Size = new System.Drawing.Size(159, 20);
            this.Label39.TabIndex = 89;
            this.Label39.Text = "Havi kerékcsökkenés";
            // 
            // FőHoltart
            // 
            this.FőHoltart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FőHoltart.BackColor = System.Drawing.Color.Lime;
            this.FőHoltart.ForeColor = System.Drawing.Color.MediumBlue;
            this.FőHoltart.Location = new System.Drawing.Point(7, 133);
            this.FőHoltart.Name = "FőHoltart";
            this.FőHoltart.Size = new System.Drawing.Size(1120, 20);
            this.FőHoltart.TabIndex = 172;
            this.FőHoltart.Visible = false;
            // 
            // AlHoltart
            // 
            this.AlHoltart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AlHoltart.BackColor = System.Drawing.Color.Lime;
            this.AlHoltart.ForeColor = System.Drawing.Color.MediumBlue;
            this.AlHoltart.Location = new System.Drawing.Point(7, 176);
            this.AlHoltart.Name = "AlHoltart";
            this.AlHoltart.Size = new System.Drawing.Size(1121, 20);
            this.AlHoltart.TabIndex = 173;
            this.AlHoltart.Visible = false;
            // 
            // Command1
            // 
            this.Command1.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Command1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command1.Location = new System.Drawing.Point(639, 5);
            this.Command1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Command1.Name = "Command1";
            this.Command1.Size = new System.Drawing.Size(40, 40);
            this.Command1.TabIndex = 177;
            this.ToolTip1.SetToolTip(this.Command1, "Kiválasztott feltételekkel Excel tervezet készítés");
            this.Command1.UseVisualStyleBackColor = true;
            this.Command1.Click += new System.EventHandler(this.Command1_Click);
            // 
            // Panel5
            // 
            this.Panel5.BackColor = System.Drawing.Color.Tomato;
            this.Panel5.Controls.Add(this.Text2);
            this.Panel5.Controls.Add(this.Label38);
            this.Panel5.Location = new System.Drawing.Point(3, 313);
            this.Panel5.Name = "Panel5";
            this.Panel5.Size = new System.Drawing.Size(233, 53);
            this.Panel5.TabIndex = 176;
            // 
            // Text2
            // 
            this.Text2.Location = new System.Drawing.Point(136, 23);
            this.Text2.Name = "Text2";
            this.Text2.Size = new System.Drawing.Size(95, 26);
            this.Text2.TabIndex = 96;
            this.Text2.Text = "24";
            this.Text2.Leave += new System.EventHandler(this.Text2_Leave);
            // 
            // Label38
            // 
            this.Label38.AutoSize = true;
            this.Label38.BackColor = System.Drawing.Color.Transparent;
            this.Label38.Location = new System.Drawing.Point(0, 0);
            this.Label38.Name = "Label38";
            this.Label38.Size = new System.Drawing.Size(199, 20);
            this.Label38.TabIndex = 89;
            this.Label38.Text = "Vizsgált időszak hónapban";
            // 
            // Panel4
            // 
            this.Panel4.BackColor = System.Drawing.Color.Tomato;
            this.Panel4.Controls.Add(this.Option12);
            this.Panel4.Controls.Add(this.Option11);
            this.Panel4.Controls.Add(this.Option10);
            this.Panel4.Controls.Add(this.Label37);
            this.Panel4.Location = new System.Drawing.Point(242, 4);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(233, 122);
            this.Panel4.TabIndex = 176;
            // 
            // Option12
            // 
            this.Option12.AutoSize = true;
            this.Option12.Checked = true;
            this.Option12.Location = new System.Drawing.Point(13, 83);
            this.Option12.Name = "Option12";
            this.Option12.Size = new System.Drawing.Size(207, 24);
            this.Option12.TabIndex = 93;
            this.Option12.TabStop = true;
            this.Option12.Text = "Felső határ átlépése előtt";
            this.Option12.UseVisualStyleBackColor = true;
            // 
            // Option11
            // 
            this.Option11.AutoSize = true;
            this.Option11.Location = new System.Drawing.Point(13, 53);
            this.Option11.Name = "Option11";
            this.Option11.Size = new System.Drawing.Size(200, 24);
            this.Option11.TabIndex = 92;
            this.Option11.Text = "Névleges érték átlépésig";
            this.Option11.UseVisualStyleBackColor = true;
            // 
            // Option10
            // 
            this.Option10.AutoSize = true;
            this.Option10.Location = new System.Drawing.Point(13, 23);
            this.Option10.Name = "Option10";
            this.Option10.Size = new System.Drawing.Size(167, 24);
            this.Option10.TabIndex = 91;
            this.Option10.Text = "Alsó határ átlépésig";
            this.Option10.UseVisualStyleBackColor = true;
            // 
            // Label37
            // 
            this.Label37.AutoSize = true;
            this.Label37.BackColor = System.Drawing.Color.Transparent;
            this.Label37.Location = new System.Drawing.Point(0, 0);
            this.Label37.Name = "Label37";
            this.Label37.Size = new System.Drawing.Size(124, 20);
            this.Label37.TabIndex = 89;
            this.Label37.Text = "Futatási szabály";
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.Tomato;
            this.Panel1.Controls.Add(this.Text1);
            this.Panel1.Controls.Add(this.Option8);
            this.Panel1.Controls.Add(this.Option9);
            this.Panel1.Controls.Add(this.Option7);
            this.Panel1.Controls.Add(this.Option6);
            this.Panel1.Controls.Add(this.Option5);
            this.Panel1.Controls.Add(this.Label36);
            this.Panel1.Location = new System.Drawing.Point(3, 133);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(233, 174);
            this.Panel1.TabIndex = 175;
            // 
            // Text1
            // 
            this.Text1.Location = new System.Drawing.Point(134, 141);
            this.Text1.Name = "Text1";
            this.Text1.Size = new System.Drawing.Size(95, 26);
            this.Text1.TabIndex = 95;
            this.Text1.Text = "5000";
            this.Text1.Leave += new System.EventHandler(this.Text1_Leave);
            // 
            // Option8
            // 
            this.Option8.AutoSize = true;
            this.Option8.Checked = true;
            this.Option8.Location = new System.Drawing.Point(3, 146);
            this.Option8.Name = "Option8";
            this.Option8.Size = new System.Drawing.Size(69, 24);
            this.Option8.TabIndex = 94;
            this.Option8.TabStop = true;
            this.Option8.Text = "Érték:";
            this.Option8.UseVisualStyleBackColor = true;
            // 
            // Option9
            // 
            this.Option9.AutoSize = true;
            this.Option9.Location = new System.Drawing.Point(4, 116);
            this.Option9.Name = "Option9";
            this.Option9.Size = new System.Drawing.Size(137, 24);
            this.Option9.TabIndex = 93;
            this.Option9.Text = "Kijelöltek átlaga";
            this.Option9.UseVisualStyleBackColor = true;
            this.Option9.Click += new System.EventHandler(this.Option9_Click);
            // 
            // Option7
            // 
            this.Option7.AutoSize = true;
            this.Option7.Location = new System.Drawing.Point(4, 86);
            this.Option7.Name = "Option7";
            this.Option7.Size = new System.Drawing.Size(104, 24);
            this.Option7.TabIndex = 92;
            this.Option7.Text = "Típus átlag";
            this.Option7.UseVisualStyleBackColor = true;
            this.Option7.Click += new System.EventHandler(this.Option7_Click);
            // 
            // Option6
            // 
            this.Option6.AutoSize = true;
            this.Option6.Location = new System.Drawing.Point(4, 56);
            this.Option6.Name = "Option6";
            this.Option6.Size = new System.Drawing.Size(133, 24);
            this.Option6.TabIndex = 91;
            this.Option6.Text = "Telephely átlag";
            this.Option6.UseVisualStyleBackColor = true;
            this.Option6.Click += new System.EventHandler(this.Option6_Click);
            // 
            // Option5
            // 
            this.Option5.AutoSize = true;
            this.Option5.Location = new System.Drawing.Point(4, 26);
            this.Option5.Name = "Option5";
            this.Option5.Size = new System.Drawing.Size(122, 24);
            this.Option5.TabIndex = 90;
            this.Option5.Text = "Kocsi havi km";
            this.Option5.UseVisualStyleBackColor = true;
            this.Option5.Click += new System.EventHandler(this.Option5_Click);
            // 
            // Label36
            // 
            this.Label36.AutoSize = true;
            this.Label36.BackColor = System.Drawing.Color.Transparent;
            this.Label36.Location = new System.Drawing.Point(0, 0);
            this.Label36.Name = "Label36";
            this.Label36.Size = new System.Drawing.Size(119, 20);
            this.Label36.TabIndex = 89;
            this.Label36.Text = "Havi km beállító";
            // 
            // PszJelölő
            // 
            this.PszJelölő.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PszJelölő.CheckOnClick = true;
            this.PszJelölő.FormattingEnabled = true;
            this.PszJelölő.Location = new System.Drawing.Point(481, 4);
            this.PszJelölő.Name = "PszJelölő";
            this.PszJelölő.Size = new System.Drawing.Size(103, 403);
            this.PszJelölő.TabIndex = 174;
            // 
            // Mindentkijelöl
            // 
            this.Mindentkijelöl.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.Mindentkijelöl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Mindentkijelöl.Location = new System.Drawing.Point(591, 5);
            this.Mindentkijelöl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Mindentkijelöl.Name = "Mindentkijelöl";
            this.Mindentkijelöl.Size = new System.Drawing.Size(40, 40);
            this.Mindentkijelöl.TabIndex = 169;
            this.ToolTip1.SetToolTip(this.Mindentkijelöl, "Minden kijelölése");
            this.Mindentkijelöl.UseVisualStyleBackColor = true;
            this.Mindentkijelöl.Click += new System.EventHandler(this.Mindentkijelöl_Click);
            // 
            // Kijelöléstörlése
            // 
            this.Kijelöléstörlése.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.Kijelöléstörlése.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Kijelöléstörlése.Location = new System.Drawing.Point(591, 55);
            this.Kijelöléstörlése.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Kijelöléstörlése.Name = "Kijelöléstörlése";
            this.Kijelöléstörlése.Size = new System.Drawing.Size(40, 40);
            this.Kijelöléstörlése.TabIndex = 170;
            this.ToolTip1.SetToolTip(this.Kijelöléstörlése, "Kijelölések megszűntetése");
            this.Kijelöléstörlése.UseVisualStyleBackColor = true;
            this.Kijelöléstörlése.Click += new System.EventHandler(this.Kijelöléstörlése_Click);
            // 
            // Command3
            // 
            this.Command3.BackgroundImage = global::Villamos.Properties.Resources.Aha_Soft_Large_Seo_SEO;
            this.Command3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command3.Location = new System.Drawing.Point(687, 5);
            this.Command3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Command3.Name = "Command3";
            this.Command3.Size = new System.Drawing.Size(40, 40);
            this.Command3.TabIndex = 171;
            this.ToolTip1.SetToolTip(this.Command3, "Kimutatásos Excel táblázatot hoz létre az eddigi összes vizsgálatról");
            this.Command3.UseVisualStyleBackColor = true;
            this.Command3.Click += new System.EventHandler(this.Command3_Click);
            // 
            // Panel6
            // 
            this.Panel6.BackColor = System.Drawing.Color.Tomato;
            this.Panel6.Controls.Add(this.Check1);
            this.Panel6.Location = new System.Drawing.Point(3, 73);
            this.Panel6.Name = "Panel6";
            this.Panel6.Size = new System.Drawing.Size(233, 53);
            this.Panel6.TabIndex = 4;
            // 
            // Check1
            // 
            this.Check1.AutoSize = true;
            this.Check1.Location = new System.Drawing.Point(18, 15);
            this.Check1.Name = "Check1";
            this.Check1.Size = new System.Drawing.Size(180, 24);
            this.Check1.TabIndex = 0;
            this.Check1.Text = "Előző futatás marad?";
            this.Check1.UseVisualStyleBackColor = true;
            // 
            // Panel3
            // 
            this.Panel3.BackColor = System.Drawing.Color.Tomato;
            this.Panel3.Controls.Add(this.Command2);
            this.Panel3.Controls.Add(this.Telephely);
            this.Panel3.Controls.Add(this.Label16);
            this.Panel3.Location = new System.Drawing.Point(3, 3);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(233, 64);
            this.Panel3.TabIndex = 1;
            // 
            // Command2
            // 
            this.Command2.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Command2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command2.Location = new System.Drawing.Point(190, 14);
            this.Command2.Name = "Command2";
            this.Command2.Size = new System.Drawing.Size(40, 40);
            this.Command2.TabIndex = 1;
            this.ToolTip1.SetToolTip(this.Command2, "Lista frissítése");
            this.Command2.UseVisualStyleBackColor = true;
            this.Command2.Click += new System.EventHandler(this.Command2_Click);
            // 
            // Telephely
            // 
            this.Telephely.FormattingEnabled = true;
            this.Telephely.Location = new System.Drawing.Point(4, 26);
            this.Telephely.Name = "Telephely";
            this.Telephely.Size = new System.Drawing.Size(151, 28);
            this.Telephely.TabIndex = 0;
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.BackColor = System.Drawing.Color.Transparent;
            this.Label16.Location = new System.Drawing.Point(0, 0);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(76, 20);
            this.Label16.TabIndex = 88;
            this.Label16.Text = "Telephely";
            // 
            // TabPage6
            // 
            this.TabPage6.BackColor = System.Drawing.Color.ForestGreen;
            this.TabPage6.Controls.Add(this.VizsAdat_Excel);
            this.TabPage6.Controls.Add(this.Tábla1);
            this.TabPage6.Controls.Add(this.VizsAdat_Frissít);
            this.TabPage6.Location = new System.Drawing.Point(4, 29);
            this.TabPage6.Name = "TabPage6";
            this.TabPage6.Size = new System.Drawing.Size(1136, 446);
            this.TabPage6.TabIndex = 5;
            this.TabPage6.Text = "Vizsgálati adatok";
            // 
            // VizsAdat_Excel
            // 
            this.VizsAdat_Excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.VizsAdat_Excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VizsAdat_Excel.Location = new System.Drawing.Point(56, 11);
            this.VizsAdat_Excel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.VizsAdat_Excel.Name = "VizsAdat_Excel";
            this.VizsAdat_Excel.Size = new System.Drawing.Size(45, 45);
            this.VizsAdat_Excel.TabIndex = 175;
            this.ToolTip1.SetToolTip(this.VizsAdat_Excel, "Állománytábla mentése Excelbe");
            this.VizsAdat_Excel.UseVisualStyleBackColor = true;
            this.VizsAdat_Excel.Click += new System.EventHandler(this.VizsAdat_Excel_Click);
            // 
            // Tábla1
            // 
            this.Tábla1.AllowUserToAddRows = false;
            this.Tábla1.AllowUserToDeleteRows = false;
            this.Tábla1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla1.Location = new System.Drawing.Point(5, 62);
            this.Tábla1.Name = "Tábla1";
            this.Tábla1.RowHeadersVisible = false;
            this.Tábla1.Size = new System.Drawing.Size(1128, 381);
            this.Tábla1.TabIndex = 0;
            this.Tábla1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla1_CellClick);
            // 
            // VizsAdat_Frissít
            // 
            this.VizsAdat_Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.VizsAdat_Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VizsAdat_Frissít.Location = new System.Drawing.Point(5, 11);
            this.VizsAdat_Frissít.Name = "VizsAdat_Frissít";
            this.VizsAdat_Frissít.Size = new System.Drawing.Size(45, 45);
            this.VizsAdat_Frissít.TabIndex = 174;
            this.ToolTip1.SetToolTip(this.VizsAdat_Frissít, "Adatok frissítése");
            this.VizsAdat_Frissít.UseVisualStyleBackColor = true;
            this.VizsAdat_Frissít.Click += new System.EventHandler(this.VizsAdat_Frissít_Click);
            // 
            // TabPage5
            // 
            this.TabPage5.BackColor = System.Drawing.Color.DarkOrange;
            this.TabPage5.Controls.Add(this.Töröl);
            this.TabPage5.Controls.Add(this.KövV2km);
            this.TabPage5.Controls.Add(this.KövV2_Sorszám);
            this.TabPage5.Controls.Add(this.KövV_Sorszám);
            this.TabPage5.Controls.Add(this.KövV2_számláló);
            this.TabPage5.Controls.Add(this.KövV2);
            this.TabPage5.Controls.Add(this.KövV1km);
            this.TabPage5.Controls.Add(this.KövV);
            this.TabPage5.Controls.Add(this.Sorszám);
            this.TabPage5.Controls.Add(this.Jjavszám);
            this.TabPage5.Controls.Add(this.KMUkm);
            this.TabPage5.Controls.Add(this.VizsgKm);
            this.TabPage5.Controls.Add(this.Vizsgfok);
            this.TabPage5.Controls.Add(this.HaviKm);
            this.TabPage5.Controls.Add(this.TEljesKmText);
            this.TabPage5.Controls.Add(this.Label35);
            this.TabPage5.Controls.Add(this.Label34);
            this.TabPage5.Controls.Add(this.CiklusrendCombo);
            this.TabPage5.Controls.Add(this.Üzemek);
            this.TabPage5.Controls.Add(this.Vizsgsorszám);
            this.TabPage5.Controls.Add(this.KMUdátum);
            this.TabPage5.Controls.Add(this.Utolsófelújításdátuma);
            this.TabPage5.Controls.Add(this.Vizsgdátumk);
            this.TabPage5.Controls.Add(this.Vizsgdátumv);
            this.TabPage5.Controls.Add(this.SAP_adatok);
            this.TabPage5.Controls.Add(this.Új_adat);
            this.TabPage5.Controls.Add(this.Utolsó_V_rögzítés);
            this.TabPage5.Controls.Add(this.Label33);
            this.TabPage5.Controls.Add(this.Label32);
            this.TabPage5.Controls.Add(this.Label31);
            this.TabPage5.Controls.Add(this.Label30);
            this.TabPage5.Controls.Add(this.Label29);
            this.TabPage5.Controls.Add(this.Label28);
            this.TabPage5.Controls.Add(this.Label27);
            this.TabPage5.Controls.Add(this.Label26);
            this.TabPage5.Controls.Add(this.Label25);
            this.TabPage5.Controls.Add(this.Label24);
            this.TabPage5.Controls.Add(this.Label23);
            this.TabPage5.Controls.Add(this.Label22);
            this.TabPage5.Controls.Add(this.Label21);
            this.TabPage5.Controls.Add(this.Label20);
            this.TabPage5.Controls.Add(this.Label19);
            this.TabPage5.Controls.Add(this.Label18);
            this.TabPage5.Controls.Add(this.Label17);
            this.TabPage5.Location = new System.Drawing.Point(4, 29);
            this.TabPage5.Name = "TabPage5";
            this.TabPage5.Size = new System.Drawing.Size(1136, 446);
            this.TabPage5.TabIndex = 4;
            this.TabPage5.Text = "Utolsó Vizsgálati adatok";
            // 
            // Töröl
            // 
            this.Töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Töröl.Location = new System.Drawing.Point(1055, 325);
            this.Töröl.Name = "Töröl";
            this.Töröl.Size = new System.Drawing.Size(45, 45);
            this.Töröl.TabIndex = 12;
            this.ToolTip1.SetToolTip(this.Töröl, "Adatok törlése");
            this.Töröl.UseVisualStyleBackColor = true;
            this.Töröl.Click += new System.EventHandler(this.Töröl_Click);
            // 
            // KövV2km
            // 
            this.KövV2km.Location = new System.Drawing.Point(670, 360);
            this.KövV2km.Name = "KövV2km";
            this.KövV2km.Size = new System.Drawing.Size(136, 26);
            this.KövV2km.TabIndex = 90;
            // 
            // KövV2_Sorszám
            // 
            this.KövV2_Sorszám.Location = new System.Drawing.Point(812, 290);
            this.KövV2_Sorszám.Name = "KövV2_Sorszám";
            this.KövV2_Sorszám.Size = new System.Drawing.Size(136, 26);
            this.KövV2_Sorszám.TabIndex = 88;
            // 
            // KövV_Sorszám
            // 
            this.KövV_Sorszám.Location = new System.Drawing.Point(811, 220);
            this.KövV_Sorszám.Name = "KövV_Sorszám";
            this.KövV_Sorszám.Size = new System.Drawing.Size(136, 26);
            this.KövV_Sorszám.TabIndex = 87;
            // 
            // KövV2_számláló
            // 
            this.KövV2_számláló.Location = new System.Drawing.Point(670, 325);
            this.KövV2_számláló.Name = "KövV2_számláló";
            this.KövV2_számláló.Size = new System.Drawing.Size(136, 26);
            this.KövV2_számláló.TabIndex = 8;
            // 
            // KövV2
            // 
            this.KövV2.Location = new System.Drawing.Point(670, 290);
            this.KövV2.Name = "KövV2";
            this.KövV2.Size = new System.Drawing.Size(136, 26);
            this.KövV2.TabIndex = 85;
            // 
            // KövV1km
            // 
            this.KövV1km.Location = new System.Drawing.Point(670, 255);
            this.KövV1km.Name = "KövV1km";
            this.KövV1km.Size = new System.Drawing.Size(136, 26);
            this.KövV1km.TabIndex = 84;
            // 
            // KövV
            // 
            this.KövV.Location = new System.Drawing.Point(670, 220);
            this.KövV.Name = "KövV";
            this.KövV.Size = new System.Drawing.Size(136, 26);
            this.KövV.TabIndex = 83;
            // 
            // Sorszám
            // 
            this.Sorszám.Enabled = false;
            this.Sorszám.Location = new System.Drawing.Point(230, 10);
            this.Sorszám.Name = "Sorszám";
            this.Sorszám.Size = new System.Drawing.Size(136, 26);
            this.Sorszám.TabIndex = 0;
            // 
            // Jjavszám
            // 
            this.Jjavszám.Location = new System.Drawing.Point(230, 360);
            this.Jjavszám.Name = "Jjavszám";
            this.Jjavszám.Size = new System.Drawing.Size(136, 26);
            this.Jjavszám.TabIndex = 8;
            // 
            // KMUkm
            // 
            this.KMUkm.Location = new System.Drawing.Point(230, 325);
            this.KMUkm.Name = "KMUkm";
            this.KMUkm.Size = new System.Drawing.Size(136, 26);
            this.KMUkm.TabIndex = 7;
            // 
            // VizsgKm
            // 
            this.VizsgKm.Location = new System.Drawing.Point(230, 185);
            this.VizsgKm.Name = "VizsgKm";
            this.VizsgKm.Size = new System.Drawing.Size(136, 26);
            this.VizsgKm.TabIndex = 5;
            // 
            // Vizsgfok
            // 
            this.Vizsgfok.Location = new System.Drawing.Point(230, 42);
            this.Vizsgfok.MaxLength = 10;
            this.Vizsgfok.Name = "Vizsgfok";
            this.Vizsgfok.Size = new System.Drawing.Size(136, 26);
            this.Vizsgfok.TabIndex = 2;
            // 
            // HaviKm
            // 
            this.HaviKm.Location = new System.Drawing.Point(670, 115);
            this.HaviKm.Name = "HaviKm";
            this.HaviKm.Size = new System.Drawing.Size(136, 26);
            this.HaviKm.TabIndex = 12;
            // 
            // TEljesKmText
            // 
            this.TEljesKmText.Location = new System.Drawing.Point(670, 10);
            this.TEljesKmText.Name = "TEljesKmText";
            this.TEljesKmText.Size = new System.Drawing.Size(136, 26);
            this.TEljesKmText.TabIndex = 10;
            // 
            // Label35
            // 
            this.Label35.AutoSize = true;
            this.Label35.BackColor = System.Drawing.Color.OrangeRed;
            this.Label35.Location = new System.Drawing.Point(420, 360);
            this.Label35.Name = "Label35";
            this.Label35.Size = new System.Drawing.Size(147, 20);
            this.Label35.TabIndex = 89;
            this.Label35.Text = "V2-V3-tól futott km:";
            // 
            // Label34
            // 
            this.Label34.AutoSize = true;
            this.Label34.BackColor = System.Drawing.Color.Silver;
            this.Label34.Location = new System.Drawing.Point(10, 10);
            this.Label34.Name = "Label34";
            this.Label34.Size = new System.Drawing.Size(76, 20);
            this.Label34.TabIndex = 81;
            this.Label34.Text = "Sorszám:";
            // 
            // CiklusrendCombo
            // 
            this.CiklusrendCombo.FormattingEnabled = true;
            this.CiklusrendCombo.Location = new System.Drawing.Point(670, 45);
            this.CiklusrendCombo.Name = "CiklusrendCombo";
            this.CiklusrendCombo.Size = new System.Drawing.Size(136, 28);
            this.CiklusrendCombo.TabIndex = 11;
            this.CiklusrendCombo.SelectedIndexChanged += new System.EventHandler(this.CiklusrendCombo_SelectedIndexChanged);
            // 
            // Üzemek
            // 
            this.Üzemek.FormattingEnabled = true;
            this.Üzemek.Location = new System.Drawing.Point(230, 220);
            this.Üzemek.Name = "Üzemek";
            this.Üzemek.Size = new System.Drawing.Size(136, 28);
            this.Üzemek.TabIndex = 6;
            // 
            // Vizsgsorszám
            // 
            this.Vizsgsorszám.FormattingEnabled = true;
            this.Vizsgsorszám.Location = new System.Drawing.Point(230, 77);
            this.Vizsgsorszám.Name = "Vizsgsorszám";
            this.Vizsgsorszám.Size = new System.Drawing.Size(136, 28);
            this.Vizsgsorszám.TabIndex = 1;
            this.Vizsgsorszám.SelectedIndexChanged += new System.EventHandler(this.Vizsgsorszám_SelectedIndexChanged);
            // 
            // KMUdátum
            // 
            this.KMUdátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.KMUdátum.Location = new System.Drawing.Point(670, 150);
            this.KMUdátum.Name = "KMUdátum";
            this.KMUdátum.Size = new System.Drawing.Size(118, 26);
            this.KMUdátum.TabIndex = 13;
            // 
            // Utolsófelújításdátuma
            // 
            this.Utolsófelújításdátuma.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Utolsófelújításdátuma.Location = new System.Drawing.Point(230, 395);
            this.Utolsófelújításdátuma.Name = "Utolsófelújításdátuma";
            this.Utolsófelújításdátuma.Size = new System.Drawing.Size(118, 26);
            this.Utolsófelújításdátuma.TabIndex = 9;
            // 
            // Vizsgdátumk
            // 
            this.Vizsgdátumk.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Vizsgdátumk.Location = new System.Drawing.Point(230, 115);
            this.Vizsgdátumk.Name = "Vizsgdátumk";
            this.Vizsgdátumk.Size = new System.Drawing.Size(118, 26);
            this.Vizsgdátumk.TabIndex = 3;
            // 
            // Vizsgdátumv
            // 
            this.Vizsgdátumv.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Vizsgdátumv.Location = new System.Drawing.Point(230, 150);
            this.Vizsgdátumv.Name = "Vizsgdátumv";
            this.Vizsgdátumv.Size = new System.Drawing.Size(118, 26);
            this.Vizsgdátumv.TabIndex = 4;
            // 
            // SAP_adatok
            // 
            this.SAP_adatok.BackgroundImage = global::Villamos.Properties.Resources.SAP;
            this.SAP_adatok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SAP_adatok.Location = new System.Drawing.Point(1050, 173);
            this.SAP_adatok.Name = "SAP_adatok";
            this.SAP_adatok.Size = new System.Drawing.Size(50, 50);
            this.SAP_adatok.TabIndex = 11;
            this.ToolTip1.SetToolTip(this.SAP_adatok, "Adatok betöltése SAP lekérdezésből");
            this.SAP_adatok.UseVisualStyleBackColor = true;
            this.SAP_adatok.Click += new System.EventHandler(this.SAP_adatok_Click);
            // 
            // Új_adat
            // 
            this.Új_adat.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Új_adat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Új_adat.Location = new System.Drawing.Point(1050, 115);
            this.Új_adat.Name = "Új_adat";
            this.Új_adat.Size = new System.Drawing.Size(50, 50);
            this.Új_adat.TabIndex = 10;
            this.ToolTip1.SetToolTip(this.Új_adat, "A következő tervezett vizsgálat adatait betölti");
            this.Új_adat.UseVisualStyleBackColor = true;
            this.Új_adat.Click += new System.EventHandler(this.Új_adat_Click);
            // 
            // Utolsó_V_rögzítés
            // 
            this.Utolsó_V_rögzítés.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Utolsó_V_rögzítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Utolsó_V_rögzítés.Location = new System.Drawing.Point(1050, 10);
            this.Utolsó_V_rögzítés.Name = "Utolsó_V_rögzítés";
            this.Utolsó_V_rögzítés.Size = new System.Drawing.Size(50, 50);
            this.Utolsó_V_rögzítés.TabIndex = 9;
            this.ToolTip1.SetToolTip(this.Utolsó_V_rögzítés, "Adatok rögzítése");
            this.Utolsó_V_rögzítés.UseVisualStyleBackColor = true;
            this.Utolsó_V_rögzítés.Click += new System.EventHandler(this.Utolsó_V_rögzítés_Click);
            // 
            // Label33
            // 
            this.Label33.AutoSize = true;
            this.Label33.BackColor = System.Drawing.Color.OrangeRed;
            this.Label33.Location = new System.Drawing.Point(420, 220);
            this.Label33.Name = "Label33";
            this.Label33.Size = new System.Drawing.Size(102, 20);
            this.Label33.TabIndex = 16;
            this.Label33.Text = "Következő V:";
            // 
            // Label32
            // 
            this.Label32.AutoSize = true;
            this.Label32.BackColor = System.Drawing.Color.OrangeRed;
            this.Label32.Location = new System.Drawing.Point(420, 290);
            this.Label32.Name = "Label32";
            this.Label32.Size = new System.Drawing.Size(132, 20);
            this.Label32.TabIndex = 15;
            this.Label32.Text = "Következő V2-V3";
            // 
            // Label31
            // 
            this.Label31.AutoSize = true;
            this.Label31.BackColor = System.Drawing.Color.OrangeRed;
            this.Label31.Location = new System.Drawing.Point(420, 255);
            this.Label31.Name = "Label31";
            this.Label31.Size = new System.Drawing.Size(163, 20);
            this.Label31.TabIndex = 14;
            this.Label31.Text = "Utolsó V-től futott km:";
            // 
            // Label30
            // 
            this.Label30.AutoSize = true;
            this.Label30.BackColor = System.Drawing.Color.OrangeRed;
            this.Label30.Location = new System.Drawing.Point(420, 325);
            this.Label30.Name = "Label30";
            this.Label30.Size = new System.Drawing.Size(160, 20);
            this.Label30.TabIndex = 13;
            this.Label30.Text = "V2-V3 számláló állás:";
            // 
            // Label29
            // 
            this.Label29.AutoSize = true;
            this.Label29.BackColor = System.Drawing.Color.Orange;
            this.Label29.Location = new System.Drawing.Point(420, 150);
            this.Label29.Name = "Label29";
            this.Label29.Size = new System.Drawing.Size(174, 20);
            this.Label29.TabIndex = 12;
            this.Label29.Text = "Adatok utolsófrissítése:";
            // 
            // Label28
            // 
            this.Label28.AutoSize = true;
            this.Label28.BackColor = System.Drawing.Color.Orange;
            this.Label28.Location = new System.Drawing.Point(420, 115);
            this.Label28.Name = "Label28";
            this.Label28.Size = new System.Drawing.Size(167, 20);
            this.Label28.TabIndex = 11;
            this.Label28.Text = "Havi futásteljesítmény:";
            // 
            // Label27
            // 
            this.Label27.AutoSize = true;
            this.Label27.BackColor = System.Drawing.Color.DarkKhaki;
            this.Label27.Location = new System.Drawing.Point(420, 45);
            this.Label27.Name = "Label27";
            this.Label27.Size = new System.Drawing.Size(133, 20);
            this.Label27.TabIndex = 10;
            this.Label27.Text = "Ütemezés típusa:";
            // 
            // Label26
            // 
            this.Label26.AutoSize = true;
            this.Label26.BackColor = System.Drawing.Color.DarkKhaki;
            this.Label26.Location = new System.Drawing.Point(420, 10);
            this.Label26.Name = "Label26";
            this.Label26.Size = new System.Drawing.Size(229, 20);
            this.Label26.TabIndex = 9;
            this.Label26.Text = "Üzembehelyezés óta futott km:";
            // 
            // Label25
            // 
            this.Label25.AutoSize = true;
            this.Label25.BackColor = System.Drawing.Color.Salmon;
            this.Label25.Location = new System.Drawing.Point(10, 395);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(180, 20);
            this.Label25.TabIndex = 8;
            this.Label25.Text = "Utolsó Felújítás dátuma:";
            // 
            // Label24
            // 
            this.Label24.AutoSize = true;
            this.Label24.BackColor = System.Drawing.Color.Salmon;
            this.Label24.Location = new System.Drawing.Point(10, 360);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(145, 20);
            this.Label24.TabIndex = 7;
            this.Label24.Text = "Felújítás sorszáma:";
            // 
            // Label23
            // 
            this.Label23.AutoSize = true;
            this.Label23.BackColor = System.Drawing.Color.Salmon;
            this.Label23.Location = new System.Drawing.Point(10, 325);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(211, 20);
            this.Label23.TabIndex = 6;
            this.Label23.Text = "Utolsó felújítás óta futott km:";
            // 
            // Label22
            // 
            this.Label22.AutoSize = true;
            this.Label22.BackColor = System.Drawing.Color.Silver;
            this.Label22.Location = new System.Drawing.Point(10, 45);
            this.Label22.Name = "Label22";
            this.Label22.Size = new System.Drawing.Size(113, 20);
            this.Label22.TabIndex = 5;
            this.Label22.Text = "Vizsgálat foka:";
            // 
            // Label21
            // 
            this.Label21.AutoSize = true;
            this.Label21.BackColor = System.Drawing.Color.Silver;
            this.Label21.Location = new System.Drawing.Point(10, 80);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(151, 20);
            this.Label21.TabIndex = 4;
            this.Label21.Text = "Vizsgálat sorszáma:";
            // 
            // Label20
            // 
            this.Label20.AutoSize = true;
            this.Label20.BackColor = System.Drawing.Color.Silver;
            this.Label20.Location = new System.Drawing.Point(10, 115);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(174, 20);
            this.Label20.TabIndex = 3;
            this.Label20.Text = "Vizsgálat kezdő dátum:";
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.BackColor = System.Drawing.Color.Silver;
            this.Label19.Location = new System.Drawing.Point(10, 150);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(173, 20);
            this.Label19.TabIndex = 2;
            this.Label19.Text = "Vizsgálat végző dátum:";
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.BackColor = System.Drawing.Color.Silver;
            this.Label18.Location = new System.Drawing.Point(10, 185);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(214, 20);
            this.Label18.TabIndex = 1;
            this.Label18.Text = "Vizsgálat km számláló állása:";
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.BackColor = System.Drawing.Color.Silver;
            this.Label17.Location = new System.Drawing.Point(10, 220);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(205, 20);
            this.Label17.TabIndex = 0;
            this.Label17.Text = "Vizsgálatot végző telephely:";
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.YellowGreen;
            this.TabPage4.Controls.Add(this.Button1);
            this.TabPage4.Controls.Add(this.Teljes_adatbázis_excel);
            this.TabPage4.Controls.Add(this.Tábla_lekérdezés);
            this.TabPage4.Controls.Add(this.Excellekérdezés);
            this.TabPage4.Controls.Add(this.Lekérdezés_lekérdezés);
            this.TabPage4.Location = new System.Drawing.Point(4, 29);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Size = new System.Drawing.Size(1136, 446);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Lekérdezések";
            // 
            // Button1
            // 
            this.Button1.BackgroundImage = global::Villamos.Properties.Resources.App_dict;
            this.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button1.Location = new System.Drawing.Point(216, 4);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(45, 45);
            this.Button1.TabIndex = 170;
            this.ToolTip1.SetToolTip(this.Button1, "Adatok rendezett listázása");
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Teljes_adatbázis_excel
            // 
            this.Teljes_adatbázis_excel.BackgroundImage = global::Villamos.Properties.Resources.Device_zip;
            this.Teljes_adatbázis_excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Teljes_adatbázis_excel.Location = new System.Drawing.Point(165, 4);
            this.Teljes_adatbázis_excel.Name = "Teljes_adatbázis_excel";
            this.Teljes_adatbázis_excel.Size = new System.Drawing.Size(45, 45);
            this.Teljes_adatbázis_excel.TabIndex = 168;
            this.ToolTip1.SetToolTip(this.Teljes_adatbázis_excel, "Teljes adatbázis mentése Excelbe");
            this.Teljes_adatbázis_excel.UseVisualStyleBackColor = true;
            this.Teljes_adatbázis_excel.Click += new System.EventHandler(this.Teljes_adatbázis_excel_Click);
            // 
            // Tábla_lekérdezés
            // 
            this.Tábla_lekérdezés.AllowUserToAddRows = false;
            this.Tábla_lekérdezés.AllowUserToDeleteRows = false;
            this.Tábla_lekérdezés.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla_lekérdezés.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla_lekérdezés.Location = new System.Drawing.Point(5, 55);
            this.Tábla_lekérdezés.Name = "Tábla_lekérdezés";
            this.Tábla_lekérdezés.RowHeadersVisible = false;
            this.Tábla_lekérdezés.Size = new System.Drawing.Size(1128, 388);
            this.Tábla_lekérdezés.TabIndex = 167;
            // 
            // Excellekérdezés
            // 
            this.Excellekérdezés.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excellekérdezés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excellekérdezés.Location = new System.Drawing.Point(54, 3);
            this.Excellekérdezés.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Excellekérdezés.Name = "Excellekérdezés";
            this.Excellekérdezés.Size = new System.Drawing.Size(45, 45);
            this.Excellekérdezés.TabIndex = 166;
            this.ToolTip1.SetToolTip(this.Excellekérdezés, "Állománytábla mentése Excelbe");
            this.Excellekérdezés.UseVisualStyleBackColor = true;
            this.Excellekérdezés.Click += new System.EventHandler(this.Excellekérdezés_Click);
            // 
            // Lekérdezés_lekérdezés
            // 
            this.Lekérdezés_lekérdezés.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Lekérdezés_lekérdezés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lekérdezés_lekérdezés.Location = new System.Drawing.Point(3, 3);
            this.Lekérdezés_lekérdezés.Name = "Lekérdezés_lekérdezés";
            this.Lekérdezés_lekérdezés.Size = new System.Drawing.Size(45, 45);
            this.Lekérdezés_lekérdezés.TabIndex = 64;
            this.ToolTip1.SetToolTip(this.Lekérdezés_lekérdezés, "Utolsó vizsgálati adatok listázása");
            this.Lekérdezés_lekérdezés.UseVisualStyleBackColor = true;
            this.Lekérdezés_lekérdezés.Click += new System.EventHandler(this.Lekérdezés_lekérdezés_Click);
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.LimeGreen;
            this.TabPage1.Controls.Add(this.Button3);
            this.TabPage1.Controls.Add(this.E_rögzít);
            this.TabPage1.Controls.Add(this.Combo_E3);
            this.TabPage1.Controls.Add(this.Combo_E2);
            this.TabPage1.Controls.Add(this.Label11);
            this.TabPage1.Controls.Add(this.Label9);
            this.TabPage1.Controls.Add(this.Típus_text);
            this.TabPage1.Controls.Add(this.Státus_text);
            this.TabPage1.Controls.Add(this.Miótaáll_text);
            this.TabPage1.Controls.Add(this.Takarítás_text);
            this.TabPage1.Controls.Add(this.Főmérnökség_text);
            this.TabPage1.Controls.Add(this.Járműtípus_text);
            this.TabPage1.Controls.Add(this.Label8);
            this.TabPage1.Controls.Add(this.Label7);
            this.TabPage1.Controls.Add(this.Label6);
            this.TabPage1.Controls.Add(this.Label3);
            this.TabPage1.Controls.Add(this.Label2);
            this.TabPage1.Controls.Add(this.Label1);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1136, 446);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Alapadatok";
            // 
            // Button3
            // 
            this.Button3.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button3.Location = new System.Drawing.Point(489, 255);
            this.Button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(45, 45);
            this.Button3.TabIndex = 174;
            this.ToolTip1.SetToolTip(this.Button3, "E2 és E3 táblázat mentése Excelbe");
            this.Button3.UseVisualStyleBackColor = true;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // E_rögzít
            // 
            this.E_rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.E_rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.E_rögzít.Location = new System.Drawing.Point(380, 250);
            this.E_rögzít.Name = "E_rögzít";
            this.E_rögzít.Size = new System.Drawing.Size(50, 50);
            this.E_rögzít.TabIndex = 20;
            this.ToolTip1.SetToolTip(this.E_rögzít, "E2 és E3 ciklus rögzítése");
            this.E_rögzít.UseVisualStyleBackColor = true;
            this.E_rögzít.Click += new System.EventHandler(this.E_rögzít_Click);
            // 
            // Combo_E3
            // 
            this.Combo_E3.FormattingEnabled = true;
            this.Combo_E3.Location = new System.Drawing.Point(207, 272);
            this.Combo_E3.Name = "Combo_E3";
            this.Combo_E3.Size = new System.Drawing.Size(121, 28);
            this.Combo_E3.TabIndex = 19;
            // 
            // Combo_E2
            // 
            this.Combo_E2.FormattingEnabled = true;
            this.Combo_E2.Location = new System.Drawing.Point(207, 234);
            this.Combo_E2.Name = "Combo_E2";
            this.Combo_E2.Size = new System.Drawing.Size(121, 28);
            this.Combo_E2.TabIndex = 18;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.BackColor = System.Drawing.Color.LightGreen;
            this.Label11.Location = new System.Drawing.Point(17, 242);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(72, 20);
            this.Label11.TabIndex = 17;
            this.Label11.Text = "E2 napja";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.BackColor = System.Drawing.Color.LightGreen;
            this.Label9.Location = new System.Drawing.Point(17, 280);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(72, 20);
            this.Label9.TabIndex = 16;
            this.Label9.Text = "E3 napja";
            // 
            // Típus_text
            // 
            this.Típus_text.AutoSize = true;
            this.Típus_text.BackColor = System.Drawing.Color.LightGreen;
            this.Típus_text.Location = new System.Drawing.Point(203, 14);
            this.Típus_text.Name = "Típus_text";
            this.Típus_text.Size = new System.Drawing.Size(57, 20);
            this.Típus_text.TabIndex = 15;
            this.Típus_text.Text = "Label9";
            // 
            // Státus_text
            // 
            this.Státus_text.AutoSize = true;
            this.Státus_text.BackColor = System.Drawing.Color.LightGreen;
            this.Státus_text.Location = new System.Drawing.Point(203, 46);
            this.Státus_text.Name = "Státus_text";
            this.Státus_text.Size = new System.Drawing.Size(66, 20);
            this.Státus_text.TabIndex = 14;
            this.Státus_text.Text = "Label10";
            // 
            // Miótaáll_text
            // 
            this.Miótaáll_text.AutoSize = true;
            this.Miótaáll_text.BackColor = System.Drawing.Color.LightGreen;
            this.Miótaáll_text.Location = new System.Drawing.Point(203, 78);
            this.Miótaáll_text.Name = "Miótaáll_text";
            this.Miótaáll_text.Size = new System.Drawing.Size(66, 20);
            this.Miótaáll_text.TabIndex = 13;
            this.Miótaáll_text.Text = "Label11";
            // 
            // Takarítás_text
            // 
            this.Takarítás_text.AutoSize = true;
            this.Takarítás_text.BackColor = System.Drawing.Color.LightGreen;
            this.Takarítás_text.Location = new System.Drawing.Point(203, 107);
            this.Takarítás_text.Name = "Takarítás_text";
            this.Takarítás_text.Size = new System.Drawing.Size(66, 20);
            this.Takarítás_text.TabIndex = 10;
            this.Takarítás_text.Text = "Label15";
            // 
            // Főmérnökség_text
            // 
            this.Főmérnökség_text.AutoSize = true;
            this.Főmérnökség_text.BackColor = System.Drawing.Color.LightGreen;
            this.Főmérnökség_text.Location = new System.Drawing.Point(203, 139);
            this.Főmérnökség_text.Name = "Főmérnökség_text";
            this.Főmérnökség_text.Size = new System.Drawing.Size(66, 20);
            this.Főmérnökség_text.TabIndex = 9;
            this.Főmérnökség_text.Text = "Label16";
            // 
            // Járműtípus_text
            // 
            this.Járműtípus_text.AutoSize = true;
            this.Járműtípus_text.BackColor = System.Drawing.Color.LightGreen;
            this.Járműtípus_text.Location = new System.Drawing.Point(203, 171);
            this.Járműtípus_text.Name = "Járműtípus_text";
            this.Járműtípus_text.Size = new System.Drawing.Size(66, 20);
            this.Járműtípus_text.TabIndex = 8;
            this.Járműtípus_text.Text = "Label17";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.BackColor = System.Drawing.Color.LightGreen;
            this.Label8.Location = new System.Drawing.Point(17, 14);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(51, 20);
            this.Label8.TabIndex = 7;
            this.Label8.Text = "Típus:";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.BackColor = System.Drawing.Color.LightGreen;
            this.Label7.Location = new System.Drawing.Point(17, 46);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(60, 20);
            this.Label7.TabIndex = 6;
            this.Label7.Text = "Státus:";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.BackColor = System.Drawing.Color.LightGreen;
            this.Label6.Location = new System.Drawing.Point(17, 78);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(71, 20);
            this.Label6.TabIndex = 5;
            this.Label6.Text = "Mióta áll:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.LightGreen;
            this.Label3.Location = new System.Drawing.Point(17, 107);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(124, 20);
            this.Label3.TabIndex = 2;
            this.Label3.Text = "Utolsó takarítás:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.LightGreen;
            this.Label2.Location = new System.Drawing.Point(17, 139);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(152, 20);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "Főmérnökségi típus:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.LightGreen;
            this.Label1.Location = new System.Drawing.Point(17, 171);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(95, 20);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Jármű típus:";
            // 
            // Fülek
            // 
            this.Fülek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fülek.Controls.Add(this.TabPage1);
            this.Fülek.Controls.Add(this.TabPage4);
            this.Fülek.Controls.Add(this.TabPage5);
            this.Fülek.Controls.Add(this.TabPage6);
            this.Fülek.Controls.Add(this.TabPage3);
            this.Fülek.Controls.Add(this.TabPage2);
            this.Fülek.Location = new System.Drawing.Point(6, 55);
            this.Fülek.Name = "Fülek";
            this.Fülek.Padding = new System.Drawing.Point(16, 3);
            this.Fülek.SelectedIndex = 0;
            this.Fülek.Size = new System.Drawing.Size(1144, 479);
            this.Fülek.TabIndex = 171;
            this.Fülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Fülek_DrawItem);
            this.Fülek.SelectedIndexChanged += new System.EventHandler(this.Fülek_SelectedIndexChanged);
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.Coral;
            this.TabPage2.Controls.Add(this.Button4);
            this.TabPage2.Controls.Add(this.Btn_Vezénylésbeírás);
            this.TabPage2.Controls.Add(this.Tábla_vezénylés);
            this.TabPage2.Controls.Add(this.Dátum_ütem);
            this.TabPage2.Controls.Add(this.Ütem_frissít);
            this.TabPage2.Controls.Add(this.Tábla_ütemező);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(1136, 446);
            this.TabPage2.TabIndex = 7;
            this.TabPage2.Text = "Vizsgálat ütemező";
            // 
            // Button4
            // 
            this.Button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button4.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button4.Location = new System.Drawing.Point(806, 57);
            this.Button4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(40, 40);
            this.Button4.TabIndex = 176;
            this.ToolTip1.SetToolTip(this.Button4, "Excel táblázatot készít");
            this.Button4.UseVisualStyleBackColor = true;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // Btn_Vezénylésbeírás
            // 
            this.Btn_Vezénylésbeírás.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Vezénylésbeírás.BackgroundImage = global::Villamos.Properties.Resources.leadott;
            this.Btn_Vezénylésbeírás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Vezénylésbeírás.Location = new System.Drawing.Point(852, 57);
            this.Btn_Vezénylésbeírás.Name = "Btn_Vezénylésbeírás";
            this.Btn_Vezénylésbeírás.Size = new System.Drawing.Size(40, 40);
            this.Btn_Vezénylésbeírás.TabIndex = 175;
            this.ToolTip1.SetToolTip(this.Btn_Vezénylésbeírás, "A napi vezénylési adatokat rögzíti a hiba táblába");
            this.Btn_Vezénylésbeírás.UseVisualStyleBackColor = true;
            this.Btn_Vezénylésbeírás.Click += new System.EventHandler(this.Btn_Vezénylésbeírás_Click);
            // 
            // Tábla_vezénylés
            // 
            this.Tábla_vezénylés.AllowUserToAddRows = false;
            this.Tábla_vezénylés.AllowUserToDeleteRows = false;
            this.Tábla_vezénylés.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla_vezénylés.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla_vezénylés.Location = new System.Drawing.Point(806, 103);
            this.Tábla_vezénylés.Name = "Tábla_vezénylés";
            this.Tábla_vezénylés.RowHeadersVisible = false;
            this.Tábla_vezénylés.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Tábla_vezénylés.Size = new System.Drawing.Size(324, 337);
            this.Tábla_vezénylés.TabIndex = 171;
            this.Tábla_vezénylés.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_vezénylés_CellClick);
            // 
            // Dátum_ütem
            // 
            this.Dátum_ütem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Dátum_ütem.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum_ütem.Location = new System.Drawing.Point(857, 25);
            this.Dátum_ütem.Name = "Dátum_ütem";
            this.Dátum_ütem.Size = new System.Drawing.Size(118, 26);
            this.Dátum_ütem.TabIndex = 170;
            this.Dátum_ütem.ValueChanged += new System.EventHandler(this.Dátum_ütem_ValueChanged);
            // 
            // Ütem_frissít
            // 
            this.Ütem_frissít.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Ütem_frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Ütem_frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Ütem_frissít.Location = new System.Drawing.Point(806, 6);
            this.Ütem_frissít.Name = "Ütem_frissít";
            this.Ütem_frissít.Size = new System.Drawing.Size(45, 45);
            this.Ütem_frissít.TabIndex = 169;
            this.ToolTip1.SetToolTip(this.Ütem_frissít, "Frissíti az ütemezési táblázatot");
            this.Ütem_frissít.UseVisualStyleBackColor = true;
            this.Ütem_frissít.Click += new System.EventHandler(this.Ütem_frissít_Click);
            // 
            // Tábla_ütemező
            // 
            this.Tábla_ütemező.AllowUserToAddRows = false;
            this.Tábla_ütemező.AllowUserToDeleteRows = false;
            this.Tábla_ütemező.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla_ütemező.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla_ütemező.Location = new System.Drawing.Point(5, 6);
            this.Tábla_ütemező.Name = "Tábla_ütemező";
            this.Tábla_ütemező.RowHeadersVisible = false;
            this.Tábla_ütemező.Size = new System.Drawing.Size(795, 434);
            this.Tábla_ütemező.TabIndex = 168;
            this.Tábla_ütemező.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_ütemező_CellClick);
            this.Tábla_ütemező.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_ütemező_CellContentClick);
            this.Tábla_ütemező.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Tábla_ütemező_CellFormatting);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Ablak_IcsKcsv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(1166, 541);
            this.Controls.Add(this.Pályaszám);
            this.Controls.Add(this.Excel_gomb);
            this.Controls.Add(this.Pályaszámkereső);
            this.Controls.Add(this.Label15);
            this.Controls.Add(this.Fülek);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.Panel2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_IcsKcsv";
            this.Text = "ICS és KCSV futás km adatok";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.IcsKcsv_Load);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.TabPage3.ResumeLayout(false);
            this.Panel7.ResumeLayout(false);
            this.Panel7.PerformLayout();
            this.Panel5.ResumeLayout(false);
            this.Panel5.PerformLayout();
            this.Panel4.ResumeLayout(false);
            this.Panel4.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.Panel6.ResumeLayout(false);
            this.Panel6.PerformLayout();
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            this.TabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla1)).EndInit();
            this.TabPage5.ResumeLayout(false);
            this.TabPage5.PerformLayout();
            this.TabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_lekérdezés)).EndInit();
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            this.Fülek.ResumeLayout(false);
            this.TabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_vezénylés)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_ütemező)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal ComboBox Pályaszám;
        internal Button Excel_gomb;
        internal Button Pályaszámkereső;
        internal Label Label15;
        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal Button BtnSúgó;
        internal Panel Panel2;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal TabPage TabPage3;
        internal Panel Panel7;
        internal TextBox Kerékcsökkenés;
        internal Label Label39;
        internal V_MindenEgyéb.MyProgressbar FőHoltart;
        internal V_MindenEgyéb.MyProgressbar AlHoltart;
        internal Button Command1;
        internal Panel Panel5;
        internal TextBox Text2;
        internal Label Label38;
        internal Panel Panel4;
        internal RadioButton Option12;
        internal RadioButton Option11;
        internal RadioButton Option10;
        internal Label Label37;
        internal Panel Panel1;
        internal TextBox Text1;
        internal RadioButton Option8;
        internal RadioButton Option9;
        internal RadioButton Option7;
        internal RadioButton Option6;
        internal RadioButton Option5;
        internal Label Label36;
        internal CheckedListBox PszJelölő;
        internal Button Mindentkijelöl;
        internal Button Kijelöléstörlése;
        internal Button Command3;
        internal Panel Panel6;
        internal CheckBox Check1;
        internal Panel Panel3;
        internal Button Command2;
        internal ComboBox Telephely;
        internal Label Label16;
        internal TabPage TabPage6;
        internal DataGridView Tábla1;
        internal TabPage TabPage5;
        internal Button Töröl;
        internal TextBox KövV2km;
        internal TextBox KövV2_Sorszám;
        internal TextBox KövV_Sorszám;
        internal TextBox KövV2_számláló;
        internal TextBox KövV2;
        internal TextBox KövV1km;
        internal TextBox KövV;
        internal TextBox Sorszám;
        internal TextBox Jjavszám;
        internal TextBox KMUkm;
        internal TextBox VizsgKm;
        internal TextBox Vizsgfok;
        internal TextBox HaviKm;
        internal TextBox TEljesKmText;
        internal Label Label35;
        internal Label Label34;
        internal ComboBox CiklusrendCombo;
        internal ComboBox Üzemek;
        internal ComboBox Vizsgsorszám;
        internal DateTimePicker KMUdátum;
        internal DateTimePicker Utolsófelújításdátuma;
        internal DateTimePicker Vizsgdátumk;
        internal DateTimePicker Vizsgdátumv;
        internal Button SAP_adatok;
        internal Button Új_adat;
        internal Button Utolsó_V_rögzítés;
        internal Label Label33;
        internal Label Label32;
        internal Label Label31;
        internal Label Label30;
        internal Label Label29;
        internal Label Label28;
        internal Label Label27;
        internal Label Label26;
        internal Label Label25;
        internal Label Label24;
        internal Label Label23;
        internal Label Label22;
        internal Label Label21;
        internal Label Label20;
        internal Label Label19;
        internal Label Label18;
        internal Label Label17;
        internal TabPage TabPage4;
        internal Button Teljes_adatbázis_excel;
        internal DataGridView Tábla_lekérdezés;
        internal Button Excellekérdezés;
        internal Button Lekérdezés_lekérdezés;
        internal TabPage TabPage1;
        internal Label Típus_text;
        internal Label Státus_text;
        internal Label Miótaáll_text;
        internal Label Takarítás_text;
        internal Label Főmérnökség_text;
        internal Label Járműtípus_text;
        internal Label Label8;
        internal Label Label7;
        internal Label Label6;
        internal Label Label3;
        internal Label Label2;
        internal Label Label1;
        internal TabControl Fülek;
        internal Button Button1;
        internal TabPage TabPage2;
        internal DataGridView Tábla_vezénylés;
        internal DateTimePicker Dátum_ütem;
        internal Button Ütem_frissít;
        internal DataGridView Tábla_ütemező;
        internal Button Btn_Vezénylésbeírás;
        internal ToolTip ToolTip1;
        internal Button E_rögzít;
        internal ComboBox Combo_E3;
        internal ComboBox Combo_E2;
        internal Label Label11;
        internal Label Label9;
        internal Button Button3;
        internal Button Button4;
        private Timer timer1;
        internal Button VizsAdat_Excel;
        internal Button VizsAdat_Frissít;
    }
}