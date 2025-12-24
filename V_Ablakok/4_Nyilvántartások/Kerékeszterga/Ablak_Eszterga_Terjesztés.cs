using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Adatszerkezet;

namespace Villamos.Villamos_Ablakok.Kerékeszterga
{
    public partial class Ablak_Eszterga_Terjesztés : Form
    {
        readonly Kezelő_Kiegészítő_Könyvtár KézKönyv = new Kezelő_Kiegészítő_Könyvtár();
        readonly Kezelő_Kerék_Eszterga_Terjesztés kéz = new Kezelő_Kerék_Eszterga_Terjesztés();

        List<Adat_Kerék_Eszterga_Terjesztés> Adatok = new List<Adat_Kerék_Eszterga_Terjesztés>();

        public Ablak_Eszterga_Terjesztés()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            Telephelyekfeltöltése();
            TerjesztésFeltöltése();
            Tábla_Író();
        }

        private void TerjesztésFeltöltése()
        {
            CmbVáltozat.Items.Clear();
            CmbVáltozat.Items.Add("");
            CmbVáltozat.Items.Add("1 - Heti terv");
            CmbVáltozat.Items.Add("2 - Heti Lejelentés");
            CmbVáltozat.Items.Add("3 - Heti terv és lejelentés");
        }


        private void Telephelyekfeltöltése()
        {
            try
            {
                List<Adat_Kiegészítő_Könyvtár> Adatok = KézKönyv.Lista_Adatok();
                Adatok = Adatok.OrderBy(a => a.Név).ToList();
                Cmbtelephely.Items.Clear();
                foreach (Adat_Kiegészítő_Könyvtár Elem in Adatok)
                    Cmbtelephely.Items.Add(Elem.Név);

                Cmbtelephely.Refresh();
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

        private void Ablak_Eszterga_Terjesztés_Load(object sender, EventArgs e)
        {

        }

        private void MezőÜrítés()
        {
            Név.Text = "";
            Email.Text = "";
            Cmbtelephely.Text = "";
            CmbVáltozat.Text = "";
        }

        private void Tábla_Író()
        {
            try
            {
                MezőÜrítés();
                Adatok = kéz.Lista_Adatok();
                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 4;

                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Név";
                Tábla.Columns[0].Width = 180;
                Tábla.Columns[1].HeaderText = "E-mail cím";
                Tábla.Columns[1].Width = 180;
                Tábla.Columns[2].HeaderText = "Telephely";
                Tábla.Columns[2].Width = 150;
                Tábla.Columns[3].HeaderText = "Terjesztési változat";
                Tábla.Columns[3].Width = 170;

                int i;
                foreach (Adat_Kerék_Eszterga_Terjesztés rekord in Adatok)
                {
                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = rekord.Név.Trim();
                    Tábla.Rows[i].Cells[1].Value = rekord.Email.Trim();
                    Tábla.Rows[i].Cells[2].Value = rekord.Telephely.Trim();
                    switch (rekord.Változat)
                    {
                        case 1:
                            Tábla.Rows[i].Cells[3].Value = "1 - Heti terv";
                            break;
                        case 2:
                            Tábla.Rows[i].Cells[3].Value = "2 - Heti Lejelentés";
                            break;
                        case 3:
                            Tábla.Rows[i].Cells[3].Value = "3 - Heti terv és lejelentés";
                            break;
                    }
                }
                Tábla.Refresh();
               
                Tábla.Visible = true;
                Tábla.ClearSelection();
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

        private void Rögzít_Click(object sender, EventArgs e)
        {
            try
            {

                if (Név.Text.Trim() == "") throw new HibásBevittAdat("A név mező nem lehet üres.");
                if (Email.Text.Trim() == "") throw new HibásBevittAdat("Az e-mail címet meg kell adni.");
                Regex validateEmailRegex = new Regex("^\\S+@\\S+\\.\\S+$");
                if (!validateEmailRegex.IsMatch(Email.Text.Trim())) throw new HibásBevittAdat("Az e-mail cím formátuma nem megfelelő.");
                if (Cmbtelephely.Text.Trim() == "") throw new HibásBevittAdat("A telephely mező nem lehet üres.");
                if (CmbVáltozat.Text.Trim() == "") throw new HibásBevittAdat("A terjesztési változat mező nem lehet üres.");

                string[] darabol = CmbVáltozat.Text.Split('-');
                Adatok = kéz.Lista_Adatok();

                Adat_Kerék_Eszterga_Terjesztés ADAT = new Adat_Kerék_Eszterga_Terjesztés(
                     Név.Text.Trim(),
                     Email.Text.Trim(),
                     Cmbtelephely.Text.Trim(),
                     int.Parse(darabol[0]));

                kéz.Döntés(ADAT);
                Tábla_Író();
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

        private void Töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (Email.Text.Trim() == "") throw new HibásBevittAdat("Az e-mail címet meg kell adni.");

                Adatok = kéz.Lista_Adatok();
                Adat_Kerék_Eszterga_Terjesztés Elem = (from a in Adatok
                                                       where a.Email == Email.Text.Trim()
                                                       select a).FirstOrDefault();

                if (Elem != null)
                {
                    kéz.Törlés(Email.Text.Trim());
                    Tábla_Író();
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
            try
            {
                if (e.RowIndex >= 0)
                {
                    Név.Text = Tábla.Rows[e.RowIndex].Cells[0].Value == null ? "" : Tábla.Rows[e.RowIndex].Cells[0].Value.ToString();
                    Email.Text = Tábla.Rows[e.RowIndex].Cells[1].Value == null ? "" : Tábla.Rows[e.RowIndex].Cells[1].Value.ToString();
                    Cmbtelephely.Text = Tábla.Rows[e.RowIndex].Cells[2].Value == null ? "" : Tábla.Rows[e.RowIndex].Cells[2].Value.ToString();
                    CmbVáltozat.Text = Tábla.Rows[e.RowIndex].Cells[3].Value == null ? "" : Tábla.Rows[e.RowIndex].Cells[3].Value.ToString();
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
    }
}
