using Villamos.Adatszerkezet;

namespace Villamos.Villamos_Adatbázis_Funkció
{
    public static partial class Adatbázis_Létrehozás
    {
        public static void Ciklusrendtábla(string hely)
        {
            string szöveg;
            string jelszó = "pocsaierzsi";
            string táblanév = "Ciklusrendtábla";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = $"CREATE TABLE {táblanév} (";
            szöveg += "[Típus]  char (15),";
            szöveg += "[Sorszám] Long,";
            szöveg += "[Vizsgálatfok]  char (10),";
            szöveg += "[Törölt]  char (1),";
            szöveg += "[névleges] Long,";
            szöveg += "[alsóérték] Long,";
            szöveg += "[felsőérték] Long)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            táblanév = "CiklusSorrendtábla";
            szöveg = $"CREATE TABLE {táblanév} (";
            szöveg += "[Sorszám] short,";
            szöveg += "[JárműTípus]  char (15),";
            szöveg += "[CiklusNév]  char (15))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }
    }
}
