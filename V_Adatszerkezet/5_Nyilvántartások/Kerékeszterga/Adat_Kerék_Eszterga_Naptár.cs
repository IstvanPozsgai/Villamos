using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kerék_Eszterga_Naptár
    {
        public DateTime Idő { get; private set; }
        public bool Munkaidő { get; private set; }
        public bool Foglalt { get; private set; }
        public string Pályaszám { get; private set; }
        public string Megjegyzés { get; private set; }
        public long BetűSzín { get; private set; }
        public long HáttérSzín { get; private set; }

        public bool Marad { get; set; }

        public DateTime Dátumtól { get; private set; }
        public DateTime Dátumig { get; private set; }

        public Adat_Kerék_Eszterga_Naptár(DateTime idő, bool munkaidő, bool foglalt, string pályaszám, string megjegyzés, long betűSzín, long háttérSzín, bool marad)
        {
            Idő = idő;
            Munkaidő = munkaidő;
            Foglalt = foglalt;
            Pályaszám = pályaszám;
            Megjegyzés = megjegyzés;
            BetűSzín = betűSzín;
            HáttérSzín = háttérSzín;
            Marad = marad;
        }

        public Adat_Kerék_Eszterga_Naptár(DateTime idő, bool foglalt, string pályaszám, string megjegyzés, long betűSzín, long háttérSzín, bool marad)
        {
            Idő = idő;
            Foglalt = foglalt;
            Pályaszám = pályaszám;
            Megjegyzés = megjegyzés;
            BetűSzín = betűSzín;
            HáttérSzín = háttérSzín;
            Marad = marad;
        }

        public Adat_Kerék_Eszterga_Naptár(DateTime idő)
        {
            Idő = idő;
        }

        public Adat_Kerék_Eszterga_Naptár(bool munkaidő, DateTime dátumtól, DateTime dátumig)
        {
            Munkaidő = munkaidő;
            Dátumtól = dátumtól;
            Dátumig = dátumig;
        }
    }
}
