using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;
using MyA = Adatbázis;

namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.TTP
{
    public partial class Ablak_TTP_Év : Form
    {

        readonly string hely = $@"{Application.StartupPath}/Főmérnökség/adatok/TTP/TTP_Adatbázis.mdb";
        readonly string jelszó = "rudolfg";

        List<Adat_TTP_Év> AdatokÉv = new List<Adat_TTP_Év>();

        public Ablak_TTP_Év()
        {
            InitializeComponent();
        }

        private void Ablak_TTP_Év_Load(object sender, EventArgs e)
        {
            ÉvListáz();
        }

        private void Btn_TTP_Rögz_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(TxtBxÉletkor.Text, out int életkor)) throw new HibásBevittAdat("Nem egész szám az életkor.");
                if (!int.TryParse(TxtBxÉv.Text, out int év)) throw new HibásBevittAdat("Nem egész szám az év.");
                string szöveg;
                if (AdatokÉv.Count == 0)
                {
                    szöveg = $"INSERT INTO TTP_Év (Életkor, Év) ";
                    szöveg += "VALUES (";
                    szöveg += $" {TxtBxÉletkor.Text}, " ;
                    szöveg += $" {TxtBxÉv.Text} )";
                }
                else
                {
                    Adat_TTP_Év Életkorr = (from a in AdatokÉv
                                            where a.Életkor == életkor
                                            select a).FirstOrDefault();

                    if (Életkorr != null)
                    {
                        szöveg = $"UPDATE TTP_Év SET Év={TxtBxÉv.Text}";
                        szöveg += $" WHERE Életkor={TxtBxÉletkor.Text}";
                    }
                    else
                    {
                        szöveg = $"INSERT INTO TTP_Év (Életkor, Év) ";
                        szöveg += "VALUES (";
                        szöveg += $" {TxtBxÉletkor.Text}, " ;
                        szöveg += $" {TxtBxÉv.Text} )";
                    }
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);
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
   
        private void ÉvListáz()
        {
            try
            {
                AdatokÉv = MyF.TTP_ÉvFeltölt();
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

        private void Tábla_Év_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            TxtBxÉletkor.Text = Tábla_Év.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtBxÉv.Text = Tábla_Év.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void BtnTöröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(TxtBxÉletkor.Text, out int életkoreredmény)) return;

                string szöveg = $"DELETE FROM TTP_Év WHERE Életkor={életkoreredmény}";
                MyA.ABtörlés(hely, jelszó, szöveg);
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
