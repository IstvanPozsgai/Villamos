using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kidobó_Segéd
    {
        public string Forgalmiszám { get; private set; }
        public string Szolgálatiszám { get; private set; }
        public DateTime Kezdés { get; private set; }
        public DateTime Végzés { get; private set; }
        public string Kezdéshely { get; private set; }
        public string Végzéshely { get; private set; }
        public string Változatnév { get; private set; }
        public string Megjegyzés { get; private set; }

        public Adat_Kidobó_Segéd(string forgalmiszám, string szolgálatiszám, DateTime kezdés, DateTime végzés, string kezdéshely, string végzéshely, string változatnév, string megjegyzés)
        {
            Forgalmiszám = forgalmiszám;
            Szolgálatiszám = szolgálatiszám;
            Kezdés = kezdés;
            Végzés = végzés;
            Kezdéshely = kezdéshely;
            Végzéshely = végzéshely;
            Változatnév = változatnév;
            Megjegyzés = megjegyzés;
        }
    }
}
