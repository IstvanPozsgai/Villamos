using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Jármű_Xnapos
    {
        public DateTime Kezdődátum { get; private set; }
        public DateTime Végdátum { get; private set; }
        public string Azonosító { get; private set; }

        public string Hibaleírása { get; private set; }

        public Adat_Jármű_Xnapos(DateTime kezdődátum, DateTime végdátum, string azonosító, string hibaleírása)
        {
            Kezdődátum = kezdődátum;
            Végdátum = végdátum;
            Azonosító = azonosító;
            Hibaleírása = hibaleírása;
        }
    }
}
