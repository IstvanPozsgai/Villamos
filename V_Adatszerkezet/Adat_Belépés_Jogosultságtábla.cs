using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Belépés_Jogosultságtábla
    {
        public string Név { get; private set; }
        public string Jogkörúj1 { get; private set; }
        public string Jogkörúj2 { get; private set; }

        public Adat_Belépés_Jogosultságtábla(string név, string jogkörúj1, string jogkörúj2)
        {
            Név = név;
            Jogkörúj1 = jogkörúj1;
            Jogkörúj2 = jogkörúj2;
        }
    }
}
