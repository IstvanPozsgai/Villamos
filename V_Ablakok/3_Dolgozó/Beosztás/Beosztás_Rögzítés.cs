using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok.Beosztás
{
    public class Beosztás_Rögzítés
    {
        #region Kezelők,Listák

        readonly Kezelő_Dolgozó_Beosztás_Új KézBEO = new Kezelő_Dolgozó_Beosztás_Új();
        readonly Kezelő_Dolgozó_Beosztás_Napló KézNapló = new Kezelő_Dolgozó_Beosztás_Napló();
        readonly Kezelő_Szatube_Beteg KézBeteg = new Kezelő_Szatube_Beteg();
        readonly Kezelő_Szatube_Szabadság KézSzabad = new Kezelő_Szatube_Szabadság();
        readonly Kezelő_Szatube_Túlóra KézTúlóra = new Kezelő_Szatube_Túlóra();
        readonly Kezelő_Szatube_Aft KézAft = new Kezelő_Szatube_Aft();
        readonly Kezelő_Szatube_Csúsztatás KézCsúsztatás = new Kezelő_Szatube_Csúsztatás();
        readonly Kezelő_Dolgozó_Alap KézDolg = new Kezelő_Dolgozó_Alap();
        readonly Kezelő_Kiegészítő_Túlórakeret KézTúlkeret = new Kezelő_Kiegészítő_Túlórakeret();
        readonly Kezelő_Kiegészítő_Beosztáskódok KézBEOKód = new Kezelő_Kiegészítő_Beosztáskódok();

        List<Adat_Dolgozó_Beosztás_Napló> AdatokNapló = new List<Adat_Dolgozó_Beosztás_Napló>();
        List<Adat_Szatube_Beteg> AdatokBeteg = new List<Adat_Szatube_Beteg>();
        List<Adat_Szatube_Szabadság> AdatokSzabad = new List<Adat_Szatube_Szabadság>();
        List<Adat_Szatube_Túlóra> AdatokTúlóra = new List<Adat_Szatube_Túlóra>();
        List<Adat_Szatube_AFT> AdatokAft = new List<Adat_Szatube_AFT>();
        List<Adat_Szatube_Csúsztatás> AdatokCsúsztatás = new List<Adat_Szatube_Csúsztatás>();
        #endregion


        #region Rögzítések
        public void Rögzít_BEO(string Cmbtelephely, DateTime Dátum, string Beosztáskód, string ElőzőBeosztásKód, string HR_Azonosító, int Ledolgozott, string Dolgozónév)
        {
            string szöveg = "Nincs hiba";
            try
            {
                List<Adat_Dolgozó_Beosztás_Új> Rekordok = KézBEO.Lista_Adatok(Cmbtelephely.Trim(), Dátum);
                Rekordok = Rekordok.Where(x => x.Dolgozószám.Trim() == HR_Azonosító.Trim() && x.Nap.ToShortDateString() == Dátum.ToShortDateString()).ToList();
                Adat_Dolgozó_Beosztás_Új Rekord_Old = Rekordok.FirstOrDefault();

                string szabiok = Beosztáskód.ToUpper().Contains("SZ") ? "Normál kivétel" : "";

                string AFTok = "";
                int AFTóra = 0;
                if (Beosztáskód.Length > 0 && Beosztáskód.Substring(0, 1) == "A")
                {
                    AFTok = "Nincs kitöltve";
                    AFTóra = Ledolgozott;
                }

                DateTime Túlórakezd = new DateTime(1900, 1, 1, 0, 0, 0);
                DateTime Túlóravég = new DateTime(1900, 1, 1, 0, 0, 0);
                int Túlóra = 0;
                if (Beosztáskód.Length > 0 && (Beosztáskód.Contains("NE") || Beosztáskód.Contains("ÉE")))
                {
                    Adat_Kiegészítő_Beosztáskódok ELEM = Beosztás_Adatok(Cmbtelephely, Beosztáskód);
                    if (ELEM != null)
                    {
                        Túlórakezd = ELEM.Munkaidőkezdet;
                        Túlóravég = ELEM.Munkaidővége;
                        Túlóra = ELEM.Munkaidő;
                    }
                }

                Adat_Dolgozó_Beosztás_Új ADAT = new Adat_Dolgozó_Beosztás_Új(
                    HR_Azonosító.Trim(),
                    Dátum,
                    Beosztáskód.Trim(),
                    Ledolgozott,
                    Túlóra, Túlórakezd, Túlóravég,
                    0, new DateTime(1900, 1, 1, 0, 0, 0), new DateTime(1900, 1, 1, 0, 0, 0),
                    "", "_", szabiok,
                    false, "_",
                    AFTóra, AFTok);
                List<Adat_Dolgozó_Beosztás_Új> ADATok = new List<Adat_Dolgozó_Beosztás_Új> { ADAT };
                if (Rekordok.Count < 1)
                {
                    KézBEO.Rögzítés(Cmbtelephely.Trim(), Dátum, ADATok);
                }
                else
                {
                    KézBEO.Módosítás(Cmbtelephely.Trim(), Dátum, ADATok);
                }


                Rekordok = KézBEO.Lista_Adatok(Cmbtelephely.Trim(), Dátum);
                Rekordok = Rekordok.Where(x => x.Dolgozószám.Trim() == HR_Azonosító.Trim() && x.Nap.ToShortDateString() == Dátum.ToShortDateString()).ToList();
                Adat_Dolgozó_Beosztás_Új Rekord_Új = Rekordok.FirstOrDefault();

                if (Beosztáskód.Length > 0 && Beosztáskód.Substring(0, 1) == "A")
                    AFT_Átírás(Cmbtelephely, Dátum, Rekord_Új, Dolgozónév);

                if (Beosztáskód.Length > 0 && (Beosztáskód.Contains("NE") || Beosztáskód.Contains("ÉE")))
                    Túlóra_Átírás(Cmbtelephely, Dátum, Rekord_Új, Dolgozónév);

                if (Beosztáskód.Length > 1 && Beosztáskód.Substring(0, 2) == "SZ")
                    Szabadság_Átírás(Cmbtelephely, Dátum, Rekord_Új, Dolgozónév);

                if (Beosztáskód.Length > 0 && Beosztáskód.Substring(0, 1) == "B")
                    Beteg_Átírás(Cmbtelephely, Dátum, Rekord_Új, Dolgozónév);

                List<Adat_Dolgozó_Beosztás_Új> RekordOk = KézBEO.Lista_Adatok(Cmbtelephely.Trim(), Dátum);
                RekordOk = RekordOk.Where(x => x.Dolgozószám.Trim() == HR_Azonosító.Trim() && x.Nap.ToShortDateString() == Dátum.ToShortDateString()).ToList();
                Rekord_Új = RekordOk.FirstOrDefault();

                Naplózás(Cmbtelephely, Rekord_Új, Dolgozónév);

                Ellenőrzés_Csúsztatás(Cmbtelephely, Dátum, HR_Azonosító);

                if (ElőzőBeosztásKód.Length > 0 && ElőzőBeosztásKód.Substring(0, 1) == "A")
                {
                    Aft_Törlés(Cmbtelephely, Dátum, Rekord_Új);
                    Ellenőrzés_Aft(Cmbtelephely, Dátum, HR_Azonosító);
                }

                if (ElőzőBeosztásKód.Length > 0 && (ElőzőBeosztásKód.Contains("NE") || ElőzőBeosztásKód.Contains("ÉE")))
                {
                    Túlóra_Törlés(Cmbtelephely, Dátum, Rekord_Új);
                    Ellenőrzés_Túlóra(Cmbtelephely, Dátum, HR_Azonosító);
                }

                if (ElőzőBeosztásKód.Length > 0 && ElőzőBeosztásKód.Substring(0, 1) == "B")
                {
                    Beteg_Törlés(Cmbtelephely, Dátum, Rekord_Új);
                    Ellenőrzés_Beteg(Cmbtelephely, Dátum, HR_Azonosító);
                }

                if (ElőzőBeosztásKód.Length > 1 && ElőzőBeosztásKód.Substring(0, 2) == "SZ")
                {
                    Szabadság_Törlés(Cmbtelephely, Dátum, Rekord_Új);
                    Ellenőrzés_Szabadság(Cmbtelephely, Dátum, HR_Azonosító);
                }

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, szöveg, ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Adat_Kiegészítő_Beosztáskódok Beosztás_Adatok(string cmbtelephely, string beosztáskód)
        {
            Adat_Kiegészítő_Beosztáskódok válasz = null;

            try
            {
                List<Adat_Kiegészítő_Beosztáskódok> Adatok = KézBEOKód.Lista_Adatok(cmbtelephely.Trim());
                válasz = Adatok.Where(y => y.Beosztáskód.Trim() == beosztáskód.Trim()).FirstOrDefault();
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

        private void Naplózás(string Cmbtelephely, Adat_Dolgozó_Beosztás_Új Rekord, string dolgozónév)
        {
            try
            {
                AdatokNapló = KézNapló.Lista_Adatok(Cmbtelephely.Trim(), DateTime.Now);
                Adat_Dolgozó_Beosztás_Napló AdatNapló = (from a in AdatokNapló
                                                         orderby a.Sorszám descending
                                                         select a).FirstOrDefault();
                double sorszám = 1;
                if (AdatNapló != null) sorszám = AdatNapló.Sorszám + 1;

                Adat_Dolgozó_Beosztás_Napló ADAT = new Adat_Dolgozó_Beosztás_Napló(
                      sorszám,
                      Rekord.Nap, Rekord.Beosztáskód.Trim(),
                      Rekord.Túlóra, Rekord.Túlórakezd, Rekord.Túlóravég,
                      Rekord.Csúszóra, Rekord.CSúszórakezd, Rekord.Csúszóravég,
                      Rekord.Megjegyzés.Trim(), Rekord.Túlóraok.Trim(), Rekord.Szabiok.Trim(),
                      Rekord.Kért, Rekord.Csúszok.Trim(),
                      Program.PostásNév.Trim(), DateTime.Now,
                      dolgozónév.Trim(), Rekord.Dolgozószám.Trim(),
                      Rekord.AFTóra, Rekord.AFTok.Trim());
                List<Adat_Dolgozó_Beosztás_Napló> ADATOK = new List<Adat_Dolgozó_Beosztás_Napló> { ADAT };
                KézNapló.Rögzítés(Cmbtelephely.Trim(), DateTime.Now, ADATOK);
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

        public void Naplózás(string Cmbtelephely, string Művelet)
        {
            try
            {
                AdatokNapló = KézNapló.Lista_Adatok(Cmbtelephely.Trim(), DateTime.Now);

                Adat_Dolgozó_Beosztás_Napló AdatNapló = (from a in AdatokNapló
                                                         orderby a.Sorszám descending
                                                         select a).FirstOrDefault();
                double sorszám = 1;
                if (AdatNapló != null) sorszám = AdatNapló.Sorszám + 1;

                Adat_Dolgozó_Beosztás_Napló ADAT = new Adat_Dolgozó_Beosztás_Napló(
                    sorszám,
                    DateTime.Today,
                    "000",
                    0, new DateTime(1900, 1, 1, 0, 0, 0), new DateTime(1900, 1, 1, 0, 0, 0),
                    0, new DateTime(1900, 1, 1, 0, 0, 0), new DateTime(1900, 1, 1, 0, 0, 0),
                    Művelet,
                    "0", "0000", false,
                    "0000",
                    Program.PostásNév.Trim(), DateTime.Now,
                    Művelet, "000000",
                    0, "0000");
                List<Adat_Dolgozó_Beosztás_Napló> ADATOK = new List<Adat_Dolgozó_Beosztás_Napló> { ADAT };
                KézNapló.Rögzítés(Cmbtelephely.Trim(), DateTime.Now, ADATOK);
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

        public void Rögzít_Megjegyzés(string Cmbtelephely, DateTime Dátum, string HR_Azonosító, string Megjegyzés, bool Kért, string Dolgozónév)
        {
            string szöveg = "Nincs hiba";
            try
            {
                List<Adat_Dolgozó_Beosztás_Új> Rekordok = KézBEO.Lista_Adatok(Cmbtelephely.Trim(), Dátum);
                Rekordok = Rekordok.Where(x => x.Dolgozószám.Trim() == HR_Azonosító.Trim() && x.Nap.ToShortDateString() == Dátum.ToShortDateString()).ToList();
                Adat_Dolgozó_Beosztás_Új Rekord = Rekordok.FirstOrDefault();

                string Beosztáskód = "";
                string AFTok = "";
                int AFTóra = 0;
                int Ledolgozott = 0;
                string szabiok = "";


                if (Rekord == null)
                {
                    Adat_Dolgozó_Beosztás_Új ADAT = new Adat_Dolgozó_Beosztás_Új(
                          HR_Azonosító.Trim(),
                          Dátum,
                          Beosztáskód.Trim(),
                          Ledolgozott,
                          0, new DateTime(1900, 1, 1, 0, 0, 0), new DateTime(1900, 1, 1, 0, 0, 0),
                          0, new DateTime(1900, 1, 1, 0, 0, 0), new DateTime(1900, 1, 1, 0, 0, 0),
                          Megjegyzés.Trim(), "_", szabiok,
                          Kért, "_",
                          AFTóra, AFTok);
                    List<Adat_Dolgozó_Beosztás_Új> Adatok = new List<Adat_Dolgozó_Beosztás_Új> { ADAT };
                    KézBEO.Rögzítés(Cmbtelephely.Trim(), Dátum, Adatok);
                }
                else
                {
                    Adat_Dolgozó_Beosztás_Új ADAT = new Adat_Dolgozó_Beosztás_Új(
                       HR_Azonosító.Trim(),
                       Dátum,
                       Megjegyzés.Trim(),
                       Kért);

                    KézBEO.MódosításMegj(Cmbtelephely.Trim(), Dátum, ADAT);
                }
                Rekordok = KézBEO.Lista_Adatok(Cmbtelephely.Trim(), Dátum);
                Rekordok = Rekordok.Where(x => x.Dolgozószám.Trim() == HR_Azonosító.Trim() && x.Nap.ToShortDateString() == Dátum.ToShortDateString()).ToList();
                Rekord = Rekordok.FirstOrDefault();

                Naplózás(Cmbtelephely, Rekord, Dolgozónév);

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, szöveg, ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Rögzít_Szabadság(string Cmbtelephely, DateTime Dátum, string Beosztáskód, string HR_Azonosító, int Ledolgozott, string Szabiok, string Dolgozónév)
        {
            string szöveg = "Nincs hiba";
            try
            {
                List<Adat_Dolgozó_Beosztás_Új> Rekord_Old = KézBEO.Lista_Adatok(Cmbtelephely.Trim(), Dátum);
                Rekord_Old = Rekord_Old.Where(x => x.Dolgozószám.Trim() == HR_Azonosító.Trim() && x.Nap.ToShortDateString() == Dátum.ToShortDateString()).ToList();

                if (Rekord_Old == null)
                {
                    Adat_Dolgozó_Beosztás_Új ADAT = new Adat_Dolgozó_Beosztás_Új(
                         HR_Azonosító.Trim(),
                         Dátum,
                         Beosztáskód.Trim(),
                         Ledolgozott,
                         0, new DateTime(1900, 1, 1, 0, 0, 0), new DateTime(1900, 1, 1, 0, 0, 0),
                         0, new DateTime(1900, 1, 1, 0, 0, 0), new DateTime(1900, 1, 1, 0, 0, 0),
                         "", "_", Szabiok,
                         false, "_",
                         0, "");
                    List<Adat_Dolgozó_Beosztás_Új> Adatok = new List<Adat_Dolgozó_Beosztás_Új> { ADAT };
                    KézBEO.Rögzítés(Cmbtelephely.Trim(), Dátum, Adatok);
                }
                else
                {
                    Adat_Dolgozó_Beosztás_Új ADAT = new Adat_Dolgozó_Beosztás_Új(HR_Azonosító.Trim(), Dátum, Szabiok);
                    KézBEO.MódosításSzabiOk(Cmbtelephely.Trim(), Dátum, ADAT);
                }

                //újra beolvassuk a módosítás/létrehozás utáni állapotot
                Rekord_Old = KézBEO.Lista_Adatok(Cmbtelephely.Trim(), Dátum);
                Rekord_Old = Rekord_Old.Where(x => x.Dolgozószám.Trim() == HR_Azonosító.Trim() && x.Nap.ToShortDateString() == Dátum.ToShortDateString()).ToList();
                Adat_Dolgozó_Beosztás_Új Rekord = Rekord_Old.FirstOrDefault();

                Szabadság_Átírás(Cmbtelephely, Dátum, Rekord, Dolgozónév);
                Naplózás(Cmbtelephely, Rekord, Dolgozónév);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, szöveg, ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region Beteg
        private void Beteg_Átírás(string Cmbtelephely, DateTime Dátum, Adat_Dolgozó_Beosztás_Új Rekord_Új, string Dolgozónév)
        {
            try
            {
                AdatokBeteg = KézBeteg.Lista_Adatok(Cmbtelephely, Dátum.Year);
                Adat_Szatube_Beteg AdatBeteg = (from a in AdatokBeteg
                                                where a.Törzsszám == Rekord_Új.Dolgozószám.Trim()
                                                && a.Kezdődátum.ToShortDateString() == Rekord_Új.Nap.ToShortDateString()
                                                && a.Státus != 3
                                                orderby a.Sorszám descending
                                                select a).FirstOrDefault();

                if (AdatBeteg == null)
                {
                    Adat_Szatube_Beteg Elem = (from a in AdatokBeteg
                                               orderby a.Sorszám descending
                                               select a).FirstOrDefault();

                    double sorszám = 1;
                    if (Elem != null) sorszám = Elem.Sorszám + 1;
                    Adat_Szatube_Beteg ADAT = new Adat_Szatube_Beteg(
                        sorszám,
                        Rekord_Új.Dolgozószám.Trim(),
                        Dolgozónév.Trim(),
                        Rekord_Új.Nap,
                        Rekord_Új.Nap,
                        1,
                        "",
                        0,
                        Program.PostásNév.Trim(),
                        DateTime.Now);
                    KézBeteg.Rögzítés(Cmbtelephely.Trim(), Dátum.Year, ADAT);
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

        private void Beteg_Törlés(string Cmbtelephely, DateTime Dátum, Adat_Dolgozó_Beosztás_Új Rekord_Új)
        {
            try
            {
                AdatokBeteg = KézBeteg.Lista_Adatok(Cmbtelephely, Dátum.Year);
                Adat_Szatube_Beteg AdatBeteg = (from a in AdatokBeteg
                                                where a.Törzsszám == Rekord_Új.Dolgozószám.Trim()
                                                && a.Kezdődátum.ToShortDateString() == Rekord_Új.Nap.ToShortDateString()
                                                && a.Státus != 3
                                                select a).FirstOrDefault();

                if (AdatBeteg != null)
                {
                    Adat_Szatube_Beteg ADAT = new Adat_Szatube_Beteg(Rekord_Új.Dolgozószám.Trim(), Rekord_Új.Nap);
                    List<Adat_Szatube_Beteg> Adatok = new List<Adat_Szatube_Beteg> { ADAT };
                    KézBeteg.Módosítás(Cmbtelephely.Trim(), Dátum.Year, Adatok);
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

        public void Ellenőrzés_Beteg(string Cmbtelephely, DateTime Dátum, string HR_Azonosító)
        {
            try
            {
                DateTime hónapelső = MyF.Hónap_elsőnapja(Dátum);
                DateTime hónaputolsó = MyF.Hónap_utolsónapja(Dátum);

                List<Adat_Szatube_Beteg> Adatok_SZA = KézBeteg.Lista_Adatok(Cmbtelephely.Trim(), Dátum.Year);
                Adatok_SZA = (from a in Adatok_SZA
                              where a.Törzsszám == HR_Azonosító.Trim()
                              && a.Státus != 3
                              && a.Kezdődátum >= hónapelső
                              && a.Kezdődátum <= hónaputolsó
                              select a).ToList();

                List<Adat_Dolgozó_Beosztás_Új> Adatok_Beo = KézBEO.Lista_Adatok(Cmbtelephely.Trim(), Dátum);
                Adatok_Beo = Adatok_Beo.Where(x => x.Dolgozószám.Trim() == HR_Azonosító.Trim()).ToList();

                //Megkeressük a beosztás táblában a SZATUBE-ben tároltat, ha nem létezik akkor töröltre állítjuk
                List<Adat_Szatube_Beteg> Adatok = new List<Adat_Szatube_Beteg>();
                foreach (Adat_Szatube_Beteg rekord in Adatok_SZA)
                {
                    string BeoKód = (from a in Adatok_Beo
                                     where rekord.Kezdődátum.ToString("yyyy.MM.dd") == a.Nap.ToString("yyyy.MM.dd")
                                     select a.Beosztáskód).FirstOrDefault();

                    if (BeoKód.Length > 0 && BeoKód.Substring(0, 1) != "B")
                    {
                        Adat_Szatube_Beteg ADAT = new Adat_Szatube_Beteg(HR_Azonosító.Trim(), rekord.Kezdődátum);
                        Adatok.Add(ADAT);
                    }
                }
                if (Adatok.Count > 0) KézBeteg.Módosítás(Cmbtelephely.Trim(), Dátum.Year, Adatok);

                // leellenőrizzük, hogy a beosztás táblában létezik és ha SZATUBE nem létezik akkor rögzítjük.
                Adatok_SZA = KézBeteg.Lista_Adatok(Cmbtelephely.Trim(), Dátum.Year);
                Adatok_SZA = (from a in Adatok_SZA
                              where a.Törzsszám == HR_Azonosító.Trim()
                              && a.Státus != 3
                              && a.Kezdődátum >= hónapelső
                              && a.Kezdődátum <= hónaputolsó
                              select a).ToList();

                foreach (Adat_Dolgozó_Beosztás_Új rekord in Adatok_Beo)
                {
                    if (rekord.Beosztáskód.Length > 0 && rekord.Beosztáskód.Substring(0, 1) == "B")
                    {
                        Adat_Dolgozó_Alap Adat_Dolg = KézDolg.Lista_Adatok(Cmbtelephely.Trim()).Where(a => a.Dolgozószám == HR_Azonosító).FirstOrDefault();
                        Beteg_Átírás(Cmbtelephely, Dátum, rekord, Adat_Dolg.DolgozóNév.Trim());
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
        #endregion


        #region Szabadság
        private void Szabadság_Átírás(string Cmbtelephely, DateTime Dátum, Adat_Dolgozó_Beosztás_Új Rekord_Új, string Dolgozónév)
        {
            try
            {
                List<Adat_Szatube_Szabadság> Adatok = KézSzabad.Lista_Adatok(Cmbtelephely.Trim(), Dátum.Year);
                Adat_Szatube_Szabadság Adat = (from a in Adatok
                                               where a.Törzsszám == Rekord_Új.Dolgozószám.Trim()
                                               && a.Kezdődátum == Rekord_Új.Nap
                                               && a.Státus != 3
                                               select a).FirstOrDefault();

                if (Adat == null)
                {
                    //Sorszám azért nulla, hogy ezen a számra gyűjtük össze a szabadságokat
                    Adat_Szatube_Szabadság ADAT = new Adat_Szatube_Szabadság(
                        0,
                        Rekord_Új.Dolgozószám.Trim(),
                        Dolgozónév.Trim(),
                        Rekord_Új.Nap,
                        Rekord_Új.Nap,
                        1,
                        Rekord_Új.Szabiok.Trim(),
                        0,
                        Program.PostásNév.Trim(),
                        DateTime.Now);
                    KézSzabad.Rögzítés(Cmbtelephely.Trim(), Dátum.Year, ADAT);
                }
                else
                {
                    Adat_Szatube_Szabadság ADAT = new Adat_Szatube_Szabadság(Rekord_Új.Dolgozószám.Trim(), Rekord_Új.Nap, Rekord_Új.Szabiok.Trim());
                    KézSzabad.Módosítás(Cmbtelephely.Trim(), Dátum.Year, ADAT);
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

        private void Szabadság_Törlés(string Cmbtelephely, DateTime Dátum, Adat_Dolgozó_Beosztás_Új Rekord_Új)
        {
            try
            {
                AdatokSzabad = KézSzabad.Lista_Adatok(Cmbtelephely, Dátum.Year);
                Adat_Szatube_Szabadság AdatSzabad = (from a in AdatokSzabad
                                                     where a.Törzsszám == Rekord_Új.Dolgozószám.Trim()
                                                     && a.Kezdődátum.ToShortDateString() == Rekord_Új.Nap.ToShortDateString()
                                                     && a.Státus != 3
                                                     select a).FirstOrDefault();

                if (AdatSzabad != null)
                {
                    if (AdatSzabad.Sorszám != 0)
                    {
                        List<double> Sorszámok = new List<double> { AdatSzabad.Sorszám };
                        KézSzabad.Státus(Cmbtelephely, Dátum.Year, Sorszámok);
                    }
                    //Státust állítjuk a törölt elemnél 0-ra
                    List<string> Dolgozók = new List<string> { Rekord_Új.Dolgozószám.Trim() };
                    List<DateTime> Napok = new List<DateTime> { Rekord_Új.Nap };
                    KézSzabad.Státus(Cmbtelephely, Dátum.Year, Dolgozók, Napok);
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

        public void Ellenőrzés_Szabadság(string Cmbtelephely, DateTime Dátum, string HR_Azonosító)
        {
            try
            {
                DateTime hónapelső = MyF.Hónap_elsőnapja(Dátum);
                DateTime hónaputolsó = MyF.Hónap_utolsónapja(Dátum);

                List<Adat_Szatube_Szabadság> Adatok_SZA = KézSzabad.Lista_Adatok(Cmbtelephely.Trim(), Dátum.Year);
                Adatok_SZA = (from a in Adatok_SZA
                              where a.Törzsszám == HR_Azonosító.Trim()
                              && a.Kezdődátum >= hónapelső
                              && a.Kezdődátum <= hónaputolsó
                              && a.Státus != 3
                              select a).ToList();

                List<Adat_Dolgozó_Beosztás_Új> Adatok_Beo = KézBEO.Lista_Adatok(Cmbtelephely.Trim(), Dátum);
                Adatok_Beo = Adatok_Beo.Where(x => x.Dolgozószám.Trim() == HR_Azonosító.Trim()).ToList();

                //Megkeressük a beosztás táblában a SZATUBE-ben tároltat, ha nem létezik akkor töröltre állítjuk
                List<double> Sorszámok = new List<double>();
                List<string> HR_Azonosítók = new List<string>();
                List<DateTime> Kezdődátumok = new List<DateTime>();
                foreach (Adat_Szatube_Szabadság rekord in Adatok_SZA)
                {
                    string BeoKód = (from a in Adatok_Beo
                                     where rekord.Kezdődátum.ToString("yyyy.MM.dd") == a.Nap.ToString("yyyy.MM.dd")
                                     select a.Beosztáskód).FirstOrDefault();

                    if (BeoKód != null && BeoKód.Length > 1 && BeoKód.Substring(0, 2) != "SZ")
                    {
                        if (rekord.Sorszám != 0)
                        {
                            Sorszámok.Add(rekord.Sorszám);
                        }
                        HR_Azonosítók.Add(HR_Azonosító.Trim());
                        Kezdődátumok.Add(rekord.Kezdődátum);
                    }
                }
                if (HR_Azonosítók.Count > 0) KézSzabad.Státus(Cmbtelephely.Trim(), Dátum.Year, HR_Azonosítók, Kezdődátumok);
                if (Sorszámok.Count > 0) KézSzabad.Státus(Cmbtelephely.Trim(), Dátum.Year, Sorszámok);


                // leellenőrizzük, hogy a beosztás táblában létezik és ha SZATUBE nem létezik akkor rögzítjük.
                Adatok_SZA = KézSzabad.Lista_Adatok(Cmbtelephely.Trim(), Dátum.Year);
                Adatok_SZA = (from a in Adatok_SZA
                              where a.Törzsszám == HR_Azonosító.Trim()
                              && a.Kezdődátum >= hónapelső
                              && a.Kezdődátum <= hónaputolsó
                              && a.Státus != 3
                              select a).ToList();

                foreach (Adat_Dolgozó_Beosztás_Új rekord in Adatok_Beo)
                {
                    if (rekord.Beosztáskód.Length > 1 && rekord.Beosztáskód.Substring(0, 2) == "SZ")
                    {
                        List<Adat_Dolgozó_Alap> Adatok_Dolg = KézDolg.Lista_Adatok(Cmbtelephely.Trim());
                        Adat_Dolgozó_Alap Adat_Dolg = Adatok_Dolg.Where(y => y.Dolgozószám == HR_Azonosító).FirstOrDefault();
                        Szabadság_Átírás(Cmbtelephely, Dátum, rekord, Adat_Dolg.DolgozóNév.Trim());
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
        #endregion


        #region Túlóra
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Cmbtelephely">Telephely neve</param>
        /// <param name="Dátum">Rögzítendő nap</param>
        /// <param name="Beosztáskód">Rögzítendő beosztáskód</param>
        /// <param name="HR_Azonosító"></param>
        /// <param name="Ledolgozott"></param>
        /// <param name="Túlórakezd"></param>
        /// <param name="Túlóravég"></param>
        /// <param name="túlóra"></param>
        /// <param name="TúlóraOk"></param>
        /// <param name="Dolgozónév"></param>
        public void Rögzít_Túlóra(string Cmbtelephely, DateTime Dátum, string Beosztáskód, string HR_Azonosító, int Ledolgozott, DateTime Túlórakezd, DateTime Túlóravég, int túlóra, string TúlóraOk, string Dolgozónév)
        {
            string szöveg = "Nincs hiba";
            try
            {
                List<Adat_Dolgozó_Beosztás_Új> Rekord_Old = KézBEO.Lista_Adatok(Cmbtelephely.Trim(), Dátum);
                Rekord_Old = Rekord_Old.Where(x => x.Dolgozószám.Trim() == HR_Azonosító.Trim() && x.Nap.ToShortDateString() == Dátum.ToShortDateString()).ToList();

                if (Rekord_Old.Count < 1)
                {
                    Adat_Dolgozó_Beosztás_Új ADAT = new Adat_Dolgozó_Beosztás_Új(
                       HR_Azonosító.Trim(),
                       Dátum,
                       Beosztáskód.Trim(),
                       Ledolgozott,
                       túlóra, Túlórakezd, Túlóravég,
                       0, new DateTime(1900, 1, 1, 0, 0, 0), new DateTime(1900, 1, 1, 0, 0, 0),
                       "",
                       TúlóraOk.Trim(),
                       "",
                       false,
                       "",
                       0, "");
                    List<Adat_Dolgozó_Beosztás_Új> ADATOK = new List<Adat_Dolgozó_Beosztás_Új>
                    {
                        ADAT
                    };
                    KézBEO.Rögzítés(Cmbtelephely, Dátum, ADATOK);
                }
                else
                {
                    Adat_Dolgozó_Beosztás_Új ADAT = new Adat_Dolgozó_Beosztás_Új(
                        HR_Azonosító.Trim(),
                        Dátum,
                        túlóra,
                        Túlórakezd,
                        Túlóravég,
                        TúlóraOk.Trim());
                    KézBEO.MódosításTúl(Cmbtelephely, Dátum, ADAT);
                }
                List<Adat_Dolgozó_Beosztás_Új> Rekordok = KézBEO.Lista_Adatok(Cmbtelephely.Trim(), Dátum);
                Rekordok = Rekordok.Where(x => x.Dolgozószám.Trim() == HR_Azonosító.Trim() && x.Nap.ToShortDateString() == Dátum.ToShortDateString()).ToList();
                Adat_Dolgozó_Beosztás_Új Rekord_Új = Rekordok.FirstOrDefault();


                Naplózás(Cmbtelephely, Rekord_Új, Dolgozónév);

                if (Rekord_Új.Túlóra != 0)
                    Túlóra_Átírás(Cmbtelephely, Dátum, Rekord_Új, Dolgozónév);
                else
                    Túlóra_Törlés(Cmbtelephely, Dátum, Rekord_Új);

                Ellenőrzés_Túlóra(Cmbtelephely, Dátum, HR_Azonosító);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, szöveg, ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Túlóra_Átírás(string Cmbtelephely, DateTime Dátum, Adat_Dolgozó_Beosztás_Új Rekord_Új, string Dolgozónév)
        {
            try
            {
                AdatokTúlóra = KézTúlóra.Lista_Adatok(Cmbtelephely, Dátum.Year);
                Adat_Szatube_Túlóra AdatTúlóra = (from a in AdatokTúlóra
                                                  where a.Törzsszám == Rekord_Új.Dolgozószám.Trim()
                                                        && a.Kezdődátum.ToShortDateString() == Rekord_Új.Nap.ToShortDateString()
                                                        && a.Státus != 3
                                                  select a).FirstOrDefault();

                if (AdatTúlóra == null)
                {
                    Adat_Szatube_Túlóra Elem = (from a in AdatokTúlóra
                                                orderby a.Sorszám descending
                                                select a).FirstOrDefault();
                    double sorszám = 1;
                    if (Elem != null) sorszám = Elem.Sorszám + 1;

                    Adat_Szatube_Túlóra ADAT = new Adat_Szatube_Túlóra(
                          sorszám,
                          Rekord_Új.Dolgozószám.Trim(),
                          Dolgozónév.Trim(),
                          Rekord_Új.Nap,
                          Rekord_Új.Nap,
                          Rekord_Új.Túlóra,
                          Rekord_Új.Túlóraok.Trim(),
                          0,
                          Program.PostásNév.Trim(),
                          DateTime.Now,
                          Rekord_Új.Túlórakezd,
                          Rekord_Új.Túlóravég);
                    KézTúlóra.Rögzítés(Cmbtelephely, Dátum.Year, ADAT);
                }
                else
                {
                    if (Rekord_Új.Túlóra == 0)
                    {
                        // ha lenullázzuk akkor a státust állítjuk
                        Adat_Szatube_Túlóra ADAT = new Adat_Szatube_Túlóra(Rekord_Új.Dolgozószám.Trim(), Rekord_Új.Nap, 3);
                        List<Adat_Szatube_Túlóra> Adatok = new List<Adat_Szatube_Túlóra> { ADAT };
                        KézTúlóra.Törlés(Cmbtelephely, Dátum.Year, Adatok);
                    }
                    else
                    {
                        // Módosítjuk 
                        Adat_Szatube_Túlóra ADAT = new Adat_Szatube_Túlóra(
                            0,
                            Rekord_Új.Dolgozószám.Trim(),
                            Dolgozónév.Trim(),
                            Rekord_Új.Nap,
                            Rekord_Új.Nap,
                            Rekord_Új.Túlóra,
                            Rekord_Új.Túlóraok.Trim(),
                            Program.PostásNév.Trim(),
                            DateTime.Now,
                            Rekord_Új.Túlórakezd,
                            Rekord_Új.Túlóravég);
                        KézTúlóra.Módosítás(Cmbtelephely, Dátum.Year, ADAT);
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

        private void Túlóra_Törlés(string Cmbtelephely, DateTime Dátum, Adat_Dolgozó_Beosztás_Új Rekord_Új)
        {
            try
            {
                AdatokTúlóra = KézTúlóra.Lista_Adatok(Cmbtelephely, Dátum.Year);
                Adat_Szatube_Túlóra AdatTúlóra = (from a in AdatokTúlóra
                                                  where a.Törzsszám == Rekord_Új.Dolgozószám.Trim()
                                                        && a.Kezdődátum.ToShortDateString() == Rekord_Új.Nap.ToShortDateString()
                                                        && a.Státus != 3
                                                  select a).FirstOrDefault();

                if (AdatTúlóra != null)
                {
                    Adat_Szatube_Túlóra ADAT = new Adat_Szatube_Túlóra(Rekord_Új.Dolgozószám.Trim(), Rekord_Új.Nap, 3);
                    List<Adat_Szatube_Túlóra> Adatok = new List<Adat_Szatube_Túlóra> { ADAT };
                    KézTúlóra.Törlés(Cmbtelephely, Dátum.Year, Adatok);
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

        public void Ellenőrzés_Túlóra(string Cmbtelephely, DateTime Dátum, string HR_Azonosító)
        {
            try
            {
                DateTime hónapelső = MyF.Hónap_elsőnapja(Dátum);
                DateTime hónaputolsó = MyF.Hónap_utolsónapja(Dátum);

                List<Adat_Szatube_Túlóra> Adatok_Aft = KézTúlóra.Lista_Adatok(Cmbtelephely.Trim(), Dátum.Year);
                Adatok_Aft = (from a in Adatok_Aft
                              where a.Törzsszám == HR_Azonosító.Trim()
                              && a.Kezdődátum >= hónapelső
                              && a.Kezdődátum <= hónaputolsó
                              && a.Státus != 3
                              select a).ToList();

                List<Adat_Dolgozó_Beosztás_Új> Adatok_Beo = KézBEO.Lista_Adatok(Cmbtelephely.Trim(), Dátum);
                Adatok_Beo = Adatok_Beo.Where(x => x.Dolgozószám.Trim() == HR_Azonosító.Trim() && x.Túlóra != 0).ToList();

                int óra;
                //Megkeressük a beosztás táblában a SZATUBE-ben tároltat, ha nem létezik akkor töröltre állítjuk

                List<Adat_Szatube_Túlóra> AdatokGY = new List<Adat_Szatube_Túlóra>();
                foreach (Adat_Szatube_Túlóra rekord in Adatok_Aft)
                {
                    óra = (from a in Adatok_Beo
                           where rekord.Kezdődátum.ToString("yyyy.MM.dd") == a.Nap.ToString("yyyy.MM.dd")
                           select a.Túlóra).FirstOrDefault();

                    if (rekord.Kivettnap != óra)
                    {
                        Adat_Szatube_Túlóra ADAT = new Adat_Szatube_Túlóra(HR_Azonosító.Trim(), rekord.Kezdődátum, 3);
                        AdatokGY.Add(ADAT);
                    }
                }
                KézTúlóra.Törlés(Cmbtelephely.Trim(), Dátum.Year, AdatokGY);
                // leellenőrizzük, hogy a beosztás táblában létezik és ha SZATUBE nem létezik akkor rögzítjük.
                Adatok_Aft = KézTúlóra.Lista_Adatok(Cmbtelephely.Trim(), Dátum.Year);
                Adatok_Aft = (from a in Adatok_Aft
                              where a.Törzsszám == HR_Azonosító.Trim()
                              && a.Kezdődátum >= hónapelső
                              && a.Kezdődátum <= hónaputolsó
                              && a.Státus != 3
                              select a).ToList();

                foreach (Adat_Dolgozó_Beosztás_Új rekord in Adatok_Beo)
                {
                    óra = (from a in Adatok_Aft
                           where rekord.Nap.ToString("yyyy.MM.dd") == a.Kezdődátum.ToString("yyyy.MM.dd")
                           select a.Kivettnap).FirstOrDefault();
                    if (rekord.Túlóra != óra)
                    {
                        List<Adat_Dolgozó_Alap> Adatok_Dolg = KézDolg.Lista_Adatok(Cmbtelephely.Trim());
                        Adat_Dolgozó_Alap Adat_Dolg = Adatok_Dolg.Where(y => y.Dolgozószám == HR_Azonosító).FirstOrDefault();
                        Túlóra_Átírás(Cmbtelephely, Dátum, rekord, Adat_Dolg.DolgozóNév.Trim());
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

        public int Túlóra_Keret_Ellenőrzés(string Cmbtelephely, DateTime Dátum, string HR_Azonosító)
        {
            int válasz = 0;
            try
            {
                // *****************************************
                // leellenőrizzük, hogy lehet-e még túlóráznia
                // *****************************************
                double Eddigi_TúlÓra = Évestúlóra_Keret_Figyelés(Cmbtelephely, Dátum, HR_Azonosító);

                List<Adat_Kiegészítő_Túlórakeret> Elemek = KézTúlkeret.Lista_Adatok();

                string telephely = (from a in Elemek
                                    where a.Telephely.Trim() == Cmbtelephely.Trim()
                                    select a.Telephely).FirstOrDefault();

                //ha van a telephelynek külön megkötése ellenben az általánost használja
                if (Elemek.Any(y => y.Telephely.Trim() == Cmbtelephely.Trim()))
                {
                    Elemek = (from a in Elemek
                              where a.Telephely.Trim() == Cmbtelephely.Trim()
                              select a).ToList();
                }
                else
                {
                    Elemek = (from a in Elemek
                              where a.Telephely.Trim() == "_"
                              select a).ToList();
                }

                foreach (Adat_Kiegészítő_Túlórakeret elem in Elemek)
                {
                    if (Eddigi_TúlÓra > elem.Határ * 60 && válasz == 0)
                        válasz = elem.Parancs;
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
            return válasz;
        }

        public double Évestúlóra_Keret_Figyelés(string Cmbtelephely, DateTime Dátum, string Hrazonosító)
        {
            double válasz = 0;
            try
            {
                AdatokTúlóra = KézTúlóra.Lista_Adatok(Cmbtelephely, Dátum.Year);
                List<Adat_Szatube_Túlóra> Adatok = (from a in AdatokTúlóra
                                                    where a.Törzsszám == Hrazonosító.Trim()
                                                    && a.Státus != 3
                                                    select a).ToList();
                if (Adatok != null) válasz = Adatok.Select(a => a.Kivettnap).Sum();
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


        #region AFT
        public void Rögzít_AFT(string Cmbtelephely, DateTime Dátum, string Beosztáskód, string HR_Azonosító, int Ledolgozott, string AFTok, int AFTóra, string Dolgozónév)
        {
            string szöveg = "Nincs hiba";
            try
            {
                List<Adat_Dolgozó_Beosztás_Új> Rekordok = KézBEO.Lista_Adatok(Cmbtelephely.Trim(), Dátum);
                Rekordok = Rekordok.Where(x => x.Dolgozószám.Trim() == HR_Azonosító.Trim() && x.Nap.ToShortDateString() == Dátum.ToShortDateString()).ToList();

                if (Rekordok.Count < 1)
                {
                    Adat_Dolgozó_Beosztás_Új ADAT = new Adat_Dolgozó_Beosztás_Új(
                         HR_Azonosító.Trim(),
                         Dátum,
                         Beosztáskód.Trim(),
                         Ledolgozott,
                         0, new DateTime(1900, 1, 1, 0, 0, 0), new DateTime(1900, 1, 1, 0, 0, 0),
                         0, new DateTime(1900, 1, 1, 0, 0, 0), new DateTime(1900, 1, 1, 0, 0, 0),
                         "",
                         "",
                         "",
                         false,
                         "",
                         AFTóra, AFTok);
                    List<Adat_Dolgozó_Beosztás_Új> ADATOK = new List<Adat_Dolgozó_Beosztás_Új> { ADAT };
                    KézBEO.Rögzítés(Cmbtelephely, Dátum, ADATOK);
                }
                else
                {
                    Adat_Dolgozó_Beosztás_Új ADAT = new Adat_Dolgozó_Beosztás_Új(
                         HR_Azonosító.Trim(),
                         Dátum,
                         Beosztáskód.Trim(),
                         Ledolgozott,
                         AFTóra, AFTok);
                    KézBEO.MódosításAft(Cmbtelephely, Dátum, ADAT);
                }

                Rekordok = KézBEO.Lista_Adatok(Cmbtelephely.Trim(), Dátum);
                Rekordok = Rekordok.Where(x => x.Dolgozószám.Trim() == HR_Azonosító.Trim() && x.Nap.ToShortDateString() == Dátum.ToShortDateString()).ToList();
                Adat_Dolgozó_Beosztás_Új Rekord_Új = Rekordok.FirstOrDefault();

                Naplózás(Cmbtelephely, Rekord_Új, Dolgozónév);

                if (Rekord_Új.AFTóra != 0)
                    AFT_Átírás(Cmbtelephely, Dátum, Rekord_Új, Dolgozónév);
                else
                    Aft_Törlés(Cmbtelephely, Dátum, Rekord_Új);

                Ellenőrzés_Aft(Cmbtelephely, Dátum, HR_Azonosító);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, szöveg, ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AFT_Átírás(string Cmbtelephely, DateTime Dátum, Adat_Dolgozó_Beosztás_Új Rekord_Új, string Dolgozónév)
        {
            try
            {
                AdatokAft = KézAft.Lista_Adatok(Cmbtelephely.Trim(), Dátum.Year);
                Adat_Szatube_AFT AdatAft = (from a in AdatokAft
                                            where a.Törzsszám == Rekord_Új.Dolgozószám.Trim()
                                            && a.Dátum.ToShortDateString() == Rekord_Új.Nap.ToShortDateString()
                                            && a.Státus != 3
                                            select a).FirstOrDefault();

                if (AdatAft == null)
                {
                    Adat_Szatube_AFT Elem = (from a in AdatokAft
                                             orderby a.Sorszám descending
                                             select a).FirstOrDefault();
                    double sorszám = 1;
                    if (Elem != null) sorszám = Elem.Sorszám + 1;

                    Adat_Szatube_AFT ADAT = new Adat_Szatube_AFT(
                        sorszám,
                        Rekord_Új.Dolgozószám.Trim(),
                        Dolgozónév.Trim(),
                        Rekord_Új.Nap,
                        Rekord_Új.AFTóra,
                        Rekord_Új.AFTok.Trim(),
                        0,
                        Program.PostásNév.Trim(),
                        DateTime.Now);
                    KézAft.Rögzítés(Cmbtelephely, Dátum.Year, ADAT);
                }
                else
                {
                    if (Rekord_Új.AFTóra == 0)
                    {
                        // ha lenullázzuk akkor a státust állítjuk
                        KézAft.StátusÁllítás(Cmbtelephely, Dátum.Year, Rekord_Új.Nap, Rekord_Új.Dolgozószám.Trim(), 3);
                    }
                    else
                    {
                        // Módosítjuk 
                        Adat_Szatube_AFT ADAT = new Adat_Szatube_AFT(
                              0,
                              Rekord_Új.Dolgozószám.Trim(),
                              Dolgozónév.Trim(),
                              Rekord_Új.Nap,
                              Rekord_Új.AFTóra,
                              Rekord_Új.AFTok.Trim(),
                              0,
                              Program.PostásNév.Trim(),
                              DateTime.Now);
                        KézAft.Módosítás(Cmbtelephely, Dátum.Year, ADAT);
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

        private void Aft_Törlés(string Cmbtelephely, DateTime Dátum, Adat_Dolgozó_Beosztás_Új Rekord_Új)
        {
            try
            {
                AdatokAft = KézAft.Lista_Adatok(Cmbtelephely.Trim(), Dátum.Year);
                Adat_Szatube_AFT AdatAft = (from a in AdatokAft
                                            where a.Törzsszám == Rekord_Új.Dolgozószám.Trim()
                                            && a.Dátum.ToShortDateString() == Rekord_Új.Nap.ToShortDateString()
                                            && a.Státus != 3
                                            select a).FirstOrDefault();

                if (AdatAft != null)
                {
                    KézAft.StátusÁllítás(Cmbtelephely, Dátum.Year, Rekord_Új.Nap, Rekord_Új.Dolgozószám.Trim(), 3);
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

        public void Ellenőrzés_Aft(string Cmbtelephely, DateTime Dátum, string HR_Azonosító)
        {
            try
            {
                DateTime hónapelső = MyF.Hónap_elsőnapja(Dátum);
                DateTime hónaputolsó = MyF.Hónap_utolsónapja(Dátum);

                List<Adat_Dolgozó_Alap> Adatok_Dolg = KézDolg.Lista_Adatok(Cmbtelephely.Trim());

                AdatokAft = KézAft.Lista_Adatok(Cmbtelephely.Trim(), Dátum.Year);
                List<Adat_Szatube_AFT> Adatok_Aft = (from a in AdatokAft
                                                     where a.Törzsszám == HR_Azonosító.Trim()
                                                     && a.Dátum >= hónapelső
                                                     && a.Dátum <= hónaputolsó
                                                     && a.Státus != 3
                                                     select a).ToList();

                List<Adat_Dolgozó_Beosztás_Új> Adatok_Beo = KézBEO.Lista_Adatok(Cmbtelephely.Trim(), Dátum);
                Adatok_Beo = Adatok_Beo.Where(x => x.Dolgozószám.Trim() == HR_Azonosító.Trim() && x.AFTóra != 0).ToList();

                //Megkeressük a beosztás táblában a SZATUBE-ben tároltat, ha nem létezik akkor töröltre állítjuk
                foreach (Adat_Szatube_AFT rekord in Adatok_Aft)
                {
                    int óra = (from a in Adatok_Beo
                               where rekord.Dátum.ToString("yyyy.MM.dd") == a.Nap.ToString("yyyy.MM.dd")
                               select a.AFTóra).FirstOrDefault();

                    if (rekord.AFTóra != óra)
                    {
                        KézAft.StátusÁllítás(Cmbtelephely, Dátum.Year, rekord.Dátum, HR_Azonosító, 3);
                    }
                }

                // leellenőrizzük, hogy a beosztás táblában létezik és ha SZATUBE nem létezik akkor rögzítjük.
                Adatok_Aft = (from a in AdatokAft
                              where a.Törzsszám == HR_Azonosító.Trim()
                              && a.Dátum >= hónapelső
                              && a.Dátum <= hónaputolsó
                              && a.Státus != 3
                              select a).ToList();
                foreach (Adat_Dolgozó_Beosztás_Új rekord in Adatok_Beo)
                {
                    int Óra = (from a in Adatok_Aft
                               where rekord.Nap.ToString("yyyy.MM.dd") == a.Dátum.ToString("yyyy.MM.dd")
                               select a.AFTóra).FirstOrDefault();
                    if (rekord.AFTóra != Óra)
                    {
                        string DolgozóNév = Adatok_Dolg.Where(x => x.Dolgozószám.Trim() == HR_Azonosító.Trim()).Select(y => y.DolgozóNév.Trim()).FirstOrDefault();
                        AFT_Átírás(Cmbtelephely, Dátum, rekord, DolgozóNév);
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
        #endregion


        #region Csúsztatás
        public void Rögzít_Csúsztatás(string Cmbtelephely, DateTime Dátum, string Beosztáskód, string HR_Azonosító, int Ledolgozott, DateTime CSúszórakezd, DateTime Csúszóravég, int Csúszóra, string Csúszok, string Dolgozónév)
        {
            string szöveg = "Nincs hiba";
            try
            {
                List<Adat_Dolgozó_Beosztás_Új> Rekordok = KézBEO.Lista_Adatok(Cmbtelephely.Trim(), Dátum);
                Rekordok = Rekordok.Where(x => x.Dolgozószám.Trim() == HR_Azonosító.Trim() && x.Nap.ToShortDateString() == Dátum.ToShortDateString()).ToList();

                if (Rekordok.Count < 1)
                {
                    Adat_Dolgozó_Beosztás_Új ADAT = new Adat_Dolgozó_Beosztás_Új(
                                HR_Azonosító.Trim(),
                                Dátum,
                                Beosztáskód.Trim(),
                                Ledolgozott,
                                0, new DateTime(1900, 1, 1, 0, 0, 0), new DateTime(1900, 1, 1, 0, 0, 0),
                                Csúszóra, CSúszórakezd, Csúszóravég,
                                "",
                                "",
                                "",
                                false,
                                Csúszok,
                                0, "");
                    List<Adat_Dolgozó_Beosztás_Új> ADATOK = new List<Adat_Dolgozó_Beosztás_Új>
                    {
                        ADAT
                    };
                    KézBEO.Rögzítés(Cmbtelephely, Dátum, ADATOK);
                }
                else
                {
                    Adat_Dolgozó_Beosztás_Új ADAT = new Adat_Dolgozó_Beosztás_Új(
                               HR_Azonosító.Trim(),
                               Dátum,
                               Beosztáskód.Trim(),
                               Ledolgozott,
                               Csúszóra, CSúszórakezd, Csúszóravég,
                               Csúszok);
                    KézBEO.MódosításCsúsz(Cmbtelephely, Dátum, ADAT);
                }

                Rekordok = KézBEO.Lista_Adatok(Cmbtelephely.Trim(), Dátum);
                Rekordok = Rekordok.Where(x => x.Dolgozószám.Trim() == HR_Azonosító.Trim() && x.Nap.ToShortDateString() == Dátum.ToShortDateString()).ToList();
                Adat_Dolgozó_Beosztás_Új Rekord_Új = Rekordok.FirstOrDefault();

                Naplózás(Cmbtelephely, Rekord_Új, Dolgozónév);
                if (Rekord_Új.Csúszóra != 0)
                    Csúsztatás_Átírás(Cmbtelephely, Dátum, Rekord_Új, Dolgozónév);
                else
                    Csúsztatás_Törlés(Cmbtelephely, Dátum, Rekord_Új);

                Ellenőrzés_Csúsztatás(Cmbtelephely, Dátum, HR_Azonosító);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, szöveg, ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Ellenőrzés_Csúsztatás(string Cmbtelephely, DateTime Dátum, string HR_Azonosító)
        {
            try
            {
                DateTime hónapelső = MyF.Hónap_elsőnapja(Dátum);
                DateTime hónaputolsó = MyF.Hónap_utolsónapja(Dátum);

                List<Adat_Szatube_Csúsztatás> AdatokCsúszik = KézCsúsztatás.Lista_Adatok(Cmbtelephely.Trim(), Dátum.Year);
                AdatokCsúszik = AdatokCsúszik.Where(x => x.Törzsszám == HR_Azonosító.Trim()
                                                     && x.Kezdődátum >= hónapelső
                                                     && x.Befejeződátum <= hónaputolsó
                                                     && x.Státus != 3).ToList();

                List<Adat_Dolgozó_Beosztás_Új> Adatok_Beo = KézBEO.Lista_Adatok(Cmbtelephely.Trim(), Dátum);
                Adatok_Beo = Adatok_Beo.Where(x => x.Dolgozószám.Trim() == HR_Azonosító.Trim() && x.Csúszóra != 0).ToList();

                //Megkeressük a beosztás táblában a SZATUBE-ben tároltat, ha nem létezik akkor töröltre állítjuk
                List<Adat_Szatube_Csúsztatás> AdatokGY = new List<Adat_Szatube_Csúsztatás>();
                foreach (Adat_Szatube_Csúsztatás rekord in AdatokCsúszik)
                {
                    int óra = (from a in Adatok_Beo
                               where rekord.Kezdődátum.ToString("yyyy.MM.dd") == a.Nap.ToString("yyyy.MM.dd")
                               select a.Csúszóra).FirstOrDefault();

                    if (rekord.Kivettnap != óra)
                    {
                        Adat_Szatube_Csúsztatás ADAT = new Adat_Szatube_Csúsztatás(
                            HR_Azonosító.Trim(),
                            rekord.Kezdődátum,
                            3);
                        AdatokGY.Add(ADAT);
                    }
                }
                if (AdatokGY.Count > 0) KézCsúsztatás.Módosítás(Cmbtelephely, Dátum.Year, AdatokGY);
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

        private void Csúsztatás_Átírás(string Cmbtelephely, DateTime Dátum, Adat_Dolgozó_Beosztás_Új Rekord_Új, string Dolgozónév)
        {
            try
            {
                AdatokCsúsztatás = KézCsúsztatás.Lista_Adatok(Cmbtelephely.Trim(), Dátum.Year);
                Adat_Szatube_Csúsztatás AdatCsúsztatás = (from a in AdatokCsúsztatás
                                                          where a.Törzsszám == Rekord_Új.Dolgozószám.Trim()
                                                          && a.Kezdődátum.ToShortDateString() == Rekord_Új.Nap.ToShortDateString()
                                                          && a.Státus != 3
                                                          select a).FirstOrDefault();

                if (AdatCsúsztatás == null)
                {
                    Adat_Szatube_Csúsztatás Elem = (from a in AdatokCsúsztatás
                                                    orderby a.Sorszám descending
                                                    select a).FirstOrDefault();
                    double sorszám = 1;
                    if (Elem != null) sorszám = Elem.Sorszám + 1;

                    Adat_Szatube_Csúsztatás ADAT = new Adat_Szatube_Csúsztatás(
                        sorszám,
                        Rekord_Új.Dolgozószám.Trim(),
                        Dolgozónév.Trim(),
                        Rekord_Új.Nap,
                        Rekord_Új.Nap,
                        Rekord_Új.Csúszóra,
                        Rekord_Új.Csúszok.Trim(),
                        0,
                        Program.PostásNév.Trim(),
                        DateTime.Now,
                        Rekord_Új.CSúszórakezd,
                        Rekord_Új.Csúszóravég);
                    KézCsúsztatás.Rögzítés(Cmbtelephely, Dátum.Year, ADAT);
                }
                else
                {
                    if (Rekord_Új.Csúszóra == 0)
                    {   // ha lenullázzuk akkor a státust állítjuk
                        Adat_Szatube_Csúsztatás ADAT = new Adat_Szatube_Csúsztatás(
                            Rekord_Új.Dolgozószám.Trim(),
                            Rekord_Új.Nap,
                            3);
                        List<Adat_Szatube_Csúsztatás> Adatok = new List<Adat_Szatube_Csúsztatás>
                        {
                            ADAT
                        };
                        KézCsúsztatás.Módosítás(Cmbtelephely, Dátum.Year, Adatok);
                    }
                    else
                    {
                        // Módosítjuk 
                        Adat_Szatube_Csúsztatás ADAT = new Adat_Szatube_Csúsztatás(
                            Rekord_Új.Dolgozószám.Trim(),
                            Rekord_Új.Nap,
                            3,
                            Rekord_Új.Csúszóra,
                            Rekord_Új.Csúszok.Trim(),
                            Program.PostásNév.Trim(),
                            DateTime.Now,
                            Rekord_Új.CSúszórakezd,
                            Rekord_Új.Csúszóravég);
                        KézCsúsztatás.Módosítás(Cmbtelephely, Dátum.Year, ADAT);
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

        private void Csúsztatás_Törlés(string Cmbtelephely, DateTime Dátum, Adat_Dolgozó_Beosztás_Új Rekord_Új)
        {
            try
            {
                AdatokCsúsztatás = KézCsúsztatás.Lista_Adatok(Cmbtelephely.Trim(), Dátum.Year);
                Adat_Szatube_Csúsztatás AdatCsúsztatás = (from a in AdatokCsúsztatás
                                                          where a.Törzsszám == Rekord_Új.Dolgozószám.Trim()
                                                          && a.Kezdődátum.ToShortDateString() == Rekord_Új.Nap.ToShortDateString()
                                                          && a.Státus != 3
                                                          select a).FirstOrDefault();

                if (AdatCsúsztatás != null)
                {
                    Adat_Szatube_Csúsztatás Adat = new Adat_Szatube_Csúsztatás(
                        Rekord_Új.Dolgozószám.Trim(),
                        Rekord_Új.Nap,
                        3);
                    KézCsúsztatás.Módosítás(Cmbtelephely, Dátum.Year, Adat);
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
    }
}