using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    internal partial class Ablak_Jelenléti
    {
        public Ablak_Jelenléti()
        {
            InitializeComponent();
        }

        readonly Kezelő_Kiegészítő_Jelenlétiív KézJelenléti = new Kezelő_Kiegészítő_Jelenlétiív();
        readonly Kezelő_Kiegészítő_főkönyvtábla KézFőkönyv = new Kezelő_Kiegészítő_főkönyvtábla();

        List<Adat_Kiegészítő_Jelenlétiív> AdatokJelenléti = new List<Adat_Kiegészítő_Jelenlétiív>();
        List<Adat_Kiegészítő_főkönyvtábla> AdatokFőkönyv = new List<Adat_Kiegészítő_főkönyvtábla>();

        #region Alap

        private void Btn_Súgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\jelenléti.html";
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

        private void AblakJelenléti_Load(object sender, EventArgs e)
        {

            Telephelyekfeltöltése();
            Csoportfeltöltés();
            Névfeltöltés();
            Irányítófeltöltés();

            JelenlétiListaFeltöltés();
            AláíróListaFeltöltés();
            Aláíróbetöltés();
        }


        private void Cmbtelephely_SelectedIndexChanged(object sender, EventArgs e)
        {

            Csoportfeltöltés();
            Névfeltöltés();
            Irányítófeltöltés();

            JelenlétiListaFeltöltés();
            AláíróListaFeltöltés();
            Aláíróbetöltés();
        }


        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                Cmbtelephely.Items.AddRange(Listák.TelephelyLista_Személy(true));
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
        #endregion



        #region Listák


        private void Csoportfeltöltés()
        {
            ChkCsoport.Items.Clear();
            ChkCsoport.BeginUpdate();
            string hely = $@"{Application.StartupPath}\" + Cmbtelephely.Text + @"\Adatok\Segéd\kiegészítő.mdb";
            string jelszó = "Mocó";
            string szöveg = "SELECT * FROM csoportbeosztás order by sorszám";
            Kezelő_Kiegészítő_Csoportbeosztás kéz = new Kezelő_Kiegészítő_Csoportbeosztás();
            List<Adat_Kiegészítő_Csoportbeosztás> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);
            foreach (Adat_Kiegészítő_Csoportbeosztás rekord in Adatok)
                ChkCsoport.Items.Add(rekord.Csoportbeosztás);

            ChkCsoport.EndUpdate();
        }


        void Névfeltöltés()
        {
            try
            {
                ChkDolgozónév.Items.Clear();
                ChkDolgozónév.BeginUpdate();
                string hely = $@"{Application.StartupPath}\" + Cmbtelephely.Text + @"\Adatok\Dolgozók.mdb";
                string jelszó = "forgalmiutasítás";
                string szöveg = "SELECT * FROM Dolgozóadatok WHERE  kilépésiidő=#1/1/1900#  order by DolgozóNév asc";

                Kezelő_Dolgozó_Alap kéz = new Kezelő_Dolgozó_Alap();
                List<Adat_Dolgozó_Alap> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);

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
            string hely = $@"{Application.StartupPath}\" + Cmbtelephely.Text + @"\Adatok\Dolgozók.mdb";
            string jelszó = "forgalmiutasítás";
            string szöveg = "SELECT * FROM Dolgozóadatok where kilépésiidő=#1/1/1900# and (főkönyvtitulus<>'' and főkönyvtitulus<>'_')  order by DolgozóNév asc";

            Kezelő_Dolgozó_Alap kéz = new Kezelő_Dolgozó_Alap();
            List<Adat_Dolgozó_Alap> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);

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
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\kiegészítő.mdb";
                string jelszó = "Mocó";
                string szöveg = "SELECT * FROM főkönyvtábla ";
                AdatokFőkönyv = KézFőkönyv.Lista_Adatok(hely, jelszó, szöveg);
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
                string hely = $@"{Application.StartupPath}\" + Cmbtelephely.Text + @"\Adatok\Dolgozók.mdb";
                string jelszó = "forgalmiutasítás";

                Kezelő_Dolgozó_Alap kéz = new Kezelő_Dolgozó_Alap();
                List<Adat_Dolgozó_Alap> Adatok;

                foreach (string Elem in ChkCsoport.CheckedItems)
                {
                    //csoporttagokat kiválogatja
                    string szöveg = $"SELECT * FROM Dolgozóadatok where [csoport]='{Elem}' order by DolgozóNév";
                    Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);

                    foreach (Adat_Dolgozó_Alap rekord in Adatok)
                    {
                        if (rekord.Kilépésiidő == new DateTime(1900, 1, 1))
                            ChkDolgozónév.Items.Add(rekord.DolgozóNév.Trim() + " = " + rekord.Dolgozószám.Trim());
                    }
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


        private void Btn_Heti_Click(object sender, EventArgs e)
        {
            try
            {
                string munkalap = "Munka1";
                int hányember = ChkDolgozónév.CheckedItems.Count;
                if (hányember < 1)
                    throw new HibásBevittAdat("Nincs kijelölve egy dolgozó sem.");

                Holtart.Be();

                string fájlexc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + "Jelenléti_" + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx";
                MyE.ExcelLétrehozás();

                MyE.Munkalap_betű("arial", 12);
                MyE.Betű("a1", 14);
                MyE.Betű("a1", false, false, true);

                Adat_Kiegészítő_Jelenlétiív Eleme = (from a in AdatokJelenléti
                                                     where a.Id == 1
                                                     select a).FirstOrDefault();
                if (Eleme != null) MyE.Kiir(Eleme.Szervezet, "a2");

                MyE.Kiir("1. oldal", "a1");
                MyE.Oszlopszélesség(munkalap, "a:a", 13);
                MyE.Oszlopszélesség(munkalap, "b:b", 25);
                MyE.Betű("k1", 16);
                MyE.Betű("k1", false, false, true);
                MyE.Kiir("JELENLÉTI ÍV", "k1");
                MyE.Betű("2:2", 12);
                MyE.Kiir("HR azonosító", "a5");
                int mennyi = 0;
                DateTime elsőnap = MyF.Hét_elsőnapja(Dátum.Value);
                DateTime utolsónap = MyF.Hét_Utolsónapja(Dátum.Value);
                MyE.Kiir("A munkavállaló neve", "b5");
                MyE.Betű("b:b", 14);
                MyE.Betű("a:a", 14);
                MyE.Betű("a5", 12);
                MyE.Betű("4:4", 12);
                if (RdBtn5Napos.Checked)
                    mennyi = 5;
                if (RdBtn6Napos.Checked)
                    mennyi = 6;
                if (RdBtn7Napos.Checked)
                    mennyi = 7;

                // napok fejlécet létrehozzuk
                string szöveg = $"{elsőnap:yyyy. év MMMM dd}.-tól - ";
                szöveg += $"{utolsónap:yyyy. év MMMM dd}.-ig ";
                MyE.Kiir(szöveg, "k3");

                // sormagasság
                MyE.Sormagasság("5:5", 120);
                int oszlop = 3;
                for (int j = 0; j < mennyi; j++)
                {
                    MyE.Kiir($"{elsőnap.AddDays(j):dddd}", $"{MyE.Oszlopnév(oszlop)}4");
                    MyE.Sortörésseltöbbsorba_egyesített(MyE.Oszlopnév(oszlop) + "4:" + MyE.Oszlopnév(oszlop + 1) + "4");
                    if (!Éjszakás.Checked)
                    {
                        MyE.Kiir($"{elsőnap.AddDays(j):dd}", $"{MyE.Oszlopnév(oszlop + 2)}4");
                        MyE.Kiir("Érkezés ideje", MyE.Oszlopnév(oszlop) + "5");
                        MyE.Kiir("Távozás ideje", MyE.Oszlopnév(oszlop + 1) + "5");
                    }
                    else
                    {
                        MyE.Kiir($"Érkezés ideje {elsőnap.AddDays(j):MM.dd.}", $"{MyE.Oszlopnév(oszlop)}5");
                        MyE.Kiir($"Távozás ideje {elsőnap.AddDays(j + 1):MM.dd.}", $"{MyE.Oszlopnév(oszlop + 1)}5");
                    }

                    MyE.Kiir("A dolgozó aláírása", MyE.Oszlopnév(oszlop + 2) + "5");
                    MyE.SzövegIrány(munkalap, MyE.Oszlopnév(oszlop) + "5:" + MyE.Oszlopnév(oszlop + 2) + "5", 90);
                    MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(oszlop) + ":" + MyE.Oszlopnév(oszlop + 1), 6);
                    MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(oszlop + 2) + ":" + MyE.Oszlopnév(oszlop + 2), 10);

                    oszlop += 3;
                }
                // beírjuk a neveket
                int hanyadikember = 0;
                int l = 0;
                foreach (string Elemei in ChkDolgozónév.CheckedItems)
                {
                    // hrazonosító
                    string[] darabol = Elemei.Split('=');
                    MyE.Kiir(darabol[1], "a" + (hanyadikember + 6).ToString());
                    // dolgozó név
                    MyE.Kiir(darabol[0], "b" + (hanyadikember + 6).ToString());
                    l++;
                    hanyadikember += 1;
                    Holtart.Lép();
                }

                hanyadikember += 2;
                // sormagasság
                MyE.Sormagasság("6:" + (hanyadikember + 6).ToString(), 24);
                MyE.Egyesít(munkalap, "a" + (hanyadikember + 6).ToString() + ":b" + (hanyadikember + 6).ToString());
                MyE.Kiir(" Az igazoló aláírása:\n" + LstKiadta.Text.Trim(), "a" + (hanyadikember + 6).ToString());
                oszlop = 3;
                for (int ii = 0; ii < mennyi; ii++)
                {
                    MyE.Egyesít(munkalap, MyE.Oszlopnév(oszlop) + (hanyadikember + 6).ToString() + ":" + MyE.Oszlopnév(oszlop + 2) + (hanyadikember + 6).ToString());
                    oszlop += 3;
                }
                // formázunk
                // fejléc rácsozás
                Holtart.Value = 1;
                MyE.Vastagkeret("a4:a5");
                MyE.Vastagkeret("b4:b5");
                oszlop = 3;
                MyE.Rácsoz(MyE.Oszlopnév(oszlop) + "4:" + MyE.Oszlopnév((oszlop + 3 * mennyi) - 1) + "5");

                for (int i = 0; i < mennyi; i++)
                {
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlop) + "4:" + MyE.Oszlopnév(oszlop + 2) + "5");
                    oszlop += 3;
                }

                // középsőrész
                Holtart.Value = 2;
                MyE.Rácsoz("a6:" + MyE.Oszlopnév(2 + mennyi * 3) + (hanyadikember + 5).ToString());
                MyE.Vastagkeret("A6:A" + (hanyadikember + 5).ToString());
                MyE.Vastagkeret("B6:B" + (hanyadikember + 5).ToString());
                for (int i = 0; i < mennyi; i++)
                {
                    MyE.Vastagkeret("A6:A" + (hanyadikember + 5).ToString());
                    MyE.Vastagkeret("B6:B" + (hanyadikember + 5).ToString());
                    oszlop += 3;
                }

                // napok rácsozása
                Holtart.Value = 3;
                oszlop = 3;
                for (int i = 0; i < mennyi; i++)
                {
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlop) + "6:" + MyE.Oszlopnév(oszlop + 2) + (hanyadikember + 5).ToString());
                    oszlop += 3;
                }

                // igazoló rész
                Holtart.Value = 4;
                MyE.Sormagasság((hanyadikember + 6).ToString() + ":" + (hanyadikember + 6).ToString(), 36);
                int vege = 2 + mennyi * 3;
                MyE.Vastagkeret(MyE.Oszlopnév(1) + (hanyadikember + 6).ToString() + ":" + MyE.Oszlopnév(2).ToString() + (hanyadikember + 6).ToString());
                for (int i = 3; i < vege; i += 3)
                {
                    MyE.Vastagkeret(MyE.Oszlopnév(i) + (hanyadikember + 6).ToString() + ":" + MyE.Oszlopnév(i + 2).ToString() + (hanyadikember + 6).ToString());
                    oszlop += 3;
                }
                MyE.sor = 9 + hanyadikember;
                MyE.Kiir(" Az igazoló aláírása:", "a" + MyE.sor.ToString());

                // kiirjuk a személyeket az ellenőrző személyeket
                Holtart.Value = 5;

                Adat_Kiegészítő_főkönyvtábla Elem;
                if (RdBtnÜzemvezető.Checked)
                    Elem = (from a in AdatokFőkönyv
                            where a.Id == 2
                            select a).FirstOrDefault();
                else
                    Elem = (from a in AdatokFőkönyv
                            where a.Id == 3
                            select a).FirstOrDefault();

                MyE.sor += 2;
                MyE.Betű("e" + MyE.sor.ToString(), 14);

                if (Elem != null)
                {
                    MyE.Kiir(Elem.Név, "e" + MyE.sor.ToString());
                    MyE.sor += 1;
                    MyE.Betű("e" + MyE.sor.ToString(), 14);
                    MyE.Kiir(Elem.Beosztás, "e" + MyE.sor.ToString());
                }
                MyE.Oszlopszélesség(munkalap, "B:B");
                // **********************************************
                // **Nyomtatási beállítások                    **
                // **********************************************
                Holtart.Value = 6;
                MyE.NyomtatásiTerület_részletes(munkalap, "a1:" + MyE.Oszlopnév(2 + mennyi * 3) + (hanyadikember + 13).ToString(),
                    0.393700787401575d, 0.393700787401575, 0.590551181102362d, 0.590551181102362d, 0.511811023622047d, 0.511811023622047d,
                    "1", "1", false, RdBtnA4.Checked == true ? "A4" : "A3", true, false);

                // **********************************************
                // **Nyomtatás                                 **
                // **********************************************
                if (RdBtnNyomtat.Checked) MyE.Nyomtatás(munkalap, 1, 1);

                Holtart.Visible = false;
                MyE.Aktív_Cella(munkalap, "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();

                if (RdBtnFájlTöröl.Checked)
                    File.Delete(fájlexc);

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
                string munkalap = "Munka1";
                int hányember = ChkDolgozónév.CheckedItems.Count;
                if (hányember < 1)
                    throw new HibásBevittAdat("Nincs kijelölve egy dolgozó sem.");

                Holtart.Be();
                string fájlexc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + "Jelenléti_Szellemi_"
                    + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx";

                MyE.ExcelLétrehozás();

                // minden cella betűméret
                MyE.Munkalap_betű("calibri", 11);
                MyE.Kiir("1. oldal", "a6");
                MyE.Kiir("Szellemi állomány", "a2");
                MyE.Oszlopszélesség(munkalap, "a:a", 7);
                MyE.Oszlopszélesség(munkalap, "b:b", 25);
                MyE.Oszlopszélesség(munkalap, "c:w", 8);
                MyE.Betű("a1", 14);
                MyE.Betű("a1", false, false, true);


                Adat_Kiegészítő_Jelenlétiív Eleme = (from a in AdatokJelenléti
                                                     where a.Id == 1
                                                     select a).FirstOrDefault();
                if (Eleme != null) MyE.Kiir(Eleme.Szervezet, "a1");

                DateTime elsőnap = MyF.Hét_elsőnapja(Dátum.Value);
                DateTime utolsónap = MyF.Hét_Utolsónapja(Dátum.Value);

                MyE.Betű("2:2", 12);
                MyE.Betű("j3", 14);
                MyE.Betű("j3", false, false, true);
                MyE.Kiir("Jelenléti ív", "j3");
                MyE.Betű("m5", 14);
                MyE.Betű("m5", false, false, true);
                // napok fejlécet létrehozzuk
                string szöveg = $"{elsőnap:yyyy. év MMMM dd}.-tól - ";
                szöveg += $"{utolsónap:yyyy. év MMMM dd}.-ig ";
                MyE.Kiir(szöveg, "m5");
                int oszlop = 3;
                for (int i = 0; i < 7; i++)
                {
                    MyE.Egyesít(munkalap, MyE.Oszlopnév(oszlop) + "7:" + MyE.Oszlopnév(oszlop + 1) + "7");
                    MyE.Kiir($"{elsőnap.AddDays(i):dddd}", $"{MyE.Oszlopnév(oszlop)}7");
                    MyE.Betű(MyE.Oszlopnév(oszlop) + "7");
                    MyE.Kiir($"{elsőnap.AddDays(i):dd}", $"{MyE.Oszlopnév(oszlop + 2)}7");
                    MyE.Betű(MyE.Oszlopnév(oszlop + 2) + "7");
                    MyE.Kiir("Érkezés ideje", MyE.Oszlopnév(oszlop) + "8");
                    MyE.Kiir("Távozás ideje", MyE.Oszlopnév(oszlop + 1) + "8");
                    MyE.Kiir("A dolgozó aláírása", MyE.Oszlopnév(oszlop + 2) + "8");
                    oszlop += 3;
                }

                MyE.Sormagasság("7:7", 31);
                MyE.Sormagasság("8:8", 90);
                MyE.Kiir("Hr azonosító", "a8");
                MyE.Kiir("Dolgozó neve", "b8");
                MyE.SzövegIrány(munkalap, "A8", 90);
                MyE.SzövegIrány(munkalap, "c8:W8", 90);
                MyE.Betű("7:7", false, false, true);
                MyE.Igazít_függőleges("8:7", "alsó");

                int hanyadikember = 0;
                int l = 0;
                foreach (string Ele in ChkDolgozónév.CheckedItems)
                {
                    // hrazonosító
                    string[] darabol = Ele.Split('=');
                    MyE.Kiir(darabol[1], "a" + (hanyadikember + 9).ToString());
                    // dolgozó név
                    MyE.Kiir(darabol[0], "b" + (hanyadikember + 9).ToString());
                    l++;
                    hanyadikember += 1;
                    Holtart.Lép();
                }
                hanyadikember += 3;

                MyE.Egyesít(munkalap, "a" + (hanyadikember + 8).ToString() + ":b" + (hanyadikember + 8).ToString());
                MyE.Kiir("Igazolta:" + LstKiadta.Text.Trim(), "a" + (hanyadikember + 8).ToString());

                // formázunk
                // fejléc rácsozás
                MyE.Rácsoz("a7:W8");
                MyE.Vastagkeret("a7:b8");
                MyE.Rácsoz("a9:" + MyE.Oszlopnév(2 + 7 * 3) + (hanyadikember + 7).ToString());
                MyE.Vastagkeret("a7:b" + (hanyadikember + 7).ToString());
                MyE.Egyesít(munkalap, "a" + (hanyadikember + 8).ToString() + ":b" + (hanyadikember + 8).ToString());
                MyE.Vastagkeret("a" + (hanyadikember + 8).ToString() + ":b" + (hanyadikember + 8).ToString());

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
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlop) + "7:" + MyE.Oszlopnév(oszlop + 2) + "8");
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlop) + "9:" + MyE.Oszlopnév(oszlop + 2) + (hanyadikember + 7).ToString());
                    MyE.Egyesít(munkalap, MyE.Oszlopnév(oszlop) + (hanyadikember + 8).ToString() + ":" + MyE.Oszlopnév(oszlop + 2) + (hanyadikember + 8).ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlop) + (hanyadikember + 8).ToString() + ":" + MyE.Oszlopnév(oszlop + 2) + (hanyadikember + 8).ToString());
                    if (MyF.Szöveg_Tisztítás(csekkoló, i, 1) == "0")
                    {
                        MyE.Rácsoz(MyE.Oszlopnév(oszlop) + "9:" + MyE.Oszlopnév(oszlop + 2) + (hanyadikember + 7).ToString());
                        MyE.Vastagkeret(MyE.Oszlopnév(oszlop) + "9:" + MyE.Oszlopnév(oszlop + 2) + (hanyadikember + 7).ToString());
                    }
                    else
                    {
                        MyE.Egyesít(munkalap, MyE.Oszlopnév(oszlop) + "9:" + MyE.Oszlopnév(oszlop + 2) + (hanyadikember + 7).ToString());
                        MyE.Kiir("Pihenőnap", MyE.Oszlopnév(oszlop) + "9:" + MyE.Oszlopnév(oszlop + 2) + (hanyadikember + 7).ToString());
                        MyE.FerdeVonal(MyE.Oszlopnév(oszlop) + "9:" + MyE.Oszlopnév(oszlop + 2) + (hanyadikember + 7).ToString());
                        MyE.Vastagkeret(MyE.Oszlopnév(oszlop) + "9:" + MyE.Oszlopnév(oszlop + 2) + (hanyadikember + 7).ToString());
                    }

                    oszlop += 3;
                }
                MyE.Sormagasság("9:" + (hanyadikember + 8).ToString(), 20);
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
                    MyE.Kiir(" Az igazoló aláírása:", "a" + sor.ToString());
                    MyE.Betű("e" + sor.ToString(), 14);
                    MyE.Kiir(Elem.Név, "e" + sor.ToString());
                    sor += 1;
                    MyE.Betű("e" + sor.ToString(), 14);
                    MyE.Kiir(Elem.Beosztás, "e" + sor.ToString());
                }
                MyE.Oszlopszélesség(munkalap, "B:B");
                // **********************************************
                // **Nyomtatási beállítások                    **
                // **********************************************
                MyE.NyomtatásiTerület_részletes(munkalap, "a1:w" + sor.ToString(),
                    0.393700787401575d, 0.393700787401575, 0.590551181102362d, 0.590551181102362d, 0.511811023622047d, 0.511811023622047d,
                    "1", "1", false, RdBtnA4.Checked == true ? "A4" : "A3", true, false);
                // **********************************************
                // **Nyomtatás                                 **
                // **********************************************
                if (RdBtnNyomtat.Checked)
                    MyE.Nyomtatás(munkalap, 1, 1);

                Holtart.Ki();
                MyE.Aktív_Cella(munkalap, "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();
                if (RdBtnFájlTöröl.Checked)
                    File.Delete(fájlexc);

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
                string munkalap = "Munka1";

                int hányember = ChkDolgozónév.CheckedItems.Count;
                if (hányember < 1)
                    throw new HibásBevittAdat("Nincs kijelölve egy dolgozó sem.");

                Holtart.Be();

                int i = 0;
                string fájlexc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + "Jelenléti_Váltó_" + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx";

                MyE.ExcelLétrehozás();

                MyE.Munkalap_betű("arial", 12);
                MyE.Kiir("1. oldal", "a1");
                MyE.Oszlopszélesség(munkalap, "a:a", 13);
                MyE.Oszlopszélesség(munkalap, "b:b", 25);
                MyE.Betű("k1", 16);
                MyE.Betű("k1", false, false, true);
                MyE.Kiir("JELENLÉTI ÍV", "k1");
                MyE.Betű("2:2", 12);

                Adat_Kiegészítő_Jelenlétiív Eleme = (from a in AdatokJelenléti
                                                     where a.Id == 1
                                                     select a).FirstOrDefault();
                if (Eleme != null) MyE.Kiir(Eleme.Szervezet, "a2");

                DateTime elsőnap = MyF.Hét_elsőnapja(Dátum.Value.ToString("yyyy.MM.dd").ToÉrt_DaTeTime());
                DateTime utolsónap = MyF.Hét_Utolsónapja(Dátum.Value.ToString("yyyy.MM.dd").ToÉrt_DaTeTime());

                MyE.Kiir("Hr azonosító", "a5");
                MyE.Kiir("A munkavállaló neve", "b5");
                MyE.Betű("b:b", 14);
                MyE.Betű("a:a", 12);
                MyE.Betű("4:4", 12);

                // napok fejlécet létrehozzuk
                string szöveg = $"{elsőnap:yyyy. év MMMM dd}.-tól - ";
                szöveg += $"{utolsónap:yyyy. év MMMM dd}.-ig ";
                MyE.Kiir(szöveg, "l2");

                // sormagasság
                MyE.Sormagasság("5:5", 120);
                //int i = 0;
                int oszlop = 3;
                string helyváltó = Application.StartupPath + @"\Főmérnökség\Adatok\kiegészítő2.mdb";
                string jelszó = "Mocó";
                int ciklusnap;

                // Ha dolgozik és éjszakás
                // kiirjuk a váltós munkarendeket

                // ha nincs kijelölve váltós akkor nem írja ki
                Kezelő_Kiegészítő_Váltóstábla kéz = new Kezelő_Kiegészítő_Váltóstábla();
                Kezelő_Kiegészítő_Beosztásciklus KézBeo = new Kezelő_Kiegészítő_Beosztásciklus();

                szöveg = "SELECT * FROM váltósbeosztás order by id";
                List<Adat_Kiegészítő_Váltóstábla> Adatok = kéz.Lista_Adatok(helyváltó, jelszó, szöveg);

                szöveg = "SELECT * FROM beosztásciklus  order by  id";
                List<Adat_Kiegészítő_Beosztásciklus> AdatokBeo = KézBeo.Lista_Adatok(helyváltó, jelszó, szöveg);

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
                    MyE.Kiir($"{elsőnap.AddDays(i):dddd}", $"{MyE.Oszlopnév(oszlop)}4");

                    MyE.Egyesít(munkalap, MyE.Oszlopnév(oszlop) + "4:" + MyE.Oszlopnév(oszlop + 1) + "4");

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
                            MyE.Kiir($"{elsőnap.AddDays(i):dd}", $"{MyE.Oszlopnév(oszlop + 2)}4");
                            MyE.Kiir("Érkezés ideje", MyE.Oszlopnév(oszlop) + "5");
                            MyE.Kiir("Távozás ideje", MyE.Oszlopnév(oszlop + 1) + "5");
                            MyE.Kiir("A dolgozó aláírása", MyE.Oszlopnév(oszlop + 2) + "5");
                            MyE.Háttérszín(MyE.Oszlopnév(oszlop) + "4:" + MyE.Oszlopnév(oszlop + 2) + "5", 65535L);
                            Napszíne += "1";
                        }
                        if (beosztáskód == "7")
                        {
                            // nappalos
                            MyE.Kiir($"{elsőnap.AddDays(i):dd}", $"{MyE.Oszlopnév(oszlop + 2)}4");
                            MyE.Kiir("A dolgozó aláírása", MyE.Oszlopnév(oszlop + 2) + "5");
                            MyE.Kiir("Érkezés ideje", MyE.Oszlopnév(oszlop) + "5");
                            MyE.Kiir("Távozás ideje", MyE.Oszlopnév(oszlop + 1) + "5");
                            MyE.Betű(MyE.Oszlopnév(oszlop) + "5", false, true, false);
                            MyE.Betű(MyE.Oszlopnév(oszlop + 1) + "5", false, true, false);
                            MyE.Betű(MyE.Oszlopnév(oszlop + 2) + "5", false, true, false);
                            MyE.Betű(MyE.Oszlopnév(oszlop) + "4", false, true, false);
                            MyE.Betű(MyE.Oszlopnév(oszlop + 2) + "4", false, true, false);
                            Napszíne += "0";
                        }
                        if (beosztáskód == "8")
                        {
                            // éjszakás
                            MyE.Kiir("ÉJ", MyE.Oszlopnév(oszlop + 2) + "4");
                            MyE.Kiir($"Érkezés ideje {elsőnap.AddDays(i):MM.dd.}", $"{MyE.Oszlopnév(oszlop)}5");
                            MyE.Kiir($"Távozás ideje {elsőnap.AddDays(i + 1):MM.dd.}", $"{MyE.Oszlopnév(oszlop + 1)}5");
                            MyE.Kiir("A dolgozó aláírása", MyE.Oszlopnév(oszlop + 2) + "5");
                            MyE.Betű(MyE.Oszlopnév(oszlop) + "5", false, false, true);
                            MyE.Betű(MyE.Oszlopnév(oszlop + 1) + "5", false, false, true);
                            MyE.Betű(MyE.Oszlopnév(oszlop + 2) + "5", false, false, true);
                            MyE.Betű(MyE.Oszlopnév(oszlop) + "4", false, false, true);
                            MyE.Betű(MyE.Oszlopnév(oszlop + 2) + "4", false, false, true);
                            Napszíne += "0";
                        }
                    }

                    MyE.SzövegIrány(munkalap, MyE.Oszlopnév(oszlop) + "5:" + MyE.Oszlopnév(oszlop + 2) + "5", 90);
                    MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(oszlop) + ":" + MyE.Oszlopnév(oszlop + 1), 6);
                    MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(oszlop + 2) + ":" + MyE.Oszlopnév(oszlop + 2), 10);

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
                            MyE.Kiir(darabol[1], "a" + (hanyadikember + 6).ToString());
                            // dolgozó név
                            MyE.Kiir(darabol[0], "b" + (hanyadikember + 6).ToString());
                            l++;
                            break;
                        }
                        l++;
                    }
                    Holtart.Value = hanyadikember + 1;
                    hanyadikember += 1;
                }
                hanyadikember += 2;
                // sormagasság
                MyE.Sormagasság("6:" + (hanyadikember + 6).ToString(), 24);
                MyE.Egyesít(munkalap, "a" + (hanyadikember + 6).ToString() + ":b" + (hanyadikember + 6).ToString());
                MyE.Kiir(" Az igazoló aláírása:\n" + LstKiadta.Text.Trim(), "a" + (hanyadikember + 6).ToString());
                i = 0;
                oszlop = 3;
                do
                {
                    MyE.Egyesít(munkalap, MyE.Oszlopnév(oszlop) + (hanyadikember + 6).ToString() + ":" + MyE.Oszlopnév(oszlop + 2) + (hanyadikember + 6).ToString());
                    i += 1;
                    oszlop += 3;
                }
                while (i != 7);
                // formázunk
                // fejléc rácsozás
                MyE.Vastagkeret("a4:a5");
                MyE.Vastagkeret("b4:b5");
                oszlop = 3;
                MyE.Rácsoz(MyE.Oszlopnév(oszlop) + "5:" + MyE.Oszlopnév(oszlop + 2) + "5");
                for (int j = 0; j < 7; j++)
                {
                    MyE.Rácsoz(MyE.Oszlopnév(oszlop) + "5:" + MyE.Oszlopnév(oszlop + 2) + "5");

                    MyE.Rácsoz(MyE.Oszlopnév(oszlop) + "4:" + MyE.Oszlopnév(oszlop + 2) + "5");
                    oszlop += 3;
                }
                // középsőrész
                MyE.Rácsoz("a6:" + MyE.Oszlopnév(2 + 7 * 3) + (hanyadikember + 5).ToString());
                MyE.Vastagkeret("a6:" + MyE.Oszlopnév(2 + 7 * 3) + (hanyadikember + 5).ToString());
                MyE.Vastagkeret("A6:A" + (hanyadikember + 5).ToString());
                MyE.Rácsoz("A6:A" + (hanyadikember + 5).ToString());
                MyE.Vastagkeret("B6:B" + (hanyadikember + 5).ToString());
                MyE.Rácsoz("B6:B" + (hanyadikember + 5).ToString());

                // napok rácsozása
                oszlop = 3;
                for (int ii = 0; ii < 6; ii++)
                {
                    MyE.Rácsoz(MyE.Oszlopnév(oszlop) + "6:" + MyE.Oszlopnév(oszlop + 2) + (hanyadikember + 5).ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlop) + "6:" + MyE.Oszlopnév(oszlop + 2) + (hanyadikember + 5).ToString());
                    oszlop += 3;
                }

                // igazoló rész
                MyE.Sormagasság((hanyadikember + 6).ToString() + ":" + (hanyadikember + 6).ToString(), 36);
                MyE.Vastagkeret(MyE.Oszlopnév(1) + (hanyadikember + 6).ToString() + ":" + MyE.Oszlopnév(2 + 7 * 3).ToString() + (hanyadikember + 6).ToString());
                for (int k = 0; k < 19; k += 3)
                {
                    MyE.Vastagkeret(MyE.Oszlopnév(k) + (hanyadikember + 6).ToString() + ":" + MyE.Oszlopnév(k + 2).ToString() + (hanyadikember + 6).ToString());
                    oszlop += 3;
                }
                MyE.sor = 9 + hanyadikember;
                MyE.Kiir(" Az igazoló aláírása", "a" + MyE.sor.ToString());
                // színezés
                oszlop = 3;
                for (i = 0; i < 7; i++)
                {

                    if (MyF.Szöveg_Tisztítás(Napszíne, i, 1) == "1")
                        MyE.Háttérszín(MyE.Oszlopnév(oszlop) + "5:" + MyE.Oszlopnév(oszlop + 2) + (hanyadikember + 6).ToString(), 65535);
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
                    MyE.sor += 2;
                    MyE.Betű("e" + MyE.sor.ToString(), 14);
                    MyE.Kiir(Elem.Név, "e" + MyE.sor.ToString());
                    MyE.sor += 1;
                    MyE.Betű("e" + MyE.sor.ToString(), 14);
                    MyE.Kiir(Elem.Beosztás, "e" + MyE.sor.ToString());
                    MyE.Igazít_függőleges("1:23", "alsó");
                    MyE.Oszlopszélesség(munkalap, "B:B");
                }
                // **********************************************
                // **Nyomtatási beállítások                    **
                // **********************************************
                MyE.NyomtatásiTerület_részletes(munkalap, "a1:w" + MyE.sor.ToString(),
                   0.393700787401575d, 0.393700787401575, 0.590551181102362d, 0.590551181102362d, 0.511811023622047d, 0.511811023622047d,
                   "1", "1", false, RdBtnA4.Checked == true ? "A4" : "A3", true, false);
                // **********************************************
                // **Nyomtatás                                 **
                // **********************************************
                if (RdBtnNyomtat.Checked)
                    MyE.Nyomtatás(munkalap, 1, 1);

                Holtart.Ki();
                MyE.Aktív_Cella(munkalap, "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();

                if (RdBtnFájlTöröl.Checked)
                    File.Delete(fájlexc);

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

                string helynap = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Beosztás\{Dátum.Value.Year}\Ebeosztás{Dátum.Value:yyyyMM}.mdb";
                if (!File.Exists(helynap))
                    return;
                string jelszónap = "kiskakas";
                string helykieg = $@"{Application.StartupPath}\" + Cmbtelephely.Text + @"\adatok\segéd\Kiegészítő.mdb";
                if (!File.Exists(helykieg))
                    return;
                string jelszókieg = "Mocó";
                string szövegkieg = "SELECT * FROM Beosztáskódok WHERE Számoló = true order by BeosztásKód";
                Kezelő_Kiegészítő_Beosztáskódok kéz = new Kezelő_Kiegészítő_Beosztáskódok();
                List<Adat_Kiegészítő_Beosztáskódok> Beosztáskód = kéz.Lista_Adatok(helykieg, jelszókieg, szövegkieg);

                string szövegnap = $"SELECT * FROM Beosztás WHERE Nap = #{Dátum.Value:M-d-yy}# order by Dolgozószám";
                Kezelő_Dolgozó_Beosztás_Új kézbeoszt = new Kezelő_Dolgozó_Beosztás_Új();
                List<Adat_Dolgozó_Beosztás_Új> Dolgbeoszt = kézbeoszt.Lista_Adatok(helynap, jelszónap, szövegnap);

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
                string munkalap = "Munka1";

                int hányember = ChkDolgozónév.CheckedItems.Count;
                if (hányember < 1)
                    throw new HibásBevittAdat("Nincs kijelölve egy dolgozó sem.");

                Holtart.Be();
                string fájlexc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + "Ittaság-vizsgálati_"
                    + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx";
                MyE.ExcelLétrehozás();
                MyE.Munkalap_betű("arial", 12);
                MyE.Oszlopszélesség(munkalap, "a:a", 9);
                MyE.Oszlopszélesség(munkalap, "b:b", 30);
                MyE.Oszlopszélesség(munkalap, "c:c", 18);
                MyE.Oszlopszélesség(munkalap, "d:d", 20);
                MyE.Egyesít(munkalap, "a1:d1");
                MyE.Egyesít(munkalap, "a2:d2");
                MyE.Egyesít(munkalap, "a3:d3");

                Adat_Kiegészítő_Jelenlétiív Eleme = (from a in AdatokJelenléti
                                                     where a.Id == 2
                                                     select a).FirstOrDefault();
                if (Eleme != null) MyE.Kiir(Eleme.Szervezet, "a1");
                Eleme = (from a in AdatokJelenléti
                         where a.Id == 3
                         select a).FirstOrDefault();
                if (Eleme != null) MyE.Kiir(Eleme.Szervezet, "a2");
                Eleme = (from a in AdatokJelenléti
                         where a.Id == 4
                         select a).FirstOrDefault();
                if (Eleme != null) MyE.Kiir(Eleme.Szervezet, "a3");


                MyE.Igazít_függőleges("A1:D3", "alsó");
                MyE.Igazít_vízszintes("A1:D3", "bal");
                MyE.Sormagasság("5:5", 32);
                MyE.Egyesít(munkalap, "a5:d5");
                MyE.Kiir("Ittasság-vizsgálati napló", "a5");
                MyE.Egyesít(munkalap, "a8:d8");
                MyE.Kiir("A vizsgálat időpontja (nap/óra)  ………………………………………………………", "a8");
                MyE.Igazít_vízszintes("A8", "bal");
                MyE.Egyesít(munkalap, "a10:d10");
                MyE.Kiir("A vizsgálatot végezte              ………………………………………………………", "a10");
                MyE.Igazít_vízszintes("A10", "bal");
                MyE.Egyesít(munkalap, "a12:d12");
                MyE.Kiir("Jelen volt                                 ……………………………………………………………", "a12");
                MyE.Igazít_vízszintes("A12", "bal");
                MyE.Igazít_függőleges("A8:D8", "alsó");
                MyE.Igazít_függőleges("A10:D10", "alsó");
                MyE.Igazít_függőleges("A12:D12", "alsó");
                MyE.Betű("a5", 20);

                // fejléc
                MyE.Kiir("Sorszám", "a14");
                MyE.Kiir("Vizsgált személy neve", "b14");
                MyE.Kiir("Vizsgálat \neredménye", "c14");
                MyE.Kiir("Megjegyzés \n(intézkedés)", "d14");
                MyE.Igazít_függőleges("A14:D14", "alsó");
                MyE.Betű("A14:D14", false, false, true);
                MyE.Igazít_függőleges("A14", "alsó");
                MyE.Sormagasság("14:14", 35);
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
                    MyE.Kiir(l.ToString(), "a" + sor.ToString());
                    // dolgozó név
                    MyE.Kiir(darabol[0], "b" + sor.ToString());
                    MyE.Sormagasság(sor + ":" + sor, 25);

                    hanyadikember += 1;
                    Holtart.Lép();
                }

                hanyadikember += 2;
                MyE.Rácsoz("a14:d" + sor.ToString());
                MyE.Vastagkeret("a14:d14");
                MyE.Vastagkeret("a15:d" + sor.ToString());

                // **********************************************
                // **Nyomtatás                                 **
                // **********************************************
                MyE.NyomtatásiTerület_részletes(munkalap, "a1:d" + sor.ToString(), "$1:$14", "",
                    "", "", "&P/&N",
                    "Budapest, " + Dátum.Value.ToString("yyyy.MM.dd"), "", "..........................................\nVizsgálatot végző aláírása", "",
                    0.393700787401575d, 0.393700787401575,
                    0.984251968503937, 0.590551181102362d,
                    0.511811023622047d, 0.511811023622047d,
                    false, false,
                    "1", "", true, RdBtnA4.Checked ? "A4" : "A3");

                if (RdBtnNyomtat.Checked)
                    MyE.Nyomtatás(munkalap, 1, 1);

                Holtart.Visible = false;
                MyE.Aktív_Cella(munkalap, "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();


                if (RdBtnFájlTöröl.Checked)
                    File.Delete(fájlexc);

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
                string munkalap = "Munka1";
                int hányember = ChkDolgozónév.CheckedItems.Count;
                if (hányember < 1)
                    throw new HibásBevittAdat("Nincs kijelölve egy dolgozó sem.");

                Holtart.Be();
                string fájlexc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + "Ittassági_"
                    + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx";
                MyE.ExcelLétrehozás();
                MyE.Munkalap_betű("arial", 14);
                MyE.Betű("a1", 14);
                MyE.Betű("a1", false, false, true);

                Adat_Kiegészítő_Jelenlétiív Eleme = (from a in AdatokJelenléti
                                                     where a.Id == 2
                                                     select a).FirstOrDefault();
                if (Eleme != null) MyE.Kiir(Eleme.Szervezet, "a1");
                Eleme = (from a in AdatokJelenléti
                         where a.Id == 3
                         select a).FirstOrDefault();
                if (Eleme != null) MyE.Kiir(Eleme.Szervezet, "a2");
                Eleme = (from a in AdatokJelenléti
                         where a.Id == 4
                         select a).FirstOrDefault();
                if (Eleme != null) MyE.Kiir(Eleme.Szervezet, "a3");

                MyE.Oszlopszélesség(munkalap, "a:a", 20);
                MyE.Oszlopszélesség(munkalap, "b:b", 35);
                MyE.Egyesít(munkalap, "a4:l4");
                MyE.Betű("a4", 20);
                MyE.Betű("a4", false, false, true);
                MyE.Kiir("Ittasság-vizsgálati napló", "a4");
                int mennyi = 5;
                DateTime elsőnap = MyF.Hét_elsőnapja(Dátum.Value);
                DateTime utolsónap = MyF.Hét_Utolsónapja(Dátum.Value);
                MyE.Kiir("HR azonosító", "a9");
                MyE.Kiir("A munkavállaló neve", "b9");
                MyE.Egyesít(munkalap, "a8:b8");
                MyE.Kiir("A vizsgálat időpontja (Óra:perc)", "a8");

                // napok fejlécet létrehozzuk
                string szöveg = $"{elsőnap:yyyy. év MMMM dd}.-tól - ";
                szöveg += $"{utolsónap:yyyy. év MMMM dd}.-ig ";
                MyE.Kiir(szöveg, "a5");

                // sormagasság
                MyE.Sormagasság("7:7", 30);
                MyE.Sormagasság("8:8", 30);
                MyE.Sormagasság("9:9", 37);
                int oszlop = 3;
                for (int i = 0; i < mennyi; i++)
                {
                    MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(oszlop) + ":" + MyE.Oszlopnév(oszlop), 20);
                    MyE.Kiir($"{elsőnap.AddDays(i):dddd}", $"{MyE.Oszlopnév(oszlop)}7");
                    MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(oszlop + 1) + ":" + MyE.Oszlopnév(oszlop + 1), 20);
                    MyE.Kiir($"{elsőnap.AddDays(i):dd}", $"{MyE.Oszlopnév(oszlop + 1)}7");
                    MyE.Kiir("Vizsgálati eredmény", MyE.Oszlopnév(oszlop) + "9");
                    MyE.Kiir("Megjegyzés (intézkedés)", MyE.Oszlopnév(oszlop + 1) + "9");
                    MyE.Egyesít(munkalap, MyE.Oszlopnév(oszlop) + "8" + ":" + MyE.Oszlopnév(oszlop + 1) + "8");
                    oszlop += 2;
                }
                MyE.Igazít_függőleges("7:7", "alsó");
                MyE.Sortörésseltöbbsorba("7:7", false);
                MyE.Igazít_függőleges("9:9", "alsó");
                MyE.Sortörésseltöbbsorba("9:9", false);
                int hanyadikember = 0;


                foreach (string Elem in ChkDolgozónév.CheckedItems)
                {
                    // hrazonosító
                    string[] darabol = Elem.Split('=');
                    MyE.Kiir(darabol[1], "a" + (hanyadikember + 10).ToString());
                    // dolgozó név
                    MyE.Kiir(darabol[0], "b" + (hanyadikember + 10).ToString());
                    hanyadikember++;
                    Holtart.Lép();
                }

                hanyadikember += 2;
                // sormagasság
                MyE.Sormagasság("10:" + (hanyadikember + 10).ToString(), 24);
                MyE.Sormagasság(hanyadikember + 10.ToString() + ":" + (hanyadikember + 13).ToString(), 35);
                MyE.Egyesít(munkalap, "a" + (hanyadikember + 10).ToString() + ":b" + (hanyadikember + 10).ToString());
                MyE.Kiir("Vizsgálatot végezte", "a" + (hanyadikember + 10).ToString());
                MyE.Sormagasság("a" + (hanyadikember + 10).ToString(), 35);
                hanyadikember += 1;
                MyE.Egyesít(munkalap, "a" + (hanyadikember + 10).ToString() + ":b" + (hanyadikember + 10).ToString());
                MyE.Kiir("Vizsgáltot végző aláírása", "a" + (hanyadikember + 10).ToString());
                MyE.Sormagasság("a" + (hanyadikember + 10).ToString(), 35);
                hanyadikember += 1;
                MyE.Egyesít(munkalap, "a" + (hanyadikember + 10).ToString() + ":b" + (hanyadikember + 10).ToString());
                MyE.Kiir("Jelen volt", "a" + (hanyadikember + 10).ToString());
                MyE.Sormagasság("a" + (hanyadikember + 10).ToString(), 35);
                oszlop = 3;
                for (int i = 0; i < mennyi; i++)
                    oszlop += 2;

                // formázunk
                // rácsozás
                MyE.Rácsoz("a7:" + MyE.Oszlopnév(oszlop - 1) + (hanyadikember + 10).ToString());
                MyE.Oszlopszélesség(munkalap, "B:B");
                oszlop = 2 + mennyi * 2;

                // rácsozunk naponta
                for (int i = 1; i < oszlop; i += 2)
                {
                    MyE.Egyesít(munkalap, MyE.Oszlopnév(i) + (hanyadikember + 10).ToString() + ":" + MyE.Oszlopnév(i + 1) + (hanyadikember + 10).ToString());
                    MyE.Egyesít(munkalap, MyE.Oszlopnév(i) + (hanyadikember + 9).ToString() + ":" + MyE.Oszlopnév(i + 1) + (hanyadikember + 9).ToString());
                    MyE.Egyesít(munkalap, MyE.Oszlopnév(i) + (hanyadikember + 8).ToString() + ":" + MyE.Oszlopnév(i + 1) + (hanyadikember + 8).ToString());
                    MyE.Rácsoz(MyE.Oszlopnév(i) + "7:" + MyE.Oszlopnév(i + 1) + (hanyadikember + 10).ToString());
                }
                MyE.Igazít_függőleges("A:P", "alsó");
                MyE.Igazít_vízszintes("A:P", "közép");
                MyE.Igazít_vízszintes("A1:A3", "bal");
                MyE.Igazít_vízszintes("A5", "bal");

                // **********************************************
                // **Nyomtatás                                 **
                // **********************************************
                MyE.NyomtatásiTerület_részletes(munkalap, "a1:" + MyE.Oszlopnév(2 + mennyi * 2) + (hanyadikember + 10).ToString(),
                        0.393700787401575, 0.393700787401575,
                        0.590551181102362, 0.590551181102362,
                        0.511811023622047, 0.511811023622047,
                        "1", "1",
                        false, RdBtnA4.Checked ? "A4" : "A3",
                        true, false);

                if (RdBtnNyomtat.Checked)
                    MyE.Nyomtatás(munkalap, 1, 1);
                Holtart.Ki();
                MyE.Aktív_Cella(munkalap, "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();

                if (RdBtnFájlTöröl.Checked)
                    File.Delete(fájlexc);
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

        #region Listák

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
        #endregion
    }
}