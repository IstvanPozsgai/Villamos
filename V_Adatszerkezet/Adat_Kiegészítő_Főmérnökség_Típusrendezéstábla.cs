using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kiegészítő_Típusrendezéstábla
    {
        public long Sorszám { get; private set; }
        public string Főkategória { get; private set; }
        public string Típus { get; private set; }
        public string AlTípus { get; private set; }
        public string Telephely { get; private set; }
        public string Telephelyitípus { get; private set; }

        public Adat_Kiegészítő_Típusrendezéstábla(long sorszám, string főkategória, string típus, string alTípus, string telephely, string telephelyitípus)
        {
            Sorszám = sorszám;
            Főkategória = főkategória;
            Típus = típus;
            AlTípus = alTípus;
            Telephely = telephely;
            Telephelyitípus = telephelyitípus;
        }
    }
}
