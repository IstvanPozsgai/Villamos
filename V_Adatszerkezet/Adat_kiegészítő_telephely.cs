using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_kiegészítő_telephely
    {

        public long Sorszám { get; private set; }
        public string Telephelynév { get; private set; }
        public string Telephelykönyvtár { get; private set; }
        public string Fortekód { get; private set; }

        public Adat_kiegészítő_telephely(long sorszám, string telephelynév, string telephelykönyvtár, string fortekód)
        {
            Sorszám = sorszám;
            Telephelynév = telephelynév;
            Telephelykönyvtár = telephelykönyvtár;
            Fortekód = fortekód;
        }
    }

}
