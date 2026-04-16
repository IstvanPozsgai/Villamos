using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kerék_Eszterga
    {
        public string Azonosító { get; private set; }
        public DateTime Eszterga { get; private set; }
        public string Módosító { get; private set; }
        public DateTime Mikor { get; private set; }
        public long KMU { get; private set; }

        public Adat_Kerék_Eszterga(string azonosító, DateTime eszterga, string módosító, DateTime mikor, long kmu)
        {
            Azonosító = azonosító;
            Eszterga = eszterga;
            Módosító = módosító;
            Mikor = mikor;
            KMU = kmu;
        }
    }
}
