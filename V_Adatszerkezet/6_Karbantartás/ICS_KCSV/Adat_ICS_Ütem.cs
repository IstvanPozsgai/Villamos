namespace Villamos.Adatszerkezet
{
    public class Adat_ICS_Ütem
    {

        public string Azonosító { get; private set; }
        public int Állapot { get; private set; }
        public bool Ütemez { get; private set; }
        public string Rendelésiszám { get; private set; }
        public int V_Sorszám { get; private set; }
        public string V_Megnevezés { get; private set; }
        public int V_km_ { get; private set; }
        public string Következő_V { get; private set; }
        public int Következővizsgálatszám { get; private set; }

        public Adat_ICS_Ütem(string azonosító, int állapot, bool ütemez, string rendelésiszám, int v_Sorszám, string v_Megnevezés, int v_km_, string következő_V, int következővizsgálatszám)
        {
            Azonosító = azonosító;
            Állapot = állapot;
            Ütemez = ütemez;
            Rendelésiszám = rendelésiszám;
            V_Sorszám = v_Sorszám;
            V_Megnevezés = v_Megnevezés;
            V_km_ = v_km_;
            Következő_V = következő_V;
            Következővizsgálatszám = következővizsgálatszám;
        }
    }
}
