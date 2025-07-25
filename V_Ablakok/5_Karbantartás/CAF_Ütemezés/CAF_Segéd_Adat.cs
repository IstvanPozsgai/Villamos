using System;

namespace Villamos.Villamos_Ablakok.CAF_Ütemezés
{
    // JAVÍTANDÓ:
    // Az osztály neve Adat_CAF_Segéd helye  V_Adatszerkezetben
    //gondold át, hogy kell-e új adatszerkezet, vagy ezzel az struktúrával túl lehet-e terhelni a Adat_CAF_Adatok-t
    public class CAF_Segéd_Adat
    {
        public string Azonosító { get; private set; }
        public DateTime Dátum { get; private set; }
        public double Sorszám { get; private set; }

        public CAF_Segéd_Adat(string azonosító, DateTime dátum, double sorszám)
        {
            Azonosító = azonosító;
            Dátum = dátum;
            Sorszám = sorszám;
        }
    }

}
