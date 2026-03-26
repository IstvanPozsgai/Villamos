using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;
namespace Villamos
{

    public partial class Ablak_Digitális_Főkönyv
    {
        readonly Kezelő_kiegészítő_telephely KézKiegTelephely = new Kezelő_kiegészítő_telephely();
        readonly Kezelő_Főkönyv_Nap FN_Kéz = new Kezelő_Főkönyv_Nap();
        readonly Kezelő_Kiadás_Összesítő KézKiadási = new Kezelő_Kiadás_Összesítő();
        readonly Kezelő_Jármű Kéz_Jármű = new Kezelő_Jármű();
        readonly Kezelő_Forte_Kiadási_Adatok KézForteKiadás = new Kezelő_Forte_Kiadási_Adatok();

        readonly List<string> Telephelykönyvtár = new List<string>();
        #region Alap
        public Ablak_Digitális_Főkönyv()
        {
            InitializeComponent();
            Start();
        }

        private void Ablak_Digitális_Főkönyv_Load(object sender, EventArgs e)
        {

        }

        private void Start()
        {
            Gombokfel();
            Választot_értékek();
            Dátum.Value = DateTime.Today;
            Dátum.MaxDate = DateTime.Today;
            Választott_Nap.Text = DateTime.Today.ToString("yyyy.MM.dd");
            Választott_napszak.Text = "Délelőtt";
            Délelőtt.Checked = true;

            Panel5.Visible = false;
            Típusfeltöltés_melyik();
            Telephelyfeltöltés_Melyik();
            //Ha az első karakter "R" akkor az új jogosultságkiosztást használjuk
            //ha nem akkor a régit használjuk
            if (Program.PostásJogkör.Substring(0, 1) == "R")
                GombLathatosagKezelo.Beallit(this);
            else
            { }
        }

        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Digitális_Főkönyv.html";
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
        #endregion


        #region Indulás
        private void Választot_értékek()
        {
            Választott_Nap.Text = "";
            Választott_Telephely.Text = "";
            Választott_napszak.Text = "";
        }

