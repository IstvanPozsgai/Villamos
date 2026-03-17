using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
namespace Villamos
{
    public class MdbToSqliteMigrator
    {
        public class MdbForras
        {
            public string Fajl { get; set; }
            public string Jelszo { get; set; }
        }

        public static void Migracio(List<MdbForras> forrasok, string celSqliteFajl, string celJelszo)
        {
            if (File.Exists(celSqliteFajl)) File.Delete(celSqliteFajl);

            string connStr = $"Data Source={celSqliteFajl};Password={celJelszo};";
            using (var sqlite = new SqliteConnection(connStr))
            {
                sqlite.Open();

                using (var pragmaCmd = new SqliteCommand("PRAGMA synchronous = OFF; PRAGMA journal_mode = WAL; PRAGMA temp_store = MEMORY;", sqlite))
                {
                    pragmaCmd.ExecuteNonQuery();
                }

                foreach (var forras in forrasok)
                {
                    FeldolgozMdb(forras, sqlite);
                }

                sqlite.Close();
            }
        }

        private static void FeldolgozMdb(MdbForras forras, SqliteConnection sqlite)
        {
            string connStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{forras.Fajl}';Jet OLEDB:Database Password={forras.Jelszo};";
            using (var mdb = new OleDbConnection(connStr))
            {
                mdb.Open();
                DataTable schema = mdb.GetSchema("Tables");

                foreach (DataRow row in schema.Rows)
                {
                    if (row["TABLE_TYPE"].ToString() != "TABLE") continue;
                    string tableName = row["TABLE_NAME"].ToString();
                    string ujTablaNev = tableName;
                    int i = 2;
                    while (TablaLetezik(sqlite, ujTablaNev))
                    {
                        ujTablaNev = tableName + "_" + i;
                        i++;
                    }
                    MasolTabla(mdb, sqlite, tableName, ujTablaNev);
                }

                mdb.Close();
            }
        }

        private static bool TablaLetezik(SqliteConnection sqlite, string tablaNev)
        {
            using (var cmd = new SqliteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name=@nev;", sqlite))
            {
                cmd.Parameters.AddWithValue("@nev", tablaNev);
                return cmd.ExecuteScalar() != null;
            }
        }

        private static void MasolTabla(OleDbConnection mdb, SqliteConnection sqlite, string forrasTabla, string celTabla)
        {
            using (var adapter = new OleDbDataAdapter($"SELECT * FROM [{forrasTabla}]", mdb))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                CreateSqliteTable(sqlite, celTabla, dt);
                InsertData(sqlite, celTabla, dt);
            }
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