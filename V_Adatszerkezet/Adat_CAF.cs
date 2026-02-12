using System;

namespace Villamos.Adatszerkezet
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

}
