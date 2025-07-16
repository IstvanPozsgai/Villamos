using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
 
    public partial class Ablak_Ciklus : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && ( components != null))
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Ciklus));
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.Excel_gomb = new System.Windows.Forms.Button();
            this.Lekérdezés_lekérdezés = new System.Windows.Forms.Button();
            this.Rögzít = new System.Windows.Forms.Button();
            this.Töröl = new System.Windows.Forms.Button();
            this.CiklusTípus = new System.Windows.Forms.ComboBox();
            this.Vizsálatsorszám = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Vizsgálatfoka = new System.Windows.Forms.TextBox();
            this.Névleges = new System.Windows.Forms.TextBox();
            this.Alsóeltérés = new System.Windows.Forms.TextBox();
            this.Felsőeltérés = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.CsoportosMásolás = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.ÚjCiklus = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.EnableHeadersVisualStyles = false;
            this.Tábla.Location = new System.Drawing.Point(342, 12);
            this.Tábla.Name = "Tábla";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Tábla.RowHeadersVisible = false;
            this.Tábla.Size = new System.Drawing.Size(555, 502);
            this.Tábla.TabIndex = 168;
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(12, 260);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 169;
            this.toolTip1.SetToolTip(this.BtnSúgó, "Súgó");
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // Excel_gomb
            // 
            this.Excel_gomb.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excel_gomb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excel_gomb.Location = new System.Drawing.Point(12, 310);
            this.Excel_gomb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Excel_gomb.Name = "Excel_gomb";
            this.Excel_gomb.Size = new System.Drawing.Size(45, 45);
            this.Excel_gomb.TabIndex = 170;
            this.toolTip1.SetToolTip(this.Excel_gomb, "Excel táblázatot készít a táblázatból");
            this.Excel_gomb.UseVisualStyleBackColor = true;
            this.Excel_gomb.Click += new System.EventHandler(this.Excel_gomb_Click);
            // 
            // Lekérdezés_lekérdezés
            // 
            this.Lekérdezés_lekérdezés.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Lekérdezés_lekérdezés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lekérdezés_lekérdezés.Location = new System.Drawing.Point(233, 256);
            this.Lekérdezés_lekérdezés.Name = "Lekérdezés_lekérdezés";
            this.Lekérdezés_lekérdezés.Size = new System.Drawing.Size(45, 45);
            this.Lekérdezés_lekérdezés.TabIndex = 171;
            this.toolTip1.SetToolTip(this.Lekérdezés_lekérdezés, "Frissíti a táblázatot");
            this.Lekérdezés_lekérdezés.UseVisualStyleBackColor = true;
            this.Lekérdezés_lekérdezés.Click += new System.EventHandler(this.Lekérdezés_lekérdezés_Click);
            // 
            // Rögzít
            // 
            this.Rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Rögzít.Location = new System.Drawing.Point(291, 256);
            this.Rögzít.Name = "Rögzít";
            this.Rögzít.Size = new System.Drawing.Size(45, 45);
            this.Rögzít.TabIndex = 172;
            this.toolTip1.SetToolTip(this.Rögzít, "Rögzít / Módosít");
            this.Rögzít.UseVisualStyleBackColor = true;
            this.Rögzít.Click += new System.EventHandler(this.Rögzít_Click);
            // 
            // Töröl
            // 
            this.Töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Töröl.Location = new System.Drawing.Point(174, 256);
            this.Töröl.Name = "Töröl";
            this.Töröl.Size = new System.Drawing.Size(45, 45);
            this.Töröl.TabIndex = 173;
            this.toolTip1.SetToolTip(this.Töröl, "Törli az adatokat");
            this.Töröl.UseVisualStyleBackColor = true;
            this.Töröl.Click += new System.EventHandler(this.Töröl_Click);
            // 
            // CiklusTípus
            // 
            this.CiklusTípus.FormattingEnabled = true;
            this.CiklusTípus.Location = new System.Drawing.Point(174, 12);
            this.CiklusTípus.MaxLength = 15;
            this.CiklusTípus.Name = "CiklusTípus";
            this.CiklusTípus.Size = new System.Drawing.Size(162, 28);
            this.CiklusTípus.TabIndex = 174;
            this.CiklusTípus.SelectedIndexChanged += new System.EventHandler(this.CiklusTípus_SelectedIndexChanged);
            // 
            // Vizsálatsorszám
            // 
            this.Vizsálatsorszám.Location = new System.Drawing.Point(174, 56);
            this.Vizsálatsorszám.Name = "Vizsálatsorszám";
            this.Vizsálatsorszám.Size = new System.Drawing.Size(162, 26);
            this.Vizsálatsorszám.TabIndex = 175;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 227);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(104, 20);
            this.Label1.TabIndex = 176;
            this.Label1.Text = "Felső eltérés:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(12, 188);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(96, 20);
            this.Label2.TabIndex = 177;
            this.Label2.Text = "Alsó eltérés:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(12, 146);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(118, 20);
            this.Label3.TabIndex = 178;
            this.Label3.Text = "Névleges érték:";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(12, 104);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(113, 20);
            this.Label4.TabIndex = 179;
            this.Label4.Text = "Vizsgálat foka:";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(12, 62);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(151, 20);
            this.Label5.TabIndex = 180;
            this.Label5.Text = "Vizsgálat sorszáma:";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(12, 20);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(134, 20);
            this.Label6.TabIndex = 181;
            this.Label6.Text = "Ciklusrend típusa:";
            // 
            // Vizsgálatfoka
            // 
            this.Vizsgálatfoka.Location = new System.Drawing.Point(174, 98);
            this.Vizsgálatfoka.MaxLength = 10;
            this.Vizsgálatfoka.Name = "Vizsgálatfoka";
            this.Vizsgálatfoka.Size = new System.Drawing.Size(162, 26);
            this.Vizsgálatfoka.TabIndex = 182;
            // 
            // Névleges
            // 
            this.Névleges.Location = new System.Drawing.Point(174, 140);
            this.Névleges.Name = "Névleges";
            this.Névleges.Size = new System.Drawing.Size(162, 26);
            this.Névleges.TabIndex = 183;
            // 
            // Alsóeltérés
            // 
            this.Alsóeltérés.Location = new System.Drawing.Point(174, 182);
            this.Alsóeltérés.Name = "Alsóeltérés";
            this.Alsóeltérés.Size = new System.Drawing.Size(162, 26);
            this.Alsóeltérés.TabIndex = 184;
            // 
            // Felsőeltérés
            // 
            this.Felsőeltérés.Location = new System.Drawing.Point(174, 224);
            this.Felsőeltérés.Name = "Felsőeltérés";
            this.Felsőeltérés.Size = new System.Drawing.Size(162, 26);
            this.Felsőeltérés.TabIndex = 185;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // CsoportosMásolás
            // 
            this.CsoportosMásolás.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CsoportosMásolás.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.CsoportosMásolás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CsoportosMásolás.Location = new System.Drawing.Point(291, 469);
            this.CsoportosMásolás.Name = "CsoportosMásolás";
            this.CsoportosMásolás.Size = new System.Drawing.Size(45, 45);
            this.CsoportosMásolás.TabIndex = 190;
            this.toolTip1.SetToolTip(this.CsoportosMásolás, "Új ciklus másolása");
            this.CsoportosMásolás.UseVisualStyleBackColor = true;
            this.CsoportosMásolás.Click += new System.EventHandler(this.CsoportosMásolás_Click);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 439);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(141, 20);
            this.label8.TabIndex = 188;
            this.label8.Text = "Új ciklusrend neve:";
            // 
            // ÚjCiklus
            // 
            this.ÚjCiklus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ÚjCiklus.Location = new System.Drawing.Point(174, 433);
            this.ÚjCiklus.MaxLength = 15;
            this.ÚjCiklus.Name = "ÚjCiklus";
            this.ÚjCiklus.Size = new System.Drawing.Size(162, 26);
            this.ÚjCiklus.TabIndex = 187;
            // 
            // Ablak_Ciklus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Peru;
            this.ClientSize = new System.Drawing.Size(909, 526);
            this.Controls.Add(this.CsoportosMásolás);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ÚjCiklus);
            this.Controls.Add(this.Felsőeltérés);
            this.Controls.Add(this.Alsóeltérés);
            this.Controls.Add(this.Névleges);
            this.Controls.Add(this.Vizsgálatfoka);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Vizsálatsorszám);
            this.Controls.Add(this.CiklusTípus);
            this.Controls.Add(this.Töröl);
            this.Controls.Add(this.Rögzít);
            this.Controls.Add(this.Lekérdezés_lekérdezés);
            this.Controls.Add(this.Excel_gomb);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.Tábla);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Ciklus";
            this.Text = "Ciklus rend definiálása";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_Ciklus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal DataGridView Tábla;
        internal Button BtnSúgó;
        internal Button Excel_gomb;
        internal Button Lekérdezés_lekérdezés;
        internal Button Rögzít;
        internal Button Töröl;
        internal ComboBox CiklusTípus;
        internal TextBox Vizsálatsorszám;
        internal Label Label1;
        internal Label Label2;
        internal Label Label3;
        internal Label Label4;
        internal Label Label5;
        internal Label Label6;
        internal TextBox Vizsgálatfoka;
        internal TextBox Névleges;
        internal TextBox Alsóeltérés;
        internal TextBox Felsőeltérés;
        internal ToolTip toolTip1;
        internal Label label8;
        internal TextBox ÚjCiklus;
        internal Button CsoportosMásolás;
    }
}