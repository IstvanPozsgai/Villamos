using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Munka_Adatok
    {
        public long ID { get; private set; }
        public int Idő { get; private set; }
        public int SUMIdő { get; private set; }
        public DateTime Dátum { get; private set; }
        public string Megnevezés { get; private set; }
        public string Művelet { get; private set; }
        public string Pályaszám { get; private set; }
        public string Rendelés { get; private set; }
        public bool Státus { get; private set; }

        public Adat_Munka_Adatok(long iD, int idő, DateTime dátum, string megnevezés, string művelet, string pályaszám, string rendelés, bool státus)
        {
            ID = iD;
            Idő = idő;
            Dátum = dátum;
            Megnevezés = megnevezés;
            Művelet = művelet;
            Pályaszám = pályaszám;
            Rendelés = rendelés;
            Státus = státus;
        }

        public Adat_Munka_Adatok(string megnevezés, string művelet, string pályaszám, string rendelés)
        {
            Megnevezés = megnevezés;
            Művelet = művelet;
            Pályaszám = pályaszám;
            Rendelés = rendelés;
        }

        public Adat_Munka_Adatok(int sUMIdő, string művelet, string rendelés)
        {
            SUMIdő = sUMIdő;
            Művelet = művelet;
            Rendelés = rendelés;
        }

        public Adat_Munka_Adatok(int sUMIdő, DateTime dátum, string megnevezés, string művelet, string pályaszám, string rendelés, bool státus)
        {
            SUMIdő = sUMIdő;
            Dátum = dátum;
            Megnevezés = megnevezés;
            Művelet = művelet;
            Pályaszám = pályaszám;
            Rendelés = rendelés;
            Státus = státus;
        }
    }
}
