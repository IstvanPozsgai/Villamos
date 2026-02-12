using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kiegészítő_Idő_Kor
    {
        public long Id { get; private set; }
        public long Kiadási { get; private set; }
        public long Érkezési { get; private set; }

        public Adat_Kiegészítő_Idő_Kor(long id, long kiadási, long érkezési)
        {
            Id = id;
            Kiadási = kiadási;
            Érkezési = érkezési;
        }
    }
}
