using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using static Villamos.Főkönyv_Funkciók;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    public partial class Ablak_Napiadatok
    {
        readonly Kezelő_kiegészítő_Hibaterv KézKiegHibaTerv = new Kezelő_kiegészítő_Hibaterv();
        readonly Kezelő_jármű_hiba KézJárműHiba = new Kezelő_jármű_hiba();
        readonly Kezelő_Jármű_Javításiátfutástábla KézÁtfutás = new Kezelő_Jármű_Javításiátfutástábla();
        readonly Kezelő_Kiadás_Összesítő KézKiadásÖ = new Kezelő_Kiadás_Összesítő();
        readonly Kezelő_Főkönyv_Személyzet KézSzemély = new Kezelő_Főkönyv_Személyzet();
        readonly Kezelő_Főkönyv_Típuscsere KézTípus = new Kezelő_Főkönyv_Típuscsere();
        readonly Kezelő_Forte_Kiadási_Adatok KézKiadási = new Kezelő_Forte_Kiadási_Adatok();

        DataTable AdatTábla = new DataTable();
        string TáblaNév = "";

        public Ablak_Napiadatok()
        {
            InitializeComponent();
        }

        string MilyenLista;

        #region Alap
        private void Ablak_Napiadatok_Load(object sender, EventArgs e)
        {
            try
            {
                Telephelyekfeltöltése();

                KeyPreview = true;
                Dátum.Value = DateTime.Today;
                Dátum.MaxDate = DateTime.Today;

                Táblaalaphelyzet();
                Jogosultságkiosztás();
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

        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Napiadatok.html";
                Module_Excel.Megnyitás(hely);
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

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    Cmbtelephely.Items.Add(Elem);

                if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér || Program.PostásTelephely == "Műszaki osztály")
                    Cmbtelephely.Text = Cmbtelephely.Items[0].ToString().Trim();
                else
                    Cmbtelephely.Text = Program.PostásTelephely;

                Cmbtelephely.Enabled = Program.Postás_Vezér;
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

        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;

                // ide kell az összes gombot tenni amit szabályozni akarunk false
                Napiadatok_Frissítése.Visible = false;


                melyikelem = 108;
                // módosítás 1
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Napiadatok_Frissítése.Visible = true;
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

        private void Excel_gomb_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.Visible == true && Tábla.Rows.Count <= 0) return;
                if (Tábla1.Visible == true && Tábla1.Rows.Count <= 0) return;
                if (Tábla2.Visible == true && Tábla2.Rows.Count <= 0) return;
                if (Tábla3.Visible == true && Tábla3.Rows.Count <= 0) return;

                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Kiadási_Javítási_adatok_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddhhmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;
                TáblaNév = "Havikiadás";
                switch (TáblaNév)
                {
                    case "Havikiadás":
                        MyE.DataTableToExcel(fájlexc, AdatTábla);
                        break;

                    default:
                        {

                            if (Tábla.Visible) Module_Excel.DataGridViewToExcel(fájlexc, Tábla);
                            else if (Tábla1.Visible) Module_Excel.DataGridViewToExcel(fájlexc, Tábla1);
                            else if (Tábla2.Visible) Module_Excel.DataGridViewToExcel(fájlexc, Tábla2);

                        }
                        break;
                }

                Module_Excel.Megnyitás(fájlexc);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

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


        #region Tábla kezelés
        private void Táblaalaphelyzet()
        {

            Tábla1.Visible = false;
            Tábla2.Visible = false;
            Tábla.Visible = true;
        }
        #endregion


        #region Állókocsik
        private void Állókocsik_Click(object sender, EventArgs e)
        {
            SUBnapihibagöngyölés(Cmbtelephely.Text.Trim());
            SUBNapielkészültek(Dátum.Value, Cmbtelephely.Text.Trim());
            MilyenLista = "minden";
            Napihibalista();
        }

        private void Napihibalista()
        {
            try
            {
                List<Adat_Jármű_Javításiátfutástábla> Adatok = new List<Adat_Jármű_Javításiátfutástábla>();

                if (MilyenLista.Trim() == "napiálló")
                {
                    Adatok = KézÁtfutás.Lista_Adatok(Cmbtelephely.Text.Trim());
                    Adatok = (from a in Adatok
                              where a.Kezdődátum >= MyF.Nap0000(Dátum.Value)
                              && a.Kezdődátum <= MyF.Nap2359(Dátum.Value)
                              orderby a.Azonosító
                              select a).ToList();
                }
                if (MilyenLista.Trim() == "elkészült")
                {
                    Adatok = KézÁtfutás.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);
                    Adatok = (from a in Adatok
                              where a.Végdátum >= MyF.Nap0000(Dátum.Value)
                              && a.Végdátum <= MyF.Nap2359(Dátum.Value)
                              orderby a.Azonosító
                              select a).ToList();
                }
                if (MilyenLista.Trim() == "havikészült")
                {
                    Adatok = KézÁtfutás.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);
                    Adatok = (from a in Adatok
                              where a.Végdátum >= MyF.Nap0000(MyF.Hónap_elsőnapja(Dátum.Value))
                              && a.Végdátum <= MyF.Nap2359(MyF.Hónap_utolsónapja(Dátum.Value))
                              orderby a.Azonosító
                              select a).ToList();
                }

                Tábla.Visible = false;
                Tábla.DataSource = null;
                Tábla.Rows.Clear();
                Tábla.Columns.Clear();

                NapiFejlécTábla();

                NapiTartalomTábla(Adatok);
                Tábla.DataSource = AdatTábla;

                NapiSzélességTábla();
                Tábla.Top = 50;
                Tábla.Height = Height - Tábla.Top - 50;
                Tábla.Width = Width - Tábla.Left - 20;
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

        private void NapiFejlécTábla()
        {
            AdatTábla.Columns.Clear();
            AdatTábla.Columns.Add("Azonosító");
            AdatTábla.Columns.Add("Kezdő dátum");
            AdatTábla.Columns.Add("Végső dátum");
            AdatTábla.Columns.Add("Állási napok").DataType = typeof(int);
            AdatTábla.Columns.Add("Hiba leírása");
        }
        private void NapiSzélességTábla()
        {
            Tábla.Columns["Azonosító"].Width = 100;
            Tábla.Columns["Kezdő dátum"].Width = 120;
            Tábla.Columns["Végső dátum"].Width = 120;
            Tábla.Columns["Állási napok"].Width = 120;
            Tábla.Columns["Hiba leírása"].Width = 400;
        }
        private void NapiTartalomTábla(List<Adat_Jármű_Javításiátfutástábla> Adatok)
        {
            try
            {
                AdatTábla.Clear();
                foreach (Adat_Jármű_Javításiátfutástábla rekord in Adatok)
                {
                    DataRow Soradat = AdatTábla.NewRow();
                    Soradat["Azonosító"] = rekord.Azonosító.Trim();
                    Soradat["Kezdő dátum"] = rekord.Kezdődátum.ToString("yyyy.MM.dd");
                    if (rekord.Végdátum.ToString("yyyy.MM.dd") != (new DateTime(1900, 1, 1)).ToString("yyyy.MM.dd"))
                        Soradat["Végső dátum"] = rekord.Végdátum.ToString("yyyy.MM.dd");
                    // nincs vég dátum annál ami áll
                    TimeSpan delta;
                    if (rekord.Végdátum.ToString("yyyy.MM.dd") == (new DateTime(1900, 1, 1)).ToString("yyyy.MM.dd"))
                    {
                        delta = DateTime.Today - rekord.Kezdődátum;
                        Soradat["Állási napok"] = (int)delta.TotalDays;
                    }
                    else
                    {
                        delta = DateTime.Today - rekord.Végdátum;
                        Soradat["Állási napok"] = (int)delta.TotalDays;
                    }
                    Soradat["Hiba leírása"] = rekord.Hibaleírása.Trim();
                    AdatTábla.Rows.Add(Soradat);
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


        #endregion


        #region Napi kiadási adatok
        private void Lista_Click(object sender, EventArgs e)
        {
            Napi_kiadási_adatok();
        }

        private void Napi_kiadási_adatok()
        {
            MilyenLista = "minden";
            Táblázatlistázás();
            Táblázatlistázásszemélyzet();
            Táblázatlistázástípuscsere();
            Napi_adatok_felirat();
        }

        private void Táblázatlistázás()
        {
            try
            {
                List<Adat_Kiadás_összesítő> Adatok = KézKiadásÖ.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);

                Adatok = (from a in Adatok
                          where a.Dátum.ToShortDateString() == Dátum.Value.ToShortDateString()
                          orderby a.Napszak, a.Típus
                          select a).ToList();

                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 11;

                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Napszak";
                Tábla.Columns[0].Width = 100;
                Tábla.Columns[1].HeaderText = "Típus";
                Tábla.Columns[1].Width = 120;
                Tábla.Columns[2].HeaderText = "Eltérés";
                Tábla.Columns[2].Width = 80;
                Tábla.Columns[3].HeaderText = "Előírás";
                Tábla.Columns[3].Width = 80;
                Tábla.Columns[4].HeaderText = "Forgalomban";
                Tábla.Columns[4].Width = 100;
                Tábla.Columns[5].HeaderText = "Tartalék";
                Tábla.Columns[5].Width = 100;
                Tábla.Columns[6].HeaderText = "Kocsiszíni";
                Tábla.Columns[6].Width = 100;
                Tábla.Columns[7].HeaderText = "Félreállítás";
                Tábla.Columns[7].Width = 100;
                Tábla.Columns[8].HeaderText = "Főjavítás";
                Tábla.Columns[8].Width = 100;
                Tábla.Columns[9].HeaderText = "Összesen";
                Tábla.Columns[9].Width = 100;
                Tábla.Columns[10].HeaderText = "Személyzethiány";
                Tábla.Columns[10].Width = 200;

                int i;
                foreach (Adat_Kiadás_összesítő rekord in Adatok)
                {

                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = rekord.Napszak.Trim();
                    Tábla.Rows[i].Cells[1].Value = rekord.Típus.Trim();
                    Tábla.Rows[i].Cells[4].Value = rekord.Forgalomban;
                    Tábla.Rows[i].Cells[5].Value = rekord.Tartalék + rekord.Személyzet;
                    Tábla.Rows[i].Cells[6].Value = rekord.Kocsiszíni;
                    Tábla.Rows[i].Cells[7].Value = rekord.Félreállítás;
                    Tábla.Rows[i].Cells[8].Value = rekord.Főjavítás;
                    Tábla.Rows[i].Cells[9].Value = rekord.Forgalomban + rekord.Tartalék + rekord.Kocsiszíni + rekord.Félreállítás + rekord.Főjavítás + rekord.Személyzet;
                    Tábla.Rows[i].Cells[10].Value = rekord.Személyzet;
                }

                Előírás_kiírás();
                Tábla.Top = 50;
                Tábla.Height = Height - Tábla.Top - 50;
                Tábla.Width = Width - Tábla.Left - 20;
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

        private void Előírás_kiírás()
        {
            try
            {
                List<Adat_Forte_Kiadási_Adatok> Adatok = KézKiadási.Lista_Adatok(Dátum.Value.Year);
                if (Adatok == null || Adatok.Count == 0) return;
                for (int i = 0; i < Tábla.Rows.Count; i++)
                {
                    long kiadás = (from a in Adatok
                                   where a.Napszak == Tábla.Rows[i].Cells[0].Value.ToString().Trim()
                                   && a.Telephely == Cmbtelephely.Text.Trim()
                                   && a.Típus == Tábla.Rows[i].Cells[1].Value.ToString().Trim()
                                   && a.Dátum == Dátum.Value
                                   select a).ToList().Sum(a => a.Kiadás);

                    Tábla.Rows[i].Cells[2].Value = long.Parse(Tábla.Rows[i].Cells[4].Value.ToString()) - kiadás;
                    Tábla.Rows[i].Cells[3].Value = kiadás;

                    if (kiadás > long.Parse(Tábla.Rows[i].Cells[4].Value.ToString())) Tábla.Rows[i].Cells[4].Style.BackColor = Color.Red;
                    if (kiadás < long.Parse(Tábla.Rows[i].Cells[4].Value.ToString())) Tábla.Rows[i].Cells[4].Style.BackColor = Color.Blue;
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

        private void Label6_Click(object sender, EventArgs e)
        {
            Napi_adatok_felirat();
        }

        private void Napi_adatok_felirat()
        {
            Label6.BackColor = Color.SkyBlue;
            Label7.BackColor = Color.LightGreen;
            Label8.BackColor = Color.LightGreen;

            Tábla3.Visible = false;
            Tábla1.Visible = false;
            Tábla2.Visible = false;
            Tábla.Visible = true;
        }

        private void Label7_Click(object sender, EventArgs e)
        {
            Személyzet_felirat();
        }

        private void Személyzet_felirat()
        {
            Label6.BackColor = Color.LightGreen;
            Label7.BackColor = Color.SkyBlue;
            Label8.BackColor = Color.LightGreen;

            Tábla1.Visible = true;
            Tábla2.Visible = false;
            Tábla.Visible = false;
        }

        private void Label8_Click(object sender, EventArgs e)
        {
            Típus_csere_felirat();
        }

        private void Típus_csere_felirat()
        {
            Label6.BackColor = Color.LightGreen;
            Label7.BackColor = Color.LightGreen;
            Label8.BackColor = Color.SkyBlue;

            Tábla1.Visible = false;
            Tábla2.Visible = true;
            Tábla.Visible = false;
        }

        private void Táblázatlistázásszemélyzet()
        {
            try
            {
                List<Adat_Főkönyv_Személyzet> Adatok = KézSzemély.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);
                Adatok = (from a in Adatok
                          where a.Dátum >= MyF.Nap0000(Dátum.Value)
                          && a.Dátum <= MyF.Nap2359(Dátum.Value)
                          orderby a.Napszak, a.Típus
                          select a).ToList();

                Tábla1.Rows.Clear();
                Tábla1.Columns.Clear();
                Tábla1.Refresh();
                Tábla1.Visible = false;
                Tábla1.ColumnCount = 7;

                // fejléc elkészítése
                Tábla1.Columns[0].HeaderText = "Dátum";
                Tábla1.Columns[0].Width = 150;
                Tábla1.Columns[1].HeaderText = "Napszak";
                Tábla1.Columns[1].Width = 150;
                Tábla1.Columns[2].HeaderText = "Típus";
                Tábla1.Columns[2].Width = 150;
                Tábla1.Columns[3].HeaderText = "Viszonylat";
                Tábla1.Columns[3].Width = 150;
                Tábla1.Columns[4].HeaderText = "Forgalmi";
                Tábla1.Columns[4].Width = 150;
                Tábla1.Columns[5].HeaderText = "Indulási idő";
                Tábla1.Columns[5].Width = 150;
                Tábla1.Columns[6].HeaderText = "Pályaszám";
                Tábla1.Columns[6].Width = 150;

                int i;
                foreach (Adat_Főkönyv_Személyzet rekord in Adatok)
                {
                    Tábla1.RowCount++;
                    i = Tábla1.RowCount - 1;

                    Tábla1.Rows[i].Cells[0].Value = rekord.Dátum.ToString("yyyy.MM.dd");
                    Tábla1.Rows[i].Cells[1].Value = rekord.Napszak.Trim();
                    Tábla1.Rows[i].Cells[2].Value = rekord.Típus.Trim();
                    Tábla1.Rows[i].Cells[3].Value = rekord.Viszonylat.Trim();
                    Tábla1.Rows[i].Cells[4].Value = rekord.Forgalmiszám.Trim();
                    Tábla1.Rows[i].Cells[5].Value = rekord.Tervindulás.ToString("hh:mm");
                    Tábla1.Rows[i].Cells[6].Value = rekord.Azonosító.Trim();
                }

                Tábla1.Top = 50;
                Tábla1.Left = 230;
                Tábla1.Height = Height - Tábla1.Top - 50;
                Tábla1.Width = Width - Tábla1.Left - 20;
                Tábla1.Visible = true;
                Tábla1.Refresh();
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

        private void Táblázatlistázástípuscsere()
        {
            try
            {
                List<Adat_FőKönyv_Típuscsere> Adatok = KézTípus.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);
                Adatok = (from a in Adatok
                          where a.Dátum >= MyF.Nap0000(Dátum.Value)
                          && a.Dátum <= MyF.Nap2359(Dátum.Value)
                          orderby a.Napszak, a.Típuselőírt, a.Viszonylat, a.Forgalmiszám
                          select a).ToList();

                Tábla2.Rows.Clear();
                Tábla2.Columns.Clear();
                Tábla2.Refresh();
                Tábla2.Visible = false;
                Tábla2.ColumnCount = 8;

                // fejléc elkészítése
                Tábla2.Columns[0].HeaderText = "Dátum";
                Tábla2.Columns[0].Width = 100;
                Tábla2.Columns[1].HeaderText = "Napszak";
                Tábla2.Columns[1].Width = 80;
                Tábla2.Columns[2].HeaderText = "Típus előírt";
                Tábla2.Columns[2].Width = 100;
                Tábla2.Columns[3].HeaderText = "Típus kiadott";
                Tábla2.Columns[3].Width = 100;
                Tábla2.Columns[4].HeaderText = "Viszonylat";
                Tábla2.Columns[4].Width = 100;
                Tábla2.Columns[5].HeaderText = "Forgalmi";
                Tábla2.Columns[5].Width = 100;
                Tábla2.Columns[6].HeaderText = "Indulási idő";
                Tábla2.Columns[6].Width = 100;
                Tábla2.Columns[7].HeaderText = "Pályaszám";
                Tábla2.Columns[7].Width = 100;
                int i;

                foreach (Adat_FőKönyv_Típuscsere rekord in Adatok)
                {
                    Tábla2.RowCount++;
                    i = Tábla2.RowCount - 1;

                    Tábla2.Rows[i].Cells[0].Value = rekord.Dátum.ToString("yyyy.MM.dd");
                    Tábla2.Rows[i].Cells[1].Value = rekord.Napszak;
                    Tábla2.Rows[i].Cells[2].Value = rekord.Típuselőírt;
                    Tábla2.Rows[i].Cells[3].Value = rekord.Típuskiadott;
                    Tábla2.Rows[i].Cells[4].Value = rekord.Viszonylat;
                    Tábla2.Rows[i].Cells[5].Value = rekord.Forgalmiszám;
                    Tábla2.Rows[i].Cells[6].Value = rekord.Tervindulás.ToString("HH:mm");
                    Tábla2.Rows[i].Cells[7].Value = rekord.Azonosító;
                }

                Tábla2.Top = 50;
                Tábla2.Left = 230;
                Tábla2.Height = Height - Tábla2.Top - 50;
                Tábla2.Width = Width - Tábla2.Left - 20;
                Tábla2.Visible = true;
                Tábla2.Refresh();
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


        #region Napi álló kocsik
        private void Napiállókocsik_Click(object sender, EventArgs e)
        {
            SUBnapihibagöngyölés(Cmbtelephely.Text.Trim());
            SUBNapielkészültek(Dátum.Value, Cmbtelephely.Text.Trim());
            MilyenLista = "napiálló";
            Napihibalista();
        }
        #endregion


        #region Havi adatok
        private void Havilista_Click(object sender, EventArgs e)
        {
            try
            {
                TáblaNév = "Havikiadás";
                Tábla.Visible = false;
                Tábla3.Visible = false;
                MilyenLista = "havilista";

                DateTime hónaputolsónapja = MyF.Hónap_utolsónapja(Dátum.Value);
                DateTime hónapelsőnapja = MyF.Hónap_elsőnapja(Dátum.Value);

                List<Adat_Kiadás_összesítő> Adatok = KézKiadásÖ.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);
                Adatok = (from a in Adatok
                          where a.Dátum >= MyF.Nap0000(hónapelsőnapja)
                          && a.Dátum <= MyF.Nap2359(hónaputolsónapja)
                          orderby a.Dátum, a.Napszak, a.Típus
                          select a).ToList();

                Tábla3.Visible = false;
                Tábla3.DataSource = null;
                Tábla3.Rows.Clear();
                Tábla3.Columns.Clear();

                HaviFejlécTábla();
                Tábla3.DataSource = AdatTábla;
                HaviTartalomTábla(Adatok);
                HaviSzélességTábla();

                Tábla3.Top = 50;
                Tábla3.Left = 230;
                Tábla3.Height = Height - Tábla3.Top - 50;
                Tábla3.Width = Width - Tábla3.Left - 20;
                Tábla3.Visible = true;
                Tábla3.Refresh();
                Tábla3.ClearSelection();

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

        private void HaviFejlécTábla()
        {
            AdatTábla.Columns.Clear();
            AdatTábla.Columns.Add("Dátum");
            AdatTábla.Columns.Add("Napszak");
            AdatTábla.Columns.Add("Típus");
            AdatTábla.Columns.Add("Forgalomban").DataType = typeof(int);
            AdatTábla.Columns.Add("Tartalék").DataType = typeof(int);
            AdatTábla.Columns.Add("Kocsiszíni").DataType = typeof(int);
            AdatTábla.Columns.Add("Félreállítás").DataType = typeof(int);
            AdatTábla.Columns.Add("Főjavítás").DataType = typeof(int);
            AdatTábla.Columns.Add("Összesen").DataType = typeof(int);
            AdatTábla.Columns.Add("Személyzethiány").DataType = typeof(int);
        }

        private void HaviSzélességTábla()
        {
            Tábla3.Columns["Dátum"].Width = 100;
            Tábla3.Columns["Napszak"].Width = 100;
            Tábla3.Columns["Típus"].Width = 100;
            Tábla3.Columns["Forgalomban"].Width = 100;
            Tábla3.Columns["Tartalék"].Width = 100;
            Tábla3.Columns["Kocsiszíni"].Width = 100;
            Tábla3.Columns["Félreállítás"].Width = 100;
            Tábla3.Columns["Főjavítás"].Width = 100;
            Tábla3.Columns["Összesen"].Width = 100;
            Tábla3.Columns["Személyzethiány"].Width = 200;
        }

        private void HaviTartalomTábla(List<Adat_Kiadás_összesítő> Adatok)
        {
            try
            {
                AdatTábla.Clear();
                foreach (Adat_Kiadás_összesítő rekord in Adatok)
                {
                    DataRow Soradat = AdatTábla.NewRow();
                    Soradat["Dátum"] = rekord.Dátum.ToShortDateString();
                    Soradat["Napszak"] = rekord.Napszak.Trim();
                    Soradat["Típus"] = rekord.Típus.Trim();
                    Soradat["Forgalomban"] = rekord.Forgalomban;
                    Soradat["Tartalék"] = rekord.Tartalék + rekord.Személyzet;
                    Soradat["Kocsiszíni"] = rekord.Kocsiszíni;
                    Soradat["Félreállítás"] = rekord.Félreállítás;
                    Soradat["Főjavítás"] = rekord.Főjavítás;
                    Soradat["Összesen"] = rekord.Forgalomban + rekord.Tartalék + rekord.Kocsiszíni + rekord.Félreállítás + rekord.Főjavítás + rekord.Személyzet;
                    Soradat["Személyzethiány"] = rekord.Személyzet;
                    AdatTábla.Rows.Add(Soradat);
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

        private void Havielkészültkocsik_Click(object sender, EventArgs e)
        {
            SUBnapihibagöngyölés(Cmbtelephely.Text.Trim());
            SUBNapielkészültek(Dátum.Value, Cmbtelephely.Text.Trim());
            MilyenLista = "havikészült";
            Napihibalista();
        }

        private void Haviszemélyzethiány_Click(object sender, EventArgs e)
        {
            try
            {
                MilyenLista = "haviszem";
                DateTime hónaputolsónapja = MyF.Hónap_utolsónapja(Dátum.Value);
                DateTime hónapelsőnapja = MyF.Hónap_elsőnapja(Dátum.Value);

                List<Adat_Főkönyv_Személyzet> Adatok = KézSzemély.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);
                Adatok = (from a in Adatok
                          where a.Dátum >= MyF.Nap0000(hónapelsőnapja)
                          && a.Dátum <= MyF.Nap2359(hónaputolsónapja)
                          orderby a.Dátum, a.Napszak, a.Típus
                          select a).ToList();


                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 7;

                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Dátum";
                Tábla.Columns[0].Width = 100;
                Tábla.Columns[1].HeaderText = "Napszak";
                Tábla.Columns[1].Width = 100;
                Tábla.Columns[2].HeaderText = "Típus";
                Tábla.Columns[2].Width = 100;
                Tábla.Columns[3].HeaderText = "Viszonylat";
                Tábla.Columns[3].Width = 100;
                Tábla.Columns[4].HeaderText = "Forgalmi";
                Tábla.Columns[4].Width = 100;
                Tábla.Columns[5].HeaderText = "Indulási idő";
                Tábla.Columns[5].Width = 100;
                Tábla.Columns[6].HeaderText = "Pályaszám";
                Tábla.Columns[6].Width = 100;
                int i;
                foreach (Adat_Főkönyv_Személyzet rekord in Adatok)
                {

                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;

                    Tábla.Rows[i].Cells[1].Value = rekord.Napszak.Trim();
                    Tábla.Rows[i].Cells[2].Value = rekord.Típus.Trim();
                    Tábla.Rows[i].Cells[0].Value = rekord.Dátum.ToString("yyyy.MM.dd");
                    Tábla.Rows[i].Cells[3].Value = rekord.Viszonylat.Trim();
                    Tábla.Rows[i].Cells[4].Value = rekord.Forgalmiszám.Trim();
                    Tábla.Rows[i].Cells[5].Value = rekord.Tervindulás.ToString("hh:mm");
                    Tábla.Rows[i].Cells[6].Value = rekord.Azonosító.Trim();
                }

                Tábla.Top = 50;
                Tábla.Left = 230;
                Tábla.Height = Height - Tábla.Top - 50;
                Tábla.Width = Width - Tábla.Left - 20;
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

        private void Havitípuscsere_Click(object sender, EventArgs e)
        {
            try
            {
                MilyenLista = "haviszem";
                DateTime hónaputolsónapja = MyF.Hónap_utolsónapja(Dátum.Value);
                DateTime hónapelsőnapja = MyF.Hónap_elsőnapja(Dátum.Value);
                List<Adat_FőKönyv_Típuscsere> Adatok = KézTípus.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);
                Adatok = (from a in Adatok
                          where a.Dátum >= MyF.Nap0000(hónapelsőnapja)
                          && a.Dátum <= MyF.Nap2359(hónaputolsónapja)
                          orderby a.Dátum, a.Napszak, a.Típuselőírt
                          select a).ToList();

                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 8;

                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Dátum";
                Tábla.Columns[0].Width = 100;
                Tábla.Columns[1].HeaderText = "Napszak";
                Tábla.Columns[1].Width = 100;
                Tábla.Columns[2].HeaderText = "Típus előírt";
                Tábla.Columns[2].Width = 100;
                Tábla.Columns[3].HeaderText = "Típus kiadott";
                Tábla.Columns[3].Width = 100;
                Tábla.Columns[4].HeaderText = "Viszonylat";
                Tábla.Columns[4].Width = 100;
                Tábla.Columns[5].HeaderText = "Forgalmi";
                Tábla.Columns[5].Width = 100;
                Tábla.Columns[6].HeaderText = "Indulási idő";
                Tábla.Columns[6].Width = 100;
                Tábla.Columns[7].HeaderText = "Pályaszám";
                Tábla.Columns[7].Width = 100;

                int i;

                foreach (Adat_FőKönyv_Típuscsere rekord in Adatok)
                {
                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;

                    Tábla.Rows[i].Cells[0].Value = rekord.Dátum.ToString("yyyy.MM.dd");
                    Tábla.Rows[i].Cells[1].Value = rekord.Napszak;
                    Tábla.Rows[i].Cells[2].Value = rekord.Típuselőírt;
                    Tábla.Rows[i].Cells[3].Value = rekord.Típuskiadott;
                    Tábla.Rows[i].Cells[4].Value = rekord.Viszonylat;
                    Tábla.Rows[i].Cells[5].Value = rekord.Forgalmiszám;
                    Tábla.Rows[i].Cells[6].Value = rekord.Tervindulás.ToString("HH:mm");
                    Tábla.Rows[i].Cells[7].Value = rekord.Azonosító;
                }

                Tábla.Top = 50;
                Tábla.Left = 230;
                Tábla.Height = Height - Tábla.Top - 50;
                Tábla.Width = Width - Tábla.Left - 20;
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
        #endregion


        #region Napi adatok stb
        private void Napielkészültek_Click(object sender, EventArgs e)
        {
            SUBnapihibagöngyölés(Cmbtelephely.Text.Trim());
            SUBNapielkészültek(Dátum.Value, Cmbtelephely.Text.Trim());
            MilyenLista = "elkészült";
            Napihibalista();
        }

        private void Napikarbantartás_Click(object sender, EventArgs e)
        {
            try
            {
                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 7;

                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Srsz";
                Tábla.Columns[0].Width = 80;
                Tábla.Columns[1].HeaderText = "Psz";
                Tábla.Columns[1].Width = 100;
                Tábla.Columns[2].HeaderText = "Dátum";
                Tábla.Columns[2].Width = 200;
                Tábla.Columns[3].HeaderText = "Hiba szöveg";
                Tábla.Columns[3].Width = 400;
                Tábla.Columns[4].HeaderText = "Hiba státus";
                Tábla.Columns[4].Width = 150;
                Tábla.Columns[5].HeaderText = "Javítva?";
                Tábla.Columns[5].Width = 100;
                Tábla.Columns[6].HeaderText = "Módosító";
                Tábla.Columns[6].Width = 100;

                // csak azokat listázzuk amik be vannak jelölve

                List<Adat_Kiegészítő_Hibaterv> KAdatok = KézKiegHibaTerv.Lista_Adatok(Cmbtelephely.Text.Trim());

                foreach (Adat_Kiegészítő_Hibaterv rekordkieg in KAdatok)
                {
                    List<Adat_Jármű_hiba> Adatok = KézJárműHiba.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value);
                    Adatok = (from a in Adatok
                              where a.Idő >= MyF.Nap0000(Dátum.Value)
                              && a.Idő < MyF.Nap2359(Dátum.Value)
                              && a.Javítva == true
                              orderby a.Azonosító
                              select a).ToList();
                    int i;
                    foreach (Adat_Jármű_hiba rekord in Adatok)
                    {
                        if (rekord.Hibaleírása.Contains(rekordkieg.Szöveg.Trim()))
                        {
                            Tábla.RowCount++;
                            i = Tábla.RowCount - 1;
                            Tábla.Rows[i].Cells[0].Value = i;
                            Tábla.Rows[i].Cells[1].Value = rekord.Azonosító.Trim();
                            Tábla.Rows[i].Cells[2].Value = rekord.Idő.ToString("yyyy.MM.dd HH:mm");
                            Tábla.Rows[i].Cells[3].Value = rekord.Hibaleírása.Trim();
                            switch (rekord.Korlát)
                            {
                                case 1:
                                    {
                                        Tábla.Rows[i].Cells[4].Value = "Szabad";
                                        break;
                                    }
                                case 2:
                                    {
                                        Tábla.Rows[i].Cells[4].Value = "Beállóba kért";
                                        break;
                                    }
                                case 3:
                                    {
                                        Tábla.Rows[i].Cells[4].Value = "Csak beálló";
                                        break;
                                    }
                                case 4:
                                    {
                                        Tábla.Rows[i].Cells[4].Value = "Nem kiadható";
                                        break;
                                    }
                            }
                            if (rekord.Javítva == true)
                                Tábla.Rows[i].Cells[5].Value = "Igen";
                            else
                                Tábla.Rows[i].Cells[5].Value = "Nem";

                            Tábla.Rows[i].Cells[6].Value = rekord.Létrehozta.Trim();
                        }
                    }
                }

                Tábla.Top = 50;
                Tábla.Left = 230;
                Tábla.Height = Height - Tábla.Top - 50;
                Tábla.Width = Width - Tábla.Left - 20;
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
        #endregion


        #region Napi adatok frissítése
        private void Napiadatok_Frissítése_Click(object sender, EventArgs e)
        {
            MilyenLista = "minden";
            Főkönyv_Funkciók.Napiadatokmentése("de", Dátum.Value, Cmbtelephely.Text);
            Főkönyv_Funkciók.Napiadatokmentése("du", Dátum.Value, Cmbtelephely.Text);
            Főkönyv_Funkciók.Napitipuscsere("de", Dátum.Value, Cmbtelephely.Text);
            Főkönyv_Funkciók.Napitipuscsere("du", Dátum.Value, Cmbtelephely.Text);
            Főkönyv_Funkciók.Napiszemélyzet("de", Dátum.Value, Cmbtelephely.Text);
            Főkönyv_Funkciók.Napiszemélyzet("du", Dátum.Value, Cmbtelephely.Text);
            Főkönyv_Funkciók.Napitöbblet("de", Dátum.Value, Cmbtelephely.Text);
            Főkönyv_Funkciók.Napitöbblet("du", Dátum.Value, Cmbtelephely.Text);

            Napi_kiadási_adatok();
        }
        #endregion
    }
}