using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_Eszköz
    {

        public string Eszköz { get; private set; }
        public string Alszám { get; private set; }
        public string Megnevezés { get; private set; }
        public string Megnevezés_folyt { get; private set; }
        public string Gyártási_szám { get; private set; }
        public string Leltárszám { get; private set; }
        public DateTime Leltár_dátuma { get; private set; }
        public double Mennyiség { get; private set; }
        public string Bázis_menny_egység { get; private set; }
        public DateTime Aktiválás_dátuma { get; private set; }
        public string Telephely { get; private set; }
        public string Telephely_megnevezése { get; private set; }
        public string Helyiség { get; private set; }
        public string Helyiség_megnevezés { get; private set; }
        public string Gyár { get; private set; }
        public string Leltári_költséghely { get; private set; }
        public string Vonalkód { get; private set; }
        public DateTime Leltár_forduló_nap { get; private set; }
        public string Szemügyi_törzsszám { get; private set; }
        public string Dolgozó_neve { get; private set; }
        public DateTime Deaktiválás_dátuma { get; private set; }
        public string Eszközosztály { get; private set; }
        public string Üzletág { get; private set; }
        public string Cím { get; private set; }
        public string Költséghely { get; private set; }
        public string Felelős_költséghely { get; private set; }
        public string Régi_leltárszám { get; private set; }
        public bool Vonalkódozható { get; private set; }
        public string Rendszám_pályaszám { get; private set; }
        public string Épület_Szerszám { get; private set; }
        public bool Épület_van { get; private set; }
        public bool Szerszám_van { get; private set; }
        public bool Státus { get; private set; }

        public Adat_Eszköz(string eszköz, string alszám, string megnevezés, string megnevezés_folyt, string gyártási_szám,
            string leltárszám, DateTime leltár_dátuma, double mennyiség, string bázis_menny_egység, DateTime aktiválás_dátuma,
            string telephely, string telephely_megnevezése, string helyiség, string helyiség_megnevezés, string gyár, string leltári_költséghely,
            string vonalkód, DateTime leltár_forduló_nap, string szemügyi_törzsszám, string dolgozó_neve, DateTime deaktiválás_dátuma,
            string eszközosztály, string üzletág, string cím, string költséghely, string felelős_költséghely, string régi_leltárszám,
            bool vonalkódozható, string rendszám_pályaszám, string épület_Szerszám, bool épület_van, bool szerszám_van, bool státus)
        {
            Eszköz = eszköz;
            Alszám = alszám;
            Megnevezés = megnevezés;
            Megnevezés_folyt = megnevezés_folyt;
            Gyártási_szám = gyártási_szám;
            Leltárszám = leltárszám;
            Leltár_dátuma = leltár_dátuma;
            Mennyiség = mennyiség;
            Bázis_menny_egység = bázis_menny_egység;
            Aktiválás_dátuma = aktiválás_dátuma;
            Telephely = telephely;
            Telephely_megnevezése = telephely_megnevezése;
            Helyiség = helyiség;
            Helyiség_megnevezés = helyiség_megnevezés;
            Gyár = gyár;
            Leltári_költséghely = leltári_költséghely;
            Vonalkód = vonalkód;
            Leltár_forduló_nap = leltár_forduló_nap;
            Szemügyi_törzsszám = szemügyi_törzsszám;
            Dolgozó_neve = dolgozó_neve;
            Deaktiválás_dátuma = deaktiválás_dátuma;
            Eszközosztály = eszközosztály;
            Üzletág = üzletág;
            Cím = cím;
            Költséghely = költséghely;
            Felelős_költséghely = felelős_költséghely;
            Régi_leltárszám = régi_leltárszám;
            Vonalkódozható = vonalkódozható;
            Rendszám_pályaszám = rendszám_pályaszám;
            Épület_Szerszám = épület_Szerszám;
            Épület_van = épület_van;
            Szerszám_van = szerszám_van;
            Státus = státus;
        }

        public Adat_Eszköz(string eszköz, string épület_Szerszám)
        {
            Eszköz = eszköz;
            Épület_Szerszám = épület_Szerszám;
        }

        public Adat_Eszköz(string eszköz, bool épület_van, bool szerszám_van)
        {
            Eszköz = eszköz;
            Épület_van = épület_van;
            Szerszám_van = szerszám_van;
        }
    }
}
