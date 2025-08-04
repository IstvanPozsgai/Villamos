using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Villamos.V_Kezelők;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.V_Ablakok._7_Gondnokság.Épület_takarítás
{
    public partial class Ablak_BMR : Form
    {
        public DateTime Dátum { get; private set; }
        public bool Jármű { get; private set; }
        public string Telephely { get; private set; }

        readonly Kezelő_Takarítás_BMR KézBMR = new Kezelő_Takarítás_BMR();
        List<Adat_Takarítás_BMR> AdatokBMR = new List<Adat_Takarítás_BMR>();

        readonly DataTable AdatTábla = new DataTable();

        public Ablak_BMR(DateTime dátum, bool jármű, string telephely)
        {
            Dátum = dátum;
            Jármű = jármű;
            Telephely = telephely;
            InitializeComponent();
        }

        private void Ablak_BMR_Load(object sender, EventArgs e)
        {
            if (Jármű)
            {
                this.Text = $"{Dátum.Year} év jármű takarítás BMR számok rögzítése";
                DátumMező.Text = $"{Dátum.Year} év\n jármű takarítás";
            }
            else
            {
                this.Text = $"{Dátum.Year} év épület takarítás BMR számok rögzítése";
                DátumMező.Text = $"{Dátum.Year} év\n épület takarítás";
            }
            BMRListaFeltöltés();
            Táblaírás();
        }

        private void Frissítés_Click(object sender, EventArgs e)
        {
            Táblaírás();
        }

        private void Táblaírás()
        {

            ABFejléc();
            ABFeltöltés();
            Tábla.CleanFilterAndSort();
            Tábla.DataSource = AdatTábla;
            ABOszlopSzélesség();
        }

        private void ABFeltöltés()
        {
            try
            {
                AdatTábla.Clear();
                List<Adat_Takarítás_BMR> Adatok;
                if (Jármű)
                    Adatok = AdatokBMR.Where(a => a.Telephely == Telephely && a.Dátum.Year == Dátum.Year && a.JárműÉpület == "Jármű").ToList();
                else
                    Adatok = AdatokBMR.Where(a => a.Telephely == Telephely && a.Dátum.Year == Dátum.Year && a.JárműÉpület == "Épület").ToList();
                if (Adatok != null && Adatok.Count == 0)
                {
                    ÉvesAlap();
                    if (Jármű)
                        Adatok = AdatokBMR.Where(a => a.Telephely == Telephely && a.Dátum.Year == Dátum.Year && a.JárműÉpület == "Jármű").ToList();
                    else
                        Adatok = AdatokBMR.Where(a => a.Telephely == Telephely && a.Dátum.Year == Dátum.Year && a.JárműÉpület == "Épület").ToList();
                }


                foreach (Adat_Takarítás_BMR rekord in Adatok)
                {
                    DataRow Soradat = AdatTábla.NewRow();
                    Soradat["Sorszám"] = rekord.Id;
                    Soradat["Hónap"] = rekord.Dátum.ToString("yyyy MMMM");
                    Soradat["BMR szám"] = rekord.BMRszám;
                    AdatTábla.Rows.Add(Soradat);
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

        private void ÉvesAlap()
        {
            try
            {

                int Sorszám = 0;
                if (AdatokBMR.Count != 0) Sorszám = AdatokBMR.Max(a => a.Id);

                List<Adat_Takarítás_BMR> AdatokRögz = new List<Adat_Takarítás_BMR>();
                for (int i = 0; i < 12; i++)
                {
                    Adat_Takarítás_BMR Elem = new Adat_Takarítás_BMR(++Sorszám,
                                                                     Telephely,
                                                                     Jármű ? "Jármű" : "Épület",
                                                                     "",
                                                                     new DateTime(Dátum.Year, i + 1, 1));
                    AdatokRögz.Add(Elem);

                }
                if (AdatokRögz.Count > 0) KézBMR.Rögzít(AdatokRögz);
                BMRListaFeltöltés();
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

        private void ABFejléc()
        {
            try
            {
                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Sorszám", typeof(int));
                AdatTábla.Columns.Add("Hónap");
                AdatTábla.Columns.Add("BMR szám");
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

        private void ABOszlopSzélesség()
        {
            Tábla.Columns["Sorszám"].Width = 100;
            Tábla.Columns["Hónap"].Width = 150;
            Tábla.Columns["BMR szám"].Width = 150;

            Tábla.Columns["Sorszám"].ReadOnly = true;
            Tábla.Columns["Hónap"].ReadOnly = true;

        }

        #region Listák
        private void BMRListaFeltöltés()
        {
            try
            {
                AdatokBMR.Clear();
                AdatokBMR = KézBMR.Lista_Adatok();
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

        private void Rögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                List<Adat_Takarítás_BMR> AdatokRögz = new List<Adat_Takarítás_BMR>();
                foreach (DataGridViewRow Sorelem in Tábla.Rows)
                {
                    Adat_Takarítás_BMR Elem = new Adat_Takarítás_BMR(Sorelem.Cells["Sorszám"].Value.ToÉrt_Int(),
                                                  Telephely,
                                                  Jármű ? "Jármű" : "Épület",
                                                  Sorelem.Cells["BMR szám"].Value.ToStrTrim(),
                                                  Dátum);
                    AdatokRögz.Add(Elem);
                }
                if (AdatokRögz.Count > 0) KézBMR.Módosít(AdatokRögz);
                BMRListaFeltöltés();
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
