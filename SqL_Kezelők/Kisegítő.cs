using Microsoft.Data.Sqlite;
using System;

namespace Villamos.Kezelők
{
    public class Kisegítő
    {
        public static void ParaméterekHozzáadása(SqliteCommand cmd, object adatObjektum)
        {
            if (adatObjektum == null) return;

            // Lekérjük az osztály összes publikus tulajdonságát
            var tulajdonságok = adatObjektum.GetType().GetProperties();

            foreach (var prop in tulajdonságok)
            {
                string név = "@" + prop.Name;
                object érték = prop.GetValue(adatObjektum) ?? DBNull.Value;

                // Dátumkezelés finomítása (ahogy korábban beszéltük)
                if (érték is DateTime dt)
                {
                    // Ha éjfél, csak dátum, különben idővel együtt
                    érték = dt.TimeOfDay.TotalSeconds == 0
                            ? dt.ToString("yyyy-MM-dd")
                            : dt.ToString("yyyy-MM-dd HH:mm:ss");
                }

                // Paraméter hozzáadása a parancshoz
                cmd.Parameters.AddWithValue(név, érték);
            }
        }


    }
}
