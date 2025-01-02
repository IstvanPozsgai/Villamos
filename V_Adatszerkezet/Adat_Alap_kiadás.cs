namespace Villamos.Villamos_Adatszerkezet
{
  public  class Adat_Alap_kiadás_Típusszín
    {
        public string Típus { get; private set; }
        public long Szín { get; private set; }

        public Adat_Alap_kiadás_Típusszín(string típus, long szín)
        {
            Típus = típus;
            Szín = szín;
        }
    }
}
