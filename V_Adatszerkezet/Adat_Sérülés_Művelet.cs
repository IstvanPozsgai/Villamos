using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Sérülés_Művelet
    {
        public string Teljesítményfajta { get; set; }
        public int Rendelés { get; set; }
        public string Visszaszám { get; set; }
        public string Műveletszöveg { get; set; }

        public Adat_Sérülés_Művelet(string teljesítményfajta, int rendelés, string visszaszám, string műveletszöveg)
        {
            Teljesítményfajta = teljesítményfajta;
            Rendelés = rendelés;
            Visszaszám = visszaszám;
            Műveletszöveg = műveletszöveg;
        }
    }
}
