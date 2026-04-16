using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kiegészítő_Beosztáskódok
    {
        public long Sorszám { get; private set; }
        public string Beosztáskód { get; private set; }
        public DateTime Munkaidőkezdet { get; private set; }
        public DateTime Munkaidővége { get; private set; }
        public int Munkaidő { get; private set; }
        public int Munkarend { get; private set; }
        public string Napszak { get; private set; }
        public bool Éjszakás { get; private set; }
        public bool Számoló { get; private set; }
        public int Óra0 { get; private set; }
        public int Óra1 { get; private set; }
        public int Óra2 { get; private set; }
        public int Óra3 { get; private set; }
        public int Óra4 { get; private set; }
        public int Óra5 { get; private set; }
        public int Óra6 { get; private set; }
        public int Óra7 { get; private set; }
        public int Óra8 { get; private set; }
        public int Óra9 { get; private set; }
        public int Óra10 { get; private set; }
        public int Óra11 { get; private set; }
        public int Óra12 { get; private set; }
        public int Óra13 { get; private set; }
        public int Óra14 { get; private set; }
        public int Óra15 { get; private set; }
        public int Óra16 { get; private set; }
        public int Óra17 { get; private set; }
        public int Óra18 { get; private set; }
        public int Óra19 { get; private set; }
        public int Óra20 { get; private set; }
        public int Óra21 { get; private set; }
        public int Óra22 { get; private set; }
        public int Óra23 { get; private set; }
        public string Magyarázat { get; private set; }

        public Adat_Kiegészítő_Beosztáskódok(long sorszám, string beosztáskód, DateTime munkaidőkezdet, DateTime munkaidővége, int munkaidő, int munkarend, string napszak, bool éjszakás, bool számoló, int óra0, int óra1, int óra2, int óra3, int óra4, int óra5, int óra6, int óra7, int óra8, int óra9, int óra10, int óra11, int óra12, int óra13, int óra14, int óra15, int óra16, int óra17, int óra18, int óra19, int óra20, int óra21, int óra22, int óra23, string magyarázat)
        {
            Sorszám = sorszám;
            Beosztáskód = beosztáskód;
            Munkaidőkezdet = munkaidőkezdet;
            Munkaidővége = munkaidővége;
            Munkaidő = munkaidő;
            Munkarend = munkarend;
            Napszak = napszak;
            Éjszakás = éjszakás;
            Számoló = számoló;
            Óra0 = óra0;
            Óra1 = óra1;
            Óra2 = óra2;
            Óra3 = óra3;
            Óra4 = óra4;
            Óra5 = óra5;
            Óra6 = óra6;
            Óra7 = óra7;
            Óra8 = óra8;
            Óra9 = óra9;
            Óra10 = óra10;
            Óra11 = óra11;
            Óra12 = óra12;
            Óra13 = óra13;
            Óra14 = óra14;
            Óra15 = óra15;
            Óra16 = óra16;
            Óra17 = óra17;
            Óra18 = óra18;
            Óra19 = óra19;
            Óra20 = óra20;
            Óra21 = óra21;
            Óra22 = óra22;
            Óra23 = óra23;
            Magyarázat = magyarázat;
        }

        public Adat_Kiegészítő_Beosztáskódok(long sorszám, string beosztáskód, DateTime munkaidőkezdet, DateTime munkaidővége, int munkaidő, int munkarend, bool éjszakás, bool számoló, string magyarázat)
        {
            Sorszám = sorszám;
            Beosztáskód = beosztáskód;
            Munkaidőkezdet = munkaidőkezdet;
            Munkaidővége = munkaidővége;
            Munkaidő = munkaidő;
            Munkarend = munkarend;
            Éjszakás = éjszakás;
            Számoló = számoló;
            Magyarázat = magyarázat;
        }
    }
}
