using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kiegészítő_Hibaterv
    {
        public long Id { get; private set; }
        public string Szöveg { get; private set; }
        public bool Főkönyv { get; private set; }

        public Adat_Kiegészítő_Hibaterv(long id, string szöveg, bool főkönyv)
        {
            Id = id;
            Szöveg = szöveg;
            Főkönyv = főkönyv;
        }
    }
}
