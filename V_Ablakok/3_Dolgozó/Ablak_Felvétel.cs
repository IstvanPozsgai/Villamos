using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{

    public partial class Ablak_Felvétel
    {
        public Ablak_Felvétel()
        {
            InitializeComponent();
        }

        readonly Kezelő_Dolgozó_Státus KézStátus = new Kezelő_Dolgozó_Státus();
        readonly Kezelő_Dolgozó_Alap KézDolgozó = new Kezelő_Dolgozó_Alap();
        readonly Kezelő_Kiegészítő_Könyvtár KézKönyvtár = new Kezelő_Kiegészítő_Könyvtár();
        readonly Kezelő_Kulcs_Kettő KézKulcs = new Kezelő_Kulcs_Kettő();

        List<Adat_Dolgozó_Alap> AdatokDolgozó = new List<Adat_Dolgozó_Alap>();
        List<Adat_Dolgozó_Státus> AdatokStátus = new List<Adat_Dolgozó_Státus>();
        List<Adat_Kiegészítő_Könyvtár> AdatokKönyvtár = new List<Adat_Kiegészítő_Könyvtár>();
        List<Adat_Kulcs> AdatokKulcs = new List<Adat_Kulcs>();


        private void AblakFelvétel_Load(object sender, EventArgs e)
        {
            Telephelyekfeltöltése();

            Jogosultságkiosztás();

            Fülekkitöltése();

            Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;
        }


        #region Alap
        private void Telephelyekfeltöltése()
        {
            try
            {
                // Adatbázis megnyitásának előkészítése
                List<Adat_Kiegészítő_Könyvtár> Adatok = KézKönyvtár.Lista_Adatok();

                // COMBO amibe az adatokat feltöltjük
                Cmbtelephely.Items.Clear();
                Cmbtelephely.Enabled = false;
                if (Program.PostásTelephely == "Főmérnökség")
                {
                    if (Adatok != null)
                    {
                        Adatok = (from a in Adatok
                                  where a.Név != "Főmérnökség"
                                  orderby a.Név
                                  select a).ToList();
                    }
                    Cmbtelephely.Enabled = true;
                    foreach (Adat_Kiegészítő_Könyvtár rekord in Adatok)
                    {
                        Cmbtelephely.Items.Add(rekord.Név.Trim());
                    }
                    Cmbtelephely.Text = Cmbtelephely.Items[0].ToString();
                }
                else
                {
                    //ha szakszolgálat, akkor csak a hozzá tartozó üzemeket tudja módosítani.
                    if (Program.Postás_Vezér)
                    {
                        if (Adatok != null)
                        {
                            Adatok = (from a in Adatok
                                      where a.Csoport1 == Program.Postás_csoport
                                      orderby a.Név
                                      select a).ToList();
                        }
                        foreach (Adat_Kiegészítő_Könyvtár rekord in Adatok)
                            Cmbtelephely.Items.Add(rekord.Név.Trim());

                        Cmbtelephely.Text = Program.PostásTelephely;
                    }
                    else
                    {
                        // kiírjuk, hogy honnan lépett be nem főmérnökség és nem szakszolgálat
                        Cmbtelephely.Items.Add(Program.PostásTelephely.Trim());
                        Cmbtelephely.Text = Program.PostásTelephely.Trim();
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

        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;

                // ide kell az összes gombot tenni amit szabályozni akarunk false
                BtnÚj.Visible = false;
                BtnÚj.Enabled = false;
                Command7.Visible = false;
                Command7.Enabled = false;
                Csoportmódosítás.Visible = false;
                Csoportmódosítás.Enabled = false;
                Command2.Enabled = false;
                Command2.Visible = false;
                Button1.Visible = false;
                Button1.Enabled = false;
                Command4.Enabled = false;

                melyikelem = 69;
                // módosítás 1 Dolgozók ki és beléptetése
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    BtnÚj.Enabled = true;
                    Command7.Enabled = true;
                    Csoportmódosítás.Enabled = true;
                }
                // módosítás 2 Állományba vétel
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    Command2.Enabled = true;
                    Button1.Enabled = true;
                }
                // módosítás 3 Vezénylés
                if (MyF.Vanjoga(melyikelem, 3))
                {
                    Command4.Enabled = true;
                }

                if (Program.Postás_Vezér)
                {
                    // törzs jogosultság
                    BtnÚj.Visible = true;
                    Command7.Visible = true;
                    Csoportmódosítás.Visible = true;
                }
                else
                {
                    // telephelyi jogosultság
                    Command2.Visible = true;
                    Button1.Visible = true;
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

        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Dolgozófelvétel.html";
                MyE.Megnyitás(hely);
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

        private void Fülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }

        private void Fülekkitöltése()
        {
            try
            {
                switch (Fülek.SelectedIndex)
                {
                    case 0:
                        {
                            // Felvétel
                            Kiürít();
                            break;
                        }

                    case 1:
                        {
                            // Dolgozó kiléptetése
                            Kiürítkilép();
                            TelephelyfeltöltésKilép();
                            KilépNévfeltöltés();
                            break;
                        }
                    case 2:
                        {
                            // dolgozó állományba vétel
                            BAkitöltés();
                            Névfeltöltésba();
                            Dolgozószámba.Text = "";
                            Dolgozóba.Text = "";
                            break;
                        }

                    case 3:
                        {
                            // dolgozó kirakás állományból
                            KItöltés();
                            NévfeltöltésKi();
                            DolgozószámKi.Text = "";
                            DolgozóKi.Text = "";
                            break;
                        }
                    case 4:
                        {
                            // vezénylés készítés
                            Vezénylésfeltöltés();
                            break;
                        }
                    case 5:
                        {
                            // vezényelt dolgozók törlése
                            Vezényeltdolgozók();
                            break;
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

        private void TelephelyfeltöltésKilép()
        {
            try
            {
                List<Adat_Kiegészítő_Könyvtár> Adatok = KézKönyvtár.Lista_Adatok();
                // COMBO amibe az adatokat feltöltjük
                KilépTelephely.Items.Clear();

                //ha szakszolgálat, akkor csak a hozzá tartozó üzemeket tudja módosítani.
                if (Program.Postás_Vezér)
                {
                    KilépTelephely.Enabled = true;
                    if (Adatok != null)
                    {
                        Adatok = (from a in Adatok
                                  where a.Csoport1 == Program.Postás_csoport
                                  orderby a.Név
                                  select a).ToList();
                    }
                    KilépTelephely.Enabled = true;
                    foreach (Adat_Kiegészítő_Könyvtár rekord in Adatok)
                    {
                        KilépTelephely.Items.Add(rekord.Név.Trim());
                    }
                    KilépTelephely.Text = Program.PostásTelephely.Trim();
                }
                else
                {
                    // kiírjuk, hogy honnan lépett be nem főmérnökség és nem szakszolgálat
                    KilépTelephely.Text = Program.PostásTelephely.Trim();
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

        private void Fülek_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Határozza meg, hogy melyik lap van jelenleg kiválasztva
            TabPage SelectedTab = Fülek.TabPages[e.Index];

            // Szerezze be a lap fejlécének területét
            Rectangle HeaderRect = Fülek.GetTabRect(e.Index);

            // Hozzon létreecsetet a szöveg megfestéséhez
            SolidBrush BlackTextBrush = new SolidBrush(Color.Black);

            // Állítsa be a szöveg igazítását
            StringFormat sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            // Festse meg a szöveget a megfelelő félkövér és szín beállítással
            if ((e.State & DrawItemState.Selected) != 0)
            {
                Font BoldFont = new Font(Fülek.Font.Name, Fülek.Font.Size, FontStyle.Bold);
                // háttér szín beállítása
                e.Graphics.FillRectangle(new SolidBrush(Color.DarkGray), e.Bounds);
                Rectangle paddedBounds = e.Bounds;
                paddedBounds.Inflate(0, 0);
                e.Graphics.DrawString(SelectedTab.Text, BoldFont, BlackTextBrush, paddedBounds, sf);
            }
            else
            {
                e.Graphics.DrawString(SelectedTab.Text, e.Font, BlackTextBrush, HeaderRect, sf);
            }
            // Munka kész – dobja ki a keféket
            BlackTextBrush.Dispose();
        }
        #endregion


        #region Új dolgozó
        private void Kiürít()
        {
            Dolgozónévúj.Text = "";
            Dolgozószámúj.Text = "";
            Belépésibér.Text = "";
            Belépésiidő.Value = DateTime.Now;
            Státusid.Text = "";
        }

        private void BtnÚj_Click(object sender, EventArgs e)
        {
            try
            {
                if (Dolgozószámúj.Text.Trim() == "") throw new HibásBevittAdat("A HR azonosítót meg kell adni.");
                if (Dolgozónévúj.Text.Trim() == "") throw new HibásBevittAdat("A dolgozó nevét meg kell adni.");
                long StátusID = 0;
                if (Státusid.Text.Trim() == "") throw new HibásBevittAdat("A státusz sorszámát meg kell adni.");
                else if (Státusid.Text.Trim() == "n")
                    StátusID = 0;
                else if (!long.TryParse(Státusid.Text, out StátusID)) throw new HibásBevittAdat("A státusz sorszámának számnak kell lennie.");

                if (!double.TryParse(Belépésibér.Text.Replace(".", ","), out double BelépésiBér))
                    Belépésibér.Text = 0.ToString();
                else
                    Belépésibér.Text = BelépésiBér.ToString();


                // Ellenőrzések

                string eredmény = "";
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Segéd\Státus.mdb";
                AdatokStátusListázás();

                Adat_Dolgozó_Státus AdatStátus = (from a in AdatokStátus
                                                  where a.ID == StátusID
                                                  select a).FirstOrDefault();

                // Státus tábla rögzítése
                if (Státusid.Text.Trim() != "n") // ha nincs státus vezetve akkor fel tudja tölteni a dolgozókat
                {
                    // ha nincs  fájl, akkor másolunk egy újat.
                    if (!File.Exists(hely)) Adatbázis_Létrehozás.Dolgozói_Státus(hely);
                    // ha van id szám 
                    if (AdatStátus != null)
                    {
                        eredmény = AdatStátus.Hrazonosítóbe;
                        if (eredmény.Trim() != "_")
                        {
                            if (Dolgozószámúj.Text.Trim() != eredmény.Trim()) throw new HibásBevittAdat("Ebbe a pozícióba van már felvéve!");
                        }
                        eredmény = AdatStátus.Státusváltozások;

                        if (eredmény.Trim().ToUpper() == ("Státus megszüntetése").ToUpper()) throw new HibásBevittAdat("Ebbe a pozícióba nem lehet felvenni, mert a pozíció megszűnt!");
                    }
                    else
                    {
                        // nincs ilyen sorszám
                        throw new HibásBevittAdat("Nincs ilyen sorszámú státus!");
                    }
                }

                hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Dolgozók.mdb";
                AdatokDolgozóListázás(hely);
                Adat_Dolgozó_Alap AdatDolgozó = (from a in AdatokDolgozó
                                                 where a.Kilépésiidő == new DateTime(1900, 1, 1)
                                                 && a.Dolgozószám == Dolgozószámúj.Text.Trim()
                                                 select a).FirstOrDefault();

                if (AdatDolgozó != null) throw new HibásBevittAdat("Van már ilyen azonosítószámú dolgozó!");

                // rögzítjük az új dolgozót
                AdatDolgozó = (from a in AdatokDolgozó
                               where a.Dolgozószám == Dolgozószámúj.Text.Trim()
                               select a).FirstOrDefault();

                if (AdatDolgozó == null)
                {
                    // ha új dolgozó
                    Adat_Dolgozó_Alap EGYADAT = new Adat_Dolgozó_Alap(MyF.Szöveg_Tisztítás(Dolgozószámúj.Text.Trim(), 0, 8),
                                                                   MyF.Szöveg_Tisztítás(Dolgozónévúj.Text.Trim(), 0, 50),
                                                                   new DateTime(1900, 1, 1),
                                                                   Belépésiidő.Value);
                    KézDolgozó.Rögzítés(hely, EGYADAT);
                }
                else
                {
                    // ha visszaléptetett
                    Adat_Dolgozó_Alap EGYADAT = new Adat_Dolgozó_Alap(
                                                      MyF.Szöveg_Tisztítás(Dolgozószámúj.Text.Trim(), 0, 8),
                                                      MyF.Szöveg_Tisztítás(Dolgozónévúj.Text.Trim(), 0, 50),
                                                      new DateTime(1900, 1, 1),
                                                      new DateTime(1900, 1, 1));
                    KézDolgozó.Módosít_Csoport(hely, EGYADAT);
                }



                // státust is rögzítünk
                if (Státusid.Text.Trim() != "n")
                {
                    hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Segéd\Státus.mdb";
                    if (AdatStátus != null)
                    {
                        Adat_Dolgozó_Státus ADATBE = new Adat_Dolgozó_Státus(StátusID,
                                                                           MyF.Szöveg_Tisztítás(Dolgozónévúj.Text.Trim(), 0, 50),
                                                                           MyF.Szöveg_Tisztítás(Dolgozószámúj.Text.Trim(), 0, 8),
                                                                           BelépésiBér,
                                                                           Belépésiidő.Value);
                        KézStátus.Módosít_Be(hely, ADATBE);
                    }
                }


                // rögzítjük a béradatokat
                AdatokKulcs = KézKulcs.Lista_Adatok();
                Adat_Kulcs AdatKulcs = (from a in AdatokKulcs
                                        where a.Adat1.Contains(MyF.Rövidkód(Dolgozószámúj.Text))
                                        select a).FirstOrDefault();


                Adat_Kulcs ADAT = new Adat_Kulcs(MyF.Rövidkód(Dolgozószámúj.Text), MyF.Kódol(Belépésibér.Text));

                if (AdatKulcs != null)
                    KézKulcs.Módosít(ADAT);
                else
                    KézKulcs.Rögzít(ADAT);


                Kiürít();
                MessageBox.Show("Az Új dolgozó adatai rögzítésre kerültek. ", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);

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


        #region Dolgozó kilép
        private void Kiürítkilép()
        {
            KilépDolgozónév.Text = "";
            KilépDolgozószám.Text = "";
            Kilépésiidő.Value = new DateTime(1900, 1, 1);
            KilépTelephely.Text = "";
            Telephely.Text = "";
            Bér.Text = "";
        }

        private void KilépNévfeltöltés()
        {
            try
            {
                KilépDolgozónév.Items.Clear();
                KilépDolgozónév.BeginUpdate();
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Dolgozók.mdb";
                if (KilépTelephely.Visible) hely = $@"{Application.StartupPath}\{KilépTelephely.Text.Trim()}\Adatok\Dolgozók.mdb";

                List<Adat_Dolgozó_Alap> Adatok = KézDolgozó.Lista_Adatok(hely).OrderBy(a => a.DolgozóNév).ToList();
                if (Adatok != null)
                {
                    Adatok = (from a in Adatok
                              where a.Kilépésiidő < new DateTime(1900, 01, 31)
                              && !a.Vezényelt
                              orderby a.DolgozóNév
                              select a).ToList();
                }

                foreach (Adat_Dolgozó_Alap rekord in Adatok)
                    KilépDolgozónév.Items.Add(rekord.DolgozóNév.Trim() + " = " + rekord.Dolgozószám.Trim());

                KilépDolgozónév.EndUpdate();
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

        private void Command7_Click(object sender, EventArgs e)
        {
            if (KilépTelephely.Visible == false)
            {
                KilépTelephely.Visible = true;
                Label10.Visible = true;
                Label6.Text = "Előzetes Tervezett Kiléptetés";
                Panel3.BackColor = Color.LightCoral;
            }
            else
            {
                KilépTelephely.Visible = false;
                Label10.Visible = false;
                Label6.Text = "Kiléptetés";
                Panel3.BackColor = Color.LightGreen;
            }
            KilépNévfeltöltés();
            Kiürítkilép();
        }

        private void KilépTelephely_SelectedIndexChanged(object sender, EventArgs e)
        {
            KilépNévfeltöltés();
            Kiürítkilép();
        }

        private void KilépDolgozónév_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string hely;
                if (KilépTelephely.Visible == true)
                    hely = $@"{Application.StartupPath}\{KilépTelephely.Text.Trim()}\Adatok\Dolgozók.mdb";
                else
                    hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Dolgozók.mdb";

                string[] darabol = KilépDolgozónév.Text.Trim().Split('=');
                KilépDolgozószám.Text = darabol[1].Trim();

                AdatokDolgozóListázás(hely);
                Adat_Dolgozó_Alap AdatDolgozó = (from a in AdatokDolgozó
                                                 where a.Dolgozószám == darabol[0].Trim()
                                                 select a).FirstOrDefault();

                if (AdatDolgozó != null)
                {
                    Kilépésiidő.Value = AdatDolgozó.Kilépésiidő;
                    Telephely.Text = AdatDolgozó.Lakcím;
                    if (Telephely.Text.Trim() == "") Telephely.Text = Cmbtelephely.Text;
                }

                // bér kiírás
                hely = Application.StartupPath + @"\Főmérnökség\adatok\Villamos10.mdb";

                if (File.Exists(hely))
                {

                    AdatokKulcs = KézKulcs.Lista_Adatok();
                    Adat_Kulcs AdatKulcs = (from a in AdatokKulcs
                                            where a.Adat1.Contains(MyF.Rövidkód(KilépDolgozószám.Text))
                                            select a).FirstOrDefault();

                    if (AdatKulcs != null) Bér.Text = MyF.Dekódolja(AdatKulcs.Adat2);
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

        private void Csoportmódosítás_Click(object sender, EventArgs e)
        {
            try
            {
                if (KilépDolgozószám.Text.Trim() == "") throw new HibásBevittAdat("A dolgozó számát meg kell adni.");
                if (Kilépésiidő.Value == new DateTime(1900, 1, 1)) throw new HibásBevittAdat("A dátum beállítás hibás!");
                if (Kilépésiidő.Value <= DateTime.Today && Label6.Text == "Előzetes Tervezett Kiléptetés") throw new HibásBevittAdat("A dátum beállítás hibás! Jövőbeli dátumot lehet csak rögzíteni.");

                // leellenőrizzük, hogy van-e bér adat
                if (!double.TryParse(Bér.Text.Replace(".", ","), out double DolgBér)) DolgBér = 0;
                Bér.Text = DolgBér.ToString();

                if (KilépTelephely.Text.Trim() == "") KilépTelephely.Text = "_";
                // le ellenőrizzük, hogy a státusban kilépett-e már

                // új sort hoz létre a státusnál
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Segéd\Státus.mdb";

                // ha nincs  fájl, akkor másolunk egy újat.
                if (!File.Exists(hely)) Adatbázis_Létrehozás.Dolgozói_Státus(hely);

                AdatokStátusListázás();

                // ha jövőbeli a kilépés akkor nem rögzítünk újat
                Adat_Dolgozó_Státus AdatStátus = (from a in AdatokStátus
                                                  where a.Hrazonosítóki == KilépDolgozószám.Text.Trim()
                                                  && a.Kilépésdátum >= Kilépésiidő.Value
                                                  select a).FirstOrDefault();

                int melyik = 1;
                if (AdatokStátus != null) melyik = AdatokStátus.Max(a => a.ID).ToÉrt_Int();


                if (AdatStátus == null)
                {
                    string[] darabol = KilépDolgozónév.Text.Trim().Split('=');
                    Adat_Dolgozó_Státus ADATBE = new Adat_Dolgozó_Státus(0,
                                                                         darabol[0].Trim(),
                                                                         darabol[1].Trim(),
                                                                         DolgBér,
                                                                         KilépTelephely.Text,
                                                                         Kilépésiidő.Value,
                                                                         "_",
                                                                         "_",
                                                                         "_",
                                                                         new DateTime(1900, 1, 1),
                                                                         "Személy csere");
                    KézStátus.Rögzítés_Alap(hely, ADATBE);
                }
                else
                {
                    // módosíthatjuk a dátumot
                    Adat_Dolgozó_Státus ADATBE = new Adat_Dolgozó_Státus(melyik,
                                                                         Kilépésiidő.Value);
                    KézStátus.Módosít_Kilép(hely, ADATBE);
                }

                // ha nem látszódik a választó, akkor a dolgozó ki lesz léptetve
                if (!KilépTelephely.Visible)
                {

                    // ha nem látszódik akkor a dolgozó kilép
                    hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Dolgozók.mdb";
                    AdatokDolgozóListázás(hely);
                    Adat_Dolgozó_Alap AdatDolgozó = (from a in AdatokDolgozó
                                                     where a.Dolgozószám == KilépDolgozószám.Text.Trim()
                                                     select a).FirstOrDefault();

                    if (AdatDolgozó != null)
                    {
                        Adat_Dolgozó_Alap ADAT = new Adat_Dolgozó_Alap(KilépDolgozószám.Text.Trim(),
                                                                       Kilépésiidő.Value);
                        KézDolgozó.Módosít_Kilép(hely, ADAT);
                    }
                }
                Kiürítkilép();
                KilépNévfeltöltés();
                MessageBox.Show("A dolgozó adatai rögzítésre kerültek. ", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        #region Telephelyre vétel
        private void Command2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Dolgozószámba.Text.Trim() == "") throw new HibásBevittAdat("A HR azonosítót meg kell adni.");

                // törzsből elvesz
                string hely = $@"{Application.StartupPath}\" + Honnanba.Text + @"\Adatok\Dolgozók.mdb";

                AdatokDolgozóListázás(hely);
                Adat_Dolgozó_Alap AdatDolgozó = (from a in AdatokDolgozó
                                                 where a.Dolgozószám == Dolgozószámba.Text.Trim()
                                                 select a).FirstOrDefault();

                DateTime belépés = new DateTime(1900, 1, 1);
                if (AdatokDolgozó != null) belépés = AdatDolgozó.Belépésiidő;

                if (AdatDolgozó != null)
                {
                    Adat_Dolgozó_Alap ADAT1 = new Adat_Dolgozó_Alap(Dolgozószámba.Text.Trim(),
                                                                   DateTime.Today,
                                                                   Honnanba.Text.Trim());
                    KézDolgozó.Módosít_Kilép(hely, ADAT1);
                }

                // sajátba betesz
                hely = $@"{Application.StartupPath}\{Hovába.Text.Trim()}\Adatok\Dolgozók.mdb";

                AdatokDolgozóListázás(hely);
                AdatDolgozó = (from a in AdatokDolgozó
                               where a.Dolgozószám == Dolgozószámba.Text.Trim()
                               select a).FirstOrDefault();

                string[] darabol = Dolgozóba.Text.Trim().Split('=');

                Adat_Dolgozó_Alap ADAT = new Adat_Dolgozó_Alap(Dolgozószámba.Text.Trim(),
                                                               darabol[0].Trim(),
                                                               new DateTime(1900, 1, 1),
                                                               belépés,
                                                               Honnanba.Text.Trim());
                if (AdatDolgozó != null)
                    KézDolgozó.Módosít_Telep(hely, ADAT);
                else
                    KézDolgozó.Rögzítés_Telep(hely, ADAT);

                Névfeltöltésba();
                Dolgozószámba.Text = "";
                Dolgozóba.Text = "";
                MessageBox.Show("A dolgozó áthelyezésre került. ", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void Névfeltöltésba()
        {
            try
            {
                Dolgozóba.Items.Clear();
                Dolgozóba.BeginUpdate();

                string hely = $@"{Application.StartupPath}\{Honnanba.Text}\Adatok\Dolgozók.mdb";
                List<Adat_Dolgozó_Alap> Adatok = KézDolgozó.Lista_Adatok(hely);
                if (Adatok != null)
                {
                    Adatok = (from a in Adatok
                              where a.Kilépésiidő < new DateTime(1900, 01, 31)
                              orderby a.DolgozóNév
                              select a).ToList();
                }

                foreach (Adat_Dolgozó_Alap rekord in Adatok)
                    Dolgozóba.Items.Add(rekord.DolgozóNév.Trim() + " = " + rekord.Dolgozószám.Trim());

                Dolgozóba.EndUpdate();
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

        private void BAkitöltés()
        {
            try
            {
                Hovába.Text = Program.PostásTelephely.Trim();

                AdatokKönyvtárListázás();
                if (AdatokKönyvtár == null) return;
                Adat_Kiegészítő_Könyvtár AdatKönyvtár = (from a in AdatokKönyvtár
                                                         where a.Név == Program.PostásTelephely.Trim()
                                                         select a).FirstOrDefault();

                int csoport = AdatKönyvtár.Csoport1;

                AdatKönyvtár = (from a in AdatokKönyvtár
                                where a.Vezér1 == true
                                && a.Csoport1 == csoport
                                select a).FirstOrDefault();

                if (AdatKönyvtár != null) Honnanba.Text = AdatKönyvtár.Név;

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

        private void Dolgozóba_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] darabol = Dolgozóba.Text.Trim().Split('=');
            Dolgozószámba.Text = darabol[1].Trim();
        }
        #endregion


        #region Telephelyről kirak
        private void KItöltés()
        {
            try
            {
                HonnanKi.Text = Program.PostásTelephely.Trim();
                AdatokKönyvtárListázás();
                if (AdatokKönyvtár == null) return;
                Adat_Kiegészítő_Könyvtár AdatKönyvtár = (from a in AdatokKönyvtár
                                                         where a.Név == Program.PostásTelephely.Trim()
                                                         select a).FirstOrDefault();

                int csoport = AdatKönyvtár.Csoport1;

                AdatKönyvtár = (from a in AdatokKönyvtár
                                where a.Vezér1 == true
                                && a.Csoport1 == csoport
                                select a).FirstOrDefault();

                if (AdatKönyvtár != null) HováKi.Text = AdatKönyvtár.Név;
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

        private void NévfeltöltésKi()
        {
            try
            {
                DolgozóKi.Items.Clear();
                DolgozóKi.BeginUpdate();

                string hely = $@"{Application.StartupPath}\{HonnanKi.Text}\Adatok\Dolgozók.mdb";

                List<Adat_Dolgozó_Alap> Adatok = KézDolgozó.Lista_Adatok(hely);
                if (Adatok != null)
                {
                    Adatok = (from a in Adatok
                              where a.Kilépésiidő < new DateTime(1900, 01, 31)
                              && !a.Vezényelt
                              orderby a.DolgozóNév
                              select a).ToList();
                }

                foreach (Adat_Dolgozó_Alap rekord in Adatok)
                    DolgozóKi.Items.Add(rekord.DolgozóNév.Trim() + " = " + rekord.Dolgozószám.Trim());

                DolgozóKi.EndUpdate();
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

        private void DolgozóKi_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string[] darabol = DolgozóKi.Text.Trim().Split('=');
                DolgozószámKi.Text = darabol[1].Trim();
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

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (DolgozószámKi.Text.Trim() == "") throw new HibásBevittAdat("A HR azonosítót meg kell adni.");
                // sajátból kirak
                string hely = $@"{Application.StartupPath}\{HonnanKi.Text.Trim()}\Adatok\Dolgozók.mdb";

                AdatokDolgozóListázás(hely);
                if (AdatokDolgozó == null) return;
                Adat_Dolgozó_Alap AdatDolgozó = (from a in AdatokDolgozó
                                                 where a.Dolgozószám == DolgozószámKi.Text.Trim()
                                                 select a).FirstOrDefault();

                DateTime belépés = AdatDolgozó.Belépésiidő;


                if (AdatDolgozó != null)
                {
                    Adat_Dolgozó_Alap ADAT1 = new Adat_Dolgozó_Alap(DolgozószámKi.Text.Trim(),
                                                                    DateTime.Today,
                                                                    HonnanKi.Text.Trim());
                    KézDolgozó.Módosít_Ki(hely, ADAT1);
                }


                // törzsbe berak
                hely = $@"{Application.StartupPath}\{HováKi.Text.Trim()}\Adatok\Dolgozók.mdb";

                AdatokDolgozóListázás(hely);
                if (AdatokDolgozó == null) return;
                AdatDolgozó = (from a in AdatokDolgozó
                               where a.Dolgozószám == DolgozószámKi.Text.Trim()
                               select a).FirstOrDefault();

                Adat_Dolgozó_Alap ADAT2 = new Adat_Dolgozó_Alap(DolgozószámKi.Text.Trim(),
                                                                MyF.Szöveg_Tisztítás(DolgozóKi.Text.Trim(), 0, 50),
                                                                new DateTime(1900, 1, 1),
                                                                belépés,
                                                                HonnanKi.Text.Trim());

                if (AdatDolgozó != null)
                    KézDolgozó.Módosít_Telep(hely, ADAT2);
                else
                    KézDolgozó.Rögzítés_Telep(hely, ADAT2);

                NévfeltöltésKi();
                DolgozószámKi.Text = "";
                DolgozóKi.Text = "";
                MessageBox.Show("A dolgozó áthelyezésre került. ", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        #region Vezénylés létrehozás
        private void Vezénylésfeltöltés()
        {
            try
            {
                Telephonnan.Items.Clear();
                Telephová.Items.Clear();
                Telephová.Items.Add("BKV egyéb");
                Telephonnan.Items.Add("BKV egyéb");

                AdatokKönyvtárListázás();
                if (AdatokKönyvtár == null) return;
                Adat_Kiegészítő_Könyvtár AdatKönyvtár = (from a in AdatokKönyvtár
                                                         where a.Név == Cmbtelephely.Text.Trim()
                                                         select a).FirstOrDefault();


                int csoport = AdatKönyvtár.Csoport1;
                // Feltöltjük a csoport tagjait
                List<Adat_Kiegészítő_Könyvtár> AdatokKönyvtárRendezett = (from a in AdatokKönyvtár
                                                                          where a.Csoport1 == csoport
                                                                          orderby a.Név
                                                                          select a).ToList();

                foreach (Adat_Kiegészítő_Könyvtár rekord in AdatokKönyvtárRendezett)
                {
                    Telephová.Items.Add(rekord.Név.Trim());
                    Telephonnan.Items.Add(rekord.Név.Trim());
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

        private void Telephonnan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Telephonnan.Text.Trim() == "") throw new HibásBevittAdat("Honnan mezőt ki kell tölteni.");
                Dolgozószámvezénylés.Text = "";

                string hely = $@"{Application.StartupPath}\{Telephonnan.Text.Trim()}\Adatok\Dolgozók.mdb";
                if (!File.Exists(hely)) throw new HibásBevittAdat("Honnan mezőt ki kell tölteni.");

                Dolgozóvez.Items.Clear();

                List<Adat_Dolgozó_Alap> Adatok = KézDolgozó.Lista_Adatok(hely);
                if (Adatok != null)
                {
                    Adatok = (from a in Adatok
                              where a.Kilépésiidő < new DateTime(1900, 01, 31)
                              orderby a.DolgozóNév
                              select a).ToList();
                }

                foreach (Adat_Dolgozó_Alap rekord in Adatok)
                    Dolgozóvez.Items.Add(rekord.DolgozóNév.Trim() + " = " + rekord.Dolgozószám.Trim());

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

        private void Dolgozóvez_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string[] darabol = Dolgozóvez.Text.Trim().Split('=');
                Dolgozószámvezénylés.Text = darabol[1].Trim();
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

        private void Command4_Click(object sender, EventArgs e)
        {
            try
            {
                if (Telephonnan.Text.Trim() == "") throw new HibásBevittAdat("Honnan mezőt ki kell töltetni.");
                if (Telephová.Text.Trim() == " ") throw new HibásBevittAdat("Hová mezőt ki kell tölteni.");
                if (Dolgozószámvezénylés.Text.Trim() == "") throw new HibásBevittAdat("A HR azonósítót ki kell tölteni.");
                if (Dolgozóvez.Text.Trim() == "") throw new HibásBevittAdat("Dolgozó neve mezőt ki kell tölteni.");

                // sajátból kirak
                string hely = $@"{Application.StartupPath}\{Telephonnan.Text.Trim()}\Adatok\Dolgozók.mdb";

                AdatokDolgozóListázás(hely);
                if (AdatokDolgozó == null) return;
                Adat_Dolgozó_Alap AdatDolgozó = (from a in AdatokDolgozó
                                                 where a.Dolgozószám == Dolgozószámvezénylés.Text.Trim()
                                                 select a).FirstOrDefault();

                if (File.Exists(hely))
                {
                    if (AdatDolgozó != null)
                    {
                        Adat_Dolgozó_Alap ADATDolg = new Adat_Dolgozó_Alap(Dolgozószámvezénylés.Text.Trim(),
                                                                          "",
                                                                          new DateTime(1900, 1, 1),
                                                                          true,
                                                                          false,
                                                                          Telephová.Text.Trim());
                        KézDolgozó.Módosít_Vezénylés_Saját(hely, ADATDolg);
                    }
                }
                string[] darabol = Dolgozóvez.Text.Trim().Split('=');
                // a kívánt telephelyre

                hely = $@"{Application.StartupPath}\{Telephová.Text.Trim()}\Adatok\Dolgozók.mdb";

                AdatokDolgozóListázás(hely);
                if (AdatokDolgozó == null) return;
                AdatDolgozó = (from a in AdatokDolgozó
                               where a.Dolgozószám == Dolgozószámvezénylés.Text.Trim()
                               select a).FirstOrDefault();

                if (File.Exists(hely))
                {
                    Adat_Dolgozó_Alap ADATVBE = new Adat_Dolgozó_Alap(Dolgozószámvezénylés.Text.Trim(),
                                                  darabol[0].Trim(),
                                                  new DateTime(1900, 1, 1),
                                                  false,
                                                  true,
                                                  Telephonnan.Text.Trim());
                    if (AdatDolgozó != null)
                        KézDolgozó.Módosít_Vezénylés(hely, ADATVBE);
                    else
                        KézDolgozó.Rögzítés_Vezénylés(hely, ADATVBE);

                }
                Dolgozószámvezénylés.Text = "";
                Dolgozóvez.Text = "";
                Telephová.Text = "";
                Telephonnan.Text = "";
                Vezénylésfeltöltés();
                MessageBox.Show("A dolgozó vezénylése elkészült. ", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        #region Vezénylés törlés
        private void Vezényeltdolgozók()
        {
            try
            {
                // feltöltjük a vezényelt dolgozókat
                Veztörlésdolgozónév.Items.Clear();
                for (int j = 0; j < Cmbtelephely.Items.Count; j++)
                {
                    Cmbtelephely.Text = Cmbtelephely.Items[j].ToString();
                    string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text}\Adatok\Dolgozók.mdb";

                    List<Adat_Dolgozó_Alap> Adatok = KézDolgozó.Lista_Adatok(hely);
                    if (Adatok != null)
                    {
                        Adatok = (from a in Adatok
                                  where a.Kilépésiidő < new DateTime(1900, 01, 31)
                                  && (a.Vezényelt || a.Vezényelve)
                                  orderby a.DolgozóNév
                                  select a).ToList();
                    }

                    foreach (Adat_Dolgozó_Alap rekord in Adatok)
                        Veztörlésdolgozónév.Items.Add(rekord.DolgozóNév.Trim() + " = " + rekord.Dolgozószám.Trim());

                }
                Cmbtelephely.Text = Program.PostásTelephely.Trim();
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

        private void Command5_Click(object sender, EventArgs e)
        {
            try
            {
                if (Label22text.Text.Trim() == "") throw new HibásBevittAdat("A HR azonosítót be kell írni.");
                string lakcím;
                bool vezénylő;
                //Telephely ahova be vagyunk jelentkezve
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Dolgozók.mdb";

                AdatokDolgozóListázás(hely);


                if (File.Exists(hely))
                {
                    Adat_Dolgozó_Alap AdatDolgozó = (from a in AdatokDolgozó
                                                     where a.Dolgozószám == Label22text.Text.Trim()
                                                     && a.Kilépésiidő == new DateTime(1900, 1, 1)
                                                     select a).FirstOrDefault();
                    if (AdatDolgozó != null)
                    {
                        lakcím = AdatDolgozó.Lakcím;
                        vezénylő = AdatDolgozó.Vezényelt;

                        if (vezénylő)
                        {
                            // a saját telephely
                            hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Dolgozók.mdb";
                            Adat_Dolgozó_Alap ADATVS = new Adat_Dolgozó_Alap(Label22text.Text.Trim(),
                                                                             "",
                                                                             new DateTime(1900, 1, 1),
                                                                             false,
                                                                             false,
                                                                             "");
                            KézDolgozó.Módosít_Vezénylés(hely, ADATVS);

                            // idegen telephely
                            hely = $@"{Application.StartupPath}\{lakcím.Trim()}\Adatok\Dolgozók.mdb";
                            if (File.Exists(hely))
                            {
                                Adat_Dolgozó_Alap ADATV = new Adat_Dolgozó_Alap(Label22text.Text.Trim(),
                                                                                "",
                                                                                DateTime.Today,
                                                                                false,
                                                                                false,
                                                                                "");
                                KézDolgozó.Módosít_Vezénylés(hely, ADATV);
                            }
                        }
                        else
                        {
                            // a saját telephely
                            hely = $@"{Application.StartupPath}\{lakcím.Trim()}\Adatok\Dolgozók.mdb";
                            if (File.Exists(hely))
                            {
                                Adat_Dolgozó_Alap ADATVS = new Adat_Dolgozó_Alap(Label22text.Text.Trim(),
                                                                                 "",
                                                                                 new DateTime(1900, 1, 1),
                                                                                 false,
                                                                                 false,
                                                                                 "");
                                KézDolgozó.Módosít_Vezénylés(hely, ADATVS);
                            }

                            // idegen telephely
                            hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Dolgozók.mdb";
                            Adat_Dolgozó_Alap ADATV = new Adat_Dolgozó_Alap(Label22text.Text.Trim(),
                                                                             "",
                                                                             DateTime.Today,
                                                                             false,
                                                                             false,
                                                                             "");
                            KézDolgozó.Módosít_Vezénylés(hely, ADATV);
                        }
                    }
                }

                Vezényeltdolgozók();
                Veztörlésdolgozónév.Text = "";
                Label22text.Text = "";
                MessageBox.Show("A dolgozó vezénylése törlésre került. ", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Veztörlésdolgozónév_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Veztörlésdolgozónév.Text.Trim() == "")
                    throw new HibásBevittAdat("Dolgozó neve mezőt ki kell tölteni.");
                string[] darabol = Veztörlésdolgozónév.Text.Trim().Split('=');
                Label22text.Text = darabol[1].Trim();
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


        #region Listák feltöltése

        private void AdatokKönyvtárListázás()
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

        private void AdatokStátusListázás()
        {
            try
            {
                AdatokStátus.Clear();
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Segéd\Státus.mdb";
                AdatokStátus = KézStátus.Lista_Adatok(hely);
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

        private void AdatokDolgozóListázás(string hely)
        {
            try
            {
                AdatokDolgozó.Clear();
                AdatokDolgozó = KézDolgozó.Lista_Adatok(hely);
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
    }
}