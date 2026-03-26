namespace Villamos.Villamos_Ablakok
{
    partial class Ablak_Eszterga_Választék
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Eszterga_Választék));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Marad = new System.Windows.Forms.CheckBox();
            this.Tev_idő = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Színválasztás_Háttér = new System.Windows.Forms.Button();
            this.Töröl = new System.Windows.Forms.Button();
            this.Tev_Új = new System.Windows.Forms.Button();
            this.Tevékenység_Tábla = new System.Windows.Forms.DataGridView();
            this.Tev_Rögzít = new System.Windows.Forms.Button();
            this.Színválasztás_Betű = new System.Windows.Forms.Button();
            this.Tev_Tábla_frissítés = new System.Windows.Forms.Button();
            this.Feljebb = new System.Windows.Forms.Button();
            this.Tev_Tevékenység = new System.Windows.Forms.TextBox();
            this.Tev_Betű = new System.Windows.Forms.TextBox();
            this.Tev_Háttér = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Tev_Id = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Állapot = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Norm_Típus = new System.Windows.Forms.ComboBox();
            this.Norma_Tábla = new System.Windows.Forms.DataGridView();
            this.Norm_Munkaidő = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Norm_Rögzítés = new System.Windows.Forms.Button();
            this.Norm_Frissít = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.TáblaAutomata = new System.Windows.Forms.DataGridView();
            this.UtolsóÜzenet = new System.Windows.Forms.DateTimePicker();
            this.TörlésAutomata = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.Felhasználók = new System.Windows.Forms.ComboBox();
            this.OKAutomata = new System.Windows.Forms.Button();
            this.FrissítAutomata = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tevékenység_Tábla)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Norma_Tábla)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TáblaAutomata)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(766, 380);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Cyan;
            this.tabPage1.Controls.Add(this.Marad);
            this.tabPage1.Controls.Add(this.Tev_idő);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.Színválasztás_Háttér);
            this.tabPage1.Controls.Add(this.Töröl);
            this.tabPage1.Controls.Add(this.Tev_Új);
            this.tabPage1.Controls.Add(this.Tevékenység_Tábla);
            this.tabPage1.Controls.Add(this.Tev_Rögzít);
            this.tabPage1.Controls.Add(this.Színválasztás_Betű);
            this.tabPage1.Controls.Add(this.Tev_Tábla_frissítés);
            this.tabPage1.Controls.Add(this.Feljebb);
            this.tabPage1.Controls.Add(this.Tev_Tevékenység);
            this.tabPage1.Controls.Add(this.Tev_Betű);
            this.tabPage1.Controls.Add(this.Tev_Háttér);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.Tev_Id);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(758, 347);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tevékenységek";
            // 
            // Marad
            // 
            this.Marad.AutoSize = true;
            this.Marad.Location = new System.Drawing.Point(284, 72);
            this.Marad.Name = "Marad";
            this.Marad.Size = new System.Drawing.Size(135, 24);
            this.Marad.TabIndex = 196;
            this.Marad.Text = "Helyben marad";
            this.Marad.UseVisualStyleBackColor = true;
            // 
            // Tev_idő
            // 
            this.Tev_idő.Location = new System.Drawing.Point(118, 70);
            this.Tev_idő.MaxLength = 50;
            this.Tev_idő.Name = "Tev_idő";
            this.Tev_idő.Size = new System.Drawing.Size(100, 26);
            this.Tev_idő.TabIndex = 195;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 20);
            this.label5.TabIndex = 194;
            this.label5.Text = "Idő:";
            // 
            // Színválasztás_Háttér
            // 
            this.Színválasztás_Háttér.BackgroundImage = global::Villamos.Properties.Resources.Dtafalonso_Modern_Xp_ModernXP_12_Workstation_Desktop_Colors;
            this.Színválasztás_Háttér.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Színválasztás_Háttér.Location = new System.Drawing.Point(224, 102);
            this.Színválasztás_Háttér.Name = "Színválasztás_Háttér";
            this.Színválasztás_Háttér.Size = new System.Drawing.Size(40, 40);
            this.Színválasztás_Háttér.TabIndex = 193;
            this.Színválasztás_Háttér.UseVisualStyleBackColor = true;
            this.Színválasztás_Háttér.Click += new System.EventHandler(this.Színválasztás_Háttér_Click);
            // 
            // Töröl
            // 
            this.Töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Töröl.Location = new System.Drawing.Point(604, 97);
            this.Töröl.Name = "Töröl";
            this.Töröl.Size = new System.Drawing.Size(45, 45);
            this.Töröl.TabIndex = 192;
            this.Töröl.UseVisualStyleBackColor = true;
            this.Töröl.Click += new System.EventHandler(this.Töröl_Click);
            // 
            // Tev_Új
            // 
            this.Tev_Új.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Tev_Új.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Tev_Új.Location = new System.Drawing.Point(553, 97);
            this.Tev_Új.Name = "Tev_Új";
            this.Tev_Új.Size = new System.Drawing.Size(45, 45);
            this.Tev_Új.TabIndex = 191;
            this.Tev_Új.UseVisualStyleBackColor = true;
            this.Tev_Új.Click += new System.EventHandler(this.Tev_Új_Click);
            // 
            // Tevékenység_Tábla
            // 
            this.Tevékenység_Tábla.AllowUserToAddRows = false;
            this.Tevékenység_Tábla.AllowUserToDeleteRows = false;
            this.Tevékenység_Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tevékenység_Tábla.Location = new System.Drawing.Point(6, 148);
            this.Tevékenység_Tábla.Name = "Tevékenység_Tábla";
            this.Tevékenység_Tábla.RowHeadersVisible = false;
            this.Tevékenység_Tábla.Size = new System.Drawing.Size(745, 193);
            this.Tevékenység_Tábla.TabIndex = 190;
            this.Tevékenység_Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tevékenység_Tábla_CellClick);
            // 
            // Tev_Rögzít
            // 
            this.Tev_Rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Tev_Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Tev_Rögzít.Location = new System.Drawing.Point(553, 21);
            this.Tev_Rögzít.Name = "Tev_Rögzít";
            this.Tev_Rögzít.Size = new System.Drawing.Size(45, 45);
            this.Tev_Rögzít.TabIndex = 188;
            this.Tev_Rögzít.UseVisualStyleBackColor = true;
            this.Tev_Rögzít.Click += new System.EventHandler(this.Tev_Rögzít_Click);
            // 
            // Színválasztás_Betű
            // 
            this.Színválasztás_Betű.BackgroundImage = global::Villamos.Properties.Resources.Dtafalonso_Modern_Xp_ModernXP_12_Workstation_Desktop_Colors;
            this.Színválasztás_Betű.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Színválasztás_Betű.Location = new System.Drawing.Point(485, 102);
            this.Színválasztás_Betű.Name = "Színválasztás_Betű";
            this.Színválasztás_Betű.Size = new System.Drawing.Size(40, 40);
            this.Színválasztás_Betű.TabIndex = 187;
            this.Színválasztás_Betű.UseVisualStyleBackColor = true;
            this.Színválasztás_Betű.Click += new System.EventHandler(this.Színválasztás_Betű_Click);
            // 
            // Tev_Tábla_frissítés
            // 
            this.Tev_Tábla_frissítés.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Tev_Tábla_frissítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Tev_Tábla_frissítés.Location = new System.Drawing.Point(706, 97);
            this.Tev_Tábla_frissítés.Name = "Tev_Tábla_frissítés";
            this.Tev_Tábla_frissítés.Size = new System.Drawing.Size(45, 45);
            this.Tev_Tábla_frissítés.TabIndex = 186;
            this.Tev_Tábla_frissítés.UseVisualStyleBackColor = true;
            this.Tev_Tábla_frissítés.Click += new System.EventHandler(this.Tev_Tábla_frissítés_Click);
            // 
            // Feljebb
            // 
            this.Feljebb.BackgroundImage = global::Villamos.Properties.Resources.Up_gyűjtemény;
            this.Feljebb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Feljebb.Location = new System.Drawing.Point(655, 97);
            this.Feljebb.Name = "Feljebb";
            this.Feljebb.Size = new System.Drawing.Size(45, 45);
            this.Feljebb.TabIndex = 185;
            this.Feljebb.UseVisualStyleBackColor = true;
            this.Feljebb.Click += new System.EventHandler(this.Feljebb_Click);
            // 
            // Tev_Tevékenység
            // 
            this.Tev_Tevékenység.Location = new System.Drawing.Point(118, 38);
            this.Tev_Tevékenység.MaxLength = 50;
            this.Tev_Tevékenység.Name = "Tev_Tevékenység";
            this.Tev_Tevékenység.Size = new System.Drawing.Size(409, 26);
            this.Tev_Tevékenység.TabIndex = 7;
            // 
            // Tev_Betű
            // 
            this.Tev_Betű.Enabled = false;
            this.Tev_Betű.Location = new System.Drawing.Point(379, 116);
            this.Tev_Betű.Name = "Tev_Betű";
            this.Tev_Betű.Size = new System.Drawing.Size(100, 26);
            this.Tev_Betű.TabIndex = 6;
            // 
            // Tev_Háttér
            // 
            this.Tev_Háttér.Enabled = false;
            this.Tev_Háttér.Location = new System.Drawing.Point(118, 116);
            this.Tev_Háttér.Name = "Tev_Háttér";
            this.Tev_Háttér.Size = new System.Drawing.Size(100, 26);
            this.Tev_Háttér.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Sorszám";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Háttérszín";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(280, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Betűszín";
            // 
            // Tev_Id
            // 
            this.Tev_Id.Enabled = false;
            this.Tev_Id.Location = new System.Drawing.Point(118, 6);
            this.Tev_Id.Name = "Tev_Id";
            this.Tev_Id.Size = new System.Drawing.Size(100, 26);
            this.Tev_Id.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tevékenység";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.DarkSalmon;
            this.tabPage2.Controls.Add(this.Állapot);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.Norm_Típus);
            this.tabPage2.Controls.Add(this.Norma_Tábla);
            this.tabPage2.Controls.Add(this.Norm_Munkaidő);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.Norm_Rögzítés);
            this.tabPage2.Controls.Add(this.Norm_Frissít);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(758, 347);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Normaidő";
            // 
            // Állapot
            // 
            this.Állapot.Location = new System.Drawing.Point(78, 41);
            this.Állapot.MaxLength = 50;
            this.Állapot.Name = "Állapot";
            this.Állapot.Size = new System.Drawing.Size(100, 26);
            this.Állapot.TabIndex = 209;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 20);
            this.label7.TabIndex = 208;
            this.label7.Text = "Állapot:";
            // 
            // Norm_Típus
            // 
            this.Norm_Típus.FormattingEnabled = true;
            this.Norm_Típus.Location = new System.Drawing.Point(78, 7);
            this.Norm_Típus.Name = "Norm_Típus";
            this.Norm_Típus.Size = new System.Drawing.Size(164, 28);
            this.Norm_Típus.TabIndex = 207;
            // 
            // Norma_Tábla
            // 
            this.Norma_Tábla.AllowUserToAddRows = false;
            this.Norma_Tábla.AllowUserToDeleteRows = false;
            this.Norma_Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Norma_Tábla.Location = new System.Drawing.Point(7, 115);
            this.Norma_Tábla.Name = "Norma_Tábla";
            this.Norma_Tábla.RowHeadersVisible = false;
            this.Norma_Tábla.Size = new System.Drawing.Size(745, 226);
            this.Norma_Tábla.TabIndex = 206;
            this.Norma_Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Norma_Tábla_CellClick);
            // 
            // Norm_Munkaidő
            // 
            this.Norm_Munkaidő.Location = new System.Drawing.Point(78, 73);
            this.Norm_Munkaidő.MaxLength = 50;
            this.Norm_Munkaidő.Name = "Norm_Munkaidő";
            this.Norm_Munkaidő.Size = new System.Drawing.Size(100, 26);
            this.Norm_Munkaidő.TabIndex = 205;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 20);
            this.label6.TabIndex = 204;
            this.label6.Text = "Idő:";
            // 
            // Norm_Rögzítés
            // 
            this.Norm_Rögzítés.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Norm_Rögzítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Norm_Rögzítés.Location = new System.Drawing.Point(320, 7);
            this.Norm_Rögzítés.Name = "Norm_Rögzítés";
            this.Norm_Rögzítés.Size = new System.Drawing.Size(45, 45);
            this.Norm_Rögzítés.TabIndex = 201;
            this.Norm_Rögzítés.UseVisualStyleBackColor = true;
            this.Norm_Rögzítés.Click += new System.EventHandler(this.Norm_Rögzítés_Click);
            // 
            // Norm_Frissít
            // 
            this.Norm_Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Norm_Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Norm_Frissít.Location = new System.Drawing.Point(320, 58);
            this.Norm_Frissít.Name = "Norm_Frissít";
            this.Norm_Frissít.Size = new System.Drawing.Size(45, 45);
            this.Norm_Frissít.TabIndex = 200;
            this.Norm_Frissít.UseVisualStyleBackColor = true;
            this.Norm_Frissít.Click += new System.EventHandler(this.Norm_Frissít_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 20);
            this.label8.TabIndex = 196;
            this.label8.Text = "Típus:";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.tabPage3.Controls.Add(this.TáblaAutomata);
            this.tabPage3.Controls.Add(this.UtolsóÜzenet);
            this.tabPage3.Controls.Add(this.TörlésAutomata);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.Felhasználók);
            this.tabPage3.Controls.Add(this.OKAutomata);
            this.tabPage3.Controls.Add(this.FrissítAutomata);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(758, 347);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Automata Üzenet";
            // 
            // TáblaAutomata
            // 
            this.TáblaAutomata.AllowUserToAddRows = false;
            this.TáblaAutomata.AllowUserToDeleteRows = false;
            this.TáblaAutomata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TáblaAutomata.Location = new System.Drawing.Point(6, 89);
            this.TáblaAutomata.Name = "TáblaAutomata";
            this.TáblaAutomata.RowHeadersVisible = false;
            this.TáblaAutomata.Size = new System.Drawing.Size(745, 255);
            this.TáblaAutomata.TabIndex = 215;
            this.TáblaAutomata.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TáblaAutomata_CellClick);
            // 
            // UtolsóÜzenet
            // 
            this.UtolsóÜzenet.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.UtolsóÜzenet.Location = new System.Drawing.Point(145, 56);
            this.UtolsóÜzenet.Name = "UtolsóÜzenet";
            this.UtolsóÜzenet.Size = new System.Drawing.Size(124, 26);
            this.UtolsóÜzenet.TabIndex = 214;
            // 
            // TörlésAutomata
            // 
            this.TörlésAutomata.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.TörlésAutomata.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TörlésAutomata.Location = new System.Drawing.Point(599, 37);
            this.TörlésAutomata.Name = "TörlésAutomata";
            this.TörlésAutomata.Size = new System.Drawing.Size(45, 45);
            this.TörlésAutomata.TabIndex = 213;
            this.TörlésAutomata.UseVisualStyleBackColor = true;
            this.TörlésAutomata.Click += new System.EventHandler(this.TörlésAutomata_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 62);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 20);
            this.label10.TabIndex = 212;
            this.label10.Text = "Dátum:";
            // 
            // Felhasználók
            // 
            this.Felhasználók.FormattingEnabled = true;
            this.Felhasználók.Location = new System.Drawing.Point(145, 11);
            this.Felhasználók.Name = "Felhasználók";
            this.Felhasználók.Size = new System.Drawing.Size(233, 28);
            this.Felhasználók.TabIndex = 211;
            // 
            // OKAutomata
            // 
            this.OKAutomata.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.OKAutomata.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.OKAutomata.Location = new System.Drawing.Point(393, 37);
            this.OKAutomata.Name = "OKAutomata";
            this.OKAutomata.Size = new System.Drawing.Size(45, 45);
            this.OKAutomata.TabIndex = 210;
            this.OKAutomata.UseVisualStyleBackColor = true;
            this.OKAutomata.Click += new System.EventHandler(this.OKAutomata_Click);
            // 
            // FrissítAutomata
            // 
            this.FrissítAutomata.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.FrissítAutomata.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FrissítAutomata.Location = new System.Drawing.Point(650, 37);
            this.FrissítAutomata.Name = "FrissítAutomata";
            this.FrissítAutomata.Size = new System.Drawing.Size(45, 45);
            this.FrissítAutomata.TabIndex = 209;
            this.FrissítAutomata.UseVisualStyleBackColor = true;
            this.FrissítAutomata.Click += new System.EventHandler(this.FrissítAutomata_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(131, 20);
            this.label9.TabIndex = 208;
            this.label9.Text = "Felhasználói név:";
            // 
            // Ablak_Eszterga_Választék
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 404);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Ablak_Eszterga_Választék";
            this.Text = "Kerékesztergálás beállítások";
            this.Load += new System.EventHandler(this.Ablak_Eszterga_Választék_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ablak_Eszterga_Választék_KeyDown);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tevékenység_Tábla)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Norma_Tábla)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TáblaAutomata)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Tev_Id;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Tev_Tevékenység;
        private System.Windows.Forms.TextBox Tev_Betű;
        private System.Windows.Forms.TextBox Tev_Háttér;
        private System.Windows.Forms.DataGridView Tevékenység_Tábla;
        internal System.Windows.Forms.Button Tev_Rögzít;
        internal System.Windows.Forms.Button Színválasztás_Betű;
        internal System.Windows.Forms.Button Tev_Tábla_frissítés;
        internal System.Windows.Forms.Button Feljebb;
        internal System.Windows.Forms.Button Töröl;
        internal System.Windows.Forms.Button Tev_Új;
        internal System.Windows.Forms.Button Színválasztás_Háttér;
        private System.Windows.Forms.TextBox Tev_idő;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView Norma_Tábla;
        private System.Windows.Forms.TextBox Norm_Munkaidő;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Button Norm_Rögzítés;
        internal System.Windows.Forms.Button Norm_Frissít;
        private System.Windows.Forms.Label label8;
        internal System.Windows.Forms.ComboBox Norm_Típus;
        private System.Windows.Forms.CheckBox Marad;
        private System.Windows.Forms.TextBox Állapot;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DateTimePicker UtolsóÜzenet;
        internal System.Windows.Forms.Button TörlésAutomata;
        private System.Windows.Forms.Label label10;
        internal System.Windows.Forms.ComboBox Felhasználók;
        internal System.Windows.Forms.Button OKAutomata;
        internal System.Windows.Forms.Button FrissítAutomata;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView TáblaAutomata;
    }
}