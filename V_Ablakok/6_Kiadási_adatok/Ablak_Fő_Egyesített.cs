using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
    public partial class Ablak_Fő_Egyesített
    {
        readonly Kezelő_FőSzemélyzet_Adatok KézSzem = new Kezelő_FőSzemélyzet_Adatok();
        readonly Kezelő_FőTípuscsere_Adatok KézTípCsere = new Kezelő_FőTípuscsere_Adatok();
        readonly Kezelő_FőKiadási_adatok KézKiad = new Kezelő_FőKiadási_adatok();
        readonly Kezelő_Forte_Kiadási_Adatok KézForteKiad = new Kezelő_Forte_Kiadási_Adatok();
        readonly Kezelő_Kiegészítő_Típusaltípustábla Kézkiegtípusal = new Kezelő_Kiegészítő_Típusaltípustábla();
        readonly Kezelő_Kiegészítő_Főkategóriatábla KézFőkat = new Kezelő_Kiegészítő_Főkategóriatábla();
        readonly Kezelő_kiegészítő_telephely KézKiegTelep = new Kezelő_kiegészítő_telephely();
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly Kezelő_Főkönyv_Nap KézFőNap = new Kezelő_Főkönyv_Nap();
        readonly Kezelő_Kiegészítő_Szolgálat KézKiegSzolgálat = new Kezelő_Kiegészítő_Szolgálat();


        List<Adat_FőKiadási_adatok> AdatokKiad = new List<Adat_FőKiadási_adatok>();
        List<Adat_Személyzet_Adatok> AdatokSzem = new List<Adat_Személyzet_Adatok>();
        List<Adat_Típuscsere_Adatok> AdatokTípCsere = new List<Adat_Típuscsere_Adatok>();
        List<Adat_Forte_Kiadási_Adatok> AdatokFortekiad = new List<Adat_Forte_Kiadási_Adatok>();
        List<Adat_Kiegészítő_Típusaltípustábla> Adatokkiegtípusal = new List<Adat_Kiegészítő_Típusaltípustábla>();

        DateTime hónaputolsónapja;
        DateTime hónapelsőnapja;
        DateTime ElőzőDátum;
        int hónapnap;
        int Oszlop_Max;

        private string[] Cím = new string[21];
        private string[] Leírás = new string[21];

        readonly Beállítás_Betű BeBetűkukac = new Beállítás_Betű { Név = "Calibri", Formátum = "@", Méret = 11 };
        readonly Beállítás_Betű BeBetűV = new Beállítás_Betű { Név = "Calibri", Vastag = true, Méret = 11 };
        readonly Beállítás_Betű BeBetűVD = new Beállítás_Betű { Név = "Calibri", Dőlt = true, Vastag = true, Méret = 11 };

        string munkalap = "";
        #region alap
        public Ablak_Fő_Egyesített()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            try
            {
                Dátum.Value = DateTime.Today;
                ElőzőDátum = Dátum.Value;
                Kategóriák();
                Listák_Feltöltés();

                if (Program.PostásJogkör.Substring(0, 1) != "R")
                {
                    Jogosultságkiosztás();
                }
                else
                {
                    GombLathatosagKezelo.Beallit(this);
                }
            }

            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ablak_Fő_Egyesített_Load(object sender, EventArgs e)
        {

        }

        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;

                // ide kell az összes gombot tenni amit szabályozni akarunk false

                // csak főmérnökségi belépéssel van módosítás
                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {
                }
                else
                {
                }

                melyikelem = 183;
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

        private void Kategóriák()
        {
            try
            {
                List<Adat_Kiegészítő_Főkategóriatábla> Adatok = KézFőkat.Lista_Adatok();

                Kategórilista.Items.Clear();
                Kategórilista.BeginUpdate();
                foreach (Adat_Kiegészítő_Főkategóriatábla Elem in Adatok)
                    Kategórilista.Items.Add(Elem.Főkategória);

                Kategórilista.EndUpdate();
                Kategórilista.Refresh();
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
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\Főmérnökség_napi_lekérdezés.html";
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


        #region Napi adatok
        private void KiadaNapi_Click(object sender, EventArgs e)
        {
            try
            {
                // megnézzük, hogy van-e kijelölve valami
                if (Kategórilista.SelectedItems.Count < 1) throw new HibásBevittAdat("Nincs kijelölve egy kategória sem.");

                List<Adat_FőKiadási_adatok> Adatok;
                if (Délelőtt.Checked)
                    Adatok = (from a in AdatokKiad
                              where a.Dátum == Dátum.Value
                              && a.Napszak == "de"
                              orderby a.Főkategória, a.Napszak, a.Típus, a.Altípus, a.Szolgálat, a.Telephely
                              select a).ToList();
                else
                    Adatok = (from a in AdatokKiad
                              where a.Dátum == Dátum.Value
                              && a.Napszak == "du"
                              orderby a.Főkategória, a.Napszak, a.Típus, a.Altípus, a.Szolgálat, a.Telephely
                              select a).ToList();
                Táblázatlistázás(Adatok);
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

        private void Táblázatlistázás(List<Adat_FőKiadási_adatok> Adatok)
        {
            try
            {
                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 16;

                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Dátum";
                Tábla.Columns[0].Width = 100;
                Tábla.Columns[1].HeaderText = "Napszak";
                Tábla.Columns[1].Width = 80;
                Tábla.Columns[2].HeaderText = "Főkategória";
                Tábla.Columns[2].Width = 130;
                Tábla.Columns[3].HeaderText = "Típus";
                Tábla.Columns[3].Width = 100;
                Tábla.Columns[4].HeaderText = "Al-Típus";
                Tábla.Columns[4].Width = 100;
                Tábla.Columns[5].HeaderText = "Szolgálat";
                Tábla.Columns[5].Width = 130;
                Tábla.Columns[6].HeaderText = "Telephely";
                Tábla.Columns[6].Width = 120;
                Tábla.Columns[7].HeaderText = "Kiadás";
                Tábla.Columns[7].Width = 80;
                Tábla.Columns[8].HeaderText = "Forgalomban";
                Tábla.Columns[8].Width = 110;
                Tábla.Columns[9].HeaderText = "Tartalék";
                Tábla.Columns[9].Width = 80;
                Tábla.Columns[10].HeaderText = "Kocsiszíni";
                Tábla.Columns[10].Width = 100;
                Tábla.Columns[11].HeaderText = "Félreállítás";
                Tábla.Columns[11].Width = 100;
                Tábla.Columns[12].HeaderText = "Főjavítás";
                Tábla.Columns[12].Width = 100;
                Tábla.Columns[13].HeaderText = "Állomány";
                Tábla.Columns[13].Width = 100;
                Tábla.Columns[14].HeaderText = "Személyzethiány";
                Tábla.Columns[14].Width = 150;
                Tábla.Columns[15].HeaderText = "Munkanap";
                Tábla.Columns[15].Width = 150;

                foreach (string Elem in Kategórilista.CheckedItems)
                {
                    long kiadásö = 0;
                    long forgalombanö = 0;
                    long tartalékö = 0;
                    long kocsiszíniö = 0;
                    long félreállításö = 0;
                    long főjavításö = 0;
                    long személyzetö = 0;
                    int i;

                    foreach (Adat_FőKiadási_adatok rekord in Adatok)
                    {
                        if (rekord.Főkategória == Elem)
                        {
                            Tábla.RowCount++;
                            i = Tábla.RowCount - 1;
                            Tábla.Rows[i].Cells[0].Value = rekord.Dátum.ToString("yyyy.MM.dd");
                            Tábla.Rows[i].Cells[1].Value = rekord.Napszak;
                            Tábla.Rows[i].Cells[2].Value = rekord.Főkategória;
                            Tábla.Rows[i].Cells[3].Value = rekord.Típus;
                            Tábla.Rows[i].Cells[4].Value = rekord.Altípus;
                            Tábla.Rows[i].Cells[5].Value = rekord.Szolgálat;
                            Tábla.Rows[i].Cells[6].Value = rekord.Telephely;
                            Tábla.Rows[i].Cells[7].Value = rekord.Kiadás;
                            Tábla.Rows[i].Cells[8].Value = rekord.Forgalomban;
                            Tábla.Rows[i].Cells[9].Value = rekord.Tartalék + rekord.Személyzet;
                            Tábla.Rows[i].Cells[10].Value = rekord.Kocsiszíni;
                            Tábla.Rows[i].Cells[11].Value = rekord.Félreállítás;
                            Tábla.Rows[i].Cells[12].Value = rekord.Főjavítás;
                            Tábla.Rows[i].Cells[13].Value = rekord.Forgalomban + rekord.Tartalék
                                                          + rekord.Kocsiszíni + rekord.Félreállítás
                                                          + rekord.Főjavítás + rekord.Személyzet;
                            Tábla.Rows[i].Cells[14].Value = rekord.Személyzet;
                            if (rekord.Munkanap == 0)
                                Tábla.Rows[i].Cells[15].Value = "Munkanap";
                            else
                                Tábla.Rows[i].Cells[15].Value = "Hétvége";

                            kiadásö += rekord.Kiadás;
                            forgalombanö += rekord.Forgalomban;
                            tartalékö += rekord.Tartalék;
                            kocsiszíniö += rekord.Kocsiszíni;
                            félreállításö += rekord.Félreállítás;
                            főjavításö += rekord.Főjavítás;
                            személyzetö += rekord.Személyzet;
                        }
                    }

                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[2].Value = Elem;
                    Tábla.Rows[i].Cells[3].Value = "Összesen:";
                    Tábla.Rows[i].Cells[7].Value = kiadásö;
                    Tábla.Rows[i].Cells[8].Value = forgalombanö;
                    Tábla.Rows[i].Cells[9].Value = tartalékö + személyzetö;
                    Tábla.Rows[i].Cells[10].Value = kocsiszíniö;
                    Tábla.Rows[i].Cells[11].Value = félreállításö;
                    Tábla.Rows[i].Cells[12].Value = főjavításö;
                    Tábla.Rows[i].Cells[13].Value = forgalombanö + tartalékö + kocsiszíniö + félreállításö + főjavításö + személyzetö;
                    Tábla.Rows[i].Cells[14].Value = személyzetö;
                    for (int j = 0; j < Tábla.Columns.Count; j++)
                        Tábla.Rows[i].Cells[j].Style.BackColor = Color.CornflowerBlue;
                }
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

        private List<Adat_FőKiadási_adatok> Szűrt_Kiadás()
        {
            List<Adat_FőKiadási_adatok> Adatok = new List<Adat_FőKiadási_adatok>();
            try
            {
                if (Délelőtt.Checked)
                    Adatok = (from a in AdatokKiad
                              where a.Dátum >= hónapelsőnapja && a.Dátum <= hónaputolsónapja
                              && a.Napszak == "de"
                              orderby a.Főkategória, a.Napszak, a.Típus, a.Altípus, a.Szolgálat, a.Telephely
                              select a).ToList();
                else
                    Adatok = (from a in AdatokKiad
                              where a.Dátum >= hónapelsőnapja && a.Dátum <= hónaputolsónapja
                              && a.Napszak == "du"
                              orderby a.Főkategória, a.Napszak, a.Típus, a.Altípus, a.Szolgálat, a.Telephely
                              select a).ToList();
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
            return Adatok;
        }

        private void KiadHavi_Click(object sender, EventArgs e)
        {
            try
            {
                // megnézzük, hogy van-e kijelölve valami
                if (Kategórilista.SelectedItems.Count < 1) throw new HibásBevittAdat("Nincs kijelölve egy kategória sem.");
                Táblázatlistázás(Szűrt_Kiadás());
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


        #region Személyzet
        private void SzemélyNapi_Click(object sender, EventArgs e)
        {
            try
            {
                List<Adat_Személyzet_Adatok> Adatok;
                if (Délelőtt.Checked)
                    Adatok = (from a in AdatokSzem
                              where a.Dátum == Dátum.Value
                              && a.Napszak == "de"
                              orderby a.Napszak, a.Típus, a.Viszonylat, a.Forgalmiszám
                              select a).ToList();
                else
                    Adatok = (from a in AdatokSzem
                              where a.Dátum == Dátum.Value
                              && a.Napszak == "du"
                              orderby a.Napszak, a.Típus, a.Viszonylat, a.Forgalmiszám
                              select a).ToList();
                Táblázatszemélyzet(Adatok);
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

        private void SzemélyHavi_Click(object sender, EventArgs e)
        {
            try
            {
                List<Adat_Személyzet_Adatok> Adatok;
                if (Délelőtt.Checked)
                    Adatok = (from a in AdatokSzem
                              where a.Dátum >= hónapelsőnapja && a.Dátum <= hónaputolsónapja
                              && a.Napszak == "de"
                              orderby a.Napszak, a.Típus, a.Viszonylat, a.Forgalmiszám
                              select a).ToList();
                else
                    Adatok = (from a in AdatokSzem
                              where a.Dátum >= hónapelsőnapja && a.Dátum <= hónaputolsónapja
                              && a.Napszak == "du"
                              orderby a.Napszak, a.Típus, a.Viszonylat, a.Forgalmiszám
                              select a).ToList();
                Táblázatszemélyzet(Adatok);
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

        private void Táblázatszemélyzet(List<Adat_Személyzet_Adatok> Adatok)
        {
            try
            {
                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 9;
                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Dátum";
                Tábla.Columns[0].Width = 120;
                Tábla.Columns[1].HeaderText = "Napszak";
                Tábla.Columns[1].Width = 100;
                Tábla.Columns[2].HeaderText = "Szolgálat";
                Tábla.Columns[2].Width = 130;
                Tábla.Columns[3].HeaderText = "Telephely";
                Tábla.Columns[3].Width = 100;
                Tábla.Columns[4].HeaderText = "Típus";
                Tábla.Columns[4].Width = 100;
                Tábla.Columns[5].HeaderText = "Viszonylat";
                Tábla.Columns[5].Width = 100;
                Tábla.Columns[6].HeaderText = "Forgalmiszám";
                Tábla.Columns[6].Width = 120;
                Tábla.Columns[7].HeaderText = "Tervindulás";
                Tábla.Columns[7].Width = 120;
                Tábla.Columns[8].HeaderText = "Psz";
                Tábla.Columns[8].Width = 100;

                int i;
                foreach (Adat_Személyzet_Adatok rekord in Adatok)
                {
                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = rekord.Dátum.ToString("yyyy.MM.dd");
                    Tábla.Rows[i].Cells[1].Value = rekord.Napszak;
                    Tábla.Rows[i].Cells[2].Value = rekord.Szolgálat;
                    Tábla.Rows[i].Cells[3].Value = rekord.Telephely;
                    Tábla.Rows[i].Cells[4].Value = rekord.Típus;
                    Tábla.Rows[i].Cells[5].Value = rekord.Viszonylat;
                    Tábla.Rows[i].Cells[6].Value = rekord.Forgalmiszám;
                    Tábla.Rows[i].Cells[7].Value = rekord.Tervindulás.ToString("HH:mm"); ;
                    Tábla.Rows[i].Cells[8].Value = rekord.Azonosító;
                }
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


        #region Típuscsere
        private void TípusNapi_Click(object sender, EventArgs e)
        {
            try
            {
                List<Adat_Típuscsere_Adatok> Adatok;
                if (Délelőtt.Checked)
                    Adatok = (from a in AdatokTípCsere
                              where a.Dátum == Dátum.Value && a.Napszak == "de"
                              orderby a.Napszak, a.Típuselőírt, a.Viszonylat, a.Forgalmiszám
                              select a).ToList();
                else
                    Adatok = (from a in AdatokTípCsere
                              where a.Dátum == Dátum.Value && a.Napszak == "du"
                              orderby a.Napszak, a.Típuselőírt, a.Viszonylat, a.Forgalmiszám
                              select a).ToList();

                Táblázattípus(Adatok);
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

        private void TípusHavi_Click(object sender, EventArgs e)
        {
            try
            {
                List<Adat_Típuscsere_Adatok> Adatok;
                if (Délelőtt.Checked)
                    Adatok = (from a in AdatokTípCsere
                              where a.Dátum >= hónapelsőnapja && a.Dátum <= hónaputolsónapja
                              && a.Napszak == "de"
                              orderby a.Napszak, a.Típuselőírt, a.Viszonylat, a.Forgalmiszám
                              select a).ToList();
                else
                    Adatok = (from a in AdatokTípCsere
                              where a.Dátum >= hónapelsőnapja && a.Dátum <= hónaputolsónapja
                              && a.Napszak == "du"
                              orderby a.Napszak, a.Típuselőírt, a.Viszonylat, a.Forgalmiszám
                              select a).ToList();

                Táblázattípus(Adatok);
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

        private void Táblázattípus(List<Adat_Típuscsere_Adatok> Adatok)
        {
            try
            {
                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 10;
                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Dátum";
                Tábla.Columns[0].Width = 100;
                Tábla.Columns[1].HeaderText = "Napszak";
                Tábla.Columns[1].Width = 100;
                Tábla.Columns[2].HeaderText = "Szolgálat";
                Tábla.Columns[2].Width = 130;
                Tábla.Columns[3].HeaderText = "Telephely";
                Tábla.Columns[3].Width = 100;
                Tábla.Columns[4].HeaderText = "Típus előírt";
                Tábla.Columns[4].Width = 100;
                Tábla.Columns[5].HeaderText = "Típus kiadott";
                Tábla.Columns[5].Width = 100;
                Tábla.Columns[6].HeaderText = "Viszonylat";
                Tábla.Columns[6].Width = 100;
                Tábla.Columns[7].HeaderText = "Forgalmiszám";
                Tábla.Columns[7].Width = 100;
                Tábla.Columns[8].HeaderText = "Tervindulás";
                Tábla.Columns[8].Width = 210;
                Tábla.Columns[9].HeaderText = "Psz";
                Tábla.Columns[9].Width = 100;

                int i;
                foreach (Adat_Típuscsere_Adatok rekord in Adatok)
                {
                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = rekord.Dátum.ToString("yyyy.MM.dd"); ;
                    Tábla.Rows[i].Cells[1].Value = rekord.Napszak;
                    Tábla.Rows[i].Cells[2].Value = rekord.Szolgálat;
                    Tábla.Rows[i].Cells[3].Value = rekord.Telephely;
                    Tábla.Rows[i].Cells[4].Value = rekord.Típuselőírt;
                    Tábla.Rows[i].Cells[5].Value = rekord.Típuskiadott;
                    Tábla.Rows[i].Cells[6].Value = rekord.Viszonylat;
                    Tábla.Rows[i].Cells[7].Value = rekord.Forgalmiszám;
                    Tábla.Rows[i].Cells[8].Value = rekord.Tervindulás.ToString("HH:mm"); ;
                    Tábla.Rows[i].Cells[9].Value = rekord.Azonosító;
                }

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


        #region Gombok
        private void Nyit_Click(object sender, EventArgs e)
        {
            Kategórilista.Height = 500;
            Nyit.Visible = false;
            Csuk.Visible = true;
        }

        private void Csuk_Click(object sender, EventArgs e)
        {
            Kategórilista.Height = 25;
            Nyit.Visible = true;
            Csuk.Visible = false;
        }

        private void CsoportkijelölMind_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= Kategórilista.Items.Count - 1; i++)
                Kategórilista.SetItemChecked(i, true);
        }

        private void CsoportVissza_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= Kategórilista.Items.Count - 1; i++)
                Kategórilista.SetItemChecked(i, false);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.Rows.Count <= 0) throw new HibásBevittAdat("Nincs kijelölve egy adatsor sem.");
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Főmérnökségi_egyesített_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMdd}",
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

        private List<Adat_Forte_Kiadási_Adatok> ForteSzűrtAdatok()
        {
            List<Adat_Forte_Kiadási_Adatok> Adatok = new List<Adat_Forte_Kiadási_Adatok>();
            try
            {
                if (Délelőtt.Checked)
                    Adatok = (from a in AdatokFortekiad
                              where a.Dátum >= hónapelsőnapja && a.Dátum <= hónaputolsónapja
                              && a.Napszak == "de"
                              orderby a.Dátum
                              select a).ToList();
                else
                    Adatok = (from a in AdatokFortekiad
                              where a.Dátum >= hónapelsőnapja && a.Dátum <= hónaputolsónapja
                              && a.Napszak == "du"
                              orderby a.Dátum
                              select a).ToList();
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
            return Adatok;
        }

        private void TípusÁllományDb_Click(object sender, EventArgs e)
        {
            try
            {

                if (Kategórilista.SelectedItems.Count < 1) throw new HibásBevittAdat("Nincs kijelölve egy kategória sem.");
                AdatokKiad = KézKiad.Lista_adatok(Dátum.Value.Year);
                Holtart.Be(hónapnap + 1);

                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 1;

                Tábla.RowCount = hónapnap;

                // elkészítjük a dátumokat
                Tábla.Columns[0].HeaderText = "Dátum";
                Tábla.Columns[0].Width = 110;

                for (int mi = 0; mi <= hónapnap - 1; mi++)
                {
                    DateTime ideigd = new DateTime(Dátum.Value.Year, Dátum.Value.Month, mi + 1);
                    Tábla.Rows[mi].Cells[0].Value = ideigd.ToString("yyyy.MM.dd");
                    Tábla.Rows[mi].Cells[0].Style.BackColor = Color.Blue;
                    Holtart.Lép();
                }


                Holtart.Be(hónapnap + 1);
                for (int ii = 0; ii < Tábla.Rows.Count; ii++)
                {
                    Adat_Forte_Kiadási_Adatok Elem = (from a in ForteSzűrtAdatok()
                                                      where a.Dátum == Tábla.Rows[ii].Cells[0].Value.ToÉrt_DaTeTime()
                                                      select a).FirstOrDefault();
                    if (Elem != null)
                    {
                        if (Elem.Munkanap == 0)
                            Tábla.Rows[ii].Cells[0].Style.BackColor = Color.Green;
                        else
                            Tábla.Rows[ii].Cells[0].Style.BackColor = Color.Red;
                    }
                    Holtart.Lép();
                }


                // ****************************************************
                // Elkészítjük a táblázatot oszlopait
                // ****************************************************
                List<string> Elemek = new List<string>();

                for (int rész = 0; rész < Kategórilista.CheckedItems.Count; rész++)
                {
                    List<string> Elemekid = (from a in Adatokkiegtípusal
                                             where a.Főkategória == Kategórilista.CheckedItems[rész].ToStrTrim()
                                             orderby a.AlTípus
                                             select a.AlTípus).ToList();
                    Elemek.AddRange(Elemekid);
                }
                Tábla.Visible = true;
                if (Elemek == null) throw new HibásBevittAdat("A kiválasztott kategóriának nincs alábontása.");
                Tábla.Visible = false;

                Tábla.ColumnCount = Elemek.Count + 1;


                int i = 0;
                Holtart.Be(Elemek.Count + 1);
                foreach (string Elem in Elemek)
                {
                    i += 1;
                    Tábla.Columns[i].HeaderText = Elem;
                    Tábla.Columns[i].Width = 100;
                    Holtart.Lép();
                }


                // ****************************************************
                // kitöltjük a táblázatot
                // ****************************************************

                for (int ki = 0; ki <= Tábla.RowCount - 1; ki++)
                {
                    for (int j = 1; j <= Tábla.ColumnCount - 1; j++)
                        Tábla.Rows[ki].Cells[j].Value = 0;
                }

                long[] összeg = new long[32];

                for (int ki = 1; ki <= 31; ki++)
                    összeg[ki] = 0;
                Holtart.Be(Tábla.ColumnCount + 1);
                for (int oszlop = 1; oszlop < Tábla.ColumnCount; oszlop++)
                {
                    for (int sor = 0; sor < Tábla.RowCount; sor++)
                    {
                        DateTime Nap = Tábla.Rows[sor].Cells[0].Value.ToÉrt_DaTeTime();
                        string Típus = Tábla.Columns[oszlop].HeaderText.ToStrTrim();

                        List<Adat_FőKiadási_adatok> AdatokElem;
                        if (Délelőtt.Checked)
                            AdatokElem = (from a in AdatokKiad
                                          where a.Dátum == Nap && a.Altípus == Típus && a.Napszak == "de"
                                          select a).ToList();
                        else
                            AdatokElem = (from a in AdatokKiad
                                          where a.Dátum == Nap && a.Altípus == Típus && a.Napszak == "du"
                                          select a).ToList();
                        long kiadás = AdatokElem.Sum(a => a.Kiadás);
                        long forgalomban = AdatokElem.Sum(a => a.Forgalomban);
                        long tartalék = AdatokElem.Sum(a => a.Tartalék);
                        long kocsiszíni = AdatokElem.Sum(a => a.Kocsiszíni);
                        long félreállítás = AdatokElem.Sum(a => a.Félreállítás);
                        long főjavítás = AdatokElem.Sum(a => a.Főjavítás);
                        long személyzet = AdatokElem.Sum(a => a.Személyzet);
                        long ideig = forgalomban + tartalék + kocsiszíni + félreállítás + főjavítás + személyzet;
                        Tábla.Rows[sor].Cells[oszlop].Value = ideig;
                        összeg[sor + 1] += ideig;
                    }
                    Holtart.Lép();
                }

                Tábla.ColumnCount += 1;
                Tábla.Columns[Tábla.ColumnCount - 1].HeaderText = "Összesen:";
                Tábla.Columns[Tábla.ColumnCount - 1].Width = 100;

                for (int ii = 1; ii <= hónapnap; ii++)
                    Tábla.Rows[ii - 1].Cells[Tábla.ColumnCount - 1].Value = összeg[ii];

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

        private void Command7_Click(object sender, EventArgs e)
        {
            try
            {
                // --- Fájl létrehozás és mentés  ---
                string hely = $"Kocsik_{Dátum.Value:yyyyMMdd}_";
                hely += Délelőtt.Checked ? "de" : "du";
                hely += $"_{DateTime.Now:yyyyMMddHHmmss}";

                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Járműpark tételes ellenőrzés",
                    FileName = hely,
                    Filter = "Excel |*.xlsx"
                };

                string fájlexc;
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                munkalap = "Munka1";
                MyX.ExcelLétrehozás(munkalap);
                Holtart.Be(50);

                int sor = 1;
                MyX.Kiir("Pályaszám", "a1");
                MyX.Kiir("Főmérnökségi Típus", "b1");
                MyX.Kiir("Jármű Típus", "c1");

                // Lekérjük a járműveket
                List<Adat_Jármű> AdatokJármű = KézJármű.Lista_Adatok("Főmérnökség");
                AdatokJármű = AdatokJármű.Where(a => a.Törölt == false).OrderBy(a => a.Azonosító).ToList();

                Dictionary<string, int> PszSorIndex = new Dictionary<string, int>();

                foreach (Adat_Jármű rekord in AdatokJármű)
                {
                    sor++;
                    MyX.Kiir("#SZÁME#" + rekord.Azonosító, "a" + sor);
                    MyX.Kiir(rekord.Valóstípus2, "b" + sor);
                    MyX.Kiir(rekord.Valóstípus, "c" + sor);

                    if (!PszSorIndex.ContainsKey(rekord.Azonosító.Trim()))
                        PszSorIndex.Add(rekord.Azonosító.Trim(), sor);
                    Holtart.Lép();
                }

                MyX.Betű(munkalap, "A2:A" + sor.ToString(), BeBetűkukac);

                int sormax = sor;

                // Telephelyi adatok kitöltése ---
                List<Adat_kiegészítő_telephely> AdatokKiegTelep = KézKiegTelep.Lista_Adatok();
                int oszlop = 3;

                foreach (Adat_kiegészítő_telephely rekord in AdatokKiegTelep)
                {
                    oszlop++;
                    MyX.Kiir(rekord.Telephelykönyvtár.ToStrTrim(), MyF.Oszlopnév(oszlop) + "1");

                    // Lekérjük az adott telephely adatait
                    List<Adat_Főkönyv_Nap> AdatokFőNap = KézFőNap.Lista_Adatok(rekord.Telephelykönyvtár, Dátum.Value, Délelőtt.Checked ? "de" : "du");

                    // Végigmegyünk a telephely járművein
                    foreach (var elem in AdatokFőNap)
                    {
                        string keresettPsz = elem.Azonosító.Trim();

                        if (PszSorIndex.ContainsKey(keresettPsz))
                        {
                            int celSor = PszSorIndex[keresettPsz];

                            MyX.Kiir("#SZÁME#1", MyF.Oszlopnév(oszlop) + celSor.ToString());
                        }
                    }
                    Holtart.Lép();
                }

                // --- Összesítés és Formázás ---
                oszlop++;
                MyX.Kiir("Összesen", MyF.Oszlopnév(oszlop) + "1");

                // Képletek beírása
                for (int i = 2; i <= sormax; i++)
                    MyX.Kiir("#KÉPLET#=SUM(RC[-" + (oszlop - 3).ToString() + "]:RC[-1])", MyF.Oszlopnév(oszlop) + i.ToString());

                // Szűrés és Formázás
                MyX.Szűrés("Munka1", "A", MyF.Oszlopnév(oszlop), sormax);

                // Keretezés csak a használt területre
                string teljesTerulet = "A1:" + MyF.Oszlopnév(oszlop) + sormax.ToString();
                MyX.Rácsoz(munkalap, teljesTerulet);

                // Oszlopszélesség
                MyX.Oszlopszélesség("Munka1", "A:" + MyF.Oszlopnév(oszlop));

                MyX.Aktív_Cella("Munka1", "A1");
                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();

                Holtart.Ki();
                MyF.Megnyitás(fájlexc);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region Kimutatás
        private void Kimutatás_Click(object sender, EventArgs e)
        {
            try
            {
                if (Kategórilista.SelectedItems.Count < 1) throw new HibásBevittAdat("Nincs kijelölve egy kategória sem.");
                string hely = $@"{Application.StartupPath}\főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_fortekiadási_adatok.mdb";
                if (!File.Exists(hely)) throw new HibásBevittAdat($"Nincs {Dátum.Value:yyyy.MM.dd} dátumnak megfelelő adat.");

                // létrehozzuk az excel táblát
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Járműpark Kimutatás készítése",
                    FileName = $"Nóta_{Dátum.Value:yyyy}_év_{Dátum.Value:MM}_hó_{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                string fájlexc;
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                Holtartfő.Visible = true;
                Holtart.Visible = true;
                Holtartfő.Maximum = 16;
                // paraméter tábla feltöltése
                Cím[1] = "állomány 1";
                Cím[2] = "állomány 2";
                Cím[3] = "állomány 3";
                Cím[4] = "Forgalmi 1";
                Cím[5] = "Forgalmi 2";
                Cím[6] = "Forgalmi 3";
                Cím[7] = "Üzemképes 1";
                Cím[8] = "Üzemképes 2";
                Cím[9] = "Üzemképes 3";
                Cím[10] = "Adatok 1";
                Cím[11] = "Adatok 2";
                Cím[12] = "Adatok 3";
                Cím[13] = "Kocsiszín";
                Cím[14] = "Kimutatás";
                Cím[15] = "Kocsiszín_1";
                Leírás[1] = "Állományi darabszámok Szolgálatonkénti bontásban";
                Leírás[2] = "Állományi darabszámok Típusonkénti bontásban";
                Leírás[3] = "Állományi darabszámok Típus és Szolgálatbontásban";
                Leírás[4] = "Forgalomba adott jármű darabszámok Szolgálatonkénti bontásban";
                Leírás[5] = "Forgalomba adott jármű darabszámok Típusonkénti bontásban";
                Leírás[6] = "Forgalomba adott jármű darabszámok Típus és Szolgálatbontásban";
                Leírás[7] = "Üzemképes jármű darabszámok Szolgálatonkénti bontásban";
                Leírás[8] = "Üzemképes jármű darabszámok Típusonkénti bontásban";
                Leírás[9] = "Üzemképes jármű darabszámok Típus és Szolgálatbontásban";
                Leírás[10] = "Nóta adatok";
                Leírás[11] = "Személyzet hiány adatok";
                Leírás[12] = "Típuscsere adatok";
                Leírás[13] = "Kocsiszíni állományi adatok";
                Leírás[14] = "Nóta adatok kimutatása";
                Leírás[15] = "Kocsiszíni forgalomba adott adatok";
                munkalap = "Tartalom";
                MyX.ExcelLétrehozás(munkalap);

                // ****************************************************
                // elkészítjük a lapokat
                // ****************************************************
                //MyX.Munkalap_átnevezés("Munka1", "Tartalom");

                for (int i = 1; i <= 15; i++)
                {
                    MyX.Munkalap_Új(Cím[i]);
                }
                // ****************************************************
                // Elkészítjük a tartalom jegyzéket
                // ****************************************************
                MyX.Munkalap_aktív(munkalap);
                MyX.Kiir("munkalapfül", "a1");
                MyX.Kiir("Leírás", "b1");

                for (int i = 1; i <= 15; i++)
                {
                    MyX.Link_beillesztés("Tartalom", "a" + (i + 1).ToString(), Cím[i]);
                    MyX.Kiir(Cím[i], "b" + (i + 1).ToString());
                }
                MyX.Oszlopszélesség("Tartalom", "A:B");

                // ****************************************************
                // Elkészítjük a munkalapokat
                // ****************************************************
                Holtartfő.Value = 1;
                Állomány1tábla();
                Holtartfő.Value = 2;
                Állomány2tábla();
                Holtartfő.Value = 3;
                Állomány3tábla();
                Holtartfő.Value = 4;
                Kiadott1tábla();
                Holtartfő.Value = 5;
                Kiadott2tábla();
                Holtartfő.Value = 6;
                Kiadott3tábla();
                Holtartfő.Value = 7;
                Üzemképes1tábla();
                Holtartfő.Value = 8;
                Üzemképes2tábla();
                Holtartfő.Value = 9;
                Üzemképes3tábla();
                Holtartfő.Value = 10;
                Adatkiiró1();
                Holtartfő.Value = 11;
                Adatkiiró2();
                Holtartfő.Value = 12;
                Adatkiiró3();
                Holtartfő.Value = 13;
                Telephelytábla();
                Holtartfő.Value = 14;
                Kimutatásvarázsló();
                Holtartfő.Value = 15;
                Telephelytábla_1();

                MyX.Munkalap_aktív("Tartalom");
                MyX.Aktív_Cella("Tartalom", "A1");

                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();

                Holtartfő.Visible = false;
                Holtart.Ki();
                MyF.Megnyitás(fájlexc);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.StackTrace + "\n" + ex.Message + "\n" + ex.Source, "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Állomány1tábla()
        {
            try
            {
                MyX.Munkalap_aktív("állomány 1");
                MyX.Link_beillesztés("állomány 1", "A1", "Tartalom");
                munkalap = "állomány 1";
                Napok_kiírása();
                MunkaVHétvége();

                // ****************************************************
                // Elkészítjük a táblázatot
                // ****************************************************
                List<Adat_Kiegészítő_Szolgálat> AdatokKiegSzolgálat = KézKiegSzolgálat.Lista_Adatok();

                string szöveg = Délelőtt.Checked ? "Reggeli " : "Délutáni ";
                szöveg += "Állományi darabszámok";
                int jj = 3;
                MyX.Kiir(szöveg, MyF.Oszlopnév(jj) + 3.ToString());

                Holtart.Be(hónapnap + 1);

                bool volt = false;
                foreach (Adat_Kiegészítő_Szolgálat rekordkieg in AdatokKiegSzolgálat)
                {
                    MyX.Kiir(rekordkieg.Szolgálatnév, MyF.Oszlopnév(jj) + 4.ToString());

                    // főkategória
                    for (int k = 0; k <= Kategórilista.CheckedItems.Count - 1; k++)
                    {
                        for (int ki = 1; ki <= hónapnap; ki++)
                        {
                            List<Adat_FőKiadási_adatok> Elemek;
                            DateTime AktNap = new DateTime(Dátum.Value.Year, Dátum.Value.Month, ki);
                            if (Délelőtt.Checked)
                                Elemek = (from a in AdatokKiad
                                          where a.Napszak == "de"
                                          && a.Főkategória == Kategórilista.CheckedItems[k].ToStrTrim()
                                          && a.Szolgálat == rekordkieg.Szolgálatnév
                                          && a.Dátum == AktNap
                                          select a).ToList();
                            else
                                Elemek = (from a in AdatokKiad
                                          where a.Napszak == "du"
                                          && a.Főkategória == Kategórilista.CheckedItems[k].ToStrTrim()
                                          && a.Szolgálat == rekordkieg.Szolgálatnév
                                          && a.Dátum == AktNap
                                          select a).ToList();

                            MyX.Kiir(Kategórilista.Items[k].ToStrTrim(), MyF.Oszlopnév(jj) + 5.ToString());
                            if (Elemek != null)
                            {
                                long kiadás = Elemek.Sum(a => a.Kiadás);
                                long forgalomban = Elemek.Sum(a => a.Forgalomban);
                                long tartalék = Elemek.Sum(a => a.Tartalék);
                                long kocsiszíni = Elemek.Sum(a => a.Kocsiszíni);
                                long félreállítás = Elemek.Sum(a => a.Félreállítás);
                                long főjavítás = Elemek.Sum(a => a.Főjavítás);
                                long személyzet = Elemek.Sum(a => a.Személyzet);
                                long érték = forgalomban + tartalék + kocsiszíni + félreállítás + főjavítás + személyzet;
                                MyX.Kiir("#SZÁME#" + érték.ToString(), MyF.Oszlopnév(jj) + (ki + 5).ToString());
                                volt = true;
                            }
                            else
                            {
                                MyX.Kiir("#SZÁME#0", MyF.Oszlopnév(jj) + (ki + 5).ToString());
                            }

                        }
                        jj += 1;

                        Holtart.Lép();
                    }
                    if (volt == true)
                    {
                        MyX.Kiir("Összesen", MyF.Oszlopnév(jj) + 5.ToString());
                        jj += 1;
                    }
                    volt = false;
                }
                MyX.Kiir("Összesen", MyF.Oszlopnév(jj) + 4.ToString());
                int oszlopmax = jj;

                // Összesítések
                // A-B Oszlop formázása
                MyX.Oszlopszélesség(munkalap, "B:B", 2);
                MyX.Oszlopszélesség(munkalap, "A:A");
               // MyX.Vastagkeret(munkalap, "A4:B" + (hónapnap + 5).ToString());
                MyX.Rácsoz(munkalap, "A4:B" + (hónapnap + 5).ToString());
                
                // állomány felirat
                MyX.Egyesít(munkalap, MyF.Oszlopnév(3) + "3:" + MyF.Oszlopnév(oszlopmax) + "3");
                if (volt == true)
                {
                    // ha csak egy főkategória volt
                    for (int vi = 1; vi <= hónapnap; vi++)
                    {
                        MyX.Kiir("#KÉPLET#=SUM(RC[-" + (oszlopmax - 3).ToString() + "]:RC[-1])", MyF.Oszlopnév(oszlopmax) + (vi + 5).ToString());
                        Holtart.Value = vi;
                    }
                    // megformázzuk
                    //MyX.Vastagkeret(munkalap, "c4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyX.Rácsoz(munkalap, "c4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyX.Rácsoz(munkalap, "c4:" + MyF.Oszlopnév(oszlopmax) + "5");
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());


                    MyX.Betű(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString(), BeBetűVD);
                    // Oszlop szélesség beállítás
                    MyX.Oszlopszélesség(munkalap, "C:" + MyF.Oszlopnév(oszlopmax));

                }

                else
                {
                    // ha több kategória volt

                    // részösszegek
                    int eleje = 3;
                    for (int wj = 3; wj <= oszlopmax; wj++)
                    {
                        if (MyX.Beolvas(munkalap, MyF.Oszlopnév(wj) + "5") == "Összesen")
                        {

                            for (int wi = 1; wi <= hónapnap; wi++)
                            {
                                MyX.Kiir("#KÉPLET#=SUM(RC[-" + (wj - eleje).ToString() + "]:RC[-1])", MyF.Oszlopnév(wj) + (wi + 5).ToString());
                                Holtart.Value = wi;
                            }
                            MyX.Egyesít(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(wj) + "4");
                            // MyX.Vastagkeret(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(wj) + (hónapnap + 5).ToString());
                            MyX.Rácsoz(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(wj) + (hónapnap + 5).ToString());
                            MyX.Rácsoz(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(wj) + "5");
                            MyX.Betű(munkalap, MyF.Oszlopnév(wj) + "5:" + MyF.Oszlopnév(wj) + (hónapnap + 5).ToString(), BeBetűV);
                            MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(eleje) + ":" + MyF.Oszlopnév(wj));

                            eleje = wj + 1;
                        }
                    }
                    // megformázzuk
                    // Összesítő rész formázása
                    //MyX.Vastagkeret(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + "5");
                    MyX.Betű(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString(), BeBetűVD);
                    szöveg = "=";

                    for (int j = 3; j <= oszlopmax; j++)
                    {
                        if (MyX.Beolvas(munkalap, MyF.Oszlopnév(j) + "5") == "Összesen")
                        {
                            if (szöveg == "=")
                            {
                                szöveg += "SUM(RC[-" + (oszlopmax - j).ToString() + "]";
                            }
                            else
                            {
                                szöveg += ",RC[-" + (oszlopmax - j).ToString() + "]";
                            }
                        }
                    }
                    szöveg += ")";
                    for (int i = 1; i <= hónapnap; i++)
                    {
                        MyX.Kiir("#KÉPLET#" + szöveg, MyF.Oszlopnév(oszlopmax) + (i + 5).ToString());
                        Holtart.Value = i;
                    }
                }
                // Alsó összesítés és átlag
                Havi_Összesítő_rész(oszlopmax);
                MyX.Aktív_Cella(munkalap, "A1");
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Állomány2tábla()
        {
            try
            {
                munkalap = "állomány 2";
                MyX.Munkalap_aktív(munkalap);
                MyX.Link_beillesztés(munkalap, "A1", "Tartalom");
                Napok_kiírása();
                MunkaVHétvége();

                string szöveg = Délelőtt.Checked ? "Reggeli " : "Délutáni ";
                szöveg += "Állományi darabszámok";
                MyX.Kiir(szöveg, MyF.Oszlopnév(3) + 3.ToString());

                // '****************************************************
                // 'Elkészítjük a táblázatot
                // '****************************************************
                List<Adat_Kiegészítő_Típusaltípustábla> AdatokKiegTípusal = Kézkiegtípusal.Lista_Adatok();

                bool volt = false;
                int pj = 3;
                int oszlopmax;
                Holtart.Be(hónapnap + 1);

                string előzőtípus = "";

                for (int k = 0; k <= Kategórilista.CheckedItems.Count - 1; k++)
                {

                    if (előzőtípus.Trim() == "")
                    {
                        MyX.Kiir(Kategórilista.CheckedItems[k].ToStrTrim(), MyF.Oszlopnév(pj) + 4.ToString());
                        előzőtípus = Kategórilista.CheckedItems[k].ToStrTrim();
                    }
                    else
                    {
                        MyX.Kiir("Összesen", MyF.Oszlopnév(pj) + 5.ToString());
                        pj += 1;
                        MyX.Kiir(Kategórilista.CheckedItems[k].ToStrTrim(), MyF.Oszlopnév(pj) + 4.ToString());
                        előzőtípus = Kategórilista.CheckedItems[k].ToStrTrim();
                    }

                    List<Adat_Kiegészítő_Típusaltípustábla> Adatok = (from a in AdatokKiegTípusal
                                                                      where a.Főkategória == Kategórilista.CheckedItems[k].ToStrTrim()
                                                                      orderby a.AlTípus
                                                                      select a).ToList();

                    foreach (Adat_Kiegészítő_Típusaltípustábla rekordkieg in Adatok)
                    {
                        for (int i = 1; i <= hónapnap; i++)
                        {
                            List<Adat_FőKiadási_adatok> Elemek;
                            DateTime AktNap = new DateTime(Dátum.Value.Year, Dátum.Value.Month, i);
                            if (Délelőtt.Checked)
                                Elemek = (from a in AdatokKiad
                                          where a.Napszak == "de"
                                          && a.Főkategória == Kategórilista.CheckedItems[k].ToStrTrim()
                                          && a.Altípus == rekordkieg.AlTípus
                                          && a.Dátum == AktNap
                                          orderby a.Altípus
                                          select a).ToList();
                            else
                                Elemek = (from a in AdatokKiad
                                          where a.Napszak == "du"
                                          && a.Főkategória == Kategórilista.CheckedItems[k].ToStrTrim()
                                          && a.Altípus == rekordkieg.AlTípus
                                          && a.Dátum == AktNap
                                          orderby a.Altípus
                                          select a).ToList();

                            MyX.Kiir(rekordkieg.AlTípus, MyF.Oszlopnév(pj) + 5.ToString());
                            if (Elemek != null)
                            {
                                long kiadás = Elemek.Sum(a => a.Kiadás);
                                long forgalomban = Elemek.Sum(a => a.Forgalomban);
                                long tartalék = Elemek.Sum(a => a.Tartalék);
                                long kocsiszíni = Elemek.Sum(a => a.Kocsiszíni);
                                long félreállítás = Elemek.Sum(a => a.Félreállítás);
                                long főjavítás = Elemek.Sum(a => a.Főjavítás);
                                long személyzet = Elemek.Sum(a => a.Személyzet);
                                long érték = forgalomban + tartalék + kocsiszíni + félreállítás + főjavítás + személyzet;
                                MyX.Kiir("#SZÁME#" + érték.ToString(), MyF.Oszlopnév(pj) + (i + 5).ToString());
                            }
                            else
                            {
                                MyX.Kiir("#SZÁME#0", MyF.Oszlopnév(pj) + (i + 5).ToString());
                            }
                        }
                        oszlopmax = pj;
                        pj += 1;
                        Holtart.Lép();
                    }
                }

                if (volt != true)
                {
                    MyX.Kiir("Összesen", MyF.Oszlopnév(pj) + 5.ToString());
                    pj += 1;
                }
                MyX.Kiir("Összesen", MyF.Oszlopnév(pj) + 4.ToString());
                oszlopmax = pj;
                // Összesítések
                // A-B Oszlop formázása
                MyX.Oszlopszélesség(munkalap, "B:B", 2);
                MyX.Oszlopszélesség(munkalap, "A:A");
                MyX.Rácsoz(munkalap, "A4:B" + (hónapnap + 5).ToString());
              //  MyX.Vastagkeret(munkalap, "A4:B" + (hónapnap + 5).ToString());
                
                

                int eleje;
                if (volt == true)
                {
                    // ha csak egy főkategória volt

                    for (int i = 1; i <= hónapnap; i++)
                    {
                        MyX.Kiir("#KÉPLET#=SUM(RC[-" + (oszlopmax - 3).ToString() + "]:RC[-1])", MyF.Oszlopnév(oszlopmax) + (i + 5).ToString());
                        Holtart.Value = i;
                    }
                    // megformázzuk
                    MyX.Egyesít(munkalap, "c3:" + MyF.Oszlopnév(oszlopmax) + "3");
                  //  MyX.Vastagkeret(munkalap, "c4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyX.Rácsoz(munkalap, "c4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                   MyX.Rácsoz(munkalap, "c4:" + MyF.Oszlopnév(oszlopmax) + "5");
                  //  MyX.Vastagkeret(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyX.Betű(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString(), BeBetűVD);
                    // Oszlop szélesség beállítás
                    MyX.Oszlopszélesség(munkalap, "C:" + MyF.Oszlopnév(oszlopmax));
                }

                else
                {
                    // ha több kategória volt
                    // részösszegek
                    eleje = 3;
                    for (int j = 3; j <= oszlopmax; j++)
                    {
                        if (MyX.Beolvas(munkalap, MyF.Oszlopnév(j) + "5") == "Összesen")
                        {
                            for (int i = 1; i <= hónapnap; i++)
                            {
                                MyX.Kiir("#KÉPLET#=SUM(RC[-" + (j - eleje).ToString() + "]:RC[-1])", MyF.Oszlopnév(j) + (i + 5).ToString());
                                Holtart.Value = i;
                            }
                            MyX.Egyesít(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(j) + "4");
                           // MyX.Vastagkeret(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(j) + (hónapnap + 5).ToString());
                            MyX.Rácsoz(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(j) + (hónapnap + 5).ToString());
                            MyX.Rácsoz(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(j) + "5");
                            MyX.Betű(munkalap, MyF.Oszlopnév(j) + "5:" + MyF.Oszlopnév(j) + (hónapnap + 5).ToString(), BeBetűV);
                            MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(eleje) + ":" + MyF.Oszlopnév(j));
                            eleje = j + 1;
                        }
                    }


                    // megformázzuk
                    // állomány felirat
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(3) + "3:" + MyF.Oszlopnév(oszlopmax) + "3");

                    // Összesítő rész formázása
                    //MyX.Vastagkeret(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + "5");
                    MyX.Betű(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString(), BeBetűVD);

                    szöveg = "";
                    szöveg = "=";

                    for (int j = 3; j <= oszlopmax; j++)
                    {
                        if (MyX.Beolvas(munkalap, MyF.Oszlopnév(j) + "5") == "Összesen")
                        {
                            if (szöveg == "=")
                            {
                                szöveg += "SUM(RC[-" + (oszlopmax - j).ToString() + "]";
                            }
                            else
                            {
                                szöveg += ",RC[-" + (oszlopmax - j).ToString() + "]";
                            }
                        }
                    }
                    szöveg += ")";

                    for (int i = 1; i <= hónapnap; i++)
                    {
                        MyX.Kiir("#KÉPLET#" + szöveg, MyF.Oszlopnév(oszlopmax) + (i + 5).ToString());
                        Holtart.Value = i;
                    }
                }
                Havi_Összesítő_rész(oszlopmax);
                MyX.Aktív_Cella(munkalap, "A1");
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Állomány3tábla()
        {
            try
            {
                munkalap = "állomány 3";
                MyX.Munkalap_aktív(munkalap);
                MyX.Link_beillesztés(munkalap, "A1", "Tartalom");

                Napok_kiírása();
                MunkaVHétvége();

                string szöveg = Délelőtt.Checked ? "Reggeli " : "Délutáni ";
                szöveg += "Állományi darabszámok";
                MyX.Kiir(szöveg, MyF.Oszlopnév(3) + 2.ToString());


                // ****************************************************
                // Elkészítjük a táblázatot
                // ****************************************************
                List<Adat_Kiegészítő_Típusaltípustábla> AdatokKiegTípusal = Kézkiegtípusal.Lista_Adatok();
                List<Adat_Kiegészítő_Szolgálat> AdatokKiegSzolgtábl = KézKiegSzolgálat.Lista_Adatok();

                int pj = 3;
                int oszlopmax;
                Holtart.Be(hónapnap + 1);

                string előzőtípus = "";
                string előzőaltípus = "";


                for (int k = 0; k < Kategórilista.CheckedItems.Count; k++)
                {
                    if (előzőtípus.Trim() == "")
                    {
                        munkalap = "állomány 3"; MyX.Munkalap_aktív(munkalap);
                        MyX.Kiir(Kategórilista.CheckedItems[k].ToStrTrim(), MyF.Oszlopnév(pj) + 3.ToString());

                        munkalap = "Forgalmi 3"; MyX.Munkalap_aktív(munkalap);
                        MyX.Kiir(Kategórilista.CheckedItems[k].ToStrTrim(), MyF.Oszlopnév(pj) + 3.ToString());

                        munkalap = "Üzemképes 3"; MyX.Munkalap_aktív(munkalap);
                        MyX.Kiir(Kategórilista.CheckedItems[k].ToStrTrim(), MyF.Oszlopnév(pj) + 3.ToString());
                        előzőtípus = Kategórilista.CheckedItems[k].ToStrTrim();
                    }
                    else
                    {
                        munkalap = "állomány 3"; MyX.Munkalap_aktív(munkalap);
                        MyX.Kiir("Összesen", MyF.Oszlopnév(pj) + 5.ToString());

                        munkalap = "Forgalmi 3"; MyX.Munkalap_aktív(munkalap);
                        MyX.Kiir("Összesen", MyF.Oszlopnév(pj) + 5.ToString());

                        munkalap = "Üzemképes 3"; MyX.Munkalap_aktív(munkalap);
                        MyX.Kiir("Összesen", MyF.Oszlopnév(pj) + 5.ToString());
                        pj += 1;

                        munkalap = "állomány 3"; MyX.Munkalap_aktív(munkalap);
                        MyX.Kiir("Összesen", MyF.Oszlopnév(pj) + 4.ToString());

                        munkalap = "Forgalmi 3"; MyX.Munkalap_aktív(munkalap);
                        MyX.Kiir("Összesen", MyF.Oszlopnév(pj) + 4.ToString());

                        munkalap = "Üzemképes 3"; MyX.Munkalap_aktív(munkalap);
                        MyX.Kiir("Összesen", MyF.Oszlopnév(pj) + 4.ToString());
                        pj += 1;

                        munkalap = "állomány 3"; MyX.Munkalap_aktív(munkalap);
                        MyX.Kiir(Kategórilista.CheckedItems[k].ToStrTrim(), MyF.Oszlopnév(pj) + 3.ToString());

                        munkalap = "Forgalmi 3"; MyX.Munkalap_aktív(munkalap);
                        MyX.Kiir(Kategórilista.CheckedItems[k].ToStrTrim(), MyF.Oszlopnév(pj) + 3.ToString());

                        munkalap = "Üzemképes 3"; MyX.Munkalap_aktív(munkalap);
                        MyX.Kiir(Kategórilista.CheckedItems[k].ToStrTrim(), MyF.Oszlopnév(pj) + 3.ToString());
                        előzőtípus = Kategórilista.CheckedItems[k].ToStrTrim();
                        előzőaltípus = "";
                    }

                    List<Adat_Kiegészítő_Típusaltípustábla> Adatok = (from a in AdatokKiegTípusal
                                                                      where a.Főkategória == Kategórilista.CheckedItems[k].ToStrTrim()
                                                                      orderby a.AlTípus
                                                                      select a).ToList();
                    List<Adat_FőKiadási_adatok> Elemek;
                    foreach (Adat_Kiegészítő_Típusaltípustábla elem in Adatok)
                    {
                        if (előzőaltípus.Trim() == "") előzőaltípus = elem.AlTípus;

                        if (előzőaltípus == elem.AlTípus)
                            előzőaltípus = elem.AlTípus;
                        else
                        {
                            munkalap = "állomány 3"; MyX.Munkalap_aktív(munkalap);
                            MyX.Kiir("Összesen", MyF.Oszlopnév(pj) + 5.ToString());

                            munkalap = "Forgalmi 3"; MyX.Munkalap_aktív(munkalap);
                            MyX.Kiir("Összesen", MyF.Oszlopnév(pj) + 5.ToString());

                            munkalap = "Üzemképes 3"; MyX.Munkalap_aktív(munkalap);
                            MyX.Kiir("Összesen", MyF.Oszlopnév(pj) + 5.ToString());
                            pj += 1;
                            előzőaltípus = elem.AlTípus;
                        }

                        munkalap = "állomány 3"; MyX.Munkalap_aktív(munkalap);
                        MyX.Kiir(elem.AlTípus, MyF.Oszlopnév(pj) + 4.ToString());

                        munkalap = "Forgalmi 3"; MyX.Munkalap_aktív(munkalap);
                        MyX.Kiir(elem.AlTípus, MyF.Oszlopnév(pj) + 4.ToString());

                        munkalap = "Üzemképes 3"; MyX.Munkalap_aktív(munkalap);
                        MyX.Kiir(elem.AlTípus, MyF.Oszlopnév(pj) + 4.ToString());

                        foreach (Adat_Kiegészítő_Szolgálat rekordkieg1 in AdatokKiegSzolgtábl)
                        {
                            munkalap = "állomány 3"; MyX.Munkalap_aktív(munkalap);
                            MyX.Kiir(rekordkieg1.Szolgálatnév, MyF.Oszlopnév(pj) + 5.ToString());

                            munkalap = "Forgalmi 3"; MyX.Munkalap_aktív(munkalap);
                            MyX.Kiir(rekordkieg1.Szolgálatnév, MyF.Oszlopnév(pj) + 5.ToString());

                            munkalap = "Üzemképes 3"; MyX.Munkalap_aktív(munkalap);
                            MyX.Kiir(rekordkieg1.Szolgálatnév, MyF.Oszlopnév(pj) + 5.ToString());

                            if (Délelőtt.Checked)
                                Elemek = (from a in AdatokKiad
                                          where a.Napszak == "de"
                                          && a.Főkategória == Kategórilista.CheckedItems[k].ToStrTrim()
                                          && a.Altípus == elem.AlTípus
                                          && a.Szolgálat == MyX.Beolvas(munkalap, MyF.Oszlopnév(pj) + 5.ToString())
                                          && a.Dátum >= hónapelsőnapja
                                          && a.Dátum <= hónaputolsónapja
                                          orderby a.Dátum
                                          select a).ToList();
                            else
                                Elemek = (from a in AdatokKiad
                                          where a.Napszak == "du"
                                          && a.Főkategória == Kategórilista.CheckedItems[k].ToStrTrim()
                                          && a.Altípus == elem.AlTípus
                                          && a.Szolgálat == MyX.Beolvas(munkalap, MyF.Oszlopnév(pj) + 5.ToString())
                                          && a.Dátum >= hónapelsőnapja
                                          && a.Dátum <= hónaputolsónapja
                                          orderby a.Dátum
                                          select a).ToList();

                            if (Elemek.Count > 1)
                            {
                                DateTime ElőzőDátum = new DateTime(1900, 1, 1);
                                Holtart.Be(hónapnap + 1);

                                int sor = 0;
                                long forgalomban = 0;
                                long tartalék = 0;
                                long kocsiszíni = 0;
                                long félreállítás = 0;
                                long főjavítás = 0;
                                long személyzet = 0;
                                long érték = 0;
                                foreach (Adat_FőKiadási_adatok rekord in Elemek)
                                {

                                    if (ElőzőDátum == new DateTime(1900, 1, 1)) ElőzőDátum = rekord.Dátum;
                                    if (ElőzőDátum != rekord.Dátum)
                                    {
                                        sor = ElőzőDátum.Day;
                                        érték = forgalomban + tartalék + kocsiszíni + félreállítás + főjavítás + személyzet;

                                        munkalap = "állomány 3"; MyX.Munkalap_aktív(munkalap);
                                        MyX.Kiir("#SZÁME#" + érték.ToString(), MyF.Oszlopnév(pj) + (sor + 5).ToString());

                                        érték = forgalomban;
                                        munkalap = "Forgalmi 3"; MyX.Munkalap_aktív(munkalap);
                                        MyX.Kiir("#SZÁME#" + érték.ToString(), MyF.Oszlopnév(pj) + (sor + 5).ToString());

                                        érték = forgalomban + tartalék;
                                        munkalap = "Üzemképes 3"; MyX.Munkalap_aktív(munkalap);
                                        MyX.Kiir("#SZÁME#" + érték.ToString(), MyF.Oszlopnév(pj) + (sor + 5).ToString());


                                        forgalomban = 0;
                                        tartalék = 0;
                                        kocsiszíni = 0;
                                        félreállítás = 0;
                                        főjavítás = 0;
                                        személyzet = 0;
                                        ElőzőDátum = rekord.Dátum;

                                    }

                                    forgalomban += rekord.Forgalomban;
                                    tartalék += rekord.Tartalék;
                                    kocsiszíni += rekord.Kocsiszíni;
                                    félreállítás += rekord.Félreállítás;
                                    főjavítás += rekord.Főjavítás;
                                    személyzet += rekord.Személyzet;
                                }

                                érték = forgalomban + tartalék + kocsiszíni + félreállítás + főjavítás + személyzet;

                                munkalap = "állomány 3"; MyX.Munkalap_aktív(munkalap);
                                MyX.Kiir("#SZÁME#" + érték.ToString(), MyF.Oszlopnév(pj) + (sor + 6).ToString());

                                érték = forgalomban;
                                munkalap = "Forgalmi 3"; MyX.Munkalap_aktív(munkalap);
                                MyX.Kiir("#SZÁME#" + érték.ToString(), MyF.Oszlopnév(pj) + (sor + 6).ToString());

                                érték = forgalomban + tartalék;
                                munkalap = "Üzemképes 3"; MyX.Munkalap_aktív(munkalap);
                                MyX.Kiir("#SZÁME#" + érték.ToString(), MyF.Oszlopnév(pj) + (sor + 6).ToString());

                                Holtart.Lép();
                                pj += 1;
                            }
                        }

                    }
                    oszlopmax = pj;
                }

                munkalap = "állomány 3"; MyX.Munkalap_aktív(munkalap);
                MyX.Kiir("Összesen", MyF.Oszlopnév(pj) + 5.ToString());

                munkalap = "Forgalmi 3"; MyX.Munkalap_aktív(munkalap);
                MyX.Kiir("Összesen", MyF.Oszlopnév(pj) + 5.ToString());

                munkalap = "Üzemképes 3"; MyX.Munkalap_aktív(munkalap);
                MyX.Kiir("Összesen", MyF.Oszlopnév(pj) + 5.ToString());
                pj += 1;

                munkalap = "állomány 3"; MyX.Munkalap_aktív(munkalap);
                MyX.Kiir("Összesen", MyF.Oszlopnév(pj) + 4.ToString());

                munkalap = "Forgalmi 3"; MyX.Munkalap_aktív(munkalap);
                MyX.Kiir("Összesen", MyF.Oszlopnév(pj) + 4.ToString());

                munkalap = "Üzemképes 3"; MyX.Munkalap_aktív(munkalap);
                MyX.Kiir("Összesen", MyF.Oszlopnév(pj) + 4.ToString());

                Oszlop_Max = pj;

                munkalap = "állomány 3";
                MyX.Munkalap_aktív(munkalap);
                Rácsoz_3(munkalap);
                Havi_Összesítő_rész(Oszlop_Max);
                MyX.Aktív_Cella(munkalap, "A1");
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Rácsoz_3(string munkalap)
        {
            int oszlopmax = Oszlop_Max;
            int eleje;
            string szöveg;

            // Összesítések
            // A-B Oszlop formázása
            MyX.Munkalap_aktív(munkalap);
            MyX.Oszlopszélesség(munkalap, "B:B", 2);

            MyX.Oszlopszélesség(munkalap, "A:A");
            //MyX.Vastagkeret(munkalap, "A4:B" + (hónapnap + 5).ToString());
            MyX.Rácsoz(munkalap, "A4:B" + (hónapnap + 5).ToString());

            // megnézzük az 5 sort ha van Összesen, akkor összesít
            Holtart.Be(hónapnap + 1);

            // ha több kategória volt
            // részösszegek
            eleje = 3;
            for (int j = 3; j < oszlopmax; j++)
            {
                if (MyX.Beolvas(munkalap, MyF.Oszlopnév(j) + "4") == "Összesen")
                {
                    eleje = j + 1;
                }
                if (MyX.Beolvas(munkalap, MyF.Oszlopnév(j) + "5") == "Összesen")
                {
                    for (int i = 1; i <= hónapnap; i++)
                    {
                        MyX.Kiir("#KÉPLET#=SUM(RC[-" + (j - eleje).ToString() + "]:RC[-1])", MyF.Oszlopnév(j) + (i + 5).ToString());
                        Holtart.Lép();
                    }

                    MyX.Egyesít(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(j) + "4");
                   // MyX.Vastagkeret(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(j) + (hónapnap + 5).ToString());
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(j) + (hónapnap + 5).ToString());
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(j) + "5");
                    MyX.Betű(munkalap, MyF.Oszlopnév(j) + "5:" + MyF.Oszlopnév(j) + (hónapnap + 5).ToString(), BeBetűV);
                    MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(eleje) + ":" + MyF.Oszlopnév(j));
                    eleje = j + 1;
                }
            }
            // megformázzuk
            // állomány felirat
            eleje = 3;
            for (int j = 3; j <= oszlopmax; j++)
            {
                if (MyX.Beolvas(munkalap, MyF.Oszlopnév(j) + "4") == "Összesen")
                {
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(eleje) + "3:" + MyF.Oszlopnév(j) + "3");
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(eleje) + "3:" + MyF.Oszlopnév(j) + "3");
                    eleje = j + 1;
                }
            }
            eleje = 3;
            int vége;

            for (int i = 3; i <= oszlopmax; i++)
            {
                if (MyX.Beolvas(munkalap, MyF.Oszlopnév(i) + "4") == "Összesen")
                {
                    // Összesítő rész formázása
                 //   MyX.Vastagkeret(munkalap, MyF.Oszlopnév(i) + "4:" + MyF.Oszlopnév(i) + (hónapnap + 5).ToString());
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(i) + "4:" + MyF.Oszlopnév(i) + (hónapnap + 5).ToString());
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(i) + "4:" + MyF.Oszlopnév(i) + "5");
                    MyX.Betű(munkalap, MyF.Oszlopnév(i) + "4:" + MyF.Oszlopnév(i) + (hónapnap + 5).ToString(), BeBetűVD);
                    vége = i;

                    szöveg = "=";
                    for (int j = eleje; j <= vége; j++)
                    {
                        if (MyX.Beolvas(munkalap, MyF.Oszlopnév(j) + "5") == "Összesen")
                        {
                            if (szöveg == "=")
                            {
                                szöveg += "SUM(RC[-" + (vége - j).ToString() + "]";
                            }
                            else
                            {
                                szöveg += ",RC[-" + (vége - j).ToString() + "]";
                            }
                        }
                    }
                    szöveg += ")";
                    for (int k = 1; k <= hónapnap; k++)
                    {
                        MyX.Kiir("#KÉPLET#" + szöveg, MyF.Oszlopnév(i) + (k + 5).ToString());
                        Holtart.Lép();
                    }
                    eleje = i + 1;
                }
            }

            // végösszesen
            MyX.Kiir("VégÖsszesen", MyF.Oszlopnév(oszlopmax + 1) + 3.ToString());
            // Összesítő rész formázása
           // MyX.Vastagkeret(munkalap, MyF.Oszlopnév(oszlopmax + 1) + "3:" + MyF.Oszlopnév(oszlopmax + 1) + (hónapnap + 5).ToString());
            MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlopmax + 1) + "3:" + MyF.Oszlopnév(oszlopmax + 1) + (hónapnap + 5).ToString());
            MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlopmax + 1) + "3:" + MyF.Oszlopnév(oszlopmax + 1) + "5");
            MyX.Betű(munkalap, MyF.Oszlopnév(oszlopmax + 1) + "3:" + MyF.Oszlopnév(oszlopmax + 1) + (hónapnap + 5).ToString(), BeBetűVD);
            MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(oszlopmax + 1) + ":" + MyF.Oszlopnév(oszlopmax + 1));

            szöveg = "=";

            for (int j = 3; j <= oszlopmax + 1; j++)
            {
                if (MyX.Beolvas(munkalap, MyF.Oszlopnév(j) + "4") == "Összesen")
                    if (szöveg == "=")
                        szöveg += "SUM(RC[-" + (oszlopmax + 1 - j).ToString() + "]";
                    else
                        szöveg += ",RC[-" + (oszlopmax + 1 - j).ToString() + "]";
            }
            szöveg += ")";
            for (int k = 1; k <= hónapnap; k++)
            {
                MyX.Kiir("#KÉPLET#" + szöveg, MyF.Oszlopnév(oszlopmax + 1) + (k + 5).ToString());
                Holtart.Lép();
            }
        }

        private void Kiadott1tábla()
        {
            try
            {
                munkalap = "Forgalmi 1";
                MyX.Munkalap_aktív(munkalap);
                MyX.Link_beillesztés(munkalap, "A1", "Tartalom");

                bool volt = false;
                Napok_kiírása();
                MunkaVHétvége();
                List<Adat_Kiegészítő_Szolgálat> AdatokKiegSzolg = KézKiegSzolgálat.Lista_Adatok();

                int pj = 3;
                int szolgálat = 0;
                Holtart.Be(hónapnap + 1);
                string szöveg = Délelőtt.Checked ? "Reggeli " : "Délutáni ";
                szöveg += "Forgalomba adott darabszámok";
                MyX.Kiir(szöveg, MyF.Oszlopnév(pj) + 3.ToString());

                foreach (Adat_Kiegészítő_Szolgálat rekordkieg in AdatokKiegSzolg)
                {
                    szolgálat += 1;
                    MyX.Kiir(rekordkieg.Szolgálatnév, MyF.Oszlopnév(pj) + "4");
                    volt = false;
                    // főkategória
                    for (int k = 0; k <= Kategórilista.CheckedItems.Count - 1; k++)
                    {
                        for (int i = 1; i <= hónapnap; i++)
                        {
                            Holtart.Value = i;
                            List<Adat_FőKiadási_adatok> Elemek;
                            DateTime AktNap = new DateTime(Dátum.Value.Year, Dátum.Value.Month, i);
                            if (Délelőtt.Checked)
                                Elemek = (from a in AdatokKiad
                                          where a.Napszak == "de"
                                          && a.Főkategória == Kategórilista.CheckedItems[k].ToStrTrim()
                                          && a.Szolgálat == rekordkieg.Szolgálatnév
                                          && a.Dátum == AktNap
                                          select a).ToList();
                            else
                                Elemek = (from a in AdatokKiad
                                          where a.Napszak == "du"
                                          && a.Főkategória == Kategórilista.CheckedItems[k].ToStrTrim()
                                          && a.Szolgálat == rekordkieg.Szolgálatnév
                                          && a.Dátum == AktNap
                                          select a).ToList();

                            MyX.Kiir(Kategórilista.Items[k].ToStrTrim(), MyF.Oszlopnév(pj) + 5.ToString());
                            if (Elemek != null)
                            {
                                long forgalomban = Elemek.Sum(a => a.Forgalomban);
                                MyX.Kiir("#SZÁME#" + forgalomban.ToString(), MyF.Oszlopnév(pj) + (i + 5).ToString());
                                volt = true;
                            }
                            else
                            {
                                MyX.Kiir("#SZÁME#0", MyF.Oszlopnév(pj) + (i + 5).ToString());
                            }
                            Holtart.Lép();
                        }
                        pj += 1;
                    }
                    if (volt == false)
                    {
                        MyX.Kiir("Összesen", MyF.Oszlopnév(pj) + 5.ToString());
                        pj += 1;
                    }
                }
                MyX.Kiir("Összesen", MyF.Oszlopnév(pj) + 4.ToString());
                int oszlopmax = pj;

                // Összesítések
                // A-B Oszlop formázása
                MyX.Oszlopszélesség(munkalap, "B:B", 2);
                MyX.Oszlopszélesség(munkalap, "A:A");
                MyX.Rácsoz(munkalap, "A4:B" + (hónapnap + 5).ToString());
              //  MyX.Vastagkeret(munkalap, "A4:B" + (hónapnap + 5).ToString());

                int eleje;
                if (volt == true)
                {
                    // ha csak egy főkategória volt

                    for (int i = 1; i <= hónapnap; i++)
                    {
                        MyX.Kiir("#KÉPLET#=SUM(RC[-" + (oszlopmax - 3).ToString() + "]:RC[-1])", MyF.Oszlopnév(oszlopmax) + (i + 5).ToString());
                        Holtart.Value = i;
                    }
                    // megformázzuk
                    MyX.Egyesít(munkalap, "c3:" + MyF.Oszlopnév(oszlopmax) + "3");
                   // MyX.Vastagkeret(munkalap, "c4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyX.Rácsoz(munkalap, "c4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyX.Rácsoz(munkalap, "c4:" + MyF.Oszlopnév(oszlopmax) + "5");
                   // MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyX.Betű(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString(), BeBetűVD);
                    // Oszlop szélesség beállítás
                    MyX.Oszlopszélesség(munkalap, "C:" + MyF.Oszlopnév(oszlopmax));
                }
                else
                {
                    // ha több kategória volt

                    // részösszegek
                    eleje = 3;

                    for (int j = 3; j <= oszlopmax; j++)
                    {
                        if (MyX.Beolvas(munkalap, MyF.Oszlopnév(j) + "5") == "Összesen")
                        {
                            for (int i = 1; i <= hónapnap; i++)
                            {
                                MyX.Kiir("#KÉPLET#=SUM(RC[-" + (j - eleje).ToString() + "]:RC[-1])", MyF.Oszlopnév(j) + (i + 5).ToString());
                                Holtart.Lép();
                            }
                            MyX.Egyesít(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(j) + "4");
                          //  MyX.Vastagkeret(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(j) + (hónapnap + 5).ToString());
                            MyX.Rácsoz(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(j) + (hónapnap + 5).ToString());
                           // MyX.Vastagkeret(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(j) + "5");
                            MyX.Betű(munkalap, MyF.Oszlopnév(j) + "5:" + MyF.Oszlopnév(j) + (hónapnap + 5).ToString(), BeBetűV);
                            MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(eleje) + ":" + MyF.Oszlopnév(j));
                            eleje = j + 1;
                        }
                    }
                    // megformázzuk
                    // állomány felirat
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(3) + "3:" + MyF.Oszlopnév(oszlopmax) + "3");
                    // Összesítő rész formázása
                   // MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + "5");
                    MyX.Betű(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString(), BeBetűVD);

                    szöveg = "";
                    szöveg = "=";
                    for (int j = 3; j <= oszlopmax; j++)
                    {
                        if (MyX.Beolvas(munkalap, MyF.Oszlopnév(j) + "5") == "Összesen")
                        {
                            if (szöveg == "=")
                            {
                                szöveg += "SUM(RC[-" + (oszlopmax - j).ToString() + "]";
                            }
                            else
                            {
                                szöveg += ",RC[-" + (oszlopmax - j).ToString() + "]";
                            }
                        }
                    }
                    szöveg += ")";
                    for (int i = 1; i <= hónapnap; i++)
                    {
                        MyX.Kiir("#KÉPLET#" + szöveg, MyF.Oszlopnév(oszlopmax) + (i + 5).ToString());
                        Holtart.Lép();
                    }
                }
                Havi_Összesítő_rész(oszlopmax);
                MyX.Aktív_Cella(munkalap, "A1");
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Kiadott2tábla()
        {
            try
            {
                munkalap = "Forgalmi 2";
                MyX.Munkalap_aktív(munkalap);
                MyX.Link_beillesztés(munkalap, "A1", "Tartalom");

                Napok_kiírása();
                MunkaVHétvége();

                string szöveg = Délelőtt.Checked ? "Reggeli " : "Délutáni ";
                szöveg += "Állományi darabszámok";
                MyX.Kiir(szöveg, MyF.Oszlopnév(3) + 3.ToString());

                // '****************************************************
                // 'Elkészítjük a táblázatot
                // '****************************************************
                List<Adat_Kiegészítő_Típusaltípustábla> AdatokKiegTípusal = Kézkiegtípusal.Lista_Adatok();

                bool volt = false;
                int pj = 3;
                int oszlopmax;
                Holtart.Be(hónapnap + 1);

                string előzőtípus = "";

                for (int k = 0; k <= Kategórilista.CheckedItems.Count - 1; k++)
                {

                    if (előzőtípus.Trim() == "")
                    {
                        MyX.Kiir(Kategórilista.CheckedItems[k].ToStrTrim(), MyF.Oszlopnév(pj) + 4.ToString());
                        előzőtípus = Kategórilista.CheckedItems[k].ToStrTrim();
                    }
                    else
                    {
                        MyX.Kiir("Összesen", MyF.Oszlopnév(pj) + 5.ToString());
                        pj += 1;
                        MyX.Kiir(Kategórilista.CheckedItems[k].ToStrTrim(), MyF.Oszlopnév(pj) + 4.ToString());
                        előzőtípus = Kategórilista.CheckedItems[k].ToStrTrim();
                    }

                    List<Adat_Kiegészítő_Típusaltípustábla> Adatok = (from a in AdatokKiegTípusal
                                                                      where a.Főkategória == Kategórilista.CheckedItems[k].ToStrTrim()
                                                                      orderby a.AlTípus
                                                                      select a).ToList();

                    foreach (Adat_Kiegészítő_Típusaltípustábla rekordkieg in Adatok)
                    {
                        for (int i = 1; i <= hónapnap; i++)
                        {
                            List<Adat_FőKiadási_adatok> Elemek;
                            DateTime AktNap = new DateTime(Dátum.Value.Year, Dátum.Value.Month, i);
                            if (Délelőtt.Checked)
                                Elemek = (from a in AdatokKiad
                                          where a.Napszak == "de"
                                          && a.Főkategória == Kategórilista.CheckedItems[k].ToStrTrim()
                                          && a.Altípus == rekordkieg.AlTípus
                                          && a.Dátum == AktNap
                                          orderby a.Altípus
                                          select a).ToList();
                            else
                                Elemek = (from a in AdatokKiad
                                          where a.Napszak == "du"
                                          && a.Főkategória == Kategórilista.CheckedItems[k].ToStrTrim()
                                          && a.Altípus == rekordkieg.AlTípus
                                          && a.Dátum == AktNap
                                          orderby a.Altípus
                                          select a).ToList();

                            MyX.Kiir(rekordkieg.AlTípus, MyF.Oszlopnév(pj) + 5.ToString());
                            if (Elemek != null)
                            {
                                long forgalomban = Elemek.Sum(a => a.Forgalomban);
                                MyX.Kiir("#SZÁME#" + forgalomban.ToString(), MyF.Oszlopnév(pj) + (i + 5).ToString());
                            }
                            else
                            {
                                MyX.Kiir("#SZÁME#0", MyF.Oszlopnév(pj) + (i + 5).ToString());
                            }
                        }
                        oszlopmax = pj;
                        pj += 1;
                        Holtart.Lép();
                    }
                }

                if (volt != true)
                {
                    MyX.Kiir("Összesen", MyF.Oszlopnév(pj) + 5.ToString());
                    pj += 1;
                }
                MyX.Kiir("Összesen", MyF.Oszlopnév(pj) + 4.ToString());
                oszlopmax = pj;
                // Összesítések
                // A-B Oszlop formázása
                MyX.Oszlopszélesség(munkalap, "B:B", 2);
                MyX.Oszlopszélesség(munkalap, "A:A");

                MyX.Rácsoz(munkalap, "A4:B" + (hónapnap + 5).ToString());
               // MyX.Vastagkeret(munkalap, "A4:B" + (hónapnap + 5).ToString());

                int eleje;
                if (volt == true)
                {
                    // ha csak egy főkategória volt

                    for (int i = 1; i <= hónapnap; i++)
                    {
                        MyX.Kiir("#KÉPLET#=SUM(RC[-" + (oszlopmax - 3).ToString() + "]:RC[-1])", MyF.Oszlopnév(oszlopmax) + (i + 5).ToString());
                        Holtart.Value = i;
                    }
                    // megformázzuk
                    MyX.Egyesít(munkalap, "c3:" + MyF.Oszlopnév(oszlopmax) + "3");
                  //  MyX.Vastagkeret(munkalap, "c4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyX.Rácsoz(munkalap, "c4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyX.Rácsoz(munkalap, "c4:" + MyF.Oszlopnév(oszlopmax) + "5");
                   // MyX.Vastagkeret(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyX.Betű(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString(), BeBetűVD);
                    // Oszlop szélesség beállítás
                    MyX.Oszlopszélesség(munkalap, "C:" + MyF.Oszlopnév(oszlopmax));
                }

                else
                {
                    // ha több kategória volt
                    // részösszegek
                    eleje = 3;
                    for (int j = 3; j <= oszlopmax; j++)
                    {
                        if (MyX.Beolvas(munkalap, MyF.Oszlopnév(j) + "5") == "Összesen")
                        {
                            for (int i = 1; i <= hónapnap; i++)
                            {
                                MyX.Kiir("#KÉPLET#=SUM(RC[-" + (j - eleje).ToString() + "]:RC[-1])", MyF.Oszlopnév(j) + (i + 5).ToString());
                                Holtart.Value = i;
                            }
                            MyX.Egyesít(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(j) + "4");
                          //  MyX.Vastagkeret(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(j) + (hónapnap + 5).ToString());
                            MyX.Rácsoz(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(j) + (hónapnap + 5).ToString());
                            MyX.Rácsoz(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(j) + "5");
                            MyX.Betű(munkalap, MyF.Oszlopnév(j) + "5:" + MyF.Oszlopnév(j) + (hónapnap + 5).ToString(), BeBetűV);
                            MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(eleje) + ":" + MyF.Oszlopnév(j));
                            eleje = j + 1;
                        }
                    }


                    // megformázzuk
                    // állomány felirat
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(3) + "3:" + MyF.Oszlopnév(oszlopmax) + "3");

                    // Összesítő rész formázása
                   // MyX.Vastagkeret(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + "5");
                    MyX.Betű(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString(), BeBetűVD);

                    szöveg = "";
                    szöveg = "=";

                    for (int j = 3; j <= oszlopmax; j++)
                    {
                        if (MyX.Beolvas(munkalap, MyF.Oszlopnév(j) + "5") == "Összesen")
                        {
                            if (szöveg == "=")
                            {
                                szöveg += "SUM(RC[-" + (oszlopmax - j).ToString() + "]";
                            }
                            else
                            {
                                szöveg += ",RC[-" + (oszlopmax - j).ToString() + "]";
                            }
                        }
                    }
                    szöveg += ")";

                    for (int i = 1; i <= hónapnap; i++)
                    {
                        MyX.Kiir("#KÉPLET#" + szöveg, MyF.Oszlopnév(oszlopmax) + (i + 5).ToString());
                        Holtart.Value = i;
                    }
                }
                Havi_Összesítő_rész(oszlopmax);
                MyX.Aktív_Cella(munkalap, "A1");
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void Kiadott3tábla()
        {
            try
            {
                munkalap = "Forgalmi 3";
                MyX.Munkalap_aktív("Forgalmi 3");
                MyX.Link_beillesztés(munkalap, "A1", "Tartalom");

                Napok_kiírása();
                MunkaVHétvége();

                Rácsoz_3(munkalap);
                Havi_Összesítő_rész(Oszlop_Max);
                MyX.Aktív_Cella(munkalap, "A1");
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Üzemképes1tábla()
        {
            try
            {
                munkalap = "Üzemképes 1";
                MyX.Munkalap_aktív(munkalap);
                MyX.Link_beillesztés(munkalap, "A1", "Tartalom");
                Napok_kiírása();
                MunkaVHétvége();

                // ****************************************************
                // Elkészítjük a táblázatot
                // ****************************************************

                List<Adat_Kiegészítő_Szolgálat> AdatokKiegSzolgálat = KézKiegSzolgálat.Lista_Adatok();

                string szöveg = Délelőtt.Checked ? "Reggeli " : "Délutáni ";
                szöveg += "Üzemképes darabszámok";
                int jj = 3;
                MyX.Kiir(szöveg, MyF.Oszlopnév(jj) + 3.ToString());

                Holtart.Be(hónapnap + 1);

                bool volt = false;
                foreach (Adat_Kiegészítő_Szolgálat rekordkieg in AdatokKiegSzolgálat)
                {

                    MyX.Kiir(rekordkieg.Szolgálatnév, MyF.Oszlopnév(jj) + 4.ToString());

                    // főkategória
                    for (int k = 0; k <= Kategórilista.CheckedItems.Count - 1; k++)
                    {
                        for (int ki = 1; ki <= hónapnap; ki++)
                        {
                            List<Adat_FőKiadási_adatok> Elemek;
                            DateTime AktNap = new DateTime(Dátum.Value.Year, Dátum.Value.Month, ki);
                            if (Délelőtt.Checked)
                                Elemek = (from a in AdatokKiad
                                          where a.Napszak == "de"
                                          && a.Főkategória == Kategórilista.CheckedItems[k].ToStrTrim()
                                          && a.Szolgálat == rekordkieg.Szolgálatnév
                                          && a.Dátum == AktNap
                                          select a).ToList();
                            else
                                Elemek = (from a in AdatokKiad
                                          where a.Napszak == "du"
                                          && a.Főkategória == Kategórilista.CheckedItems[k].ToStrTrim()
                                          && a.Szolgálat == rekordkieg.Szolgálatnév
                                          && a.Dátum == AktNap
                                          select a).ToList();

                            MyX.Kiir(Kategórilista.Items[k].ToStrTrim(), MyF.Oszlopnév(jj) + 5.ToString());
                            if (Elemek != null)
                            {
                                long forgalomban = Elemek.Sum(a => a.Forgalomban);
                                long tartalék = Elemek.Sum(a => a.Tartalék);

                                long érték = forgalomban + tartalék;
                                MyX.Kiir("#SZÁME#" + érték.ToString(), MyF.Oszlopnév(jj) + (ki + 5).ToString());
                                volt = true;
                            }
                            else
                            {
                                MyX.Kiir("#SZÁME#0", MyF.Oszlopnév(jj) + (ki + 5).ToString());
                            }

                        }
                        jj += 1;

                        Holtart.Lép();
                    }
                    if (volt == true)
                    {
                        MyX.Kiir("Összesen", MyF.Oszlopnév(jj) + 5.ToString());
                        jj += 1;
                    }
                    volt = false;
                }
                MyX.Kiir("Összesen", MyF.Oszlopnév(jj) + 4.ToString());
                int oszlopmax = jj;

                // Összesítések
                // A-B Oszlop formázása
                MyX.Oszlopszélesség(munkalap, "B:B", 2);
                MyX.Oszlopszélesség(munkalap, "A:A");

             //   MyX.Vastagkeret(munkalap, "A4:B" + (hónapnap + 5).ToString());
                MyX.Rácsoz(munkalap, "A4:B" + (hónapnap + 5).ToString());
                
                // állomány felirat
                MyX.Egyesít(munkalap, MyF.Oszlopnév(3) + "3:" + MyF.Oszlopnév(oszlopmax) + "3");
                if (volt == true)
                {
                    // ha csak egy főkategória volt
                    for (int vi = 1; vi <= hónapnap; vi++)
                    {
                        MyX.Kiir("#KÉPLET#=SUM(RC[-" + (oszlopmax - 3).ToString() + "]:RC[-1])", MyF.Oszlopnév(oszlopmax) + (vi + 5).ToString());
                        Holtart.Value = vi;
                    }
                    // megformázzuk
                   // MyX.Vastagkeret(munkalap, "c4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyX.Rácsoz(munkalap, "c4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyX.Rácsoz(munkalap, "c4:" + MyF.Oszlopnév(oszlopmax) + "5");
                  //  MyX.Vastagkeret(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyX.Betű(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString(), BeBetűVD);
                    // Oszlop szélesség beállítás
                    MyX.Oszlopszélesség(munkalap, "C:" + MyF.Oszlopnév(oszlopmax));

                }

                else
                {
                    // ha több kategória volt

                    // részösszegek
                    int eleje = 3;
                    for (int wj = 3; wj <= oszlopmax; wj++)
                    {
                        if (MyX.Beolvas(munkalap, MyF.Oszlopnév(wj) + "5") == "Összesen")
                        {

                            for (int wi = 1; wi <= hónapnap; wi++)
                            {
                                MyX.Kiir("#KÉPLET#=SUM(RC[-" + (wj - eleje).ToString() + "]:RC[-1])", MyF.Oszlopnév(wj) + (wi + 5).ToString());
                                Holtart.Value = wi;
                            }
                            MyX.Egyesít(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(wj) + "4");
                           // MyX.Vastagkeret(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(wj) + (hónapnap + 5).ToString());
                            MyX.Rácsoz(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(wj) + (hónapnap + 5).ToString());
                            MyX.Rácsoz(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(wj) + "5");
                            MyX.Betű(munkalap, MyF.Oszlopnév(wj) + "5:" + MyF.Oszlopnév(wj) + (hónapnap + 5).ToString(), BeBetűV);
                            MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(eleje) + ":" + MyF.Oszlopnév(wj));

                            eleje = wj + 1;
                        }
                    }
                    // megformázzuk
                    // Összesítő rész formázása
                 //   MyX.Vastagkeret(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + "5");
                    MyX.Betű(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString(), BeBetűVD);
                    szöveg = "=";

                    for (int j = 3; j <= oszlopmax; j++)
                    {
                        if (MyX.Beolvas(munkalap, MyF.Oszlopnév(j) + "5") == "Összesen")
                        {
                            if (szöveg == "=")
                            {
                                szöveg += "SUM(RC[-" + (oszlopmax - j).ToString() + "]";
                            }
                            else
                            {
                                szöveg += ",RC[-" + (oszlopmax - j).ToString() + "]";
                            }
                        }
                    }
                    szöveg += ")";
                    for (int i = 1; i <= hónapnap; i++)
                    {
                        MyX.Kiir("#KÉPLET#" + szöveg, MyF.Oszlopnév(oszlopmax) + (i + 5).ToString());
                        Holtart.Value = i;
                    }
                }
                // Alsó összesítés és átlag
                Havi_Összesítő_rész(oszlopmax);
                MyX.Aktív_Cella(munkalap, "A1");
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Üzemképes2tábla()
        {
            try
            {
                munkalap = "Üzemképes 2";
                MyX.Munkalap_aktív(munkalap);
                MyX.Link_beillesztés(munkalap, "A1", "Tartalom");
                Napok_kiírása();
                MunkaVHétvége();

                string szöveg = Délelőtt.Checked ? "Reggeli " : "Délutáni ";
                szöveg += "Állományi darabszámok";
                MyX.Kiir(szöveg, MyF.Oszlopnév(3) + 3.ToString());

                // '****************************************************
                // 'Elkészítjük a táblázatot
                // '****************************************************
                List<Adat_Kiegészítő_Típusaltípustábla> AdatokKiegTípusal = Kézkiegtípusal.Lista_Adatok();

                bool volt = false;
                int pj = 3;
                int oszlopmax;
                Holtart.Be(hónapnap + 1);

                string előzőtípus = "";

                for (int k = 0; k <= Kategórilista.CheckedItems.Count - 1; k++)
                {

                    if (előzőtípus.Trim() == "")
                    {
                        MyX.Kiir(Kategórilista.CheckedItems[k].ToStrTrim(), MyF.Oszlopnév(pj) + 4.ToString());
                        előzőtípus = Kategórilista.CheckedItems[k].ToStrTrim();
                    }
                    else
                    {
                        MyX.Kiir("Összesen", MyF.Oszlopnév(pj) + 5.ToString());
                        pj += 1;
                        MyX.Kiir(Kategórilista.CheckedItems[k].ToStrTrim(), MyF.Oszlopnév(pj) + 4.ToString());
                        előzőtípus = Kategórilista.CheckedItems[k].ToStrTrim();
                    }

                    List<Adat_Kiegészítő_Típusaltípustábla> Adatok = (from a in AdatokKiegTípusal
                                                                      where a.Főkategória == Kategórilista.CheckedItems[k].ToStrTrim()
                                                                      orderby a.AlTípus
                                                                      select a).ToList();

                    foreach (Adat_Kiegészítő_Típusaltípustábla rekordkieg in Adatok)
                    {
                        for (int i = 1; i <= hónapnap; i++)
                        {
                            List<Adat_FőKiadási_adatok> Elemek;
                            DateTime AktNap = new DateTime(Dátum.Value.Year, Dátum.Value.Month, i);
                            if (Délelőtt.Checked)
                                Elemek = (from a in AdatokKiad
                                          where a.Napszak == "de"
                                          && a.Főkategória == Kategórilista.CheckedItems[k].ToStrTrim()
                                          && a.Altípus == rekordkieg.AlTípus
                                          && a.Dátum == AktNap
                                          orderby a.Altípus
                                          select a).ToList();
                            else
                                Elemek = (from a in AdatokKiad
                                          where a.Napszak == "du"
                                          && a.Főkategória == Kategórilista.CheckedItems[k].ToStrTrim()
                                          && a.Altípus == rekordkieg.AlTípus
                                          && a.Dátum == AktNap
                                          orderby a.Altípus
                                          select a).ToList();

                            MyX.Kiir(rekordkieg.AlTípus, MyF.Oszlopnév(pj) + 5.ToString());
                            if (Elemek != null)
                            {
                                long forgalomban = Elemek.Sum(a => a.Forgalomban);
                                long tartalék = Elemek.Sum(a => a.Tartalék);

                                long érték = forgalomban + tartalék;
                                MyX.Kiir("#SZÁME#" + érték.ToString(), MyF.Oszlopnév(pj) + (i + 5).ToString());
                            }
                            else
                            {
                                MyX.Kiir("#SZÁME#0", MyF.Oszlopnév(pj) + (i + 5).ToString());
                            }
                        }
                        oszlopmax = pj;
                        pj += 1;
                        Holtart.Lép();
                    }
                }

                if (volt != true)
                {
                    MyX.Kiir("Összesen", MyF.Oszlopnév(pj) + 5.ToString());
                    pj += 1;
                }
                MyX.Kiir("Összesen", MyF.Oszlopnév(pj) + 4.ToString());
                oszlopmax = pj;
                // Összesítések
                // A-B Oszlop formázása
                MyX.Oszlopszélesség(munkalap, "B:B", 2);
                MyX.Oszlopszélesség(munkalap, "A:A");

              //  MyX.Vastagkeret(munkalap, "A4:B" + (hónapnap + 5).ToString());
                MyX.Rácsoz(munkalap, "A4:B" + (hónapnap + 5).ToString());
                
                int eleje;
                if (volt == true)
                {
                    // ha csak egy főkategória volt

                    for (int i = 1; i <= hónapnap; i++)
                    {
                        MyX.Kiir("#KÉPLET#=SUM(RC[-" + (oszlopmax - 3).ToString() + "]:RC[-1])", MyF.Oszlopnév(oszlopmax) + (i + 5).ToString());
                        Holtart.Value = i;
                    }
                    // megformázzuk
                    MyX.Egyesít(munkalap, "c3:" + MyF.Oszlopnév(oszlopmax) + "3");
                   // MyX.Vastagkeret(munkalap, "c4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyX.Rácsoz(munkalap, "c4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyX.Rácsoz(munkalap, "c4:" + MyF.Oszlopnév(oszlopmax) + "5");
                  //  MyX.Vastagkeret(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyX.Betű(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString(), BeBetűVD);
                    // Oszlop szélesség beállítás
                    MyX.Oszlopszélesség(munkalap, "C:" + MyF.Oszlopnév(oszlopmax));
                }

                else
                {
                    // ha több kategória volt
                    // részösszegek
                    eleje = 3;
                    for (int j = 3; j <= oszlopmax; j++)
                    {
                        if (MyX.Beolvas(munkalap, MyF.Oszlopnév(j) + "5") == "Összesen")
                        {
                            for (int i = 1; i <= hónapnap; i++)
                            {
                                MyX.Kiir("#KÉPLET#=SUM(RC[-" + (j - eleje).ToString() + "]:RC[-1])", MyF.Oszlopnév(j) + (i + 5).ToString());
                                Holtart.Value = i;
                            }
                            MyX.Egyesít(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(j) + "4");
                            //MyX.Vastagkeret(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(j) + (hónapnap + 5).ToString());
                            MyX.Rácsoz(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(j) + (hónapnap + 5).ToString());
                            MyX.Rácsoz(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(j) + "5");
                            MyX.Betű(munkalap, MyF.Oszlopnév(j) + "5:" + MyF.Oszlopnév(j) + (hónapnap + 5).ToString(), BeBetűV);
                            MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(eleje) + ":" + MyF.Oszlopnév(j));
                            eleje = j + 1;
                        }
                    }


                    // megformázzuk
                    // állomány felirat
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(3) + "3:" + MyF.Oszlopnév(oszlopmax) + "3");

                    // Összesítő rész formázása
                    //MyX.Vastagkeret(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + "5");
                    MyX.Betű(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString(), BeBetűVD);

                    szöveg = "";
                    szöveg = "=";

                    for (int j = 3; j <= oszlopmax; j++)
                    {
                        if (MyX.Beolvas(munkalap, MyF.Oszlopnév(j) + "5") == "Összesen")
                        {
                            if (szöveg == "=")
                            {
                                szöveg += "SUM(RC[-" + (oszlopmax - j).ToString() + "]";
                            }
                            else
                            {
                                szöveg += ",RC[-" + (oszlopmax - j).ToString() + "]";
                            }
                        }
                    }
                    szöveg += ")";

                    for (int i = 1; i <= hónapnap; i++)
                    {
                        MyX.Kiir("#KÉPLET#" + szöveg, MyF.Oszlopnév(oszlopmax) + (i + 5).ToString());
                        Holtart.Value = i;
                    }
                }
                Havi_Összesítő_rész(oszlopmax);
                MyX.Aktív_Cella(munkalap, "A1");

            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Üzemképes3tábla()
        {
            try
            {
                munkalap = "Üzemképes 3";
                MyX.Munkalap_aktív(munkalap);
                MyX.Link_beillesztés(munkalap, "A1", "Tartalom");

                Napok_kiírása();
                MunkaVHétvége();

                Rácsoz_3(munkalap);
                Havi_Összesítő_rész(Oszlop_Max);
                MyX.Aktív_Cella(munkalap, "A1");
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Adatkiiró1()
        {
            try
            {
                alsóPanels4.Text = "";
                munkalap = "Adatok 1";
                MyX.Munkalap_aktív(munkalap);
                MyX.Link_beillesztés(munkalap, "A1", "Tartalom");

                // hónap eleje és vége
                int hónapnap = MyF.Hónap_hossza(Dátum.Value);
                DateTime hónaputolsónapja = MyF.Hónap_utolsónapja(Dátum.Value);
                DateTime hónapelsőnapja = MyF.Hónap_elsőnapja(Dátum.Value);

                // fejléc elkészítése
                MyX.Kiir("Dátum", "A5");
                MyX.Kiir("Napszak", "B5");
                MyX.Kiir("Főkategória", "C5");
                MyX.Kiir("Típus", "d5");
                MyX.Kiir("AlTípus", "e5");
                MyX.Kiir("Szolgálat", "F5");
                MyX.Kiir("Telephely", "g5");
                MyX.Kiir("Kiadás", "h5");
                MyX.Kiir("Forgalomban", "i5");
                MyX.Kiir("Eltérés", "j5");
                MyX.Kiir("Tartalék", "k5");
                MyX.Kiir("Kocsiszíni", "l5");
                MyX.Kiir("Félreállítás", "m5");
                MyX.Kiir("Főjavítás", "n5");
                MyX.Kiir("Állomány", "o5");
                MyX.Kiir("Személyzethiány", "p5");
                MyX.Kiir("Munkanap", "q5");
                MyX.Kiir("Tart.+Szem.hiány", "r5");

                Holtart.Be(50);

                List<Adat_FőKiadási_adatok> AdatokKiadAd = KézKiad.Lista_adatok(Dátum.Value.Year);
                AdatokKiadAd = (from a in AdatokKiadAd
                                where a.Dátum >= hónapelsőnapja
                                && a.Dátum <= hónaputolsónapja
                                && a.Napszak == (Délelőtt.Checked ? "de" : "du")
                                orderby a.Dátum
                                select a).ToList();
                int i = 6;

                foreach (Adat_FőKiadási_adatok rekord in AdatokKiadAd)
                {

                    MyX.Kiir(DateTime.Parse(rekord.Dátum.ToString()).ToString("yyyy.MM.dd"), "A" + i.ToString());
                    MyX.Kiir(rekord.Napszak, "B" + i.ToString());
                    MyX.Kiir(rekord.Főkategória, "C" + i.ToString());
                    MyX.Kiir(rekord.Típus, "d" + i.ToString());
                    MyX.Kiir(rekord.Altípus, "e" + i.ToString());
                    MyX.Kiir(rekord.Szolgálat, "F" + i.ToString());
                    MyX.Kiir(rekord.Telephely, "g" + i.ToString());
                    MyX.Kiir("#SZÁME#" + rekord.Kiadás.ToString(), "h" + i.ToString());
                    MyX.Kiir("#SZÁME#" + rekord.Forgalomban.ToString(), "i" + i.ToString());
                    MyX.Kiir("#SZÁME#" + (int.Parse(rekord.Kiadás.ToString()) - int.Parse(rekord.Forgalomban.ToString())).ToString(), "j" + i.ToString());
                    MyX.Kiir("#SZÁME#" + rekord.Tartalék.ToString(), "k" + i.ToString());
                    MyX.Kiir("#SZÁME#" + rekord.Kocsiszíni.ToString(), "l" + i.ToString());
                    MyX.Kiir("#SZÁME#" + rekord.Félreállítás.ToString(), "m" + i.ToString());
                    MyX.Kiir("#SZÁME#" + rekord.Főjavítás.ToString(), "n" + i.ToString());
                    MyX.Kiir("#SZÁME#" + rekord.Személyzet.ToString(), "p" + i.ToString());
                    MyX.Kiir("#SZÁME#" + (int.Parse(rekord.Tartalék.ToString()) + int.Parse(rekord.Személyzet.ToString())).ToString(), "r" + i.ToString());
                    if (int.Parse(rekord.Munkanap.ToString()) == 0)
                        MyX.Kiir("Munkanap", "q" + i.ToString());
                    else
                        MyX.Kiir("Hétvége", "q" + i.ToString());

                    int összesen = int.Parse(rekord.Forgalomban.ToString()) + int.Parse(rekord.Tartalék.ToString()) + int.Parse(rekord.Kocsiszíni.ToString())
                        + int.Parse(rekord.Félreállítás.ToString()) + int.Parse(rekord.Főjavítás.ToString()) + int.Parse(rekord.Személyzet.ToString());
                    MyX.Kiir("#SZÁME#" + összesen.ToString(), "o" + i.ToString());

                    Holtart.Lép();
                    i += 1;
                }

                alsóPanels4.Text = i.ToString();

                MyX.Oszlopszélesség(munkalap, "A:R");
                MyX.Aktív_Cella(munkalap, "A1");
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Adatkiiró2()
        {
            try
            {
                munkalap = "Adatok 2";
                MyX.Munkalap_aktív(munkalap);
                MyX.Link_beillesztés(munkalap, "A1", "Tartalom");

                // hónap eleje és vége
                int hónapnap = MyF.Hónap_hossza(Dátum.Value);
                DateTime hónaputolsónapja = MyF.Hónap_utolsónapja(Dátum.Value);
                DateTime hónapelsőnapja = MyF.Hónap_elsőnapja(Dátum.Value);

                // fejléc elkészítése
                MyX.Kiir("Dátum", "A5");
                MyX.Kiir("Napszak", "B5");
                MyX.Kiir("Telephely", "C5");
                MyX.Kiir("Szolgálat", "D5");
                MyX.Kiir("Típus", "E5");
                MyX.Kiir("Viszonylat", "F5");
                MyX.Kiir("Forgalmiszám", "G5");
                MyX.Kiir("tervindulás", "H5");
                MyX.Kiir("Azonosító", "I5");

                Holtart.Be(50);

                List<Adat_Személyzet_Adatok> AdatokSzemAd = KézSzem.Lista_adatok(Dátum.Value.Year);
                AdatokSzemAd = (from a in AdatokSzemAd
                                where a.Dátum >= hónapelsőnapja
                                && a.Dátum <= hónaputolsónapja
                                && a.Napszak == (Délelőtt.Checked ? "de" : "du")
                                orderby a.Dátum, a.Viszonylat, a.Tervindulás
                                select a).ToList();
                int i = 6;
                foreach (Adat_Személyzet_Adatok rekord in AdatokSzemAd)
                {

                    MyX.Kiir(DateTime.Parse(rekord.Dátum.ToString()).ToString("yyyy.MM.dd"), "A" + i.ToString());
                    MyX.Kiir(rekord.Napszak, "B" + i.ToString());
                    MyX.Kiir(rekord.Telephely, "C" + i.ToString());
                    MyX.Kiir(rekord.Szolgálat, "D" + i.ToString());
                    MyX.Kiir(rekord.Típus, "E" + i.ToString());
                    MyX.Kiir(rekord.Viszonylat, "F" + i.ToString());
                    MyX.Kiir(rekord.Forgalmiszám, "G" + i.ToString());
                    MyX.Kiir(rekord.Tervindulás.ToString(), "H" + i.ToString());
                    MyX.Kiir(rekord.Azonosító, "I" + i.ToString());
                    Holtart.Lép();
                    i += 1;
                }
                MyX.Oszlopszélesség(munkalap, "A:i");
                MyX.Aktív_Cella(munkalap, "A1");
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Adatkiiró3()
        {
            try
            {
                munkalap = "Adatok 3";
                MyX.Munkalap_aktív(munkalap);
                MyX.Link_beillesztés(munkalap, "A1", "Tartalom");

                // hónap eleje és vége
                int hónapnap = MyF.Hónap_hossza(Dátum.Value);
                DateTime hónaputolsónapja = MyF.Hónap_utolsónapja(Dátum.Value);
                DateTime hónapelsőnapja = MyF.Hónap_elsőnapja(Dátum.Value);

                // fejléc elkészítése
                MyX.Kiir("Dátum", "A5");
                MyX.Kiir("Napszak", "B5");
                MyX.Kiir("Telephely", "C5");
                MyX.Kiir("Szolgálat", "D5");
                MyX.Kiir("Típuselőírt", "E5");
                MyX.Kiir("Típuskiadott", "F5");
                MyX.Kiir("Viszonylat", "G5");
                MyX.Kiir("Forgalmiszám", "H5");
                MyX.Kiir("tervindulás", "I5");
                MyX.Kiir("Azonosító", "J5");

                Holtart.Be(50);

                List<Adat_Típuscsere_Adatok> AdatokTípuscsereAdatok = KézTípCsere.Lista_adatok(Dátum.Value.Year);
                AdatokTípuscsereAdatok = (from a in AdatokTípuscsereAdatok
                                          where a.Dátum >= hónapelsőnapja
                                          && a.Dátum <= hónaputolsónapja
                                          && a.Napszak == (Délelőtt.Checked ? "de" : "du")
                                          orderby a.Dátum, a.Viszonylat, a.Tervindulás
                                          select a).ToList();
                int i = 6;
                foreach (Adat_Típuscsere_Adatok rekord in AdatokTípuscsereAdatok)
                {

                    MyX.Kiir(DateTime.Parse(rekord.Dátum.ToString()).ToString("yyyy.MM.dd"), "A" + i.ToString());
                    MyX.Kiir("#SZÁME#" + rekord.Napszak, "B" + i.ToString());
                    MyX.Kiir("#SZÁME#" + rekord.Telephely, "C" + i.ToString());
                    MyX.Kiir("#SZÁME#" + rekord.Szolgálat, "D" + i.ToString());
                    MyX.Kiir("#SZÁME#" + rekord.Típuselőírt, "E" + i.ToString());
                    MyX.Kiir("#SZÁME#" + rekord.Típuskiadott, "f" + i.ToString());
                    MyX.Kiir("#SZÁME#" + rekord.Viszonylat, "g" + i.ToString());
                    MyX.Kiir("#SZÁME#" + rekord.Forgalmiszám, "h" + i.ToString());
                    MyX.Kiir("#SZÁME#" + rekord.Tervindulás.ToString(), "i" + i.ToString());
                    MyX.Kiir("#SZÁME#" + rekord.Azonosító, "j" + i.ToString());
                    Holtart.Lép();
                    i += 1;
                }
                MyX.Oszlopszélesség(munkalap, "A:j");
                MyX.Aktív_Cella(munkalap, "A1");
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Telephelytábla()
        {
            try
            {
                munkalap = "Kocsiszín";
                MyX.Munkalap_aktív(munkalap);
                MyX.Link_beillesztés(munkalap, "A1", "Tartalom");

                string szöveg = "Kocsiszíni darabszámok";
                MyX.Kiir(szöveg, MyF.Oszlopnév(3) + 3.ToString());

                Napok_kiírása();
                MunkaVHétvége();

                List<Adat_kiegészítő_telephely> AdatokKiegTeleph = KézKiegTelep.Lista_Adatok();

                int oszlopmax = 0, eleje;
                Holtart.Be(hónapnap + 1);
                int pj = 3;

                for (int k = 0; k <= Kategórilista.CheckedItems.Count - 1; k++)
                {

                    foreach (Adat_kiegészítő_telephely rekordkieg in AdatokKiegTeleph)
                    {

                        List<Adat_FőKiadási_adatok> Adatok;
                        if (Délelőtt.Checked)
                            Adatok = (from a in AdatokKiad
                                      where a.Főkategória == Kategórilista.CheckedItems[k].ToStrTrim()
                                      && a.Telephely == rekordkieg.Telephelynév
                                      && a.Dátum >= hónapelsőnapja
                                      && a.Dátum <= hónaputolsónapja
                                      && a.Napszak == "de"
                                      orderby a.Főkategória, a.Telephely, a.Altípus, a.Dátum
                                      select a).ToList();
                        else
                            Adatok = (from a in AdatokKiad
                                      where a.Főkategória == Kategórilista.CheckedItems[k].ToStrTrim()
                                      && a.Telephely == rekordkieg.Telephelynév
                                      && a.Dátum >= hónapelsőnapja
                                      && a.Dátum <= hónaputolsónapja
                                      && a.Napszak == "du"
                                      orderby a.Főkategória, a.Telephely, a.Altípus, a.Dátum
                                      select a).ToList();

                        string előzőtípus = "";
                        if (Adatok.Count > 0)
                        {
                            MyX.Kiir(rekordkieg.Telephelynév, MyF.Oszlopnév(pj) + 4.ToString());

                            foreach (Adat_FőKiadási_adatok Elem in Adatok)
                            {
                                int sor = Elem.Dátum.Day;
                                long érték = Elem.Forgalomban + Elem.Tartalék + Elem.Kocsiszíni + Elem.Félreállítás + Elem.Főjavítás + Elem.Személyzet;
                                if (előzőtípus.Trim() == "") előzőtípus = Elem.Altípus;
                                if (előzőtípus != Elem.Altípus)
                                {
                                    pj++;
                                    előzőtípus = Elem.Altípus;
                                }
                                MyX.Kiir(Elem.Altípus, MyF.Oszlopnév(pj) + 5.ToString());
                                MyX.Kiir("#SZÁME#" + érték.ToString(), MyF.Oszlopnév(pj) + (sor + 5).ToString());
                            }
                            pj += 1;
                            MyX.Kiir("Összesen", MyF.Oszlopnév(pj) + 5.ToString());
                            pj += 1;
                        }
                    }
                }
                MyX.Kiir("Összesen", MyF.Oszlopnév(pj) + 4.ToString());
                oszlopmax = pj;

                // Összesítések
                // A-B Oszlop formázása
                MyX.Oszlopszélesség(munkalap, "B:B", 2);
                MyX.Oszlopszélesség(munkalap, "A:A");
                //MyX.Vastagkeret(munkalap, "A4:B" + (hónapnap + 5).ToString());
                MyX.Rácsoz(munkalap, "A4:B" + (hónapnap + 5).ToString());
                // állomány felirat
                MyX.Egyesít(munkalap, MyF.Oszlopnév(3) + "3:" + MyF.Oszlopnév(oszlopmax) + "3");
                MyX.Rácsoz(munkalap, MyF.Oszlopnév(3) + "3:" + MyF.Oszlopnév(oszlopmax) + "3");


                eleje = 3;
                for (int j = 3; j <= oszlopmax; j++)
                {
                    if (MyX.Beolvas(munkalap, MyF.Oszlopnév(j) + "5") == "Összesen")
                    {
                        for (int i = 1; i <= hónapnap; i++)
                        {
                            MyX.Kiir("#KÉPLET#=SUM(RC[-" + (j - eleje).ToString() + "]:RC[-1])", MyF.Oszlopnév(j) + (i + 5).ToString());
                            Holtart.Value = i;
                        }
                        MyX.Egyesít(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(j) + "4");
                       // MyX.Vastagkeret(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(j) + (hónapnap + 5).ToString());
                        MyX.Rácsoz(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(j) + (hónapnap + 5).ToString());
                        MyX.Rácsoz(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(j) + "5");
                        MyX.Betű(munkalap, MyF.Oszlopnév(j) + "5:" + MyF.Oszlopnév(j) + (hónapnap + 5).ToString(), BeBetűV);
                        MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(eleje) + ":" + MyF.Oszlopnév(j));

                        eleje = j + 1;
                    }
                }
                // ha csak egy főkategória volt
                szöveg = "=";
                for (int j = 3; j <= oszlopmax; j++)
                {
                    if (MyX.Beolvas(munkalap, MyF.Oszlopnév(j) + "5") == "Összesen")
                    {
                        if (szöveg == "=")
                        {
                            szöveg += "SUM(RC[-" + (oszlopmax - j).ToString() + "]";
                        }
                        else
                        {
                            szöveg += ",RC[-" + (oszlopmax - j).ToString() + "]";
                        }
                    }
                }
                szöveg += ")";
                for (int i = 1; i <= hónapnap; i++)
                {
                    MyX.Kiir("#KÉPLET#" + szöveg, MyF.Oszlopnév(oszlopmax) + (i + 5).ToString());
                    Holtart.Lép();
                }
                // megformázzuk
                // MyX.Egyesít("c3:" + oszlopnév(oszlopmax) + "3")
               // MyX.Vastagkeret(munkalap, "c4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                MyX.Rácsoz(munkalap, "c4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                MyX.Rácsoz(munkalap, "c4:" + MyF.Oszlopnév(oszlopmax) + "5");
               // MyX.Vastagkeret(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                MyX.Betű(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString(), BeBetűVD);
                // Oszlop szélesség beállítás
                MyX.Oszlopszélesség(munkalap, "C:" + MyF.Oszlopnév(oszlopmax));

                // Alsó összesítés és átlag
                Havi_Összesítő_rész(oszlopmax);
                MyX.Aktív_Cella(munkalap, "A1");
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Kimutatásvarázsló()
        {
            try
            {
                munkalap = "Adatok 1";
                string balfelső = "A5";
                string jobbalsó = "R" + alsóPanels4.Text.Trim();
                string kimutatás_munkalap = "Kimutatás";
                MyX.Link_beillesztés(kimutatás_munkalap, "A1", "Tartalom");
                string Kimutatás_cella = "A8";
                string Kimutatás_név = "Kimutatás1";
                List<string> összesítNév = new List<string>();
                List<string> sorNév = new List<string>();
                List<string> oszlopNév = new List<string>();
                List<string> SzűrőNév = new List<string>();

                összesítNév.Add("Kiadás");
                összesítNév.Add("Forgalomban");
                összesítNév.Add("Tartalék");
                összesítNév.Add("Kocsiszíni");
                összesítNév.Add("Félreállítás");
                összesítNév.Add("Főjavítás");
                összesítNév.Add("Állomány");
                összesítNév.Add("Személyzethiány");
                összesítNév.Add("Tart.+Szem.hiány");

                sorNév.Add("AlTípus");
                sorNév.Add("Telephely");
                SzűrőNév.Add("Dátum");
                SzűrőNév.Add("Főkategória");

                MyX.Kimutatás_Fő(munkalap, balfelső, jobbalsó, kimutatás_munkalap, Kimutatás_cella, Kimutatás_név
                , összesítNév, sorNév, oszlopNév, SzűrőNév);
                MyX.SzövegIrány("Kimutatás", "9:9", 90);
                MyX.Oszlopszélesség("Kimutatás", "B:J");
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
            }
        }

        private void Telephelytábla_1()
        {
            try
            {
                munkalap = "Kocsiszín_1";
                MyX.Munkalap_aktív(munkalap);
                MyX.Link_beillesztés(munkalap, "A1", "Tartalom");
                Napok_kiírása();
                MunkaVHétvége();
                string szöveg = "Kocsiszíni kiadási darabszámok";
                MyX.Kiir(szöveg, MyF.Oszlopnév(3) + 3.ToString());

                List<Adat_kiegészítő_telephely> AdatokKiegTeleph = KézKiegTelep.Lista_Adatok();

                int oszlopmax = 0, eleje;
                Holtart.Be(hónapnap + 1);
                int pj = 3;

                for (int k = 0; k <= Kategórilista.CheckedItems.Count - 1; k++)
                {

                    foreach (Adat_kiegészítő_telephely rekordkieg in AdatokKiegTeleph)
                    {

                        List<Adat_FőKiadási_adatok> Adatok;
                        if (Délelőtt.Checked)
                            Adatok = (from a in AdatokKiad
                                      where a.Főkategória == Kategórilista.CheckedItems[k].ToStrTrim()
                                      && a.Telephely == rekordkieg.Telephelynév
                                      && a.Dátum >= hónapelsőnapja
                                      && a.Dátum <= hónaputolsónapja
                                      && a.Napszak == "de"
                                      orderby a.Főkategória, a.Telephely, a.Altípus, a.Dátum
                                      select a).ToList();
                        else
                            Adatok = (from a in AdatokKiad
                                      where a.Főkategória == Kategórilista.CheckedItems[k].ToStrTrim()
                                      && a.Telephely == rekordkieg.Telephelynév
                                      && a.Dátum >= hónapelsőnapja
                                      && a.Dátum <= hónaputolsónapja
                                      && a.Napszak == "du"
                                      orderby a.Főkategória, a.Telephely, a.Altípus, a.Dátum
                                      select a).ToList();

                        string előzőtípus = "";
                        if (Adatok.Count > 0)
                        {
                            MyX.Kiir(rekordkieg.Telephelynév, MyF.Oszlopnév(pj) + 4.ToString());

                            foreach (Adat_FőKiadási_adatok Elem in Adatok)
                            {
                                int sor = Elem.Dátum.Day;
                                long érték = Elem.Forgalomban;
                                if (előzőtípus.Trim() == "") előzőtípus = Elem.Altípus;
                                if (előzőtípus != Elem.Altípus)
                                {
                                    pj++;
                                    előzőtípus = Elem.Altípus;
                                }
                                MyX.Kiir(Elem.Altípus, MyF.Oszlopnév(pj) + 5.ToString());
                                MyX.Kiir("#SZÁME#" + érték.ToString(), MyF.Oszlopnév(pj) + (sor + 5).ToString());
                            }
                            pj += 1;
                            MyX.Kiir("Összesen", MyF.Oszlopnév(pj) + 5.ToString());
                            pj += 1;
                        }
                    }
                }
                MyX.Kiir("Összesen", MyF.Oszlopnév(pj) + 4.ToString());
                oszlopmax = pj;

                // Összesítések
                // A-B Oszlop formázása
                MyX.Oszlopszélesség(munkalap, "B:B", 2);
                MyX.Oszlopszélesség(munkalap, "A:A");
              //  MyX.Vastagkeret(munkalap, "A4:B" + (hónapnap + 5).ToString());
                MyX.Rácsoz(munkalap, "A4:B" + (hónapnap + 5).ToString());
                // állomány felirat
                MyX.Egyesít(munkalap, MyF.Oszlopnév(3) + "3:" + MyF.Oszlopnév(oszlopmax) + "3");
                MyX.Rácsoz(munkalap, MyF.Oszlopnév(3) + "3:" + MyF.Oszlopnév(oszlopmax) + "3");


                eleje = 3;
                for (int j = 3; j <= oszlopmax; j++)
                {
                    if (MyX.Beolvas(munkalap, MyF.Oszlopnév(j) + "5") == "Összesen")
                    {
                        for (int i = 1; i <= hónapnap; i++)
                        {
                            MyX.Kiir("#KÉPLET#=SUM(RC[-" + (j - eleje).ToString() + "]:RC[-1])", MyF.Oszlopnév(j) + (i + 5).ToString());
                            Holtart.Value = i;
                        }
                        MyX.Egyesít(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(j) + "4");
                       // MyX.Vastagkeret(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(j) + (hónapnap + 5).ToString());
                        MyX.Rácsoz(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(j) + (hónapnap + 5).ToString());
                        MyX.Rácsoz(munkalap, MyF.Oszlopnév(eleje) + "4:" + MyF.Oszlopnév(j) + "5");
                        MyX.Betű(munkalap, MyF.Oszlopnév(j) + "5:" + MyF.Oszlopnév(j) + (hónapnap + 5).ToString(), BeBetűV);
                        MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(eleje) + ":" + MyF.Oszlopnév(j));

                        eleje = j + 1;
                    }
                }
                // ha csak egy főkategória volt
                szöveg = "=";
                for (int j = 3; j <= oszlopmax; j++)
                {
                    if (MyX.Beolvas(munkalap, MyF.Oszlopnév(j) + "5") == "Összesen")
                    {
                        if (szöveg == "=")
                        {
                            szöveg += "SUM(RC[-" + (oszlopmax - j).ToString() + "]";
                        }
                        else
                        {
                            szöveg += ",RC[-" + (oszlopmax - j).ToString() + "]";
                        }
                    }
                }
                szöveg += ")";
                for (int i = 1; i <= hónapnap; i++)
                {
                    MyX.Kiir("#KÉPLET#" + szöveg, MyF.Oszlopnév(oszlopmax) + (i + 5).ToString());
                    Holtart.Lép();
                }
                // megformázzuk
               // MyX.Vastagkeret(munkalap, "c4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                MyX.Rácsoz(munkalap, "c4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                MyX.Rácsoz(munkalap, "c4:" + MyF.Oszlopnév(oszlopmax) + "5");
               // MyX.Vastagkeret(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                MyX.Betű(munkalap, MyF.Oszlopnév(oszlopmax) + "4:" + MyF.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString(), BeBetűVD);
                // Oszlop szélesség beállítás
                MyX.Oszlopszélesség(munkalap, "C:" + MyF.Oszlopnév(oszlopmax));

                // Alsó összesítés és átlag
                Havi_Összesítő_rész(oszlopmax);
                MyX.Aktív_Cella(munkalap, "A1");
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Napok_kiírása()
        {
            try
            {
                for (int ki = 1; ki <= hónapnap; ki++)
                {
                    DateTime ideig = new DateTime(Dátum.Value.Year, Dátum.Value.Month, ki);
                    MyX.Kiir(ideig.ToString("yyyy.MM.dd"), "a" + (ki + 5).ToString());
                    MyX.Kiir("#SZÁME#3", "b" + (ki + 5).ToString());
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MunkaVHétvége()
        {
            try
            {

                // Meg kell fordítani, hogy a hónapnap megfelelően olvassuk be az exceléből az adatokat LINQ-val lehet szűrni.
                //Nem kell a forte szűr adat lehet a teljes adathalmaz.
                for (int i = 0; i <= hónapnap; i++)
                {
                    DateTime melyiknap = MyX.Beolvas(munkalap, "a" + (i + 5)).ToÉrt_DaTeTime();
                    Adat_Forte_Kiadási_Adatok rekord = (from a in AdatokFortekiad
                                                        where a.Dátum == melyiknap
                                                        select a).FirstOrDefault();
                    if (rekord != null)
                    {
                        if (rekord.Munkanap == 0)
                            MyX.Kiir("#SZÁME#0", "b" + (i + 5).ToString());
                        else
                            MyX.Kiir("#SZÁME#1", "b" + (i + 5).ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Havi_Összesítő_rész(int oszlopmax_)
        {
            try
            {
                string szöveg, szöveg1;
                // Alsó összesítés és átlag
                MyX.Kiir("Hétköznap", "a40");
                MyX.Kiir("Összesen", "a41");
                MyX.Kiir("Átlag", "a42");
                MyX.Kiir("Hétvége", "a44");
                MyX.Kiir("Összesen", "a45");
                MyX.Kiir("Átlag", "a46");
                MyX.Kiir("Havi", "a48");
                MyX.Kiir("Összesen", "a49");
                MyX.Kiir("Átlag", "a50");

                // megszámoljuk hány munkanap van
                int hétköznapdb = 0;
                int hétvégedb = 0;

                for (int i = 6; i <= hónapnap + 5; i++)
                {
                    if (!int.TryParse(MyX.Beolvas(munkalap, "b" + i.ToString()), out int kód))
                        kód = 0;

                    if (kód == 0)
                    {
                        hétköznapdb += 1;
                    }
                    else
                    {
                        hétvégedb += 1;
                        MyX.Háttérszín(munkalap, "a" + i.ToString() + ":" + MyF.Oszlopnév(oszlopmax_) + i.ToString(), Color.GreenYellow);
                    }
                }
                szöveg = "=";
                szöveg1 = "=";

                for (int i = 6; i <= hónapnap + 5; i++)
                {
                    if (!int.TryParse(MyX.Beolvas(munkalap, "b" + i.ToString()), out int kód))
                        kód = 0;

                    if (kód == 0)
                    {
                        if (szöveg == "=")
                        {
                            szöveg += "R[-" + (41 - i).ToString() + "]C";
                        }
                        else
                        {
                            szöveg += "+R[-" + (41 - i).ToString() + "]C";
                        }
                    }
                    else if (szöveg1 == "=")
                    {
                        szöveg1 += "R[-" + (45 - i).ToString() + "]C";
                    }
                    else
                    {
                        szöveg1 += "+R[-" + (45 - i).ToString() + "]C";
                    }
                    Holtart.Value = i - 5;
                }

                for (int j = 3; j <= oszlopmax_; j++)
                {
                    // hétköznap
                    MyX.Kiir("#SZÁME#" + hétköznapdb.ToString(), MyF.Oszlopnév(j) + "40");
                    MyX.Kiir("#KÉPLET#" + szöveg, MyF.Oszlopnév(j) + "41");
                    MyX.Kiir("#KÉPLET#=R[-1]C/R[-2]C", MyF.Oszlopnév(j) + "42");

                    // hétvége
                    MyX.Kiir("#SZÁME#" + hétvégedb.ToString(), MyF.Oszlopnév(j) + "44");
                    MyX.Kiir("#KÉPLET#" + szöveg1, MyF.Oszlopnév(j) + "45");
                    MyX.Kiir("#KÉPLET#=R[-1]C/R[-2]C", MyF.Oszlopnév(j) + "46");

                    // összesen
                    MyX.Kiir("#SZÁME#" + (hétvégedb + hétköznapdb).ToString(), MyF.Oszlopnév(j) + "48");
                    MyX.Kiir("#KÉPLET#=SUM(R[-43]C:R[-13]C)", MyF.Oszlopnév(j) + "49");
                    MyX.Kiir("#KÉPLET#=R[-1]C/R[-2]C", MyF.Oszlopnév(j) + "50");

                }
                MyX.Háttérszín(munkalap, "a44:" + MyF.Oszlopnév(oszlopmax_) + "46", Color.GreenYellow);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region Listák
        private void Listák_Feltöltés()
        {
            AdatokSzem = KézSzem.Lista_adatok(Dátum.Value.Year);
            AdatokTípCsere = KézTípCsere.Lista_adatok(Dátum.Value.Year);
            AdatokKiad = KézKiad.Lista_adatok(Dátum.Value.Year);
            Forte_Lista_Feltöltése();
            Adatokkiegtípusal = Kézkiegtípusal.Lista_Adatok();

            hónapnap = MyF.Hónap_hossza(Dátum.Value);
            hónaputolsónapja = MyF.Hónap_utolsónapja(Dátum.Value);
            hónapelsőnapja = MyF.Hónap_elsőnapja(Dátum.Value);
            ElőzőDátum = Dátum.Value;
        }

        private void Forte_Lista_Feltöltése()
        {
            try
            {
                AdatokFortekiad.Clear();
                AdatokFortekiad = KézForteKiad.Lista_Adatok(Dátum.Value.Year);
                if (AdatokFortekiad == null || AdatokFortekiad.Count == 0) throw new HibásBevittAdat($"Nincs {Dátum.Value:yyyy} évnek megfelelő adat.");
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

        private void Dátum_ValueChanged(object sender, EventArgs e)
        {
            if (ElőzőDátum.Year != Dátum.Value.Year) Listák_Feltöltés();

            hónapnap = MyF.Hónap_hossza(Dátum.Value);
            hónaputolsónapja = MyF.Hónap_utolsónapja(Dátum.Value);
            hónapelsőnapja = MyF.Hónap_elsőnapja(Dátum.Value);
            ElőzőDátum = Dátum.Value;
        }

    }
}