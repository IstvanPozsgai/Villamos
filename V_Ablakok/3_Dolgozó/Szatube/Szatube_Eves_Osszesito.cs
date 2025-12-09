using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_Adatszerkezet;
using Villamos.Villamos_Adatszerkezet;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos.V_Ablakok._3_Dolgozó.Szatube
{
    public class Szatube_Eves_Osszesito
    {
        //JAV: Holtart nem működik
        readonly Beállítás_Betű BeBetuDoltVastag = new Beállítás_Betű() { Dőlt = true, Vastag = true, Méret = 11, Név = "Aptos Narrow" };

        public void Eves_Osszesito(string fájlexc, string[] darabol, Kezelő_Szatube_Szabadság KézSzabadság, string CmbTelephely, int Adat_Évek)
        {
            string munkalap = "Munka1";
            MyX.ExcelLétrehozás();

            // elkészítjük a fejlécet
            MyX.Oszlopszélesség(munkalap, "A:A", 35);
            MyX.Oszlopszélesség(munkalap, "B:D", 12);
            MyX.Oszlopszélesség(munkalap, "e:e", 18);
            // dolgozó törzszáma

            MyX.Egyesít(munkalap, "b1:d1");
            MyX.Kiir($"Szabadság Összesítő a {Adat_Évek} évre", "b1");
            //MyX.Betű("b1");

            MyX.Kiir("Név:", "a3");
            MyX.Egyesít(munkalap, "b3:e3");
            MyX.Kiir(darabol[0].Trim(), "b3");
            //MyX.Betű("b3");

            MyX.Kiir("Azonosító:", "a5");
            MyX.Egyesít(munkalap, "b5:e5");
            MyX.Kiir($"#SZÁME#{darabol[1].Trim()}", "b5");
            //MyX.Betű("b5");

            MyX.Egyesít(munkalap, "a9:b9");
            MyX.Kiir("Felhasználható szabadságok", "a9");
            //MyX.Betű("a9");
            MyX.Kiir("Jogcím", "a10");
            //MyX.Betű("a10");
            MyX.Kiir("Nap", "b10");
            //MyX.Betű("b10");

            int sor = 11;
            int összesen = 0;

            List<Adat_Szatube_Szabadság> AdatokÖ = KézSzabadság.Lista_Adatok(CmbTelephely, Adat_Évek);
            List<Adat_Szatube_Szabadság> Adatok = (from a in AdatokÖ
                                                   where a.Törzsszám == darabol[1].Trim() &&
                                                   a.Státus != 3 &&
                                                   (a.Szabiok.ToLower().Contains("pót") || a.Szabiok.ToLower().Contains("alap"))
                                                   orderby a.Kezdődátum
                                                   select a).ToList();

            foreach (Adat_Szatube_Szabadság rekord in Adatok)
            {
                // ha nincs dátum akkor jogcím
                MyX.Kiir(rekord.Szabiok.Trim(), "a" + sor);
                MyX.Kiir($"#SZÁME#{rekord.Kivettnap}", "b" + sor);
                összesen += rekord.Kivettnap;
                sor += 1;
                //Holtart.Lép();
            }
            MyX.Kiir("Összesen:", "a" + sor);
            MyX.Betű(munkalap, "A" + sor, BeBetuDoltVastag);
            MyX.Kiir($"#SZÁME#{összesen}", "b" + sor);
            MyX.Betű(munkalap, "B" + sor, BeBetuDoltVastag);

            MyX.Rácsoz(munkalap, "a9:b" + sor);
            MyX.Vastagkeret(munkalap, "a9:b" + sor);
            MyX.Vastagkeret(munkalap, "a10:b" + sor);
            MyX.Vastagkeret(munkalap, "a" + sor + ":b" + sor);
            sor += 3;
            int eleje = sor;
            MyX.Egyesít(munkalap, "a" + sor + ":e" + sor);
            MyX.Kiir("Szabadság felhasználás", "a" + sor);
            //MyX.Betű("a" + sor);
            sor += 1;
            MyX.Kiir("Sorszám", "a" + sor);
            MyX.Kiir("Kezdete", "b" + sor);
            MyX.Kiir("Vége", "c" + sor);
            MyX.Kiir("Kivett nap", "d" + sor);
            MyX.Kiir("Kivétel oka", "e" + sor);
            //MyX.Betű("a" + sor + ":e" + sor);
            sor += 1;

            int kivett = 0;

            Adatok = (from a in AdatokÖ
                      where a.Törzsszám == darabol[1].Trim() &&
                                  a.Státus != 3 &&
                                  a.Szabiok.ToLower().Contains("kivétel")
                      orderby a.Kezdődátum
                      select a).ToList();

            foreach (Adat_Szatube_Szabadság rekord in Adatok)
            {
                // ha nincs dátum akkor jogcím
                MyX.Kiir($"#SZÁME#{rekord.Sorszám}", "a" + sor);
                MyX.Kiir(rekord.Kezdődátum.ToString("yyyy.MM.dd"), "b" + sor);
                MyX.Kiir(rekord.Befejeződátum.ToString("yyyy.MM.dd"), "c" + sor);
                MyX.Kiir($"#SZÁME#{rekord.Kivettnap}", "d" + sor);
                MyX.Kiir(rekord.Szabiok.Trim(), "e" + sor);
                kivett += rekord.Kivettnap;
                sor += 1;
                //Holtart.Lép();
            }

            MyX.Kiir("Összesen:", "a" + sor);
            MyX.Betű(munkalap, "A" + sor, BeBetuDoltVastag);
            MyX.Kiir($"#SZÁME#{kivett}", "d" + sor);
            MyX.Betű(munkalap, "D" + sor, BeBetuDoltVastag);
            MyX.Rácsoz(munkalap, "a" + eleje.ToString() + ":e" + sor);
            MyX.Vastagkeret(munkalap, "a" + eleje.ToString() + ":e" + sor);
            MyX.Vastagkeret(munkalap, "a" + eleje.ToString() + ":e" + (eleje + 1).ToString());
            MyX.Vastagkeret(munkalap, "a" + sor + ":e" + sor);
            sor += 2;
            MyX.Kiir($"A {Adat_Évek} évről marad:", "a" + sor);
            MyX.Kiir($"#SZÁME#{(összesen - kivett)}", "d" + sor);
            MyX.Betű(munkalap, sor + ":" + sor, BeBetuDoltVastag);
            Beállítás_Nyomtatás beallitas_szabadsag = new Beállítás_Nyomtatás
            {
                Munkalap = munkalap,
                NyomtatásiTerület = $"A1:E{sor}",
                Álló = true
            };
            MyX.NyomtatásiTerület_részletes(munkalap, beallitas_szabadsag);

            MyX.ExcelMentés(fájlexc + ".xlsx");
            MyX.ExcelBezárás();
            //Holtart.Ki();

            MessageBox.Show("Elkészült az Excel tábla.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


    }
}
