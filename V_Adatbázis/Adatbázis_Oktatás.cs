using Villamos.Adatszerkezet;

namespace Villamos.Villamos_Adatbázis_Funkció
{
    public static partial class Adatbázis_Létrehozás
    {
        public static void Oktatás_ALAP(string hely)
        {
            string szöveg;
            string jelszó = "pázmányt";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE OKtatásisegéd (";
            szöveg += "[IDoktatás]  long,";
            szöveg += "[telephely]  char (50),";
            szöveg += "[oktatásoka]  char (255),";
            szöveg += "[Oktatástárgya]  LONGTEXT,";
            szöveg += "[Oktatáshelye]  char (255),";
            szöveg += "[oktatásidőtartama] long,";
            szöveg += "[Oktató]  char (255),";
            szöveg += "[Oktatóbeosztása]  char (255),";
            szöveg += "[Egyébszöveg]  LONGTEXT";
            szöveg += "[email]  char (255),";
            szöveg += "[oktatás]  long)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);


            szöveg = "CREATE TABLE Oktatásrajelöltek (";
            szöveg += "[HRazonosító]  char (10),";
            szöveg += "[IDoktatás]  long,";
            szöveg += "[mikortól]  date,";
            szöveg += "[Státus]  long,";
            szöveg += "[telephely]  char (50))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);


            szöveg = "CREATE TABLE OKtatástábla (";
            szöveg += "[IDoktatás]  long,";
            szöveg += "[Téma]  char (255),";
            szöveg += "[Kategória]  char (255),";
            szöveg += "[gyakoriság]   char (25),";
            szöveg += "[státus]  char (25),";
            szöveg += "[dátum] date,";
            szöveg += "[telephely]  char (50),";
            szöveg += "[listázásisorrend]  long,";
            szöveg += "[ismétlődés]  long";
            szöveg += "[PDFfájl]  char (255))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Oktatás_Napló(string hely)
        {
            string szöveg;
            string jelszó = "pázmányt";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE OKtatásnapló (";
            szöveg += "[ID]  long,";
            szöveg += "[HRazonosító]  char (10),";
            szöveg += "[IDoktatás]  long,";
            szöveg += "[Oktatásdátuma] date,";
            szöveg += "[Kioktatta]  char (100),";
            szöveg += "[Rögzítésdátuma] date,";
            szöveg += "[telephely]  char (50),";
            szöveg += "[PDFFájlneve]  char (255),";
            szöveg += "[Számonkérés]  long,";
            szöveg += "[státus]  long,";
            szöveg += "[Rögzítő]  char (100),";
            szöveg += "[Megjegyzés]  char (255))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }
    }
}
