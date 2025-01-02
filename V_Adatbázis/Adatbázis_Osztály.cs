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

        public static void Osztálytábla(string hely)
        {
            string szöveg;
            string jelszó = "kéménybe";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE osztályadatok (";
            szöveg += "[azonosító]  char (4), ";
            szöveg += "[típus]  char (50), ";
            szöveg += "[altípus]  char (50), ";
            szöveg += "[telephely]  char (50), ";
            szöveg += "[szolgálat]  char (50), ";
            szöveg += "[Adat1]  char (50), ";
            szöveg += "[Adat2]  char (50), ";
            szöveg += "[Adat3]  char (50), ";
            szöveg += "[Adat4]  char (50), ";
            szöveg += "[Adat5]  char (50), ";
            szöveg += "[Adat6]  char (50), ";
            szöveg += "[Adat7]  char (50), ";
            szöveg += "[Adat8]  char (50), ";
            szöveg += "[Adat9]  char (50), ";
            szöveg += "[Adat10]  char (50), ";
            szöveg += "[Adat11]  char (50), ";
            szöveg += "[Adat12]  char (50), ";
            szöveg += "[Adat13]  char (50), ";
            szöveg += "[Adat14]  char (50), ";
            szöveg += "[Adat15]  char (50), ";
            szöveg += "[Adat16]  char (50), ";
            szöveg += "[Adat17]  char (50), ";
            szöveg += "[Adat18]  char (50), ";
            szöveg += "[Adat19]  char (50), ";
            szöveg += "[Adat20]  char (50), ";
            szöveg += "[Adat21]  char (50), ";
            szöveg += "[Adat22]  char (50), ";
            szöveg += "[Adat23]  char (50), ";
            szöveg += "[Adat24]  char (50), ";
            szöveg += "[Adat25]  char (50), ";
            szöveg += "[Adat26]  char (50), ";
            szöveg += "[Adat27]  char (50), ";
            szöveg += "[Adat28]  char (50), ";
            szöveg += "[Adat29]  char (50), ";
            szöveg += "[Adat30]  char (50), ";
            szöveg += "[Adat31]  char (50), ";
            szöveg += "[Adat32]  char (50), ";
            szöveg += "[Adat33]  char (50), ";
            szöveg += "[Adat34]  char (50), ";
            szöveg += "[Adat35]  char (50), ";
            szöveg += "[Adat36]  char (50), ";
            szöveg += "[Adat37]  char (50), ";
            szöveg += "[Adat38]  char (50), ";
            szöveg += "[Adat39]  char (50), ";
            szöveg += "[Adat40]  char (50)) ";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Osztálytábla (";
            szöveg += "[id] int,";
            szöveg += "[Osztálynév]  char (50), ";
            szöveg += "[Osztálymező]  char (50), ";
            szöveg += "[Használatban]  char (1)) ";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }
    }
}
