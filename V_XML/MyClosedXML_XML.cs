using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Villamos.Adatszerkezet;

namespace Villamos
{

    public static partial class MyClosedXML_Excel
    {
        private static readonly XNamespace ss = "urn:schemas-microsoft-com:office:spreadsheet";

        static readonly List<string> VartFejlec = new List<string>
            {
                "Viszonylat/ Szolg.szám",
                "Forg. szám",
                "Törzsszám",
                "Járművezető neve",
                "M. kód",
                "Kezdési idő",
                "Kezdési hely",
                "Végzési idő",
                "Végzési hely",
                "Start hossza",
                "Elv.",
                "Menetrendi járműtípusok",
                "Kért",
                "Át",
                "Dolg."
            };

        private static bool EllenorizFejlec(XDocument doc, out string hiba)
        {
            hiba = "";

            var rows = doc.Descendants(ss + "Row").ToList();
            if (rows.Count < 7)
            {
                hiba = "A fájl túl kevés sort tartalmaz.";
                return false;
            }

            // A fejléc a 6. sor (index 5)
            var headerRow = rows[5];
            var headerCells = headerRow.Elements(ss + "Cell").ToList();

            // Ha kevesebb cella van, mint a várt fejléc
            if (headerCells.Count < VartFejlec.Count)
            {
                hiba = "A fejléc túl kevés oszlopot tartalmaz.";
                return false;
            }

            // Soronként összehasonlítjuk
            for (int i = 0; i < VartFejlec.Count; i++)
            {
                string kapott = XmlCell.GetValue(headerCells[i]).Trim();
                string vart = VartFejlec[i];

                if (!kapott.Equals(vart, StringComparison.OrdinalIgnoreCase))
                {
                    hiba = $"A fejléc nem megfelelő.\nVárt: \"{vart}\"\nKapott: \"{kapott}\"";
                    return false;
                }
            }

            return true;
        }



        public static List<Adat_Kidobó> BeolvasKidobo(string Fájlnév, out List<string> hibak)
        {
            hibak = new List<string>();
            List<Adat_Kidobó> Adatok = new List<Adat_Kidobó>();

            // fájl beolvasása nyers szövegként
            var enc = Encoding.GetEncoding("ISO-8859-2");
            string raw;
            using (var sr = new StreamReader(Fájlnév, enc, detectEncodingFromByteOrderMarks: false))
            { raw = sr.ReadToEnd(); }
            string clean = TisztitXML(raw);

            XDocument doc;

            try
            {
                doc = XDocument.Parse(clean);
            }
            catch (Exception ex)
            {
                hibak.Add("A fájl nem olvasható: " + ex.Message);
                return Adatok;
            }

            //  FEJLÉC ELLENŐRZÉS
            if (!EllenorizFejlec(doc, out string fejlecHiba))
            {
                hibak.Add(fejlecHiba);
                return Adatok;
            }

            //  Ha a fejléc jó → mehet a sima index alapú beolvasás
            var rows = doc.Descendants(ss + "Row").ToList();

            for (int r = 7; r < rows.Count; r++)
            {
                var row = rows[r];
                var cells = row.Elements(ss + "Cell").ToList();

                if (cells.Count == 0)
                    continue;

                try
                {
                    string viszSzolg = XmlCell.GetValue(cells[0]);
                    if (string.IsNullOrWhiteSpace(viszSzolg))
                        continue;

                    string[] darabol = viszSzolg.Split('/');
                    string viszonylat = darabol[0].Trim();

                    string forgalmi = XmlCell.GetValue(cells[1]);
                    string torzsszam = XmlCell.GetValue(cells[2]);
                    string jvez = XmlCell.GetValue(cells[3]);
                    string mkod = XmlCell.GetValue(cells[4]);

                    DateTime kezd = ParseIdo(XmlCell.GetValue(cells[5]));
                    DateTime veg = ParseIdo(XmlCell.GetValue(cells[7]));

                    string kezdhely = XmlCell.GetValue(cells[6]);
                    string veghely = XmlCell.GetValue(cells[8]);

                    string szerelveny = XmlCell.GetValue(cells[10]);

                    Adat_Kidobó adat = new Adat_Kidobó(
                        viszonylat,
                        forgalmi,
                        viszSzolg,
                        jvez,
                        kezd,
                        veg,
                        kezdhely,
                        veghely,
                        mkod,
                        "_",
                        "_",
                        "_",
                        szerelveny,
                        torzsszam
                    );

                    Adatok.Add(adat);
                }
                catch (Exception ex)
                {
                    hibak.Add($"Hiba a(z) {r + 1}. sor feldolgozásakor: {ex.Message}");
                }
            }

            return Adatok;
        }



        private static DateTime ParseIdo(string s)
        {
            if (DateTime.TryParse(s, out DateTime dt))
                return dt;

            return DateTime.Today;
        }

        public static DateTime KidobóDátumEll(string Fájlnév)
        {
            DateTime Válasz = new DateTime(1900, 1, 1);
            try
            {

                // fájl beolvasása nyers szövegként
                string raw = File.ReadAllText(Fájlnév);
                // tisztítás
                string clean = TisztitXML(raw);
                // XML betöltése a tisztított szövegből
                XDocument doc = XDocument.Parse(clean);

                // A dátum a 2. sor első cellájában van
                var row = doc.Descendants(ss + "Row").Skip(1).FirstOrDefault();
                if (row == null) return Válasz;
                var cell = row.Element(ss + "Cell");
                if (cell == null) return Válasz;
                string value = XmlCell.GetValue(cell);
                if (DateTime.TryParse(value, out Válasz)) return Válasz;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "KidobóDátumEll", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return Válasz;
        }

        public static string TisztitXML(string input)
        {
            return new string(input.Where(ch =>
        ch == 0x9 || ch == 0xA || ch == 0xD || ch >= 0x20
        ).ToArray());
        }
    }
}

public static class XmlCell
{
    private static readonly XNamespace ss = "urn:schemas-microsoft-com:office:spreadsheet";

    public static string GetValue(XElement cell)
    {
        return cell.Element(ss + "Data")?.Value?.Trim() ?? "";
    }
}


