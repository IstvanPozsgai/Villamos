using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Jármű_Javításiátfutástábla
    {
        public DateTime Kezdődátum { get; private set; }
        public DateTime Végdátum { get; private set; }
        public string Azonosító { get; private set; }
        public string Hibaleírása { get; private set; }

        public Adat_Jármű_Javításiátfutástábla(DateTime kezdődátum, DateTime végdátum, string azonosító, string hibaleírása)
        {
            Kezdődátum = kezdődátum;
            Végdátum = végdátum;
            Azonosító = azonosító;
            Hibaleírása = hibaleírása;
        }

        public Adat_Jármű_Javításiátfutástábla(string azonosító, string hibaleírása)
        {
            Azonosító = azonosító;
            Hibaleírása = hibaleírása;
        }
    }
}
