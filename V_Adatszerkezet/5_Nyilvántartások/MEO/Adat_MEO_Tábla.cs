using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_MEO_Tábla
    {
        public string Név { get; set; }
        public string Típus { get; set; }

        public Adat_MEO_Tábla(string név, string típus)
        {
            Név = név;
            Típus = típus;
        }
    }
}
