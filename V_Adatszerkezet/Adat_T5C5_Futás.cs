using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_T5C5_Futás
    {
        public string Azonosító { get; private set; }
        public DateTime Dátum { get; private set; }
        public string Futásstátus { get; private set; }
        public long Státus { get; private set; }

        public Adat_T5C5_Futás(string azonosító, DateTime dátum, string futásstátus, long státus)
        {
            Azonosító = azonosító;
            Dátum = dátum;
            Futásstátus = futásstátus;
            Státus = státus;
        }
    }
}
