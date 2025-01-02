using Villamos.Adatszerkezet;

namespace Villamos.Villamos_Adatbázis_Funkció
{
    public static partial class Adatbázis_Létrehozás
    {

        public static void Kieg1_Telephely_Felmentés(string hely)
        {
            string szöveg;
            string jelszó = "Mocó";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            szöveg = "CREATE TABLE Felmentés (";
            szöveg += "[id]  short,";
            szöveg += "[Címzett]  char (255),";
            szöveg += "[Másolat]  char (255),";
            szöveg += "[Tárgy]  char (255),";
            szöveg += "[Kértvizsgálat]  char (20),";
            szöveg += "[Bevezetés]  memo,";
            szöveg += "[Tárgyalás]  memo,";
            szöveg += "[Befejezés]  memo,";
            szöveg += "[CiklusTípus]  char (10))";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }
    }
}
