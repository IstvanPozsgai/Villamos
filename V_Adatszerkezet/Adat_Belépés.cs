using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Villamos_Adatszerkezet
{
    public  class Adat_Belépés_Bejelentkezés
    {
        public long Sorszám { get; private set; }
        public string  Név { get; private set; }
        public string  Jelszó { get; private set; }
        public string  Jogkör { get; private set; }

        public Adat_Belépés_Bejelentkezés(long sorszám, string név, string jelszó, string jogkör)
        {
            Sorszám = sorszám;
            Név = név;
            Jelszó = jelszó;
            Jogkör = jogkör;
        }
    }

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

    public class Adat_Belépés_WinTábla 
    {
        public string Név { get; private set; }
        public string Telephely { get; private set; }
        public string WinUser { get; private set; }

        public Adat_Belépés_WinTábla(string név, string telephely, string winUser)
        {
            Név = név;
            Telephely = telephely;
            WinUser = winUser;
        }
    }

    public class Adat_Belépés_Verzió
    {
        public long Id { get; private set; }
        public double Verzió { get; private set; }

        public Adat_Belépés_Verzió(long id, double verzió)
        {
            Id = id;
            Verzió = verzió;
        }
    }

}
