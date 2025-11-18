using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;

namespace Villamos
{
    public static partial class MyClosedXML_Excel
    {
        public static void NyomtatásiTerület_részletes(string munkalap, Beállítás_Nyomtatás beállítás)
        {
            try
            {
                NyomtatásiBeállítások[munkalap] = beállítás;
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "NyomtatásiTerület_részletes", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void AlkalmazNyomtatásiBeállításokat(string fájlnév, Dictionary<string, Beállítás_Nyomtatás> beállítások)
        {
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fájlnév, true))
            {
                WorkbookPart workbookPart = doc.WorkbookPart;
                List<Sheet> sheets = workbookPart.Workbook.Sheets?.Elements<Sheet>().ToList();

                if (sheets == null) return;

                for (int i = 0; i < sheets.Count; i++)
                {
                    Sheet sheet = sheets[i];
                    string lapNév = sheet?.Name?.Value;

                    if (string.IsNullOrEmpty(lapNév) || !beállítások.TryGetValue(lapNév, out var beállítás)) continue;

                    WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                    Worksheet worksheet = worksheetPart.Worksheet;

                    //// 1. PageSetup létrehozása/frissítése
                    PageSetup pageSetup = worksheet.GetFirstChild<PageSetup>();
                    if (pageSetup == null)
                    {
                        pageSetup = new PageSetup();
                        worksheet.Append(pageSetup);
                    }
                    pageSetup.PapírTájolás(beállítás);
                    pageSetup.Papírméret(beállítás);

                    worksheet.Papírkitöltés(beállítás);

                    // 2.
                    // Létező PageMargins eltávolítása, ha van
                    PageMargins pageMargins = worksheet.GetFirstChild<PageMargins>();
                    if (pageMargins == null)
                    {
                        // Ha nincs, létrehozzuk
                        pageMargins = new PageMargins();
                        worksheet.Append(pageMargins); // vagy InsertAfter<PageMargins>(...), ha fontos a sorrend
                    }
                    pageMargins.MargóBeállítás(beállítás);

                    // 3. Header/Footer
                    HeaderFooter headerFooter = worksheet.GetFirstChild<HeaderFooter>();
                    if (headerFooter == null)
                    {
                        headerFooter = new HeaderFooter();
                        worksheet.Append(headerFooter);
                    }
                    headerFooter.LáblécBeállítás(beállítás);


                    //       4.PrintOptions
                    PrintOptions printOptions = worksheet.GetFirstChild<PrintOptions>();
                    if (printOptions == null)
                    {
                        printOptions = new PrintOptions();
                        worksheet.InsertBefore(printOptions, worksheet.GetFirstChild<PageMargins>());
                    }
                    printOptions.Headings = false;
                    printOptions.GridLines = false;



                    workbookPart.SorOszlopIsmétlődés(beállítás, lapNév, i);
                    workbookPart.NyomtatásiTerület(beállítás, lapNév, i);

                    worksheet.Save();
                }
            }
        }

        // ===== Segédfüggvények=====
        private static string NévEllenőr(string name)
        {
            if (string.IsNullOrEmpty(name)) return name;

            // Ha tartalmaz szóközt, idézőjelet, vagy egyéb speciális karaktert, idézőjelek közé tesszük
            if (name.Contains(' ') || name.Contains('\'') || name.Contains('!') || name.Contains('#') ||
                name.Contains('%') || name.Contains('&') || name.Contains('(') || name.Contains(')') ||
                name.Contains('+') || name.Contains('-') || name.Contains(',') || name.Contains(';') ||
                name.Contains('<') || name.Contains('>') || name.Contains('=') || name.Contains('{') ||
                name.Contains('}') || name.Contains('[') || name.Contains(']') || name.Contains('^') ||
                name.Contains('~') || name.Contains('\\') || name.Contains('|'))
            {
                return "'" + name.Replace("'", "''") + "'";
            }

            return name;
        }

