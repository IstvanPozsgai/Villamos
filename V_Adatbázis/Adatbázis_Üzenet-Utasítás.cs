using Villamos.Adatszerkezet;

namespace Villamos.Villamos_Adatbázis_Funkció
{
    public static partial class Adatbázis_Létrehozás
    {

        public static void ALÜzenetadatok(string hely)
        {
            string szöveg;
            string jelszó = "katalin";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Üzenetek (";
            szöveg += "[sorszám] DOUBLE,";
            szöveg += "[Szöveg] LONGTEXT,";
            szöveg += "[Írta] CHAR(15),";
            szöveg += "[mikor] DATE,";
            szöveg += "[válaszsorszám] DOUBLE";
            szöveg += ")";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Olvasás (";
            szöveg += "[sorszám] DOUBLE,";
            szöveg += "[ki] CHAR(15),";
            szöveg += "[üzenetid] DOUBLE,";
            szöveg += "[mikor] DATE,";
            szöveg += "[olvasva]  YESNO";
            szöveg += ")";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void UtasításadatokTábla(string hely)
        {
            string jelszó = "katalin";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);


            string szöveg;
            szöveg = "CREATE TABLE Üzenetek (";
            szöveg += "[sorszám] DOUBLE,";
            szöveg += "[Szöveg] LONGTEXT,";
            szöveg += "[Írta] CHAR(15),";
            szöveg += "[mikor] DATE,";
            szöveg += "[érvényes] DOUBLE";
            szöveg += ")";
            //Létrehozzuk az adatbázist
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Olvasás (";
            szöveg += "[sorszám] DOUBLE,";
            szöveg += "[ki] CHAR(15),";
            szöveg += "[üzenetid] DOUBLE,";
            szöveg += "[mikor] DATE,";
            szöveg += "[olvasva]  YESNO";
            szöveg += ")";
            //Létrehozzuk az adatbázist
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }
    }
}
