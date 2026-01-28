using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public  class Adat_Általános_Long_String
    {
        public long Id { get;private set; }
        public string  Szöveg { get; private set; }

        public Adat_Általános_Long_String(long id, string szöveg)
        {
            Id = id;
            Szöveg = szöveg;
        }
    }

    public class Adat_Általános_Int_String
    {
        public int Id { get; private set; }
        public string Szöveg { get; private set; }

        public Adat_Általános_Int_String(int id, string szöveg)
        {
            Id = id;
            Szöveg = szöveg;
        }
    }

    public class Adat_Általános_String_Dátum
    {
        public DateTime  Dátum { get; private set; }
        public string Szöveg { get; private set; }

        public Adat_Általános_String_Dátum(DateTime dátum, string szöveg)
        {
            Dátum = dátum;
            Szöveg = szöveg;
        }
    }

    public class Szín
    {
        public int Red { get; private set; }
        public int Green { get; private set; }
        public int Blue { get; private set; }

        public Szín(int red, int green, int blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }
    }


}
