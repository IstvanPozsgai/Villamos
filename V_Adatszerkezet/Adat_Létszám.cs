using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Létszám_Elrendezés_Változatok
    {
        public int Id { get; private set; }
        public string Változatnév { get; private set; }
        public string Csoportnév { get; private set; }
        public string Oszlop { get; private set; }
        public int Sor { get; private set; }
        public int Szélesség { get; private set; }

        public Adat_Létszám_Elrendezés_Változatok(int id, string változatnév, string csoportnév, string oszlop, int sor, int szélesség)
        {
            Id = id;
            Változatnév = változatnév;
            Csoportnév = csoportnév;
            Oszlop = oszlop;
            Sor = sor;
            Szélesség = szélesség;
        }
    }
}
