using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_TW6000_Ütemezés_Plusz
    {
        public string Azonosító { get; private set; }
        public string Ciklusrend { get; private set; }
        public bool Elkészült { get; private set; }
        public string Megjegyzés { get; private set; }
        public long Státus { get; private set; }
        public DateTime Velkészülés { get; private set; }
        public DateTime Vesedékesség { get; private set; }
        public string Vizsgfoka { get; private set; }
        public long Vsorszám { get; private set; }
        public DateTime Vütemezés { get; private set; }
        public string Vvégezte { get; private set; }
        public string Telephely { get; private set; }

        public Adat_TW6000_Ütemezés_Plusz(string azonosító, string ciklusrend, bool elkészült, string megjegyzés, long státus, DateTime velkészülés, DateTime vesedékesség, string vizsgfoka, long vsorszám, DateTime vütemezés, string vvégezte, string telephely)
        {
            Azonosító = azonosító;
            Ciklusrend = ciklusrend;
            Elkészült = elkészült;
            Megjegyzés = megjegyzés;
            Státus = státus;
            Velkészülés = velkészülés;
            Vesedékesség = vesedékesség;
            Vizsgfoka = vizsgfoka;
            Vsorszám = vsorszám;
            Vütemezés = vütemezés;
            Vvégezte = vvégezte;
            Telephely = telephely;
        }
    }
}
