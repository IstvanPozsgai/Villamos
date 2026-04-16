using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Védő_Cikktörzs
    {
        public string Azonosító { get; private set; }
        public string Megnevezés { get; private set; }
        public string Méret { get; private set; }
        public int Státus { get; private set; }

        public string Költséghely { get; private set; }
        public string Védelem { get; private set; }
        public string Kockázat { get; private set; }

        public string Szabvány { get; private set; }
        public string Szint { get; private set; }
        public string Munk_megnevezés { get; private set; }

        public Adat_Védő_Cikktörzs(string azonosító, string megnevezés, string méret, int státus, string költséghely, string védelem, string kockázat, string szabvány, string szint, string munk_megnevezés)
        {
            Azonosító = azonosító;
            Megnevezés = megnevezés;
            Méret = méret;
            Státus = státus;
            Költséghely = költséghely;
            Védelem = védelem;
            Kockázat = kockázat;
            Szabvány = szabvány;
            Szint = szint;
            Munk_megnevezés = munk_megnevezés;
        }
    }
}
