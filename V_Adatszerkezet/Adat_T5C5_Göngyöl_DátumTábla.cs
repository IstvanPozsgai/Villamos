using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_T5C5_Göngyöl_DátumTábla
    {

        public string Telephely { get; private set; }
        public DateTime Utolsórögzítés { get; set; }

        public bool Zárol { get; private set; }

        public Adat_T5C5_Göngyöl_DátumTábla(string telephely, DateTime utolsórögzítés, bool zárol)
        {
            Telephely = telephely;
            Utolsórögzítés = utolsórögzítés;
            Zárol = zárol;
        }
    }
}
