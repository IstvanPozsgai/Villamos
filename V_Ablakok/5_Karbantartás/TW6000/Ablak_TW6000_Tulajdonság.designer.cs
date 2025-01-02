using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
    public partial class Ablak_TW6000_Tulajdonság : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_TW6000_Tulajdonság));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Járműadatok_rögzít = new System.Windows.Forms.Button();
            this.Pályaszámkereső = new System.Windows.Forms.Button();
            this.BtnKarbantartExcel = new System.Windows.Forms.Button();
            this.BtnKarbantartFrissít = new System.Windows.Forms.Button();
            this.Keresés = new System.Windows.Forms.Button();
            this.Excelkimenet = new System.Windows.Forms.Button();
            this.Terv_lista = new System.Windows.Forms.Button();
            this.BtnÜtemÜtemezés = new System.Windows.Forms.Button();
            this.Telephely_lap = new System.Windows.Forms.Button();
            this.BtnSzínező = new System.Windows.Forms.Button();
            this.Ütemfrissít = new System.Windows.Forms.Button();
            this.BtnÜtemTörlés = new System.Windows.Forms.Button();
            this.BtnÜtemRészRögz = new System.Windows.Forms.Button();
            this.BtnÜtemRészTerv = new System.Windows.Forms.Button();
            this.BtnÜtemNaplóExcel = new System.Windows.Forms.Button();
            this.BtnÜtemNaplóFrissít = new System.Windows.Forms.Button();
            this.BtnElőtervezőFrissít = new System.Windows.Forms.Button();
            this.Mindentkijelöl = new System.Windows.Forms.Button();
            this.Kijelöléstörlése = new System.Windows.Forms.Button();
            this.BtnElőtervezőKeres = new System.Windows.Forms.Button();
            this.Holtart = new V_MindenEgyéb.MyProgressbar();
            this.Pá = new System.Windows.Forms.Label();
            this.Pályaszám = new System.Windows.Forms.ComboBox();
            this.TabPage6 = new System.Windows.Forms.TabPage();
            this.PszJelölő = new System.Windows.Forms.CheckedListBox();
            this.Panel6 = new System.Windows.Forms.Panel();
            this.Check1 = new System.Windows.Forms.CheckBox();
            this.Panel5 = new System.Windows.Forms.Panel();
            this.VizsgálatLista = new System.Windows.Forms.CheckedListBox();
            this.ElőCiklusrend = new System.Windows.Forms.ComboBox();
            this.Label28 = new System.Windows.Forms.Label();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.ElőbefejezőDátum = new System.Windows.Forms.DateTimePicker();
            this.Előkezdődátum = new System.Windows.Forms.DateTimePicker();
            this.Label27 = new System.Windows.Forms.Label();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.Telephely = new System.Windows.Forms.ComboBox();
            this.Label26 = new System.Windows.Forms.Label();
            this.TabPage5 = new System.Windows.Forms.TabPage();
            this.ÜtemNapló = new System.Windows.Forms.DataGridView();
            this.ÜtemPályaszám = new System.Windows.Forms.ComboBox();
            this.Label25 = new System.Windows.Forms.Label();
            this.ÜtemNaplóVége = new System.Windows.Forms.DateTimePicker();
            this.ÜtemNaplóKezdet = new System.Windows.Forms.DateTimePicker();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.Label24 = new System.Windows.Forms.Label();
            this.Label23 = new System.Windows.Forms.Label();
            this.Label22 = new System.Windows.Forms.Label();
            this.Label21 = new System.Windows.Forms.Label();
            this.Üstátus = new System.Windows.Forms.ComboBox();
            this.ÜVÜtemezés = new System.Windows.Forms.DateTimePicker();
            this.ÜVElkészülés = new System.Windows.Forms.DateTimePicker();
            this.Label20 = new System.Windows.Forms.Label();
            this.ÜVizsgfoka = new System.Windows.Forms.TextBox();
            this.ÜVVégezte = new System.Windows.Forms.ComboBox();
            this.Üazonosító = new System.Windows.Forms.TextBox();
            this.ÜCiklusrend = new System.Windows.Forms.ComboBox();
            this.Label19 = new System.Windows.Forms.Label();
            this.Label18 = new System.Windows.Forms.Label();
            this.Label17 = new System.Windows.Forms.Label();
            this.ÜMegjegyzés = new System.Windows.Forms.TextBox();
            this.Üelkészült = new System.Windows.Forms.CheckBox();
            this.ÜVSorszám = new System.Windows.Forms.ComboBox();
            this.ÜVEsedékesség = new System.Windows.Forms.DateTimePicker();
            this.Label15 = new System.Windows.Forms.Label();
            this.Label16 = new System.Windows.Forms.Label();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.Ütemvége = new System.Windows.Forms.DateTimePicker();
            this.Ütemkezdete = new System.Windows.Forms.DateTimePicker();
            this.Táblaütemezés = new System.Windows.Forms.DataGridView();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.Napló_Tábla = new System.Windows.Forms.DataGridView();
            this.NaplóPályaszám = new System.Windows.Forms.ComboBox();
            this.Label14 = new System.Windows.Forms.Label();
            this.NaplóVége = new System.Windows.Forms.DateTimePicker();
            this.NaplóKezdete = new System.Windows.Forms.DateTimePicker();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.Vizsgsorszám = new System.Windows.Forms.ComboBox();
            this.VizsgNév = new System.Windows.Forms.TextBox();
            this.Oka = new System.Windows.Forms.TextBox();
            this.KötöttStart = new System.Windows.Forms.CheckBox();
            this.Megállítás = new System.Windows.Forms.CheckBox();
            this.Ciklusrend = new System.Windows.Forms.ComboBox();
            this.Vizsgdátum = new System.Windows.Forms.DateTimePicker();
            this.Start = new System.Windows.Forms.DateTimePicker();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.LapFülek = new System.Windows.Forms.TabControl();
            this.Btn_súgó = new System.Windows.Forms.Button();
            this.Panel1.SuspendLayout();
            this.TabPage6.SuspendLayout();
            this.Panel6.SuspendLayout();
            this.Panel5.SuspendLayout();
            this.Panel4.SuspendLayout();
            this.Panel3.SuspendLayout();
            this.TabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ÜtemNapló)).BeginInit();
            this.TabPage4.SuspendLayout();
            this.TabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Táblaütemezés)).BeginInit();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Napló_Tábla)).BeginInit();
            this.TabPage1.SuspendLayout();
            this.LapFülek.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.Cmbtelephely);
            this.Panel1.Controls.Add(this.Label13);
            this.Panel1.Location = new System.Drawing.Point(2, 8);
            this.Panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(374, 42);
            this.Panel1.TabIndex = 58;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(176, 8);
            this.Cmbtelephely.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(12, 11);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(145, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // Járműadatok_rögzít
            // 
            this.Járműadatok_rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Járműadatok_rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Járműadatok_rögzít.Location = new System.Drawing.Point(643, 75);
            this.Járműadatok_rögzít.Name = "Járműadatok_rögzít";
            this.Járműadatok_rögzít.Size = new System.Drawing.Size(40, 40);
            this.Járműadatok_rögzít.TabIndex = 8;
            this.ToolTip1.SetToolTip(this.Járműadatok_rögzít, "Rögzít");
            this.Járműadatok_rögzít.UseVisualStyleBackColor = true;
            this.Járműadatok_rögzít.Click += new System.EventHandler(this.Járműadatok_rögzít_Click);
            // 
            // Pályaszámkereső
            // 
            this.Pályaszámkereső.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Pályaszámkereső.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Pályaszámkereső.Location = new System.Drawing.Point(321, 9);
            this.Pályaszámkereső.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Pályaszámkereső.Name = "Pályaszámkereső";
            this.Pályaszámkereső.Size = new System.Drawing.Size(40, 40);
            this.Pályaszámkereső.TabIndex = 1;
            this.ToolTip1.SetToolTip(this.Pályaszámkereső, "Pályaszám adatait megkeresi");
            this.Pályaszámkereső.UseVisualStyleBackColor = true;
            this.Pályaszámkereső.Click += new System.EventHandler(this.Pályaszámkereső_Click);
            // 
            // BtnKarbantartExcel
            // 
            this.BtnKarbantartExcel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.BtnKarbantartExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnKarbantartExcel.Location = new System.Drawing.Point(529, 6);
            this.BtnKarbantartExcel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnKarbantartExcel.Name = "BtnKarbantartExcel";
            this.BtnKarbantartExcel.Size = new System.Drawing.Size(40, 40);
            this.BtnKarbantartExcel.TabIndex = 4;
            this.ToolTip1.SetToolTip(this.BtnKarbantartExcel, "Excel tábla készítés a táblázat adataiból");
            this.BtnKarbantartExcel.UseVisualStyleBackColor = true;
            this.BtnKarbantartExcel.Click += new System.EventHandler(this.BtnKarbantartExcel_Click);
            // 
            // BtnKarbantartFrissít
            // 
            this.BtnKarbantartFrissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.BtnKarbantartFrissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnKarbantartFrissít.Location = new System.Drawing.Point(482, 6);
            this.BtnKarbantartFrissít.Name = "BtnKarbantartFrissít";
            this.BtnKarbantartFrissít.Size = new System.Drawing.Size(40, 40);
            this.BtnKarbantartFrissít.TabIndex = 3;
            this.ToolTip1.SetToolTip(this.BtnKarbantartFrissít, "Listázza az előzményeket");
            this.BtnKarbantartFrissít.UseVisualStyleBackColor = true;
            this.BtnKarbantartFrissít.Click += new System.EventHandler(this.BtnKarbantartFrissít_Click);
            // 
            // Keresés
            // 
            this.Keresés.BackgroundImage = global::Villamos.Properties.Resources.Nagyító;
            this.Keresés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Keresés.Location = new System.Drawing.Point(598, 5);
            this.Keresés.Name = "Keresés";
            this.Keresés.Size = new System.Drawing.Size(40, 40);
            this.Keresés.TabIndex = 7;
            this.ToolTip1.SetToolTip(this.Keresés, "Keresés a táblázatban");
            this.Keresés.UseVisualStyleBackColor = true;
            this.Keresés.Click += new System.EventHandler(this.Keresés_Click);
            // 
            // Excelkimenet
            // 
            this.Excelkimenet.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excelkimenet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excelkimenet.Location = new System.Drawing.Point(702, 5);
            this.Excelkimenet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Excelkimenet.Name = "Excelkimenet";
            this.Excelkimenet.Size = new System.Drawing.Size(40, 40);
            this.Excelkimenet.TabIndex = 8;
            this.ToolTip1.SetToolTip(this.Excelkimenet, "Excel tábla készítés a táblázat adataiból");
            this.Excelkimenet.UseVisualStyleBackColor = true;
            this.Excelkimenet.Click += new System.EventHandler(this.Excelkimenet_Click);
            // 
            // Terv_lista
            // 
            this.Terv_lista.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Terv_lista.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Terv_lista.Location = new System.Drawing.Point(279, 5);
            this.Terv_lista.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Terv_lista.Name = "Terv_lista";
            this.Terv_lista.Size = new System.Drawing.Size(40, 40);
            this.Terv_lista.TabIndex = 3;
            this.ToolTip1.SetToolTip(this.Terv_lista, "Listázza a rögzített előtervet");
            this.Terv_lista.UseVisualStyleBackColor = true;
            this.Terv_lista.Click += new System.EventHandler(this.Terv_lista_Click);
            // 
            // BtnÜtemÜtemezés
            // 
            this.BtnÜtemÜtemezés.BackgroundImage = global::Villamos.Properties.Resources.Document_preferences;
            this.BtnÜtemÜtemezés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnÜtemÜtemezés.Location = new System.Drawing.Point(346, 5);
            this.BtnÜtemÜtemezés.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnÜtemÜtemezés.Name = "BtnÜtemÜtemezés";
            this.BtnÜtemÜtemezés.Size = new System.Drawing.Size(40, 40);
            this.BtnÜtemÜtemezés.TabIndex = 4;
            this.ToolTip1.SetToolTip(this.BtnÜtemÜtemezés, "Ütemezi a vizsgálatokat");
            this.BtnÜtemÜtemezés.UseVisualStyleBackColor = true;
            this.BtnÜtemÜtemezés.Click += new System.EventHandler(this.BtnÜtemÜtemezés_Click);
            // 
            // Telephely_lap
            // 
            this.Telephely_lap.BackgroundImage = global::Villamos.Properties.Resources.Google_Noto_Emoji_Travel_Places_42498_factory;
            this.Telephely_lap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Telephely_lap.Location = new System.Drawing.Point(503, 5);
            this.Telephely_lap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Telephely_lap.Name = "Telephely_lap";
            this.Telephely_lap.Size = new System.Drawing.Size(40, 40);
            this.Telephely_lap.TabIndex = 5;
            this.ToolTip1.SetToolTip(this.Telephely_lap, "Telephelyi sorrend beállítás");
            this.Telephely_lap.UseVisualStyleBackColor = true;
            this.Telephely_lap.Click += new System.EventHandler(this.Telephely_lap_Click);
            // 
            // BtnSzínező
            // 
            this.BtnSzínező.BackgroundImage = global::Villamos.Properties.Resources.Dtafalonso_Modern_Xp_ModernXP_12_Workstation_Desktop_Colors;
            this.BtnSzínező.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSzínező.Location = new System.Drawing.Point(551, 5);
            this.BtnSzínező.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnSzínező.Name = "BtnSzínező";
            this.BtnSzínező.Size = new System.Drawing.Size(40, 40);
            this.BtnSzínező.TabIndex = 6;
            this.ToolTip1.SetToolTip(this.BtnSzínező, "Vizsgálat színező");
            this.BtnSzínező.UseVisualStyleBackColor = true;
            this.BtnSzínező.Click += new System.EventHandler(this.BtnSzínező_Click);
            // 
            // Ütemfrissít
            // 
            this.Ütemfrissít.BackgroundImage = global::Villamos.Properties.Resources.BOOKS7;
            this.Ütemfrissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Ütemfrissít.Location = new System.Drawing.Point(5, 5);
            this.Ütemfrissít.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Ütemfrissít.Name = "Ütemfrissít";
            this.Ütemfrissít.Size = new System.Drawing.Size(40, 40);
            this.Ütemfrissít.TabIndex = 0;
            this.ToolTip1.SetToolTip(this.Ütemfrissít, "Előzetes terv készítés");
            this.Ütemfrissít.UseVisualStyleBackColor = true;
            this.Ütemfrissít.Click += new System.EventHandler(this.Ütemfrissít_Click);
            // 
            // BtnÜtemTörlés
            // 
            this.BtnÜtemTörlés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.BtnÜtemTörlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnÜtemTörlés.Location = new System.Drawing.Point(393, 5);
            this.BtnÜtemTörlés.Name = "BtnÜtemTörlés";
            this.BtnÜtemTörlés.Size = new System.Drawing.Size(40, 40);
            this.BtnÜtemTörlés.TabIndex = 219;
            this.ToolTip1.SetToolTip(this.BtnÜtemTörlés, "Törli az előzetesen ütemezett kocsikat");
            this.BtnÜtemTörlés.UseVisualStyleBackColor = true;
            this.BtnÜtemTörlés.Click += new System.EventHandler(this.BtnÜtemTörlésClick);
            // 
            // BtnÜtemRészRögz
            // 
            this.BtnÜtemRészRögz.BackColor = System.Drawing.Color.Gray;
            this.BtnÜtemRészRögz.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.BtnÜtemRészRögz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnÜtemRészRögz.Location = new System.Drawing.Point(771, 10);
            this.BtnÜtemRészRögz.Name = "BtnÜtemRészRögz";
            this.BtnÜtemRészRögz.Size = new System.Drawing.Size(40, 40);
            this.BtnÜtemRészRögz.TabIndex = 11;
            this.ToolTip1.SetToolTip(this.BtnÜtemRészRögz, "Rögzít");
            this.BtnÜtemRészRögz.UseVisualStyleBackColor = true;
            this.BtnÜtemRészRögz.Click += new System.EventHandler(this.BtnÜtemRészRögz_Click);
            // 
            // BtnÜtemRészTerv
            // 
            this.BtnÜtemRészTerv.BackColor = System.Drawing.Color.Gray;
            this.BtnÜtemRészTerv.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.BtnÜtemRészTerv.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnÜtemRészTerv.Location = new System.Drawing.Point(668, 10);
            this.BtnÜtemRészTerv.Name = "BtnÜtemRészTerv";
            this.BtnÜtemRészTerv.Size = new System.Drawing.Size(40, 40);
            this.BtnÜtemRészTerv.TabIndex = 12;
            this.ToolTip1.SetToolTip(this.BtnÜtemRészTerv, "Előzetes tervet készít");
            this.BtnÜtemRészTerv.UseVisualStyleBackColor = true;
            this.BtnÜtemRészTerv.Click += new System.EventHandler(this.BtnÜtemRészTerv_Click);
            // 
            // BtnÜtemNaplóExcel
            // 
            this.BtnÜtemNaplóExcel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.BtnÜtemNaplóExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnÜtemNaplóExcel.Location = new System.Drawing.Point(530, 7);
            this.BtnÜtemNaplóExcel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnÜtemNaplóExcel.Name = "BtnÜtemNaplóExcel";
            this.BtnÜtemNaplóExcel.Size = new System.Drawing.Size(40, 40);
            this.BtnÜtemNaplóExcel.TabIndex = 4;
            this.ToolTip1.SetToolTip(this.BtnÜtemNaplóExcel, "Excel tábla készítés a táblázat adataiból");
            this.BtnÜtemNaplóExcel.UseVisualStyleBackColor = true;
            this.BtnÜtemNaplóExcel.Click += new System.EventHandler(this.BtnÜtemNaplóExcel_Click);
            // 
            // BtnÜtemNaplóFrissít
            // 
            this.BtnÜtemNaplóFrissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.BtnÜtemNaplóFrissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnÜtemNaplóFrissít.Location = new System.Drawing.Point(483, 7);
            this.BtnÜtemNaplóFrissít.Name = "BtnÜtemNaplóFrissít";
            this.BtnÜtemNaplóFrissít.Size = new System.Drawing.Size(40, 40);
            this.BtnÜtemNaplóFrissít.TabIndex = 3;
            this.ToolTip1.SetToolTip(this.BtnÜtemNaplóFrissít, "Listázza az előzményeket");
            this.BtnÜtemNaplóFrissít.UseVisualStyleBackColor = true;
            this.BtnÜtemNaplóFrissít.Click += new System.EventHandler(this.BtnÜtemNaplóFrissít_Click);
            // 
            // BtnElőtervezőFrissít
            // 
            this.BtnElőtervezőFrissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.BtnElőtervezőFrissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnElőtervezőFrissít.Location = new System.Drawing.Point(190, 14);
            this.BtnElőtervezőFrissít.Name = "BtnElőtervezőFrissít";
            this.BtnElőtervezőFrissít.Size = new System.Drawing.Size(40, 40);
            this.BtnElőtervezőFrissít.TabIndex = 1;
            this.ToolTip1.SetToolTip(this.BtnElőtervezőFrissít, "Listázza az előzményeket");
            this.BtnElőtervezőFrissít.UseVisualStyleBackColor = true;
            this.BtnElőtervezőFrissít.Click += new System.EventHandler(this.BtnElőtervezőFrissít_Click);
            // 
            // Mindentkijelöl
            // 
            this.Mindentkijelöl.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.Mindentkijelöl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Mindentkijelöl.Location = new System.Drawing.Point(355, 5);
            this.Mindentkijelöl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Mindentkijelöl.Name = "Mindentkijelöl";
            this.Mindentkijelöl.Size = new System.Drawing.Size(40, 40);
            this.Mindentkijelöl.TabIndex = 0;
            this.ToolTip1.SetToolTip(this.Mindentkijelöl, "Mindent kijelöl");
            this.Mindentkijelöl.UseVisualStyleBackColor = true;
            this.Mindentkijelöl.Click += new System.EventHandler(this.Mindentkijelöl_Click);
            // 
            // Kijelöléstörlése
            // 
            this.Kijelöléstörlése.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.Kijelöléstörlése.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Kijelöléstörlése.Location = new System.Drawing.Point(355, 55);
            this.Kijelöléstörlése.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Kijelöléstörlése.Name = "Kijelöléstörlése";
            this.Kijelöléstörlése.Size = new System.Drawing.Size(40, 40);
            this.Kijelöléstörlése.TabIndex = 1;
            this.ToolTip1.SetToolTip(this.Kijelöléstörlése, "Minden kijelölés törlése");
            this.Kijelöléstörlése.UseVisualStyleBackColor = true;
            this.Kijelöléstörlése.Click += new System.EventHandler(this.Kijelöléstörlése_Click);
            // 
            // BtnElőtervezőKeres
            // 
            this.BtnElőtervezőKeres.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.BtnElőtervezőKeres.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnElőtervezőKeres.Location = new System.Drawing.Point(425, 6);
            this.BtnElőtervezőKeres.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnElőtervezőKeres.Name = "BtnElőtervezőKeres";
            this.BtnElőtervezőKeres.Size = new System.Drawing.Size(40, 40);
            this.BtnElőtervezőKeres.TabIndex = 2;
            this.ToolTip1.SetToolTip(this.BtnElőtervezőKeres, "Pályaszám adatait megkeresi");
            this.BtnElőtervezőKeres.UseVisualStyleBackColor = true;
            this.BtnElőtervezőKeres.Click += new System.EventHandler(this.BtnElőtervezőKeres_Click);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.BackColor = System.Drawing.Color.Lime;
            this.Holtart.ForeColor = System.Drawing.Color.MediumBlue;
            this.Holtart.Location = new System.Drawing.Point(380, 21);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(673, 20);
            this.Holtart.TabIndex = 151;
            this.Holtart.Visible = false;
            // 
            // Pá
            // 
            this.Pá.AutoSize = true;
            this.Pá.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Pá.Location = new System.Drawing.Point(6, 28);
            this.Pá.Name = "Pá";
            this.Pá.Size = new System.Drawing.Size(89, 20);
            this.Pá.TabIndex = 152;
            this.Pá.Text = "Pályaszám:";
            // 
            // Pályaszám
            // 
            this.Pályaszám.FormattingEnabled = true;
            this.Pályaszám.Location = new System.Drawing.Point(204, 21);
            this.Pályaszám.Name = "Pályaszám";
            this.Pályaszám.Size = new System.Drawing.Size(107, 28);
            this.Pályaszám.TabIndex = 0;
            this.Pályaszám.SelectedIndexChanged += new System.EventHandler(this.Pályaszám_SelectedIndexChanged);
            // 
            // TabPage6
            // 
            this.TabPage6.BackColor = System.Drawing.Color.Coral;
            this.TabPage6.Controls.Add(this.PszJelölő);
            this.TabPage6.Controls.Add(this.Panel6);
            this.TabPage6.Controls.Add(this.Panel5);
            this.TabPage6.Controls.Add(this.Panel4);
            this.TabPage6.Controls.Add(this.Panel3);
            this.TabPage6.Controls.Add(this.Mindentkijelöl);
            this.TabPage6.Controls.Add(this.Kijelöléstörlése);
            this.TabPage6.Controls.Add(this.BtnElőtervezőKeres);
            this.TabPage6.Location = new System.Drawing.Point(4, 29);
            this.TabPage6.Name = "TabPage6";
            this.TabPage6.Size = new System.Drawing.Size(1077, 444);
            this.TabPage6.TabIndex = 5;
            this.TabPage6.Text = "Előtervező";
            // 
            // PszJelölő
            // 
            this.PszJelölő.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PszJelölő.FormattingEnabled = true;
            this.PszJelölő.Location = new System.Drawing.Point(245, 8);
            this.PszJelölő.Name = "PszJelölő";
            this.PszJelölő.Size = new System.Drawing.Size(103, 424);
            this.PszJelölő.TabIndex = 168;
            // 
            // Panel6
            // 
            this.Panel6.BackColor = System.Drawing.Color.Tomato;
            this.Panel6.Controls.Add(this.Check1);
            this.Panel6.Location = new System.Drawing.Point(5, 149);
            this.Panel6.Name = "Panel6";
            this.Panel6.Size = new System.Drawing.Size(233, 53);
            this.Panel6.TabIndex = 3;
            // 
            // Check1
            // 
            this.Check1.AutoSize = true;
            this.Check1.Location = new System.Drawing.Point(18, 15);
            this.Check1.Name = "Check1";
            this.Check1.Size = new System.Drawing.Size(180, 24);
            this.Check1.TabIndex = 0;
            this.Check1.Text = "Előző futatás marad?";
            this.Check1.UseVisualStyleBackColor = true;
            // 
            // Panel5
            // 
            this.Panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Panel5.BackColor = System.Drawing.Color.Tomato;
            this.Panel5.Controls.Add(this.VizsgálatLista);
            this.Panel5.Controls.Add(this.ElőCiklusrend);
            this.Panel5.Controls.Add(this.Label28);
            this.Panel5.Location = new System.Drawing.Point(5, 205);
            this.Panel5.Name = "Panel5";
            this.Panel5.Size = new System.Drawing.Size(233, 233);
            this.Panel5.TabIndex = 2;
            // 
            // VizsgálatLista
            // 
            this.VizsgálatLista.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.VizsgálatLista.FormattingEnabled = true;
            this.VizsgálatLista.Location = new System.Drawing.Point(72, 46);
            this.VizsgálatLista.Name = "VizsgálatLista";
            this.VizsgálatLista.Size = new System.Drawing.Size(152, 172);
            this.VizsgálatLista.TabIndex = 1;
            // 
            // ElőCiklusrend
            // 
            this.ElőCiklusrend.FormattingEnabled = true;
            this.ElőCiklusrend.Location = new System.Drawing.Point(73, 12);
            this.ElőCiklusrend.Name = "ElőCiklusrend";
            this.ElőCiklusrend.Size = new System.Drawing.Size(151, 28);
            this.ElőCiklusrend.TabIndex = 0;
            this.ElőCiklusrend.SelectedIndexChanged += new System.EventHandler(this.ElőCiklusrend_SelectedIndexChanged);
            // 
            // Label28
            // 
            this.Label28.AutoSize = true;
            this.Label28.BackColor = System.Drawing.Color.Transparent;
            this.Label28.Location = new System.Drawing.Point(0, 0);
            this.Label28.Name = "Label28";
            this.Label28.Size = new System.Drawing.Size(74, 20);
            this.Label28.TabIndex = 88;
            this.Label28.Text = "Vizsgálat";
            // 
            // Panel4
            // 
            this.Panel4.BackColor = System.Drawing.Color.Tomato;
            this.Panel4.Controls.Add(this.ElőbefejezőDátum);
            this.Panel4.Controls.Add(this.Előkezdődátum);
            this.Panel4.Controls.Add(this.Label27);
            this.Panel4.Location = new System.Drawing.Point(5, 75);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(233, 69);
            this.Panel4.TabIndex = 1;
            // 
            // ElőbefejezőDátum
            // 
            this.ElőbefejezőDátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ElőbefejezőDátum.Location = new System.Drawing.Point(117, 23);
            this.ElőbefejezőDátum.Name = "ElőbefejezőDátum";
            this.ElőbefejezőDátum.Size = new System.Drawing.Size(107, 26);
            this.ElőbefejezőDátum.TabIndex = 1;
            // 
            // Előkezdődátum
            // 
            this.Előkezdődátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Előkezdődátum.Location = new System.Drawing.Point(4, 23);
            this.Előkezdődátum.Name = "Előkezdődátum";
            this.Előkezdődátum.Size = new System.Drawing.Size(107, 26);
            this.Előkezdődátum.TabIndex = 0;
            // 
            // Label27
            // 
            this.Label27.AutoSize = true;
            this.Label27.BackColor = System.Drawing.Color.Transparent;
            this.Label27.Location = new System.Drawing.Point(0, 0);
            this.Label27.Name = "Label27";
            this.Label27.Size = new System.Drawing.Size(65, 20);
            this.Label27.TabIndex = 88;
            this.Label27.Text = "Időszak";
            // 
            // Panel3
            // 
            this.Panel3.BackColor = System.Drawing.Color.Tomato;
            this.Panel3.Controls.Add(this.BtnElőtervezőFrissít);
            this.Panel3.Controls.Add(this.Telephely);
            this.Panel3.Controls.Add(this.Label26);
            this.Panel3.Location = new System.Drawing.Point(5, 5);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(233, 64);
            this.Panel3.TabIndex = 0;
            // 
            // Telephely
            // 
            this.Telephely.FormattingEnabled = true;
            this.Telephely.Location = new System.Drawing.Point(4, 26);
            this.Telephely.Name = "Telephely";
            this.Telephely.Size = new System.Drawing.Size(151, 28);
            this.Telephely.TabIndex = 0;
            // 
            // Label26
            // 
            this.Label26.AutoSize = true;
            this.Label26.BackColor = System.Drawing.Color.Transparent;
            this.Label26.Location = new System.Drawing.Point(0, 0);
            this.Label26.Name = "Label26";
            this.Label26.Size = new System.Drawing.Size(76, 20);
            this.Label26.TabIndex = 88;
            this.Label26.Text = "Telephely";
            // 
            // TabPage5
            // 
            this.TabPage5.BackColor = System.Drawing.Color.ForestGreen;
            this.TabPage5.Controls.Add(this.ÜtemNapló);
            this.TabPage5.Controls.Add(this.ÜtemPályaszám);
            this.TabPage5.Controls.Add(this.Label25);
            this.TabPage5.Controls.Add(this.ÜtemNaplóVége);
            this.TabPage5.Controls.Add(this.ÜtemNaplóKezdet);
            this.TabPage5.Controls.Add(this.BtnÜtemNaplóExcel);
            this.TabPage5.Controls.Add(this.BtnÜtemNaplóFrissít);
            this.TabPage5.Location = new System.Drawing.Point(4, 29);
            this.TabPage5.Name = "TabPage5";
            this.TabPage5.Size = new System.Drawing.Size(1077, 444);
            this.TabPage5.TabIndex = 4;
            this.TabPage5.Text = "Ütemezés napló";
            // 
            // ÜtemNapló
            // 
            this.ÜtemNapló.AllowUserToAddRows = false;
            this.ÜtemNapló.AllowUserToDeleteRows = false;
            this.ÜtemNapló.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ÜtemNapló.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ÜtemNapló.Location = new System.Drawing.Point(4, 53);
            this.ÜtemNapló.Name = "ÜtemNapló";
            this.ÜtemNapló.RowHeadersVisible = false;
            this.ÜtemNapló.RowHeadersWidth = 51;
            this.ÜtemNapló.Size = new System.Drawing.Size(1068, 385);
            this.ÜtemNapló.TabIndex = 236;
            // 
            // ÜtemPályaszám
            // 
            this.ÜtemPályaszám.FormattingEnabled = true;
            this.ÜtemPályaszám.Location = new System.Drawing.Point(339, 13);
            this.ÜtemPályaszám.Name = "ÜtemPályaszám";
            this.ÜtemPályaszám.Size = new System.Drawing.Size(107, 28);
            this.ÜtemPályaszám.TabIndex = 2;
            // 
            // Label25
            // 
            this.Label25.AutoSize = true;
            this.Label25.Location = new System.Drawing.Point(244, 21);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(89, 20);
            this.Label25.TabIndex = 232;
            this.Label25.Text = "Pályaszám:";
            // 
            // ÜtemNaplóVége
            // 
            this.ÜtemNaplóVége.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ÜtemNaplóVége.Location = new System.Drawing.Point(120, 15);
            this.ÜtemNaplóVége.Name = "ÜtemNaplóVége";
            this.ÜtemNaplóVége.Size = new System.Drawing.Size(107, 26);
            this.ÜtemNaplóVége.TabIndex = 1;
            // 
            // ÜtemNaplóKezdet
            // 
            this.ÜtemNaplóKezdet.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ÜtemNaplóKezdet.Location = new System.Drawing.Point(7, 15);
            this.ÜtemNaplóKezdet.Name = "ÜtemNaplóKezdet";
            this.ÜtemNaplóKezdet.Size = new System.Drawing.Size(107, 26);
            this.ÜtemNaplóKezdet.TabIndex = 0;
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.Gray;
            this.TabPage4.Controls.Add(this.Label24);
            this.TabPage4.Controls.Add(this.Label23);
            this.TabPage4.Controls.Add(this.Label22);
            this.TabPage4.Controls.Add(this.Label21);
            this.TabPage4.Controls.Add(this.Üstátus);
            this.TabPage4.Controls.Add(this.ÜVÜtemezés);
            this.TabPage4.Controls.Add(this.ÜVElkészülés);
            this.TabPage4.Controls.Add(this.Label20);
            this.TabPage4.Controls.Add(this.ÜVizsgfoka);
            this.TabPage4.Controls.Add(this.ÜVVégezte);
            this.TabPage4.Controls.Add(this.Üazonosító);
            this.TabPage4.Controls.Add(this.ÜCiklusrend);
            this.TabPage4.Controls.Add(this.Label19);
            this.TabPage4.Controls.Add(this.Label18);
            this.TabPage4.Controls.Add(this.Label17);
            this.TabPage4.Controls.Add(this.ÜMegjegyzés);
            this.TabPage4.Controls.Add(this.Üelkészült);
            this.TabPage4.Controls.Add(this.ÜVSorszám);
            this.TabPage4.Controls.Add(this.ÜVEsedékesség);
            this.TabPage4.Controls.Add(this.Label15);
            this.TabPage4.Controls.Add(this.Label16);
            this.TabPage4.Controls.Add(this.BtnÜtemRészRögz);
            this.TabPage4.Controls.Add(this.BtnÜtemRészTerv);
            this.TabPage4.Location = new System.Drawing.Point(4, 29);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Size = new System.Drawing.Size(1077, 444);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Ütemezés részletes";
            // 
            // Label24
            // 
            this.Label24.AutoSize = true;
            this.Label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Label24.Location = new System.Drawing.Point(10, 203);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(210, 20);
            this.Label24.TabIndex = 249;
            this.Label24.Text = "Vizsgálat ütemezés dátuma:";
            // 
            // Label23
            // 
            this.Label23.AutoSize = true;
            this.Label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Label23.Location = new System.Drawing.Point(418, 200);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(147, 20);
            this.Label23.TabIndex = 248;
            this.Label23.Text = "Elkészülés dátuma:";
            // 
            // Label22
            // 
            this.Label22.AutoSize = true;
            this.Label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Label22.Location = new System.Drawing.Point(418, 169);
            this.Label22.Name = "Label22";
            this.Label22.Size = new System.Drawing.Size(154, 20);
            this.Label22.TabIndex = 247;
            this.Label22.Text = "Vizsgálat telephelye:";
            // 
            // Label21
            // 
            this.Label21.AutoSize = true;
            this.Label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Label21.Location = new System.Drawing.Point(10, 168);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(168, 20);
            this.Label21.TabIndex = 246;
            this.Label21.Text = "Esedékesség dátuma:";
            // 
            // Üstátus
            // 
            this.Üstátus.FormattingEnabled = true;
            this.Üstátus.Location = new System.Drawing.Point(588, 107);
            this.Üstátus.Name = "Üstátus";
            this.Üstátus.Size = new System.Drawing.Size(177, 28);
            this.Üstátus.TabIndex = 7;
            // 
            // ÜVÜtemezés
            // 
            this.ÜVÜtemezés.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ÜVÜtemezés.Location = new System.Drawing.Point(232, 197);
            this.ÜVÜtemezés.Name = "ÜVÜtemezés";
            this.ÜVÜtemezés.Size = new System.Drawing.Size(107, 26);
            this.ÜVÜtemezés.TabIndex = 6;
            // 
            // ÜVElkészülés
            // 
            this.ÜVElkészülés.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ÜVElkészülés.Location = new System.Drawing.Point(588, 198);
            this.ÜVElkészülés.Name = "ÜVElkészülés";
            this.ÜVElkészülés.Size = new System.Drawing.Size(107, 26);
            this.ÜVElkészülés.TabIndex = 8;
            // 
            // Label20
            // 
            this.Label20.AutoSize = true;
            this.Label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Label20.Location = new System.Drawing.Point(418, 115);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(60, 20);
            this.Label20.TabIndex = 242;
            this.Label20.Text = "Státus:";
            // 
            // ÜVizsgfoka
            // 
            this.ÜVizsgfoka.Location = new System.Drawing.Point(232, 74);
            this.ÜVizsgfoka.Name = "ÜVizsgfoka";
            this.ÜVizsgfoka.Size = new System.Drawing.Size(123, 26);
            this.ÜVizsgfoka.TabIndex = 2;
            // 
            // ÜVVégezte
            // 
            this.ÜVVégezte.FormattingEnabled = true;
            this.ÜVVégezte.Location = new System.Drawing.Point(588, 160);
            this.ÜVVégezte.Name = "ÜVVégezte";
            this.ÜVVégezte.Size = new System.Drawing.Size(177, 28);
            this.ÜVVégezte.TabIndex = 4;
            // 
            // Üazonosító
            // 
            this.Üazonosító.Location = new System.Drawing.Point(232, 4);
            this.Üazonosító.Name = "Üazonosító";
            this.Üazonosító.Size = new System.Drawing.Size(123, 26);
            this.Üazonosító.TabIndex = 0;
            // 
            // ÜCiklusrend
            // 
            this.ÜCiklusrend.FormattingEnabled = true;
            this.ÜCiklusrend.Location = new System.Drawing.Point(232, 37);
            this.ÜCiklusrend.Name = "ÜCiklusrend";
            this.ÜCiklusrend.Size = new System.Drawing.Size(164, 28);
            this.ÜCiklusrend.TabIndex = 1;
            this.ÜCiklusrend.SelectedIndexChanged += new System.EventHandler(this.ÜCiklusrend_SelectedIndexChanged);
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Label19.Location = new System.Drawing.Point(10, 115);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(151, 20);
            this.Label19.TabIndex = 237;
            this.Label19.Text = "Vizsgálat sorszáma:";
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Label18.Location = new System.Drawing.Point(10, 80);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(109, 20);
            this.Label18.TabIndex = 236;
            this.Label18.Text = "Vizsgálatfoka:";
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Label17.Location = new System.Drawing.Point(10, 45);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(87, 20);
            this.Label17.TabIndex = 235;
            this.Label17.Text = "Ciklusrend:";
            // 
            // ÜMegjegyzés
            // 
            this.ÜMegjegyzés.Location = new System.Drawing.Point(232, 261);
            this.ÜMegjegyzés.Multiline = true;
            this.ÜMegjegyzés.Name = "ÜMegjegyzés";
            this.ÜMegjegyzés.Size = new System.Drawing.Size(842, 57);
            this.ÜMegjegyzés.TabIndex = 10;
            // 
            // Üelkészült
            // 
            this.Üelkészült.AutoSize = true;
            this.Üelkészült.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Üelkészült.Location = new System.Drawing.Point(771, 199);
            this.Üelkészült.Name = "Üelkészült";
            this.Üelkészült.Size = new System.Drawing.Size(92, 24);
            this.Üelkészült.TabIndex = 9;
            this.Üelkészült.Text = "Elkészült";
            this.Üelkészült.UseVisualStyleBackColor = false;
            // 
            // ÜVSorszám
            // 
            this.ÜVSorszám.FormattingEnabled = true;
            this.ÜVSorszám.Location = new System.Drawing.Point(232, 107);
            this.ÜVSorszám.Name = "ÜVSorszám";
            this.ÜVSorszám.Size = new System.Drawing.Size(164, 28);
            this.ÜVSorszám.TabIndex = 3;
            this.ÜVSorszám.SelectedIndexChanged += new System.EventHandler(this.ÜVSorszám_SelectedIndexChanged);
            // 
            // ÜVEsedékesség
            // 
            this.ÜVEsedékesség.Enabled = false;
            this.ÜVEsedékesség.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ÜVEsedékesség.Location = new System.Drawing.Point(232, 163);
            this.ÜVEsedékesség.Name = "ÜVEsedékesség";
            this.ÜVEsedékesség.Size = new System.Drawing.Size(107, 26);
            this.ÜVEsedékesség.TabIndex = 5;
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Label15.Location = new System.Drawing.Point(10, 10);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(89, 20);
            this.Label15.TabIndex = 230;
            this.Label15.Text = "Pályaszám:";
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Label16.Location = new System.Drawing.Point(10, 261);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(97, 20);
            this.Label16.TabIndex = 229;
            this.Label16.Text = "Megjegyzés:";
            // 
            // TabPage3
            // 
            this.TabPage3.Controls.Add(this.Keresés);
            this.TabPage3.Controls.Add(this.Excelkimenet);
            this.TabPage3.Controls.Add(this.Terv_lista);
            this.TabPage3.Controls.Add(this.BtnÜtemÜtemezés);
            this.TabPage3.Controls.Add(this.Telephely_lap);
            this.TabPage3.Controls.Add(this.BtnSzínező);
            this.TabPage3.Controls.Add(this.Ütemfrissít);
            this.TabPage3.Controls.Add(this.Ütemvége);
            this.TabPage3.Controls.Add(this.Ütemkezdete);
            this.TabPage3.Controls.Add(this.Táblaütemezés);
            this.TabPage3.Controls.Add(this.BtnÜtemTörlés);
            this.TabPage3.Location = new System.Drawing.Point(4, 29);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(1077, 444);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Ütemezés";
            this.TabPage3.UseVisualStyleBackColor = true;
            // 
            // Ütemvége
            // 
            this.Ütemvége.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Ütemvége.Location = new System.Drawing.Point(165, 12);
            this.Ütemvége.Name = "Ütemvége";
            this.Ütemvége.Size = new System.Drawing.Size(107, 26);
            this.Ütemvége.TabIndex = 2;
            // 
            // Ütemkezdete
            // 
            this.Ütemkezdete.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Ütemkezdete.Location = new System.Drawing.Point(52, 12);
            this.Ütemkezdete.Name = "Ütemkezdete";
            this.Ütemkezdete.Size = new System.Drawing.Size(107, 26);
            this.Ütemkezdete.TabIndex = 1;
            // 
            // Táblaütemezés
            // 
            this.Táblaütemezés.AllowUserToAddRows = false;
            this.Táblaütemezés.AllowUserToDeleteRows = false;
            this.Táblaütemezés.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Táblaütemezés.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Táblaütemezés.Location = new System.Drawing.Point(3, 50);
            this.Táblaütemezés.Name = "Táblaütemezés";
            this.Táblaütemezés.RowHeadersWidth = 51;
            this.Táblaütemezés.Size = new System.Drawing.Size(1071, 388);
            this.Táblaütemezés.TabIndex = 218;
            this.Táblaütemezés.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Táblaütemezés_CellClick);
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.TabPage2.Controls.Add(this.Napló_Tábla);
            this.TabPage2.Controls.Add(this.NaplóPályaszám);
            this.TabPage2.Controls.Add(this.Label14);
            this.TabPage2.Controls.Add(this.NaplóVége);
            this.TabPage2.Controls.Add(this.NaplóKezdete);
            this.TabPage2.Controls.Add(this.BtnKarbantartExcel);
            this.TabPage2.Controls.Add(this.BtnKarbantartFrissít);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(1077, 444);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Karbantartási előzmények";
            // 
            // Napló_Tábla
            // 
            this.Napló_Tábla.AllowUserToAddRows = false;
            this.Napló_Tábla.AllowUserToDeleteRows = false;
            this.Napló_Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Napló_Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Napló_Tábla.Location = new System.Drawing.Point(3, 52);
            this.Napló_Tábla.Name = "Napló_Tábla";
            this.Napló_Tábla.RowHeadersVisible = false;
            this.Napló_Tábla.RowHeadersWidth = 51;
            this.Napló_Tábla.Size = new System.Drawing.Size(1068, 386);
            this.Napló_Tábla.TabIndex = 229;
            // 
            // NaplóPályaszám
            // 
            this.NaplóPályaszám.FormattingEnabled = true;
            this.NaplóPályaszám.Location = new System.Drawing.Point(338, 12);
            this.NaplóPályaszám.Name = "NaplóPályaszám";
            this.NaplóPályaszám.Size = new System.Drawing.Size(107, 28);
            this.NaplóPályaszám.TabIndex = 2;
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(243, 20);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(89, 20);
            this.Label14.TabIndex = 208;
            this.Label14.Text = "Pályaszám:";
            // 
            // NaplóVége
            // 
            this.NaplóVége.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.NaplóVége.Location = new System.Drawing.Point(119, 14);
            this.NaplóVége.Name = "NaplóVége";
            this.NaplóVége.Size = new System.Drawing.Size(107, 26);
            this.NaplóVége.TabIndex = 1;
            // 
            // NaplóKezdete
            // 
            this.NaplóKezdete.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.NaplóKezdete.Location = new System.Drawing.Point(6, 14);
            this.NaplóKezdete.Name = "NaplóKezdete";
            this.NaplóKezdete.Size = new System.Drawing.Size(107, 26);
            this.NaplóKezdete.TabIndex = 0;
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.TabPage1.Controls.Add(this.Vizsgsorszám);
            this.TabPage1.Controls.Add(this.Járműadatok_rögzít);
            this.TabPage1.Controls.Add(this.Pályaszámkereső);
            this.TabPage1.Controls.Add(this.Pályaszám);
            this.TabPage1.Controls.Add(this.VizsgNév);
            this.TabPage1.Controls.Add(this.Pá);
            this.TabPage1.Controls.Add(this.Oka);
            this.TabPage1.Controls.Add(this.KötöttStart);
            this.TabPage1.Controls.Add(this.Megállítás);
            this.TabPage1.Controls.Add(this.Ciklusrend);
            this.TabPage1.Controls.Add(this.Vizsgdátum);
            this.TabPage1.Controls.Add(this.Start);
            this.TabPage1.Controls.Add(this.Label12);
            this.TabPage1.Controls.Add(this.Label11);
            this.TabPage1.Controls.Add(this.Label10);
            this.TabPage1.Controls.Add(this.Label9);
            this.TabPage1.Controls.Add(this.Label8);
            this.TabPage1.Controls.Add(this.Label7);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1077, 444);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Jármű adatok";
            // 
            // Vizsgsorszám
            // 
            this.Vizsgsorszám.FormattingEnabled = true;
            this.Vizsgsorszám.Location = new System.Drawing.Point(204, 270);
            this.Vizsgsorszám.Name = "Vizsgsorszám";
            this.Vizsgsorszám.Size = new System.Drawing.Size(121, 28);
            this.Vizsgsorszám.TabIndex = 153;
            this.Vizsgsorszám.SelectedIndexChanged += new System.EventHandler(this.Vizsgsorszám_SelectedIndexChanged);
            // 
            // VizsgNév
            // 
            this.VizsgNév.Location = new System.Drawing.Point(204, 305);
            this.VizsgNév.Name = "VizsgNév";
            this.VizsgNév.Size = new System.Drawing.Size(120, 26);
            this.VizsgNév.TabIndex = 5;
            // 
            // Oka
            // 
            this.Oka.Location = new System.Drawing.Point(204, 207);
            this.Oka.MaxLength = 255;
            this.Oka.Multiline = true;
            this.Oka.Name = "Oka";
            this.Oka.Size = new System.Drawing.Size(842, 57);
            this.Oka.TabIndex = 4;
            // 
            // KötöttStart
            // 
            this.KötöttStart.AutoSize = true;
            this.KötöttStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.KötöttStart.Location = new System.Drawing.Point(204, 175);
            this.KötöttStart.Name = "KötöttStart";
            this.KötöttStart.Size = new System.Drawing.Size(217, 24);
            this.KötöttStart.TabIndex = 3;
            this.KötöttStart.Text = "Utolsó Vizsgálattól ütemez";
            this.KötöttStart.UseVisualStyleBackColor = false;
            // 
            // Megállítás
            // 
            this.Megállítás.AutoSize = true;
            this.Megállítás.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Megállítás.Location = new System.Drawing.Point(204, 145);
            this.Megállítás.Name = "Megállítás";
            this.Megállítás.Size = new System.Drawing.Size(108, 24);
            this.Megállítás.TabIndex = 2;
            this.Megállítás.Text = "Ciklus Stop";
            this.Megállítás.UseVisualStyleBackColor = false;
            // 
            // Ciklusrend
            // 
            this.Ciklusrend.FormattingEnabled = true;
            this.Ciklusrend.Location = new System.Drawing.Point(204, 111);
            this.Ciklusrend.Name = "Ciklusrend";
            this.Ciklusrend.Size = new System.Drawing.Size(167, 28);
            this.Ciklusrend.TabIndex = 1;
            this.Ciklusrend.SelectedIndexChanged += new System.EventHandler(this.Ciklusrend_SelectedIndexChanged);
            // 
            // Vizsgdátum
            // 
            this.Vizsgdátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Vizsgdátum.Location = new System.Drawing.Point(204, 338);
            this.Vizsgdátum.Name = "Vizsgdátum";
            this.Vizsgdátum.Size = new System.Drawing.Size(107, 26);
            this.Vizsgdátum.TabIndex = 7;
            // 
            // Start
            // 
            this.Start.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Start.Location = new System.Drawing.Point(204, 79);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(107, 26);
            this.Start.TabIndex = 0;
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Label12.Location = new System.Drawing.Point(6, 119);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(87, 20);
            this.Label12.TabIndex = 5;
            this.Label12.Text = "Ciklusrend:";
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Label11.Location = new System.Drawing.Point(6, 207);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(116, 20);
            this.Label11.TabIndex = 4;
            this.Label11.Text = "Módosítás oka:";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Label10.Location = new System.Drawing.Point(6, 273);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(175, 20);
            this.Label10.TabIndex = 3;
            this.Label10.Text = "Utolsó vizsgálat száma:";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Label9.Location = new System.Drawing.Point(6, 308);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(124, 20);
            this.Label9.TabIndex = 2;
            this.Label9.Text = "Utolsó vizsgálat:";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Label8.Location = new System.Drawing.Point(6, 343);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(161, 20);
            this.Label8.TabIndex = 1;
            this.Label8.Text = "Utolsó vizsgálat ideje:";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Label7.Location = new System.Drawing.Point(6, 85);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(91, 20);
            this.Label7.TabIndex = 0;
            this.Label7.Text = "Ciklus start:";
            // 
            // LapFülek
            // 
            this.LapFülek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LapFülek.Controls.Add(this.TabPage3);
            this.LapFülek.Controls.Add(this.TabPage4);
            this.LapFülek.Controls.Add(this.TabPage1);
            this.LapFülek.Controls.Add(this.TabPage2);
            this.LapFülek.Controls.Add(this.TabPage5);
            this.LapFülek.Controls.Add(this.TabPage6);
            this.LapFülek.Location = new System.Drawing.Point(7, 58);
            this.LapFülek.Name = "LapFülek";
            this.LapFülek.Padding = new System.Drawing.Point(16, 3);
            this.LapFülek.SelectedIndex = 0;
            this.LapFülek.Size = new System.Drawing.Size(1085, 477);
            this.LapFülek.TabIndex = 155;
            this.LapFülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Lapfülek_DrawItem);
            this.LapFülek.SelectedIndexChanged += new System.EventHandler(this.LapFülek_SelectedIndexChanged);
            // 
            // Btn_súgó
            // 
            this.Btn_súgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_súgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.Btn_súgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_súgó.Location = new System.Drawing.Point(1059, 1);
            this.Btn_súgó.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_súgó.Name = "Btn_súgó";
            this.Btn_súgó.Size = new System.Drawing.Size(40, 40);
            this.Btn_súgó.TabIndex = 62;
            this.Btn_súgó.UseVisualStyleBackColor = true;
            this.Btn_súgó.Click += new System.EventHandler(this.Btn_súgó_Click);
            // 
            // Ablak_TW6000_Tulajdonság
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1104, 537);
            this.Controls.Add(this.LapFülek);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Btn_súgó);
            this.Controls.Add(this.Panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_TW6000_Tulajdonság";
            this.Text = "TW6000 Karbantartási adatok";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_TW6000_Tulajdonság_FormClosed);
            this.Load += new System.EventHandler(this.Tulajdonság_TW6000_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.TabPage6.ResumeLayout(false);
            this.Panel6.ResumeLayout(false);
            this.Panel6.PerformLayout();
            this.Panel5.ResumeLayout(false);
            this.Panel5.PerformLayout();
            this.Panel4.ResumeLayout(false);
            this.Panel4.PerformLayout();
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            this.TabPage5.ResumeLayout(false);
            this.TabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ÜtemNapló)).EndInit();
            this.TabPage4.ResumeLayout(false);
            this.TabPage4.PerformLayout();
            this.TabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Táblaütemezés)).EndInit();
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Napló_Tábla)).EndInit();
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            this.LapFülek.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        internal Panel Panel1;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal Button Btn_súgó;
        internal ToolTip ToolTip1;
        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal Label Pá;
        internal ComboBox Pályaszám;
        internal Button Pályaszámkereső;
        internal TabPage TabPage6;
        internal TabPage TabPage5;
        internal TabPage TabPage4;
        internal TabPage TabPage3;
        internal Button Keresés;
        internal Button Excelkimenet;
        internal Button Terv_lista;
        internal Button BtnÜtemÜtemezés;
        internal Button Telephely_lap;
        internal Button BtnSzínező;
        internal Button Ütemfrissít;
        internal DateTimePicker Ütemvége;
        internal DateTimePicker Ütemkezdete;
        internal TabPage TabPage2;
        internal TabPage TabPage1;
        internal Button Járműadatok_rögzít;
        internal TextBox VizsgNév;
        internal TextBox Oka;
        internal CheckBox KötöttStart;
        internal CheckBox Megállítás;
        internal ComboBox Ciklusrend;
        internal DateTimePicker Vizsgdátum;
        internal DateTimePicker Start;
        internal Label Label12;
        internal Label Label11;
        internal Label Label10;
        internal Label Label9;
        internal Label Label8;
        internal Label Label7;
        internal TabControl LapFülek;
        internal DataGridView Napló_Tábla;
        internal Button BtnKarbantartExcel;
        internal Button BtnKarbantartFrissít;
        internal ComboBox NaplóPályaszám;
        internal Label Label14;
        internal DateTimePicker NaplóVége;
        internal DateTimePicker NaplóKezdete;
        internal Label Label24;
        internal Label Label23;
        internal Label Label22;
        internal Label Label21;
        internal ComboBox Üstátus;
        internal DateTimePicker ÜVÜtemezés;
        internal DateTimePicker ÜVElkészülés;
        internal Label Label20;
        internal TextBox ÜVizsgfoka;
        internal ComboBox ÜVVégezte;
        internal TextBox Üazonosító;
        internal ComboBox ÜCiklusrend;
        internal Label Label19;
        internal Label Label18;
        internal Label Label17;
        internal TextBox ÜMegjegyzés;
        internal CheckBox Üelkészült;
        internal ComboBox ÜVSorszám;
        internal DateTimePicker ÜVEsedékesség;
        internal Label Label15;
        internal Label Label16;
        internal Button BtnÜtemRészRögz;
        internal Button BtnÜtemRészTerv;
        internal DataGridView ÜtemNapló;
        internal Button BtnÜtemNaplóExcel;
        internal Button BtnÜtemNaplóFrissít;
        internal ComboBox ÜtemPályaszám;
        internal Label Label25;
        internal DateTimePicker ÜtemNaplóVége;
        internal DateTimePicker ÜtemNaplóKezdet;
        internal Panel Panel4;
        internal DateTimePicker ElőbefejezőDátum;
        internal DateTimePicker Előkezdődátum;
        internal Label Label27;
        internal Panel Panel3;
        internal Button BtnElőtervezőFrissít;
        internal ComboBox Telephely;
        internal Label Label26;
        internal Panel Panel6;
        internal CheckBox Check1;
        internal Panel Panel5;
        internal CheckedListBox VizsgálatLista;
        internal ComboBox ElőCiklusrend;
        internal Label Label28;
        internal Button Mindentkijelöl;
        internal Button Kijelöléstörlése;
        internal Button BtnElőtervezőKeres;
        internal DataGridView Táblaütemezés;
        internal CheckedListBox PszJelölő;
        internal Button BtnÜtemTörlés;
        private ComboBox Vizsgsorszám;
    }
}