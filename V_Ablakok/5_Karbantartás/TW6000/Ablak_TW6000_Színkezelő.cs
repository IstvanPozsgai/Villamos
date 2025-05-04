using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;


namespace Villamos.Villamos_Ablakok.TW6000
{
    public partial class Ablak_TW6000_Színkezelő : Form
    {
        readonly Kezelő_TW600_Színezés kéz = new Kezelő_TW600_Színezés();
        List<Adat_TW6000_Színezés> Adatok = new List<Adat_TW6000_Színezés>();

        public Ablak_TW6000_Színkezelő()
        {
            InitializeComponent();
        }

        private void Szín_tábla_lista_Click(object sender, EventArgs e)
        {
            Szín_tábla_kiírás();
        }

        private void Karb_új_Click(object sender, EventArgs e)
        {
            Vonal.Text = "";
            Színe.Text = "";
        }

        private void Karb_töröl_Click(object sender, EventArgs e)
        {
            if (Vonal.Text.Trim() == "") return;

            Adatok = kéz.Lista_Adatok();
            Adat_TW6000_Színezés Elem = (from a in Adatok
                                         where a.Vizsgálatnév == Vonal.Text.Trim()
                                         select a).FirstOrDefault();

            if (Elem != null) kéz.Törlés(Vonal.Text.Trim());
            Szín_tábla_kiírás();
        }

        private void SzínPaletta_Click(object sender, EventArgs e)
        {
            double zöld;
            double piros;
            double kék;

            Színe.Text = 0.ToString();
            if (ColorDialog1.ShowDialog() != DialogResult.Cancel)
            {
                piros = ColorDialog1.Color.R;
                zöld = ColorDialog1.Color.G;
                kék = ColorDialog1.Color.B;

                Színe.Text = (piros + zöld * 256d + kék * 65536d).ToString();
                Színe.BackColor = ColorDialog1.Color;
            }
        }

        private void Karb_rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Vonal.Text.Trim() == "") return;
                if (Színe.Text.Trim() == "") return;
                if (!int.TryParse(Színe.Text, out int Színszám)) return;

                Adatok = kéz.Lista_Adatok();
                Adat_TW6000_Színezés Elem = (from a in Adatok
                                             where a.Vizsgálatnév == Vonal.Text.Trim()
                                             select a).FirstOrDefault();
                Adat_TW6000_Színezés ADAT = new Adat_TW6000_Színezés(Színszám, Vonal.Text.Trim());
                if (Elem == null)
                    kéz.Rögzítés(ADAT);
                else
                    kéz.Módosítás(ADAT);
                Szín_tábla_kiírás();
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

        public void Szín_tábla_kiírás()
        {
            try
            {
                double zöld;
                double piros;
                double kék;
                double színszám;

                Adatok = kéz.Lista_Adatok();

                Szín_Tábla.Rows.Clear();
                Szín_Tábla.Columns.Clear();
                Szín_Tábla.Refresh();
                Szín_Tábla.Visible = false;
                Szín_Tábla.ColumnCount = 2;

                // fejléc elkészítése
                Szín_Tábla.Columns[0].HeaderText = "Vizsgálat";
                Szín_Tábla.Columns[0].Width = 100;
                Szín_Tábla.Columns[1].HeaderText = "Szín";
                Szín_Tábla.Columns[1].Width = 150;


                foreach (Adat_TW6000_Színezés rekord in Adatok)
                {
                    Szín_Tábla.RowCount++;
                    int i = Szín_Tábla.RowCount - 1;
                    Szín_Tábla.Rows[i].Cells[0].Value = rekord.Vizsgálatnév;
                    Szín_Tábla.Rows[i].Cells[1].Value = rekord.Szín;

                    //szín visszafejtés
                    színszám = rekord.Szín;
                    if (színszám / 65536d > 1d)
                    {
                        kék = (int)(színszám / 65536d);
                        színszám -= kék * 65536d;
                    }
                    else
                        kék = 0d;

                    if (színszám / 256d > 1d)
                    {
                        zöld = (int)(színszám / 256d);
                        színszám += -zöld * 256d;
                    }
                    else
                        zöld = 0d;

                    piros = színszám;

                    Szín_Tábla.Rows[i].Cells[1].Style.BackColor = Color.FromArgb((int)Math.Round(piros), (int)Math.Round(zöld), (int)Math.Round(kék));
                }
                Szín_Tábla.Visible = true;
                Szín_Tábla.Refresh();

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

        private void Ablak_TW6000_Színkezelő_Load(object sender, EventArgs e)
        {
            Szín_tábla_kiírás();
        }

        private void Szín_Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 1) return;
            Vonal.Text = Szín_Tábla.Rows[e.RowIndex].Cells[0].Value.ToString();
            Színe.Text = Szín_Tábla.Rows[e.RowIndex].Cells[1].Value.ToString();
        }
    }
}
