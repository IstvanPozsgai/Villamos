using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kidobó
    {
        public string Viszonylat { get; private set; }
        public string Forgalmiszám { get; private set; }
        public string Szolgálatiszám { get; private set; }
        public string Jvez { get; private set; }
        public DateTime Kezdés { get; private set; }
        public DateTime Végzés { get; private set; }
        public string Kezdéshely { get; private set; }
        public string Végzéshely { get; private set; }
        public string Kód { get; private set; }
        public string Tárolásihely { get; private set; }
        public string Villamos { get; private set; }
        public string Megjegyzés { get; private set; }
        public string Szerelvénytípus { get; private set; }

        public string Törzsszám { get; private set; }

        public Adat_Kidobó(string viszonylat, string forgalmiszám, string szolgálatiszám, string jvez, DateTime kezdés, DateTime végzés, string kezdéshely, string végzéshely, string kód, string tárolásihely, string villamos, string megjegyzés, string szerelvénytípus)
        {
            Viszonylat = viszonylat;
            Forgalmiszám = forgalmiszám;
            Szolgálatiszám = szolgálatiszám;
            Jvez = jvez;
            Kezdés = kezdés;
            Végzés = végzés;
            Kezdéshely = kezdéshely;
            Végzéshely = végzéshely;
            Kód = kód;
            Tárolásihely = tárolásihely;
            Villamos = villamos;
            Megjegyzés = megjegyzés;
            Szerelvénytípus = szerelvénytípus;
        }

        public Adat_Kidobó(string viszonylat, string forgalmiszám, string szolgálatiszám, string jvez, DateTime kezdés, DateTime végzés, string kezdéshely, string végzéshely, string kód, string tárolásihely, string villamos, string megjegyzés, string szerelvénytípus, string törzsszám)
        {
            Viszonylat = viszonylat;
            Forgalmiszám = forgalmiszám;
            Szolgálatiszám = szolgálatiszám;
            Jvez = jvez;
            Kezdés = kezdés;
            Végzés = végzés;
            Kezdéshely = kezdéshely;
            Végzéshely = végzéshely;
            Kód = kód;
            Tárolásihely = tárolásihely;
            Villamos = villamos;
            Megjegyzés = megjegyzés;
            Szerelvénytípus = szerelvénytípus;
            Törzsszám = törzsszám;
        }
    }

}
