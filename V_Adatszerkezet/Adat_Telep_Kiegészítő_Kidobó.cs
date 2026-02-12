using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Telep_Kiegészítő_Kidobó
    {
        public long Id { get; private set; }
        public string Telephely { get; private set; }

        public Adat_Telep_Kiegészítő_Kidobó(long id, string telephely)
        {
            Id = id;
            Telephely = telephely;
        }
    }
}
