using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Sérülés_Jelentés
    {
        public int Sorszám { get; set; }
        public string Telephely { get; set; }
        public DateTime Dátum { get; set; }
        public string Balesethelyszín { get; set; }
        public string Viszonylat { get; set; }
        public string Rendszám { get; set; }
        public string Járművezető { get; set; }
        public int Rendelésszám { get; set; }
        public int Kimenetel { get; set; }
        public int Státus { get; set; }
        public string Iktatószám { get; set; }
        public string Típus { get; set; }
        public string Szerelvény { get; set; }
        public int Forgalmiakadály { get; set; }
        public bool Műszaki { get; set; }
        public bool Anyagikár { get; set; }
        public string Biztosító { get; set; }
        public bool Személyisérülés { get; set; }
        public bool Személyisérülés1 { get; set; }
        public int Biztosítóidő { get; set; }
        public string Mivelütközött { get; set; }
        public int Anyagikárft { get; set; }
        public string Leírás { get; set; }
        public string Leírás1 { get; set; }
        public string Balesethelyszín1 { get; set; }
        public string Esemény { get; set; }
        public int Anyagikárft1 { get; set; }
        public int Státus1 { get; set; }
        public string Kmóraállás { get; set; }

        public Adat_Sérülés_Jelentés(int sorszám, string telephely, DateTime dátum, string balesethelyszín, string viszonylat, string rendszám, string járművezető, int rendelésszám, int kimenetel,
                int státus, string iktatószám, string típus, string szerelvény, int forgalmiakadály, bool műszaki, bool anyagikár, string biztosító, bool személyisérülés, bool személyisérülés1, int biztosítóidő,
                string mivelütközött, int anyagikárft, string leírás, string leírás1, string balesethelyszín1, string esemény, int anyagikárft1, int státus1, string kmóraállás)
        {
            Sorszám = sorszám;
            Telephely = telephely;
            Dátum = dátum;
            Balesethelyszín = balesethelyszín;
            Viszonylat = viszonylat;
            Rendszám = rendszám;
            Járművezető = járművezető;
            Rendelésszám = rendelésszám;
            Kimenetel = kimenetel;
            Státus = státus;
            Iktatószám = iktatószám;
            Típus = típus;
            Szerelvény = szerelvény;
            Forgalmiakadály = forgalmiakadály;
            Műszaki = műszaki;
            Anyagikár = anyagikár;
            Biztosító = biztosító;
            Személyisérülés = személyisérülés;
            Személyisérülés1 = személyisérülés1;
            Biztosítóidő = biztosítóidő;
            Mivelütközött = mivelütközött;
            Anyagikárft = anyagikárft;
            Leírás = leírás;
            Leírás1 = leírás1;
            Balesethelyszín1 = balesethelyszín1;
            Esemény = esemény;
            Anyagikárft1 = anyagikárft1;
            Státus1 = státus1;
            Kmóraállás = kmóraállás;
        }

        public Adat_Sérülés_Jelentés(int sorszám, int státus1)
        {
            Sorszám = sorszám;
            Státus1 = státus1;
        }
    }
}
