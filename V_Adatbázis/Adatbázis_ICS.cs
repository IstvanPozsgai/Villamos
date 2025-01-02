using Villamos.Adatszerkezet;

namespace Villamos.Villamos_Adatbázis_Funkció
{
    public static partial class Adatbázis_Létrehozás
    {
        public static void VillamostáblaICS(string hely)
        {
            string szöveg;
            string jelszó = "pozsgaii";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            // tábla létrehozása
            szöveg = "CREATE TABLE Állománytábla (";
            szöveg += "[azonosító]  char (10), ";
            szöveg += "[takarítás] DATE,";
            szöveg += "[E2]  Short,";
            szöveg += "[E3]  Short)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        public static void ElőtervkmfutástáblaICS(string hely)
        {
            string szöveg;
            string jelszó = "pocsaierzsi";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            //tábla létrehozása
            szöveg = "CREATE TABLE KMtábla (";
            szöveg += "[ID] Long,";
            szöveg += "[azonosító]  char (10),";
            szöveg += "[jjavszám] Long,";
            szöveg += "[KMUkm] Long,";
            szöveg += "[KMUdátum] DATE,";
            szöveg += "[vizsgfok]  char (10),";
            szöveg += "[vizsgdátumk] DATE,";
            szöveg += "[vizsgdátumv] DATE,";
            szöveg += "[vizsgkm] Long,";
            szöveg += "[havikm] Long,";
            szöveg += "[vizsgsorszám] Long,";
            szöveg += "[fudátum] DATE,";
            szöveg += "[Teljeskm] Long,";
            szöveg += "[Ciklusrend]  char (10),";
            szöveg += "[V2végezte]  char (50),";
            szöveg += "[KövV2_sorszám] Long,";
            szöveg += "[KövV2]  char (10),";
            szöveg += "[KövV_sorszám] Long,";
            szöveg += "[KövV]  char (10),";
            szöveg += "[törölt] YESNO, ";
            szöveg += "[Módosító]  char (50),";
            szöveg += "[Mikor] DATE,";

            szöveg += "[Honostelephely]  char (50),";
            szöveg += "[tervsorszám] Long,";

            szöveg += "[Kerék_K1]  double,";
            szöveg += "[Kerék_K2]  double,";
            szöveg += "[Kerék_K3]  double,";
            szöveg += "[Kerék_K4]  double,";
            szöveg += "[Kerék_K5]  double,";
            szöveg += "[Kerék_K6]  double,";
            szöveg += "[Kerék_K7]  double,";
            szöveg += "[Kerék_K8]  double,";
            szöveg += "[Kerék_min]  double,";

            szöveg += "[V2V3Számláló] Long)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }
    }
}
