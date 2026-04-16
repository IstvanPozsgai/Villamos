using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Rezsi_Lista
    {
        public string Azonosító { get; private set; }
        public double Mennyiség { get; private set; }
        public DateTime Dátum { get; private set; }
        public bool Státus { get; private set; }

        public string Telephely { get; private set; }

        public Adat_Rezsi_Lista(string azonosító, double mennyiség, DateTime dátum, bool státus)
        {
            Azonosító = azonosító;
            Mennyiség = mennyiség;
            Dátum = dátum;
            Státus = státus;
        }

        public Adat_Rezsi_Lista(string azonosító, double mennyiség, DateTime dátum, bool státus, string telephely)
        {
            Azonosító = azonosító;
            Mennyiség = mennyiség;
            Dátum = dátum;
            Státus = státus;
            Telephely = telephely;
        }
    }

}
