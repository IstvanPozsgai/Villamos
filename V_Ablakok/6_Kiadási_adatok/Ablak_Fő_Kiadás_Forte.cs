using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos
{

    public partial class Ablak_Fő_Kiadás_Forte
    {
        readonly Kezelő_Forte_Kiadási_Adatok Kéz_Forte = new Kezelő_Forte_Kiadási_Adatok();
        readonly Kezelő_Kiegészítő_Fortetípus KézTípus = new Kezelő_Kiegészítő_Fortetípus();
        readonly Kezelő_kiegészítő_telephely KézTelep = new Kezelő_kiegészítő_telephely();

        List<Adat_Forte_Kiadási_Adatok> Adatok_Forte = new List<Adat_Forte_Kiadási_Adatok>();

        bool Figyel = false;

        public Ablak_Fő_Kiadás_Forte()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            Dátum.Value = DateTime.Today;
            Dátumról.Value = DateTime.Today;
            //Ha az első karakter "R" akkor az új jogosultságkiosztást használjuk
            //ha nem akkor a régit használjuk
            if (Program.PostásJogkör.Substring(0, 1) == "R")
                GombLathatosagKezelo.Beallit(this);
            else
                Jogosultságkiosztás();
            Adatok_Forte = Kéz_Forte.Lista_Adatok(Dátum.Value.Year);
        }

        private void Ablak_Fő_Kiadás_Forte_Load(object sender, EventArgs e)
        {

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
                Adatok_Forte = Kéz_Forte.Lista_Adatok(Dátum.Value.Year);

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

                List<Adat_Forte_Kiadási_Adatok> Adatok;

                if (Délelőtt.Checked)
                    Adatok = (from a in Adatok_Forte
                              where a.Dátum == Dátum.Value && a.Napszak == "de"
                              select a).ToList();
                else
                    Adatok = (from a in Adatok_Forte
                              where a.Dátum == Dátum.Value && a.Napszak == "du"
                              select a).ToList();

                long összesen = 0;
                int i = 0;

                foreach (Adat_Forte_Kiadási_Adatok Adat in Adatok)
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
                Adatok_Forte = Kéz_Forte.Lista_Adatok(Dátum.Value.Year);

                List<Adat_Forte_Kiadási_Adatok> Adatok;

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
                foreach (Adat_Forte_Kiadási_Adatok Adat in Adatok)
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
                Adatok_Forte = Kéz_Forte.Lista_Adatok(Dátum.Value.Year);
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

                MyX.DataGridViewToXML(fájlexc, Tábla);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MyF.Megnyitás(fájlexc);
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
                if (!Figyel)
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
            string fájlexc = "";
            try
            {
                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "FORTE-s Adatok betöltése",
                    FileName = "",
                    Filter = "Excel |*.xlsx"
                };

                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialogPI.ShowDialogEllenőr(OpenFileDialog1) == DialogResult.OK)
                    fájlexc = OpenFileDialog1.FileName;
                else
                {
                    Figyel = true;
                    return;
                }

                // megnyitjuk a beolvasandó táblát
                string munkalap = "Munka1";
                MyX.ExcelMegnyitás(fájlexc);


                // leellenőrizzük, hogy az adat nap egyezik-e

                if (DateTime.Parse(MyX.Beolvas(munkalap, "A4")).ToString("yyyyMMdd") != Dátum.Value.ToString("yyyyMMdd"))
                {
                    // ha nem egyezik akkor
                    throw new HibásBevittAdat("A betölteni kívánt adatok nem egyeznek meg a beállított nappal ");
                }

                // megnézzük, hogy hány sorból áll a tábla
                int i = 1;
                int utolsó = 0;
                int első = 0;
                while (MyX.Beolvas(munkalap, $"a{i}").Trim() != "Mindösszesen:")
                {
                    if (MyX.Beolvas(munkalap, $"a{i}").Trim() == "Összesen:")
                        első = i;
                    utolsó = i;
                    i += 1;
                }
                Holtart.Be(utolsó + 1);

                Napiadattörlés();

                List<Adat_Forte_Kiadási_Adatok> AdatokGY = new List<Adat_Forte_Kiadási_Adatok>();

                if (utolsó > 1)
                {
                    i = első;
                    while (utolsó + 1 != i)
                    {
                        // délelőtti adatok beolvasása
                        DateTime dátum_ = Dátum.Value;
                        string napszak_ = "de";
                        string telephelyforte_ = MyX.Beolvas(munkalap, $"d{i}").Trim();
                        string típusforte_ = MyX.Beolvas(munkalap, $"e{i}").Trim();
                        string telephely_ = "_";
                        string típus_ = "_";
                        if (!int.TryParse(MyX.Beolvas(munkalap, $"H{i}"), out int kiadás_)) kiadás_ = 0;
                        int munkanap_ = Munkanap.Checked ? 0 : 1;

                        Adat_Forte_Kiadási_Adatok Adat = new Adat_Forte_Kiadási_Adatok(dátum_, napszak_, telephelyforte_, típusforte_, telephely_, típus_, kiadás_, munkanap_);
                        AdatokGY.Add(Adat);

                        // délutáni adatok beolvasása
                        napszak_ = "du";
                        kiadás_ = int.Parse(MyX.Beolvas(munkalap, $"j{i}").Trim());

                        Adat = new Adat_Forte_Kiadási_Adatok(dátum_, napszak_, telephelyforte_, típusforte_, telephely_, típus_, kiadás_, munkanap_);
                        AdatokGY.Add(Adat);

                        Holtart.Lép();
                        i++;
                    }
                }
                MyX.ExcelBezárás();
                if (AdatokGY != null && AdatokGY.Count > 0) Kéz_Forte.Rögzítés(Dátum.Value.Year, AdatokGY);
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
                List<Adat_Kiegészítő_Fortetípus> AdatokTípus = KézTípus.Lista_Adatok();
                List<Adat_kiegészítő_telephely> AdatokTelep = KézTelep.Lista_Adatok();

                Holtart.Be();

                Adatok_Forte = Kéz_Forte.Lista_Adatok(Dátum.Value.Year);
                List<Adat_Forte_Kiadási_Adatok> Adatok = (from a in Adatok_Forte
                                                          where a.Dátum == Dátum.Value
                                                          select a).ToList();

                List<Adat_Forte_Kiadási_Adatok> AdatokGY = new List<Adat_Forte_Kiadási_Adatok>();
                foreach (Adat_Forte_Kiadási_Adatok rekord in Adatok)
                {

                    string telephely = (from a in AdatokTelep
                                        where a.Fortekód == rekord.Telephelyforte
                                        select a.Telephelynév.Trim()).FirstOrDefault() ?? "_";

                    string típus = (from a in AdatokTípus
                                    where a.Ftípus == rekord.Típusforte && a.Telephely == telephely
                                    select a.Telephelyitípus.Trim()).FirstOrDefault() ?? "_";

                    Adat_Forte_Kiadási_Adatok ADAT = new Adat_Forte_Kiadási_Adatok(
                                               Dátum.Value,
                                               rekord.Napszak,
                                               rekord.Telephelyforte,
                                               rekord.Típusforte,
                                               telephely,
                                               típus,
                                               0, 0);
                    AdatokGY.Add(ADAT);
                    Holtart.Lép();
                }
                if (AdatokGY != null && AdatokGY.Count > 0) Kéz_Forte.Módosítás(Dátum.Value.Year, AdatokGY);

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
                Adatok_Forte = Kéz_Forte.Lista_Adatok(Dátum.Value.Year);
                Adat_Forte_Kiadási_Adatok Elem = (from a in Adatok_Forte
                                                  where a.Dátum == Dátum.Value
                                                  select a).FirstOrDefault();
                if (Elem != null)
                {
                    Kéz_Forte.Törlés(Dátum.Value.Year, Dátum.Value);
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
                // kitöröljük az adott napi adatokat
                Napiadattörlés();
                Adatok_Forte = Kéz_Forte.Lista_Adatok(Dátum.Value.Year);

                List<Adat_Forte_Kiadási_Adatok> Adatok = (from a in Adatok_Forte
                                                          where a.Dátum == Dátumról.Value
                                                          select a).ToList();
                // rögzítjük az adatokat
                List<Adat_Forte_Kiadási_Adatok> AdatokGy = new List<Adat_Forte_Kiadási_Adatok>();
                foreach (Adat_Forte_Kiadási_Adatok rekord in Adatok)
                {
                    Adat_Forte_Kiadási_Adatok ADAT = new Adat_Forte_Kiadási_Adatok(
                                              Dátum.Value,
                                              rekord.Napszak,
                                              rekord.Telephelyforte,
                                              rekord.Típusforte,
                                              rekord.Telephely,
                                              rekord.Típus,
                                              rekord.Kiadás,
                                              rekord.Munkanap);
                    AdatokGy.Add(ADAT);
                }
                if (AdatokGy != null && AdatokGy.Count > 0) Kéz_Forte.Rögzítés(Dátum.Value.Year, AdatokGy);

                MessageBox.Show("A napi adatok másolása megtörtént.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Adatok_Forte = Kéz_Forte.Lista_Adatok(Dátum.Value.Year);
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
                Adatok_Forte = Kéz_Forte.Lista_Adatok(Dátum.Value.Year);

                // 'módosítjuk a mukanapi adatokat
                List<Adat_Forte_Kiadási_Adatok> Elemek = (from a in Adatok_Forte
                                                          where a.Dátum == Dátum.Value
                                                          select a).ToList();
                int munkanap = 1;
                if (Elemek != null)
                {
                    //Megfordítjuk minden elemre                  
                    if (Elemek[0].Munkanap == 1) munkanap = 0;
                    Kéz_Forte.Módosítás(Dátum.Value.Year, Dátum.Value, munkanap);

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
    }
}