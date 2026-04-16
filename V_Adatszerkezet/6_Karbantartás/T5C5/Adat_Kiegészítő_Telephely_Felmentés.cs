using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kiegészítő_Felmentés
    {
        public int Id { get; private set; }
        public string Címzett { get; private set; }
        public string Másolat { get; private set; }
        public string Tárgy { get; private set; }
        public string Kértvizsgálat { get; private set; }
        public string Bevezetés { get; private set; }
        public string Tárgyalás { get; private set; }
        public string Befejezés { get; private set; }
        public string CiklusTípus { get; private set; }

        public Adat_Kiegészítő_Felmentés(int id, string címzett, string másolat, string tárgy, string kértvizsgálat,
            string bevezetés, string tárgyalás, string befejezés, string ciklusTípus)
        {
            Id = id;
            Címzett = címzett;
            Másolat = másolat;
            Tárgy = tárgy;
            Kértvizsgálat = kértvizsgálat;
            Bevezetés = bevezetés;
            Tárgyalás = tárgyalás;
            Befejezés = befejezés;
            CiklusTípus = ciklusTípus;
        }
    }
}
