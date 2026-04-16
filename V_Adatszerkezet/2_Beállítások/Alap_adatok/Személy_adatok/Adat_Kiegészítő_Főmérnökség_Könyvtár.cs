using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kiegészítő_Könyvtár
    {
        public int ID { get; private set; }
        public string Név { get; private set; }
        public bool Vezér1 { get; private set; }
        public int Csoport1 { get; private set; }
        public int Csoport2 { get; private set; }
        public bool Vezér2 { get; private set; }
        public int Sorrend1 { get; private set; }
        public int Sorrend2 { get; private set; }

        public Adat_Kiegészítő_Könyvtár(int iD, string név, bool vezér1, int csoport1, int csoport2, bool vezér2, int sorrend1, int sorrend2)
        {
            ID = iD;
            Név = név;
            Vezér1 = vezér1;
            Csoport1 = csoport1;
            Csoport2 = csoport2;
            Vezér2 = vezér2;
            Sorrend1 = sorrend1;
            Sorrend2 = sorrend2;
        }
    }
}
