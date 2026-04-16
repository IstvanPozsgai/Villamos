using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kiegészítő_Munkaidő
    {
        public string Munkarendelnevezés { get; private set; }

        public double Munkaidő { get; private set; }

        public Adat_Kiegészítő_Munkaidő(string munkarendelnevezés, double munkaidő)
        {
            Munkarendelnevezés = munkarendelnevezés;
            Munkaidő = munkaidő;
        }
    }
}
