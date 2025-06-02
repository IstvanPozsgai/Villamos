using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{

    public partial class Ablak_JogKiosztás : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_JogKiosztás));
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Frissít = new System.Windows.Forms.Button();
            this.SzervezetMinden = new System.Windows.Forms.Button();
            this.SzervezetSemmi = new System.Windows.Forms.Button();
            this.GombokMinden = new System.Windows.Forms.Button();
            this.GombokSemmi = new System.Windows.Forms.Button();
            this.Rögzít = new System.Windows.Forms.Button();
            this.BtnSugó = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LstChkGombok = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CmbAblak = new System.Windows.Forms.ComboBox();
            this.LstChkSzervezet = new System.Windows.Forms.CheckedListBox();
            this.Tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.Felhasználók = new System.Windows.Forms.ComboBox();
            this.DolgozóNév = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // ToolTip1
            // 
            this.ToolTip1.IsBalloon = true;
            // 
            // Frissít
            // 
            this.Frissít.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Frissít.Location = new System.Drawing.Point(1340, 106);
            this.Frissít.Name = "Frissít";
            this.Frissít.Size = new System.Drawing.Size(45, 45);
            this.Frissít.TabIndex = 223;
            this.ToolTip1.SetToolTip(this.Frissít, "Frissíti a táblázatot");
            this.Frissít.UseVisualStyleBackColor = true;
            this.Frissít.Click += new System.EventHandler(this.Frissít_Click);
            // 
            // SzervezetMinden
            // 
            this.SzervezetMinden.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SzervezetMinden.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.SzervezetMinden.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.SzervezetMinden.Location = new System.Drawing.Point(404, 9);
            this.SzervezetMinden.Name = "SzervezetMinden";
            this.SzervezetMinden.Size = new System.Drawing.Size(45, 44);
            this.SzervezetMinden.TabIndex = 104;
            this.ToolTip1.SetToolTip(this.SzervezetMinden, "Minden kijeölése");
            this.SzervezetMinden.UseVisualStyleBackColor = true;
            this.SzervezetMinden.Click += new System.EventHandler(this.SzervezetMinden_Click);
            // 
            // SzervezetSemmi
            // 
            this.SzervezetSemmi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SzervezetSemmi.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.SzervezetSemmi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.SzervezetSemmi.Location = new System.Drawing.Point(455, 9);
            this.SzervezetSemmi.Name = "SzervezetSemmi";
            this.SzervezetSemmi.Size = new System.Drawing.Size(45, 44);
            this.SzervezetSemmi.TabIndex = 103;
            this.ToolTip1.SetToolTip(this.SzervezetSemmi, "Minden kijelölést megszüntet");
            this.SzervezetSemmi.UseVisualStyleBackColor = true;
            this.SzervezetSemmi.Click += new System.EventHandler(this.SzervezetSemmi_Click);
            // 
            // GombokMinden
            // 
            this.GombokMinden.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GombokMinden.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.GombokMinden.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.GombokMinden.Location = new System.Drawing.Point(404, 9);
            this.GombokMinden.Name = "GombokMinden";
            this.GombokMinden.Size = new System.Drawing.Size(45, 44);
            this.GombokMinden.TabIndex = 104;
            this.ToolTip1.SetToolTip(this.GombokMinden, "Minden kijeölése");
            this.GombokMinden.UseVisualStyleBackColor = true;
            this.GombokMinden.Click += new System.EventHandler(this.GombokMinden_Click);
            // 
            // GombokSemmi
            // 
            this.GombokSemmi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GombokSemmi.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.GombokSemmi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.GombokSemmi.Location = new System.Drawing.Point(455, 9);
            this.GombokSemmi.Name = "GombokSemmi";
            this.GombokSemmi.Size = new System.Drawing.Size(45, 44);
            this.GombokSemmi.TabIndex = 103;
            this.ToolTip1.SetToolTip(this.GombokSemmi, "Minden kijelölést megszüntet");
            this.GombokSemmi.UseVisualStyleBackColor = true;
            this.GombokSemmi.Click += new System.EventHandler(this.GombokSemmi_Click);
            // 
            // Rögzít
            // 
            this.Rögzít.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Rögzít.Location = new System.Drawing.Point(1340, 296);
            this.Rögzít.Name = "Rögzít";
            this.Rögzít.Size = new System.Drawing.Size(45, 45);
            this.Rögzít.TabIndex = 97;
            this.ToolTip1.SetToolTip(this.Rögzít, "Rögzíti az adatokat");
            this.Rögzít.UseVisualStyleBackColor = true;
            this.Rögzít.Click += new System.EventHandler(this.Rögzít_Click);
            // 
            // BtnSugó
            // 
            this.BtnSugó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSugó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSugó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSugó.Location = new System.Drawing.Point(1340, 12);
            this.BtnSugó.Name = "BtnSugó";
            this.BtnSugó.Size = new System.Drawing.Size(45, 45);
            this.BtnSugó.TabIndex = 2;
            this.ToolTip1.SetToolTip(this.BtnSugó, "Online sugó megjelenítése");
            this.BtnSugó.UseVisualStyleBackColor = true;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(124, 20);
            this.Label1.TabIndex = 87;
            this.Label1.Text = "Felhasználónév:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 510F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 510F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.LstChkGombok, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.CmbAblak, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.LstChkSzervezet, 2, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 38);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1320, 315);
            this.tableLayoutPanel1.TabIndex = 99;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.SzervezetMinden);
            this.groupBox2.Controls.Add(this.SzervezetSemmi);
            this.groupBox2.Location = new System.Drawing.Point(813, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(500, 59);
            this.groupBox2.TabIndex = 226;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Szervezet";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.GombokMinden);
            this.groupBox1.Controls.Add(this.GombokSemmi);
            this.groupBox1.Location = new System.Drawing.Point(303, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(500, 59);
            this.groupBox1.TabIndex = 225;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Gombok";
            // 
            // LstChkGombok
            // 
            this.LstChkGombok.FormattingEnabled = true;
            this.LstChkGombok.Location = new System.Drawing.Point(303, 68);
            this.LstChkGombok.Name = "LstChkGombok";
            this.LstChkGombok.Size = new System.Drawing.Size(500, 235);
            this.LstChkGombok.TabIndex = 102;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 20);
            this.label4.TabIndex = 99;
            this.label4.Text = "Ablak";
            // 
            // CmbAblak
            // 
            this.CmbAblak.FormattingEnabled = true;
            this.CmbAblak.Location = new System.Drawing.Point(3, 68);
            this.CmbAblak.Name = "CmbAblak";
            this.CmbAblak.Size = new System.Drawing.Size(292, 28);
            this.CmbAblak.TabIndex = 101;
            this.CmbAblak.SelectionChangeCommitted += new System.EventHandler(this.CmbAblak_SelectionChangeCommitted);
            // 
            // LstChkSzervezet
            // 
            this.LstChkSzervezet.FormattingEnabled = true;
            this.LstChkSzervezet.Location = new System.Drawing.Point(813, 68);
            this.LstChkSzervezet.Name = "LstChkSzervezet";
            this.LstChkSzervezet.Size = new System.Drawing.Size(500, 235);
            this.LstChkSzervezet.TabIndex = 98;
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.FilterAndSortEnabled = true;
            this.Tábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.Location = new System.Drawing.Point(12, 359);
            this.Tábla.MaxFilterButtonImageHeight = 23;
            this.Tábla.Name = "Tábla";
            this.Tábla.ReadOnly = true;
            this.Tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tábla.Size = new System.Drawing.Size(1373, 124);
            this.Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.TabIndex = 221;
            // 
            // Felhasználók
            // 
            this.Felhasználók.FormattingEnabled = true;
            this.Felhasználók.Location = new System.Drawing.Point(142, 6);
            this.Felhasználók.Name = "Felhasználók";
            this.Felhasználók.Size = new System.Drawing.Size(223, 28);
            this.Felhasználók.TabIndex = 103;
            this.Felhasználók.SelectionChangeCommitted += new System.EventHandler(this.Felhasználók_SelectionChangeCommitted);
            // 
            // DolgozóNév
            // 
            this.DolgozóNév.AutoSize = true;
            this.DolgozóNév.Location = new System.Drawing.Point(371, 12);
            this.DolgozóNév.Name = "DolgozóNév";
            this.DolgozóNév.Size = new System.Drawing.Size(49, 20);
            this.DolgozóNév.TabIndex = 222;
            this.DolgozóNév.Text = "<< >>";
            // 
            // Ablak_JogKiosztás
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1397, 495);
            this.Controls.Add(this.Frissít);
            this.Controls.Add(this.DolgozóNév);
            this.Controls.Add(this.Felhasználók);
            this.Controls.Add(this.Tábla);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Rögzít);
            this.Controls.Add(this.BtnSugó);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_JogKiosztás";
            this.Text = "Felhasználók jogkiosztása";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_JogKiosztás_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal ToolTip ToolTip1;
        internal Button BtnSugó;
        internal Button Rögzít;
        internal Label Label1;
        private TableLayoutPanel tableLayoutPanel1;
        internal Label label4;
        private Zuby.ADGV.AdvancedDataGridView Tábla;
        private ComboBox Felhasználók;
        internal Label DolgozóNév;
        internal Button Frissít;
        internal Button GombokSemmi;
        internal Button GombokMinden;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        internal Button SzervezetMinden;
        internal Button SzervezetSemmi;
        private CheckedListBox LstChkGombok;
        private ComboBox CmbAblak;
        private CheckedListBox LstChkSzervezet;
    }
}