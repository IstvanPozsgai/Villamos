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
        private System.Windows.Forms.TextBox TxtCélJelszó;
        private System.Windows.Forms.Button btnTallozCel;
        private System.Windows.Forms.Button BtnIndit;
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
            this.Könyvtár = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnTorol = new System.Windows.Forms.Button();
            this.txtCelFajl = new System.Windows.Forms.TextBox();
            this.TxtCélJelszó = new System.Windows.Forms.TextBox();
            this.btnTallozCel = new System.Windows.Forms.Button();
            this.BtnIndit = new System.Windows.Forms.Button();
            this.lblCelFajl = new System.Windows.Forms.Label();
            this.lblCelJelszo = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BtnTáblák = new System.Windows.Forms.Button();
            this.BtnHozzaad = new System.Windows.Forms.Button();
            this.Btn_Súgó = new System.Windows.Forms.Button();
            this.BtnCélTallózás = new System.Windows.Forms.Button();
            this.BtnMintaKiválasztás = new System.Windows.Forms.Button();
            this.MintaListázása = new System.Windows.Forms.Button();
            this.ChkTáblák = new System.Windows.Forms.CheckedListBox();
            this.ChkMezők = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCélKönyvtár = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.TxtCélTábla = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.MintaFájl = new System.Windows.Forms.TextBox();
            this.MintaJelszó = new System.Windows.Forms.TextBox();
            this.MintaKönyvtár = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.DvgFájlok)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // DvgFájlok
            // 
            this.DvgFájlok.AllowUserToAddRows = false;
            this.DvgFájlok.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DvgFájlok.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DvgFájlok.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Könyvtár,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.DvgFájlok.Location = new System.Drawing.Point(12, 188);
            this.DvgFájlok.Name = "DvgFájlok";
            this.DvgFájlok.RowHeadersWidth = 30;
            this.DvgFájlok.Size = new System.Drawing.Size(534, 256);
            this.DvgFájlok.TabIndex = 0;
            this.DvgFájlok.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DvgFájlok_CellClick);
            // 
            // Könyvtár
            // 
            this.Könyvtár.HeaderText = "Könyvtár";
            this.Könyvtár.Name = "Könyvtár";
            this.Könyvtár.Width = 200;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "MDB fájl";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Jelszó";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // btnTorol
            // 
            this.btnTorol.Location = new System.Drawing.Point(1510, 113);
            this.btnTorol.Name = "btnTorol";
            this.btnTorol.Size = new System.Drawing.Size(75, 23);
            this.btnTorol.TabIndex = 2;
            this.btnTorol.Text = "Kijelölt törlése";
            this.btnTorol.Click += new System.EventHandler(this.BtnTorol_Click);
            // 
            // txtCelFajl
            // 
            this.txtCelFajl.Location = new System.Drawing.Point(148, 33);
            this.txtCelFajl.Name = "txtCelFajl";
            this.txtCelFajl.Size = new System.Drawing.Size(387, 22);
            this.txtCelFajl.TabIndex = 3;
            this.txtCelFajl.Text = "PróbaAdatBázis";
            // 
            // TxtCélJelszó
            // 
            this.TxtCélJelszó.Location = new System.Drawing.Point(148, 63);
            this.TxtCélJelszó.Name = "TxtCélJelszó";
            this.TxtCélJelszó.Size = new System.Drawing.Size(387, 22);
            this.TxtCélJelszó.TabIndex = 4;
            this.TxtCélJelszó.Text = "PróbaJelszó";
            // 
            // btnTallozCel
            // 
            this.btnTallozCel.Location = new System.Drawing.Point(1510, 142);
            this.btnTallozCel.Name = "btnTallozCel";
            this.btnTallozCel.Size = new System.Drawing.Size(75, 23);
            this.btnTallozCel.TabIndex = 5;
            this.btnTallozCel.Text = "...";
            this.btnTallozCel.Click += new System.EventHandler(this.btnTallozCel_Click);
            // 
            // BtnIndit
            // 
            this.BtnIndit.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.BtnIndit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnIndit.Location = new System.Drawing.Point(1334, 178);
            this.BtnIndit.Name = "BtnIndit";
            this.BtnIndit.Size = new System.Drawing.Size(45, 45);
            this.BtnIndit.TabIndex = 6;
            this.toolTip1.SetToolTip(this.BtnIndit, "A beállításoknak megfelelően elkészíti a SqLite adatbázist");
            this.BtnIndit.Click += new System.EventHandler(this.BtnIndit_Click);
            // 
            // lblCelFajl
            // 
            this.lblCelFajl.Location = new System.Drawing.Point(3, 30);
            this.lblCelFajl.Name = "lblCelFajl";
            this.lblCelFajl.Size = new System.Drawing.Size(100, 23);
            this.lblCelFajl.TabIndex = 7;
            this.lblCelFajl.Text = "Cél SQLite fájl:";
            // 
            // lblCelJelszo
            // 
            this.lblCelJelszo.Location = new System.Drawing.Point(3, 60);
            this.lblCelJelszo.Name = "lblCelJelszo";
            this.lblCelJelszo.Size = new System.Drawing.Size(100, 23);
            this.lblCelJelszo.TabIndex = 8;
            this.lblCelJelszo.Text = "SQLite jelszó:";
            // 
            // BtnTáblák
            // 
            this.BtnTáblák.BackgroundImage = global::Villamos.Properties.Resources.Nagyító;
            this.BtnTáblák.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnTáblák.Location = new System.Drawing.Point(1540, 62);
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
            // Btn_Súgó
            // 
            this.Btn_Súgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Súgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.Btn_Súgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Súgó.Location = new System.Drawing.Point(1540, 2);
            this.Btn_Súgó.Name = "Btn_Súgó";
            this.Btn_Súgó.Size = new System.Drawing.Size(45, 45);
            this.Btn_Súgó.TabIndex = 54;
            this.toolTip1.SetToolTip(this.Btn_Súgó, "Súgó");
            this.Btn_Súgó.UseVisualStyleBackColor = true;
            this.Btn_Súgó.Click += new System.EventHandler(this.Btn_Súgó_Click);
            // 
            // BtnCélTallózás
            // 
            this.BtnCélTallózás.BackgroundImage = global::Villamos.Properties.Resources.Folder_;
            this.BtnCélTallózás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnCélTallózás.Location = new System.Drawing.Point(1334, 72);
            this.BtnCélTallózás.Name = "BtnCélTallózás";
            this.BtnCélTallózás.Size = new System.Drawing.Size(45, 45);
            this.BtnCélTallózás.TabIndex = 58;
            this.toolTip1.SetToolTip(this.BtnCélTallózás, "SqLite fájlok tallózása");
            this.BtnCélTallózás.Click += new System.EventHandler(this.BtnCélTallózás_Click);
            // 
            // BtnMintaKiválasztás
            // 
            this.BtnMintaKiválasztás.BackgroundImage = global::Villamos.Properties.Resources.Folder_;
            this.BtnMintaKiválasztás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnMintaKiválasztás.Location = new System.Drawing.Point(552, 21);
            this.BtnMintaKiválasztás.Name = "BtnMintaKiválasztás";
            this.BtnMintaKiválasztás.Size = new System.Drawing.Size(45, 45);
            this.BtnMintaKiválasztás.TabIndex = 61;
            this.toolTip1.SetToolTip(this.BtnMintaKiválasztás, "mdb fájlok tallózása");
            this.BtnMintaKiválasztás.Click += new System.EventHandler(this.BtnMintaKiválasztás_Click);
            // 
            // MintaListázása
            // 
            this.MintaListázása.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.MintaListázása.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MintaListázása.Location = new System.Drawing.Point(552, 72);
            this.MintaListázása.Name = "MintaListázása";
            this.MintaListázása.Size = new System.Drawing.Size(45, 45);
            this.MintaListázása.TabIndex = 62;
            this.toolTip1.SetToolTip(this.MintaListázása, "mdb fájlok tallózása");
            this.MintaListázása.Click += new System.EventHandler(this.MintaListázása_Click);
            // 
            // ChkTáblák
            // 
            this.ChkTáblák.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ChkTáblák.CheckOnClick = true;
            this.ChkTáblák.FormattingEnabled = true;
            this.ChkTáblák.Location = new System.Drawing.Point(552, 188);
            this.ChkTáblák.Name = "ChkTáblák";
            this.ChkTáblák.Size = new System.Drawing.Size(179, 259);
            this.ChkTáblák.TabIndex = 11;
            this.ChkTáblák.SelectedIndexChanged += new System.EventHandler(this.ChkTáblák_SelectedIndexChanged);
            // 
            // ChkMezők
            // 
            this.ChkMezők.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ChkMezők.FormattingEnabled = true;
            this.ChkMezők.Location = new System.Drawing.Point(737, 188);
            this.ChkMezők.Name = "ChkMezők";
            this.ChkMezők.Size = new System.Drawing.Size(179, 259);
            this.ChkMezők.TabIndex = 55;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 23);
            this.label2.TabIndex = 56;
            this.label2.Text = "Cél SQLite könyvtár:";
            // 
            // txtCélKönyvtár
            // 
            this.txtCélKönyvtár.Location = new System.Drawing.Point(148, 3);
            this.txtCélKönyvtár.Name = "txtCélKönyvtár";
            this.txtCélKönyvtár.Size = new System.Drawing.Size(387, 22);
            this.txtCélKönyvtár.TabIndex = 57;
            this.txtCélKönyvtár.Text = "Próba";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.88652F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.11347F));
            this.tableLayoutPanel1.Controls.Add(this.TxtCélTábla, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblCelFajl, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblCelJelszo, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtCelFajl, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.TxtCélJelszó, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtCélKönyvtár, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(764, 21);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(564, 133);
            this.tableLayoutPanel1.TabIndex = 59;
            // 
            // TxtCélTábla
            // 
            this.TxtCélTábla.Location = new System.Drawing.Point(148, 93);
            this.TxtCélTábla.Name = "TxtCélTábla";
            this.TxtCélTábla.Size = new System.Drawing.Size(387, 22);
            this.TxtCélTábla.TabIndex = 59;
            this.TxtCélTábla.Text = "Próba";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 23);
            this.label5.TabIndex = 58;
            this.label5.Text = "SQLite táblanév :";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.88652F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.11347F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.MintaFájl, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.MintaJelszó, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.MintaKönyvtár, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 21);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(534, 96);
            this.tableLayoutPanel2.TabIndex = 60;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 23);
            this.label1.TabIndex = 56;
            this.label1.Text = "Minta könyvtár:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 7;
            this.label3.Text = "Minta fájl:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 8;
            this.label4.Text = "Minta jelszó:";
            // 
            // MintaFájl
            // 
            this.MintaFájl.Location = new System.Drawing.Point(141, 33);
            this.MintaFájl.Name = "MintaFájl";
            this.MintaFájl.Size = new System.Drawing.Size(387, 22);
            this.MintaFájl.TabIndex = 3;
            // 
            // MintaJelszó
            // 
            this.MintaJelszó.Location = new System.Drawing.Point(141, 63);
            this.MintaJelszó.Name = "MintaJelszó";
            this.MintaJelszó.Size = new System.Drawing.Size(387, 22);
            this.MintaJelszó.TabIndex = 4;
            // 
            // MintaKönyvtár
            // 
            this.MintaKönyvtár.Location = new System.Drawing.Point(141, 3);
            this.MintaKönyvtár.Name = "MintaKönyvtár";
            this.MintaKönyvtár.Size = new System.Drawing.Size(387, 22);
            this.MintaKönyvtár.TabIndex = 57;
            // 
            // Ablak_AdatbázisRendezés
            // 
            this.ClientSize = new System.Drawing.Size(1587, 462);
            this.Controls.Add(this.MintaListázása);
            this.Controls.Add(this.BtnMintaKiválasztás);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.BtnCélTallózás);
            this.Controls.Add(this.ChkMezők);
            this.Controls.Add(this.Btn_Súgó);
            this.Controls.Add(this.ChkTáblák);
            this.Controls.Add(this.BtnTáblák);
            this.Controls.Add(this.DvgFájlok);
            this.Controls.Add(this.BtnHozzaad);
            this.Controls.Add(this.btnTorol);
            this.Controls.Add(this.btnTallozCel);
            this.Controls.Add(this.BtnIndit);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "Ablak_AdatbázisRendezés";
            this.Text = "MDB → SQLCipher migrátor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_AdatbázisRendezés_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DvgFájlok)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }
        private ToolTip toolTip1;
        private Button BtnTáblák;
        private CheckedListBox ChkTáblák;
        internal Button Btn_Súgó;
        private CheckedListBox ChkMezők;
        private DataGridViewTextBoxColumn Könyvtár;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private Label label2;
        private TextBox txtCélKönyvtár;
        private Button BtnCélTallózás;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label1;
        private Label label3;
        private Label label4;
        private TextBox MintaFájl;
        private TextBox MintaJelszó;
        private TextBox MintaKönyvtár;
        private Button BtnMintaKiválasztás;
        private Button MintaListázása;
        private TextBox TxtCélTábla;
        private Label label5;
    }
}