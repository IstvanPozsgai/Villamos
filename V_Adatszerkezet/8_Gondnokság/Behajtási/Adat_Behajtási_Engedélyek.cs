using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Behajtási_Engedélyek
    {
        public string Telephely { get; set; }
        public int Engedély { get; set; }
        public string Megjegyzés { get; set; }

        public Adat_Behajtási_Engedélyek(string telephely, int engedély, string megjegyzés)
        {
            Telephely = telephely;
            Engedély = engedély;
            Megjegyzés = megjegyzés;
        }
    }
}
