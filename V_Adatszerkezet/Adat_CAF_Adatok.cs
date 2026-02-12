using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
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

        public Adat_CAF_Adatok(double id, string azonosító, string vizsgálat, DateTime dátum, DateTime dátum_program, long számláló, int státus, int kM_Sorszám, int iDŐ_Sorszám, int iDŐvKM, string megjegyzés, bool kmRogzitett_e, string telephely = "")
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
}
