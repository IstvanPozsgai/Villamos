using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_TTP_Alapadat
    {
        public string Azonosító { get; private set; }
        public DateTime Gyártási_Év { get; private set; }
        public bool TTP { get; private set; }
        public string Megjegyzés { get; set; }

        public Adat_TTP_Alapadat(string azonosító, DateTime gyártási_Év, bool tTP, string megjegyzés)
        {
            Azonosító = azonosító;
            Gyártási_Év = gyártási_Év;
            TTP = tTP;
            Megjegyzés = megjegyzés;
        }
    }
}
