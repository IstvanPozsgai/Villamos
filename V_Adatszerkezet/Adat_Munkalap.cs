using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_MunkaRend
    {
        public long ID { get; private set; }
        public string Munkarend { get; private set; }
        public bool Látszódik { get; private set; }

        public Adat_MunkaRend(long iD, string munkarend, bool látszódik)
        {
            ID = iD;
            Munkarend = munkarend;
            Látszódik = látszódik;
        }
    }

}
