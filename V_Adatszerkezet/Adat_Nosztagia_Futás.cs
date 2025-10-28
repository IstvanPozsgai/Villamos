using System;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_Nosztagia_Futás
    {
        public string Azonosító { get; private set; }
        public DateTime Dátum { get; private set; }
        public bool Státusz { get; private set; }
        public DateTime Mikor { get; private set; }
        public string Ki { get; private set; }
        public string Telephely { get; private set; }

        public Adat_Nosztagia_Futás(string azonosító, DateTime dátum, bool státusz, DateTime mikor, string ki, string telephely)
        {
            Azonosító = azonosító;
            Dátum = dátum;
            Státusz = státusz;
            Mikor = mikor;
            Ki = ki;
            Telephely = telephely;
        }
    }

}
