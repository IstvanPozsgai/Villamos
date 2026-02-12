using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Általános_String_Dátum
    {
        public DateTime  Dátum { get; private set; }
        public string Szöveg { get; private set; }

        public Adat_Általános_String_Dátum(DateTime dátum, string szöveg)
        {
            Dátum = dátum;
            Szöveg = szöveg;
        }
    }

}
