using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos
{

    public partial class Ablak_Szerszám
    {
        public Ablak_Szerszám()
        {
            InitializeComponent();

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

        readonly Beállítás_Betű BeBetű11 = new Beállítás_Betű { Méret = 11 };
        readonly Beállítás_Betű BeBetű11V = new Beállítás_Betű { Méret = 11, Vastag = true };
        readonly Beállítás_Betű BeBetű16 = new Beállítás_Betű { Méret = 16 };
        readonly Beállítás_Betű BeBetű16V = new Beállítás_Betű { Méret = 16, Vastag = true };
        readonly Beállítás_Betű BeBetűC12 = new Beállítás_Betű { Név = "Calibri", Méret = 12 };
        readonly Beállítás_Betű BeBetűC12K = new Beállítás_Betű { Név = "Calibri", Méret = 12, Formátum = "@" };
        readonly Beállítás_Betű BeBetűC12V = new Beállítás_Betű { Név = "Calibri", Méret = 12, Vastag = true };
        readonly Beállítás_Betű BeBetűC10V = new Beállítás_Betű { Név = "Calibri", Méret = 10, Vastag = true };

        //szerszámot ad át ha szerszámnyilvántartás
        //... ad át ha épületnyilvántartás
        private string Könyvtár_adat;

        #region Alap
        //Itt kapjuk meg a főoldaltól, hogy melyik funkciót akarom használni
        public void SetData(string Könyvtár_adat)
        {
            if (this.Könyvtár_adat == null)
            {
                this.Könyvtár_adat = Könyvtár_adat;
            }
        }

        private void Ablak_Szerszám_Load(object sender, EventArgs e)
        {
            Start();
        }

        private void Start()
        {
            try
            {
                if (Könyvtár_adat.Trim() == "Szerszám")
                    this.Text = "Szerszám Nyilvántartás";
                else
                    this.Text = "Helység tartozék nyilvántartás";

                //Ha az első karakter "R" akkor az új jogosultságkiosztást használjuk
                //ha nem akkor a régit használjuk
                if (Program.PostásJogkör.Substring(0, 1) == "R")
                {
                    TelephelyekFeltöltéseÚj();
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
                }
                else
                {
                    Telephelyekfeltöltése();
                    Jogosultságkiosztás();
                }

                // hozzáadjuk az előírt értékeket
                Adat_Szerszám_Könyvtörzs Adat;
                Adat = new Adat_Szerszám_Könyvtörzs("Érkezett", "Új eszközök beérkeztetése", "_", "_", false);
                KézKönyv.Döntés(Cmbtelephely.Text.Trim(), Könyvtár_adat, Adat);

                Adat = new Adat_Szerszám_Könyvtörzs("Raktár", "Szerszámraktárban lévő anyagok és eszközök", "_", "_", false);
                KézKönyv.Döntés(Cmbtelephely.Text.Trim(), Könyvtár_adat, Adat);

                Adat = new Adat_Szerszám_Könyvtörzs("Selejt", "Leselejtezett", "_", "_", false);
                KézKönyv.Döntés(Cmbtelephely.Text.Trim(), Könyvtár_adat, Adat);

                Adat = new Adat_Szerszám_Könyvtörzs("Selejtre", "Selejtezésre előkészítés", "_", "_", false);
                KézKönyv.Döntés(Cmbtelephely.Text.Trim(), Könyvtár_adat, Adat);



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
                if (Cmbtelephely.Items.Cast<string>().Contains(Program.PostásTelephely))
                    Cmbtelephely.Text = Program.PostásTelephely;
                else
                    Cmbtelephely.Text = Cmbtelephely.Items[0].ToStrTrim();
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

                if (Könyvtár_adat.Trim() == "Szerszám")
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

        private void Cmbtelephely_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                Cmbtelephely.Text = Cmbtelephely.Items[Cmbtelephely.SelectedIndex].ToStrTrim();
                if (Cmbtelephely.Text.Trim() == "") return;
                //Ha az első karakter "R" akkor az új jogosultságkiosztást használjuk
                //ha nem akkor a régit használjuk
                if (Program.PostásJogkör.Substring(0, 1) == "R")
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
                else
                {

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
                Alap_Aktív.Checked = Adat.Státus == 1;
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
                    KézSzerszámCikk.Módosítás(Cmbtelephely.Text.Trim(), Könyvtár_adat, Adat);
                    MessageBox.Show("Az adatok módosítás megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    KézSzerszámCikk.Rögzítés(Cmbtelephely.Text.Trim(), Könyvtár_adat, Adat);
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

                MyX.DataGridViewToXML(fájlexc, Alap_tábla);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyF.Megnyitás(fájlexc);
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

                Adat_Szerszám_Könyvtörzs Adat = new Adat_Szerszám_Könyvtörzs(Könyv_szám.Text.Trim(),
                                                                             Könyv_megnevezés.Text.Trim(),
                                                                             Könyv_Felelős1.Text.Trim(),
                                                                             Könyv_Felelős2.Text.Trim(),
                                                                             Könyv_Törlés.Checked);
                Adat_Szerszám_Könyvtörzs Elem = (from a in AdatokKönyv
                                                 where a.Szerszámkönyvszám == Könyv_szám.Text.Trim()
                                                 select a).FirstOrDefault();

                if (Elem == null)
                    KézKönyv.Rögzítés(Cmbtelephely.Text.Trim(), Könyvtár_adat, Adat);
                else
                    KézKönyv.Módosítás(Cmbtelephely.Text.Trim(), Könyvtár_adat, Adat);

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

                MyX.DataGridViewToXML(fájlexc, Könyv_tábla);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyF.Megnyitás(fájlexc);
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

                MyX.DataGridViewToXML(fájlexc, Lekérd_Tábla);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyF.Megnyitás(fájlexc);
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
                            string könyvtár = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                            fájlexc = $@"{könyvtár}\Szerszámköny_Leltár_{szerszámkönyszám}_{Program.PostásNév.Trim()}.xlsx";
                            if (File.Exists(fájlexc)) File.Delete(fájlexc);

                            Holtart.Lép();
                            // megnyitjuk az excelt
                            string munkalap = "Munka1";
                            MyX.ExcelLétrehozás(munkalap);
                            MyX.Munkalap_betű(munkalap, BeBetű11);

                            MyX.Oszlopszélesség(munkalap, "A:A", 23);
                            MyX.Oszlopszélesség(munkalap, "B:B", 54);
                            MyX.Oszlopszélesség(munkalap, "C:D", 17);
                            MyX.Oszlopszélesség(munkalap, "E:E", 14);
                            MyX.Oszlopszélesség(munkalap, "F:F", 16);
                            MyX.Kiir(Szervezet1.Trim(), "a1");
                            MyX.Kiir(Szervezet2.Trim(), "a2");
                            MyX.Kiir(Szervezet3.Trim(), "a3");
                            MyX.Betű(munkalap, "a1:a3", BeBetű11V);
                            Holtart.Lép();
                            MyX.Egyesít(munkalap, "a5:f5");
                            MyX.Betű(munkalap, "a5", BeBetű16V);
                            MyX.Kiir("Egyéni Szerszám nyilvántartó lap", "a5");
                            Holtart.Lép();
                            MyX.Egyesít(munkalap, "b7:E7");
                            MyX.Egyesít(munkalap, "b9:E9");
                            MyX.Egyesít(munkalap, "b11:E11");
                            MyX.Kiir("Könyvszám:", "a7");
                            MyX.Kiir("Könyv megnevezése:", "a9");
                            MyX.Kiir("Könyvért felelős", "a11");
                            Holtart.Lép();
                            // beírjuk a szerszámkönyv adatokat
                            Adat_Szerszám_Könyvtörzs Adat = (from a in AdatokKönyv
                                                             where a.Szerszámkönyvszám == szerszámkönyszám.Trim()
                                                             select a).FirstOrDefault();
                            MyX.Kiir(Adat.Szerszámkönyvszám, "b7");
                            MyX.Kiir(Adat.Szerszámkönyvnév, "b9");
                            MyX.Kiir(Adat.Felelős1, "b11");
                            MyX.Kiir(Adat.Felelős2, "b13");
                            Holtart.Lép();
                            // elkészítjük a fejlécet
                            MyX.Kiir("Nyilvántartásiszám:", "a15");
                            MyX.Kiir("Szerszám megnevezése:", "b15");
                            MyX.Kiir("Méret:", "c15");
                            MyX.Kiir("Gyáriszám:", "e15");
                            MyX.Kiir("Mennyiség:", "d15");
                            MyX.Kiir("Felvétel dátuma:", "f15");
                            // beírjuk a felvett szerszámokat
                            int sor = 16;
                            int oszlop;
                            {     // tartalom kiírása
                                for (sor = 0; sor <= Lekérd_Tábla.RowCount - 1; sor++)
                                {
                                    for (oszlop = 0; oszlop <= 5; oszlop++)
                                        MyX.Kiir(Lekérd_Tábla.Rows[sor].Cells[oszlop].Value.ToString(), MyF.Oszlopnév(oszlop + 1) + (sor + 16).ToString());
                                    Holtart.Lép();
                                }
                            }
                            sor = Lekérd_Tábla.Rows.Count + 15;

                            // keretezünk
                            MyX.Rácsoz(munkalap, $"a15:f{sor}");
                            MyX.Rácsoz(munkalap, "a15:f15");

                            sor += 2;
                            MyX.Kiir("Kelt:" + DateTime.Today.ToString("yyyy.MM.dd"), $"A{sor}");
                            sor += 2;
                            MyX.Kiir("A felsorolt szerszám(oka)t használatra felvettem.", $"A{sor}");
                            sor += 2;
                            MyX.Egyesít(munkalap, $"C{sor}:F{sor}");
                            MyX.Kiir("Dolgozó aláírása", $"C{sor}");
                            // pontozás az aláírásnak
                            MyX.Pontvonal(munkalap, $"C{sor}:F{sor}");
                            Holtart.Lép();
                            sor += 5;
                            MyX.Egyesít(munkalap, $"C{sor}:F{sor}");
                            MyX.Kiir("Raktáros", $"C{sor}");
                            // pontozás az aláírásnak
                            MyX.Pontvonal(munkalap, $"C{sor}:F{sor}");

                            // nyomtatási beállítások
                            Beállítás_Nyomtatás BeNyom = new Beállítás_Nyomtatás
                            {
                                Munkalap = munkalap,
                                NyomtatásiTerület = $"a1:f{sor}",
                                LapSzéles = 1,
                                FejlécJobb = DateTime.Now.ToString("yyyy.MM.dd HH:mm"),
                                LáblécKözép = "&P/&N"

                            };
                            MyX.NyomtatásiTerület_részletes(munkalap, BeNyom);

                            // bezárjuk az Excel-t
                            Holtart.Lép();
                            MyX.ExcelMentés(fájlexc);
                            MyX.ExcelBezárás();
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
                if (OpenFileDialogPI.ShowDialogEllenőr(OpenFileDialog1) == DialogResult.OK)
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
                if (OpenFileDialogPI.ShowDialogEllenőr(OpenFileDialog1) == DialogResult.OK)
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
                if (Napló_Tábla.Rows.Count <= 0) return;
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

                MyX.DataGridViewToXML(fájlexc, Napló_Tábla);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyF.Megnyitás(fájlexc);
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
                string munkalap = "Munka1";
                MyX.ExcelLétrehozás(munkalap);
                MyX.Munkalap_betű(munkalap, BeBetű11);


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
                MyX.Oszlopszélesség(munkalap, "a:a", 23);
                MyX.Oszlopszélesség(munkalap, "b:b", 54);
                MyX.Oszlopszélesség(munkalap, "c:d", 17);
                MyX.Oszlopszélesség(munkalap, "e:e", 14);
                MyX.Kiir(szervezet3, "a1");
                MyX.Kiir(szervezet4, "a2");
                MyX.Kiir(szervezet5, "a3");
                MyX.Betű(munkalap, "a1:a3", BeBetű11V);

                MyX.Egyesít(munkalap, "a5:E5");
                MyX.Betű(munkalap, "a5", BeBetű16V);

                switch (eset)
                {
                    case 1:
                        {
                            MyX.Kiir("Bizonylat a Szerszám felvételről", "a5");
                            break;
                        }
                    case 2:
                        {
                            MyX.Kiir("Bizonylat a Szerszám leadásáról", "a5");
                            break;
                        }
                    case 3:
                        {
                            MyX.Kiir("Bizonylat a selejtessévált Szerszám leadásáról", "a5");
                            break;
                        }
                }
                MyX.Egyesít(munkalap, "b7:E7");
                MyX.Egyesít(munkalap, "b9:E9");
                MyX.Egyesít(munkalap, "b11:E11");
                MyX.Kiir("Könyvszám:", "a7");
                MyX.Kiir("Könyv megnevezése:", "a9");
                MyX.Kiir("Könyvért felelős", "a11");

                // beírjuk a védőkönyv adatokat
                Adat_Szerszám_Könyvtörzs ElemKönyv = (from a in AdatokKönyv
                                                      where a.Szerszámkönyvszám == milyenkönyv
                                                      select a).FirstOrDefault();
                if (ElemKönyv != null)
                {
                    MyX.Kiir(ElemKönyv.Szerszámkönyvszám, "b7");
                    MyX.Kiir(ElemKönyv.Szerszámkönyvnév, "b9");
                    MyX.Kiir(ElemKönyv.Felelős1, "b11");
                    MyX.Kiir(ElemKönyv.Felelős2, "b13");
                }


                // elkészítjük a fejlécet
                MyX.Kiir("Nyilvántartásiszám:", "a15");
                MyX.Kiir("Szerszám megnevezése:", "b15");
                MyX.Kiir("Méret:", "c15");
                MyX.Kiir("Gyáriszám:", "d15");
                MyX.Kiir("Mennyiség:", "e15");
                // beírjuk a felvett szerszámokat
                int sor = 16;
                int hanyadik = 0;
                Holtart.Be(Napló_Tábla.Rows.Count + 2);
                for (int j = 0; j <= Napló_Tábla.Rows.Count - 1; j++)
                {
                    if (Napló_Tábla.Rows[j].Selected)
                    {
                        // ha ki van jelölve
                        MyX.Kiir(Napló_Tábla.Rows[j].Cells[1].Value.ToStrTrim(), $"B{sor}");
                        MyX.Kiir(Napló_Tábla.Rows[j].Cells[3].Value.ToString(), $"E{sor}");
                        MyX.Kiir(Napló_Tábla.Rows[j].Cells[0].Value.ToString(), $"A{sor}");
                        if (Napló_Tábla.Rows[j].Cells[2].Value.ToStrTrim() != "0")
                        {
                            MyX.Kiir(Napló_Tábla.Rows[j].Cells[2].Value.ToStrTrim(), $"C{sor}");
                        }
                        else
                        {
                            MyX.Kiir("-", $"C{sor}");
                        }
                        if (Napló_Tábla.Rows[j].Cells[4].Value.ToStrTrim() != "0")
                        {
                            MyX.Kiir(Napló_Tábla.Rows[j].Cells[4].Value.ToStrTrim(), $"D{sor}");
                        }
                        else
                        {
                            MyX.Kiir("-", $"D{sor}");
                        }
                        sor += 1;
                        hanyadik += 1;
                    }
                    Holtart.Lép();
                }

                // keretezünk
                MyX.Rácsoz(munkalap, $"a15:e{sor}");
                MyX.Rácsoz(munkalap, "a15:e15");

                sor += 2;
                MyX.Kiir($"Kelt:{DateTime.Now:yyyy.MM.dd}", $"A{sor}");
                sor += 2;
                switch (eset)
                {
                    case 1:
                        {
                            MyX.Kiir("A felsorolt Szerszám(okat) használatra felvettem.", $"A{sor}");
                            break;
                        }
                    case 2:
                        {
                            MyX.Kiir("A felsorolt Szerszám(okat) selejtezés / javítás céljából / tovább használatra leadtam.", $"A{sor}");
                            break;
                        }
                    case 3:
                        {
                            MyX.Kiir("A felsorolt Szerszám(okat) selejtezés / javítás céljából leadtam.", $"A{sor}");
                            break;
                        }
                }
                sor += 2;
                MyX.Egyesít(munkalap, $"C{sor}:E{sor}");
                MyX.Kiir("Dolgozó aláírása", $"C{sor}");
                // pontozás az aláírásnak
                MyX.Pontvonal(munkalap, $"C{sor}:E{sor}");

                sor += 2;
                switch (eset)
                {
                    case 1:
                        {
                            MyX.Kiir("A dolgozónak kiadtam  a felsorolt szerszámo(ka)t.", $"A{sor}");
                            break;
                        }
                    case 2:
                        {
                            MyX.Kiir("A dolgozótól visszavettem a fenn felsorolt szerszámo(ka)t.", $"A{sor}");
                            break;
                        }
                    case 3:
                        {
                            MyX.Kiir("A dolgozótól visszavettem a fenn felsorolt szerszámo(ka)t.", $"A{sor}");
                            break;
                        }
                }

                sor += 2;
                MyX.Egyesít(munkalap, $"C{sor}:E{sor}");
                MyX.Kiir("Raktáros", $"C{sor}");
                // pontozás az aláírásnak
                MyX.Pontvonal(munkalap, $"C{sor}:E{sor}");

                if (eset == 3)
                {
                    sor += 2;
                    MyX.Egyesít(munkalap, $"A{sor}:E{sor}");
                    MyX.Kiir("A leadott szerszámo(ka)t megvizsgáltam és megállapítottam ,hogy a", $"A{sor}");
                    sor += 2;
                    MyX.Egyesít(munkalap, $"A{sor}:E{sor}");
                    MyX.Kiir("kártérítési felelősség fenn áll.         /      kártérítési felelősséggel a dolgozó nem tartozik.", $"A{sor}");
                    sor += 2;
                    MyX.Egyesít(munkalap, $"C{sor}:E{sor}");
                    MyX.Kiir("Munkahelyivezető", $"C{sor}");
                    // pontozás az aláírásnak
                    MyX.Pontvonal(munkalap, $"C{sor}:E{sor}");
                }
                // nyomtatási beállítások
                Beállítás_Nyomtatás BeNyom = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"a1:f{sor}",
                    LapSzéles = 1,
                    FejlécJobb = DateTime.Now.ToString("yyyy.MM.dd HH:mm"),
                    LáblécKözép = "&P/&N"

                };
                MyX.NyomtatásiTerület_részletes(munkalap, BeNyom);

                // bezárjuk az Excel-t
                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();
                Holtart.Ki();
                List<string> Fájlok = new List<string> { fájlexc };

                if (Napló_Nyomtat.Checked)
                {
                    MyF.ExcelNyomtatás(Fájlok);
                    MessageBox.Show("A bizonylatok nyomtatása elkészült.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (Napló_Fájltöröl.Checked)
                    File.Delete(fájlexc);
                else
                {
                    MyF.Megnyitás(fájlexc);
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

                string azonostító = SzerszámAzonosító.Text.Trim();
                string honann = Honnan.Text.Trim();
                string hova = Hova.Text.Trim();

                Adat_Szerszám_Napló NaplóAdat = new Adat_Szerszám_Napló(azonostító, honann, hova, mennyiség);
                KézNapló.Rögzítés(Könyvtár_adat, Cmbtelephely.Text.Trim(), DateTime.Today.Year, NaplóAdat);
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
                        KézKönyvelés.Módosítás(Cmbtelephely.Text.Trim(), Könyvtár_adat, Adat);
                        MessageBox.Show("Módosítás megtörtént !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    else
                    {
                        KézKönyvelés.Törlés(Cmbtelephely.Text.Trim(), Könyvtár_adat, Adat);
                        MessageBox.Show("Törlés megtörtént !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    mennyiség = int.Parse(Mennyiség.Text.Trim());
                    Szerszámkönyvszám = Hova.Text.Trim();
                    Adat_Szerszám_Könyvelés Adat = new Adat_Szerszám_Könyvelés(mennyiség, DateTime.Now, SzerszámAzonosító.Text.Trim(), Szerszámkönyvszám);

                    KézKönyvelés.Rögzítés(Cmbtelephely.Text.Trim(), Könyvtár_adat, Adat);
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
                if (Napló_Tábla.SelectedRows.Count < 1) throw new HibásBevittAdat("Nincs kiválasztva a táblázatban nyomtatandó elem.");

                List<Adat_Szerszám_Könyvtörzs> Könyv_Törzs = KézKönyv.Lista_Adatok(Cmbtelephely.Text.Trim(), Könyvtár_adat);

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
                MyX.ExcelLétrehozás(munkalap);
                MyX.Munkalap_betű(munkalap, BeBetűC12);
                //Nyomtatvány eleje
                MyX.Oszlopszélesség(munkalap, "a:a", 5);
                MyX.Oszlopszélesség(munkalap, "b:b", 15);
                MyX.Oszlopszélesség(munkalap, "c:c", 6);
                MyX.Oszlopszélesség(munkalap, "d:d", 30);
                MyX.Oszlopszélesség(munkalap, "e:e", 12);
                MyX.Oszlopszélesség(munkalap, "f:f", 13);
                MyX.Oszlopszélesség(munkalap, "g:g", 10);
                MyX.Oszlopszélesség(munkalap, "h:h", 22);
                MyX.Oszlopszélesség(munkalap, "i:i", 16);
                MyX.Oszlopszélesség(munkalap, "j:j", 10);
                MyX.Oszlopszélesség(munkalap, "k:k", 30);
                MyX.Oszlopszélesség(munkalap, "l:l", 16);


                MyX.VékonyFelső(munkalap, $"a{sor}:l{sor}");

                MyX.Sormagasság(munkalap, $"{sor}:{sor + 3}", 18);

                sor++;// ez a második sor
                MyX.Egyesít(munkalap, $"j{sor}:k{sor}");
                MyX.Kiir("Iktatószám:", $"j{sor}:k{sor}");
                MyX.Betű(munkalap, $"j{sor}:k{sor}", BeBetűC10V);

                MyX.Igazít_függőleges(munkalap, $"j{sor}:k{sor}", "felső");
                MyX.Igazít_vízszintes(munkalap, $"j{sor}:k{sor}", "jobb");

                sor++;//harmadik sor

                sor++; //negyedik sor    
                MyX.Egyesít(munkalap, $"e{sor}:i{sor}");
                MyX.Kiir("Tárgyi eszközök költséghelyen belüli áthelyezése", $"e{sor}:i{sor}");
                MyX.Betű(munkalap, $"e{sor}:i{sor}", BeBetűC10V);
                MyX.Igazít_függőleges(munkalap, $"e{sor}:i{sor}", "közép");
                MyX.Igazít_vízszintes(munkalap, $"e{sor}:i{sor}", "közép");

                sor++;//ötödik sor
                MyX.Sormagasság(munkalap, $"{sor}:{sor}", 20);

                sor++;//hatodik sor
                MyX.Sormagasság(munkalap, $"{sor}:{sor}", 16);

                MyX.Egyesít(munkalap, $"a{sor}:a{sor + 1}");
                MyX.Kiir("Ssz.", $"a{sor}:a{sor + 1}");
                MyX.Betű(munkalap, $"a{sor}:a{sor + 1}", BeBetűC12V);
                MyX.Igazít_függőleges(munkalap, $"a{sor}:a{sor + 1}", "közép");
                MyX.Igazít_vízszintes(munkalap, $"a{sor}:a{sor + 1}", "közép");
                MyX.Vékonykeret(munkalap, $"a{sor}:a{sor + 1}");

                MyX.Egyesít(munkalap, $"b{sor}:f{sor}");
                MyX.Kiir("Alapadatok", $"b{sor}:f{sor}");
                MyX.Betű(munkalap, $"b{sor}:f{sor}", BeBetűC12V);
                MyX.Igazít_függőleges(munkalap, $"b{sor}:f{sor}", "közép");
                MyX.Igazít_vízszintes(munkalap, $"b{sor}:f{sor}", "közép");
                MyX.Vékonykeret(munkalap, $"b{sor}:f{sor}");

                MyX.Egyesít(munkalap, $"g{sor}:i{sor}");
                MyX.Kiir("Honnan", $"g{sor}:i{sor}");
                MyX.Betű(munkalap, $"g{sor}:i{sor}", BeBetűC12V);
                MyX.Igazít_függőleges(munkalap, $"g{sor}:i{sor}", "közép");
                MyX.Igazít_vízszintes(munkalap, $"g{sor}:i{sor}", "közép");
                MyX.Vékonykeret(munkalap, $"g{sor}:i{sor}");

                MyX.Egyesít(munkalap, $"j{sor}:l{sor}");
                MyX.Kiir("Hova", $"j{sor}:l{sor}");
                MyX.Betű(munkalap, $"j{sor}:l{sor}", BeBetűC12V);
                MyX.Igazít_függőleges(munkalap, $"j{sor}:l{sor}", "közép");
                MyX.Igazít_vízszintes(munkalap, $"j{sor}:l{sor}", "közép");
                MyX.Vékonykeret(munkalap, $"j{sor}:l{sor}");

                sor++;// ez a hetedik sor

                MyX.Kiir("Eszközszám", $"b{sor}");
                MyX.Betű(munkalap, $"b{sor}", BeBetűC12V);
                MyX.Igazít_függőleges(munkalap, $"b{sor}", "közép");
                MyX.Igazít_vízszintes(munkalap, $"b{sor}", "közép");
                MyX.Vékonykeret(munkalap, $"b{sor}");

                MyX.Kiir("Alsz.", $"c{sor}");
                MyX.Betű(munkalap, $"c{sor}", BeBetűC12V);
                MyX.Igazít_függőleges(munkalap, $"c{sor}", "közép");
                MyX.Igazít_vízszintes(munkalap, $"c{sor}", "közép");
                MyX.Vékonykeret(munkalap, $"c{sor}");

                MyX.Kiir("Megnevezés", $"d{sor}");
                MyX.Betű(munkalap, $"d{sor}", BeBetűC12V);
                MyX.Igazít_függőleges(munkalap, $"d{sor}", "közép");
                MyX.Igazít_vízszintes(munkalap, $"d{sor}", "közép");
                MyX.Vékonykeret(munkalap, $"d{sor}");

                MyX.Kiir("Gyártási szám", $"e{sor}");
                MyX.Betű(munkalap, $"e{sor}", BeBetűC12V);
                MyX.Sortörésseltöbbsorba(munkalap, $"e{sor}");
                MyX.Igazít_függőleges(munkalap, $"e{sor}", "közép");
                MyX.Igazít_vízszintes(munkalap, $"e{sor}", "közép");
                MyX.Vékonykeret(munkalap, $"e{sor}");

                MyX.Kiir("Leltárszám", $"f{sor}");
                MyX.Betű(munkalap, $"f{sor}", BeBetűC12V);
                MyX.Igazít_függőleges(munkalap, $"f{sor}", "közép");
                MyX.Igazít_vízszintes(munkalap, $"f{sor}", "közép");
                MyX.Vékonykeret(munkalap, $"f{sor}");

                MyX.Kiir("Telephely", $"g{sor}");
                MyX.Betű(munkalap, $"g{sor}", BeBetűC12V);
                MyX.Igazít_függőleges(munkalap, $"g{sor}", "közép");
                MyX.Igazít_vízszintes(munkalap, $"g{sor}", "közép");
                MyX.Vékonykeret(munkalap, $"g{sor}");

                MyX.Kiir("Helyiség", $"h{sor}");
                MyX.Betű(munkalap, $"h{sor}", BeBetűC12V);
                MyX.Igazít_függőleges(munkalap, $"h{sor}", "közép");
                MyX.Igazít_vízszintes(munkalap, $"h{sor}", "közép");
                MyX.Vékonykeret(munkalap, $"h{sor}");

                MyX.Kiir("Személyügyi törzsszám \n (9/b. számú melléklet csatolandó)", $"i{sor}");
                MyX.Betű(munkalap, $"i{sor}", BeBetűC12V);
                MyX.Sortörésseltöbbsorba(munkalap, $"i{sor}");
                MyX.Igazít_függőleges(munkalap, $"i{sor}", "közép");
                MyX.Igazít_vízszintes(munkalap, $"i{sor}", "közép");
                MyX.Vékonykeret(munkalap, $"i{sor}");

                MyX.Kiir("Telephely", $"j{sor}");
                MyX.Betű(munkalap, $"j{sor}", BeBetűC12V);
                MyX.Igazít_függőleges(munkalap, $"j{sor}", "közép");
                MyX.Igazít_vízszintes(munkalap, $"j{sor}", "közép");
                MyX.Vékonykeret(munkalap, $"j{sor}");

                MyX.Kiir("Helyiség", $"k{sor}");
                MyX.Betű(munkalap, $"k{sor}", BeBetűC12V);
                MyX.Igazít_függőleges(munkalap, $"k{sor}", "közép");
                MyX.Igazít_vízszintes(munkalap, $"k{sor}", "közép");
                MyX.Vékonykeret(munkalap, $"k{sor}");

                MyX.Kiir("Személyügyi törzsszám \n (9/b. számú melléklet csatolandó)", $"l{sor}");
                MyX.Betű(munkalap, $"l{sor}", BeBetűC12V);
                MyX.Sortörésseltöbbsorba(munkalap, $"l{sor}");
                MyX.Sormagasság(munkalap, $"l{sor}", 77);
                MyX.Igazít_függőleges(munkalap, $"l{sor}", "közép");
                MyX.Igazít_vízszintes(munkalap, $"l{sor}", "közép");
                MyX.Vékonykeret(munkalap, $"l{sor}");


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
                        MyX.Sormagasság(munkalap, $"{sor}:{sor}", 30);

                        MyX.Kiir((i + 1).ToString(), $"a{sor}");//sorszám
                        MyX.Sormagasság(munkalap, $"a{sor}", 30);
                        MyX.Igazít_függőleges(munkalap, $"a{sor}", "közép");
                        MyX.Igazít_vízszintes(munkalap, $"a{sor}", "közép");
                        MyX.Vékonykeret(munkalap, $"a{sor}");

                        MyX.Betű(munkalap, $"b{sor}", BeBetűC12K);
                        MyX.Kiir(eszközszám, $"b{sor}"); //Eszközszám
                        MyX.Igazít_függőleges(munkalap, $"b{sor}", "közép");
                        MyX.Igazít_vízszintes(munkalap, $"b{sor}", "közép");
                        MyX.Vékonykeret(munkalap, $"b{sor}");

                        MyX.Kiir(EgyElem.Alszám.Trim(), $"c{sor}"); //Alszám
                        MyX.Igazít_függőleges(munkalap, $"c{sor}", "közép");
                        MyX.Igazít_vízszintes(munkalap, $"c{sor}", "közép");
                        MyX.Vékonykeret(munkalap, $"c{sor}");

                        MyX.Kiir(EgyElem.Megnevezés.Trim(), $"d{sor}"); //Megnevezés
                        MyX.Igazít_függőleges(munkalap, $"d{sor}", "közép");
                        MyX.Igazít_vízszintes(munkalap, $"d{sor}", "közép");
                        MyX.Vékonykeret(munkalap, $"d{sor}");

                        MyX.Kiir(EgyElem.Gyártási_szám.Trim(), $"e{sor}");//Gyártásiszám
                        MyX.Igazít_függőleges(munkalap, $"e{sor}", "közép");
                        MyX.Igazít_vízszintes(munkalap, $"e{sor}", "közép");
                        MyX.Vékonykeret(munkalap, $"e{sor}");

                        MyX.Kiir(EgyElem.Leltárszám.Trim(), $"f{sor}");//Leltáriszám
                        MyX.Igazít_függőleges(munkalap, $"f{sor}", "közép");
                        MyX.Igazít_vízszintes(munkalap, $"f{sor}", "közép");
                        MyX.Vékonykeret(munkalap, $"f{sor}");

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
                            MyX.Kiir(telephely, $"g{sor}");//telephely
                            MyX.Igazít_függőleges(munkalap, $"g{sor}", "közép");
                            MyX.Igazít_vízszintes(munkalap, $"g{sor}", "közép");
                            MyX.Vékonykeret(munkalap, $"g{sor}");

                            MyX.Kiir(helyiség, $"h{sor}");//helyiség
                            MyX.Igazít_függőleges(munkalap, $"h{sor}", "közép");
                            MyX.Igazít_vízszintes(munkalap, $"h{sor}", "közép");
                            MyX.Vékonykeret(munkalap, $"h{sor}");

                            MyX.Kiir(HR_azonosító, $"i{sor}");// HR azonosító
                            MyX.Igazít_függőleges(munkalap, $"i{sor}", "közép");
                            MyX.Igazít_vízszintes(munkalap, $"i{sor}", "közép");
                            MyX.Vékonykeret(munkalap, $"i{sor}");
                        }
                        else
                        {
                            MyX.Kiir(telephely, $"g{sor}");//telephely
                            MyX.Igazít_függőleges(munkalap, $"g{sor}", "közép");
                            MyX.Igazít_vízszintes(munkalap, $"g{sor}", "közép");
                            MyX.Vékonykeret(munkalap, $"g{sor}");

                            MyX.Kiir(helyiség, $"h{sor}");//helyiség
                            MyX.Igazít_függőleges(munkalap, $"h{sor}", "közép");
                            MyX.Igazít_vízszintes(munkalap, $"h{sor}", "közép");
                            MyX.Vékonykeret(munkalap, $"h{sor}");

                            MyX.Kiir(HR_azonosító, $"i{sor}");// HR azonosító
                            MyX.Igazít_függőleges(munkalap, $"i{sor}", "közép");
                            MyX.Igazít_vízszintes(munkalap, $"i{sor}", "közép");
                            MyX.Vékonykeret(munkalap, $"i{sor}");
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
                            MyX.Kiir(telephely, $"j{sor}");//telephely
                            MyX.Igazít_függőleges(munkalap, $"j{sor}", "közép");
                            MyX.Igazít_vízszintes(munkalap, $"j{sor}", "közép");
                            MyX.Vékonykeret(munkalap, $"j{sor}");

                            MyX.Kiir(helyiség, $"k{sor}");//helyiség
                            MyX.Igazít_függőleges(munkalap, $"k{sor}", "közép");
                            MyX.Igazít_vízszintes(munkalap, $"k{sor}", "közép");
                            MyX.Vékonykeret(munkalap, $"k{sor}");

                            MyX.Kiir(HR_azonosító, $"l{sor}");//hr azonosító
                            MyX.Igazít_függőleges(munkalap, $"l{sor}", "közép");
                            MyX.Igazít_vízszintes(munkalap, $"l{sor}", "közép");
                            MyX.Vékonykeret(munkalap, $"l{sor}");
                        }
                        else
                        {
                            MyX.Kiir(telephely, $"j{sor}");//telephely
                            MyX.Igazít_függőleges(munkalap, $"j{sor}", "közép");
                            MyX.Igazít_vízszintes(munkalap, $"j{sor}", "közép");
                            MyX.Vékonykeret(munkalap, $"j{sor}");

                            MyX.Kiir(helyiség, $"k{sor}");//helyiség
                            MyX.Igazít_függőleges(munkalap, $"k{sor}", "közép");
                            MyX.Igazít_vízszintes(munkalap, $"k{sor}", "közép");
                            MyX.Vékonykeret(munkalap, $"k{sor}");

                            MyX.Kiir(HR_azonosító, $"l{sor}");//hr azonosító
                            MyX.Igazít_függőleges(munkalap, $"l{sor}", "közép");
                            MyX.Igazít_vízszintes(munkalap, $"l{sor}", "közép");
                            MyX.Vékonykeret(munkalap, $"l{sor}");
                        }
                    }
                }
                MyX.Oszlopszélesség(munkalap, "G:G");
                MyX.Oszlopszélesség(munkalap, "J:J");
                MyX.Oszlopszélesség(munkalap, "D:D");
                MyX.Oszlopszélesség(munkalap, "E:E");
                MyX.Oszlopszélesség(munkalap, "H:H");
                MyX.Oszlopszélesség(munkalap, "K:K");
                sor++;//kilencedik sor

                sor += 3;//tizedik sor
                MyX.Sormagasság(munkalap, $"{sor}:{sor}", 16);

                MyX.Egyesít(munkalap, $"a{sor}:e{sor}");
                MyX.Kiir($"Budapest, {Napló_Dátumtól.Value.Year} év {Napló_Dátumtól.Value:MM} hó {Napló_Dátumtól.Value:dd} nap", $"a{sor}:e{sor}");
                MyX.Igazít_függőleges(munkalap, $"a{sor}:e{sor}", "közép");
                MyX.Igazít_vízszintes(munkalap, $"a{sor}:e{sor}", "bal");

                MyX.Egyesít(munkalap, $"i{sor}:j{sor}");
                MyX.Kiir("Nyilvántartó neve,aláírása:", $"i{sor}");

                sor++;
                MyX.Egyesít(munkalap, $"k{sor}:l{sor}");
                MyX.Aláírásvonal(munkalap, $"k{sor}:l{sor}");

                DolgozóListaFeltöltés();
                Adat_Dolgozó_Alap Nyilvántartó = (from a in AdatokDolgozó
                                                  where a.Bejelentkezésinév == Program.PostásNév
                                                  select a).FirstOrDefault();

                if (Nyilvántartó != null && Nyilvántartó.DolgozóNév != null)
                    MyX.Kiir(Nyilvántartó.DolgozóNév.Trim(), $"k{sor}");

                MyX.Igazít_függőleges(munkalap, $"i{sor}:l{sor}", "közép");
                MyX.Igazít_vízszintes(munkalap, $"i{sor}:l{sor}", "bal");

                List<Adat_Szerszám_FejLáb> Adatok = KézSzerszámFejLáb.Lista_Adatok();
                Adat_Szerszám_FejLáb Adat = Adatok.Where(a => a.Típus == "9A").FirstOrDefault();

                if (Adat != null)
                {
                    Beállítás_Nyomtatás BeNyom = new Beállítás_Nyomtatás
                    {
                        Munkalap = munkalap,
                        NyomtatásiTerület = $"A1:L{sor}",
                        Álló = false,
                        FejlécBal = Adat.Fejléc_Bal,
                        FejlécKözép = Adat.Fejléc_Közép,
                        FejlécJobb = Adat.Fejléc_Jobb,
                        LáblécBal = Adat.Lábléc_Bal,
                        LáblécKözép = Adat.Lábléc_Közép,
                        LáblécJobb = Adat.Lábléc_Jobb,
                        BalMargó = 18,
                        JobbMargó = 18,
                        AlsóMargó = 0,
                        FelsőMargó = 17,
                        FejlécMéret = 11,
                        LáblécMéret = 4
                    };
                    MyX.NyomtatásiTerület_részletes(munkalap, BeNyom);
                }
                // bezárjuk az Excel-t
                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();

                List<string> Fájlok = new List<string> { fájlexc };
                if (Napló_Nyomtat.Checked)
                {
                    MyF.ExcelNyomtatás(Fájlok);
                    MessageBox.Show("A bizonylatok nyomtatása elkészült.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (Napló_Fájltöröl.Checked)
                    File.Delete(fájlexc);
                else
                {
                    MyF.Megnyitás(fájlexc);
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
                    HonnanDarabol = IdeigHonnan.Felelős1.Split('=');
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
                MyX.ExcelLétrehozás(munkalap);
                MyX.Munkalap_betű(munkalap, BeBetűC12);

                //Nyomtatvány eleje
                MyX.Oszlopszélesség(munkalap, "a:a", 5);
                MyX.Oszlopszélesség(munkalap, "b:b", 5);
                MyX.Oszlopszélesség(munkalap, "c:c", 10);
                MyX.Oszlopszélesség(munkalap, "d:d", 5);
                MyX.Oszlopszélesség(munkalap, "e:e", 8);
                MyX.Oszlopszélesség(munkalap, "f:f", 8);
                MyX.Oszlopszélesség(munkalap, "g:g", 8);
                MyX.Oszlopszélesség(munkalap, "h:h", 8);
                MyX.Oszlopszélesség(munkalap, "i:i", 13);
                MyX.Oszlopszélesség(munkalap, "j:j", 12);


                MyX.Sormagasság(munkalap, $"{sor}:{sor + 8}", 16);

                //első sor
                MyX.Kiir("Iktatószám:", $"i{sor}");

                MyX.Igazít_függőleges(munkalap, $"i{sor}", "közép");
                MyX.Igazít_vízszintes(munkalap, $"i{sor}", "bal");

                sor++; //második sor
                MyX.Egyesít(munkalap, $"b{sor}:j{sor}");
                MyX.Kiir("Átvételi elismervény", $"b{sor}:j{sor}");
                MyX.Betű(munkalap, $"b{sor}:j{sor}", BeBetűC12V);
                MyX.Igazít_függőleges(munkalap, $"b{sor}:j{sor}", "alsó");
                MyX.Igazít_vízszintes(munkalap, $"b{sor}:j{sor}", "közép");

                sor++; //harmdik sor
                MyX.Egyesít(munkalap, $"b{sor}:j{sor}");
                MyX.Kiir("személyi használatra kiadott eszközökről", $"b{sor}:j{sor}");
                MyX.Betű(munkalap, $"b{sor}:j{sor}", BeBetűC12V);
                MyX.Igazít_függőleges(munkalap, $"b{sor}:j{sor}", "alsó");
                MyX.Igazít_vízszintes(munkalap, $"b{sor}:j{sor}", "közép");

                sor++; //negyedik sor 

                sor++; //ötödik sor 
                MyX.Egyesít(munkalap, $"a{sor}:b{sor}");
                MyX.Kiir("Alurírott", $"a{sor}:b{sor}");
                MyX.Igazít_függőleges(munkalap, $"a{sor}:b{sor}", "alsó");
                MyX.Igazít_vízszintes(munkalap, $"a{sor}:b{sor}", "bal");


                MyX.Egyesít(munkalap, $"c{sor}:e{sor}");
                MyX.Kiir(DolgozóNév, $"c{sor}:e{sor}"); //dolgozónév
                MyX.Betű(munkalap, $"c{sor}:e{sor}", BeBetűC12V);
                MyX.Igazít_függőleges(munkalap, $"c{sor}:e{sor}", "alsó");
                MyX.Igazít_vízszintes(munkalap, $"c{sor}:e{sor}", "bal");

                MyX.Egyesít(munkalap, $"f{sor}:g{sor}");
                MyX.Kiir("(HR azonosító: ", $"f{sor}:g{sor}");
                MyX.Igazít_függőleges(munkalap, $"f{sor}:g{sor}", "alsó");
                MyX.Igazít_vízszintes(munkalap, $"f{sor}:g{sor}", "bal");

                MyX.Egyesít(munkalap, $"h{sor}:h{sor}");
                MyX.Kiir(HrAzonosító, $"h{sor}:h{sor}"); //Hr azonosító
                MyX.Igazít_függőleges(munkalap, $"h{sor}:h{sor}", "alsó");
                MyX.Igazít_vízszintes(munkalap, $"h{sor}:h{sor}", "jobb");

                MyX.Egyesít(munkalap, $"i{sor}:i{sor}");
                MyX.Kiir(",", $"i{sor}:i{sor}");
                MyX.Igazít_függőleges(munkalap, $"i{sor}:i{sor}", "alsó");
                MyX.Igazít_vízszintes(munkalap, $"i{sor}:i{sor}", "bal");


                sor++; //hatodik sor
                MyX.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyX.Kiir("felelős költséghely: ", $"a{sor}:c{sor}");
                MyX.Igazít_függőleges(munkalap, $"a{sor}:c{sor}", "alsó");
                MyX.Igazít_vízszintes(munkalap, $"a{sor}:c{sor}", "bal");

                MyX.Egyesít(munkalap, $"d{sor}:e{sor}");
                MyX.Kiir(EgyElem.Költséghely.Trim(), $"d{sor}:e{sor}"); //Költséghely
                MyX.Betű(munkalap, $"d{sor}:e{sor}", BeBetűC12V);
                MyX.Igazít_függőleges(munkalap, $"d{sor}:e{sor}", "alsó");
                MyX.Igazít_vízszintes(munkalap, $"d{sor}:e{sor}", "bal");

                MyX.Egyesít(munkalap, $"f{sor}:g{sor}");
                MyX.Kiir("szervezeti egység", $"f{sor}:g{sor}");
                MyX.Igazít_függőleges(munkalap, $"f{sor}:g{sor}", "alsó");
                MyX.Igazít_vízszintes(munkalap, $"f{sor}:g{sor}", "bal");

                MyX.Egyesít(munkalap, $"h{sor}:j{sor}");
                MyX.Kiir(szervezet, $"h{sor}:j{sor}");// Szervezeti egység
                MyX.Betű(munkalap, $"h{sor}:j{sor}", BeBetűC12V);
                MyX.Igazít_függőleges(munkalap, $"h{sor}:j{sor}", "alsó");
                MyX.Igazít_vízszintes(munkalap, $"h{sor}:j{sor}", "bal");


                sor++; //hetedik sor
                MyX.Egyesít(munkalap, $"a{sor}:h{sor}");
                MyX.Kiir("a mai naptól személyes használatra a következő eszköz(öket):", $"a{sor}:h{sor}");
                MyX.Igazít_függőleges(munkalap, $"a{sor}:h{sor}", "alsó");
                MyX.Igazít_vízszintes(munkalap, $"a{sor}:h{sor}", "bal");

                MyX.Egyesít(munkalap, $"i{sor}:i{sor}");
                MyX.Kiir(" átvettem / ", $"i{sor}:i{sor}");

                MyX.Igazít_függőleges(munkalap, $"i{sor}:i{sor}", "alsó");
                MyX.Igazít_vízszintes(munkalap, $"i{sor}:i{sor}", "bal");

                MyX.Egyesít(munkalap, $"j{sor}:j{sor}");
                MyX.Kiir("leadtam", $"j{sor}:j{sor}");
                MyX.Igazít_függőleges(munkalap, $"j{sor}:j{sor}", "alsó");
                MyX.Igazít_vízszintes(munkalap, $"j{sor}:j{sor}", "bal");

                if (Napló_Honnan.Text.Trim() == "Raktár")
                    MyX.Betű(munkalap, $"i{sor}:i{sor}", BeBetűC12V); //átvettem
                else
                    MyX.Betű(munkalap, $"j{sor}:j{sor}", BeBetűC12V); //leadtam


                sor++; //nyolcadik sor
                sor++; //kilencedik sor


                sor++; //tizedik sor
                MyX.Sormagasság(munkalap, $"{sor}:{sor}", 48);
                MyX.Rácsoz(munkalap, $"a{sor}:j{sor}");

                MyX.Egyesít(munkalap, $"a{sor}:a{sor}");
                MyX.Kiir("Sor- szám", $"a{sor}:a{sor}");
                MyX.Sortörésseltöbbsorba(munkalap, $"a{sor}:a{sor}");
                MyX.Betű(munkalap, $"a{sor}:a{sor}", BeBetűC12V);
                MyX.Igazít_függőleges(munkalap, $"a{sor}:a{sor}", "közép");
                MyX.Igazít_vízszintes(munkalap, $"a{sor}:a{sor}", "közép");

                MyX.Egyesít(munkalap, $"b{sor}:d{sor}");
                MyX.Kiir("Eszközszám / Alszám", $"b{sor}:d{sor}");
                MyX.Betű(munkalap, $"b{sor}:d{sor}", BeBetűC12V);
                MyX.Igazít_függőleges(munkalap, $"b{sor}:d{sor}", "közép");
                MyX.Igazít_vízszintes(munkalap, $"b{sor}:d{sor}", "közép");

                MyX.Egyesít(munkalap, $"e{sor}:h{sor}");
                MyX.Kiir("Eszköz megnevezése", $"e{sor}:h{sor}");
                MyX.Betű(munkalap, $"e{sor}:h{sor}", BeBetűC12V);
                MyX.Igazít_függőleges(munkalap, $"e{sor}:h{sor}", "közép");
                MyX.Igazít_vízszintes(munkalap, $"e{sor}:h{sor}", "közép");

                MyX.Egyesít(munkalap, $"i{sor}:i{sor}");
                MyX.Kiir("Gyártási szám", $"i{sor}:i{sor}");
                MyX.Betű(munkalap, $"i{sor}:i{sor}", BeBetűC12V);
                MyX.Igazít_függőleges(munkalap, $"i{sor}:i{sor}", "közép");
                MyX.Igazít_vízszintes(munkalap, $"i{sor}:i{sor}", "közép");

                MyX.Egyesít(munkalap, $"j{sor}:j{sor}");
                MyX.Kiir("Leltárszám", $"j{sor}:j{sor}");
                MyX.Betű(munkalap, $"j{sor}:j{sor}", BeBetűC12V);
                MyX.Igazít_függőleges(munkalap, $"j{sor}:j{sor}", "közép");
                MyX.Igazít_vízszintes(munkalap, $"j{sor}:j{sor}", "közép");


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
                        MyX.Sormagasság(munkalap, $"{sor}:{sor + 6}", 20);

                        MyX.Egyesít(munkalap, $"a{sor}:a{sor}");
                        MyX.Kiir((i + 1).ToString(), $"a{sor}:a{sor}"); //sorszám
                        MyX.Igazít_függőleges(munkalap, $"a{sor}:a{sor}", "közép");
                        MyX.Igazít_vízszintes(munkalap, $"a{sor}:a{sor}", "közép");

                        MyX.Egyesít(munkalap, $"b{sor}:c{sor}");
                        MyX.Betű(munkalap, $"b{sor}:c{sor}", BeBetűC12K);
                        MyX.Kiir(eszközszám, $"b{sor}:c{sor}"); // Eszközszám
                        MyX.Igazít_függőleges(munkalap, $"b{sor}:c{sor}", "közép");
                        MyX.Igazít_vízszintes(munkalap, $"b{sor}:c{sor}", "közép");

                        MyX.Egyesít(munkalap, $"d{sor}:d{sor}");
                        MyX.Kiir(EgyElem.Alszám.Trim(), $"d{sor}:d{sor}"); //Alszám
                        MyX.Igazít_függőleges(munkalap, $"d{sor}:d{sor}", "közép");
                        MyX.Igazít_vízszintes(munkalap, $"d{sor}:d{sor}", "közép");

                        MyX.Egyesít(munkalap, $"e{sor}:h{sor}");
                        MyX.Kiir(EgyElem.Megnevezés.Trim(), $"e{sor}:h{sor}");//Megnevezés
                        MyX.Igazít_függőleges(munkalap, $"e{sor}:h{sor}", "közép");
                        MyX.Igazít_vízszintes(munkalap, $"e{sor}:h{sor}", "közép");

                        MyX.Egyesít(munkalap, $"i{sor}:i{sor}");
                        MyX.Kiir(EgyElem.Gyártási_szám.Trim(), $"i{sor}:i{sor}");// Gyártási szám
                        MyX.Igazít_függőleges(munkalap, $"i{sor}:i{sor}", "közép");
                        MyX.Igazít_vízszintes(munkalap, $"i{sor}:i{sor}", "közép");

                        MyX.Egyesít(munkalap, $"j{sor}:j{sor}");
                        MyX.Kiir(EgyElem.Leltárszám.Trim(), $"j{sor}:j{sor}"); //Leltáriszám
                        MyX.Igazít_függőleges(munkalap, $"j{sor}:j{sor}", "közép");
                        MyX.Igazít_vízszintes(munkalap, $"j{sor}:j{sor}", "közép");
                    }
                }
                MyX.Rácsoz(munkalap, $"a{soreleje}:j{sor}");

                sor++; //tizenkettedik sor
                sor++; //tizenharmadik sor

                sor++; //tizennegyedik sor
                MyX.Egyesít(munkalap, $"a{sor}:d{sor}");
                MyX.Kiir($"Budapest, {Napló_Dátumtól.Value:yyyy.MM.dd}", $"a{sor}:d{sor}");
                MyX.Igazít_függőleges(munkalap, $"a{sor}:d{sor}", "közép");
                MyX.Igazít_vízszintes(munkalap, $"a{sor}:d{sor}", "bal");

                sor++; //tizenötödik sor

                sor++; //tizenhatodik sor
                MyX.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyX.Kiir("Munkavállaló neve: ", $"a{sor}:c{sor}");
                MyX.Kiir(DolgozóNév, $"e{sor}");
                MyX.Igazít_függőleges(munkalap, $"a{sor}:c{sor}", "közép");
                MyX.Igazít_vízszintes(munkalap, $"a{sor}:c{sor}", "bal");

                sor++; //tizenhetedik sor
                MyX.Sormagasság(munkalap, $"{sor}:{sor + 8}", 16);

                sor++; //tizennyolcadik sor
                sor++; //tizenkilencedik sor
                MyX.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyX.Kiir("Munkavállaló aláírása:", $"a{sor}:c{sor}");
                MyX.Igazít_függőleges(munkalap, $"a{sor}:c{sor}", "közép");
                MyX.Igazít_vízszintes(munkalap, $"a{sor}:c{sor}", "bal");

                MyX.Egyesít(munkalap, $"E{sor}:H{sor}");

                sor++; //huszadik sor
                MyX.Aláírásvonal(munkalap, $"E{sor}:H{sor}");
                sor++; //huszonegyedik sor
                sor++; //huszonkettedik sor
                MyX.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyX.Kiir("Nyilvántartó neve:", $"a{sor}:c{sor}");
                MyX.Igazít_függőleges(munkalap, $"a{sor}:c{sor}", "közép");
                MyX.Igazít_vízszintes(munkalap, $"a{sor}:c{sor}", "bal");

                DolgozóListaFeltöltés();
                Adat_Dolgozó_Alap Nyilvántartó = (from a in AdatokDolgozó
                                                  where a.Bejelentkezésinév == Program.PostásNév
                                                  select a).FirstOrDefault();

                if (Nyilvántartó != null && Nyilvántartó.DolgozóNév != null)
                    MyX.Kiir(Nyilvántartó.DolgozóNév.Trim(), $"E{sor}");

                sor++; //huszonharmadik sor
                sor++; //huszonnegyedik sor
                sor++; //huszonötödik sor
                MyX.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyX.Kiir("Nyivántartó aláírása:", $"a{sor}:c{sor}");
                MyX.Igazít_függőleges(munkalap, $"a{sor}:c{sor}", "közép");
                MyX.Igazít_vízszintes(munkalap, $"a{sor}:c{sor}", "bal");

                MyX.Egyesít(munkalap, $"E{sor}:H{sor}");
                sor++;
                MyX.Aláírásvonal(munkalap, $"E{sor}:H{sor}");

                List<Adat_Szerszám_FejLáb> Adatok = KézSzerszámFejLáb.Lista_Adatok();
                Adat_Szerszám_FejLáb Adat = Adatok.Where(a => a.Típus == "9B").FirstOrDefault();
                if (Adat != null)
                {
                    Beállítás_Nyomtatás BeNyom = new Beállítás_Nyomtatás
                    {
                        Munkalap = munkalap,
                        NyomtatásiTerület = $"A1:J{sor}",
                        FejlécBal = Adat.Fejléc_Bal,
                        FejlécKözép = Adat.Fejléc_Közép,
                        FejlécJobb = Adat.Fejléc_Jobb,
                        LáblécBal = Adat.Lábléc_Bal,
                        LáblécKözép = Adat.Lábléc_Közép,
                        LáblécJobb = Adat.Lábléc_Jobb,
                        BalMargó = 10,
                        JobbMargó = 10,
                        AlsóMargó = 19,
                        FelsőMargó = 19,
                        FejlécMéret = 8,
                        LáblécMéret = 8,
                        VízKözép = true
                    };
                    MyX.NyomtatásiTerület_részletes(munkalap, BeNyom);
                }
                // bezárjuk az Excel-t
                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();

                List<string> Fájlok = new List<string> { fájlexc };
                if (Napló_Nyomtat.Checked)
                {
                    MyF.ExcelNyomtatás(Fájlok);
                    MessageBox.Show("A bizonylatok nyomtatása elkészült.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (Napló_Fájltöröl.Checked)
                    File.Delete(fájlexc + ".xlsx");
                else
                {
                    MyF.Megnyitás(fájlexc);
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
                AdatokCikk = KézSzerszámCikk.Lista_Adatok(Cmbtelephely.Text.Trim(), Könyvtár_adat);
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
                AdatokKönyvelés = KézKönyvelés.Lista_Adatok(Cmbtelephely.Text.Trim(), Könyvtár_adat);
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
                AdatokKönyv.Clear();
                AdatokKönyv = KézKönyv.Lista_Adatok(Cmbtelephely.Text.Trim(), Könyvtár_adat);
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
                AdatokDolgozó.Clear();
                AdatokDolgozó = KézDolgozó.Lista_Adatok(Cmbtelephely.Text.Trim());
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
                AdatokJelenléti = KézJelenléti.Lista_Adatok(Cmbtelephely.Text.Trim());
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
                AdatokEszköz = KézEszköz.Lista_Adatok(Cmbtelephely.Text.Trim());
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
                AdatokNapló = KézNapló.Lista_Adatok(Könyvtár_adat, Cmbtelephely.Text.Trim(), Dátum.Year);
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

