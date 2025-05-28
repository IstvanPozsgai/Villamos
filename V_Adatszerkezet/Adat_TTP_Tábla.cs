using System;

namespace Villamos.Adatszerkezet
{
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

}
