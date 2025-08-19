using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Villamos_Ablakok
{
    public partial class Ablak_munkalap_dekádoló_csoport : Form
    {
        public DateTime Dátum { get; private set; }
        public string Cmbtelephely { get; private set; }

        readonly Kezelő_Kiegészítő_Beosztáskódok KézKód = new Kezelő_Kiegészítő_Beosztáskódok();
        readonly Kezelő_Kiegészítő_Csoportbeosztás KézCsopBeo = new Kezelő_Kiegészítő_Csoportbeosztás();
        readonly Kezelő_Dolgozó_Alap KézDolgozóAlap = new Kezelő_Dolgozó_Alap();
        readonly Kezelő_Dolgozó_Beosztás_Új KézBeoLista = new Kezelő_Dolgozó_Beosztás_Új();

        public Ablak_munkalap_dekádoló_csoport(DateTime dátum, string cmbtelephely)
        {
            InitializeComponent();
            Dátum = dátum;
            Cmbtelephely = cmbtelephely;
        }

        public Ablak_munkalap_dekádoló_csoport()
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
                List<Adat_Kiegészítő_Csoportbeosztás> Adatok = KézCsopBeo.Lista_Adatok(Cmbtelephely.Trim());

                CsoportTábla.Rows.Clear();
                CsoportTábla.Columns.Clear();
                CsoportTábla.Refresh();
                CsoportTábla.Visible = false;
                CsoportTábla.ColumnCount = 3;

                // fejléc elkészítése
                CsoportTábla.Columns[0].HeaderText = "Csoport";
                CsoportTábla.Columns[0].Width = 200;
                CsoportTábla.Columns[1].HeaderText = "8 órás létszám:";
                CsoportTábla.Columns[1].Width = 80;
                CsoportTábla.Columns[2].HeaderText = "12 órás létszám:";
                CsoportTábla.Columns[2].Width = 80;
                foreach (Adat_Kiegészítő_Csoportbeosztás rekord in Adatok)
                {
                    CsoportTábla.RowCount++;
                    int i = CsoportTábla.RowCount - 1;
                    Létszám(rekord.Csoportbeosztás, out int lét8, out int lét12);
                    CsoportTábla.Rows[i].Cells[0].Value = rekord.Csoportbeosztás.Trim();
                    CsoportTábla.Rows[i].Cells[1].Value = lét8;
                    CsoportTábla.Rows[i].Cells[2].Value = lét12;
                    Holtart.Lép();
                }
                CsoportTábla.Visible = true;
                CsoportTábla.Refresh();
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

        private void Létszám(string Csoportbeosztás, out int Fő8, out int Fő12)
        {
            Fő8 = 0;
            Fő12 = 0;

            List<Adat_Kiegészítő_Beosztáskódok> AdatokKód = KézKód.Lista_Adatok(Cmbtelephely.Trim());
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

            //Csoport dolgozói
            List<Adat_Dolgozó_Alap> AdatokDolgÖ = KézDolgozóAlap.Lista_Adatok(Cmbtelephely.Trim());
            List<Adat_Dolgozó_Alap> AdatokDolg = (from a in AdatokDolgÖ
                                                  where a.Kilépésiidő < new DateTime(1900, 1, 31)
                                                  && a.Csoport == Csoportbeosztás
                                                  select a).ToList();

            List<Adat_Dolgozó_Beosztás_Új> BeoListaÖ = KézBeoLista.Lista_Adatok(Cmbtelephely.Trim(), Dátum);
            List<Adat_Dolgozó_Beosztás_Új> BeoLista = (from a in BeoListaÖ
                                                       where a.Nap.ToShortDateString() == Dátum.ToShortDateString()
                                                       orderby a.Dolgozószám
                                                       select a).ToList();

            foreach (Adat_Dolgozó_Alap rekord in AdatokDolg)
            {
                // ha van adattáblában olyan dolgozó akkor megnézzük, hogy dolgozott-e
                string BeosztásKód = (from a in BeoLista
                                      where a.Dolgozószám.Trim() == rekord.Dolgozószám.Trim()
                                      select a.Beosztáskód.Trim()).FirstOrDefault();

                if (BeosztásKód != null)
                {
                    if (Kód8.Contains(BeosztásKód.Trim())) Fő8++;
                    if (Kód12.Contains(BeosztásKód.Trim())) Fő12++;
                }
            }
        }

        private void Ablak_munkalap_dekádoló_csoport_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC
            if ((int)e.KeyCode == 27)
            {
                this.Close();
            }
        }
    }
}
