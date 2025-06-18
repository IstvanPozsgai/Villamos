using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Ablakok._4_Nyilvántartások.Kerékeszterga;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using Villamos.Villamos_Kezelők;
using Application = System.Windows.Forms.Application;
using Funkcio = Villamos.Villamos_Ablakok._4_Nyilvántartások.Kerékeszterga.Eszterga_Funkció;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok._5_Karbantartás.Eszterga_Karbantartás
{
    public delegate void Event_Kidobó();
    public partial class Ablak_Eszterga_Karbantartás : Form
    {
        #region Osztalyszintu elemek
        DateTime TervDatum;
        readonly bool Baross = Program.PostásTelephely.Trim() == "Angyalföld";
        private string AktivTablaTipus;
        readonly DataTable AdatTabla = new DataTable();
        #endregion

        #region Listák
        private List<Adat_Eszterga_Muveletek> AdatokMuvelet;
        private List<Adat_Eszterga_Uzemora> AdatokUzemora;
        private List<Adat_Eszterga_Muveletek_Naplo> AdatokMuveletNaplo;
        #endregion

        #region Kezelők
        readonly Kezelo_Eszterga_Muveletek Kez_Muvelet = new Kezelo_Eszterga_Muveletek();
        readonly Kezelo_Eszterga_Muveletek_Naplo Kez_Muvelet_Naplo = new Kezelo_Eszterga_Muveletek_Naplo();
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
                string hely = $@"{Application.StartupPath}/Főmérnökség/Adatok/Kerékeszterga";

                if (!Directory.Exists(hely))
                    Directory.CreateDirectory(hely);

                //hely += "/Eszterga_Karbantartás.accdb";
                hely += "/Eszterga_Karbantartás.mdb";

                if (!File.Exists(hely))
                    Adatbázis_Létrehozás.Eszterga_Karbantartás(hely);

                //string helyNapló = $@"{Application.StartupPath}/Főmérnökség/adatok/Kerékeszterga/Eszterga_Karbantartás_{DateTime.Now.Year}_napló.accdb";
                string helyNaplo = $@"{Application.StartupPath}/Főmérnökség/adatok/Kerékeszterga/Eszterga_Karbantartás_{DateTime.Now.Year}_napló.mdb";

                if (!File.Exists(helyNaplo))
                    Adatbázis_Létrehozás.Eszterga_Karbantartas_Naplo(helyNaplo);

                AdatokUzemora = Funkcio.Eszterga_UzemoraFeltolt();

                Adat_Eszterga_Uzemora rekord = (from a in AdatokUzemora
                                                where a.Dátum.Date == DateTime.Today && a.Státus != true
                                                select a).FirstOrDefault();

                if (rekord != null)
                {
                    MessageBox.Show($"A mai napon már rögzítettek üzemóra adatot.\nAz utolsó rögzített üzemóra: {rekord.Uzemora}.",
                                    "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Uzemora = rekord.Uzemora;
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
                Jogosultsagkiosztas();
                TablaListazas();
                AtlagUzemoraFrissites(30);
                AdatokMuvelet = Funkcio.Eszterga_KarbantartasFeltolt();
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
                Btn_Rögzít.Visible = Baross;
                Btn_Módosítás.Visible = Baross;

                //módosítás 1
                //Ablak_Eszterga_Karbantartás_Segéd oldal használja az 1. módosításokat
                Btn_Módosítás.Enabled = MyF.Vanjoga(melyikelem, 1);

                //módosítás 2
                Btn_Rögzít.Enabled = MyF.Vanjoga(melyikelem, 2);

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
        private void Btn_Módosítás_Click(object sender, EventArgs e)
        {
            if (Uj_ablak_EsztergaModosit == null)
            {
                Uj_ablak_EsztergaModosit = new Ablak_Eszterga_Karbantartás_Módosít();
                Uj_ablak_EsztergaModosit.FormClosed += Új_ablak_EsztergaMódosít_Closed;
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
        private void Új_ablak_EsztergaMódosít_Closed(object sender, FormClosedEventArgs e)
        {
            Uj_ablak_EsztergaModosit = null;
        }

        /// <summary>
        /// A fő ablak bezárásakor automatikusan bezárja a megnyitott módosító ablakot is,
        /// ha az még fut.
        /// </summary>
        private void Ablak_Eszterga_Karbantartás_FormClosed(object sender, FormClosedEventArgs e)
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
            Bekövetkezés = 3
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
                TablaUrites();
                AdatTabla.Columns.Clear();
                AdatTabla.Rows.Clear();
                AdatTabla.Columns.Add("Sorszám");
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

                AdatokMuvelet = Funkcio.Eszterga_KarbantartasFeltolt();
                AdatokUzemora = Funkcio.Eszterga_UzemoraFeltolt();
                TervDatum = DtmPckrElőTerv.Value.Date;

                AdatokMuvelet = AdatokMuvelet.OrderBy(rekord =>
                    Kiszinezes(rekord, TervDatum) == Color.IndianRed ? 0 :
                    Kiszinezes(rekord, TervDatum) == Color.Yellow ? 1 : 2).ToList();

                foreach (Adat_Eszterga_Muveletek rekord in AdatokMuvelet)
                {
                    if (rekord.Státus != true)
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

                        Soradat["Utolsó Üzemóra"] = uzemoraRekord != null ? uzemoraRekord.Uzemora : rekord.Utolsó_Üzemóra_Állás;
                        DateTime EsedekesDatum = DatumEsedekesegSzamitasa(rekord.Utolsó_Dátum, rekord, uzemoraRekord);
                        Soradat["Esedékesség Dátuma"] = EsedekesDatum.ToShortDateString();
                        Soradat["Becsült Üzemóra"] = BecsultUzemora(EsedekesDatum);

                        Soradat["Megjegyzés"] = rekord.Megjegyzés;

                        AdatTabla.Rows.Add(Soradat);
                    }
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
        /// Előrejelzést készít a jövőbeli karbantartási műveletekről az üzemóra és a dátum alapján.
        /// </summary>
        private void EloreTervezesListazasa()
        {
            try
            {
                AktivTablaTipus = "EloreTervezes";
                TablaUrites();
                DataTable AdatTabla = new DataTable();
                AdatTabla.Columns.Clear();
                AdatTabla.Rows.Clear();
                AdatTabla.Columns.Add("Sorszám");
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

                AdatokMuvelet = Funkcio.Eszterga_KarbantartasFeltolt();
                AdatokUzemora = Funkcio.Eszterga_UzemoraFeltolt();
                TervDatum = DtmPckrElőTerv.Value.Date;
                double SzuksegesNapok;

                List<DataRow> RendezettSorok = new List<DataRow>();

                foreach (Adat_Eszterga_Muveletek rekord in AdatokMuvelet)
                {
                    if (rekord.Státus == true) continue;

                    int ID = rekord.ID;
                    DateTime UtolsoDatum = rekord.Utolsó_Dátum;
                    long UtolsoUzemora = rekord.Utolsó_Üzemóra_Állás;
                    long BecsultUzemora = this.BecsultUzemora(TervDatum);

                    while (UtolsoDatum.AddDays(rekord.Mennyi_Dátum) <= TervDatum || (UtolsoUzemora + rekord.Mennyi_Óra) >= BecsultUzemora)
                    {
                        bool Esedekes = false;

                        if (rekord.Egység == (int)EsztergaEgyseg.Dátum)
                        {
                            if ((TervDatum - UtolsoDatum).TotalDays >= rekord.Mennyi_Dátum)
                            {
                                Esedekes = true;
                                UtolsoDatum = UtolsoDatum.AddDays(rekord.Mennyi_Dátum);
                            }
                        }
                        else
                        {
                            double AtlagosNapiUzemNovekedes = AtlagUzemoraNovekedesKiszamitasa(TervDatum);

                            if (rekord.Egység == (int)EsztergaEgyseg.Üzemóra)
                            {
                                if ((BecsultUzemora - UtolsoUzemora) >= rekord.Mennyi_Óra)
                                {
                                    Esedekes = true;

                                    SzuksegesNapok = Math.Ceiling(rekord.Mennyi_Óra / AtlagosNapiUzemNovekedes);

                                    UtolsoDatum = UtolsoDatum.AddDays(SzuksegesNapok);
                                    UtolsoUzemora += rekord.Mennyi_Óra;
                                }
                            }
                            else if (rekord.Egység == (int)EsztergaEgyseg.Bekövetkezés)
                            {
                                bool NapEsedekes = (TervDatum - UtolsoDatum).TotalDays >= rekord.Mennyi_Dátum;
                                bool UzemoraEsedekes = (BecsultUzemora - UtolsoUzemora) >= rekord.Mennyi_Óra;

                                if (NapEsedekes && UzemoraEsedekes)
                                {
                                    DateTime EsedekesDatumNap = UtolsoDatum.AddDays(rekord.Mennyi_Dátum);
                                    DateTime EsedekesDatumUzemora = UtolsoDatum.AddDays(Math.Ceiling(rekord.Mennyi_Óra / AtlagosNapiUzemNovekedes));

                                    if (EsedekesDatumNap <= EsedekesDatumUzemora)
                                    {
                                        Esedekes = true;
                                        UtolsoDatum = EsedekesDatumNap;
                                    }
                                    else
                                    {
                                        Esedekes = true;
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
                                    UtolsoUzemora += rekord.Mennyi_Óra;
                                    UtolsoDatum = UtolsoDatum.AddDays(SzuksegesNapok);
                                }
                            }
                        }

                        if (Esedekes && UtolsoDatum.Date <= DtmPckrElőTerv.Value.Date)
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
                    .ThenBy(sor => int.Parse(sor["Sorszám"].ToStrTrim()));

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
                DataTable AdatTabla = new DataTable();
                AdatTabla.Columns.Clear();
                AdatTabla.Columns.Add("Művelet Sorszáma");
                AdatTabla.Columns.Add("Művelet");
                AdatTabla.Columns.Add("Nap");
                AdatTabla.Columns.Add("Óra");
                AdatTabla.Columns.Add("Utolsó Dátum");
                AdatTabla.Columns.Add("Utolsó Üzemóra");
                AdatTabla.Columns.Add("Megjegyzés");
                AdatTabla.Columns.Add("Rögzítő");
                AdatTabla.Columns.Add("Rögzítés Dátuma");

                AdatokMuveletNaplo = Funkcio.Eszterga_KarbantartasNaplóFeltölt();
                List<DataRow> RendezettSorok = new List<DataRow>();
                foreach (Adat_Eszterga_Muveletek_Naplo rekord in AdatokMuveletNaplo)
                {
                    DataRow Soradat = AdatTabla.NewRow();

                    Soradat["Művelet Sorszáma"] = rekord.ID;
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
                    .ThenBy(sor => int.Parse(sor["Művelet Sorszáma"].ToStrTrim()));

                foreach (DataRow sor in RendezettAdatok)
                    AdatTabla.Rows.Add(sor);

                Tabla.DataSource = AdatTabla;

                Tabla.Columns["Művelet Sorszáma"].Width = 110;
                Tabla.Columns["Művelet"].Width = 950;
                Tabla.Columns["Nap"].Width = 60;
                Tabla.Columns["Óra"].Width = 60;
                Tabla.Columns["Utolsó Dátum"].Width = 105;
                Tabla.Columns["Utolsó Üzemóra"].Width = 120;
                Tabla.Columns["Megjegyzés"].Width = 221;
                Tabla.Columns["Rögzítő"].Width = 150;
                Tabla.Columns["Rögzítés Dátuma"].Width = 115;
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
            Tabla.Columns["Sorszám"].Width = 97;
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
        private void Naplozas(DataGridViewRow Sor, DateTime UtolsóDátum, long UtolsóÜzemóra)
        {
            try
            {
                int Id = Sor.Cells[0].Value.ToÉrt_Int();
                string Muvelet = Sor.Cells[1].Value?.ToStrTrim() ?? "";
                int MennyiNap = Sor.Cells[3].Value.ToÉrt_Int();
                int MennyiOra = Sor.Cells[2].Value.ToÉrt_Int();
                string Megjegyzes = Sor.Cells[10].Value.ToStrTrim();
                string Rogzito = Program.PostásNév.ToStrTrim();
                DateTime MaiDatum = DateTime.Today;

                Adat_Eszterga_Muveletek_Naplo ADAT = new Adat_Eszterga_Muveletek_Naplo(Id,
                                                              Muvelet,
                                                              MennyiNap,
                                                              MennyiOra,
                                                              UtolsóDátum,
                                                              UtolsóÜzemóra,
                                                              Megjegyzes,
                                                              Rogzito,
                                                              MaiDatum);
                Kez_Muvelet_Naplo.EsztergaNaplozas(ADAT);
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
                foreach (DataGridViewRow row in Tabla.Rows)
                {
                    if (AktivTablaTipus == "Napló") return;
                    if (row.IsNewRow) continue;
                    if (row.Cells["Sorszám"].Value != null && int.TryParse(row.Cells["Sorszám"].Value.ToStrTrim(), out int Sorszam))
                    {
                        Adat_Eszterga_Muveletek rekord = AdatokMuvelet.FirstOrDefault(r => r.ID == Sorszam);

                        if (rekord != null)
                        {
                            Color hatterszin = Kiszinezes(rekord, TervDatum);
                            row.DefaultCellStyle.BackColor = hatterszin;
                        }
                    }
                    else
                        row.DefaultCellStyle.BackColor = Color.White;
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
                int Egyseg = rekord.Egység;
                DateTime UtolsoDatum = rekord.Utolsó_Dátum;
                long UtolsoUzemora = rekord.Utolsó_Üzemóra_Állás;
                long AktualisUzemora = BecsultUzemora(TervDatum);
                int ElteltNapok = (int)(TervDatum - UtolsoDatum).TotalDays;
                long ElteltOrak = AktualisUzemora - UtolsoUzemora;
                int ElorejelezDatum = rekord.Mennyi_Dátum - TxtBxNapi.Text.ToÉrt_Int();
                int ElorejelezUzem = rekord.Mennyi_Óra - TxtBxÜzem.Text.ToÉrt_Int();

                if (Egyseg == (int)EsztergaEgyseg.Dátum)
                {
                    if (ElteltNapok >= rekord.Mennyi_Dátum)
                        return Color.IndianRed;
                    else if (ElteltNapok >= ElorejelezDatum && rekord.Mennyi_Dátum > 1)
                        return Color.Yellow;
                    else
                        return Color.LawnGreen;
                }
                else if (Egyseg == (int)EsztergaEgyseg.Üzemóra)
                {
                    if (ElteltOrak >= rekord.Mennyi_Óra)
                        return Color.IndianRed;
                    else if (ElteltOrak >= ElorejelezUzem)
                        return Color.Yellow;
                    else
                        return Color.LawnGreen;
                }
                else if (Egyseg == (int)EsztergaEgyseg.Bekövetkezés)
                {
                    bool Datum = (TervDatum - UtolsoDatum).TotalDays >= rekord.Mennyi_Dátum;
                    bool Uzemora = (AktualisUzemora - UtolsoUzemora) >= rekord.Mennyi_Óra;

                    if (Datum && Uzemora || Datum || Uzemora)
                        return Color.IndianRed;
                    else if (ElteltNapok >= ElorejelezDatum || ElteltOrak >= ElorejelezUzem)
                        return Color.Yellow;
                    else
                        return Color.LawnGreen;
                }
                return Color.LawnGreen;
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
        #endregion

        #region Szamolasok

        /// <summary>
        /// Kiszámítja az átlagos napi üzemóra-növekedést a megadott dátumig bezárólag.
        /// Legalább két, nem törölt adat szükséges a számításhoz.  
        /// Az eredményt a napokra eső üzemóra-különbségek átlagaként adja vissza.
        /// </summary>
        private double AtlagUzemoraNovekedesKiszamitasa(DateTime tervDatum)
        {
            try
            {
                List<Adat_Eszterga_Uzemora> rekord = AdatokUzemora
                    .Where(a => a.Dátum <= tervDatum && !a.Státus)
                    .OrderBy(a => a.Dátum)
                    .ToList();

                if (rekord.Count < 1)
                    throw new Exception("Nincs elegendő adat az üzemóra átlagának számításához.");

                double NapiAtlagaosUzemNovekedes = 0;
                for (int i = 1; i < rekord.Count; i++)
                {
                    double napok = (rekord[i].Dátum - rekord[i - 1].Dátum).TotalDays;
                    if (napok > 0)
                        NapiAtlagaosUzemNovekedes += (rekord[i].Uzemora - rekord[i - 1].Uzemora) / napok;
                }
                return NapiAtlagaosUzemNovekedes / (rekord.Count - 1);
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
        /// Meghatározza, hogy egy művelet következő esedékessége melyik dátumra várható, 
        /// figyelembe véve mind a dátum-, mind az üzemóra-alapú ütemezést.  
        /// A két lehetséges esedékességi dátum közül a korábbit adja vissza.
        /// </summary>
        private DateTime DatumEsedekesegSzamitasa(DateTime UtolsoDatum, Adat_Eszterga_Muveletek rekord, Adat_Eszterga_Uzemora uzemoraRekord)
        {
            try
            {
                DateTime? EsedekesDatumNap = null;
                if (rekord.Mennyi_Dátum > 0)
                    EsedekesDatumNap = UtolsoDatum.AddDays(rekord.Mennyi_Dátum);

                DateTime? EsedekesDatumUzemora = null;
                if (rekord.Mennyi_Óra > 0 && uzemoraRekord != null)
                {
                    double NapiUzemoraNovekedes = AtlagUzemoraNovekedesKiszamitasa(UtolsoDatum);

                    if (NapiUzemoraNovekedes > 0)
                    {
                        double NapokEsedekessegig = rekord.Mennyi_Óra / NapiUzemoraNovekedes;
                        EsedekesDatumUzemora = UtolsoDatum.AddDays(Math.Ceiling(NapokEsedekessegig));
                    }
                }

                if (EsedekesDatumNap.HasValue && EsedekesDatumUzemora.HasValue)
                {
                    return EsedekesDatumNap.Value <= EsedekesDatumUzemora.Value
                        ? EsedekesDatumNap.Value
                        : EsedekesDatumUzemora.Value;
                }

                if (EsedekesDatumNap.HasValue)
                    return EsedekesDatumNap.Value;

                if (EsedekesDatumUzemora.HasValue)
                    return EsedekesDatumUzemora.Value;

                return UtolsoDatum;
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
        /// Becsült üzemóra értéket számol a megadott jövőbeli dátumhoz, 
        /// az eddigi rögzített üzemóra növekedés átlaga alapján.
        /// </summary>
        private long BecsultUzemora(DateTime EloDatum)
        {
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

                double NapiNovekedes = 0;

                for (int i = 1; i < rekord.Count; i++)
                {
                    double Napok = (rekord[i].Dátum - rekord[i - 1].Dátum).TotalDays;
                    if (Napok > 0)
                        NapiNovekedes += (rekord[i].Uzemora - rekord[i - 1].Uzemora) / Napok;
                }
                NapiNovekedes /= rekord.Count - 1;
                NapiNovekedes = Math.Floor(NapiNovekedes);

                Adat_Eszterga_Uzemora UtolsoRekord = rekord
                    .Where(a => !a.Státus)
                    .LastOrDefault();

                double NapokEloDatumhoz = (EloDatum - UtolsoRekord.Dátum).TotalDays;
                return UtolsoRekord.Uzemora + (long)(NapiNovekedes * NapokEloDatumhoz);
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
                    TxtBxNapiUzemoraAtlag.Text = "30";

                DateTime KezdetiDatum = DateTime.Today.AddDays(-Napok);

                List<Adat_Eszterga_Uzemora> rekord = (from a in AdatokUzemora
                                                      where !a.Státus && a.Dátum >= KezdetiDatum
                                                      orderby a.Dátum
                                                      select a).ToList();

                if (rekord.Count < 2)
                {
                    LblÁtlagÜzemóraSzám.Text = $"Nincs elegendő adat az átlag számításhoz.";
                    return;
                }

                List<double> NovekedesiAranyok = new List<double>();
                for (int i = 1; i < rekord.Count; i++)
                {
                    double Kulonbseg = rekord[i].Uzemora - rekord[i - 1].Uzemora;
                    double napok = (rekord[i].Dátum - rekord[i - 1].Dátum).TotalDays;
                    if (napok > 0)
                        NovekedesiAranyok.Add(Kulonbseg / napok);
                }

                double Atlag = NovekedesiAranyok.Count > 0 ? NovekedesiAranyok.Average() : 0;

                LblÁtlagÜzemóraSzám.Text = $"Üzemóra növekedése {Napok} napig átlagolva: {Math.Floor(Atlag)} üzemóra";

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
        private void Btn_Súgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\KerékEsztergaKarbantartás.html";
                MyE.Megnyitás(hely);
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
        private void Btn_Frissít_Click(object sender, EventArgs e)
        {
            try
            {
                TxtBxNapiUzemoraAtlag.Text = "30";
                TxtBxNapi.Text = "5";
                TxtBxÜzem.Text = "8";
                DtmPckrElőTerv.Value = DateTime.Today;
                Btn_Rögzít.Visible = true;
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
        private void Btn_Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tabla.SelectedRows.Count > 0)
                {
                    List<Adat_Eszterga_Muveletek> AdatLista = new List<Adat_Eszterga_Muveletek>();
                    List<DataGridViewRow> NaplozandoSorok = new List<DataGridViewRow>();

                    TervDatum = DtmPckrElőTerv.Value.Date;

                    if (!DatumEllenorzes(DateTime.Today, TervDatum))
                        return;

                    foreach (DataGridViewRow Sor in Tabla.SelectedRows)
                    {
                        Color HatterSzin = Sor.DefaultCellStyle.BackColor;

                        if (HatterSzin == Color.LawnGreen || HatterSzin == Color.Yellow)
                        {
                            MessageBox.Show("Ez a sor nem módosítható, mert már a művelet elkészült vagy nem kell még végrehajtani.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        int Id = Sor.Cells[0].Value.ToÉrt_Int();
                        long AktivUzemora = AdatokUzemora.Count > 0 ? AdatokUzemora.Max(a => a.Uzemora) : 0;

                        Adat_Eszterga_Muveletek ADAT = new Adat_Eszterga_Muveletek(DateTime.Today, AktivUzemora, Id);
                        AdatLista.Add(ADAT);
                        NaplozandoSorok.Add(Sor);
                    }
                    Kez_Muvelet.Modositas(AdatLista);
                    Kez_Muvelet.Torles(AdatLista, false);

                    for (int i = 0; i < AdatLista.Count; i++)
                    {
                        NaplozandoSorok[i].Cells[4].Value = DateTime.Today;
                        NaplozandoSorok[i].Cells[5].Value = AdatLista[i].Utolsó_Üzemóra_Állás;

                        Naplozas(NaplozandoSorok[i], DateTime.Today, AdatLista[i].Utolsó_Üzemóra_Állás);
                    }

                    TablaListazas();

                    MessageBox.Show("Az adatok rögzítése megtörtént.", "Rögzítve.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Válasszon ki egy vagy több sort a táblázatból.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// A táblázat tartalmát Excel fájlba exportálja, majd automatikusan megnyitja a fájlt.
        /// A felhasználó kiválaszthatja a fájl mentési helyét és nevét.
        /// </summary>
        //private void Btn_Excel_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Tábla.Rows.Count <= 0) throw new HibásBevittAdat("Nincs sora a táblázatnak!");
        //        string fájlexc;
        //        SaveFileDialog SaveFileDialog1 = new SaveFileDialog
        //        {
        //            InitialDirectory = "MyDocuments",
        //            Title = "Teljes tartalom mentése Excel fájlba",
        //            FileName = $"Eszterga_Karbantartás_Műveletek_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
        //            Filter = "Excel |*.xlsx"
        //        };
        //        if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
        //            fájlexc = SaveFileDialog1.FileName;
        //        else
        //            return;
        //        fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);

        //        MyE.EXCELtábla(fájlexc, Tábla, false, true);
        //        MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        MyE.Megnyitás($"{fájlexc}.xlsx");
        //    }
        //    catch (HibásBevittAdat ex)
        //    {
        //        MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    catch (Exception ex)
        //    {
        //        HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
        //        MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        private void Btn_Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tabla.Rows.Count <= 0)
                    throw new HibásBevittAdat("Nincs sora a táblázatnak!");

                DialogResult Valasztas = MessageBox.Show(
                    "Hogyan szeretné menteni a táblázatot?\n\nIgen = Excel\nNem = PDF\nMégse = Kilépés",
                    "Mentés típusa",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1);

                if (Valasztas == DialogResult.Cancel)
                    return;

                bool pdf = Valasztas == DialogResult.No;

                SaveFileDialog MentesAblak = new SaveFileDialog
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    Title = pdf ? "Mentés PDF fájlba" : "Mentés Excel fájlba",
                    FileName = $"Eszterga_Karbantartás_{Program.PostásNév.Trim()}_{DateTime.Now:yyyyMMdd_HHmmss}",
                    Filter = pdf ? "PDF fájl (*.pdf)|*.pdf" : "Excel fájl (*.xlsx)|*.xlsx"
                };

                if (MentesAblak.ShowDialog() != DialogResult.OK)
                    return;

                string FajlNev = MentesAblak.FileName;
                Stopwatch stopper = new Stopwatch();
                stopper.Start();

                if (pdf)
                {
                    if (!FajlNev.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                        FajlNev += ".pdf";

                    PDFtábla(FajlNev, Tabla);
                    MessageBox.Show("Elkészült a PDF fájl:\n" + FajlNev, "Sikeres mentés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (!FajlNev.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                        FajlNev += ".xlsx";

                    MyE.EXCELtábla(FajlNev, Tabla, false, true);
                    MessageBox.Show("Elkészült az Excel fájl:\n" + FajlNev, "Sikeres mentés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                stopper.Stop();

                string Tipus = pdf ? "PDF" : "Excel";
                string Ido = (stopper.Elapsed.TotalSeconds).ToString("0.00");

                MessageBox.Show($"Elkészült a {Tipus} fájl:\n{FajlNev}\n\nIdő: {Ido} másodperc", "Sikeres mentés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyE.Megnyitás(FajlNev);
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
        /// Betölti a naplózott műveletek listáját a táblázatba, és elrejti a rögzítés gombot.
        /// </summary>
        private void Bttn_Napló_Listáz_Click(object sender, EventArgs e)
        {
            try
            {
                TablaNaploListazas();
                Btn_Rögzít.Visible = false;
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
        /// Amikor az üzemóra átlag számításához megadott napok száma megváltozik, újra kiszámítja és frissíti az értéket.
        /// </summary>
        private void TxtBxNapiUzemoraAtlag_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(TxtBxNapiUzemoraAtlag.Text, out int napok))
                {
                    if (napok > 100000)
                        TxtBxNapiUzemoraAtlag.Text = "100000";

                    if (napok > 0)
                        AtlagUzemoraFrissites(Math.Min(napok, 100000));
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
                if (DateTime.Today == DtmPckrElőTerv.Value)
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
        private void TxtBxÜzem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (DateTime.Today == DtmPckrElőTerv.Value)
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
        private void DtmPckrElőTerv_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (DtmPckrElőTerv.Value >= DateTime.Today)
                {
                    if (DtmPckrElőTerv.Value > DateTime.Today)
                        EloreTervezesListazasa();
                    else
                        TablaListazas();
                    Btn_Rögzít.Visible = true;
                }
                else
                {
                    MessageBox.Show("A dátum nem lehet kisebb, mint a mai dátum.", "Érvénytelen dátum", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DtmPckrElőTerv.Value = DateTime.Today;
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
        /// A megjegyzés cella szerkesztésének lezárásakor ellenőrzi, történt-e változás.
        /// Ha új megjegyzés került be, elmenti azt, ha törlés történt, törli az értéket.
        /// </summary>
        private void Tábla_CellEndEdit(object sender, DataGridViewCellEventArgs e)
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

                string ElozoMegjegyzes = (from rekord in Funkcio.Eszterga_KarbantartasFeltolt()
                                          where rekord.ID == ID
                                          select rekord.Megjegyzés)?.FirstOrDefault()?.ToStrTrim();

                if (ElozoMegjegyzes == Megjegyzes)
                {
                    MessageBox.Show("Nem történt változás a megjegyzés változtatásakor.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!string.IsNullOrEmpty(Megjegyzes))
                {
                    Adat_Eszterga_Muveletek ADAT = new Adat_Eszterga_Muveletek(Megjegyzes, ID);
                    Kez_Muvelet.Megjegyzes_Modositas(ADAT);
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
        /// A táblázat szűrési feltételeinek módosításakor újraszínezi a sorokat az aktuális szűrés után.
        /// </summary>
        private void Tábla_FilterStringChanged(object sender, Zuby.ADGV.AdvancedDataGridView.FilterEventArgs e)
        {
            Tabla.DataBindingComplete += (s, ev) => SorSzinezes();
            Tabla.Refresh();
        }

        /// <summary>
        /// A táblázat rendezésének módosításakor újraszínezi a sorokat az aktuális állapot alapján.
        /// </summary>
        private void Tábla_SortStringChanged(object sender, Zuby.ADGV.AdvancedDataGridView.SortEventArgs e)
        {
            Tabla.DataBindingComplete += (s, ev) => SorSzinezes();
            Tabla.Refresh();
        }
        #endregion
    }
}