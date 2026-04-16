using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Technológia_Változat
    {
        public long Technológia_Id { get; private set; }
        public string Változatnév { get; private set; }
        public string Végzi { get; private set; }
        public string Karbantartási_fokozat { get; private set; }

        public Adat_Technológia_Változat(long technológia_Id, string változatnév, string végzi, string karbantartási_fokozat)
        {
            Technológia_Id = technológia_Id;
            Változatnév = változatnév;
            Végzi = végzi;
            Karbantartási_fokozat = karbantartási_fokozat;
        }

        public Adat_Technológia_Változat(long technológia_Id)
        {
            Technológia_Id = technológia_Id;
        }
    }
}
