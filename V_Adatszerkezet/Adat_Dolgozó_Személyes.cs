using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Dolgozó_Személyes
    {
        public string Anyja { get; private set; }
        public string Dolgozószám { get; private set; }
        public string Ideiglenescím { get; private set; }
        public string Lakcím { get; private set; }
        public string Leánykori { get; private set; }
        public string Születésihely { get; private set; }

        public DateTime Születésiidő { get; private set; }
        public string Telefonszám1 { get; private set; }
        public string Telefonszám2 { get; private set; }
        public string Telefonszám3 { get; private set; }

        public Adat_Dolgozó_Személyes(string anyja, string dolgozószám, string ideiglenescím, string lakcím, string leánykori, string születésihely, DateTime születésiidő, string telefonszám1, string telefonszám2, string telefonszám3)
        {
            Anyja = anyja;
            Dolgozószám = dolgozószám;
            Ideiglenescím = ideiglenescím;
            Lakcím = lakcím;
            Leánykori = leánykori;
            Születésihely = születésihely;
            Születésiidő = születésiidő;
            Telefonszám1 = telefonszám1;
            Telefonszám2 = telefonszám2;
            Telefonszám3 = telefonszám3;
        }
    }
}
