using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kiegészítő_Munkakör
    {
        public long Id { get; private set; }
        public string Megnevezés { get; private set; }
        public string Kategória { get; private set; }
        public bool Státus { get; private set; }

        public Adat_Kiegészítő_Munkakör(long id, string megnevezés, string kategória, bool státus)
        {
            Id = id;
            Megnevezés = megnevezés;
            Kategória = kategória;
            Státus = státus;
        }
    }
}
