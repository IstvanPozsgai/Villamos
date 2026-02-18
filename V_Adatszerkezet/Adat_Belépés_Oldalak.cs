using SQLite.CodeFirst;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;


namespace Villamos.Adatszerkezet
{
    public class Adat_Belépés_Oldalak
    {
        [Key]
        public int OldalId { get; private set; }

        [StringLength(255)]
        public string FromName { get; private set; }

        [StringLength(255)]
        public string MenuName { get; private set; }

        [StringLength(255)]
        public string MenuFelirat { get; private set; }
        public bool Látható { get; private set; }
        public bool Törölt { get; private set; }

        public Adat_Belépés_Oldalak()
        {

        }

        public Adat_Belépés_Oldalak(int oldalId, string fromName, string menuName, string menuFelirat, bool látható, bool törölt)
        {
            OldalId = oldalId;
            FromName = fromName;
            MenuName = menuName;
            MenuFelirat = menuFelirat;
            Látható = látható;
            Törölt = törölt;
        }
    }

    public class Context_Bejelentkezés_Oldalak : DbContext
    {
        // A konstruktorban megadjuk a kapcsolat nevét az App.config-ból
        public Context_Bejelentkezés_Oldalak() : base("SajátSqliteKapcsolat") { }

        public DbSet<Adat_Belépés_Oldalak> Oldalak { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Ez a sor aktiválja az automatikus táblalétrehozást SQLite-hoz
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<Context_Bejelentkezés_Oldalak>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }
    }
}
