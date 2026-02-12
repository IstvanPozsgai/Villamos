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

}
