using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_CAF_Szinezés
    {
        public string Telephely { get; private set; }
        public double SzínPSZgar { get; private set; }
        public double SzínPsz { get; private set; }
        public double SzínIStűrés { get; private set; }
        public double SzínIS { get; private set; }
        public double SzínP { get; private set; }
        public double Színszombat { get; private set; }
        public double SzínVasárnap { get; private set; }

        public double Szín_E { get; private set; }
        public double Szín_dollár { get; private set; }
        public double Szín_Kukac { get; private set; }
        public double Szín_Hasteg { get; private set; }
        public double Szín_jog { get; private set; }
        public double Szín_nagyobb { get; private set; }

        public Adat_CAF_Szinezés(string telephely, double színPSZgar, double színPsz, double színIStűrés, double színIS, double színP, double színszombat, double színVasárnap, double szín_E, double szín_dollár, double szín_Kukac, double szín_Hasteg, double szín_jog, double szín_nagyobb)
        {
            Telephely = telephely;
            SzínPSZgar = színPSZgar;
            SzínPsz = színPsz;
            SzínIStűrés = színIStűrés;
            SzínIS = színIS;
            SzínP = színP;
            Színszombat = színszombat;
            SzínVasárnap = színVasárnap;
            Szín_E = szín_E;
            Szín_dollár = szín_dollár;
            Szín_Kukac = szín_Kukac;
            Szín_Hasteg = szín_Hasteg;
            Szín_jog = szín_jog;
            Szín_nagyobb = szín_nagyobb;
        }
    }
}
