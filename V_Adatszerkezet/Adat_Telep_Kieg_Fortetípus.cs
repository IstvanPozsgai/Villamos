using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Telep_Kieg_Fortetípus
    {
        public string Típus { get; private set; }

        public string Ftípus { get; private set; }

        public Adat_Telep_Kieg_Fortetípus(string típus, string ftípus)
        {
            Típus = típus;
            Ftípus = ftípus;
        }
    }
}
