using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
    public partial class Ablak_Karbantartási_adatok : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Karbantartási_adatok));
            this.Panel200 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Holtart = new System.Windows.Forms.ProgressBar();
            this.Fülek = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.Táblalista = new Zuby.ADGV.AdvancedDataGridView();
            this.Frissíti_táblalistát = new System.Windows.Forms.Button();
            this.Excel_gomb = new System.Windows.Forms.Button();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.Tábla_Hibalista = new System.Windows.Forms.DataGridView();
            this.Egysorfel = new System.Windows.Forms.Button();
            this.Járműlista_excel = new System.Windows.Forms.Button();
            this.Sorszám = new System.Windows.Forms.TextBox();
            this.Javítva = new System.Windows.Forms.CheckBox();
            this.Hibaterv_combo = new System.Windows.Forms.ComboBox();
            this.Hibaszöveg = new System.Windows.Forms.TextBox();
            this.Rögzít_Módosít = new System.Windows.Forms.Button();
            this.Új_hiba_command1 = new System.Windows.Forms.Button();
            this.Hibaterv_command4 = new System.Windows.Forms.Button();
            this.Jel1 = new System.Windows.Forms.RadioButton();
            this.Jel3 = new System.Windows.Forms.RadioButton();
            this.Jel4 = new System.Windows.Forms.RadioButton();
            this.Pályaszám = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Lekérdez = new System.Windows.Forms.Button();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.Napló_tábla = new System.Windows.Forms.DataGridView();
            this.Dátumig = new System.Windows.Forms.DateTimePicker();
            this.Label4 = new System.Windows.Forms.Label();
            this.Dátumtól = new System.Windows.Forms.DateTimePicker();
            this.Label3 = new System.Windows.Forms.Label();
            this.Szűrés = new System.Windows.Forms.Button();
            this.Napló_excel = new System.Windows.Forms.Button();
            this.Napló_pályaszám = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.Frissíti_darabszámokat = new System.Windows.Forms.Button();
            this.Tábla_darabszámok = new System.Windows.Forms.DataGridView();
            this.TabPage5 = new System.Windows.Forms.TabPage();
            this.GombPanel = new System.Windows.Forms.Panel();
            this.Gombok_frissít = new System.Windows.Forms.Button();
            this.Gombok_típus = new System.Windows.Forms.ComboBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.Panel200.SuspendLayout();
            this.Fülek.SuspendLayout();
            this.TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Táblalista)).BeginInit();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_Hibalista)).BeginInit();
            this.TabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Napló_tábla)).BeginInit();
            this.TabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_darabszámok)).BeginInit();
            this.TabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel200
            // 
            this.Panel200.Controls.Add(this.Cmbtelephely);
            this.Panel200.Controls.Add(this.Label13);
            this.Panel200.Location = new System.Drawing.Point(12, 12);
            this.Panel200.Name = "Panel200";
            this.Panel200.Size = new System.Drawing.Size(335, 33);
            this.Panel200.TabIndex = 57;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(143, 0);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
            this.Cmbtelephely.SelectionChangeCommitted += new System.EventHandler(this.Cmbtelephely_SelectionChangeCommitted);
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
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(353, 13);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(871, 27);
            this.Holtart.TabIndex = 169;
            this.Holtart.Visible = false;
            // 
            // Fülek
            // 
            this.Fülek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fülek.Controls.Add(this.TabPage1);
            this.Fülek.Controls.Add(this.TabPage2);
            this.Fülek.Controls.Add(this.TabPage3);
            this.Fülek.Controls.Add(this.TabPage4);
            this.Fülek.Controls.Add(this.TabPage5);
            this.Fülek.Location = new System.Drawing.Point(5, 71);
            this.Fülek.Name = "Fülek";
            this.Fülek.Padding = new System.Drawing.Point(16, 3);
            this.Fülek.SelectedIndex = 0;
            this.Fülek.Size = new System.Drawing.Size(1270, 220);
            this.Fülek.TabIndex = 174;
            this.Fülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Fülek_DrawItem);
            this.Fülek.SelectedIndexChanged += new System.EventHandler(this.Fülek_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.Blue;
            this.TabPage1.Controls.Add(this.Táblalista);
            this.TabPage1.Controls.Add(this.Frissíti_táblalistát);
            this.TabPage1.Controls.Add(this.Excel_gomb);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1262, 187);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Típus lista";
            // 
            // Táblalista
            // 
            this.Táblalista.AllowUserToAddRows = false;
            this.Táblalista.AllowUserToDeleteRows = false;
            this.Táblalista.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Táblalista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Táblalista.FilterAndSortEnabled = true;
            this.Táblalista.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Táblalista.Location = new System.Drawing.Point(6, 55);
            this.Táblalista.MaxFilterButtonImageHeight = 23;
            this.Táblalista.Name = "Táblalista";
            this.Táblalista.ReadOnly = true;
            this.Táblalista.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Táblalista.RowHeadersVisible = false;
            this.Táblalista.Size = new System.Drawing.Size(1250, 126);
            this.Táblalista.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Táblalista.TabIndex = 182;
            this.Táblalista.SortStringChanged += new System.EventHandler<Zuby.ADGV.AdvancedDataGridView.SortEventArgs>(this.Táblalista_SortStringChanged);
            this.Táblalista.FilterStringChanged += new System.EventHandler<Zuby.ADGV.AdvancedDataGridView.FilterEventArgs>(this.Táblalista_FilterStringChanged);
            this.Táblalista.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Táblalista_CellClick);
            // 
            // Frissíti_táblalistát
            // 
            this.Frissíti_táblalistát.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Frissíti_táblalistát.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Frissíti_táblalistát.Location = new System.Drawing.Point(10, 6);
            this.Frissíti_táblalistát.Name = "Frissíti_táblalistát";
            this.Frissíti_táblalistát.Size = new System.Drawing.Size(45, 45);
            this.Frissíti_táblalistát.TabIndex = 181;
            this.Frissíti_táblalistát.UseVisualStyleBackColor = true;
            this.Frissíti_táblalistát.Click += new System.EventHandler(this.Frissíti_táblalistát_Click);
            // 
            // Excel_gomb
            // 
            this.Excel_gomb.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excel_gomb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excel_gomb.Location = new System.Drawing.Point(61, 6);
            this.Excel_gomb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Excel_gomb.Name = "Excel_gomb";
            this.Excel_gomb.Size = new System.Drawing.Size(45, 45);
            this.Excel_gomb.TabIndex = 177;
            this.Excel_gomb.UseVisualStyleBackColor = true;
            this.Excel_gomb.Click += new System.EventHandler(this.Excel_gomb_Click);
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.TabPage2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TabPage2.Controls.Add(this.Tábla_Hibalista);
            this.TabPage2.Controls.Add(this.Egysorfel);
            this.TabPage2.Controls.Add(this.Járműlista_excel);
            this.TabPage2.Controls.Add(this.Sorszám);
            this.TabPage2.Controls.Add(this.Javítva);
            this.TabPage2.Controls.Add(this.Hibaterv_combo);
            this.TabPage2.Controls.Add(this.Hibaszöveg);
            this.TabPage2.Controls.Add(this.Rögzít_Módosít);
            this.TabPage2.Controls.Add(this.Új_hiba_command1);
            this.TabPage2.Controls.Add(this.Hibaterv_command4);
            this.TabPage2.Controls.Add(this.Jel1);
            this.TabPage2.Controls.Add(this.Jel3);
            this.TabPage2.Controls.Add(this.Jel4);
            this.TabPage2.Controls.Add(this.Pályaszám);
            this.TabPage2.Controls.Add(this.Label1);
            this.TabPage2.Controls.Add(this.Lekérdez);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(1262, 187);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Jármű lista";
            // 
            // Tábla_Hibalista
            // 
            this.Tábla_Hibalista.AllowUserToAddRows = false;
            this.Tábla_Hibalista.AllowUserToDeleteRows = false;
            this.Tábla_Hibalista.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla_Hibalista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla_Hibalista.Location = new System.Drawing.Point(5, 73);
            this.Tábla_Hibalista.Name = "Tábla_Hibalista";
            this.Tábla_Hibalista.ReadOnly = true;
            this.Tábla_Hibalista.RowHeadersVisible = false;
            this.Tábla_Hibalista.Size = new System.Drawing.Size(1252, 111);
            this.Tábla_Hibalista.TabIndex = 196;
            this.Tábla_Hibalista.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_Hibalista_CellClick);
            // 
            // Egysorfel
            // 
            this.Egysorfel.BackgroundImage = global::Villamos.Properties.Resources.Button_Upload_01;
            this.Egysorfel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Egysorfel.Location = new System.Drawing.Point(272, 21);
            this.Egysorfel.Name = "Egysorfel";
            this.Egysorfel.Size = new System.Drawing.Size(45, 45);
            this.Egysorfel.TabIndex = 195;
            this.ToolTip1.SetToolTip(this.Egysorfel, "Feljebb viszi a sorban az adatot");
            this.Egysorfel.UseVisualStyleBackColor = true;
            this.Egysorfel.Click += new System.EventHandler(this.Egysorfel_Click);
            // 
            // Járműlista_excel
            // 
            this.Járműlista_excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Járműlista_excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Járműlista_excel.Location = new System.Drawing.Point(221, 21);
            this.Járműlista_excel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Járműlista_excel.Name = "Járműlista_excel";
            this.Járműlista_excel.Size = new System.Drawing.Size(45, 45);
            this.Járműlista_excel.TabIndex = 194;
            this.Járműlista_excel.UseVisualStyleBackColor = true;
            this.Járműlista_excel.Click += new System.EventHandler(this.Járműlista_excel_Click);
            // 
            // Sorszám
            // 
            this.Sorszám.Enabled = false;
            this.Sorszám.Location = new System.Drawing.Point(327, 6);
            this.Sorszám.Name = "Sorszám";
            this.Sorszám.Size = new System.Drawing.Size(70, 26);
            this.Sorszám.TabIndex = 193;
            // 
            // Javítva
            // 
            this.Javítva.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Javítva.AutoSize = true;
            this.Javítva.BackColor = System.Drawing.Color.Green;
            this.Javítva.Location = new System.Drawing.Point(960, 9);
            this.Javítva.Name = "Javítva";
            this.Javítva.Size = new System.Drawing.Size(143, 24);
            this.Javítva.TabIndex = 191;
            this.Javítva.Text = "Javítás elkészült";
            this.Javítva.UseVisualStyleBackColor = false;
            // 
            // Hibaterv_combo
            // 
            this.Hibaterv_combo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Hibaterv_combo.FormattingEnabled = true;
            this.Hibaterv_combo.Location = new System.Drawing.Point(327, 39);
            this.Hibaterv_combo.Name = "Hibaterv_combo";
            this.Hibaterv_combo.Size = new System.Drawing.Size(776, 28);
            this.Hibaterv_combo.TabIndex = 190;
            this.Hibaterv_combo.Visible = false;
            this.Hibaterv_combo.SelectedIndexChanged += new System.EventHandler(this.Hibaterv_combo_SelectedIndexChanged);
            // 
            // Hibaszöveg
            // 
            this.Hibaszöveg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Hibaszöveg.Location = new System.Drawing.Point(327, 38);
            this.Hibaszöveg.Multiline = true;
            this.Hibaszöveg.Name = "Hibaszöveg";
            this.Hibaszöveg.Size = new System.Drawing.Size(777, 28);
            this.Hibaszöveg.TabIndex = 189;
            // 
            // Rögzít_Módosít
            // 
            this.Rögzít_Módosít.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Rögzít_Módosít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Rögzít_Módosít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Rögzít_Módosít.Location = new System.Drawing.Point(1110, 21);
            this.Rögzít_Módosít.Name = "Rögzít_Módosít";
            this.Rögzít_Módosít.Size = new System.Drawing.Size(45, 45);
            this.Rögzít_Módosít.TabIndex = 188;
            this.ToolTip1.SetToolTip(this.Rögzít_Módosít, "Rögzít/Módosít");
            this.Rögzít_Módosít.UseVisualStyleBackColor = true;
            this.Rögzít_Módosít.Click += new System.EventHandler(this.Rögzít_Módosít_Click);
            // 
            // Új_hiba_command1
            // 
            this.Új_hiba_command1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Új_hiba_command1.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Új_hiba_command1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Új_hiba_command1.Location = new System.Drawing.Point(1161, 21);
            this.Új_hiba_command1.Name = "Új_hiba_command1";
            this.Új_hiba_command1.Size = new System.Drawing.Size(45, 45);
            this.Új_hiba_command1.TabIndex = 187;
            this.ToolTip1.SetToolTip(this.Új_hiba_command1, "Új Hiba");
            this.Új_hiba_command1.UseVisualStyleBackColor = true;
            this.Új_hiba_command1.Click += new System.EventHandler(this.Új_hiba_command1_Click);
            // 
            // Hibaterv_command4
            // 
            this.Hibaterv_command4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Hibaterv_command4.BackgroundImage = global::Villamos.Properties.Resources.Document_write;
            this.Hibaterv_command4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Hibaterv_command4.Location = new System.Drawing.Point(1212, 21);
            this.Hibaterv_command4.Name = "Hibaterv_command4";
            this.Hibaterv_command4.Size = new System.Drawing.Size(45, 45);
            this.Hibaterv_command4.TabIndex = 186;
            this.ToolTip1.SetToolTip(this.Hibaterv_command4, "Hibaterv");
            this.Hibaterv_command4.UseVisualStyleBackColor = true;
            this.Hibaterv_command4.Click += new System.EventHandler(this.Hibaterv_command4_Click);
            // 
            // Jel1
            // 
            this.Jel1.AutoSize = true;
            this.Jel1.BackColor = System.Drawing.Color.Green;
            this.Jel1.Location = new System.Drawing.Point(636, 8);
            this.Jel1.Name = "Jel1";
            this.Jel1.Size = new System.Drawing.Size(82, 24);
            this.Jel1.TabIndex = 185;
            this.Jel1.TabStop = true;
            this.Jel1.Text = "Szabad";
            this.Jel1.UseVisualStyleBackColor = false;
            // 
            // Jel3
            // 
            this.Jel3.AutoSize = true;
            this.Jel3.BackColor = System.Drawing.Color.Yellow;
            this.Jel3.Location = new System.Drawing.Point(559, 8);
            this.Jel3.Name = "Jel3";
            this.Jel3.Size = new System.Drawing.Size(71, 24);
            this.Jel3.TabIndex = 184;
            this.Jel3.TabStop = true;
            this.Jel3.Text = "Beálló";
            this.Jel3.UseVisualStyleBackColor = false;
            // 
            // Jel4
            // 
            this.Jel4.AutoSize = true;
            this.Jel4.BackColor = System.Drawing.Color.Red;
            this.Jel4.Location = new System.Drawing.Point(428, 8);
            this.Jel4.Name = "Jel4";
            this.Jel4.Size = new System.Drawing.Size(125, 24);
            this.Jel4.TabIndex = 183;
            this.Jel4.TabStop = true;
            this.Jel4.Text = "Nem kiadható";
            this.Jel4.UseVisualStyleBackColor = false;
            // 
            // Pályaszám
            // 
            this.Pályaszám.FormattingEnabled = true;
            this.Pályaszám.Location = new System.Drawing.Point(6, 39);
            this.Pályaszám.Name = "Pályaszám";
            this.Pályaszám.Size = new System.Drawing.Size(158, 28);
            this.Pályaszám.TabIndex = 182;
            this.Pályaszám.SelectionChangeCommitted += new System.EventHandler(this.Pályaszám_SelectionChangeCommitted);
            this.Pályaszám.TextUpdate += new System.EventHandler(this.Pályaszám_TextUpdate);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(7, 10);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(89, 20);
            this.Label1.TabIndex = 181;
            this.Label1.Text = "Pályaszám:";
            // 
            // Lekérdez
            // 
            this.Lekérdez.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Lekérdez.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lekérdez.Location = new System.Drawing.Point(170, 21);
            this.Lekérdez.Name = "Lekérdez";
            this.Lekérdez.Size = new System.Drawing.Size(45, 45);
            this.Lekérdez.TabIndex = 180;
            this.Lekérdez.UseVisualStyleBackColor = true;
            this.Lekérdez.Click += new System.EventHandler(this.Lekérdez_Click);
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.Gray;
            this.TabPage3.Controls.Add(this.Napló_tábla);
            this.TabPage3.Controls.Add(this.Dátumig);
            this.TabPage3.Controls.Add(this.Label4);
            this.TabPage3.Controls.Add(this.Dátumtól);
            this.TabPage3.Controls.Add(this.Label3);
            this.TabPage3.Controls.Add(this.Szűrés);
            this.TabPage3.Controls.Add(this.Napló_excel);
            this.TabPage3.Controls.Add(this.Napló_pályaszám);
            this.TabPage3.Controls.Add(this.Label2);
            this.TabPage3.Location = new System.Drawing.Point(4, 29);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(1262, 187);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Naplózás";
            // 
            // Napló_tábla
            // 
            this.Napló_tábla.AllowUserToAddRows = false;
            this.Napló_tábla.AllowUserToDeleteRows = false;
            this.Napló_tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Napló_tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Napló_tábla.Location = new System.Drawing.Point(5, 59);
            this.Napló_tábla.Name = "Napló_tábla";
            this.Napló_tábla.RowHeadersVisible = false;
            this.Napló_tábla.Size = new System.Drawing.Size(1252, 125);
            this.Napló_tábla.TabIndex = 198;
            // 
            // Dátumig
            // 
            this.Dátumig.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátumig.Location = new System.Drawing.Point(298, 15);
            this.Dátumig.Name = "Dátumig";
            this.Dátumig.Size = new System.Drawing.Size(110, 26);
            this.Dátumig.TabIndex = 197;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.BackColor = System.Drawing.Color.Gray;
            this.Label4.Location = new System.Drawing.Point(219, 21);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(73, 20);
            this.Label4.TabIndex = 196;
            this.Label4.Text = "Dátumig:";
            // 
            // Dátumtól
            // 
            this.Dátumtól.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátumtól.Location = new System.Drawing.Point(90, 15);
            this.Dátumtól.Name = "Dátumtól";
            this.Dátumtól.Size = new System.Drawing.Size(110, 26);
            this.Dátumtól.TabIndex = 195;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.Gray;
            this.Label3.Location = new System.Drawing.Point(6, 21);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(78, 20);
            this.Label3.TabIndex = 194;
            this.Label3.Text = "Dátumtól:";
            // 
            // Szűrés
            // 
            this.Szűrés.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Szűrés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Szűrés.Location = new System.Drawing.Point(705, 6);
            this.Szűrés.Name = "Szűrés";
            this.Szűrés.Size = new System.Drawing.Size(45, 45);
            this.Szűrés.TabIndex = 186;
            this.Szűrés.UseVisualStyleBackColor = true;
            this.Szűrés.Click += new System.EventHandler(this.Szűrés_Click);
            // 
            // Napló_excel
            // 
            this.Napló_excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Napló_excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Napló_excel.Location = new System.Drawing.Point(788, 6);
            this.Napló_excel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Napló_excel.Name = "Napló_excel";
            this.Napló_excel.Size = new System.Drawing.Size(45, 45);
            this.Napló_excel.TabIndex = 185;
            this.Napló_excel.UseVisualStyleBackColor = true;
            this.Napló_excel.Click += new System.EventHandler(this.Napló_excel_Click);
            // 
            // Napló_pályaszám
            // 
            this.Napló_pályaszám.FormattingEnabled = true;
            this.Napló_pályaszám.Location = new System.Drawing.Point(530, 15);
            this.Napló_pályaszám.Name = "Napló_pályaszám";
            this.Napló_pályaszám.Size = new System.Drawing.Size(158, 28);
            this.Napló_pályaszám.TabIndex = 184;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Gray;
            this.Label2.Location = new System.Drawing.Point(435, 21);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(89, 20);
            this.Label2.TabIndex = 183;
            this.Label2.Text = "Pályaszám:";
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.Silver;
            this.TabPage4.Controls.Add(this.Frissíti_darabszámokat);
            this.TabPage4.Controls.Add(this.Tábla_darabszámok);
            this.TabPage4.Location = new System.Drawing.Point(4, 29);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Size = new System.Drawing.Size(1262, 187);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Aktuális adatok";
            // 
            // Frissíti_darabszámokat
            // 
            this.Frissíti_darabszámokat.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Frissíti_darabszámokat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Frissíti_darabszámokat.Location = new System.Drawing.Point(5, 3);
            this.Frissíti_darabszámokat.Name = "Frissíti_darabszámokat";
            this.Frissíti_darabszámokat.Size = new System.Drawing.Size(45, 45);
            this.Frissíti_darabszámokat.TabIndex = 184;
            this.Frissíti_darabszámokat.UseVisualStyleBackColor = true;
            this.Frissíti_darabszámokat.Click += new System.EventHandler(this.Frissíti_darabszámokat_Click);
            // 
            // Tábla_darabszámok
            // 
            this.Tábla_darabszámok.AllowUserToAddRows = false;
            this.Tábla_darabszámok.AllowUserToDeleteRows = false;
            this.Tábla_darabszámok.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla_darabszámok.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla_darabszámok.Location = new System.Drawing.Point(5, 54);
            this.Tábla_darabszámok.Name = "Tábla_darabszámok";
            this.Tábla_darabszámok.RowHeadersVisible = false;
            this.Tábla_darabszámok.Size = new System.Drawing.Size(1252, 130);
            this.Tábla_darabszámok.TabIndex = 183;
            // 
            // TabPage5
            // 
            this.TabPage5.BackColor = System.Drawing.Color.Tan;
            this.TabPage5.Controls.Add(this.GombPanel);
            this.TabPage5.Controls.Add(this.Gombok_frissít);
            this.TabPage5.Controls.Add(this.Gombok_típus);
            this.TabPage5.Controls.Add(this.Label5);
            this.TabPage5.Location = new System.Drawing.Point(4, 29);
            this.TabPage5.Name = "TabPage5";
            this.TabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage5.Size = new System.Drawing.Size(1262, 187);
            this.TabPage5.TabIndex = 4;
            this.TabPage5.Text = "Gombok";
            // 
            // GombPanel
            // 
            this.GombPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GombPanel.AutoScroll = true;
            this.GombPanel.BackColor = System.Drawing.Color.Tomato;
            this.GombPanel.Location = new System.Drawing.Point(3, 56);
            this.GombPanel.Name = "GombPanel";
            this.GombPanel.Size = new System.Drawing.Size(1256, 136);
            this.GombPanel.TabIndex = 208;
            // 
            // Gombok_frissít
            // 
            this.Gombok_frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Gombok_frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Gombok_frissít.Location = new System.Drawing.Point(287, 5);
            this.Gombok_frissít.Name = "Gombok_frissít";
            this.Gombok_frissít.Size = new System.Drawing.Size(45, 45);
            this.Gombok_frissít.TabIndex = 184;
            this.Gombok_frissít.UseVisualStyleBackColor = true;
            this.Gombok_frissít.Click += new System.EventHandler(this.Gombok_frissít_Click);
            // 
            // Gombok_típus
            // 
            this.Gombok_típus.FormattingEnabled = true;
            this.Gombok_típus.Location = new System.Drawing.Point(119, 21);
            this.Gombok_típus.Name = "Gombok_típus";
            this.Gombok_típus.Size = new System.Drawing.Size(158, 28);
            this.Gombok_típus.TabIndex = 182;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Label5.ForeColor = System.Drawing.Color.White;
            this.Label5.Location = new System.Drawing.Point(6, 29);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(107, 20);
            this.Label5.TabIndex = 183;
            this.Label5.Text = "Jármű típus:";
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1230, 8);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 168;
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // Ablak_Karbantartási_adatok
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Blue;
            this.ClientSize = new System.Drawing.Size(1284, 297);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Fülek);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.Panel200);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Karbantartási_adatok";
            this.Text = "Jármű karbantartási adatok";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_Jármű_állapotok_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ablak_Jármű_állapotok_KeyDown);
            this.Panel200.ResumeLayout(false);
            this.Panel200.PerformLayout();
            this.Fülek.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Táblalista)).EndInit();
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_Hibalista)).EndInit();
            this.TabPage3.ResumeLayout(false);
            this.TabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Napló_tábla)).EndInit();
            this.TabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_darabszámok)).EndInit();
            this.TabPage5.ResumeLayout(false);
            this.TabPage5.PerformLayout();
            this.ResumeLayout(false);

        }

        internal Panel Panel200;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal ProgressBar Holtart;
        internal Button BtnSúgó;
        internal TabControl Fülek;
        internal TabPage TabPage1;
        internal Button Frissíti_táblalistát;
        internal Button Excel_gomb;
        internal TabPage TabPage2;
        internal CheckBox Javítva;
        internal ComboBox Hibaterv_combo;
        internal TextBox Hibaszöveg;
        internal Button Rögzít_Módosít;
        internal ToolTip ToolTip1;
        internal Button Új_hiba_command1;
        internal Button Hibaterv_command4;
        internal RadioButton Jel1;
        internal RadioButton Jel3;
        internal RadioButton Jel4;
        internal Label Label1;
        internal Button Lekérdez;
        internal TextBox Sorszám;
        internal ComboBox Pályaszám;
        internal TabPage TabPage3;
        internal Button Szűrés;
        internal Button Napló_excel;
        internal ComboBox Napló_pályaszám;
        internal Label Label2;
        internal DateTimePicker Dátumtól;
        internal Label Label3;
        internal DateTimePicker Dátumig;
        internal Label Label4;
        internal TabPage TabPage4;
        internal Button Frissíti_darabszámokat;
        internal DataGridView Tábla_darabszámok;
        internal TabPage TabPage5;
        internal Button Gombok_frissít;
        internal ComboBox Gombok_típus;
        internal Label Label5;
        internal Panel GombPanel;
        internal Button Járműlista_excel;
        internal Button Egysorfel;
        internal DataGridView Tábla_Hibalista;
        internal DataGridView Napló_tábla;
        private Zuby.ADGV.AdvancedDataGridView Táblalista;
    }
}