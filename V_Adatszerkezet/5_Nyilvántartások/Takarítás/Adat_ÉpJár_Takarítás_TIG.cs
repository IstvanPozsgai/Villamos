using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_ÉpJár_Takarítás_TIG
    {
        public string Telephely { get; private set; }
        public string Tevékenység { get; private set; }
        public double Mennyiség { get; private set; }
        public string ME { get; private set; }
        public double Egységár { get; private set; }
        public double Összesen { get; private set; }

        public Adat_ÉpJár_Takarítás_TIG(string telephely, string tevékenység, double mennyiség, string mE, double egységár, double összesen)
        {
            Telephely = telephely;
            Tevékenység = tevékenység;
            Mennyiség = mennyiség;
            ME = mE;
            Egységár = egységár;
            Összesen = összesen;
        }
    }
}
