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

        readonly DataTable AdatTábla = new DataTable();
        readonly DataTable AdatTábla1 = new DataTable();
        readonly DataTable AdatTábla2 = new DataTable();
        string TáblaNév = "";

        public Ablak_Napiadatok()
        {
            InitializeComponent();
            Start();
        }

        string MilyenLista;

        #region Alap

        private void Start()
        {
            try
            {
                //Ha az első karakter "R" akkor az új jogosultságkiosztást használjuk
                //ha nem akkor a régit használjuk
                if (Program.PostásJogkör.Substring(0, 1) == "R")
                {
                    TelephelyekFeltöltéseÚj();
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
                }
                else
                {
                    Telephelyekfeltöltése();
                    Jogosultságkiosztás();
                }

                KeyPreview = true;
                Dátum.Value = DateTime.Today;
                Dátum.MaxDate = DateTime.Today;

                Táblaalaphelyzet();
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

        private void Ablak_Napiadatok_Load(object sender, EventArgs e)
        {

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
        private void TelephelyekFeltöltéseÚj()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Adat in GombLathatosagKezelo.Telephelyek(this.Name))
                    Cmbtelephely.Items.Add(Adat.Trim());
                //Alapkönyvtárat beállítjuk 
                if (Cmbtelephely.Items.Cast<string>().Contains(Program.PostásTelephely))
                    Cmbtelephely.Text = Program.PostásTelephely;
                else
                    Cmbtelephely.Text = Cmbtelephely.Items[0].ToStrTrim();
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

                switch (TáblaNév)
                {
                    case "Havikiadás":
                        MyE.DataTableToExcel(fájlexc, AdatTábla);
                        break;
                    case "ÁllóKocsik":
                        MyE.DataTableToExcel(fájlexc, AdatTábla);
                        break;
                    case "Napikiadás":
                        MyE.DataTableToExcel(fájlexc, AdatTábla);
                        break;
                    case "napiálló":
                        MyE.DataTableToExcel(fájlexc, AdatTábla);
                        break;
                    case "Típuscsere":
                        MyE.DataTableToExcel(fájlexc, AdatTábla2);
                        break;
                    case "Személyzet":
                        MyE.DataTableToExcel(fájlexc, AdatTábla1);
                        break;
                    case "elkészült":
                        MyE.DataTableToExcel(fájlexc, AdatTábla);
                        break;
                    case "havikészült":
                        MyE.DataTableToExcel(fájlexc, AdatTábla);
                        break;
                    case "NapiKarban":
                        MyE.DataTableToExcel(fájlexc, AdatTábla);
                        break;
                    case "HaviSzem":
                        MyE.DataTableToExcel(fájlexc, AdatTábla);
                        break;
                    case "HaviTípus":
                        MyE.DataTableToExcel(fájlexc, AdatTábla);
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


        #region Állókocsik, Napi álló kocsik, Napielkészültek,Havielkészültkocsik
        private void Állókocsik_Click(object sender, EventArgs e)
        {
            Napi_adatok_felirat();
            SUBnapihibagöngyölés(Cmbtelephely.Text.Trim());
            SUBNapielkészültek(Dátum.Value, Cmbtelephely.Text.Trim());
            MilyenLista = "minden";
            TáblaNév = "ÁllóKocsik";
            Napihibalista();
        }

        private void Napihibalista()
        {
            try
            {
                List<Adat_Jármű_Javításiátfutástábla> Adatok = KézÁtfutás.Lista_Adatok(Cmbtelephely.Text.Trim());

                if (MilyenLista.Trim() == "napiálló")
                {
                    Adatok = (from a in Adatok
                              where a.Kezdődátum >= MyF.Nap0000(Dátum.Value)
                              && a.Kezdődátum <= MyF.Nap2359(Dátum.Value)
                              orderby a.Azonosító
                              select a).ToList();
                }
                if (MilyenLista.Trim() == "elkészült")
                {
                    Adatok = (from a in Adatok
                              where a.Végdátum >= MyF.Nap0000(Dátum.Value)
                              && a.Végdátum <= MyF.Nap2359(Dátum.Value)
                              orderby a.Azonosító
                              select a).ToList();
                }
                if (MilyenLista.Trim() == "havikészült")
                {
                    Adatok = (from a in Adatok
                              where a.Végdátum >= MyF.Nap0000(MyF.Hónap_elsőnapja(Dátum.Value))
                              && a.Végdátum <= MyF.Nap2359(MyF.Hónap_utolsónapja(Dátum.Value))
                              orderby a.Azonosító
                              select a).ToList();
                }

                Tábla.CleanFilterAndSort();
                Tábla.Visible = false;
                Tábla.DataSource = null;
                Tábla.Rows.Clear();
                Tábla.Columns.Clear();


                NapiFejlécTábla();
                NapiTartalomTábla(Adatok);
                KötésiOsztály.DataSource = AdatTábla;
                Tábla.DataSource = KötésiOsztály;
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
            try
            {

                AdatTábla.Rows.Clear();
                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Azonosító");
                AdatTábla.Columns.Add("Kezdő dátum");
                AdatTábla.Columns.Add("Végső dátum");
                AdatTábla.Columns.Add("Állási napok", typeof(int));
                AdatTábla.Columns.Add("Hiba leírása");
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

        private void Napiállókocsik_Click(object sender, EventArgs e)
        {
            Napi_adatok_felirat();
            SUBnapihibagöngyölés(Cmbtelephely.Text.Trim());
            SUBNapielkészültek(Dátum.Value, Cmbtelephely.Text.Trim());
            MilyenLista = "napiálló";
            TáblaNév = "napiálló";

            Napihibalista();
        }

        private void Napielkészültek_Click(object sender, EventArgs e)
        {
            Napi_adatok_felirat();
            SUBnapihibagöngyölés(Cmbtelephely.Text.Trim());
            SUBNapielkészültek(Dátum.Value, Cmbtelephely.Text.Trim());
            MilyenLista = "elkészült";
            TáblaNév = "elkészült";
            Napihibalista();
        }

        private void Havielkészültkocsik_Click(object sender, EventArgs e)
        {
            Napi_adatok_felirat();
            SUBnapihibagöngyölés(Cmbtelephely.Text.Trim());
            SUBNapielkészültek(Dátum.Value, Cmbtelephely.Text.Trim());
            MilyenLista = "havikészült";
            TáblaNév = "havikészült";
            Napihibalista();
        }
        #endregion


        #region Napi kiadási adatok
        private void Lista_Click(object sender, EventArgs e)
        {
            TáblaNév = "Napikiadás";
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
                Tábla.CleanFilterAndSort();
                Tábla.Visible = false;
                Tábla.DataSource = null;
                Tábla.Rows.Clear();
                Tábla.Columns.Clear();

                NapiKiadFejlécTábla();
                NapiKiadTartalomTábla();
                KötésiOsztály.DataSource = AdatTábla;
                Tábla.DataSource = KötésiOsztály;
                NapiKiadSzélességTábla();
                NapiKiadTáblaSzínezés();

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

        private void NapiKiadTáblaSzínezés()
        {
            if (Tábla.Rows.Count < 1) return;
            try
            {
                foreach (DataGridViewRow Sor in Tábla.Rows)
                {
                    if (Sor.Cells["Eltérés"].Value.ToÉrt_Long() > 0) Sor.Cells["Eltérés"].Style.BackColor = Color.Red;
                    if (Sor.Cells["Eltérés"].Value.ToÉrt_Long() < 0) Sor.Cells["Eltérés"].Style.BackColor = Color.Blue;
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

        private void NapiKiadFejlécTábla()
        {
            AdatTábla.Columns.Clear();
            AdatTábla.Columns.Add("Napszak");
            AdatTábla.Columns.Add("Típus");
            AdatTábla.Columns.Add("Eltérés", typeof(long));
            AdatTábla.Columns.Add("Előírás", typeof(long));
            AdatTábla.Columns.Add("Forgalomban", typeof(long));
            AdatTábla.Columns.Add("Tartalék", typeof(long));
            AdatTábla.Columns.Add("Kocsiszíni", typeof(long));
            AdatTábla.Columns.Add("Félreállítás", typeof(long));
            AdatTábla.Columns.Add("Főjavítás", typeof(long));
            AdatTábla.Columns.Add("Összesen", typeof(long));
            AdatTábla.Columns.Add("Személyzethiány", typeof(long));
        }

        private void NapiKiadSzélességTábla()
        {
            Tábla.Columns["Napszak"].Width = 100;
            Tábla.Columns["Típus"].Width = 120;
            Tábla.Columns["Eltérés"].Width = 80;
            Tábla.Columns["Előírás"].Width = 80;
            Tábla.Columns["Forgalomban"].Width = 100;
            Tábla.Columns["Tartalék"].Width = 100;
            Tábla.Columns["Kocsiszíni"].Width = 100;
            Tábla.Columns["Félreállítás"].Width = 100;
            Tábla.Columns["Főjavítás"].Width = 100;
            Tábla.Columns["Összesen"].Width = 100;
            Tábla.Columns["Személyzethiány"].Width = 200;
        }

        private void NapiKiadTartalomTábla()
        {
            try
            {
                List<Adat_Kiadás_összesítő> Adatok = KézKiadásÖ.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);

                Adatok = (from a in Adatok
                          where a.Dátum.ToShortDateString() == Dátum.Value.ToShortDateString()
                          orderby a.Napszak, a.Típus
                          select a).ToList();

                List<Adat_Forte_Kiadási_Adatok> AdatokKiad = KézKiadási.Lista_Adatok(Dátum.Value.Year);

                AdatTábla.Clear();
                foreach (Adat_Kiadás_összesítő rekord in Adatok)
                {
                    long kiadás = (from a in AdatokKiad
                                   where a.Napszak == rekord.Napszak.Trim()
                                   && a.Telephely == Cmbtelephely.Text.Trim()
                                   && a.Típus == rekord.Típus.Trim()
                                   && a.Dátum == Dátum.Value
                                   select a).ToList().Sum(a => a.Kiadás);

                    DataRow Soradat = AdatTábla.NewRow();
                    Soradat["Napszak"] = rekord.Napszak.Trim();
                    Soradat["Típus"] = rekord.Típus.Trim();
                    Soradat["Eltérés"] = rekord.Forgalomban - kiadás;
                    Soradat["Előírás"] = kiadás;
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

        private void Label6_Click(object sender, EventArgs e)
        {
            Napi_adatok_felirat();
        }

        private void Napi_adatok_felirat()
        {
            Label6.BackColor = Color.SkyBlue;
            Label7.BackColor = Color.LightGreen;
            Label8.BackColor = Color.LightGreen;

            Tábla1.Visible = false;
            Tábla2.Visible = false;
            Tábla.Visible = true;
            TáblaNév = "Napikiadás";
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
            TáblaNév = "Személyzet";
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
            TáblaNév = "Típuscsere";
        }
        #endregion


        #region Személyzet hiány tábla
        private void Táblázatlistázásszemélyzet()
        {
            try
            {

                Tábla1.CleanFilterAndSort();
                Tábla1.Visible = false;
                Tábla1.DataSource = null;
                Tábla1.Rows.Clear();
                Tábla1.Columns.Clear();

                SzemélyFejlécTábla();
                SzemélyTartalomTábla();
                KötésiOsztály1.DataSource = AdatTábla1;
                Tábla1.DataSource = KötésiOsztály1;
                SzemélySzélességTábla();

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

        private void SzemélyFejlécTábla()
        {
            AdatTábla1.Columns.Clear();
            AdatTábla1.Columns.Add("Dátum");
            AdatTábla1.Columns.Add("Napszak");
            AdatTábla1.Columns.Add("Típus");
            AdatTábla1.Columns.Add("Viszonylat");
            AdatTábla1.Columns.Add("Forgalmi");
            AdatTábla1.Columns.Add("Indulási idő");
            AdatTábla1.Columns.Add("Pályaszám");
        }

        private void SzemélySzélességTábla()
        {
            Tábla1.Columns["Dátum"].Width = 150;
            Tábla1.Columns["Napszak"].Width = 150;
            Tábla1.Columns["Típus"].Width = 150;
            Tábla1.Columns["Viszonylat"].Width = 150;
            Tábla1.Columns["Forgalmi"].Width = 150;
            Tábla1.Columns["Indulási idő"].Width = 150;
            Tábla1.Columns["Pályaszám"].Width = 150;
        }

        private void SzemélyTartalomTábla()
        {
            try
            {
                List<Adat_Főkönyv_Személyzet> Adatok = KézSzemély.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);
                Adatok = (from a in Adatok
                          where a.Dátum >= MyF.Nap0000(Dátum.Value)
                          && a.Dátum <= MyF.Nap2359(Dátum.Value)
                          orderby a.Napszak, a.Típus
                          select a).ToList();

                AdatTábla1.Clear();
                foreach (Adat_Főkönyv_Személyzet rekord in Adatok)
                {
                    DataRow Soradat = AdatTábla1.NewRow();
                    Soradat["Dátum"] = rekord.Dátum.ToString("yyyy.MM.dd");
                    Soradat["Napszak"] = rekord.Napszak.Trim();
                    Soradat["Típus"] = rekord.Típus.Trim();
                    Soradat["Viszonylat"] = rekord.Viszonylat.Trim();
                    Soradat["Forgalmi"] = rekord.Forgalmiszám.Trim();
                    Soradat["Indulási idő"] = rekord.Tervindulás.ToString("hh:mm");
                    Soradat["Pályaszám"] = rekord.Azonosító.Trim();
                    AdatTábla1.Rows.Add(Soradat);
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


        #region Típus csere tábla
        private void Táblázatlistázástípuscsere()
        {
            try
            {
                Tábla2.CleanFilterAndSort();
                Tábla2.Visible = false;
                Tábla2.DataSource = null;
                Tábla2.Rows.Clear();
                Tábla2.Columns.Clear();

                TípusCsereFejlécTábla();
                TípusCsereTartalomTábla();
                KötésiOsztály2.DataSource = AdatTábla2;
                Tábla2.DataSource = KötésiOsztály2;
                TípusCsereSzélességTábla();


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

        private void TípusCsereFejlécTábla()
        {
            AdatTábla2.Columns.Clear();
            AdatTábla2.Columns.Add("Dátum");
            AdatTábla2.Columns.Add("Napszak");
            AdatTábla2.Columns.Add("Típus előírt");
            AdatTábla2.Columns.Add("Típus kiadott");
            AdatTábla2.Columns.Add("Viszonylat");
            AdatTábla2.Columns.Add("Forgalmi");
            AdatTábla2.Columns.Add("Indulási idő");
            AdatTábla2.Columns.Add("Pályaszám");
        }

        private void TípusCsereSzélességTábla()
        {
            Tábla2.Columns["Dátum"].Width = 100;
            Tábla2.Columns["Napszak"].Width = 80;
            Tábla2.Columns["Típus előírt"].Width = 200;
            Tábla2.Columns["Típus kiadott"].Width = 200;
            Tábla2.Columns["Viszonylat"].Width = 100;
            Tábla2.Columns["Forgalmi"].Width = 100;
            Tábla2.Columns["Indulási idő"].Width = 100;
            Tábla2.Columns["Pályaszám"].Width = 100;
        }

        private void TípusCsereTartalomTábla()
        {
            try
            {
                List<Adat_FőKönyv_Típuscsere> Adatok = KézTípus.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);
                Adatok = (from a in Adatok
                          where a.Dátum >= MyF.Nap0000(Dátum.Value)
                          && a.Dátum <= MyF.Nap2359(Dátum.Value)
                          orderby a.Napszak, a.Típuselőírt, a.Viszonylat, a.Forgalmiszám
                          select a).ToList();
                AdatTábla2.Clear();

                foreach (Adat_FőKönyv_Típuscsere rekord in Adatok)
                {
                    DataRow Soradat = AdatTábla2.NewRow();
                    Soradat["Dátum"] = rekord.Dátum.ToString("yyyy.MM.dd");
                    Soradat["Napszak"] = rekord.Napszak;
                    Soradat["Típus előírt"] = rekord.Típuselőírt;
                    Soradat["Típus kiadott"] = rekord.Típuskiadott;
                    Soradat["Viszonylat"] = rekord.Viszonylat;
                    Soradat["Forgalmi"] = rekord.Forgalmiszám;
                    Soradat["Indulási idő"] = rekord.Tervindulás.ToString("HH:mm");
                    Soradat["Pályaszám"] = rekord.Azonosító;

                    AdatTábla2.Rows.Add(Soradat);
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


        #region Havi adatok
        private void Havilista_Click(object sender, EventArgs e)
        {
            try
            {
                Napi_adatok_felirat();
                TáblaNév = "Havikiadás";
                MilyenLista = "havilista";

                DateTime hónaputolsónapja = MyF.Hónap_utolsónapja(Dátum.Value);
                DateTime hónapelsőnapja = MyF.Hónap_elsőnapja(Dátum.Value);

                List<Adat_Kiadás_összesítő> Adatok = KézKiadásÖ.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);
                Adatok = (from a in Adatok
                          where a.Dátum >= MyF.Nap0000(hónapelsőnapja)
                          && a.Dátum <= MyF.Nap2359(hónaputolsónapja)
                          orderby a.Dátum, a.Napszak, a.Típus
                          select a).ToList();

                Tábla.CleanFilterAndSort();
                Tábla.Visible = false;
                Tábla.DataSource = null;
                Tábla.Rows.Clear();
                Tábla.Columns.Clear();

                HaviFejlécTábla();
                KötésiOsztály.DataSource = AdatTábla;
                Tábla.DataSource = KötésiOsztály;
                HaviTartalomTábla(Adatok);
                HaviSzélességTábla();

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

        private void HaviFejlécTábla()
        {
            AdatTábla.Columns.Clear();
            AdatTábla.Columns.Add("Dátum");
            AdatTábla.Columns.Add("Napszak");
            AdatTábla.Columns.Add("Típus");
            AdatTábla.Columns.Add("Forgalomban", typeof(int));
            AdatTábla.Columns.Add("Tartalék", typeof(int));
            AdatTábla.Columns.Add("Kocsiszíni", typeof(int));
            AdatTábla.Columns.Add("Félreállítás", typeof(int));
            AdatTábla.Columns.Add("Főjavítás", typeof(int));
            AdatTábla.Columns.Add("Összesen", typeof(int));
            AdatTábla.Columns.Add("Személyzethiány", typeof(int));
        }

        private void HaviSzélességTábla()
        {
            Tábla.Columns["Dátum"].Width = 100;
            Tábla.Columns["Napszak"].Width = 100;
            Tábla.Columns["Típus"].Width = 100;
            Tábla.Columns["Forgalomban"].Width = 100;
            Tábla.Columns["Tartalék"].Width = 100;
            Tábla.Columns["Kocsiszíni"].Width = 100;
            Tábla.Columns["Félreállítás"].Width = 100;
            Tábla.Columns["Főjavítás"].Width = 100;
            Tábla.Columns["Összesen"].Width = 100;
            Tábla.Columns["Személyzethiány"].Width = 200;
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

        private void Haviszemélyzethiány_Click(object sender, EventArgs e)
        {
            try
            {
                Napi_adatok_felirat();
                TáblaNév = "HaviSzem";

                MilyenLista = "haviszem";
                Tábla.CleanFilterAndSort();
                Tábla.Visible = false;
                Tábla.DataSource = null;
                Tábla.Rows.Clear();
                Tábla.Columns.Clear();

                NapiSzemFejlécTábla();
                NapiSzemTartalomTábla();
                KötésiOsztály.DataSource = AdatTábla;
                Tábla.DataSource = KötésiOsztály;
                NapiSzemSzélességTábla();

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

        private void NapiSzemFejlécTábla()
        {
            AdatTábla.Columns.Clear();
            AdatTábla.Columns.Add("Dátum");
            AdatTábla.Columns.Add("Napszak");
            AdatTábla.Columns.Add("Típus");
            AdatTábla.Columns.Add("Viszonylat");
            AdatTábla.Columns.Add("Forgalmi");
            AdatTábla.Columns.Add("Indulási idő");
            AdatTábla.Columns.Add("Pályaszám");
        }

        private void NapiSzemSzélességTábla()
        {
            Tábla.Columns["Dátum"].Width = 100;
            Tábla.Columns["Napszak"].Width = 100;
            Tábla.Columns["Típus"].Width = 100;
            Tábla.Columns["Viszonylat"].Width = 100;
            Tábla.Columns["Forgalmi"].Width = 100;
            Tábla.Columns["Indulási idő"].Width = 100;
            Tábla.Columns["Pályaszám"].Width = 100;
        }

        private void NapiSzemTartalomTábla()
        {
            try
            {
                DateTime hónaputolsónapja = MyF.Hónap_utolsónapja(Dátum.Value);
                DateTime hónapelsőnapja = MyF.Hónap_elsőnapja(Dátum.Value);

                List<Adat_Főkönyv_Személyzet> Adatok = KézSzemély.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);
                Adatok = (from a in Adatok
                          where a.Dátum >= MyF.Nap0000(hónapelsőnapja)
                          && a.Dátum <= MyF.Nap2359(hónaputolsónapja)
                          orderby a.Dátum, a.Napszak, a.Típus
                          select a).ToList();

                AdatTábla.Clear();
                foreach (Adat_Főkönyv_Személyzet rekord in Adatok)
                {
                    DataRow Soradat = AdatTábla.NewRow();
                    Soradat["Dátum"] = rekord.Dátum.ToString("yyyy.MM.dd");
                    Soradat["Napszak"] = rekord.Napszak.Trim();
                    Soradat["Típus"] = rekord.Típus.Trim();
                    Soradat["Viszonylat"] = rekord.Viszonylat.Trim();
                    Soradat["Forgalmi"] = rekord.Forgalmiszám.Trim();
                    Soradat["Indulási idő"] = rekord.Tervindulás.ToString("hh:mm");
                    Soradat["Pályaszám"] = rekord.Azonosító.Trim();
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

        private void Havitípuscsere_Click(object sender, EventArgs e)
        {
            try
            {
                Napi_adatok_felirat();
                TáblaNév = "HaviTípus";

                Tábla.CleanFilterAndSort();
                Tábla.Visible = false;
                Tábla.DataSource = null;
                Tábla.Rows.Clear();
                Tábla.Columns.Clear();

                HaviTípusFejlécTábla();
                HaviTípusTartalomTábla();
                KötésiOsztály.DataSource = AdatTábla;
                Tábla.DataSource = KötésiOsztály;
                HaviTípusSzélességTábla();

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

        private void HaviTípusFejlécTábla()
        {
            AdatTábla.Columns.Clear();
            AdatTábla.Columns.Add("Dátum");
            AdatTábla.Columns.Add("Napszak");
            AdatTábla.Columns.Add("Típus előírt");
            AdatTábla.Columns.Add("Típus kiadott");
            AdatTábla.Columns.Add("Viszonylat");
            AdatTábla.Columns.Add("Forgalmi");
            AdatTábla.Columns.Add("Indulási idő");
            AdatTábla.Columns.Add("Pályaszám");
        }

        private void HaviTípusSzélességTábla()
        {
            Tábla.Columns["Napszak"].Width = 100;
            Tábla.Columns["Típus előírt"].Width = 100;
            Tábla.Columns["Típus kiadott"].Width = 100;
            Tábla.Columns["Viszonylat"].Width = 100;
            Tábla.Columns["Forgalmi"].Width = 100;
            Tábla.Columns["Indulási idő"].Width = 100;
            Tábla.Columns["Pályaszám"].Width = 100;
        }

        private void HaviTípusTartalomTábla()
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

                AdatTábla.Clear();
                foreach (Adat_FőKönyv_Típuscsere rekord in Adatok)
                {
                    DataRow Soradat = AdatTábla.NewRow();
                    Soradat["Dátum"] = rekord.Dátum.ToString("yyyy.MM.dd");
                    Soradat["Napszak"] = rekord.Napszak.Trim();
                    Soradat["Típus előírt"] = rekord.Típuselőírt.Trim();
                    Soradat["Típus kiadott"] = rekord.Típuskiadott.Trim();
                    Soradat["Viszonylat"] = rekord.Viszonylat.Trim();
                    Soradat["Forgalmi"] = rekord.Forgalmiszám.Trim();
                    Soradat["Indulási idő"] = rekord.Tervindulás.ToString("HH:mm");
                    Soradat["Pályaszám"] = rekord.Azonosító.Trim();
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


        #region Napi adatok stb
        private void Napikarbantartás_Click(object sender, EventArgs e)
        {
            try
            {
                Napi_adatok_felirat();
                TáblaNév = "NapiKarban";

                Tábla.CleanFilterAndSort();
                Tábla.Visible = false;
                Tábla.DataSource = null;
                Tábla.Rows.Clear();
                Tábla.Columns.Clear();

                NapiKarbFejlécTábla();
                NapiKarbTartalomTábla();
                KötésiOsztály.DataSource = AdatTábla;
                Tábla.DataSource = KötésiOsztály;
                NapiKarbSzélességTábla();

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

        private void NapiKarbFejlécTábla()
        {
            AdatTábla.Columns.Clear();
            AdatTábla.Columns.Add("Srsz");
            AdatTábla.Columns.Add("Psz");
            AdatTábla.Columns.Add("Dátum");
            AdatTábla.Columns.Add("Hiba szöveg");
            AdatTábla.Columns.Add("Hiba státus");
            AdatTábla.Columns.Add("Javítva");
            AdatTábla.Columns.Add("Módosító");
        }

        private void NapiKarbSzélességTábla()
        {
            Tábla.Columns["Srsz"].Width = 80;
            Tábla.Columns["Psz"].Width = 100;
            Tábla.Columns["Dátum"].Width = 200;
            Tábla.Columns["Hiba szöveg"].Width = 400;
            Tábla.Columns["Hiba státus"].Width = 150;
            Tábla.Columns["Javítva"].Width = 100;
            Tábla.Columns["Módosító"].Width = 100;
        }

        private void NapiKarbTartalomTábla()
        {
            try
            {
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
                    AdatTábla.Clear();
                    int i = 1;
                    foreach (Adat_Jármű_hiba rekord in Adatok)
                    {
                        if (rekord.Hibaleírása.Contains(rekordkieg.Szöveg.Trim()))
                        {
                            string Státusz = "";
                            switch (rekord.Korlát)
                            {
                                case 1:
                                    {
                                        Státusz = "Szabad";
                                        break;
                                    }
                                case 2:
                                    {
                                        Státusz = "Beállóba kért";
                                        break;
                                    }
                                case 3:
                                    {
                                        Státusz = "Csak beálló";
                                        break;
                                    }
                                case 4:
                                    {
                                        Státusz = "Nem kiadható";
                                        break;
                                    }
                            }

                            DataRow Soradat = AdatTábla.NewRow();
                            Soradat["Srsz"] = i++;
                            Soradat["Psz"] = rekord.Azonosító.Trim();
                            Soradat["Dátum"] = rekord.Idő.ToString("yyyy.MM.dd HH:mm");
                            Soradat["Hiba szöveg"] = rekord.Hibaleírása.Trim();
                            Soradat["Hiba státus"] = Státusz;
                            Soradat["Javítva"] = rekord.Javítva ? "Igen" : "Nem";
                            Soradat["Módosító"] = rekord.Létrehozta.Trim();
                            AdatTábla.Rows.Add(Soradat);
                        }
                    }
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

        private void Cmbtelephely_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                Cmbtelephely.Text = Cmbtelephely.Items[Cmbtelephely.SelectedIndex].ToStrTrim();
                if (Cmbtelephely.Text.Trim() == "") return;
                if (Program.PostásJogkör.Any(c => c != '0'))
                {

                }
                else
                {
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
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