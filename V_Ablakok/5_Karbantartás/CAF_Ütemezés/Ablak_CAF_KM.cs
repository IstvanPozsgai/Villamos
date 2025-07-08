using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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

        Kezelő_CAF_Adatok KézAdatok = new Kezelő_CAF_Adatok();
        List<Adat_CAF_Adatok> CafAdatok = new List<Adat_CAF_Adatok>();

        public Ablak_CAF_KM()
        {
            InitializeComponent();
            Tablalista.CellValidating += Tablalista_CellValidating;
            Tablalista.CellEndEdit += Tablalista_CellEndEdit;
        }

        // Kérdés: Jól látom, hogy csak az utolsó vizsgálat telephelyét tároljuk az alap táblában?
        private void Ablak_CAF_KM_Load(object sender, EventArgs e)
        {
            Start();
        }

        private void Start()
        {
            ABFejléc();
            Tablalista_kiírás();
            
            Tablalista.DefaultCellStyle.Font = new Font(Tablalista.Font.FontFamily, 11);
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
            AdatTábla.Columns.Add("Telephely");
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
                if (villamos.Státus == 6 && villamos.Megjegyzés != "Ütemezési Segéd")
                {
                    DataRow Soradat = AdatTábla.NewRow();
                    Soradat["Pályaszám"] = villamos.Azonosító;
                    Soradat["Vizsgálat"] = villamos.Vizsgálat;
                    Soradat["Dátum"] = villamos.Dátum.ToString("yyyy.MM.dd");
                    Soradat["Számláló állás"] = villamos.Számláló;
                    Soradat["Telephely"] = villamos.Telephely;                    
                   
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
                        Tablalista_kiírás();
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
