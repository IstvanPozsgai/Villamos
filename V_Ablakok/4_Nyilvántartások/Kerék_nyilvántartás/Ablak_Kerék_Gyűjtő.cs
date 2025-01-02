using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Villamos_Ablakok.Kerék_nyilvántartás
{
    public partial class Ablak_Kerék_Gyűjtő : Form
    {

        public string Pályaszám { get; private set; }
        public Ablak_Kerék_Gyűjtő(string pályaszám)
        {
            InitializeComponent();
            Pályaszám = pályaszám;
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
                string hely, jelszó, szöveg;



                hely = Application.StartupPath + @"\Főmérnökség\adatok\Kerék.mdb";
                jelszó = "szabólászló";
                szöveg = "";

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
                Tábla.Columns[3].Width =80;
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

                int i;


                szöveg = $"SELECT * FROM tábla where [azonosító]='{Pályaszám.Trim()}' AND objektumfajta='V.KERÉKPÁR' order by pozíció ";

                Kezelő_Kerék_Tábla kéz = new Kezelő_Kerék_Tábla();
                List<Adat_Kerék_Tábla> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);

                foreach (Adat_Kerék_Tábla rekord in Adatok)
                {

                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = rekord.Azonosító.Trim();
                    Tábla.Rows[i].Cells[1].Value = rekord.Kerékberendezés.Trim();
                    Tábla.Rows[i].Cells[2].Value = rekord.Kerékgyártásiszám.Trim();
                    Tábla.Rows[i].Cells[3].Value = rekord.Pozíció.Trim();
                    Tábla.Rows[i].Cells[4].Value = "_";
                    Tábla.Rows[i].Cells[5].Value = "_";
                    Tábla.Rows[i].Cells[6].Value = "";
                    Tábla.Rows[i].Cells[7].Value = "";
                }

                Tábla.Visible = true;
                Tábla.Refresh(); 
                if (Tábla.Rows.Count > 0)
                {
                    hely = Application.StartupPath + @"\Főmérnökség\adatok\" + DateTime.Today.ToString("yyyy") + @"\telepikerék.mdb";
                    Mérésieredmények(hely, jelszó);
                    hely = Application.StartupPath + @"\Főmérnökség\adatok\" + (DateTime.Today.ToString("yyyy").ToÉrt_Int() - 1).ToString() + @"\telepikerék.mdb";
                    Mérésieredmények(hely, jelszó);
                    Tábla.Sort(Tábla.Columns[3], System.ComponentModel.ListSortDirection.Ascending);
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
        private void Mérésieredmények(String helykerék, String jelszó)
        {
            try
            {

                int hiba = 0;
                if (File.Exists(helykerék) == true)
                {
                    string szöveg;
                    Tábla.Sort(Tábla.Columns[1], System.ComponentModel.ListSortDirection.Ascending);

                    szöveg = "SELECT * FROM keréktábla where azonosító='" + Pályaszám.Trim() + "'";
                    szöveg += " order by kerékberendezés asc, mikor desc";

                    Kezelő_Kerék_Mérés kéz = new Kezelő_Kerék_Mérés();
                    List<Adat_Kerék_Mérés> Adatok = kéz.Lista_Adatok(helykerék, jelszó, szöveg);

                    int i = 0;
                    foreach (Adat_Kerék_Mérés rekord in Adatok)
                    {


                        // ha kisebb a táblázatban lévő szám akkor addig növeljük amíg egyenlő nem lesz
                        while (String.Compare(Tábla.Rows[i].Cells[1].Value.ToString().Trim(), rekord.Kerékberendezés.Trim()) < 0)
                        {
                            i += 1;
                            if (i == Tábla.Rows.Count)
                            {
                                hiba = 1;
                                break;
                            }
                        }
                        if (hiba == 1)
                            break;
                        while (string.Compare(Tábla.Rows[i].Cells[1].Value.ToString().Trim(), rekord.Kerékberendezés.Trim()) <= 0)
                        {

                            if (Tábla.Rows[i].Cells[1].Value.ToString().Trim() == rekord.Kerékberendezés.Trim())
                            {
                                // ha egyforma akkor kiírjuk
                                if (Tábla.Rows[i].Cells[5].Value.ToString().Trim() == "_")
                                {
                                    Tábla.Rows[i].Cells[4].Value = MilyenÁllapot(rekord.Állapot);
                                    Tábla.Rows[i].Cells[5].Value = rekord.Méret;
                                }
                            }
                            i += 1;
                            if (i == Tábla.Rows.Count)
                            {
                                hiba = 1;
                                break;
                            }
                        }
                        if (hiba == 1)
                            break;
                    }

                    Tábla.Visible = true;
                    Tábla.Refresh(); 
                    Tábla.ClearSelection();
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

        string MilyenÁllapot(string Állapot)
        {
            string MilyenÁllapot = "";
            switch (Állapot.Trim().Substring(0, 1))
            {
                case "1":
                    MilyenÁllapot = "1 Frissen esztergált";
                    break;
                case "2":
                    MilyenÁllapot = "2 Üzemszerűen kopott forgalomban";
                    break;
                case "3":
                    MilyenÁllapot = "3 Forgalomképes esztergálandó";
                    break;
                case "4":
                    MilyenÁllapot = "4 Forgalomképtelen azonnali esztergálást igényel";
                    break;
            }
            return MilyenÁllapot;

        }

        private void Frissít_Click(object sender, EventArgs e)
        {
            Berendezés_adatok();
        }

        private void ValidateKeyPress(object sender, KeyPressEventArgs e)
        {
            int Állapot = 0;
            //Kerék állapot
            if ((char)(e.KeyChar) != 13 && (char)(e.KeyChar) != 8 && !int.TryParse(e.KeyChar.ToString(), out Állapot))
            {
                MessageBox.Show("Csak egész számot lehet beírni!");
                e.Handled = true;
            }
        }



        private void Tábla_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (Tábla.CurrentCell.ColumnIndex == 6) // put columnindextovalidate
            {
                e.Control.KeyPress -= ValidateKeyPress;
                e.Control.KeyPress -= ValidateKeyPress;
                e.Control.KeyPress += ValidateKeyPress;
            }

            if (Tábla.CurrentCell.ColumnIndex == 7) // put columnindextovalidate
            {
                e.Control.KeyPress -= ValidateKeyPress;
                e.Control.KeyPress -= ValidateKeyPress;
                e.Control.KeyPress += ValidateKeyPress;
            }
        }

        private void Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (RögzítOka.Text.Trim() == "")
                    throw new HibásBevittAdat("A rögzítés okát meg kell adni.");
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\" + DateTime.Today.ToString("yyyy") + @"\telepikerék.mdb";

                string jelszó = "szabólászló";
                Kezelő_Kerék_Mérés kéz = new Kezelő_Kerék_Mérés();


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
                            0
                            );

                        kéz.Rögzít(hely, jelszó, Adat);
                        MessageBox.Show("Az adat rögzítésre került!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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

