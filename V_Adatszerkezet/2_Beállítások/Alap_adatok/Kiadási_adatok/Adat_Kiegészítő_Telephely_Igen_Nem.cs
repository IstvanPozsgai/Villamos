using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kiegészítő_Igen_Nem
    {
        public long Id { get; private set; }
        public bool Válasz { get; private set; }
        public string Megjegyzés { get; private set; }

        public Adat_Kiegészítő_Igen_Nem(long id, bool válasz, string megjegyzés)
        {
            Id = id;
            Válasz = válasz;
            Megjegyzés = megjegyzés;
        }
    }
}
