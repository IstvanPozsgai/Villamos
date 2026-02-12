using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_Rezsi_Törzs
    {
        public string Azonosító { get; private set; }
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
}
