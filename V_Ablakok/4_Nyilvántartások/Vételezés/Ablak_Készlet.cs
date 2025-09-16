using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Villamos.V_Adatszerkezet;

namespace Villamos.V_Ablakok._4_Nyilvántartások.Vételezés
{
    public partial class Ablak_Készlet : Form
    {
        List<Adat_Raktár> Adatok { get; set; }
        string Cikkszám { get; set; }
        readonly DataTable AdatTábla = new DataTable();
        public Ablak_Készlet(List<Adat_Raktár> adatok, string cikkszám)
        {
            Adatok = adatok;
            Cikkszám = cikkszám;
            InitializeComponent();
            this.Text = $"Készlet ({Cikkszám})";
        }

        private void Ablak_Készlet_Load(object sender, EventArgs e)
        {
            Fejléc();
            ABFeltöltése();
            Tábla.DataSource = AdatTábla;
            OszlopSzélesség();
        }

        private void Fejléc()
        {
            try
            {
                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Cikkszám");
                AdatTábla.Columns.Add("Raktárhely");
                AdatTábla.Columns.Add("Készlet", typeof(double));
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

        private void OszlopSzélesség()
        {
            Tábla.Columns["Cikkszám"].Width = 130;
            Tábla.Columns["Raktárhely"].Width = 100;
            Tábla.Columns["Készlet"].Width = 100;
        }

        private void ABFeltöltése()
        {
            try
            {
                AdatTábla.Clear();
                foreach (Adat_Raktár rekord in Adatok)
                {
                    DataRow Soradat = AdatTábla.NewRow();
                    Soradat["Cikkszám"] = rekord.Cikkszám;
                    Soradat["Raktárhely"] = rekord.Raktárhely;
                    Soradat["Készlet"] = rekord.Mennyiség;
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
    }
}
