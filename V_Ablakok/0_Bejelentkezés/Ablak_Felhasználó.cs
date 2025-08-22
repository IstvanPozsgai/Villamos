﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.MindenEgyéb;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;


namespace Villamos
{
    public partial class Ablak_Felhasználó : Form
    {
        readonly Kezelő_Users Kéz = new Kezelő_Users();
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
            AdatokDolg = KézDolgozó.Lista_Adatok();
            //    AdatokDolg = KézDolgozó.Lista_Adatok().Where(a => a.Státus == true).ToList();
            CombokFeltöltése();
            Üres();
            TáblázatListázás();
            SzervezetFeltöltésChk();
            //    GombLathatosagKezelo.Beallit(this);
            Admin();

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
                Adatok = Kéz.Lista_Adatok().OrderBy(a => a.UserName).ToList();
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
                AdatTáblaALap.Columns.Add("Szervezet");
                AdatTáblaALap.Columns.Add("Szervezetek");
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
            try
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
                    Soradat["Szervezet"] = rekord.Szervezet;
                    Soradat["Szervezetek"] = rekord.Szervezetek; // ÚJ: közvetlenül a Dolgozó Név után
                    Soradat["Jelszó"] = rekord.Password;
                    Soradat["Dátum"] = rekord.Dátum.ToShortDateString();
                    Soradat["Frissít"] = rekord.Frissít ? "Igen" : "Nem";
                    Soradat["Törölt"] = rekord.Törölt == true ? "Törölt" : "Aktív";
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
            Tábla.Columns["Id"].Width = 80;
            Tábla.Columns["Felhasználó név"].Width = 180;
            Tábla.Columns["WinFelhasználó név"].Width = 180;
            Tábla.Columns["Dolgozószám"].Width = 110;
            Tábla.Columns["Dolgozó Név"].Width = 250;
            Tábla.Columns["Szervezet"].Width = 150;
            Tábla.Columns["Szervezetek"].Width = 250;
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
                CmbSzervezet.Text = adat.Szervezet;
                GlobalAdmin.Checked = adat.GlobalAdmin;
                TelephelyAdmin.Checked = adat.TelepAdmin;

                for (int i = 0; i < ChkSzervezet.Items.Count; i++)
                    ChkSzervezet.SetItemChecked(i, false);

                if (!string.IsNullOrWhiteSpace(adat.Szervezetek))
                {
                    string[] szervezetek = adat.Szervezetek.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < ChkSzervezet.Items.Count; i++)
                    {
                        string itemText = ChkSzervezet.Items[i].ToString();
                        if (szervezetek.Contains(itemText))
                        {
                            ChkSzervezet.SetItemChecked(i, true);
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
                if (CmbSzervezet.Text.Trim() == "") throw new HibásBevittAdat("Kérem töltse ki a Alap szervezet mezőt!"); ;

                // --- ÚJ: Szervezetek szöveg összeállítása a ChkSzervezet kijelölt elemeiből ---
                string szervezetek = "";
                if (ChkSzervezet.CheckedItems.Count > 0)
                {
                    List<string> szervezetLista = new List<string>();
                    foreach (object item in ChkSzervezet.CheckedItems)
                    {
                        szervezetLista.Add(item.ToString());
                    }
                    szervezetek = string.Join(";", szervezetLista);
                }
                // ------------------------------------------------------------------------------

                Adat_Users ADAT = new Adat_Users(
                    Id,
                    TextUserNév.Text.Trim(),
                    TextWinUser.Text.Trim(),
                    CmbDolgozószám.Text.Trim(),
                    jelszó,
                    DateTime.Now,
                    Frissít.Checked,
                    Törölt.Checked,
                    szervezetek,
                    CmbSzervezet.Text.Trim(),
                    GlobalAdmin.Checked,
                    TelephelyAdmin.Checked
                );
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

        private void BtnDolgozóilsta_Click(object sender, EventArgs e)
        {
            try
            {
                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "IDM-s Adatok betöltése",
                    FileName = "",
                    Filter = "Excel |*.xlsx"
                };
                string fájlexc;

                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;

                IDM_beolvasás.Behajtási_beolvasás(fájlexc);

                MessageBox.Show("Az adat konvertálás befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void JelszóMódosítás_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(UserId.Text, out int Id)) Id = 0;
                if (Id == 0) throw new HibásBevittAdat("Kérem válasszon ki egy felhasználót a táblázatból!");
                if (TxtPassword.Text.Trim() == "") TxtPassword.Text = "123456";
                string jelszó = Jelszó.HashPassword(TxtPassword.Text.Trim());
                Adat_Users adat = new Adat_Users(Id, jelszó, true);
                Kéz.MódosításJeszó(adat);
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
            for (int i = 0; i < ChkSzervezet.Items.Count; i++)
                ChkSzervezet.SetItemChecked(i, false);
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

        private void SzervezetFeltöltésChk()
        {
            try
            {
                Kezelő_Kiegészítő_Könyvtár kezSzervezet = new Kezelő_Kiegészítő_Könyvtár();
                List<Adat_Kiegészítő_Könyvtár> adatokSzervezet = kezSzervezet.Lista_Adatok().OrderBy(a => a.Név).ToList();
                CmbSzervezet.Items.Clear();
                ChkSzervezet.Items.Clear();
                for (int i = 0; i < adatokSzervezet.Count; i++)
                {
                    CmbSzervezet.Items.Add(adatokSzervezet[i].Név);
                    ChkSzervezet.Items.Add(adatokSzervezet[i].Név);
                    ChkSzervezet.SetItemChecked(i, false);
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


        #region AlapSzervezet

        /// <summary>
        /// Alapszervezet, hogy melyik telephelyre jelentkezett be a felhasználó
        /// A lista feltöltése a ChkSzervezet elemeknél történik
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbSzervezet_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //Leellenőrizzük, hogy van-e kiválasztva szervezet szerepel a chkszervetben, ha nem kijelöljük
            try
            {
                CmbSzervezet.Text = CmbSzervezet.Items[CmbSzervezet.SelectedIndex].ToString();
                if (CmbSzervezet.Text.Trim() == "") return;

                for (int i = 0; i < ChkSzervezet.Items.Count; i++)
                {
                    if (ChkSzervezet.Items[i].ToString() == CmbSzervezet.Text.Trim())
                    {
                        ChkSzervezet.SetItemChecked(i, true);
                        break;
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

        #endregion

        #region Admin
        private void Admin()
        {
            GlobalAdmin.Visible = false;
            TelephelyAdmin.Visible = false;
            if (Program.PostásUsers?.TelepAdmin == true)
            {
                GlobalAdmin.Visible = false;
                TelephelyAdmin.Visible = true;
            }
            if (Program.PostásUsers?.GlobalAdmin == true)
            {
                GlobalAdmin.Visible = true;
                TelephelyAdmin.Visible = true;
            }

        }
        #endregion
    }
}