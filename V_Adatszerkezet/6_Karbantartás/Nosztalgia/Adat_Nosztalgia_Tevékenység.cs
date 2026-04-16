using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_Nosztalgia_Tevékenység
    {
        public string Azonosító { get; private set; }
        public string Ciklus_idő { get; private set; }
        public string Ciklus_km1 { get; private set; }
        public string Ciklus_km2 { get; private set; }
        public DateTime Vizsgálatdátuma_idő { get; private set; }
        public DateTime Vizsgálatdátuma_km { get; private set; }
        public string Vizsgálatfokozata { get; private set; }
        public string Vizsgálatszáma_idő { get; private set; }
        public string Vizsgálatszáma_km { get; private set; }
        public DateTime Utolsóforgalminap { get; private set; }
        public int Km_v { get; private set; }
        public int Km_u { get; private set; }
        public DateTime Utolsórögzítés { get; private set; }
        public string Telephely { get; private set; }

        public Adat_Nosztalgia_Tevékenység(string azonosító, string ciklus_idő, string ciklus_km1, string ciklus_km2, DateTime vizsgálatdátuma_idő, DateTime vizsgálatdátuma_km, string vizsgálatfokozata, string vizsgálatszáma_idő, string vizsgálatszáma_km, DateTime utolsóforgalminap, int km_v, int km_u, DateTime utolsórögzítés, string telephely)
        {
            Azonosító = azonosító;
            Ciklus_idő = ciklus_idő;
            Ciklus_km1 = ciklus_km1;
            Ciklus_km2 = ciklus_km2;
            Vizsgálatdátuma_idő = vizsgálatdátuma_idő;
            Vizsgálatdátuma_km = vizsgálatdátuma_km;
            Vizsgálatfokozata = vizsgálatfokozata;
            Vizsgálatszáma_idő = vizsgálatszáma_idő;
            Vizsgálatszáma_km = vizsgálatszáma_km;
            Utolsóforgalminap = utolsóforgalminap;
            Km_v = km_v;
            Km_u = km_u;
            Utolsórögzítés = utolsórögzítés;
            Telephely = telephely;
        }
    }
}
