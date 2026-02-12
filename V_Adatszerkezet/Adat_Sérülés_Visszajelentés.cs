using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Sérülés_Visszajelentés
    {
        public string Visszaszám { get; set; }
        public int Munkaidő { get; set; }
        public string Storno { get; set; }
        public int Rendelés { get; set; }
        public string Teljesítményfajta { get; set; }

        public Adat_Sérülés_Visszajelentés(string visszaszám, int munkaidő, string storno, int rendelés, string teljesítményfajta)
        {
            Visszaszám = visszaszám;
            Munkaidő = munkaidő;
            Storno = storno;
            Rendelés = rendelés;
            Teljesítményfajta = teljesítményfajta;
        }
    }
}
