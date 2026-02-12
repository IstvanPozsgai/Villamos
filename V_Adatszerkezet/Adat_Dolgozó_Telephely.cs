using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Dolgozó_Telephely
    {
        public Adat_Dolgozó_Alap Dolgozó { get; private set; }
        public string Telephely { get; private set; }

        public Adat_Dolgozó_Telephely(Adat_Dolgozó_Alap dolgozó, string telephely)
        {
            Dolgozó = dolgozó;
            Telephely = telephely;
        }
    }
}
