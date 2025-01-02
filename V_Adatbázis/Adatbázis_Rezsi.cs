using Villamos.Adatszerkezet;

namespace Villamos.Villamos_Adatbázis_Funkció
{
    public static partial class Adatbázis_Létrehozás
    {
        public static void Rezsitörzs(string hely)
        {
            string szöveg;

            string jelszó = "csavarhúzó";
            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE törzs (";
            szöveg += "[azonosító]  char (18), ";
            szöveg += "[Megnevezés]  char (50), ";
            szöveg += "[Méret]  char (20), ";
            szöveg += "[státus] short, ";
            szöveg += "[csoport]  char (20)) ";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Rezsihely(string hely)
        {
            string szöveg;

            string jelszó = "csavarhúzó";
            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE tábla (";
            szöveg += "[azonosító]  char (18), ";
            szöveg += "[Állvány]  char (15), ";
            szöveg += "[Polc]  char (15), ";
            szöveg += "[helyiség]  char (15), ";
            szöveg += "[megjegyzés]  char (254))";


            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Rezsilistanapló(string hely)
        {
            string szöveg;

            string jelszó = "csavarhúzó";
            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE napló (";
            szöveg += "[azonosító]  char (18), ";
            szöveg += "[Honnan]  char (70), ";
            szöveg += "[Hova]  char (70), ";
            szöveg += "[mennyiség] double, ";
            szöveg += "[Mirehasznál]  char (50), ";
            szöveg += "[Módosította]  char (50), ";
            szöveg += "[módosításidátum] DATE,";
            szöveg += "[státus] yesno) ";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        public static void Rezsilista(string hely)
        {
            string szöveg;

            string jelszó = "csavarhúzó";
            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE könyv (";
            szöveg += "[azonosító]  char (18), ";
            szöveg += "[mennyiség] double, ";
            szöveg += "[dátum] DATE,";
            szöveg += "[státus] yesno) ";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

    }
}
