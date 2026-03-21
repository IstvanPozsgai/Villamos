using System.IO;

namespace Villamos.Adatszerkezet
{
    public class Sql_Működés
    {
        public int Id { get; set; }
        public string Fájl { get; set; }
        public string Jelszó { get; set; }
        public string Tábla { get; set; }

        public bool Törölt { get; set; } = false;

        public string Könyvtár => Path.GetDirectoryName(Fájl);
        public string Fájlnév => Path.GetFileName(Fájl);

        public Sql_Működés(int id, string fájl, string jelszó, string tábla)
        {
            Id = id;
            Fájl = fájl;
            Jelszó = jelszó;
            Tábla = tábla;
        }

        public Sql_Működés(int id, string fájl, string jelszó, string tábla, bool törölt) : this(id, fájl, jelszó, tábla)
        {
            Törölt = törölt;
        }

        public Sql_Működés()
        {
        }
    }



}