        private void Gombokfel()
        {
            try
            {
                GombTároló.Controls.Clear();
                int Gombokszáma = 0;
                List<Adat_kiegészítő_telephely> Adatok = KézKiegTelephely.Lista_Adatok();

                int i = 1;
                foreach (Adat_kiegészítő_telephely item in Adatok)
                {
                    Gombokszáma++;
                    Button Telephelygomb = new Button
                    {
                        Location = new Point(10, 10 + Gombokszáma * 40),
                        Size = new Size(145, 35),
                        Name = $"Járgomb_{Gombokszáma + 1}",
                        Text = item.Telephelynév,
                        Visible = true
                    };

                    List<Adat_Kiadás_összesítő> AdatokÖsszesítő = KézKiadási.Lista_Adatok(item.Telephelykönyvtár.Trim(), Dátum.Value.Year);

                    if (AdatokÖsszesítő != null)
                    {
                        List<Adat_Kiadás_összesítő> Elemek = (from a in AdatokÖsszesítő
                                                              where a.Dátum.Date == Dátum.Value.Date
                                                              orderby a.Napszak, a.Típus
                                                              select a).ToList();
                        if (Elemek != null)
                        {
                            if (Délelőtt.Checked)
                                Elemek = (from a in Elemek
                                          where a.Napszak == "de"
                                          select a).ToList();
                            else
                                Elemek = (from a in Elemek
                                          where a.Napszak == "du"
                                          select a).ToList();
                        }

                        if (Elemek.Any())
                        {
                            Telephelygomb.BackColor = Color.MediumSpringGreen;
                            Telephelygomb.Enabled = true;
                        }
                        else
                        {
                            Telephelygomb.BackColor = Color.Red;
                            Telephelygomb.Enabled = false;
                        }
                    }
                    else
                    {
                        Telephelygomb.BackColor = Color.Red;
                        Telephelygomb.Enabled = false;
                    }
                    Telephelygomb.Click += Telephelyre_Click;
                    GombTároló.Controls.Add(Telephelygomb);
                    i += 1;
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

        private void Telephelyre_Click(object sender, EventArgs e)
        {
            try
            {
                // ha gombra kattintottunk
                Button Telephelygomb = (Button)sender;
                if (sender is Button)
                {
                    Választott_Telephely.Text = Telephelygomb.Text;
                    Kiirtáblák();
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
            Választott_Nap.Text = Dátum.Value.ToString("yyyy.MM.dd");
            Gombokfel();
            Kiirtáblák();
        }
        #endregion


        #region Nézetek váltása
        private void TáblákKi()
        {
            Tábla.Visible = false;
            Tábla1.Visible = false;
            Tábla2.Visible = false;
            Tábla3.Visible = false;
        }

        private void Option1_CheckedChanged(object sender, EventArgs e)
        {
            TáblákKi();
            Tábla1.Visible = true;
        }

        private void Option2_CheckedChanged(object sender, EventArgs e)
        {
            TáblákKi();
            Tábla.Visible = true;
        }

        private void Option3_CheckedChanged(object sender, EventArgs e)
        {
            TáblákKi();
            Tábla2.Visible = true;
        }

        private void Option4_CheckedChanged(object sender, EventArgs e)
        {
            TáblákKi();
            Tábla3.Visible = true;
        }

        private void Délelőtt_CheckedChanged(object sender, EventArgs e)
        {
            Választott_napszak.Text = "Délelőtt";
            Gombokfel();
            Kiirtáblák();
        }

        private void Délután_CheckedChanged(object sender, EventArgs e)
        {
            Választott_napszak.Text = "Délután";
            Gombokfel();
            Kiirtáblák();
        }
        #endregion


        #region kiirások
        private void Kiirtáblák()
        {
            try
            {
                if (Választott_Nap.Text.Trim() == "" || Választott_napszak.Text.Trim() == "" || Választott_Telephely.Text.Trim() == "") return;

                Tartalékokkiírása();
                Kiirforgalomban();
                Kiirjavításon();
                Kiirösszesítő();

                // alaphelyzet
                Tábla1.Visible = true;
                Tábla.Visible = false;
                Tábla2.Visible = false;
                Tábla3.Visible = false;
                Option1.Checked = true;
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

        private void Kiirjavításon()
        {
            try
            {
                List<Adat_Főkönyv_Nap> Adatok = FN_Kéz.Lista_Adatok(Választott_Telephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");

                // fejléc
                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 4;

                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Típus";
                Tábla.Columns[0].Width = 100;
                Tábla.Columns[1].HeaderText = "Psz";
                Tábla.Columns[1].Width = 70;
                Tábla.Columns[2].HeaderText = "Dátum";
                Tábla.Columns[2].Width = 150;
                Tábla.Columns[3].HeaderText = "Javítás leírása";
                Tábla.Columns[3].Width = 670;

                string[] fejléc = { "Kocsiszíni javítás",
                                    "Telephelyen kívüli javítás",
                                    "Félreállítás",
                                    "Főjavítás"            };
                Adatok = (from a in Adatok
                          where a.Státus == 4
                          orderby a.Típus, a.Azonosító
                          select a).ToList();

                bool kell = false;
                for (int k = 0; k < fejléc.Length; k++)
                {
                    Tábla.RowCount++;
                    int i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[3].Value = fejléc[k];
                    foreach (Adat_Főkönyv_Nap item in Adatok)
                    {
                        switch (k)
                        {
                            case 0:
                                if (!item.Hibaleírása.Contains("§") && !item.Hibaleírása.Contains("&") && !item.Hibaleírása.Contains("#"))
                                    kell = true;
                                else
                                    kell = false;
                                break;
                            case 1:
                                if (item.Hibaleírása.Contains("§"))
                                    kell = true;
                                else
                                    kell = false;
                                break;
                            case 2:
                                if (item.Hibaleírása.Contains("&"))
                                    kell = true;
                                else
                                    kell = false;
                                break;
                            case 3:
                                if (item.Hibaleírása.Contains("#"))
                                    kell = true;
                                else
                                    kell = false;
                                break;
                        }
                        if (kell)
                        {
                            Tábla.RowCount++;
                            i = Tábla.RowCount - 1;
                            Tábla.Rows[i].Cells[1].Value = item.Azonosító.Trim();
                            Tábla.Rows[i].Cells[0].Value = item.Típus.Trim();
                            Tábla.Rows[i].Cells[2].Value = item.Miótaáll.ToString("yyyy.MM.dd");
                            Tábla.Rows[i].Cells[3].Value = item.Hibaleírása.Trim();
                        }
                    }

                }
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

        private void Tábla_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (Tábla.Rows.Count > 1)
            {
                foreach (DataGridViewRow row in Tábla.Rows)
                {
                    if (row.Cells[1].Value == null)
                    {
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                        row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f, FontStyle.Bold);
                    }
                }
            }
        }
        #endregion


        #region Digitális Listázás másként
        private void Kiirösszesítő()
        {
            try
            {
                List<Adat_Kiadás_összesítő> Adatok = KézKiadási.Lista_Adatok(Választott_Telephely.Text.Trim(), Dátum.Value.Year);
                Adatok = (from a in Adatok
                          where a.Dátum == Dátum.Value
                          orderby a.Napszak, a.Típus
                          select a).ToList();
                if (Délelőtt.Checked)
                    Adatok = Adatok.Where(a => a.Napszak == "de").ToList();
                else
                    Adatok = Adatok.Where(a => a.Napszak == "du").ToList();

                List<Adat_Forte_Kiadási_Adatok> AdatokKiadási = KézForteKiadás.Lista_Adatok(Dátum.Value.Year);


                Tábla2.Rows.Clear();
                Tábla2.Columns.Clear();
                Tábla2.Refresh();
                Tábla2.Visible = false;
                Tábla2.ColumnCount = 12;

                // fejléc elkészítése
                Tábla2.Columns[0].HeaderText = "Napszak";
                Tábla2.Columns[0].Width = 80;
                Tábla2.Columns[1].HeaderText = "Típus";
                Tábla2.Columns[1].Width = 100;
                Tábla2.Columns[2].HeaderText = "Elt";
                Tábla2.Columns[2].Width = 40;
                Tábla2.Columns[3].HeaderText = "Kiadási igény";
                Tábla2.Columns[3].Width = 140;
                Tábla2.Columns[4].HeaderText = "Forgalomban";
                Tábla2.Columns[4].Width = 120;
                Tábla2.Columns[5].HeaderText = "Tartalék";
                Tábla2.Columns[5].Width = 70;
                Tábla2.Columns[6].HeaderText = "Kocsiszíni";
                Tábla2.Columns[6].Width = 90;
                Tábla2.Columns[7].HeaderText = "Félreáll.";
                Tábla2.Columns[7].Width = 70;
                Tábla2.Columns[8].HeaderText = "Főjavítás";
                Tábla2.Columns[8].Width = 75;
                Tábla2.Columns[9].HeaderText = "Összesen";
                Tábla2.Columns[9].Width = 80;
                Tábla2.Columns[10].HeaderText = "Személyzethiány";
                Tábla2.Columns[10].Width = 140;
                Tábla2.Columns[11].HeaderText = "Munkanap";
                Tábla2.Columns[11].Width = 100;

                foreach (Adat_Kiadás_összesítő elem in Adatok)
                {
                    Tábla2.RowCount++;
                    int i = Tábla2.RowCount - 1;
                    Tábla2.Rows[i].Cells[0].Value = elem.Napszak.Trim();
                    Tábla2.Rows[i].Cells[1].Value = elem.Típus.Trim();
                    int munkanap = AdatokKiadási.Count(a =>
                        a.Dátum.Date == Dátum.Value.Date &&
                        a.Napszak.Trim() == elem.Napszak.Trim() &&
                        a.Telephely.Trim() == Választott_Telephely.Text.Trim() &&
                        a.Típus.Trim() == elem.Típus.Trim());

                    Tábla2.Rows[i].Cells[11].Value = (munkanap == 0) ? "Munkanap" : "Hétvége";

                    long kiadás = AdatokKiadási
                         .Where(a =>
                         a.Dátum.Date == Dátum.Value.Date &&
                         a.Napszak.Trim() == elem.Napszak.Trim() &&
                         a.Telephely.Trim() == Választott_Telephely.Text.Trim() &&
                         a.Típus.Trim() == elem.Típus.Trim())
                        .Sum(a => a.Kiadás);

                    Tábla2.Rows[i].Cells[2].Value = elem.Forgalomban - kiadás;
                    Tábla2.Rows[i].Cells[3].Value = kiadás;
                    Tábla2.Rows[i].Cells[4].Value = elem.Forgalomban;
                    Tábla2.Rows[i].Cells[5].Value = elem.Tartalék + elem.Személyzet;
                    Tábla2.Rows[i].Cells[6].Value = elem.Kocsiszíni;
                    Tábla2.Rows[i].Cells[7].Value = elem.Félreállítás;
                    Tábla2.Rows[i].Cells[8].Value = elem.Főjavítás;
                    Tábla2.Rows[i].Cells[9].Value = elem.Forgalomban + elem.Tartalék + elem.Kocsiszíni + elem.Félreállítás + elem.Főjavítás + elem.Személyzet;
                    Tábla2.Rows[i].Cells[10].Value = elem.Személyzet;
                }
                Tábla2.Refresh();
                Tábla2.Visible = true;
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

        private void Tábla2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            for (int j = 0; j < Tábla2.RowCount; j++)
            {
                if (int.Parse(Tábla2.Rows[j].Cells[2].Value.ToString()) == 0)
                {
                    Tábla2.Rows[j].Cells[2].Style.BackColor = Color.Green;
                }
                if (int.Parse(Tábla2.Rows[j].Cells[2].Value.ToString()) < 0)
                {
                    Tábla2.Rows[j].Cells[2].Style.BackColor = Color.Red;
                }
                if (int.Parse(Tábla2.Rows[j].Cells[2].Value.ToString()) > 0)
                {
                    Tábla2.Rows[j].Cells[2].Style.BackColor = Color.Blue;
                }
            }
        }

        private void Kiirforgalomban()
        {
            try
            {
                List<Adat_Főkönyv_Nap> Adatok = FN_Kéz.Lista_Adatok(Választott_Telephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
                Adatok = (from a in Adatok
                          where a.Viszonylat != "-"
                          && a.Napszak.Trim () == (Délelőtt.Checked ? "DE" : "DU")
                          orderby a.Viszonylat, a.Tényindulás, a.Forgalmiszám, a.Azonosító
                          select a).ToList();
                // típusokat letároljuk
                List<string> típus = Adatok.OrderBy(a => a.Típus).Select(a => a.Típus).Distinct().ToList();

                // elkészítjük a fejlécet
                Tábla1.Rows.Clear();
                Tábla1.Columns.Clear();
                Tábla1.Refresh();
                Tábla1.Visible = false;
                Tábla1.ColumnCount = 11;

                Tábla1.Columns[0].HeaderText = "Viszonylat";
                Tábla1.Columns[0].Width = 100;
                Tábla1.Columns[1].Width = 60;
                Tábla1.Columns[2].Width = 60;
                Tábla1.Columns[3].Width = 60;
                Tábla1.Columns[4].Width = 60;
                Tábla1.Columns[5].Width = 60;
                Tábla1.Columns[6].Width = 60;
                Tábla1.Columns[7].Width = 60;
                Tábla1.Columns[8].Width = 60;
                Tábla1.Columns[9].Width = 60;
                Tábla1.Columns[10].Width = 60;

                foreach (string item in típus)
                {
                    Tábla1.ColumnCount++;
                    Tábla1.Columns[Tábla1.ColumnCount - 1].HeaderText = item.Trim();
                    Tábla1.Columns[Tábla1.ColumnCount - 1].Width = 80;
                }

                // forgalomban része
                string viszonylatelőző = "";
                string előzőforgalmi = "";

                int[] forgalombanösszesen = new int[16];
                int[] sordarab = new int[16];

                // lenullázzuk a darabszámokat kiirjuk a darabszámokat
                for (int j = 1; j < 15; j++)
                {
                    forgalombanösszesen[j] = 0;
                    sordarab[j] = 0;
                }

                int sor;
                int sorvég;
                int sorelőző;
                int Oszlop;
                long előzőkocsihossz;
                int nemelső;

                sor = 0;
                sorvég = 0;
                sorelőző = 0;
                Oszlop = 0;


                Tábla1.RowCount = 1;
                előzőkocsihossz = 0;
                nemelső = 0;
                foreach (Adat_Főkönyv_Nap rekord in Adatok)
                {
                    if (viszonylatelőző.Trim() != rekord.Viszonylat.Trim() && viszonylatelőző.Trim() != "")
                    {
                        // kiirjuk a darabszámokat
                        for (int j = 0; j < típus.Count; j++)
                        {
                            if (típus[j].Trim() != "")
                                Tábla1.Rows[sorvég].Cells[11 + j].Value = sordarab[j];
                        }
                        // lenullázzuk a darabszámokat
                        for (int j = 0; j < 15; j++)
                            sordarab[j] = 0;
                        // ha új viszonylat, akkor újra kezdjük
                        Oszlop = 0;
                        sorelőző = sorvég + 2;
                        sor = sorvég + 2;
                        Tábla1.RowCount += 2;
                        sorvég += 2;
                    }

                    if (viszonylatelőző.Trim() != rekord.Viszonylat.Trim())
                        viszonylatelőző = rekord.Viszonylat.Trim();

                    //Közbenső szerelvény
                    if (előzőforgalmi.Trim() != "" && előzőforgalmi.Trim() == rekord.Forgalmiszám.Trim() && rekord.Kocsikszáma > 1)
                    {
                        // ha szerelvényben közlekedik akkor új sorba írjuk
                        sor += 1;
                        if (sorelőző != sor && Oszlop == 1)
                        {
                            Tábla1.RowCount += 1;
                            sorvég = sor;
                        }
                    }

                    //Legelső szerelvény
                    if (előzőforgalmi.Trim() == "" && rekord.Kocsikszáma > 1)
                    {
                        // ha szerelvényben közlekedik akkor új sorba írjuk
                        //       sor += 1;
                        if (sorelőző != sor && Oszlop == 1)
                        {
                            Tábla1.RowCount += 1;
                            sorvég = sor;
                        }
                    }

                    if (Tábla1.RowCount <= sor)
                    {
                        Tábla1.RowCount += 1;
                        sorvég = sor;
                    }

                    if (előzőforgalmi.Trim() != rekord.Forgalmiszám.Trim() && előzőforgalmi.Trim() != "")
                    {
                        // ha új forgalmi, akkor újra kezdjük
                        Oszlop += 1;
                        sor = sorelőző;
                    }
                    if (előzőforgalmi.Trim() != rekord.Forgalmiszám.Trim())
                        előzőforgalmi = rekord.Forgalmiszám.Trim();

                    //ha oszlopok számában elértük a maximumot
                    if (Oszlop == 11)
                    {
                        // kiirjuk a darabszámokat
                        for (int j = 0; j < típus.Count; j++)
                        {
                            if (típus[j].Trim() != "")
                                Tábla1.Rows[sorvég].Cells[11 + j].Value = sordarab[j];
                        }
                        // lenullázzuk a darabszámokat
                        for (int j = 0; j < 15; j++)
                            sordarab[j] = 0;

                        Oszlop = 1;
                        sorelőző = sorvég + 2;
                        sor = sorvég + 2;
                        Tábla1.RowCount += 2;
                        sorvég += 2;
                    }

                    if (Oszlop == 0)
                        Oszlop += 1;

                    if (előzőkocsihossz < rekord.Kocsikszáma && nemelső == 0 && előzőkocsihossz != 0)
                    {
                        Tábla1.RowCount += 1;
                        sorvég = sor + 1;
                        nemelső = 1;
                    }
                    Tábla1.Rows[sor].Cells[Oszlop].Value = rekord.Azonosító.Trim();

                    // ha beálló akkor színez


                    if (rekord.Tényérkezés.Hour > 7 && rekord.Tényérkezés.Hour < 14)
                        Tábla1.Rows[sor].Cells[Oszlop].Style.BackColor = Color.Yellow;


                    // ha beállóba kért akkor dőlt betű
                    if (rekord.Státus == 3)
                        Tábla1.Rows[sor].Cells[Oszlop].Style.Font = new Font("Arial Narrow", 12f, FontStyle.Bold | FontStyle.Italic);

                    // személyzet hiány
                    if (rekord.Megjegyzés.Substring(0, 1) == "S")
                        Tábla1.Rows[sor].Cells[Oszlop].Style.BackColor = Color.OliveDrab;

                    // többlet kiadás
                    if (rekord.Megjegyzés.Substring(0, 1) == "T")
                    {
                        Tábla1.Rows[sor].Cells[Oszlop].Style.BackColor = Color.CadetBlue;
                        Tábla1.Rows[sor].Cells[Oszlop].Style.Font = new Font("Arial Narrow", 12f, FontStyle.Bold | FontStyle.Underline);
                    }

                    Tábla1.Rows[sor].Cells[0].Value = rekord.Viszonylat;
                    // emeljük a darabszámokat
                    for (int j = 0; j < típus.Count; j++)
                    {
                        if (típus[j].Trim() == rekord.Típus.Trim())
                        {
                            sordarab[j]++;
                            forgalombanösszesen[j]++;
                        }
                    }
                    előzőkocsihossz = rekord.Kocsikszáma;

                }
                // kiirjuk a darabszámokat
                for (int j = 0; j < típus.Count; j++)
                {
                    if (típus[j].Trim() != "")
                        Tábla1.Rows[sorvég].Cells[11 + j].Value = sordarab[j];
                }
                Tábla1.RowCount += 1;
                Tábla1.Rows[sorvég + 1].Cells[0].Value = "Összesen:";

                for (int j = 0; j < típus.Count; j++)
                {
                    if (típus[j].Trim() != "")
                        Tábla1.Rows[sorvég + 1].Cells[11 + j].Value = forgalombanösszesen[j];
                }
                Tábla1.Refresh();
                Tábla1.Visible = true;
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

        private void Tartalékokkiírása()
        {
            try
            {
                List<Adat_Főkönyv_Nap> Adatok = FN_Kéz.Lista_Adatok(Választott_Telephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
                Adatok = (from a in Adatok
                          where a.Viszonylat == "-"
                          orderby a.Típus ascending, a.Kocsikszáma descending, a.Szerelvény, a.Azonosító
                          select a).ToList();
                // elkészítjük a fejlécet
                // tábla formázás
                Tábla3.Rows.Clear();
                Tábla3.Columns.Clear();
                Tábla3.Refresh();
                Tábla3.Visible = false;
                Tábla3.ColumnCount = 12;
                Tábla3.RowCount = 1;
                // fejléc elkészítése
                Tábla3.Columns[0].HeaderText = "Típus";
                Tábla3.Columns[0].Width = 100;
                Tábla3.Columns[1].HeaderText = "Pályasz.";
                Tábla3.Columns[1].Width = 80;
                Tábla3.Columns[2].HeaderText = "Pályasz.";
                Tábla3.Columns[2].Width = 80;
                Tábla3.Columns[3].HeaderText = "Pályasz.";
                Tábla3.Columns[3].Width = 80;
                Tábla3.Columns[4].HeaderText = "Pályasz.";
                Tábla3.Columns[4].Width = 80;
                Tábla3.Columns[5].HeaderText = "Pályasz.";
                Tábla3.Columns[5].Width = 80;
                Tábla3.Columns[6].HeaderText = "Pályasz.";
                Tábla3.Columns[6].Width = 80;
                Tábla3.Columns[7].HeaderText = "Pályasz.";
                Tábla3.Columns[7].Width = 80;
                Tábla3.Columns[8].HeaderText = "Pályasz.";
                Tábla3.Columns[8].Width = 80;
                Tábla3.Columns[9].HeaderText = "Pályasz.";
                Tábla3.Columns[9].Width = 80;
                Tábla3.Columns[10].HeaderText = "Pályasz.";
                Tábla3.Columns[10].Width = 80;
                Tábla3.Columns[11].HeaderText = "Összesen:";
                Tábla3.Columns[11].Width = 120;

                // tartalékok kiírása
                int szerelvényhossz;
                int szerelvényhossz1;
                long előzőszerelvény;
                int oszlop1;
                int sor;

                string előzőtípus = "";
                szerelvényhossz = 0;
                szerelvényhossz1 = 0;
                előzőszerelvény = 0;
                oszlop1 = 0;
                sor = 0;
                foreach (Adat_Főkönyv_Nap rekord in Adatok)

                {
                    // az első típus
                    if (előzőtípus.Trim() == "")
                        előzőtípus = rekord.Típus.Trim();

                    // ha a másik típus lesz
                    if (előzőtípus != rekord.Típus.Trim())
                    {
                        sor = sor + 1 + szerelvényhossz1;
                        oszlop1 = 0;
                        Tábla3.RowCount = Tábla3.RowCount + 1 + szerelvényhossz1;
                        szerelvényhossz1 = szerelvényhossz;
                        Tábla3.Rows[sor + szerelvényhossz].Cells[0].Value = rekord.Típus.Trim();
                        előzőtípus = rekord.Típus.Trim();
                    }
                    // ha a kocsik száma egyenlő a szerelvény számmal akkor új oszlopba írja
                    if (rekord.Kocsikszáma <= 1 && rekord.Státus != 4)
                    {
                        oszlop1 += 1;
                        if (oszlop1 == 11)
                        {
                            sor = sor + 1 + szerelvényhossz1;
                            oszlop1 = 1;
                            Tábla3.RowCount = Tábla3.RowCount + 1 + szerelvényhossz1;
                            szerelvényhossz1 = szerelvényhossz;

                        }
                        if (szerelvényhossz1 < szerelvényhossz)
                            szerelvényhossz1 = szerelvényhossz;
                        szerelvényhossz = 0;
                    }

                    // ha más a szerelényszám akkor új oszlopba írjuk előzőszerelvény = rekord("szerelvény")
                    if (előzőszerelvény != rekord.Szerelvény && rekord.Kocsikszáma > 1)
                    {
                        oszlop1 += 1;
                        if (oszlop1 == 11)
                        {

                            sor = sor + 1 + szerelvényhossz1;
                            oszlop1 = 1;
                            Tábla3.RowCount = Tábla3.RowCount + 1 + szerelvényhossz1;
                            szerelvényhossz1 = szerelvényhossz;

                        }
                        if (szerelvényhossz1 < szerelvényhossz)
                            szerelvényhossz1 = szerelvényhossz;
                        szerelvényhossz = 0;
                    }
                    előzőszerelvény = rekord.Szerelvény;
                    if (szerelvényhossz != rekord.Kocsikszáma)
                    {
                        if (rekord.Kocsikszáma > 1)
                        {
                            Tábla3.Rows[sor + szerelvényhossz].Cells[oszlop1].Value = rekord.Azonosító.Trim();

                            if (rekord.Státus == 4)
                            {
                                // színez
                                Tábla3.Rows[sor + szerelvényhossz].Cells[oszlop1].Style.BackColor = Color.Red;
                            }

                            else
                            {
                                // számol
                                if (Tábla3.Rows[sor].Cells[11].Value == null)
                                    Tábla3.Rows[sor].Cells[11].Value = 0;
                                Tábla3.Rows[sor].Cells[11].Value = int.Parse(Tábla3.Rows[sor].Cells[11].Value.ToString()) + 1;
                            }
                            Tábla3.Rows[sor + szerelvényhossz].Cells[0].Value = rekord.Típus.Trim();
                            szerelvényhossz += 1;
                            if (Tábla3.RowCount <= sor + szerelvényhossz)
                            {
                                Tábla3.RowCount += 1;
                            }
                        }
                        else if (rekord.Státus != 4)
                        {
                            Tábla3.Rows[sor + szerelvényhossz].Cells[oszlop1].Value = rekord.Azonosító.Trim();
                            Tábla3.Rows[sor + szerelvényhossz].Cells[0].Value = rekord.Típus.Trim();

                            if (Tábla3.Rows[sor].Cells[11].Value == null)
                                Tábla3.Rows[sor].Cells[11].Value = 0;
                            Tábla3.Rows[sor].Cells[11].Value = int.Parse(Tábla3.Rows[sor].Cells[11].Value.ToString()) + 1;

                            szerelvényhossz += 1;
                            if (Tábla3.RowCount <= sor + szerelvényhossz)
                                Tábla3.RowCount += 1;
                        }
                    }
                    // ha nincs szerelvényben
                    if (rekord.Kocsikszáma == 0)
                    {
                        if (rekord.Státus != 4)
                        {

                            // ha nem álló akkor kiírja
                            Tábla3.Rows[sor + szerelvényhossz].Cells[oszlop1].Value = rekord.Azonosító.Trim();
                            Tábla3.Rows[sor + szerelvényhossz].Cells[0].Value = rekord.Típus.Trim();

                            if (Tábla3.Rows[sor].Cells[11].Value == null)
                                Tábla3.Rows[sor].Cells[11].Value = 0;
                            if (Tábla3.Rows[sor].Cells[11].Value.ToString().Trim() != "Összesen:")
                                Tábla3.Rows[sor].Cells[11].Value = int.Parse(Tábla3.Rows[sor].Cells[11].Value.ToString()) + 1;
                        }
                    }
                }
                Tábla3.Refresh();
                Tábla3.Visible = true;
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

        private void Járgomb_Click(object sender, EventArgs e)
        {
            try
            {
                Választott_Nap.Text = Dátum.Value.ToString("yyyy.MM.dd");
                Gombokfel();
                Kiirtáblák();
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

        private void Button1_Click(object sender, EventArgs e)
        {
            Tábla.Visible = true;
            if (Panel5.Visible == false)
            {
                Panel5.Visible = true;
                Panel3.Visible = false;
                Választott_Nap.Visible = false;
                Választott_napszak.Visible = false;
                Választott_Telephely.Visible = false;
                Járgomb.Visible = false;
            }
            else
            {
                Panel5.Visible = false;
                Panel3.Visible = true;
                Választott_Nap.Visible = true;
                Választott_napszak.Visible = true;
                Választott_Telephely.Visible = true;
                Járgomb.Visible = true;
            }
        }
        #endregion


        #region Keresés
        private void Becsukja_Click(object sender, EventArgs e)
        {
            Kereső.Visible = false;
        }

        private void Keresőnév_MouseMove(object sender, MouseEventArgs e)
        {
            // egér bal gomb hatására a groupbox1 bal felső sarkánál fogva mozgatja a lapot.
            if (e.Button == MouseButtons.Left)
            {
                Kereső.Top = Top + Kereső.Top + e.Y;
                Kereső.Left = Left + Kereső.Left + e.X;
            }
        }

        private void Szövegkeresés()
        {
            if (TextKeres_Text.Text.Trim() == "") return;

            if (Tábla.Visible == true)
            {
                // megkeressük a szöveget a táblázatban

                if (Tábla.Rows.Count < 0)
                    return;
                for (int i = 0; i < Tábla.Rows.Count; i++)
                {
                    for (int j = 0; j < Tábla.Columns.Count; j++)
                    {
                        if (Tábla.Rows[i].Cells[j].Value.ToString().Trim() == TextKeres_Text.Text.Trim())
                        {
                            Tábla.Rows[i].Cells[j].Style.BackColor = Color.Orange;
                            Tábla.FirstDisplayedScrollingRowIndex = i;
                            return;
                        }
                    }
                }

            }
            if (Tábla1.Visible == true)
            {
                // megkeressük a szöveget a táblázatban

                if (Tábla1.Rows.Count < 0)
                    return;
                for (int i = 0; i < Tábla1.Rows.Count; i++)
                {
                    for (int j = 0; j < Tábla1.Columns.Count; j++)
                    {
                        if (Tábla1.Rows[i].Cells[j].Value.ToString().Trim() == TextKeres_Text.Text.Trim())
                        {
                            Tábla1.Rows[i].Cells[j].Style.BackColor = Color.Orange;
                            Tábla1.FirstDisplayedScrollingRowIndex = i;
                            return;
                        }
                    }
                }

            }
            if (Tábla2.Visible == true)
            {
                // megkeressük a szöveget a táblázatban


                if (Tábla2.Rows.Count < 0)
                    return;
                for (int i = 0; i < Tábla2.Rows.Count; i++)
                {
                    for (int j = 0; j < Tábla2.Columns.Count; j++)
                    {
                        if (Tábla2.Rows[i].Cells[j].Value.ToString().Trim() == TextKeres_Text.Text.Trim())
                        {
                            Tábla2.Rows[i].Cells[j].Style.BackColor = Color.Orange;
                            Tábla2.FirstDisplayedScrollingRowIndex = i;
                            return;
                        }
                    }
                }

            }
            if (Tábla3.Visible == true)
            {
                // megkeressük a szöveget a táblázatban
                {

                    if (Tábla3.Rows.Count < 0)
                        return;
                    for (int i = 0; i < Tábla3.Rows.Count; i++)
                    {
                        for (int j = 0; j < Tábla3.Columns.Count; j++)
                        {
                            if (Tábla3.Rows[i].Cells[j].Value.ToString().Trim() == TextKeres_Text.Text.Trim())
                            {
                                Tábla3.Rows[i].Cells[j].Style.BackColor = Color.Orange;
                                Tábla3.FirstDisplayedScrollingRowIndex = i;
                                return;
                            }
                        }
                    }
                }
            }

        }

        private void BtnKeres_command2_Click(object sender, EventArgs e)
        {
            Szövegkeresés();
        }
        #endregion


        #region Listázás másként
        private void Típusfeltöltés_melyik()
        {
            try
            {
                Típuslista.Items.Clear();
                List<Adat_Jármű> Adatok = Kéz_Jármű.Lista_Adatok("Főmérnökség");
                List<string> Típusok = Adatok.OrderBy(a => a.Valóstípus).Select(a => a.Valóstípus).Distinct().ToList();
                foreach (string Elem in Típusok)
                    Típuslista.Items.Add(Elem);

                Típuslista.Refresh();
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

        private void Telephelyfeltöltés_Melyik()
        {
            try
            {
                List<Adat_kiegészítő_telephely> Adatok = KézKiegTelephely.Lista_Adatok();
                Adatok = Adatok.OrderBy(a => a.Telephelykönyvtár).ToList();

                Telephelykönyvtár.Clear();
                foreach (Adat_kiegészítő_telephely Elem in Adatok)
                    Telephelykönyvtár.Add(Elem.Telephelykönyvtár);
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

        private void CsoportkijelölMind_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Típuslista.Items.Count - 1; i++)
                Típuslista.SetItemChecked(i, true);
        }

        private void CsoportVissza_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Típuslista.Items.Count - 1; i++)
                Típuslista.SetItemChecked(i, false);
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton2.Checked == true)
            {
                Tábla.Visible = true;
                Tábla3.Visible = false;
            }
            else
            {
                Tábla.Visible = false;
                Tábla3.Visible = true;
            }
        }

        private void Command3_Click(object sender, EventArgs e)
        {
            try
            {

                if (Típuslista.CheckedItems.Count == 0) return;
                Holtart.Be(20);

                List<Adat_Jármű> AdatokT = Kéz_Jármű.Lista_Adatok("Főmérnökség");
                AdatokT = (from a in AdatokT
                           orderby a.Valóstípus, a.Üzem, a.Azonosító
                           select a).ToList();

                List<Adat_Jármű> Adatok = new List<Adat_Jármű>();
                for (int i = 0; i < Típuslista.CheckedItems.Count; i++)
                {
                    List<Adat_Jármű> AdatokIdeig = AdatokT.Where(a => a.Valóstípus.Trim() == Típuslista.CheckedItems[i].ToStrTrim()).ToList();
                    Adatok.AddRange(AdatokIdeig);
                }
                Adatok = (from a in Adatok
                          where a.Törölt == false
                          orderby a.Valóstípus, a.Üzem, a.Azonosító
                          select a).ToList();

                TáblákKi();

                Tábla1.Rows.Clear();
                Tábla1.Columns.Clear();
                Tábla1.Refresh();
                Tábla1.Visible = false;
                Tábla1.ColumnCount = 9;

                // fejléc elkészítése
                Tábla1.Columns[0].HeaderText = "P.sz.";
                Tábla1.Columns[0].Width = 70;
                Tábla1.Columns[1].HeaderText = "Típus";
                Tábla1.Columns[1].Width = 70;
                Tábla1.Columns[2].HeaderText = "Telephely";
                Tábla1.Columns[2].Width = 110;
                Tábla1.Columns[3].HeaderText = "Beírás";
                Tábla1.Columns[3].Width = 400;
                Tábla1.Columns[4].HeaderText = "Viszonylat";
                Tábla1.Columns[4].Width = 100;
                Tábla1.Columns[5].HeaderText = "Forgalmi";
                Tábla1.Columns[5].Width = 100;

                if (RadioButton4.Checked == true)
                {
                    Tábla1.Columns[6].HeaderText = "Terv indulás";
                    Tábla1.Columns[6].Width = 100;
                    Tábla1.Columns[7].HeaderText = "Terv érkezés";
                    Tábla1.Columns[7].Width = 100;
                }
                else
                {
                    Tábla1.Columns[6].HeaderText = "Terv indulás";
                    Tábla1.Columns[6].Width = 165;
                    Tábla1.Columns[7].HeaderText = "Terv érkezés";
                    Tábla1.Columns[7].Width = 165;
                }
                Tábla1.Columns[8].HeaderText = "Státus";
                Tábla1.Columns[8].Width = 80;

                Tábla1.Columns[3].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                Tábla1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                int sor = 0;
                List<Adat_Főkönyv_Nap> Fadatok = new List<Adat_Főkönyv_Nap>();
                for (int i = 0; i < Telephelykönyvtár.Count; i++)
                {
                    List<Adat_Főkönyv_Nap> FadatokIdeig = FN_Kéz.Lista_Adatok(Telephelykönyvtár[i].ToStrTrim(), Dátum_Melyik.Value, "de", false);
                    Fadatok.AddRange(FadatokIdeig);
                }

                foreach (Adat_Jármű rekord in Adatok)
                {
                    sor += 1;
                    Tábla1.RowCount = sor + 1;
                    Tábla1.Rows[sor - 1].Cells[0].Value = rekord.Azonosító.Trim();
                    Tábla1.Rows[sor - 1].Cells[1].Value = rekord.Valóstípus.Trim();

                    Adat_Főkönyv_Nap FadatokAzon = Fadatok.FirstOrDefault(a => a.Azonosító.Trim() == rekord.Azonosító.Trim());
                    if (FadatokAzon != null)
                    {
                        Tábla1.Rows[sor - 1].Cells[8].Value = FadatokAzon.Státus.ToString();
                        switch (FadatokAzon.Státus)
                        {
                            case 3:
                                {
                                    Tábla1.Rows[sor - 1].Cells[0].Style.BackColor = Color.Yellow;
                                    break;
                                }

                            case 4:
                                {
                                    Tábla1.Rows[sor - 1].Cells[0].Style.BackColor = Color.Red;
                                    break;
                                }
                        }
                        Tábla1.Rows[sor - 1].Cells[2].Value = FadatokAzon.Telephely.Trim();
                        Tábla1.Rows[sor - 1].Cells[3].Value = FadatokAzon.Hibaleírása.Trim();
                        Tábla1.Rows[sor - 1].Cells[4].Value = FadatokAzon.Viszonylat.Trim();
                        Tábla1.Rows[sor - 1].Cells[5].Value = FadatokAzon.Forgalmiszám.Trim();
                        if (RadioButton4.Checked == true)
                        {
                            if (FadatokAzon.Tervindulás != new DateTime(1900, 1, 1))
                                Tábla1.Rows[sor - 1].Cells[6].Value = FadatokAzon.Tervindulás.ToString("hh:mm");

                            if (FadatokAzon.Tervérkezés != new DateTime(1900, 1, 1))
                                Tábla1.Rows[sor - 1].Cells[7].Value = FadatokAzon.Tervérkezés.ToString("hh:mm");
                        }
                        else
                        {
                            Tábla1.Rows[sor - 1].Cells[6].Value = FadatokAzon.Tervindulás;
                            Tábla1.Rows[sor - 1].Cells[7].Value = FadatokAzon.Tervérkezés;
                        }
                    }
                    Holtart.Lép();
                }
                Tábla1.Visible = true;
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

        private void Command4_Click(object sender, EventArgs e)
        {
            try
            {
                RadioButton2.Checked = true;
                if (Típuslista.CheckedItems.Count == 0) return;

                TáblákKi();
                Holtart.Be(Telephelykönyvtár.Count + 2);

                // áttöltjük az adatokat a villamos táblából
                List<Adat_Jármű> Adatok = Kéz_Jármű.Lista_Adatok("Főmérnökség");

                List<Adat_Összevont> ÖAdatokÖ = new List<Adat_Összevont>();
                for (int i = 0; i < Telephelykönyvtár.Count; i++)
                {

                    List<Adat_Főkönyv_Nap> FAdatok = FN_Kéz.Lista_Adatok(Telephelykönyvtár[i].ToStrTrim(), Dátum_Melyik.Value, "de");
                    foreach (Adat_Főkönyv_Nap Elem in FAdatok)
                    {
                        Adat_Jármű Jármű = Adatok.FirstOrDefault(a => a.Azonosító.Trim() == Elem.Azonosító.Trim());
                        DateTime MiótaÁll = new DateTime(1900, 1, 1);
                        DateTime Üzembehelyezés = new DateTime(1900, 1, 1);
                        string Valóstípus = "";
                        if (Jármű != null)
                        {
                            MiótaÁll = Jármű.Miótaáll;
                            Valóstípus = Jármű.Valóstípus;
                            Üzembehelyezés = Jármű.Üzembehelyezés;
                        }


                        Adat_Összevont ADAT = new Adat_Összevont(
                                       Elem.Azonosító.Trim(),
                                       Elem.Státus,
                                       Telephelykönyvtár[i].ToStrTrim(),
                                       MiótaÁll,
                                       Valóstípus,
                                       Üzembehelyezés,
                                       Elem.Hibaleírása.Trim());
                        ÖAdatokÖ.Add(ADAT);
                        Holtart.Lép();
                    }
                }
                TáblaMásik(ÖAdatokÖ);
                Tábla1Másik(ÖAdatokÖ);
                Tábla.Visible = true;
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

        private void TáblaMásik(List<Adat_Összevont> ÖAdatokÖ)
        {
            try
            {
                TáblaFejléc();

                Holtart.Be(20);
                // kiírjuk az üzemképes kocsikat
                Tábla.RowCount += 1;
                int sor = Tábla.RowCount - 1;
                Tábla.Rows[0].Cells[0].Value = "Üzemképes";
                List<Adat_Összevont> ÖAdatokIdeig = (from a in ÖAdatokÖ
                                                     where a.Státus != 4
                                                     select a).ToList();
                List<Adat_Összevont> ÖAdatokE = new List<Adat_Összevont>();
                for (int ii = 0; ii < Típuslista.CheckedItems.Count; ii++)
                {
                    List<Adat_Összevont> Ideig = ÖAdatokIdeig.Where(a => a.Valóstípus.Trim() == Típuslista.CheckedItems[ii].ToStrTrim()).ToList();
                    ÖAdatokE.AddRange(Ideig);
                }
                ÖAdatokE = (from a in ÖAdatokE
                            orderby a.Valóstípus, a.Üzem, a.Azonosító
                            select a).ToList();
                sor = TáblaÍrás(ÖAdatokE, sor);

                // üzemképtelen kocsik
                Tábla.RowCount++;
                sor = Tábla.RowCount - 1;
                Tábla.Rows[sor].Cells[0].Value = "Üzemképtelen";
                ÖAdatokIdeig = (from a in ÖAdatokÖ
                                where a.Státus == 4
                                && !a.Hibaleírása.Contains("§")
                                && !a.Hibaleírása.Contains("#")
                                && !a.Hibaleírása.Contains("&")
                                select a).ToList();
                ÖAdatokE.Clear();
                for (int ii = 0; ii < Típuslista.CheckedItems.Count; ii++)
                {
                    List<Adat_Összevont> Ideig = ÖAdatokIdeig.Where(a => a.Valóstípus.Trim() == Típuslista.CheckedItems[ii].ToStrTrim()).ToList();
                    ÖAdatokE.AddRange(Ideig);
                }
                ÖAdatokE = (from a in ÖAdatokE
                            orderby a.Valóstípus, a.Üzem, a.Azonosító
                            select a).ToList();
                sor = TáblaÍrás(ÖAdatokE, sor);

                // ***   Telepkívül ****
                Tábla.RowCount++;
                sor = Tábla.RowCount - 1;
                Tábla.Rows[sor].Cells[0].Value = "Telepkívül";
                ÖAdatokIdeig = (from a in ÖAdatokÖ
                                where a.Státus == 4
                                && a.Hibaleírása.Contains("§")
                                select a).ToList();
                ÖAdatokE.Clear();
                for (int ii = 0; ii < Típuslista.CheckedItems.Count; ii++)
                {
                    List<Adat_Összevont> Ideig = ÖAdatokIdeig.Where(a => a.Valóstípus.Trim() == Típuslista.CheckedItems[ii].ToStrTrim()).ToList();
                    ÖAdatokE.AddRange(Ideig);
                }
                ÖAdatokE = (from a in ÖAdatokE
                            orderby a.Valóstípus, a.Üzem, a.Azonosító
                            select a).ToList();
                sor = TáblaÍrás(ÖAdatokE, sor);

                // ***   Főjavítás ****
                Tábla.RowCount++;
                sor = Tábla.RowCount - 1;
                Tábla.Rows[sor].Cells[0].Value = "Főjavítás";
                ÖAdatokIdeig = (from a in ÖAdatokÖ
                                where a.Státus == 4
                                && a.Hibaleírása.Contains("#")
                                select a).ToList();
                ÖAdatokE.Clear();
                for (int ii = 0; ii < Típuslista.CheckedItems.Count; ii++)
                {
                    List<Adat_Összevont> Ideig = ÖAdatokIdeig.Where(a => a.Valóstípus.Trim() == Típuslista.CheckedItems[ii].ToStrTrim()).ToList();
                    ÖAdatokE.AddRange(Ideig);
                }
                ÖAdatokE = (from a in ÖAdatokE
                            orderby a.Valóstípus, a.Üzem, a.Azonosító
                            select a).ToList();
                sor = TáblaÍrás(ÖAdatokE, sor);


                // ***   Félre állítás ****
                Tábla.RowCount++;
                sor = Tábla.RowCount - 1;
                Tábla.Rows[sor].Cells[0].Value = "Félreállítás";
                ÖAdatokIdeig = (from a in ÖAdatokÖ
                                where a.Státus == 4
                                && a.Hibaleírása.Contains("&")
                                select a).ToList();
                ÖAdatokE.Clear();
                for (int ii = 0; ii < Típuslista.CheckedItems.Count; ii++)
                {
                    List<Adat_Összevont> Ideig = ÖAdatokIdeig.Where(a => a.Valóstípus.Trim() == Típuslista.CheckedItems[ii].ToStrTrim()).ToList();
                    ÖAdatokE.AddRange(Ideig);
                }
                ÖAdatokE = (from a in ÖAdatokE
                            orderby a.Valóstípus, a.Üzem, a.Azonosító
                            select a).ToList();
                sor = TáblaÍrás(ÖAdatokE, sor);

                // összesen
                Holtart.Lép();

                sor += 1;
                Tábla.RowCount = sor + 1;
                Tábla.Rows[sor].Cells[0].Value = "Összesen";

                int oszlop = Tábla.Columns.Count - 1;
                int darab = 0;
                for (int ik = 1; ik < Tábla.Rows.Count; ik++)
                    if (Tábla.Rows[ik].Cells[oszlop].Value != null && int.TryParse(Tábla.Rows[ik].Cells[oszlop].Value.ToString(), out int result)) darab += result;

                if (darab != 0)
                {
                    if (oszlop == 2)
                        Tábla.Rows[sor - 1].Cells[12].Value = darab;
                    else
                        Tábla.Rows[sor].Cells[12].Value = darab;
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

        private int TáblaÍrás(List<Adat_Összevont> Adatok, int sor)
        {
            try
            {
                string előző = "";
                string előzőüzem = "";
                int oszlop = 2;
                int darab = 0;

                foreach (Adat_Összevont rekord in Adatok)
                {

                    if (előző.Trim() == "")
                    {
                        előző = rekord.Valóstípus.Trim();
                        előzőüzem = rekord.Üzem.Trim();
                        sor += 1;
                        Tábla.RowCount = sor + 1;
                    }
                    // ha új típust ír ki
                    if (előző.Trim() != rekord.Valóstípus.Trim())
                    {
                        if (oszlop == 2)
                            Tábla.Rows[sor - 1].Cells[12].Value = darab;
                        else
                            Tábla.Rows[sor].Cells[12].Value = darab;

                        darab = 0;
                        előző = rekord.Valóstípus.Trim();
                        előzőüzem = rekord.Üzem.Trim();
                        sor += 1;
                        Tábla.RowCount = sor + 1;
                        oszlop = 2;
                    }
                    // ha másik üzemben van akkor új sor
                    if (előzőüzem.Trim() != rekord.Üzem.Trim())
                    {
                        Tábla.Rows[sor].Cells[12].Value = darab;
                        darab = 0;
                        sor += 1;
                        Tábla.RowCount = sor + 1;
                        oszlop = 2;
                        előzőüzem = rekord.Üzem.Trim();
                    }

                    Tábla.Rows[sor].Cells[0].Value = rekord.Valóstípus.Trim();
                    Tábla.Rows[sor].Cells[1].Value = rekord.Üzem.Trim();
                    Tábla.Rows[sor].Cells[oszlop].Value = rekord.Azonosító.Trim();

                    oszlop += 1;
                    darab += 1;

                    if (oszlop == 12)
                    {
                        oszlop = 2;
                        sor += 1;
                        Tábla.RowCount = sor + 1;
                    }
                    Holtart.Lép();

                }
                if (oszlop == 2)
                    Tábla.Rows[sor - 1].Cells[12].Value = darab;
                else
                    Tábla.Rows[sor].Cells[12].Value = darab;

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
            return sor;
        }

        private void TáblaFejléc()
        {
            // kiírjuk az adatokat
            Tábla.Rows.Clear();
            Tábla.Columns.Clear();
            Tábla.Refresh();
            Tábla.Visible = false;
            Tábla.ColumnCount = 13;

            // fejléc elkészítése
            Tábla.Columns[0].HeaderText = "Típus";
            Tábla.Columns[0].Width = 100;
            Tábla.Columns[1].HeaderText = "Telephely";
            Tábla.Columns[1].Width = 100;
            Tábla.Columns[2].HeaderText = "";
            Tábla.Columns[2].Width = 60;
            Tábla.Columns[3].HeaderText = "";
            Tábla.Columns[3].Width = 60;
            Tábla.Columns[4].HeaderText = "";
            Tábla.Columns[4].Width = 60;
            Tábla.Columns[5].HeaderText = "";
            Tábla.Columns[5].Width = 60;
            Tábla.Columns[6].HeaderText = "";
            Tábla.Columns[6].Width = 60;
            Tábla.Columns[7].HeaderText = "";
            Tábla.Columns[7].Width = 60;
            Tábla.Columns[8].HeaderText = "";
            Tábla.Columns[8].Width = 60;
            Tábla.Columns[9].HeaderText = "";
            Tábla.Columns[9].Width = 60;
            Tábla.Columns[10].HeaderText = "";
            Tábla.Columns[10].Width = 60;
            Tábla.Columns[11].HeaderText = "";
            Tábla.Columns[11].Width = 60;
            Tábla.Columns[12].HeaderText = "Darabszám";
            Tábla.Columns[12].Width = 100;
        }

        private void Tábla1Másik(List<Adat_Összevont> Adatok)
        {
            try
            {
                // Részletes tábla
                Tábla1Fejléc();

                Tábla3.RowCount += 2;
                int sor = 0;
                Tábla3.Rows[sor].Cells[4].Value = "Kocsiszíni javítás";
                List<Adat_Összevont> Szűrt = (from a in Adatok
                                              where a.Státus == 4
                                              && !a.Hibaleírása.Contains("§")
                                              && !a.Hibaleírása.Contains("#")
                                              && !a.Hibaleírása.Contains("&")
                                              select a).ToList();

                List<Adat_Összevont> ListaAdatok = new List<Adat_Összevont>();
                for (int ij = 0; ij < Típuslista.CheckedItems.Count; ij++)
                {
                    List<Adat_Összevont> Ideig = Szűrt.Where(a => a.Valóstípus.Trim() == Típuslista.CheckedItems[ij].ToStrTrim()).ToList();
                    ListaAdatok.AddRange(Ideig);
                }
                ListaAdatok = (from a in ListaAdatok
                               orderby a.Valóstípus, a.Üzem, a.Azonosító
                               select a).ToList();
                sor = Tábla1Írás(ListaAdatok, sor);

                sor += 1;
                Tábla3.RowCount = sor + 1;
                Tábla3.Rows[sor].Cells[4].Value = "Telephelyen kívüli javítás";
                Szűrt = (from a in Adatok
                         where a.Státus == 4
                         && a.Hibaleírása.Contains("§")
                         select a).ToList();

                ListaAdatok = new List<Adat_Összevont>();
                for (int ij = 0; ij < Típuslista.CheckedItems.Count; ij++)
                {
                    List<Adat_Összevont> Ideig = Szűrt.Where(a => a.Valóstípus.Trim() == Típuslista.CheckedItems[ij].ToStrTrim()).ToList();
                    ListaAdatok.AddRange(Ideig);
                }
                ListaAdatok = (from a in ListaAdatok
                               orderby a.Valóstípus, a.Üzem, a.Azonosító
                               select a).ToList();
                sor = Tábla1Írás(ListaAdatok, sor);

                sor += 1;
                Tábla3.RowCount = sor + 1;
                Tábla3.Rows[sor].Cells[4].Value = "Félreállítás";
                Szűrt = (from a in Adatok
                         where a.Státus == 4
                         && a.Hibaleírása.Contains("&")
                         select a).ToList();

                ListaAdatok = new List<Adat_Összevont>();
                for (int ij = 0; ij < Típuslista.CheckedItems.Count; ij++)
                {
                    List<Adat_Összevont> Ideig = Szűrt.Where(a => a.Valóstípus.Trim() == Típuslista.CheckedItems[ij].ToStrTrim()).ToList();
                    ListaAdatok.AddRange(Ideig);
                }
                ListaAdatok = (from a in ListaAdatok
                               orderby a.Valóstípus, a.Üzem, a.Azonosító
                               select a).ToList();
                sor = Tábla1Írás(ListaAdatok, sor);

                sor += 1;
                Tábla3.RowCount = sor + 1;
                Tábla3.Rows[sor].Cells[4].Value = "Főjavítás";
                Szűrt = (from a in Adatok
                         where a.Státus == 4
                         && a.Hibaleírása.Contains("#")
                         select a).ToList();

                ListaAdatok = new List<Adat_Összevont>();
                for (int ij = 0; ij < Típuslista.CheckedItems.Count; ij++)
                {
                    List<Adat_Összevont> Ideig = Szűrt.Where(a => a.Valóstípus.Trim() == Típuslista.CheckedItems[ij].ToStrTrim()).ToList();
                    ListaAdatok.AddRange(Ideig);
                }
                ListaAdatok = (from a in ListaAdatok
                               orderby a.Valóstípus, a.Üzem, a.Azonosító
                               select a).ToList();
                sor = Tábla1Írás(ListaAdatok, sor);
                Holtart.Ki();
                Tábla3_Formázás();
                Tábla3.Refresh();
                Tábla3.Visible = false;

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

        private int Tábla1Írás(List<Adat_Összevont> Adatok, int sor)
        {
            try
            {
                foreach (Adat_Összevont rekord in Adatok)
                {
                    sor += 1;
                    Tábla3.RowCount = sor + 1;
                    Tábla3.Rows[sor].Cells[0].Value = rekord.Valóstípus.Trim();
                    Tábla3.Rows[sor].Cells[1].Value = rekord.Üzem.Trim();
                    Tábla3.Rows[sor].Cells[2].Value = rekord.Azonosító.Trim();
                    Tábla3.Rows[sor].Cells[3].Value = rekord.Miótaáll.ToString("yyyy.MM.dd");
                    Tábla3.Rows[sor].Cells[4].Value = rekord.Hibaleírása.Trim();
                    Holtart.Lép();
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
            return sor;
        }

        private void Tábla1Fejléc()
        {
            Tábla3.Rows.Clear();
            Tábla3.Columns.Clear();
            Tábla3.Refresh();
            Tábla3.Visible = false;
            Tábla3.ColumnCount = 5;

            // fejléc elkészítése
            Tábla3.Columns[0].HeaderText = "Típus";
            Tábla3.Columns[0].Width = 100;
            Tábla3.Columns[1].HeaderText = "Telephely";
            Tábla3.Columns[1].Width = 100;
            Tábla3.Columns[2].HeaderText = "Psz";
            Tábla3.Columns[2].Width = 100;
            Tábla3.Columns[3].HeaderText = "Dátum";
            Tábla3.Columns[3].Width = 100;
            Tábla3.Columns[4].HeaderText = "Javítás leírása";
            Tábla3.Columns[4].Width = 700;
            Tábla3.Columns[4].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            Tábla3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void Excel_Melyik_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.Visible == true & Tábla.Rows.Count <= 0) return;
                if (Tábla1.Visible == true & Tábla1.Rows.Count <= 0) return;
                if (Tábla2.Visible == true & Tábla2.Rows.Count <= 0) return;
                if (Tábla3.Visible == true & Tábla3.Rows.Count <= 0) return;

                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Járművek_Telephelyek_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                if (Tábla.Visible) MyX.DataGridViewToXML(fájlexc, Tábla);
                if (Tábla1.Visible) MyX.DataGridViewToXML(fájlexc, Tábla1);
                if (Tábla2.Visible) MyX.DataGridViewToXML(fájlexc, Tábla2);
                if (Tábla3.Visible) MyX.DataGridViewToXML(fájlexc, Tábla3);

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
        #endregion

        private void Tábla3_Formázás()
        {
            if (Tábla3.Rows.Count == 0) return;

            for (int i = 0; i < Tábla3.Rows.Count; i++)
            {
                if (Tábla3.Rows[i].Cells[0].Value.ToStrTrim() == "")
                {
                    for (int j = 0; j < Tábla3.Columns.Count; j++)
                    {
                        Tábla3.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                        Tábla3.Rows[i].Cells[j].Style.BackColor = Color.Yellow;
                        Tábla3.Rows[i].Cells[j].Style.Font = new Font("Arial Narrow", 12f, FontStyle.Bold);
                    }

                }
            }
        }
    }
}