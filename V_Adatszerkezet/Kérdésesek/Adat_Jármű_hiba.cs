using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Jármű_hiba
    {

        public string Létrehozta { get; private set; }
        public long Korlát { get; private set; }
        public string Hibaleírása { get; private set; }
        public DateTime Idő { get; private set; }
        public bool Javítva { get; private set; }
        public string Típus { get; private set; }
        public string Azonosító { get; private set; }
        public long Hibáksorszáma { get; private set; }

        public Adat_Jármű_hiba(string létrehozta, long korlát, string hibaleírása, DateTime idő, bool javítva, string típus, string azonosító, long hibáksorszáma)
        {
            Létrehozta = létrehozta;
            Korlát = korlát;
            Hibaleírása = hibaleírása;
            Idő = idő;
            Javítva = javítva;
            Típus = típus;
            Azonosító = azonosító;
            Hibáksorszáma = hibáksorszáma;
        }
    }
}
