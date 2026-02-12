using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kiegészítő_Fortetípus
    {
        public long Sorszám { get; private set; }
        public string Ftípus { get; private set; }
        public string Telephely { get; private set; }
        public string Telephelyitípus { get; private set; }

        public Adat_Kiegészítő_Fortetípus(long sorszám, string ftípus, string telephely, string telephelyitípus)
        {
            Sorszám = sorszám;
            Ftípus = ftípus;
            Telephely = telephely;
            Telephelyitípus = telephelyitípus;
        }
    }
}
