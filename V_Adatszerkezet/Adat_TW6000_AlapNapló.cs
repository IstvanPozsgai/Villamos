using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_TW6000_AlapNapló
    {
        public string Azonosító { get; private set; }
        public string Ciklusrend { get; private set; }
        public bool Kötöttstart { get; private set; }
        public bool Megállítás { get; private set; }
        public string Oka { get; private set; }
        public DateTime Rögzítésiidő { get; private set; }
        public string Rögzítő { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime Vizsgdátum { get; private set; }
        public string Vizsgnév { get; private set; }
        public int Vizsgsorszám { get; set; }
        public Adat_TW6000_AlapNapló(string azonosító, string ciklusrend, bool kötöttstart, bool megállítás, string oka, DateTime rögzítésiidő, string rögzítő, DateTime start, DateTime vizsgdátum, string vizsgnév, int vizsgsorszám)
        {
            Azonosító = azonosító;
            Ciklusrend = ciklusrend;
            Kötöttstart = kötöttstart;
            Megállítás = megállítás;
            Oka = oka;
            Rögzítésiidő = rögzítésiidő;
            Rögzítő = rögzítő;
            Start = start;
            Vizsgdátum = vizsgdátum;
            Vizsgnév = vizsgnév;
            Vizsgsorszám = vizsgsorszám;
        }
    }
}
