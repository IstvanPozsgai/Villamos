using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.V_Ablakok._5_Karbantartás.T5C5
{
    public partial class Ablak_T5C5_Vonalak : Form
    {
        readonly Kezelő_Hétvége_Előírás KézElőírás = new Kezelő_Hétvége_Előírás();


        List<Adat_Hétvége_Előírás> AdatokElőírás = new List<Adat_Hétvége_Előírás>();
        public Ablak_T5C5_Vonalak()
        {
            InitializeComponent();
        }

        private void Ablak_T5C5_Vonalak_Load(object sender, EventArgs e)
        {

        }

        #region Vonalak lapfül
        private void Command9_színkereső_Click(object sender, EventArgs e)
        {
            try
            {
                ColorDialog ColorDialog1 = new ColorDialog();
                if (ColorDialog1.ShowDialog() != DialogResult.Cancel)
                {
                    Vonal_red.BackColor = ColorDialog1.Color;
                    Vonal_green.BackColor = ColorDialog1.Color;
                    Vonal_blue.BackColor = ColorDialog1.Color;

                    Vonal_red.Text = ColorDialog1.Color.R.ToString();
                    Vonal_green.Text = ColorDialog1.Color.G.ToString();
                    Vonal_blue.Text = ColorDialog1.Color.B.ToString();
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

        private void Command8_Új_Click(object sender, EventArgs e)
        {
            Vonal_kiürít();
        }

        private void Command7_Rögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Vonal_Vonal.Text.Trim() == "") throw new HibásBevittAdat("A Vonal beviteli mező nem lehet üres.");
                if (Vonal_Mennyiség.Text.Trim() == "") throw new HibásBevittAdat("A Mennyiség beviteli mező nem lehet üres.");
                if (Vonal_red.Text.Trim() == "") throw new HibásBevittAdat("A Szín beviteli mező nem lehet üres.");
                if (Vonal_green.Text.Trim() == "") throw new HibásBevittAdat("A Szín beviteli mező nem lehet üres.");
                if (Vonal_blue.Text.Trim() == "") throw new HibásBevittAdat("A Szín beviteli mező nem lehet üres.");
                if (!int.TryParse(Vonal_red.Text.Trim(), out int Red)) Red = 0;
                if (!int.TryParse(Vonal_green.Text.Trim(), out int Green)) Green = 0;
                if (!int.TryParse(Vonal_blue.Text.Trim(), out int Blue)) Blue = 0;
                if (!long.TryParse(Vonal_Mennyiség.Text.Trim(), out long Mennyiség)) Mennyiség = 0;
                if (!long.TryParse(Vonal_Id.Text.Trim(), out long Id)) Id = 0;


                Vonal_Vonal.Text = MyF.Szöveg_Tisztítás(Vonal_Vonal.Text, 0, 20);

                AdatokElőírás = KézElőírás.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adat_Hétvége_Előírás ElőírásElem = (from a in AdatokElőírás
                                                    where a.Id == Id
                                                    select a).FirstOrDefault();

                Adat_Hétvége_Előírás ADAT = new Adat_Hétvége_Előírás(
                    Id,
                    Vonal_Vonal.Text.Trim(),
                    Mennyiség,
                    Red,
                    Green,
                    Blue);

                if (ElőírásElem != null)
                    KézElőírás.Módosítás(Cmbtelephely.Text.Trim(), ADAT);
                else
                    KézElőírás.Rögzítés(Cmbtelephely.Text.Trim(), ADAT);

                Vonal_tábla_író();
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

        private void Command11_frissít_Click(object sender, EventArgs e)
        {
            Vonal_tábla_író();
        }

        private void Vonal_tábla_író()
        {
            try
            {
                AdatokElőírás = KézElőírás.Lista_Adatok(Cmbtelephely.Text.Trim());

                Vonal_tábla.Rows.Clear();
                Vonal_tábla.Columns.Clear();
                Vonal_tábla.Refresh();
                Vonal_tábla.Visible = false;
                Vonal_tábla.ColumnCount = 6;

                // fejléc elkészítése
                Vonal_tábla.Columns[0].HeaderText = "Sorszám";
                Vonal_tábla.Columns[0].Width = 100;
                Vonal_tábla.Columns[1].HeaderText = "Vonal";
                Vonal_tábla.Columns[1].Width = 200;
                Vonal_tábla.Columns[2].HeaderText = "Mennyiség";
                Vonal_tábla.Columns[2].Width = 200;
                Vonal_tábla.Columns[3].HeaderText = "Piros";
                Vonal_tábla.Columns[3].Width = 100;
                Vonal_tábla.Columns[4].HeaderText = "Zöld";
                Vonal_tábla.Columns[4].Width = 100;
                Vonal_tábla.Columns[5].HeaderText = "Kék";
                Vonal_tábla.Columns[5].Width = 100;

                foreach (Adat_Hétvége_Előírás rekord in AdatokElőírás)
                {
                    Vonal_tábla.RowCount++;
                    int i = Vonal_tábla.RowCount - 1;

                    Vonal_tábla.Rows[i].Cells[0].Value = rekord.Id;
                    Vonal_tábla.Rows[i].Cells[1].Value = rekord.Vonal;
                    Vonal_tábla.Rows[i].Cells[2].Value = rekord.Mennyiség;
                    Vonal_tábla.Rows[i].Cells[3].Value = rekord.Red;
                    Vonal_tábla.Rows[i].Cells[4].Value = rekord.Green;
                    Vonal_tábla.Rows[i].Cells[5].Value = rekord.Blue;
                    Vonal_tábla.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(rekord.Red, rekord.Green, rekord.Blue);
                    Vonal_tábla.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(rekord.Red, rekord.Green, rekord.Blue);
                    Vonal_tábla.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(rekord.Red, rekord.Green, rekord.Blue);
                }

                Vonal_tábla.Visible = true;
                Vonal_tábla.Refresh();

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

        private void Command10_Listát_töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (Vonal_Id.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelöve a törlendő tétel");
                if (!long.TryParse(Vonal_Id.Text, out long Id)) throw new HibásBevittAdat("Nincs kijelöve a törlendő tétel");
                AdatokElőírás = KézElőírás.Lista_Adatok(Cmbtelephely.Text.Trim());

                Adat_Hétvége_Előírás ElőírásElem = (from a in AdatokElőírás
                                                    where a.Id == Id
                                                    select a).FirstOrDefault();

                if (ElőírásElem != null)
                    KézElőírás.Törlés(Cmbtelephely.Text.Trim(), Id);

                Vonal_tábla_író();
                Vonal_kiürít();
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

        private void Vonal_kiürít()
        {
            Vonal_Id.Text = "";
            Vonal_Vonal.Text = "";
            Vonal_Mennyiség.Text = "";
            Vonal_red.Text = "";
            Vonal_green.Text = "";
            Vonal_blue.Text = "";
            Vonal_red.BackColor = Color.White;
            Vonal_green.BackColor = Color.White;
            Vonal_blue.BackColor = Color.White;
        }

        private void Vonal_tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                Vonal_Id.Text = Vonal_tábla.Rows[e.RowIndex].Cells[0].Value.ToString();
                Vonal_Vonal.Text = Vonal_tábla.Rows[e.RowIndex].Cells[1].Value.ToString();
                Vonal_Mennyiség.Text = Vonal_tábla.Rows[e.RowIndex].Cells[2].Value.ToString();

                Vonal_red.Text = Vonal_tábla.Rows[e.RowIndex].Cells[3].Value.ToString();
                Vonal_green.Text = Vonal_tábla.Rows[e.RowIndex].Cells[4].Value.ToString();
                Vonal_blue.Text = Vonal_tábla.Rows[e.RowIndex].Cells[5].Value.ToString();

                Vonal_red.BackColor = Color.FromArgb(int.Parse(Vonal_red.Text), int.Parse(Vonal_green.Text), int.Parse(Vonal_blue.Text));
                Vonal_green.BackColor = Color.FromArgb(int.Parse(Vonal_red.Text), int.Parse(Vonal_green.Text), int.Parse(Vonal_blue.Text));
                Vonal_blue.BackColor = Color.FromArgb(int.Parse(Vonal_red.Text), int.Parse(Vonal_green.Text), int.Parse(Vonal_blue.Text));
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

        private void Vonal_fel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Vonal_Id.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve Vonal.");
                if (!long.TryParse(Vonal_Id.Text.Trim(), out long ID)) ID = 0;
                if (ID <= 1) throw new HibásBevittAdat("Az első elemet nem lehet előrébb tenni.");
                KézElőírás.Csere(Cmbtelephely.Text.Trim(), ID);
                Vonal_tábla_író();
                Vonal_kiürít();
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
        #endregion
    }
}
