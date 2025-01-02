using Villamos.Adatszerkezet;

namespace Villamos.Villamos_Adatbázis_Funkció
{
    public static partial class Adatbázis_Létrehozás
    {
        public static void TTP_Adatbázis(string hely)
        {
            string szöveg;
            string jelszó = "rudolfg";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE TTP_Alapadat (";
            szöveg += "[Azonosító] CHAR(10),";
            szöveg += "[Gyártási_Év] DATE,";
            szöveg += "[TTP] YESNO,";
            szöveg += "[Megjegyzés] CHAR(255))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE TTP_Naptár (";
            szöveg += "[Dátum] DATE,";
            szöveg += "[Munkanap] YESNO)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE TTP_Tábla (";
            szöveg += "[Azonosító] CHAR(10),";
            szöveg += "[Lejárat_Dátum] DATE,";
            szöveg += "[Ütemezés_Dátum] DATE,";
            szöveg += "[TTP_Dátum] DATE,";
            szöveg += "[TTP_Javítás] YESNO,";
            szöveg += "[Rendelés] CHAR(10),";
            szöveg += "[JavBefDát] DATE,";
            szöveg += "[Együtt] CHAR(50),";
            szöveg += "[Státus] SHORT,";
            szöveg += "[Megjegyzés] CHAR(255))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE TTP_Év (";
            szöveg += "[Év] SHORT,";
            szöveg += "[Életkor] SHORT)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

    }
}
