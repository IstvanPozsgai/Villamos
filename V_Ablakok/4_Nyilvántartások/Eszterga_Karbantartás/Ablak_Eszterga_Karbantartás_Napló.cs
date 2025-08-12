using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using Villamos.Villamos_Kezelők;
using MyF = Függvénygyűjtemény;

namespace Villamos.V_Ablakok._4_Nyilvántartások.Eszterga_Karbantartás
{
    public partial class Ablak_Eszterga_Karbantartás_Napló : Form
    {
        public delegate void Event_Kidobo();
        //databindingot elhanyagolni mas mod a szinezesre keresni egy masik oldalon 
        //ugyanezek a modosit oldalra
        #region Osztalyszintű elemek

        readonly bool Baross = Program.PostásTelephely.Trim() == "Baross";
        private bool frissul = false;
        private int elozoEv = DateTime.Today.Year;
        DataTable AdatTablaMuvelet = new DataTable();
        DataTable AdatTablaNaplo = new DataTable();
        #endregion

        #region Listák

        List<Adat_Eszterga_Muveletek> AdatokMuvelet = new List<Adat_Eszterga_Muveletek>();
        List<Adat_Eszterga_Uzemora> AdatokUzemora = new List<Adat_Eszterga_Uzemora>();
        List<Adat_Eszterga_Muveletek_Naplo> AdatokMuveletNaplo = new List<Adat_Eszterga_Muveletek_Naplo>();
        #endregion

        #region Kezelők

        readonly Kezelő_Eszterga_Műveletek Kez_Muvelet = new Kezelő_Eszterga_Műveletek();
        readonly Kezelő_Eszterga_Műveletek_Napló Kez_Muvelet_Naplo = new Kezelő_Eszterga_Műveletek_Napló();
        readonly Kezelő_Eszterga_Üzemóra Kez_Uzemora = new Kezelő_Eszterga_Üzemóra();
        #endregion

        #region Alap

        /// <summary>
        /// Az ablak konstruktorfüggvénye.  
        /// Betölti az aktuális évhez tartozó naplóbejegyzéseket, valamint az összes karbantartási műveletet.
        /// </summary>
        public Ablak_Eszterga_Karbantartás_Napló()
        {
            InitializeComponent();
            TablaNaploListazas(DtmPckr.Value.Year);
            TablaListazasMuvelet();
        }

        /// <summary>
        /// Az ablak betöltésekor fut le.  
        /// Jogosultságokat állít be, majd alapértelmezetten törli a kijelölést a táblázatokból,  
        /// és beolvassa az aktuális dátumhoz tartozó üzemóra értéket.
        /// </summary>
        private void Ablak_Eszterga_Karbantartás_Napló_Load(object sender, EventArgs e)
        {
            JogosultsagKiosztas();
            TablaMuvelet.ClearSelection();
            TablaNaplo.ClearSelection();
            UzemoraKiolvasasEsBeiras(DtmPckr.Value, TxtBxUzemora);
        }

