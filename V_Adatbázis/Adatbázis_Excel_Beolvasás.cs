using Villamos.Adatszerkezet;

namespace Villamos.Villamos_Adatbázis_Funkció
{
    public static partial class Adatbázis_Létrehozás
    {
        public static void Adatbázis_Excel_Beolvasás(string hely)
        {
            string jelszó = "sajátmagam";
            string táblanév = "Tábla_Excel_Beolvasás";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            string szöveg = $"CREATE TABLE {táblanév} (";
            szöveg += " [csoport] CHAR(20), ";
            szöveg += " [oszlop] short,";
            szöveg += " [fejléc] CHAR(255),";
            szöveg += " [Státusz] yesno,";
            szöveg += " [Változónév] char(50))";

            //Létrehozzuk az adattáblát
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg, táblanév);
        }

    }
}

