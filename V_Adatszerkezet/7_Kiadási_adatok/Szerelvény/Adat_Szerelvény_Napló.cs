using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Szerelvény_Napló
    {
        public long ID { get; private set; }
        public long Szerelvényhossz { get; private set; }
        public string Kocsi1 { get; private set; }
        public string Kocsi2 { get; private set; }
        public string Kocsi3 { get; private set; }
        public string Kocsi4 { get; private set; }
        public string Kocsi5 { get; private set; }
        public string Kocsi6 { get; private set; }

        public string Módosító { get; private set; }

        public DateTime Mikor { get; private set; }

        public Adat_Szerelvény_Napló(long id, long szerelvényhossz, string kocsi1, string kocsi2, string kocsi3, string kocsi4, string kocsi5, string kocsi6, string módosító, DateTime mikor)
        {
            ID = id;
            Szerelvényhossz = szerelvényhossz;
            Kocsi1 = kocsi1;
            Kocsi2 = kocsi2;
            Kocsi3 = kocsi3;
            Kocsi4 = kocsi4;
            Kocsi5 = kocsi5;
            Kocsi6 = kocsi6;
            Módosító = módosító;
            Mikor = mikor;
        }

        public Adat_Szerelvény_Napló() { }
    }
}
