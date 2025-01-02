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

        public static void Fortekiadásifőmtábla(string hely)
        {

            string jelszó = "gémkapocs";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            // tábla létrehozása
            string szöveg = "CREATE TABLE fortekiadástábla (";
            szöveg += "[Dátum] DATE,";
            szöveg += "[napszak]  char (2),";
            szöveg += "[telephelyforte]  char (50),";
            szöveg += "[típusforte]  char (50),";
            szöveg += "[telephely]  char (50),";
            szöveg += "[típus]  char (50),";
            szöveg += "[kiadás]  short,";
            szöveg += "[munkanap]  short)";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        public static void Kiadásiösszesítőfőmérnöktábla(string hely)
        {

            string jelszó = "pozsi";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            string szöveg = "CREATE TABLE kiadástábla (";
            szöveg += "[Dátum] DATE,";
            szöveg += "[napszak]  char (2),";
            szöveg += "[forgalomban]  short,";
            szöveg += "[tartalék]  short,";
            szöveg += "[kocsiszíni]  short,";
            szöveg += "[félreállítás]  short,";
            szöveg += "[főjavítás]  short,";
            szöveg += "[személyzet]  short,";
            szöveg += "[kiadás]  short,";
            szöveg += "[főkategória]  char (50),";
            szöveg += "[típus]  char (50),";
            szöveg += "[altípus]  char (50),";
            szöveg += "[telephely]  char (50),";
            szöveg += "[szolgálat]  char (50),";
            szöveg += "[telephelyitípus]  char (50),";
            szöveg += "[munkanap]  short)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Kiadásiszemélyzetfőmérnöktábla(string hely)
        {
            string jelszó = "pozsi";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            string szöveg = "CREATE TABLE személyzettábla (";
            szöveg += "[Dátum] DATE,";
            szöveg += "[napszak]  char (2),";
            szöveg += "[telephely]  char (50),";
            szöveg += "[szolgálat]  char (50),";
            szöveg += "[típus]  char (50),";
            szöveg += "[viszonylat]  char (6),";
            szöveg += "[forgalmiszám]  char (6),";
            szöveg += "[tervindulás] DATE,";
            szöveg += "[azonosító]  char (10))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Kiadásitípuscserefőmérnöktábla(string hely)
        {
            string jelszó = "pozsi";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            string szöveg = "CREATE TABLE típuscseretábla (";
            szöveg += "[Dátum] DATE,";
            szöveg += "[napszak]  char (2),";
            szöveg += "[telephely]  char (50),";
            szöveg += "[szolgálat]  char (50),";
            szöveg += "[típuselőírt]  char (20),";
            szöveg += "[típuskiadott]  char (20),";
            szöveg += "[viszonylat]  char (6),";
            szöveg += "[forgalmiszám]  char (6),";
            szöveg += "[tervindulás] DATE,";
            szöveg += "[azonosító]  char (10))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }
    }
}
