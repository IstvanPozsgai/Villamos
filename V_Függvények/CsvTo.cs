using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;

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




    public static List<T> CsvToList<T>(string filePath, char separator = ';') where T : new()
    {
        var lista = new List<T>();
        var sorok = File.ReadAllLines(filePath, System.Text.Encoding.UTF8);

        if (sorok.Length <= 1) return lista; // Üres vagy csak fejléc

        // Megkeressük a T típus publikus tulajdonságait
        PropertyInfo[] properties = typeof(T).GetProperties();

        foreach (var sor in sorok.Skip(1)) // Fejléc átugrása
        {
            if (string.IsNullOrWhiteSpace(sor)) continue;

            var adatok = sor.Split(separator);
            var elem = new T();

            // Sorrendben feltöltjük a tulajdonságokat
            for (int i = 0; i < Math.Min(properties.Length, adatok.Length); i++)
            {
                properties[i].SetValue(elem, adatok[i].Trim());
            }

            lista.Add(elem);
        }

        return lista;
    }


}

