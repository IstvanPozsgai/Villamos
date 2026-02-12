using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Jármű_Takarítás_J1
    {
        public DateTime Dátum { get; private set; }
        public int J1megfelelő { get; private set; }
        public int J1nemmegfelelő { get; private set; }
        public int Napszak { get; private set; }
        public string Típus { get; private set; }

        public Adat_Jármű_Takarítás_J1(DateTime dátum, int j1megfelelő, int j1nemmegfelelő, int napszak, string típus)
        {
            Dátum = dátum;
            J1megfelelő = j1megfelelő;
            J1nemmegfelelő = j1nemmegfelelő;
            Napszak = napszak;
            Típus = típus;
        }
    }
}
