using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Takarítás_Telep_Opció
    {
        public int Id { get; private set; }
        public DateTime Dátum { get; private set; }
        public double Megrendelt { get; private set; }
        public double Teljesített { get; private set; }

        public Adat_Takarítás_Telep_Opció(int id, DateTime dátum, double megrendelt, double teljesített)
        {
            Id = id;
            Dátum = dátum;
            Megrendelt = megrendelt;
            Teljesített = teljesített;
        }
    }
}
