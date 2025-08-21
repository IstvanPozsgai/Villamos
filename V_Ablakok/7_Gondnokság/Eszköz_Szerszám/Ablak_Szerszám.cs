using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_Adatszerkezet;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{

    public partial class Ablak_Szerszám
    {
        public Ablak_Szerszám()
        {
            InitializeComponent();
            Start();
        }
        readonly Kezelő_Szerszám_Cikk KézSzerszámCikk = new Kezelő_Szerszám_Cikk();
        readonly Kezelő_Szerszám_Könyv KézKönyv = new Kezelő_Szerszám_Könyv();
        readonly Kezelő_Szerszám_könvyvelés KézKönyvelés = new Kezelő_Szerszám_könvyvelés();
        readonly Kezelő_Dolgozó_Alap KézDolgozó = new Kezelő_Dolgozó_Alap();
        readonly Kezelő_Kiegészítő_Jelenlétiív KézJelenléti = new Kezelő_Kiegészítő_Jelenlétiív();
        readonly Kezelő_Eszköz KézEszköz = new Kezelő_Eszköz();
        readonly Kezelő_Szerszám_Napló KézNapló = new Kezelő_Szerszám_Napló();
        readonly Kezelő_Szerszám_FejLáb KézSzerszámFejLáb = new Kezelő_Szerszám_FejLáb();

        List<Adat_Kiegészítő_Jelenlétiív> AdatokJelenléti = new List<Adat_Kiegészítő_Jelenlétiív>();
        List<Adat_Szerszám_Cikktörzs> AdatokCikk = new List<Adat_Szerszám_Cikktörzs>();
        List<Adat_Szerszám_Könyvtörzs> AdatokKönyv = new List<Adat_Szerszám_Könyvtörzs>();
        List<Adat_Szerszám_Könyvelés> AdatokKönyvelés = new List<Adat_Szerszám_Könyvelés>();
        List<Adat_Dolgozó_Alap> AdatokDolgozó = new List<Adat_Dolgozó_Alap>();
        List<Adat_Eszköz> AdatokEszköz = new List<Adat_Eszköz>();
        List<Adat_Szerszám_Napló> AdatokNapló = new List<Adat_Szerszám_Napló>();

        readonly DataTable AdatTáblaCikk = new DataTable();
        readonly DataTable AdatTáblaKönyv = new DataTable();
        readonly DataTable AdatTáblaKönyvelés = new DataTable();
        readonly DataTable AdatTáblaNapló = new DataTable();
        readonly DataTable AdatTáblaLekérd = new DataTable();

        //szerszámot ad át ha szerszámnyilvántartás
        //... ad át ha épületnyilvántartás
        private string Könyvtár_adat;


        //Itt kapjuk meg a főoldaltól, hogy melyik funkciót akarom használni
        public void SetData(string Könyvtár_adat)
        {
            if (this.Könyvtár_adat == null)
            {
                this.Könyvtár_adat = $@"Adatok\{Könyvtár_adat}";
            }
        }

        private void Ablak_Szerszám_Load(object sender, EventArgs e)
        {

        }

        #region Alap
        private void Start()
        {
            try
            {
                //Ha van 0-tól különböző akkor a régi jogosultságkiosztást használjuk
                //ha mind 0 akkor a GombLathatosagKezelo-t használjuk
                if (Program.PostásJogkör.Any(c => c != '0'))
                {
                    Telephelyekfeltöltése();
                    Jogosultságkiosztás();
                }
                else
                {
                    TelephelyekFeltöltéseÚj();
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
                }
                if (Könyvtár_adat.Trim() == "Adatok\\Szerszám")
                    this.Text = "Szerszám Nyilvántartás";
                else
                    this.Text = "Helység tartozék nyilvántartás";

                string hova;

                // létrehozzuk a  könyvtárat
                hova = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\" + Könyvtár_adat;
                if (!Directory.Exists(hova)) Directory.CreateDirectory(hova);

                hova = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\{Könyvtár_adat}\Adatok";
                if (!Directory.Exists(hova)) Directory.CreateDirectory(hova);

                hova = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\{Könyvtár_adat}\Szerszám_képek";
                if (!Directory.Exists(hova)) Directory.CreateDirectory(hova);

                hova = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\{Könyvtár_adat}\Szerszám_PDF";
                if (!Directory.Exists(hova)) Directory.CreateDirectory(hova);

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\{Könyvtár_adat}\Adatok\Szerszám.mdb";
                string jelszó = "csavarhúzó";

                if (!File.Exists(hely))
                {
                    Adatbázis_Létrehozás.Szerszám_nyilvántartás(hely);
                    // hozzáadjuk az előírt értékeket
                    Adat_Szerszám_Könyvtörzs Adat;
                    Adat = new Adat_Szerszám_Könyvtörzs("Érkezett", "Új eszközök beérkeztetése", "_", "_", false);
                    KézKönyv.Rögzítés(hely, jelszó, Adat);

                    Adat = new Adat_Szerszám_Könyvtörzs("Raktár", "Szerszámraktárban lévő anyagok és eszközök", "_", "_", false);
                    KézKönyv.Rögzítés(hely, jelszó, Adat);

                    Adat = new Adat_Szerszám_Könyvtörzs("Selejt", "Leselejtezett", "_", "_", false);
                    KézKönyv.Rögzítés(hely, jelszó, Adat);

                    Adat = new Adat_Szerszám_Könyvtörzs("Selejtre", "Selejtezésre előkészítés", "_", "_", false);
                    KézKönyv.Rögzítés(hely, jelszó, Adat);
                }

                CikktörzsListaFeltöltés();
                KönyvListaFeltöltés();
                DolgozóListaFeltöltés();
                JelenlétiListaFeltöltés();
                EszközListaFeltöltés();
                KönyvelésListaFeltöltés();
                NaplóListaFeltöltés(DateTime.Today);

                Fülekkitöltése();
                Azonosítók();

                JelenlétiListaFeltöltés();
                Lapfülek.DrawMode = TabDrawMode.OwnerDrawFixed;

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
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Személy(true))
                    Cmbtelephely.Items.Add(Elem);

                if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér)
                    Cmbtelephely.Text = Cmbtelephely.Items[0].ToStrTrim();
                else
                    Cmbtelephely.Text = Program.PostásTelephely;

                Cmbtelephely.Enabled = Program.Postás_Vezér;
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
        private void TelephelyekFeltöltéseÚj()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Adat in GombLathatosagKezelo.Telephelyek(this.Name))
                    Cmbtelephely.Items.Add(Adat.Trim());
                //Alapkönyvtárat beállítjuk 
                Cmbtelephely.Text = Program.PostásTelephely;
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
                Alap_Rögzít.Enabled = false;
                KépTörlés.Enabled = false;
                Kép_rögzít.Enabled = false;

                Könyv_Rögzít.Enabled = false;
                Rögzít.Enabled = false;

                // csak főmérnökségi belépéssel törölhető
                if (Program.PostásTelephely == "Főmérnökség")
                { }
                else
                { }

                if (Könyvtár_adat.Trim() == "Adatok\\Szerszám")
                { melyikelem = 230; }
                else
                { melyikelem = 229; }



                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Alap_Rögzít.Enabled = true;
                    KépTörlés.Enabled = true;
                    Kép_rögzít.Enabled = true;
                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    Könyv_Rögzít.Enabled = true;
                }
                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 3))
                {
                    Rögzít.Enabled = true;
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
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\szerszám.html";
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

        private void LapFülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }

        private void Fülekkitöltése()
        {
            try
            {
                switch (Lapfülek.SelectedIndex)
                {
                    case 0:
                        {
                            // törzs lap
                            Ürít();
                            Alap_Beszerzési_dátum.Value = DateTime.Today;
                            AcceptButton = Alap_Rögzít;
                            break;
                        }
                    case 1:
                        {
                            // könyvlap
                            Szeszámkönyvfeltöltés();
                            Névfeltöltés();
                            Könyv_tábla_író();
                            AcceptButton = Könyv_Rögzít;
                            break;
                        }
                    case 2:
                        {
                            // rögzítés
                            Honnan_feltöltések();
                            AcceptButton = Rögzít;
                            break;
                        }


                    case 3:
                        {
                            // Lekérdezés
                            if (Lekérd_Szerszámkönyvszám.Items.Count <= 0)
                            {
                                Lekérd_Szeszámkönyvfeltöltés();
                                Lekérd_névfeltöltés();
                                AcceptButton = Lekérd_Jelöltszersz;
                            }
                            break;
                        }
                    case 4:
                        {
                            // Naplózás
                            Napló_Dátumtól.Value = DateTime.Today;
                            Napló_Dátumig.Value = DateTime.Today;
                            Napló_könyv_feltöltés();
                            Napló_táblaíró();
                            AcceptButton = Napló_Listáz;
                            break;
                        }

                    case 5:
                        {
                            // képek
                            break;
                        }
                    case 6:
                        {
                            // pdf
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

        private void LapFülek_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                // Határozza meg, hogy melyik lap van jelenleg kiválasztva
                TabPage SelectedTab = Lapfülek.TabPages[e.Index];

                // Szerezze be a lap fejlécének területét
                Rectangle HeaderRect = Lapfülek.GetTabRect(e.Index);

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
                    Font BoldFont = new Font(Lapfülek.Font.Name, Lapfülek.Font.Size, FontStyle.Bold);
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

        private void Azonosítók()
        {
            CikktörzsListaFeltöltés();
            Alap_Azonosítók();
            Lekérd_Azonosítók();
            Képek_Azonosítók_feltöltése();
            PDF_Azonosítók_feltöltése();
        }
        #endregion

        #region CikkTörzslap 
        private void Alap_Azonosítók()
        {
            try
            {
                Alap_Azonosító.Items.Clear();
                List<Adat_Szerszám_Cikktörzs> AdatokSzűrt;
                if (!Alap_Töröltek.Checked)
                    AdatokSzűrt = (from a in AdatokCikk
                                   where a.Státus == 0
                                   orderby a.Azonosító
                                   select a).ToList();
                else
                    AdatokSzűrt = (from a in AdatokCikk
                                   where a.Státus == 0
                                   orderby a.Azonosító
                                   select a).ToList();
                if (AdatokSzűrt != null)
                {
                    foreach (Adat_Szerszám_Cikktörzs elem in AdatokSzűrt)
                        Alap_Azonosító.Items.Add(elem.Azonosító);
                }
                Alap_Azonosító.Refresh();
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

        private void Ürít()
        {
            Alap_Leltáriszám.Text = "";
            Alap_Megnevezés.Text = "";
            Alap_Méret.Text = "";
            Alap_Beszerzési_dátum.Value = new DateTime(1900, 1, 1);
            Alap_Aktív.Checked = false;
            Alap_Azonosító.Text = "";
            Alap_Költséghely.Text = "";
            Alap_tárolás.Text = "";
            Alap_Gyáriszám.Text = "";
        }

        private void Frissít_Click(object sender, EventArgs e)
        {
            Azonosítók();
            Ürít();
            Alap_tábla_író();
        }

        private void Azonosító_SelectedIndexChanged(object sender, EventArgs e)
        {
            Alap_azonísító_választó();
        }

        private void Alap_Azonosító_TextChanged(object sender, EventArgs e)
        {
            Alap_azonísító_választó();
        }

        private void Alap_lekérd_megnevezés_MouseClick(object sender, MouseEventArgs e)
        {
            AcceptButton = Alap_Frissít;
        }

        private void Alap_Lekérdezés_Méret_MouseClick(object sender, MouseEventArgs e)
        {
            AcceptButton = Alap_Frissít;
        }

        private void Alap_Azonosító_MouseClick(object sender, MouseEventArgs e)
        {
            AcceptButton = Alap_Rögzít;
        }

        private void Alap_azonísító_választó()
        {
            try
            {
                if (Alap_Azonosító.Text.Trim() == "") return;
                CikktörzsListaFeltöltés();

                Adat_Szerszám_Cikktörzs Adat = (from a in AdatokCikk
                                                where a.Azonosító == Alap_Azonosító.Text.Trim()
                                                select a).FirstOrDefault();
                if (Adat == null) return;

                Alap_Leltáriszám.Text = Adat.Leltáriszám;
                Alap_Megnevezés.Text = Adat.Megnevezés;
                Alap_Méret.Text = Adat.Méret;
                Alap_Költséghely.Text = Adat.Költséghely;
                Alap_tárolás.Text = Adat.Hely;
                Alap_Beszerzési_dátum.Value = Adat.Beszerzésidátum;
                Alap_Gyáriszám.Text = Adat.Gyáriszám;
                if (Adat.Státus == 1)
                    Alap_Aktív.Checked = true;
                else
                    Alap_Aktív.Checked = false;

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

        private void Új_adat_Click(object sender, EventArgs e)
        {
            Ürít();
        }

        private void Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                Alap_Azonosító.Text = Alap_Azonosító.Text.ToUpper();
                if (Alap_Azonosító.Text.Trim() == "") throw new HibásBevittAdat("Nincs az azonosító mező kitöltve.");
                if (Alap_Azonosító.Text.Length > 20) throw new HibásBevittAdat("Azonosító maximum 20 karakter hosszú lehet!");
                if (!(Alap_Azonosító.Text.Substring(0, 1) == "E" || Alap_Azonosító.Text.Substring(0, 1) == "A")) throw new HibásBevittAdat("Azonosítónak 'A'-val, vagy 'E'-vel kell kezdődnie!");
                if (Alap_Megnevezés.Text.Trim() == "") throw new HibásBevittAdat("Megnevezés nem lehet üres!");
                if (Alap_Megnevezés.Text.Length > 50) throw new HibásBevittAdat("Megnevezés maximum 50 karakter hosszú lehet!");
                if (Alap_Méret.Text.Length > 15) throw new HibásBevittAdat("Méret maximum 15 karakter hosszú lehet!");
                if (Alap_tárolás.Text.Length > 50) throw new HibásBevittAdat("Tárolási hely maximum 50 karakter hosszú lehet!");
                if (Alap_Leltáriszám.Text.Length > 50) throw new HibásBevittAdat("Leltáriszám maximum 20 karakter hosszú lehet!");
                if (Alap_Leltáriszám.Text.Trim() == "" && Alap_Azonosító.Text.Substring(0, 1) == "E") throw new HibásBevittAdat("Leltáriszám nem lehet nulla hosszúságú, ha 'E'-vel kezdődik!");
                if (Alap_Költséghely.Text.Length > 6) throw new HibásBevittAdat("Költséghely maximum 6 karakter hosszú lehet!");

                CikktörzsListaFeltöltés();
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\{Könyvtár_adat}\Adatok\Szerszám.mdb";
                string jelszó = "csavarhúzó";

                string azonosító = Alap_Azonosító.Text.Replace("\"", "");
                string megnevezés = Alap_Megnevezés.Text.Replace("\"", "");
                string méret = Alap_Méret.Text.Replace("\"", "");
                string helyes = Alap_tárolás.Text.Trim();
                string leltáriszám = Alap_Leltáriszám.Text.Trim();
                DateTime beszerzésidátum = Alap_Beszerzési_dátum.Value;
                string költséghely = Alap_Költséghely.Text.Trim();
                string gyáriszám = Alap_Gyáriszám.Text.Trim();
                int státus = Alap_Aktív.Checked ? 1 : 0;

                Adat_Szerszám_Cikktörzs Adat = new Adat_Szerszám_Cikktörzs(azonosító, megnevezés, méret, helyes, leltáriszám, beszerzésidátum, státus, költséghely, gyáriszám);

                Adat_Szerszám_Cikktörzs Elem = (from a in AdatokCikk
                                                where a.Azonosító == Alap_Azonosító.Text.Trim()
                                                select a).FirstOrDefault();

                if (Elem != null)
                {
                    KézSzerszámCikk.Módosítás(hely, jelszó, Adat);
                    MessageBox.Show("Az adatok módosítás megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    KézSzerszámCikk.Rögzítés(hely, jelszó, Adat);
                    Azonosítók();
                    Ürít();
                    MessageBox.Show("Az adatok rögzítése megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Alap_tábla_író();
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

        private void Alap_tábla_író()
        {
            try
            {
                CikktörzsListaFeltöltés();
                KönyvelésListaFeltöltés();

                Alap_tábla.Visible = false;
                // fejléc elkészítése
                AdatCikk_Fejléc();
                AdatCikk_Tartalom();
                Alap_tábla.CleanFilterAndSort();
                Alap_tábla.DataSource = AdatTáblaCikk;
                CikkOszlopSzélesség();
                Alap_tábla.Visible = true;
                Alap_tábla.Refresh();
                Alap_tábla.ClearSelection();
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

        private void AdatCikk_Tartalom()
        {
            List<Adat_Szerszám_Cikktörzs> SzűrtAdatok;

            if (!Alap_Töröltek.Checked)
                SzűrtAdatok = (from a in AdatokCikk
                               where a.Státus == 0
                               orderby a.Azonosító
                               select a).ToList();
            else
                SzűrtAdatok = (from a in AdatokCikk
                               where a.Státus == 1
                               orderby a.Azonosító
                               select a).ToList();

            if (Radio_A.Checked)
                SzűrtAdatok = (from a in SzűrtAdatok
                               where a.Azonosító.Substring(0, 1) == "A"
                               orderby a.Azonosító
                               select a).ToList();
            if (Radio_E.Checked)
                SzűrtAdatok = (from a in SzűrtAdatok
                               where a.Azonosító.Substring(0, 1) == "E"
                               orderby a.Azonosító
                               select a).ToList();

            if (Alap_lekérd_megnevezés.Text.Trim() != "")
                SzűrtAdatok = (from a in SzűrtAdatok
                               where a.Megnevezés.Contains(Alap_lekérd_megnevezés.Text.Trim())
                               orderby a.Azonosító
                               select a).ToList();

            if (Alap_Lekérdezés_Méret.Text.Trim() != "")
                SzűrtAdatok = (from a in SzűrtAdatok
                               where a.Méret.Contains(Alap_Lekérdezés_Méret.Text.Trim())
                               orderby a.Azonosító
                               select a).ToList();
            if (SzűrtAdatok == null) return;

            AdatTáblaCikk.Clear();
            foreach (Adat_Szerszám_Cikktörzs rekord in SzűrtAdatok)
            {
                DataRow Soradat = AdatTáblaCikk.NewRow();
                Soradat["Azonosító"] = rekord.Azonosító;
                Soradat["Leltári szám"] = rekord.Leltáriszám;
                Soradat["Megnevezés"] = rekord.Megnevezés;
                Soradat["Méret"] = rekord.Méret;
                Soradat["Gyári szám"] = rekord.Gyáriszám;
                Soradat["Tárolási hely"] = rekord.Hely;
                Soradat["Beszerzési dátum"] = rekord.Beszerzésidátum.ToString("yyyy.MM.dd");
                Soradat["Költséghely"] = rekord.Költséghely;
                Soradat["Aktív"] = rekord.Státus == 1 ? "Törölt" : "Élő";
                Soradat["Szer.Könyv"] = "";
                if (rekord.Azonosító.Contains("E"))
                {
                    Adat_Szerszám_Könyvelés Elem = (from a in AdatokKönyvelés
                                                    where a.AzonosítóMás == rekord.Azonosító
                                                    select a).FirstOrDefault();
                    if (Elem != null) Soradat["Szer.Könyv"] = Elem.SzerszámkönyvszámMás;
                }
                Soradat["Fénykép szám"] = Fényképek_száma(rekord.Azonosító);
                Soradat["PDF szám"] = PDF_száma(rekord.Azonosító);
                AdatTáblaCikk.Rows.Add(Soradat);
            }
        }

        private void AdatCikk_Fejléc()
        {
            AdatTáblaCikk.Columns.Clear();
            AdatTáblaCikk.Columns.Add("Azonosító");
            AdatTáblaCikk.Columns.Add("Leltári szám");
            AdatTáblaCikk.Columns.Add("Megnevezés");
            AdatTáblaCikk.Columns.Add("Méret");
            AdatTáblaCikk.Columns.Add("Gyári szám");
            AdatTáblaCikk.Columns.Add("Tárolási hely");
            AdatTáblaCikk.Columns.Add("Beszerzési dátum");
            AdatTáblaCikk.Columns.Add("Költséghely");
            AdatTáblaCikk.Columns.Add("Aktív");
            AdatTáblaCikk.Columns.Add("Szer.Könyv");
            AdatTáblaCikk.Columns.Add("Fénykép szám");
            AdatTáblaCikk.Columns.Add("PDF szám");
        }

        private void CikkOszlopSzélesség()
        {
            Alap_tábla.Columns["Azonosító"].Width = 135;
            Alap_tábla.Columns["Leltári szám"].Width = 135;
            Alap_tábla.Columns["Megnevezés"].Width = 400;
            Alap_tábla.Columns["Méret"].Width = 110;
            Alap_tábla.Columns["Gyári szám"].Width = 100;
            Alap_tábla.Columns["Tárolási hely"].Width = 200;
            Alap_tábla.Columns["Beszerzési dátum"].Width = 120;
            Alap_tábla.Columns["Költséghely"].Width = 80;
            Alap_tábla.Columns["Aktív"].Width = 80;
            Alap_tábla.Columns["Szer.Könyv"].Width = 180;
            Alap_tábla.Columns["Fénykép szám"].Width = 80;
            Alap_tábla.Columns["PDF szám"].Width = 80;
        }

        private void Alap_tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                Alap_Azonosító.Text = Alap_tábla.Rows[e.RowIndex].Cells[0].Value.ToStrTrim();
                PDF_Azonosító.Text = Alap_Azonosító.Text;
                Kép_Azonosító.Text = Alap_Azonosító.Text;
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

        private void Alap_excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Alap_tábla.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Szerszám_Cikk_{Program.PostásNév}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyE.DataGridViewToExcel(fájlexc, Alap_tábla);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyE.Megnyitás(fájlexc);
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

        private int Fényképek_száma(string Azonosító)
        {
            int válasz = 0;
            try
            {

                string helykép = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\{Könyvtár_adat}\Szerszám_képek";
                if (!System.IO.Directory.Exists(helykép))
                {
                    // Megnézzük, hogy létezik-e a könyvtár, ha nem létrehozzuk
                    System.IO.Directory.CreateDirectory(helykép);
                    return válasz;
                }

                Kép_szűrés.Items.Clear();
                DirectoryInfo Directories = new DirectoryInfo(helykép);
                string mialapján = $"{Azonosító}_*.jpg";

                FileInfo[] fileInfo = Directories.GetFiles(mialapján, System.IO.SearchOption.AllDirectories);

                if (fileInfo.Length < 1)
                    válasz = 0;
                else
                    válasz = fileInfo.Length;
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
            return válasz;
        }

        private int PDF_száma(string Azonosító)
        {
            int válasz = 0;
            try
            {
                string helykép = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\{Könyvtár_adat}\Szerszám_PDF";
                if (System.IO.Directory.Exists(helykép) == false)
                {
                    // Megnézzük, hogy létezik-e a könyvtár, ha nem létrehozzuk
                    System.IO.Directory.CreateDirectory(helykép);
                    return válasz;
                }
                Kép_szűrés.Items.Clear();
                DirectoryInfo Directories = new DirectoryInfo(helykép);

                string mialapján = $"{Azonosító}_*.jpg";

                FileInfo[] fileInfo = Directories.GetFiles(mialapján, System.IO.SearchOption.AllDirectories);

                if (fileInfo.Length < 1)
                    válasz = 0;
                else
                    válasz = fileInfo.Length;

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
            return válasz;
        }
        #endregion

        #region Könyv lap
        private void Szeszámkönyvfeltöltés()
        {
            try
            {
                Könyv_szám.Items.Clear();
                KönyvListaFeltöltés();
                List<Adat_Szerszám_Könyvtörzs> Adatok = (from a in AdatokKönyv
                                                         where a.Státus == Könyv_Töröltek.Checked
                                                         select a).ToList();
                foreach (Adat_Szerszám_Könyvtörzs elem in Adatok)
                    Könyv_szám.Items.Add(elem.Szerszámkönyvszám);
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

        private void Névfeltöltés()
        {
            try
            {
                Könyv_Felelős1.Items.Clear();
                Könyv_Felelős1.BeginUpdate();
                Könyv_Felelős2.Items.Clear();
                Könyv_Felelős2.BeginUpdate();

                DolgozóListaFeltöltés();

                List<Adat_Dolgozó_Alap> Adatok = (from a in AdatokDolgozó
                                                  where a.Kilépésiidő == new DateTime(1900, 1, 1)
                                                  select a).ToList();

                foreach (Adat_Dolgozó_Alap Adat in Adatok)
                {
                    Könyv_Felelős1.Items.Add($"{Adat.DolgozóNév} = {Adat.Dolgozószám}");
                    Könyv_Felelős2.Items.Add($"{Adat.DolgozóNév} = {Adat.Dolgozószám}");
                }

                Könyv_Felelős1.EndUpdate();
                Könyv_Felelős2.EndUpdate();
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

        private void Frissít_Click1(object sender, EventArgs e)
        {
            Szeszámkönyvfeltöltés();
            Névfeltöltés();
            Könyv_tábla_író();
        }

        private void Töröltek_CheckedChanged_1(object sender, EventArgs e)
        {
            Könyv_szám.Text = "";
            Szeszámkönyvfeltöltés();
            Könyv_tábla_író();
        }

        private void Szerszámkönyvszám_SelectedIndexChanged(object sender, EventArgs e)
        {
            Kírja_könyvet();
        }

        private void Kírja_könyvet()
        {
            try
            {
                Könyv_ürít();
                KönyvListaFeltöltés();

                Adat_Szerszám_Könyvtörzs Adat = (from a in AdatokKönyv
                                                 where a.Szerszámkönyvszám == Könyv_szám.Text.Trim()
                                                 select a).FirstOrDefault();
                if (Adat == null) return;

                Könyv_szám.Text = Adat.Szerszámkönyvszám.Trim();
                Könyv_megnevezés.Text = Adat.Szerszámkönyvnév.Trim();
                Könyv_Felelős1.Text = Adat.Felelős1.Trim();
                Könyv_Felelős2.Text = Adat.Felelős2.Trim();
                Könyv_Törlés.Checked = Adat.Státus;
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

        private void Könyv_ürít()
        {
            Könyv_megnevezés.Text = "";
            Könyv_Felelős1.Text = "";
            Könyv_Felelős2.Text = "";
            Könyv_Törlés.Checked = false;
        }

        private void Könyv_tábla_író()
        {
            try
            {
                KönyvListaFeltöltés();
                List<Adat_Szerszám_Könyvtörzs> Adatok = (from a in AdatokKönyv
                                                         where a.Státus == Könyv_Töröltek.Checked
                                                         select a).ToList();

                AdatTáblaKönyv.Columns.Clear();
                AdatTáblaKönyv.Columns.Add("Könyvszám");
                AdatTáblaKönyv.Columns.Add("Könyvmegnevezés");
                AdatTáblaKönyv.Columns.Add("Felelős személy 1");
                AdatTáblaKönyv.Columns.Add("Felelős személy 2");
                AdatTáblaKönyv.Columns.Add("Aktív");

                AdatTáblaKönyv.Clear();
                foreach (Adat_Szerszám_Könyvtörzs adat in Adatok)
                {
                    DataRow Soradat = AdatTáblaKönyv.NewRow();

                    Soradat["Könyvszám"] = adat.Szerszámkönyvszám.Trim();
                    Soradat["Könyvmegnevezés"] = adat.Szerszámkönyvnév.Trim();
                    Soradat["Felelős személy 1"] = adat.Felelős1.Trim();
                    Soradat["Felelős személy 2"] = adat.Felelős2.Trim();
                    Soradat["Aktív"] = adat.Státus ? "Törölt" : "Élő";

                    AdatTáblaKönyv.Rows.Add(Soradat);
                }
                Könyv_tábla.CleanFilterAndSort();
                Könyv_tábla.DataSource = AdatTáblaKönyv;

                Könyv_tábla.Columns["Könyvszám"].Width = 100;
                Könyv_tábla.Columns["Könyvmegnevezés"].Width = 350;
                Könyv_tábla.Columns["Felelős személy 1"].Width = 350;
                Könyv_tábla.Columns["Felelős személy 2"].Width = 350;
                Könyv_tábla.Columns["Aktív"].Width = 100;

                Könyv_tábla.Visible = true;
                Könyv_tábla.Refresh();

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

        private void Könyv_tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                Könyv_szám.Text = Könyv_tábla.Rows[e.RowIndex].Cells[0].Value.ToString();
                Kírja_könyvet();
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

        private void Könyv_új_Click(object sender, EventArgs e)
        {
            Könyv_ürít();
            Könyv_szám.Text = "";
        }

        private void Rögzít_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (Könyv_szám.Text.Length > 10) throw new HibásBevittAdat("Könyvszám maximum 10 karakter hosszú lehet!");
                if (Könyv_szám.Text.Trim() == "") throw new HibásBevittAdat("Könyvszámnak lennie kell!");
                if (Könyv_megnevezés.Text.Trim() == "") throw new HibásBevittAdat("Könyvnévnek lennie kell!");
                if (Könyv_megnevezés.Text.Length > 50) throw new HibásBevittAdat("Könyvnév maximum 50 karakter hosszú lehet!");
                if (Könyv_Felelős1.Text.Length > 50) throw new HibásBevittAdat("Felelős1 maximum 50 karakter hosszú lehet!");
                if (Könyv_Felelős2.Text.Length > 50) throw new HibásBevittAdat("Felelős2 maximum 50 karakter hosszú lehet!");
                if (Könyv_szám.Text == "") throw new HibásBevittAdat("Nincs kijelölve egy könyv sem.");
                if (Könyv_megnevezés.Text == "") throw new HibásBevittAdat("Nincs kijelölve egy könyv sem.");

                KönyvListaFeltöltés();
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\{Könyvtár_adat}\Adatok\Szerszám.mdb";
                string jelszó = "csavarhúzó";

                Adat_Szerszám_Könyvtörzs Adat = new Adat_Szerszám_Könyvtörzs(Könyv_szám.Text.Trim(),
                                                                             Könyv_megnevezés.Text.Trim(),
                                                                             Könyv_Felelős1.Text.Trim(),
                                                                             Könyv_Felelős2.Text.Trim(),
                                                                             Könyv_Törlés.Checked);
                Adat_Szerszám_Könyvtörzs Elem = (from a in AdatokKönyv
                                                 where a.Szerszámkönyvszám == Könyv_szám.Text.Trim()
                                                 select a).FirstOrDefault();

                if (Elem == null)
                    KézKönyv.Rögzítés(hely, jelszó, Adat);
                else
                    KézKönyv.Módosítás(hely, jelszó, Adat);

                Könyv_szám.Text = "";
                Könyv_ürít();
                Szeszámkönyvfeltöltés();
                Lekérd_Szeszámkönyvfeltöltés();
                Könyv_tábla_író();
                MessageBox.Show("Az adatok rögzítése/módosítása megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Könyv_excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Könyv_tábla.Rows.Count <= 0) return;
                string fájlexc;


                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Szerszám_Könyv_{Program.PostásNév}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyE.DataGridViewToExcel(fájlexc, Könyv_tábla);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyE.Megnyitás(fájlexc);
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

        #region Lekérdezés
        private void Lekérd_Szeszámkönyvfeltöltés()
        {
            try
            {
                KönyvListaFeltöltés();
                List<Adat_Szerszám_Könyvtörzs> Adat = (from a in AdatokKönyv
                                                       where a.Státus == Lekérd_Töröltek.Checked
                                                       orderby a.Szerszámkönyvszám
                                                       select a).ToList();

                Lekérd_Szerszámkönyvszám.Items.Clear();
                Lekérd_Szerszámkönyvszám.BeginUpdate();

                foreach (Adat_Szerszám_Könyvtörzs A in Adat)
                {
                    Lekérd_Szerszámkönyvszám.Items.Add(A.Szerszámkönyvszám.ToStrTrim() + " = " + A.Szerszámkönyvnév.Trim());
                }
                Lekérd_Szerszámkönyvszám.EndUpdate();
                Lekérd_Szerszámkönyvszám.Refresh();
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
        private void Lekérd_Azonosítók()
        {
            try
            {
                Lekérd_Szerszámazonosító.Items.Clear();
                List<Adat_Szerszám_Cikktörzs> AdatokSzűrt;
                if (!Lekérd_Töröltek.Checked)
                    AdatokSzűrt = (from a in AdatokCikk
                                   where a.Státus == 0
                                   orderby a.Azonosító
                                   select a).ToList();
                else
                    AdatokSzűrt = (from a in AdatokCikk
                                   where a.Státus == 0
                                   orderby a.Azonosító
                                   select a).ToList();
                if (AdatokSzűrt != null)
                {
                    foreach (Adat_Szerszám_Cikktörzs elem in AdatokSzűrt)
                        Lekérd_Szerszámazonosító.Items.Add(elem.Azonosító);
                }
                Lekérd_Szerszámazonosító.Refresh();
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

        private void Lekérd_névfeltöltés()
        {
            try
            {
                Lekérd_Felelős1.Items.Clear();
                Lekérd_Felelős1.BeginUpdate();
                DolgozóListaFeltöltés();

                DateTime kilépett = new DateTime(1900, 1, 1);
                List<Adat_Dolgozó_Alap> Adatok = (from a in AdatokDolgozó
                                                  where a.Kilépésiidő == kilépett
                                                  select a).ToList();

                foreach (Adat_Dolgozó_Alap Adat in Adatok)
                    Lekérd_Felelős1.Items.Add(Adat.DolgozóNév + " = " + Adat.Dolgozószám);

                Lekérd_Felelős1.EndUpdate();
                Lekérd_Felelős1.Refresh();
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

        private void Lenyit_Click(object sender, EventArgs e)
        {
            Lekérd_Szerszámkönyvszám.Height = 500;
        }

        private void Visszacsuk_Click(object sender, EventArgs e)
        {
            Lekérd_Szerszámkönyvszám.Height = 25;
        }

        private void Összeskijelöl_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= Lekérd_Szerszámkönyvszám.Items.Count - 1; i++)
                Lekérd_Szerszámkönyvszám.SetItemChecked(i, true);
            Lekérd_táblaíró();
            Lekérd_Szerszámkönyvszám.Height = 25;
        }

        private void Mindtöröl_Click(object sender, EventArgs e)
        {
            Lekérd_mindtöröl_esemény();
        }

        private void Lekérd_mindtöröl_esemény()
        {
            for (int i = 0; i <= Lekérd_Szerszámkönyvszám.Items.Count - 1; i++)
                Lekérd_Szerszámkönyvszám.SetItemChecked(i, false);
            Lekérd_táblaíró();
            Lekérd_Szerszámkönyvszám.Height = 25;
        }

        private void Lekérd_táblaíró()
        {
            try
            {
                AdatTáblaLekérd.Clear();
                if (Lekérd_Szerszámkönyvszám.CheckedItems.Count < 1) return;

                Lekérd_Tábla.Visible = false;
                AdatTáblaLekérd.Clear();
                Lekérd_Tábla.CleanFilterAndSort();
                Lekérd_Tábla_Fejléc();
                Lekérd_Tábla_tartalom();
                Lekérd_Tábla.DataSource = AdatTáblaLekérd;
                Lekérd_Tábla_Szélesség();
                Lekérd_Tábla.Visible = true;
                Holtart.Ki();
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

        private void Lekérd_Tábla_Fejléc()
        {
            AdatTáblaLekérd.Columns.Clear();
            AdatTáblaLekérd.Columns.Add("Azonosító");
            AdatTáblaLekérd.Columns.Add("Megnevezés");
            AdatTáblaLekérd.Columns.Add("Méret");
            AdatTáblaLekérd.Columns.Add("Mennyiség");
            AdatTáblaLekérd.Columns.Add("Gyáriszám");
            AdatTáblaLekérd.Columns.Add("Dátum");
            AdatTáblaLekérd.Columns.Add("Könyvszám");
            AdatTáblaLekérd.Columns.Add("Könyv megnevezés");
        }

        private void Lekérd_Tábla_Szélesség()
        {
            Lekérd_Tábla.Columns["Azonosító"].Width = 180;
            Lekérd_Tábla.Columns["Megnevezés"].Width = 350;
            Lekérd_Tábla.Columns["Méret"].Width = 100;
            Lekérd_Tábla.Columns["Mennyiség"].Width = 100;
            Lekérd_Tábla.Columns["Mennyiség"].Width = 130;
            Lekérd_Tábla.Columns["Dátum"].Width = 100;
            Lekérd_Tábla.Columns["Könyvszám"].Width = 100;
            Lekérd_Tábla.Columns["Könyv megnevezés"].Width = 300;
        }

        private void Lekérd_Tábla_tartalom()
        {
            KönyvelésListaFeltöltés();
            CikktörzsListaFeltöltés();
            KönyvListaFeltöltés();

            for (int j = 0; j <= Lekérd_Szerszámkönyvszám.CheckedItems.Count - 1; j++)
            {

                string[] elem = Lekérd_Szerszámkönyvszám.CheckedItems[j].ToString().Split('=');
                string szerszámkönyszám = elem[0].Trim();

                List<Adat_Szerszám_Könyvelés> Adatok = (from a in AdatokKönyvelés
                                                        where a.SzerszámkönyvszámMás == szerszámkönyszám
                                                        select a).ToList();
                Holtart.Be(Adatok.Count + 2);
                foreach (Adat_Szerszám_Könyvelés A in Adatok)
                {
                    DataRow Soradat = AdatTáblaLekérd.NewRow();
                    Soradat["Azonosító"] = A.AzonosítóMás;
                    Soradat["Mennyiség"] = A.Mennyiség;
                    Soradat["Dátum"] = A.Dátum.ToString("yyyy.MM.dd");
                    Soradat["Könyvszám"] = A.SzerszámkönyvszámMás;
                    Adat_Szerszám_Cikktörzs ElemCikk = (from a in AdatokCikk
                                                        where a.Azonosító == A.AzonosítóMás
                                                        select a).FirstOrDefault();
                    if (ElemCikk != null)
                    {
                        Soradat["Megnevezés"] = ElemCikk.Megnevezés;
                        Soradat["Méret"] = ElemCikk.Méret.Trim();
                        Soradat["Gyáriszám"] = ElemCikk.Gyáriszám.Trim();
                    }
                    Adat_Szerszám_Könyvtörzs ElemKönyv = (from a in AdatokKönyv
                                                          where a.Szerszámkönyvszám == A.SzerszámkönyvszámMás
                                                          select a).FirstOrDefault();
                    if (ElemKönyv != null) Soradat["Könyv megnevezés"] = ElemKönyv.Szerszámkönyvnév.Trim();
                    AdatTáblaLekérd.Rows.Add(Soradat);
                    Holtart.Lép();
                }

            }
        }

        private void Lekérd_Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                Kép_Azonosító.Text = Lekérd_Tábla.Rows[e.RowIndex].Cells[0].Value.ToString();
                PDF_Azonosító.Text = Lekérd_Tábla.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }

        private void Jelöltszersz_Click(object sender, EventArgs e)
        {
            Lekérd_táblaíró();
            Lekérd_Szerszámkönyvszám.Height = 25;
        }

        private void Töröltek_CheckedChanged_2(object sender, EventArgs e)
        {
            Lekérd_Szeszámkönyvfeltöltés();
        }

        private void Excelclick_Click(object sender, EventArgs e)
        {
            try
            {
                if (Lekérd_Tábla.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Szerszám_Lekérdezés_{Program.PostásNév}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyE.DataGridViewToExcel(fájlexc, Lekérd_Tábla);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyE.Megnyitás(fájlexc);
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

        private void Nevekkiválasztása_Click(object sender, EventArgs e)
        {
            try
            {
                Lekérd_mindtöröl_esemény();

                Lekérd_Felelős1.Text.Trim();
                KönyvListaFeltöltés();

                List<Adat_Szerszám_Könyvtörzs> Adatok = (from a in AdatokKönyv
                                                         where a.Felelős1 == Lekérd_Felelős1.Text.Trim() ||
                                                        a.Felelős2 == Lekérd_Felelős1.Text.Trim()
                                                         select a).ToList();

                foreach (Adat_Szerszám_Könyvtörzs A in Adatok)
                {
                    for (int j = 0; j <= Lekérd_Szerszámkönyvszám.Items.Count - 1; j++)
                    {
                        string[] elem = Lekérd_Szerszámkönyvszám.Items[j].ToString().Split('=');
                        if (A.Szerszámkönyvszám == elem[0].Trim())
                        {
                            Lekérd_Szerszámkönyvszám.SetItemChecked(j, true);
                            break;
                        }
                    }
                }
                Lekérd_táblaíró();
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

        private void Szerszámazonosító_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Lekérd_Szerszámazonosító.Text.Trim() == "") return;

                string azonosító = Lekérd_Szerszámazonosító.Text.Trim().Length > 20 ? Lekérd_Szerszámazonosító.Text.Trim().Substring(0, 20).Trim() : Lekérd_Szerszámazonosító.Text.Trim();

                Adat_Szerszám_Cikktörzs ElemCikk = (from a in AdatokCikk
                                                    where a.Azonosító == azonosító
                                                    select a).FirstOrDefault();
                if (ElemCikk != null)
                {
                    Lekérd_Megnevezés.Text = ElemCikk.Megnevezés;
                    Lekérd_Méret.Text = ElemCikk.Méret;
                }
                else
                {
                    Lekérd_Megnevezés.Text = "";
                    Lekérd_Méret.Text = "";
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

        private void Anyagkiíró_Click(object sender, EventArgs e)
        {
            Lekérd_táblaíróanyagra();
        }

        private void Lekérd_táblaíróanyagra()
        {
            try
            {
                Lekérd_Tábla.Visible = false;
                AdatTáblaLekérd.Clear();
                Lekérd_Tábla.CleanFilterAndSort();
                Lekérd_Tábla_Fejléc();
                Lekérd_Tábla_tartalom_Anyag();
                Lekérd_Tábla.DataSource = AdatTáblaLekérd;
                Lekérd_Tábla_Szélesség();
                Lekérd_Tábla.Visible = true;
                Holtart.Ki();
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

        private void Lekérd_Tábla_tartalom_Anyag()
        {
            KönyvelésListaFeltöltés();
            CikktörzsListaFeltöltés();
            KönyvListaFeltöltés();

            List<Adat_Szerszám_Cikktörzs> Adatok = AdatokCikk;
            // ha van kijelölve azonosító akkor csak arra szűrünk.
            if (Lekérd_Szerszámazonosító.Text.Trim() != "")
                Adatok = (from a in Adatok
                          where a.Azonosító.Contains(Lekérd_Szerszámazonosító.Text.Trim()) &&
                          a.Státus == (Lekérd_Töröltek1.Checked ? 1 : 0)
                          select a).ToList();

            if (Radio_lek_A.Checked)
                Adatok = (from a in Adatok
                          where a.Azonosító.Substring(0, 1) == "A"
                          select a).ToList();

            if (Radio_lek_E.Checked)
                Adatok = (from a in Adatok
                          where a.Azonosító.Substring(0, 1) == "E"
                          select a).ToList();
            if (Lekérd_Megnevezés.Text.Trim() != "")
                Adatok = (from a in Adatok
                          where a.Megnevezés.Contains(Lekérd_Megnevezés.Text.Trim())
                          select a).ToList();
            if (Lekérd_Méret.Text.Trim() != "")
                Adatok = (from a in Adatok
                          where a.Méret.Contains(Lekérd_Méret.Text.Trim())
                          select a).ToList();
            if (Adatok == null) return;

            List<string> Cikkszámok = Adatok.Select(elem => elem.Azonosító).Distinct().ToList();


            Holtart.Be(Adatok.Count + 1);
            int Összeg = 0;
            List<Adat_Szerszám_Könyvelés> AdatokSzűrt = (from a in AdatokKönyvelés
                                                         where Cikkszámok.Contains(a.AzonosítóMás)
                                                         select a).ToList();

            foreach (Adat_Szerszám_Könyvelés rekord in AdatokSzűrt)
            {
                DataRow Soradat = AdatTáblaLekérd.NewRow();
                Soradat["Azonosító"] = rekord.AzonosítóMás;
                Soradat["Mennyiség"] = rekord.Mennyiség;
                Összeg += rekord.Mennyiség;
                Soradat["Dátum"] = rekord.Dátum.ToString("yyyy.MM.dd");
                Soradat["Könyvszám"] = rekord.SzerszámkönyvszámMás;
                Adat_Szerszám_Cikktörzs ElemCikk = (from a in AdatokCikk
                                                    where a.Azonosító == rekord.AzonosítóMás
                                                    select a).FirstOrDefault();
                if (ElemCikk != null)
                {
                    Soradat["Megnevezés"] = ElemCikk.Megnevezés;
                    Soradat["Méret"] = ElemCikk.Méret.Trim();
                    Soradat["Gyáriszám"] = ElemCikk.Gyáriszám.Trim();
                }
                Adat_Szerszám_Könyvtörzs ElemKönyv = (from a in AdatokKönyv
                                                      where a.Szerszámkönyvszám == rekord.SzerszámkönyvszámMás
                                                      select a).FirstOrDefault();
                if (ElemKönyv != null) Soradat["Könyv megnevezés"] = ElemKönyv.Szerszámkönyvnév.Trim();
                AdatTáblaLekérd.Rows.Add(Soradat);
                Holtart.Lép();
            }

            if (AdatTáblaLekérd.Rows.Count > 0)
            {
                DataRow Soradat = AdatTáblaLekérd.NewRow();
                Soradat["Megnevezés"] = "Összesen:";
                Soradat["Mennyiség"] = Összeg;
                AdatTáblaLekérd.Rows.Add(Soradat);
            }
            Lekérd_Tábla.Visible = true;
            Holtart.Ki();
        }

        private void Lekérd_Command1_Click(object sender, EventArgs e)
        {
            try
            {
                //leellenőrizük, hogy van-e kijelölve könyv
                if (Lekérd_Szerszámkönyvszám.SelectedItems.Count < 1) throw new HibásBevittAdat("Nincs kijelölve egy könyv sem.");

                // beolvassuk a három szervezeti egységet, és a beosztásokat
                string Szervezet1 = "";
                Adat_Kiegészítő_Jelenlétiív Szerv = (from a in AdatokJelenléti
                                                     where a.Id == 2
                                                     select a).FirstOrDefault();
                if (Szerv != null) Szervezet1 = Szerv.Szervezet;

                string Szervezet2 = "";
                Szerv = (from a in AdatokJelenléti
                         where a.Id == 3
                         select a).FirstOrDefault();
                if (Szerv != null) Szervezet2 = Szerv.Szervezet;

                string Szervezet3 = "";
                Szerv = (from a in AdatokJelenléti
                         where a.Id == 4
                         select a).FirstOrDefault();
                if (Szerv != null) Szervezet3 = Szerv.Szervezet;

                Lekérd_Szerszámkönyvszám.Height = 25;

                string fájlexc;

                Holtart.Be(Lekérd_Szerszámkönyvszám.Items.Count + 2);
                // táblázatba kilistázzuk a könyv tartalmát
                for (int j = 0; j <= Lekérd_Szerszámkönyvszám.Items.Count - 1; j++)
                {
                    if (Lekérd_Szerszámkönyvszám.GetItemChecked(j))
                    {
                        Lekérd_Tábla.Visible = false;
                        AdatTáblaLekérd.Clear();
                        Lekérd_Tábla_Fejléc();

                        string[] elem = Lekérd_Szerszámkönyvszám.Items[j].ToString().Split('=');
                        string szerszámkönyszám = elem[0].Trim();

                        List<Adat_Szerszám_Könyvelés> Adatok = (from a in AdatokKönyvelés
                                                                where a.SzerszámkönyvszámMás == szerszámkönyszám
                                                                select a).ToList();
                        Holtart.Be(Adatok.Count + 2);
                        foreach (Adat_Szerszám_Könyvelés A in Adatok)
                        {
                            DataRow Soradat = AdatTáblaLekérd.NewRow();
                            Soradat["Azonosító"] = A.AzonosítóMás;
                            Soradat["Mennyiség"] = A.Mennyiség;
                            Soradat["Dátum"] = A.Dátum.ToString("yyyy.MM.dd");
                            Soradat["Könyvszám"] = A.SzerszámkönyvszámMás;
                            Adat_Szerszám_Cikktörzs ElemCikk = (from a in AdatokCikk
                                                                where a.Azonosító == A.AzonosítóMás
                                                                select a).FirstOrDefault();
                            if (ElemCikk != null)
                            {
                                Soradat["Megnevezés"] = ElemCikk.Megnevezés;
                                Soradat["Méret"] = ElemCikk.Méret.Trim();
                                Soradat["Gyáriszám"] = ElemCikk.Gyáriszám.Trim();
                            }
                            Adat_Szerszám_Könyvtörzs ElemKönyv = (from a in AdatokKönyv
                                                                  where a.Szerszámkönyvszám == A.SzerszámkönyvszámMás
                                                                  select a).FirstOrDefault();
                            if (ElemKönyv != null) Soradat["Könyv megnevezés"] = ElemKönyv.Szerszámkönyvnév.Trim();
                            AdatTáblaLekérd.Rows.Add(Soradat);
                            Holtart.Lép();
                        }
                        Lekérd_Tábla.CleanFilterAndSort();
                        Lekérd_Tábla.DataSource = AdatTáblaLekérd;
                        Lekérd_Tábla_Szélesség();
                        Lekérd_Tábla.Visible = true;

                        // kiirt táblából készítünk excel táblát ha a címsoron kívül van tétel
                        if (Lekérd_Tábla.Rows.Count > 0)
                        {
                            // a fájlnév előkészítése
                            SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                            {
                                InitialDirectory = "MyDocuments",
                                FileName = "Szerszámköny_Leltár-" + Program.PostásNév + "-" + szerszámkönyszám.Trim() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss")
                            };

                            fájlexc = SaveFileDialog1.FileName;
                            Holtart.Lép();
                            // megnyitjuk az excelt
                            MyE.ExcelLétrehozás();
                            MyE.Munkalap_betű("Arial", 11);

                            MyE.Oszlopszélesség("Munka1", "A:A", 23);
                            MyE.Oszlopszélesség("Munka1", "B:B", 54);
                            MyE.Oszlopszélesség("Munka1", "C:D", 17);
                            MyE.Oszlopszélesség("Munka1", "E:E", 14);
                            MyE.Oszlopszélesség("Munka1", "F:F", 16);
                            MyE.Kiir(Szervezet1.Trim(), "a1");
                            MyE.Kiir(Szervezet2.Trim(), "a2");
                            MyE.Kiir(Szervezet3.Trim(), "a3");
                            MyE.Betű("a1:a3", false, false, true);
                            Holtart.Lép();
                            MyE.Egyesít("Munka1", "a5:f5");
                            MyE.Betű("a5", 16);
                            MyE.Betű("a5", false, false, true);
                            MyE.Kiir("Egyéni Szerszám nyilvántartó lap", "a5");
                            Holtart.Lép();
                            MyE.Egyesít("Munka1", "b7:E7");
                            MyE.Egyesít("Munka1", "b9:E9");
                            MyE.Egyesít("Munka1", "b11:E11");
                            MyE.Kiir("Könyvszám:", "a7");
                            MyE.Kiir("Könyv megnevezése:", "a9");
                            MyE.Kiir("Könyvért felelős", "a11");
                            Holtart.Lép();
                            // beírjuk a szerszámkönyv adatokat
                            Adat_Szerszám_Könyvtörzs Adat = (from a in AdatokKönyv
                                                             where a.Szerszámkönyvszám == szerszámkönyszám.Trim()
                                                             select a).FirstOrDefault();
                            MyE.Kiir(Adat.Szerszámkönyvszám, "b7");
                            MyE.Kiir(Adat.Szerszámkönyvnév, "b9");
                            MyE.Kiir(Adat.Felelős1, "b11");
                            MyE.Kiir(Adat.Felelős2, "b13");
                            Holtart.Lép();
                            // elkészítjük a fejlécet
                            MyE.Kiir("Nyilvántartásiszám:", "a15");
                            MyE.Kiir("Szerszám megnevezése:", "b15");
                            MyE.Kiir("Méret:", "c15");
                            MyE.Kiir("Gyáriszám:", "e15");
                            MyE.Kiir("Mennyiség:", "d15");
                            MyE.Kiir("Felvétel dátuma:", "f15");
                            // beírjuk a felvett szerszámokat
                            int sor = 16;
                            int oszlop;
                            {     // tartalom kiírása
                                for (sor = 0; sor <= Lekérd_Tábla.RowCount - 1; sor++)
                                {
                                    for (oszlop = 0; oszlop <= 5; oszlop++)
                                        MyE.Kiir(Lekérd_Tábla.Rows[sor].Cells[oszlop].Value.ToString(), MyE.Oszlopnév(oszlop + 1) + (sor + 16).ToString());
                                    Holtart.Lép();
                                }
                            }
                            sor = Lekérd_Tábla.Rows.Count + 15;

                            // keretezünk
                            MyE.Rácsoz($"a15:f{sor}");
                            MyE.Vastagkeret("a15:f15");
                            MyE.Vastagkeret($"a15:f{sor}");
                            sor += 2;
                            MyE.Kiir("Kelt:" + DateTime.Today.ToString("yyyy.MM.dd"), $"A{sor}");
                            sor += 2;
                            MyE.Kiir("A felsorolt szerszám(oka)t használatra felvettem.", $"A{sor}");
                            sor += 2;
                            MyE.Egyesít("Munka1", $"C{sor}:F{sor}");
                            MyE.Kiir("Dolgozó aláírása", $"C{sor}");
                            // pontozás az aláírásnak
                            MyE.Pontvonal($"C{sor}:F{sor}");
                            Holtart.Lép();
                            sor += 5;
                            MyE.Egyesít("Munka1", $"C{sor}:F{sor}");
                            MyE.Kiir("Raktáros", $"C{sor}");
                            // pontozás az aláírásnak
                            MyE.Pontvonal($"C{sor}:F{sor}");

                            // nyomtatási beállítások
                            MyE.NyomtatásiTerület_részletes("Munka1", $"a1:f{sor}", "", "", true);
                            // bezárjuk az Excel-t
                            MyE.Aktív_Cella("Munka1", "A1");
                            Holtart.Lép();
                            MyE.ExcelMentés(fájlexc);
                            MyE.ExcelBezárás();
                        }
                    }
                }
                Holtart.Ki();

                MessageBox.Show("A kívánt nyilvántartások kiírása megtörtént Excelbe!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        #region KÉP
        private void Képek_Azonosítók_feltöltése()
        {
            try
            {
                Kép_Azonosító.Items.Clear();
                List<Adat_Szerszám_Cikktörzs> AdatokSzűrt;
                if (!Alap_Töröltek.Checked)
                    AdatokSzűrt = (from a in AdatokCikk
                                   where a.Státus == 0
                                   orderby a.Azonosító
                                   select a).ToList();
                else
                    AdatokSzűrt = (from a in AdatokCikk
                                   where a.Státus == 1
                                   orderby a.Azonosító
                                   select a).ToList();
                if (AdatokSzűrt != null)
                {
                    foreach (Adat_Szerszám_Cikktörzs elem in AdatokSzűrt)
                        Kép_Azonosító.Items.Add(elem.Azonosító);
                }
                Kép_Azonosító.Refresh();
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

        private void Kép_Listázás_Click(object sender, EventArgs e)
        {
            PictureBox1.Image?.Dispose();
            Kép_azonísító_választó();
            Kép_lista_szűrés();
        }

        private void Képek_azonosító_SelectedIndexChanged(object sender, EventArgs e)
        {
            Kép_azonísító_választó();
        }

        private void Kép_azonísító_választó()
        {
            try
            {
                if (Kép_Azonosító.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve egy Azonosító sem.");

                Adat_Szerszám_Cikktörzs Elem = (from a in AdatokCikk
                                                where a.Azonosító == Kép_Azonosító.Text.Trim()
                                                select a).FirstOrDefault();
                if (Elem != null) Kép_megnevezés.Text = Elem.Megnevezés;

                Kép_lista_szűrés();
                PictureBox1.Image = null;
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

        private void Kép_lista_szűrés()
        {
            try
            {
                if (Kép_Azonosító.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve egy Azonosító sem.");

                Kép_listbox.Items.Clear();

                string helykép = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\{Könyvtár_adat}\Szerszám_képek";
                DirectoryInfo Directories = new System.IO.DirectoryInfo(helykép);

                string mialapján = $"{Kép_Azonosító.Text.Trim()}*.jpg";
                // ha nem üres

                System.IO.FileInfo[] fileInfo = Directories.GetFiles(mialapján, System.IO.SearchOption.AllDirectories);
                foreach (FileInfo file in fileInfo)
                    Kép_listbox.Items.Add(file.Name);
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

        private void Kép_btn_Click(object sender, EventArgs e)
        {
            try
            {
                Kép_Feltöltendő.Text = "";
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    Filter = "JPG Files |*.jpg"
                };
                if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Kép_Feltöltendő.Text = OpenFileDialog1.FileName;
                    Kép_megjelenítés();
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

        private void Kép_megjelenítés()
        {
            try
            {
                string helykép = Kép_Feltöltendő.Text.Trim();

                if (!File.Exists(helykép)) throw new HibásBevittAdat("Nincs kiválasztva egy kép sem.");
                Kezelő_Kép.KépMegnyitás(PictureBox1, helykép, toolTip1);
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

        private void Kép_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Kép_listbox.SelectedItems.Count == 0) return;

            Kép_Feltöltendő.Text = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\{Könyvtár_adat}\Szerszám_képek\" + Kép_listbox.SelectedItems[0].ToString();
            Kép_megjelenítés();
        }

        private void Kép_rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Kép_Azonosító.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve egy Azonosítós sem.");
                if (Kép_Feltöltendő.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve egy feltöltendő kép sem.");

                string helykép = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\{Könyvtár_adat}\Szerszám_képek";
                if (Directory.Exists(helykép) == false)
                {
                    // Megnézzük, hogy létezik-e a könyvtár, ha nem létrehozzuk
                    System.IO.Directory.CreateDirectory(helykép);
                }

                // A tervezett fájlnévnek megfelelően szűrjük a könyvtár tartalmát
                Kép_szűrés.Items.Clear();
                DirectoryInfo Directories = new System.IO.DirectoryInfo(helykép);

                string mialapján = $"{Kép_Azonosító.Text.Trim()}_*.jpg";

                System.IO.FileInfo[] fileInfo = Directories.GetFiles(mialapján, System.IO.SearchOption.AllDirectories);

                foreach (FileInfo file in fileInfo)
                    Kép_szűrés.Items.Add(file.Name);

                int i;
                if (fileInfo.Length < 1)
                    i = 1;
                else
                {
                    string[] darab = Kép_szűrés.Items[Kép_szűrés.Items.Count - 1].ToString().Split('_');
                    i = int.Parse(darab[1].Replace(".jpg", "")) + 1;
                }

                // átmásoljuk a fájl és átnevezzük
                string újfájlnév = $@"{helykép}\{Kép_Azonosító.Text.Trim()}_{i}.jpg";

                File.Copy(Kép_Feltöltendő.Text.Trim(), újfájlnév);
                File.Delete(Kép_Feltöltendő.Text.Trim());
                Kép_lista_szűrés();
                Kép_Feltöltendő.Text = "";
                MessageBox.Show("A Kép feltöltése elkészült!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void KépTörlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Kép_listbox.SelectedItems.Count < 1) throw new HibásBevittAdat("Nincs kijelölve egy kép sem.");
                if (Kép_listbox.SelectedItems[0].ToStrTrim() == "") throw new HibásBevittAdat("Nincs kijelölve egy kép sem.");
                if (MessageBox.Show("Biztos, hogy a töröljük a fájlt?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    string helykép = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\{Könyvtár_adat}\Szerszám_képek\";
                    File.Delete(helykép + Kép_listbox.SelectedItems[0].ToStrTrim());
                    Kép_lista_szűrés();
                    Kép_Feltöltendő.Text = "";
                    PictureBox1.Visible = false;
                    MessageBox.Show("Az kép törlése megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Mentés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Kép_listbox.SelectedItems.Count < 1) throw new HibásBevittAdat("Nincs kijelölve egy kép sem.");
                if (Kép_listbox.SelectedItems[0].ToStrTrim() == "") throw new HibásBevittAdat("Nincs kijelölve egy kép sem.");

                string helykép = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\{Könyvtár_adat}\Szerszám_képek\";
                if (!Directory.Exists(helykép)) throw new HibásBevittAdat("A tárhely nem létezik.");

                string hova = "";
                FolderBrowserDialog FolderBrowserDialog1 = new FolderBrowserDialog();
                if (FolderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    DirectoryInfo di = new DirectoryInfo(FolderBrowserDialog1.SelectedPath);
                    hova = FolderBrowserDialog1.SelectedPath;
                }
                if (hova.Trim() == "") throw new HibásBevittAdat("Nincs hova menteni a kiválaszott képet.");


                for (int i = 0; i <= Kép_listbox.SelectedItems.Count - 1; i++)
                    File.Copy(helykép + Kép_listbox.SelectedItems[0].ToStrTrim(), hova + @"\" + Kép_listbox.SelectedItems[i].ToStrTrim());

                MessageBox.Show("A Kép(ek) másolása megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        #region PDF lapofül
        private void PDF_Azonosítók_feltöltése()
        {
            try
            {
                PDF_Azonosító.Items.Clear();
                List<Adat_Szerszám_Cikktörzs> AdatokSzűrt;
                if (!Alap_Töröltek.Checked)
                    AdatokSzűrt = (from a in AdatokCikk
                                   where a.Státus == 0
                                   orderby a.Azonosító
                                   select a).ToList();
                else
                    AdatokSzűrt = (from a in AdatokCikk
                                   where a.Státus == 0
                                   orderby a.Azonosító
                                   select a).ToList();
                if (AdatokSzűrt != null)
                {
                    foreach (Adat_Szerszám_Cikktörzs elem in AdatokSzűrt)
                        PDF_Azonosító.Items.Add(elem.Azonosító);
                }
                PDF_Azonosító.Refresh();
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

        private void PDF_lista_szűrés()
        {
            try
            {
                if (PDF_Azonosító.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve egy szonosító sem.");

                Pdf_listbox.Items.Clear();

                string helypdf = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\{Könyvtár_adat}\Szerszám_PDF";
                var Directories = new System.IO.DirectoryInfo(helypdf);

                string mialapján = PDF_Azonosító.Text.Trim() + "_*.pdf";
                // ha nem üres

                FileInfo[] fileInfo = Directories.GetFiles(mialapján, SearchOption.AllDirectories);
                foreach (FileInfo file in fileInfo)
                    Pdf_listbox.Items.Add(file.Name);
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

        private void PDF_Frissít_Click(object sender, EventArgs e)
        {
            PDF_lista_szűrés();
        }

        private void BtnPDF_Click(object sender, EventArgs e)
        {
            try
            {
                Feltöltendő.Text = "";
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    Filter = "PDF Files |*.pdf"
                };
                if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Kezelő_Pdf.PdfMegnyitás(PDF_néző, OpenFileDialog1.FileName);
                    Feltöltendő.Text = OpenFileDialog1.FileName;
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

        private void PDF_rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (PDF_Azonosító.Text.Trim() == "") throw new HibásBevittAdat("Nincs megadva az azonosító.");
                if (Feltöltendő.Text.Trim() == "") throw new HibásBevittAdat("Nincs feltöltendő fájl.");

                string helypdf = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\{Könyvtár_adat}\Szerszám_PDF";
                if (!Directory.Exists(helypdf))
                {
                    // Megnézzük, hogy létezik-e a könyvtár, ha nem létrehozzuk
                    Directory.CreateDirectory(helypdf);
                }

                // A tervezett fájlnévnek megfelelően szűrjük a könyvtár tartalmát
                Szűrés.Items.Clear();
                DirectoryInfo Directories = new System.IO.DirectoryInfo(helypdf);

                string mialapján = PDF_Azonosító.Text.Trim() + "_*.pdf";

                FileInfo[] fileInfo = Directories.GetFiles(mialapján, SearchOption.AllDirectories);

                foreach (FileInfo file in fileInfo)
                    Szűrés.Items.Add(file.Name);

                int i;

                if (fileInfo.Length < 1)
                    i = 1;
                else
                {
                    string[] darab = Szűrés.Items[Szűrés.Items.Count - 1].ToString().Split('_');
                    i = int.Parse(darab[1].Replace(".pdf", "")) + 1;
                }

                //létrehozzuk az új fájlnevet és átmásoljuk a tárhelyre
                string újfájlnév = helypdf + @"\" + PDF_Azonosító.Text.Trim() + "_" + i.ToString() + ".pdf";

                File.Copy(Feltöltendő.Text.Trim(), újfájlnév);
                //kitöröljük a feltöltendő fájlt
                File.Delete(Feltöltendő.Text.Trim());
                Feltöltendő.Text = "";

                PDF_lista_szűrés();

                MessageBox.Show("A PDF feltöltése elkészült!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void PDF_törlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Pdf_listbox.SelectedItems.Count < 1) throw new HibásBevittAdat("Nincs kijelölve egy elem sem.");
                if (Pdf_listbox.SelectedItems[0].ToStrTrim() == "") throw new HibásBevittAdat("Nincs kijelölve egy elem sem.");

                if (MessageBox.Show("Biztos, hogy a töröljük a fájlt?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    string helypdf = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\{Könyvtár_adat}\Szerszám_pdf\";
                    File.Delete(helypdf + Pdf_listbox.SelectedItems[0].ToStrTrim());
                    // igent választottuk
                    PDF_lista_szűrés();
                    Feltöltendő.Text = "";
                    PDF_néző.Visible = false;
                    MessageBox.Show("A PDF fájl törlése megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void PDF_Azonosító_SelectedIndexChanged(object sender, EventArgs e)
        {
            PDF_lista_szűrés();
            PDF_azonísító_választó();
            PDF_néző.Visible = false;
        }

        private void PDF_azonísító_választó()
        {
            try
            {
                if (PDF_Azonosító.Text.Trim() == "") return;

                Adat_Szerszám_Cikktörzs Elem = (from a in AdatokCikk
                                                where a.Megnevezés == PDF_Azonosító.Text.Trim()
                                                select a).FirstOrDefault();
                if (Elem != null) PDF_megnevezés.Text = Elem.Megnevezés;
                PDF_lista_szűrés();
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

        private void Pdf_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Pdf_listbox.SelectedItems.Count == 0) return;
                string helypdf = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\{Könyvtár_adat}\Szerszám_PDF\" + Pdf_listbox.SelectedItems[0].ToString();
                Kezelő_Pdf.PdfMegnyitás(PDF_néző, helypdf);
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

        #region Napló lapfül
        private void Napló_könyv_feltöltés()
        {
            try
            {
                KönyvListaFeltöltés();
                List<Adat_Szerszám_Könyvtörzs> Adatok = (from a in AdatokKönyv
                                                         where a.Státus == false
                                                         select a).ToList();
                Napló_Honnan.Items.Clear();
                Napló_Honnan.Items.Add("");
                Napló_Hova.Items.Clear();
                Napló_Hova.Items.Add("");
                Napló_Honnannév.Items.Clear();
                Napló_Honnannév.Items.Add("");
                Napló_Hovánév.Items.Clear();
                Napló_Hovánév.Items.Add("");

                foreach (Adat_Szerszám_Könyvtörzs elem in Adatok)
                {
                    Napló_Honnan.Items.Add(elem.Szerszámkönyvszám);
                    Napló_Hova.Items.Add(elem.Szerszámkönyvszám);
                    Napló_Honnannév.Items.Add(elem.Szerszámkönyvnév);
                    Napló_Hovánév.Items.Add(elem.Szerszámkönyvnév);
                }
                Napló_Hova.Refresh();
                Napló_Honnannév.Refresh();
                Napló_Honnan.Refresh();
                Napló_Hovánév.Refresh();
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

        private void Napló_táblaíró()
        {
            try
            {
                Napló_Tábla.Visible = false;
                AdatTáblaNapló.Clear();
                Napló_Tábla.CleanFilterAndSort();
                Napló_Fejléc();
                Napló_Tartalom();
                Napló_Tábla.DataSource = AdatTáblaNapló;
                Napló_Tábla_Szélesség();
                Napló_Tábla.Visible = true;
                Napló_Tábla.Refresh();
                Napló_Tábla.ClearSelection();

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

        private void Napló_Tábla_Szélesség()
        {
            Napló_Tábla.Columns["Azonosító"].Width = 180;
            Napló_Tábla.Columns["Megnevezés"].Width = 300;
            Napló_Tábla.Columns["Méret"].Width = 100;
            Napló_Tábla.Columns["Mennyiség"].Width = 100;
            Napló_Tábla.Columns["Gyáriszám"].Width = 130;
            Napló_Tábla.Columns["Honnan"].Width = 100;
            Napló_Tábla.Columns["Hova"].Width = 100;
            Napló_Tábla.Columns["Módosította"].Width = 120;
            Napló_Tábla.Columns["Mód. dátum"].Width = 180;
        }

        private void Napló_Fejléc()
        {
            AdatTáblaNapló.Columns.Clear();
            AdatTáblaNapló.Columns.Add("Azonosító");
            AdatTáblaNapló.Columns.Add("Megnevezés");
            AdatTáblaNapló.Columns.Add("Méret");
            AdatTáblaNapló.Columns.Add("Mennyiség");
            AdatTáblaNapló.Columns.Add("Gyáriszám");
            AdatTáblaNapló.Columns.Add("Honnan");
            AdatTáblaNapló.Columns.Add("Hova");
            AdatTáblaNapló.Columns.Add("Módosította");
            AdatTáblaNapló.Columns.Add("Mód. dátum");
        }

        private void Napló_Tartalom()
        {
            NaplóListaFeltöltés(Napló_Dátumtól.Value);

            List<Adat_Szerszám_Napló> Adatok = (from a in AdatokNapló
                                                where a.Módosításidátum > Napló_Dátumtól.Value.AddDays(-1) &&
                                                a.Módosításidátum < Napló_Dátumig.Value.AddDays(1)
                                                orderby a.Azonosító
                                                select a).ToList();

            if (Napló_Honnan.Text.Trim() != "" && Adatok != null && Adatok.Count > 0)
                Adatok = (from a in Adatok
                          where a.Honnan == Napló_Honnan.Text.Trim()
                          select a).ToList();

            if (Napló_Hova.Text.Trim() != "" && Adatok != null && Adatok.Count > 0)
                Adatok = (from a in Adatok
                          where a.Hova == Napló_Hova.Text.Trim()
                          select a).ToList();

            CikktörzsListaFeltöltés();

            Holtart.Be(Adatok.Count + 1);


            foreach (Adat_Szerszám_Napló a in Adatok)
            {
                DataRow Soradat = AdatTáblaNapló.NewRow();
                Soradat["Megnevezés"] = "";
                Soradat["Méret"] = "";
                Soradat["Gyáriszám"] = "";
                Adat_Szerszám_Cikktörzs rekord = AdatokCikk.Where(obj => obj.Azonosító == a.Azonosító).FirstOrDefault();
                if (rekord != null)
                {
                    Soradat["Megnevezés"] = rekord.Megnevezés;
                    Soradat["Méret"] = rekord.Méret;
                    Soradat["Gyáriszám"] = rekord.Gyáriszám;
                }

                Soradat["Azonosító"] = a.Azonosító.Trim();
                Soradat["Mennyiség"] = a.Mennyiség;
                Soradat["Honnan"] = a.Honnan.Trim(); ;
                Soradat["Hova"] = a.Hova.Trim();
                Soradat["Módosította"] = a.Módosította.Trim();
                Soradat["Mód. dátum"] = a.Módosításidátum.ToString("yyyy.MM.dd HH:mm:ss");
                AdatTáblaNapló.Rows.Add(Soradat);
                Holtart.Lép();
            }
            Holtart.Ki();
        }

        private void Listáz_Click(object sender, EventArgs e)
        {
            Napló_táblaíró();
        }

        private void Dátumtól_ValueChanged(object sender, EventArgs e)
        {
            if (Napló_Dátumtól.Value > Napló_Dátumig.Value)
                Napló_Dátumig.Value = Napló_Dátumtól.Value;
        }

        private void Dátumig_ValueChanged(object sender, EventArgs e)
        {
            if (Napló_Dátumtól.Value > Napló_Dátumig.Value)
                Napló_Dátumtól.Value = Napló_Dátumig.Value;
        }

        private void Excel_gomb_Click(object sender, EventArgs e)
        {
            try
            {
                if (Napló_Tábla.Rows.Count <= 0)
                    return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Szerszám_Napló_{Program.PostásNév}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyE.DataGridViewToExcel(fájlexc, Napló_Tábla);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyE.Megnyitás(fájlexc);
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

        private void Honnannév_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Napló_Honnannév.Text.Trim() == "") return;
            Napló_Honnan.Text = "";
            Adat_Szerszám_Könyvtörzs ElemKönyv = (from a in AdatokKönyv
                                                  where a.Szerszámkönyvnév == Napló_Honnannév.Text.Trim()
                                                  select a).FirstOrDefault();
            if (ElemKönyv != null) Napló_Honnan.Text = ElemKönyv.Szerszámkönyvszám;
        }

        private void Hovánév_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Napló_Hovánév.Text.Trim() == "") return;
            Napló_Hova.Text = "";
            Adat_Szerszám_Könyvtörzs ElemKönyv = (from a in AdatokKönyv
                                                  where a.Szerszámkönyvnév == Napló_Hovánév.Text.Trim()
                                                  select a).FirstOrDefault();
            if (ElemKönyv != null) Napló_Hova.Text = ElemKönyv.Szerszámkönyvszám;
        }

        private void Napló_Honnan_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Napló_Honnan.Text.Trim() == "") return;
            Napló_Honnannév.Text = "";
            Adat_Szerszám_Könyvtörzs ElemKönyv = (from a in AdatokKönyv
                                                  where a.Szerszámkönyvszám == Napló_Honnan.Text.Trim()
                                                  select a).FirstOrDefault();
            if (ElemKönyv != null)
                Napló_Honnannév.Text = ElemKönyv.Szerszámkönyvnév;

            Napló_táblaíró();
        }

        private void Napló_Hova_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Napló_Hova.Text.Trim() == "") return;
            Napló_Hovánév.Text = "";
            Adat_Szerszám_Könyvtörzs ElemKönyv = (from a in AdatokKönyv
                                                  where a.Szerszámkönyvszám == Napló_Hova.Text.Trim()
                                                  select a).FirstOrDefault();
            if (ElemKönyv != null)
                Napló_Hovánév.Text = ElemKönyv.Szerszámkönyvnév;

            Napló_táblaíró();
        }

        private void Nyomtatvány_Click(object sender, EventArgs e)
        {
            try
            {
                // megvizsgáljuk, hogy a feltételeknek megfelel
                if (Napló_Honnan.Text.Trim() == "" || Napló_Hova.Text.Trim() == "") throw new HibásBevittAdat("A Honnan, vagy a Hova mező nincs kitöltve,ezért nem készül nyomtatványt!");

                // ha van kijelölve sor akkor tovább megy
                if (Napló_Tábla.SelectedRows.Count < 1) throw new HibásBevittAdat("Nincs kijelölve egy sor sem,ezért nem készül nyomtatványt!");

                // melyik eset áll fenn?
                int eset = 0;
                string milyenkönyv = "";
                if (Napló_Honnan.Text.Trim() == "Raktár")
                {
                    eset = 1;
                    milyenkönyv = Napló_Hova.Text.Trim();
                }
                if (Napló_Hova.Text.Trim() == "Raktár")
                {
                    eset = 2;
                    milyenkönyv = Napló_Honnan.Text.Trim();
                }
                if (Napló_Hova.Text.Trim() == "Selejtre")
                {
                    eset = 3;
                    milyenkönyv = Napló_Honnan.Text.Trim();
                }

                if (eset == 0) throw new HibásBevittAdat("Program használati hiba miatt nem készül nyomtatványt!");

                // létrehozzuk az excel táblát
                string fájlexc;

                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Szerszám felvételi nyomtatvány készítés",
                    FileName = $"Szerszám_bizonylat_{Program.PostásNév}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép

                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                // megnyitjuk az excelt
                MyE.ExcelLétrehozás();
                MyE.Munkalap_betű("Arial", 11);


                // beolvassuk a három szervezeti egységet, és a beosztásokat
                string szervezet3 = "";
                Adat_Kiegészítő_Jelenlétiív Szerv = (from a in AdatokJelenléti
                                                     where a.Id == 2
                                                     select a).FirstOrDefault();
                if (Szerv != null) szervezet3 = Szerv.Szervezet;

                string szervezet4 = "";
                Szerv = (from a in AdatokJelenléti
                         where a.Id == 3
                         select a).FirstOrDefault();
                if (Szerv != null) szervezet4 = Szerv.Szervezet;

                string szervezet5 = "";
                Szerv = (from a in AdatokJelenléti
                         where a.Id == 4
                         select a).FirstOrDefault();
                if (Szerv != null) szervezet5 = Szerv.Szervezet;

                // Szervezeti kiírások
                MyE.Oszlopszélesség("Munka1", "a:a", 23);
                MyE.Oszlopszélesség("Munka1", "b:b", 54);
                MyE.Oszlopszélesség("Munka1", "c:d", 17);
                MyE.Oszlopszélesség("Munka1", "e:e", 14);
                MyE.Kiir(szervezet3, "a1");
                MyE.Kiir(szervezet4, "a2");
                MyE.Kiir(szervezet5, "a3");
                MyE.Betű("a1:a3", false, false, true);

                MyE.Egyesít("Munka1", "a5:E5");
                MyE.Betű("a5", 16);
                MyE.Betű("a5", false, false, true);
                switch (eset)
                {
                    case 1:
                        {
                            MyE.Kiir("Bizonylat a Szerszám felvételről", "a5");
                            break;
                        }
                    case 2:
                        {
                            MyE.Kiir("Bizonylat a Szerszám leadásáról", "a5");
                            break;
                        }
                    case 3:
                        {
                            MyE.Kiir("Bizonylat a selejtessévált Szerszám leadásáról", "a5");
                            break;
                        }
                }
                MyE.Egyesít("Munka1", "b7:E7");
                MyE.Egyesít("Munka1", "b9:E9");
                MyE.Egyesít("Munka1", "b11:E11");
                MyE.Kiir("Könyvszám:", "a7");
                MyE.Kiir("Könyv megnevezése:", "a9");
                MyE.Kiir("Könyvért felelős", "a11");

                // beírjuk a védőkönyv adatokat
                Adat_Szerszám_Könyvtörzs ElemKönyv = (from a in AdatokKönyv
                                                      where a.Szerszámkönyvszám == milyenkönyv
                                                      select a).FirstOrDefault();
                if (ElemKönyv != null)
                {
                    MyE.Kiir(ElemKönyv.Szerszámkönyvszám, "b7");
                    MyE.Kiir(ElemKönyv.Szerszámkönyvnév, "b9");
                    MyE.Kiir(ElemKönyv.Felelős1, "b11");
                    MyE.Kiir(ElemKönyv.Felelős2, "b13");
                }


                // elkészítjük a fejlécet
                MyE.Kiir("Nyilvántartásiszám:", "a15");
                MyE.Kiir("Szerszám megnevezése:", "b15");
                MyE.Kiir("Méret:", "c15");
                MyE.Kiir("Gyáriszám:", "d15");
                MyE.Kiir("Mennyiség:", "e15");
                // beírjuk a felvett szerszámokat
                int sor = 16;
                int hanyadik = 0;
                Holtart.Be(Napló_Tábla.Rows.Count + 2);
                for (int j = 0; j <= Napló_Tábla.Rows.Count - 1; j++)
                {
                    if (Napló_Tábla.Rows[j].Selected)
                    {
                        // ha ki van jelölve
                        MyE.Kiir(Napló_Tábla.Rows[j].Cells[1].Value.ToStrTrim(), $"B{sor}");
                        MyE.Kiir(Napló_Tábla.Rows[j].Cells[3].Value.ToString(), $"E{sor}");
                        MyE.Kiir(Napló_Tábla.Rows[j].Cells[0].Value.ToString(), $"A{sor}");
                        if (Napló_Tábla.Rows[j].Cells[2].Value.ToStrTrim() != "0")
                        {
                            MyE.Kiir(Napló_Tábla.Rows[j].Cells[2].Value.ToStrTrim(), $"C{sor}");
                        }
                        else
                        {
                            MyE.Kiir("-", $"C{sor}");
                        }
                        if (Napló_Tábla.Rows[j].Cells[4].Value.ToStrTrim() != "0")
                        {
                            MyE.Kiir(Napló_Tábla.Rows[j].Cells[4].Value.ToStrTrim(), $"D{sor}");
                        }
                        else
                        {
                            MyE.Kiir("-", $"D{sor}");
                        }
                        sor += 1;
                        hanyadik += 1;
                    }
                    Holtart.Lép();
                }

                // keretezünk
                MyE.Rácsoz($"a15:e{sor}");
                MyE.Vastagkeret("a15:e15");
                MyE.Vastagkeret($"a15:e{sor}");
                sor += 2;
                MyE.Kiir($"Kelt:{DateTime.Now:yyyy.MM.dd}", $"A{sor}");
                sor += 2;
                switch (eset)
                {
                    case 1:
                        {
                            MyE.Kiir("A felsorolt Szerszám(okat) használatra felvettem.", $"A{sor}");
                            break;
                        }
                    case 2:
                        {
                            MyE.Kiir("A felsorolt Szerszám(okat) selejtezés / javítás céljából / tovább használatra leadtam.", $"A{sor}");
                            break;
                        }
                    case 3:
                        {
                            MyE.Kiir("A felsorolt Szerszám(okat) selejtezés / javítás céljából leadtam.", $"A{sor}");
                            break;
                        }
                }
                sor += 2;
                MyE.Egyesít("Munka1", $"C{sor}:E{sor}");
                MyE.Kiir("Dolgozó aláírása", $"C{sor}");
                // pontozás az aláírásnak
                MyE.Pontvonal($"C{sor}:E{sor}");

                sor += 2;
                switch (eset)
                {
                    case 1:
                        {
                            MyE.Kiir("A dolgozónak kiadtam  a felsorolt szerszámo(ka)t.", $"A{sor}");
                            break;
                        }
                    case 2:
                        {
                            MyE.Kiir("A dolgozótól visszavettem a fenn felsorolt szerszámo(ka)t.", $"A{sor}");
                            break;
                        }
                    case 3:
                        {
                            MyE.Kiir("A dolgozótól visszavettem a fenn felsorolt szerszámo(ka)t.", $"A{sor}");
                            break;
                        }
                }

                sor += 2;
                MyE.Egyesít("Munka1", $"C{sor}:E{sor}");
                MyE.Kiir("Raktáros", $"C{sor}");
                // pontozás az aláírásnak
                MyE.Pontvonal($"C{sor}:E{sor}");

                if (eset == 3)
                {
                    sor += 2;
                    MyE.Egyesít("Munka1", $"A{sor}:E{sor}");
                    MyE.Kiir("A leadott szerszámo(ka)t megvizsgáltam és megállapítottam ,hogy a", $"A{sor}");
                    sor += 2;
                    MyE.Egyesít("Munka1", $"A{sor}:E{sor}");
                    MyE.Kiir("kártérítési felelősség fenn áll.         /      kártérítési felelősséggel a dolgozó nem tartozik.", $"A{sor}");
                    sor += 2;
                    MyE.Egyesít("Munka1", $"C{sor}:E{sor}");
                    MyE.Kiir("Munkahelyivezető", $"C{sor}");
                    // pontozás az aláírásnak
                    MyE.Pontvonal($"C{sor}:E{sor}");
                }
                // nyomtatási beállítások
                MyE.NyomtatásiTerület_részletes("Munka1", $"A1:E{sor}", "", "", true);

                if (Napló_Nyomtat.Checked)
                {
                    MyE.Nyomtatás("Munka1", 1, 1);
                    MessageBox.Show("A bizonylatok nyomtatása elkészült.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // bezárjuk az Excel-t
                MyE.Aktív_Cella("Munka1", "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();

                if (Napló_Fájltöröl.Checked)
                    File.Delete(fájlexc + ".xlsx");
                else
                {
                    MyE.Megnyitás(fájlexc);
                    MessageBox.Show("A bizonylat elkészült.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Holtart.Ki();
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

        #region Könyvelés lapfül   
        private void Honnan_feltöltések()
        {
            try
            {
                KönyvListaFeltöltés();
                List<Adat_Szerszám_Könyvtörzs> Adatok = (from a in AdatokKönyv
                                                         where a.Státus == false
                                                         orderby a.Szerszámkönyvszám
                                                         select a).ToList();

                Honnan.Items.Clear();
                // Honnan.Items.Add("")
                Honnan.BeginUpdate();
                foreach (Adat_Szerszám_Könyvtörzs rekord in Adatok)
                    Honnan.Items.Add(rekord.Szerszámkönyvszám);
                Honnan.EndUpdate();
                Honnan.Refresh();

                HonnanNév.Items.Clear();
                // HonnanNév.Items.Add("")
                HonnanNév.BeginUpdate();
                foreach (Adat_Szerszám_Könyvtörzs rekord in Adatok)
                    HonnanNév.Items.Add(rekord.Szerszámkönyvnév);
                HonnanNév.EndUpdate();
                HonnanNév.Refresh();
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

        private void Hova_feltöltések()
        {
            try
            {
                KönyvListaFeltöltés();
                List<Adat_Szerszám_Könyvtörzs> Adatok = (from a in AdatokKönyv
                                                         where a.Státus == false
                                                         orderby a.Szerszámkönyvszám
                                                         select a).ToList();
                Hova.Items.Clear();
                // Hova.Items.Add("")
                Hova.BeginUpdate();
                foreach (Adat_Szerszám_Könyvtörzs rekord in Adatok)
                    Hova.Items.Add(rekord.Szerszámkönyvszám);
                Hova.EndUpdate();
                Hova.Refresh();

                HováNév.Items.Clear();
                // HováNév.Items.Add("")
                HováNév.BeginUpdate();
                foreach (Adat_Szerszám_Könyvtörzs rekord in Adatok)
                    HováNév.Items.Add(rekord.Szerszámkönyvnév);
                HováNév.EndUpdate();
                HováNév.Refresh();
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

        private void HonnanNév_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                Honnan_kiíró_kszám();
                Hova.Enabled = true;
                HováNév.Enabled = true;
                Hova.Items.Clear();
                HováNév.Items.Clear();
                HonnanMennyiség.Text = 0.ToString();
                HováMennyiség.Text = 0.ToString();
                switch (HonnanNév.Text.Trim())
                {
                    case "Új védőeszköz beérkeztetése":
                        {
                            // betölti a teljes választék listát
                            Rögzítés_azonosítók();

                            Hova.Text = "Raktár";
                            HováNév.Text = "Szerszámraktárban lévő anyagok és eszközök";
                            Hova.Enabled = false;
                            HováNév.Enabled = false;
                            break;
                        }
                    case "Szerszámraktárban lévő anyagok és eszközök":
                        {
                            Azonosítóhelyen();
                            Hova.Text = "";
                            HováNév.Text = "";
                            Hova_feltöltések();
                            // ide nem lehet könyvelni
                            Hova.Items.Remove("Selejt");
                            HováNév.Items.Remove("Leselejtezett");
                            break;
                        }
                    case "Leselejtezett":
                        {
                            Azonosítóhelyen();
                            Hova.Text = "Selejtre";
                            HováNév.Text = "Selejtezésre előkészítés";
                            Hova.Enabled = false;
                            HováNév.Enabled = false;
                            break;
                        }

                    case "Selejtezésre előkészítés":
                        {
                            Azonosítóhelyen();
                            Hova.Items.Add("Raktár");
                            Hova.Items.Add("Selejt");
                            HováNév.Items.Add("Szerszámraktárban lévő anyagok és eszközök");
                            HováNév.Items.Add("Leselejtezett");
                            break;
                        }

                    default:
                        {
                            Azonosítóhelyen();
                            Hova.Text = "Raktár";
                            HováNév.Text = "Szerszámraktárban lévő anyagok és eszközök";
                            Hova.Enabled = false;
                            HováNév.Enabled = false;
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

        private void Rögzítés_azonosítók()
        {
            try
            {
                CikktörzsListaFeltöltés();
                SzerszámAzonosító.Items.Clear();

                List<Adat_Szerszám_Cikktörzs> AdatokSzűrt = (from a in AdatokCikk
                                                             where a.Státus == 0
                                                             orderby a.Azonosító
                                                             select a).ToList();

                if (AdatokSzűrt != null)
                {
                    foreach (Adat_Szerszám_Cikktörzs elem in AdatokSzűrt)
                        SzerszámAzonosító.Items.Add(elem.Azonosító);
                }
                SzerszámAzonosító.Refresh();

                SzerszámAzonosító.Text = "";
                Megnevezés.Text = "";
                Mennyiség.Text = "";

                Könyvelés_Tábla_író();
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

        private void Könyvelés_Tábla_író()
        {
            try
            {
                if (Honnan.Text.Trim() == "") return;
                KönyvListaFeltöltés();
                CikktörzsListaFeltöltés();
                KönyvelésListaFeltöltés();

                Könyvelés_tábla.Visible = false;

                List<string> Cikkszámok;
                if (Honnan.Text.Trim().Trim() != "Érkezett")
                    Cikkszámok = (from a in AdatokKönyvelés
                                  where a.SzerszámkönyvszámMás == Honnan.Text.Trim()
                                  select a.AzonosítóMás).ToList();
                else
                    Cikkszámok = (from a in AdatokCikk
                                  where a.Státus == 0
                                  select a.Azonosító).ToList();

                if (Radio_könyv_A.Checked)
                    Cikkszámok = (from a in Cikkszámok
                                  where a.Substring(0, 1) == "A"
                                  select a).ToList();
                else if (Radio_könyv_E.Checked)
                    Cikkszámok = (from a in Cikkszámok
                                  where a.Substring(0, 1) == "E"
                                  select a).ToList();

                List<Adat_Szerszám_Cikktörzs> Adatok = (from a in AdatokCikk
                                                        where Cikkszámok.Contains(a.Azonosító)
                                                        select a).ToList();

                if (Könyvelés_Méret.Text.Trim() != "" && Adatok != null && Adatok.Count > 0)
                    Adatok = (from a in Adatok
                              where a.Méret.Contains(Könyvelés_Méret.Text.Trim())
                              select a).ToList();

                if (Könyvelés_megnevezés.Text.Trim() != "" && Adatok != null && Adatok.Count > 0)
                    Adatok = (from a in Adatok
                              where a.Megnevezés.Contains(Könyvelés_megnevezés.Text.Trim())
                              select a).ToList();

                AdatTáblaKönyvelés.Columns.Clear();
                AdatTáblaKönyvelés.Columns.Add("Azonosító");
                AdatTáblaKönyvelés.Columns.Add("Megnevezés");
                AdatTáblaKönyvelés.Columns.Add("Méret");
                AdatTáblaKönyvelés.Columns.Add("Mennyiség");
                AdatTáblaKönyvelés.Columns.Add("Gyáriszám");

                Holtart.Be(Cikkszámok.Count + 1);

                AdatTáblaKönyvelés.Clear();

                foreach (Adat_Szerszám_Cikktörzs rekord in Adatok)
                {
                    DataRow Soradat = AdatTáblaKönyvelés.NewRow();

                    Soradat["Azonosító"] = rekord.Azonosító;
                    if (Honnan.Text.Trim().Trim() != "Érkezett")
                    {
                        Adat_Szerszám_Könyvelés ElemKönyvelés = (from a in AdatokKönyvelés
                                                                 where a.SzerszámkönyvszámMás == Honnan.Text.Trim() &&
                                                                 a.AzonosítóMás == rekord.Azonosító
                                                                 select a).FirstOrDefault();
                        if (ElemKönyvelés != null) Soradat["Mennyiség"] = ElemKönyvelés.Mennyiség;
                    }
                    else
                        Soradat["Mennyiség"] = 1;

                    Soradat["Megnevezés"] = rekord.Megnevezés;
                    Soradat["Méret"] = rekord.Méret;
                    Soradat["Gyáriszám"] = rekord.Gyáriszám;

                    AdatTáblaKönyvelés.Rows.Add(Soradat);

                    Holtart.Lép();
                }
                Könyvelés_tábla.CleanFilterAndSort();
                Könyvelés_tábla.DataSource = AdatTáblaKönyvelés;

                Könyvelés_tábla.Columns["Azonosító"].Width = 200;
                Könyvelés_tábla.Columns["Megnevezés"].Width = 450;
                Könyvelés_tábla.Columns["Méret"].Width = 100;
                Könyvelés_tábla.Columns["Mennyiség"].Width = 100;
                Könyvelés_tábla.Columns["Gyáriszám"].Width = 130;

                Könyvelés_tábla.Refresh();

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

            Könyvelés_tábla.Visible = true;
            Holtart.Ki();
        }

        private void Honnan_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                Honnan_kiíró_név();
                Hova.Enabled = true;
                HováNév.Enabled = true;
                Hova.Items.Clear();
                HováNév.Items.Clear();
                HonnanMennyiség.Text = 0.ToString();
                HováMennyiség.Text = 0.ToString();
                switch (Honnan.Text.Trim())
                {
                    case "Érkezett":
                        {
                            // betölti a teljes választék listát
                            Rögzítés_azonosítók();
                            Hova.Text = "Raktár";
                            HováNév.Text = "Szerszámraktárban lévő anyagok és eszközök";
                            Hova.Enabled = false;
                            HováNév.Enabled = false;
                            break;
                        }
                    case "Raktár":
                        {
                            Azonosítóhelyen();
                            Hova.Text = "";
                            HováNév.Text = "";
                            Hova_feltöltések();
                            // ide nem lehet könyvelni
                            Hova.Items.RemoveAt(Hova.FindStringExact("Selejt"));
                            HováNév.Items.RemoveAt(HováNév.FindStringExact("Leselejtezett"));


                            Hova.Refresh();
                            HováNév.Refresh();
                            break;
                        }
                    case "Selejt":
                        {
                            Azonosítóhelyen();
                            Hova.Text = "Selejtre";
                            HováNév.Text = "Selejtezésre előkészítés";
                            Hova.Enabled = false;
                            HováNév.Enabled = false;

                            Hova.Refresh();
                            HováNév.Refresh();
                            break;
                        }

                    case "Selejtre":
                        {
                            Azonosítóhelyen();
                            Hova.Items.Add("Raktár");
                            Hova.Items.Add("Selejt");
                            HováNév.Items.Add("Szerszámraktárban lévő anyagok és eszközök");
                            HováNév.Items.Add("Leselejtezett");

                            Hova.Refresh();
                            HováNév.Refresh();
                            break;
                        }

                    default:
                        {
                            Azonosítóhelyen();
                            Hova.Text = "Raktár";
                            HováNév.Text = "Szerszámraktárban lévő anyagok és eszközök";
                            Hova.Enabled = false;
                            HováNév.Enabled = false;

                            Hova.Refresh();
                            HováNév.Refresh();
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Könyvelés_után()
        {
            Üríti_mezőket();
            switch (Honnan.Text.Trim())
            {
                case "Érkezett":
                    {
                        // betölti a teljes választék listát
                        Rögzítés_azonosítók();
                        break;
                    }

                case "Raktár":
                    {
                        Azonosítóhelyen();
                        break;
                    }

                case "Selejt":
                    {
                        Azonosítóhelyen();
                        break;
                    }


                case "Selejtre":
                    {
                        Azonosítóhelyen();
                        break;
                    }

                default:
                    {
                        Azonosítóhelyen();
                        break;
                    }

            }
        }
        private void Hova_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Hova_kiíró_név();
            Darabszámok_kiírása();
        }

        private void Hova_kiíró_név()
        {
            if (Hova.Text.Trim() == "") return;
            HováNév.Text = "";
            Adat_Szerszám_Könyvtörzs ElemKönyv = (from a in AdatokKönyv
                                                  where a.Szerszámkönyvszám == Hova.Text.Trim()
                                                  select a).FirstOrDefault();
            if (ElemKönyv != null) HováNév.Text = ElemKönyv.Szerszámkönyvnév;
        }

        private void Hova_kiíró_kszám()
        {
            if (HováNév.Text.Trim() == "") return;
            Hova.Text = "";
            Adat_Szerszám_Könyvtörzs ElemKönyv = (from a in AdatokKönyv
                                                  where a.Szerszámkönyvnév == HováNév.Text.Trim()
                                                  select a).FirstOrDefault();
            if (ElemKönyv != null) Hova.Text = ElemKönyv.Szerszámkönyvszám;
        }

        private void Honnan_kiíró_név()
        {
            if (Honnan.Text.Trim() == "") return;
            HonnanNév.Text = "";
            Adat_Szerszám_Könyvtörzs ElemKönyv = (from a in AdatokKönyv
                                                  where a.Szerszámkönyvszám == Honnan.Text.Trim()
                                                  select a).FirstOrDefault();
            if (ElemKönyv != null) HonnanNév.Text = ElemKönyv.Szerszámkönyvnév;

        }

        private void Honnan_kiíró_kszám()
        {
            if (HonnanNév.Text.Trim() == "") return;
            Honnan.Text = "";
            Adat_Szerszám_Könyvtörzs ElemKönyv = (from a in AdatokKönyv
                                                  where a.Szerszámkönyvnév == HonnanNév.Text.Trim()
                                                  select a).FirstOrDefault();
            if (ElemKönyv != null) Honnan.Text = ElemKönyv.Szerszámkönyvszám;
        }

        private void HováNév_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Hova_kiíró_kszám();
            Darabszámok_kiírása();
        }

        private void SzerszámAzonosító_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            SzAzonosító_kiíró();
        }

        private void SzerszámAzonosító_TextUpdate(object sender, EventArgs e)
        {
            SzAzonosító_kiíró();
        }

        private void SzAzonosító_kiíró()
        {
            try
            {

                if (SzerszámAzonosító.Text.Trim() == "") return;
                CikktörzsListaFeltöltés();
                Adat_Szerszám_Cikktörzs Elem = (from a in AdatokCikk
                                                where a.Azonosító == SzerszámAzonosító.Text.Trim().ToUpper()
                                                select a).FirstOrDefault();
                Mennyiség.Text = "";
                Mennyiség.Enabled = true;
                if (Elem != null)
                {
                    SzerszámAzonosító.Text = SzerszámAzonosító.Text.ToUpper().Trim();
                    Megnevezés.Text = Elem.Megnevezés;
                    Darabszámok_kiírása();
                    Mennyiség.Focus();
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SzAzonosító_kiíró_szám()
        {
            Mennyiség.Text = "";
            Mennyiség.Enabled = true;
            if (Megnevezés.Text.Trim() == "") return;

            Adat_Szerszám_Cikktörzs Elem = (from a in AdatokCikk
                                            where a.Státus == 0 &&
                                            a.Megnevezés == Megnevezés.Text.Trim()
                                            select a).FirstOrDefault();
            if (Elem != null) SzerszámAzonosító.Text = Elem.Azonosító;
        }

        private void Darabszámok_kiírása()
        {
            try
            {
                HonnanMennyiség.Text = "0";
                HováMennyiség.Text = "0";

                Adat_Szerszám_Könyvelés ElemKönyvelés = (from a in AdatokKönyvelés
                                                         where a.AzonosítóMás == SzerszámAzonosító.Text.Trim() &&
                                                         a.SzerszámkönyvszámMás == Honnan.Text.Trim()
                                                         select a).FirstOrDefault();
                if (ElemKönyvelés != null) HonnanMennyiség.Text = ElemKönyvelés.Mennyiség.ToString();

                ElemKönyvelés = (from a in AdatokKönyvelés
                                 where a.AzonosítóMás == SzerszámAzonosító.Text.Trim() &&
                                 a.SzerszámkönyvszámMás == Hova.Text.Trim()
                                 select a).FirstOrDefault();
                if (ElemKönyvelés != null) HováMennyiség.Text = ElemKönyvelés.Mennyiség.ToString();
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Megnevezés_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            SzAzonosító_kiíró_szám();
            Darabszámok_kiírása();
        }

        private void Üríti_mezőket()
        {
            Megnevezés.Text = "";
            Mennyiség.Text = "";
            SzerszámAzonosító.Text = "";
        }

        private void Azonosítóhelyen()
        {
            try
            {
                List<Adat_Szerszám_Könyvelés> Adatok = (from a in AdatokKönyvelés
                                                        where a.SzerszámkönyvszámMás == Honnan.Text.Trim()
                                                        orderby a.AzonosítóMás
                                                        select a).ToList();
                SzerszámAzonosító.Text = "";
                SzerszámAzonosító.Items.Clear();

                foreach (Adat_Szerszám_Könyvelés rekord in Adatok)
                    SzerszámAzonosító.Items.Add(rekord.AzonosítóMás);

                SzerszámAzonosító.Refresh();

                Könyvelés_Tábla_író();
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Könyvelés_tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                SzerszámAzonosító.Text = Könyvelés_tábla.Rows[e.RowIndex].Cells[0].Value.ToStrTrim();
                Megnevezés.Text = Könyvelés_tábla.Rows[e.RowIndex].Cells[1].Value.ToStrTrim();
                Darabszámok_kiírása();
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Rögzít_Click_2(object sender, EventArgs e)
        {
            try
            {

                if (Honnan.Text.Trim() == "" || Honnan.Text.ToStrTrim() == "") throw new HibásBevittAdat("Nincs a Könyvelés Honnan mezeje kitöltve.");
                if (Hova.Text.Trim() == "" || Hova.Text.ToStrTrim() == "") throw new HibásBevittAdat("Nincs a Könyvelés Hova mezeje kitöltve.");
                if (Megnevezés.Text.Trim() == "") throw new HibásBevittAdat("Nincs a Könyvelendő anyag kiválasztva.");
                if (Mennyiség.Text == null) throw new HibásBevittAdat("Nincs a mennyiség mező kitöltve.");
                if (Mennyiség.Text.Trim() == "") throw new HibásBevittAdat("Nincs a mennyiség mező kitöltve.");
                if (!int.TryParse(Mennyiség.Text, out int érték)) throw new HibásBevittAdat("Mennyiségnek számnak kell lennie !");
                if (érték < 0) throw new HibásBevittAdat("Könyvelendő mennyiség nem lehet nulla és negatív!");
                if (HováMennyiség.Text.Trim() == "") return;
                if (HonnanMennyiség.Text == "") return;
                if (Honnan.Text.Trim() != "Érkezett" && int.Parse(HonnanMennyiség.Text) < int.Parse(Mennyiség.Text)) throw new HibásBevittAdat("Csak a készleteten lévő mennyiséget lehet könyvelni !");

                // Beraktározás
                if (Honnan.Text.Trim() == "Érkezett" && Hova.Text.Trim() == "Raktár")
                {
                    Rögzítés("Raktár", "plusz");
                    Naplózás();
                    Darabszámok_kiírása();
                    Könyvelés_után();
                    return;
                }
                // beraktározás storno
                if (Hova.Text.Trim() == "Érkezett" && Honnan.Text.Trim() == "Raktár")
                {
                    Rögzítés("Raktár", "mínusz");
                    Naplózás();
                    Darabszámok_kiírása();
                    Könyvelés_után();
                    return;
                }

                // dolgozónak kiadás
                if (Honnan.Text.Trim() == "Raktár" && Hova.Text.Trim() != "Érkezett" && Hova.Text.Trim() != "Selejt" && Hova.Text.Trim() != "Selejtre")
                {
                    Rögzítés("Raktár", "mínusz");
                    Rögzítés(Hova.Text.Trim(), "plusz");

                    Naplózás();
                    Darabszámok_kiírása();
                    Könyvelés_után();
                    return;
                }

                // dolgozó visszaraktár
                if (Hova.Text.Trim() == "Raktár" && Honnan.Text.Trim() != "Érkezett" && Honnan.Text.Trim() != "Selejt" && Honnan.Text.Trim() != "Selejtre")
                {
                    Rögzítés("Raktár", "plusz");
                    Rögzítés(Honnan.Text.Trim(), "mínusz");

                    Naplózás();
                    Darabszámok_kiírása();
                    Könyvelés_után();
                    return;
                }

                // selejt előkészítés
                if (Honnan.Text.Trim() == "Raktár" && Hova.Text.Trim() == "Selejtre")
                {
                    Rögzítés("Raktár", "mínusz");
                    Rögzítés(Hova.Text.Trim(), "plusz");

                    Naplózás();
                    Darabszámok_kiírása();
                    Könyvelés_után();
                    return;
                }

                // selejt előkészítés storno
                if (Hova.Text.Trim() == "Raktár" && Honnan.Text.Trim() == "Selejtre")
                {
                    Rögzítés("Raktár", "plusz");
                    Rögzítés(Honnan.Text.Trim(), "mínusz");

                    Naplózás();
                    Darabszámok_kiírása();
                    Könyvelés_után();
                    return;
                }

                // selejtezés
                if (Honnan.Text.Trim() == "Selejtre" && Hova.Text.Trim() == "Selejt")
                {
                    Rögzítés("Selejtre", "mínusz");
                    Rögzítés("Selejt", "plusz");

                    Naplózás();
                    Darabszámok_kiírása();
                    Könyvelés_után();
                    return;
                }

                // selejtezés storno
                if (Hova.Text.Trim() == "Selejtre" && Honnan.Text.Trim() == "Selejt")
                {
                    Rögzítés("Selejtre", "plusz");
                    Rögzítés("Selejt", "mínusz");

                    Naplózás();
                    Könyvelés_után();
                    return;
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

        private void Naplózás()
        {
            try
            {
                if (Honnan.Text.Length > 10 || Hova.Text.Length > 10) throw new HibásBevittAdat("Könyvszám maximum 10 karakter hosszú lehet!");
                if (SzerszámAzonosító.Text.Trim().Length > 20) throw new HibásBevittAdat("Azonosító maximum 20 karakter hosszú lehet!");
                if (SzerszámAzonosító.Text.Trim().Trim() == "") throw new HibásBevittAdat("Azonosítónak lennie kell!");
                if (!int.TryParse(Mennyiség.Text, out int mennyiség)) throw new HibásBevittAdat("Mennyiségnek számnak kell lennie.");
                if (mennyiség <= 0) throw new HibásBevittAdat("Könyvelendő mennyiség nem lehet nulla és negatív!");

                string helyn = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\{Könyvtár_adat}\Adatok\szerszámnapló{DateTime.Today.Year}.mdb";
                string jelszó = "csavarhúzó";
                if (!File.Exists(helyn)) Adatbázis_Létrehozás.Szerszámlistanapló(helyn);

                string azonostító = SzerszámAzonosító.Text.Trim();
                string honann = Honnan.Text.Trim();
                string hova = Hova.Text.Trim();

                Adat_Szerszám_Napló NaplóAdat = new Adat_Szerszám_Napló(azonostító, honann, hova, mennyiség);
                KézNapló.Rögzítés(helyn, jelszó, NaplóAdat);
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

        private void Rögzítés(string cím, string előjel)
        {
            try
            {
                KönyvelésListaFeltöltés();
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\{Könyvtár_adat}\Adatok\Szerszám.mdb";
                string jelszó = "csavarhúzó";

                Adat_Szerszám_Könyvelés ElemKönyvelés = (from a in AdatokKönyvelés
                                                         where a.AzonosítóMás == SzerszámAzonosító.Text.Trim() &&
                                                         a.SzerszámkönyvszámMás == cím
                                                         select a).FirstOrDefault();
                int mennyiség = 0;
                string Szerszámkönyvszám = "";
                // ha van akkor módosítjuk a darabszámot
                if (ElemKönyvelés != null)
                {

                    if (előjel == "plusz")
                    {
                        mennyiség = int.Parse(Mennyiség.Text.Trim()) + int.Parse(HováMennyiség.Text);
                        Szerszámkönyvszám = Hova.Text.Trim();
                    }
                    else
                    {
                        mennyiség = int.Parse(HonnanMennyiség.Text) - int.Parse(Mennyiség.Text.Trim());
                        Szerszámkönyvszám = Honnan.Text.Trim();
                    }

                    Adat_Szerszám_Könyvelés Adat = new Adat_Szerszám_Könyvelés(mennyiség, DateTime.Now, SzerszámAzonosító.Text.Trim(), cím);

                    if (mennyiség > 0)
                    {
                        KézKönyvelés.Módosítás(hely, jelszó, Adat);
                        MessageBox.Show("Módosítás megtörtént !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    else
                    {
                        KézKönyvelés.Törlés(hely, jelszó, Adat);
                        MessageBox.Show("Törlés megtörtént !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    mennyiség = int.Parse(Mennyiség.Text.Trim());
                    Szerszámkönyvszám = Hova.Text.Trim();
                    Adat_Szerszám_Könyvelés Adat = new Adat_Szerszám_Könyvelés(mennyiség, DateTime.Now, SzerszámAzonosító.Text.Trim(), Szerszámkönyvszám);

                    KézKönyvelés.Rögzítés(hely, jelszó, Adat);
                    MessageBox.Show("Rögzítés megtörtént !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Könyvelés_Szűr_Click(object sender, EventArgs e)
        {
            Könyvelés_Tábla_író();
        }

        private void Könyvelés_szűrés_ürítés_Click(object sender, EventArgs e)
        {
            Radio_könyv_Minden.Checked = true;
            Könyvelés_megnevezés.Text = "";
            Könyvelés_Méret.Text = "";
        }
        #endregion

        #region Nyomtatványok
        private void Nyomtatvány9A_Click(object sender, EventArgs e)
        {
            try
            {

                //Azon elemeket amelyik nem E-vel kezdődik visszaállítjuk
                for (int i = 0; i < Napló_Tábla.Rows.Count; i++)
                {
                    if (Napló_Tábla.Rows[i].Cells[0].Value.ToString().Substring(0, 1) != "E")
                        Napló_Tábla.Rows[i].Selected = false;
                }
                if (Napló_Tábla.SelectedRows.Count < 1)
                    throw new HibásBevittAdat("Nincs kiválasztva a táblázatban nyomtatandó elem.");

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\{Könyvtár_adat}\Adatok\Szerszám.mdb";
                string jelszó = "csavarhúzó";
                string szöveg = "SELECT * FROM könyvtörzs ";


                List<Adat_Szerszám_Könyvtörzs> Könyv_Törzs = KézKönyv.Lista_Adatok(hely, jelszó, szöveg);

                string szervezet = "";
                Adat_Kiegészítő_Jelenlétiív Szerv = (from a in AdatokJelenléti
                                                     where a.Id == 5
                                                     select a).FirstOrDefault();
                if (Szerv != null) szervezet = Szerv.Szervezet;

                EszközListaFeltöltés();

                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    Filter = "Excel |*.xlsx",
                    FileName = $"9A_Felvétel_{DateTime.Now:yyyyMMddHHmmss}"
                };

                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                string munkalap = "Munka1";
                int sor = 1;

                // megnyitjuk az excelt
                MyE.ExcelLétrehozás();

                //Nyomtatvány eleje

                MyE.Oszlopszélesség(munkalap, "a:a", 5);
                MyE.Oszlopszélesség(munkalap, "b:b", 15);
                MyE.Oszlopszélesség(munkalap, "c:c", 6);
                MyE.Oszlopszélesség(munkalap, "d:d", 30);
                MyE.Oszlopszélesség(munkalap, "e:e", 12);
                MyE.Oszlopszélesség(munkalap, "f:f", 13);
                MyE.Oszlopszélesség(munkalap, "g:g", 10);
                MyE.Oszlopszélesség(munkalap, "h:h", 22);
                MyE.Oszlopszélesség(munkalap, "i:i", 16);
                MyE.Oszlopszélesség(munkalap, "j:j", 10);
                MyE.Oszlopszélesség(munkalap, "k:k", 30);
                MyE.Oszlopszélesség(munkalap, "l:l", 16);

                MyE.Munkalap_betű("Calibri", 12);
                MyE.VékonyFelső($"a{sor}:l{sor}");

                MyE.Sormagasság($"{sor}:{sor + 3}", 18);

                sor++;// ez a második sor
                MyE.Egyesít(munkalap, $"j{sor}:k{sor}");
                MyE.Kiir("Iktatószám:", $"j{sor}:k{sor}");
                MyE.Betű($"j{sor}:k{sor}", 10);
                MyE.Betű($"j{sor}:k{sor}", false, false, true);
                MyE.Igazít_függőleges($"j{sor}:k{sor}", "felső");
                MyE.Igazít_vízszintes($"j{sor}:k{sor}", "jobb");

                sor++;//harmadik sor

                sor++; //negyedik sor    
                MyE.Egyesít("Munka1", $"e{sor}:i{sor}");
                MyE.Kiir("Tárgyi eszközök költséghelyen belüli áthelyezése", $"e{sor}:i{sor}");
                MyE.Betű($"e{sor}:i{sor}", false, false, true);
                MyE.Igazít_függőleges($"e{sor}:i{sor}", "közép");
                MyE.Igazít_vízszintes($"e{sor}:i{sor}", "közép");

                sor++;//ötödik sor
                MyE.Sormagasság($"{sor}:{sor}", 20);

                sor++;//hatodik sor
                MyE.Sormagasság($"{sor}:{sor}", 16);

                MyE.Egyesít("Munka1", $"a{sor}:a{sor + 1}");
                MyE.Kiir("Ssz.", $"a{sor}:a{sor + 1}");
                MyE.Betű($"a{sor}:a{sor + 1}", false, false, true);
                MyE.Igazít_függőleges($"a{sor}:a{sor + 1}", "közép");
                MyE.Igazít_vízszintes($"a{sor}:a{sor + 1}", "közép");
                MyE.Vékonykeret($"a{sor}:a{sor + 1}");

                MyE.Egyesít("Munka1", $"b{sor}:f{sor}");
                MyE.Kiir("Alapadatok", $"b{sor}:f{sor}");
                MyE.Betű($"b{sor}:f{sor}", false, false, true);
                MyE.Igazít_függőleges($"b{sor}:f{sor}", "közép");
                MyE.Igazít_vízszintes($"b{sor}:f{sor}", "közép");
                MyE.Vékonykeret($"b{sor}:f{sor}");

                MyE.Egyesít("Munka1", $"g{sor}:i{sor}");
                MyE.Kiir("Honnan", $"g{sor}:i{sor}");
                MyE.Betű($"g{sor}:i{sor}", false, false, true);
                MyE.Igazít_függőleges($"g{sor}:i{sor}", "közép");
                MyE.Igazít_vízszintes($"g{sor}:i{sor}", "közép");
                MyE.Vékonykeret($"g{sor}:i{sor}");

                MyE.Egyesít("Munka1", $"j{sor}:l{sor}");
                MyE.Kiir("Hova", $"j{sor}:l{sor}");
                MyE.Betű($"j{sor}:l{sor}", false, false, true);
                MyE.Igazít_függőleges($"j{sor}:l{sor}", "közép");
                MyE.Igazít_vízszintes($"j{sor}:l{sor}", "közép");
                MyE.Vékonykeret($"j{sor}:l{sor}");

                sor++;// ez a hetedik sor

                MyE.Kiir("Eszközszám", $"b{sor}");
                MyE.Betű($"b{sor}", false, false, true);
                MyE.Igazít_függőleges($"b{sor}", "közép");
                MyE.Igazít_vízszintes($"b{sor}", "közép");
                MyE.Vékonykeret($"b{sor}");

                MyE.Kiir("Alsz.", $"c{sor}");
                MyE.Betű($"c{sor}", false, false, true);
                MyE.Igazít_függőleges($"c{sor}", "közép");
                MyE.Igazít_vízszintes($"c{sor}", "közép");
                MyE.Vékonykeret($"c{sor}");

                MyE.Kiir("Megnevezés", $"d{sor}");
                MyE.Betű($"d{sor}", false, false, true);
                MyE.Igazít_függőleges($"d{sor}", "közép");
                MyE.Igazít_vízszintes($"d{sor}", "közép");
                MyE.Vékonykeret($"d{sor}");

                MyE.Kiir("Gyártási szám", $"e{sor}");
                MyE.Betű($"e{sor}", false, false, true);
                MyE.Sortörésseltöbbsorba($"e{sor}");
                MyE.Igazít_függőleges($"e{sor}", "közép");
                MyE.Igazít_vízszintes($"e{sor}", "közép");
                MyE.Vékonykeret($"e{sor}");

                MyE.Kiir("Leltárszám", $"f{sor}");
                MyE.Betű($"f{sor}", false, false, true);
                MyE.Igazít_függőleges($"f{sor}", "közép");
                MyE.Igazít_vízszintes($"f{sor}", "közép");
                MyE.Vékonykeret($"f{sor}");

                MyE.Kiir("Telephely", $"g{sor}");
                MyE.Betű($"g{sor}", false, false, true);
                MyE.Igazít_függőleges($"g{sor}", "közép");
                MyE.Igazít_vízszintes($"g{sor}", "közép");
                MyE.Vékonykeret($"g{sor}");

                MyE.Kiir("Helyiség", $"h{sor}");
                MyE.Betű($"h{sor}", false, false, true);
                MyE.Igazít_függőleges($"h{sor}", "közép");
                MyE.Igazít_vízszintes($"h{sor}", "közép");
                MyE.Vékonykeret($"h{sor}");

                MyE.Kiir("Személyügyi törzsszám \n (9/b. számú melléklet csatolandó)", $"i{sor}");
                MyE.Betű($"i{sor}", false, false, true);
                MyE.Sortörésseltöbbsorba($"i{sor}");
                MyE.Igazít_függőleges($"i{sor}", "közép");
                MyE.Igazít_vízszintes($"i{sor}", "közép");
                MyE.Vékonykeret($"i{sor}");

                MyE.Kiir("Telephely", $"j{sor}");
                MyE.Betű($"j{sor}", false, false, true);
                MyE.Igazít_függőleges($"j{sor}", "közép");
                MyE.Igazít_vízszintes($"j{sor}", "közép");
                MyE.Vékonykeret($"j{sor}");

                MyE.Kiir("Helyiség", $"k{sor}");
                MyE.Betű($"k{sor}", false, false, true);
                MyE.Igazít_függőleges($"k{sor}", "közép");
                MyE.Igazít_vízszintes($"k{sor}", "közép");
                MyE.Vékonykeret($"k{sor}");

                MyE.Kiir("Személyügyi törzsszám \n (9/b. számú melléklet csatolandó)", $"l{sor}");
                MyE.Betű($"l{sor}", false, false, true);
                MyE.Sortörésseltöbbsorba($"l{sor}");
                MyE.Sormagasság($"l{sor}", 77);
                MyE.Igazít_függőleges($"l{sor}", "közép");
                MyE.Igazít_vízszintes($"l{sor}", "közép");
                MyE.Vékonykeret($"l{sor}");


                //nyolcadik sor

                for (int i = 0; i < Napló_Tábla.SelectedRows.Count; i++)
                {
                    string eszközszám = Napló_Tábla.SelectedRows[i].Cells[0].Value.ToString().Substring(1, Napló_Tábla.SelectedRows[i].Cells[0].Value.ToString().Length - 1); //E betűt elhagyjuk
                    string Honnan_Tábla = Napló_Tábla.SelectedRows[i].Cells[5].Value.ToStrTrim();
                    string Hova_Tábla = Napló_Tábla.SelectedRows[i].Cells[6].Value.ToStrTrim();

                    Adat_Eszköz EgyElem = (from a in AdatokEszköz
                                           where a.Eszköz == eszközszám
                                           select a).FirstOrDefault();

                    if (EgyElem != null)
                    {
                        sor++;
                        MyE.Sormagasság($"{sor}:{sor}", 30);

                        MyE.Kiir((i + 1).ToString(), $"a{sor}");//sorszám
                        MyE.Sormagasság($"a{sor}", 30);
                        MyE.Igazít_függőleges($"a{sor}", "közép");
                        MyE.Igazít_vízszintes($"a{sor}", "közép");
                        MyE.Vékonykeret($"a{sor}");

                        MyE.Betű($"b{sor}", "", "@");
                        MyE.Kiir(eszközszám, $"b{sor}"); //Eszközszám
                        MyE.Igazít_függőleges($"b{sor}", "közép");
                        MyE.Igazít_vízszintes($"b{sor}", "közép");
                        MyE.Vékonykeret($"b{sor}");

                        MyE.Kiir(EgyElem.Alszám.Trim(), $"c{sor}"); //Alszám
                        MyE.Igazít_függőleges($"c{sor}", "közép");
                        MyE.Igazít_vízszintes($"c{sor}", "közép");
                        MyE.Vékonykeret($"c{sor}");

                        MyE.Kiir(EgyElem.Megnevezés.Trim(), $"d{sor}"); //Megnevezés
                        MyE.Igazít_függőleges($"d{sor}", "közép");
                        MyE.Igazít_vízszintes($"d{sor}", "közép");
                        MyE.Vékonykeret($"d{sor}");

                        MyE.Kiir(EgyElem.Gyártási_szám.Trim(), $"e{sor}");//Gyártásiszám
                        MyE.Igazít_függőleges($"e{sor}", "közép");
                        MyE.Igazít_vízszintes($"e{sor}", "közép");
                        MyE.Vékonykeret($"e{sor}");

                        MyE.Kiir(EgyElem.Leltárszám.Trim(), $"f{sor}");//Leltáriszám
                        MyE.Igazít_függőleges($"f{sor}", "közép");
                        MyE.Igazít_vízszintes($"f{sor}", "közép");
                        MyE.Vékonykeret($"f{sor}");

                        string Ideig = (from a in Könyv_Törzs
                                        where a.Szerszámkönyvszám == Honnan_Tábla
                                        select a.Felelős1).FirstOrDefault();

                        string helyiség = "-";
                        string telephely = "-";
                        string HR_azonosító = "-";

                        if (Ideig != null)
                        {
                            if (Ideig.Contains("-"))
                            {
                                string[] raktárdb = Ideig.Split('-');
                                helyiség = raktárdb[0].Trim();
                                telephely = raktárdb[1].Trim();
                                HR_azonosító = "-";
                            }
                            else
                            {
                                string[] raktárdb = Ideig.Split('=');
                                helyiség = "N.A.";
                                telephely = "-";
                                HR_azonosító = raktárdb[1].Trim();
                            }
                        }

                        //Honnan
                        if (HR_azonosító == "-")
                        {
                            MyE.Kiir(telephely, $"g{sor}");//telephely
                            MyE.Igazít_függőleges($"g{sor}", "közép");
                            MyE.Igazít_vízszintes($"g{sor}", "közép");
                            MyE.Vékonykeret($"g{sor}");

                            MyE.Kiir(helyiség, $"h{sor}");//helyiség
                            MyE.Igazít_függőleges($"h{sor}", "közép");
                            MyE.Igazít_vízszintes($"h{sor}", "közép");
                            MyE.Vékonykeret($"h{sor}");

                            MyE.Kiir(HR_azonosító, $"i{sor}");// HR azonosító
                            MyE.Igazít_függőleges($"i{sor}", "közép");
                            MyE.Igazít_vízszintes($"i{sor}", "közép");
                            MyE.Vékonykeret($"i{sor}");
                        }
                        else
                        {
                            MyE.Kiir(telephely, $"g{sor}");//telephely
                            MyE.Igazít_függőleges($"g{sor}", "közép");
                            MyE.Igazít_vízszintes($"g{sor}", "közép");
                            MyE.Vékonykeret($"g{sor}");

                            MyE.Kiir(helyiség, $"h{sor}");//helyiség
                            MyE.Igazít_függőleges($"h{sor}", "közép");
                            MyE.Igazít_vízszintes($"h{sor}", "közép");
                            MyE.Vékonykeret($"h{sor}");

                            MyE.Kiir(HR_azonosító, $"i{sor}");// HR azonosító
                            MyE.Igazít_függőleges($"i{sor}", "közép");
                            MyE.Igazít_vízszintes($"i{sor}", "közép");
                            MyE.Vékonykeret($"i{sor}");
                        }

                        helyiség = "-";
                        telephely = "-";
                        HR_azonosító = "-";
                        Ideig = (from a in Könyv_Törzs
                                 where a.Szerszámkönyvszám == Hova_Tábla
                                 select a.Felelős1).FirstOrDefault();

                        if (Ideig != null)
                        {
                            if (Ideig.Contains("-"))
                            {
                                string[] raktárdb = Ideig.Split('-');
                                helyiség = raktárdb[0].Trim();
                                telephely = raktárdb[1].Trim();
                                HR_azonosító = "-";
                            }
                            else
                            {
                                string[] raktárdb = Ideig.Split('=');
                                helyiség = "N.A.";
                                telephely = "-";
                                HR_azonosító = raktárdb[1].Trim();
                            }
                        }
                        //Hova
                        if (HR_azonosító == "-")
                        {
                            MyE.Kiir(telephely, $"j{sor}");//telephely
                            MyE.Igazít_függőleges($"j{sor}", "közép");
                            MyE.Igazít_vízszintes($"j{sor}", "közép");
                            MyE.Vékonykeret($"j{sor}");

                            MyE.Kiir(helyiség, $"k{sor}");//helyiség
                            MyE.Igazít_függőleges($"k{sor}", "közép");
                            MyE.Igazít_vízszintes($"k{sor}", "közép");
                            MyE.Vékonykeret($"k{sor}");

                            MyE.Kiir(HR_azonosító, $"l{sor}");//hr azonosító
                            MyE.Igazít_függőleges($"l{sor}", "közép");
                            MyE.Igazít_vízszintes($"l{sor}", "közép");
                            MyE.Vékonykeret($"l{sor}");
                        }
                        else
                        {
                            MyE.Kiir(telephely, $"j{sor}");//telephely
                            MyE.Igazít_függőleges($"j{sor}", "közép");
                            MyE.Igazít_vízszintes($"j{sor}", "közép");
                            MyE.Vékonykeret($"j{sor}");

                            MyE.Kiir(helyiség, $"k{sor}");//helyiség
                            MyE.Igazít_függőleges($"k{sor}", "közép");
                            MyE.Igazít_vízszintes($"k{sor}", "közép");
                            MyE.Vékonykeret($"k{sor}");

                            MyE.Kiir(HR_azonosító, $"l{sor}");//hr azonosító
                            MyE.Igazít_függőleges($"l{sor}", "közép");
                            MyE.Igazít_vízszintes($"l{sor}", "közép");
                            MyE.Vékonykeret($"l{sor}");
                        }
                    }
                }
                MyE.Oszlopszélesség(munkalap, "G:G");
                MyE.Oszlopszélesség(munkalap, "J:J");
                MyE.Oszlopszélesség(munkalap, "D:D");
                MyE.Oszlopszélesség(munkalap, "E:E");
                MyE.Oszlopszélesség(munkalap, "H:H");
                MyE.Oszlopszélesség(munkalap, "K:K");
                sor++;//kilencedik sor

                sor += 3;//tizedik sor
                MyE.Sormagasság($"{sor}:{sor}", 16);

                MyE.Egyesít("Munka1", $"a{sor}:e{sor}");
                MyE.Kiir($"Budapest, {Napló_Dátumtól.Value.Year} év {Napló_Dátumtól.Value:MM} hó {Napló_Dátumtól.Value:dd} nap", $"a{sor}:e{sor}");
                MyE.Igazít_függőleges($"a{sor}:e{sor}", "közép");
                MyE.Igazít_vízszintes($"a{sor}:e{sor}", "bal");

                MyE.Egyesít("Munka1", $"i{sor}:j{sor}");
                MyE.Kiir("Nyilvántartó neve,aláírása:", $"i{sor}");

                sor++;
                MyE.Egyesít("Munka1", $"k{sor}:l{sor}");
                MyE.Aláírásvonal($"k{sor}:l{sor}");

                DolgozóListaFeltöltés();
                Adat_Dolgozó_Alap Nyilvántartó = (from a in AdatokDolgozó
                                                  where a.Bejelentkezésinév == Program.PostásNév
                                                  select a).FirstOrDefault();

                if (Nyilvántartó != null && Nyilvántartó.DolgozóNév != null)
                    MyE.Kiir(Nyilvántartó.DolgozóNév.Trim(), $"k{sor}");

                MyE.Igazít_függőleges($"i{sor}:l{sor}", "közép");
                MyE.Igazít_vízszintes($"i{sor}:l{sor}", "bal");

                List<Adat_Szerszám_FejLáb> Adatok = KézSzerszámFejLáb.Lista_Adatok();
                Adat_Szerszám_FejLáb Adat = Adatok.Where(a => a.Típus == "9A").FirstOrDefault();
                if (Adat != null)
                    MyE.NyomtatásiTerület_részletes(munkalap, $"A1:L{sor}", "", "",
                        Adat.Fejléc_Bal,
                        Adat.Fejléc_Közép,
                        Adat.Fejléc_Jobb,
                        Adat.Lábléc_Bal,
                        Adat.Lábléc_Közép,
                        Adat.Lábléc_Jobb,
                        "", 0.708661417322835, 0.708661417322835, 0, 0.669291338582677, 0.433070866141732, 0.15748031496063, false, false, "Fekvő");

                if (Napló_Nyomtat.Checked == true)
                {
                    MyE.Nyomtatás("Munka1", 1, 1);
                    MessageBox.Show("A bizonylatok nyomtatása elkészült.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // bezárjuk az Excel-t
                MyE.Aktív_Cella(munkalap, "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();

                if (Napló_Fájltöröl.Checked)
                    File.Delete(fájlexc + ".xlsx");
                else
                {
                    MyE.Megnyitás(fájlexc);
                    MessageBox.Show("A bizonylat elkészült.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Nyomtatvány9B_Click(object sender, EventArgs e)
        {
            try
            {
                if (Napló_Honnan.Text.Trim() == "") throw new HibásBevittAdat("Nincs érvényes adat a Honnan mezőben.");
                if (Napló_Hova.Text.Trim() == "") throw new HibásBevittAdat("Nincs érvényes adat a Hova mezőben.");


                //Azon elemeket amelyik nem E-vel kezdődik visszaállítjuk
                for (int i = 0; i < Napló_Tábla.Rows.Count; i++)
                {
                    if (Napló_Tábla.Rows[i].Cells[0].Value.ToString().Substring(0, 1) != "E")
                        Napló_Tábla.Rows[i].Selected = false;
                }
                if (Napló_Tábla.SelectedRows.Count < 1) throw new HibásBevittAdat("Nincs kiválasztva a táblázatban nyomtatandó elem.");

                Adat_Szerszám_Könyvtörzs IdeigHova = (from a in AdatokKönyv
                                                      where a.Szerszámkönyvszám == Napló_Hova.Text.Trim()
                                                      select a).FirstOrDefault();
                Adat_Szerszám_Könyvtörzs IdeigHonnan = (from a in AdatokKönyv
                                                        where a.Szerszámkönyvszám == Napló_Honnan.Text.Trim()
                                                        select a).FirstOrDefault();

                string[] HonnanDarabol;
                string[] HovaDarabol;
                string DolgozóNév, HrAzonosító;

                if (Napló_Hova.Text.Trim() == "Raktár")
                {
                    HonnanDarabol = IdeigHonnan.Felelős1.Split('=');
                    DolgozóNév = HonnanDarabol[0].Trim();
                    HrAzonosító = HonnanDarabol[1].Trim();
                    HovaDarabol = IdeigHova.Felelős1.Split('-');
                }
                else
                {
                    HonnanDarabol = IdeigHonnan.Felelős1.Split('-');
                    if (IdeigHova.Felelős1.Contains("="))
                    {
                        HovaDarabol = IdeigHova.Felelős1.Split('=');
                        DolgozóNév = HovaDarabol[0].Trim();
                        HrAzonosító = HovaDarabol[1].Trim();
                    }
                    else if (IdeigHova.Felelős1.Contains("-"))
                    {
                        HovaDarabol = IdeigHova.Felelős1.Split('-');
                        DolgozóNév = HovaDarabol[0].Trim();
                        HrAzonosító = HovaDarabol[1].Trim();
                    }
                    else
                        throw new HibásBevittAdat("Nem megfelelő a Hova könyv felelős meghatározása");


                }

                if (HonnanDarabol.Length < 2 || HovaDarabol.Length < 2) throw new HibásBevittAdat("A könyv felelőse nincs jól rögzítve a Könyvtörzsben.");
                string szervezet = "";
                Adat_Kiegészítő_Jelenlétiív Szerv = (from a in AdatokJelenléti
                                                     where a.Id == 5
                                                     select a).FirstOrDefault();
                if (Szerv != null) szervezet = Szerv.Szervezet;


                EszközListaFeltöltés();
                string eszközszám = Napló_Tábla.SelectedRows[0].Cells[0].Value.ToString().Substring(1, Napló_Tábla.SelectedRows[0].Cells[0].Value.ToString().Length - 1); //E betűt elhagyjuk
                Adat_Eszköz EgyElem = (from a in AdatokEszköz
                                       where a.Eszköz == eszközszám
                                       select a).FirstOrDefault();

                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"9B_{Napló_Hovánév.Text.Trim()}_{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;
                string munkalap = "Munka1";
                int sor = 1;

                // megnyitjuk az excelt
                MyE.ExcelLétrehozás();

                //Nyomtatvány eleje

                MyE.Oszlopszélesség(munkalap, "a:a", 5);
                MyE.Oszlopszélesség(munkalap, "b:b", 5);
                MyE.Oszlopszélesség(munkalap, "c:c", 10);
                MyE.Oszlopszélesség(munkalap, "d:d", 5);
                MyE.Oszlopszélesség(munkalap, "e:e", 8);
                MyE.Oszlopszélesség(munkalap, "f:f", 8);
                MyE.Oszlopszélesség(munkalap, "g:g", 8);
                MyE.Oszlopszélesség(munkalap, "h:h", 8);
                MyE.Oszlopszélesség(munkalap, "i:i", 13);
                MyE.Oszlopszélesség(munkalap, "j:j", 12);

                MyE.Munkalap_betű("Calibri", 12);
                MyE.Sormagasság($"{sor}:{sor + 8}", 16);

                //első sor
                MyE.Kiir("Iktatószám:", $"i{sor}");
                MyE.Betű($"i{sor}", 12);
                MyE.Igazít_függőleges($"i{sor}", "közép");
                MyE.Igazít_vízszintes($"i{sor}", "bal");

                sor++; //második sor
                MyE.Egyesít(munkalap, $"b{sor}:j{sor}");
                MyE.Kiir("Átvételi elismervény", $"b{sor}:j{sor}");
                MyE.Betű($"b{sor}:j{sor}", false, false, true);
                MyE.Igazít_függőleges($"b{sor}:j{sor}", "alsó");
                MyE.Igazít_vízszintes($"b{sor}:j{sor}", "közép");

                sor++; //harmdik sor
                MyE.Egyesít(munkalap, $"b{sor}:j{sor}");
                MyE.Kiir("személyi használatra kiadott eszközökről", $"b{sor}:j{sor}");
                MyE.Betű($"b{sor}:j{sor}", false, false, true);
                MyE.Igazít_függőleges($"b{sor}:j{sor}", "alsó");
                MyE.Igazít_vízszintes($"b{sor}:j{sor}", "közép");

                sor++; //negyedik sor 

                sor++; //ötödik sor 
                MyE.Egyesít(munkalap, $"a{sor}:b{sor}");
                MyE.Kiir("Alurírott", $"a{sor}:b{sor}");
                MyE.Igazít_függőleges($"a{sor}:b{sor}", "alsó");
                MyE.Igazít_vízszintes($"a{sor}:b{sor}", "bal");


                MyE.Egyesít(munkalap, $"c{sor}:e{sor}");
                MyE.Kiir(DolgozóNév, $"c{sor}:e{sor}"); //dolgozónév
                MyE.Betű($"c{sor}:e{sor}", false, false, true);
                MyE.Igazít_függőleges($"c{sor}:e{sor}", "alsó");
                MyE.Igazít_vízszintes($"c{sor}:e{sor}", "bal");

                MyE.Egyesít(munkalap, $"f{sor}:g{sor}");
                MyE.Kiir("(HR azonosító: ", $"f{sor}:g{sor}");
                MyE.Igazít_függőleges($"f{sor}:g{sor}", "alsó");
                MyE.Igazít_vízszintes($"f{sor}:g{sor}", "bal");

                MyE.Egyesít(munkalap, $"h{sor}:h{sor}");
                MyE.Kiir(HrAzonosító, $"h{sor}:h{sor}"); //Hr azonosító
                MyE.Igazít_függőleges($"h{sor}:h{sor}", "alsó");
                MyE.Igazít_vízszintes($"h{sor}:h{sor}", "jobb");

                MyE.Egyesít(munkalap, $"i{sor}:i{sor}");
                MyE.Kiir(",", $"i{sor}:i{sor}");
                MyE.Igazít_függőleges($"i{sor}:i{sor}", "alsó");
                MyE.Igazít_vízszintes($"i{sor}:i{sor}", "bal");


                sor++; //hatodik sor
                MyE.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyE.Kiir("felelős költséghely: ", $"a{sor}:c{sor}");
                MyE.Igazít_függőleges($"a{sor} :c{sor}", "alsó");
                MyE.Igazít_vízszintes($"a{sor} :c{sor}", "bal");

                MyE.Egyesít(munkalap, $"d{sor}:e{sor}");
                MyE.Kiir(EgyElem.Költséghely.Trim(), $"d{sor}:e{sor}"); //Költséghely
                MyE.Betű($"d{sor}:e{sor}", false, false, true);
                MyE.Igazít_függőleges($"d{sor}:e{sor}", "alsó");
                MyE.Igazít_vízszintes($"d{sor}:e{sor}", "bal");

                MyE.Egyesít(munkalap, $"f{sor}:g{sor}");
                MyE.Kiir("szervezeti egység", $"f{sor}:g{sor}");
                MyE.Igazít_függőleges($"f{sor}:g{sor}", "alsó");
                MyE.Igazít_vízszintes($"f{sor}:g{sor}", "bal");

                MyE.Egyesít(munkalap, $"h{sor}:j{sor}");
                MyE.Kiir(szervezet, $"h{sor}:j{sor}");// Szervezeti egység
                MyE.Betű($"h{sor}:j{sor}", false, false, true);
                MyE.Igazít_függőleges($"h{sor}:j{sor}", "alsó");
                MyE.Igazít_vízszintes($"h{sor}:j{sor}", "bal");


                sor++; //hetedik sor
                MyE.Egyesít(munkalap, $"a{sor}:h{sor}");
                MyE.Kiir("a mai naptól személyes használatra a következő eszköz(öket):", $"a{sor}:h{sor}");
                MyE.Igazít_függőleges($"a{sor}:h{sor}", "alsó");
                MyE.Igazít_vízszintes($"a{sor}:h{sor}", "bal");

                MyE.Egyesít(munkalap, $"i{sor}:i{sor}");
                MyE.Kiir(" átvettem / ", $"i{sor}:i{sor}");

                MyE.Igazít_függőleges($"i{sor}:i{sor}", "alsó");
                MyE.Igazít_vízszintes($"i{sor}:i{sor}", "bal");

                MyE.Egyesít(munkalap, $"j{sor}:j{sor}");
                MyE.Kiir("leadtam", $"j{sor}:j{sor}");
                MyE.Igazít_függőleges($"j{sor}:j{sor}", "alsó");
                MyE.Igazít_vízszintes($"j{sor}:j{sor}", "bal");

                if (Napló_Honnan.Text.Trim() == "Raktár")
                    MyE.Betű($"i{sor}:i{sor}", true, false, true); //átvettem
                else
                    MyE.Betű($"j{sor}:j{sor}", true, false, true); //leadtam


                sor++; //nyolcadik sor
                sor++; //kilencedik sor


                sor++; //tizedik sor
                MyE.Sormagasság($"{sor}:{sor}", 48);
                MyE.Rácsoz($"a{sor}:j{sor}");

                MyE.Egyesít(munkalap, $"a{sor}:a{sor}");
                MyE.Kiir("Sor- szám", $"a{sor}:a{sor}");
                MyE.Sortörésseltöbbsorba($"a{sor}:a{sor}");
                MyE.Betű($"a{sor}:a{sor}", false, false, true);
                MyE.Igazít_függőleges($"a{sor}:a{sor}", "közép");
                MyE.Igazít_vízszintes($"a{sor}:a{sor}", "közép");

                MyE.Egyesít(munkalap, $"b{sor}:d{sor}");
                MyE.Kiir("Eszközszám / Alszám", $"b{sor}:d{sor}");
                MyE.Betű($"b{sor}:d{sor}", false, false, true);
                MyE.Igazít_függőleges($"b{sor}:d{sor}", "közép");
                MyE.Igazít_vízszintes($"b{sor}:d{sor}", "közép");

                MyE.Egyesít(munkalap, $"e{sor}:h{sor}");
                MyE.Kiir("Eszköz megnevezése", $"e{sor}:h{sor}");
                MyE.Betű($"e{sor}:h{sor}", false, false, true);
                MyE.Igazít_függőleges($"e{sor}:h{sor}", "közép");
                MyE.Igazít_vízszintes($"e{sor}:h{sor}", "közép");

                MyE.Egyesít(munkalap, $"i{sor}:i{sor}");
                MyE.Kiir("Gyártási szám", $"i{sor}:i{sor}");
                MyE.Betű($"i{sor}:i{sor}", false, false, true);
                MyE.Igazít_függőleges($"i{sor}:i{sor}", "közép");
                MyE.Igazít_vízszintes($"i{sor}:i{sor}", "közép");

                MyE.Egyesít(munkalap, $"j{sor}:j{sor}");
                MyE.Kiir("Leltárszám", $"j{sor}:j{sor}");
                MyE.Betű($"j{sor}:j{sor}", false, false, true);
                MyE.Igazít_függőleges($"j{sor}:j{sor}", "közép");
                MyE.Igazít_vízszintes($"j{sor}:j{sor}", "közép");


                //tizenegyedik sor
                int soreleje = sor;
                for (int i = 0; i < Napló_Tábla.SelectedRows.Count; i++)
                {

                    eszközszám = Napló_Tábla.SelectedRows[i].Cells[0].Value.ToString().Substring(1, Napló_Tábla.SelectedRows[i].Cells[0].Value.ToString().Length - 1); //E betűt elhagyjuk
                    EgyElem = (from a in AdatokEszköz
                               where a.Eszköz == eszközszám
                               select a).FirstOrDefault();
                    if (EgyElem != null)
                    {
                        sor++;
                        MyE.Sormagasság($"{sor}:{sor + 6}", 20);

                        MyE.Egyesít(munkalap, $"a{sor}:a{sor}");
                        MyE.Kiir((i + 1).ToString(), $"a{sor}:a{sor}"); //sorszám
                        MyE.Igazít_függőleges($"a{sor}:a{sor}", "közép");
                        MyE.Igazít_vízszintes($"a{sor}:a{sor}", "közép");

                        MyE.Egyesít(munkalap, $"b{sor}:c{sor}");
                        MyE.Betű($"b{sor}:c{sor}", "", "@");
                        MyE.Kiir(eszközszám, $"b{sor}:c{sor}"); // Eszközszám
                        MyE.Igazít_függőleges($"b{sor}:c{sor}", "közép");
                        MyE.Igazít_vízszintes($"b{sor}:c{sor}", "közép");

                        MyE.Egyesít(munkalap, $"d{sor}:d{sor}");
                        MyE.Kiir(EgyElem.Alszám.Trim(), $"d{sor}:d{sor}"); //Alszám
                        MyE.Igazít_függőleges($"d{sor}:d{sor}", "közép");
                        MyE.Igazít_vízszintes($"d{sor}:d{sor}", "közép");

                        MyE.Egyesít(munkalap, $"e{sor}:h{sor}");
                        MyE.Kiir(EgyElem.Megnevezés.Trim(), $"e{sor}:h{sor}");//Megnevezés
                        MyE.Igazít_függőleges($"e{sor}:h{sor}", "közép");
                        MyE.Igazít_vízszintes($"e{sor}:h{sor}", "közép");

                        MyE.Egyesít(munkalap, $"i{sor}:i{sor}");
                        MyE.Kiir(EgyElem.Gyártási_szám.Trim(), $"i{sor}:i{sor}");// Gyártási szám
                        MyE.Igazít_függőleges($"i{sor}:i{sor}", "közép");
                        MyE.Igazít_vízszintes($"i{sor}:i{sor}", "közép");

                        MyE.Egyesít(munkalap, $"j{sor}:j{sor}");
                        MyE.Kiir(EgyElem.Leltárszám.Trim(), $"j{sor}:j{sor}"); //Leltáriszám
                        MyE.Igazít_függőleges($"j{sor}:j{sor}", "közép");
                        MyE.Igazít_vízszintes($"j{sor}:j{sor}", "közép");
                    }
                }
                MyE.Rácsoz($"a{soreleje}:j{sor}");

                sor++; //tizenkettedik sor
                sor++; //tizenharmadik sor

                sor++; //tizennegyedik sor
                MyE.Egyesít(munkalap, $"a{sor}:d{sor}");
                MyE.Kiir($"Budapest, {Napló_Dátumtól.Value:yyyy.MM.dd}", $"a{sor}:d{sor}");
                MyE.Igazít_függőleges($"a{sor}:d{sor}", "közép");
                MyE.Igazít_vízszintes($"a{sor}:d{sor}", "bal");

                sor++; //tizenötödik sor

                sor++; //tizenhatodik sor
                MyE.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyE.Kiir("Munkavállaló neve: ", $"a{sor}:c{sor}");
                MyE.Kiir(DolgozóNév, $"e{sor}");
                MyE.Igazít_függőleges($"a{sor}:c{sor}", "közép");
                MyE.Igazít_vízszintes($"a{sor}:c{sor}", "bal");

                sor++; //tizenhetedik sor
                MyE.Sormagasság($"{sor}:{sor + 8}", 16);

                sor++; //tizennyolcadik sor
                sor++; //tizenkilencedik sor
                MyE.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyE.Kiir("Munkavállaló aláírása:", $"a{sor}:c{sor}");
                MyE.Igazít_függőleges($"a{sor}:c{sor}", "közép");
                MyE.Igazít_vízszintes($"a{sor}:c{sor}", "bal");

                MyE.Egyesít(munkalap, $"E{sor}:H{sor}");

                sor++; //huszadik sor
                MyE.Aláírásvonal($"E{sor}:H{sor}");
                sor++; //huszonegyedik sor
                sor++; //huszonkettedik sor
                MyE.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyE.Kiir("Nyilvántartó neve:", $"a{sor}:c{sor}");
                MyE.Igazít_függőleges($"a{sor}:c{sor}", "közép");
                MyE.Igazít_vízszintes($"a{sor}:c{sor}", "bal");

                DolgozóListaFeltöltés();
                Adat_Dolgozó_Alap Nyilvántartó = (from a in AdatokDolgozó
                                                  where a.Bejelentkezésinév == Program.PostásNév
                                                  select a).FirstOrDefault();

                if (Nyilvántartó != null && Nyilvántartó.DolgozóNév != null)
                    MyE.Kiir(Nyilvántartó.DolgozóNév.Trim(), $"E{sor}");

                sor++; //huszonharmadik sor
                sor++; //huszonnegyedik sor
                sor++; //huszonötödik sor
                MyE.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyE.Kiir("Nyivántartó aláírása:", $"a{sor}:c{sor}");
                MyE.Igazít_függőleges($"a{sor}:c{sor}", "közép");
                MyE.Igazít_vízszintes($"a{sor}:c{sor}", "bal");

                MyE.Egyesít(munkalap, $"E{sor}:H{sor}");
                sor++;
                MyE.Aláírásvonal($"E{sor}:H{sor}");

                List<Adat_Szerszám_FejLáb> Adatok = KézSzerszámFejLáb.Lista_Adatok();
                Adat_Szerszám_FejLáb Adat = Adatok.Where(a => a.Típus == "9B").FirstOrDefault();
                if (Adat != null)
                {
                    MyE.NyomtatásiTerület_részletes(munkalap, $"A1:J{sor}", "", "",
                        Adat.Fejléc_Bal,
                        Adat.Fejléc_Közép,
                        Adat.Fejléc_Jobb,
                        Adat.Lábléc_Bal,
                        Adat.Lábléc_Közép,
                        Adat.Lábléc_Jobb, "", 0.393700787401575, 0.393700787401575, 0.748031496062992, 0.748031496062992, 0.31496062992126, 0.31496062992126, true, false, "Álló");
                }
                if (Napló_Nyomtat.Checked)
                {
                    MyE.Nyomtatás("Munka1", 1, 1);
                    MessageBox.Show("A bizonylatok nyomtatása elkészült.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //Nyomtatvány vége

                // bezárjuk az Excel-t
                MyE.Aktív_Cella(munkalap, "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();

                if (Napló_Fájltöröl.Checked)
                    File.Delete(fájlexc + ".xlsx");
                else
                {
                    MyE.Megnyitás(fájlexc);
                    MessageBox.Show("A bizonylat elkészült.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        #endregion

        #region ListákFeltöltése
        private void CikktörzsListaFeltöltés()
        {
            try
            {
                AdatokCikk.Clear();
                string szöveg = "SELECT * FROM cikktörzs ORDER BY azonosító";
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\{Könyvtár_adat}\Adatok\Szerszám.mdb";
                string jelszó = "csavarhúzó";
                AdatokCikk = KézSzerszámCikk.Lista_Adatok(hely, jelszó, szöveg);
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

        private void KönyvelésListaFeltöltés()
        {
            try
            {
                AdatokKönyvelés.Clear();
                string szöveg = "SELECT * FROM Könyvelés ORDER BY azonosító";
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\{Könyvtár_adat}\Adatok\Szerszám.mdb";
                string jelszó = "csavarhúzó";
                AdatokKönyvelés = KézKönyvelés.Lista_Adatok(hely, jelszó, szöveg);
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

        private void KönyvListaFeltöltés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\{Könyvtár_adat}\Adatok\Szerszám.mdb";
                string jelszó = "csavarhúzó";
                string szöveg = "SELECT * FROM könyvtörzs ORDER BY Szerszámkönyvszám";
                AdatokKönyv.Clear();
                AdatokKönyv = KézKönyv.Lista_Adatok(hely, jelszó, szöveg);
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

        private void DolgozóListaFeltöltés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text}\Adatok\Dolgozók.mdb";
                string jelszó = "forgalmiutasítás";
                string szöveg = "SELECT * FROM Dolgozóadatok ORDER BY Dolgozónév";
                AdatokDolgozó.Clear();
                AdatokDolgozó = KézDolgozó.Lista_Adatok(hely, jelszó, szöveg);
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

        private void JelenlétiListaFeltöltés()
        {
            try
            {
                AdatokJelenléti.Clear();
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Segéd\kiegészítő.mdb";
                string jelszó = "Mocó";
                string szöveg = "SELECT * FROM jelenlétiív ";
                AdatokJelenléti = KézJelenléti.Lista_Adatok(hely, jelszó, szöveg);
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

        private void EszközListaFeltöltés()
        {
            try
            {
                AdatokEszköz.Clear();
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Eszköz\Eszköz.mdb";
                string jelszó = "TóthKatalin";
                string szöveg = $"SELECT * FROM adatok ";

                AdatokEszköz = KézEszköz.Lista_Adatok(hely, jelszó, szöveg);
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

        private void NaplóListaFeltöltés(DateTime Dátum)
        {
            try
            {
                AdatokNapló.Clear();
                string jelszó = "csavarhúzó";
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\{Könyvtár_adat}\Adatok\szerszámnapló{Dátum.Year}.mdb";
                if (!File.Exists(hely)) return;
                string szöveg = "SELECT * FROM napló ";

                AdatokNapló = KézNapló.Lista_Adatok(hely, jelszó, szöveg);
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

