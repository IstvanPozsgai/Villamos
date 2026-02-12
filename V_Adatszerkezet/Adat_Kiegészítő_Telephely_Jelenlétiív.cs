using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kiegészítő_Jelenlétiív
    {
        public long Id { get; private set; }
        public string Szervezet { get; private set; }

        public Adat_Kiegészítő_Jelenlétiív(long id, string szervezet)
        {
            Id = id;
            Szervezet = szervezet;
        }
    }
}
