using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;

namespace Villamos.Villamos_Ablakok
{
    public partial class Ablak_Dolgozók_Napjai : Form
    {
        private DateTime Dátum { get; set; }
        private string Cmbtelephely { get; set; }
        private Dictionary<string, string> Dolgozók { get; set; }

        readonly Kezelő_Kiegészítő_Beosztáskódok KézKód = new Kezelő_Kiegészítő_Beosztáskódok();
        readonly Kezelő_Dolgozó_Alap KézDolgozóAlap = new Kezelő_Dolgozó_Alap();
        readonly Kezelő_Dolgozó_Beosztás_Új KézBeoLista = new Kezelő_Dolgozó_Beosztás_Új();

        List<Adat_Kiegészítő_Beosztáskódok> AdatokKód = new List<Adat_Kiegészítő_Beosztáskódok>();
        List<Adat_Dolgozó_Beosztás_Új> BeoListaÖ = new List<Adat_Dolgozó_Beosztás_Új>();

        public Ablak_Dolgozók_Napjai(string cmbtelephely, DateTime dátum, Dictionary<string, string> dolgozók)
        {
            InitializeComponent();
            Dátum = dátum;
            Cmbtelephely = cmbtelephely;
            Dolgozók = dolgozók;
            Start();
        }

        private void Start()
        {
            AdatokKód = KézKód.Lista_Adatok(Cmbtelephely.Trim());
            BeoListaÖ = KézBeoLista.Lista_Adatok(Cmbtelephely.Trim(), Dátum);
            Csoportlétszám();
        }

        public Ablak_Dolgozók_Napjai()
        {
            InitializeComponent();
        }

        private void Ablak_munkalap_dekádoló_csoport_Load(object sender, EventArgs e)
        {
        }

        private void Command21_Click(object sender, EventArgs e)
        {
            Csoportlétszám();
        }

        private void Csoportlétszám()
        {
            try
            {
                Holtart.Be(20);


                CsoportTábla.Rows.Clear();
                CsoportTábla.Columns.Clear();
                CsoportTábla.Refresh();
                CsoportTábla.Visible = false;
                CsoportTábla.ColumnCount = 4;

                // fejléc elkészítése
                CsoportTábla.Columns[0].HeaderText = "Hr azonosító";
                CsoportTábla.Columns[0].Width = 140;
                CsoportTábla.Columns[1].HeaderText = "Dolgozó név";
                CsoportTábla.Columns[1].Width = 250;
                CsoportTábla.Columns[2].HeaderText = "8 nap:";
                CsoportTábla.Columns[2].Width = 90;
                CsoportTábla.Columns[3].HeaderText = "12 nap:";
                CsoportTábla.Columns[3].Width = 90;
                foreach (var Dolgozó in Dolgozók)
                {
                    CsoportTábla.RowCount++;
                    int i = CsoportTábla.RowCount - 1;
                    Létszám(Dolgozó.Key, out int lét8, out int lét12);
                    CsoportTábla.Rows[i].Cells[0].Value = Dolgozó.Key;
                    CsoportTábla.Rows[i].Cells[1].Value = Dolgozó.Value;
                    CsoportTábla.Rows[i].Cells[2].Value = lét8;
                    CsoportTábla.Rows[i].Cells[3].Value = lét12;
                    Holtart.Lép();
                }
                CsoportTábla.Visible = true;
                CsoportTábla.Refresh();
                CsoportTábla.ClearSelection();
                Holtart.Ki();
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

        private void Létszám(string DolgozóSzám, out int Fő8, out int Fő12)
        {
            Fő8 = 0;
            Fő12 = 0;

            List<string> Kód8 = (from a in AdatokKód
                                 where a.Számoló == true
                                 && a.Munkarend == 8
                                 orderby a.Beosztáskód
                                 select a.Beosztáskód).ToList();

            List<string> Kód12 = (from a in AdatokKód
                                  where a.Számoló == true
                                  && a.Munkarend == 12
                                  orderby a.Beosztáskód
                                  select a.Beosztáskód).ToList();


            List<Adat_Dolgozó_Beosztás_Új> BeoLista = (from a in BeoListaÖ
                                                       where a.Dolgozószám.Trim() == DolgozóSzám.Trim()
                                                       select a).ToList();

            foreach (Adat_Dolgozó_Beosztás_Új Nap in BeoLista)
            {
                if (Kód8.Contains(Nap.Beosztáskód.Trim())) Fő8++;
                if (Kód12.Contains(Nap.Beosztáskód.Trim())) Fő12++;
            }
        }

        private void Ablak_Dolgozók_Napjai_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC
            if ((int)e.KeyCode == 27)
            {
                this.Close();
            }
        }
    }
}
