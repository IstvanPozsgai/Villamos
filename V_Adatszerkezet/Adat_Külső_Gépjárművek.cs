using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Külső_Gépjárművek
    {
        public double Id { get; private set; }
        public string Frsz { get; private set; }
        public double Cégid { get; private set; }
        public bool Státus { get; private set; }

        public Adat_Külső_Gépjárművek(double id, string frsz, double cégid, bool státus)
        {
            Id = id;
            Frsz = frsz;
            Cégid = cégid;
            Státus = státus;
        }

        public Adat_Külső_Gépjárművek(double id, bool státus)
        {
            Id = id;
            Státus = státus;
        }
    }
}
