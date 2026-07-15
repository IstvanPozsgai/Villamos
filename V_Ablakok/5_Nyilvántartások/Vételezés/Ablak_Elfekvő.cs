using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Ablakok.Közös;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos.V_Ablakok._4_Nyilvántartások.Vételezés
{
    public partial class Ablak_Elfekvő : Form
    {
        readonly Kezelő_Elfekvő KézElfekvő = new Kezelő_Elfekvő();

        public Ablak_Elfekvő()
        {
            InitializeComponent();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Holtart.Lép();
        }

        private async void Btn_AdatFeldolgozás_Click(object sender, EventArgs e)
        {
            string fájl_MB52 = FájlMegnyitás("Válaszd ki az AKTUÁLIS RAKTÁRKÉSZLET (MB52) Excel fájlt");
            if (string.IsNullOrEmpty(fájl_MB52)) return;

            string fájl_MB51 = FájlMegnyitás("Válaszd ki az ANYAGMOZGÁSOK (MB51) Excel fájlt");
            if (string.IsNullOrEmpty(fájl_MB51)) return;

            Holtart.Be();
            timer1.Enabled = true;

            try
            {
                await Task.Run(() => FeldolgozÉsMent(fájl_MB52, fájl_MB51));

                MessageBox.Show("Az adatok feldolgozása és az SQLite adatbázisba történő rögzítése sikeresen befejeződött!",
                                "Sikeres művelet", MessageBoxButtons.OK, MessageBoxIcon.Information);

                TáblaÍró();
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba rögzítésre került a naplóban.", "Hiba történt", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                timer1.Enabled = false;
                Holtart.Ki();
            }
        }

        private string FájlMegnyitás(string ablakCím)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = "MyDocuments";
                ofd.Title = ablakCím;
                ofd.Filter = "Excel fájlok |*.xlsx;*.xls";
                if (ofd.ShowDialog() == DialogResult.OK)
                    return ofd.FileName;
            }
            return string.Empty;
        }

        private Dictionary<string, int> GetFejlecIndexek(ClosedXML.Excel.IXLWorksheet ws)
        {
            var indexek = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            var elsoSor = ws.FirstRowUsed();
            if (elsoSor != null)
            {
                int maxOszlop = elsoSor.LastCellUsed().Address.ColumnNumber;
                for (int i = 1; i <= maxOszlop; i++)
                {
                    string fejlecNev = elsoSor.Cell(i).GetString().Trim();
                    if (!string.IsNullOrEmpty(fejlecNev) && !indexek.ContainsKey(fejlecNev))
                    {
                        indexek.Add(fejlecNev, i);
                    }
                }
            }
            return indexek;
        }

        private void FeldolgozÉsMent(string fájl_MB52, string fájl_MB51)
        {
            Dictionary<string, DateTime> mozgásokKereső = new Dictionary<string, DateTime>();
            Dictionary<string, string> megnevezésekEllenőrző = new Dictionary<string, string>();

            // MB51 mozgások beolvasása dinamikus fejléc-indexek alapján
            using (var wbMB51 = new ClosedXML.Excel.XLWorkbook(fájl_MB51))
            {
                var ws = wbMB51.Worksheet(1);
                var indexekMB51 = GetFejlecIndexek(ws);

                if (!indexekMB51.ContainsKey("Anyag") || !indexekMB51.ContainsKey("Sarzs"))
                    throw new HibásBevittAdat("Az MB51 fájl nem tartalmazza az 'Anyag' vagy 'Sarzs' oszlopot!");

                int idxAnyag = indexekMB51["Anyag"];
                int idxSarzs = indexekMB51["Sarzs"];

                int idxDatum = indexekMB51.ContainsKey("Könyvelési dátum") ? indexekMB51["Könyvelési dátum"] :
                               (indexekMB51.ContainsKey("Könyvelés dátuma") ? indexekMB51["Könyvelés dátuma"] : -1);

                if (idxDatum == -1)
                    throw new HibásBevittAdat("Az MB51 fájl nem tartalmazza a 'Könyvelési dátum' oszlopot!");

                int idxMegnMB51 = indexekMB51.ContainsKey("Anyag rövid szövege") ? indexekMB51["Anyag rövid szövege"] : -1;

                var sorok = ws.RowsUsed().Skip(1);

                foreach (var sor in sorok)
                {
                    string cikkszám = sor.Cell(idxAnyag).GetString().Trim();
                    string sarzs = sor.Cell(idxSarzs).GetString().Trim();

                    if (string.IsNullOrWhiteSpace(cikkszám) || string.IsNullOrWhiteSpace(sarzs))
                        continue;

                    string kulcs = $"{cikkszám}|{sarzs}";

                    if (idxMegnMB51 != -1)
                    {
                        string megn = sor.Cell(idxMegnMB51).GetString().Trim();
                        if (!string.IsNullOrEmpty(megn) && !megnevezésekEllenőrző.ContainsKey(kulcs))
                            megnevezésekEllenőrző[kulcs] = megn;
                    }

                    DateTime mozgásDátum;
                    bool sikeresDátum = false;

                    if (sor.Cell(idxDatum).TryGetValue(out mozgásDátum))
                    {
                        sikeresDátum = true;
                    }
                    else
                    {
                        string dátumSzöveg = sor.Cell(idxDatum).GetString().Trim();
                        if (DateTime.TryParse(dátumSzöveg, out mozgásDátum))
                            sikeresDátum = true;
                    }

                    if (sikeresDátum)
                    {
                        if (mozgásokKereső.TryGetValue(kulcs, out DateTime eddigiUtolsó))
                        {
                            if (mozgásDátum > eddigiUtolsó)
                                mozgásokKereső[kulcs] = mozgásDátum;
                        }
                        else
                        {
                            mozgásokKereső.Add(kulcs, mozgásDátum);
                        }
                    }
                }
            }

            //MB52 raktárkészlet beolvasása dinamikus fejléc-indexek alapján
            List<Adat_Elfekvő> elfekvőLista = new List<Adat_Elfekvő>();

            using (var wbMB52 = new ClosedXML.Excel.XLWorkbook(fájl_MB52))
            {
                var ws = wbMB52.Worksheet(1);
                var indexekMB52 = GetFejlecIndexek(ws);

                string[] szuksegesOszlopok = { "Anyag", "Anyag rövid szövege", "Raktárhely", "Szabadon használható", "Szab.felh. érték", "Sarzs" };
                foreach (var oszlop in szuksegesOszlopok)
                {
                    if (!indexekMB52.ContainsKey(oszlop))
                        throw new HibásBevittAdat($"Az MB52 fájl nem tartalmazza a(z) '{oszlop}' oszlopot!");
                }

                int idxAnyag = indexekMB52["Anyag"];
                int idxMegnevezes = indexekMB52["Anyag rövid szövege"];
                int idxRaktarhely = indexekMB52["Raktárhely"];
                int idxSzabadon = indexekMB52["Szabadon használható"];
                int idxErtek = indexekMB52["Szab.felh. érték"];
                int idxSarzs = indexekMB52["Sarzs"];

                var sorok = ws.RowsUsed().Skip(1);

                foreach (var sor in sorok)
                {
                    string cikkszám = sor.Cell(idxAnyag).GetString().Trim();
                    string sarzs = sor.Cell(idxSarzs).GetString().Trim();

                    if (string.IsNullOrWhiteSpace(cikkszám) || string.IsNullOrWhiteSpace(sarzs))
                        continue;

                    string megnevezés = sor.Cell(idxMegnevezes).GetString().Trim();
                    string raktárhely = sor.Cell(idxRaktarhely).GetString().Trim();

                    double mennyiség = 0;
                    if (!sor.Cell(idxSzabadon).TryGetValue(out mennyiség))
                    {
                        double.TryParse(sor.Cell(idxSzabadon).GetString().Trim(), out mennyiség);
                    }

                    double érték = 0;
                    if (!sor.Cell(idxErtek).TryGetValue(out érték))
                    {
                        double.TryParse(sor.Cell(idxErtek).GetString().Trim(), out érték);
                    }

                    string kulcs = $"{cikkszám}|{sarzs}";

                    if (megnevezésekEllenőrző.TryGetValue(kulcs, out string mentettMegn))
                    {
                        if (!string.Equals(mentettMegn, megnevezés, StringComparison.OrdinalIgnoreCase))
                        {
                            HibaNapló.Log($"Figyelmeztetés: Eltérő megnevezés a(z) {kulcs} kulcshoz. MB51: '{mentettMegn}', MB52: '{megnevezés}'", "Ablak_Elfekvő", "", "", 0);
                        }
                    }

                    DateTime utolsóMozgásDatuma = new DateTime(1900, 1, 1);
                    if (mozgásokKereső.TryGetValue(kulcs, out DateTime megtaláltDátum))
                        utolsóMozgásDatuma = megtaláltDátum;

                    elfekvőLista.Add(new Adat_Elfekvő(
                        0,
                        cikkszám,
                        megnevezés,
                        raktárhely,
                        mennyiség,
                        érték,
                        sarzs,
                        utolsóMozgásDatuma
                    ));
                }
            }

            if (elfekvőLista.Count > 0)
            {
                KézElfekvő.Tábla_Kiürítés();
                KézElfekvő.Tömeges_Rögzítés(elfekvőLista);
            }
        }

        private void Ablak_Elfekvő_Load(object sender, EventArgs e)
        {
            TáblaÍró();
        }

        private void TáblaÍró()
        {
            try
            {
                List<Adat_Elfekvő> adatok = KézElfekvő.Lista_Adatok();

                Tábla.DataSource = null;
                Tábla.DataSource = adatok;

                OszlopokFormázása();
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
            }
        }

        private void OszlopokFormázása()
        {
            if (Tábla.Columns["Id"] != null) Tábla.Columns["Id"].Visible = false;

            if (Tábla.Columns["Anyag"] != null) { Tábla.Columns["Anyag"].HeaderText = "Anyag"; Tábla.Columns["Anyag"].Width = 130; }
            if (Tábla.Columns["Anyag_rövid_szövege"] != null) { Tábla.Columns["Anyag_rövid_szövege"].HeaderText = "Anyag rövid szövege"; Tábla.Columns["Anyag_rövid_szövege"].Width = 350; }
            if (Tábla.Columns["Raktárhely"] != null) { Tábla.Columns["Raktárhely"].HeaderText = "Raktárhely"; Tábla.Columns["Raktárhely"].Width = 100; }
            if (Tábla.Columns["Szabadon_használható"] != null) { Tábla.Columns["Szabadon_használható"].HeaderText = "Szabadon használható"; Tábla.Columns["Szabadon_használható"].Width = 150; }
            if (Tábla.Columns["Szab_felh_érték"] != null) { Tábla.Columns["Szab_felh_érték"].HeaderText = "Szab.felh. érték"; Tábla.Columns["Szab_felh_érték"].Width = 150; }
            if (Tábla.Columns["Sarzs"] != null) { Tábla.Columns["Sarzs"].HeaderText = "Sarzs"; Tábla.Columns["Sarzs"].Width = 80; }
            if (Tábla.Columns["Utolsó_mozgás"] != null) { Tábla.Columns["Utolsó_mozgás"].HeaderText = "Utolsó mozgás"; Tábla.Columns["Utolsó_mozgás"].Width = 130; }
        }

        private void Btn_Frissit_Click(object sender, EventArgs e)
        {
            TáblaÍró();
        }

        private void Btn_ExcelExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Elfekvő készlet mentése Excel fájlba";
                sfd.Filter = "Excel fájlok |*.xlsx";
                sfd.FileName = $"Elfekvo_Keszlet_{DateTime.Now:yyyyMMdd}";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Excel_Elfekvő_Export exporter = new Excel_Elfekvő_Export();

                    exporter.Export(sfd.FileName);

                    MessageBox.Show("Az Excel fájl sikeresen legenerálásra került!", "Sikeres export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}