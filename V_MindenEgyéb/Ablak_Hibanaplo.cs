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

namespace Villamos.V_MindenEgyéb
{
    public partial class Ablak_Hibanaplo : Form
    {

        readonly DataTable AdatTábla = new DataTable();
        
        public Ablak_Hibanaplo()
        {
            InitializeComponent();
            cmb_valaszthato_evek.Items.AddRange(KorabbiEvek());
            Start();
        }

        private void Ablak_Hibanaplo_Load(object sender, EventArgs e)
        {

        }

        private void Start()
        {
            Fejlec();

            int ev = cmb_valaszthato_evek.SelectedItem != null
                     ? cmb_valaszthato_evek.SelectedItem.ToÉrt_Int()
                     : DateTime.Now.Year;

            Tablalista_kiírás(ev);
        }

        private void Tablalista_kiírás(int kiirasEv)
        {
            ABFeltöltése(evesLogFajltBetolt(kiirasEv));
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
            AdatTábla.Columns.Add("Dátum");
            AdatTábla.Columns.Add("Telephely");
            AdatTábla.Columns.Add("Felhasználó");
            AdatTábla.Columns.Add("Hiba üzenet");
            AdatTábla.Columns.Add("Hiba osztály");
            AdatTábla.Columns.Add("Hiba metódus");
            AdatTábla.Columns.Add("Névtér");
            AdatTábla.Columns.Add("Egyéb");
        }

        private void ABFeltöltése(string[] betoltott_log)
        {
            // A CSV ANSI kódolásban van, a 1250-es ANSI kódolás tartalmaz ékezeteket.            

            AdatTábla.Clear();
            foreach (string sor in betoltott_log.Skip(1))
            {
                // Dátum;Telephely;Felhsználó;Hiba üzenet;Hiba Osztály; Hiba Metódus; Névtér; Egyéb; Dátum
                DataRow Soradat = AdatTábla.NewRow();
                Soradat["Dátum"] = sor.Split(';')[0];
                Soradat["Telephely"] = sor.Split(';')[1];
                Soradat["Felhasználó"] = sor.Split(';')[2];
                Soradat["Hiba üzenet"] = sor.Split(';')[3];
                Soradat["Hiba osztály"] = sor.Split(';')[4];
                Soradat["Hiba metódus"] = sor.Split(';')[5];
                Soradat["Névtér"] = sor.Split(';')[6];
                Soradat["Egyéb"] = sor.Split(';')[7];
                AdatTábla.Rows.Add(Soradat);
            }                      
        }

        private void OszlopSzélesség()
        {
            Hibanaplo_Tablazat.Columns["Dátum"].Width = 150;
            Hibanaplo_Tablazat.Columns["Telephely"].Width = 90;
            Hibanaplo_Tablazat.Columns["Felhasználó"].Width = 70;
            Hibanaplo_Tablazat.Columns["Hiba üzenet"].Width = 450;
            Hibanaplo_Tablazat.Columns["Hiba osztály"].Width = 300;
            Hibanaplo_Tablazat.Columns["Hiba metódus"].Width = 300;
            Hibanaplo_Tablazat.Columns["Névtér"].Width = 70;
            Hibanaplo_Tablazat.Columns["Egyéb"].Width = 40;
        }

        static string[] KorabbiEvek()
        {
            string path = $@"{Application.StartupPath}\Főmérnökség\adatok\Hibanapló";
            List<string> result = new List<string>();

            Regex regex = new Regex(@"^\d{4}$");

            foreach (string dir in Directory.GetDirectories(path))
            {
                string folderName = Path.GetFileName(dir);
                if (regex.IsMatch(folderName))
                {
                    result.Add(folderName);
                }
            }
            return result.ToArray();
        }

        private void btn_frissit_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void cmb_valaszthato_evek_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Start(); 
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

    }
}
