using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos
{
    public class MdbToSqliteMigrator
    {
        static string connStrMdb = null;
        static string connStrSqLite = null;


        public static void Migracio(List<S_Működés> forrasok, string celSqliteFajl, string celJelszo)
        {


            connStrSqLite = $"Data Source={celSqliteFajl};Password={celJelszo};";
            using (SqliteConnection sqlite = new SqliteConnection(connStrSqLite))
            {
                sqlite.Open();

                using (var pragmaCmd = new SqliteCommand("PRAGMA synchronous = OFF; PRAGMA journal_mode = WAL; PRAGMA temp_store = MEMORY;", sqlite))
                {
                    pragmaCmd.ExecuteNonQuery();
                }

                foreach (S_Működés forras in forrasok)
                {
                    //  EgyTáblaMigrálása(forras, sqlite);
                }

                sqlite.Close();
            }
        }

        /// <summary>
        /// A kapott adatok alapján elvégzi a tábla Migrálását mdb - - >sqlite
        /// Feltételezzük, hogy az mdb fájlban lévő tábla létezik
        /// </summary>
        /// <param name="forras"></param>
        /// <param name="sqlite"></param>
        public static void EgyTáblaMigrálása(S_Működés MdbAdat, S_Működés SqLiteAdat)
        {
            try
            {
                connStrMdb = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{MdbAdat.Fájl}';Jet OLEDB:Database Password={MdbAdat.Jelszó};";
                connStrSqLite = $"Data Source={SqLiteAdat.Fájl};Password={SqLiteAdat.Jelszó};";
                using (OleDbConnection mdb = new OleDbConnection(connStrMdb))
                {
                    //megnyitjuk az Mdb fájlt 
                    mdb.Open();

                    using (SqliteConnection sqlite = new SqliteConnection(connStrSqLite))
                    {
                        sqlite.Open();
                        MásolTábla(mdb, sqlite, MdbAdat.Tábla, SqLiteAdat.Tábla);

                        sqlite.Close();
                    }
                    mdb.Close();
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "EgyTáblaMigrálása", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private static void FeldolgozMdb(S_Működés forras, SqliteConnection sqlite)
        {
            connStrMdb = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{forras.Fájl}';Jet OLEDB:Database Password={forras.Jelszó};";
            using (OleDbConnection mdb = new OleDbConnection(connStrMdb))
            {
                //megnyitjuk az Mdb fájlt kiolvassuk a táblaneveket
                mdb.Open();
                DataTable schema = mdb.GetSchema("Tables");

                foreach (DataRow row in schema.Rows)
                {
                    if (row["TABLE_TYPE"].ToString() != "TABLE") continue;
                    string tableName = row["TABLE_NAME"].ToString();
                    string ujTablaNev = tableName;
                    int i = 2;
                    while (MyA.TáblaVanSqLite(sqlite, ujTablaNev))
                    {
                        ujTablaNev = tableName + "_" + i;
                        i++;
                    }
                    MásolTábla(mdb, sqlite, tableName, ujTablaNev);
                }

                mdb.Close();
            }
        }



        private static void MasolTabla_(OleDbConnection mdb, SqliteConnection sqlite, string forrasTabla, string celTabla)
        {
            using (var adapter = new OleDbDataAdapter($"SELECT * FROM [{forrasTabla}]", mdb))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                CreateSqliteTable(sqlite, celTabla, dt);
                InsertData(sqlite, celTabla, dt);
            }
        }


        /// <summary>
        ///  Mi történik most?
        ///    Szituáció → Eredmény
        ///    SQLite tábla nem létezikLétrehozza → adatokat bemásolja
        ///    Létezik, és szerkezet megegyezik❗ →  Nem hoz létre új táblát – csak hozzáfűzi az adatokat
        ///    Létezik, de szerkezet eltér → Új táblát hoz létre(Tábla_2, Tábla_3, …)
        /// </summary>
        /// <param name="mdb"></param>
        /// <param name="sqlite"></param>
        /// <param name="forrasTabla"></param>
        /// <param name="celTabla"></param>
        private static void MásolTábla(OleDbConnection mdb, SqliteConnection sqlite, string forrasTabla, string celTabla)
        {
            try
            {
                using (var adapter = new OleDbDataAdapter($"SELECT * FROM [{forrasTabla}]", mdb))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    var mdbSchema = GetDataTableSchema(dt);

                    bool letezik = MyA.TáblaVanSqLite(sqlite, celTabla);

                    if (letezik)
                    {
                        Dictionary<string, string> sqliteSchema = GetSqliteSchema(sqlite, celTabla);

                        if (SchemaEgyezik(sqliteSchema, mdbSchema))
                        {
                            // ---- CSAK HOZZÁFŰZÜNK ----
                            InsertData(sqlite, celTabla, dt);
                            return;
                        }
                        else
                        {
                            throw new HibásBevittAdat($"A(z) '{celTabla}' SQLite tábla létezik, de az adatszerkezete nem egyezik az MDB '{forrasTabla}' táblával. " +
                                   "A migrálás nem történt meg.");
                        }
                    }

                    // Ha idáig eljutunk, létre kell hozni a táblát
                    CreateSqliteTable(sqlite, celTabla, dt);
                    InsertData(sqlite, celTabla, dt);
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "MásolTábla", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Struktúra-összehasonlító függvény
        /// </summary>
        /// <param name="sqlite"></param>
        /// <param name="mdb"></param>
        /// <returns></returns>
        private static bool SchemaEgyezik(Dictionary<string, string> sqlite, Dictionary<string, string> mdb)
        {
            if (sqlite.Count != mdb.Count) return false;

            foreach (var kv in mdb)
            {
                if (!sqlite.ContainsKey(kv.Key)) return false;

                string t1 = sqlite[kv.Key];
                string t2 = kv.Value;

                // SQLite típusok lazák, ezért csak fő kategóriát vizsgálunk
                if (!t1.StartsWith(t2, StringComparison.OrdinalIgnoreCase)) return false;
            }
            return true;
        }

        private static Dictionary<string, string> GetSqliteSchema(SqliteConnection sqlite, string tableName)
        {
            var result = new Dictionary<string, string>();
            using (var cmd = new SqliteCommand($"PRAGMA table_info([{tableName}]);", sqlite))
            using (var rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    string name = rdr["name"].ToString();
                    string type = rdr["type"].ToString().ToUpper();
                    result[name] = type;
                }
            }
            return result;
        }

        private static Dictionary<string, string> GetDataTableSchema(DataTable dt)
        {
            var dict = new Dictionary<string, string>();
            foreach (DataColumn col in dt.Columns)
            {
                dict[col.ColumnName] = ConvertType(col.DataType);
            }
            return dict;
        }


        private static void CreateSqliteTable(SqliteConnection sqlite, string tablaNev, DataTable dt)
        {
            List<string> oszlopok = new List<string>();
            foreach (DataColumn col in dt.Columns)
            {
                string tipus = ConvertType(col.DataType);
                oszlopok.Add($"[{col.ColumnName}] {tipus}");
            }

            string sql = $"CREATE TABLE [{tablaNev}] ({string.Join(",", oszlopok)});";
            using (var cmd = new SqliteCommand(sql, sqlite))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private static string ConvertType(Type type)
        {
            if (type == typeof(string)) return "TEXT";
            if (type == typeof(int) || type == typeof(long) || type == typeof(bool)) return "INTEGER";
            if (type == typeof(double) || type == typeof(decimal)) return "REAL";
            if (type == typeof(DateTime)) return "TEXT";
            if (type == typeof(byte[])) return "BLOB";
            return "TEXT";
        }

        private static void InsertData(SqliteConnection sqlite, string tablaNev, DataTable dt)
        {
            if (dt.Rows.Count == 0) return; // Ha üres a tábla, nincs mit beszúrni

            // Oszlopok és SQL parancs előkészítése a cikluson kívül
            List<string> columns = new List<string>();
            List<string> values = new List<string>();
            foreach (DataColumn col in dt.Columns)
            {
                columns.Add($"[{col.ColumnName}]");
                values.Add($"@{col.ColumnName}");
            }

            string sql = $"INSERT INTO [{tablaNev}] ({string.Join(",", columns)}) VALUES ({string.Join(",", values)});";

            // Tranzakció megnyitása
            using (var tran = sqlite.BeginTransaction())
            {
                using (var cmd = new SqliteCommand(sql, sqlite, tran))
                {
                    // Paraméterek struktúrájának létrehozása (értékadás nélkül)
                    foreach (DataColumn col in dt.Columns)
                    {
                        var param = cmd.CreateParameter();
                        param.ParameterName = "@" + col.ColumnName;
                        cmd.Parameters.Add(param);
                    }

                    // Adatsorok bejárása: csak a paraméterek értékeit frissítjük
                    foreach (DataRow row in dt.Rows)
                    {
                        foreach (DataColumn col in dt.Columns)
                        {
                            cmd.Parameters["@" + col.ColumnName].Value = row[col] ?? DBNull.Value;
                        }
                        cmd.ExecuteNonQuery();
                    }
                }
                // Tranzakció véglegesítése
                tran.Commit();
            }
        }
    }
}