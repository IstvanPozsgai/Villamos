using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;
using MyA = Adatbázis;
using MyLista = Villamos.Villamos_Ablakok._3_Dolgozó.Karbantartási_Munkalapok.Karbantartási_ListaFeltöltés;

namespace Villamos.Villamos_Ablakok._3_Dolgozó.Karbantartási_Munkalapok
{
    public partial class Ablak_Karbantartási_Rendelés : Form
    {
        public string CmbTelephely { get; private set; }

        List<Adat_Technológia_Rendelés> AdatokRendelés = new List<Adat_Technológia_Rendelés>();
        List<Adat_technológia_Ciklus> AdatokCiklus = new List<Adat_technológia_Ciklus>();
        List<Adat_Technológia_TípusT> AdatokTípusT = new List<Adat_Technológia_TípusT>();

        public Ablak_Karbantartási_Rendelés(string cmbTelephely)
        {
            InitializeComponent();
            CmbTelephely = cmbTelephely;
        }

        private void Ablak_Karbantartási_Rendelés_Load(object sender, EventArgs e)
        {
            Rendelés_Dátum.Value = DateTime.Today;
            Jogosultságkiosztás();
            Rendelés_Típus_feltöltés();
            Rendelés_tábla_frissít();
        }

        private void Jogosultságkiosztás()
        {
            int melyikelem;
            // ide kell az összes gombot tenni amit szabályozni akarunk false

            melyikelem = 170;
            // módosítás 1 
            if (MyF.Vanjoga(melyikelem, 1))
            {
            }
            // módosítás 2 
            if (MyF.Vanjoga(melyikelem, 2))
            {
            }
            // módosítás 3 
            if (MyF.Vanjoga(melyikelem, 3))
            {
             }
        }
 
