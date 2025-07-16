using Villamos.Adatszerkezet;

namespace Villamos.Villamos_Adatbázis_Funkció
{
    public static partial class Adatbázis_Létrehozás
    {

        public static void TW6000tábla(string hely)
        {
            string szöveg;
            string jelszó = "czapmiklós";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE Alap (";
            szöveg += "[azonosító]  char (10),";
            szöveg += "[Ciklusrend]  char (15),";
            szöveg += "[kötöttstart] YESNO, ";
            szöveg += "[megállítás] YESNO, ";
            szöveg += "[start] DATE,";
            szöveg += "[vizsgdátum] DATE,";
            szöveg += "[vizsgnév]  char (10),";
            szöveg += "[vizsgsorszám] Long)";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            // tábla létrehozása
            szöveg = "CREATE TABLE szinezés (";
            szöveg += "[szín] DOUBLE,";
            szöveg += "[vizsgálatnév]  char (10))";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            // tábla létrehozása
            szöveg = "CREATE TABLE telephely (";
            szöveg += "[sorrend] Long,";
            szöveg += "[telephely]  char (50))";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            // tábla létrehozása
            szöveg = "CREATE TABLE Ütemezés (";
            szöveg += "[azonosító]  char (10),";
            szöveg += "[Ciklusrend]  char (10),";
            szöveg += "[Elkészült] YESNO, ";
            szöveg += "[Megjegyzés]  char (255),";
            szöveg += "[státus] Long,";
            szöveg += "[velkészülés] DATE,";
            szöveg += "[vesedékesség] DATE,";
            szöveg += "[vizsgfoka]  char (10),";
            szöveg += "[vsorszám] Long,";
            szöveg += "[vütemezés] DATE,";
            szöveg += "[Vvégezte]  char (50))";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        public static void TW6000táblanapló(string hely)
        {
            string szöveg;
            string jelszó = "czapmiklós";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE Alapnapló (";
            szöveg += "[azonosító]  char (10),";
            szöveg += "[Ciklusrend]  char (15),";
            szöveg += "[kötöttstart] YESNO, ";
            szöveg += "[megállítás] YESNO, ";
            szöveg += "[Oka]  char (255),";
            szöveg += "[rögzítésiidő] DATE,";
            szöveg += "[rögzítő]  char (255),";
            szöveg += "[start] DATE,";
            szöveg += "[vizsgdátum] DATE,";
            szöveg += "[vizsgnév]  char (10),";
            szöveg += "[vizsgsorszám] Long)";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        public static void TW6000ütemnapló(string hely)
        {
            string szöveg;
            string jelszó = "czapmiklós";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE ütemezésnapló (";
            szöveg += "[azonosító]  char (10),";
            szöveg += "[Ciklusrend]  char (15),";
            szöveg += "[Elkészült] YESNO, ";
            szöveg += "[Megjegyzés]  char (255),";
            szöveg += "[rögzítésideje] DATE,";
            szöveg += "[rögzítő]  char (50),";
            szöveg += "[státus] Long,";
            szöveg += "[velkészülés] DATE,";
            szöveg += "[vesedékesség] DATE,";
            szöveg += "[vizsgfoka]  char (10),";
            szöveg += "[vsorszám] Long,";
            szöveg += "[vütemezés] DATE,";
            szöveg += "[Vvégezte]  char (50))";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }
    }
}
