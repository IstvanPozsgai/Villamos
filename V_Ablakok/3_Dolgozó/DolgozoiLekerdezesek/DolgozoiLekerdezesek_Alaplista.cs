using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_Adatszerkezet;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos.V_Ablakok._3_Dolgozó.DolgozoiLekerdezesek
{
    public class DolgozoiLekerdezesek_Alaplista
    {
        Beállítás_Betű BeBetű = new Beállítás_Betű() { Név = "Arial", Méret = 12 };
        Beállítás_Betű BeBetűVastag = new Beállítás_Betű() { Név = "Arial", Méret = 12, Vastag = true };
        public void ExcelGeneral(string fájlexc, string Cmbtelephely, string Változatoklist, string[] Csoportlista, Kezelő_Létszám_Elrendezés_Változatok Kéz_Változatok, Kezelő_Dolgozó_Alap KézDolg)
        {
            string munkalap = "Munka1";
            MyX.ExcelLétrehozás();

            MyX.Munkalap_betű(munkalap, BeBetű);
            //Holtart.Be();

            List<Adat_Létszám_Elrendezés_Változatok> AdatokLétÖ = Kéz_Változatok.Lista_Adatok(Cmbtelephely);
            List<Adat_Létszám_Elrendezés_Változatok> AdatokLét = (from a in AdatokLétÖ
                                                                  where a.Változatnév == Változatoklist
                                                                  select a).ToList();

            List<Adat_Dolgozó_Alap> AdatokDolg = KézDolg.Lista_Adatok(Cmbtelephely);

            // passzív
            int i = 1;
            MyX.Kiir("Passzív", $"A{i}");
            MyX.Háttérszín(munkalap, $"A{i}", Color.FromArgb(15773696));
            List<Adat_Dolgozó_Alap> Rész = (from a in AdatokDolg
                                            where a.Kilépésiidő == new DateTime(1900, 1, 1)
                                            && a.Passzív == true
                                            select a).ToList();
            if (Rész.Count > 0)
                MyX.Kiir($"#SZÁME#{Rész.Count}", $"b{i}");
            else
                MyX.Kiir("0", $"b{i}");

            i += 1;
            // vezényelt
            MyX.Kiir("El Vezényelt", $"A{i}");
            MyX.Háttérszín(munkalap, "A" + i, Color.FromArgb(65535));
            Rész.Clear();
            Rész = (from a in AdatokDolg
                    where a.Vezényelt == true && a.Kilépésiidő == new DateTime(1900, 1, 1)
                    select a).ToList();
            if (Rész != null)
                MyX.Kiir($"#SZÁME#{Rész.Count}", $"b{i}");
            else
                MyX.Kiir("0", $"b{i}");


            i += 1;
            // vezényelve
            MyX.Kiir("Ide Vezényelve", $"A{i}".ToString());
            MyX.Háttérszín(munkalap, "A" + i, Color.FromArgb(33023));
            Rész.Clear();
            Rész = (from a in AdatokDolg
                    where a.Vezényelve == true && a.Kilépésiidő == new DateTime(1900, 1, 1)
                    select a).ToList();
            if (Rész != null)
                MyX.Kiir($"#SZÁME#{Rész.Count}", $"b{i}");
            else
                MyX.Kiir("0", $"b{i}");


            i += 1;
            // részmunkaidős
            MyX.Kiir("Részmunkaidős", $"A{i}".ToString());
            MyX.Háttérszín(munkalap, "A" + i, Color.FromArgb(65280));
            Rész.Clear();
            Rész = (from a in AdatokDolg
                    where a.Részmunkaidős == true && a.Kilépésiidő == new DateTime(1900, 1, 1)
                    select a).ToList();
            if (Rész != null)
                MyX.Kiir($"#SZÁME#{Rész.Count}", $"b{i}");
            else
                MyX.Kiir("0", $"b{i}");


            i += 1;
            // Nincs csoportban
            MyX.Kiir("Nincs csoportban", $"A{i}".ToString());
            Rész.Clear();
            Rész = (from a in AdatokDolg
                    where a.Csoport == "" && a.Kilépésiidő == new DateTime(1900, 1, 1)
                    select a).ToList();
            if (Rész != null)
                MyX.Kiir($"#SZÁME#{Rész.Count}", $"b{i}");
            else
                MyX.Kiir("0", $"b{i}");


            // csoportok létszáma
            i += 2;
            int darabö = 0;

            int k = 0;
            while (k != Csoportlista.Length)
            {
                MyX.Kiir(Csoportlista[k], $"A{i}".ToString());
                Rész.Clear();
                Rész = (from a in AdatokDolg
                        where a.Csoport == Csoportlista[k]
                        && a.Passzív == false
                        && a.Vezényelve == false
                        && a.Részmunkaidős == false
                        && a.Kilépésiidő == new DateTime(1900, 1, 1)
                        select a).ToList();
                if (Rész != null)
                    MyX.Kiir($"#SZÁME#{Rész.Count}", $"b{i}");
                else
                    MyX.Kiir("0", $"b{i}");

                i += 1;
                k += 1;
                darabö += Rész.Count;
            }
            MyX.Kiir("Összesen:", $"A{i}".ToString());
            MyX.Betű(munkalap,"A" + i, BeBetűVastag);
            MyX.Kiir($"#SZÁME#{darabö}", $"b{i}");
            MyX.Betű(munkalap,"b" + i, BeBetűVastag);

            // kiirja a passzívokat
            i += 2;
            MyX.Kiir("Passzív", $"A{i}");
            i += 1;

            List<Adat_Dolgozó_Alap> Adatok = KézDolg.Lista_Adatok(Cmbtelephely);
            Adatok = (from a in AdatokDolg
                      where a.Kilépésiidő == new DateTime(1900, 1, 1)
                      && a.Passzív == true
                      select a).ToList();
            foreach (Adat_Dolgozó_Alap rekord in Adatok)
            {
                MyX.Kiir(rekord.DolgozóNév.Trim(), $"A{i}");
                i += 1;
            }

            // kirja a nincs csoportbant
            i += 2;
            MyX.Kiir("Nincs csoportban", $"A{i}");
            i += 1;

            AdatokDolg = KézDolg.Lista_Adatok(Cmbtelephely);
            Adatok = (from a in AdatokDolg
                      where a.Csoport == ""
                      && a.Kilépésiidő == new DateTime(1900, 1, 1)
                      select a).ToList();
            foreach (Adat_Dolgozó_Alap rekord in Adatok)
            {
                MyX.Kiir(rekord.DolgozóNév.Trim(), $"A{i}");
                i += 1;
            }

            int utolsósor = 0;

            int utolsóoszlop = 1;
            int sor;
            // csoportonkénti kiírás
            foreach (Adat_Létszám_Elrendezés_Változatok Ábrázol in AdatokLét)
            {
                Adatok = (from a in AdatokDolg
                          where a.Csoport == Ábrázol.Csoportnév.Trim()
                          && a.Kilépésiidő == new DateTime(1900, 1, 1)
                          select a).ToList();
                sor = Ábrázol.Sor;
                //csoportnév
                MyX.Kiir(Ábrázol.Csoportnév.Trim(), Ábrázol.Oszlop + sor);
                MyX.Betű(munkalap,Ábrázol.Oszlop + sor, BeBetűVastag);
                MyX.Háttérszín(munkalap, Ábrázol.Oszlop + sor, Color.FromArgb(13092807));

                //Csoport tagjai
                sor++;
                foreach (Adat_Dolgozó_Alap rekord in Adatok)
                {
                    MyX.Kiir(rekord.DolgozóNév.Trim(), Ábrázol.Oszlop + sor);
                    if (rekord.Vezényelve) MyX.Háttérszín(munkalap, Ábrázol.Oszlop + sor, Color.FromArgb(33023));
                    if (rekord.Vezényelt) MyX.Háttérszín(munkalap, Ábrázol.Oszlop + sor, Color.FromArgb(65535));
                    if (rekord.Részmunkaidős) MyX.Háttérszín(munkalap, Ábrázol.Oszlop + sor, Color.FromArgb(65280));
                    if (rekord.Passzív) MyX.Háttérszín(munkalap, Ábrázol.Oszlop + sor, Color.FromArgb(15773696));
                    if (rekord.Csopvez) MyX.Betű(munkalap,Ábrázol.Oszlop + sor, BeBetűVastag);
                    sor += 1;
                    //Holtart.Lép();
                }
                if (utolsósor < sor) utolsósor = sor;
                if (Ábrázol.Oszlop.Length == 1)
                {
                    int oszlop = (int)(char.Parse(Ábrázol.Oszlop.Substring(0, 1).ToUpper())) - 64;
                    if (utolsóoszlop < oszlop)
                        utolsóoszlop = oszlop;
                }
                MyX.Oszlopszélesség(munkalap, Ábrázol.Oszlop + ":" + Ábrázol.Oszlop, Ábrázol.Szélesség);
                //Holtart.Lép();
            }


            MyX.Oszlopszélesség(munkalap, "A:A", 25);

            MyX.Vastagkeret(munkalap, "A1:" + MyF.Oszlopnév(utolsóoszlop) + utolsósor);
            MyX.Rácsoz(munkalap, "A1:" + MyF.Oszlopnév(utolsóoszlop) + utolsósor);
            Beállítás_Nyomtatás NyomtatásBeállít = new Beállítás_Nyomtatás() {Munkalap=munkalap, NyomtatásiTerület= "A1:" + MyF.Oszlopnév(utolsóoszlop) + utolsósor, Álló = false };
            MyX.NyomtatásiTerület_részletes(munkalap, NyomtatásBeállít);
            MyX.Aktív_Cella(munkalap, "A1");
            MyX.ExcelMentés(fájlexc+".xlsx");
            MyX.ExcelBezárás();

            //Holtart.Ki();

            MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
