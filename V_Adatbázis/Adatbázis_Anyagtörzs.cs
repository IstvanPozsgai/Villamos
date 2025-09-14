using Villamos.Adatszerkezet;

namespace Villamos.V_Adatbázis
{
    public static partial class Adatbázis_Létrehozás
    {
        public static void AnyagTörzs(string hely)
        {
            string szöveg;
            string jelszó = "SzőkeLászló";
            string táblanév = "AnyagTábla";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = $"CREATE TABLE {táblanév} (";
            szöveg += "[Cikkszám] CHAR(20),";
            szöveg += "[Megnevezés] CHAR(255),";
            szöveg += "[KeresőFogalom] LONGTEXT,";
            szöveg += "[Sarzs] CHAR(5),";
            szöveg += "[Ár] double)";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }
        public static void Raktárkészlet(string hely)
        {
            string szöveg;
            string jelszó = "SzőkeLászló";
            string táblanév = "RaktárTábla";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = $"CREATE TABLE {táblanév} (";
            szöveg += "[Cikkszám] CHAR(20),";
            szöveg += "[Sarzs] CHAR(5),";
            szöveg += "[Raktárhely] CHAR(10),";
            szöveg += "[Mennyiség] double)";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }
    }
}
