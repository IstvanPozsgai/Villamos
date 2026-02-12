using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Jármű_Takarítás_Árak
    {
        public double Id { get; private set; }
        public string JárműTípus { get; private set; }
        public string Takarítási_fajta { get; private set; }
        public int Napszak { get; private set; }
        public double Ár { get; private set; }
        public DateTime Érv_kezdet { get; private set; }
        public DateTime Érv_vég { get; private set; }

        public Adat_Jármű_Takarítás_Árak(double id, string járműTípus, string takarítási_fajta, int napszak, double ár, DateTime érv_kezdet, DateTime érv_vég)
        {
            Id = id;
            JárműTípus = járműTípus;
            Takarítási_fajta = takarítási_fajta;
            Napszak = napszak;
            Ár = ár;
            Érv_kezdet = érv_kezdet;
            Érv_vég = érv_vég;
        }

        public Adat_Jármű_Takarítás_Árak(double id, DateTime érv_vég)
        {
            Id = id;
            Érv_vég = érv_vég;
        }
    }
}
