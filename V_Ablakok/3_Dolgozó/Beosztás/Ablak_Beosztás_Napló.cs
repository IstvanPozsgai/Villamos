using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;


namespace Villamos
{
    public partial class Ablak_Beosztás_Napló
    {
        readonly Kezelő_Dolgozó_Beosztás_Napló Kéz = new Kezelő_Dolgozó_Beosztás_Napló();
        readonly Kezelő_Dolgozó_Alap KézDolgozó = new Kezelő_Dolgozó_Alap();
        readonly List<Adat_Dolgozó_Beosztás_Napló> Adatok = new List<Adat_Dolgozó_Beosztás_Napló>();

        readonly DataTable AdatTábla = new DataTable();

        #region Alap
        public Ablak_Beosztás_Napló()
        {
            InitializeComponent();
        }

        private void Ablak_Beosztás_Napló_Load(object sender, EventArgs e)
        {
            Telephelyekfeltöltése();
            Névfeltöltés();
            Dátumtól.Value = MyF.Hónap_elsőnapja(DateTime.Today);
            Dátumig.Value = DateTime.Today;
            VizsgDátum.Value = DateTime.Today;
        }

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Személy(true))
                    Cmbtelephely.Items.Add(Elem);
                if (Program.PostásTelephely == "Főmérnökség")
                { Cmbtelephely.Text = Cmbtelephely.Items[0].ToString().Trim(); }
                else
                { Cmbtelephely.Text = Program.PostásTelephely; }

                Cmbtelephely.Enabled = Program.Postás_Vezér;
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

        private void Súgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\beosztás_napló.html";
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


