namespace Villamos.Adatszerkezet
{
    public class Adat_Bejelentkezés_Fordító
    {
        public int GombokId { get; private set; }
        public string FromName { get; private set; }
        public string GombName { get; private set; }
        public string Szervezet { get; private set; }

        public int MelyikBetű { get; private set; }
        public int MelyikOszlop { get; private set; }

        public Adat_Bejelentkezés_Fordító(int gombid, string formname, string gombname, string szervezet, int melyikbetű, int melyikoszlop)
        {
            GombokId = gombid;
            FromName = formname;
            GombName = gombname;
            Szervezet = szervezet;
            MelyikBetű = melyikbetű;
            MelyikOszlop = melyikoszlop;
        }
    }
}

