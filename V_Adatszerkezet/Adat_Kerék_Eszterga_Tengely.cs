using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kerék_Eszterga_Tengely
    {
        public string Típus { get; private set; }

        public int Munkaidő { get; private set; }

        public int Állapot { get; private set; }

        public Adat_Kerék_Eszterga_Tengely(string típus, int munkaidő, int állapot)
        {
            Típus = típus;
            Munkaidő = munkaidő;
            Állapot = állapot;
        }
    }
}
