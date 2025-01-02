using Villamos.Adatszerkezet;

namespace Villamos.Villamos_Adatbázis_Funkció
{
    public static partial class Adatbázis_Létrehozás
    {
        public static void Külsős_Táblák(string hely)
        {

            string szöveg;
            string jelszó = "Janda";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE Cégek (";
            szöveg += "[Cégid]  double, ";
            szöveg += "[Cég]  char(200), ";
            szöveg += "[Címe]  char(200), ";
            szöveg += "[Cég_email]  char(200), ";
            szöveg += "[Felelős_személy]  char(200), ";
            szöveg += "[Felelős_telefonszám]  char(200), ";
            szöveg += "[Munkaleírás]  char(200), ";
            szöveg += "[Mikor]  char(200), ";
            szöveg += "[Érv_kezdet] DATE,";
            szöveg += "[Érv_vég] DATE,";
            szöveg += "[Engedélyezés_dátuma] DATE,";
            szöveg += "[Engedélyező]  char(200), ";
            szöveg += "[Engedély]  short, ";
            szöveg += "[Státus]  yesno, ";
            szöveg += "[Terület]  char(20)) ";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Gépjárművek (";
            szöveg += "[id]  double, ";
            szöveg += "[Frsz]  char (20), ";
            szöveg += "[Cégid]  double, ";
            szöveg += "[Státus]  yesno) ";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Dolgozók (";
            szöveg += "[id]  double, ";
            szöveg += "[Név]  char (200), ";
            szöveg += "[Okmányszám]  char (200), ";
            szöveg += "[Anyjaneve]  char (200), ";
            szöveg += "[Születésihely]  char (200), ";
            szöveg += "[Születésiidő] DATE,";
            szöveg += "[Cégid]  double, ";
            szöveg += "[Státus]  yesno) ";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Telephelyek (";
            szöveg += "[id]  double, ";
            szöveg += "[Telephely]  char (50), ";
            szöveg += "[Cégid]  double, ";
            szöveg += "[Státus]  yesno) ";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Email (";
            szöveg += "[id]  double, ";
            szöveg += "[Másolat]  LONGTEXT, ";
            szöveg += "[Aláírás]  LONGTEXT) ";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

    }
}
