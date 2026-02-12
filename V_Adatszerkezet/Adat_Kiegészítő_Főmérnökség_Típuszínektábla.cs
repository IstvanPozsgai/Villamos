using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kiegészítő_Típuszínektábla
    {
        public string Típus { get; private set; }
        public long Színszám { get; private set; }

        public Adat_Kiegészítő_Típuszínektábla(string típus, long színszám)
        {
            Típus = típus;
            Színszám = színszám;
        }
    }
}
