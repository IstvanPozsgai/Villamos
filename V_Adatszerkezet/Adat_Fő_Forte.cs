using System;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_Fő_Forte
    {
        public DateTime  Dátum { get; private set; }
        public string Napszak { get; private set; }
        public string Telephelyforte { get; private set; }
        public string Típusforte { get; private set; }
        public string Telephely { get; private set; }
        public string Típus { get; private set; }
        public int Kiadás { get; private set; }
        public int Munkanap { get; private set; }

        /// <summary>
        /// Adatrögzítés és lekérdezés
        /// </summary>
        /// <param name="dátum"></param>
        /// <param name="napszak"></param>
        /// <param name="telephelyforte"></param>
        /// <param name="típusforte"></param>
        /// <param name="telephely"></param>
        /// <param name="típus"></param>
        /// <param name="kiadás"></param>
        /// <param name="munkanap"></param>
        public Adat_Fő_Forte(DateTime dátum, string napszak, string telephelyforte, string típusforte, string telephely, string típus, int kiadás, int munkanap)
        {
            Dátum = dátum;
            Napszak = napszak;
            Telephelyforte = telephelyforte;
            Típusforte = típusforte;
            Telephely = telephely;
            Típus = típus;
            Kiadás = kiadás;
            Munkanap = munkanap;
        }
    }
}
