using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Behajtás_Kérelemsátus
    {
        public int ID { get; set; }
        public string Státus { get; set; }

        public Adat_Behajtás_Kérelemsátus(int iD, string státus)
        {
            ID = iD;
            Státus = státus;
        }
    }
}
