using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Behajtás_Dolgozótábla
    {
        public string Dolgozószám { get; set; }
        public string Dolgozónév { get; set; }
        public string Szervezetiegység { get; set; }
        public string Munkakör { get; set; }
        public bool Státus { get; set; }

        public Adat_Behajtás_Dolgozótábla(string dolgozószám, string dolgozónév, string szervezetiegység, string munkakör, bool státus)
        {
            Dolgozószám = dolgozószám;
            Dolgozónév = dolgozónév;
            Szervezetiegység = szervezetiegység;
            Munkakör = munkakör;
            Státus = státus;
        }
    }
}
