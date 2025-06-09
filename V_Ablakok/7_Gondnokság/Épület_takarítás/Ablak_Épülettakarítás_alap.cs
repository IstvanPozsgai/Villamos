using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    public partial class Ablak_épülettakarítás_alap
    {
        readonly Kezelő_Épület_Takarítás_Osztály KézTakOsztály = new Kezelő_Épület_Takarítás_Osztály();
        readonly Kezelő_Épület_Takarítás_Adattábla KézÉptakarításAdat = new Kezelő_Épület_Takarítás_Adattábla();
        readonly Kezelő_Takarítás_Opció KézOpció = new Kezelő_Takarítás_Opció();

        List<Adat_Épület_Takarítás_Osztály> AdatokTakOsztály = new List<Adat_Épület_Takarítás_Osztály>();
        List<Adat_Épület_Takarítás_Adattábla> AdatokÉptakarításAdat = new List<Adat_Épület_Takarítás_Adattábla>();
        List<Adat_Takarítás_Opció> AdatokTakOpció = new List<Adat_Takarítás_Opció>();

        DataTable AdatTábla = new DataTable();
        public Ablak_épülettakarítás_alap()
        {
            InitializeComponent();
            Start();
        }

        #region Alap
        private void Start()
        {
            Telephelyekfeltöltése();
            // leellenőrizzük, hogy van-e adatbázis
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Épület";
            if (!System.IO.Directory.Exists(hely)) System.IO.Directory.CreateDirectory(hely);

            hely += @"\épülettörzs.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Épülettakarításlétrehozás(hely);

            Jogosultságkiosztás();
            Combofeltöltése();

        }

        private void Ablak_épülettakarítás_alap_Load(object sender, EventArgs e)
        {
            LapFülek.SelectedIndex = 0;
            Fülekkitöltése();
        }

        private void Fülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }

        private void Fülekkitöltése()
        {
            switch (LapFülek.SelectedIndex)
            {
                case 0:
                    {
                        Osztályürítés();
                        Osztálykiirás();
                        AcceptButton = Osztály_rögzít;
                        break;
                    }
                case 1:
                    {
                        Helységürítés();
                        Helységlistáz();
                        break;
                    }
                case 2:
                    {
                        break;
                    }
                case 3:
                    {
                        OpcióListaFeltöltés();
                        break;
                    }
            }
        }

        private void Jogosultságkiosztás()
        {
            int melyikelem;
            // ide kell az összes gombot tenni amit szabályozni akarunk false
            Részletes_Kuka.Enabled = false;
            Részletes_feljebb.Enabled = false;
            Részletes_rögzít.Enabled = false;
            Helység_feljebb.Enabled = false;
            Osztály_rögzít.Enabled = false;
            Osztály_törlés.Enabled = false;
            Osztály_feljebb.Enabled = false;
            Adatok_beolvasása.Enabled = false;
            Opció_OK.Enabled = false;


            melyikelem = 235;
            // módosítás 1 
            if (MyF.Vanjoga(melyikelem, 1))
            {
                Osztály_rögzít.Enabled = true;
                Osztály_törlés.Enabled = true;
                Osztály_feljebb.Enabled = true;
                Adatok_beolvasása.Enabled = true;
                Opció_OK.Enabled = true;
            }
            // módosítás 2 
            if (MyF.Vanjoga(melyikelem, 2))
            {
                Helység_feljebb.Enabled = true;
            }
            // módosítás 3
            if (MyF.Vanjoga(melyikelem, 3))
            {
                Részletes_Kuka.Enabled = true;
                Részletes_feljebb.Enabled = true;
                Részletes_rögzít.Enabled = true;
            }
        }

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    Cmbtelephely.Items.Add(Elem);
                if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér)
                { Cmbtelephely.Text = Cmbtelephely.Items[0].ToString().Trim(); }
                else if (Program.PostásTelephely.Contains("törzs"))
                { Cmbtelephely.Text = Cmbtelephely.Items[0].ToString().Trim(); }
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

        private void Btn_súgó_Click(object sender, EventArgs e)
        {
            string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Épület_törzsadatok.html";
            Module_Excel.Megnyitás(hely);
        }

        #endregion

        #region Takarítási osztály
        private void Osztályürítés()
        {
            Sorszám.Text = "";
            Osztálynév.Text = "";
            E1ár.Text = "0";
            E2ár.Text = "0";
            E3ár.Text = "0";
        }

        private void Osztálykiirás()
        {
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Épület\épülettörzs.mdb";
            if (!File.Exists(hely))
                Adatbázis_Létrehozás.Épülettakarításlétrehozás(hely);
            string jelszó = "seprűéslapát";
            // mentési helyek kiirása
            string szöveg = "SELECT * FROM takarításosztály where státus=0  order by  id";

            Tábla1.Rows.Clear();
            Tábla1.Columns.Clear();
            Tábla1.Refresh();
            Tábla1.Visible = false;
            Tábla1.ColumnCount = 5;

            // fejléc elkészítése
            Tábla1.Columns[0].HeaderText = "Sorszám";
            Tábla1.Columns[0].Width = 150;
            Tábla1.Columns[1].HeaderText = "Osztály";
            Tábla1.Columns[1].Width = 400;
            Tábla1.Columns[2].HeaderText = "E1 takarítási ár";
            Tábla1.Columns[2].Width = 200;
            Tábla1.Columns[3].HeaderText = "E2 takarítási ár";
            Tábla1.Columns[3].Width = 200;
            Tábla1.Columns[4].HeaderText = "E3 takarítási ár";
            Tábla1.Columns[4].Width = 200;

            Kezelő_Épület_Takarítás_Osztály Kéz = new Kezelő_Épület_Takarítás_Osztály();
            List<Adat_Épület_Takarítás_Osztály> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);
            int i;

            foreach (Adat_Épület_Takarítás_Osztály rekord in Adatok)
            {
                Tábla1.RowCount++;
                i = Tábla1.RowCount - 1;
                Tábla1.Rows[i].Cells[0].Value = rekord.Id;
                Tábla1.Rows[i].Cells[1].Value = rekord.Osztály.ToString();
                Tábla1.Rows[i].Cells[2].Value = rekord.E1Ft.ToString();
                Tábla1.Rows[i].Cells[3].Value = rekord.E2Ft.ToString();
                Tábla1.Rows[i].Cells[4].Value = rekord.E3Ft.ToString();
            }
            Tábla1.Visible = true;
            Tábla1.Refresh();
        }

        private void Tábla1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Sorszám.Text = Tábla1.Rows[e.RowIndex].Cells[0].Value.ToString();
            Osztálynév.Text = Tábla1.Rows[e.RowIndex].Cells[1].Value.ToString();
            E1ár.Text = Tábla1.Rows[e.RowIndex].Cells[2].Value.ToString();
            E2ár.Text = Tábla1.Rows[e.RowIndex].Cells[3].Value.ToString();
            E3ár.Text = Tábla1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void Osztály_Új_Click(object sender, EventArgs e)
        {
            Osztályürítés();
        }

        private void Osztály_rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Osztálynév.Text.Trim() == "") throw new HibásBevittAdat("Oszálynevet meg kell adni.");
                if (!double.TryParse(E1ár.Text, out double E1Ár) && E1Ár < 0) throw new HibásBevittAdat("Az E1 takarítási árnak számnak kell lennie és nem lehet negatív szám.");
                if (!double.TryParse(E2ár.Text, out double E2Ár) && E2Ár < 0) throw new HibásBevittAdat("Az E2 takarítási árnak számnak kell lennie és nem lehet negatív szám.");
                if (!double.TryParse(E3ár.Text, out double E3Ár) && E3Ár < 0) throw new HibásBevittAdat("Az E3 takarítási árnak számnak kell lennie és nem lehet negatív szám.");

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Épület\épülettörzs.mdb";
                if (!File.Exists(hely)) Adatbázis_Létrehozás.Épülettakarításlétrehozás(hely);
                string jelszó = "seprűéslapát";
                string szöveg;

                AdatokTakListázás();

                if (!int.TryParse(Sorszám.Text, out int sorszám))
                {
                    sorszám = 1;
                    if (AdatokTakOsztály.Count > 0)
                        sorszám = AdatokTakOsztály.Max(a => a.Id) + 1;
                }

                Adat_Épület_Takarítás_Osztály AdatTakOsztály = (from a in AdatokTakOsztály
                                                                where a.Id == sorszám
                                                                select a).FirstOrDefault();
                if (AdatTakOsztály != null)
                {
                    szöveg = "UPDATE takarításosztály  SET ";
                    szöveg += "osztály='" + Osztálynév.Text.Trim() + "', ";
                    szöveg += "E1Ft=" + E1ár.Text.Replace(",", ".") + ", ";
                    szöveg += "E2Ft=" + E2ár.Text.Replace(",", ".") + ", ";
                    szöveg += "E3Ft=" + E3ár.Text.Replace(",", ".");
                    szöveg += $" WHERE id={sorszám}";
                }
                else
                {

                    szöveg = "INSERT INTO takarításosztály  (id, osztály, E1Ft, E2Ft, E3Ft, státus  ) VALUES (";
                    szöveg += $"{sorszám}, ";
                    szöveg += "'" + Osztálynév.Text.Trim() + "', ";
                    szöveg += E1ár.Text.Replace(",", ".") + ", ";
                    szöveg += E2ár.Text.Replace(",", ".") + ", ";
                    szöveg += E3ár.Text.Replace(",", ".") + ", false )";
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);

                Osztályürítés();
                Osztálykiirás();
                Combofeltöltése();
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
        private void Osztálytörlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Sorszám.Text, out int sorszám)) return;
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Épület\épülettörzs.mdb";
                if (!File.Exists(hely)) Adatbázis_Létrehozás.Épülettakarításlétrehozás(hely);
                string jelszó = "seprűéslapát";
                string szöveg;

                Adat_Épület_Takarítás_Osztály AdatTakOsztály = (from a in AdatokTakOsztály
                                                                where a.Id == sorszám
                                                                select a).FirstOrDefault();

                if (AdatTakOsztály != null)
                {
                    szöveg = "UPDATE takarításosztály  SET ";
                    szöveg += "státus=true ";
                    szöveg += $" WHERE id={sorszám}";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                }
                Osztályürítés();
                Osztálykiirás();
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

        private void Felljebb_Click(object sender, EventArgs e)
        {
            try
            {
                if (Sorszám.Text.Trim() == "")
                    throw new HibásBevittAdat("Nincs kijelölve egy elem sem.");
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Épület\épülettörzs.mdb";
                if (!File.Exists(hely)) return;
                string jelszó = "seprűéslapát";

                int előző = 0;
                int választott = int.Parse(Sorszám.Text.Trim());
                Tábla1.Sort(Tábla1.Columns[0], System.ComponentModel.ListSortDirection.Ascending);

                for (int i = 0; i < Tábla1.Rows.Count; i++)
                {
                    if (Tábla1.Rows[i].Cells[0].Value.ToString().Trim() == Sorszám.Text.Trim())
                    {
                        if (i == 0)
                        {
                            // legelső
                            előző = 0;
                            return;
                        }
                        előző = int.Parse(Tábla1.Rows[i - 1].Cells[0].Value.ToString());
                        break;
                    }
                }
                if (előző == 0)
                    throw new HibásBevittAdat("A legelső elemet nem lehet előrébb helyezni a sorban.");

                Kezelő_Épület_Takarítás_Osztály Kéz = new Kezelő_Épület_Takarítás_Osztály();

                string szöveg = $"SELECT * FROM takarításosztály WHERE id={előző}";
                Adat_Épület_Takarítás_Osztály A_elem = Kéz.Egy_Adat(hely, jelszó, szöveg);

                szöveg = $"SELECT * FROM takarításosztály WHERE id={választott}";
                Adat_Épület_Takarítás_Osztály B_elem = Kéz.Egy_Adat(hely, jelszó, szöveg);

                // rögzítjük eggyel előrébb a kiválasztott elemet
                szöveg = "UPDATE takarításosztály  SET id=" + előző.ToString();
                szöveg += " WHERE ";
                szöveg += " osztály='" + B_elem.Osztály.Trim() + "'";
                MyA.ABMódosítás(hely, jelszó, szöveg);

                szöveg = "UPDATE takarításosztály  SET id=" + választott.ToString();
                szöveg += " WHERE ";
                szöveg += " osztály='" + A_elem.Osztály.Trim() + "'";
                MyA.ABMódosítás(hely, jelszó, szöveg);

                Osztályürítés();
                Osztálykiirás();
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

        private void Oszály_Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla1.Rows.Count <= 0)
                    return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Épület_osztály_" + Program.PostásNév + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                Module_Excel.EXCELtábla(fájlexc, Tábla1, false);
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


        void Adatok_beolvasása_Click(object sender, EventArgs e)
        {
            try
            {
                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Épület takarítási árak betöltése",
                    FileName = "",
                    Filter = "Excel |*.xlsx"
                };
                string fájlexc;
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;
                MyE.ExcelMegnyitás(fájlexc);
                MyE.Munkalap_aktív("Munka1");
                int sor = 2;
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Épület\épülettörzs.mdb";
                if (!File.Exists(hely)) Adatbázis_Létrehozás.Épülettakarításlétrehozás(hely);
                string jelszó = "seprűéslapát";

                List<string> SzövegGy = new List<string>();
                while (MyE.Beolvas("A" + sor.ToString()).Trim() != "_")
                {
                    string osztály = MyE.Beolvas("A" + sor.ToString()).Trim();
                    double E1 = double.TryParse(MyE.Beolvas("B" + sor.ToString()).Trim(), out double E1P) ? E1P : 0;
                    double E2 = double.TryParse(MyE.Beolvas("C" + sor.ToString()).Trim(), out double E2P) ? E2P : 0;
                    double E3 = double.TryParse(MyE.Beolvas("D" + sor.ToString()).Trim(), out double E3P) ? E3P : 0;

                    string szöveg;

                    Adat_Épület_Takarítás_Osztály AdatTakOsztály = (from a in AdatokTakOsztály
                                                                    where a.Osztály == osztály.Trim()
                                                                    select a).FirstOrDefault();

                    if (AdatTakOsztály != null)
                    {
                        szöveg = $"UPDATE takarításosztály  SET E1Ft={E1.ToString().Replace(',', '.')}, E2Ft={E2.ToString().Replace(',', '.')}, E3Ft={E3.ToString().Replace(',', '.')} ";
                        szöveg += $" WHERE osztály='{osztály.Trim()}'";
                    }
                    else
                    {
                        int sorszám = 1;
                        if (AdatokTakOsztály.Count > 0) sorszám = AdatokTakOsztály.Max(a => a.Id) + 1;
                        szöveg = "INSERT INTO takarításosztály  (id, osztály, E1Ft, E2Ft, E3Ft, státus  ) VALUES (";
                        szöveg += $"{sorszám}, '{osztály}', {E1.ToString().Replace(',', '.')}, {E2.ToString().Replace(',', '.')}, {E3.ToString().Replace(',', '.')}, false )";
                    }
                    SzövegGy.Add(szöveg);
                    sor++;
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
                MyE.ExcelBezárás();
                MessageBox.Show("Az Excel tábla feldolgozása megtörtént. !", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Osztálykiirás();

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



        private void Beviteli_táblakészítés_Click(object sender, EventArgs e)
        {
            try
            {
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Excel tábla készítés adatok beolvasásához",
                    FileName = "Beolvasó_Takarítás_" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.ExcelLétrehozás();

                MyE.Kiir("Megnevezés", "A1");
                MyE.Kiir("E1 Egységár", "B1");
                MyE.Kiir("E2 Egységár", "C1");
                MyE.Kiir("E3 Egységár", "D1");
                int sor = 1;
                //kitöljük az megnevezéseket
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Épület\épülettörzs.mdb";
                if (File.Exists(hely))
                {
                    string jelszó = "seprűéslapát";
                    string szöveg = "SELECT * FROM takarításosztály where státus=0  order by  id";
                    Kezelő_Épület_Takarítás_Osztály Kéz = new Kezelő_Épület_Takarítás_Osztály();
                    List<Adat_Épület_Takarítás_Osztály> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                    foreach (Adat_Épület_Takarítás_Osztály rekord in Adatok)
                    {
                        sor++;
                        MyE.Kiir(rekord.Osztály, "A" + sor);
                    }
                }

                MyE.Oszlopszélesség("Munka1", "A:D");
                MyE.Rácsoz("a1:D" + sor);
                MyE.NyomtatásiTerület_részletes("Munka1", "A1:D" + sor, "", "", true);
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();

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

        #region Helység lista

        private void Helységlistáz()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Épület\épülettörzs.mdb";
                if (!File.Exists(hely))
                    Adatbázis_Létrehozás.Épülettakarításlétrehozás(hely);
                string jelszó = "seprűéslapát";
                string szöveg = "SELECT * FROM Adattábla where státus=0 order by  id";


                Tábla2.Rows.Clear();
                Tábla2.Columns.Clear();
                Tábla2.Refresh();
                Tábla2.Visible = false;
                Tábla2.ColumnCount = 15;

                // fejléc elkészítése
                Tábla2.Columns[0].HeaderText = "Sorszám";
                Tábla2.Columns[0].Width = 100;
                Tábla2.Columns[1].HeaderText = "Megnevezés";
                Tábla2.Columns[1].Width = 450;
                Tábla2.Columns[2].HeaderText = "Osztály";
                Tábla2.Columns[2].Width = 350;
                Tábla2.Columns[3].HeaderText = "Méret";
                Tábla2.Columns[3].Width = 100;
                Tábla2.Columns[4].HeaderText = "helységkód";
                Tábla2.Columns[4].Width = 130;
                Tábla2.Columns[5].HeaderText = "E1évdb";
                Tábla2.Columns[5].Width = 100;
                Tábla2.Columns[6].HeaderText = "E2évdb";
                Tábla2.Columns[6].Width = 100;
                Tábla2.Columns[7].HeaderText = "E3évdb";
                Tábla2.Columns[7].Width = 100;
                Tábla2.Columns[8].HeaderText = "Kezd";
                Tábla2.Columns[8].Width = 100;
                Tábla2.Columns[9].HeaderText = "Végez";
                Tábla2.Columns[9].Width = 100;
                Tábla2.Columns[10].HeaderText = "Ellenőrneve";
                Tábla2.Columns[10].Width = 250;
                Tábla2.Columns[11].HeaderText = "Ellenőremail";
                Tábla2.Columns[11].Width = 250;
                Tábla2.Columns[12].HeaderText = "Ellenőrtelefonszám";
                Tábla2.Columns[12].Width = 200;
                Tábla2.Columns[13].HeaderText = "Kapcsolthelység";
                Tábla2.Columns[13].Width = 200;
                Tábla2.Columns[14].HeaderText = "Szemetes";
                Tábla2.Columns[14].Width = 100;

                // kiirjuk a tartalmat
                Kezelő_Épület_Takarítás_Adattábla kéz = new Kezelő_Épület_Takarítás_Adattábla();
                List<Adat_Épület_Takarítás_Adattábla> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);
                int i;
                foreach (Adat_Épület_Takarítás_Adattábla rekord in Adatok)
                {

                    Tábla2.RowCount++;
                    i = Tábla2.RowCount - 1;
                    Tábla2.Rows[i].Cells[0].Value = rekord.Id;
                    Tábla2.Rows[i].Cells[1].Value = rekord.Megnevezés.Trim();
                    Tábla2.Rows[i].Cells[2].Value = rekord.Osztály.Trim();
                    Tábla2.Rows[i].Cells[3].Value = rekord.Méret;
                    Tábla2.Rows[i].Cells[4].Value = rekord.Helységkód.Trim();
                    Tábla2.Rows[i].Cells[5].Value = rekord.E1évdb;
                    Tábla2.Rows[i].Cells[6].Value = rekord.E2évdb;
                    Tábla2.Rows[i].Cells[7].Value = rekord.E3évdb;
                    Tábla2.Rows[i].Cells[8].Value = rekord.Kezd.Trim();
                    Tábla2.Rows[i].Cells[9].Value = rekord.Végez.Trim();
                    Tábla2.Rows[i].Cells[10].Value = rekord.Ellenőrneve.Trim();
                    Tábla2.Rows[i].Cells[11].Value = rekord.Ellenőremail.Trim();
                    Tábla2.Rows[i].Cells[12].Value = rekord.Ellenőrtelefonszám.Trim();
                    Tábla2.Rows[i].Cells[13].Value = rekord.Kapcsolthelység.Trim();
                    if (rekord.Szemetes == true)
                        Tábla2.Rows[i].Cells[14].Value = "Van";
                    else
                        Tábla2.Rows[i].Cells[14].Value = "Nincs";
                }

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

        private void Helység_frissít_Click(object sender, EventArgs e)
        {
            Helységlistáz();
        }

        private void Tábla2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            Hsorszám.Text = Tábla2.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
            Hmegnevezés.Text = Tábla2.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
            Combo1.Text = Tábla2.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();
            Hméret.Text = Tábla2.Rows[e.RowIndex].Cells[3].Value.ToString().Trim();
            Hhelyiségkód.Text = Tábla2.Rows[e.RowIndex].Cells[4].Value.ToString().Trim();
            He1évdb.Text = Tábla2.Rows[e.RowIndex].Cells[5].Value.ToString().Trim();
            He2évdb.Text = Tábla2.Rows[e.RowIndex].Cells[6].Value.ToString().Trim();
            He3évdb.Text = Tábla2.Rows[e.RowIndex].Cells[7].Value.ToString().Trim();
            Hkezd.Text = Tábla2.Rows[e.RowIndex].Cells[8].Value.ToString().Trim();
            Hvégez.Text = Tábla2.Rows[e.RowIndex].Cells[9].Value.ToString().Trim();
            Hellenőrneve.Text = Tábla2.Rows[e.RowIndex].Cells[10].Value.ToString().Trim();
            Hellenőremail.Text = Tábla2.Rows[e.RowIndex].Cells[11].Value.ToString().Trim();
            Hellenőrtelefon.Text = Tábla2.Rows[e.RowIndex].Cells[12].Value.ToString().Trim();
            Kapcsolthelység.Text = Tábla2.Rows[e.RowIndex].Cells[13].Value.ToString().Trim();

            if (Tábla2.Rows[e.RowIndex].Cells[14].Value.ToString().Trim() == "Van")
                Check1.Checked = true;
            else
                Check1.Checked = false;

            LapFülek.SelectedIndex = 2;
        }

        private void Helység_excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla2.Rows.Count <= 0)
                    return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Helyiség_" + Program.PostásNév + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
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

        private void Helységürítés()
        {
            Hsorszám.Text = "";
            Combo1.Text = "";
            Hméret.Text = 0.ToString();
            Hkezd.Text = "";
            Hvégez.Text = "";
            Hellenőremail.Text = "";
            Hellenőrneve.Text = "";
            Hellenőrtelefon.Text = "";
            He1évdb.Text = 0.ToString();
            He2évdb.Text = 0.ToString();
            He3évdb.Text = 0.ToString();
            Hmegnevezés.Text = "";
            Hhelyiségkód.Text = "";
            Kapcsolthelység.Text = "";
            Check1.Checked = false;
        }

        private void Helység_feljebb_Click(object sender, EventArgs e)
        {
            Elől_teszi();
        }

        private void Elől_teszi()
        {
            try
            {
                if (Hsorszám.Text.Trim() == "")
                    throw new HibásBevittAdat("A sorszámot ki kell választani.");
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Épület\épülettörzs.mdb";
                if (!File.Exists(hely))
                    Adatbázis_Létrehozás.Épülettakarításlétrehozás(hely);
                string jelszó = "seprűéslapát";
                Tábla2.Sort(Tábla2.Columns[0], System.ComponentModel.ListSortDirection.Ascending);

                int sor_előző = 0;
                int sor = 0;
                string megnevezés_előző = "";
                string osztály_előző = "";
                string megnevezés = "";
                string osztály = "";

                for (int i = 0; i < Tábla2.Rows.Count; i++)
                {
                    if (Tábla2.Rows[i].Cells[0].Value.ToString().Trim() == Hsorszám.Text.Trim())
                    {
                        if (i == 0)
                            return;

                        sor = int.Parse(Tábla2.Rows[i].Cells[0].Value.ToString());
                        megnevezés = Tábla2.Rows[i].Cells[1].Value.ToString();
                        osztály = Tábla2.Rows[i].Cells[2].Value.ToString();
                        sor_előző = int.Parse(Tábla2.Rows[i - 1].Cells[0].Value.ToString());
                        megnevezés_előző = Tábla2.Rows[i - 1].Cells[1].Value.ToString();
                        osztály_előző = Tábla2.Rows[i - 1].Cells[2].Value.ToString();
                        break;
                    }
                }
                // rögzítjük eggyel előrébb a kiválasztott elemet

                string szöveg = "UPDATE Adattábla  SET id=" + sor_előző.ToString();
                szöveg += " WHERE ";
                szöveg += " osztály='" + osztály.Trim() + "' AND ";
                szöveg += " megnevezés='" + megnevezés.Trim() + "'";
                MyA.ABMódosítás(hely, jelszó, szöveg);

                szöveg = "UPDATE Adattábla  SET id=" + sor.ToString();
                szöveg += " WHERE ";
                szöveg += " osztály='" + osztály_előző.Trim() + "' AND ";
                szöveg += " megnevezés='" + megnevezés_előző.Trim() + "'";
                MyA.ABMódosítás(hely, jelszó, szöveg);

                Helységlistáz();

                LapFülek.SelectedIndex = 1;
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

        #region Részletes lap
        private void Részletes_feljebb_Click(object sender, EventArgs e)
        {
            Elől_teszi();
        }


        private void Combofeltöltése()
        {
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Épület\épülettörzs.mdb";
            if (!File.Exists(hely))
                Adatbázis_Létrehozás.Épülettakarításlétrehozás(hely);
            string jelszó = "seprűéslapát";

            string szöveg = "SELECT * FROM takarításosztály where státus=0  order by  id";


            Combo1.Items.Clear();
            Combo1.BeginUpdate();
            Combo1.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "osztály"));
            Combo1.EndUpdate();
            Combo1.Refresh();
        }


        private void Részletes_Kuka_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Épület\épülettörzs.mdb";
                if (!File.Exists(hely)) Adatbázis_Létrehozás.Épülettakarításlétrehozás(hely);
                string jelszó = "seprűéslapát";

                if (Hsorszám.Text.Trim() == "") return;

                string szöveg = "SELECT * FROM adattábla";

                Kezelő_Épület_Takarítás_Adattábla KézÉptakarításAdat = new Kezelő_Épület_Takarítás_Adattábla();
                List<Adat_Épület_Takarítás_Adattábla> AdatokÉptakarításAdat = KézÉptakarításAdat.Lista_Adatok(hely, jelszó, szöveg);

                AdatokÉptakarításAdatListázás();

                Adat_Épület_Takarítás_Adattábla AdatÉptakarításAdat = (from a in AdatokÉptakarításAdat
                                                                       where a.Id == Hsorszám.Text.Trim().ToÉrt_Int()
                                                                       select a).FirstOrDefault();



                if (AdatÉptakarításAdat != null)
                {
                    szöveg = "UPDATE adattábla  SET ";
                    szöveg += "státus=true ";
                    szöveg += " WHERE id=" + Hsorszám.Text.Trim();
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                }

                Helységlistáz();
                Helységürítés();
                LapFülek.SelectedIndex = 1;
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



        private void Részletes_Új_Click(object sender, EventArgs e)
        {
            Helységürítés();
        }


        private void Részletes_rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Hmegnevezés.Text.Trim() == "") throw new HibásBevittAdat("A Megnevezés mezőt ki kell tölteni.");
                if (Combo1.Text.Trim() == "") throw new HibásBevittAdat("A Osztály mezőt ki kell tölteni.");
                if (Hméret.Text.Trim() == "") throw new HibásBevittAdat("A Méret mezőt ki kell tölteni.");
                if (He1évdb.Text.Trim() == "") throw new HibásBevittAdat("Az E1 éves mennyiség mezőt ki kell tölteni.");
                if (He2évdb.Text.Trim() == "") throw new HibásBevittAdat("Az E2 éves mennyiség mezőt ki kell tölteni.");
                if (He3évdb.Text.Trim() == "") throw new HibásBevittAdat("Az E3 éves mennyiség mezőt ki kell tölteni.");
                if (Hkezd.Text.Trim() == "") throw new HibásBevittAdat("Az Kezd mezőt ki kell tölteni.");
                if (Hvégez.Text.Trim() == "") throw new HibásBevittAdat("Az Végez mezőt ki kell tölteni.");

                if (Hellenőrneve.Text.Trim() == "") throw new HibásBevittAdat("Az Ellenőr neve mezőt ki kell tölteni.");
                if (Hellenőremail.Text.Trim() == "") throw new HibásBevittAdat("Az Ellenőr e-mail mezőt ki kell tölteni.");
                if (Hellenőrtelefon.Text.Trim() == "") throw new HibásBevittAdat("Az Ellenőr telefonszáma mezőt ki kell tölteni.");

                if (!double.TryParse(He1évdb.Text.Trim(), out double E1)) throw new HibásBevittAdat("Az E1 éves mennyiségnek egész számnak kell lennie.");
                if (!double.TryParse(He2évdb.Text.Trim(), out double E2)) throw new HibásBevittAdat("Az E2 éves mennyiségnek egész számnak kell lennie.");
                if (!double.TryParse(He3évdb.Text.Trim(), out double E3)) throw new HibásBevittAdat("Az E3 éves mennyiségnek egész számnak kell lennie.");
                if (!double.TryParse(Hméret.Text.Trim(), out double HM)) throw new HibásBevittAdat("A Méret mezőnek számnak kell lennie.");


                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Épület\épülettörzs.mdb";
                if (!File.Exists(hely)) Adatbázis_Létrehozás.Épülettakarításlétrehozás(hely);
                string jelszó = "seprűéslapát";
                string szöveg;

                AdatokÉptakarításAdatListázás();



                if (!int.TryParse(Hsorszám.Text, out int hsorszám))
                {
                    hsorszám = 1;
                    if (AdatokÉptakarításAdat.Count > 0) hsorszám = AdatokÉptakarításAdat.Max(a => a.Id) + 1;
                }

                Adat_Épület_Takarítás_Adattábla AdatÉptakarításAdat = (from a in AdatokÉptakarításAdat
                                                                       where a.Id == hsorszám
                                                                       select a).FirstOrDefault();

                if (AdatÉptakarításAdat != null)
                {
                    szöveg = "UPDATE adattábla  SET ";
                    szöveg += "megnevezés='" + Hmegnevezés.Text.Trim() + "', ";
                    szöveg += "Osztály='" + Combo1.Text.Trim() + "', ";
                    szöveg += "Méret=" + Hméret.Text.Replace(',', '.') + ", ";
                    szöveg += "helységkód='E" + Hsorszám.Text + "', ";
                    szöveg += "E1évdb='" + He1évdb.Text + "', ";
                    szöveg += "E2évdb='" + He2évdb.Text + "', ";
                    szöveg += "E3évdb='" + He3évdb.Text + "', ";
                    szöveg += "kezd='" + Hkezd.Text.Trim() + "', ";
                    szöveg += "végez='" + Hvégez.Text.Trim() + "', ";
                    szöveg += "ellenőremail='" + Hellenőremail.Text.Trim() + "', ";
                    szöveg += "ellenőrneve='" + Hellenőrneve.Text.Trim() + "', ";
                    szöveg += "ellenőrtelefonszám='" + Hellenőrtelefon.Text.Trim() + "', ";
                    if (Check1.Checked == false)
                        szöveg += " szemetes=false, ";
                    else
                        szöveg += " szemetes=true, ";
                    szöveg += "kapcsolthelység='" + Kapcsolthelység.Text.Trim() + "' ";
                    szöveg += $" WHERE id ={hsorszám}";
                }
                else
                {
                    szöveg = "INSERT INTO adattábla  (id, Megnevezés, Osztály, Méret, helységkód, státus, E1évdb, E2évdb, E3évdb," +
                        " kezd, végez, ellenőremail, ellenőrneve, ellenőrtelefonszám, szemetes, kapcsolthelység ) VALUES (";
                    szöveg += $"{hsorszám}, ";
                    szöveg += "'" + Hmegnevezés.Text.Trim() + "', ";
                    szöveg += "'" + Combo1.Text.Trim() + "', ";
                    szöveg += Hméret.Text.Replace(',', '.') + ", ";
                    szöveg += "'E" + Hsorszám.Text + "', ";
                    szöveg += "false, ";
                    szöveg += He1évdb.Text + ", ";
                    szöveg += He2évdb.Text + ", ";
                    szöveg += He3évdb.Text + ", ";
                    szöveg += "'" + Hkezd.Text.Trim() + "', ";
                    szöveg += "'" + Hvégez.Text.Trim() + "', ";
                    szöveg += "'" + Hellenőremail.Text.Trim() + "', ";
                    szöveg += "'" + Hellenőrneve.Text.Trim() + "', ";
                    szöveg += "'" + Hellenőrtelefon.Text.Trim() + "', ";
                    if (Check1.Checked == false)
                        szöveg += " false, ";
                    else
                        szöveg += " true, ";
                    szöveg += "'" + Kapcsolthelység.Text.Trim() + "')";
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);

                Helységlistáz();

                LapFülek.SelectedIndex = 1;
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

        #region Listák
        private void AdatokÉptakarításAdatListázás()
        {
            try
            {
                AdatokÉptakarításAdat.Clear();

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Épület\épülettörzs.mdb";
                if (!File.Exists(hely))
                    Adatbázis_Létrehozás.Épülettakarításlétrehozás(hely);

                string jelszó = "seprűéslapát";

                string szöveg = "SELECT * FROM adattábla";// where id=" + Hsorszám.Text.Trim();

                AdatokÉptakarításAdat = KézÉptakarításAdat.Lista_Adatok(hely, jelszó, szöveg);
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

        private void AdatokTakListázás()
        {
            try
            {
                AdatokTakOsztály.Clear();

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Épület\épülettörzs.mdb";
                if (!File.Exists(hely)) Adatbázis_Létrehozás.Épülettakarításlétrehozás(hely);

                string jelszó = "seprűéslapát";
                string szöveg = "SELECT * FROM takarításosztály";

                AdatokTakOsztály = KézTakOsztály.Lista_Adatok(hely, jelszó, szöveg);
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

        #region Opcionális

        private void OpcióListaFeltöltés()
        {
            try
            {
                AdatokTakOpció.Clear();
                AdatokTakOpció = KézOpció.Lista_Adatok();
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

        private void Opció_Frissít_Click(object sender, EventArgs e)
        {
            OpcióListaFeltöltés();
            OpcióTáblaListázás();
        }

        private void OpcióTáblaListázás()
        {
            try
            {
                AdatTábla.Clear();
                ABFejléc();
                ABFeltöltése();
                Opció_Tábla.DataSource = AdatTábla;
                ABOszlopSzélesség();
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

        private void ABFejléc()
        {
            try
            {
                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Sorszám");
                AdatTábla.Columns.Add("Megnevezés");
                AdatTábla.Columns.Add("Mennyisége");
                AdatTábla.Columns.Add("Ár");
                AdatTábla.Columns.Add("Kezdet", typeof(DateTime));
                AdatTábla.Columns.Add("Vég", typeof(DateTime));

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

        private void ABFeltöltése()
        {
            try
            {
                AdatTábla.Clear();
                foreach (Adat_Takarítás_Opció rekord in AdatokTakOpció)
                {
                    DataRow Soradat = AdatTábla.NewRow();
                    Soradat["Sorszám"] = rekord.Id;
                    Soradat["Megnevezés"] = rekord.Megnevezés;
                    Soradat["Mennyisége"] = rekord.Mennyisége;
                    Soradat["Ár"] = rekord.Ár;
                    Soradat["Kezdet"] = rekord.Kezdet;
                    Soradat["Vég"] = rekord.Vég;
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

        private void ABOszlopSzélesség()
        {
            Opció_Tábla.Columns["Sorszám"].Width = 100;
            Opció_Tábla.Columns["Megnevezés"].Width = 400;
            Opció_Tábla.Columns["Mennyisége"].Width = 150;
            Opció_Tábla.Columns["Ár"].Width = 150;
            Opció_Tábla.Columns["Kezdet"].Width = 150;
            Opció_Tábla.Columns["Vég"].Width = 150;
        }

        private void Opció_Új_Click(object sender, EventArgs e)
        {
            Opció_Id.Text = "";
            Opció_Megnevezés.Text = "";
            Opció_Mennyisége.Text = "";
            Opció_Ár.Text = "";
            Opció_Kezdet.Value = new DateTime(1900, 1, 1);
            Opció_Vég.Value = new DateTime(1900, 1, 1);
        }

        private void Opció_Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Adat_Takarítás_Opció Elem = AdatokTakOpció.Where(a => a.Id == Opció_Tábla.Rows[e.RowIndex].Cells[0].Value.ToÉrt_Int()).FirstOrDefault();
            if (Elem != null)
            {
                Opció_Id.Text = Elem.Id.ToString();
                Opció_Megnevezés.Text = Elem.Megnevezés;
                Opció_Mennyisége.Text = Elem.Mennyisége;
                Opció_Ár.Text = Elem.Ár.ToString();
                Opció_Kezdet.Value = Elem.Kezdet;
                Opció_Vég.Value = Elem.Vég;
            }
        }

        private void Opció_OK_Click(object sender, EventArgs e)
        {
            try
            {
                if (!double.TryParse(Opció_Ár.Text, out double Ár)) throw new HibásBevittAdat("Az Ár mezőben számnak kell lennie.");
                if (Opció_Megnevezés.Text.Trim() == "") throw new HibásBevittAdat("Megnevezés mezőt ki kell tölteni.");
                if (Opció_Mennyisége.Text.Trim() == "") throw new HibásBevittAdat("Mennyiség egység mezőt ki kell tölteni.");

                OpcióListaFeltöltés();

                if (int.TryParse(Opció_Id.Text, out int ID))
                {
                    KézOpció.Módosít(new Adat_Takarítás_Opció(ID,
                                                                             Opció_Megnevezés.Text.Trim(),
                                                                             Opció_Mennyisége.Text.Trim(),
                                                                             Ár,
                                                                             Opció_Kezdet.Value,
                                                                             Opció_Vég.Value));
                }
                else
                {
                    if (AdatokTakOpció.Count == 0)
                        ID = 1;
                    else
                        ID = AdatokTakOpció.Max(a => a.Id) + 1;
                    KézOpció.Rögzít(new Adat_Takarítás_Opció(ID,
                                                         MyF.Szöveg_Tisztítás(Opció_Megnevezés.Text.Trim()),
                                                         Opció_Mennyisége.Text.Trim(),
                                                         Ár,
                                                         Opció_Kezdet.Value,
                                                         Opció_Vég.Value));
                }
                OpcióListaFeltöltés();
                OpcióTáblaListázás();
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

        private void Opció_Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Opció_Tábla.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Opcionális_{Program.PostásNév}-{DateTime.Now.ToString("yyyyMMddHHmmss")}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                Module_Excel.EXCELtábla(fájlexc, Opció_Tábla, false);
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


    }
}