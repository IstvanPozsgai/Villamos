using System;

namespace Villamos.Villamos_Adatszerkezet
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


    public class Adat_Kerék_Erő
    {
        public string Azonosító { get; private set; }
        public string Van { get; private set; }
        public string Módosító { get; private set; }
        public DateTime Mikor { get; private set; }

        public Adat_Kerék_Erő(string azonosító, string van, string módosító, DateTime mikor)
        {
            Azonosító = azonosító;
            Van = van;
            Módosító = módosító;
            Mikor = mikor;
        }
    }



    public class Adat_Kerék_Eszterga
    {
        public string Azonosító { get; private set; }
        public DateTime Eszterga { get; private set; }
        public string Módosító { get; private set; }
        public DateTime Mikor { get; private set; }
        public long KMU { get; private set; }

        public Adat_Kerék_Eszterga(string azonosító, DateTime eszterga, string módosító, DateTime mikor, long kmu)
        {
            Azonosító = azonosító;
            Eszterga = eszterga;
            Módosító = módosító;
            Mikor = mikor;
            KMU = kmu;
        }
    }



    public class Adat_Kerék_Tábla
    {
        public string Kerékberendezés { get; private set; }
        public string Kerékmegnevezés { get; private set; }
        public string Kerékgyártásiszám { get; private set; }
        public string Föléberendezés { get; private set; }
        public string Azonosító { get; private set; }
        public string Pozíció { get; private set; }
        public DateTime Dátum { get; private set; }
        public string Objektumfajta { get; private set; }

        public Adat_Kerék_Tábla(string kerékberendezés, string kerékmegnevezés, string kerékgyártásiszám,
            string föléberendezés, string azonosító, string pozíció, DateTime dátum, string objektumfajta)
        {
            Kerékberendezés = kerékberendezés;
            Kerékmegnevezés = kerékmegnevezés;
            Kerékgyártásiszám = kerékgyártásiszám;
            Föléberendezés = föléberendezés;
            Azonosító = azonosító;
            Pozíció = pozíció;
            Dátum = dátum;
            Objektumfajta = objektumfajta;
        }
    }


    public class Adat_Kerék_Eszterga_Beállítás
    {
        public string Azonosító { get; private set; }
        public int KM_lépés { get; private set; }
        public int Idő_lépés { get; private set; }
        public bool KM_IDŐ { get; private set; }
        public DateTime Ütemezve { get; private set; }
        public Adat_Kerék_Eszterga_Beállítás(string azonosító, int kM_lépés, int idő_lépés, bool kM_IDŐ, DateTime ütemezve)
        {
            Azonosító = azonosító;
            KM_lépés = kM_lépés;
            Idő_lépés = idő_lépés;
            KM_IDŐ = kM_IDŐ;
            Ütemezve = ütemezve;
        }
    }
}
