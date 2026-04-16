using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Hétvége_Előírás
    {
        public long Id { get; private set; }
        public string Vonal { get; private set; }
        public long Mennyiség { get; private set; }
        public int Red { get; private set; }
        public int Green { get; private set; }
        public int Blue { get; private set; }

        public Adat_Hétvége_Előírás(long id, string vonal, long mennyiség, int red, int green, int blue)
        {
            Id = id;
            Vonal = vonal;
            Mennyiség = mennyiség;
            Red = red;
            Green = green;
            Blue = blue;
        }
    }
}
