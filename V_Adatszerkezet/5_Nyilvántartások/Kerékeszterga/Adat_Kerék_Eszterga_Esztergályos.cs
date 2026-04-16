using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kerék_Eszterga_Esztergályos
    {
        public string Dolgozószám { get; private set; }
        public string Dolgozónév { get; private set; }

        public string Telephely { get; private set; }
        public int Státus { get; private set; }

        public Adat_Kerék_Eszterga_Esztergályos(string dolgozószám, string dolgozónév, string telephely, int státus)
        {
            Dolgozószám = dolgozószám;
            Dolgozónév = dolgozónév;
            Telephely = telephely;
            Státus = státus;
        }
    }

}
