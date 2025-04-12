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
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{

    public partial class Ablak_Fő_Egyesített
    {
        readonly Kezelő_FőSzemélyzet_Adatok KézSzem = new Kezelő_FőSzemélyzet_Adatok();
        readonly Kezelő_FőTípuscsere_Adatok KézTípCsere = new Kezelő_FőTípuscsere_Adatok();
        readonly Kezelő_FőKiadási_adatok KézKiad = new Kezelő_FőKiadási_adatok();
        readonly Kezelő_Forte_Kiadási_Adatok KézForteKiad = new Kezelő_Forte_Kiadási_Adatok();
        readonly Kezelő_Kiegészítő_Típusaltípustábla Kézkiegtípusal = new Kezelő_Kiegészítő_Típusaltípustábla();

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


        public Ablak_Fő_Egyesített()
        {
            InitializeComponent();
        }




        private void Ablak_Fő_Egyesített_Load(object sender, EventArgs e)
        {
            try
            {
                Dátum.Value = DateTime.Today;
                ElőzőDátum = Dátum.Value;

                string hely = $@"{Application.StartupPath}\főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_kiadási_adatok.mdb";
                if (!File.Exists(hely)) Adatbázis_Létrehozás.Kiadásiösszesítőfőmérnöktábla(hely);

                hely = $@"{Application.StartupPath}\főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_fortekiadási_adatok.mdb";
                if (!File.Exists(hely)) Adatbázis_Létrehozás.Fortekiadásifőmtábla(hely);

                Jogosultságkiosztás();

                Kategóriák();
                Listák_Feltöltés();
            }

            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #region alap
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

                Kezelő_Kiegészítő_Főkategóriatábla Kéz = new Kezelő_Kiegészítő_Főkategóriatábla();
                List<Adat_Kiegészítő_Főkategóriatábla> Adatok = Kéz.Lista_Adatok();



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
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Főmérnökség_napi_lekérdezés.html";
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
        #endregion


        #region Napi adatok
        private void KiadaNapi_Click(object sender, EventArgs e)
        {
            try
            {         // megnézzük, hogy van-e kijelölve valami
                if (Kategórilista.SelectedItems.Count < 1) throw new HibásBevittAdat("Nincs kijelölve egy kategória sem.");

                string hely = $@"{Application.StartupPath}\főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_kiadási_adatok.mdb";
                if (!File.Exists(hely)) throw new HibásBevittAdat($"Nincs {Dátum.Value.Year} évnek megfelelő adat.");

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
            {         // megnézzük, hogy van-e kijelölve valami
                if (Kategórilista.SelectedItems.Count < 1) throw new HibásBevittAdat("Nincs kijelölve egy kategória sem.");

                string hely = $@"{Application.StartupPath}\főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_kiadási_adatok.mdb";
                if (!File.Exists(hely)) throw new HibásBevittAdat($"Nincs {Dátum.Value.Year} évnek megfelelő adat.");

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
                string hely = $@"{Application.StartupPath}\főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_személyzet_adatok.mdb";
                if (!File.Exists(hely)) throw new HibásBevittAdat($"Nincs {Dátum.Value:yyyy} évnek megfelelő adat.");

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
                string hely = $@"{Application.StartupPath}\főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_személyzet_adatok.mdb";
                if (!File.Exists(hely)) throw new HibásBevittAdat($"Nincs {Dátum.Value:yyyy} évnek megfelelő adat.");

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
                string hely = $@"{Application.StartupPath}\főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_típuscsere_adatok.mdb";
                if (!File.Exists(hely)) throw new HibásBevittAdat($"Nincs {Dátum.Value:yyyy} évnek megfelelő adat.");

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
                string hely = $@"{Application.StartupPath}\főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_típuscsere_adatok.mdb";
                if (!File.Exists(hely)) throw new HibásBevittAdat($"Nincs {Dátum.Value:yyyy} évnek megfelelő adat.");

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
                    FileName = "Főmérnökségi_egyesített_" + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMdd"),
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

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_fortekiadási_adatok.mdb";
                if (!File.Exists(hely)) throw new HibásBevittAdat($"Nincs {Dátum.Value:yyyy} évnek megfelelő adat.");



                Holtart_Be(hónapnap + 1);


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
                    Holtart_Lép();
                }


                Holtart_Be(hónapnap + 1);
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
                    Holtart_Lép();
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
                Holtart_Be(Elemek.Count + 1);
                foreach (string Elem in Elemek)
                {
                    i += 1;
                    Tábla.Columns[i].HeaderText = Elem;
                    Tábla.Columns[i].Width = 100;
                    Holtart_Lép();
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
                Holtart_Be(Tábla.ColumnCount + 1);
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
                    Holtart_Lép();
                }

                Tábla.ColumnCount += 1;
                Tábla.Columns[Tábla.ColumnCount - 1].HeaderText = "Összesen:";
                Tábla.Columns[Tábla.ColumnCount - 1].Width = 100;

                for (int ii = 1; ii <= hónapnap; ii++)
                    Tábla.Rows[ii - 1].Cells[Tábla.ColumnCount - 1].Value = összeg[ii];

                Tábla.Visible = true;
                Holtart_Ki();

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
                string hely, jelszó, szöveg;
                // létrehozzuk az excel táblát
                hely = "Kocsik_" + Dátum.Value.ToString("yyyyMMdd") + "_";
                if (Délelőtt.Checked)
                    hely += "de";
                else
                    hely += "du";
                hely += "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Járműpark tételes ellenőrzés",
                    FileName = hely,
                    Filter = "Excel |*.xlsx"
                };
                string fájlexc;
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyE.ExcelLétrehozás();

                Holtart_Be(50);

                // szöveg formátumban írjuk ki a psz-okat

                MyE.Betű("A:A", "", "@");

                // kiírjuk a főmérnökségi pályaszámokat
                hely = Application.StartupPath + @"\főmérnökség\adatok\villamos.mdb";
                jelszó = "pozsgaii";
                szöveg = "SELECT * FROM állománytábla where törölt=0 order by azonosító";

                int sor = 1;
                MyE.Kiir("Pályaszám", "a1");
                MyE.Kiir("Főmérnökségi Típus", "b1");
                MyE.Kiir("Jármű Típus", "c1");


                Kezelő_Jármű KézJármű = new Kezelő_Jármű();
                List<Adat_Jármű> AdatokJármű = KézJármű.Lista_Adatok(hely, jelszó, szöveg);
                foreach (Adat_Jármű rekord in AdatokJármű)
                {
                    sor += 1;
                    MyE.Kiir(rekord.Azonosító, "a" + sor);
                    MyE.Kiir(rekord.Valóstípus2, "b" + sor);
                    MyE.Kiir(rekord.Valóstípus, "c" + sor);
                    Holtart_Lép();
                }

                int sormax = sor;

                Kezelő_kiegészítő_telephely KézKiegTelep = new Kezelő_kiegészítő_telephely();
                List<Adat_kiegészítő_telephely> AdatokKiegTelep = KézKiegTelep.Lista_adatok();

                int oszlop = 3;

                string helytelep;

                foreach (Adat_kiegészítő_telephely rekord in AdatokKiegTelep)
                {
                    oszlop += 1;

                    MyE.Kiir(rekord.Telephelykönyvtár.ToStrTrim(), MyE.Oszlopnév(oszlop) + "1");
                    // megnyitjuk a telephelyi adatokat a régi helyen
                    helytelep = $@"{Application.StartupPath}\{rekord.Telephelykönyvtár}\adatok\főkönyv\nap\{Dátum.Value:yyyyMMdd}";
                    if (Délelőtt.Checked)
                        helytelep += "de";
                    else
                        helytelep += "du";
                    helytelep += "nap.mdb";
                    // ha létezik a telephelyi tábla akkor kiírjuk
                    if (!File.Exists(helytelep))
                    {
                        // az új helyen
                        helytelep = $@"{Application.StartupPath}\{rekord.Telephelykönyvtár}\adatok\főkönyv\{Dátum.Value:yyyy}\nap\{Dátum.Value:yyyyMMdd}";
                        if (Délelőtt.Checked)
                            helytelep += "de";
                        else
                            helytelep += "du";
                        helytelep += "nap.mdb";
                    }

                    string jelszótelep = "lilaakác";
                    szöveg = "SELECT * FROM adattábla ORDER BY azonosító";
                    if (File.Exists(helytelep))
                    {
                        Kezelő_Főkönyv_Nap KézFőNap = new Kezelő_Főkönyv_Nap();
                        List<Adat_Főkönyv_Nap> AdatokFőNap = KézFőNap.Lista_adatok(helytelep, jelszótelep, szöveg);

                        int i = 2;

                        foreach (Adat_Főkönyv_Nap elem in AdatokFőNap)
                        {
                            while (String.Compare(MyE.Beolvas("A" + i.ToString()).Trim(), elem.Azonosító) < 0)
                            {
                                i += 1;
                                string valami = MyE.Beolvas("A" + i.ToString()).Trim();
                                if (valami == "_") break;
                            }

                            if (elem.Azonosító == MyE.Beolvas("a" + i).Trim())
                            {
                                MyE.Kiir("1", MyE.Oszlopnév(oszlop) + i.ToString());
                            }
                            if (sormax == i)
                                break;
                            Holtart_Lép();
                            i++;
                        }
                    }
                    Holtart_Lép();
                }
                oszlop += 1;
                MyE.Kiir("Összesen", MyE.Oszlopnév(oszlop) + "1");

                for (int i = 2; i <= sormax; i++)
                    MyE.Kiir("=SUM(RC[-" + (oszlop - 3).ToString() + "]:RC[-1])", MyE.Oszlopnév(oszlop) + i.ToString());
                // szűrés
                MyE.Szűrés("Munka1", 1, oszlop, 1);

                // rácsozás
                MyE.Rácsoz("a1:" + MyE.Oszlopnév(oszlop) + sormax.ToString());
                MyE.Vastagkeret("a1:" + MyE.Oszlopnév(oszlop) + "1");
                MyE.Vastagkeret("a1:" + MyE.Oszlopnév(oszlop) + sormax.ToString());
                // oszlop szélesség
                MyE.Oszlopszélesség("Munka1", "A:" + MyE.Oszlopnév(oszlop));

                MyE.Aktív_Cella("Munka1", "A1");

                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();

                Holtart_Ki();
                MyE.Megnyitás(fájlexc);
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
                    FileName = "Nóta_" + Dátum.Value.ToString("yyyy") + "_év_" + Dátum.Value.ToString("MM") + "_hó_" + DateTime.Now.ToString("yyyyMMddHHmmss"),
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

                MyE.ExcelLétrehozás();

                // ****************************************************
                // elkészítjük a lapokat
                // ****************************************************
                MyE.Munkalap_átnevezés("Munka1", "Tartalom");

                for (int i = 1; i <= 15; i++)
                {
                    MyE.Új_munkalap(Cím[i]);
                }
                // ****************************************************
                // Elkészítjük a tartalom jegyzéket
                // ****************************************************
                MyE.Munkalap_aktív("Tartalom");
                MyE.Kiir("Munkalapfül", "a1");
                MyE.Kiir("Leírás", "b1");

                for (int i = 1; i <= 15; i++)
                {
                    MyE.Link_beillesztés("Tartalom", "a" + (i + 1).ToString(), Cím[i]);
                    MyE.Kiir(Cím[i], "b" + (i + 1).ToString());
                }
                MyE.Oszlopszélesség("Tartalom", "A:B");

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


                MyE.Munkalap_aktív("Tartalom");
                MyE.Aktív_Cella("Tartalom", "A1");

                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();

                Holtartfő.Visible = false;
                Holtart_Ki();
                MyE.Megnyitás(fájlexc);

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
                MyE.Munkalap_aktív("állomány 1");
                MyE.Link_beillesztés("állomány 1", "A1", "Tartalom");
                string MunkaLap = "állomány 1";
                Napok_kiírása();
                MunkaVHétvége();

                // ****************************************************
                // Elkészítjük a táblázatot
                // ****************************************************
                string helykieg = Application.StartupPath + @"\főmérnökség\adatok\Kiegészítő.mdb";
                string jelszókieg = "Mocó";
                string szöveg = "SELECT * FROM szolgálattábla order by sorszám";
                Kezelő_Kiegészítő_Szolgálat KézKiegSzolgálat = new Kezelő_Kiegészítő_Szolgálat();
                List<Adat_Kiegészítő_Szolgálat> AdatokKiegSzolgálat = KézKiegSzolgálat.Lista_Adatok(helykieg, jelszókieg, szöveg);

                szöveg = Délelőtt.Checked ? "Reggeli " : "Délutáni ";
                szöveg += "Állományi darabszámok";
                int jj = 3;
                MyE.Kiir(szöveg, MyE.Oszlopnév(jj) + 3.ToString());

                Holtart_Be(hónapnap + 1);

                bool volt = false;
                foreach (Adat_Kiegészítő_Szolgálat rekordkieg in AdatokKiegSzolgálat)
                {

                    MyE.Kiir(rekordkieg.Szolgálatnév, MyE.Oszlopnév(jj) + 4.ToString());

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

                            MyE.Kiir(Kategórilista.Items[k].ToStrTrim(), MyE.Oszlopnév(jj) + 5.ToString());
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
                                MyE.Kiir(érték.ToString(), MyE.Oszlopnév(jj) + (ki + 5).ToString());
                                volt = true;
                            }
                            else
                            {
                                MyE.Kiir("0", MyE.Oszlopnév(jj) + (ki + 5).ToString());
                            }

                        }
                        jj += 1;

                        Holtart_Lép();
                    }
                    if (volt == true)
                    {
                        MyE.Kiir("Összesen", MyE.Oszlopnév(jj) + 5.ToString());
                        jj += 1;
                    }
                    volt = false;
                }
                MyE.Kiir("Összesen", MyE.Oszlopnév(jj) + 4.ToString());
                int oszlopmax = jj;

                // Összesítések
                // A-B Oszlop formázása
                MyE.Oszlopszélesség(MunkaLap, "B:B", 2);
                MyE.Oszlopszélesség(MunkaLap, "A:A");

                MyE.Rácsoz("A4:B" + (hónapnap + 5).ToString());
                MyE.Vastagkeret("A4:B" + (hónapnap + 5).ToString());
                // állomány felirat
                MyE.Egyesít(MunkaLap, MyE.Oszlopnév(3) + "3:" + MyE.Oszlopnév(oszlopmax) + "3");
                if (volt == true)
                {
                    // ha csak egy főkategória volt
                    for (int vi = 1; vi <= hónapnap; vi++)
                    {
                        MyE.Kiir("=SUM(RC[-" + (oszlopmax - 3).ToString() + "]:RC[-1])", MyE.Oszlopnév(oszlopmax) + (vi + 5).ToString());
                        Holtart.Value = vi;
                    }
                    // megformázzuk
                    MyE.Rácsoz("c4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyE.Vastagkeret("c4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyE.Vastagkeret("c4:" + MyE.Oszlopnév(oszlopmax) + "5");
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyE.Betű(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString(), false, true, true);
                    // Oszlop szélesség beállítás
                    MyE.Oszlopszélesség(MunkaLap, "C:" + MyE.Oszlopnév(oszlopmax));

                }

                else
                {
                    // ha több kategória volt

                    // részösszegek
                    int eleje = 3;
                    for (int wj = 3; wj <= oszlopmax; wj++)
                    {
                        if (MyE.Beolvas(MyE.Oszlopnév(wj) + "5") == "Összesen")
                        {

                            for (int wi = 1; wi <= hónapnap; wi++)
                            {
                                MyE.Kiir("=SUM(RC[-" + (wj - eleje).ToString() + "]:RC[-1])", MyE.Oszlopnév(wj) + (wi + 5).ToString());
                                Holtart.Value = wi;
                            }
                            MyE.Egyesít(MunkaLap, MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(wj) + "4");
                            MyE.Rácsoz(MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(wj) + (hónapnap + 5).ToString());
                            MyE.Vastagkeret(MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(wj) + (hónapnap + 5).ToString());
                            MyE.Vastagkeret(MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(wj) + "5");
                            MyE.Betű(MyE.Oszlopnév(wj) + "5:" + MyE.Oszlopnév(wj) + (hónapnap + 5).ToString(), false, false, true);
                            MyE.Oszlopszélesség(MunkaLap, MyE.Oszlopnév(eleje) + ":" + MyE.Oszlopnév(wj));

                            eleje = wj + 1;
                        }
                    }
                    // megformázzuk
                    // Összesítő rész formázása
                    MyE.Rácsoz(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + "5");
                    MyE.Betű(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString(), false, true, true);
                    szöveg = "=";

                    for (int j = 3; j <= oszlopmax; j++)
                    {
                        if (MyE.Beolvas(MyE.Oszlopnév(j) + "5") == "Összesen")
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
                        MyE.Kiir(szöveg, MyE.Oszlopnév(oszlopmax) + (i + 5).ToString());
                        Holtart.Value = i;
                    }
                }
                // Alsó összesítés és átlag
                Havi_Összesítő_rész(oszlopmax);
                MyE.Aktív_Cella(MunkaLap, "A1");
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
                MyE.Munkalap_aktív("állomány 2");
                MyE.Link_beillesztés("állomány 2", "A1", "Tartalom");
                string MunkaLap = "állomány 2";
                Napok_kiírása();
                MunkaVHétvége();

                string szöveg = Délelőtt.Checked ? "Reggeli " : "Délutáni ";
                szöveg += "Állományi darabszámok";
                MyE.Kiir(szöveg, MyE.Oszlopnév(3) + 3.ToString());

                // '****************************************************
                // 'Elkészítjük a táblázatot
                // '****************************************************
                string helykieg = $@"{Application.StartupPath}\főmérnökség\adatok\Kiegészítő.mdb";
                string jelszókieg = "Mocó";
                szöveg = "SELECT * FROM típusaltípustábla";
                Kezelő_Kiegészítő_Típusaltípustábla KézKiegTípusal = new Kezelő_Kiegészítő_Típusaltípustábla();
                List<Adat_Kiegészítő_Típusaltípustábla> AdatokKiegTípusal = KézKiegTípusal.Lista_Adatok(helykieg, jelszókieg, szöveg);

                bool volt = false;
                int pj = 3;
                int oszlopmax;
                Holtart_Be(hónapnap + 1);

                string előzőtípus = "";

                for (int k = 0; k <= Kategórilista.CheckedItems.Count - 1; k++)
                {

                    if (előzőtípus.Trim() == "")
                    {
                        MyE.Kiir(Kategórilista.CheckedItems[k].ToStrTrim(), MyE.Oszlopnév(pj) + 4.ToString());
                        előzőtípus = Kategórilista.CheckedItems[k].ToStrTrim();
                    }
                    else
                    {
                        MyE.Kiir("Összesen", MyE.Oszlopnév(pj) + 5.ToString());
                        pj += 1;
                        MyE.Kiir(Kategórilista.CheckedItems[k].ToStrTrim(), MyE.Oszlopnév(pj) + 4.ToString());
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

                            MyE.Kiir(rekordkieg.AlTípus, MyE.Oszlopnév(pj) + 5.ToString());
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
                                MyE.Kiir(érték.ToString(), MyE.Oszlopnév(pj) + (i + 5).ToString());
                            }
                            else
                            {
                                MyE.Kiir("0", MyE.Oszlopnév(pj) + (i + 5).ToString());
                            }
                        }
                        oszlopmax = pj;
                        pj += 1;
                        Holtart_Lép();
                    }
                }

                if (volt != true)
                {
                    MyE.Kiir("Összesen", MyE.Oszlopnév(pj) + 5.ToString());
                    pj += 1;
                }
                MyE.Kiir("Összesen", MyE.Oszlopnév(pj) + 4.ToString());
                oszlopmax = pj;
                // Összesítések
                // A-B Oszlop formázása
                MyE.Oszlopszélesség(MunkaLap, "B:B", 2);
                MyE.Oszlopszélesség(MunkaLap, "A:A");

                MyE.Rácsoz("A4:B" + (hónapnap + 5).ToString());
                MyE.Vastagkeret("A4:B" + (hónapnap + 5).ToString());

                int eleje;
                if (volt == true)
                {
                    // ha csak egy főkategória volt

                    for (int i = 1; i <= hónapnap; i++)
                    {
                        MyE.Kiir("=SUM(RC[-" + (oszlopmax - 3).ToString() + "]:RC[-1])", MyE.Oszlopnév(oszlopmax) + (i + 5).ToString());
                        Holtart.Value = i;
                    }
                    // megformázzuk
                    MyE.Egyesít(MunkaLap, "c3:" + MyE.Oszlopnév(oszlopmax) + "3");
                    MyE.Rácsoz("c4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyE.Vastagkeret("c4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyE.Vastagkeret("c4:" + MyE.Oszlopnév(oszlopmax) + "5");
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyE.Betű(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString(), false, true, true);
                    // Oszlop szélesség beállítás
                    MyE.Oszlopszélesség(MunkaLap, "C:" + MyE.Oszlopnév(oszlopmax));
                }

                else
                {
                    // ha több kategória volt
                    // részösszegek
                    eleje = 3;
                    for (int j = 3; j <= oszlopmax; j++)
                    {
                        if (MyE.Beolvas(MyE.Oszlopnév(j) + "5") == "Összesen")
                        {
                            for (int i = 1; i <= hónapnap; i++)
                            {
                                MyE.Kiir("=SUM(RC[-" + (j - eleje).ToString() + "]:RC[-1])", MyE.Oszlopnév(j) + (i + 5).ToString());
                                Holtart.Value = i;
                            }
                            MyE.Egyesít(MunkaLap, MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(j) + "4");
                            MyE.Rácsoz(MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(j) + (hónapnap + 5).ToString());
                            MyE.Vastagkeret(MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(j) + (hónapnap + 5).ToString());
                            MyE.Vastagkeret(MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(j) + "5");
                            MyE.Betű(MyE.Oszlopnév(j) + "5:" + MyE.Oszlopnév(j) + (hónapnap + 5).ToString(), false, false, true);
                            MyE.Oszlopszélesség(MunkaLap, MyE.Oszlopnév(eleje) + ":" + MyE.Oszlopnév(j));
                            eleje = j + 1;
                        }
                    }


                    // megformázzuk
                    // állomány felirat
                    MyE.Egyesít(MunkaLap, MyE.Oszlopnév(3) + "3:" + MyE.Oszlopnév(oszlopmax) + "3");

                    // Összesítő rész formázása
                    MyE.Rácsoz(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + "5");
                    MyE.Betű(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString(), false, true, true);

                    szöveg = "";
                    szöveg = "=";

                    for (int j = 3; j <= oszlopmax; j++)
                    {
                        if (MyE.Beolvas(MyE.Oszlopnév(j) + "5") == "Összesen")
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
                        MyE.Kiir(szöveg, MyE.Oszlopnév(oszlopmax) + (i + 5).ToString());
                        Holtart.Value = i;
                    }
                }
                Havi_Összesítő_rész(oszlopmax);
                MyE.Aktív_Cella(MunkaLap, "A1");
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
                string MunkaLap = "állomány 3";
                MyE.Munkalap_aktív(MunkaLap);
                MyE.Link_beillesztés(MunkaLap, "A1", "Tartalom");

                Napok_kiírása();
                MunkaVHétvége();

                string szöveg = Délelőtt.Checked ? "Reggeli " : "Délutáni ";
                szöveg += "Állományi darabszámok";
                MyE.Kiir(szöveg, MyE.Oszlopnév(3) + 2.ToString());


                // ****************************************************
                // Elkészítjük a táblázatot
                // ****************************************************
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő.mdb";
                string jelszó = "Mocó";
                szöveg = "SELECT * FROM típusaltípustábla";
                Kezelő_Kiegészítő_Típusaltípustábla KézKiegTípusal = new Kezelő_Kiegészítő_Típusaltípustábla();
                List<Adat_Kiegészítő_Típusaltípustábla> AdatokKiegTípusal = KézKiegTípusal.Lista_Adatok(hely, jelszó, szöveg);

                szöveg = "SELECT * FROM szolgálattábla order by sorszám";
                Kezelő_Kiegészítő_Szolgálat KézKiegSzolgtábl = new Kezelő_Kiegészítő_Szolgálat();
                List<Adat_Kiegészítő_Szolgálat> AdatokKiegSzolgtábl = KézKiegSzolgtábl.Lista_Adatok(hely, jelszó, szöveg);

                int pj = 3;
                int oszlopmax;
                Holtart_Be(hónapnap + 1);

                string előzőtípus = "";
                string előzőaltípus = "";


                for (int k = 0; k < Kategórilista.CheckedItems.Count; k++)
                {
                    if (előzőtípus.Trim() == "")
                    {
                        MunkaLap = "állomány 3"; MyE.Munkalap_aktív(MunkaLap);
                        MyE.Kiir(Kategórilista.CheckedItems[k].ToStrTrim(), MyE.Oszlopnév(pj) + 3.ToString());

                        MunkaLap = "Forgalmi 3"; MyE.Munkalap_aktív(MunkaLap);
                        MyE.Kiir(Kategórilista.CheckedItems[k].ToStrTrim(), MyE.Oszlopnév(pj) + 3.ToString());

                        MunkaLap = "Üzemképes 3"; MyE.Munkalap_aktív(MunkaLap);
                        MyE.Kiir(Kategórilista.CheckedItems[k].ToStrTrim(), MyE.Oszlopnév(pj) + 3.ToString());
                        előzőtípus = Kategórilista.CheckedItems[k].ToStrTrim();
                    }
                    else
                    {
                        MunkaLap = "állomány 3"; MyE.Munkalap_aktív(MunkaLap);
                        MyE.Kiir("Összesen", MyE.Oszlopnév(pj) + 5.ToString());

                        MunkaLap = "Forgalmi 3"; MyE.Munkalap_aktív(MunkaLap);
                        MyE.Kiir("Összesen", MyE.Oszlopnév(pj) + 5.ToString());

                        MunkaLap = "Üzemképes 3"; MyE.Munkalap_aktív(MunkaLap);
                        MyE.Kiir("Összesen", MyE.Oszlopnév(pj) + 5.ToString());
                        pj += 1;

                        MunkaLap = "állomány 3"; MyE.Munkalap_aktív(MunkaLap);
                        MyE.Kiir("Összesen", MyE.Oszlopnév(pj) + 4.ToString());

                        MunkaLap = "Forgalmi 3"; MyE.Munkalap_aktív(MunkaLap);
                        MyE.Kiir("Összesen", MyE.Oszlopnév(pj) + 4.ToString());

                        MunkaLap = "Üzemképes 3"; MyE.Munkalap_aktív(MunkaLap);
                        MyE.Kiir("Összesen", MyE.Oszlopnév(pj) + 4.ToString());
                        pj += 1;

                        MunkaLap = "állomány 3"; MyE.Munkalap_aktív(MunkaLap);
                        MyE.Kiir(Kategórilista.CheckedItems[k].ToStrTrim(), MyE.Oszlopnév(pj) + 3.ToString());

                        MunkaLap = "Forgalmi 3"; MyE.Munkalap_aktív(MunkaLap);
                        MyE.Kiir(Kategórilista.CheckedItems[k].ToStrTrim(), MyE.Oszlopnév(pj) + 3.ToString());

                        MunkaLap = "Üzemképes 3"; MyE.Munkalap_aktív(MunkaLap);
                        MyE.Kiir(Kategórilista.CheckedItems[k].ToStrTrim(), MyE.Oszlopnév(pj) + 3.ToString());
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
                            MunkaLap = "állomány 3"; MyE.Munkalap_aktív(MunkaLap);
                            MyE.Kiir("Összesen", MyE.Oszlopnév(pj) + 5.ToString());

                            MunkaLap = "Forgalmi 3"; MyE.Munkalap_aktív(MunkaLap);
                            MyE.Kiir("Összesen", MyE.Oszlopnév(pj) + 5.ToString());

                            MunkaLap = "Üzemképes 3"; MyE.Munkalap_aktív(MunkaLap);
                            MyE.Kiir("Összesen", MyE.Oszlopnév(pj) + 5.ToString());
                            pj += 1;
                            előzőaltípus = elem.AlTípus;
                        }

                        MunkaLap = "állomány 3"; MyE.Munkalap_aktív(MunkaLap);
                        MyE.Kiir(elem.AlTípus, MyE.Oszlopnév(pj) + 4.ToString());

                        MunkaLap = "Forgalmi 3"; MyE.Munkalap_aktív(MunkaLap);
                        MyE.Kiir(elem.AlTípus, MyE.Oszlopnév(pj) + 4.ToString());

                        MunkaLap = "Üzemképes 3"; MyE.Munkalap_aktív(MunkaLap);
                        MyE.Kiir(elem.AlTípus, MyE.Oszlopnév(pj) + 4.ToString());

                        foreach (Adat_Kiegészítő_Szolgálat rekordkieg1 in AdatokKiegSzolgtábl)
                        {
                            MunkaLap = "állomány 3"; MyE.Munkalap_aktív(MunkaLap);
                            MyE.Kiir(rekordkieg1.Szolgálatnév, MyE.Oszlopnév(pj) + 5.ToString());

                            MunkaLap = "Forgalmi 3"; MyE.Munkalap_aktív(MunkaLap);
                            MyE.Kiir(rekordkieg1.Szolgálatnév, MyE.Oszlopnév(pj) + 5.ToString());

                            MunkaLap = "Üzemképes 3"; MyE.Munkalap_aktív(MunkaLap);
                            MyE.Kiir(rekordkieg1.Szolgálatnév, MyE.Oszlopnév(pj) + 5.ToString());

                            if (Délelőtt.Checked)
                                Elemek = (from a in AdatokKiad
                                          where a.Napszak == "de"
                                          && a.Főkategória == Kategórilista.CheckedItems[k].ToStrTrim()
                                          && a.Altípus == elem.AlTípus
                                          && a.Szolgálat == MyE.Beolvas(MyE.Oszlopnév(pj) + 5.ToString())
                                          && a.Dátum >= hónapelsőnapja
                                          && a.Dátum <= hónaputolsónapja
                                          orderby a.Dátum
                                          select a).ToList();
                            else
                                Elemek = (from a in AdatokKiad
                                          where a.Napszak == "du"
                                          && a.Főkategória == Kategórilista.CheckedItems[k].ToStrTrim()
                                          && a.Altípus == elem.AlTípus
                                          && a.Szolgálat == MyE.Beolvas(MyE.Oszlopnév(pj) + 5.ToString())
                                          && a.Dátum >= hónapelsőnapja
                                          && a.Dátum <= hónaputolsónapja
                                          orderby a.Dátum
                                          select a).ToList();

                            if (Elemek.Count > 1)
                            {
                                DateTime ElőzőDátum = new DateTime(1900, 1, 1);
                                Holtart_Be(hónapnap + 1);

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

                                        MunkaLap = "állomány 3"; MyE.Munkalap_aktív(MunkaLap);
                                        MyE.Kiir(érték.ToString(), MyE.Oszlopnév(pj) + (sor + 5).ToString());

                                        érték = forgalomban;
                                        MunkaLap = "Forgalmi 3"; MyE.Munkalap_aktív(MunkaLap);
                                        MyE.Kiir(érték.ToString(), MyE.Oszlopnév(pj) + (sor + 5).ToString());

                                        érték = forgalomban + tartalék;
                                        MunkaLap = "Üzemképes 3"; MyE.Munkalap_aktív(MunkaLap);
                                        MyE.Kiir(érték.ToString(), MyE.Oszlopnév(pj) + (sor + 5).ToString());


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

                                MunkaLap = "állomány 3"; MyE.Munkalap_aktív(MunkaLap);
                                MyE.Kiir(érték.ToString(), MyE.Oszlopnév(pj) + (sor + 6).ToString());

                                érték = forgalomban;
                                MunkaLap = "Forgalmi 3"; MyE.Munkalap_aktív(MunkaLap);
                                MyE.Kiir(érték.ToString(), MyE.Oszlopnév(pj) + (sor + 6).ToString());

                                érték = forgalomban + tartalék;
                                MunkaLap = "Üzemképes 3"; MyE.Munkalap_aktív(MunkaLap);
                                MyE.Kiir(érték.ToString(), MyE.Oszlopnév(pj) + (sor + 6).ToString());

                                Holtart_Lép();
                                pj += 1;
                            }
                        }

                    }
                    oszlopmax = pj;

                }

                MunkaLap = "állomány 3"; MyE.Munkalap_aktív(MunkaLap);
                MyE.Kiir("Összesen", MyE.Oszlopnév(pj) + 5.ToString());

                MunkaLap = "Forgalmi 3"; MyE.Munkalap_aktív(MunkaLap);
                MyE.Kiir("Összesen", MyE.Oszlopnév(pj) + 5.ToString());

                MunkaLap = "Üzemképes 3"; MyE.Munkalap_aktív(MunkaLap);
                MyE.Kiir("Összesen", MyE.Oszlopnév(pj) + 5.ToString());
                pj += 1;

                MunkaLap = "állomány 3"; MyE.Munkalap_aktív(MunkaLap);
                MyE.Kiir("Összesen", MyE.Oszlopnév(pj) + 4.ToString());

                MunkaLap = "Forgalmi 3"; MyE.Munkalap_aktív(MunkaLap);
                MyE.Kiir("Összesen", MyE.Oszlopnév(pj) + 4.ToString());

                MunkaLap = "Üzemképes 3"; MyE.Munkalap_aktív(MunkaLap);
                MyE.Kiir("Összesen", MyE.Oszlopnév(pj) + 4.ToString());

                Oszlop_Max = pj;

                MunkaLap = "állomány 3"; MyE.Munkalap_aktív(MunkaLap);
                Rácsoz_3(MunkaLap);
                Havi_Összesítő_rész(Oszlop_Max);
                MyE.Aktív_Cella(MunkaLap, "A1");
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Rácsoz_3(string MunkaLap)
        {
            int oszlopmax = Oszlop_Max;
            int eleje;
            string szöveg;

            // Összesítések
            // A-B Oszlop formázása
            MyE.Munkalap_aktív(MunkaLap);
            MyE.Oszlopszélesség(MunkaLap, "B:B", 2);

            MyE.Oszlopszélesség(MunkaLap, "A:A");
            MyE.Rácsoz("A4:B" + (hónapnap + 5).ToString());
            MyE.Vastagkeret("A4:B" + (hónapnap + 5).ToString());

            // megnézzük az 5 sort ha van Összesen, akkor összesít
            Holtart_Be(hónapnap + 1);

            // ha több kategória volt
            // részösszegek
            eleje = 3;
            for (int j = 3; j < oszlopmax; j++)
            {
                if (MyE.Beolvas(MyE.Oszlopnév(j) + "4") == "Összesen")
                {
                    eleje = j + 1;
                }
                if (MyE.Beolvas(MyE.Oszlopnév(j) + "5") == "Összesen")
                {
                    for (int i = 1; i <= hónapnap; i++)
                    {
                        MyE.Kiir("=SUM(RC[-" + (j - eleje).ToString() + "]:RC[-1])", MyE.Oszlopnév(j) + (i + 5).ToString());
                        Holtart_Lép();
                    }

                    MyE.Egyesít(MunkaLap, MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(j) + "4");
                    MyE.Rácsoz(MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(j) + (hónapnap + 5).ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(j) + (hónapnap + 5).ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(j) + "5");
                    MyE.Betű(MyE.Oszlopnév(j) + "5:" + MyE.Oszlopnév(j) + (hónapnap + 5).ToString(), false, false, true);
                    MyE.Oszlopszélesség(MunkaLap, MyE.Oszlopnév(eleje) + ":" + MyE.Oszlopnév(j));
                    eleje = j + 1;
                }
            }
            // megformázzuk
            // állomány felirat
            eleje = 3;
            for (int j = 3; j <= oszlopmax; j++)
            {
                if (MyE.Beolvas(MyE.Oszlopnév(j) + "4") == "Összesen")
                {
                    MyE.Egyesít(MunkaLap, MyE.Oszlopnév(eleje) + "3:" + MyE.Oszlopnév(j) + "3");
                    MyE.Vastagkeret(MyE.Oszlopnév(eleje) + "3:" + MyE.Oszlopnév(j) + "3");
                    eleje = j + 1;
                }
            }
            eleje = 3;
            int vége;

            for (int i = 3; i <= oszlopmax; i++)
            {
                if (MyE.Beolvas(MyE.Oszlopnév(i) + "4") == "Összesen")
                {
                    // Összesítő rész formázása
                    MyE.Rácsoz(MyE.Oszlopnév(i) + "4:" + MyE.Oszlopnév(i) + (hónapnap + 5).ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(i) + "4:" + MyE.Oszlopnév(i) + (hónapnap + 5).ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(i) + "4:" + MyE.Oszlopnév(i) + "5");
                    MyE.Betű(MyE.Oszlopnév(i) + "4:" + MyE.Oszlopnév(i) + (hónapnap + 5).ToString(), false, true, true);
                    vége = i;

                    szöveg = "=";
                    for (int j = eleje; j <= vége; j++)
                    {
                        if (MyE.Beolvas(MyE.Oszlopnév(j) + "5") == "Összesen")
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
                        MyE.Kiir(szöveg, MyE.Oszlopnév(i) + (k + 5).ToString());
                        Holtart_Lép();
                    }
                    eleje = i + 1;
                }
            }

            // végösszesen
            MyE.Kiir("VégÖsszesen", MyE.Oszlopnév(oszlopmax + 1) + 3.ToString());
            // Összesítő rész formázása
            MyE.Rácsoz(MyE.Oszlopnév(oszlopmax + 1) + "3:" + MyE.Oszlopnév(oszlopmax + 1) + (hónapnap + 5).ToString());
            MyE.Vastagkeret(MyE.Oszlopnév(oszlopmax + 1) + "3:" + MyE.Oszlopnév(oszlopmax + 1) + (hónapnap + 5).ToString());
            MyE.Vastagkeret(MyE.Oszlopnév(oszlopmax + 1) + "3:" + MyE.Oszlopnév(oszlopmax + 1) + "5");
            MyE.Betű(MyE.Oszlopnév(oszlopmax + 1) + "3:" + MyE.Oszlopnév(oszlopmax + 1) + (hónapnap + 5).ToString(), false, true, true);
            MyE.Oszlopszélesség(MunkaLap, MyE.Oszlopnév(oszlopmax + 1) + ":" + MyE.Oszlopnév(oszlopmax + 1));

            szöveg = "=";

            for (int j = 3; j <= oszlopmax + 1; j++)
            {
                if (MyE.Beolvas(MyE.Oszlopnév(j) + "4") == "Összesen")
                {
                    if (szöveg == "=")
                    {
                        szöveg += "SUM(RC[-" + (oszlopmax + 1 - j).ToString() + "]";
                    }
                    else
                    {
                        szöveg += ",RC[-" + (oszlopmax + 1 - j).ToString() + "]";
                    }
                }
            }
            szöveg += ")";
            for (int k = 1; k <= hónapnap; k++)
            {
                MyE.Kiir(szöveg, MyE.Oszlopnév(oszlopmax + 1) + (k + 5).ToString());
                Holtart_Lép();
            }
        }

        private void Kiadott1tábla()
        {
            try
            {
                string MunkaLap = "Forgalmi 1";
                MyE.Munkalap_aktív(MunkaLap);
                MyE.Link_beillesztés(MunkaLap, "A1", "Tartalom");

                bool volt = false;
                Napok_kiírása();
                MunkaVHétvége();

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő.mdb";
                string jelszó = "Mocó";
                string szöveg = "SELECT * FROM szolgálattábla order by sorszám";
                Kezelő_Kiegészítő_Szolgálat KézKiegSzolg = new Kezelő_Kiegészítő_Szolgálat();
                List<Adat_Kiegészítő_Szolgálat> AdatokKiegSzolg = KézKiegSzolg.Lista_Adatok(hely, jelszó, szöveg);

                int pj = 3;
                int szolgálat = 0;
                Holtart_Be(hónapnap + 1);
                szöveg = Délelőtt.Checked ? "Reggeli " : "Délutáni ";
                szöveg += "Forgalomba adott darabszámok";
                MyE.Kiir(szöveg, MyE.Oszlopnév(pj) + 3.ToString());

                foreach (Adat_Kiegészítő_Szolgálat rekordkieg in AdatokKiegSzolg)
                {
                    szolgálat += 1;
                    MyE.Kiir(rekordkieg.Szolgálatnév, MyE.Oszlopnév(pj) + "4");
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

                            MyE.Kiir(Kategórilista.Items[k].ToStrTrim(), MyE.Oszlopnév(pj) + 5.ToString());
                            if (Elemek != null)
                            {
                                long forgalomban = Elemek.Sum(a => a.Forgalomban);
                                MyE.Kiir(forgalomban.ToString(), MyE.Oszlopnév(pj) + (i + 5).ToString());
                                volt = true;
                            }
                            else
                            {
                                MyE.Kiir("0", MyE.Oszlopnév(pj) + (i + 5).ToString());
                            }
                            Holtart_Lép();
                        }
                        pj += 1;
                    }
                    if (volt == false)
                    {
                        MyE.Kiir("Összesen", MyE.Oszlopnév(pj) + 5.ToString());
                        pj += 1;
                    }
                }
                MyE.Kiir("Összesen", MyE.Oszlopnév(pj) + 4.ToString());
                int oszlopmax = pj;

                // Összesítések
                // A-B Oszlop formázása
                MyE.Oszlopszélesség(MunkaLap, "B:B", 2);
                MyE.Oszlopszélesség(MunkaLap, "A:A");
                MyE.Rácsoz("A4:B" + (hónapnap + 5).ToString());
                MyE.Vastagkeret("A4:B" + (hónapnap + 5).ToString());

                int eleje;
                if (volt == true)
                {
                    // ha csak egy főkategória volt

                    for (int i = 1; i <= hónapnap; i++)
                    {
                        MyE.Kiir("=SUM(RC[-" + (oszlopmax - 3).ToString() + "]:RC[-1])", MyE.Oszlopnév(oszlopmax) + (i + 5).ToString());
                        Holtart.Value = i;
                    }
                    // megformázzuk
                    MyE.Egyesít(MunkaLap, "c3:" + MyE.Oszlopnév(oszlopmax) + "3");
                    MyE.Rácsoz("c4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyE.Vastagkeret("c4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyE.Vastagkeret("c4:" + MyE.Oszlopnév(oszlopmax) + "5");
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyE.Betű(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString(), false, true, true);
                    // Oszlop szélesség beállítás
                    MyE.Oszlopszélesség(MunkaLap, "C:" + MyE.Oszlopnév(oszlopmax));
                }
                else
                {
                    // ha több kategória volt

                    // részösszegek
                    eleje = 3;

                    for (int j = 3; j <= oszlopmax; j++)
                    {
                        if (MyE.Beolvas(MyE.Oszlopnév(j) + "5") == "Összesen")
                        {
                            for (int i = 1; i <= hónapnap; i++)
                            {
                                MyE.Kiir("=SUM(RC[-" + (j - eleje).ToString() + "]:RC[-1])", MyE.Oszlopnév(j) + (i + 5).ToString());
                                Holtart_Lép();
                            }
                            MyE.Egyesít(MunkaLap, MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(j) + "4");
                            MyE.Rácsoz(MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(j) + (hónapnap + 5).ToString());
                            MyE.Vastagkeret(MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(j) + (hónapnap + 5).ToString());
                            MyE.Vastagkeret(MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(j) + "5");
                            MyE.Betű(MyE.Oszlopnév(j) + "5:" + MyE.Oszlopnév(j) + (hónapnap + 5).ToString(), false, false, true);
                            MyE.Oszlopszélesség(MunkaLap, MyE.Oszlopnév(eleje) + ":" + MyE.Oszlopnév(j));
                            eleje = j + 1;
                        }
                    }
                    // megformázzuk
                    // állomány felirat
                    MyE.Egyesít(MunkaLap, MyE.Oszlopnév(3) + "3:" + MyE.Oszlopnév(oszlopmax) + "3");
                    // Összesítő rész formázása
                    MyE.Rácsoz(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + "5");
                    MyE.Betű(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString(), false, true, true);

                    szöveg = "";
                    szöveg = "=";
                    for (int j = 3; j <= oszlopmax; j++)
                    {
                        if (MyE.Beolvas(MyE.Oszlopnév(j) + "5") == "Összesen")
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
                        MyE.Kiir(szöveg, MyE.Oszlopnév(oszlopmax) + (i + 5).ToString());
                        Holtart_Lép();
                    }
                }
                Havi_Összesítő_rész(oszlopmax);
                MyE.Aktív_Cella(MunkaLap, "A1");
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
                string MunkaLap = "Forgalmi 2";
                MyE.Munkalap_aktív(MunkaLap);
                MyE.Link_beillesztés(MunkaLap, "A1", "Tartalom");

                Napok_kiírása();
                MunkaVHétvége();

                string szöveg = Délelőtt.Checked ? "Reggeli " : "Délutáni ";
                szöveg += "Állományi darabszámok";
                MyE.Kiir(szöveg, MyE.Oszlopnév(3) + 3.ToString());

                // '****************************************************
                // 'Elkészítjük a táblázatot
                // '****************************************************
                string helykieg = $@"{Application.StartupPath}\főmérnökség\adatok\Kiegészítő.mdb";
                string jelszókieg = "Mocó";
                szöveg = "SELECT * FROM típusaltípustábla";
                Kezelő_Kiegészítő_Típusaltípustábla KézKiegTípusal = new Kezelő_Kiegészítő_Típusaltípustábla();
                List<Adat_Kiegészítő_Típusaltípustábla> AdatokKiegTípusal = KézKiegTípusal.Lista_Adatok(helykieg, jelszókieg, szöveg);

                bool volt = false;
                int pj = 3;
                int oszlopmax;
                Holtart_Be(hónapnap + 1);

                string előzőtípus = "";

                for (int k = 0; k <= Kategórilista.CheckedItems.Count - 1; k++)
                {

                    if (előzőtípus.Trim() == "")
                    {
                        MyE.Kiir(Kategórilista.CheckedItems[k].ToStrTrim(), MyE.Oszlopnév(pj) + 4.ToString());
                        előzőtípus = Kategórilista.CheckedItems[k].ToStrTrim();
                    }
                    else
                    {
                        MyE.Kiir("Összesen", MyE.Oszlopnév(pj) + 5.ToString());
                        pj += 1;
                        MyE.Kiir(Kategórilista.CheckedItems[k].ToStrTrim(), MyE.Oszlopnév(pj) + 4.ToString());
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

                            MyE.Kiir(rekordkieg.AlTípus, MyE.Oszlopnév(pj) + 5.ToString());
                            if (Elemek != null)
                            {
                                long forgalomban = Elemek.Sum(a => a.Forgalomban);
                                MyE.Kiir(forgalomban.ToString(), MyE.Oszlopnév(pj) + (i + 5).ToString());
                            }
                            else
                            {
                                MyE.Kiir("0", MyE.Oszlopnév(pj) + (i + 5).ToString());
                            }
                        }
                        oszlopmax = pj;
                        pj += 1;
                        Holtart_Lép();
                    }
                }

                if (volt != true)
                {
                    MyE.Kiir("Összesen", MyE.Oszlopnév(pj) + 5.ToString());
                    pj += 1;
                }
                MyE.Kiir("Összesen", MyE.Oszlopnév(pj) + 4.ToString());
                oszlopmax = pj;
                // Összesítések
                // A-B Oszlop formázása
                MyE.Oszlopszélesség(MunkaLap, "B:B", 2);
                MyE.Oszlopszélesség(MunkaLap, "A:A");

                MyE.Rácsoz("A4:B" + (hónapnap + 5).ToString());
                MyE.Vastagkeret("A4:B" + (hónapnap + 5).ToString());

                int eleje;
                if (volt == true)
                {
                    // ha csak egy főkategória volt

                    for (int i = 1; i <= hónapnap; i++)
                    {
                        MyE.Kiir("=SUM(RC[-" + (oszlopmax - 3).ToString() + "]:RC[-1])", MyE.Oszlopnév(oszlopmax) + (i + 5).ToString());
                        Holtart.Value = i;
                    }
                    // megformázzuk
                    MyE.Egyesít(MunkaLap, "c3:" + MyE.Oszlopnév(oszlopmax) + "3");
                    MyE.Rácsoz("c4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyE.Vastagkeret("c4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyE.Vastagkeret("c4:" + MyE.Oszlopnév(oszlopmax) + "5");
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyE.Betű(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString(), false, true, true);
                    // Oszlop szélesség beállítás
                    MyE.Oszlopszélesség(MunkaLap, "C:" + MyE.Oszlopnév(oszlopmax));
                }

                else
                {
                    // ha több kategória volt
                    // részösszegek
                    eleje = 3;
                    for (int j = 3; j <= oszlopmax; j++)
                    {
                        if (MyE.Beolvas(MyE.Oszlopnév(j) + "5") == "Összesen")
                        {
                            for (int i = 1; i <= hónapnap; i++)
                            {
                                MyE.Kiir("=SUM(RC[-" + (j - eleje).ToString() + "]:RC[-1])", MyE.Oszlopnév(j) + (i + 5).ToString());
                                Holtart.Value = i;
                            }
                            MyE.Egyesít(MunkaLap, MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(j) + "4");
                            MyE.Rácsoz(MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(j) + (hónapnap + 5).ToString());
                            MyE.Vastagkeret(MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(j) + (hónapnap + 5).ToString());
                            MyE.Vastagkeret(MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(j) + "5");
                            MyE.Betű(MyE.Oszlopnév(j) + "5:" + MyE.Oszlopnév(j) + (hónapnap + 5).ToString(), false, false, true);
                            MyE.Oszlopszélesség(MunkaLap, MyE.Oszlopnév(eleje) + ":" + MyE.Oszlopnév(j));
                            eleje = j + 1;
                        }
                    }


                    // megformázzuk
                    // állomány felirat
                    MyE.Egyesít(MunkaLap, MyE.Oszlopnév(3) + "3:" + MyE.Oszlopnév(oszlopmax) + "3");

                    // Összesítő rész formázása
                    MyE.Rácsoz(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + "5");
                    MyE.Betű(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString(), false, true, true);

                    szöveg = "";
                    szöveg = "=";

                    for (int j = 3; j <= oszlopmax; j++)
                    {
                        if (MyE.Beolvas(MyE.Oszlopnév(j) + "5") == "Összesen")
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
                        MyE.Kiir(szöveg, MyE.Oszlopnév(oszlopmax) + (i + 5).ToString());
                        Holtart.Value = i;
                    }
                }
                Havi_Összesítő_rész(oszlopmax);
                MyE.Aktív_Cella(MunkaLap, "A1");
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
                string MunkaLap = "Forgalmi 3";
                MyE.Munkalap_aktív("Forgalmi 3");
                MyE.Link_beillesztés(MunkaLap, "A1", "Tartalom");

                Napok_kiírása();
                MunkaVHétvége();

                Rácsoz_3(MunkaLap);
                Havi_Összesítő_rész(Oszlop_Max);
                MyE.Aktív_Cella(MunkaLap, "A1");
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
                string MunkaLap = "Üzemképes 1";
                MyE.Munkalap_aktív(MunkaLap);
                MyE.Link_beillesztés(MunkaLap, "A1", "Tartalom");
                Napok_kiírása();
                MunkaVHétvége();

                // ****************************************************
                // Elkészítjük a táblázatot
                // ****************************************************
                string helykieg = Application.StartupPath + @"\főmérnökség\adatok\Kiegészítő.mdb";
                string jelszókieg = "Mocó";
                string szöveg = "SELECT * FROM szolgálattábla order by sorszám";
                Kezelő_Kiegészítő_Szolgálat KézKiegSzolgálat = new Kezelő_Kiegészítő_Szolgálat();
                List<Adat_Kiegészítő_Szolgálat> AdatokKiegSzolgálat = KézKiegSzolgálat.Lista_Adatok(helykieg, jelszókieg, szöveg);

                szöveg = Délelőtt.Checked ? "Reggeli " : "Délutáni ";
                szöveg += "Üzemképes darabszámok";
                int jj = 3;
                MyE.Kiir(szöveg, MyE.Oszlopnév(jj) + 3.ToString());

                Holtart_Be(hónapnap + 1);

                bool volt = false;
                foreach (Adat_Kiegészítő_Szolgálat rekordkieg in AdatokKiegSzolgálat)
                {

                    MyE.Kiir(rekordkieg.Szolgálatnév, MyE.Oszlopnév(jj) + 4.ToString());

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

                            MyE.Kiir(Kategórilista.Items[k].ToStrTrim(), MyE.Oszlopnév(jj) + 5.ToString());
                            if (Elemek != null)
                            {
                                long forgalomban = Elemek.Sum(a => a.Forgalomban);
                                long tartalék = Elemek.Sum(a => a.Tartalék);

                                long érték = forgalomban + tartalék;
                                MyE.Kiir(érték.ToString(), MyE.Oszlopnév(jj) + (ki + 5).ToString());
                                volt = true;
                            }
                            else
                            {
                                MyE.Kiir("0", MyE.Oszlopnév(jj) + (ki + 5).ToString());
                            }

                        }
                        jj += 1;

                        Holtart_Lép();
                    }
                    if (volt == true)
                    {
                        MyE.Kiir("Összesen", MyE.Oszlopnév(jj) + 5.ToString());
                        jj += 1;
                    }
                    volt = false;
                }
                MyE.Kiir("Összesen", MyE.Oszlopnév(jj) + 4.ToString());
                int oszlopmax = jj;

                // Összesítések
                // A-B Oszlop formázása
                MyE.Oszlopszélesség(MunkaLap, "B:B", 2);
                MyE.Oszlopszélesség(MunkaLap, "A:A");

                MyE.Rácsoz("A4:B" + (hónapnap + 5).ToString());
                MyE.Vastagkeret("A4:B" + (hónapnap + 5).ToString());
                // állomány felirat
                MyE.Egyesít(MunkaLap, MyE.Oszlopnév(3) + "3:" + MyE.Oszlopnév(oszlopmax) + "3");
                if (volt == true)
                {
                    // ha csak egy főkategória volt
                    for (int vi = 1; vi <= hónapnap; vi++)
                    {
                        MyE.Kiir("=SUM(RC[-" + (oszlopmax - 3).ToString() + "]:RC[-1])", MyE.Oszlopnév(oszlopmax) + (vi + 5).ToString());
                        Holtart.Value = vi;
                    }
                    // megformázzuk
                    MyE.Rácsoz("c4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyE.Vastagkeret("c4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyE.Vastagkeret("c4:" + MyE.Oszlopnév(oszlopmax) + "5");
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyE.Betű(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString(), false, true, true);
                    // Oszlop szélesség beállítás
                    MyE.Oszlopszélesség(MunkaLap, "C:" + MyE.Oszlopnév(oszlopmax));

                }

                else
                {
                    // ha több kategória volt

                    // részösszegek
                    int eleje = 3;
                    for (int wj = 3; wj <= oszlopmax; wj++)
                    {
                        if (MyE.Beolvas(MyE.Oszlopnév(wj) + "5") == "Összesen")
                        {

                            for (int wi = 1; wi <= hónapnap; wi++)
                            {
                                MyE.Kiir("=SUM(RC[-" + (wj - eleje).ToString() + "]:RC[-1])", MyE.Oszlopnév(wj) + (wi + 5).ToString());
                                Holtart.Value = wi;
                            }
                            MyE.Egyesít(MunkaLap, MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(wj) + "4");
                            MyE.Rácsoz(MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(wj) + (hónapnap + 5).ToString());
                            MyE.Vastagkeret(MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(wj) + (hónapnap + 5).ToString());
                            MyE.Vastagkeret(MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(wj) + "5");
                            MyE.Betű(MyE.Oszlopnév(wj) + "5:" + MyE.Oszlopnév(wj) + (hónapnap + 5).ToString(), false, false, true);
                            MyE.Oszlopszélesség(MunkaLap, MyE.Oszlopnév(eleje) + ":" + MyE.Oszlopnév(wj));

                            eleje = wj + 1;
                        }
                    }
                    // megformázzuk
                    // Összesítő rész formázása
                    MyE.Rácsoz(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + "5");
                    MyE.Betű(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString(), false, true, true);
                    szöveg = "=";

                    for (int j = 3; j <= oszlopmax; j++)
                    {
                        if (MyE.Beolvas(MyE.Oszlopnév(j) + "5") == "Összesen")
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
                        MyE.Kiir(szöveg, MyE.Oszlopnév(oszlopmax) + (i + 5).ToString());
                        Holtart.Value = i;
                    }
                }
                // Alsó összesítés és átlag
                Havi_Összesítő_rész(oszlopmax);
                MyE.Aktív_Cella(MunkaLap, "A1");
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
                string MunkaLap = "Üzemképes 2";
                MyE.Munkalap_aktív(MunkaLap);
                MyE.Link_beillesztés(MunkaLap, "A1", "Tartalom");
                Napok_kiírása();
                MunkaVHétvége();

                string szöveg = Délelőtt.Checked ? "Reggeli " : "Délutáni ";
                szöveg += "Állományi darabszámok";
                MyE.Kiir(szöveg, MyE.Oszlopnév(3) + 3.ToString());

                // '****************************************************
                // 'Elkészítjük a táblázatot
                // '****************************************************
                string helykieg = $@"{Application.StartupPath}\főmérnökség\adatok\Kiegészítő.mdb";
                string jelszókieg = "Mocó";
                szöveg = "SELECT * FROM típusaltípustábla";
                Kezelő_Kiegészítő_Típusaltípustábla KézKiegTípusal = new Kezelő_Kiegészítő_Típusaltípustábla();
                List<Adat_Kiegészítő_Típusaltípustábla> AdatokKiegTípusal = KézKiegTípusal.Lista_Adatok(helykieg, jelszókieg, szöveg);

                bool volt = false;
                int pj = 3;
                int oszlopmax;
                Holtart_Be(hónapnap + 1);

                string előzőtípus = "";

                for (int k = 0; k <= Kategórilista.CheckedItems.Count - 1; k++)
                {

                    if (előzőtípus.Trim() == "")
                    {
                        MyE.Kiir(Kategórilista.CheckedItems[k].ToStrTrim(), MyE.Oszlopnév(pj) + 4.ToString());
                        előzőtípus = Kategórilista.CheckedItems[k].ToStrTrim();
                    }
                    else
                    {
                        MyE.Kiir("Összesen", MyE.Oszlopnév(pj) + 5.ToString());
                        pj += 1;
                        MyE.Kiir(Kategórilista.CheckedItems[k].ToStrTrim(), MyE.Oszlopnév(pj) + 4.ToString());
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

                            MyE.Kiir(rekordkieg.AlTípus, MyE.Oszlopnév(pj) + 5.ToString());
                            if (Elemek != null)
                            {
                                long forgalomban = Elemek.Sum(a => a.Forgalomban);
                                long tartalék = Elemek.Sum(a => a.Tartalék);

                                long érték = forgalomban + tartalék;
                                MyE.Kiir(érték.ToString(), MyE.Oszlopnév(pj) + (i + 5).ToString());
                            }
                            else
                            {
                                MyE.Kiir("0", MyE.Oszlopnév(pj) + (i + 5).ToString());
                            }
                        }
                        oszlopmax = pj;
                        pj += 1;
                        Holtart_Lép();
                    }
                }

                if (volt != true)
                {
                    MyE.Kiir("Összesen", MyE.Oszlopnév(pj) + 5.ToString());
                    pj += 1;
                }
                MyE.Kiir("Összesen", MyE.Oszlopnév(pj) + 4.ToString());
                oszlopmax = pj;
                // Összesítések
                // A-B Oszlop formázása
                MyE.Oszlopszélesség(MunkaLap, "B:B", 2);
                MyE.Oszlopszélesség(MunkaLap, "A:A");

                MyE.Rácsoz("A4:B" + (hónapnap + 5).ToString());
                MyE.Vastagkeret("A4:B" + (hónapnap + 5).ToString());

                int eleje;
                if (volt == true)
                {
                    // ha csak egy főkategória volt

                    for (int i = 1; i <= hónapnap; i++)
                    {
                        MyE.Kiir("=SUM(RC[-" + (oszlopmax - 3).ToString() + "]:RC[-1])", MyE.Oszlopnév(oszlopmax) + (i + 5).ToString());
                        Holtart.Value = i;
                    }
                    // megformázzuk
                    MyE.Egyesít(MunkaLap, "c3:" + MyE.Oszlopnév(oszlopmax) + "3");
                    MyE.Rácsoz("c4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyE.Vastagkeret("c4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyE.Vastagkeret("c4:" + MyE.Oszlopnév(oszlopmax) + "5");
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyE.Betű(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString(), false, true, true);
                    // Oszlop szélesség beállítás
                    MyE.Oszlopszélesség(MunkaLap, "C:" + MyE.Oszlopnév(oszlopmax));
                }

                else
                {
                    // ha több kategória volt
                    // részösszegek
                    eleje = 3;
                    for (int j = 3; j <= oszlopmax; j++)
                    {
                        if (MyE.Beolvas(MyE.Oszlopnév(j) + "5") == "Összesen")
                        {
                            for (int i = 1; i <= hónapnap; i++)
                            {
                                MyE.Kiir("=SUM(RC[-" + (j - eleje).ToString() + "]:RC[-1])", MyE.Oszlopnév(j) + (i + 5).ToString());
                                Holtart.Value = i;
                            }
                            MyE.Egyesít(MunkaLap, MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(j) + "4");
                            MyE.Rácsoz(MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(j) + (hónapnap + 5).ToString());
                            MyE.Vastagkeret(MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(j) + (hónapnap + 5).ToString());
                            MyE.Vastagkeret(MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(j) + "5");
                            MyE.Betű(MyE.Oszlopnév(j) + "5:" + MyE.Oszlopnév(j) + (hónapnap + 5).ToString(), false, false, true);
                            MyE.Oszlopszélesség(MunkaLap, MyE.Oszlopnév(eleje) + ":" + MyE.Oszlopnév(j));
                            eleje = j + 1;
                        }
                    }


                    // megformázzuk
                    // állomány felirat
                    MyE.Egyesít(MunkaLap, MyE.Oszlopnév(3) + "3:" + MyE.Oszlopnév(oszlopmax) + "3");

                    // Összesítő rész formázása
                    MyE.Rácsoz(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + "5");
                    MyE.Betű(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString(), false, true, true);

                    szöveg = "";
                    szöveg = "=";

                    for (int j = 3; j <= oszlopmax; j++)
                    {
                        if (MyE.Beolvas(MyE.Oszlopnév(j) + "5") == "Összesen")
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
                        MyE.Kiir(szöveg, MyE.Oszlopnév(oszlopmax) + (i + 5).ToString());
                        Holtart.Value = i;
                    }
                }
                Havi_Összesítő_rész(oszlopmax);
                MyE.Aktív_Cella(MunkaLap, "A1");

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
                string MunkaLap = "Üzemképes 3";
                MyE.Munkalap_aktív(MunkaLap);
                MyE.Link_beillesztés(MunkaLap, "A1", "Tartalom");

                Napok_kiírása();
                MunkaVHétvége();

                Rácsoz_3(MunkaLap);
                Havi_Összesítő_rész(Oszlop_Max);
                MyE.Aktív_Cella(MunkaLap, "A1");
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
                string MunkaLap = "Adatok 1";
                MyE.Munkalap_aktív(MunkaLap);
                MyE.Link_beillesztés(MunkaLap, "A1", "Tartalom");

                // hónap eleje és vége
                int hónapnap = MyF.Hónap_hossza(Dátum.Value);
                DateTime hónaputolsónapja = MyF.Hónap_utolsónapja(Dátum.Value);
                DateTime hónapelsőnapja = MyF.Hónap_elsőnapja(Dátum.Value);
                string hely, jelszó, szöveg;
                int i;

                // fejléc elkészítése
                MyE.Kiir("Dátum", "A5");
                MyE.Kiir("Napszak", "B5");
                MyE.Kiir("Főkategória", "C5");
                MyE.Kiir("Típus", "d5");
                MyE.Kiir("AlTípus", "e5");
                MyE.Kiir("Szolgálat", "F5");
                MyE.Kiir("Telephely", "g5");
                MyE.Kiir("Kiadás", "h5");
                MyE.Kiir("Forgalomban", "i5");
                MyE.Kiir("Eltérés", "j5");
                MyE.Kiir("Tartalék", "k5");
                MyE.Kiir("Kocsiszíni", "l5");
                MyE.Kiir("Félreállítás", "m5");
                MyE.Kiir("Főjavítás", "n5");
                MyE.Kiir("Állomány", "o5");
                MyE.Kiir("Személyzethiány", "p5");
                MyE.Kiir("Munkanap", "q5");
                MyE.Kiir("Tart.+Szem.hiány", "r5");

                hely = $@"{Application.StartupPath}\főmérnökség\adatok\{Dátum.Value:yyyy}\{Dátum.Value:yyyy}_kiadási_adatok.mdb";
                jelszó = "pozsi";
                szöveg = "SELECT * FROM kiadástábla where [dátum]>=#" + hónapelsőnapja.ToString("MM-dd-yyyy") + "#";
                szöveg += " and [dátum]<=#" + hónaputolsónapja.ToString("MM-dd-yyyy") + "#";
                if (Délelőtt.Checked)
                {
                    szöveg += " and napszak='de'";
                }
                else
                {
                    szöveg += " and napszak='du'";
                }
                szöveg += " order by dátum";

                Holtart.Maximum = 50;
                Holtart.Value = 1;

                Kezelő_FőKiadási_adatok KézKiadAd = new Kezelő_FőKiadási_adatok();
                List<Adat_FőKiadási_adatok> AdatokKiadAd = KézKiadAd.Lista_adatok(hely, jelszó, szöveg);
                i = 6;

                foreach (Adat_FőKiadási_adatok rekord in AdatokKiadAd)
                {

                    MyE.Kiir(DateTime.Parse(rekord.Dátum.ToString()).ToString("yyyy.MM.dd"), "A" + i.ToString());
                    MyE.Kiir(rekord.Napszak, "B" + i.ToString());
                    MyE.Kiir(rekord.Főkategória, "C" + i.ToString());
                    MyE.Kiir(rekord.Típus, "d" + i.ToString());
                    MyE.Kiir(rekord.Altípus, "e" + i.ToString());
                    MyE.Kiir(rekord.Szolgálat, "F" + i.ToString());
                    MyE.Kiir(rekord.Telephely, "g" + i.ToString());
                    MyE.Kiir(rekord.Kiadás.ToString(), "h" + i.ToString());
                    MyE.Kiir(rekord.Forgalomban.ToString(), "i" + i.ToString());
                    MyE.Kiir((int.Parse(rekord.Kiadás.ToString()) - int.Parse(rekord.Forgalomban.ToString())).ToString(), "j" + i.ToString());
                    MyE.Kiir(rekord.Tartalék.ToString(), "k" + i.ToString());
                    MyE.Kiir(rekord.Kocsiszíni.ToString(), "l" + i.ToString());
                    MyE.Kiir(rekord.Félreállítás.ToString(), "m" + i.ToString());
                    MyE.Kiir(rekord.Főjavítás.ToString(), "n" + i.ToString());
                    MyE.Kiir(rekord.Személyzet.ToString(), "p" + i.ToString());
                    MyE.Kiir((int.Parse(rekord.Tartalék.ToString()) + int.Parse(rekord.Személyzet.ToString())).ToString(), "r" + i.ToString());
                    if (int.Parse(rekord.Munkanap.ToString()) == 0)
                        MyE.Kiir("Munkanap", "q" + i.ToString());
                    else
                        MyE.Kiir("Hétvége", "q" + i.ToString());

                    int összesen = int.Parse(rekord.Forgalomban.ToString()) + int.Parse(rekord.Tartalék.ToString()) + int.Parse(rekord.Kocsiszíni.ToString())
                        + int.Parse(rekord.Félreállítás.ToString()) + int.Parse(rekord.Főjavítás.ToString()) + int.Parse(rekord.Személyzet.ToString());
                    MyE.Kiir(összesen.ToString(), "o" + i.ToString());

                    Holtart.Value += 1;
                    if (Holtart.Value == 50)
                        Holtart.Value = 1;
                    i += 1;
                }

                alsóPanels4.Text = i.ToString();

                MyE.Oszlopszélesség(MunkaLap, "A:R");
                MyE.Aktív_Cella(MunkaLap, "A1");
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
                string MunkaLap = "Adatok 2";
                MyE.Munkalap_aktív(MunkaLap);
                MyE.Link_beillesztés(MunkaLap, "A1", "Tartalom");

                // hónap eleje és vége
                int hónapnap = MyF.Hónap_hossza(Dátum.Value);
                DateTime hónaputolsónapja = MyF.Hónap_utolsónapja(Dátum.Value);
                DateTime hónapelsőnapja = MyF.Hónap_elsőnapja(Dátum.Value);

                string hely, jelszó, szöveg;
                int i;
                // fejléc elkészítése
                MyE.Kiir("Dátum", "A5");
                MyE.Kiir("Napszak", "B5");
                MyE.Kiir("Telephely", "C5");
                MyE.Kiir("Szolgálat", "D5");
                MyE.Kiir("Típus", "E5");
                MyE.Kiir("Viszonylat", "F5");
                MyE.Kiir("Forgalmiszám", "G5");
                MyE.Kiir("tervindulás", "H5");
                MyE.Kiir("Azonosító", "I5");

                hely = $@"{Application.StartupPath}\főmérnökség\adatok\{Dátum.Value:yyyy}\{Dátum.Value:yyyy}_személyzet_adatok.mdb";
                jelszó = "pozsi";
                szöveg = "SELECT * FROM személyzettábla where [dátum]>=#" + hónapelsőnapja.ToString("MM-dd-yyyy") + "#";
                szöveg += " and [dátum]<=#" + hónaputolsónapja.ToString("MM-dd-yyyy") + "#";
                if (Délelőtt.Checked)
                {
                    szöveg += " and napszak='de'";
                }
                else
                {
                    szöveg += " and napszak='du'";
                }
                szöveg += " order by dátum, viszonylat, tervindulás";

                Holtart.Maximum = 50;
                Holtart.Value = 1;

                Kezelő_FőSzemélyzet_Adatok KézSzemAd = new Kezelő_FőSzemélyzet_Adatok();
                List<Adat_Személyzet_Adatok> AdatokSzemAd = KézSzemAd.Lista_adatok(hely, jelszó, szöveg);
                i = 6;
                foreach (Adat_Személyzet_Adatok rekord in AdatokSzemAd)
                {

                    MyE.Kiir(DateTime.Parse(rekord.Dátum.ToString()).ToString("yyyy.MM.dd"), "A" + i.ToString());
                    MyE.Kiir(rekord.Napszak, "B" + i.ToString());
                    MyE.Kiir(rekord.Telephely, "C" + i.ToString());
                    MyE.Kiir(rekord.Szolgálat, "D" + i.ToString());
                    MyE.Kiir(rekord.Típus, "E" + i.ToString());
                    MyE.Kiir(rekord.Viszonylat, "F" + i.ToString());
                    MyE.Kiir(rekord.Forgalmiszám, "G" + i.ToString());
                    MyE.Kiir(rekord.Tervindulás.ToString(), "H" + i.ToString());
                    MyE.Kiir(rekord.Azonosító, "I" + i.ToString());
                    Holtart.Value += 1;
                    if (Holtart.Value == 50)
                        Holtart.Value = 1;
                    i += 1;
                }
                MyE.Oszlopszélesség(MunkaLap, "A:i");
                MyE.Aktív_Cella(MunkaLap, "A1");
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
                string MunkaLap = "Adatok 3";
                MyE.Munkalap_aktív(MunkaLap);
                MyE.Link_beillesztés(MunkaLap, "A1", "Tartalom");

                // hónap eleje és vége
                int hónapnap = MyF.Hónap_hossza(Dátum.Value);
                DateTime hónaputolsónapja = MyF.Hónap_utolsónapja(Dátum.Value);
                DateTime hónapelsőnapja = MyF.Hónap_elsőnapja(Dátum.Value);

                string hely, jelszó, szöveg; int i;

                // fejléc elkészítése
                MyE.Kiir("Dátum", "A5");
                MyE.Kiir("Napszak", "B5");
                MyE.Kiir("Telephely", "C5");
                MyE.Kiir("Szolgálat", "D5");
                MyE.Kiir("Típuselőírt", "E5");
                MyE.Kiir("Típuskiadott", "F5");
                MyE.Kiir("Viszonylat", "G5");
                MyE.Kiir("Forgalmiszám", "H5");
                MyE.Kiir("tervindulás", "I5");
                MyE.Kiir("Azonosító", "J5");

                hely = $@"{Application.StartupPath}\főmérnökség\adatok\{Dátum.Value:yyyy}\{Dátum.Value:yyyy}_típuscsere_adatok.mdb";
                jelszó = "pozsi";
                szöveg = "SELECT * FROM típuscseretábla where [dátum]>=#" + System.Threading.Thread.CurrentThread.CurrentCulture.Calendar.GetMonth(Dátum.Value).ToString() + "-1-" + System.Threading.Thread.CurrentThread.CurrentCulture.Calendar.GetYear(Dátum.Value).ToString() + "#";
                szöveg += " and [dátum]<=#" + hónaputolsónapja.ToString("MM-dd-yyyy") + "#";
                if (Délelőtt.Checked)
                {
                    szöveg += " and napszak='de'";
                }
                else
                {
                    szöveg += " and napszak='du'";
                }
                szöveg += " order by dátum, viszonylat, tervindulás";
                Holtart.Maximum = 50;
                Holtart.Value = 1;

                Kezelő_FőTípuscsere_Adatok KézTípuscsereAdatok = new Kezelő_FőTípuscsere_Adatok();
                List<Adat_Típuscsere_Adatok> AdatokTípuscsereAdatok = KézTípuscsereAdatok.Lista_adatok(hely, jelszó, szöveg);
                i = 6;
                foreach (Adat_Típuscsere_Adatok rekord in AdatokTípuscsereAdatok)
                {

                    MyE.Kiir(DateTime.Parse(rekord.Dátum.ToString()).ToString("yyyy.MM.dd"), "A" + i.ToString());
                    MyE.Kiir(rekord.Napszak, "B" + i.ToString());
                    MyE.Kiir(rekord.Telephely, "C" + i.ToString());
                    MyE.Kiir(rekord.Szolgálat, "D" + i.ToString());
                    MyE.Kiir(rekord.Típuselőírt, "E" + i.ToString());
                    MyE.Kiir(rekord.Típuskiadott, "f" + i.ToString());
                    MyE.Kiir(rekord.Viszonylat, "g" + i.ToString());
                    MyE.Kiir(rekord.Forgalmiszám, "h" + i.ToString());
                    MyE.Kiir(rekord.Tervindulás.ToString(), "i" + i.ToString());
                    MyE.Kiir(rekord.Azonosító, "j" + i.ToString());
                    Holtart.Value += 1;
                    if (Holtart.Value == 50)
                        Holtart.Value = 1;
                    i += 1;
                }
                MyE.Oszlopszélesség(MunkaLap, "A:j");
                MyE.Aktív_Cella(MunkaLap, "A1");
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
                string MunkaLap = "Kocsiszín";
                MyE.Munkalap_aktív(MunkaLap);
                MyE.Link_beillesztés(MunkaLap, "A1", "Tartalom");

                string szöveg = "Kocsiszíni darabszámok";
                MyE.Kiir(szöveg, MyE.Oszlopnév(3) + 3.ToString());

                Napok_kiírása();
                MunkaVHétvége();

                Kezelő_kiegészítő_telephely KézKiegTeleph = new Kezelő_kiegészítő_telephely();
                List<Adat_kiegészítő_telephely> AdatokKiegTeleph = KézKiegTeleph.Lista_adatok();

                int oszlopmax = 0, eleje;
                Holtart_Be(hónapnap + 1);
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
                            MyE.Kiir(rekordkieg.Telephelynév, MyE.Oszlopnév(pj) + 4.ToString());

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
                                MyE.Kiir(Elem.Altípus, MyE.Oszlopnév(pj) + 5.ToString());
                                MyE.Kiir(érték.ToString(), MyE.Oszlopnév(pj) + (sor + 5).ToString());
                            }
                            pj += 1;
                            MyE.Kiir("Összesen", MyE.Oszlopnév(pj) + 5.ToString());
                            pj += 1;
                        }
                    }
                }
                MyE.Kiir("Összesen", MyE.Oszlopnév(pj) + 4.ToString());
                oszlopmax = pj;

                // Összesítések
                // A-B Oszlop formázása
                MyE.Oszlopszélesség(MunkaLap, "B:B", 2);
                MyE.Oszlopszélesség(MunkaLap, "A:A");
                MyE.Rácsoz("A4:B" + (hónapnap + 5).ToString());
                MyE.Vastagkeret("A4:B" + (hónapnap + 5).ToString());
                // állomány felirat
                MyE.Egyesít(MunkaLap, MyE.Oszlopnév(3) + "3:" + MyE.Oszlopnév(oszlopmax) + "3");
                MyE.Vastagkeret(MyE.Oszlopnév(3) + "3:" + MyE.Oszlopnév(oszlopmax) + "3");


                eleje = 3;
                for (int j = 3; j <= oszlopmax; j++)
                {
                    if (MyE.Beolvas(MyE.Oszlopnév(j) + "5") == "Összesen")
                    {
                        for (int i = 1; i <= hónapnap; i++)
                        {
                            MyE.Kiir("=SUM(RC[-" + (j - eleje).ToString() + "]:RC[-1])", MyE.Oszlopnév(j) + (i + 5).ToString());
                            Holtart.Value = i;
                        }
                        MyE.Egyesít(MunkaLap, MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(j) + "4");
                        MyE.Rácsoz(MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(j) + (hónapnap + 5).ToString());
                        MyE.Vastagkeret(MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(j) + (hónapnap + 5).ToString());
                        MyE.Vastagkeret(MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(j) + "5");
                        MyE.Betű(MyE.Oszlopnév(j) + "5:" + MyE.Oszlopnév(j) + (hónapnap + 5).ToString(), false, false, true);
                        MyE.Oszlopszélesség(MunkaLap, MyE.Oszlopnév(eleje) + ":" + MyE.Oszlopnév(j));

                        eleje = j + 1;
                    }
                }
                // ha csak egy főkategória volt
                szöveg = "=";
                for (int j = 3; j <= oszlopmax; j++)
                {
                    if (MyE.Beolvas(MyE.Oszlopnév(j) + "5") == "Összesen")
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
                    MyE.Kiir(szöveg, MyE.Oszlopnév(oszlopmax) + (i + 5).ToString());
                    Holtart_Lép();
                }
                // megformázzuk
                // MyE.Egyesít("c3:" + oszlopnév(oszlopmax) + "3")
                MyE.Rácsoz("c4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                MyE.Vastagkeret("c4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                MyE.Vastagkeret("c4:" + MyE.Oszlopnév(oszlopmax) + "5");
                MyE.Vastagkeret(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                MyE.Betű(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString(), false, true, true);
                // Oszlop szélesség beállítás
                MyE.Oszlopszélesség(MunkaLap, "C:" + MyE.Oszlopnév(oszlopmax));

                // Alsó összesítés és átlag
                Havi_Összesítő_rész(oszlopmax);
                MyE.Aktív_Cella(MunkaLap, "A1");
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
                MyE.Link_beillesztés("Kimutatás", "A1", "Tartalom");

                string munkalap_adat = "Adatok 1";
                string balfelső = "A5";
                string jobbalsó = "R" + alsóPanels4.Text.Trim();
                string kimutatás_Munkalap = "Kimutatás";
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

                MyE.Kimutatás_Fő(munkalap_adat, balfelső, jobbalsó, kimutatás_Munkalap, Kimutatás_cella, Kimutatás_név
                , összesítNév, sorNév, oszlopNév, SzűrőNév);
                MyE.SzövegIrány("Kimutatás", "9:9", 90);
                MyE.Oszlopszélesség("Kimutatás", "B:J");
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
                string MunkaLap = "Kocsiszín_1";
                MyE.Munkalap_aktív(MunkaLap);
                MyE.Link_beillesztés(MunkaLap, "A1", "Tartalom");
                Napok_kiírása();
                MunkaVHétvége();
                string szöveg = "Kocsiszíni kiadási darabszámok";
                MyE.Kiir(szöveg, MyE.Oszlopnév(3) + 3.ToString());


                Kezelő_kiegészítő_telephely KézKiegTeleph = new Kezelő_kiegészítő_telephely();
                List<Adat_kiegészítő_telephely> AdatokKiegTeleph = KézKiegTeleph.Lista_adatok();

                int oszlopmax = 0, eleje;
                Holtart_Be(hónapnap + 1);
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
                            MyE.Kiir(rekordkieg.Telephelynév, MyE.Oszlopnév(pj) + 4.ToString());

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
                                MyE.Kiir(Elem.Altípus, MyE.Oszlopnév(pj) + 5.ToString());
                                MyE.Kiir(érték.ToString(), MyE.Oszlopnév(pj) + (sor + 5).ToString());
                            }
                            pj += 1;
                            MyE.Kiir("Összesen", MyE.Oszlopnév(pj) + 5.ToString());
                            pj += 1;
                        }
                    }
                }
                MyE.Kiir("Összesen", MyE.Oszlopnév(pj) + 4.ToString());
                oszlopmax = pj;

                // Összesítések
                // A-B Oszlop formázása
                MyE.Oszlopszélesség(MunkaLap, "B:B", 2);
                MyE.Oszlopszélesség(MunkaLap, "A:A");
                MyE.Rácsoz("A4:B" + (hónapnap + 5).ToString());
                MyE.Vastagkeret("A4:B" + (hónapnap + 5).ToString());
                // állomány felirat
                MyE.Egyesít(MunkaLap, MyE.Oszlopnév(3) + "3:" + MyE.Oszlopnév(oszlopmax) + "3");
                MyE.Vastagkeret(MyE.Oszlopnév(3) + "3:" + MyE.Oszlopnév(oszlopmax) + "3");


                eleje = 3;
                for (int j = 3; j <= oszlopmax; j++)
                {
                    if (MyE.Beolvas(MyE.Oszlopnév(j) + "5") == "Összesen")
                    {
                        for (int i = 1; i <= hónapnap; i++)
                        {
                            MyE.Kiir("=SUM(RC[-" + (j - eleje).ToString() + "]:RC[-1])", MyE.Oszlopnév(j) + (i + 5).ToString());
                            Holtart.Value = i;
                        }
                        MyE.Egyesít(MunkaLap, MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(j) + "4");
                        MyE.Rácsoz(MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(j) + (hónapnap + 5).ToString());
                        MyE.Vastagkeret(MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(j) + (hónapnap + 5).ToString());
                        MyE.Vastagkeret(MyE.Oszlopnév(eleje) + "4:" + MyE.Oszlopnév(j) + "5");
                        MyE.Betű(MyE.Oszlopnév(j) + "5:" + MyE.Oszlopnév(j) + (hónapnap + 5).ToString(), false, false, true);
                        MyE.Oszlopszélesség(MunkaLap, MyE.Oszlopnév(eleje) + ":" + MyE.Oszlopnév(j));

                        eleje = j + 1;
                    }
                }
                // ha csak egy főkategória volt
                szöveg = "=";
                for (int j = 3; j <= oszlopmax; j++)
                {
                    if (MyE.Beolvas(MyE.Oszlopnév(j) + "5") == "Összesen")
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
                    MyE.Kiir(szöveg, MyE.Oszlopnév(oszlopmax) + (i + 5).ToString());
                    Holtart_Lép();
                }
                // megformázzuk
                MyE.Rácsoz("c4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                MyE.Vastagkeret("c4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                MyE.Vastagkeret("c4:" + MyE.Oszlopnév(oszlopmax) + "5");
                MyE.Vastagkeret(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString());
                MyE.Betű(MyE.Oszlopnév(oszlopmax) + "4:" + MyE.Oszlopnév(oszlopmax) + (hónapnap + 5).ToString(), false, true, true);
                // Oszlop szélesség beállítás
                MyE.Oszlopszélesség(MunkaLap, "C:" + MyE.Oszlopnév(oszlopmax));

                // Alsó összesítés és átlag
                Havi_Összesítő_rész(oszlopmax);
                MyE.Aktív_Cella(MunkaLap, "A1");
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
                    MyE.Kiir(ideig.ToString("yyyy.MM.dd"), "a" + (ki + 5).ToString());
                    MyE.Kiir("3", "b" + (ki + 5).ToString());
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
                    DateTime melyiknap = MyE.Beolvas("a" + (i + 5)).ToÉrt_DaTeTime();
                    Adat_Forte_Kiadási_Adatok rekord = (from a in AdatokFortekiad
                                                        where a.Dátum == melyiknap
                                                        select a).FirstOrDefault();
                    if (rekord != null)
                    {
                        if (rekord.Munkanap == 0)
                            MyE.Kiir("0", "b" + (i + 5).ToString());
                        else
                            MyE.Kiir("1", "b" + (i + 5).ToString());
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
                MyE.Kiir("Hétköznap", "a40");
                MyE.Kiir("Összesen", "a41");
                MyE.Kiir("Átlag", "a42");
                MyE.Kiir("Hétvége", "a44");
                MyE.Kiir("Összesen", "a45");
                MyE.Kiir("Átlag", "a46");
                MyE.Kiir("Havi", "a48");
                MyE.Kiir("Összesen", "a49");
                MyE.Kiir("Átlag", "a50");

                // megszámoljuk hány munkanap van
                int hétköznapdb = 0;
                int hétvégedb = 0;

                for (int i = 6; i <= hónapnap + 5; i++)
                {
                    if (!int.TryParse(MyE.Beolvas("b" + i.ToString()), out int kód))
                        kód = 0;

                    if (kód == 0)
                    {
                        hétköznapdb += 1;
                    }
                    else
                    {
                        hétvégedb += 1;
                        MyE.Háttérszín("a" + i.ToString() + ":" + MyE.Oszlopnév(oszlopmax_) + i.ToString(), Color.GreenYellow);
                    }
                }
                szöveg = "=";
                szöveg1 = "=";

                for (int i = 6; i <= hónapnap + 5; i++)
                {
                    if (!int.TryParse(MyE.Beolvas("b" + i.ToString()), out int kód))
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
                    MyE.Kiir(hétköznapdb.ToString(), MyE.Oszlopnév(j) + "40");
                    MyE.Kiir(szöveg, MyE.Oszlopnév(j) + "41");
                    MyE.Kiir("=R[-1]C/R[-2]C", MyE.Oszlopnév(j) + "42");

                    // hétvége
                    MyE.Kiir(hétvégedb.ToString(), MyE.Oszlopnév(j) + "44");
                    MyE.Kiir(szöveg1, MyE.Oszlopnév(j) + "45");
                    MyE.Kiir("=R[-1]C/R[-2]C", MyE.Oszlopnév(j) + "46");

                    // összesen
                    MyE.Kiir((hétvégedb + hétköznapdb).ToString(), MyE.Oszlopnév(j) + "48");
                    MyE.Kiir("=SUM(R[-43]C:R[-13]C)", MyE.Oszlopnév(j) + "49");
                    MyE.Kiir("=R[-1]C/R[-2]C", MyE.Oszlopnév(j) + "50");

                }
                MyE.Háttérszín("a44:" + MyE.Oszlopnév(oszlopmax_) + "46", Color.GreenYellow);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Holtartok

        void Holtart_Be(int maximum = 20)
        {
            Holtart.Maximum = maximum;
            Holtart.Visible = true;
            Holtart.Value = 1;
        }

        void Holtart_Lép()
        {
            Holtart.Value++;
            if (Holtart.Value >= Holtart.Maximum) Holtart.Value = 1;
        }

        void Holtart_Ki()
        {
            Holtart.Visible = false;
        }

        #endregion

        #region Listák
        private void Listák_Feltöltés()
        {
            Személyzet_Lista_Feltöltése();
            TípusCsere_Lista_Feltöltése();
            Kiadás_Lista_Feltöltése();
            Forte_Lista_Feltöltése();
            TípusLista_Feltöltése();

            hónapnap = MyF.Hónap_hossza(Dátum.Value);
            hónaputolsónapja = MyF.Hónap_utolsónapja(Dátum.Value);
            hónapelsőnapja = MyF.Hónap_elsőnapja(Dátum.Value);
            ElőzőDátum = Dátum.Value;
        }

        private void Kiadás_Lista_Feltöltése()
        {
            try
            {
                AdatokKiad.Clear();
                string hely = $@"{Application.StartupPath}\főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_kiadási_adatok.mdb";
                if (!File.Exists(hely)) throw new HibásBevittAdat($"Nincs {Dátum.Value.Year} évnek megfelelő adat.");
                string jelszó = "pozsi";
                string szöveg = "SELECT * FROM kiadástábla";

                AdatokKiad = KézKiad.Lista_adatok(hely, jelszó, szöveg);
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

        private void Személyzet_Lista_Feltöltése()
        {
            try
            {
                AdatokSzem.Clear();
                string hely = $@"{Application.StartupPath}\főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_személyzet_adatok.mdb";
                if (!File.Exists(hely)) throw new HibásBevittAdat($"Nincs {Dátum.Value:yyyy} évnek megfelelő adat.");
                string jelszó = "pozsi";

                string szöveg = $"SELECT * FROM személyzettábla ";
                AdatokSzem = KézSzem.Lista_adatok(hely, jelszó, szöveg);

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

        private void TípusCsere_Lista_Feltöltése()
        {
            try
            {
                AdatokTípCsere.Clear();
                string hely = $@"{Application.StartupPath}\főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_típuscsere_adatok.mdb";
                string jelszó = "pozsi";
                string szöveg = $"SELECT * FROM típuscseretábla";
                AdatokTípCsere = KézTípCsere.Lista_adatok(hely, jelszó, szöveg);
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


        private void TípusLista_Feltöltése()
        {
            try
            {
                Adatokkiegtípusal.Clear();

                string hely = Application.StartupPath + @"\főmérnökség\adatok\Kiegészítő.mdb";
                string jelszó = "Mocó";

                string szöveg = $"SELECT * FROM típusaltípustábla";
                Adatokkiegtípusal = Kézkiegtípusal.Lista_Adatok(hely, jelszó, szöveg);
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