namespace Villamos.Villamos_Ablakok
{
    partial class Ablak_Eszköz
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Eszköz));
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.Fülek = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Szűr_Megnevezés = new System.Windows.Forms.TextBox();
            this.Frissítés = new System.Windows.Forms.Button();
            this.Szűr_Név = new System.Windows.Forms.TextBox();
            this.Szűr_Hely = new System.Windows.Forms.TextBox();
            this.Szűr_Osztás = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.SAP_adatok = new System.Windows.Forms.Button();
            this.BtnExcelkimenet = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.Ellen_Ellenőrzés = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.Ellen_Besorolás = new System.Windows.Forms.ComboBox();
            this.Ellen_Szűrő = new System.Windows.Forms.ComboBox();
            this.Ellen_Frissít = new System.Windows.Forms.Button();
            this.Ellen_Excel = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Besorolás_Combo = new System.Windows.Forms.ComboBox();
            this.Át_Tölt = new System.Windows.Forms.Button();
            this.Besorol = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.Ellen_Tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.Panel2.SuspendLayout();
            this.Fülek.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Ellen_Tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.Cmbtelephely);
            this.Panel2.Controls.Add(this.Label13);
            this.Panel2.Location = new System.Drawing.Point(5, 5);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(335, 33);
            this.Panel2.TabIndex = 174;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(143, 0);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
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
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(346, 10);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(1093, 28);
            this.Holtart.TabIndex = 177;
            this.Holtart.Visible = false;
            // 
            // Fülek
            // 
            this.Fülek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fülek.Controls.Add(this.tabPage1);
            this.Fülek.Controls.Add(this.tabPage2);
            this.Fülek.Location = new System.Drawing.Point(5, 55);
            this.Fülek.Name = "Fülek";
            this.Fülek.SelectedIndex = 0;
            this.Fülek.Size = new System.Drawing.Size(1482, 370);
            this.Fülek.TabIndex = 182;
            this.Fülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Fülek_DrawItem);
            this.Fülek.SelectedIndexChanged += new System.EventHandler(this.Fülek_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Goldenrod;
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.Tábla);
            this.tabPage1.Controls.Add(this.SAP_adatok);
            this.tabPage1.Controls.Add(this.BtnExcelkimenet);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1474, 337);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Adatbeolvasás";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gold;
            this.panel1.Controls.Add(this.Szűr_Megnevezés);
            this.panel1.Controls.Add(this.Frissítés);
            this.panel1.Controls.Add(this.Szűr_Név);
            this.panel1.Controls.Add(this.Szűr_Hely);
            this.panel1.Controls.Add(this.Szűr_Osztás);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(6, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(922, 60);
            this.panel1.TabIndex = 195;
            // 
            // Szűr_Megnevezés
            // 
            this.Szűr_Megnevezés.Location = new System.Drawing.Point(88, 26);
            this.Szűr_Megnevezés.Name = "Szűr_Megnevezés";
            this.Szűr_Megnevezés.Size = new System.Drawing.Size(195, 26);
            this.Szűr_Megnevezés.TabIndex = 182;
            // 
            // Frissítés
            // 
            this.Frissítés.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Frissítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Frissítés.Location = new System.Drawing.Point(866, 3);
            this.Frissítés.Name = "Frissítés";
            this.Frissítés.Size = new System.Drawing.Size(50, 50);
            this.Frissítés.TabIndex = 181;
            this.toolTip1.SetToolTip(this.Frissítés, "A feltételeknek megfelelően listáz");
            this.Frissítés.UseVisualStyleBackColor = true;
            this.Frissítés.Click += new System.EventHandler(this.Frissítés_Click);
            // 
            // Szűr_Név
            // 
            this.Szűr_Név.Location = new System.Drawing.Point(490, 26);
            this.Szűr_Név.Name = "Szűr_Név";
            this.Szűr_Név.Size = new System.Drawing.Size(195, 26);
            this.Szűr_Név.TabIndex = 183;
            // 
            // Szűr_Hely
            // 
            this.Szűr_Hely.Location = new System.Drawing.Point(289, 26);
            this.Szűr_Hely.Name = "Szűr_Hely";
            this.Szűr_Hely.Size = new System.Drawing.Size(195, 26);
            this.Szűr_Hely.TabIndex = 184;
            // 
            // Szűr_Osztás
            // 
            this.Szűr_Osztás.FormattingEnabled = true;
            this.Szűr_Osztás.Location = new System.Drawing.Point(691, 24);
            this.Szűr_Osztás.Name = "Szűr_Osztás";
            this.Szűr_Osztás.Size = new System.Drawing.Size(169, 28);
            this.Szűr_Osztás.TabIndex = 185;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(694, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 20);
            this.label5.TabIndex = 190;
            this.label5.Text = "Besorolás";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.TabIndex = 186;
            this.label1.Text = "Szűrések:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(493, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 20);
            this.label4.TabIndex = 189;
            this.label4.Text = "Dolgozó név:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(91, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 20);
            this.label2.TabIndex = 187;
            this.label2.Text = "Megnevezés:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(292, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 20);
            this.label3.TabIndex = 188;
            this.label3.Text = "Helység:";
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
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.FilterAndSortEnabled = true;
            this.Tábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.Location = new System.Drawing.Point(6, 73);
            this.Tábla.MaxFilterButtonImageHeight = 23;
            this.Tábla.Name = "Tábla";
            this.Tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Tábla.RowHeadersWidth = 25;
            this.Tábla.Size = new System.Drawing.Size(1462, 258);
            this.Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.TabIndex = 191;
            // 
            // SAP_adatok
            // 
            this.SAP_adatok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SAP_adatok.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.SAP_adatok.BackgroundImage = global::Villamos.Properties.Resources.SAP;
            this.SAP_adatok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SAP_adatok.Location = new System.Drawing.Point(1417, 17);
            this.SAP_adatok.Name = "SAP_adatok";
            this.SAP_adatok.Size = new System.Drawing.Size(50, 50);
            this.SAP_adatok.TabIndex = 178;
            this.toolTip1.SetToolTip(this.SAP_adatok, "SAP-s Adatok betöltése");
            this.SAP_adatok.UseVisualStyleBackColor = false;
            this.SAP_adatok.Click += new System.EventHandler(this.SAP_adatok_Click);
            // 
            // BtnExcelkimenet
            // 
            this.BtnExcelkimenet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnExcelkimenet.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnExcelkimenet.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.BtnExcelkimenet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnExcelkimenet.Location = new System.Drawing.Point(1361, 17);
            this.BtnExcelkimenet.Name = "BtnExcelkimenet";
            this.BtnExcelkimenet.Size = new System.Drawing.Size(50, 50);
            this.BtnExcelkimenet.TabIndex = 179;
            this.toolTip1.SetToolTip(this.BtnExcelkimenet, "A táblázatos részt Excel táblába menti.");
            this.BtnExcelkimenet.UseVisualStyleBackColor = false;
            this.BtnExcelkimenet.Click += new System.EventHandler(this.BtnExcelkimenet_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.tabPage2.Controls.Add(this.panel4);
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Controls.Add(this.Ellen_Tábla);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1474, 337);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Ellenőrzések";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Gold;
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.Ellen_Ellenőrzés);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.Ellen_Besorolás);
            this.panel4.Controls.Add(this.Ellen_Szűrő);
            this.panel4.Controls.Add(this.Ellen_Frissít);
            this.panel4.Controls.Add(this.Ellen_Excel);
            this.panel4.Location = new System.Drawing.Point(7, 8);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(547, 67);
            this.panel4.TabIndex = 206;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 20);
            this.label7.TabIndex = 196;
            this.label7.Text = "Besorolás";
            // 
            // Ellen_Ellenőrzés
            // 
            this.Ellen_Ellenőrzés.BackgroundImage = global::Villamos.Properties.Resources.App_network_connection_manager;
            this.Ellen_Ellenőrzés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Ellen_Ellenőrzés.Location = new System.Drawing.Point(489, 11);
            this.Ellen_Ellenőrzés.Name = "Ellen_Ellenőrzés";
            this.Ellen_Ellenőrzés.Size = new System.Drawing.Size(50, 50);
            this.Ellen_Ellenőrzés.TabIndex = 193;
            this.toolTip1.SetToolTip(this.Ellen_Ellenőrzés, "A táblázatban listázott elemeknél megnézi, hogy melyik nyilvántartásban szerepel." +
        "");
            this.Ellen_Ellenőrzés.UseVisualStyleBackColor = true;
            this.Ellen_Ellenőrzés.Click += new System.EventHandler(this.Ellen_Ellenőr_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(185, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(181, 20);
            this.label8.TabIndex = 200;
            this.label8.Text = "Nyilvántartás Ellenőrzés:";
            // 
            // Ellen_Besorolás
            // 
            this.Ellen_Besorolás.FormattingEnabled = true;
            this.Ellen_Besorolás.Location = new System.Drawing.Point(7, 31);
            this.Ellen_Besorolás.Name = "Ellen_Besorolás";
            this.Ellen_Besorolás.Size = new System.Drawing.Size(171, 28);
            this.Ellen_Besorolás.TabIndex = 195;
            // 
            // Ellen_Szűrő
            // 
            this.Ellen_Szűrő.FormattingEnabled = true;
            this.Ellen_Szűrő.Location = new System.Drawing.Point(184, 31);
            this.Ellen_Szűrő.Name = "Ellen_Szűrő";
            this.Ellen_Szűrő.Size = new System.Drawing.Size(187, 28);
            this.Ellen_Szűrő.TabIndex = 199;
            // 
            // Ellen_Frissít
            // 
            this.Ellen_Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Ellen_Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Ellen_Frissít.Location = new System.Drawing.Point(377, 11);
            this.Ellen_Frissít.Name = "Ellen_Frissít";
            this.Ellen_Frissít.Size = new System.Drawing.Size(50, 50);
            this.Ellen_Frissít.TabIndex = 197;
            this.toolTip1.SetToolTip(this.Ellen_Frissít, "A feltételeknek megfelelően listáz");
            this.Ellen_Frissít.UseVisualStyleBackColor = true;
            this.Ellen_Frissít.Click += new System.EventHandler(this.Ellen_Frissít_Click);
            // 
            // Ellen_Excel
            // 
            this.Ellen_Excel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Ellen_Excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Ellen_Excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Ellen_Excel.Location = new System.Drawing.Point(433, 11);
            this.Ellen_Excel.Name = "Ellen_Excel";
            this.Ellen_Excel.Size = new System.Drawing.Size(50, 50);
            this.Ellen_Excel.TabIndex = 198;
            this.toolTip1.SetToolTip(this.Ellen_Excel, "A táblázatos részt Excel táblába menti.");
            this.Ellen_Excel.UseVisualStyleBackColor = false;
            this.Ellen_Excel.Click += new System.EventHandler(this.Ellen_Excel_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Gold;
            this.panel3.Controls.Add(this.Besorolás_Combo);
            this.panel3.Controls.Add(this.Át_Tölt);
            this.panel3.Controls.Add(this.Besorol);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Location = new System.Drawing.Point(562, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(303, 68);
            this.panel3.TabIndex = 205;
            // 
            // Besorolás_Combo
            // 
            this.Besorolás_Combo.FormattingEnabled = true;
            this.Besorolás_Combo.Location = new System.Drawing.Point(8, 24);
            this.Besorolás_Combo.Name = "Besorolás_Combo";
            this.Besorolás_Combo.Size = new System.Drawing.Size(171, 28);
            this.Besorolás_Combo.TabIndex = 202;
            // 
            // Át_Tölt
            // 
            this.Át_Tölt.BackgroundImage = global::Villamos.Properties.Resources.process_accept;
            this.Át_Tölt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Át_Tölt.Location = new System.Drawing.Point(241, 11);
            this.Át_Tölt.Name = "Át_Tölt";
            this.Át_Tölt.Size = new System.Drawing.Size(50, 50);
            this.Át_Tölt.TabIndex = 204;
            this.toolTip1.SetToolTip(this.Át_Tölt, "A kijelölt elemeket létrehozza a Épület/Szerszám nyilvántatásban.");
            this.Át_Tölt.UseVisualStyleBackColor = true;
            this.Át_Tölt.Click += new System.EventHandler(this.Át_Tölt_Click);
            // 
            // Besorol
            // 
            this.Besorol.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Besorol.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Besorol.Location = new System.Drawing.Point(185, 11);
            this.Besorol.Name = "Besorol";
            this.Besorol.Size = new System.Drawing.Size(50, 50);
            this.Besorol.TabIndex = 201;
            this.toolTip1.SetToolTip(this.Besorol, "A kijelölt elemek besorolását megváltoztatja.");
            this.Besorol.UseVisualStyleBackColor = true;
            this.Besorol.Click += new System.EventHandler(this.Besorol_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 20);
            this.label6.TabIndex = 203;
            this.label6.Text = "Besorolás";
            // 
            // Ellen_Tábla
            // 
            this.Ellen_Tábla.AllowUserToAddRows = false;
            this.Ellen_Tábla.AllowUserToDeleteRows = false;
            this.Ellen_Tábla.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Ellen_Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.Ellen_Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Ellen_Tábla.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.Ellen_Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Ellen_Tábla.FilterAndSortEnabled = true;
            this.Ellen_Tábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Ellen_Tábla.Location = new System.Drawing.Point(6, 80);
            this.Ellen_Tábla.MaxFilterButtonImageHeight = 23;
            this.Ellen_Tábla.Name = "Ellen_Tábla";
            this.Ellen_Tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Ellen_Tábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.Ellen_Tábla.RowHeadersWidth = 25;
            this.Ellen_Tábla.Size = new System.Drawing.Size(1462, 250);
            this.Ellen_Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Ellen_Tábla.TabIndex = 192;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1445, 5);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 176;
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // Ablak_Eszköz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.ClientSize = new System.Drawing.Size(1495, 434);
            this.Controls.Add(this.Fülek);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.Panel2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Eszköz";
            this.Text = "SAP Eszköz nyilvántartás";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_Eszköz_Load);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.Fülek.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Ellen_Tábla)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.ComboBox Cmbtelephely;
        internal System.Windows.Forms.Label Label13;
        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal System.Windows.Forms.Button BtnSúgó;
        internal System.Windows.Forms.Button SAP_adatok;
        internal System.Windows.Forms.Button BtnExcelkimenet;
        internal System.Windows.Forms.Button Frissítés;
        internal System.Windows.Forms.TabPage tabPage1;
        internal System.Windows.Forms.TabPage tabPage2;
        internal System.Windows.Forms.ComboBox Szűr_Osztás;
        internal System.Windows.Forms.TextBox Szűr_Hely;
        internal System.Windows.Forms.TextBox Szűr_Név;
        internal System.Windows.Forms.TextBox Szűr_Megnevezés;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.ToolTip toolTip1;
        internal Zuby.ADGV.AdvancedDataGridView Tábla;
        internal System.Windows.Forms.TabControl Fülek;
        internal System.Windows.Forms.Panel panel1;
        internal Zuby.ADGV.AdvancedDataGridView Ellen_Tábla;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.ComboBox Ellen_Besorolás;
        internal System.Windows.Forms.Button Ellen_Ellenőrzés;
        internal System.Windows.Forms.Button Ellen_Excel;
        internal System.Windows.Forms.Button Ellen_Frissít;
        internal System.Windows.Forms.Label label8;
        internal System.Windows.Forms.ComboBox Ellen_Szűrő;
        internal System.Windows.Forms.Panel panel3;
        internal System.Windows.Forms.ComboBox Besorolás_Combo;
        internal System.Windows.Forms.Button Át_Tölt;
        internal System.Windows.Forms.Button Besorol;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Panel panel4;
    }
}