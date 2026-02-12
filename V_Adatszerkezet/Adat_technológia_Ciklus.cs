using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_technológia_Ciklus
    {

        public int Sorszám { get; private set; }
        public string Fokozat { get; private set; }
        public int Csoportos { get; private set; }
        public string Elérés { get; private set; }
        public string Verzió { get; private set; }

        public Adat_technológia_Ciklus(int sorszám, string fokozat, int csoportos, string elérés, string verzió)
        {
            Sorszám = sorszám;
            Fokozat = fokozat;
            Csoportos = csoportos;
            Elérés = elérés;
            Verzió = verzió;
        }

        public Adat_technológia_Ciklus(int sorszám, string fokozat)
        {
            Sorszám = sorszám;
            Fokozat = fokozat;
        }
    }

}
