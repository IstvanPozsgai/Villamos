using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Jármű_Állomány_Típus
    {
        public long Id { get; private set; }
        public long Állomány { get; private set; }
        public string Típus { get; private set; }

        public Adat_Jármű_Állomány_Típus(long id, long állomány, string típus)
        {
            Id = id;
            Állomány = állomány;
            Típus = típus;
        }
    }
}
