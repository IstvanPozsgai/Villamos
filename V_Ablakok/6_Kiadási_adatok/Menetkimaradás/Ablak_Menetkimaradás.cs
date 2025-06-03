using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Ablakok;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using static System.IO.File;
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;
using MyMen = Villamos.Villamos_Ablakok._6_Kiadási_adatok.Menetkimaradás.Menetkimaradás;
using MyO = Microsoft.Office.Interop.Outlook;

namespace Villamos
{
    public partial class AblakMenetkimaradás
    {
        Kezelő_Menetkimaradás KézMenet = new Kezelő_Menetkimaradás();

        string Html_szöveg = "";
        string hely_;
        string jelszó_;
        int idszám_;

        Ablak_Menetrögítés Új_Ablak_Menetrögítés = null;
        Ablak_Menetkimaradás_Kiegészítő Új_Ablak_Menetkimaradás_Kiegészítő = null;

        public AblakMenetkimaradás()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            // beállítjuk a dátumot az előző napra mert mai adat még nincs
            Dátum.Value = DateTime.Now.AddDays(-1);
            Telephelyekfeltöltése();
            Szolgálatoklista();
            Telephelyek_Feltöltése_lista();
        }

        private void Menetkimaradás_Load(object sender, EventArgs e)
        {
            string hely;
            // ha járműkiadó telephely, akkor csak a saját telephelyet kezeli.
            if (cmbtelephely.Enabled == false)
            {
                Panel1.Visible = false;
                Panel2.Visible = false;
                // leellenőrizzük, hogy létezik-e a feltölteni kívánt adat helye
                hely = $@"{Application.StartupPath}\{cmbtelephely.Text.Trim()}\Adatok\főkönyv\menet" + Dátum.Value.ToString("yyyy") + ".mdb";
                if (!Exists(hely))
                    Adatbázis_Létrehozás.Menekimaradás_telephely(hely);
            }
            else
            {
                Panel1.Visible = true;
                Panel2.Visible = true;
            }

            // leellenőrizzük a főmérnökségi tábla létezik-e ha nem akkor másoljuk
            hely = Application.StartupPath + @"\Főmérnökség\Adatok\" + Dátum.Value.ToString("yyyy") + @"\" + Dátum.Value.ToString("yyyy") + "_menet_adatok.mdb";
            if (!Exists(hely))
                Adatbázis_Létrehozás.Menekimaradás_Főmérnökség(hely);
            Pályaszámokfeltöltése();

            // Jogosultságok beállítása
            Jogosultságkiosztás();
        }

        #region Alap

