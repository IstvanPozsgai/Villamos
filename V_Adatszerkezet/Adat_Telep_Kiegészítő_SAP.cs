using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Telep_Kiegészítő_SAP
    {
        public long Id { get; private set; }
        public string Felelősmunkahely { get; private set; }

        public Adat_Telep_Kiegészítő_SAP(long id, string felelősmunkahely)
        {
            Id = id;
            Felelősmunkahely = felelősmunkahely;
        }
    }
}