        #region listázza adatokat
        private void ListaFeltöltés(DateTime Kezdet, DateTime Vég)
        {
            try
            {
                Adatok.Clear();
                DateTime ideigdátum = MyF.Hónap_elsőnapja(Kezdet);
                while (Vég > ideigdátum)
                {
                    List<Adat_Dolgozó_Beosztás_Napló> AdatokRész = Kéz.Lista_Adatok(Cmbtelephely.Text.Trim(), ideigdátum);
                    Adatok.AddRange(AdatokRész);
                    ideigdátum = ideigdátum.AddMonths(1);
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

        private void Listáz_Click(object sender, EventArgs e)
        {
            try
            {
                Tábla.CleanFilterAndSort();
                Kiírás();
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

        private void Kiírás()
        {
            ABFejléc();
            ABFeltöltése();
            Tábla.CleanFilterAndSort();
            Tábla.DataSource = AdatTábla;
            OszlopSzélesség();
            Tábla.Visible = true;
            Tábla.Refresh();
            Tábla.ClearSelection();
        }

        private void ABFejléc()
        {
            try
            {
                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Dolgozónév");
                AdatTábla.Columns.Add("Törzsszám");
                AdatTábla.Columns.Add("Dátum");
                AdatTábla.Columns.Add("Beosztáskód");
                AdatTábla.Columns.Add("Rögzítette");
                AdatTábla.Columns.Add("Rögzítés dátuma");
                AdatTábla.Columns.Add("Túlóra");
                AdatTábla.Columns.Add("Csúszóra");
                AdatTábla.Columns.Add("Szabiok");
                AdatTábla.Columns.Add("Megjegyzés");
                AdatTábla.Columns.Add("Túlórakezd");
                AdatTábla.Columns.Add("Túlóravég");
                AdatTábla.Columns.Add("Túlóraok");
                AdatTábla.Columns.Add("Csúszórakezd");
                AdatTábla.Columns.Add("Csúszóravég");
                AdatTábla.Columns.Add("Csúszóraok");
                AdatTábla.Columns.Add("Sorszám");
                AdatTábla.Columns.Add("Kért");
                AdatTábla.Columns.Add("AFT óra");
                AdatTábla.Columns.Add("AFT ok");
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

        private void ABFeltöltése()
        {
            try
            {
                if (Dátumtól.Value > Dátumig.Value) Dátumtól.Value = Dátumig.Value;
                ListaFeltöltés(Dátumtól.Value, Dátumig.Value);

                DateTime Időtől = new DateTime(Dátumtól.Value.Year, Dátumtól.Value.Month, Dátumtól.Value.Day, 0, 0, 0);
                DateTime Időig = new DateTime(Dátumig.Value.Year, Dátumig.Value.Month, Dátumig.Value.Day, 23, 59, 59);

                List<Adat_Dolgozó_Beosztás_Napló> AdatokRész = (from a in Adatok
                                                                where a.Rögzítésdátum >= Időtől &&
                                                                a.Rögzítésdátum <= Időig
                                                                orderby a.Rögzítésdátum
                                                                select a).ToList();
                if (Dolgozónév.Text.Trim() != "")
                {
                    MyF.Dolgozó_Darabol(Dolgozónév.Text.Trim(), out string dolgnév, out string dolgszám);
                    AdatokRész = (from a in AdatokRész
                                  where a.Törzsszám == dolgszám
                                  select a).ToList();
                }

                if (Egy_Nap.Checked)
                {
                    AdatokRész = (from a in AdatokRész
                                  where a.Dátum == VizsgDátum.Value
                                  select a).ToList();
                }

                AdatTábla.Clear();
                foreach (Adat_Dolgozó_Beosztás_Napló rekord in AdatokRész)
                {
                    DataRow Soradat = AdatTábla.NewRow();
                    Soradat["Dolgozónév"] = rekord.Dolgozónév;
                    Soradat["Törzsszám"] = rekord.Törzsszám;
                    Soradat["Dátum"] = rekord.Dátum.ToShortDateString();
                    Soradat["Beosztáskód"] = rekord.Beosztáskód;
                    Soradat["Rögzítette"] = rekord.Rögzítette;
                    Soradat["Rögzítés dátuma"] = rekord.Rögzítésdátum;
                    Soradat["Túlóra"] = rekord.Túlóra;
                    Soradat["Csúszóra"] = rekord.Csúszóra;
                    Soradat["Szabiok"] = rekord.Szabiok;
                    Soradat["Megjegyzés"] = rekord.Megjegyzés;
                    Soradat["Túlórakezd"] = rekord.Túlórakezd.ToString("HH:mm");
                    Soradat["Túlóravég"] = rekord.Túlóravég.ToString("HH:mm");
                    Soradat["Túlóraok"] = rekord.Túlóraok;
                    Soradat["Csúszórakezd"] = rekord.CSúszórakezd.ToString("HH:mm");
                    Soradat["Csúszóravég"] = rekord.Csúszóravég.ToString("HH:mm");
                    Soradat["Csúszóraok"] = rekord.Csúszok;
                    Soradat["Sorszám"] = rekord.Sorszám;
                    Soradat["Kért"] = rekord.Kért ? "Igen" : "";
                    Soradat["AFT óra"] = rekord.AFTóra;
                    Soradat["AFT ok"] = rekord.AFTok;
                    AdatTábla.Rows.Add(Soradat);
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

        private void OszlopSzélesség()
        {
            Tábla.Columns["Dolgozónév"].Width = 200;
            Tábla.Columns["Törzsszám"].Width = 90;
            Tábla.Columns["Dátum"].Width = 110;
            Tábla.Columns["Beosztáskód"].Width = 80;
            Tábla.Columns["Rögzítette"].Width = 120;
            Tábla.Columns["Rögzítés dátuma"].Width = 160;
            Tábla.Columns["Túlóra"].Width = 80;
            Tábla.Columns["Csúszóra"].Width = 80;
            Tábla.Columns["Szabiok"].Width = 80;
            Tábla.Columns["Megjegyzés"].Width = 80;
            Tábla.Columns["Túlórakezd"].Width = 80;
            Tábla.Columns["Túlóravég"].Width = 80;
            Tábla.Columns["Túlóraok"].Width = 80;
            Tábla.Columns["Csúszórakezd"].Width = 80;
            Tábla.Columns["Csúszóravég"].Width = 80;
            Tábla.Columns["Csúszóraok"].Width = 80;
            Tábla.Columns["Sorszám"].Width = 80;
            Tábla.Columns["Kért"].Width = 80;
            Tábla.Columns["AFT óra"].Width = 80;
            Tábla.Columns["AFT ok"].Width = 80;
        }
        #endregion


        private void Névfeltöltés()
        {
            try
            {
                Dolgozónév.Items.Clear();
                Dolgozónév.BeginUpdate();
                Dolgozónév.Items.Add("");

                List<Adat_Dolgozó_Alap> Adatok = KézDolgozó.Lista_Adatok(Cmbtelephely.Text.Trim());
                if (!Kilépettjel.Checked)
                    Adatok = Adatok.Where(a => a.Kilépésiidő.ToShortDateString() == new DateTime(1900, 1, 1).ToShortDateString()).ToList();

                foreach (Adat_Dolgozó_Alap rekord in Adatok)
                    Dolgozónév.Items.Add(rekord.DolgozóNév.Trim() + " = " + rekord.Dolgozószám.Trim());

                Dolgozónév.EndUpdate();

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

        private void Kilépettjel_CheckedChanged(object sender, EventArgs e)
        {
            Névfeltöltés();
        }

        private void Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.Rows.Count <= 0) return;
                string fájlexc;
                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Beosztás_Napló_" + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                Module_Excel.DataGridViewToExcel(fájlexc, Tábla);

                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Module_Excel.Megnyitás(fájlexc + ".xlsx");
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