        void Telephelyek_Feltöltése_lista()
        {
            string hely = Application.StartupPath + @"\Főmérnökség\Adatok\kiegészítő.mdb";
            string jelszó = "Mocó";
            string szöveg = "SELECT * FROM telephelytábla ORDER BY telephelynév";

            Lstüzemek.BeginUpdate();
            Lstüzemek.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "telephelynév"));
            Lstüzemek.EndUpdate();
            Lstüzemek.Refresh();
        }

        private void Telephelyekfeltöltése()
        {
            try
            {
                cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    cmbtelephely.Items.Add(Elem);

                cmbtelephely1.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    cmbtelephely1.Items.Add(Elem);
                cmbtelephely.Enabled = false;

                if (Program.PostásTelephely == "Főmérnökség" || Program.PostásTelephely.Contains("törzs"))
                {
                    cmbtelephely.Text = cmbtelephely.Items[0].ToString().Trim();
                    cmbtelephely1.Text = cmbtelephely1.Items[0].ToString().Trim();
                }
                else
                {
                    cmbtelephely.Text = Program.PostásTelephely;
                    cmbtelephely1.Text = Program.PostásTelephely;
                }

                cmbtelephely.Enabled = Program.Postás_Vezér;
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

        private void SúgóToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string hely = Application.StartupPath + @"\Súgó\VillamosLapok\menetkimaradás.html";
            Module_Excel.Megnyitás(hely);
        }

        private void Pályaszámokfeltöltése()
        {
            try
            {
                Pályaszámok.Items.Clear();
                string hely;
                if (cmbtelephely.Enabled == false)
                {
                    // telephelyi adatok
                    hely = $@"{Application.StartupPath}\{cmbtelephely.Text.Trim()}\Adatok\főkönyv\menet" + Dátum.Value.ToString("yyyy") + ".mdb";
                }
                else
                {
                    // főmérnökségi adatok 
                    hely = Application.StartupPath + @"\Főmérnökség\adatok\" + Dátum.Value.ToString("yyyy") + @"\" + Dátum.Value.ToString("yyyy") + "_menet_adatok.mdb";
                }
                if (!File.Exists(hely)) throw new HibásBevittAdat($"A {Dátum.Value:yyyy.MM.dd} dátumra a program nem rendelkezik adatokkal.");
                string jelszó = "lilaakác";
                string szöveg = "SELECT DISTINCT azonosító FROM menettábla order by azonosító";
                Pályaszámok.BeginUpdate();
                Pályaszámok.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "azonosító"));
                Pályaszámok.EndUpdate();
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
            int melyikelem;

            // ide kell az összes gombot tenni amit szabályozni akarunk false

            SAPAdatokBetöltéseToolStripMenuItem.Enabled = false;
            CheckBox2.Checked = false;
            FőmérnökségiLekérdezésToolStripMenuItem.Visible = false;
            Button1.Visible = false;
            Button2.Visible = false;
            Button3.Visible = false;
            Button4.Enabled = false;

            melyikelem = 20;

            // módosítás 1 
            if (MyF.Vanjoga(melyikelem, 1))
            {
                SAPAdatokBetöltéseToolStripMenuItem.Enabled = true;
            }
            // módosítás 2 
            if (MyF.Vanjoga(melyikelem, 2))
            {
                CheckBox2.Checked = true;
            }

            // módosítás 3
            if (MyF.Vanjoga(melyikelem, 3))
            {

                Panel1.Visible = true;
                Panel2.Visible = true;
                FőmérnökségiLekérdezésToolStripMenuItem.Visible = true;
            }

            melyikelem = 21;
            // Megjelenítés
            if (Program.PostásJogkör.Substring(melyikelem - 1, 1) == "1")
            {
                // e-mail
                Button4.Enabled = true;
            }

            // módosítás 1 
            if (MyF.Vanjoga(melyikelem, 1))
            {

                // I szakszolgálat
                Button1.Visible = true;
                Button4.Enabled = true;
            }
            // módosítás 2 
            if (MyF.Vanjoga(melyikelem, 2))
            {
                // II szakszolgálat
                Button2.Visible = true;
                Button4.Enabled = true;
            }

            // módosítás 3
            if (MyF.Vanjoga(melyikelem, 3))
            {
                // III szakszolgálat
                Button3.Visible = true;
                Button4.Enabled = true;
            }
        }

        private void Táblatörlése()
        {
            Tábla.Rows.Clear();
            Tábla.Columns.Clear();
            Tábla.Refresh();
            Tábla.ClearSelection();
        }

        private void AblakMenetkimaradás_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Menetrögítés?.Close();
            Új_Ablak_Menetkimaradás_Kiegészítő?.Close();
        }

        #endregion


        #region Beolvasás
        private void SAPAdatokBetöltéseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                // ha nincs kiválasztva telephely, akkor nem tudunk feltölteni adatot.
                if (cmbtelephely.Text.Trim() == "") return;
                // leellenőrizzük, hogy létezik-e a feltölteni kívánt adat helye
                string hely = $@"{Application.StartupPath}\{cmbtelephely.Text.Trim()}\Adatok\főkönyv\menet{Dátum.Value:yyyy}.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.Menekimaradás_telephely(hely);

                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "SAP-s Adatok betöltése",
                    FileName = "",
                    Filter = "Excel |*.xlsx"
                };
                string fájlexc;
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;

                // beolvassuk a felelős munkahelyet
                string felelősmunkahely = Felelős_Munkahely();

                DateTime Eleje = DateTime.Now;
                //Adattáblába tesszük
                DataTable Tábla = MyF.Excel_Tábla_Beolvas(fájlexc);

                //  beolvassuk a fejlécet ha eltér a megadotttól, akkor kiírja és bezárja
                if (!MyMen.Adategyezzés(Tábla)) throw new HibásBevittAdat("Nem megfelelő a betölteni kívánt adatok formátuma ! ");

                //Készítünk egy listát az adatszerkezetnek megfelelően   az Excel táblából
                hely = $@"{Application.StartupPath}\{cmbtelephely.Text.Trim()}\Adatok\főkönyv\menet{Dátum.Value.Year}.mdb";

                List<Adat_Menetkimaradás> Excel_Listában = MyMen.Excel_Lista(Tábla, felelősmunkahely);

                //Feltöltjük az eddig rögzített adatokat
                string jelszó = "lilaakác";
                string szöveg = "SELECT * FROM Menettábla order by id desc";
                Kezelő_Menetkimaradás KézMen = new Kezelő_Menetkimaradás();
                List<Adat_Menetkimaradás> Adatok = KézMen.Lista_Adatok(hely, jelszó, szöveg);
                long Id = 1;
                if (Adatok.Count > 0) Id = Adatok.Max(a => a.Id);

                Holtart.Be(Adatok.Count + 1);
                List<string> szövegGy = new List<string>();
                // Excel elemeket rögzítéshez előkészítjük
                foreach (Adat_Menetkimaradás rekord in Excel_Listában)
                {
                    Adat_Menetkimaradás Elem = (from a in Adatok
                                                where a.Tétel == rekord.Tétel && a.Jelentés == rekord.Jelentés
                                                select a).FirstOrDefault();

                    if (Elem != null)
                        szöveg = MyMen.Módosít(rekord);
                    else
                    {
                        // ha még nem volt akkor újként rögzítjük
                        Id++;
                        szöveg = MyMen.Rögzít(rekord, Id);
                    }
                    szövegGy.Add(szöveg);
                    Holtart.Lép();

                }
                if (szövegGy.Count > 0) MyA.ABMódosítás(hely, jelszó, szövegGy);

                Holtart.Ki();
                // kitöröljük a betöltött fájlt
                File.Delete(fájlexc);
                MessageBox.Show("Az adat konvertálás befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private string Felelős_Munkahely()
        {
            string válasz = "";
            try
            {
                Kezelő_Telep_Kiegészítő_SAP KézSap = new Kezelő_Telep_Kiegészítő_SAP();
                List<Adat_Telep_Kiegészítő_SAP> AdatokSAP = KézSap.Lista_Adatok(cmbtelephely.Text.Trim());
                Adat_Telep_Kiegészítő_SAP RekordSAP = (from a in AdatokSAP
                                                       where a.Id == 1
                                                       select a).FirstOrDefault();

                if (RekordSAP != null) válasz = RekordSAP.Felelősmunkahely;
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
            return válasz;
        }

        #endregion


        #region Excel

        private void ExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Menetkimaradás_" + Program.PostásNév + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                Module_Excel.EXCELtábla(fájlexc, Tábla, true);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyE.Megnyitás(fájlexc + ".xlsx");
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


        #region Lekérdezések

        private void Táblalistázás(int melyiket)
        {

            // ha ütes a telephely választó akkor nem listáz
            if (cmbtelephely1.Text.Trim() == "")
                return;

            string hely = $@"{Application.StartupPath}\" + cmbtelephely1.Text.Trim() + @"\Adatok\főkönyv\menet" + Dátum.Value.ToString("yyyy") + ".mdb";
            string jelszó = "lilaakác";
            hely_ = hely;
            jelszó_ = jelszó;
            Kezelő_Menetkimaradás kéz = new Kezelő_Menetkimaradás();


            string szöveg = "SELECT * ";

            switch (melyiket)
            {
                case 1:
                    {
                        szöveg += " FROM menettábla where [bekövetkezés]>=#" + Dátum.Value.ToString("yyyy-MM-dd") + " 00:00:0#";
                        szöveg += " and [bekövetkezés]<#" + Dátum.Value.ToString("yyyy-MM-dd") + " 23:59:0#";
                        break;
                    }
                case 2:
                    {
                        DateTime hónapelsőnapja = MyF.Hónap_elsőnapja(Dátum.Value);
                        DateTime hónaputolsónapja = MyF.Hónap_utolsónapja(Dátum.Value);
                        szöveg += " FROM menettábla where [bekövetkezés]>=#" + hónapelsőnapja.ToString("yyyy-MM-dd") + " 00:00:0#";
                        szöveg += " and [bekövetkezés]<#" + hónaputolsónapja.ToString("yyyy-MM-dd") + " 23:59:0#";
                        break;
                    }
            }

            List<Adat_Menetkimaradás> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);

            Tábla.ColumnCount = 12;
            Tábla.RowCount = 0;
            Tábla.Visible = false;
            // Táblázat fejléce
            Tábla.Columns[0].HeaderText = "Srsz";
            Tábla.Columns[1].HeaderText = "ABC";
            Tábla.Columns[2].HeaderText = "Visz.";
            Tábla.Columns[3].HeaderText = "Típus";
            Tábla.Columns[4].HeaderText = "Psz";
            Tábla.Columns[5].HeaderText = "Járművezetői beírás";
            Tábla.Columns[6].HeaderText = "Javítás";
            Tábla.Columns[7].HeaderText = "Idő";
            Tábla.Columns[8].HeaderText = "Menet";
            Tábla.Columns[9].HeaderText = "Törölt";
            Tábla.Columns[10].HeaderText = "Jelentés";
            Tábla.Columns[11].HeaderText = "Tétel";
            Tábla.Columns[0].Width = 55;
            Tábla.Columns[1].Width = 45;
            Tábla.Columns[2].Width = 45;
            Tábla.Columns[3].Width = 100;
            Tábla.Columns[4].Width = 50;
            Tábla.Columns[5].Width = 250;
            Tábla.Columns[6].Width = 250;
            Tábla.Columns[7].Width = 180;
            Tábla.Columns[8].Width = 60;
            Tábla.Columns[9].Width = 45;
            Tábla.Columns[10].Width = 90;
            Tábla.Columns[11].Width = 45;
            int i;
            foreach (Adat_Menetkimaradás rekord in Adatok)
            {
                Tábla.RowCount++;
                i = Tábla.RowCount - 1;
                Tábla.Rows[i].Cells[0].Value = rekord.Id;
                Tábla.Rows[i].Cells[1].Value = rekord.Eseményjele;
                Tábla.Rows[i].Cells[2].Value = rekord.Viszonylat;
                Tábla.Rows[i].Cells[3].Value = rekord.Típus;
                Tábla.Rows[i].Cells[4].Value = rekord.Azonosító;
                Tábla.Rows[i].Cells[5].Value = rekord.Jvbeírás;
                Tábla.Rows[i].Cells[6].Value = rekord.Javítás;
                Tábla.Rows[i].Cells[7].Value = rekord.Bekövetkezés;
                Tábla.Rows[i].Cells[8].Value = rekord.Kimaradtmenet;
                if (!rekord.Törölt)
                    Tábla.Rows[i].Cells[9].Value = "Aktív";
                else
                    Tábla.Rows[i].Cells[9].Value = "Törölt";
                Tábla.Rows[i].Cells[10].Value = rekord.Jelentés;
                Tábla.Rows[i].Cells[11].Value = rekord.Tétel;
            }
            Tábla.Visible = true;
        }

        private void NapiListaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Táblatörlése();
            int argmelyiket = 1;
            Táblalistázás(argmelyiket);

        }

        private void HaviLlistaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Táblatörlése();
            int argmelyiket = 2;
            Táblalistázás(argmelyiket);

        }

        private void VonalasListaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Táblatörlése();
                if (cmbtelephely1.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve telephely.");

                string hely = $@"{Application.StartupPath}\" + cmbtelephely1.Text.Trim() + @"\Adatok\főkönyv\menet" + Dátum.Value.ToString("yyyy") + ".mdb";
                string jelszó = "lilaakác";
                Kezelő_Menetkimaradás kéz = new Kezelő_Menetkimaradás();

                DateTime hónapelsőnapja = MyF.Hónap_elsőnapja(Dátum.Value);
                DateTime hónaputolsónapja = MyF.Hónap_utolsónapja(Dátum.Value);

                string szöveg = $"SELECT * FROM menettábla where [bekövetkezés]>=#{hónapelsőnapja:MM-dd-yyyy} 00:00:0#";
                szöveg += " and [bekövetkezés]<#" + hónaputolsónapja.ToString("MM-dd-yyyy") + " 23:59:0#";
                szöveg += " and [viszonylat]<> '_' and [Eseményjele]<> '_'";
                szöveg += " and [törölt]<>-1 ORDER BY viszonylat, típus, Eseményjele, Bekövetkezés";
                List<Adat_Menetkimaradás> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);

                // kiírjuk az adatokat a táblába
                Tábla.Visible = false;

                Tábla.ColumnCount = 1;
                Tábla.RowCount = 34;

                Tábla.Columns[0].HeaderText = "Nap";
                Tábla.Columns[0].Width = 45;

                //sorok kiírása
                for (int i = 1; i <= 31; i++)
                    Tábla.Rows[1 + i].Cells[0].Value = i;
                Tábla.Rows[33].Cells[0].Value = "Össz.";

                string előzőviszonylat = "";
                string előzőtípus = "";
                string[] szöveg1 = { "A db", "A menet", "B db", "B menet", "C db" };
                long a = 0;
                long b = 0;
                long c = 0;
                long am = 0;
                long bm = 0;
                int sor;
                int oszlop = 1;
                if (Adatok.Count > 0)
                {
                    foreach (Adat_Menetkimaradás rekord in Adatok)
                    {
                        // fejléc készítés
                        if (előzőviszonylat.Trim() != rekord.Viszonylat.Trim())
                        {
                            if (Tábla.ColumnCount > 5)
                            {
                                // kiirjuk az összesítést
                                Tábla.Rows[33].Cells[Tábla.ColumnCount - 5].Value = a;
                                Tábla.Rows[33].Cells[Tábla.ColumnCount - 4].Value = am;
                                Tábla.Rows[33].Cells[Tábla.ColumnCount - 3].Value = b;
                                Tábla.Rows[33].Cells[Tábla.ColumnCount - 2].Value = bm;
                                Tábla.Rows[33].Cells[Tábla.ColumnCount - 1].Value = c;
                            }
                            előzőviszonylat = rekord.Viszonylat.Trim();
                            előzőtípus = rekord.Típus.Trim();
                            a = 0;
                            b = 0;
                            c = 0;
                            am = 0;
                            bm = 0;
                            if (Tábla.ColumnCount == 1)
                                oszlop = 1;
                            else
                                oszlop = Tábla.ColumnCount;

                            Tábla.ColumnCount += 5;

                            for (int i = 0; i < 5; i++)
                            {
                                Tábla.Columns[oszlop + i].HeaderText = rekord.Viszonylat.Trim();
                                Tábla.Rows[0].Cells[oszlop + i].Value = rekord.Típus.Trim();
                                Tábla.Rows[1].Cells[oszlop + i].Value = szöveg1[i].Trim();
                            }

                        }

                        if (előzőtípus.Trim() != rekord.Típus.Trim())
                        {
                            előzőtípus = rekord.Típus.Trim();
                            if (Tábla.ColumnCount > 5)
                            {
                                // kiirjuk az összesítést
                                Tábla.Rows[33].Cells[Tábla.ColumnCount - 5].Value = a;
                                Tábla.Rows[33].Cells[Tábla.ColumnCount - 4].Value = am;
                                Tábla.Rows[33].Cells[Tábla.ColumnCount - 3].Value = b;
                                Tábla.Rows[33].Cells[Tábla.ColumnCount - 2].Value = bm;
                                Tábla.Rows[33].Cells[Tábla.ColumnCount - 1].Value = c;
                            }
                            a = 0;
                            b = 0;
                            c = 0;
                            am = 0;
                            bm = 0;
                            if (Tábla.ColumnCount == 1)
                                oszlop = 1;
                            else
                                oszlop = Tábla.ColumnCount;

                            Tábla.ColumnCount += 5;

                            for (int i = 0; i < 5; i++)
                            {
                                Tábla.Columns[oszlop + i].HeaderText = rekord.Viszonylat.Trim();
                                Tábla.Rows[0].Cells[oszlop + i].Value = rekord.Típus.Trim();
                                Tábla.Rows[1].Cells[oszlop + i].Value = szöveg1[i].Trim();
                            }
                            oszlop = Tábla.ColumnCount - 1;
                        }
                        // Adatokat kiírjuk
                        switch (rekord.Eseményjele.ToUpper())
                        {
                            case "A":
                                {
                                    a++;
                                    am += rekord.Kimaradtmenet;
                                    oszlop = Tábla.ColumnCount - 5;
                                    sor = rekord.Bekövetkezés.Day + 1;
                                    if (Tábla.Rows[sor].Cells[oszlop].Value == null || Tábla.Rows[sor].Cells[oszlop].Value.ToString().Trim() == "")
                                        Tábla.Rows[sor].Cells[oszlop].Value = 1;
                                    else
                                        Tábla.Rows[sor].Cells[oszlop].Value = int.Parse(Tábla.Rows[sor].Cells[oszlop].Value.ToString()) + 1;

                                    oszlop = Tábla.ColumnCount - 4;
                                    if (Tábla.Rows[sor].Cells[oszlop].Value == null || Tábla.Rows[sor].Cells[oszlop].Value.ToString().Trim() == "")
                                        Tábla.Rows[sor].Cells[oszlop].Value = 0;
                                    Tábla.Rows[sor].Cells[oszlop].Value = int.Parse(Tábla.Rows[sor].Cells[oszlop].Value.ToString()) + rekord.Kimaradtmenet;
                                    break;
                                }

                            case "B":
                                {
                                    b++;
                                    bm += rekord.Kimaradtmenet;
                                    oszlop = Tábla.ColumnCount - 3;
                                    sor = rekord.Bekövetkezés.Day + 1;
                                    if (Tábla.Rows[sor].Cells[oszlop].Value == null || Tábla.Rows[sor].Cells[oszlop].Value.ToString().Trim() == "")
                                        Tábla.Rows[sor].Cells[oszlop].Value = 1;
                                    else
                                        Tábla.Rows[sor].Cells[oszlop].Value = int.Parse(Tábla.Rows[sor].Cells[oszlop].Value.ToString()) + 1;

                                    oszlop = Tábla.ColumnCount - 2;
                                    if (Tábla.Rows[sor].Cells[oszlop].Value == null || Tábla.Rows[sor].Cells[oszlop].Value.ToString().Trim() == "")
                                        Tábla.Rows[sor].Cells[oszlop].Value = 0;
                                    Tábla.Rows[sor].Cells[oszlop].Value = int.Parse(Tábla.Rows[sor].Cells[oszlop].Value.ToString()) + rekord.Kimaradtmenet;
                                    break;
                                }

                            case "C":
                                {
                                    c++;
                                    oszlop = Tábla.ColumnCount - 1;
                                    sor = rekord.Bekövetkezés.Day + 1;
                                    if (Tábla.Rows[sor].Cells[oszlop].Value == null || Tábla.Rows[sor].Cells[oszlop].Value.ToString().Trim() == "")
                                        Tábla.Rows[sor].Cells[oszlop].Value = 1;
                                    else
                                        Tábla.Rows[sor].Cells[oszlop].Value = int.Parse(Tábla.Rows[sor].Cells[oszlop].Value.ToString()) + 1;
                                    break;
                                }
                        }
                    }

                    // kiirjuk az összesítést
                    Tábla.Rows[33].Cells[Tábla.ColumnCount - 5].Value = a;
                    Tábla.Rows[33].Cells[Tábla.ColumnCount - 4].Value = am;
                    Tábla.Rows[33].Cells[Tábla.ColumnCount - 5].Value = b;
                    Tábla.Rows[33].Cells[Tábla.ColumnCount - 2].Value = bm;
                    Tábla.Rows[33].Cells[Tábla.ColumnCount - 1].Value = c;
                }
                Tábla.Visible = true;

                Tábla.Columns[0].Width = 50;
                for (int i = 1; i < Tábla.ColumnCount; i++)
                    Tábla.Columns[i].Width = 75;

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

        private void HaviÖsszesítőToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                Táblatörlése();
                if (cmbtelephely1.Text == "")
                    throw new HibásBevittAdat("Nincs kiválasztva telephely.");
                long dbö = 0;
                long menetö = 0;
                long aö = 0;
                long bö = 0;
                long cö = 0;
                long amö = 0;
                long bmö = 0;
                long a = 0;
                long b = 0;
                long c = 0;
                long am = 0;
                long bm = 0;
                string előzőtípus = "";
                int oszlop;
                int sor;
                string hely = $@"{Application.StartupPath}\" + cmbtelephely1.Text.Trim() + @"\Adatok\főkönyv\menet" + Dátum.Value.ToString("yyyy") + ".mdb";
                string jelszó = "lilaakác";
                DateTime hónapelsőnapja = MyF.Hónap_elsőnapja(Dátum.Value);
                DateTime hónaputolsónapja = MyF.Hónap_utolsónapja(Dátum.Value);

                Tábla.Visible = false;

                Tábla.ColumnCount = 1;
                Tábla.RowCount = 6;

                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Jel";

                string[] szövegt = { "A", "B", "C" };
                for (int i = 0; i < 3; i++)
                    Tábla.Rows[i + 1].Cells[0].Value = szövegt[i];
                Tábla.Rows[5].Cells[0].Value = "Össz.";

                string szöveg = $"SELECT * FROM menettábla where [bekövetkezés]>=# {hónapelsőnapja:MM-dd-yyyy} 00:00:0#";
                szöveg += " and [bekövetkezés]<#" + hónaputolsónapja.ToString("MM-dd-yyyy") + " 23:59:0#";
                szöveg += " and [viszonylat]<> '_'";
                szöveg += " and [törölt]<>-1 ORDER BY típus, Eseményjele, Bekövetkezés";

                Kezelő_Menetkimaradás kéz = new Kezelő_Menetkimaradás();
                List<Adat_Menetkimaradás> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);

                foreach (Adat_Menetkimaradás rekord in Adatok)
                {
                    // fejléc készítés
                    if (előzőtípus.Trim() != rekord.Típus.Trim() && előzőtípus != "")
                    {
                        Tábla.ColumnCount += 2;
                        // ha másik típus akkor kiírja az adatokat
                        oszlop = Tábla.ColumnCount - 2;
                        sor = 0;
                        Tábla.Rows[sor].Cells[oszlop].Value = előzőtípus;
                        oszlop = Tábla.ColumnCount - 1;
                        sor = 0;
                        Tábla.Rows[sor].Cells[oszlop].Value = előzőtípus;
                        oszlop = Tábla.ColumnCount - 2;
                        sor = 1;
                        Tábla.Rows[sor].Cells[oszlop].Value = "darab";
                        oszlop = Tábla.ColumnCount - 1;
                        sor = 1;
                        Tábla.Rows[sor].Cells[oszlop].Value = "menet";
                        aö += a;
                        amö += am;
                        bö += b;
                        bmö += bm;
                        cö += c;
                        oszlop = Tábla.ColumnCount - 2;
                        sor = 2;
                        Tábla.Rows[sor].Cells[oszlop].Value = a;
                        oszlop = Tábla.ColumnCount - 1;
                        sor = 2;
                        Tábla.Rows[sor].Cells[oszlop].Value = am;
                        oszlop = Tábla.ColumnCount - 2;
                        sor = 3;
                        Tábla.Rows[sor].Cells[oszlop].Value = b;
                        oszlop = Tábla.ColumnCount - 1;
                        sor = 3;
                        Tábla.Rows[sor].Cells[oszlop].Value = bm;
                        oszlop = Tábla.ColumnCount - 2;
                        sor = 4;
                        Tábla.Rows[sor].Cells[oszlop].Value = c;
                        dbö = dbö + a + b + c;
                        menetö = menetö + am + bm;
                        oszlop = Tábla.ColumnCount - 2;
                        sor = 5;
                        Tábla.Rows[sor].Cells[oszlop].Value = a + b + c;
                        oszlop = Tábla.ColumnCount - 1;
                        sor = 5;
                        Tábla.Rows[sor].Cells[oszlop].Value = am + bm;

                        a = 0;
                        b = 0;
                        c = 0;
                        am = 0;
                        bm = 0;

                    }
                    előzőtípus = rekord.Típus.Trim();
                    // Adatokat kiírjuk
                    switch (rekord.Eseményjele.Trim())
                    {
                        case "A":
                            {
                                a++;
                                am += rekord.Kimaradtmenet;
                                break;
                            }
                        case "B":
                            {
                                b++;
                                bm += rekord.Kimaradtmenet;
                                break;
                            }
                        case "C":
                            {
                                c++;
                                break;
                            }
                    }

                }


                Tábla.ColumnCount += 2;
                oszlop = Tábla.ColumnCount - 2;
                sor = 0;
                Tábla.Rows[sor].Cells[oszlop].Value = előzőtípus;
                oszlop = Tábla.ColumnCount - 1;
                sor = 0;
                Tábla.Rows[sor].Cells[oszlop].Value = előzőtípus;
                oszlop = Tábla.ColumnCount - 2;
                sor = 1;
                Tábla.Rows[sor].Cells[oszlop].Value = "darab";
                oszlop = Tábla.ColumnCount - 1;
                sor = 1;
                Tábla.Rows[sor].Cells[oszlop].Value = "menet";
                oszlop = Tábla.ColumnCount - 2;
                sor = 2;
                Tábla.Rows[sor].Cells[oszlop].Value = a;
                oszlop = Tábla.ColumnCount - 1;
                sor = 2;
                Tábla.Rows[sor].Cells[oszlop].Value = am;
                oszlop = Tábla.ColumnCount - 2;
                sor = 3;
                Tábla.Rows[sor].Cells[oszlop].Value = b;
                oszlop = Tábla.ColumnCount - 1;
                sor = 3;
                Tábla.Rows[sor].Cells[oszlop].Value = bm;
                oszlop = Tábla.ColumnCount - 2;
                sor = 4;
                Tábla.Rows[sor].Cells[oszlop].Value = c;
                aö += a;
                amö += am;
                bö += b;
                bmö += bm;
                cö += c;
                dbö = dbö + a + b + c;
                menetö = menetö + am + bm;
                oszlop = Tábla.ColumnCount - 2;
                sor = 5;
                Tábla.Rows[sor].Cells[oszlop].Value = a + b + c;
                oszlop = Tábla.ColumnCount - 1;
                sor = 5;
                Tábla.Rows[sor].Cells[oszlop].Value = am + bm;
                Tábla.ColumnCount += 2;
                oszlop = Tábla.ColumnCount - 2;
                sor = 0;
                Tábla.Rows[sor].Cells[oszlop].Value = "Összesen:";
                oszlop = Tábla.ColumnCount - 1;
                sor = 0;
                Tábla.Rows[sor].Cells[oszlop].Value = "Összesen:";
                oszlop = Tábla.ColumnCount - 2;
                sor = 1;
                Tábla.Rows[sor].Cells[oszlop].Value = "darab";
                oszlop = Tábla.ColumnCount - 1;
                sor = 1;
                Tábla.Rows[sor].Cells[oszlop].Value = "menet";
                oszlop = Tábla.ColumnCount - 2;
                sor = 2;
                Tábla.Rows[sor].Cells[oszlop].Value = aö;
                oszlop = Tábla.ColumnCount - 1;
                sor = 2;
                Tábla.Rows[sor].Cells[oszlop].Value = amö;
                oszlop = Tábla.ColumnCount - 2;
                sor = 3;
                Tábla.Rows[sor].Cells[oszlop].Value = bö;
                oszlop = Tábla.ColumnCount - 1;
                sor = 3;
                Tábla.Rows[sor].Cells[oszlop].Value = bmö;
                oszlop = Tábla.ColumnCount - 2;
                sor = 4;
                Tábla.Rows[sor].Cells[oszlop].Value = cö;
                oszlop = Tábla.ColumnCount - 2;
                sor = 5;
                Tábla.Rows[sor].Cells[oszlop].Value = dbö;
                oszlop = Tábla.ColumnCount - 1;
                sor = 5;
                Tábla.Rows[sor].Cells[oszlop].Value = menetö;
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

        private void Pályaszámok_Click(object sender, EventArgs e)
        {
            PSZtáblalistázás();
        }

        private void Pályaszámok_TextChanged(object sender, EventArgs e)
        {
            PSZtáblalistázás();
        }

        private void PSZtáblalistázás()
        {
            try
            {

                if (Pályaszámok.Text == "") return;
                if (cmbtelephely1.Text == "") throw new HibásBevittAdat("Nincs választva telephely, így a listázás nem lehetséges.");

                Táblatörlése();
                // telephelyi adatok
                string hely = $@"{Application.StartupPath}\" + cmbtelephely1.Text.Trim() + @"\Adatok\főkönyv\menet" + Dátum.Value.ToString("yyyy") + ".mdb";

                string jelszó = "lilaakác";

                string szöveg = $"SELECT * FROM menettábla where  azonosító='{Pályaszámok.Text.Trim()}' order by bekövetkezés desc";

                Kezelő_Menetkimaradás kéz = new Kezelő_Menetkimaradás();
                List<Adat_Menetkimaradás> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);

                Tábla.ColumnCount = 12;
                Tábla.RowCount = 0;
                Tábla.Visible = false;
                // Táblázat fejléce
                Tábla.Columns[0].HeaderText = "Srsz";
                Tábla.Columns[1].HeaderText = "ABC";
                Tábla.Columns[2].HeaderText = "Visz.";
                Tábla.Columns[3].HeaderText = "Típus";
                Tábla.Columns[4].HeaderText = "Psz";
                Tábla.Columns[5].HeaderText = "Járművezetői beírás";
                Tábla.Columns[6].HeaderText = "Javítás";
                Tábla.Columns[7].HeaderText = "Idő";
                Tábla.Columns[8].HeaderText = "Menet";
                Tábla.Columns[9].HeaderText = "Törölt";
                Tábla.Columns[10].HeaderText = "Jelentés";
                Tábla.Columns[11].HeaderText = "Tétel";
                Tábla.Columns[0].Width = 45;
                Tábla.Columns[1].Width = 45;
                Tábla.Columns[2].Width = 45;
                Tábla.Columns[3].Width = 100;
                Tábla.Columns[4].Width = 50;
                Tábla.Columns[5].Width = 250;
                Tábla.Columns[6].Width = 250;
                Tábla.Columns[7].Width = 150;
                Tábla.Columns[8].Width = 60;
                Tábla.Columns[9].Width = 45;
                Tábla.Columns[10].Width = 70;
                Tábla.Columns[11].Width = 45;
                int i;
                foreach (Adat_Menetkimaradás rekord in Adatok)
                {
                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = rekord.Id;
                    Tábla.Rows[i].Cells[1].Value = rekord.Eseményjele;
                    Tábla.Rows[i].Cells[2].Value = rekord.Viszonylat;
                    Tábla.Rows[i].Cells[3].Value = rekord.Típus;
                    Tábla.Rows[i].Cells[4].Value = rekord.Azonosító;
                    Tábla.Rows[i].Cells[5].Value = rekord.Jvbeírás;
                    Tábla.Rows[i].Cells[6].Value = rekord.Javítás;
                    Tábla.Rows[i].Cells[7].Value = rekord.Bekövetkezés;
                    Tábla.Rows[i].Cells[8].Value = rekord.Kimaradtmenet;
                    if (rekord.Törölt == false)
                        Tábla.Rows[i].Cells[9].Value = "Aktív";
                    else
                        Tábla.Rows[i].Cells[9].Value = "Törölt";
                    Tábla.Rows[i].Cells[10].Value = rekord.Jelentés;
                    Tábla.Rows[i].Cells[11].Value = rekord.Tétel;
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
        #endregion


        #region Gombok

        private void Button1_Click(object sender, EventArgs e)
        {
            Button1.Visible = false;
            alsópanels1.Text = 1.ToString();
            Szolgálatválasztó();
            Excelbeíró();
            if (Html_szöveg.Trim() == "")
                Button4.Visible = false;
            else
                Button4.Visible = true;
            Button1.Visible = true;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Button2.Visible = false;
            alsópanels1.Text = 2.ToString();
            Szolgálatválasztó();
            Excelbeíró();
            if (Html_szöveg.Trim() == "")
                Button4.Visible = false;
            else
                Button4.Visible = true;
            Button2.Visible = true;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Button3.Visible = false;
            alsópanels1.Text = 3.ToString();
            Szolgálatválasztó();
            Excelbeíró();
            if (Html_szöveg.Trim() == "")
                Button4.Visible = false;
            else
                Button4.Visible = true;
            Button3.Visible = true;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Email();
        }

        private void Email()
        {
            try
            {
                Kezelő_Kiegészítő_Adatok_Terjesztés kéz = new Kezelő_Kiegészítő_Adatok_Terjesztés();
                List<Adat_Kiegészítő_Adatok_Terjesztés> Adatok = kéz.Lista_Adatok();

                string email = (from a in Adatok
                                where a.Id == Convert.ToInt32(alsópanels1.Text.Trim())
                                select a.Email).FirstOrDefault();
                if (email != null)
                {
                    MyO._Application _app = new MyO.Application();
                    MyO.MailItem mail = (MyO.MailItem)_app.CreateItem(MyO.OlItemType.olMailItem);
                    // címzett
                    mail.To = email;
                    // üzenet tárgya
                    mail.Subject = "Események " + DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
                    // üzent szövege
                    mail.HTMLBody = Html_szöveg;
                    mail.Importance = MyO.OlImportance.olImportanceNormal;
                    mail.Attachments.Add(alsópanels2.Text);
                    ((MyO._MailItem)mail).Send();

                    MessageBox.Show("Üzenet el lett küldve", "Üzenet küldés sikeres", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Nem található a megadott ID-hoz tartozó rekord.", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void Excelbeíró()
        {
            // beolvassuk az elérési utat
            try
            {
                string helyideig = alsópanels2.Text;
                if (!Exists(helyideig)) throw new HibásBevittAdat("Nem létezik az elérési út/ vagy az Excel tábla.");

                Html_szöveg = "<html><body>";
                // ha létezik, akkor benyitjuk az excel táblát.
                Holtart.Be(10);

                MyE.ExcelMegnyitás(helyideig);

                int vége = 0;

                // hány oszlopból áll
                Holtart.Lép();
                int oszlopmax = 0;
                int i = 1;
                while (vége == 0)
                {
                    i++;
                    if (MyE.Beolvas(MyE.Oszlopnév(i) + "1") == "_")
                    {
                        vége = 1;
                        oszlopmax = i - 1;
                    }
                }


                Holtart.Lép();
                i = 1;
                int szám = 0;
                vége = 0;
                while (vége == 0)
                {
                    i++;
                    if (MyE.Beolvas("a" + i.ToString()).ToUpper() == "X")
                    {
                        vége = 1;
                        szám = i;
                    }
                }
                // töröljük az utolsó hogy melyik dátum volt az utolsó
                MyE.Kiir("", "a" + szám.ToString());
                string szöveg1;
                string szöveg2;
                string szöveg_html;
                DateTime utolsónap = DateTime.Parse(MyE.Beolvas("b" + szám.ToString()));
                i = 1;
                Holtart.Lép();
                Kezelő_Menetkimaradás kéz = new Kezelő_Menetkimaradás();
                List<Adat_Menetkimaradás> Adatok;

                while (utolsónap.ToString("MM/dd/yyyy") != DateTime.Today.AddDays(-1).ToString("MM/dd/yyyy"))
                {
                    Holtart.Lép();
                    utolsónap = utolsónap.AddDays(1);
                    szám++;
                    Html_szöveg += $"<p>{utolsónap:yyyy.MM.dd}</p> ";
                    MyE.Kiir(utolsónap.ToString("yyyy.MM.dd"), "b" + szám.ToString());
                    MyE.Kiir(utolsónap.ToString("ddd"), "c" + szám.ToString());
                    for (int j = 4; j <= oszlopmax; j++)
                    {
                        Holtart.Lép();
                        string telep = MyE.Beolvas(MyE.Oszlopnév(j) + "1").Trim();

                        string hely = $@"{Application.StartupPath}\{telep.Trim()}\Adatok\főkönyv\menet{Dátum.Value.Year}.mdb";
                        if (Exists(hely))
                        {
                            szöveg1 = "";
                            string szöveg = "SELECT * FROM menettábla where [bekövetkezés]>=#" + utolsónap.ToString("M-d-yy") + " 00:00:0#";
                            szöveg += " and [bekövetkezés]<=#" + utolsónap.ToString("M-d-yy") + " 23:59:0#";
                            szöveg += " and eseményjele<>'_' order by eseményjele, típus";
                            string jelszó = "lilaakác";

                            Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);

                            szöveg_html = "<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 12pt'>";
                            szöveg_html += $"<tr><td style='background-color: #B8DBFD;border: 1px solid #ccc'>{telep}</td></tr>";

                            if (Adatok.Count != 0)
                            {
                                //Fejléc
                                szöveg_html += "<tr><th style='background-color: #B8DBFD;border: 1px solid #ccc'>Jel</th>";
                                szöveg_html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Viszonylat</th>";
                                szöveg_html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Típus</th>";
                                szöveg_html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Psz</th>";
                                szöveg_html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Járművezetői beírás</th>";
                                szöveg_html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Javítás</th>";
                                szöveg_html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Menet</th></tr>";
                                foreach (Adat_Menetkimaradás rekord in Adatok)
                                {
                                    szöveg1 += " " + rekord.Eseményjele.Trim();
                                    szöveg1 += " " + rekord.Viszonylat.Trim();
                                    szöveg1 += " " + rekord.Típus.Trim();
                                    szöveg1 += " " + rekord.Azonosító.Trim();
                                    szöveg1 += " " + rekord.Jvbeírás.Trim();
                                    szöveg1 += " - " + rekord.Javítás.Trim();
                                    szöveg1 += " " + rekord.Kimaradtmenet.ToString() + " menet\n";

                                    szöveg_html += $"<tr><td style='border: 1px solid #ccc'>{rekord.Eseményjele.Trim()}</td>" +
                                                 $"<td style='border: 1px solid #ccc'>{rekord.Viszonylat.Trim()}</td>" +
                                                 $"<td style='border: 1px solid #ccc'>{rekord.Típus.Trim()}</td>" +
                                                 $"<td style='border: 1px solid #ccc'>{rekord.Azonosító.Trim()}</td>" +
                                                 $"<td style='border: 1px solid #ccc'>{rekord.Jvbeírás.Trim()}</td>" +
                                                 $"<td style='border: 1px solid #ccc'>{rekord.Javítás.Trim()}</td>" +
                                                 $"<td style='border: 1px solid #ccc'>{rekord.Kimaradtmenet}</td></tr>";
                                }
                            }
                            else
                            {
                                szöveg1 += "OK";
                                szöveg_html += $"<tr><td style='border: 1px solid #ccc'> OK </td></tr>";
                            }
                            szöveg_html += "</table>";
                            Html_szöveg += szöveg_html;

                            szöveg2 = MyE.Beolvas(MyE.Oszlopnév(j) + $"{szám}");
                            if (szöveg2.Trim() != "_")
                                szöveg1 = szöveg2 + "\n" + szöveg1;
                            MyE.Kiir(szöveg1, MyE.Oszlopnév(j) + szám.ToString());
                        }
                    }
                }
                MyE.Kiir("X", "a" + szám.ToString());
                MyE.Kiir(szám.ToString(), "aa1");
                MyE.Aktív_Cella("Munka1", "A" + szám.ToString());
                MyE.ExcelMentés();
                MyE.ExcelBezárás();
                Html_szöveg += "</body></html>";

                Holtart.Ki();
                Module_Excel.Megnyitás(helyideig);
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


        #region MindenEgyéb
        private void Dátum_ValueChanged(object sender, EventArgs e)
        {
            if (cmbtelephely.Text.Trim() == "")
                return;
            // leellenőrizzük, hogy létezik-e a feltölteni kívánt adat helye
            string hely = $@"{Application.StartupPath}\{cmbtelephely.Text.Trim()}\Adatok\főkönyv\menet" + Dátum.Value.ToString("yyyy") + ".mdb";
            if (!Exists(hely))
                Adatbázis_Létrehozás.Menekimaradás_telephely(hely);

            Pályaszámokfeltöltése();
        }

        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // melyik sorra kattintottunk
                if (e.RowIndex < 0) return;
                txtSorszám.Text = e.RowIndex.ToString();
                if (Tábla.Columns[0].HeaderCell.Value.ToString() == "Srsz")
                    idszám_ = int.Parse(Tábla.Rows[e.RowIndex].Cells[0].Value.ToString());
                if (Tábla.Columns.Count > 9 && Tábla.Columns[9].HeaderCell.Value.ToString() == "ID")
                    idszám_ = int.Parse(Tábla.Rows[e.RowIndex].Cells[9].Value.ToString());
                if (!cmbtelephely.Enabled)
                {
                    // telephelyi adatok
                    txthely.Text = $@"{Application.StartupPath}\{cmbtelephely.Text.Trim()}\Adatok\főkönyv\menet{Dátum.Value.Year}.mdb";
                }
                else
                {
                    // főmérnökségi adatok 
                    txthely.Text = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_menet_adatok.mdb";
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

        private void AdatRészletesMegjelenítéseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                List<Adat_Menetkimaradás> Adatok = new List<Adat_Menetkimaradás>();
                if (cmbtelephely.Enabled)
                    Adatok = KézMenet.Lista_Adatok("Főmérnökség", Dátum.Value.Year);
                else
                    Adatok = KézMenet.Lista_Adatok(cmbtelephely.Text.Trim(), Dátum.Value.Year);

                Adat_Menetkimaradás ADAT = Adatok.Where(a => a.Id == idszám_).FirstOrDefault();

                if (ADAT != null)
                {
                    Új_Ablak_Menetrögítés?.Close();
                    Új_Ablak_Menetrögítés = new Ablak_Menetrögítés(ADAT);
                    Új_Ablak_Menetrögítés.FormClosed += Ablak_Menetrögítés_FormClosed;
                    Új_Ablak_Menetrögítés.Show();
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

        private void Ablak_Menetrögítés_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Menetrögítés = null;
        }

        #endregion


        #region Oldal Panelen Lévő   
        private void Szolgálatoklista()
        {
            // szolgálatok listázása
            Lstszolgálatok.Items.Clear();
            string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő.mdb";
            string jelszó = "Mocó";
            string szöveg = "SELECT * FROM szolgálattábla order by sorszám";

            Lstszolgálatok.BeginUpdate();
            Lstszolgálatok.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "szolgálatnév"));
            Lstszolgálatok.EndUpdate();
        }

        private void Lstszolgálatok_SelectedIndexChanged(object sender, EventArgs e)
        {
            // kitöröljük az üzemek jelölését
            for (int i = 0; i < Lstüzemek.Items.Count; i++)
                Lstüzemek.SetItemChecked(i, false);

            string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő.mdb";
            string jelszó = "Mocó";
            string szöveg;

            Kezelő_Kiegészítő_Szolgálattelepei kéz = new Kezelő_Kiegészítő_Szolgálattelepei();
            List<Adat_Kiegészítő_Szolgálattelepei> Adatok;

            for (int i = 0; i < Lstszolgálatok.Items.Count; i++)
            {
                if (Lstszolgálatok.GetItemChecked(i) == true)
                {
                    szöveg = $"SELECT * FROM szolgálattelepeitábla where szolgálatnév='{Lstszolgálatok.Items[i].ToString().Trim()}'";
                    Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);
                    foreach (Adat_Kiegészítő_Szolgálattelepei rekord in Adatok)
                    {
                        for (int j = 0; j < Lstüzemek.Items.Count; j++)
                        {
                            if (rekord.Telephelynév.Trim() == Lstüzemek.Items[j].ToString().Trim())
                                Lstüzemek.SetItemChecked(j, true);
                        }
                    }
                }
            }
        }

        private void BtnNapilista_Click(object sender, EventArgs e)
        {
            try
            {
                Táblatörlése();
                bool volt = false;
                // ha volt kijelölve akkor végre hajtja

                for (int ii = 0; ii < Lstüzemek.Items.Count; ii++)
                {
                    if (Lstüzemek.GetItemChecked(ii) == true)
                    {
                        volt = true;
                        break;
                    }
                }
                if (!volt)
                    throw new HibásBevittAdat("Nincs kijelölve egy üzem sem.");


                Tábla.ColumnCount = 10;
                Tábla.RowCount = 0;
                Tábla.Visible = false;

                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Telephely";
                Tábla.Columns[0].Width = 110;
                Tábla.Columns[1].HeaderText = "ABC";
                Tábla.Columns[1].Width = 45;
                Tábla.Columns[2].HeaderText = "Visz.";
                Tábla.Columns[2].Width = 45;
                Tábla.Columns[3].HeaderText = "Típus";
                Tábla.Columns[3].Width = 100;
                Tábla.Columns[4].HeaderText = "Psz";
                Tábla.Columns[4].Width = 60;
                Tábla.Columns[5].HeaderText = "Járművezetői beírás";
                Tábla.Columns[5].Width = 250;
                Tábla.Columns[6].HeaderText = "Javítás";
                Tábla.Columns[6].Width = 250;
                Tábla.Columns[7].HeaderText = "Menet";
                Tábla.Columns[7].Width = 60;
                Tábla.Columns[8].HeaderText = "Bekövetkezés";
                Tábla.Columns[8].Width = 180;
                Tábla.Columns[9].HeaderText = "ID";
                Tábla.Columns[9].Width = 80;
                int i;
                Kezelő_Menetkimaradás kéz = new Kezelő_Menetkimaradás();
                List<Adat_Menetkimaradás> Adatok;

                for (int j = 0; j < Lstüzemek.Items.Count; j++)
                {
                    if (Lstüzemek.GetItemChecked(j) == true)
                    {
                        string hely = $@"{Application.StartupPath}\" + Lstüzemek.Items[j].ToString().Trim() + @"\Adatok\főkönyv\menet" + Dátum.Value.ToString("yyyy") + ".mdb";
                        if (File.Exists(hely))
                        {
                            string jelszó = "lilaakác";
                            string szöveg = "SELECT * FROM menettábla where [bekövetkezés]>=#" + Dátum.Value.ToString("M-d-yy") + " 00:00:0#";
                            szöveg += " and [bekövetkezés]<=#" + Dátum.Value.ToString("M-d-yy") + " 23:59:0#";
                            if (CheckBox1.Checked == true)
                                szöveg += " and eseményjele<>'_' ";

                            szöveg += " order by eseményjele, típus";

                            Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);

                            foreach (Adat_Menetkimaradás rekord in Adatok)
                            {

                                Tábla.RowCount++;
                                i = Tábla.RowCount - 1;
                                Tábla.Rows[i].Cells[0].Value = Lstüzemek.Items[j];
                                Tábla.Rows[i].Cells[1].Value = rekord.Eseményjele;
                                Tábla.Rows[i].Cells[2].Value = rekord.Viszonylat;
                                Tábla.Rows[i].Cells[3].Value = rekord.Típus;
                                Tábla.Rows[i].Cells[4].Value = rekord.Azonosító;
                                Tábla.Rows[i].Cells[5].Value = rekord.Jvbeírás;
                                Tábla.Rows[i].Cells[6].Value = rekord.Javítás;
                                Tábla.Rows[i].Cells[7].Value = rekord.Kimaradtmenet;
                                Tábla.Rows[i].Cells[8].Value = rekord.Bekövetkezés;
                                Tábla.Rows[i].Cells[9].Value = rekord.Id;
                            }
                        }
                    }
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

        private void Szolgálatválasztó()
        {
            try
            {
                if (!int.TryParse(alsópanels1.Text, out int ID)) throw new HibásBevittAdat("Nincs megfelelő adat");
                // Kiolvassuk az excel fájl helyét

                Kezelő_Kiegészítő_Adatok_Terjesztés kéz = new Kezelő_Kiegészítő_Adatok_Terjesztés();
                List<Adat_Kiegészítő_Adatok_Terjesztés> Adatok = kéz.Lista_Adatok();

                string rekordszöveg = (from a in Adatok
                                       where a.Id == ID
                                       select a.Szöveg).FirstOrDefault().Trim();
                if (rekordszöveg != null) alsópanels2.Text = rekordszöveg;
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


        #region Főmérnökségi panel
        private void FőmérnökségiLekérdezésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Menetkimaradás_Kiegészítő == null)
            {
                Új_Ablak_Menetkimaradás_Kiegészítő = new Ablak_Menetkimaradás_Kiegészítő();
                Új_Ablak_Menetkimaradás_Kiegészítő.FormClosed += Ablak_Menetkimaradás_Kiegészítő_FormClosed;

                Új_Ablak_Menetkimaradás_Kiegészítő.Show();
            }
            else
            {
                Új_Ablak_Menetkimaradás_Kiegészítő.Activate();
                Új_Ablak_Menetkimaradás_Kiegészítő.WindowState = FormWindowState.Normal;
            }
        }

        private void Ablak_Menetkimaradás_Kiegészítő_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Menetkimaradás_Kiegészítő = null;
        }

        #endregion
    }
}