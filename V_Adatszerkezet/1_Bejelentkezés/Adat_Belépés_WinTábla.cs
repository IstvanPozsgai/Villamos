using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Belépés_WinTábla
    {
        public string Név { get; private set; }
        public string Telephely { get; private set; }
        public string WinUser { get; private set; }

        public Adat_Belépés_WinTábla(string név, string telephely, string winUser)
        {
            Név = név;
            Telephely = telephely;
            WinUser = winUser;
        }
    }
}
