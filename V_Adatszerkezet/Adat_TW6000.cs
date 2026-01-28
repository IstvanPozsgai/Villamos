using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_TW6000_Alap
    {
        public string Azonosító { get; set; }
        public string Ciklusrend { get; set; }
        public Boolean Kötöttstart { get; set; }
        public Boolean Megállítás { get; set; }
        public DateTime Start { get; set; }
        public DateTime Vizsgdátum { get; set; }
        public string Vizsgnév { get; set; }
        public int Vizsgsorszám { get; set; }

        public Adat_TW6000_Alap(string azonosító, string ciklusrend, bool kötöttstart, bool megállítás, DateTime start, DateTime vizsgdátum, string vizsgnév, int vizsgsorszám)
        {
            Azonosító = azonosító;
            Ciklusrend = ciklusrend;
            Kötöttstart = kötöttstart;
            Megállítás = megállítás;
            Start = start;
            Vizsgdátum = vizsgdátum;
            Vizsgnév = vizsgnév;
            Vizsgsorszám = vizsgsorszám;
        }
    }

    public class Adat_TW6000_Színezés
    {
        public double Szín { get; set; }
        public string Vizsgálatnév { get; set; }
        public Adat_TW6000_Színezés(double szín, string vizsgálatnév)
        {
            Szín = szín;
            Vizsgálatnév = vizsgálatnév;
        }
    }

    public class Adat_TW6000_Telephely
    {
        public int Sorrend { get; set; }
        public string Telephely { get; set; }
        public Adat_TW6000_Telephely(int sorrend, string telephely)
        {
            Sorrend = sorrend;
            Telephely = telephely;
        }

    }

    public class Adat_TW6000_Ütemezés
    {
        public string Azonosító { get; private set; }
        public string Ciklusrend { get; private set; }
        public bool Elkészült { get; private set; }
        public string Megjegyzés { get; private set; }
        public long Státus { get; private set; }
        public DateTime Velkészülés { get; private set; }
        public DateTime Vesedékesség { get; private set; }
        public string Vizsgfoka { get; private set; }
        public long Vsorszám { get; private set; }
        public DateTime Vütemezés { get; private set; }
        public string Vvégezte { get; private set; }

        public Adat_TW6000_Ütemezés(string azonosító, string ciklusrend, bool elkészült, string megjegyzés, long státus, DateTime velkészülés, DateTime vesedékesség, string vizsgfoka, long vsorszám, DateTime vütemezés, string vvégezte)
        {
            Azonosító = azonosító;
            Ciklusrend = ciklusrend;
            Elkészült = elkészült;
            Megjegyzés = megjegyzés;
            Státus = státus;
            Velkészülés = velkészülés;
            Vesedékesség = vesedékesség;
            Vizsgfoka = vizsgfoka;
            Vsorszám = vsorszám;
            Vütemezés = vütemezés;
            Vvégezte = vvégezte;
        }

        public Adat_TW6000_Ütemezés(string azonosító, string megjegyzés, long státus, DateTime vütemezés)
        {
            Azonosító = azonosító;
            Megjegyzés = megjegyzés;
            Státus = státus;
            Vütemezés = vütemezés;
        }

        /// <summary>
        /// Törléshez
        /// </summary>
        /// <param name="azonosító"></param>
        /// <param name="státus"></param>
        /// <param name="vütemezés"></param>
        public Adat_TW6000_Ütemezés(string azonosító, long státus, DateTime vütemezés)
        {
            Azonosító = azonosító;
            Státus = státus;
            Vütemezés = vütemezés;
        }
    }
    public class Adat_TW6000_AlapNapló
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
        public int Vizsgsorszám { get; set; }
        public Adat_TW6000_AlapNapló(string azonosító, string ciklusrend, bool kötöttstart, bool megállítás, string oka, DateTime rögzítésiidő, string rögzítő, DateTime start, DateTime vizsgdátum, string vizsgnév, int vizsgsorszám)
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

    public class Adat_TW6000_ÜtemNapló
    {
        public string Azonosító { get; private set; }
        public string Ciklusrend { get; private set; }
        public bool Elkészült { get; private set; }
        public string Megjegyzés { get; private set; }
        public DateTime Rögzítésideje { get; private set; }
        public string Rögzítő { get; set; }
        public long Státus { get; set; }
        public DateTime Velkészülés { get; private set; }
        public DateTime Vesedékesség { get; private set; }
        public string Vizsgfoka { get; private set; }
        public long Vsorszám { get; private set; }
        public DateTime Vütemezés { get; private set; }
        public string Vvégezte { get; private set; }

        public Adat_TW6000_ÜtemNapló(string azonosító, string ciklusrend, bool elkészült, string megjegyzés, DateTime rögzítésideje, string rögzítő, long státus, DateTime velkészülés, DateTime vesedékesség, string vizsgfoka, long vsorszám, DateTime vütemezés, string vvégezte)
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

    public class Adat_TW6000_Ütemezés_Plusz
    {
        public string Azonosító { get; private set; }
        public string Ciklusrend { get; private set; }
        public bool Elkészült { get; private set; }
        public string Megjegyzés { get; private set; }
        public long Státus { get; private set; }
        public DateTime Velkészülés { get; private set; }
        public DateTime Vesedékesség { get; private set; }
        public string Vizsgfoka { get; private set; }
        public long Vsorszám { get; private set; }
        public DateTime Vütemezés { get; private set; }
        public string Vvégezte { get; private set; }
        public string Telephely { get; private set; }

        public Adat_TW6000_Ütemezés_Plusz(string azonosító, string ciklusrend, bool elkészült, string megjegyzés, long státus, DateTime velkészülés, DateTime vesedékesség, string vizsgfoka, long vsorszám, DateTime vütemezés, string vvégezte, string telephely)
        {
            Azonosító = azonosító;
            Ciklusrend = ciklusrend;
            Elkészült = elkészült;
            Megjegyzés = megjegyzés;
            Státus = státus;
            Velkészülés = velkészülés;
            Vesedékesség = vesedékesség;
            Vizsgfoka = vizsgfoka;
            Vsorszám = vsorszám;
            Vütemezés = vütemezés;
            Vvégezte = vvégezte;
            Telephely = telephely;
        }
    }
}
