using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Villamos_Adatszerkezet
{
    public  class Adat_Karbantartási
    {
        public string Azonosító { get; private set; }
        public string Álló { get; private set; } 
        public string Beálló { get; private set; }
        public string Szabad { get; private set; }
        public string Típus { get; private set; }
        public DateTime Miótaáll { get; private set; }
        public string Szerelvény { get; private set; }

        public Adat_Karbantartási(string azonosító, string álló, string beálló, string szabad, string típus, DateTime miótaáll, string szerelvény)
        {
            Azonosító = azonosító;
            Álló = álló;
            Beálló = beálló;
            Szabad = szabad;
            Típus = típus;
            Miótaáll = miótaáll;
            Szerelvény = szerelvény;
        }
    }
}
