using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Védő_Napló
    {
        public string Azonosító { get; private set; }
        public string Honnan { get; private set; }
        public string Hova { get; private set; }
        public double Mennyiség { get; private set; }
        public string Gyáriszám { get; private set; }
        public string Módosította { get; private set; }
        public DateTime Módosításidátum { get; private set; }
        public bool Státus { get; private set; }

        public Adat_Védő_Napló(string azonosító, string honnan, string hova, double mennyiség, string gyáriszám, string módosította, DateTime módosításidátum, bool státus)
        {
            Azonosító = azonosító;
            Honnan = honnan;
            Hova = hova;
            Mennyiség = mennyiség;
            Gyáriszám = gyáriszám;
            Módosította = módosította;
            Módosításidátum = módosításidátum;
            Státus = státus;
        }
    }
}
