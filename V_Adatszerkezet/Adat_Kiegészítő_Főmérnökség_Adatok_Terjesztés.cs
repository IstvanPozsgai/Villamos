using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kiegészítő_Adatok_Terjesztés
    {
        public long Id { get; private set; }
        public string Szöveg { get; private set; }
        public string Email { get; private set; }

        public Adat_Kiegészítő_Adatok_Terjesztés(long id, string szöveg, string email)
        {
            Id = id;
            Szöveg = szöveg;
            Email = email;
        }
    }
}
