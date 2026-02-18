using SQLite.CodeFirst;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Villamos.Adatszerkezet
{
    public class SAdat_Belépés_Oldalak
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
        

        public SAdat_Belépés_Oldalak() { }

        public SAdat_Belépés_Oldalak(int oldalId, string fromName, string menuName,
                                     string menuFelirat, bool látható, bool törölt)
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
        public Context_Bejelentkezés_Oldalak(string ConnectionSTring)
            : base(ConnectionSTring) { }

        public DbSet<SAdat_Belépés_Oldalak> Oldalak { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteInitializer =
                new SqliteCreateDatabaseIfNotExists<Context_Bejelentkezés_Oldalak>(modelBuilder);
            Database.SetInitializer(sqliteInitializer);
        }
    }
}