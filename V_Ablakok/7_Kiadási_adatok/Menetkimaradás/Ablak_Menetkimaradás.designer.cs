using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
      public partial class AblakMenetkimaradás : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AblakMenetkimaradás));
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Button4 = new System.Windows.Forms.Button();
            this.Button3 = new System.Windows.Forms.Button();
            this.Button2 = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.Btnexcel = new System.Windows.Forms.Button();
            this.BtnFőmérnükség = new System.Windows.Forms.Button();
            this.BtnSap = new System.Windows.Forms.Button();
            this.BtnFrissít = new System.Windows.Forms.Button();
            this.BtnRészletes = new System.Windows.Forms.Button();
            this.BtnVonal = new System.Windows.Forms.Button();
            this.BtnHavi = new System.Windows.Forms.Button();
            this.Dátum = new System.Windows.Forms.DateTimePicker();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.CheckBox1 = new System.Windows.Forms.CheckBox();
            this.Lstszolgálatok = new System.Windows.Forms.CheckedListBox();
            this.Lstüzemek = new System.Windows.Forms.CheckedListBox();
            this.BtnNapilista = new System.Windows.Forms.Button();
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.DátumTól = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.DátumIg = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Pályaszámok = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbtelephely1 = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Panel2.SuspendLayout();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.Button4);
            this.Panel2.Controls.Add(this.Button3);
            this.Panel2.Controls.Add(this.Button2);
            this.Panel2.Controls.Add(this.Button1);
            this.Panel2.Location = new System.Drawing.Point(6, 91);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(214, 62);
            this.Panel2.TabIndex = 16;
            // 
            // Button4
            // 
            this.Button4.BackgroundImage = global::Villamos.Properties.Resources.email;
            this.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Button4.Location = new System.Drawing.Point(159, 9);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(45, 45);
            this.Button4.TabIndex = 4;
            this.ToolTip1.SetToolTip(this.Button4, "Emailt elküldi a terjesztési listának megfelelően");
            this.Button4.UseVisualStyleBackColor = true;
            this.Button4.Visible = false;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // Button3
            // 
            this.Button3.BackgroundImage = global::Villamos.Properties.Resources._3B;
            this.Button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Button3.Location = new System.Drawing.Point(108, 9);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(45, 45);
            this.Button3.TabIndex = 2;
            this.ToolTip1.SetToolTip(this.Button3, "Előre definiált Excel táblába kiírja az adatokat szakszolgálatonként");
            this.Button3.UseVisualStyleBackColor = true;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Button2
            // 
            this.Button2.BackgroundImage = global::Villamos.Properties.Resources._2B;
            this.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Button2.Location = new System.Drawing.Point(57, 9);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(45, 45);
            this.Button2.TabIndex = 1;
            this.ToolTip1.SetToolTip(this.Button2, "Előre definiált Excel táblába kiírja az adatokat szakszolgálatonként");
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button1
            // 
            this.Button1.BackgroundImage = global::Villamos.Properties.Resources._1B;
            this.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Button1.Location = new System.Drawing.Point(6, 9);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(45, 45);
            this.Button1.TabIndex = 0;
            this.ToolTip1.SetToolTip(this.Button1, "Előre definiált Excel táblába kiírja az adatokat szakszolgálatonként");
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1111, 3);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 175;
            this.ToolTip1.SetToolTip(this.BtnSúgó, "Súgó");
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // Btnexcel
            // 
            this.Btnexcel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Btnexcel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Btnexcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btnexcel.Location = new System.Drawing.Point(153, 2);
            this.Btnexcel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btnexcel.Name = "Btnexcel";
            this.Btnexcel.Size = new System.Drawing.Size(44, 45);
            this.Btnexcel.TabIndex = 211;
            this.ToolTip1.SetToolTip(this.Btnexcel, "Táblázat adatait excelbe menti");
            this.Btnexcel.UseVisualStyleBackColor = true;
            this.Btnexcel.Click += new System.EventHandler(this.Btnexcel_Click);
            // 
            // BtnFőmérnükség
            // 
            this.BtnFőmérnükség.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnFőmérnükség.BackgroundImage = global::Villamos.Properties.Resources.Google_Noto_Emoji_Travel_Places_42498_factory;
            this.BtnFőmérnükség.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnFőmérnükség.Location = new System.Drawing.Point(53, 2);
            this.BtnFőmérnükség.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnFőmérnükség.Name = "BtnFőmérnükség";
            this.BtnFőmérnükség.Size = new System.Drawing.Size(44, 45);
            this.BtnFőmérnükség.TabIndex = 212;
            this.ToolTip1.SetToolTip(this.BtnFőmérnükség, "Főmérnökségi lekérdezés");
            this.BtnFőmérnükség.UseVisualStyleBackColor = true;
            this.BtnFőmérnükség.Click += new System.EventHandler(this.BtnFőmérnükség_Click);
            // 
            // BtnSap
            // 
            this.BtnSap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSap.BackgroundImage = global::Villamos.Properties.Resources.SAP;
            this.BtnSap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSap.Location = new System.Drawing.Point(3, 2);
            this.BtnSap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnSap.Name = "BtnSap";
            this.BtnSap.Size = new System.Drawing.Size(44, 45);
            this.BtnSap.TabIndex = 213;
            this.ToolTip1.SetToolTip(this.BtnSap, "SAP adatok betöltése");
            this.BtnSap.UseVisualStyleBackColor = true;
            this.BtnSap.Click += new System.EventHandler(this.BtnSap_Click);
            // 
            // BtnFrissít
            // 
            this.BtnFrissít.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnFrissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.BtnFrissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnFrissít.Location = new System.Drawing.Point(822, 2);
            this.BtnFrissít.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnFrissít.Name = "BtnFrissít";
            this.BtnFrissít.Size = new System.Drawing.Size(44, 45);
            this.BtnFrissít.TabIndex = 214;
            this.ToolTip1.SetToolTip(this.BtnFrissít, "Táblázat adatait excelbe menti");
            this.BtnFrissít.UseVisualStyleBackColor = true;
            this.BtnFrissít.Click += new System.EventHandler(this.BtnFrissít_Click);
            // 
            // BtnRészletes
            // 
            this.BtnRészletes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnRészletes.BackgroundImage = global::Villamos.Properties.Resources.Nagyító;
            this.BtnRészletes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnRészletes.Location = new System.Drawing.Point(103, 2);
            this.BtnRészletes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnRészletes.Name = "BtnRészletes";
            this.BtnRészletes.Size = new System.Drawing.Size(44, 45);
            this.BtnRészletes.TabIndex = 216;
            this.ToolTip1.SetToolTip(this.BtnRészletes, "Részletes adatok megjelenítése");
            this.BtnRészletes.UseVisualStyleBackColor = true;
            this.BtnRészletes.Click += new System.EventHandler(this.BtnRészletes_Click);
            // 
            // BtnVonal
            // 
            this.BtnVonal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnVonal.BackgroundImage = global::Villamos.Properties.Resources.App_spreadsheet1;
            this.BtnVonal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnVonal.Location = new System.Drawing.Point(922, 2);
            this.BtnVonal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnVonal.Name = "BtnVonal";
            this.BtnVonal.Size = new System.Drawing.Size(44, 45);
            this.BtnVonal.TabIndex = 217;
            this.ToolTip1.SetToolTip(this.BtnVonal, "Havi vonalas listát készít");
            this.BtnVonal.UseVisualStyleBackColor = true;
            this.BtnVonal.Click += new System.EventHandler(this.BtnVonal_Click);
            // 
            // BtnHavi
            // 
            this.BtnHavi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnHavi.BackgroundImage = global::Villamos.Properties.Resources.Aha_Soft_Large_Seo_SEO;
            this.BtnHavi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnHavi.Location = new System.Drawing.Point(972, 2);
            this.BtnHavi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnHavi.Name = "BtnHavi";
            this.BtnHavi.Size = new System.Drawing.Size(44, 45);
            this.BtnHavi.TabIndex = 218;
            this.ToolTip1.SetToolTip(this.BtnHavi, "Havi ABC Listát készít");
            this.BtnHavi.UseVisualStyleBackColor = true;
            this.BtnHavi.Click += new System.EventHandler(this.BtnHavi_Click);
            // 
            // Dátum
            // 
            this.Dátum.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Dátum.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum.Location = new System.Drawing.Point(45, 50);
            this.Dátum.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Dátum.Name = "Dátum";
            this.Dátum.Size = new System.Drawing.Size(122, 26);
            this.Dátum.TabIndex = 1;
            this.Dátum.ValueChanged += new System.EventHandler(this.Dátum_ValueChanged);
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.CheckBox1);
            this.Panel1.Controls.Add(this.Lstszolgálatok);
            this.Panel1.Controls.Add(this.Lstüzemek);
            this.Panel1.Controls.Add(this.BtnNapilista);
            this.Panel1.Location = new System.Drawing.Point(6, 159);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(217, 439);
            this.Panel1.TabIndex = 17;
            // 
            // CheckBox1
            // 
            this.CheckBox1.AutoSize = true;
            this.CheckBox1.Checked = true;
            this.CheckBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CheckBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CheckBox1.Location = new System.Drawing.Point(39, 362);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(142, 22);
            this.CheckBox1.TabIndex = 10;
            this.CheckBox1.Text = "Csak események";
            this.CheckBox1.UseVisualStyleBackColor = true;
            // 
            // Lstszolgálatok
            // 
            this.Lstszolgálatok.CheckOnClick = true;
            this.Lstszolgálatok.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Lstszolgálatok.FormattingEnabled = true;
            this.Lstszolgálatok.Location = new System.Drawing.Point(3, 6);
            this.Lstszolgálatok.Name = "Lstszolgálatok";
            this.Lstszolgálatok.Size = new System.Drawing.Size(204, 88);
            this.Lstszolgálatok.TabIndex = 9;
            this.Lstszolgálatok.SelectedIndexChanged += new System.EventHandler(this.Lstszolgálatok_SelectedIndexChanged);
            // 
            // Lstüzemek
            // 
            this.Lstüzemek.CheckOnClick = true;
            this.Lstüzemek.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Lstüzemek.FormattingEnabled = true;
            this.Lstüzemek.Location = new System.Drawing.Point(3, 100);
            this.Lstüzemek.Name = "Lstüzemek";
            this.Lstüzemek.Size = new System.Drawing.Size(204, 256);
            this.Lstüzemek.TabIndex = 8;
            // 
            // BtnNapilista
            // 
            this.BtnNapilista.BackColor = System.Drawing.Color.Silver;
            this.BtnNapilista.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.BtnNapilista.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BtnNapilista.Location = new System.Drawing.Point(19, 391);
            this.BtnNapilista.Margin = new System.Windows.Forms.Padding(4);
            this.BtnNapilista.Name = "BtnNapilista";
            this.BtnNapilista.Size = new System.Drawing.Size(178, 32);
            this.BtnNapilista.TabIndex = 0;
            this.BtnNapilista.Text = "Napi lista";
            this.BtnNapilista.UseVisualStyleBackColor = false;
            this.BtnNapilista.Click += new System.EventHandler(this.BtnNapilista_Click);
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.EnableHeadersVisualStyles = false;
            this.Tábla.Location = new System.Drawing.Point(229, 99);
            this.Tábla.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Tábla.Name = "Tábla";
            this.Tábla.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Tábla.Size = new System.Drawing.Size(1164, 504);
            this.Tábla.TabIndex = 18;
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.BackColor = System.Drawing.Color.ForestGreen;
            this.Holtart.ForeColor = System.Drawing.Color.SpringGreen;
            this.Holtart.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Holtart.Location = new System.Drawing.Point(353, 3);
            this.Holtart.Maximum = 10;
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(973, 23);
            this.Holtart.TabIndex = 21;
            this.Holtart.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 627F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Holtart, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1329, 40);
            this.tableLayoutPanel1.TabIndex = 176;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.Cmbtelephely);
            this.panel3.Controls.Add(this.Label13);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(335, 35);
            this.panel3.TabIndex = 176;
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
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 14;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox3, 7, 0);
            this.tableLayoutPanel2.Controls.Add(this.BtnVonal, 10, 0);
            this.tableLayoutPanel2.Controls.Add(this.BtnSap, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.BtnHavi, 11, 0);
            this.tableLayoutPanel2.Controls.Add(this.Btnexcel, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.BtnFrissít, 8, 0);
            this.tableLayoutPanel2.Controls.Add(this.BtnSúgó, 13, 0);
            this.tableLayoutPanel2.Controls.Add(this.BtnFőmérnükség, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.BtnRészletes, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 5, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(234, 40);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1159, 50);
            this.tableLayoutPanel2.TabIndex = 177;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.DátumTól);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.DátumIg);
            this.groupBox3.Location = new System.Drawing.Point(565, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(251, 54);
            this.groupBox3.TabIndex = 215;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Dátum szűrő";
            // 
            // DátumTól
            // 
            this.DátumTól.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DátumTól.Location = new System.Drawing.Point(6, 22);
            this.DátumTól.Name = "DátumTól";
            this.DátumTól.Size = new System.Drawing.Size(106, 26);
            this.DátumTól.TabIndex = 1;
            this.DátumTól.ValueChanged += new System.EventHandler(this.DátumTól_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(118, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 20);
            this.label1.TabIndex = 216;
            this.label1.Text = "-";
            // 
            // DátumIg
            // 
            this.DátumIg.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DátumIg.Location = new System.Drawing.Point(138, 22);
            this.DátumIg.Name = "DátumIg";
            this.DátumIg.Size = new System.Drawing.Size(106, 26);
            this.DátumIg.TabIndex = 2;
            this.DátumIg.ValueChanged += new System.EventHandler(this.DátumIg_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.Pályaszámok);
            this.groupBox2.Location = new System.Drawing.Point(428, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(131, 54);
            this.groupBox2.TabIndex = 214;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pályaszám";
            // 
            // Pályaszámok
            // 
            this.Pályaszámok.FormattingEnabled = true;
            this.Pályaszámok.Location = new System.Drawing.Point(7, 20);
            this.Pályaszámok.Name = "Pályaszámok";
            this.Pályaszámok.Size = new System.Drawing.Size(117, 28);
            this.Pályaszámok.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cmbtelephely1);
            this.groupBox1.Location = new System.Drawing.Point(223, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(199, 54);
            this.groupBox1.TabIndex = 214;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Telephely";
            // 
            // cmbtelephely1
            // 
            this.cmbtelephely1.FormattingEnabled = true;
            this.cmbtelephely1.Location = new System.Drawing.Point(6, 20);
            this.cmbtelephely1.Name = "cmbtelephely1";
            this.cmbtelephely1.Size = new System.Drawing.Size(186, 28);
            this.cmbtelephely1.TabIndex = 19;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // AblakMenetkimaradás
            // 
            this.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.ClientSize = new System.Drawing.Size(1397, 610);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Dátum);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.Tábla);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AblakMenetkimaradás";
            this.Text = "Menetkimaradás karbantartás és lekérdezés";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AblakMenetkimaradás_FormClosed);
            this.Load += new System.EventHandler(this.Menetkimaradás_Load);
            this.Panel2.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        internal Panel Panel2;
        internal Button Button4;
        internal Button Button3;
        internal Button Button2;
        internal Button Button1;
        internal ToolTip ToolTip1;
        internal DateTimePicker Dátum;
        internal Panel Panel1;
        internal CheckBox CheckBox1;
        internal CheckedListBox Lstszolgálatok;
        internal CheckedListBox Lstüzemek;
        internal Button BtnNapilista;
        internal DataGridView Tábla;
        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal Button BtnSúgó;
        private TableLayoutPanel tableLayoutPanel1;
        internal Panel panel3;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal Button Btnexcel;
        internal Button BtnFőmérnükség;
        internal Button BtnSap;
        private TableLayoutPanel tableLayoutPanel2;
        private DateTimePicker DátumTól;
        private ComboBox Pályaszámok;
        private Timer timer1;
        internal ComboBox cmbtelephely1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        internal Button BtnFrissít;
        private GroupBox groupBox3;
        private Label label1;
        private DateTimePicker DátumIg;
        internal Button BtnRészletes;
        internal Button BtnVonal;
        internal Button BtnHavi;
    }
}