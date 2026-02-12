using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Technológia_Rendelés
    {
        public long Év { get; private set; }
        public string Karbantartási_fokozat { get; private set; }
        public string Technológia_típus { get; private set; }
        public string Rendelésiszám { get; private set; }

        public Adat_Technológia_Rendelés(long év, string karbantartási_fokozat, string technológia_típus, string rendelésiszám)
        {
            Év = év;
            Karbantartási_fokozat = karbantartási_fokozat;
            Technológia_típus = technológia_típus;
            Rendelésiszám = rendelésiszám;
        }
    }
}
