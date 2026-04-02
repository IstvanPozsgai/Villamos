namespace Villamos.Adatszerkezet
{
    public class Adat_Bejelentkezés_Gombok
    {
        public int GombokId { get; private set; }
        public string FormName { get; private set; }
        public string GombName { get; private set; }
        public string GombFelirat { get; private set; }
        public string Szervezet { get; private set; }

        public bool Látható { get; private set; }
        public bool Törölt { get; private set; }

        public bool Súgó { get; set; }

        public Adat_Bejelentkezés_Gombok(int gombokId, string formName, string gombName, string gombFelirat, string szervezet, bool látható, bool törölt)
        {
            GombokId = gombokId;
            FormName = formName;
            GombName = gombName;
            GombFelirat = gombFelirat;
            Szervezet = szervezet;
            Látható = látható;
            Törölt = törölt;
        }
    }
}
