using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Külső_Telephelyek
    {
        public double Id { get; private set; }
        public string Telephely { get; private set; }
        public double Cégid { get; private set; }
        public bool Státus { get; private set; }

        public Adat_Külső_Telephelyek(double id, string telephely, double cégid, bool státus)
        {
            Id = id;
            Telephely = telephely;
            Cégid = cégid;
            Státus = státus;
        }
    }
}
