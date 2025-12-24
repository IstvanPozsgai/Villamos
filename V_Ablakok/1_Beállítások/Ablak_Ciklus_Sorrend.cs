using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_Kezelők;


namespace Villamos.V_Ablakok._1_Beállítások
{
    public partial class Ablak_Ciklus_Sorrend : Form
    {
        readonly Kezelő_Ciklus KézCiklus = new Kezelő_Ciklus();
        readonly Kezelő_Ciklus_Sorrend KézSorrend = new Kezelő_Ciklus_Sorrend();
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();

        List<Adat_Ciklus> AdatokCiklus = new List<Adat_Ciklus>();
        List<Adat_Jármű> Adatok_Állomány = new List<Adat_Jármű>();

        public Ablak_Ciklus_Sorrend()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            AdatokCiklus = KézCiklus.Lista_Adatok();
            Adatok_Állomány = KézJármű.Lista_Adatok("Főmérnökség");
            CiklusTípusfeltöltés();
            Típusfeltöltés();
            Táblaíró();

            //Ha az első karakter "R" akkor az új jogosultságkiosztást használjuk
            //ha nem akkor a régit használjuk
            if (Program.PostásJogkör.Substring(0, 1) == "R")
                GombLathatosagKezelo.Beallit(this, "Főmérnökség");
            else
            { }
        }

        private void Ablak_Ciklus_Sorrend_Load(object sender, EventArgs e)
        {

        }


        #region Táblázat
        private void Táblaíró()
        {
            try
            {
                List<Adat_Ciklus_Sorrend> Adatok = KézSorrend.Lista_Adatok();
                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 3;

                // fejléc elkészítése 
                Tábla.Columns[0].HeaderText = "Sorszám";
                Tábla.Columns[0].Width = 80;
                Tábla.Columns[1].HeaderText = "Jármű típus";
                Tábla.Columns[1].Width = 200;
                Tábla.Columns[2].HeaderText = "Ciklus típus";
                Tábla.Columns[2].Width = 200;

                foreach (Adat_Ciklus_Sorrend rekord in Adatok)
                {
                    Tábla.RowCount++;
                    int i = Tábla.RowCount - 1;

                    Tábla.Rows[i].Cells[0].Value = rekord.Sorszám;
                    Tábla.Rows[i].Cells[1].Value = rekord.JárműTípus;
                    Tábla.Rows[i].Cells[2].Value = rekord.CiklusNév;
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

        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return; // Ellenőrizzük, hogy érvényes sorra kattintottunk-e

                Sorszám.Text = Tábla.Rows[e.RowIndex].Cells[0].Value.ToString();
                CiklusTípus.Text = Tábla.Rows[e.RowIndex].Cells[2].Value.ToString();
                JárműTípus.Text = Tábla.Rows[e.RowIndex].Cells[1].Value.ToString();
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
                List<string> SzűrtAdatok = (from a in AdatokCiklus
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
                Táblaíró();
                MezőkŰrítése();
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
                Táblaíró();
                MezőkŰrítése();
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

        private void BeoÚj_Click(object sender, EventArgs e)
        {
            MezőkŰrítése();
        }

        private void MezőkŰrítése()
        {
            Sorszám.Text = "";
            CiklusTípus.Text = "";
            JárműTípus.Text = "";
        }
        #endregion
    }
}
