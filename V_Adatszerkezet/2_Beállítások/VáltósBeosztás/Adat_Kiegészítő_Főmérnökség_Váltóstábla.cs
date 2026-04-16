using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kiegészítő_Váltóstábla
    {
        public int Id { get; private set; }
        public DateTime Kezdődátum { get; private set; }
        public int Ciklusnap { get; private set; }
        public string Megnevezés { get; private set; }
        public string Csoport { get; private set; }

        public Adat_Kiegészítő_Váltóstábla(int id, DateTime kezdődátum, int ciklusnap, string megnevezés, string csoport)
        {
            Id = id;
            Kezdődátum = kezdődátum;
            Ciklusnap = ciklusnap;
            Megnevezés = megnevezés;
            Csoport = csoport;
        }
    }
}
