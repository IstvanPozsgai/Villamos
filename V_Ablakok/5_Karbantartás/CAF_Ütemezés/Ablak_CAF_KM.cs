using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.V_Ablakok._5_Karbantartás.CAF_Ütemezés
{
    public partial class Ablak_CAF_KM : Form
    {
        DataTable AdatTábla = new DataTable();
        string szűrő = "";
        string sorba = "";

        Kezelő_CAF_Adatok KézAdatok = new Kezelő_CAF_Adatok();
        List<Adat_CAF_Adatok> CafAdatok = new List<Adat_CAF_Adatok>();

        public Ablak_CAF_KM()
        {
            InitializeComponent();
        }

        private void Ablak_CAF_KM_Load(object sender, EventArgs e)
        {
            Start();
        }

        private void Start()
        {
            ABFejléc();
            Tablalista_kiírás();
        }

        private void Tablalista_kiírás()
        {
            Listázás();
            ABFeltöltése();
            Tablalista.DataSource = AdatTábla;
            OszlopSzélesség();
            Tablalista.Refresh();
            Tablalista.Visible = true;
            Tablalista.ClearSelection();
        }

        private void ABFejléc()
        {
            AdatTábla.Columns.Clear();
            AdatTábla.Columns.Add("Pályaszám");
            AdatTábla.Columns.Add("Vizsgálat");
            AdatTábla.Columns.Add("Dátum");
            AdatTábla.Columns.Add("Számláló állás");
            AdatTábla.Columns.Add("Státusz");
            AdatTábla.Columns.Add("KM vizsgálat sorszáma");
            AdatTábla.Columns.Add("Idő vizsgálat sorszáma");
            AdatTábla.Columns.Add("Idő vagy Km vizsgálat");
        }

        private void Listázás()
        {
            CafAdatok.Clear();
            CafAdatok = KézAdatok.Lista_Adatok();
        }

        private void ABFeltöltése()
        {
            AdatTábla.Clear();
            foreach (Adat_CAF_Adatok villamos in CafAdatok)
            {
                if (villamos.KmRogzitett_e && villamos.Megjegyzés!= "Ütemezési Segéd")
                {
                    DataRow Soradat = AdatTábla.NewRow();
                    Soradat["Pályaszám"] = villamos.Azonosító;
                    Soradat["Vizsgálat"] = villamos.Vizsgálat;
                    Soradat["Dátum"] = villamos.Dátum;
                    Soradat["Számláló állás"] = villamos.Számláló;
                    Soradat["Státusz"] = villamos.Státus;
                    Soradat["KM vizsgálat sorszáma"] = villamos.KM_Sorszám;
                    Soradat["Idő vizsgálat sorszáma"] = villamos.IDŐ_Sorszám;
                    Soradat["Idő vagy Km vizsgálat"] = villamos.IDŐvKM;
                    AdatTábla.Rows.Add(Soradat);
                }            
            }
        }

        private void OszlopSzélesség()
        {
            Tablalista.Columns["Pályaszám"].Width = 80;
            Tablalista.Columns["Vizsgálat"].Width = 150;
            Tablalista.Columns["Dátum"].Width = 100;
            Tablalista.Columns["Számláló állás"].Width = 120;
            Tablalista.Columns["Státusz"].Width = 100;
            Tablalista.Columns["KM vizsgálat sorszáma"].Width = 100;
            Tablalista.Columns["Idő vizsgálat sorszáma"].Width = 100;
            Tablalista.Columns["Idő vagy Km vizsgálat"].Width = 100;
        }
    }
}
