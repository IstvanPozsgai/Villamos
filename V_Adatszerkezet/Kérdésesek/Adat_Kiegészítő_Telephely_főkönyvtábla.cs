using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kiegészítő_főkönyvtábla
    {
        public long Id { get; private set; }
        public string Név { get; private set; }
        public string Beosztás { get; private set; }
        public string Email { get; private set; }

        public Adat_Kiegészítő_főkönyvtábla(long id, string név, string beosztás)
        {
            Id = id;
            Név = név;
            Beosztás = beosztás;
        }

        public Adat_Kiegészítő_főkönyvtábla(long id, string név, string beosztás, string email)
        {
            Id = id;
            Név = név;
            Beosztás = beosztás;
            Email = email;
        }
    }
}
