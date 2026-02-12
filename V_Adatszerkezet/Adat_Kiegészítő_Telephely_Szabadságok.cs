using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kiegészítő_Szabadságok
    {

        public long Sorszám { get; private set; }
        public string Megnevezés { get; private set; }

        public Adat_Kiegészítő_Szabadságok(long sorszám, string megnevezés)
        {
            Sorszám = sorszám;
            Megnevezés = megnevezés;
        }
    }
}
