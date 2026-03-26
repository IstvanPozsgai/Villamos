using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
    
    public partial class Ablak_Épülettakarítás : Form
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
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.LapFülek = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.Idő_lakat = new System.Windows.Forms.Panel();
            this.Nyitva = new System.Windows.Forms.Button();
            this.Zárva = new System.Windows.Forms.Button();
            this.Helység_friss = new System.Windows.Forms.Button();
            this.E2Minden_töröl = new System.Windows.Forms.Button();
            this.E3Minden_töröl = new System.Windows.Forms.Button();
            this.E1Minden_töröl = new System.Windows.Forms.Button();
            this.Osztálylista = new System.Windows.Forms.CheckedListBox();
            this.Tábla_terv = new System.Windows.Forms.DataGridView();
            this.DataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.E2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.E3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Nap = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.Label2 = new System.Windows.Forms.Label();
            this.Option10 = new System.Windows.Forms.RadioButton();
            this.Option11 = new System.Windows.Forms.RadioButton();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Label1 = new System.Windows.Forms.Label();
            this.Option9 = new System.Windows.Forms.RadioButton();
            this.Option8 = new System.Windows.Forms.RadioButton();
            this.Excellekérdezés = new System.Windows.Forms.Button();
            this.Command4 = new System.Windows.Forms.Button();
            this.Szemetes = new System.Windows.Forms.Button();
            this.KapcsoltHelységFő = new System.Windows.Forms.Button();
            this.KapcsoltHelységAl = new System.Windows.Forms.Button();
            this.Terv_Rögzítés = new System.Windows.Forms.Button();
            this.E1MindenNap = new System.Windows.Forms.Button();
            this.E2MindenNap = new System.Windows.Forms.Button();
            this.E3MindenNap = new System.Windows.Forms.Button();
            this.E1Munkanap = new System.Windows.Forms.Button();
            this.E2Munkanap = new System.Windows.Forms.Button();
            this.E3Munkanap = new System.Windows.Forms.Button();
            this.Dátum = new System.Windows.Forms.DateTimePicker();
            this.Command14 = new System.Windows.Forms.Button();
            this.Mindtöröl = new System.Windows.Forms.Button();
            this.ÖsszesKijelöl = new System.Windows.Forms.Button();
            this.Jelöltcsoport = new System.Windows.Forms.Button();
            this.CsoportVissza = new System.Windows.Forms.Button();
            this.Csoportkijelöltmind = new System.Windows.Forms.Button();
            this.Helyiséglista = new System.Windows.Forms.CheckedListBox();
            this.Csuk = new System.Windows.Forms.Button();
            this.Nyit = new System.Windows.Forms.Button();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.BMR = new System.Windows.Forms.Button();
            this.Opció_kifizetés = new System.Windows.Forms.Button();
            this.Opció_Megrendelés = new System.Windows.Forms.Button();
            this.List1 = new System.Windows.Forms.CheckedListBox();
            this.Tábla1 = new System.Windows.Forms.DataGridView();
            this.Dátum1 = new System.Windows.Forms.DateTimePicker();
            this.Zárva1 = new System.Windows.Forms.Button();
            this.Mentés = new System.Windows.Forms.Button();
            this.Nyitva1 = new System.Windows.Forms.Button();
            this.Command9 = new System.Windows.Forms.Button();
            this.Le1 = new System.Windows.Forms.Button();
            this.Command2 = new System.Windows.Forms.Button();
            this.Command10 = new System.Windows.Forms.Button();
            this.Fel1 = new System.Windows.Forms.Button();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.Táblázat_frissítése = new System.Windows.Forms.Button();
            this.Naptár_Tábla = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Munkanap = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Hétvége = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Dátum2 = new System.Windows.Forms.DateTimePicker();
            this.Alap_Rögzít = new System.Windows.Forms.Button();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Chk_CTRL = new System.Windows.Forms.CheckBox();
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Panel2.SuspendLayout();
            this.LapFülek.SuspendLayout();
            this.TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_terv)).BeginInit();
            this.Panel3.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla1)).BeginInit();
            this.TabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Naptár_Tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(347, 11);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(752, 28);
            this.Holtart.TabIndex = 175;
            this.Holtart.Visible = false;
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.Cmbtelephely);
            this.Panel2.Controls.Add(this.Label13);
            this.Panel2.Location = new System.Drawing.Point(5, 5);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(335, 45);
            this.Panel2.TabIndex = 173;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(145, 6);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
            this.Cmbtelephely.SelectionChangeCommitted += new System.EventHandler(this.Cmbtelephely_SelectionChangeCommitted);
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(4, 12);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(145, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // LapFülek
            // 
            this.LapFülek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LapFülek.Controls.Add(this.TabPage1);
            this.LapFülek.Controls.Add(this.TabPage2);
            this.LapFülek.Controls.Add(this.TabPage3);
            this.LapFülek.Location = new System.Drawing.Point(5, 56);
            this.LapFülek.Name = "LapFülek";
            this.LapFülek.Padding = new System.Drawing.Point(16, 3);
            this.LapFülek.SelectedIndex = 0;
            this.LapFülek.Size = new System.Drawing.Size(1139, 562);
            this.LapFülek.TabIndex = 176;
            this.LapFülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LapFülek_DrawItem);
            this.LapFülek.SelectedIndexChanged += new System.EventHandler(this.LapFülek_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.TabPage1.Controls.Add(this.Idő_lakat);
            this.TabPage1.Controls.Add(this.Nyitva);
            this.TabPage1.Controls.Add(this.Zárva);
            this.TabPage1.Controls.Add(this.Helység_friss);
            this.TabPage1.Controls.Add(this.E2Minden_töröl);
            this.TabPage1.Controls.Add(this.E3Minden_töröl);
            this.TabPage1.Controls.Add(this.E1Minden_töröl);
            this.TabPage1.Controls.Add(this.Osztálylista);
            this.TabPage1.Controls.Add(this.Tábla_terv);
            this.TabPage1.Controls.Add(this.Panel3);
            this.TabPage1.Controls.Add(this.Panel1);
            this.TabPage1.Controls.Add(this.Excellekérdezés);
            this.TabPage1.Controls.Add(this.Command4);
            this.TabPage1.Controls.Add(this.Szemetes);
            this.TabPage1.Controls.Add(this.KapcsoltHelységFő);
            this.TabPage1.Controls.Add(this.KapcsoltHelységAl);
            this.TabPage1.Controls.Add(this.Terv_Rögzítés);
            this.TabPage1.Controls.Add(this.E1MindenNap);
            this.TabPage1.Controls.Add(this.E2MindenNap);
            this.TabPage1.Controls.Add(this.E3MindenNap);
            this.TabPage1.Controls.Add(this.E1Munkanap);
            this.TabPage1.Controls.Add(this.E2Munkanap);
            this.TabPage1.Controls.Add(this.E3Munkanap);
            this.TabPage1.Controls.Add(this.Dátum);
            this.TabPage1.Controls.Add(this.Command14);
            this.TabPage1.Controls.Add(this.Mindtöröl);
            this.TabPage1.Controls.Add(this.ÖsszesKijelöl);
            this.TabPage1.Controls.Add(this.Jelöltcsoport);
            this.TabPage1.Controls.Add(this.CsoportVissza);
            this.TabPage1.Controls.Add(this.Csoportkijelöltmind);
            this.TabPage1.Controls.Add(this.Helyiséglista);
            this.TabPage1.Controls.Add(this.Csuk);
            this.TabPage1.Controls.Add(this.Nyit);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1131, 529);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Takarítás terv készítés";
            // 
            // Idő_lakat
            // 
            this.Idő_lakat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.Idő_lakat.Location = new System.Drawing.Point(432, 93);
            this.Idő_lakat.Name = "Idő_lakat";
            this.Idő_lakat.Size = new System.Drawing.Size(93, 45);
            this.Idő_lakat.TabIndex = 217;
            this.ToolTip1.SetToolTip(this.Idő_lakat, "Biztonsági zár, CTRL mellett kattintással nyílik");
            this.Idő_lakat.Click += new System.EventHandler(this.Idő_lakat_Click);
            // 
            // Nyitva
            // 
            this.Nyitva.BackgroundImage = global::Villamos.Properties.Resources.lakatnyitva32;
            this.Nyitva.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Nyitva.Location = new System.Drawing.Point(587, 96);
            this.Nyitva.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Nyitva.Name = "Nyitva";
            this.Nyitva.Size = new System.Drawing.Size(40, 40);
            this.Nyitva.TabIndex = 191;
            this.ToolTip1.SetToolTip(this.Nyitva, "Nyitott Tervezés, rákattintást követően záródik le.");
            this.Nyitva.UseVisualStyleBackColor = true;
            this.Nyitva.Click += new System.EventHandler(this.Nyitva_Click);
            // 
            // Zárva
            // 
            this.Zárva.BackgroundImage = global::Villamos.Properties.Resources.Lakatzárva32;
            this.Zárva.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Zárva.Location = new System.Drawing.Point(545, 96);
            this.Zárva.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Zárva.Name = "Zárva";
            this.Zárva.Size = new System.Drawing.Size(40, 40);
            this.Zárva.TabIndex = 192;
            this.ToolTip1.SetToolTip(this.Zárva, "Lezárt Tervezés, rákattintást követően nyilik ki.");
            this.Zárva.UseVisualStyleBackColor = true;
            this.Zárva.Click += new System.EventHandler(this.Zárva_Click);
            // 
            // Helység_friss
            // 
            this.Helység_friss.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Helység_friss.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Helység_friss.Location = new System.Drawing.Point(425, 50);
            this.Helység_friss.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Helység_friss.Name = "Helység_friss";
            this.Helység_friss.Size = new System.Drawing.Size(40, 40);
            this.Helység_friss.TabIndex = 216;
            this.ToolTip1.SetToolTip(this.Helység_friss, "Helységlistában kijelölt elemnek kiírja a napi tervét");
            this.Helység_friss.UseVisualStyleBackColor = true;
            this.Helység_friss.Click += new System.EventHandler(this.Helység_friss_Click);
            // 
            // E2Minden_töröl
            // 
            this.E2Minden_töröl.BackgroundImage = global::Villamos.Properties.Resources.e2_töröl;
            this.E2Minden_töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.E2Minden_töröl.Location = new System.Drawing.Point(465, 260);
            this.E2Minden_töröl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.E2Minden_töröl.Name = "E2Minden_töröl";
            this.E2Minden_töröl.Size = new System.Drawing.Size(40, 40);
            this.E2Minden_töröl.TabIndex = 212;
            this.ToolTip1.SetToolTip(this.E2Minden_töröl, "Minden E2 jelölést töröl");
            this.E2Minden_töröl.UseVisualStyleBackColor = true;
            this.E2Minden_töröl.Click += new System.EventHandler(this.E2Minden_töröl_Click);
            // 
            // E3Minden_töröl
            // 
            this.E3Minden_töröl.BackgroundImage = global::Villamos.Properties.Resources.e3_töröl;
            this.E3Minden_töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.E3Minden_töröl.Location = new System.Drawing.Point(505, 260);
            this.E3Minden_töröl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.E3Minden_töröl.Name = "E3Minden_töröl";
            this.E3Minden_töröl.Size = new System.Drawing.Size(40, 40);
            this.E3Minden_töröl.TabIndex = 211;
            this.ToolTip1.SetToolTip(this.E3Minden_töröl, "Minden E3 jelölést töröl");
            this.E3Minden_töröl.UseVisualStyleBackColor = true;
            this.E3Minden_töröl.Click += new System.EventHandler(this.E3Minden_töröl_Click);
            // 
            // E1Minden_töröl
            // 
            this.E1Minden_töröl.BackgroundImage = global::Villamos.Properties.Resources.E1_töröl;
            this.E1Minden_töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.E1Minden_töröl.Location = new System.Drawing.Point(425, 260);
            this.E1Minden_töröl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.E1Minden_töröl.Name = "E1Minden_töröl";
            this.E1Minden_töröl.Size = new System.Drawing.Size(40, 40);
            this.E1Minden_töröl.TabIndex = 210;
            this.ToolTip1.SetToolTip(this.E1Minden_töröl, "Minden E1 jelölést töröl");
            this.E1Minden_töröl.UseVisualStyleBackColor = true;
            this.E1Minden_töröl.Click += new System.EventHandler(this.E1Minden_töröl_Click);
            // 
            // Osztálylista
            // 
            this.Osztálylista.BackColor = System.Drawing.Color.Turquoise;
            this.Osztálylista.CheckOnClick = true;
            this.Osztálylista.FormattingEnabled = true;
            this.Osztálylista.Location = new System.Drawing.Point(5, 20);
            this.Osztálylista.Name = "Osztálylista";
            this.Osztálylista.Size = new System.Drawing.Size(412, 25);
            this.Osztálylista.TabIndex = 131;
            // 
            // Tábla_terv
            // 
            this.Tábla_terv.AllowUserToAddRows = false;
            this.Tábla_terv.AllowUserToDeleteRows = false;
            this.Tábla_terv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla_terv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla_terv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DataGridViewTextBoxColumn1,
            this.E1,
            this.E2,
            this.E3,
            this.Nap});
            this.Tábla_terv.Location = new System.Drawing.Point(631, 6);
            this.Tábla_terv.Name = "Tábla_terv";
            this.Tábla_terv.RowHeadersVisible = false;
            this.Tábla_terv.Size = new System.Drawing.Size(410, 517);
            this.Tábla_terv.TabIndex = 209;
            this.Tábla_terv.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Tábla_terv_CellFormatting);
            // 
            // DataGridViewTextBoxColumn1
            // 
            this.DataGridViewTextBoxColumn1.HeaderText = "Nap";
            this.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1";
            // 
            // E1
            // 
            this.E1.HeaderText = "E1";
            this.E1.Name = "E1";
            this.E1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.E1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // E2
            // 
            this.E2.HeaderText = "E2";
            this.E2.Name = "E2";
            // 
            // E3
            // 
            this.E3.HeaderText = "E3";
            this.E3.Name = "E3";
            // 
            // Nap
            // 
            this.Nap.HeaderText = "Nap";
            this.Nap.Name = "Nap";
            this.Nap.ReadOnly = true;
            this.Nap.Visible = false;
            // 
            // Panel3
            // 
            this.Panel3.BackColor = System.Drawing.Color.SteelBlue;
            this.Panel3.Controls.Add(this.Label2);
            this.Panel3.Controls.Add(this.Option10);
            this.Panel3.Controls.Add(this.Option11);
            this.Panel3.Location = new System.Drawing.Point(425, 463);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(139, 61);
            this.Panel3.TabIndex = 207;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Label2.Location = new System.Drawing.Point(3, 3);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(86, 20);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "Fájl törlése";
            // 
            // Option10
            // 
            this.Option10.AutoSize = true;
            this.Option10.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Option10.Checked = true;
            this.Option10.Location = new System.Drawing.Point(7, 31);
            this.Option10.Name = "Option10";
            this.Option10.Size = new System.Drawing.Size(59, 24);
            this.Option10.TabIndex = 1;
            this.Option10.TabStop = true;
            this.Option10.Text = "Igen";
            this.Option10.UseVisualStyleBackColor = false;
            // 
            // Option11
            // 
            this.Option11.AutoSize = true;
            this.Option11.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Option11.Location = new System.Drawing.Point(72, 31);
            this.Option11.Name = "Option11";
            this.Option11.Size = new System.Drawing.Size(60, 24);
            this.Option11.TabIndex = 0;
            this.Option11.Text = "Nem";
            this.Option11.UseVisualStyleBackColor = false;
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Controls.Add(this.Option9);
            this.Panel1.Controls.Add(this.Option8);
            this.Panel1.Location = new System.Drawing.Point(425, 396);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(139, 61);
            this.Panel1.TabIndex = 206;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Label1.Location = new System.Drawing.Point(3, 3);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(85, 20);
            this.Label1.TabIndex = 2;
            this.Label1.Text = "Nyomtatás";
            // 
            // Option9
            // 
            this.Option9.AutoSize = true;
            this.Option9.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Option9.Checked = true;
            this.Option9.Location = new System.Drawing.Point(7, 31);
            this.Option9.Name = "Option9";
            this.Option9.Size = new System.Drawing.Size(59, 24);
            this.Option9.TabIndex = 1;
            this.Option9.TabStop = true;
            this.Option9.Text = "Igen";
            this.Option9.UseVisualStyleBackColor = false;
            // 
            // Option8
            // 
            this.Option8.AutoSize = true;
            this.Option8.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Option8.Location = new System.Drawing.Point(72, 31);
            this.Option8.Name = "Option8";
            this.Option8.Size = new System.Drawing.Size(60, 24);
            this.Option8.TabIndex = 0;
            this.Option8.Text = "Nem";
            this.Option8.UseVisualStyleBackColor = false;
            // 
            // Excellekérdezés
            // 
            this.Excellekérdezés.BackgroundImage = global::Villamos.Properties.Resources.App_edit;
            this.Excellekérdezés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excellekérdezés.Location = new System.Drawing.Point(585, 348);
            this.Excellekérdezés.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Excellekérdezés.Name = "Excellekérdezés";
            this.Excellekérdezés.Size = new System.Drawing.Size(40, 40);
            this.Excellekérdezés.TabIndex = 205;
            this.ToolTip1.SetToolTip(this.Excellekérdezés, "Takarítási megrendelő késítése");
            this.Excellekérdezés.UseVisualStyleBackColor = true;
            this.Excellekérdezés.Click += new System.EventHandler(this.Excellekérdezés_Click);
            // 
            // Command4
            // 
            this.Command4.BackgroundImage = global::Villamos.Properties.Resources.Document_write;
            this.Command4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command4.Location = new System.Drawing.Point(585, 484);
            this.Command4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Command4.Name = "Command4";
            this.Command4.Size = new System.Drawing.Size(40, 40);
            this.Command4.TabIndex = 204;
            this.ToolTip1.SetToolTip(this.Command4, "Helység takarítási igazolólap készítés");
            this.Command4.UseVisualStyleBackColor = true;
            this.Command4.Click += new System.EventHandler(this.Command4_Click);
            // 
            // Szemetes
            // 
            this.Szemetes.BackgroundImage = global::Villamos.Properties.Resources.szemetes64;
            this.Szemetes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Szemetes.Location = new System.Drawing.Point(425, 307);
            this.Szemetes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Szemetes.Name = "Szemetes";
            this.Szemetes.Size = new System.Drawing.Size(40, 37);
            this.Szemetes.TabIndex = 203;
            this.ToolTip1.SetToolTip(this.Szemetes, "Van szemetes a helységben");
            this.Szemetes.UseVisualStyleBackColor = true;
            this.Szemetes.Visible = false;
            // 
            // KapcsoltHelységFő
            // 
            this.KapcsoltHelységFő.BackgroundImage = global::Villamos.Properties.Resources.hozzá;
            this.KapcsoltHelységFő.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.KapcsoltHelységFő.Location = new System.Drawing.Point(503, 304);
            this.KapcsoltHelységFő.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.KapcsoltHelységFő.Name = "KapcsoltHelységFő";
            this.KapcsoltHelységFő.Size = new System.Drawing.Size(40, 40);
            this.KapcsoltHelységFő.TabIndex = 202;
            this.ToolTip1.SetToolTip(this.KapcsoltHelységFő, "Kapcsolt helyiség van a helységhez társítva");
            this.KapcsoltHelységFő.UseVisualStyleBackColor = true;
            this.KapcsoltHelységFő.Visible = false;
            this.KapcsoltHelységFő.Click += new System.EventHandler(this.KapcsolHelység_Click);
            // 
            // KapcsoltHelységAl
            // 
            this.KapcsoltHelységAl.BackgroundImage = global::Villamos.Properties.Resources.alá;
            this.KapcsoltHelységAl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.KapcsoltHelységAl.Location = new System.Drawing.Point(503, 348);
            this.KapcsoltHelységAl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.KapcsoltHelységAl.Name = "KapcsoltHelységAl";
            this.KapcsoltHelységAl.Size = new System.Drawing.Size(40, 40);
            this.KapcsoltHelységAl.TabIndex = 201;
            this.ToolTip1.SetToolTip(this.KapcsoltHelységAl, "A helyiség egy másik helységhez van kapcsolva.");
            this.KapcsoltHelységAl.UseVisualStyleBackColor = true;
            this.KapcsoltHelységAl.Visible = false;
            this.KapcsoltHelységAl.Click += new System.EventHandler(this.KapcsoltHelységAl_Click);
            // 
            // Terv_Rögzítés
            // 
            this.Terv_Rögzítés.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Terv_Rögzítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Terv_Rögzítés.Location = new System.Drawing.Point(584, 216);
            this.Terv_Rögzítés.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Terv_Rögzítés.Name = "Terv_Rögzítés";
            this.Terv_Rögzítés.Size = new System.Drawing.Size(40, 40);
            this.Terv_Rögzítés.TabIndex = 200;
            this.ToolTip1.SetToolTip(this.Terv_Rögzítés, "Rögzíti az előtervet a helyiségeknek");
            this.Terv_Rögzítés.UseVisualStyleBackColor = true;
            this.Terv_Rögzítés.Click += new System.EventHandler(this.Terv_Rögzítés_Click);
            // 
            // E1MindenNap
            // 
            this.E1MindenNap.BackgroundImage = global::Villamos.Properties.Resources.E1minden;
            this.E1MindenNap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.E1MindenNap.Location = new System.Drawing.Point(425, 216);
            this.E1MindenNap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.E1MindenNap.Name = "E1MindenNap";
            this.E1MindenNap.Size = new System.Drawing.Size(40, 40);
            this.E1MindenNap.TabIndex = 199;
            this.ToolTip1.SetToolTip(this.E1MindenNap, "E1 minden napra jelöl");
            this.E1MindenNap.UseVisualStyleBackColor = true;
            this.E1MindenNap.Click += new System.EventHandler(this.E1MindenNap_Click);
            // 
            // E2MindenNap
            // 
            this.E2MindenNap.BackgroundImage = global::Villamos.Properties.Resources.e2minden;
            this.E2MindenNap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.E2MindenNap.Location = new System.Drawing.Point(465, 216);
            this.E2MindenNap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.E2MindenNap.Name = "E2MindenNap";
            this.E2MindenNap.Size = new System.Drawing.Size(40, 40);
            this.E2MindenNap.TabIndex = 198;
            this.ToolTip1.SetToolTip(this.E2MindenNap, "E2 minden napra jelöl");
            this.E2MindenNap.UseVisualStyleBackColor = true;
            this.E2MindenNap.Click += new System.EventHandler(this.E2MindenNap_Click);
            // 
            // E3MindenNap
            // 
            this.E3MindenNap.BackgroundImage = global::Villamos.Properties.Resources.e3minden;
            this.E3MindenNap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.E3MindenNap.Location = new System.Drawing.Point(505, 216);
            this.E3MindenNap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.E3MindenNap.Name = "E3MindenNap";
            this.E3MindenNap.Size = new System.Drawing.Size(40, 40);
            this.E3MindenNap.TabIndex = 197;
            this.ToolTip1.SetToolTip(this.E3MindenNap, "E3 minden napra jelöl");
            this.E3MindenNap.UseVisualStyleBackColor = true;
            this.E3MindenNap.Click += new System.EventHandler(this.E3MindenNap_Click);
            // 
            // E1Munkanap
            // 
            this.E1Munkanap.BackgroundImage = global::Villamos.Properties.Resources.E1hp;
            this.E1Munkanap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.E1Munkanap.Location = new System.Drawing.Point(425, 172);
            this.E1Munkanap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.E1Munkanap.Name = "E1Munkanap";
            this.E1Munkanap.Size = new System.Drawing.Size(40, 40);
            this.E1Munkanap.TabIndex = 196;
            this.ToolTip1.SetToolTip(this.E1Munkanap, "E1 hétköznap bejelöli");
            this.E1Munkanap.UseVisualStyleBackColor = true;
            this.E1Munkanap.Click += new System.EventHandler(this.E1Munkanap_Click);
            // 
            // E2Munkanap
            // 
            this.E2Munkanap.BackgroundImage = global::Villamos.Properties.Resources.e2hp;
            this.E2Munkanap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.E2Munkanap.Location = new System.Drawing.Point(465, 172);
            this.E2Munkanap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.E2Munkanap.Name = "E2Munkanap";
            this.E2Munkanap.Size = new System.Drawing.Size(40, 40);
            this.E2Munkanap.TabIndex = 195;
            this.ToolTip1.SetToolTip(this.E2Munkanap, "E2 hétköznap bejelöli");
            this.E2Munkanap.UseVisualStyleBackColor = true;
            this.E2Munkanap.Click += new System.EventHandler(this.E2Munkanap_Click);
            // 
            // E3Munkanap
            // 
            this.E3Munkanap.BackgroundImage = global::Villamos.Properties.Resources.e3hp;
            this.E3Munkanap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.E3Munkanap.Location = new System.Drawing.Point(505, 172);
            this.E3Munkanap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.E3Munkanap.Name = "E3Munkanap";
            this.E3Munkanap.Size = new System.Drawing.Size(40, 40);
            this.E3Munkanap.TabIndex = 194;
            this.ToolTip1.SetToolTip(this.E3Munkanap, "E3 hétköznap bejelöli");
            this.E3Munkanap.UseVisualStyleBackColor = true;
            this.E3Munkanap.Click += new System.EventHandler(this.E3Munkanap_Click);
            // 
            // Dátum
            // 
            this.Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum.Location = new System.Drawing.Point(425, 141);
            this.Dátum.Name = "Dátum";
            this.Dátum.Size = new System.Drawing.Size(122, 26);
            this.Dátum.TabIndex = 193;
            this.Dátum.ValueChanged += new System.EventHandler(this.Dátum_ValueChanged);
            // 
            // Command14
            // 
            this.Command14.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Command14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command14.Location = new System.Drawing.Point(545, 260);
            this.Command14.Name = "Command14";
            this.Command14.Size = new System.Drawing.Size(40, 40);
            this.Command14.TabIndex = 138;
            this.ToolTip1.SetToolTip(this.Command14, "Minden kijelölést töröl");
            this.Command14.UseVisualStyleBackColor = true;
            this.Command14.Click += new System.EventHandler(this.Command14_Click);
            // 
            // Mindtöröl
            // 
            this.Mindtöröl.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.Mindtöröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Mindtöröl.Location = new System.Drawing.Point(585, 50);
            this.Mindtöröl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Mindtöröl.Name = "Mindtöröl";
            this.Mindtöröl.Size = new System.Drawing.Size(40, 40);
            this.Mindtöröl.TabIndex = 137;
            this.ToolTip1.SetToolTip(this.Mindtöröl, "Minden kijelölést töröl");
            this.Mindtöröl.UseVisualStyleBackColor = true;
            this.Mindtöröl.Click += new System.EventHandler(this.Mindtöröl_Click);
            // 
            // ÖsszesKijelöl
            // 
            this.ÖsszesKijelöl.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.ÖsszesKijelöl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ÖsszesKijelöl.Location = new System.Drawing.Point(545, 50);
            this.ÖsszesKijelöl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ÖsszesKijelöl.Name = "ÖsszesKijelöl";
            this.ÖsszesKijelöl.Size = new System.Drawing.Size(40, 40);
            this.ÖsszesKijelöl.TabIndex = 136;
            this.ToolTip1.SetToolTip(this.ÖsszesKijelöl, "Mindent kijelöl");
            this.ÖsszesKijelöl.UseVisualStyleBackColor = true;
            this.ÖsszesKijelöl.Click += new System.EventHandler(this.ÖsszesKijelöl_Click);
            // 
            // Jelöltcsoport
            // 
            this.Jelöltcsoport.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Jelöltcsoport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Jelöltcsoport.Location = new System.Drawing.Point(505, 5);
            this.Jelöltcsoport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Jelöltcsoport.Name = "Jelöltcsoport";
            this.Jelöltcsoport.Size = new System.Drawing.Size(40, 40);
            this.Jelöltcsoport.TabIndex = 135;
            this.ToolTip1.SetToolTip(this.Jelöltcsoport, "Osztálylista kijelölt elemeinek helyiségeit listázza.");
            this.Jelöltcsoport.UseVisualStyleBackColor = true;
            this.Jelöltcsoport.Click += new System.EventHandler(this.Jelöltcsoport_Click);
            // 
            // CsoportVissza
            // 
            this.CsoportVissza.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.CsoportVissza.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CsoportVissza.Location = new System.Drawing.Point(585, 5);
            this.CsoportVissza.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CsoportVissza.Name = "CsoportVissza";
            this.CsoportVissza.Size = new System.Drawing.Size(40, 40);
            this.CsoportVissza.TabIndex = 134;
            this.ToolTip1.SetToolTip(this.CsoportVissza, "Minden kijelölést töröl");
            this.CsoportVissza.UseVisualStyleBackColor = true;
            this.CsoportVissza.Click += new System.EventHandler(this.CsoportVissza_Click);
            // 
            // Csoportkijelöltmind
            // 
            this.Csoportkijelöltmind.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.Csoportkijelöltmind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Csoportkijelöltmind.Location = new System.Drawing.Point(545, 5);
            this.Csoportkijelöltmind.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Csoportkijelöltmind.Name = "Csoportkijelöltmind";
            this.Csoportkijelöltmind.Size = new System.Drawing.Size(40, 40);
            this.Csoportkijelöltmind.TabIndex = 133;
            this.ToolTip1.SetToolTip(this.Csoportkijelöltmind, "Mindent kijelöl");
            this.Csoportkijelöltmind.UseVisualStyleBackColor = true;
            this.Csoportkijelöltmind.Click += new System.EventHandler(this.Csoportkijelöltmind_Click);
            // 
            // Helyiséglista
            // 
            this.Helyiséglista.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Helyiséglista.CheckOnClick = true;
            this.Helyiséglista.FormattingEnabled = true;
            this.Helyiséglista.Location = new System.Drawing.Point(5, 50);
            this.Helyiséglista.Name = "Helyiséglista";
            this.Helyiséglista.Size = new System.Drawing.Size(412, 466);
            this.Helyiséglista.TabIndex = 132;
            this.Helyiséglista.Click += new System.EventHandler(this.Helyiséglista_Click);
            // 
            // Csuk
            // 
            this.Csuk.BackgroundImage = global::Villamos.Properties.Resources.fel;
            this.Csuk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Csuk.Location = new System.Drawing.Point(465, 5);
            this.Csuk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Csuk.Name = "Csuk";
            this.Csuk.Size = new System.Drawing.Size(40, 40);
            this.Csuk.TabIndex = 130;
            this.Csuk.UseVisualStyleBackColor = true;
            this.Csuk.Click += new System.EventHandler(this.Csuk_Click);
            // 
            // Nyit
            // 
            this.Nyit.BackgroundImage = global::Villamos.Properties.Resources.le;
            this.Nyit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Nyit.Location = new System.Drawing.Point(425, 5);
            this.Nyit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Nyit.Name = "Nyit";
            this.Nyit.Size = new System.Drawing.Size(40, 40);
            this.Nyit.TabIndex = 129;
            this.Nyit.UseVisualStyleBackColor = true;
            this.Nyit.Click += new System.EventHandler(this.Nyit_Click);
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.SlateBlue;
            this.TabPage2.Controls.Add(this.BMR);
            this.TabPage2.Controls.Add(this.Opció_kifizetés);
            this.TabPage2.Controls.Add(this.Opció_Megrendelés);
            this.TabPage2.Controls.Add(this.List1);
            this.TabPage2.Controls.Add(this.Tábla1);
            this.TabPage2.Controls.Add(this.Dátum1);
            this.TabPage2.Controls.Add(this.Zárva1);
            this.TabPage2.Controls.Add(this.Mentés);
            this.TabPage2.Controls.Add(this.Nyitva1);
            this.TabPage2.Controls.Add(this.Command9);
            this.TabPage2.Controls.Add(this.Le1);
            this.TabPage2.Controls.Add(this.Command2);
            this.TabPage2.Controls.Add(this.Command10);
            this.TabPage2.Controls.Add(this.Fel1);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(1131, 529);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Elkészült Takarítás Rögzítése";
            // 
            // BMR
            // 
            this.BMR.BackgroundImage = global::Villamos.Properties.Resources.App_spreadsheet1;
            this.BMR.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BMR.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BMR.Location = new System.Drawing.Point(868, 5);
            this.BMR.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BMR.Name = "BMR";
            this.BMR.Size = new System.Drawing.Size(45, 45);
            this.BMR.TabIndex = 195;
            this.BMR.Text = "BMR";
            this.BMR.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ToolTip1.SetToolTip(this.BMR, "Takarítási Terv-Tény-Eltérés");
            this.BMR.UseVisualStyleBackColor = true;
            this.BMR.Click += new System.EventHandler(this.BMR_Click);
            // 
            // Opció_kifizetés
            // 
            this.Opció_kifizetés.BackgroundImage = global::Villamos.Properties.Resources.Calc;
            this.Opció_kifizetés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Opció_kifizetés.Location = new System.Drawing.Point(999, 4);
            this.Opció_kifizetés.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Opció_kifizetés.Name = "Opció_kifizetés";
            this.Opció_kifizetés.Size = new System.Drawing.Size(45, 45);
            this.Opció_kifizetés.TabIndex = 194;
            this.ToolTip1.SetToolTip(this.Opció_kifizetés, "Opció kifizetés");
            this.Opció_kifizetés.UseVisualStyleBackColor = true;
            this.Opció_kifizetés.Click += new System.EventHandler(this.Opció_kifizetés_Click);
            // 
            // Opció_Megrendelés
            // 
            this.Opció_Megrendelés.BackgroundImage = global::Villamos.Properties.Resources.shopping_cart;
            this.Opció_Megrendelés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Opció_Megrendelés.Location = new System.Drawing.Point(948, 4);
            this.Opció_Megrendelés.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Opció_Megrendelés.Name = "Opció_Megrendelés";
            this.Opció_Megrendelés.Size = new System.Drawing.Size(45, 45);
            this.Opció_Megrendelés.TabIndex = 193;
            this.ToolTip1.SetToolTip(this.Opció_Megrendelés, "Opciós megrendelés");
            this.Opció_Megrendelés.UseVisualStyleBackColor = true;
            this.Opció_Megrendelés.Click += new System.EventHandler(this.Opció_Megrendelés_Click);
            // 
            // List1
            // 
            this.List1.CheckOnClick = true;
            this.List1.FormattingEnabled = true;
            this.List1.Location = new System.Drawing.Point(133, 19);
            this.List1.Name = "List1";
            this.List1.Size = new System.Drawing.Size(412, 25);
            this.List1.TabIndex = 126;
            // 
            // Tábla1
            // 
            this.Tábla1.AllowUserToAddRows = false;
            this.Tábla1.AllowUserToDeleteRows = false;
            this.Tábla1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla1.Location = new System.Drawing.Point(3, 55);
            this.Tábla1.Name = "Tábla1";
            this.Tábla1.Size = new System.Drawing.Size(1125, 471);
            this.Tábla1.TabIndex = 191;
            this.Tábla1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Tábla1_CellFormatting);
            this.Tábla1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.Tábla1_EditingControlShowing);
            // 
            // Dátum1
            // 
            this.Dátum1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum1.Location = new System.Drawing.Point(5, 18);
            this.Dátum1.Name = "Dátum1";
            this.Dátum1.Size = new System.Drawing.Size(122, 26);
            this.Dátum1.TabIndex = 3;
            this.Dátum1.ValueChanged += new System.EventHandler(this.Dátum1_ValueChanged);
            // 
            // Zárva1
            // 
            this.Zárva1.BackgroundImage = global::Villamos.Properties.Resources.Lakatzárva32;
            this.Zárva1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Zárva1.Location = new System.Drawing.Point(676, 5);
            this.Zárva1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Zárva1.Name = "Zárva1";
            this.Zárva1.Size = new System.Drawing.Size(45, 45);
            this.Zárva1.TabIndex = 190;
            this.ToolTip1.SetToolTip(this.Zárva1, "Rögzítési lehetőség zárva, rögzítési lehetőséget kinyitja");
            this.Zárva1.UseVisualStyleBackColor = true;
            this.Zárva1.Click += new System.EventHandler(this.Zárva1_Click);
            // 
            // Mentés
            // 
            this.Mentés.BackgroundImage = global::Villamos.Properties.Resources.mentés32;
            this.Mentés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Mentés.Location = new System.Drawing.Point(1080, 5);
            this.Mentés.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Mentés.Name = "Mentés";
            this.Mentés.Size = new System.Drawing.Size(45, 45);
            this.Mentés.TabIndex = 188;
            this.Mentés.UseVisualStyleBackColor = true;
            this.Mentés.Click += new System.EventHandler(this.Mentés_Click);
            // 
            // Nyitva1
            // 
            this.Nyitva1.BackgroundImage = global::Villamos.Properties.Resources.lakatnyitva32;
            this.Nyitva1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Nyitva1.Location = new System.Drawing.Point(676, 4);
            this.Nyitva1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Nyitva1.Name = "Nyitva1";
            this.Nyitva1.Size = new System.Drawing.Size(45, 45);
            this.Nyitva1.TabIndex = 187;
            this.ToolTip1.SetToolTip(this.Nyitva1, "Rögzítési lehetőség nyitva; rögzítési lehetőséget lezárja");
            this.Nyitva1.UseVisualStyleBackColor = true;
            this.Nyitva1.Click += new System.EventHandler(this.Nyitva1_Click);
            // 
            // Command9
            // 
            this.Command9.BackgroundImage = global::Villamos.Properties.Resources.Aha_Soft_Large_Seo_SEO;
            this.Command9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command9.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F);
            this.Command9.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Command9.Location = new System.Drawing.Point(794, 5);
            this.Command9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Command9.Name = "Command9";
            this.Command9.Size = new System.Drawing.Size(45, 45);
            this.Command9.TabIndex = 185;
            this.Command9.Text = "TIG";
            this.Command9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ToolTip1.SetToolTip(this.Command9, "Takarítási teljesítési igazolás készítés");
            this.Command9.UseVisualStyleBackColor = true;
            this.Command9.Click += new System.EventHandler(this.Command9_Click);
            // 
            // Le1
            // 
            this.Le1.BackgroundImage = global::Villamos.Properties.Resources.fel;
            this.Le1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Le1.Location = new System.Drawing.Point(551, 5);
            this.Le1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Le1.Name = "Le1";
            this.Le1.Size = new System.Drawing.Size(45, 45);
            this.Le1.TabIndex = 127;
            this.Le1.UseVisualStyleBackColor = true;
            this.Le1.Click += new System.EventHandler(this.Le1_Click);
            // 
            // Command2
            // 
            this.Command2.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Command2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command2.Location = new System.Drawing.Point(617, 4);
            this.Command2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Command2.Name = "Command2";
            this.Command2.Size = new System.Drawing.Size(45, 45);
            this.Command2.TabIndex = 189;
            this.Command2.UseVisualStyleBackColor = true;
            this.Command2.Click += new System.EventHandler(this.Command2_Click);
            // 
            // Command10
            // 
            this.Command10.BackgroundImage = global::Villamos.Properties.Resources.App_dict;
            this.Command10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command10.Location = new System.Drawing.Point(743, 5);
            this.Command10.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Command10.Name = "Command10";
            this.Command10.Size = new System.Drawing.Size(45, 45);
            this.Command10.TabIndex = 186;
            this.ToolTip1.SetToolTip(this.Command10, "Takarítási Terv-Tény-Eltérés");
            this.Command10.UseVisualStyleBackColor = true;
            this.Command10.Click += new System.EventHandler(this.Command10_Click);
            // 
            // Fel1
            // 
            this.Fel1.BackgroundImage = global::Villamos.Properties.Resources.le;
            this.Fel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Fel1.Location = new System.Drawing.Point(551, 5);
            this.Fel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Fel1.Name = "Fel1";
            this.Fel1.Size = new System.Drawing.Size(45, 45);
            this.Fel1.TabIndex = 128;
            this.Fel1.UseVisualStyleBackColor = true;
            this.Fel1.Visible = false;
            this.Fel1.Click += new System.EventHandler(this.Fel1_Click);
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.LightSkyBlue;
            this.TabPage3.Controls.Add(this.Táblázat_frissítése);
            this.TabPage3.Controls.Add(this.Naptár_Tábla);
            this.TabPage3.Controls.Add(this.Dátum2);
            this.TabPage3.Controls.Add(this.Alap_Rögzít);
            this.TabPage3.Location = new System.Drawing.Point(4, 29);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage3.Size = new System.Drawing.Size(1131, 529);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Naptár";
            // 
            // Táblázat_frissítése
            // 
            this.Táblázat_frissítése.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Táblázat_frissítése.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Táblázat_frissítése.Location = new System.Drawing.Point(134, 3);
            this.Táblázat_frissítése.Name = "Táblázat_frissítése";
            this.Táblázat_frissítése.Size = new System.Drawing.Size(45, 45);
            this.Táblázat_frissítése.TabIndex = 193;
            this.Táblázat_frissítése.UseVisualStyleBackColor = true;
            this.Táblázat_frissítése.Click += new System.EventHandler(this.Táblázat_frissítése_Click);
            // 
            // Naptár_Tábla
            // 
            this.Naptár_Tábla.AllowUserToAddRows = false;
            this.Naptár_Tábla.AllowUserToDeleteRows = false;
            this.Naptár_Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Naptár_Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Naptár_Tábla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Munkanap,
            this.Hétvége});
            this.Naptár_Tábla.Location = new System.Drawing.Point(3, 50);
            this.Naptár_Tábla.Name = "Naptár_Tábla";
            this.Naptár_Tábla.RowHeadersVisible = false;
            this.Naptár_Tábla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Naptár_Tábla.Size = new System.Drawing.Size(1041, 473);
            this.Naptár_Tábla.TabIndex = 192;
            this.Naptár_Tábla.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Naptár_Tábla_CellFormatting);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Nap";
            this.Column1.Name = "Column1";
            // 
            // Munkanap
            // 
            this.Munkanap.HeaderText = "Munkanap";
            this.Munkanap.Name = "Munkanap";
            this.Munkanap.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Munkanap.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Hétvége
            // 
            this.Hétvége.HeaderText = "Hétvége";
            this.Hétvége.Name = "Hétvége";
            // 
            // Dátum2
            // 
            this.Dátum2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum2.Location = new System.Drawing.Point(6, 20);
            this.Dátum2.Name = "Dátum2";
            this.Dátum2.Size = new System.Drawing.Size(122, 26);
            this.Dátum2.TabIndex = 2;
            this.Dátum2.ValueChanged += new System.EventHandler(this.Dátum2_ValueChanged);
            // 
            // Alap_Rögzít
            // 
            this.Alap_Rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Alap_Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Alap_Rögzít.Location = new System.Drawing.Point(198, 3);
            this.Alap_Rögzít.Name = "Alap_Rögzít";
            this.Alap_Rögzít.Size = new System.Drawing.Size(45, 45);
            this.Alap_Rögzít.TabIndex = 67;
            this.Alap_Rögzít.UseVisualStyleBackColor = true;
            this.Alap_Rögzít.Click += new System.EventHandler(this.Alap_Rögzít_Click);
            // 
            // ToolTip1
            // 
            this.ToolTip1.IsBalloon = true;
            // 
            // Chk_CTRL
            // 
            this.Chk_CTRL.AutoSize = true;
            this.Chk_CTRL.Location = new System.Drawing.Point(598, 26);
            this.Chk_CTRL.Name = "Chk_CTRL";
            this.Chk_CTRL.Size = new System.Drawing.Size(127, 24);
            this.Chk_CTRL.TabIndex = 177;
            this.Chk_CTRL.Text = "CTRL nyomva";
            this.Chk_CTRL.UseVisualStyleBackColor = true;
            this.Chk_CTRL.Visible = false;
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1109, 5);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 174;
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Ablak_Épülettakarítás
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Goldenrod;
            this.ClientSize = new System.Drawing.Size(1157, 622);
            this.Controls.Add(this.Chk_CTRL);
            this.Controls.Add(this.LapFülek);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.Panel2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Épülettakarítás";
            this.Text = "Épülettakarítás";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_Épülettakarítás_FormClosed);
            this.Load += new System.EventHandler(this.Ablak_Épülettakarítás_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ablak_Épülettakarítás_KeyDown);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.LapFülek.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_terv)).EndInit();
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.TabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla1)).EndInit();
            this.TabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Naptár_Tábla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal Button BtnSúgó;
        internal Panel Panel2;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal TabControl LapFülek;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal TabPage TabPage3;
        internal DateTimePicker Dátum2;
        internal Button Alap_Rögzít;
        internal DateTimePicker Dátum1;
        internal CheckedListBox List1;
        internal Button Fel1;
        internal Button Le1;
        internal Button Command9;
        internal Button Zárva1;
        internal Button Command2;
        internal Button Mentés;
        internal Button Nyitva1;
        internal Button Command10;
        internal DataGridView Tábla1;
        internal Button Táblázat_frissítése;
        internal DataGridView Naptár_Tábla;
        internal CheckedListBox Helyiséglista;
        internal CheckedListBox Osztálylista;
        internal Button Csuk;
        internal Button Nyit;
        internal Button Mindtöröl;
        internal Button ÖsszesKijelöl;
        internal Button Jelöltcsoport;
        internal Button CsoportVissza;
        internal Button Csoportkijelöltmind;
        internal Button Command14;
        internal Button E1MindenNap;
        internal Button E2MindenNap;
        internal Button E3MindenNap;
        internal Button E1Munkanap;
        internal Button E2Munkanap;
        internal Button E3Munkanap;
        internal DateTimePicker Dátum;
        internal Button Zárva;
        internal Button Nyitva;
        internal Button Terv_Rögzítés;
        internal Button KapcsoltHelységFő;
        internal Button KapcsoltHelységAl;
        internal Button Szemetes;
        internal Button Excellekérdezés;
        internal Button Command4;
        internal Panel Panel3;
        internal Label Label2;
        internal RadioButton Option10;
        internal RadioButton Option11;
        internal Panel Panel1;
        internal Label Label1;
        internal RadioButton Option9;
        internal RadioButton Option8;
        internal DataGridView Tábla_terv;
        internal Button E2Minden_töröl;
        internal Button E3Minden_töröl;
        internal Button E1Minden_töröl;
        internal Button Helység_friss;
        internal ToolTip ToolTip1;
        internal DataGridViewTextBoxColumn DataGridViewTextBoxColumn1;
        internal DataGridViewCheckBoxColumn E1;
        internal DataGridViewCheckBoxColumn E2;
        internal DataGridViewCheckBoxColumn E3;
        internal DataGridViewCheckBoxColumn Nap;
        internal DataGridViewTextBoxColumn Column1;
        internal DataGridViewCheckBoxColumn Munkanap;
        internal DataGridViewCheckBoxColumn Hétvége;
        internal CheckBox Chk_CTRL;
        internal Panel Idő_lakat;
        internal Button Opció_kifizetés;
        internal Button Opció_Megrendelés;
        internal Button BMR;
        private Timer timer1;
    }
}