using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Technológia_Kivételek
    {
        public long Id { get; private set; }
        public string Azonosító { get; private set; }
        public string Altípus { get; private set; }

        public Adat_Technológia_Kivételek(long id, string azonosító, string altípus)
        {
            Id = id;
            Azonosító = azonosító;
            Altípus = altípus;
        }
    }

}
