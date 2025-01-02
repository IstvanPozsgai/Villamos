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

        public static void Takarításnapló(string hely)
        {
            string szöveg;
            string jelszó = "pozsgaii";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Napló (";
            szöveg += "[Módosító]  char (20),";
            szöveg += "[Mikor] DATE,";
            szöveg += "[azonosító]  char (10),";
            szöveg += "[takarításúj] DATE,";
            szöveg += "[takarításold] DATE)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Járműtakarítótábla(string hely)
        {
            string szöveg;
            string jelszó = "seprűéslapát";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE J1adatok (";
            szöveg += "[dátum] DATE,";
            szöveg += "[j1megfelelő]  short,";
            szöveg += "[j1nemmegfelelő]  short,";
            szöveg += "[napszak]  short,";
            szöveg += "[típus]  char (20))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE J2adatok (";
            szöveg += "[azonosító]  char (10),";
            szöveg += "[dátum] DATE,";
            szöveg += "[napszak]  short,";
            szöveg += "[státus]  long,";
            szöveg += "[típus]  char (20))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE létszám (";
            szöveg += "[dátum] DATE,";
            szöveg += "[előírt]  long,";
            szöveg += "[megjelent]  long,";
            szöveg += "[napszak]  short,";
            szöveg += "[ruhátlan]  long)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE opcionális (";
            szöveg += "[dátum] DATE,";
            szöveg += "[fertőtlenítés]  double,";
            szöveg += "[graffiti]  double,";
            szöveg += "[státus]  long,";
            szöveg += "[típus]  char (20))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        public static void Járműtakarító_Főmérnök_tábla(string hely)
        {
            string szöveg;
            string jelszó = "seprűéslapát";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Ütemező (";
            szöveg += "[azonosító]  char (10),";
            szöveg += "[Kezdő_dátum] DATE,";
            szöveg += "[növekmény]  short,";
            szöveg += "[Mérték]  char(20),";
            szöveg += "[Takarítási_fajta]  char(20),";
            szöveg += "[Telephely]  char (50),";
            szöveg += "[státus]  short)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Takarítások (";
            szöveg += "[azonosító]  char (10),";
            szöveg += "[dátum] DATE,";
            szöveg += "[Takarítási_fajta]  char(20),";
            szöveg += "[Telephely]  char (50),";
            szöveg += "[státus]  short)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Kötbér (";
            szöveg += "[Takarítási_fajta]  char(20),";
            szöveg += "[NemMegfelel]  double,";
            szöveg += "[Póthatáridő]  double)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);


            szöveg = "CREATE TABLE Árak (";
            szöveg += "[id] DOUBLE,";
            szöveg += "[JárműTípus]  char(50),";
            szöveg += "[Takarítási_fajta]  char(20),";
            szöveg += "[napszak]  short,";
            szöveg += "[ár]  double,";
            szöveg += "[Érv_kezdet] DATE,";
            szöveg += "[Érv_vég] DATE)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Mátrix (";
            szöveg += "[id] DOUBLE,";
            szöveg += "[fajta]  char(5),";
            szöveg += "[fajtamásik]  char(5),";
            szöveg += "[igazság]  YESNO)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        public static void Járműtakarító_Főmérnök_Napló(string hely)
        {
            string szöveg;
            string jelszó = "seprűéslapát";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);


            szöveg = "CREATE TABLE Takarítások_napló (";
            szöveg += "[azonosító]  char (10),";
            szöveg += "[dátum] DATE,";
            szöveg += "[Takarítási_fajta]  char(20),";
            szöveg += "[Telephely]  char (50),";
            szöveg += "[Mikor] DATE,";
            szöveg += "[Módosító]  char (20),";
            szöveg += "[státus]  short)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Járműtakarító_Telephely_tábla(string hely)
        {
            string szöveg;
            string jelszó = "seprűéslapát";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Teljesítés (";
            szöveg += "[azonosító]  char (10),";
            szöveg += "[Takarítási_fajta]  char(20),";
            szöveg += "[dátum] DATE,";
            szöveg += "[megfelelt1]  short,";
            szöveg += "[státus]  short,";
            szöveg += "[megfelelt2]  short,";
            szöveg += "[pótdátum] yesno,";
            szöveg += "[napszak]  short,";
            szöveg += "[mérték]  double,";
            szöveg += "[Típus]  char (20))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE J1adatok (";
            szöveg += "[dátum] DATE,";
            szöveg += "[j1megfelelő]  short,";
            szöveg += "[j1nemmegfelelő]  short,";
            szöveg += "[napszak]  short,";
            szöveg += "[típus]  char (20))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE létszám (";
            szöveg += "[dátum] DATE,";
            szöveg += "[előírt]  long,";
            szöveg += "[megjelent]  long,";
            szöveg += "[napszak]  short,";
            szöveg += "[ruhátlan]  long)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Vezénylés (";
            szöveg += "[id] long,";
            szöveg += "[azonosító]  char (10),";
            szöveg += "[dátum] DATE,";
            szöveg += "[Takarítási_fajta]  char(20),";
            szöveg += "[szerelvényszám] Long,";
            szöveg += "[státus]  short)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }
    }
}
