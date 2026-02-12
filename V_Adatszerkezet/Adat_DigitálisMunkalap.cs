using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_DigitálisMunkalap_Fej
    {
        public long Id { get; private set; }
        public string Típus { get; private set; }
        public string Karbantartási_fokozat { get; private set; }
        public string EllDolgozóNév { get; private set; }
        public string EllDolgozószám { get; private set; }
        public string Telephely { get; private set; }
        public DateTime Dátum { get; private set; }

        public Adat_DigitálisMunkalap_Fej(long id, string típus, string karbantartási_fokozat, string ellDolgozóNév, string ellDolgozószám, string telephely, DateTime dátum)
        {
            Id = id;
            Típus = típus;
            Karbantartási_fokozat = karbantartási_fokozat;
            EllDolgozóNév = ellDolgozóNév;
            EllDolgozószám = ellDolgozószám;
            Telephely = telephely;
            Dátum = dátum;
        }
    }

}
