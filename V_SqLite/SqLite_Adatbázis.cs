using Microsoft.Data.Sqlite;
using System.IO;
using System.Windows.Forms;

namespace Villamos
{
    public class SqLite_Adatbázis
    {
        string Hely { get; set; }
        string Jelszó { get; set; }

        string ConnectionString;



        public SqLite_Adatbázis()
        {
            Könyvtár();
            ConnectionString = BuildConnectionString();
        }

        private void Könyvtár()
        {
            string dir = Path.GetDirectoryName(Hely);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        //private string BuildConnectionString()
        //{
        //    return new SqliteConnectionStringBuilder
        //    {
        //        DataSource = Hely,
        //        Mode = SqliteOpenMode.ReadWriteCreate,
        //        Password = Jelszó
        //    }.ToString();
        //}

        private string BuildConnectionString()
        {
            return new SqliteConnectionStringBuilder
            {
                DataSource = Hely,
                Mode = SqliteOpenMode.ReadWriteCreate,
                Password = Jelszó,
                // Bekapcsolja a kapcsolatgyűjtőt, ami segít a zárolások hatékonyabb kezelésében
                Pooling = true,
                // Növeli a várakozási időt (másodpercben), ha az adatbázis épp foglalt
                DefaultTimeout = 30
            }.ToString();
        }


        public void CreateTable(string sql)
        {
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
                MessageBox.Show(ex.Message);
            }
        }
    }
}
