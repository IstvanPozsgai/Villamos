using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Munka_Folyamat
    {
        public long ID { get; private set; }
        public string Rendelésiszám { get; private set; }
        public string Azonosító { get; private set; }
        public string Munkafolyamat { get; private set; }
        public bool Látszódik { get; private set; }

        public Adat_Munka_Folyamat(long iD, string rendelésiszám, string azonosító, string munkafolyamat, bool látszódik)
        {
            ID = iD;
            Rendelésiszám = rendelésiszám;
            Azonosító = azonosító;
            Munkafolyamat = munkafolyamat;
            Látszódik = látszódik;
        }
    }
}
