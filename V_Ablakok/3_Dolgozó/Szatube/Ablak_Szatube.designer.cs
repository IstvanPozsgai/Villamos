using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{

    public partial class Ablak_Szatube : Form
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Szatube));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.CmbTelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.TabFülek = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.Határnapig_Összesít = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Határnap = new System.Windows.Forms.DateTimePicker();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.SzabLeadás = new System.Windows.Forms.Button();
            this.SzabNyomtatás = new System.Windows.Forms.Button();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Rögzített = new System.Windows.Forms.RadioButton();
            this.Nyomtatott = new System.Windows.Forms.RadioButton();
            this.Kért = new System.Windows.Forms.RadioButton();
            this.Mind = new System.Windows.Forms.RadioButton();
            this.BtnÖsszSzabiLista = new System.Windows.Forms.Button();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.SzabNyilat = new System.Windows.Forms.Button();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.Szabiok = new System.Windows.Forms.ComboBox();
            this.Szab_Rögzít = new System.Windows.Forms.Button();
            this.Szabipótnap = new System.Windows.Forms.TextBox();
            this.Éves_Összesítő = new System.Windows.Forms.Button();
            this.Szabi_Egyéni_Listáz = new System.Windows.Forms.Button();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.Chk_CTRL = new System.Windows.Forms.CheckBox();
            this.Panel6 = new System.Windows.Forms.Panel();
            this.CheckBox2 = new System.Windows.Forms.CheckBox();
            this.TúlCsopNyom = new System.Windows.Forms.Button();
            this.Túl_Eng_Beáll = new System.Windows.Forms.Button();
            this.EgyéniTúlNyom = new System.Windows.Forms.Button();
            this.Panel5 = new System.Windows.Forms.Panel();
            this.Túlórarögzített = new System.Windows.Forms.RadioButton();
            this.Túlóranyomtatott = new System.Windows.Forms.RadioButton();
            this.Túlóraigényelt = new System.Windows.Forms.RadioButton();
            this.Túlóramind = new System.Windows.Forms.RadioButton();
            this.BtnTúlóraÖsszlekérd = new System.Windows.Forms.Button();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.Túl_egy_kiirás = new System.Windows.Forms.Button();
            this.TabPage5 = new System.Windows.Forms.TabPage();
            this.Beteg_Össz = new System.Windows.Forms.Button();
            this.TabPage6 = new System.Windows.Forms.TabPage();
            this.Beteg_Egy = new System.Windows.Forms.Button();
            this.TabPage7 = new System.Windows.Forms.TabPage();
            this.Csúsz_Össz_lista = new System.Windows.Forms.Button();
            this.TabPage8 = new System.Windows.Forms.TabPage();
            this.Csúsz_Egy_lista = new System.Windows.Forms.Button();
            this.TabPage9 = new System.Windows.Forms.TabPage();
            this.Aft_Össz_Lista = new System.Windows.Forms.Button();
            this.TabPage10 = new System.Windows.Forms.TabPage();
            this.Aft_Egy_Lista = new System.Windows.Forms.Button();
            this.Kilépettjel = new System.Windows.Forms.CheckBox();
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.Dolgozónév = new System.Windows.Forms.ComboBox();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.Adat_Évek = new System.Windows.Forms.ComboBox();
            this.Súgó = new System.Windows.Forms.Button();
            this.BtnExcelkimenet = new System.Windows.Forms.Button();
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.Panel1.SuspendLayout();
            this.TabFülek.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.Panel3.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.TabPage2.SuspendLayout();
            this.Panel4.SuspendLayout();
            this.TabPage3.SuspendLayout();
            this.Panel6.SuspendLayout();
            this.Panel5.SuspendLayout();
            this.TabPage4.SuspendLayout();
            this.TabPage5.SuspendLayout();
            this.TabPage6.SuspendLayout();
            this.TabPage7.SuspendLayout();
            this.TabPage8.SuspendLayout();
            this.TabPage9.SuspendLayout();
            this.TabPage10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.CmbTelephely);
            this.Panel1.Controls.Add(this.Label13);
            this.Panel1.Location = new System.Drawing.Point(12, 19);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(373, 33);
            this.Panel1.TabIndex = 45;
            // 
            // CmbTelephely
            // 
            this.CmbTelephely.FormattingEnabled = true;
            this.CmbTelephely.Location = new System.Drawing.Point(175, 2);
            this.CmbTelephely.Name = "CmbTelephely";
            this.CmbTelephely.Size = new System.Drawing.Size(186, 28);
            this.CmbTelephely.TabIndex = 18;
            this.CmbTelephely.SelectedIndexChanged += new System.EventHandler(this.CmbTelephely_SelectedIndexChanged);
            this.CmbTelephely.SelectionChangeCommitted += new System.EventHandler(this.CmbTelephely_SelectionChangeCommitted);
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(3, 4);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(128, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // TabFülek
            // 
            this.TabFülek.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabFülek.Controls.Add(this.TabPage1);
            this.TabFülek.Controls.Add(this.TabPage2);
            this.TabFülek.Controls.Add(this.TabPage3);
            this.TabFülek.Controls.Add(this.TabPage4);
            this.TabFülek.Controls.Add(this.TabPage5);
            this.TabFülek.Controls.Add(this.TabPage6);
            this.TabFülek.Controls.Add(this.TabPage7);
            this.TabFülek.Controls.Add(this.TabPage8);
            this.TabFülek.Controls.Add(this.TabPage9);
            this.TabFülek.Controls.Add(this.TabPage10);
            this.TabFülek.ItemSize = new System.Drawing.Size(144, 25);
            this.TabFülek.Location = new System.Drawing.Point(5, 73);
            this.TabFülek.Name = "TabFülek";
            this.TabFülek.Padding = new System.Drawing.Point(16, 3);
            this.TabFülek.SelectedIndex = 0;
            this.TabFülek.Size = new System.Drawing.Size(1251, 102);
            this.TabFülek.TabIndex = 47;
            this.TabFülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Tabfülek_DrawItem);
            this.TabFülek.SelectedIndexChanged += new System.EventHandler(this.TabFülek_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.LimeGreen;
            this.TabPage1.Controls.Add(this.Határnapig_Összesít);
            this.TabPage1.Controls.Add(this.label2);
            this.TabPage1.Controls.Add(this.Határnap);
            this.TabPage1.Controls.Add(this.Panel3);
            this.TabPage1.Controls.Add(this.Panel2);
            this.TabPage1.Controls.Add(this.BtnÖsszSzabiLista);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1243, 69);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Szabadság Összesítő";
            // 
            // Határnapig_Összesít
            // 
            this.Határnapig_Összesít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Határnapig_Összesít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Határnapig_Összesít.Location = new System.Drawing.Point(667, 15);
            this.Határnapig_Összesít.Name = "Határnapig_Összesít";
            this.Határnapig_Összesít.Size = new System.Drawing.Size(45, 45);
            this.Határnapig_Összesít.TabIndex = 42;
            this.ToolTip1.SetToolTip(this.Határnapig_Összesít, "Összesíti az szabadságokat");
            this.Határnapig_Összesít.UseVisualStyleBackColor = true;
            this.Határnapig_Összesít.Click += new System.EventHandler(this.Határnapig_Összesít_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.ForestGreen;
            this.label2.Location = new System.Drawing.Point(521, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 25);
            this.label2.TabIndex = 41;
            this.label2.Text = "Összesítési határnap:";
            // 
            // Határnap
            // 
            this.Határnap.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Határnap.Location = new System.Drawing.Point(546, 36);
            this.Határnap.Name = "Határnap";
            this.Határnap.Size = new System.Drawing.Size(87, 26);
            this.Határnap.TabIndex = 40;
            // 
            // Panel3
            // 
            this.Panel3.BackColor = System.Drawing.Color.ForestGreen;
            this.Panel3.Controls.Add(this.SzabLeadás);
            this.Panel3.Controls.Add(this.SzabNyomtatás);
            this.Panel3.Location = new System.Drawing.Point(393, 5);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(111, 58);
            this.Panel3.TabIndex = 39;
            // 
            // SzabLeadás
            // 
            this.SzabLeadás.BackgroundImage = global::Villamos.Properties.Resources.leadott;
            this.SzabLeadás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SzabLeadás.Location = new System.Drawing.Point(58, 7);
            this.SzabLeadás.Name = "SzabLeadás";
            this.SzabLeadás.Size = new System.Drawing.Size(45, 45);
            this.SzabLeadás.TabIndex = 39;
            this.ToolTip1.SetToolTip(this.SzabLeadás, "Szabadságengedély státusának leadottra állítása");
            this.SzabLeadás.UseVisualStyleBackColor = true;
            this.SzabLeadás.Click += new System.EventHandler(this.SzabLeadás_Click);
            // 
            // SzabNyomtatás
            // 
            this.SzabNyomtatás.BackgroundImage = global::Villamos.Properties.Resources.Yellow_Glass_Folders_Icon_28;
            this.SzabNyomtatás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SzabNyomtatás.Location = new System.Drawing.Point(7, 7);
            this.SzabNyomtatás.Name = "SzabNyomtatás";
            this.SzabNyomtatás.Size = new System.Drawing.Size(45, 45);
            this.SzabNyomtatás.TabIndex = 38;
            this.ToolTip1.SetToolTip(this.SzabNyomtatás, "Szabadságengedélyek nyomtatása");
            this.SzabNyomtatás.UseVisualStyleBackColor = true;
            this.SzabNyomtatás.Click += new System.EventHandler(this.SzabNyomtatás_Click);
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.Rögzített);
            this.Panel2.Controls.Add(this.Nyomtatott);
            this.Panel2.Controls.Add(this.Kért);
            this.Panel2.Controls.Add(this.Mind);
            this.Panel2.Location = new System.Drawing.Point(8, 18);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(325, 30);
            this.Panel2.TabIndex = 38;
            // 
            // Rögzített
            // 
            this.Rögzített.AutoSize = true;
            this.Rögzített.BackColor = System.Drawing.Color.ForestGreen;
            this.Rögzített.Location = new System.Drawing.Point(240, 4);
            this.Rögzített.Name = "Rögzített";
            this.Rögzített.Size = new System.Drawing.Size(79, 24);
            this.Rögzített.TabIndex = 3;
            this.Rögzített.Text = "Rögzített";
            this.Rögzített.UseVisualStyleBackColor = false;
            this.Rögzített.Click += new System.EventHandler(this.Rögzített_Click);
            // 
            // Nyomtatott
            // 
            this.Nyomtatott.AutoSize = true;
            this.Nyomtatott.BackColor = System.Drawing.Color.ForestGreen;
            this.Nyomtatott.Location = new System.Drawing.Point(145, 3);
            this.Nyomtatott.Name = "Nyomtatott";
            this.Nyomtatott.Size = new System.Drawing.Size(89, 24);
            this.Nyomtatott.TabIndex = 2;
            this.Nyomtatott.Text = "Nyomtatott";
            this.Nyomtatott.UseVisualStyleBackColor = false;
            this.Nyomtatott.Click += new System.EventHandler(this.Nyomtatott_Click);
            // 
            // Kért
            // 
            this.Kért.AutoSize = true;
            this.Kért.BackColor = System.Drawing.Color.ForestGreen;
            this.Kért.Location = new System.Drawing.Point(65, 3);
            this.Kért.Name = "Kért";
            this.Kért.Size = new System.Drawing.Size(74, 24);
            this.Kért.TabIndex = 1;
            this.Kért.Text = "Igényelt";
            this.Kért.UseVisualStyleBackColor = false;
            this.Kért.Click += new System.EventHandler(this.Kért_Click);
            // 
            // Mind
            // 
            this.Mind.AutoSize = true;
            this.Mind.BackColor = System.Drawing.Color.ForestGreen;
            this.Mind.Checked = true;
            this.Mind.Location = new System.Drawing.Point(3, 3);
            this.Mind.Name = "Mind";
            this.Mind.Size = new System.Drawing.Size(56, 24);
            this.Mind.TabIndex = 0;
            this.Mind.TabStop = true;
            this.Mind.Text = "Mind";
            this.Mind.UseVisualStyleBackColor = false;
            this.Mind.Click += new System.EventHandler(this.Mind_Click);
            // 
            // BtnÖsszSzabiLista
            // 
            this.BtnÖsszSzabiLista.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.BtnÖsszSzabiLista.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnÖsszSzabiLista.Location = new System.Drawing.Point(341, 13);
            this.BtnÖsszSzabiLista.Name = "BtnÖsszSzabiLista";
            this.BtnÖsszSzabiLista.Size = new System.Drawing.Size(45, 45);
            this.BtnÖsszSzabiLista.TabIndex = 37;
            this.ToolTip1.SetToolTip(this.BtnÖsszSzabiLista, "Frissíti a táblázat adatait.");
            this.BtnÖsszSzabiLista.UseVisualStyleBackColor = true;
            this.BtnÖsszSzabiLista.Click += new System.EventHandler(this.BtnÖsszSzabiLista_Click);
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.LightSeaGreen;
            this.TabPage2.Controls.Add(this.SzabNyilat);
            this.TabPage2.Controls.Add(this.Panel4);
            this.TabPage2.Controls.Add(this.Éves_Összesítő);
            this.TabPage2.Controls.Add(this.Szabi_Egyéni_Listáz);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(1243, 69);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Szabadság Egyéni";
            // 
            // SzabNyilat
            // 
            this.SzabNyilat.BackgroundImage = global::Villamos.Properties.Resources.App_edit;
            this.SzabNyilat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SzabNyilat.Location = new System.Drawing.Point(723, 11);
            this.SzabNyilat.Name = "SzabNyilat";
            this.SzabNyilat.Size = new System.Drawing.Size(45, 45);
            this.SzabNyilat.TabIndex = 40;
            this.ToolTip1.SetToolTip(this.SzabNyilat, "Szabadság kivételének nyilatkozata");
            this.SzabNyilat.UseVisualStyleBackColor = true;
            this.SzabNyilat.Click += new System.EventHandler(this.SzabNyilat_Click);
            // 
            // Panel4
            // 
            this.Panel4.BackColor = System.Drawing.Color.Turquoise;
            this.Panel4.Controls.Add(this.Szabiok);
            this.Panel4.Controls.Add(this.Szab_Rögzít);
            this.Panel4.Controls.Add(this.Szabipótnap);
            this.Panel4.Location = new System.Drawing.Point(225, 6);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(440, 50);
            this.Panel4.TabIndex = 39;
            // 
            // Szabiok
            // 
            this.Szabiok.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Szabiok.FormattingEnabled = true;
            this.Szabiok.Location = new System.Drawing.Point(3, 9);
            this.Szabiok.Name = "Szabiok";
            this.Szabiok.Size = new System.Drawing.Size(284, 28);
            this.Szabiok.TabIndex = 40;
            this.ToolTip1.SetToolTip(this.Szabiok, "Korrekció oka");
            // 
            // Szab_Rögzít
            // 
            this.Szab_Rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Szab_Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Szab_Rögzít.Location = new System.Drawing.Point(392, 3);
            this.Szab_Rögzít.Name = "Szab_Rögzít";
            this.Szab_Rögzít.Size = new System.Drawing.Size(45, 45);
            this.Szab_Rögzít.TabIndex = 39;
            this.ToolTip1.SetToolTip(this.Szab_Rögzít, "Rögzíti a szabadság korrekciós adatokat");
            this.Szab_Rögzít.UseVisualStyleBackColor = true;
            this.Szab_Rögzít.Click += new System.EventHandler(this.Szab_Rögzít_Click);
            // 
            // Szabipótnap
            // 
            this.Szabipótnap.Location = new System.Drawing.Point(293, 9);
            this.Szabipótnap.Name = "Szabipótnap";
            this.Szabipótnap.Size = new System.Drawing.Size(93, 26);
            this.Szabipótnap.TabIndex = 1;
            this.ToolTip1.SetToolTip(this.Szabipótnap, "Korrekciós napok száma +/-");
            // 
            // Éves_Összesítő
            // 
            this.Éves_Összesítő.BackgroundImage = global::Villamos.Properties.Resources.CALENDR1;
            this.Éves_Összesítő.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Éves_Összesítő.Location = new System.Drawing.Point(671, 11);
            this.Éves_Összesítő.Name = "Éves_Összesítő";
            this.Éves_Összesítő.Size = new System.Drawing.Size(45, 45);
            this.Éves_Összesítő.TabIndex = 38;
            this.ToolTip1.SetToolTip(this.Éves_Összesítő, "Éves szabadság összesítő készítése");
            this.Éves_Összesítő.UseVisualStyleBackColor = true;
            this.Éves_Összesítő.Click += new System.EventHandler(this.Éves_Összesítő_Click);
            // 
            // Szabi_Egyéni_Listáz
            // 
            this.Szabi_Egyéni_Listáz.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Szabi_Egyéni_Listáz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Szabi_Egyéni_Listáz.Location = new System.Drawing.Point(6, 11);
            this.Szabi_Egyéni_Listáz.Name = "Szabi_Egyéni_Listáz";
            this.Szabi_Egyéni_Listáz.Size = new System.Drawing.Size(45, 45);
            this.Szabi_Egyéni_Listáz.TabIndex = 37;
            this.ToolTip1.SetToolTip(this.Szabi_Egyéni_Listáz, "Frissíti a táblázat adatait.");
            this.Szabi_Egyéni_Listáz.UseVisualStyleBackColor = true;
            this.Szabi_Egyéni_Listáz.Click += new System.EventHandler(this.Szabi_Egyéni_Listáz_Click);
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.LimeGreen;
            this.TabPage3.Controls.Add(this.Chk_CTRL);
            this.TabPage3.Controls.Add(this.Panel6);
            this.TabPage3.Controls.Add(this.Panel5);
            this.TabPage3.Controls.Add(this.BtnTúlóraÖsszlekérd);
            this.TabPage3.Location = new System.Drawing.Point(4, 29);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(1243, 69);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Túlóra Összesítő";
            // 
            // Chk_CTRL
            // 
            this.Chk_CTRL.AutoSize = true;
            this.Chk_CTRL.Location = new System.Drawing.Point(887, 11);
            this.Chk_CTRL.Name = "Chk_CTRL";
            this.Chk_CTRL.Size = new System.Drawing.Size(111, 24);
            this.Chk_CTRL.TabIndex = 59;
            this.Chk_CTRL.Text = "CTRL nyomva";
            this.Chk_CTRL.UseVisualStyleBackColor = true;
            this.Chk_CTRL.Visible = false;
            // 
            // Panel6
            // 
            this.Panel6.BackColor = System.Drawing.Color.ForestGreen;
            this.Panel6.Controls.Add(this.CheckBox2);
            this.Panel6.Controls.Add(this.TúlCsopNyom);
            this.Panel6.Controls.Add(this.Túl_Eng_Beáll);
            this.Panel6.Controls.Add(this.EgyéniTúlNyom);
            this.Panel6.Location = new System.Drawing.Point(407, 5);
            this.Panel6.Name = "Panel6";
            this.Panel6.Size = new System.Drawing.Size(368, 55);
            this.Panel6.TabIndex = 40;
            this.Panel6.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Panel6_MouseClick);
            // 
            // CheckBox2
            // 
            this.CheckBox2.AutoSize = true;
            this.CheckBox2.Location = new System.Drawing.Point(167, 11);
            this.CheckBox2.Name = "CheckBox2";
            this.CheckBox2.Size = new System.Drawing.Size(120, 24);
            this.CheckBox2.TabIndex = 41;
            this.CheckBox2.Text = "Ne törölje a fájlt";
            this.CheckBox2.UseVisualStyleBackColor = true;
            this.CheckBox2.Visible = false;
            // 
            // TúlCsopNyom
            // 
            this.TúlCsopNyom.BackgroundImage = global::Villamos.Properties.Resources.Yellow_Glass_Folders_Icon_28;
            this.TúlCsopNyom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TúlCsopNyom.Location = new System.Drawing.Point(92, 3);
            this.TúlCsopNyom.Name = "TúlCsopNyom";
            this.TúlCsopNyom.Size = new System.Drawing.Size(45, 45);
            this.TúlCsopNyom.TabIndex = 40;
            this.ToolTip1.SetToolTip(this.TúlCsopNyom, "Csoportos túlóra nyomtatás");
            this.TúlCsopNyom.UseVisualStyleBackColor = true;
            this.TúlCsopNyom.Click += new System.EventHandler(this.TúlCsopNyom_Click);
            // 
            // Túl_Eng_Beáll
            // 
            this.Túl_Eng_Beáll.BackgroundImage = global::Villamos.Properties.Resources.leadott;
            this.Túl_Eng_Beáll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Túl_Eng_Beáll.Location = new System.Drawing.Point(320, 3);
            this.Túl_Eng_Beáll.Name = "Túl_Eng_Beáll";
            this.Túl_Eng_Beáll.Size = new System.Drawing.Size(45, 45);
            this.Túl_Eng_Beáll.TabIndex = 39;
            this.ToolTip1.SetToolTip(this.Túl_Eng_Beáll, "Túlóra leadott státusának beállítása");
            this.Túl_Eng_Beáll.UseVisualStyleBackColor = true;
            this.Túl_Eng_Beáll.Click += new System.EventHandler(this.Túl_Eng_Beáll_Click);
            // 
            // EgyéniTúlNyom
            // 
            this.EgyéniTúlNyom.BackgroundImage = global::Villamos.Properties.Resources.Yellow_Glass_Folders_Icon_28;
            this.EgyéniTúlNyom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.EgyéniTúlNyom.Location = new System.Drawing.Point(3, 3);
            this.EgyéniTúlNyom.Name = "EgyéniTúlNyom";
            this.EgyéniTúlNyom.Size = new System.Drawing.Size(45, 45);
            this.EgyéniTúlNyom.TabIndex = 38;
            this.ToolTip1.SetToolTip(this.EgyéniTúlNyom, "Egyéni túlóra nyomtatás");
            this.EgyéniTúlNyom.UseVisualStyleBackColor = true;
            this.EgyéniTúlNyom.Click += new System.EventHandler(this.EgyéniTúlNyom_Click);
            // 
            // Panel5
            // 
            this.Panel5.Controls.Add(this.Túlórarögzített);
            this.Panel5.Controls.Add(this.Túlóranyomtatott);
            this.Panel5.Controls.Add(this.Túlóraigényelt);
            this.Panel5.Controls.Add(this.Túlóramind);
            this.Panel5.Location = new System.Drawing.Point(8, 18);
            this.Panel5.Name = "Panel5";
            this.Panel5.Size = new System.Drawing.Size(325, 30);
            this.Panel5.TabIndex = 39;
            // 
            // Túlórarögzített
            // 
            this.Túlórarögzített.AutoSize = true;
            this.Túlórarögzített.BackColor = System.Drawing.Color.ForestGreen;
            this.Túlórarögzített.Location = new System.Drawing.Point(240, 4);
            this.Túlórarögzített.Name = "Túlórarögzített";
            this.Túlórarögzített.Size = new System.Drawing.Size(79, 24);
            this.Túlórarögzített.TabIndex = 3;
            this.Túlórarögzített.TabStop = true;
            this.Túlórarögzített.Text = "Rögzített";
            this.Túlórarögzített.UseVisualStyleBackColor = false;
            this.Túlórarögzített.Click += new System.EventHandler(this.Túlórarögzített_Click);
            // 
            // Túlóranyomtatott
            // 
            this.Túlóranyomtatott.AutoSize = true;
            this.Túlóranyomtatott.BackColor = System.Drawing.Color.ForestGreen;
            this.Túlóranyomtatott.Location = new System.Drawing.Point(145, 3);
            this.Túlóranyomtatott.Name = "Túlóranyomtatott";
            this.Túlóranyomtatott.Size = new System.Drawing.Size(89, 24);
            this.Túlóranyomtatott.TabIndex = 2;
            this.Túlóranyomtatott.TabStop = true;
            this.Túlóranyomtatott.Text = "Nyomtatott";
            this.Túlóranyomtatott.UseVisualStyleBackColor = false;
            this.Túlóranyomtatott.Click += new System.EventHandler(this.Túlóranyomtatott_Click);
            // 
            // Túlóraigényelt
            // 
            this.Túlóraigényelt.AutoSize = true;
            this.Túlóraigényelt.BackColor = System.Drawing.Color.ForestGreen;
            this.Túlóraigényelt.Location = new System.Drawing.Point(65, 3);
            this.Túlóraigényelt.Name = "Túlóraigényelt";
            this.Túlóraigényelt.Size = new System.Drawing.Size(74, 24);
            this.Túlóraigényelt.TabIndex = 1;
            this.Túlóraigényelt.TabStop = true;
            this.Túlóraigényelt.Text = "Igényelt";
            this.Túlóraigényelt.UseVisualStyleBackColor = false;
            this.Túlóraigényelt.Click += new System.EventHandler(this.Túlóraigényelt_Click);
            // 
            // Túlóramind
            // 
            this.Túlóramind.AutoSize = true;
            this.Túlóramind.BackColor = System.Drawing.Color.ForestGreen;
            this.Túlóramind.Checked = true;
            this.Túlóramind.Location = new System.Drawing.Point(3, 4);
            this.Túlóramind.Name = "Túlóramind";
            this.Túlóramind.Size = new System.Drawing.Size(56, 24);
            this.Túlóramind.TabIndex = 0;
            this.Túlóramind.TabStop = true;
            this.Túlóramind.Text = "Mind";
            this.Túlóramind.UseVisualStyleBackColor = false;
            this.Túlóramind.Click += new System.EventHandler(this.Túlóramind_Click);
            // 
            // BtnTúlóraÖsszlekérd
            // 
            this.BtnTúlóraÖsszlekérd.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.BtnTúlóraÖsszlekérd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnTúlóraÖsszlekérd.Location = new System.Drawing.Point(339, 11);
            this.BtnTúlóraÖsszlekérd.Name = "BtnTúlóraÖsszlekérd";
            this.BtnTúlóraÖsszlekérd.Size = new System.Drawing.Size(45, 45);
            this.BtnTúlóraÖsszlekérd.TabIndex = 37;
            this.ToolTip1.SetToolTip(this.BtnTúlóraÖsszlekérd, "Frissíti a táblázat adatait.");
            this.BtnTúlóraÖsszlekérd.UseVisualStyleBackColor = true;
            this.BtnTúlóraÖsszlekérd.Click += new System.EventHandler(this.BtnTúlóraÖsszlekérd_Click);
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.LightSeaGreen;
            this.TabPage4.Controls.Add(this.Túl_egy_kiirás);
            this.TabPage4.Location = new System.Drawing.Point(4, 29);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Size = new System.Drawing.Size(1243, 69);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Túlóra Egyéni";
            // 
            // Túl_egy_kiirás
            // 
            this.Túl_egy_kiirás.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Túl_egy_kiirás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Túl_egy_kiirás.Location = new System.Drawing.Point(1, 3);
            this.Túl_egy_kiirás.Name = "Túl_egy_kiirás";
            this.Túl_egy_kiirás.Size = new System.Drawing.Size(45, 45);
            this.Túl_egy_kiirás.TabIndex = 37;
            this.ToolTip1.SetToolTip(this.Túl_egy_kiirás, "Frissíti a táblázat adatait.");
            this.Túl_egy_kiirás.UseVisualStyleBackColor = true;
            this.Túl_egy_kiirás.Click += new System.EventHandler(this.Túl_egy_kiirás_Click);
            // 
            // TabPage5
            // 
            this.TabPage5.BackColor = System.Drawing.Color.LimeGreen;
            this.TabPage5.Controls.Add(this.Beteg_Össz);
            this.TabPage5.Location = new System.Drawing.Point(4, 29);
            this.TabPage5.Name = "TabPage5";
            this.TabPage5.Size = new System.Drawing.Size(1243, 69);
            this.TabPage5.TabIndex = 4;
            this.TabPage5.Text = "Beteg Összesítő";
            // 
            // Beteg_Össz
            // 
            this.Beteg_Össz.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Beteg_Össz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Beteg_Össz.Location = new System.Drawing.Point(6, 5);
            this.Beteg_Össz.Name = "Beteg_Össz";
            this.Beteg_Össz.Size = new System.Drawing.Size(45, 45);
            this.Beteg_Össz.TabIndex = 37;
            this.ToolTip1.SetToolTip(this.Beteg_Össz, "Frissíti a táblázat adatait.");
            this.Beteg_Össz.UseVisualStyleBackColor = true;
            this.Beteg_Össz.Click += new System.EventHandler(this.Beteg_Össz_Click);
            // 
            // TabPage6
            // 
            this.TabPage6.BackColor = System.Drawing.Color.LightSeaGreen;
            this.TabPage6.Controls.Add(this.Beteg_Egy);
            this.TabPage6.Location = new System.Drawing.Point(4, 29);
            this.TabPage6.Name = "TabPage6";
            this.TabPage6.Size = new System.Drawing.Size(1243, 69);
            this.TabPage6.TabIndex = 5;
            this.TabPage6.Text = "Beteg Egyéni";
            // 
            // Beteg_Egy
            // 
            this.Beteg_Egy.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Beteg_Egy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Beteg_Egy.Location = new System.Drawing.Point(6, 5);
            this.Beteg_Egy.Name = "Beteg_Egy";
            this.Beteg_Egy.Size = new System.Drawing.Size(45, 45);
            this.Beteg_Egy.TabIndex = 37;
            this.ToolTip1.SetToolTip(this.Beteg_Egy, "Frissíti a táblázat adatait.");
            this.Beteg_Egy.UseVisualStyleBackColor = true;
            this.Beteg_Egy.Click += new System.EventHandler(this.Beteg_Egy_Click);
            // 
            // TabPage7
            // 
            this.TabPage7.BackColor = System.Drawing.Color.LimeGreen;
            this.TabPage7.Controls.Add(this.Csúsz_Össz_lista);
            this.TabPage7.Location = new System.Drawing.Point(4, 29);
            this.TabPage7.Name = "TabPage7";
            this.TabPage7.Size = new System.Drawing.Size(1243, 69);
            this.TabPage7.TabIndex = 6;
            this.TabPage7.Text = "Csúsztatás Összesítő";
            // 
            // Csúsz_Össz_lista
            // 
            this.Csúsz_Össz_lista.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Csúsz_Össz_lista.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Csúsz_Össz_lista.Location = new System.Drawing.Point(3, 3);
            this.Csúsz_Össz_lista.Name = "Csúsz_Össz_lista";
            this.Csúsz_Össz_lista.Size = new System.Drawing.Size(45, 45);
            this.Csúsz_Össz_lista.TabIndex = 37;
            this.ToolTip1.SetToolTip(this.Csúsz_Össz_lista, "Frissíti a táblázat adatait.");
            this.Csúsz_Össz_lista.UseVisualStyleBackColor = true;
            this.Csúsz_Össz_lista.Click += new System.EventHandler(this.Csúsz_Össz_lista_Click);
            // 
            // TabPage8
            // 
            this.TabPage8.BackColor = System.Drawing.Color.LightSeaGreen;
            this.TabPage8.Controls.Add(this.Csúsz_Egy_lista);
            this.TabPage8.Location = new System.Drawing.Point(4, 29);
            this.TabPage8.Name = "TabPage8";
            this.TabPage8.Size = new System.Drawing.Size(1243, 69);
            this.TabPage8.TabIndex = 7;
            this.TabPage8.Text = "Csúsztatás Egyéni";
            // 
            // Csúsz_Egy_lista
            // 
            this.Csúsz_Egy_lista.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Csúsz_Egy_lista.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Csúsz_Egy_lista.Location = new System.Drawing.Point(3, 3);
            this.Csúsz_Egy_lista.Name = "Csúsz_Egy_lista";
            this.Csúsz_Egy_lista.Size = new System.Drawing.Size(45, 45);
            this.Csúsz_Egy_lista.TabIndex = 37;
            this.ToolTip1.SetToolTip(this.Csúsz_Egy_lista, "Frissíti a táblázat adatait.");
            this.Csúsz_Egy_lista.UseVisualStyleBackColor = true;
            this.Csúsz_Egy_lista.Click += new System.EventHandler(this.Csúsz_Egy_lista_Click);
            // 
            // TabPage9
            // 
            this.TabPage9.BackColor = System.Drawing.Color.LimeGreen;
            this.TabPage9.Controls.Add(this.Aft_Össz_Lista);
            this.TabPage9.Location = new System.Drawing.Point(4, 29);
            this.TabPage9.Name = "TabPage9";
            this.TabPage9.Size = new System.Drawing.Size(1243, 69);
            this.TabPage9.TabIndex = 8;
            this.TabPage9.Text = "AFT Összesítő";
            // 
            // Aft_Össz_Lista
            // 
            this.Aft_Össz_Lista.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Aft_Össz_Lista.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Aft_Össz_Lista.Location = new System.Drawing.Point(3, 3);
            this.Aft_Össz_Lista.Name = "Aft_Össz_Lista";
            this.Aft_Össz_Lista.Size = new System.Drawing.Size(45, 45);
            this.Aft_Össz_Lista.TabIndex = 37;
            this.ToolTip1.SetToolTip(this.Aft_Össz_Lista, "Frissíti a táblázat adatait.");
            this.Aft_Össz_Lista.UseVisualStyleBackColor = true;
            this.Aft_Össz_Lista.Click += new System.EventHandler(this.Aft_Össz_Lista_Click);
            // 
            // TabPage10
            // 
            this.TabPage10.BackColor = System.Drawing.Color.LightSeaGreen;
            this.TabPage10.Controls.Add(this.Aft_Egy_Lista);
            this.TabPage10.Location = new System.Drawing.Point(4, 29);
            this.TabPage10.Name = "TabPage10";
            this.TabPage10.Size = new System.Drawing.Size(1243, 69);
            this.TabPage10.TabIndex = 9;
            this.TabPage10.Text = "AFT Egyéni";
            // 
            // Aft_Egy_Lista
            // 
            this.Aft_Egy_Lista.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Aft_Egy_Lista.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Aft_Egy_Lista.Location = new System.Drawing.Point(3, 3);
            this.Aft_Egy_Lista.Name = "Aft_Egy_Lista";
            this.Aft_Egy_Lista.Size = new System.Drawing.Size(45, 45);
            this.Aft_Egy_Lista.TabIndex = 37;
            this.ToolTip1.SetToolTip(this.Aft_Egy_Lista, "Frissíti a táblázat adatait.");
            this.Aft_Egy_Lista.UseVisualStyleBackColor = true;
            this.Aft_Egy_Lista.Click += new System.EventHandler(this.Aft_Egy_Lista_Click);
            // 
            // Kilépettjel
            // 
            this.Kilépettjel.AutoSize = true;
            this.Kilépettjel.BackColor = System.Drawing.Color.LawnGreen;
            this.Kilépettjel.Location = new System.Drawing.Point(786, 28);
            this.Kilépettjel.Name = "Kilépettjel";
            this.Kilépettjel.Size = new System.Drawing.Size(151, 24);
            this.Kilépettjel.TabIndex = 48;
            this.Kilépettjel.Text = "Kilépett dolgozókkal";
            this.Kilépettjel.UseVisualStyleBackColor = false;
            this.Kilépettjel.CheckStateChanged += new System.EventHandler(this.Kilépettjel_CheckStateChanged);
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.OliveDrab;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.EnableHeadersVisualStyles = false;
            this.Tábla.Location = new System.Drawing.Point(5, 180);
            this.Tábla.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Tábla.Name = "Tábla";
            this.Tábla.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.OliveDrab;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Tábla.RowHeadersWidth = 30;
            this.Tábla.Size = new System.Drawing.Size(1251, 210);
            this.Tábla.TabIndex = 49;
            // 
            // Dolgozónév
            // 
            this.Dolgozónév.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Dolgozónév.FormattingEnabled = true;
            this.Dolgozónév.Location = new System.Drawing.Point(406, 24);
            this.Dolgozónév.MaxDropDownItems = 15;
            this.Dolgozónév.Name = "Dolgozónév";
            this.Dolgozónév.Size = new System.Drawing.Size(374, 28);
            this.Dolgozónév.TabIndex = 53;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.LimeGreen;
            this.label1.Location = new System.Drawing.Point(962, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.TabIndex = 19;
            this.label1.Text = "Év választó:";
            // 
            // Adat_Évek
            // 
            this.Adat_Évek.FormattingEnabled = true;
            this.Adat_Évek.Location = new System.Drawing.Point(953, 39);
            this.Adat_Évek.Name = "Adat_Évek";
            this.Adat_Évek.Size = new System.Drawing.Size(102, 28);
            this.Adat_Évek.TabIndex = 59;
            this.Adat_Évek.SelectedIndexChanged += new System.EventHandler(this.Adat_Évek_SelectedIndexChanged);
            // 
            // Súgó
            // 
            this.Súgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Súgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.Súgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Súgó.Location = new System.Drawing.Point(1213, 7);
            this.Súgó.Name = "Súgó";
            this.Súgó.Size = new System.Drawing.Size(45, 45);
            this.Súgó.TabIndex = 51;
            this.Súgó.UseVisualStyleBackColor = true;
            this.Súgó.Click += new System.EventHandler(this.Súgó_Click);
            // 
            // BtnExcelkimenet
            // 
            this.BtnExcelkimenet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnExcelkimenet.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.BtnExcelkimenet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnExcelkimenet.Location = new System.Drawing.Point(1162, 7);
            this.BtnExcelkimenet.Name = "BtnExcelkimenet";
            this.BtnExcelkimenet.Size = new System.Drawing.Size(45, 45);
            this.BtnExcelkimenet.TabIndex = 50;
            this.BtnExcelkimenet.UseVisualStyleBackColor = true;
            this.BtnExcelkimenet.Click += new System.EventHandler(this.BtnExcelkimenet_Click);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(10, 210);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(1240, 25);
            this.Holtart.TabIndex = 60;
            this.Holtart.Visible = false;
            // 
            // Ablak_Szatube
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OliveDrab;
            this.ClientSize = new System.Drawing.Size(1263, 401);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Adat_Évek);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Dolgozónév);
            this.Controls.Add(this.Súgó);
            this.Controls.Add(this.BtnExcelkimenet);
            this.Controls.Add(this.Tábla);
            this.Controls.Add(this.Kilépettjel);
            this.Controls.Add(this.TabFülek);
            this.Controls.Add(this.Panel1);
            this.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Szatube";
            this.Text = "Szabadság - Túlóra - Betegállomány -  AFT- Csúsztatás";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_Szatube_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AblakSzaTuBe_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.AblakSzaTuBe_KeyUp);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.TabFülek.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.Panel3.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.TabPage2.ResumeLayout(false);
            this.Panel4.ResumeLayout(false);
            this.Panel4.PerformLayout();
            this.TabPage3.ResumeLayout(false);
            this.TabPage3.PerformLayout();
            this.Panel6.ResumeLayout(false);
            this.Panel6.PerformLayout();
            this.Panel5.ResumeLayout(false);
            this.Panel5.PerformLayout();
            this.TabPage4.ResumeLayout(false);
            this.TabPage5.ResumeLayout(false);
            this.TabPage6.ResumeLayout(false);
            this.TabPage7.ResumeLayout(false);
            this.TabPage8.ResumeLayout(false);
            this.TabPage9.ResumeLayout(false);
            this.TabPage10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal Panel Panel1;
        internal ComboBox CmbTelephely;
        internal Label Label13;
        internal TabControl TabFülek;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal TabPage TabPage3;
        internal TabPage TabPage4;
        internal TabPage TabPage5;
        internal TabPage TabPage6;
        internal TabPage TabPage7;
        internal TabPage TabPage8;
        internal TabPage TabPage9;
        internal TabPage TabPage10;
        internal Button BtnÖsszSzabiLista;
        internal Button Szabi_Egyéni_Listáz;
        internal Button BtnTúlóraÖsszlekérd;
        internal Button Túl_egy_kiirás;
        internal Button Beteg_Össz;
        internal Button Beteg_Egy;
        internal Button Csúsz_Össz_lista;
        internal Button Csúsz_Egy_lista;
        internal Button Aft_Össz_Lista;
        internal Button Aft_Egy_Lista;
        internal CheckBox Kilépettjel;
        internal DataGridView Tábla;
        internal Panel Panel2;
        internal RadioButton Rögzített;
        internal RadioButton Nyomtatott;
        internal RadioButton Kért;
        internal RadioButton Mind;
        internal Panel Panel3;
        internal Button SzabLeadás;
        internal Button SzabNyomtatás;
        internal Panel Panel4;
        internal Button Szab_Rögzít;
        internal TextBox Szabipótnap;
        internal Button Éves_Összesítő;
        internal Button BtnExcelkimenet;
        internal Button Súgó;
        internal Panel Panel6;
        internal CheckBox CheckBox2;
        internal Button TúlCsopNyom;
        internal Button Túl_Eng_Beáll;
        internal Button EgyéniTúlNyom;
        internal Panel Panel5;
        internal RadioButton Túlórarögzített;
        internal RadioButton Túlóranyomtatott;
        internal RadioButton Túlóraigényelt;
        internal RadioButton Túlóramind;
        internal ComboBox Dolgozónév;
        internal ComboBox Szabiok;
        internal Button SzabNyilat;
        internal ToolTip ToolTip1;
        internal Label label1;
        internal ComboBox Adat_Évek;
        internal CheckBox Chk_CTRL;
        private Label label2;
        private DateTimePicker Határnap;
        internal Button Határnapig_Összesít;
        internal V_MindenEgyéb.MyProgressbar Holtart;
    }
}