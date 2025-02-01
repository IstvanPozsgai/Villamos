using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

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
        readonly Kezelő_Kiegészítő_Beosztáskódok KézBeoKód = new Kezelő_Kiegészítő_Beosztáskódok();
        readonly Kezelő_Dolgozó_Beosztás_Új KézBeosztás = new Kezelő_Dolgozó_Beosztás_Új();

        public Ablak_Munkalap_készítés()
        {
            InitializeComponent();
        }
        // Ez egy szöveg  Ez kék színű
        private void Ablak_Munkalap_Load(object sender, EventArgs e)
        {
            try
            {
                Telephelyekfeltöltése();
                Dátum.Value = DateTime.Today;

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Munkalap";
                if (!Directory.Exists(hely)) System.IO.Directory.CreateDirectory(hely);

                // ha nincs olyan évi adatbázis, akkor létrehozzuk az előző évi alapján ha van.
                //hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Munkalap\munkalap{Dátum.Value.Year}.mdb";
                //if (!File.Exists(hely)) KézMunkaFoly.AdatbázisLétrehozás(Cmbtelephely.Text, Dátum.Value);


                Jogosultságkiosztás();
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


        #region Alap
        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                Cmbtelephely.Items.AddRange(Listák.TelephelyLista_Jármű());
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

        private void Button13_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Munkalap.html";
                MyE.Megnyitás(hely);
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
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Segéd\kiegészítő.mdb";
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
                // töröljük a neveket
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Dolgozók.mdb";
                if (!File.Exists(hely)) return;

                List<Adat_Dolgozó_Alap> AdatokÖ = KézDolgozó.Lista_Adatok(Cmbtelephely.Text.Trim());

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

                for (int j = 0; j < Csoport.Items.Count; j++)
                {
                    if (Csoport.GetItemChecked(j))
                    {
                        List<Adat_Dolgozó_Alap> Adatok = new List<Adat_Dolgozó_Alap>();
                        // csoporttagokat kiválogatja
                        if (Csoport.Items[j].ToStrTrim() == "Összes")
                            Adatok = (from a in AdatokÖ
                                      where a.Kilépésiidő == new DateTime(1900, 1, 1)
                                      orderby a.DolgozóNév
                                      select a).ToList();
                        else
                            Adatok = (from a in AdatokÖ
                                      where a.Kilépésiidő == new DateTime(1900, 1, 1)
                                      && a.Csoport == Csoport.Items[j].ToStrTrim()
                                      orderby a.DolgozóNév
                                      select a).ToList();

                        foreach (Adat_Dolgozó_Alap rekord in Adatok)
                        {
                            Dolgozónév.RowCount++;
                            int i = Dolgozónév.RowCount - 1;
                            Dolgozónév.Rows[i].Cells[0].Value = rekord.Dolgozószám;
                            Dolgozónév.Rows[i].Cells[1].Value = rekord.DolgozóNév;
                        }
                    }
                }
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
        #endregion


        private void Irányítófeltöltés()
        {
            try
            {
                Kiadta.Items.Clear();
                Ellenőrizte.Items.Clear();
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Dolgozók.mdb";
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
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\futás\{Dátum.Value.Year}\vezénylés{Dátum.Value.Year}.mdb";
                if (!File.Exists(hely)) return;
                List<Adat_Vezénylés> AdatokÖ = KézVezénylés.Lista_Adatok(hely);

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


        #region Excel

        private void Excel_Click(object sender, EventArgs e)
        {
            if (Option6.Checked)
                ExcelKészítés_Egyéni();
            else
                ExcelKészítés_Csoportos();

            MessageBox.Show("A Munkalapok generálása befejeződött.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (Dolgozónév.Rows.Count == 0) throw new HibásBevittAdat("Nincs kiválasztva dolgozó");
                if (Dolgozónév.SelectedRows.Count == 0) throw new HibásBevittAdat("Nincs kiválasztva dolgozó");

                string könyvtár;
                string fájlexc;
                maximum = Típusoklistája.SelectedItems.Count;

                for (int hanyadikember = 0; hanyadikember < Dolgozónév.Rows.Count; hanyadikember++)
                {
                    if (Dolgozónév.Rows[hanyadikember].Selected)
                    {

                        Dolgozó = Dolgozónév.Rows[hanyadikember].Cells[1].Value.ToStrTrim();
                        HR = Dolgozónév.Rows[hanyadikember].Cells[0].Value.ToStrTrim();
                        string szöveg1 = DateTime.Now.ToString("yyMMddHHmmss");
                        fájlexc = $"Munkalap_{DateTime.Now:yyMMddHHmmss}_{HR}.xlsx";
                        könyvtár = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + $@"\{fájlexc}.xlsx";

                        sor = 5;
                        blokkeleje = 5;

                        Holtart.Be(10);
                        MyE.ExcelLétrehozás();
                        MyE.Munkalap_betű("arial", 12);
                        MyE.Oszlopszélesség(munkalap, "a:r", 6);

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
                        if (Option9.Checked) MyE.Nyomtatás(munkalap, 1, 1);

                        Holtart.Ki();
                        MyE.Aktív_Cella(munkalap, "A1");
                        MyE.ExcelMentés(fájlexc);
                        MyE.ExcelBezárás();

                        if (Option10.Checked) File.Delete(fájlexc + ".xlsx");
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
                if (Dolgozónév.Rows.Count == 0) throw new HibásBevittAdat("Nincs kiválasztva dolgozó");
                if (Dolgozónév.SelectedRows.Count == 0) throw new HibásBevittAdat("Nincs kiválasztva dolgozó");

                string könyvtár;
                string fájlexc;
                maximum = Típusoklistája.SelectedItems.Count;

                string szöveg1 = DateTime.Now.ToString("yyMMddHHmmss");
                fájlexc = $"Munkalap_{DateTime.Now:yyMMddHHmmss}.xlsx";
                könyvtár = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + $@"\{fájlexc}.xlsx";

                sor = 5;
                blokkeleje = 5;

                Holtart.Be(10);
                MyE.ExcelLétrehozás();
                MyE.Munkalap_betű("arial", 12);
                MyE.Oszlopszélesség(munkalap, "a:r", 6);

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
                if (Option9.Checked) MyE.Nyomtatás(munkalap, 1, 1);

                Holtart.Ki();
                MyE.Aktív_Cella(munkalap, "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();
                if (Option10.Checked) File.Delete(fájlexc + ".xlsx");
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

            MyE.NyomtatásiTerület_részletes(munkalap, $"a1:r{sor}", balMargó: 0.393700787401575d, jobbMargó: 0.393700787401575d, alsóMargó: 0.590551181102362d, felsőMargó: 0.590551181102362d,
                fejlécMéret: 0.511811023622047d, LáblécMéret: 0.511811023622047d, oldalszéles: "1", oldalmagas: "1");
        }

        private void Munkalap_Aláíró()
        {
            // **********************************************
            // **Aláíró sor                            ******
            // **********************************************
            sor += 1;
            MyE.Sormagasság($"{sor}:{sor}", 35);
            MyE.Egyesít(munkalap, $"a{sor}:c{sor + 1}");
            MyE.Kiir("A munkát kiadta:", $"a{sor}");
            MyE.Egyesít(munkalap, $"d{sor}:f{sor}");
            MyE.Egyesít(munkalap, $"g{sor}:i{sor + 1}");
            MyE.Kiir("A kiadott munkát\nelvégezte:", $"g{sor}");
            MyE.Egyesít(munkalap, $"j{sor}:l{sor + 1}");
            MyE.Egyesít(munkalap, $"m{sor}:o{sor + 1}");
            MyE.Sortörésseltöbbsorba_egyesített($"M{sor}:O{sor + 1}");
            MyE.Sortörésseltöbbsorba_egyesített($"G{sor}:I{sor + 1}");

            MyE.Igazít_vízszintes($"M{sor}:O{sor + 1}", "közép");
            MyE.Igazít_vízszintes($"G{sor}:I{sor + 1}", "közép");
            MyE.Kiir("A kiadott munkát\n ellenőrizte:", $"m{sor}");
            MyE.Egyesít(munkalap, $"p{sor}:r{sor}");

            MyE.Betű($"a{sor}:r{sor}", 10);
            MyE.Betű($"a{sor}:r{sor}", false, true, true);
            MyE.Rácsoz($"A{sor}:R{sor + 1}");


            sor += 1;
            MyE.Egyesít(munkalap, $"d{sor}:f{sor}");
            MyE.Kiir(Kiadta.Text.Trim(), $"d{sor}");
            MyE.Betű($"D{sor}", 10);
            MyE.Betű($"D{sor}", false, true, true);
            MyE.Egyesít(munkalap, $"p{sor}:r{sor}");
            MyE.Kiir(Ellenőrizte.Text.Trim(), $"p{sor}");
            MyE.Betű($"P{sor}", 10);
            MyE.Betű($"P{sor}", false, true, true);
            MyE.Igazít_függőleges($"A{sor}", "alsó");
            MyE.Igazít_függőleges($"G{sor}", "alsó");
            MyE.Igazít_függőleges($"M{sor}", "alsó");

            MyE.Vastagkeret($"A{sor - 1}:R{sor}");
            Holtart.Lép();
        }

        private void Munkalap_Pályaszám_E2_ICS()
        {

            // ////////////////////////////////////////
            // ///  E2     PÁLYASZÁM    ICS        ////
            // ////////////////////////////////////////

            List<string> AdatokÖssz;
            List<string> AdatokRész;

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
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";
                string hely3 = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos2ICS.mdb";

                for (int i = 0; i < Típusoklistája.Items.Count; i++)
                {
                    if (Típusoklistája.GetItemChecked(i))
                    {
                        AdatokRész = KézJármű.Lista_Pályaszámok(hely3, mennyi);
                        AdatokÖssz = KézJármű.Lista_Pályaszámok(hely, Típusoklistája.Items[i].ToStrTrim());

                        if (AdatokRész != null)
                        {
                            sor += 1;
                            blokkeleje = sor;
                            oszlop = 3;
                            foreach (string PályaszámLista in AdatokRész)
                            {
                                if (AdatokÖssz.Contains(PályaszámLista.Trim()))
                                    if (oszlop == 18)
                                    {
                                        oszlop = 3;
                                        sor += 1;
                                    }
                                oszlop += 1;
                                MyE.Kiir(PályaszámLista.Trim(), MyE.Oszlopnév(oszlop) + sor.ToString());
                            }
                        }

                        MyE.Egyesít(munkalap, $"a{blokkeleje}:c{sor}");
                        if (Típusoklistája.Items[i].ToStrTrim() != "Üres")
                        {
                            MyE.Kiir("E2-  " + Típusoklistája.Items[i].ToStrTrim(), $"A{blokkeleje}");
                        }

                        MyE.Rácsoz($"d{blokkeleje}:r{sor}");
                        MyE.Vastagkeret($"d{blokkeleje}:r{sor}");
                        MyE.Vastagkeret($"a{blokkeleje}:r{sor}");
                        MyE.Sormagasság($"{blokkeleje}:{sor}", 25);
                        MyE.Betű($"A{blokkeleje}:R{sor}", false, false, true);
                    }
                }
            }
        }

        private void Munkalap_Pályaszám_E3_ICS()
        {

            // ////////////////////////////////////////
            // ///  E3     PÁLYASZÁM    ICS        ////
            // ////////////////////////////////////////
            List<string> AdatokÖssz;
            List<string> AdatokRész;

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

            if (E3ICS.Checked == true && maximum >= 1)
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";
                string hely3 = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos2ICS.mdb";

                for (int i = 0; i < Típusoklistája.Items.Count; i++)
                {
                    if (Típusoklistája.GetItemChecked(i) == true)
                    {
                        AdatokRész = KézJármű.Lista_Pályaszámok(hely3, mennyi);
                        AdatokÖssz = KézJármű.Lista_Pályaszámok(hely, Típusoklistája.Items[i].ToStrTrim());

                        if (AdatokRész != null)
                        {
                            sor += 1;
                            blokkeleje = sor;
                            oszlop = 3;
                            foreach (string PályaszámLista in AdatokRész)
                            {
                                if (AdatokÖssz.Contains(PályaszámLista.Trim()))
                                {
                                    if (oszlop == 18)
                                    {
                                        oszlop = 3;
                                        sor += 1;
                                    }
                                    oszlop += 1;
                                    MyE.Kiir(PályaszámLista.Trim(), MyE.Oszlopnév(oszlop) + sor.ToString());
                                }
                            }

                            MyE.Egyesít(munkalap, $"a{blokkeleje}:c{sor}");

                            if (Típusoklistája.Items[i].ToStrTrim() != "Üres")
                            {
                                MyE.Kiir("E3-  " + Típusoklistája.Items[i].ToString(), $"A{blokkeleje}");
                            }

                            MyE.Rácsoz($"d{blokkeleje}:r{sor}");
                            MyE.Vastagkeret($"d{blokkeleje}:r{sor}");
                            MyE.Vastagkeret($"a{blokkeleje}:r{sor}");
                            MyE.Sormagasság($"{blokkeleje}:{sor}", 25);
                            MyE.Betű($"A{blokkeleje}:R{sor}", false, false, true);
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
            List<string> AdatokVez;
            List<string> AdatokÖssz;

            if (E3pályaszám.Checked & maximum >= 1)
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";
                string helyvez = $@"{Application.StartupPath}\{Cmbtelephely.Text}\adatok\főkönyv\futás\{Dátum.Value.Year}\vezénylés{Dátum.Value.Year}.mdb";
                if (!File.Exists(helyvez)) return;
                for (int i = 0; i < Típusoklistája.Items.Count; i++)
                {
                    if (Típusoklistája.GetItemChecked(i) == true)
                    {
                        AdatokVez = KézVezénylés.Lista_Pályaszámok(helyvez, Dátum.Value);
                        AdatokÖssz = KézJármű.Lista_Pályaszámok(hely, Típusoklistája.Items[i].ToStrTrim());

                        if (AdatokVez != null)
                        {
                            sor += 1;
                            blokkeleje = sor;
                            oszlop = 3;
                            foreach (string PályaszámLista in AdatokVez)
                            {
                                if (AdatokÖssz.Contains(PályaszámLista.Trim()))
                                {
                                    if (oszlop == 18)
                                    {
                                        oszlop = 3;
                                        sor += 1;
                                    }
                                    oszlop += 1;
                                    MyE.Kiir(PályaszámLista, MyE.Oszlopnév(oszlop) + sor.ToString());
                                }
                            }
                            MyE.Egyesít(munkalap, $"a{blokkeleje}:c{sor}");
                            if (Típusoklistája.Items[i].ToStrTrim() != "Üres")
                            {
                                MyE.Kiir("E3-  " + Típusoklistája.Items[i].ToString(), $"A{blokkeleje}");
                            }
                            MyE.Rácsoz($"d{blokkeleje}:r{sor}");
                            MyE.Vastagkeret($"d{blokkeleje}:r{sor}");
                            MyE.Vastagkeret($"a{blokkeleje}:r{sor}");
                            MyE.Sormagasság($"{blokkeleje}:{sor}", 25);
                            MyE.Betű($"A{blokkeleje}:R{sor}", false, false, true);
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
            List<string> AdatokÖssz;
            List<string> AdatokRész;

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
            if (E2pályaszám.Checked == true && maximum >= 1 && mennyi > 0)
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";
                string hely3 = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos2.mdb";

                for (int i = 0; i < Típusoklistája.Items.Count; i++)
                {
                    if (Típusoklistája.GetItemChecked(i))
                    {
                        AdatokRész = KézJármű.Lista_Pályaszámok(hely3, mennyi, "T5C5");
                        AdatokÖssz = KézJármű.Lista_Pályaszámok(hely, Típusoklistája.Items[i].ToStrTrim());

                        if (AdatokRész != null)
                        {
                            sor += 1;
                            blokkeleje = sor;
                            oszlop = 3;
                            foreach (string PályaszámLista in AdatokRész)
                            {
                                if (AdatokÖssz.Contains(PályaszámLista))
                                {
                                    if (oszlop == 18)
                                    {
                                        oszlop = 3;
                                        sor += 1;
                                    }
                                    oszlop += 1;
                                    MyE.Kiir(PályaszámLista, MyE.Oszlopnév(oszlop) + sor.ToString());
                                }
                            }

                            MyE.Egyesít(munkalap, $"a{blokkeleje}:c{sor}");
                            if (Típusoklistája.Items[i].ToStrTrim() != "Üres")
                            {
                                MyE.Kiir("E2-  " + Típusoklistája.Items[i].ToString(), $"A{blokkeleje}");
                            }

                            MyE.Rácsoz($"d{blokkeleje}:r{sor}");
                            MyE.Vastagkeret($"d{blokkeleje}:r{sor}");
                            MyE.Vastagkeret($"a{blokkeleje}:r{sor}");
                            MyE.Sormagasság($"{blokkeleje}:{sor}", 25);
                            MyE.Betű($"A{blokkeleje}:R{sor}", false, false, true);
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
            List<string> Adatok;

            // minden pályaszám
            if (E1_pályaszámok.Checked == true && maximum >= 1)
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";

                blokkeleje = sor;
                for (int i = 0; i < Típusoklistája.Items.Count; i++)
                {
                    if (Típusoklistája.GetItemChecked(i) == true)
                    {
                        Adatok = KézJármű.Lista_Pályaszámok(hely, Típusoklistája.Items[i].ToStrTrim());

                        if (Adatok != null)
                        {
                            sor += 1;
                            blokkeleje = sor;
                            oszlop = 3;
                            foreach (string PályaszámLista in Adatok)
                            {
                                if (oszlop == 18)
                                {
                                    oszlop = 3;
                                    sor += 1;
                                }
                                oszlop += 1;
                                MyE.Kiir(PályaszámLista, MyE.Oszlopnév(oszlop) + sor.ToString());
                            }

                            MyE.Egyesít(munkalap, $"a{blokkeleje}" + $":c{sor}");
                            if (Típusoklistája.Items[i].ToStrTrim() != "Üres")
                            {
                                MyE.Kiir("E1- " + Típusoklistája.Items[i].ToStrTrim(), $"a{blokkeleje}");
                            }
                            MyE.Rácsoz($"d{blokkeleje}:r{sor}");
                            MyE.Vastagkeret($"d{blokkeleje}:r{sor}");
                            MyE.Vastagkeret($"a{blokkeleje}:r{sor}");
                            MyE.Sormagasság($"{blokkeleje}:{sor}", 25);
                            MyE.Betű($"A{blokkeleje}:R{sor}", false, false, true);
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
            List<string> Adatok;
            if (Mindenpsz.Checked == true && maximum >= 1)
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";
                blokkeleje = sor;
                for (int i = 0; i < Típusoklistája.Items.Count; i++)
                {
                    if (Típusoklistája.GetItemChecked(i))
                    {
                        Adatok = KézJármű.Lista_Pályaszámok(hely, Típusoklistája.Items[i].ToStrTrim());

                        if (Adatok != null)
                        {
                            sor += 1;
                            blokkeleje = sor;
                            oszlop = 3;
                            foreach (string PályaszámLista in Adatok)
                            {
                                if (oszlop == 18)
                                {
                                    oszlop = 3;
                                    sor += 1;
                                }
                                oszlop += 1;
                                MyE.Kiir(PályaszámLista, MyE.Oszlopnév(oszlop) + sor.ToString());

                            }

                            MyE.Egyesít(munkalap, $"a{blokkeleje}:c{sor}");
                            if (Típusoklistája.Items[i].ToStrTrim() != "Üres")
                            {
                                MyE.Kiir(Típusoklistája.Items[i].ToStrTrim(), $"a{blokkeleje}");
                            }
                            MyE.Rácsoz($"d{blokkeleje}:r{sor}");
                            MyE.Vastagkeret($"d{blokkeleje}:r{sor}");
                            MyE.Vastagkeret($"a{blokkeleje}:r{sor}");
                            MyE.Sormagasság($"{blokkeleje}:{sor}", 25);
                            MyE.Betű($"A{blokkeleje}:R{sor}", false, false, true);
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
            MyE.Egyesít(munkalap, $"a{sor}:c{sor}");
            MyE.Kiir("Típus:", $"a{sor}");
            MyE.Egyesít(munkalap, $"d{sor}:r{sor}");
            MyE.Kiir("Pályaszámok:", $"d{sor}");
            MyE.Betű($"a{sor}:r{sor}", 10);
            MyE.Betű($"a{sor}:r{sor}", false, true, true);
            MyE.Rácsoz($"a{sor}:r{sor}");
            MyE.Vastagkeret($"a{sor}:r{sor}");
            Holtart.Lép();
        }

        private void Munkalap_Összesítő()
        {

            // **********************************************
            // ** öszesítős sor                           ***
            // **********************************************
            sor += 1;
            MyE.Egyesít(munkalap, $"a{sor}:p{sor}");
            MyE.Egyesít(munkalap, $"q{sor}:r{sor}");
            MyE.Kiir("Összesen:", $"a{sor}");
            MyE.Sormagasság($"{sor}:{sor}", 25);
            MyE.Igazít_vízszintes($"A{sor}", "jobb");
            MyE.Igazít_függőleges($"A{sor}", "alsó");
            MyE.Betű($"a{sor}:r{sor}", 10);
            MyE.Betű($"a{sor}:r{sor}", false, true, true);
            MyE.Rácsoz($"a{sor}:r{sor}");
            MyE.Vastagkeret($"a{sor}:r{sor}");
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
                        MyE.Egyesít(munkalap, $"a{sor}:c{sor}");
                        MyE.Egyesít(munkalap, $"d{sor}:e{sor}");
                        MyE.Egyesít(munkalap, $"f{sor}:g{sor}");
                        MyE.Egyesít(munkalap, $"h{sor}:p{sor}");
                        MyE.Egyesít(munkalap, $"q{sor}:r{sor}");
                    }
                    MyE.Betű($"a{blokkeleje}:r{sor}", 16);
                    MyE.Betű($"h{blokkeleje}:p{sor}", 11);
                    MyE.Sormagasság($"a{blokkeleje}:r{sor}", 24);
                    MyE.Rácsoz($"a{blokkeleje}:r{sor}");
                    MyE.Vastagkeret($"a{blokkeleje}:r{sor}");
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
                                MyE.Sormagasság($"a{sor}:r{sor}", 24);
                                MyE.Egyesít(munkalap, $"a{sor}:c{sor}");
                                MyE.Egyesít(munkalap, $"d{sor}:e{sor}");
                                MyE.Egyesít(munkalap, $"f{sor}:g{sor}");
                                MyE.Egyesít(munkalap, $"h{sor}:p{sor}");
                                MyE.Egyesít(munkalap, $"q{sor}:r{sor}");
                                MyE.Kiir(V1Tábla.Rows[k].Cells[0].Value.ToString(), $"a{sor}");
                                MyE.Kiir(V1Tábla.Rows[k].Cells[1].Value.ToString(), $"d{sor}");
                                MyE.Kiir(MunkafolyamatTábla.Rows[i].Cells[0].Value.ToString(), $"h{sor}");
                                if (MunkafolyamatTábla.Rows[i].Cells[0].Value.ToString().Length > 55)
                                {
                                    // ha hosszabb a szöveg 55-nél akkor több sorba írja
                                    MyE.Sortörésseltöbbsorba_egyesített($"H{sor}");
                                    int sor_magasság = ((MunkafolyamatTábla.Rows[i].Cells[0].Value.ToString().Length / 55) + 1) * 20;
                                    MyE.Sormagasság($"{sor}:{sor}", sor_magasság);
                                }
                                else
                                {
                                    // Ha rövidebb
                                    MyE.Sormagasság($"{sor}:{sor}", 24);
                                }
                            }
                        }

                        else
                        {
                            sor += 1;
                            MyE.Sormagasság($"a{sor}:r{sor}", 24);
                            MyE.Egyesít(munkalap, $"a{sor}:c{sor}");
                            MyE.Egyesít(munkalap, $"d{sor}:e{sor}");
                            MyE.Egyesít(munkalap, $"f{sor}:g{sor}");
                            MyE.Egyesít(munkalap, $"h{sor}:p{sor}");
                            MyE.Egyesít(munkalap, $"q{sor}:r{sor}");
                            MyE.Kiir(MunkafolyamatTábla.Rows[i].Cells[3].Value.ToString(), $"a{sor}");
                            MyE.Kiir(MunkafolyamatTábla.Rows[i].Cells[1].Value.ToString(), $"d{sor}");
                            MyE.Kiir(MunkafolyamatTábla.Rows[i].Cells[0].Value.ToString(), $"h{sor}");
                            if (MunkafolyamatTábla.Rows[i].Cells[0].Value.ToString().Length > 55)
                            {
                                // ha hosszabb a szöveg 55-nél akkor több sorba írja
                                MyE.Sortörésseltöbbsorba_egyesített($"H{sor}");
                                int sor_magasság = ((MunkafolyamatTábla.Rows[i].Cells[0].Value.ToString().Length / 55) + 1) * 20;
                                MyE.Sormagasság($"{sor}:{sor}", sor_magasság);
                            }
                            else
                            {
                                MyE.Sormagasság($"{sor}:{sor}", 24);
                            }
                        }
                    }
                }
                MyE.Betű($"a{blokkeleje}:r{sor}", 16);
                MyE.Betű($"A{blokkeleje}:R{sor}", false, false, true);
                MyE.Igazít_függőleges($"A{blokkeleje}:R{sor}", "alsó");
                MyE.Betű($"h{blokkeleje}:p{sor}", 11);
                MyE.Betű($"H{blokkeleje}:P{sor}", false, false, true);
                MyE.Igazít_függőleges($"H{blokkeleje}:P{sor}", "alsó");
                MyE.Rácsoz($"a{blokkeleje}:r{sor}");
                MyE.Vastagkeret($"a{blokkeleje}:r{sor}");
                Holtart.Lép();
            }
        }

        private void Munkalap_munkaFejléc()
        {
            // **********************************************
            // ** munkafejléc                              **
            // **********************************************
            MyE.Egyesít(munkalap, $"a{sor}:c{sor}");
            MyE.Kiir("Rendelési szám:", $"a{sor}");
            MyE.Egyesít(munkalap, $"d{sor}:e{sor}");
            MyE.Kiir("Pályaszám:", $"d{sor}");
            MyE.Egyesít(munkalap, $"f{sor}:g{sor}");
            MyE.Kiir("Darab:", $"f{sor}");
            MyE.Egyesít(munkalap, $"h{sor}:p{sor}");
            MyE.Kiir("Munkafolyamat megnevezése:", $"h{sor}");
            MyE.Egyesít(munkalap, $"q{sor}:r{sor}");
            MyE.Kiir("Időráfordítás [perc]:", $"q{sor}");
            MyE.Betű($"a{sor}:r{sor}", 10);
            MyE.Betű($"a{sor}:r{sor}", false, true, true);
            MyE.Igazít_függőleges($"a{sor}:r{sor}", "alsó");

            MyE.Sortörésseltöbbsorba_egyesített($"Q{sor}:R{sor}");
            MyE.Igazít_függőleges($"Q{sor}:R{sor}", "alsó");
            MyE.Igazít_vízszintes($"Q{sor}:R{sor}", "közép");


            MyE.Rácsoz($"a{sor}:r{sor}");
            MyE.Vastagkeret($"a{sor}:r{sor}");
            MyE.Sormagasság($"{sor}:{sor}", 35);
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
                    MyE.Egyesít(munkalap, $"a{sor}:e{sor}");
                    MyE.Kiir(dolgozószám, $"a{sor}");
                    MyE.Egyesít(munkalap, $"f{sor}:n{sor}");
                    MyE.Kiir(dolgozóneve, $"f{sor}");
                    MyE.Egyesít(munkalap, $"o{sor}:r{sor}");
                    sor += 1;
                }
            }

            MyE.Betű($"a{blokkeleje}:r{sor}", 20);
            MyE.Betű($"a{blokkeleje}:r{sor}", false, false, true);
            MyE.Sormagasság($"{blokkeleje}:{sor}", 35);
            MyE.Rácsoz($"a{blokkeleje}:r{sor}");
            MyE.Vastagkeret($"a{blokkeleje}:r{sor}");
        }

        private void Dolgozó_Neve_Egy()
        {
            // **********************************************
            // ** dolgozók neve                          ****
            // **********************************************

            MyE.Egyesít(munkalap, $"a{sor}:e{sor}");
            MyE.Kiir(HR, $"a{sor}");
            MyE.Egyesít(munkalap, $"f{sor}:n{sor}");
            MyE.Kiir(Dolgozó, $"f{sor}");
            MyE.Egyesít(munkalap, $"o{sor}:r{sor}");
            sor += 1;


            MyE.Betű($"a{blokkeleje}:r{sor}", 20);
            MyE.Betű($"a{blokkeleje}:r{sor}", false, false, true);
            MyE.Igazít_függőleges($"a{blokkeleje}:r{sor}", "alsó");
            MyE.Sormagasság($"{blokkeleje}:{sor}", 35);
            MyE.Rácsoz($"a{blokkeleje}:r{sor}");
            MyE.Vastagkeret($"a{blokkeleje}:r{sor}");
        }

        private void Dolgozó_Fejléc()
        {
            // **********************************************
            // ** dolgozó fejléc                          ***
            // **********************************************
            MyE.Egyesít(munkalap, "a4:e4");
            MyE.Kiir("Dolgozószám:", "a4");
            MyE.Egyesít(munkalap, "f4:n4");
            MyE.Kiir("Dolgozó neve:", "f4");
            MyE.Egyesít(munkalap, "o4:r4");
            MyE.Kiir("Dolgozó aláírása:", "o4");
            MyE.Rácsoz("a4:r4");
            MyE.Vastagkeret("a4:r4");
            MyE.Betű("a4:r4", 10);
            MyE.Betű("a4:r4", false, true, true);
            MyE.Igazít_függőleges("a4:r4", "alsó");
            blokkeleje = sor;
            MyE.Sormagasság("1:4", 25);
            Holtart.Lép();
        }

        private void Munkalap_Fejléc(string szöveg1)
        {
            // **********************************************
            // ** munkalap fejléce         ******************
            // **********************************************
            MyE.Egyesít(munkalap, "a1:r1");
            MyE.Kiir("Munkautasítás", "a1");
            MyE.Betű("a1:r1", 22);
            MyE.Betű("a1:r1", false, false, true);
            MyE.Igazít_vízszintes("a1:r1", "bal");
            MyE.Egyesít(munkalap, "a2:d2");
            MyE.Kiir("Munkautasítás száma:", "a2");
            MyE.Egyesít(munkalap, "a3:d3");

            MyE.Egyesít(munkalap, "e2:h2");
            MyE.Kiir("Munkarend:", "e2");
            MyE.Betű("a2:r2", 10);
            MyE.Betű("a2:r2", false, true, true);

            MyE.Egyesít(munkalap, "e3:h3");
            if (Munkarendlist.SelectedItems.Count != 0)
            {
                MyE.Kiir(Munkarendlist.SelectedItems[0].ToString(), "e3");
                MyE.Betű("e3", false, false, true);
            }

            MyE.Egyesít(munkalap, "i2:k2");
            MyE.Kiir("Dátum:", "i2");
            MyE.Egyesít(munkalap, "i3:k3");
            MyE.Kiir(Dátum.Value.ToString("yyyy.MM.dd"), "i3");
            MyE.Egyesít(munkalap, "l2:n2");
            MyE.Kiir("Költséghely:", "l2");
            MyE.Egyesít(munkalap, "l3:n3");
            MyE.Egyesít(munkalap, "o2:r2");
            MyE.Egyesít(munkalap, "o3:r3");


            Kezelő_Munka_Szolgálat kéz = new Kezelő_Munka_Szolgálat();
            List<Adat_Munka_Szolgálat> Adatok = kéz.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);
            Adat_Munka_Szolgálat Elem = (from a in Adatok
                                         orderby a.Üzem
                                         select a).FirstOrDefault();
            if (Elem != null)
            {
                MyE.Kiir(Elem.Költséghely.Trim(), "l3");
                MyE.Kiir(Elem.Szolgálat.Trim(), "o2");
                MyE.Kiir(Elem.Üzem.Trim(), "o3");
                MyE.Kiir(Elem.Üzem.Trim().Substring(0, 1) + szöveg1, "a3");
            }
            MyE.Betű("a3:r3", false, false, true);

            MyE.Rácsoz("a2:r3");
            MyE.Vastagkeret("a2:d3");
            MyE.Vastagkeret("e2:h3");
            MyE.Vastagkeret("i2:k3");
            MyE.Vastagkeret("l2:n3");
            MyE.Vastagkeret("o2:r3");
            MyE.Igazít_függőleges("a2:r2", "alsó");
            MyE.Betű("a3:r3", 18);
            Holtart.Lép();
        }
        #endregion

        private void Benn_Lévők_Click(object sender, EventArgs e)
        {
            try
            {
                Holtart.Be();
                // minden kijelölést töröl
                for (int i = 0; i < Dolgozónév.Rows.Count; i++)
                    Dolgozónév.Rows[i].Selected = false;

                Kiválogat_dolgozó();

                Holtart.Lép();
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

        private void Kiválogat_dolgozó()
        {
            try
            {
                string helykieg = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő.mdb";
                if (!File.Exists(helykieg)) return;

                List<Adat_Kiegészítő_Beosztáskódok> BeosztáskódÖ = KézBeoKód.Lista_Adatok(helykieg);
                List<Adat_Kiegészítő_Beosztáskódok> Beosztáskód = (from a in BeosztáskódÖ
                                                                   where a.Számoló == true
                                                                   orderby a.Beosztáskód
                                                                   select a).ToList();

                List<Adat_Dolgozó_Beosztás_Új> DolgbeosztÖ = KézBeosztás.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value);
                List<Adat_Dolgozó_Beosztás_Új> Dolgbeoszt = (from a in DolgbeosztÖ
                                                             where a.Nap == Dátum.Value
                                                             orderby a.Dolgozószám
                                                             select a).ToList();


                //ha ki van jelölve
                for (int i = 0; i < Dolgozónév.Rows.Count; i++)
                {
                    string HRazonosító = Dolgozónév.Rows[i].Cells[0].Value.ToStrTrim();

                    string dolgozik = (from a in Dolgbeoszt
                                       where a.Dolgozószám.Trim() == HRazonosító.Trim()
                                       select a.Beosztáskód).FirstOrDefault();
                    //Van beosztása, akkor megnézzük, hogy az olyan amit be akarunk jelölni.
                    if (dolgozik != null)
                    {
                        string biztosdolgozik = (from a in Beosztáskód
                                                 where dolgozik.Trim() == a.Beosztáskód.Trim()
                                                 select a.Beosztáskód).FirstOrDefault();
                        if (biztosdolgozik != null)
                            Dolgozónév.Rows[i].Selected = true;
                    }
                    Holtart.Lép();
                }
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
    }
}