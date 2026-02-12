using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Menetkimaradás_Főmérnökség
    {

        public string Viszonylat { get; private set; }
        public string Azonosító { get; private set; }
        public string Típus { get; private set; }
        public string Eseményjele { get; private set; }
        public DateTime Bekövetkezés { get; private set; }
        public long Kimaradtmenet { get; private set; }
        public string Jvbeírás { get; private set; }
        public string Vmbeírás { get; private set; }
        public string Javítás { get; private set; }
        public long Id { get; private set; }
        public bool Törölt { get; private set; }
        public string Jelentés { get; private set; }
        public long Tétel { get; private set; }

        public string Telephely { get; private set; }
        public string Szolgálat { get; private set; }

        public Adat_Menetkimaradás_Főmérnökség(string viszonylat, string azonosító, string típus, string eseményjele, DateTime bekövetkezés, long kimaradtmenet, string jvbeírás, string vmbeírás, string javítás, long id, bool törölt, string jelentés, long tétel, string telephely, string szolgálat)
        {
            Viszonylat = viszonylat;
            Azonosító = azonosító;
            Típus = típus;
            Eseményjele = eseményjele;
            Bekövetkezés = bekövetkezés;
            Kimaradtmenet = kimaradtmenet;
            Jvbeírás = jvbeírás;
            Vmbeírás = vmbeírás;
            Javítás = javítás;
            Id = id;
            Törölt = törölt;
            Jelentés = jelentés;
            Tétel = tétel;
            Telephely = telephely;
            Szolgálat = szolgálat;
        }
    }
}
