using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Villamos.Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
using MyEn = Villamos.V_MindenEgyéb.Enumok;

namespace Villamos.Villamos_Ablakok.Kerékeszterga
{
    public partial class Ablak_Eszterga_Adatok_Javítás : Form
    {
        public event Event_Kidobó Változás;
        readonly Kezelő_Baross_Mérési_Adatok Kéz = new Kezelő_Baross_Mérési_Adatok();
        Adat_Baross_Mérési_Adatok Adat = null;



        public long ID { get; private set; }
        public Ablak_Eszterga_Adatok_Javítás(long id)
        {
            InitializeComponent();
            ID = id;
        }

        private void Ablak_Eszterga_Adatok_Javítás_Load(object sender, EventArgs e)
        {
            Beolvasás();
            Kiírja();
            Berendezés_adatok();
            AcceptButton = Rögzítés;
        }


        private void Beolvasás()
        {
            try
            {
                string hely = Application.StartupPath + $@"\Főmérnökség\Adatok\Kerékeszterga\Baross_Mérés.mdb";
                string jelszó = "RónaiSándor";
                string szöveg = $"SELECT * FROM mérés WHERE Eszterga_Id=" + ID;
                Adat = Kéz.Egy_Adat(hely, jelszó, szöveg);
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

        private void Kiírja()
        {
            try
            {
                Ürít();
                if (Adat != null)
                {
                    Dátum_1.Text = Adat.Dátum_1.ToString();
                    Azonosító.Text = Adat.Azonosító.Trim();
                    Kerékpárszám.Text = Adat.Kerékpár_szám.Trim();
                    Típus_Eszt.Text = Adat.Típus_Eszt.Trim();
                    Pozíció.Text = Adat.Pozíció_Eszt.ToString();
                    Eszterga_Id.Text = Adat.Eszterga_Id.ToString();
                    Státus.Text = Enum.GetName(typeof(MyEn.Eszt_Adat_Állapot_Státus), Adat.Státus); 
                    B_Átmérő_Ú.Text = Adat.B_Átmérő_Ú.ToString();
                    J_Átmérő_Ú.Text = Adat.J_Átmérő_Ú.ToString();
                    Megjegyzés.Text = Adat.Megjegyzés.Trim();
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

        void Ürít()
        {
            Dátum_1.Text = "";
            Azonosító.Text = "";
            Kerékpárszám.Text = "";
            Típus_Eszt.Text = "";
            Pozíció.Text = "";
            Eszterga_Id.Text = "";
            Státus.Text = "";
            B_Átmérő_Ú.Text = "";
            J_Átmérő_Ú.Text = "";
            Megjegyzés.Text = "";
        }

        private void Tábla_Listázás_Click(object sender, EventArgs e)
        {
            Berendezés_adatok();
        }

        private void Berendezés_adatok()
        {
            try
            {
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Kerék.mdb";
                string jelszó = "szabólászló";
                string szöveg = "";

                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 4;

                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Psz";
                Tábla.Columns[0].Width = 80;
                Tábla.Columns[1].HeaderText = "Berendezésszám";
                Tábla.Columns[1].Width = 150;
                Tábla.Columns[2].HeaderText = "Gyári szám";
                Tábla.Columns[2].Width = 150;
                Tábla.Columns[3].HeaderText = "Pozíció";
                Tábla.Columns[3].Width = 100;

                szöveg = $"SELECT * FROM tábla where [azonosító]='{Azonosító.Text.Trim()}' AND objektumfajta='V.KERÉKPÁR' order by pozíció ";

                Kezelő_Kerék_Tábla kéz = new Kezelő_Kerék_Tábla();
                List<Adat_Kerék_Tábla> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);

                foreach (Adat_Kerék_Tábla rekord in Adatok)
                {

                    Tábla.RowCount++;
                    int i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = rekord.Azonosító.Trim();
                    Tábla.Rows[i].Cells[1].Value = rekord.Kerékberendezés.Trim();
                    Tábla.Rows[i].Cells[2].Value = rekord.Kerékgyártásiszám.Trim();
                    Tábla.Rows[i].Cells[3].Value = rekord.Pozíció.Trim();
                }

                Tábla.Visible = true;
                Tábla.Refresh(); 
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

        private void Rögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Azonosító.Text.Trim() == "") throw new HibásBevittAdat("A pályaszám mező nem lehet üres.");
                if (Kerékpárszám.Text.Trim() == "") throw new HibásBevittAdat("A Kerékpárszám mező nem lehet üres.");
                if (Típus_Eszt.Text.Trim() == "") throw new HibásBevittAdat("A Típus mező nem lehet üres.");
                if (Pozíció.Text.Trim() == "") throw new HibásBevittAdat("A Pozíció mező nem lehet üres.");
                if (!int.TryParse(Pozíció.Text.Trim(), out int pozi)) throw new HibásBevittAdat("A Pozíció mezőnek számnak kell lennie.");



                if (Azonosító.Text == Adat.Azonosító.Trim() &&
                Kerékpárszám.Text == Adat.Kerékpár_szám.Trim() &&
                Típus_Eszt.Text == Adat.Típus_Eszt.Trim() &&
                Pozíció.Text == Adat.Pozíció_Eszt.ToString() &&
                Megjegyzés.Text == Adat.Megjegyzés.Trim()) throw new HibásBevittAdat("Az adatokban nem történt változás.");

                string szövegmegjegyzés = "";
                bool volt = false;

                string hely = Application.StartupPath + $@"\Főmérnökség\Adatok\Kerékeszterga\Baross_Mérés.mdb";
                string jelszó = "RónaiSándor";
                string szöveg = $"UPDATE mérés SET ";
                if (Azonosító.Text != Adat.Azonosító.Trim())
                {
                    szöveg += $" Azonosító='{Azonosító.Text.Trim()}'";
                    szövegmegjegyzés += $"Pályaszám:{Adat.Azonosító.Trim()},";
                    volt = true;
                }
                if (Kerékpárszám.Text != Adat.Kerékpár_szám.Trim())
                {
                    if (volt) szöveg += ",";
                    szöveg += $" Kerékpár_szám='{Kerékpárszám.Text.Trim()}'";
                    szövegmegjegyzés += $"Kerékpárszám:{Adat.Kerékpár_szám.Trim()},";
                    volt = true;
                }
                if (Típus_Eszt.Text != Adat.Típus_Eszt.Trim())
                {
                    if (volt) szöveg += ",";
                    szöveg += $" Típus_Eszt='{Típus_Eszt.Text.Trim()}'";
                    szövegmegjegyzés += $"Típus:{Adat.Típus_Eszt.Trim()},";
                    volt = true;
                }
                if (Pozíció.Text != Adat.Pozíció_Eszt.ToString())
                {
                    if (volt) szöveg += ",";
                    szöveg += $" Pozíció_Eszt={pozi}";
                    szövegmegjegyzés += $"Pozíció:{Adat.Pozíció_Eszt},";
                    volt = true;
                }
                if (Megjegyzés.Text.Trim () != Adat.Megjegyzés.Trim())
                {
                    if (volt) szöveg += ",";
                    szövegmegjegyzés += $"Megjegyzés:{Megjegyzés.Text.Trim()}\n";
                    szöveg += $" Megjegyzés='{Adat.Megjegyzés.Trim() + "\n" + szövegmegjegyzés}'";
                }
                   
                szöveg += " WHERE Eszterga_Id=" + ID;
                MyA.ABMódosítás(hely, jelszó, szöveg);
                MessageBox.Show("Az adatok rögzítésre kerültek!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Változás?.Invoke();
                this.Close();
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
