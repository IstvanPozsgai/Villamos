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
using Funkció = Villamos.Villamos_Ablakok._4_Nyilvántartások.Kerékeszterga.Eszterga_Funkció;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos.V_Ablakok._4_Nyilvántartások.Eszterga_Karbantartás
{
    public delegate void Event_Kidobó();
    public partial class Ablak_Eszterga_Karbantartás_Üzemóra : Form
    {
        #region Osztályszintű elemek
        readonly private DataTable AdatTábla = new DataTable();
        public event Event_Kidobó Eszterga_Változás;
        readonly DateTime MaiDatum = DateTime.Today;
        readonly bool Baross = Program.PostásTelephely.Trim() == "Angyalföld";
        #endregion

        #region Listák
        private List<Adat_Eszterga_Műveletek> AdatokMűvelet;
        private List<Adat_Eszterga_Üzemóra> AdatokÜzemóra;
        #endregion

        #region Kezelők
        private Kezelő_Eszterga_Műveletek KézMűveletek = new Kezelő_Eszterga_Műveletek();
        private Kezelő_Eszterga_Üzemóra KézÜzemóra = new Kezelő_Eszterga_Üzemóra();
        #endregion

        #region Alap
        public Ablak_Eszterga_Karbantartás_Üzemóra()
        {
            InitializeComponent();
        }
        private void Ablak_Eszterga_Karbantartás_Üzemóra_Load(object sender, EventArgs e)
        {
            TáblaListázás();
            Jogosultságkiosztás();
            Tábla.CellFormatting += Tábla_CellFormatting;
        }
        private void Jogosultságkiosztás()
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
        private void TáblaListázás()
        {
            try
            {
                AdatTábla.Columns.Clear();
                AdatTábla.Rows.Clear();
                AdatTábla.Columns.Add("ID");
                AdatTábla.Columns.Add("Üzemóra");
                AdatTábla.Columns.Add("Dátum");
                AdatTábla.Columns.Add("Státusz");

                AdatokÜzemóra = Funkció.Eszterga_ÜzemóraFeltölt();

                AdatTábla.Rows.Clear();

                foreach (Adat_Eszterga_Üzemóra rekord in AdatokÜzemóra)
                {
                    DataRow Soradat = AdatTábla.NewRow();
                    Soradat["ID"] = rekord.ID;
                    Soradat["Üzemóra"] = rekord.Üzemóra;
                    Soradat["Dátum"] = rekord.Dátum.ToShortDateString();
                    Soradat["Státusz"] = rekord.Státus ? "Törölt" : "Aktív";

                    AdatTábla.Rows.Add(Soradat);
                }

                Tábla.DataSource = AdatTábla;
                OszlopSzélesség();
                Tábla.Visible = true;
                Tábla.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void OszlopSzélesség()
        {
            Tábla.Columns["ID"].Width = 50;
            Tábla.Columns["Üzemóra"].Width = 159;
            Tábla.Columns["Dátum"].Width = 110;
            Tábla.Columns["Státusz"].Width = 100;
        }
        private void Tábla_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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
        private void Btn_ÚjFelvétel_Click(object sender, EventArgs e)
        {
            Tábla.ClearSelection();
            TxtBxÜzem.Text = string.Empty;
            TxtBxÜzem.Focus();
            DtmPckrDátum.Value = MaiDatum;
            ChckBxStátus.Checked = false;
        }
        private void Btn_Módosít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.SelectedRows.Count > 1)
                    throw new HibásBevittAdat("Egyszerre csak 1 sort lehet módosítani");

                long UjUzemora = TxtBxÜzem.Text.ToÉrt_Long();
                DateTime UjDatum = DtmPckrDátum.Value.Date;
                bool UjStatus = ChckBxStátus.Checked;

                AdatokÜzemóra = Funkció.Eszterga_ÜzemóraFeltölt();

                if (!DátumEllenőrzés(UjDatum)) return;

                if (Tábla.SelectedRows.Count == 0)
                { if (!ÚjRekordHozzáadása(UjDatum, UjUzemora, UjStatus)) return; }

                else
                { if (MeglévőRekordMódosítása(UjDatum, UjUzemora, UjStatus)) return; }
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
            Eszterga_Változás?.Invoke();
            TáblaListázás();
        }
        private void Btn_Excel_Click(object sender, EventArgs e)
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
        private bool DátumEllenőrzés(DateTime UjDatum)
        {
            if (UjDatum > MaiDatum)
            {
                MessageBox.Show("Nem lehet jövőbeli dátumot beállítani", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }
        private bool ÚjRekordHozzáadása(DateTime UjDatum, long UjUzemora, bool UjStatus)
        {
            if (AdatokÜzemóra.Any(a => a.Dátum.Date == UjDatum && !a.Státus))
            {
                MessageBox.Show("Az adott dátumhoz már létezik rekord. Nem hozható létre új.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            long ElozoUzemora = (from a in AdatokÜzemóra
                                 where a.Dátum < UjDatum && a.Státus == false
                                 orderby a.Dátum descending
                                 select a.Üzemóra).FirstOrDefault();

            long UtanaUzemora = (from a in AdatokÜzemóra
                                 where a.Dátum > UjDatum && a.Státus == false
                                 orderby a.Dátum
                                 select a.Üzemóra).FirstOrDefault();

            if (UjUzemora <= ElozoUzemora || (UtanaUzemora != 0 && UjUzemora >= UtanaUzemora))
            {
                MessageBox.Show($"Az üzemóra értéknek az előző: {ElozoUzemora} és következő: {UtanaUzemora} közé kell esnie.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }



            Adat_Eszterga_Üzemóra ADAT = new Adat_Eszterga_Üzemóra(0,
                                              UjUzemora,
                                              UjDatum,
                                              UjStatus);
            KézÜzemóra.Rögzítés(ADAT);

            MessageBox.Show("Új rekord sikeresen létrehozva.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }
        private bool MeglévőRekordMódosítása(DateTime UjDatum, long UjUzemora, bool UjStatus)
        {
            DataGridViewRow KivalasztottSor = Tábla.SelectedRows[0];
            int AktivID = KivalasztottSor.Cells[0].Value.ToÉrt_Int();

            if (!UzemoraSzamEllenorzes(UjUzemora, UjDatum))
                return false;

            if (!TáblaEllenőrzés(AktivID, UjUzemora, UjDatum, UjStatus))
                return false;

            Adat_Eszterga_Üzemóra VanID = AdatokÜzemóra.FirstOrDefault(a => a.ID == AktivID);

            if (VanID != null)
            {
                DateTime EredetiDatum = VanID.Dátum;
                long EredetiUzemora = VanID.Üzemóra;
                if (UjStatus && VanID.Dátum == MaiDatum)
                {
                    UtolsoUzemoraTorles(AktivID);
                    return true;
                }
                else
                {
                    Adat_Eszterga_Üzemóra ADATTörlés = new Adat_Eszterga_Üzemóra(AktivID);
                    KézÜzemóra.Törlés(ADATTörlés);

                    Adat_Eszterga_Üzemóra ADATTörlésÚjLétrehoz = new Adat_Eszterga_Üzemóra(0, UjUzemora, UjDatum, false);
                    KézÜzemóra.Rögzítés(ADATTörlésÚjLétrehoz);

                    Frissít_Táblázat(EredetiDatum, UjDatum, EredetiUzemora, UjUzemora);
                    MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            return false;
        }
        private void Frissít_Táblázat(DateTime EredetiDatum, DateTime UjDatum, long EredetiUzemora, long UjUzemora)
        {
            AdatokMűvelet = Funkció.Eszterga_KarbantartasFeltölt();
            if (UjDatum != EredetiDatum || UjUzemora != EredetiUzemora)
            {
                List<Adat_Eszterga_Műveletek> rekord = (from a in AdatokMűvelet
                                                        where (a.Utolsó_Dátum == EredetiDatum || a.Utolsó_Üzemóra_Állás == EredetiUzemora)
                                                        && a.Státus != true
                                                        select a).ToList();

                foreach (Adat_Eszterga_Műveletek Muvelet in rekord)
                {
                    Adat_Eszterga_Műveletek ADAT = new Adat_Eszterga_Műveletek(UjDatum, UjUzemora, Muvelet.ID);
                    KézMűveletek.Módosítás(ADAT);
                }
            }
            else
                return;
        }
        private bool TáblaEllenőrzés(int AktivID, long UjUzemora, DateTime UjDatum, bool UjStatus)
        {
            try
            {
                Adat_Eszterga_Üzemóra AktivRekord = AdatokÜzemóra.FirstOrDefault(a => a.Dátum == UjDatum && !a.Státus);

                if (UjStatus == false && AktivRekord != null && AktivRekord.ID != AktivID)
                {
                    MessageBox.Show("Az adott napon már van egy aktív rekord. Nem állítható töröltről aktívra.",
                                    "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                Adat_Eszterga_Üzemóra KivalasztottRekord = AdatokÜzemóra.FirstOrDefault(a => a.ID == AktivID);
                if (KivalasztottRekord != null &&
                    KivalasztottRekord.Üzemóra == UjUzemora &&
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
                return false;
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private bool UzemoraSzamEllenorzes(long UjUzemora, DateTime UjDatum)
        {
            if (UjUzemora <= 0)
            {
                MessageBox.Show("Az üzemóra értékének pozitív egész számnak kell lennie.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            Adat_Eszterga_Üzemóra ElozoRekord = AdatokÜzemóra
                .Where(a => a.Dátum < UjDatum && !a.Státus)
                .OrderByDescending(a => a.Dátum)
                .FirstOrDefault();

            long ElozoUzemora = ElozoRekord?.Üzemóra ?? int.MinValue;

            Adat_Eszterga_Üzemóra UtanaRekord = AdatokÜzemóra
                .Where(a => a.Dátum > UjDatum && !a.Státus)
                .OrderBy(a => a.Dátum)
                .FirstOrDefault();

            long UtanaUzemora = UtanaRekord?.Üzemóra ?? int.MaxValue;

            if (UjUzemora <= ElozoUzemora || UjUzemora >= UtanaUzemora)
            {
                MessageBox.Show($"Az üzemóra értéknek az előző: {(ElozoRekord != null ? ElozoUzemora.ToStrTrim() : "nincs")}" +
                    $" és következő: {(UtanaRekord != null ? UtanaUzemora.ToStrTrim() : "nincs")} közé kell esnie.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }
        private void UtolsoUzemoraTorles(int AktivID)
        {
            Adat_Eszterga_Üzemóra ADAT = new Adat_Eszterga_Üzemóra(AktivID);
            KézÜzemóra.Törlés(ADAT);

            using (Ablak_Eszterga_Karbantartás_Segéd SegedAblak = new Ablak_Eszterga_Karbantartás_Segéd())
            {
                if (SegedAblak.ShowDialog() == DialogResult.OK)
                    MessageBox.Show("Mai napra vonatkozó új üzemóra sikeresen mentve.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    EsztergaAblakokBezárása();
            }
        }
        private void EsztergaAblakokBezárása()
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
        #endregion
    }
}
