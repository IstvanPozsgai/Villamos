using Villamos.Adatszerkezet;

namespace Villamos.Villamos_Adatbázis_Funkció
{
    public static partial class Adatbázis_Létrehozás
    {

        public static void KocsikTípusaTelep(string hely)
        {
            string szöveg;
            string jelszó = "pozsgaii";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Állománytábla (";
            szöveg += "[azonosító] CHAR(10),";
            szöveg += "[hibák] Long,";
            szöveg += "[státus] Long,";
            szöveg += "[típus] CHAR(20),";
            szöveg += "[üzem] CHAR(30),";
            szöveg += "[törölt] YESNO,";
            szöveg += "[hibáksorszáma] Long,";
            szöveg += "[szerelvény] YESNO,";
            szöveg += "[szerelvénykocsik] Long,";
            szöveg += "[miótaáll] Date,";
            szöveg += "[valóstípus] CHAR(50),";
            szöveg += "[valóstípus2] CHAR(50))";
            //Létrehozzuk az adatbázist
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Hibatáblalap(string hely)
        {
            string szöveg;
            string jelszó = "pozsgaii";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Hibatábla (";
            szöveg += "[létrehozta] CHAR(20), ";
            szöveg += "[Korlát] long, ";
            szöveg += "[hibaleírása] CHAR(85), ";
            szöveg += "[idő] DATE,";
            szöveg += "[javítva] YESNO, ";
            szöveg += "[típus] CHAR(20), ";
            szöveg += "[azonosító] CHAR(10), ";
            szöveg += "[hibáksorszáma] long) ";
            //Létrehozzuk az adattáblát
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void KocsikTípusa(string hely)
        {
            string szöveg;
            string jelszó = "pozsgaii";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Állománytábla (";
            szöveg += "[azonosító] CHAR(10),";
            szöveg += "[hibák] Long,";
            szöveg += "[státus] Long,";
            szöveg += "[típus] CHAR(20),";
            szöveg += "[üzem] CHAR(30),";
            szöveg += "[törölt] YESNO,";
            szöveg += "[hibáksorszáma] Long,";
            szöveg += "[szerelvény] YESNO,";
            szöveg += "[szerelvénykocsik] Long,";
            szöveg += "[miótaáll] Date,";
            szöveg += "[valóstípus] CHAR(50),";
            szöveg += "[valóstípus2] CHAR(50),";
            szöveg += "[üzembehelyezés] Date)";
            //Létrehozzuk az adattáblát
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Vendégtábla (";
            szöveg += "[azonosító] CHAR(10),";
            szöveg += "[típus] CHAR(20),";
            szöveg += "[BázisTelephely] CHAR(30),";
            szöveg += "[KiadóTelephely] CHAR(30))";

            //Létrehozzuk az adattáblát
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Villamostábla(string hely)
        {
            string szöveg;
            string jelszó = "pozsgaii";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Állománytábla (";
            szöveg += "[azonosító]  char (10), ";
            szöveg += "[takarítás] DATE,";
            szöveg += "[haromnapos]  Short)";
            //Létrehozzuk az adattáblát
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        public static void Villamostábla3(string hely)
        {
            string szöveg;
            string jelszó = "pozsgaii";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Állománytábla (";
            szöveg += "[azonosító] CHAR(10),";
            szöveg += "[vizsgálatdátuma] Date,";
            szöveg += "[Vizsgálatfokozata] CHAR(4),";
            szöveg += "[vizsgálatszáma] short,";
            szöveg += "[futásnap] short,";
            szöveg += "[utolsórögzítés] Date)";
            //Létrehozzuk az adattáblát
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        public static void Kocsitípusanapló(string hely)
        {
            string szöveg;
            string jelszó = "pozsgaii";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Állománytáblanapló (";
            szöveg += " [azonosító]  CHAR(10),";
            szöveg += " [típus]  CHAR(20),";
            szöveg += " [hova]  CHAR(30),";
            szöveg += " [honnan]  CHAR(30),";
            szöveg += " [törölt] YESNO,";
            szöveg += " [Módosító]  CHAR(20),";
            szöveg += " [Mikor] Date,";
            szöveg += " [Céltelep]  CHAR(30),";
            szöveg += " [üzenet] short)";
            //Létrehozzuk az adattáblát
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        public static void Összevonttáblakészítő(string hely)
        {
            string szöveg;
            string jelszó = "pozsgaii";
            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE tábla (";
            szöveg += "[azonosító]  char (10), ";
            szöveg += "[Státus] Long, ";
            szöveg += "[üzem]  char (30), ";
            szöveg += "[miótaáll] DATE, ";
            szöveg += "[valóstípus]  char (50), ";
            szöveg += "[üzembehelyezés] DATE, ";
            szöveg += "[hibaleírása]  Memo) ";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        public static void Futásnapalap(string hely)
        {
            string szöveg;
            string jelszó = "lilaakác";
            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Futástábla (";
            szöveg += "[azonosító] CHAR(8),";
            szöveg += "[Dátum] DATE,";
            szöveg += "[Futásstátus] CHAR(15),";
            szöveg += "[Státus] Long )";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Futástábla1 (";
            szöveg += "[Státus] Long )";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        public static void Szerelvénytáblalap(string hely)
        {
            string szöveg;
            string jelszó = "pozsgaii";
            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE szerelvénytábla (";
            szöveg += "[id] Long,";
            szöveg += "[szerelvényhossz] Long,";
            szöveg += "[kocsi1] CHAR(10),";
            szöveg += "[kocsi2] CHAR(10),";
            szöveg += "[kocsi3] CHAR(10),";
            szöveg += "[kocsi4] CHAR(10),";
            szöveg += "[kocsi5] CHAR(10),";
            szöveg += "[kocsi6] CHAR(10))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        public static void Szerelvénytáblalapnapló(string hely)
        {
            string szöveg;
            string jelszó = "pozsgaii";
            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE szerelvénytáblanapló (";
            szöveg += "[id] Long,";
            szöveg += "[szerelvényhossz] Long,";
            szöveg += "[kocsi1] CHAR(10),";
            szöveg += "[kocsi2] CHAR(10),";
            szöveg += "[kocsi3] CHAR(10),";
            szöveg += "[kocsi4] CHAR(10),";
            szöveg += "[kocsi5] CHAR(10),";
            szöveg += "[kocsi6] CHAR(10),";
            szöveg += "[Módosító] CHAR(20),";
            szöveg += "[Mikor] DATE)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        public static void Jármű_Állomány_Típus(string hely)
        {
            //Jármű.mdb
            string szöveg;
            string jelszó = "pozsgaii";
            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Típustábla (";
            szöveg += "[id] Long,";
            szöveg += "[állomány] Long,";
            szöveg += "[Típus] CHAR(10))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }


    }
}
