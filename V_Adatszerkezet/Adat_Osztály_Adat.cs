using System.Collections.Generic;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_Osztály_Adat
    {
        public string Azonosító { get; private set; }
        public List<string> Adatok { get; private set; }
        public List<string> Mezőnév { get; private set; }
        public string Telephely { get; private set; }
        public string Típus { get; private set; }

        public Adat_Osztály_Adat(string azonosító, List<string> adatok, List<string> mezőnév)
        {
            Azonosító = azonosító;
            Adatok = adatok;
            Mezőnév = mezőnév;
        }

        public Adat_Osztály_Adat(string azonosító, List<string> adatok, List<string> mezőnév, string telephely, string típus)
        {
            Azonosító = azonosító;
            Adatok = adatok;
            Mezőnév = mezőnév;
            Telephely = telephely;
            Típus = típus;
        }
    }

    public class Adat_Osztály_Név
    {
        public int Id { get; private set; }
        public string Osztálynév { get; private set; }
        public string Osztálymező { get; private set; }
        public bool Használatban { get; private set; }

        public Adat_Osztály_Név(int id, string osztálynév, string osztálymező, bool használatban)
        {
            Id = id;
            Osztálynév = osztálynév;
            Osztálymező = osztálymező;
            Használatban = használatban;
        }
    }
}
