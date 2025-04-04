using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using static Villamos.Főkönyv_Funkciók;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    public partial class Ablak_Napiadatok
    {

        public Ablak_Napiadatok()
        {
            InitializeComponent();
        }

        string AlsóPanels1;
        string AlsóPanels2;

        private void Ablak_Napiadatok_Load(object sender, EventArgs e)
        {
            try
            {
                Telephelyekfeltöltése();

                KeyPreview = true;
                Dátum.Value = DateTime.Today;
                Dátum.MaxDate = DateTime.Today;

                Táblaalaphelyzet();

                // megnézzük, hogy létezik-e az éves tábla fájl
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\kiadás" + Dátum.Value.ToString("yyyy") + ".mdb";
                if (!File.Exists(hely))
                    Adatbázis_Létrehozás.Kiadásiösszesítőtábla(hely);

                // megnézzük, hogy létezik-e az éves tábla fájl
                hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\személyzet" + Dátum.Value.ToString("yyyy") + ".mdb";
                if (!File.Exists(hely))
                    Adatbázis_Létrehozás.Személyzetösszesítőtábla(hely);

                // megnézzük, hogy létezik-e az éves tábla fájl
                hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\típuscsere" + Dátum.Value.ToString("yyyy") + ".mdb";
                if (!File.Exists(hely))
                    Adatbázis_Létrehozás.Tipuscsereösszesítőtábla(hely);

                // xnapos tábla
                hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\hibanapló\Elkészült" + Dátum.Value.ToString("yyyy") + ".mdb";
                if (!File.Exists(hely))
                    Adatbázis_Létrehozás.Javításiátfutástábla(hely);

                // napi állók
                // xnapos tábla
                hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\hibanapló\Napi.mdb";

                if (!File.Exists(hely))
                    Adatbázis_Létrehozás.Javításiátfutástábla(hely);

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


        #region Alap
        private void BtnSúgó_Click(object sender, EventArgs e)
        {

            string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Napiadatok.html";
            Module_Excel.Megnyitás(hely);

        }


        private void Telephelyekfeltöltése()
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
                if (Tábla.Visible == true & Tábla.Rows.Count <= 0)
                    return;
                if (Tábla1.Visible == true & Tábla1.Rows.Count <= 0)
                    return;
                if (Tábla2.Visible == true & Tábla2.Rows.Count <= 0)
                    return;

                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog1.InitialDirectory = "MyDocuments";

                SaveFileDialog1.Title = "Listázott tartalom mentése Excel fájlba";
                SaveFileDialog1.FileName = "Kiadási_Javítási_adatok_" + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMdd");
                SaveFileDialog1.Filter = "Excel |*.xlsx";
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);


                if (Tábla.Visible == true)
                    Module_Excel.EXCELtábla(fájlexc, Tábla, false);
                else if (Tábla1.Visible == true)
                    Module_Excel.EXCELtábla(fájlexc, Tábla1, false);
                else if (Tábla2.Visible == true)
                    Module_Excel.EXCELtábla(fájlexc, Tábla2, false);

                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Module_Excel.Megnyitás(fájlexc + ".xlsx");
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
            AlsóPanels1 = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\hibanapló\napi.mdb";
            AlsóPanels2 = "minden";
            Napihibalista();
        }

        private void Napihibalista()
        {
            try
            {
                string hely = AlsóPanels1;
                string jelszó = "plédke";
                int hónapnaputolsónapja = DateTime.DaysInMonth(Dátum.Value.Year, Dátum.Value.Month);
                DateTime hónaputolsónapja = new DateTime(Dátum.Value.Year, Dátum.Value.Month, hónapnaputolsónapja);
                DateTime hónapelsőnapja = new DateTime(Dátum.Value.Year, Dátum.Value.Month, 1);
                string szöveg = "SELECT * FROM xnapostábla ";
                if (AlsóPanels2.Trim() == "napiálló")
                {
                    szöveg += " WHERE kezdődátum>= #" + Dátum.Value.ToString("MM-dd-yyyy") + " 00:00:00" + "#";
                    szöveg += " AND kezdődátum<=  #" + Dátum.Value.ToString("MM-dd-yyyy") + " 23:59:59" + "#";
                }
                if (AlsóPanels2.Trim() == "elkészült")
                {
                    szöveg += " WHERE végdátum>= #" + Dátum.Value.ToString("MM-dd-yyyy") + " 00:00:00" + "#";
                    szöveg += " AND végdátum<= #" + Dátum.Value.ToString("MM-dd-yyyy") + " 23:59:59" + "#";
                }
                if (AlsóPanels2.Trim() == "havikészült")
                {
                    szöveg += " WHERE  [végdátum]>=#" + hónapelsőnapja.ToString("MM-dd-yyyy") + " 00:00:00" + "#";
                    szöveg += " and [végdátum]<=#" + hónaputolsónapja.ToString("MM-dd-yyyy") + " 23:59:59#";
                }
                szöveg += " ORDER BY azonosító";

                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 5;

                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Azonosító";
                Tábla.Columns[0].Width = 100;
                Tábla.Columns[1].HeaderText = "Kezdő dátum";
                Tábla.Columns[1].Width = 120;
                Tábla.Columns[2].HeaderText = "Végső dátum";
                Tábla.Columns[2].Width = 120;
                Tábla.Columns[3].HeaderText = "Állási napok";
                Tábla.Columns[3].Width = 120;
                Tábla.Columns[4].HeaderText = "Hiba leírása";
                Tábla.Columns[4].Width = 400;

                Kezelő_Jármű_Javításiátfutástábla KJJ_kéz = new Kezelő_Jármű_Javításiátfutástábla();
                List<Adat_Jármű_Javításiátfutástábla> Adatok = KJJ_kéz.Lista_adatok(hely, jelszó, szöveg);
                int i;
                foreach (Adat_Jármű_Javításiátfutástábla rekord in Adatok)
                {
                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;

                    Tábla.Rows[i].Cells[0].Value = rekord.Azonosító.Trim();
                    Tábla.Rows[i].Cells[1].Value = rekord.Kezdődátum.ToString("yyyy.MM.dd");
                    if (rekord.Végdátum.ToString("yyyy.MM.dd") != (new DateTime(1900, 1, 1)).ToString("yyyy.MM.dd"))
                        Tábla.Rows[i].Cells[2].Value = rekord.Végdátum.ToString("yyyy.MM.dd");
                    // nincs vég dátum annál ami áll
                    TimeSpan delta;
                    if (rekord.Végdátum.ToString("yyyy.MM.dd") == (new DateTime(1900, 1, 1)).ToString("yyyy.MM.dd"))
                    {
                        delta = DateTime.Today - rekord.Kezdődátum;
                        Tábla.Rows[i].Cells[3].Value = (int)delta.TotalDays;
                    }
                    else
                    {
                        delta = DateTime.Today - rekord.Végdátum;
                        Tábla.Rows[i].Cells[3].Value = (int)delta.TotalDays;
                    }
                    Tábla.Rows[i].Cells[4].Value = rekord.Hibaleírása.Trim();
                }
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

        #endregion


        #region Napi kiadási adatok
        private void Lista_Click(object sender, EventArgs e)
        {
            Napi_kiadási_adatok();
        }


        private void Napi_kiadási_adatok()
        {
            AlsóPanels2 = "minden";

            // megnézzük, hogy létezik-e az éves tábla fájl
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\kiadás" + Dátum.Value.ToString("yyyy") + ".mdb";
            if (!File.Exists(hely))
                Adatbázis_Létrehozás.Kiadásiösszesítőtábla(hely);

            Táblázatlistázás();
            Táblázatlistázásszemélyzet();
            Táblázatlistázástípuscsere();
            Napi_adatok_felirat();
        }


        private void Táblázatlistázás()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\kiadás" + Dátum.Value.ToString("yyyy") + ".mdb";
                string jelszó = "plédke";
                string szöveg = "SELECT * FROM tábla where [dátum]=#" + Dátum.Value.ToString("MM-dd-yyyy") + "# ORDER BY napszak, típus";

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

                Kezelő_Kiadás_Összesítő KKö_kéz = new Kezelő_Kiadás_Összesítő();
                List<Adat_Kiadás_összesítő> Adatok = KKö_kéz.Lista_adatok(hely, jelszó, szöveg);
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
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_fortekiadási_adatok.mdb";
                string jelszó = "gémkapocs";
                if (!File.Exists(hely)) return;

                string szöveg = "SELECT  * FROM fortekiadástábla";
                Kezelő_Forte_Kiadási_Adatok Kéz = new Kezelő_Forte_Kiadási_Adatok();
                List<Adat_Forte_Kiadási_Adatok> Adatok = Kéz.Lista_adatok(hely, jelszó, szöveg);

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
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\személyzet" + Dátum.Value.ToString("yyyy") + ".mdb";
                string jelszó = "plédke";
                string szöveg = "SELECT * FROM tábla where [dátum]=#" + Dátum.Value.ToString("MM-dd-yyyy") + "# ORDER BY napszak,típus";

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

                Kezelő_Főkönyv_Személyzet KFK_Kéz = new Kezelő_Főkönyv_Személyzet();
                List<Adat_Főkönyv_Személyzet> Adatok = KFK_Kéz.Lista_adatok(hely, jelszó, szöveg);

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
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\típuscsere" + Dátum.Value.ToString("yyyy") + ".mdb";
                string jelszó = "plédke";
                string szöveg = "SELECT * FROM típuscseretábla where [dátum]=#" + Dátum.Value.ToString("MM-dd-yyyy") + "#";
                szöveg += " order by napszak, típuselőírt,viszonylat,forgalmiszám";

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

                Kezelő_Főkönyv_Típuscsere KFT_kéz = new Kezelő_Főkönyv_Típuscsere();
                List<Adat_FőKönyv_Típuscsere> Adatok = KFT_kéz.Lista_adatok(hely, jelszó, szöveg);
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
            AlsóPanels1 = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\hibanapló\Elkészült" + Dátum.Value.ToString("yyyy") + ".mdb";
            AlsóPanels2 = "napiálló";
            Napihibalista();
        }

        #endregion


        #region Havi adatok

        private void Havilista_Click(object sender, EventArgs e)
        {
            try
            {
                Tábla.Visible = false;
                AlsóPanels2 = "havilista";
                int hónapnaputolsónapja = DateTime.DaysInMonth(Dátum.Value.Year, Dátum.Value.Month);
                DateTime hónaputolsónapja = new DateTime(Dátum.Value.Year, Dátum.Value.Month, hónapnaputolsónapja);
                DateTime hónapelsőnapja = new DateTime(Dátum.Value.Year, Dátum.Value.Month, 1);

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\kiadás" + Dátum.Value.ToString("yyyy") + ".mdb";
                string jelszó = "plédke";
                string szöveg = "SELECT * FROM tábla where [dátum]>=#" + hónapelsőnapja.ToString("MM-dd-yyyy") + " 00:00:00#";
                szöveg += " and [dátum]<=#" + hónaputolsónapja.ToString("MM-dd-yyyy") + " 23:59:59#";
                szöveg += " order by dátum,napszak, típus";

                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();

                Tábla.ColumnCount = 10;

                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Dátum";
                Tábla.Columns[0].Width = 100;
                Tábla.Columns[1].HeaderText = "Napszak";
                Tábla.Columns[1].Width = 100;
                Tábla.Columns[2].HeaderText = "Típus";
                Tábla.Columns[2].Width = 100;
                Tábla.Columns[3].HeaderText = "Forgalomban";
                Tábla.Columns[3].Width = 100;
                Tábla.Columns[4].HeaderText = "Tartalék";
                Tábla.Columns[4].Width = 100;
                Tábla.Columns[5].HeaderText = "Kocsiszíni";
                Tábla.Columns[5].Width = 100;
                Tábla.Columns[6].HeaderText = "Félreállítás";
                Tábla.Columns[6].Width = 100;
                Tábla.Columns[7].HeaderText = "Főjavítás";
                Tábla.Columns[7].Width = 100;
                Tábla.Columns[8].HeaderText = "Összesen";
                Tábla.Columns[8].Width = 100;
                Tábla.Columns[9].HeaderText = "Személyzethiány";
                Tábla.Columns[9].Width = 200;

                Kezelő_Kiadás_Összesítő KKö_kéz = new Kezelő_Kiadás_Összesítő();
                List<Adat_Kiadás_összesítő> Adatok = KKö_kéz.Lista_adatok(hely, jelszó, szöveg);
                int i;
                foreach (Adat_Kiadás_összesítő rekord in Adatok)
                {
                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = rekord.Dátum.ToString("yyyy.MM.dd");
                    Tábla.Rows[i].Cells[1].Value = rekord.Napszak.Trim();
                    Tábla.Rows[i].Cells[2].Value = rekord.Típus.Trim();
                    Tábla.Rows[i].Cells[3].Value = rekord.Forgalomban;
                    Tábla.Rows[i].Cells[4].Value = rekord.Tartalék + rekord.Személyzet;
                    Tábla.Rows[i].Cells[5].Value = rekord.Kocsiszíni;
                    Tábla.Rows[i].Cells[6].Value = rekord.Félreállítás;
                    Tábla.Rows[i].Cells[7].Value = rekord.Főjavítás;
                    Tábla.Rows[i].Cells[8].Value = rekord.Forgalomban + rekord.Tartalék + rekord.Kocsiszíni + rekord.Félreállítás + rekord.Főjavítás + rekord.Személyzet;
                    Tábla.Rows[i].Cells[9].Value = rekord.Személyzet;
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


        private void Havielkészültkocsik_Click(object sender, EventArgs e)
        {
            SUBnapihibagöngyölés(Cmbtelephely.Text.Trim());
            SUBNapielkészültek(Dátum.Value, Cmbtelephely.Text.Trim());
            AlsóPanels1 = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\hibanapló\Elkészült" + Dátum.Value.ToString("yyyy") + ".mdb";
            AlsóPanels2 = "havikészült";
            Napihibalista();
        }


        private void Haviszemélyzethiány_Click(object sender, EventArgs e)
        {
            try
            {
                AlsóPanels2 = "haviszem";
                int hónapnaputolsónapja = DateTime.DaysInMonth(Dátum.Value.Year, Dátum.Value.Month);
                DateTime hónaputolsónapja = new DateTime(Dátum.Value.Year, Dátum.Value.Month, hónapnaputolsónapja);
                DateTime hónapelsőnapja = new DateTime(Dátum.Value.Year, Dátum.Value.Month, 1);

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\személyzet" + Dátum.Value.ToString("yyyy") + ".mdb";
                string jelszó = "plédke";
                string szöveg = "SELECT * FROM tábla where [dátum]>=#" + hónapelsőnapja.ToString("MM-dd-yyyy") + " 00:00:00#";
                szöveg += " and [dátum]<=#" + hónaputolsónapja.ToString("MM-dd-yyyy") + " 23:59:59#";
                szöveg += " order by dátum,napszak, típus";


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


                Kezelő_Főkönyv_Személyzet KFK_Kéz = new Kezelő_Főkönyv_Személyzet();
                List<Adat_Főkönyv_Személyzet> Adatok = KFK_Kéz.Lista_adatok(hely, jelszó, szöveg);

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
                AlsóPanels2 = "haviszem";
                int hónapnaputolsónapja = DateTime.DaysInMonth(Dátum.Value.Year, Dátum.Value.Month);
                DateTime hónaputolsónapja = new DateTime(Dátum.Value.Year, Dátum.Value.Month, hónapnaputolsónapja);
                DateTime hónapelsőnapja = new DateTime(Dátum.Value.Year, Dátum.Value.Month, 1);

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\típuscsere" + Dátum.Value.ToString("yyyy") + ".mdb";
                string jelszó = "plédke";
                string szöveg = "SELECT * FROM típuscseretábla where [dátum]>=#" + hónapelsőnapja.ToString("MM-dd-yyyy") + " 00:00:00#";
                szöveg += " and [dátum]<=#" + hónaputolsónapja.ToString("MM-dd-yyyy") + " 23:59:59#";
                szöveg += " order by dátum,napszak, típuselőírt";

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


                Kezelő_Főkönyv_Típuscsere KFT_kéz = new Kezelő_Főkönyv_Típuscsere();
                List<Adat_FőKönyv_Típuscsere> Adatok = KFT_kéz.Lista_adatok(hely, jelszó, szöveg);
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
            AlsóPanels1 = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\hibanapló\Elkészült" + Dátum.Value.ToString("yyyy") + ".mdb";
            AlsóPanels2 = "elkészült";
            Napihibalista();
        }


        private void Napikarbantartás_Click(object sender, EventArgs e)
        {
            try
            {
                string helykieg = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő.mdb";
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\hibanapló" + @"\" + Dátum.Value.ToString("yyyyMM") + "hibanapló.mdb";

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
                Kezelő_kiegészítő_Hibaterv KKH_kéz = new Kezelő_kiegészítő_Hibaterv();
                List<Adat_Kiegészítő_Hibaterv> KAdatok = KKH_kéz.Lista_Adatok(Cmbtelephely.Text.Trim());

                Kezelő_jármű_hiba KJH_kéz = new Kezelő_jármű_hiba();

                foreach (Adat_Kiegészítő_Hibaterv rekordkieg in KAdatok)
                {
                    List<Adat_Jármű_hiba> Adatok = KJH_kéz.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value);
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
            AlsóPanels2 = "minden";
            // megnézzük, hogy létezik-e az éves tábla fájl

            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\kiadás" + Dátum.Value.ToString("yyyy") + ".mdb";
            if (!File.Exists(hely))
                Adatbázis_Létrehozás.Kiadásiösszesítőtábla(hely);

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