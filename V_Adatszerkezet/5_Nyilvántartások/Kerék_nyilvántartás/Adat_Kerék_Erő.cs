using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kerék_Erő
    {
        public string Azonosító { get; private set; }
        public string Van { get; private set; }
        public string Módosító { get; private set; }
        public DateTime Mikor { get; private set; }

        public Adat_Kerék_Erő(string azonosító, string van, string módosító, DateTime mikor)
        {
            Azonosító = azonosító;
            Van = van;
            Módosító = módosító;
            Mikor = mikor;
        }
    }
}
