using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_SQLite
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\SQLite\Test.db";
        readonly string Password = "CzabalayL";
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

        // Read

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

        // Update

        // Delete

    }
}

