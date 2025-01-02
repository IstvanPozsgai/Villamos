using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;
using MyA = Adatbázis;


namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.TTP
{
    public partial class Ablak_TTP_Naptár : Form
    {
        List<Adat_TTP_Naptár> NaptárLista = new List<Adat_TTP_Naptár>();
        readonly string Hely = $@"{Application.StartupPath}/Főmérnökség/adatok/TTP/TTP_Adatbázis.mdb";
        readonly string Jelszó = "rudolfg";

        public Ablak_TTP_Naptár()
        {
            InitializeComponent();
        }

        private void Ablak_TTP_Naptár_Load(object sender, EventArgs e)
        {
            Dátum.Value = DateTime.Today;
                  TáblaListázás();
        }

        private void Adatok_Áttöltése()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value.Year}\munkaidőnaptár.mdb";
                string jelszó = "katalin";
                string szöveg = "SELECT * FROM naptár ";

                Kezelő_Váltós_Naptár Kéz = new Kezelő_Váltós_Naptár();
                List<Adat_Váltós_Naptár> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                NaptárLista.Clear();
                NaptárLista = MyF.TTP_NaptárFeltölt(Dátum.Value);

                List<string> szövegGy = new List<string>();
                foreach (Adat_Váltós_Naptár rekord in Adatok)
                {
                    Adat_TTP_Naptár Elem = (from a in NaptárLista
                                            where a.Dátum == rekord.Dátum
                                            select a).FirstOrDefault();
                    if (Elem == null)
                    {
                        szöveg = "INSERT INTO TTP_Naptár (Dátum, Munkanap) VALUES (";
                        szöveg += $"'{rekord.Dátum.ToShortDateString()}', {rekord.Nap == "1"})";
                        szövegGy.Add(szöveg);
                    }
                }
                MyA.ABMódosítás(Hely, Jelszó, szövegGy);
                NaptárLista = MyF.TTP_NaptárFeltölt(Dátum.Value);
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

        private void TáblaListázás()
        {
            try
            {
                NaptárLista.Clear();
                NaptárLista = MyF.TTP_NaptárFeltölt(Dátum.Value);
                TáblaNaptár.Rows.Clear();
                TáblaNaptár.Columns.Clear();
                TáblaNaptár.Refresh();
                TáblaNaptár.Visible = false;
                TáblaNaptár.ColumnCount = 2;

                TáblaNaptár.Columns[0].HeaderText = "Dátum";
                TáblaNaptár.Columns[0].Width = 110;
                TáblaNaptár.Columns[1].HeaderText = "Munkanap";
                TáblaNaptár.Columns[1].Width = 90;


                for (int i = 0; i < NaptárLista.Count; i++)
                {
                    TáblaNaptár.RowCount++;
                    TáblaNaptár.Rows[i].Cells[0].Value = NaptárLista[i].Dátum.ToShortDateString();

                    if (!NaptárLista[i].Munkanap)
                    {
                        TáblaNaptár.Rows[i].Cells[1].Style.BackColor = Color.Red;
                        TáblaNaptár.Rows[i].Cells[1].Value = "Nem";
                    }
                    else
                    {
                        TáblaNaptár.Rows[i].Cells[1].Style.BackColor = Color.Green;
                        TáblaNaptár.Rows[i].Cells[1].Value = "Igen";

                    }
                }


                TáblaNaptár.Refresh();
                TáblaNaptár.Visible = true;
                TáblaNaptár.ClearSelection();
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

        private void TáblaNaptár_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                Dátum.Value = TáblaNaptár.Rows[e.RowIndex].Cells[0].Value.ToÉrt_DaTeTime();
                if (TáblaNaptár.Rows[e.RowIndex].Cells[1].Value.ToStrTrim() == "Igen")
                    ChkMunkanap.Checked = true;
                else
                    ChkMunkanap.Checked = false;

                TáblaNaptár.Rows[e.RowIndex].Selected = true;
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

        private void BtnRögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (TáblaNaptár.SelectedRows.Count < 1) return;

                if (Dátum.Value == TáblaNaptár.Rows[TáblaNaptár.SelectedRows[0].Index].Cells[0].Value.ToÉrt_DaTeTime())
                {
                    string szöveg = $"UPDATE TTP_Naptár SET Munkanap={ChkMunkanap.Checked} WHERE Dátum=#{Dátum.Value:MM-dd-yyyy}# ";
                    MyA.ABMódosítás(Hely, Jelszó, szöveg);
                    TáblaListázás();
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

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            Adatok_Áttöltése();
            TáblaListázás();
            button1.Visible = true;
        }

        private void Dátum_ValueChanged(object sender, EventArgs e)
        {
            TáblaListázás();
        }
    }
}
