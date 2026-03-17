using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using Villamos;

internal static partial class Adatbázis
{

    /// <summary>
    /// Adatbázisban módosít a küldött szöveg alapján (SQL)
    /// </summary>
    /// <param name="holvan"> A fájl elérhetőségének helye </param>
    /// <param name="ABjelszó"> Adatbázis jelszó </param>
    /// <param name="SQLszöveg"> SQl módosítási szöveg </param>
    public static void ABMódosítás(string holvan, string ABjelszó, string SQLszöveg)
    {
        if (holvan.Contains(".mdb"))
        {
            Mdb_Módosítás(holvan, ABjelszó, SQLszöveg);
        }
        else
        {
            SqLite_Módosítás(holvan, ABjelszó, SQLszöveg);
        }
    }

    private static void Mdb_Módosítás(string holvan, string ABjelszó, string SQLszöveg)
    {
        try
        {
            string kapcsolatiszöveg = $"Provider=Microsoft.Jet.OleDb.4.0;Data Source= '{holvan}'; Jet Oledb:Database Password={ABjelszó}";
            // módosítjuk az adatokat
            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                using (OleDbCommand Parancs = new OleDbCommand(SQLszöveg, Kapcsolat))
                {
                    Kapcsolat.Open();
                    Parancs.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, $"MDB Adat módosítás:\n{holvan}\n{SQLszöveg}", ex.StackTrace, ex.Source, ex.HResult);
            throw new Exception("Adatbázis rögzítési hiba, az adotok rögzítése/módosítása nem történt meg.");
        }
    }

    private static void SqLite_Módosítás(string holvan, string ABjelszó, string SQLszöveg)
    {
        string kapcsolatiszöveg = BuildConnectionString(holvan, ABjelszó);
        try
        {
            using (var connection = new SqliteConnection(kapcsolatiszöveg))
            {
                connection.Open();
                var command = new SqliteCommand(SQLszöveg, connection);
                command.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, $"SqLite Adat módosítás:\n{holvan}\n{SQLszöveg}", ex.StackTrace, ex.Source, ex.HResult);
            throw new Exception("Adatbázis rögzítési hiba, az adotok rögzítése/módosítása nem történt meg.");
        }
    }



    /// <summary>
    /// Adatbázisban módosít a küldött szöveg alapján (SQL)
    /// </summary>
    /// <param name="holvan"> A fájl elérhetőségének helye </param>
    /// <param name="ABjelszó"> Adatbázis jelszó </param>
    /// <param name="SQLszöveg">Lista SQl módosítási szöveg </param>
    public static void ABMódosítás(string holvan, string ABjelszó, List<string> SQLszöveg)
    {
        if (holvan.Contains(".mdb"))
        {
            Mdb_Módosítás(holvan, ABjelszó, SQLszöveg);
        }
        else
        {
            SqLite_Módosítás(holvan, ABjelszó, SQLszöveg);
        }
    }

