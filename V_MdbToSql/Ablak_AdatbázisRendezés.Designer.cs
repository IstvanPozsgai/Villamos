using System.Windows.Forms;

namespace Villamos
{
    partial class Ablak_AdatbázisRendezés
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView DvgFájlok;
        private System.Windows.Forms.Button BtnHozzaad;
        private System.Windows.Forms.TextBox txtCelFajl;
        private System.Windows.Forms.TextBox TxtCélJelszó;
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
            this.txtCelFajl = new System.Windows.Forms.TextBox();
            this.TxtCélJelszó = new System.Windows.Forms.TextBox();
            this.BtnIndit = new System.Windows.Forms.Button();
            this.lblCelFajl = new System.Windows.Forms.Label();
            this.lblCelJelszo = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BtnHozzaad = new System.Windows.Forms.Button();
            this.Btn_Súgó = new System.Windows.Forms.Button();
            this.TáblaNévMód = new System.Windows.Forms.Button();
            this.TáblaNévKieg = new System.Windows.Forms.Button();
            this.TáblanevekMásolása = new System.Windows.Forms.Button();
            this.BtnAlaphelyzet = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.BtnFrissít = new System.Windows.Forms.Button();
            this.BtnSqlTáblaLista = new System.Windows.Forms.Button();
            this.ChkTáblák = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCélKönyvtár = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.TxtCélTábla = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ÚjTáblanevek = new System.Windows.Forms.CheckedListBox();
            this.ÚjTáblaNév = new System.Windows.Forms.TextBox();
            this.LstMezők = new System.Windows.Forms.ListBox();
            this.SqlTábla = new System.Windows.Forms.DataGridView();
            this.DgvAdatok = new System.Windows.Forms.DataGridView();
            this.SqlTáblaAdatok = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DvgFájlok)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SqlTábla)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvAdatok)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SqlTáblaAdatok)).BeginInit();
            this.SuspendLayout();
            // 
            // DvgFájlok
            // 
            this.DvgFájlok.AllowUserToAddRows = false;
            this.DvgFájlok.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DvgFájlok.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Könyvtár,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.DvgFájlok.Location = new System.Drawing.Point(6, 55);
            this.DvgFájlok.Name = "DvgFájlok";
            this.DvgFájlok.RowHeadersWidth = 30;
            this.DvgFájlok.Size = new System.Drawing.Size(534, 292);
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
            // txtCelFajl
            // 
            this.txtCelFajl.Location = new System.Drawing.Point(135, 31);
            this.txtCelFajl.Name = "txtCelFajl";
            this.txtCelFajl.Size = new System.Drawing.Size(387, 22);
            this.txtCelFajl.TabIndex = 58;
            // 
            // TxtCélJelszó
            // 
            this.TxtCélJelszó.Location = new System.Drawing.Point(135, 59);
            this.TxtCélJelszó.Name = "TxtCélJelszó";
            this.TxtCélJelszó.Size = new System.Drawing.Size(387, 22);
            this.TxtCélJelszó.TabIndex = 59;
            // 
            // BtnIndit
            // 
            this.BtnIndit.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.BtnIndit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnIndit.Location = new System.Drawing.Point(731, 423);
            this.BtnIndit.Name = "BtnIndit";
            this.BtnIndit.Size = new System.Drawing.Size(45, 45);
            this.BtnIndit.TabIndex = 6;
            this.toolTip1.SetToolTip(this.BtnIndit, "A beállításoknak megfelelően elkészíti a SqLite adatbázist");
            this.BtnIndit.Click += new System.EventHandler(this.BtnIndit_Click);
            // 
            // lblCelFajl
            // 
            this.lblCelFajl.Location = new System.Drawing.Point(3, 28);
            this.lblCelFajl.Name = "lblCelFajl";
            this.lblCelFajl.Size = new System.Drawing.Size(100, 23);
            this.lblCelFajl.TabIndex = 7;
            this.lblCelFajl.Text = "Cél SQLite fájl:";
            // 
            // lblCelJelszo
            // 
            this.lblCelJelszo.Location = new System.Drawing.Point(3, 56);
            this.lblCelJelszo.Name = "lblCelJelszo";
            this.lblCelJelszo.Size = new System.Drawing.Size(100, 23);
            this.lblCelJelszo.TabIndex = 8;
            this.lblCelJelszo.Text = "SQLite jelszó:";
            // 
            // BtnHozzaad
            // 
            this.BtnHozzaad.BackgroundImage = global::Villamos.Properties.Resources.database_search;
            this.BtnHozzaad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnHozzaad.Location = new System.Drawing.Point(491, 4);
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
            this.Btn_Súgó.Location = new System.Drawing.Point(1561, 4);
            this.Btn_Súgó.Name = "Btn_Súgó";
            this.Btn_Súgó.Size = new System.Drawing.Size(45, 45);
            this.Btn_Súgó.TabIndex = 54;
            this.toolTip1.SetToolTip(this.Btn_Súgó, "Súgó");
            this.Btn_Súgó.UseVisualStyleBackColor = true;
            this.Btn_Súgó.Click += new System.EventHandler(this.Btn_Súgó_Click);
            // 
            // TáblaNévMód
            // 
            this.TáblaNévMód.BackgroundImage = global::Villamos.Properties.Resources.Document_write;
            this.TáblaNévMód.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TáblaNévMód.Location = new System.Drawing.Point(731, 473);
            this.TáblaNévMód.Name = "TáblaNévMód";
            this.TáblaNévMód.Size = new System.Drawing.Size(45, 45);
            this.TáblaNévMód.TabIndex = 64;
            this.toolTip1.SetToolTip(this.TáblaNévMód, "Táblanév módosítása ki kell jelölni a listában majd átírni.");
            this.TáblaNévMód.Click += new System.EventHandler(this.TáblaNévMód_Click);
            // 
            // TáblaNévKieg
            // 
            this.TáblaNévKieg.BackgroundImage = global::Villamos.Properties.Resources.comment_edit;
            this.TáblaNévKieg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TáblaNévKieg.Location = new System.Drawing.Point(731, 524);
            this.TáblaNévKieg.Name = "TáblaNévKieg";
            this.TáblaNévKieg.Size = new System.Drawing.Size(45, 45);
            this.TáblaNévKieg.TabIndex = 65;
            this.toolTip1.SetToolTip(this.TáblaNévKieg, "Táblanév kiegészítése");
            this.TáblaNévKieg.Click += new System.EventHandler(this.TáblaNévKieg_Click);
            // 
            // TáblanevekMásolása
            // 
            this.TáblanevekMásolása.BackgroundImage = global::Villamos.Properties.Resources.Action_arrow_blue_double_down;
            this.TáblanevekMásolása.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TáblanevekMásolása.Location = new System.Drawing.Point(680, 354);
            this.TáblanevekMásolása.Name = "TáblanevekMásolása";
            this.TáblanevekMásolása.Size = new System.Drawing.Size(45, 45);
            this.TáblanevekMásolása.TabIndex = 66;
            this.toolTip1.SetToolTip(this.TáblanevekMásolása, "A kijelölt táblanévek másolása");
            this.TáblanevekMásolása.Click += new System.EventHandler(this.TáblanevekMásolása_Click);
            // 
            // BtnAlaphelyzet
            // 
            this.BtnAlaphelyzet.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.BtnAlaphelyzet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnAlaphelyzet.Location = new System.Drawing.Point(2, 4);
            this.BtnAlaphelyzet.Name = "BtnAlaphelyzet";
            this.BtnAlaphelyzet.Size = new System.Drawing.Size(45, 45);
            this.BtnAlaphelyzet.TabIndex = 67;
            this.toolTip1.SetToolTip(this.BtnAlaphelyzet, "Minden mezőt kiürít");
            this.BtnAlaphelyzet.Click += new System.EventHandler(this.BtnAlaphelyzet_Click);
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::Villamos.Properties.Resources.database_search;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(548, 354);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(45, 45);
            this.button1.TabIndex = 70;
            this.toolTip1.SetToolTip(this.button1, "mdb fájlok tallózása");
            // 
            // BtnFrissít
            // 
            this.BtnFrissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.BtnFrissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnFrissít.Location = new System.Drawing.Point(782, 423);
            this.BtnFrissít.Name = "BtnFrissít";
            this.BtnFrissít.Size = new System.Drawing.Size(45, 45);
            this.BtnFrissít.TabIndex = 74;
            this.toolTip1.SetToolTip(this.BtnFrissít, "SqLite fájlok tallózása");
            this.BtnFrissít.Click += new System.EventHandler(this.BtnFrissít_Click);
            // 
            // BtnSqlTáblaLista
            // 
            this.BtnSqlTáblaLista.BackgroundImage = global::Villamos.Properties.Resources.App_spreadsheet;
            this.BtnSqlTáblaLista.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSqlTáblaLista.Location = new System.Drawing.Point(833, 422);
            this.BtnSqlTáblaLista.Name = "BtnSqlTáblaLista";
            this.BtnSqlTáblaLista.Size = new System.Drawing.Size(45, 45);
            this.BtnSqlTáblaLista.TabIndex = 75;
            this.toolTip1.SetToolTip(this.BtnSqlTáblaLista, "A kiválasztott sorban szereplő adatok listázása");
            this.BtnSqlTáblaLista.Click += new System.EventHandler(this.BtnSqlTáblaLista_Click);
            // 
            // ChkTáblák
            // 
            this.ChkTáblák.CheckOnClick = true;
            this.ChkTáblák.FormattingEnabled = true;
            this.ChkTáblák.Location = new System.Drawing.Point(546, 55);
            this.ChkTáblák.Name = "ChkTáblák";
            this.ChkTáblák.Size = new System.Drawing.Size(179, 293);
            this.ChkTáblák.TabIndex = 11;
            this.ChkTáblák.SelectedIndexChanged += new System.EventHandler(this.ChkTáblák_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 23);
            this.label2.TabIndex = 56;
            this.label2.Text = "Cél SQLite könyvtár:";
            // 
            // txtCélKönyvtár
            // 
            this.txtCélKönyvtár.Location = new System.Drawing.Point(135, 3);
            this.txtCélKönyvtár.Name = "txtCélKönyvtár";
            this.txtCélKönyvtár.Size = new System.Drawing.Size(387, 22);
            this.txtCélKönyvtár.TabIndex = 57;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 132F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 268F));
            this.tableLayoutPanel1.Controls.Add(this.TxtCélTábla, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblCelFajl, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblCelJelszo, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtCelFajl, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.TxtCélJelszó, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtCélKönyvtár, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 353);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(536, 114);
            this.tableLayoutPanel1.TabIndex = 59;
            // 
            // TxtCélTábla
            // 
            this.TxtCélTábla.Location = new System.Drawing.Point(135, 87);
            this.TxtCélTábla.Name = "TxtCélTábla";
            this.TxtCélTábla.Size = new System.Drawing.Size(387, 22);
            this.TxtCélTábla.TabIndex = 60;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 20);
            this.label5.TabIndex = 58;
            this.label5.Text = "SQLite táblanév :";
            // 
            // ÚjTáblanevek
            // 
            this.ÚjTáblanevek.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ÚjTáblanevek.CheckOnClick = true;
            this.ÚjTáblanevek.FormattingEnabled = true;
            this.ÚjTáblanevek.Location = new System.Drawing.Point(548, 473);
            this.ÚjTáblanevek.Name = "ÚjTáblanevek";
            this.ÚjTáblanevek.Size = new System.Drawing.Size(179, 191);
            this.ÚjTáblanevek.TabIndex = 63;
            this.ÚjTáblanevek.SelectedIndexChanged += new System.EventHandler(this.ÚjTáblanevek_SelectedIndexChanged);
            // 
            // ÚjTáblaNév
            // 
            this.ÚjTáblaNév.Location = new System.Drawing.Point(548, 445);
            this.ÚjTáblaNév.Name = "ÚjTáblaNév";
            this.ÚjTáblaNév.Size = new System.Drawing.Size(179, 22);
            this.ÚjTáblaNév.TabIndex = 60;
            // 
            // LstMezők
            // 
            this.LstMezők.FormattingEnabled = true;
            this.LstMezők.ItemHeight = 16;
            this.LstMezők.Location = new System.Drawing.Point(733, 54);
            this.LstMezők.Name = "LstMezők";
            this.LstMezők.Size = new System.Drawing.Size(226, 292);
            this.LstMezők.TabIndex = 68;
            // 
            // SqlTábla
            // 
            this.SqlTábla.AllowUserToAddRows = false;
            this.SqlTábla.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SqlTábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SqlTábla.Location = new System.Drawing.Point(4, 473);
            this.SqlTábla.Name = "SqlTábla";
            this.SqlTábla.RowHeadersWidth = 30;
            this.SqlTábla.Size = new System.Drawing.Size(536, 190);
            this.SqlTábla.TabIndex = 69;
            this.SqlTábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SqlTábla_CellClick);
            // 
            // DgvAdatok
            // 
            this.DgvAdatok.AllowUserToAddRows = false;
            this.DgvAdatok.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvAdatok.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvAdatok.Location = new System.Drawing.Point(965, 55);
            this.DgvAdatok.Name = "DgvAdatok";
            this.DgvAdatok.RowHeadersWidth = 30;
            this.DgvAdatok.Size = new System.Drawing.Size(641, 293);
            this.DgvAdatok.TabIndex = 72;
            // 
            // SqlTáblaAdatok
            // 
            this.SqlTáblaAdatok.AllowUserToAddRows = false;
            this.SqlTáblaAdatok.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SqlTáblaAdatok.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SqlTáblaAdatok.Location = new System.Drawing.Point(782, 473);
            this.SqlTáblaAdatok.Name = "SqlTáblaAdatok";
            this.SqlTáblaAdatok.RowHeadersWidth = 30;
            this.SqlTáblaAdatok.Size = new System.Drawing.Size(822, 190);
            this.SqlTáblaAdatok.TabIndex = 73;
            // 
            // Ablak_AdatbázisRendezés
            // 
            this.ClientSize = new System.Drawing.Size(1616, 671);
            this.Controls.Add(this.BtnSqlTáblaLista);
            this.Controls.Add(this.BtnFrissít);
            this.Controls.Add(this.SqlTáblaAdatok);
            this.Controls.Add(this.DgvAdatok);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.SqlTábla);
            this.Controls.Add(this.LstMezők);
            this.Controls.Add(this.BtnAlaphelyzet);
            this.Controls.Add(this.TáblanevekMásolása);
            this.Controls.Add(this.TáblaNévKieg);
            this.Controls.Add(this.TáblaNévMód);
            this.Controls.Add(this.ÚjTáblaNév);
            this.Controls.Add(this.ÚjTáblanevek);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Btn_Súgó);
            this.Controls.Add(this.ChkTáblák);
            this.Controls.Add(this.DvgFájlok);
            this.Controls.Add(this.BtnHozzaad);
            this.Controls.Add(this.BtnIndit);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "Ablak_AdatbázisRendezés";
            this.Text = "MDB → SQLCipher migrátor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_AdatbázisRendezés_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DvgFájlok)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SqlTábla)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvAdatok)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SqlTáblaAdatok)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private ToolTip toolTip1;
        private CheckedListBox ChkTáblák;
        internal Button Btn_Súgó;
        private DataGridViewTextBoxColumn Könyvtár;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private Label label2;
        private TextBox txtCélKönyvtár;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox TxtCélTábla;
        private Label label5;
        private CheckedListBox ÚjTáblanevek;
        private TextBox ÚjTáblaNév;
        private Button TáblaNévMód;
        private Button TáblaNévKieg;
        private Button TáblanevekMásolása;
        private Button BtnAlaphelyzet;
        private ListBox LstMezők;
        private DataGridView SqlTábla;
        private Button button1;
        private DataGridView DgvAdatok;
        private DataGridView SqlTáblaAdatok;
        private Button BtnFrissít;
        private Button BtnSqlTáblaLista;
    }
}