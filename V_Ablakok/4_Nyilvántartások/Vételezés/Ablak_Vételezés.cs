using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.V_Adatszerkezet;
using Villamos.V_Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Ablakok.Közös;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos.V_Ablakok._4_Nyilvántartások.Vételezés
{
    public partial class Ablak_Vételezés : Form
    {
        readonly Kezelő_AnyagTörzs KézAnyag = new Kezelő_AnyagTörzs();
        readonly Kezelő_Rezsi_Könyvelés KézRezsi = new Kezelő_Rezsi_Könyvelés();
        readonly Kezelő_Raktár KézRaktár = new Kezelő_Raktár();
        readonly Kezelő_Kiegészítő_Szolgálattelepei KézSzolgálattelepei = new Kezelő_Kiegészítő_Szolgálattelepei();


        List<Adat_Anyagok> Adatok = new List<Adat_Anyagok>();
        List<Adat_Rezsi_Lista> AdatokKész = new List<Adat_Rezsi_Lista>();
        List<Adat_Raktár> AdatokRaktár = new List<Adat_Raktár>();
        string[] Keresőszavak;

        readonly DataTable AdatTábla = new DataTable();
        readonly DataTable AdatTáblaFelső = new DataTable();

        string Raktárhely = "";
        string fájlexc = "";
        string CikkSzám = "";
        string Sarzs = "";
        public Ablak_Vételezés()
        {
            InitializeComponent();

        }



        #region Alap
        private void Start()
        {
            //Ha az első karakter "R" akkor az új jogosultságkiosztást használjuk
            //ha nem akkor a régit használjuk
            if (Program.PostásJogkör.Substring(0, 1) == "R")
            {
                TelephelyekFeltöltéseÚj();
                GombLathatosagKezelo.Beallit(this, CmbTelephely.Text.Trim());
            }
            else
            {
                Telephelyekfeltöltése();
                Jogosultságkiosztás();
            }
            AdatokFrissítése();
        }

        private void AdatokFrissítése()
        {
            AdatokKész = KézRezsi.Lista_Adatok(CmbTelephely.Text.Trim());
            AdatokRaktár = KézRaktár.Lista_Adatok();
            Raktárhely = KézSzolgálattelepei.Lista_Adatok().Where(a => a.Telephelynév == CmbTelephely.Text.Trim()).Select(a => a.Raktár).FirstOrDefault() ?? "";
            TáblaÍrás();
        }

        private void Ablak_Vételezés_Load(object sender, EventArgs e)
        {
            Start();
        }

        private void Ablak_Vételezés_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Anyag_Karbantartás?.Close();
            Új_Ablak_Fénykép_Betöltés?.Close();
            Új_Ablak_Készlet?.Close();
        }

        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Vételezés_segéd.html";
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

        private void Telephelyekfeltöltése()
        {
            try
            {
                CmbTelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    CmbTelephely.Items.Add(Elem);
                if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér)
                { CmbTelephely.Text = CmbTelephely.Items[0].ToString().Trim(); }
                else
                { CmbTelephely.Text = Program.PostásTelephely; }

                CmbTelephely.Enabled = Program.Postás_Vezér;
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

        private void TelephelyekFeltöltéseÚj()
        {
            try
            {
                CmbTelephely.Items.Clear();
                foreach (string Adat in GombLathatosagKezelo.Telephelyek(this.Name))
                    CmbTelephely.Items.Add(Adat.Trim());
                //Alapkönyvtárat beállítjuk 
                if (CmbTelephely.Items.Cast<string>().Contains(Program.PostásTelephely))
                    CmbTelephely.Text = Program.PostásTelephely;
                else
                    CmbTelephely.Text = CmbTelephely.Items[0].ToStrTrim();
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


        private void Jogosultságkiosztás()
        {
            int melyikelem;
            BtnSAP.Visible = false;
            AnyagMódosítás.Visible = false;

            // ide kell az összes gombot tenni amit szabályozni akarunk false


            melyikelem = 50;
            // módosítás 1 
            if (MyF.Vanjoga(melyikelem, 1))
            {
                BtnSAP.Visible = true;
            }
            // módosítás 2 
            if (MyF.Vanjoga(melyikelem, 2))
            {
                AnyagMódosítás.Visible = true;
            }
            // módosítás 3
            if (MyF.Vanjoga(melyikelem, 3))
            {

            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Holtart.Lép();
        }
        #endregion


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
            Tábla.CleanFilterAndSort();
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
                        Soradat["Kereső fogalom"] = KeresőFogalom(rekord.Cikkszám);
                        Soradat["Sarzs"] = rekord.Sarzs;
                        Soradat["Ár"] = Math.Round(rekord.Ár, 1);
                        double RaktárK = 0;
                        Adat_Raktár EgyRaktár = (from a in AdatokRaktár
                                                 where a.Cikkszám.Trim() == rekord.Cikkszám.Trim()
                                                 && a.Sarzs.Trim() == rekord.Sarzs.Trim()
                                                 && a.Raktárhely.Trim() == Raktárhely.Trim()
                                                 select a).FirstOrDefault();
                        if (EgyRaktár != null) RaktárK = EgyRaktár.Mennyiség;
                        Soradat["Saját Raktár"] = RaktárK;

                        double RezsiK = 0;
                        Adat_Rezsi_Lista EgyRezsi = (from a in AdatokKész
                                                     where a.Azonosító.Trim() == rekord.Cikkszám.Trim()
                                                     select a).FirstOrDefault();
                        if (EgyRezsi != null) RezsiK = EgyRezsi.Mennyiség;
                        Soradat["Rezsi készlet"] = RezsiK;

                        double RaktárKE = 0;
                        RaktárKE = (from a in AdatokRaktár
                                    where a.Cikkszám.Trim() == rekord.Cikkszám.Trim()
                                    && a.Sarzs.Trim() == rekord.Sarzs.Trim()
                                    && a.Raktárhely.Trim() != Raktárhely.Trim()
                                    select a).ToList().Sum(a => a.Mennyiség);

                        Soradat["Egyéb Raktár"] = RaktárKE;

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

        private string KeresőFogalom(string cikkszám)
        {
            string válasz = "";
            List<Adat_Anyagok> AdatokKereső = (from a in Adatok
                                               where a.Cikkszám.Trim() == cikkszám.Trim()
                                               select a).ToList();
            foreach (Adat_Anyagok rekord in AdatokKereső)
            {
                if (!válasz.Contains(rekord.KeresőFogalom.Trim())) válasz += rekord.KeresőFogalom.Trim() + " ";
            }
            return válasz;
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
                AdatTábla.Columns.Add("Ár", typeof(double));
                AdatTábla.Columns.Add("Saját Raktár", typeof(double));
                AdatTábla.Columns.Add("Rezsi készlet", typeof(double));
                AdatTábla.Columns.Add("Egyéb Raktár", typeof(double));
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
            Tábla.Columns["Saját Raktár"].Width = 100;
            Tábla.Columns["Rezsi készlet"].Width = 100;
            Tábla.Columns["Egyéb Raktár"].Width = 100;

        }

        private void Kereső_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                TáblaÍrás();
            }
        }

        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            CikkSzám = Tábla.Rows[e.RowIndex].Cells["Cikkszám"].Value.ToString();
            Sarzs = Tábla.Rows[e.RowIndex].Cells["Sarzs"].Value.ToString();
        }
        #endregion


        #region FelsőTáblázat
        private void FejlécFelső()
        {
            try
            {
                Tábla.CleanFilterAndSort();
                AdatTáblaFelső.Columns.Clear();
                AdatTáblaFelső.Columns.Add("Cikkszám");
                AdatTáblaFelső.Columns.Add("Mennyiség", typeof(double));
                AdatTáblaFelső.Columns.Add("Sarzs");
                AdatTáblaFelső.Columns.Add("Raktár");
                AdatTáblaFelső.Columns.Add("Művelet");
                AdatTáblaFelső.Columns.Add("Fogadó");
                AdatTáblaFelső.Columns.Add("Megnevezés");
                AdatTáblaFelső.Columns.Add("Ár", typeof(double));
                AdatTáblaFelső.Columns.Add("Összesen", typeof(double));
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
            TáblaFelső.Columns["Sarzs"].Width = 80;
            TáblaFelső.Columns["Ár"].Width = 80;
            TáblaFelső.Columns["Mennyiség"].Width = 120;
            TáblaFelső.Columns["Összesen"].Width = 120;

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
                Összesít();
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

        private void Összesít()
        {
            try
            {
                double összesen = 0;
                foreach (DataGridViewRow row in TáblaFelső.Rows)
                {
                    if (row.Cells["Összesen"].Value != null && double.TryParse(row.Cells["Összesen"].Value.ToString(), out double cellaErtek))
                    {
                        összesen += cellaErtek;
                    }
                }
                Összesen.Text = $"Összeg: {összesen} Ft";
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

        private void TáblaFelső_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            CikkSzám = TáblaFelső.Rows[e.RowIndex].Cells["Cikkszám"].Value.ToString();
            Sarzs = TáblaFelső.Rows[e.RowIndex].Cells["Sarzs"].Value.ToString();
        }
        #endregion


        #region Gombok
        /// <summary>
        /// Betöltjük a raktárkészletet és módosítjuk a cikkszámokat és árakat SAP szerint
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnSAP_Click(object sender, EventArgs e)
        {
            try
            {

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

                Holtart.Be();
                timer1.Enabled = true;
                await Task.Run(() => SAP_Adatokbeolvasása.Raktár_beolvasó(fájlexc));
                timer1.Enabled = false;
                Holtart.Ki();

                AdatokFrissítése();
                MessageBox.Show($"Az adat konvertálás befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        Soradat["Sarzs"] = row.Cells["Sarzs"].Value;
                        Soradat["Ár"] = row.Cells["Ár"].Value;
                        Soradat["Mennyiség"] = 0;
                        Soradat["Összesen"] = 0;
                        Soradat["Raktár"] = Raktárhely;
                        Soradat["Művelet"] = "0010";
                        Soradat["Összesen"] = 0;
                        Soradat["Fogadó"] = "";
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

        private void Másol_Click(object sender, EventArgs e)
        {
            // Először töröljük az esetleges korábbi kijelöléseket
            TáblaFelső.ClearSelection();

            // Végigmegyünk az összes soron
            foreach (DataGridViewRow row in TáblaFelső.Rows)
            {
                // Csak a nem új sorokat jelöljük ki
                if (!row.IsNewRow)
                {
                    // Az első 6 oszlopot jelöljük ki
                    for (int i = 0; i < 6 && i < TáblaFelső.ColumnCount; i++)
                    {
                        row.Cells[i].Selected = true;
                    }
                }
            }

            // Sorok adatainak összegyűjtése
            List<string> lines = new List<string>();

            foreach (DataGridViewRow row in TáblaFelső.Rows)
            {
                if (!row.IsNewRow)
                {
                    List<string> cells = new List<string>();
                    for (int i = 0; i < 6 && i < TáblaFelső.ColumnCount; i++)
                    {
                        cells.Add(row.Cells[i].Value?.ToString() ?? "");
                    }
                    lines.Add(string.Join("\t", cells));
                }
            }

            // Vágólapra helyezés
            string result = string.Join(Environment.NewLine, lines);
            Clipboard.SetText(result);
        }

        private void FelsőÜrítés_Click(object sender, EventArgs e)
        {
            AdatTáblaFelső.Clear(); // Csak a sorokat törli, az oszlopokat nem
            TáblaFelső.DataSource = AdatTáblaFelső; // Biztosan frissül a nézet
            TáblaFelső.Refresh();
            Összesen.Text = $"Összeg: <<< - >>>";
        }

        private void Excel_gomb_Click(object sender, EventArgs e)
        {
            if (TáblaFelső.Rows.Count <= 0) return;
            MyE.Mentés("MyDocuments",
                                "Listázott tartalom mentése Excel fájlba",
                                $"Vételezés-{Program.PostásNév}-{DateTime.Now:yyyyMMddHHmmss}",
                                "Excel |*.xlsx",
                                TáblaFelső);
        }

        private void SorTörlés_Click(object sender, EventArgs e)
        {
            // Ellenőrizzük, hogy van-e kijelölt sor
            if (TáblaFelső.SelectedRows.Count > 0)
            {
                // Végigmegyünk a kijelölt sorokon
                foreach (DataGridViewRow dgvRow in TáblaFelső.SelectedRows)
                {
                    // Megkeressük a megfelelő DataRow-t
                    int index = dgvRow.Index;
                    if (index >= 0 && index < AdatTáblaFelső.Rows.Count)
                    {
                        AdatTáblaFelső.Rows[index].Delete(); // Megjelöli törlésre
                    }
                }

                // Véglegesítjük a törlést
                AdatTáblaFelső.AcceptChanges();

                // Frissítjük a DataGridView-t
                TáblaFelső.DataSource = AdatTáblaFelső;
                TáblaFelső.Refresh();
                Összesít();
            }
        }

        private void Előjeletvált_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in TáblaFelső.Rows)
                {
                    if (!row.IsNewRow && row.Cells["Mennyiség"].Value != null)
                    {
                        if (double.TryParse(row.Cells["Mennyiség"].Value.ToString(), out double mennyiseg))
                        {
                            double ujMennyiseg = mennyiseg * -1;
                            row.Cells["Mennyiség"].Value = ujMennyiseg;

                            // Ha van "Ár" oszlop, frissítsük az "Összesen" értéket is
                            if (row.Cells["Ár"].Value != null && double.TryParse(row.Cells["Ár"].Value.ToString(), out double ar))
                            {
                                row.Cells["Összesen"].Value = ujMennyiseg * ar;
                            }
                        }
                    }
                }
                TáblaFelső.Refresh();
                Összesít();
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


        #region Fényképek megjelenítése és rögzítése
        Ablak_Fénykép_Betöltés Új_Ablak_Fénykép_Betöltés;
        private void Képnéző_Click(object sender, EventArgs e)
        {
            try
            {
                if (CikkSzám.Trim() == "") return;
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Rezsiképek".KönyvSzerk();

                DirectoryInfo dir = new DirectoryInfo(hely);
                System.IO.FileInfo[] aryFi = dir.GetFiles($"*{CikkSzám.Trim()}*.jpg");
                if (aryFi.Length == 0)
                    if (MessageBox.Show("Nincs kép a kiválasztott cikkszámhoz.\nFolytatjuk a képek feltöltésével?", "Információ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No) return;

                Új_Ablak_Fénykép_Betöltés?.Close();
                Új_Ablak_Fénykép_Betöltés = new Ablak_Fénykép_Betöltés(hely, CikkSzám);
                Új_Ablak_Fénykép_Betöltés.FormClosed += Új_Ablak_Fénykép_Betöltés_Closed;
                Új_Ablak_Fénykép_Betöltés.Top = 50;
                Új_Ablak_Fénykép_Betöltés.Left = 50;
                Új_Ablak_Fénykép_Betöltés.Show();
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

        private void Új_Ablak_Fénykép_Betöltés_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Fénykép_Betöltés = null;
        }

        #endregion


        #region Készletek
        Ablak_Készlet Új_Ablak_Készlet;
        private void RaktárKészlet_Click(object sender, EventArgs e)
        {
            if (CikkSzám.Trim() == "") return;
            List<Adat_Raktár> RaktárKészlet = (from a in AdatokRaktár
                                               where a.Cikkszám.Trim() == CikkSzám
                                               && a.Sarzs.Trim() == Sarzs
                                               select a).ToList();

            Új_Ablak_Készlet?.Close();
            Új_Ablak_Készlet = new Ablak_Készlet(RaktárKészlet, CikkSzám);
            Új_Ablak_Készlet.FormClosed += Új_Ablak_Készlet_Closed;
            Új_Ablak_Készlet.Top = 50;
            Új_Ablak_Készlet.Left = 50;
            Új_Ablak_Készlet.Show();
        }

        private void Új_Ablak_Készlet_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Készlet = null;
        }
        #endregion
    }
}
