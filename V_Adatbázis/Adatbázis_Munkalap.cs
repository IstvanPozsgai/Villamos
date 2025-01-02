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
        public static void Munkalapösszesítő_tábla(string hely)
        {
            string szöveg;
            string jelszó = "felépítés";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE időválaszték (";
            szöveg += "[ID] számláló,";
            szöveg += "[idő] long)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE rendeléstábla (";
            szöveg += "[ID] számláló,";
            szöveg += "[megnevezés] CHAR(20),";
            szöveg += "[művelet] CHAR(20),";
            szöveg += "[pályaszám] CHAR(20),";
            szöveg += "[rendelés] CHAR(20))";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Munkalapelszámoló_tábla(string hely)
        {
            string szöveg;
            string jelszó = "dekádoló";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Adatoktábla (";
            szöveg += "[ID] számláló,";
            szöveg += "[idő] long,";
            szöveg += "[dátum] DATE,";
            szöveg += "[megnevezés] CHAR(20),";
            szöveg += "[művelet] CHAR(20),";
            szöveg += "[pályaszám] CHAR(20),";
            szöveg += "[rendelés] CHAR(20),";
            szöveg += "[státus] YESNO)";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Munkalap_tábla(string hely)
        {
            string szöveg;
            string jelszó = "kismalac";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE folyamattábla (";
            szöveg += "[ID] long,";
            szöveg += "[rendelésiszám] CHAR(20),";
            szöveg += "[azonosító] CHAR(6),";
            szöveg += "[munkafolyamat] CHAR(150),";
            szöveg += "[látszódik] YESNO)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);


            szöveg = "CREATE TABLE munkarendtábla (";
            szöveg += "[ID] long,";
            szöveg += "[munkarend] CHAR(20),";
            szöveg += "[látszódik] YESNO)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);


            szöveg = "CREATE TABLE szolgálattábla (";
            szöveg += "[Költséghely] CHAR(30),";
            szöveg += "[Szolgálat] CHAR(30),";
            szöveg += "[Üzem] CHAR(30),";
            szöveg += "[A1] CHAR(5),";
            szöveg += "[A2] CHAR(5),";
            szöveg += "[A3] CHAR(5),";
            szöveg += "[A4] CHAR(5),";
            szöveg += "[A5] CHAR(5),";
            szöveg += "[A6] CHAR(5),";
            szöveg += "[A7] CHAR(5))";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Munkalapkedvencek(string hely)
        {
            string szöveg;
            string jelszó = "felépítés";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE időválaszték (";
            szöveg += "[id] counter,";
            szöveg += "[idő] int";
            szöveg += ")";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);


            szöveg = "CREATE TABLE rendeléstábla (";
            szöveg += "[id] counter,";
            szöveg += "[megnevezés] CHAR(20),";
            szöveg += "[művelet] CHAR(20),";
            szöveg += "[pályaszám] CHAR(20),";
            szöveg += "[rendelés] CHAR(20)";
            szöveg += ")";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }


        public static void Munkalapévestábla(string hely)
        {
            string szöveg;
            string jelszó = "dekádoló";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Adatoktábla (";
            szöveg += "[id] counter, ";
            szöveg += "[idő] int, ";
            szöveg += "[dátum] datetime, ";
            szöveg += "[megnevezés] CHAR(20), ";
            szöveg += "[művelet] CHAR(20), ";
            szöveg += "[pályaszám] CHAR(20), ";
            szöveg += "[rendelés] CHAR(20), ";
            szöveg += "[státus] yesno";
            szöveg += ")";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

    }
}
