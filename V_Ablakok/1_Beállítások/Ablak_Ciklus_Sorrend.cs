using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_Kezelők;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.V_Ablakok._1_Beállítások
{
    public partial class Ablak_Ciklus_Sorrend : Form
    {
        readonly Kezelő_Ciklus Kéz = new Kezelő_Ciklus();
        readonly Kezelő_Ciklus_Sorrend KézSorrend = new Kezelő_Ciklus_Sorrend();
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();

        List<Adat_Ciklus> Adatok = new List<Adat_Ciklus>();
        List<Adat_Jármű> Adatok_Állomány = new List<Adat_Jármű>();

        public Ablak_Ciklus_Sorrend()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            Adatok = Kéz.Lista_Adatok();
            Adatok_Állomány = KézJármű.Lista_Adatok("Főmérnökség");
            CiklusTípusfeltöltés();
            Típusfeltöltés();
        }

        private void Ablak_Ciklus_Sorrend_Load(object sender, EventArgs e)
        {

        }


        #region Táblázat
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
        #endregion


        #region Listák Feltöltése
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

        private void Típusfeltöltés()
        {
            try
            {
                List<string> Valóstípus = (from a in Adatok_Állomány
                                           orderby a.Valóstípus
                                           select a.Valóstípus).ToList().Distinct().ToList();

                JárműTípus.Items.Clear();

                foreach (string Elem in Valóstípus)
                {
                    JárműTípus.Items.Add(Elem);
                }

                JárműTípus.Refresh();
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


        #region Gombok
        private void Rögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Sorszám.Text, out int sorszám)) sorszám = -1;
                if (string.IsNullOrWhiteSpace(CiklusTípus.Text)) throw new HibásBevittAdat("A ciklus típus nem lehet üres!");
                if (string.IsNullOrWhiteSpace(JárműTípus.Text)) throw new HibásBevittAdat("A jármű típus nem lehet üres!");
                Adat_Ciklus_Sorrend ADAT = new Adat_Ciklus_Sorrend(sorszám, JárműTípus.Text, CiklusTípus.Text);
                KézSorrend.Döntés(ADAT);
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
                if (!int.TryParse(Sorszám.Text, out int sorszám)) sorszám = -1;
                if (string.IsNullOrWhiteSpace(CiklusTípus.Text)) throw new HibásBevittAdat("A ciklus típus nem lehet üres!");
                if (string.IsNullOrWhiteSpace(JárműTípus.Text)) throw new HibásBevittAdat("A jármű típus nem lehet üres!");
                Adat_Ciklus_Sorrend ADAT = new Adat_Ciklus_Sorrend(sorszám, JárműTípus.Text, CiklusTípus.Text);
                KézSorrend.Törlés(ADAT);
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

        private void BeoFrissít_Click(object sender, EventArgs e)
        {
            Táblaíró();
        }
        #endregion


    }
}
