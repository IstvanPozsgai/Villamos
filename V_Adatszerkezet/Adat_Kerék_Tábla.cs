using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kerék_Tábla
    {
        public string Kerékberendezés { get; private set; }
        public string Kerékmegnevezés { get; private set; }
        public string Kerékgyártásiszám { get; private set; }
        public string Föléberendezés { get; private set; }
        public string Azonosító { get; private set; }
        public string Pozíció { get; private set; }
        public DateTime Dátum { get; private set; }
        public string Objektumfajta { get; private set; }

        public Adat_Kerék_Tábla(string kerékberendezés, string kerékmegnevezés, string kerékgyártásiszám,
            string föléberendezés, string azonosító, string pozíció, DateTime dátum, string objektumfajta)
        {
            Kerékberendezés = kerékberendezés;
            Kerékmegnevezés = kerékmegnevezés;
            Kerékgyártásiszám = kerékgyártásiszám;
            Föléberendezés = föléberendezés;
            Azonosító = azonosító;
            Pozíció = pozíció;
            Dátum = dátum;
            Objektumfajta = objektumfajta;
        }
    }
}
