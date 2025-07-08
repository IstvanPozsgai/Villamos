using System;
using System.Collections.Generic;
using System.Data;
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
            Start();
        }

        // Kérdés: Jól látom, hogy csak az utolsó vizsgálat telephelyét tároljuk az alap táblában?
        private void Ablak_CAF_KM_Load(object sender, EventArgs e)
        {

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
            CafAdatok = KézAdatok.Lista_Adatok();
            CafAdatok = (from a in CafAdatok
                         where a.Státus == 6
                         && a.Megjegyzés != "Ütemezési Segéd"
                         && a.KmRogzitett_e
                         orderby a.Azonosító, a.Dátum
                         select a).ToList();
        }

        private void ABFeltöltése()
        {
            AdatTábla.Clear();
            foreach (Adat_CAF_Adatok villamos in CafAdatok)
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

        private void OszlopSzélesség()
        {
            Tablalista.Columns["ID"].Width = 80;
            Tablalista.Columns["ID"].ReadOnly = true;
            Tablalista.Columns["Pályaszám"].Width = 180;
            Tablalista.Columns["Pályaszám"].ReadOnly = true;
            Tablalista.Columns["Vizsgálat"].Width = 150;
            Tablalista.Columns["Vizsgálat"].ReadOnly = true;
            Tablalista.Columns["Dátum"].Width = 100;
            Tablalista.Columns["Dátum"].ReadOnly = true;
            Tablalista.Columns["Számláló állás"].Width = 120;
            Tablalista.Columns["Számláló állás"].ReadOnly = false;
        }

        private void Tablalista_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string ujErtek = Tablalista.Rows[Sor].Cells["Számláló állás"].Value.ToString();

                if (!int.TryParse(ujErtek, out int ujSzamlalo))
                    throw new HibásBevittAdat("Kérem csak számot adjon meg a számláló állás mezőben!");

                DataGridViewRow sor = Tablalista.Rows[Sor];

                string ID = sor.Cells["ID"].Value?.ToString() ?? "";
                string azonosito = sor.Cells["Pályaszám"].Value?.ToString() ?? "";
                string vizsgalat = sor.Cells["Vizsgálat"].Value?.ToString() ?? "";
                DateTime datum = sor.Cells["Dátum"].Value.ToÉrt_DaTeTime();

                Adat_CAF_Adatok ADAT = (from a in CafAdatok
                                        where a.Azonosító == azonosito
                                        && a.Státus == 6
                                        && a.Dátum < datum
                                        orderby a.Dátum
                                        select a).LastOrDefault();
                if (ADAT.Számláló > ujSzamlalo) throw new HibásBevittAdat($"Az új számláló érték nem lehet kisebb, mint az előző ({ADAT.Számláló})!");


                Adat_CAF_Adatok villamos = CafAdatok.FirstOrDefault(a => a.Azonosító == azonosito && a.Vizsgálat == vizsgalat && a.Dátum == datum);

                if (ID != "")
                {
                    KézAdatok.Módosítás_Km(villamos.Id, ujSzamlalo);
                    Tablalista_kiírás();
                }
                else
                {
                    throw new HibásBevittAdat("A módosítandó rekord nem található.");
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
    }
}
