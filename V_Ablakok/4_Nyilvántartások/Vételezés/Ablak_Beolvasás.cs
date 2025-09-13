using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    public partial class Ablak_Beolvasás : Form
    {
        readonly Kezelő_Excel_Beolvasás KézBeolv = new Kezelő_Excel_Beolvasás();

        public Ablak_Beolvasás()
        {
            InitializeComponent();
            CiklusTípusfeltöltés();
        }

        private void Ablak_Beállítások_Load(object sender, EventArgs e)
        {

        }

        private void Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (SAPTábla.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Beolvasási_adatok_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddhhmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyE.DataGridViewToExcel(fájlexc, SAPTábla);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MyE.Megnyitás(fájlexc);
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

        private void CiklusTípusfeltöltés()
        {
            try
            {
                List<Adat_Excel_Beolvasás> AdatokBeolvÖ = KézBeolv.Lista_Adatok();
                List<string> AdatokBeolv = (from a in AdatokBeolvÖ
                                            where a.Státusz == false
                                            orderby a.Csoport
                                            select a.Csoport).Distinct().ToList();
                SAPCsoport.Items.Clear();
                SAPCsoport.Items.Add("");
                foreach (string elem in AdatokBeolv)
                    SAPCsoport.Items.Add(elem);
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

        private void Csoport_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
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

        private void Táblaíró()
        {
            try
            {
                if (SAPCsoport.Text.Trim() == "") return;
                List<Adat_Excel_Beolvasás> AdatokBeolvÖ = KézBeolv.Lista_Adatok();
                List<Adat_Excel_Beolvasás> AdatokBeolv = (from a in AdatokBeolvÖ
                                                          where a.Csoport == SAPCsoport.Text.Trim()
                                                          && a.Státusz == false
                                                          orderby a.Oszlop
                                                          select a).ToList();
                SAPTábla.Rows.Clear();
                SAPTábla.Columns.Clear();
                SAPTábla.Refresh();
                SAPTábla.Visible = false;
                SAPTábla.ColumnCount = 4;
                SAPTábla.RowCount = 0;
                // ' fejléc elkészítése
                SAPTábla.Columns[0].HeaderText = "Csoport";
                SAPTábla.Columns[0].Width = 100;
                SAPTábla.Columns[1].HeaderText = "Oszlop száma";
                SAPTábla.Columns[1].Width = 100;
                SAPTábla.Columns[2].HeaderText = "Fejléc";
                SAPTábla.Columns[2].Width = 400;
                SAPTábla.Columns[3].HeaderText = "Változónév";
                SAPTábla.Columns[3].Width = 250;

                foreach (Adat_Excel_Beolvasás rekord in AdatokBeolv)
                {
                    SAPTábla.RowCount++;
                    int i = SAPTábla.RowCount - 1;
                    SAPTábla.Rows[i].Cells[0].Value = rekord.Csoport;
                    SAPTábla.Rows[i].Cells[1].Value = rekord.Oszlop;
                    SAPTábla.Rows[i].Cells[2].Value = rekord.Fejléc;
                    SAPTábla.Rows[i].Cells[3].Value = rekord.Változónév;
                }
                SAPTábla.Refresh();
                SAPTábla.Visible = true;
                SAPTábla.ClearSelection();
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

        private void Command1_Click(object sender, EventArgs e)
        {
            Listázás();
        }

        private void Listázás()
        {
            try
            {
                SAPCsoport.Items.Clear();
                CiklusTípusfeltöltés();
                SAPOSzlopszám.Text = "";
                SAPFejléc.Text = "";
                Változónév.Text = "";
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

        private void Tábla_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (SAPTábla.SelectedRows.Count != 0)
                {
                    SAPOSzlopszám.Text = SAPTábla.Rows[SAPTábla.SelectedRows[0].Index].Cells[1].Value.ToStrTrim();
                    SAPFejléc.Text = SAPTábla.Rows[SAPTábla.SelectedRows[0].Index].Cells[2].Value.ToStrTrim();
                    Változónév.Text = SAPTábla.Rows[SAPTábla.SelectedRows[0].Index].Cells[3].Value.ToStrTrim();
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

        private void Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                SAPCsoport.Text = MyF.Szöveg_Tisztítás(SAPCsoport.Text);
                SAPFejléc.Text = SAPFejléc.Text.Trim();
                Változónév.Text = MyF.Szöveg_Tisztítás(Változónév.Text);

                // leellenőrizzük, hogy minden adat ki van-e töltve
                if ((SAPCsoport.Text.Trim() == "")) return;
                if ((SAPFejléc.Text.Trim() == "")) return;
                if ((Változónév.Text.Trim() == "")) return;             //Méretre vágjuk
                if (!int.TryParse(SAPOSzlopszám.Text, out int SAPoszlop)) return;

                List<Adat_Excel_Beolvasás> AdatokBeolv = KézBeolv.Lista_Adatok();

                Adat_Excel_Beolvasás Elem = (from a in AdatokBeolv
                                             where a.Csoport == SAPCsoport.Text.Trim() && a.Oszlop == SAPoszlop && a.Státusz == false
                                             select a).FirstOrDefault();

                Adat_Excel_Beolvasás Adat = new Adat_Excel_Beolvasás(SAPCsoport.Text.Trim(),
                                                                   SAPoszlop,
                                                                   SAPFejléc.Text.Trim(),
                                                                   false,
                                                                   MyF.Szöveg_Tisztítás(Változónév.Text, 0, 50));

                if (Elem != null)
                    KézBeolv.Módosítás(Adat);
                else
                    KézBeolv.Rögzítés(Adat);


                Listázás();
                CiklusTípusfeltöltés();
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
                if (SAPCsoport.Text.Trim() == "") throw new HibásBevittAdat("Beolvasási mező nincs kiválasztva.");
                if (SAPOSzlopszám.Text.Trim() == "") throw new HibásBevittAdat("Oszlop száma mező nincs kötöltve.");
                if (!int.TryParse(SAPOSzlopszám.Text, out int SAPoszlop)) throw new HibásBevittAdat("Oszlop száma mezőnek egész számnak kell lennie.");

                List<Adat_Excel_Beolvasás> AdatokBeolv = KézBeolv.Lista_Adatok();

                Adat_Excel_Beolvasás Elem = (from a in AdatokBeolv
                                             where a.Csoport == SAPCsoport.Text.Trim() && a.Oszlop == SAPoszlop && a.Státusz == false
                                             select a).FirstOrDefault();

                if (Elem != null)
                {
                    Adat_Excel_Beolvasás ADAT = new Adat_Excel_Beolvasás(SAPCsoport.Text.Trim(),
                                   SAPoszlop,
                                   "",
                                   false,
                                   "0");
                    // ha van
                    KézBeolv.Törlés(ADAT);
                    Listázás();
                    MessageBox.Show("Az adat törlése megtörtént. ", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void SAPTábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SAPTábla.Rows[e.RowIndex].Selected = true;
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
        /// Beolvassa az excel fájlt és a fejlécet beírja a kiválasztott csoportba
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FejlécBeolvasása_Click(object sender, EventArgs e)
        {
            try
            {
                if (SAPCsoport.Text.Trim() == "") throw new HibásBevittAdat("Beolvasási mező nincs kiválasztva.");
                if (SAPCsoport.Items.Contains(SAPCsoport.Text.Trim())) throw new HibásBevittAdat("Van már ilyen csoport létrehozva.");
                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "IDM-s Adatok betöltése",
                    FileName = "",
                    Filter = "Excel 97-2003 (*.xls)|*.xls|Excel (*.xlsx)|*.xlsx"
                };
                string fájlexc;
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;

                DataTable Tábla = MyF.Excel_Tábla_Beolvas(fájlexc);

                List<Adat_Excel_Beolvasás> AdatokGy = new List<Adat_Excel_Beolvasás>();
                for (int i = 0; i < Tábla.Columns.Count; i++)
                {
                    Adat_Excel_Beolvasás ADAT = new Adat_Excel_Beolvasás(
                           SAPCsoport.Text.Trim(),
                           i + 1,
                           Tábla.Columns[i].ColumnName.ToStrTrim(),
                           false,
                           "0");
                    AdatokGy.Add(ADAT);
                }
                KézBeolv.Rögzítés(AdatokGy);
                Listázás();
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
