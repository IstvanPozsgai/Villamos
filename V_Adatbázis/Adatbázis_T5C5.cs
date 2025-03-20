using Villamos.Adatszerkezet;

namespace Villamos.Villamos_Adatbázis_Funkció
{
    public static partial class Adatbázis_Létrehozás
    {
        public static void Kmfutástábla(string hely)
        {
            string szöveg;
            string jelszó = "pocsaierzsi";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            // tábla létrehozása
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
            szöveg += "[V2V3Számláló] Long)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void KmfutástáblaNapló(string hely)
        {
            string szöveg;
            string jelszó = "pocsaierzsi";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            // tábla létrehozása
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE KMtáblaNapló (";
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
            szöveg += "[V2V3Számláló] Long)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Előtervkmfutástábla(string hely)
        {
            string szöveg;
            string jelszó = "pocsaierzsi";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            // tábla létrehozása
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
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

            szöveg += "[Kerék_K11]  double,";
            szöveg += "[Kerék_K12]  double,";
            szöveg += "[Kerék_K21]  double,";
            szöveg += "[Kerék_K22]  double,";
            szöveg += "[Kerék_min]  double,";

            szöveg += "[V2V3Számláló] Long)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void T5C5_fűtés_tábla(string hely)
        {
            string szöveg;
            string jelszó = "RózsahegyiK";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            // tábla létrehozása
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE Fűtés_tábla (";
            szöveg += "[ID] Long,";
            szöveg += "[pályaszám]  char (10),";
            szöveg += "[telephely]  char (50),";
            szöveg += "[dátum] DATE,";
            szöveg += "[dolgozó]  char (50),";

            szöveg += "[I_szakasz] double,";
            szöveg += "[II_szakasz] double,";
            szöveg += "[fűtés_típusa]  short,";
            szöveg += "[Jófűtés]  char (20),";

            szöveg += "[Megjegyzés]  char (255),";
            szöveg += "[Beállítási_értékek]  short,";

            szöveg += "[Módosító]  char (50),";
            szöveg += "[Mikor] DATE)";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Futásnaptábla_Létrehozás(string hely)
        {
            string szöveg;
            string jelszó = "pozsgaii";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE Állománytábla (";
            szöveg += "[azonosító] CHAR(8),";
            szöveg += "[utolsórögzítés] DATE,";
            szöveg += "[vizsgálatdátuma] DATE,";
            szöveg += "[utolsóforgalminap] DATE,";
            szöveg += "[Vizsgálatfokozata] CHAR(4),";
            szöveg += "[vizsgálatszáma] SHORT,";
            szöveg += "[futásnap] SHORT, ";
            szöveg += "[telephely] CHAR(20) )";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Dátumtábla (";
            szöveg += "[telephely] CHAR(20),";
            szöveg += "[utolsórögzítés] DATE)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Havifutástábla_Létrehozás(string hely)
        {
            string szöveg;
            string jelszó = "pozsgaii";
            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE Állománytábla (";
            szöveg += "[azonosító] CHAR(8),";
            szöveg += "[N1] CHAR(4),";
            szöveg += "[N2] CHAR(4),";
            szöveg += "[N3] CHAR(4),";
            szöveg += "[N4] CHAR(4),";
            szöveg += "[N5] CHAR(4),";
            szöveg += "[N6] CHAR(4),";
            szöveg += "[N7] CHAR(4),";
            szöveg += "[N8] CHAR(4),";
            szöveg += "[N9] CHAR(4),";
            szöveg += "[N10] CHAR(4),";
            szöveg += "[N11] CHAR(4),";
            szöveg += "[N12] CHAR(4),";
            szöveg += "[N13] CHAR(4),";
            szöveg += "[N14] CHAR(4),";
            szöveg += "[N15] CHAR(4),";
            szöveg += "[N16] CHAR(4),";
            szöveg += "[N17] CHAR(4),";
            szöveg += "[N18] CHAR(4),";
            szöveg += "[N19] CHAR(4),";
            szöveg += "[N20] CHAR(4),";
            szöveg += "[N21] CHAR(4),";
            szöveg += "[N22] CHAR(4),";
            szöveg += "[N23] CHAR(4),";
            szöveg += "[N24] CHAR(4),";
            szöveg += "[N25] CHAR(4),";
            szöveg += "[N26] CHAR(4),";
            szöveg += "[N27] CHAR(4),";
            szöveg += "[N28] CHAR(4),";
            szöveg += "[N29] CHAR(4),";
            szöveg += "[N30] CHAR(4),";
            szöveg += "[N31] CHAR(4),";
            szöveg += "[futásnap] SHORT,";
            szöveg += "[telephely] CHAR(20) )";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Vezényléstábla(string hely)
        {
            string szöveg;
            string jelszó = "tápijános";
            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE vezényléstábla (";
            szöveg += "[azonosító] CHAR(10),";
            szöveg += "[Dátum] DATE,";
            szöveg += "[Státus] SHORT, ";
            szöveg += "[vizsgálatraütemez] SHORT, ";
            szöveg += "[takarításraütemez] SHORT, ";
            szöveg += "[vizsgálat] CHAR(10),";
            szöveg += "[vizsgálatszám] SHORT, ";
            szöveg += "[rendelésiszám] CHAR(15),";
            szöveg += "[álljon] SHORT, ";
            szöveg += "[fusson] SHORT, ";
            szöveg += "[törlés] SHORT, ";
            szöveg += "[szerelvényszám] Long, ";
            szöveg += "[típus] char(10))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Kiadáshétvége(string hely)
        {
            string szöveg;
            string jelszó = "pozsgaii";
            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE előírás (";
            szöveg += "[id] Long,";
            szöveg += "[vonal]  char (20),";
            szöveg += "[Mennyiség] long, ";
            szöveg += "[red] long,";
            szöveg += "[green] long,";
            szöveg += "[blue] long)";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            // tábla létrehozása
            szöveg = "CREATE TABLE beosztás (";
            szöveg += "[id] Long,";
            szöveg += "[vonal]  char (20),";
            szöveg += "[kocsi1] CHAR(10), ";
            szöveg += "[kocsi2] CHAR(10), ";
            szöveg += "[kocsi3] CHAR(10), ";
            szöveg += "[kocsi4] CHAR(10), ";
            szöveg += "[kocsi5] CHAR(10), ";
            szöveg += "[kocsi6] CHAR(10), ";
            szöveg += "[vissza1] CHAR(1), ";
            szöveg += "[vissza2] CHAR(1), ";
            szöveg += "[vissza3] CHAR(1), ";
            szöveg += "[vissza4] CHAR(1), ";
            szöveg += "[vissza5] CHAR(1), ";
            szöveg += "[vissza6] CHAR(1))  ";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);


        }
    }
}
