using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kerék_Eszterga_Terjesztés
    {
        public string Név { get; private set; }
        public string Email { get; private set; }
        public string Telephely { get; private set; }
        public int Változat { get; private set; }

        public Adat_Kerék_Eszterga_Terjesztés(string név, string email, string telephely, int változat)
        {
            Név = név;
            Email = email;
            Telephely = telephely;
            Változat = változat;
        }
    }
}
