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

        DataTable AdatTabla = new DataTable();
        public event Event_Kidobo Eszterga_Valtozas;
        // JAVÍTANDÓ:?
        readonly bool Baross = Program.PostásTelephely.Trim() == "Baross";
        #endregion

        #region Listák

        List<Adat_Eszterga_Muveletek> AdatokMuvelet = new List<Adat_Eszterga_Muveletek>();
        List<Adat_Eszterga_Uzemora> AdatokUzemora = new List<Adat_Eszterga_Uzemora>();
        #endregion

        #region Kezelők

        readonly Kezelő_Eszterga_Műveletek Kez_Muvelet = new Kezelő_Eszterga_Műveletek();
        readonly Kezelő_Eszterga_Üzemóra Kez_Uzemora = new Kezelő_Eszterga_Üzemóra();
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
            // A DataGridView adatforrásának kötése után automatikusan meghívja a ToroltTablaSzinezes metódust,
            // hogy a törölt státuszú sorokat színezve jelenítse meg.
            Tabla.DataBindingComplete += (s, ev) => Szinezes(Tabla);
        }

        /// <summary>
        /// Jogosultság alapján engedélyezi vagy tiltja a felhasználó számára a műveletek (új, módosít, Excel export) elérhetőségét.
        /// </summary>
        private void Jogosultsagkiosztas()
        {
            try
            {
                int melyikelem = 160;
                Btn_Modosit.Visible = Baross;

                // módosítás 1 
                //Ablak_Eszterga_Karbantartás_Segéd oldal használja az 1. módosításokat
                Btn_Excel.Enabled = MyF.Vanjoga(melyikelem, 1);

                // módosítás 2
                //Ablak_Eszterga_Karbantartás oldal használja a 2. módosításokat

                // módosítás 3 
                Btn_Modosit.Enabled = MyF.Vanjoga(melyikelem, 3);
                Btn_UjFelvetel.Enabled = MyF.Vanjoga(melyikelem, 3);
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
                Tabla.DataSource = null;
                AdatTabla = new DataTable();
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
                Szinezes(Tabla);
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
        /// Színezi a táblázat sorait a státusz alapján, ha a státusz "Törölt".
        /// Ha a státusz "Törölt", a sor háttérszíne piros, szövege fekete, és áthúzott betűtípust kap.
        /// Ha a státusz nem "Törölt", visszaáll a szokásos megjelenítés fehér háttérre.
        /// </summary>
        private void Szinezes(DataGridView tabla)
        {
            foreach (DataGridViewRow sor in tabla.Rows)
            {
                string statusz = sor.Cells["Státusz"].Value?.ToStrTrim();

                if (statusz == "Törölt")
                {
                    foreach (DataGridViewCell cell in sor.Cells)
                    {
                        cell.Style.BackColor = Color.IndianRed;
                        cell.Style.ForeColor = Color.Black;
                        cell.Style.Font = new System.Drawing.Font(tabla.DefaultCellStyle.Font, FontStyle.Strikeout);
                    }
                }
                else
                {
                    foreach (DataGridViewCell cell in sor.Cells)
                    {
                        cell.Style.BackColor = Color.White;
                        cell.Style.ForeColor = Color.Black;
                        cell.Style.Font = new System.Drawing.Font(tabla.DefaultCellStyle.Font, FontStyle.Regular);
                    }
                }
            }
        }

        /// <summary>
        /// Eseménykezelő, amely a DataGridView adatforrásának kötése után hívódik meg.
        /// Meghívja a ToroltTablaSzinezes metódust, hogy a törölt státuszú sorokat megjelenítési színezéssel lássa el.
        /// </summary>
        private void Tabla_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Szinezes(Tabla);
        }

        /// <summary>
        /// Sor kijelölésekor betölti annak adatait a szerkesztőmezőkbe (üzemóra, dátum, státusz).
        /// </summary>
        private void Tabla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = Tabla.Rows[e.RowIndex];
                    TxtBxUzem.Text = row.Cells[1].Value.ToStrTrim();
                    DtmPckr.Value = row.Cells[2].Value.ToÉrt_DaTeTime();
                    ChckBxStatus.Checked = row.Cells[3].Value.ToStrTrim() == "Törölt";
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
        private void Btn_UjFelvetel_Click(object sender, EventArgs e)
        {
            Tabla.ClearSelection();
            TxtBxUzem.Text = string.Empty;
            TxtBxUzem.Focus();
            DtmPckr.Value = DateTime.Today;
            ChckBxStatus.Checked = false;
        }

        /// <summary>
        /// A kijelölt sor adatait módosítja vagy új rekordot hoz létre, ha nincs kiválasztott sor.
        /// Előtte érvényesíti az adatokat, majd a változásokat adatbázisba menti, és frissíti a táblát.
        /// </summary>
        private void Btn_Modosit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tabla.SelectedRows.Count > 1)
                    throw new HibásBevittAdat("Egyszerre csak 1 sort lehet módosítani");

                long UjUzemora = TxtBxUzem.Text.ToÉrt_Long();
                DateTime UjDatum = DtmPckr.Value.Date;
                bool UjStatus = ChckBxStatus.Checked;

                AdatokUzemora = Kez_Uzemora.Lista_Adatok();

                if (!DatumEllenorzes(UjDatum)) return;

                if (AdatokUzemora.Any(a => a.Dátum.Date == UjDatum && !a.Státus))
                    MeglevoRekordModositasa(UjDatum, UjUzemora, UjStatus);
                else
                    UjRekordHozzaadasa(UjDatum, UjUzemora, UjStatus);

                if (ActiveForm is Ablak_Eszterga_Karbantartás_Üzemóra)
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
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Eszterga_Karbantartás_Üzemórák_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };

                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyE.DataGridViewToExcel(fájlexc, Tabla, true);
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

                PDFtabla(fajlNev, Tabla);

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
        private void PDFtabla(string fájlNév, DataGridView tábla)
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
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            string szoveg = cell.Value?.ToStrTrim() ?? "";

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
            bool Valasz = true;
            try
            {
                if (UjDatum > DateTime.Today)
                {
                    Valasz = false;
                    throw new HibásBevittAdat("Nem lehet jövőbeli dátumot beállítani");
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
            return Valasz;
        }

        /// <summary>
        /// Új üzemóra rekordot hoz létre, ha az adott dátumhoz még nem létezik aktív bejegyzés,
        /// és az üzemóra értéke megfelelő a környező rekordokhoz képest.
        /// </summary>
        private void UjRekordHozzaadasa(DateTime UjDatum, long UjUzemora, bool UjStatus)
        {
            try
            {

                long ElozoUzemora = (from a in AdatokUzemora
                                     where a.Dátum < UjDatum && !a.Státus
                                     orderby a.Dátum descending
                                     select a.Uzemora).FirstOrDefault();

                long UtanaUzemora = (from a in AdatokUzemora
                                     where a.Dátum > UjDatum && !a.Státus
                                     orderby a.Dátum
                                     select a.Uzemora).FirstOrDefault();

                if (UjUzemora <= ElozoUzemora || (UtanaUzemora != 0 && UjUzemora >= UtanaUzemora))
                    throw new HibásBevittAdat($"Az üzemóra értéknek az előző: {ElozoUzemora} és következő: {UtanaUzemora} közé kell esnie.");

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
        }

        /// <summary>
        /// Módosítja a kiválasztott üzemóra rekordot.  
        /// Érvényesítés után elvégzi a törlést és újrarögzítést, ha szükséges.
        /// Az érintett karbantartási műveleteket is frissíti, ha az üzemóra vagy dátum változott.
        /// </summary>
        private void MeglevoRekordModositasa(DateTime UjDatum, long UjUzemora, bool UjStatus)
        {
            bool UtolsoTorles = false;
            try
            {
                if (Tabla.SelectedRows.Count == 0)
                    throw new HibásBevittAdat("Nincs kiválasztott sor a módosításhoz.");

                int AktivID = Tabla.SelectedRows[0].Cells[0].Value.ToÉrt_Int();
                Adat_Eszterga_Uzemora VanID = AdatokUzemora.FirstOrDefault(a => a.ID == AktivID)
                           ?? throw new HibásBevittAdat("A kiválasztott rekord nem található.");

                DateTime EredetiDatum = VanID.Dátum;
                long EredetiUzemora = VanID.Uzemora;
                bool EredetiStatusz = VanID.Státus;

                if (UjDatum == EredetiDatum && UjUzemora == EredetiUzemora && UjStatus == EredetiStatusz)
                    throw new HibásBevittAdat("Nem történt változás.");

                if (!UzemoraSzamEllenorzes(UjUzemora, UjDatum))
                    return;

                if (!TablaEllenorzes(AktivID, UjUzemora, UjDatum, UjStatus))
                    return;

                if (EredetiDatum != DateTime.Today && EredetiDatum == UjDatum && EredetiUzemora == UjUzemora && EredetiStatusz != UjStatus)
                {
                    if (UjStatus)
                        Kez_Uzemora.Torles(new Adat_Eszterga_Uzemora(AktivID));
                    else
                        //statusz toroltrol aktivra allitasnal fut le
                        Kez_Uzemora.Rogzites(new Adat_Eszterga_Uzemora(0, EredetiUzemora, EredetiDatum, false));
                }
                else
                {
                    if (UjStatus && EredetiDatum == DateTime.Today)
                    {
                        UtolsoUzemoraTorles(AktivID);
                        UtolsoTorles = true;
                    }

                    else
                    {
                        Kez_Uzemora.Torles(new Adat_Eszterga_Uzemora(AktivID));
                        Kez_Uzemora.Rogzites(new Adat_Eszterga_Uzemora(0, UjUzemora, UjDatum, false));
                    }
                }
                if (Form.ActiveForm is Ablak_Eszterga_Karbantartás_Üzemóra)
                {
                    TablaListazas();
                    Eszterga_Valtozas?.Invoke();
                }
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
        }

        /// <summary>
        /// Ellenőrzi, hogy az adott napon létezik-e másik aktív rekord,  
        /// illetve történt-e valós adatváltozás a módosításhoz képest.
        /// </summary>
        private bool TablaEllenorzes(int AktivID, long UjUzemora, DateTime UjDatum, bool UjStatus)
        {
            bool Valasz = true;
            try
            {
                Adat_Eszterga_Uzemora AktivRekord = AdatokUzemora.FirstOrDefault(a => a.Dátum == UjDatum && !a.Státus);

                if (UjStatus == false && AktivRekord != null && AktivRekord.ID != AktivID)
                {
                    Valasz = false;
                    throw new HibásBevittAdat("Az adott napon már van egy aktív rekord. Nem állítható töröltről aktívra.");
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
            return Valasz;
        }

        /// <summary>
        /// Ellenőrzi, hogy az új üzemóra érték pozitív szám-e,  
        /// és hogy logikusan illeszkedik-e a környező (időben előtte és utána lévő) rekordok közé.
        /// </summary>
        private bool UzemoraSzamEllenorzes(long UjUzemora, DateTime UjDatum)
        {
            bool Valasz = true;
            try
            {
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
                    Valasz = false;
                    throw new HibásBevittAdat($"Az üzemóra értéknek az előző: {(ElozoRekord != null ? ElozoUzemora.ToStrTrim() : "nincs")}" +
                        $" és következő: {(UtanaRekord != null ? UtanaUzemora.ToStrTrim() : "nincs")} közé kell esnie.");
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
            return Valasz;
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
