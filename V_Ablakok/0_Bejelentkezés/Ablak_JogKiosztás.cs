using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;


namespace Villamos
{

    public partial class Ablak_JogKiosztás : Form
    {
        readonly Kezelő_Oldalok KézOldal = new Kezelő_Oldalok();
        readonly Kezelő_Gombok KézGombok = new Kezelő_Gombok();
        readonly Kezelő_Kiegészítő_Könyvtár KézSzervezet = new Kezelő_Kiegészítő_Könyvtár();
        readonly Kezelő_Users KézUsers = new Kezelő_Users();
        readonly Kezelő_Behajtás_Dolgozótábla KézDolgozó = new Kezelő_Behajtás_Dolgozótábla();
        readonly Kezelő_Jogosultságok KézJogosultságok = new Kezelő_Jogosultságok();

        List<Adat_Users> AdatokUsers = new List<Adat_Users>();
        List<Adat_Oldalak> AdatokOldal = new List<Adat_Oldalak>();
        List<Adat_Gombok> AdatokGombok = new List<Adat_Gombok>();
        List<Adat_Kiegészítő_Könyvtár> AdatokSzervezet = new List<Adat_Kiegészítő_Könyvtár>();
        List<Adat_Behajtás_Dolgozótábla> AdatokDolgozó = new List<Adat_Behajtás_Dolgozótábla>();
        List<Adat_Jogosultságok> AdatokJogosultságok = new List<Adat_Jogosultságok>();

#pragma warning disable IDE0044
        DataTable AdatTáblaALap = new DataTable();
#pragma warning restore IDE0044

        //Kiválasztott felhasználó id-je
        int FelhasználóId = -1;
        string AblakForm = "";
        int AblakID = -1;

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
            AdatokUsers = KézUsers.Lista_Adatok().Where(a => a.Törölt == false).ToList();
            AdatokDolgozó = KézDolgozó.Lista_Adatok().Where(a => a.Státus == false).OrderBy(a => a.Dolgozónév).ToList();
            AdatokJogosultságok = KézJogosultságok.Lista_Adatok();
            OldalFeltöltés();
            FelhasználóFeltöltés();
            //   GombLathatosagKezelo.Beallit(this);
        }


        #region Mezők feltöltése

