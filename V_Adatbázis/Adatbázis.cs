using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using Villamos;

internal static partial class Adatbázis
{
    #region Választó
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
    #endregion


    #region Mdb Megoldások
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

    public static DataTable Mdb_TáblaLekérése(string eleresiUt, string jelszo, string tablaNev)
    {
        DataTable dt = new DataTable();

        // Kapcsolati karakterlánc (.mdb fájl esetén Jet.OLEDB.4.0, .accdb esetén ACE.OLEDB.12.0 kell)
        string connectionString = $@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={eleresiUt};Jet OLEDB:Database Password={jelszo};";

        using (OleDbConnection conn = new OleDbConnection(connectionString))
        {
            try
            {
                conn.Open();
                // A tábla nevét szögletes zárójelbe tesszük a biztonság kedvéért (pl. szóközök miatt)
                string query = $"SELECT * FROM [{tablaNev}]";

                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    // Az adapter feltölti a DataTable-t az eredményekkel
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                // Továbbdobjuk a hibát, hogy a hívó oldalon (a Form-ban) naplózni lehessen
                throw new Exception($"Hiba az adatok lekérésekor a(z) {tablaNev} táblából.", ex);
            }
        }
        return dt;
    }
    #endregion


    #region SqLite Megoldások

    private static string BuildConnectionString(string hely, string jelszó)
    {
        return new SqliteConnectionStringBuilder
        {
            DataSource = hely,
            Mode = SqliteOpenMode.ReadWriteCreate,
            Password = jelszó,
            // Bekapcsolja a kapcsolatgyűjtőt, ami segít a zárolások hatékonyabb kezelésében
            Pooling = false,
            // Növeli a várakozási időt (másodpercben), ha az adatbázis épp foglalt
            DefaultTimeout = 60,
            Cache = SqliteCacheMode.Shared
        }.ToString();
    }

    // ÚJ SEGÉDMETÓDUS: Minden megnyitott kapcsolatnál beállítja a WAL módot és a várakozást
    private static void KapcsolatElokeszitese(SqliteConnection connection)
    {
        using (var walCmd = connection.CreateCommand())
        {
            // WAL mód bekapcsolása és a beépített várakozás (busy_timeout) 60 másodpercre növelése
            walCmd.CommandText = "PRAGMA busy_timeout=60000; PRAGMA journal_mode=WAL;";
            walCmd.ExecuteNonQuery();
        }
    }

    #region Módosítás
    private static void SqLite_Módosítás(string holvan, string ABjelszó, string SQLszöveg)
    {
        string kapcsolatiszöveg = BuildConnectionString(holvan, ABjelszó);
        try
        {
            using (var connection = new SqliteConnection(kapcsolatiszöveg))
            {
                connection.Open();
                KapcsolatElokeszitese(connection);

                using (var command = new SqliteCommand(SQLszöveg, connection))
                {
                    command.CommandTimeout = 30;
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, $"SqLite Adat módosítás:\n{holvan}\n{SQLszöveg}", ex.StackTrace, ex.Source, ex.HResult);
            throw new Exception("Adatbázis rögzítési hiba, az adatok rögzítése/módosítása nem történt meg.");
        }
    }

    public static void SqLite_Módosítás(string holvan, string ABjelszó, SqliteCommand cmd)
    {
        string kapcsolatiszöveg = BuildConnectionString(holvan, ABjelszó);
        try
        {
            using (var connection = new SqliteConnection(kapcsolatiszöveg))
            {
                connection.Open();
                KapcsolatElokeszitese(connection);

                cmd.Connection = connection;
                if (cmd.CommandTimeout < 30) cmd.CommandTimeout = 30;

                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, $"SqLite Adat módosítás:\n{holvan}", ex.StackTrace, ex.Source, ex.HResult);
            throw new Exception("Adatbázis rögzítési hiba, az adatok rögzítése/módosítása nem történt meg.");
        }
    }

    public static void SqLite_Módosítások(string holvan, string ABjelszó, List<SqliteCommand> parancsok)
    {
        string kapcsolatiszöveg = BuildConnectionString(holvan, ABjelszó);
        using (var connection = new SqliteConnection(kapcsolatiszöveg))
        {
            connection.Open();
            KapcsolatElokeszitese(connection);

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    foreach (var cmd in parancsok)
                    {
                        cmd.Connection = connection;
                        cmd.Transaction = transaction;
                        if (cmd.CommandTimeout < 30) cmd.CommandTimeout = 30;
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    HibaNapló.Log(ex.Message, $"SqLite Tranzakciós hiba:\n{holvan}", ex.StackTrace, ex.Source, ex.HResult);
                    throw new Exception("Hiba történt a csoportos művelet során. Semmi nem került rögzítésre.");
                }
            }
        }
    }

    private static void SqLite_Módosítás(string holvan, string ABjelszó, List<string> SQLszöveg)
    {
        string kapcsolatiszöveg = BuildConnectionString(holvan, ABjelszó);

        using (var connection = new SqliteConnection(kapcsolatiszöveg))
        {
            connection.Open();
            KapcsolatElokeszitese(connection);

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    foreach (var sql in SQLszöveg)
                    {
                        using (var command = new SqliteCommand(sql, connection, transaction))
                        {
                            command.CommandTimeout = 30;
                            command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    HibaNapló.Log(ex.Message, $"SQLite hiba: {holvan}", ex.StackTrace, ex.Source, ex.HResult);
                    throw new Exception("Adatbázis hiba, a módosítások visszavonva.", ex);
                }
            }
        }
    }
    #endregion

