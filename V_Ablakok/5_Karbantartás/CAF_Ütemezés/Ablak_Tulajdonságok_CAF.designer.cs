using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
    public partial class Ablak_Tulajdonságok_CAF : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Tulajdonságok_CAF));
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Caf_Listák = new System.Windows.Forms.Button();
            this.Színbeállítás = new System.Windows.Forms.Button();
            this.Alap_adatok = new System.Windows.Forms.Button();
            this.Button3 = new System.Windows.Forms.Button();
            this.Button2 = new System.Windows.Forms.Button();
            this.Elő_Lehívás = new System.Windows.Forms.Button();
            this.Elő_Excel = new System.Windows.Forms.Button();
            this.Elő_havi = new System.Windows.Forms.Button();
            this.ELő_törlés = new System.Windows.Forms.Button();
            this.Elő_Visszacsuk = new System.Windows.Forms.Button();
            this.Elő_Mindtöröl = new System.Windows.Forms.Button();
            this.Elő_Összeskijelöl = new System.Windows.Forms.Button();
            this.Elő_Lenyit = new System.Windows.Forms.Button();
            this.Tábla_frissítés = new System.Windows.Forms.Button();
            this.Előtervet_készít = new System.Windows.Forms.Button();
            this.Elő_tervező_telephely = new System.Windows.Forms.Button();
            this.Elő_ütemez = new System.Windows.Forms.Button();
            this.Segédablak_hívó = new System.Windows.Forms.Button();
            this.Elő_törölt = new System.Windows.Forms.CheckBox();
            this.Elő_pályaszám = new System.Windows.Forms.CheckedListBox();
            this.Elő_Idő = new System.Windows.Forms.RadioButton();
            this.Elő_Km = new System.Windows.Forms.RadioButton();
            this.Elő_Mind = new System.Windows.Forms.RadioButton();
            this.Label21 = new System.Windows.Forms.Label();
            this.Elő_Dátumig = new System.Windows.Forms.DateTimePicker();
            this.Elő_Dátumtól = new System.Windows.Forms.DateTimePicker();
            this.Tábla_elő = new System.Windows.Forms.DataGridView();
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.km_modosit_btn = new System.Windows.Forms.Button();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_elő)).BeginInit();
            this.SuspendLayout();
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.BackColor = System.Drawing.Color.Lime;
            this.Holtart.ForeColor = System.Drawing.Color.MediumBlue;
            this.Holtart.Location = new System.Drawing.Point(469, 15);
            this.Holtart.Maximum = 20;
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(683, 20);
            this.Holtart.TabIndex = 154;
            this.Holtart.Visible = false;
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.Cmbtelephely);
            this.Panel1.Controls.Add(this.Label13);
            this.Panel1.Location = new System.Drawing.Point(10, 10);
            this.Panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(374, 42);
            this.Panel1.TabIndex = 152;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(176, 2);
            this.Cmbtelephely.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            // ToolTip1
            // 
            this.ToolTip1.IsBalloon = true;
            // 
            // Caf_Listák
            // 
            this.Caf_Listák.BackgroundImage = global::Villamos.Properties.Resources.CARDFIL3;
            this.Caf_Listák.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Caf_Listák.Location = new System.Drawing.Point(1026, 78);
            this.Caf_Listák.Name = "Caf_Listák";
            this.Caf_Listák.Size = new System.Drawing.Size(40, 40);
            this.Caf_Listák.TabIndex = 240;
            this.ToolTip1.SetToolTip(this.Caf_Listák, "Jármű Listák");
            this.Caf_Listák.UseVisualStyleBackColor = true;
            this.Caf_Listák.Click += new System.EventHandler(this.Caf_Listák_Click);
            // 
            // Színbeállítás
            // 
            this.Színbeállítás.BackgroundImage = global::Villamos.Properties.Resources.Dtafalonso_Modern_Xp_ModernXP_12_Workstation_Desktop_Colors;
            this.Színbeállítás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Színbeállítás.Location = new System.Drawing.Point(1164, 79);
            this.Színbeállítás.Name = "Színbeállítás";
            this.Színbeállítás.Size = new System.Drawing.Size(40, 40);
            this.Színbeállítás.TabIndex = 239;
            this.ToolTip1.SetToolTip(this.Színbeállítás, "Excel tábla színkezelés");
            this.Színbeállítás.UseVisualStyleBackColor = true;
            this.Színbeállítás.Click += new System.EventHandler(this.Színbeállítás_Click);
            // 
            // Alap_adatok
            // 
            this.Alap_adatok.BackgroundImage = global::Villamos.Properties.Resources.process_accept;
            this.Alap_adatok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Alap_adatok.Location = new System.Drawing.Point(1072, 79);
            this.Alap_adatok.Name = "Alap_adatok";
            this.Alap_adatok.Size = new System.Drawing.Size(40, 40);
            this.Alap_adatok.TabIndex = 238;
            this.ToolTip1.SetToolTip(this.Alap_adatok, "Jármű alapadatok");
            this.Alap_adatok.UseVisualStyleBackColor = true;
            this.Alap_adatok.Click += new System.EventHandler(this.Alap_adatok_Click);
            // 
            // Button3
            // 
            this.Button3.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button3.Location = new System.Drawing.Point(980, 44);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(30, 30);
            this.Button3.TabIndex = 237;
            this.ToolTip1.SetToolTip(this.Button3, "Frissíti a táblázat adatait");
            this.Button3.UseVisualStyleBackColor = true;
            this.Button3.Visible = false;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Button2
            // 
            this.Button2.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button2.Location = new System.Drawing.Point(665, 41);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(30, 30);
            this.Button2.TabIndex = 236;
            this.ToolTip1.SetToolTip(this.Button2, "Frissíti a táblázat adatait");
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Visible = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Elő_Lehívás
            // 
            this.Elő_Lehívás.BackgroundImage = global::Villamos.Properties.Resources.leadott;
            this.Elő_Lehívás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Elő_Lehívás.Location = new System.Drawing.Point(850, 78);
            this.Elő_Lehívás.Name = "Elő_Lehívás";
            this.Elő_Lehívás.Size = new System.Drawing.Size(40, 40);
            this.Elő_Lehívás.TabIndex = 233;
            this.ToolTip1.SetToolTip(this.Elő_Lehívás, "Az aktuális napra ütemezett státusú karbantartásokat beírja a karbantartási adato" +
        "kba.");
            this.Elő_Lehívás.UseVisualStyleBackColor = true;
            this.Elő_Lehívás.Click += new System.EventHandler(this.Elő_Lehívás_Click);
            // 
            // Elő_Excel
            // 
            this.Elő_Excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Elő_Excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Elő_Excel.Location = new System.Drawing.Point(757, 78);
            this.Elő_Excel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Elő_Excel.Name = "Elő_Excel";
            this.Elő_Excel.Size = new System.Drawing.Size(40, 40);
            this.Elő_Excel.TabIndex = 215;
            this.ToolTip1.SetToolTip(this.Elő_Excel, "Táblazatban szereplő adatok exportálása Excelbe.");
            this.Elő_Excel.UseVisualStyleBackColor = true;
            this.Elő_Excel.Click += new System.EventHandler(this.Elő_Excel_Click);
            // 
            // Elő_havi
            // 
            this.Elő_havi.BackgroundImage = global::Villamos.Properties.Resources.Aha_Soft_Large_Seo_SEO;
            this.Elő_havi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Elő_havi.Location = new System.Drawing.Point(921, 78);
            this.Elő_havi.Name = "Elő_havi";
            this.Elő_havi.Size = new System.Drawing.Size(40, 40);
            this.Elő_havi.TabIndex = 231;
            this.ToolTip1.SetToolTip(this.Elő_havi, "Formázott előtervet készít Excel fájlban.");
            this.Elő_havi.UseVisualStyleBackColor = true;
            this.Elő_havi.Click += new System.EventHandler(this.Elő_havi_Click);
            // 
            // ELő_törlés
            // 
            this.ELő_törlés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.ELő_törlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ELő_törlés.Location = new System.Drawing.Point(711, 78);
            this.ELő_törlés.Name = "ELő_törlés";
            this.ELő_törlés.Size = new System.Drawing.Size(40, 40);
            this.ELő_törlés.TabIndex = 229;
            this.ToolTip1.SetToolTip(this.ELő_törlés, "Törli az előtervet");
            this.ELő_törlés.UseVisualStyleBackColor = true;
            this.ELő_törlés.Click += new System.EventHandler(this.ELő_törlés_Click);
            // 
            // Elő_Visszacsuk
            // 
            this.Elő_Visszacsuk.BackgroundImage = global::Villamos.Properties.Resources.fel;
            this.Elő_Visszacsuk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Elő_Visszacsuk.Location = new System.Drawing.Point(301, 76);
            this.Elő_Visszacsuk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Elő_Visszacsuk.Name = "Elő_Visszacsuk";
            this.Elő_Visszacsuk.Size = new System.Drawing.Size(40, 40);
            this.Elő_Visszacsuk.TabIndex = 228;
            this.ToolTip1.SetToolTip(this.Elő_Visszacsuk, "Pályaszám lista egy soros megjelenítése");
            this.Elő_Visszacsuk.UseVisualStyleBackColor = true;
            this.Elő_Visszacsuk.Click += new System.EventHandler(this.Elő_Visszacsuk_Click);
            // 
            // Elő_Mindtöröl
            // 
            this.Elő_Mindtöröl.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.Elő_Mindtöröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Elő_Mindtöröl.Location = new System.Drawing.Point(387, 76);
            this.Elő_Mindtöröl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Elő_Mindtöröl.Name = "Elő_Mindtöröl";
            this.Elő_Mindtöröl.Size = new System.Drawing.Size(40, 40);
            this.Elő_Mindtöröl.TabIndex = 227;
            this.ToolTip1.SetToolTip(this.Elő_Mindtöröl, "Minden kijelölés megszüntetése");
            this.Elő_Mindtöröl.UseVisualStyleBackColor = true;
            this.Elő_Mindtöröl.Click += new System.EventHandler(this.Elő_Mindtöröl_Click);
            // 
            // Elő_Összeskijelöl
            // 
            this.Elő_Összeskijelöl.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.Elő_Összeskijelöl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Elő_Összeskijelöl.Location = new System.Drawing.Point(344, 76);
            this.Elő_Összeskijelöl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Elő_Összeskijelöl.Name = "Elő_Összeskijelöl";
            this.Elő_Összeskijelöl.Size = new System.Drawing.Size(40, 40);
            this.Elő_Összeskijelöl.TabIndex = 226;
            this.ToolTip1.SetToolTip(this.Elő_Összeskijelöl, "Minden Pályaszám kijelölése");
            this.Elő_Összeskijelöl.UseVisualStyleBackColor = true;
            this.Elő_Összeskijelöl.Click += new System.EventHandler(this.Elő_Összeskijelöl_Click);
            // 
            // Elő_Lenyit
            // 
            this.Elő_Lenyit.BackgroundImage = global::Villamos.Properties.Resources.le;
            this.Elő_Lenyit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Elő_Lenyit.Location = new System.Drawing.Point(261, 76);
            this.Elő_Lenyit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Elő_Lenyit.Name = "Elő_Lenyit";
            this.Elő_Lenyit.Size = new System.Drawing.Size(40, 40);
            this.Elő_Lenyit.TabIndex = 225;
            this.ToolTip1.SetToolTip(this.Elő_Lenyit, "Pályaszám lista nagy méretben történő megjelenítése");
            this.Elő_Lenyit.UseVisualStyleBackColor = true;
            this.Elő_Lenyit.Click += new System.EventHandler(this.Elő_Click);
            // 
            // Tábla_frissítés
            // 
            this.Tábla_frissítés.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Tábla_frissítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Tábla_frissítés.Location = new System.Drawing.Point(619, 78);
            this.Tábla_frissítés.Name = "Tábla_frissítés";
            this.Tábla_frissítés.Size = new System.Drawing.Size(40, 40);
            this.Tábla_frissítés.TabIndex = 220;
            this.ToolTip1.SetToolTip(this.Tábla_frissítés, "Frissíti a táblázat adatait");
            this.Tábla_frissítés.UseVisualStyleBackColor = true;
            this.Tábla_frissítés.Click += new System.EventHandler(this.Tábla_frissítés_Click);
            // 
            // Előtervet_készít
            // 
            this.Előtervet_készít.BackgroundImage = global::Villamos.Properties.Resources._0;
            this.Előtervet_készít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Előtervet_készít.Location = new System.Drawing.Point(665, 78);
            this.Előtervet_készít.Name = "Előtervet_készít";
            this.Előtervet_készít.Size = new System.Drawing.Size(40, 40);
            this.Előtervet_készít.TabIndex = 218;
            this.ToolTip1.SetToolTip(this.Előtervet_készít, "Elkészíti az előtervet");
            this.Előtervet_készít.UseVisualStyleBackColor = true;
            this.Előtervet_készít.Click += new System.EventHandler(this.Előtervet_készít_Click);
            // 
            // Elő_tervező_telephely
            // 
            this.Elő_tervező_telephely.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Elő_tervező_telephely.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Elő_tervező_telephely.Location = new System.Drawing.Point(390, 6);
            this.Elő_tervező_telephely.Name = "Elő_tervező_telephely";
            this.Elő_tervező_telephely.Size = new System.Drawing.Size(45, 45);
            this.Elő_tervező_telephely.TabIndex = 156;
            this.ToolTip1.SetToolTip(this.Elő_tervező_telephely, "Telephely járműveit betölti a pályaszám mezőbe");
            this.Elő_tervező_telephely.UseVisualStyleBackColor = true;
            this.Elő_tervező_telephely.Click += new System.EventHandler(this.Elő_tervező_telephely_Click);
            // 
            // Elő_ütemez
            // 
            this.Elő_ütemez.BackgroundImage = global::Villamos.Properties.Resources.Document_preferences;
            this.Elő_ütemez.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Elő_ütemez.Location = new System.Drawing.Point(804, 78);
            this.Elő_ütemez.Name = "Elő_ütemez";
            this.Elő_ütemez.Size = new System.Drawing.Size(40, 40);
            this.Elő_ütemez.TabIndex = 232;
            this.ToolTip1.SetToolTip(this.Elő_ütemez, "A listázott elemeket átállítja tervezettről Ütemezettre");
            this.Elő_ütemez.UseVisualStyleBackColor = true;
            this.Elő_ütemez.Click += new System.EventHandler(this.Elő_ütemez_Click);
            // 
            // Segédablak_hívó
            // 
            this.Segédablak_hívó.BackgroundImage = global::Villamos.Properties.Resources.BeCardStack;
            this.Segédablak_hívó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Segédablak_hívó.Location = new System.Drawing.Point(980, 78);
            this.Segédablak_hívó.Name = "Segédablak_hívó";
            this.Segédablak_hívó.Size = new System.Drawing.Size(40, 40);
            this.Segédablak_hívó.TabIndex = 234;
            this.ToolTip1.SetToolTip(this.Segédablak_hívó, "Ütemezési segédablak");
            this.Segédablak_hívó.UseVisualStyleBackColor = true;
            this.Segédablak_hívó.Click += new System.EventHandler(this.Segédablak_hívó_Click);
            // 
            // Elő_törölt
            // 
            this.Elő_törölt.AutoSize = true;
            this.Elő_törölt.BackColor = System.Drawing.Color.BurlyWood;
            this.Elő_törölt.Location = new System.Drawing.Point(440, 65);
            this.Elő_törölt.Name = "Elő_törölt";
            this.Elő_törölt.Size = new System.Drawing.Size(85, 24);
            this.Elő_törölt.TabIndex = 230;
            this.Elő_törölt.Text = "Töröltek";
            this.Elő_törölt.UseVisualStyleBackColor = false;
            // 
            // Elő_pályaszám
            // 
            this.Elő_pályaszám.CheckOnClick = true;
            this.Elő_pályaszám.FormattingEnabled = true;
            this.Elő_pályaszám.Location = new System.Drawing.Point(135, 91);
            this.Elő_pályaszám.Name = "Elő_pályaszám";
            this.Elő_pályaszám.Size = new System.Drawing.Size(120, 25);
            this.Elő_pályaszám.TabIndex = 224;
            // 
            // Elő_Idő
            // 
            this.Elő_Idő.AutoSize = true;
            this.Elő_Idő.BackColor = System.Drawing.Color.BurlyWood;
            this.Elő_Idő.Location = new System.Drawing.Point(507, 92);
            this.Elő_Idő.Name = "Elő_Idő";
            this.Elő_Idő.Size = new System.Drawing.Size(50, 24);
            this.Elő_Idő.TabIndex = 223;
            this.Elő_Idő.Text = "Idő";
            this.Elő_Idő.UseVisualStyleBackColor = false;
            // 
            // Elő_Km
            // 
            this.Elő_Km.AutoSize = true;
            this.Elő_Km.BackColor = System.Drawing.Color.BurlyWood;
            this.Elő_Km.Location = new System.Drawing.Point(563, 92);
            this.Elő_Km.Name = "Elő_Km";
            this.Elő_Km.Size = new System.Drawing.Size(50, 24);
            this.Elő_Km.TabIndex = 222;
            this.Elő_Km.Text = "Km";
            this.Elő_Km.UseVisualStyleBackColor = false;
            // 
            // Elő_Mind
            // 
            this.Elő_Mind.AutoSize = true;
            this.Elő_Mind.BackColor = System.Drawing.Color.BurlyWood;
            this.Elő_Mind.Checked = true;
            this.Elő_Mind.Location = new System.Drawing.Point(440, 92);
            this.Elő_Mind.Name = "Elő_Mind";
            this.Elő_Mind.Size = new System.Drawing.Size(61, 24);
            this.Elő_Mind.TabIndex = 221;
            this.Elő_Mind.TabStop = true;
            this.Elő_Mind.Text = "Mind";
            this.Elő_Mind.UseVisualStyleBackColor = false;
            // 
            // Label21
            // 
            this.Label21.AutoSize = true;
            this.Label21.Location = new System.Drawing.Point(135, 66);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(89, 20);
            this.Label21.TabIndex = 219;
            this.Label21.Text = "Pályaszám:";
            // 
            // Elő_Dátumig
            // 
            this.Elő_Dátumig.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Elő_Dátumig.Location = new System.Drawing.Point(10, 90);
            this.Elő_Dátumig.Name = "Elő_Dátumig";
            this.Elő_Dátumig.Size = new System.Drawing.Size(119, 26);
            this.Elő_Dátumig.TabIndex = 217;
            // 
            // Elő_Dátumtól
            // 
            this.Elő_Dátumtól.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Elő_Dátumtól.Location = new System.Drawing.Point(10, 60);
            this.Elő_Dátumtól.Name = "Elő_Dátumtól";
            this.Elő_Dátumtól.Size = new System.Drawing.Size(119, 26);
            this.Elő_Dátumtól.TabIndex = 216;
            // 
            // Tábla_elő
            // 
            this.Tábla_elő.AllowUserToAddRows = false;
            this.Tábla_elő.AllowUserToDeleteRows = false;
            this.Tábla_elő.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla_elő.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Tábla_elő.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla_elő.Location = new System.Drawing.Point(10, 124);
            this.Tábla_elő.Name = "Tábla_elő";
            this.Tábla_elő.ReadOnly = true;
            this.Tábla_elő.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Tábla_elő.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.Tábla_elő.Size = new System.Drawing.Size(1250, 200);
            this.Tábla_elő.TabIndex = 214;
            this.Tábla_elő.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_elő_CellClick);
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1216, 11);
            this.BtnSúgó.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(40, 40);
            this.BtnSúgó.TabIndex = 153;
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            // 
            // km_modosit_btn
            // 
            this.km_modosit_btn.BackgroundImage = global::Villamos.Properties.Resources.Button_Forward_01;
            this.km_modosit_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.km_modosit_btn.Location = new System.Drawing.Point(1118, 79);
            this.km_modosit_btn.Name = "km_modosit_btn";
            this.km_modosit_btn.Size = new System.Drawing.Size(40, 40);
            this.km_modosit_btn.TabIndex = 241;
            this.ToolTip1.SetToolTip(this.km_modosit_btn, "Excel tábla színkezelés");
            this.km_modosit_btn.UseVisualStyleBackColor = true;
            this.km_modosit_btn.Click += new System.EventHandler(this.km_modosit_btn_Click);
            // 
            // Ablak_Tulajdonságok_CAF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.BurlyWood;
            this.ClientSize = new System.Drawing.Size(1268, 333);
            this.Controls.Add(this.km_modosit_btn);
            this.Controls.Add(this.Caf_Listák);
            this.Controls.Add(this.Színbeállítás);
            this.Controls.Add(this.Alap_adatok);
            this.Controls.Add(this.Button3);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Elő_törölt);
            this.Controls.Add(this.Segédablak_hívó);
            this.Controls.Add(this.Elő_Lehívás);
            this.Controls.Add(this.Elő_ütemez);
            this.Controls.Add(this.Elő_Excel);
            this.Controls.Add(this.Elő_havi);
            this.Controls.Add(this.ELő_törlés);
            this.Controls.Add(this.Elő_Visszacsuk);
            this.Controls.Add(this.Elő_Mindtöröl);
            this.Controls.Add(this.Elő_Összeskijelöl);
            this.Controls.Add(this.Elő_Lenyit);
            this.Controls.Add(this.Elő_pályaszám);
            this.Controls.Add(this.Elő_Idő);
            this.Controls.Add(this.Elő_Km);
            this.Controls.Add(this.Elő_Mind);
            this.Controls.Add(this.Label21);
            this.Controls.Add(this.Elő_Dátumig);
            this.Controls.Add(this.Elő_Dátumtól);
            this.Controls.Add(this.Tábla_elő);
            this.Controls.Add(this.Tábla_frissítés);
            this.Controls.Add(this.Előtervet_készít);
            this.Controls.Add(this.Elő_tervező_telephely);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.Panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Tulajdonságok_CAF";
            this.Text = "CAF5-CAF9 vizsgálati adatok";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_Tulajdonságok_CAF_FormClosed);
            this.Load += new System.EventHandler(this.Ablak_Tulajdonságok_CAF_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_elő)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal Button BtnSúgó;
        internal Panel Panel1;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal Button Elő_tervező_telephely;
        internal ToolTip ToolTip1;
        internal Button Caf_Listák;
        internal Button Színbeállítás;
        internal Button Alap_adatok;
        internal Button Button3;
        internal Button Button2;
        internal CheckBox Elő_törölt;
        internal Button Segédablak_hívó;
        internal Button Elő_Lehívás;
        internal Button Elő_ütemez;
        internal Button Elő_Excel;
        internal Button Elő_havi;
        internal Button ELő_törlés;
        internal Button Elő_Visszacsuk;
        internal Button Elő_Mindtöröl;
        internal Button Elő_Összeskijelöl;
        internal Button Elő_Lenyit;
        internal CheckedListBox Elő_pályaszám;
        internal RadioButton Elő_Idő;
        internal RadioButton Elő_Km;
        internal RadioButton Elő_Mind;
        internal Label Label21;
        internal DateTimePicker Elő_Dátumig;
        internal DateTimePicker Elő_Dátumtól;
        internal DataGridView Tábla_elő;
        internal Button Tábla_frissítés;
        internal Button Előtervet_készít;
        private Timer timer1;
        internal Button km_modosit_btn;
    }
}