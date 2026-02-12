using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_FőKönyv_Típuscsere
    {
        public DateTime Dátum { get; private set; }
        public string Napszak { get; private set; }
        public string Típuselőírt { get; private set; }
        public string Típuskiadott { get; private set; }
        public string Viszonylat { get; private set; }
        public string Forgalmiszám { get; private set; }
        public DateTime Tervindulás { get; private set; }
        public string Azonosító { get; private set; }
        public string Kocsi { get; private set; }

        public Adat_FőKönyv_Típuscsere(DateTime dátum, string napszak, string típuselőírt, string típuskiadott, string viszonylat, string forgalmiszám, DateTime tervindulás, string azonosító, string kocsi)
        {
            Dátum = dátum;
            Napszak = napszak;
            Típuselőírt = típuselőírt;
            Típuskiadott = típuskiadott;
            Viszonylat = viszonylat;
            Forgalmiszám = forgalmiszám;
            Tervindulás = tervindulás;
            Azonosító = azonosító;
            Kocsi = kocsi;
        }
    }
}