    #region Törlés
    public static void SqLite_ABtörlés(string holvan, string ABjelszó, string SQLszöveg)
    {
        try
        {
            string kapcsolatiszöveg = BuildConnectionString(holvan, ABjelszó);

            using (SqliteConnection connection = new SqliteConnection(kapcsolatiszöveg))
            {
                connection.Open();
                KapcsolatElokeszitese(connection);

                using (var command = new SqliteCommand(SQLszöveg, connection))
                {
                    command.CommandTimeout = 30;
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, $"SqLite Adat törlés:\n{holvan}\n{SQLszöveg}", ex.StackTrace, ex.Source, ex.HResult);
            throw new Exception("Adatbázis törlési hiba, az adatok törlése nem történt meg.");
        }
    }

    public static void SqLite_ABtörlés(string holvan, string ABjelszó, List<string> SQLszöveg)
    {
        string kapcsolatiszöveg = BuildConnectionString(holvan, ABjelszó);

        using (var connection = new SqliteConnection(kapcsolatiszöveg))
        {
            connection.Open();
            KapcsolatElokeszitese(connection);

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    foreach (string sql in SQLszöveg)
                    {
                        using (var command = new SqliteCommand(sql, connection, transaction))
                        {
                            command.CommandTimeout = 30;
                            command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    HibaNapló.Log(ex.Message, $"SqLite törlési hiba az adatbázisban: {holvan}", ex.StackTrace, ex.Source, ex.HResult);
                    throw new Exception("Adatbázis törlési hiba, a folyamat megállt és a változások visszavonva.", ex);
                }
            }
        }
    }
    #endregion

    #region Listázás
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
                KapcsolatElokeszitese(connection);

                using (SqliteCommand command = new SqliteCommand(sql, connection))
                {
                    command.CommandTimeout = 30;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            VálaszAdatok.Add(mapFüggvény(reader));
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, $"SqLite Adat lekérés:\n{hely}\n{táblanév}", ex.StackTrace, ex.Source, ex.HResult);
            throw;
        }
        return VálaszAdatok;
    }
    #endregion

    public static bool SqLite_ABvanTábla(string holvan, string ABjelszó, string táblanév)
    {
        bool válasz = false;
        try
        {
            string kapcsolatiszöveg = BuildConnectionString(holvan, ABjelszó);
            using (var connection = new SqliteConnection(kapcsolatiszöveg))
            {
                connection.Open();
                KapcsolatElokeszitese(connection);
                válasz = SqLite_TáblaVan(connection, táblanév);
            }
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, "SqLite ABvanTábla", ex.StackTrace, ex.Source, ex.HResult, "_", false);
        }
        return válasz;
    }

    public static bool SqLite_TáblaVan(SqliteConnection sqlite, string tablaNev)
    {
        using (var cmd = new SqliteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name=@nev;", sqlite))
        {
            cmd.CommandTimeout = 30;
            cmd.Parameters.AddWithValue("@nev", tablaNev);
            return cmd.ExecuteScalar() != null;
        }
    }

    public static List<string> SqLite_ABMezők(string holvan, string ABjelszó, string táblaNeve)
    {
        List<string> válasz = new List<string>();
        try
        {
            string kapcsolatiszöveg = BuildConnectionString(holvan, ABjelszó);

            using (var Kapcsolat = new SqliteConnection(kapcsolatiszöveg))
            {
                Kapcsolat.Open();
                KapcsolatElokeszitese(Kapcsolat);

                using (var cmd = new SqliteCommand($"PRAGMA table_info([{táblaNeve}])", Kapcsolat))
                {
                    cmd.CommandTimeout = 30;
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string mezoNev = reader["name"].ToString();
                            string tipusNev = reader["type"].ToString();

                            válasz.Add($"{mezoNev}-{tipusNev}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, "Mdb Mdb_ABMezők", ex.StackTrace, ex.Source, ex.HResult, "_", false);
        }
        return válasz;
    }

    public static void SqLite_TáblaLétrehozás(string hely, string jelszó, string sql)
    {
        try
        {
            string connectionString = BuildConnectionString(hely, jelszó);
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                KapcsolatElokeszitese(connection);

                using (var command = new SqliteCommand(sql, connection))
                {
                    command.CommandTimeout = 30;
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, $"SqLite_TáblaLétrehozás hiba: {sql}", ex.StackTrace, ex.Source, ex.HResult);
            throw new Exception("Adatbázis tábla létrehozási hiba.", ex);
        }
    }

    public static DataTable SqLite_TáblaLekérése(string hely, string jelszó, string tablaNev)
    {
        DataTable dt = new DataTable();
        string connectionString = BuildConnectionString(hely, jelszó);

        using (var conn = new SqliteConnection(connectionString))
        {
            try
            {
                conn.Open();
                KapcsolatElokeszitese(conn);

                string query = $"SELECT * FROM [{tablaNev}]";

                using (var cmd = new SqliteCommand(query, conn))
                {
                    cmd.CommandTimeout = 30;
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Hiba az adatok lekérésekor a(z) {tablaNev} táblából.", ex);
            }
        }
        return dt;
    }

    #endregion
}