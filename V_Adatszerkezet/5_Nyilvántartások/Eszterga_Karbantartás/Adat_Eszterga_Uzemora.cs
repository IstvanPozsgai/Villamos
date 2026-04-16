using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Eszterga_Uzemora
    {
        public int ID { get; set; }
        public long Uzemora { get; set; }
        public DateTime Dátum { get; set; }
        public bool Státus { get; set; }

        public Adat_Eszterga_Uzemora(int iD, long üzemóra, DateTime dátum, bool státus)
        {
            ID = iD;
            Uzemora = üzemóra;
            Dátum = dátum;
            Státus = státus;
        }
        public Adat_Eszterga_Uzemora(int iD)
        {
            ID = iD;
        }
    }
}
