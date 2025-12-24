using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_MEO_Naptábla
    {
        public int Id { get; private set; }

        public Adat_MEO_Naptábla(int id)
        {
            Id = id;
        }
    }

    public class Adat_MEO_Tábla
    {
        public string Név { get; set; }
        public string Típus { get; set; }

        public Adat_MEO_Tábla(string név, string típus)
        {
            Név = név;
            Típus = típus;
        }
    }

    public class Adat_MEO_KerékMérés
    {
        public string Azonosító {  get; set; }
        public DateTime Bekövetkezés { get; set; }
        public string Üzem {  get; set; }
        public bool Törölt {  get; set; }
        public DateTime Mikor {  get; set; }
        public string Ki { get; set; }
        public string Típus { get; set; }

        public Adat_MEO_KerékMérés(string azonosító, DateTime bekövetkezés, string üzem, bool törölt, DateTime mikor, string ki, string típus)
        {
            Azonosító = azonosító;
            Bekövetkezés = bekövetkezés;
            Üzem = üzem;
            Törölt = törölt;
            Mikor = mikor;
            Ki = ki;
            Típus = típus;
        }
    }
}
