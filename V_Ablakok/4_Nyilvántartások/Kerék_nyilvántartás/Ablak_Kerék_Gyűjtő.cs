using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Adatszerkezet;
using MyEn = Villamos.V_MindenEgyéb.Enumok;

namespace Villamos.Villamos_Ablakok.Kerék_nyilvántartás
{
    public partial class Ablak_Kerék_Gyűjtő : Form
    {
        readonly Kezelő_Kerék_Tábla kéz = new Kezelő_Kerék_Tábla();
        readonly Kezelő_Kerék_Mérés KézMérés = new Kezelő_Kerék_Mérés();
        public string Pályaszám { get; private set; }
        public Ablak_Kerék_Gyűjtő(string pályaszám)
        {
            InitializeComponent();
            Pályaszám = pályaszám;
        }

        public Ablak_Kerék_Gyűjtő()
        {
            InitializeComponent();
        }

        private void Ablak_Kerék_Gyűjtő_Load(object sender, EventArgs e)
        {
            PályaszámTxt.Text = Pályaszám.Trim();
            Berendezés_adatok();
        }

        private void Berendezés_adatok()
        {
            try
            {
                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 8;

                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Psz";
                Tábla.Columns[0].Width = 70;
                Tábla.Columns[0].ReadOnly = true;
                Tábla.Columns[1].HeaderText = "Berendezésszám";
                Tábla.Columns[1].Width = 150;
                Tábla.Columns[1].ReadOnly = true;
                Tábla.Columns[2].HeaderText = "Gyári szám";
                Tábla.Columns[2].Width = 80;
                Tábla.Columns[2].ReadOnly = true;
                Tábla.Columns[3].HeaderText = "Pozíció";
                Tábla.Columns[3].Width = 80;
                Tábla.Columns[3].ReadOnly = true;
                Tábla.Columns[4].HeaderText = "Előző Állapot";
                Tábla.Columns[4].Width = 375;
                Tábla.Columns[4].ReadOnly = true;
                Tábla.Columns[5].HeaderText = "Előző Méret";
                Tábla.Columns[5].Width = 100;
                Tábla.Columns[5].ReadOnly = true;
                Tábla.Columns[6].HeaderText = "Új Állapot";
                Tábla.Columns[6].Width = 100;
                Tábla.Columns[7].HeaderText = "Új Méret";
                Tábla.Columns[7].Width = 100;

                List<Adat_Kerék_Tábla> Adatok = kéz.Lista_Adatok();
                Adatok = (from a in Adatok
                          where a.Azonosító.Trim() == Pályaszám.Trim()
                          && a.Objektumfajta.Trim() == "V.KERÉKPÁR"
                          orderby a.Pozíció.Trim()
                          select a).ToList();
                List<Adat_Kerék_Mérés> AdatokMérés = KézMérés.Lista_Adatok(DateTime.Today.Year);
                List<Adat_Kerék_Mérés> AdatokMérésE = KézMérés.Lista_Adatok(DateTime.Today.Year - 1);
                AdatokMérés.AddRange(AdatokMérésE);
                AdatokMérés = AdatokMérés.OrderByDescending(a => a.Mikor).ToList();

                foreach (Adat_Kerék_Tábla rekord in Adatok)
                {
                    Tábla.RowCount++;
                    int i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = rekord.Azonosító.Trim();
                    Tábla.Rows[i].Cells[1].Value = rekord.Kerékberendezés.Trim();
                    Tábla.Rows[i].Cells[2].Value = rekord.Kerékgyártásiszám.Trim();
                    Tábla.Rows[i].Cells[3].Value = rekord.Pozíció.Trim();
                    Adat_Kerék_Mérés EgyMérés = (from a in AdatokMérés
                                                 where a.Kerékberendezés == rekord.Kerékberendezés
                                                 select a).FirstOrDefault();
                    Tábla.Rows[i].Cells[4].Value = "_";
                    Tábla.Rows[i].Cells[5].Value = "_";
                    if (EgyMérés != null)
                    {
                        // ha van mérési adat akkor kiírjuk
                        Tábla.Rows[i].Cells[4].Value = MilyenÁllapot(EgyMérés.Állapot);
                        Tábla.Rows[i].Cells[5].Value = EgyMérés.Méret;
                    }
                    Tábla.Rows[i].Cells[6].Value = "";
                    Tábla.Rows[i].Cells[7].Value = "";
                }
                Tábla.Visible = true;
                Tábla.Refresh();
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

        string MilyenÁllapot(string Állapot)
        {
            string MilyenÁllapot = "";
            try
            {
                int szám = int.Parse(Állapot);
                MilyenÁllapot = ((MyEn.Kerék_Állapot)szám).ToString().Replace('_', ' ');
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
            return MilyenÁllapot;
        }

        private void Frissít_Click(object sender, EventArgs e)
        {
            Berendezés_adatok();
        }

        private void ValidateKeyPressEnum(object sender, KeyPressEventArgs e)
        {
            //Enum állapot
            if (!(((char)(e.KeyChar) >= 49 && (char)(e.KeyChar) <= 52) || (char)(e.KeyChar) == 8))
            {
                MessageBox.Show("Csak 1-4 közötti számot lehet beírni!");
                e.Handled = true;
            }
        }

        private void ValidateKeyPress(object sender, KeyPressEventArgs e)
        {
            //Kerék állapot
            if (!(((char)(e.KeyChar) >= 48 && (char)(e.KeyChar) <= 59) || (char)(e.KeyChar) == 8 ))
            {
                MessageBox.Show("Csak 0-9 közötti számot lehet beírni!");
                e.Handled = true;
            }
        }

        private void Tábla_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (Tábla.CurrentCell.ColumnIndex == 6) // put columnindextovalidate
            {
                e.Control.KeyPress -= ValidateKeyPressEnum;
                e.Control.KeyPress -= ValidateKeyPress;
                e.Control.KeyPress += ValidateKeyPressEnum;
            }

            if (Tábla.CurrentCell.ColumnIndex == 7) // put columnindextovalidate
            {
                e.Control.KeyPress -= ValidateKeyPressEnum;
                e.Control.KeyPress -= ValidateKeyPress;
                e.Control.KeyPress += ValidateKeyPress;
            }
        }

        private void Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (RögzítOka.Text.Trim() == "") throw new HibásBevittAdat("A rögzítés okát meg kell adni.");

                List<Adat_Kerék_Mérés> AdatokGy = new List<Adat_Kerék_Mérés>();
                for (int i = 0; i < Tábla.Rows.Count; i++)
                {
                    if (Tábla.Rows[i].Cells[6].Value.ToString() != "" && Tábla.Rows[i].Cells[7].Value.ToString() != "")
                    {
                        // csak akkor rögzítjük ha ki van töltve a mező
                        Adat_Kerék_Mérés Adat = new Adat_Kerék_Mérés(
                            Pályaszám.Trim(),
                            Tábla.Rows[i].Cells[3].Value.ToString().Trim(),
                            Tábla.Rows[i].Cells[1].Value.ToString().Trim(),
                            Tábla.Rows[i].Cells[2].Value.ToString().Trim(),
                            Tábla.Rows[i].Cells[6].Value.ToString().Trim(),
                            int.Parse(Tábla.Rows[i].Cells[7].Value.ToString()),
                            Program.PostásNév.Trim(),
                            DateTime.Now,
                            RögzítOka.Text.Trim(),
                            0);
                        AdatokGy.Add(Adat);
                    }
                }
                if (AdatokGy.Count > 0)
                {
                    KézMérés.Rögzítés(DateTime.Now.Year, AdatokGy);
                    MessageBox.Show("Az adat rögzítésre került!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

