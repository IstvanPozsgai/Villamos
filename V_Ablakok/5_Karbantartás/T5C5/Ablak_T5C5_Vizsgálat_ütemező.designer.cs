using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
    
    public partial class Ablak_T5C5_Vizsgálat_ütemező : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_T5C5_Vizsgálat_ütemező));
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Kereső = new System.Windows.Forms.Button();
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.Felmentés = new System.Windows.Forms.Button();
            this.Excel = new System.Windows.Forms.Button();
            this.Utasítás = new System.Windows.Forms.Button();
            this.Vonalak = new System.Windows.Forms.Button();
            this.AktSzerelvény = new System.Windows.Forms.Button();
            this.AktuálisLista = new System.Windows.Forms.Button();
            this.Előírt = new System.Windows.Forms.Button();
            this.BeosztásTörlés = new System.Windows.Forms.Button();
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.Panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.Cmbtelephely);
            this.Panel2.Controls.Add(this.Label13);
            this.Panel2.Location = new System.Drawing.Point(4, 9);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(335, 33);
            this.Panel2.TabIndex = 57;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(149, 3);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(3, 7);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(145, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // Kereső
            // 
            this.Kereső.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Kereső.BackgroundImage = global::Villamos.Properties.Resources.Nagyító;
            this.Kereső.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Kereső.Location = new System.Drawing.Point(12, 3);
            this.Kereső.Name = "Kereső";
            this.Kereső.Size = new System.Drawing.Size(40, 40);
            this.Kereső.TabIndex = 65;
            this.ToolTip1.SetToolTip(this.Kereső, "Kereső ");
            this.Kereső.UseVisualStyleBackColor = true;
            this.Kereső.Click += new System.EventHandler(this.Kereső_Click);
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1160, 3);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(40, 40);
            this.BtnSúgó.TabIndex = 62;
            this.ToolTip1.SetToolTip(this.BtnSúgó, "Súgó");
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // Felmentés
            // 
            this.Felmentés.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Felmentés.BackgroundImage = global::Villamos.Properties.Resources.App_edit;
            this.Felmentés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Felmentés.Location = new System.Drawing.Point(837, 3);
            this.Felmentés.Name = "Felmentés";
            this.Felmentés.Size = new System.Drawing.Size(40, 40);
            this.Felmentés.TabIndex = 63;
            this.ToolTip1.SetToolTip(this.Felmentés, "Felmentési engedély generálás");
            this.Felmentés.UseVisualStyleBackColor = true;
            this.Felmentés.Click += new System.EventHandler(this.Felmentés_Click);
            // 
            // Excel
            // 
            this.Excel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excel.Location = new System.Drawing.Point(727, 3);
            this.Excel.Name = "Excel";
            this.Excel.Size = new System.Drawing.Size(40, 40);
            this.Excel.TabIndex = 67;
            this.ToolTip1.SetToolTip(this.Excel, "Excel kimenet készítése");
            this.Excel.UseVisualStyleBackColor = true;
            this.Excel.Click += new System.EventHandler(this.Excel_Click);
            // 
            // Utasítás
            // 
            this.Utasítás.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Utasítás.BackgroundImage = global::Villamos.Properties.Resources.Document_write;
            this.Utasítás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Utasítás.Location = new System.Drawing.Point(397, 3);
            this.Utasítás.Name = "Utasítás";
            this.Utasítás.Size = new System.Drawing.Size(40, 40);
            this.Utasítás.TabIndex = 68;
            this.ToolTip1.SetToolTip(this.Utasítás, "Beosztás adatok utasításba írása");
            this.Utasítás.UseVisualStyleBackColor = true;
            this.Utasítás.Click += new System.EventHandler(this.Utasítás_Click);
            // 
            // Vonalak
            // 
            this.Vonalak.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Vonalak.BackgroundImage = global::Villamos.Properties.Resources.Elegantthemes_Beautiful_Flat_Running;
            this.Vonalak.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Vonalak.Location = new System.Drawing.Point(617, 3);
            this.Vonalak.Name = "Vonalak";
            this.Vonalak.Size = new System.Drawing.Size(40, 40);
            this.Vonalak.TabIndex = 69;
            this.ToolTip1.SetToolTip(this.Vonalak, "Vonal adatok rögzítése");
            this.Vonalak.UseVisualStyleBackColor = true;
            this.Vonalak.Click += new System.EventHandler(this.Vonalak_Click);
            // 
            // AktSzerelvény
            // 
            this.AktSzerelvény.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AktSzerelvény.BackgroundImage = global::Villamos.Properties.Resources.Yellow_Glass_Folders_Icon_47;
            this.AktSzerelvény.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AktSzerelvény.Location = new System.Drawing.Point(232, 3);
            this.AktSzerelvény.Name = "AktSzerelvény";
            this.AktSzerelvény.Size = new System.Drawing.Size(40, 40);
            this.AktSzerelvény.TabIndex = 67;
            this.ToolTip1.SetToolTip(this.AktSzerelvény, "Aktuális szerelvény szerint Vizsgálat ");
            this.AktSzerelvény.UseVisualStyleBackColor = true;
            this.AktSzerelvény.Click += new System.EventHandler(this.AktSzerelvény_Click);
            // 
            // AktuálisLista
            // 
            this.AktuálisLista.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AktuálisLista.BackgroundImage = global::Villamos.Properties.Resources.Junior_Icon_111;
            this.AktuálisLista.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AktuálisLista.Location = new System.Drawing.Point(177, 3);
            this.AktuálisLista.Name = "AktuálisLista";
            this.AktuálisLista.Size = new System.Drawing.Size(40, 40);
            this.AktuálisLista.TabIndex = 67;
            this.ToolTip1.SetToolTip(this.AktuálisLista, "Aktuális Szerelvény szerinti Lista");
            this.AktuálisLista.UseVisualStyleBackColor = true;
            this.AktuálisLista.Click += new System.EventHandler(this.AktuálisLista_Click);
            // 
            // Előírt
            // 
            this.Előírt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Előírt.BackgroundImage = global::Villamos.Properties.Resources.Treetog_Junior_Document_scroll;
            this.Előírt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Előírt.Location = new System.Drawing.Point(122, 3);
            this.Előírt.Name = "Előírt";
            this.Előírt.Size = new System.Drawing.Size(40, 40);
            this.Előírt.TabIndex = 67;
            this.ToolTip1.SetToolTip(this.Előírt, "Előírt Szerelvény szerinti Lista");
            this.Előírt.UseVisualStyleBackColor = true;
            this.Előírt.Click += new System.EventHandler(this.Előírt_Click);
            // 
            // BeosztásTörlés
            // 
            this.BeosztásTörlés.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BeosztásTörlés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.BeosztásTörlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BeosztásTörlés.Location = new System.Drawing.Point(342, 3);
            this.BeosztásTörlés.Name = "BeosztásTörlés";
            this.BeosztásTörlés.Size = new System.Drawing.Size(40, 40);
            this.BeosztásTörlés.TabIndex = 66;
            this.ToolTip1.SetToolTip(this.BeosztásTörlés, "Beosztás adatok törlése");
            this.BeosztásTörlés.UseVisualStyleBackColor = true;
            this.BeosztásTörlés.Click += new System.EventHandler(this.BeosztásTörlés_Click);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(350, 18);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(804, 21);
            this.Holtart.TabIndex = 67;
            this.Holtart.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 17;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 323F));
            this.tableLayoutPanel1.Controls.Add(this.BtnSúgó, 16, 0);
            this.tableLayoutPanel1.Controls.Add(this.Felmentés, 15, 0);
            this.tableLayoutPanel1.Controls.Add(this.Excel, 13, 0);
            this.tableLayoutPanel1.Controls.Add(this.Kereső, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Utasítás, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.BeosztásTörlés, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.AktuálisLista, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.Előírt, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.AktSzerelvény, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.Vonalak, 11, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 45);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1203, 48);
            this.tableLayoutPanel1.TabIndex = 68;
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla.BackgroundColor = System.Drawing.Color.Silver;
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.Location = new System.Drawing.Point(4, 99);
            this.Tábla.Name = "Tábla";
            this.Tábla.Size = new System.Drawing.Size(1203, 614);
            this.Tábla.TabIndex = 70;
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            this.Tábla.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Tábla_CellFormatting);
            // 
            // Ablak_T5C5_Vizsgálat_ütemező
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Tan;
            this.ClientSize = new System.Drawing.Size(1219, 717);
            this.Controls.Add(this.Tábla);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Panel2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_T5C5_Vizsgálat_ütemező";
            this.Text = "T5C5 Km alapú vezénylése és Hétvégi kiadás";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_T5C5_Vizsgálat_ütemező_FormClosed);
            this.Load += new System.EventHandler(this.Ablak_Vizsgálat_ütemező_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ablak_Vizsgálat_ütemező_KeyDown);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.ResumeLayout(false);

        }

        internal Panel Panel2;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal ToolTip ToolTip1;
        internal Button BtnSúgó;
        private V_MindenEgyéb.MyProgressbar Holtart;
        private TableLayoutPanel tableLayoutPanel1;
        internal Button Felmentés;
        internal Button Kereső;
        internal Button Excel;
        internal Button AktuálisLista;
        internal Button AktSzerelvény;
        internal Button Előírt;
        internal Button Utasítás;
        internal Button Vonalak;
        internal DataGridView Tábla;
        internal Button BeosztásTörlés;
    }
}