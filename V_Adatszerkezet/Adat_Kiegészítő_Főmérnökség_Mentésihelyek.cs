using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kiegészítő_Mentésihelyek
    {
        public long Sorszám { get; private set; }
        public string Alprogram { get; private set; }
        public string Elérésiút { get; private set; }

        public Adat_Kiegészítő_Mentésihelyek(long sorszám, string alprogram, string elérésiút)
        {
            Sorszám = sorszám;
            Alprogram = alprogram;
            Elérésiút = elérésiút;
        }
    }
}
