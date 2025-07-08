using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.V_Ablakok._5_Karbantartás.CAF_Ütemezés
{
    public partial class Ablak_CAF_KM : Form
    {
        readonly DataTable AdatTábla = new DataTable();

        readonly Kezelő_CAF_Adatok KézAdatok = new Kezelő_CAF_Adatok();
        List<Adat_CAF_Adatok> CafAdatok = new List<Adat_CAF_Adatok>();

        int Sor = -1;
        public Ablak_CAF_KM()
        {
            InitializeComponent();

            //   Tablalista.CellEndEdit += Tablalista_CellEndEdit;
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

            //       Tablalista.RowPrePaint += Tablalista_RowPrePaint; // piros színezés aktiválása
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
            AdatTábla.Columns.Add("ID");
            AdatTábla.Columns.Add("Pályaszám");
            AdatTábla.Columns.Add("Vizsgálat");
            AdatTábla.Columns.Add("Dátum", typeof(DateTime));
            AdatTábla.Columns.Add("Számláló állás", typeof(long));
            AdatTábla.Columns.Add("Telephely");
        }

        private void Listázás()
        {
            CafAdatok.Clear();
            CafAdatok = KézAdatok.Lista_Adatok()
                .OrderBy(a => a.Azonosító)
                .ThenBy(a => a.Dátum)
                .ToList();
        }


        private void ABFeltöltése()
        {
            AdatTábla.Clear();
            foreach (Adat_CAF_Adatok villamos in CafAdatok)
            {
                if (villamos.Státus == 6 && villamos.Megjegyzés != "Ütemezési Segéd" && villamos.KmRogzitett_e)
                {
                    DataRow Soradat = AdatTábla.NewRow();
                    Soradat["ID"] = villamos.Id;
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
            Tablalista.Columns["ID"].Width = 80;
            Tablalista.Columns["Pályaszám"].Width = 180;
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



        private void Tablalista_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Tablalista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                Sor = e.RowIndex;


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

        private void ValidateKeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char)(e.KeyChar) != 13 && (char)(e.KeyChar) != 8 && !int.TryParse(e.KeyChar.ToString(), out int Állapot))
            {
                MessageBox.Show("Csak egész számot lehet beírni!");
                e.Handled = true;
                return;
            }


            if ((char)(e.KeyChar) == 13)
            {

                string ujErtek = Tablalista.Rows[Sor].Cells["Számláló állás"].Value.ToString();

                if (!int.TryParse(ujErtek, out int ujSzamlalo))
                {
                    MessageBox.Show("Kérem csak számot adjon meg a számláló állás mezőben!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                DataGridViewRow sor = Tablalista.Rows[Sor];

                string azonosito = sor.Cells["Pályaszám"].Value?.ToString() ?? "";
                string vizsgalat = sor.Cells["Vizsgálat"].Value?.ToString() ?? "";
                DateTime datum = sor.Cells["Dátum"].Value.ToÉrt_DaTeTime();

                Adat_CAF_Adatok ADAT = (from a in CafAdatok
                                        where a.Azonosító == azonosito
                                        && a.Státus == 6
                                        && a.Dátum < datum
                                        orderby a.Dátum
                                        select a).LastOrDefault();
                if (ADAT.Számláló > ujSzamlalo)
                {
                    MessageBox.Show($"Az új számláló érték nem lehet kisebb, mint az előző ({ADAT.Számláló})!",
                                    "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Adat_CAF_Adatok villamos = CafAdatok.FirstOrDefault(a => a.Azonosító == azonosito && a.Vizsgálat == vizsgalat && a.Dátum == datum);

                if (villamos != null)
                {
                    try
                    {
                        KézAdatok.Módosítás_Km(villamos.Id, ujSzamlalo);
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

        //private void Tablalista_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        //{
        //    if (e.RowIndex < 0 || e.RowIndex >= Tablalista.Rows.Count)
        //        return;

        //    DataGridViewRow jelenlegiRow = Tablalista.Rows[e.RowIndex];
        //    string jelenlegiAzonosito = jelenlegiRow.Cells["Pályaszám"].Value?.ToString() ?? "";
        //    string jelenlegiDatumStr = jelenlegiRow.Cells["Dátum"].Value?.ToString() ?? "";
        //    int.TryParse(jelenlegiRow.Cells["Számláló állás"].Value?.ToString(), out int jelenlegiSzamlalo);

        //    jelenlegiRow.DefaultCellStyle.BackColor = Tablalista.DefaultCellStyle.BackColor;
        //    jelenlegiRow.DefaultCellStyle.ForeColor = Tablalista.DefaultCellStyle.ForeColor;

        //    bool hiba = false;

        //    if (jelenlegiSzamlalo == 0)
        //    {
        //        hiba = true;
        //    }
        //    else
        //    {
        //        //Hátulról nézem
        //        for (int i = e.RowIndex - 1; i >= 0; i--)
        //        {
        //            var elozoRow = Tablalista.Rows[i];
        //            string elozoAzonosito = elozoRow.Cells["Pályaszám"].Value?.ToString() ?? "";

        //            if (elozoAzonosito != jelenlegiAzonosito)
        //                break;

        //            int.TryParse(elozoRow.Cells["Számláló állás"].Value?.ToString(), out int elozoSzamlalo);

        //            if (jelenlegiSzamlalo < elozoSzamlalo)
        //            {
        //                hiba = true;
        //            }

        //            break;
        //        }
        //    }
        //    if (hiba)
        //    {
        //        jelenlegiRow.DefaultCellStyle.BackColor = Color.Red;
        //        jelenlegiRow.DefaultCellStyle.ForeColor = Color.White;
        //    }
        //}

    }
}
