using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
  
    public partial class Ablak_reklám : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components!= null)
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_reklám));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Lapfülek = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.Utasítás = new System.Windows.Forms.Button();
            this.Beilleszt = new System.Windows.Forms.Button();
            this.Másol = new System.Windows.Forms.Button();
            this.Törlés = new System.Windows.Forms.Button();
            this.Rögzít = new System.Windows.Forms.Button();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Label11 = new System.Windows.Forms.Label();
            this.Command3 = new System.Windows.Forms.Button();
            this.Ragaszt = new System.Windows.Forms.DateTimePicker();
            this.Listáz = new System.Windows.Forms.Button();
            this.Méret = new System.Windows.Forms.ComboBox();
            this.Rekezd = new System.Windows.Forms.DateTimePicker();
            this.Revég = new System.Windows.Forms.DateTimePicker();
            this.CheckBox1 = new System.Windows.Forms.CheckBox();
            this.Telephely = new System.Windows.Forms.TextBox();
            this.Típus = new System.Windows.Forms.TextBox();
            this.Reklám = new System.Windows.Forms.TextBox();
            this.Vonal = new System.Windows.Forms.TextBox();
            this.Megjegyzés = new System.Windows.Forms.TextBox();
            this.Szerelvény = new System.Windows.Forms.TextBox();
            this.Pályaszám = new System.Windows.Forms.TextBox();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.Telephely_Semmi = new System.Windows.Forms.Button();
            this.Telephely_Mind = new System.Windows.Forms.Button();
            this.Típus_Semmi = new System.Windows.Forms.Button();
            this.Típus_Mind = new System.Windows.Forms.Button();
            this.Reklám_Semmi = new System.Windows.Forms.Button();
            this.Reklám_Mind = new System.Windows.Forms.Button();
            this.TelephelyList = new System.Windows.Forms.CheckedListBox();
            this.Excellekérdezés = new System.Windows.Forms.Button();
            this.Button3 = new System.Windows.Forms.Button();
            this.Reklámnevelista = new System.Windows.Forms.CheckedListBox();
            this.Típuslista = new System.Windows.Forms.CheckedListBox();
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.ListPályaszám = new System.Windows.Forms.ComboBox();
            this.Naplótól = new System.Windows.Forms.DateTimePicker();
            this.Naplóig = new System.Windows.Forms.DateTimePicker();
            this.TáblaNapló = new System.Windows.Forms.DataGridView();
            this.Command6 = new System.Windows.Forms.Button();
            this.Command5 = new System.Windows.Forms.Button();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Button13 = new System.Windows.Forms.Button();
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.Panel1.SuspendLayout();
            this.Lapfülek.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.TabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TáblaNapló)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.Cmbtelephely);
            this.Panel1.Controls.Add(this.Label13);
            this.Panel1.Location = new System.Drawing.Point(3, 12);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(373, 33);
            this.Panel1.TabIndex = 64;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(175, 2);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
            this.Cmbtelephely.SelectedIndexChanged += new System.EventHandler(this.Cmbtelephely_SelectedIndexChanged);
            this.Cmbtelephely.SelectionChangeCommitted += new System.EventHandler(this.Cmbtelephely_SelectionChangeCommitted);
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
            // Lapfülek
            // 
            this.Lapfülek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Lapfülek.Controls.Add(this.TabPage1);
            this.Lapfülek.Controls.Add(this.TabPage2);
            this.Lapfülek.Controls.Add(this.TabPage3);
            this.Lapfülek.Location = new System.Drawing.Point(5, 55);
            this.Lapfülek.Name = "Lapfülek";
            this.Lapfülek.Padding = new System.Drawing.Point(16, 3);
            this.Lapfülek.SelectedIndex = 0;
            this.Lapfülek.Size = new System.Drawing.Size(1191, 412);
            this.Lapfülek.TabIndex = 67;
            this.Lapfülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Lapfülek_DrawItem);
            this.Lapfülek.SelectedIndexChanged += new System.EventHandler(this.LAPFülek_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.Turquoise;
            this.TabPage1.Controls.Add(this.Utasítás);
            this.TabPage1.Controls.Add(this.Beilleszt);
            this.TabPage1.Controls.Add(this.Másol);
            this.TabPage1.Controls.Add(this.Törlés);
            this.TabPage1.Controls.Add(this.Rögzít);
            this.TabPage1.Controls.Add(this.Panel2);
            this.TabPage1.Controls.Add(this.Listáz);
            this.TabPage1.Controls.Add(this.Méret);
            this.TabPage1.Controls.Add(this.Rekezd);
            this.TabPage1.Controls.Add(this.Revég);
            this.TabPage1.Controls.Add(this.CheckBox1);
            this.TabPage1.Controls.Add(this.Telephely);
            this.TabPage1.Controls.Add(this.Típus);
            this.TabPage1.Controls.Add(this.Reklám);
            this.TabPage1.Controls.Add(this.Vonal);
            this.TabPage1.Controls.Add(this.Megjegyzés);
            this.TabPage1.Controls.Add(this.Szerelvény);
            this.TabPage1.Controls.Add(this.Pályaszám);
            this.TabPage1.Controls.Add(this.Label10);
            this.TabPage1.Controls.Add(this.Label9);
            this.TabPage1.Controls.Add(this.Label8);
            this.TabPage1.Controls.Add(this.Label7);
            this.TabPage1.Controls.Add(this.Label6);
            this.TabPage1.Controls.Add(this.Label5);
            this.TabPage1.Controls.Add(this.Label4);
            this.TabPage1.Controls.Add(this.Label3);
            this.TabPage1.Controls.Add(this.Label2);
            this.TabPage1.Controls.Add(this.Label1);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1183, 379);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Rögzítés";
            // 
            // Utasítás
            // 
            this.Utasítás.BackgroundImage = global::Villamos.Properties.Resources.Document_write;
            this.Utasítás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Utasítás.Location = new System.Drawing.Point(619, 32);
            this.Utasítás.Name = "Utasítás";
            this.Utasítás.Size = new System.Drawing.Size(40, 40);
            this.Utasítás.TabIndex = 123;
            this.ToolTip1.SetToolTip(this.Utasítás, "Reklám adatok az utasításban való rögzítése");
            this.Utasítás.UseVisualStyleBackColor = true;
            this.Utasítás.Click += new System.EventHandler(this.Utasítás_Click);
            // 
            // Beilleszt
            // 
            this.Beilleszt.BackColor = System.Drawing.Color.DarkTurquoise;
            this.Beilleszt.BackgroundImage = global::Villamos.Properties.Resources.Clipboard_Paste_01;
            this.Beilleszt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Beilleszt.Location = new System.Drawing.Point(670, 211);
            this.Beilleszt.Name = "Beilleszt";
            this.Beilleszt.Size = new System.Drawing.Size(45, 45);
            this.Beilleszt.TabIndex = 13;
            this.ToolTip1.SetToolTip(this.Beilleszt, "Beilleszt");
            this.Beilleszt.UseVisualStyleBackColor = false;
            this.Beilleszt.Click += new System.EventHandler(this.Beilleszt_Click);
            // 
            // Másol
            // 
            this.Másol.BackColor = System.Drawing.Color.DarkTurquoise;
            this.Másol.BackgroundImage = global::Villamos.Properties.Resources.Document_Copy_01;
            this.Másol.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Másol.Location = new System.Drawing.Point(619, 211);
            this.Másol.Name = "Másol";
            this.Másol.Size = new System.Drawing.Size(45, 45);
            this.Másol.TabIndex = 12;
            this.ToolTip1.SetToolTip(this.Másol, "Másol");
            this.Másol.UseVisualStyleBackColor = false;
            this.Másol.Click += new System.EventHandler(this.Másol_Click);
            // 
            // Törlés
            // 
            this.Törlés.BackColor = System.Drawing.Color.Red;
            this.Törlés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Törlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Törlés.Location = new System.Drawing.Point(619, 305);
            this.Törlés.Name = "Törlés";
            this.Törlés.Size = new System.Drawing.Size(45, 45);
            this.Törlés.TabIndex = 11;
            this.ToolTip1.SetToolTip(this.Törlés, "Törli a reklám összes adatát.");
            this.Törlés.UseVisualStyleBackColor = false;
            this.Törlés.Click += new System.EventHandler(this.Törlés_Click);
            // 
            // Rögzít
            // 
            this.Rögzít.BackColor = System.Drawing.Color.Green;
            this.Rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Rögzít.Location = new System.Drawing.Point(670, 305);
            this.Rögzít.Name = "Rögzít";
            this.Rögzít.Size = new System.Drawing.Size(45, 45);
            this.Rögzít.TabIndex = 10;
            this.ToolTip1.SetToolTip(this.Rögzít, "Rögzíti/módosítja az adatokat");
            this.Rögzít.UseVisualStyleBackColor = false;
            this.Rögzít.Click += new System.EventHandler(this.Rögzít_Click);
            // 
            // Panel2
            // 
            this.Panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel2.BackColor = System.Drawing.Color.Green;
            this.Panel2.Controls.Add(this.Label11);
            this.Panel2.Controls.Add(this.Command3);
            this.Panel2.Controls.Add(this.Ragaszt);
            this.Panel2.Location = new System.Drawing.Point(966, 6);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(211, 75);
            this.Panel2.TabIndex = 122;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(5, 5);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(142, 20);
            this.Label11.TabIndex = 121;
            this.Label11.Text = "Ragasztási tilalom:";
            // 
            // Command3
            // 
            this.Command3.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Command3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command3.Location = new System.Drawing.Point(163, 26);
            this.Command3.Name = "Command3";
            this.Command3.Size = new System.Drawing.Size(40, 40);
            this.Command3.TabIndex = 1;
            this.ToolTip1.SetToolTip(this.Command3, "Rögziti a ragasztási tilalom végét");
            this.Command3.UseVisualStyleBackColor = true;
            this.Command3.Click += new System.EventHandler(this.Command3_Click);
            // 
            // Ragaszt
            // 
            this.Ragaszt.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Ragaszt.Location = new System.Drawing.Point(9, 40);
            this.Ragaszt.Name = "Ragaszt";
            this.Ragaszt.Size = new System.Drawing.Size(105, 26);
            this.Ragaszt.TabIndex = 0;
            // 
            // Listáz
            // 
            this.Listáz.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Listáz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Listáz.Location = new System.Drawing.Point(157, 29);
            this.Listáz.Name = "Listáz";
            this.Listáz.Size = new System.Drawing.Size(45, 45);
            this.Listáz.TabIndex = 121;
            this.ToolTip1.SetToolTip(this.Listáz, "listázza a pályaszámhoz tartozó adatokat");
            this.Listáz.UseVisualStyleBackColor = true;
            this.Listáz.Click += new System.EventHandler(this.Listáz_Click);
            // 
            // Méret
            // 
            this.Méret.FormattingEnabled = true;
            this.Méret.Location = new System.Drawing.Point(168, 196);
            this.Méret.Name = "Méret";
            this.Méret.Size = new System.Drawing.Size(186, 28);
            this.Méret.TabIndex = 6;
            // 
            // Rekezd
            // 
            this.Rekezd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Rekezd.Location = new System.Drawing.Point(168, 134);
            this.Rekezd.Name = "Rekezd";
            this.Rekezd.Size = new System.Drawing.Size(105, 26);
            this.Rekezd.TabIndex = 4;
            // 
            // Revég
            // 
            this.Revég.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Revég.Location = new System.Drawing.Point(168, 164);
            this.Revég.Name = "Revég";
            this.Revég.Size = new System.Drawing.Size(105, 26);
            this.Revég.TabIndex = 5;
            // 
            // CheckBox1
            // 
            this.CheckBox1.AutoSize = true;
            this.CheckBox1.Location = new System.Drawing.Point(169, 294);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(132, 24);
            this.CheckBox1.TabIndex = 18;
            this.CheckBox1.Text = "Szerelvényben";
            this.CheckBox1.UseVisualStyleBackColor = true;
            // 
            // Telephely
            // 
            this.Telephely.Enabled = false;
            this.Telephely.Location = new System.Drawing.Point(229, 48);
            this.Telephely.Name = "Telephely";
            this.Telephely.Size = new System.Drawing.Size(157, 26);
            this.Telephely.TabIndex = 1;
            // 
            // Típus
            // 
            this.Típus.Enabled = false;
            this.Típus.Location = new System.Drawing.Point(392, 48);
            this.Típus.Name = "Típus";
            this.Típus.Size = new System.Drawing.Size(100, 26);
            this.Típus.TabIndex = 2;
            // 
            // Reklám
            // 
            this.Reklám.Location = new System.Drawing.Point(169, 102);
            this.Reklám.Name = "Reklám";
            this.Reklám.Size = new System.Drawing.Size(402, 26);
            this.Reklám.TabIndex = 3;
            // 
            // Vonal
            // 
            this.Vonal.Location = new System.Drawing.Point(169, 230);
            this.Vonal.Name = "Vonal";
            this.Vonal.Size = new System.Drawing.Size(100, 26);
            this.Vonal.TabIndex = 7;
            // 
            // Megjegyzés
            // 
            this.Megjegyzés.Location = new System.Drawing.Point(169, 262);
            this.Megjegyzés.Name = "Megjegyzés";
            this.Megjegyzés.Size = new System.Drawing.Size(402, 26);
            this.Megjegyzés.TabIndex = 8;
            // 
            // Szerelvény
            // 
            this.Szerelvény.Location = new System.Drawing.Point(169, 324);
            this.Szerelvény.Name = "Szerelvény";
            this.Szerelvény.Size = new System.Drawing.Size(217, 26);
            this.Szerelvény.TabIndex = 9;
            // 
            // Pályaszám
            // 
            this.Pályaszám.Location = new System.Drawing.Point(35, 48);
            this.Pályaszám.Name = "Pályaszám";
            this.Pályaszám.Size = new System.Drawing.Size(100, 26);
            this.Pályaszám.TabIndex = 0;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(225, 25);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(76, 20);
            this.Label10.TabIndex = 9;
            this.Label10.Text = "Telephely";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(388, 25);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(47, 20);
            this.Label9.TabIndex = 8;
            this.Label9.Text = "Típus";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(31, 108);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(105, 20);
            this.Label8.TabIndex = 7;
            this.Label8.Text = "Reklám neve:";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(31, 140);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(128, 20);
            this.Label7.TabIndex = 6;
            this.Label7.Text = "Reklám kezdete:";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(31, 170);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(105, 20);
            this.Label6.TabIndex = 5;
            this.Label6.Text = "Reklám vége:";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(31, 204);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(121, 20);
            this.Label5.TabIndex = 4;
            this.Label5.Text = "Reklám mérete:";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(31, 236);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(54, 20);
            this.Label4.TabIndex = 3;
            this.Label4.Text = "Vonal:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(31, 268);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(97, 20);
            this.Label3.TabIndex = 2;
            this.Label3.Text = "Megjegyzés:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(31, 330);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(132, 20);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "Szerelvény szám:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(31, 25);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(85, 20);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Pályaszám";
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.SystemColors.Highlight;
            this.TabPage2.Controls.Add(this.Telephely_Semmi);
            this.TabPage2.Controls.Add(this.Telephely_Mind);
            this.TabPage2.Controls.Add(this.Típus_Semmi);
            this.TabPage2.Controls.Add(this.Típus_Mind);
            this.TabPage2.Controls.Add(this.Reklám_Semmi);
            this.TabPage2.Controls.Add(this.Reklám_Mind);
            this.TabPage2.Controls.Add(this.TelephelyList);
            this.TabPage2.Controls.Add(this.Excellekérdezés);
            this.TabPage2.Controls.Add(this.Button3);
            this.TabPage2.Controls.Add(this.Reklámnevelista);
            this.TabPage2.Controls.Add(this.Típuslista);
            this.TabPage2.Controls.Add(this.Tábla);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(1183, 379);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Áttekintés";
            // 
            // Telephely_Semmi
            // 
            this.Telephely_Semmi.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.Telephely_Semmi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Telephely_Semmi.Location = new System.Drawing.Point(1009, 6);
            this.Telephely_Semmi.Name = "Telephely_Semmi";
            this.Telephely_Semmi.Size = new System.Drawing.Size(45, 45);
            this.Telephely_Semmi.TabIndex = 144;
            this.ToolTip1.SetToolTip(this.Telephely_Semmi, "Frissíti a táblázatot");
            this.Telephely_Semmi.UseVisualStyleBackColor = true;
            this.Telephely_Semmi.Click += new System.EventHandler(this.Telephely_Semmi_Click);
            // 
            // Telephely_Mind
            // 
            this.Telephely_Mind.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.Telephely_Mind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Telephely_Mind.Location = new System.Drawing.Point(958, 6);
            this.Telephely_Mind.Name = "Telephely_Mind";
            this.Telephely_Mind.Size = new System.Drawing.Size(45, 45);
            this.Telephely_Mind.TabIndex = 143;
            this.ToolTip1.SetToolTip(this.Telephely_Mind, "Frissíti a táblázatot");
            this.Telephely_Mind.UseVisualStyleBackColor = true;
            this.Telephely_Mind.Click += new System.EventHandler(this.Telephely_Mind_Click);
            // 
            // Típus_Semmi
            // 
            this.Típus_Semmi.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.Típus_Semmi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Típus_Semmi.Location = new System.Drawing.Point(651, 6);
            this.Típus_Semmi.Name = "Típus_Semmi";
            this.Típus_Semmi.Size = new System.Drawing.Size(45, 45);
            this.Típus_Semmi.TabIndex = 142;
            this.ToolTip1.SetToolTip(this.Típus_Semmi, "Frissíti a táblázatot");
            this.Típus_Semmi.UseVisualStyleBackColor = true;
            this.Típus_Semmi.Click += new System.EventHandler(this.Típus_Semmi_Click);
            // 
            // Típus_Mind
            // 
            this.Típus_Mind.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.Típus_Mind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Típus_Mind.Location = new System.Drawing.Point(600, 6);
            this.Típus_Mind.Name = "Típus_Mind";
            this.Típus_Mind.Size = new System.Drawing.Size(45, 45);
            this.Típus_Mind.TabIndex = 141;
            this.ToolTip1.SetToolTip(this.Típus_Mind, "Frissíti a táblázatot");
            this.Típus_Mind.UseVisualStyleBackColor = true;
            this.Típus_Mind.Click += new System.EventHandler(this.Típus_Mind_Click);
            // 
            // Reklám_Semmi
            // 
            this.Reklám_Semmi.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.Reklám_Semmi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Reklám_Semmi.Location = new System.Drawing.Point(395, 6);
            this.Reklám_Semmi.Name = "Reklám_Semmi";
            this.Reklám_Semmi.Size = new System.Drawing.Size(45, 45);
            this.Reklám_Semmi.TabIndex = 140;
            this.ToolTip1.SetToolTip(this.Reklám_Semmi, "Frissíti a táblázatot");
            this.Reklám_Semmi.UseVisualStyleBackColor = true;
            this.Reklám_Semmi.Click += new System.EventHandler(this.Reklám_Semmi_Click);
            // 
            // Reklám_Mind
            // 
            this.Reklám_Mind.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.Reklám_Mind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Reklám_Mind.Location = new System.Drawing.Point(344, 6);
            this.Reklám_Mind.Name = "Reklám_Mind";
            this.Reklám_Mind.Size = new System.Drawing.Size(45, 45);
            this.Reklám_Mind.TabIndex = 139;
            this.ToolTip1.SetToolTip(this.Reklám_Mind, "Frissíti a táblázatot");
            this.Reklám_Mind.UseVisualStyleBackColor = true;
            this.Reklám_Mind.Click += new System.EventHandler(this.Reklám_Mind_Click);
            // 
            // TelephelyList
            // 
            this.TelephelyList.CheckOnClick = true;
            this.TelephelyList.FormattingEnabled = true;
            this.TelephelyList.Location = new System.Drawing.Point(702, 26);
            this.TelephelyList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TelephelyList.Name = "TelephelyList";
            this.TelephelyList.Size = new System.Drawing.Size(250, 25);
            this.TelephelyList.TabIndex = 138;
            this.TelephelyList.MouseEnter += new System.EventHandler(this.TelephelyList_MouseEnter);
            this.TelephelyList.MouseLeave += new System.EventHandler(this.TelephelyList_MouseLeave);
            this.TelephelyList.MouseHover += new System.EventHandler(this.TelephelyList_MouseHover);
            // 
            // Excellekérdezés
            // 
            this.Excellekérdezés.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excellekérdezés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excellekérdezés.Location = new System.Drawing.Point(1128, 6);
            this.Excellekérdezés.Name = "Excellekérdezés";
            this.Excellekérdezés.Size = new System.Drawing.Size(45, 45);
            this.Excellekérdezés.TabIndex = 137;
            this.ToolTip1.SetToolTip(this.Excellekérdezés, "Excel táblázatot készít a táblázat adataiból");
            this.Excellekérdezés.UseVisualStyleBackColor = true;
            this.Excellekérdezés.Click += new System.EventHandler(this.Excellekérdezés_Click);
            // 
            // Button3
            // 
            this.Button3.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button3.Location = new System.Drawing.Point(1077, 6);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(45, 45);
            this.Button3.TabIndex = 136;
            this.ToolTip1.SetToolTip(this.Button3, "Frissíti a táblázatot");
            this.Button3.UseVisualStyleBackColor = true;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Reklámnevelista
            // 
            this.Reklámnevelista.CheckOnClick = true;
            this.Reklámnevelista.FormattingEnabled = true;
            this.Reklámnevelista.Location = new System.Drawing.Point(10, 26);
            this.Reklámnevelista.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Reklámnevelista.Name = "Reklámnevelista";
            this.Reklámnevelista.Size = new System.Drawing.Size(328, 25);
            this.Reklámnevelista.TabIndex = 132;
            this.Reklámnevelista.MouseEnter += new System.EventHandler(this.Reklámnevelista_MouseEnter);
            this.Reklámnevelista.MouseLeave += new System.EventHandler(this.Reklámnevelista_MouseLeave);
            this.Reklámnevelista.MouseHover += new System.EventHandler(this.Reklámnevelista_MouseHover);
            // 
            // Típuslista
            // 
            this.Típuslista.CheckOnClick = true;
            this.Típuslista.FormattingEnabled = true;
            this.Típuslista.Location = new System.Drawing.Point(446, 26);
            this.Típuslista.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Típuslista.Name = "Típuslista";
            this.Típuslista.Size = new System.Drawing.Size(148, 25);
            this.Típuslista.TabIndex = 129;
            this.Típuslista.MouseEnter += new System.EventHandler(this.Típuslista_MouseEnter);
            this.Típuslista.MouseLeave += new System.EventHandler(this.Típuslista_MouseLeave);
            this.Típuslista.MouseHover += new System.EventHandler(this.Típuslista_MouseHover);
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.EnableHeadersVisualStyles = false;
            this.Tábla.Location = new System.Drawing.Point(3, 56);
            this.Tábla.Name = "Tábla";
            this.Tábla.RowHeadersVisible = false;
            this.Tábla.RowHeadersWidth = 51;
            this.Tábla.Size = new System.Drawing.Size(1174, 317);
            this.Tábla.TabIndex = 114;
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.TabPage3.Controls.Add(this.ListPályaszám);
            this.TabPage3.Controls.Add(this.Naplótól);
            this.TabPage3.Controls.Add(this.Naplóig);
            this.TabPage3.Controls.Add(this.TáblaNapló);
            this.TabPage3.Controls.Add(this.Command6);
            this.TabPage3.Controls.Add(this.Command5);
            this.TabPage3.Location = new System.Drawing.Point(4, 29);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(1183, 379);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Naplózás";
            // 
            // ListPályaszám
            // 
            this.ListPályaszám.FormattingEnabled = true;
            this.ListPályaszám.Location = new System.Drawing.Point(233, 14);
            this.ListPályaszám.Name = "ListPályaszám";
            this.ListPályaszám.Size = new System.Drawing.Size(156, 28);
            this.ListPályaszám.TabIndex = 150;
            // 
            // Naplótól
            // 
            this.Naplótól.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Naplótól.Location = new System.Drawing.Point(11, 16);
            this.Naplótól.Name = "Naplótól";
            this.Naplótól.Size = new System.Drawing.Size(105, 26);
            this.Naplótól.TabIndex = 149;
            // 
            // Naplóig
            // 
            this.Naplóig.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Naplóig.Location = new System.Drawing.Point(122, 16);
            this.Naplóig.Name = "Naplóig";
            this.Naplóig.Size = new System.Drawing.Size(105, 26);
            this.Naplóig.TabIndex = 148;
            // 
            // TáblaNapló
            // 
            this.TáblaNapló.AllowUserToAddRows = false;
            this.TáblaNapló.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.TáblaNapló.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.TáblaNapló.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TáblaNapló.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.TáblaNapló.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TáblaNapló.EnableHeadersVisualStyles = false;
            this.TáblaNapló.Location = new System.Drawing.Point(11, 57);
            this.TáblaNapló.Name = "TáblaNapló";
            this.TáblaNapló.RowHeadersVisible = false;
            this.TáblaNapló.RowHeadersWidth = 51;
            this.TáblaNapló.Size = new System.Drawing.Size(1161, 316);
            this.TáblaNapló.TabIndex = 138;
            // 
            // Command6
            // 
            this.Command6.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Command6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command6.Location = new System.Drawing.Point(463, 5);
            this.Command6.Name = "Command6";
            this.Command6.Size = new System.Drawing.Size(45, 45);
            this.Command6.TabIndex = 146;
            this.ToolTip1.SetToolTip(this.Command6, "Táblázat tartalmát kimenti Excelbe.\r\n");
            this.Command6.UseVisualStyleBackColor = true;
            this.Command6.Click += new System.EventHandler(this.Command6_Click);
            // 
            // Command5
            // 
            this.Command5.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Command5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command5.Location = new System.Drawing.Point(412, 5);
            this.Command5.Name = "Command5";
            this.Command5.Size = new System.Drawing.Size(45, 45);
            this.Command5.TabIndex = 145;
            this.ToolTip1.SetToolTip(this.Command5, "Kiválasztott feltételeknek megfelelően kiírja a naplózást.\r\n");
            this.Command5.UseVisualStyleBackColor = true;
            this.Command5.Click += new System.EventHandler(this.Command5_Click);
            // 
            // Button13
            // 
            this.Button13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button13.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.Button13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button13.Location = new System.Drawing.Point(1151, 5);
            this.Button13.Name = "Button13";
            this.Button13.Size = new System.Drawing.Size(45, 45);
            this.Button13.TabIndex = 65;
            this.ToolTip1.SetToolTip(this.Button13, "Súgó");
            this.Button13.UseVisualStyleBackColor = true;
            this.Button13.Click += new System.EventHandler(this.Button13_Click);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(380, 15);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(765, 30);
            this.Holtart.TabIndex = 123;
            this.Holtart.Visible = false;
            // 
            // Ablak_reklám
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1200, 479);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Lapfülek);
            this.Controls.Add(this.Button13);
            this.Controls.Add(this.Panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_reklám";
            this.Text = "Ablak_reklám";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_reklám_FormClosed);
            this.Load += new System.EventHandler(this.Ablak_reklám_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.Lapfülek.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.TabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.TabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TáblaNapló)).EndInit();
            this.ResumeLayout(false);

        }
        internal Button Button13;
        internal Panel Panel1;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal TabControl Lapfülek;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal CheckBox CheckBox1;
        internal TextBox Telephely;
        internal TextBox Típus;
        internal TextBox Reklám;
        internal TextBox Vonal;
        internal TextBox Megjegyzés;
        internal TextBox Szerelvény;
        internal TextBox Pályaszám;
        internal Label Label10;
        internal Label Label9;
        internal Label Label8;
        internal Label Label7;
        internal Label Label6;
        internal Label Label5;
        internal Label Label4;
        internal Label Label3;
        internal Label Label2;
        internal Label Label1;
        internal TabPage TabPage3;
        internal ComboBox Méret;
        internal DateTimePicker Rekezd;
        internal DateTimePicker Revég;
        internal Panel Panel2;
        internal DateTimePicker Ragaszt;
        internal Button Listáz;
        internal Button Beilleszt;
        internal Button Másol;
        internal Button Törlés;
        internal Button Rögzít;
        internal Label Label11;
        internal Button Command3;
        internal DataGridView Tábla;
        internal CheckedListBox Reklámnevelista;
        internal CheckedListBox Típuslista;
        internal Button Button3;
        internal Button Excellekérdezés;
        internal Button Command6;
        internal Button Command5;
        internal DataGridView TáblaNapló;
        internal CheckedListBox TelephelyList;
        internal DateTimePicker Naplótól;
        internal DateTimePicker Naplóig;
        internal ComboBox ListPályaszám;
        internal ToolTip ToolTip1;
        internal Button Reklám_Semmi;
        internal Button Reklám_Mind;
        internal Button Telephely_Semmi;
        internal Button Telephely_Mind;
        internal Button Típus_Semmi;
        internal Button Típus_Mind;
        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal Button Utasítás;
    }
}