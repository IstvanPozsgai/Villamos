using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    /// <summary>
    /// Állomány táblánál külön beállítási lehetőség, hogy hol is van a kocsi
    /// </summary>
    public class Adat_Jármű_Vendég
    {
        public string Azonosító { get; private set; }
        public string Típus { get; private set; }
        public string BázisTelephely { get; private set; }
        public string KiadóTelephely { get; private set; }

        /// <summary>
        /// Rögzítéshez, módosításhoz
        /// </summary>
        /// <param name="azonosító"></param>
        /// <param name="típus"></param>
        /// <param name="bázisTelephely"></param>
        /// <param name="kiadóTelephely"></param>
        public Adat_Jármű_Vendég(string azonosító, string típus, string bázisTelephely, string kiadóTelephely)
        {
            Azonosító = azonosító;
            Típus = típus;
            BázisTelephely = bázisTelephely;
            KiadóTelephely = kiadóTelephely;
        }
    }
}
