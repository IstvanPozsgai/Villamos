using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Rezsi_Hely
    {
        public string Azonosító { get; private set; }
        public string Állvány { get; private set; }
        public string Polc { get; private set; }
        public string Helyiség { get; private set; }
        public string Megjegyzés { get; private set; }

        public Adat_Rezsi_Hely(string azonosító, string állvány, string polc, string helyiség, string megjegyzés)
        {
            Azonosító = azonosító;
            Állvány = állvány;
            Polc = polc;
            Helyiség = helyiség;
            Megjegyzés = megjegyzés;
        }
    }
}
