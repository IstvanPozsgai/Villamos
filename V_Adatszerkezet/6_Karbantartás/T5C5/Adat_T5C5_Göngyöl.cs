using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{

    public class Adat_T5C5_Göngyöl
    {
        public string Azonosító { get; private set; }
        public DateTime Utolsórögzítés { get; private set; }
        public DateTime Vizsgálatdátuma { get; private set; }
        public DateTime Utolsóforgalminap { get; private set; }
        public string Vizsgálatfokozata { get; private set; }
        public int Vizsgálatszáma { get; private set; }
        public int Futásnap { get; private set; }
        public string Telephely { get; private set; }

        public Adat_T5C5_Göngyöl(string azonosító, DateTime utolsórögzítés, DateTime vizsgálatdátuma, DateTime utolsóforgalminap, string vizsgálatfokozata, int vizsgálatszáma, int futásnap, string telephely)
        {
            Azonosító = azonosító;
            Utolsórögzítés = utolsórögzítés;
            Vizsgálatdátuma = vizsgálatdátuma;
            Utolsóforgalminap = utolsóforgalminap;
            Vizsgálatfokozata = vizsgálatfokozata;
            Vizsgálatszáma = vizsgálatszáma;
            Futásnap = futásnap;
            Telephely = telephely;
        }
    }
}
