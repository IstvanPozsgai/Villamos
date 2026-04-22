using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;

namespace Villamos
{
    public partial class Ablak_JogKiosztás : Form
    {
        readonly SQL_Kezelő_Belépés_Oldalak KézOldal = new SQL_Kezelő_Belépés_Oldalak();
        readonly SQL_Kezelő_Belépés_Gombok KézGombok = new SQL_Kezelő_Belépés_Gombok();
        readonly SQL_Kezelő_Belépés_Users KézUsers = new SQL_Kezelő_Belépés_Users();
        readonly SQL_Kezelő_Belépés_Jogosultságok KézJogosultságok = new SQL_Kezelő_Belépés_Jogosultságok();

        readonly Kezelő_Kiegészítő_Könyvtár KézSzervezet = new Kezelő_Kiegészítő_Könyvtár();
        readonly Kezelő_Behajtás_Dolgozótábla KézDolgozó = new Kezelő_Behajtás_Dolgozótábla();

        List<Adat_Bejelentkezés_Users> AdatokUsers = new List<Adat_Bejelentkezés_Users>();
        List<Adat_Belépés_Oldalak> AdatokOldal = new List<Adat_Belépés_Oldalak>();
        List<Adat_Bejelentkezés_Gombok> AdatokGombok = new List<Adat_Bejelentkezés_Gombok>();
        List<Adat_Kiegészítő_Könyvtár> AdatokSzervezet = new List<Adat_Kiegészítő_Könyvtár>();
        List<Adat_Behajtás_Dolgozótábla> AdatokDolgozó = new List<Adat_Behajtás_Dolgozótábla>();
        List<Adat_Bejelentkezés_Jogosultságok> AdatokJogosultságok = new List<Adat_Bejelentkezés_Jogosultságok>();
        List<Adat_Bejelentkezés_Jogosultságok> MásolatAdatok = new List<Adat_Bejelentkezés_Jogosultságok>();

        private A_Főoldal FőoldalPéldány;

#pragma warning disable IDE0044
        DataTable AdatTáblaALap = new DataTable();
#pragma warning restore IDE0044

        int FelhasználóFőId = -1;
        string AblakFormName = "";
        int AblakFőID = -1;
        int GombFőID = -1;

        // Változó a listbox programozott bepipálásának figyelésére
        private bool SzervezetBetoltesAlatt = false;

        public Ablak_JogKiosztás(A_Főoldal Példány)
        {
            InitializeComponent();
            FőoldalPéldány = Példány;
            LoadMenuFromMain();
            Start();
        }

        private void Ablak_JogKiosztás_Load(object sender, System.EventArgs e)
        {
        }

        private void Start()
        {
            AdatokOldal = KézOldal.Lista_Adatok().Where(a => a.Törölt == false).ToList();
            AdatokGombok = KézGombok.Lista_Adatok().Where(a => a.Törölt == false).ToList();
            AdatokSzervezet = KézSzervezet.Lista_Adatok().OrderBy(a => a.Név).ToList();
            AdatokUsers = KézUsers.Lista_Adatok().Where(a => a.Törölt == false).OrderBy(a => a.UserName).ToList();
            AdatokDolgozó = KézDolgozó.Lista_Adatok().Where(a => a.Státus == true).OrderBy(a => a.Dolgozónév).ToList();

            // EZ AZ EGYETLEN HELY, AHOL A PROGRAM INDULÁSKOR BETÖLTI AZ ADATBÁZIST A MEMÓRIÁBA
            AdatokJogosultságok = KézJogosultságok.Lista_Adatok();

            OldalFeltöltés();
            FelhasználóFeltöltés();
            GombLathatosagKezelo.Beallit(this, Program.PostásTelephely);
        }

        #region Mezők feltöltése
        private void OldalFeltöltés()
        {
            try
            {
                foreach (Adat_Belépés_Oldalak Elem in AdatokOldal)
                {
                    CmbAblak.Items.Add(Elem.MenuFelirat);
                    CmbAblakId.Items.Add(Elem.OldalId.ToString());
                }
            }
            catch (Exception ex) { HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult); }
        }

