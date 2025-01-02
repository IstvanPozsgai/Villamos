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
        public static void TakarításBMRlétrehozás(string hely)
        {

            string szöveg;
            string jelszó = "seprűéslapát";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE TakarításBMR (";
            szöveg += "[Id] short, ";
            szöveg += "[Telephely]  char (25), ";
            szöveg += "[JárműÉpület]  char (10), "; 
            szöveg += "[BMRszám]  char (15), ";
            szöveg += "[Dátum] DATE)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }
    }
}
