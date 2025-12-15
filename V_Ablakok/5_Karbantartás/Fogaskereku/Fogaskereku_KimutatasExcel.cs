using System.Collections.Generic;
using System.Data;
using Villamos.Adatszerkezet;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos.V_Ablakok._5_Karbantartás.Fogaskereku
{
    public class Fogaskereku_KimutatasExcel
    {
        public void KimutatastKeszit(string fájlexc, DataTable Tábla)
        {
            MyX.ExcelLétrehozás();


            string munkalap = "Adatok";
            MyX.Munkalap_átnevezés("Munka1", munkalap);

            long utolsósor = MyX.Munkalap(Tábla, 1, munkalap);

            //Holtart.Lép();


            MyX.Kiir("Év", "v1");
            MyX.Kiir("hó", "w1");
            MyX.Kiir("Vizsgálat rövid", "x1");

            // kiírjuk az évet, hónapot és a 2 betűs vizsgálatot
            MyX.Kiir("#KÉPLET#=YEAR(RC[-15])", "v2");
            MyX.Kiir("#KÉPLET#=MONTH(RC[-16])", "w2");
            MyX.Kiir("#KÉPLET#=LEFT(RC[-18],2)", "x2");
            //Holtart.Lép();

            MyX.Képlet_másol(munkalap, "V2:X2", "V3:X" + (utolsósor + 1));
            MyX.Rácsoz(munkalap, "A1:X" + (utolsósor + 1));

            MyX.Oszlopszélesség(munkalap, "A:X");
            //Holtart.Lép();

            MyX.Aktív_Cella(munkalap, "A1");
            Beállítás_Nyomtatás NyomtatásBeállít = new Beállítás_Nyomtatás() { Munkalap = munkalap, NyomtatásiTerület = "A1:X" + (utolsósor + 1), IsmétlődőSorok = "$1:$1", Álló = true };
            MyX.NyomtatásiTerület_részletes(munkalap, NyomtatásBeállít);
            //Holtart.Lép();

            munkalap = "Kimutatás";
            MyX.Munkalap_Új(munkalap);
            MyX.Munkalap_aktív(munkalap);

            Kimutatás3(utolsósor);
            //Holtart.Lép();
            MyX.ExcelMentés(fájlexc);
            MyX.ExcelBezárás();

        }

        private void Kimutatás3(long utolsósor)
        {

            string munkalap = "Kimutatás";
            MyX.Munkalap_aktív(munkalap);


            string munkalap_adat = "Adatok";
            string balfelső = "A1";
            string jobbalsó = "X" + (utolsósor + 1);
            string kimutatás_Munkalap = munkalap;
            string Kimutatás_cella = "A6";
            string Kimutatás_név = "Kimutatás";

            List<string> összesítNév = new List<string>();
            List<string> Összesít_módja = new List<string>();
            List<string> sorNév = new List<string>();
            List<string> oszlopNév = new List<string>();
            List<string> SzűrőNév = new List<string>();

            összesítNév.Add("azonosító");

            Összesít_módja.Add("xlCount");

            sorNév.Add("Vizsgálat rövid");


            SzűrőNév.Add("Év");
            SzűrőNév.Add("hó");

            oszlopNév.Add("V2végezte");


            Beállítás_Kimutatás Bekimutat = new Beállítás_Kimutatás
            {
                Munkalapnév = munkalap_adat,
                Balfelső = balfelső,
                Jobbalsó = jobbalsó,
                Kimutatás_Munkalapnév = kimutatás_Munkalap,
                Kimutatás_cella = Kimutatás_cella,
                Kimutatás_név = Kimutatás_név,
                ÖsszesítNév = összesítNév,
                Összesítés_módja = Összesít_módja,
                SorNév = sorNév,
                OszlopNév = oszlopNév,
                SzűrőNév = SzűrőNév
            };

            MyX.Kimutatás_Fő(Bekimutat);
            MyX.Aktív_Cella(munkalap, "A1");

        }
    }
}
