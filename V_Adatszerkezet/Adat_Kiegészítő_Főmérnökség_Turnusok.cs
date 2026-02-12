using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kiegészítő_Turnusok
    {
        public string Csoport { get; private set; }

        public Adat_Kiegészítő_Turnusok(string csoport)
        {
            Csoport = csoport;
        }
    }
}
