using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kiegészítő_Jogtípus
    {
        public long Sorszám { get; private set; }
        public string Típus { get; private set; }

        public Adat_Kiegészítő_Jogtípus(long sorszám, string típus)
        {
            Sorszám = sorszám;
            Típus = típus;
        }
    }
}
