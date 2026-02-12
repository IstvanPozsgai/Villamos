using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Épület_Naptár
    {
        public bool Előterv { get; private set; }
        public int Hónap { get; private set; }
        public bool Igazolás { get; private set; }
        public string Napok { get; private set; }

        public Adat_Épület_Naptár(bool előterv, int hónap, bool igazolás, string napok)
        {
            Előterv = előterv;
            Hónap = hónap;
            Igazolás = igazolás;
            Napok = napok;
        }
    }

}
