﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos.Kezelők;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{

    public partial class Ablak_Digitális_Főkönyv
    {
        public Ablak_Digitális_Főkönyv()
        {
            InitializeComponent();
        }

        readonly Kezelő_kiegészítő_telephely KKT_Kéz = new Kezelő_kiegészítő_telephely();
        readonly Kezelő_Főkönyv_Nap FN_Kéz = new Kezelő_Főkönyv_Nap();
        readonly Kezelő_Kiadás_Összesítő KKÖ_kéz = new Kezelő_Kiadás_Összesítő();
        readonly Kezelő_Jármű KJ_kéz = new Kezelő_Jármű();
        readonly Kezelő_összevont KÖ_kéz = new Kezelő_összevont();
        readonly Kezelő_FőKiadási_adatok KFK_Kéz = new Kezelő_FőKiadási_adatok();
        readonly Kezelő_Forte_Kiadási_Adatok KézForteKiadás = new Kezelő_Forte_Kiadási_Adatok();

        private void Ablak_Digitális_Főkönyv_Load(object sender, EventArgs e)
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
        }


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
                List<Adat_kiegészítő_telephely> Adatok = KKT_Kéz.Lista_adatok();

                int i = 1;
                foreach (Adat_kiegészítő_telephely item in Adatok)
                {
                    Gombokszáma++;
                    Button Telephelygomb = new Button { Location = new Point(10, 10 + Gombokszáma * 40) };
                    Telephelygomb.Size = new Size(145, 35);
                    Telephelygomb.Name = $"Járgomb_{Gombokszáma + 1}";
                    Telephelygomb.Text = item.Telephelynév;
                    Telephelygomb.Visible = true;

                    string helytelep = $@"{Application.StartupPath}\{item.Telephelykönyvtár.Trim()}\adatok\főkönyv\kiadás{Dátum.Value:yyyy}.mdb";
                    if (File.Exists(helytelep))
                    {
                        string jelszótelep = "plédke";
                        szöveg = "SELECT * FROM tábla  ";
                        List<Adat_Kiadás_összesítő> AdatokÖsszesítő = KKÖ_kéz.Lista_adatok(helytelep, jelszótelep, szöveg);

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
            // ha gombra kattintottunk
            Button Telephelygomb = (Button)sender;
            if (sender is Button)
            {
                Választott_Telephely.Text = Telephelygomb.Text;
                Kiirtáblák();
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
        private void Option1_CheckedChanged(object sender, EventArgs e)
        {
            Tábla1.Visible = true;
            Tábla.Visible = false;
            Tábla2.Visible = false;
            Tábla3.Visible = false;
        }


        private void Option2_CheckedChanged(object sender, EventArgs e)
        {
            Tábla.Visible = true;
            Tábla1.Visible = false;
            Tábla2.Visible = false;
            Tábla3.Visible = false;
        }


        private void Option3_CheckedChanged(object sender, EventArgs e)
        {
            Tábla.Visible = false;
            Tábla1.Visible = false;
            Tábla2.Visible = true;
            Tábla3.Visible = false;
        }


        private void Option4_CheckedChanged(object sender, EventArgs e)
        {
            Tábla.Visible = false;
            Tábla1.Visible = false;
            Tábla2.Visible = false;
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
                if (Választott_Nap.Text.Trim() == "" || Választott_napszak.Text.Trim() == "" || Választott_Telephely.Text.Trim() == "")
                    return;

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
                string hely, jelszó, szöveg;
                if (Délelőtt.Checked)
                    hely = $@"{Application.StartupPath}\" + Választott_Telephely.Text.Trim() + @"\adatok\főkönyv\nap\" + Dátum.Value.ToString("yyyyMMdd") + "denap.mdb";
                else
                    hely = $@"{Application.StartupPath}\" + Választott_Telephely.Text.Trim() + @"\adatok\főkönyv\nap\" + Dátum.Value.ToString("yyyyMMdd") + "dunap.mdb";

                jelszó = "lilaakác";
                if (!File.Exists(hely))
                {
                    // új hely
                    hely = $@"{Application.StartupPath}\" + Választott_Telephely.Text.Trim() + @"\adatok\főkönyv\" + Dátum.Value.ToString("yyyy") + @"\nap\" + Dátum.Value.ToString("yyyyMMdd");
                    if (Délelőtt.Checked)
                        hely += "denap.mdb";
                    else
                        hely += "dunap.mdb";

                    if (!File.Exists(hely))
                        return;
                }
                // ******************************************
                // ide kerül a kocsiszíni javítás
                // ******************************************
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

                szöveg = "SELECT * FROM adattábla where adattábla.státus=4 and (Adattábla.napszak ='-' or Adattábla.napszak ='_') ORDER BY típus,azonosító";

                List<Adat_Főkönyv_Nap> Adatok = FN_Kéz.Lista_adatok(hely, jelszó, szöveg);
                int i;
                bool kell = false;
                for (int k = 0; k < fejléc.Length; k++)
                {
                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;
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
                    if (row.Cells[0].Value == null)
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
                string hely = $@"{Application.StartupPath}\{Választott_Telephely.Text.Trim()}\adatok\főkönyv\kiadás{Dátum.Value.Year}.mdb";
                string helykiadás = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_fortekiadási_adatok.mdb";
                if (!File.Exists(hely)) return;
                if (!File.Exists(helykiadás)) return;

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
                string jelszó = "plédke";
                string jelszókiadás = "gémkapocs";
                string szöveg = "SELECT * FROM tábla where [dátum]=#" + Dátum.Value.ToString("M-d-yy") + "#";
                if (Délelőtt.Checked)
                    szöveg += " and napszak='de'";
                else
                    szöveg += " and napszak='du'";
                szöveg += " ORDER BY napszak, típus";

                List<Adat_Kiadás_összesítő> Adatok = KKÖ_kéz.Lista_adatok(hely, jelszó, szöveg);

                szöveg = "SELECT * FROM fortekiadástábla";
                List<Adat_Forte_Kiadási_Adatok> AdatokKiadási = KézForteKiadás.Lista_adatok(helykiadás, jelszókiadás, szöveg);

                int i;
                foreach (Adat_Kiadás_összesítő elem in Adatok)
                {
                    Tábla2.RowCount++;
                    i = Tábla2.RowCount - 1;
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
                // régi helye
                string hely = $@"{Application.StartupPath}\" + Választott_Telephely.Text.Trim() + @"\adatok\főkönyv\nap\" + Dátum.Value.ToString("yyyyMMdd");
                if (Délelőtt.Checked)
                {
                    hely += "denap.mdb";
                }
                else
                {
                    hely += "dunap.mdb";
                }
                if (!File.Exists(hely))
                {
                    // új hely
                    hely = $@"{Application.StartupPath}\" + Választott_Telephely.Text.Trim() + @"\adatok\főkönyv\" + Dátum.Value.ToString("yyyy") + @"\nap\" + Dátum.Value.ToString("yyyyMMdd");
                    if (Délelőtt.Checked)
                    {
                        hely += "denap.mdb";
                    }
                    else
                    {
                        hely += "dunap.mdb";
                    }
                    if (!File.Exists(hely))
                        return;
                }
                string jelszó = "lilaakác";

                // típusokat letároljuk
                string szöveg = "SELECT DISTINCT típus From Adattábla where viszonylat <> '-' order by típus";
                List<string> típus = FN_Kéz.Lista_típus(hely, jelszó, szöveg);

                // elkészítjük a fejlécet

                Tábla1.Rows.Clear();
                Tábla1.Columns.Clear();
                Tábla1.Refresh();
                //    Tábla1.Visible = false;
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
                szöveg = "SELECT * FROM adattábla where Adattábla.viszonylat <> '-' ORDER BY viszonylat,tényindulás,forgalmiszám, azonosító ";
                List<Adat_Főkönyv_Nap> Adatok = FN_Kéz.Lista_adatok(hely, jelszó, szöveg);
                string viszonylatelőző = "";
                string előzőforgalmi = "";

                int[] forgalombanösszesen = new int[16];
                int[] sordarab = new int[16];


                // lenullázzuk a darabszámokat
                // kiirjuk a darabszámokat
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
                string hely = $@"{Application.StartupPath}\" + Választott_Telephely.Text.Trim() + @"\adatok\főkönyv\nap\" + Dátum.Value.ToString("yyyyMMdd");
                if (Délelőtt.Checked)
                {
                    hely += "denap.mdb";
                }
                else
                {
                    hely += "dunap.mdb";
                }
                if (!File.Exists(hely))
                {
                    // új hely
                    hely = $@"{Application.StartupPath}\" + Választott_Telephely.Text.Trim() + @"\adatok\főkönyv\" + Dátum.Value.ToString("yyyy") + @"\nap\" + Dátum.Value.ToString("yyyyMMdd");
                    if (Délelőtt.Checked)
                    {
                        hely += "denap.mdb";
                    }
                    else
                    {
                        hely += "dunap.mdb";
                    }
                    if (!File.Exists(hely))
                        return;
                }
                string jelszó = "lilaakác";

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
                string szöveg = "SELECT * FROM adattábla where Adattábla.viszonylat ='-'  order by  típus asc,adattábla.kocsikszáma  desc, adattábla.szerelvény, Adattábla.azonosító ";
                List<Adat_Főkönyv_Nap> Adatok = FN_Kéz.Lista_adatok(hely, jelszó, szöveg);

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


        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Digitális_Főkönyv.html";
            MyE.Megnyitás(hely);
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
            if (TextKeres_Text.Text.Trim() == "")
                return;

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
                string hely, jelszó, szöveg;
                hely = Application.StartupPath + @"\Főmérnökség\adatok\villamos.mdb";
                jelszó = "pozsgaii";
                szöveg = "SELECT DISTINCT valóstípus2  FROM állománytábla order by valóstípus2";

                Típuslista.Items.Clear();
                Típuslista.BeginUpdate();
                Típuslista.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "valóstípus2"));
                Típuslista.EndUpdate();
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
                string hely, jelszó, szöveg;
                hely = Application.StartupPath + @"\Főmérnökség\adatok\kiegészítő.mdb";
                jelszó = "Mocó";
                szöveg = "SELECT * FROM telephelytábla order by telephelykönyvtár ";

                Telephelykönyvtár.Items.Clear();
                Telephelykönyvtár.BeginUpdate();
                Telephelykönyvtár.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "telephelykönyvtár"));
                Telephelykönyvtár.EndUpdate();
                Telephelykönyvtár.Refresh();
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
                Tábla1.Visible = false;
            }
            else
            {
                Tábla.Visible = false;
                Tábla1.Visible = true;
            }
        }


        private void Command3_Click(object sender, EventArgs e)
        {
            try
            {
                int volt = 0;
                for (int i = 0; i < Típuslista.Items.Count; i++)
                {
                    if (Típuslista.GetItemChecked(i) == true)
                        volt = 1;
                }
                if (volt == 0)
                    return;
                Holtart.Be(20);
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\villamos.mdb";
                string jelszó = "pozsgaii";
                int talált = 0;

                string szöveg = "SELECT * FROM állománytábla where  ";

                for (int i = 0; i < Típuslista.Items.Count; i++)
                {
                    if (Típuslista.GetItemChecked(i) == true)
                    {
                        if (talált == 0)
                        {
                            szöveg += "  valóstípus2='" + Típuslista.Items[i].ToString().Trim() + "'";
                            talált = 1;
                        }
                        else
                            szöveg += " OR valóstípus2='" + Típuslista.Items[i].ToString().Trim() + "'";

                    }
                }
                szöveg += " order by valóstípus2,üzem, azonosító";
                Tábla.Visible = false;
                Tábla1.Visible = false;
                Tábla2.Visible = false;
                Tábla3.Visible = false;
                string jelszóúj = "lilaakác";
                List<Adat_Jármű> Adatok = KJ_kéz.Lista_Adatok(hely, jelszó, szöveg);


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
                string szöveg1;

                foreach (Adat_Jármű rekord in Adatok)
                {
                    if (rekord.Törölt == false)
                    {
                        sor += 1;
                        Tábla1.RowCount = sor + 1;

                        Tábla1.Rows[sor - 1].Cells[0].Value = rekord.Azonosító.Trim();
                        Tábla1.Rows[sor - 1].Cells[1].Value = rekord.Valóstípus2.Trim();
                        volt = 0;

                        for (int i = 0; i < Telephelykönyvtár.Items.Count; i++)
                        {
                            string helyúj = $@"{Application.StartupPath}\" + Telephelykönyvtár.Items[i].ToString().Trim() + @"\adatok\főkönyv\" + Dátum_Melyik.Value.ToString("yyyy") + @"\nap\" + Dátum_Melyik.Value.ToString("yyyyMMdd") + "denap.mdb";
                            // ha nincs az új helyen akkor nézi az régin
                            if (File.Exists(helyúj) == false)
                                helyúj = $@"{Application.StartupPath}\" + Telephelykönyvtár.Items[i].ToString().Trim() + @"\adatok\főkönyv\nap\" + Dátum_Melyik.Value.ToString("yyyyMMdd") + "denap.mdb";
                            if (File.Exists(helyúj) == true)
                            {

                                szöveg1 = "SELECT * FROM adattábla where azonosító='" + rekord.Azonosító.Trim() + "'";
                                List<Adat_Főkönyv_Nap> Fadatok = FN_Kéz.Lista_adatok(helyúj, jelszóúj, szöveg1);
                                foreach (Adat_Főkönyv_Nap rekordideig in Fadatok)
                                {
                                    volt = 1;
                                    Tábla1.Rows[sor - 1].Cells[8].Value = rekordideig.Státus.ToString();
                                    switch (rekordideig.Státus)
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
                                    Tábla1.Rows[sor - 1].Cells[2].Value = Telephelykönyvtár.Items[i].ToString().Trim();
                                    Tábla1.Rows[sor - 1].Cells[3].Value = rekordideig.Hibaleírása.Trim();
                                    Tábla1.Rows[sor - 1].Cells[4].Value = rekordideig.Viszonylat.Trim();
                                    Tábla1.Rows[sor - 1].Cells[5].Value = rekordideig.Forgalmiszám.Trim();
                                    if (RadioButton4.Checked == true)
                                    {
                                        if (rekordideig.Tervindulás != new DateTime(1900, 1, 1))
                                            Tábla1.Rows[sor - 1].Cells[6].Value = rekordideig.Tervindulás.ToString("hh:mm");

                                        if (rekordideig.Tervérkezés != new DateTime(1900, 1, 1))
                                            Tábla1.Rows[sor - 1].Cells[7].Value = rekordideig.Tervérkezés.ToString("hh:mm");
                                    }
                                    else
                                    {
                                        Tábla1.Rows[sor - 1].Cells[6].Value = rekordideig.Tervindulás;
                                        Tábla1.Rows[sor - 1].Cells[7].Value = rekordideig.Tervérkezés;
                                    }
                                }
                            }
                            if (volt == 1)
                                break;
                        }
                        Holtart.Lép();
                    }
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
                int volt = 0;
                int sor;
                string szöveg, helyúj, jelszóúj;
                List<Adat_Összevont> ÖAdatokÖ;
                for (int i = 0; i < Típuslista.Items.Count; i++)
                {
                    if (Típuslista.GetItemChecked(i) == true)
                    {
                        volt += 1;
                    }
                }
                if (volt == 0)
                    return;

                string hely = Application.StartupPath + @"\Főmérnökség\adatok\" + Dátum_Melyik.Value.ToString("yyyy");
                if (System.IO.Directory.Exists(hely) == false)
                    System.IO.Directory.CreateDirectory(hely);

                hely = Application.StartupPath + @"\Főmérnökség\adatok\" + Dátum_Melyik.Value.ToString("yyyy") + @"\napiadatok";
                if (System.IO.Directory.Exists(hely) == false)
                    System.IO.Directory.CreateDirectory(hely);


                hely = Application.StartupPath + @"\Főmérnökség\adatok\" + Dátum_Melyik.Value.ToString("yyyy") + @"\napiadatok\" + Dátum_Melyik.Value.ToString("yyyyMMdd") + ".mdb";
                string jelszó = "pozsgaii";
                // ha már van akkor töröljük, hogy frissüljön, így nem kell figyelni, hogy volt-e már olyan adat
                if (File.Exists(hely))
                    File.Delete(hely);
                Adatbázis_Létrehozás.Összevonttáblakészítő(hely);

                // áttöltjük az adatokat a villamos táblából
                Tábla.Visible = false;
                Tábla1.Visible = false;
                Tábla2.Visible = false;
                Tábla3.Visible = false;

                Holtart.Be(Telephelykönyvtár.Items.Count + 2);

                List<string> szövegGy = new List<string>();
                for (int i = 0; i < Telephelykönyvtár.Items.Count; i++)
                {
                    helyúj = $@"{Application.StartupPath}\" + Telephelykönyvtár.Items[i].ToString().Trim() + @"\adatok\főkönyv\" + Dátum_Melyik.Value.ToString("yyyy") + @"\nap\" + Dátum_Melyik.Value.ToString("yyyyMMdd") + "denap.mdb";
                    jelszóúj = "lilaakác";

                    // ha nincs az új helyen akkor nézi az régin
                    if (!File.Exists(helyúj)) helyúj = $@"{Application.StartupPath}\" + Telephelykönyvtár.Items[i].ToString().Trim() + @"\adatok\főkönyv\nap\" + Dátum_Melyik.Value.ToString("yyyyMMdd") + "denap.mdb";
                    if (File.Exists(helyúj))
                    {

                        szöveg = "SELECT * FROM adattábla ";
                        List<Adat_Főkönyv_Nap> FAdatok = FN_Kéz.Lista_adatok(helyúj, jelszóúj, szöveg);

                        szövegGy.Clear();
                        foreach (Adat_Főkönyv_Nap rekord in FAdatok)
                        {
                            szöveg = "INSERT INTO tábla  (azonosító, státus, üzem, miótaáll, valóstípus, üzembehelyezés, hibaleírása ) VALUES (";
                            szöveg += "'" + rekord.Azonosító.Trim() + "', ";
                            szöveg += rekord.Státus.ToString() + ", ";
                            szöveg += "'" + Telephelykönyvtár.Items[i].ToString().Trim() + "', ";
                            szöveg += "'" + rekord.Miótaáll.ToString() + "', ";
                            szöveg += "'_', '1900.01.01',";
                            szöveg += "'" + rekord.Hibaleírása.Trim() + "') ";
                            szövegGy.Add(szöveg);
                        }
                        MyA.ABMódosítás(hely, jelszó, szövegGy);

                        Holtart.Lép();
                    }
                }


                // összevetjük a főmérnökségi villamos adatokkal
                helyúj = Application.StartupPath + @"\Főmérnökség\adatok\villamos.mdb";
                szöveg = "SELECT * FROM állománytábla ORDER BY azonosító ";


                List<Adat_Jármű> Adatok = KJ_kéz.Lista_Adatok(helyúj, jelszó, szöveg);
                Holtart.Be(20);
                szövegGy.Clear();
                foreach (Adat_Jármű rekord in Adatok)
                {
                    szöveg = "UPDATE tábla SET ";
                    szöveg += "valóstípus='" + rekord.Valóstípus.Trim() + "', ";
                    if (rekord.Üzembehelyezés == null)
                        szöveg += " üzembehelyezés='1900.01.01' ";
                    else
                        szöveg += " üzembehelyezés='" + rekord.Üzembehelyezés.ToString("yyyy.MM.dd") + "' ";
                    szöveg += " WHERE azonosító= '" + rekord.Azonosító.Trim() + "'";
                    szövegGy.Add(szöveg);
                    Holtart.Lép();
                }
                MyA.ABMódosítás(hely, jelszó, szövegGy);
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

                Tábla.RowCount += 2;
                Tábla.Rows[0].Cells[0].Value = "Üzemképes";

                int oszlop = 2;
                sor = 1;
                // kiírjuk az üzemképes kocsikat
                Holtart.Lép();
                int találó = 0;
                szöveg = "SELECT * FROM tábla where státus<>4 AND ( ";

                for (int ii = 0; ii < Típuslista.Items.Count; ii++)
                {
                    if (Típuslista.GetItemChecked(ii) == true)
                    {
                        if (találó == 0)
                        {
                            szöveg += "  valóstípus='" + Típuslista.Items[ii].ToString().Trim() + "'";
                            találó = 1;
                        }
                        else
                            szöveg += "OR valóstípus='" + Típuslista.Items[ii].ToString().Trim() + "'";
                    }
                }
                szöveg += " ) order by valóstípus,üzem, azonosító";

                Holtart.Be(20);
                string előző = "";
                string előzőüzem = "";
                int darab = 0;

                ÖAdatokÖ = KÖ_kéz.Lista_Adat(hely, jelszó, szöveg);
                foreach (Adat_Összevont rekord in ÖAdatokÖ)
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

                sor += 2;
                Tábla.RowCount = sor + 1;
                Tábla.Rows[sor].Cells[0].Value = "Üzemképtelen";
                sor += 1;
                Tábla.RowCount = sor + 1;
                // üzemképtelen kocsik
                Holtart.Lép();
                találó = 0;
                szöveg = "SELECT * FROM tábla where státus=4 and ( ";

                for (int ij = 0; ij < Típuslista.Items.Count; ij++)
                {
                    if (Típuslista.GetItemChecked(ij) == true)
                    {
                        if (találó == 0)
                        {
                            szöveg += "  valóstípus='" + Típuslista.Items[ij].ToString() + "'";
                            találó = 1;
                        }
                        else
                            szöveg += " OR valóstípus='" + Típuslista.Items[ij].ToString() + "'";

                    }
                }
                szöveg += " ) order by valóstípus,üzem, azonosító";
                ÖAdatokÖ = KÖ_kéz.Lista_Adat(hely, jelszó, szöveg);

                előző = "";
                előzőüzem = "";
                darab = 0;
                oszlop = 2;

                foreach (Adat_Összevont rekord in ÖAdatokÖ)
                {
                    if (rekord.Hibaleírása.Substring(0, 1) != "#" && rekord.Hibaleírása.Substring(0, 1) != "&" && rekord.Hibaleírása.Substring(0, 1) != "§")
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
                    }
                    Holtart.Lép();
                }

                if (oszlop == 2)
                    Tábla.Rows[sor - 1].Cells[12].Value = darab;
                else
                    Tábla.Rows[sor].Cells[12].Value = darab;



                // ***   Telepkívül ****
                Holtart.Lép();
                sor += 1;
                Tábla.RowCount = sor + 1;
                Tábla.Rows[sor].Cells[0].Value = "Üzemképtelen";
                sor += 1;
                Tábla.RowCount = sor + 1;

                előző = "";
                előzőüzem = "";
                darab = 0;
                oszlop = 2;

                foreach (Adat_Összevont rekord in ÖAdatokÖ)
                {

                    if (rekord.Hibaleírása.Substring(0, 1) == "§")
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
                    }
                    Holtart.Lép();
                }

                if (oszlop == 2)
                    Tábla.Rows[sor - 1].Cells[12].Value = darab;
                else
                    Tábla.Rows[sor].Cells[12].Value = darab;


                // ***   Főjavítás ****
                Holtart.Lép();
                sor += 1;
                Tábla.RowCount = sor + 1;
                Tábla.Rows[sor].Cells[0].Value = "Főjavítás";
                sor += 1;
                Tábla.RowCount = sor + 1;

                előző = "";
                előzőüzem = "";
                darab = 0;
                oszlop = 2;

                foreach (Adat_Összevont rekord in ÖAdatokÖ)
                {

                    if (rekord.Hibaleírása.Substring(0, 1) == "#")
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
                    }
                    Holtart.Lép();
                }

                if (darab != 0)
                {
                    if (oszlop == 2)
                        Tábla.Rows[sor - 1].Cells[12].Value = darab;
                    else
                        Tábla.Rows[sor].Cells[12].Value = darab;
                }

                // ***   Félre állítás ****
                Holtart.Lép();
                sor += 1;
                Tábla.RowCount = sor + 1;
                Tábla.Rows[sor].Cells[0].Value = "Félreállítás";
                sor += 1;
                Tábla.RowCount = sor + 1;

                előző = "";
                előzőüzem = "";
                darab = 0;
                oszlop = 2;

                foreach (Adat_Összevont rekord in ÖAdatokÖ)
                {

                    if (rekord.Hibaleírása.Substring(0, 1) == "&")
                    {
                        if (előző.Trim() == "")
                        {
                            előző = rekord.Valóstípus.Trim();
                            előzőüzem = rekord.Üzem.Trim();
                            sor += 1;
                            Tábla.RowCount = sor + 1;
                        }
                        // ha új típust ír ki
                        if ((előző ?? "") != (rekord.Valóstípus.Trim() ?? ""))
                        {
                            Tábla.Rows[sor].Cells[12].Value = darab;
                            darab = 0;
                            előző = rekord.Valóstípus.Trim();
                            előzőüzem = rekord.Üzem.Trim();
                            sor += 1;
                            Tábla.RowCount = sor + 1;
                            oszlop = 2;
                        }
                        // ha másik üzemben van akkor új sor
                        if ((előzőüzem ?? "") != (rekord.Üzem.Trim() ?? ""))
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
                    }
                    Holtart.Lép();
                }

                if (darab != 0)
                {
                    if (oszlop == 2)
                        Tábla.Rows[sor - 1].Cells[12].Value = darab;
                    else
                        Tábla.Rows[sor].Cells[12].Value = darab;

                }


                // összesen
                Holtart.Lép();

                sor += 1;
                Tábla.RowCount = sor + 1;
                Tábla.Rows[sor].Cells[0].Value = "Összesen";

                oszlop = Tábla.Columns.Count - 1;
                darab = 0;
                for (int ik = 1; ik < Tábla.Rows.Count; ik++)
                {
                    if (Tábla.Rows[ik].Cells[oszlop].Value != null)
                    {
                        if (int.TryParse(Tábla.Rows[ik].Cells[oszlop].Value.ToString(), out int result))
                            darab += int.Parse(Tábla.Rows[ik].Cells[oszlop].Value.ToString());
                    }
                }
                if (darab != 0)
                {
                    if (oszlop == 2)
                        Tábla.Rows[sor - 1].Cells[12].Value = darab;
                    else
                        Tábla.Rows[sor].Cells[12].Value = darab;
                }
                Tábla.Visible = true;


                // Részletes tábla

                Tábla1.Rows.Clear();
                Tábla1.Columns.Clear();
                Tábla1.Refresh();
                Tábla1.Visible = false;
                Tábla1.ColumnCount = 5;

                // fejléc elkészítése
                Tábla1.Columns[0].HeaderText = "Típus";
                Tábla1.Columns[0].Width = 100;
                Tábla1.Columns[1].HeaderText = "Telephely";
                Tábla1.Columns[1].Width = 100;
                Tábla1.Columns[2].HeaderText = "Psz";
                Tábla1.Columns[2].Width = 100;
                Tábla1.Columns[3].HeaderText = "Dátum";
                Tábla1.Columns[3].Width = 100;
                Tábla1.Columns[4].HeaderText = "Javítás leírása";
                Tábla1.Columns[4].Width = 700;
                Tábla1.Columns[4].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                Tábla1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                Tábla1.RowCount += 2;
                sor = 0;
                int talált = 0;
                Tábla1.Rows[sor].Cells[4].Value = "Kocsiszíni javítás";

                szöveg = "SELECT * FROM tábla where státus=4 and ( ";

                for (int ij = 0; ij < Típuslista.Items.Count; ij++)
                {
                    if (Típuslista.GetItemChecked(ij) == true)
                    {
                        if (talált == 0)
                        {
                            szöveg += "  valóstípus='" + Típuslista.Items[ij].ToString() + "'";
                            talált = 1;
                        }
                        else
                            szöveg += " OR valóstípus='" + Típuslista.Items[ij].ToString() + "'";

                    }
                }
                szöveg += " ) order by valóstípus,üzem, azonosító";
                ÖAdatokÖ = KÖ_kéz.Lista_Adat(hely, jelszó, szöveg);
                foreach (Adat_Összevont rekord in ÖAdatokÖ)
                {
                    if (rekord.Hibaleírása.Substring(0, 1) != "#" && rekord.Hibaleírása.Substring(0, 1) != "#" && rekord.Hibaleírása.Substring(0, 1) != "&"
                        && rekord.Hibaleírása.Substring(0, 1) != "§")
                    {
                        sor += 1;
                        Tábla1.RowCount = sor + 1;

                        Tábla1.Rows[sor].Cells[0].Value = rekord.Valóstípus.Trim();
                        Tábla1.Rows[sor].Cells[1].Value = rekord.Üzem.Trim();
                        Tábla1.Rows[sor].Cells[2].Value = rekord.Azonosító.Trim();
                        Tábla1.Rows[sor].Cells[3].Value = rekord.Miótaáll.ToString("yyyy.MM.dd");
                        Tábla1.Rows[sor].Cells[4].Value = rekord.Hibaleírása.Trim();
                    }
                    Holtart.Lép();
                }


                sor += 1;
                Tábla1.RowCount = sor + 1;
                Tábla1.Rows[sor].Cells[4].Value = "Telephelyen kívüli javítás";


                foreach (Adat_Összevont rekord in ÖAdatokÖ)
                {
                    if (rekord.Hibaleírása.Substring(0, 1) == "§")
                    {
                        sor += 1;
                        Tábla1.RowCount = sor + 1;

                        Tábla1.Rows[sor].Cells[0].Value = rekord.Valóstípus.Trim();
                        Tábla1.Rows[sor].Cells[1].Value = rekord.Üzem.Trim();
                        Tábla1.Rows[sor].Cells[2].Value = rekord.Azonosító.Trim();
                        Tábla1.Rows[sor].Cells[3].Value = rekord.Miótaáll.ToString("yyyy.MM.dd");
                        Tábla1.Rows[sor].Cells[4].Value = rekord.Hibaleírása.Trim();
                    }
                    Holtart.Lép();
                }


                sor += 1;
                Tábla1.RowCount = sor + 1;
                Tábla1.Rows[sor].Cells[4].Value = "Félreállítás";

                foreach (Adat_Összevont rekord in ÖAdatokÖ)
                {
                    if (rekord.Hibaleírása.Substring(0, 1) == "&")

                    {
                        sor += 1;
                        Tábla1.RowCount = sor + 1;

                        Tábla1.Rows[sor].Cells[0].Value = rekord.Valóstípus.Trim();
                        Tábla1.Rows[sor].Cells[1].Value = rekord.Üzem.Trim();
                        Tábla1.Rows[sor].Cells[2].Value = rekord.Azonosító.Trim();
                        Tábla1.Rows[sor].Cells[3].Value = rekord.Miótaáll.ToString("yyyy.MM.dd");
                        Tábla1.Rows[sor].Cells[4].Value = rekord.Hibaleírása.Trim();
                    }
                    Holtart.Lép();
                }


                sor += 1;
                Tábla1.RowCount = sor + 1;
                Tábla1.Rows[sor].Cells[4].Value = "Főjavítás";

                foreach (Adat_Összevont rekord in ÖAdatokÖ)
                {
                    if (rekord.Hibaleírása.Substring(0, 1) == "#")

                    {
                        sor += 1;
                        Tábla1.RowCount = sor + 1;

                        Tábla1.Rows[sor].Cells[0].Value = rekord.Valóstípus.Trim();
                        Tábla1.Rows[sor].Cells[1].Value = rekord.Üzem.Trim();
                        Tábla1.Rows[sor].Cells[2].Value = rekord.Azonosító.Trim();
                        Tábla1.Rows[sor].Cells[3].Value = rekord.Miótaáll.ToString("yyyy.MM.dd");
                        Tábla1.Rows[sor].Cells[4].Value = rekord.Hibaleírása.Trim();
                    };
                    Holtart.Lép();
                }
                Holtart.Ki();
                Tábla.Visible = true;
                Tábla1.Visible = false;
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


        private void Excel_Melyik_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.Visible == true & Tábla.Rows.Count <= 0)
                    return;
                if (Tábla1.Visible == true & Tábla1.Rows.Count <= 0)
                    return;
                if (Tábla2.Visible == true & Tábla2.Rows.Count <= 0)
                    return;
                if (Tábla3.Visible == true & Tábla3.Rows.Count <= 0)
                    return;

                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog1.InitialDirectory = "MyDocuments";

                SaveFileDialog1.Title = "Listázott tartalom mentése Excel fájlba";
                SaveFileDialog1.FileName = "Járművek_Telephelyek_" + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                SaveFileDialog1.Filter = "Excel |*.xlsx";
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);

                if (Tábla.Visible == true)
                    Module_Excel.EXCELtábla(fájlexc, Tábla, false);
                if (Tábla1.Visible == true)
                    Module_Excel.EXCELtábla(fájlexc, Tábla1, false);
                if (Tábla2.Visible == true)
                    Module_Excel.EXCELtábla(fájlexc, Tábla2, false);
                if (Tábla3.Visible == true)
                    Module_Excel.EXCELtábla(fájlexc, Tábla3, false);

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