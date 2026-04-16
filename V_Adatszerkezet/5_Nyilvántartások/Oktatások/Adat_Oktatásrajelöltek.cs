using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Oktatásrajelöltek
    {
        public string HRazonosító { get; private set; }
        public long IDoktatás { get; private set; }
        public DateTime Mikortól { get; private set; }
        public long Státus { get; private set; }
        public string Telephely { get; private set; }

        public Adat_Oktatásrajelöltek(string hRazonosító, long iDoktatás, DateTime mikortól, long státus, string telephely)
        {
            HRazonosító = hRazonosító;
            IDoktatás = iDoktatás;
            Mikortól = mikortól;
            Státus = státus;
            Telephely = telephely;
        }
    }
}
