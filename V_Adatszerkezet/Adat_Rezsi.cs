using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Villamos_Adatszerkezet
{
    public  class Adat_Rezsi_Törzs
    {
        public string Azonosító { get;private set; }
        public string Megnevezés { get; set; }
        public string Méret { get; set; }
        public int Státusz { get; set; }
        public string Csoport { get; set; }

        public Adat_Rezsi_Törzs(string azonositó, string megnevezés, string méret, int státusz, string csoport)
        {
            Azonosító = azonositó;
            Megnevezés = megnevezés;
            Méret = méret;
            Státusz = státusz;
            Csoport = csoport;
        }   
    }


    public class Adat_Rezsi_Hely
    {
        public string Azonosító { get; private set; }
        public string Állvány { get; private set; }
        public string Polc { get; private set; }
        public string Helyiség { get; private set; }
        public string Megjegyzés { get; private set; }

        public Adat_Rezsi_Hely(string azonosító, string állvány, string polc, string helyiség, string megjegyzés)
        {
            Azonosító = azonosító;
            Állvány = állvány;
            Polc = polc;
            Helyiség = helyiség;
            Megjegyzés = megjegyzés;      
        }
    }


    public class Adat_Rezsi_Listanapló
    {
        public string Azonosító { get; private set; }
        public string Honnan { get; private set; }
        public string Hova { get; private set; }
        public string Mennyiség { get; private set; }
        public string Mirehasznál { get; private set; }
        public string Módosította { get; private set; }
        public DateTime Módosításidátum { get; private set; }
        public bool Státus { get; private set; }

        public Adat_Rezsi_Listanapló(string azonosító, string honnan, string hova, string mennyiség, string mirehasznál, string módosította, DateTime módosításidátum, bool státus)
        {
            Azonosító = azonosító;
            Honnan = honnan;
            Hova = hova;
            Mennyiség = mennyiség;
            Mirehasznál = mirehasznál;
            Módosította = módosította;
            Módosításidátum = módosításidátum;
            Státus = státus;
        }
    }


    public class Adat_Rezsi_Lista
    {
        public string Azonosító { get; private set; }
        public string Mennyiség { get; private set; }
        public DateTime Dátum { get; private set; }
        public bool Státus { get; private set; }

        public Adat_Rezsi_Lista(string azonosító, string mennyiség, DateTime dátum, bool státus)
        {
            Azonosító = azonosító;
            Mennyiség = mennyiség;
            Dátum = dátum;
            Státus = státus;
        }
    }

}
