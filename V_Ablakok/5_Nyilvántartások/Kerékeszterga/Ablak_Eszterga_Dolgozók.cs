using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Adatszerkezet;


namespace Villamos.Villamos_Ablakok
{
    public partial class Ablak_Eszterga_Dolgozók : Form
    {
        public string Cmbtelephely { get; private set; }
        readonly Kezelő_Kerék_Eszterga_Esztergályos KézEszterga = new Kezelő_Kerék_Eszterga_Esztergályos();
        readonly Kezelő_Kiegészítő_Könyvtár KézKiegKönyv = new Kezelő_Kiegészítő_Könyvtár();
        readonly Kezelő_Dolgozó_Alap KézDolgozó = new Kezelő_Dolgozó_Alap();


        List<Adat_Kerék_Eszterga_Esztergályos> Adatok = new List<Adat_Kerék_Eszterga_Esztergályos>();

        public Ablak_Eszterga_Dolgozók(string cmbtelephely)
        {
            InitializeComponent();
            Cmbtelephely = cmbtelephely;
            Start();
        }

        public Ablak_Eszterga_Dolgozók()
        {
            InitializeComponent();
        }

        private void Start()
        {
            Telephelyfeltöltés();
            Telephely.Text = Cmbtelephely.Trim();
            Dolg_Nevek_Fel();
            Munka_Jelleg_feltöltés();
            Tábla_Lista();
        }

        private void Ablak_Eszterga_Dolgozók_Load(object sender, EventArgs e)
        {
        }

        private void Munka_Jelleg_feltöltés()
        {
            Munkajelleg.Items.Add("1- Főállású");
            Munkajelleg.Items.Add("2- Honos telephely Besegítő");
            Munkajelleg.Items.Add("3- Idegen telephely Besegítő");
        }

        private void Telephelyfeltöltés()
        {
            try
            {
                Telephely.Items.Clear();
                List<Adat_Kiegészítő_Könyvtár> Adatok = KézKiegKönyv.Lista_Adatok().OrderBy(a => a.Név).ToList();

                foreach (Adat_Kiegészítő_Könyvtár rekord in Adatok)
                    Telephely.Items.Add(rekord.Név.Trim());
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

        private void Dolg_Nevek_Fel()
        {
            try
            {
                Dolgozó_nevek.Items.Clear();
                List<Adat_Dolgozó_Alap> Adatok = KézDolgozó.Lista_Adatok(Cmbtelephely.Trim());

                foreach (Adat_Dolgozó_Alap rekord in Adatok)
                    Dolgozó_nevek.Items.Add(rekord.DolgozóNév + " = " + rekord.Dolgozószám);
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

        private void TörzsListaFeltöltés()
        {
            try
            {
                Adatok.Clear();
                Adatok = KézEszterga.Lista_Adatok().OrderBy(a => a.Dolgozónév).ToList();
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

        private void Tábla_Lista()
        {
            try
            {
                TörzsListaFeltöltés();
                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 4;


                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "HR Azonosító";
                Tábla.Columns[0].Width = 100;
                Tábla.Columns[1].HeaderText = "Név";
                Tábla.Columns[1].Width = 200;
                Tábla.Columns[2].HeaderText = "Telephely";
                Tábla.Columns[2].Width = 150;
                Tábla.Columns[3].HeaderText = "Munka jellege";
                Tábla.Columns[3].Width = 200;

                int i;

                foreach (Adat_Kerék_Eszterga_Esztergályos rekord in Adatok)
                {
                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = rekord.Dolgozószám.Trim();
                    Tábla.Rows[i].Cells[1].Value = rekord.Dolgozónév.Trim();
                    Tábla.Rows[i].Cells[2].Value = rekord.Telephely.Trim();
                    switch (rekord.Státus)
                    {
                        case 1:
                            Tábla.Rows[i].Cells[3].Value = "1- Főállású";
                            break;

                        case 2:
                            Tábla.Rows[i].Cells[3].Value = "2- Honos telephely Besegítő";
                            break;

                        case 3:
                            Tábla.Rows[i].Cells[3].Value = "3- Idegen telephely Besegítő";
                            break;

                        default:
                            Tábla.Rows[i].Cells[3].Value = "";
                            break;
                    }

                }
                Tábla.Refresh();
                Tábla.ClearSelection();
                Tábla.Visible = true;
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

        private void Frissít_Click(object sender, EventArgs e)
        {
            Tábla_Lista();
        }

        private void Esztergályos_Rögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Dolgozó_nevek.Text.Trim() == "") throw new HibásBevittAdat("A dolgozót ki kell választani.");
                if (Telephely.Text.Trim() == "") throw new HibásBevittAdat("A Telephelyet ki kell választani, nem lehet üres.");
                if (Munkajelleg.Text.Trim() == "") throw new HibásBevittAdat("A munka jellegét ki kell választani, nem lehet üres.");

                TörzsListaFeltöltés();
                string[] darabol = Dolgozó_nevek.Text.Trim().Split('=');
                string[] darabos = Munkajelleg.Text.Trim().Split('-');

                Adat_Kerék_Eszterga_Esztergályos ADAT = new Adat_Kerék_Eszterga_Esztergályos(
                    darabol[1].Trim(), // Dolgozószám
                    darabol[0].Trim(), // Dolgozónév
                    Telephely.Text.Trim(), // Telephely
                    int.Parse(darabos[0]) // Státus
                );
                KézEszterga.Döntés(ADAT);
                MessageBox.Show("Az adatok rögzítésre kerültek!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Tábla_Lista();
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


        private void Esztergályos_törlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Dolgozó_nevek.Text.Trim() == "") throw new HibásBevittAdat("A dolgozót ki kell választani.");
                TörzsListaFeltöltés();
                string[] darabol = Dolgozó_nevek.Text.Trim().Split('=');
                Adat_Kerék_Eszterga_Esztergályos Elem = (from a in Adatok
                                                         where a.Dolgozószám == darabol[1].Trim()
                                                         select a).FirstOrDefault();

                if (Elem != null)
                {
                    KézEszterga.Törlés(darabol[1].Trim());
                    MessageBox.Show("Az adatok törlésre kerültek!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Tábla_Lista();
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

        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i < 0) return;
            Dolgozó_nevek.Text = Tábla.Rows[i].Cells[1].Value.ToString().Trim() + " = " + Tábla.Rows[i].Cells[0].Value.ToString().Trim();
            Telephely.Text = Tábla.Rows[i].Cells[2].Value.ToString().Trim();
            Munkajelleg.Text = Tábla.Rows[i].Cells[3].Value.ToString().Trim();
        }

        private void Ablak_Eszterga_Dolgozók_KeyDown(object sender, KeyEventArgs e)
        {

            // ESC
            if ((int)e.KeyCode == 27)
            {
                this.Close();
            }
        }

        private void Telephely_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cmbtelephely = Telephely.Text.Trim();
            Dolg_Nevek_Fel();
        }
    }
}
