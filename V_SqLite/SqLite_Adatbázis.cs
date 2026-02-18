using Microsoft.Data.Sqlite;
using System.Windows.Forms;

namespace Villamos
{
    public static class SqLite_Adatbázis
    {
        static SqLite_Adatbázis()
        {
        }

        /// <summary>
        /// Kapcsolati karakterláncot hoz létre egy SQLite adatbázishoz a megadott fájlútvonal és jelszó használatával.
        /// </summary>
        /// <remarks>A létrehozott kapcsolati karakterlánc írási-olvasási hozzáférést biztosít, és létrehozza az
        /// adatbázisfájlt, ha az még nem létezik.</remarks>
        /// <param name="Hely">Az SQLite adatbázis fájlútvonala vagy helye. Ez a paraméter nem lehet null vagy üres.</param>
        /// <param name="Jelszó">Az adatbázis-kapcsolat titkosításához használt jelszó. Ez a paraméter nem lehet null.</param>
        /// <returns>A megadott SQLite adatbázisfájlhoz és jelszóhoz konfigurált kapcsolati karakterlánc, írási-olvasási hozzáféréssel
        /// és automatikus létrehozással, ha az adatbázis nem létezik.</returns>
        public static string BuildConnectionString(string Hely, string Jelszó)
        {
            return new SqliteConnectionStringBuilder
            {
                DataSource = Hely,
                Mode = SqliteOpenMode.ReadWriteCreate,
                Password = Jelszó
            }.ToString();
        }


        public static void CreateTable(string sql)
        {
            try
            {
                SqliteConnection connection = new SqliteConnection(ConnectionString);
                connection.Open();

                var command = new SqliteCommand(sql, connection);
                command.ExecuteNonQuery();

                connection.Close();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
