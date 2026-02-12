using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{

    public class Adat_JogosítványVonal
    {
        public int Sorszám { get; private set; }
        public string Törzsszám { get; private set; }
        public DateTime Jogvonalérv { get; private set; }
        public DateTime Jogvonalmegszerzés { get; private set; }
        public string Vonalmegnevezés { get; private set; }
        public string Vonalszám { get; private set; }
        public bool Státus { get; private set; }

        public Adat_JogosítványVonal(int sorszám, string törzsszám, DateTime jogvonalérv, DateTime jogvonalmegszerzés, string vonalmegnevezés, string vonalszám, bool státus)
        {
            Sorszám = sorszám;
            Törzsszám = törzsszám;
            Jogvonalérv = jogvonalérv;
            Jogvonalmegszerzés = jogvonalmegszerzés;
            Vonalmegnevezés = vonalmegnevezés;
            Vonalszám = vonalszám;
            Státus = státus;
        }
    }
}
