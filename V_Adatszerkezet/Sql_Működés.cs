namespace Villamos.Adatszerkezet
{
    public class Sql_Működés
    {
        public int Id { get; set; }
        public string Fájl { get; set; }
        public string Jelszó { get; set; }
        public string Tábla { get; set; }

        public Sql_Működés(int id,string fájl, string jelszó, string tábla)
        {
            Id=id;
            Fájl = fájl;
            Jelszó = jelszó;
            Tábla = tábla;
        }

        public Sql_Működés()
        {
        }
    }

    //A sqlite adatbázisban a következő táblát hoztuk létre, hogy a mdb fájlok adatait tároljuk:
    //CREATE TABLE "Tbl_Muk" (
    //"Fájl"	TEXT NOT NULL,
    //"Jelszó"	TEXT NOT NULL,
    //"Tábla"	TEXT NOT NULL,
    //"Id"	INTEGER NOT NULL,
    //PRIMARY KEY("Id" AUTOINCREMENT)

}
