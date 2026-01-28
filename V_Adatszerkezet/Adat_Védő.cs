using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public  class Adat_Védő_Könyv
    {
        public string Szerszámkönyvszám { get;private  set; }
        public string Szerszámkönyvnév { get; private set; }
        public string Felelős1 { get; private set; }
        public bool Státus { get; private set; }

        public Adat_Védő_Könyv(string szerszámkönyvszám, string szerszámkönyvnév, string felelős1, bool státus)
        {
            Szerszámkönyvszám = szerszámkönyvszám;
            Szerszámkönyvnév = szerszámkönyvnév;
            Felelős1 = felelős1;
            Státus = státus;
        }
    }

    public class Adat_Védő_Cikktörzs 
    {
        public string Azonosító { get; private set; }
        public string Megnevezés { get; private set; }
        public string Méret { get; private set; }
        public int Státus { get; private set; }

        public string Költséghely { get; private set; }
        public string Védelem { get; private set; }
        public string Kockázat { get; private set; }

        public string Szabvány { get; private set; }
        public string Szint { get; private set; }  
        public string Munk_megnevezés { get; private set; }

        public Adat_Védő_Cikktörzs(string azonosító, string megnevezés, string méret, int státus, string költséghely, string védelem, string kockázat, string szabvány, string szint, string munk_megnevezés)
        {
            Azonosító = azonosító;
            Megnevezés = megnevezés;
            Méret = méret;
            Státus = státus;
            Költséghely = költséghely;
            Védelem = védelem;
            Kockázat = kockázat;
            Szabvány = szabvány;
            Szint = szint;
            Munk_megnevezés = munk_megnevezés;
        }
    }

    public class Adat_Védő_Könyvelés 
    {
        public string Azonosító { get; private set; }
        public string Szerszámkönyvszám { get; private set; }
        public double Mennyiség { get; private set; }

        public string Gyáriszám { get; private set; }
        public DateTime Dátum { get; private set; }
        public bool Státus { get; private set; }

        public Adat_Védő_Könyvelés(string azonosító, string szerszámkönyvszám, double mennyiség, string gyáriszám, DateTime dátum, bool státus)
        {
            Azonosító = azonosító;
            Szerszámkönyvszám = szerszámkönyvszám;
            Mennyiség = mennyiség;
            Gyáriszám = gyáriszám;
            Dátum = dátum;
            Státus = státus;
        }
    }


    public class Adat_Védő_Napló 
    {
        public string Azonosító { get; private set; }
        public string Honnan { get; private set; }
        public string Hova { get; private set; }
        public double Mennyiség { get; private set; }
        public string Gyáriszám { get; private set; }
        public string Módosította { get; private set; }
        public DateTime  Módosításidátum { get; private set; }
        public bool Státus { get; private set; }

        public Adat_Védő_Napló(string azonosító, string honnan, string hova, double mennyiség, string gyáriszám, string módosította, DateTime módosításidátum, bool státus)
        {
            Azonosító = azonosító;
            Honnan = honnan;
            Hova = hova;
            Mennyiség = mennyiség;
            Gyáriszám = gyáriszám;
            Módosította = módosította;
            Módosításidátum = módosításidátum;
            Státus = státus;
        }
    }
}
