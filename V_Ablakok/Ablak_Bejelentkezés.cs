using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using static System.IO.File;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    public partial class AblakBejelentkezés
    {
        readonly Kezelő_Belépés_Jogosultságtábla Kéz_Jogosultság = new Kezelő_Belépés_Jogosultságtábla();
        readonly Kezelő_Belépés_Bejelentkezés Kéz_Bejelentkezés = new Kezelő_Belépés_Bejelentkezés();
        readonly Kezelő_Kiegészítő_Könyvtár KézKönyvtár = new Kezelő_Kiegészítő_Könyvtár();
        readonly Kezelő_Belépés_Verzió KézVerzió = new Kezelő_Belépés_Verzió();
        readonly Kezelő_Belépés_WinTábla KézWin = new Kezelő_Belépés_WinTábla();

        List<Adat_Belépés_Jogosultságtábla> AdatokJogosultságTelephely = new List<Adat_Belépés_Jogosultságtábla>();
        List<Adat_Belépés_Bejelentkezés> AdatokBelépésTelephely = new List<Adat_Belépés_Bejelentkezés>();

        bool Beléphet = true;
        public AblakBejelentkezés()
        {
            InitializeComponent();
            Start();
        }


        private void AblakBejelentkezés_Load(object sender, EventArgs e)
        {

        }

        private void AblakBejelentkezés_Shown(object sender, EventArgs e)
        {
            if (Beléphet) WinVan();
        }

        #region Alap
        private void Súgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\Főoldal.html";
                MyF.Megnyitás(hely);
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


        #region Indulás

        private void Start()
        {
            lblVerzió.Text = "Verzió: " + Application.ProductVersion;
            lblProgramnév.Text = Application.ProductName;
            Timer_kilép.Enabled = false;
            Hálózat();
            Dátumformátumellenőrzés();
            Karbantartásellenőrzés();
            Subtelephelyfeltöltés();
        }

        private void Hálózat()
        {
            if (Application.StartupPath.Substring(0, 2) == @"\\")
            {
                FigyKiírás("A programot csak hálózati meghajtón keresztül lehet elindítani. \n Kérem csatlakoztasson hálózati meghajtót.");
                KönyvtárEllenőr KV = new KönyvtárEllenőr();
                KV.Megfelelő(Application.StartupPath);
                Beléphet = false;
            }
        }



        private void Dátumformátumellenőrzés()
        {
            try
            {
                string formátum = @"^([12]\d{3}.(0[1-9]|1[0-2]).(0[1-9]|[12]\d|3[01]). ([0-2][0-9]|[0-9]):([0-5][0-9]):([0-5][0-9]))$";

                if (!Regex.IsMatch(DateTime.Now.ToString(), formátum))
                {
                    // Ha rossz a dátum formátum, akkor átállítjuk
                    string dateFormat = "yyyy.MM.dd.";
                    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
                    key.SetValue("sShortDate", dateFormat);

                    //Ha kijavítottuk, akkor és tájékozatunk
                    string szöveg = "A számítógépen az idő formátum nem volt jól beállíva, ezért a program átírta a rendszer időformátumát. \n A helyes működéshez a program újraindul.";
                    MessageBox.Show(szöveg, "Számítógép beállítási Hiba javítva lett", MessageBoxButtons.OK);
                    Application.Restart();

                    if (!Regex.IsMatch(DateTime.Now.ToString(), formátum))
                    {
                        //Ha továbbra is rossz a dátum formátum akkor kilépünk üzenettel
                        szöveg = "A számítógépen az idő formátum nincs jól beállíva, ezért a program leáll.\r\n";
                        szöveg += " A helyes formátum: éééé.HH.nn. nincs szóköz a pont után.\r\n";
                        szöveg += " Beállítani a területi/ régió beállításoknál kell.";
                        FigyKiírás(szöveg);
                        //     HibaNapló.Log("Hibás  dátum forma.", $"Dátum: {DateTime.Now}\n Felhasználó : {Environment.UserName}", "Dátumformátumellenőrzés", "", 0);
                        Timer_kilép.Enabled = true;

                        Application.Exit();
                    }
                    else
                    {
                        //Ha kijavítottuk, akkor és tájékozatunk
                        szöveg = "A számítógépen az idő formátum nem volt jól beállíva, ezért a program átírta a rendszer időformátumát.";
                        MessageBox.Show(szöveg, "Számítógép beállítási Hiba javítva lett", MessageBoxButtons.OK);
                    }
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

        private void Karbantartásellenőrzés()
        {
            // ha karbantartás van nem engedünk belépni
            string hely = $@"{Application.StartupPath}\Főmérnökség\Szerszám\a.txt";
            if (Exists(hely))
            {
                FigyKiírás("\nJelenleg az adatok karbantartása folyik. \r\n\n Kis türelmet kérek ....");
                Beléphet = false;
            }
            else
                Verzióellenőrzés();
        }

        private void Subtelephelyfeltöltés()
        {
            try
            {
                CmbTelephely.Items.Clear();
                List<Adat_Kiegészítő_Könyvtár> Adatok = KézKönyvtár.Lista_Adatok().OrderBy(a => a.Név).ToList();
                foreach (Adat_Kiegészítő_Könyvtár rekord in Adatok)
                    CmbTelephely.Items.Add(rekord.Név);
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

        private void WinVan()
        {
            try
            {
                List<Adat_Belépés_WinTábla> Adatok = KézWin.Lista_Adatok();

                if (Adatok != null)
                {
                    Adat_Belépés_WinTábla Elem = (from a in Adatok
                                                  where a.WinUser.ToUpper() == Environment.UserName.ToUpper()
                                                  select a).FirstOrDefault();

                    //Ha van ilyen dolgozó, akkor beléptetjük
                    if (Elem != null)
                    {
                        AdatokBelépésTelephely = Kéz_Bejelentkezés.Lista_Adatok(Elem.Telephely);
                        Adat_Belépés_Bejelentkezés Kiaz = (from a in AdatokBelépésTelephely
                                                           where a.Név.ToUpper() == Elem.Név.ToUpper()
                                                           select a).FirstOrDefault();

                        Belépés(Elem.Telephely, Elem.Név.ToUpper(), Kiaz.Jelszó.ToUpper());
                    }
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

        private void Verzióellenőrzés()
        {
            try
            {
                List<Adat_Belépés_Verzió> Adatok = KézVerzió.Lista_Adatok();

                Adat_Belépés_Verzió Elem = (from a in Adatok
                                            where a.Id == 2
                                            select a).FirstOrDefault();
                if (Elem != null)
                {
                    if (Application.ProductVersion.Replace(".", "").ToÉrt_Double() < Elem.Verzió)
                    {
                        FigyKiírás("Elavult a program verzió!\n\n Új parancsikont kell készíteni.\n\n Ha szükséges a számítógépet újra kell indítani.");
                        Beléphet = false;
                    }
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

        private void FigyKiírás(string szöveg)
        {
            GroupBox1.Visible = false;
            Timer_kilép.Enabled = true;

            Label Figyelmeztetés = new Label
            {
                Left = 5,
                Top = 5,
                Width = 555,
                Height = 275,
                Text = szöveg,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Tomato,
                Font = new System.Drawing.Font("Arial", 20, FontStyle.Bold),
                Visible = true
            };
            this.Controls.Add(Figyelmeztetés);

            Timer_kilép.Enabled = true;
            Application.Exit();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            // kilépünk
            Application.Exit();
        }
        #endregion


        #region Beléptetés
        private void Btnlekérdezés_Click(object sender, EventArgs e)
        {
            CmbUserName.Text = "VENDÉG";
            Jogosultság_Belépés(CmbTelephely.Text.Trim(), CmbUserName.Text.Trim());
        }

        public void BtnBelépés_Click(object sender, EventArgs e)
        {
            Belépés(CmbTelephely.Text.Trim(), CmbUserName.Text.Trim(), TxtPassword.Text.Trim().ToUpper());
        }

        private void Belépés(string Telephely, string UserName, string Begépeltjelszó)
        {
            try
            {
                if (Begépeltjelszó.Trim().ToUpper() == "INIT") throw new HibásBevittAdat("A jelszó még nem lett módosítva az alapbeállításról, a jelszó módosítani szükséges.");

                Adat_Belépés_Bejelentkezés Elem = (from a in AdatokBelépésTelephely
                                                   where a.Név.ToUpper() == UserName.ToUpper()
                                                   select a).FirstOrDefault();
                if (Elem != null)
                {
                    if (Begépeltjelszó.Trim().ToUpper() == Elem.Jelszó.Trim().ToUpper())
                        Jogosultság_Belépés(Telephely, UserName);
                    else
                    {
                        MessageBox.Show("Hibás jelszó!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        TxtPassword.Text = "";
                        TxtPassword.Focus();
                    }
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

        private void Jogosultság_Belépés(string Telephely, string Név)
        {
            try
            {
                AdatokJogosultságTelephely = Kéz_Jogosultság.Lista_Adatok(Telephely);
                Adat_Belépés_Jogosultságtábla rekord = (from a in AdatokJogosultságTelephely
                                                        where a.Név.ToUpper() == Név.ToUpper()
                                                        select a).FirstOrDefault();
                if (rekord != null)
                {
                    Program.PostásJogkör = rekord.Jogkörúj1.Trim();
                    Program.PostásTelephely = Telephely;
                    Program.PostásNév = Név.ToUpper();
                    A_Főoldal Főoldalablak = new A_Főoldal();
                    Főoldalablak.Show();
                    this.Hide();
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

        private void BtnMégse_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CmbUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbUserName.Text.Trim() != "")
            {
                TxtPassword.Text = "";
                TxtPassword.Focus();
            }
        }
        #endregion


        #region Telephelyfeltöltés
        private void CmbTelephely_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbTelephely.Text.Trim() == "")
            {
                Btnlekérdezés.Visible = false;
                GroupBox2.Visible = false;
            }
            else
            {
                Btnlekérdezés.Visible = true;
                GroupBox2.Visible = true;
                CmbUserName.Focus();
                TxtPassword.Text = "";
                Subdolgozófeltöltés();
            }
        }

        private void Subdolgozófeltöltés()
        {
            AdatokJogosultságTelephely = Kéz_Jogosultság.Lista_Adatok(CmbTelephely.Text.Trim());
            AdatokBelépésTelephely = Kéz_Bejelentkezés.Lista_Adatok(CmbTelephely.Text.Trim());
            CmbUserName.Items.Clear();

            foreach (Adat_Belépés_Bejelentkezés Elem in AdatokBelépésTelephely)
                CmbUserName.Items.Add(Elem.Név.ToUpper());

            CmbUserName.Refresh();
        }
        #endregion


        #region Jelszó Módosítás
        private void BtnJelszóMódosítás_Click(object sender, EventArgs e)
        {
            if (CmbUserName.Text.Trim() == "") return;
            TxtPassword.Text = "";
            Subjelszómódosítás();
            AdatokBelépésTelephely = Kéz_Bejelentkezés.Lista_Adatok(CmbTelephely.Text.Trim());
        }


        private void Subjelszómódosítás()
        {
            AblakJelszóváltoztatás jelszó_váltás = new AblakJelszóváltoztatás(CmbTelephely.Text.Trim(), CmbUserName.Text.Trim());
            jelszó_váltás.ShowDialog();
            TxtPassword.Text = "";
            TxtPassword.Focus();
        }
        #endregion

        //Conttoll mellett az új bejelentekezési ablakot nyitja meg
        bool CTRL_le = false;
        private void LblVerzió_DoubleClick(object sender, EventArgs e)
        {
            AblakBejelentkezés_Új Újablak = new AblakBejelentkezés_Új();
            Újablak.Show();
            this.Hide();
        }

        private void AblakBejelentkezés_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control) CTRL_le = true;
        }

        private void AblakBejelentkezés_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control) CTRL_le = false;
        }
    }
}