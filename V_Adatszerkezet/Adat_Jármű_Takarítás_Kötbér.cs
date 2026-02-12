using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Jármű_Takarítás_Kötbér
    {
        public string Takarítási_fajta { get; private set; }
        public string NemMegfelel { get; private set; }
        public string Póthatáridő { get; private set; }

        public Adat_Jármű_Takarítás_Kötbér(string takarítási_fajta, string nemMegfelel, string póthatáridő)
        {
            Takarítási_fajta = takarítási_fajta;
            NemMegfelel = nemMegfelel;
            Póthatáridő = póthatáridő;
        }
    }
}
