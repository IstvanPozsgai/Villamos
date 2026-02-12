using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Behajtás_Jogosultság
    {
        public int ID { get; set; }
        public string Státustípus { get; set; }

        public Adat_Behajtás_Jogosultság(int iD, string státustípus)
        {
            ID = iD;
            Státustípus = státustípus;
        }
    }
}
