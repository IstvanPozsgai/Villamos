using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Típuscsere_Adatok
    {
        public DateTime Dátum { get; private set; }
        public string Napszak { get; private set; }
        public string Szolgálat { get; private set; }
        public string Telephely { get; private set; }
        public string Típuselőírt { get; private set; }
        public string Típuskiadott { get; private set; }
        public string Viszonylat { get; private set; }
        public string Forgalmiszám { get; private set; }
        public DateTime Tervindulás { get; private set; }
        public string Azonosító { get; private set; }

        public Adat_Típuscsere_Adatok(DateTime dátum, string napszak, string szolgálat, string telephely, string típuselőírt,
        string típuskiadott, string viszonylat, string forgalmiszám, DateTime tervindulás, string azonosító)
        {
            Dátum = dátum;
            Napszak = napszak;
            Szolgálat = szolgálat;
            Telephely = telephely;
            Típuselőírt = típuselőírt;
            Típuskiadott = típuskiadott;
            Viszonylat = viszonylat;
            Forgalmiszám = forgalmiszám;
            Tervindulás = tervindulás;
            Azonosító = azonosító;
        }

        public Adat_Típuscsere_Adatok(DateTime dátum, string napszak, string telephely)
        {
            Dátum = dátum;
            Napszak = napszak;
            Telephely = telephely;
        }
    }
}
