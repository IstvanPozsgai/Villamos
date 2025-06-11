using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
using MyColor = Villamos.V_MindenEgyéb.Kezelő_Szín;

namespace Villamos.Villamos_Ablakok
{
    public partial class Ablak_Eszterga_Választék : Form
    {
        string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Törzs.mdb";
        string jelszó = "RónaiSándor";

        readonly Kezelő_Kerék_Eszterga_Tengely KézEsztergaTengely = new Kezelő_Kerék_Eszterga_Tengely();
        readonly Kezelő_Kerék_Eszterga_Tevékenység KézEsztergaTevékenység = new Kezelő_Kerék_Eszterga_Tevékenység();
        readonly Kezelő_Kerék_Eszterga_Automata KézAutomata = new Kezelő_Kerék_Eszterga_Automata();
        public Ablak_Eszterga_Választék()
        {
            InitializeComponent();
        }

        private void Ablak_Eszterga_Választék_Load(object sender, EventArgs e)
        {
            Tev_Tábla_frissítés_esemény();
            Norm_Típus_feltöltés();
            Norma_Tábla_kiír();
            Felhasználók_Feltöltése();
            UtolsóÜzenet.Value = DateTime.Now;
            AutomataTáblaÍró();
        }



        private void Ablak_Eszterga_Választék_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC
            if ((int)e.KeyCode == 27)
            {
                this.Close();
            }
        }


        #region Tevékenység
        private void Tev_Tábla_frissítés_Click(object sender, EventArgs e)
        {
            Tev_Tábla_frissítés_esemény();
        }


        void Tev_Tábla_frissítés_esemény()
        {
            try
            {

                string szöveg = "SELECT * FROM Tevékenység ORDER BY id";


                Tevékenység_Tábla.Rows.Clear();
                Tevékenység_Tábla.Columns.Clear();
                Tevékenység_Tábla.Refresh();
                Tevékenység_Tábla.Visible = false;
                Tevékenység_Tábla.ColumnCount = 6;

                // fejléc elkészítése
                Tevékenység_Tábla.Columns[0].HeaderText = "Id.";
                Tevékenység_Tábla.Columns[0].Width = 50;
                Tevékenység_Tábla.Columns[1].HeaderText = "Tevékenység";
                Tevékenység_Tábla.Columns[1].Width = 220;
                Tevékenység_Tábla.Columns[2].HeaderText = "Munkaidő";
                Tevékenység_Tábla.Columns[2].Width = 100;
                Tevékenység_Tábla.Columns[3].HeaderText = "Háttérszín";
                Tevékenység_Tábla.Columns[3].Width = 100;
                Tevékenység_Tábla.Columns[4].HeaderText = "Betűszín";
                Tevékenység_Tábla.Columns[4].Width = 100;
                Tevékenység_Tábla.Columns[5].HeaderText = "Helyben marad";
                Tevékenység_Tábla.Columns[5].Width = 150;


                List<Adat_Kerék_Eszterga_Tevékenység> Adatok = KézEsztergaTevékenység.Lista_Adatok(hely, jelszó, szöveg);
                int i;
                Szín_kódolás Szín;

                foreach (Adat_Kerék_Eszterga_Tevékenység rekord in Adatok)
                {
                    Tevékenység_Tábla.RowCount++;
                    i = Tevékenység_Tábla.RowCount - 1;
                    Tevékenység_Tábla.Rows[i].Cells[0].Value = rekord.Id;

                    Tevékenység_Tábla.Rows[i].Cells[1].Value = rekord.Tevékenység.Trim();
                    Szín = MyColor.Szín_váltó(rekord.Háttérszín);
                    Tevékenység_Tábla.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);
                    Szín = MyColor.Szín_váltó(rekord.Betűszín);
                    Tevékenység_Tábla.Rows[i].Cells[1].Style.ForeColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);
                    Tevékenység_Tábla.Rows[i].Cells[2].Value = rekord.Munkaidő;
                    Tevékenység_Tábla.Rows[i].Cells[3].Value = rekord.Háttérszín;
                    Tevékenység_Tábla.Rows[i].Cells[4].Value = rekord.Betűszín;
                    Tevékenység_Tábla.Rows[i].Cells[5].Value = rekord.Marad ? "Igen" : "Nem";
                }
                Tevékenység_Tábla.Refresh();
                Tevékenység_Tábla.Visible = true;
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

