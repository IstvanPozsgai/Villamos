using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Villamos.Ablakok;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos.Kezelők;
using Villamos.Villamos_Ablakok;
using Villamos.Villamos_Ablakok._4_Nyilvántartások.TTP;
using Villamos.Villamos_Ablakok._5_Karbantartás.Eszterga_Karbantartás;
using Villamos.Villamos_Ablakok.Kerékeszterga;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using Villamos.Villamos_Kezelők;
using static System.IO.File;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    public partial class A_Főoldal
    {
        private static PerformanceCounter myCounter;
        public delegate void VoidDelegate(string data);
        public event VoidDelegate MyEvent;

        bool CTRL_le = false;
        bool Shift_le = false;
        bool Alt_le = false;

        readonly Kezelő_Üzenet Kéz_Üzenet = new Kezelő_Üzenet();
        readonly Kezelő_Belépés_Verzió Kéz_Belépés_Verzió = new Kezelő_Belépés_Verzió();
        readonly Kezelő_Kiegészítő_Könyvtár KézKönyvtár = new Kezelő_Kiegészítő_Könyvtár();
        readonly Kezelő_Jármű_Napló KézNapló = new Kezelő_Jármű_Napló();
        readonly Kezelő_Utasítás KézUtasítás = new Kezelő_Utasítás();
        readonly Kezelő_Belépés_Jogosultságtábla Kéz_Jogosultság = new Kezelő_Belépés_Jogosultságtábla();

        List<Adat_Belépés_Verzió> AdatokVerzó = new List<Adat_Belépés_Verzió>();
        List<Adat_Kiegészítő_Könyvtár> AdatokKönyvtár = new List<Adat_Kiegészítő_Könyvtár>();
        List<Adat_Jármű_Napló> AdatokNapló = new List<Adat_Jármű_Napló>();

        public A_Főoldal()
        {
            InitializeComponent();
        }

        [DllImport("user32")]
        private static extern int SetCursorPos(int x, int y);

        /// <summary>
        /// Menü sorszámából el kell venni egyet, hogy jó helyre mutasson
        /// </summary>
        private void Menükbeállítása()
        {
            if (panels2.Text.Substring(0, 1) == "0")
                FelhasználókBeállításaMenü.Enabled = false;
            else
                FelhasználókBeállításaMenü.Enabled = true;
            if (panels2.Text.Substring(1, 1) == "0")
                ProgramAdatokKiadásiAdatokToolStripMenuItem.Enabled = false;
            else
                ProgramAdatokKiadásiAdatokToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(4, 1) == "0")
                ProgramAdatokSzemélyMenü.Enabled = false;
            else
                ProgramAdatokSzemélyMenü.Enabled = true;
            if (panels2.Text.Substring(6, 1) == "0")
                CiklusrendToolStripMenuItem.Enabled = false;
            else
                CiklusrendToolStripMenuItem.Enabled = true;

            if (panels2.Text.Substring(10, 1) == "0")
                VáltósMunkarendÉsTúlóraToolStripMenuItem.Enabled = false;
            else
                VáltósMunkarendÉsTúlóraToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(14, 1) == "0")
                ProgramAdatokEgyébToolStripMenuItem.Enabled = false;
            else
                ProgramAdatokEgyébToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(15, 1) == "0")
                járműTechnológiákToolStripMenuItem.Enabled = false;
            else
                járműTechnológiákToolStripMenuItem.Enabled = true;


            if (panels2.Text.Substring(19, 1) == "0")
                MenetkimaradásMenü.Enabled = false;
            else
                MenetkimaradásMenü.Enabled = true;

            if (panels2.Text.Substring(21, 1) == "0")
                BeosztásToolStripMenuItem.Enabled = false;
            else
                BeosztásToolStripMenuItem.Enabled = true;


            if (panels2.Text.Substring(59, 1) == "0")
                ListákJelenlétiÍvekToolStripMenuItem.Enabled = false;
            else
                ListákJelenlétiÍvekToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(60, 1) == "0")
                SzabadságTúlóraBetegállományToolStripMenuItem.Enabled = false;
            else
                SzabadságTúlóraBetegállományToolStripMenuItem.Enabled = true;

            if (panels2.Text.Substring(63, 1) == "0")
                OktatásokToolStripMenuItem.Enabled = false;
            else
                OktatásokToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(65, 1) == "0")
                DolgozóiAlapadatokToolStripMenuItem.Enabled = false;
            else
                DolgozóiAlapadatokToolStripMenuItem.Enabled = true;

            if (panels2.Text.Substring(68, 1) == "0")
                DolgozóFelvételátvételvezénylésToolStripMenuItem.Enabled = false;
            else
                DolgozóFelvételátvételvezénylésToolStripMenuItem.Enabled = true;

            if (panels2.Text.Substring(74, 1) == "0")
                LétszámGazdálkodásToolStripMenuItem.Enabled = false;
            else
                LétszámGazdálkodásToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(76, 1) == "0")
                LekérdezésekToolStripMenuItem.Enabled = false;
            else
                LekérdezésekToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(77, 1) == "0")
                TúlóraEllenőrzésToolStripMenuItem.Enabled = false;
            else
                TúlóraEllenőrzésToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(78, 1) == "0")
                BeosztásNaplóToolStripMenuItem.Enabled = false;
            else
                BeosztásNaplóToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(79, 1) == "0")
                MunkalapAdatokkarbantartásaToolStripMenuItem.Enabled = false;
            else
                MunkalapAdatokkarbantartásaToolStripMenuItem.Enabled = true;

            if (panels2.Text.Substring(84, 1) == "0")
                MunkalapKészítésToolStripMenuItem.Enabled = false;
            else
                MunkalapKészítésToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(85, 1) == "0")
                MunkalapDekádolóToolStripMenuItem.Enabled = false;
            else
                MunkalapDekádolóToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(88, 1) == "0")
                ReklámNyilvántartásToolStripMenuItem.Enabled = false;
            else
                ReklámNyilvántartásToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(89, 1) == "0")
                JárműLétrehozásMozgásToolStripMenuItem.Enabled = false;
            else
                JárműLétrehozásMozgásToolStripMenuItem.Enabled = true;

            if (panels2.Text.Substring(91, 1) == "0")
                SérülésNyilvántartásToolStripMenuItem.Enabled = false;
            else
                SérülésNyilvántartásToolStripMenuItem.Enabled = true;

            if (panels2.Text.Substring(97, 1) == "0")
                FőkönyvToolStripMenuItem.Enabled = false;
            else
                FőkönyvToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(98, 1) == "0")
                JárműKarbantartásiAdatokToolStripMenuItem.Enabled = false;
            else
                JárműKarbantartásiAdatokToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(99, 1) == "0")
                SzerelvényToolStripMenuItem.Enabled = false;
            else
                SzerelvényToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(100, 1) == "0")
                T5C5FutásnapRögzítésToolStripMenuItem.Enabled = false;
            else
                T5C5FutásnapRögzítésToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(101, 1) == "0")
                T5C5FutásnapÜtemezésToolStripMenuItem.Enabled = false;
            else
                T5C5FutásnapÜtemezésToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(102, 1) == "0")
                T5C5VJavításÜtemezésToolStripMenuItem.Enabled = false;
            else
                T5C5VJavításÜtemezésToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(105, 1) == "0")
                T5C5AdatokMódosításaToolStripMenuItem.Enabled = false;
            else
                T5C5AdatokMódosításaToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(107, 1) == "0")
                NapiAdatokToolStripMenuItem.Enabled = false;
            else
                NapiAdatokToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(108, 1) == "0")
                FogaskerekűToolStripMenuItem.Enabled = false;
            else
                FogaskerekűToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(109, 1) == "0")
                TW6000AdatokToolStripMenuItem.Enabled = false;
            else
                TW6000AdatokToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(112, 1) == "0")
                ICSKCSVToolStripMenuItem.Enabled = false;
            else
                ICSKCSVToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(114, 1) == "0")
                CAF5CAF9AdatokÉsÜtemezésToolStripMenuItem.Enabled = false;
            else
                CAF5CAF9AdatokÉsÜtemezésToolStripMenuItem.Enabled = true;

            if (panels2.Text.Substring(124, 1) == "0")
                nosztalgiaToolStripMenuItem.Enabled = false;
            else
                nosztalgiaToolStripMenuItem.Enabled = true;

            if (panels2.Text.Substring(129, 1) == "0")
                TTTPToolStripMenuItem.Enabled = false;
            else
                TTTPToolStripMenuItem.Enabled = true;

            if (panels2.Text.Substring(159, 1) == "0")
                EsztergaKarbantartásToolStripMenuItem.Enabled = false;
            else
                EsztergaKarbantartásToolStripMenuItem.Enabled = true;

            if (panels2.Text.Substring(164, 1) == "0")
                kerékesztergálásSzervezésToolStripMenuItem.Enabled = false;
            else
                kerékesztergálásSzervezésToolStripMenuItem.Enabled = true;

            if (panels2.Text.Substring(167, 1) == "0")
                kerékesztergálásiAdatokBarossToolStripMenuItem.Enabled = false;
            else
                kerékesztergálásiAdatokBarossToolStripMenuItem.Enabled = true;

            if (panels2.Text.Substring(169, 1) == "0")
                karbantartásiMunkalapokToolStripMenuItem.Enabled = false;
            else
                karbantartásiMunkalapokToolStripMenuItem.Enabled = true;

            if (panels2.Text.Substring(176, 1) == "0")
                T5C5UtastérFűtésToolStripMenuItem.Enabled = false;
            else
                T5C5UtastérFűtésToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(177, 1) == "0")
                KidobóKészítésToolStripMenuItem.Enabled = false;
            else
                KidobóKészítésToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(178, 1) == "0")
                ÁllományTáblaToolStripMenuItem.Enabled = false;
            else
                ÁllományTáblaToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(179, 1) == "0")
                ToolStripMenuItem1.Enabled = false;
            else
                ToolStripMenuItem1.Enabled = true;
            if (panels2.Text.Substring(180, 1) == "0")
                ToolStripMenuItem2.Enabled = false;
            else
                ToolStripMenuItem2.Enabled = true;

            if (panels2.Text.Substring(182, 1) == "0")
                FőmérnökségiAdatokToolStripMenuItem.Enabled = false;
            else
                FőmérnökségiAdatokToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(183, 1) == "0")
                TelephelyiAdatokÖsszesítéseToolStripMenuItem.Enabled = false;
            else
                TelephelyiAdatokÖsszesítéseToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(184, 1) == "0")
                KiadásiForteAdatokToolStripMenuItem.Enabled = false;
            else
                KiadásiForteAdatokToolStripMenuItem.Enabled = true;

            if (panels2.Text.Substring(185, 1) == "0")
                KerékátmérőNyilvántartásSAPBerendezésekToolStripMenuItem.Enabled = false;
            else
                KerékátmérőNyilvántartásSAPBerendezésekToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(187, 1) == "0")
                DigitálisFőkönyvToolStripMenuItem.Enabled = false;
            else
                DigitálisFőkönyvToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(188, 1) == "0")
                SAPOsztályToolStripMenuItem.Enabled = false;
            else
                SAPOsztályToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(189, 1) == "0")
                AkkumulátorNyilvántartásToolStripMenuItem1.Enabled = false;
            else
                AkkumulátorNyilvántartásToolStripMenuItem1.Enabled = true;

            if (panels2.Text.Substring(199, 1) == "0")
                ÜzenetekToolStripMenuItem.Enabled = false;
            else
                ÜzenetekToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(201, 1) == "0")
                UtasításokToolStripMenuItem.Enabled = false;
            else
                UtasításokToolStripMenuItem.Enabled = true;

            if (panels2.Text.Substring(219, 1) == "0")
                RezsiRaktárToolStripMenuItem.Enabled = false;
            else
                RezsiRaktárToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(227, 1) == "0")
                eszközNyilvántartásToolStripMenuItem.Enabled = false;
            else
                eszközNyilvántartásToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(228, 1) == "0")
                épületTartozékNyilvántartásToolStripMenuItem.Enabled = false;
            else
                épületTartozékNyilvántartásToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(229, 1) == "0")
                SzerszámNyilvántartásToolStripMenuItem.Enabled = false;
            else
                SzerszámNyilvántartásToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(233, 1) == "0")
                ÉpületTakarításToolStripMenuItem.Enabled = false;
            else
                ÉpületTakarításToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(234, 1) == "0")
                ÉpületTakarításTörzsAdatokToolStripMenuItem.Enabled = false;
            else
                ÉpületTakarításTörzsAdatokToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(236, 1) == "0")
                VédőeszközToolStripMenuItem.Enabled = false;
            else
                VédőeszközToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(239, 1) == "0")
                BehajtásiEngedélyToolStripMenuItem.Enabled = false;
            else
                BehajtásiEngedélyToolStripMenuItem.Enabled = true;
            if (panels2.Text.Substring(246, 1) == "0")
                KülsősDolgozókBelépésiÉsBehajtásaToolStripMenuItem.Enabled = false;
            else
                KülsősDolgozókBelépésiÉsBehajtásaToolStripMenuItem.Enabled = true;
        }


        #region Főoldal elemek
        private void AblakFőoldal_Load(object sender, EventArgs e)
        {
            Timer1.Enabled = true;
            Timer2.Enabled = true;
        }

        private void AblakFőoldal_Shown(object sender, EventArgs e)
        {
            lbltelephely.Text = Program.PostásTelephely;
            lblVerzió.Text = $"Verzió: {Application.ProductVersion}";
            Panels1.Text = Program.PostásNév;
            panels2.Text = Program.PostásJogkör; // új jogosultság
            panels4.Text = Program.PostásTelephely;
            Könyvtárak_Létrehozása();
            Képetvált();
            Járművek_Mozogtak();
            Menükbeállítása();
            Üzenetkiírása();
            Utasításkiírása();
            Verziószám_kiírás();
            Program_változó(lbltelephely.Text.Trim());
        }

        private void Könyvtárak_Létrehozása()
        {
            string hely = $@"{Application.StartupPath}\{lbltelephely.Text.Trim()}\Adatok";
            if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

            hely = $@"{Application.StartupPath}\{lbltelephely.Text.Trim()}\adatok\Főkönyv";
            if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

            hely = $@"{Application.StartupPath}\{lbltelephely.Text.Trim()}\adatok\Főkönyv\{DateTime.Today.Year}\Nap";
            if (Directory.Exists(hely)) Directory.CreateDirectory(hely);

            hely = $@"{Application.StartupPath}\{lbltelephely.Text.Trim()}\adatok\Beosztás";
            if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

            hely = $@"{Application.StartupPath}\{lbltelephely.Text.Trim()}\adatok\Főkönyv\Futás";
            if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

            hely = $@"{Application.StartupPath}\{lbltelephely.Text.Trim()}\adatok\Naplózás";
            if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

            hely = $@"{Application.StartupPath}\{lbltelephely.Text.Trim()}\adatok\Üzenetek";
            if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

            hely = $@"{Application.StartupPath}\{lbltelephely.Text.Trim()}\adatok\Villamos";
            if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

            hely = $@"{Application.StartupPath}\{lbltelephely.Text.Trim()}\adatok\Hibanapló";
            if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

            hely = $@"{Application.StartupPath}\{lbltelephely.Text.Trim()}\adatok\Segéd";
            if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

            hely = $@"{Application.StartupPath}\{lbltelephely.Text.Trim()}\Hangok";
            if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

            hely = $@"{Application.StartupPath}\{lbltelephely.Text.Trim()}\Szerszám";
            if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

            hely = $@"{Application.StartupPath}\{lbltelephely.Text.Trim()}\Képek";
            if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

            hely = Application.StartupPath + @"\Főmérnökség\Napló";
            if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

            hely = $@"{Application.StartupPath}\{lbltelephely.Text.Trim()}\adatok\főkönyv\{DateTime.Today.Year}";
            if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

            hely = $@"{Application.StartupPath}\{lbltelephely.Text.Trim()}\adatok\Főkönyv\{DateTime.Today.Year}\Zser";
            if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

            hely = $@"{Application.StartupPath}\{lbltelephely.Text.Trim()}\adatok\Főkönyv\{DateTime.Today.Year}\Nap";
            if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

            hely = $@"{Application.StartupPath}\Főmérnökség\napló\napló{DateTime.Now.Year}.mdb";
            if (!Exists(hely)) Adatbázis_Létrehozás.Kocsitípusanapló(hely);
        }

        private void AblakFőoldal_Resize(object sender, EventArgs e)
        {
            lbltelephely.Width = Width - 480;
            lblVerzió.Left = lbltelephely.Width + lbltelephely.Left + 4;
            lblVerzió.Width = 255;

            Képkeret.Left = 0;
            Képkeret.Top = LblÓra.Top + LblÓra.Height;
            Képkeret.Height = (int)Math.Round(Convert.ToInt32(Height - LblÓra.Height - Menü.Height - 45) / 2d);
            Képkeret.Width = (int)Math.Round(Convert.ToInt32(Width - 20) / 2d);

            Üzenetektext.Left = Képkeret.Width;
            Üzenetektext.Top = Képkeret.Top;
            Üzenetektext.Height = Képkeret.Height;
            Üzenetektext.Width = Képkeret.Width;

            Utasításoktext.Left = 0;
            Utasításoktext.Top = Képkeret.Top + Képkeret.Height;
            Utasításoktext.Height = Képkeret.Height;
            Utasításoktext.Width = Képkeret.Width;

            Képkeret1.Left = Üzenetektext.Left;
            Képkeret1.Top = Utasításoktext.Top;
            Képkeret1.Height = Képkeret.Height;
            Képkeret1.Width = Képkeret.Width;

            Btnüzenetfrissítés.Top = Képkeret.Top;
            Btnüzenetfrissítés.Left = Üzenetektext.Left - Btnüzenetfrissítés.Width;

            Btnutasításfrissítés.Top = Képkeret1.Top;
            Btnutasításfrissítés.Left = Képkeret1.Left;

            Alsó.Left = lbltelephely.Left;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            // Órát írja ki
            LblÓra.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                // 5 percenként frissíti az üzeneteket, stb.
                // beállítása a tulajdonságokban  5 perc=300000 érték
                Járművek_Mozogtak();
                Képetvált();
                Üzenetkiírása();
                Utasításkiírása();

                // ha látszódik a figyelmeztetés, akkor kiléptetjük
                if (Figyelmeztetés.Visible == true)
                {
                    if (!PerformanceCounterCategory.Exists("Processor"))
                    {
                        if (MessageBox.Show("Az objektumfeldolgozó nem létezik! Kilép a program.", "A program karbantartás miatt kiléptet.", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            Close();
                        }
                        else
                        {
                            Close();
                        }
                    }

                    if (!PerformanceCounterCategory.CounterExists("% Processor Time", "Processor"))
                    {
                        if (MessageBox.Show("Számláló % Processzoridő nem létezik! Kilép a program.", "A program karbantartás miatt kiléptet.", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            Close();
                        }
                        else
                        {
                            Close();
                        }
                    }
                    myCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

                    if ((int)Math.Round(myCounter.NextValue()) < 2)
                        Close();
                }


                // ha létezik a fájl akkor megjelenítjük a figyelmeztető üzenetet
                string hely = Application.StartupPath + @"\Főmérnökség\Szerszám\a.txt";
                if (Exists(hely))
                {
                    // ha épp dolgozik akkor figyelmezetjük, hogy ki kell lépni
                    FigyKiírás($"Karbantartás miatt a program\n ~{DateTime.Now.AddMinutes(5):HH:mm}- kor\n kiléptet.");
                }

                Verziószám_kiírás();
                // Verzió váltás  akkor megjelenítjük a figyelmeztető üzenetet
                if (Convert.ToDouble(TároltVerzió.Text.Trim()) > Convert.ToDouble(Application.ProductVersion.Replace(".", "").Trim()))
                {
                    FigyKiírás($"Elavult a program verzió,\n ezért a program ki fog léptetni\n ~{DateTime.Now.AddMinutes(5d):HH:mm}- kor.");
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("Meghatározatlan hiba"))
                {
                    HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void FigyKiírás(string szöveg)
        {
            Figyelmeztetés.Left = lbltelephely.Left;
            Figyelmeztetés.Top = 100;
            Figyelmeztetés.Width = lbltelephely.Width;
            Figyelmeztetés.Height = 360;
            Figyelmeztetés.Text = szöveg;
            Figyelmeztetés.Visible = true;

            Képkeret.Visible = false;
            Képkeret1.Visible = false;
            Üzenetektext.Visible = false;
            Utasításoktext.Visible = false;
            Btnüzenetfrissítés.Visible = false;
            Btnutasításfrissítés.Visible = false;
        }


        private void Képetvált()
        {
            int választottkép = -1;
            int választottkép1 = -1;
            string[] dirs = { "_" };

            try
            {
                string hely = $@"{Application.StartupPath}\{lbltelephely.Text.Trim()}\képek";
                if (!Directory.Exists(hely)) return;


                //Ezt lehet allitani ha szeretnenk, ertekek amiket var: helye a kepnek, Max merete bajtban,
                //kep max szélessége és magassága és az összesen mennyi pixel lehet a kép
                //Most ki van veve belole az az opcio hogy megadjuk neki az ossz pixel mennyiséget.
                //Amik commentezve vannak reszek a programban azok a reszek amik segitenek abban
                //500_000 bájt = 500KB
                //szelesseg magassag pixelben
                //2_073_600 pixel a szelesseg es a magassag szorzata az ossz pixel

                dirs = Directory.GetFiles(hely, "*.jpg")
                                .Where(kép => ÉrvényesKép(kép, 500_000, 1920, 1080/*, 2_073_600*/))
                                .ToArray();
                if (dirs.Length < 2) return;

                //Azért van do while hogy a két kép ne legyen ugyanaz egyszerre
                Random rnd = new Random();
                választottkép = rnd.Next(dirs.Length);
                do
                {
                    választottkép1 = rnd.Next(dirs.Length);
                } while (választottkép1 == választottkép);

                string helykép = dirs[választottkép];
                string helykép1 = dirs[választottkép1];
                Kezelő_Kép.KépMegnyitás(Képkeret, helykép, ToolTip1);
                Kezelő_Kép.KépMegnyitás(Képkeret1, helykép1, ToolTip1);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("Meghatározatlan hiba"))
                {
                    HibaNapló.Log(ex.Message, this.ToString() + $"\nKép1:{dirs?[választottkép]}\nKép2:{dirs?[választottkép1]}", ex.StackTrace, ex.Source, ex.HResult);
                    MessageBox.Show(ex.Message + $"\nKép1:{dirs?[választottkép]}\nKép2:{dirs?[választottkép1]}" + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ÉrvényesKép(string helykép, long MaxMéret, int MaxSzélesség, int MaxMagasság/*, long ÖsszPixel*/)
        {
            try
            {
                // A FileInfo-val lekerdezhetjuk a fajl adatait (pl. meret, nev),
                // es fajlmuveleteket vegezhetunk rajta, mint a torles vagy datumok kezelese.

                FileInfo Flnf = new FileInfo(helykép);

                if (Flnf.Length > MaxMéret)
                {
                    FájlTörlés(helykép);
                    return false;
                }

                using (Image kép = Image.FromFile(helykép))
                {
                    //int ÖsszPixel = kép.Width * kép.Height;
                    if (kép.Width > MaxSzélesség || kép.Height > MaxMagasság /*|| ÖsszPixel > ÖsszPixel*/)
                    {
                        kép.Dispose();
                        FájlTörlés(helykép);
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                FájlTörlés(helykép);
                return false;
            }
        }

        private void FájlTörlés(string helykép) { }

        private void Súgómenü_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Főoldal.html";
                Module_Excel.Megnyitás(hely);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AblakFőoldal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift) Shift_le = true;
            if (e.Alt) Alt_le = true;
            if (e.Control) CTRL_le = true;
        }

        private void AblakFőoldal_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Shift) Shift_le = false;
            if (e.Alt) Alt_le = false;
            if (e.Control) CTRL_le = false;
        }

        private void Rejtett_Frissít_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{panels4.Text.Trim()}\Adatok\belépés.mdb";

                List<Adat_Belépés_Jogosultságtábla> Adatok = Kéz_Jogosultság.Lista_Adatok(hely);

                Adat_Belépés_Jogosultságtábla Elem = (from a in Adatok
                                                      where a.Név.ToUpper() == Panels1.Text.Trim().ToUpper()
                                                      select a).FirstOrDefault();

                if (Elem != null)
                {
                    panels2.Text = Elem.Jogkörúj1;
                    Program.PostásJogkör = Elem.Jogkörúj1;
                    Menükbeállítása();
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Telephelyekfeltöltése()
        {
            // Adatbázis megnyitásának előkészítése
            TelephelyListaFeltöltés();
            Cmbtelephely.Items.Clear();
            foreach (Adat_Kiegészítő_Könyvtár rekord in AdatokKönyvtár)
                Cmbtelephely.Items.Add(rekord.Név);
        }

        private void TelephelyListaFeltöltés()
        {
            try
            {
                AdatokKönyvtár.Clear();
                AdatokKönyvtár = KézKönyvtár.Lista_Adatok();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion


        #region Üzenet
        private void Btnüzenetfrissítés_Click(object sender, EventArgs e)
        {
            Üzenetkiírása();
        }

        private void Üzenetkiírása()
        {
            try
            {
                // megkeressük azt az érvényes utasítást amit még nem olvastunk
                string hely = $@"{Application.StartupPath}\{lbltelephely.Text.Trim()}\adatok\üzenetek\";
                if (!Directory.Exists(hely)) throw new HibásBevittAdat("A program nem találja a hálózatot, ezért kilép.");

                hely = $@"{Application.StartupPath}\{lbltelephely.Text.Trim()}\adatok\üzenetek\{DateTime.Now.Year}üzenet.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.ALÜzenetadatok(hely);

                Adat_Üzenet rekord = Kéz_Üzenet.ElsőOlvasatlan(hely);
                if (rekord != null)
                {
                    Üzenetektext.Text = $"Dátum: {rekord.Mikor:yyyy.MMMM dd. HH:mm}\r\n";
                    Üzenetektext.Text += $"Írta: {rekord.Írta.Trim()}\r\n\r\n";
                    Üzenetektext.Text += "Üzenet tartalma:\r\n\r\n" + rekord.Szöveg.Trim();
                }
                else
                    Üzenetektext.Text = "Nincs Olvasatlan Üzenet.";
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (ex.Message == "A program nem találja a hálózatot, ezért kilép.") Kilépés();
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Járművek_Mozogtak()
        {
            try
            {
                NaplóListaFeltöltés();
                List<Adat_Jármű_Napló> Adatok = (from a in AdatokNapló
                                                 where a.Céltelep == lbltelephely.Text.Trim()
                                                 && a.Üzenet == 0
                                                 select a).ToList();

                if (Adatok != null && Adatok.Count > 0)
                {
                    string szöveg = "A következő járművek érkeznek a telephelyre a következő telephely(ek)ről:\r\n";
                    foreach (Adat_Jármű_Napló rekord in Adatok)
                        szöveg += $"{rekord.Honnan.Trim()} : {rekord.Azonosító.Trim()}\r\n";

                    Adat_Üzenet ElemÜzenet = new Adat_Üzenet(0, szöveg.Trim(), "Program", DateTime.Now, 0);
                    string hely = $@"{Application.StartupPath}\{lbltelephely.Text.Trim()}\Adatok\üzenetek\{DateTime.Today.Year}Üzenet.mdb";
                    Kéz_Üzenet.Rögzítés(hely, ElemÜzenet);

                    //Naplózás
                    hely = $@"{Application.StartupPath}\Főmérnökség\napló\napló{DateTime.Today.Year}.mdb";
                    KézNapló.Módosítás(hely, Adatok);
                }

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("Meghatározatlan hiba"))
                {
                    HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void NaplóListaFeltöltés()
        {
            try
            {
                AdatokNapló.Clear();
                // ha nincs naplófájl akkor létrehozzuk
                string hely = $@"{Application.StartupPath}\Főmérnökség\napló\napló{DateTime.Today.Year}.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.Kocsitípusanapló(hely);
                AdatokNapló = KézNapló.Lista_adatok(hely);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region Utasítás
        private void Utasításkiírása()
        {
            try
            {
                Utasításoktext.Text = "Nincs Olvasatlan Utasítás.";

                string hely = $@"{Application.StartupPath}\{lbltelephely.Text.Trim()}\adatok\üzenetek\";
                if (!Directory.Exists(hely)) throw new HibásBevittAdat("A program nem találja a hálózatot, ezért kilép.");

                // megkeressük azt az érvényes utasítást amit még nem olvastunk
                hely = $@"{Application.StartupPath}\{lbltelephely.Text.Trim()}\adatok\üzenetek\{DateTime.Now.Year}utasítás.mdb";

                if (!Exists(hely)) Adatbázis_Létrehozás.UtasításadatokTábla(hely);

                Adat_Utasítás rekord = KézUtasítás.ElsőOlvasatlan(hely);
                if (rekord != null)
                {
                    Utasításoktext.Text = $"Dátum: {rekord.Mikor:yyyy.MMMM dd. HH:mm}\r\n";
                    Utasításoktext.Text += "Üzenet tartalma:\r\n\r\n" + rekord.Szöveg.Trim();
                    Utasításoktext.Text += $"Írta: {rekord.Írta.Trim()}\r\n\r\n";
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (ex.Message == "A program nem találja a hálózatot, ezért kilép.") Kilépés();
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Btnutasításfrissítés_Click(object sender, EventArgs e)
        {
            Utasításkiírása();
        }
        #endregion


        #region Hardver_kulcs


        private void BtnHardverkulcs_Click(object sender, EventArgs e)
        {
            try
            {

                string hely = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Villamos";
                // ha a könyvtás akkor létre hozzuk
                if (!Exists(hely)) Directory.CreateDirectory(hely);
                hely += @"\Kulcs.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.Kulcs_Adatok(hely);

                // A jogokat beírjuk
                string adat1 = MyF.MÁSRövidkód(Panels1.Text.Trim());

                Kezelő_Kulcs_Fekete KézFekete = new Kezelő_Kulcs_Fekete();
                Kezelő_Kulcs Kéz = new Kezelő_Kulcs();
                List<Adat_Kulcs> AdatokTeljes = KézFekete.Lista_Adatok();

                List<Adat_Kulcs> AdatokSzűrt = (from a in AdatokTeljes
                                                where a.Adat1.Contains(adat1.Substring(0, adat1.Length - 3))
                                                select a).ToList();

                foreach (Adat_Kulcs rekord in AdatokSzűrt)
                {
                    Adat_Kulcs ADAT = new Adat_Kulcs(rekord.Adat1, rekord.Adat2, rekord.Adat3);
                    Kéz.Rögzít(ADAT);
                    KézFekete.Törlés(ADAT);
                }
                MessageBox.Show("Az adatok rögzítése megtörtént. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnHardverkulcs.Visible = false;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region Beállítások menük

        AblakFelhasználó Új_ablak_Felhasználó;
        private void FelhasználókBeállításaMenü_Click(object sender, EventArgs e)
        {
            if (Új_ablak_Felhasználó == null)
            {
                Új_ablak_Felhasználó = new AblakFelhasználó();
                Új_ablak_Felhasználó.FormClosed += Új_ablak_Felhasználó_Closed;
                Új_ablak_Felhasználó.Show();
            }
            else
            {
                Új_ablak_Felhasználó.Activate();
                Új_ablak_Felhasználó.WindowState = FormWindowState.Maximized;
            }
        }

        private void Új_ablak_Felhasználó_Closed(object sender, FormClosedEventArgs e)
        {
            Új_ablak_Felhasználó = null;
        }

        Ablak_alap_program_kiadás Új_ablak_alap_program_kiadás;
        private void ProgramAdatokKiadásiAdatokToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_ablak_alap_program_kiadás == null)
            {
                Új_ablak_alap_program_kiadás = new Ablak_alap_program_kiadás();
                Új_ablak_alap_program_kiadás.FormClosed += Új_ablak_alap_program_kiadás_Closed;
                Új_ablak_alap_program_kiadás.Show();
            }
            else
            {
                Új_ablak_alap_program_kiadás.Activate();
                Új_ablak_alap_program_kiadás.WindowState = FormWindowState.Maximized;
            }
        }

        private void Új_ablak_alap_program_kiadás_Closed(object sender, FormClosedEventArgs e)
        {
            Új_ablak_alap_program_kiadás = null;

        }

        Ablak_alap_program_személy Új_ablak_alap_program_Személy;
        private void ProgramAdatokSzemélyMenü_Click(object sender, EventArgs e)
        {
            if (Új_ablak_alap_program_Személy == null)
            {
                Új_ablak_alap_program_Személy = new Ablak_alap_program_személy();
                Új_ablak_alap_program_Személy.FormClosed += Új_ablak_alap_program_Személy_Closed;
                Új_ablak_alap_program_Személy.Show();
            }
            else
            {
                Új_ablak_alap_program_Személy.Activate();
                Új_ablak_alap_program_Személy.WindowState = FormWindowState.Maximized;
            }
        }

        private void Új_ablak_alap_program_Személy_Closed(object sender, FormClosedEventArgs e)
        {
            Új_ablak_alap_program_Személy = null;
        }

        Ablak_alap_program_egyéb Új_Ablak_alap_program_egyéb;
        private void ProgramAdatokEgyébToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_alap_program_egyéb == null)
            {
                Új_Ablak_alap_program_egyéb = new Ablak_alap_program_egyéb();
                Új_Ablak_alap_program_egyéb.FormClosed += Új_Ablak_alap_program_egyéb_Closed;
                Új_Ablak_alap_program_egyéb.Show();
            }
            else
            {
                Új_Ablak_alap_program_egyéb.Activate();
                Új_Ablak_alap_program_egyéb.WindowState = FormWindowState.Maximized;
            }
        }

        private void Új_Ablak_alap_program_egyéb_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_alap_program_egyéb = null;
        }

        public static Ablak_Ciklus Új_Ablak_Ciklus;
        private void CiklusrendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Ciklus == null)
            {
                Új_Ablak_Ciklus = new Ablak_Ciklus();
                Új_Ablak_Ciklus.FormClosed += Új_Ablak_Ciklus_Closed;
                Új_Ablak_Ciklus.Show();
            }
            else
            {
                Új_Ablak_Ciklus.Activate();
                Új_Ablak_Ciklus.WindowState = FormWindowState.Maximized;
            }

        }

        private void Új_Ablak_Ciklus_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Ciklus = null;
        }

        Ablak_Váltós Új_Ablak_Váltós;
        private void VáltósMunkarendÉsTúlóraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Váltós == null)
            {
                Új_Ablak_Váltós = new Ablak_Váltós();
                Új_Ablak_Váltós.FormClosed += Új_Ablak_Váltós_FormClosed;
                Új_Ablak_Váltós.Show();
            }
            else
            {
                Új_Ablak_Váltós.Activate();
                Új_Ablak_Váltós.WindowState = FormWindowState.Maximized;
            }
        }


        private void Új_Ablak_Váltós_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Váltós = null;
        }

        Ablak_technológia Új_Ablak_technológia;
        private void JárműTechnológiákToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_technológia == null)
            {
                Új_Ablak_technológia = new Ablak_technológia();
                Új_Ablak_technológia.FormClosed += Új_Ablak_technológia_FormClosed;
                Új_Ablak_technológia.Show();
            }
            else
            {
                Új_Ablak_technológia.Activate();
                Új_Ablak_technológia.WindowState = FormWindowState.Maximized;
            }
        }

        private void Új_Ablak_technológia_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_technológia = null;
        }
        #endregion


        #region Információk

        Ablak_üzenet Új_Ablak_üzenet;
        private void ÜzenetekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_üzenet == null)
            {
                Új_Ablak_üzenet = new Ablak_üzenet();
                Új_Ablak_üzenet.FormClosed += Új_Ablak_üzenet_FormClosed;
                Új_Ablak_üzenet.Show();
            }
            else
            {
                Új_Ablak_üzenet.Activate();
                Új_Ablak_üzenet.WindowState = FormWindowState.Maximized;
            }

        }


        private void Új_Ablak_üzenet_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_üzenet = null;
        }


        private void Üzenetektext_DoubleClick(object sender, EventArgs e)
        {
            if (Új_Ablak_üzenet == null)
            {
                Új_Ablak_üzenet = new Ablak_üzenet();
                Új_Ablak_üzenet.FormClosed += Új_Ablak_üzenet_FormClosed;
                Új_Ablak_üzenet.Show();
            }
            else
            {
                Új_Ablak_üzenet.Activate();
                Új_Ablak_üzenet.WindowState = FormWindowState.Maximized;
            }
        }


        Ablak_Utasítás Új_Ablak_utasítás;
        private void UtasításokToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_utasítás == null)
            {
                Új_Ablak_utasítás = new Ablak_Utasítás();
                Új_Ablak_utasítás.FormClosed += Új_Ablak_utasítás_FormClosed;
                Új_Ablak_utasítás.Show();
            }
            else
            {
                Új_Ablak_utasítás.Activate();
                Új_Ablak_utasítás.WindowState = FormWindowState.Maximized;
            }
        }


        private void Utasításoktext_DoubleClick(object sender, EventArgs e)
        {
            if (Új_Ablak_utasítás == null)
            {
                Új_Ablak_utasítás = new Ablak_Utasítás();
                Új_Ablak_utasítás.FormClosed += Új_Ablak_utasítás_FormClosed;
                Új_Ablak_utasítás.Show();
            }
            else
            {
                Új_Ablak_utasítás.Activate();
                Új_Ablak_utasítás.WindowState = FormWindowState.Maximized;
            }
        }

        private void Új_Ablak_utasítás_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_utasítás = null;
        }

        #endregion


        #region Dolgozói adatok 
        Ablak_Felvétel Új_Ablak_Felvétel;
        private void DolgozóFelvételátvételvezénylésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Felvétel == null)
            {
                Új_Ablak_Felvétel = new Ablak_Felvétel();
                Új_Ablak_Felvétel.FormClosed += Új_Ablak_Felvétel_FormClosed;
                Új_Ablak_Felvétel.Show();
            }
            else
            {
                Új_Ablak_Felvétel.Activate();
                Új_Ablak_Felvétel.WindowState = FormWindowState.Maximized;
            }

        }

        private void Új_Ablak_Felvétel_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Felvétel = null;
        }




        Ablak_Dolgozóialapadatok Új_Ablak_Dolgozóialapadatok;

        private void DolgozóiAlapadatokToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Dolgozóialapadatok == null)
            {
                Új_Ablak_Dolgozóialapadatok = new Ablak_Dolgozóialapadatok();
                Új_Ablak_Dolgozóialapadatok.FormClosed += Új_Ablak_Dolgozóialapadatok_FormClosed;
                Új_Ablak_Dolgozóialapadatok.Show();
            }
            else
            {
                Új_Ablak_Dolgozóialapadatok.Activate();
                Új_Ablak_Dolgozóialapadatok.WindowState = FormWindowState.Maximized;
            }
        }

        private void Új_Ablak_Dolgozóialapadatok_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Dolgozóialapadatok = null;
        }




        Ablak_Beosztás Új_Ablak_Beosztás;

        private void BeosztásToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Beosztás == null)
            {
                Új_Ablak_Beosztás = new Ablak_Beosztás();
                Új_Ablak_Beosztás.FormClosed += Új_Ablak_Beosztás_FormClosed;
                Új_Ablak_Beosztás.Show();
            }
            else
            {
                Új_Ablak_Beosztás.Activate();
                Új_Ablak_Beosztás.WindowState = FormWindowState.Maximized;
            }
        }
        private void Új_Ablak_Beosztás_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Beosztás = null;
        }



        Ablak_Beosztás_Napló Új_Ablak_Beosztás_Napló;
        private void BeosztásNaplóToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Beosztás_Napló == null)
            {
                Új_Ablak_Beosztás_Napló = new Ablak_Beosztás_Napló();
                Új_Ablak_Beosztás_Napló.FormClosed += Új_Ablak_Beosztás_Napló_FormClosed;
                Új_Ablak_Beosztás_Napló.Show();
            }
            else
            {
                Új_Ablak_Beosztás_Napló.Activate();
                Új_Ablak_Beosztás_Napló.WindowState = FormWindowState.Maximized;
            }
        }

        private void Új_Ablak_Beosztás_Napló_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Beosztás_Napló = null;
        }




        Ablak_Jelenléti Új_Ablak_Jelenléti;
        private void ListákJelenlétiÍvekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Jelenléti == null)
            {
                Új_Ablak_Jelenléti = new Ablak_Jelenléti();
                Új_Ablak_Jelenléti.FormClosed += Új_Ablak_Jelenléti_Napló_FormClosed;
                Új_Ablak_Jelenléti.Show();
            }
            else
            {
                Új_Ablak_Jelenléti.Activate();
                Új_Ablak_Jelenléti.WindowState = FormWindowState.Maximized;
            }
        }

        private void Új_Ablak_Jelenléti_Napló_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Jelenléti = null;
        }




        Ablak_Szatube Új_Ablak_Szatube;

        private void SzabadságTúlóraBetegállományToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (Új_Ablak_Szatube == null)
            {
                Új_Ablak_Szatube = new Ablak_Szatube();
                Új_Ablak_Szatube.FormClosed += Új_Ablak_Szatube_FormClosed;
                Új_Ablak_Szatube.Show();
            }
            else
            {
                Új_Ablak_Szatube.Activate();
                Új_Ablak_Szatube.WindowState = FormWindowState.Maximized;
            }
        }
        private void Új_Ablak_Szatube_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Szatube = null;
        }




        Ablak_Oktatások Új_Ablak_Oktatások;

        private void OktatásokToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Oktatások == null)
            {
                Új_Ablak_Oktatások = new Ablak_Oktatások();
                Új_Ablak_Oktatások.FormClosed += Új_Ablak_Oktatások_FormClosed;
                Új_Ablak_Oktatások.Show();
            }
            else
            {
                Új_Ablak_Oktatások.Activate();
                Új_Ablak_Oktatások.WindowState = FormWindowState.Maximized;
            }
        }
        private void Új_Ablak_Oktatások_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Oktatások = null;
        }

        Ablak_Túlóra_Figyelés Új_Ablak_Túlóra_Figyelés;
        private void TúlóraEllenőrzésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Túlóra_Figyelés == null)
            {
                Új_Ablak_Túlóra_Figyelés = new Ablak_Túlóra_Figyelés();
                Új_Ablak_Túlóra_Figyelés.FormClosed += Új_Ablak_Túlóra_Figyelés_FormClosed;
                Új_Ablak_Túlóra_Figyelés.Show();
            }
            else
            {
                Új_Ablak_Túlóra_Figyelés.Activate();
                Új_Ablak_Túlóra_Figyelés.WindowState = FormWindowState.Maximized;
            }
        }
        private void Új_Ablak_Túlóra_Figyelés_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Túlóra_Figyelés = null;
        }


        Ablak_DolgozóiLekérdezések Új_Ablak_DolgozóiLekérdezések;
        private void LekérdezésekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_DolgozóiLekérdezések == null)
            {
                Új_Ablak_DolgozóiLekérdezések = new Ablak_DolgozóiLekérdezések();
                Új_Ablak_DolgozóiLekérdezések.FormClosed += Új_Ablak_DolgozóiLekérdezések_FormClosed;
                Új_Ablak_DolgozóiLekérdezések.Show();
            }
            else
            {
                Új_Ablak_DolgozóiLekérdezések.Activate();
                Új_Ablak_DolgozóiLekérdezések.WindowState = FormWindowState.Maximized;
            }
        }
        private void Új_Ablak_DolgozóiLekérdezések_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_DolgozóiLekérdezések = null;
        }


        AblakLétszámgazdálkodás Új_AblakLétszámgazdálkodás;

        private void LétszámGazdálkodásToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_AblakLétszámgazdálkodás == null)
            {
                Új_AblakLétszámgazdálkodás = new AblakLétszámgazdálkodás();
                Új_AblakLétszámgazdálkodás.FormClosed += Új_AblakLétszámgazdálkodás_FormClosed;
                Új_AblakLétszámgazdálkodás.Show();
            }
            else
            {
                Új_AblakLétszámgazdálkodás.Activate();
                Új_AblakLétszámgazdálkodás.WindowState = FormWindowState.Maximized;
            }
        }

        private void Új_AblakLétszámgazdálkodás_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_AblakLétszámgazdálkodás = null;
        }




        Ablak_Munkalap_admin Új_Ablak_Munkalap_admin;
        private void MunkalapAdatokkarbantartásaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Munkalap_admin == null)
            {
                Új_Ablak_Munkalap_admin = new Ablak_Munkalap_admin();
                Új_Ablak_Munkalap_admin.FormClosed += Új_Ablak_Munkalap_admin_FormClosed;
                Új_Ablak_Munkalap_admin.Show();
            }
            else
            {
                Új_Ablak_Munkalap_admin.Activate();
                Új_Ablak_Munkalap_admin.WindowState = FormWindowState.Maximized;
            }
        }
        private void Új_Ablak_Munkalap_admin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Munkalap_admin = null;
        }



        Ablak_Munkalap_készítés Új_Ablak_Munkalap_készítés;
        private void MunkalapKészítésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Munkalap_készítés == null)
            {
                Új_Ablak_Munkalap_készítés = new Ablak_Munkalap_készítés();
                Új_Ablak_Munkalap_készítés.FormClosed += Új_Ablak_Munkalap_készítés_FormClosed;
                Új_Ablak_Munkalap_készítés.Show();
            }
            else
            {
                Új_Ablak_Munkalap_készítés.Activate();
                Új_Ablak_Munkalap_készítés.WindowState = FormWindowState.Maximized;
            }
        }
        private void Új_Ablak_Munkalap_készítés_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Munkalap_készítés = null;
        }


        Ablak_munkalap_dekádoló Új_Ablak_munkalap_dekádoló;
        private void MunkalapDekádolóToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_munkalap_dekádoló == null)
            {
                Új_Ablak_munkalap_dekádoló = new Ablak_munkalap_dekádoló();
                Új_Ablak_munkalap_dekádoló.FormClosed += Új_Ablak_munkalap_dekádoló_FormClosed;
                Új_Ablak_munkalap_dekádoló.Show();
            }
            else
            {
                Új_Ablak_munkalap_dekádoló.Activate();
                Új_Ablak_munkalap_dekádoló.WindowState = FormWindowState.Maximized;
            }
        }
        private void Új_Ablak_munkalap_dekádoló_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_munkalap_dekádoló = null;
        }



        Ablak_Karbantartási_Munkalapok Új_Ablak_Karbantartási_Munkalapok;
        private void KarbantartásiMunkalapokToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Karbantartási_Munkalapok == null)
            {
                Új_Ablak_Karbantartási_Munkalapok = new Ablak_Karbantartási_Munkalapok();
                Új_Ablak_Karbantartási_Munkalapok.FormClosed += Ablak_Karbantartási_Munkalapok_Closed;
                Új_Ablak_Karbantartási_Munkalapok.Show();
            }
            else
            {
                Új_Ablak_Karbantartási_Munkalapok.Activate();
                Új_Ablak_Karbantartási_Munkalapok.WindowState = FormWindowState.Maximized;
            }
        }

        private void Ablak_Karbantartási_Munkalapok_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Karbantartási_Munkalapok = null;
        }
        #endregion


        #region Nyilvántartások
        Ablak_Akkumulátor Új_Ablak_Akkumulátor;
        private void AkkumulátorNyilvántartásToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Akkumulátor == null)
            {
                Új_Ablak_Akkumulátor = new Ablak_Akkumulátor();
                Új_Ablak_Akkumulátor.FormClosed += Új_Ablak_Akkumulátor_Closed;
                Új_Ablak_Akkumulátor.Show();
            }
            else
            {
                Új_Ablak_Akkumulátor.Activate();
                Új_Ablak_Akkumulátor.WindowState = FormWindowState.Maximized;
            }
        }
        private void Új_Ablak_Akkumulátor_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Akkumulátor = null;
        }


        Ablak_keréknyilvántartás Új_Ablak_keréknyilvántartás;
        private void KerékátmérőNyilvántartásSAPBerendezésekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_keréknyilvántartás == null)
            {
                Új_Ablak_keréknyilvántartás = new Ablak_keréknyilvántartás();
                Új_Ablak_keréknyilvántartás.FormClosed += Új_Ablak_keréknyilvántartás_Closed;
                Új_Ablak_keréknyilvántartás.Show();
            }
            else
            {
                Új_Ablak_keréknyilvántartás.Activate();
                Új_Ablak_keréknyilvántartás.WindowState = FormWindowState.Maximized;
            }

        }
        private void Új_Ablak_keréknyilvántartás_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_keréknyilvántartás = null;
        }


        Ablak_sérülés Új_Ablak_sérülés;
        private void SérülésNyilvántartásToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_sérülés == null)
            {
                Új_Ablak_sérülés = new Ablak_sérülés();
                Új_Ablak_sérülés.FormClosed += Ablak_sérülés_Closed;
                Új_Ablak_sérülés.Show();
            }
            else
            {
                Új_Ablak_sérülés.Activate();
                Új_Ablak_sérülés.WindowState = FormWindowState.Maximized;
            }
        }
        private void Ablak_sérülés_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_sérülés = null;
        }


        Ablak_reklám Új_Ablak_reklám;
        private void ReklámNyilvántartásToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_reklám == null)
            {
                Új_Ablak_reklám = new Ablak_reklám();
                Új_Ablak_reklám.FormClosed += Ablak_reklám_Closed;
                Új_Ablak_reklám.Show();
            }
            else
            {
                Új_Ablak_reklám.Activate();
                Új_Ablak_reklám.WindowState = FormWindowState.Maximized;
            }
        }
        private void Ablak_reklám_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_reklám = null;
        }


        Ablak_SAP_osztály Új_Ablak_SAP_osztály;
        private void SAPOsztályToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_SAP_osztály == null)
            {
                Új_Ablak_SAP_osztály = new Ablak_SAP_osztály();
                Új_Ablak_SAP_osztály.FormClosed += Ablak_SAP_osztály_Closed;
                Új_Ablak_SAP_osztály.Show();
            }
            else
            {
                Új_Ablak_SAP_osztály.Activate();
                Új_Ablak_SAP_osztály.WindowState = FormWindowState.Maximized;
            }
        }
        private void Ablak_SAP_osztály_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_SAP_osztály = null;
        }


        Ablak_Jármű_takarítás_új Új_Ablak_Jármű_takarítás_új;
        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Jármű_takarítás_új == null)
            {
                Új_Ablak_Jármű_takarítás_új = new Ablak_Jármű_takarítás_új();
                Új_Ablak_Jármű_takarítás_új.FormClosed += Ablak_Jármű_takarítás_új_Closed;
                Új_Ablak_Jármű_takarítás_új.Show();
            }
            else
            {
                Új_Ablak_Jármű_takarítás_új.Activate();
                Új_Ablak_Jármű_takarítás_új.WindowState = FormWindowState.Maximized;
            }

        }
        private void Ablak_Jármű_takarítás_új_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Jármű_takarítás_új = null;
        }


        Ablak_MEO_kerék Új_Ablak_MEO_kerék;
        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_MEO_kerék == null)
            {
                Új_Ablak_MEO_kerék = new Ablak_MEO_kerék();
                Új_Ablak_MEO_kerék.FormClosed += Ablak_MEO_kerék_Closed;
                Új_Ablak_MEO_kerék.Show();
            }
            else
            {
                Új_Ablak_MEO_kerék.Activate();
                Új_Ablak_MEO_kerék.WindowState = FormWindowState.Maximized;
            }

        }
        private void Ablak_MEO_kerék_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_MEO_kerék = null;
        }


        Ablak_T5C5_fűtés új_Ablak_T5C5_fűtés;
        private void T5C5UtastérFűtésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (új_Ablak_T5C5_fűtés == null)
            {
                új_Ablak_T5C5_fűtés = new Ablak_T5C5_fűtés();
                új_Ablak_T5C5_fűtés.FormClosed += Ablak_T5C5_fűtés_Closed;
                új_Ablak_T5C5_fűtés.Show();
            }
            else
            {
                új_Ablak_T5C5_fűtés.Activate();
                új_Ablak_T5C5_fűtés.WindowState = FormWindowState.Maximized;
            }
        }
        private void Ablak_T5C5_fűtés_Closed(object sender, FormClosedEventArgs e)
        {
            új_Ablak_T5C5_fűtés = null;
        }

        Ablak_KerékEszterga_Ütemezés Új_Ablak_KerékEszterga_Ütemezés;
        private void KerékesztergálásSzervezésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_KerékEszterga_Ütemezés == null)
            {
                Új_Ablak_KerékEszterga_Ütemezés = new Ablak_KerékEszterga_Ütemezés();
                Új_Ablak_KerékEszterga_Ütemezés.FormClosed += Ablak_KerékEszterga_Ütemezés_Closed;
                Új_Ablak_KerékEszterga_Ütemezés.Show();
            }
            else
            {
                Új_Ablak_KerékEszterga_Ütemezés.Activate();
                Új_Ablak_KerékEszterga_Ütemezés.WindowState = FormWindowState.Maximized;
            }
        }

        private void Ablak_KerékEszterga_Ütemezés_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_KerékEszterga_Ütemezés = null;
        }


        Ablak_Eszterga_Adatok_Baross Új_Ablak_Eszterga_Adatok_Baross;

        private void KerékesztergálásiAdatokBarossToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Eszterga_Adatok_Baross == null)
            {
                Új_Ablak_Eszterga_Adatok_Baross = new Ablak_Eszterga_Adatok_Baross();
                Új_Ablak_Eszterga_Adatok_Baross.FormClosed += Ablak_Eszterga_Adatok_Baross_Closed;
                Új_Ablak_Eszterga_Adatok_Baross.Show();
            }
            else
            {
                Új_Ablak_Eszterga_Adatok_Baross.Activate();
                Új_Ablak_Eszterga_Adatok_Baross.WindowState = FormWindowState.Maximized;
            }
        }

        private void Ablak_Eszterga_Adatok_Baross_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Eszterga_Adatok_Baross = null;
        }


        Ablak_TTP Új_Ablak_TTP;
        private void TTPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_TTP == null)
            {
                Új_Ablak_TTP = new Ablak_TTP();
                Új_Ablak_TTP.FormClosed += Új_Ablak_TTP_FormClosed;
                Új_Ablak_TTP.Show();
            }
            else
            {
                Új_Ablak_TTP.Activate();
                Új_Ablak_TTP.WindowState = FormWindowState.Maximized;
            }
        }

        private void Új_Ablak_TTP_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_TTP = null;
        }

        Ablak_Eszterga_Karbantartás Új_Ablak_Eszterga_Karbantartás;
        private void EsztergaKarbantartásToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Eszterga_Karbantartás == null)
            {
                Új_Ablak_Eszterga_Karbantartás = new Ablak_Eszterga_Karbantartás();
                Új_Ablak_Eszterga_Karbantartás.FormClosed += Új_Ablak_Eszterga_Karbantartás_FormClosed;
                Új_Ablak_Eszterga_Karbantartás.Show();
            }
            else
            {
                Új_Ablak_Eszterga_Karbantartás.Activate();
                Új_Ablak_Eszterga_Karbantartás.WindowState = FormWindowState.Maximized;
            }
        }

        private void Új_Ablak_Eszterga_Karbantartás_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Eszterga_Karbantartás = null;
        }
        #endregion


        #region Karbantartás
        Ablak_Karbantartási_adatok Új_Ablak_Karbantartási_adatok;

        private void JárműKarbantartásiAdatokToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //   Ablak_Jármű_állapotok.Show();
            if (Új_Ablak_Karbantartási_adatok == null)
            {
                Új_Ablak_Karbantartási_adatok = new Ablak_Karbantartási_adatok();
                Új_Ablak_Karbantartási_adatok.FormClosed += Új_Ablak_Karbantartási_adatok_Closed;
                Új_Ablak_Karbantartási_adatok.Show();
            }
            else
            {
                Új_Ablak_Karbantartási_adatok.Activate();
                Új_Ablak_Karbantartási_adatok.WindowState = FormWindowState.Maximized;
            }
        }
        private void Új_Ablak_Karbantartási_adatok_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Karbantartási_adatok = null;
        }


        private void JárműKarbantartásiAdatokToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //   Ablak_Jármű_állapotok.Show();
        }


        Ablak_T5C5_Tulajdonság Új_Ablak_T5C5_Tulajdonság;
        private void T5C5AdatokMódosításaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_T5C5_Tulajdonság == null)
            {
                Új_Ablak_T5C5_Tulajdonság = new Ablak_T5C5_Tulajdonság();
                Új_Ablak_T5C5_Tulajdonság.FormClosed += Új_Ablak_T5C5_Tulajdonság_FormClosed;
                Új_Ablak_T5C5_Tulajdonság.Show();
            }
            else
            {
                Új_Ablak_T5C5_Tulajdonság.Activate();
                Új_Ablak_T5C5_Tulajdonság.WindowState = FormWindowState.Maximized;
            }
        }
        private void Új_Ablak_T5C5_Tulajdonság_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_T5C5_Tulajdonság = null;
        }


        Ablak_T5C5_futás Új_Ablak_T5C5_futás;
        private void T5C5FutásnapRögzítésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_T5C5_futás == null)
            {
                Új_Ablak_T5C5_futás = new Ablak_T5C5_futás();
                Új_Ablak_T5C5_futás.FormClosed += Új_Ablak_T5C5_futás_FormClosed;
                Új_Ablak_T5C5_futás.Show();
            }
            else
            {
                Új_Ablak_T5C5_futás.Activate();
                Új_Ablak_T5C5_futás.WindowState = FormWindowState.Maximized;
            }

        }
        private void Új_Ablak_T5C5_futás_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_T5C5_futás = null;
        }



        Ablak_T5C5_napütemezés Új_Ablak_T5C5_napütemezés;
        private void T5C5FutásnapÜtemezésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_T5C5_napütemezés == null)
            {
                Új_Ablak_T5C5_napütemezés = new Ablak_T5C5_napütemezés();
                Új_Ablak_T5C5_napütemezés.FormClosed += Új_Ablak_T5C5_napütemezés_FormClosed;
                Új_Ablak_T5C5_napütemezés.Show();
            }
            else
            {
                Új_Ablak_T5C5_napütemezés.Activate();
                Új_Ablak_T5C5_napütemezés.WindowState = FormWindowState.Maximized;
            }
        }
        private void Új_Ablak_T5C5_napütemezés_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_T5C5_napütemezés = null;
        }



        Ablak_T5C5_Vizsgálat_ütemező Új_Ablak_T5C5_Vizsgálat_ütemező;
        private void T5C5VJavításÜtemezésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_T5C5_Vizsgálat_ütemező == null)
            {
                Új_Ablak_T5C5_Vizsgálat_ütemező = new Ablak_T5C5_Vizsgálat_ütemező();
                Új_Ablak_T5C5_Vizsgálat_ütemező.FormClosed += Új_Ablak_T5C5_Vizsgálat_ütemező_FormClosed;
                Új_Ablak_T5C5_Vizsgálat_ütemező.Show();
            }
            else
            {
                Új_Ablak_T5C5_Vizsgálat_ütemező.Activate();
                Új_Ablak_T5C5_Vizsgálat_ütemező.WindowState = FormWindowState.Maximized;
            }
        }
        private void Új_Ablak_T5C5_Vizsgálat_ütemező_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_T5C5_Vizsgálat_ütemező = null;
        }



        Ablak_TW6000_Tulajdonság Új_Ablak_TW6000_Tulajdonság;
        private void TW6000AdatokToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_TW6000_Tulajdonság == null)
            {
                Új_Ablak_TW6000_Tulajdonság = new Ablak_TW6000_Tulajdonság();
                Új_Ablak_TW6000_Tulajdonság.FormClosed += Új_Ablak_TW6000_Tulajdonság_FormClosed;
                Új_Ablak_TW6000_Tulajdonság.Show();
            }
            else
            {
                Új_Ablak_TW6000_Tulajdonság.Activate();
                Új_Ablak_TW6000_Tulajdonság.WindowState = FormWindowState.Maximized;
            }
        }
        private void Új_Ablak_TW6000_Tulajdonság_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_TW6000_Tulajdonság = null;
        }


        Ablak_IcsKcsv Új_Ablak_IcsKcsv;
        private void ICSKCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_IcsKcsv == null)
            {
                Új_Ablak_IcsKcsv = new Ablak_IcsKcsv();
                Új_Ablak_IcsKcsv.FormClosed += Új_Ablak_IcsKcsv_FormClosed;
                Új_Ablak_IcsKcsv.Show();
            }
            else
            {
                Új_Ablak_IcsKcsv.Activate();
                Új_Ablak_IcsKcsv.WindowState = FormWindowState.Maximized;
            }
        }
        private void Új_Ablak_IcsKcsv_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_IcsKcsv = null;
        }


        Ablak_Fogaskerekű_Tulajdonságok Új_Ablak_Fogaskerekű_Tulajdonságok;
        private void FogaskerekűToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Fogaskerekű_Tulajdonságok == null)
            {
                Új_Ablak_Fogaskerekű_Tulajdonságok = new Ablak_Fogaskerekű_Tulajdonságok();
                Új_Ablak_Fogaskerekű_Tulajdonságok.FormClosed += Új_Ablak_Fogaskerekű_Tulajdonságok_FormClosed;
                Új_Ablak_Fogaskerekű_Tulajdonságok.Show();
            }
            else
            {
                Új_Ablak_Fogaskerekű_Tulajdonságok.Activate();
                Új_Ablak_Fogaskerekű_Tulajdonságok.WindowState = FormWindowState.Maximized;
            }
        }


        private void Új_Ablak_Fogaskerekű_Tulajdonságok_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Fogaskerekű_Tulajdonságok = null;
        }



        Ablak_Tulajdonságok_CAF Új_Ablak_Tulajdonságok_CAF;
        private void CAF5CAF9AdatokÉsÜtemezésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Tulajdonságok_CAF == null)
            {
                Új_Ablak_Tulajdonságok_CAF = new Ablak_Tulajdonságok_CAF();
                Új_Ablak_Tulajdonságok_CAF.FormClosed += Új_Ablak_Tulajdonságok_CAF_FormClosed;
                Új_Ablak_Tulajdonságok_CAF.Show();
            }
            else
            {
                Új_Ablak_Tulajdonságok_CAF.Activate();
                Új_Ablak_Tulajdonságok_CAF.WindowState = FormWindowState.Maximized;
            }
        }
        private void Új_Ablak_Tulajdonságok_CAF_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Tulajdonságok_CAF = null;
        }


        Ablak_Nosztalgia Új_Ablak_Nosztalgia;
        private void NosztalgiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Nosztalgia == null)
            {
                Új_Ablak_Nosztalgia = new Ablak_Nosztalgia();
                Új_Ablak_Nosztalgia.FormClosed += Ablak_Nosztalgia_FormClosed;
                Új_Ablak_Nosztalgia.Show();
            }
            else
            {
                Új_Ablak_Nosztalgia.Activate();
                Új_Ablak_Nosztalgia.WindowState = FormWindowState.Maximized;
            }
        }

        private void Ablak_Nosztalgia_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Nosztalgia = null;
        }

        #endregion


        #region Kiadási adatok

        Ablak_állomány Új_Ablak_állomány;
        private void ÁllományTáblaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_állomány == null)
            {
                Új_Ablak_állomány = new Ablak_állomány();
                Új_Ablak_állomány.FormClosed += Ablak_állomány_Closed;
                Új_Ablak_állomány.Show();
            }
            else
            {
                Új_Ablak_állomány.Activate();
                Új_Ablak_állomány.WindowState = FormWindowState.Maximized;
            }
        }

        private void Ablak_állomány_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_állomány = null;
        }

        Ablak_Jármű Új_Ablak_Jármű;
        private void JárműLétrehozásMozgásToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Jármű == null)
            {
                Új_Ablak_Jármű = new Ablak_Jármű();
                Új_Ablak_Jármű.FormClosed += Ablak_Jármű_Closed;
                Új_Ablak_Jármű.Show();
            }
            else
            {
                Új_Ablak_Jármű.Activate();
                Új_Ablak_Jármű.WindowState = FormWindowState.Maximized;
            }
        }
        private void Ablak_Jármű_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Jármű = null;
        }



        Ablak_Főkönyv Új_Ablak_Főkönyv;
        private void FőkönyvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Főkönyv == null)
            {
                Új_Ablak_Főkönyv = new Ablak_Főkönyv();
                Új_Ablak_Főkönyv.FormClosed += Ablak_Főkönyv_Closed;
                Új_Ablak_Főkönyv.Show();
            }
            else
            {
                Új_Ablak_Főkönyv.Activate();
                Új_Ablak_Főkönyv.WindowState = FormWindowState.Maximized;
            }
        }
        private void Ablak_Főkönyv_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Főkönyv = null;
        }



        Ablak_Napiadatok új_Ablak_Napiadatok;
        private void NapiAdatokToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (új_Ablak_Napiadatok == null)
            {
                új_Ablak_Napiadatok = new Ablak_Napiadatok();
                új_Ablak_Napiadatok.FormClosed += Ablak_Napiadatok_Closed;
                új_Ablak_Napiadatok.Show();
            }
            else
            {
                új_Ablak_Napiadatok.Activate();
                új_Ablak_Napiadatok.WindowState = FormWindowState.Maximized;
            }
        }

        private void Ablak_Napiadatok_Closed(object sender, FormClosedEventArgs e)
        {
            új_Ablak_Napiadatok = null;
        }



        public Ablak_kidobó Új_Ablak_kidobó;
        private void KidobóKészítésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_kidobó == null)
            {
                Új_Ablak_kidobó = new Ablak_kidobó();
                Új_Ablak_kidobó.FormClosed += Ablak_kidobó_Closed;
                Új_Ablak_kidobó.Show();
            }
            else
            {
                Új_Ablak_kidobó.Activate();
                Új_Ablak_kidobó.WindowState = FormWindowState.Maximized;
            }
        }

        private void Ablak_kidobó_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_kidobó = null;
        }



        AblakMenetkimaradás új_AblakMenetkimaradás;
        private void MenetkimaradásMenü_Click(object sender, EventArgs e)
        {
            if (új_AblakMenetkimaradás == null)
            {
                új_AblakMenetkimaradás = new AblakMenetkimaradás();
                új_AblakMenetkimaradás.FormClosed += AblakMenetkimaradás_Closed;
                új_AblakMenetkimaradás.Show();
            }
            else
            {
                új_AblakMenetkimaradás.Activate();
                új_AblakMenetkimaradás.WindowState = FormWindowState.Maximized;
            }
            // AblakMenetkimaradás.Show();
        }

        private void AblakMenetkimaradás_Closed(object sender, FormClosedEventArgs e)
        {
            új_AblakMenetkimaradás = null;
        }

        Ablak_Digitális_Főkönyv Új_Ablak_Digitális_Főkönyv;
        private void DigitálisFőkönyvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Digitális_Főkönyv == null)
            {
                Új_Ablak_Digitális_Főkönyv = new Ablak_Digitális_Főkönyv();
                Új_Ablak_Digitális_Főkönyv.FormClosed += Ablak_Digitális_Főkönyv_Closed;
                Új_Ablak_Digitális_Főkönyv.Show();
            }
            else
            {
                Új_Ablak_Digitális_Főkönyv.Activate();
                Új_Ablak_Digitális_Főkönyv.WindowState = FormWindowState.Maximized;
            }
        }

        private void Ablak_Digitális_Főkönyv_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Digitális_Főkönyv = null;
        }

        Ablak_szerelvény Új_Ablak_szerelvény;
        private void SzerelvényToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_szerelvény == null)
            {
                Új_Ablak_szerelvény = new Ablak_szerelvény();
                Új_Ablak_szerelvény.FormClosed += Ablak_szerelvény_Closed;
                Új_Ablak_szerelvény.Show();
            }
            else
            {
                Új_Ablak_szerelvény.Activate();
                Új_Ablak_szerelvény.WindowState = FormWindowState.Maximized;
            }
        }
        private void Ablak_szerelvény_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_szerelvény = null;
        }


        Ablak_Fő_Kiadás_Forte Új_Ablak_Fő_Kiadás_Forte;
        private void KiadásiForteAdatokToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Fő_Kiadás_Forte == null)
            {
                Új_Ablak_Fő_Kiadás_Forte = new Ablak_Fő_Kiadás_Forte();
                Új_Ablak_Fő_Kiadás_Forte.FormClosed += Ablak_Fő_Kiadás_Forte_Closed;
                Új_Ablak_Fő_Kiadás_Forte.Show();
            }
            else
            {
                Új_Ablak_Fő_Kiadás_Forte.Activate();
                Új_Ablak_Fő_Kiadás_Forte.WindowState = FormWindowState.Maximized;
            }
        }
        private void Ablak_Fő_Kiadás_Forte_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Fő_Kiadás_Forte = null;
        }


        Ablak_Fő_Napiadatok Új_Ablak_Fő_Napiadatok;
        private void TelephelyiAdatokÖsszesítéseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Fő_Napiadatok == null)
            {
                Új_Ablak_Fő_Napiadatok = new Ablak_Fő_Napiadatok();
                Új_Ablak_Fő_Napiadatok.FormClosed += Ablak_Fő_Napiadatok_Closed;
                Új_Ablak_Fő_Napiadatok.Show();
            }
            else
            {
                Új_Ablak_Fő_Napiadatok.Activate();
                Új_Ablak_Fő_Napiadatok.WindowState = FormWindowState.Maximized;
            }
        }
        private void Ablak_Fő_Napiadatok_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Fő_Napiadatok = null;
        }


        Ablak_Fő_Egyesített Új_Ablak_Fő_Egyesített;
        private void FőmérnökségiAdatokToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Fő_Egyesített == null)
            {
                Új_Ablak_Fő_Egyesített = new Ablak_Fő_Egyesített();
                Új_Ablak_Fő_Egyesített.FormClosed += Ablak_Fő_Egyesített_Closed;
                Új_Ablak_Fő_Egyesített.Show();
            }
            else
            {
                Új_Ablak_Fő_Egyesített.Activate();
                Új_Ablak_Fő_Egyesített.WindowState = FormWindowState.Maximized;
            }
        }
        private void Ablak_Fő_Egyesített_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Fő_Egyesített = null;
        }
        #endregion


        #region Gondnokság


        Ablak_Behajtási Új_Ablak_Behajtási;
        private void BehajtásiEngedélyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Behajtási == null)
            {
                Új_Ablak_Behajtási = new Ablak_Behajtási();
                Új_Ablak_Behajtási.FormClosed += Új_Ablak_Behajtási_FormClosed;
                Új_Ablak_Behajtási.Show();

            }
            else
            {
                Új_Ablak_Behajtási.Activate();
                Új_Ablak_Behajtási.WindowState = FormWindowState.Maximized;
            }
        }
        private void Új_Ablak_Behajtási_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Behajtási = null;
        }



        Ablak_külső Új_Ablak_külső;
        private void KülsősDolgozókBelépésiÉsBehajtásaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_külső == null)
            {
                Új_Ablak_külső = new Ablak_külső();
                Új_Ablak_külső.FormClosed += Új_Ablak_külső_FormClosed;
                Új_Ablak_külső.Show();
            }
            else
            {
                Új_Ablak_külső.Activate();
                Új_Ablak_külső.WindowState = FormWindowState.Maximized;
            }
        }
        private void Új_Ablak_külső_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_külső = null;
        }



        Ablak_épülettakarítás_alap Új_Ablak_épülettakarítás_alap;
        private void ÉpületTakarításTörzsAdatokToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_épülettakarítás_alap == null)
            {
                Új_Ablak_épülettakarítás_alap = new Ablak_épülettakarítás_alap();
                Új_Ablak_épülettakarítás_alap.FormClosed += Ablak_épülettakarítás_alap_Closed;
                Új_Ablak_épülettakarítás_alap.Show();
            }
            else
            {
                Új_Ablak_épülettakarítás_alap.Activate();
                Új_Ablak_épülettakarítás_alap.WindowState = FormWindowState.Maximized;
            }
        }

        private void Ablak_épülettakarítás_alap_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_épülettakarítás_alap = null;
        }


        Ablak_Épülettakarítás Új_Ablak_Épülettakarítás;
        private void ÉpületTakarításToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Épülettakarítás == null)
            {
                Új_Ablak_Épülettakarítás = new Ablak_Épülettakarítás();
                Új_Ablak_Épülettakarítás.FormClosed += Új_Ablak_Épülettakarítás_FormClosed;
                Új_Ablak_Épülettakarítás.Show();
            }
            else
            {
                Új_Ablak_Épülettakarítás.Activate();
                Új_Ablak_Épülettakarítás.WindowState = FormWindowState.Maximized;
            }
        }
        private void Új_Ablak_Épülettakarítás_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Épülettakarítás = null;
        }


        Ablak_védő Új_Ablak_védő;
        private void VédőeszközToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_védő == null)
            {
                Új_Ablak_védő = new Ablak_védő();
                Új_Ablak_védő.FormClosed += Új_Ablak_védő_FormClosed;
                Új_Ablak_védő.Show();
            }
            else
            {
                Új_Ablak_védő.Activate();
                Új_Ablak_védő.WindowState = FormWindowState.Maximized;
            }

        }
        private void Új_Ablak_védő_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_védő = null;
        }


        Ablak_Eszköz Új_Ablak_Eszköz;
        private void EszközNyilvántartásToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Eszköz == null)
            {
                Új_Ablak_Eszköz = new Ablak_Eszköz();
                Új_Ablak_Eszköz.FormClosed += Új_Ablak_Eszköz_FormClosed;
                Új_Ablak_Eszköz.Show();
            }
            else
            {
                Új_Ablak_Eszköz.Activate();
                Új_Ablak_Eszköz.WindowState = FormWindowState.Maximized;
            }
        }

        private void Új_Ablak_Eszköz_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Eszköz = null;
        }


        Ablak_Szerszám Új_Ablak_Szerszám_ép;
        private void ÉpületTartozékNyilvántartásToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Szerszám_ép == null)
            {
                Új_Ablak_Szerszám_ép = new Ablak_Szerszám();
                Új_Ablak_Szerszám_ép.FormClosed += Ablak_Szerszám_ép_FormClosed;
                MyEvent += Új_Ablak_Szerszám_ép.SetData;
                MyEvent("Helység");
                Új_Ablak_Szerszám_ép.Show();
            }
            else
            {
                Új_Ablak_Szerszám_ép.Activate();
                Új_Ablak_Szerszám_ép.WindowState = FormWindowState.Maximized;
            }
        }

        private void Ablak_Szerszám_ép_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Szerszám_ép = null;
        }


        Ablak_Szerszám Új_Ablak_Szerszám;
        private void SzerszámNyilvántartásToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Szerszám == null)
            {
                Új_Ablak_Szerszám = new Ablak_Szerszám();
                Új_Ablak_Szerszám.FormClosed += Ablak_Szerszám_FormClosed;
                MyEvent += Új_Ablak_Szerszám.SetData;
                MyEvent("Szerszám");
                Új_Ablak_Szerszám.Show();
            }
            else
            {
                Új_Ablak_Szerszám.Activate();
                Új_Ablak_Szerszám.WindowState = FormWindowState.Maximized;
            }
        }

        private void Ablak_Szerszám_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Szerszám = null;
        }



        Ablak_Rezsi Új_Ablak_Rezsi;
        private void RezsiRaktárToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Rezsi == null)
            {
                Új_Ablak_Rezsi = new Ablak_Rezsi();
                Új_Ablak_Rezsi.FormClosed += Új_Ablak_Rezsi_FormClosed;
                Új_Ablak_Rezsi.Show();
            }
            else
            {
                Új_Ablak_Rezsi.Activate();
                Új_Ablak_Rezsi.WindowState = FormWindowState.Maximized;
            }
        }
        private void Új_Ablak_Rezsi_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Rezsi = null;
        }

        #endregion


        #region Kilépés
        private void KilépésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kilépés();
        }

        private void A_Főoldal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Kilépés();
        }

        private void Kilépés()
        {
            Application.Exit();
        }
        #endregion


        #region RejtettPanel
        private void LblVerzió_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Shift_le && CTRL_le)
            {
                if (panels2.Text.Substring(0, 1) == "b")
                {
                    Telephelyekfeltöltése();
                    Alsó.Left = 200;
                    Alsó.Top = 100;
                    Hardvergomb();
                    Alsó.Visible = true;
                    Rejtett.Visible = true;
                    Alsó.Height = 395;
                }
            }
        }

        private void Label6_MouseMove(object sender, MouseEventArgs e)
        {
            // egér bal gomb hatására a groupbox1 bal felső sarkánál fogva mozgatja a lapot.
            if (e.Button == MouseButtons.Left)
            {
                Alsó.Top = Top + Alsó.Top + e.Y;
                Alsó.Left = Left + Alsó.Left + e.X;
            }
        }

        private void Command9_Click(object sender, EventArgs e)
        {
            Alsó.Visible = false;
        }

        private void Label8_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Cmbtelephely.Visible = Alt_le;
        }

        private void Cmbtelephely_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbltelephely.Text = Cmbtelephely.Text;
            panels4.Text = Cmbtelephely.Text;
            Program_változó(Cmbtelephely.Text.Trim());
        }

        private void LblÓra_MouseClick(object sender, MouseEventArgs e)
        {
            if (CTRL_le)
            {
                Rejtett.Visible = false;
                Alsó.Height = 100;
                Alsó.Left = 200;
                Alsó.Top = 100;
                Hardvergomb();
                Alsó.Visible = true;
            }
        }

        private void Program_változó(string telephely)
        {
            Program.PostásTelephely = telephely.Trim();
            TelephelyListaFeltöltés();

            Adat_Kiegészítő_Könyvtár Adat = (from a in AdatokKönyvtár
                                             where a.Név == Program.PostásTelephely
                                             select a).FirstOrDefault();
            if (Adat != null)
            {
                Program.Postás_Vezér = Adat.Vezér1;
                Program.Postás_csoport = Adat.Csoport1;
            }
        }

        private void Hardvergomb()
        {
            BtnHardverkulcs.Visible = false;
            string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Villamos9.mdb";
            if (!Exists(hely)) return;

            string keres = MyF.MÁSRövidkód(Panels1.Text.Trim());
            Kezelő_Kulcs_Fekete Kézkulcs = new Kezelő_Kulcs_Fekete();
            List<Adat_Kulcs> Adatok = Kézkulcs.Lista_Adatok();
            Adat_Kulcs Elem = (from a in Adatok
                               where a.Adat1.Contains(keres.Substring(0, keres.Length - 3))
                               select a).FirstOrDefault();
            if (Elem != null) BtnHardverkulcs.Visible = true;
        }
        #endregion


        #region Verzió kezelés
        private void VerzióListaFeltöltés()
        {
            try
            {
                AdatokVerzó.Clear();
                AdatokVerzó = Kéz_Belépés_Verzió.Lista_Adatok();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Verziószám_kiírás()
        {
            VerzióListaFeltöltés();
            Adat_Belépés_Verzió Elem = (from a in AdatokVerzó
                                        where a.Id == 2
                                        select a).FirstOrDefault();
            if (Elem != null) TároltVerzió.Text = Elem.Verzió.ToString();
        }

        private void Verzió_Váltás_Click(object sender, EventArgs e)
        {
            // frissítjük a verziót
            Adat_Belépés_Verzió Elem = (from a in AdatokVerzó
                                        where a.Id == 2
                                        select a).FirstOrDefault();
            double verzió = double.Parse(Application.ProductVersion.Replace(".", ""));
            Adat_Belépés_Verzió ADAT = new Adat_Belépés_Verzió(2, verzió);
            if (Elem != null)
                Kéz_Belépés_Verzió.Módosítás(ADAT);
            else
                Kéz_Belépés_Verzió.Rögzítés(ADAT);
            Verziószám_kiírás();
            MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Label5_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Verzió_Váltás.Visible = Shift_le;
        }
        #endregion


    }
}