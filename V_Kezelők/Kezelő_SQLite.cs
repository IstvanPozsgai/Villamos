using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Villamos.V_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_SQLite
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\SQLite\Test.db";
        readonly string Password = "VivaTV";
        readonly string TableName = "TestTable";
        string ConnectionString;

        public Kezelő_SQLite()
        {
            EnsureDirectory();
            ConnectionString = BuildConnectionString();
        }

        private void EnsureDirectory()
        {
            var dir = Path.GetDirectoryName(hely);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        string BuildConnectionString()
        {
            return new SqliteConnectionStringBuilder
            {
                DataSource = hely,
                Mode = SqliteOpenMode.ReadWriteCreate,
                Password = Password
            }.ToString();
        }

        // Create
        public void CreateTable()
        {
            var sql = $@"CREATE TABLE {TableName}(
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        username TEXT NOT NULL,
                        date INTEGER NOT NULL,
                        trueorfalse INTEGER NOT NULL
                        )";
            try
            {
                SqliteConnection connection = new SqliteConnection(ConnectionString);
                connection.Open();

                var command = new SqliteCommand(sql, connection);
                command.ExecuteNonQuery();

                connection.Close();

            }
            catch (SqliteException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void InsertData(string username, int date, int trueorfalse)
        {
            var sql = $@"INSERT INTO {TableName} (username, date, trueorfalse)
                         VALUES ('{username}', {date}, {trueorfalse})";
            try
            {
                SqliteConnection connection = new SqliteConnection(ConnectionString);
                connection.Open();

                var command = new SqliteCommand(sql, connection);
                command.ExecuteNonQuery();

                connection.Close();

            }
            catch (SqliteException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        // Read
        public List<Adat_SQLite> ReadAllData()
        {
            List<Adat_SQLite> TestList = new List<Adat_SQLite>();
            var sql = $@"SELECT * FROM {TableName}";
            try
            {
                SqliteConnection connection = new SqliteConnection(ConnectionString);
                connection.Open();

                var command = new SqliteCommand(sql, connection);
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string Username = reader.GetString(1);
                        int Date = reader.GetInt32(2);
                        int TrueOrFalse = reader.GetInt32(3);

                        TestList.Add(new Adat_SQLite(id, Username, DateTimeOffset.FromUnixTimeSeconds(Date).DateTime, TrueOrFalse == 1));
                    }
                    return TestList;
                }
                else
                {
                    Console.WriteLine("No authors found.");
                }

                connection.Close();
                return TestList;

            }
            catch (SqliteException ex)
            {
                Console.WriteLine(ex.Message);
                return TestList;
            }
        }
        // Update
        public void UpdateData(Adat_SQLite Data, int ID)
        {
            int trueOrFalse = Data.TrueOrFalse ? 0 : 1;
            var sql = $@"UPDATE {TableName} SET trueorfalse = {trueOrFalse}, date = {DateTimeOffset.Now.ToUnixTimeSeconds()} WHERE id = {ID}";
            try
            {
                SqliteConnection connection = new SqliteConnection(ConnectionString);
                connection.Open();

                var command = new SqliteCommand(sql, connection);
                command.ExecuteNonQuery();

                connection.Close();
            }
            catch (SqliteException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        // Delete
        public void DeleteData(int ID)
        {
            var sql = $@"DELETE FROM {TableName} WHERE id ={ID}";
            try
            {
                SqliteConnection connection = new SqliteConnection(ConnectionString);
                connection.Open();

                var command = new SqliteCommand(sql, connection);
                command.ExecuteNonQuery();

                connection.Close();

            }
            catch (SqliteException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

}


