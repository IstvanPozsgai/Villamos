using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{

    public partial class Ablak_Utasítás : Form
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

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        internal void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Utasítás));
            this.RadioMinden = new System.Windows.Forms.RadioButton();
            this.Radioolvastan = new System.Windows.Forms.RadioButton();
            this.Radioolvas = new System.Windows.Forms.RadioButton();
            this.txtírásimező = new System.Windows.Forms.RichTextBox();
            this.txtsorszám = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtszövegrészlet = new System.Windows.Forms.TextBox();
            this.cmbNév = new System.Windows.Forms.ComboBox();
            this.Dátumig = new System.Windows.Forms.DateTimePicker();
            this.Dátumtól = new System.Windows.Forms.DateTimePicker();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnVisszavon = new System.Windows.Forms.Button();
            this.Súgó = new System.Windows.Forms.Button();
            this.btnOlvasva = new System.Windows.Forms.Button();
            this.Alaphelyzet = new System.Windows.Forms.Button();
            this.btnújüzenet = new System.Windows.Forms.Button();
            this.btnolvasás = new System.Windows.Forms.Button();
            this.Utolsó = new System.Windows.Forms.Button();
            this.Következő = new System.Windows.Forms.Button();
            this.Előző = new System.Windows.Forms.Button();
            this.Első = new System.Windows.Forms.Button();
            this.Frissít = new System.Windows.Forms.Button();
            this.Excel_kimenet = new System.Windows.Forms.Button();
            this.Lefelé = new System.Windows.Forms.Button();
            this.Felfelé = new System.Windows.Forms.Button();
            this.tábla = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            ((System.ComponentModel.ISupportInitialize)(this.tábla)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // RadioMinden
            // 
            this.RadioMinden.AutoSize = true;
            this.RadioMinden.BackColor = System.Drawing.Color.LimeGreen;
            this.RadioMinden.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.RadioMinden.Location = new System.Drawing.Point(855, 6);
            this.RadioMinden.Name = "RadioMinden";
            this.RadioMinden.Size = new System.Drawing.Size(79, 24);
            this.RadioMinden.TabIndex = 55;
            this.RadioMinden.Text = "Minden";
            this.RadioMinden.UseVisualStyleBackColor = false;
            // 
            // Radioolvastan
            // 
            this.Radioolvastan.AutoSize = true;
            this.Radioolvastan.BackColor = System.Drawing.Color.LimeGreen;
            this.Radioolvastan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Radioolvastan.Location = new System.Drawing.Point(745, 6);
            this.Radioolvastan.Name = "Radioolvastan";
            this.Radioolvastan.Size = new System.Drawing.Size(104, 24);
            this.Radioolvastan.TabIndex = 54;
            this.Radioolvastan.Text = "Visszavont";
            this.Radioolvastan.UseVisualStyleBackColor = false;
            // 
            // Radioolvas
            // 
            this.Radioolvas.AutoSize = true;
            this.Radioolvas.BackColor = System.Drawing.Color.LimeGreen;
            this.Radioolvas.Checked = true;
            this.Radioolvas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Radioolvas.Location = new System.Drawing.Point(647, 6);
            this.Radioolvas.Name = "Radioolvas";
            this.Radioolvas.Size = new System.Drawing.Size(92, 24);
            this.Radioolvas.TabIndex = 53;
            this.Radioolvas.TabStop = true;
            this.Radioolvas.Text = "Érvényes";
            this.Radioolvas.UseVisualStyleBackColor = false;
            // 
            // txtírásimező
            // 
            this.txtírásimező.AcceptsTab = true;
            this.txtírásimező.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtírásimező.Location = new System.Drawing.Point(5, 340);
            this.txtírásimező.Name = "txtírásimező";
            this.txtírásimező.Size = new System.Drawing.Size(1256, 346);
            this.txtírásimező.TabIndex = 49;
            this.txtírásimező.Text = "";
            // 
            // txtsorszám
            // 
            this.txtsorszám.Location = new System.Drawing.Point(128, 9);
            this.txtsorszám.Name = "txtsorszám";
            this.txtsorszám.Size = new System.Drawing.Size(96, 26);
            this.txtsorszám.TabIndex = 48;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(4, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(123, 20);
            this.Label1.TabIndex = 47;
            this.Label1.Text = "Utasítás száma:";
            // 
            // txtszövegrészlet
            // 
            this.txtszövegrészlet.Location = new System.Drawing.Point(657, 38);
            this.txtszövegrészlet.Name = "txtszövegrészlet";
            this.txtszövegrészlet.Size = new System.Drawing.Size(277, 26);
            this.txtszövegrészlet.TabIndex = 37;
            // 
            // cmbNév
            // 
            this.cmbNév.FormattingEnabled = true;
            this.cmbNév.Location = new System.Drawing.Point(430, 5);
            this.cmbNév.Name = "cmbNév";
            this.cmbNév.Size = new System.Drawing.Size(135, 28);
            this.cmbNév.TabIndex = 36;
            // 
            // Dátumig
            // 
            this.Dátumig.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátumig.Location = new System.Drawing.Point(544, 38);
            this.Dátumig.Name = "Dátumig";
            this.Dátumig.Size = new System.Drawing.Size(107, 26);
            this.Dátumig.TabIndex = 35;
            // 
            // Dátumtól
            // 
            this.Dátumtól.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátumtól.Location = new System.Drawing.Point(431, 38);
            this.Dátumtól.Name = "Dátumtól";
            this.Dátumtól.Size = new System.Drawing.Size(107, 26);
            this.Dátumtól.TabIndex = 34;
            this.Dátumtól.ValueChanged += new System.EventHandler(this.Dátumtól_ValueChanged);
            // 
            // btnVisszavon
            // 
            this.btnVisszavon.BackgroundImage = global::Villamos.Properties.Resources.Go_back;
            this.btnVisszavon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnVisszavon.Location = new System.Drawing.Point(669, 5);
            this.btnVisszavon.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnVisszavon.Name = "btnVisszavon";
            this.btnVisszavon.Size = new System.Drawing.Size(40, 40);
            this.btnVisszavon.TabIndex = 59;
            this.ToolTip1.SetToolTip(this.btnVisszavon, "Visszavonás");
            this.btnVisszavon.UseVisualStyleBackColor = true;
            this.btnVisszavon.Click += new System.EventHandler(this.BtnVisszavon_Click);
            // 
            // Súgó
            // 
            this.Súgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Súgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.Súgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Súgó.Location = new System.Drawing.Point(1214, 5);
            this.Súgó.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Súgó.Name = "Súgó";
            this.Súgó.Size = new System.Drawing.Size(40, 40);
            this.Súgó.TabIndex = 56;
            this.ToolTip1.SetToolTip(this.Súgó, "Súgó");
            this.Súgó.UseVisualStyleBackColor = true;
            this.Súgó.Click += new System.EventHandler(this.Súgó_Click);
            // 
            // btnOlvasva
            // 
            this.btnOlvasva.BackgroundImage = global::Villamos.Properties.Resources.Junior_Icon_111;
            this.btnOlvasva.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOlvasva.Location = new System.Drawing.Point(621, 5);
            this.btnOlvasva.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOlvasva.Name = "btnOlvasva";
            this.btnOlvasva.Size = new System.Drawing.Size(40, 40);
            this.btnOlvasva.TabIndex = 51;
            this.ToolTip1.SetToolTip(this.btnOlvasva, "Olvasási visszaigazolás");
            this.btnOlvasva.UseVisualStyleBackColor = true;
            this.btnOlvasva.Click += new System.EventHandler(this.BtnOlvasva_Click);
            // 
            // Alaphelyzet
            // 
            this.Alaphelyzet.BackgroundImage = global::Villamos.Properties.Resources.Mimetype_recycled;
            this.Alaphelyzet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Alaphelyzet.Location = new System.Drawing.Point(375, 5);
            this.Alaphelyzet.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Alaphelyzet.Name = "Alaphelyzet";
            this.Alaphelyzet.Size = new System.Drawing.Size(48, 48);
            this.Alaphelyzet.TabIndex = 46;
            this.ToolTip1.SetToolTip(this.Alaphelyzet, "A szűrési feltételeknek alaphelyzetbe állítása");
            this.Alaphelyzet.UseVisualStyleBackColor = true;
            this.Alaphelyzet.Click += new System.EventHandler(this.Alaphelyzet_Click);
            // 
            // btnújüzenet
            // 
            this.btnújüzenet.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.btnújüzenet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnújüzenet.Location = new System.Drawing.Point(573, 5);
            this.btnújüzenet.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnújüzenet.Name = "btnújüzenet";
            this.btnújüzenet.Size = new System.Drawing.Size(40, 40);
            this.btnújüzenet.TabIndex = 44;
            this.ToolTip1.SetToolTip(this.btnújüzenet, "Új üzenet írás");
            this.btnújüzenet.UseVisualStyleBackColor = true;
            this.btnújüzenet.Click += new System.EventHandler(this.Btnújüzenet_Click);
            // 
            // btnolvasás
            // 
            this.btnolvasás.BackgroundImage = global::Villamos.Properties.Resources.App_dict;
            this.btnolvasás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnolvasás.Location = new System.Drawing.Point(247, 5);
            this.btnolvasás.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnolvasás.Name = "btnolvasás";
            this.btnolvasás.Size = new System.Drawing.Size(40, 40);
            this.btnolvasás.TabIndex = 42;
            this.ToolTip1.SetToolTip(this.btnolvasás, "Üzenet olvasás");
            this.btnolvasás.UseVisualStyleBackColor = true;
            this.btnolvasás.Click += new System.EventHandler(this.Btnolvasás_Click);
            // 
            // Utolsó
            // 
            this.Utolsó.BackgroundImage = global::Villamos.Properties.Resources.Button_Forward_01;
            this.Utolsó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Utolsó.Location = new System.Drawing.Point(448, 5);
            this.Utolsó.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Utolsó.Name = "Utolsó";
            this.Utolsó.Size = new System.Drawing.Size(40, 40);
            this.Utolsó.TabIndex = 41;
            this.ToolTip1.SetToolTip(this.Utolsó, "Évben a legutolsó üzenet");
            this.Utolsó.UseVisualStyleBackColor = true;
            this.Utolsó.Click += new System.EventHandler(this.Utolsó_Click);
            // 
            // Következő
            // 
            this.Következő.BackgroundImage = global::Villamos.Properties.Resources.Button_Next_01;
            this.Következő.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Következő.Location = new System.Drawing.Point(400, 5);
            this.Következő.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Következő.Name = "Következő";
            this.Következő.Size = new System.Drawing.Size(40, 40);
            this.Következő.TabIndex = 40;
            this.ToolTip1.SetToolTip(this.Következő, "Következő üzenet");
            this.Következő.UseVisualStyleBackColor = true;
            this.Következő.Click += new System.EventHandler(this.Következő_Click);
            // 
            // Előző
            // 
            this.Előző.BackgroundImage = global::Villamos.Properties.Resources.Button_Previous_01;
            this.Előző.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Előző.Location = new System.Drawing.Point(352, 5);
            this.Előző.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Előző.Name = "Előző";
            this.Előző.Size = new System.Drawing.Size(40, 40);
            this.Előző.TabIndex = 39;
            this.ToolTip1.SetToolTip(this.Előző, "Előző üzenet");
            this.Előző.UseVisualStyleBackColor = true;
            this.Előző.Click += new System.EventHandler(this.Előző_Click);
            // 
            // Első
            // 
            this.Első.BackgroundImage = global::Villamos.Properties.Resources.Button_Rewind_01;
            this.Első.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Első.Location = new System.Drawing.Point(304, 5);
            this.Első.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Első.Name = "Első";
            this.Első.Size = new System.Drawing.Size(40, 40);
            this.Első.TabIndex = 38;
            this.ToolTip1.SetToolTip(this.Első, "Évben a legelső üzenet");
            this.Első.UseVisualStyleBackColor = true;
            this.Első.Click += new System.EventHandler(this.Első_Click);
            // 
            // Frissít
            // 
            this.Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Frissít.Location = new System.Drawing.Point(941, 16);
            this.Frissít.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Frissít.Name = "Frissít";
            this.Frissít.Size = new System.Drawing.Size(40, 40);
            this.Frissít.TabIndex = 32;
            this.ToolTip1.SetToolTip(this.Frissít, "A szűrt feltételeknek megfelelő listát készít.");
            this.Frissít.UseVisualStyleBackColor = true;
            this.Frissít.Click += new System.EventHandler(this.Frissít_Click);
            // 
            // Excel_kimenet
            // 
            this.Excel_kimenet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Excel_kimenet.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excel_kimenet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excel_kimenet.Location = new System.Drawing.Point(989, 16);
            this.Excel_kimenet.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Excel_kimenet.Name = "Excel_kimenet";
            this.Excel_kimenet.Size = new System.Drawing.Size(40, 40);
            this.Excel_kimenet.TabIndex = 131;
            this.ToolTip1.SetToolTip(this.Excel_kimenet, "A szűrt feltételeknek megfelelő Excel táblát készít.");
            this.Excel_kimenet.UseVisualStyleBackColor = true;
            this.Excel_kimenet.Click += new System.EventHandler(this.Excel_kimenet_Click);
            // 
            // Lefelé
            // 
            this.Lefelé.BackgroundImage = global::Villamos.Properties.Resources.le;
            this.Lefelé.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lefelé.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Lefelé.Location = new System.Drawing.Point(875, 5);
            this.Lefelé.Name = "Lefelé";
            this.Lefelé.Size = new System.Drawing.Size(40, 40);
            this.Lefelé.TabIndex = 133;
            this.ToolTip1.SetToolTip(this.Lefelé, "Lejjebb viszi a gombsort");
            this.Lefelé.UseVisualStyleBackColor = true;
            this.Lefelé.Click += new System.EventHandler(this.Lefelé_Click);
            // 
            // Felfelé
            // 
            this.Felfelé.BackgroundImage = global::Villamos.Properties.Resources.fel;
            this.Felfelé.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Felfelé.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Felfelé.Location = new System.Drawing.Point(829, 5);
            this.Felfelé.Name = "Felfelé";
            this.Felfelé.Size = new System.Drawing.Size(40, 40);
            this.Felfelé.TabIndex = 132;
            this.ToolTip1.SetToolTip(this.Felfelé, "Feljebb viszi a gombsort");
            this.Felfelé.UseVisualStyleBackColor = true;
            this.Felfelé.Click += new System.EventHandler(this.Felfelé_Click);
            // 
            // tábla
            // 
            this.tábla.AllowUserToAddRows = false;
            this.tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Khaki;
            this.tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.tábla.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tábla.BackgroundColor = System.Drawing.Color.DarkKhaki;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Moccasin;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tábla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.tábla.EnableHeadersVisualStyles = false;
            this.tábla.Location = new System.Drawing.Point(6, 72);
            this.tábla.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tábla.Name = "tábla";
            this.tábla.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Khaki;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.tábla.RowHeadersWidth = 42;
            this.tábla.RowTemplate.Height = 30;
            this.tábla.Size = new System.Drawing.Size(1256, 216);
            this.tábla.TabIndex = 31;
            this.tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
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
            this.Column5.HeaderText = "Érvényes";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 70;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Cmbtelephely);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(6, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(361, 41);
            this.panel2.TabIndex = 122;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(144, 5);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(214, 28);
            this.Cmbtelephely.TabIndex = 18;
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
            // panel1
            // 
            this.panel1.Controls.Add(this.Lefelé);
            this.panel1.Controls.Add(this.Felfelé);
            this.panel1.Controls.Add(this.Label1);
            this.panel1.Controls.Add(this.txtsorszám);
            this.panel1.Controls.Add(this.btnolvasás);
            this.panel1.Controls.Add(this.btnVisszavon);
            this.panel1.Controls.Add(this.Első);
            this.panel1.Controls.Add(this.Előző);
            this.panel1.Controls.Add(this.Következő);
            this.panel1.Controls.Add(this.btnOlvasva);
            this.panel1.Controls.Add(this.Utolsó);
            this.panel1.Controls.Add(this.btnújüzenet);
            this.panel1.Location = new System.Drawing.Point(5, 290);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(965, 50);
            this.panel1.TabIndex = 133;
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(171, 123);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(930, 25);
            this.Holtart.TabIndex = 134;
            this.Holtart.Visible = false;
            // 
            // Ablak_Utasítás
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGreen;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1267, 701);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Excel_kimenet);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.Súgó);
            this.Controls.Add(this.RadioMinden);
            this.Controls.Add(this.Radioolvastan);
            this.Controls.Add(this.Radioolvas);
            this.Controls.Add(this.txtírásimező);
            this.Controls.Add(this.Alaphelyzet);
            this.Controls.Add(this.txtszövegrészlet);
            this.Controls.Add(this.cmbNév);
            this.Controls.Add(this.Dátumig);
            this.Controls.Add(this.Dátumtól);
            this.Controls.Add(this.Frissít);
            this.Controls.Add(this.tábla);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Utasítás";
            this.Text = "Utasítások";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablaküzenet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tábla)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal Button Súgó;
        internal ToolTip ToolTip1;
        internal RadioButton RadioMinden;
        internal RadioButton Radioolvastan;
        internal RadioButton Radioolvas;
        internal Button btnOlvasva;
        internal RichTextBox txtírásimező;
        internal TextBox txtsorszám;
        internal Label Label1;
        internal Button Alaphelyzet;
        internal Button btnújüzenet;
        internal Button btnolvasás;
        internal Button Utolsó;
        internal Button Következő;
        internal Button Előző;
        internal Button Első;
        internal TextBox txtszövegrészlet;
        internal ComboBox cmbNév;
        internal DateTimePicker Dátumig;
        internal DateTimePicker Dátumtól;
        internal Button Frissít;
        internal DataGridView tábla;
        internal Button btnVisszavon;
        internal DataGridViewTextBoxColumn Column1;
        internal DataGridViewTextBoxColumn Column2;
        internal DataGridViewTextBoxColumn Column3;
        internal DataGridViewTextBoxColumn Column4;
        internal DataGridViewCheckBoxColumn Column5;
        internal Panel panel2;
        internal ComboBox Cmbtelephely;
        internal Label label2;
        internal Button Excel_kimenet;
        internal Panel panel1;
        internal Button Lefelé;
        internal Button Felfelé;
        internal V_MindenEgyéb.MyProgressbar Holtart;
        private System.ComponentModel.IContainer components;
    }
}