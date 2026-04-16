using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Jármű_Takarítás_Létszám
    {
        public DateTime Dátum { get; private set; }
        public int Előírt { get; private set; }
        public int Megjelent { get; private set; }
        public int Napszak { get; private set; }
        public int Ruhátlan { get; private set; }

        public Adat_Jármű_Takarítás_Létszám(DateTime dátum, int előírt, int megjelent, int napszak, int ruhátlan)
        {
            Dátum = dátum;
            Előírt = előírt;
            Megjelent = megjelent;
            Napszak = napszak;
            Ruhátlan = ruhátlan;
        }
    }
}
