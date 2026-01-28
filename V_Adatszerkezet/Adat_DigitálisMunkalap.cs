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

    public class Adat_DigitálisMunkalap_Dolgozó
    {
        public string DolgozóNév { get; private set; }
        public string Dolgozószám { get; private set; }

        public long Fej_Id { get; private set; }
        public long Technológia_Id { get; private set; }

        public Adat_DigitálisMunkalap_Dolgozó(string dolgozóNév, string dolgozószám, long fej_Id, long technológia_Id)
        {
            DolgozóNév = dolgozóNév;
            Dolgozószám = dolgozószám;
            Fej_Id = fej_Id;
            Technológia_Id = technológia_Id;
        }
    }

    public class Adat_DigitálisMunkalap_Kocsik
    {
        public long Fej_Id { get; private set; }

        public string Azonosító { get; private set; }

        public long KMU { get; private set; }
        public string Rendelés { get; private set; }

        public Adat_DigitálisMunkalap_Kocsik(long fej_Id, string azonosító, long kMU, string rendelés)
        {
            Fej_Id = fej_Id;
            Azonosító = azonosító;
            KMU = kMU;
            Rendelés = rendelés;
        }
    }


}