        private void Tev_Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tev_Tevékenység.Text.Trim() == "") throw new HibásBevittAdat("Tevékenység mezőt ki kell tölteni.");
                if (Tev_idő.Text.Trim() == "" || !int.TryParse(Tev_idő.Text, out int result)) throw new HibásBevittAdat("Munkaidő mértékét meg kell adni és egész szám lehet.");

                string szöveg = "SELECT * FROM Tevékenység ORDER BY id desc";
                List<Adat_Kerék_Eszterga_Tevékenység> Adatok = KézEsztergaTevékenység.Lista_Adatok(hely, jelszó, szöveg);

                long ID = 1;
                if (Adatok.Count > 0) ID = Adatok.Max(a => a.Id) + 1;
                Tev_Id.Text = ID.ToString();


                if (Tev_Háttér.Text.Trim() == "" || !long.TryParse(Tev_Háttér.Text.Trim(), out long háttér)) Tev_Háttér.Text = "12632256";
                if (Tev_Betű.Text.Trim() == "" || !long.TryParse(Tev_Betű.Text.Trim(), out long betű)) Tev_Betű.Text = "0";

                Adat_Kerék_Eszterga_Tevékenység Elem = (from a in Adatok
                                                        where a.Id == ID
                                                        select a).FirstOrDefault();
                szöveg = $"SELECT * FROM Tevékenység WHERE id={Tev_Id.Text.Trim()}";

                if (Elem != null)
                {
                    szöveg = "UPDATE Tevékenység  SET ";
                    szöveg += $" Tevékenység='{Tev_Tevékenység.Text.Trim()}', ";
                    szöveg += $" HáttérSzín={Tev_Háttér.Text.Trim()}, ";
                    szöveg += $" BetűSzín={Tev_Betű.Text.Trim()}, ";
                    szöveg += $" munkaidő={Tev_idő.Text.Trim()}, ";
                    szöveg += $" marad={Marad.Checked} ";
                    szöveg += $" WHERE id={Tev_Id.Text.Trim()}";
                }
                else
                {
                    szöveg = "INSERT INTO Tevékenység  (Id, Tevékenység, Munkaidő, HáttérSzín, BetűSzín, marad) VALUES (";
                    szöveg += $" {ID}, '{Tev_Tevékenység.Text.Trim()}', {Tev_idő.Text.Trim()}, " +
                              $"{Tev_Háttér.Text.Trim()}, {Tev_Betű.Text.Trim()}, {Marad.Checked})";
                }

                MyA.ABMódosítás(hely, jelszó, szöveg);

                Tev_Tábla_frissítés_esemény();
                MessageBox.Show("Az adatok rögzítésre kerültek!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private void Tev_Új_Click(object sender, EventArgs e)
        {
            Tev_Betű.Text = "";
            Tev_Betű.BackColor = Color.White;
            Tev_Háttér.BackColor = Color.White;
            Tev_Háttér.Text = "";
            Tev_Id.Text = "";
            Tev_Tevékenység.Text = "";
            Marad.Checked = false;
            Tev_Tevékenység.Focus();

        }


        private void Tevékenység_Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Tev_Id.Text = Tevékenység_Tábla.Rows[e.RowIndex].Cells[0].Value.ToString();
            Tev_Tevékenység.Text = Tevékenység_Tábla.Rows[e.RowIndex].Cells[1].Value.ToString();
            Tev_idő.Text = Tevékenység_Tábla.Rows[e.RowIndex].Cells[2].Value.ToString();
            Tev_Háttér.Text = Tevékenység_Tábla.Rows[e.RowIndex].Cells[3].Value.ToString();
            Tev_Betű.Text = Tevékenység_Tábla.Rows[e.RowIndex].Cells[4].Value.ToString();
            Marad.Checked = Tevékenység_Tábla.Rows[e.RowIndex].Cells[5].Value.ToString() == "Igen" ? true : false;
            Kiválasztott_sor = e.RowIndex;
        }

        private void Színválasztás_Háttér_Click(object sender, EventArgs e)
        {
            try
            {
                ColorDialog ColorDialog1 = new ColorDialog();
                // ha nem mégsemmel tér vissza a színválasztásból
                if (ColorDialog1.ShowDialog() != DialogResult.Cancel)
                {
                    double piros = ColorDialog1.Color.R;
                    double zöld = ColorDialog1.Color.G;
                    double kék = ColorDialog1.Color.B;

                    Tev_Háttér.Text = (piros + zöld * 256d + kék * 65536d).ToString();

                    Tev_Háttér.BackColor = ColorDialog1.Color;
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

        private void Színválasztás_Betű_Click(object sender, EventArgs e)
        {
            try
            {
                ColorDialog ColorDialog1 = new ColorDialog();
                // ha nem mégsemmel tér vissza a színválasztásból
                if (ColorDialog1.ShowDialog() != DialogResult.Cancel)
                {
                    double piros = ColorDialog1.Color.R;
                    double zöld = ColorDialog1.Color.G;
                    double kék = ColorDialog1.Color.B;

                    Tev_Betű.Text = (piros + zöld * 256d + kék * 65536d).ToString();

                    Tev_Betű.BackColor = ColorDialog1.Color;
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

        private void Töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tev_Tevékenység.Text.Trim() == "")
                    throw new HibásBevittAdat("Tevékenység mezőt ki kell tölteni.");
                string szöveg = $"DELETE FROM Tevékenység   WHERE id={Tev_Id.Text.Trim()}";
                MyA.ABtörlés(hely, jelszó, szöveg);

                Rendezés();
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

        private void Rendezés()
        {
            try
            {
                string szöveg = "SELECT * FROM Tevékenység ORDER BY id";
                Kezelő_Kerék_Eszterga_Tevékenység kéz = new Kezelő_Kerék_Eszterga_Tevékenység();
                List<Adat_Kerék_Eszterga_Tevékenység> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);
                szöveg = $"DELETE FROM Tevékenység";
                MyA.ABtörlés(hely, jelszó, szöveg);
                int i = 1;
                foreach (Adat_Kerék_Eszterga_Tevékenység rekord in Adatok)
                {
                    szöveg = "INSERT INTO Tevékenység  (Id, Tevékenység, Munkaidő, HáttérSzín, BetűSzín) VALUES (";
                    szöveg += $" {i}, '{rekord.Tevékenység.Trim()}', {rekord.Munkaidő}, {rekord.Háttérszín}, {rekord.Betűszín} )";
                    i++;
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                }
                Tev_Tábla_frissítés_esemény();
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

        int Kiválasztott_sor = -1;
        private void Lista_Tábla_Click(object sender, EventArgs e)
        {
            try
            {
                if (Kiválasztott_sor < 1)
                    throw new HibásBevittAdat("Nincs kiválasztva érvényes elem a táblázatban.");

                Kezelő_Kerék_Eszterga_Tevékenység kéz = new Kezelő_Kerék_Eszterga_Tevékenység();
                string szöveg = $"SELECT * FROM tevékenység WHERE id={Tevékenység_Tábla.Rows[Kiválasztott_sor].Cells[0].Value.ToString()}";
                Adat_Kerék_Eszterga_Tevékenység Első = kéz.Egy_Adat(hely, jelszó, szöveg);
                szöveg = $"SELECT * FROM tevékenység WHERE id={Tevékenység_Tábla.Rows[Kiválasztott_sor - 1].Cells[0].Value.ToString()}";
                Adat_Kerék_Eszterga_Tevékenység Második = kéz.Egy_Adat(hely, jelszó, szöveg);

                //Rögzítjük keresztben
                szöveg = "UPDATE tevékenység SET";
                szöveg += $" Tevékenység='{Első.Tevékenység.Trim()}', ";
                szöveg += $" HáttérSzín={Első.Háttérszín}, ";
                szöveg += $" BetűSzín={Első.Betűszín}, ";
                szöveg += $" munkaidő={Első.Munkaidő}, ";
                szöveg += $" marad={Első.Marad} ";
                szöveg += $" WHERE id={Második.Id}";
                MyA.ABMódosítás(hely, jelszó, szöveg);

                szöveg = "UPDATE tevékenység SET";
                szöveg += $" Tevékenység='{Második.Tevékenység.Trim()}', ";
                szöveg += $" HáttérSzín={Második.Háttérszín}, ";
                szöveg += $" BetűSzín={Második.Betűszín}, ";
                szöveg += $" munkaidő={Második.Munkaidő}, ";
                szöveg += $" marad={Második.Marad} ";
                szöveg += $" WHERE id={Első.Id}";
                MyA.ABMódosítás(hely, jelszó, szöveg);

                Kiválasztott_sor = -1;

                Tev_Tábla_frissítés_esemény();
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


        #region Normaidők

        private void Norm_Típus_feltöltés()
        {
            try
            {
                Kezelő_Jármű KézJármű = new Kezelő_Jármű();
                List<Adat_Jármű> Adatok = KézJármű.Lista_Adatok("Főmérnökség").OrderBy(a => a.Valóstípus2).ToList();
                List<string> Típusok = Adatok.Select(a => a.Valóstípus2).Distinct().ToList();

                Norm_Típus.Items.Clear();
                foreach (string Elem in Típusok)
                    Norm_Típus.Items.Add(Elem);
                Norm_Típus.Refresh();
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



        private void Norm_Frissít_Click(object sender, EventArgs e)
        {
            Norma_Tábla_kiír();
        }


        void Norma_Tábla_kiír()
        {
            try
            {
                string szöveg = "SELECT * FROM tengely ORDER BY típus, állapot";

                Norma_Tábla.Rows.Clear();
                Norma_Tábla.Columns.Clear();
                Norma_Tábla.Refresh();
                Norma_Tábla.Visible = false;
                Norma_Tábla.ColumnCount = 3;

                // fejléc elkészítése
                Norma_Tábla.Columns[0].HeaderText = "Típus";
                Norma_Tábla.Columns[0].Width = 200;
                Norma_Tábla.Columns[2].HeaderText = "Norma idő";
                Norma_Tábla.Columns[2].Width = 180;
                Norma_Tábla.Columns[1].HeaderText = "Állapot";
                Norma_Tábla.Columns[1].Width = 180;


                List<Adat_Kerék_Eszterga_Tengely> Adatok = KézEsztergaTengely.Lista_Adatok(hely, jelszó, szöveg);
                int i;


                foreach (Adat_Kerék_Eszterga_Tengely rekord in Adatok)
                {
                    Norma_Tábla.RowCount++;
                    i = Norma_Tábla.RowCount - 1;
                    Norma_Tábla.Rows[i].Cells[0].Value = rekord.Típus;
                    Norma_Tábla.Rows[i].Cells[2].Value = rekord.Munkaidő;
                    Norma_Tábla.Rows[i].Cells[1].Value = rekord.Állapot;
                }
                Norma_Tábla.Refresh();
                Norma_Tábla.Visible = true;
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


        private void Norm_Rögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Norm_Típus.Text.Trim() == "") throw new HibásBevittAdat("Típust meg kell adni.");
                if (Norm_Munkaidő.Text.Trim() == "" || !int.TryParse(Norm_Munkaidő.Text, out int Munkaidő)) throw new HibásBevittAdat("A munkaidőnek pozítív egész számnak kell lennie.");
                if (Munkaidő < 0) throw new HibásBevittAdat("A munkaidőnek nagyobbnak kell lennie nullánál.");
                if (Állapot.Text.Trim() == "" || !int.TryParse(Állapot.Text, out int Állapota)) throw new HibásBevittAdat("Az állapotnak pozítív egész számnak kell lennie.");
                if (Állapota < 0) throw new HibásBevittAdat("Az állapotnak nagyobbnak kell lennie nullánál.");

                string szöveg = $"SELECT * FROM tengely";
                List<Adat_Kerék_Eszterga_Tengely> Adatok = KézEsztergaTengely.Lista_Adatok(hely, jelszó, szöveg);

                Adat_Kerék_Eszterga_Tengely Elem = (from a in Adatok
                                                    where a.Típus == Norm_Típus.Text.Trim()
                                                    && a.Állapot == Állapota
                                                    select a).FirstOrDefault();

                Adat_Kerék_Eszterga_Tengely Ideig = new Adat_Kerék_Eszterga_Tengely(Norm_Típus.Text.Trim(), Munkaidő, Állapota);

                if (Elem != null)
                {
                    KézEsztergaTengely.Egy_Módosítás(hely, jelszó, Ideig);
                }
                else
                {
                    //Új
                    KézEsztergaTengely.Egy_Rögzítés(hely, jelszó, Ideig);
                }

                Norma_Tábla_kiír();
                MessageBox.Show("Az adatok rögzítésre kerültek!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Norma_Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            Norm_Típus.Text = Norma_Tábla.Rows[e.RowIndex].Cells[0].Value.ToString();
            Norm_Munkaidő.Text = Norma_Tábla.Rows[e.RowIndex].Cells[2].Value.ToString();
            Állapot.Text = Norma_Tábla.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        #endregion


        #region Automata

        private void Felhasználók_Feltöltése()
        {
            try
            {
                Kezelő_Belépés_Bejelentkezés KézBejelen = new Kezelő_Belépés_Bejelentkezés();
                List<Adat_Belépés_Bejelentkezés> Adatok = KézBejelen.Lista_Adatok("Baross").OrderBy(a => a.Név).ToList();

                Felhasználók.Items.Clear();
                foreach (Adat_Belépés_Bejelentkezés Elem in Adatok)
                    Felhasználók.Items.Add(Elem.Név);

                Felhasználók.Refresh();
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


        private void OKAutomata_Click(object sender, EventArgs e)
        {
            try
            {
                if (Felhasználók.Text.Trim() == "") throw new HibásBevittAdat("A felhasználóinév nem lehet üres");

                string szöveg = $"SELECT * FROM Automata ";
                List<Adat_Kerék_Eszterga_Automata> Adatok = KézAutomata.Lista_Adatok(hely, jelszó, szöveg);

                Adat_Kerék_Eszterga_Automata Elem = (from a in Adatok
                                                     where a.FelhasználóiNév == Felhasználók.Text.Trim()
                                                     select a).FirstOrDefault();

                if (Elem == null)
                    szöveg = $"INSERT INTO Automata (FelhasználóiNév, UtolsóÜzenet) VALUES ( '{Felhasználók.Text.Trim()}', '{UtolsóÜzenet.Value.ToString("yyyy.MM.dd")}')";
                else
                    szöveg = $"UPDATE Automata SET UtolsóÜzenet='{UtolsóÜzenet.Value.ToString("yyyy.MM.dd")}' WHERE FelhasználóiNév='{Felhasználók.Text.Trim()}'";

                MyA.ABMódosítás(hely, jelszó, szöveg);
                AutomataTáblaÍró();
                MessageBox.Show("Az adatok rögzítésre kerültek!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private void TörlésAutomata_Click(object sender, EventArgs e)
        {
            try
            {
                if (Felhasználók.Text.Trim() == "") throw new HibásBevittAdat("A felhasználóinév nem lehet üres");

                string szöveg = $"SELECT * FROM Automata ";
                List<Adat_Kerék_Eszterga_Automata> Adatok = KézAutomata.Lista_Adatok(hely, jelszó, szöveg);

                Adat_Kerék_Eszterga_Automata Elem = (from a in Adatok
                                                     where a.FelhasználóiNév == Felhasználók.Text.Trim()
                                                     select a).FirstOrDefault();

                if (Elem != null)
                {
                    szöveg = $"DELETE FROM Automata  WHERE FelhasználóiNév='{Felhasználók.Text.Trim()}'";
                    MyA.ABtörlés(hely, jelszó, szöveg);
                    AutomataTáblaÍró();
                    MessageBox.Show("Az adatok törlésre kerültek!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private void FrissítAutomata_Click(object sender, EventArgs e)
        {
            AutomataTáblaÍró();
        }


        void AutomataTáblaÍró()
        {
            try
            {
                string szöveg = "SELECT * FROM automata ORDER BY FelhasználóiNév";

                TáblaAutomata.Rows.Clear();
                TáblaAutomata.Columns.Clear();
                TáblaAutomata.Refresh();
                TáblaAutomata.Visible = false;
                TáblaAutomata.ColumnCount = 2;

                // fejléc elkészítése
                TáblaAutomata.Columns[0].HeaderText = "Felhasználó";
                TáblaAutomata.Columns[0].Width = 200;
                TáblaAutomata.Columns[1].HeaderText = "Utolsó küldés";
                TáblaAutomata.Columns[1].Width = 180;



                List<Adat_Kerék_Eszterga_Automata> Adatok = KézAutomata.Lista_Adatok(hely, jelszó, szöveg);
                int i;


                foreach (Adat_Kerék_Eszterga_Automata rekord in Adatok)
                {
                    TáblaAutomata.RowCount++;
                    i = TáblaAutomata.RowCount - 1;
                    TáblaAutomata.Rows[i].Cells[0].Value = rekord.FelhasználóiNév;
                    TáblaAutomata.Rows[i].Cells[1].Value = rekord.UtolsóÜzenet.ToString("yyyy.MM.dd");

                }
                TáblaAutomata.Refresh();
                TáblaAutomata.Visible = true;
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


        private void TáblaAutomata_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            Felhasználók.Text = TáblaAutomata.Rows[e.RowIndex].Cells[0].Value.ToString();
            UtolsóÜzenet.Value = DateTime.Parse(TáblaAutomata.Rows[e.RowIndex].Cells[1].Value.ToString());
        }

        #endregion
    }
}
