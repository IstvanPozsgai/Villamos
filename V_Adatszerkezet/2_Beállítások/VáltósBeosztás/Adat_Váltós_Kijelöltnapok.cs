using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
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
}
