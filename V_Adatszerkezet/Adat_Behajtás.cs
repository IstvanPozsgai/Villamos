using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_Behajtás_Alap
    {
        public int Id { get; set; }
        public string Adatbázisnév { get; set; }
        public string Sorszámbetűjele { get; set; }
        public int Sorszámkezdete { get; set; }
        public DateTime Engedélyérvényes { get; set; }
        public int Státus { get; set; }
        public string Adatbáziskönyvtár { get; set; }

        public Adat_Behajtás_Alap(int id, string adatbázisnév, string sorszámbetűjele, int sorszámkezdete, DateTime engedélyérvényes, int státus, string adatbáziskönyvtár)
        {
            Id = id;
            Adatbázisnév = adatbázisnév;
            Sorszámbetűjele = sorszámbetűjele;
            Sorszámkezdete = sorszámkezdete;
            Engedélyérvényes = engedélyérvényes;
            Státus = státus;
            Adatbáziskönyvtár = adatbáziskönyvtár;
        }
    }
}
