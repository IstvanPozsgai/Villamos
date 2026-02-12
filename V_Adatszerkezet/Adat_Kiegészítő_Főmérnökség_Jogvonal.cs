using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kiegészítő_Jogvonal
    {
        public long Sorszám { get; private set; }
        public string Szám { get; private set; }
        public string Megnevezés { get; private set; }

        public Adat_Kiegészítő_Jogvonal(long sorszám, string szám, string megnevezés)
        {
            Sorszám = sorszám;
            Szám = szám;
            Megnevezés = megnevezés;
        }
    }
}
