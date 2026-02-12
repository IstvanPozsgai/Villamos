using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_DigitálisMunkalap_Kocsik
    {
        public long Fej_Id { get; private set; }

        public string Azonosító { get; private set; }

        public long KMU { get; private set; }
        public string Rendelés { get; private set; }

        public Adat_DigitálisMunkalap_Kocsik(long fej_Id, string azonosító, long kMU, string rendelés)
        {
            Fej_Id = fej_Id;
            Azonosító = azonosító;
            KMU = kMU;
            Rendelés = rendelés;
        }
    }
}
