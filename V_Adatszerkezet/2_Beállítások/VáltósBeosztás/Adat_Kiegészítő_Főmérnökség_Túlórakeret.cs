using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kiegészítő_Túlórakeret
    {
        public int Határ { get; private set; }
        public int Parancs { get; private set; }
        public string Telephely { get; private set; }

        public Adat_Kiegészítő_Túlórakeret(int határ, int parancs, string telephely)
        {
            Határ = határ;
            Parancs = parancs;
            Telephely = telephely;
        }
    }
}
