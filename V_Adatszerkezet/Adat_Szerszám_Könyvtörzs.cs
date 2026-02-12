using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Szerszám_Könyvtörzs
    {
        public string Szerszámkönyvszám { get; private set; }
        public string Szerszámkönyvnév { get; private set; }
        public string Felelős1 { get; private set; }
        public string Felelős2 { get; private set; }
        public bool Státus { get; private set; }

        public Adat_Szerszám_Könyvtörzs(string szerszámkönyvszám, string szerszámkönyvnév)
        {
            Szerszámkönyvszám = szerszámkönyvszám;
            Szerszámkönyvnév = szerszámkönyvnév;
        }


        /// <summary>
        /// Új rögzítése
        /// </summary>
        /// <param name="szerszámkönyvszám"></param>
        /// <param name="szerszámkönyvnév"></param>
        /// <param name="felelős1"></param>
        /// <param name="felelős2"></param>
        public Adat_Szerszám_Könyvtörzs(string szerszámkönyvszám, string szerszámkönyvnév, string felelős1, string felelős2)
        {
            Szerszámkönyvszám = szerszámkönyvszám;
            Szerszámkönyvnév = szerszámkönyvnév;
            Felelős1 = felelős1;
            Felelős2 = felelős2;
        }


        /// <summary>
        /// Lekérdezés és módosítás
        /// </summary>
        /// <param name="szerszámkönyvszám"></param>
        /// <param name="szerszámkönyvnév"></param>
        /// <param name="felelős1"></param>
        /// <param name="felelős2"></param>
        /// <param name="státus"></param>
        public Adat_Szerszám_Könyvtörzs(string szerszámkönyvszám, string szerszámkönyvnév, string felelős1, string felelős2, bool státus)
        {
            Szerszámkönyvszám = szerszámkönyvszám;
            Szerszámkönyvnév = szerszámkönyvnév;
            Felelős1 = felelős1;
            Felelős2 = felelős2;
            Státus = státus;
        }
    }
}
