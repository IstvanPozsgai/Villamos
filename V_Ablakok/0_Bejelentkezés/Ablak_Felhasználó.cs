using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;


namespace Villamos
{
    public partial class Ablak_Felhasználó : Form
    {
        readonly Kezelők_Users Kéz = new Kezelők_Users();
        readonly Kezelő_Behajtás_Dolgozótábla KézDolgozó = new Kezelő_Behajtás_Dolgozótábla();
        List<Adat_Users> Adatok = new List<Adat_Users>();
        List<Adat_Behajtás_Dolgozótábla> AdatokDolg = new List<Adat_Behajtás_Dolgozótábla>();
#pragma warning disable IDE0044
        DataTable AdatTáblaALap = new DataTable();
#pragma warning restore IDE0044


        #region Alap
        public Ablak_Felhasználó()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            Adatok = Kéz.Lista_Adatok();
            AdatokDolg = KézDolgozó.Lista_Adatok().Where(a => a.Státus == false).ToList();
            CombokFeltöltése();
            Üres();
            TáblázatListázás();
            GombLathatosagKezelo.Beallit(this);
        }

        private void AblakFelhasználó_Load(object sender, EventArgs e)
        {
        }

        private void BtnSugó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\felhasználó.html";
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
        #endregion


        #region Táblázat
        private void TáblázatListázás()
        {
            try
            {
                Adatok = Kéz.Lista_Adatok();
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
                AdatTáblaALap.Columns.Add("Id");
                AdatTáblaALap.Columns.Add("Felhasználó név");
                AdatTáblaALap.Columns.Add("WinFelhasználó név");
                AdatTáblaALap.Columns.Add("Dolgozószám");
                AdatTáblaALap.Columns.Add("Dolgozó Név");
                AdatTáblaALap.Columns.Add("Jelszó");
                AdatTáblaALap.Columns.Add("Dátum");
                AdatTáblaALap.Columns.Add("Frissít");
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
            AdatTáblaALap.Clear();


            foreach (Adat_Users rekord in Adatok)
            {
                DataRow Soradat = AdatTáblaALap.NewRow();
                Adat_Behajtás_Dolgozótábla Elem = (from a in AdatokDolg
                                                   where a.Dolgozószám == rekord.Dolgozószám
                                                   select a).FirstOrDefault();
                string DolgozóNév = Elem == null ? "" : Elem.Dolgozónév;
                Soradat["Id"] = rekord.UserId;
                Soradat["Felhasználó név"] = rekord.UserName;
                Soradat["WinFelhasználó név"] = rekord.WinUserName;
                Soradat["Dolgozószám"] = rekord.Dolgozószám;
                Soradat["Dolgozó Név"] = DolgozóNév;
                Soradat["Jelszó"] = rekord.Password;
                Soradat["Dátum"] = rekord.Dátum.ToShortDateString();
                Soradat["Frissít"] = rekord.Frissít ? "Igen" : "Nem";
                Soradat["Törölt"] = rekord.Törölt == true ? "Törölt" : "Aktív";
                AdatTáblaALap.Rows.Add(Soradat);
            }
        }

        private void AlapTáblaOszlopSzélesség()
        {
            Tábla.Columns["Id"].Width = 80;
            Tábla.Columns["Felhasználó név"].Width = 180;
            Tábla.Columns["WinFelhasználó név"].Width = 180;
            Tábla.Columns["Dolgozószám"].Width = 110;
            Tábla.Columns["Dolgozó Név"].Width = 250;
            Tábla.Columns["Jelszó"].Width = 250;
            Tábla.Columns["Dátum"].Width = 130;
            Tábla.Columns["Frissít"].Width = 110;
            Tábla.Columns["Törölt"].Width = 110;
        }

        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string DolgIDS = Tábla.Rows[e.RowIndex].Cells[0].Value.ToStrTrim();
            if (!int.TryParse(DolgIDS, out int DolgID)) return;
            Adatokkiírása(DolgID);
        }

