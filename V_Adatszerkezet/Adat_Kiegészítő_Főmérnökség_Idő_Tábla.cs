using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kiegészítő_Idő_Tábla
    {
        public long Sorszám { get; private set; }
        public DateTime Reggel { get; private set; }
        public DateTime Este { get; private set; }
        public DateTime Délután { get; private set; }

        public Adat_Kiegészítő_Idő_Tábla(long sorszám, DateTime reggel, DateTime este, DateTime délután)
        {
            Sorszám = sorszám;
            Reggel = reggel;
            Este = este;
            Délután = délután;
        }


    }
}
