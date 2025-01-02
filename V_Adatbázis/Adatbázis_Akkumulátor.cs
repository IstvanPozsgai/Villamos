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
        public static void Akku_adatok(string hely)
        {
            string szöveg;
            string jelszó = "kasosmiklós";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE Akkutábla (";
            szöveg += "[beépítve] CHAR(15),";
            szöveg += "[fajta] CHAR(10),";
            szöveg += "[gyártó] CHAR(30),";
            szöveg += "[Gyáriszám] CHAR(30),";
            szöveg += "[típus] CHAR(30),";
            szöveg += "[garancia] DATE,";
            szöveg += "[gyártásiidő] DATE,";
            szöveg += "[státus] short,";
            szöveg += "[Megjegyzés] CHAR(250),";
            szöveg += "[Módosításdátuma] DATE,";
            szöveg += "[kapacitás] short,";
            szöveg += "[Telephely] CHAR(30)";
            szöveg += ")";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }


        public static void Akku_Mérés(string hely)
        {
            string szöveg;
            string jelszó = "kasosmiklós";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg =  "CREATE TABLE méréstábla (";
            szöveg += "[Gyáriszám] CHAR(30),";
            szöveg += "[kisütésiáram] long,";
            szöveg += "[kezdetifesz] double,";
            szöveg += "[végfesz] double,";
            szöveg += "[kisütésiidő] DATE,";
            szöveg += "[kapacitás] double,";
            szöveg += "[Megjegyzés] CHAR(250),";
            szöveg += "[van] CHAR(1),";
            szöveg += "[Mérésdátuma] DATE,";
            szöveg += "[Rögzítés] DATE,";
            szöveg += "[Rögzítő] CHAR(30),";
            szöveg += "[id] long";
            szöveg += ")";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Akkutábla_Napló (";
            szöveg += "[beépítve] CHAR(15),";
            szöveg += "[fajta] CHAR(10),";
            szöveg += "[gyártó] CHAR(30),";
            szöveg += "[Gyáriszám] CHAR(30),";
            szöveg += "[típus] CHAR(30),";
            szöveg += "[garancia] DATE,";
            szöveg += "[gyártásiidő] DATE,";
            szöveg += "[státus] short,";
            szöveg += "[Megjegyzés] CHAR(250),";
            szöveg += "[Módosításdátuma] DATE,";
            szöveg += "[kapacitás] short,";
            szöveg += "[Telephely] CHAR(30),";
            szöveg += "[Rögzítés] DATE,";
            szöveg += "[Rögzítő] CHAR(30)";
            szöveg += ")";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);


        }
    }
}
