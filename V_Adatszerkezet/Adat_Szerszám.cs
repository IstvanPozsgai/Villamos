using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_Szerszám_Napló
    {
        public string Azonosító { get; private set; }
        public string Honnan { get; private set; }
        public string Hova { get; private set; }
        public int Mennyiség { get; private set; }
        public string Módosította { get; private set; }
        public DateTime Módosításidátum { get; private set; }


        /// <summary>
        /// Rögzítés
        /// </summary>
        /// <param name="azonosító"></param>
        /// <param name="honnan"></param>
        /// <param name="hova"></param>
        /// <param name="mennyiség"></param>
        public Adat_Szerszám_Napló(string azonosító, string honnan, string hova, int mennyiség)
        {
            Azonosító = azonosító;
            Honnan = honnan;
            Hova = hova;
            Mennyiség = mennyiség;
        }


        /// <summary>
        /// Lekérdezés
        /// </summary>
        /// <param name="azonosító"></param>
        /// <param name="honnan"></param>
        /// <param name="hova"></param>
        /// <param name="mennyiség"></param>
        /// <param name="módosította"></param>
        /// <param name="módosításidátum"></param>
        public Adat_Szerszám_Napló(string azonosító, string honnan, string hova, int mennyiség, string módosította, DateTime módosításidátum)
        {
            Azonosító = azonosító;
            Honnan = honnan;
            Hova = hova;
            Mennyiség = mennyiség;
            Módosította = módosította;
            Módosításidátum = módosításidátum;
        }



    }

}
