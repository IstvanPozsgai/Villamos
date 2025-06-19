using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Adatszerkezet;
using MyColor = Villamos.V_MindenEgyéb.Kezelő_Szín;

namespace Villamos.Villamos_Ablakok
{
    public partial class Ablak_Eszterga_Választék : Form
    {
        readonly Kezelő_Kerék_Eszterga_Tengely KézEsztergaTengely = new Kezelő_Kerék_Eszterga_Tengely();
        readonly Kezelő_Kerék_Eszterga_Tevékenység KézEsztergaTevékenység = new Kezelő_Kerék_Eszterga_Tevékenység();
        readonly Kezelő_Kerék_Eszterga_Automata KézAutomata = new Kezelő_Kerék_Eszterga_Automata();
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly Kezelő_Belépés_Bejelentkezés KézBejelen = new Kezelő_Belépés_Bejelentkezés();

        public Ablak_Eszterga_Választék()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            Tev_Tábla_frissítés_esemény();
            Norm_Típus_feltöltés();
            Norma_Tábla_kiír();
            Felhasználók_Feltöltése();
            UtolsóÜzenet.Value = DateTime.Now;
            AutomataTáblaÍró();
        }

        private void Ablak_Eszterga_Választék_Load(object sender, EventArgs e)
        { }

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

        private void Tev_Tábla_frissítés_esemény()
        {
            try
            {
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

                List<Adat_Kerék_Eszterga_Tevékenység> Adatok = KézEsztergaTevékenység.Lista_Adatok();
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
                if (Tev_idő.Text.Trim() == "" || !double.TryParse(Tev_idő.Text, out double idő)) throw new HibásBevittAdat("Munkaidő mértékét meg kell adni és egész szám lehet.");
                if (!int.TryParse(Tev_Id.Text.Trim(), out int ID)) ID = 0;
                if (Tev_Háttér.Text.Trim() == "" || !long.TryParse(Tev_Háttér.Text.Trim(), out long háttér)) háttér = 12632256;
                if (Tev_Betű.Text.Trim() == "" || !long.TryParse(Tev_Betű.Text.Trim(), out long betű)) betű = 0;

                Adat_Kerék_Eszterga_Tevékenység ADAT = new Adat_Kerék_Eszterga_Tevékenység(
                          Tev_Tevékenység.Text.Trim(),
                          idő,
                          betű,
                          háttér,
                          ID,
                          Marad.Checked);
                KézEsztergaTevékenység.Döntés(ADAT);
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
            Ürítés();
        }

        private void Ürítés()
        {
            Tev_idő.Text = "";
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
            Marad.Checked = Tevékenység_Tábla.Rows[e.RowIndex].Cells[5].Value.ToString() == "Igen";
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
                if (Tev_Tevékenység.Text.Trim() == "") throw new HibásBevittAdat("Tevékenység mezőt ki kell tölteni.");
                if (!int.TryParse(Tev_Id.Text.Trim(), out int ID)) throw new HibásBevittAdat("A sorszám mező nem tartalmaz számot.");
                KézEsztergaTevékenység.Törlés(ID);
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
                KézEsztergaTevékenység.Rendezés();
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

        private void Feljebb_Click(object sender, EventArgs e)
        {
            try
            {

                if (!int.TryParse(Tev_Id.Text, out int ID)) throw new HibásBevittAdat("Nincs kiválasztva érvényes elem a táblázatban.");
                if (ID == 1) throw new HibásBevittAdat("Az első elem nem mozgatható feljebb.");
                KézEsztergaTevékenység.Feljebb(ID);
                Ürítés();
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

        private void Norma_Tábla_kiír()
        {
            try
            {
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

                List<Adat_Kerék_Eszterga_Tengely> Adatok = KézEsztergaTengely.Lista_Adatok();
                Adatok = (from a in Adatok
                          orderby a.Típus, a.Állapot
                          select a).ToList();

                foreach (Adat_Kerék_Eszterga_Tengely rekord in Adatok)
                {
                    Norma_Tábla.RowCount++;
                    int i = Norma_Tábla.RowCount - 1;
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

                Adat_Kerék_Eszterga_Tengely ADAT = new Adat_Kerék_Eszterga_Tengely(
                    Norm_Típus.Text.Trim(),
                    Munkaidő,
                    Állapota);
                KézEsztergaTengely.Döntés(ADAT);

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

                Adat_Kerék_Eszterga_Automata ADAT = new Adat_Kerék_Eszterga_Automata(
                         Felhasználók.Text.Trim(),
                         UtolsóÜzenet.Value);
                KézAutomata.Döntés(ADAT);

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

                List<Adat_Kerék_Eszterga_Automata> Adatok = KézAutomata.Lista_Adatok();
                Adat_Kerék_Eszterga_Automata Elem = (from a in Adatok
                                                     where a.FelhasználóiNév == Felhasználók.Text.Trim()
                                                     select a).FirstOrDefault();

                if (Elem != null)
                {
                    KézAutomata.Törlés(Felhasználók.Text.Trim());
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

        private void AutomataTáblaÍró()
        {
            try
            {
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

                List<Adat_Kerék_Eszterga_Automata> Adatok = KézAutomata.Lista_Adatok().OrderBy(a => a.FelhasználóiNév).ToList();
                foreach (Adat_Kerék_Eszterga_Automata rekord in Adatok)
                {
                    TáblaAutomata.RowCount++;
                    int i = TáblaAutomata.RowCount - 1;
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
