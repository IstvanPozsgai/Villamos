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

        readonly bool Baross = Program.PostásTelephely.Trim() == "Baross";
        private bool frissul = false;
        DataTable AdatTablaMuvelet = new DataTable();
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
        /// Ablak inicializálása és adatok betöltése a vezérlőelemekbe
        /// </summary>
        public Ablak_Eszterga_Karbantartás_Módosít()
        {
            InitializeComponent();
            TablaListazasMuvelet();
            TxtBxId.Enabled = false;
            TxtBxId.Text = "0";
            CmbxEgyseg.DataSource = Enum.GetValues(typeof(EsztergaEgyseg));
        }

        /// <summary>
        /// Az ablak betöltésekor lefutó inicializálási műveletek
        /// </summary>
        private void Ablak_Eszterga_Karbantartás_Módosít_Load(object sender, EventArgs e)
        {
            Eszterga_Valtozas?.Invoke();
            TablaMuvelet.ClearSelection();
            JogosultsagKiosztas();
            Btn_Csere.Visible = false;
            Btn_Sorrend.Visible = false;
            // A DataGridView adatforrásának kötése után automatikusan meghívja a ToroltTablaSzinezes metódust,
            // hogy a törölt státuszú sorokat színezve jelenítse meg.
            TablaMuvelet.DataBindingComplete += (s, ev) => TablaSzinezes(TablaMuvelet);
            EgysegBeallitasa();
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
                Btn_Modosit.Visible = Baross;
                Btn_Sorrend.Visible = Baross;
                Btn_Torles.Visible = Baross;
                Btn_UjFelvetel.Visible = Baross;
                Btn_Csere.Visible = Baross;
                Btn_Naplo_Oldal.Visible = Baross;

                // módosítás 1 
                //Ablak_Eszterga_Karbantartás_Segéd oldal használja az 1. módosításokat

                // módosítás 2
                //Ablak_Eszterga_Karbantartás oldal használja a 2. módosításokat

                // módosítás 3 
                Btn_Modosit.Enabled = MyF.Vanjoga(melyikelem, 3);
                Btn_Sorrend.Enabled = MyF.Vanjoga(melyikelem, 3);
                Btn_Torles.Enabled = MyF.Vanjoga(melyikelem, 3);
                Btn_UjFelvetel.Enabled = MyF.Vanjoga(melyikelem, 3);
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
                TxtBxMennyiOra.Enabled = true;
                TxtBxMennyiNap.Enabled = true;
                TxtBxUtolsoUzemoraAllas.Enabled = true;
                DtmPckrUtolsoDatum.Enabled = true;

                switch (Egyseg)
                {
                    case "Dátum":
                        TxtBxMennyiOra.Enabled = false;
                        TxtBxMennyiOra.Text = "0";
                        TxtBxUtolsoUzemoraAllas.Enabled = false;

                        Adat_Eszterga_Uzemora uzemoraRekordDatum = KeresUzemora(0, DtmPckrUtolsoDatum.Value, EsztergaEgyseg.Dátum);
                        TxtBxUtolsoUzemoraAllas.Text = uzemoraRekordDatum != null ? uzemoraRekordDatum.Uzemora.ToStrTrim() : "0";
                        break;

                    case "Üzemóra":
                        TxtBxMennyiNap.Enabled = false;
                        TxtBxMennyiNap.Text = "0";
                        DtmPckrUtolsoDatum.Enabled = false;

                        if (long.TryParse(TxtBxUtolsoUzemoraAllas.Text, out long uzemora))
                        {
                            Adat_Eszterga_Uzemora uzemoraRekordUzemora = KeresUzemora(uzemora, DateTime.MinValue, EsztergaEgyseg.Üzemóra);
                            DtmPckrUtolsoDatum.Value = uzemoraRekordUzemora?.Dátum ?? new DateTime(1900, 1, 1);
                        }
                        else
                            DtmPckrUtolsoDatum.Value = new DateTime(1900, 1, 1);
                        break;

                    case "Bekövetkezés":
                        TxtBxMennyiOra.Enabled = true;
                        TxtBxMennyiNap.Enabled = true;
                        TxtBxUtolsoUzemoraAllas.Enabled = true;
                        DtmPckrUtolsoDatum.Enabled = true;
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
        private void CmbxEgyseg_SelectedIndexChanged(object sender, EventArgs e)
        {
            string kivalasztottEgyseg = CmbxEgyseg.SelectedItem.ToStrTrim();
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
                CmbxEgyseg.SelectedItem = EsztergaEgyseg.Bekövetkezés;
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
                AdatTablaMuvelet = new DataTable();
                AdatTablaMuvelet.Columns.Clear();
                AdatTablaMuvelet.Columns.Add("Sorszám");
                AdatTablaMuvelet.Columns.Add("Művelet");
                AdatTablaMuvelet.Columns.Add("Egység");
                AdatTablaMuvelet.Columns.Add("Nap");
                AdatTablaMuvelet.Columns.Add("Óra");
                AdatTablaMuvelet.Columns.Add("Státusz");
                AdatTablaMuvelet.Columns.Add("Utolsó Dátum");
                AdatTablaMuvelet.Columns.Add("Utolsó Üzemóra");

                AdatokMuvelet = Kez_Muvelet.Lista_Adatok();
                AdatokUzemora = Kez_Uzemora.Lista_Adatok();
                AdatTablaMuvelet.Clear();

                foreach (Adat_Eszterga_Muveletek rekord in AdatokMuvelet)
                {
                    DataRow Soradat = AdatTablaMuvelet.NewRow();

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

                    AdatTablaMuvelet.Rows.Add(Soradat);
                }

                TablaMuvelet.DataSource = AdatTablaMuvelet;
                OszlopSzelessegMuvelet();
                TablaSzinezes(TablaMuvelet);
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
        /// Meghivja a tabla listazasokat egyszerre
        /// </summary>
        private void TablakListazasa()
        {
            TablaListazasMuvelet();
            TorlesEllenorzes();
        }
        #endregion

        #region Metodusok

        /// <summary>
        /// Ellenőrzi a törlés gomb láthatóságát a státusz checkbox alapján
        /// </summary>
        private void TorlesEllenorzes()
        {
            Btn_Torles.Visible = !ChckBxStatus.Checked;
        }

        /// <summary>
        /// Ellenőrzi a megadott adatokat a felhasználói űrlapon. 
        /// Új rekord hozzáadása esetén ellenőrzi, hogy az azonosító már létezik-e az adatbázisban, 
        /// illetve biztosítja, hogy minden mező érvényes adatokat tartalmazzon.
        /// </summary>
        private bool TxtBxEllenorzes(int ujRekord, bool torles = false)
        {
            bool Eredmeny = false;
            try
            {
                if (torles)
                    return true;
                //Ujrekordnal nem ellenőrizzük, hogy van-e kiválasztott sor
                if (ujRekord != 0 && TablaMuvelet.SelectedRows.Count < 1)
                    throw new HibásBevittAdat("Nincs kiválasztott művelet.");

                if (string.IsNullOrEmpty(TxtBxId.Text))
                    throw new HibásBevittAdat("Töltse ki az Azonosító mezőt.");

                string Egyseg = CmbxEgyseg.SelectedItem?.ToStrTrim();
                bool Nap = int.TryParse(TxtBxMennyiNap.Text, out int MennyiNap);
                bool Ora = int.TryParse(TxtBxMennyiOra.Text, out int MennyiÓra);

                if (Egyseg == "Dátum" && (!Nap || MennyiNap <= 0))
                    throw new HibásBevittAdat("A Nap mezőben csak pozitív egész szám szerepelhet.");

                else if (Egyseg == "Üzemóra" && (!Ora || MennyiÓra <= 0))
                    throw new HibásBevittAdat("Az Óra mezőben csak pozitív egész szám szerepelhet.");

                else if (Egyseg == "Bekövetkezés" && (!Nap || !Ora || MennyiNap <= 0 || MennyiÓra <= 0))
                    throw new HibásBevittAdat("A Nap és Óra mezőkben csak pozitív egész szám szerepelhetnek.");

                if (string.IsNullOrEmpty(TxtBxMuvelet.Text))
                    throw new HibásBevittAdat("Töltse ki a Művelet mezőt.");

                if (Egyseg == "Üzemóra" || Egyseg == "Bekövetkezés")
                {
                    AdatokUzemora = Kez_Uzemora.Lista_Adatok();

                    if (string.IsNullOrEmpty(TxtBxUtolsoUzemoraAllas.Text) || TxtBxUtolsoUzemoraAllas.Text == "0" ||
                        !long.TryParse(TxtBxUtolsoUzemoraAllas.Text, out _))
                        throw new HibásBevittAdat("Az Utolsó Üzemóra Állás mező csak pozitív egész számot tartalmazhat.");

                    else
                    {
                        long aktualisUzemora = AdatokUzemora.Count > 0 ? AdatokUzemora.Max(u => u.Uzemora) : 0;
                        if (long.Parse(TxtBxUtolsoUzemoraAllas.Text) > aktualisUzemora)
                            throw new HibásBevittAdat("Az Utolsó Üzemóra Állás nem lehet nagyobb, mint az aktuális Üzemóra érték.");
                    }

                }
                Eredmeny = true;
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
        /// Kiválasztja a kijelölt sorokat a TáblaMűveletből, és visszaadja az id-jük alapján a megfelelő rekordokat
        /// az adatbázisból.
        /// </summary>
        private List<Adat_Eszterga_Muveletek> SorKivalasztas()
        {
            List<Adat_Eszterga_Muveletek> rekordok = new List<Adat_Eszterga_Muveletek>();
            try
            {
                foreach (DataGridViewRow sor in TablaMuvelet.SelectedRows)
                {
                    int id = sor.Cells[0].Value.ToÉrt_Int();

                    AdatokMuvelet = Kez_Muvelet.Lista_Adatok();

                    Adat_Eszterga_Muveletek rekord = (from a in AdatokMuvelet
                                                      where a.ID == id
                                                      select a).FirstOrDefault();

                    rekordok.Add(rekord);
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
            return rekordok;
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

                if (AdatokMuvelet.Count() == 0)
                    return Muvelet;

                Adat_Eszterga_Muveletek rekord = AdatokMuvelet.FirstOrDefault(a => a.ID == int.Parse(TxtBxId.Text));

                Enum.TryParse(CmbxEgyseg.SelectedItem.ToStrTrim(), out EsztergaEgyseg egyseg);

                return
                    rekord.Művelet.Trim() != TxtBxMuvelet.Text.Trim() ||
                    rekord.Egység != (int)egyseg ||
                    rekord.Státus != ChckBxStatus.Checked ||
                    rekord.Mennyi_Dátum != int.Parse(TxtBxMennyiNap.Text) ||
                    rekord.Mennyi_Óra != int.Parse(TxtBxMennyiOra.Text) ||
                    rekord.Utolsó_Dátum != DtmPckrUtolsoDatum.Value ||
                    rekord.Utolsó_Üzemóra_Állás != int.Parse(TxtBxUtolsoUzemoraAllas.Text);
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
        /// Az üzemórát kiolvassa az adatbázisból a megadott dátum alapján, és beírja a TextBox-ba.
        /// Ha található üzemóra rekord, akkor az üzemóra értékét beírja a TextBox-ba, és letiltja a szerkeszthetőséget.
        /// Ha nincs találat, akkor 0-t ír be és engedélyezi a TextBox szerkesztését.
        /// </summary>
        private void UzemoraKiolvasasEsBeiras(DateTime datum, TextBox txt)
        {
            try
            {
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
        #endregion

        #region Gombok,Muveletek

        /// <summary>
        /// Ha van kijelölt rekord, akkor módosítja azt az új adatokkal, ha nem, akkor új rekordot ad hozzá.
        /// Ellenőrzi, hogy a szükséges adatokat megfelelően kitöltötték-e, majd végrehajtja a módosítást vagy a hozzáadást.
        /// </summary>
        private void Btn_Modosit_Click(object sender, EventArgs e)
        {
            try
            {
                int AktivId = int.TryParse(TxtBxId.Text?.Trim(), out int id) ? id : 0;

                if (AktivId != 0 && !ModositasEll(true))
                    throw new HibásBevittAdat("Nem történt változás.");

                if (!TxtBxEllenorzes(AktivId, false))
                    return;

                Adat_Eszterga_Muveletek ADAT = new Adat_Eszterga_Muveletek(
                    AktivId,
                    TxtBxMuvelet.Text.ToStrTrim(),
                    (int)CmbxEgyseg.SelectedItem,
                    TxtBxMennyiNap.Text.ToÉrt_Int(),
                    TxtBxMennyiOra.Text.ToÉrt_Int(),
                    ChckBxStatus.Checked,
                    DtmPckrUtolsoDatum.Value.Date,
                    TxtBxUtolsoUzemoraAllas.Text.ToÉrt_Long());

                if (AktivId != 0)
                    Kez_Muvelet.Modositas_MeglevoMuvelet(ADAT);
                else
                    Kez_Muvelet.Rogzites(ADAT);

                Eszterga_Valtozas?.Invoke();
                TablakListazasa();
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
        private void Btn_Torles_Click(object sender, EventArgs e)
        {
            try
            {
                int AktivId = int.TryParse(TxtBxId.Text?.Trim(), out int id) ? id : 0;
                if (!TxtBxEllenorzes(AktivId, true))
                    return;

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
                TablakListazasa();
                Btn_Torles.Visible = false;
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
        private void Btn_UjFelvetel_Click(object sender, EventArgs e)
        {
            try
            {
                Btn_Torles.Visible = false;
                AdatokMuvelet = Kez_Muvelet.Lista_Adatok();
                TxtBxId.Text = "0";
                TxtBxMuvelet.Text = "";
                CmbxEgyseg.SelectedItem = EsztergaEgyseg.Bekövetkezés;
                TxtBxMennyiNap.Text = "0";
                TxtBxMennyiOra.Text = "0";
                ChckBxStatus.Checked = false;
                DtmPckrUtolsoDatum.Value = DateTime.Today;
                Adat_Eszterga_Uzemora legutolsoUzemora = (from a in AdatokUzemora
                                                          where !a.Státus
                                                          orderby a.Dátum descending
                                                          select a).FirstOrDefault();

                TxtBxUtolsoUzemoraAllas.Text = legutolsoUzemora != null ? legutolsoUzemora.Uzemora.ToStrTrim() : "0";
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

                int Id1 = rekordok[0].ID;
                int Id2 = rekordok[1].ID;

                Kez_Muvelet.Csere(Id1, Id2);
                TablakListazasa();
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

                int Id1 = rekordok[1].ID;
                int Id2 = rekordok[0].ID;

                Kez_Muvelet.Sorrendezes(Id1, Id2);
                TablakListazasa();
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

                PDFtabla(fajlNev, TablaMuvelet);

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
        /// Eseménykezelő, amely a TablaMuvelet DataGridView adatforrásának kötése után hívódik meg.
        /// Meghívja a ToroltTablaSzinezes metódust, hogy a törölt státuszú sorokat megjelenítési színezéssel lássa el.
        /// </summary>
        private void TablaMuvelet_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            TablaSzinezes(TablaMuvelet);
        }

        /// <summary>
        /// Ellenőrzi, hogy pontosan két sor van-e kijelölve a táblában.
        /// Ha igen, akkor megjeleníti a csere- és sorrendgombokat, egyébként elrejti őket.
        /// </summary>
        private void Tabla_SelectionChanged(object sender, EventArgs e)
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
        private void Tabla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = TablaMuvelet.Rows[e.RowIndex];
                    TxtBxId.Text = row.Cells[0].Value.ToStrTrim();
                    TxtBxMuvelet.Text = row.Cells[1].Value.ToStrTrim();

                    string egysegValue = row.Cells[2].Value.ToStrTrim();
                    if (Enum.TryParse(egysegValue, out EsztergaEgyseg egysegEnum))
                        CmbxEgyseg.SelectedItem = egysegEnum;

                    TxtBxMennyiNap.Text = row.Cells[3].Value.ToStrTrim();
                    TxtBxMennyiOra.Text = row.Cells[4].Value.ToStrTrim();
                    ChckBxStatus.Checked = row.Cells[5].Value.ToStrTrim() == "Törölt";
                    DtmPckrUtolsoDatum.Value = row.Cells[6].Value.ToÉrt_DaTeTime();
                    TxtBxUtolsoUzemoraAllas.Text = row.Cells[7].Value.ToStrTrim();
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
        private void DtmPckrUtolsoDatum_ValueChanged(object sender, EventArgs e)
        {
            if (frissul) return;
            frissul = true;
            try
            {
                DateTime ValasztottDatum = DtmPckrUtolsoDatum.Value.Date;

                if (ValasztottDatum > DateTime.Today)
                {
                    UzemoraKiolvasasEsBeiras(DateTime.Today, TxtBxUtolsoUzemoraAllas);
                    throw new HibásBevittAdat($"A választott dátum nem lehet később mint a mai nap {DateTime.Today}");
                }

                UzemoraKiolvasasEsBeiras(ValasztottDatum, TxtBxUtolsoUzemoraAllas);
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
            frissul = false;
        }

        /// <summary>
        /// Ha az üzemóra mező értéke megváltozik, annak megfelelő dátumot keres az adatbázisban,
        /// és automatikusan beállítja a dátummezőt.
        /// </summary>
        private void TxtBxUtolsoUzemoraAllas_TextChanged(object sender, EventArgs e)
        {
            if (frissul) return;

            frissul = true;
            try
            {
                if (!long.TryParse(TxtBxUtolsoUzemoraAllas.Text, out long ValasztottUzemora))
                    throw new HibásBevittAdat("Csak pozitív egész szám lehet az üzemóra állásánál.");

                Adat_Eszterga_Uzemora uzemoraRekord = KeresUzemora(ValasztottUzemora, DateTime.MinValue, EsztergaEgyseg.Üzemóra);

                if (uzemoraRekord != null)
                    DtmPckrUtolsoDatum.Value = uzemoraRekord.Dátum;
                else
                    DtmPckrUtolsoDatum.Value = new DateTime(1900, 1, 1);
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
            frissul = false;
        }
        #endregion

        #region Ablakok
        Ablak_Eszterga_Karbantartás_Üzemóra Uj_ablak_EsztergaUzemora;

        /// <summary>
        /// Megnyitja az Eszterga üzemóra ablakot, ha még nincs megnyitva.  
        /// Ha az ablak már létezik, akkor előtérbe hozza és maximalizálja.
        /// Az ablak bezárásakor visszaállítja a hivatkozást, és frissítést kér a fő táblára, ha adatváltozás történt.
        /// </summary>
        private void Btn_Uzemora_Oldal_Click(object sender, EventArgs e)
        {
            try
            {
                if (Uj_ablak_EsztergaUzemora == null)
                {
                    Uj_ablak_EsztergaUzemora = new Ablak_Eszterga_Karbantartás_Üzemóra();
                    Uj_ablak_EsztergaUzemora.FormClosed += Uj_ablak_EsztergaUzemora_Closed;
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

        Ablak_Eszterga_Karbantartás_Napló Uj_ablak_EsztergaNaplo;
        private void Btn_Naplo_Oldal_Click(object sender, EventArgs e)
        {
            try
            {
                if (Uj_ablak_EsztergaNaplo == null)
                {
                    Uj_ablak_EsztergaNaplo = new Ablak_Eszterga_Karbantartás_Napló();
                    Uj_ablak_EsztergaNaplo.FormClosed += Uj_ablak_EsztergaNaplo_Closed;
                    Uj_ablak_EsztergaNaplo.Show();
                }
                else
                {
                    Uj_ablak_EsztergaNaplo.Activate();
                    Uj_ablak_EsztergaNaplo.WindowState = FormWindowState.Maximized;
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
        private void Uj_ablak_EsztergaMódosít_Closed(object sender, FormClosedEventArgs e)
        {
            Uj_ablak_EsztergaUzemora?.Close();
        }

        /// <summary>
        /// Az üzemóra ablak bezárásakor nullára állítja a hozzá tartozó hivatkozást,
        /// így lehetővé teszi annak újranyitását.
        /// </summary>
        private void Uj_ablak_EsztergaUzemora_Closed(object sender, FormClosedEventArgs e)
        {
            Uj_ablak_EsztergaUzemora = null;
        }

        private void Uj_ablak_EsztergaNaplo_Closed(object sender, FormClosedEventArgs e)
        {
            Uj_ablak_EsztergaNaplo = null;
        }
        #endregion
    }
}