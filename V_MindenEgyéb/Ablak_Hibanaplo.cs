using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Villamos.V_MindenEgyéb
{
    public partial class Ablak_Hibanaplo : Form
    {

        readonly DataTable AdatTábla = new DataTable();

        // Dátum;Telephely;Felhsználó;Hiba üzenet;Hiba Osztály; Hiba Metódus; Névtér; Egyéb; Dátum
        public Ablak_Hibanaplo()
        {
            InitializeComponent();
        }

        private void Ablak_Hibanaplo_Load(object sender, EventArgs e)
        {

        }

        private void Start()
        {

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
           
        }

        private void Listazas()
        {

        }

        private void ABFeltöltése()
        {

        }

        private void OszlopSzélesség()
        {

        }

    }
}
