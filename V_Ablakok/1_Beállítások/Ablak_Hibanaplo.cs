using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos
{
    public partial class Ablak_Hibanaplo : Form
    {

        readonly DataTable AdatTábla = new DataTable();

        public Ablak_Hibanaplo()
        {
            InitializeComponent();            
            Start();
        }

        private void Ablak_Hibanaplo_Load(object sender, EventArgs e)
        {

        }

        private void Start()
        {
            Fejlec();
            Tablalista_kiírás();
        }

        private void Tablalista_kiírás()
        {
            ABFeltöltése();
            Hibanaplo_Tablazat.CleanFilterAndSort();
            Hibanaplo_Tablazat.DataSource = AdatTábla;
            Hibanaplo_Tablazat.Sort(Hibanaplo_Tablazat.Columns["Dátum"], ListSortDirection.Descending);
            OszlopSzélesség();
            Hibanaplo_Tablazat.Refresh();
            Hibanaplo_Tablazat.Visible = true;
            Hibanaplo_Tablazat.ClearSelection();
        }

        private void Fejlec()
        {
            // Dátum;Telephely;Felhsználó;Hiba üzenet;Hiba Osztály; Hiba Metódus; Névtér; Egyéb; Dátum
            AdatTábla.Columns.Clear();
            AdatTábla.Columns.Add("Dátum");
            AdatTábla.Columns.Add("Idő");
            AdatTábla.Columns.Add("Telephely");
            AdatTábla.Columns.Add("Felhasználó");
            AdatTábla.Columns.Add("Hiba üzenet");
            AdatTábla.Columns.Add("Hiba osztály");
            AdatTábla.Columns.Add("Hiba metódus");
            AdatTábla.Columns.Add("Névtér");
            AdatTábla.Columns.Add("Egyéb");
        }

        private void ABFeltöltése()
        {
            AdatTábla.Clear();

            int ideiEv = DateTime.Now.Year;
            int tavalyiEv = ideiEv - 1;

            List<string> osszesSor = new List<string>();

            if (FileLetezik(ideiEv))
            {
                osszesSor.AddRange(evesLogFajltBetolt(ideiEv).Skip(1));
            }
            else
            {
                MessageBox.Show(
                    $"A {ideiEv}. évi hibanapló fájl nem található.\n\n" +
                    $@"Elvárt hely: {Application.StartupPath}\Főmérnökség\Adatok\Hibanapló\hiba{ideiEv}.csv",
                    "Hiányzó fájl",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

            if (FileLetezik(tavalyiEv))
            {
                osszesSor.AddRange(evesLogFajltBetolt(tavalyiEv).Skip(1));
            }
            else
            {
                MessageBox.Show(
                    $"A {tavalyiEv}. évi hibanapló fájl nem található.\n\n" +
                    $@"Elvárt hely: {Application.StartupPath}\Főmérnökség\Adatok\Hibanapló\{tavalyiEv}\hiba{tavalyiEv}.csv",
                    "Hiányzó fájl",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

            foreach (string sor in osszesSor)
            {
                // Dátum;Telephely;Felhsználó;Hiba üzenet;Hiba Osztály; Hiba Metódus; Névtér; Egyéb; Dátum
                DataRow Soradat = AdatTábla.NewRow();
                string[] mezok = sor.Split(';');

                Soradat["Dátum"] = mezok[0].Split(' ')[0];
                Soradat["Idő"] = mezok[0].Split(' ')[1];
                Soradat["Telephely"] = mezok[1];
                Soradat["Felhasználó"] = mezok[2];
                Soradat["Hiba üzenet"] = mezok[3];
                Soradat["Hiba osztály"] = mezok[4];
                Soradat["Hiba metódus"] = mezok[5];
                Soradat["Névtér"] = mezok[6];
                Soradat["Egyéb"] = mezok[7];
                AdatTábla.Rows.Add(Soradat);
            }
        }


        private void OszlopSzélesség()
        {
            Hibanaplo_Tablazat.Columns["Dátum"].Width = 100;
            Hibanaplo_Tablazat.Columns["Idő"].Width = 50;
            Hibanaplo_Tablazat.Columns["Telephely"].Width = 90;
            Hibanaplo_Tablazat.Columns["Felhasználó"].Width = 70;
            Hibanaplo_Tablazat.Columns["Hiba üzenet"].Width = 450;
            Hibanaplo_Tablazat.Columns["Hiba osztály"].Width = 300;
            Hibanaplo_Tablazat.Columns["Hiba metódus"].Width = 300;
            Hibanaplo_Tablazat.Columns["Névtér"].Width = 70;
            Hibanaplo_Tablazat.Columns["Egyéb"].Width = 40;
        }

        private string[] evesLogFajltBetolt(int ev)
        {
            string fajlUtvonal;
            if (ev == DateTime.Now.Year)
            {
                fajlUtvonal = $@"{Application.StartupPath}\Főmérnökség\Adatok\Hibanapló\hiba{ev}.csv";
            }
            else
            {
                fajlUtvonal = $@"{Application.StartupPath}\Főmérnökség\Adatok\Hibanapló\{ev}\hiba{ev}.csv";
            }
            return File.ReadAllLines(fajlUtvonal, Encoding.GetEncoding(1250));
        }

        private bool FileLetezik(int ev)
        {
            string fajlUtvonal;
            if (ev == DateTime.Now.Year)
                fajlUtvonal = $@"{Application.StartupPath}\Főmérnökség\Adatok\Hibanapló\hiba{ev}.csv";
            else
                fajlUtvonal = $@"{Application.StartupPath}\Főmérnökség\Adatok\Hibanapló\{ev}\hiba{ev}.csv";

            return File.Exists(fajlUtvonal);
        }

    }
}
