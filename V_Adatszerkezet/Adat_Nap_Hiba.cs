using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_Nap_Hiba
    {
        public string Azonosító { get; private set; }
        public DateTime Mikori { get; private set; }
        public string Beálló { get; private set; }
        public string Üzemképtelen { get; private set; }
        public string Üzemképeshiba { get; private set; }
        public string Típus { get; private set; }
        public long Státus { get; private set; }

        public Adat_Nap_Hiba(string azonosító, DateTime mikori, string beálló, string üzemképtelen, string üzemképeshiba, string típus, long státus)
        {
            Azonosító = azonosító;
            Mikori = mikori;
            Beálló = beálló;
            Üzemképtelen = üzemképtelen;
            Üzemképeshiba = üzemképeshiba;
            Típus = típus;
            Státus = státus;
        }
    }
}
