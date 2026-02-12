using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kiegészítő_Beosegéd
    {
        public string Beosztáskód { get; private set; }
        public int Túlóra { get; private set; }
        public DateTime Kezdőidő { get; private set; }
        public DateTime Végeidő { get; private set; }
        public string Túlóraoka { get; private set; }
        public string Telephely { get; private set; }

        public Adat_Kiegészítő_Beosegéd(string beosztáskód, int túlóra, DateTime kezdőidő, DateTime végeidő, string túlóraoka, string telephely)
        {
            Beosztáskód = beosztáskód;
            Túlóra = túlóra;
            Kezdőidő = kezdőidő;
            Végeidő = végeidő;
            Túlóraoka = túlóraoka;
            Telephely = telephely;
        }
    }
}
