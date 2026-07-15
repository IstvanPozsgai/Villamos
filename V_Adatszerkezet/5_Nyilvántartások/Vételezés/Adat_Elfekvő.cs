using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_Elfekvő
    {
        public long Id { get; private set; }
        public string Anyag { get; private set; }
        public string Anyag_rövid_szövege { get; private set; }
        public string Raktárhely { get; private set; }
        public double Szabadon_használható { get; private set; }
        public double Szab_felh_érték { get; private set; }
        public string Sarzs { get; private set; }
        public DateTime Utolsó_mozgás { get; private set; } 

        public Adat_Elfekvő(long id, string anyag, string anyag_rövid_szövege, string raktárhely,
                            double szabadon_használható, double szab_felh_érték, string sarzs, DateTime utolsó_mozgás)
        {
            Id = id;
            Anyag = anyag;
            Anyag_rövid_szövege = anyag_rövid_szövege;
            Raktárhely = raktárhely;
            Szabadon_használható = szabadon_használható;
            Szab_felh_érték = szab_felh_érték;
            Sarzs = sarzs;
            Utolsó_mozgás = utolsó_mozgás;
        }
    }
}