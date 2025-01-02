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

        public static void Villamostábla5reklám(string hely)
        {
            string szöveg;
            string jelszó = "morecs";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            // tábla létrehozása
            szöveg = "CREATE TABLE Reklámtábla (";
            szöveg += "[azonosító]  char (10),";
            szöveg += "[kezdődátum] DATE,";
            szöveg += "[befejeződátum] DATE,";
            szöveg += "[Reklámneve]  char (50),";
            szöveg += "[Viszonylat]  char (15),";
            szöveg += "[Telephely]  char (50),";
            szöveg += "[reklámmérete]  char (50),";
            szöveg += "[szerelvényben] SHORT,";
            szöveg += "[szerelvény]  char (50),";
            szöveg += "[ragasztásitilalom] DATE,";
            szöveg += "[megjegyzés]  char (250),";
            szöveg += "[típus]  char (50))";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        public static void Villamostábla5reklámnapló(string hely)
        {
            string szöveg;
            string jelszó = "morecs";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE Reklámtábla (";
            szöveg += "[azonosító]  char (10),";
            szöveg += "[kezdődátum] DATE,";
            szöveg += "[befejeződátum] DATE,";
            szöveg += "[Reklámneve]  char (50),";
            szöveg += "[Viszonylat]  char (15),";
            szöveg += "[Telephely]  char (50),";
            szöveg += "[reklámmérete]  char (50),";
            szöveg += "[szerelvényben] SHORT,";
            szöveg += "[szerelvény]  char (50),";
            szöveg += "[ragasztásitilalom] DATE,";
            szöveg += "[megjegyzés]  char (250),";
            szöveg += "[típus]  char (50),";

            szöveg += "[id] long,";
            szöveg += "[Mikor] DATE,";
            szöveg += "[Módosító]  char (20))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }
    }
}
