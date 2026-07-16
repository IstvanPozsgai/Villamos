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

        #region Hierarchia Adatszerkezet

        // Segédosztály a hierarchia tárolására
        private class HelyInfo
        {
            public string Szakszolgalat { get; set; }
            public string Telephely { get; set; }
        }

        // A memóriában tárolt hierarchia fa (könnyen bővíthető új elemekkel)
        private readonly Dictionary<string, HelyInfo> _hierarchia = new Dictionary<string, HelyInfo>(StringComparer.OrdinalIgnoreCase)
        {
            // 1. Szakszolgálat
            { "V180", new HelyInfo { Szakszolgalat = "1. Szakszolgálat", Telephely = "Zugló" } },
            { "V220", new HelyInfo { Szakszolgalat = "1. Szakszolgálat", Telephely = "Száva" } },
            { "V200", new HelyInfo { Szakszolgalat = "1. Szakszolgálat", Telephely = "Hungária" } },
            
            // 2. Szakszolgálat
            { "V130", new HelyInfo { Szakszolgalat = "2. Szakszolgálat", Telephely = "Fogas" } },
            { "V170", new HelyInfo { Szakszolgalat = "2. Szakszolgálat", Telephely = "Angyalföld" } },
            { "V190", new HelyInfo { Szakszolgalat = "2. Szakszolgálat", Telephely = "Baross" } },
            { "V260", new HelyInfo { Szakszolgalat = "2. Szakszolgálat", Telephely = "Szépilona" } },
            
            // 3. Szakszolgálat
            { "V490", new HelyInfo { Szakszolgalat = "3. Szakszolgálat", Telephely = "Kelenföld" } },
            { "V230", new HelyInfo { Szakszolgalat = "3. Szakszolgálat", Telephely = "Ferencváros" } },
            { "V390", new HelyInfo { Szakszolgalat = "3. Szakszolgálat", Telephely = "Budafok" } }
        };

        // Metódus a raktárhelyek automatikus beazonosítására
        private HelyInfo GetHelyInfo(string raktarhely)
        {
            if (string.IsNullOrWhiteSpace(raktarhely))
                return new HelyInfo { Szakszolgalat = "Ismeretlen Szakszolgálat", Telephely = "Ismeretlen Telephely" };

            if (_hierarchia.TryGetValue(raktarhely, out var info))
                return info;

            // Opcionális naplózás ismeretlen raktárhely esetén (a program nem áll le)
            HibaNapló.Log($"Ismeretlen raktárhely azonosítva: '{raktarhely}'. Kérem bővítse a hierarchiát.", "Excel_Elfekvő_Export", "", "", 0);

            return new HelyInfo { Szakszolgalat = "Ismeretlen Szakszolgálat", Telephely = "Ismeretlen Telephely" };
        }

        #endregion

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
                using (var workbook = new XLWorkbook())
                {
                    // LINQ GroupBy segítségével egyszer csoportosítunk Raktárhely alapján a részletes lapokhoz
                    var raktarCsoportok = adatok.GroupBy(a => a.Raktárhely).OrderBy(g => g.Key).ToList();

                    // 1. Összesítő lap (Dashboard) elkészítése a háromszintű hierarchia alapján
                    KészítsDashboard(workbook, adatok);

                    // 2. Részletes lapok automatikus elkészítése Raktárhelyenként
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

        private void KészítsDashboard(IXLWorkbook workbook, List<Adat_Elfekvő> adatok)
        {
            var ws = workbook.Worksheets.Add("Összesítés");
            int sor = 1;

            // Fejléc adatok
            ws.Cell(sor, 1).Value = "Készítés dátuma:";
            ws.Cell(sor, 1).Style.Font.Bold = true;
            ws.Cell(sor, 2).Value = MA;
            ws.Cell(sor, 2).Style.NumberFormat.Format = "yyyy.MM.dd";
            ws.Cell(sor, 2).Style.Font.Bold = true;
            sor += 3;

            // Összesítő táblázat fejléce
            ws.Cell(sor, 1).Value = "Szint / Megnevezés";
            ws.Cell(sor, 2).Value = "Cikkszámok száma";
            ws.Cell(sor, 3).Value = "Összes készlet (db)";
            ws.Cell(sor, 4).Value = "Készlet érték";
            ws.Cell(sor, 5).Value = "Átlagos cikkérték";
            ws.Cell(sor, 6).Value = "Elfekvő készlet érték";
            ws.Cell(sor, 7).Value = "Elfekvő százalék";

            var fejlécTartomány = ws.Range(sor, 1, sor, 7);
            fejlécTartomány.Style.Font.Bold = true;
            fejlécTartomány.Style.Fill.BackgroundColor = XLColor.LightGray;
            fejlécTartomány.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            sor++;

            // Adatok kibővítése a hierarchia infókkal a memóriában
            var hierarchikusAdatok = adatok.Select(a => new {
                Adat = a,
                Hely = GetHelyInfo(a.Raktárhely)
            }).ToList();

            // Csoportosítás Szakszolgálat szerint
            var szakszCsoportok = hierarchikusAdatok.GroupBy(x => x.Hely.Szakszolgalat).OrderBy(g => g.Key).ToList();

            foreach (var szakszGroup in szakszCsoportok)
            {
                // Szakszolgálaton belül Csoportosítás Telephely szerint
                var telephelyCsoportok = szakszGroup.GroupBy(x => x.Hely.Telephely).OrderBy(g => g.Key).ToList();

                foreach (var thGroup in telephelyCsoportok)
                {
                    // 1. Szint: Telephely szintű összesítés
                    var thAdatok = thGroup.Select(x => x.Adat).ToList();
                    KiirOsszesitoSor(ws, ref sor, thGroup.Key, thAdatok, behuzas: 2);
                }

                // 2. Szint: Szakszolgálat szintű összesítés
                var szakszAdatok = szakszGroup.Select(x => x.Adat).ToList();
                KiirOsszesitoSor(ws, ref sor, $"{szakszGroup.Key.ToUpper()} ÖSSZESEN", szakszAdatok, behuzas: 0, felkover: true);

                sor++; // Üres sor az átláthatóságért a szakszolgálatok között
            }

            // 3. Szint: Teljes Összesítés (Grand Total)
            KiirOsszesitoSor(ws, ref sor, "TELJES ÁLLOMÁNY ÖSSZESEN", adatok, behuzas: 0, felkover: true, hatter: true);

            // Lap formázása (oszlopszélességek)
            ws.Columns().AdjustToContents();
            foreach (var col in ws.ColumnsUsed()) col.Width += 3.0; // Biztonsági szélesítés a ###### ellen
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

        /// <summary>
        /// Közös számoló metódus, amely bármilyen (Telephely, Szakszolgálat, Teljes) rekordlistára meghívható
        /// és azonos struktúrában írja ki az összesítéseket a Dashboardra.
        /// </summary>
        private void KiirOsszesitoSor(IXLWorksheet ws, ref int sor, string megnevezes, IEnumerable<Adat_Elfekvő> adatok, int behuzas = 0, bool felkover = false, bool hatter = false)
        {
            // Közös számítások
            int cikkSzam = adatok.Count();
            double osszKeszlet = adatok.Sum(a => a.Szabadon_használható);
            double keszletErtek = adatok.Sum(a => a.Szab_felh_érték);
            double atlagErtek = cikkSzam > 0 ? (keszletErtek / cikkSzam) : 0;

            // 365 napnál régebbi, vagy 1900.01.01 dátumú (sosem mozgott) tételek
            double elfekvoErtek = adatok.Where(a =>
            {
                if (a.Utolsó_mozgás <= ALAP_DATUM) return true;
                return (MA - a.Utolsó_mozgás).TotalDays > 365;
            }).Sum(a => a.Szab_felh_érték);

            double elfekvoSzazalek = keszletErtek > 0 ? (elfekvoErtek / keszletErtek) : 0;

            // Cellák kitöltése
            ws.Cell(sor, 1).Value = megnevezes;
            if (behuzas > 0) ws.Cell(sor, 1).Style.Alignment.Indent = behuzas; // Strukturált megjelenés (behúzás)

            ws.Cell(sor, 2).Value = cikkSzam;
            ws.Cell(sor, 3).Value = osszKeszlet;
            ws.Cell(sor, 4).Value = keszletErtek;
            ws.Cell(sor, 5).Value = atlagErtek;
            ws.Cell(sor, 6).Value = elfekvoErtek;
            ws.Cell(sor, 7).Value = elfekvoSzazalek;

            // Formázások alkalmazása a sorra
            ws.Cell(sor, 2).Style.NumberFormat.Format = "#,##0";
            ws.Cell(sor, 3).Style.NumberFormat.Format = "#,##0";
            ws.Cell(sor, 4).Style.NumberFormat.Format = "#,##0 Ft";
            ws.Cell(sor, 5).Style.NumberFormat.Format = "#,##0 Ft";
            ws.Cell(sor, 6).Style.NumberFormat.Format = "#,##0 Ft";
            ws.Cell(sor, 7).Style.NumberFormat.Format = "0.00%";

            if (felkover) ws.Range(sor, 1, sor, 7).Style.Font.Bold = true;
            if (hatter) ws.Range(sor, 1, sor, 7).Style.Fill.BackgroundColor = XLColor.LightYellow;

            sor++;
        }

        #endregion
    }
}