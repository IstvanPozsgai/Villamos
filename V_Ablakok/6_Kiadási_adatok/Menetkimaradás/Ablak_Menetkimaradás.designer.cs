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
            this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.RögzítésekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbtelephely = new System.Windows.Forms.ToolStripComboBox();
            this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.SAPAdatokBetöltéseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
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
            this.PályaszámraTörténőLekérdezésToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Pályaszámok = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.FőmérnökségiLekérdezésToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SúgóToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Button4 = new System.Windows.Forms.Button();
            this.Button3 = new System.Windows.Forms.Button();
            this.Button2 = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Dátum = new System.Windows.Forms.DateTimePicker();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.CheckBox1 = new System.Windows.Forms.CheckBox();
            this.Lstszolgálatok = new System.Windows.Forms.CheckedListBox();
            this.Lstüzemek = new System.Windows.Forms.CheckedListBox();
            this.BtnNapilista = new System.Windows.Forms.Button();
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.txtSorszám = new System.Windows.Forms.TextBox();
            this.txthely = new System.Windows.Forms.TextBox();
            this.Holtart = new V_MindenEgyéb.MyProgressbar();
            this.alsópanels1 = new System.Windows.Forms.TextBox();
            this.alsópanels2 = new System.Windows.Forms.TextBox();
            this.alsópanels3 = new System.Windows.Forms.TextBox();
            this.alsópanels5 = new System.Windows.Forms.TextBox();
            this.alsópanels6 = new System.Windows.Forms.TextBox();
            this.alsópanels4 = new System.Windows.Forms.TextBox();
            this.CheckBox2 = new System.Windows.Forms.CheckBox();
            this.MenuStrip1.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // MenuStrip1
            // 
            this.MenuStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RögzítésekToolStripMenuItem,
            this.LekérdezésekToolStripMenuItem,
            this.FőmérnökségiLekérdezésToolStripMenuItem,
            this.ExcelToolStripMenuItem,
            this.SúgóToolStripMenuItem});
            this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip1.Name = "MenuStrip1";
            this.MenuStrip1.Size = new System.Drawing.Size(822, 26);
            this.MenuStrip1.TabIndex = 0;
            this.MenuStrip1.Text = "menuStrip1";
            // 
            // RögzítésekToolStripMenuItem
            // 
            this.RögzítésekToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem1,
            this.cmbtelephely,
            this.ToolStripSeparator4,
            this.SAPAdatokBetöltéseToolStripMenuItem,
            this.ToolStripSeparator2});
            this.RögzítésekToolStripMenuItem.Name = "RögzítésekToolStripMenuItem";
            this.RögzítésekToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
            this.RögzítésekToolStripMenuItem.Text = "&Rögzítések";
            // 
            // ToolStripMenuItem1
            // 
            this.ToolStripMenuItem1.Enabled = false;
            this.ToolStripMenuItem1.Name = "ToolStripMenuItem1";
            this.ToolStripMenuItem1.Size = new System.Drawing.Size(218, 22);
            this.ToolStripMenuItem1.Text = "Telephely választás:";
            // 
            // cmbtelephely
            // 
            this.cmbtelephely.DropDownHeight = 200;
            this.cmbtelephely.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cmbtelephely.IntegralHeight = false;
            this.cmbtelephely.Name = "cmbtelephely";
            this.cmbtelephely.Size = new System.Drawing.Size(150, 29);
            // 
            // ToolStripSeparator4
            // 
            this.ToolStripSeparator4.Name = "ToolStripSeparator4";
            this.ToolStripSeparator4.Size = new System.Drawing.Size(215, 6);
            // 
            // SAPAdatokBetöltéseToolStripMenuItem
            // 
            this.SAPAdatokBetöltéseToolStripMenuItem.Name = "SAPAdatokBetöltéseToolStripMenuItem";
            this.SAPAdatokBetöltéseToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.SAPAdatokBetöltéseToolStripMenuItem.Text = "&SAP adatok betöltése";
            this.SAPAdatokBetöltéseToolStripMenuItem.Click += new System.EventHandler(this.SAPAdatokBetöltéseToolStripMenuItem_Click);
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(215, 6);
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
            this.PályaszámraTörténőLekérdezésToolStripMenuItem,
            this.Pályaszámok,
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
            this.AdatRészletesMegjelenítéseToolStripMenuItem.Enabled = false;
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
            // PályaszámraTörténőLekérdezésToolStripMenuItem
            // 
            this.PályaszámraTörténőLekérdezésToolStripMenuItem.Enabled = false;
            this.PályaszámraTörténőLekérdezésToolStripMenuItem.Name = "PályaszámraTörténőLekérdezésToolStripMenuItem";
            this.PályaszámraTörténőLekérdezésToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.PályaszámraTörténőLekérdezésToolStripMenuItem.Text = "Pályaszám választás:";
            // 
            // Pályaszámok
            // 
            this.Pályaszámok.DropDownHeight = 200;
            this.Pályaszámok.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Pályaszámok.IntegralHeight = false;
            this.Pályaszámok.Name = "Pályaszámok";
            this.Pályaszámok.Size = new System.Drawing.Size(121, 29);
            this.Pályaszámok.Click += new System.EventHandler(this.Pályaszámok_Click);
            this.Pályaszámok.TextChanged += new System.EventHandler(this.Pályaszámok_TextChanged);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(260, 6);
            // 
            // FőmérnökségiLekérdezésToolStripMenuItem
            // 
            this.FőmérnökségiLekérdezésToolStripMenuItem.Name = "FőmérnökségiLekérdezésToolStripMenuItem";
            this.FőmérnökségiLekérdezésToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.FőmérnökségiLekérdezésToolStripMenuItem.Text = "&Főmérnökségi lekérdezés";
            this.FőmérnökségiLekérdezésToolStripMenuItem.Click += new System.EventHandler(this.FőmérnökségiLekérdezésToolStripMenuItem_Click);
            // 
            // ExcelToolStripMenuItem
            // 
            this.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem";
            this.ExcelToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.ExcelToolStripMenuItem.Text = "&Excel exportálás";
            this.ExcelToolStripMenuItem.Click += new System.EventHandler(this.ExcelToolStripMenuItem_Click);
            // 
            // SúgóToolStripMenuItem
            // 
            this.SúgóToolStripMenuItem.Name = "SúgóToolStripMenuItem";
            this.SúgóToolStripMenuItem.Size = new System.Drawing.Size(55, 22);
            this.SúgóToolStripMenuItem.Text = "Súgó";
            this.SúgóToolStripMenuItem.Click += new System.EventHandler(this.SúgóToolStripMenuItem_Click);
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.Button4);
            this.Panel2.Controls.Add(this.Button3);
            this.Panel2.Controls.Add(this.Button2);
            this.Panel2.Controls.Add(this.Button1);
            this.Panel2.Location = new System.Drawing.Point(6, 66);
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
            // Dátum
            // 
            this.Dátum.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Dátum.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum.Location = new System.Drawing.Point(6, 30);
            this.Dátum.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Dátum.Name = "Dátum";
            this.Dátum.Size = new System.Drawing.Size(178, 26);
            this.Dátum.TabIndex = 1;
            this.Dátum.ValueChanged += new System.EventHandler(this.Dátum_ValueChanged);
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.CheckBox1);
            this.Panel1.Controls.Add(this.Lstszolgálatok);
            this.Panel1.Controls.Add(this.Lstüzemek);
            this.Panel1.Controls.Add(this.BtnNapilista);
            this.Panel1.Location = new System.Drawing.Point(3, 136);
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
            this.Tábla.Location = new System.Drawing.Point(226, 30);
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
            this.Tábla.Size = new System.Drawing.Size(589, 586);
            this.Tábla.TabIndex = 18;
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            // 
            // txtSorszám
            // 
            this.txtSorszám.Location = new System.Drawing.Point(240, 65);
            this.txtSorszám.Name = "txtSorszám";
            this.txtSorszám.Size = new System.Drawing.Size(100, 26);
            this.txtSorszám.TabIndex = 19;
            this.txtSorszám.Visible = false;
            // 
            // txthely
            // 
            this.txthely.Location = new System.Drawing.Point(240, 104);
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
            this.Holtart.Location = new System.Drawing.Point(240, 190);
            this.Holtart.Maximum = 10;
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(546, 23);
            this.Holtart.TabIndex = 21;
            this.Holtart.Visible = false;
            // 
            // alsópanels1
            // 
            this.alsópanels1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.alsópanels1.Location = new System.Drawing.Point(250, 247);
            this.alsópanels1.Name = "alsópanels1";
            this.alsópanels1.Size = new System.Drawing.Size(100, 24);
            this.alsópanels1.TabIndex = 16;
            this.alsópanels1.Visible = false;
            // 
            // alsópanels2
            // 
            this.alsópanels2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.alsópanels2.Location = new System.Drawing.Point(250, 277);
            this.alsópanels2.Name = "alsópanels2";
            this.alsópanels2.Size = new System.Drawing.Size(100, 24);
            this.alsópanels2.TabIndex = 17;
            this.alsópanels2.Visible = false;
            // 
            // alsópanels3
            // 
            this.alsópanels3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.alsópanels3.Location = new System.Drawing.Point(250, 307);
            this.alsópanels3.Name = "alsópanels3";
            this.alsópanels3.Size = new System.Drawing.Size(100, 24);
            this.alsópanels3.TabIndex = 20;
            this.alsópanels3.Visible = false;
            // 
            // alsópanels5
            // 
            this.alsópanels5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.alsópanels5.Location = new System.Drawing.Point(250, 368);
            this.alsópanels5.Name = "alsópanels5";
            this.alsópanels5.Size = new System.Drawing.Size(100, 24);
            this.alsópanels5.TabIndex = 22;
            this.alsópanels5.Visible = false;
            // 
            // alsópanels6
            // 
            this.alsópanels6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.alsópanels6.Location = new System.Drawing.Point(250, 398);
            this.alsópanels6.Name = "alsópanels6";
            this.alsópanels6.Size = new System.Drawing.Size(100, 24);
            this.alsópanels6.TabIndex = 23;
            this.alsópanels6.Visible = false;
            // 
            // alsópanels4
            // 
            this.alsópanels4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.alsópanels4.Location = new System.Drawing.Point(250, 337);
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
            this.CheckBox2.Location = new System.Drawing.Point(536, 383);
            this.CheckBox2.Name = "CheckBox2";
            this.CheckBox2.Size = new System.Drawing.Size(104, 22);
            this.CheckBox2.TabIndex = 18;
            this.CheckBox2.Text = "CheckBox2";
            this.CheckBox2.UseVisualStyleBackColor = true;
            this.CheckBox2.Visible = false;
            // 
            // AblakMenetkimaradás
            // 
            this.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.ClientSize = new System.Drawing.Size(822, 619);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal MenuStrip MenuStrip1;
        internal ToolStripMenuItem RögzítésekToolStripMenuItem;
        internal Panel Panel2;
        internal Button Button4;
        internal Button Button3;
        internal Button Button2;
        internal Button Button1;
        internal ToolTip ToolTip1;
        internal ToolStripMenuItem ToolStripMenuItem1;
        internal ToolStripComboBox cmbtelephely;
        internal ToolStripSeparator ToolStripSeparator4;
        internal ToolStripMenuItem SAPAdatokBetöltéseToolStripMenuItem;
        internal ToolStripMenuItem LekérdezésekToolStripMenuItem;
        internal ToolStripMenuItem TelephelyVálasztóToolStripMenuItem;
        internal ToolStripComboBox cmbtelephely1;
        internal ToolStripSeparator ToolStripSeparator5;
        internal ToolStripMenuItem NapiListaToolStripMenuItem;
        internal ToolStripMenuItem HaviLlistaToolStripMenuItem;
        internal ToolStripMenuItem VonalasListaToolStripMenuItem;
        internal ToolStripMenuItem HaviÖsszesítőToolStripMenuItem;
        internal ToolStripSeparator ToolStripSeparator3;
        internal ToolStripMenuItem PályaszámraTörténőLekérdezésToolStripMenuItem;
        internal ToolStripComboBox Pályaszámok;
        internal ToolStripMenuItem FőmérnökségiLekérdezésToolStripMenuItem;
        internal ToolStripMenuItem ExcelToolStripMenuItem;
        internal ToolStripMenuItem SúgóToolStripMenuItem;
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
        internal ToolStripSeparator ToolStripSeparator2;
        internal ToolStripSeparator ToolStripSeparator1;
        internal ToolStripSeparator toolStripSeparator6;
        internal ToolStripMenuItem AdatRészletesMegjelenítéseToolStripMenuItem;
        internal ToolStripSeparator toolStripSeparator7;
    }
}