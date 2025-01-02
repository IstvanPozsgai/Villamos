using System;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_TTP_Alapadat
    {
        public string Azonosító { get; private set; }
        public DateTime Gyártási_Év { get; private set; }
        public bool TTP { get; private set; }
        public string Megjegyzés { get; set; }

        public Adat_TTP_Alapadat(string azonosító, DateTime gyártási_Év, bool tTP, string megjegyzés)
        {
            Azonosító = azonosító;
            Gyártási_Év = gyártási_Év;
            TTP = tTP;
            Megjegyzés = megjegyzés;
        }
    }

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

    public class Adat_TTP_Tábla
    {
        public string Azonosító { get; set; }
        public DateTime Lejárat_Dátum { get; set; }
        public DateTime Ütemezés_Dátum { get; set; }
        public DateTime TTP_Dátum { get; set; }
        public bool TTP_Javítás { get; set; }
        public string Rendelés { get; set; }
        public DateTime JavBefDát { get; set; }
        public string Együtt { get; set; }
        public int Státus { get; set; }
        public string Megjegyzés { get; set; }

        public Adat_TTP_Tábla(string azonosító, DateTime lejárat_Dátum, DateTime ütemezés_Dátum, DateTime tTP_Dátum, bool tTP_Javítás, string rendelés, DateTime javBefDát, string együtt, int státus, string megjegyzés)
        {
            Azonosító = azonosító;
            Lejárat_Dátum = lejárat_Dátum;
            Ütemezés_Dátum = ütemezés_Dátum;
            TTP_Dátum = tTP_Dátum;
            TTP_Javítás = tTP_Javítás;
            Rendelés = rendelés;
            JavBefDát = javBefDát;
            Együtt = együtt;
            Státus = státus;
            Megjegyzés = megjegyzés;
        }
    }

    public class Adat_TTP_Év
    {
        public int Év { get; set; }
        public int Életkor { get; set; }

        public Adat_TTP_Év(int év, int életkor)
        {
            Év = év;
            Életkor = életkor;
        }
    }

    public class Adat_Tábla_Vezénylés
    {
        public string Azonosító { get; set; }
        public DateTime Le_Dátum { get; set; }
        public DateTime Ütem_Dátum { get; set; }
        public string Hiba { get; set; }
        public long Kocsistátus { get; set; }
        public string Típus { get; set; }
        public string Telephely { get; set; }
        public string TTP_Kötelezett { get; private set; }
        public string Megjegyzés { get; set; }
        public DateTime Utolsó_Dátum { get; set; }
        public int Státus { get; set; }

        public Adat_Tábla_Vezénylés(string azonosító, DateTime le_Dátum, DateTime ütem_Dátum, string hiba, long kocsistátus, string típus, string telephely, string tTP_Kötelezett,
            string megjegyzés, DateTime utolsó_Dátum,int  státus)
        {
            Azonosító = azonosító;
            Le_Dátum = le_Dátum;
            Ütem_Dátum = ütem_Dátum;
            Hiba = hiba;
            Kocsistátus = kocsistátus;
            Típus = típus;
            Telephely = telephely;
            TTP_Kötelezett = tTP_Kötelezett;
            Megjegyzés = megjegyzés;
            Utolsó_Dátum = utolsó_Dátum;
            Státus = státus;
        }
    }
}
