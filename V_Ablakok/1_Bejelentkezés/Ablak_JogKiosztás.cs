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

#pragma warning disable IDE0044
        DataTable AdatTáblaALap = new DataTable();
#pragma warning restore IDE0044

        //Kiválasztott felhasználó id-je
        int FelhasználóFőId = -1;
        string AblakFormName = "";
        int AblakFőID = -1;
        int GombFőID = -1;

        public Ablak_JogKiosztás()
        {
            InitializeComponent();
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
            AdatokUsers = KézUsers.Lista_Adatok();
            AdatokUsers = (from a in AdatokUsers
                           where a.Törölt == false
                           orderby a.UserName
                           select a).ToList();
            AdatokDolgozó = KézDolgozó.Lista_Adatok().Where(a => a.Státus == true).OrderBy(a => a.Dolgozónév).ToList();
            AdatokJogosultságok = KézJogosultságok.Lista_Adatok();
            OldalFeltöltés();
            FelhasználóFeltöltés();
            GombLathatosagKezelo.Beallit(this, "Főmérnökség");
        }


        #region Mezők feltöltése

        /// <summary>
        /// Oldalak feltöltése a comboxba.
        /// </summary>
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
        /// Ablak gombok feltöltése a comboxba.
        /// </summary>
        private void GombokFeltöltése()
        {
            if (CmbAblak.Text.Trim() == "") return;
            ChkGombok.Items.Clear();

            Adat_Belépés_Oldalak oldal = (from a in AdatokOldal
                                          where a.Törölt == false
                                          && a.MenuFelirat == CmbAblak.Text.Trim()
                                          orderby a.MenuFelirat
                                          select a).FirstOrDefault();
            if (oldal == null) return;
            List<Adat_Bejelentkezés_Gombok> gombok = (from a in AdatokGombok
                                                      where a.Törölt == false
                                                      && a.FormName == oldal.FromName
                                                      select a).ToList();
            if (gombok == null) return;
            for (int i = 0; i < gombok.Count; i++)
            {
                Adat_Bejelentkezés_Gombok item = gombok[i];
                string felirat = $"{item.GombokId} = {item.GombFelirat} = {item.GombName}";
                ChkGombok.Items.Add(felirat);
            }
        }



        /// <summary>
        /// Feltöltjük a felhasználókat a comboxba.
        /// </summary>
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


        #region Mezők kijelölése és választása
        private void CmbAblak_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
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


        private void Felhasználók_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
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
                    if (dolgozó != null)
                        DolgozóNév.Text = $"<<{dolgozó.Dolgozószám} - {dolgozó.Dolgozónév}>>";
                    else
                        DolgozóNév.Text = $"<<{Felhasználó.Dolgozószám} - >>";
                }
                MezőkÜrítése();
                TáblázatListázás();
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

        private void MezőkÜrítése(bool minden = true)
        {
            if (minden)
            {
                CmbAblak.Text = "";
                CmbAblakId.Text = "";

            }


            ChkGombok.Items.Clear();
            LstChkSzervezet.Items.Clear();
        }
        #endregion


        #region Gombok 
        /// <summary>
        /// Kilistázzuk a kiválaszo felhasználóhoz tartozó jogosultságokat.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frissít_Click(object sender, EventArgs e)
        {
            TáblázatListázás();
        }


        /// <summary>
        /// Rögzítjük a kiválasztott felhasználóhoz az ablak, gombokat és szervezeteket jogosultásgát.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Felhasználók.Text.Trim() == "") throw new HibásBevittAdat("Kérem adja meg a Felhasználót!");
                if (CmbAblak.Text.Trim() == "") throw new HibásBevittAdat("Kérem válasszon egy Ablakot!");
                if (ChkGombok.SelectedItems.Count == 0) throw new HibásBevittAdat("Kérem válasszon legalább egy Gombot gombot!");
                //Ha van kiválasztott gomb akkor azt rögzítjük
                string[] gomb = ChkGombok.CheckedItems[0].ToStrTrim().Split('=');
                int GombokID = AdatokGombok.FirstOrDefault(a => a.GombName == gomb[2].Trim() && a.FormName == AblakFormName)?.GombokId ?? -1;

                List<Adat_Bejelentkezés_Jogosultságok> Adatok = new List<Adat_Bejelentkezés_Jogosultságok>();
                for (int i = 0; i < LstChkSzervezet.Items.Count; i++)
                {
                    int SzervezetId = AdatokSzervezet.FirstOrDefault(a => a.Név == LstChkSzervezet.Items[i].ToString())?.ID ?? -1;
                    Adat_Bejelentkezés_Jogosultságok adat = new Adat_Bejelentkezés_Jogosultságok
                    (
                        FelhasználóFőId,
                        AblakFőID,
                        GombokID,
                        SzervezetId,
                        !LstChkSzervezet.GetItemChecked(i)
                    );
                    Adatok.Add(adat);
                }
                if (Adatok.Count > 0) KézJogosultságok.Döntés(Adatok);
                TáblázatListázás();
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

        private void MindenGomb_Click(object sender, EventArgs e)
        {
            try
            {
                if (Felhasználók.Text.Trim() == "") throw new HibásBevittAdat("Kérem adja meg a Felhasználót!");
                if (CmbAblak.Text.Trim() == "") throw new HibásBevittAdat("Kérem válasszon egy Ablakot!");

                List<Adat_Bejelentkezés_Jogosultságok> Adatok = new List<Adat_Bejelentkezés_Jogosultságok>();
                for (int j = 0; j < ChkGombok.Items.Count; j++)
                {
                    string[] SzámDarabol = ChkGombok.CheckedItems[0].ToStrTrim().Split('-');
                    for (int i = 0; i < LstChkSzervezet.Items.Count; i++)
                    {
                        int SzervezetId = AdatokSzervezet.FirstOrDefault(a => a.Név == LstChkSzervezet.Items[i].ToString())?.ID ?? -1;
                        Adat_Bejelentkezés_Jogosultságok adat = new Adat_Bejelentkezés_Jogosultságok
                        (
                            FelhasználóFőId,
                            AblakFőID,
                            SzámDarabol[0].ToÉrt_Int(),
                            SzervezetId,
                            !LstChkSzervezet.GetItemChecked(i)
                        );
                        Adatok.Add(adat);
                    }
                }
                if (Adatok.Count > 0) KézJogosultságok.Döntés(Adatok);
                TáblázatListázás();
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



        private void Btn_MindenMasol_Click(object sender, EventArgs e)
        {

            try
            {
                if (Felhasználók.Text.Trim() == "") throw new HibásBevittAdat("Kérem válasszon ki egy felhasználót!");

                Kezelő_Belépés_MindenMásol kezelo = new Kezelő_Belépés_MindenMásol();
                kezelo.Másolás(Program.PostásTelephely, Felhasználók.Text);
                TáblázatListázás();
                MessageBox.Show("Másolás megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        #region Táblázat
        private void TáblázatListázás()
        {
            try
            {
                AdatokJogosultságok = KézJogosultságok.Lista_Adatok();
                Tábla.Visible = false;
                Tábla.CleanFilterAndSort();
                AlapTáblaFejléc();
                AlapTáblaTartalom();
                Tábla.DataSource = AdatTáblaALap;
                AlapTáblaOszlopSzélesség();
                Tábla.Visible = true;
                Tábla.Refresh();
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

        private void AlapTáblaFejléc()
        {
            try
            {
                AdatTáblaALap.Columns.Clear();
                AdatTáblaALap.Columns.Add("Felhasználó név");
                AdatTáblaALap.Columns.Add("Ablak név");
                AdatTáblaALap.Columns.Add("Gomb név");
                AdatTáblaALap.Columns.Add("Szervezet");
                AdatTáblaALap.Columns.Add("AblakId");
                AdatTáblaALap.Columns.Add("GombId");
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

        private void AlapTáblaTartalom()
        {
            try
            {
                AdatTáblaALap.Clear();
                AdatokJogosultságok = KézJogosultságok.Lista_Adatok();
                //ha nincs kiválasztva akkor az összes adatot írjuk ki
                List<Adat_Bejelentkezés_Jogosultságok> Adatok = AdatokJogosultságok;
                if (Felhasználók.Text.Trim() != "")
                {
                    //csak a kiválasztott felhasználó adatait írjuk ki
                    Adat_Bejelentkezés_Users Egy = (from a in AdatokUsers
                                                    where a.UserName == Felhasználók.Text.Trim()
                                                    select a).FirstOrDefault();
                    Adatok = AdatokJogosultságok.Where(a => a.UserId == Egy.UserId).ToList();
                    if (CmbAblak.Text.Trim() != "")
                    {
                        int oldalid = AdatokOldal.FirstOrDefault(a => a.MenuFelirat == CmbAblak.Text.Trim())?.OldalId ?? -1;
                        Adatok = Adatok.Where(a => a.OldalId == oldalid && a.Törölt).ToList();
                    }
                }
                foreach (Adat_Bejelentkezés_Jogosultságok rekord in Adatok)
                {
                    DataRow Soradat = AdatTáblaALap.NewRow();
                    Soradat["Felhasználó név"] = AdatokUsers.FirstOrDefault(a => a.UserId == rekord.UserId)?.UserName ?? "<<Nincs felhasználó>>";
                    Soradat["Ablak név"] = AdatokOldal.FirstOrDefault(a => a.OldalId == rekord.OldalId)?.MenuFelirat ?? "<<Nincs Ablak>>";
                    string gombnév = "<<Nincs Gomb>>";
                    Adat_Bejelentkezés_Gombok EgyGomb = AdatokGombok.FirstOrDefault(a => a.GombokId == rekord.GombokId);
                    if (EgyGomb != null)
                        gombnév = $"{EgyGomb.GombFelirat} = {EgyGomb.GombName}";
                    Soradat["Gomb név"] = gombnév;
                    string szervezet = "<<Nincs Szervezet>>";
                    szervezet = AdatokSzervezet.FirstOrDefault(a => a.ID == rekord.SzervezetId)?.Név ?? "<<Nincs Szervezet>>";
                    Soradat["Szervezet"] = szervezet;
                    Soradat["AblakId"] = rekord.OldalId;
                    Soradat["GombId"] = rekord.GombokId;
                    AdatTáblaALap.Rows.Add(Soradat);
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
            GombKijelöl(gombnév[1]);
            Szervezetek();


        }

        private void GombKijelöl(string gombnév)
        {
            try
            {
                if (ChkGombok.Items.Count < 1) return;
                for (int i = 0; i < ChkGombok.Items.Count; i++)
                {
                    string[] gomb = ChkGombok.Items[i].ToStrTrim().Split('=');
                    if (gomb[2] == gombnév) ChkGombok.SetItemChecked(i, true);
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

        private void ChkGombok_SelectedIndexChanged(object sender, EventArgs e)
        {
            Szervezetek();
        }


        #region Szervezet
        //A gombhoz tartozó szervezetek betöltése
        private void Szervezetek()
        {
            try
            {
                LstChkSzervezet.Items.Clear();
                if (ChkGombok.Items.Count == 0) return;
                string[] Darabol = ChkGombok.CheckedItems[0].ToStrTrim().Split('=');
                Adat_Bejelentkezés_Gombok Gomb = AdatokGombok.FirstOrDefault(a => a.GombName == Darabol[2].Trim() && a.FormName == AblakFormName);
                GombFőID = Gomb?.GombokId ?? -1;

                if (Gomb == null) return;
                string[] Gomb_Szervezetek_darabolva = Gomb.Szervezet.Split(';');//A gombhoz tartozó szervezetek tömbben
                string[] Jogadó_Szervezetek_darabolva = Program.Postás_Felhasználó.Szervezetek.Split(';');//Jogosultságot adó jogosultsága
                //A teljes lista csorbítása a beállító jogosultságaival
                foreach (string szervezet in Gomb_Szervezetek_darabolva)
                {
                    // A jogosztó adhat jogot
                    if (Jogadó_Szervezetek_darabolva.Contains(szervezet))
                    {
                        //Csak azokat a szervezeteket írjuk ki amelyek a beállító jogosultságai között is szerepelnek
                        LstChkSzervezet.Items.Add(szervezet.Trim());
                        //Jogosoultságok kiírása a meglévő alapján
                        int UserId = FelhasználóFőId;
                        Adat_Bejelentkezés_Jogosultságok Jog = (from a in AdatokJogosultságok
                                                                where a.UserId == FelhasználóFőId
                                                                && a.OldalId == AblakFőID
                                                                && a.GombokId == GombFőID
                                                                && a.SzervezetId == AdatokSzervezet.FirstOrDefault(b => b.Név == szervezet.Trim())?.ID
                                                                && !a.Törölt 
                                                                select a).FirstOrDefault();
                        if (Jog != null && !Jog.Törölt)
                            LstChkSzervezet.SetItemChecked(LstChkSzervezet.Items.IndexOf(szervezet.Trim()), true);
                        else
                            LstChkSzervezet.SetItemChecked(LstChkSzervezet.Items.IndexOf(szervezet.Trim()), false);
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

        private void SzervezetMinden_Click(object sender, EventArgs e)
        {
            SzervezetJelöl(true);
        }

        private void SzervezetSemmi_Click(object sender, EventArgs e)
        {
            SzervezetJelöl(false);
        }

        private void SzervezetJelöl(bool kell)
        {
            for (int i = 0; i < LstChkSzervezet.Items.Count; i++)
                LstChkSzervezet.SetItemChecked(i, kell);
        }

        #endregion

        private void JogTörlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Felhasználók.Text.Trim() == "") throw new HibásBevittAdat("Kérem válasszon ki egy felhasználót!");
                //csak a kiválasztott felhasználó jogain megyünk végig
                Adat_Bejelentkezés_Users Egy = (from a in AdatokUsers
                                                where a.UserName == Felhasználók.Text.Trim()
                                                select a).FirstOrDefault();
                List<Adat_Bejelentkezés_Jogosultságok> Adatok = AdatokJogosultságok.Where(a => a.UserId == Egy.UserId).ToList();

                List<Adat_Bejelentkezés_Jogosultságok> AdatokKüldés = new List<Adat_Bejelentkezés_Jogosultságok>();
                foreach (Adat_Bejelentkezés_Jogosultságok adat in Adatok)
                {
                    Adat_Bejelentkezés_Jogosultságok Törlés = new Adat_Bejelentkezés_Jogosultságok
                    (
                        adat.UserId,
                        adat.OldalId,
                        adat.GombokId,
                        adat.SzervezetId,
                        true
                    );
                    AdatokKüldés.Add(Törlés);
                }

                KézJogosultságok.Döntés(AdatokKüldés);
                TáblázatListázás();
                MessageBox.Show("Jogosultságok törlése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void CmbAblakId_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CmbAblakId.Text = CmbAblakId.Items[CmbAblakId.SelectedIndex].ToString();
            Adat_Belépés_Oldalak Ablak = AdatokOldal.FirstOrDefault(a => a.OldalId == CmbAblakId.Text.ToÉrt_Int());
            AblakFormName = Ablak.FromName;
            CmbAblak.Text = Ablak.MenuFelirat;
            AblakFőID = Ablak.OldalId;
            MezőkÜrítése(false);
            GombokFeltöltése();
            TáblázatListázás();
        }


    }
}