using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Sérülés_Ideig
    {
        public int Rendelés { get; set; }
        public int Anyagköltség { get; set; }
        public int Munkaköltség { get; set; }
        public int Gépköltség { get; set; }
        public int Szolgáltatás { get; set; }
        public int Státus { get; set; }

        public Adat_Sérülés_Ideig(int rendelés, int anyagköltség, int munkaköltség, int gépköltség, int szolgáltatás, int státus)
        {
            Rendelés = rendelés;
            Anyagköltség = anyagköltség;
            Munkaköltség = munkaköltség;
            Gépköltség = gépköltség;
            Szolgáltatás = szolgáltatás;
            Státus = státus;
        }
    }
}
