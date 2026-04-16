using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_TTP_Naptár
    {
        public DateTime Dátum { get; private set; }
        public bool Munkanap { get; private set; }


        public Adat_TTP_Naptár(DateTime dátum, bool munkanap)
        {
            Dátum = dátum;
            Munkanap = munkanap;
        }
    }
}
