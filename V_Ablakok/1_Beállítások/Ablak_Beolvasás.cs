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
        readonly Kezelő_Alap_Beolvasás KézBeolv = new Kezelő_Alap_Beolvasás();

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

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, SAPTábla, true);
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

        private void CiklusTípusfeltöltés()
        {
            try
            {
                List<Adat_Alap_Beolvasás> AdatokBeolvÖ = KézBeolv.Lista_Adatok();
                List<string> AdatokBeolv = (from a in AdatokBeolvÖ
                                            where a.Törölt == "0"
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
                List<Adat_Alap_Beolvasás> AdatokBeolvÖ = KézBeolv.Lista_Adatok();
                List<Adat_Alap_Beolvasás> AdatokBeolv = (from a in AdatokBeolvÖ
                                                         where a.Csoport == SAPCsoport.Text.Trim()
                                                         && a.Törölt == "0"
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
                SAPTábla.Columns[3].HeaderText = "Beolvassuk";
                SAPTábla.Columns[3].Width = 100;

                foreach (Adat_Alap_Beolvasás rekord in AdatokBeolv)
                {
                    SAPTábla.RowCount++;
                    int i = SAPTábla.RowCount - 1;
                    SAPTábla.Rows[i].Cells[0].Value = rekord.Csoport;
                    SAPTábla.Rows[i].Cells[1].Value = rekord.Oszlop;
                    SAPTábla.Rows[i].Cells[2].Value = rekord.Fejléc;
                    SAPTábla.Rows[i].Cells[3].Value = rekord.Kell;
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
            try
            {
                SAPCsoport.Items.Clear();
                CiklusTípusfeltöltés();
                SAPOSzlopszám.Text = "";
                SAPFejléc.Text = "";
                SAPBeolvassuk.Text = "";
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
                    SAPBeolvassuk.Text = SAPTábla.Rows[SAPTábla.SelectedRows[0].Index].Cells[3].Value.ToStrTrim();
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
                SAPFejléc.Text = MyF.Szöveg_Tisztítás(SAPFejléc.Text);
                SAPBeolvassuk.Text = MyF.Szöveg_Tisztítás(SAPBeolvassuk.Text);

                // leellenőrizzük, hogy minden adat ki van-e töltve
                if ((SAPCsoport.Text.Trim() == "")) return;
                if ((SAPFejléc.Text.Trim() == "")) return;
                if ((SAPBeolvassuk.Text.Trim() == "")) return;
                if (!int.TryParse(SAPOSzlopszám.Text, out int SAPoszlop)) return;
                if (!int.TryParse(SAPBeolvassuk.Text, out int SAPBeolvas)) return;

                List<Adat_Alap_Beolvasás> AdatokBeolv = KézBeolv.Lista_Adatok();

                Adat_Alap_Beolvasás Elem = (from a in AdatokBeolv
                                            where a.Csoport == SAPCsoport.Text.Trim() && a.Oszlop == SAPoszlop && a.Törölt == "0"
                                            select a).FirstOrDefault();

                Adat_Alap_Beolvasás Adat = new Adat_Alap_Beolvasás(SAPCsoport.Text.Trim(),
                                                                   SAPoszlop,
                                                                   SAPFejléc.Text.Trim(),
                                                                   "0",
                                                                   SAPBeolvas);

                if (Elem != null)
                    KézBeolv.Módosítás(Adat);
                else
                    KézBeolv.Rögzítés(Adat);


                Táblaíró();
                MessageBox.Show("Az adat rögzítése megtörtént. ", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                List<Adat_Alap_Beolvasás> AdatokBeolv = KézBeolv.Lista_Adatok();

                Adat_Alap_Beolvasás Elem = (from a in AdatokBeolv
                                            where a.Csoport == SAPCsoport.Text.Trim() && a.Oszlop == SAPoszlop && a.Törölt == "0"
                                            select a).FirstOrDefault();

                if (Elem != null)
                {
                    Adat_Alap_Beolvasás ADAT = new Adat_Alap_Beolvasás(SAPCsoport.Text.Trim(),
                                   SAPoszlop,
                                   "",
                                   "0",
                                   0);
                    // ha van
                    KézBeolv.Törlés(ADAT);
                    Táblaíró();
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


    }
}
