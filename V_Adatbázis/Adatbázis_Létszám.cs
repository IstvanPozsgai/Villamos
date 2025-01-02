using Villamos.Adatszerkezet;

namespace Villamos.Villamos_Adatbázis_Funkció
{
    public static partial class Adatbázis_Létrehozás
    {

        public static void Létszám_Elrendezés_Változatok(string hely)
        {
            string jelszó = "repülő";


            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            string szöveg = "CREATE TABLE Alaplista (";
            szöveg += "[id] short,";
            szöveg += "[változatnév] CHAR(50), ";
            szöveg += "[Csoportnév] CHAR(50), ";
            szöveg += "[oszlop] CHAR(2), ";
            szöveg += "[sor] short, ";
            szöveg += "[szélesség] short )";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }
    }
}
