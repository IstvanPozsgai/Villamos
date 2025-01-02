using Villamos.Adatszerkezet;

namespace Villamos.Villamos_Adatbázis_Funkció
{
    public static partial class Adatbázis_Létrehozás
    {

        public static void Futásnaptábla_Nosztalgia(string hely)
        {
            string szöveg;
            string jelszó = "kloczkal";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Állomány (";

            szöveg += "[azonosító]  char (30),";
            szöveg += "[gyártó]  char (60),";
            szöveg += "[év]  short,";
            szöveg += "[Ntípus]  char (30),";
            szöveg += "[eszközszám]  char (30),";
            szöveg += "[leltári_szám]  char (30),";

            szöveg += "[vizsgálatdátuma] DATE,";
            szöveg += "[vizsgálatfokozata] CHAR(4),";
            szöveg += "[vizsgálatszáma]  CHAR(4),";

            szöveg += "[utolsóforgalminap] DATE,";
            szöveg += "[futásnap] SHORT, ";

            szöveg += "[km_v]  short,";
            szöveg += "[km_u]  short,";

            szöveg += "[utolsórögzítés] DATE,";
            szöveg += "[telephely] CHAR(20) )";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void NosztTábla(string hely)
        {
            string szöveg;
            string jelszó = "kloczkal";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            // tábla létrehozása
            szöveg = "CREATE TABLE Állomány (";
            szöveg += "[azonosító]  char (10),";
            szöveg += "[gyártó]  char (30),";
            szöveg += "[év]  short,";
            szöveg += "[Ntípus]  char (30),";
            szöveg += "[eszközszám]  char (20),";
            szöveg += "[leltári_szám]  char (20))";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void NosztFutás(string hely)
        {
            string szöveg;
            string jelszó = "kloczkal";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            // tábla létrehozása
            szöveg = "CREATE TABLE Futás (";
            szöveg += "[azonosító]  char (10),";
            szöveg += "[dátum]  date ,";
            szöveg += "[státusz]  yesno,";
            szöveg += "[mikor]  date,";
            szöveg += "[ki]  char (30),";
            szöveg += "[telephely]  char (30))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }


        public static void VillamostáblaNosztalgia(string hely)
        {

            string szöveg;
            string jelszó = "pozsgaii";
            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            // tábla létrehozása
            szöveg = "CREATE TABLE Állománytábla (";
            szöveg += "[azonosító]  char (10), ";
            szöveg += "[típus] char (30),";
            szöveg += "[jármű_típus] char (30),";
            szöveg += "[E2]  Short,";
            szöveg += "[E3]  Short,";
            szöveg += "[V1]  Short)";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }



    }
}
