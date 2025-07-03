using System;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_CAF_alap
    {
        public string Azonosító { get; private set; }
        public string Ciklusnap { get; private set; }
        public string Utolsó_Nap { get; private set; }
        public long Utolsó_Nap_sorszám { get; private set; }
        public string Végezte_nap { get; private set; }
        public DateTime Vizsgdátum_nap { get; private set; }
        public string Cikluskm { get; private set; }
        public string Utolsó_Km { get; private set; }
        public long Utolsó_Km_sorszám { get; private set; }
        public string Végezte_km { get; private set; }
        public DateTime Vizsgdátum_km { get; private set; }
        public long Számláló { get; private set; }
        public long Havikm { get; private set; }
        public long KMUkm { get; private set; }
        public DateTime KMUdátum { get; private set; }
        public DateTime Fudátum { get; private set; }
        public long Teljeskm { get; private set; }
        public string Típus { get; private set; }
        public bool Garancia { get; private set; }
        public bool Törölt { get; private set; }

        public Adat_CAF_alap(string azonosító, string ciklusnap, string utolsó_Nap, long utolsó_Nap_sorszám, string végezte_nap, DateTime vizsgdátum_nap, string cikluskm, string utolsó_Km, long utolsó_Km_sorszám, string végezte_km, DateTime vizsgdátum_km, long számláló, long havikm, long kMUkm, DateTime kMUdátum, DateTime fudátum, long teljeskm, string típus, bool garancia, bool törölt)
        {
            Azonosító = azonosító;
            Ciklusnap = ciklusnap;
            Utolsó_Nap = utolsó_Nap;
            Utolsó_Nap_sorszám = utolsó_Nap_sorszám;
            Végezte_nap = végezte_nap;
            Vizsgdátum_nap = vizsgdátum_nap;
            Cikluskm = cikluskm;
            Utolsó_Km = utolsó_Km;
            Utolsó_Km_sorszám = utolsó_Km_sorszám;
            Végezte_km = végezte_km;
            Vizsgdátum_km = vizsgdátum_km;
            Számláló = számláló;
            Havikm = havikm;
            KMUkm = kMUkm;
            KMUdátum = kMUdátum;
            Fudátum = fudátum;
            Teljeskm = teljeskm;
            Típus = típus;
            Garancia = garancia;
            Törölt = törölt;
        }

        public Adat_CAF_alap(string azonosító, long havikm, long kMUkm, DateTime kMUdátum)
        {
            Azonosító = azonosító;
            Havikm = havikm;
            KMUkm = kMUkm;
            KMUdátum = kMUdátum;
        }

        public Adat_CAF_alap(string azonosító, string utolsó_Nap, long utolsó_Nap_sorszám, string végezte_nap, DateTime vizsgdátum_nap)
        {
            Azonosító = azonosító;
            Utolsó_Nap = utolsó_Nap;
            Utolsó_Nap_sorszám = utolsó_Nap_sorszám;
            Végezte_nap = végezte_nap;
            Vizsgdátum_nap = vizsgdátum_nap;
        }

        public Adat_CAF_alap(string azonosító, string utolsó_Km, long utolsó_Km_sorszám, string végezte_km, DateTime vizsgdátum_km, long számláló)
        {
            Azonosító = azonosító;
            Számláló = számláló;
            Utolsó_Km = utolsó_Km;
            Utolsó_Km_sorszám = utolsó_Km_sorszám;
            Végezte_km = végezte_km;
            Vizsgdátum_km = vizsgdátum_km;
        }
    }

    public class Adat_CAF_Adatok
    {
        public double Id { get; private set; }
        public string Azonosító { get; private set; }
        public string Vizsgálat { get; private set; }
        public DateTime Dátum { get; private set; }
        public DateTime Dátum_program { get; private set; }
        public long Számláló { get; private set; }
        public int Státus { get; private set; }
        public int KM_Sorszám { get; private set; }
        public int IDŐ_Sorszám { get; private set; }
        public int IDŐvKM { get; private set; }
        public string Megjegyzés { get; private set; }
        public bool KmRogzitett_e { get; private set; }
        public string Telephely { get; private set; }

        public Adat_CAF_Adatok(double id, string azonosító, string vizsgálat, DateTime dátum, DateTime dátum_program, long számláló, int státus, int kM_Sorszám, int iDŐ_Sorszám, int iDŐvKM, string megjegyzés)
        {
            Id = id;
            Azonosító = azonosító;
            Vizsgálat = vizsgálat;
            Dátum = dátum;
            Dátum_program = dátum_program;
            Számláló = számláló;
            Státus = státus;
            KM_Sorszám = kM_Sorszám;
            IDŐ_Sorszám = iDŐ_Sorszám;
            IDŐvKM = iDŐvKM;
            Megjegyzés = megjegyzés;
        }

        public Adat_CAF_Adatok(double id, string azonosító, string vizsgálat, DateTime dátum, DateTime dátum_program, long számláló, int státus, int kM_Sorszám, int iDŐ_Sorszám, int iDŐvKM, string megjegyzés, bool kmRogzitett_e, string telephely="")
        {
            Id = id;
            Azonosító = azonosító;
            Vizsgálat = vizsgálat;
            Dátum = dátum;
            Dátum_program = dátum_program;
            Számláló = számláló;
            Státus = státus;
            KM_Sorszám = kM_Sorszám;
            IDŐ_Sorszám = iDŐ_Sorszám;
            IDŐvKM = iDŐvKM;
            Megjegyzés = megjegyzés;
            KmRogzitett_e = kmRogzitett_e;
            Telephely = telephely;
        }
    }

    public class Adat_CAF_Szinezés
    {
        public string Telephely { get; private set; }
        public double SzínPSZgar { get; private set; }
        public double SzínPsz { get; private set; }
        public double SzínIStűrés { get; private set; }
        public double SzínIS { get; private set; }
        public double SzínP { get; private set; }
        public double Színszombat { get; private set; }
        public double SzínVasárnap { get; private set; }

        public double Szín_E { get; private set; }
        public double Szín_dollár { get; private set; }
        public double Szín_Kukac { get; private set; }
        public double Szín_Hasteg { get; private set; }
        public double Szín_jog { get; private set; }
        public double Szín_nagyobb { get; private set; }

        public Adat_CAF_Szinezés(string telephely, double színPSZgar, double színPsz, double színIStűrés, double színIS, double színP, double színszombat, double színVasárnap, double szín_E, double szín_dollár, double szín_Kukac, double szín_Hasteg, double szín_jog, double szín_nagyobb)
        {
            Telephely = telephely;
            SzínPSZgar = színPSZgar;
            SzínPsz = színPsz;
            SzínIStűrés = színIStűrés;
            SzínIS = színIS;
            SzínP = színP;
            Színszombat = színszombat;
            SzínVasárnap = színVasárnap;
            Szín_E = szín_E;
            Szín_dollár = szín_dollár;
            Szín_Kukac = szín_Kukac;
            Szín_Hasteg = szín_Hasteg;
            Szín_jog = szín_jog;
            Szín_nagyobb = szín_nagyobb;
        }
    }

    public class Adat_CAF_Alapnapló
    {
        public string Azonosító { get; private set; }
        public string Ciklusrend { get; private set; }
        public bool Kötöttstart { get; private set; }
        public bool Megállítás { get; private set; }
        public string Oka { get; private set; }
        public DateTime Rögzítésiidő { get; private set; }
        public string Rögzítő { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime Vizsgdátum { get; private set; }
        public string Vizsgnév { get; private set; }
        public long Vizsgsorszám { get; private set; }

        public Adat_CAF_Alapnapló(string azonosító, string ciklusrend, bool kötöttstart, bool megállítás, string oka, DateTime rögzítésiidő, string rögzítő, DateTime start, DateTime vizsgdátum, string vizsgnév, long vizsgsorszám)
        {
            Azonosító = azonosító;
            Ciklusrend = ciklusrend;
            Kötöttstart = kötöttstart;
            Megállítás = megállítás;
            Oka = oka;
            Rögzítésiidő = rögzítésiidő;
            Rögzítő = rögzítő;
            Start = start;
            Vizsgdátum = vizsgdátum;
            Vizsgnév = vizsgnév;
            Vizsgsorszám = vizsgsorszám;
        }
    }

    public class Adat_CAF_Ütemezésnapló
    {
        public string Azonosító { get; private set; }
        public string Ciklusrend { get; private set; }
        public bool Elkészült { get; private set; }
        public string Megjegyzés { get; private set; }
        public DateTime Rögzítésideje { get; private set; }
        public string Rögzítő { get; private set; }
        public long Státus { get; private set; }
        public DateTime Velkészülés { get; private set; }
        public DateTime Vesedékesség { get; private set; }
        public string Vizsgfoka { get; private set; }
        public long Vsorszám { get; private set; }
        public DateTime Vütemezés { get; private set; }
        public string Vvégezte { get; private set; }

        public Adat_CAF_Ütemezésnapló(string azonosító, string ciklusrend, bool elkészült, string megjegyzés, DateTime rögzítésideje, string rögzítő, long státus, DateTime velkészülés, DateTime vesedékesség, string vizsgfoka, long vsorszám, DateTime vütemezés, string vvégezte)
        {
            Azonosító = azonosító;
            Ciklusrend = ciklusrend;
            Elkészült = elkészült;
            Megjegyzés = megjegyzés;
            Rögzítésideje = rögzítésideje;
            Rögzítő = rögzítő;
            Státus = státus;
            Velkészülés = velkészülés;
            Vesedékesség = vesedékesség;
            Vizsgfoka = vizsgfoka;
            Vsorszám = vsorszám;
            Vütemezés = vütemezés;
            Vvégezte = vvégezte;
        }
    }

    public class Adat_CAF_Adatok_Pót
    {
        public string Azonosító { get; private set; }
        public DateTime Dátum { get; private set; }
        public DateTime Dátumtól { get; private set; }
        public DateTime Dátumig { get; private set; }
        public int Státus { get; private set; }
        public bool KmRogzitett_e { get; private set; }

        public Adat_CAF_Adatok_Pót(string azonosító, DateTime dátum, int státus)
        {
            Azonosító = azonosító;
            Dátum = dátum;
            Státus = státus;
        }

        public Adat_CAF_Adatok_Pót(string azonosító, DateTime dátumtól, DateTime dátumig, int státus)
        {
            Azonosító = azonosító;
            Dátumtól = dátumtól;
            Dátumig = dátumig;
            Státus = státus;
        }

        public Adat_CAF_Adatok_Pót(string azonosító, DateTime dátumtól, DateTime dátumig, int státus, bool kmrogzitett_e)
        {
            Azonosító = azonosító;
            Dátumtól = dátumtól;
            Dátumig = dátumig;
            Státus = státus;
            KmRogzitett_e = kmrogzitett_e;
        }
    }

}
