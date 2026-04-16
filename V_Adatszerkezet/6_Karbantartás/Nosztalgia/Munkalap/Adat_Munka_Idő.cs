using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Munka_Idő
    {
        public long ID { get; private set; }
        public long Idő { get; private set; }

        public Adat_Munka_Idő(long iD, long idő)
        {
            ID = iD;
            Idő = idő;
        }
    }
}
