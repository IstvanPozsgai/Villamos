using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{

    public partial class Ablak_Fogaskerekű_Tulajdonságok : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Fogaskerekű_Tulajdonságok));
            this.Pályaszám = new System.Windows.Forms.ComboBox();
            this.Label15 = new System.Windows.Forms.Label();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Fülek = new System.Windows.Forms.TabControl();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.Teljes_adatbázis_excel = new System.Windows.Forms.Button();
            this.Tábla_lekérdezés = new System.Windows.Forms.DataGridView();
            this.Excellekérdezés = new System.Windows.Forms.Button();
            this.Lekérdezés_lekérdezés = new System.Windows.Forms.Button();
            this.TabPage6 = new System.Windows.Forms.TabPage();
            this.SAP_adatok = new System.Windows.Forms.Button();
            this.Módosítás = new System.Windows.Forms.Button();
            this.VizsA_Excel = new System.Windows.Forms.Button();
            this.Tábla1 = new System.Windows.Forms.DataGridView();
            this.VizsA_Frisss = new System.Windows.Forms.Button();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.AlHoltart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.FőHoltart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.Panel7 = new System.Windows.Forms.Panel();
            this.Kerékcsökkenés = new System.Windows.Forms.TextBox();
            this.Label39 = new System.Windows.Forms.Label();
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
            this.Option5 = new System.Windows.Forms.RadioButton();
            this.Label36 = new System.Windows.Forms.Label();
            this.Command1 = new System.Windows.Forms.Button();
            this.PszJelölő = new System.Windows.Forms.CheckedListBox();
            this.Panel6 = new System.Windows.Forms.Panel();
            this.Check1 = new System.Windows.Forms.CheckBox();
            this.Mindentkijelöl = new System.Windows.Forms.Button();
            this.Kijelöléstörlése = new System.Windows.Forms.Button();
            this.Command3 = new System.Windows.Forms.Button();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Excel_gomb = new System.Windows.Forms.Button();
            this.Pályaszámkereső = new System.Windows.Forms.Button();
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.Panel2.SuspendLayout();
            this.Fülek.SuspendLayout();
            this.TabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_lekérdezés)).BeginInit();
            this.TabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla1)).BeginInit();
            this.TabPage3.SuspendLayout();
            this.Panel7.SuspendLayout();
            this.Panel5.SuspendLayout();
            this.Panel4.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.Panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pályaszám
            // 
            this.Pályaszám.FormattingEnabled = true;
            this.Pályaszám.Location = new System.Drawing.Point(439, 8);
            this.Pályaszám.Name = "Pályaszám";
            this.Pályaszám.Size = new System.Drawing.Size(124, 28);
            this.Pályaszám.TabIndex = 166;
            this.Pályaszám.SelectedIndexChanged += new System.EventHandler(this.Pályaszám_SelectedIndexChanged);
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Location = new System.Drawing.Point(344, 10);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(89, 20);
            this.Label15.TabIndex = 167;
            this.Label15.Text = "Pályaszám:";
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.Cmbtelephely);
            this.Panel2.Controls.Add(this.Label13);
            this.Panel2.Location = new System.Drawing.Point(5, 5);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(335, 33);
            this.Panel2.TabIndex = 168;
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
            this.Label13.Location = new System.Drawing.Point(5, 5);
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
            this.Fülek.Controls.Add(this.TabPage4);
            this.Fülek.Controls.Add(this.TabPage6);
            this.Fülek.Controls.Add(this.TabPage3);
            this.Fülek.Location = new System.Drawing.Point(3, 55);
            this.Fülek.Name = "Fülek";
            this.Fülek.Padding = new System.Drawing.Point(16, 3);
            this.Fülek.SelectedIndex = 0;
            this.Fülek.Size = new System.Drawing.Size(1144, 445);
            this.Fülek.TabIndex = 171;
            this.Fülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Fülek_DrawItem);
            this.Fülek.SelectedIndexChanged += new System.EventHandler(this.Fülek_SelectedIndexChanged);
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.YellowGreen;
            this.TabPage4.Controls.Add(this.Teljes_adatbázis_excel);
            this.TabPage4.Controls.Add(this.Tábla_lekérdezés);
            this.TabPage4.Controls.Add(this.Excellekérdezés);
            this.TabPage4.Controls.Add(this.Lekérdezés_lekérdezés);
            this.TabPage4.Location = new System.Drawing.Point(4, 29);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Size = new System.Drawing.Size(1136, 412);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Lekérdezések";
            // 
            // Teljes_adatbázis_excel
            // 
            this.Teljes_adatbázis_excel.BackgroundImage = global::Villamos.Properties.Resources.Device_zip;
            this.Teljes_adatbázis_excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Teljes_adatbázis_excel.Location = new System.Drawing.Point(165, 4);
            this.Teljes_adatbázis_excel.Name = "Teljes_adatbázis_excel";
            this.Teljes_adatbázis_excel.Size = new System.Drawing.Size(45, 45);
            this.Teljes_adatbázis_excel.TabIndex = 168;
            this.ToolTip1.SetToolTip(this.Teljes_adatbázis_excel, "A teljes adatbázist kiírja Excelbe");
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
            this.Tábla_lekérdezés.Size = new System.Drawing.Size(1128, 340);
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
            this.ToolTip1.SetToolTip(this.Excellekérdezés, "Táblázat adatait excelbe menti");
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
            this.ToolTip1.SetToolTip(this.Lekérdezés_lekérdezés, "Listázza az állományi adatkat");
            this.Lekérdezés_lekérdezés.UseVisualStyleBackColor = true;
            this.Lekérdezés_lekérdezés.Click += new System.EventHandler(this.Lekérdezés_lekérdezés_Click);
            // 
            // TabPage6
            // 
            this.TabPage6.BackColor = System.Drawing.Color.ForestGreen;
            this.TabPage6.Controls.Add(this.SAP_adatok);
            this.TabPage6.Controls.Add(this.Módosítás);
            this.TabPage6.Controls.Add(this.VizsA_Excel);
            this.TabPage6.Controls.Add(this.Tábla1);
            this.TabPage6.Controls.Add(this.VizsA_Frisss);
            this.TabPage6.Location = new System.Drawing.Point(4, 29);
            this.TabPage6.Name = "TabPage6";
            this.TabPage6.Size = new System.Drawing.Size(1136, 412);
            this.TabPage6.TabIndex = 5;
            this.TabPage6.Text = "Vizsgálati adatok";
            // 
            // SAP_adatok
            // 
            this.SAP_adatok.BackgroundImage = global::Villamos.Properties.Resources.SAP;
            this.SAP_adatok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SAP_adatok.Location = new System.Drawing.Point(231, 3);
            this.SAP_adatok.Name = "SAP_adatok";
            this.SAP_adatok.Size = new System.Drawing.Size(45, 45);
            this.SAP_adatok.TabIndex = 177;
            this.ToolTip1.SetToolTip(this.SAP_adatok, "SAP adatokkal frissíti az adatbázist");
            this.SAP_adatok.UseVisualStyleBackColor = true;
            this.SAP_adatok.Click += new System.EventHandler(this.SAP_adatok_Click);
            // 
            // Módosítás
            // 
            this.Módosítás.BackgroundImage = global::Villamos.Properties.Resources.Action_configure;
            this.Módosítás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Módosítás.Location = new System.Drawing.Point(180, 3);
            this.Módosítás.Name = "Módosítás";
            this.Módosítás.Size = new System.Drawing.Size(45, 45);
            this.Módosítás.TabIndex = 176;
            this.ToolTip1.SetToolTip(this.Módosítás, "Frissíti a lekérdezést");
            this.Módosítás.UseVisualStyleBackColor = true;
            this.Módosítás.Click += new System.EventHandler(this.Módosítás_Click);
            // 
            // VizsA_Excel
            // 
            this.VizsA_Excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.VizsA_Excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VizsA_Excel.Location = new System.Drawing.Point(56, 3);
            this.VizsA_Excel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.VizsA_Excel.Name = "VizsA_Excel";
            this.VizsA_Excel.Size = new System.Drawing.Size(45, 45);
            this.VizsA_Excel.TabIndex = 175;
            this.ToolTip1.SetToolTip(this.VizsA_Excel, "Állomány táblát készít");
            this.VizsA_Excel.UseVisualStyleBackColor = true;
            this.VizsA_Excel.Click += new System.EventHandler(this.VizsA_Excel_Click);
            // 
            // Tábla1
            // 
            this.Tábla1.AllowUserToAddRows = false;
            this.Tábla1.AllowUserToDeleteRows = false;
            this.Tábla1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla1.Location = new System.Drawing.Point(5, 54);
            this.Tábla1.Name = "Tábla1";
            this.Tábla1.RowHeadersVisible = false;
            this.Tábla1.Size = new System.Drawing.Size(1128, 355);
            this.Tábla1.TabIndex = 0;
            this.Tábla1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla1_CellClick);
            // 
            // VizsA_Frisss
            // 
            this.VizsA_Frisss.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.VizsA_Frisss.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VizsA_Frisss.Location = new System.Drawing.Point(5, 3);
            this.VizsA_Frisss.Name = "VizsA_Frisss";
            this.VizsA_Frisss.Size = new System.Drawing.Size(45, 45);
            this.VizsA_Frisss.TabIndex = 174;
            this.ToolTip1.SetToolTip(this.VizsA_Frisss, "Pályaszámnak megfelelően kiírja az adatokat");
            this.VizsA_Frisss.UseVisualStyleBackColor = true;
            this.VizsA_Frisss.Click += new System.EventHandler(this.VizsA_Frisss_Click);
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.TabPage3.Controls.Add(this.AlHoltart);
            this.TabPage3.Controls.Add(this.FőHoltart);
            this.TabPage3.Controls.Add(this.Panel7);
            this.TabPage3.Controls.Add(this.Panel5);
            this.TabPage3.Controls.Add(this.Panel4);
            this.TabPage3.Controls.Add(this.Panel1);
            this.TabPage3.Controls.Add(this.Command1);
            this.TabPage3.Controls.Add(this.PszJelölő);
            this.TabPage3.Controls.Add(this.Panel6);
            this.TabPage3.Controls.Add(this.Mindentkijelöl);
            this.TabPage3.Controls.Add(this.Kijelöléstörlése);
            this.TabPage3.Controls.Add(this.Command3);
            this.TabPage3.Location = new System.Drawing.Point(4, 29);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(1136, 412);
            this.TabPage3.TabIndex = 6;
            this.TabPage3.Text = "Előtervező";
            // 
            // AlHoltart
            // 
            this.AlHoltart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AlHoltart.BackColor = System.Drawing.Color.ForestGreen;
            this.AlHoltart.ForeColor = System.Drawing.Color.SpringGreen;
            this.AlHoltart.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.AlHoltart.Location = new System.Drawing.Point(7, 176);
            this.AlHoltart.Name = "AlHoltart";
            this.AlHoltart.Size = new System.Drawing.Size(1121, 23);
            this.AlHoltart.TabIndex = 175;
            this.AlHoltart.Visible = false;
            // 
            // FőHoltart
            // 
            this.FőHoltart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FőHoltart.BackColor = System.Drawing.Color.ForestGreen;
            this.FőHoltart.ForeColor = System.Drawing.Color.SpringGreen;
            this.FőHoltart.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.FőHoltart.Location = new System.Drawing.Point(7, 147);
            this.FőHoltart.Name = "FőHoltart";
            this.FőHoltart.Size = new System.Drawing.Size(1121, 23);
            this.FőHoltart.TabIndex = 175;
            this.FőHoltart.Visible = false;
            // 
            // Panel7
            // 
            this.Panel7.BackColor = System.Drawing.Color.Tomato;
            this.Panel7.Controls.Add(this.Kerékcsökkenés);
            this.Panel7.Controls.Add(this.Label39);
            this.Panel7.Location = new System.Drawing.Point(242, 5);
            this.Panel7.Name = "Panel7";
            this.Panel7.Size = new System.Drawing.Size(233, 53);
            this.Panel7.TabIndex = 189;
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
            // Panel5
            // 
            this.Panel5.BackColor = System.Drawing.Color.Tomato;
            this.Panel5.Controls.Add(this.Text2);
            this.Panel5.Controls.Add(this.Label38);
            this.Panel5.Location = new System.Drawing.Point(3, 225);
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
            this.Panel4.Location = new System.Drawing.Point(3, 284);
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
            this.Panel1.Controls.Add(this.Option5);
            this.Panel1.Controls.Add(this.Label36);
            this.Panel1.Location = new System.Drawing.Point(3, 69);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(233, 150);
            this.Panel1.TabIndex = 175;
            // 
            // Text1
            // 
            this.Text1.Location = new System.Drawing.Point(138, 114);
            this.Text1.Name = "Text1";
            this.Text1.Size = new System.Drawing.Size(95, 26);
            this.Text1.TabIndex = 95;
            this.Text1.Text = "1500";
            this.Text1.Leave += new System.EventHandler(this.Text1_Leave);
            // 
            // Option8
            // 
            this.Option8.AutoSize = true;
            this.Option8.Checked = true;
            this.Option8.Location = new System.Drawing.Point(3, 116);
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
            this.Option9.Location = new System.Drawing.Point(4, 86);
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
            this.Option7.Location = new System.Drawing.Point(4, 56);
            this.Option7.Name = "Option7";
            this.Option7.Size = new System.Drawing.Size(104, 24);
            this.Option7.TabIndex = 92;
            this.Option7.Text = "Típus átlag";
            this.Option7.UseVisualStyleBackColor = true;
            this.Option7.Click += new System.EventHandler(this.Option7_Click);
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
            // Command1
            // 
            this.Command1.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Command1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command1.Location = new System.Drawing.Point(639, 6);
            this.Command1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Command1.Name = "Command1";
            this.Command1.Size = new System.Drawing.Size(40, 40);
            this.Command1.TabIndex = 177;
            this.ToolTip1.SetToolTip(this.Command1, "Előtervet készít a megadott feltételeknek megfelelően");
            this.Command1.UseVisualStyleBackColor = true;
            this.Command1.Click += new System.EventHandler(this.Command1_Click);
            // 
            // PszJelölő
            // 
            this.PszJelölő.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PszJelölő.CheckOnClick = true;
            this.PszJelölő.FormattingEnabled = true;
            this.PszJelölő.Location = new System.Drawing.Point(481, 5);
            this.PszJelölő.Name = "PszJelölő";
            this.PszJelölő.Size = new System.Drawing.Size(103, 403);
            this.PszJelölő.TabIndex = 174;
            // 
            // Panel6
            // 
            this.Panel6.BackColor = System.Drawing.Color.Tomato;
            this.Panel6.Controls.Add(this.Check1);
            this.Panel6.Location = new System.Drawing.Point(3, 5);
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
            // Mindentkijelöl
            // 
            this.Mindentkijelöl.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.Mindentkijelöl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Mindentkijelöl.Location = new System.Drawing.Point(591, 6);
            this.Mindentkijelöl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Mindentkijelöl.Name = "Mindentkijelöl";
            this.Mindentkijelöl.Size = new System.Drawing.Size(40, 40);
            this.Mindentkijelöl.TabIndex = 169;
            this.ToolTip1.SetToolTip(this.Mindentkijelöl, "Mindent kijelöl");
            this.Mindentkijelöl.UseVisualStyleBackColor = true;
            this.Mindentkijelöl.Click += new System.EventHandler(this.Mindentkijelöl_Click);
            // 
            // Kijelöléstörlése
            // 
            this.Kijelöléstörlése.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.Kijelöléstörlése.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Kijelöléstörlése.Location = new System.Drawing.Point(591, 56);
            this.Kijelöléstörlése.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Kijelöléstörlése.Name = "Kijelöléstörlése";
            this.Kijelöléstörlése.Size = new System.Drawing.Size(40, 40);
            this.Kijelöléstörlése.TabIndex = 170;
            this.ToolTip1.SetToolTip(this.Kijelöléstörlése, "Minden kijelölést töröl");
            this.Kijelöléstörlése.UseVisualStyleBackColor = true;
            this.Kijelöléstörlése.Click += new System.EventHandler(this.Kijelöléstörlése_Click);
            // 
            // Command3
            // 
            this.Command3.BackgroundImage = global::Villamos.Properties.Resources.Aha_Soft_Large_Seo_SEO;
            this.Command3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command3.Location = new System.Drawing.Point(687, 6);
            this.Command3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Command3.Name = "Command3";
            this.Command3.Size = new System.Drawing.Size(40, 40);
            this.Command3.TabIndex = 171;
            this.ToolTip1.SetToolTip(this.Command3, "Tényadatok kimentése Excelbe, előre beállított kimutatással\r\n");
            this.Command3.UseVisualStyleBackColor = true;
            this.Command3.Click += new System.EventHandler(this.Command3_Click);
            // 
            // Excel_gomb
            // 
            this.Excel_gomb.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excel_gomb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excel_gomb.Location = new System.Drawing.Point(620, 5);
            this.Excel_gomb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Excel_gomb.Name = "Excel_gomb";
            this.Excel_gomb.Size = new System.Drawing.Size(45, 45);
            this.Excel_gomb.TabIndex = 173;
            this.ToolTip1.SetToolTip(this.Excel_gomb, "Állomány táblát készít");
            this.Excel_gomb.UseVisualStyleBackColor = true;
            this.Excel_gomb.Click += new System.EventHandler(this.Excel_gomb_Click);
            // 
            // Pályaszámkereső
            // 
            this.Pályaszámkereső.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Pályaszámkereső.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Pályaszámkereső.Location = new System.Drawing.Point(569, 5);
            this.Pályaszámkereső.Name = "Pályaszámkereső";
            this.Pályaszámkereső.Size = new System.Drawing.Size(45, 45);
            this.Pályaszámkereső.TabIndex = 172;
            this.ToolTip1.SetToolTip(this.Pályaszámkereső, "Pályaszámnak megfelelően kiírja az adatokat");
            this.Pályaszámkereső.UseVisualStyleBackColor = true;
            this.Pályaszámkereső.Click += new System.EventHandler(this.Pályaszámkereső_Click);
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1102, 2);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 169;
            this.ToolTip1.SetToolTip(this.BtnSúgó, "Súgó");
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.BackColor = System.Drawing.Color.ForestGreen;
            this.Holtart.ForeColor = System.Drawing.Color.SpringGreen;
            this.Holtart.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Holtart.Location = new System.Drawing.Point(671, 15);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(425, 23);
            this.Holtart.TabIndex = 174;
            this.Holtart.Visible = false;
            // 
            // Ablak_Fogaskerekű_Tulajdonságok
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Coral;
            this.ClientSize = new System.Drawing.Size(1151, 507);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Pályaszám);
            this.Controls.Add(this.Excel_gomb);
            this.Controls.Add(this.Pályaszámkereső);
            this.Controls.Add(this.Label15);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.Fülek);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Fogaskerekű_Tulajdonságok";
            this.Text = "Fogaskerekű adatok";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_Fogaskerekű_Tulajdonságok_FormClosed);
            this.Load += new System.EventHandler(this.Tulajdonságok_Fogaskerekű_Load);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.Fülek.ResumeLayout(false);
            this.TabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_lekérdezés)).EndInit();
            this.TabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla1)).EndInit();
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal ComboBox Pályaszám;
        internal Button Excel_gomb;
        internal Button Pályaszámkereső;
        internal Label Label15;
        internal Button BtnSúgó;
        internal Panel Panel2;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal TabControl Fülek;
        internal TabPage TabPage4;
        internal Button Teljes_adatbázis_excel;
        internal DataGridView Tábla_lekérdezés;
        internal Button Excellekérdezés;
        internal Button Lekérdezés_lekérdezés;
        internal TabPage TabPage6;
        internal DataGridView Tábla1;
        internal TabPage TabPage3;
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
        internal RadioButton Option5;
        internal Label Label36;
        internal Button Command1;
        internal CheckedListBox PszJelölő;
        internal Panel Panel6;
        internal CheckBox Check1;
        internal Button Mindentkijelöl;
        internal Button Kijelöléstörlése;
        internal Button Command3;
        internal Panel Panel7;
        internal TextBox Kerékcsökkenés;
        internal Label Label39;
        internal ToolTip ToolTip1;
        private Timer timer1;
        internal Button VizsA_Excel;
        internal Button VizsA_Frisss;
        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal V_MindenEgyéb.MyProgressbar AlHoltart;
        internal V_MindenEgyéb.MyProgressbar FőHoltart;
        internal Button Módosítás;
        internal Button SAP_adatok;
    }
}