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
            this.BtnVeglegesMentes = new System.Windows.Forms.Button();
            this.BtnSugó = new System.Windows.Forms.Button();
            this.JogTörlés = new System.Windows.Forms.Button();
            this.MindenGomb = new System.Windows.Forms.Button();
            this.BtnMásol = new System.Windows.Forms.Button();
            this.BtnBeilleszt = new System.Windows.Forms.Button();
            this.BtnAblakTörlés = new System.Windows.Forms.Button();
            this.BtnOsszesMentese = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.MenűFa = new System.Windows.Forms.TreeView();
            this.CmbAblakId = new System.Windows.Forms.ComboBox();
            this.CmbAblak = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LstGombok = new System.Windows.Forms.ListBox();
            this.LstChkSzervezet = new System.Windows.Forms.CheckedListBox();
            this.LstJogokAdni = new System.Windows.Forms.ListBox();
            this.lb_jogosultsagok = new System.Windows.Forms.Label();
            this.Tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.Felhasználók = new System.Windows.Forms.ComboBox();
            this.DolgozóNév = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Másolat = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.BtnCSVBeolvasas = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolTip1
            // 
            this.ToolTip1.IsBalloon = true;
            // 
            // Frissít
            // 
            this.Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Frissít.Location = new System.Drawing.Point(3, 3);
            this.Frissít.Name = "Frissít";
            this.Frissít.Size = new System.Drawing.Size(44, 44);
            this.Frissít.TabIndex = 223;
            this.ToolTip1.SetToolTip(this.Frissít, "Frissíti a táblázat adatait");
            this.Frissít.UseVisualStyleBackColor = true;
            this.Frissít.Click += new System.EventHandler(this.Frissít_Click);
            // 
            // SzervezetMinden
            // 
            this.SzervezetMinden.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SzervezetMinden.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.SzervezetMinden.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.SzervezetMinden.Location = new System.Drawing.Point(53, 3);
            this.SzervezetMinden.Name = "SzervezetMinden";
            this.SzervezetMinden.Size = new System.Drawing.Size(44, 44);
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
            this.SzervezetSemmi.Location = new System.Drawing.Point(3, 3);
            this.SzervezetSemmi.Name = "SzervezetSemmi";
            this.SzervezetSemmi.Size = new System.Drawing.Size(44, 44);
            this.SzervezetSemmi.TabIndex = 103;
            this.ToolTip1.SetToolTip(this.SzervezetSemmi, "Minden kijelölést megszüntet");
            this.SzervezetSemmi.UseVisualStyleBackColor = true;
            this.SzervezetSemmi.Click += new System.EventHandler(this.SzervezetSemmi_Click);
            // 
            // BtnVeglegesMentes
            // 
            this.BtnVeglegesMentes.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.BtnVeglegesMentes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnVeglegesMentes.Location = new System.Drawing.Point(453, 3);
            this.BtnVeglegesMentes.Name = "BtnVeglegesMentes";
            this.BtnVeglegesMentes.Size = new System.Drawing.Size(44, 44);
            this.BtnVeglegesMentes.TabIndex = 97;
            this.ToolTip1.SetToolTip(this.BtnVeglegesMentes, "Menti az adatokat");
            this.BtnVeglegesMentes.UseVisualStyleBackColor = true;
            this.BtnVeglegesMentes.Click += new System.EventHandler(this.BtnVeglegesMentes_Click);
            // 
            // BtnSugó
            // 
            this.BtnSugó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSugó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSugó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSugó.Location = new System.Drawing.Point(446, 3);
            this.BtnSugó.Name = "BtnSugó";
            this.BtnSugó.Size = new System.Drawing.Size(45, 44);
            this.BtnSugó.TabIndex = 2;
            this.ToolTip1.SetToolTip(this.BtnSugó, "Online sugó megjelenítése");
            this.BtnSugó.UseVisualStyleBackColor = true;
            // 
            // JogTörlés
            // 
            this.JogTörlés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.JogTörlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.JogTörlés.Location = new System.Drawing.Point(286, 3);
            this.JogTörlés.Name = "JogTörlés";
            this.JogTörlés.Size = new System.Drawing.Size(44, 44);
            this.JogTörlés.TabIndex = 225;
            this.ToolTip1.SetToolTip(this.JogTörlés, "A felhasználó összes jogosultságának törlése");
            this.JogTörlés.UseVisualStyleBackColor = true;
            this.JogTörlés.Click += new System.EventHandler(this.JogTörlés_Click);
            // 
            // MindenGomb
            // 
            this.MindenGomb.BackgroundImage = global::Villamos.Properties.Resources.Action_view_bottom;
            this.MindenGomb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.MindenGomb.Location = new System.Drawing.Point(53, 3);
            this.MindenGomb.Name = "MindenGomb";
            this.MindenGomb.Size = new System.Drawing.Size(44, 44);
            this.MindenGomb.TabIndex = 226;
            this.ToolTip1.SetToolTip(this.MindenGomb, "Az ablakhoz tartozó összes gomb jogosultság beállítása");
            this.MindenGomb.UseVisualStyleBackColor = true;
            this.MindenGomb.Click += new System.EventHandler(this.MindenGomb_Click);
            // 
            // BtnMásol
            // 
            this.BtnMásol.BackgroundImage = global::Villamos.Properties.Resources.Document_Copy_01;
            this.BtnMásol.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnMásol.Location = new System.Drawing.Point(203, 3);
            this.BtnMásol.Name = "BtnMásol";
            this.BtnMásol.Size = new System.Drawing.Size(44, 44);
            this.BtnMásol.TabIndex = 227;
            this.ToolTip1.SetToolTip(this.BtnMásol, "A felhasználó összes jogosultságának másolása");
            this.BtnMásol.UseVisualStyleBackColor = true;
            this.BtnMásol.Click += new System.EventHandler(this.BtnMásol_Click);
            // 
            // BtnBeilleszt
            // 
            this.BtnBeilleszt.BackgroundImage = global::Villamos.Properties.Resources.Clipboard_Paste_01;
            this.BtnBeilleszt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnBeilleszt.Location = new System.Drawing.Point(253, 3);
            this.BtnBeilleszt.Name = "BtnBeilleszt";
            this.BtnBeilleszt.Size = new System.Drawing.Size(44, 44);
            this.BtnBeilleszt.TabIndex = 228;
            this.ToolTip1.SetToolTip(this.BtnBeilleszt, "A másolt jogosultságokat megkapja a kiválasztott felhasználó");
            this.BtnBeilleszt.UseVisualStyleBackColor = true;
            this.BtnBeilleszt.Click += new System.EventHandler(this.BtnBeilleszt_Click);
            // 
            // BtnAblakTörlés
            // 
            this.BtnAblakTörlés.BackgroundImage = global::Villamos.Properties.Resources.Fatcow_Farm_Fresh_Table_row_delete_32;
            this.BtnAblakTörlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnAblakTörlés.Location = new System.Drawing.Point(303, 3);
            this.BtnAblakTörlés.Name = "BtnAblakTörlés";
            this.BtnAblakTörlés.Size = new System.Drawing.Size(44, 44);
            this.BtnAblakTörlés.TabIndex = 229;
            this.ToolTip1.SetToolTip(this.BtnAblakTörlés, "A felhasználó ablakra vonatkozó jogosultságainak törlése");
            this.BtnAblakTörlés.UseVisualStyleBackColor = true;
            this.BtnAblakTörlés.Click += new System.EventHandler(this.BtnAblakTörlés_Click);
            // 
            // BtnOsszesMentese
            // 
            this.BtnOsszesMentese.BackgroundImage = global::Villamos.Properties.Resources.shopping_cart;
            this.BtnOsszesMentese.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnOsszesMentese.Location = new System.Drawing.Point(403, 3);
            this.BtnOsszesMentese.Name = "BtnOsszesMentese";
            this.BtnOsszesMentese.Size = new System.Drawing.Size(44, 44);
            this.BtnOsszesMentese.TabIndex = 230;
            this.ToolTip1.SetToolTip(this.BtnOsszesMentese, "Rögzíti az adatokat");
            this.BtnOsszesMentese.UseVisualStyleBackColor = true;
            this.BtnOsszesMentese.Click += new System.EventHandler(this.BtnOsszesMentese_Click);
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
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 520F));
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.LstGombok, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.LstChkSzervezet, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.LstJogokAdni, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.lb_jogosultsagok, 4, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 67);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1555, 286);
            this.tableLayoutPanel1.TabIndex = 99;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(863, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 20);
            this.label3.TabIndex = 230;
            this.label3.Text = "Szervezet";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.MenűFa);
            this.panel2.Controls.Add(this.CmbAblakId);
            this.panel2.Controls.Add(this.CmbAblak);
            this.panel2.Location = new System.Drawing.Point(3, 23);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(294, 260);
            this.panel2.TabIndex = 223;
            // 
            // MenűFa
            // 
            this.MenűFa.Location = new System.Drawing.Point(4, 71);
            this.MenűFa.Name = "MenűFa";
            this.MenűFa.Size = new System.Drawing.Size(287, 186);
            this.MenűFa.TabIndex = 104;
            this.MenűFa.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.MenűFa_AfterSelect);
            // 
            // CmbAblakId
            // 
            this.CmbAblakId.FormattingEnabled = true;
            this.CmbAblakId.Location = new System.Drawing.Point(4, 37);
            this.CmbAblakId.Name = "CmbAblakId";
            this.CmbAblakId.Size = new System.Drawing.Size(121, 28);
            this.CmbAblakId.TabIndex = 103;
            this.CmbAblakId.SelectionChangeCommitted += new System.EventHandler(this.CmbAblakId_SelectionChangeCommitted);
            // 
            // CmbAblak
            // 
            this.CmbAblak.FormattingEnabled = true;
            this.CmbAblak.Location = new System.Drawing.Point(3, 3);
            this.CmbAblak.Name = "CmbAblak";
            this.CmbAblak.Size = new System.Drawing.Size(288, 28);
            this.CmbAblak.Sorted = true;
            this.CmbAblak.TabIndex = 101;
            this.CmbAblak.SelectionChangeCommitted += new System.EventHandler(this.CmbAblak_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(303, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 20);
            this.label2.TabIndex = 228;
            this.label2.Text = "Gombok";
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
            // LstGombok
            // 
            this.LstGombok.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LstGombok.FormattingEnabled = true;
            this.LstGombok.IntegralHeight = false;
            this.LstGombok.ItemHeight = 20;
            this.LstGombok.Location = new System.Drawing.Point(303, 23);
            this.LstGombok.Name = "LstGombok";
            this.LstGombok.Size = new System.Drawing.Size(554, 260);
            this.LstGombok.TabIndex = 229;
            this.LstGombok.SelectedIndexChanged += new System.EventHandler(this.LstGombok_SelectedIndexChanged);
            // 
            // LstChkSzervezet
            // 
            this.LstChkSzervezet.CheckOnClick = true;
            this.LstChkSzervezet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LstChkSzervezet.FormattingEnabled = true;
            this.LstChkSzervezet.IntegralHeight = false;
            this.LstChkSzervezet.Location = new System.Drawing.Point(863, 23);
            this.LstChkSzervezet.Name = "LstChkSzervezet";
            this.LstChkSzervezet.Size = new System.Drawing.Size(169, 260);
            this.LstChkSzervezet.TabIndex = 98;
            this.LstChkSzervezet.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.LstChkSzervezet_ItemCheck);
            // 
            // LstJogokAdni
            // 
            this.LstJogokAdni.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LstJogokAdni.FormattingEnabled = true;
            this.LstJogokAdni.ItemHeight = 20;
            this.LstJogokAdni.Location = new System.Drawing.Point(1038, 23);
            this.LstJogokAdni.Name = "LstJogokAdni";
            this.LstJogokAdni.Size = new System.Drawing.Size(514, 260);
            this.LstJogokAdni.TabIndex = 231;
            // 
            // lb_jogosultsagok
            // 
            this.lb_jogosultsagok.AutoSize = true;
            this.lb_jogosultsagok.Location = new System.Drawing.Point(1038, 0);
            this.lb_jogosultsagok.Name = "lb_jogosultsagok";
            this.lb_jogosultsagok.Size = new System.Drawing.Size(150, 20);
            this.lb_jogosultsagok.TabIndex = 232;
            this.lb_jogosultsagok.Text = "Adott jogosultságok";
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
            this.Tábla.Size = new System.Drawing.Size(1555, 124);
            this.Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.TabIndex = 221;
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
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
            this.DolgozóNév.Location = new System.Drawing.Point(12, 44);
            this.DolgozóNév.Name = "DolgozóNév";
            this.DolgozóNév.Size = new System.Drawing.Size(49, 20);
            this.DolgozóNév.TabIndex = 222;
            this.DolgozóNév.Text = "<< >>";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 10;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.Controls.Add(this.Frissít, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.MindenGomb, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.BtnBeilleszt, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.BtnMásol, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.BtnVeglegesMentes, 9, 0);
            this.tableLayoutPanel2.Controls.Add(this.BtnAblakTörlés, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.BtnOsszesMentese, 8, 0);
            this.tableLayoutPanel2.Controls.Add(this.BtnCSVBeolvasas, 7, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(565, 11);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(499, 50);
            this.tableLayoutPanel2.TabIndex = 223;
            // 
            // Másolat
            // 
            this.Másolat.AutoSize = true;
            this.Másolat.Location = new System.Drawing.Point(371, 41);
            this.Másolat.Name = "Másolat";
            this.Másolat.Size = new System.Drawing.Size(49, 20);
            this.Másolat.TabIndex = 224;
            this.Másolat.Text = "<< >>";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel3.Controls.Add(this.JogTörlés, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.SzervezetSemmi, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.SzervezetMinden, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.BtnSugó, 5, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(1070, 11);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(494, 50);
            this.tableLayoutPanel3.TabIndex = 225;
            // 
            // BtnCSVBeolvasas
            // 
            this.BtnCSVBeolvasas.BackgroundImage = global::Villamos.Properties.Resources.alá;
            this.BtnCSVBeolvasas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnCSVBeolvasas.Location = new System.Drawing.Point(353, 3);
            this.BtnCSVBeolvasas.Name = "BtnCSVBeolvasas";
            this.BtnCSVBeolvasas.Size = new System.Drawing.Size(44, 44);
            this.BtnCSVBeolvasas.TabIndex = 231;
            this.ToolTip1.SetToolTip(this.BtnCSVBeolvasas, "Rögzíti az adatokat");
            this.BtnCSVBeolvasas.UseVisualStyleBackColor = true;
            this.BtnCSVBeolvasas.Click += new System.EventHandler(this.BtnCSVBeolvasas_Click);
            // 
            // Ablak_JogKiosztás
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1574, 495);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.Másolat);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.DolgozóNév);
            this.Controls.Add(this.Felhasználók);
            this.Controls.Add(this.Tábla);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Label1);
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
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal ToolTip ToolTip1;
        internal Button BtnSugó;
        internal Button BtnVeglegesMentes;
        internal Label Label1;
        private TableLayoutPanel tableLayoutPanel1;
        internal Label label4;
        private Zuby.ADGV.AdvancedDataGridView Tábla;
        private ComboBox Felhasználók;
        internal Label DolgozóNév;
        internal Button Frissít;
        internal Button SzervezetMinden;
        internal Button SzervezetSemmi;
        private ComboBox CmbAblak;
        private CheckedListBox LstChkSzervezet;
        internal Label label2;
        internal Button JogTörlés;
        private Panel panel2;
        private ComboBox CmbAblakId;
        internal Button MindenGomb;
        private ListBox LstGombok;
        private TableLayoutPanel tableLayoutPanel2;
        internal Label label3;
        internal Label Másolat;
        internal Button BtnBeilleszt;
        internal Button BtnMásol;
        private TableLayoutPanel tableLayoutPanel3;
        internal Button BtnAblakTörlés;
        private TreeView MenűFa;
        private ListBox LstJogokAdni;
        private Label lb_jogosultsagok;
        internal Button BtnOsszesMentese;
        internal Button BtnCSVBeolvasas;
    }
}