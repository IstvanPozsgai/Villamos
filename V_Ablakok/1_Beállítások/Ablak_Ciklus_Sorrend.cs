using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.V_Ablakok._1_Beállítások
{
    public partial class Ablak_Ciklus_Sorrend : Form
    {
        readonly Kezelő_Ciklus Kéz = new Kezelő_Ciklus();

        List<Adat_Ciklus> Adatok = new List<Adat_Ciklus>();

        public Ablak_Ciklus_Sorrend()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            Adatok = Kéz.Lista_Adatok();
            CiklusTípusfeltöltés();
        }

        private void Ablak_Ciklus_Sorrend_Load(object sender, EventArgs e)
        {

        }

        private void Táblaíró()
        {
            try
            {
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

                //       foreach (Adat_Ciklus rekord in AdatokSzűrt)
                {
                    Tábla.RowCount++;
                    int i = Tábla.RowCount - 1;

                    //Tábla.Rows[i].Cells[0].Value = rekord.Típus;
                    //Tábla.Rows[i].Cells[1].Value = rekord.Sorszám;
                    //Tábla.Rows[i].Cells[2].Value = rekord.Vizsgálatfok;
                    //Tábla.Rows[i].Cells[3].Value = rekord.Névleges;
                    //Tábla.Rows[i].Cells[4].Value = rekord.Alsóérték;
                    //Tábla.Rows[i].Cells[5].Value = rekord.Felsőérték;
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

        private void Rögzítés_Click(object sender, EventArgs e)
        {

        }

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
    }
}