        private void Adatokkiírása(int dolgID)
        {

            try
            {
                Adat_Users adat = (from a in Adatok
                                   where a.UserId == dolgID
                                   select a).FirstOrDefault();
                if (adat == null) return;
                UserId.Text = adat.UserId.ToString();
                TextUserNév.Text = adat.UserName;
                TextWinUser.Text = adat.WinUserName;
                CmbDolgozószám.Text = adat.Dolgozószám;
                CmbDolgozónév.Text = DolgozóNév(adat.Dolgozószám);
                TxtPassword.Text = "";
                Frissít.Checked = adat.Frissít;
                Törölt.Checked = adat.Törölt;
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


        #region Gombok
        /// <summary>
        /// Beviteli mezőket üríti
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnÚj_Click(object sender, EventArgs e)
        {
            Üres();
        }

        /// <summary>
        /// Rögzíti vagy módosítja az adatokat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(UserId.Text, out int Id)) Id = 0;
                if (string.IsNullOrWhiteSpace(TextUserNév.Text)) throw new HibásBevittAdat("Kérem töltse ki az Felhasználó név mezőt!");
                if (string.IsNullOrWhiteSpace(CmbDolgozószám.Text)) throw new HibásBevittAdat("Kérem töltse ki a Dolgozószám mezőt!");
                TextUserNév.Text = TextUserNév.Text.ToLower();
                if (Adatok.Any(a => a.UserName == TextUserNév.Text.Trim() && a.UserId != Id)) throw new HibásBevittAdat("A felhasználónév már létezik!");
                if (TextWinUser.Text.Trim() != "" && Adatok.Any(a => a.WinUserName == TextWinUser.Text.Trim() && a.UserId != Id)) throw new HibásBevittAdat("A Windows felhasználónév már létezik egy másik felhasználónál!");
                if (Adatok.Any(a => a.Dolgozószám == CmbDolgozószám.Text.Trim() && a.UserId != Id)) throw new HibásBevittAdat("A Dolgozószámhoz már létezik egy másik felhasználó!");
                string jelszó = Jelszó.HashPassword(TxtPassword.Text.Trim());

                Adat_Users ADAT = new Adat_Users(
                    Id,
                    TextUserNév.Text.Trim(),
                    TextWinUser.Text.Trim(),
                    CmbDolgozószám.Text.Trim(),
                    jelszó,
                    DateTime.Now,
                    Frissít.Checked,
                    Törölt.Checked);
                Kéz.Döntés(ADAT);
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

        private void BtnFrissít_Click(object sender, EventArgs e)
        {
            TáblázatListázás();
        }
        #endregion


        #region BeviteliMezők
        private void Üres()
        {
            UserId.Text = "";
            TextUserNév.Text = "";
            TextWinUser.Text = "";
            CmbDolgozószám.Text = "";
            CmbDolgozónév.Text = "";
            TxtPassword.Text = "123456";
            Frissít.Checked = true;
            Törölt.Checked = false;
        }

        private void CombokFeltöltése()
        {
            CmbDolgozószám.Items.Clear();
            CmbDolgozónév.Items.Clear();
            CmbDolgozószám.Items.Add("");
            CmbDolgozónév.Items.Add("");
            AdatokDolg.OrderBy(a => a.Dolgozószám).ToList();
            foreach (Adat_Behajtás_Dolgozótábla elem in AdatokDolg)
                CmbDolgozószám.Items.Add(elem.Dolgozószám);

            AdatokDolg.OrderBy(a => a.Dolgozónév).ToList();
            foreach (Adat_Behajtás_Dolgozótábla elem in AdatokDolg)
                CmbDolgozónév.Items.Add(elem.Dolgozónév);

        }

        private void TxtPassword_TextChanged(object sender, EventArgs e)
        {
            Frissít.Checked = true;
        }
        #endregion


        #region ComboBoxok
        private void CmbDolgozószám_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CmbDolgozószám.Text = CmbDolgozószám.Items[CmbDolgozószám.SelectedIndex].ToString();
            CmbDolgozónév.Text = DolgozóNév(CmbDolgozószám.Text.Trim());

        }

        private string DolgozóNév(string dolgozószám)
        {
            string válasz = "";
            try
            {
                if (dolgozószám.Trim() != "")
                {
                    Adat_Behajtás_Dolgozótábla elem = (from a in AdatokDolg
                                                       where a.Dolgozószám == dolgozószám
                                                       select a).FirstOrDefault();
                    if (elem != null) válasz = elem.Dolgozónév; else CmbDolgozónév.Text = "";
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

            return válasz;
        }

        private void CmbDolgozónév_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CmbDolgozónév.Text = CmbDolgozónév.Items[CmbDolgozónév.SelectedIndex].ToString();
            if (CmbDolgozónév.Text.Trim() != "")
            {
                Adat_Behajtás_Dolgozótábla elem = (from a in AdatokDolg
                                                   where a.Dolgozónév == CmbDolgozónév.Text.Trim()
                                                   select a).FirstOrDefault();
                if (elem != null) CmbDolgozószám.Text = elem.Dolgozószám; else CmbDolgozószám.Text = "";
            }
            else CmbDolgozószám.Text = "";

        }

        #endregion


    }
}