        private void Rendelés_Típus_feltöltés()
        {
            try
            {
                Rendelés_Típus.Items.Clear();
                AdatokTípusT = MyLista.TípustáblaLista();
                foreach (Adat_Technológia_TípusT rekord in AdatokTípusT)
                    Rendelés_Típus.Items.Add(rekord.Típus);
                Rendelés_Típus.Refresh();
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

        void Rendelés_Ciklus_feltöltés()
        {
            try
            {
                if (Rendelés_Típus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva járműtípus.");
                AdatokCiklus= MyLista.KarbCiklusLista(Rendelés_Típus.Text.Trim());
                Rendelés_Ciklus.Items.Clear();

                foreach (Adat_technológia_Ciklus rekord in AdatokCiklus)
                    Rendelés_Ciklus.Items.Add(rekord.Fokozat);
                Rendelés_Ciklus.Refresh();
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


        private void Rendelés_Típus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Rendelés_Ciklus_feltöltés();
        }

        private void Rendelés_Ok_Click(object sender, EventArgs e)
        {
            try
            {
                if (Rendelés_Típus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva típus.");
                if (Rendelés_Ciklus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva ciklus.");
                if (Rendelés_Rendelés.Text.Trim() == "") throw new HibásBevittAdat("Nincs kitöltve a rendelési szám mező.");
                if (Rendelés_Rendelés.Text.Trim().Length > 20) throw new HibásBevittAdat("A rendelési szám hossza maximum 20 karakter lehet.");

                Adat_Technológia_Rendelés Elem = (from a in AdatokRendelés
                                                  where a.Év == Rendelés_Dátum.Value.Year
                                                       && a.Technológia_típus == Rendelés_Típus.Text.Trim()
                                                       && a.Karbantartási_fokozat == Rendelés_Ciklus.Text.Trim()
                                                  select a).FirstOrDefault();
                string szöveg;
                if (Elem != null)
                {
                    szöveg = $"UPDATE {CmbTelephely}  SET ";
                    szöveg += $" Rendelésiszám='{Rendelés_Rendelés.Text.Trim()}' ";
                    szöveg += $"  WHERE év = {Rendelés_Dátum.Value.Year} AND technológia_típus = '{Rendelés_Típus.Text.Trim()}' AND Karbantartási_fokozat='{Rendelés_Ciklus.Text.Trim()}' ";
                }
                else
                {
                    szöveg = $"INSERT INTO {CmbTelephely}  (Év, Karbantartási_fokozat, Technológia_típus, Rendelésiszám) VALUES (";
                    szöveg += $" {Rendelés_Dátum.Value.Year}, '{Rendelés_Ciklus.Text.Trim()}', '{Rendelés_Típus.Text.Trim()}', '{Rendelés_Rendelés.Text.Trim()}')";
                }
                string hely = $@"{Application.StartupPath}\{CmbTelephely}\Adatok\\Munkalap\Rendelés.mdb";
                string jelszó = "Bezzegh";
                MyA.ABMódosítás(hely, jelszó, szöveg);
                Rendelés_tábla_frissít();
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

        private void Rendelés_Töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (Rendelés_Típus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva típus.");
                if (Rendelés_Ciklus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva ciklus.");

                Adat_Technológia_Rendelés Elem = (from a in AdatokRendelés
                                                  where a.Év == Rendelés_Dátum.Value.Year
                                                       && a.Technológia_típus == Rendelés_Típus.Text.Trim()
                                                       && a.Karbantartási_fokozat == Rendelés_Ciklus.Text.Trim()
                                                  select a).FirstOrDefault();

                if (Elem != null)
                {
                    string hely = $@"{Application.StartupPath}\{CmbTelephely}\Adatok\\Munkalap\Rendelés.mdb";
                    string jelszó = "Bezzegh";
                    string szöveg = $"DELETE FROM  {CmbTelephely}  ";
                    szöveg += $"  WHERE év={Rendelés_Dátum.Value.Year} AND technológia_típus='{Rendelés_Típus.Text.Trim()}' AND Karbantartási_fokozat='{Rendelés_Ciklus.Text.Trim()}' ";
                    MyA.ABtörlés(hely, jelszó, szöveg);
                    Rendelés_tábla_frissít();
                }
                else
                    throw new HibásBevittAdat("Nincs olyan elem amit törölni lehet.");

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

        private void Rendelés_Frissít_Click(object sender, EventArgs e)
        {
            Rendelés_tábla_frissít();
        }

        private void Rendelés_tábla_frissít()
        {
            try
            {
                AdatokRendelés= MyLista.RendelésLista(CmbTelephely, Rendelés_Dátum.Value);
                List<Adat_Technológia_Rendelés> AdatokSzűrt = (from a in AdatokRendelés
                                                               where a.Év == Rendelés_Dátum.Value.Year
                                                               orderby a.Technológia_típus, a.Karbantartási_fokozat
                                                               select a).ToList();

                Rendelés_Tábla.Rows.Clear();
                Rendelés_Tábla.Columns.Clear();
                Rendelés_Tábla.Refresh();
                Rendelés_Tábla.Visible = false;
                Rendelés_Tábla.ColumnCount = 4;

                // fejléc elkészítése
                Rendelés_Tábla.Columns[0].HeaderText = "Év";
                Rendelés_Tábla.Columns[0].Width = 80;
                Rendelés_Tábla.Columns[1].HeaderText = "Technológia típus";
                Rendelés_Tábla.Columns[1].Width = 180;
                Rendelés_Tábla.Columns[2].HeaderText = "Ciklus";
                Rendelés_Tábla.Columns[2].Width = 150;
                Rendelés_Tábla.Columns[3].HeaderText = "Rendelési szám";
                Rendelés_Tábla.Columns[3].Width = 150;
                foreach (Adat_Technológia_Rendelés adat in AdatokSzűrt)
                {
                    Rendelés_Tábla.RowCount++;
                    int i = Rendelés_Tábla.RowCount - 1;
                    Rendelés_Tábla.Rows[i].Cells[0].Value = adat.Év;
                    Rendelés_Tábla.Rows[i].Cells[1].Value = adat.Technológia_típus.Trim();
                    Rendelés_Tábla.Rows[i].Cells[2].Value = adat.Karbantartási_fokozat.Trim();
                    Rendelés_Tábla.Rows[i].Cells[3].Value = adat.Rendelésiszám.Trim();

                }

                Rendelés_Tábla.Visible = true;
                Rendelés_Tábla.ClearSelection();
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

        private void Rendelés_Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                Rendelés_Dátum.Value = new DateTime(Rendelés_Tábla.Rows[e.RowIndex].Cells[0].Value.ToÉrt_Int(), 1, 1);
                Rendelés_Típus.Text = Rendelés_Tábla.Rows[e.RowIndex].Cells[1].Value.ToString();
                Rendelés_Ciklus.Text = Rendelés_Tábla.Rows[e.RowIndex].Cells[2].Value.ToString();
                Rendelés_Rendelés.Text = Rendelés_Tábla.Rows[e.RowIndex].Cells[3].Value.ToString();
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
