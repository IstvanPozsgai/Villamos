using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Ablakok._4_Nyilvántartások.Kerékeszterga;
using Villamos.Villamos_Ablakok._5_Karbantartás.Eszterga_Karbantartás;
using Villamos.Villamos_Adatszerkezet;
using Villamos.Villamos_Kezelők;
using Application = System.Windows.Forms.Application;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos.V_Ablakok._4_Nyilvántartások.Eszterga_Karbantartás
{
    public delegate void Event_Kidobo();
    public partial class Ablak_Eszterga_Karbantartás_Üzemóra : Form
    {
        #region Osztályszintű elemek
        readonly private DataTable AdatTabla = new DataTable();
        public event Event_Kidobo Eszterga_Valtozas;
        // JAVÍTANDÓ:?
        readonly bool Baross = Program.PostásTelephely.Trim() == "Angyalföld";
        #endregion

        #region Listák
        List<Adat_Eszterga_Muveletek> AdatokMuvelet = new List<Adat_Eszterga_Muveletek>();
        List<Adat_Eszterga_Uzemora> AdatokUzemora = new List<Adat_Eszterga_Uzemora>();
        #endregion

        #region Kezelők
        // JAVÍTANDÓ:ha nem kell akkor minek?
        //kesz
        readonly Kezelo_Eszterga_Muveletek Kez_Muvelet = new Kezelo_Eszterga_Muveletek();
        readonly Kezelo_Eszterga_Uzemora Kez_Uzemora = new Kezelo_Eszterga_Uzemora();
        #endregion

        #region Alap

        /// <summary>
        /// Inicializálja az Eszterga üzemóra nyilvántartó ablak komponenseit.
        /// </summary>
        public Ablak_Eszterga_Karbantartás_Üzemóra()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Az ablak betöltésekor feltölti az üzemóra adatokat, beállítja a jogosultságokat,
        /// és regisztrálja az eseményt a cellák formázásához.
        /// </summary>
        private void Ablak_Eszterga_Karbantartás_Üzemóra_Load(object sender, EventArgs e)
        {
            TablaListazas();
            Jogosultsagkiosztas();
            Tabla.CellFormatting += Tábla_CellFormatting;
        }

        /// <summary>
        /// Jogosultság alapján engedélyezi vagy tiltja a felhasználó számára a műveletek (új, módosít, Excel export) elérhetőségét.
        /// </summary>
        private void Jogosultsagkiosztas()
        {
            try
            {
                int melyikelem = 160;
                Btn_Módosít.Visible = Baross;

                // módosítás 1 
                //Ablak_Eszterga_Karbantartás_Segéd oldal használja az 1. módosításokat
                Btn_Excel.Enabled = MyF.Vanjoga(melyikelem, 1);

                // módosítás 2
                //Ablak_Eszterga_Karbantartás oldal használja a 2. módosításokat
                Btn_Módosít.Enabled = MyF.Vanjoga(melyikelem, 2);
                Btn_ÚjFelvétel.Enabled = MyF.Vanjoga(melyikelem, 2);

                // módosítás 3 
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Lista

        /// <summary>
        /// Feltölti az üzemóra rekordokat a táblázatba, beállítja az oszlopokat és frissíti a megjelenést.
        /// </summary>
        private void TablaListazas()
        {
            try
            {
                AdatTabla.Columns.Clear();
                AdatTabla.Rows.Clear();
                AdatTabla.Columns.Add("ID");
                AdatTabla.Columns.Add("Üzemóra");
                AdatTabla.Columns.Add("Dátum");
                AdatTabla.Columns.Add("Státusz");

                AdatokUzemora = Kez_Uzemora.Lista_Adatok();

                AdatTabla.Rows.Clear();

                foreach (Adat_Eszterga_Uzemora rekord in AdatokUzemora)
                {
                    DataRow Soradat = AdatTabla.NewRow();
                    Soradat["ID"] = rekord.ID;
                    Soradat["Üzemóra"] = rekord.Uzemora;
                    Soradat["Dátum"] = rekord.Dátum.ToShortDateString();
                    Soradat["Státusz"] = rekord.Státus ? "Törölt" : "Aktív";

                    AdatTabla.Rows.Add(Soradat);
                }

                Tabla.DataSource = AdatTabla;
                OszlopSzelesseg();
                Tabla.Visible = true;
                Tabla.ClearSelection();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Beállítja az oszlopok szélességét a táblázatban az átlátható megjelenítés érdekében.
        /// </summary>
        private void OszlopSzelesseg()
        {
            Tabla.Columns["ID"].Width = 60;
            Tabla.Columns["Üzemóra"].Width = 172;
            Tabla.Columns["Dátum"].Width = 120;
            Tabla.Columns["Státusz"].Width = 100;
        }

        /// <summary>
        /// A törölt sorokat piros háttérrel és áthúzott betűstílussal jeleníti meg.
        /// Minden más sor fehér háttérrel és normál stílussal formázódik.
        /// </summary>
        private void Tábla_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (Tabla.Columns[e.ColumnIndex].Name == "Státusz" && e.Value is string státusz)
                {
                    DataGridViewRow sor = Tabla.Rows[e.RowIndex];
                    if (státusz == "Törölt")
                    {
                        sor.DefaultCellStyle.BackColor = Color.IndianRed;
                        sor.DefaultCellStyle.ForeColor = Color.Black;
                        sor.DefaultCellStyle.Font = new System.Drawing.Font(Tabla.DefaultCellStyle.Font, FontStyle.Strikeout);
                    }
                    else
                    {
                        sor.DefaultCellStyle.BackColor = Color.White;
                        sor.DefaultCellStyle.ForeColor = Color.Black;
                        sor.DefaultCellStyle.Font = new System.Drawing.Font(Tabla.DefaultCellStyle.Font, FontStyle.Regular);
                    }
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Sor kijelölésekor betölti annak adatait a szerkesztőmezőkbe (üzemóra, dátum, státusz).
        /// </summary>
        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = Tabla.Rows[e.RowIndex];
                    TxtBxÜzem.Text = row.Cells[1].Value.ToStrTrim();
                    DtmPckrDátum.Value = row.Cells[2].Value.ToÉrt_DaTeTime();
                    ChckBxStátus.Checked = row.Cells[3].Value.ToStrTrim() == "Törölt";
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Metodusok, Gombok

        /// <summary>
        /// Előkészíti az űrlapot egy új üzemóra adat rögzítéséhez: törli a mezők értékeit, beállítja a mai dátumot.
        /// </summary>
        private void Btn_ÚjFelvétel_Click(object sender, EventArgs e)
        {
            Tabla.ClearSelection();
            TxtBxÜzem.Text = string.Empty;
            TxtBxÜzem.Focus();
            DtmPckrDátum.Value = DateTime.Today;
            ChckBxStátus.Checked = false;
        }

        /// <summary>
        /// A kijelölt sor adatait módosítja vagy új rekordot hoz létre, ha nincs kiválasztott sor.
        /// Előtte érvényesíti az adatokat, majd a változásokat adatbázisba menti, és frissíti a táblát.
        /// </summary>
        private void Btn_Módosít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tabla.SelectedRows.Count > 1)
                    throw new HibásBevittAdat("Egyszerre csak 1 sort lehet módosítani");

                long UjUzemora = TxtBxÜzem.Text.ToÉrt_Long();
                DateTime UjDatum = DtmPckrDátum.Value.Date;
                bool UjStatus = ChckBxStátus.Checked;

                AdatokUzemora = Kez_Uzemora.Lista_Adatok();

                if (!DatumEllenorzes(UjDatum)) return;

                if (Tabla.SelectedRows.Count == 0)
                { if (!UjRekordHozzaadasa(UjDatum, UjUzemora, UjStatus)) return; }

                else
                { if (MeglevoRekordModositasa(UjDatum, UjUzemora, UjStatus)) return; }
                TablaListazas();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// A táblázat tartalmát Excel fájlba exportálja, majd automatikusan megnyitja a fájlt.
        /// A felhasználó kiválaszthatja a fájl mentési helyét és nevét.
        /// </summary>
        private void Btn_Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tabla.Rows.Count <= 0) throw new HibásBevittAdat("Nincs sora a táblázatnak!");
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Teljes tartalom mentése Excel fájlba",
                    FileName = $"Eszterga_Karbantartás_Üzemórák_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;
                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);

                MyE.EXCELtábla(fájlexc, Tabla, false, true);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MyE.Megnyitás($"{fájlexc}.xlsx");
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Eseménykezelő, amely PDF fájlba exportálja a megjelenített műveleti táblázatot.
        /// Ellenőrzi, hogy van-e adat, majd mentési helyet kér a felhasználótól, 
        /// és meghívja a PDF létrehozó metódust. Sikeres mentés után megnyitja a PDF-et.
        /// </summary>
        private void Btn_Pdf_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tabla.Rows.Count <= 0)
                    throw new HibásBevittAdat("Nincs sora a táblázatnak!");

                SaveFileDialog saveDlg = new SaveFileDialog
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    Title = "Mentés PDF fájlba",
                    FileName = $"Eszterga_Karbantartás_Üzemórák_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "PDF fájl (*.pdf)|*.pdf"
                };

                if (saveDlg.ShowDialog() != DialogResult.OK)
                    return;

                string fajlNev = saveDlg.FileName;
                if (!fajlNev.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                    fajlNev += ".pdf";

                PDFtábla(fajlNev, Tabla);

                MessageBox.Show($"Elkészült a PDF fájl:\n{fajlNev}", "Sikeres mentés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyE.Megnyitás(fajlNev);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\nA hiba naplózásra került.", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Egy adott DataGridView tartalmát exportálja PDF formátumba, megtartva a cellák háttér- és szövegszínét.
        /// Unicode-kompatibilis betűtípussal dolgozik, és Arial-t használ a PDF generálásához.
        /// </summary>
        private void PDFtábla(string fájlNév, DataGridView tábla)
        {
            try
            {
                using (FileStream stream = new FileStream(fájlNév, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4.Rotate(), 10f, 10f, 20f, 20f);
                    PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    // Betűtípus betöltése (Arial, Unicode támogatás)
                    string betutipusUt = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                    BaseFont alapFont = BaseFont.CreateFont(betutipusUt, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                    // Fejléc betűtípus - fekete, vastag
                    iTextSharp.text.Font fejlecBetu = new iTextSharp.text.Font(alapFont, 10f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                    PdfPTable pdfTable = new PdfPTable(tábla.Columns.Count)
                    {
                        WidthPercentage = 100
                    };

                    // Fejléc hozzáadása, egységes fekete háttérrel (vagy tetszőleges színnel)
                    foreach (DataGridViewColumn column in tábla.Columns)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, fejlecBetu))
                        {
                            BackgroundColor = new BaseColor(240, 240, 240)
                        };
                        pdfTable.AddCell(cell);
                    }

                    // Sorok bejárása
                    foreach (DataGridViewRow row in tábla.Rows)
                    {
                        if (row.IsNewRow) continue;

                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            string szoveg = cell.Value?.ToString() ?? "";

                            // Színek lekérése az InheritedStyle-ból (ez tartalmazza a tényleges megjelenő színt)
                            BaseColor háttérSzín = cell.InheritedStyle.BackColor.IsEmpty
                                ? BaseColor.WHITE
                                : new BaseColor(cell.InheritedStyle.BackColor.R, cell.InheritedStyle.BackColor.G, cell.InheritedStyle.BackColor.B);

                            BaseColor szovegSzín = cell.InheritedStyle.ForeColor.IsEmpty
                                ? BaseColor.BLACK
                                : new BaseColor(cell.InheritedStyle.ForeColor.R, cell.InheritedStyle.ForeColor.G, cell.InheritedStyle.ForeColor.B);

                            // Betűtípus az adott cella szövegszínével
                            iTextSharp.text.Font betu = new iTextSharp.text.Font(alapFont, 10f, iTextSharp.text.Font.NORMAL, szovegSzín);

                            PdfPCell pdfCell = new PdfPCell(new Phrase(szoveg, betu))
                            {
                                BackgroundColor = háttérSzín
                            };

                            pdfTable.AddCell(pdfCell);
                        }
                    }

                    pdfDoc.Add(pdfTable);
                    pdfDoc.Close();
                    stream.Close();
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ellenőrzi, hogy a megadott dátum nem jövőbeli-e.
        /// </summary>
        private bool DatumEllenorzes(DateTime UjDatum)
        {
            if (UjDatum > DateTime.Today)
            {
                MessageBox.Show("Nem lehet jövőbeli dátumot beállítani", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Új üzemóra rekordot hoz létre, ha az adott dátumhoz még nem létezik aktív bejegyzés,
        /// és az üzemóra értéke megfelelő a környező rekordokhoz képest.
        /// </summary>
        private bool UjRekordHozzaadasa(DateTime UjDatum, long UjUzemora, bool UjStatus)
        {
            try
            {
                if (AdatokUzemora.Any(a => a.Dátum.Date == UjDatum && !a.Státus))
                {
                    MessageBox.Show("Az adott dátumhoz már létezik rekord. Nem hozható létre új.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                long ElozoUzemora = (from a in AdatokUzemora
                                     where a.Dátum < UjDatum && a.Státus == false
                                     orderby a.Dátum descending
                                     select a.Uzemora).FirstOrDefault();

                long UtanaUzemora = (from a in AdatokUzemora
                                     where a.Dátum > UjDatum && a.Státus == false
                                     orderby a.Dátum
                                     select a.Uzemora).FirstOrDefault();

                if (UjUzemora <= ElozoUzemora || (UtanaUzemora != 0 && UjUzemora >= UtanaUzemora))
                {
                    MessageBox.Show($"Az üzemóra értéknek az előző: {ElozoUzemora} és következő: {UtanaUzemora} közé kell esnie.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                Adat_Eszterga_Uzemora ADAT = new Adat_Eszterga_Uzemora(0,
                                                  UjUzemora,
                                                  UjDatum,
                                                  UjStatus);
                Kez_Uzemora.Rogzites(ADAT);

                MessageBox.Show("Új rekord sikeresen létrehozva.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return true;
        }

        /// <summary>
        /// Módosítja a kiválasztott üzemóra rekordot.  
        /// Érvényesítés után elvégzi a törlést és újrarögzítést, ha szükséges.
        /// Az érintett karbantartási műveleteket is frissíti, ha az üzemóra vagy dátum változott.
        /// </summary>
        private bool MeglevoRekordModositasa(DateTime UjDatum, long UjUzemora, bool UjStatus)
        {
            bool UtolsoTorles = false;
            try
            {
                DataGridViewRow KivalasztottSor = Tabla.SelectedRows[0];
                int AktivID = KivalasztottSor.Cells[0].Value.ToÉrt_Int();

                if (!UzemoraSzamEllenorzes(UjUzemora, UjDatum))
                    return false;

                if (!TablaEllenorzes(AktivID, UjUzemora, UjDatum, UjStatus))
                    return false;

                Adat_Eszterga_Uzemora VanID = AdatokUzemora.FirstOrDefault(a => a.ID == AktivID);

                if (VanID == null)
                    return false;

                DateTime EredetiDatum = VanID.Dátum;
                long EredetiUzemora = VanID.Uzemora;
                bool EredetiStatusz = VanID.Státus;

                if (EredetiDatum != DateTime.Today && EredetiDatum == UjDatum && EredetiUzemora == UjUzemora && EredetiStatusz != UjStatus)
                {
                    if (UjStatus)
                        Kez_Uzemora.Torles(new Adat_Eszterga_Uzemora(AktivID));
                    else
                        Kez_Uzemora.Rogzites(new Adat_Eszterga_Uzemora(0, EredetiUzemora, EredetiDatum, false));
                }
                else
                {
                    if (UjStatus && EredetiDatum == DateTime.Today)
                        UtolsoUzemoraTorles(AktivID);

                    else
                    {
                        Kez_Uzemora.Torles(new Adat_Eszterga_Uzemora(AktivID));
                        Kez_Uzemora.Rogzites(new Adat_Eszterga_Uzemora(0, UjUzemora, UjDatum, false));
                    }
                    UtolsoTorles = true;
                }
                TablaListazas();
                Frissit_MuveletTablazat(EredetiDatum, UjDatum, EredetiUzemora, UjUzemora);
                Eszterga_Valtozas?.Invoke();
                if (!UtolsoTorles)
                    MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return true;
        }

        /// <summary>
        /// Frissíti a karbantartási műveletek utolsó üzemóra és dátum mezőit,
        /// ha az üzemóra vagy dátum változott.
        /// </summary>
        private void Frissit_MuveletTablazat(DateTime EredetiDatum, DateTime UjDatum, long EredetiUzemora, long UjUzemora)
        {
            try
            {
                AdatokMuvelet = Kez_Muvelet.Lista_Adatok();
                if (UjDatum != EredetiDatum || UjUzemora != EredetiUzemora)
                {
                    List<Adat_Eszterga_Muveletek> rekord = (from a in AdatokMuvelet
                                                            where (a.Utolsó_Dátum == EredetiDatum || a.Utolsó_Üzemóra_Állás == EredetiUzemora)
                                                            && a.Státus != true
                                                            select a).ToList();

                    List<Adat_Eszterga_Muveletek> ModLista = new List<Adat_Eszterga_Muveletek>();

                    foreach (Adat_Eszterga_Muveletek Muvelet in rekord)
                        ModLista.Add(new Adat_Eszterga_Muveletek(UjDatum, UjUzemora, Muvelet.ID));
                }
                else
                    //csak akkor frissítjük a műveleteket, ha az üzemóra vagy dátum változott
                    return;

                // JAVÍTANDÓ:Ez minek?
                //kesz
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ellenőrzi, hogy az adott napon létezik-e másik aktív rekord,  
        /// illetve történt-e valós adatváltozás a módosításhoz képest.
        /// </summary>
        private bool TablaEllenorzes(int AktivID, long UjUzemora, DateTime UjDatum, bool UjStatus)
        {
            try
            {
                Adat_Eszterga_Uzemora AktivRekord = AdatokUzemora.FirstOrDefault(a => a.Dátum == UjDatum && !a.Státus);

                if (UjStatus == false && AktivRekord != null && AktivRekord.ID != AktivID)
                {
                    MessageBox.Show("Az adott napon már van egy aktív rekord. Nem állítható töröltről aktívra.",
                                    "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                Adat_Eszterga_Uzemora KivalasztottRekord = AdatokUzemora.FirstOrDefault(a => a.ID == AktivID);
                if (KivalasztottRekord != null &&
                    KivalasztottRekord.Uzemora == UjUzemora &&
                    KivalasztottRekord.Dátum == UjDatum &&
                    KivalasztottRekord.Státus == UjStatus)
                {
                    MessageBox.Show("Az adatok nem változtak. Nincs szükség módosításra.",
                                    "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return true;
        }

        /// <summary>
        /// Ellenőrzi, hogy az új üzemóra érték pozitív szám-e,  
        /// és hogy logikusan illeszkedik-e a környező (időben előtte és utána lévő) rekordok közé.
        /// </summary>
        private bool UzemoraSzamEllenorzes(long UjUzemora, DateTime UjDatum)
        {
            try
            {
                if (UjUzemora <= 0)
                {
                    MessageBox.Show("Az üzemóra értékének pozitív egész számnak kell lennie.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                Adat_Eszterga_Uzemora ElozoRekord = AdatokUzemora
                    .Where(a => a.Dátum < UjDatum && !a.Státus)
                    .OrderByDescending(a => a.Dátum)
                    .FirstOrDefault();

                long ElozoUzemora = ElozoRekord?.Uzemora ?? int.MinValue;

                Adat_Eszterga_Uzemora UtanaRekord = AdatokUzemora
                    .Where(a => a.Dátum > UjDatum && !a.Státus)
                    .OrderBy(a => a.Dátum)
                    .FirstOrDefault();

                long UtanaUzemora = UtanaRekord?.Uzemora ?? int.MaxValue;

                if (UjUzemora <= ElozoUzemora || UjUzemora >= UtanaUzemora)
                {
                    MessageBox.Show($"Az üzemóra értéknek az előző: {(ElozoRekord != null ? ElozoUzemora.ToStrTrim() : "nincs")}" +
                        $" és következő: {(UtanaRekord != null ? UtanaUzemora.ToStrTrim() : "nincs")} közé kell esnie.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return true;
        }

        /// <summary>
        /// Törli a kiválasztott, mai napra vonatkozó üzemóra rekordot,  
        /// majd új adatot kér be a felhasználótól a segédablakban.
        /// Ha a felhasználó megszakítja a rögzítést, bezárja az összes kapcsolódó ablakot.
        /// </summary>
        private void UtolsoUzemoraTorles(int AktivID)
        {
            try
            {
                Adat_Eszterga_Uzemora ADAT = new Adat_Eszterga_Uzemora(AktivID);
                Kez_Uzemora.Torles(ADAT);

                using (Ablak_Eszterga_Karbantartás_Segéd SegedAblak = new Ablak_Eszterga_Karbantartás_Segéd())
                {
                    if (SegedAblak.ShowDialog() == DialogResult.OK)
                        MessageBox.Show("Mai napra vonatkozó új üzemóra sikeresen mentve.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        EsztergaAblakokBezarasa();
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Bezárja az összes megnyitott esztergához kapcsolódó ablakot a programból,  
        /// hogy biztosítsa a következő adatbevitel tiszta környezetét.
        /// </summary>
        private void EsztergaAblakokBezarasa()
        {
            try
            {
                foreach (Form NyitottAblak in Application.OpenForms.Cast<Form>().ToArray())
                {
                    if (NyitottAblak is Ablak_Eszterga_Karbantartás ||
                        NyitottAblak is Ablak_Eszterga_Karbantartás_Módosít ||
                        NyitottAblak is Ablak_Eszterga_Karbantartás_Üzemóra ||
                        NyitottAblak is Ablak_Eszterga_Karbantartás_Segéd
                        )
                        NyitottAblak.Close();
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
