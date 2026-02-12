using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Villamos.Adatszerkezet;

namespace Villamos.Adatszerkezet
{
    public class Adat_Szerszám_Könyvelés
    {
        public Adat_Szerszám_Cikktörzs Azonosító { get; private set; }
        public Adat_Szerszám_Könyvtörzs Szerszámkönyvszám { get; private set; }
        public int Mennyiség { get; private set; }
        public DateTime Dátum { get; private set; }

        public string AzonosítóMás { get; private set; }

        public string SzerszámkönyvszámMás { get; private set; }

        /// <summary>
        /// Új rögzítése
        /// </summary>
        /// <param name="azonosító"></param>
        /// <param name="szerszámkönyvszám"></param>
        /// <param name="mennyiség"></param>
        public Adat_Szerszám_Könyvelés(Adat_Szerszám_Cikktörzs azonosító, Adat_Szerszám_Könyvtörzs szerszámkönyvszám, int mennyiség)
        {
            Azonosító = azonosító;
            Szerszámkönyvszám = szerszámkönyvszám;
            Mennyiség = mennyiség;
        }

        public Adat_Szerszám_Könyvelés(int mennyiség, DateTime dátum, string azonosítóMás, string szerszámkönyvszámMás)
        {
            Mennyiség = mennyiség;
            Dátum = dátum;
            AzonosítóMás = azonosítóMás;
            SzerszámkönyvszámMás = szerszámkönyvszámMás;
        }




        /// <summary>
        /// Lekérdedezés
        /// </summary>
        /// <param name="azonosító"></param>
        /// <param name="szerszámkönyvszám"></param>
        /// <param name="mennyiség"></param>
        /// <param name="dátum"></param>
        public Adat_Szerszám_Könyvelés(Adat_Szerszám_Cikktörzs azonosító, Adat_Szerszám_Könyvtörzs szerszámkönyvszám, int mennyiség, DateTime dátum)
        {
            Azonosító = azonosító;
            Szerszámkönyvszám = szerszámkönyvszám;
            Mennyiség = mennyiség;
            Dátum = dátum;
        }
    }
}
