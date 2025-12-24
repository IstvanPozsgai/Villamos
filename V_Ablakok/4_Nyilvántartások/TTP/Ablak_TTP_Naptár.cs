using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Adatszerkezet;
using MyF = Függvénygyűjtemény;


namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.TTP
{
    public partial class Ablak_TTP_Naptár : Form
    {
        readonly Kezelő_TTP_Naptár KézNaptár = new Kezelő_TTP_Naptár();
        readonly Kezelő_Váltós_Naptár KézVálNaptár = new Kezelő_Váltós_Naptár();

        List<Adat_TTP_Naptár> NaptárLista = new List<Adat_TTP_Naptár>();

        public Ablak_TTP_Naptár()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            Dátum.Value = DateTime.Today;
            TáblaListázás();
        }

        private void Ablak_TTP_Naptár_Load(object sender, EventArgs e)
        { }

        private void Adatok_Áttöltése()
        {
            try
            {
                List<Adat_Váltós_Naptár> Adatok = KézVálNaptár.Lista_Adatok(Dátum.Value.Year, "");

                NaptárLista.Clear();
                NaptárLista = KézNaptár.Lista_Adatok();
                NaptárLista = (from a in NaptárLista
                               where a.Dátum >= MyF.Év_elsőnapja(Dátum.Value) && a.Dátum <= MyF.Év_utolsónapja(Dátum.Value)
                               orderby a.Dátum
                               select a).ToList();

                List<Adat_TTP_Naptár> AdatokGy = new List<Adat_TTP_Naptár>();
                foreach (Adat_Váltós_Naptár rekord in Adatok)
                {
                    Adat_TTP_Naptár Elem = (from a in NaptárLista
                                            where a.Dátum == rekord.Dátum
                                            select a).FirstOrDefault();
                    if (Elem == null)
                    {
                        Adat_TTP_Naptár ADAT = new Adat_TTP_Naptár(rekord.Dátum, rekord.Nap == "1");
                        AdatokGy.Add(ADAT);
                    }
                }
                KézNaptár.Rögzítés(AdatokGy);
                NaptárLista = KézNaptár.Lista_Adatok();
                NaptárLista = (from a in NaptárLista
                               where a.Dátum >= MyF.Év_elsőnapja(Dátum.Value) && a.Dátum <= MyF.Év_utolsónapja(Dátum.Value)
                               orderby a.Dátum
                               select a).ToList();
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
                NaptárLista = KézNaptár.Lista_Adatok();
                NaptárLista = (from a in NaptárLista
                               where a.Dátum >= MyF.Év_elsőnapja(Dátum.Value) && a.Dátum <= MyF.Év_utolsónapja(Dátum.Value)
                               orderby a.Dátum
                               select a).ToList();
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
                    Adat_TTP_Naptár ADAT = new Adat_TTP_Naptár(Dátum.Value, ChkMunkanap.Checked);
                    KézNaptár.Módosítás(ADAT);
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

        private void Button1_Click(object sender, EventArgs e)
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
