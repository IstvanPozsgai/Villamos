using System;

namespace Villamos.Adatszerkezet
{
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
            string megjegyzés, DateTime utolsó_Dátum, int státus)
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
