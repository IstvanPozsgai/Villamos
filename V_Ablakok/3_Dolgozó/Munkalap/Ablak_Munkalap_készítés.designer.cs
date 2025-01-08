using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
    public partial class Ablak_Munkalap_készítés : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Munkalap_készítés));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Dátum = new System.Windows.Forms.DateTimePicker();
            this.Button13 = new System.Windows.Forms.Button();
            this.Csoport = new System.Windows.Forms.CheckedListBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Kiadta = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Ellenőrizte = new System.Windows.Forms.ComboBox();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Label3 = new System.Windows.Forms.Label();
            this.Option7 = new System.Windows.Forms.RadioButton();
            this.Option6 = new System.Windows.Forms.RadioButton();
            this.Munkarendlist = new System.Windows.Forms.ListBox();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.Label4 = new System.Windows.Forms.Label();
            this.Option8 = new System.Windows.Forms.RadioButton();
            this.Option9 = new System.Windows.Forms.RadioButton();
            this.MunkafolyamatTábla = new System.Windows.Forms.DataGridView();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.Üressorszám = new System.Windows.Forms.TextBox();
            this.Üressor = new System.Windows.Forms.CheckBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Panel5 = new System.Windows.Forms.Panel();
            this.E1_pályaszámok = new System.Windows.Forms.CheckBox();
            this.E3ICS = new System.Windows.Forms.CheckBox();
            this.E2ICS = new System.Windows.Forms.CheckBox();
            this.E3pályaszám = new System.Windows.Forms.CheckBox();
            this.E2pályaszám = new System.Windows.Forms.CheckBox();
            this.Mindenpsz = new System.Windows.Forms.CheckBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Panel6 = new System.Windows.Forms.Panel();
            this.Label7 = new System.Windows.Forms.Label();
            this.Option11 = new System.Windows.Forms.RadioButton();
            this.Option10 = new System.Windows.Forms.RadioButton();
            this.Excel = new System.Windows.Forms.Button();
            this.Command14 = new System.Windows.Forms.Button();
            this.Csuk = new System.Windows.Forms.Button();
            this.Jelöltcsoport = new System.Windows.Forms.Button();
            this.Csoportvissza = new System.Windows.Forms.Button();
            this.Mindtöröl = new System.Windows.Forms.Button();
            this.Összeskijelöl = new System.Windows.Forms.Button();
            this.Dolgozónév = new System.Windows.Forms.DataGridView();
            this.V1Tábla = new System.Windows.Forms.DataGridView();
            this.Típusoklistája = new System.Windows.Forms.CheckedListBox();
            this.ListFájl = new System.Windows.Forms.ListBox();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Csoportkijelölmind = new System.Windows.Forms.Button();
            this.Nyit = new System.Windows.Forms.Button();
            this.Benn_Lévők = new System.Windows.Forms.Button();
            this.Holtart = new V_MindenEgyéb.MyProgressbar();
            this.Panel1.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.Panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MunkafolyamatTábla)).BeginInit();
            this.Panel4.SuspendLayout();
            this.Panel5.SuspendLayout();
            this.Panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dolgozónév)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.V1Tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.Cmbtelephely);
            this.Panel1.Controls.Add(this.Label13);
            this.Panel1.Location = new System.Drawing.Point(3, 3);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(373, 33);
            this.Panel1.TabIndex = 54;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(175, 2);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(12, 5);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(145, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // Dátum
            // 
            this.Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum.Location = new System.Drawing.Point(381, 8);
            this.Dátum.MinDate = new System.DateTime(2016, 1, 1, 0, 0, 0, 0);
            this.Dátum.Name = "Dátum";
            this.Dátum.Size = new System.Drawing.Size(130, 26);
            this.Dátum.TabIndex = 57;
            this.Dátum.ValueChanged += new System.EventHandler(this.Dátum_ValueChanged);
            // 
            // Button13
            // 
            this.Button13.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.Button13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button13.Location = new System.Drawing.Point(380, 93);
            this.Button13.Name = "Button13";
            this.Button13.Size = new System.Drawing.Size(40, 40);
            this.Button13.TabIndex = 58;
            this.Button13.UseVisualStyleBackColor = true;
            this.Button13.Click += new System.EventHandler(this.Button13_Click);
            // 
            // Csoport
            // 
            this.Csoport.CheckOnClick = true;
            this.Csoport.FormattingEnabled = true;
            this.Csoport.Location = new System.Drawing.Point(3, 42);
            this.Csoport.Name = "Csoport";
            this.Csoport.Size = new System.Drawing.Size(371, 25);
            this.Csoport.TabIndex = 60;
            // 
            // Label1
            // 
            this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 500);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(113, 20);
            this.Label1.TabIndex = 62;
            this.Label1.Text = "Munkát kiadta:";
            // 
            // Kiadta
            // 
            this.Kiadta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Kiadta.FormattingEnabled = true;
            this.Kiadta.Location = new System.Drawing.Point(8, 523);
            this.Kiadta.Name = "Kiadta";
            this.Kiadta.Size = new System.Drawing.Size(368, 28);
            this.Kiadta.TabIndex = 63;
            // 
            // Label2
            // 
            this.Label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(12, 554);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(142, 20);
            this.Label2.TabIndex = 64;
            this.Label2.Text = "Munkát ellenőrizte:";
            // 
            // Ellenőrizte
            // 
            this.Ellenőrizte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Ellenőrizte.FormattingEnabled = true;
            this.Ellenőrizte.Location = new System.Drawing.Point(8, 577);
            this.Ellenőrizte.Name = "Ellenőrizte";
            this.Ellenőrizte.Size = new System.Drawing.Size(368, 28);
            this.Ellenőrizte.TabIndex = 65;
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.Color.Green;
            this.Panel2.Controls.Add(this.Label3);
            this.Panel2.Controls.Add(this.Option7);
            this.Panel2.Controls.Add(this.Option6);
            this.Panel2.Location = new System.Drawing.Point(382, 238);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(160, 92);
            this.Panel2.TabIndex = 67;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(4, 3);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(78, 20);
            this.Label3.TabIndex = 2;
            this.Label3.Text = "Munkalap";
            // 
            // Option7
            // 
            this.Option7.AutoSize = true;
            this.Option7.Location = new System.Drawing.Point(9, 62);
            this.Option7.Name = "Option7";
            this.Option7.Size = new System.Drawing.Size(100, 24);
            this.Option7.TabIndex = 1;
            this.Option7.Text = "Csoportos";
            this.Option7.UseVisualStyleBackColor = true;
            // 
            // Option6
            // 
            this.Option6.AutoSize = true;
            this.Option6.Checked = true;
            this.Option6.Location = new System.Drawing.Point(8, 32);
            this.Option6.Name = "Option6";
            this.Option6.Size = new System.Drawing.Size(75, 24);
            this.Option6.TabIndex = 0;
            this.Option6.TabStop = true;
            this.Option6.Text = "Egyéni";
            this.Option6.UseVisualStyleBackColor = true;
            // 
            // Munkarendlist
            // 
            this.Munkarendlist.FormattingEnabled = true;
            this.Munkarendlist.ItemHeight = 20;
            this.Munkarendlist.Location = new System.Drawing.Point(381, 336);
            this.Munkarendlist.Name = "Munkarendlist";
            this.Munkarendlist.Size = new System.Drawing.Size(161, 124);
            this.Munkarendlist.TabIndex = 68;
            // 
            // Panel3
            // 
            this.Panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel3.BackColor = System.Drawing.Color.Green;
            this.Panel3.Controls.Add(this.Label4);
            this.Panel3.Controls.Add(this.Option8);
            this.Panel3.Controls.Add(this.Option9);
            this.Panel3.Location = new System.Drawing.Point(1059, 446);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(168, 52);
            this.Panel3.TabIndex = 69;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.BackColor = System.Drawing.Color.Green;
            this.Label4.Location = new System.Drawing.Point(4, 3);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(85, 20);
            this.Label4.TabIndex = 2;
            this.Label4.Text = "Nyomtatás";
            // 
            // Option8
            // 
            this.Option8.AutoSize = true;
            this.Option8.BackColor = System.Drawing.Color.Green;
            this.Option8.Location = new System.Drawing.Point(89, 26);
            this.Option8.Name = "Option8";
            this.Option8.Size = new System.Drawing.Size(60, 24);
            this.Option8.TabIndex = 1;
            this.Option8.Text = "Nem";
            this.Option8.UseVisualStyleBackColor = false;
            // 
            // Option9
            // 
            this.Option9.AutoSize = true;
            this.Option9.BackColor = System.Drawing.Color.Green;
            this.Option9.Checked = true;
            this.Option9.Location = new System.Drawing.Point(8, 26);
            this.Option9.Name = "Option9";
            this.Option9.Size = new System.Drawing.Size(59, 24);
            this.Option9.TabIndex = 0;
            this.Option9.TabStop = true;
            this.Option9.Text = "Igen";
            this.Option9.UseVisualStyleBackColor = false;
            // 
            // MunkafolyamatTábla
            // 
            this.MunkafolyamatTábla.AllowUserToAddRows = false;
            this.MunkafolyamatTábla.AllowUserToDeleteRows = false;
            this.MunkafolyamatTábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MunkafolyamatTábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MunkafolyamatTábla.Location = new System.Drawing.Point(548, 39);
            this.MunkafolyamatTábla.Name = "MunkafolyamatTábla";
            this.MunkafolyamatTábla.RowHeadersWidth = 51;
            this.MunkafolyamatTábla.Size = new System.Drawing.Size(505, 572);
            this.MunkafolyamatTábla.TabIndex = 85;
            // 
            // Panel4
            // 
            this.Panel4.BackColor = System.Drawing.Color.Green;
            this.Panel4.Controls.Add(this.Üressorszám);
            this.Panel4.Controls.Add(this.Üressor);
            this.Panel4.Controls.Add(this.Label5);
            this.Panel4.Location = new System.Drawing.Point(382, 482);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(160, 92);
            this.Panel4.TabIndex = 86;
            // 
            // Üressorszám
            // 
            this.Üressorszám.Location = new System.Drawing.Point(10, 58);
            this.Üressorszám.Name = "Üressorszám";
            this.Üressorszám.Size = new System.Drawing.Size(102, 26);
            this.Üressorszám.TabIndex = 4;
            this.Üressorszám.Text = "5";
            // 
            // Üressor
            // 
            this.Üressor.AutoSize = true;
            this.Üressor.Checked = true;
            this.Üressor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Üressor.Location = new System.Drawing.Point(9, 27);
            this.Üressor.Name = "Üressor";
            this.Üressor.Size = new System.Drawing.Size(114, 24);
            this.Üressor.TabIndex = 3;
            this.Üressor.Text = "Kell üres sor";
            this.Üressor.UseVisualStyleBackColor = true;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(4, 3);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(86, 20);
            this.Label5.TabIndex = 2;
            this.Label5.Text = "Üres sorok";
            // 
            // Panel5
            // 
            this.Panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel5.BackColor = System.Drawing.Color.Green;
            this.Panel5.Controls.Add(this.E1_pályaszámok);
            this.Panel5.Controls.Add(this.E3ICS);
            this.Panel5.Controls.Add(this.E2ICS);
            this.Panel5.Controls.Add(this.E3pályaszám);
            this.Panel5.Controls.Add(this.E2pályaszám);
            this.Panel5.Controls.Add(this.Mindenpsz);
            this.Panel5.Controls.Add(this.Label6);
            this.Panel5.Location = new System.Drawing.Point(1059, 12);
            this.Panel5.Name = "Panel5";
            this.Panel5.Size = new System.Drawing.Size(168, 211);
            this.Panel5.TabIndex = 88;
            // 
            // E1_pályaszámok
            // 
            this.E1_pályaszámok.AutoSize = true;
            this.E1_pályaszámok.BackColor = System.Drawing.Color.Green;
            this.E1_pályaszámok.Location = new System.Drawing.Point(5, 60);
            this.E1_pályaszámok.Name = "E1_pályaszámok";
            this.E1_pályaszámok.Size = new System.Drawing.Size(127, 24);
            this.E1_pályaszámok.TabIndex = 8;
            this.E1_pályaszámok.Text = "E1 pályaszám";
            this.E1_pályaszámok.UseVisualStyleBackColor = false;
            // 
            // E3ICS
            // 
            this.E3ICS.AutoSize = true;
            this.E3ICS.BackColor = System.Drawing.Color.Green;
            this.E3ICS.Location = new System.Drawing.Point(5, 181);
            this.E3ICS.Name = "E3ICS";
            this.E3ICS.Size = new System.Drawing.Size(127, 24);
            this.E3ICS.TabIndex = 7;
            this.E3ICS.Text = "E3 ICS-KCSV";
            this.E3ICS.UseVisualStyleBackColor = false;
            // 
            // E2ICS
            // 
            this.E2ICS.AutoSize = true;
            this.E2ICS.BackColor = System.Drawing.Color.Green;
            this.E2ICS.Location = new System.Drawing.Point(5, 151);
            this.E2ICS.Name = "E2ICS";
            this.E2ICS.Size = new System.Drawing.Size(127, 24);
            this.E2ICS.TabIndex = 6;
            this.E2ICS.Text = "E2 ICS-KCSV";
            this.E2ICS.UseVisualStyleBackColor = false;
            // 
            // E3pályaszám
            // 
            this.E3pályaszám.AutoSize = true;
            this.E3pályaszám.BackColor = System.Drawing.Color.Green;
            this.E3pályaszám.Location = new System.Drawing.Point(5, 120);
            this.E3pályaszám.Name = "E3pályaszám";
            this.E3pályaszám.Size = new System.Drawing.Size(90, 24);
            this.E3pályaszám.TabIndex = 5;
            this.E3pályaszám.Text = "E3 T5C5";
            this.E3pályaszám.UseVisualStyleBackColor = false;
            // 
            // E2pályaszám
            // 
            this.E2pályaszám.AutoSize = true;
            this.E2pályaszám.BackColor = System.Drawing.Color.Green;
            this.E2pályaszám.Location = new System.Drawing.Point(5, 90);
            this.E2pályaszám.Name = "E2pályaszám";
            this.E2pályaszám.Size = new System.Drawing.Size(90, 24);
            this.E2pályaszám.TabIndex = 4;
            this.E2pályaszám.Text = "E2 T5C5";
            this.E2pályaszám.UseVisualStyleBackColor = false;
            // 
            // Mindenpsz
            // 
            this.Mindenpsz.AutoSize = true;
            this.Mindenpsz.BackColor = System.Drawing.Color.Green;
            this.Mindenpsz.Location = new System.Drawing.Point(5, 30);
            this.Mindenpsz.Name = "Mindenpsz";
            this.Mindenpsz.Size = new System.Drawing.Size(159, 24);
            this.Mindenpsz.TabIndex = 3;
            this.Mindenpsz.Text = "Minden pályaszám";
            this.Mindenpsz.UseVisualStyleBackColor = false;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.BackColor = System.Drawing.Color.Green;
            this.Label6.Location = new System.Drawing.Point(4, 3);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(102, 20);
            this.Label6.TabIndex = 2;
            this.Label6.Text = "Pályaszámok";
            // 
            // Panel6
            // 
            this.Panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel6.BackColor = System.Drawing.Color.Green;
            this.Panel6.Controls.Add(this.Label7);
            this.Panel6.Controls.Add(this.Option11);
            this.Panel6.Controls.Add(this.Option10);
            this.Panel6.Location = new System.Drawing.Point(1059, 504);
            this.Panel6.Name = "Panel6";
            this.Panel6.Size = new System.Drawing.Size(168, 56);
            this.Panel6.TabIndex = 90;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.BackColor = System.Drawing.Color.Green;
            this.Label7.Location = new System.Drawing.Point(4, 3);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(86, 20);
            this.Label7.TabIndex = 2;
            this.Label7.Text = "Fájl törlése";
            // 
            // Option11
            // 
            this.Option11.AutoSize = true;
            this.Option11.BackColor = System.Drawing.Color.Green;
            this.Option11.Location = new System.Drawing.Point(89, 26);
            this.Option11.Name = "Option11";
            this.Option11.Size = new System.Drawing.Size(60, 24);
            this.Option11.TabIndex = 1;
            this.Option11.Text = "Nem";
            this.Option11.UseVisualStyleBackColor = false;
            // 
            // Option10
            // 
            this.Option10.AutoSize = true;
            this.Option10.BackColor = System.Drawing.Color.Green;
            this.Option10.Checked = true;
            this.Option10.Location = new System.Drawing.Point(8, 26);
            this.Option10.Name = "Option10";
            this.Option10.Size = new System.Drawing.Size(59, 24);
            this.Option10.TabIndex = 0;
            this.Option10.TabStop = true;
            this.Option10.Text = "Igen";
            this.Option10.UseVisualStyleBackColor = false;
            // 
            // Excel
            // 
            this.Excel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Excel.BackgroundImage = global::Villamos.Properties.Resources.Yellow_Glass_Folders_Icon_28;
            this.Excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excel.Location = new System.Drawing.Point(1059, 566);
            this.Excel.Name = "Excel";
            this.Excel.Size = new System.Drawing.Size(45, 45);
            this.Excel.TabIndex = 91;
            this.ToolTip1.SetToolTip(this.Excel, "Elkészíti a feltételeknek megfelelően a munkalapot");
            this.Excel.UseVisualStyleBackColor = true;
            this.Excel.Click += new System.EventHandler(this.Excel_Click);
            // 
            // Command14
            // 
            this.Command14.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Command14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command14.Location = new System.Drawing.Point(502, 93);
            this.Command14.Name = "Command14";
            this.Command14.Size = new System.Drawing.Size(40, 40);
            this.Command14.TabIndex = 92;
            this.Command14.UseVisualStyleBackColor = true;
            this.Command14.Click += new System.EventHandler(this.Command14_Click);
            // 
            // Csuk
            // 
            this.Csuk.BackgroundImage = global::Villamos.Properties.Resources.fel;
            this.Csuk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Csuk.Location = new System.Drawing.Point(378, 47);
            this.Csuk.Name = "Csuk";
            this.Csuk.Size = new System.Drawing.Size(40, 40);
            this.Csuk.TabIndex = 94;
            this.ToolTip1.SetToolTip(this.Csuk, "Felcsukja a csoport listát");
            this.Csuk.UseVisualStyleBackColor = true;
            this.Csuk.Visible = false;
            this.Csuk.Click += new System.EventHandler(this.Csuk_Click);
            // 
            // Jelöltcsoport
            // 
            this.Jelöltcsoport.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Jelöltcsoport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Jelöltcsoport.Location = new System.Drawing.Point(502, 47);
            this.Jelöltcsoport.Name = "Jelöltcsoport";
            this.Jelöltcsoport.Size = new System.Drawing.Size(40, 40);
            this.Jelöltcsoport.TabIndex = 97;
            this.ToolTip1.SetToolTip(this.Jelöltcsoport, "Listázza a kijelölt csoport tagjait");
            this.Jelöltcsoport.UseVisualStyleBackColor = true;
            this.Jelöltcsoport.Click += new System.EventHandler(this.Jelöltcsoport_Click);
            // 
            // Csoportvissza
            // 
            this.Csoportvissza.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.Csoportvissza.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Csoportvissza.Location = new System.Drawing.Point(463, 47);
            this.Csoportvissza.Name = "Csoportvissza";
            this.Csoportvissza.Size = new System.Drawing.Size(40, 40);
            this.Csoportvissza.TabIndex = 96;
            this.ToolTip1.SetToolTip(this.Csoportvissza, "Minden kijelölést töröl");
            this.Csoportvissza.UseVisualStyleBackColor = true;
            this.Csoportvissza.Click += new System.EventHandler(this.Csoportvissza_Click);
            // 
            // Mindtöröl
            // 
            this.Mindtöröl.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.Mindtöröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Mindtöröl.Location = new System.Drawing.Point(465, 192);
            this.Mindtöröl.Name = "Mindtöröl";
            this.Mindtöröl.Size = new System.Drawing.Size(40, 40);
            this.Mindtöröl.TabIndex = 99;
            this.ToolTip1.SetToolTip(this.Mindtöröl, "Minden kijelölést töröl");
            this.Mindtöröl.UseVisualStyleBackColor = true;
            this.Mindtöröl.Click += new System.EventHandler(this.Mindtöröl_Click);
            // 
            // Összeskijelöl
            // 
            this.Összeskijelöl.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.Összeskijelöl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Összeskijelöl.Location = new System.Drawing.Point(424, 192);
            this.Összeskijelöl.Name = "Összeskijelöl";
            this.Összeskijelöl.Size = new System.Drawing.Size(40, 40);
            this.Összeskijelöl.TabIndex = 98;
            this.ToolTip1.SetToolTip(this.Összeskijelöl, "Minden dolgozót kijelöl");
            this.Összeskijelöl.UseVisualStyleBackColor = true;
            this.Összeskijelöl.Click += new System.EventHandler(this.Összeskijelöl_Click);
            // 
            // Dolgozónév
            // 
            this.Dolgozónév.AllowUserToAddRows = false;
            this.Dolgozónév.AllowUserToDeleteRows = false;
            this.Dolgozónév.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Dolgozónév.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dolgozónév.Location = new System.Drawing.Point(3, 77);
            this.Dolgozónév.Name = "Dolgozónév";
            this.Dolgozónév.RowHeadersWidth = 20;
            this.Dolgozónév.Size = new System.Drawing.Size(370, 423);
            this.Dolgozónév.TabIndex = 100;
            // 
            // V1Tábla
            // 
            this.V1Tábla.AllowUserToAddRows = false;
            this.V1Tábla.AllowUserToDeleteRows = false;
            this.V1Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.V1Tábla.Location = new System.Drawing.Point(381, 586);
            this.V1Tábla.Name = "V1Tábla";
            this.V1Tábla.RowHeadersWidth = 51;
            this.V1Tábla.Size = new System.Drawing.Size(39, 25);
            this.V1Tábla.TabIndex = 101;
            this.V1Tábla.Visible = false;
            // 
            // Típusoklistája
            // 
            this.Típusoklistája.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Típusoklistája.CheckOnClick = true;
            this.Típusoklistája.FormattingEnabled = true;
            this.Típusoklistája.Location = new System.Drawing.Point(1059, 229);
            this.Típusoklistája.Name = "Típusoklistája";
            this.Típusoklistája.Size = new System.Drawing.Size(168, 193);
            this.Típusoklistája.TabIndex = 103;
            // 
            // ListFájl
            // 
            this.ListFájl.FormattingEnabled = true;
            this.ListFájl.ItemHeight = 20;
            this.ListFájl.Location = new System.Drawing.Point(502, 587);
            this.ListFájl.Name = "ListFájl";
            this.ListFájl.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ListFájl.Size = new System.Drawing.Size(38, 4);
            this.ListFájl.TabIndex = 104;
            this.ListFájl.Visible = false;
            // 
            // ToolTip1
            // 
            this.ToolTip1.ToolTipTitle = "Dolgozónevek";
            // 
            // Csoportkijelölmind
            // 
            this.Csoportkijelölmind.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.Csoportkijelölmind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Csoportkijelölmind.Location = new System.Drawing.Point(422, 47);
            this.Csoportkijelölmind.Name = "Csoportkijelölmind";
            this.Csoportkijelölmind.Size = new System.Drawing.Size(40, 40);
            this.Csoportkijelölmind.TabIndex = 95;
            this.ToolTip1.SetToolTip(this.Csoportkijelölmind, "Minden csoportot kijelöl");
            this.Csoportkijelölmind.UseVisualStyleBackColor = true;
            this.Csoportkijelölmind.Click += new System.EventHandler(this.Csoportkijelölmind_Click);
            // 
            // Nyit
            // 
            this.Nyit.BackgroundImage = global::Villamos.Properties.Resources.le;
            this.Nyit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Nyit.Location = new System.Drawing.Point(380, 47);
            this.Nyit.Name = "Nyit";
            this.Nyit.Size = new System.Drawing.Size(40, 40);
            this.Nyit.TabIndex = 93;
            this.ToolTip1.SetToolTip(this.Nyit, "Lenyitja a csoport listát");
            this.Nyit.UseVisualStyleBackColor = true;
            this.Nyit.Click += new System.EventHandler(this.Nyit_Click);
            // 
            // Benn_Lévők
            // 
            this.Benn_Lévők.BackgroundImage = global::Villamos.Properties.Resources.felhasználók32;
            this.Benn_Lévők.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Benn_Lévők.Location = new System.Drawing.Point(380, 141);
            this.Benn_Lévők.Name = "Benn_Lévők";
            this.Benn_Lévők.Size = new System.Drawing.Size(40, 40);
            this.Benn_Lévők.TabIndex = 105;
            this.ToolTip1.SetToolTip(this.Benn_Lévők, "A kiválasztott csoport(ok) benn levő dolgozóinak kijelölése.");
            this.Benn_Lévők.UseVisualStyleBackColor = true;
            this.Benn_Lévők.Click += new System.EventHandler(this.Benn_Lévők_Click);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(530, 10);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(525, 25);
            this.Holtart.TabIndex = 106;
            this.Holtart.Visible = false;
            // 
            // Ablak_Munkalap_készítés
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.ClientSize = new System.Drawing.Size(1239, 616);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Benn_Lévők);
            this.Controls.Add(this.ListFájl);
            this.Controls.Add(this.Típusoklistája);
            this.Controls.Add(this.V1Tábla);
            this.Controls.Add(this.Csoport);
            this.Controls.Add(this.Dolgozónév);
            this.Controls.Add(this.Mindtöröl);
            this.Controls.Add(this.Összeskijelöl);
            this.Controls.Add(this.Jelöltcsoport);
            this.Controls.Add(this.Csoportvissza);
            this.Controls.Add(this.Csoportkijelölmind);
            this.Controls.Add(this.Nyit);
            this.Controls.Add(this.Csuk);
            this.Controls.Add(this.Command14);
            this.Controls.Add(this.Excel);
            this.Controls.Add(this.Panel6);
            this.Controls.Add(this.Panel5);
            this.Controls.Add(this.Panel4);
            this.Controls.Add(this.MunkafolyamatTábla);
            this.Controls.Add(this.Panel3);
            this.Controls.Add(this.Munkarendlist);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.Ellenőrizte);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Kiadta);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Button13);
            this.Controls.Add(this.Dátum);
            this.Controls.Add(this.Panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Munkalap_készítés";
            this.Text = "Ablak_Munkalap";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_Munkalap_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MunkafolyamatTábla)).EndInit();
            this.Panel4.ResumeLayout(false);
            this.Panel4.PerformLayout();
            this.Panel5.ResumeLayout(false);
            this.Panel5.PerformLayout();
            this.Panel6.ResumeLayout(false);
            this.Panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dolgozónév)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.V1Tábla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal Panel Panel1;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal DateTimePicker Dátum;
        internal Button Button13;
        internal CheckedListBox Csoport;
        internal Label Label1;
        internal ComboBox Kiadta;
        internal Label Label2;
        internal ComboBox Ellenőrizte;
        internal Panel Panel2;
        internal Label Label3;
        internal RadioButton Option7;
        internal RadioButton Option6;
        internal ListBox Munkarendlist;
        internal Panel Panel3;
        internal Label Label4;
        internal RadioButton Option8;
        internal RadioButton Option9;
        internal DataGridView MunkafolyamatTábla;
        internal Panel Panel4;
        internal TextBox Üressorszám;
        internal CheckBox Üressor;
        internal Label Label5;
        internal Panel Panel5;
        internal CheckBox E3pályaszám;
        internal CheckBox E2pályaszám;
        internal CheckBox Mindenpsz;
        internal Label Label6;
        internal Panel Panel6;
        internal Label Label7;
        internal RadioButton Option11;
        internal RadioButton Option10;
        internal Button Excel;
        internal Button Command14;
        internal Button Nyit;
        internal Button Csuk;
        internal Button Jelöltcsoport;
        internal Button Csoportvissza;
        internal Button Csoportkijelölmind;
        internal Button Mindtöröl;
        internal Button Összeskijelöl;
        internal DataGridView Dolgozónév;
        internal DataGridView V1Tábla;
        internal CheckedListBox Típusoklistája;
        internal ListBox ListFájl;
        internal ToolTip ToolTip1;
        internal CheckBox E3ICS;
        internal CheckBox E2ICS;
        internal CheckBox E1_pályaszámok;
        internal Button Benn_Lévők;
        private V_MindenEgyéb.MyProgressbar Holtart;
    }
}