using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kerék_Eszterga_Igény
    {
        public string Pályaszám { get; private set; }
        public string Megjegyzés { get; private set; }
        public DateTime Rögzítés_dátum { get; private set; }
        public string Igényelte { get; private set; }
        public int Tengelyszám { get; private set; }
        public int Szerelvény { get; private set; }
        public int Prioritás { get; private set; }
        public DateTime Ütemezés_dátum { get; private set; }
        public int Státus { get; private set; }
        public string Telephely { get; private set; }

        public string Típus { get; private set; }

        public int Norma { get; private set; }

        public Adat_Kerék_Eszterga_Igény(string pályaszám, string megjegyzés, DateTime rögzítés_dátum, string igényelte, int tengelyszám, int szerelvény, int prioritás, DateTime ütemezés_dátum, int státus, string telephely, string típus, int norma)
        {
            Pályaszám = pályaszám;
            Megjegyzés = megjegyzés;
            Rögzítés_dátum = rögzítés_dátum;
            Igényelte = igényelte;
            Tengelyszám = tengelyszám;
            Szerelvény = szerelvény;
            Prioritás = prioritás;
            Ütemezés_dátum = ütemezés_dátum;
            Státus = státus;
            Telephely = telephely;
            Típus = típus;
            Norma = norma;
        }

        /// <summary>
        ///   Módosításhoz konstruktor, csak a pályaszám, ütemezés dátum és telephely szükséges.
        /// </summary>
        /// <param name="pályaszám"></param>
        /// <param name="ütemezés_dátum"></param>
        /// <param name="státus"></param>
        /// <param name="telephely"></param>
        public Adat_Kerék_Eszterga_Igény(string pályaszám, DateTime ütemezés_dátum, int státus, string telephely)
        {
            Pályaszám = pályaszám;
            Ütemezés_dátum = ütemezés_dátum;
            Státus = státus;
            Telephely = telephely;
        }
    }

}
