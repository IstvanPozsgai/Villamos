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
        public static void Menekimaradás_telephely(string hely)
        {
            string szöveg;
            string jelszó = "lilaakác";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE Menettábla (";
            szöveg += "[viszonylat] CHAR(6),";
            szöveg += "[azonosító] CHAR(10),";
            szöveg += "[típus] CHAR(20),";
            szöveg += "[Eseményjele] CHAR(1),";
            szöveg += "[Bekövetkezés] date,";
            szöveg += "[kimaradtmenet] long,";
            szöveg += "[jvbeírás] CHAR(150),";
            szöveg += "[vmbeírás] CHAR(150),";
            szöveg += "[javítás] CHAR(150),";
            szöveg += "[id] long,";
            szöveg += "[törölt] YESNO,";
            szöveg += "[jelentés]  CHAR(20),";
            szöveg += "[tétel] long)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Menekimaradás_Főmérnökség(string hely)
        {
            string szöveg;
            string jelszó = "lilaakác";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE Menettábla (";
            szöveg += "[viszonylat] CHAR(6),";
            szöveg += "[azonosító] CHAR(10),";
            szöveg += "[típus] CHAR(20),";
            szöveg += "[Eseményjele] CHAR(1),";
            szöveg += "[Bekövetkezés] date,";
            szöveg += "[kimaradtmenet] long,";
            szöveg += "[jvbeírás] CHAR(150),";
            szöveg += "[vmbeírás] CHAR(150),";
            szöveg += "[javítás] CHAR(150),";
            szöveg += "[id] long,";
            szöveg += "[törölt] YESNO,";
            szöveg += "[jelentés]  CHAR(20),";
            szöveg += "[tétel] long,";
            szöveg += "[telephely]  CHAR(50),";
            szöveg += "[szolgálat]  CHAR(50))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }
    }
}
