using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Nóta_SAP
    {
        public string Berendezés { get; private set; }
        public string Rendszerstátus { get; private set; }
        public string Készlet_Sarzs { get; private set; }
        public string Raktár { get; private set; }
        public string Rendezési { get; private set; }
        public string Cikkszám { get; private set; }

        public Adat_Nóta_SAP(string berendezés, string rendszerstátus, string készlet_Sarzs, string raktár, string rendezési, string cikkszám)
        {
            Berendezés = berendezés;
            Rendszerstátus = rendszerstátus;
            Készlet_Sarzs = készlet_Sarzs;
            Raktár = raktár;
            Rendezési = rendezési;
            Cikkszám = cikkszám;
        }
    }
}
