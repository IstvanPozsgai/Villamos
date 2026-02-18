using Microsoft.Data.Sqlite;
using System.IO;
using System.Windows.Forms;
using Villamos.Adatszerkezet;

namespace Villamos
{
    public static  class SqLite_Adatbázis
    {
        static string ConnectionString;



        static SqLite_Adatbázis()
        {
            //ConnectionString = BuildConnectionString();
        }


        public static  string BuildConnectionString(string Hely, string Jelszó)
        {
            return new SqliteConnectionStringBuilder
            {
                DataSource = Hely,
                Mode = SqliteOpenMode.ReadWriteCreate,
                Password = Jelszó
            }.ToString();
        }


        public static  void CreateTable(string sql)
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
        public static 
            void valami()
        {
            using (Context_Bejelentkezés_Oldalak db = new Context_Bejelentkezés_Oldalak())
            {
                SAdat_Belépés_Oldalak ujOldal = new SAdat_Belépés_Oldalak(1, "Home", "Főmenü", "Kezdőlap", true, false);
                db.Oldalak.Add(ujOldal);
                db.SaveChanges(); // Itt jön létre az adatbázis és a tábla, ha még nem létezik
            }
        }
    }
}
