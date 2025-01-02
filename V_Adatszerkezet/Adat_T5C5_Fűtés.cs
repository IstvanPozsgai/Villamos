using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Villamos_Adatszerkezet
{
    public  class Adat_T5C5_Fűtés
    {

        public long ID { get; private set; }
        public string Pályaszám { get; private set; }
        public string Telephely { get; private set; }
        public DateTime Dátum { get; private set; }
        public string Dolgozó { get; private set; }
        public double I_szakasz { get; private set; }
        public double II_szakasz { get; private set; }
        public int Fűtés_típusa { get; private set; }
        public string Jófűtés { get; private set; }
        public string Megjegyzés { get; private set; }
        public int Beállítási_értékek { get; private set; }
        public string Módosító { get; private set; }
        public DateTime Mikor { get; private set; }

        public Adat_T5C5_Fűtés(long iD, string pályaszám, string telephely, DateTime dátum, string dolgozó, double i_szakasz, double iI_szakasz, int fűtés_típusa, string jófűtés, string megjegyzés, int beállítási_értékek, string módosító, DateTime mikor)
        {
            ID = iD;
            Pályaszám = pályaszám;
            Telephely = telephely;
            Dátum = dátum;
            Dolgozó = dolgozó;
            I_szakasz = i_szakasz;
            II_szakasz = iI_szakasz;
            Fűtés_típusa = fűtés_típusa;
            Jófűtés = jófűtés;
            Megjegyzés = megjegyzés;
            Beállítási_értékek = beállítási_értékek;
            Módosító = módosító;
            Mikor = mikor;
        }
    }
}
