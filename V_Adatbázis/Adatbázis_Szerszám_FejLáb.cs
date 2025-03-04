using Villamos.Adatszerkezet;

namespace Villamos.Villamos_Adatbázis_Funkció
{
    public static partial class Adatbázis_Létrehozás
    {
        public static void Szerszám_FejLáb(string hely)
        {
            string szöveg;
            string jelszó = "Mocó";
            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = $"CREATE TABLE Szerszám_FejLáb (";
            szöveg += " [Típus] char(25), ";
            szöveg += " [Fejléc_Bal] char(250), ";
            szöveg += " [Fejléc_Közép] char(250), ";
            szöveg += " [Fejléc_Jobb] char(250), ";
            szöveg += " [Lábléc_Bal] char(250), ";
            szöveg += " [Lábléc_Közép] char(250), ";
            szöveg += " [Lábléc_Jobb] char(250))";

            //Létrehozzuk az adattáblát
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }
    }
}
