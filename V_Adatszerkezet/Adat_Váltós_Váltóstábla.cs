using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Váltós_Váltóstábla
    {
        public string Telephely { get; private set; }
        public string Csoport { get; private set; }
        public int Év { get; private set; }
        public int Félév { get; private set; }
        public double Zknap { get; private set; }
        public double Epnap { get; private set; }
        public double Tperc { get; private set; }

        public Adat_Váltós_Váltóstábla(string telephely, string csoport, int év, int félév, double zknap, double epnap, double tperc)
        {
            Telephely = telephely;
            Csoport = csoport;
            Év = év;
            Félév = félév;
            Zknap = zknap;
            Epnap = epnap;
            Tperc = tperc;
        }
    }
}
