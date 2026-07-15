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
        public static void Elfekvőtábla(string hely)
        {
            string szöveg;
            string jelszó = "bozaim";
            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Tbl_Elfekvő (";
            szöveg += "[Id] COUNTER PRIMARY KEY, ";
            szöveg += "[Anyag] char (30), ";
            szöveg += "[Anyag rövid szövege] char (254), ";
            szöveg += "[Raktárhely] char (20), ";
            szöveg += "[Szabadon használható] double, ";
            szöveg += "[Szab_felh_érték] double, ";
            szöveg += "[Sarzs] char (10), ";
            szöveg += "[Utolsó mozgás] DATE) ";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }
    }
}