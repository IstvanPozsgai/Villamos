using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_TW6000_ÜtemNapló
    {
        public string Azonosító { get; private set; }
        public string Ciklusrend { get; private set; }
        public bool Elkészült { get; private set; }
        public string Megjegyzés { get; private set; }
        public DateTime Rögzítésideje { get; private set; }
        public string Rögzítő { get; set; }
        public long Státus { get; set; }
        public DateTime Velkészülés { get; private set; }
        public DateTime Vesedékesség { get; private set; }
        public string Vizsgfoka { get; private set; }
        public long Vsorszám { get; private set; }
        public DateTime Vütemezés { get; private set; }
        public string Vvégezte { get; private set; }

        public Adat_TW6000_ÜtemNapló(string azonosító, string ciklusrend, bool elkészült, string megjegyzés, DateTime rögzítésideje, string rögzítő, long státus, DateTime velkészülés, DateTime vesedékesség, string vizsgfoka, long vsorszám, DateTime vütemezés, string vvégezte)
        {
            Azonosító = azonosító;
            Ciklusrend = ciklusrend;
            Elkészült = elkészült;
            Megjegyzés = megjegyzés;
            Rögzítésideje = rögzítésideje;
            Rögzítő = rögzítő;
            Státus = státus;
            Velkészülés = velkészülés;
            Vesedékesség = vesedékesség;
            Vizsgfoka = vizsgfoka;
            Vsorszám = vsorszám;
            Vütemezés = vütemezés;
            Vvégezte = vvégezte;
        }
    }
}
