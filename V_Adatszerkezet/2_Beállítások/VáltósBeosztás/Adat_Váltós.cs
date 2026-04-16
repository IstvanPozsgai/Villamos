using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_Váltós_Naptár
    {
        public string Nap { get; private set; }
        public DateTime Dátum { get; private set; }

        public Adat_Váltós_Naptár(string nap, DateTime dátum)
        {
            Nap = nap;
            Dátum = dátum;
        }
    }

}
