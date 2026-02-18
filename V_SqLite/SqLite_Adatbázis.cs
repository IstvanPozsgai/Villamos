using Microsoft.Data.Sqlite;
using System.IO;
using System.Windows.Forms;
using Villamos.Adatszerkezet;

namespace Villamos
{
    public class SqLite_Adatbázis
    {
        string Hely { get; set; }
        string Jelszó { get; set; }

        readonly string TableName = "TestTable";

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

        private string BuildConnectionString()
        {
            return new SqliteConnectionStringBuilder
            {
                DataSource = Hely,
                Mode = SqliteOpenMode.ReadWriteCreate,
                Password = Jelszó
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


        //Használati példa:
        public void valami()
        {
            using (Context_Bejelentkezés_Oldalak db = new Context_Bejelentkezés_Oldalak())
            {
                Adat_Belépés_Oldalak ujOldal = new Adat_Belépés_Oldalak(1, "Home", "Főmenü", "Kezdőlap", true, false);
                db.Oldalak.Add(ujOldal);
                db.SaveChanges(); // Itt jön létre az adatbázis és a tábla, ha még nem létezik
            }


        }
    }
}
