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

        /// <summary>
        /// Független hívási pont: adatbázisból olvassa ki az adatokat, majd exportál.
        /// </summary>
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

        /// <summary>
        /// Fő exportáló motor: egy meglévő memórialistát dolgoz fel és ír ki Excelbe.
        /// </summary>
        public void Export(List<Adat_Elfekvő> adatok, string fájlnév)
        {
            if (adatok == null || adatok.Count == 0) return;

            try
            {
                using (var workbook = new XLWorkbook())
                {
                    // Dashboard (Összesítő) generálása
                    KészítsDashboard(workbook, adatok);

                    // Részletes munkalapok dinamikus létrehozása kizárólag Raktárhely alapján
                    KészítsRészletesLapokat(workbook, adatok);

                    // Fájl mentése
                    workbook.SaveAs(fájlnév);

                    Függvénygyűjtemény.Megnyitás(fájlnév);
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

        private void KészítsDashboard(IXLWorkbook workbook, List<Adat_Elfekvő> adatok)
        {
            var ws = workbook.Worksheets.Add("Összesítés");
            int sor = 1;

            // Készítés dátuma a B1 és C1 cellákba
            ws.Cell(sor, 2).Value = "Készítés dátuma:";
            ws.Cell(sor, 2).Style.Font.Bold = true;
            ws.Cell(sor, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            ws.Cell(sor, 3).Value = MA;
            ws.Cell(sor, 3).Style.NumberFormat.Format = "yyyy.MM.dd";
            ws.Cell(sor, 3).Style.Font.Bold = true;
            sor += 3; // Térköz az összesítő blokkok előtt

            KészítsMintaÖsszesítőBlokkokat(ws, adatok, ref sor);

            sor += 3; // Térköz

            // Bővített, részletes Dashboard mátrix
            KészítsRészletesMátrixot(ws, adatok, ref sor);

            sor += 3; // Térköz

            // Diagramok adatforrásának előkészítése
            KészítsDiagramAdatforrást(ws, adatok, ref sor);

            // Lap egységes oszlopszélesség igazítása
            ws.Columns().AdjustToContents();


            foreach (var col in ws.ColumnsUsed())
                col.Width += 3.0;

            ws.Column("D").Width = Math.Max(ws.Column("D").Width, 20);
        }

        private void KészítsRészletesLapokat(IXLWorkbook workbook, List<Adat_Elfekvő> adatok)
        {
            // Csoportosítás kizárólag Raktárhely alapján
            var raktarCsoportok = adatok.GroupBy(a => a.Raktárhely).OrderBy(g => g.Key).ToList();

            foreach (var csoport in raktarCsoportok)
            {
                string rNev = string.IsNullOrWhiteSpace(csoport.Key) ? "Ismeretlen" : csoport.Key;

                // Lapnév tisztítása
                string lapNév = rNev.Replace("[", "").Replace("]", "").Replace("*", "").Replace("?", "").Replace(":", "").Replace("\\", "").Replace("/", "");
                if (lapNév.Length > 31) lapNév = lapNév.Substring(0, 31);

                // Névütközés elkerülése, ha véletlenül két raktárhely neve levágva megegyezne
                int counter = 1;
                string eredetiNév = lapNév;
                while (workbook.Worksheets.Contains(lapNév))
                {
                    lapNév = eredetiNév.Substring(0, Math.Min(eredetiNév.Length, 28)) + "_" + counter;
                    counter++;
                }

                KészítsEgyRészletesLapot(workbook, lapNév, csoport);
            }
        }

        private void KészítsEgyRészletesLapot(IXLWorkbook workbook, string lapNév, IEnumerable<Adat_Elfekvő> elemek)
        {
            var ws = workbook.Worksheets.Add(lapNév);

            // Fejléc beállítása 
            ws.Cell(1, 1).Value = "Anyag";
            ws.Cell(1, 2).Value = "Anyag rövid szövege";
            ws.Cell(1, 3).Value = "Raktárhely";
            ws.Cell(1, 4).Value = "Sarzs";
            ws.Cell(1, 5).Value = "Szabadon használható";
            ws.Cell(1, 6).Value = "Szab.felh. érték";
            ws.Cell(1, 7).Value = "Utolsó mozgás";

            FormázFejlécet(ws, 1, 7);

            int dSor = 2;
            foreach (var elem in elemek)
            {
                ws.Cell(dSor, 1).Value = elem.Anyag ?? "";
                ws.Cell(dSor, 1).Style.NumberFormat.Format = "@"; // Nullák megtartása

                ws.Cell(dSor, 2).Value = elem.Anyag_rövid_szövege ?? "";
                ws.Cell(dSor, 3).Value = elem.Raktárhely ?? "";

                ws.Cell(dSor, 4).Value = elem.Sarzs ?? "";
                ws.Cell(dSor, 4).Style.NumberFormat.Format = "@";

                // Mennyiség egész számként, tizedesek nélkül
                ws.Cell(dSor, 5).Value = elem.Szabadon_használható;
                ws.Cell(dSor, 5).Style.NumberFormat.Format = "#,##0";

                ws.Cell(dSor, 6).Value = elem.Szab_felh_érték;
                ws.Cell(dSor, 6).Style.NumberFormat.Format = "#,##0 Ft";

                ws.Cell(dSor, 7).Value = elem.Utolsó_mozgás;
                ws.Cell(dSor, 7).Style.NumberFormat.Format = "yyyy.MM.dd";

                dSor++;
            }

            ws.Columns().AdjustToContents();
        }

        #endregion

        #region Üzleti Logika és Számítási Metódusok

        private void KészítsMintaÖsszesítőBlokkokat(IXLWorksheet ws, List<Adat_Elfekvő> adatok, ref int sor)
        {
            var raktarCsoportok = adatok.GroupBy(a => a.Raktárhely).OrderBy(g => g.Key).ToList();

            foreach (var rh in raktarCsoportok)
            {
                string rNév = string.IsNullOrWhiteSpace(rh.Key) ? "Ismeretlen Raktárhely" : rh.Key;

                ws.Cell(sor, 1).Value = rNév;
                ws.Cell(sor, 1).Style.Font.Bold = true;
                sor++;

                ws.Cell(sor, 1).Value = "Készlet érték";
                ws.Cell(sor, 2).Value = "Elfekvő készlet érték";
                ws.Cell(sor, 3).Value = "Elfekvő százalékos értéke";
                ws.Range(sor, 1, sor, 3).Style.Font.Bold = true;
                ws.Range(sor, 1, sor, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                sor++;

                double keszletErtek = rh.Sum(a => a.Szab_felh_érték);
                double elfekvoErtek = SzámolElfekvőÉrték(rh, 365);
                double elfekvoSzazalek = keszletErtek > 0 ? (elfekvoErtek / keszletErtek) : 0;

                ws.Cell(sor, 1).Value = keszletErtek;
                ws.Cell(sor, 1).Style.NumberFormat.Format = "#,##0 Ft";

                ws.Cell(sor, 2).Value = elfekvoErtek;
                ws.Cell(sor, 2).Style.NumberFormat.Format = "#,##0 Ft";

                ws.Cell(sor, 3).Value = elfekvoSzazalek;
                ws.Cell(sor, 3).Style.NumberFormat.Format = "0.00%";

                sor += 3;
            }

            // Globális (Teljes) összesítő blokk
            ws.Cell(sor, 1).Value = "II. Szakszolgálat összesen";
            ws.Cell(sor, 1).Style.Font.Bold = true;
            sor++;

            ws.Cell(sor, 1).Value = "Készlet érték";
            ws.Cell(sor, 2).Value = "Elfekvő készlet érték";
            ws.Cell(sor, 3).Value = "Elfekvő százalékos értéke";
            ws.Range(sor, 1, sor, 3).Style.Font.Bold = true;
            ws.Range(sor, 1, sor, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            sor++;

            double globalKeszlet = adatok.Sum(a => a.Szab_felh_érték);
            double globalElfekvo = SzámolElfekvőÉrték(adatok, 365);
            double globalSzazalek = globalKeszlet > 0 ? (globalElfekvo / globalKeszlet) : 0;

            ws.Cell(sor, 1).Value = globalKeszlet;
            ws.Cell(sor, 1).Style.NumberFormat.Format = "#,##0 Ft";
            ws.Cell(sor, 2).Value = globalElfekvo;
            ws.Cell(sor, 2).Style.NumberFormat.Format = "#,##0 Ft";
            ws.Cell(sor, 3).Value = globalSzazalek;
            ws.Cell(sor, 3).Style.NumberFormat.Format = "0.00%";

            ws.Range(sor, 1, sor, 3).Style.Font.Bold = true;
            ws.Range(sor, 1, sor, 3).Style.Fill.BackgroundColor = XLColor.LightYellow;
            sor++;
        }

        private void KészítsRészletesMátrixot(IXLWorksheet ws, List<Adat_Elfekvő> adatok, ref int sor)
        {
            ws.Cell(sor, 1).Value = "BŐVÍTETT ÖSSZESÍTŐ MÁTRIX";
            ws.Cell(sor, 1).Style.Font.Bold = true;
            ws.Cell(sor, 1).Style.Font.FontSize = 14;
            sor += 2;

            ws.Cell(sor, 1).Value = "Raktárhely";
            ws.Cell(sor, 2).Value = "Cikkszámok száma";
            ws.Cell(sor, 3).Value = "Összes készlet (db)";
            ws.Cell(sor, 4).Value = "Készletérték";
            ws.Cell(sor, 5).Value = "Átlagos cikkérték";
            ws.Cell(sor, 6).Value = "Legrégebbi mozgás";
            ws.Cell(sor, 7).Value = "Legújabb mozgás";
            ws.Cell(sor, 8).Value = "30 napnál régebbi";
            ws.Cell(sor, 9).Value = "90 napnál régebbi";
            ws.Cell(sor, 10).Value = "180 napnál régebbi";
            ws.Cell(sor, 11).Value = "365 napnál régebbi";

            FormázFejlécet(ws, sor, 11);
            sor++;

            var raktarCsoportok = adatok.GroupBy(a => a.Raktárhely).OrderBy(g => g.Key).ToList();

            foreach (var g in raktarCsoportok)
            {
                int cikkSzám = g.Count();
                double osszKeszlet = g.Sum(a => a.Szabadon_használható);
                double keszletErtek = g.Sum(a => a.Szab_felh_érték);
                double atlagErtek = cikkSzám > 0 ? (keszletErtek / cikkSzám) : 0;

                var valosMozgasok = g.Where(a => a.Utolsó_mozgás > ALAP_DATUM).Select(a => a.Utolsó_mozgás).ToList();
                DateTime legrégebbi = valosMozgasok.Any() ? valosMozgasok.Min() : ALAP_DATUM;
                DateTime legújabb = valosMozgasok.Any() ? valosMozgasok.Max() : ALAP_DATUM;

                double r30 = SzámolElfekvőÉrték(g, 30);
                double r90 = SzámolElfekvőÉrték(g, 90);
                double r180 = SzámolElfekvőÉrték(g, 180);
                double r365 = SzámolElfekvőÉrték(g, 365);

                ws.Cell(sor, 1).Value = string.IsNullOrWhiteSpace(g.Key) ? "Ismeretlen" : g.Key;
                ws.Cell(sor, 2).Value = cikkSzám;
                ws.Cell(sor, 3).Value = osszKeszlet;
                ws.Cell(sor, 4).Value = keszletErtek;
                ws.Cell(sor, 5).Value = atlagErtek;

                ws.Cell(sor, 6).Value = legrégebbi;
                ws.Cell(sor, 7).Value = legújabb;

                ws.Cell(sor, 8).Value = r30;
                ws.Cell(sor, 9).Value = r90;
                ws.Cell(sor, 10).Value = r180;
                ws.Cell(sor, 11).Value = r365;

                FormázAdatsort(ws, sor);
                sor++;
            }
        }

        private void KészítsDiagramAdatforrást(IXLWorksheet ws, List<Adat_Elfekvő> adatok, ref int sor)
        {
            ws.Cell(sor, 1).Value = "--- DIAGRAM ADATFORRÁS ---";
            ws.Cell(sor, 1).Style.Font.Italic = true;
            ws.Cell(sor, 1).Style.Font.FontColor = XLColor.DimGray;
            sor += 2;

            ws.Cell(sor, 1).Value = "Raktárhely";
            ws.Cell(sor, 2).Value = "Teljes Készletérték";
            ws.Cell(sor, 3).Value = "365 napos (Elfekvő) érték";
            FormázFejlécet(ws, sor, 3);
            sor++;

            var raktarCsoportok = adatok.GroupBy(a => a.Raktárhely).OrderBy(g => g.Key).ToList();
            foreach (var rh in raktarCsoportok)
            {
                ws.Cell(sor, 1).Value = string.IsNullOrWhiteSpace(rh.Key) ? "Ismeretlen" : rh.Key;
                ws.Cell(sor, 2).Value = rh.Sum(a => a.Szab_felh_érték);
                ws.Cell(sor, 3).Value = SzámolElfekvőÉrték(rh, 365);

                ws.Cell(sor, 2).Style.NumberFormat.Format = "#,##0 Ft";
                ws.Cell(sor, 3).Style.NumberFormat.Format = "#,##0 Ft";
                sor++;
            }
        }

        #endregion

        #region Segédmetódusok és Formázás

        private double SzámolElfekvőÉrték(IEnumerable<Adat_Elfekvő> lista, int napokKuszob)
        {
            return lista.Where(a => ÉletkorNapokban(a.Utolsó_mozgás) > napokKuszob)
                        .Sum(a => a.Szab_felh_érték);
        }

        private double ÉletkorNapokban(DateTime utolsóMozgás)
        {
            if (utolsóMozgás <= ALAP_DATUM) return 9999;
            return (MA - utolsóMozgás).TotalDays;
        }

        private void FormázFejlécet(IXLWorksheet ws, int sor, int maxOszlop)
        {
            var range = ws.Range(sor, 1, sor, maxOszlop);
            range.Style.Font.Bold = true;
            range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            range.Style.Fill.BackgroundColor = XLColor.LightGray;

            // Csak az első soros fejlécekre teszünk szűrőt
            if (sor == 1)
            {
                ws.AutoFilter.Clear();
                range.SetAutoFilter();
                ws.SheetView.FreezeRows(1);
            }
        }

        private void FormázAdatsort(IXLWorksheet ws, int sor)
        {
            ws.Cell(sor, 2).Style.NumberFormat.Format = "#,##0";
            ws.Cell(sor, 3).Style.NumberFormat.Format = "#,##0";
            ws.Cell(sor, 4).Style.NumberFormat.Format = "#,##0 Ft";
            ws.Cell(sor, 5).Style.NumberFormat.Format = "#,##0 Ft";
            ws.Cell(sor, 6).Style.NumberFormat.Format = "yyyy.MM.dd";
            ws.Cell(sor, 7).Style.NumberFormat.Format = "yyyy.MM.dd";
            ws.Cell(sor, 8).Style.NumberFormat.Format = "#,##0 Ft";
            ws.Cell(sor, 9).Style.NumberFormat.Format = "#,##0 Ft";
            ws.Cell(sor, 10).Style.NumberFormat.Format = "#,##0 Ft";
            ws.Cell(sor, 11).Style.NumberFormat.Format = "#,##0 Ft";
        }

        #endregion
    }
}