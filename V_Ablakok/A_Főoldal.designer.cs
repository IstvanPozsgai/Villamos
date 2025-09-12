using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace Villamos
{
    public partial class A_Főoldal : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(A_Főoldal));
            this.Menü = new System.Windows.Forms.MenuStrip();
            this.ProgramAdatokMenü = new System.Windows.Forms.ToolStripMenuItem();
            this.ablakokBeállításaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gombokBeállításaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator37 = new System.Windows.Forms.ToolStripSeparator();
            this.felhasználókLétrehozásaTörléseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jogosultságKiosztásToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator38 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator36 = new System.Windows.Forms.ToolStripSeparator();
            this.FelhasználókBeállításaMenü = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator39 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ProgramAdatokKiadásiAdatokToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProgramAdatokSzemélyMenü = new System.Windows.Forms.ToolStripMenuItem();
            this.ProgramAdatokEgyébToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.CiklusrendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.VáltósMunkarendÉsTúlóraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.járműTechnológiákToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InformációkMenü = new System.Windows.Forms.ToolStripMenuItem();
            this.ÜzenetekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UtasításokToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DolgozóiAdatokToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.DolgozóFelvételátvételvezénylésToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DolgozóiAlapadatokToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.BeosztásToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BeosztásNaplóToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ListákJelenlétiÍvekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SzabadságTúlóraBetegállományToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.OktatásokToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.LekérdezésekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LétszámGazdálkodásToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TúlóraEllenőrzésToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.MunkalapAdatokkarbantartásaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MunkalapKészítésToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MunkalapDekádolóToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator28 = new System.Windows.Forms.ToolStripSeparator();
            this.karbantartásiMunkalapokToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.JárműAdatokToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator31 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator27 = new System.Windows.Forms.ToolStripSeparator();
            this.AkkumulátorNyilvántartásToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.KerékátmérőNyilvántartásSAPBerendezésekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.kerékesztergálásSzervezésToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kerékesztergálásiAdatokBarossToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator34 = new System.Windows.Forms.ToolStripSeparator();
            this.EsztergaKarbantartásToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator30 = new System.Windows.Forms.ToolStripSeparator();
            this.SérülésNyilvántartásToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator22 = new System.Windows.Forms.ToolStripSeparator();
            this.ReklámNyilvántartásToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.SAPOsztályToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator32 = new System.Windows.Forms.ToolStripSeparator();
            this.TTTPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator35 = new System.Windows.Forms.ToolStripSeparator();
            this.fődarabNótaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KarbantartásToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.JárműKarbantartásiAdatokToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.T5C5AdatokMódosításaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.T5C5FutásnapRögzítésToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.T5C5FutásnapÜtemezésToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.T5C5VJavításÜtemezésToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.T5C5UtastérFűtésToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.TW6000AdatokToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.ICSKCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.FogaskerekűToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator26 = new System.Windows.Forms.ToolStripSeparator();
            this.CAF5CAF9AdatokÉsÜtemezésToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator29 = new System.Windows.Forms.ToolStripSeparator();
            this.nosztalgiaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KiadásiAdatokToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ÁllományTáblaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
            this.JárműLétrehozásMozgásToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.FőkönyvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NapiAdatokToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator25 = new System.Windows.Forms.ToolStripSeparator();
            this.KidobóKészítésToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.MenetkimaradásMenü = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator33 = new System.Windows.Forms.ToolStripSeparator();
            this.DigitálisFőkönyvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.SzerelvényToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.KiadásiForteAdatokToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TelephelyiAdatokÖsszesítéseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FőmérnökségiAdatokToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GondnokságToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BehajtásiEngedélyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KülsősDolgozókBelépésiÉsBehajtásaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            this.ÉpületTakarításTörzsAdatokToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ÉpületTakarításToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
            this.VédőeszközToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
            this.eszközNyilvántartásToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.épületTartozékNyilvántartásToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SzerszámNyilvántartásToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator24 = new System.Windows.Forms.ToolStripSeparator();
            this.RezsiRaktárToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Súgómenü = new System.Windows.Forms.ToolStripMenuItem();
            this.KilépésToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LblÓra = new System.Windows.Forms.Label();
            this.lblVerzió = new System.Windows.Forms.Label();
            this.lbltelephely = new System.Windows.Forms.Label();
            this.Üzenetektext = new System.Windows.Forms.RichTextBox();
            this.Utasításoktext = new System.Windows.Forms.RichTextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.panels4 = new System.Windows.Forms.Label();
            this.panels2 = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtüzsorszám = new System.Windows.Forms.TextBox();
            this.txtutsorszám = new System.Windows.Forms.TextBox();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Alsó = new System.Windows.Forms.GroupBox();
            this.Command9 = new System.Windows.Forms.Button();
            this.Label6 = new System.Windows.Forms.Label();
            this.Rejtett = new System.Windows.Forms.GroupBox();
            this.TároltVerzió = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Verzió_Váltás = new System.Windows.Forms.Button();
            this.BtnHardverkulcs = new System.Windows.Forms.Button();
            this.Rejtett_Frissít = new System.Windows.Forms.Button();
            this.Figyelmeztetés = new System.Windows.Forms.Label();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.Timer2 = new System.Windows.Forms.Timer(this.components);
            this.Btnutasításfrissítés = new System.Windows.Forms.Button();
            this.Btnüzenetfrissítés = new System.Windows.Forms.Button();
            this.Képkeret = new System.Windows.Forms.PictureBox();
            this.Képkeret1 = new System.Windows.Forms.PictureBox();
            this.Panels1 = new System.Windows.Forms.Label();
            this.toolStripSeparator40 = new System.Windows.Forms.ToolStripSeparator();
            this.VételezésMenü = new System.Windows.Forms.ToolStripMenuItem();
            this.Menü.SuspendLayout();
            this.Alsó.SuspendLayout();
            this.Rejtett.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Képkeret)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Képkeret1)).BeginInit();
            this.SuspendLayout();
            // 
            // Menü
            // 
            this.Menü.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Menü.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProgramAdatokMenü,
            this.InformációkMenü,
            this.DolgozóiAdatokToolStripMenuItem1,
            this.JárműAdatokToolStripMenuItem,
            this.KarbantartásToolStripMenuItem,
            this.KiadásiAdatokToolStripMenuItem,
            this.GondnokságToolStripMenuItem,
            this.Súgómenü,
            this.KilépésToolStripMenuItem});
            this.Menü.Location = new System.Drawing.Point(0, 0);
            this.Menü.Name = "Menü";
            this.Menü.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.Menü.Size = new System.Drawing.Size(890, 24);
            this.Menü.TabIndex = 1;
            this.Menü.Text = "MenuStrip1";
            // 
            // ProgramAdatokMenü
            // 
            this.ProgramAdatokMenü.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ablakokBeállításaToolStripMenuItem,
            this.gombokBeállításaToolStripMenuItem,
            this.toolStripSeparator37,
            this.felhasználókLétrehozásaTörléseToolStripMenuItem,
            this.jogosultságKiosztásToolStripMenuItem,
            this.toolStripSeparator38,
            this.toolStripSeparator36,
            this.FelhasználókBeállításaMenü,
            this.toolStripSeparator39,
            this.ToolStripSeparator1,
            this.ProgramAdatokKiadásiAdatokToolStripMenuItem,
            this.ProgramAdatokSzemélyMenü,
            this.ProgramAdatokEgyébToolStripMenuItem,
            this.ToolStripSeparator10,
            this.CiklusrendToolStripMenuItem,
            this.ToolStripSeparator11,
            this.VáltósMunkarendÉsTúlóraToolStripMenuItem,
            this.ToolStripSeparator2,
            this.járműTechnológiákToolStripMenuItem});
            this.ProgramAdatokMenü.Image = global::Villamos.Properties.Resources.Action_configure;
            this.ProgramAdatokMenü.Name = "ProgramAdatokMenü";
            this.ProgramAdatokMenü.Size = new System.Drawing.Size(102, 20);
            this.ProgramAdatokMenü.Text = "Beállítások";
            // 
            // ablakokBeállításaToolStripMenuItem
            // 
            this.ablakokBeállításaToolStripMenuItem.Name = "ablakokBeállításaToolStripMenuItem";
            this.ablakokBeállításaToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.ablakokBeállításaToolStripMenuItem.Text = "Ablakok beállítása";
            this.ablakokBeállításaToolStripMenuItem.Click += new System.EventHandler(this.AblakokBeállításaToolStripMenuItem_Click);
            // 
            // gombokBeállításaToolStripMenuItem
            // 
            this.gombokBeállításaToolStripMenuItem.Name = "gombokBeállításaToolStripMenuItem";
            this.gombokBeállításaToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.gombokBeállításaToolStripMenuItem.Text = "Gombok beállítása";
            this.gombokBeállításaToolStripMenuItem.Click += new System.EventHandler(this.GombokBeállításaToolStripMenuItem_Click);
            // 
            // toolStripSeparator37
            // 
            this.toolStripSeparator37.Name = "toolStripSeparator37";
            this.toolStripSeparator37.Size = new System.Drawing.Size(268, 6);
            // 
            // felhasználókLétrehozásaTörléseToolStripMenuItem
            // 
            this.felhasználókLétrehozásaTörléseToolStripMenuItem.Name = "felhasználókLétrehozásaTörléseToolStripMenuItem";
            this.felhasználókLétrehozásaTörléseToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.felhasználókLétrehozásaTörléseToolStripMenuItem.Text = "Felhasználók létrehozása törlése";
            this.felhasználókLétrehozásaTörléseToolStripMenuItem.Click += new System.EventHandler(this.FelhasználókLétrehozásaTörléseToolStripMenuItem_Click);
            // 
            // jogosultságKiosztásToolStripMenuItem
            // 
            this.jogosultságKiosztásToolStripMenuItem.Name = "jogosultságKiosztásToolStripMenuItem";
            this.jogosultságKiosztásToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.jogosultságKiosztásToolStripMenuItem.Text = "Jogosultság kiosztás";
            this.jogosultságKiosztásToolStripMenuItem.Click += new System.EventHandler(this.JogosultságKiosztásToolStripMenuItem_Click);
            // 
            // toolStripSeparator38
            // 
            this.toolStripSeparator38.Name = "toolStripSeparator38";
            this.toolStripSeparator38.Size = new System.Drawing.Size(268, 6);
            // 
            // toolStripSeparator36
            // 
            this.toolStripSeparator36.Name = "toolStripSeparator36";
            this.toolStripSeparator36.Size = new System.Drawing.Size(268, 6);
            // 
            // FelhasználókBeállításaMenü
            // 
            this.FelhasználókBeállításaMenü.Image = global::Villamos.Properties.Resources.felhasználók;
            this.FelhasználókBeállításaMenü.Name = "FelhasználókBeállításaMenü";
            this.FelhasználókBeállításaMenü.Size = new System.Drawing.Size(271, 22);
            this.FelhasználókBeállításaMenü.Text = "Felhasználók jogosultág ";
            this.FelhasználókBeállításaMenü.Click += new System.EventHandler(this.FelhasználókBeállításaMenü_Click);
            // 
            // toolStripSeparator39
            // 
            this.toolStripSeparator39.Name = "toolStripSeparator39";
            this.toolStripSeparator39.Size = new System.Drawing.Size(268, 6);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(268, 6);
            // 
            // ProgramAdatokKiadásiAdatokToolStripMenuItem
            // 
            this.ProgramAdatokKiadásiAdatokToolStripMenuItem.Image = global::Villamos.Properties.Resources.Aha_Soft_Standard_Transport_Tram;
            this.ProgramAdatokKiadásiAdatokToolStripMenuItem.Name = "ProgramAdatokKiadásiAdatokToolStripMenuItem";
            this.ProgramAdatokKiadásiAdatokToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.ProgramAdatokKiadásiAdatokToolStripMenuItem.Text = "Kiadási adatok beállítása";
            this.ProgramAdatokKiadásiAdatokToolStripMenuItem.Click += new System.EventHandler(this.ProgramAdatokKiadásiAdatokToolStripMenuItem_Click);
            // 
            // ProgramAdatokSzemélyMenü
            // 
            this.ProgramAdatokSzemélyMenü.Image = global::Villamos.Properties.Resources.user_accept_256;
            this.ProgramAdatokSzemélyMenü.Name = "ProgramAdatokSzemélyMenü";
            this.ProgramAdatokSzemélyMenü.Size = new System.Drawing.Size(271, 22);
            this.ProgramAdatokSzemélyMenü.Text = "Személy adatok beállítása";
            this.ProgramAdatokSzemélyMenü.Click += new System.EventHandler(this.ProgramAdatokSzemélyMenü_Click);
            // 
            // ProgramAdatokEgyébToolStripMenuItem
            // 
            this.ProgramAdatokEgyébToolStripMenuItem.Image = global::Villamos.Properties.Resources.Gear_01;
            this.ProgramAdatokEgyébToolStripMenuItem.Name = "ProgramAdatokEgyébToolStripMenuItem";
            this.ProgramAdatokEgyébToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.ProgramAdatokEgyébToolStripMenuItem.Text = "Egyéb adatok beállítása";
            this.ProgramAdatokEgyébToolStripMenuItem.Click += new System.EventHandler(this.ProgramAdatokEgyébToolStripMenuItem_Click);
            // 
            // ToolStripSeparator10
            // 
            this.ToolStripSeparator10.Name = "ToolStripSeparator10";
            this.ToolStripSeparator10.Size = new System.Drawing.Size(268, 6);
            // 
            // CiklusrendToolStripMenuItem
            // 
            this.CiklusrendToolStripMenuItem.Name = "CiklusrendToolStripMenuItem";
            this.CiklusrendToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.CiklusrendToolStripMenuItem.Text = "Ciklusrend";
            this.CiklusrendToolStripMenuItem.Click += new System.EventHandler(this.CiklusrendToolStripMenuItem_Click);
            // 
            // ToolStripSeparator11
            // 
            this.ToolStripSeparator11.Name = "ToolStripSeparator11";
            this.ToolStripSeparator11.Size = new System.Drawing.Size(268, 6);
            // 
            // VáltósMunkarendÉsTúlóraToolStripMenuItem
            // 
            this.VáltósMunkarendÉsTúlóraToolStripMenuItem.Name = "VáltósMunkarendÉsTúlóraToolStripMenuItem";
            this.VáltósMunkarendÉsTúlóraToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.VáltósMunkarendÉsTúlóraToolStripMenuItem.Text = "Váltós munkarend és Túlóra";
            this.VáltósMunkarendÉsTúlóraToolStripMenuItem.Click += new System.EventHandler(this.VáltósMunkarendÉsTúlóraToolStripMenuItem_Click);
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(268, 6);
            // 
            // járműTechnológiákToolStripMenuItem
            // 
            this.járműTechnológiákToolStripMenuItem.Image = global::Villamos.Properties.Resources.Action_configure;
            this.járműTechnológiákToolStripMenuItem.Name = "járműTechnológiákToolStripMenuItem";
            this.járműTechnológiákToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.járműTechnológiákToolStripMenuItem.Text = "Jármű technológiák";
            this.járműTechnológiákToolStripMenuItem.Click += new System.EventHandler(this.JárműTechnológiákToolStripMenuItem_Click);
            // 
            // InformációkMenü
            // 
            this.InformációkMenü.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ÜzenetekToolStripMenuItem,
            this.UtasításokToolStripMenuItem});
            this.InformációkMenü.Image = global::Villamos.Properties.Resources.Document_write;
            this.InformációkMenü.Name = "InformációkMenü";
            this.InformációkMenü.Size = new System.Drawing.Size(104, 20);
            this.InformációkMenü.Text = "Információk";
            // 
            // ÜzenetekToolStripMenuItem
            // 
            this.ÜzenetekToolStripMenuItem.Image = global::Villamos.Properties.Resources.Document_write;
            this.ÜzenetekToolStripMenuItem.Name = "ÜzenetekToolStripMenuItem";
            this.ÜzenetekToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.ÜzenetekToolStripMenuItem.Text = "Üzenetek";
            this.ÜzenetekToolStripMenuItem.Click += new System.EventHandler(this.ÜzenetekToolStripMenuItem_Click);
            // 
            // UtasításokToolStripMenuItem
            // 
            this.UtasításokToolStripMenuItem.Image = global::Villamos.Properties.Resources.Document_write;
            this.UtasításokToolStripMenuItem.Name = "UtasításokToolStripMenuItem";
            this.UtasításokToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.UtasításokToolStripMenuItem.Text = "Utasítások";
            this.UtasításokToolStripMenuItem.Click += new System.EventHandler(this.UtasításokToolStripMenuItem_Click);
            // 
            // DolgozóiAdatokToolStripMenuItem1
            // 
            this.DolgozóiAdatokToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DolgozóFelvételátvételvezénylésToolStripMenuItem,
            this.DolgozóiAlapadatokToolStripMenuItem,
            this.ToolStripSeparator3,
            this.BeosztásToolStripMenuItem,
            this.BeosztásNaplóToolStripMenuItem,
            this.ListákJelenlétiÍvekToolStripMenuItem,
            this.SzabadságTúlóraBetegállományToolStripMenuItem,
            this.ToolStripSeparator4,
            this.OktatásokToolStripMenuItem,
            this.ToolStripSeparator5,
            this.LekérdezésekToolStripMenuItem,
            this.LétszámGazdálkodásToolStripMenuItem,
            this.TúlóraEllenőrzésToolStripMenuItem,
            this.ToolStripSeparator6,
            this.MunkalapAdatokkarbantartásaToolStripMenuItem,
            this.MunkalapKészítésToolStripMenuItem,
            this.MunkalapDekádolóToolStripMenuItem,
            this.toolStripSeparator28,
            this.karbantartásiMunkalapokToolStripMenuItem});
            this.DolgozóiAdatokToolStripMenuItem1.Name = "DolgozóiAdatokToolStripMenuItem1";
            this.DolgozóiAdatokToolStripMenuItem1.Size = new System.Drawing.Size(118, 20);
            this.DolgozóiAdatokToolStripMenuItem1.Text = "Dolgozói adatok";
            // 
            // DolgozóFelvételátvételvezénylésToolStripMenuItem
            // 
            this.DolgozóFelvételátvételvezénylésToolStripMenuItem.Image = global::Villamos.Properties.Resources.felhasználók;
            this.DolgozóFelvételátvételvezénylésToolStripMenuItem.Name = "DolgozóFelvételátvételvezénylésToolStripMenuItem";
            this.DolgozóFelvételátvételvezénylésToolStripMenuItem.Size = new System.Drawing.Size(291, 22);
            this.DolgozóFelvételátvételvezénylésToolStripMenuItem.Text = "Dolgozó felvétel-átvétel-vezénylés";
            this.DolgozóFelvételátvételvezénylésToolStripMenuItem.Click += new System.EventHandler(this.DolgozóFelvételátvételvezénylésToolStripMenuItem_Click);
            // 
            // DolgozóiAlapadatokToolStripMenuItem
            // 
            this.DolgozóiAlapadatokToolStripMenuItem.Image = global::Villamos.Properties.Resources.felhasználók32;
            this.DolgozóiAlapadatokToolStripMenuItem.Name = "DolgozóiAlapadatokToolStripMenuItem";
            this.DolgozóiAlapadatokToolStripMenuItem.Size = new System.Drawing.Size(291, 22);
            this.DolgozóiAlapadatokToolStripMenuItem.Text = "Dolgozói alapadatok";
            this.DolgozóiAlapadatokToolStripMenuItem.Click += new System.EventHandler(this.DolgozóiAlapadatokToolStripMenuItem_Click);
            // 
            // ToolStripSeparator3
            // 
            this.ToolStripSeparator3.Name = "ToolStripSeparator3";
            this.ToolStripSeparator3.Size = new System.Drawing.Size(288, 6);
            // 
            // BeosztásToolStripMenuItem
            // 
            this.BeosztásToolStripMenuItem.Image = global::Villamos.Properties.Resources.felhasználók;
            this.BeosztásToolStripMenuItem.Name = "BeosztásToolStripMenuItem";
            this.BeosztásToolStripMenuItem.Size = new System.Drawing.Size(291, 22);
            this.BeosztásToolStripMenuItem.Text = "Beosztás";
            this.BeosztásToolStripMenuItem.Click += new System.EventHandler(this.BeosztásToolStripMenuItem_Click);
            // 
            // BeosztásNaplóToolStripMenuItem
            // 
            this.BeosztásNaplóToolStripMenuItem.Image = global::Villamos.Properties.Resources.felhasználók;
            this.BeosztásNaplóToolStripMenuItem.Name = "BeosztásNaplóToolStripMenuItem";
            this.BeosztásNaplóToolStripMenuItem.Size = new System.Drawing.Size(291, 22);
            this.BeosztásNaplóToolStripMenuItem.Text = "Beosztás napló";
            this.BeosztásNaplóToolStripMenuItem.Click += new System.EventHandler(this.BeosztásNaplóToolStripMenuItem_Click);
            // 
            // ListákJelenlétiÍvekToolStripMenuItem
            // 
            this.ListákJelenlétiÍvekToolStripMenuItem.Image = global::Villamos.Properties.Resources.Yellow_Glass_Folders_Icon_28;
            this.ListákJelenlétiÍvekToolStripMenuItem.Name = "ListákJelenlétiÍvekToolStripMenuItem";
            this.ListákJelenlétiÍvekToolStripMenuItem.Size = new System.Drawing.Size(291, 22);
            this.ListákJelenlétiÍvekToolStripMenuItem.Text = "Jelenléti ívek ";
            this.ListákJelenlétiÍvekToolStripMenuItem.Click += new System.EventHandler(this.ListákJelenlétiÍvekToolStripMenuItem_Click);
            // 
            // SzabadságTúlóraBetegállományToolStripMenuItem
            // 
            this.SzabadságTúlóraBetegállományToolStripMenuItem.Image = global::Villamos.Properties.Resources.folder_blue_printer;
            this.SzabadságTúlóraBetegállományToolStripMenuItem.Name = "SzabadságTúlóraBetegállományToolStripMenuItem";
            this.SzabadságTúlóraBetegállományToolStripMenuItem.Size = new System.Drawing.Size(291, 22);
            this.SzabadságTúlóraBetegállományToolStripMenuItem.Text = "Szabadság, túlóra, betegállomány";
            this.SzabadságTúlóraBetegállományToolStripMenuItem.Click += new System.EventHandler(this.SzabadságTúlóraBetegállományToolStripMenuItem_Click_1);
            // 
            // ToolStripSeparator4
            // 
            this.ToolStripSeparator4.Name = "ToolStripSeparator4";
            this.ToolStripSeparator4.Size = new System.Drawing.Size(288, 6);
            // 
            // OktatásokToolStripMenuItem
            // 
            this.OktatásokToolStripMenuItem.Image = global::Villamos.Properties.Resources.App_warehause;
            this.OktatásokToolStripMenuItem.Name = "OktatásokToolStripMenuItem";
            this.OktatásokToolStripMenuItem.Size = new System.Drawing.Size(291, 22);
            this.OktatásokToolStripMenuItem.Text = "Oktatások";
            this.OktatásokToolStripMenuItem.Click += new System.EventHandler(this.OktatásokToolStripMenuItem_Click);
            // 
            // ToolStripSeparator5
            // 
            this.ToolStripSeparator5.Name = "ToolStripSeparator5";
            this.ToolStripSeparator5.Size = new System.Drawing.Size(288, 6);
            // 
            // LekérdezésekToolStripMenuItem
            // 
            this.LekérdezésekToolStripMenuItem.Image = global::Villamos.Properties.Resources.felhasználók;
            this.LekérdezésekToolStripMenuItem.Name = "LekérdezésekToolStripMenuItem";
            this.LekérdezésekToolStripMenuItem.Size = new System.Drawing.Size(291, 22);
            this.LekérdezésekToolStripMenuItem.Text = "Dolgozói lekérdezések";
            this.LekérdezésekToolStripMenuItem.Click += new System.EventHandler(this.LekérdezésekToolStripMenuItem_Click);
            // 
            // LétszámGazdálkodásToolStripMenuItem
            // 
            this.LétszámGazdálkodásToolStripMenuItem.Image = global::Villamos.Properties.Resources.felhasználók;
            this.LétszámGazdálkodásToolStripMenuItem.Name = "LétszámGazdálkodásToolStripMenuItem";
            this.LétszámGazdálkodásToolStripMenuItem.Size = new System.Drawing.Size(291, 22);
            this.LétszámGazdálkodásToolStripMenuItem.Text = "Létszám gazdálkodás";
            this.LétszámGazdálkodásToolStripMenuItem.Click += new System.EventHandler(this.LétszámGazdálkodásToolStripMenuItem_Click);
            // 
            // TúlóraEllenőrzésToolStripMenuItem
            // 
            this.TúlóraEllenőrzésToolStripMenuItem.Image = global::Villamos.Properties.Resources.felhasználók;
            this.TúlóraEllenőrzésToolStripMenuItem.Name = "TúlóraEllenőrzésToolStripMenuItem";
            this.TúlóraEllenőrzésToolStripMenuItem.Size = new System.Drawing.Size(291, 22);
            this.TúlóraEllenőrzésToolStripMenuItem.Text = "Munkaidő keret és Túlóra ellenőrzés";
            this.TúlóraEllenőrzésToolStripMenuItem.Click += new System.EventHandler(this.TúlóraEllenőrzésToolStripMenuItem_Click);
            // 
            // ToolStripSeparator6
            // 
            this.ToolStripSeparator6.Name = "ToolStripSeparator6";
            this.ToolStripSeparator6.Size = new System.Drawing.Size(288, 6);
            // 
            // MunkalapAdatokkarbantartásaToolStripMenuItem
            // 
            this.MunkalapAdatokkarbantartásaToolStripMenuItem.Image = global::Villamos.Properties.Resources.App_edit;
            this.MunkalapAdatokkarbantartásaToolStripMenuItem.Name = "MunkalapAdatokkarbantartásaToolStripMenuItem";
            this.MunkalapAdatokkarbantartásaToolStripMenuItem.Size = new System.Drawing.Size(291, 22);
            this.MunkalapAdatokkarbantartásaToolStripMenuItem.Text = "Munkalap adatok karbantartása";
            this.MunkalapAdatokkarbantartásaToolStripMenuItem.Click += new System.EventHandler(this.MunkalapAdatokkarbantartásaToolStripMenuItem_Click);
            // 
            // MunkalapKészítésToolStripMenuItem
            // 
            this.MunkalapKészítésToolStripMenuItem.Image = global::Villamos.Properties.Resources.App_edit;
            this.MunkalapKészítésToolStripMenuItem.Name = "MunkalapKészítésToolStripMenuItem";
            this.MunkalapKészítésToolStripMenuItem.Size = new System.Drawing.Size(291, 22);
            this.MunkalapKészítésToolStripMenuItem.Text = "Munkalap készítés";
            this.MunkalapKészítésToolStripMenuItem.Click += new System.EventHandler(this.MunkalapKészítésToolStripMenuItem_Click);
            // 
            // MunkalapDekádolóToolStripMenuItem
            // 
            this.MunkalapDekádolóToolStripMenuItem.Image = global::Villamos.Properties.Resources.App_edit;
            this.MunkalapDekádolóToolStripMenuItem.Name = "MunkalapDekádolóToolStripMenuItem";
            this.MunkalapDekádolóToolStripMenuItem.Size = new System.Drawing.Size(291, 22);
            this.MunkalapDekádolóToolStripMenuItem.Text = "Munkalap elszámolás ";
            this.MunkalapDekádolóToolStripMenuItem.Click += new System.EventHandler(this.MunkalapDekádolóToolStripMenuItem_Click);
            // 
            // toolStripSeparator28
            // 
            this.toolStripSeparator28.Name = "toolStripSeparator28";
            this.toolStripSeparator28.Size = new System.Drawing.Size(288, 6);
            // 
            // karbantartásiMunkalapokToolStripMenuItem
            // 
            this.karbantartásiMunkalapokToolStripMenuItem.Image = global::Villamos.Properties.Resources.Action_configure;
            this.karbantartásiMunkalapokToolStripMenuItem.Name = "karbantartásiMunkalapokToolStripMenuItem";
            this.karbantartásiMunkalapokToolStripMenuItem.Size = new System.Drawing.Size(291, 22);
            this.karbantartásiMunkalapokToolStripMenuItem.Text = "Karbantartási Munkalapok";
            this.karbantartásiMunkalapokToolStripMenuItem.Click += new System.EventHandler(this.KarbantartásiMunkalapokToolStripMenuItem_Click);
            // 
            // JárműAdatokToolStripMenuItem
            // 
            this.JárműAdatokToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator31,
            this.ToolStripMenuItem2,
            this.ToolStripSeparator27,
            this.AkkumulátorNyilvántartásToolStripMenuItem1,
            this.ToolStripSeparator7,
            this.KerékátmérőNyilvántartásSAPBerendezésekToolStripMenuItem,
            this.ToolStripMenuItem1,
            this.kerékesztergálásSzervezésToolStripMenuItem,
            this.kerékesztergálásiAdatokBarossToolStripMenuItem,
            this.toolStripSeparator34,
            this.EsztergaKarbantartásToolStripMenuItem,
            this.toolStripSeparator30,
            this.SérülésNyilvántartásToolStripMenuItem,
            this.ToolStripSeparator22,
            this.ReklámNyilvántartásToolStripMenuItem,
            this.ToolStripSeparator18,
            this.SAPOsztályToolStripMenuItem,
            this.toolStripSeparator32,
            this.TTTPToolStripMenuItem,
            this.toolStripSeparator35,
            this.fődarabNótaToolStripMenuItem,
            this.toolStripSeparator40,
            this.VételezésMenü});
            this.JárműAdatokToolStripMenuItem.Name = "JárműAdatokToolStripMenuItem";
            this.JárműAdatokToolStripMenuItem.Size = new System.Drawing.Size(112, 20);
            this.JárműAdatokToolStripMenuItem.Text = "Nyilvántartások";
            // 
            // toolStripSeparator31
            // 
            this.toolStripSeparator31.Name = "toolStripSeparator31";
            this.toolStripSeparator31.Size = new System.Drawing.Size(350, 6);
            // 
            // ToolStripMenuItem2
            // 
            this.ToolStripMenuItem2.Image = global::Villamos.Properties.Resources.takarítás;
            this.ToolStripMenuItem2.Name = "ToolStripMenuItem2";
            this.ToolStripMenuItem2.Size = new System.Drawing.Size(353, 22);
            this.ToolStripMenuItem2.Text = "Takarítás Jármű";
            this.ToolStripMenuItem2.Click += new System.EventHandler(this.ToolStripMenuItem2_Click);
            // 
            // ToolStripSeparator27
            // 
            this.ToolStripSeparator27.Name = "ToolStripSeparator27";
            this.ToolStripSeparator27.Size = new System.Drawing.Size(350, 6);
            // 
            // AkkumulátorNyilvántartásToolStripMenuItem1
            // 
            this.AkkumulátorNyilvántartásToolStripMenuItem1.Image = global::Villamos.Properties.Resources.battery_2;
            this.AkkumulátorNyilvántartásToolStripMenuItem1.Name = "AkkumulátorNyilvántartásToolStripMenuItem1";
            this.AkkumulátorNyilvántartásToolStripMenuItem1.Size = new System.Drawing.Size(353, 22);
            this.AkkumulátorNyilvántartásToolStripMenuItem1.Text = "Akkumulátor nyilvántartás";
            this.AkkumulátorNyilvántartásToolStripMenuItem1.Click += new System.EventHandler(this.AkkumulátorNyilvántartásToolStripMenuItem1_Click);
            // 
            // ToolStripSeparator7
            // 
            this.ToolStripSeparator7.Name = "ToolStripSeparator7";
            this.ToolStripSeparator7.Size = new System.Drawing.Size(350, 6);
            // 
            // KerékátmérőNyilvántartásSAPBerendezésekToolStripMenuItem
            // 
            this.KerékátmérőNyilvántartásSAPBerendezésekToolStripMenuItem.Image = global::Villamos.Properties.Resources.kerék24;
            this.KerékátmérőNyilvántartásSAPBerendezésekToolStripMenuItem.Name = "KerékátmérőNyilvántartásSAPBerendezésekToolStripMenuItem";
            this.KerékátmérőNyilvántartásSAPBerendezésekToolStripMenuItem.Size = new System.Drawing.Size(353, 22);
            this.KerékátmérőNyilvántartásSAPBerendezésekToolStripMenuItem.Text = "Kerékátmérő nyilvántartás- SAP berendezések";
            this.KerékátmérőNyilvántartásSAPBerendezésekToolStripMenuItem.Click += new System.EventHandler(this.KerékátmérőNyilvántartásSAPBerendezésekToolStripMenuItem_Click);
            // 
            // ToolStripMenuItem1
            // 
            this.ToolStripMenuItem1.Name = "ToolStripMenuItem1";
            this.ToolStripMenuItem1.Size = new System.Drawing.Size(353, 22);
            this.ToolStripMenuItem1.Text = "MEO Kerékmérések";
            this.ToolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuItem1_Click);
            // 
            // kerékesztergálásSzervezésToolStripMenuItem
            // 
            this.kerékesztergálásSzervezésToolStripMenuItem.Name = "kerékesztergálásSzervezésToolStripMenuItem";
            this.kerékesztergálásSzervezésToolStripMenuItem.Size = new System.Drawing.Size(353, 22);
            this.kerékesztergálásSzervezésToolStripMenuItem.Text = "Baross Kerékesztergálás";
            this.kerékesztergálásSzervezésToolStripMenuItem.Click += new System.EventHandler(this.KerékesztergálásSzervezésToolStripMenuItem_Click);
            // 
            // kerékesztergálásiAdatokBarossToolStripMenuItem
            // 
            this.kerékesztergálásiAdatokBarossToolStripMenuItem.Name = "kerékesztergálásiAdatokBarossToolStripMenuItem";
            this.kerékesztergálásiAdatokBarossToolStripMenuItem.Size = new System.Drawing.Size(353, 22);
            this.kerékesztergálásiAdatokBarossToolStripMenuItem.Text = "Kerékesztergálási Adatok Baross";
            this.kerékesztergálásiAdatokBarossToolStripMenuItem.Click += new System.EventHandler(this.KerékesztergálásiAdatokBarossToolStripMenuItem_Click);
            // 
            // toolStripSeparator34
            // 
            this.toolStripSeparator34.Name = "toolStripSeparator34";
            this.toolStripSeparator34.Size = new System.Drawing.Size(350, 6);
            // 
            // EsztergaKarbantartásToolStripMenuItem
            // 
            this.EsztergaKarbantartásToolStripMenuItem.Name = "EsztergaKarbantartásToolStripMenuItem";
            this.EsztergaKarbantartásToolStripMenuItem.Size = new System.Drawing.Size(353, 22);
            this.EsztergaKarbantartásToolStripMenuItem.Text = "Eszterga Karbantartás";
            this.EsztergaKarbantartásToolStripMenuItem.Click += new System.EventHandler(this.EsztergaKarbantartásToolStripMenuItem_Click);
            // 
            // toolStripSeparator30
            // 
            this.toolStripSeparator30.Name = "toolStripSeparator30";
            this.toolStripSeparator30.Size = new System.Drawing.Size(350, 6);
            // 
            // SérülésNyilvántartásToolStripMenuItem
            // 
            this.SérülésNyilvántartásToolStripMenuItem.Image = global::Villamos.Properties.Resources.Atyourservice_Service_Categories_Car_Repair;
            this.SérülésNyilvántartásToolStripMenuItem.Name = "SérülésNyilvántartásToolStripMenuItem";
            this.SérülésNyilvántartásToolStripMenuItem.Size = new System.Drawing.Size(353, 22);
            this.SérülésNyilvántartásToolStripMenuItem.Text = "Sérülés nyilvántartás";
            this.SérülésNyilvántartásToolStripMenuItem.Click += new System.EventHandler(this.SérülésNyilvántartásToolStripMenuItem_Click);
            // 
            // ToolStripSeparator22
            // 
            this.ToolStripSeparator22.Name = "ToolStripSeparator22";
            this.ToolStripSeparator22.Size = new System.Drawing.Size(350, 6);
            // 
            // ReklámNyilvántartásToolStripMenuItem
            // 
            this.ReklámNyilvántartásToolStripMenuItem.Name = "ReklámNyilvántartásToolStripMenuItem";
            this.ReklámNyilvántartásToolStripMenuItem.Size = new System.Drawing.Size(353, 22);
            this.ReklámNyilvántartásToolStripMenuItem.Text = "Reklám nyilvántartás";
            this.ReklámNyilvántartásToolStripMenuItem.Click += new System.EventHandler(this.ReklámNyilvántartásToolStripMenuItem_Click);
            // 
            // ToolStripSeparator18
            // 
            this.ToolStripSeparator18.Name = "ToolStripSeparator18";
            this.ToolStripSeparator18.Size = new System.Drawing.Size(350, 6);
            // 
            // SAPOsztályToolStripMenuItem
            // 
            this.SAPOsztályToolStripMenuItem.Name = "SAPOsztályToolStripMenuItem";
            this.SAPOsztályToolStripMenuItem.Size = new System.Drawing.Size(353, 22);
            this.SAPOsztályToolStripMenuItem.Text = "SAP Osztály";
            this.SAPOsztályToolStripMenuItem.Click += new System.EventHandler(this.SAPOsztályToolStripMenuItem_Click);
            // 
            // toolStripSeparator32
            // 
            this.toolStripSeparator32.Name = "toolStripSeparator32";
            this.toolStripSeparator32.Size = new System.Drawing.Size(350, 6);
            // 
            // TTTPToolStripMenuItem
            // 
            this.TTTPToolStripMenuItem.Name = "TTTPToolStripMenuItem";
            this.TTTPToolStripMenuItem.Size = new System.Drawing.Size(353, 22);
            this.TTTPToolStripMenuItem.Text = "TTP vizsgálatok";
            this.TTTPToolStripMenuItem.Click += new System.EventHandler(this.TTPToolStripMenuItem_Click);
            // 
            // toolStripSeparator35
            // 
            this.toolStripSeparator35.Name = "toolStripSeparator35";
            this.toolStripSeparator35.Size = new System.Drawing.Size(350, 6);
            // 
            // fődarabNótaToolStripMenuItem
            // 
            this.fődarabNótaToolStripMenuItem.Name = "fődarabNótaToolStripMenuItem";
            this.fődarabNótaToolStripMenuItem.Size = new System.Drawing.Size(353, 22);
            this.fődarabNótaToolStripMenuItem.Text = "Fődarab Nóta";
            this.fődarabNótaToolStripMenuItem.Click += new System.EventHandler(this.FődarabNótaToolStripMenuItem_Click);
            // 
            // KarbantartásToolStripMenuItem
            // 
            this.KarbantartásToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.JárműKarbantartásiAdatokToolStripMenuItem,
            this.ToolStripSeparator9,
            this.T5C5AdatokMódosításaToolStripMenuItem,
            this.T5C5FutásnapRögzítésToolStripMenuItem,
            this.T5C5FutásnapÜtemezésToolStripMenuItem,
            this.T5C5VJavításÜtemezésToolStripMenuItem,
            this.T5C5UtastérFűtésToolStripMenuItem,
            this.ToolStripSeparator13,
            this.TW6000AdatokToolStripMenuItem,
            this.ToolStripSeparator15,
            this.ICSKCSVToolStripMenuItem,
            this.ToolStripSeparator16,
            this.FogaskerekűToolStripMenuItem,
            this.ToolStripSeparator26,
            this.CAF5CAF9AdatokÉsÜtemezésToolStripMenuItem,
            this.toolStripSeparator29,
            this.nosztalgiaToolStripMenuItem});
            this.KarbantartásToolStripMenuItem.Name = "KarbantartásToolStripMenuItem";
            this.KarbantartásToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.KarbantartásToolStripMenuItem.Text = "Karbantartás";
            // 
            // JárműKarbantartásiAdatokToolStripMenuItem
            // 
            this.JárműKarbantartásiAdatokToolStripMenuItem.Image = global::Villamos.Properties.Resources.Action_configure;
            this.JárműKarbantartásiAdatokToolStripMenuItem.Name = "JárműKarbantartásiAdatokToolStripMenuItem";
            this.JárműKarbantartásiAdatokToolStripMenuItem.Size = new System.Drawing.Size(278, 22);
            this.JárműKarbantartásiAdatokToolStripMenuItem.Text = "Jármű karbantartási adatok";
            this.JárműKarbantartásiAdatokToolStripMenuItem.Click += new System.EventHandler(this.JárműKarbantartásiAdatokToolStripMenuItem_Click_1);
            // 
            // ToolStripSeparator9
            // 
            this.ToolStripSeparator9.Name = "ToolStripSeparator9";
            this.ToolStripSeparator9.Size = new System.Drawing.Size(275, 6);
            // 
            // T5C5AdatokMódosításaToolStripMenuItem
            // 
            this.T5C5AdatokMódosításaToolStripMenuItem.Image = global::Villamos.Properties.Resources.CKD;
            this.T5C5AdatokMódosításaToolStripMenuItem.Name = "T5C5AdatokMódosításaToolStripMenuItem";
            this.T5C5AdatokMódosításaToolStripMenuItem.Size = new System.Drawing.Size(278, 22);
            this.T5C5AdatokMódosításaToolStripMenuItem.Text = "T5C5 adatok módosítása";
            this.T5C5AdatokMódosításaToolStripMenuItem.Click += new System.EventHandler(this.T5C5AdatokMódosításaToolStripMenuItem_Click);
            // 
            // T5C5FutásnapRögzítésToolStripMenuItem
            // 
            this.T5C5FutásnapRögzítésToolStripMenuItem.Image = global::Villamos.Properties.Resources.CKD;
            this.T5C5FutásnapRögzítésToolStripMenuItem.Name = "T5C5FutásnapRögzítésToolStripMenuItem";
            this.T5C5FutásnapRögzítésToolStripMenuItem.Size = new System.Drawing.Size(278, 22);
            this.T5C5FutásnapRögzítésToolStripMenuItem.Text = "T5C5 futásnap rögzítés";
            this.T5C5FutásnapRögzítésToolStripMenuItem.Click += new System.EventHandler(this.T5C5FutásnapRögzítésToolStripMenuItem_Click);
            // 
            // T5C5FutásnapÜtemezésToolStripMenuItem
            // 
            this.T5C5FutásnapÜtemezésToolStripMenuItem.Image = global::Villamos.Properties.Resources.CKD;
            this.T5C5FutásnapÜtemezésToolStripMenuItem.Name = "T5C5FutásnapÜtemezésToolStripMenuItem";
            this.T5C5FutásnapÜtemezésToolStripMenuItem.Size = new System.Drawing.Size(278, 22);
            this.T5C5FutásnapÜtemezésToolStripMenuItem.Text = "T5C5 futásnap ütemezés";
            this.T5C5FutásnapÜtemezésToolStripMenuItem.Click += new System.EventHandler(this.T5C5FutásnapÜtemezésToolStripMenuItem_Click);
            // 
            // T5C5VJavításÜtemezésToolStripMenuItem
            // 
            this.T5C5VJavításÜtemezésToolStripMenuItem.Image = global::Villamos.Properties.Resources.CKD;
            this.T5C5VJavításÜtemezésToolStripMenuItem.Name = "T5C5VJavításÜtemezésToolStripMenuItem";
            this.T5C5VJavításÜtemezésToolStripMenuItem.Size = new System.Drawing.Size(278, 22);
            this.T5C5VJavításÜtemezésToolStripMenuItem.Text = "T5C5 V javítás ütemezés";
            this.T5C5VJavításÜtemezésToolStripMenuItem.Click += new System.EventHandler(this.T5C5VJavításÜtemezésToolStripMenuItem_Click);
            // 
            // T5C5UtastérFűtésToolStripMenuItem
            // 
            this.T5C5UtastérFűtésToolStripMenuItem.Image = global::Villamos.Properties.Resources.CKD;
            this.T5C5UtastérFűtésToolStripMenuItem.Name = "T5C5UtastérFűtésToolStripMenuItem";
            this.T5C5UtastérFűtésToolStripMenuItem.Size = new System.Drawing.Size(278, 22);
            this.T5C5UtastérFűtésToolStripMenuItem.Text = "T5C5 utastér fűtés";
            this.T5C5UtastérFűtésToolStripMenuItem.Click += new System.EventHandler(this.T5C5UtastérFűtésToolStripMenuItem_Click);
            // 
            // ToolStripSeparator13
            // 
            this.ToolStripSeparator13.Name = "ToolStripSeparator13";
            this.ToolStripSeparator13.Size = new System.Drawing.Size(275, 6);
            // 
            // TW6000AdatokToolStripMenuItem
            // 
            this.TW6000AdatokToolStripMenuItem.Image = global::Villamos.Properties.Resources.TW6000_;
            this.TW6000AdatokToolStripMenuItem.Name = "TW6000AdatokToolStripMenuItem";
            this.TW6000AdatokToolStripMenuItem.Size = new System.Drawing.Size(278, 22);
            this.TW6000AdatokToolStripMenuItem.Text = "TW6000 adatok és ütemezés";
            this.TW6000AdatokToolStripMenuItem.Click += new System.EventHandler(this.TW6000AdatokToolStripMenuItem_Click);
            // 
            // ToolStripSeparator15
            // 
            this.ToolStripSeparator15.Name = "ToolStripSeparator15";
            this.ToolStripSeparator15.Size = new System.Drawing.Size(275, 6);
            // 
            // ICSKCSVToolStripMenuItem
            // 
            this.ICSKCSVToolStripMenuItem.Image = global::Villamos.Properties.Resources.GANZ48;
            this.ICSKCSVToolStripMenuItem.Name = "ICSKCSVToolStripMenuItem";
            this.ICSKCSVToolStripMenuItem.Size = new System.Drawing.Size(278, 22);
            this.ICSKCSVToolStripMenuItem.Text = "ICS-KCSV";
            this.ICSKCSVToolStripMenuItem.Click += new System.EventHandler(this.ICSKCSVToolStripMenuItem_Click);
            // 
            // ToolStripSeparator16
            // 
            this.ToolStripSeparator16.Name = "ToolStripSeparator16";
            this.ToolStripSeparator16.Size = new System.Drawing.Size(275, 6);
            // 
            // FogaskerekűToolStripMenuItem
            // 
            this.FogaskerekűToolStripMenuItem.Image = global::Villamos.Properties.Resources.fogas_logo;
            this.FogaskerekűToolStripMenuItem.Name = "FogaskerekűToolStripMenuItem";
            this.FogaskerekűToolStripMenuItem.Size = new System.Drawing.Size(278, 22);
            this.FogaskerekűToolStripMenuItem.Text = "Fogaskerekű adatok és ütemezés";
            this.FogaskerekűToolStripMenuItem.Click += new System.EventHandler(this.FogaskerekűToolStripMenuItem_Click);
            // 
            // ToolStripSeparator26
            // 
            this.ToolStripSeparator26.Name = "ToolStripSeparator26";
            this.ToolStripSeparator26.Size = new System.Drawing.Size(275, 6);
            // 
            // CAF5CAF9AdatokÉsÜtemezésToolStripMenuItem
            // 
            this.CAF5CAF9AdatokÉsÜtemezésToolStripMenuItem.Image = global::Villamos.Properties.Resources.CAF;
            this.CAF5CAF9AdatokÉsÜtemezésToolStripMenuItem.Name = "CAF5CAF9AdatokÉsÜtemezésToolStripMenuItem";
            this.CAF5CAF9AdatokÉsÜtemezésToolStripMenuItem.Size = new System.Drawing.Size(278, 22);
            this.CAF5CAF9AdatokÉsÜtemezésToolStripMenuItem.Text = "CAF5-CAF9 adatok és ütemezés";
            this.CAF5CAF9AdatokÉsÜtemezésToolStripMenuItem.Click += new System.EventHandler(this.CAF5CAF9AdatokÉsÜtemezésToolStripMenuItem_Click);
            // 
            // toolStripSeparator29
            // 
            this.toolStripSeparator29.Name = "toolStripSeparator29";
            this.toolStripSeparator29.Size = new System.Drawing.Size(275, 6);
            // 
            // nosztalgiaToolStripMenuItem
            // 
            this.nosztalgiaToolStripMenuItem.Name = "nosztalgiaToolStripMenuItem";
            this.nosztalgiaToolStripMenuItem.Size = new System.Drawing.Size(278, 22);
            this.nosztalgiaToolStripMenuItem.Text = "Nosztalgia";
            this.nosztalgiaToolStripMenuItem.Click += new System.EventHandler(this.NosztalgiaToolStripMenuItem_Click);
            // 
            // KiadásiAdatokToolStripMenuItem
            // 
            this.KiadásiAdatokToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ÁllományTáblaToolStripMenuItem,
            this.ToolStripSeparator23,
            this.JárműLétrehozásMozgásToolStripMenuItem,
            this.ToolStripSeparator14,
            this.FőkönyvToolStripMenuItem,
            this.NapiAdatokToolStripMenuItem,
            this.ToolStripSeparator25,
            this.KidobóKészítésToolStripMenuItem,
            this.ToolStripSeparator12,
            this.MenetkimaradásMenü,
            this.ToolStripSeparator33,
            this.DigitálisFőkönyvToolStripMenuItem,
            this.ToolStripSeparator8,
            this.SzerelvényToolStripMenuItem,
            this.ToolStripSeparator17,
            this.KiadásiForteAdatokToolStripMenuItem,
            this.TelephelyiAdatokÖsszesítéseToolStripMenuItem,
            this.FőmérnökségiAdatokToolStripMenuItem});
            this.KiadásiAdatokToolStripMenuItem.Name = "KiadásiAdatokToolStripMenuItem";
            this.KiadásiAdatokToolStripMenuItem.Size = new System.Drawing.Size(110, 20);
            this.KiadásiAdatokToolStripMenuItem.Text = "Kiadási Adatok";
            // 
            // ÁllományTáblaToolStripMenuItem
            // 
            this.ÁllományTáblaToolStripMenuItem.Image = global::Villamos.Properties.Resources.Aha_Soft_Standard_Transport_Tram;
            this.ÁllományTáblaToolStripMenuItem.Name = "ÁllományTáblaToolStripMenuItem";
            this.ÁllományTáblaToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.ÁllományTáblaToolStripMenuItem.Text = "Állomány tábla";
            this.ÁllományTáblaToolStripMenuItem.Click += new System.EventHandler(this.ÁllományTáblaToolStripMenuItem_Click);
            // 
            // ToolStripSeparator23
            // 
            this.ToolStripSeparator23.Name = "ToolStripSeparator23";
            this.ToolStripSeparator23.Size = new System.Drawing.Size(255, 6);
            // 
            // JárműLétrehozásMozgásToolStripMenuItem
            // 
            this.JárműLétrehozásMozgásToolStripMenuItem.Image = global::Villamos.Properties.Resources.Aha_Soft_Standard_Transport_Tram;
            this.JárműLétrehozásMozgásToolStripMenuItem.Name = "JárműLétrehozásMozgásToolStripMenuItem";
            this.JárműLétrehozásMozgásToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.JárműLétrehozásMozgásToolStripMenuItem.Text = "Jármű";
            this.JárműLétrehozásMozgásToolStripMenuItem.Click += new System.EventHandler(this.JárműLétrehozásMozgásToolStripMenuItem_Click);
            // 
            // ToolStripSeparator14
            // 
            this.ToolStripSeparator14.Name = "ToolStripSeparator14";
            this.ToolStripSeparator14.Size = new System.Drawing.Size(255, 6);
            // 
            // FőkönyvToolStripMenuItem
            // 
            this.FőkönyvToolStripMenuItem.Image = global::Villamos.Properties.Resources.book;
            this.FőkönyvToolStripMenuItem.Name = "FőkönyvToolStripMenuItem";
            this.FőkönyvToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.FőkönyvToolStripMenuItem.Text = "Főkönyv";
            this.FőkönyvToolStripMenuItem.Click += new System.EventHandler(this.FőkönyvToolStripMenuItem_Click);
            // 
            // NapiAdatokToolStripMenuItem
            // 
            this.NapiAdatokToolStripMenuItem.Image = global::Villamos.Properties.Resources.App_spreadsheet;
            this.NapiAdatokToolStripMenuItem.Name = "NapiAdatokToolStripMenuItem";
            this.NapiAdatokToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.NapiAdatokToolStripMenuItem.Text = "Kiadási és Javítási adatok";
            this.NapiAdatokToolStripMenuItem.Click += new System.EventHandler(this.NapiAdatokToolStripMenuItem_Click);
            // 
            // ToolStripSeparator25
            // 
            this.ToolStripSeparator25.Name = "ToolStripSeparator25";
            this.ToolStripSeparator25.Size = new System.Drawing.Size(255, 6);
            // 
            // KidobóKészítésToolStripMenuItem
            // 
            this.KidobóKészítésToolStripMenuItem.Image = global::Villamos.Properties.Resources.Filesystem_blockdevice_cubes;
            this.KidobóKészítésToolStripMenuItem.Name = "KidobóKészítésToolStripMenuItem";
            this.KidobóKészítésToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.KidobóKészítésToolStripMenuItem.Text = "Kidobó készítés";
            this.KidobóKészítésToolStripMenuItem.Click += new System.EventHandler(this.KidobóKészítésToolStripMenuItem_Click);
            // 
            // ToolStripSeparator12
            // 
            this.ToolStripSeparator12.Name = "ToolStripSeparator12";
            this.ToolStripSeparator12.Size = new System.Drawing.Size(255, 6);
            // 
            // MenetkimaradásMenü
            // 
            this.MenetkimaradásMenü.Image = global::Villamos.Properties.Resources.Aha_Soft_Standard_Transport_Tram;
            this.MenetkimaradásMenü.Name = "MenetkimaradásMenü";
            this.MenetkimaradásMenü.Size = new System.Drawing.Size(258, 22);
            this.MenetkimaradásMenü.Text = "Menetkimaradás kezelés";
            this.MenetkimaradásMenü.Click += new System.EventHandler(this.MenetkimaradásMenü_Click);
            // 
            // ToolStripSeparator33
            // 
            this.ToolStripSeparator33.Name = "ToolStripSeparator33";
            this.ToolStripSeparator33.Size = new System.Drawing.Size(255, 6);
            // 
            // DigitálisFőkönyvToolStripMenuItem
            // 
            this.DigitálisFőkönyvToolStripMenuItem.Image = global::Villamos.Properties.Resources.book;
            this.DigitálisFőkönyvToolStripMenuItem.Name = "DigitálisFőkönyvToolStripMenuItem";
            this.DigitálisFőkönyvToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.DigitálisFőkönyvToolStripMenuItem.Text = "Digitális Főkönyv";
            this.DigitálisFőkönyvToolStripMenuItem.Click += new System.EventHandler(this.DigitálisFőkönyvToolStripMenuItem_Click);
            // 
            // ToolStripSeparator8
            // 
            this.ToolStripSeparator8.Name = "ToolStripSeparator8";
            this.ToolStripSeparator8.Size = new System.Drawing.Size(255, 6);
            // 
            // SzerelvényToolStripMenuItem
            // 
            this.SzerelvényToolStripMenuItem.Image = global::Villamos.Properties.Resources.Aha_Soft_Standard_Transport_Tram;
            this.SzerelvényToolStripMenuItem.Name = "SzerelvényToolStripMenuItem";
            this.SzerelvényToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.SzerelvényToolStripMenuItem.Text = "Szerelvény";
            this.SzerelvényToolStripMenuItem.Click += new System.EventHandler(this.SzerelvényToolStripMenuItem_Click);
            // 
            // ToolStripSeparator17
            // 
            this.ToolStripSeparator17.Name = "ToolStripSeparator17";
            this.ToolStripSeparator17.Size = new System.Drawing.Size(255, 6);
            // 
            // KiadásiForteAdatokToolStripMenuItem
            // 
            this.KiadásiForteAdatokToolStripMenuItem.Image = global::Villamos.Properties.Resources.App_spreadsheet;
            this.KiadásiForteAdatokToolStripMenuItem.Name = "KiadásiForteAdatokToolStripMenuItem";
            this.KiadásiForteAdatokToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.KiadásiForteAdatokToolStripMenuItem.Text = "Kiadási Forte Adatok";
            this.KiadásiForteAdatokToolStripMenuItem.Click += new System.EventHandler(this.KiadásiForteAdatokToolStripMenuItem_Click);
            // 
            // TelephelyiAdatokÖsszesítéseToolStripMenuItem
            // 
            this.TelephelyiAdatokÖsszesítéseToolStripMenuItem.Image = global::Villamos.Properties.Resources.App_spreadsheet;
            this.TelephelyiAdatokÖsszesítéseToolStripMenuItem.Name = "TelephelyiAdatokÖsszesítéseToolStripMenuItem";
            this.TelephelyiAdatokÖsszesítéseToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.TelephelyiAdatokÖsszesítéseToolStripMenuItem.Text = "Telephelyi adatok összesítése";
            this.TelephelyiAdatokÖsszesítéseToolStripMenuItem.Click += new System.EventHandler(this.TelephelyiAdatokÖsszesítéseToolStripMenuItem_Click);
            // 
            // FőmérnökségiAdatokToolStripMenuItem
            // 
            this.FőmérnökségiAdatokToolStripMenuItem.Image = global::Villamos.Properties.Resources.App_spreadsheet;
            this.FőmérnökségiAdatokToolStripMenuItem.Name = "FőmérnökségiAdatokToolStripMenuItem";
            this.FőmérnökségiAdatokToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.FőmérnökségiAdatokToolStripMenuItem.Text = "Főmérnökségi adatok";
            this.FőmérnökségiAdatokToolStripMenuItem.Click += new System.EventHandler(this.FőmérnökségiAdatokToolStripMenuItem_Click);
            // 
            // GondnokságToolStripMenuItem
            // 
            this.GondnokságToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BehajtásiEngedélyToolStripMenuItem,
            this.KülsősDolgozókBelépésiÉsBehajtásaToolStripMenuItem,
            this.ToolStripSeparator19,
            this.ÉpületTakarításTörzsAdatokToolStripMenuItem,
            this.ÉpületTakarításToolStripMenuItem,
            this.ToolStripSeparator20,
            this.VédőeszközToolStripMenuItem,
            this.ToolStripSeparator21,
            this.eszközNyilvántartásToolStripMenuItem,
            this.épületTartozékNyilvántartásToolStripMenuItem,
            this.SzerszámNyilvántartásToolStripMenuItem,
            this.ToolStripSeparator24,
            this.RezsiRaktárToolStripMenuItem});
            this.GondnokságToolStripMenuItem.Name = "GondnokságToolStripMenuItem";
            this.GondnokságToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
            this.GondnokságToolStripMenuItem.Text = "Gondnokság";
            // 
            // BehajtásiEngedélyToolStripMenuItem
            // 
            this.BehajtásiEngedélyToolStripMenuItem.Image = global::Villamos.Properties.Resources.CAR52;
            this.BehajtásiEngedélyToolStripMenuItem.Name = "BehajtásiEngedélyToolStripMenuItem";
            this.BehajtásiEngedélyToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.BehajtásiEngedélyToolStripMenuItem.Text = "Behajtási Engedély";
            this.BehajtásiEngedélyToolStripMenuItem.Click += new System.EventHandler(this.BehajtásiEngedélyToolStripMenuItem_Click);
            // 
            // KülsősDolgozókBelépésiÉsBehajtásaToolStripMenuItem
            // 
            this.KülsősDolgozókBelépésiÉsBehajtásaToolStripMenuItem.Image = global::Villamos.Properties.Resources.CAR51;
            this.KülsősDolgozókBelépésiÉsBehajtásaToolStripMenuItem.Name = "KülsősDolgozókBelépésiÉsBehajtásaToolStripMenuItem";
            this.KülsősDolgozókBelépésiÉsBehajtásaToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.KülsősDolgozókBelépésiÉsBehajtásaToolStripMenuItem.Text = "Külsős dolgozók belépési és behajtása";
            this.KülsősDolgozókBelépésiÉsBehajtásaToolStripMenuItem.Click += new System.EventHandler(this.KülsősDolgozókBelépésiÉsBehajtásaToolStripMenuItem_Click);
            // 
            // ToolStripSeparator19
            // 
            this.ToolStripSeparator19.Name = "ToolStripSeparator19";
            this.ToolStripSeparator19.Size = new System.Drawing.Size(307, 6);
            // 
            // ÉpületTakarításTörzsAdatokToolStripMenuItem
            // 
            this.ÉpületTakarításTörzsAdatokToolStripMenuItem.Image = global::Villamos.Properties.Resources.home;
            this.ÉpületTakarításTörzsAdatokToolStripMenuItem.Name = "ÉpületTakarításTörzsAdatokToolStripMenuItem";
            this.ÉpületTakarításTörzsAdatokToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.ÉpületTakarításTörzsAdatokToolStripMenuItem.Text = "Épület takarítás törzs adatok";
            this.ÉpületTakarításTörzsAdatokToolStripMenuItem.Click += new System.EventHandler(this.ÉpületTakarításTörzsAdatokToolStripMenuItem_Click);
            // 
            // ÉpületTakarításToolStripMenuItem
            // 
            this.ÉpületTakarításToolStripMenuItem.Image = global::Villamos.Properties.Resources.home;
            this.ÉpületTakarításToolStripMenuItem.Name = "ÉpületTakarításToolStripMenuItem";
            this.ÉpületTakarításToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.ÉpületTakarításToolStripMenuItem.Text = "Épület takarítás ";
            this.ÉpületTakarításToolStripMenuItem.Click += new System.EventHandler(this.ÉpületTakarításToolStripMenuItem_Click);
            // 
            // ToolStripSeparator20
            // 
            this.ToolStripSeparator20.Name = "ToolStripSeparator20";
            this.ToolStripSeparator20.Size = new System.Drawing.Size(307, 6);
            // 
            // VédőeszközToolStripMenuItem
            // 
            this.VédőeszközToolStripMenuItem.Image = global::Villamos.Properties.Resources.Védő;
            this.VédőeszközToolStripMenuItem.Name = "VédőeszközToolStripMenuItem";
            this.VédőeszközToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.VédőeszközToolStripMenuItem.Text = "Védőeszköz nyilvántartás";
            this.VédőeszközToolStripMenuItem.Click += new System.EventHandler(this.VédőeszközToolStripMenuItem_Click);
            // 
            // ToolStripSeparator21
            // 
            this.ToolStripSeparator21.Name = "ToolStripSeparator21";
            this.ToolStripSeparator21.Size = new System.Drawing.Size(307, 6);
            // 
            // eszközNyilvántartásToolStripMenuItem
            // 
            this.eszközNyilvántartásToolStripMenuItem.Image = global::Villamos.Properties.Resources.Iconarchive_Red_Orb_Alphabet_Exclamation_mark;
            this.eszközNyilvántartásToolStripMenuItem.Name = "eszközNyilvántartásToolStripMenuItem";
            this.eszközNyilvántartásToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.eszközNyilvántartásToolStripMenuItem.Text = "Eszköz nyilvántartás";
            this.eszközNyilvántartásToolStripMenuItem.Click += new System.EventHandler(this.EszközNyilvántartásToolStripMenuItem_Click);
            // 
            // épületTartozékNyilvántartásToolStripMenuItem
            // 
            this.épületTartozékNyilvántartásToolStripMenuItem.Image = global::Villamos.Properties.Resources.home_next;
            this.épületTartozékNyilvántartásToolStripMenuItem.Name = "épületTartozékNyilvántartásToolStripMenuItem";
            this.épületTartozékNyilvántartásToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.épületTartozékNyilvántartásToolStripMenuItem.Text = "Helység tartozék nyilvántartás";
            this.épületTartozékNyilvántartásToolStripMenuItem.Click += new System.EventHandler(this.ÉpületTartozékNyilvántartásToolStripMenuItem_Click);
            // 
            // SzerszámNyilvántartásToolStripMenuItem
            // 
            this.SzerszámNyilvántartásToolStripMenuItem.Image = global::Villamos.Properties.Resources.Action_configure;
            this.SzerszámNyilvántartásToolStripMenuItem.Name = "SzerszámNyilvántartásToolStripMenuItem";
            this.SzerszámNyilvántartásToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.SzerszámNyilvántartásToolStripMenuItem.Text = "Szerszám nyilvántartás";
            this.SzerszámNyilvántartásToolStripMenuItem.Click += new System.EventHandler(this.SzerszámNyilvántartásToolStripMenuItem_Click);
            // 
            // ToolStripSeparator24
            // 
            this.ToolStripSeparator24.Name = "ToolStripSeparator24";
            this.ToolStripSeparator24.Size = new System.Drawing.Size(307, 6);
            // 
            // RezsiRaktárToolStripMenuItem
            // 
            this.RezsiRaktárToolStripMenuItem.Image = global::Villamos.Properties.Resources.App_ark;
            this.RezsiRaktárToolStripMenuItem.Name = "RezsiRaktárToolStripMenuItem";
            this.RezsiRaktárToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.RezsiRaktárToolStripMenuItem.Text = "Rezsi Raktár";
            this.RezsiRaktárToolStripMenuItem.Click += new System.EventHandler(this.RezsiRaktárToolStripMenuItem_Click);
            // 
            // Súgómenü
            // 
            this.Súgómenü.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Súgómenü.Image = global::Villamos.Properties.Resources.Help_Support;
            this.Súgómenü.Name = "Súgómenü";
            this.Súgómenü.Size = new System.Drawing.Size(67, 20);
            this.Súgómenü.Text = "Súgó";
            this.Súgómenü.Click += new System.EventHandler(this.Súgómenü_Click);
            // 
            // KilépésToolStripMenuItem
            // 
            this.KilépésToolStripMenuItem.Name = "KilépésToolStripMenuItem";
            this.KilépésToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.KilépésToolStripMenuItem.Text = "Kilépés";
            this.KilépésToolStripMenuItem.Click += new System.EventHandler(this.KilépésToolStripMenuItem_Click);
            // 
            // LblÓra
            // 
            this.LblÓra.BackColor = System.Drawing.Color.Bisque;
            this.LblÓra.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblÓra.Font = new System.Drawing.Font("Monotype Corsiva", 26.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LblÓra.Location = new System.Drawing.Point(2, 26);
            this.LblÓra.Name = "LblÓra";
            this.LblÓra.Size = new System.Drawing.Size(200, 50);
            this.LblÓra.TabIndex = 8;
            this.LblÓra.Text = "Óra";
            this.LblÓra.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblÓra.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LblÓra_MouseClick);
            // 
            // lblVerzió
            // 
            this.lblVerzió.BackColor = System.Drawing.Color.Bisque;
            this.lblVerzió.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblVerzió.Font = new System.Drawing.Font("Monotype Corsiva", 26.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblVerzió.Location = new System.Drawing.Point(707, 26);
            this.lblVerzió.Name = "lblVerzió";
            this.lblVerzió.Size = new System.Drawing.Size(177, 50);
            this.lblVerzió.TabIndex = 10;
            this.lblVerzió.Text = "Verzió";
            this.lblVerzió.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblVerzió.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LblVerzió_MouseDoubleClick);
            // 
            // lbltelephely
            // 
            this.lbltelephely.BackColor = System.Drawing.Color.Bisque;
            this.lbltelephely.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbltelephely.Font = new System.Drawing.Font("Monotype Corsiva", 26.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lbltelephely.Location = new System.Drawing.Point(410, 26);
            this.lbltelephely.Name = "lbltelephely";
            this.lbltelephely.Size = new System.Drawing.Size(291, 50);
            this.lbltelephely.TabIndex = 11;
            this.lbltelephely.Text = "Telephely";
            this.lbltelephely.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Üzenetektext
            // 
            this.Üzenetektext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Üzenetektext.Location = new System.Drawing.Point(12, 143);
            this.Üzenetektext.Name = "Üzenetektext";
            this.Üzenetektext.Size = new System.Drawing.Size(90, 63);
            this.Üzenetektext.TabIndex = 14;
            this.Üzenetektext.Text = "";
            this.Üzenetektext.DoubleClick += new System.EventHandler(this.Üzenetektext_DoubleClick);
            // 
            // Utasításoktext
            // 
            this.Utasításoktext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Utasításoktext.Location = new System.Drawing.Point(110, 144);
            this.Utasításoktext.Name = "Utasításoktext";
            this.Utasításoktext.Size = new System.Drawing.Size(84, 62);
            this.Utasításoktext.TabIndex = 15;
            this.Utasításoktext.Text = "";
            this.Utasításoktext.DoubleClick += new System.EventHandler(this.Utasításoktext_DoubleClick);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Label2.Location = new System.Drawing.Point(11, 35);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(40, 20);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "Név:";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Label4.Location = new System.Drawing.Point(5, 20);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(76, 20);
            this.Label4.TabIndex = 3;
            this.Label4.Text = "Jogkör Új";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Label8.Location = new System.Drawing.Point(5, 139);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(80, 20);
            this.Label8.TabIndex = 7;
            this.Label8.Text = "Telephely:";
            this.Label8.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Label8_MouseDoubleClick);
            // 
            // panels4
            // 
            this.panels4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panels4.Location = new System.Drawing.Point(167, 144);
            this.panels4.Name = "panels4";
            this.panels4.Size = new System.Drawing.Size(186, 20);
            this.panels4.TabIndex = 8;
            this.panels4.Text = "telephely";
            // 
            // panels2
            // 
            this.panels2.Location = new System.Drawing.Point(118, 17);
            this.panels2.Multiline = true;
            this.panels2.Name = "panels2";
            this.panels2.Size = new System.Drawing.Size(490, 113);
            this.panels2.TabIndex = 12;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Label1.Location = new System.Drawing.Point(5, 218);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(98, 20);
            this.Label1.TabIndex = 14;
            this.Label1.Text = "Üzenet srsz:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Label3.Location = new System.Drawing.Point(5, 176);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(105, 20);
            this.Label3.TabIndex = 15;
            this.Label3.Text = "Utasítás srsz:";
            // 
            // txtüzsorszám
            // 
            this.txtüzsorszám.Location = new System.Drawing.Point(165, 212);
            this.txtüzsorszám.Name = "txtüzsorszám";
            this.txtüzsorszám.Size = new System.Drawing.Size(100, 26);
            this.txtüzsorszám.TabIndex = 16;
            // 
            // txtutsorszám
            // 
            this.txtutsorszám.Location = new System.Drawing.Point(165, 170);
            this.txtutsorszám.Name = "txtutsorszám";
            this.txtutsorszám.Size = new System.Drawing.Size(100, 26);
            this.txtutsorszám.TabIndex = 17;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(359, 136);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(249, 28);
            this.Cmbtelephely.TabIndex = 22;
            this.Cmbtelephely.Visible = false;
            this.Cmbtelephely.SelectedIndexChanged += new System.EventHandler(this.Cmbtelephely_SelectedIndexChanged);
            // 
            // Alsó
            // 
            this.Alsó.BackColor = System.Drawing.Color.LightCoral;
            this.Alsó.Controls.Add(this.Command9);
            this.Alsó.Controls.Add(this.Label6);
            this.Alsó.Controls.Add(this.Rejtett);
            this.Alsó.Controls.Add(this.BtnHardverkulcs);
            this.Alsó.Controls.Add(this.Rejtett_Frissít);
            this.Alsó.Controls.Add(this.Label2);
            this.Alsó.Location = new System.Drawing.Point(200, 100);
            this.Alsó.Name = "Alsó";
            this.Alsó.Size = new System.Drawing.Size(635, 395);
            this.Alsó.TabIndex = 2;
            this.Alsó.TabStop = false;
            this.Alsó.Visible = false;
            // 
            // Command9
            // 
            this.Command9.BackgroundImage = global::Villamos.Properties.Resources.bezár;
            this.Command9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command9.Location = new System.Drawing.Point(598, 0);
            this.Command9.Name = "Command9";
            this.Command9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Command9.Size = new System.Drawing.Size(35, 35);
            this.Command9.TabIndex = 31;
            this.ToolTip1.SetToolTip(this.Command9, "Bezárja az ablakot");
            this.Command9.UseVisualStyleBackColor = true;
            this.Command9.Click += new System.EventHandler(this.Command9_Click);
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Label6.Location = new System.Drawing.Point(0, 0);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(153, 20);
            this.Label6.TabIndex = 30;
            this.Label6.Text = "Jogosultság frissítés";
            this.Label6.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label6_MouseMove);
            // 
            // Rejtett
            // 
            this.Rejtett.BackColor = System.Drawing.Color.Peru;
            this.Rejtett.Controls.Add(this.Cmbtelephely);
            this.Rejtett.Controls.Add(this.TároltVerzió);
            this.Rejtett.Controls.Add(this.txtutsorszám);
            this.Rejtett.Controls.Add(this.Label5);
            this.Rejtett.Controls.Add(this.Label3);
            this.Rejtett.Controls.Add(this.Verzió_Váltás);
            this.Rejtett.Controls.Add(this.panels4);
            this.Rejtett.Controls.Add(this.Label8);
            this.Rejtett.Controls.Add(this.Label4);
            this.Rejtett.Controls.Add(this.panels2);
            this.Rejtett.Controls.Add(this.Label1);
            this.Rejtett.Controls.Add(this.txtüzsorszám);
            this.Rejtett.Location = new System.Drawing.Point(5, 100);
            this.Rejtett.Name = "Rejtett";
            this.Rejtett.Size = new System.Drawing.Size(624, 289);
            this.Rejtett.TabIndex = 29;
            this.Rejtett.TabStop = false;
            this.Rejtett.Visible = false;
            // 
            // TároltVerzió
            // 
            this.TároltVerzió.Location = new System.Drawing.Point(165, 253);
            this.TároltVerzió.Name = "TároltVerzió";
            this.TároltVerzió.Size = new System.Drawing.Size(100, 26);
            this.TároltVerzió.TabIndex = 28;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Label5.Location = new System.Drawing.Point(5, 258);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(144, 20);
            this.Label5.TabIndex = 27;
            this.Label5.Text = "Tárolt Verzió szám:";
            this.Label5.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Label5_MouseDoubleClick);
            // 
            // Verzió_Váltás
            // 
            this.Verzió_Váltás.BackgroundImage = global::Villamos.Properties.Resources.Mimetype_recycled;
            this.Verzió_Váltás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Verzió_Váltás.Location = new System.Drawing.Point(337, 239);
            this.Verzió_Váltás.Name = "Verzió_Váltás";
            this.Verzió_Váltás.Size = new System.Drawing.Size(40, 40);
            this.Verzió_Váltás.TabIndex = 26;
            this.ToolTip1.SetToolTip(this.Verzió_Váltás, "Aktuális verziót állítja be a verzió számnak");
            this.Verzió_Váltás.UseVisualStyleBackColor = true;
            this.Verzió_Váltás.Visible = false;
            this.Verzió_Váltás.Click += new System.EventHandler(this.Verzió_Váltás_Click);
            // 
            // BtnHardverkulcs
            // 
            this.BtnHardverkulcs.BackgroundImage = global::Villamos.Properties.Resources.Dolgozó;
            this.BtnHardverkulcs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnHardverkulcs.Location = new System.Drawing.Point(486, 15);
            this.BtnHardverkulcs.Name = "BtnHardverkulcs";
            this.BtnHardverkulcs.Size = new System.Drawing.Size(75, 76);
            this.BtnHardverkulcs.TabIndex = 21;
            this.BtnHardverkulcs.UseVisualStyleBackColor = true;
            this.BtnHardverkulcs.Click += new System.EventHandler(this.BtnHardverkulcs_Click);
            // 
            // Rejtett_Frissít
            // 
            this.Rejtett_Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Rejtett_Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Rejtett_Frissít.Location = new System.Drawing.Point(408, 15);
            this.Rejtett_Frissít.Name = "Rejtett_Frissít";
            this.Rejtett_Frissít.Size = new System.Drawing.Size(40, 40);
            this.Rejtett_Frissít.TabIndex = 19;
            this.Rejtett_Frissít.UseVisualStyleBackColor = true;
            this.Rejtett_Frissít.Click += new System.EventHandler(this.Rejtett_Frissít_Click);
            // 
            // Figyelmeztetés
            // 
            this.Figyelmeztetés.BackColor = System.Drawing.Color.LightSalmon;
            this.Figyelmeztetés.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Figyelmeztetés.Location = new System.Drawing.Point(58, 212);
            this.Figyelmeztetés.Name = "Figyelmeztetés";
            this.Figyelmeztetés.Size = new System.Drawing.Size(136, 86);
            this.Figyelmeztetés.TabIndex = 25;
            this.Figyelmeztetés.Text = "A program karbantartás miatt ki fogja léptetni!";
            this.Figyelmeztetés.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Figyelmeztetés.Visible = false;
            // 
            // Timer1
            // 
            this.Timer1.Interval = 1000;
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Timer2
            // 
            this.Timer2.Interval = 300000;
            this.Timer2.Tick += new System.EventHandler(this.Timer2_Tick);
            // 
            // Btnutasításfrissítés
            // 
            this.Btnutasításfrissítés.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Btnutasításfrissítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btnutasításfrissítés.Location = new System.Drawing.Point(12, 258);
            this.Btnutasításfrissítés.Name = "Btnutasításfrissítés";
            this.Btnutasításfrissítés.Size = new System.Drawing.Size(40, 40);
            this.Btnutasításfrissítés.TabIndex = 13;
            this.Btnutasításfrissítés.UseVisualStyleBackColor = true;
            this.Btnutasításfrissítés.Click += new System.EventHandler(this.Btnutasításfrissítés_Click);
            // 
            // Btnüzenetfrissítés
            // 
            this.Btnüzenetfrissítés.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Btnüzenetfrissítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btnüzenetfrissítés.Location = new System.Drawing.Point(12, 212);
            this.Btnüzenetfrissítés.Name = "Btnüzenetfrissítés";
            this.Btnüzenetfrissítés.Size = new System.Drawing.Size(40, 40);
            this.Btnüzenetfrissítés.TabIndex = 12;
            this.Btnüzenetfrissítés.UseVisualStyleBackColor = true;
            this.Btnüzenetfrissítés.Click += new System.EventHandler(this.Btnüzenetfrissítés_Click);
            // 
            // Képkeret
            // 
            this.Képkeret.Location = new System.Drawing.Point(12, 85);
            this.Képkeret.Name = "Képkeret";
            this.Képkeret.Size = new System.Drawing.Size(90, 53);
            this.Képkeret.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Képkeret.TabIndex = 26;
            this.Képkeret.TabStop = false;
            // 
            // Képkeret1
            // 
            this.Képkeret1.Location = new System.Drawing.Point(108, 85);
            this.Képkeret1.Name = "Képkeret1";
            this.Képkeret1.Size = new System.Drawing.Size(90, 53);
            this.Képkeret1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Képkeret1.TabIndex = 27;
            this.Képkeret1.TabStop = false;
            // 
            // Panels1
            // 
            this.Panels1.BackColor = System.Drawing.Color.Bisque;
            this.Panels1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Panels1.Font = new System.Drawing.Font("Monotype Corsiva", 26.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Panels1.Location = new System.Drawing.Point(206, 26);
            this.Panels1.Name = "Panels1";
            this.Panels1.Size = new System.Drawing.Size(200, 50);
            this.Panels1.TabIndex = 28;
            this.Panels1.Text = "Név";
            this.Panels1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Panels1.DoubleClick += new System.EventHandler(this.Panels1_DoubleClick);
            // 
            // toolStripSeparator40
            // 
            this.toolStripSeparator40.Name = "toolStripSeparator40";
            this.toolStripSeparator40.Size = new System.Drawing.Size(350, 6);
            // 
            // VételezésMenü
            // 
            this.VételezésMenü.Name = "VételezésMenü";
            this.VételezésMenü.Size = new System.Drawing.Size(353, 22);
            this.VételezésMenü.Text = "Anyag Vételezési segéd";
            this.VételezésMenü.Click += new System.EventHandler(this.VételezésMenü_Click);
            // 
            // A_Főoldal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Tan;
            this.ClientSize = new System.Drawing.Size(890, 515);
            this.Controls.Add(this.Panels1);
            this.Controls.Add(this.Btnüzenetfrissítés);
            this.Controls.Add(this.Btnutasításfrissítés);
            this.Controls.Add(this.Figyelmeztetés);
            this.Controls.Add(this.Alsó);
            this.Controls.Add(this.Utasításoktext);
            this.Controls.Add(this.Üzenetektext);
            this.Controls.Add(this.lbltelephely);
            this.Controls.Add(this.lblVerzió);
            this.Controls.Add(this.LblÓra);
            this.Controls.Add(this.Menü);
            this.Controls.Add(this.Képkeret);
            this.Controls.Add(this.Képkeret1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.Menü;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "A_Főoldal";
            this.Text = "Villamos Nyilvántartások";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.A_Főoldal_FormClosed);
            this.Load += new System.EventHandler(this.AblakFőoldal_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AblakFőoldal_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.AblakFőoldal_KeyUp);
            this.Resize += new System.EventHandler(this.AblakFőoldal_Resize);
            this.Menü.ResumeLayout(false);
            this.Menü.PerformLayout();
            this.Alsó.ResumeLayout(false);
            this.Alsó.PerformLayout();
            this.Rejtett.ResumeLayout(false);
            this.Rejtett.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Képkeret)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Képkeret1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal MenuStrip Menü;
        internal ToolStripMenuItem ProgramAdatokMenü;
        internal ToolStripMenuItem FelhasználókBeállításaMenü;
        internal ToolStripMenuItem ProgramAdatokSzemélyMenü;
        internal ToolStripSeparator ToolStripSeparator1;
        internal ToolStripSeparator ToolStripSeparator2;
        internal ToolStripMenuItem Súgómenü;
        internal Label LblÓra;
        internal Label lblVerzió;
        internal Label lbltelephely;
        internal Button Btnüzenetfrissítés;
        internal Button Btnutasításfrissítés;
        internal ToolStripMenuItem KilépésToolStripMenuItem;
        internal ToolStripMenuItem KiadásiAdatokToolStripMenuItem;
        internal ToolStripMenuItem MenetkimaradásMenü;
        internal ToolStripSeparator ToolStripSeparator33;
        internal ToolStripMenuItem ProgramAdatokKiadásiAdatokToolStripMenuItem;
        internal ToolStripMenuItem InformációkMenü;
        internal ToolStripMenuItem ÜzenetekToolStripMenuItem;
        internal ToolStripMenuItem UtasításokToolStripMenuItem;
        internal RichTextBox Üzenetektext;
        internal RichTextBox Utasításoktext;
        internal ToolStripMenuItem JárműAdatokToolStripMenuItem;
        internal ToolStripMenuItem AkkumulátorNyilvántartásToolStripMenuItem1;
        internal ToolStripMenuItem DolgozóiAdatokToolStripMenuItem1;
        internal ToolStripMenuItem ListákJelenlétiÍvekToolStripMenuItem;
        internal ToolStripMenuItem SzabadságTúlóraBetegállományToolStripMenuItem;
        internal ToolStripMenuItem GondnokságToolStripMenuItem;
        internal ToolStripMenuItem BehajtásiEngedélyToolStripMenuItem;
        internal ToolStripMenuItem OktatásokToolStripMenuItem;
        internal ToolStripMenuItem DolgozóiAlapadatokToolStripMenuItem;
        internal ToolStripMenuItem DolgozóFelvételátvételvezénylésToolStripMenuItem;
        internal ToolStripMenuItem ProgramAdatokEgyébToolStripMenuItem;
        internal ToolStripSeparator ToolStripSeparator3;
        internal ToolStripSeparator ToolStripSeparator4;
        internal ToolStripSeparator ToolStripSeparator5;
        internal ToolStripMenuItem LekérdezésekToolStripMenuItem;
        internal ToolStripMenuItem LétszámGazdálkodásToolStripMenuItem;
        internal ToolStripMenuItem TúlóraEllenőrzésToolStripMenuItem;
        internal ToolStripSeparator ToolStripSeparator6;
        internal ToolStripMenuItem MunkalapAdatokkarbantartásaToolStripMenuItem;
        internal ToolStripMenuItem MunkalapKészítésToolStripMenuItem;
        internal ToolStripMenuItem MunkalapDekádolóToolStripMenuItem;
        internal Label Label2;
        internal Label Label4;
        internal Label Label8;
        internal Label panels4;
        internal TextBox panels2;
        internal Label Label1;
        internal Label Label3;
        internal TextBox txtüzsorszám;
        internal TextBox txtutsorszám;
        internal Button Rejtett_Frissít;
        internal Button BtnHardverkulcs;
        internal ComboBox Cmbtelephely;
        internal GroupBox Alsó;
        internal ToolStripMenuItem BeosztásToolStripMenuItem;
        internal ToolStripMenuItem BeosztásNaplóToolStripMenuItem;
        internal ToolStripSeparator ToolStripSeparator7;
        internal ToolStripMenuItem SAPOsztályToolStripMenuItem;
        internal ToolStripMenuItem VáltósMunkarendÉsTúlóraToolStripMenuItem;
        internal ToolStripMenuItem KülsősDolgozókBelépésiÉsBehajtásaToolStripMenuItem;
        internal ToolStripMenuItem DigitálisFőkönyvToolStripMenuItem;
        internal ToolStripMenuItem SzerelvényToolStripMenuItem;
        internal ToolStripSeparator ToolStripSeparator8;
        internal ToolStripMenuItem KarbantartásToolStripMenuItem;
        internal ToolStripMenuItem T5C5FutásnapRögzítésToolStripMenuItem;
        internal ToolStripMenuItem T5C5FutásnapÜtemezésToolStripMenuItem;
        internal ToolStripMenuItem T5C5VJavításÜtemezésToolStripMenuItem;
        internal ToolStripSeparator ToolStripSeparator9;
        internal ToolStripMenuItem T5C5AdatokMódosításaToolStripMenuItem;
        internal ToolStripMenuItem JárműKarbantartásiAdatokToolStripMenuItem;
        internal ToolStripMenuItem CiklusrendToolStripMenuItem;
        internal ToolStripSeparator ToolStripSeparator10;
        internal ToolStripSeparator ToolStripSeparator11;
        internal ToolStripMenuItem FőkönyvToolStripMenuItem;
        internal ToolStripSeparator ToolStripSeparator12;
        internal ToolStripMenuItem NapiAdatokToolStripMenuItem;
        internal ToolStripSeparator ToolStripSeparator13;
        internal ToolStripMenuItem TW6000AdatokToolStripMenuItem;
        internal ToolStripMenuItem KerékátmérőNyilvántartásSAPBerendezésekToolStripMenuItem;
        internal ToolStripSeparator ToolStripSeparator14;
        internal ToolStripMenuItem JárműLétrehozásMozgásToolStripMenuItem;
        internal ToolStripMenuItem SérülésNyilvántartásToolStripMenuItem;
        internal ToolStripMenuItem ReklámNyilvántartásToolStripMenuItem;
        internal ToolStripSeparator ToolStripSeparator15;
        internal ToolStripMenuItem ICSKCSVToolStripMenuItem;
        internal ToolStripSeparator ToolStripSeparator16;
        internal ToolStripMenuItem FogaskerekűToolStripMenuItem;
        internal ToolStripSeparator ToolStripSeparator17;
        internal ToolStripMenuItem KiadásiForteAdatokToolStripMenuItem;
        internal ToolStripMenuItem TelephelyiAdatokÖsszesítéseToolStripMenuItem;
        internal ToolStripMenuItem FőmérnökségiAdatokToolStripMenuItem;
        internal ToolStripSeparator ToolStripSeparator18;
        internal ToolStripSeparator ToolStripSeparator19;
        internal ToolStripMenuItem ÉpületTakarításTörzsAdatokToolStripMenuItem;
        internal ToolStripMenuItem ÉpületTakarításToolStripMenuItem;
        internal ToolStripSeparator ToolStripSeparator20;
        internal ToolStripMenuItem VédőeszközToolStripMenuItem;
        internal ToolStripSeparator ToolStripSeparator21;
        internal ToolStripMenuItem SzerszámNyilvántartásToolStripMenuItem;
        internal ToolStripSeparator ToolStripSeparator22;
        internal ToolStripMenuItem ToolStripMenuItem1;
        internal ToolStripMenuItem ÁllományTáblaToolStripMenuItem;
        internal ToolStripSeparator ToolStripSeparator23;
        internal ToolStripSeparator ToolStripSeparator24;
        internal ToolStripMenuItem RezsiRaktárToolStripMenuItem;
        internal ToolStripSeparator ToolStripSeparator25;
        internal ToolStripMenuItem KidobóKészítésToolStripMenuItem;
        internal ToolStripSeparator ToolStripSeparator26;
        internal ToolStripMenuItem CAF5CAF9AdatokÉsÜtemezésToolStripMenuItem;
        internal Label  Figyelmeztetés;
        internal ToolStripMenuItem ToolStripMenuItem2;
        internal ToolTip ToolTip1;
        internal Button Verzió_Váltás;
        internal TextBox TároltVerzió;
        internal Label Label5;
        internal GroupBox Rejtett;
        internal Label Label6;
        internal Button Command9;
        internal ToolStripSeparator ToolStripSeparator27;
        internal Timer Timer1;
        internal Timer Timer2;
        internal ToolStripMenuItem járműTechnológiákToolStripMenuItem;
        internal ToolStripSeparator toolStripSeparator28;
        internal ToolStripMenuItem karbantartásiMunkalapokToolStripMenuItem;
        internal ToolStripMenuItem épületTartozékNyilvántartásToolStripMenuItem;
        internal ToolStripMenuItem eszközNyilvántartásToolStripMenuItem;
        internal ToolStripSeparator toolStripSeparator29;
        internal ToolStripMenuItem nosztalgiaToolStripMenuItem;
        internal ToolStripSeparator toolStripSeparator30;
        internal ToolStripMenuItem kerékesztergálásSzervezésToolStripMenuItem;
        internal ToolStripSeparator toolStripSeparator31;
        internal ToolStripSeparator toolStripSeparator32;
        internal ToolStripMenuItem kerékesztergálásiAdatokBarossToolStripMenuItem;
        internal PictureBox Képkeret;
        internal PictureBox Képkeret1;
        internal ToolStripMenuItem T5C5UtastérFűtésToolStripMenuItem;
        internal ToolStripMenuItem TTTPToolStripMenuItem;
        internal ToolStripSeparator toolStripSeparator34;
        internal ToolStripMenuItem EsztergaKarbantartásToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator35;
        private ToolStripMenuItem fődarabNótaToolStripMenuItem;
        private ToolStripMenuItem ablakokBeállításaToolStripMenuItem;
        private ToolStripMenuItem felhasználókLétrehozásaTörléseToolStripMenuItem;
        private ToolStripMenuItem gombokBeállításaToolStripMenuItem;
        private ToolStripMenuItem jogosultságKiosztásToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator36;
        internal Label Panels1;
        private ToolStripSeparator toolStripSeparator37;
        private ToolStripSeparator toolStripSeparator38;
        private ToolStripSeparator toolStripSeparator39;
        private ToolStripSeparator toolStripSeparator40;
        private ToolStripMenuItem VételezésMenü;
    }
}