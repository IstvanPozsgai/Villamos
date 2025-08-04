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
        public static void Kidobósegédadattábla(string hely)
        {
            string szöveg;
            string jelszó = "erzsébet";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE Kidobósegédtábla (";
            szöveg += "[forgalmiszám]  char (6), ";
            szöveg += "[szolgálatiszám]  char (20), ";
            szöveg += "[kezdés] DATE,";
            szöveg += "[végzés] DATE,";
            szöveg += "[Kezdéshely]  char (50), ";
            szöveg += "[Végzéshely]  char (50), ";
            szöveg += "[Változatnév]  char (50), ";
            szöveg += "[megjegyzés]  char (50)) ";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Változattábla (";
            szöveg += "[id] long,";
            szöveg += "[Változatnév]  char (50)) ";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Kidobóadattábla(string hely)
        {
            string szöveg;
            string jelszó = "lilaakác";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE Kidobótábla (";
            szöveg += "[viszonylat]  char (6), ";
            szöveg += "[forgalmiszám]  char (6), ";
            szöveg += "[szolgálatiszám]  char (20), ";
            szöveg += "[jvez]  char (100), ";
            szöveg += "[kezdés] DATE,";
            szöveg += "[végzés] DATE,";
            szöveg += "[Kezdéshely]  char (50), ";
            szöveg += "[Végzéshely]  char (50), ";
            szöveg += "[Kód]  char (3), ";
            szöveg += "[Tárolásihely]  char (30), ";
            szöveg += "[Villamos]  char (30), ";
            szöveg += "[megjegyzés]  char (50), ";
            szöveg += "[szerelvénytípus]  char (30)) ";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }
    }
}
