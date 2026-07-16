using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Excel_Elfekvő_Export
    {
        private readonly DateTime MA = DateTime.Today;
        private readonly DateTime ALAP_DATUM = new DateTime(1900, 1, 1);

        #region Publikus Export Metódusok

        public void Export(string fájlnév)
        {
            try
            {
                Kezelő_Elfekvő kézElfekvő = new Kezelő_Elfekvő();
                List<Adat_Elfekvő> adatok = kézElfekvő.Lista_Adatok();

                if (adatok == null || adatok.Count == 0)
                    throw new HibásBevittAdat("Nincs exportálható adat az adatbázisban!");

                Export(adatok, fájlnév);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba rögzítésre került a naplóban.", "Hiba történt", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Export(List<Adat_Elfekvő> adatok, string fájlnév)
        {
            if (adatok == null || adatok.Count == 0) return;

            try
            {
                // Szolgálattelepek lekérdezése az adatbázisból a párosításhoz
                Kezelő_Kiegészítő_Szolgálattelepei kezelőSzolg = new Kezelő_Kiegészítő_Szolgálattelepei();
                var telephelyAdatok = kezelőSzolg.Lista_Adatok();

                using (var workbook = new XLWorkbook())
                {
                    // Részletes lapokhoz a csoportosítás Raktárhely alapján
                    var raktarCsoportok = adatok.GroupBy(a => a.Raktárhely).OrderBy(g => g.Key).ToList();

                    // Összesítő lap (Dashboard)
                    KészítsDashboard(workbook, adatok, telephelyAdatok);

                    // Részletes lapok automatikus elkészítése
                    KészítsRészletesLapokat(workbook, raktarCsoportok);

                    workbook.SaveAs(fájlnév);
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                throw;
            }
        }

        #endregion

        #region Munkalap Generáló Metódusok

        private void KészítsDashboard(IXLWorkbook workbook, List<Adat_Elfekvő> adatok, List<Adat_Kiegészítő_Szolgálattelepei> telephelyAdatok)
        {
            var ws = workbook.Worksheets.Add("Összesítés");
            int sor = 1;

            ws.Cell(sor, 1).Value = "Lekérdezés dátuma";
            ws.Cell(sor, 2).Value = MA;
            ws.Cell(sor, 2).Style.NumberFormat.Format = "yyyy.MM.dd";

            ws.Range(sor, 1, sor, 2).Style.Font.Bold = true;
            ws.Range(sor, 1, sor, 2).Style.Fill.BackgroundColor = XLColor.LightBlue;
            sor += 2;

            // Összesítő táblázat fejléce 
            ws.Cell(sor, 1).Value = "Szint / Megnevezés";
            ws.Cell(sor, 2).Value = "Készlet érték";
            ws.Cell(sor, 3).Value = "Elfekvő készlet érték";
            ws.Cell(sor, 4).Value = "Elfekvő százalék";

            var fejlécTartomány = ws.Range(sor, 1, sor, 4);
            fejlécTartomány.Style.Font.Bold = true;
            fejlécTartomány.Style.Fill.BackgroundColor = XLColor.LightGray;
            fejlécTartomány.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            fejlécTartomány.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            sor++;

            // HIERARCHIA PÁROSÍTÁS
            var illesztettAdatok = adatok.Select(a =>
            {
                string rKod = a.Raktárhely?.Trim().ToUpper() ?? "";

                // Közvetlen illesztés az adatbázisból lekért listával
                var info = telephelyAdatok?.FirstOrDefault(t => t.Raktár?.Trim().ToUpper() == rKod);

                string tNev = info?.Telephelynév?.Trim() ?? "";
                string szaksz = info?.Szolgálatnév?.Trim() ?? "Ismeretlen Szakszolgálat";

                // Ha nincs hozzá telephely, csak a raktárkód jelenik meg
                string megnevezes = string.IsNullOrEmpty(tNev) ? a.Raktárhely?.Trim() : $"{a.Raktárhely?.Trim()} - {tNev}";

                return new
                {
                    Adat = a,
                    Szakszolgalat = szaksz,
                    Megnevezes = megnevezes
                };
            }).ToList();

            // Csoportosítás Szakszolgálat alapján
            var szakszGroups = illesztettAdatok.GroupBy(x => x.Szakszolgalat).OrderBy(g => g.Key).ToList();

            foreach (var szakszGroup in szakszGroups)
            {
                // Csoportosítás Telephelyek (Megnevezések) alapján az adott szakszolgálaton belül
                var telephelyCsoportok = szakszGroup.GroupBy(x => x.Megnevezes).OrderBy(g => g.Key).ToList();

                foreach (var thGroup in telephelyCsoportok)
                {
                    // Raktárhely / Telephely sorok kiírása
                    KiirOsszesitoSor(ws, ref sor, thGroup.Key, thGroup.Select(x => x.Adat), XLColor.NoColor, felkover: false);
                }

                // A Szakszolgálati blokk végén egyetlen lezáró ÖSSZESEN sor (Halványsárga háttérrel)
                KiirOsszesitoSor(ws, ref sor, "ÖSSZESEN", szakszGroup.Select(x => x.Adat), XLColor.LightYellow, felkover: true);

                sor++; // Üres sor a blokkok közé az átláthatóságért
            }

            // Oszlopok automatikus szélesítése
            ws.Columns().AdjustToContents();
            foreach (var col in ws.ColumnsUsed()) col.Width += 3.0; // Biztonsági ráhagyás
        }

        private void KészítsRészletesLapokat(IXLWorkbook workbook, List<IGrouping<string, Adat_Elfekvő>> raktarCsoportok)
        {
            foreach (var csoport in raktarCsoportok)
            {
                string rNev = string.IsNullOrWhiteSpace(csoport.Key) ? "Ismeretlen" : csoport.Key;
                string lapNév = rNev.Replace("[", "").Replace("]", "").Replace("*", "").Replace("?", "").Replace(":", "").Replace("\\", "").Replace("/", "");
                if (lapNév.Length > 31) lapNév = lapNév.Substring(0, 31);

                int counter = 1;
                string eredetiNév = lapNév;
                while (workbook.Worksheets.Contains(lapNév))
                {
                    lapNév = eredetiNév.Substring(0, Math.Min(eredetiNév.Length, 28)) + "_" + counter++;
                }

                var ws = workbook.Worksheets.Add(lapNév);

                ws.Cell(1, 1).Value = "Anyag";
                ws.Cell(1, 2).Value = "Anyag rövid szövege";
                ws.Cell(1, 3).Value = "Raktárhely";
                ws.Cell(1, 4).Value = "Sarzs";
                ws.Cell(1, 5).Value = "Szabadon használható";
                ws.Cell(1, 6).Value = "Szab.felh. érték";
                ws.Cell(1, 7).Value = "Utolsó mozgás";

                var range = ws.Range(1, 1, 1, 7);
                range.Style.Font.Bold = true;
                range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                range.Style.Fill.BackgroundColor = XLColor.LightGray;
                ws.AutoFilter.Clear();
                range.SetAutoFilter();
                ws.SheetView.FreezeRows(1);

                int dSor = 2;
                foreach (var elem in csoport)
                {
                    ws.Cell(dSor, 1).Value = elem.Anyag ?? "";
                    ws.Cell(dSor, 1).Style.NumberFormat.Format = "@";

                    ws.Cell(dSor, 2).Value = elem.Anyag_rövid_szövege ?? "";
                    ws.Cell(dSor, 3).Value = elem.Raktárhely ?? "";

                    ws.Cell(dSor, 4).Value = elem.Sarzs ?? "";
                    ws.Cell(dSor, 4).Style.NumberFormat.Format = "@";

                    ws.Cell(dSor, 5).Value = elem.Szabadon_használható;
                    ws.Cell(dSor, 5).Style.NumberFormat.Format = "#,##0";

                    ws.Cell(dSor, 6).Value = elem.Szab_felh_érték;
                    ws.Cell(dSor, 6).Style.NumberFormat.Format = "#,##0 Ft";

                    ws.Cell(dSor, 7).Value = elem.Utolsó_mozgás;
                    ws.Cell(dSor, 7).Style.NumberFormat.Format = "yyyy.MM.dd";

                    dSor++;
                }

                ws.Columns().AdjustToContents();
                foreach (var col in ws.ColumnsUsed()) col.Width += 3.0;
            }
        }

        #endregion

        #region Üzleti Logika és Közös Számítási Metódusok

        private void KiirOsszesitoSor(IXLWorksheet ws, ref int sor, string megnevezes, IEnumerable<Adat_Elfekvő> adatok, XLColor hatterSzin, bool felkover = false)
        {
            double keszletErtek = adatok.Sum(a => a.Szab_felh_érték);

            // 365 napnál régebbi (vagy 1900.01.01 = sosem mozgott) tételek értéke
            double elfekvoErtek = adatok.Where(a => a.Utolsó_mozgás <= ALAP_DATUM || (MA - a.Utolsó_mozgás).TotalDays > 365)
                                        .Sum(a => a.Szab_felh_érték);

            double szazalek = keszletErtek > 0 ? (elfekvoErtek / keszletErtek) : 0;

            ws.Cell(sor, 1).Value = megnevezes;
            ws.Cell(sor, 2).Value = keszletErtek;
            ws.Cell(sor, 3).Value = elfekvoErtek;
            ws.Cell(sor, 4).Value = szazalek;

            ws.Cell(sor, 2).Style.NumberFormat.Format = "#,##0 Ft";
            ws.Cell(sor, 3).Style.NumberFormat.Format = "#,##0 Ft";
            ws.Cell(sor, 4).Style.NumberFormat.Format = "0.00%";

            var formatRng = ws.Range(sor, 1, sor, 4);
            formatRng.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            formatRng.Style.Border.BottomBorderColor = XLColor.LightGray;

            if (felkover) formatRng.Style.Font.Bold = true;
            if (hatterSzin != XLColor.NoColor) formatRng.Style.Fill.BackgroundColor = hatterSzin;

            if (megnevezes == "ÖSSZESEN")
            {
                formatRng.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                formatRng.Style.Border.TopBorderColor = XLColor.Gray;
                formatRng.Style.Border.BottomBorder = XLBorderStyleValues.Double;
                formatRng.Style.Border.BottomBorderColor = XLColor.Black;
            }

            sor++;
        }

        #endregion
    }
}