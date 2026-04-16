using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_Jármű_Takarítás_Vezénylés
    {
        public long Id { get; private set; }
        public string Azonosító { get; private set; }
        public DateTime Dátum { get; private set; }
        public string Takarítási_fajta { get; private set; }
        public long Szerelvényszám { get; private set; }
        public int Státus { get; private set; }

        public Adat_Jármű_Takarítás_Vezénylés(long id, string azonosító, DateTime dátum, string takarítási_fajta, long szerelvényszám, int státus)
        {
            Id = id;
            Azonosító = azonosító;
            Dátum = dátum;
            Takarítási_fajta = takarítási_fajta;
            Szerelvényszám = szerelvényszám;
            Státus = státus;
        }
    }
}
