using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Ablakok._4_Nyilvántartások.Kerékeszterga;
using Villamos.Villamos_Ablakok._5_Karbantartás.Eszterga_Karbantartás;
using Villamos.Villamos_Adatszerkezet;
using Villamos.Villamos_Kezelők;
using Application = System.Windows.Forms.Application;
using Funkcio = Villamos.Villamos_Ablakok._4_Nyilvántartások.Kerékeszterga.Eszterga_Funkció;
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
        readonly bool Baross = Program.PostásTelephely.Trim() == "Angyalföld";
        #endregion

        #region Listák
        private List<Adat_Eszterga_Muveletek> AdatokMuvelet;
        private List<Adat_Eszterga_Uzemora> AdatokUzemora;
        #endregion

        #region Kezelők
        readonly private Kezelo_Eszterga_Muveletek KezMuveletek = new Kezelo_Eszterga_Muveletek();
        readonly private Kezelő_Eszterga_Üzemóra KezUzemora = new Kezelő_Eszterga_Üzemóra();
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
            Tábla.CellFormatting += Tábla_CellFormatting;
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

                AdatokUzemora = Funkcio.Eszterga_UzemoraFeltolt();

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

                Tábla.DataSource = AdatTabla;
                OszlopSzelesseg();
                Tábla.Visible = true;
                Tábla.ClearSelection();
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
            Tábla.Columns["ID"].Width = 50;
            Tábla.Columns["Üzemóra"].Width = 159;
            Tábla.Columns["Dátum"].Width = 110;
            Tábla.Columns["Státusz"].Width = 100;
        }

        /// <summary>
        /// A törölt sorokat piros háttérrel és áthúzott betűstílussal jeleníti meg.
        /// Minden más sor fehér háttérrel és normál stílussal formázódik.
        /// </summary>
        private void Tábla_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (Tábla.Columns[e.ColumnIndex].Name == "Státusz" && e.Value is string státusz)
                {
                    DataGridViewRow sor = Tábla.Rows[e.RowIndex];
                    if (státusz == "Törölt")
                    {
                        sor.DefaultCellStyle.BackColor = Color.IndianRed;
                        sor.DefaultCellStyle.ForeColor = Color.Black;
                        sor.DefaultCellStyle.Font = new Font(Tábla.DefaultCellStyle.Font, FontStyle.Strikeout);
                    }
                    else
                    {
                        sor.DefaultCellStyle.BackColor = Color.White;
                        sor.DefaultCellStyle.ForeColor = Color.Black;
                        sor.DefaultCellStyle.Font = new Font(Tábla.DefaultCellStyle.Font, FontStyle.Regular);
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
                    DataGridViewRow row = Tábla.Rows[e.RowIndex];
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
            Tábla.ClearSelection();
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
                if (Tábla.SelectedRows.Count > 1)
                    throw new HibásBevittAdat("Egyszerre csak 1 sort lehet módosítani");

                long UjUzemora = TxtBxÜzem.Text.ToÉrt_Long();
                DateTime UjDatum = DtmPckrDátum.Value.Date;
                bool UjStatus = ChckBxStátus.Checked;

                AdatokUzemora = Funkcio.Eszterga_UzemoraFeltolt();

                if (!DatumEllenorzes(UjDatum)) return;

                if (Tábla.SelectedRows.Count == 0)
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
        /// Az üzemóra táblázat tartalmát Excel fájlba exportálja és megnyitja.
        /// A fájl nevét automatikusan generálja, a felhasználó kiválaszthatja a mentési helyet.
        /// </summary>
        private void Btn_Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.Rows.Count <= 0) throw new HibásBevittAdat("Nincs sora a táblázatnak!");
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

                MyE.EXCELtábla(fájlexc, Tábla, false);
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
                KezUzemora.Rogzites(ADAT);

                MessageBox.Show("Új rekord sikeresen létrehozva.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw;
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        /// <summary>
        /// Módosítja a kiválasztott üzemóra rekordot.  
        /// Érvényesítés után elvégzi a törlést és újrarögzítést, ha szükséges.
        /// Az érintett karbantartási műveleteket is frissíti, ha az üzemóra vagy dátum változott.
        /// </summary>
        private bool MeglevoRekordModositasa(DateTime UjDatum, long UjUzemora, bool UjStatus)
        {
            try
            {
                DataGridViewRow KivalasztottSor = Tábla.SelectedRows[0];
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
                        KezUzemora.Torles(new Adat_Eszterga_Uzemora(AktivID));
                    else
                        KezUzemora.Rogzites(new Adat_Eszterga_Uzemora(0, EredetiUzemora, EredetiDatum, false));
                }
                else
                {
                    if (UjStatus && EredetiDatum == DateTime.Today)
                        UtolsoUzemoraTorles(AktivID);

                    else
                    {
                        KezUzemora.Torles(new Adat_Eszterga_Uzemora(AktivID));
                        KezUzemora.Rogzites(new Adat_Eszterga_Uzemora(0, UjUzemora, UjDatum, false));
                    }
                }
                TablaListazas();
                Frissit_MuveletTablazat(EredetiDatum, UjDatum, EredetiUzemora, UjUzemora);
                Eszterga_Valtozas?.Invoke();
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw;
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        /// <summary>
        /// Frissíti a karbantartási műveletek utolsó üzemóra és dátum mezőit,
        /// ha az üzemóra vagy dátum változott.
        /// </summary>
        private void Frissit_MuveletTablazat(DateTime EredetiDatum, DateTime UjDatum, long EredetiUzemora, long UjUzemora)
        {
            try
            {
                AdatokMuvelet = Funkcio.Eszterga_KarbantartasFeltolt();
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
                    return;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw;
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
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

                return true;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw;
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
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

                return true;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw;
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
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
                KezUzemora.Torles(ADAT);

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
