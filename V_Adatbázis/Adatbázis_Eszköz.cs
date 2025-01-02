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
        public static void Eszköztábla(string hely)
        {
            string szöveg;
            string jelszó = "TóthKatalin";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Adatok (";
            szöveg += " Eszköz char (20),";
            szöveg += " Alszám char (10),";
            szöveg += " Megnevezés char (100),";
            szöveg += " Megnevezés_folyt char (100),";
            szöveg += " Gyártási_szám char (30),";
            szöveg += " Leltárszám char (100),";
            szöveg += " Leltár_dátuma DATE,";
            szöveg += " Mennyiség Double,";
            szöveg += " Bázis_menny_egység char (10),";
            szöveg += " Aktiválás_dátuma DATE,";
            szöveg += " Telephely char (10),";
            szöveg += " Telephely_megnevezése char (50),";
            szöveg += " Helyiség char (50),";
            szöveg += " Helyiség_megnevezés char (50),";
            szöveg += " Gyár char (10),";
            szöveg += " Leltári_költséghely char (10),";
            szöveg += " Vonalkód char (50),";
            szöveg += " Leltár_forduló_nap DATE,";
            szöveg += " Szemügyi_törzsszám char (10),";
            szöveg += " Dolgozó_neve char (50),";
            szöveg += " Deaktiválás_dátuma DATE,";
            szöveg += " Eszközosztály char (10),";
            szöveg += " Üzletág char (10),";
            szöveg += " Cím char (10),";
            szöveg += " Költséghely char (10),";
            szöveg += " Felelős_költséghely char (10),";
            szöveg += " Régi_leltárszám char (20),";
            szöveg += " Vonalkódozható yesno,";
            szöveg += " Rendszám_pályaszám char (10),";
            szöveg += " Épület_Szerszám char (20),";
            szöveg += " Épület_van yesno,";
            szöveg += " Szerszám_van yesno,";
            szöveg += " státus yesno)";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }
    }
}
