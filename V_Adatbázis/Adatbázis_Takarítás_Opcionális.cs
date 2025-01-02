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
        public static void ÉpülettakarításOpcionálislétrehozás(string hely)
        {

            string szöveg;
            string jelszó = "seprűéslapát";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE TakarításOpcionális (";
            szöveg += "[Id] short, ";
            szöveg += "[Megnevezés]  char (255), ";
            szöveg += "[Mennyisége]  char (10), ";
            szöveg += "[Ár]  Double, ";
            szöveg += "[Kezdet] DATE,";
            szöveg += "[Vég] DATE)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        public static void ÉpülettakarításTelepOpcionálisLétrehozás(string hely)
        {

            string szöveg;
            string jelszó = "seprűéslapát";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE TakarításOpcTelepAdatok (";
            szöveg += "[Id] short, ";
            szöveg += "[Dátum] DATE,";
            szöveg += "[Megrendelt] double, ";
            szöveg += "[Teljesített] double)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }
    }
}
