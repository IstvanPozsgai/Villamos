using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Villamos.Adatszerkezet;

namespace Villamos.Villamos_Adatbázis_Funkció
{
    public static partial class Adatbázis_Létrehozás
    {

        public static void Kiadásiösszesítőtábla(string hely)
        {
            string szöveg;
            string jelszó = "plédke";
            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            // tábla létrehozása
            szöveg = "CREATE TABLE tábla (";
            szöveg += "[dátum] DATE,";
            szöveg += "[napszak]  char (2),";
            szöveg += "[típus]  char (20),";
            szöveg += "[forgalomban] SHORT,";
            szöveg += "[tartalék] SHORT,";
            szöveg += "[kocsiszíni] Short,";
            szöveg += "[félreállítás] Short,";
            szöveg += "[főjavítás] SHORT,";
            szöveg += "[személyzet] SHORT )";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Személyzetösszesítőtábla(string hely)
        {
            string szöveg;
            string jelszó = "plédke";
            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            // tábla létrehozása
            szöveg = "CREATE TABLE tábla (";
            szöveg += "[dátum] DATE,";
            szöveg += "[napszak]  char (2),";
            szöveg += "[típus]  char (20),";
            szöveg += "[viszonylat]  char (6),";
            szöveg += "[forgalmiszám]  char (6),";
            szöveg += "[tervindulás] DATE,";
            szöveg += "[azonosító]  char (10) )";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }


        public static void Tipuscsereösszesítőtábla(string hely)
        {
            string szöveg;
            string jelszó = "plédke";
            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            // tábla létrehozása
            szöveg = "CREATE TABLE típuscseretábla (";
            szöveg += "[dátum] DATE,";
            szöveg += "[napszak]  char (2),";
            szöveg += "[típuselőírt]  char (20),";
            szöveg += "[típuskiadott]  char (20),";
            szöveg += "[viszonylat]  char (6),";
            szöveg += "[forgalmiszám]  char (6),";
            szöveg += "[tervindulás] DATE,";
            szöveg += "[azonosító]  char (10) ,";
            szöveg += "[kocsi]  char (10))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Javításiátfutástábla(string hely)
        {
            string szöveg;
            string jelszó = "plédke";
            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            // tábla létrehozása
            szöveg = "CREATE TABLE xnapostábla (";
            szöveg += "[kezdődátum] DATE,";
            szöveg += "[végdátum] DATE,";
            szöveg += "[azonosító] CHAR(10), ";
            szöveg += "[hibaleírása] Memo)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Napihibatábla(string hely)
        {
            string szöveg;
            string jelszó = "pozsgaii";
            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            // tábla létrehozása
            szöveg = "CREATE TABLE Hiba (";
            szöveg += "[azonosító]  char(10), ";
            szöveg += "[mikori] DATE, ";
            szöveg += "[beálló] LONGTEXT, ";
            szöveg += "[üzemképtelen] LONGTEXT, ";
            szöveg += "[üzemképeshiba] LONGTEXT, ";
            szöveg += "[típus]  char(20), ";
            szöveg += "[státus] Long)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        public static void Zseltáblaalap(string hely)
        {
            string szöveg;
            string jelszó = "lilaakác";
            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Zseltábla (";
            szöveg += "[viszonylat] CHAR(10),";
            szöveg += "[forgalmiszám] CHAR(10),";
            szöveg += "[tervindulás] DATE,";
            szöveg += "[tényindulás] DATE,";
            szöveg += "[tervérkezés] DATE,";
            szöveg += "[tényérkezés] DATE,";
            szöveg += "[napszak] CHAR(3),";
            szöveg += "[szerelvénytípus] CHAR(10),";
            szöveg += "[kocsikszáma] Long,";
            szöveg += "[megjegyzés] CHAR(20),";
            szöveg += "[kocsi1] CHAR(10),";
            szöveg += "[kocsi2] CHAR(10),";
            szöveg += "[kocsi3] CHAR(10),";
            szöveg += "[kocsi4] CHAR(10),";
            szöveg += "[kocsi5] CHAR(10),";
            szöveg += "[kocsi6] CHAR(10),";
            szöveg += "[ellenőrző] CHAR(20),";
            szöveg += "[Státus] CHAR(10))";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void ZSER_km(string hely)
        {
            string szöveg;
            string jelszó = "pozsgaii";
            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Tábla (";
            szöveg += "[azonosító] CHAR(10),";
            szöveg += "[Dátum] DATE,";
            szöveg += "[Napikm] SHORT, ";
            szöveg += "[telephely] CHAR(20) )";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        public static void Főkönyvtáblaalap(string hely)
        {
            string szöveg;
            string jelszó = "lilaakác";
            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            // tábla létrehozása
            szöveg = "CREATE TABLE Adattábla (";
            szöveg += "[Státus] Long, ";
            szöveg += "[hibaleírása] LONGTEXT, ";
            szöveg += "[típus]  char (20), ";
            szöveg += "[azonosító]  char (10), ";
            szöveg += "[szerelvény] Long, ";
            szöveg += "[viszonylat]  char (6), ";
            szöveg += "[forgalmiszám]  char (6), ";
            szöveg += "[kocsikszáma] Long, ";
            szöveg += "[tervindulás] DATE, ";
            szöveg += "[tényindulás] DATE, ";
            szöveg += "[tervérkezés] DATE, ";
            szöveg += "[tényérkezés] DATE, ";
            szöveg += "[miótaáll] DATE, ";
            szöveg += "[napszak]  char (3), ";
            szöveg += "[megjegyzés]  char(20) ) ";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE segédtábla (";
            szöveg += "[id] Long, ";
            szöveg += "[Bejelentkezésinév]  char (15) ) ";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }
    }
}