        private static void Papírkitöltés(this Worksheet worksheet, Beállítás_Nyomtatás beállítás)
        {
            try
            {
                if (worksheet == null) throw new ArgumentNullException(nameof(worksheet));

                // 1. PageSetup beállítása (ez a nyomtatási méretezés részletei)
                var pageSetup = worksheet.GetFirstChild<PageSetup>();
                if (pageSetup == null)
                {
                    pageSetup = new PageSetup();
                    worksheet.Append(pageSetup);
                }
                pageSetup.FitToWidth = (UInt32Value)(uint)beállítás.LapSzéles;
                pageSetup.FitToHeight = (UInt32Value)(uint)beállítás.LapMagas;

                // 2. SheetProperties (<sheetPr>) lekérése – ha nem létezik, létrehozzuk
                var sheetPr = worksheet.GetFirstChild<SheetProperties>();
                if (sheetPr == null)
                {
                    sheetPr = new SheetProperties();
                    // A sheetPr MINDIG legyen az első gyermek elem
                    worksheet.InsertAt(sheetPr, 0);
                }

                // 3. PageSetupProperties (<pageSetUpPr>) lekérése vagy létrehozása a sheetPr-ben
                var pageSetupPr = sheetPr.GetFirstChild<PageSetupProperties>();
                if (pageSetupPr == null)
                {
                    pageSetupPr = new PageSetupProperties();
                    sheetPr.Append(pageSetupPr);
                }

                // 4. FitToPage bekapcsolása
                pageSetupPr.FitToPage = true;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Papírkitöltés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void Papírméret(this PageSetup pageSetup, Beállítás_Nyomtatás beállítás)
        {
            try
            {   // Papírméret: A4 = 9
                if (beállítás.Papírméret.Trim() == "A3")
                    pageSetup.PaperSize = 8;
                else
                    pageSetup.PaperSize = 9;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Papírméret", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void PapírTájolás(this PageSetup pageSetup, Beállítás_Nyomtatás beállítás)
        {
            try
            {
                pageSetup.Orientation = beállítás.Álló ? OrientationValues.Portrait : OrientationValues.Landscape;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "PapírTájolás", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void MargóBeállítás(this PageMargins pageMargins, Beállítás_Nyomtatás beállítás)
        {
            try
            {
                pageMargins.Left = beállítás.BalMargó * MmToInch;
                pageMargins.Right = beállítás.JobbMargó * MmToInch;
                pageMargins.Top = beállítás.FelsőMargó * MmToInch;
                pageMargins.Bottom = beállítás.AlsóMargó * MmToInch;
                pageMargins.Header = beállítás.FejlécMéret * MmToInch;
                pageMargins.Footer = beállítás.LáblécMéret * MmToInch;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "MargóBeállítás", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void LáblécBeállítás(this HeaderFooter headerFooter, Beállítás_Nyomtatás beállítás)
        {
            try
            {
                headerFooter.AlignWithMargins = true;
                headerFooter.DifferentFirst = false;
                headerFooter.DifferentOddEven = false;

                headerFooter.OddHeader = new OddHeader
                {
                    Text = $"&L{beállítás.FejlécBal}&C{beállítás.FejlécKözép}&R{beállítás.FejlécJobb}"
                };

                headerFooter.OddFooter = new OddFooter
                {
                    Text = $"&L{beállítás.LáblécBal}&C{beállítás.LáblécKözép}&R{beállítás.LáblécJobb}"
                };
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "LáblécBeállítás", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void SorOszlopIsmétlődés(this WorkbookPart workbookPart, Beállítás_Nyomtatás beállítás, string lapNév, int i)
        {
            try
            {
                if (!string.IsNullOrEmpty(beállítás.IsmétlődőSorok) || !string.IsNullOrEmpty(beállítás.IsmétlődőOszlopok))
                {
                    Workbook workbook = workbookPart.Workbook;
                    DefinedNames definedNames = workbook.DefinedNames;
                    if (definedNames == null)
                    {
                        definedNames = new DefinedNames();
                        workbook.Append(definedNames);
                    }

                    // Eltávolítjuk a meglévő Print_Titles-t erre a munkalapra (index alapján)
                    DefinedName existingPrintTitle = definedNames.OfType<DefinedName>()
                        .FirstOrDefault(dn => dn.Name == "_xlnm.Print_Titles" && dn.LocalSheetId?.Value == (uint)i);
                    existingPrintTitle?.Remove();

                    // Referencia összeállítása
                    string escapedName = NévEllenőr(lapNév);
                    string reference = "";

                    if (!string.IsNullOrEmpty(beállítás.IsmétlődőSorok) && !string.IsNullOrEmpty(beállítás.IsmétlődőOszlopok))
                        reference = $"{escapedName}!{beállítás.IsmétlődőSorok},{escapedName}!{beállítás.IsmétlődőOszlopok}";
                    else if (!string.IsNullOrEmpty(beállítás.IsmétlődőSorok))
                        reference = $"{escapedName}!{beállítás.IsmétlődőSorok}";
                    else if (!string.IsNullOrEmpty(beállítás.IsmétlődőOszlopok))
                        reference = $"{escapedName}!{beállítás.IsmétlődőOszlopok}";

                    definedNames.Append(new DefinedName
                    {
                        Name = "_xlnm.Print_Titles",
                        LocalSheetId = (uint)i,
                        Text = reference
                    });
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "SorOszlopIsmétlődés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void NyomtatásiTerület(this WorkbookPart workbookPart, Beállítás_Nyomtatás beállítás, string lapNév, int i)
        {
            try
            {
                if (!string.IsNullOrEmpty(beállítás.NyomtatásiTerület))
                {
                    DefinedNames definedNames = workbookPart.Workbook.GetFirstChild<DefinedNames>();
                    if (definedNames == null)
                    {
                        definedNames = new DefinedNames();
                        workbookPart.Workbook.InsertAfter(definedNames, workbookPart.Workbook.GetFirstChild<Sheets>());
                    }

                    DefinedName printArea = definedNames.Elements<DefinedName>()
                        .FirstOrDefault(dn => dn.Name?.Value == "_xlnm.Print_Area" && dn.LocalSheetId?.Value == (uint)i);

                    printArea?.Remove();

                    definedNames.Append(new DefinedName
                    {
                        Name = "_xlnm.Print_Area",
                        LocalSheetId = (uint)i,
                        Text = $"{NévEllenőr(lapNév)}!{beállítás.NyomtatásiTerület}",
                        Hidden = true
                    });
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "NyomtatásiTerület", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}