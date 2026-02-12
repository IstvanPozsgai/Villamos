using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Főkönyv_ZSER
    {
        public string Viszonylat { get; private set; }
        public string Forgalmiszám { get; private set; }
        public DateTime Tervindulás { get; private set; }
        public DateTime Tényindulás { get; private set; }
        public DateTime Tervérkezés { get; private set; }
        public DateTime Tényérkezés { get; private set; }
        public string Napszak { get; private set; }
        public string Szerelvénytípus { get; private set; }
        public long Kocsikszáma { get; private set; }
        public string Megjegyzés { get; private set; }
        public string Kocsi1 { get; private set; }
        public string Kocsi2 { get; private set; }
        public string Kocsi3 { get; private set; }
        public string Kocsi4 { get; private set; }
        public string Kocsi5 { get; private set; }
        public string Kocsi6 { get; private set; }
        public string Ellenőrző { get; private set; }
        public string Státus { get; private set; }

        public Adat_Főkönyv_ZSER(string viszonylat, string forgalmiszám, DateTime tervindulás, DateTime tényindulás, DateTime tervérkezés, DateTime tényérkezés, string napszak, string szerelvénytípus, long kocsikszáma, string megjegyzés, string kocsi1, string kocsi2, string kocsi3, string kocsi4, string kocsi5, string kocsi6, string ellenőrző, string státus)
        {
            Viszonylat = viszonylat;
            Forgalmiszám = forgalmiszám;
            Tervindulás = tervindulás;
            Tényindulás = tényindulás;
            Tervérkezés = tervérkezés;
            Tényérkezés = tényérkezés;
            Napszak = napszak;
            Szerelvénytípus = szerelvénytípus;
            Kocsikszáma = kocsikszáma;
            Megjegyzés = megjegyzés;
            Kocsi1 = kocsi1;
            Kocsi2 = kocsi2;
            Kocsi3 = kocsi3;
            Kocsi4 = kocsi4;
            Kocsi5 = kocsi5;
            Kocsi6 = kocsi6;
            Ellenőrző = ellenőrző;
            Státus = státus;
        }

        public Adat_Főkönyv_ZSER(string viszonylat, string forgalmiszám, DateTime tervindulás)
        {
            Viszonylat = viszonylat;
            Forgalmiszám = forgalmiszám;
            Tervindulás = tervindulás;
        }

        public Adat_Főkönyv_ZSER(string viszonylat, string forgalmiszám, DateTime tervindulás, string ellenőrző)
        {
            Viszonylat = viszonylat;
            Forgalmiszám = forgalmiszám;
            Tervindulás = tervindulás;
            Ellenőrző = ellenőrző;
        }

        public Adat_Főkönyv_ZSER(string napszak, string viszonylat, string forgalmiszám, DateTime tervindulás)
        {
            Viszonylat = viszonylat;
            Forgalmiszám = forgalmiszám;
            Tervindulás = tervindulás;
            Napszak = napszak;
        }
    }
}