        /// <summary>
        /// Jogosultságok alapján gombok láthatóságát és engedélyezettségét állítja be
        /// </summary>
        private void JogosultsagKiosztas()
        {
            try
            {
                int melyikelem = 160;
                Btn_Modosit.Visible = Baross;
                // módosítás 1 
                //Ablak_Eszterga_Karbantartás_Segéd oldal használja az 1. módosításokat

                // módosítás 2
                //Ablak_Eszterga_Karbantartás oldal használja a 2. módosításokat

                // módosítás 3 
                Btn_Modosit.Enabled = MyF.Vanjoga(melyikelem, 3);
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

        #region Tablak listazasa

        /// <summary>
        /// Színezi a táblázat sorait a státusz alapján, ha a státusz "Törölt".
        /// Ha a státusz "Törölt", a sor háttérszíne piros, szövege fekete, és áthúzott betűtípust kap.
        /// Ha a státusz nem "Törölt", visszaáll a szokásos megjelenítés fehér háttérre.
        /// </summary>
        private void TablaSzinezes(DataGridView tabla)
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
        /// A karbantartási műveletek naplóbejegyzései betöltése és megjelenítése a TáblaMűveletbe
        /// </summary>
        private void TablaNaploListazas(int Ev)
        {
            try
            {
                TablaNaplo.CleanFilterAndSort();
                TablaNaplo.DataSource = null;
                AdatTablaNaplo = new DataTable();
                AdatTablaNaplo.Columns.Add("Művelet Sorszáma");
                AdatTablaNaplo.Columns.Add("Művelet");
                AdatTablaNaplo.Columns.Add("Utolsó Dátum");
                AdatTablaNaplo.Columns.Add("Utolsó Üzemóra");
                AdatTablaNaplo.Columns.Add("Megjegyzés");
                AdatTablaNaplo.Columns.Add("Rögzítő");
                AdatTablaNaplo.Columns.Add("Rögzítés Dátuma");

                AdatokMuveletNaplo = Kez_Muvelet_Naplo.Lista_Adatok(Ev)
                    .OrderBy(a => a.Utolsó_Dátum)
                    .ThenBy(a => a.ID)
                    .ToList();

                foreach (Adat_Eszterga_Muveletek_Naplo rekord in AdatokMuveletNaplo)
                {
                    DataRow sor = AdatTablaNaplo.NewRow();

                    sor["Művelet Sorszáma"] = rekord.ID;
                    sor["Művelet"] = rekord.Művelet;
                    sor["Utolsó Dátum"] = rekord.Utolsó_Dátum.ToShortDateString();
                    sor["Utolsó Üzemóra"] = rekord.Utolsó_Üzemóra_Állás;
                    sor["Megjegyzés"] = rekord.Megjegyzés;
                    sor["Rögzítő"] = rekord.Rögzítő;
                    sor["Rögzítés Dátuma"] = rekord.Rögzítés_Dátuma.ToShortDateString();

                    AdatTablaNaplo.Rows.Add(sor);
                }

                TablaNaplo.DataSource = AdatTablaNaplo;
                OszlopSzelessegNaplo();

                for (int i = 0; i < TablaNaplo.Columns.Count; i++)
                    TablaNaplo.Columns[i].ReadOnly = true;

                TablaNaplo.Visible = true;
                TablaMuvelet.Visible = true;
                TablaMuvelet.ClearSelection();
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
        /// A naplótábla oszlopszélességeit állítja be
        /// </summary>
        private void OszlopSzelessegNaplo()
        {
            TablaNaplo.Columns["Művelet Sorszáma"].Width = 110;
            TablaNaplo.Columns["Művelet"].Width = 550;
            TablaNaplo.Columns["Utolsó Dátum"].Width = 105;
            TablaNaplo.Columns["Utolsó Üzemóra"].Width = 120;
            TablaNaplo.Columns["Megjegyzés"].Width = 305;
            TablaNaplo.Columns["Rögzítő"].Width = 150;
            TablaNaplo.Columns["Rögzítés Dátuma"].Width = 105;
        }

        /// <summary>
        /// Betölti és megjeleníti az utólagos karbantartási műveleteket a TáblaMűveletben
        /// </summary>
        private void TablaListazasMuvelet()
        {
            try
            {
                TablaMuvelet.CleanFilterAndSort();
                TablaMuvelet.DataSource = null;
                AdatTablaMuvelet = new DataTable();
                AdatTablaMuvelet.Columns.Clear();
                AdatTablaMuvelet.Columns.Add("Sorszám");
                AdatTablaMuvelet.Columns.Add("Művelet");
                AdatTablaMuvelet.Columns.Add("Státusz");
                AdatTablaMuvelet.Columns.Add("Nap");
                AdatTablaMuvelet.Columns.Add("Óra");

                AdatokMuvelet = Kez_Muvelet.Lista_Adatok();
                AdatTablaMuvelet.Clear();

                foreach (Adat_Eszterga_Muveletek rekord in AdatokMuvelet)
                {
                    DataRow Soradat = AdatTablaMuvelet.NewRow();

                    Soradat["Sorszám"] = rekord.ID;
                    Soradat["Művelet"] = rekord.Művelet;
                    Soradat["Státusz"] = rekord.Státus ? "Törölt" : "Aktív";
                    Soradat["Nap"] = rekord.Mennyi_Dátum;
                    Soradat["Óra"] = rekord.Mennyi_Óra;
                    AdatTablaMuvelet.Rows.Add(Soradat);
                }

                TablaMuvelet.DataSource = AdatTablaMuvelet;
                OszlopSzelessegMuvelet();
                TablaMuvelet.Visible = true;
                TablaSzinezes(TablaMuvelet);
                TablaMuvelet.ClearSelection();
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
        /// Az utólagos művelet TáblaMűvelet oszlopszélességeit állítja be
        /// </summary>
        private void OszlopSzelessegMuvelet()
        {
            TablaMuvelet.Columns["Sorszám"].Width = 100;
            TablaMuvelet.Columns["Művelet"].Width = 1245;
            TablaMuvelet.Columns["Státusz"].Width = 100;

            TablaMuvelet.Columns["Nap"].Visible = false;
            TablaMuvelet.Columns["Óra"].Visible = false;
        }
        #endregion

        #region Egyseg

        /// <summary>
        /// Az esztergagép karbantartási egységeit leíró felsorolás.
        /// Meghatározza, hogy a karbantartási művelet milyen típusú ütemezés szerint történik.
        /// </summary>
        public enum EsztergaEgyseg
        {
            Dátum = 1,
            Üzemóra = 2,
            Bekövetkezés = 3
        }
        #endregion

        #region Metodusok

        /// <summary>
        /// Új üzemóra rekordot ad hozzá az adatbázishoz a megadott dátum, üzemóra érték és státusz alapján.
        /// Az új üzemórát csak akkor rögzíti, ha az érték az előző és következő üzemóra értékek között helyezkedik el.
        /// Ha a feltételek nem teljesülnek, akkor figyelmeztetést ad, és nem rögzíti az új üzemórát.
        /// </summary>
        private bool UjUzemoraHozzaadasa(DateTime UjDatum, long UjUzemora, bool UjStatus)
        {
            bool Eredmeny = true;
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
                {
                    Eredmeny = false;
                    throw new HibásBevittAdat($"Az üzemóra értéknek az előző: {ElozoUzemora} és következő: {UtanaUzemora} közé kell esnie.");
                }

                Adat_Eszterga_Uzemora ADAT = new Adat_Eszterga_Uzemora(0,
                                                  UjUzemora,
                                                  UjDatum,
                                                  UjStatus);
                Kez_Uzemora.Rogzites(ADAT);
                MessageBox.Show("Az üzemóra rögzítése megtörtént.", "Rögzítve.", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            return Eredmeny;
        }

        /// <summary>
        /// Ellenőrzi, hogy az adott dátumhoz tartozik-e már üzemóra bejegyzés.
        /// Ha nem létezik, létrehoz egy újat a megadott érték alapján.
        /// </summary>
        private bool UjUzemora(DateTime datum, bool status)
        {
            bool Eredmeny = true;
            try
            {
                if (!int.TryParse(TxtBxUzemora.Text, out int uzemora))
                {
                    Eredmeny = false;
                    throw new HibásBevittAdat("Hibás üzemóra érték! Kérlek, csak számot adj meg.");
                }

                Adat_Eszterga_Uzemora VanEUzemora = AdatokUzemora
                    .FirstOrDefault(u => u.Dátum.Date == datum.Date && !u.Státus);

                bool sikeres = true;

                if (VanEUzemora == null)
                    sikeres = UjUzemoraHozzaadasa(datum, uzemora, status);

                if (!sikeres)
                    Eredmeny = false;
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
            return Eredmeny;
        }

        /// <summary>
        /// Új naplóbejegyzés(ek) létrehozása a kijelölt műveletek alapján a megadott dátumra.
        /// Előtte ellenőrzi, hogy a bejegyzés nem ismétlődik-e, valamint az üzemóra mező is érvényes-e.
        /// </summary>
        private bool UjNaplozas()
        {
            bool Eredmeny = true;
            try
            {
                if (TablaMuvelet.SelectedRows[0].Cells["Státusz"].Value?.ToStrTrim() == "Törölt")
                {
                    Eredmeny = false;
                    throw new HibásBevittAdat("Törölt műveletet nem lehet naplózni");
                }

                if (TxtBxMegjegyzes.Text == "")
                {
                    Eredmeny = false;
                    throw new HibásBevittAdat("A megjegyzés mező nem lehet üres.");
                }
                // JAVÍTANDÓ:a dátum az nem dátum?
                //kesz
                DateTime datum = DtmPckr.Value;
                string megjegyzes = TxtBxMegjegyzes.Text.Trim();

                if (!UjUzemora(datum, false))
                    return false;

                List<Adat_Eszterga_Muveletek_Naplo> naploLista = new List<Adat_Eszterga_Muveletek_Naplo>();
                foreach (DataGridViewRow sor in TablaMuvelet.SelectedRows)
                {
                    int id = sor.Cells[0].Value.ToÉrt_Int();

                    Adat_Eszterga_Muveletek rekord = AdatokMuvelet.FirstOrDefault(a => a.ID == id);

                    bool VanE = AdatokMuveletNaplo.Any(a => a.ID == id && a.Utolsó_Dátum.Date == datum);
                    if (VanE)
                    {
                        Eredmeny = false;
                        throw new HibásBevittAdat("Erre a dátumra már rögzítve lett ez a feladat egyszer.");
                    }
                    int MennyiNap = sor.Cells["Nap"].Value.ToÉrt_Int();
                    int MennyiÓra = sor.Cells["Óra"].Value.ToÉrt_Int();

                    long utolsoUzemora = TxtBxUzemora.Text.ToÉrt_Long();

                    Adat_Eszterga_Muveletek_Naplo adat = new Adat_Eszterga_Muveletek_Naplo(
                        id,
                        rekord.Művelet,
                        MennyiNap,
                        MennyiÓra,
                        datum,
                        utolsoUzemora,
                        megjegyzes,
                        Program.PostásNév.ToStrTrim(),
                        DateTime.Today);

                    naploLista.Add(adat);
                }
                // JAVÍTANDÓ:   Nincs Év
                //kesz
                Kez_Muvelet_Naplo.Rogzites(naploLista, DtmPckr.Value.Year);
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
            return Eredmeny;
        }

        /// <summary>
        /// Keres egy üzemórát az adatbázisban a megadott feltételek alapján.
        /// Az üzemóra keresése az 'Üzemóra', 'Dátum' és 'Bekövetkezés' egységek szerint történik.
        /// Ha a 'Bekövetkezés' egységet választjuk, akkor a függvény null-t ad vissza.
        /// A 'Üzemóra' és 'Dátum' esetén az adatokat az AdatokUzemora lista alapján keresük.
        /// </summary>
        private Adat_Eszterga_Uzemora KeresUzemora(long uzemora, DateTime datum, EsztergaEgyseg egyseg)
        {
            Adat_Eszterga_Uzemora Eredmeny = null;
            try
            {
                switch (egyseg)
                {
                    case EsztergaEgyseg.Bekövetkezés:
                        Eredmeny = null;
                        break;
                    case EsztergaEgyseg.Üzemóra:
                        Eredmeny = AdatokUzemora.FirstOrDefault(u => u.Uzemora == uzemora && !u.Státus);
                        break;
                    case EsztergaEgyseg.Dátum:
                        Eredmeny = AdatokUzemora.FirstOrDefault(u => u.Dátum.Date == datum.Date && !u.Státus);
                        break;
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
            return Eredmeny;
        }

        /// <summary>
        /// Beolvassa az adott dátumhoz tartozó üzemóra adatot, és megjeleníti a megadott szövegdobozban.
        /// Ha nincs ilyen adat, akkor alapértelmezetten 0-t állít be és szerkeszthetővé teszi a mezőt.
        /// </summary>
        private void UzemoraKiolvasasEsBeiras(DateTime datum, TextBox txt)
        {
            try
            {
                AdatokUzemora = Kez_Uzemora.Lista_Adatok();
                Adat_Eszterga_Uzemora uzemoraRekord = KeresUzemora(0, datum, EsztergaEgyseg.Dátum);
                if (uzemoraRekord != null)
                {
                    txt.Text = uzemoraRekord.Uzemora.ToStrTrim();
                    txt.Enabled = false;
                }
                else
                {
                    txt.Text = "0";
                    txt.Enabled = true;
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
        /// Az utólagos naplóbejegyzések módosítását végzi.
        /// Ellenőrzi, hogy legalább egy sor ki legyen választva a napló táblában,
        /// majd az új értékek alapján frissíti a memóriában lévő naplóadatokat és adatbázisban is módosítja őket.
        /// Hibák esetén megfelelő üzenetet jelenít meg.
        /// </summary>
        private void NaploModositas()
        {
            // JAVÍTANDÓ:
            //Kesz
            try
            {
                DateTime datum = DtmPckr.Value;
                if (!UjUzemora(datum, false))
                    return;

                foreach (DataGridViewRow sor in TablaNaplo.SelectedRows)
                {
                    int id = sor.Cells["Művelet Sorszáma"].Value.ToÉrt_Int();
                    DateTime eredetiDatum = sor.Cells["Utolsó Dátum"].Value.ToÉrt_DaTeTime();

                    Adat_Eszterga_Muveletek_Naplo eredeti = AdatokMuveletNaplo.FirstOrDefault(
                        a => a.ID == id && a.Utolsó_Dátum.Date == eredetiDatum.Date);

                    DateTime ujDatum = DtmPckr.Value.Date;
                    long ujUzemora = TxtBxUzemora.Text.ToÉrt_Long();
                    string ujMegjegyzes = TxtBxMegjegyzes.Text.Trim();

                    bool Valtozas =
                        ujDatum != eredeti.Utolsó_Dátum.Date ||
                        ujUzemora != eredeti.Utolsó_Üzemóra_Állás ||
                        ujMegjegyzes != eredeti.Megjegyzés;

                    if (!Valtozas)
                        continue;

                    Adat_Eszterga_Muveletek_Naplo modositott = new Adat_Eszterga_Muveletek_Naplo(
                        id,
                        eredeti.Művelet,
                        eredeti.Mennyi_Dátum,
                        eredeti.Mennyi_Óra,
                        ujDatum,
                        ujUzemora,
                        ujMegjegyzes,
                        Program.PostásNév.ToStrTrim(),
                        DateTime.Today
                    );
                    Kez_Muvelet_Naplo.Modositas(modositott, eredetiDatum, DtmPckr.Value.Year);
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

        #region Gombok,Muveletek

        /// <summary>
        /// Művelet naplózása vagy meglévő napló módosítása a kiválasztott sor alapján.
        /// Ha a műveletlistából van kiválasztva sor, új naplóbejegyzést hoz létre.
        /// Ha a naplóból van kiválasztva sor, akkor azt módosítja.
        /// </summary>
        private void Btn_Modosit_Click(object sender, EventArgs e)
        {
            try
            {
                if (TablaMuvelet.SelectedRows.Count == 0 && TablaNaplo.SelectedRows.Count == 0)
                    throw new HibásBevittAdat("Kérlek, válassz ki egy sort a listából!");

                if (DtmPckr.Value.Date > DateTime.Today)
                    throw new HibásBevittAdat("A kiválasztott dátum nem lehet későbbi, mint a mai dátum.");

                bool sikeres = true;
                if (TablaMuvelet.SelectedRows.Count != 0)
                    sikeres = UjNaplozas();
                else if (TablaNaplo.SelectedRows.Count != 0)
                    NaploModositas();

                if (sikeres)
                {
                    TablaNaploListazas(DtmPckr.Value.Year);
                    MessageBox.Show("Sikeres rögzítés a naplóba.", "Rögzítve", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// Ha a napló táblában új sort választunk ki, akkor a dátum, üzemóra és megjegyzés mezők frissülnek az adott rekord alapján.
        /// Emellett törli a művelet táblából a kijelölést.
        /// </summary>
        private void TablaNaplo_SelectionChanged(object sender, EventArgs e)
        {
            if (frissul || !TablaNaplo.Focused || TablaNaplo.SelectedRows.Count != 1)
                return;

            frissul = true;
            try
            {
                TablaMuvelet.ClearSelection();

                DataGridViewRow sor = TablaNaplo.SelectedRows[0];

                DtmPckr.Value = sor.Cells["Utolsó Dátum"].Value.ToÉrt_DaTeTime();
                TxtBxUzemora.Text = sor.Cells["Utolsó Üzemóra"].Value.ToStrTrim();
                TxtBxMegjegyzes.Text = sor.Cells["Megjegyzés"].Value.ToStrTrim();
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
            finally { frissul = false; }
        }

        /// <summary>
        /// Ha a művelet táblában új sort választunk ki, az aznapi dátum, az aktuális üzemóra (ha van), és egy üres megjegyzés jelenik meg.
        /// Emellett törli a napló táblából a kijelölést.
        /// </summary>
        private void TablaMuvelet_SelectionChanged(object sender, EventArgs e)
        {
            if (frissul || !TablaMuvelet.Focused || TablaMuvelet.SelectedRows.Count != 1)
                return;

            frissul = true;
            try
            {
                TablaNaplo.ClearSelection();

                DateTime maiNap = DateTime.Today;
                DtmPckr.Value = maiNap;

                UzemoraKiolvasasEsBeiras(maiNap, TxtBxUzemora);

                TxtBxMegjegyzes.Text = string.Empty;
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
            finally { frissul = false; }
        }

        /// <summary>
        /// A dátumválasztó módosítására frissíti a megjelenített adatokat.
        /// Ha más évre váltunk, új adatbázist tölt be.
        /// Ha a választott dátumhoz nem tartozik rögzített üzemóra, akkor engedélyezi az üzemóra mezőt.
        /// </summary>
        private void DtmPckr_ValueChanged(object sender, EventArgs e)
        {
            if (frissul) return;
            frissul = true;

            try
            {
                DateTime AktualisDatum = DtmPckr.Value;

                if (AktualisDatum > DateTime.Today)
                {
                    DtmPckr.Value = DateTime.Today;
                    UzemoraKiolvasasEsBeiras(DateTime.Today, TxtBxUzemora);
                    throw new HibásBevittAdat($"A választott dátum nem lehet később mint a mai nap {DateTime.Today}");
                }

                if (AktualisDatum.Year != elozoEv)
                {
                    string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Eszterga_Karbantartás_{AktualisDatum.Year}_Napló.mdb".KönyvSzerk();

                    if (!File.Exists(hely))
                    {
                        DtmPckr.Value = DateTime.Today;
                        throw new HibásBevittAdat($"A {AktualisDatum.Year}. évhez nem található napló adatbázis.");
                    }

                    TablaNaploListazas(AktualisDatum.Year);
                    elozoEv = AktualisDatum.Year;
                }
                Adat_Eszterga_Uzemora uzemoraRekord = KeresUzemora(0, AktualisDatum, EsztergaEgyseg.Dátum);

                if (uzemoraRekord != null)
                {
                    TxtBxUzemora.Text = uzemoraRekord.Uzemora.ToStrTrim();
                    TxtBxUzemora.Enabled = false;
                }
                else
                {
                    TxtBxUzemora.Text = "0";
                    TxtBxUzemora.Enabled = true;
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\nA hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { frissul = false; }
        }

        /// <summary>
        /// A művelet táblázat adatainak betöltése után meghívódik a sorok színezésének frissítésére.
        /// </summary>
        private void TablaMuvelet_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            TablaSzinezes(TablaMuvelet);
        }
        #endregion
    }
}
