using System;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_Jármű_Takarítás_Vezénylés
    {
        public long Id { get; private set; }
        public string Azonosító { get; private set; }
        public DateTime Dátum { get; private set; }
        public string Takarítási_fajta { get; private set; }
        public long Szerelvényszám { get; private set; }
        public int Státus { get; private set; }

        public Adat_Jármű_Takarítás_Vezénylés(long id, string azonosító, DateTime dátum, string takarítási_fajta, long szerelvényszám, int státus)
        {
            Id = id;
            Azonosító = azonosító;
            Dátum = dátum;
            Takarítási_fajta = takarítási_fajta;
            Szerelvényszám = szerelvényszám;
            Státus = státus;
        }
    }

    public class Adat_Jármű_Takarítás_Árak
    {
        public double Id { get; private set; }
        public string JárműTípus { get; private set; }
        public string Takarítási_fajta { get; private set; }
        public int Napszak { get; private set; }
        public double Ár { get; private set; }
        public DateTime Érv_kezdet { get; private set; }
        public DateTime Érv_vég { get; private set; }

        public Adat_Jármű_Takarítás_Árak(double id, string járműTípus, string takarítási_fajta, int napszak, double ár, DateTime érv_kezdet, DateTime érv_vég)
        {
            Id = id;
            JárműTípus = járműTípus;
            Takarítási_fajta = takarítási_fajta;
            Napszak = napszak;
            Ár = ár;
            Érv_kezdet = érv_kezdet;
            Érv_vég = érv_vég;
        }

        public Adat_Jármű_Takarítás_Árak(double id, DateTime érv_vég)
        {
            Id = id;
            Érv_vég = érv_vég;
        }
    }
    public class Adat_Jármű_Takarítás_Takarítások
    {
        //takarítások tábla
        public string Azonosító { get; private set; }
        public DateTime Dátum { get; private set; }
        public string Takarítási_fajta { get; private set; }
        public string Telephely { get; private set; }

        public int Státus { get; private set; }

        public Adat_Jármű_Takarítás_Takarítások(string azonosító, DateTime dátum, string takarítási_fajta, string telephely, int státus)
        {
            Azonosító = azonosító;
            Dátum = dátum;
            Takarítási_fajta = takarítási_fajta;
            Telephely = telephely;
            Státus = státus;
        }

    }

    public class Adat_Jármű_Takarítás_Ütemező
    {
        //takarítások tábla
        public string Azonosító { get; private set; }
        public DateTime Dátum { get; private set; }
        public int Növekmény { get; private set; }
        public string Mérték { get; private set; }

        public string Takarítási_fajta { get; private set; }
        public string Telephely { get; private set; }

        public int Státus { get; private set; }

        public Adat_Jármű_Takarítás_Ütemező(string azonosító, DateTime dátum, int növekmény, string mérték, string takarítási_fajta, string telephely, int státus)
        {
            Azonosító = azonosító;
            Dátum = dátum;
            Növekmény = növekmény;
            Mérték = mérték;
            Takarítási_fajta = takarítási_fajta;
            Telephely = telephely;
            Státus = státus;
        }

    }


    public class Adat_Jármű_Takarítás_Napló
    {
        public string Azonosító { get; private set; }
        public DateTime Dátum { get; private set; }
        public string Takarítási_fajta { get; private set; }
        public string Telephely { get; private set; }
        public DateTime Mikor { get; private set; }
        public string Módosító { get; private set; }
        public int Státus { get; private set; }

        public Adat_Jármű_Takarítás_Napló(string azonosító, DateTime dátum, string takarítási_fajta, string telephely, DateTime mikor, string módosító, int státus)
        {
            Azonosító = azonosító;
            Dátum = dátum;
            Takarítási_fajta = takarítási_fajta;
            Telephely = telephely;
            Mikor = mikor;
            Módosító = módosító;
            Státus = státus;
        }
    }

    public class Adat_Jármű_Takarítás_Kötbér
    {
        public string Takarítási_fajta { get; private set; }
        public string NemMegfelel { get; private set; }
        public string Póthatáridő { get; private set; }

        public Adat_Jármű_Takarítás_Kötbér(string takarítási_fajta, string nemMegfelel, string póthatáridő)
        {
            Takarítási_fajta = takarítási_fajta;
            NemMegfelel = nemMegfelel;
            Póthatáridő = póthatáridő;
        }
    }


    public class Adat_Jármű_Takarítás_Mátrix
    {
 
        public int Id { get; private set; }
        public string Fajta { get; private set; }
        public string Fajtamásik { get; private set; }

       public bool Igazság { get; private set; }
        public Adat_Jármű_Takarítás_Mátrix(int id, string fajta, string fajtamásik, bool igazság)
        {
            Id = id;
            Fajta = fajta;
            Fajtamásik = fajtamásik;
            Igazság = igazság;

        }
    }

    public class Adat_Jármű_Takarítás_Teljesítés
    {
        public string Azonosító { get; private set; }
        public string Takarítási_fajta { get; private set; }
        public DateTime Dátum { get; private set; }
        public int Megfelelt1 { get; private set; }
        public int Státus { get; private set; }
        public int Megfelelt2 { get; private set; }
        public bool Pótdátum { get; private set; }
        public int Napszak { get; private set; }
        public double Mérték { get; private set; }
        public string Típus { get; private set; }

        public Adat_Jármű_Takarítás_Teljesítés(string azonosító, string takarítási_fajta, DateTime dátum, int megfelelt1, int státus, int megfelelt2, bool pótdátum, int napszak, double mérték, string típus)
        {
            Azonosító = azonosító;
            Takarítási_fajta = takarítási_fajta;
            Dátum = dátum;
            Megfelelt1 = megfelelt1;
            Státus = státus;
            Megfelelt2 = megfelelt2;
            Pótdátum = pótdátum;
            Napszak = napszak;
            Mérték = mérték;
            Típus = típus;
        }
    }
    public class Adat_Jármű_Takarítás_J1
    {
        public DateTime Dátum { get; private set; }
        public int J1megfelelő { get; private set; }
        public int J1nemmegfelelő { get; private set; }
        public int Napszak { get; private set; }
        public string Típus { get; private set; }

        public Adat_Jármű_Takarítás_J1(DateTime dátum, int j1megfelelő, int j1nemmegfelelő, int napszak, string típus)
        {
            Dátum = dátum;
            J1megfelelő = j1megfelelő;
            J1nemmegfelelő = j1nemmegfelelő;
            Napszak = napszak;
            Típus = típus;
        }
    }

    public class Adat_Jármű_Takarítás_Létszám
    {
        public DateTime Dátum { get; private set; }
        public int Előírt { get; private set; }
        public int Megjelent { get; private set; }
        public int Napszak { get; private set; }
        public int Ruhátlan { get; private set; }

        public Adat_Jármű_Takarítás_Létszám(DateTime dátum, int előírt, int megjelent, int napszak, int ruhátlan)
        {
            Dátum = dátum;
            Előírt = előírt;
            Megjelent = megjelent;
            Napszak = napszak;
            Ruhátlan = ruhátlan;
        }
    }

    public class Adat_Jármű_Takarítás_TIG 
    {
       public string Telephely { get; private set; }
        public string Tevékenység { get; private set; }

        public double Mennyiség { get; private set; }
        public string ME { get; private set; }
        public double Egységár { get; private set; }
        public double Összesen { get; private set; }

        public Adat_Jármű_Takarítás_TIG(string telephely, string tevékenység, double mennyiség, string mE, double egységár, double összesen)
        {
            Telephely = telephely;
            Tevékenység = tevékenység;
            Mennyiség = mennyiség;
            ME = mE;
            Egységár = egységár;
            Összesen = összesen;
        }
    }
}
