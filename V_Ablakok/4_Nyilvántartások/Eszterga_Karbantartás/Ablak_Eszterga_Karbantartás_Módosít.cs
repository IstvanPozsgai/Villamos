using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.V_Ablakok._4_Nyilvántartások.Eszterga_Karbantartás;
using Villamos.Villamos_Adatszerkezet;
using Villamos.Villamos_Kezelők;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.Kerékeszterga
{
    public partial class Ablak_Eszterga_Karbantartás_Módosít : Form
    {
        #region Osztalyszintű elemek
        public event Event_Kidobó Eszterga_Valtozas;
        // JAVÍTANDÓ:
        readonly bool Baross = Program.PostásTelephely.Trim() == "Angyalföld";
        private bool frissul = false;
        DataTable AdatTabla = new DataTable();
        DataTable AdatTablaUtolag = new DataTable();
        DataTable AdatTablaNaplo = new DataTable();
        #endregion

        #region Listák
        List<Adat_Eszterga_Muveletek> AdatokMuvelet = new List<Adat_Eszterga_Muveletek>();
        List<Adat_Eszterga_Uzemora> AdatokUzemora = new List<Adat_Eszterga_Uzemora>();
        List<Adat_Eszterga_Muveletek_Naplo> AdatokMuveletNaplo = new List<Adat_Eszterga_Muveletek_Naplo>();
        #endregion

        #region Kezelők
        readonly Kezelo_Eszterga_Muveletek Kez_Muvelet = new Kezelo_Eszterga_Muveletek();
        readonly Kezelo_Eszterga_Muveletek_Naplo Kez_Muvelet_Naplo = new Kezelo_Eszterga_Muveletek_Naplo();
        readonly Kezelo_Eszterga_Uzemora Kez_Uzemora = new Kezelo_Eszterga_Uzemora();
        #endregion

        #region Alap
        /// <summary>
        /// Ablak inicializálása és adatok betöltése a vezérlőelemekbe
        /// </summary>
        public Ablak_Eszterga_Karbantartás_Módosít()
        {
            InitializeComponent();
            TablaListazasMuvelet();
            TablaNaploListazas();
            TablaListazasMuveletUtolag();
            TxtBxId.Enabled = false;
            CmbxEgység.DataSource = Enum.GetValues(typeof(EsztergaEgyseg));
        }
        /// <summary>
        /// Az ablak betöltésekor lefutó inicializálási műveletek
        /// </summary>
        private void Ablak_Eszterga_Karbantartás_Módosít_Load(object sender, EventArgs e)
        {
            Eszterga_Valtozas?.Invoke();
            TablaMuvelet.ClearSelection();
            TablaNaplo.ClearSelection();
            TablaUtolagMuvelet.ClearSelection();
            JogosultsagKiosztas();
            Btn_Csere.Visible = false;
            Btn_Sorrend.Visible = false;
            // A DataGridView adatforrásának kötése után automatikusan meghívja a ToroltTablaSzinezes metódust,
            // hogy a törölt státuszú sorokat színezve jelenítse meg.
            TablaMuvelet.DataBindingComplete += (s, ev) => ToroltTablaSzinezes(TablaMuvelet);
            TablaUtolagMuvelet.DataBindingComplete += (s, ev) => ToroltTablaSzinezes(TablaUtolagMuvelet);
            EgysegBeallitasa();
            UzemoraKiolvasasEsBeiras(DtmPckrUtolagos.Value, TxtBxUtolagUzemora);
            TxtBxMennyiNap.Text = "0";
        }
        /// <summary>
        /// Jogosultságok alapján gombok láthatóságát és engedélyezettségét állítja be
        /// </summary>
        private void JogosultsagKiosztas()
        {
            try
            {
                int melyikelem = 160;
                Btn_Módosít.Visible = Baross;
                Btn_Sorrend.Visible = Baross;
                Btn_Törlés.Visible = Baross;
                Btn_ÚjFelvétel.Visible = Baross;
                Btn_Csere.Visible = Baross;

                // módosítás 1 
                //Ablak_Eszterga_Karbantartás_Segéd oldal használja az 1. módosításokat

                // módosítás 2
                //Ablak_Eszterga_Karbantartás oldal használja a 2. módosításokat

                // módosítás 3 
                Btn_Módosít.Enabled = MyF.Vanjoga(melyikelem, 3);
                Btn_Sorrend.Enabled = MyF.Vanjoga(melyikelem, 3);
                Btn_Törlés.Enabled = MyF.Vanjoga(melyikelem, 3);
                Btn_ÚjFelvétel.Enabled = MyF.Vanjoga(melyikelem, 3);
                Btn_Csere.Enabled = MyF.Vanjoga(melyikelem, 3);
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

        #region Egyseg
        public enum EsztergaEgyseg
        {
            Dátum = 1,
            Üzemóra = 2,
            Bekövetkezés = 3
        }

        /// <summary>
        /// Beállítja az űrlap különböző mezőinek engedélyezettségét és értékeit az adott egység típusának megfelelően.
        /// A metódus az egység típusától függően engedélyezi vagy letiltja a napi és üzemóra mezőket, valamint az utolsó üzemóra állás és utolsó dátum mezőket.
        /// Három egységtípus kezelése történik:
        /// Dátum
        /// Csak a dátum alapú beállítás engedélyezett, az üzemóra mezők tiltva és értékük 0-ra állítva. Az utolsó üzemóra állás értéke a hozzá tartozó rekord alapján töltődik
        /// Üzemóra
        /// Csak az üzemóra mezők engedélyezettek, a dátum mező letiltva és az érték 1900.01.01-re állítva vagy az üzemóra alapján lekért rekord dátumára.
        /// Bekövetkezés
        /// Mind a dátum, mind az üzemóra mezők engedélyezettek, nincs tiltás.
        /// </summary>
        private void EgysegEllenorzes(string Egyseg)
        {
            try
            {
                TxtBxMennyiÓra.Enabled = true;
                TxtBxMennyiNap.Enabled = true;
                TxtBxUtolsóÜzemóraÁllás.Enabled = true;
                DtmPckrUtolsóDátum.Enabled = true;

                switch (Egyseg)
                {
                    case "Dátum":
                        TxtBxMennyiÓra.Enabled = false;
                        TxtBxMennyiÓra.Text = "0";
                        TxtBxUtolsóÜzemóraÁllás.Enabled = false;

                        Adat_Eszterga_Uzemora uzemoraRekordDatum = KeresÜzemóra(0, DtmPckrUtolsóDátum.Value, EsztergaEgyseg.Dátum);
                        TxtBxUtolsóÜzemóraÁllás.Text = uzemoraRekordDatum != null ? uzemoraRekordDatum.Uzemora.ToStrTrim() : "0";
                        break;

                    case "Üzemóra":
                        TxtBxMennyiNap.Enabled = false;
                        TxtBxMennyiNap.Text = "0";
                        DtmPckrUtolsóDátum.Enabled = false;

                        if (long.TryParse(TxtBxUtolsóÜzemóraÁllás.Text, out long uzemora))
                        {
                            Adat_Eszterga_Uzemora uzemoraRekordUzemora = KeresÜzemóra(uzemora, DateTime.MinValue, EsztergaEgyseg.Üzemóra);
                            DtmPckrUtolsóDátum.Value = uzemoraRekordUzemora?.Dátum ?? new DateTime(1900, 1, 1);
                        }
                        else
                            DtmPckrUtolsóDátum.Value = new DateTime(1900, 1, 1);
                        break;

                    case "Bekövetkezés":
                        TxtBxMennyiÓra.Enabled = true;
                        TxtBxMennyiNap.Enabled = true;
                        TxtBxUtolsóÜzemóraÁllás.Enabled = true;
                        DtmPckrUtolsóDátum.Enabled = true;
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
        }

        /// <summary>
        /// Kezeli az egység kiválasztásának változását a legördülő listában.
        /// Az újonnan kiválasztott egység alapján ellenőrzi annak érvényességét vagy egyéb logikát hajt végre.
        /// </summary>
        private void CmbxEgység_SelectedIndexChanged(object sender, EventArgs e)
        {
            string kivalasztottEgyseg = CmbxEgység.SelectedItem.ToStrTrim();
            EgysegEllenorzes(kivalasztottEgyseg);
        }

        /// <summary>
        /// Beállítja az egységet az alapértelmezett (Bekövetkezés) értékre,
        /// lekéri az aktuális sorszámot és megjeleníti azt a megfelelő mezőben,
        /// majd ellenőrzi az egység helyességét.
        /// </summary>
        private void EgysegBeallitasa()
        {
            try
            {
                CmbxEgység.SelectedItem = EsztergaEgyseg.Bekövetkezés;
                // JAVÍTANDÓ:miért kell tudnunk itt , hogy mi az ID? kezelőben a helye
                //Nem ezt beszéltük meg.
                TxtBxId.Text = Kez_Muvelet.Sorszam().ToStrTrim();
                EgysegEllenorzes(EsztergaEgyseg.Bekövetkezés.ToStrTrim());
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

        #region Táblák Listázása
        /// <summary>
        /// A karbantartási műveletek adatainak betöltése és megjelenítése a TáblaMűveletbe
        /// </summary>
        private void TablaListazasMuvelet()
        {
            try
            {
                TablaMuvelet.DataSource = null;
                AdatTabla = new DataTable();
                AdatTabla.Columns.Clear();
                AdatTabla.Columns.Add("Sorszám");
                AdatTabla.Columns.Add("Művelet");
                AdatTabla.Columns.Add("Egység");
                AdatTabla.Columns.Add("Nap");
                AdatTabla.Columns.Add("Óra");
                AdatTabla.Columns.Add("Státusz");
                AdatTabla.Columns.Add("Utolsó Dátum");
                AdatTabla.Columns.Add("Utolsó Üzemóra");

                AdatokMuvelet = Kez_Muvelet.Lista_Adatok();
                AdatokUzemora = Kez_Uzemora.Lista_Adatok();
                AdatTabla.Clear();

                foreach (Adat_Eszterga_Muveletek rekord in AdatokMuvelet)
                {
                    DataRow Soradat = AdatTabla.NewRow();

                    Soradat["Sorszám"] = rekord.ID;
                    Soradat["Művelet"] = rekord.Művelet;
                    Soradat["Egység"] = Enum.GetName(typeof(EsztergaEgyseg), rekord.Egység);
                    Soradat["Nap"] = rekord.Mennyi_Dátum;
                    Soradat["Óra"] = rekord.Mennyi_Óra;
                    Soradat["Státusz"] = rekord.Státus ? "Törölt" : "Aktív";
                    Soradat["Utolsó Dátum"] = rekord.Utolsó_Dátum.ToShortDateString();
                    Adat_Eszterga_Uzemora uzemoraRekord = AdatokUzemora
                        .FirstOrDefault(a => a.Dátum.Date == rekord.Utolsó_Dátum.Date && a.Státus == false);

                    if (uzemoraRekord != null)
                        Soradat["Utolsó Üzemóra"] = uzemoraRekord.Uzemora;
                    else
                        Soradat["Utolsó Üzemóra"] = rekord.Utolsó_Üzemóra_Állás;

                    AdatTabla.Rows.Add(Soradat);
                }

                TablaMuvelet.DataSource = AdatTabla;
                OszlopSzelessegMuvelet();
                ToroltTablaSzinezes(TablaMuvelet);
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
        /// A karbantartási művelet tábla oszlopszélességeit állítja be
        /// </summary>
        private void OszlopSzelessegMuvelet()
        {
            TablaMuvelet.Columns["Sorszám"].Width = 100;
            TablaMuvelet.Columns["Művelet"].Width = 1155;
            TablaMuvelet.Columns["Egység"].Width = 110;
            TablaMuvelet.Columns["Nap"].Width = 60;
            TablaMuvelet.Columns["Óra"].Width = 60;
            TablaMuvelet.Columns["Státusz"].Width = 85;
            TablaMuvelet.Columns["Utolsó Dátum"].Width = 120;
            TablaMuvelet.Columns["Utolsó Üzemóra"].Width = 160;
        }

        /// <summary>
        /// A karbantartási műveletek naplóbejegyzései betöltése és megjelenítése a TáblaMűveletbe
        /// </summary>
        private void TablaNaploListazas()
        {
            try
            {
                TablaNaplo.DataSource = null;
                AdatTablaNaplo = new DataTable();
                AdatTablaNaplo.Columns.Add("Művelet Sorszáma");
                AdatTablaNaplo.Columns.Add("Művelet");
                AdatTablaNaplo.Columns.Add("Utolsó Dátum");
                AdatTablaNaplo.Columns.Add("Utolsó Üzemóra");
                AdatTablaNaplo.Columns.Add("Megjegyzés");
                AdatTablaNaplo.Columns.Add("Rögzítő");
                AdatTablaNaplo.Columns.Add("Rögzítés Dátuma");

                AdatokMuveletNaplo = Kez_Muvelet_Naplo.Lista_Adatok()
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
            TablaNaplo.Columns["Megjegyzés"].Width = 220;
            TablaNaplo.Columns["Rögzítő"].Width = 150;
            TablaNaplo.Columns["Rögzítés Dátuma"].Width = 105;
        }


        /// <summary>
        /// Betölti és megjeleníti az utólagos karbantartási műveleteket a TáblaMűveletben
        /// </summary>
        private void TablaListazasMuveletUtolag()
        {
            try
            {
                TablaUtolagMuvelet.DataSource = null;
                AdatTablaUtolag = new DataTable();
                AdatTablaUtolag.Columns.Clear();
                AdatTablaUtolag.Columns.Add("Sorszám");
                AdatTablaUtolag.Columns.Add("Művelet");
                AdatTablaUtolag.Columns.Add("Státusz");
                AdatTablaUtolag.Columns.Add("Nap");
                AdatTablaUtolag.Columns.Add("Óra");

                AdatokMuvelet = Kez_Muvelet.Lista_Adatok();
                AdatTablaUtolag.Clear();

                foreach (Adat_Eszterga_Muveletek rekord in AdatokMuvelet)
                {
                    DataRow Soradat = AdatTablaUtolag.NewRow();

                    Soradat["Sorszám"] = rekord.ID;
                    Soradat["Művelet"] = rekord.Művelet;
                    Soradat["Státusz"] = rekord.Státus ? "Törölt" : "Aktív";
                    Soradat["Nap"] = rekord.Mennyi_Dátum;
                    Soradat["Óra"] = rekord.Mennyi_Óra;
                    AdatTablaUtolag.Rows.Add(Soradat);
                }

                TablaUtolagMuvelet.DataSource = AdatTablaUtolag;
                OszlopSzelessegMuveletUtolag();
                TablaUtolagMuvelet.Visible = true;
                ToroltTablaSzinezes(TablaUtolagMuvelet);
                TablaUtolagMuvelet.ClearSelection();
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
        private void OszlopSzelessegMuveletUtolag()
        {
            TablaUtolagMuvelet.Columns["Sorszám"].Width = 100;
            TablaUtolagMuvelet.Columns["Művelet"].Width = 1160;
            TablaUtolagMuvelet.Columns["Státusz"].Width = 100;

            TablaUtolagMuvelet.Columns["Nap"].Visible = false;
            TablaUtolagMuvelet.Columns["Óra"].Visible = false;
        }

        #endregion

        #region Metodusok

        /// <summary>
        /// Ellenőrzi a törlés gomb láthatóságát a státusz checkbox alapján
        /// </summary>
        private void TorlesEllenorzes()
        {
            Btn_Törlés.Visible = !ChckBxStátus.Checked;
        }

        /// <summary>
        /// Ellenőrzi a megadott adatokat a felhasználói űrlapon. 
        /// Új rekord hozzáadása esetén ellenőrzi, hogy az azonosító már létezik-e az adatbázisban, 
        /// illetve biztosítja, hogy minden mező érvényes adatokat tartalmazzon.
        /// </summary>
        private void TxtBxEllenorzes(bool ujRekord)
        {
            try
            {
                bool VanE = AdatokMuvelet.Any(a => a.ID == TxtBxId.Text.ToÉrt_Int());

                //Ujrekordnal nem ellenőrizzük, hogy van-e kiválasztott sor
                if (!ujRekord && TablaMuvelet.SelectedRows.Count < 1)
                    throw new HibásBevittAdat("Nincs kiválasztott művelet.");

                if (ujRekord && VanE)
                    throw new HibásBevittAdat("Az azonosító már létezik az adatbázisban.");

                if (string.IsNullOrEmpty(TxtBxId.Text))
                    throw new HibásBevittAdat("Töltse ki az Azonosító mezőt.");

                if (!ujRekord && !VanE)
                    throw new HibásBevittAdat("Az azonosító nem található az adatbázisban.");

                // JAVÍTANDÓ:a true mikor lesz false?
                //kesz
                //A törlésnél megvan hivva és ott nem lehet uj adatot torolni szoval ezert ezeket kihagyja, és alapértelmezetten false
                //olvasd el mégegyszer!!!!!!!!!!!!
                if (ujRekord)
                {
                    string Egyseg = CmbxEgység.SelectedItem?.ToStrTrim();
                    bool Nap = int.TryParse(TxtBxMennyiNap.Text, out int MennyiNap);
                    bool Ora = int.TryParse(TxtBxMennyiÓra.Text, out int MennyiÓra);

                    if (Egyseg == "Dátum" && (!Nap || MennyiNap <= 0))
                        throw new HibásBevittAdat("A Nap mezőben csak pozitív egész szám szerepelhet.");

                    else if (Egyseg == "Üzemóra" && (!Ora || MennyiÓra <= 0))
                        throw new HibásBevittAdat("Az Óra mezőben csak pozitív egész szám szerepelhet.");

                    else if (Egyseg == "Bekövetkezés" && (!Nap || !Ora || MennyiNap <= 0 || MennyiÓra <= 0))
                        throw new HibásBevittAdat("A Nap és Óra mezőkben csak pozitív egész szám szerepelhetnek.");

                    if (string.IsNullOrEmpty(TxtBxMűvelet.Text))
                        throw new HibásBevittAdat("Töltse ki a Művelet mezőt.");

                    if (Egyseg == "Üzemóra" || Egyseg == "Bekövetkezés")
                    {
                        AdatokUzemora = Kez_Uzemora.Lista_Adatok();

                        if (string.IsNullOrEmpty(TxtBxUtolsóÜzemóraÁllás.Text) || TxtBxUtolsóÜzemóraÁllás.Text == "0" ||
                            !long.TryParse(TxtBxUtolsóÜzemóraÁllás.Text, out _))
                            throw new HibásBevittAdat("Az Utolsó Üzemóra Állás mező csak pozitív egész számot tartalmazhat.");

                        else
                        {
                            long aktualisUzemora = AdatokUzemora.Count > 0 ? AdatokUzemora.Max(u => u.Uzemora) : 0;
                            if (long.Parse(TxtBxUtolsóÜzemóraÁllás.Text) > aktualisUzemora)
                                throw new HibásBevittAdat("Az Utolsó Üzemóra Állás nem lehet nagyobb, mint az aktuális Üzemóra érték.");
                        }
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
        /// Kiválasztja a kijelölt sorokat a TáblaMűveletből, és visszaadja az id-jük alapján a megfelelő rekordokat
        /// az adatbázisból.
        /// </summary>
        private List<Adat_Eszterga_Muveletek> SorKivalasztas()
        {
            try
            {
                List<Adat_Eszterga_Muveletek> rekordok = new List<Adat_Eszterga_Muveletek>();

                foreach (DataGridViewRow sor in TablaMuvelet.SelectedRows)
                {
                    int id = sor.Cells[0].Value.ToÉrt_Int();

                    AdatokMuvelet = Kez_Muvelet.Lista_Adatok();

                    Adat_Eszterga_Muveletek rekord = (from a in AdatokMuvelet
                                                      where a.ID == id
                                                      select a).FirstOrDefault();

                    rekordok.Add(rekord);
                }

                return rekordok;

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw;
                // JAVÍTANDÓ:
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        /// <summary>
        /// Ellenőrzi, hogy a megadott adat megegyezik-e az adatbázisban lévővel.
        /// Műveleti mód esetén a művelet adatait, üzemóra mód esetén pedig az üzemórát és dátumot ellenőrzi.
        /// </summary>
        private bool ModositasEll(bool Muvelet)
        {
            try
            {
                AdatokMuvelet = Kez_Muvelet.Lista_Adatok();

                Adat_Eszterga_Muveletek rekord = AdatokMuvelet.FirstOrDefault(a => a.ID == int.Parse(TxtBxId.Text));

                Enum.TryParse(CmbxEgység.SelectedItem.ToStrTrim(), out EsztergaEgyseg egyseg);

                bool NincsValtozas =
                    rekord.Művelet.Trim() == TxtBxMűvelet.Text.Trim() &&
                    rekord.Egység == (int)egyseg &&
                    rekord.Státus == ChckBxStátus.Checked &&
                    rekord.Mennyi_Dátum == int.Parse(TxtBxMennyiNap.Text) &&
                    rekord.Mennyi_Óra == int.Parse(TxtBxMennyiÓra.Text) &&
                    rekord.Utolsó_Dátum == DtmPckrUtolsóDátum.Value &&
                    rekord.Utolsó_Üzemóra_Állás == int.Parse(TxtBxUtolsóÜzemóraÁllás.Text);

                if (NincsValtozas)
                    return false;
                // JAVÍTANDÓ:Mikor lesz igaz?
                //Ezt még gondold át mégegyszer van egyszerűbb megoldás is
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
            return Muvelet;
        }

        // JAVÍTANDÓ:Akkor tulajdonképpen táblát színez
        /// <summary>
        /// Színezi a táblázat sorait a státusz alapján, ha a státusz "Törölt".
        /// Ha a státusz "Törölt", a sor háttérszíne piros, szövege fekete, és áthúzott betűtípust kap.
        /// Ha a státusz nem "Törölt", visszaáll a szokásos megjelenítés fehér háttérre.
        /// </summary>
        private void ToroltTablaSzinezes(DataGridView tabla)
        {
            if (!tabla.Columns.Contains("Státusz"))
                return;

            foreach (DataGridViewRow sor in tabla.Rows)
            {
                if (sor.IsNewRow) continue;

                string statusz = sor.Cells["Státusz"].Value?.ToString().Trim();

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
        /// Keres egy üzemórát az adatbázisban a megadott feltételek alapján.
        /// Az üzemóra keresése az 'Üzemóra', 'Dátum' és 'Bekövetkezés' egységek szerint történik.
        /// Ha a 'Bekövetkezés' egységet választjuk, akkor a függvény null-t ad vissza.
        /// A 'Üzemóra' és 'Dátum' esetén az adatokat az AdatokUzemora lista alapján keresük.
        /// </summary>
        private Adat_Eszterga_Uzemora KeresÜzemóra(long uzemora, DateTime datum, EsztergaEgyseg egyseg)
        {
            try
            {
                if (egyseg == EsztergaEgyseg.Bekövetkezés)
                    return null;
                if (egyseg == EsztergaEgyseg.Üzemóra)
                    return AdatokUzemora.FirstOrDefault(u => u.Uzemora == uzemora && !u.Státus);
                if (egyseg == EsztergaEgyseg.Dátum)
                    return AdatokUzemora.FirstOrDefault(u => u.Dátum.Date == datum.Date && !u.Státus);
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
            // JAVÍTANDÓ:Ez miért kell?
            return null;
        }

        /// <summary>
        /// Az üzemórát kiolvassa az adatbázisból a megadott dátum alapján, és beírja a TextBox-ba.
        /// Ha található üzemóra rekord, akkor az üzemóra értékét beírja a TextBox-ba, és letiltja a szerkeszthetőséget.
        /// Ha nincs találat, akkor 0-t ír be és engedélyezi a TextBox szerkesztését.
        /// </summary>
        private void UzemoraKiolvasasEsBeiras(DateTime datum, TextBox txt)
        {
            try
            {
                Adat_Eszterga_Uzemora uzemoraRekord = KeresÜzemóra(0, datum, EsztergaEgyseg.Dátum);

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
        /// Új üzemóra rekordot ad hozzá az adatbázishoz a megadott dátum, üzemóra érték és státusz alapján.
        /// Az új üzemórát csak akkor rögzíti, ha az érték az előző és következő üzemóra értékek között helyezkedik el.
        /// Ha a feltételek nem teljesülnek, akkor figyelmeztetést ad, és nem rögzíti az új üzemórát.
        /// </summary>
        private bool UjUzemoraHozzaadasa(DateTime UjDatum, long UjUzemora, bool UjStatus)
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
            // JAVÍTANDÓ:Ez miért kell?
            return true;
        }

        #endregion

        #region Gombok,Muveletek

        /// <summary>
        /// Ha van kijelölt rekord, akkor módosítja azt az új adatokkal, ha nem, akkor új rekordot ad hozzá.
        /// Ellenőrzi, hogy a szükséges adatokat megfelelően kitöltötték-e, majd végrehajtja a módosítást vagy a hozzáadást.
        /// </summary>
        private void Btn_Módosít_Click(object sender, EventArgs e)
        {
            try
            {
                bool UjRekord = string.IsNullOrEmpty(TxtBxId.Text) || !AdatokMuvelet.Any(a => a.ID == TxtBxId.Text.ToÉrt_Int());

                TxtBxEllenorzes(UjRekord);

                if (!UjRekord && !ModositasEll(true))
                    throw new HibásBevittAdat("Nem történt változás.");

                // JAVÍTANDÓ: Nem azt mondtam, hogy nem vezethetsz be új változót
                if (!AdatokMuvelet.Any(a => a.ID == TxtBxId.Text.ToÉrt_Int()))
                    TxtBxId.Text = Kez_Muvelet.Sorszam().ToStrTrim();

                Adat_Eszterga_Muveletek ADAT = new Adat_Eszterga_Muveletek(
                    TxtBxId.Text.ToÉrt_Int(),
                    TxtBxMűvelet.Text.ToStrTrim(),
                    (int)CmbxEgység.SelectedItem,
                    TxtBxMennyiNap.Text.ToÉrt_Int(),
                    TxtBxMennyiÓra.Text.ToÉrt_Int(),
                    ChckBxStátus.Checked,
                    DtmPckrUtolsóDátum.Value.Date,
                    TxtBxUtolsóÜzemóraÁllás.Text.ToÉrt_Long()
                );

                if (AdatokMuvelet.Any(a => a.ID == ADAT.ID))
                    Kez_Muvelet.MeglevoMuvelet_Modositas(ADAT);
                else
                    Kez_Muvelet.Rogzites(ADAT);

                Eszterga_Valtozas?.Invoke();
                TablaListazasMuvelet();
                TablaListazasMuveletUtolag();
                TorlesEllenorzes();
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Rögzítve.", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// Ha van kijelölt sor, és az még nincs törölve, akkor a hozzá tartozó rekordot eltávolítja az adatbázisból.
        /// Először ellenőrzi a kijelölést és a sor állapotát (törölt-e), majd végrehajtja a törlést.
        /// </summary>
        private void Btn_Törlés_Click(object sender, EventArgs e)
        {
            try
            {
                TxtBxEllenorzes(false);

                foreach (DataGridViewRow row in TablaMuvelet.SelectedRows)
                    if (row.Cells[5].Value.ToStrTrim() == "Törölt")
                        throw new HibásBevittAdat("Csak olyan sorokat lehet törölni, amik nincsenek törölve.");

                AdatokMuvelet = Kez_Muvelet.Lista_Adatok();

                List<int> rekordok = new List<int>();
                foreach (DataGridViewRow row in TablaMuvelet.SelectedRows)
                    rekordok.Add(row.Cells[0].Value.ToÉrt_Int());

                List<Adat_Eszterga_Muveletek> TorlesAdatok = new List<Adat_Eszterga_Muveletek>();

                foreach (int Id in rekordok)
                {
                    Adat_Eszterga_Muveletek rekord = AdatokMuvelet.FirstOrDefault(a => a.ID == Id);
                    if (rekord != null)
                        TorlesAdatok.Add(new Adat_Eszterga_Muveletek(Id));
                }
                Kez_Muvelet.Torles(TorlesAdatok, true);
                Eszterga_Valtozas?.Invoke();
                TablaListazasMuvelet();
                TablaListazasMuveletUtolag();
                TorlesEllenorzes();
                Btn_Törlés.Visible = false;
                MessageBox.Show("Az adatok törlése megtörtént.", "Törölve.", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// Előkészíti az új rekord felvételéhez szükséges mezőket: új ID-t generál, kiüríti a mezőket, beállítja az alapértékeket,
        /// és lekéri az utolsó üzemóra állását a listából.
        /// </summary>
        private void Btn_ÚjFelvétel_Click(object sender, EventArgs e)
        {
            try
            {
                Btn_Törlés.Visible = false;
                AdatokMuvelet = Kez_Muvelet.Lista_Adatok();
                TxtBxId.Text = (AdatokMuvelet.Any() ? AdatokMuvelet.Max(a => a.ID) + 1 : 1).ToStrTrim();
                TxtBxMűvelet.Text = "";
                CmbxEgység.SelectedItem = EsztergaEgyseg.Bekövetkezés;
                TxtBxMennyiNap.Text = "0";
                TxtBxMennyiÓra.Text = "0";
                ChckBxStátus.Checked = false;
                DtmPckrUtolsóDátum.Value = DateTime.Today;
                Adat_Eszterga_Uzemora legutolsoUzemora = (from a in AdatokUzemora
                                                          where !a.Státus
                                                          orderby a.Dátum descending
                                                          select a).FirstOrDefault();

                TxtBxUtolsóÜzemóraÁllás.Text = legutolsoUzemora != null ? legutolsoUzemora.Uzemora.ToStrTrim() : "0";
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

        // JAVÍTANDÓ:Kezelőben
        //felig kesz
        /// Két kijelölt rekord sorrendjének felcserélése az adatbázisban.
        /// Ellenőrzi, hogy pontosan két sor van-e kijelölve, majd végrehajtja a cserét, frissíti a táblázatot.
        /// </summary>
        private void Btn_Csere_Click(object sender, EventArgs e)
        {
            try
            {
                if (TablaMuvelet.SelectedRows.Count != 2)
                    throw new HibásBevittAdat("A cseréhez 2 sor kijelölésére van szükség");

                List<Adat_Eszterga_Muveletek> rekordok = SorKivalasztas();

                Adat_Eszterga_Muveletek rekord1 = rekordok[0];
                Adat_Eszterga_Muveletek rekord2 = rekordok[1];

                Kez_Muvelet.MuveletCsere(rekord1, rekord2);
                Kez_Muvelet.Rendezes();
                TablaListazasMuvelet();
                TablaListazasMuveletUtolag();
                TorlesEllenorzes();
                MessageBox.Show("A sorok sorszámai sikeresen kicserélve.", "Sikeres csere", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        // JAVÍTANDÓ:ez miben tér el az előzőtől?
        /// <summary>
        /// Két kijelölt sor sorrendjét felcseréli az adatbázisban úgy, hogy az egyik rekord a másik helyére kerül.
        /// Először ellenőrzi, hogy pontosan két sor van-e kijelölve, majd a cserét végrehajtja, frissíti a listát.
        /// </summary>
        private void Btn_Sorrend_Click(object sender, EventArgs e)
        {
            try
            {
                if (TablaMuvelet.SelectedRows.Count != 2)
                    throw new HibásBevittAdat("A sorrend módosításához 2 sor kijelölésére van szükség");

                List<Adat_Eszterga_Muveletek> rekordok = SorKivalasztas();

                Adat_Eszterga_Muveletek elso = rekordok[1];
                Adat_Eszterga_Muveletek masodik = rekordok[0];
                int ElsoID = elso.ID;
                int MasodikID = masodik.ID;

                Kez_Muvelet.MuveletSorrend(ElsoID, MasodikID);
                Kez_Muvelet.Rendezes();
                TablaListazasMuvelet();
                TablaListazasMuveletUtolag();
                TorlesEllenorzes();
                MessageBox.Show("A sorrend módosítása sikeresen megtörtént.", "Sikeres módosítás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// A táblázat teljes tartalmát Excel fájlba menti, ha van legalább egy sor.
        /// A felhasználó kiválaszthatja a mentés helyét, majd a fájl automatikusan létrejön és megnyílik.
        /// </summary>
        private void Btn_Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (TablaMuvelet.Rows.Count <= 0) throw new HibásBevittAdat("Nincs sora a táblázatnak!");
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Eszterga_Karbantartás_Műveletek_Teljes_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };

                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyE.DataGridViewToExcel(fájlexc, TablaMuvelet, true);
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
                if (TablaMuvelet.Rows.Count <= 0)
                    throw new HibásBevittAdat("Nincs sora a táblázatnak!");

                SaveFileDialog saveDlg = new SaveFileDialog
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    Title = "Mentés PDF fájlba",
                    FileName = $"Eszterga_Karbantartás_Műveletek_Teljes_{Program.PostásNév.Trim()}_{DateTime.Now:yyyyMMdd_HHmmss}",
                    Filter = "PDF fájl (*.pdf)|*.pdf"
                };

                if (saveDlg.ShowDialog() != DialogResult.OK)
                    return;

                string fajlNev = saveDlg.FileName;
                if (!fajlNev.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                    fajlNev += ".pdf";

                PDFtábla(fajlNev, TablaMuvelet);

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

        // JAVÍTANDÓ:Ez annyiszor fut le ahány sor változott?
        //kesz
        /// <summary>
        /// Eseménykezelő, amely a TablaMuvelet DataGridView adatforrásának kötése után hívódik meg.
        /// Meghívja a ToroltTablaSzinezes metódust, hogy a törölt státuszú sorokat megjelenítési színezéssel lássa el.
        /// </summary>
        private void TablaMuvelet_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ToroltTablaSzinezes(TablaMuvelet);
        }

        /// <summary>
        /// Eseménykezelő, amely a TablaUtolagMuvelet DataGridView adatforrásának kötése után hívódik meg.
        /// Meghívja a ToroltTablaSzinezes metódust, hogy a törölt státuszú sorokat megjelenési színezéssel lássa el.
        /// </summary>
        private void TablaUtolagMuvelet_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ToroltTablaSzinezes(TablaUtolagMuvelet);
        }

        /// <summary>
        /// Kezeli a TablaUtolagMuvelet DataGridView kijelölésének változását.
        /// Amennyiben a felhasználó egyetlen sort választ ki és a kontroll fókuszban van,
        /// törli a TablaNaplo kijelölését, beállítja az utólagos dátumválasztót a mai napra,
        /// és törli az utólagos megjegyzés szövegmező tartalmát.
        /// </summary>
        private void TáblaUtólagMűvelet_SelectionChanged(object sender, EventArgs e)
        {
            if (TablaUtolagMuvelet.Focused && TablaUtolagMuvelet.SelectedRows.Count == 1)
            {
                TablaNaplo.ClearSelection();
                DtmPckrUtolagos.Value = DateTime.Today;
                TxtBxUtolagMegjegyzes.Text = "";
            }
        }

        /// <summary>
        /// Ellenőrzi, hogy pontosan két sor van-e kijelölve a táblában.
        /// Ha igen, akkor megjeleníti a csere- és sorrendgombokat, egyébként elrejti őket.
        /// </summary>
        private void Tábla_SelectionChanged(object sender, EventArgs e)
        {
            int Sorok = TablaMuvelet.SelectedRows.Count;

            if (Sorok == 2)
            {
                Btn_Csere.Visible = true;
                Btn_Sorrend.Visible = true;
            }
            else
            {
                Btn_Csere.Visible = false;
                Btn_Sorrend.Visible = false;
            }
        }

        /// <summary>
        /// Ha egy sorra kattintanak, a sor adatai betöltődnek a beviteli mezőkbe szerkesztés céljából.
        /// Dátum és enum érték is feldolgozásra kerül.
        /// </summary>
        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = TablaMuvelet.Rows[e.RowIndex];
                    TxtBxId.Text = row.Cells[0].Value.ToStrTrim();
                    TxtBxMűvelet.Text = row.Cells[1].Value.ToStrTrim();

                    string egysegValue = row.Cells[2].Value.ToStrTrim();
                    if (Enum.TryParse(egysegValue, out EsztergaEgyseg egysegEnum))
                        CmbxEgység.SelectedItem = egysegEnum;

                    TxtBxMennyiNap.Text = row.Cells[3].Value.ToStrTrim();
                    TxtBxMennyiÓra.Text = row.Cells[4].Value.ToStrTrim();
                    ChckBxStátus.Checked = row.Cells[5].Value.ToStrTrim() == "Törölt";
                    DtmPckrUtolsóDátum.Value = row.Cells[6].Value.ToÉrt_DaTeTime();
                    TxtBxUtolsóÜzemóraÁllás.Text = row.Cells[7].Value.ToStrTrim();
                    TorlesEllenorzes();
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
        /// Ha a felhasználó módosítja az utolsó dátum értékét, lekérdezi az ahhoz tartozó üzemóra adatot,
        /// és beírja a megfelelő mezőbe. A jövőbeni dátumokra figyelmeztet.
        /// </summary>
        private void DtmPckrUtolsóDátum_ValueChanged(object sender, EventArgs e)
        {

            if (frissul) return;

            try
            {
                frissul = true;

                DateTime ValasztottDatum;
                // JAVÍTANDÓ:ez miért van itt try-catch? Megint bonyolítasz
                try { ValasztottDatum = DtmPckrUtolsóDátum.Value.Date; }
                catch { return; }
                if (ValasztottDatum > DateTime.Today)
                    throw new HibásBevittAdat($"A választott dátum nem lehet később mint a mai nap {DateTime.Today}");

                UzemoraKiolvasasEsBeiras(ValasztottDatum, TxtBxUtolsóÜzemóraÁllás);
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
            finally
            {
                frissul = false;
            }
        }

        /// <summary>
        /// Ha az üzemóra mező értéke megváltozik, annak megfelelő dátumot keres az adatbázisban,
        /// és automatikusan beállítja a dátummezőt.
        /// </summary>
        private void TxtBxUtolsóÜzemóraÁllás_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (frissul) return;

                frissul = true;
                if (!long.TryParse(TxtBxUtolsóÜzemóraÁllás.Text, out long ValasztottUzemora))
                    throw new HibásBevittAdat("Csak pozitív egész szám lehet az üzemóra állásánál.");

                Adat_Eszterga_Uzemora uzemoraRekord = KeresÜzemóra(ValasztottUzemora, DateTime.MinValue, EsztergaEgyseg.Üzemóra);

                if (uzemoraRekord != null)
                    DtmPckrUtolsóDátum.Value = uzemoraRekord.Dátum;
                else
                    DtmPckrUtolsóDátum.Value = new DateTime(1900, 1, 1);
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
            finally
            {
                frissul = false;
            }
        }

        /// <summary>
        /// Amikor a felhasználó megváltoztatja az utólagos dátumot, automatikusan kiolvassa és beírja a megfelelő üzemóra értéket.
        /// </summary>
        private void DtmPckrUtolagos_ValueChanged(object sender, EventArgs e)
        {
            UzemoraKiolvasasEsBeiras(DtmPckrUtolagos.Value, TxtBxUtolagUzemora);
        }

        /// <summary>
        /// Ellenőrzi és rögzíti az utólagos naplózási adatokat, ha a kiválasztott sor érvényes és nincs már rögzítve ugyanarra a napra.
        /// </summary>
        private void BttnUtolag_Modosit_Click(object sender, EventArgs e)
        {
            try
            {
                if (TablaUtolagMuvelet.SelectedRows.Count == 0 && TablaNaplo.SelectedRows.Count == 0)
                    throw new HibásBevittAdat("Kérlek, válassz ki egy sort a listából!");

                if (DtmPckrUtolagos.Value.Date > DateTime.Today)
                    throw new HibásBevittAdat("A kiválasztott dátum nem lehet későbbi, mint a mai dátum.");
                bool sikeres = false;
                if (TablaUtolagMuvelet.SelectedRows.Count != 0)
                    sikeres = UjUtolagosNaplozas();
                else if (TablaNaplo.SelectedRows.Count != 0)
                    sikeres = UtolagNaploModositas();

                if (sikeres)
                {
                    TablaNaploListazas();
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
        /// Az utólagos naplóbejegyzések módosítását végzi.
        /// Ellenőrzi, hogy legalább egy sor ki legyen választva a napló táblában,
        /// majd az új értékek alapján frissíti a memóriában lévő naplóadatokat és adatbázisban is módosítja őket.
        /// Hibák esetén megfelelő üzenetet jelenít meg.
        /// </summary>
        private bool UtolagNaploModositas()
        {
            // JAVÍTANDÓ:
            try
            {
                if (TablaNaplo.SelectedRows.Count == 0)
                    throw new HibásBevittAdat("Kérlek, válassz ki legalább egy sort a naplóból!");

                foreach (DataGridViewRow sor in TablaNaplo.SelectedRows)
                {
                    int id = sor.Cells["Művelet Sorszáma"].Value.ToÉrt_Int();
                    DateTime eredetiDatum = sor.Cells["Utolsó Dátum"].Value.ToÉrt_DaTeTime();

                    Adat_Eszterga_Muveletek_Naplo eredeti = AdatokMuveletNaplo.FirstOrDefault(
                        a => a.ID == id && a.Utolsó_Dátum.Date == eredetiDatum.Date)
                        ?? throw new HibásBevittAdat($"A(z) {id} azonosítójú naplózott sor nem található a memóriában.");

                    DateTime ujDatum = DtmPckrUtolagos.Value.Date;
                    long ujUzemora = TxtBxUtolagUzemora.Text.ToÉrt_Long();
                    string ujMegjegyzes = TxtBxUtolagMegjegyzes.Text.Trim();

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
                    Kez_Muvelet_Naplo.UtolagModositas(modositott, eredetiDatum);
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

        /// <summary>
        /// Új utólagos naplóbejegyzést hoz létre a kiválasztott műveletek alapján.
        /// Ellenőrzi az üzemóra és dátum helyességét, és ha minden rendben van,
        /// hozzáadja az új bejegyzéseket az adatbázishoz.
        /// Hibák esetén megfelelő üzenetet jelenít meg.
        /// </summary>
        private bool UjUtolagosNaplozas()
        {
            try
            {
                // JAVÍTANDÓ:a dátum az nem dátum?
                DateTime datum = DtmPckrUtolagos.Value.Date;
                string megjegyzes = TxtBxUtolagMegjegyzes.Text.Trim();

                if (TxtBxUtolagUzemora.Enabled)
                {
                    if (!int.TryParse(TxtBxUtolagUzemora.Text, out int uzemora))
                        throw new HibásBevittAdat("Hibás üzemóra érték! Kérlek, csak számot adj meg.");

                    bool sikeres = UjUzemoraHozzaadasa(datum, uzemora, false);
                    if (!sikeres)
                        throw new HibásBevittAdat("Az üzemóra rögzítése sikertelen volt.");
                }

                List<Adat_Eszterga_Muveletek_Naplo> naploLista = new List<Adat_Eszterga_Muveletek_Naplo>();
                foreach (DataGridViewRow sor in TablaUtolagMuvelet.SelectedRows)
                {
                    int id = sor.Cells[0].Value.ToÉrt_Int();
                    // JAVÍTANDÓ:ha ki tudta választani akkor ott van és több elem esetén nem futunk végig?
                    Adat_Eszterga_Muveletek rekord = AdatokMuvelet.FirstOrDefault(a => a.ID == id)
                        ?? throw new HibásBevittAdat($"A(z) {id} azonosítójú művelet nem található.");

                    if (rekord.Státus)
                        throw new HibásBevittAdat("Törölt műveletet nem lehet naplózni.");

                    bool VanE = AdatokMuveletNaplo.Any(a => a.ID == id && a.Utolsó_Dátum.Date == datum);
                    if (VanE)
                        throw new HibásBevittAdat("Erre a dátumra már rögzítve lett ez a feladat egyszer.");
                    int MennyiNap = sor.Cells["Nap"].Value.ToÉrt_Int();
                    int MennyiÓra = sor.Cells["Óra"].Value.ToÉrt_Int();

                    long utolsoUzemora = TxtBxUtolagUzemora.Text.ToÉrt_Long();

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
                Kez_Muvelet_Naplo.EsztergaNaplozas(naploLista);
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

        /// <summary>
        /// Kezeli a napló táblázat kijelölésének változását.
        /// Ha a felhasználó egy sort választ ki, akkor a másik táblázat kijelölése törlődik,
        /// és a részletes adatok (dátum, üzemóra, megjegyzés) megjelennek a megfelelő vezérlőkben.
        /// </summary>
        private void TáblaNapló_SelectionChanged(object sender, EventArgs e)
        {
            if (TablaNaplo.Focused && TablaNaplo.SelectedRows.Count == 1)
            {
                TablaUtolagMuvelet.ClearSelection();

                DataGridViewRow sor = TablaNaplo.SelectedRows[0];
                DtmPckrUtolagos.Value = sor.Cells["Utolsó Dátum"].Value.ToÉrt_DaTeTime();
                TxtBxUtolagUzemora.Text = sor.Cells["Utolsó Üzemóra"].Value.ToStrTrim();
                TxtBxUtolagMegjegyzes.Text = sor.Cells["Megjegyzés"].Value.ToStrTrim();
            }
        }

        #endregion

        #region Ablakok
        Ablak_Eszterga_Karbantartás_Üzemóra Uj_ablak_EsztergaUzemora;

        /// <summary>
        /// Megnyitja az Eszterga üzemóra ablakot, ha még nincs megnyitva.  
        /// Ha az ablak már létezik, akkor előtérbe hozza és maximalizálja.
        /// Az ablak bezárásakor visszaállítja a hivatkozást, és frissítést kér a fő táblára, ha adatváltozás történt.
        /// </summary>
        private void Üzemóra_Oldal_Click(object sender, EventArgs e)
        {
            try
            {
                if (Uj_ablak_EsztergaUzemora == null)
                {
                    Uj_ablak_EsztergaUzemora = new Ablak_Eszterga_Karbantartás_Üzemóra();
                    Uj_ablak_EsztergaUzemora.FormClosed += Új_ablak_EsztergaÜzemóra_Closed;
                    Uj_ablak_EsztergaUzemora.Show();
                    Uj_ablak_EsztergaUzemora.Eszterga_Valtozas += TablaListazasMuvelet;
                }
                else
                {
                    Uj_ablak_EsztergaUzemora.Activate();
                    Uj_ablak_EsztergaUzemora.WindowState = FormWindowState.Maximized;
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
        /// Az Eszterga módosító ablak bezárásakor automatikusan bezárja az üzemóra ablakot is, ha nyitva van.
        /// </summary>
        private void Új_ablak_EsztergaMódosít_Closed(object sender, FormClosedEventArgs e)
        {
            Uj_ablak_EsztergaUzemora?.Close();
        }

        /// <summary>
        /// Az üzemóra ablak bezárásakor nullára állítja a hozzá tartozó hivatkozást,
        /// így lehetővé teszi annak újranyitását.
        /// </summary>
        private void Új_ablak_EsztergaÜzemóra_Closed(object sender, FormClosedEventArgs e)
        {
            Uj_ablak_EsztergaUzemora = null;
        }
        #endregion
    }
}