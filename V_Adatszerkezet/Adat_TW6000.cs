using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_TW6000_Alap
    {
        public string Azonosító { get; set; }
        public string Ciklusrend { get; set; }
        public Boolean Kötöttstart { get; set; }
        public Boolean Megállítás { get; set; }
        public DateTime Start { get; set; }
        public DateTime Vizsgdátum { get; set; }
        public string Vizsgnév { get; set; }
        public int Vizsgsorszám { get; set; }

        public Adat_TW6000_Alap(string azonosító, string ciklusrend, bool kötöttstart, bool megállítás, DateTime start, DateTime vizsgdátum, string vizsgnév, int vizsgsorszám)
        {
            Azonosító = azonosító;
            Ciklusrend = ciklusrend;
            Kötöttstart = kötöttstart;
            Megállítás = megállítás;
            Start = start;
            Vizsgdátum = vizsgdátum;
            Vizsgnév = vizsgnév;
            Vizsgsorszám = vizsgsorszám;
        }
    }

}
