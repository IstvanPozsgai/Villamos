using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Forte_Kiadási_Adatok
    {
        public DateTime Dátum { get; private set; }
        public string Napszak { get; private set; }
        public string Telephelyforte { get; private set; }
        public string Típusforte { get; private set; }
        public string Telephely { get; private set; }
        public string Típus { get; private set; }
        public long Kiadás { get; private set; }
        public long Munkanap { get; private set; }

        public Adat_Forte_Kiadási_Adatok(DateTime dátum, string napszak, string telephelyforte,
            string típusforte, string telephely, string típus, long kiadás, long munkanap)
        {
            Dátum = dátum;
            Napszak = napszak;
            Telephelyforte = telephelyforte;
            Típusforte = típusforte;
            Telephely = telephely;
            Típus = típus;
            Kiadás = kiadás;
            Munkanap = munkanap;
        }
    }
}
