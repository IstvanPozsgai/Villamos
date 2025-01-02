using System; 
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Villamos.Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok.TW6000
{
    public partial class Ablak_TW6000_Telephely : Form
    {

        readonly string TW6000_Villamos = $@"{Application.StartupPath}\Főmérnökség\adatok\villamos4TW.mdb";
        readonly Kezelő_TW600_Telephely kéz = new Kezelő_TW600_Telephely();
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
                TelephelyListaFeltöltés();

                string hely = TW6000_Villamos;
                string jelszó = "czapmiklós";

                string szöveg;
                Adat_TW6000_Telephely Elem = (from a in Adatok
                                              where a.Telephely == Üzemek.Text.Trim()
                                              select a).FirstOrDefault ();

                if (Elem==null)
                {
                    // új rögzítés
                    szöveg = "INSERT INTO telephely (telephely, sorrend) VALUES (";
                    szöveg += $"'{Üzemek.Text.Trim()}', ";
                    szöveg += $"{Sorszám})";
                }
                else
                {
                    // meglévő módosítás
                    szöveg = $"UPDATE  telephely SET sorrend={Üzem_sorszám.Text.Trim()}";
                    szöveg += $" WHERE telephely='{Üzemek.Text.Trim()}'";

                }
                MyA.ABMódosítás(hely, jelszó, szöveg);
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
                TelephelyListaFeltöltés();
                string hely = TW6000_Villamos;
                string jelszó = "czapmiklós";

                string szöveg;
                Adat_TW6000_Telephely Elem = (from a in Adatok
                                              where a.Telephely == Üzemek.Text.Trim()
                                              select a).FirstOrDefault();


                if (Elem!=null)
                {
                    szöveg = $"DELETE FROM telephely where telephely='{Üzemek.Text.Trim()}'";
                    MyA.ABtörlés(hely, jelszó, szöveg);
                }
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

        private void TelephelyListaFeltöltés()
        {
            try
            {
                Adatok.Clear();
                string hely = TW6000_Villamos.Trim();
                string jelszó = "czapmiklós";
                string szöveg = "SELECT * FROM telephely order by sorrend";

                Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);
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
                TelephelyListaFeltöltés();

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
            Üzemek.Items.Clear();

            string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő.mdb";
            string jelszó = "Mocó";
            string szöveg = "SELECT * FROM telephelytábla ORDER BY sorszám";

            Üzemek.BeginUpdate();
            Üzemek.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "telephelykönyvtár"));
            Üzemek.EndUpdate();
            Üzemek.Refresh();
        }

        private void Ablak_TW6000_Telephely_Load(object sender, EventArgs e)
        {
            Üzemeklista_feltöltése();
            Telephely_lista();
        }
    }
}
