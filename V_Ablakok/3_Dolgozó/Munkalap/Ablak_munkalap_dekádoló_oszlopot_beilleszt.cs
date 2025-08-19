using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Villamos_Ablakok
{
    public partial class Ablak_munkalap_dekádoló_oszlopot_beilleszt : Form
    {
        public string Választott;
        public event Event_Kidobó Változás;
        public DateTime Dátum { get; private set; }
        public string Cmbtelephely { get; private set; }

        readonly Kezelő_Munka_Adatok KézMunkaAdat = new Kezelő_Munka_Adatok();

        int sor = -1;

        public Ablak_munkalap_dekádoló_oszlopot_beilleszt(DateTime dátum, string cmbtelephely)
        {
            InitializeComponent();
            Dátum = dátum;
            Cmbtelephely = cmbtelephely;
        }

        public Ablak_munkalap_dekádoló_oszlopot_beilleszt()
        {
            InitializeComponent();
        }

        private void Ablak_munkalap_dekádoló_oszlopot_beilleszt_Load(object sender, EventArgs e)
        {
            AcceptButton = Command17;
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            Táblakitöltés4();
        }


        private void Táblakitöltés4()
        {
            try
            {

                if (Text6.Text.Trim() == "" || !int.TryParse(Text6.Text.Trim(), out int Szám)) Szám = 5;

                Text6.Text = Szám.ToString();

                List<Adat_Munka_Adatok> AdatokÖ = KézMunkaAdat.Lista_Adatok(Cmbtelephely, Dátum.Year);
                List<string> rendelés = (from a in AdatokÖ
                                         where a.Státus == true
                                         && a.Dátum > Dátum.AddDays(-1 * Szám)
                                         orderby a.Rendelés
                                         select a.Rendelés).Distinct().ToList();

                Tábla4.Rows.Clear();
                Tábla4.Columns.Clear();
                Tábla4.Refresh();
                Tábla4.Visible = false;
                Tábla4.ColumnCount = 4;

                // fejléc elkészítése
                Tábla4.Columns[0].HeaderText = "Rendelés";
                Tábla4.Columns[0].Width = 100;
                Tábla4.Columns[1].HeaderText = "Művelet";
                Tábla4.Columns[1].Width = 80;
                Tábla4.Columns[2].HeaderText = "Típus";
                Tábla4.Columns[2].Width = 140;
                Tábla4.Columns[3].HeaderText = "Munka";
                Tábla4.Columns[3].Width = 100;

                foreach (string elem in rendelés)
                {
                    Adat_Munka_Adatok rekord = (from a in AdatokÖ
                                                where a.Rendelés == elem
                                                select a).FirstOrDefault();
                    if (rekord != null)
                    {
                        Tábla4.RowCount++;
                        int i = Tábla4.RowCount - 1;

                        Tábla4.Rows[i].Cells[0].Value = rekord.Rendelés.Trim();
                        Tábla4.Rows[i].Cells[1].Value = rekord.Művelet.Trim();
                        Tábla4.Rows[i].Cells[2].Value = rekord.Megnevezés.Trim();
                        Tábla4.Rows[i].Cells[3].Value = rekord.Pályaszám.Trim();
                    }
                }

                Tábla4.Visible = true;
                Tábla4.Refresh();

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

        private void Command17_Click(object sender, EventArgs e)
        {
            try
            {
                if (sor == -1) return;

                string szöveg = "";
                szöveg += Tábla4.Rows[sor].Cells[0].Value.ToString() + "\r\n";
                szöveg += Tábla4.Rows[sor].Cells[1].Value.ToString() + "\r\n";
                szöveg += Tábla4.Rows[sor].Cells[2].Value.ToString() + "\r\n";
                szöveg += Tábla4.Rows[sor].Cells[3].Value.ToString() + "\r\n";
                Választott = szöveg;
                Változás?.Invoke();
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



        private void Tábla4_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                sor = -1;
            else
                sor = e.RowIndex;
        }

        private void Tábla4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                sor = -1;
            else
                sor = e.RowIndex;
        }

        private void Ablak_munkalap_dekádoló_oszlopot_beilleszt_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC
            if ((int)e.KeyCode == 27)
            {
                this.Close();
            }
        }
    }
}
