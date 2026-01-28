using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Adatszerkezet;

namespace Villamos.Villamos_Ablakok.TW6000
{
    public partial class Ablak_TW6000_Telephely : Form
    {
        readonly Kezelő_TW600_Telephely Kéz = new Kezelő_TW600_Telephely();
        readonly Kezelő_kiegészítő_telephely KézKieg = new Kezelő_kiegészítő_telephely();

        List<Adat_TW6000_Telephely> Adatok = new List<Adat_TW6000_Telephely>();

        public Ablak_TW6000_Telephely()
        {
            InitializeComponent();
        }

        private void Üzem_rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Üzem_sorszám.Text.Trim() == "") return;
                if (!int.TryParse(Üzem_sorszám.Text, out int Sorszám)) return;
                if (Üzemek.Text.Trim() == "") return;
                Adatok = Kéz.Lista_Adatok();

                Adat_TW6000_Telephely Elem = (from a in Adatok
                                              where a.Telephely == Üzemek.Text.Trim()
                                              select a).FirstOrDefault();
                Adat_TW6000_Telephely ADAT = new Adat_TW6000_Telephely(Sorszám, Üzemek.Text.Trim());
                if (Elem == null)
                    Kéz.Rögzítés(ADAT);
                else
                    Kéz.Módosítás(ADAT);

                Telephely_lista();
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

        private void ÜzemTöröl_Click(object sender, EventArgs e)
        {
            Telephely_lista();
        }

        private void Üzem_töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (Üzemek.Text.Trim() == "") return;
                Adatok = Kéz.Lista_Adatok();
                Adat_TW6000_Telephely Elem = (from a in Adatok
                                              where a.Telephely == Üzemek.Text.Trim()
                                              select a).FirstOrDefault();

                if (Elem != null) Kéz.Törlés(Üzemek.Text.Trim());
                Telephely_lista();
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

        public void Telephely_lista()
        {
            try
            {
                Adatok = Kéz.Lista_Adatok();

                Telephely_tábla.Rows.Clear();
                Telephely_tábla.Columns.Clear();
                Telephely_tábla.Refresh();
                Telephely_tábla.Visible = false;
                Telephely_tábla.ColumnCount = 2;

                // fejléc elkészítése
                Telephely_tábla.Columns[0].HeaderText = "Sorszám";
                Telephely_tábla.Columns[0].Width = 90;
                Telephely_tábla.Columns[1].HeaderText = "Telephely";
                Telephely_tábla.Columns[1].Width = 200;


                foreach (Adat_TW6000_Telephely rekord in Adatok)
                {
                    Telephely_tábla.RowCount++;
                    int i = Telephely_tábla.RowCount - 1;
                    Telephely_tábla.Rows[i].Cells[0].Value = rekord.Sorrend;
                    Telephely_tábla.Rows[i].Cells[1].Value = rekord.Telephely.Trim();
                }
                Telephely_tábla.Visible = true;
                Telephely_tábla.Refresh();
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

        private void Telephely_tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            Üzem_sorszám.Text = Telephely_tábla.Rows[e.RowIndex].Cells[0].Value.ToString();
            Üzemek.Text = Telephely_tábla.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void Üzemeklista_feltöltése()
        {
            try
            {
                Üzemek.Items.Clear();
                List<Adat_kiegészítő_telephely> Adatok = KézKieg.Lista_Adatok();
                foreach (Adat_kiegészítő_telephely Elem in Adatok)
                    Üzemek.Items.Add(Elem.Telephelykönyvtár);
                Üzemek.Refresh();
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

        private void Ablak_TW6000_Telephely_Load(object sender, EventArgs e)
        {
            Üzemeklista_feltöltése();
            Telephely_lista();
        }
    }
}
