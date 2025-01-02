using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
    
    public partial class Ablak_T5C5_fűtés : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_T5C5_fűtés));
            this.Fülek = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Label2 = new System.Windows.Forms.Label();
            this.CheckBox1 = new System.Windows.Forms.CheckBox();
            this.CheckBox2 = new System.Windows.Forms.CheckBox();
            this.Btnkilelöltörlés = new System.Windows.Forms.Button();
            this.CheckBox3 = new System.Windows.Forms.CheckBox();
            this.BtnKijelölcsop = new System.Windows.Forms.Button();
            this.CheckBox4 = new System.Windows.Forms.CheckBox();
            this.CheckBox5 = new System.Windows.Forms.CheckBox();
            this.CheckBox6 = new System.Windows.Forms.CheckBox();
            this.CheckBox7 = new System.Windows.Forms.CheckBox();
            this.CheckBox8 = new System.Windows.Forms.CheckBox();
            this.CheckBox9 = new System.Windows.Forms.CheckBox();
            this.CheckBox10 = new System.Windows.Forms.CheckBox();
            this.CheckBox11 = new System.Windows.Forms.CheckBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.CheckBox12 = new System.Windows.Forms.CheckBox();
            this.CheckBox13 = new System.Windows.Forms.CheckBox();
            this.CheckBox14 = new System.Windows.Forms.CheckBox();
            this.CheckBox15 = new System.Windows.Forms.CheckBox();
            this.CheckBox16 = new System.Windows.Forms.CheckBox();
            this.CheckBox17 = new System.Windows.Forms.CheckBox();
            this.CheckBox18 = new System.Windows.Forms.CheckBox();
            this.CheckBox19 = new System.Windows.Forms.CheckBox();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.Beállítási_értékek = new System.Windows.Forms.CheckBox();
            this.Új_elem = new System.Windows.Forms.Button();
            this.Rögzít = new System.Windows.Forms.Button();
            this.Megjegyzés = new System.Windows.Forms.TextBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.I_szakasz = new System.Windows.Forms.TextBox();
            this.II_szakasz = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Dolgozó = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Dátum = new System.Windows.Forms.DateTimePicker();
            this.Label16 = new System.Windows.Forms.Label();
            this.Button1 = new System.Windows.Forms.Button();
            this.Label15 = new System.Windows.Forms.Label();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.RadioButton3 = new System.Windows.Forms.RadioButton();
            this.RadioButton2 = new System.Windows.Forms.RadioButton();
            this.RadioButton1 = new System.Windows.Forms.RadioButton();
            this.Pályaszám = new System.Windows.Forms.ComboBox();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.Lekérdezés_minden = new System.Windows.Forms.CheckBox();
            this.PSZ_hiány = new System.Windows.Forms.Button();
            this.Kimutatás_készítés = new System.Windows.Forms.Button();
            this.BtnExcelkimenet = new System.Windows.Forms.Button();
            this.Dátum_év = new System.Windows.Forms.DateTimePicker();
            this.Label8 = new System.Windows.Forms.Label();
            this.Lekérdezés_Tábla = new System.Windows.Forms.DataGridView();
            this.Lekérdezés = new System.Windows.Forms.Button();
            this.Holtart = new V_MindenEgyéb.MyProgressbar();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Fülek.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            this.GroupBox1.SuspendLayout();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Lekérdezés_Tábla)).BeginInit();
            this.Panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Fülek
            // 
            this.Fülek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fülek.Controls.Add(this.TabPage1);
            this.Fülek.Controls.Add(this.TabPage2);
            this.Fülek.Location = new System.Drawing.Point(5, 55);
            this.Fülek.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Fülek.Name = "Fülek";
            this.Fülek.SelectedIndex = 0;
            this.Fülek.Size = new System.Drawing.Size(1322, 673);
            this.Fülek.TabIndex = 0;
            this.Fülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Fülek_DrawItem);
            this.Fülek.SelectedIndexChanged += new System.EventHandler(this.Fülek_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.ForestGreen;
            this.TabPage1.Controls.Add(this.panel1);
            this.TabPage1.Controls.Add(this.PictureBox2);
            this.TabPage1.Controls.Add(this.Beállítási_értékek);
            this.TabPage1.Controls.Add(this.Új_elem);
            this.TabPage1.Controls.Add(this.Rögzít);
            this.TabPage1.Controls.Add(this.Megjegyzés);
            this.TabPage1.Controls.Add(this.Label7);
            this.TabPage1.Controls.Add(this.I_szakasz);
            this.TabPage1.Controls.Add(this.II_szakasz);
            this.TabPage1.Controls.Add(this.Label5);
            this.TabPage1.Controls.Add(this.Label4);
            this.TabPage1.Controls.Add(this.Dolgozó);
            this.TabPage1.Controls.Add(this.Label1);
            this.TabPage1.Controls.Add(this.Dátum);
            this.TabPage1.Controls.Add(this.Label16);
            this.TabPage1.Controls.Add(this.Button1);
            this.TabPage1.Controls.Add(this.Label15);
            this.TabPage1.Controls.Add(this.GroupBox1);
            this.TabPage1.Controls.Add(this.Pályaszám);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TabPage1.Size = new System.Drawing.Size(1314, 640);
            this.TabPage1.TabIndex = 2;
            this.TabPage1.Text = "Mérés rögzítés";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Label2);
            this.panel1.Controls.Add(this.CheckBox1);
            this.panel1.Controls.Add(this.CheckBox2);
            this.panel1.Controls.Add(this.Btnkilelöltörlés);
            this.panel1.Controls.Add(this.CheckBox3);
            this.panel1.Controls.Add(this.BtnKijelölcsop);
            this.panel1.Controls.Add(this.CheckBox4);
            this.panel1.Controls.Add(this.CheckBox5);
            this.panel1.Controls.Add(this.CheckBox6);
            this.panel1.Controls.Add(this.CheckBox7);
            this.panel1.Controls.Add(this.CheckBox8);
            this.panel1.Controls.Add(this.CheckBox9);
            this.panel1.Controls.Add(this.CheckBox10);
            this.panel1.Controls.Add(this.CheckBox11);
            this.panel1.Controls.Add(this.Label3);
            this.panel1.Controls.Add(this.CheckBox12);
            this.panel1.Controls.Add(this.CheckBox13);
            this.panel1.Controls.Add(this.CheckBox14);
            this.panel1.Controls.Add(this.CheckBox15);
            this.panel1.Controls.Add(this.CheckBox16);
            this.panel1.Controls.Add(this.CheckBox17);
            this.panel1.Controls.Add(this.CheckBox18);
            this.panel1.Controls.Add(this.CheckBox19);
            this.panel1.Controls.Add(this.PictureBox1);
            this.panel1.Location = new System.Drawing.Point(7, 186);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1300, 446);
            this.panel1.TabIndex = 134;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Location = new System.Drawing.Point(11, 12);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(167, 20);
            this.Label2.TabIndex = 122;
            this.Label2.Text = "Tapintásos ellenőrzés:";
            // 
            // CheckBox1
            // 
            this.CheckBox1.AutoSize = true;
            this.CheckBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.CheckBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CheckBox1.Location = new System.Drawing.Point(982, 12);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(48, 35);
            this.CheckBox1.TabIndex = 10;
            this.CheckBox1.Text = "1";
            this.CheckBox1.UseVisualStyleBackColor = false;
            // 
            // CheckBox2
            // 
            this.CheckBox2.AutoSize = true;
            this.CheckBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.CheckBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CheckBox2.Location = new System.Drawing.Point(898, 12);
            this.CheckBox2.Name = "CheckBox2";
            this.CheckBox2.Size = new System.Drawing.Size(48, 35);
            this.CheckBox2.TabIndex = 11;
            this.CheckBox2.Text = "2";
            this.CheckBox2.UseVisualStyleBackColor = false;
            // 
            // Btnkilelöltörlés
            // 
            this.Btnkilelöltörlés.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.Btnkilelöltörlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btnkilelöltörlés.Location = new System.Drawing.Point(1243, 6);
            this.Btnkilelöltörlés.Name = "Btnkilelöltörlés";
            this.Btnkilelöltörlés.Size = new System.Drawing.Size(40, 40);
            this.Btnkilelöltörlés.TabIndex = 9;
            this.ToolTip1.SetToolTip(this.Btnkilelöltörlés, "Összes fűtési hely törlése");
            this.Btnkilelöltörlés.UseVisualStyleBackColor = true;
            this.Btnkilelöltörlés.Click += new System.EventHandler(this.Btnkilelöltörlés_Click);
            // 
            // CheckBox3
            // 
            this.CheckBox3.AutoSize = true;
            this.CheckBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.CheckBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CheckBox3.Location = new System.Drawing.Point(808, 12);
            this.CheckBox3.Name = "CheckBox3";
            this.CheckBox3.Size = new System.Drawing.Size(48, 35);
            this.CheckBox3.TabIndex = 12;
            this.CheckBox3.Text = "3";
            this.CheckBox3.UseVisualStyleBackColor = false;
            // 
            // BtnKijelölcsop
            // 
            this.BtnKijelölcsop.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.BtnKijelölcsop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnKijelölcsop.Location = new System.Drawing.Point(1192, 6);
            this.BtnKijelölcsop.Name = "BtnKijelölcsop";
            this.BtnKijelölcsop.Size = new System.Drawing.Size(40, 40);
            this.BtnKijelölcsop.TabIndex = 7;
            this.ToolTip1.SetToolTip(this.BtnKijelölcsop, "Összes fűtési hely kiválasztása");
            this.BtnKijelölcsop.UseVisualStyleBackColor = true;
            this.BtnKijelölcsop.Click += new System.EventHandler(this.BtnKijelölcsop_Click);
            // 
            // CheckBox4
            // 
            this.CheckBox4.AutoSize = true;
            this.CheckBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.CheckBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CheckBox4.Location = new System.Drawing.Point(720, 12);
            this.CheckBox4.Name = "CheckBox4";
            this.CheckBox4.Size = new System.Drawing.Size(48, 35);
            this.CheckBox4.TabIndex = 13;
            this.CheckBox4.Text = "4";
            this.CheckBox4.UseVisualStyleBackColor = false;
            // 
            // CheckBox5
            // 
            this.CheckBox5.AutoSize = true;
            this.CheckBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.CheckBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CheckBox5.Location = new System.Drawing.Point(504, 12);
            this.CheckBox5.Name = "CheckBox5";
            this.CheckBox5.Size = new System.Drawing.Size(48, 35);
            this.CheckBox5.TabIndex = 14;
            this.CheckBox5.Text = "5";
            this.CheckBox5.UseVisualStyleBackColor = false;
            // 
            // CheckBox6
            // 
            this.CheckBox6.AutoSize = true;
            this.CheckBox6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.CheckBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CheckBox6.Location = new System.Drawing.Point(390, 12);
            this.CheckBox6.Name = "CheckBox6";
            this.CheckBox6.Size = new System.Drawing.Size(48, 35);
            this.CheckBox6.TabIndex = 15;
            this.CheckBox6.Text = "6";
            this.CheckBox6.UseVisualStyleBackColor = false;
            // 
            // CheckBox7
            // 
            this.CheckBox7.AutoSize = true;
            this.CheckBox7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.CheckBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CheckBox7.Location = new System.Drawing.Point(263, 12);
            this.CheckBox7.Name = "CheckBox7";
            this.CheckBox7.Size = new System.Drawing.Size(48, 35);
            this.CheckBox7.TabIndex = 16;
            this.CheckBox7.Text = "7";
            this.CheckBox7.UseVisualStyleBackColor = false;
            // 
            // CheckBox8
            // 
            this.CheckBox8.AutoSize = true;
            this.CheckBox8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.CheckBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CheckBox8.Location = new System.Drawing.Point(263, 400);
            this.CheckBox8.Name = "CheckBox8";
            this.CheckBox8.Size = new System.Drawing.Size(48, 35);
            this.CheckBox8.TabIndex = 17;
            this.CheckBox8.Text = "8";
            this.CheckBox8.UseVisualStyleBackColor = false;
            // 
            // CheckBox9
            // 
            this.CheckBox9.AutoSize = true;
            this.CheckBox9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.CheckBox9.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CheckBox9.Location = new System.Drawing.Point(339, 400);
            this.CheckBox9.Name = "CheckBox9";
            this.CheckBox9.Size = new System.Drawing.Size(48, 35);
            this.CheckBox9.TabIndex = 18;
            this.CheckBox9.Text = "9";
            this.CheckBox9.UseVisualStyleBackColor = false;
            // 
            // CheckBox10
            // 
            this.CheckBox10.AutoSize = true;
            this.CheckBox10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.CheckBox10.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CheckBox10.Location = new System.Drawing.Point(412, 400);
            this.CheckBox10.Name = "CheckBox10";
            this.CheckBox10.Size = new System.Drawing.Size(63, 35);
            this.CheckBox10.TabIndex = 19;
            this.CheckBox10.Text = "10";
            this.CheckBox10.UseVisualStyleBackColor = false;
            // 
            // CheckBox11
            // 
            this.CheckBox11.AutoSize = true;
            this.CheckBox11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.CheckBox11.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CheckBox11.Location = new System.Drawing.Point(509, 400);
            this.CheckBox11.Name = "CheckBox11";
            this.CheckBox11.Size = new System.Drawing.Size(63, 35);
            this.CheckBox11.TabIndex = 20;
            this.CheckBox11.Text = "11";
            this.CheckBox11.UseVisualStyleBackColor = false;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.Red;
            this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Label3.ForeColor = System.Drawing.Color.White;
            this.Label3.Location = new System.Drawing.Point(9, 203);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(419, 62);
            this.Label3.TabIndex = 123;
            this.Label3.Text = "A tapintás útján megfelelőnek ítélt\r\n fűtéseket kell kipipálni!!!";
            // 
            // CheckBox12
            // 
            this.CheckBox12.AutoSize = true;
            this.CheckBox12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.CheckBox12.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CheckBox12.Location = new System.Drawing.Point(725, 400);
            this.CheckBox12.Name = "CheckBox12";
            this.CheckBox12.Size = new System.Drawing.Size(63, 35);
            this.CheckBox12.TabIndex = 21;
            this.CheckBox12.Text = "12";
            this.CheckBox12.UseVisualStyleBackColor = false;
            // 
            // CheckBox13
            // 
            this.CheckBox13.AutoSize = true;
            this.CheckBox13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.CheckBox13.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CheckBox13.Location = new System.Drawing.Point(813, 400);
            this.CheckBox13.Name = "CheckBox13";
            this.CheckBox13.Size = new System.Drawing.Size(63, 35);
            this.CheckBox13.TabIndex = 22;
            this.CheckBox13.Text = "13";
            this.CheckBox13.UseVisualStyleBackColor = false;
            // 
            // CheckBox14
            // 
            this.CheckBox14.AutoSize = true;
            this.CheckBox14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.CheckBox14.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CheckBox14.Location = new System.Drawing.Point(903, 400);
            this.CheckBox14.Name = "CheckBox14";
            this.CheckBox14.Size = new System.Drawing.Size(63, 35);
            this.CheckBox14.TabIndex = 23;
            this.CheckBox14.Text = "14";
            this.CheckBox14.UseVisualStyleBackColor = false;
            // 
            // CheckBox15
            // 
            this.CheckBox15.AutoSize = true;
            this.CheckBox15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.CheckBox15.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CheckBox15.Location = new System.Drawing.Point(987, 400);
            this.CheckBox15.Name = "CheckBox15";
            this.CheckBox15.Size = new System.Drawing.Size(63, 35);
            this.CheckBox15.TabIndex = 24;
            this.CheckBox15.Text = "15";
            this.CheckBox15.UseVisualStyleBackColor = false;
            // 
            // CheckBox16
            // 
            this.CheckBox16.AutoSize = true;
            this.CheckBox16.BackColor = System.Drawing.Color.Green;
            this.CheckBox16.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CheckBox16.Location = new System.Drawing.Point(967, 153);
            this.CheckBox16.Name = "CheckBox16";
            this.CheckBox16.Size = new System.Drawing.Size(83, 35);
            this.CheckBox16.TabIndex = 25;
            this.CheckBox16.Text = "TP1";
            this.CheckBox16.UseVisualStyleBackColor = false;
            // 
            // CheckBox17
            // 
            this.CheckBox17.AutoSize = true;
            this.CheckBox17.BackColor = System.Drawing.Color.Green;
            this.CheckBox17.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CheckBox17.Location = new System.Drawing.Point(773, 272);
            this.CheckBox17.Name = "CheckBox17";
            this.CheckBox17.Size = new System.Drawing.Size(83, 35);
            this.CheckBox17.TabIndex = 26;
            this.CheckBox17.Text = "TP4";
            this.CheckBox17.UseVisualStyleBackColor = false;
            // 
            // CheckBox18
            // 
            this.CheckBox18.AutoSize = true;
            this.CheckBox18.BackColor = System.Drawing.Color.Green;
            this.CheckBox18.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CheckBox18.Location = new System.Drawing.Point(438, 272);
            this.CheckBox18.Name = "CheckBox18";
            this.CheckBox18.Size = new System.Drawing.Size(83, 35);
            this.CheckBox18.TabIndex = 27;
            this.CheckBox18.Text = "TP2";
            this.CheckBox18.UseVisualStyleBackColor = false;
            // 
            // CheckBox19
            // 
            this.CheckBox19.AutoSize = true;
            this.CheckBox19.BackColor = System.Drawing.Color.Green;
            this.CheckBox19.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CheckBox19.Location = new System.Drawing.Point(237, 153);
            this.CheckBox19.Name = "CheckBox19";
            this.CheckBox19.Size = new System.Drawing.Size(83, 35);
            this.CheckBox19.TabIndex = 28;
            this.CheckBox19.Text = "TP3";
            this.CheckBox19.UseVisualStyleBackColor = false;
            // 
            // PictureBox1
            // 
            this.PictureBox1.BackgroundImage = global::Villamos.Properties.Resources.T5C5_fűtés;
            this.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PictureBox1.Location = new System.Drawing.Point(5, 53);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(1292, 340);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox1.TabIndex = 0;
            this.PictureBox1.TabStop = false;
            // 
            // PictureBox2
            // 
            this.PictureBox2.BackColor = System.Drawing.Color.Yellow;
            this.PictureBox2.Location = new System.Drawing.Point(292, 8);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new System.Drawing.Size(38, 35);
            this.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox2.TabIndex = 133;
            this.PictureBox2.TabStop = false;
            this.PictureBox2.Visible = false;
            // 
            // Beállítási_értékek
            // 
            this.Beállítási_értékek.AutoSize = true;
            this.Beállítási_értékek.BackColor = System.Drawing.Color.LimeGreen;
            this.Beállítási_értékek.Location = new System.Drawing.Point(506, 115);
            this.Beállítási_értékek.Name = "Beállítási_értékek";
            this.Beállítási_értékek.Size = new System.Drawing.Size(172, 24);
            this.Beállítási_értékek.TabIndex = 5;
            this.Beállítási_értékek.Text = "Beállítási értékek jók";
            this.ToolTip1.SetToolTip(this.Beállítási_értékek, "Automata fűtés esetén a beállítási értékek megfelelnek az előírásnak");
            this.Beállítási_értékek.UseVisualStyleBackColor = false;
            this.Beállítási_értékek.MouseEnter += new System.EventHandler(this.Beállítási_értékek_MouseEnter);
            this.Beállítási_értékek.MouseLeave += new System.EventHandler(this.Beállítási_értékek_MouseLeave);
            // 
            // Új_elem
            // 
            this.Új_elem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Új_elem.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Új_elem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Új_elem.Location = new System.Drawing.Point(1205, 8);
            this.Új_elem.Name = "Új_elem";
            this.Új_elem.Size = new System.Drawing.Size(40, 40);
            this.Új_elem.TabIndex = 131;
            this.ToolTip1.SetToolTip(this.Új_elem, "Új adat rögzítéséhez üíti a mezők adatait");
            this.Új_elem.UseVisualStyleBackColor = true;
            this.Új_elem.Click += new System.EventHandler(this.Új_elem_Click);
            // 
            // Rögzít
            // 
            this.Rögzít.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Rögzít.Location = new System.Drawing.Point(1265, 8);
            this.Rögzít.Name = "Rögzít";
            this.Rögzít.Size = new System.Drawing.Size(40, 40);
            this.Rögzít.TabIndex = 8;
            this.ToolTip1.SetToolTip(this.Rögzít, "Rögzíti az adatokat");
            this.Rögzít.UseVisualStyleBackColor = true;
            this.Rögzít.Click += new System.EventHandler(this.Rögzít_Click);
            // 
            // Megjegyzés
            // 
            this.Megjegyzés.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Megjegyzés.Location = new System.Drawing.Point(754, 53);
            this.Megjegyzés.Multiline = true;
            this.Megjegyzés.Name = "Megjegyzés";
            this.Megjegyzés.Size = new System.Drawing.Size(496, 86);
            this.Megjegyzés.TabIndex = 6;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.BackColor = System.Drawing.Color.Transparent;
            this.Label7.Location = new System.Drawing.Point(754, 16);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(97, 20);
            this.Label7.TabIndex = 130;
            this.Label7.Text = "Megjegyzés:";
            // 
            // I_szakasz
            // 
            this.I_szakasz.Location = new System.Drawing.Point(639, 49);
            this.I_szakasz.Name = "I_szakasz";
            this.I_szakasz.Size = new System.Drawing.Size(100, 26);
            this.I_szakasz.TabIndex = 3;
            // 
            // II_szakasz
            // 
            this.II_szakasz.Location = new System.Drawing.Point(639, 81);
            this.II_szakasz.Name = "II_szakasz";
            this.II_szakasz.Size = new System.Drawing.Size(100, 26);
            this.II_szakasz.TabIndex = 4;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.BackColor = System.Drawing.Color.Transparent;
            this.Label5.Location = new System.Drawing.Point(502, 55);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(103, 20);
            this.Label5.TabIndex = 125;
            this.Label5.Text = "I szakasz [A]:";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.Location = new System.Drawing.Point(502, 87);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(108, 20);
            this.Label4.TabIndex = 124;
            this.Label4.Text = "II szakasz [A]:";
            // 
            // Dolgozó
            // 
            this.Dolgozó.FormattingEnabled = true;
            this.Dolgozó.Location = new System.Drawing.Point(136, 146);
            this.Dolgozó.Name = "Dolgozó";
            this.Dolgozó.Size = new System.Drawing.Size(466, 28);
            this.Dolgozó.TabIndex = 2;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Location = new System.Drawing.Point(7, 149);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(122, 20);
            this.Label1.TabIndex = 120;
            this.Label1.Text = "Mérést végezte:";
            // 
            // Dátum
            // 
            this.Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum.Location = new System.Drawing.Point(136, 109);
            this.Dátum.Name = "Dátum";
            this.Dátum.Size = new System.Drawing.Size(105, 26);
            this.Dátum.TabIndex = 1;
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.BackColor = System.Drawing.Color.Transparent;
            this.Label16.Location = new System.Drawing.Point(5, 115);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(115, 20);
            this.Label16.TabIndex = 118;
            this.Label16.Text = "Mérés dátuma:";
            // 
            // Button1
            // 
            this.Button1.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button1.Location = new System.Drawing.Point(448, 6);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(40, 40);
            this.Button1.TabIndex = 111;
            this.ToolTip1.SetToolTip(this.Button1, "Frissiti a pályaszám listát");
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Location = new System.Drawing.Point(7, 6);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(89, 20);
            this.Label15.TabIndex = 22;
            this.Label15.Text = "Pályaszám:";
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.RadioButton3);
            this.GroupBox1.Controls.Add(this.RadioButton2);
            this.GroupBox1.Controls.Add(this.RadioButton1);
            this.GroupBox1.Location = new System.Drawing.Point(9, 42);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(463, 54);
            this.GroupBox1.TabIndex = 20;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Fűtés típusa";
            // 
            // RadioButton3
            // 
            this.RadioButton3.AutoSize = true;
            this.RadioButton3.BackColor = System.Drawing.Color.LimeGreen;
            this.RadioButton3.Checked = true;
            this.RadioButton3.Location = new System.Drawing.Point(301, 24);
            this.RadioButton3.Name = "RadioButton3";
            this.RadioButton3.Size = new System.Drawing.Size(156, 24);
            this.RadioButton3.TabIndex = 2;
            this.RadioButton3.TabStop = true;
            this.RadioButton3.Text = "T5C5K2 automata";
            this.RadioButton3.UseVisualStyleBackColor = false;
            this.RadioButton3.CheckedChanged += new System.EventHandler(this.RadioButton3_CheckedChanged);
            // 
            // RadioButton2
            // 
            this.RadioButton2.AutoSize = true;
            this.RadioButton2.BackColor = System.Drawing.Color.LimeGreen;
            this.RadioButton2.Location = new System.Drawing.Point(140, 24);
            this.RadioButton2.Name = "RadioButton2";
            this.RadioButton2.Size = new System.Drawing.Size(155, 24);
            this.RadioButton2.TabIndex = 1;
            this.RadioButton2.Text = "Hagyományos K-s";
            this.RadioButton2.UseVisualStyleBackColor = false;
            this.RadioButton2.CheckedChanged += new System.EventHandler(this.RadioButton2_CheckedChanged);
            // 
            // RadioButton1
            // 
            this.RadioButton1.AutoSize = true;
            this.RadioButton1.BackColor = System.Drawing.Color.LimeGreen;
            this.RadioButton1.Location = new System.Drawing.Point(6, 24);
            this.RadioButton1.Name = "RadioButton1";
            this.RadioButton1.Size = new System.Drawing.Size(128, 24);
            this.RadioButton1.TabIndex = 0;
            this.RadioButton1.Text = "Hagyományos";
            this.RadioButton1.UseVisualStyleBackColor = false;
            this.RadioButton1.CheckedChanged += new System.EventHandler(this.RadioButton1_CheckedChanged);
            // 
            // Pályaszám
            // 
            this.Pályaszám.FormattingEnabled = true;
            this.Pályaszám.Location = new System.Drawing.Point(138, 8);
            this.Pályaszám.MaxLength = 4;
            this.Pályaszám.Name = "Pályaszám";
            this.Pályaszám.Size = new System.Drawing.Size(124, 28);
            this.Pályaszám.TabIndex = 0;
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.ForestGreen;
            this.TabPage2.Controls.Add(this.Lekérdezés_minden);
            this.TabPage2.Controls.Add(this.PSZ_hiány);
            this.TabPage2.Controls.Add(this.Kimutatás_készítés);
            this.TabPage2.Controls.Add(this.BtnExcelkimenet);
            this.TabPage2.Controls.Add(this.Dátum_év);
            this.TabPage2.Controls.Add(this.Label8);
            this.TabPage2.Controls.Add(this.Lekérdezés_Tábla);
            this.TabPage2.Controls.Add(this.Lekérdezés);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TabPage2.Size = new System.Drawing.Size(1314, 640);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Lekérdezés";
            // 
            // Lekérdezés_minden
            // 
            this.Lekérdezés_minden.AutoSize = true;
            this.Lekérdezés_minden.Location = new System.Drawing.Point(260, 29);
            this.Lekérdezés_minden.Name = "Lekérdezés_minden";
            this.Lekérdezés_minden.Size = new System.Drawing.Size(140, 24);
            this.Lekérdezés_minden.TabIndex = 198;
            this.Lekérdezés_minden.Text = "Minden rögzítés";
            this.Lekérdezés_minden.UseVisualStyleBackColor = true;
            // 
            // PSZ_hiány
            // 
            this.PSZ_hiány.BackgroundImage = global::Villamos.Properties.Resources.App_dict;
            this.PSZ_hiány.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PSZ_hiány.Location = new System.Drawing.Point(559, 8);
            this.PSZ_hiány.Name = "PSZ_hiány";
            this.PSZ_hiány.Size = new System.Drawing.Size(45, 45);
            this.PSZ_hiány.TabIndex = 196;
            this.ToolTip1.SetToolTip(this.PSZ_hiány, "Hiányzó pályaszámok");
            this.PSZ_hiány.UseVisualStyleBackColor = true;
            this.PSZ_hiány.Click += new System.EventHandler(this.PSZ_hiány_Click);
            // 
            // Kimutatás_készítés
            // 
            this.Kimutatás_készítés.BackgroundImage = global::Villamos.Properties.Resources.Aha_Soft_Large_Seo_SEO;
            this.Kimutatás_készítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Kimutatás_készítés.Location = new System.Drawing.Point(508, 8);
            this.Kimutatás_készítés.Name = "Kimutatás_készítés";
            this.Kimutatás_készítés.Size = new System.Drawing.Size(45, 45);
            this.Kimutatás_készítés.TabIndex = 195;
            this.ToolTip1.SetToolTip(this.Kimutatás_készítés, "Kimutatás készítés");
            this.Kimutatás_készítés.UseVisualStyleBackColor = true;
            this.Kimutatás_készítés.Click += new System.EventHandler(this.Kimutatás_készítés_Click);
            // 
            // BtnExcelkimenet
            // 
            this.BtnExcelkimenet.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.BtnExcelkimenet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnExcelkimenet.Location = new System.Drawing.Point(457, 8);
            this.BtnExcelkimenet.Name = "BtnExcelkimenet";
            this.BtnExcelkimenet.Size = new System.Drawing.Size(45, 45);
            this.BtnExcelkimenet.TabIndex = 194;
            this.ToolTip1.SetToolTip(this.BtnExcelkimenet, "Táblázat tartalmának Excel táblába való mentése");
            this.BtnExcelkimenet.UseVisualStyleBackColor = true;
            this.BtnExcelkimenet.Click += new System.EventHandler(this.BtnExcelkimenet_Click);
            // 
            // Dátum_év
            // 
            this.Dátum_év.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum_év.Location = new System.Drawing.Point(131, 27);
            this.Dátum_év.Name = "Dátum_év";
            this.Dátum_év.Size = new System.Drawing.Size(112, 26);
            this.Dátum_év.TabIndex = 192;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.BackColor = System.Drawing.Color.Transparent;
            this.Label8.Location = new System.Drawing.Point(7, 33);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(86, 20);
            this.Label8.TabIndex = 193;
            this.Label8.Text = "Mérés éve:";
            // 
            // Lekérdezés_Tábla
            // 
            this.Lekérdezés_Tábla.AllowUserToAddRows = false;
            this.Lekérdezés_Tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Lekérdezés_Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Lekérdezés_Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Lekérdezés_Tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Lekérdezés_Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Lekérdezés_Tábla.EnableHeadersVisualStyles = false;
            this.Lekérdezés_Tábla.Location = new System.Drawing.Point(8, 59);
            this.Lekérdezés_Tábla.Name = "Lekérdezés_Tábla";
            this.Lekérdezés_Tábla.RowHeadersVisible = false;
            this.Lekérdezés_Tábla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Lekérdezés_Tábla.Size = new System.Drawing.Size(1299, 573);
            this.Lekérdezés_Tábla.TabIndex = 191;
            this.Lekérdezés_Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Lekérdezés_Tábla_CellClick);
            // 
            // Lekérdezés
            // 
            this.Lekérdezés.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Lekérdezés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lekérdezés.Location = new System.Drawing.Point(406, 8);
            this.Lekérdezés.Name = "Lekérdezés";
            this.Lekérdezés.Size = new System.Drawing.Size(45, 45);
            this.Lekérdezés.TabIndex = 190;
            this.ToolTip1.SetToolTip(this.Lekérdezés, "Listázza az éves adatokat");
            this.Lekérdezés.UseVisualStyleBackColor = true;
            this.Lekérdezés.Click += new System.EventHandler(this.Lekérdezés_Click);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(515, 13);
            this.Holtart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(714, 28);
            this.Holtart.TabIndex = 178;
            this.Holtart.Visible = false;
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.Cmbtelephely);
            this.Panel2.Controls.Add(this.Label13);
            this.Panel2.Location = new System.Drawing.Point(5, 5);
            this.Panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(502, 40);
            this.Panel2.TabIndex = 176;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(214, 5);
            this.Cmbtelephely.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(277, 28);
            this.Cmbtelephely.TabIndex = 18;
            this.Cmbtelephely.SelectedIndexChanged += new System.EventHandler(this.Cmbtelephely_SelectedIndexChanged);
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(8, 8);
            this.Label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(145, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1282, 5);
            this.BtnSúgó.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 177;
            this.ToolTip1.SetToolTip(this.BtnSúgó, "Súgó");
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // Ablak_T5C5_fűtés
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.ForestGreen;
            this.ClientSize = new System.Drawing.Size(1334, 729);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.Fülek);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_T5C5_fűtés";
            this.Text = "T5C5 utastér fűtés";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.Fülek.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Lekérdezés_Tábla)).EndInit();
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        internal TabControl Fülek;
        internal TabPage TabPage2;
        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal Button BtnSúgó;
        internal Panel Panel2;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal TabPage TabPage1;
        internal Button Btnkilelöltörlés;
        internal Button BtnKijelölcsop;
        internal Button Rögzít;
        internal TextBox Megjegyzés;
        internal Label Label7;
        internal TextBox I_szakasz;
        internal TextBox II_szakasz;
        internal Label Label5;
        internal Label Label4;
        internal Label Label3;
        internal Label Label2;
        internal ComboBox Dolgozó;
        internal Label Label1;
        internal DateTimePicker Dátum;
        internal Label Label16;
        internal Button Button1;
        internal ComboBox Pályaszám;
        internal Label Label15;
        internal GroupBox GroupBox1;
        internal RadioButton RadioButton3;
        internal RadioButton RadioButton2;
        internal RadioButton RadioButton1;
        internal CheckBox CheckBox19;
        internal CheckBox CheckBox18;
        internal CheckBox CheckBox17;
        internal CheckBox CheckBox16;
        internal CheckBox CheckBox15;
        internal CheckBox CheckBox14;
        internal CheckBox CheckBox13;
        internal CheckBox CheckBox12;
        internal CheckBox CheckBox11;
        internal CheckBox CheckBox10;
        internal CheckBox CheckBox9;
        internal CheckBox CheckBox8;
        internal CheckBox CheckBox7;
        internal CheckBox CheckBox6;
        internal CheckBox CheckBox5;
        internal CheckBox CheckBox4;
        internal CheckBox CheckBox3;
        internal CheckBox CheckBox2;
        internal CheckBox CheckBox1;
        internal PictureBox PictureBox1;
        internal Button Új_elem;
        internal DataGridView Lekérdezés_Tábla;
        internal Button Lekérdezés;
        internal DateTimePicker Dátum_év;
        internal Label Label8;
        internal CheckBox Beállítási_értékek;
        internal PictureBox PictureBox2;
        internal Button BtnExcelkimenet;
        internal Button Kimutatás_készítés;
        internal ToolTip ToolTip1;
        internal Button PSZ_hiány;
        internal CheckBox Lekérdezés_minden;
        internal Panel panel1;
    }
}