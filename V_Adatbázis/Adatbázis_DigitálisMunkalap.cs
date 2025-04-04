using Villamos.Adatszerkezet;

namespace Villamos.Villamos_Adatbázis_Funkció
{
    public static partial class Adatbázis_Létrehozás
    {
        public static void DigitálisMunkalap(string hely)
        {
            string szöveg;
            string jelszó = "";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE FejTábla (";
            szöveg += "[Id] long,";
            szöveg += "[típus] CHAR(20),";
            szöveg += "[Karbantartási_fokozat] char(20), ";
            szöveg += "[EllDolgozóNév] CHAR(50),";
            szöveg += "[EllDolgozószám] CHAR(8),";
            szöveg += "[Telephely] CHAR(30),";
            szöveg += "[Dátum] DATE)";


            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE DolgozóTábla (";
            szöveg += "[Fej_Id] long,";
            szöveg += "[DolgozóNév] CHAR(50),";
            szöveg += "[Dolgozószám] CHAR(8),";
            szöveg += "[Technológia_Id] long)";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE KocsikTábla (";
            szöveg += "[Fej_Id] long,";
            szöveg += "[azonosító] CHAR(10),";
            szöveg += "[KMU] long,";
            szöveg += "[rendelés] CHAR(20))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

    }
}
