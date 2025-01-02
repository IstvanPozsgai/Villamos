using System;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_Váltós_Naptár
    {
        public string Nap { get; private set; }
        public DateTime Dátum { get; private set; }

        public Adat_Váltós_Naptár(string nap, DateTime dátum)
        {
            Nap = nap;
            Dátum = dátum;
        }
    }

    public class Adat_Váltós_Összesítő
    {
        public long Perc { get; private set; }
        public DateTime Dátum { get; private set; }
        public string Csoport { get; private set; }

        public Adat_Váltós_Összesítő(long perc, DateTime dátum)
        {
            Perc = perc;
            Dátum = dátum;
        }

        public Adat_Váltós_Összesítő(long perc, DateTime dátum, string csoport) : this(perc, dátum)
        {
            Csoport = csoport;
        }
    }


    public class Adat_Váltós_Kijelöltnapok
    {
        public string Telephely { get; private set; }
        public string Csoport { get; private set; }
        public DateTime Dátum { get; private set; }

        public Adat_Váltós_Kijelöltnapok(string telephely, string csoport, DateTime dátum)
        {
            Telephely = telephely;
            Csoport = csoport;
            Dátum = dátum;
        }
    }
  
    
    public class Adat_Váltós_Váltóstábla
    {
        public string Telephely { get; private set; }
        public string Csoport { get; private set; }
        public int Év { get; private set; }
        public int Félév { get; private set; }
        public double Zknap { get; private set; }
        public double Epnap { get; private set; }
        public double Tperc { get; private set; }

        public Adat_Váltós_Váltóstábla(string telephely, string csoport, int év, int félév, double zknap, double epnap, double tperc)
        {
            Telephely = telephely;
            Csoport = csoport;
            Év = év;
            Félév = félév;
            Zknap = zknap;
            Epnap = epnap;
            Tperc = tperc;
        }
    }
   
    public class Adat_Váltós_Váltóscsopitábla
    {
        public string Csoport { get; private set; }
        public string Telephely { get; private set; }
        public string Név { get; private set; }

        public Adat_Váltós_Váltóscsopitábla(string csoport, string telephely, string név)
        {
            Csoport = csoport;
            Telephely = telephely;
            Név = név;
        }
    }

}
