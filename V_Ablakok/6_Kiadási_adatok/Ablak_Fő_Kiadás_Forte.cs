using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos.Kezelők;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using Villamos.Villamos_Kezelők;
using static System.IO.File;
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{

    public partial class Ablak_Fő_Kiadás_Forte
    {

        readonly Kezelő_Fő_Forte Kéz_Forte = new Kezelő_Fő_Forte();

        List<Adat_Fő_Forte> Adatok_Forte = new List<Adat_Fő_Forte>();

        bool Figyel = false;

        public Ablak_Fő_Kiadás_Forte()
        {
            InitializeComponent();
        }


        private void Ablak_Fő_Kiadás_Forte_Load(object sender, EventArgs e)
        {
            Dátum.Value = DateTime.Today;
            Dátumról.Value = DateTime.Today;
            string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value.Year}";
            if (!Exists(hely)) Directory.CreateDirectory(hely);

            hely += $@"\{Dátum.Value.Year}_fortekiadási_adatok.mdb";
            if (!Exists(hely)) Adatbázis_Létrehozás.Fortekiadásifőmtábla(hely);

            Jogosultságkiosztás();
            Adatok_Forte_Feltöltés();
        }


        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;

                // ide kell az összes gombot tenni amit szabályozni akarunk false
                Töröl.Enabled = false;
                Fortebeolvasás.Enabled = false;
                AdatMásol.Enabled = false;
                MunkaHétvége.Enabled = false;
                // csak főmérnökségi belépéssel van módosítás
                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {
                    Töröl.Visible = true;
                    Fortebeolvasás.Visible = true;
                    AdatMásol.Visible = true;
                    MunkaHétvége.Visible = true;
                }
                else
                {
                    Töröl.Visible = false;
                    Fortebeolvasás.Visible = false;
                    AdatMásol.Visible = false;
                    MunkaHétvége.Visible = false;
                }

                melyikelem = 185;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Töröl.Enabled = true;
                    Fortebeolvasás.Enabled = true;
                    AdatMásol.Enabled = true;
                    MunkaHétvége.Enabled = true;
                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                { }
                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 3))
                { }
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


        private void Lista_Click(object sender, EventArgs e)
        {
            Táblaírása();
        }


        private void Táblaírása()
        {
            try
            {
                Adatok_Forte_Feltöltés();

                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 8;

                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Dátum";
                Tábla.Columns[0].Width = 120;
                Tábla.Columns[1].HeaderText = "Napszak";
                Tábla.Columns[1].Width = 100;
                Tábla.Columns[2].HeaderText = "Telephely Forte";
                Tábla.Columns[2].Width = 100;
                Tábla.Columns[3].HeaderText = "Típus Forte";
                Tábla.Columns[3].Width = 100;
                Tábla.Columns[4].HeaderText = "Telephely";
                Tábla.Columns[4].Width = 100;
                Tábla.Columns[5].HeaderText = "Típus";
                Tábla.Columns[5].Width = 100;
                Tábla.Columns[6].HeaderText = "Kiadás";
                Tábla.Columns[6].Width = 100;
                Tábla.Columns[7].HeaderText = "Munkanap";
                Tábla.Columns[7].Width = 100;

                List<Adat_Fő_Forte> Adatok;

                if (Délelőtt.Checked)
                    Adatok = (from a in Adatok_Forte
                              where a.Dátum == Dátum.Value && a.Napszak == "de"
                              select a).ToList();
                else
                    Adatok = (from a in Adatok_Forte
                              where a.Dátum == Dátum.Value && a.Napszak == "du"
                              select a).ToList();

                int összesen = 0;
                int i = 0;

                foreach (Adat_Fő_Forte Adat in Adatok)
                {
                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = Adat.Dátum.ToString("yyyy.MM.dd");
                    Tábla.Rows[i].Cells[1].Value = Adat.Napszak.Trim();
                    Tábla.Rows[i].Cells[2].Value = Adat.Telephelyforte.Trim();
                    Tábla.Rows[i].Cells[3].Value = Adat.Típusforte.Trim();
                    Tábla.Rows[i].Cells[4].Value = Adat.Telephely.Trim();
                    Tábla.Rows[i].Cells[5].Value = Adat.Típus.Trim();
                    Tábla.Rows[i].Cells[6].Value = Adat.Kiadás.ToString();
                    összesen += Adat.Kiadás;
                    if (Adat.Munkanap == 0)
                        Tábla.Rows[i].Cells[7].Value = "Munkanap";
                    else
                        Tábla.Rows[i].Cells[7].Value = "Hétvége";
                }
                Tábla.RowCount++;
                i = Tábla.RowCount - 1;
                Tábla.Rows[i].Cells[5].Value = "Összesen";
                Tábla.Rows[i].Cells[6].Value = összesen.ToString();

                Tábla.Visible = true;
                Tábla.Refresh();
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


        private void Command1_Click(object sender, EventArgs e)
        {
            Táblaírásahavi();
        }


        private void Táblaírásahavi()
        {
            try
            {
                Adatok_Forte_Feltöltés();

                List<Adat_Fő_Forte> Adatok;

                if (Délelőtt.Checked)
                    Adatok = (from a in Adatok_Forte
                              where a.Dátum >= MyF.Hónap_elsőnapja(Dátum.Value)
                              && a.Dátum < MyF.Hónap_utolsónapja(Dátum.Value)
                              && a.Napszak == "de"
                              orderby a.Dátum
                              select a).ToList();
                else
                    Adatok = (from a in Adatok_Forte
                              where a.Dátum >= MyF.Hónap_elsőnapja(Dátum.Value)
                              && a.Dátum < MyF.Hónap_utolsónapja(Dátum.Value)
                              && a.Napszak == "du"
                              orderby a.Dátum
                              select a).ToList();

                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 8;

                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Dátum";
                Tábla.Columns[0].Width = 120;
                Tábla.Columns[1].HeaderText = "Napszak";
                Tábla.Columns[1].Width = 100;
                Tábla.Columns[2].HeaderText = "Telephely Forte";
                Tábla.Columns[2].Width = 100;
                Tábla.Columns[3].HeaderText = "Típus Forte";
                Tábla.Columns[3].Width = 100;
                Tábla.Columns[4].HeaderText = "Telephely";
                Tábla.Columns[4].Width = 100;
                Tábla.Columns[5].HeaderText = "Típus";
                Tábla.Columns[5].Width = 100;
                Tábla.Columns[6].HeaderText = "Kiadás";
                Tábla.Columns[6].Width = 100;
                Tábla.Columns[7].HeaderText = "Munkanap";
                Tábla.Columns[7].Width = 100;

                int i;
                foreach (Adat_Fő_Forte Adat in Adatok)
                {
                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = Adat.Dátum.ToString("yyyy.MM.dd");
                    Tábla.Rows[i].Cells[1].Value = Adat.Napszak.Trim();
                    Tábla.Rows[i].Cells[2].Value = Adat.Telephelyforte.Trim();
                    Tábla.Rows[i].Cells[3].Value = Adat.Típusforte.Trim();
                    Tábla.Rows[i].Cells[4].Value = Adat.Telephely.Trim();
                    Tábla.Rows[i].Cells[5].Value = Adat.Típus.Trim();
                    Tábla.Rows[i].Cells[6].Value = Adat.Kiadás.ToString();

                    if (Adat.Munkanap == 0)
                        Tábla.Rows[i].Cells[7].Value = "Munkanap";
                    else
                        Tábla.Rows[i].Cells[7].Value = "Hétvége";
                }

                Tábla.Visible = true;
                Tábla.Refresh();
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


        private void Délelőtt_Click(object sender, EventArgs e)
        {
            Táblaírása();
        }


        private void Délután_Click(object sender, EventArgs e)
        {
            Táblaírása();
        }


        private void Dátum_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string hely;
                hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value.Year}";
                if (!Exists(hely)) Directory.CreateDirectory(hely);

                hely += $@"\{Dátum.Value.Year}_fortekiadási_adatok.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.Fortekiadásifőmtábla(hely);

                Adatok_Forte_Feltöltés();
                Táblaírása();
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


        private void Button3_Click(object sender, EventArgs e)
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
                    FileName = $"ZSER_{Program.PostásNév}-{DateTime.Now:yyyyMMddHHmmss}-{Dátum.Value:yyyyMMdd}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Tábla, false);
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


        private void Fortebeolvasás_Click(object sender, EventArgs e)
        {
            try
            {
                Excelbeolvasás();
                if (Figyel == false)
                {
                    Adatokegyeztetése();
                    MessageBox.Show("Az adat konvertálás befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Táblaírása();
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


        private void Excelbeolvasás()
        {
            string fájlexc="";
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value.Year}";
                string jelszó = "gémkapocs";
                if (!Exists(hely)) Directory.CreateDirectory(hely);

                hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_fortekiadási_adatok.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.Fortekiadásifőmtábla(hely);

                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "FORTE-s Adatok betöltése",
                    FileName = "",
                    Filter = "Excel |*.xlsx"
                };
          
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = OpenFileDialog1.FileName;
                else
                {
                    Figyel = true;
                    return;
                }

                // megnyitjuk a beolvasandó táblát
                MyE.ExcelMegnyitás(fájlexc);


                // leellenőrizzük, hogy az adat nap egyezik-e

                if (DateTime.Parse(MyE.Beolvas("A4")).ToString("yyyyMMdd") != Dátum.Value.ToString("yyyyMMdd"))
                {
                    // ha nem egyezik akkor
                    throw new HibásBevittAdat("A betölteni kívánt adatok nem egyeznek meg a beállított nappal ");
                }

                // megnézzük, hogy hány sorból áll a tábla
                int i = 1;
                int utolsó = 0;
                int első = 0;
                while (MyE.Beolvas($"a{i}").Trim() != "Mindösszesen:")
                {
                    if (MyE.Beolvas($"a{i}").Trim() == "Összesen:")
                        első = i;
                    utolsó = i;
                    i += 1;
                }
                Holtart.Be(utolsó + 1);

                Napiadattörlés();

                if (utolsó > 1)
                {
                    i = első;
                    while (utolsó + 1 != i)
                    {
                        // délelőtti adatok beolvasása
                        DateTime dátum_ = Dátum.Value;
                        string napszak_ = "de";
                        string telephelyforte_ = MyE.Beolvas($"d{i}").Trim();
                        string típusforte_ = MyE.Beolvas($"e{i}").Trim();
                        string telephely_ = "_";
                        string típus_ = "_";
                        if (!int.TryParse(MyE.Beolvas($"H{i}"), out int kiadás_))
                            kiadás_ = 0;

                        int munkanap_ = Munkanap.Checked ? 0 : 1;

                        Adat_Fő_Forte Adat = new Adat_Fő_Forte(dátum_, napszak_, telephelyforte_, típusforte_, telephely_, típus_, kiadás_, munkanap_);
                        Kéz_Forte.Rögzít_Fő_forte(hely, jelszó, Adat);


                        // délutáni adatok beolvasása
                        napszak_ = "du";
                        kiadás_ = int.Parse(MyE.Beolvas("j" + i.ToString()).Trim());

                        Adat = new Adat_Fő_Forte(dátum_, napszak_, telephelyforte_, típusforte_, telephely_, típus_, kiadás_, munkanap_);
                        Kéz_Forte.Rögzít_Fő_forte(hely, jelszó, Adat);

                        Holtart.Lép();
                        i++;
                    }
                }
                MyE.ExcelBezárás();
                Figyel = false;
                Holtart.Ki();
                // kitöröljük a betöltött fájlt
                File.Delete(fájlexc);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (ex.StackTrace.Contains("System.IO.File.InternalDelete"))
                    MessageBox.Show($"A programnak a beolvasott adatokat tartalmazó fájlt nem sikerült törölni.\n Valószínüleg a {fájlexc} nyitva van.\n\nAz adat konvertálás befejeződött!", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void Adatokegyeztetése()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő.mdb";
                string jelszó = "Mocó";
                string szöveg = "SELECT * FROM fortetípus";

                Kezelő_Kiegészítő_Fortetípus KézTípus = new Kezelő_Kiegészítő_Fortetípus();
                List<Adat_Kiegészítő_Fortetípus> AdatokTípus = KézTípus.Lista_Adatok(hely, jelszó, szöveg);

                szöveg = "SELECT * FROM telephelytábla";
                Kezelő_kiegészítő_telephely KézTelep = new Kezelő_kiegészítő_telephely();
                List<Adat_kiegészítő_telephely> AdatokTelep = KézTelep.Lista_adatok(hely, jelszó, szöveg);

                Adatok_Forte_Feltöltés();

                hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_fortekiadási_adatok.mdb";
                jelszó = "gémkapocs";

                Holtart.Be();

                List<Adat_Fő_Forte> Adatok = (from a in Adatok_Forte
                                              where a.Dátum == Dátum.Value
                                              select a).ToList();

                List<string> SzövegGy = new List<string>();
                foreach (Adat_Fő_Forte rekord in Adatok)
                {

                    string telephely = (from a in AdatokTelep
                                        where a.Fortekód == rekord.Telephelyforte
                                        select a.Telephelynév.Trim()).FirstOrDefault() ?? "_";

                    string típus = (from a in AdatokTípus
                                    where a.Ftípus == rekord.Típusforte && a.Telephely == telephely
                                    select a.Telephelyitípus.Trim()).FirstOrDefault() ?? "_";

                    szöveg = "UPDATE fortekiadástábla  SET ";
                    szöveg += $"telephely='{telephely}', ";
                    szöveg += $"típus='{típus}' ";
                    szöveg += $" WHERE [dátum]=#{Dátum.Value:M-d-yy}# AND napszak='{rekord.Napszak}' AND ";
                    szöveg += $" telephelyforte='{rekord.Telephelyforte}' AND típusforte='{rekord.Típusforte}'";

                    Adat_Fő_Forte Elem = (from a in Adatok_Forte
                                          where a.Dátum == Dátum.Value && a.Napszak == rekord.Napszak && a.Telephelyforte == rekord.Telephelyforte && a.Típusforte == rekord.Típusforte
                                          select a).FirstOrDefault();

                    if (Elem != null) 
                    SzövegGy.Add(szöveg);
                    Holtart.Lép();
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);

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


        private void Napiadattörlés()
        {
            try
            {

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value.Year}";
                if (!Exists(hely)) Directory.CreateDirectory(hely);

                hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_fortekiadási_adatok.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.Fortekiadásifőmtábla(hely);

                Adatok_Forte_Feltöltés();


                Adat_Fő_Forte Elem = (from a in Adatok_Forte
                                      where a.Dátum == Dátum.Value
                                      select a).FirstOrDefault();
                if (Elem != null)
                {
                    string jelszó = "gémkapocs";
                    string szöveg = $"DELETE FROM fortekiadástábla where [dátum]=#{Dátum.Value:M-d-yy}#";
                    MyA.ABtörlés(hely, jelszó, szöveg);
                    Figyel = true;
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


        private void Töröl_Click(object sender, EventArgs e)
        {
            try
            {

                Figyel = false;
                Napiadattörlés();
                if (Figyel == true)
                {
                    MessageBox.Show("A napi adatok törlése megtörtént.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Táblaírása();
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


        private void AdatMásol_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value.Year}";
                if (!Exists(hely)) Directory.CreateDirectory(hely);

                hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_fortekiadási_adatok.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.Fortekiadásifőmtábla(hely);

                Napiadattörlés();
                Adatok_Forte_Feltöltés();
                // kitöröljük az adott napi adatokat

                List<Adat_Fő_Forte> Adatok = (from a in Adatok_Forte
                                              where a.Dátum == Dátumról.Value
                                              select a).ToList();
                // rögzítjük az adatokat
                string jelszó = "gémkapocs";

                List<string> SzövegGy = new List<string>();
                foreach (Adat_Fő_Forte rekord in Adatok)
                {
                    string szöveg = "INSERT INTO fortekiadástábla  (dátum, napszak, telephelyforte, típusforte, telephely, típus, kiadás, munkanap  ) VALUES (";
                    szöveg += $"'{Dátum.Value:yyyy.MM.dd}', ";
                    szöveg += $"'{rekord.Napszak}', ";
                    szöveg += $"'{rekord.Telephelyforte}', ";
                    szöveg += $"'{rekord.Típusforte}', ";
                    szöveg += $"'{rekord.Telephely}', ";
                    szöveg += $"'{rekord.Típus}', ";
                    szöveg += $"{rekord.Kiadás}, ";
                    szöveg += $"{rekord.Munkanap}) ";
                    SzövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);

                MessageBox.Show("A napi adatok másolása megtörtént.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Adatok_Forte_Feltöltés();
                Táblaírása();
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


        private void MunkaHétvége_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value.Year}";
                if (!Exists(hely)) Directory.CreateDirectory(hely);

                hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_fortekiadási_adatok.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.Fortekiadásifőmtábla(hely);


                Adatok_Forte_Feltöltés();

                // 'módosítjuk a mukanapi adatokat
                List<Adat_Fő_Forte> Elemek = (from a in Adatok_Forte
                                              where a.Dátum == Dátum.Value
                                              select a).ToList();
                int munkanap = 1;
                if (Elemek != null)
                {
                    //Megfordítjuk minden elemre                  
                    if (Elemek[0].Munkanap == 1)
                        munkanap = 0;
                    string szöveg = "UPDATE fortekiadástábla  SET ";
                    szöveg += $"munkanap={munkanap}";
                    szöveg += $" WHERE [dátum]=#{Dátum.Value:M-d-yy}#";
                    string jelszó = "gémkapocs";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                    MessageBox.Show("A napi adatok munkanap állítása megtörtént.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Táblaírása();
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
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\forte_beolvas.html";
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


        #region Listákfeltöltése

        private void Adatok_Forte_Feltöltés()
        {
            try
            {
                Adatok_Forte.Clear();

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_fortekiadási_adatok.mdb";
                if (!Exists(hely)) return;
                string jelszó = "gémkapocs";
                string szöveg = "SELECT * FROM fortekiadástábla ";

                Adatok_Forte = Kéz_Forte.Lista_Adatok(hely, jelszó, szöveg);
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