using Villamos.Adatszerkezet;

namespace Villamos.Villamos_Adatbázis_Funkció
{
    public static partial class Adatbázis_Létrehozás
    {
        public static void FődarabNóta(string hely)
        {
            string szöveg;
            string jelszó = "TörökKasos";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Nóta_Adatok (";
            szöveg += "[Id]  long,";
            szöveg += "[Berendezés]  char (10),";
            szöveg += "[Készlet_Sarzs]  char (3),";
            szöveg += "[Raktár]  char (5),";
            szöveg += "[Telephely]  char (50),";
            szöveg += "[Forgóváz]  char (10),";
            szöveg += "[Beépíthető]  yesno,";
            szöveg += "[MűszakiM]  LONGTEXT,";
            szöveg += "[OsztásiM]  LONGTEXT,";
            szöveg += "[Dátum] DATE,";
            szöveg += "[Státus]  short)";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

    }
}
