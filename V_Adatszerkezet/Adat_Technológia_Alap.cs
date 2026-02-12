using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Technológia_Alap
    {
        public long Id { get; private set; }
        public string Típus { get; private set; }


        public Adat_Technológia_Alap(long id, string típus)
        {
            Id = id;
            Típus = típus;
        }
    }
}
