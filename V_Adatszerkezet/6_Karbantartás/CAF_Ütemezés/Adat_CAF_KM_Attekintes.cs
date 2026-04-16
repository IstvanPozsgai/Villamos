using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_CAF_KM_Attekintes
    {
        public Adat_CAF_KM_Attekintes(string azonosito, long? utolso_vizsgalat_valos_allasa, long? kov_p0, long? kov_p1, long? kov_p2, long? utolso_p0_kozott, long? utolso_p1_kozott, long? utolso_p3_es_p2_kozott, long? elso_p2, long? elso_p3, long? utolso_p0_sorszam, long? utolso_p1_sorszam, long? utolso_p2_sorszam, long? utolso_p3_sorszam)
        {
            this.azonosito = azonosito;
            this.utolso_vizsgalat_valos_allasa = utolso_vizsgalat_valos_allasa;
            this.kov_p0 = kov_p0;
            this.kov_p1 = kov_p1;
            this.kov_p2 = kov_p2;
            this.utolso_p0_kozott = utolso_p0_kozott;
            this.utolso_p1_kozott = utolso_p1_kozott;
            this.utolso_p3_es_p2_kozott = utolso_p3_es_p2_kozott;
            this.elso_p2 = elso_p2;
            this.elso_p3 = elso_p3;
            this.utolso_p0_sorszam = utolso_p0_sorszam;
            this.utolso_p1_sorszam = utolso_p1_sorszam;
            this.utolso_p2_sorszam = utolso_p2_sorszam;
            this.utolso_p3_sorszam = utolso_p3_sorszam;
        }

        public string azonosito { get; set; }
        public long? utolso_vizsgalat_valos_allasa { get; set; }
        public long? kov_p0 { get; set; }
        public long? kov_p1 { get; set; }
        public long? kov_p2 { get; set; }
        public long? utolso_p0_kozott { get; set; }
        public long? utolso_p1_kozott { get; set; }
        public long? utolso_p3_es_p2_kozott { get; set; }
        public long? elso_p2 { get; set; }
        public long? elso_p3 { get; set; }
        public long? utolso_p0_sorszam { get; set; }
        public long? utolso_p1_sorszam { get; set; }
        public long? utolso_p2_sorszam { get; set; }
        public long? utolso_p3_sorszam { get; set; }


    }
}
