using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{

    public partial class AblakLétszámgazdálkodás : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AblakLétszámgazdálkodás));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Fülek = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.SzűrtLista = new System.Windows.Forms.CheckBox();
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.MindAKettő = new System.Windows.Forms.RadioButton();
            this.NyitottFolyamat = new System.Windows.Forms.RadioButton();
            this.NyitottÜres = new System.Windows.Forms.RadioButton();
            this.Command5 = new System.Windows.Forms.Button();
            this.Command6 = new System.Windows.Forms.Button();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.Új_Sorszám = new System.Windows.Forms.TextBox();
            this.Áthelyez = new System.Windows.Forms.Button();
            this.label29 = new System.Windows.Forms.Label();
            this.Panel8 = new System.Windows.Forms.Panel();
            this.Státusváltozások = new System.Windows.Forms.ComboBox();
            this.StátusMódosítás = new System.Windows.Forms.Button();
            this.Label23 = new System.Windows.Forms.Label();
            this.Panel7 = new System.Windows.Forms.Panel();
            this.Új_Státus = new System.Windows.Forms.Button();
            this.Command4 = new System.Windows.Forms.Button();
            this.Label22 = new System.Windows.Forms.TextBox();
            this.Státusváltozásoka = new System.Windows.Forms.TextBox();
            this.Megjegyzés = new System.Windows.Forms.TextBox();
            this.Label21 = new System.Windows.Forms.Label();
            this.Label20 = new System.Windows.Forms.Label();
            this.Label19 = new System.Windows.Forms.Label();
            this.Check1 = new System.Windows.Forms.CheckBox();
            this.StatusFull = new System.Windows.Forms.Button();
            this.Status = new System.Windows.Forms.Button();
            this.Label18 = new System.Windows.Forms.Label();
            this.Label17 = new System.Windows.Forms.Label();
            this.Panel6 = new System.Windows.Forms.Panel();
            this.BelépőFull = new System.Windows.Forms.Button();
            this.Belépő = new System.Windows.Forms.Button();
            this.Hrazonosítóbe = new System.Windows.Forms.TextBox();
            this.Bérbe = new System.Windows.Forms.TextBox();
            this.Honnanjött = new System.Windows.Forms.TextBox();
            this.Belépésidátum = new System.Windows.Forms.DateTimePicker();
            this.Telephelybe = new System.Windows.Forms.ComboBox();
            this.Névbe = new System.Windows.Forms.TextBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.Label16 = new System.Windows.Forms.Label();
            this.Panel5 = new System.Windows.Forms.Panel();
            this.Kilépőfull = new System.Windows.Forms.Button();
            this.Kilépő = new System.Windows.Forms.Button();
            this.Hrazonosítóki = new System.Windows.Forms.TextBox();
            this.Bérki = new System.Windows.Forms.TextBox();
            this.KilépésOka = new System.Windows.Forms.TextBox();
            this.KilépésDátum = new System.Windows.Forms.DateTimePicker();
            this.Telephelyki = new System.Windows.Forms.ComboBox();
            this.Névki = new System.Windows.Forms.TextBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Id = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.Panel9 = new System.Windows.Forms.Panel();
            this.label30 = new System.Windows.Forms.Label();
            this.Label28 = new System.Windows.Forms.Label();
            this.Command9 = new System.Windows.Forms.Button();
            this.Text4 = new System.Windows.Forms.TextBox();
            this.Text5 = new System.Windows.Forms.TextBox();
            this.Text2 = new System.Windows.Forms.TextBox();
            this.Label27 = new System.Windows.Forms.Label();
            this.Label26 = new System.Windows.Forms.Label();
            this.Label25 = new System.Windows.Forms.Label();
            this.Label24 = new System.Windows.Forms.Label();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.Panel1.SuspendLayout();
            this.Fülek.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.Panel3.SuspendLayout();
            this.TabPage2.SuspendLayout();
            this.Panel4.SuspendLayout();
            this.panel10.SuspendLayout();
            this.Panel8.SuspendLayout();
            this.Panel7.SuspendLayout();
            this.Panel6.SuspendLayout();
            this.Panel5.SuspendLayout();
            this.TabPage3.SuspendLayout();
            this.Panel9.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.Cmbtelephely);
            this.Panel1.Controls.Add(this.Label13);
            this.Panel1.Location = new System.Drawing.Point(5, 5);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(335, 33);
            this.Panel1.TabIndex = 56;
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
            this.Label13.Location = new System.Drawing.Point(3, 4);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(145, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // Fülek
            // 
            this.Fülek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fülek.Controls.Add(this.TabPage1);
            this.Fülek.Controls.Add(this.TabPage2);
            this.Fülek.Controls.Add(this.TabPage3);
            this.Fülek.Location = new System.Drawing.Point(5, 55);
            this.Fülek.Name = "Fülek";
            this.Fülek.Padding = new System.Drawing.Point(16, 3);
            this.Fülek.SelectedIndex = 0;
            this.Fülek.Size = new System.Drawing.Size(1246, 588);
            this.Fülek.TabIndex = 60;
            this.Fülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Fülek_DrawItem);
            this.Fülek.SelectedIndexChanged += new System.EventHandler(this.Fülek_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.Controls.Add(this.Panel2);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1238, 555);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Státusok listázása";
            this.TabPage1.UseVisualStyleBackColor = true;
            // 
            // Panel2
            // 
            this.Panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel2.BackColor = System.Drawing.Color.SlateBlue;
            this.Panel2.Controls.Add(this.SzűrtLista);
            this.Panel2.Controls.Add(this.Tábla);
            this.Panel2.Controls.Add(this.Panel3);
            this.Panel2.Controls.Add(this.Command5);
            this.Panel2.Controls.Add(this.Command6);
            this.Panel2.Location = new System.Drawing.Point(0, 1);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(1238, 554);
            this.Panel2.TabIndex = 0;
            // 
            // SzűrtLista
            // 
            this.SzűrtLista.AutoSize = true;
            this.SzűrtLista.Location = new System.Drawing.Point(8, 14);
            this.SzűrtLista.Name = "SzűrtLista";
            this.SzűrtLista.Size = new System.Drawing.Size(161, 24);
            this.SzűrtLista.TabIndex = 117;
            this.SzűrtLista.Text = "Szűrt listát készít a";
            this.SzűrtLista.UseVisualStyleBackColor = true;
            this.SzűrtLista.CheckedChanged += new System.EventHandler(this.SzűrtLista_CheckedChanged);
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            this.Tábla.AllowUserToResizeRows = false;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.Location = new System.Drawing.Point(5, 57);
            this.Tábla.Name = "Tábla";
            this.Tábla.RowHeadersWidth = 20;
            this.Tábla.Size = new System.Drawing.Size(1231, 493);
            this.Tábla.TabIndex = 116;
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            this.Tábla.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Tábla_CellFormatting);
            // 
            // Panel3
            // 
            this.Panel3.Controls.Add(this.MindAKettő);
            this.Panel3.Controls.Add(this.NyitottFolyamat);
            this.Panel3.Controls.Add(this.NyitottÜres);
            this.Panel3.Location = new System.Drawing.Point(175, 9);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(515, 36);
            this.Panel3.TabIndex = 115;
            this.Panel3.Visible = false;
            // 
            // MindAKettő
            // 
            this.MindAKettő.AutoSize = true;
            this.MindAKettő.BackColor = System.Drawing.Color.GhostWhite;
            this.MindAKettő.Checked = true;
            this.MindAKettő.Location = new System.Drawing.Point(384, 3);
            this.MindAKettő.Name = "MindAKettő";
            this.MindAKettő.Size = new System.Drawing.Size(114, 24);
            this.MindAKettő.TabIndex = 2;
            this.MindAKettő.TabStop = true;
            this.MindAKettő.Text = "Mind a kettő";
            this.ToolTip1.SetToolTip(this.MindAKettő, "Az első kettő uniója");
            this.MindAKettő.UseVisualStyleBackColor = false;
            this.MindAKettő.Click += new System.EventHandler(this.MindAKettő_Click);
            // 
            // NyitottFolyamat
            // 
            this.NyitottFolyamat.AutoSize = true;
            this.NyitottFolyamat.BackColor = System.Drawing.Color.GhostWhite;
            this.NyitottFolyamat.Location = new System.Drawing.Point(183, 3);
            this.NyitottFolyamat.Name = "NyitottFolyamat";
            this.NyitottFolyamat.Size = new System.Drawing.Size(195, 24);
            this.NyitottFolyamat.TabIndex = 1;
            this.NyitottFolyamat.Text = "Nyitott folyamatban lévő";
            this.ToolTip1.SetToolTip(this.NyitottFolyamat, "A belépő dolgozó neve nem üres és a belépési dátuma a jövőben van.");
            this.NyitottFolyamat.UseVisualStyleBackColor = false;
            this.NyitottFolyamat.Click += new System.EventHandler(this.NyitottFolyamat_Click);
            // 
            // NyitottÜres
            // 
            this.NyitottÜres.AutoSize = true;
            this.NyitottÜres.BackColor = System.Drawing.Color.GhostWhite;
            this.NyitottÜres.Location = new System.Drawing.Point(3, 3);
            this.NyitottÜres.Name = "NyitottÜres";
            this.NyitottÜres.Size = new System.Drawing.Size(174, 24);
            this.NyitottÜres.TabIndex = 0;
            this.NyitottÜres.Text = "Nyitott be nem töltött";
            this.ToolTip1.SetToolTip(this.NyitottÜres, "A belépő dolgozó neve és a Hr azonosítója nem tartalmaz adatot.");
            this.NyitottÜres.UseVisualStyleBackColor = false;
            this.NyitottÜres.Click += new System.EventHandler(this.NyitottÜres_Click);
            // 
            // Command5
            // 
            this.Command5.BackColor = System.Drawing.Color.SlateGray;
            this.Command5.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Command5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command5.Location = new System.Drawing.Point(747, 4);
            this.Command5.Name = "Command5";
            this.Command5.Size = new System.Drawing.Size(45, 45);
            this.Command5.TabIndex = 114;
            this.ToolTip1.SetToolTip(this.Command5, "Az eredmény táblát kiírja Excelbe.");
            this.Command5.UseVisualStyleBackColor = false;
            this.Command5.Click += new System.EventHandler(this.Command5_Click);
            // 
            // Command6
            // 
            this.Command6.BackColor = System.Drawing.Color.SlateGray;
            this.Command6.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Command6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command6.Location = new System.Drawing.Point(696, 4);
            this.Command6.Name = "Command6";
            this.Command6.Size = new System.Drawing.Size(45, 45);
            this.Command6.TabIndex = 113;
            this.ToolTip1.SetToolTip(this.Command6, "Frissíti az eredmény tábla tartalmát");
            this.Command6.UseVisualStyleBackColor = false;
            this.Command6.Click += new System.EventHandler(this.Command6_Click);
            // 
            // TabPage2
            // 
            this.TabPage2.Controls.Add(this.Panel4);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(1238, 555);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Státus lista módosítás";
            this.TabPage2.UseVisualStyleBackColor = true;
            // 
            // Panel4
            // 
            this.Panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel4.BackColor = System.Drawing.Color.Aquamarine;
            this.Panel4.Controls.Add(this.panel10);
            this.Panel4.Controls.Add(this.Panel8);
            this.Panel4.Controls.Add(this.Panel7);
            this.Panel4.Controls.Add(this.Label17);
            this.Panel4.Controls.Add(this.Panel6);
            this.Panel4.Controls.Add(this.Panel5);
            this.Panel4.Controls.Add(this.Id);
            this.Panel4.Controls.Add(this.Label1);
            this.Panel4.Location = new System.Drawing.Point(0, 0);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(1238, 555);
            this.Panel4.TabIndex = 0;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.SpringGreen;
            this.panel10.Controls.Add(this.Új_Sorszám);
            this.panel10.Controls.Add(this.Áthelyez);
            this.panel10.Controls.Add(this.label29);
            this.panel10.Location = new System.Drawing.Point(857, 462);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(375, 85);
            this.panel10.TabIndex = 11;
            // 
            // Új_Sorszám
            // 
            this.Új_Sorszám.Location = new System.Drawing.Point(230, 30);
            this.Új_Sorszám.Name = "Új_Sorszám";
            this.Új_Sorszám.Size = new System.Drawing.Size(80, 26);
            this.Új_Sorszám.TabIndex = 106;
            // 
            // Áthelyez
            // 
            this.Áthelyez.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Áthelyez.BackColor = System.Drawing.Color.Lime;
            this.Áthelyez.BackgroundImage = global::Villamos.Properties.Resources.Designcontest_Ecommerce_Business_Save;
            this.Áthelyez.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Áthelyez.Location = new System.Drawing.Point(319, 11);
            this.Áthelyez.Name = "Áthelyez";
            this.Áthelyez.Size = new System.Drawing.Size(45, 45);
            this.Áthelyez.TabIndex = 99;
            this.ToolTip1.SetToolTip(this.Áthelyez, "Menti az adatokat");
            this.Áthelyez.UseVisualStyleBackColor = false;
            this.Áthelyez.Click += new System.EventHandler(this.Áthelyez_Click);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(0, 2);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(148, 20);
            this.label29.TabIndex = 9;
            this.label29.Text = "Belépő áthelyezése";
            // 
            // Panel8
            // 
            this.Panel8.BackColor = System.Drawing.Color.SpringGreen;
            this.Panel8.Controls.Add(this.Státusváltozások);
            this.Panel8.Controls.Add(this.StátusMódosítás);
            this.Panel8.Controls.Add(this.Label23);
            this.Panel8.Location = new System.Drawing.Point(857, 382);
            this.Panel8.Name = "Panel8";
            this.Panel8.Size = new System.Drawing.Size(375, 74);
            this.Panel8.TabIndex = 10;
            // 
            // Státusváltozások
            // 
            this.Státusváltozások.FormattingEnabled = true;
            this.Státusváltozások.Location = new System.Drawing.Point(4, 28);
            this.Státusváltozások.Name = "Státusváltozások";
            this.Státusváltozások.Size = new System.Drawing.Size(306, 28);
            this.Státusváltozások.TabIndex = 100;
            // 
            // StátusMódosítás
            // 
            this.StátusMódosítás.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StátusMódosítás.BackColor = System.Drawing.Color.Lime;
            this.StátusMódosítás.BackgroundImage = global::Villamos.Properties.Resources.Designcontest_Ecommerce_Business_Save;
            this.StátusMódosítás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.StátusMódosítás.Location = new System.Drawing.Point(319, 11);
            this.StátusMódosítás.Name = "StátusMódosítás";
            this.StátusMódosítás.Size = new System.Drawing.Size(45, 45);
            this.StátusMódosítás.TabIndex = 99;
            this.ToolTip1.SetToolTip(this.StátusMódosítás, "Menti az adatokat");
            this.StátusMódosítás.UseVisualStyleBackColor = false;
            this.StátusMódosítás.Click += new System.EventHandler(this.StátusMódosítás_Click);
            // 
            // Label23
            // 
            this.Label23.AutoSize = true;
            this.Label23.Location = new System.Drawing.Point(0, 0);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(133, 20);
            this.Label23.TabIndex = 9;
            this.Label23.Text = "Státus módosítás";
            // 
            // Panel7
            // 
            this.Panel7.BackColor = System.Drawing.Color.Lime;
            this.Panel7.Controls.Add(this.Új_Státus);
            this.Panel7.Controls.Add(this.Command4);
            this.Panel7.Controls.Add(this.Label22);
            this.Panel7.Controls.Add(this.Státusváltozásoka);
            this.Panel7.Controls.Add(this.Megjegyzés);
            this.Panel7.Controls.Add(this.Label21);
            this.Panel7.Controls.Add(this.Label20);
            this.Panel7.Controls.Add(this.Label19);
            this.Panel7.Controls.Add(this.Check1);
            this.Panel7.Controls.Add(this.StatusFull);
            this.Panel7.Controls.Add(this.Status);
            this.Panel7.Controls.Add(this.Label18);
            this.Panel7.Location = new System.Drawing.Point(6, 382);
            this.Panel7.Name = "Panel7";
            this.Panel7.Size = new System.Drawing.Size(838, 165);
            this.Panel7.TabIndex = 9;
            // 
            // Új_Státus
            // 
            this.Új_Státus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Új_Státus.BackColor = System.Drawing.Color.Gray;
            this.Új_Státus.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Új_Státus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Új_Státus.Location = new System.Drawing.Point(785, 57);
            this.Új_Státus.Name = "Új_Státus";
            this.Új_Státus.Size = new System.Drawing.Size(45, 45);
            this.Új_Státus.TabIndex = 107;
            this.ToolTip1.SetToolTip(this.Új_Státus, "Új státust készít");
            this.Új_Státus.UseVisualStyleBackColor = false;
            this.Új_Státus.Click += new System.EventHandler(this.Új_Státus_Click);
            // 
            // Command4
            // 
            this.Command4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Command4.BackColor = System.Drawing.Color.Gray;
            this.Command4.BackgroundImage = global::Villamos.Properties.Resources.New_32_piros;
            this.Command4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command4.Location = new System.Drawing.Point(785, 108);
            this.Command4.Name = "Command4";
            this.Command4.Size = new System.Drawing.Size(45, 45);
            this.Command4.TabIndex = 106;
            this.ToolTip1.SetToolTip(this.Command4, "Törli a státust");
            this.Command4.UseVisualStyleBackColor = false;
            this.Command4.Click += new System.EventHandler(this.Command4_Click);
            // 
            // Label22
            // 
            this.Label22.Location = new System.Drawing.Point(160, 22);
            this.Label22.Name = "Label22";
            this.Label22.Size = new System.Drawing.Size(340, 26);
            this.Label22.TabIndex = 105;
            // 
            // Státusváltozásoka
            // 
            this.Státusváltozásoka.Location = new System.Drawing.Point(160, 54);
            this.Státusváltozásoka.Multiline = true;
            this.Státusváltozásoka.Name = "Státusváltozásoka";
            this.Státusváltozásoka.Size = new System.Drawing.Size(595, 48);
            this.Státusváltozásoka.TabIndex = 104;
            // 
            // Megjegyzés
            // 
            this.Megjegyzés.Location = new System.Drawing.Point(160, 108);
            this.Megjegyzés.Multiline = true;
            this.Megjegyzés.Name = "Megjegyzés";
            this.Megjegyzés.Size = new System.Drawing.Size(595, 47);
            this.Megjegyzés.TabIndex = 103;
            // 
            // Label21
            // 
            this.Label21.AutoSize = true;
            this.Label21.Location = new System.Drawing.Point(6, 108);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(97, 20);
            this.Label21.TabIndex = 102;
            this.Label21.Text = "Megjegyzés:";
            // 
            // Label20
            // 
            this.Label20.AutoSize = true;
            this.Label20.Location = new System.Drawing.Point(6, 54);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(148, 20);
            this.Label20.TabIndex = 101;
            this.Label20.Text = "Státusváltozás oka:";
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.Location = new System.Drawing.Point(6, 28);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(60, 20);
            this.Label19.TabIndex = 100;
            this.Label19.Text = "Státus:";
            // 
            // Check1
            // 
            this.Check1.AutoSize = true;
            this.Check1.Location = new System.Drawing.Point(533, 22);
            this.Check1.Name = "Check1";
            this.Check1.Size = new System.Drawing.Size(138, 24);
            this.Check1.TabIndex = 99;
            this.Check1.Text = "Rész munkaidő";
            this.Check1.UseVisualStyleBackColor = true;
            // 
            // StatusFull
            // 
            this.StatusFull.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StatusFull.BackColor = System.Drawing.Color.Lime;
            this.StatusFull.BackgroundImage = global::Villamos.Properties.Resources.Designcontest_Ecommerce_Business_Save;
            this.StatusFull.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.StatusFull.Location = new System.Drawing.Point(785, 3);
            this.StatusFull.Name = "StatusFull";
            this.StatusFull.Size = new System.Drawing.Size(45, 45);
            this.StatusFull.TabIndex = 98;
            this.ToolTip1.SetToolTip(this.StatusFull, "Menti az adatokat");
            this.StatusFull.UseVisualStyleBackColor = false;
            this.StatusFull.Click += new System.EventHandler(this.StatusFull_Click);
            // 
            // Status
            // 
            this.Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Status.BackColor = System.Drawing.Color.Cyan;
            this.Status.BackgroundImage = global::Villamos.Properties.Resources.Designcontest_Ecommerce_Business_Save;
            this.Status.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Status.Location = new System.Drawing.Point(734, 3);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(45, 45);
            this.Status.TabIndex = 97;
            this.ToolTip1.SetToolTip(this.Status, "Részlegesen menti az adatokat ");
            this.Status.UseVisualStyleBackColor = false;
            this.Status.Click += new System.EventHandler(this.Status_Click);
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.Location = new System.Drawing.Point(0, 0);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(109, 20);
            this.Label18.TabIndex = 8;
            this.Label18.Text = "Státus adatok";
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.Location = new System.Drawing.Point(140, 325);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(900, 40);
            this.Label17.TabIndex = 8;
            this.Label17.Text = resources.GetString("Label17.Text");
            // 
            // Panel6
            // 
            this.Panel6.BackColor = System.Drawing.Color.Turquoise;
            this.Panel6.Controls.Add(this.BelépőFull);
            this.Panel6.Controls.Add(this.Belépő);
            this.Panel6.Controls.Add(this.Hrazonosítóbe);
            this.Panel6.Controls.Add(this.Bérbe);
            this.Panel6.Controls.Add(this.Honnanjött);
            this.Panel6.Controls.Add(this.Belépésidátum);
            this.Panel6.Controls.Add(this.Telephelybe);
            this.Panel6.Controls.Add(this.Névbe);
            this.Panel6.Controls.Add(this.Label9);
            this.Panel6.Controls.Add(this.Label10);
            this.Panel6.Controls.Add(this.Label11);
            this.Panel6.Controls.Add(this.Label12);
            this.Panel6.Controls.Add(this.Label14);
            this.Panel6.Controls.Add(this.Label15);
            this.Panel6.Controls.Add(this.Label16);
            this.Panel6.Location = new System.Drawing.Point(636, 41);
            this.Panel6.Name = "Panel6";
            this.Panel6.Size = new System.Drawing.Size(596, 281);
            this.Panel6.TabIndex = 3;
            // 
            // BelépőFull
            // 
            this.BelépőFull.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BelépőFull.BackColor = System.Drawing.Color.Lime;
            this.BelépőFull.BackgroundImage = global::Villamos.Properties.Resources.Designcontest_Ecommerce_Business_Save;
            this.BelépőFull.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BelépőFull.Location = new System.Drawing.Point(548, 9);
            this.BelépőFull.Name = "BelépőFull";
            this.BelépőFull.Size = new System.Drawing.Size(45, 45);
            this.BelépőFull.TabIndex = 96;
            this.ToolTip1.SetToolTip(this.BelépőFull, "Menti az adatokat");
            this.BelépőFull.UseVisualStyleBackColor = false;
            this.BelépőFull.Click += new System.EventHandler(this.BelépőFull_Click);
            // 
            // Belépő
            // 
            this.Belépő.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Belépő.BackColor = System.Drawing.Color.Cyan;
            this.Belépő.BackgroundImage = global::Villamos.Properties.Resources.Designcontest_Ecommerce_Business_Save;
            this.Belépő.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Belépő.Location = new System.Drawing.Point(498, 9);
            this.Belépő.Name = "Belépő";
            this.Belépő.Size = new System.Drawing.Size(45, 45);
            this.Belépő.TabIndex = 95;
            this.ToolTip1.SetToolTip(this.Belépő, "Részlegesen menti az adatokat ");
            this.Belépő.UseVisualStyleBackColor = false;
            this.Belépő.Click += new System.EventHandler(this.Belépő_Click);
            // 
            // Hrazonosítóbe
            // 
            this.Hrazonosítóbe.Location = new System.Drawing.Point(152, 60);
            this.Hrazonosítóbe.Name = "Hrazonosítóbe";
            this.Hrazonosítóbe.Size = new System.Drawing.Size(340, 26);
            this.Hrazonosítóbe.TabIndex = 13;
            // 
            // Bérbe
            // 
            this.Bérbe.Location = new System.Drawing.Point(152, 92);
            this.Bérbe.Name = "Bérbe";
            this.Bérbe.Size = new System.Drawing.Size(340, 26);
            this.Bérbe.TabIndex = 12;
            // 
            // Honnanjött
            // 
            this.Honnanjött.Location = new System.Drawing.Point(152, 158);
            this.Honnanjött.Multiline = true;
            this.Honnanjött.Name = "Honnanjött";
            this.Honnanjött.Size = new System.Drawing.Size(340, 68);
            this.Honnanjött.TabIndex = 11;
            // 
            // Belépésidátum
            // 
            this.Belépésidátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Belépésidátum.Location = new System.Drawing.Point(152, 232);
            this.Belépésidátum.Name = "Belépésidátum";
            this.Belépésidátum.Size = new System.Drawing.Size(105, 26);
            this.Belépésidátum.TabIndex = 10;
            // 
            // Telephelybe
            // 
            this.Telephelybe.FormattingEnabled = true;
            this.Telephelybe.Location = new System.Drawing.Point(152, 124);
            this.Telephelybe.Name = "Telephelybe";
            this.Telephelybe.Size = new System.Drawing.Size(306, 28);
            this.Telephelybe.TabIndex = 9;
            // 
            // Névbe
            // 
            this.Névbe.Location = new System.Drawing.Point(152, 28);
            this.Névbe.Name = "Névbe";
            this.Névbe.Size = new System.Drawing.Size(340, 26);
            this.Névbe.TabIndex = 8;
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(0, 0);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(125, 20);
            this.Label9.TabIndex = 7;
            this.Label9.Text = "Belépési Adatok";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(16, 238);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(61, 20);
            this.Label10.TabIndex = 6;
            this.Label10.Text = "Dátum:";
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(16, 158);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(135, 20);
            this.Label11.TabIndex = 5;
            this.Label11.Text = "Régi munkahelye:";
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(16, 132);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(80, 20);
            this.Label12.TabIndex = 4;
            this.Label12.Text = "Telephely:";
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(16, 98);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(38, 20);
            this.Label14.TabIndex = 3;
            this.Label14.Text = "Bér:";
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Location = new System.Drawing.Point(16, 66);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(110, 20);
            this.Label15.TabIndex = 2;
            this.Label15.Text = "HR azonosító:";
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Location = new System.Drawing.Point(16, 34);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(40, 20);
            this.Label16.TabIndex = 1;
            this.Label16.Text = "Név:";
            // 
            // Panel5
            // 
            this.Panel5.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.Panel5.Controls.Add(this.Kilépőfull);
            this.Panel5.Controls.Add(this.Kilépő);
            this.Panel5.Controls.Add(this.Hrazonosítóki);
            this.Panel5.Controls.Add(this.Bérki);
            this.Panel5.Controls.Add(this.KilépésOka);
            this.Panel5.Controls.Add(this.KilépésDátum);
            this.Panel5.Controls.Add(this.Telephelyki);
            this.Panel5.Controls.Add(this.Névki);
            this.Panel5.Controls.Add(this.Label8);
            this.Panel5.Controls.Add(this.Label7);
            this.Panel5.Controls.Add(this.Label6);
            this.Panel5.Controls.Add(this.Label5);
            this.Panel5.Controls.Add(this.Label4);
            this.Panel5.Controls.Add(this.Label3);
            this.Panel5.Controls.Add(this.Label2);
            this.Panel5.Location = new System.Drawing.Point(8, 41);
            this.Panel5.Name = "Panel5";
            this.Panel5.Size = new System.Drawing.Size(596, 281);
            this.Panel5.TabIndex = 2;
            // 
            // Kilépőfull
            // 
            this.Kilépőfull.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Kilépőfull.BackColor = System.Drawing.Color.Lime;
            this.Kilépőfull.BackgroundImage = global::Villamos.Properties.Resources.Designcontest_Ecommerce_Business_Save;
            this.Kilépőfull.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Kilépőfull.Location = new System.Drawing.Point(551, 9);
            this.Kilépőfull.Name = "Kilépőfull";
            this.Kilépőfull.Size = new System.Drawing.Size(45, 45);
            this.Kilépőfull.TabIndex = 96;
            this.ToolTip1.SetToolTip(this.Kilépőfull, "Menti az adatokat");
            this.Kilépőfull.UseVisualStyleBackColor = false;
            this.Kilépőfull.Click += new System.EventHandler(this.Kilépőfull_Click);
            // 
            // Kilépő
            // 
            this.Kilépő.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Kilépő.BackColor = System.Drawing.Color.Cyan;
            this.Kilépő.BackgroundImage = global::Villamos.Properties.Resources.Designcontest_Ecommerce_Business_Save;
            this.Kilépő.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Kilépő.Location = new System.Drawing.Point(500, 9);
            this.Kilépő.Name = "Kilépő";
            this.Kilépő.Size = new System.Drawing.Size(45, 45);
            this.Kilépő.TabIndex = 95;
            this.ToolTip1.SetToolTip(this.Kilépő, "Részlegesen menti az adatokat ");
            this.Kilépő.UseVisualStyleBackColor = false;
            this.Kilépő.Click += new System.EventHandler(this.Kilépő_Click);
            // 
            // Hrazonosítóki
            // 
            this.Hrazonosítóki.Location = new System.Drawing.Point(154, 60);
            this.Hrazonosítóki.Name = "Hrazonosítóki";
            this.Hrazonosítóki.Size = new System.Drawing.Size(340, 26);
            this.Hrazonosítóki.TabIndex = 13;
            // 
            // Bérki
            // 
            this.Bérki.Location = new System.Drawing.Point(154, 92);
            this.Bérki.Name = "Bérki";
            this.Bérki.Size = new System.Drawing.Size(340, 26);
            this.Bérki.TabIndex = 12;
            // 
            // KilépésOka
            // 
            this.KilépésOka.Location = new System.Drawing.Point(154, 158);
            this.KilépésOka.Multiline = true;
            this.KilépésOka.Name = "KilépésOka";
            this.KilépésOka.Size = new System.Drawing.Size(340, 68);
            this.KilépésOka.TabIndex = 11;
            // 
            // KilépésDátum
            // 
            this.KilépésDátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.KilépésDátum.Location = new System.Drawing.Point(154, 233);
            this.KilépésDátum.Name = "KilépésDátum";
            this.KilépésDátum.Size = new System.Drawing.Size(105, 26);
            this.KilépésDátum.TabIndex = 10;
            // 
            // Telephelyki
            // 
            this.Telephelyki.FormattingEnabled = true;
            this.Telephelyki.Location = new System.Drawing.Point(154, 124);
            this.Telephelyki.Name = "Telephelyki";
            this.Telephelyki.Size = new System.Drawing.Size(306, 28);
            this.Telephelyki.TabIndex = 9;
            // 
            // Névki
            // 
            this.Névki.Location = new System.Drawing.Point(154, 28);
            this.Névki.Name = "Névki";
            this.Névki.Size = new System.Drawing.Size(340, 26);
            this.Névki.TabIndex = 8;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(0, 0);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(118, 20);
            this.Label8.TabIndex = 7;
            this.Label8.Text = "Kilépési Adatok";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(16, 238);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(61, 20);
            this.Label7.TabIndex = 6;
            this.Label7.Text = "Dátum:";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(16, 158);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(42, 20);
            this.Label6.TabIndex = 5;
            this.Label6.Text = "Oka:";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(16, 132);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(80, 20);
            this.Label5.TabIndex = 4;
            this.Label5.Text = "Telephely:";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(16, 98);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(38, 20);
            this.Label4.TabIndex = 3;
            this.Label4.Text = "Bér:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(16, 66);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(110, 20);
            this.Label3.TabIndex = 2;
            this.Label3.Text = "HR azonosító:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(16, 34);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(40, 20);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "Név:";
            // 
            // Id
            // 
            this.Id.Enabled = false;
            this.Id.Location = new System.Drawing.Point(105, 9);
            this.Id.Name = "Id";
            this.Id.Size = new System.Drawing.Size(110, 26);
            this.Id.TabIndex = 1;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(14, 14);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(76, 20);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Sorszám:";
            // 
            // TabPage3
            // 
            this.TabPage3.Controls.Add(this.Panel9);
            this.TabPage3.Location = new System.Drawing.Point(4, 29);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(1238, 555);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Bér adatok frissítése";
            this.TabPage3.UseVisualStyleBackColor = true;
            // 
            // Panel9
            // 
            this.Panel9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel9.BackColor = System.Drawing.Color.Salmon;
            this.Panel9.Controls.Add(this.label30);
            this.Panel9.Controls.Add(this.Label28);
            this.Panel9.Controls.Add(this.Command9);
            this.Panel9.Controls.Add(this.Text4);
            this.Panel9.Controls.Add(this.Text5);
            this.Panel9.Controls.Add(this.Text2);
            this.Panel9.Controls.Add(this.Label27);
            this.Panel9.Controls.Add(this.Label26);
            this.Panel9.Controls.Add(this.Label25);
            this.Panel9.Controls.Add(this.Label24);
            this.Panel9.Location = new System.Drawing.Point(0, 0);
            this.Panel9.Name = "Panel9";
            this.Panel9.Size = new System.Drawing.Size(1238, 555);
            this.Panel9.TabIndex = 0;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(8, 159);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(711, 100);
            this.label30.TabIndex = 99;
            this.label30.Text = resources.GetString("label30.Text");
            // 
            // Label28
            // 
            this.Label28.AutoSize = true;
            this.Label28.Location = new System.Drawing.Point(8, 170);
            this.Label28.Name = "Label28";
            this.Label28.Size = new System.Drawing.Size(0, 20);
            this.Label28.TabIndex = 98;
            // 
            // Command9
            // 
            this.Command9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Command9.BackColor = System.Drawing.Color.Lime;
            this.Command9.BackgroundImage = global::Villamos.Properties.Resources.Designcontest_Ecommerce_Business_Save;
            this.Command9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command9.Location = new System.Drawing.Point(490, 75);
            this.Command9.Name = "Command9";
            this.Command9.Size = new System.Drawing.Size(45, 45);
            this.Command9.TabIndex = 97;
            this.ToolTip1.SetToolTip(this.Command9, "Menti az adatokat");
            this.Command9.UseVisualStyleBackColor = false;
            this.Command9.Click += new System.EventHandler(this.Command9_Click);
            // 
            // Text4
            // 
            this.Text4.Location = new System.Drawing.Point(305, 75);
            this.Text4.Name = "Text4";
            this.Text4.Size = new System.Drawing.Size(123, 26);
            this.Text4.TabIndex = 8;
            this.Text4.Text = "A";
            // 
            // Text5
            // 
            this.Text5.Location = new System.Drawing.Point(305, 114);
            this.Text5.Name = "Text5";
            this.Text5.Size = new System.Drawing.Size(123, 26);
            this.Text5.TabIndex = 7;
            this.Text5.Text = "B";
            // 
            // Text2
            // 
            this.Text2.Location = new System.Drawing.Point(144, 75);
            this.Text2.Name = "Text2";
            this.Text2.Size = new System.Drawing.Size(123, 26);
            this.Text2.TabIndex = 6;
            this.Text2.Text = "2";
            // 
            // Label27
            // 
            this.Label27.AutoSize = true;
            this.Label27.Location = new System.Drawing.Point(301, 39);
            this.Label27.Name = "Label27";
            this.Label27.Size = new System.Drawing.Size(58, 20);
            this.Label27.TabIndex = 5;
            this.Label27.Text = "Oszlop";
            // 
            // Label26
            // 
            this.Label26.AutoSize = true;
            this.Label26.Location = new System.Drawing.Point(140, 39);
            this.Label26.Name = "Label26";
            this.Label26.Size = new System.Drawing.Size(38, 20);
            this.Label26.TabIndex = 4;
            this.Label26.Text = "Sor:";
            // 
            // Label25
            // 
            this.Label25.AutoSize = true;
            this.Label25.Location = new System.Drawing.Point(8, 117);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(38, 20);
            this.Label25.TabIndex = 3;
            this.Label25.Text = "Bér:";
            // 
            // Label24
            // 
            this.Label24.AutoSize = true;
            this.Label24.Location = new System.Drawing.Point(8, 81);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(99, 20);
            this.Label24.TabIndex = 2;
            this.Label24.Text = "Hr azonosító";
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1206, 5);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 59;
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(350, 10);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(850, 25);
            this.Holtart.TabIndex = 1;
            this.Holtart.Visible = false;
            // 
            // AblakLétszámgazdálkodás
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.ClientSize = new System.Drawing.Size(1255, 646);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Fülek);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.Panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AblakLétszámgazdálkodás";
            this.Text = "Létszám gazdálkodás";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AblakLétszámgazdálkodás_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.Fülek.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            this.TabPage2.ResumeLayout(false);
            this.Panel4.ResumeLayout(false);
            this.Panel4.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.Panel8.ResumeLayout(false);
            this.Panel8.PerformLayout();
            this.Panel7.ResumeLayout(false);
            this.Panel7.PerformLayout();
            this.Panel6.ResumeLayout(false);
            this.Panel6.PerformLayout();
            this.Panel5.ResumeLayout(false);
            this.Panel5.PerformLayout();
            this.TabPage3.ResumeLayout(false);
            this.Panel9.ResumeLayout(false);
            this.Panel9.PerformLayout();
            this.ResumeLayout(false);

        }

        internal Panel Panel1;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal Button BtnSúgó;
        internal TabControl Fülek;
        internal TabPage TabPage1;
        internal Panel Panel2;
        internal TabPage TabPage2;
        internal TabPage TabPage3;
        internal Panel Panel3;
        internal RadioButton MindAKettő;
        internal RadioButton NyitottFolyamat;
        internal RadioButton NyitottÜres;
        internal Button Command5;
        internal Button Command6;
        internal DataGridView Tábla;
        internal ToolTip ToolTip1;
        internal CheckBox SzűrtLista;
        internal Panel Panel4;
        internal Panel Panel5;
        internal TextBox Hrazonosítóki;
        internal TextBox Bérki;
        internal TextBox KilépésOka;
        internal DateTimePicker KilépésDátum;
        internal ComboBox Telephelyki;
        internal TextBox Névki;
        internal Label Label8;
        internal Label Label7;
        internal Label Label6;
        internal Label Label5;
        internal Label Label4;
        internal Label Label3;
        internal Label Label2;
        internal TextBox Id;
        internal Label Label1;
        internal Button Kilépőfull;
        internal Button Kilépő;
        internal Label Label17;
        internal Panel Panel6;
        internal Button BelépőFull;
        internal Button Belépő;
        internal TextBox Hrazonosítóbe;
        internal TextBox Bérbe;
        internal TextBox Honnanjött;
        internal DateTimePicker Belépésidátum;
        internal ComboBox Telephelybe;
        internal TextBox Névbe;
        internal Label Label9;
        internal Label Label10;
        internal Label Label11;
        internal Label Label12;
        internal Label Label14;
        internal Label Label15;
        internal Label Label16;
        internal Panel Panel7;
        internal TextBox Label22;
        internal TextBox Státusváltozásoka;
        internal TextBox Megjegyzés;
        internal Label Label21;
        internal Label Label20;
        internal Label Label19;
        internal CheckBox Check1;
        internal Button StatusFull;
        internal Button Status;
        internal Label Label18;
        internal Button Új_Státus;
        internal Button Command4;
        internal Panel Panel8;
        internal ComboBox Státusváltozások;
        internal Button StátusMódosítás;
        internal Label Label23;
        internal Panel Panel9;
        internal Label Label28;
        internal Button Command9;
        internal TextBox Text4;
        internal TextBox Text5;
        internal TextBox Text2;
        internal Label Label27;
        internal Label Label26;
        internal Label Label25;
        internal Label Label24;
        internal Panel panel10;
        internal TextBox Új_Sorszám;
        internal Button Áthelyez;
        internal Label label29;
        internal Label label30;
        internal  V_MindenEgyéb.MyProgressbar Holtart;
    }
}