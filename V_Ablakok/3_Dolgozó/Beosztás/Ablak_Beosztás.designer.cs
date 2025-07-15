using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
    public partial class Ablak_Beosztás : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Beosztás));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Kilépettjel = new System.Windows.Forms.CheckBox();
            this.Dátum = new System.Windows.Forms.DateTimePicker();
            this.Csoport = new System.Windows.Forms.CheckedListBox();
            this.Váltósbeosztás = new System.Windows.Forms.CheckBox();
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.Dolgozónév = new System.Windows.Forms.CheckedListBox();
            this.Dolgozóneve = new System.Windows.Forms.Label();
            this.NapKiválaszt = new System.Windows.Forms.Label();
            this.Hrazonosító = new System.Windows.Forms.Label();
            this.Nyolcórás = new System.Windows.Forms.ComboBox();
            this.Tizenkétórás = new System.Windows.Forms.ComboBox();
            this.Minden = new System.Windows.Forms.ComboBox();
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.Ledolgozottidő = new System.Windows.Forms.Label();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Gomb_nappalos = new System.Windows.Forms.Button();
            this.Előzmény = new System.Windows.Forms.Button();
            this.Adatok_egyeztetése = new System.Windows.Forms.Button();
            this.Váltós = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.CsukDolgozó = new System.Windows.Forms.Button();
            this.CsukCsoport = new System.Windows.Forms.Button();
            this.Chk_CTRL = new System.Windows.Forms.CheckBox();
            this.Kiegészítő_Doboz = new System.Windows.Forms.Button();
            this.DolgozóFrissít = new System.Windows.Forms.Button();
            this.Dolgozóvissza = new System.Windows.Forms.Button();
            this.Dolgozókijelölmind = new System.Windows.Forms.Button();
            this.NyitDolgozó = new System.Windows.Forms.Button();
            this.Csoportvissza = new System.Windows.Forms.Button();
            this.Csoportkijelölmind = new System.Windows.Forms.Button();
            this.NyitCsoport = new System.Windows.Forms.Button();
            this.CsoportFrissít = new System.Windows.Forms.Button();
            this.Excel_gomb = new System.Windows.Forms.Button();
            this.Súgó = new System.Windows.Forms.Button();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.Cmbtelephely);
            this.Panel1.Controls.Add(this.Label13);
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(374, 32);
            this.Panel1.TabIndex = 57;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(176, 2);
            this.Cmbtelephely.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
            this.Cmbtelephely.SelectedIndexChanged += new System.EventHandler(this.Cmbtelephely_SelectedIndexChanged);
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
            // Kilépettjel
            // 
            this.Kilépettjel.AutoSize = true;
            this.Kilépettjel.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.Kilépettjel.Location = new System.Drawing.Point(568, 96);
            this.Kilépettjel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Kilépettjel.Name = "Kilépettjel";
            this.Kilépettjel.Size = new System.Drawing.Size(169, 24);
            this.Kilépettjel.TabIndex = 109;
            this.Kilépettjel.Text = "Kilépett dolgozókkal";
            this.Kilépettjel.UseVisualStyleBackColor = false;
            this.Kilépettjel.CheckedChanged += new System.EventHandler(this.Kilépettjel_CheckedChanged);
            // 
            // Dátum
            // 
            this.Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum.Location = new System.Drawing.Point(389, 8);
            this.Dátum.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Dátum.Name = "Dátum";
            this.Dátum.Size = new System.Drawing.Size(110, 26);
            this.Dátum.TabIndex = 107;
            this.Dátum.ValueChanged += new System.EventHandler(this.Dátum_ValueChanged);
            // 
            // Csoport
            // 
            this.Csoport.CheckOnClick = true;
            this.Csoport.FormattingEnabled = true;
            this.Csoport.Location = new System.Drawing.Point(12, 102);
            this.Csoport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Csoport.Name = "Csoport";
            this.Csoport.Size = new System.Drawing.Size(371, 25);
            this.Csoport.TabIndex = 111;
            // 
            // Váltósbeosztás
            // 
            this.Váltósbeosztás.AutoSize = true;
            this.Váltósbeosztás.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.Váltósbeosztás.Location = new System.Drawing.Point(766, 96);
            this.Váltósbeosztás.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Váltósbeosztás.Name = "Váltósbeosztás";
            this.Váltósbeosztás.Size = new System.Drawing.Size(162, 24);
            this.Váltósbeosztás.TabIndex = 121;
            this.Váltósbeosztás.Text = "Váltós beosztással";
            this.Váltósbeosztás.UseVisualStyleBackColor = false;
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            this.Tábla.AllowUserToResizeColumns = false;
            this.Tábla.AllowUserToResizeRows = false;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.MediumSeaGreen;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Tábla.ColumnHeadersHeight = 25;
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.Tábla.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.Tábla.EnableHeadersVisualStyles = false;
            this.Tábla.Location = new System.Drawing.Point(5, 140);
            this.Tábla.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Tábla.MultiSelect = false;
            this.Tábla.Name = "Tábla";
            this.Tábla.RowHeadersVisible = false;
            this.Tábla.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Tábla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Tábla.Size = new System.Drawing.Size(1191, 339);
            this.Tábla.TabIndex = 122;
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            this.Tábla.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Tábla_Scroll);
            this.Tábla.Sorted += new System.EventHandler(this.Tábla_Sorted);
            this.Tábla.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Tábla_KeyDown);
            // 
            // Dolgozónév
            // 
            this.Dolgozónév.CheckOnClick = true;
            this.Dolgozónév.FormattingEnabled = true;
            this.Dolgozónév.Location = new System.Drawing.Point(12, 56);
            this.Dolgozónév.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Dolgozónév.Name = "Dolgozónév";
            this.Dolgozónév.Size = new System.Drawing.Size(371, 25);
            this.Dolgozónév.TabIndex = 128;
            // 
            // Dolgozóneve
            // 
            this.Dolgozóneve.AutoSize = true;
            this.Dolgozóneve.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.Dolgozóneve.Location = new System.Drawing.Point(1032, 12);
            this.Dolgozóneve.Name = "Dolgozóneve";
            this.Dolgozóneve.Size = new System.Drawing.Size(18, 20);
            this.Dolgozóneve.TabIndex = 130;
            this.Dolgozóneve.Text = "_";
            // 
            // NapKiválaszt
            // 
            this.NapKiválaszt.AutoSize = true;
            this.NapKiválaszt.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.NapKiválaszt.Location = new System.Drawing.Point(1032, 69);
            this.NapKiválaszt.Name = "NapKiválaszt";
            this.NapKiválaszt.Size = new System.Drawing.Size(18, 20);
            this.NapKiválaszt.TabIndex = 131;
            this.NapKiválaszt.Text = "_";
            // 
            // Hrazonosító
            // 
            this.Hrazonosító.AutoSize = true;
            this.Hrazonosító.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.Hrazonosító.Location = new System.Drawing.Point(1032, 40);
            this.Hrazonosító.Name = "Hrazonosító";
            this.Hrazonosító.Size = new System.Drawing.Size(18, 20);
            this.Hrazonosító.TabIndex = 132;
            this.Hrazonosító.Text = "_";
            // 
            // Nyolcórás
            // 
            this.Nyolcórás.BackColor = System.Drawing.Color.Gold;
            this.Nyolcórás.DropDownHeight = 350;
            this.Nyolcórás.FormattingEnabled = true;
            this.Nyolcórás.IntegralHeight = false;
            this.Nyolcórás.Location = new System.Drawing.Point(901, 308);
            this.Nyolcórás.Name = "Nyolcórás";
            this.Nyolcórás.Size = new System.Drawing.Size(220, 28);
            this.Nyolcórás.TabIndex = 133;
            this.Nyolcórás.Visible = false;
            this.Nyolcórás.SelectedIndexChanged += new System.EventHandler(this.Nyolcórás_SelectedIndexChanged);
            this.Nyolcórás.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Nyolcórás_KeyDown);
            this.Nyolcórás.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Nyolcórás_MouseDown);
            // 
            // Tizenkétórás
            // 
            this.Tizenkétórás.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.Tizenkétórás.DropDownHeight = 350;
            this.Tizenkétórás.FormattingEnabled = true;
            this.Tizenkétórás.IntegralHeight = false;
            this.Tizenkétórás.Location = new System.Drawing.Point(901, 234);
            this.Tizenkétórás.Name = "Tizenkétórás";
            this.Tizenkétórás.Size = new System.Drawing.Size(220, 28);
            this.Tizenkétórás.TabIndex = 134;
            this.Tizenkétórás.Visible = false;
            this.Tizenkétórás.SelectedIndexChanged += new System.EventHandler(this.Tizenkétórás_SelectedIndexChanged);
            this.Tizenkétórás.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Tizenkétórás_KeyDown);
            this.Tizenkétórás.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Tizenkétórás_MouseDown);
            // 
            // Minden
            // 
            this.Minden.BackColor = System.Drawing.Color.DodgerBlue;
            this.Minden.DropDownHeight = 350;
            this.Minden.FormattingEnabled = true;
            this.Minden.IntegralHeight = false;
            this.Minden.Location = new System.Drawing.Point(901, 274);
            this.Minden.Name = "Minden";
            this.Minden.Size = new System.Drawing.Size(220, 28);
            this.Minden.TabIndex = 135;
            this.Minden.Visible = false;
            this.Minden.SelectedIndexChanged += new System.EventHandler(this.Minden_SelectedIndexChanged);
            this.Minden.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Minden_KeyDown);
            this.Minden.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Minden_MouseDown);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.BackColor = System.Drawing.Color.Lime;
            this.Holtart.ForeColor = System.Drawing.Color.MediumBlue;
            this.Holtart.Location = new System.Drawing.Point(16, 158);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(1160, 31);
            this.Holtart.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.Holtart.TabIndex = 149;
            this.Holtart.Visible = false;
            // 
            // Ledolgozottidő
            // 
            this.Ledolgozottidő.AutoSize = true;
            this.Ledolgozottidő.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.Ledolgozottidő.Location = new System.Drawing.Point(1032, 96);
            this.Ledolgozottidő.Name = "Ledolgozottidő";
            this.Ledolgozottidő.Size = new System.Drawing.Size(18, 20);
            this.Ledolgozottidő.TabIndex = 155;
            this.Ledolgozottidő.Text = "_";
            this.Ledolgozottidő.Visible = false;
            // 
            // Gomb_nappalos
            // 
            this.Gomb_nappalos.BackgroundImage = global::Villamos.Properties.Resources.felhasználók32;
            this.Gomb_nappalos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Gomb_nappalos.Location = new System.Drawing.Point(754, 41);
            this.Gomb_nappalos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Gomb_nappalos.Name = "Gomb_nappalos";
            this.Gomb_nappalos.Size = new System.Drawing.Size(40, 40);
            this.Gomb_nappalos.TabIndex = 167;
            this.ToolTip1.SetToolTip(this.Gomb_nappalos, "Nappalos beosztás adatok előre rögzítése\r\nCsak ÜRES beosztás esetén használható.");
            this.Gomb_nappalos.UseVisualStyleBackColor = true;
            this.Gomb_nappalos.Click += new System.EventHandler(this.Gomb_nappalos_Click);
            // 
            // Előzmény
            // 
            this.Előzmény.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Előzmény.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Előzmény.Location = new System.Drawing.Point(846, 41);
            this.Előzmény.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Előzmény.Name = "Előzmény";
            this.Előzmény.Size = new System.Drawing.Size(40, 40);
            this.Előzmény.TabIndex = 163;
            this.ToolTip1.SetToolTip(this.Előzmény, "Törli a beosztást és a háttér adatokat");
            this.Előzmény.UseVisualStyleBackColor = true;
            this.Előzmény.Visible = false;
            this.Előzmény.Click += new System.EventHandler(this.Előzmény_Click);
            // 
            // Adatok_egyeztetése
            // 
            this.Adatok_egyeztetése.BackgroundImage = global::Villamos.Properties.Resources.Yellow_Glass_Folders_Icon_47;
            this.Adatok_egyeztetése.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Adatok_egyeztetése.Location = new System.Drawing.Point(708, 40);
            this.Adatok_egyeztetése.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Adatok_egyeztetése.Name = "Adatok_egyeztetése";
            this.Adatok_egyeztetése.Size = new System.Drawing.Size(40, 40);
            this.Adatok_egyeztetése.TabIndex = 141;
            this.ToolTip1.SetToolTip(this.Adatok_egyeztetése, "Beosztás adatok és a Gyűjtő adatok ellenőrzése");
            this.Adatok_egyeztetése.UseVisualStyleBackColor = true;
            this.Adatok_egyeztetése.Click += new System.EventHandler(this.Adatok_egyeztetése_Click);
            // 
            // Váltós
            // 
            this.Váltós.BackgroundImage = global::Villamos.Properties.Resources.CALENDR1;
            this.Váltós.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Váltós.Location = new System.Drawing.Point(800, 40);
            this.Váltós.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Váltós.Name = "Váltós";
            this.Váltós.Size = new System.Drawing.Size(40, 40);
            this.Váltós.TabIndex = 127;
            this.ToolTip1.SetToolTip(this.Váltós, "Éves beosztást feltölti a kijelölt váltós dolgozóknak\r\nCsak ÜRES beosztás esetén " +
        "használható.");
            this.Váltós.UseVisualStyleBackColor = true;
            this.Váltós.Visible = false;
            this.Váltós.Click += new System.EventHandler(this.Váltós_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(0, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 0;
            // 
            // CsukDolgozó
            // 
            this.CsukDolgozó.BackgroundImage = global::Villamos.Properties.Resources.fel;
            this.CsukDolgozó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CsukDolgozó.Location = new System.Drawing.Point(389, 41);
            this.CsukDolgozó.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CsukDolgozó.Name = "CsukDolgozó";
            this.CsukDolgozó.Size = new System.Drawing.Size(40, 40);
            this.CsukDolgozó.TabIndex = 117;
            this.CsukDolgozó.UseVisualStyleBackColor = true;
            this.CsukDolgozó.Visible = false;
            this.CsukDolgozó.Click += new System.EventHandler(this.Csukdolgozó_Click);
            // 
            // CsukCsoport
            // 
            this.CsukCsoport.BackgroundImage = global::Villamos.Properties.Resources.fel;
            this.CsukCsoport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CsukCsoport.Location = new System.Drawing.Point(389, 87);
            this.CsukCsoport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CsukCsoport.Name = "CsukCsoport";
            this.CsukCsoport.Size = new System.Drawing.Size(40, 40);
            this.CsukCsoport.TabIndex = 113;
            this.CsukCsoport.UseVisualStyleBackColor = true;
            this.CsukCsoport.Visible = false;
            this.CsukCsoport.Click += new System.EventHandler(this.CsukCsoport_Click);
            // 
            // Chk_CTRL
            // 
            this.Chk_CTRL.AutoSize = true;
            this.Chk_CTRL.Location = new System.Drawing.Point(535, 8);
            this.Chk_CTRL.Name = "Chk_CTRL";
            this.Chk_CTRL.Size = new System.Drawing.Size(127, 24);
            this.Chk_CTRL.TabIndex = 166;
            this.Chk_CTRL.Text = "CTRL nyomva";
            this.Chk_CTRL.UseVisualStyleBackColor = true;
            this.Chk_CTRL.Visible = false;
            // 
            // Kiegészítő_Doboz
            // 
            this.Kiegészítő_Doboz.BackgroundImage = global::Villamos.Properties.Resources.BeCardStack;
            this.Kiegészítő_Doboz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Kiegészítő_Doboz.Location = new System.Drawing.Point(584, 41);
            this.Kiegészítő_Doboz.Name = "Kiegészítő_Doboz";
            this.Kiegészítő_Doboz.Size = new System.Drawing.Size(40, 40);
            this.Kiegészítő_Doboz.TabIndex = 139;
            this.ToolTip1.SetToolTip(this.Kiegészítő_Doboz, "Adott napi kiegészítő információk beviteléhez szükséges segédablakot jeleníti meg" +
        "");
            this.Kiegészítő_Doboz.UseVisualStyleBackColor = true;
            this.Kiegészítő_Doboz.Click += new System.EventHandler(this.Kiegészítő_Doboz_Click);
            // 
            // DolgozóFrissít
            // 
            this.DolgozóFrissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.DolgozóFrissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DolgozóFrissít.Location = new System.Drawing.Point(522, 41);
            this.DolgozóFrissít.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DolgozóFrissít.Name = "DolgozóFrissít";
            this.DolgozóFrissít.Size = new System.Drawing.Size(40, 40);
            this.DolgozóFrissít.TabIndex = 120;
            this.ToolTip1.SetToolTip(this.DolgozóFrissít, "Listáz");
            this.DolgozóFrissít.UseVisualStyleBackColor = true;
            this.DolgozóFrissít.Click += new System.EventHandler(this.DolgozóFrissít_Click);
            // 
            // Dolgozóvissza
            // 
            this.Dolgozóvissza.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.Dolgozóvissza.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Dolgozóvissza.Location = new System.Drawing.Point(476, 41);
            this.Dolgozóvissza.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Dolgozóvissza.Name = "Dolgozóvissza";
            this.Dolgozóvissza.Size = new System.Drawing.Size(40, 40);
            this.Dolgozóvissza.TabIndex = 119;
            this.ToolTip1.SetToolTip(this.Dolgozóvissza, "A kijelöléseket eltávolítja");
            this.Dolgozóvissza.UseVisualStyleBackColor = true;
            this.Dolgozóvissza.Click += new System.EventHandler(this.Dolgozóvissza_Click);
            // 
            // Dolgozókijelölmind
            // 
            this.Dolgozókijelölmind.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.Dolgozókijelölmind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Dolgozókijelölmind.Location = new System.Drawing.Point(435, 41);
            this.Dolgozókijelölmind.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Dolgozókijelölmind.Name = "Dolgozókijelölmind";
            this.Dolgozókijelölmind.Size = new System.Drawing.Size(40, 40);
            this.Dolgozókijelölmind.TabIndex = 118;
            this.ToolTip1.SetToolTip(this.Dolgozókijelölmind, "Mindent kijelöl");
            this.Dolgozókijelölmind.UseVisualStyleBackColor = true;
            this.Dolgozókijelölmind.Click += new System.EventHandler(this.Dolgozókijelölmind_Click);
            // 
            // NyitDolgozó
            // 
            this.NyitDolgozó.BackgroundImage = global::Villamos.Properties.Resources.le;
            this.NyitDolgozó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.NyitDolgozó.Location = new System.Drawing.Point(389, 43);
            this.NyitDolgozó.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NyitDolgozó.Name = "NyitDolgozó";
            this.NyitDolgozó.Size = new System.Drawing.Size(40, 40);
            this.NyitDolgozó.TabIndex = 116;
            this.NyitDolgozó.UseVisualStyleBackColor = true;
            this.NyitDolgozó.Click += new System.EventHandler(this.Nyitdolgozó_Click);
            // 
            // Csoportvissza
            // 
            this.Csoportvissza.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.Csoportvissza.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Csoportvissza.Location = new System.Drawing.Point(476, 87);
            this.Csoportvissza.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Csoportvissza.Name = "Csoportvissza";
            this.Csoportvissza.Size = new System.Drawing.Size(40, 40);
            this.Csoportvissza.TabIndex = 115;
            this.ToolTip1.SetToolTip(this.Csoportvissza, "A kijelöléseket eltávolítja");
            this.Csoportvissza.UseVisualStyleBackColor = true;
            this.Csoportvissza.Click += new System.EventHandler(this.Csoportvissza_Click);
            // 
            // Csoportkijelölmind
            // 
            this.Csoportkijelölmind.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.Csoportkijelölmind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Csoportkijelölmind.Location = new System.Drawing.Point(435, 87);
            this.Csoportkijelölmind.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Csoportkijelölmind.Name = "Csoportkijelölmind";
            this.Csoportkijelölmind.Size = new System.Drawing.Size(40, 40);
            this.Csoportkijelölmind.TabIndex = 114;
            this.ToolTip1.SetToolTip(this.Csoportkijelölmind, "Mindent kijelöl");
            this.Csoportkijelölmind.UseVisualStyleBackColor = true;
            this.Csoportkijelölmind.Click += new System.EventHandler(this.Csoportkijelölmind_Click);
            // 
            // NyitCsoport
            // 
            this.NyitCsoport.BackgroundImage = global::Villamos.Properties.Resources.le;
            this.NyitCsoport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.NyitCsoport.Location = new System.Drawing.Point(389, 87);
            this.NyitCsoport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NyitCsoport.Name = "NyitCsoport";
            this.NyitCsoport.Size = new System.Drawing.Size(40, 40);
            this.NyitCsoport.TabIndex = 112;
            this.NyitCsoport.UseVisualStyleBackColor = true;
            this.NyitCsoport.Click += new System.EventHandler(this.NyitCsoport_Click);
            // 
            // CsoportFrissít
            // 
            this.CsoportFrissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.CsoportFrissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CsoportFrissít.Location = new System.Drawing.Point(522, 87);
            this.CsoportFrissít.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CsoportFrissít.Name = "CsoportFrissít";
            this.CsoportFrissít.Size = new System.Drawing.Size(40, 40);
            this.CsoportFrissít.TabIndex = 110;
            this.ToolTip1.SetToolTip(this.CsoportFrissít, "Listáz");
            this.CsoportFrissít.UseVisualStyleBackColor = true;
            this.CsoportFrissít.Click += new System.EventHandler(this.CsoportFrissít_Click);
            // 
            // Excel_gomb
            // 
            this.Excel_gomb.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excel_gomb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excel_gomb.Location = new System.Drawing.Point(645, 40);
            this.Excel_gomb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Excel_gomb.Name = "Excel_gomb";
            this.Excel_gomb.Size = new System.Drawing.Size(40, 40);
            this.Excel_gomb.TabIndex = 106;
            this.ToolTip1.SetToolTip(this.Excel_gomb, "Excelt készít");
            this.Excel_gomb.UseVisualStyleBackColor = true;
            this.Excel_gomb.Click += new System.EventHandler(this.Excel_gomb_Click);
            // 
            // Súgó
            // 
            this.Súgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Súgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.Súgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Súgó.Location = new System.Drawing.Point(1151, 8);
            this.Súgó.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Súgó.Name = "Súgó";
            this.Súgó.Size = new System.Drawing.Size(40, 40);
            this.Súgó.TabIndex = 61;
            this.Súgó.UseVisualStyleBackColor = true;
            this.Súgó.Click += new System.EventHandler(this.Súgó_Click);
            // 
            // Ablak_Beosztás
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.ClientSize = new System.Drawing.Size(1200, 484);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.Gomb_nappalos);
            this.Controls.Add(this.Chk_CTRL);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Előzmény);
            this.Controls.Add(this.Ledolgozottidő);
            this.Controls.Add(this.Adatok_egyeztetése);
            this.Controls.Add(this.Kiegészítő_Doboz);
            this.Controls.Add(this.Minden);
            this.Controls.Add(this.Tizenkétórás);
            this.Controls.Add(this.Nyolcórás);
            this.Controls.Add(this.Dolgozónév);
            this.Controls.Add(this.Hrazonosító);
            this.Controls.Add(this.NapKiválaszt);
            this.Controls.Add(this.Dolgozóneve);
            this.Controls.Add(this.Váltós);
            this.Controls.Add(this.Váltósbeosztás);
            this.Controls.Add(this.DolgozóFrissít);
            this.Controls.Add(this.Dolgozóvissza);
            this.Controls.Add(this.Dolgozókijelölmind);
            this.Controls.Add(this.NyitDolgozó);
            this.Controls.Add(this.CsukDolgozó);
            this.Controls.Add(this.Csoportvissza);
            this.Controls.Add(this.Csoportkijelölmind);
            this.Controls.Add(this.NyitCsoport);
            this.Controls.Add(this.CsukCsoport);
            this.Controls.Add(this.CsoportFrissít);
            this.Controls.Add(this.Kilépettjel);
            this.Controls.Add(this.Dátum);
            this.Controls.Add(this.Excel_gomb);
            this.Controls.Add(this.Súgó);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Csoport);
            this.Controls.Add(this.Tábla);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Beosztás";
            this.Text = "Beosztás";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_Beosztás_FormClosed);
            this.Load += new System.EventHandler(this.Ablak_Beosztás_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ablak_Beosztás_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Ablak_Beosztás_KeyUp);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal Panel Panel1;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal Button Súgó;
        internal Button CsoportFrissít;
        internal CheckBox Kilépettjel;
        internal DateTimePicker Dátum;
        internal Button Excel_gomb;
        internal CheckedListBox Csoport;
        internal Button Csoportvissza;
        internal Button Csoportkijelölmind;
        internal Button NyitCsoport;
        internal Button CsukCsoport;
        internal Button Dolgozóvissza;
        internal Button Dolgozókijelölmind;
        internal Button NyitDolgozó;
        internal Button CsukDolgozó;
        internal Button DolgozóFrissít;
        internal CheckBox Váltósbeosztás;
        internal DataGridView Tábla;
        internal Button Váltós;
        internal CheckedListBox Dolgozónév;
        internal Label Dolgozóneve;
        internal Label NapKiválaszt;
        internal Label Hrazonosító;
        internal ComboBox Nyolcórás;
        internal ComboBox Tizenkétórás;
        internal ComboBox Minden;
        internal Button Kiegészítő_Doboz;
        internal Button Adatok_egyeztetése;
        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal Label Ledolgozottidő;
        internal ToolTip ToolTip1;
        internal Button Előzmény;
        internal CheckBox Chk_CTRL;
        internal Button Gomb_nappalos;
        internal Button button3;
    }
}