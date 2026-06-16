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
        if (holvan.EndsWith(".mdb", StringComparison.OrdinalIgnoreCase))
            Mdb_Módosítás(holvan, ABjelszó, SQLszöveg);
        else
            SqLite_Módosítás(holvan, ABjelszó, SQLszöveg);
    }


    /// <summary>
    /// Adatbázisban módosít a küldött szöveg alapján (SQL)
    /// </summary>
    /// <param name="holvan"> A fájl elérhetőségének helye </param>
    /// <param name="ABjelszó"> Adatbázis jelszó </param>
    /// <param name="SQLszöveg">Lista SQl módosítási szöveg </param>
    public static void ABMódosítás(string holvan, string ABjelszó, List<string> SQLszöveg)
    {
        if (holvan.EndsWith(".mdb", StringComparison.OrdinalIgnoreCase))
            Mdb_Módosítás(holvan, ABjelszó, SQLszöveg);
        else
            SqLite_Módosítás(holvan, ABjelszó, SQLszöveg);
    }

    public static void ABtörlés(string holvan, string ABjelszó, string SQLszöveg)
    {
        if (holvan.EndsWith(".mdb", StringComparison.OrdinalIgnoreCase))
            Mdb_ABtörlés(holvan, ABjelszó, SQLszöveg);
        else
            SqLite_ABtörlés(holvan, ABjelszó, SQLszöveg);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="holvan">A fájl elérhetőségének helye </param>
    /// <param name="ABjelszó">Adatbázis jelszó</param>
    /// <param name="SQLszöveg">Lista SQl módosítási szöveg </param>
    public static void ABtörlés(string holvan, string ABjelszó, List<string> SQLszöveg)
    {
        if (holvan.EndsWith(".mdb", StringComparison.OrdinalIgnoreCase))
            Mdb_ABtörlés(holvan, ABjelszó, SQLszöveg);
        else
            SqLite_ABtörlés(holvan, ABjelszó, SQLszöveg);
    }
    #endregion

    #region Mdb Megoldások

    // Segédmetódus a kapcsolati sztringhez
    private static string GetOleDbConnectionString(string holvan, string jelszó)
    {
        return $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{holvan}';Jet OLEDB:Database Password={jelszó};OLE DB Services=-1;";
    }

    private static void Mdb_Módosítás(string holvan, string ABjelszó, string SQLszöveg)
    {
        try
        {
            Központi_Adatbázis.EnsureKeepAlive(holvan, ABjelszó);
            string kapcsolatiszöveg = GetOleDbConnectionString(holvan, ABjelszó);

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
            throw new Exception("Adatbázis rögzítési hiba, az adatok rögzítése/módosítása nem történt meg.");
        }
    }

    private static void Mdb_Módosítás(string holvan, string ABjelszó, List<string> SQLszöveg)
    {
        bool hiba = false;
        string utolsóSzöveg = "";
        try
        {
            Központi_Adatbázis.EnsureKeepAlive(holvan, ABjelszó);
            string kapcsolatiszöveg = GetOleDbConnectionString(holvan, ABjelszó);

            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                Kapcsolat.Open();
                using (OleDbTransaction tranzakció = Kapcsolat.BeginTransaction())
                {
                    try
                    {
                        foreach (string szöveg in SQLszöveg)
                        {
                            utolsóSzöveg = szöveg;
                            using (OleDbCommand Parancs = new OleDbCommand(szöveg, Kapcsolat, tranzakció))
                            {
                                Parancs.ExecuteNonQuery();
                            }
                        }
                        tranzakció.Commit();
                    }
                    catch (Exception ex)
                    {
                        tranzakció.Rollback();
                        HibaNapló.Log(ex.Message, $"Mdb Adat módosítás:\n{holvan}\n{utolsóSzöveg}", ex.StackTrace, ex.Source, ex.HResult);
                        hiba = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, $"Mdb Adat módosítás globális hiba:\n{holvan}", ex.StackTrace, ex.Source, ex.HResult);
            throw new Exception("Adatbázis rögzítési hiba, az adatok rögzítése/módosítása nem történt meg.");
        }
        if (hiba) throw new Exception("Mdb Adatbázis rögzítési hiba a tranzakció során.");
    }

    public static void Mdb_ABtörlés(string holvan, string ABjelszó, string SQLszöveg)
    {
        try
        {
            Központi_Adatbázis.EnsureKeepAlive(holvan, ABjelszó);
            string kapcsolatiszöveg = GetOleDbConnectionString(holvan, ABjelszó);

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
            HibaNapló.Log(ex.Message, $"Mdb Adat törlés:\n{holvan}\n{SQLszöveg}", ex.StackTrace, ex.Source, ex.HResult);
            throw new Exception("Adatbázis törlési hiba, az adatok törlése nem történt meg.");
        }
    }

    public static void Mdb_ABtörlés(string holvan, string ABjelszó, List<string> SQLszöveg)
    {
        // Analóg módon a Mdb_Módosítás listás verziójával
        Mdb_Módosítás(holvan, ABjelszó, SQLszöveg);
    }

    public static bool ABvanTábla(string holvan, string ABjelszó, string SQLszöveg)
    {
        if (holvan.EndsWith(".mdb", StringComparison.OrdinalIgnoreCase))
        {
            return Mdb_ABvanTábla(holvan, ABjelszó, SQLszöveg);
        }
        else
        {
            return SqLite_ABvanTábla(holvan, ABjelszó, SQLszöveg.Replace("SELECT * FROM", "").Replace(";", "").Trim());
        }
    }

    public static bool Mdb_ABvanTábla(string holvan, string ABjelszó, string SQLszöveg)
    {
        try
        {
            Központi_Adatbázis.EnsureKeepAlive(holvan, ABjelszó);
            string kapcsolatiszöveg = GetOleDbConnectionString(holvan, ABjelszó);

            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                using (OleDbCommand Parancs = new OleDbCommand(SQLszöveg, Kapcsolat))
                {
                    Kapcsolat.Open();
                    using (OleDbDataReader rekord = Parancs.ExecuteReader(CommandBehavior.SchemaOnly))
                    {
                        return true;
                    }
                }
            }
        }
        catch
        {
            return false;
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
            Központi_Adatbázis.EnsureKeepAlive(holvan, ABjelszó);
            string kapcsolatiszöveg = GetOleDbConnectionString(holvan, ABjelszó);

            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                Kapcsolat.Open();
                DataTable schemaTable = Kapcsolat.GetSchema("Tables");
                foreach (DataRow row in schemaTable.Rows)
                {
                    if (row["TABLE_TYPE"].ToString() == "TABLE")
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
            Központi_Adatbázis.EnsureKeepAlive(holvan, ABjelszó);
            string kapcsolatiszöveg = GetOleDbConnectionString(holvan, ABjelszó);

            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                Kapcsolat.Open();
                DataTable schemaTable = Kapcsolat.GetSchema("Columns", new string[] { null, null, táblaNeve, null });
                foreach (DataRow row in schemaTable.Rows)
                {
                    string mezoNev = row["COLUMN_NAME"].ToString();
                    int tipusKod = Convert.ToInt32(row["DATA_TYPE"]);
                    string tipusNev = ((OleDbType)tipusKod).ToString();
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
        string connectionString = GetOleDbConnectionString(eleresiUt, jelszo);

        using (OleDbConnection conn = new OleDbConnection(connectionString))
        {
            try
            {
                Központi_Adatbázis.EnsureKeepAlive(eleresiUt, jelszo);
                conn.Open();
                string query = $"SELECT * FROM [{tablaNev}]";

                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    adapter.Fill(dt);
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

    #region SqLite Megoldások

    private static string BuildConnectionString(string hely, string jelszó)
    {
        return new SqliteConnectionStringBuilder
        {
            DataSource = hely,
            Mode = SqliteOpenMode.ReadWriteCreate,
            Password = jelszó,
            Pooling = true,
            DefaultTimeout = 60,
            Cache = SqliteCacheMode.Shared
        }.ToString();
    }

    private static void KapcsolatElokeszitese(SqliteConnection connection)
    {
        using (var walCmd = connection.CreateCommand())
        {
            walCmd.CommandText = "PRAGMA busy_timeout=60000; PRAGMA journal_mode=WAL; PRAGMA synchronous=NORMAL;";
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
            HibaNapló.Log(ex.Message, $"SqLite Adat módosítás paraméterezve:\n{holvan}", ex.StackTrace, ex.Source, ex.HResult);
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
        SqLite_Módosítás(holvan, ABjelszó, SQLszöveg);
    }

    public static void SqLite_ABtörlés(string holvan, string ABjelszó, List<string> SQLszöveg)
    {
        SqLite_Módosítás(holvan, ABjelszó, SQLszöveg);
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
            HibaNapló.Log(ex.Message, "SqLite_ABMezők", ex.StackTrace, ex.Source, ex.HResult, "_", false);
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

    #region Központi Adatbázis (Keep-Alive Menedzser)
    /// <summary>
    /// Felelős az MDB (Access) fájlok .ldb zárolásának életben tartásáért.
    /// Ezzel elkerülhető a folyamatos fájlmegnyitási és zárolási overhead a hálózaton.
    /// </summary>
    public static class Központi_Adatbázis
    {
        // Szótár a nyitott kapcsolatok tárolására (fájlútvonal -> kapcsolat)
        private static readonly Dictionary<string, OleDbConnection> _keepAliveConnections = new Dictionary<string, OleDbConnection>(StringComparer.OrdinalIgnoreCase);
        private static readonly object _lockObj = new object();

        /// <summary>
        /// Biztosítja, hogy az adott adatbázishoz létezzen egy állandó, nyitott kapcsolat a memóriában.
        /// </summary>
        public static void EnsureKeepAlive(string hely, string jelszó)
        {
            lock (_lockObj)
            {
                if (!_keepAliveConnections.ContainsKey(hely) || _keepAliveConnections[hely].State != ConnectionState.Open)
                {
                    string cs = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}';Jet OLEDB:Database Password={jelszó};";
                    OleDbConnection conn = new OleDbConnection(cs);
                    try
                    {
                        conn.Open();
                        _keepAliveConnections[hely] = conn;
                    }
                    catch (Exception ex)
                    {
                        HibaNapló.Log(ex.Message, $"Keep-Alive nyitási hiba: {hely}", ex.StackTrace, ex.Source, ex.HResult);
                    }
                }
            }
        }

        /// <summary>
        /// Az alkalmazás bezárásakor hívandó (pl. FormClosed eseményben), hogy elengedje a hálózati zárolásokat.
        /// </summary>
        public static void MindenKapcsolatotZár()
        {
            lock (_lockObj)
            {
                foreach (var conn in _keepAliveConnections.Values)
                {
                    if (conn != null && conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
                _keepAliveConnections.Clear();
            }
        }
    }
    #endregion
}