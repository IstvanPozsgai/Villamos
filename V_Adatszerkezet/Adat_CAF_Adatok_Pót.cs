using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_CAF_Adatok_Pót
    {
        public string Azonosító { get; private set; }
        public DateTime Dátum { get; private set; }
        public DateTime Dátumtól { get; private set; }
        public DateTime Dátumig { get; private set; }
        public int Státus { get; private set; }

        public Adat_CAF_Adatok_Pót(string azonosító, DateTime dátum, int státus)
        {
            Azonosító = azonosító;
            Dátum = dátum;
            Státus = státus;
        }

        public Adat_CAF_Adatok_Pót(string azonosító, DateTime dátumtól, DateTime dátumig, int státus)
        {
            Azonosító = azonosító;
            Dátumtól = dátumtól;
            Dátumig = dátumig;
            Státus = státus;
        }

    }
}
