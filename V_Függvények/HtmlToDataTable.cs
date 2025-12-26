using HtmlAgilityPack;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

public static partial class Függvénygyűjtemény
{



    /// <summary>
    /// BOM alapján kódolás detektálása, majd fallback UTF-8 / Windows-1250 próbálgatással
    /// </summary>
    public static Encoding DetectEncodingSafe(string filePath)
    {
        // 1. Először ellenőrizzük a BOM-ot
        var bom = new byte[4];
        using (var fs = File.OpenRead(filePath))
        {
            fs.Read(bom, 0, 4);
        }

        // UTF-8 BOM: EF BB BF
        if (bom[0] == 0xEF && bom[1] == 0xBB && bom[2] == 0xBF)
            return Encoding.UTF8;

        // UTF-16 BE BOM: FE FF
        if (bom[0] == 0xFE && bom[1] == 0xFF)
            return Encoding.BigEndianUnicode;

        // UTF-16 LE BOM: FF FE
        if (bom[0] == 0xFF && bom[1] == 0xFE)
            return Encoding.Unicode;

        // 2. Ha nincs BOM, akkor próbálkozunk
        // UTF-8 a leggyakoribb modern HTML-hez, de magyar tartalom esetén gyakran Windows-1250
        string contentUtf8;
        try
        {
            contentUtf8 = File.ReadAllText(filePath, Encoding.UTF8);
            // Ha nincs benne jellegzetes "mocskos" mintázat (pl. Ã¡, â€‹), akkor jó a UTF-8
            if (!Regex.IsMatch(contentUtf8, @"[ÃÂâ€¢ï¿½]"))
                return Encoding.UTF8;
        }
        catch
        {
            // Ha UTF-8 olvasás kivételt dob, akkor nem UTF-8
        }

        // 3. Fallback: Windows-1250 (magyar nyelvű tartalomhoz ideális)
        return Encoding.GetEncoding(1250);
    }

    /// <summary>
    /// Tisztítja a szöveget HTML specifikus "szeméttől"
    /// </summary>
    public static string Html_Szöveg_Tisztítás(string bemenet)
    {
        if (string.IsNullOrWhiteSpace(bemenet))
            return string.Empty;

        string tiszta = System.Net.WebUtility.HtmlDecode(bemenet);

        tiszta = tiszta
            .Replace('\u00A0', ' ')   // &nbsp;
            .Replace('\u200B', ' ')   // Zero-width space
            .Replace('\uFEFF', ' ')   // BOM in content
        //    .Replace('\uFFFD', '')    // Replacement character
            .Replace("â€‹", "")        // UTF-8/1250 mismatch
            .Replace("Ã¡", "á")        // Gyakori hibák javítása (továbbiak hozzáadhatók)
            .Replace("Ã©", "é")
            .Replace("Ã­", "í")
            .Replace("Ã³", "ó")
            .Replace("Ãº", "ú")
            .Replace("Ã¶", "ö")
            .Replace("Ã¼", "ü")
            .Replace("Ã¶", "ő")
            .Replace("Å±", "ű")
            .Replace("Ã–", "Ö")
            .Replace("Ãœ", "Ü")
            .Replace("Å", "Ő")
            .Replace("Å°", "Ű")
            .Replace("ï¿½", "");       // Invalid char

        tiszta = Regex.Replace(tiszta, @"\s+", " ");
        return tiszta.Trim();
    }

    /// <summary>
    /// HTML táblázat beolvasása – Ude nélkül
    /// </summary>
    public static DataTable Html_Tábla_Beolvas(string htmlPath)
    {
        Encoding encoding = DetectEncodingSafe(htmlPath);
        string htmlContent = File.ReadAllText(htmlPath, encoding);

        var doc = new HtmlDocument();
        doc.LoadHtml(htmlContent);

        var table = doc.DocumentNode.SelectSingleNode("//table") ?? throw new InvalidOperationException("Nem található <table> elem a HTML fájlban!");
        var dt = new DataTable();
        var headerRow = table.SelectSingleNode(".//tr") ?? throw new InvalidOperationException("Hiányzik a fejléc sor!");
        var headerCells = headerRow.SelectNodes(".//th|.//td") ?? new HtmlNodeCollection(null);
        foreach (var th in headerCells)
        {
            string raw = th.InnerText;
            string clean = Html_Szöveg_Tisztítás(raw);
            string colName = string.IsNullOrWhiteSpace(clean)
                ? $"Oszlop_{dt.Columns.Count + 1}"
                : clean;

            int i = 1;
            string finalName = colName;
            while (dt.Columns.Contains(finalName))
                finalName = $"{colName}_{i++}";

            dt.Columns.Add(finalName, typeof(string));
        }

        var dataRows = table.SelectNodes(".//tr[position()>1]");
        if (dataRows != null)
        {
            foreach (var row in dataRows)
            {
                var cells = row.SelectNodes(".//td|.//th") ?? new HtmlNodeCollection(null);
                var values = new string[dt.Columns.Count];
                for (int i = 0; i < values.Length; i++)
                    values[i] = string.Empty;

                for (int i = 0; i < cells.Count && i < dt.Columns.Count; i++)
                {
                    values[i] = Html_Szöveg_Tisztítás(cells[i].InnerText);
                }

                dt.Rows.Add(values);
            }
        }

        return dt;
    }

}
