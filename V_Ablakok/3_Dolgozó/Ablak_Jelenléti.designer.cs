using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
    
    internal partial class Ablak_Jelenléti : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Jelenléti));
            this.ChkCsoport = new System.Windows.Forms.CheckedListBox();
            this.ChkDolgozónév = new System.Windows.Forms.CheckedListBox();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.LstKiadta = new System.Windows.Forms.ComboBox();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.RdBtnNemNyomtat = new System.Windows.Forms.RadioButton();
            this.RdBtnNyomtat = new System.Windows.Forms.RadioButton();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.RdBtnFájlNemTöröl = new System.Windows.Forms.RadioButton();
            this.RdBtnFájlTöröl = new System.Windows.Forms.RadioButton();
            this.GroupBox4 = new System.Windows.Forms.GroupBox();
            this.RdBtnSzakszolgálatVezető = new System.Windows.Forms.RadioButton();
            this.RdBtnÜzemvezető = new System.Windows.Forms.RadioButton();
            this.GroupBox5 = new System.Windows.Forms.GroupBox();
            this.Option21 = new System.Windows.Forms.RadioButton();
            this.RdBtnA4 = new System.Windows.Forms.RadioButton();
            this.Dátum = new System.Windows.Forms.DateTimePicker();
            this.GroupBox6 = new System.Windows.Forms.GroupBox();
            this.Éjszakás = new System.Windows.Forms.CheckBox();
            this.Btn_Heti = new System.Windows.Forms.Button();
            this.RdBtn7Napos = new System.Windows.Forms.RadioButton();
            this.RdBtn6Napos = new System.Windows.Forms.RadioButton();
            this.RdBtn5Napos = new System.Windows.Forms.RadioButton();
            this.GroupBox7 = new System.Windows.Forms.GroupBox();
            this.ChckBxHVasárnap = new System.Windows.Forms.CheckBox();
            this.ChckBxHSzombat = new System.Windows.Forms.CheckBox();
            this.ChckBxHPéntek = new System.Windows.Forms.CheckBox();
            this.ChckBxHCsütörtök = new System.Windows.Forms.CheckBox();
            this.ChckBxHSzerda = new System.Windows.Forms.CheckBox();
            this.ChckBxHKedd = new System.Windows.Forms.CheckBox();
            this.ChckBxHétfő = new System.Windows.Forms.CheckBox();
            this.Btn_Szellemi = new System.Windows.Forms.Button();
            this.GroupBox9 = new System.Windows.Forms.GroupBox();
            this.Btn_Váltós = new System.Windows.Forms.Button();
            this.BtnKijelölésátjelöl = new System.Windows.Forms.Button();
            this.Btnkilelöltörlés = new System.Windows.Forms.Button();
            this.Btnkijelöléstöröl = new System.Windows.Forms.Button();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Súgó = new System.Windows.Forms.Button();
            this.GroupBox8 = new System.Windows.Forms.GroupBox();
            this.Heti_ittas = new System.Windows.Forms.RadioButton();
            this.Napi_ittas = new System.Windows.Forms.RadioButton();
            this.Btn_Kiválogat = new System.Windows.Forms.Button();
            this.Btn_Ittasság = new System.Windows.Forms.Button();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Btnmindkijelöl = new System.Windows.Forms.Button();
            this.BtnKijelölcsop = new System.Windows.Forms.Button();
            this.Holtart = new V_MindenEgyéb.MyProgressbar();
            this.GroupBox1.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.GroupBox4.SuspendLayout();
            this.GroupBox5.SuspendLayout();
            this.GroupBox6.SuspendLayout();
            this.GroupBox7.SuspendLayout();
            this.GroupBox9.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.GroupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChkCsoport
            // 
            this.ChkCsoport.CheckOnClick = true;
            this.ChkCsoport.FormattingEnabled = true;
            this.ChkCsoport.Location = new System.Drawing.Point(4, 46);
            this.ChkCsoport.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ChkCsoport.Name = "ChkCsoport";
            this.ChkCsoport.Size = new System.Drawing.Size(379, 151);
            this.ChkCsoport.TabIndex = 0;
            this.ToolTip1.SetToolTip(this.ChkCsoport, "Csoport választó");
            // 
            // ChkDolgozónév
            // 
            this.ChkDolgozónév.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ChkDolgozónév.CheckOnClick = true;
            this.ChkDolgozónév.FormattingEnabled = true;
            this.ChkDolgozónév.Location = new System.Drawing.Point(4, 203);
            this.ChkDolgozónév.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ChkDolgozónév.Name = "ChkDolgozónév";
            this.ChkDolgozónév.Size = new System.Drawing.Size(379, 403);
            this.ChkDolgozónév.TabIndex = 1;
            this.ToolTip1.SetToolTip(this.ChkDolgozónév, "Dolgozó választó");
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GroupBox1.BackColor = System.Drawing.Color.LightSalmon;
            this.GroupBox1.Controls.Add(this.LstKiadta);
            this.GroupBox1.Location = new System.Drawing.Point(4, 623);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(379, 60);
            this.GroupBox1.TabIndex = 2;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Napi igazoló";
            // 
            // LstKiadta
            // 
            this.LstKiadta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LstKiadta.FormattingEnabled = true;
            this.LstKiadta.Location = new System.Drawing.Point(11, 25);
            this.LstKiadta.Name = "LstKiadta";
            this.LstKiadta.Size = new System.Drawing.Size(362, 28);
            this.LstKiadta.TabIndex = 0;
            this.ToolTip1.SetToolTip(this.LstKiadta, "Napi igazoló személy választó");
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.RdBtnNemNyomtat);
            this.GroupBox2.Controls.Add(this.RdBtnNyomtat);
            this.GroupBox2.Location = new System.Drawing.Point(556, 80);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(224, 94);
            this.GroupBox2.TabIndex = 3;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Nyomtatás";
            // 
            // RdBtnNemNyomtat
            // 
            this.RdBtnNemNyomtat.AutoSize = true;
            this.RdBtnNemNyomtat.Location = new System.Drawing.Point(13, 53);
            this.RdBtnNemNyomtat.Name = "RdBtnNemNyomtat";
            this.RdBtnNemNyomtat.Size = new System.Drawing.Size(60, 24);
            this.RdBtnNemNyomtat.TabIndex = 1;
            this.RdBtnNemNyomtat.Text = "Nem";
            this.RdBtnNemNyomtat.UseVisualStyleBackColor = true;
            // 
            // RdBtnNyomtat
            // 
            this.RdBtnNyomtat.AutoSize = true;
            this.RdBtnNyomtat.Checked = true;
            this.RdBtnNyomtat.Location = new System.Drawing.Point(13, 23);
            this.RdBtnNyomtat.Name = "RdBtnNyomtat";
            this.RdBtnNyomtat.Size = new System.Drawing.Size(59, 24);
            this.RdBtnNyomtat.TabIndex = 0;
            this.RdBtnNyomtat.TabStop = true;
            this.RdBtnNyomtat.Text = "Igen";
            this.RdBtnNyomtat.UseVisualStyleBackColor = true;
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.RdBtnFájlNemTöröl);
            this.GroupBox3.Controls.Add(this.RdBtnFájlTöröl);
            this.GroupBox3.Location = new System.Drawing.Point(556, 185);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(224, 94);
            this.GroupBox3.TabIndex = 4;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "Fájl törlés";
            // 
            // RdBtnFájlNemTöröl
            // 
            this.RdBtnFájlNemTöröl.AutoSize = true;
            this.RdBtnFájlNemTöröl.Location = new System.Drawing.Point(13, 53);
            this.RdBtnFájlNemTöröl.Name = "RdBtnFájlNemTöröl";
            this.RdBtnFájlNemTöröl.Size = new System.Drawing.Size(60, 24);
            this.RdBtnFájlNemTöröl.TabIndex = 1;
            this.RdBtnFájlNemTöröl.Text = "Nem";
            this.RdBtnFájlNemTöröl.UseVisualStyleBackColor = true;
            // 
            // RdBtnFájlTöröl
            // 
            this.RdBtnFájlTöröl.AutoSize = true;
            this.RdBtnFájlTöröl.Checked = true;
            this.RdBtnFájlTöröl.Location = new System.Drawing.Point(13, 23);
            this.RdBtnFájlTöröl.Name = "RdBtnFájlTöröl";
            this.RdBtnFájlTöröl.Size = new System.Drawing.Size(59, 24);
            this.RdBtnFájlTöröl.TabIndex = 0;
            this.RdBtnFájlTöröl.TabStop = true;
            this.RdBtnFájlTöröl.Text = "Igen";
            this.RdBtnFájlTöröl.UseVisualStyleBackColor = true;
            // 
            // GroupBox4
            // 
            this.GroupBox4.Controls.Add(this.RdBtnSzakszolgálatVezető);
            this.GroupBox4.Controls.Add(this.RdBtnÜzemvezető);
            this.GroupBox4.Location = new System.Drawing.Point(556, 293);
            this.GroupBox4.Name = "GroupBox4";
            this.GroupBox4.Size = new System.Drawing.Size(224, 94);
            this.GroupBox4.TabIndex = 5;
            this.GroupBox4.TabStop = false;
            this.GroupBox4.Text = "Igazoló aláíró";
            // 
            // RdBtnSzakszolgálatVezető
            // 
            this.RdBtnSzakszolgálatVezető.AutoSize = true;
            this.RdBtnSzakszolgálatVezető.Location = new System.Drawing.Point(13, 53);
            this.RdBtnSzakszolgálatVezető.Name = "RdBtnSzakszolgálatVezető";
            this.RdBtnSzakszolgálatVezető.Size = new System.Drawing.Size(178, 24);
            this.RdBtnSzakszolgálatVezető.TabIndex = 1;
            this.RdBtnSzakszolgálatVezető.Text = "Szakszolgálat-vezető";
            this.RdBtnSzakszolgálatVezető.UseVisualStyleBackColor = true;
            // 
            // RdBtnÜzemvezető
            // 
            this.RdBtnÜzemvezető.AutoSize = true;
            this.RdBtnÜzemvezető.Checked = true;
            this.RdBtnÜzemvezető.Location = new System.Drawing.Point(13, 23);
            this.RdBtnÜzemvezető.Name = "RdBtnÜzemvezető";
            this.RdBtnÜzemvezető.Size = new System.Drawing.Size(116, 24);
            this.RdBtnÜzemvezető.TabIndex = 0;
            this.RdBtnÜzemvezető.TabStop = true;
            this.RdBtnÜzemvezető.Text = "Üzemvezető";
            this.RdBtnÜzemvezető.UseVisualStyleBackColor = true;
            // 
            // GroupBox5
            // 
            this.GroupBox5.Controls.Add(this.Option21);
            this.GroupBox5.Controls.Add(this.RdBtnA4);
            this.GroupBox5.Location = new System.Drawing.Point(556, 397);
            this.GroupBox5.Name = "GroupBox5";
            this.GroupBox5.Size = new System.Drawing.Size(224, 94);
            this.GroupBox5.TabIndex = 5;
            this.GroupBox5.TabStop = false;
            this.GroupBox5.Text = "Kiviteli formátum";
            // 
            // Option21
            // 
            this.Option21.AutoSize = true;
            this.Option21.Location = new System.Drawing.Point(13, 53);
            this.Option21.Name = "Option21";
            this.Option21.Size = new System.Drawing.Size(47, 24);
            this.Option21.TabIndex = 1;
            this.Option21.Text = "A3";
            this.Option21.UseVisualStyleBackColor = true;
            // 
            // RdBtnA4
            // 
            this.RdBtnA4.AutoSize = true;
            this.RdBtnA4.Checked = true;
            this.RdBtnA4.Location = new System.Drawing.Point(13, 23);
            this.RdBtnA4.Name = "RdBtnA4";
            this.RdBtnA4.Size = new System.Drawing.Size(47, 24);
            this.RdBtnA4.TabIndex = 0;
            this.RdBtnA4.TabStop = true;
            this.RdBtnA4.Text = "A4";
            this.RdBtnA4.UseVisualStyleBackColor = true;
            // 
            // Dátum
            // 
            this.Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum.Location = new System.Drawing.Point(556, 46);
            this.Dátum.Name = "Dátum";
            this.Dátum.Size = new System.Drawing.Size(118, 26);
            this.Dátum.TabIndex = 38;
            // 
            // GroupBox6
            // 
            this.GroupBox6.Controls.Add(this.Éjszakás);
            this.GroupBox6.Controls.Add(this.Btn_Heti);
            this.GroupBox6.Controls.Add(this.RdBtn7Napos);
            this.GroupBox6.Controls.Add(this.RdBtn6Napos);
            this.GroupBox6.Controls.Add(this.RdBtn5Napos);
            this.GroupBox6.Location = new System.Drawing.Point(786, 80);
            this.GroupBox6.Name = "GroupBox6";
            this.GroupBox6.Size = new System.Drawing.Size(185, 166);
            this.GroupBox6.TabIndex = 39;
            this.GroupBox6.TabStop = false;
            this.GroupBox6.Text = "Egy hetes jelenléti";
            // 
            // Éjszakás
            // 
            this.Éjszakás.AutoSize = true;
            this.Éjszakás.Location = new System.Drawing.Point(13, 136);
            this.Éjszakás.Name = "Éjszakás";
            this.Éjszakás.Size = new System.Drawing.Size(92, 24);
            this.Éjszakás.TabIndex = 37;
            this.Éjszakás.Text = "Éjszakás";
            this.Éjszakás.UseVisualStyleBackColor = true;
            // 
            // Btn_Heti
            // 
            this.Btn_Heti.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Btn_Heti.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Heti.Location = new System.Drawing.Point(134, 23);
            this.Btn_Heti.Name = "Btn_Heti";
            this.Btn_Heti.Size = new System.Drawing.Size(45, 45);
            this.Btn_Heti.TabIndex = 36;
            this.ToolTip1.SetToolTip(this.Btn_Heti, "Jelenléti ív elkészítése");
            this.Btn_Heti.UseVisualStyleBackColor = true;
            this.Btn_Heti.Click += new System.EventHandler(this.Btn_Heti_Click);
            // 
            // RdBtn7Napos
            // 
            this.RdBtn7Napos.AutoSize = true;
            this.RdBtn7Napos.Location = new System.Drawing.Point(13, 83);
            this.RdBtn7Napos.Name = "RdBtn7Napos";
            this.RdBtn7Napos.Size = new System.Drawing.Size(117, 24);
            this.RdBtn7Napos.TabIndex = 2;
            this.RdBtn7Napos.Text = "Heti 7 napos";
            this.RdBtn7Napos.UseVisualStyleBackColor = true;
            // 
            // RdBtn6Napos
            // 
            this.RdBtn6Napos.AutoSize = true;
            this.RdBtn6Napos.Location = new System.Drawing.Point(13, 53);
            this.RdBtn6Napos.Name = "RdBtn6Napos";
            this.RdBtn6Napos.Size = new System.Drawing.Size(117, 24);
            this.RdBtn6Napos.TabIndex = 1;
            this.RdBtn6Napos.Text = "Heti 6 napos";
            this.RdBtn6Napos.UseVisualStyleBackColor = true;
            // 
            // RdBtn5Napos
            // 
            this.RdBtn5Napos.AutoSize = true;
            this.RdBtn5Napos.Checked = true;
            this.RdBtn5Napos.Location = new System.Drawing.Point(13, 23);
            this.RdBtn5Napos.Name = "RdBtn5Napos";
            this.RdBtn5Napos.Size = new System.Drawing.Size(117, 24);
            this.RdBtn5Napos.TabIndex = 0;
            this.RdBtn5Napos.TabStop = true;
            this.RdBtn5Napos.Text = "Heti 5 napos";
            this.RdBtn5Napos.UseVisualStyleBackColor = true;
            // 
            // GroupBox7
            // 
            this.GroupBox7.Controls.Add(this.ChckBxHVasárnap);
            this.GroupBox7.Controls.Add(this.ChckBxHSzombat);
            this.GroupBox7.Controls.Add(this.ChckBxHPéntek);
            this.GroupBox7.Controls.Add(this.ChckBxHCsütörtök);
            this.GroupBox7.Controls.Add(this.ChckBxHSzerda);
            this.GroupBox7.Controls.Add(this.ChckBxHKedd);
            this.GroupBox7.Controls.Add(this.ChckBxHétfő);
            this.GroupBox7.Controls.Add(this.Btn_Szellemi);
            this.GroupBox7.Location = new System.Drawing.Point(786, 252);
            this.GroupBox7.Name = "GroupBox7";
            this.GroupBox7.Size = new System.Drawing.Size(185, 241);
            this.GroupBox7.TabIndex = 40;
            this.GroupBox7.TabStop = false;
            this.GroupBox7.Text = "Szellemi jelenléti";
            // 
            // ChckBxHVasárnap
            // 
            this.ChckBxHVasárnap.AutoSize = true;
            this.ChckBxHVasárnap.Checked = true;
            this.ChckBxHVasárnap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChckBxHVasárnap.Location = new System.Drawing.Point(13, 205);
            this.ChckBxHVasárnap.Name = "ChckBxHVasárnap";
            this.ChckBxHVasárnap.Size = new System.Drawing.Size(97, 24);
            this.ChckBxHVasárnap.TabIndex = 43;
            this.ChckBxHVasárnap.Text = "Vasárnap";
            this.ChckBxHVasárnap.UseVisualStyleBackColor = true;
            // 
            // ChckBxHSzombat
            // 
            this.ChckBxHSzombat.AutoSize = true;
            this.ChckBxHSzombat.Checked = true;
            this.ChckBxHSzombat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChckBxHSzombat.Location = new System.Drawing.Point(13, 175);
            this.ChckBxHSzombat.Name = "ChckBxHSzombat";
            this.ChckBxHSzombat.Size = new System.Drawing.Size(92, 24);
            this.ChckBxHSzombat.TabIndex = 42;
            this.ChckBxHSzombat.Text = "Szombat";
            this.ChckBxHSzombat.UseVisualStyleBackColor = true;
            // 
            // ChckBxHPéntek
            // 
            this.ChckBxHPéntek.AutoSize = true;
            this.ChckBxHPéntek.Location = new System.Drawing.Point(13, 145);
            this.ChckBxHPéntek.Name = "ChckBxHPéntek";
            this.ChckBxHPéntek.Size = new System.Drawing.Size(78, 24);
            this.ChckBxHPéntek.TabIndex = 41;
            this.ChckBxHPéntek.Text = "Péntek";
            this.ChckBxHPéntek.UseVisualStyleBackColor = true;
            // 
            // ChckBxHCsütörtök
            // 
            this.ChckBxHCsütörtök.AutoSize = true;
            this.ChckBxHCsütörtök.Location = new System.Drawing.Point(13, 115);
            this.ChckBxHCsütörtök.Name = "ChckBxHCsütörtök";
            this.ChckBxHCsütörtök.Size = new System.Drawing.Size(97, 24);
            this.ChckBxHCsütörtök.TabIndex = 40;
            this.ChckBxHCsütörtök.Text = "Csütörtök";
            this.ChckBxHCsütörtök.UseVisualStyleBackColor = true;
            // 
            // ChckBxHSzerda
            // 
            this.ChckBxHSzerda.AutoSize = true;
            this.ChckBxHSzerda.Location = new System.Drawing.Point(13, 85);
            this.ChckBxHSzerda.Name = "ChckBxHSzerda";
            this.ChckBxHSzerda.Size = new System.Drawing.Size(79, 24);
            this.ChckBxHSzerda.TabIndex = 39;
            this.ChckBxHSzerda.Text = "Szerda";
            this.ChckBxHSzerda.UseVisualStyleBackColor = true;
            // 
            // ChckBxHKedd
            // 
            this.ChckBxHKedd.AutoSize = true;
            this.ChckBxHKedd.Location = new System.Drawing.Point(13, 55);
            this.ChckBxHKedd.Name = "ChckBxHKedd";
            this.ChckBxHKedd.Size = new System.Drawing.Size(65, 24);
            this.ChckBxHKedd.TabIndex = 38;
            this.ChckBxHKedd.Text = "Kedd";
            this.ChckBxHKedd.UseVisualStyleBackColor = true;
            // 
            // ChckBxHétfő
            // 
            this.ChckBxHétfő.AutoSize = true;
            this.ChckBxHétfő.Location = new System.Drawing.Point(13, 25);
            this.ChckBxHétfő.Name = "ChckBxHétfő";
            this.ChckBxHétfő.Size = new System.Drawing.Size(68, 24);
            this.ChckBxHétfő.TabIndex = 37;
            this.ChckBxHétfő.Text = "Hétfő";
            this.ChckBxHétfő.UseVisualStyleBackColor = true;
            // 
            // Btn_Szellemi
            // 
            this.Btn_Szellemi.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Btn_Szellemi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Szellemi.Location = new System.Drawing.Point(134, 25);
            this.Btn_Szellemi.Name = "Btn_Szellemi";
            this.Btn_Szellemi.Size = new System.Drawing.Size(45, 45);
            this.Btn_Szellemi.TabIndex = 36;
            this.ToolTip1.SetToolTip(this.Btn_Szellemi, "Jelenléti ív elkészítése");
            this.Btn_Szellemi.UseVisualStyleBackColor = true;
            this.Btn_Szellemi.Click += new System.EventHandler(this.Btn_Szellemi_Click);
            // 
            // GroupBox9
            // 
            this.GroupBox9.Controls.Add(this.Btn_Váltós);
            this.GroupBox9.Location = new System.Drawing.Point(984, 80);
            this.GroupBox9.Name = "GroupBox9";
            this.GroupBox9.Size = new System.Drawing.Size(250, 80);
            this.GroupBox9.TabIndex = 41;
            this.GroupBox9.TabStop = false;
            this.GroupBox9.Text = "Váltós jelenléti";
            // 
            // Btn_Váltós
            // 
            this.Btn_Váltós.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Btn_Váltós.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Váltós.Location = new System.Drawing.Point(199, 23);
            this.Btn_Váltós.Name = "Btn_Váltós";
            this.Btn_Váltós.Size = new System.Drawing.Size(45, 45);
            this.Btn_Váltós.TabIndex = 36;
            this.ToolTip1.SetToolTip(this.Btn_Váltós, "Jelenléti ív elkészítése");
            this.Btn_Váltós.UseVisualStyleBackColor = true;
            this.Btn_Váltós.Click += new System.EventHandler(this.Btn_Váltós_Click);
            // 
            // BtnKijelölésátjelöl
            // 
            this.BtnKijelölésátjelöl.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.BtnKijelölésátjelöl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnKijelölésátjelöl.Location = new System.Drawing.Point(492, 46);
            this.BtnKijelölésátjelöl.Name = "BtnKijelölésátjelöl";
            this.BtnKijelölésátjelöl.Size = new System.Drawing.Size(45, 45);
            this.BtnKijelölésátjelöl.TabIndex = 43;
            this.ToolTip1.SetToolTip(this.BtnKijelölésátjelöl, "Kijelölés alapján listázás");
            this.BtnKijelölésátjelöl.UseVisualStyleBackColor = true;
            this.BtnKijelölésátjelöl.Click += new System.EventHandler(this.BtnKijelölésátjelöl_Click);
            // 
            // Btnkilelöltörlés
            // 
            this.Btnkilelöltörlés.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.Btnkilelöltörlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btnkilelöltörlés.Location = new System.Drawing.Point(441, 46);
            this.Btnkilelöltörlés.Name = "Btnkilelöltörlés";
            this.Btnkilelöltörlés.Size = new System.Drawing.Size(45, 45);
            this.Btnkilelöltörlés.TabIndex = 37;
            this.ToolTip1.SetToolTip(this.Btnkilelöltörlés, "Kijelölések törlése");
            this.Btnkilelöltörlés.UseVisualStyleBackColor = true;
            this.Btnkilelöltörlés.Click += new System.EventHandler(this.Btnkilelöltörlés_Click);
            // 
            // Btnkijelöléstöröl
            // 
            this.Btnkijelöléstöröl.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.Btnkijelöléstöröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btnkijelöléstöröl.Location = new System.Drawing.Point(441, 224);
            this.Btnkijelöléstöröl.Name = "Btnkijelöléstöröl";
            this.Btnkijelöléstöröl.Size = new System.Drawing.Size(45, 45);
            this.Btnkijelöléstöröl.TabIndex = 35;
            this.ToolTip1.SetToolTip(this.Btnkijelöléstöröl, "Kijelölések törlése");
            this.Btnkijelöléstöröl.UseVisualStyleBackColor = true;
            this.Btnkijelöléstöröl.Click += new System.EventHandler(this.Btnkijelöléstöröl_Click);
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.Cmbtelephely);
            this.Panel1.Controls.Add(this.Label13);
            this.Panel1.Location = new System.Drawing.Point(10, 12);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(373, 26);
            this.Panel1.TabIndex = 44;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(174, 0);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
            this.ToolTip1.SetToolTip(this.Cmbtelephely, "Telephely választó");
            this.Cmbtelephely.SelectedIndexChanged += new System.EventHandler(this.Cmbtelephely_SelectedIndexChanged);
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(3, 4);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(145, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // Súgó
            // 
            this.Súgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Súgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.Súgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Súgó.Location = new System.Drawing.Point(1186, 5);
            this.Súgó.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Súgó.Name = "Súgó";
            this.Súgó.Size = new System.Drawing.Size(48, 48);
            this.Súgó.TabIndex = 57;
            this.ToolTip1.SetToolTip(this.Súgó, "Súgó");
            this.Súgó.UseVisualStyleBackColor = true;
            this.Súgó.Click += new System.EventHandler(this.Btn_Súgó_Click);
            // 
            // GroupBox8
            // 
            this.GroupBox8.Controls.Add(this.Heti_ittas);
            this.GroupBox8.Controls.Add(this.Napi_ittas);
            this.GroupBox8.Controls.Add(this.Btn_Kiválogat);
            this.GroupBox8.Controls.Add(this.Btn_Ittasság);
            this.GroupBox8.Location = new System.Drawing.Point(984, 166);
            this.GroupBox8.Name = "GroupBox8";
            this.GroupBox8.Size = new System.Drawing.Size(250, 121);
            this.GroupBox8.TabIndex = 58;
            this.GroupBox8.TabStop = false;
            this.GroupBox8.Text = "Ittaság-vizsgálati";
            // 
            // Heti_ittas
            // 
            this.Heti_ittas.AutoSize = true;
            this.Heti_ittas.Location = new System.Drawing.Point(15, 58);
            this.Heti_ittas.Name = "Heti_ittas";
            this.Heti_ittas.Size = new System.Drawing.Size(56, 24);
            this.Heti_ittas.TabIndex = 170;
            this.Heti_ittas.Text = "Heti";
            this.Heti_ittas.UseVisualStyleBackColor = true;
            // 
            // Napi_ittas
            // 
            this.Napi_ittas.AutoSize = true;
            this.Napi_ittas.Checked = true;
            this.Napi_ittas.Location = new System.Drawing.Point(15, 28);
            this.Napi_ittas.Name = "Napi_ittas";
            this.Napi_ittas.Size = new System.Drawing.Size(59, 24);
            this.Napi_ittas.TabIndex = 169;
            this.Napi_ittas.TabStop = true;
            this.Napi_ittas.Text = "Napi";
            this.Napi_ittas.UseVisualStyleBackColor = true;
            // 
            // Btn_Kiválogat
            // 
            this.Btn_Kiválogat.BackgroundImage = global::Villamos.Properties.Resources.felhasználók32;
            this.Btn_Kiválogat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Kiválogat.Location = new System.Drawing.Point(199, 16);
            this.Btn_Kiválogat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Kiválogat.Name = "Btn_Kiválogat";
            this.Btn_Kiválogat.Size = new System.Drawing.Size(45, 45);
            this.Btn_Kiválogat.TabIndex = 168;
            this.ToolTip1.SetToolTip(this.Btn_Kiválogat, "Kiválogatja az embereket");
            this.Btn_Kiválogat.UseVisualStyleBackColor = true;
            this.Btn_Kiválogat.Click += new System.EventHandler(this.Btn_Kiválogat_Click);
            // 
            // Btn_Ittasság
            // 
            this.Btn_Ittasság.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Btn_Ittasság.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Ittasság.Location = new System.Drawing.Point(199, 67);
            this.Btn_Ittasság.Name = "Btn_Ittasság";
            this.Btn_Ittasság.Size = new System.Drawing.Size(45, 45);
            this.Btn_Ittasság.TabIndex = 36;
            this.ToolTip1.SetToolTip(this.Btn_Ittasság, "Ittasság-vitsgálati Jelenléti ív elkészítése");
            this.Btn_Ittasság.UseVisualStyleBackColor = true;
            this.Btn_Ittasság.Click += new System.EventHandler(this.Btn_Ittasság_Click);
            // 
            // ToolTip1
            // 
            this.ToolTip1.IsBalloon = true;
            // 
            // Btnmindkijelöl
            // 
            this.Btnmindkijelöl.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.Btnmindkijelöl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btnmindkijelöl.Location = new System.Drawing.Point(390, 224);
            this.Btnmindkijelöl.Name = "Btnmindkijelöl";
            this.Btnmindkijelöl.Size = new System.Drawing.Size(45, 45);
            this.Btnmindkijelöl.TabIndex = 36;
            this.ToolTip1.SetToolTip(this.Btnmindkijelöl, "Mindent kijelöl");
            this.Btnmindkijelöl.UseVisualStyleBackColor = true;
            this.Btnmindkijelöl.Click += new System.EventHandler(this.Btnmindkijelöl_Click);
            // 
            // BtnKijelölcsop
            // 
            this.BtnKijelölcsop.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.BtnKijelölcsop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnKijelölcsop.Location = new System.Drawing.Point(390, 46);
            this.BtnKijelölcsop.Name = "BtnKijelölcsop";
            this.BtnKijelölcsop.Size = new System.Drawing.Size(45, 45);
            this.BtnKijelölcsop.TabIndex = 34;
            this.ToolTip1.SetToolTip(this.BtnKijelölcsop, "Mindent kijelöl");
            this.BtnKijelölcsop.UseVisualStyleBackColor = true;
            this.BtnKijelölcsop.Click += new System.EventHandler(this.BtnKijelölcsop_Click);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(390, 13);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(789, 27);
            this.Holtart.TabIndex = 59;
            this.Holtart.Visible = false;
            // 
            // Ablak_Jelenléti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSalmon;
            this.ClientSize = new System.Drawing.Size(1246, 692);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.GroupBox8);
            this.Controls.Add(this.Súgó);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.BtnKijelölésátjelöl);
            this.Controls.Add(this.GroupBox9);
            this.Controls.Add(this.GroupBox7);
            this.Controls.Add(this.GroupBox6);
            this.Controls.Add(this.Dátum);
            this.Controls.Add(this.Btnkilelöltörlés);
            this.Controls.Add(this.Btnmindkijelöl);
            this.Controls.Add(this.Btnkijelöléstöröl);
            this.Controls.Add(this.BtnKijelölcsop);
            this.Controls.Add(this.GroupBox5);
            this.Controls.Add(this.GroupBox4);
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.ChkDolgozónév);
            this.Controls.Add(this.ChkCsoport);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Jelenléti";
            this.Text = "Jelenlétiív készítés";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AblakJelenléti_Load);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox3.PerformLayout();
            this.GroupBox4.ResumeLayout(false);
            this.GroupBox4.PerformLayout();
            this.GroupBox5.ResumeLayout(false);
            this.GroupBox5.PerformLayout();
            this.GroupBox6.ResumeLayout(false);
            this.GroupBox6.PerformLayout();
            this.GroupBox7.ResumeLayout(false);
            this.GroupBox7.PerformLayout();
            this.GroupBox9.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.GroupBox8.ResumeLayout(false);
            this.GroupBox8.PerformLayout();
            this.ResumeLayout(false);

        }

        internal CheckedListBox ChkCsoport;
        internal CheckedListBox ChkDolgozónév;
        internal GroupBox GroupBox1;
        internal GroupBox GroupBox2;
        internal RadioButton RdBtnNemNyomtat;
        internal RadioButton RdBtnNyomtat;
        internal GroupBox GroupBox3;
        internal RadioButton RdBtnFájlNemTöröl;
        internal RadioButton RdBtnFájlTöröl;
        internal GroupBox GroupBox4;
        internal RadioButton RdBtnSzakszolgálatVezető;
        internal RadioButton RdBtnÜzemvezető;
        internal GroupBox GroupBox5;
        internal RadioButton Option21;
        internal RadioButton RdBtnA4;
        internal Button BtnKijelölcsop;
        internal Button Btnkijelöléstöröl;
        internal Button Btnmindkijelöl;
        internal Button Btnkilelöltörlés;
        internal DateTimePicker Dátum;
        internal GroupBox GroupBox6;
        internal CheckBox Éjszakás;
        internal Button Btn_Heti;
        internal RadioButton RdBtn7Napos;
        internal RadioButton RdBtn6Napos;
        internal RadioButton RdBtn5Napos;
        internal GroupBox GroupBox7;
        internal CheckBox ChckBxHVasárnap;
        internal CheckBox ChckBxHSzombat;
        internal CheckBox ChckBxHPéntek;
        internal CheckBox ChckBxHCsütörtök;
        internal CheckBox ChckBxHSzerda;
        internal CheckBox ChckBxHKedd;
        internal CheckBox ChckBxHétfő;
        internal Button Btn_Szellemi;
        internal GroupBox GroupBox9;
        internal Button Btn_Váltós;
        internal Button BtnKijelölésátjelöl;
        internal Panel Panel1;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal ComboBox LstKiadta;
        internal Button Súgó;
        internal GroupBox GroupBox8;
        internal Button Btn_Ittasság;
        internal Button Btn_Kiválogat;
        internal ToolTip ToolTip1;
        internal RadioButton Heti_ittas;
        internal RadioButton Napi_ittas;
        private V_MindenEgyéb.MyProgressbar Holtart;
    }
}