    private static void Mdb_Módosítás(string holvan, string ABjelszó, List<string> SQLszöveg)
    {
        bool hiba = false;
        string szöveg = "";
        try
        {
            // módosítjuk az adatokat
            string kapcsolatiszöveg = $"Provider=Microsoft.Jet.OleDb.4.0;Data Source= '{holvan}'; Jet Oledb:Database Password={ABjelszó}";

            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                Kapcsolat.Open();
                for (int i = 0; i < SQLszöveg.Count; i++)
                {
                    try
                    {
                        szöveg = SQLszöveg[i];
                        using (OleDbCommand Parancs = new OleDbCommand(SQLszöveg[i], Kapcsolat))
                        {
                            Parancs.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        HibaNapló.Log(ex.Message, $"Mdb Adat módosítás:\n{holvan}\n{szöveg}", ex.StackTrace, ex.Source, ex.HResult);
                        hiba = true;
                        continue;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, $"Mdb Adat módosítás:\n{holvan}\n{szöveg}", ex.StackTrace, ex.Source, ex.HResult);
            throw new Exception("Adatbázis rögzítési hiba, az adotok rögzítése/módosítása nem történt meg.");
        }
        if (hiba) throw new Exception(" Mdb Adatbázis rögzítési hiba, az adotok rögzítése/módosítása nem történt meg.");
    }

    private static void SqLite_Módosítás(string holvan, string ABjelszó, List<string> SQLszöveg)
    {
        string kapcsolatiszöveg = BuildConnectionString(holvan, ABjelszó);

        using (var connection = new SqliteConnection(kapcsolatiszöveg))
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction()) // Tranzakció indítása
            {
                try
                {
                    foreach (var sql in SQLszöveg)
                    {
                        using (var command = new SqliteCommand(sql, connection, transaction))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit(); // Csak akkor ment, ha minden sikerült
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Hiba esetén mindent visszavon
                    HibaNapló.Log(ex.Message, $"SQLite hiba: {holvan}", ex.StackTrace, ex.Source, ex.HResult);
                    throw new Exception("Adatbázis hiba, a módosítások visszavonva.", ex);
                }
            }
        }
    }




    /// <summary>
    /// 
    /// </summary>
    /// <param name="holvan">A fájl elérhetőségének helye </param>
    /// <param name="ABjelszó">Adatbázis jelszó</param>
    /// <param name="SQLszöveg">SQl módosítási szöveg </param>
    public static void ABtörlés(string holvan, string ABjelszó, string SQLszöveg)
    {
        if (holvan.Contains(".mdb"))
        {
            Mdb_ABtörlés(holvan, ABjelszó, SQLszöveg);
        }
        else
        {
            SqLite_ABtörlés(holvan, ABjelszó, SQLszöveg);
        }
    }

    public static void Mdb_ABtörlés(string holvan, string ABjelszó, string SQLszöveg)
    {
        try
        {
            // módosítjuk az adatokat
            string kapcsolatiszöveg = $"Provider=Microsoft.Jet.OleDb.4.0;Data Source= '{holvan}'; Jet Oledb:Database Password={ABjelszó}";
            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                using (OleDbCommand Parancs = new OleDbCommand(SQLszöveg, Kapcsolat))
                {
                    Kapcsolat.Open();
                    Parancs.ExecuteScalar();
                }
            }
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, $"Mdb Adat törlés:\n{holvan}\n{SQLszöveg}", ex.StackTrace, ex.Source, ex.HResult);
            throw new Exception("Adatbázis törlési hiba, az adotok törlése nem történt meg.");
        }
    }

    public static void SqLite_ABtörlés(string holvan, string ABjelszó, string SQLszöveg)
    {
        try
        {
            string kapcsolatiszöveg = BuildConnectionString(holvan, ABjelszó);

            using (SqliteConnection connection = new SqliteConnection(kapcsolatiszöveg))
            {
                connection.Open();

                var command = new SqliteCommand(SQLszöveg, connection);
                command.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, $"SqLite Adat törlés:\n{holvan}\n{SQLszöveg}", ex.StackTrace, ex.Source, ex.HResult);
            throw new Exception("Adatbázis törlési hiba, az adotok törlése nem történt meg.");
        }
    }




    /// <summary>
    /// 
    /// </summary>
    /// <param name="holvan">A fájl elérhetőségének helye </param>
    /// <param name="ABjelszó">Adatbázis jelszó</param>
    /// <param name="SQLszöveg">Lista SQl módosítási szöveg </param>
    public static void ABtörlés(string holvan, string ABjelszó, List<string> SQLszöveg)
    {
        if (holvan.Contains(".mdb"))
        {
            Mdb_ABtörlés(holvan, ABjelszó, SQLszöveg);
        }
        else
        {
            SqLite_ABtörlés(holvan, ABjelszó, SQLszöveg);
        }
    }

    public static void Mdb_ABtörlés(string holvan, string ABjelszó, List<string> SQLszöveg)
    {
        try
        {
            // módosítjuk az adatokat
            string kapcsolatiszöveg = $"Provider=Microsoft.Jet.OleDb.4.0;Data Source= '{holvan}'; Jet Oledb:Database Password={ABjelszó}";

            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                Kapcsolat.Open();
                for (int i = 0; i < SQLszöveg.Count; i++)
                {
                    try
                    {
                        using (OleDbCommand Parancs = new OleDbCommand(SQLszöveg[i], Kapcsolat))
                        {
                            Parancs.ExecuteScalar();
                        }
                    }
                    catch (Exception ex)
                    {
                        HibaNapló.Log(ex.Message, $"Adat módosítás:\n{holvan}\n{SQLszöveg}", ex.StackTrace, ex.Source, ex.HResult);
                        continue;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, $"Mdb Adat törlés:\n{holvan}\n{SQLszöveg}", ex.StackTrace, ex.Source, ex.HResult);
            throw new Exception("Adatbázis törlési hiba, az adotok törlése nem történt meg.");
        }
    }

    public static void SqLite_ABtörlés(string holvan, string ABjelszó, List<string> SQLszöveg)
    {
        string kapcsolatiszöveg = BuildConnectionString(holvan, ABjelszó);

        using (var connection = new SqliteConnection(kapcsolatiszöveg))
        {
            connection.Open();
            // Tranzakció indítása: vagy az összes törlés sikerül, vagy egyik sem marad meg
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    foreach (string sql in SQLszöveg)
                    {
                        using (var command = new SqliteCommand(sql, connection, transaction))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit(); // Ha idáig eljutott, minden törlés véglegesítve
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Hiba esetén MINDENT visszacsinál
                    HibaNapló.Log(ex.Message, $"SqLite törlési hiba az adatbázisban: {holvan}", ex.StackTrace, ex.Source, ex.HResult);
                    throw new Exception("Adatbázis törlési hiba, a folyamat megállt és a változások visszavonva.", ex);
                }
            }
        }
    }




    public static bool ABvanTábla(string holvan, string ABjelszó, string SQLszöveg)
    {
        bool válasz;
        if (holvan.Contains(".mdb"))
        {
            válasz = Mdb_ABvanTábla(holvan, ABjelszó, SQLszöveg);
        }
        else
        {
            // Csak a táblanév kell, ezért levágjuk a "SELECT * FROM" részt, és megmaradó szöveget trim-eljük
            válasz = SqLite_ABvanTábla(holvan, ABjelszó, SQLszöveg.Replace("SELECT * FROM", "").Trim());
        }
        return válasz;
    }

    public static bool Mdb_ABvanTábla(string holvan, string ABjelszó, string SQLszöveg)
    {
        bool válasz = false;
        try
        {
            string kapcsolatiszöveg = "Provider=Microsoft.Jet.OleDb.4.0;Data Source= '" + holvan + "'; Jet Oledb:Database Password=" + ABjelszó;

            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                using (OleDbCommand Parancs = new OleDbCommand(SQLszöveg, Kapcsolat))
                {
                    Kapcsolat.Open();
                    using (OleDbDataReader rekord = Parancs.ExecuteReader())
                    {
                        válasz = true;
                    }
                }
            }
            return válasz;
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, "Mdb ABvanTábla", ex.StackTrace, ex.Source, ex.HResult, "_", false);
            return válasz;
        }
    }



    public static bool SqLite_ABvanTábla(string holvan, string ABjelszó, string táblanév)
    {
        bool válasz = false;
        try
        {
            string kapcsolatiszöveg = BuildConnectionString(holvan, ABjelszó);
            using (var connection = new SqliteConnection(kapcsolatiszöveg))
            {
                connection.Open();

                string sql = $@"SELECT COUNT(*) 
                   FROM sqlite_master
                   WHERE type='table' AND name=@name;";

                using (var cmd = new SqliteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@name", táblanév);

                    long count = cmd.ExecuteScalar().ToÉrt_Long();
                    if (count > 0) válasz = true;
                }
            }
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, "SqLite ABvanTábla", ex.StackTrace, ex.Source, ex.HResult, "_", false);
        }
        return válasz;
    }

    /// <summary>
    /// Adattáblanevek listája egy adott adatbázisban
    /// </summary>
    /// <param name="holvan"></param>
    /// <param name="ABjelszó"></param>
    /// <returns></returns>
    public static List<string> Mdb_ABTáblák(string holvan, string ABjelszó)
    {
        List<string> válasz = new List<string>();
        try
        {
            string kapcsolatiszöveg = $"Provider=Microsoft.Jet.OleDb.4.0;Data Source='{holvan}'; Jet Oledb:Database Password={ABjelszó}";

            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                Kapcsolat.Open();
                // A GetSchema("Tables") lekéri az összes tábla metaadatát
                DataTable schemaTable = Kapcsolat.GetSchema("Tables");
                foreach (DataRow row in schemaTable.Rows)
                {
                    string tipus = row["TABLE_TYPE"].ToString();

                    // Csak a tényleges felhasználói táblákat adjuk hozzá (kiszűrjük a rendszertáblákat)
                    if (tipus == "TABLE")
                    {
                        válasz.Add(row["TABLE_NAME"].ToString());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, "Mdb Mdb_ABTáblák", ex.StackTrace, ex.Source, ex.HResult, "_", false);
        }
        return válasz;
    }


    /// <summary>
    /// Adott Adattábla mezőinek listája egy adott adatbázisban
    /// </summary>
    /// <param name="hely"></param>
    /// <param name="jelszó"></param>
    /// <returns></returns>
    public static List<string> Mdb_ABMezők(string holvan, string ABjelszó, string táblaNeve)
    {
        List<string> válasz = new List<string>();
        try
        {
            string kapcsolatiszöveg = $"Provider=Microsoft.Jet.OleDb.4.0;Data Source='{holvan}';Jet Oledb:Database Password={ABjelszó}";

            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                Kapcsolat.Open();

                // A GetSchema("Columns") szűrői: [Adatbázis, Séma, Táblanév, Oszlopnév]
                // Mi csak a táblanévre szűrünk (a 3. paraméter)
                DataTable schemaTable = Kapcsolat.GetSchema("Columns", new string[] { null, null, táblaNeve, null });

                foreach (DataRow row in schemaTable.Rows)
                {
                    string mezoNev = row["COLUMN_NAME"].ToString();
                    int tipusKod = Convert.ToInt32(row["DATA_TYPE"]);
                    // A számkód átalakítása olvasható OleDbType névvé
                    string tipusNev = ((OleDbType)tipusKod).ToString();

                    // Példa: "ID (Integer)" vagy "Nev (VarWChar)"
                    válasz.Add($"{mezoNev}-{tipusNev}");
                }
            }
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, "Mdb Mdb_ABMezők", ex.StackTrace, ex.Source, ex.HResult, "_", false);
        }
        return válasz;
    }

    private static string BuildConnectionString(string hely, string jelszó)
    {
        return new SqliteConnectionStringBuilder
        {
            DataSource = hely,
            Mode = SqliteOpenMode.ReadWriteCreate,
            Password = jelszó
        }.ToString();
    }


    public static void SqLite_TáblaLétrehozás(string hely, string jelszó, string sql)
    {
        try
        {
            string connectionString = BuildConnectionString(hely, jelszó);
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
            } // Itt a kapcsolat automatikusan lezárul, akkor is, ha hiba történt.
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, $"SqLite_TáblaLétrehozás hiba: {sql}", ex.StackTrace, ex.Source, ex.HResult);
            throw new Exception("Adatbázis tábla létrehozási hiba.", ex);
        }
    }


    public static List<T> Lista_Adatok<T>(string hely, string jelszó, string táblanév, Func<SqliteDataReader, T> mapFüggvény)
    {
        List<T> VálaszAdatok = new List<T>();
        try
        {
            string sql = $@"SELECT * FROM {táblanév}";
            string kapcsolatiszöveg = BuildConnectionString(hely, jelszó);

            using (SqliteConnection connection = new SqliteConnection(kapcsolatiszöveg))
            {
                connection.Open();
                using (SqliteCommand command = new SqliteCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Itt hívjuk meg a kívülről átadott leképezést
                            VálaszAdatok.Add(mapFüggvény(reader));
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, $"Mdb Adat módosítás:\n{hely}\n{táblanév}", ex.StackTrace, ex.Source, ex.HResult);
            throw new Exception("Adatbázis rögzítési hiba, az adotok rögzítése/módosítása nem történt meg.");
        }
        return VálaszAdatok;
    }

}