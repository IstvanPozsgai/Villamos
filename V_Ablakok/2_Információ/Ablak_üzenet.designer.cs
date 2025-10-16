using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos 
{
    public partial class Ablak_üzenet : Form
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_üzenet));
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Dátumtól = new System.Windows.Forms.DateTimePicker();
            this.Dátumig = new System.Windows.Forms.DateTimePicker();
            this.cmbNév = new System.Windows.Forms.ComboBox();
            this.txtszövegrészlet = new System.Windows.Forms.TextBox();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Button6 = new System.Windows.Forms.Button();
            this.BtnOlvasva = new System.Windows.Forms.Button();
            this.Button10 = new System.Windows.Forms.Button();
            this.btnújüzenet = new System.Windows.Forms.Button();
            this.btnválaszol = new System.Windows.Forms.Button();
            this.btnolvasás = new System.Windows.Forms.Button();
            this.Utolsó = new System.Windows.Forms.Button();
            this.Következő = new System.Windows.Forms.Button();
            this.Előző = new System.Windows.Forms.Button();
            this.Első = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.Excel_kimenet = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.Bit64 = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtsorszám = new System.Windows.Forms.TextBox();
            this.Txtírásimező = new System.Windows.Forms.RichTextBox();
            this.txtválasz = new System.Windows.Forms.TextBox();
            this.Radioolvas = new System.Windows.Forms.RadioButton();
            this.Radioolvastan = new System.Windows.Forms.RadioButton();
            this.RadioMinden = new System.Windows.Forms.RadioButton();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.Panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkKhaki;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.Tábla.EnableHeadersVisualStyles = false;
            this.Tábla.Location = new System.Drawing.Point(5, 70);
            this.Tábla.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Tábla.Name = "Tábla";
            this.Tábla.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.DarkKhaki;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Tábla.RowHeadersWidth = 20;
            this.Tábla.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.Tábla.RowTemplate.Height = 30;
            this.Tábla.Size = new System.Drawing.Size(1268, 212);
            this.Tábla.TabIndex = 0;
            this.Tábla.MultiSelectChanged += new System.EventHandler(this.Tábla_MultiSelectChanged);
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            this.Tábla.SelectionChanged += new System.EventHandler(this.Tábla_SelectionChanged);
            // 
            // Column1
            // 
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "Sorszám";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 80;
            // 
            // Column2
            // 
            this.Column2.Frozen = true;
            this.Column2.HeaderText = "Írta";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 105;
            // 
            // Column3
            // 
            this.Column3.Frozen = true;
            this.Column3.HeaderText = "Mikor";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 145;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.HeaderText = "Szöveg";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Olvasott";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 70;
            // 
            // Dátumtól
            // 
            this.Dátumtól.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátumtól.Location = new System.Drawing.Point(429, 7);
            this.Dátumtól.Name = "Dátumtól";
            this.Dátumtól.Size = new System.Drawing.Size(107, 26);
            this.Dátumtól.TabIndex = 3;
            // 
            // Dátumig
            // 
            this.Dátumig.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátumig.Location = new System.Drawing.Point(429, 39);
            this.Dátumig.Name = "Dátumig";
            this.Dátumig.Size = new System.Drawing.Size(107, 26);
            this.Dátumig.TabIndex = 4;
            // 
            // cmbNév
            // 
            this.cmbNév.FormattingEnabled = true;
            this.cmbNév.Location = new System.Drawing.Point(542, 5);
            this.cmbNév.Name = "cmbNév";
            this.cmbNév.Size = new System.Drawing.Size(135, 28);
            this.cmbNév.TabIndex = 5;
            // 
            // txtszövegrészlet
            // 
            this.txtszövegrészlet.Location = new System.Drawing.Point(542, 39);
            this.txtszövegrészlet.Name = "txtszövegrészlet";
            this.txtszövegrészlet.Size = new System.Drawing.Size(277, 26);
            this.txtszövegrészlet.TabIndex = 6;
            // 
            // Button6
            // 
            this.Button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button6.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.Button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button6.Location = new System.Drawing.Point(1243, 10);
            this.Button6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Button6.Name = "Button6";
            this.Button6.Size = new System.Drawing.Size(40, 40);
            this.Button6.TabIndex = 29;
            this.ToolTip1.SetToolTip(this.Button6, "Súgó");
            this.Button6.UseVisualStyleBackColor = true;
            this.Button6.Click += new System.EventHandler(this.Button6_Click_1);
            // 
            // BtnOlvasva
            // 
            this.BtnOlvasva.BackgroundImage = global::Villamos.Properties.Resources.Junior_Icon_111;
            this.BtnOlvasva.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnOlvasva.Location = new System.Drawing.Point(603, 4);
            this.BtnOlvasva.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnOlvasva.Name = "BtnOlvasva";
            this.BtnOlvasva.Size = new System.Drawing.Size(40, 40);
            this.BtnOlvasva.TabIndex = 24;
            this.ToolTip1.SetToolTip(this.BtnOlvasva, "Olvasási visszaigazolás");
            this.BtnOlvasva.UseVisualStyleBackColor = true;
            this.BtnOlvasva.Click += new System.EventHandler(this.BtnOlvasva_Click);
            // 
            // Button10
            // 
            this.Button10.BackgroundImage = global::Villamos.Properties.Resources.Mimetype_recycled;
            this.Button10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button10.Location = new System.Drawing.Point(373, 10);
            this.Button10.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Button10.Name = "Button10";
            this.Button10.Size = new System.Drawing.Size(40, 40);
            this.Button10.TabIndex = 16;
            this.ToolTip1.SetToolTip(this.Button10, "A szűrési feltételeknek alaphelyzetbe állítása");
            this.Button10.UseVisualStyleBackColor = true;
            this.Button10.Click += new System.EventHandler(this.Button10_Click);
            // 
            // btnújüzenet
            // 
            this.btnújüzenet.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.btnújüzenet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnújüzenet.Location = new System.Drawing.Point(555, 4);
            this.btnújüzenet.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnújüzenet.Name = "btnújüzenet";
            this.btnújüzenet.Size = new System.Drawing.Size(40, 40);
            this.btnújüzenet.TabIndex = 14;
            this.ToolTip1.SetToolTip(this.btnújüzenet, "Új üzenet írás");
            this.btnújüzenet.UseVisualStyleBackColor = true;
            this.btnújüzenet.Click += new System.EventHandler(this.Btnújüzenet_Click);
            // 
            // btnválaszol
            // 
            this.btnválaszol.BackgroundImage = global::Villamos.Properties.Resources.App_aim_3;
            this.btnválaszol.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnválaszol.Location = new System.Drawing.Point(507, 5);
            this.btnválaszol.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnválaszol.Name = "btnválaszol";
            this.btnválaszol.Size = new System.Drawing.Size(40, 40);
            this.btnválaszol.TabIndex = 13;
            this.ToolTip1.SetToolTip(this.btnválaszol, "Üzenetre válasz írás");
            this.btnválaszol.UseVisualStyleBackColor = true;
            this.btnválaszol.Click += new System.EventHandler(this.Btnválaszol_Click);
            // 
            // btnolvasás
            // 
            this.btnolvasás.BackgroundImage = global::Villamos.Properties.Resources.App_dict;
            this.btnolvasás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnolvasás.Location = new System.Drawing.Point(230, 5);
            this.btnolvasás.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnolvasás.Name = "btnolvasás";
            this.btnolvasás.Size = new System.Drawing.Size(40, 40);
            this.btnolvasás.TabIndex = 12;
            this.ToolTip1.SetToolTip(this.btnolvasás, "Üzenet olvasás");
            this.btnolvasás.UseVisualStyleBackColor = true;
            this.btnolvasás.Click += new System.EventHandler(this.Btnolvasás_Click);
            // 
            // Utolsó
            // 
            this.Utolsó.BackgroundImage = global::Villamos.Properties.Resources.Button_Forward_01;
            this.Utolsó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Utolsó.Location = new System.Drawing.Point(435, 5);
            this.Utolsó.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Utolsó.Name = "Utolsó";
            this.Utolsó.Size = new System.Drawing.Size(40, 40);
            this.Utolsó.TabIndex = 11;
            this.ToolTip1.SetToolTip(this.Utolsó, "Évben a legutolsó üzenet");
            this.Utolsó.UseVisualStyleBackColor = true;
            this.Utolsó.Click += new System.EventHandler(this.Utolsó_Click);
            // 
            // Következő
            // 
            this.Következő.BackgroundImage = global::Villamos.Properties.Resources.Button_Next_01;
            this.Következő.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Következő.Location = new System.Drawing.Point(387, 5);
            this.Következő.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Következő.Name = "Következő";
            this.Következő.Size = new System.Drawing.Size(40, 40);
            this.Következő.TabIndex = 10;
            this.ToolTip1.SetToolTip(this.Következő, "Következő üzenet");
            this.Következő.UseVisualStyleBackColor = true;
            this.Következő.Click += new System.EventHandler(this.Következő_Click);
            // 
            // Előző
            // 
            this.Előző.BackgroundImage = global::Villamos.Properties.Resources.Button_Previous_01;
            this.Előző.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Előző.Location = new System.Drawing.Point(339, 5);
            this.Előző.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Előző.Name = "Előző";
            this.Előző.Size = new System.Drawing.Size(40, 40);
            this.Előző.TabIndex = 9;
            this.ToolTip1.SetToolTip(this.Előző, "Előző üzenet");
            this.Előző.UseVisualStyleBackColor = true;
            this.Előző.Click += new System.EventHandler(this.Előző_Click);
            // 
            // Első
            // 
            this.Első.BackgroundImage = global::Villamos.Properties.Resources.Button_Rewind_01;
            this.Első.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Első.Location = new System.Drawing.Point(291, 5);
            this.Első.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Első.Name = "Első";
            this.Első.Size = new System.Drawing.Size(40, 40);
            this.Első.TabIndex = 8;
            this.ToolTip1.SetToolTip(this.Első, "Évben a legelső üzenet");
            this.Első.UseVisualStyleBackColor = true;
            this.Első.Click += new System.EventHandler(this.Első_Click);
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button1.Location = new System.Drawing.Point(1062, 10);
            this.Button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(40, 40);
            this.Button1.TabIndex = 1;
            this.ToolTip1.SetToolTip(this.Button1, "A szűrt feltételeknek megfelelő listát készít.");
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Excel_kimenet
            // 
            this.Excel_kimenet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Excel_kimenet.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excel_kimenet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excel_kimenet.Location = new System.Drawing.Point(1110, 10);
            this.Excel_kimenet.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Excel_kimenet.Name = "Excel_kimenet";
            this.Excel_kimenet.Size = new System.Drawing.Size(40, 40);
            this.Excel_kimenet.TabIndex = 130;
            this.ToolTip1.SetToolTip(this.Excel_kimenet, "A szűrt feltételeknek megfelelő Excel táblázatot készít.");
            this.Excel_kimenet.UseVisualStyleBackColor = true;
            this.Excel_kimenet.Click += new System.EventHandler(this.Excel_kimenet_Click);
            // 
            // button7
            // 
            this.button7.BackgroundImage = global::Villamos.Properties.Resources.fel;
            this.button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button7.Location = new System.Drawing.Point(784, 5);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(40, 40);
            this.button7.TabIndex = 130;
            this.ToolTip1.SetToolTip(this.button7, "Feljebb viszi a gombsort");
            this.button7.UseVisualStyleBackColor = true;
            this.button7.DoubleClick += new System.EventHandler(this.Button7_DoubleClick);
            this.button7.Click += new System.EventHandler(this.Button7_Click);
            // 
            // button8
            // 
            this.button8.BackgroundImage = global::Villamos.Properties.Resources.le;
            this.button8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button8.Location = new System.Drawing.Point(830, 5);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(40, 40);
            this.button8.TabIndex = 131;
            this.ToolTip1.SetToolTip(this.button8, "Lejjebb viszi a gombsort");
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.Button8_Click);
            // 
            // Bit64
            // 
            this.Bit64.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Bit64.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Bit64.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Bit64.Location = new System.Drawing.Point(881, 5);
            this.Bit64.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Bit64.Name = "Bit64";
            this.Bit64.Size = new System.Drawing.Size(40, 40);
            this.Bit64.TabIndex = 132;
            this.ToolTip1.SetToolTip(this.Bit64, "Adatok konvertálása");
            this.Bit64.UseVisualStyleBackColor = true;
            this.Bit64.Visible = false;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(3, 5);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(116, 20);
            this.Label1.TabIndex = 18;
            this.Label1.Text = "Üzenet száma:";
            // 
            // txtsorszám
            // 
            this.txtsorszám.Location = new System.Drawing.Point(125, 5);
            this.txtsorszám.Name = "txtsorszám";
            this.txtsorszám.Size = new System.Drawing.Size(96, 26);
            this.txtsorszám.TabIndex = 19;
            // 
            // Txtírásimező
            // 
            this.Txtírásimező.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Txtírásimező.Location = new System.Drawing.Point(5, 346);
            this.Txtírásimező.Name = "Txtírásimező";
            this.Txtírásimező.Size = new System.Drawing.Size(1268, 352);
            this.Txtírásimező.TabIndex = 22;
            this.Txtírásimező.Text = "";
            // 
            // txtválasz
            // 
            this.txtválasz.Location = new System.Drawing.Point(784, 11);
            this.txtválasz.Name = "txtválasz";
            this.txtválasz.Size = new System.Drawing.Size(11, 26);
            this.txtválasz.TabIndex = 25;
            this.txtválasz.Visible = false;
            // 
            // Radioolvas
            // 
            this.Radioolvas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Radioolvas.AutoSize = true;
            this.Radioolvas.BackColor = System.Drawing.Color.LimeGreen;
            this.Radioolvas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Radioolvas.Location = new System.Drawing.Point(734, 10);
            this.Radioolvas.Name = "Radioolvas";
            this.Radioolvas.Size = new System.Drawing.Size(85, 24);
            this.Radioolvas.TabIndex = 26;
            this.Radioolvas.Text = "Olvasott";
            this.Radioolvas.UseVisualStyleBackColor = false;
            // 
            // Radioolvastan
            // 
            this.Radioolvastan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Radioolvastan.AutoSize = true;
            this.Radioolvastan.BackColor = System.Drawing.Color.LimeGreen;
            this.Radioolvastan.Checked = true;
            this.Radioolvastan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Radioolvastan.Location = new System.Drawing.Point(825, 10);
            this.Radioolvastan.Name = "Radioolvastan";
            this.Radioolvastan.Size = new System.Drawing.Size(101, 24);
            this.Radioolvastan.TabIndex = 27;
            this.Radioolvastan.TabStop = true;
            this.Radioolvastan.Text = "Olvasatlan";
            this.Radioolvastan.UseVisualStyleBackColor = false;
            // 
            // RadioMinden
            // 
            this.RadioMinden.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RadioMinden.AutoSize = true;
            this.RadioMinden.BackColor = System.Drawing.Color.LimeGreen;
            this.RadioMinden.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.RadioMinden.Location = new System.Drawing.Point(932, 10);
            this.RadioMinden.Name = "RadioMinden";
            this.RadioMinden.Size = new System.Drawing.Size(79, 24);
            this.RadioMinden.TabIndex = 28;
            this.RadioMinden.Text = "Minden";
            this.RadioMinden.UseVisualStyleBackColor = false;
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.Cmbtelephely);
            this.Panel1.Controls.Add(this.label2);
            this.Panel1.Location = new System.Drawing.Point(5, 12);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(361, 41);
            this.Panel1.TabIndex = 121;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(144, 5);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(214, 28);
            this.Cmbtelephely.TabIndex = 18;
            this.Cmbtelephely.SelectedIndexChanged += new System.EventHandler(this.CMBtelephely_SelectedIndexChanged);
            this.Cmbtelephely.SelectionChangeCommitted += new System.EventHandler(this.Cmbtelephely_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 20);
            this.label2.TabIndex = 17;
            this.label2.Text = "Telephelyi beállítás:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Bit64);
            this.panel2.Controls.Add(this.button8);
            this.panel2.Controls.Add(this.button7);
            this.panel2.Controls.Add(this.Label1);
            this.panel2.Controls.Add(this.txtsorszám);
            this.panel2.Controls.Add(this.btnolvasás);
            this.panel2.Controls.Add(this.Első);
            this.panel2.Controls.Add(this.Előző);
            this.panel2.Controls.Add(this.Következő);
            this.panel2.Controls.Add(this.Utolsó);
            this.panel2.Controls.Add(this.txtválasz);
            this.panel2.Controls.Add(this.btnválaszol);
            this.panel2.Controls.Add(this.btnújüzenet);
            this.panel2.Controls.Add(this.BtnOlvasva);
            this.panel2.Location = new System.Drawing.Point(5, 290);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(965, 50);
            this.panel2.TabIndex = 132;
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(170, 100);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(930, 25);
            this.Holtart.TabIndex = 133;
            this.Holtart.Visible = false;
            // 
            // Ablak_üzenet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(1284, 701);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Excel_kimenet);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Button6);
            this.Controls.Add(this.RadioMinden);
            this.Controls.Add(this.Radioolvastan);
            this.Controls.Add(this.Radioolvas);
            this.Controls.Add(this.Txtírásimező);
            this.Controls.Add(this.Button10);
            this.Controls.Add(this.txtszövegrészlet);
            this.Controls.Add(this.cmbNév);
            this.Controls.Add(this.Dátumig);
            this.Controls.Add(this.Dátumtól);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.Tábla);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_üzenet";
            this.Text = "Üzenetek írása, olvasása";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_üzenet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal DataGridView Tábla;
        internal Button Button1;
        internal DateTimePicker Dátumtól;
        internal DateTimePicker Dátumig;
        internal ComboBox cmbNév;
        internal TextBox txtszövegrészlet;
        internal Button Első;
        internal Button Előző;
        internal Button Következő;
        internal Button Utolsó;
        internal Button btnolvasás;
        internal Button btnválaszol;
        internal Button btnújüzenet;
        internal ToolTip ToolTip1;
        internal Button Button10;
        internal Label Label1;
        internal TextBox txtsorszám;
        internal RichTextBox Txtírásimező;
        internal Button BtnOlvasva;
        internal TextBox txtválasz;
        internal RadioButton Radioolvas;
        internal RadioButton Radioolvastan;
        internal RadioButton RadioMinden;
        internal Button Button6;
        internal Panel Panel1;
        internal Label label2;
        internal DataGridViewTextBoxColumn Column1;
        internal DataGridViewTextBoxColumn Column2;
        internal DataGridViewTextBoxColumn Column3;
        internal DataGridViewTextBoxColumn Column4;
        internal DataGridViewCheckBoxColumn Column5;
        internal ComboBox Cmbtelephely;
        internal Button Excel_kimenet;
        private Panel panel2;
        internal Button button8;
        internal Button button7;
        private V_MindenEgyéb.MyProgressbar Holtart;
        internal Button Bit64;
    }
}