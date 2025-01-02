using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Villamos.Villamos_Adatbázis_Funkció
{
    /// <summary>
    /// Useful utilities for Microsoft Access Database files.
    /// </summary>
    public static class AccessDbLoader
    {
        /// <summary>
        /// Betölt egy Microsoft Access Database filet egy DataSet objektumba.
        /// A fájl lehet ACCDB vagy MDB formátumú is.
        /// </summary>
        /// <param name="fileName">A fájl neve amit be akarunk tölteni.</param>
        /// <returns>Egy DataSet objektum amelyben a Tables objektumban benne vannak a kiválasztott Microsoft Access Database meghatározott táblájának elemei.</returns>
        public static DataSet LoadFromFile(string fileName)
        {
            DataSet result = new DataSet();

            // Az egyserűség érdekében a DataSet-re a betöltött fájl neve alapján hivatkozhatunk  (kiterjesztés nélkül).
            result.DataSetName = Path.GetFileNameWithoutExtension(fileName).Replace(" ", "_");

            // ConnectionString létrehozása (OLEDB v12.0 használatával)
            fileName = Path.GetFullPath(fileName);
            string connString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}; Jet Oledb:Database Password=kloczkal", fileName);


            // Kapcsolat megnyitása
            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                conn.Open();

                // Lekérdezi az összes táblát az adatbázisban
                DataTable dt = conn.GetSchema("Tables");
                List<string> tablesName = dt.AsEnumerable().Select(dr => dr.Field<string>("TABLE_NAME")).Where(dr => !dr.StartsWith("MSys")).ToList();

                // Lekérdezi az összes adatot a táblákban
                foreach (string tableName in tablesName)
                {
                    using (OleDbCommand cmd = new OleDbCommand(string.Format("SELECT * FROM [{0}]", tableName), conn))
                    {
                        using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                        {
                            // Elmentjük a táblákat a DataSet-be.
                            DataTable buf = new DataTable("[" + tableName + "]");
                            adapter.Fill(buf);
                            result.Tables.Add(buf);
                        } // adapter
                    } // cmd
                } // tableName
            } // conn

            // Visszaadja a kitöltött DataSet-et
            return result;
        }
    }
}
