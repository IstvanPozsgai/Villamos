namespace Villamos.V_Adatszerkezet
{
    public class Adat_Szerszám_FejLáb
    {
        public int Id { get; private set; }
        public string Fejléc_Bal { get; private set; }
        public string Fejléc_Közép { get; private set; }
        public string Fejléc_Jobb { get; private set; }
        public string Lábléc_Bal { get; private set; }
        public string Lábléc_Közép { get; private set; }
        public string Lábléc_Jobb { get; private set; }

        public Adat_Szerszám_FejLáb(int id, string fejléc_Bal, string fejléc_Közép, string fejléc_Jobb, string lábléc_Bal, string lábléc_Közép, string lábléc_Jobb)
        {
            Id = id;
            Fejléc_Bal = fejléc_Bal;
            Fejléc_Közép = fejléc_Közép;
            Fejléc_Jobb = fejléc_Jobb;
            Lábléc_Bal = lábléc_Bal;
            Lábléc_Közép = lábléc_Közép;
            Lábléc_Jobb = lábléc_Jobb;
        }
    }
}
