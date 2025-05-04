using System;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_ICS
    {
        public string Azonosító { get; private set; }
        public DateTime Takarítás { get; private set; }
        public int E2 { get; private set; }
        public int E3 { get; private set; }

        public Adat_ICS(string azonosító, DateTime takarítás, int e2, int e3)
        {
            Azonosító = azonosító;
            Takarítás = takarítás;
            E2 = e2;
            E3 = e3;
        }
    }

}
