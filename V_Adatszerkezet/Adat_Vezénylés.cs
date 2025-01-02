using System;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_Vezénylés
    {
        public string Azonosító { get; private set; }
        public DateTime Dátum { get; private set; }
        public int Státus { get; private set; }
        public int Vizsgálatraütemez { get; private set; }
        public int Takarításraütemez { get; private set; }
        public string Vizsgálat { get; private set; }
        public int Vizsgálatszám { get; private set; }
        public string Rendelésiszám { get; private set; }
        public int Álljon { get; private set; }
        public int Fusson { get; private set; }
        public int Törlés { get; private set; }
        public long Szerelvényszám { get; private set; }
        public string Típus { get; private set; }

        public Adat_Vezénylés(string azonosító, DateTime dátum, int státus, int vizsgálatraütemez, int takarításraütemez, string vizsgálat, int vizsgálatszám, string rendelésiszám, int álljon, int fusson, int törlés, long szerelvényszám, string típus)
        {
            Azonosító = azonosító;
            Dátum = dátum;
            Státus = státus;
            Vizsgálatraütemez = vizsgálatraütemez;
            Takarításraütemez = takarításraütemez;
            Vizsgálat = vizsgálat;
            Vizsgálatszám = vizsgálatszám;
            Rendelésiszám = rendelésiszám;
            Álljon = álljon;
            Fusson = fusson;
            Törlés = törlés;
            Szerelvényszám = szerelvényszám;
            Típus = típus;
        }
    }
}
