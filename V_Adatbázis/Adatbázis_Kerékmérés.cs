using Villamos.Adatszerkezet;

namespace Villamos.Villamos_Adatbázis_Funkció
{
    public static partial class Adatbázis_Létrehozás
    {
        public static void Kerékbeolvasástábla(string hely)
        {
            string szöveg;
            string jelszó = "szabólászló";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            // tábla létrehozása
            szöveg = "CREATE TABLE tábla (";
            szöveg += "[kerékberendezés]  char (10),";
            szöveg += "[kerékmegnevezés]  char (255),";
            szöveg += "[kerékgyártásiszám]  char (30),";
            szöveg += "[föléberendezés]  char (10),";
            szöveg += "[azonosító]  char (10),";
            szöveg += "[pozíció]  char (10),";
            szöveg += "[Dátum] DATE,";
            szöveg += "[objektumfajta]  char (20))";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            Kerék_Eszterga_Beállítás(hely);

        }

        public static void Kerék_Eszterga_Beállítás(string hely)
        {
            string szöveg;
            string jelszó = "szabólászló";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            // tábla létrehozása
            szöveg = "CREATE TABLE Eszterga_Beállítás (";
            szöveg += "[Azonosító]  char (10),";
            szöveg += "[KM_lépés]  long,";
            szöveg += "[Idő_lépés]  long,";
            szöveg += "[KM_IDŐ]  yesno,";
            szöveg += "[Ütemezve]  Date)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Kerék_Pozíció(string hely)
        {
            string szöveg;
            string jelszó = "szabólászló";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            // tábla létrehozása
            szöveg = "CREATE TABLE Eszterga_Beállítás (";
            szöveg += "[Azonosító]  char (10),";
            szöveg += "[KM_lépés]  long,";
            szöveg += "[Idő_lépés]  long,";
            szöveg += "[KM_IDŐ]  yesno,";
            szöveg += "[Ütemezve]  Date)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }


        public static void Méréstáblakerék(string hely)
        {
            string szöveg;
            string jelszó = "szabólászló";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE keréktábla (";
            szöveg += "[azonosító]  char (10),";
            szöveg += "[pozíció]  char (10),";
            szöveg += "[kerékberendezés]  char (10),";
            szöveg += "[kerékgyártásiszám]  char (30),";
            szöveg += "[Állapot]  char (20),";
            szöveg += "[Méret] SHORT,";
            szöveg += "[Módosító]  char (20),";
            szöveg += "[Mikor] DATE,";
            szöveg += "[Oka]  char (20),";
            szöveg += "[SAP] SHORT)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            // tábla létrehozása
            szöveg = "CREATE TABLE erőtábla (";
            szöveg += "[azonosító]  char (10),";
            szöveg += "[Van]  char (1),";
            szöveg += "[Módosító]  char (20),";
            szöveg += "[Mikor] DATE)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            // tábla létrehozása
            szöveg = "CREATE TABLE esztergatábla (";
            szöveg += "[azonosító]  char (10),";
            szöveg += "[Eszterga]  Date,";
            szöveg += "[Módosító]  char (20),";
            szöveg += "[Mikor] DATE, ";
            szöveg += "[KMU] long )";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }
    }
}
