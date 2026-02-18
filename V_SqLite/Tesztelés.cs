using Villamos.Adatszerkezet;

namespace Villamos.V_SqLite
{
    public static class Tesztelés
    {
        public  void Tesztelés()
        {
            using (Context_Bejelentkezés_Oldalak db = new Context_Bejelentkezés_Oldalak())
            {
                SAdat_Belépés_Oldalak ujOldal = new SAdat_Belépés_Oldalak(1, "Home", "Főmenü", "Kezdőlap", true, false);
                db.Oldalak.Add(ujOldal);
                db.SaveChanges(); // Itt jön létre az adatbázis és a tábla, ha még nem létezik
            }
        }

    }
}
