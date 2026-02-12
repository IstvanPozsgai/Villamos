using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
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
