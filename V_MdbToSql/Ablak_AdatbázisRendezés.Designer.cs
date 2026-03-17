using System.Windows.Forms;

namespace Villamos
{
    partial class Ablak_AdatbázisRendezés
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView DvgFájlok;
        private System.Windows.Forms.Button BtnHozzaad;
        private System.Windows.Forms.Button btnTorol;
        private System.Windows.Forms.TextBox txtCelFajl;
        private System.Windows.Forms.TextBox txtCelJelszo;
        private System.Windows.Forms.Button btnTallozCel;
        private System.Windows.Forms.Button btnIndit;
        private System.Windows.Forms.Label lblCelFajl;
        private System.Windows.Forms.Label lblCelJelszo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.DvgFájlok = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnTorol = new System.Windows.Forms.Button();
            this.txtCelFajl = new System.Windows.Forms.TextBox();
            this.txtCelJelszo = new System.Windows.Forms.TextBox();
            this.btnTallozCel = new System.Windows.Forms.Button();
            this.btnIndit = new System.Windows.Forms.Button();
            this.lblCelFajl = new System.Windows.Forms.Label();
            this.lblCelJelszo = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BtnTáblák = new System.Windows.Forms.Button();
            this.BtnHozzaad = new System.Windows.Forms.Button();
            this.ChkTáblák = new System.Windows.Forms.CheckedListBox();
            this.TxtMdbJelszó = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Btn_Súgó = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DvgFájlok)).BeginInit();
            this.SuspendLayout();
            // 
            // DvgFájlok
            // 
            this.DvgFájlok.AllowUserToAddRows = false;
            this.DvgFájlok.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DvgFájlok.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DvgFájlok.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.DvgFájlok.Location = new System.Drawing.Point(12, 188);
            this.DvgFájlok.Name = "DvgFájlok";
            this.DvgFájlok.RowHeadersWidth = 30;
            this.DvgFájlok.Size = new System.Drawing.Size(687, 256);
            this.DvgFájlok.TabIndex = 0;
            this.DvgFájlok.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DvgFájlok_CellClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "MDB fájl";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 400;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Jelszó";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 250;
            // 
            // btnTorol
            // 
            this.btnTorol.Location = new System.Drawing.Point(150, 12);
            this.btnTorol.Name = "btnTorol";
            this.btnTorol.Size = new System.Drawing.Size(75, 23);
            this.btnTorol.TabIndex = 2;
            this.btnTorol.Text = "Kijelölt törlése";
            this.btnTorol.Click += new System.EventHandler(this.btnTorol_Click);
            // 
            // txtCelFajl
            // 
            this.txtCelFajl.Location = new System.Drawing.Point(120, 59);
            this.txtCelFajl.Name = "txtCelFajl";
            this.txtCelFajl.Size = new System.Drawing.Size(550, 26);
            this.txtCelFajl.TabIndex = 3;
            // 
            // txtCelJelszo
            // 
            this.txtCelJelszo.Location = new System.Drawing.Point(120, 99);
            this.txtCelJelszo.Name = "txtCelJelszo";
            this.txtCelJelszo.Size = new System.Drawing.Size(300, 26);
            this.txtCelJelszo.TabIndex = 4;
            this.txtCelJelszo.UseSystemPasswordChar = true;
            // 
            // btnTallozCel
            // 
            this.btnTallozCel.Location = new System.Drawing.Point(680, 57);
            this.btnTallozCel.Name = "btnTallozCel";
            this.btnTallozCel.Size = new System.Drawing.Size(75, 23);
            this.btnTallozCel.TabIndex = 5;
            this.btnTallozCel.Text = "...";
            this.btnTallozCel.Click += new System.EventHandler(this.btnTallozCel_Click);
            // 
            // btnIndit
            // 
            this.btnIndit.Location = new System.Drawing.Point(823, 113);
            this.btnIndit.Name = "btnIndit";
            this.btnIndit.Size = new System.Drawing.Size(200, 40);
            this.btnIndit.TabIndex = 6;
            this.btnIndit.Text = "Migráció indítása";
            this.btnIndit.Click += new System.EventHandler(this.btnIndit_Click);
            // 
            // lblCelFajl
            // 
            this.lblCelFajl.Location = new System.Drawing.Point(12, 62);
            this.lblCelFajl.Name = "lblCelFajl";
            this.lblCelFajl.Size = new System.Drawing.Size(100, 23);
            this.lblCelFajl.TabIndex = 7;
            this.lblCelFajl.Text = "Cél SQLite fájl:";
            // 
            // lblCelJelszo
            // 
            this.lblCelJelszo.Location = new System.Drawing.Point(12, 102);
            this.lblCelJelszo.Name = "lblCelJelszo";
            this.lblCelJelszo.Size = new System.Drawing.Size(100, 23);
            this.lblCelJelszo.TabIndex = 8;
            this.lblCelJelszo.Text = "SQLite jelszó:";
            // 
            // BtnTáblák
            // 
            this.BtnTáblák.BackgroundImage = global::Villamos.Properties.Resources.Nagyító;
            this.BtnTáblák.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnTáblák.Location = new System.Drawing.Point(654, 137);
            this.BtnTáblák.Name = "BtnTáblák";
            this.BtnTáblák.Size = new System.Drawing.Size(45, 45);
            this.BtnTáblák.TabIndex = 10;
            this.toolTip1.SetToolTip(this.BtnTáblák, "Táblák kiírása");
            this.BtnTáblák.Click += new System.EventHandler(this.BtnTáblák_Click);
            // 
            // BtnHozzaad
            // 
            this.BtnHozzaad.BackgroundImage = global::Villamos.Properties.Resources.Folder_;
            this.BtnHozzaad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnHozzaad.Location = new System.Drawing.Point(395, 137);
            this.BtnHozzaad.Name = "BtnHozzaad";
            this.BtnHozzaad.Size = new System.Drawing.Size(45, 45);
            this.BtnHozzaad.TabIndex = 1;
            this.toolTip1.SetToolTip(this.BtnHozzaad, "mdb fájlok tallózása");
            this.BtnHozzaad.Click += new System.EventHandler(this.BtnHozzaad_Click);
            // 
            // ChkTáblák
            // 
            this.ChkTáblák.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ChkTáblák.FormattingEnabled = true;
            this.ChkTáblák.Location = new System.Drawing.Point(705, 188);
            this.ChkTáblák.Name = "ChkTáblák";
            this.ChkTáblák.Size = new System.Drawing.Size(319, 256);
            this.ChkTáblák.TabIndex = 11;
            // 
            // TxtMdbJelszó
            // 
            this.TxtMdbJelszó.Location = new System.Drawing.Point(89, 156);
            this.TxtMdbJelszó.Name = "TxtMdbJelszó";
            this.TxtMdbJelszó.Size = new System.Drawing.Size(300, 26);
            this.TxtMdbJelszó.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 159);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 23);
            this.label1.TabIndex = 13;
            this.label1.Text = "Jelszó:";
            // 
            // Btn_Súgó
            // 
            this.Btn_Súgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Súgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.Btn_Súgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Súgó.Location = new System.Drawing.Point(988, 2);
            this.Btn_Súgó.Name = "Btn_Súgó";
            this.Btn_Súgó.Size = new System.Drawing.Size(45, 45);
            this.Btn_Súgó.TabIndex = 54;
            this.toolTip1.SetToolTip(this.Btn_Súgó, "Súgó");
            this.Btn_Súgó.UseVisualStyleBackColor = true;
            this.Btn_Súgó.Click += new System.EventHandler(this.Btn_Súgó_Click);
            // 
            // Ablak_AdatbázisRendezés
            // 
            this.ClientSize = new System.Drawing.Size(1035, 462);
            this.Controls.Add(this.Btn_Súgó);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtMdbJelszó);
            this.Controls.Add(this.ChkTáblák);
            this.Controls.Add(this.BtnTáblák);
            this.Controls.Add(this.DvgFájlok);
            this.Controls.Add(this.BtnHozzaad);
            this.Controls.Add(this.btnTorol);
            this.Controls.Add(this.txtCelFajl);
            this.Controls.Add(this.txtCelJelszo);
            this.Controls.Add(this.btnTallozCel);
            this.Controls.Add(this.btnIndit);
            this.Controls.Add(this.lblCelFajl);
            this.Controls.Add(this.lblCelJelszo);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "Ablak_AdatbázisRendezés";
            this.Text = "MDB → SQLCipher migrátor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_AdatbázisRendezés_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DvgFájlok)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private ToolTip toolTip1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private Button BtnTáblák;
        private CheckedListBox ChkTáblák;
        private TextBox TxtMdbJelszó;
        private Label label1;
        internal Button Btn_Súgó;
    }
}