        /// <summary>
        /// Oldalak feltöltése a comboxba.
        /// </summary>
        private void OldalFeltöltés()
        {
            try
            {
                foreach (Adat_Oldalak Elem in AdatokOldal)
                    CmbAblak.Items.Add(Elem.MenuFelirat);
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
            CmbGombok.Items.Clear();
            Adat_Oldalak oldal = (from a in AdatokOldal
                                  where a.Törölt == false
                                  && a.MenuFelirat == CmbAblak.Text.Trim()
                                  orderby a.MenuFelirat
                                  select a).FirstOrDefault();
            if (oldal == null) return;
            List<Adat_Gombok> gombok = (from a in AdatokGombok
                                        where a.Törölt == false
                                        && a.FromName == oldal.FromName
                                        select a).ToList();
            if (gombok == null) return;
            for (int i = 0; i < gombok.Count; i++)
            {
                Adat_Gombok item = gombok[i];
                string felirat = $"{item.GombFelirat} = {item.GombName}";
                CmbGombok.Items.Add(felirat);
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
                foreach (Adat_Users item in AdatokUsers)
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
                Adat_Oldalak Ablak = AdatokOldal.FirstOrDefault(a => a.MenuFelirat == CmbAblak.Text);
                AblakForm = Ablak.FromName;
                AblakID = Ablak.OldalId;
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
                Adat_Users Felhasználó = AdatokUsers.FirstOrDefault(a => a.UserName == Felhasználók.Text);
                if (Felhasználó == null)
                {
                    DolgozóNév.Text = $"<< - >>";
                    FelhasználóId = -1;
                }

                else
                {
                    FelhasználóId = Felhasználó.UserId;
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

        private void MezőkÜrítése()
        {
            CmbAblak.Text = "";
            CmbGombok.Text = "";
            CmbGombok.Items.Clear();
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
                if (CmbGombok.Text.Trim() == "") throw new HibásBevittAdat("Kérem válasszon legalább egy Gombot gombot!");
                //Ha van kiválasztott gomb akkor azt rögzítjük
                string[] gomb = CmbGombok.Text.Trim().Split('=');
                int GombokID = AdatokGombok.FirstOrDefault(a => a.GombName == gomb[1].Trim() && a.FromName == AblakForm)?.GombokId ?? -1;

                if (LstChkSzervezet.CheckedItems.Count == 0)
                {
                    Adat_Jogosultságok adat = new Adat_Jogosultságok
                          (
                              FelhasználóId,
                              AblakID,
                              GombokID,
                              -1,
                              true
                          );
                    KézJogosultságok.Törlés(adat);
                }
                else
                {

                    if (LstChkSzervezet.CheckedItems.Count == 0) throw new HibásBevittAdat("Kérem válasszon legalább egy Szervezetet!");
                    List<Adat_Jogosultságok> Adatok = new List<Adat_Jogosultságok>();

                    foreach (string Szervezet in LstChkSzervezet.CheckedItems)
                    {
                        int SzervezetId = AdatokSzervezet.FirstOrDefault(a => a.Név == Szervezet)?.ID ?? -1;
                        Adat_Jogosultságok adat = new Adat_Jogosultságok
                        (
                            FelhasználóId,
                            AblakID,
                            GombokID,
                            SzervezetId,
                            false
                        );
                        Adatok.Add(adat);
                    }
                    if (Adatok.Count > 0) KézJogosultságok.Rögzítés(Adatok);
                }
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
                AdatTáblaALap.Columns.Add("Törölt");
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
                List<Adat_Jogosultságok> Adatok = AdatokJogosultságok;
                if (Felhasználók.Text.Trim() != "")
                {
                    //csak a kiválasztott felhasználó adatait írjuk ki
                    Adat_Users Egy = (from a in AdatokUsers
                                      where a.UserName == Felhasználók.Text.Trim()
                                      select a).FirstOrDefault();
                    Adatok = AdatokJogosultságok.Where(a => a.UserId == Egy.UserId).ToList();
                    if (CmbAblak.Text.Trim() != "")
                    {
                        int oldalid = AdatokOldal.FirstOrDefault(a => a.MenuFelirat == CmbAblak.Text.Trim())?.OldalId ?? -1;
                        Adatok = Adatok.Where(a => a.OldalId == oldalid).ToList();
                    }
                }
                foreach (Adat_Jogosultságok rekord in Adatok)
                {
                    DataRow Soradat = AdatTáblaALap.NewRow();
                    Soradat["Felhasználó név"] = AdatokUsers.FirstOrDefault(a => a.UserId == rekord.UserId)?.UserName ?? "<<Nincs felhasználó>>";
                    Soradat["Ablak név"] = AdatokOldal.FirstOrDefault(a => a.OldalId == rekord.OldalId)?.MenuFelirat ?? "<<Nincs Ablak>>";
                    string gombnév = "<<Nincs Gomb>>";
                    Adat_Gombok EgyGomb = AdatokGombok.FirstOrDefault(a => a.GombokId == rekord.GombokId);
                    if (EgyGomb != null)
                        gombnév = $"{EgyGomb.GombFelirat} = {EgyGomb.GombName}";
                    Soradat["Gomb név"] = gombnév;
                    string szervezet = "<<Nincs Szervezet>>";
                    szervezet = AdatokSzervezet.FirstOrDefault(a => a.ID == rekord.SzervezetId)?.Név ?? "<<Nincs Szervezet>>";
                    Soradat["Szervezet"] = szervezet;
                    Soradat["Törölt"] = rekord.Törölt ? "Igen" : "Nem";
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
            Tábla.Columns["Törölt"].Width = 110;
        }


        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Felhasználók.Text = Tábla.Rows[e.RowIndex].Cells[0].Value.ToStrTrim();
            CmbGombok.Text = Tábla.Rows[e.RowIndex].Cells[2].Value.ToStrTrim();

            CmbAblak.Text = Tábla.Rows[e.RowIndex].Cells[1].Value.ToStrTrim();
            Adat_Oldalak Ablak = AdatokOldal.FirstOrDefault(a => a.MenuFelirat == CmbAblak.Text);
            AblakForm = Ablak.FromName;
            AblakID = Ablak.OldalId;
            SzervezetFeltöltés();
        }
        #endregion

        private void CmbGombok_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                CmbGombok.Text = CmbGombok.Items[CmbGombok.SelectedIndex].ToString();
                SzervezetFeltöltés();
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

        #region Szervezet
        /// <summary>
        /// Feltöltjük, hogy melyik szervezetnek engedjük meg a módosítást
        /// </summary>
        private void SzervezetFeltöltés()
        {
            try
            {
                LstChkSzervezet.Items.Clear();
                Adat_Users EgyFelhasználó = AdatokUsers.FirstOrDefault(a => a.UserName == Felhasználók.Text.Trim());
                if (EgyFelhasználó == null) return;

                //Végig megyünk a szervezeteken
                foreach (Adat_Kiegészítő_Könyvtár szervezet in AdatokSzervezet)
                {
                    //Ha van ehhez a szervezethez joga akkor engedélyezzük a kiírást
                    string[] gomb = CmbGombok.Text.Split('=');
                    Adat_Gombok EgyGomb = (from a in AdatokGombok
                                           where a.Szervezet.Contains(szervezet.Név)
                                           && a.GombName == gomb[1].Trim()
                                           select a).FirstOrDefault();
                    if (EgyGomb != null)
                    {
                        // ha van joga ahhoz a szervezethez a felhasználónak akkor kiírjuk a lehetőséget
                        if (EgyFelhasználó.Szervezetek.Contains(szervezet.Név))
                        {
                            LstChkSzervezet.Items.Add(szervezet.Név);

                            // Jogosultság beállítása, ha már van ilyen
                            Adat_Jogosultságok EgyJog = AdatokJogosultságok.FirstOrDefault(a =>
                                a.UserId == FelhasználóId &&
                                a.OldalId == AblakID &&
                                a.SzervezetId == szervezet.ID
                                && a.GombokId == EgyGomb.GombokId
                            );
                            if (EgyJog != null) LstChkSzervezet.SetItemChecked(LstChkSzervezet.Items.Count - 1, true);
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


    }
}