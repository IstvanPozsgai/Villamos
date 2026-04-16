using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kiegészítő_Főkategóriatábla
    {
        public long Sorszám { get; private set; }
        public string Főkategória { get; private set; }

        public Adat_Kiegészítő_Főkategóriatábla(long sorszám, string főkategória)
        {
            Sorszám = sorszám;
            Főkategória = főkategória;
        }
    }
}
