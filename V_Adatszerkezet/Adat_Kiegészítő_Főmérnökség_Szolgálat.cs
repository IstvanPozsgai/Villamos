using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kiegészítő_Szolgálat
    {
        public int Sorszám { get; private set; }
        public string Szolgálatnév { get; private set; }

        public Adat_Kiegészítő_Szolgálat(int sorszám, string szolgálatnév)
        {
            Sorszám = sorszám;
            Szolgálatnév = szolgálatnév;
        }
    }
}
