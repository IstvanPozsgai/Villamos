using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Villamos_Ablakok.Kerékeszterga
{
    public partial class Ablak_Eszterga_Terjesztés : Form
    {
        readonly string hely = Application.StartupPath + @"\Főmérnökség\Adatok\Kerékeszterga\Törzs.mdb";
        readonly string jelszó = "RónaiSándor";


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
                Kezelő_Kiegészítő_Könyvtár KézKönyv = new Kezelő_Kiegészítő_Könyvtár();
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

        void MezőÜrítés()
        {
            Név.Text = "";
            Email.Text = "";
            Cmbtelephely.Text = "";
            CmbVáltozat.Text = "";
        }

        private void ListaFeltöltés()
        {
            try
            {
                Adatok.Clear();
                string szöveg = $"SELECT * FROM terjesztés ORDER BY  név";
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

        private void Tábla_Író()
        {
            try
            {
                MezőÜrítés();
                ListaFeltöltés();
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
                ListaFeltöltés();
                string szöveg;

                Adat_Kerék_Eszterga_Terjesztés Elem = (from a in Adatok
                                                       where a.Email == Email.Text.Trim()
                                                       select a).FirstOrDefault();

                if (Elem != null)
                {
                    szöveg = "UPDATE terjesztés SET ";
                    szöveg += $"név='{Név.Text.Trim()}', ";  //Név
                    szöveg += $"Telephely='{Cmbtelephely.Text.Trim()}', ";    //Telephely
                    szöveg += $"Változat={int.Parse(darabol[0])} ";    //Változat
                    szöveg += $" WHERE email='{Email.Text.Trim()}'";
                }
                else
                {
                    szöveg = "INSERT INTO terjesztés (Név, Email, Telephely, Változat ) VALUES (";
                    szöveg += $"'{Név.Text.Trim()}', ";  //Név
                    szöveg += $"'{Email.Text.Trim()}', ";  // Email
                    szöveg += $"'{Cmbtelephely.Text.Trim()}', ";    //Telephely
                    szöveg += $"{int.Parse(darabol[0])} )";    //Változat
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);
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

                ListaFeltöltés();
                Adat_Kerék_Eszterga_Terjesztés Elem = (from a in Adatok
                                                       where a.Email == Email.Text.Trim()
                                                       select a).FirstOrDefault();

                if (Elem != null)
                {
                    string szöveg = $"DELETE FROM terjesztés WHERE email='{Email.Text.Trim()}'";
                    MyA.ABtörlés(hely, jelszó, szöveg);
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
