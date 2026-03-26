using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Adatszerkezet;

namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.TTP
{
    public partial class Ablak_TTP_Év : Form
    {
        readonly Kezelő_TTP_Év Kéz = new Kezelő_TTP_Év();

        List<Adat_TTP_Év> AdatokÉv = new List<Adat_TTP_Év>();

        public Ablak_TTP_Év()
        {
            InitializeComponent();
        }

        private void Ablak_TTP_Év_Load(object sender, EventArgs e)
        {
            ÉvListáz();
        }


        /// <summary>
        /// Rögzíti vagy módosítja az adatokat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_TTP_Rögz_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(TxtBxÉletkor.Text, out int életkor)) throw new HibásBevittAdat("Nem egész szám az életkor.");
                if (!int.TryParse(TxtBxÉv.Text, out int év)) throw new HibásBevittAdat("Nem egész szám az év.");
                Adat_TTP_Év ADAT = new Adat_TTP_Év(életkor, év);
                if (AdatokÉv.Count == 0)
                    Kéz.Rögzítés(ADAT);
                else
                {
                    Adat_TTP_Év Életkorr = (from a in AdatokÉv
                                            where a.Életkor == életkor
                                            select a).FirstOrDefault();

                    if (Életkorr != null)
                        Kéz.Módosítás(ADAT);
                    else
                        Kéz.Rögzítés(ADAT);
                }
                ÉvListáz();
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
        /// Kiírja a TTP_Év táblában lévő adatokat a DataGridView-be
        /// </summary>
        private void ÉvListáz()
        {
            try
            {
                AdatokÉv = Kéz.Lista_Adatok();
                Tábla_Év.Rows.Clear();
                Tábla_Év.Columns.Clear();
                Tábla_Év.Refresh();
                Tábla_Év.Visible = false;
                Tábla_Év.ColumnCount = 2;

                Tábla_Év.Columns[0].HeaderText = "Életkor";
                Tábla_Év.Columns[0].Width = 80;

                Tábla_Év.Columns[1].HeaderText = "Év";
                Tábla_Év.Columns[1].Width = 80;

                foreach (Adat_TTP_Év rekord in AdatokÉv)
                {
                    Tábla_Év.RowCount++;
                    int i = Tábla_Év.Rows.Count - 1;
                    Tábla_Év.Rows[i].Cells[0].Value = rekord.Életkor;
                    Tábla_Év.Rows[i].Cells[1].Value = rekord.Év;
                }
                Tábla_Év.Visible = true;
                Tábla_Év.ClearSelection();
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
        /// Kiválaszt egy sort a DataGridView-ből, és beírja az értékeket a megfelelő TextBox-okba
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tábla_Év_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            TxtBxÉletkor.Text = Tábla_Év.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtBxÉv.Text = Tábla_Év.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        /// <summary>
        /// Törls gomb eseménykezelője, amely törli a kiválasztott életkort az adatbázisból
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTöröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(TxtBxÉletkor.Text, out int életkoreredmény)) return;
                Kéz.Törlés(életkoreredmény);

                ÉvListáz();
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
