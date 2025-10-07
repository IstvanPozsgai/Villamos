using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.V_MindenEgyéb
{
    public partial class Ablak_Hibanaplo : Form
    {

        readonly DataTable AdatTábla = new DataTable();
        
        public Ablak_Hibanaplo()
        {
            InitializeComponent();
            Start();
            // var teszt = ReadFileLines($@"{Application.StartupPath}\Főmérnökség\adatok\Hibanapló\hiba2025.csv");                
        }

        private void Ablak_Hibanaplo_Load(object sender, EventArgs e)
        {

        }

        private void Start()
        {
            Fejlec();
        }

        private void Tablalista_kiírás()
        {
            Listazas();
            ABFeltöltése();
            Hibanaplo_Tablazat.CleanFilterAndSort();
            Hibanaplo_Tablazat.DataSource = AdatTábla;
            OszlopSzélesség();
            Hibanaplo_Tablazat.Refresh();
            Hibanaplo_Tablazat.Visible = true;
            Hibanaplo_Tablazat.ClearSelection();
        }

        private void Fejlec()
        {
            // Dátum;Telephely;Felhsználó;Hiba üzenet;Hiba Osztály; Hiba Metódus; Névtér; Egyéb; Dátum
            AdatTábla.Columns.Clear();
            AdatTábla.Columns.Add("Dátum", typeof(DateTime));
            AdatTábla.Columns.Add("Telephely");
            AdatTábla.Columns.Add("Felhasználó");
            AdatTábla.Columns.Add("Hiba üzenet");
            AdatTábla.Columns.Add("Hiba osztály");
            AdatTábla.Columns.Add("Hiba metódus");
            AdatTábla.Columns.Add("Névtér");
            AdatTábla.Columns.Add("Egyéb");
        }

        private void Listazas()
        {

        }

        private void ABFeltöltése()
        {
            // A CSV ANSI kódolásban van, a 1250-es ANSI kódolás tartalmaz ékezeteket.
            string[] betoltott_log = File.ReadAllLines($@"{Application.StartupPath}\Főmérnökség\Adatok\Hibanapló\hiba2025.csv", Encoding.GetEncoding(1250));

            //AdatTábla.Clear();
            //foreach (Adat_CAF_Adatok villamos in CafAdatok)
            //{
            //    DataRow Soradat = AdatTábla.NewRow();
            //    Soradat["ID"] = villamos.Id;
            //    Soradat["Pályaszám"] = villamos.Azonosító;
            //    Soradat["Vizsgálat"] = villamos.Vizsgálat;
            //    Soradat["Dátum"] = villamos.Dátum.ToString("yyyy.MM.dd");
            //    Soradat["Számláló állás"] = villamos.Számláló;
            //    Soradat["Telephely"] = villamos.Telephely;

            //    AdatTábla.Rows.Add(Soradat);
            //}

            AdatTábla.Clear();

        }

        private void OszlopSzélesség()
        {

        }

    }
}
