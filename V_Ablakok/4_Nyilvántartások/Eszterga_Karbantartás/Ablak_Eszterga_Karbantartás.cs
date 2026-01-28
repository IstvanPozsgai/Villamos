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
using Villamos.Villamos_Ablakok._4_Nyilvántartások.Kerékeszterga;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Kezelők;
using Application = System.Windows.Forms.Application;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos.Villamos_Ablakok._5_Karbantartás.Eszterga_Karbantartás
{
    public delegate void Event_Kidobó();
    public partial class Ablak_Eszterga_Karbantartás : Form
    {
        #region Osztalyszintu elemek

        DateTime TervDatum;
        readonly bool Baross = Program.PostásTelephely.Trim() == "Baross";
        private string AktivTablaTipus;
        DataTable AdatTabla = new DataTable();
        private const int Alap_Napi_Atlag = 30;
        private const int Alap_Napi_Szam = 5;
        private const int Alap_Uzemora_Szam = 8;
        private const int Max_Napok = 100000;
        #endregion

        #region Listák

        List<Adat_Eszterga_Muveletek> AdatokMuvelet = new List<Adat_Eszterga_Muveletek>();
        List<Adat_Eszterga_Uzemora> AdatokUzemora = new List<Adat_Eszterga_Uzemora>();
        List<Adat_Eszterga_Muveletek_Naplo> AdatokMuveletNaplo = new List<Adat_Eszterga_Muveletek_Naplo>();
        #endregion

        #region Kezelők

        readonly Kezelő_Eszterga_Műveletek Kez_Muvelet = new Kezelő_Eszterga_Műveletek();
        readonly Kezelő_Eszterga_Üzemóra Kez_Uzemora = new Kezelő_Eszterga_Üzemóra();
        readonly Kezelő_Eszterga_Műveletek_Napló Kez_Muvelet_Naplo = new Kezelő_Eszterga_Műveletek_Napló();
        #endregion

        #region Alap

        /// <summary>
        /// Inicializálja az Eszterga karbantartás ablak komponenseit.
        /// Konstruktor, amely az ablak felépítését indítja el a komponens inicializálással.
        /// </summary>
        public Ablak_Eszterga_Karbantartás()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Az ablak betöltésekor előkészíti az adatbázis fájlokat, szükség esetén létrehozza azokat.  
        /// Betölti az aktuális üzemóra adatokat, és ha nincs rögzítve az adott napra, a felhasználótól kér be értéket.  
        /// Elvégzi a jogosultságok beállítását, betölti a táblázatokat, és kiszámítja az átlag üzemórát az elmúlt 30 napra.
        /// </summary>
        private void Ablak_Eszterga_Karbantartás_Load(object sender, EventArgs e)
        {
            try
            {
                long Uzemora = 0;
                AdatokUzemora = Kez_Uzemora.Lista_Adatok();
                Adat_Eszterga_Uzemora rekord = (from a in AdatokUzemora
                                                where a.Dátum.Date == DateTime.Today && !a.Státus
                                                select a).FirstOrDefault();

                if (rekord != null)
                {
                    Uzemora = rekord.Uzemora;
                    throw new HibásBevittAdat($"A mai napon már rögzítettek üzemóra adatot.\nAz utolsó rögzített üzemóra: {rekord.Uzemora}.");
                }
                else
                {
                    using (Ablak_Eszterga_Karbantartás_Segéd SegedAblak = new Ablak_Eszterga_Karbantartás_Segéd())
                    {
                        if (SegedAblak.ShowDialog() == DialogResult.OK)
                            Uzemora = SegedAblak.Uzemora;
                        else
                        {
                            this.Close();
                            return;
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

            Start();
        }
        private void Start()
        {
            try
            {
                //Ha az első karakter "R" akkor az új jogosultságkiosztást használjuk
                //ha nem akkor a régit használjuk
                if (Program.PostásJogkör.Substring(0, 1) == "R")
                    GombLathatosagKezelo.Beallit(this, "Baross");
                else
                    Jogosultsagkiosztas();

                // Tábla és átlag üzemóra beállítása
                TablaListazas();
                AtlagUzemoraFrissites(Alap_Napi_Atlag);

                // Sorok színezése adatkötés után is
                Tabla.DataBindingComplete += (s, ev) => SorSzinezes();
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
        /// Beállítja a felhasználó jogosultságait a gombok (rögzítés, módosítás) elérhetőségéhez.  
        /// A jogosultságokat azonosító alapján kérdezi le, és engedélyezi vagy tiltja az adott funkciókat.  
        /// Hiba esetén figyelmeztető üzenetet jelenít meg, vagy naplózza a kivételt.
        /// </summary>
        private void Jogosultsagkiosztas()
        {
            try
            {
                int melyikelem;
                melyikelem = 160;
                Btn_Rogzit.Visible = Baross;
                Btn_Modositas.Visible = Baross;

                //módosítás 1
                //Ablak_Eszterga_Karbantartás_Segéd oldal használja az 1. módosításokat
                Btn_Modositas.Enabled = MyF.Vanjoga(melyikelem, 1);

                //módosítás 2
                Btn_Rogzit.Enabled = MyF.Vanjoga(melyikelem, 2);

                //módosítás 3
                //Ablak_Eszterga_Karbantartás_Módosít oldalon is felhasználva.
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

        #region Ablakok
        Ablak_Eszterga_Karbantartás_Módosít Uj_ablak_EsztergaModosit;

        /// <summary>
        /// Megnyitja az Eszterga karbantartás módosító ablakot, ha az még nincs megnyitva.
        /// Ha már meg van nyitva, akkor előtérbe hozza és maximalizálja.
        /// Az ablak bezárásakor frissíti a fő ablak tábláját, ha történt változás.
        /// </summary>
        private void Btn_Modositas_Click(object sender, EventArgs e)
        {
            if (Uj_ablak_EsztergaModosit == null)
            {
                Uj_ablak_EsztergaModosit = new Ablak_Eszterga_Karbantartás_Módosít();
                Uj_ablak_EsztergaModosit.FormClosed += Uj_ablak_EsztergaMódosít_Closed;
                Uj_ablak_EsztergaModosit.Show();
                Uj_ablak_EsztergaModosit.Eszterga_Valtozas += TablaListazas;
            }
            else
            {
                Uj_ablak_EsztergaModosit.Activate();
                Uj_ablak_EsztergaModosit.WindowState = FormWindowState.Maximized;
            }
        }

        /// <summary>
        /// A módosító ablak bezárásakor törli a példány hivatkozását,
        /// így lehetővé teszi az újranyitást.
        /// </summary>
        private void Uj_ablak_EsztergaMódosít_Closed(object sender, FormClosedEventArgs e)
        {
            Uj_ablak_EsztergaModosit = null;
        }

        /// <summary>
        /// A fő ablak bezárásakor automatikusan bezárja a megnyitott módosító ablakot is,
        /// ha az még fut.
        /// </summary>
        private void Ablak_Eszterga_Karbantartas_FormClosed(object sender, FormClosedEventArgs e)
        {
            Uj_ablak_EsztergaModosit?.Close();
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
            Beköv = 3
        }
        #endregion

        #region Tábla Listázások && Naplózás

        /// <summary>
        /// Betölti az aktív (nem törölt) karbantartási műveletek listáját a táblázatba.
        /// Számolja és beállítja az esedékességi dátumokat, üzemóra becsléseket és a megjelenítést színezéssel.
        /// Az oszlopokat lezárja szerkesztés ellen és alaphelyzetbe állítja a táblázatot.
        /// </summary>
        private void TablaListazas()
        {
            try
            {
                AktivTablaTipus = "Muvelet";
                Tabla.DataSource = null;
                AdatTabla = new DataTable();
                TablaUrites();
                AdatTabla.Columns.Clear();
                AdatTabla.Rows.Clear();
                AdatTabla.Columns.Add("Sorsz.");
                AdatTabla.Columns.Add("Művelet");
                AdatTabla.Columns.Add("Egység");
                AdatTabla.Columns.Add("Nap");
                AdatTabla.Columns.Add("Óra");
                AdatTabla.Columns.Add("Státusz");
                AdatTabla.Columns.Add("Utolsó Dátum");
                AdatTabla.Columns.Add("Utolsó Üzemóra");
                AdatTabla.Columns.Add("Esedékesség Dátuma");
                AdatTabla.Columns.Add("Becsült Üzemóra");
                AdatTabla.Columns.Add("Megjegyzés");

                AdatokMuvelet = Kez_Muvelet.Lista_Adatok();
                AdatokUzemora = Kez_Uzemora.Lista_Adatok();
                TervDatum = DtmPckrEloTerv.Value.Date;

                AdatokMuvelet = AdatokMuvelet
                    .Where(rekord => !rekord.Státus)
                    .OrderBy(rekord =>
                        Kiszinezes(rekord, TervDatum) == Color.IndianRed ? 0 :
                        Kiszinezes(rekord, TervDatum) == Color.Yellow ? 1 : 2)
                    .ToList();

                foreach (Adat_Eszterga_Muveletek rekord in AdatokMuvelet)
                {
                    DataRow Soradat = AdatTabla.NewRow();

                    Soradat["Sorsz."] = rekord.ID;
                    Soradat["Művelet"] = rekord.Művelet;
                    Soradat["Egység"] = Enum.GetName(typeof(EsztergaEgyseg), rekord.Egység);
                    Soradat["Nap"] = rekord.Mennyi_Dátum;
                    Soradat["Óra"] = rekord.Mennyi_Óra;
                    Soradat["Státusz"] = rekord.Státus ? "Törölt" : "Aktív";
                    Soradat["Utolsó Dátum"] = rekord.Utolsó_Dátum.ToShortDateString();

                    Adat_Eszterga_Uzemora uzemoraRekord = AdatokUzemora
                        .FirstOrDefault(a => a.Dátum.Date == rekord.Utolsó_Dátum.Date && a.Státus == false);

                    Soradat["Utolsó Üzemóra"] = uzemoraRekord != null ? uzemoraRekord.Uzemora : rekord.Utolsó_Üzemóra_Állás;
                    DateTime EsedekesDatum = VegsoEsedekesDatumSzamitasa(rekord.Utolsó_Dátum, rekord, uzemoraRekord);
                    Soradat["Esedékesség Dátuma"] = EsedekesDatum.ToShortDateString();
                    Soradat["Becsült Üzemóra"] = BecsultUzemora(EsedekesDatum);

                    Soradat["Megjegyzés"] = rekord.Megjegyzés;

                    AdatTabla.Rows.Add(Soradat);
                }

                Tabla.DataSource = AdatTabla;
                SorSzinezes();
                OszlopSzelesseg();
                for (int i = 0; i < 10; i++)
                    Tabla.Columns[i].ReadOnly = true;
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
        /// Előre tervezett eszterga műveletek esedékességi listáját állítja össze és jeleníti meg a táblázatban egy adott dátumig.
        /// 
        /// Működése:
        /// - Lekéri az összes aktív műveleti rekordot (kivéve a státusz=igaz, azaz törölt elemeket).
        /// - Minden művelethez megnézi, hogy a megadott előre tervezési dátumig (TervDatum) esedékessé válik-e:
        ///   - Dátumalapú esedékesség: adott nap elteltével esedékes.
        ///   - Üzemóra alapú esedékesség: becsült üzemóra érték alapján esedékes.
        ///   - Bekövetkezés típus: a fenti két feltétel bármelyikének teljesülése.
        /// - A kiszámolt esedékességi adatokat új sorokként hozzáadja a megjelenítendő DataTable-hez.
        /// - Az adatokat dátum és Sorsz. szerint rendezi, majd megjeleníti a táblázatban.
        /// - A sorokat színezi és az oszlopokat lezárja írásvédettként.
        /// </summary>
        private void EloreTervezesListazasa()
        {
            try
            {
                AktivTablaTipus = "EloreTervezes";
                TablaUrites();
                Tabla.DataSource = null;
                AdatTabla = new DataTable();
                AdatTabla.Columns.Clear();
                AdatTabla.Rows.Clear();
                AdatTabla.Columns.Add("Sorsz.");
                AdatTabla.Columns.Add("Művelet");
                AdatTabla.Columns.Add("Egység");
                AdatTabla.Columns.Add("Nap");
                AdatTabla.Columns.Add("Óra");
                AdatTabla.Columns.Add("Státusz");
                AdatTabla.Columns.Add("Utolsó Dátum");
                AdatTabla.Columns.Add("Utolsó Üzemóra");
                AdatTabla.Columns.Add("Esedékesség Dátuma");
                AdatTabla.Columns.Add("Becsült Üzemóra");
                AdatTabla.Columns.Add("Megjegyzés");

                AdatokMuvelet = Kez_Muvelet.Lista_Adatok()
                    .Where(rekord => !rekord.Státus)
                    .OrderBy(rekord => rekord.Művelet)
                    .ToList();
                AdatokUzemora = Kez_Uzemora.Lista_Adatok();
                TervDatum = DtmPckrEloTerv.Value.Date;
                double SzuksegesNapok;

                List<DataRow> RendezettSorok = new List<DataRow>();

                foreach (Adat_Eszterga_Muveletek rekord in AdatokMuvelet)
                {
                    int ID = rekord.ID;
                    DateTime UtolsoDatum = rekord.Utolsó_Dátum.Date;
                    long UtolsoUzemora = rekord.Utolsó_Üzemóra_Állás;
                    long BecsultUzemora = this.BecsultUzemora(TervDatum);

                    while (UtolsoDatum.AddDays(rekord.Mennyi_Dátum) <= TervDatum || (UtolsoUzemora + rekord.Mennyi_Óra) >= BecsultUzemora)
                    {
                        bool Esedekes = false;

                        double AtlagosNapiUzemNovekedes = AtlagUzemoraNovekedesKiszamitasa(TervDatum);

                        switch ((EsztergaEgyseg)rekord.Egység)
                        {
                            case EsztergaEgyseg.Dátum:
                                if ((TervDatum - UtolsoDatum).TotalDays >= rekord.Mennyi_Dátum)
                                {
                                    Esedekes = true;
                                    UtolsoDatum = UtolsoDatum.AddDays(rekord.Mennyi_Dátum);
                                }
                                break;

                            case EsztergaEgyseg.Üzemóra:
                                long aktualisMaxUzemora = AdatokUzemora.Where(a => !a.Státus).Max(a => a.Uzemora);

                                long kovetkezoCel = UtolsoUzemora + rekord.Mennyi_Óra;
                                if (UtolsoDatum <= DateTime.Today && kovetkezoCel > aktualisMaxUzemora)
                                {
                                    long maradekOra = kovetkezoCel - aktualisMaxUzemora;

                                    if (AtlagosNapiUzemNovekedes > 0)
                                    {
                                        double szuksegesNap = Math.Ceiling(maradekOra / AtlagosNapiUzemNovekedes);

                                        if (DateTime.Today.AddDays(szuksegesNap) <= TervDatum)
                                        {
                                            Esedekes = true;
                                            UtolsoDatum = DateTime.Today.AddDays(szuksegesNap);
                                            UtolsoUzemora = kovetkezoCel;
                                        }
                                    }
                                }
                                else if ((BecsultUzemora - UtolsoUzemora) >= rekord.Mennyi_Óra)
                                {
                                    Esedekes = true;

                                    if (AtlagosNapiUzemNovekedes > 0)
                                        SzuksegesNapok = Math.Ceiling(rekord.Mennyi_Óra / AtlagosNapiUzemNovekedes);
                                    else
                                        SzuksegesNapok = 0;

                                    UtolsoDatum = UtolsoDatum.AddDays(SzuksegesNapok);
                                    UtolsoUzemora += rekord.Mennyi_Óra;
                                }
                                break;

                            case EsztergaEgyseg.Beköv:
                                bool NapEsedekes = (TervDatum - UtolsoDatum).TotalDays >= rekord.Mennyi_Dátum;
                                bool UzemoraEsedekes = (BecsultUzemora - UtolsoUzemora) >= rekord.Mennyi_Óra;

                                if (NapEsedekes && UzemoraEsedekes)
                                {
                                    DateTime EsedekesDatumNap = UtolsoDatum.AddDays(rekord.Mennyi_Dátum);
                                    DateTime EsedekesDatumUzemora = UtolsoDatum.AddDays(Math.Ceiling(rekord.Mennyi_Óra / AtlagosNapiUzemNovekedes));

                                    Esedekes = true;
                                    if (EsedekesDatumNap <= EsedekesDatumUzemora)
                                        UtolsoDatum = EsedekesDatumNap;

                                    else
                                    {
                                        UtolsoDatum = EsedekesDatumUzemora;
                                        UtolsoUzemora += rekord.Mennyi_Óra;
                                    }
                                }
                                else if (NapEsedekes)
                                {
                                    Esedekes = true;
                                    UtolsoDatum = UtolsoDatum.AddDays(rekord.Mennyi_Dátum);
                                }
                                else if (UzemoraEsedekes)
                                {
                                    Esedekes = true;
                                    SzuksegesNapok = Math.Ceiling(rekord.Mennyi_Óra / AtlagosNapiUzemNovekedes);
                                    UtolsoDatum = UtolsoDatum.AddDays(SzuksegesNapok);
                                    UtolsoUzemora += rekord.Mennyi_Óra;
                                }
                                break;
                        }

                        if (Esedekes && UtolsoDatum.Date <= DtmPckrEloTerv.Value.Date)
                        {
                            DataRow Soradat = AdatTabla.NewRow();

                            Soradat["Sorsz."] = rekord.ID;
                            Soradat["Művelet"] = rekord.Művelet;
                            Soradat["Egység"] = Enum.GetName(typeof(EsztergaEgyseg), rekord.Egység);
                            Soradat["Nap"] = rekord.Mennyi_Dátum;
                            Soradat["Óra"] = rekord.Mennyi_Óra;
                            Soradat["Státusz"] = rekord.Státus ? "Törölt" : "Aktív";
                            Soradat["Utolsó Dátum"] = rekord.Utolsó_Dátum.ToShortDateString();

                            Adat_Eszterga_Uzemora uzemoraRekord = AdatokUzemora
                                .FirstOrDefault(a => a.Dátum.Date == rekord.Utolsó_Dátum.Date && a.Státus == false);
                            Soradat["Utolsó Üzemóra"] = uzemoraRekord != null ? uzemoraRekord.Uzemora : rekord.Utolsó_Üzemóra_Állás;

                            Soradat["Esedékesség Dátuma"] = UtolsoDatum.ToShortDateString();
                            Soradat["Becsült Üzemóra"] = this.BecsultUzemora(UtolsoDatum);
                            Soradat["Megjegyzés"] = rekord.Megjegyzés;

                            RendezettSorok.Add(Soradat);
                        }
                        if (!Esedekes) break;
                    }
                }

                IEnumerable<DataRow> RendezettAdatok = RendezettSorok
                    .OrderBy(sor => DateTime.Parse(sor["Esedékesség Dátuma"].ToStrTrim()))
                    .ThenBy(sor => int.Parse(sor["Sorsz."].ToStrTrim()));

                foreach (DataRow sor in RendezettAdatok)
                    AdatTabla.Rows.Add(sor);

                Tabla.DataSource = AdatTabla;
                SorSzinezes();
                OszlopSzelesseg();
                for (int i = 0; i < 11; i++)
                    Tabla.Columns[i].ReadOnly = true;
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
        /// Betölti a karbantartási műveletek naplózott adatait a táblázatba.
        /// A naplóból származó adatok (művelet, dátum, üzemóra, rögzítő stb.) megjelennek,
        /// rendezve dátum és azonosító szerint.
        /// </summary>
        private void TablaNaploListazas()
        {
            try
            {
                AktivTablaTipus = "Napló";
                Tabla.DataSource = null;
                Tabla.Rows.Clear();
                Tabla.Columns.Clear();
                AdatTabla = new DataTable();
                AdatTabla.Columns.Clear();
                AdatTabla.Columns.Add("Sorsz.");
                AdatTabla.Columns.Add("Művelet");
                AdatTabla.Columns.Add("Nap");
                AdatTabla.Columns.Add("Óra");
                AdatTabla.Columns.Add("Utolsó Dátum");
                AdatTabla.Columns.Add("Utolsó Üzemóra");
                AdatTabla.Columns.Add("Megjegyzés");
                AdatTabla.Columns.Add("Rögzítő");
                AdatTabla.Columns.Add("Rögzítés Dátuma");

                AdatokMuveletNaplo = Kez_Muvelet_Naplo.Lista_Adatok(DtmPckrEloTerv.Value.Year);
                List<DataRow> RendezettSorok = new List<DataRow>();
                foreach (Adat_Eszterga_Muveletek_Naplo rekord in AdatokMuveletNaplo)
                {
                    DataRow Soradat = AdatTabla.NewRow();

                    Soradat["Sorsz."] = rekord.ID;
                    Soradat["Művelet"] = rekord.Művelet;
                    Soradat["Nap"] = rekord.Mennyi_Dátum;
                    Soradat["Óra"] = rekord.Mennyi_Óra;
                    Soradat["Utolsó Dátum"] = rekord.Utolsó_Dátum.ToShortDateString();
                    Soradat["Utolsó Üzemóra"] = rekord.Utolsó_Üzemóra_Állás;
                    Soradat["Megjegyzés"] = rekord.Megjegyzés;
                    Soradat["Rögzítő"] = rekord.Rögzítő;
                    Soradat["Rögzítés Dátuma"] = rekord.Rögzítés_Dátuma.ToShortDateString();

                    RendezettSorok.Add(Soradat);
                }
                IEnumerable<DataRow> RendezettAdatok = RendezettSorok
                    .OrderBy(sor => DateTime.Parse(sor["Utolsó Dátum"].ToStrTrim()))
                    .ThenBy(sor => int.Parse(sor["Sorsz."].ToStrTrim()));

                foreach (DataRow sor in RendezettAdatok)
                    AdatTabla.Rows.Add(sor);

                Tabla.DataSource = AdatTabla;

                Tabla.Columns["Sorsz."].Width = 110;
                Tabla.Columns["Művelet"].Width = 943;
                Tabla.Columns["Nap"].Width = 60;
                Tabla.Columns["Óra"].Width = 60;
                Tabla.Columns["Utolsó Dátum"].Width = 105;
                Tabla.Columns["Utolsó Üzemóra"].Width = 120;
                Tabla.Columns["Megjegyzés"].Width = 221;
                Tabla.Columns["Rögzítő"].Width = 150;
                Tabla.Columns["Rögzítés Dátuma"].Width = 105;
                for (int i = 0; i < 9; i++)
                    Tabla.Columns[i].ReadOnly = true;
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
        /// Beállítja a fő karbantartási tábla oszlopainak szélességét fix értékekkel.
        /// </summary>
        private void OszlopSzelesseg()
        {
            Tabla.Columns["Sorsz."].Width = 80;
            Tabla.Columns["Művelet"].Width = 700;
            Tabla.Columns["Egység"].Width = 110;
            Tabla.Columns["Nap"].Width = 60;
            Tabla.Columns["Óra"].Width = 60;
            Tabla.Columns["Státusz"].Width = 90;
            Tabla.Columns["Utolsó Dátum"].Width = 110;
            Tabla.Columns["Utolsó Üzemóra"].Width = 140;
            Tabla.Columns["Esedékesség Dátuma"].Width = 130;
            Tabla.Columns["Becsült Üzemóra"].Width = 140;
            Tabla.Columns["Megjegyzés"].Width = 254;
        }

        /// <summary>
        /// Beállítja a fő karbantartási tábla oszlopainak szélességét fix értékekkel.
        /// </summary>
        private void TablaUrites()
        {
            Tabla.DataSource = null;
            Tabla.Rows.Clear();
            Tabla.Columns.Clear();
        }

        /// <summary>
        /// Egy adott táblázatsor alapján létrehoz egy naplórekordot a karbantartási művelethez.
        /// Beállítja a szükséges mezőket, mint a dátum, üzemóra, megjegyzés és a rögzítő neve.
        /// A létrejött naplóbejegyzést menti adatbázisba.
        /// </summary>
        private void Naplozas(List<DataGridViewRow> sorok, List<Adat_Eszterga_Muveletek> adatok)
        {
            try
            {
                List<Adat_Eszterga_Muveletek_Naplo> naploLista = new List<Adat_Eszterga_Muveletek_Naplo>();

                for (int i = 0; i < sorok.Count; i++)
                {
                    DataGridViewRow sor = sorok[i];
                    Adat_Eszterga_Muveletek adat = adatok[i];

                    int id = sor.Cells[0].Value.ToÉrt_Int();
                    string muvelet = sor.Cells[1].Value?.ToStrTrim() ?? string.Empty;
                    int mennyiNap = sor.Cells[3].Value.ToÉrt_Int();
                    int mennyiOra = sor.Cells[4].Value.ToÉrt_Int();
                    string megjegyzes = sor.Cells[10].Value.ToStrTrim();

                    naploLista.Add(new Adat_Eszterga_Muveletek_Naplo(
                        id,
                        muvelet,
                        mennyiNap,
                        mennyiOra,
                        adat.Utolsó_Dátum,
                        adat.Utolsó_Üzemóra_Állás,
                        megjegyzes,
                        Program.PostásNév.ToStrTrim(),
                        DateTime.Today));
                }

                Kez_Muvelet_Naplo.Rogzites(naploLista, DtmPckrEloTerv.Value.Year);
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

        #region Szinezes

        /// <summary>
        /// A karbantartási táblázat minden sorához színt rendel a művelet esedékessége alapján.
        /// Ha a sorhoz tartozó művelet adatai alapján közeledik vagy lejárt az esedékesség,
        /// a háttérszín piros (lejárt), sárga (közeledik), vagy zöld (rendben) lesz beállítva.
        /// A napló táblázat esetén nem történik színezés.
        /// </summary>
        private void SorSzinezes()
        {
            try
            {
                if (AktivTablaTipus == "Napló") return;

                foreach (DataGridViewRow row in Tabla.Rows)
                {
                    if (row.Cells["Sorsz."].Value != null && int.TryParse(row.Cells["Sorsz."].Value.ToStrTrim(), out int Sorszam))
                    {
                        Adat_Eszterga_Muveletek rekord = AdatokMuvelet.FirstOrDefault(r => r.ID == Sorszam);
                        if (rekord != null)
                        {
                            Color hatterszin = Kiszinezes(rekord, TervDatum);

                            row.DefaultCellStyle.BackColor = hatterszin;
                            row.DefaultCellStyle.ForeColor = Color.Black;

                            foreach (DataGridViewCell cell in row.Cells)
                                cell.Style.BackColor = hatterszin;
                        }
                    }
                    else
                        foreach (DataGridViewCell cell in row.Cells)
                            cell.Style.BackColor = Color.White;
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
        /// Meghatározza egy karbantartási rekord színét az esedékesség állapota alapján, a megadott tervdátumhoz viszonyítva.
        /// A szín az idő- vagy üzemóra-alapú határidőkhöz igazodik:
        /// piros (lejárt), sárga (figyelmeztető küszöb közelében), zöld (még nem esedékes).
        /// Bekövetkezés típusnál bármely feltétel teljesülése esedékességnek számít.
        /// </summary>
        private Color Kiszinezes(Adat_Eszterga_Muveletek rekord, DateTime TervDatum)
        {
            try
            {
                int elteltNap = (int)(TervDatum - rekord.Utolsó_Dátum).TotalDays;
                long elteltÓra = BecsultUzemora(TervDatum) - rekord.Utolsó_Üzemóra_Állás;

                int figyNap = rekord.Mennyi_Dátum - TxtBxNapi.Text.ToÉrt_Int();
                int figyÓra = rekord.Mennyi_Óra - TxtBxUzem.Text.ToÉrt_Int();

                switch ((EsztergaEgyseg)rekord.Egység)
                {
                    case EsztergaEgyseg.Dátum:
                        if (elteltNap >= rekord.Mennyi_Dátum)
                            return Color.IndianRed;
                        else if (elteltNap >= figyNap && rekord.Mennyi_Dátum > 1)
                            return Color.Yellow;
                        break;

                    case EsztergaEgyseg.Üzemóra:
                        if (elteltÓra >= rekord.Mennyi_Óra)
                            return Color.IndianRed;
                        else if (elteltÓra >= figyÓra)
                            return Color.Yellow;
                        break;

                    case EsztergaEgyseg.Beköv:
                        bool datumEsedekes = elteltNap >= rekord.Mennyi_Dátum;
                        bool oraEsedekes = elteltÓra >= rekord.Mennyi_Óra;

                        if (datumEsedekes || oraEsedekes)
                            return Color.IndianRed;
                        else if (elteltNap >= figyNap || elteltÓra >= figyÓra)
                            return Color.Yellow;
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
            return Color.LawnGreen;
        }
        #endregion

        #region Szamolasok

        /// <summary>
        /// Kiszámítja az átlagos napi üzemóra-növekedést a megadott dátumig bezárólag.
        /// Legalább két, nem törölt adat szükséges a számításhoz.  
        /// Az eredményt a napokra eső üzemóra-különbségek átlagaként adja vissza.
        /// </summary>
        private double AtlagUzemoraNovekedesKiszamitasa(DateTime tervDatum)
        {
            double Szamlalo = 0;
            double Osszeg = 0;
            try
            {
                List<Adat_Eszterga_Uzemora> Rekordok = AdatokUzemora
                    .Where(a => a.Dátum <= tervDatum && !a.Státus)
                    .OrderBy(a => a.Dátum)
                    .ToList();

                if (Rekordok.Count < 2)
                    return 0;
                int napokSzama = int.TryParse(TxtBxNapiUzemoraAtlag.Text, out int n) ? n : 30;
                List<Adat_Eszterga_Uzemora> utolsoNUzemora = Rekordok.Skip(Math.Max(0, Rekordok.Count - napokSzama)).ToList();

                for (int i = 1; i < utolsoNUzemora.Count; i++)
                {
                    double Napok = (utolsoNUzemora[i].Dátum - utolsoNUzemora[i - 1].Dátum).TotalDays;
                    double Kulonbseg = utolsoNUzemora[i].Uzemora - utolsoNUzemora[i - 1].Uzemora;

                    if (Napok > 0 && Kulonbseg > 0)
                    {
                        Osszeg += Kulonbseg / Napok;
                        Szamlalo++;
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
            return Szamlalo > 0 ? Osszeg / Szamlalo : 0;
        }

        /// <summary>
        /// Becsült üzemóra értéket számol a megadott jövőbeli dátumhoz, 
        /// az eddigi rögzített üzemóra növekedés átlaga alapján.
        /// </summary>
        private long BecsultUzemora(DateTime EloDatum)
        {
            double NapiNovekedes = 0;
            double NapokEloDatumhoz = 0;
            double osszegRate = 0;
            int aktivNapokSzama = 0;
            long tenylegesMaximum = 0;
            Adat_Eszterga_Uzemora UtolsoRekord = null;

            try
            {
                if (AdatokUzemora == null || AdatokUzemora.Count < 2)
                    return 0;

                List<Adat_Eszterga_Uzemora> rekord = (from a in AdatokUzemora
                                                      where !a.Státus
                                                      orderby a.Dátum
                                                      select a).ToList();

                if (rekord.Count < 2)
                    return 0;
                tenylegesMaximum = rekord.Max(r => r.Uzemora);
                NapiNovekedes = 0;

                for (int i = 1; i < rekord.Count; i++)
                {
                    double Napok = (rekord[i].Dátum - rekord[i - 1].Dátum).TotalDays;
                    double Kulonbseg = rekord[i].Uzemora - rekord[i - 1].Uzemora;

                    if (Napok > 0 && Kulonbseg > 0)
                    {
                        osszegRate += Kulonbseg / Napok;
                        aktivNapokSzama++;
                    }
                }

                if (aktivNapokSzama > 0)
                    NapiNovekedes = osszegRate / aktivNapokSzama;
                else
                    NapiNovekedes = 0;

                NapiNovekedes = Math.Floor(NapiNovekedes);

                UtolsoRekord = rekord.Where(a => !a.Státus).LastOrDefault();
                NapokEloDatumhoz = (EloDatum - UtolsoRekord.Dátum).TotalDays;

                if (rekord.Count > 0)
                    tenylegesMaximum = rekord.Max(r => r.Uzemora);
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
            if (UtolsoRekord == null) return 0;

            long becsultErtek = UtolsoRekord.Uzemora + (long)(NapiNovekedes * NapokEloDatumhoz);

            if (EloDatum.Date <= DateTime.Today)
                return Math.Max(becsultErtek, tenylegesMaximum);

            return becsultErtek;
        }

        /// <summary>
        /// Kiszámítja az esedékes dátumot nap alapú karbantartás esetén.
        /// </summary>
        /// <param name="utolsoDatum">Az utolsó elvégzett karbantartás dátuma.</param>
        /// <param name="mennyiNap">A karbantartás gyakorisága napokban.</param>
        /// <returns>
        /// Esedékes dátum, vagy 1900.01.01, ha a gyakoriság nulla vagy kisebb.
        /// </returns>
        private DateTime DatumAlapuEsedekesDatum(DateTime utolsoDatum, int mennyiNap)
        {
            return mennyiNap > 0
                ? utolsoDatum.AddDays(mennyiNap)
                : new DateTime(1900, 1, 1);
        }

        /// <summary>
        /// Kiszámítja az esedékes dátumot üzemóra alapú karbantartás esetén.
        /// </summary>
        /// <param name="mennyiOra">A karbantartás gyakorisága üzemórában.</param>
        /// <param name="utolsoUzemora">Az utolsó elvégzett karbantartás üzemóra állása.</param>
        /// <returns>
        /// Esedékes dátum az átlagos napi üzemóra-növekedés alapján,
        /// vagy 1900.01.01, ha a számítás nem végezhető el.
        /// </returns>
        private DateTime UzemoraAlapuEsedekesDatum(int mennyiOra, long utolsoUzemora)
        {
            if (mennyiOra <= 0)
                return new DateTime(1900, 1, 1);

            long aktualisUzemora = AdatokUzemora
                .Where(a => !a.Státus)
                .OrderByDescending(a => a.Dátum)
                .FirstOrDefault()?.Uzemora ?? 0;

            long teljesitett = Math.Max(0, aktualisUzemora - utolsoUzemora);
            long hatralevo = mennyiOra - teljesitett;

            if (hatralevo <= 0)
                return DateTime.Today;

            double napiNov = AtlagUzemoraNovekedesKiszamitasa(DateTime.Today);
            if (napiNov <= 0)
                return new DateTime(1900, 1, 1);

            double napok = Math.Ceiling(hatralevo / napiNov);

            double maxNap = (DateTime.MaxValue.Date - DateTime.Today).TotalDays - 1;
            napok = Math.Min(napok, maxNap);

            return DateTime.Today.AddDays(napok);
        }

        /// <summary>
        /// Meghatározza a végső esedékes dátumot, figyelembe véve a nap alapú
        /// és üzemóra alapú karbantartási számításokat.
        /// </summary>
        /// <param name="utolsoDatum">Az utolsó karbantartás dátuma.</param>
        /// <param name="rekord">A karbantartási művelet adatai.</param>
        /// <param name="uzemoraRekord">Az utolsó üzemóra állást tartalmazó rekord.</param>
        /// <returns>
        /// A legkorábbi érvényes esedékes dátum a két módszer közül,
        /// vagy 1900.01.01, ha egyik sem érvényes.
        /// </returns>
        private DateTime VegsoEsedekesDatumSzamitasa(DateTime utolsoDatum, Adat_Eszterga_Muveletek rekord, Adat_Eszterga_Uzemora uzemoraRekord)
        {
            long utolsoUzemora = uzemoraRekord?.Uzemora ?? rekord.Utolsó_Üzemóra_Állás;

            DateTime datumAlapu = DatumAlapuEsedekesDatum(utolsoDatum, rekord.Mennyi_Dátum);
            DateTime uzemoraAlapu = UzemoraAlapuEsedekesDatum(rekord.Mennyi_Óra, utolsoUzemora);

            bool vanDatum = datumAlapu.Year > 1900;
            bool vanUzemora = uzemoraAlapu.Year > 1900;

            if (vanDatum && vanUzemora) return (datumAlapu <= uzemoraAlapu) ? datumAlapu : uzemoraAlapu;
            if (vanDatum) return datumAlapu;
            if (vanUzemora) return uzemoraAlapu;

            return new DateTime(1900, 1, 1);
        }

        /// <summary>
        /// Ellenőrzi, hogy a művelet módosítása aktuális napon történik-e.  
        /// Ha nem, figyelmeztető üzenetet jelenít meg, és false értékkel tér vissza.
        /// </summary>
        private bool DatumEllenorzes(DateTime MaiDatum, DateTime TervDatum)
        {
            if (MaiDatum != TervDatum)
            {
                MessageBox.Show("Nem lehet előretervezéssel módosítani a műveletet.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Kiszámítja az elmúlt X nap üzemóra-növekedésének átlagát, és megjeleníti az eredményt a felhasználó számára.
        /// Alapértelmezett időtartam 30 nap.  
        /// Ha nincs elegendő adat, figyelmeztető üzenet jelenik meg.
        /// </summary>
        private void AtlagUzemoraFrissites(int Napok = 30)
        {
            try
            {
                if (string.IsNullOrEmpty(TxtBxNapiUzemoraAtlag.Text))
                    TxtBxNapiUzemoraAtlag.Text = Alap_Napi_Atlag.ToStrTrim();

                DateTime KezdetiDatum = DateTime.Today.AddDays(-Napok);

                List<Adat_Eszterga_Uzemora> rekord = (from a in AdatokUzemora
                                                      where !a.Státus && a.Dátum >= KezdetiDatum
                                                      orderby a.Dátum
                                                      select a).ToList();
                if (rekord.Count == 0)
                {
                    LblÁtlagÜzemóraSzám.Text = "Nincs adat az átlag számításhoz.";
                    return;
                }

                double atlag;

                if (rekord.Count == 1)
                {
                    double elteltNap = (DateTime.Today - rekord[0].Dátum).TotalDays;
                    if (elteltNap > 0)
                        atlag = rekord[0].Uzemora / elteltNap;
                    else
                        atlag = rekord[0].Uzemora;
                }
                else
                {
                    List<double> NovekedesiAranyok = new List<double>();
                    for (int i = 1; i < rekord.Count; i++)
                    {
                        double Kulonbseg = rekord[i].Uzemora - rekord[i - 1].Uzemora;
                        double napok = (rekord[i].Dátum - rekord[i - 1].Dátum).TotalDays;
                        if (napok > 0)
                            NovekedesiAranyok.Add(Kulonbseg / napok);
                    }
                    atlag = NovekedesiAranyok.Count > 0 ? NovekedesiAranyok.Average() : 0;
                }

                LblÁtlagÜzemóraSzám.Text = $"Üzemóra növekedése {Napok} napig átlagolva: {Math.Floor(atlag)} üzemóra";
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

        #region Gombok && Muveletek

        /// <summary>
        /// Megnyitja a programhoz tartozó HTML súgófájlt, ha az elérhető.
        /// </summary>
        private void Btn_Sugo_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\KerékEsztergaKarbantartás.html";
                MyF.Megnyitás(hely);
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
        /// Visszaállítja az alapértelmezett értékeket (átlag nap, napi, üzemóra), 
        /// mai napra állítja a tervdátumot, és újratölti az adatokat a táblázatba.
        /// </summary>
        private void Btn_Frissit_Click(object sender, EventArgs e)
        {
            try
            {
                TxtBxNapiUzemoraAtlag.Text = Alap_Napi_Atlag.ToStrTrim();
                TxtBxNapi.Text = Alap_Napi_Szam.ToStrTrim();
                TxtBxUzem.Text = Alap_Uzemora_Szam.ToStrTrim();
                DtmPckrEloTerv.Value = DateTime.Today;
                Btn_Rogzit.Visible = true;
                TablaListazas();
                AtlagUzemoraFrissites();
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
        /// A kiválasztott sor(ok) végrehajtott műveletként történő rögzítése a mai dátummal és aktuális üzemórával.
        /// Lezárja a műveletet, naplózza az adatokat, és frissíti a táblázatot.
        /// </summary>
        private void Btn_Rogzit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tabla.SelectedRows.Count == 0) throw new HibásBevittAdat("Válasszon ki egy vagy több sort a táblázatból.");

                List<Adat_Eszterga_Muveletek> adatLista = new List<Adat_Eszterga_Muveletek>();
                List<DataGridViewRow> naplozandoSorok = new List<DataGridViewRow>();

                DateTime tervDatum = DtmPckrEloTerv.Value.Date;

                if (!DatumEllenorzes(DateTime.Today, tervDatum))
                    return;

                foreach (DataGridViewRow sor in Tabla.SelectedRows)
                {
                    Color hatterSzin = sor.DefaultCellStyle.BackColor;
                    if (hatterSzin == Color.LawnGreen || hatterSzin == Color.Yellow) throw new HibásBevittAdat("Ez a sor nem módosítható, mert már a művelet elkészült vagy nem kell még végrehajtani.");

                    int id = sor.Cells[0].Value.ToÉrt_Int();
                    long aktivUzemora = AdatokUzemora.Count > 0 ? AdatokUzemora.Max(a => a.Uzemora) : 0;

                    Adat_Eszterga_Muveletek adat = new Adat_Eszterga_Muveletek(DateTime.Today, aktivUzemora, id);
                    adatLista.Add(adat);
                    naplozandoSorok.Add(sor);
                }

                Kez_Muvelet.Modositas(adatLista);
                Kez_Muvelet.Torles(adatLista, false);

                for (int i = 0; i < adatLista.Count; i++)
                {
                    naplozandoSorok[i].Cells[6].Value = DateTime.Today.ToShortDateString();
                    naplozandoSorok[i].Cells[7].Value = adatLista[i].Utolsó_Üzemóra_Állás;
                }

                Naplozas(naplozandoSorok, adatLista);
                TablaListazas();

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
                    FileName = $"Eszterga_Karbantartás_Műveletek_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };

                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                string munkalap = "Munka1";
                MyX.DataGridViewToXML(fájlexc, Tabla,munkalap, true);

                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MyF.Megnyitás(fájlexc);
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
                    FileName = $"Eszterga_Karbantartás_Műveletek_{Program.PostásNév.Trim()}_{DateTime.Now:yyyyMMdd_HHmmss}",
                    Filter = "PDF fájl (*.pdf)|*.pdf"
                };

                if (saveDlg.ShowDialog() != DialogResult.OK)
                    return;

                string fajlNev = saveDlg.FileName;
                if (!fajlNev.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                    fajlNev += ".pdf";

                PDFtabla(fajlNev, Tabla);

                MessageBox.Show($"Elkészült a PDF fájl:\n{fajlNev}", "Sikeres mentés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyF.Megnyitás(fajlNev);
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
                List<DataGridViewColumn> visibleCols = tábla.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).ToList();
                if (visibleCols.Count == 0)
                    throw new Exception("Nincsenek látható oszlopok a táblázatban.");

                using (FileStream stream = new FileStream(fájlNév, FileMode.Create))
                {
                    Document doc = new Document(PageSize.A4.Rotate(), 10f, 10f, 20f, 20f);
                    PdfWriter.GetInstance(doc, stream);
                    doc.Open();

                    string arial = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                    BaseFont baseFont = File.Exists(arial)
                        ? BaseFont.CreateFont(arial, BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
                        : BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.EMBEDDED);

                    float headerPt = 10f;
                    float cellPt = 9f;

                    // fix szélességek definiálása (pontban, kb. 1 karakter ~ 5–6 pont)
                    Dictionary<string, float> fixWidths = new Dictionary<string, float>(StringComparer.OrdinalIgnoreCase)
                    {
                        { "Sorsz.", 29f },
                        { "Művelet", 180f },
                        { "Egység", 35f },
                        { "Nap", 22f },
                        { "Óra", 27f },
                        { "Státusz", 33f },
                        { "Utolsó dátum", 43f },
                        { "Utolsó üzemóra", 40f },
                        { "Esedékesség dátuma", 53f },
                        { "Becsült üzemóra", 38f },
                        { "Megjegyzés", 130f }
                    };

                    // PDF tábla létrehozása fix oszlopszélességekkel
                    PdfPTable pdfTable = new PdfPTable(visibleCols.Count)
                    {
                        WidthPercentage = 100f
                    };

                    float[] colWidths = new float[visibleCols.Count];
                    for (int i = 0; i < visibleCols.Count; i++)
                    {
                        string colName = visibleCols[i].HeaderText.Trim();
                        if (fixWidths.ContainsKey(colName))
                            colWidths[i] = fixWidths[colName];
                        else
                            colWidths[i] = 50f; // alapértelmezett szélesség
                    }
                    pdfTable.SetWidths(colWidths);

                    // betűk
                    iTextSharp.text.Font headerITextFont = new iTextSharp.text.Font(baseFont, headerPt, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    iTextSharp.text.Font cellITextFontBase = new iTextSharp.text.Font(baseFont, cellPt, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                    // fejlécek
                    foreach (DataGridViewColumn col in visibleCols)
                    {
                        string headerText = (col.HeaderText ?? "").Trim();
                        bool allowWrap = true;

                        // dátum és óra mezőknél engedjük törni a fejlécet, de az adatnál nem
                        if (headerText.Contains("dátum") || headerText.Contains("üzemóra"))
                            allowWrap = true;

                        PdfPCell headCell = new PdfPCell(new Phrase(headerText, headerITextFont))
                        {
                            BackgroundColor = new BaseColor(240, 240, 240),
                            Padding = 4f,
                            NoWrap = !allowWrap
                        };
                        pdfTable.AddCell(headCell);
                    }

                    // sorok
                    foreach (DataGridViewRow row in tábla.Rows)
                    {
                        if (row.IsNewRow) continue;

                        foreach (DataGridViewColumn col in visibleCols)
                        {
                            DataGridViewCell dgvc = row.Cells[col.Index];
                            string text = dgvc.Value?.ToString() ?? "";

                            Color fore = dgvc.Style.ForeColor;
                            if (fore.IsEmpty) fore = row.DefaultCellStyle.ForeColor;
                            if (fore.IsEmpty) fore = Color.Black;
                            BaseColor foreBase = new BaseColor(fore.R, fore.G, fore.B);

                            Color back = dgvc.Style.BackColor;
                            if (back.IsEmpty) back = row.DefaultCellStyle.BackColor;
                            if (back.IsEmpty) back = Color.White;
                            BaseColor backBase = new BaseColor(back.R, back.G, back.B);

                            iTextSharp.text.Font cellFont = new iTextSharp.text.Font(baseFont, cellPt, iTextSharp.text.Font.NORMAL, foreBase);

                            bool noWrap = false;
                            string header = col.HeaderText.Trim();

                            // bizonyos mezőknél fix hosszt és NoWrap-et kérünk
                            if (header == "Utolsó dátum" || header == "Esedékesség dátuma")
                                noWrap = true; // dátum fix 11 char, nem törjük
                            if (header == "Utolsó üzemóra" || header == "Becsült üzemóra")
                                noWrap = true; // szám mezők fix

                            PdfPCell pdfCell = new PdfPCell(new Phrase(text, cellFont))
                            {
                                BackgroundColor = backBase,
                                Padding = 4f,
                                NoWrap = noWrap,
                                HorizontalAlignment = Element.ALIGN_LEFT,
                                VerticalAlignment = Element.ALIGN_MIDDLE
                            };

                            pdfTable.AddCell(pdfCell);
                        }
                    }

                    doc.Add(pdfTable);
                    doc.Close();
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
        /// Betölti a naplózott műveletek listáját a táblázatba, és elrejti a rögzítés gombot.
        /// </summary>
        private void Btn_Naplo_Listaz_Click(object sender, EventArgs e)
        {
            try
            {
                TablaNaploListazas();
                Btn_Rogzit.Visible = false;
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
        /// Kezeli a napi üzemóra átlag számításához megadott napok számának változását.
        /// Ellenőrzi, hogy a bevitt érték egész szám és nem haladja meg a 100000-es(Max_Napok konstans) felső korlátot, 
        /// Ha az érték nagyobb ennél, visszaállítja 100000-re, majd frissíti az átlag üzemórát.
        /// Hibakezeléssel biztosítja a megbízható működést és a felhasználó tájékoztatását.
        /// </summary>
        private void TxtBxNapiUzemoraAtlag_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(TxtBxNapiUzemoraAtlag.Text, out int napok))
                {
                    if (napok > Max_Napok)
                        TxtBxNapiUzemoraAtlag.Text = Max_Napok.ToStrTrim();

                    if (napok > 0)
                        AtlagUzemoraFrissites(Math.Min(napok, Max_Napok));
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
        /// A napi határérték mező módosításakor újratölti a táblázatot – aktuális vagy jövőbeni terv szerint.
        /// </summary>
        private void TxtBxNapi_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (DtmPckrEloTerv.Value.Date == DateTime.Today)
                    TablaListazas();
                else
                    EloreTervezesListazasa();
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
        /// Az üzemóra határérték mező módosításakor újratölti a táblázatot – aktuális vagy jövőbeni terv szerint.
        /// </summary>
        private void TxtBxUzem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (DtmPckrEloTerv.Value.Date == DateTime.Today)
                    TablaListazas();
                else
                    EloreTervezesListazasa();
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
        /// A terv dátum megváltozásakor betölti az aktuális vagy előre tervezett műveleti listát.
        /// Ha a dátum a mai napnál korábbi, figyelmeztet és visszaállítja a mai dátumra.
        /// </summary>
        private void DtmPckrEloTerv_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (DtmPckrEloTerv.Value < DateTime.Today)
                {
                    DtmPckrEloTerv.Value = DateTime.Today;
                    throw new HibásBevittAdat("A dátum nem lehet kisebb, mint a mai dátum.");
                }

                if (DtmPckrEloTerv.Value == DateTime.Today)
                    TablaListazas();
                else
                    EloreTervezesListazasa();

                Btn_Rogzit.Visible = true;
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
        /// A megjegyzés cella szerkesztésének lezárásakor ellenőrzi, történt-e változás.
        /// Ha új megjegyzés került be, elmenti azt, ha törlés történt, törli az értéket.
        /// </summary>
        private void Tabla_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow sor = Tabla.Rows[e.RowIndex];

                if (!Baross || !DatumEllenorzes(DateTime.Today, TervDatum))
                {
                    sor.Cells[10].Value = null;
                    Tabla.InvalidateRow(e.RowIndex);
                    return;
                }

                Tabla.EndEdit();

                string Megjegyzes = sor.Cells[10].Value?.ToStrTrim();
                int ID = sor.Cells[0].Value.ToÉrt_Int();

                string ElozoMegjegyzes = (from rekord in Kez_Muvelet.Lista_Adatok()
                                          where rekord.ID == ID
                                          select rekord.Megjegyzés)?.FirstOrDefault()?.ToStrTrim();

                if (ElozoMegjegyzes == Megjegyzes) throw new HibásBevittAdat("Nem történt változás a megjegyzés változtatásakor.");


                if (!string.IsNullOrEmpty(Megjegyzes))
                {
                    Kez_Muvelet.Modositas_Megjegyzes(Megjegyzes, ID);
                    MessageBox.Show("A megjegyzés mentésre került.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    List<Adat_Eszterga_Muveletek> lista = new List<Adat_Eszterga_Muveletek>
                    {
                        new Adat_Eszterga_Muveletek(ID)
                    };
                    Kez_Muvelet.Torles(lista, false);
                    MessageBox.Show("A megjegyzés törlésre került.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Tabla.InvalidateRow(e.RowIndex);
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
        /// Eseménykezelő, amely a DataGridView adatforrásának kötése után hívódik meg.
        /// Meghívja a Sorszinezes metódust, hogy a sorokat megjelenítési színezéssel lássa el.
        /// </summary>
        private void Tabla_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            SorSzinezes();
        }
        #endregion
    }
}