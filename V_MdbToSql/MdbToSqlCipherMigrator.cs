using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using MyA = Adatbázis;

namespace Villamos
{
    public class MdbToSqliteMigrator
    {
        static string connStrMdb = null;
        static string connStrSqLite = null;

        /// <summary>
        /// A kapott adatok alapján elvégzi a tábla Migrálását mdb - - >sqlite
        /// Feltételezzük, hogy az mdb fájlban lévő tábla létezik
        /// </summary>
        /// <param name="forras"></param>
        /// <param name="sqlite"></param>
        public static void EgyTáblaMigrálása(Sql_Működés MdbAdat, Sql_Működés SqLiteAdat)
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
                        MásolTábla(mdb, sqlite, MdbAdat, SqLiteAdat);

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
        private static void MásolTábla(OleDbConnection mdb, SqliteConnection sqlite, Sql_Működés Forrás, Sql_Működés Cél)
        {
            try
            {
                Sql_Kezelő_Áttöltés Kéz = new Sql_Kezelő_Áttöltés();
                using (var adapter = new OleDbDataAdapter($"SELECT * FROM [{Forrás.Tábla}]", mdb))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    var mdbSchema = GetDataTableSchema(dt);

                    bool letezik = MyA.TáblaVanSqLite(sqlite, Cél.Tábla);

                    if (letezik)
                    {
                        Dictionary<string, string> sqliteSchema = GetSqliteSchema(sqlite, Cél.Tábla);

                        if (SchemaEgyezik(sqliteSchema, mdbSchema))
                        {
                            // ---- CSAK HOZZÁFŰZÜNK ----
                            InsertData(sqlite, Cél.Tábla, dt);
                            Kéz.Rögzítés(Forrás);
                            MessageBox.Show("A tábla és az adatok másolása megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            throw new HibásBevittAdat($"A(z) '{Cél.Tábla}' SQLite tábla létezik, de az adatszerkezete nem egyezik az MDB '{Forrás.Tábla}' táblával. " +
                                   "A migrálás nem történt meg.");
                        }
                    }

                    // Ha idáig eljutunk, létre kell hozni a táblát
                    CreateSqliteTable(sqlite, Cél, dt);
                    InsertData(sqlite, Cél.Tábla, dt);

                    Kéz.Rögzítés(Forrás);
                    MessageBox.Show("A tábla és az adatok másolása megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    string name = rdr["name"].ToString().ToUpper();
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
                dict[col.ColumnName.ToString().ToUpper()] = ConvertType(col.DataType).ToString ().ToUpper();
            }
            return dict;
        }


        private static void CreateSqliteTable(SqliteConnection sqlite, Sql_Működés Cél, DataTable dt)
        {
            List<string> oszlopok = new List<string>();
            foreach (DataColumn col in dt.Columns)
            {
                string tipus = ConvertType(col.DataType);
                oszlopok.Add($"[{col.ColumnName}] {tipus}");
            }

            string sql = $"CREATE TABLE [{Cél.Tábla}] ({string.Join(",", oszlopok)});";
            using (var cmd = new SqliteCommand(sql, sqlite))
            {
                cmd.ExecuteNonQuery();
            }

            Sql_Kezelő_Működés Kéz = new Sql_Kezelő_Működés();
            Kéz.Döntés(Cél);
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
            if (dt.Rows.Count == 0) return;

            List<string> columns = new List<string>();
            List<string> values = new List<string>();
            foreach (DataColumn col in dt.Columns)
            {
                columns.Add($"[{col.ColumnName}]");
                values.Add($"@{col.ColumnName}");
            }

            string sql = $"INSERT INTO [{tablaNev}] ({string.Join(",", columns)}) VALUES ({string.Join(",", values)});";

            using (var tran = sqlite.BeginTransaction())
            {
                using (var cmd = new SqliteCommand(sql, sqlite, tran))
                {
                    // Paraméterek előkészítése
                    foreach (DataColumn col in dt.Columns)
                    {
                        var param = cmd.CreateParameter();
                        param.ParameterName = "@" + col.ColumnName;
                        cmd.Parameters.Add(param);
                    }

                    foreach (DataRow row in dt.Rows)
                    {
                        foreach (DataColumn col in dt.Columns)
                        {
                            object value = row[col];

                            // --- DÁTUM KEZELÉS FINOMÍTÁSA ---
                            if (value is DateTime dtValue)
                            {
                                // Ha az időrész pontosan éjfél (00:00:00), akkor csak dátumot mentünk
                                if (dtValue.TimeOfDay.TotalSeconds == 0)
                                {
                                    cmd.Parameters["@" + col.ColumnName].Value = dtValue.ToString("yyyy-MM-dd");
                                }
                                else
                                {
                                    // Ha van benne időinformáció, akkor másodperc pontossággal mentjük
                                    cmd.Parameters["@" + col.ColumnName].Value = dtValue.ToString("yyyy-MM-dd HH:mm:ss");
                                }
                            }
                            else
                            {
                                // Minden más típus marad az eredeti
                                cmd.Parameters["@" + col.ColumnName].Value = value ?? DBNull.Value;
                            }
                        }
                        cmd.ExecuteNonQuery();
                    }
                }
                tran.Commit();
            }
        }

    }
}