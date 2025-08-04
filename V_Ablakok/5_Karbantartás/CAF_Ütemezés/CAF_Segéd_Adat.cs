using System;

namespace Villamos.Villamos_Ablakok.CAF_Ütemezés
{
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
