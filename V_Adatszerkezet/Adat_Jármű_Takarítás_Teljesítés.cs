using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Jármű_Takarítás_Teljesítés
    {
        public string Azonosító { get; private set; }
        public string Takarítási_fajta { get; private set; }
        public DateTime Dátum { get; private set; }
        public int Megfelelt1 { get; private set; }
        public int Státus { get; private set; }
        public int Megfelelt2 { get; private set; }
        public bool Pótdátum { get; private set; }
        public int Napszak { get; private set; }
        public double Mérték { get; private set; }
        public string Típus { get; private set; }

        public Adat_Jármű_Takarítás_Teljesítés(string azonosító, string takarítási_fajta, DateTime dátum, int megfelelt1, int státus, int megfelelt2, bool pótdátum, int napszak, double mérték, string típus)
        {
            Azonosító = azonosító;
            Takarítási_fajta = takarítási_fajta;
            Dátum = dátum;
            Megfelelt1 = megfelelt1;
            Státus = státus;
            Megfelelt2 = megfelelt2;
            Pótdátum = pótdátum;
            Napszak = napszak;
            Mérték = mérték;
            Típus = típus;
        }
    }
}
