using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Védő_Könyvelés
    {
        public string Azonosító { get; private set; }
        public string Szerszámkönyvszám { get; private set; }
        public double Mennyiség { get; private set; }

        public string Gyáriszám { get; private set; }
        public DateTime Dátum { get; private set; }
        public bool Státus { get; private set; }

        public Adat_Védő_Könyvelés(string azonosító, string szerszámkönyvszám, double mennyiség, string gyáriszám, DateTime dátum, bool státus)
        {
            Azonosító = azonosító;
            Szerszámkönyvszám = szerszámkönyvszám;
            Mennyiség = mennyiség;
            Gyáriszám = gyáriszám;
            Dátum = dátum;
            Státus = státus;
        }
    }
}
