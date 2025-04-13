using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_Jármű_2
    {
        public string Azonosító { get; private set; }

        public DateTime Takarítás { get; private set; }
        public int Haromnapos { get; private set; }

        public Adat_Jármű_2(string azonosító, DateTime takarítás, int haromnapos)
        {
            Azonosító = azonosító;
            Takarítás = takarítás;
            Haromnapos = haromnapos;
        }

        public Adat_Jármű_2(string azonosító, int haromnapos)
        {
            Azonosító = azonosító;
            Haromnapos = haromnapos;
        }
    }

    public class Adat_Jármű_2ICS
    {
        public string Azonosító { get; private set; }
        public DateTime Takarítás { get; private set; }
        public int E2 { get; private set; }
        public int E3 { get; private set; }

        public Adat_Jármű_2ICS(string azonosító, DateTime takarítás, int e2, int e3)
        {
            Azonosító = azonosító;
            Takarítás = takarítás;
            E2 = e2;
            E3 = e3;
        }
    }
}