        private void GombokFeltöltése()
        {
            if (CmbAblak.Text.Trim() == "") return;
            LstGombok.Items.Clear();

            Adat_Belépés_Oldalak oldal = AdatokOldal.FirstOrDefault(a => a.Törölt == false && a.MenuFelirat == CmbAblak.Text.Trim());
            if (oldal == null) return;

            List<Adat_Bejelentkezés_Gombok> gombok = AdatokGombok.Where(a => a.Törölt == false && a.FormName == oldal.FromName).ToList();
            foreach (var item in gombok)
            {
                LstGombok.Items.Add($"{item.GombokId} = {item.GombFelirat} = {item.GombName}");
            }
        }

        private void FelhasználóFeltöltés()
        {
            try
            {
                Felhasználók.Items.Clear();
                Felhasználók.Items.Add("");
                foreach (Adat_Bejelentkezés_Users item in AdatokUsers)
                {
                    Felhasználók.Items.Add(item.UserName);
                }
            }
            catch (Exception ex) { HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult); }
        }
        #endregion

        #region Mezők kijelölése és választása
        private void CmbAblak_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CmbAblak.Text = CmbAblak.Items[CmbAblak.SelectedIndex].ToString();
            Adat_Belépés_Oldalak Ablak = AdatokOldal.FirstOrDefault(a => a.MenuFelirat == CmbAblak.Text);
            AblakFormName = Ablak.FromName;
            AblakFőID = Ablak.OldalId;
            CmbAblakId.Text = Ablak.OldalId.ToString();

            MezőkÜrítése(false);
            GombokFeltöltése();
            TáblázatListázás();
        }

        private void Felhasználók_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Felhasználók.Text = Felhasználók.Items[Felhasználók.SelectedIndex].ToString();
            Adat_Bejelentkezés_Users Felhasználó = AdatokUsers.FirstOrDefault(a => a.UserName == Felhasználók.Text);

            if (Felhasználó == null)
            {
                DolgozóNév.Text = $"<< - >>";
                FelhasználóFőId = -1;
            }
            else
            {
                FelhasználóFőId = Felhasználó.UserId;
                Adat_Behajtás_Dolgozótábla dolgozó = AdatokDolgozó.FirstOrDefault(a => a.Dolgozószám == Felhasználó.Dolgozószám);
                DolgozóNév.Text = dolgozó != null ? $"<<{dolgozó.Dolgozószám} - {dolgozó.Dolgozónév}>>" : $"<<{Felhasználó.Dolgozószám} - >>";
            }
            MezőkÜrítése();
            TáblázatListázás();
        }

        private void MezőkÜrítése(bool minden = true)
        {
            if (minden)
            {
                CmbAblak.Text = "";
                CmbAblakId.Text = "";
            }
            LstGombok.Items.Clear();
            LstChkSzervezet.Items.Clear();
        }
        #endregion

        #region Gombok 
        private void Frissít_Click(object sender, EventArgs e)
        {
            TáblázatListázás();
        }

        private void MindenGomb_Click(object sender, EventArgs e)
        {
            try
            {
                if (Felhasználók.Text.Trim() == "") throw new HibásBevittAdat("Kérem adja meg a Felhasználót!");
                if (CmbAblak.Text.Trim() == "") throw new HibásBevittAdat("Kérem válasszon egy Ablakot!");

                // Memóriába rakjuk a jogokat
                for (int j = 0; j < LstGombok.Items.Count; j++)
                {
                    string[] SzámDarabol = LstGombok.Items[j].ToStrTrim().Split('=');
                    int gombId = SzámDarabol[0].ToÉrt_Int();

                    for (int i = 0; i < LstChkSzervezet.Items.Count; i++)
                    {
                        int SzervezetId = AdatokSzervezet.FirstOrDefault(a => a.Név == LstChkSzervezet.Items[i].ToString())?.ID ?? -1;
                        bool toroltStatus = !LstChkSzervezet.GetItemChecked(i);

                        var letezo = AdatokJogosultságok.FirstOrDefault(a => a.UserId == FelhasználóFőId && a.OldalId == AblakFőID && a.GombokId == gombId && a.SzervezetId == SzervezetId);
                        if (letezo != null)
                        {
                            letezo.Törölt = toroltStatus;
                        }
                        else if (!toroltStatus)
                        {
                            AdatokJogosultságok.Add(new Adat_Bejelentkezés_Jogosultságok(FelhasználóFőId, AblakFőID, gombId, SzervezetId, false));
                        }
                    }
                }
                TáblázatListázás();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnAblakTörlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Felhasználók.Text.Trim() == "") throw new HibásBevittAdat("Kérem válasszon ki egy felhasználót!");
                if (CmbAblakId.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva ablak!");

                Adat_Bejelentkezés_Users ADAT = AdatokUsers.FirstOrDefault(a => a.UserName == Felhasználók.Text.Trim());
                int ablakId = CmbAblakId.Text.ToÉrt_Int();

                // Memóriában töröltre állítjuk az ablakhoz tartozó jogokat
                foreach (var adat in AdatokJogosultságok.Where(a => a.UserId == ADAT.UserId && a.OldalId == ablakId))
                {
                    adat.Törölt = true;
                }

                TáblázatListázás();
                MessageBox.Show("A jogosultságok törlése a memóriában megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void JogTörlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Felhasználók.Text.Trim() == "") throw new HibásBevittAdat("Kérem válasszon ki egy felhasználót!");

                Adat_Bejelentkezés_Users Egy = AdatokUsers.FirstOrDefault(a => a.UserName == Felhasználók.Text.Trim());

                // Memóriában töröljük a felhasználó összes jogát
                foreach (var adat in AdatokJogosultságok.Where(a => a.UserId == Egy.UserId))
                {
                    adat.Törölt = true;
                }

                TáblázatListázás();
                MessageBox.Show("Jogosultságok törlése a memóriában megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void LstGombok_SelectedIndexChanged(object sender, EventArgs e)
        {
            Szervezetek();
        }
        #endregion

        #region KOSÁR ÉS VÉGLEGES MENTÉS FUNKCIÓK

        private void LstChkSzervezet_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Ne reagáljon, ha épp mi töltjük fel a listát
            if (SzervezetBetoltesAlatt) return;

            if (Felhasználók.Text.Trim() == "" || CmbAblak.Text.Trim() == "" || LstGombok.SelectedItems.Count == 0) return;

            string szervezetNév = LstChkSzervezet.Items[e.Index].ToString();
            int szervezetId = AdatokSzervezet.FirstOrDefault(a => a.Név == szervezetNév)?.ID ?? -1;

            string[] gomb = LstGombok.SelectedItems[0].ToStrTrim().Split('=');
            int GombokID = AdatokGombok.FirstOrDefault(a => a.GombName == gomb[2].Trim() && a.FormName == AblakFormName)?.GombokId ?? -1;

            string megjelenitoSzoveg = $"{Felhasználók.Text} | {CmbAblak.Text} | {gomb[1].Trim()} | {szervezetNév}";

            if (e.NewValue == CheckState.Checked)
            {
                bool marBenneVan = LstJogokAdni.Items.Cast<KiosztandoJog>().Any(x => x.Megjelenites == megjelenitoSzoveg);
                if (!marBenneVan)
                {
                    Adat_Bejelentkezés_Jogosultságok ujJog = new Adat_Bejelentkezés_Jogosultságok(FelhasználóFőId, AblakFőID, GombokID, szervezetId, false);
                    LstJogokAdni.Items.Add(new KiosztandoJog { JogAdat = ujJog, Megjelenites = megjelenitoSzoveg });
                }
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                var torlendo = LstJogokAdni.Items.Cast<KiosztandoJog>().FirstOrDefault(x => x.Megjelenites == megjelenitoSzoveg);
                if (torlendo != null) LstJogokAdni.Items.Remove(torlendo);
            }
        }

        private void BtnOsszesMentese_Click(object sender, EventArgs e)
        {
            try
            {
                if (LstJogokAdni.Items.Count == 0) return;

                // Memóriába írjuk a kosár tartalmát
                foreach (KiosztandoJog item in LstJogokAdni.Items)
                {
                    var meglévő = AdatokJogosultságok.FirstOrDefault(a => a.UserId == item.JogAdat.UserId && a.OldalId == item.JogAdat.OldalId && a.GombokId == item.JogAdat.GombokId && a.SzervezetId == item.JogAdat.SzervezetId);
                    if (meglévő != null) meglévő.Törölt = false;
                    else AdatokJogosultságok.Add(item.JogAdat);
                }

                LstJogokAdni.Items.Clear();
                TáblázatListázás();
                MessageBox.Show("A jogok a memóriában kiosztásra kerültek! Ne felejtsd el elmenteni az adatbázisba!", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // AZ ÚJ VÉGLEGES MENTÉS GOMB - Ez írja ki a memóriát az adatbázisba!
        private void BtnVeglegesMentes_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                // Elküldjük a memóriát szinkronizálásra
                KézJogosultságok.Teljes_Szinkronizáció(AdatokJogosultságok);

                // Frissítjük a belső listát a biztos adatbázis állapotra
                AdatokJogosultságok = KézJogosultságok.Lista_Adatok();
                TáblázatListázás();
                Cursor = Cursors.Default;

                MessageBox.Show("A módosítások sikeresen mentve az adatbázisba!", "Sikeres mentés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Táblázat
        private void TáblázatListázás()
        {
            try
            {
                // ITT NINCS ADATBÁZIS OLVASÁS! Tisztán a memóriából (AdatokJogosultságok) dolgozik.
                Tábla.Visible = false;
                Tábla.CleanFilterAndSort();
                AlapTáblaFejléc();
                AlapTáblaTartalom();
                Tábla.DataSource = AdatTáblaALap;
                AlapTáblaOszlopSzélesség();
                Tábla.Visible = true;
                Tábla.Refresh();
            }
            catch (Exception ex) { HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult); }
        }

        private void AlapTáblaFejléc()
        {
            AdatTáblaALap.Columns.Clear();
            AdatTáblaALap.Columns.Add("Felhasználó név");
            AdatTáblaALap.Columns.Add("Ablak név");
            AdatTáblaALap.Columns.Add("Gomb név");
            AdatTáblaALap.Columns.Add("Szervezet");
            AdatTáblaALap.Columns.Add("AblakId");
            AdatTáblaALap.Columns.Add("GombId");
        }

        private void AlapTáblaTartalom()
        {
            try
            {
                AdatTáblaALap.Clear();

                // Csak a NEM TÖRÖLT adatokat vesszük figyelembe a megjelenítésnél
                List<Adat_Bejelentkezés_Jogosultságok> Adatok = AdatokJogosultságok.Where(a => !a.Törölt).ToList();

                if (Felhasználók.Text.Trim() != "")
                {
                    Adat_Bejelentkezés_Users Egy = AdatokUsers.FirstOrDefault(a => a.UserName == Felhasználók.Text.Trim());
                    if (Egy != null)
                    {
                        Adatok = Adatok.Where(a => a.UserId == Egy.UserId).ToList();
                        if (CmbAblak.Text.Trim() != "")
                        {
                            int oldalid = AdatokOldal.FirstOrDefault(a => a.MenuFelirat == CmbAblak.Text.Trim())?.OldalId ?? -1;
                            Adatok = Adatok.Where(a => a.OldalId == oldalid).ToList();
                        }
                    }
                }

                foreach (Adat_Bejelentkezés_Jogosultságok rekord in Adatok)
                {
                    DataRow Soradat = AdatTáblaALap.NewRow();
                    Soradat["Felhasználó név"] = AdatokUsers.FirstOrDefault(a => a.UserId == rekord.UserId)?.UserName ?? "<<Nincs felhasználó>>";
                    Soradat["Ablak név"] = AdatokOldal.FirstOrDefault(a => a.OldalId == rekord.OldalId)?.MenuFelirat ?? "<<Nincs Ablak>>";

                    string gombnév = "<<Nincs Gomb>>";
                    Adat_Bejelentkezés_Gombok EgyGomb = AdatokGombok.FirstOrDefault(a => a.GombokId == rekord.GombokId);
                    if (EgyGomb != null) gombnév = $"{EgyGomb.GombFelirat} = {EgyGomb.GombName}";

                    Soradat["Gomb név"] = gombnév;
                    Soradat["Szervezet"] = AdatokSzervezet.FirstOrDefault(a => a.ID == rekord.SzervezetId)?.Név ?? "<<Nincs Szervezet>>";
                    Soradat["AblakId"] = rekord.OldalId;
                    Soradat["GombId"] = rekord.GombokId;

                    AdatTáblaALap.Rows.Add(Soradat);
                }
            }
            catch (Exception ex) { HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult); }
        }

        private void AlapTáblaOszlopSzélesség()
        {
            Tábla.Columns["Felhasználó név"].Width = 180;
            Tábla.Columns["Ablak név"].Width = 250;
            Tábla.Columns["Gomb név"].Width = 600;
            Tábla.Columns["Szervezet"].Width = 500;
            Tábla.Columns["AblakId"].Width = 110;
            Tábla.Columns["GombId"].Width = 110;
        }

        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Felhasználók.Text = Tábla.Rows[e.RowIndex].Cells[0].Value.ToStrTrim();
            string[] gombnév = Tábla.Rows[e.RowIndex].Cells[2].Value.ToStrTrim().Split('=');
            CmbAblak.Text = Tábla.Rows[e.RowIndex].Cells[1].Value.ToStrTrim();
            CmbAblakId.Text = Tábla.Rows[e.RowIndex].Cells[4].Value.ToStrTrim();

            Adat_Belépés_Oldalak Ablak = AdatokOldal.FirstOrDefault(a => a.MenuFelirat == CmbAblak.Text);
            AblakFormName = Ablak.FromName;
            AblakFőID = Ablak.OldalId;

            GombokFeltöltése();
            GombKijelöl(gombnév[1].Trim());
            Szervezetek();
        }

        private void GombKijelöl(string gombnév)
        {
            for (int i = 0; i < LstGombok.Items.Count; i++)
            {
                string[] gomb = LstGombok.Items[i].ToStrTrim().Split('=');
                if (gomb[2].Trim() == gombnév) LstGombok.SelectedIndex = i;
            }
        }
        #endregion

        #region Szervezetek beállítása
        private void Szervezetek()
        {
            try
            {
                // JELZÜNK A RENDSZERNEK, HOGY MOST MI PIPÁLUNK, NEM A FELHASZNÁLÓ
                SzervezetBetoltesAlatt = true;

                LstChkSzervezet.Items.Clear();
                if (LstGombok.Items.Count == 0) return;

                string[] Darabol = LstGombok.SelectedItems[0].ToStrTrim().Split('=');
                Adat_Bejelentkezés_Gombok Gomb = AdatokGombok.FirstOrDefault(a => a.GombName == Darabol[2].Trim() && a.FormName == AblakFormName);
                GombFőID = Gomb?.GombokId ?? -1;

                if (Gomb == null) return;

                string[] Gomb_Szervezetek_darabolva = Gomb.Szervezet.Split(';');
                string[] Jogadó_Szervezetek_darabolva = Program.Postás_Felhasználó.Szervezetek.Split(';');

                foreach (string szervezet in Gomb_Szervezetek_darabolva)
                {
                    if (Jogadó_Szervezetek_darabolva.Contains(szervezet.Trim()))
                    {
                        LstChkSzervezet.Items.Add(szervezet.Trim());
                        int szervezetID = AdatokSzervezet.FirstOrDefault(b => b.Név == szervezet.Trim())?.ID ?? -1;

                        // A memóriából nézzük a jogot
                        Adat_Bejelentkezés_Jogosultságok Jog = AdatokJogosultságok.FirstOrDefault(a =>
                            a.UserId == FelhasználóFőId && a.OldalId == AblakFőID && a.GombokId == GombFőID && a.SzervezetId == szervezetID);

                        bool chkState = (Jog != null && !Jog.Törölt);
                        LstChkSzervezet.SetItemChecked(LstChkSzervezet.Items.IndexOf(szervezet.Trim()), chkState);
                    }
                }
            }
            finally
            {
                SzervezetBetoltesAlatt = false;
            }
        }

        private void SzervezetMinden_Click(object sender, EventArgs e) => SzervezetJelöl(true);
        private void SzervezetSemmi_Click(object sender, EventArgs e) => SzervezetJelöl(false);

        private void SzervezetJelöl(bool kell)
        {
            for (int i = 0; i < LstChkSzervezet.Items.Count; i++)
                LstChkSzervezet.SetItemChecked(i, kell);
        }
        #endregion

        #region Jogosultság másolása
        private void BtnMásol_Click(object sender, EventArgs e)
        {
            try
            {
                MásolatAdatok.Clear();
                Másolat.Text = $"<< >>";

                if (Felhasználók.Text.Trim() == "") return;
                Másolat.Text = $"Másolás: {Felhasználók.Text}";

                Adat_Bejelentkezés_Users Felhasználó = AdatokUsers.FirstOrDefault(a => a.UserName == Felhasználók.Text);

                // Memóriából másolunk
                MásolatAdatok = AdatokJogosultságok.Where(a => a.UserId == Felhasználó.UserId && !a.Törölt).ToList();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnBeilleszt_Click(object sender, EventArgs e)
        {
            try
            {
                if (Másolat.Text.Trim() == "") throw new HibásBevittAdat("Nincs másolani kívánt felhasználó!");
                if (MásolatAdatok.Count < 1) throw new HibásBevittAdat("Nincs másolani kívánt adat!");

                Adat_Bejelentkezés_Users Felhasználó = AdatokUsers.FirstOrDefault(a => a.UserName == Felhasználók.Text);

                // Memóriába illesztünk be
                foreach (Adat_Bejelentkezés_Jogosultságok adat in MásolatAdatok)
                {
                    var letezo = AdatokJogosultságok.FirstOrDefault(a => a.UserId == Felhasználó.UserId && a.OldalId == adat.OldalId && a.GombokId == adat.GombokId && a.SzervezetId == adat.SzervezetId);
                    if (letezo != null)
                    {
                        letezo.Törölt = false;
                    }
                    else
                    {
                        AdatokJogosultságok.Add(new Adat_Bejelentkezés_Jogosultságok(Felhasználó.UserId, adat.OldalId, adat.GombokId, adat.SzervezetId, false));
                    }
                }
                TáblázatListázás();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        #endregion

        #region MenüszerkezetFába írása és egyéb segédfüggvények
        private void CmbAblakId_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CmbAblakId.Text = CmbAblakId.Items[CmbAblakId.SelectedIndex].ToString();
            KiválasztottAblak();
        }

        private void KiválasztottAblak()
        {
            Adat_Belépés_Oldalak Ablak = AdatokOldal.FirstOrDefault(a => a.OldalId == CmbAblakId.Text.ToÉrt_Int());
            AblakFormName = Ablak.FromName;
            CmbAblak.Text = Ablak.MenuFelirat;
            AblakFőID = Ablak.OldalId;
            MezőkÜrítése(false);
            GombokFeltöltése();
            TáblázatListázás();
        }

        private void LoadMenuFromMain()
        {
            MenűFa.Nodes.Clear();
            foreach (ToolStripMenuItem item in FőoldalPéldány.MainMenuStrip.Items)
            {
                AddNodesRecursive(item, MenűFa.Nodes);
            }
        }

        private void CopyMenuToTree(ToolStripItem menuItem, TreeNodeCollection treeNodes)
        {
            if (menuItem is ToolStripMenuItem menuEntry)
            {
                TreeNode newNode = new TreeNode(menuEntry.Text.Replace("&", ""));
                newNode.Tag = menuEntry.Name;
                treeNodes.Add(newNode);

                foreach (ToolStripItem subItem in menuEntry.DropDownItems)
                {
                    CopyMenuToTree(subItem, newNode.Nodes);
                }
            }
        }

        private void AddNodesRecursive(ToolStripItem menuItem, TreeNodeCollection treeNodes)
        {
            if (menuItem is ToolStripMenuItem menuEntry)
            {
                TreeNode newNode = new TreeNode(menuEntry.Text.Replace("&", ""));
                newNode.Tag = menuEntry.Name;
                treeNodes.Add(newNode);

                foreach (ToolStripItem subItem in menuEntry.DropDownItems)
                {
                    AddNodesRecursive(subItem, newNode.Nodes);
                }
            }
        }

        private void MenűFa_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string KijelöltSzöveg = e.Node.Text.Trim();
            if (KijelöltSzöveg.Trim() == "") return;
            Adat_Belépés_Oldalak Elem = AdatokOldal.FirstOrDefault(a => a.MenuFelirat.Trim() == KijelöltSzöveg);
            CmbAblakId.Text = "";
            if (Elem != null)
            {
                CmbAblakId.Text = Elem.OldalId.ToString();
                KiválasztottAblak();
            }
        }

        public class KiosztandoJog
        {
            public Adat_Bejelentkezés_Jogosultságok JogAdat { get; set; }
            public string Megjelenites { get; set; }
            public override string ToString() => Megjelenites;
        }
        #endregion

        private void BtnCSVBeolvasas_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    // Csak CSV fájlokat engedünk kiválasztani
                    ofd.Filter = "CSV fájlok (*.csv)|*.csv|Minden fájl (*.*)|*.*";
                    ofd.Title = "Jogosultságok beolvasása CSV-ből";

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        // Beolvassuk a fájl összes sorát (UTF-8 kódolással, hogy az ékezetek is jók legyenek)
                        string[] sorok = System.IO.File.ReadAllLines(ofd.FileName, System.Text.Encoding.UTF8);

                        int sikeres = 0;
                        int hibas = 0;

                        foreach (string sor in sorok)
                        {
                            // Üres sorokat átugorjuk
                            if (string.IsNullOrWhiteSpace(sor)) continue;

                            // Ha az első sor a fejléc lenne (felhasznalo;gombid;szervezet), azt is átugorjuk
                            if (sor.ToLower().StartsWith("felhasznalo")) continue;

                            string[] adatok = sor.Split(';');
                            if (adatok.Length < 3)
                            {
                                hibas++;
                                continue; // Ha nincs meg a 3 adat (felhasználó;gomb;szervezet), ugrunk a köv sorra
                            }

                            string userName = adatok[0].Trim();
                            string gombIdStr = adatok[1].Trim();
                            string szervezetNev = adatok[2].Trim();

                            // 1. Felhasználó azonosítása a memóriából
                            var user = AdatokUsers.FirstOrDefault(a => a.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
                            if (user == null) { hibas++; continue; } // Nincs ilyen user

                            // 2. Gomb azonosítása
                            if (!int.TryParse(gombIdStr, out int gombId)) { hibas++; continue; }
                            var gomb = AdatokGombok.FirstOrDefault(a => a.GombokId == gombId);
                            if (gomb == null) { hibas++; continue; } // Nincs ilyen gomb

                            // 3. Oldal (Ablak) azonosítása a gomb alapján
                            // A gombhoz tartozó FormName alapján megkeressük az Oldalt (aminél ez FromName néven fut)
                            var oldal = AdatokOldal.FirstOrDefault(a => a.FromName == gomb.FormName);
                            if (oldal == null) { hibas++; continue; }

                            // 4. Szervezet azonosítása
                            var szervezet = AdatokSzervezet.FirstOrDefault(a => a.Név.Equals(szervezetNev, StringComparison.OrdinalIgnoreCase));
                            if (szervezet == null) { hibas++; continue; } // Nincs ilyen szervezet

                            // HA MINDEN ADAT MEGVAN, BETESSZÜK A MEMÓRIÁBA
                            var letezo = AdatokJogosultságok.FirstOrDefault(a =>
                                a.UserId == user.UserId &&
                                a.OldalId == oldal.OldalId &&
                                a.GombokId == gomb.GombokId &&
                                a.SzervezetId == szervezet.ID);

                            if (letezo != null)
                            {
                                // Ha már létezett (esetleg töröltként), akkor aktiváljuk
                                letezo.Törölt = false;
                            }
                            else
                            {
                                // Ha teljesen új jog, felvesszük a listába
                                AdatokJogosultságok.Add(new Adat_Bejelentkezés_Jogosultságok(
                                    user.UserId,
                                    oldal.OldalId,
                                    gomb.GombokId,
                                    szervezet.ID,
                                    false));
                            }
                            sikeres++;
                        }

                        // Frissítjük a képernyőt, hogy lássuk a beolvasott adatokat
                        TáblázatListázás();

                        MessageBox.Show($"A CSV beolvasása befejeződött!\n\n" +
                                        $"Sikeresen feldolgozott sorok: {sikeres}\n" +
                                        $"Hibás vagy kihagyott sorok: {hibas}\n\n" +
                                        $"A jogok a memóriában vannak. Ne felejtsd el megnyomni a 'Végleges mentés' gombot!",
                                        "Beolvasás kész", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show($"Hiba a CSV feldolgozása közben:\n{ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}