using System.Data;
using System.IO;

public static partial class Függvénygyűjtemény
{
    /// <summary>
    /// Ez az eljárás a megkapott csv fájlt beolvassa az első sornak a mezőnévnek kell lennie
    /// </summary>
    /// <param name="Fájlnév"></param>
    /// <returns></returns>
    public static DataTable CsvToDataTable(string Fájlnév)
    {
        DataTable dt = new DataTable();

        using (StreamReader sr = new StreamReader(Fájlnév))
        {
            // 1. Sor: Fejléc beolvasása az oszlopnevekhez
            string headerLine = sr.ReadLine();
            if (string.IsNullOrEmpty(headerLine)) return dt;

            string[] columns = headerLine.Split(';'); // Vagy ';' a régiótól függően
            foreach (string column in columns)
            {
                dt.Columns.Add(column.Trim());
            }

            // 2. Többi sor: Adatok feltöltése
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (string.IsNullOrEmpty(line)) continue;

                string[] rows = line.Split(';');
                dt.Rows.Add(rows);
            }
        }

        return dt;
    }

}

