using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_TW6000_Telephely
    {
        public int Sorrend { get; set; }
        public string Telephely { get; set; }
        public Adat_TW6000_Telephely(int sorrend, string telephely)
        {
            Sorrend = sorrend;
            Telephely = telephely;
        }

    }
}
