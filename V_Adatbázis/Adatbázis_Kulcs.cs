using Villamos.Adatszerkezet;

namespace Villamos.Villamos_Adatbázis_Funkció
{
    public static partial class Adatbázis_Létrehozás
    {

        public static void Kulcs_Adatok(string hely)
        {
            string szöveg;
            string jelszó = "Tóth_Katalin";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE ADAT (";
            szöveg += "[ADAT1] CHAR(50),";
            szöveg += "[Adat2] CHAR(50),";
            szöveg += "[Adat3] CHAR(50) ";
            szöveg += ")";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        public static void Kulcs_Adatok_Kettő(string hely)
        {
            string szöveg;
            string jelszó = "fütyülősbarack";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE Adattábla (";
            szöveg += "[ADAT1] CHAR(50),";
            szöveg += "[Adat2] CHAR(50))";


            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        /// <summary>
        /// Ez a főmérkökségi tábla más jelszóval
        /// </summary>
        /// <param name="hely"></param>
        public static void Felhasználó_Extra(string hely)
        {
            string szöveg;
            string jelszó = "Fekete_Könyv";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE ADAT (";
            szöveg += "[ADAT1] CHAR(50),";
            szöveg += "[Adat2] CHAR(50),";
            szöveg += "[Adat3] CHAR(50)";
            szöveg += ")";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

    }
}
