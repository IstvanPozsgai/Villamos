using Villamos.Adatszerkezet;

namespace Villamos.Villamos_Adatbázis_Funkció
{
    public static partial class Adatbázis_Létrehozás
    {

        public static void Osztálytábla(string hely)
        {
            string szöveg;
            string jelszó = "kéménybe";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE osztályadatok (";
            szöveg += "[Azonosító]  char (10)) ";



            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Osztálytábla (";
            szöveg += "[id] int,";
            szöveg += "[Osztálynév]  char (50), ";
            szöveg += "[Osztálymező]  char (50), ";
            szöveg += "[Használatban]  yesno) ";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }
    }
}
