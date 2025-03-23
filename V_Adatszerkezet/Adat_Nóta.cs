using System;

namespace Villamos.V_Adatszerkezet
{
    public class Adat_Nóta
    {
        public long Id { get; private set; }
        public string Berendezés { get; private set; }
        public string Készlet_Sarzs { get; private set; }
        public string Raktár { get; private set; }
        public string Telephely { get; private set; }
        public string Forgóváz { get; private set; }
        public bool Beépíthető { get; private set; }
        public string MűszakiM { get; private set; }
        public string OsztásiM { get; private set; }
        public DateTime Dátum { get; private set; }
        public int Státus { get; private set; }

        public Adat_Nóta(long id, string berendezés, string készlet_Sarzs, string raktár, string telephely, string forgóváz, bool beépíthető, string műszakiM, string osztásiM, DateTime dátum, int státus)
        {
            Id = id;
            Berendezés = berendezés;
            Készlet_Sarzs = készlet_Sarzs;
            Raktár = raktár;
            Telephely = telephely;
            Forgóváz = forgóváz;
            Beépíthető = beépíthető;
            MűszakiM = műszakiM;
            OsztásiM = osztásiM;
            Dátum = dátum;
            Státus = státus;
        }

        public Adat_Nóta(long id, string berendezés, string készlet_Sarzs, string raktár, int státus)
        {
            Id = id;
            Berendezés = berendezés;
            Készlet_Sarzs = készlet_Sarzs;
            Raktár = raktár;
            Státus = státus;
        }
    }

    public class Adat_Nóta_SAP
    {
        public string Berendezés { get; private set; }
        public string Rendszerstátus { get; private set; }
        public string Készlet_Sarzs { get; private set; }
        public string Raktár { get; private set; }
        public string Rendezési { get; private set; }

        public Adat_Nóta_SAP(string berendezés, string rendszerstátus, string készlet_Sarzs, string raktár, string rendezési)
        {
            Berendezés = berendezés;
            Rendszerstátus = rendszerstátus;
            Készlet_Sarzs = készlet_Sarzs;
            Raktár = raktár;
            Rendezési = rendezési;
        }
    }
}
