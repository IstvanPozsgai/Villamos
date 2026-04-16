using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Hétvége_Beosztás
    {
        public long Id { get; private set; }
        public string Vonal { get; private set; }
        public string Kocsi1 { get; private set; }
        public string Kocsi2 { get; private set; }
        public string Kocsi3 { get; private set; }
        public string Kocsi4 { get; private set; }
        public string Kocsi5 { get; private set; }
        public string Kocsi6 { get; private set; }
        public string Vissza1 { get; private set; }
        public string Vissza2 { get; private set; }
        public string Vissza3 { get; private set; }
        public string Vissza4 { get; private set; }
        public string Vissza5 { get; private set; }
        public string Vissza6 { get; private set; }

        public Adat_Hétvége_Beosztás(long id, string vonal, string kocsi1, string kocsi2, string kocsi3, string kocsi4, string kocsi5, string kocsi6, string vissza1, string vissza2, string vissza3, string vissza4, string vissza5, string vissza6)
        {
            Id = id;
            Vonal = vonal;
            Kocsi1 = kocsi1;
            Kocsi2 = kocsi2;
            Kocsi3 = kocsi3;
            Kocsi4 = kocsi4;
            Kocsi5 = kocsi5;
            Kocsi6 = kocsi6;
            Vissza1 = vissza1;
            Vissza2 = vissza2;
            Vissza3 = vissza3;
            Vissza4 = vissza4;
            Vissza5 = vissza5;
            Vissza6 = vissza6;
        }
    }
}
