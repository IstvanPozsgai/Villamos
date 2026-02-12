using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Munka_Szolgálat
    {
        public string Költséghely { get; private set; }
        public string Szolgálat { get; private set; }
        public string Üzem { get; private set; }
        public string A1 { get; private set; }
        public string A2 { get; private set; }
        public string A3 { get; private set; }

        public string A4 { get; private set; }
        public string A5 { get; private set; }

        public string A6 { get; private set; }
        public string A7 { get; private set; }

        public Adat_Munka_Szolgálat(string költséghely, string szolgálat, string üzem, string a1, string a2, string a3, string a4, string a5, string a6, string a7)
        {
            Költséghely = költséghely;
            Szolgálat = szolgálat;
            Üzem = üzem;
            A1 = a1;
            A2 = a2;
            A3 = a3;
            A4 = a4;
            A5 = a5;
            A6 = a6;
            A7 = a7;
        }
    }
}
