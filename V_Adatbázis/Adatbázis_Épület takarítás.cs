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
        public static void Épülettakarításlétrehozás(string hely)
        {

            string szöveg;
            string jelszó = "seprűéslapát";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE Adattábla (";
            szöveg += "[id] short, ";
            szöveg += "[Megnevezés]  char (255), ";
            szöveg += "[Osztály]  char (255), ";
            szöveg += "[Méret]  Double, ";
            szöveg += "[helységkód]  char (10), ";
            szöveg += "[státus]  yesno, ";
            szöveg += "[E1évdb]  short, ";
            szöveg += "[E2évdb]  short, ";
            szöveg += "[E3évdb]  short, ";
            szöveg += "[kezd]  char (20), ";
            szöveg += "[végez]  char (20), ";
            szöveg += "[ellenőremail]  char (255), ";
            szöveg += "[ellenőrneve]  char (255), ";
            szöveg += "[ellenőrtelefonszám]  char (255),";
            szöveg += "[szemetes] yesno, ";
            szöveg += "[kapcsolthelység]  char (10)) ";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);


            szöveg = "CREATE TABLE takarításosztály (";
            szöveg += "[id] short, ";
            szöveg += "[Osztály]  char (255), ";
            szöveg += "[E1Ft]  Double, ";
            szöveg += "[E2Ft]  Double, ";
            szöveg += "[E3Ft]  Double, ";
            szöveg += "[státus]  yesno) ";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Épülettakarítótábla(string hely)
        {

            string szöveg;
            string jelszó = "seprűéslapát";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE Naptár (";
            szöveg += "[előterv]  yesno, ";
            szöveg += "[hónap] short, ";
            szöveg += "[igazolás]  yesno, ";
            szöveg += "[napok]  char (50))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE takarításrakijelölt (";
            szöveg += "[E1elvégzettdb] short, ";
            szöveg += "[E1kijelöltdb] short, ";
            szöveg += "[E1rekijelölt]  char (31), ";
            szöveg += "[E2elvégzettdb] short, ";
            szöveg += "[E2kijelöltdb] short, ";
            szöveg += "[E2rekijelölt]  char (31), ";
            szöveg += "[E3elvégzettdb] short, ";
            szöveg += "[E3kijelöltdb] short, ";
            szöveg += "[E3rekijelölt]  char (31), ";
            szöveg += "[helységkód]  char (10), ";
            szöveg += "[hónap] short, ";
            szöveg += "[Megnevezés]  char (255), ";
            szöveg += "[osztály]  char (255)) ";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }
    }
}
