using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_KerékmérőTengely
    {
        public string Név { get; private set; }

        public string SAP { get; private set; }

        public long Km { get; private set; }

        public Adat_KerékmérőTengely(string név, string sAP)
        {
            Név = név;
            SAP = sAP;
        }

        public Adat_KerékmérőTengely(string név, string sAP, long km) : this(név, sAP)
        {
            Km = km;
        }
    }
}
