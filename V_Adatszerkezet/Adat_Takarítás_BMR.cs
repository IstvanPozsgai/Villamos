using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Villamos_Adatszerkezet
{
    public  class Adat_Takarítás_BMR
    {
        public int Id { get; private set; }
        public string Telephely { get; private set; }
        public string JárműÉpület { get; private set; }
        public string BMRszám { get; private set; }
        public DateTime Dátum { get; private set; }

        public Adat_Takarítás_BMR(int id, string telephely, string járműÉpület, string bMRszám, DateTime dátum)
        {
            Id = id;
            Telephely = telephely;
            JárműÉpület = járműÉpület;
            BMRszám = bMRszám;
            Dátum = dátum;
        }
    }
}
