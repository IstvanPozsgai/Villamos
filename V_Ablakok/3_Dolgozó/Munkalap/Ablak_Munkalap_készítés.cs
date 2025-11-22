using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_Adatszerkezet;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos
{
    public partial class Ablak_Munkalap_készítés
    {
        readonly Kezelő_Munka_Folyamat KézMunkaFoly = new Kezelő_Munka_Folyamat();
        readonly Kezelő_Kiegészítő_Csoportbeosztás KézCsoport = new Kezelő_Kiegészítő_Csoportbeosztás();
        readonly Kezelő_Dolgozó_Alap KézDolgozó = new Kezelő_Dolgozó_Alap();
        readonly Kezelő_MunkaRend KézMunkaRend = new Kezelő_MunkaRend();
        readonly Kezelő_Jármű_Állomány_Típus KézJárműTípus = new Kezelő_Jármű_Állomány_Típus();
        readonly Kezelő_Vezénylés KézVezénylés = new Kezelő_Vezénylés();
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly Kezelő_Munka_Szolgálat KézMunkaSzolg = new Kezelő_Munka_Szolgálat();
        readonly Kezelő_Jármű2ICS KézICS = new Kezelő_Jármű2ICS();
        readonly Kezelő_Jármű2 KézT5C5 = new Kezelő_Jármű2();

        List<Adat_Dolgozó_Alap> AdatokDolgozó = new List<Adat_Dolgozó_Alap>();
        List<string> Fájlok = new List<string>();

        #region Alap
        public Ablak_Munkalap_készítés()
        {
            InitializeComponent();
            Start();
        }

        private void Ablak_Munkalap_Load(object sender, EventArgs e)
        {

        }

        private void Start()
        {
            try
            {
                //Ha van 0-tól különböző akkor a régi jogosultságkiosztást használjuk
                //ha mind 0 akkor a GombLathatosagKezelo-t használjuk
                if (Program.PostásJogkör.Any(c => c != '0'))
                {
                    Telephelyekfeltöltése();
                    Jogosultságkiosztás();
                }
                else
                {
                    TelephelyekFeltöltéseÚj();
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
                }

                Dátum.Value = DateTime.Today;
                Feltöltiválasztékot();
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

        private void Feltöltiválasztékot()
        {
            Csoportfeltöltés();
            Irányítófeltöltés();
            Folyamatlistáz();
            Rendlistáz();
            Típusfeltöltés();
            V1feltöltés();
        }

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    Cmbtelephely.Items.Add(Elem);
                if (Program.PostásTelephely == "Főmérnökség")
                { Cmbtelephely.Text = Cmbtelephely.Items[0].ToStrTrim(); }
                else
                { Cmbtelephely.Text = Program.PostásTelephely; }

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

        private void Button13_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Munkalap.html";
                MyF.Megnyitás(hely);
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
                // ide kell az összes gombot tenni amit szabályozni akarunk

                melyikelem = 80;
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

        private void Dátum_ValueChanged(object sender, EventArgs e)
        {
            Feltöltiválasztékot();
        }
        #endregion


        #region Csoport
        private void Csoportfeltöltés()
        {
            try
            {
                Csoport.Items.Clear();
                List<Adat_Kiegészítő_Csoportbeosztás> Adatok = KézCsoport.Lista_Adatok(Cmbtelephely.Text.Trim());

                foreach (Adat_Kiegészítő_Csoportbeosztás rekord in Adatok)
                    Csoport.Items.Add(rekord.Csoportbeosztás);
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

        private void Csuk_Click(object sender, EventArgs e)
        {
            try
            {
                Csoport.Height = 25;
                Csuk.Visible = false;
                Nyit.Visible = true;
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

        private void Nyit_Click(object sender, EventArgs e)
        {
            try
            {
                Csoport.Height = 500;
                Csuk.Visible = true;
                Nyit.Visible = false;
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

        private void Csoportkijelölmind_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < Csoport.Items.Count; j++)
                Csoport.SetItemChecked(j, true);
            Jelöltcsoportfel();
        }

        private void Csoportvissza_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < Csoport.Items.Count; j++)
                Csoport.SetItemChecked(j, false);
            Jelöltcsoportfel();
        }

        private void Jelöltcsoport_Click(object sender, EventArgs e)
        {
            Jelöltcsoportfel();
        }

        private void Jelöltcsoportfel()
        {
            try
            {
                Csoport.Height = 25;
                Csuk.Visible = false;
                Nyit.Visible = true;

                AdatokDolgozó = KézDolgozó.Lista_Adatok(Cmbtelephely.Text.Trim());

                Dolgozónév.Rows.Clear();
                Dolgozónév.Columns.Clear();
                Dolgozónév.Refresh();
                Dolgozónév.Visible = false;
                Dolgozónév.ColumnCount = 2;

                // fejléc elkészítése
                Dolgozónév.Columns[0].HeaderText = "HR azonosító";
                Dolgozónév.Columns[0].Width = 100;  // 15-el kell osztani
                Dolgozónév.Columns[1].HeaderText = "Dolgozónév";
                Dolgozónév.Columns[1].Width = 230;

                List<Adat_Dolgozó_Alap> AdatokÖ = new List<Adat_Dolgozó_Alap>();
                for (int j = 0; j < Csoport.Items.Count; j++)
                {
                    if (Csoport.GetItemChecked(j))
                    {
                        List<Adat_Dolgozó_Alap> Adatok = new List<Adat_Dolgozó_Alap>();
                        // csoporttagokat kiválogatja
                        if (Csoport.Items[j].ToStrTrim() == "Összes")
                            Adatok = (from a in AdatokDolgozó
                                      where a.Kilépésiidő == new DateTime(1900, 1, 1)
                                      orderby a.DolgozóNév
                                      select a).ToList();
                        else
                            Adatok = (from a in AdatokDolgozó
                                      where a.Kilépésiidő == new DateTime(1900, 1, 1)
                                      && a.Csoport == Csoport.Items[j].ToStrTrim()
                                      orderby a.DolgozóNév
                                      select a).ToList();
                        AdatokÖ.AddRange(Adatok);

                    }
                }
                AdatokDolgozó = AdatokÖ;
                DolgozóListaFeltöltés(AdatokDolgozó);
                Dolgozónév.Visible = true;
                Dolgozónév.Refresh();
                Csoport.Height = 25;
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

        private void DolgozóListaFeltöltés(List<Adat_Dolgozó_Alap> Adatok)
        {

            try
            {
                Dolgozónév.Rows.Clear();
                foreach (Adat_Dolgozó_Alap rekord in Adatok)
                {
                    Dolgozónév.RowCount++;
                    int i = Dolgozónév.RowCount - 1;
                    Dolgozónév.Rows[i].Cells[0].Value = rekord.Dolgozószám;
                    Dolgozónév.Rows[i].Cells[1].Value = rekord.DolgozóNév;
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


        #region Dolgozó választás
        private void Összeskijelöl_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Dolgozónév.Rows.Count; i++)
                Dolgozónév.Rows[i].Selected = true;
        }

        private void Mindtöröl_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Dolgozónév.Rows.Count; i++)
                Dolgozónév.Rows[i].Selected = false;
        }

        private void Benn_Lévők_Click(object sender, EventArgs e)
        {
            try
            {
                Holtart.Be();
                // minden kijelölést töröl
                for (int i = 0; i < Dolgozónév.Rows.Count; i++)
                    Dolgozónév.Rows[i].Selected = false;


                List<Adat_Dolgozó_Alap> Adatok = KézDolgozó.MunkaVégzőLista(Cmbtelephely.Text.Trim(), Dátum.Value, AdatokDolgozó);
                DolgozóListaFeltöltés(Adatok);

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
        #endregion


        #region Feltöltések
        private void Irányítófeltöltés()
        {
            try
            {
                Kiadta.Items.Clear();
                Ellenőrizte.Items.Clear();
                List<Adat_Dolgozó_Alap> AdatokÖ = KézDolgozó.Lista_Adatok(Cmbtelephely.Text.Trim());
                List<Adat_Dolgozó_Alap> Adatok = (from a in AdatokÖ
                                                  where a.Kilépésiidő == new DateTime(1900, 1, 1)
                                                  && a.Főkönyvtitulus != ""
                                                  && a.Főkönyvtitulus != "_"
                                                  orderby a.DolgozóNév
                                                  select a).ToList();
                Kiadta.Items.Add("");
                Ellenőrizte.Items.Add("");
                foreach (Adat_Dolgozó_Alap elem in Adatok)
                {
                    Kiadta.Items.Add(elem.DolgozóNév);
                    Ellenőrizte.Items.Add(elem.DolgozóNév);
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

        private void Rendlistáz()
        {
            try
            {
                Munkarendlist.Items.Clear();
                List<Adat_MunkaRend> AdatokÖ = KézMunkaRend.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);
                List<Adat_MunkaRend> Adatok = (from a in AdatokÖ
                                               where a.Látszódik == true
                                               select a).ToList();
                foreach (Adat_MunkaRend elem in Adatok)
                    Munkarendlist.Items.Add(elem.Munkarend);
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

        private void Típusfeltöltés()
        {
            try
            {
                Típusoklistája.Items.Clear();

                List<Adat_Jármű_Állomány_Típus> Adatok = KézJárműTípus.Lista_Adatok(Cmbtelephely.Text.Trim());

                foreach (Adat_Jármű_Állomány_Típus Elem in Adatok)
                    Típusoklistája.Items.Add(Elem.Típus);

                Típusoklistája.Items.Add("Üres");
                Típusoklistája.EndUpdate();
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

        private void Command14_Click(object sender, EventArgs e)
        {
            Jelöltcsoportfel();
            Csoportfeltöltés();
            Folyamatlistáz();
            Rendlistáz();
            Típusfeltöltés();
            Mindenpsz.Checked = false;
            E2pályaszám.Checked = false;
            E3pályaszám.Checked = false;
        }

        private void Folyamatlistáz()
        {
            try
            {
                List<Adat_Munka_Folyamat> AdatokÖ = KézMunkaFoly.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);
                List<Adat_Munka_Folyamat> Adatok = (from a in AdatokÖ
                                                    where a.Látszódik == true
                                                    select a).ToList();

                MunkafolyamatTábla.Rows.Clear();
                MunkafolyamatTábla.Columns.Clear();
                MunkafolyamatTábla.Refresh();
                MunkafolyamatTábla.Visible = false;
                MunkafolyamatTábla.ColumnCount = 4;

                // fejléc elkészítése
                MunkafolyamatTábla.Columns[0].HeaderText = "Munkafolyamat";
                MunkafolyamatTábla.Columns[0].Width = 400;
                MunkafolyamatTábla.Columns[1].HeaderText = "Pályaszám";
                MunkafolyamatTábla.Columns[1].Width = 80;
                MunkafolyamatTábla.Columns[2].HeaderText = "Sorszám";
                MunkafolyamatTábla.Columns[2].Width = 80;
                MunkafolyamatTábla.Columns[3].HeaderText = "Rendelési szám";
                MunkafolyamatTábla.Columns[3].Width = 150;

                foreach (Adat_Munka_Folyamat rekord in Adatok)
                {
                    MunkafolyamatTábla.RowCount++;
                    int i = MunkafolyamatTábla.RowCount - 1;

                    MunkafolyamatTábla.Rows[i].Cells[0].Value = rekord.Munkafolyamat.Trim();
                    MunkafolyamatTábla.Rows[i].Cells[1].Value = rekord.Azonosító;
                    MunkafolyamatTábla.Rows[i].Cells[2].Value = rekord.ID;
                    MunkafolyamatTábla.Rows[i].Cells[3].Value = rekord.Rendelésiszám;
                }

                MunkafolyamatTábla.Visible = true;
                MunkafolyamatTábla.Refresh();

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

        private void V1feltöltés()
        {
            try
            {
                List<Adat_Vezénylés> AdatokÖ = KézVezénylés.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value);

                List<Adat_Vezénylés> Adatok = (from a in AdatokÖ
                                               where a.Törlés == 0
                                               && a.Vizsgálatraütemez == 1
                                               && a.Vizsgálat == "V1"
                                               && a.Dátum.ToShortDateString() == Dátum.Value.ToShortDateString()
                                               orderby a.Azonosító
                                               select a).ToList();

                V1Tábla.Rows.Clear();
                V1Tábla.Columns.Clear();
                V1Tábla.Refresh();
                V1Tábla.Visible = false;
                V1Tábla.ColumnCount = 2;

                // fejléc elkészítése
                V1Tábla.Columns[0].HeaderText = "rendelésiszám";
                V1Tábla.Columns[0].Width = 80;  // 15-el kell osztani
                V1Tábla.Columns[1].HeaderText = "azonosító";
                V1Tábla.Columns[1].Width = 80;  // 15-el kell osztani

                foreach (Adat_Vezénylés rekord in Adatok)
                {
                    V1Tábla.RowCount++;
                    int i = V1Tábla.RowCount - 1;

                    V1Tábla.Rows[i].Cells[0].Value = rekord.Rendelésiszám;
                    V1Tábla.Rows[i].Cells[1].Value = rekord.Azonosító;
                }

                V1Tábla.Refresh();
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


        #region Excel


        private void Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Dolgozónév.Rows.Count == 0) throw new HibásBevittAdat("Nincs kiválasztva dolgozó");
                if (Dolgozónév.SelectedRows.Count == 0) throw new HibásBevittAdat("Nincs kiválasztva dolgozó");
                Fájlok.Clear();
                if (Option6.Checked)
                    ExcelKészítés_Egyéni();
                else
                    ExcelKészítés_Csoportos();
                if (Option9.Checked && Fájlok.Count > 0) MyF.ExcelNyomtatás(Fájlok, Option10.Checked);

                MessageBox.Show("A Munkalapok generálása befejeződött.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private readonly string munkalap = "Munka1";
        int sor;
        int oszlop;
        int blokkeleje;
        string HR;
        string Dolgozó;
        int maximum = 0;

        private void ExcelKészítés_Egyéni()
        {
            try
            {
                maximum = Típusoklistája.SelectedItems.Count;

                for (int hanyadikember = 0; hanyadikember < Dolgozónév.Rows.Count; hanyadikember++)
                {
                    if (Dolgozónév.Rows[hanyadikember].Selected)
                    {

                        Dolgozó = Dolgozónév.Rows[hanyadikember].Cells[1].Value.ToStrTrim();
                        HR = Dolgozónév.Rows[hanyadikember].Cells[0].Value.ToStrTrim();
                        string szöveg1 = DateTime.Now.ToString("yyMMddHHmmss");
                        string fájlexc = $"Munkalap_{DateTime.Now:yyMMddHHmmss}_{HR}.xlsx";
                        string könyvtár = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + $@"\{fájlexc}";

                        sor = 5;
                        blokkeleje = 5;

                        Holtart.Be(10);
                        MyX.ExcelLétrehozás(munkalap);
                        Beállítás_Betű beállBetű = new Beállítás_Betű();
                        MyX.Munkalap_betű(munkalap, beállBetű);
                        MyX.Oszlopszélesség(munkalap, "a:a", 18);
                        MyX.Oszlopszélesség(munkalap, "b:r", 6);

                        Munkalap_Fejléc(szöveg1);
                        Dolgozó_Fejléc();
                        Dolgozó_Neve_Egy();
                        Munkalap_munkaFejléc();
                        Munkalap_MunkaFolyamatok();
                        Munkalap_ÜresSorok();
                        Munkalap_Összesítő();
                        Munkalap_Pályaszám_fejléc();
                        Munkalap_Pályaszám_Minden();
                        Munkalap_Pályaszám_E1();
                        Munkalap_Pályaszám_E2();
                        Munkalap_Pályaszám_E3();
                        Munkalap_Pályaszám_E2_ICS();
                        Munkalap_Pályaszám_E3_ICS();
                        Munkalap_Aláíró();
                        Munkalap_NyomtatásBeállítás();

                        // **********************************************
                        // **Nyomtatás                                 **
                        // **********************************************
                        if (Option9.Checked) Fájlok.Add(könyvtár);

                        Holtart.Ki();

                        MyX.ExcelMentés(könyvtár);

                        MyX.ExcelBezárás();


                        Dolgozónév.Rows[hanyadikember].Selected = false;
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

        private void ExcelKészítés_Csoportos()
        {
            try
            {
                maximum = Típusoklistája.SelectedItems.Count;

                string szöveg1 = DateTime.Now.ToString("yyMMddHHmmss");
                string fájlexc = $"Munkalap_{DateTime.Now:yyMMddHHmmss}.xlsx";
                string könyvtár = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + $@"\{fájlexc}";

                sor = 5;
                blokkeleje = 5;

                Holtart.Be(10);
                MyX.ExcelLétrehozás(munkalap);
                Beállítás_Betű beállBetű = new Beállítás_Betű();
                MyX.Munkalap_betű(munkalap, beállBetű);
                MyX.Oszlopszélesség(munkalap, "a:a", 18);
                MyX.Oszlopszélesség(munkalap, "b:r", 6);

                Munkalap_Fejléc(szöveg1);
                Dolgozó_Fejléc();
                Dolgozó_Neve_Csoportos();
                Munkalap_munkaFejléc();
                Munkalap_MunkaFolyamatok();
                Munkalap_ÜresSorok();
                Munkalap_Összesítő();
                Munkalap_Pályaszám_fejléc();
                Munkalap_Pályaszám_Minden();
                Munkalap_Pályaszám_E1();
                Munkalap_Pályaszám_E2();
                Munkalap_Pályaszám_E3();
                Munkalap_Pályaszám_E2_ICS();
                Munkalap_Pályaszám_E3_ICS();
                Munkalap_Aláíró();
                Munkalap_NyomtatásBeállítás();

                // **********************************************
                // **Nyomtatás                                 **
                // **********************************************
                if (Option9.Checked) Fájlok.Add(könyvtár);

                Holtart.Ki();

                MyX.ExcelMentés(könyvtár);
                MyX.ExcelBezárás();

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

        private void Munkalap_NyomtatásBeállítás()
        {

            // **********************************************
            // **Nyomtatási beállítások                    **
            // **********************************************
            Beállítás_Nyomtatás beállNyom = new Beállítás_Nyomtatás
            {
                Munkalap = munkalap,
                NyomtatásiTerület = $"a1:r{sor}",
                BalMargó = 10,
                JobbMargó = 10,
                AlsóMargó = 15,
                FelsőMargó = 15,
                FejlécMéret = 13,
                LáblécMéret = 13,
                LapSzéles = 1,
                LapMagas = 1
            };
            MyX.NyomtatásiTerület_részletes(munkalap, beállNyom);
        }

        private void Munkalap_Aláíró()
        {
            // **********************************************
            // **Aláíró sor                            ******
            // **********************************************
            sor += 1;
            MyX.Sormagasság(munkalap, $"{sor}:{sor}", 35);
            MyX.Egyesít(munkalap, $"a{sor}:b{sor + 1}");
            MyX.Kiir("A munkát kiadta:", $"a{sor}");
            MyX.Egyesít(munkalap, $"c{sor}:f{sor}");
            MyX.Egyesít(munkalap, $"g{sor}:i{sor + 1}");
            MyX.Kiir("A kiadott munkát\nelvégezte:", $"g{sor}");
            MyX.Egyesít(munkalap, $"j{sor}:l{sor + 1}");
            MyX.Egyesít(munkalap, $"m{sor}:o{sor + 1}");
            MyX.Sortörésseltöbbsorba(munkalap, $"M{sor}:O{sor + 1}", true);
            MyX.Sortörésseltöbbsorba(munkalap, $"G{sor}:I{sor + 1}", true);

            MyX.Igazít_vízszintes(munkalap, $"M{sor}:O{sor + 1}", "közép");
            MyX.Igazít_vízszintes(munkalap, $"G{sor}:I{sor + 1}", "közép");       //
            MyX.Kiir("A kiadott munkát\n ellenőrizte:", $"m{sor}");     //
            MyX.Egyesít(munkalap, $"p{sor}:r{sor}");                    //
            Beállítás_Betű beállBetű = new Beállítás_Betű { Méret = 10, Dőlt = true, Vastag = true };
            MyX.Betű(munkalap, $"a{sor}:r{sor}", beállBetű);                             //
            MyX.Rácsoz(munkalap, $"A{sor}:R{sor + 1}");                           //


            sor += 1;
            MyX.Egyesít(munkalap, $"c{sor}:f{sor}");                    //
            MyX.Kiir(Kiadta.Text.Trim(), $"c{sor}");
            beállBetű = new Beállítás_Betű { Méret = 10, Dőlt = true, Vastag = true };
            MyX.Betű(munkalap, $"c{sor}", beállBetű);
            MyX.Egyesít(munkalap, $"p{sor}:r{sor}");
            MyX.Kiir(Ellenőrizte.Text.Trim(), $"p{sor}");
            beállBetű = new Beállítás_Betű { Méret = 10, Dőlt = true, Vastag = true };
            MyX.Betű(munkalap, $"P{sor}", beállBetű);
            MyX.Igazít_függőleges(munkalap, $"A{sor}", "alsó");
            MyX.Igazít_függőleges(munkalap, $"G{sor}", "alsó");
            MyX.Igazít_függőleges(munkalap, $"M{sor}", "alsó");

            MyX.Vastagkeret(munkalap, $"A{sor - 1}:R{sor}");
            Holtart.Lép();
        }

        private void Munkalap_Pályaszám_E2_ICS()
        {
            // ////////////////////////////////////////
            // ///  E2     PÁLYASZÁM    ICS        ////
            // ////////////////////////////////////////
            int mennyi = 0;
            switch ((int)Dátum.Value.DayOfWeek)
            {
                case 1:
                    {
                        mennyi = 1;
                        break;
                    }
                case 2:
                    {
                        mennyi = 2;
                        break;
                    }
                case 3:
                    {
                        mennyi = 3;
                        break;
                    }
                case 4:
                    {
                        mennyi = 4;
                        break;
                    }
                case 5:
                    {
                        mennyi = 5;
                        break;
                    }
                case 6:
                    {
                        mennyi = 6;
                        break;
                    }
                case 7:
                    {
                        mennyi = 7;
                        break;
                    }
            }
            if (E2ICS.Checked && maximum >= 1 && mennyi > 0)
            {
                for (int i = 0; i < Típusoklistája.Items.Count; i++)
                {
                    if (Típusoklistája.GetItemChecked(i))
                    {
                        List<Adat_Jármű_2ICS> AdatokICS = KézICS.Lista_Adatok(Cmbtelephely.Text.Trim());
                        List<string> AdatokRész = (from a in AdatokICS
                                                   where a.E2 == mennyi
                                                   orderby a.Azonosító
                                                   select a.Azonosító).ToList();

                        List<Adat_Jármű> AdatokÖsszes = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                        List<string> AdatokÖssz = (from a in AdatokÖsszes
                                                   where a.Típus == Típusoklistája.Items[i].ToStrTrim()
                                                   orderby a.Azonosító
                                                   select a.Azonosító).ToList();
                        if (AdatokRész != null)
                        {
                            sor += 1;
                            blokkeleje = sor;
                            oszlop = 1;
                            foreach (string PályaszámLista in AdatokRész)
                            {
                                if (AdatokÖssz.Contains(PályaszámLista.Trim()))
                                    if (oszlop == 18)
                                    {
                                        oszlop = 1;
                                        sor += 1;
                                    }
                                oszlop += 1;
                                MyX.Kiir(PályaszámLista.Trim(), MyF.Oszlopnév(oszlop) + sor.ToString());
                            }
                        }

                        MyX.Egyesít(munkalap, $"a{blokkeleje}:a{sor}");
                        if (Típusoklistája.Items[i].ToStrTrim() != "Üres")
                        {
                            MyX.Kiir("E2-" + Típusoklistája.Items[i].ToStrTrim(), $"A{blokkeleje}");
                        }

                        MyX.Rácsoz(munkalap, $"b{blokkeleje}:r{sor}");
                        MyX.Vastagkeret(munkalap, $"b{blokkeleje}:r{sor}");
                        MyX.Vastagkeret(munkalap, $"a{blokkeleje}:r{sor}");
                        MyX.Sormagasság(munkalap, $"{blokkeleje}:{sor}", 25);
                        Beállítás_Betű beállBetű = new Beállítás_Betű { Vastag = true };
                        MyX.Betű(munkalap, $"A{blokkeleje}:R{sor}", beállBetű);
                    }
                }
            }
        }

        private void Munkalap_Pályaszám_E3_ICS()
        {

            // ////////////////////////////////////////
            // ///  E3     PÁLYASZÁM    ICS        ////
            // ////////////////////////////////////////
            int mennyi = 0;
            switch ((int)Dátum.Value.DayOfWeek)
            {
                case 1:
                    {
                        mennyi = 1;
                        break;
                    }
                case 2:
                    {
                        mennyi = 2;
                        break;
                    }
                case 3:
                    {
                        mennyi = 3;
                        break;
                    }
                case 4:
                    {
                        mennyi = 4;
                        break;
                    }
                case 5:
                    {
                        mennyi = 5;
                        break;
                    }
                case 6:
                    {
                        mennyi = 6;
                        break;
                    }
                case 7:
                    {
                        mennyi = 7;
                        break;
                    }
            }

            if (E3ICS.Checked && maximum >= 1)
            {
                for (int i = 0; i < Típusoklistája.Items.Count; i++)
                {
                    if (Típusoklistája.GetItemChecked(i))
                    {
                        List<Adat_Jármű_2ICS> AdatokICS = KézICS.Lista_Adatok(Cmbtelephely.Text.Trim());
                        List<string> AdatokRész = (from a in AdatokICS
                                                   where a.E3 == mennyi
                                                   orderby a.Azonosító
                                                   select a.Azonosító).ToList();

                        List<Adat_Jármű> AdatokÖsszes = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                        List<string> AdatokÖssz = (from a in AdatokÖsszes
                                                   where a.Típus == Típusoklistája.Items[i].ToStrTrim()
                                                   orderby a.Azonosító
                                                   select a.Azonosító).ToList();

                        if (AdatokRész != null)
                        {
                            sor += 1;
                            blokkeleje = sor;
                            oszlop = 1;
                            foreach (string PályaszámLista in AdatokRész)
                            {
                                if (AdatokÖssz.Contains(PályaszámLista.Trim()))
                                {
                                    if (oszlop == 18)
                                    {
                                        oszlop = 1;
                                        sor += 1;
                                    }
                                    oszlop += 1;
                                    MyX.Kiir(PályaszámLista.Trim(), MyF.Oszlopnév(oszlop) + sor.ToString());
                                }
                            }

                            MyX.Egyesít(munkalap, $"a{blokkeleje}:a{sor}");

                            if (Típusoklistája.Items[i].ToStrTrim() != "Üres")
                            {
                                MyX.Kiir("E3-  " + Típusoklistája.Items[i].ToString(), $"A{blokkeleje}");
                            }

                            MyX.Rácsoz(munkalap, $"b{blokkeleje}:r{sor}");
                            MyX.Vastagkeret(munkalap, $"b{blokkeleje}:r{sor}");
                            MyX.Vastagkeret(munkalap, $"a{blokkeleje}:r{sor}");
                            MyX.Sormagasság(munkalap, $"{blokkeleje}:{sor}", 25);
                            Beállítás_Betű beállBetű = new Beállítás_Betű { Vastag = true };
                            MyX.Betű(munkalap, $"A{blokkeleje}:R{sor}", beállBetű);
                        }
                    }
                }
            }
        }

        private void Munkalap_Pályaszám_E3()
        {

            // ////////////////////////////////////////
            // ///  E3     PÁLYASZÁM     T5C5      ////
            // ////////////////////////////////////////
            if (E3pályaszám.Checked && maximum >= 1)
            {
                for (int i = 0; i < Típusoklistája.Items.Count; i++)
                {
                    if (Típusoklistája.GetItemChecked(i) == true)
                    {
                        List<Adat_Vezénylés> AdatokVezénylés = KézVezénylés.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value);
                        AdatokVezénylés = (from a in AdatokVezénylés
                                           where a.Törlés == 0
                                           && a.Vizsgálatraütemez == 1
                                           && a.Vizsgálat == "E3"
                                           && a.Dátum.ToShortDateString() == Dátum.Value.ToShortDateString()
                                           orderby a.Azonosító
                                           select a).ToList();

                        List<Adat_Jármű> AdatokÖsszes = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                        List<string> AdatokÖssz = (from a in AdatokÖsszes
                                                   where a.Típus == Típusoklistája.Items[i].ToStrTrim()
                                                   orderby a.Azonosító
                                                   select a.Azonosító).ToList();

                        if (AdatokVezénylés != null)
                        {
                            sor += 1;
                            blokkeleje = sor;
                            oszlop = 1;
                            foreach (Adat_Vezénylés PályaszámLista in AdatokVezénylés)
                            {
                                if (AdatokÖssz.Contains(PályaszámLista.Azonosító.Trim()))
                                {
                                    if (oszlop == 18)
                                    {
                                        oszlop = 1;
                                        sor += 1;
                                    }
                                    oszlop += 1;
                                    MyX.Kiir(PályaszámLista.Azonosító.Trim(), MyF.Oszlopnév(oszlop) + sor.ToString());
                                }
                            }
                            MyX.Egyesít(munkalap, $"a{blokkeleje}:a{sor}");
                            if (Típusoklistája.Items[i].ToStrTrim() != "Üres")
                            {
                                MyX.Kiir("E3-" + Típusoklistája.Items[i].ToString(), $"A{blokkeleje}");
                            }
                            MyX.Rácsoz(munkalap, $"b{blokkeleje}:r{sor}");
                            MyX.Vastagkeret(munkalap, $"b{blokkeleje}:r{sor}");
                            MyX.Vastagkeret(munkalap, $"a{blokkeleje}:r{sor}");
                            MyX.Sormagasság(munkalap, $"{blokkeleje}:{sor}", 25);
                            Beállítás_Betű beállBetű = new Beállítás_Betű { Vastag = true };
                            MyX.Betű(munkalap, $"A{blokkeleje}:R{sor}", beállBetű);
                        }
                    }
                }
            }
        }

        private void Munkalap_Pályaszám_E2()
        {
            //// ////////////////////////////////////////
            //// ///  E2     PÁLYASZÁM    T5C5       ////
            //// ////////////////////////////////////////
            int mennyi = 0;
            switch ((int)Dátum.Value.DayOfWeek)
            {
                case 1:
                    {
                        mennyi = 1;
                        break;
                    }
                case 2:
                    {
                        mennyi = 2;
                        break;
                    }
                case 3:
                    {
                        mennyi = 3;
                        break;
                    }
                case 4:
                    {
                        mennyi = 1;
                        break;
                    }
                case 5:
                    {
                        mennyi = 2;
                        break;
                    }
                case 6:
                    {
                        mennyi = 3;
                        break;
                    }
                case 7:
                    {
                        mennyi = 0;
                        break;
                    }
            }
            if (E2pályaszám.Checked && maximum >= 1 && mennyi > 0)
            {
                string hely3 = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos2.mdb";

                for (int i = 0; i < Típusoklistája.Items.Count; i++)
                {
                    if (Típusoklistája.GetItemChecked(i))
                    {
                        List<Adat_Jármű_2> AdatokÖ = KézT5C5.Lista_Adatok(Cmbtelephely.Text.Trim());
                        List<string> AdatokRész = (from a in AdatokÖ
                                                   where a.Haromnapos == mennyi
                                                   orderby a.Azonosító
                                                   select a.Azonosító).ToList();

                        List<Adat_Jármű> AdatokÖsszes = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                        List<string> AdatokÖssz = (from a in AdatokÖsszes
                                                   where a.Típus == Típusoklistája.Items[i].ToStrTrim()
                                                   orderby a.Azonosító
                                                   select a.Azonosító).ToList();
                        if (AdatokRész != null)
                        {
                            sor += 1;
                            blokkeleje = sor;
                            oszlop = 1;
                            foreach (string PályaszámLista in AdatokRész)
                            {
                                if (AdatokÖssz.Contains(PályaszámLista))
                                {
                                    if (oszlop == 18)
                                    {
                                        oszlop = 1;
                                        sor += 1;
                                    }
                                    oszlop += 1;
                                    MyX.Kiir(PályaszámLista, MyF.Oszlopnév(oszlop) + sor.ToString());
                                }
                            }

                            MyX.Egyesít(munkalap, $"a{blokkeleje}:a{sor}");
                            if (Típusoklistája.Items[i].ToStrTrim() != "Üres")
                            {
                                MyX.Kiir("E2- \n" + Típusoklistája.Items[i].ToString(), $"A{blokkeleje}");
                            }

                            MyX.Rácsoz(munkalap, $"b{blokkeleje}:r{sor}");
                            MyX.Vastagkeret(munkalap, $"b{blokkeleje}:r{sor}");
                            MyX.Vastagkeret(munkalap, $"a{blokkeleje}:r{sor}");
                            MyX.Sormagasság(munkalap, $"{blokkeleje}:{sor}", 25);
                            Beállítás_Betű beállBetű = new Beállítás_Betű { Vastag = true };
                            MyX.Betű(munkalap, $"A{blokkeleje}:R{sor}", beállBetű);
                        }
                    }
                }
            }

        }

        private void Munkalap_Pályaszám_E1()
        {

            // ////////////////////////////////////////
            // ///  E1 PÁLYASZÁM                   ////
            // ////////////////////////////////////////
            // megnézzük, hogy hány típus van kijelölve
            maximum = Típusoklistája.SelectedItems.Count;
            // minden pályaszám
            if (E1_pályaszámok.Checked && maximum >= 1)
            {
                blokkeleje = sor;
                for (int i = 0; i < Típusoklistája.Items.Count; i++)
                {
                    if (Típusoklistája.GetItemChecked(i))
                    {
                        List<Adat_Jármű> AdatokÖ = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                        List<string> Adatok = (from a in AdatokÖ
                                               where a.Típus == Típusoklistája.Items[i].ToStrTrim()
                                               orderby a.Azonosító
                                               select a.Azonosító).ToList();
                        if (Adatok != null)
                        {
                            sor += 1;
                            blokkeleje = sor;
                            oszlop = 1;
                            foreach (string PályaszámLista in Adatok)
                            {
                                if (oszlop == 18)
                                {
                                    oszlop = 1;
                                    sor += 1;
                                }
                                oszlop += 1;
                                MyX.Kiir(PályaszámLista, MyF.Oszlopnév(oszlop) + sor.ToString());
                            }

                            MyX.Egyesít(munkalap, $"a{blokkeleje}" + $":a{sor}");
                            if (Típusoklistája.Items[i].ToStrTrim() != "Üres")
                            {
                                MyX.Kiir("E1- \n" + Típusoklistája.Items[i].ToStrTrim(), $"a{blokkeleje}");
                            }
                            MyX.Rácsoz(munkalap, $"b{blokkeleje}:r{sor}");
                            MyX.Vastagkeret(munkalap, $"b{blokkeleje}:r{sor}");
                            MyX.Vastagkeret(munkalap, $"a{blokkeleje}:r{sor}");
                            MyX.Sormagasság(munkalap, $"{blokkeleje}:{sor}", 25);
                            Beállítás_Betű beállBetű = new Beállítás_Betű { Vastag = true };
                            MyX.Betű(munkalap, $"A{blokkeleje}:R{sor}", beállBetű);
                        }
                    }
                }
            }
        }

        private void Munkalap_Pályaszám_Minden()
        {
            ////////////////////////////////////////
            ///  MINDEN PÁLYASZÁM               ////
            ////////////////////////////////////////
            if (Mindenpsz.Checked && maximum >= 1)
            {
                blokkeleje = sor;
                for (int i = 0; i < Típusoklistája.Items.Count; i++)
                {
                    if (Típusoklistája.GetItemChecked(i))
                    {
                        List<Adat_Jármű> AdatokÖ = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                        List<string> Adatok = (from a in AdatokÖ
                                               where a.Típus == Típusoklistája.Items[i].ToStrTrim()
                                               orderby a.Azonosító
                                               select a.Azonosító).ToList();

                        if (Adatok != null)
                        {
                            sor += 1;
                            blokkeleje = sor;
                            oszlop = 1;
                            foreach (string PályaszámLista in Adatok)
                            {
                                if (oszlop == 18)
                                {
                                    oszlop = 1;
                                    sor += 1;
                                }
                                oszlop += 1;
                                MyX.Kiir(PályaszámLista, MyF.Oszlopnév(oszlop) + sor.ToString());

                            }

                            MyX.Egyesít(munkalap, $"a{blokkeleje}:a{sor}");
                            if (Típusoklistája.Items[i].ToStrTrim() != "Üres")
                            {
                                MyX.Kiir(Típusoklistája.Items[i].ToStrTrim(), $"a{blokkeleje}");
                            }
                            MyX.Rácsoz(munkalap, $"b{blokkeleje}:r{sor}");
                            MyX.Vastagkeret(munkalap, $"b{blokkeleje}:r{sor}");
                            MyX.Vastagkeret(munkalap, $"a{blokkeleje}:r{sor}");
                            MyX.Sormagasság(munkalap, $"{blokkeleje}:{sor}", 25);
                            Beállítás_Betű beállBetű = new Beállítás_Betű { Vastag = true };
                            MyX.Betű(munkalap, $"A{blokkeleje}:R{sor}", beállBetű);
                        }
                    }
                }
            }
        }

        private void Munkalap_Pályaszám_fejléc()
        {
            // **********************************************
            // **Pályaszám típus sor                      ***
            // **********************************************
            sor += 1;
            MyX.Egyesít(munkalap, $"a{sor}:a{sor}");
            MyX.Kiir("Típus:", $"a{sor}");
            MyX.Egyesít(munkalap, $"b{sor}:r{sor}");
            MyX.Kiir("Pályaszámok:", $"b{sor}");
            Beállítás_Betű beállBetű = new Beállítás_Betű { Méret = 10, Dőlt = true, Vastag = true };
            MyX.Betű(munkalap, $"a{sor}:r{sor}", beállBetű);
            MyX.Rácsoz(munkalap, $"a{sor}:r{sor}");
            MyX.Vastagkeret(munkalap, $"a{sor}:r{sor}");
            Holtart.Lép();
        }

        private void Munkalap_Összesítő()
        {

            // **********************************************
            // ** öszesítős sor                           ***
            // **********************************************
            sor += 1;
            MyX.Egyesít(munkalap, $"a{sor}:p{sor}");
            MyX.Egyesít(munkalap, $"q{sor}:r{sor}");
            MyX.Kiir("Összesen:", $"a{sor}");
            MyX.Sormagasság(munkalap, $"{sor}:{sor}", 25);
            MyX.Igazít_vízszintes(munkalap, $"A{sor}", "jobb");
            MyX.Igazít_függőleges(munkalap, $"A{sor}", "alsó");
            Beállítás_Betű beállBetű = new Beállítás_Betű { Méret = 10, Dőlt = true, Vastag = true };
            MyX.Betű(munkalap, $"a{sor}:r{sor}", beállBetű);
            MyX.Rácsoz(munkalap, $"a{sor}:r{sor}");
            MyX.Vastagkeret(munkalap, $"a{sor}:r{sor}");
        }

        private void Munkalap_ÜresSorok()
        {
            // **********************************************
            // ** Üres sorok                              ***
            // **********************************************
            blokkeleje = sor + 1;

            if (Üressor.Checked)
            {
                if (int.TryParse(Üressorszám.Text.Trim(), out int üressor))
                {

                    for (int i = 1; i <= üressor; i++)
                    {
                        sor += 1;
                        MyX.Egyesít(munkalap, $"a{sor}:a{sor}");
                        MyX.Egyesít(munkalap, $"b{sor}:c{sor}");
                        MyX.Egyesít(munkalap, $"d{sor}:e{sor}");
                        MyX.Egyesít(munkalap, $"f{sor}:p{sor}");
                        MyX.Egyesít(munkalap, $"q{sor}:r{sor}");
                    }
                    Beállítás_Betű beállBetű = new Beállítás_Betű { Méret = 16 };
                    MyX.Betű(munkalap, $"a{blokkeleje}:r{sor}", beállBetű);
                    beállBetű = new Beállítás_Betű { Méret = 11 };
                    MyX.Betű(munkalap, $"f{blokkeleje}:p{sor}", beállBetű);
                    MyX.Sormagasság(munkalap, $"a{blokkeleje}:r{sor}", 24);
                    MyX.Rácsoz(munkalap, $"a{blokkeleje}:r{sor}");
                    MyX.Vastagkeret(munkalap, $"a{blokkeleje}:r{sor}");
                }
            }
            Holtart.Lép();
        }

        private void Munkalap_MunkaFolyamatok()
        {
            if (MunkafolyamatTábla.SelectedRows.Count != 0)
            {
                // **********************************************
                // ** munkafolyamatok            ****************
                // **********************************************

                for (int i = 0; i < MunkafolyamatTábla.Rows.Count; i++)
                {
                    if (MunkafolyamatTábla.Rows[i].Selected == true)
                    {
                        // ha ki van jelölve
                        if (MunkafolyamatTábla.Rows[i].Cells[3].Value.ToString().Contains("V1"))
                        {
                            for (int k = 0; k < V1Tábla.Rows.Count; k++)
                            {
                                sor += 1;
                                MyX.Sormagasság(munkalap, $"a{sor}:r{sor}", 24);
                                MyX.Egyesít(munkalap, $"a{sor}:a{sor}");
                                MyX.Egyesít(munkalap, $"b{sor}:c{sor}");
                                MyX.Egyesít(munkalap, $"d{sor}:e{sor}");
                                MyX.Egyesít(munkalap, $"f{sor}:p{sor}");
                                MyX.Egyesít(munkalap, $"q{sor}:r{sor}");
                                MyX.Kiir(V1Tábla.Rows[k].Cells[0].Value.ToString(), $"a{sor}");
                                MyX.Kiir(V1Tábla.Rows[k].Cells[1].Value.ToString(), $"b{sor}");
                                MyX.Kiir(MunkafolyamatTábla.Rows[i].Cells[0].Value.ToString(), $"f{sor}");
                                if (MunkafolyamatTábla.Rows[i].Cells[0].Value.ToString().Length > 55)
                                {
                                    // ha hosszabb a szöveg 55-nél akkor több sorba írja
                                    MyX.Sortörésseltöbbsorba(munkalap, $"H{sor}");
                                    int sor_magasság = ((MunkafolyamatTábla.Rows[i].Cells[0].Value.ToString().Length / 55) + 1) * 20;
                                    MyX.Sormagasság(munkalap, $"{sor}:{sor}", sor_magasság);
                                }
                                else
                                {
                                    // Ha rövidebb
                                    MyX.Sormagasság(munkalap, $"{sor}:{sor}", 24);
                                }
                            }
                        }

                        else
                        {
                            sor += 1;
                            MyX.Sormagasság(munkalap, $"a{sor}:r{sor}", 24);
                            MyX.Egyesít(munkalap, $"a{sor}:a{sor}");
                            MyX.Egyesít(munkalap, $"b{sor}:c{sor}");
                            MyX.Egyesít(munkalap, $"d{sor}:e{sor}");
                            MyX.Egyesít(munkalap, $"f{sor}:p{sor}");
                            MyX.Egyesít(munkalap, $"q{sor}:r{sor}");
                            MyX.Kiir(MunkafolyamatTábla.Rows[i].Cells[3].Value.ToString(), $"a{sor}");
                            MyX.Kiir(MunkafolyamatTábla.Rows[i].Cells[1].Value.ToString(), $"b{sor}");
                            MyX.Kiir(MunkafolyamatTábla.Rows[i].Cells[0].Value.ToString(), $"f{sor}");
                            if (MunkafolyamatTábla.Rows[i].Cells[0].Value.ToString().Length > 55)
                            {
                                // ha hosszabb a szöveg 55-nél akkor több sorba írja
                                MyX.Sortörésseltöbbsorba(munkalap, $"H{sor}");
                                int sor_magasság = ((MunkafolyamatTábla.Rows[i].Cells[0].Value.ToString().Length / 55) + 1) * 20;
                                MyX.Sormagasság(munkalap, $"{sor}:{sor}", sor_magasság);
                            }
                            else
                            {
                                MyX.Sormagasság(munkalap, $"{sor}:{sor}", 24);
                            }
                        }
                    }
                }
                Beállítás_Betű beállBetű = new Beállítás_Betű { Méret = 16, Vastag = true };
                MyX.Betű(munkalap, $"a{blokkeleje}:r{sor}", beállBetű);
                MyX.Igazít_függőleges(munkalap, $"A{blokkeleje}:R{sor}", "alsó");
                beállBetű = new Beállítás_Betű { Méret = 11, Vastag = true };
                MyX.Betű(munkalap, $"f{blokkeleje}:p{sor}", beállBetű);
                MyX.Igazít_függőleges(munkalap, $"f{blokkeleje}:P{sor}", "alsó");
                MyX.Rácsoz(munkalap, $"a{blokkeleje}:r{sor}");
                MyX.Vastagkeret(munkalap, $"a{blokkeleje}:r{sor}");
                Holtart.Lép();
            }
        }

        private void Munkalap_munkaFejléc()
        {
            // **********************************************
            // ** munkafejléc                              **
            // **********************************************
            MyX.Egyesít(munkalap, $"a{sor}:a{sor}");
            MyX.Kiir("Rendelési szám:", $"a{sor}");
            MyX.Egyesít(munkalap, $"b{sor}:c{sor}");
            MyX.Kiir("Pályaszám:", $"b{sor}");
            MyX.Egyesít(munkalap, $"d{sor}:e{sor}");
            MyX.Kiir("Darab:", $"d{sor}");
            MyX.Egyesít(munkalap, $"f{sor}:p{sor}");
            MyX.Kiir("Munkafolyamat megnevezése:", $"f{sor}");
            MyX.Egyesít(munkalap, $"q{sor}:r{sor}");
            MyX.Kiir("Időráfordítás [perc]:", $"q{sor}");
            Beállítás_Betű beállBetű = new Beállítás_Betű { Méret = 10, Dőlt = true, Vastag = true };
            MyX.Betű(munkalap, $"a{sor}:r{sor}", beállBetű);
            MyX.Igazít_függőleges(munkalap, $"a{sor}:r{sor}", "alsó");

            MyX.Sortörésseltöbbsorba(munkalap, $"Q{sor}:R{sor}");
            MyX.Igazít_függőleges(munkalap, $"Q{sor}:R{sor}", "alsó");
            MyX.Igazít_vízszintes(munkalap, $"Q{sor}:R{sor}", "közép");


            MyX.Rácsoz(munkalap, $"a{sor}:r{sor}");
            MyX.Vastagkeret(munkalap, $"a{sor}:r{sor}");
            MyX.Sormagasság(munkalap, $"{sor}:{sor}", 35);
            blokkeleje = sor + 1;
        }

        private void Dolgozó_Neve_Csoportos()
        {
            // **********************************************
            // ** dolgozók neve                          ****
            // **********************************************

            for (int m = 0; m < Dolgozónév.Rows.Count; m++)
            {
                if (Dolgozónév.Rows[m].Selected)
                {
                    Dolgozónév.Rows[m].Selected = false;
                    string dolgozószám = Dolgozónév.Rows[m].Cells[0].Value.ToStrTrim();
                    string dolgozóneve = Dolgozónév.Rows[m].Cells[1].Value.ToStrTrim();
                    MyX.Egyesít(munkalap, $"a{sor}:c{sor}");
                    MyX.Kiir(dolgozószám, $"a{sor}");
                    MyX.Egyesít(munkalap, $"d{sor}:n{sor}");
                    MyX.Kiir(dolgozóneve, $"d{sor}");
                    MyX.Egyesít(munkalap, $"o{sor}:r{sor}");
                    sor += 1;
                }
            }
            Beállítás_Betű beállBetű = new Beállítás_Betű { Méret = 20, Vastag = true };
            MyX.Betű(munkalap, $"a{blokkeleje}:r{sor}", beállBetű);
            MyX.Sormagasság(munkalap, $"{blokkeleje}:{sor}", 35);
            MyX.Rácsoz(munkalap, $"a{blokkeleje}:r{sor}");
            MyX.Vastagkeret(munkalap, $"a{blokkeleje}:r{sor}");
        }

        private void Dolgozó_Neve_Egy()
        {
            // **********************************************
            // ** dolgozók neve                          ****
            // **********************************************

            MyX.Egyesít(munkalap, $"a{sor}:c{sor}");
            MyX.Kiir(HR, $"a{sor}");
            MyX.Egyesít(munkalap, $"d{sor}:n{sor}");
            MyX.Kiir(Dolgozó, $"d{sor}");
            MyX.Egyesít(munkalap, $"o{sor}:r{sor}");
            sor += 1;

            Beállítás_Betű beállBetű = new Beállítás_Betű { Méret = 20, Vastag = true };
            MyX.Betű(munkalap, $"a{blokkeleje}:r{sor}", beállBetű);
            MyX.Igazít_függőleges(munkalap, $"a{blokkeleje}:r{sor}", "alsó");
            MyX.Sormagasság(munkalap, $"{blokkeleje}:{sor}", 35);
            MyX.Rácsoz(munkalap, $"a{blokkeleje}:r{sor}");
            MyX.Vastagkeret(munkalap, $"a{blokkeleje}:r{sor}");
        }

        private void Dolgozó_Fejléc()
        {
            // **********************************************
            // ** dolgozó fejléc                          ***
            // **********************************************
            MyX.Egyesít(munkalap, "a4:c4");
            MyX.Kiir("Dolgozószám:", "a4");
            MyX.Egyesít(munkalap, "d4:n4");
            MyX.Kiir("Dolgozó neve:", "d4");
            MyX.Egyesít(munkalap, "o4:r4");
            MyX.Kiir("Dolgozó aláírása:", "o4");
            MyX.Rácsoz(munkalap, "a4:r4");
            MyX.Vastagkeret(munkalap, "a4:r4");
            Beállítás_Betű beállBetű = new Beállítás_Betű { Méret = 10, Dőlt = true, Vastag = true };
            MyX.Betű(munkalap, "a4:r4", beállBetű);
            MyX.Igazít_függőleges(munkalap, "a4:r4", "alsó");
            blokkeleje = sor;
            MyX.Sormagasság(munkalap, "1:4", 25);
            Holtart.Lép();
        }

        private void Munkalap_Fejléc(string szöveg1)
        {
            // **********************************************
            // ** munkalap fejléce         ******************
            // **********************************************
            MyX.Egyesít(munkalap, "a1:r1");
            MyX.Kiir("Munkautasítás", "a1");
            Beállítás_Betű beállBetű = new Beállítás_Betű { Méret = 22, Vastag = true };
            MyX.Betű(munkalap, "a1:r1", beállBetű);
            MyX.Igazít_vízszintes(munkalap, "a1:r1", "bal");
            MyX.Egyesít(munkalap, "a2:c2");
            MyX.Kiir("Munkautasítás száma:", "a2");
            MyX.Egyesít(munkalap, "a3:c3");

            MyX.Egyesít(munkalap, "d2:h2");
            MyX.Kiir("Munkarend:", "d2");
            beállBetű = new Beállítás_Betű { Méret = 10, Dőlt = true, Vastag = true };
            MyX.Betű(munkalap, "d2:h2", beállBetű);


            MyX.Egyesít(munkalap, "d3:h3");
            if (Munkarendlist.SelectedItems.Count != 0)
            {
                MyX.Kiir(Munkarendlist.SelectedItems[0].ToString(), "d3");
                beállBetű = new Beállítás_Betű { Vastag = true };
                MyX.Betű(munkalap, "d3", beállBetű);
            }

            MyX.Egyesít(munkalap, "i2:k2");
            MyX.Kiir("Dátum:", "i2");
            MyX.Egyesít(munkalap, "i3:k3");
            beállBetű = new Beállítás_Betű { Méret = 10, Dőlt = true, Vastag = true };
            MyX.Betű(munkalap, "i3:k3", beállBetű);


            MyX.Kiir(Dátum.Value.ToString("yyyy.MM.dd"), "i3");
            MyX.Egyesít(munkalap, "l2:n2");
            MyX.Kiir("Költséghely:", "l2");
            MyX.Egyesít(munkalap, "l3:n3");
            beállBetű = new Beállítás_Betű { Méret = 10, Dőlt = true, Vastag = true };
            MyX.Betű(munkalap, "l3:n3", beállBetű);


            MyX.Egyesít(munkalap, "o2:r2");
            beállBetű = new Beállítás_Betű { Méret = 10, Dőlt = true, Vastag = true };
            MyX.Betű(munkalap, "o2:r2", beállBetű);
            MyX.Egyesít(munkalap, "o3:r3");



            List<Adat_Munka_Szolgálat> Adatok = KézMunkaSzolg.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);
            Adat_Munka_Szolgálat Elem = (from a in Adatok
                                         orderby a.Üzem
                                         select a).FirstOrDefault();
            if (Elem != null)
            {
                MyX.Kiir(Elem.Költséghely.Trim(), "l3");
                MyX.Kiir(Elem.Szolgálat.Trim(), "o2");
                MyX.Kiir(Elem.Üzem.Trim(), "o3");
                MyX.Kiir(Elem.Üzem.Trim().Substring(0, 1) + szöveg1, "a3");
            }
            beállBetű = new Beállítás_Betű { Vastag = true };
            MyX.Betű(munkalap, "a3:r3", beállBetű);

            MyX.Rácsoz(munkalap, "a2:r3");
            MyX.Vastagkeret(munkalap, "a2:b3");
            MyX.Vastagkeret(munkalap, "c2:h3");
            MyX.Vastagkeret(munkalap, "i2:k3");
            MyX.Vastagkeret(munkalap, "l2:n3");
            MyX.Vastagkeret(munkalap, "o2:r3");
            MyX.Igazít_függőleges(munkalap, "a2:r2", "alsó");
            beállBetű = new Beállítás_Betű { Méret = 18 };
            MyX.Betű(munkalap, "a3:r3", beállBetű);
            Holtart.Lép();
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