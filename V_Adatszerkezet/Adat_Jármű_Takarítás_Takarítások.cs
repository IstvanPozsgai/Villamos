using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Jármű_Takarítás_Takarítások
    {
        //takarítások tábla
        public string Azonosító { get; private set; }
        public DateTime Dátum { get; private set; }
        public string Takarítási_fajta { get; private set; }
        public string Telephely { get; private set; }

        public int Státus { get; private set; }

        public Adat_Jármű_Takarítás_Takarítások(string azonosító, DateTime dátum, string takarítási_fajta, string telephely, int státus)
        {
            Azonosító = azonosító;
            Dátum = dátum;
            Takarítási_fajta = takarítási_fajta;
            Telephely = telephely;
            Státus = státus;
        }

    }
}
