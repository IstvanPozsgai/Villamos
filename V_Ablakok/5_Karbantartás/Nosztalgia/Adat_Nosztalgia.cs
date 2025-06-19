using System;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_Nosztalgia_Állomány
    {
        public string Azonosító { get; private set; }
        public string Ciklus_idő { get; private set; }
        public string Ciklus_km1 { get; private set; }
        public string Ciklus_km2 { get; private set; }
        //public string Gyártó { get; private set; }
        //public int Év { get; private set; }
        //public string Ntípus { get; private set; }
        //public string Eszközszám { get; private set; }
        //public string Leltári_szám { get; private set; }
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

        public Adat_Nosztalgia_Állomány(string azonosító, string ciklus_idő, string ciklus_km1, string ciklus_km2,/* string gyártó, int év, string ntípus, string eszközszám, string leltári_szám, */DateTime vizsgálatdátuma_idő, DateTime vizsgálatdátuma_km, string vizsgálatfokozata, string vizsgálatszáma_idő, string vizsgálatszáma_km, DateTime utolsóforgalminap, int km_v, int km_u, DateTime utolsórögzítés, string telephely)
        {
            Azonosító = azonosító;
            Ciklus_idő = ciklus_idő;
            Ciklus_km1 = ciklus_km1;
            Ciklus_km2 = ciklus_km2;
            //Gyártó = gyártó;
            //Év = év;
            //Ntípus = ntípus;
            //Eszközszám = eszközszám;
            //Leltári_szám = leltári_szám;
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

    public class Adat_Nosztagia_Futás
    {
        public string Azonosító { get; private set; }
        public DateTime Dátum { get; private set; }
        public bool Státusz { get; private set; }
        public DateTime Mikor { get; private set; }
        public string Ki { get; private set; }
        public string Telephely { get; private set; }

        public Adat_Nosztagia_Futás(string azonosító, DateTime dátum, bool státusz, DateTime mikor, string ki, string telephely)
        {
            Azonosító = azonosító;
            Dátum = dátum;
            Státusz = státusz;
            Mikor = mikor;
            Ki = ki;
            Telephely = telephely;
        }
    }

}
