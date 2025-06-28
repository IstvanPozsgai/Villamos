using Villamos.Adatszerkezet;

namespace Villamos.Villamos_Adatbázis_Funkció
{
    public static partial class Adatbázis_Létrehozás
    {
        public static void CAFtábla(string hely)
        {
            string szöveg;
            string jelszó = "CzabalayL";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            // tábla létrehozása
            szöveg = "CREATE TABLE Alap (";
            szöveg += "[azonosító]  char (10),";

            szöveg += "[Ciklusnap]  char (10),";
            szöveg += "[Utolsó_Nap]  char (10),";
            szöveg += "[Utolsó_Nap_sorszám] Long,";
            szöveg += "[Végezte_nap]  char (50),";
            szöveg += "[Vizsgdátum_nap] DATE,";

            szöveg += "[Cikluskm]  char (10),";
            szöveg += "[Utolsó_Km]  char (10),";
            szöveg += "[Utolsó_Km_sorszám] Long,";
            szöveg += "[Végezte_km]  char (50),";
            szöveg += "[Vizsgdátum_km] DATE,";
            szöveg += "[Számláló] Long,";

            szöveg += "[havikm] Long,";
            szöveg += "[KMUkm] Long,";
            szöveg += "[KMUdátum] DATE,";
            szöveg += "[fudátum] DATE,";
            szöveg += "[Teljeskm] Long,";
            szöveg += "[Típus]  char (10),";
            szöveg += "[Garancia] YESNO, ";
            szöveg += "[törölt] YESNO) ";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            // JAVÍTANDÓ:Nem jó szintaktika, a char (255) után kellene egy vessző
            // Adatok létrehozása
            szöveg = "CREATE TABLE Adatok (";
            szöveg += "[Id] Double,";
            szöveg += "[azonosító]  char (10),";
            szöveg += "[Vizsgálat]  char (10),";
            szöveg += "[Dátum] DATE,";
            szöveg += "[Dátum_program] DATE,";
            szöveg += "[Számláló] Long,";
            szöveg += "[Státus] SHORT,";
            szöveg += "[KM_Sorszám] SHORT,";
            szöveg += "[IDŐ_Sorszám] SHORT,";
            szöveg += "[IDŐvKM] SHORT,";
            szöveg += "[Megjegyzés]  char (255)";
            szöveg += "[KmRogzitett_e] YESNO))";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            // Szin tábla
            szöveg = "CREATE TABLE szinezés (";
            szöveg += "[telephely]  char (50),";
            szöveg += "[SzínPSZgar] DOUBLE,";
            szöveg += "[SzínPsz] DOUBLE,";
            szöveg += "[SzínIStűrés] DOUBLE,";
            szöveg += "[SzínIS] DOUBLE,";
            szöveg += "[SzínP] DOUBLE,";
            szöveg += "[Színszombat] DOUBLE,";
            szöveg += "[SzínVasárnap] DOUBLE,";

            szöveg += "[Szín_E] DOUBLE,";
            szöveg += "[Szín_dollár] DOUBLE,";
            szöveg += "[Szín_Kukac] DOUBLE,";
            szöveg += "[Szín_Hasteg] DOUBLE,";
            szöveg += "[Szín_jog] DOUBLE,";
            szöveg += "[Szín_nagyobb] DOUBLE)";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void CAFAdatokArchív(string hely)
        {
            string szöveg;
            string jelszó = "CzabalayL";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            // Adatok létrehozása
            szöveg = "CREATE TABLE Adatok (";
            szöveg += "[Id] Double,";
            szöveg += "[azonosító]  char (10),";
            szöveg += "[Vizsgálat]  char (10),";
            szöveg += "[Dátum] DATE,";
            szöveg += "[Dátum_program] DATE,";
            szöveg += "[Számláló] Long,";
            szöveg += "[Státus] SHORT,";
            szöveg += "[KM_Sorszám] SHORT,";
            szöveg += "[IDŐ_Sorszám] SHORT,";
            szöveg += "[IDŐvKM] SHORT,";
            szöveg += "[Megjegyzés]  char (255)";
            szöveg += "[KmRogzitett_e] YESNO))";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }
    }
}
