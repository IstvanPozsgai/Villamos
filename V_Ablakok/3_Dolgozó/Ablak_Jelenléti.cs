using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_Adatszerkezet;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos
{
    internal partial class Ablak_Jelenléti
    {
        #region Kezelők és Listák
        readonly Kezelő_Kiegészítő_Jelenlétiív KézJelenléti = new Kezelő_Kiegészítő_Jelenlétiív();
        readonly Kezelő_Kiegészítő_főkönyvtábla KézFőkönyv = new Kezelő_Kiegészítő_főkönyvtábla();
        readonly Kezelő_Kiegészítő_Csoportbeosztás KézCsopBeo = new Kezelő_Kiegészítő_Csoportbeosztás();
        readonly Kezelő_Dolgozó_Alap KézDolg = new Kezelő_Dolgozó_Alap();
        readonly Kezelő_Kiegészítő_Váltóstábla KézVáltó = new Kezelő_Kiegészítő_Váltóstábla();
        readonly Kezelő_Kiegészítő_Beosztásciklus KézBeo = new Kezelő_Kiegészítő_Beosztásciklus();
        readonly Kezelő_Kiegészítő_Beosztáskódok KézBeoKód = new Kezelő_Kiegészítő_Beosztáskódok();
        readonly Kezelő_Dolgozó_Beosztás_Új KézBeosztásÚj = new Kezelő_Dolgozó_Beosztás_Új();

        List<Adat_Kiegészítő_Jelenlétiív> AdatokJelenléti = new List<Adat_Kiegészítő_Jelenlétiív>();
        List<Adat_Kiegészítő_főkönyvtábla> AdatokFőkönyv = new List<Adat_Kiegészítő_főkönyvtábla>();
        #endregion


        #region Paraméterek
        readonly private Beállítás_Betű beállBetű = new Beállítás_Betű();
        readonly private Beállítás_Betű beállB7 = new Beállítás_Betű { Méret = 7 };
        readonly private Beállítás_Betű beállB12 = new Beállítás_Betű { Méret = 12 };
        readonly private Beállítás_Betű beállB12V = new Beállítás_Betű { Méret = 12, Vastag = true };
        readonly private Beállítás_Betű beállB12D = new Beállítás_Betű { Méret = 12, Dőlt = true };
        readonly private Beállítás_Betű beállB14 = new Beállítás_Betű { Méret = 14 };
        readonly private Beállítás_Betű beállB14V = new Beállítás_Betű { Méret = 14, Vastag = true };
        readonly private Beállítás_Betű beállB16V = new Beállítás_Betű { Méret = 16, Vastag = true };
        readonly private Beállítás_Betű beállB20 = new Beállítás_Betű { Méret = 20 };
        readonly private Beállítás_Betű beállB20V = new Beállítás_Betű { Méret = 20, Vastag = true };
        readonly private string munkalap = "Munka1";

        #endregion
        #region Alap
        public Ablak_Jelenléti()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            try
            {
                //Ha van 0-tól különböző akkor a régi jogosultságkiosztást használjuk
                //ha mind 0 akkor a GombLathatosagKezelo-t használjuk
                if (Program.PostásJogkör.Any(c => c != '0'))
                {
                    Telephelyekfeltöltése();
                }
                else
                {
                    TelephelyekFeltöltéseÚj();
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
                }
                Csoportfeltöltés();
                Névfeltöltés();
                Irányítófeltöltés();

                AdatokJelenléti = KézJelenléti.Lista_Adatok(Cmbtelephely.Text.Trim());
                AláíróListaFeltöltés();
                Aláíróbetöltés();

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


        private void Btn_Súgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\jelenléti.html";
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

        private void AblakJelenléti_Load(object sender, EventArgs e)
        {

        }

        private void Cmbtelephely_SelectedIndexChanged(object sender, EventArgs e)
        {
            Csoportfeltöltés();
            Névfeltöltés();
            Irányítófeltöltés();

            AdatokJelenléti = KézJelenléti.Lista_Adatok(Cmbtelephely.Text.Trim());
            AláíróListaFeltöltés();
            Aláíróbetöltés();
        }

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Személy(true))
                    Cmbtelephely.Items.Add(Elem);
                if (Program.PostásTelephely == "Főmérnökség")
                    Cmbtelephely.Text = Cmbtelephely.Items[0].ToString().Trim();
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


        #endregion


        #region Listák
        private void Csoportfeltöltés()
        {
            try
            {
                ChkCsoport.Items.Clear();
                List<Adat_Kiegészítő_Csoportbeosztás> Adatok = KézCsopBeo.Lista_Adatok(Cmbtelephely.Text.Trim());
                foreach (Adat_Kiegészítő_Csoportbeosztás rekord in Adatok)
                    ChkCsoport.Items.Add(rekord.Csoportbeosztás);

                ChkCsoport.EndUpdate();
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

        void Névfeltöltés()
        {
            try
            {
                ChkDolgozónév.Items.Clear();
                ChkDolgozónév.BeginUpdate();
                List<Adat_Dolgozó_Alap> Adatok = KézDolg.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adatok = (from a in Adatok
                          where a.Kilépésiidő == new DateTime(1900, 1, 1)
                          select a).ToList();

                foreach (Adat_Dolgozó_Alap rekord in Adatok)
                    ChkDolgozónév.Items.Add(rekord.DolgozóNév.Trim() + "=" + rekord.Dolgozószám.Trim());

                ChkDolgozónév.EndUpdate();
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

        private void Irányítófeltöltés()
        {
            LstKiadta.Items.Clear();
            LstKiadta.Items.Add("");
            List<Adat_Dolgozó_Alap> Adatok = KézDolg.Lista_Adatok(Cmbtelephely.Text.Trim());
            Adatok = (from a in Adatok
                      where a.Kilépésiidő == new DateTime(1900, 1, 1)
                      && (a.Főkönyvtitulus != "" || a.Főkönyvtitulus != "_")
                      select a).ToList();

            foreach (Adat_Dolgozó_Alap rekord in Adatok)
                LstKiadta.Items.Add(rekord.DolgozóNév.Trim());
        }

        private void Aláíróbetöltés()
        {
            try
            {
                Adat_Kiegészítő_főkönyvtábla Elem = (from a in AdatokFőkönyv
                                                     where a.Id == 2
                                                     select a).FirstOrDefault();
                if (Elem != null) RdBtnÜzemvezető.Text = Elem.Beosztás;

                Elem = (from a in AdatokFőkönyv
                        where a.Id == 3
                        select a).FirstOrDefault();
                if (Elem != null) RdBtnSzakszolgálatVezető.Text = Elem.Beosztás;

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

        private void AláíróListaFeltöltés()
        {
            try
            {
                AdatokFőkönyv.Clear();
                AdatokFőkönyv = KézFőkönyv.Lista_Adatok(Cmbtelephely.Text.Trim());
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

        private void BtnKijelölcsop_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ChkCsoport.Items.Count; i++)
                ChkCsoport.SetItemChecked(i, true);
            Jelöltcsoport();
        }

        private void Btnkilelöltörlés_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ChkCsoport.Items.Count; i++)
                ChkCsoport.SetItemChecked(i, false);
            Jelöltcsoport();
        }

        private void Btnmindkijelöl_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ChkDolgozónév.Items.Count; i++)
                ChkDolgozónév.SetItemChecked(i, true);
        }

        private void Btnkijelöléstöröl_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ChkDolgozónév.Items.Count; i++)
                ChkDolgozónév.SetItemChecked(i, false);
        }

        private void BtnKijelölésátjelöl_Click(object sender, EventArgs e)
        {
            Jelöltcsoport();
        }

        private void Jelöltcsoport()
        {
            try
            {
                ChkDolgozónév.Items.Clear();
                ChkDolgozónév.BeginUpdate();

                List<Adat_Dolgozó_Alap> AdatokÖ = KézDolg.Lista_Adatok(Cmbtelephely.Text.Trim());
                AdatokÖ = (from a in AdatokÖ
                           where a.Kilépésiidő == new DateTime(1900, 1, 1)
                           select a).ToList();

                foreach (string Elem in ChkCsoport.CheckedItems)
                {
                    List<Adat_Dolgozó_Alap> Adatok = (from a in AdatokÖ
                                                      where a.Csoport == Elem
                                                      select a).ToList();
                    foreach (Adat_Dolgozó_Alap rekord in Adatok)
                        ChkDolgozónév.Items.Add(rekord.DolgozóNév.Trim() + " = " + rekord.Dolgozószám.Trim());
                }
                ChkDolgozónév.EndUpdate();
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


        #region Jelenléti ívek
        private void Btn_Heti_Click(object sender, EventArgs e)
        {
            try
            {

                int hányember = ChkDolgozónév.CheckedItems.Count;
                if (hányember < 1) throw new HibásBevittAdat("Nincs kijelölve egy dolgozó sem.");

                Holtart.Be();

                string fájlexc = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Jelenléti_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddhhmmss}.xlsx";
                MyX.ExcelLétrehozás(munkalap);


                MyX.Munkalap_betű(munkalap, beállBetű);

                MyX.Betű(munkalap, "a1", beállB14V);


                Adat_Kiegészítő_Jelenlétiív Eleme = (from a in AdatokJelenléti
                                                     where a.Id == 1
                                                     select a).FirstOrDefault();
                if (Eleme != null) MyX.Kiir(Eleme.Szervezet, "a2");

                MyX.Kiir("1. oldal", "a1");
                MyX.Oszlopszélesség(munkalap, "a:a", 13);
                MyX.Oszlopszélesség(munkalap, "b:b", 25);


                MyX.Betű(munkalap, "k1", beállB16V);

                MyX.Kiir("JELENLÉTI ÍV", "k1");

                MyX.Betű(munkalap, "2:2", beállB12);
                MyX.Kiir("HR azonosító", "a5");
                int mennyi = 0;
                DateTime elsőnap = MyF.Hét_elsőnapja(Dátum.Value);
                DateTime utolsónap = MyF.Hét_Utolsónapja(Dátum.Value);
                MyX.Kiir("A munkavállaló neve", "b5");

                if (RdBtn5Napos.Checked)
                    mennyi = 5;
                if (RdBtn6Napos.Checked)
                    mennyi = 6;
                if (RdBtn7Napos.Checked)
                    mennyi = 7;

                // napok fejlécet létrehozzuk
                string szöveg = $"{elsőnap:yyyy. év MMMM dd}.-tól - ";
                szöveg += $"{utolsónap:yyyy. év MMMM dd}.-ig ";
                MyX.Kiir(szöveg, "k3");

                // sormagasság
                MyX.Sormagasság(munkalap, "5:5", 120);
                int oszlop = 3;
                int sor = 1;
                for (int j = 0; j < mennyi; j++)
                {
                    MyX.Kiir($"{elsőnap.AddDays(j):dddd}", $"{MyF.Oszlopnév(oszlop)}4");
                    MyX.Sortörésseltöbbsorba(munkalap, MyF.Oszlopnév(oszlop) + "4:" + MyF.Oszlopnév(oszlop + 1) + "4", true);
                    if (!Éjszakás.Checked)
                    {
                        MyX.Kiir($"{elsőnap.AddDays(j):dd}", $"{MyF.Oszlopnév(oszlop + 2)}4");
                        MyX.Kiir("Érkezés ideje", MyF.Oszlopnév(oszlop) + "5");
                        MyX.Kiir("Távozás ideje", MyF.Oszlopnév(oszlop + 1) + "5");
                    }
                    else
                    {
                        MyX.Kiir($"Érkezés ideje {elsőnap.AddDays(j):MM.dd.}", $"{MyF.Oszlopnév(oszlop)}5");
                        MyX.Kiir($"Távozás ideje {elsőnap.AddDays(j + 1):MM.dd.}", $"{MyF.Oszlopnév(oszlop + 1)}5");
                    }

                    MyX.Kiir("A dolgozó aláírása", MyF.Oszlopnév(oszlop + 2) + "5");
                    MyX.SzövegIrány(munkalap, MyF.Oszlopnév(oszlop) + "5:" + MyF.Oszlopnév(oszlop + 2) + "5", 90);
                    MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(oszlop) + ":" + MyF.Oszlopnév(oszlop + 1), 6);
                    MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(oszlop + 2) + ":" + MyF.Oszlopnév(oszlop + 2), 10);

                    oszlop += 3;
                }
                // beírjuk a neveket
                int hanyadikember = 0;
                int l = 0;
                foreach (string Elemei in ChkDolgozónév.CheckedItems)
                {
                    // hrazonosító
                    string[] darabol = Elemei.Split('=');
                    MyX.Kiir(darabol[1], "a" + (hanyadikember + 6).ToString());
                    // dolgozó név
                    MyX.Kiir(darabol[0], "b" + (hanyadikember + 6).ToString());
                    l++;
                    hanyadikember += 1;
                    Holtart.Lép();
                }

                hanyadikember += 2;
                // sormagasság
                MyX.Sormagasság(munkalap, "6:" + (hanyadikember + 6).ToString(), 24);
                MyX.Egyesít(munkalap, "a" + (hanyadikember + 6).ToString() + ":b" + (hanyadikember + 6).ToString());
                MyX.Kiir(" Az igazoló aláírása:\n" + LstKiadta.Text.Trim(), "a" + (hanyadikember + 6).ToString());
                oszlop = 3;
                // formázunk
                MyX.Vastagkeret(munkalap, "a4:a5");
                MyX.Vastagkeret(munkalap, "b4:b5");
                MyX.Rácsoz(munkalap, $"A6:A{hanyadikember + 5}");
                MyX.Rácsoz(munkalap, $"B6:B{hanyadikember + 5}");

                for (int i = 0; i < mennyi; i++)
                {
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlop) + "4:" + MyF.Oszlopnév(oszlop + 2) + "5");
                    MyX.Rácsoz(munkalap, $"{MyF.Oszlopnév(oszlop)}6:{MyF.Oszlopnév(oszlop + 2)}{hanyadikember + 5}");
                    MyX.Egyesít(munkalap, $"{MyF.Oszlopnév(oszlop)}{hanyadikember + 6}:{MyF.Oszlopnév(oszlop + 2)}{hanyadikember + 6}");
                    MyX.Vastagkeret(munkalap, $"{MyF.Oszlopnév(oszlop)}{hanyadikember + 6}:{MyF.Oszlopnév(oszlop + 2)}{hanyadikember + 6}");
                    oszlop += 3;
                }

                // igazoló rész
                Holtart.Lép();
                MyX.Sormagasság(munkalap, (hanyadikember + 6).ToString() + ":" + (hanyadikember + 6).ToString(), 36);
                int vege = 2 + mennyi * 3;
                MyX.Vastagkeret(munkalap, $"A{hanyadikember + 6}:B{hanyadikember + 6}");

                sor = 9 + hanyadikember;
                MyX.Kiir(" Az igazoló aláírása:", $"a{sor}");

                // kiirjuk a személyeket az ellenőrző személyeket
                Holtart.Lép();

                Adat_Kiegészítő_főkönyvtábla Elem;
                if (RdBtnÜzemvezető.Checked)
                    Elem = (from a in AdatokFőkönyv
                            where a.Id == 2
                            select a).FirstOrDefault();
                else
                    Elem = (from a in AdatokFőkönyv
                            where a.Id == 3
                            select a).FirstOrDefault();

                sor += 2;
                MyX.Betű(munkalap, $"e{sor}", beállB14);

                if (Elem != null)
                {
                    MyX.Kiir(Elem.Név, $"e{sor}");
                    sor += 1;
                    MyX.Betű(munkalap, $"e{sor}", beállB14);
                    MyX.Kiir(Elem.Beosztás, $"e{sor}");
                }
                MyX.Oszlopszélesség(munkalap, "B:B");

                Holtart.Lép();
                Beállítás_Nyomtatás BeNyom = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"a1:w{sor}",
                    LapMagas = 1,
                    LapSzéles = 1,
                    Álló = false,
                    Papírméret = RdBtnA4.Checked ? "A4" : "A3",
                    BalMargó = 10,
                    JobbMargó = 10,
                    FelsőMargó = 15,
                    AlsóMargó = 15,
                    FejlécMéret = 13,
                    LáblécMéret = 13,
                    VízKözép = true
                };
                MyX.NyomtatásiTerület_részletes(munkalap, BeNyom);

                MyX.ExcelMentés(fájlexc);

                List<string> Fájlok = new List<string> { fájlexc };
                if (RdBtnNyomtat.Checked) MyF.ExcelNyomtatás(Fájlok);

                MyX.ExcelBezárás();
                if (RdBtnFájlTöröl.Checked) File.Delete(fájlexc);
                Holtart.Ki();
                MessageBox.Show("A nyomtatvány elkészült.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Btn_Szellemi_Click(object sender, EventArgs e)
        {
            try
            {

                int hányember = ChkDolgozónév.CheckedItems.Count;
                if (hányember < 1) throw new HibásBevittAdat("Nincs kijelölve egy dolgozó sem.");

                Holtart.Be();
                string fájlexc = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Jelenléti_Szellemi_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddhhmmss}.xlsx";

                MyX.ExcelLétrehozás(munkalap);

                // minden cella betűméret
                MyX.Munkalap_betű(munkalap, beállBetű);

                MyX.Kiir("1. oldal", "a6");
                MyX.Kiir("Szellemi állomány", "a2");
                MyX.Oszlopszélesség(munkalap, "a:a", 7);
                MyX.Oszlopszélesség(munkalap, "b:b", 25);
                MyX.Oszlopszélesség(munkalap, "c:w", 8);

                MyX.Betű(munkalap, "a1", beállB14V);

                Adat_Kiegészítő_Jelenlétiív Eleme = (from a in AdatokJelenléti
                                                     where a.Id == 1
                                                     select a).FirstOrDefault();
                if (Eleme != null) MyX.Kiir(Eleme.Szervezet, "a1");

                DateTime elsőnap = MyF.Hét_elsőnapja(Dátum.Value);
                DateTime utolsónap = MyF.Hét_Utolsónapja(Dátum.Value);



                MyX.Betű(munkalap, "2:2", beállB12);

                MyX.Betű(munkalap, "j3", beállB14V);

                MyX.Kiir("Jelenléti ív", "j3");
                MyX.Betű(munkalap, "m5", beállB14V);

                // napok fejlécet létrehozzuk
                string szöveg = $"{elsőnap:yyyy. év MMMM dd}.-tól - ";
                szöveg += $"{utolsónap:yyyy. év MMMM dd}.-ig ";
                MyX.Kiir(szöveg, "m5");
                int oszlop = 3;

                for (int i = 0; i < 7; i++)
                {
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(oszlop) + "7:" + MyF.Oszlopnév(oszlop + 1) + "7");
                    MyX.Kiir($"{elsőnap.AddDays(i):dddd}", $"{MyF.Oszlopnév(oszlop)}7");
                    MyX.Betű(munkalap, MyF.Oszlopnév(oszlop) + "7", beállB7);
                    MyX.Kiir($"{elsőnap.AddDays(i):dd}", $"{MyF.Oszlopnév(oszlop + 2)}7");
                    MyX.Betű(munkalap, MyF.Oszlopnév(oszlop + 2) + "7", beállB7);
                    MyX.Kiir("Érkezés ideje", MyF.Oszlopnév(oszlop) + "8");
                    MyX.Kiir("Távozás ideje", MyF.Oszlopnév(oszlop + 1) + "8");
                    MyX.Kiir("A dolgozó aláírása", MyF.Oszlopnév(oszlop + 2) + "8");
                    oszlop += 3;
                }

                MyX.Sormagasság(munkalap, "7:7", 31);
                MyX.Sormagasság(munkalap, "8:8", 90);
                MyX.Kiir("Hr azonosító", "a8");
                MyX.Kiir("Dolgozó neve", "b8");
                MyX.SzövegIrány(munkalap, "A8", 90);
                MyX.SzövegIrány(munkalap, "c8:W8", 90);
                MyX.Betű(munkalap, "7:7", beállB12V);
                MyX.Igazít_függőleges(munkalap, "8:7", "alsó");

                int hanyadikember = 0;
                int l = 0;
                foreach (string Ele in ChkDolgozónév.CheckedItems)
                {
                    // hrazonosító
                    string[] darabol = Ele.Split('=');
                    MyX.Kiir(darabol[1], "a" + (hanyadikember + 9).ToString());
                    // dolgozó név
                    MyX.Kiir(darabol[0], "b" + (hanyadikember + 9).ToString());
                    l++;
                    hanyadikember += 1;
                    Holtart.Lép();
                }
                hanyadikember += 3;

                MyX.Egyesít(munkalap, "a" + (hanyadikember + 8).ToString() + ":b" + (hanyadikember + 8).ToString());
                MyX.Kiir("Igazolta:" + LstKiadta.Text.Trim(), "a" + (hanyadikember + 8).ToString());

                // formázunk
                // fejléc rácsozás
                MyX.Rácsoz(munkalap, "a7:W8");
                MyX.Rácsoz(munkalap, "c7:e8");
                MyX.Rácsoz(munkalap, "f7:h8");
                MyX.Rácsoz(munkalap, "i7:k8");
                MyX.Rácsoz(munkalap, "l7:n8");
                MyX.Rácsoz(munkalap, "o7:q8");
                MyX.Rácsoz(munkalap, "r7:t8");
                MyX.Rácsoz(munkalap, "u7:w8");

                MyX.Rácsoz(munkalap, "a9:" + MyF.Oszlopnév(2 + 7 * 3) + (hanyadikember + 7).ToString());

                MyX.Egyesít(munkalap, $"a{hanyadikember + 8}:b{hanyadikember + 8}");


                oszlop = 3;
                string csekkoló;
                if (ChckBxHétfő.Checked)
                    csekkoló = "1";
                else
                    csekkoló = "0";
                if (ChckBxHKedd.Checked)
                    csekkoló += "1";
                else
                    csekkoló += "0";
                if (ChckBxHSzerda.Checked)
                    csekkoló += "1";
                else
                    csekkoló += "0";
                if (ChckBxHCsütörtök.Checked)
                    csekkoló += "1";
                else
                    csekkoló += "0";
                if (ChckBxHPéntek.Checked)
                    csekkoló += "1";
                else
                    csekkoló += "0";
                if (ChckBxHSzombat.Checked)
                    csekkoló += "1";
                else
                    csekkoló += "0";
                if (ChckBxHVasárnap.Checked)
                    csekkoló += "1";
                else
                    csekkoló += "0";

                for (int i = 0; i < 7; i++)
                {
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(oszlop) + (hanyadikember + 8).ToString() + ":" + MyF.Oszlopnév(oszlop + 2) + (hanyadikember + 8).ToString());
                    if (MyF.Szöveg_Tisztítás(csekkoló, i, 1) == "0")
                    {
                        MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlop) + "9:" + MyF.Oszlopnév(oszlop + 2) + (hanyadikember + 7).ToString());
                        MyX.Vastagkeret(munkalap, MyF.Oszlopnév(oszlop) + $"{hanyadikember + 8}:" + MyF.Oszlopnév(oszlop + 2) + (hanyadikember + 8).ToString());
                    }
                    else
                    {
                        MyX.Egyesít(munkalap, MyF.Oszlopnév(oszlop) + "9:" + MyF.Oszlopnév(oszlop + 2) + (hanyadikember + 7).ToString());
                        MyX.Kiir("Pihenőnap", MyF.Oszlopnév(oszlop) + "9:" + MyF.Oszlopnév(oszlop + 2) + (hanyadikember + 7).ToString());
                        Beállítás_Ferde beállFerde = new Beállítás_Ferde
                        {
                            Munkalap = munkalap,
                            Terület = MyF.Oszlopnév(oszlop) + "9:" + MyF.Oszlopnév(oszlop + 2) + (hanyadikember + 7).ToString(),
                            Jobb = true
                        };
                        MyX.FerdeVonal(beállFerde);
                        MyX.Vastagkeret(munkalap, MyF.Oszlopnév(oszlop) + $"{hanyadikember + 8}:" + MyF.Oszlopnév(oszlop + 2) + (hanyadikember + 8).ToString());
                    }
                    oszlop += 3;
                }
                MyX.Vastagkeret(munkalap, $"A{hanyadikember + 8}:B{(hanyadikember + 8)}");
                MyX.Sormagasság(munkalap, "9:" + (hanyadikember + 8).ToString(), 20);
                int sor = hanyadikember + 10;
                // kiirjuk a személyeket az ellenőrző személyeket
                Adat_Kiegészítő_főkönyvtábla Elem;
                if (RdBtnÜzemvezető.Checked)
                    Elem = (from a in AdatokFőkönyv
                            where a.Id == 2
                            select a).FirstOrDefault();
                else
                    Elem = (from a in AdatokFőkönyv
                            where a.Id == 3
                            select a).FirstOrDefault();

                sor += 2;
                if (Elem != null)
                {
                    MyX.Kiir(" Az igazoló aláírása:", $"a{sor}");
                    MyX.Betű(munkalap, $"e{sor}", beállB14);
                    MyX.Kiir(Elem.Név, $"e{sor}");
                    sor += 1;
                    MyX.Betű(munkalap, $"e{sor}", beállB14);
                    MyX.Kiir(Elem.Beosztás, $"e{sor}");
                }
                MyX.Oszlopszélesség(munkalap, "B:B");
                // **********************************************
                // **Nyomtatási beállítások                    **
                // **********************************************
                Beállítás_Nyomtatás BeNyom = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"a1:w{sor}",
                    LapMagas = 1,
                    LapSzéles = 1,
                    Álló = false,
                    Papírméret = RdBtnA4.Checked ? "A4" : "A3",
                    BalMargó = 10,
                    JobbMargó = 10,
                    FelsőMargó = 15,
                    AlsóMargó = 15,
                    FejlécMéret = 13,
                    LáblécMéret = 13,
                    VízKözép = true
                };
                MyX.NyomtatásiTerület_részletes(munkalap, BeNyom);

                Holtart.Ki();

                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();

                List<string> Fájlok = new List<string> { fájlexc };
                if (RdBtnNyomtat.Checked) MyF.ExcelNyomtatás(Fájlok);

                if (RdBtnFájlTöröl.Checked) File.Delete(fájlexc);

                MessageBox.Show("A nyomtatvány elkészült.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Btn_Váltós_Click(object sender, EventArgs e)
        {
            try
            {

                int hányember = ChkDolgozónév.CheckedItems.Count;
                if (hányember < 1) throw new HibásBevittAdat("Nincs kijelölve egy dolgozó sem.");

                Holtart.Be();

                int i = 0;
                string fájlexc = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Jelenléti_Váltó_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddhhmmss}.xlsx";

                MyX.ExcelLétrehozás(munkalap);
                MyX.Munkalap_betű(munkalap, beállBetű);
                MyX.Kiir("1. oldal", "a1");
                MyX.Oszlopszélesség(munkalap, "a:a", 13);
                MyX.Oszlopszélesség(munkalap, "b:b", 25);

                MyX.Betű(munkalap, "k1", beállB16V);
                MyX.Kiir("JELENLÉTI ÍV", "k1");

                Adat_Kiegészítő_Jelenlétiív Eleme = (from a in AdatokJelenléti
                                                     where a.Id == 1
                                                     select a).FirstOrDefault();
                if (Eleme != null) MyX.Kiir(Eleme.Szervezet, "a2");


                // napok fejlécet létrehozzuk
                DateTime elsőnap = MyF.Hét_elsőnapja(Dátum.Value.ToString("yyyy.MM.dd").ToÉrt_DaTeTime());
                DateTime utolsónap = MyF.Hét_Utolsónapja(Dátum.Value.ToString("yyyy.MM.dd").ToÉrt_DaTeTime());
                string szöveg = $"{elsőnap:yyyy. év MMMM dd}.-tól - ";
                szöveg += $"{utolsónap:yyyy. év MMMM dd}.-ig ";
                MyX.Kiir(szöveg, "L2");


                MyX.Kiir("Hr azonosító", "a5");
                MyX.Kiir("A munkavállaló neve", "b5");

                // sormagasság
                MyX.Sormagasság(munkalap, "5:5", 120);
                int oszlop = 3;
                int ciklusnap;
                List<Adat_Kiegészítő_Váltóstábla> Adatok = KézVáltó.Lista_Adatok();
                List<Adat_Kiegészítő_Beosztásciklus> AdatokBeo = KézBeo.Lista_Adatok("beosztásciklus");

                int volt = 0;
                int melyikcsoport = 0;
                int rekordciklus = 0;
                foreach (Adat_Kiegészítő_Váltóstábla rekord in Adatok)
                {
                    bool exitDo = false;
                    for (int j = 0; j < ChkCsoport.Items.Count; j++)
                    {
                        if (ChkCsoport.GetItemChecked(j))
                        {

                            if (ChkCsoport.Items[j].ToString().ToUpper().Contains(rekord.Megnevezés.ToString().ToUpper()))
                            {
                                volt = 1;
                                melyikcsoport = rekord.Id;
                                rekordciklus = rekord.Ciklusnap;
                                exitDo = true;
                                break;
                            }
                        }
                    }

                    if (exitDo)
                        break;
                }
                double hanyadik;
                DateTime kezdődátum = new DateTime(1900, 1, 1);
                string Napszíne = "";
                do
                {
                    MyX.Kiir($"{elsőnap.AddDays(i):dddd}", $"{MyF.Oszlopnév(oszlop)}4");
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(oszlop) + "4:" + MyF.Oszlopnév(oszlop + 1) + "4");

                    if (volt == 1)
                    {
                        // ha volt váltócsoport

                        Adat_Kiegészítő_Váltóstábla VElem = (from a in Adatok
                                                             where a.Id == melyikcsoport
                                                             select a).FirstOrDefault();
                        if (VElem != null) kezdődátum = VElem.Kezdődátum;


                        hanyadik = (elsőnap.AddDays(i) - kezdődátum).TotalDays;
                        // hanyadik = elsőnap - rekordváltó("kezdődátum") + i
                        if ((hanyadik / (double)rekordciklus).ToÉrt_Int() == hanyadik / (double)rekordciklus)
                            // ha pont első napra esik
                            ciklusnap = 1;
                        else
                            ciklusnap = (int)Math.Round(1 + (hanyadik / (double)rekordciklus - Math.Floor(hanyadik / (double)rekordciklus)) * 28);

                        Adat_Kiegészítő_Beosztásciklus Kód = (from a in AdatokBeo
                                                              where a.Id == ciklusnap
                                                              select a).FirstOrDefault();
                        string beosztáskód = "_";
                        if (Kód != null) beosztáskód = Kód.Beosztáskód;

                        if (beosztáskód == "_" | beosztáskód == "P" | (beosztáskód) == "")
                        {
                            // ha nem dolgoznak
                            MyX.Kiir($"{elsőnap.AddDays(i):dd}", $"{MyF.Oszlopnév(oszlop + 2)}4");
                            MyX.Kiir("Érkezés ideje", MyF.Oszlopnév(oszlop) + "5");
                            MyX.Kiir("Távozás ideje", MyF.Oszlopnév(oszlop + 1) + "5");
                            MyX.Kiir("A dolgozó aláírása", MyF.Oszlopnév(oszlop + 2) + "5");
                            Napszíne += "1";
                        }
                        if (beosztáskód == "7")
                        {
                            // nappalos
                            MyX.Kiir($"{elsőnap.AddDays(i):dd}", $"{MyF.Oszlopnév(oszlop + 2)}4");
                            MyX.Kiir("A dolgozó aláírása", MyF.Oszlopnév(oszlop + 2) + "5");
                            MyX.Kiir("Érkezés ideje", MyF.Oszlopnév(oszlop) + "5");
                            MyX.Kiir("Távozás ideje", MyF.Oszlopnév(oszlop + 1) + "5");
                            MyX.Betű(munkalap, MyF.Oszlopnév(oszlop) + "5", beállB12D);
                            MyX.Betű(munkalap, MyF.Oszlopnév(oszlop + 1) + "5", beállB12D);
                            MyX.Betű(munkalap, MyF.Oszlopnév(oszlop + 2) + "5", beállB12D);
                            MyX.Betű(munkalap, MyF.Oszlopnév(oszlop) + "4", beállB12D);
                            MyX.Betű(munkalap, MyF.Oszlopnév(oszlop + 2) + "4", beállB12D);
                            Napszíne += "0";
                        }
                        if (beosztáskód == "8")
                        {
                            // éjszakás
                            MyX.Kiir("ÉJ", MyF.Oszlopnév(oszlop + 2) + "4");
                            MyX.Kiir($"Érkezés ideje {elsőnap.AddDays(i):MM.dd.}", $"{MyF.Oszlopnév(oszlop)}5");
                            MyX.Kiir($"Távozás ideje {elsőnap.AddDays(i + 1):MM.dd.}", $"{MyF.Oszlopnév(oszlop + 1)}5");
                            MyX.Kiir("A dolgozó aláírása", MyF.Oszlopnév(oszlop + 2) + "5");
                            MyX.Betű(munkalap, MyF.Oszlopnév(oszlop) + "5", beállB12V);
                            MyX.Betű(munkalap, MyF.Oszlopnév(oszlop + 1) + "5", beállB12V);
                            MyX.Betű(munkalap, MyF.Oszlopnév(oszlop + 2) + "5", beállB12V);
                            MyX.Betű(munkalap, MyF.Oszlopnév(oszlop) + "4", beállB12V);
                            MyX.Betű(munkalap, MyF.Oszlopnév(oszlop + 2) + "4", beállB12V);
                            Napszíne += "0";
                        }
                    }

                    MyX.SzövegIrány(munkalap, MyF.Oszlopnév(oszlop) + "5:" + MyF.Oszlopnév(oszlop + 2) + "5", 90);
                    MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(oszlop) + ":" + MyF.Oszlopnév(oszlop + 1), 6);
                    MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(oszlop + 2) + ":" + MyF.Oszlopnév(oszlop + 2), 10);

                    i += 1;
                    oszlop += 3;
                }
                while (i != 7);

                // beírjuk a neveket
                int hanyadikember = 0;
                int l = 0;
                while (hányember > hanyadikember)
                {
                    // megkeressük az első jelöltet
                    while (l < ChkDolgozónév.Items.Count)
                    {
                        if (ChkDolgozónév.GetItemChecked(l))
                        {
                            // hrazonosító
                            string[] darabol = ChkDolgozónév.Items[l].ToString().Split('=');
                            MyX.Kiir(darabol[1], "a" + (hanyadikember + 6).ToString());
                            // dolgozó név
                            MyX.Kiir(darabol[0], "b" + (hanyadikember + 6).ToString());
                            l++;
                            break;
                        }
                        l++;
                    }
                    Holtart.Lép();
                    hanyadikember += 1;
                }
                hanyadikember += 2;
                // sormagasság
                MyX.Sormagasság(munkalap, "6:" + (hanyadikember + 6).ToString(), 24);
                MyX.Egyesít(munkalap, "a" + (hanyadikember + 6).ToString() + ":b" + (hanyadikember + 6).ToString());
                MyX.Vastagkeret(munkalap, "a" + (hanyadikember + 6).ToString() + ":b" + (hanyadikember + 6).ToString());
                MyX.Kiir(" Az igazoló aláírása:\n" + LstKiadta.Text.Trim(), "a" + (hanyadikember + 6).ToString());
                i = 0;
                oszlop = 3;

                do
                {
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(oszlop) + (hanyadikember + 6).ToString() + ":" + MyF.Oszlopnév(oszlop + 2) + (hanyadikember + 6).ToString());
                    MyX.Vastagkeret(munkalap, MyF.Oszlopnév(oszlop) + (hanyadikember + 6).ToString() + ":" + MyF.Oszlopnév(oszlop + 2).ToString() + (hanyadikember + 6).ToString());
                    i += 1;
                    oszlop += 3;
                }
                while (i != 7);
                // formázunk
                // fejléc rácsozás
                MyX.Vastagkeret(munkalap, "a4:a5");
                MyX.Vastagkeret(munkalap, "b4:b5");
                MyX.Rácsoz(munkalap, $"A6:A{hanyadikember + 5}");
                MyX.Rácsoz(munkalap, $"B6:B{hanyadikember + 5}");
                oszlop = 3;
                for (int j = 0; j < 7; j++)
                {
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlop) + "4:" + MyF.Oszlopnév(oszlop + 2) + "5");
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlop) + "6:" + MyF.Oszlopnév(2 + oszlop) + (hanyadikember + 5).ToString());
                    MyX.Vastagkeret(munkalap, MyF.Oszlopnév(oszlop) + $"{hanyadikember + 6}:" + MyF.Oszlopnév(2 + 7 * 3) + (hanyadikember + 6).ToString());
                    oszlop += 3;
                }

                // igazoló rész
                MyX.Sormagasság(munkalap, (hanyadikember + 6).ToString() + ":" + (hanyadikember + 6).ToString(), 36);
                int sor = 9 + hanyadikember;
                MyX.Kiir(" Az igazoló aláírása", $"a{sor}");
                // színezés
                oszlop = 3;
                for (i = 0; i < 7; i++)
                {

                    if (MyF.Szöveg_Tisztítás(Napszíne, i, 1) == "1")
                        MyX.Háttérszín(munkalap, MyF.Oszlopnév(oszlop) + "4:" + MyF.Oszlopnév(oszlop + 2) + (hanyadikember + 6).ToString(), Color.Cyan);
                    oszlop += 3;
                }

                // kiirjuk a személyeket az ellenőrző személyeket
                Adat_Kiegészítő_főkönyvtábla Elem;
                if (RdBtnÜzemvezető.Checked)
                    Elem = (from a in AdatokFőkönyv
                            where a.Id == 2
                            select a).FirstOrDefault();
                else
                    Elem = (from a in AdatokFőkönyv
                            where a.Id == 3
                            select a).FirstOrDefault();

                if (Elem != null)
                {
                    sor += 2;
                    MyX.Betű(munkalap, $"e{sor}", beállB14);
                    MyX.Kiir(Elem.Név, $"e{sor}");
                    sor += 1;
                    MyX.Betű(munkalap, $"e{sor}", beállB14);
                    MyX.Kiir(Elem.Beosztás, $"e{sor}");
                    MyX.Igazít_függőleges(munkalap, "1:23", "alsó");
                    MyX.Oszlopszélesség(munkalap, "B:B");
                }

                Beállítás_Nyomtatás BeNyom = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"a1:w{sor}",
                    LapMagas = 1,
                    LapSzéles = 1,
                    Álló = false,
                    Papírméret = RdBtnA4.Checked ? "A4" : "A3",
                    BalMargó = 10,
                    JobbMargó = 10,
                    FelsőMargó = 15,
                    AlsóMargó = 15,
                    FejlécMéret = 13,
                    LáblécMéret = 13,
                    VízKözép = true
                };
                MyX.NyomtatásiTerület_részletes(munkalap, BeNyom);

                Holtart.Ki();

                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();

                List<string> Fájlok = new List<string> { fájlexc };
                if (RdBtnNyomtat.Checked) MyF.ExcelNyomtatás(Fájlok);

                if (RdBtnFájlTöröl.Checked) File.Delete(fájlexc);

                MessageBox.Show("A nyomtatvány elkészült.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        #region Ittaság vizsgálati
        private void Btn_Kiválogat_Click(object sender, EventArgs e)
        {
            Holtart.Be();
            // minden kijelölést töröl
            for (int i = 0; i < ChkDolgozónév.Items.Count; i++)
                ChkDolgozónév.SetItemChecked(i, false);

            Kiválogat_dolgozó();
            Holtart.Ki();
            MessageBox.Show("A dolgozók kijelölése elkészült.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Kiválogat_dolgozó()
        {
            try
            {
                List<Adat_Kiegészítő_Beosztáskódok> Beosztáskód = KézBeoKód.Lista_Adatok(Cmbtelephely.Text.Trim());
                Beosztáskód = (from a in Beosztáskód
                               where a.Számoló == true
                               orderby a.Beosztáskód
                               select a).ToList();

                List<Adat_Dolgozó_Beosztás_Új> Dolgbeoszt = KézBeosztásÚj.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value);
                Dolgbeoszt = (from a in Dolgbeoszt
                              where a.Nap.ToShortDateString() == Dátum.Value.ToShortDateString()
                              orderby a.Dolgozószám
                              select a).ToList();
                Holtart.Be();

                //ha ki van jelölve
                for (int i = 0; i < ChkDolgozónév.Items.Count; i++)
                {
                    string[] darabol = ChkDolgozónév.Items[i].ToString().Split('=');

                    string dolgozik = (from a in Dolgbeoszt
                                       where a.Dolgozószám.Trim() == darabol[1].Trim()
                                       select a.Beosztáskód).FirstOrDefault();
                    //Van beosztása, akkor megnézzük, hogy az olyan amit be akarunk jelölni.
                    if (dolgozik != null)
                    {
                        string biztosdolgozik = (from a in Beosztáskód
                                                 where dolgozik.Trim() == a.Beosztáskód.Trim()
                                                 select a.Beosztáskód).FirstOrDefault();
                        if (biztosdolgozik != null)
                            ChkDolgozónév.SetItemChecked(i, true);
                    }
                    Holtart.Lép();
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

        private void Btn_Ittasság_Click(object sender, EventArgs e)
        {
            if (Napi_ittas.Checked)
                Napi_ittassági_excel();
            else
                Heti_ittassági_excel();
        }

        private void Napi_ittassági_excel()
        {
            try
            {
                int hányember = ChkDolgozónév.CheckedItems.Count;
                if (hányember < 1) throw new HibásBevittAdat("Nincs kijelölve egy dolgozó sem.");

                Holtart.Be();
                string fájlexc = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Ittaság-vizsgálati_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddhhmmss}.xlsx";
                MyX.ExcelLétrehozás(munkalap);

                MyX.Munkalap_betű(munkalap, beállBetű);
                MyX.Oszlopszélesség(munkalap, "a:a", 10);
                MyX.Oszlopszélesség(munkalap, "b:b", 30);
                MyX.Oszlopszélesség(munkalap, "c:c", 18);
                MyX.Oszlopszélesség(munkalap, "d:d", 20);
                MyX.Egyesít(munkalap, "a1:d1");
                MyX.Egyesít(munkalap, "a2:d2");
                MyX.Egyesít(munkalap, "a3:d3");

                Adat_Kiegészítő_Jelenlétiív Eleme = (from a in AdatokJelenléti
                                                     where a.Id == 2
                                                     select a).FirstOrDefault();
                if (Eleme != null) MyX.Kiir(Eleme.Szervezet, "a1");
                Eleme = (from a in AdatokJelenléti
                         where a.Id == 3
                         select a).FirstOrDefault();
                if (Eleme != null) MyX.Kiir(Eleme.Szervezet, "a2");
                Eleme = (from a in AdatokJelenléti
                         where a.Id == 4
                         select a).FirstOrDefault();
                if (Eleme != null) MyX.Kiir(Eleme.Szervezet, "a3");


                MyX.Igazít_függőleges(munkalap, "A1:D3", "alsó");
                MyX.Igazít_vízszintes(munkalap, "A1:D3", "bal");
                MyX.Sormagasság(munkalap, "5:5", 32);
                MyX.Egyesít(munkalap, "a5:d5");
                MyX.Kiir("Ittasság-vizsgálati napló", "a5");
                MyX.Egyesít(munkalap, "a8:d8");
                MyX.Kiir("A vizsgálat időpontja (nap/óra)  ………………………………………………………", "a8");
                MyX.Igazít_vízszintes(munkalap, "A8", "bal");
                MyX.Egyesít(munkalap, "a10:d10");
                MyX.Kiir("A vizsgálatot végezte              ………………………………………………………", "a10");
                MyX.Igazít_vízszintes(munkalap, "A10", "bal");
                MyX.Egyesít(munkalap, "a12:d12");
                MyX.Kiir("Jelen volt                                 ……………………………………………………………", "a12");
                MyX.Igazít_vízszintes(munkalap, "A12", "bal");
                MyX.Igazít_függőleges(munkalap, "A8:D8", "alsó");
                MyX.Igazít_függőleges(munkalap, "A10:D10", "alsó");
                MyX.Igazít_függőleges(munkalap, "A12:D12", "alsó");
                MyX.Betű(munkalap, "a5", beállB20);

                // fejléc
                MyX.Kiir("Sorszám", "a14");
                MyX.Kiir("Vizsgált személy neve", "b14");
                MyX.Kiir("Vizsgálat \neredménye", "c14");
                MyX.Kiir("Megjegyzés \n(intézkedés)", "d14");
                MyX.Igazít_függőleges(munkalap, "A14:D14", "alsó");
                MyX.Betű(munkalap, "A14:D14", beállB12V);
                MyX.Igazít_függőleges(munkalap, "A14", "alsó");
                MyX.Sormagasság(munkalap, "14:14", 35);
                MyX.Sortörésseltöbbsorba(munkalap, "A14:D14");
                int sor = 14;
                int sorszám = 0;
                int hanyadikember = 0;
                int l = 0;

                foreach (string Elem in ChkDolgozónév.CheckedItems)
                {
                    l++;
                    sor++;
                    sorszám++;
                    // hrazonosító
                    string[] darabol = Elem.Split('=');
                    MyX.Kiir(l.ToString(), $"a{sor}");
                    // dolgozó név
                    MyX.Kiir(darabol[0], $"b{sor}");
                    MyX.Sormagasság(munkalap, sor + ":" + sor, 25);

                    hanyadikember += 1;
                    Holtart.Lép();
                }

                hanyadikember += 2;
                MyX.Rácsoz(munkalap, $"a14:d14");
                MyX.Rácsoz(munkalap, $"a15:d{sor}");

                Beállítás_Nyomtatás BeNyom = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"a1:w{hanyadikember + 10}",
                    LapSzéles = 1,
                    Papírméret = RdBtnA4.Checked ? "A4" : "A3",
                    BalMargó = 10,
                    JobbMargó = 10,
                    FelsőMargó = 15,
                    AlsóMargó = 15,
                    FejlécMéret = 13,
                    LáblécMéret = 13,
                    VízKözép = true,
                    IsmétlődőSorok = "$1:$14",
                    FejlécJobb = "&P/&N",
                    LáblécBal = $"Budapest, {Dátum.Value:yyyy.MM.dd}",
                    LáblécJobb = "..........................................\nVizsgálatot végző aláírása"

                };
                MyX.NyomtatásiTerület_részletes(munkalap, BeNyom);

                Holtart.Ki();
                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();

                List<string> Fájlok = new List<string> { fájlexc };
                if (RdBtnNyomtat.Checked) MyF.ExcelNyomtatás(Fájlok);
                if (RdBtnFájlTöröl.Checked) File.Delete(fájlexc);

                MessageBox.Show("A nyomtatvány elkészült.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Heti_ittassági_excel()
        {
            try
            {

                int hányember = ChkDolgozónév.CheckedItems.Count;
                if (hányember < 1) throw new HibásBevittAdat("Nincs kijelölve egy dolgozó sem.");

                Holtart.Be();
                string fájlexc = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Ittassági_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddhhmmss}.xlsx";
                MyX.ExcelLétrehozás(munkalap);
                Beállítás_Betű beállBetű = new Beállítás_Betű { Méret = 14 };
                MyX.Munkalap_betű(munkalap, beállBetű);
                MyX.Betű(munkalap, "a1", beállB14V);


                Adat_Kiegészítő_Jelenlétiív Eleme = (from a in AdatokJelenléti
                                                     where a.Id == 2
                                                     select a).FirstOrDefault();
                if (Eleme != null) MyX.Kiir(Eleme.Szervezet, "a1");
                Eleme = (from a in AdatokJelenléti
                         where a.Id == 3
                         select a).FirstOrDefault();
                if (Eleme != null) MyX.Kiir(Eleme.Szervezet, "a2");
                Eleme = (from a in AdatokJelenléti
                         where a.Id == 4
                         select a).FirstOrDefault();
                if (Eleme != null) MyX.Kiir(Eleme.Szervezet, "a3");

                MyX.Oszlopszélesség(munkalap, "a:a", 20);
                MyX.Oszlopszélesség(munkalap, "b:b", 35);
                MyX.Egyesít(munkalap, "a4:l4");
                MyX.Betű(munkalap, "a4", beállB20V);

                MyX.Kiir("Ittasság-vizsgálati napló", "a4");
                int mennyi = 5;
                DateTime elsőnap = MyF.Hét_elsőnapja(Dátum.Value);
                DateTime utolsónap = MyF.Hét_Utolsónapja(Dátum.Value);
                MyX.Kiir("HR azonosító", "a9");
                MyX.Kiir("A munkavállaló neve", "b9");
                MyX.Egyesít(munkalap, "a8:b8");
                MyX.Kiir("A vizsgálat időpontja (Óra:perc)", "a8");

                // napok fejlécet létrehozzuk
                string szöveg = $"{elsőnap:yyyy. év MMMM dd}.-tól - ";
                szöveg += $"{utolsónap:yyyy. év MMMM dd}.-ig ";
                MyX.Kiir(szöveg, "a5");

                // sormagasság
                MyX.Sormagasság(munkalap, "7:7", 30);
                MyX.Sormagasság(munkalap, "8:8", 30);
                MyX.Sormagasság(munkalap, "9:9", 37);
                int oszlop = 3;
                for (int i = 0; i < mennyi; i++)
                {
                    MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(oszlop) + ":" + MyF.Oszlopnév(oszlop), 20);
                    MyX.Kiir($"{elsőnap.AddDays(i):dddd}", $"{MyF.Oszlopnév(oszlop)}7");
                    MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(oszlop + 1) + ":" + MyF.Oszlopnév(oszlop + 1), 20);
                    MyX.Kiir($"{elsőnap.AddDays(i):dd}", $"{MyF.Oszlopnév(oszlop + 1)}7");
                    MyX.Kiir("Vizsgálati eredmény", MyF.Oszlopnév(oszlop) + "9");
                    MyX.Kiir("Megjegyzés (intézkedés)", MyF.Oszlopnév(oszlop + 1) + "9");
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(oszlop) + "8" + ":" + MyF.Oszlopnév(oszlop + 1) + "8");
                    oszlop += 2;
                }
                MyX.Igazít_függőleges(munkalap, "7:7", "alsó");
                MyX.Sortörésseltöbbsorba(munkalap, "7:7");
                MyX.Igazít_függőleges(munkalap, "9:9", "alsó");
                MyX.Sortörésseltöbbsorba(munkalap, "9:9");
                int hanyadikember = 0;


                foreach (string Elem in ChkDolgozónév.CheckedItems)
                {
                    // hrazonosító
                    string[] darabol = Elem.Split('=');
                    MyX.Kiir(darabol[1], "a" + (hanyadikember + 10).ToString());
                    // dolgozó név
                    MyX.Kiir(darabol[0], "b" + (hanyadikember + 10).ToString());
                    hanyadikember++;
                    Holtart.Lép();
                }

                hanyadikember += 2;
                // sormagasság
                MyX.Sormagasság(munkalap, $"10:{hanyadikember + 10}", 24);
                MyX.Sormagasság(munkalap, $"{hanyadikember + 10}:{hanyadikember + 13}", 35);
                MyX.Egyesít(munkalap, $"a{hanyadikember + 10}:b{hanyadikember + 10}");
                MyX.Kiir("Vizsgálatot végezte", "a" + (hanyadikember + 10).ToString());

                hanyadikember += 1;
                MyX.Egyesít(munkalap, "a" + (hanyadikember + 10).ToString() + ":b" + (hanyadikember + 10).ToString());
                MyX.Kiir("Vizsgáltot végző aláírása", "a" + (hanyadikember + 10).ToString());

                hanyadikember += 1;
                MyX.Egyesít(munkalap, "a" + (hanyadikember + 10).ToString() + ":b" + (hanyadikember + 10).ToString());
                MyX.Kiir("Jelen volt", "a" + (hanyadikember + 10).ToString());

                oszlop = 3;
                for (int i = 0; i < mennyi; i++)
                    oszlop += 2;

                // formázunk
                // rácsozás
                MyX.Rácsoz(munkalap, "a7:" + MyF.Oszlopnév(oszlop - 1) + (hanyadikember + 10).ToString());
                MyX.Oszlopszélesség(munkalap, "B:B");
                oszlop = 2 + mennyi * 2;

                // rácsozunk naponta
                for (int i = 1; i < oszlop; i += 2)
                {
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(i) + (hanyadikember + 10).ToString() + ":" + MyF.Oszlopnév(i + 1) + (hanyadikember + 10).ToString());
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(i) + (hanyadikember + 9).ToString() + ":" + MyF.Oszlopnév(i + 1) + (hanyadikember + 9).ToString());
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(i) + (hanyadikember + 8).ToString() + ":" + MyF.Oszlopnév(i + 1) + (hanyadikember + 8).ToString());
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(i) + "7:" + MyF.Oszlopnév(i + 1) + (hanyadikember + 10).ToString());
                }
                MyX.Igazít_függőleges(munkalap, $"A{7}:P{hanyadikember + 10}", "alsó");
                MyX.Igazít_vízszintes(munkalap, $"A{7}:P{hanyadikember + 10}", "közép");
                MyX.Igazít_vízszintes(munkalap, "A1:A3", "bal");
                MyX.Igazít_vízszintes(munkalap, "A5", "bal");

                Beállítás_Nyomtatás BeNyom = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"a1:w{hanyadikember + 10}",
                    LapMagas = 1,
                    LapSzéles = 1,
                    Álló = false,
                    Papírméret = RdBtnA4.Checked ? "A4" : "A3",
                    BalMargó = 10,
                    JobbMargó = 10,
                    FelsőMargó = 15,
                    AlsóMargó = 15,
                    FejlécMéret = 13,
                    LáblécMéret = 13,
                    VízKözép = true
                };
                MyX.NyomtatásiTerület_részletes(munkalap, BeNyom);

                Holtart.Ki();

                MyX.ExcelMentés(fájlexc);
                List<string> Fájlok = new List<string> { fájlexc };
                if (RdBtnNyomtat.Checked) MyF.ExcelNyomtatás(Fájlok);
                MyX.ExcelBezárás();

                if (RdBtnFájlTöröl.Checked) File.Delete(fájlexc);
                MessageBox.Show("A nyomtatvány elkészült.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Cmbtelephely_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                Cmbtelephely.Text = Cmbtelephely.Items[Cmbtelephely.SelectedIndex].ToStrTrim();
                if (Cmbtelephely.Text.Trim() == "") return;
                if (Program.PostásJogkör.Any(c => c != '0'))
                {

                }
                else
                {
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
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
    }
}