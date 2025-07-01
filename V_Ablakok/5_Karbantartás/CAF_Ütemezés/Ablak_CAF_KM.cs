using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
            Tablalista.CellValidating += Tablalista_CellValidating;
            Tablalista.CellEndEdit += Tablalista_CellEndEdit;
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
            OszlopEngedelyezes();
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
            AdatTábla.Columns.Add("Idő vagy Km vizsgálat");
            AdatTábla.Columns.Add("KM vizsgálat sorszáma");
            AdatTábla.Columns.Add("Idő vizsgálat sorszáma");     
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
                if (villamos.KmRogzitett_e && villamos.Megjegyzés != "Ütemezési Segéd")
                {
                    DataRow Soradat = AdatTábla.NewRow();
                    Soradat["Pályaszám"] = villamos.Azonosító;
                    Soradat["Vizsgálat"] = villamos.Vizsgálat;
                    Soradat["Dátum"] = villamos.Dátum.ToString("yyyy.MM.dd");
                    Soradat["Számláló állás"] = villamos.Számláló;
                    //Soradat["Státusz"] = villamos.Státus;
                    switch (villamos.Státus)
                    {
                        case 0:
                            Soradat["Státusz"] = "0 - Tervezési";
                            break;
                        case 2:
                            Soradat["Státusz"] = "2 - Ütemezett";
                            break;
                        case 4:
                            Soradat["Státusz"] = "4 - Előjegyzett";
                            break;
                        case 6:
                            Soradat["Státusz"] = "6 - Elvégzett";
                            break;
                        case 8:
                            Soradat["Státusz"] = "8 - Tervezési segéd";
                            break;
                        case 9:
                            Soradat["Státusz"] = "9 - Törölt";
                            break;
                        default:
                            Soradat["Státusz"] = "-";
                            break;
                    }
                    //Soradat["Idő vagy Km vizsgálat"] = villamos.IDŐvKM;
                    //Jól tudom, hogy az 1 a KM és a 2 az idő alapú?
                    switch (villamos.IDŐvKM)
                    {
                        case 1:
                            Soradat["Idő vagy Km vizsgálat"] = "Km";
                            break;
                        case 2:
                            Soradat["Idő vagy Km vizsgálat"] = "Idő";
                            break;
                        default:
                            Soradat["Idő vagy Km vizsgálat"] = "-";
                            break;
                    }
                    Soradat["KM vizsgálat sorszáma"] = villamos.KM_Sorszám;
                    Soradat["Idő vizsgálat sorszáma"] = villamos.IDŐ_Sorszám;

                   
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
            Tablalista.Columns["Idő vagy Km vizsgálat"].Width = 100;
            Tablalista.Columns["KM vizsgálat sorszáma"].Width = 100;
            Tablalista.Columns["Idő vizsgálat sorszáma"].Width = 100;          
        }

        private void OszlopEngedelyezes()
        {
            foreach (DataGridViewColumn column in Tablalista.Columns)
            {
                if (column.Name == "Számláló állás")
                {
                    column.ReadOnly = false;
                }
                else
                {
                    column.ReadOnly = true;
                }
            }
        }

        private void Tablalista_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (Tablalista.Columns[e.ColumnIndex].Name == "Számláló állás")
            {
                string ujErtek = e.FormattedValue?.ToString() ?? "";

                if (!int.TryParse(ujErtek, out int ujSzamlalo))
                {
                    MessageBox.Show("Kérem csak számot adjon meg a számláló állás mezőben!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }

                DataGridViewRow sor = Tablalista.Rows[e.RowIndex];

                string azonosito = sor.Cells["Pályaszám"].Value?.ToString() ?? "";
                string vizsgalat = sor.Cells["Vizsgálat"].Value?.ToString() ?? "";
                string datum = sor.Cells["Dátum"].Value?.ToString() ?? "";

                var villamos = CafAdatok.FirstOrDefault(a => a.Azonosító == azonosito && a.Vizsgálat == vizsgalat && a.Dátum.ToString("yyyy.MM.dd") == datum);

                if (villamos != null && ujSzamlalo < villamos.Számláló)
                {
                    MessageBox.Show($"Az új számláló érték nem lehet kisebb, mint a jelenlegi ({villamos.Számláló})!",
                                    "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
            }
        }

        private void Tablalista_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (Tablalista.Columns[e.ColumnIndex].Name == "Számláló állás")
            {
                DataGridViewRow sor = Tablalista.Rows[e.RowIndex];

                string azonosito = sor.Cells["Pályaszám"].Value?.ToString() ?? "";
                string vizsgalat = sor.Cells["Vizsgálat"].Value?.ToString() ?? "";
                string datum = sor.Cells["Dátum"].Value?.ToString() ?? "";
                int szamlalo = int.Parse(sor.Cells["Számláló állás"].Value?.ToString() ?? "0");

                var villamos = CafAdatok.FirstOrDefault(a => a.Azonosító == azonosito && a.Vizsgálat == vizsgalat && a.Dátum.ToString("yyyy.MM.dd") == datum);

                if (villamos != null)
                {
                    try
                    {
                        KézAdatok.Módosítás_Km(villamos.Id, szamlalo);
                        MessageBox.Show("Módosítás sikeres.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hiba a mentés során: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("A módosítandó rekord nem található.", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
