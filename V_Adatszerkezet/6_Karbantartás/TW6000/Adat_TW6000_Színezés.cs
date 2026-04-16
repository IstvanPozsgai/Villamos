using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_TW6000_Színezés
    {
        public double Szín { get; set; }
        public string Vizsgálatnév { get; set; }
        public Adat_TW6000_Színezés(double szín, string vizsgálatnév)
        {
            Szín = szín;
            Vizsgálatnév = vizsgálatnév;
        }
    }
}
