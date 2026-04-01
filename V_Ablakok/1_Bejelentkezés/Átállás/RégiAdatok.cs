using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Villamos.Ablakok
{
    public class RégiAdatok
    {
        public static void TelephelyJogosultsaga()
        {
            // ⚙️ Konfiguráció – itt módosíthatod az útvonalakat
            string sourcePath = $@"{Application.StartupPath}\V_Ablakok";
            string outputFile = $@"{Application.StartupPath}\Temp\Telephely.csv";


            List<string> logLines = new List<string>();

            if (!Directory.Exists(sourcePath))
            {
                MessageBox.Show("❌ HIBA: A forráskönyvtár nem létezik!");
                return;
            }

            List<LáthatóságAdatok> Adatok = new List<LáthatóságAdatok>();

            // .cs fájlok keresése, .designer.cs KIZÁRVA
            string[] csFiles = Directory.GetFiles(sourcePath, "*.cs", SearchOption.AllDirectories)
                .Where(f => !f.EndsWith(".designer.cs", StringComparison.OrdinalIgnoreCase))
                .ToArray();


            // 🔍 Regex minták – Unicode támogatással
            Regex buttonPattern = new Regex(
                @"(@?[\p{L}_][\p{L}\p{N}_]*)\s*\.\s*(Visible|Enabled)\s*=\s*(true|false)",
                RegexOptions.Compiled);

            Regex vanjogaPattern = new Regex(
                @"Vanjoga\s*\(\s*([a-zA-Z_][a-zA-Z0-9_]*|\d+)\s*,\s*(\d+)\s*\)",
                RegexOptions.Compiled);

            Regex visiblePattern = new Regex(
                @"Program\.(Postás[\p{L}_][\p{L}\p{N}_]*)",
                RegexOptions.Compiled);

            // 🎯 Gomb név szűrő – rugalmas, de pontos
            Regex isButtonName = new Regex(
                @"^(Btn|Button)|_gomb$|_Gomb$|(Rögzít|Töröl|OK|Fel|Mentés|Beolvas|Nyomtat|Tölt|Át|Vonaltöröl|VonalOk|Kiad|Javít)",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

            foreach (string filePath in csFiles)
            {
                try
                {
                    //if (Path.GetFileName(filePath).Trim() == "Ablak_alap_program_kiadás.cs")
                    //    return;

                    // 📦 Fájl olvasás több kódolással
                    string content = ReadFileWithEncoding(filePath);
                    if (string.IsNullOrEmpty(content)) continue;

                    // 🎯 1. LÉPÉS: Jogosultságkiosztás() metódus kinyerése
                    string MetódusTest = JogMetódus(content, "Jogosultságkiosztás");

                    if (string.IsNullOrEmpty(MetódusTest)) continue;

                    //Metisztjuk a metódus törzsét, hogy könnyebben dolgozhassunk vele (pl. kontextus keresés)
                    MetódusTest = MetódusTest.Replace("{", "");
                    MetódusTest = MetódusTest.Replace("}", "");
                    MetódusTest = MetódusTest.Replace(";", "");

                    if (!MetódusTest.Contains("Program.PostásTelephely")) continue; // Ha nincs ilyen, akkor nem is érdekel minket ez a metódus

                    string formName = Path.GetFileNameWithoutExtension(filePath);

                    // Felosztás a sortörés karakterek mentén és listává alakítás
                    List<string> Sorok = MetódusTest.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();
                    string gombnév = "";
                    string tulajdonság = "";
                    string érték = "";
                    string reláció = "";
                    int i = 0;

                    while (Sorok[i].Trim() != "else")
                    {
                        string Sor = Sorok[i].Trim(); // Sor elejének és végének levágása

                        if (Sor.Contains("if (Program.PostásTelephely.Trim() !="))
                        {
                            reláció = "_";

                        }
                        if (Sor.Contains("if (Program.PostásTelephely.Trim() =="))
                        {
                            reláció = "Főmérnökség";

                        }
                        if (Sor.Contains("if (Program.PostásTelephely !="))
                        {
                            reláció = "_";

                        }
                        if (Sor.Contains("if (Program.PostásTelephely =="))
                        {
                            reláció = "Főmérnökség";

                        }

                        if (reláció.Trim() != "" && (Sor.Contains("true") || Sor.Contains("false")) && Sor.Contains('='))
                        {
                            string[] darabol = Sor.Split('=');
                            string[] darabol2 = darabol[0].Split('.');

                            gombnév = darabol2[0].Trim();
                            tulajdonság = darabol2[1].Trim();
                            érték = darabol[1].Trim();
                            if (reláció.Trim() == "_")
                            {
                                if (érték == "true")
                                    érték = "false";
                                else
                                    érték = "true";
                            }

                            if (reláció.Trim() != "" && tulajdonság.Trim() != "" && érték.Trim() != "")
                            {
                                Adatok.Add(new LáthatóságAdatok
                                {
                                    AblakNev = formName,
                                    GombNev = gombnév,
                                    Tulajdonsag = tulajdonság,
                                    Ertek = érték,
                                    Reláció = reláció
                                });
                                gombnév = "";
                                tulajdonság = "";
                                érték = "";
                                
                            }
                        }
                        i++;
                    }
                }
                catch (Exception ex)
                {
                }
            }

            // 💾 CSV export
            if (Adatok.Count > 0)
            {
                var sb = new StringBuilder();
                sb.AppendLine("AblakNev;GombNev;Tulajdonsag;Ertek;Reláció");

                foreach (var r in Adatok)
                {
                    string EscapeCsv(string val) => val?.Contains(";") == true ? $"\"{val}\"" : val;
                    sb.AppendLine($"{EscapeCsv(r.AblakNev)};{EscapeCsv(r.GombNev)};{r.Tulajdonsag};{r.Ertek};{r.Reláció};");

                }

                File.WriteAllText(outputFile, sb.ToString(), new UTF8Encoding(true)); // UTF8 + BOM
            }
        }


        public static void GombokJogosultsaga()
        {
            // ⚙️ Konfiguráció – itt módosíthatod az útvonalakat
            string sourcePath = $@"{Application.StartupPath}\V_Ablakok";
            string outputFile = $@"{Application.StartupPath}\Temp\AblakokGombok.csv";

            if (!Directory.Exists(sourcePath)) return;


            List<GombAdatok> Adatok = new List<GombAdatok>();

            // .cs fájlok keresése, .designer.cs KIZÁRVA
            var csFiles = Directory.GetFiles(sourcePath, "*.cs", SearchOption.AllDirectories)
                .Where(f => !f.EndsWith(".designer.cs", StringComparison.OrdinalIgnoreCase))
                .ToArray();

            // 🔍 Regex minták – Unicode támogatással
            Regex buttonPattern = new Regex(
                @"(@?[\p{L}_][\p{L}\p{N}_]*)\s*\.\s*(Visible|Enabled)\s*=\s*(true|false)",
                RegexOptions.Compiled);

            Regex vanjogaPattern = new Regex(
                @"Vanjoga\s*\(\s*([a-zA-Z_][a-zA-Z0-9_]*|\d+)\s*,\s*(\d+)\s*\)",
                RegexOptions.Compiled);

            Regex visiblePattern = new Regex(
                @"Program\.(Postás[\p{L}_][\p{L}\p{N}_]*)",
                RegexOptions.Compiled);

            // 🎯 Gomb név szűrő – rugalmas, de pontos
            Regex isButtonName = new Regex(
                @"^(Btn|Button)|_gomb$|_Gomb$|(Rögzít|Töröl|OK|Fel|Mentés|Beolvas|Nyomtat|Tölt|Át|Vonaltöröl|VonalOk|Kiad|Javít)",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

            foreach (var filePath in csFiles)
            {
                try
                {
                    //if (Path.GetFileName(filePath).Trim() == "A_Felhasználó.cs")
                    //    return;

                    // 📦 Fájl olvasás 
                    string content = ReadFileWithEncoding(filePath);
                    if (string.IsNullOrEmpty(content)) continue;

                    // 🎯 1. LÉPÉS: Jogosultságkiosztás() metódus kinyerése
                    string MetódusTest = JogMetódus(content, "Jogosultságkiosztás");

                    if (string.IsNullOrEmpty(MetódusTest)) continue; // Nincs ilyen metódus, ugorjuk át

                    //Metisztjuk a metódus törzsét, hogy könnyebben dolgozhassunk vele (pl. kontextus keresés)
                    MetódusTest = MetódusTest.Replace("{", "");
                    MetódusTest = MetódusTest.Replace("}", "");
                    MetódusTest = MetódusTest.Replace(";", "");

                    string formName = Path.GetFileNameWithoutExtension(filePath);

                    // Felosztás a sortörés karakterek mentén és listává alakítás
                    List<string> Sorok = MetódusTest.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();
                    string melyikelem = "";
                    string egykettőhárom = "";
                    string gombnév = "";
                    string tulajdonság = "";
                    string érték = "";
                    for (int i = 0; i < Sorok.Count; i++)
                    {

                        string Sor = Sorok[i].Trim(); // Sor elejének és végének levágása
                        if (Sor.StartsWith("//")) continue; // Kommentek kihagyása

                        if (Sor.Contains("int Melyikelem ="))
                        {
                            melyikelem = Sor.Replace("int Melyikelem =", "").Trim();
                            egykettőhárom = "";
                        }
                        else if (Sor.Contains("int melyikelem ="))
                        {
                            melyikelem = Sor.Replace("int melyikelem =", "").Trim();
                            egykettőhárom = "";
                        }
                        else if (Sor.Contains("Melyikelem ="))
                        {
                            melyikelem = Sor.Replace("Melyikelem =", "").Trim();
                            egykettőhárom = "";
                        }
                        else if (Sor.Contains("melyikelem ="))
                        {
                            melyikelem = Sor.Replace("melyikelem =", "").Trim();
                            egykettőhárom = "";
                        }

                        else if (Sor.Contains("if (MyF.Vanjoga(Melyikelem,"))
                        {
                            egykettőhárom = Sor.Replace("if (MyF.Vanjoga(Melyikelem, ", "").Trim();
                            egykettőhárom = egykettőhárom.Replace("))", "").Trim();
                        }
                        else if (Sor.Contains("if (MyF.Vanjoga(melyikelem,"))
                        {
                            egykettőhárom = Sor.Replace("if (MyF.Vanjoga(melyikelem, ", "").Trim();
                            egykettőhárom = egykettőhárom.Replace("))", "").Trim();
                        }

                        if (melyikelem.Trim() != "" && egykettőhárom.Trim() != "" && (Sor.Contains("true") || Sor.Contains("false")))
                        {
                            string[] darabol = Sor.Split('=');
                            string[] darabol2 = darabol[0].Split('.');

                            gombnév = darabol2[0].Trim();
                            tulajdonság = darabol2[1].Trim();
                            érték = darabol[1].Trim();

                        }
                        if (gombnév.Trim() != "" && tulajdonság.Trim() != "" && érték.Trim() != "")
                        {
                            Adatok.Add(new GombAdatok
                            {
                                AblakNev = formName,
                                GombNev = gombnév,
                                Tulajdonsag = tulajdonság,
                                Ertek = érték,
                                MelyikElem = melyikelem,
                                EgyKettőHárom = egykettőhárom
                            });
                            gombnév = "";
                            tulajdonság = "";
                            érték = "";
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }

            // 💾 CSV export
            if (Adatok.Count > 0)
            {
                var sb = new StringBuilder();
                sb.AppendLine("AblakNev;GombNev;Tulajdonsag;Ertek;MelyikElem;EgyKettőHárom");

                foreach (var r in Adatok)
                {
                    string EscapeCsv(string val) => val?.Contains(";") == true ? $"\"{val}\"" : val;
                    sb.AppendLine($"{EscapeCsv(r.AblakNev)};{EscapeCsv(r.GombNev)};{r.Tulajdonsag};{r.Ertek};" +
                        $"{EscapeCsv(r.MelyikElem)};{EscapeCsv(r.EgyKettőHárom)}");

                }

                File.WriteAllText(outputFile, sb.ToString(), new UTF8Encoding(true)); // UTF8 + BOM
            }
        }


        // 📦 Segédfüggvény: Fájl olvasás több kódolással
        static string ReadFileWithEncoding(string filePath)
        {
            var encodings = new[]
            {
                Encoding.UTF8,
                Encoding.Default,              // Magyar Windows-on = Windows-1250 (nem kell regisztrálni!)
                new UTF8Encoding(false)        // UTF8 BOM nélkül
            };

            foreach (var enc in encodings)
            {
                try
                {
                    string content = File.ReadAllText(filePath, enc);
                    if (!string.IsNullOrEmpty(content) && !content.Contains('\0'))
                        return content;
                }
                catch { }
            }
            return null;
        }


        // 🎯 Segédfüggvény: Metódus törzsének kinyerése brace-countinggal
        static string JogMetódus(string code, string methodName)
        {
            // Rugalmas regex a metódus signature-re (private/public/protected, static, paraméterek)
            var pattern = $@"(?:public|private|protected)?\s*(?:static\s+)?void\s+{Regex.Escape(methodName)}\s*\([^)]*\)\s*\{{";
            var match = Regex.Match(code, pattern, RegexOptions.IgnoreCase);

            if (!match.Success)
                return null;

            int startBrace = match.Index + match.Length - 1;
            int braceCount = 1;
            int pos = startBrace + 1;

            while (pos < code.Length && braceCount > 0)
            {
                char c = code[pos];

                // String és char literálok átugrása
                if (c == '"' || c == '\'')
                {
                    char quote = c;
                    pos++;
                    while (pos < code.Length)
                    {
                        if (code[pos] == '\\' && pos + 1 < code.Length) { pos += 2; continue; }
                        if (code[pos] == quote) { pos++; break; }
                        pos++;
                    }
                    continue;
                }

                // Kommentek egyszerű figyelmen kívül hagyása (// és /* */)
                if (c == '/' && pos + 1 < code.Length)
                {
                    if (code[pos + 1] == '/') // // komment
                    {
                        pos += 2;
                        while (pos < code.Length && code[pos] != '\n') pos++;
                        continue;
                    }
                    else if (code[pos + 1] == '*') // /* komment */
                    {
                        pos += 2;
                        while (pos + 1 < code.Length && !(code[pos] == '*' && code[pos + 1] == '/')) pos++;
                        pos += 2;
                        continue;
                    }
                }

                if (c == '{') braceCount++;
                else if (c == '}') braceCount--;

                pos++;
            }

            if (braceCount != 0)
                return null;

            // Visszaadjuk a metódus törzsét kapcsosok NÉLKÜL
            return code.Substring(startBrace + 1, pos - startBrace - 2);
        }
    }
}
