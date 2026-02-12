using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public  class Adat_Védő_Könyv
    {
        public string Szerszámkönyvszám { get;private  set; }
        public string Szerszámkönyvnév { get; private set; }
        public string Felelős1 { get; private set; }
        public bool Státus { get; private set; }

        public Adat_Védő_Könyv(string szerszámkönyvszám, string szerszámkönyvnév, string felelős1, bool státus)
        {
            Szerszámkönyvszám = szerszámkönyvszám;
            Szerszámkönyvnév = szerszámkönyvnév;
            Felelős1 = felelős1;
            Státus = státus;
        }
    }

}
