using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_T5C5_Posta
    {
        public string Azonosító { get; private set; }
        public string Típus { get; private set; }
        public string Csatolható { get; private set; }
        public int V_Sorszám { get; private set; }
        public string V2_következő { get; private set; }
        public int V2_Futott_Km { get; private set; }
        public string V_Következő { get; private set; }
        public int V_futott_Km { get; private set; }
        public int Napszám { get; private set; }
        public string Terv_Nap { get; private set; }
        public string Hiba { get; private set; }
        public string Előírt_szerelvény { get; private set; }
        public string Tényleges_szerelvény { get; private set; }
        public string Rendelésszám { get; private set; }
        public long Szerelvényszám { get; private set; }

        public int Státus { get; private set; }
        public int E3_sorszám { get; private set; }
        public int Vizsgál { get; private set; }
        public int Marad { get; private set; }

        public string Kiad { get; private set; }
        public string Vissza { get; private set; }
        public string Vonal { get; private set; }


        public Adat_T5C5_Posta(string azonosító, string típus, string csatolható, int v_Sorszám, string v2_következő, int v2_Futott_Km,
            string v_Következő, int v_futott_Km, int napszám, string terv_Nap, string hiba, string előírt_szerelvény, string tényleges_szerelvény,
            string rendelésszám, long szerelvényszám, int státus, int e3_sorszám, int vizsgál, int marad, string kiad, string vissza, string vonal, bool terv)
        {
            Azonosító = azonosító;
            Típus = típus;
            Csatolható = csatolható;
            V_Sorszám = v_Sorszám;
            V2_következő = v2_következő;
            V2_Futott_Km = v2_Futott_Km;
            V_Következő = v_Következő;
            V_futott_Km = v_futott_Km;
            Napszám = napszám;
            Terv_Nap = terv_Nap;
            Hiba = hiba;
            Előírt_szerelvény = előírt_szerelvény;
            Tényleges_szerelvény = tényleges_szerelvény;
            Rendelésszám = rendelésszám;
            Szerelvényszám = szerelvényszám;
            Státus = státus;
            E3_sorszám = e3_sorszám;
            Vizsgál = vizsgál;
            Marad = marad;
            Kiad = kiad;
            Vissza = vissza;
            Vonal = vonal;

        }
    }
}
