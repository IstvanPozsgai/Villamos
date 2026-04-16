using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kerék_Mérés
    {
        public string Azonosító { get; private set; }
        public string Pozíció { get; private set; }
        public string Kerékberendezés { get; private set; }
        public string Kerékgyártásiszám { get; private set; }
        public string Állapot { get; private set; }
        public int Méret { get; private set; }
        public string Módosító { get; private set; }
        public DateTime Mikor { get; private set; }
        public string Oka { get; private set; }
        public int SAP { get; private set; }

        public Adat_Kerék_Mérés(string azonosító, string pozíció, string kerékberendezés, string kerékgyártásiszám,
            string állapot, int méret, string módosító, DateTime mikor, string oka, int sAP)
        {
            Azonosító = azonosító;
            Pozíció = pozíció;
            Kerékberendezés = kerékberendezés;
            Kerékgyártásiszám = kerékgyártásiszám;
            Állapot = állapot;
            Méret = méret;
            Módosító = módosító;
            Mikor = mikor;
            Oka = oka;
            SAP = sAP;
        }

        public Adat_Kerék_Mérés(string kerékberendezés, DateTime mikor, int sAP)
        {
            Kerékberendezés = kerékberendezés;
            Mikor = mikor;
            SAP = sAP;
        }
    }
}
