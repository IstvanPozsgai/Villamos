using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_Szerelvény
    {
        public long Szerelvény_ID { get; private set; }

        public long Szerelvényhossz { get; private set; }
        public string Kocsi1 { get; private set; }
        public string Kocsi2 { get; private set; }
        public string Kocsi3 { get; private set; }
        public string Kocsi4 { get; private set; }
        public string Kocsi5 { get; private set; }
        public string Kocsi6 { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="szerelvény_ID"></param>
        /// <param name="szerelvényhossz"></param>
        /// <param name="kocsi1"></param>
        /// <param name="kocsi2"></param>
        /// <param name="kocsi3"></param>
        /// <param name="kocsi4"></param>
        /// <param name="kocsi5"></param>
        /// <param name="kocsi6"></param>
        public Adat_Szerelvény(long szerelvény_ID, long szerelvényhossz, string kocsi1, string kocsi2, string kocsi3, string kocsi4, string kocsi5, string kocsi6)
        {
            Szerelvény_ID = szerelvény_ID;
            Szerelvényhossz = szerelvényhossz;
            Kocsi1 = kocsi1;
            Kocsi2 = kocsi2;
            Kocsi3 = kocsi3;
            Kocsi4 = kocsi4;
            Kocsi5 = kocsi5;
            Kocsi6 = kocsi6;
        }

        public Adat_Szerelvény(long szerelvény_ID, long szerelvényhossz)
        {
            Szerelvény_ID = szerelvény_ID;
            Szerelvényhossz = szerelvényhossz;
        }
    }
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