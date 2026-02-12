using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
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

        public Adat_Váltós_Összesítő(string csoport, long perc, DateTime dátum)
        {
            Csoport = csoport;
            Perc = perc;
            Dátum = dátum;
        }

    }
}
