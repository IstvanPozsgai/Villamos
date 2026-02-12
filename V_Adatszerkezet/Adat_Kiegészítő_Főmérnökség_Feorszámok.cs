using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kiegészítő_Feorszámok
    {
        public long Sorszám { get; private set; }
        public string Feorszám { get; private set; }

        public string Feormegnevezés { get; private set; }
        public long Státus { get; private set; }

        public Adat_Kiegészítő_Feorszámok(long sorszám, string feorszám, string feormegnevezés, long státus)
        {
            Sorszám = sorszám;
            Feorszám = feorszám;
            Feormegnevezés = feormegnevezés;
            Státus = státus;
        }
    }
}
