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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AblakMenetkimaradás));
            this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.LekérdezésekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TelephelyVálasztóToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbtelephely1 = new System.Windows.Forms.ToolStripComboBox();
            this.ToolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.NapiListaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HaviLlistaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.AdatRészletesMegjelenítéseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.VonalasListaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HaviÖsszesítőToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
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
            this.Dátum = new System.Windows.Forms.DateTimePicker();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.CheckBox1 = new System.Windows.Forms.CheckBox();
            this.Lstszolgálatok = new System.Windows.Forms.CheckedListBox();
            this.Lstüzemek = new System.Windows.Forms.CheckedListBox();
            this.BtnNapilista = new System.Windows.Forms.Button();
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.txtSorszám = new System.Windows.Forms.TextBox();
            this.txthely = new System.Windows.Forms.TextBox();
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.alsópanels1 = new System.Windows.Forms.TextBox();
            this.alsópanels2 = new System.Windows.Forms.TextBox();
            this.alsópanels3 = new System.Windows.Forms.TextBox();
            this.alsópanels5 = new System.Windows.Forms.TextBox();
            this.alsópanels6 = new System.Windows.Forms.TextBox();
            this.alsópanels4 = new System.Windows.Forms.TextBox();
            this.CheckBox2 = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.Pályaszámok = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.MenuStrip1.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip1
            // 
            this.MenuStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LekérdezésekToolStripMenuItem});
            this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip1.Name = "MenuStrip1";
            this.MenuStrip1.Size = new System.Drawing.Size(1047, 26);
            this.MenuStrip1.TabIndex = 0;
            this.MenuStrip1.Text = "menuStrip1";
            // 
            // LekérdezésekToolStripMenuItem
            // 
            this.LekérdezésekToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TelephelyVálasztóToolStripMenuItem,
            this.cmbtelephely1,
            this.ToolStripSeparator5,
            this.NapiListaToolStripMenuItem,
            this.HaviLlistaToolStripMenuItem,
            this.toolStripSeparator6,
            this.AdatRészletesMegjelenítéseToolStripMenuItem,
            this.ToolStripSeparator3,
            this.VonalasListaToolStripMenuItem,
            this.HaviÖsszesítőToolStripMenuItem,
            this.ToolStripSeparator1,
            this.toolStripSeparator7});
            this.LekérdezésekToolStripMenuItem.Name = "LekérdezésekToolStripMenuItem";
            this.LekérdezésekToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.LekérdezésekToolStripMenuItem.Text = "&Lekérdezések";
            // 
            // TelephelyVálasztóToolStripMenuItem
            // 
            this.TelephelyVálasztóToolStripMenuItem.Name = "TelephelyVálasztóToolStripMenuItem";
            this.TelephelyVálasztóToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.TelephelyVálasztóToolStripMenuItem.Text = "Telephely választó:";
            // 
            // cmbtelephely1
            // 
            this.cmbtelephely1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cmbtelephely1.Name = "cmbtelephely1";
            this.cmbtelephely1.Size = new System.Drawing.Size(121, 29);
            // 
            // ToolStripSeparator5
            // 
            this.ToolStripSeparator5.Name = "ToolStripSeparator5";
            this.ToolStripSeparator5.Size = new System.Drawing.Size(260, 6);
            // 
            // NapiListaToolStripMenuItem
            // 
            this.NapiListaToolStripMenuItem.Name = "NapiListaToolStripMenuItem";
            this.NapiListaToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.NapiListaToolStripMenuItem.Text = "Napi lista";
            this.NapiListaToolStripMenuItem.Click += new System.EventHandler(this.NapiListaToolStripMenuItem_Click);
            // 
            // HaviLlistaToolStripMenuItem
            // 
            this.HaviLlistaToolStripMenuItem.Name = "HaviLlistaToolStripMenuItem";
            this.HaviLlistaToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.HaviLlistaToolStripMenuItem.Text = "&Havi lista";
            this.HaviLlistaToolStripMenuItem.Click += new System.EventHandler(this.HaviLlistaToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(260, 6);
            // 
            // AdatRészletesMegjelenítéseToolStripMenuItem
            // 
            this.AdatRészletesMegjelenítéseToolStripMenuItem.Name = "AdatRészletesMegjelenítéseToolStripMenuItem";
            this.AdatRészletesMegjelenítéseToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.AdatRészletesMegjelenítéseToolStripMenuItem.Text = "Adat részletes megjelenítése";
            this.AdatRészletesMegjelenítéseToolStripMenuItem.Click += new System.EventHandler(this.AdatRészletesMegjelenítéseToolStripMenuItem_Click);
            // 
            // ToolStripSeparator3
            // 
            this.ToolStripSeparator3.Name = "ToolStripSeparator3";
            this.ToolStripSeparator3.Size = new System.Drawing.Size(260, 6);
            // 
            // VonalasListaToolStripMenuItem
            // 
            this.VonalasListaToolStripMenuItem.Name = "VonalasListaToolStripMenuItem";
            this.VonalasListaToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.VonalasListaToolStripMenuItem.Text = "&Vonalas lista";
            this.VonalasListaToolStripMenuItem.Click += new System.EventHandler(this.VonalasListaToolStripMenuItem_Click);
            // 
            // HaviÖsszesítőToolStripMenuItem
            // 
            this.HaviÖsszesítőToolStripMenuItem.Name = "HaviÖsszesítőToolStripMenuItem";
            this.HaviÖsszesítőToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.HaviÖsszesítőToolStripMenuItem.Text = "H&avi összesítő";
            this.HaviÖsszesítőToolStripMenuItem.Click += new System.EventHandler(this.HaviÖsszesítőToolStripMenuItem_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(260, 6);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(260, 6);
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.Button4);
            this.Panel2.Controls.Add(this.Button3);
            this.Panel2.Controls.Add(this.Button2);
            this.Panel2.Controls.Add(this.Button1);
            this.Panel2.Location = new System.Drawing.Point(6, 227);
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
            this.BtnSúgó.Location = new System.Drawing.Point(981, 3);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 175;
            this.ToolTip1.SetToolTip(this.BtnSúgó, "Súgó");
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // Btnexcel
            // 
            this.Btnexcel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Btnexcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btnexcel.Location = new System.Drawing.Point(644, 2);
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
            this.BtnFőmérnükség.BackgroundImage = global::Villamos.Properties.Resources.Google_Noto_Emoji_Travel_Places_42498_factory;
            this.BtnFőmérnükség.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnFőmérnükség.Location = new System.Drawing.Point(594, 2);
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
            this.BtnSap.BackgroundImage = global::Villamos.Properties.Resources.SAP;
            this.BtnSap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSap.Location = new System.Drawing.Point(344, 2);
            this.BtnSap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnSap.Name = "BtnSap";
            this.BtnSap.Size = new System.Drawing.Size(44, 45);
            this.BtnSap.TabIndex = 213;
            this.ToolTip1.SetToolTip(this.BtnSap, "SAP adatok betöltése");
            this.BtnSap.UseVisualStyleBackColor = true;
            this.BtnSap.Click += new System.EventHandler(this.BtnSap_Click);
            // 
            // Dátum
            // 
            this.Dátum.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Dátum.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum.Location = new System.Drawing.Point(6, 196);
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
            this.Panel1.Location = new System.Drawing.Point(6, 295);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(217, 445);
            this.Panel1.TabIndex = 17;
            // 
            // CheckBox1
            // 
            this.CheckBox1.AutoSize = true;
            this.CheckBox1.Checked = true;
            this.CheckBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CheckBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CheckBox1.Location = new System.Drawing.Point(39, 373);
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
            this.Lstüzemek.Location = new System.Drawing.Point(3, 111);
            this.Lstüzemek.Name = "Lstüzemek";
            this.Lstüzemek.Size = new System.Drawing.Size(204, 256);
            this.Lstüzemek.TabIndex = 8;
            // 
            // BtnNapilista
            // 
            this.BtnNapilista.BackColor = System.Drawing.Color.Silver;
            this.BtnNapilista.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.BtnNapilista.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BtnNapilista.Location = new System.Drawing.Point(22, 402);
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
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.EnableHeadersVisualStyles = false;
            this.Tábla.Location = new System.Drawing.Point(226, 152);
            this.Tábla.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Tábla.Name = "Tábla";
            this.Tábla.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.Tábla.Size = new System.Drawing.Size(814, 589);
            this.Tábla.TabIndex = 18;
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            // 
            // txtSorszám
            // 
            this.txtSorszám.Location = new System.Drawing.Point(238, 468);
            this.txtSorszám.Name = "txtSorszám";
            this.txtSorszám.Size = new System.Drawing.Size(100, 26);
            this.txtSorszám.TabIndex = 19;
            this.txtSorszám.Visible = false;
            // 
            // txthely
            // 
            this.txthely.Location = new System.Drawing.Point(238, 500);
            this.txthely.Name = "txthely";
            this.txthely.Size = new System.Drawing.Size(100, 26);
            this.txthely.TabIndex = 20;
            this.txthely.Visible = false;
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.BackColor = System.Drawing.Color.ForestGreen;
            this.Holtart.ForeColor = System.Drawing.Color.SpringGreen;
            this.Holtart.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Holtart.Location = new System.Drawing.Point(241, 218);
            this.Holtart.Maximum = 10;
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(771, 23);
            this.Holtart.TabIndex = 21;
            this.Holtart.Visible = false;
            // 
            // alsópanels1
            // 
            this.alsópanels1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.alsópanels1.Location = new System.Drawing.Point(238, 532);
            this.alsópanels1.Name = "alsópanels1";
            this.alsópanels1.Size = new System.Drawing.Size(100, 24);
            this.alsópanels1.TabIndex = 16;
            this.alsópanels1.Visible = false;
            // 
            // alsópanels2
            // 
            this.alsópanels2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.alsópanels2.Location = new System.Drawing.Point(238, 562);
            this.alsópanels2.Name = "alsópanels2";
            this.alsópanels2.Size = new System.Drawing.Size(100, 24);
            this.alsópanels2.TabIndex = 17;
            this.alsópanels2.Visible = false;
            // 
            // alsópanels3
            // 
            this.alsópanels3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.alsópanels3.Location = new System.Drawing.Point(241, 606);
            this.alsópanels3.Name = "alsópanels3";
            this.alsópanels3.Size = new System.Drawing.Size(100, 24);
            this.alsópanels3.TabIndex = 20;
            this.alsópanels3.Visible = false;
            // 
            // alsópanels5
            // 
            this.alsópanels5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.alsópanels5.Location = new System.Drawing.Point(229, 666);
            this.alsópanels5.Name = "alsópanels5";
            this.alsópanels5.Size = new System.Drawing.Size(100, 24);
            this.alsópanels5.TabIndex = 22;
            this.alsópanels5.Visible = false;
            // 
            // alsópanels6
            // 
            this.alsópanels6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.alsópanels6.Location = new System.Drawing.Point(229, 705);
            this.alsópanels6.Name = "alsópanels6";
            this.alsópanels6.Size = new System.Drawing.Size(100, 24);
            this.alsópanels6.TabIndex = 23;
            this.alsópanels6.Visible = false;
            // 
            // alsópanels4
            // 
            this.alsópanels4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.alsópanels4.Location = new System.Drawing.Point(238, 636);
            this.alsópanels4.Name = "alsópanels4";
            this.alsópanels4.Size = new System.Drawing.Size(100, 24);
            this.alsópanels4.TabIndex = 21;
            this.alsópanels4.Visible = false;
            // 
            // CheckBox2
            // 
            this.CheckBox2.AutoSize = true;
            this.CheckBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CheckBox2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CheckBox2.Location = new System.Drawing.Point(631, 686);
            this.CheckBox2.Name = "CheckBox2";
            this.CheckBox2.Size = new System.Drawing.Size(104, 22);
            this.CheckBox2.TabIndex = 18;
            this.CheckBox2.Text = "CheckBox2";
            this.CheckBox2.UseVisualStyleBackColor = true;
            this.CheckBox2.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 13;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Controls.Add(this.BtnSap, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Btnexcel, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.BtnFőmérnükség, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.BtnSúgó, 12, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 38);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1029, 51);
            this.tableLayoutPanel1.TabIndex = 176;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.Cmbtelephely);
            this.panel3.Controls.Add(this.Label13);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(335, 33);
            this.panel3.TabIndex = 176;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(143, 0);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
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
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.Pályaszámok, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 95);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1026, 52);
            this.tableLayoutPanel2.TabIndex = 177;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dateTimePicker1);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(200, 46);
            this.panel4.TabIndex = 0;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(60, 9);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(137, 26);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // Pályaszámok
            // 
            this.Pályaszámok.FormattingEnabled = true;
            this.Pályaszámok.Location = new System.Drawing.Point(506, 3);
            this.Pályaszámok.Name = "Pályaszámok";
            this.Pályaszámok.Size = new System.Drawing.Size(117, 28);
            this.Pályaszámok.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // AblakMenetkimaradás
            // 
            this.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.ClientSize = new System.Drawing.Size(1047, 752);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.CheckBox2);
            this.Controls.Add(this.alsópanels4);
            this.Controls.Add(this.alsópanels6);
            this.Controls.Add(this.alsópanels5);
            this.Controls.Add(this.alsópanels3);
            this.Controls.Add(this.alsópanels2);
            this.Controls.Add(this.alsópanels1);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.txthely);
            this.Controls.Add(this.txtSorszám);
            this.Controls.Add(this.Tábla);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Dátum);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.MenuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuStrip1;
            this.Name = "AblakMenetkimaradás";
            this.Text = "Menetkimaradás karbantartás és lekérdezés";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AblakMenetkimaradás_FormClosed);
            this.Load += new System.EventHandler(this.Menetkimaradás_Load);
            this.MenuStrip1.ResumeLayout(false);
            this.MenuStrip1.PerformLayout();
            this.Panel2.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal MenuStrip MenuStrip1;
        internal Panel Panel2;
        internal Button Button4;
        internal Button Button3;
        internal Button Button2;
        internal Button Button1;
        internal ToolTip ToolTip1;
        internal ToolStripMenuItem LekérdezésekToolStripMenuItem;
        internal ToolStripMenuItem TelephelyVálasztóToolStripMenuItem;
        internal ToolStripComboBox cmbtelephely1;
        internal ToolStripSeparator ToolStripSeparator5;
        internal ToolStripMenuItem NapiListaToolStripMenuItem;
        internal ToolStripMenuItem HaviLlistaToolStripMenuItem;
        internal ToolStripMenuItem VonalasListaToolStripMenuItem;
        internal ToolStripMenuItem HaviÖsszesítőToolStripMenuItem;
        internal ToolStripSeparator ToolStripSeparator3;
        internal DateTimePicker Dátum;
        internal Panel Panel1;
        internal CheckBox CheckBox1;
        internal CheckedListBox Lstszolgálatok;
        internal CheckedListBox Lstüzemek;
        internal Button BtnNapilista;
        internal DataGridView Tábla;
        internal TextBox txtSorszám;
        internal TextBox txthely;
        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal TextBox alsópanels1;
        internal TextBox alsópanels2;
        internal TextBox alsópanels3;
        internal TextBox alsópanels5;
        internal TextBox alsópanels6;
        internal TextBox alsópanels4;
        internal CheckBox CheckBox2;
        internal ToolStripSeparator ToolStripSeparator1;
        internal ToolStripSeparator toolStripSeparator6;
        internal ToolStripMenuItem AdatRészletesMegjelenítéseToolStripMenuItem;
        internal ToolStripSeparator toolStripSeparator7;
        internal Button BtnSúgó;
        private TableLayoutPanel tableLayoutPanel1;
        internal Panel panel3;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal Button Btnexcel;
        internal Button BtnFőmérnükség;
        internal Button BtnSap;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel panel4;
        private DateTimePicker dateTimePicker1;
        private Label label1;
        private ComboBox Pályaszámok;
        private Timer timer1;
    }
}