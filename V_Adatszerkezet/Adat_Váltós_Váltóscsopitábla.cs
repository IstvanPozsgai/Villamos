using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
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
