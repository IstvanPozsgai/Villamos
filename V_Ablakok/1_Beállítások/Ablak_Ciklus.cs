using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    public partial class Ablak_Ciklus
    {
        readonly Kezelő_Ciklus Kéz = new Kezelő_Ciklus();

        List<Adat_Ciklus> Adatok = new List<Adat_Ciklus>();

        public Ablak_Ciklus()
        {
            InitializeComponent();
        }

        private void Ablak_Ciklus_Load(object sender, EventArgs e)
        {
            Adatok = Kéz.Lista_Adatok();
            CiklusTípusfeltöltés();
            GombLathatosagKezelo.Beallit(this);
            Jogosultságkiosztás();
        }


        #region ALAP
        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\ciklus.html";
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

        private void Jogosultságkiosztás()
        {
            int melyikelem;

            // ide kell az összes gombot tenni amit szabályozni akarunk false
            Töröl.Enabled = false;
            Rögzít.Enabled = false;
            CsoportosMásolás.Enabled = false;
            if (Program.PostásTelephely.Trim() == "Főmérnökség")
            {
                Töröl.Visible = true;
                Rögzít.Visible = true;
                CsoportosMásolás.Visible = true;
            }
            else
            {
                Töröl.Visible = false;
                Rögzít.Visible = false;
                CsoportosMásolás.Visible = false;
            }

            melyikelem = 7;
            // módosítás 1 
            if (MyF.Vanjoga(melyikelem, 1))
            {
                Töröl.Enabled = true;
                Rögzít.Enabled = true;
                CsoportosMásolás.Enabled = true;
            }
            // módosítás 2
            if (MyF.Vanjoga(melyikelem, 2))
            {

            }
            // módosítás 3 
            if (MyF.Vanjoga(melyikelem, 3))
            {

            }
        }

        private void Excel_gomb_Click(object sender, EventArgs e)
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
                    FileName = $"Ciklusok_listája_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Tábla, false);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MyE.Megnyitás(fájlexc + ".xlsx");
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


        private void CiklusTípusfeltöltés()
        {
            try
            {
                CiklusTípus.Items.Clear();
                List<string> SzűrtAdatok = (from a in Adatok
                                            where a.Törölt == "0"
                                            orderby a.Típus
                                            select a.Típus).ToList().Distinct().ToList();
                foreach (string elem in SzűrtAdatok)
                    CiklusTípus.Items.Add(elem);
                CiklusTípus.Refresh();
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

        private void CiklusTípus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Táblaíró();
            Vizsálatsorszám.Text = "";
            Vizsgálatfoka.Text = "";
        }

        private void Táblaíró()
        {
            try
            {
                if (CiklusTípus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva ciklus");
                Adatok = Kéz.Lista_Adatok();

                List<Adat_Ciklus> AdatokSzűrt = (from a in Adatok
                                                 where a.Törölt == "0" && a.Típus == CiklusTípus.Text.Trim()
                                                 orderby a.Sorszám
                                                 select a).ToList();
                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 6;

                // fejléc elkészítése 
                Tábla.Columns[0].HeaderText = "Ciklus Típus";
                Tábla.Columns[0].Width = 120;
                Tábla.Columns[1].HeaderText = "Sorszám";
                Tábla.Columns[1].Width = 80;
                Tábla.Columns[2].HeaderText = "Vizsgálat";
                Tábla.Columns[2].Width = 200;
                Tábla.Columns[3].HeaderText = "Névleges";
                Tábla.Columns[3].Width = 150;
                Tábla.Columns[4].HeaderText = "Alsó eltérés";
                Tábla.Columns[4].Width = 150;
                Tábla.Columns[5].HeaderText = "Felső eltérés";
                Tábla.Columns[5].Width = 150;

                foreach (Adat_Ciklus rekord in AdatokSzűrt)
                {
                    Tábla.RowCount++;
                    int i = Tábla.RowCount - 1;

                    Tábla.Rows[i].Cells[0].Value = rekord.Típus;
                    Tábla.Rows[i].Cells[1].Value = rekord.Sorszám;
                    Tábla.Rows[i].Cells[2].Value = rekord.Vizsgálatfok;
                    Tábla.Rows[i].Cells[3].Value = rekord.Névleges;
                    Tábla.Rows[i].Cells[4].Value = rekord.Alsóérték;
                    Tábla.Rows[i].Cells[5].Value = rekord.Felsőérték;
                }

                Tábla.Visible = true;
                Tábla.Refresh();
                Tábla.ClearSelection();

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

        private void Lekérdezés_lekérdezés_Click(object sender, EventArgs e)
        {
            CiklusTípusfeltöltés();
            Táblaíró();
        }

        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                int i = e.RowIndex;

                // kiirjuk az adatokat.
                CiklusTípus.Text = Tábla.Rows[i].Cells[0].Value.ToStrTrim();
                Vizsálatsorszám.Text = Tábla.Rows[i].Cells[1].Value.ToStrTrim();
                Vizsgálatfoka.Text = Tábla.Rows[i].Cells[2].Value.ToStrTrim();
                Névleges.Text = Tábla.Rows[i].Cells[3].Value.ToStrTrim();
                Alsóeltérés.Text = Tábla.Rows[i].Cells[4].Value.ToStrTrim();
                Felsőeltérés.Text = Tábla.Rows[i].Cells[5].Value.ToStrTrim();
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

        private void Rögzít_Click(object sender, EventArgs e)
        {
            try
            {

                // leellenőrizzük, hogy minden adat ki van-e töltve
                if (CiklusTípus.Text.Trim() == "") throw new HibásBevittAdat("A ciklus rend mezőt ki kell tölteni.");
                if (!long.TryParse(Vizsálatsorszám.Text, out long Sorszám)) throw new HibásBevittAdat("A vizsgálat sorszám mezőt ki kell tölteni és egész számnak kell lennie.");
                if (Vizsgálatfoka.Text.Trim() == "") throw new HibásBevittAdat("A vizsgálat foka mezőt ki kell tölteni.");
                if (!int.TryParse(Névleges.Text, out int NévlegesÉ)) throw new HibásBevittAdat("A névleges érték mezőt ki kell tölteni és egész számnak kell lennie.");
                if (!int.TryParse(Alsóeltérés.Text, out int AlsóÉ)) throw new HibásBevittAdat("A alsó eltérés érték mezőt ki kell tölteni és egész számnak kell lennie.");
                if (!int.TryParse(Felsőeltérés.Text, out int FelsőÉ)) throw new HibásBevittAdat("A felső eltérés érték mezőt ki kell tölteni és egész számnak kell lennie.");

                Adat_Ciklus Elem = (from a in Adatok
                                    where a.Típus == CiklusTípus.Text.Trim() && a.Sorszám == Sorszám && a.Törölt == "0"
                                    select a).FirstOrDefault();

                Adat_Ciklus ADAT = new Adat_Ciklus(CiklusTípus.Text.Trim(),
                                                   Sorszám,
                                                   Vizsgálatfoka.Text.Trim(),
                                                   "0",
                                                   NévlegesÉ,
                                                   AlsóÉ,
                                                   FelsőÉ);

                if (Elem == null)
                    Kéz.Rögzítés(ADAT);
                else
                    Kéz.Módosítás(ADAT);

                MessageBox.Show("Az adatok rögzítése megtörtént !", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Táblaíró();
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

        private void Töröl_Click(object sender, EventArgs e)
        {
            try
            {
                // leellenőrizzük, hogy minden adat ki van-e töltve
                if (CiklusTípus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva ciklus.");
                if (!long.TryParse(Vizsálatsorszám.Text, out long Sorszám)) throw new HibásBevittAdat("Nincs kiválasztva a sorszám.");

                Adat_Ciklus Elem = (from a in Adatok
                                    where a.Típus == CiklusTípus.Text.Trim() && a.Sorszám == Sorszám && a.Törölt == "0"
                                    select a).FirstOrDefault();

                Adat_Ciklus ADAT = new Adat_Ciklus(CiklusTípus.Text.Trim(),
                                                   Sorszám,
                                                   "",
                                                   "0",
                                                   0,
                                                   0,
                                                   0);

                if (Elem != null)
                {
                    Kéz.Töröl(ADAT);
                    MessageBox.Show("Az adatok törlése megtörtént !", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Táblaíró();
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



        private void CsoportosMásolás_Click(object sender, EventArgs e)
        {

            try
            {
                if (CiklusTípus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva másolandó ciklus rend.");
                if (ÚjCiklus.Text.Trim() == "") throw new HibásBevittAdat("Nincs megadva az új másolandó ciklus rend neve.");
                for (int j = 0; j < CiklusTípus.Items.Count; j++)
                    if (CiklusTípus.Items[j].ToString().Trim() == ÚjCiklus.Text.Trim()) throw new HibásBevittAdat("Van már ilyen ciklusrend.");

                List<Adat_Ciklus> SzűrtAdatok = (from a in Adatok
                                                 where a.Típus == CiklusTípus.Text.Trim() && a.Törölt == "0"
                                                 select a).ToList();

                List<Adat_Ciklus> ADATGy = new List<Adat_Ciklus>();
                int i = 0;

                foreach (Adat_Ciklus rekord in SzűrtAdatok)
                {
                    Adat_Ciklus ADAT = new Adat_Ciklus(ÚjCiklus.Text.Trim(),
                                                        i,
                                                        rekord.Vizsgálatfok,
                                                        "0",
                                                        rekord.Névleges,
                                                        rekord.Alsóérték,
                                                        rekord.Felsőérték);
                    ADATGy.Add(ADAT);
                    i++;
                }
                Kéz.Rögzítés(ADATGy);
                MessageBox.Show("Az adatok rögzítése megtörtént !", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Adatok = Kéz.Lista_Adatok();
                CiklusTípusfeltöltés();
                CiklusTípus.Text = ÚjCiklus.Text.Trim();
                ÚjCiklus.Text = "";
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