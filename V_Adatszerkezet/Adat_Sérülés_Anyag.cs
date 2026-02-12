using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Sérülés_Anyag
    {
        public string Cikkszám { get; private set; }
        public string Anyagnév { get; private set; }
        public double Mennyiség { get; private set; }
        public string Me { get; private set; }
        public double Ár { get; private set; }
        public string Állapot { get; private set; }
        public double Rendelés { get; private set; }
        public string Mozgásnem { get; private set; }

        public Adat_Sérülés_Anyag(string cikkszám, string anyagnév, double mennyiség, string me, double ár, string állapot, double rendelés, string mozgásnem)
        {
            Cikkszám = cikkszám;
            Anyagnév = anyagnév;
            Mennyiség = mennyiség;
            Me = me;
            Ár = ár;
            Állapot = állapot;
            Rendelés = rendelés;
            Mozgásnem = mozgásnem;
        }
    }
}
