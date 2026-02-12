using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Munka_Rendelés
    {
        public long ID { get; private set; }

        public string Megnevezés { get; private set; }
        public string Műveletet { get; private set; }
        public string Pályaszám { get; private set; }
        public string Rendelés { get; private set; }

        public Adat_Munka_Rendelés(long iD, string megnevezés, string műveletet, string pályaszám, string rendelés)
        {
            ID = iD;
            Megnevezés = megnevezés;
            Műveletet = műveletet;
            Pályaszám = pályaszám;
            Rendelés = rendelés;
        }
    }
}
