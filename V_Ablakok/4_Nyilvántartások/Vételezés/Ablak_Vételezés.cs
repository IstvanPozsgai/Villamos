using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.V_Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.V_Ablakok._4_Nyilvántartások.Vételezés
{
    public partial class Ablak_Vételezés : Form
    {
        readonly Kezelő_AnyagTörzs KézAnyag = new Kezelő_AnyagTörzs();
        readonly Kezelő_Rezsi_Könyvelés KézRezsi = new Kezelő_Rezsi_Könyvelés();


        List<Adat_Anyagok> Adatok = new List<Adat_Anyagok>();
        List<Adat_Rezsi_Lista> AdatokKész = new List<Adat_Rezsi_Lista>();
        string[] Keresőszavak;

        readonly DataTable AdatTábla = new DataTable();
        readonly DataTable AdatTáblaFelső = new DataTable();
        public Ablak_Vételezés()
        {
            InitializeComponent();
            Start();
        }

        #region Alap

        private void Start()
        {
            TáblaÍrás();
            CmbTelephely.Text = "Angyalföld";
            //  List<Adat_Rezsi_Lista> AdatokKész = KézRezsi.Lista_Adatok(CmbTelephely.Text.Trim());
            AdatokKész = KézRezsi.Lista_Adatok("Angyalföld");
        }

        private void Ablak_Vételezés_Load(object sender, EventArgs e)
        {

        }

        private void Ablak_Vételezés_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Anyag_Karbantartás?.Close();
        }

        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\akkumulátor.html";
                Module_Excel.Megnyitás(hely);
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
        /// <summary>
        /// Betöltjük a raktárkészletet és módosítjuk a cikkszámokat és árakat SAP szerint
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSAP_Click(object sender, EventArgs e)
        {
            try
            {
                string fájlexc = "";
                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "SAP-s Adatok betöltése",
                    FileName = "",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialogPI.ShowDialogEllenőr(OpenFileDialog1) == DialogResult.OK)
                {
                    fájlexc = OpenFileDialog1.FileName.ToLower();
                    string[] darabol = fájlexc.Split('.');
                    if (darabol.Length < 2) throw new HibásBevittAdat("Nem megfelelő a betölteni kívánt fájl formátuma!");
                    if (!darabol[darabol.Length - 1].Contains("xls")) throw new HibásBevittAdat("Nem megfelelő a betölteni kívánt fájl kiterjesztés formátuma!");
                }
                else
                    return;

                SAP_Adatokbeolvasása.Raktár_beolvasó(fájlexc);



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



        #region Anyagkarbantartás 
        Ablak_Anyag_Karbantartás Új_Ablak_Anyag_Karbantartás;
        private void AnyagMódosítás_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Anyag_Karbantartás == null)
            {
                Új_Ablak_Anyag_Karbantartás = new Ablak_Anyag_Karbantartás();
                Új_Ablak_Anyag_Karbantartás.FormClosed += Új_Ablak_Anyag_Karbantartás_FormClosed;
                Új_Ablak_Anyag_Karbantartás.Show();
            }
            else
            {
                Új_Ablak_Anyag_Karbantartás.Activate();
                Új_Ablak_Anyag_Karbantartás.WindowState = FormWindowState.Maximized;
            }
        }

        private void Új_Ablak_Anyag_Karbantartás_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Anyag_Karbantartás = null;
        }
        #endregion


        #region Kereső Tábla

        private void TáblaÍrás()
        {
            Adatok = KézAnyag.Lista_Adatok().OrderBy(a => a.Cikkszám).ToList();
            Fejléc();
            ABFeltöltése();
            Tábla.DataSource = AdatTábla;
            OszlopSzélesség();
            Tábla.Refresh();
            Tábla.Visible = true;
            Tábla.ClearSelection();
        }

        private void ABFeltöltése()
        {
            try
            {
                Keresőszavak = Kereső.Text.ToLower().Split(' ');
                AdatTábla.Clear();
                foreach (Adat_Anyagok rekord in Adatok)
                {
                    if (KellÍrni(rekord.Megnevezés.ToLower() + rekord.KeresőFogalom.ToLower()))
                    {
                        DataRow Soradat = AdatTábla.NewRow();
                        Soradat["Cikkszám"] = rekord.Cikkszám;
                        Soradat["Megnevezés"] = rekord.Megnevezés;
                        Soradat["Kereső fogalom"] = rekord.KeresőFogalom;
                        Soradat["Sarzs"] = rekord.Sarzs;
                        Soradat["Ár"] = rekord.Ár;
                        int RaktárK = 0;

                        Soradat["Raktárkészlet"] = 0;

                        int RezsiK = 0;
                        Adat_Rezsi_Lista EgyRezsi = (from a in AdatokKész
                                                     where a.Azonosító.Trim() == rekord.Cikkszám.Trim()
                                                     select a).FirstOrDefault();
                        if (EgyRezsi != null) RezsiK = (int)EgyRezsi.Mennyiség;
                        Soradat["Rezsikészlet"] = RezsiK;
                        AdatTábla.Rows.Add(Soradat);
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

        private bool KellÍrni(string szöveg)
        {
            //ha nincs keresés akkor minden kiírunk
            if (string.IsNullOrEmpty(Kereső.Text)) return true;
            if (string.IsNullOrEmpty(szöveg)) return false;
            foreach (string szó in Keresőszavak)
            {
                if (!string.IsNullOrWhiteSpace(szó) && !szöveg.Contains(szó))
                    return false;
            }
            return true;
        }

        private void Fejléc()
        {
            try
            {
                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Cikkszám");
                AdatTábla.Columns.Add("Megnevezés");
                AdatTábla.Columns.Add("Kereső fogalom");
                AdatTábla.Columns.Add("Sarzs");
                AdatTábla.Columns.Add("Ár");
                AdatTábla.Columns.Add("Raktárkészlet");
                AdatTábla.Columns.Add("Rezsikészlet");
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

        private void OszlopSzélesség()
        {
            Tábla.Columns["Cikkszám"].Width = 130;
            Tábla.Columns["Megnevezés"].Width = 400;
            Tábla.Columns["Kereső fogalom"].Width = 400;
            Tábla.Columns["Sarzs"].Width = 80;
            Tábla.Columns["Ár"].Width = 80;
            Tábla.Columns["Raktárkészlet"].Width = 120;
            Tábla.Columns["Rezsikészlet"].Width = 120;

        }

        private void Kereső_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                TáblaÍrás();
            }
        }
        #endregion


        #region FelsőTáblázat
        private void Frissíti_táblalistát_Click(object sender, EventArgs e)
        {
            AdatTáblaFelső.Clear();
            TáblaFelső.Refresh();
        }

        private void FejlécFelső()
        {
            try
            {
                AdatTáblaFelső.Columns.Clear();
                AdatTáblaFelső.Columns.Add("Cikkszám");
                AdatTáblaFelső.Columns.Add("Megnevezés");
                AdatTáblaFelső.Columns.Add("Kereső fogalom");
                AdatTáblaFelső.Columns.Add("Sarzs");
                AdatTáblaFelső.Columns.Add("Ár");
                AdatTáblaFelső.Columns.Add("Mennyiség");
                AdatTáblaFelső.Columns.Add("Összesen");
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

        private void OszlopSzélességFelső()
        {
            TáblaFelső.Columns["Cikkszám"].Width = 130;
            TáblaFelső.Columns["Megnevezés"].Width = 400;
            TáblaFelső.Columns["Kereső fogalom"].Width = 400;
            TáblaFelső.Columns["Sarzs"].Width = 80;
            TáblaFelső.Columns["Ár"].Width = 80;
            TáblaFelső.Columns["Mennyiség"].Width = 80;
            TáblaFelső.Columns["Összesen"].Width = 80;

        }
        #endregion

        private void MásikTáblázatba_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.SelectedRows.Count < 1) return;
                // Ha még nincs beállítva a felső tábla szerkezete, hozzuk létre
                if (AdatTáblaFelső.Columns.Count == 0) FejlécFelső();

                foreach (DataGridViewRow row in Tábla.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        DataRow Soradat = AdatTáblaFelső.NewRow();
                        Soradat["Cikkszám"] = row.Cells["Cikkszám"].Value;
                        Soradat["Megnevezés"] = row.Cells["Megnevezés"].Value;
                        Soradat["Kereső fogalom"] = row.Cells["Kereső fogalom"].Value;
                        Soradat["Sarzs"] = row.Cells["Sarzs"].Value;
                        Soradat["Ár"] = row.Cells["Ár"].Value;
                        Soradat["Mennyiség"] = 0;
                        Soradat["Összesen"] = 0;
                        AdatTáblaFelső.Rows.Add(Soradat);
                    }
                }
                // Frissítjük a felső táblázatot
                TáblaFelső.DataSource = AdatTáblaFelső;
                TáblaFelső.Refresh();
                OszlopSzélességFelső();
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

        private void TáblaFelső_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Ellenőrizzük, hogy a "Mennyiség" oszlopban történt-e változás
                if (TáblaFelső.Columns[e.ColumnIndex].Name == "Mennyiség")
                {
                    DataGridViewRow row = TáblaFelső.Rows[e.RowIndex];
                    if (row.Cells["Mennyiség"].Value != null && row.Cells["Ár"].Value != null)
                    {
                        if (double.TryParse(row.Cells["Mennyiség"].Value.ToString(), out double mennyiseg) &&
                            double.TryParse(row.Cells["Ár"].Value.ToString(), out double ar))
                        {
                            row.Cells["Összesen"].Value = mennyiseg * ar;
                        }
                        else
                        {
                            row.Cells["Összesen"].Value = 0;
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
    }
}
