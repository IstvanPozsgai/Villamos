using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Adatszerkezet;
using MyEn = Villamos.V_MindenEgyéb.Enumok;

namespace Villamos.Villamos_Ablakok.Kerékeszterga
{
    public partial class Ablak_Eszterga_Adatok_Javítás : Form
    {
        public event Event_Kidobó Változás;
        readonly Kezelő_Baross_Mérési_Adatok Kéz = new Kezelő_Baross_Mérési_Adatok();
        readonly Kezelő_Kerék_Tábla KézKerék = new Kezelő_Kerék_Tábla();
        List<Adat_Baross_Mérési_Adatok> Adatok = new List<Adat_Baross_Mérési_Adatok>();
        Adat_Baross_Mérési_Adatok Adat = null;

        public long ID { get; private set; }
        public Ablak_Eszterga_Adatok_Javítás(long id)
        {
            InitializeComponent();
            ID = id;
            Start();
        }

        public Ablak_Eszterga_Adatok_Javítás()
        {
            InitializeComponent();
        }

        private void Start()
        {
            Adatok = Kéz.Lista_Adatok();
            Beolvasás();
            Kiírja();
            Berendezés_adatok();
            AcceptButton = Rögzítés;
        }

        private void Ablak_Eszterga_Adatok_Javítás_Load(object sender, EventArgs e)
        {
        }


        private void Beolvasás()
        {
            try
            {
                Adat = Adatok.Where(a => a.Eszterga_Id == ID).FirstOrDefault();
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

        private void Ürít()
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

                List<Adat_Kerék_Tábla> Adatok = KézKerék.Lista_Adatok();
                Adatok = (from a in Adatok
                          where a.Azonosító == Azonosító.Text.Trim()
                          && a.Objektumfajta == "V.KERÉKPÁR"
                          orderby a.Pozíció
                          select a).ToList();
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
                if (Azonosító.Text != Adat.Azonosító.Trim()) szövegmegjegyzés += $"Pályaszám:{Adat.Azonosító.Trim()},";
                if (Kerékpárszám.Text != Adat.Kerékpár_szám.Trim()) szövegmegjegyzés += $"Kerékpárszám:{Adat.Kerékpár_szám.Trim()},";
                if (Típus_Eszt.Text != Adat.Típus_Eszt.Trim()) szövegmegjegyzés += $"Típus:{Adat.Típus_Eszt.Trim()},";
                if (Pozíció.Text != Adat.Pozíció_Eszt.ToString()) szövegmegjegyzés += $"Pozíció:{Adat.Pozíció_Eszt},";
                if (Megjegyzés.Text.Trim() != Adat.Megjegyzés.Trim()) szövegmegjegyzés += $"Megjegyzés:{Megjegyzés.Text.Trim()}\n";

                Adat_Baross_Mérési_Adatok ADAT = new Adat_Baross_Mérési_Adatok(
                        Azonosító.Text != Adat.Azonosító.Trim() ? Azonosító.Text.Trim() : Adat.Azonosító.Trim(),
                        Kerékpárszám.Text != Adat.Kerékpár_szám.Trim() ? Kerékpárszám.Text.Trim() : Adat.Kerékpár_szám.Trim(),
                        Típus_Eszt.Text != Adat.Típus_Eszt.Trim() ? Típus_Eszt.Text.Trim() : Adat.Típus_Eszt.Trim(),
                        Pozíció.Text != Adat.Pozíció_Eszt.ToString() ? pozi : Adat.Pozíció_Eszt,
                        ID,
                        Megjegyzés.Text.Trim() != szövegmegjegyzés ? $"{Adat.Megjegyzés.Trim()}\n{szövegmegjegyzés}" : Adat.Megjegyzés.Trim());
                Kéz.Módosítás(ADAT);
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
