using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Excel_Elfekvő_Export
    {
        public void Export(string fájlnév)
        {
            try
            {
                Kezelő_Elfekvő kézElfekvő = new Kezelő_Elfekvő();
                List<Adat_Elfekvő> adatok = kézElfekvő.Lista_Adatok();

                if (adatok == null || adatok.Count == 0)
                {
                    throw new HibásBevittAdat("Nincs exportálható adat az adatbázisban!");
                }

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
                using (var workbook = new XLWorkbook())
                {
                    // Az elfekvő készlet küszöbértéke (1 évnél régebbi mozgások)
                    DateTime elfekvoKuszob = DateTime.Today.AddYears(-1);

                    var raktarCsoportok = adatok.GroupBy(a => a.Raktárhely).OrderBy(g => g.Key).ToList();

                    // ==========================================
                    // 1. LÉPÉS: "Összesítés" munkalap kiértékeléssel
                    // ==========================================
                    var wsOsszesito = workbook.Worksheets.Add("Összesítés");

                    // Fejlécek beállítása az elvárt kiértékelési oszlopokkal
                    wsOsszesito.Cell(1, 1).Value = "Raktárhely";
                    wsOsszesito.Cell(1, 2).Value = "Cikkszámok száma";
                    wsOsszesito.Cell(1, 3).Value = "Készlet érték összesen";
                    wsOsszesito.Cell(1, 4).Value = "Darabszám összesen";
                    wsOsszesito.Cell(1, 5).Value = "Elfekvő készlet érték";
                    wsOsszesito.Cell(1, 6).Value = "Elfekvő százalékos értéke";
                    wsOsszesito.Cell(1, 7).Value = "Legrégebbi utolsó mozgás";
                    wsOsszesito.Cell(1, 8).Value = "Legújabb utolsó mozgás";

                    int osszSorIndex = 2;
                    foreach (var csoport in raktarCsoportok)
                    {
                        string raktarhely = string.IsNullOrWhiteSpace(csoport.Key) ? "Ismeretlen" : csoport.Key;
                        int cikkszamokSzama = csoport.Count();
                        double keszletErtek = csoport.Sum(a => a.Szab_felh_érték);
                        double darabszam = csoport.Sum(a => a.Szabadon_használható);

                        // Elfekvő számítás: ha régebbi mint 1 év, VAGY ha 1900.01.01 (sosem mozdult meg)
                        double elfekvoErtek = csoport
                            .Where(a => a.Utolsó_mozgás <= elfekvoKuszob || a.Utolsó_mozgás == new DateTime(1900, 1, 1))
                            .Sum(a => a.Szab_felh_érték);

                        double elfekvoSzazalek = keszletErtek > 0 ? (elfekvoErtek / keszletErtek) : 0;

                        // Dátum statisztikák (kihagyva az alapértelmezett 1900-as dátumokat a torzítás elkerülésére)
                        var valosMozgasok = csoport.Select(a => a.Utolsó_mozgás).Where(d => d > new DateTime(1900, 1, 1)).ToList();
                        DateTime legregebbi = valosMozgasok.Any() ? valosMozgasok.Min() : new DateTime(1900, 1, 1);
                        DateTime legujabb = valosMozgasok.Any() ? valosMozgasok.Max() : new DateTime(1900, 1, 1);

                        wsOsszesito.Cell(osszSorIndex, 1).Value = raktarhely;
                        wsOsszesito.Cell(osszSorIndex, 2).Value = cikkszamokSzama;
                        wsOsszesito.Cell(osszSorIndex, 3).Value = keszletErtek;
                        wsOsszesito.Cell(osszSorIndex, 4).Value = darabszam;
                        wsOsszesito.Cell(osszSorIndex, 5).Value = elfekvoErtek;
                        wsOsszesito.Cell(osszSorIndex, 6).Value = elfekvoSzazalek;
                        wsOsszesito.Cell(osszSorIndex, 7).Value = legregebbi;
                        wsOsszesito.Cell(osszSorIndex, 8).Value = legujabb;

                        osszSorIndex++;
                    }

                    // GLOBÁLIS ÖSSZESÍTŐ SOR (II. Szakszolgálat szinten)
                    double globalKeszletErtek = adatok.Sum(a => a.Szab_felh_érték);
                    double globalElfekvoErtek = adatok
                        .Where(a => a.Utolsó_mozgás <= elfekvoKuszob || a.Utolsó_mozgás == new DateTime(1900, 1, 1))
                        .Sum(a => a.Szab_felh_érték);

                    var globalValosMozgasok = adatok.Select(a => a.Utolsó_mozgás).Where(d => d > new DateTime(1900, 1, 1)).ToList();

                    var globalSor = wsOsszesito.Row(osszSorIndex);
                    globalSor.Cell(1).Value = "II. Szakszolgálat összesen";
                    globalSor.Cell(2).Value = adatok.Count;
                    globalSor.Cell(3).Value = globalKeszletErtek;
                    globalSor.Cell(4).Value = adatok.Sum(a => a.Szabadon_használható);
                    globalSor.Cell(5).Value = globalElfekvoErtek;
                    globalSor.Cell(6).Value = globalKeszletErtek > 0 ? (globalElfekvoErtek / globalKeszletErtek) : 0;
                    globalSor.Cell(7).Value = globalValosMozgasok.Any() ? globalValosMozgasok.Min() : new DateTime(1900, 1, 1);
                    globalSor.Cell(8).Value = globalValosMozgasok.Any() ? globalValosMozgasok.Max() : new DateTime(1900, 1, 1);

                    // Összesen sor kiemelése félkövér stílussal és alsó dupla vonallal
                    globalSor.Style.Font.Bold = true;
                    globalSor.Style.Border.BottomBorder = XLBorderStyleValues.Double;
                    globalSor.Style.Border.TopBorder = XLBorderStyleValues.Thin;

                    // Összesítő lap formázása oszlopszinten
                    FormazzFejlecet(wsOsszesito, 8);
                    wsOsszesito.Column(2).Style.NumberFormat.Format = "#,##0";
                    wsOsszesito.Column(3).Style.NumberFormat.Format = "#,##0\" Ft\"";
                    wsOsszesito.Column(4).Style.NumberFormat.Format = "#,##0.000";
                    wsOsszesito.Column(5).Style.NumberFormat.Format = "#,##0\" Ft\"";
                    wsOsszesito.Column(6).Style.NumberFormat.Format = "0.0%";
                    wsOsszesito.Column(7).Style.NumberFormat.Format = "yyyy.MM.dd";
                    wsOsszesito.Column(8).Style.NumberFormat.Format = "yyyy.MM.dd";
                    wsOsszesito.Columns(1, 8).AdjustToContents();

                    // ==========================================
                    // 2. LÉPÉS: Dinamikus Raktárhely munkalapok
                    // ==========================================
                    foreach (var csoport in raktarCsoportok)
                    {
                        string lapNev = string.IsNullOrWhiteSpace(csoport.Key) ? "Ismeretlen" : csoport.Key;
                        lapNev = string.Concat(lapNev.Split(Path.GetInvalidFileNameChars())).Trim();
                        if (lapNev.Length > 31) lapNev = lapNev.Substring(0, 31);

                        var wsRaktar = workbook.Worksheets.Add(lapNev);

                        wsRaktar.Cell(1, 1).Value = "Anyag";
                        wsRaktar.Cell(1, 2).Value = "Anyag rövid szövege";
                        wsRaktar.Cell(1, 3).Value = "Raktárhely";
                        wsRaktar.Cell(1, 4).Value = "Sarzs";
                        wsRaktar.Cell(1, 5).Value = "Szabadon használható";
                        wsRaktar.Cell(1, 6).Value = "Szab.felh. érték";
                        wsRaktar.Cell(1, 7).Value = "Utolsó mozgás";

                        int raktarSorIndex = 2;
                        foreach (var elem in csoport)
                        {
                            var cellaAnyag = wsRaktar.Cell(raktarSorIndex, 1);
                            cellaAnyag.Style.NumberFormat.Format = "@";
                            cellaAnyag.Value = elem.Anyag ?? "";

                            wsRaktar.Cell(raktarSorIndex, 2).Value = elem.Anyag_rövid_szövege ?? "";
                            wsRaktar.Cell(raktarSorIndex, 3).Value = elem.Raktárhely ?? "";

                            var cellaSarzs = wsRaktar.Cell(raktarSorIndex, 4);
                            cellaSarzs.Style.NumberFormat.Format = "@";
                            cellaSarzs.Value = elem.Sarzs ?? "";

                            wsRaktar.Cell(raktarSorIndex, 5).Value = elem.Szabadon_használható;
                            wsRaktar.Cell(raktarSorIndex, 6).Value = elem.Szab_felh_érték;
                            wsRaktar.Cell(raktarSorIndex, 7).Value = elem.Utolsó_mozgás;

                            raktarSorIndex++;
                        }

                        FormazzFejlecet(wsRaktar, 7);
                        wsRaktar.Column(1).Style.NumberFormat.Format = "@";
                        wsRaktar.Column(4).Style.NumberFormat.Format = "@";
                        wsRaktar.Column(5).Style.NumberFormat.Format = "#,##0.000";
                        wsRaktar.Column(6).Style.NumberFormat.Format = "#,##0\" Ft\"";
                        wsRaktar.Column(7).Style.NumberFormat.Format = "yyyy.MM.dd";

                        wsRaktar.Columns(1, 7).AdjustToContents();
                    }

                    workbook.SaveAs(fájlnév);
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                throw;
            }
        }

        private void FormazzFejlecet(IXLWorksheet ws, int oszlopSzam)
        {
            var fejlecTartomany = ws.Range(1, 1, 1, oszlopSzam);
            fejlecTartomany.Style.Font.Bold = true;
            fejlecTartomany.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            fejlecTartomany.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            fejlecTartomany.Style.Fill.BackgroundColor = XLColor.LightGray;

            ws.AutoFilter.Clear();
            fejlecTartomany.SetAutoFilter();
            ws.SheetView.FreezeRows(1);
        }
    }
}