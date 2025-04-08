using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.V_Adatszerkezet;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.V_Ablakok._4_Nyilvántartások.Nóta
{
    public partial class Ablak_Nóta_Összesítés : Form
    {
        readonly Kezelő_Nóta KézNóta = new Kezelő_Nóta();
        readonly Kezelő_Kerék_Tábla KézKerék = new Kezelő_Kerék_Tábla();


        public Ablak_Nóta_Összesítés()
        {
            InitializeComponent();
        }

        private void Ablak_Nóta_Összesítés_Load(object sender, EventArgs e)
        {
            FődarabTípusok_Feltöltése();
        }

        private void FődarabTípusok_Feltöltése()
        {
            List<Adat_Kerék_Tábla> Adatok = KézKerék.Lista_Adatok();
            List<string> Objektumok = Adatok.OrderBy(a => a.Objektumfajta).Select(a => a.Objektumfajta).Distinct().ToList();
            foreach (string objektum in Objektumok)
                FődarabTípusok.Items.Add(objektum);
        }

        private void Frissíti_táblalistát_Click(object sender, EventArgs e)
        {
            try
            {
                if (FődarabTípusok.Text.Trim() == "") return;
                List<Adat_Nóta> AdatokNóta = KézNóta.Lista_Adat();
                List<Adat_Kerék_Tábla> AdatokKerék = KézKerék.Lista_Adatok();
                List<string> Berendezés = (from a in AdatokKerék
                                           where a.Objektumfajta == FődarabTípusok.Text
                                           orderby a.Objektumfajta
                                           select a.Kerékberendezés).ToList();

                //kiszűrjük azokat az elemeket amik ebbe a csoportba tartoznak
                List<Adat_Nóta> Adatok = new List<Adat_Nóta>();
                foreach (string elem in Berendezés)
                {
                    Adat_Nóta Elem = AdatokNóta.FirstOrDefault(a => a.Berendezés == elem);
                    if (Elem != null) Adatok.Add(Elem);
                }


                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                //  Tábla.Visible = false;
                Tábla.ColumnCount = 6;
                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Telephely";
                Tábla.Columns[0].Width = 150;
                Tábla.Columns[1].HeaderText = "01 állapot";
                Tábla.Columns[1].Width = 120;
                Tábla.Columns[2].HeaderText = "02 állapot";
                Tábla.Columns[2].Width = 120;
                Tábla.Columns[3].HeaderText = "03 állapot";
                Tábla.Columns[3].Width = 120;
                Tábla.Columns[4].HeaderText = "? állapot";
                Tábla.Columns[4].Width = 120;
                Tábla.Columns[5].HeaderText = "Összesen";
                Tábla.Columns[5].Width = 120;

                int i;

                List<string> Telephelyek = Adatok.Select(a => a.Telephely).Distinct().ToList();
                foreach (string Elem in Telephelyek)
                {
                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = Elem;
                    int Á1 = AdatokNóta.Where(a => a.Telephely == Elem).ToList().Count(a => a.Készlet_Sarzs == "01");
                    int Á2 = AdatokNóta.Where(a => a.Telephely == Elem).ToList().Count(a => a.Készlet_Sarzs == "02");
                    int Á3 = AdatokNóta.Where(a => a.Telephely == Elem).ToList().Count(a => a.Készlet_Sarzs == "03");
                    int Á4 = AdatokNóta.Where(a => a.Telephely == Elem).ToList().Count(a => a.Készlet_Sarzs == "");
                    Tábla.Rows[i].Cells[1].Value = Á1;
                    Tábla.Rows[i].Cells[2].Value = Á2;
                    Tábla.Rows[i].Cells[3].Value = Á3;
                    Tábla.Rows[i].Cells[4].Value = Á4;
                    Tábla.Rows[i].Cells[5].Value = Á1 + Á2 + Á3 + Á4;


                }
                Tábla.RowCount++;
                i = Tábla.RowCount - 1;
                Tábla.Rows[i].Cells[0].Value = "Összesen";
                for (int oszlop = 1; oszlop < Tábla.Columns.Count; oszlop++)
                {
                    int szum = 0;
                    for (int sor = 0; sor < Tábla.Rows.Count - 1; sor++)
                    {
                        szum += Tábla.Rows[sor].Cells[oszlop].Value.ToÉrt_Int();
                    }
                    Tábla.Rows[i].Cells[oszlop].Value = szum;
                }
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
    }
}
