using System;
using System.Collections.Generic;
using System.Drawing;
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

    public partial class Ablak_Fő_Napiadatok
    {
        string Gyökér_telephely = "";
        string SzolgálatNév = "";
        string Vál_Telephely = "";
        string Főkategória = "";
        string Típus = "";
        string Altípus = "";
        int VálasztottNap = 0;
        int Telephelyekszáma = 0;

        readonly Kezelő_Kiegészítő_Típusrendezéstábla KézTipRend = new Kezelő_Kiegészítő_Típusrendezéstábla();
        readonly Kezelő_Kiegészítő_Szolgálattelepei KézSzolgTelep = new Kezelő_Kiegészítő_Szolgálattelepei();
        readonly Kezelő_kiegészítő_telephely KézTelep = new Kezelő_kiegészítő_telephely();
        readonly Kezelő_FőKiadási_adatok KézKiadás = new Kezelő_FőKiadási_adatok();
        readonly Kezelő_Személyzet_Adatok KézSzeméyzet = new Kezelő_Személyzet_Adatok();
        readonly Kezelő_Típuscsere_Adatok KézTípuscsere = new Kezelő_Típuscsere_Adatok();
        readonly Kezelő_Kiadás_Összesítő KézTelepKiadás = new Kezelő_Kiadás_Összesítő();
        readonly Kezelő_Forte_Kiadási_Adatok KézFőForte = new Kezelő_Forte_Kiadási_Adatok();


        List<Adat_Kiegészítő_Típusrendezéstábla> AdatokTipRend = new List<Adat_Kiegészítő_Típusrendezéstábla>();
        List<Adat_Kiegészítő_Szolgálattelepei> AdatokSzolgTelep = new List<Adat_Kiegészítő_Szolgálattelepei>();
        List<Adat_kiegészítő_telephely> AdatokTelep = new List<Adat_kiegészítő_telephely>();
        List<Adat_FőKiadási_adatok> AdatokKiadási = new List<Adat_FőKiadási_adatok>();
        List<Adat_Személyzet_Adatok> AdatokSzemélyzet = new List<Adat_Személyzet_Adatok>();
        List<Adat_Típuscsere_Adatok> AdatokTípuscsere = new List<Adat_Típuscsere_Adatok>();
        List<Adat_Kiadás_összesítő> AdatokKiadásTelep = new List<Adat_Kiadás_összesítő>();
        public Ablak_Fő_Napiadatok()
        {
            InitializeComponent();
        }


        private Button Telephelygomb;
        string Munkanap = "Munkanap";

        private void Ablak_Fő_Napiadatok_Load(object sender, EventArgs e)
        {
            try
            {
                TáblákLétrehozása();
                Dátum.Value = DateTime.Today;
                Dátum.MaxDate = DateTime.Today;
                Listák_Töltése();
                Táblaalaphelyzet();
                Gombokfel();
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

        private void TáblákLétrehozása()
        {
            string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{DateTime.Today.Year}";
            if (!File.Exists(hely)) Directory.CreateDirectory(hely);

            // éves kiadási darabok
            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{DateTime.Today.Year}\{DateTime.Today.Year}_kiadási_adatok.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kiadásiösszesítőfőmérnöktábla(hely);

            // személyzet hiány tábla
            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{DateTime.Today.Year}\{DateTime.Today.Year}_személyzet_adatok.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kiadásiszemélyzetfőmérnöktábla(hely);

            // típuscsere tábla
            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{DateTime.Today.Year}\{DateTime.Today.Year}_típuscsere_adatok.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kiadásitípuscserefőmérnöktábla(hely);


            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{DateTime.Today.Year}\{DateTime.Today.Year}_fortekiadási_adatok.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Fortekiadásifőmtábla(hely);
        }


        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;

                // ide kell az összes gombot tenni amit szabályozni akarunk false
                Command1.Enabled = false;
                Command4.Enabled = false;
                // csak főmérnökségi belépéssel van módosítás
                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {
                    Command1.Visible = true;
                    Command4.Visible = true;
                }
                else
                {
                    Command1.Visible = false;
                    Command4.Visible = false;
                }

                melyikelem = 184;
                // módosítás 1
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Command1.Enabled = true;
                    Command4.Enabled = true;
                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                // módosítás 3
                { }
                if (MyF.Vanjoga(melyikelem, 3)) { }
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
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Főmérnökség_napi_rögzítés.html";
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

        #region Tábla 
        private void Táblaalaphelyzet()
        {
            Tábla.Top = 80;
            Tábla.Left = 365;

            Tábla1.Top = 80;
            Tábla1.Left = 365;

            Tábla2.Top = 80;
            Tábla2.Left = 365;

            Tábla1.Visible = false;
            Tábla2.Visible = false;
            Tábla.Visible = true;

            Label6.BackColor = Color.Green;
            Label7.BackColor = Color.Black;
            Label8.BackColor = Color.Black;
        }


        private void Táblázatlistázás()
        {
            try
            {

                string hely = $@"{Gyökér_telephely}\adatok\főkönyv\kiadás{Dátum.Value.Year}.mdb";
                if (!File.Exists(hely)) return;
                string jelszó = "plédke";
                string szöveg = "SELECT * FROM tábla";

                Kezelő_Kiadás_Összesítő KézKiadás = new Kezelő_Kiadás_Összesítő();
                List<Adat_Kiadás_összesítő> AdatokKiad = KézKiadás.Lista_adatok(hely, jelszó, szöveg);
                List<Adat_Kiadás_összesítő> AdatokKiadás = (from a in AdatokKiad
                                                            where a.Dátum == Dátum.Value
                                                            orderby a.Napszak, a.Típus
                                                            select a).ToList();

                List<Adat_Forte_Kiadási_Adatok> AdatokFőForte = KézFőForte.Lista_Adatok(Dátum.Value.Year);

                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 13;

                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Napszak";
                Tábla.Columns[0].Width = 90;
                Tábla.Columns[1].HeaderText = "Típus";
                Tábla.Columns[1].Width = 100;
                Tábla.Columns[2].HeaderText = "Elt.";
                Tábla.Columns[2].Width = 50;
                Tábla.Columns[3].HeaderText = "Kiadási igény";
                Tábla.Columns[3].Width = 70;
                Tábla.Columns[4].HeaderText = "Forgalomban";
                Tábla.Columns[4].Width = 110;
                Tábla.Columns[5].HeaderText = "Tartalék";
                Tábla.Columns[5].Width = 80;
                Tábla.Columns[6].HeaderText = "Kocsiszíni";
                Tábla.Columns[6].Width = 100;
                Tábla.Columns[7].HeaderText = "Félreáll.";
                Tábla.Columns[7].Width = 80;
                Tábla.Columns[8].HeaderText = "Főjavítás";
                Tábla.Columns[8].Width = 80;
                Tábla.Columns[9].HeaderText = "Összesen";
                Tábla.Columns[9].Width = 100;
                Tábla.Columns[10].HeaderText = "Személyzet hiány";
                Tábla.Columns[10].Width = 100;
                Tábla.Columns[11].HeaderText = "Előzőnapi";
                Tábla.Columns[11].Width = 80;
                Tábla.Columns[12].HeaderText = "Munkanap";
                Tábla.Columns[12].Width = 110;

                Holtart.Be(AdatokKiadás.Count + 1);

                foreach (Adat_Kiadás_összesítő rekord in AdatokKiadás)
                {
                    Tábla.RowCount++;
                    int i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = rekord.Napszak;
                    Tábla.Rows[i].Cells[1].Value = rekord.Típus;

                    long Kiadás = 0;

                    List<Adat_Forte_Kiadási_Adatok> Elemek = (from a in AdatokFőForte
                                                              where a.Dátum == Dátum.Value
                                                              && a.Napszak == rekord.Napszak
                                                              && a.Telephely == LabelTelephely.Text.Trim()
                                                              && a.Típus == rekord.Típus
                                                              select a).ToList();
                    foreach (Adat_Forte_Kiadási_Adatok Elem in Elemek)
                    {
                        if (Elem.Munkanap == 0)
                            Tábla.Rows[i].Cells[12].Value = "Munkanap";
                        else
                            Tábla.Rows[i].Cells[12].Value = "Hétvége";

                        Kiadás += Elem.Kiadás;
                    }

                    Tábla.Rows[i].Cells[2].Value = rekord.Forgalomban - Kiadás;
                    Tábla.Rows[i].Cells[3].Value = Kiadás;
                    Tábla.Rows[i].Cells[4].Value = rekord.Forgalomban;
                    if (Kiadás > rekord.Forgalomban) Tábla.Rows[i].Cells[4].Style.BackColor = Color.Red;
                    if (Kiadás < rekord.Forgalomban) Tábla.Rows[i].Cells[4].Style.BackColor = Color.CornflowerBlue;

                    Tábla.Rows[i].Cells[5].Value = rekord.Tartalék + rekord.Személyzet;
                    Tábla.Rows[i].Cells[6].Value = rekord.Kocsiszíni;
                    Tábla.Rows[i].Cells[7].Value = rekord.Félreállítás;
                    Tábla.Rows[i].Cells[8].Value = rekord.Főjavítás;
                    int állomány = rekord.Forgalomban + rekord.Tartalék + rekord.Kocsiszíni + rekord.Félreállítás + rekord.Főjavítás + rekord.Személyzet;
                    Tábla.Rows[i].Cells[9].Value = állomány;
                    Tábla.Rows[i].Cells[10].Value = rekord.Személyzet;

                    Adat_Kiadás_összesítő Tegnap = (from a in AdatokKiad
                                                    where a.Dátum == Dátum.Value.AddDays(-1) && a.Napszak == rekord.Napszak && a.Típus == rekord.Típus
                                                    select a).FirstOrDefault();
                    if (Tegnap != null)
                    {
                        int állománytegnap = Tegnap.Forgalomban + Tegnap.Tartalék + Tegnap.Kocsiszíni + Tegnap.Félreállítás + Tegnap.Főjavítás + Tegnap.Személyzet;
                        Tábla.Rows[i].Cells[11].Value = állománytegnap;
                        if (állomány < állománytegnap)
                            Tábla.Rows[i].Cells[11].Style.BackColor = Color.CornflowerBlue;
                        if (állomány > állománytegnap)
                            Tábla.Rows[i].Cells[11].Style.BackColor = Color.Red;
                    }
                    Holtart.Lép();
                }


                Tábla.Visible = true;
                Tábla.Refresh();

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


        private void Táblázatlistázásszemélyzet()
        {
            try
            {
                string hely = $@"{Gyökér_telephely}\adatok\főkönyv\személyzet{Dátum.Value.Year}.mdb";
                if (!File.Exists(hely)) return;
                string jelszó = "plédke";
                string szöveg = "SELECT * FROM tábla";

                Kezelő_Főkönyv_Személyzet KézSzem = new Kezelő_Főkönyv_Személyzet();
                List<Adat_Főkönyv_Személyzet> AdatokSzem = KézSzem.Lista_adatok(hely, jelszó, szöveg);

                List<Adat_Főkönyv_Személyzet> Adatok = (from a in AdatokSzem
                                                        where a.Dátum == Dátum.Value
                                                        orderby a.Napszak, a.Típus
                                                        select a).ToList();

                Tábla1.Rows.Clear();
                Tábla1.Columns.Clear();
                Tábla1.Refresh();
                Tábla1.Visible = false;
                Tábla1.ColumnCount = 7;

                // fejléc elkészítése
                Tábla1.Columns[0].HeaderText = "Dátum";
                Tábla1.Columns[0].Width = 120;
                Tábla1.Columns[1].HeaderText = "Napszak";
                Tábla1.Columns[1].Width = 100;
                Tábla1.Columns[2].HeaderText = "Típus";
                Tábla1.Columns[2].Width = 100;
                Tábla1.Columns[3].HeaderText = "Viszonylat";
                Tábla1.Columns[3].Width = 100;
                Tábla1.Columns[4].HeaderText = "Forgalmi";
                Tábla1.Columns[4].Width = 100;
                Tábla1.Columns[5].HeaderText = "Indulási idő";
                Tábla1.Columns[5].Width = 100;
                Tábla1.Columns[6].HeaderText = "Pályaszám";
                Tábla1.Columns[6].Width = 100;

                Holtart.Be(Adatok.Count + 1);
                int i;
                foreach (Adat_Főkönyv_Személyzet rekord in Adatok)
                {
                    Tábla1.RowCount++;
                    i = Tábla1.RowCount - 1;
                    Tábla1.Rows[i].Cells[0].Value = rekord.Dátum.ToString("yyyy.MM.dd");
                    Tábla1.Rows[i].Cells[1].Value = rekord.Napszak;
                    Tábla1.Rows[i].Cells[2].Value = rekord.Típus;
                    Tábla1.Rows[i].Cells[3].Value = rekord.Viszonylat;
                    Tábla1.Rows[i].Cells[4].Value = rekord.Forgalmiszám;
                    Tábla1.Rows[i].Cells[5].Value = rekord.Tervindulás.ToString("HH:mm");
                    Tábla1.Rows[i].Cells[6].Value = rekord.Azonosító;
                    Holtart.Lép();
                }

                Tábla1.Refresh();
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


        private void Táblázatlistázástípuscsere()
        {
            try
            {

                string hely = $@"{Gyökér_telephely}\adatok\főkönyv\típuscsere{Dátum.Value.Year}.mdb";
                if (!File.Exists(hely)) return;
                string jelszó = "plédke";
                string szöveg = "SELECT * FROM típuscseretábla";

                Kezelő_Főkönyv_Típuscsere KézCsere = new Kezelő_Főkönyv_Típuscsere();
                List<Adat_FőKönyv_Típuscsere> AdatokCsere = KézCsere.Lista_adatok(hely, jelszó, szöveg);

                List<Adat_FőKönyv_Típuscsere> Adatok = (from a in AdatokCsere
                                                        where a.Dátum == Dátum.Value
                                                        orderby a.Napszak, a.Típuselőírt, a.Viszonylat, a.Forgalmiszám
                                                        select a).ToList();

                Tábla2.Rows.Clear();
                Tábla2.Columns.Clear();
                Tábla2.Refresh();
                Tábla2.Visible = false;
                Tábla2.ColumnCount = 8;

                // fejléc elkészítése
                Tábla2.Columns[0].HeaderText = "Dátum";
                Tábla2.Columns[0].Width = 100;
                Tábla2.Columns[1].HeaderText = "Napszak";
                Tábla2.Columns[1].Width = 100;
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

                Holtart.Be(Adatok.Count + 1);

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
                    Tábla2.Rows[i].Cells[6].Value = rekord.Tervindulás.ToString("hh:mm");
                    Tábla2.Rows[i].Cells[7].Value = rekord.Azonosító;
                    Holtart.Lép();
                }

                Tábla2.Refresh();
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


        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int oszlop, sor;
                if (Gyökér_telephely.Trim() == "")
                {
                    // a rögzítési  tábla esetén kiírjuk a telephely adatait
                    if (SzolgálatNév.Trim() == "")
                    {
                        if (Tábla.Columns.Count == 0) return;
                        // ma vagy a múlt esetén
                        DateTime ideig = new DateTime(Dátum.Value.Year, Dátum.Value.Month, Tábla.Rows[e.RowIndex].Cells[0].Value.ToÉrt_Int());
                        if (ideig <= DateTime.Today)
                        {
                            oszlop = e.ColumnIndex;
                            sor = e.RowIndex;
                            Vál_Telephely = Tábla.Columns[oszlop].HeaderText.Trim();
                            VálasztottNap = Tábla.Rows[sor].Cells[0].Value.ToÉrt_Int();

                            Dátum.Value = new DateTime(Dátum.Value.Year, Dátum.Value.Month, VálasztottNap);

                            SzolgálatNév = "";

                            Adat_Kiegészítő_Szolgálattelepei Elem = (from a in AdatokSzolgTelep
                                                                     where a.Telephelynév == Vál_Telephely.Trim()
                                                                     select a).FirstOrDefault();
                            if (Elem != null)
                                SzolgálatNév = Elem.Szolgálatnév.Trim();


                            Gyökér_telephely = $@"{Application.StartupPath}\{Vál_Telephely}";
                            LabelTelephely.Text = Vál_Telephely;

                            Táblázatlistázás();
                            Táblázatlistázásszemélyzet();
                            Táblázatlistázástípuscsere();
                            Rögzítgomb();
                            Label6_eseménye();
                        }
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
        #endregion


        #region Telephelyek gombok
        private void Gombokfel()
        {
            try
            {
                Panel3.Controls.Clear();
                Holtart.Be(AdatokTelep.Count + 1);
                Telephelyekszáma = 0;
                int j = 1;
                int k = 1;

                foreach (Adat_kiegészítő_telephely rekord in AdatokTelep)
                {
                    Telephelyekszáma++; ;
                    Telephelygomb = new Button
                    {
                        Location = new Point(10 + 170 * (k - 1), 10 + (j - 1) * 40),
                        Size = new Size(160, 35),
                        Name = $"Telephelyek_{Telephelyekszáma}",
                        Text = rekord.Telephelynév.Trim(),
                        // alapszín szürke
                        BackColor = Color.Cornsilk
                    };
                    string hely = $@"{Application.StartupPath}\{rekord.Telephelynév.Trim()}\adatok\főkönyv\kiadás{Dátum.Value.Year}.mdb";
                    if (File.Exists(hely))
                    {
                        TelepKidásiListaFeltöltés(hely);

                        Adat_Kiadás_összesítő ElemKiad = (from a in AdatokKiadásTelep
                                                          where a.Dátum.ToShortDateString() == Dátum.Value.ToShortDateString() &&
                                                          a.Napszak == (Délelőtt.Checked ? "de" : "du")
                                                          select a).FirstOrDefault();

                        if (ElemKiad != null)
                            Telephelygomb.BackColor = Color.Green;
                        else
                            Telephelygomb.BackColor = Color.Red;

                    }
                    Telephelygomb.Visible = true;

                    Telephelygomb.MouseDown += Telephelyre_MouseDown;

                    Panel3.Controls.Add(Telephelygomb);
                    j += 1;
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


        private void Telephelyre_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Tábla.Rows.Clear();
                Tábla1.Rows.Clear();
                Tábla2.Rows.Clear();
                if ((sender as Button).BackColor == Color.Green)
                {
                    LabelTelephely.Text = (sender as Button).Text.Trim();
                    Gyökér_telephely = $@"{Application.StartupPath}\{LabelTelephely.Text}";

                    // megkeressük, hogy melyik szolgálat
                    SzolgálatNév = (from a in AdatokSzolgTelep
                                    where a.Telephelynév == LabelTelephely.Text.Trim()
                                    select a.Szolgálatnév).FirstOrDefault() ?? "";

                    Vál_Telephely = LabelTelephely.Text.Trim();
                    Táblázatlistázás();
                    Táblázatlistázásszemélyzet();
                    Táblázatlistázástípuscsere();
                    Rögzítgomb();
                    Label6_eseménye();
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
        #endregion


        #region Gombok
        private void Délelőtt_Click(object sender, EventArgs e)
        {
            Listázás();
            Gombokfel();
            Rögzítgomb();
        }


        private void Délután_Click(object sender, EventArgs e)
        {
            Listázás();
            Gombokfel();
            Rögzítgomb();
        }


        private void Dátum_ValueChanged(object sender, EventArgs e)
        {
            Gombokfel();
            Rögzítgomb();
            Munkanap_lekérdezés();
        }


        private void Munkanap_lekérdezés()
        {
            try
            {
                List<Adat_Forte_Kiadási_Adatok> Adatok = KézFőForte.Lista_Adatok(Dátum.Value.Year);
                Adat_Forte_Kiadási_Adatok Elem = Adatok.FirstOrDefault(a => a.Dátum == Dátum.Value);
                Munkanap = "Munkanap";
                if (Elem != null)
                    Munkanap = "Munkanap";
                else
                    Munkanap = "Hétvége";
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


        private void Label7_Click(object sender, EventArgs e)
        {
            // személyzet hiány tábla
            Táblaalaphelyzet();
            Tábla1.Visible = true;
            Tábla2.Visible = false;
            Tábla.Visible = false;

            Label6.BackColor = Color.Black;
            Label7.BackColor = Color.Green;
            Label8.BackColor = Color.Black;
        }


        private void Label6_Click(object sender, EventArgs e)
        {
            Label6_eseménye();
        }


        private void Label6_eseménye()
        {
            Táblaalaphelyzet();
            Tábla1.Visible = false;
            Tábla2.Visible = false;
            Tábla.Visible = true;
            Label6.BackColor = Color.Green;
            Label7.BackColor = Color.Black;
            Label8.BackColor = Color.Black;
        }


        private void Label8_Click(object sender, EventArgs e)
        {
            Táblaalaphelyzet();
            Tábla1.Visible = false;
            Tábla2.Visible = true;
            Tábla.Visible = false;
            Label6.BackColor = Color.Black;
            Label7.BackColor = Color.Black;
            Label8.BackColor = Color.Green;
        }


        private void Lista_Click(object sender, EventArgs e)
        {
            Listázás();
        }


        private void Listázás()
        {
            if (Gyökér_telephely == null || Gyökér_telephely == "") return;

            Táblázatlistázás();
            Táblázatlistázásszemélyzet();
            Táblázatlistázástípuscsere();
        }


        private void Rögzítgomb()
        {
            try
            {
                FőmérnökségListaFeltöltés();
                Adat_FőKiadási_adatok Elem = (from a in AdatokKiadási
                                              where a.Napszak == (Délelőtt.Checked ? "de" : "du") &&
                                               a.Dátum.ToShortDateString() == Dátum.Value.ToShortDateString() &&
                                               a.Telephely == Vál_Telephely.Trim()
                                              select a).FirstOrDefault();

                if (Elem != null)
                {
                    // ha volt rögzítve
                    Command1.BackColor = Color.CornflowerBlue;
                    Command1.ForeColor = Color.Black;
                    Command4.Enabled = true;
                }
                else
                {
                    // ha nem volt rögzítve
                    Command1.BackColor = Color.Green;
                    Command1.ForeColor = Color.White;
                    Command4.Enabled = false;
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


        private void HiányzóAlap_Click(object sender, EventArgs e)
        {
            try
            {
                LabelTelephely.Text = "";
                Command4.Enabled = false;
                Gyökér_telephely = "";
                Vál_Telephely = "";
                SzolgálatNév = "nem";

                int hónapnap = DateTime.DaysInMonth(Dátum.Value.Year, Dátum.Value.Month);
                DateTime hónaputolsónapja = new DateTime(Dátum.Value.Year, Dátum.Value.Month, hónapnap);

                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = Telephelyekszáma + 1;
                Tábla.RowCount = hónapnap;
                Tábla.Columns[0].HeaderText = "Dátum";
                Tábla.Columns[0].Width = 80;

                for (int i = 0; i <= hónapnap - 1; i++)
                    Tábla.Rows[i].Cells[0].Value = i + 1;

                Holtart.Be(Telephelyekszáma + 1);

                for (int oszlop = 1; oszlop <= Telephelyekszáma; oszlop++)
                {
                    Tábla.Columns[oszlop].HeaderText = AdatokTelep[oszlop - 1].Telephelynév;
                    Tábla.Columns[oszlop].Width = 110;
                    // kitöltjük a táblázatot 0-val

                    for (int sor = 0; sor <= Tábla.Rows.Count - 1; sor++)
                    {
                        Tábla.Rows[sor].Cells[oszlop].Value = 0;
                        Tábla.Rows[sor].Cells[oszlop].Style.BackColor = Color.Red;
                    }
                }
                Kezelő_Kiadás_Összesítő KKö_kéz = new Kezelő_Kiadás_Összesítő();
                List<Adat_Kiadás_összesítő> Adatok = new List<Adat_Kiadás_összesítő>();

                for (int oszlop = 1; oszlop < Tábla.Columns.Count; oszlop++)
                {
                    string hely = $@"{Application.StartupPath}\{AdatokTelep[oszlop - 1].Telephelynév}\adatok\főkönyv\kiadás{Dátum.Value.Year}.mdb";
                    string jelszó = "plédke";

                    Adatok.Clear();
                    // ha létezik a fájl akkor kiolvaasuk
                    if (File.Exists(hely))
                    {
                        DateTime Hónapelső = MyF.Hónap_elsőnapja(Dátum.Value);
                        string szöveg = $@"SELECT * FROM tábla where [dátum]>=#{Hónapelső:M-d-yy}# AND [dátum]<=#{hónaputolsónapja:M-d-yy}#";
                        if (Délelőtt.Checked)
                            szöveg += " AND napszak ='de'";
                        if (Délután.Checked == true)
                            szöveg += " AND napszak='du'";
                        szöveg += " order by dátum,napszak, típus";
                        Adatok = KKö_kéz.Lista_adatok(hely, jelszó, szöveg);

                        foreach (Adat_Kiadás_összesítő rekord in Adatok)
                        {
                            int aktnap = rekord.Dátum.Day;
                            Tábla.Rows[aktnap - 1].Cells[oszlop].Value = "1";
                            Tábla.Rows[aktnap - 1].Cells[oszlop].Style.BackColor = Color.Green;
                        }
                        Holtart.Lép();
                    }
                }
                Tábla.Refresh();
                Tábla.Visible = true;

                Label6_eseménye();
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


        private void HiányzóRögz_Click(object sender, EventArgs e)
        {
            try
            {
                LabelTelephely.Text = "";
                Command4.Enabled = false;
                Gyökér_telephely = "";
                Vál_Telephely = "";
                SzolgálatNév = "";

                int hónapnap = MyF.Hónap_hossza(Dátum.Value);
                DateTime hónaputolsónapja = MyF.Hónap_utolsónapja(Dátum.Value);

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_kiadási_adatok.mdb";
                if (!File.Exists(hely)) return;
                string jelszó = "pozsi";

                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = Telephelyekszáma + 1;
                Tábla.RowCount = hónapnap;
                Tábla.Columns[0].HeaderText = "Dátum";
                Tábla.Columns[0].Width = 80;


                for (int i = 0; i <= hónapnap - 1; i++)
                    Tábla.Rows[i].Cells[0].Value = i + 1;


                for (int oszlop = 1; oszlop <= Telephelyekszáma; oszlop++)
                {
                    Tábla.Columns[oszlop].HeaderText = AdatokTelep[oszlop - 1].Telephelynév;
                    Tábla.Columns[oszlop].Width = 110;
                    // kitöltjük a táblázatot 0-val

                    for (int sor = 0; sor <= Tábla.Rows.Count - 1; sor++)
                    {
                        Tábla.Rows[sor].Cells[oszlop].Value = 0;
                        Tábla.Rows[sor].Cells[oszlop].Style.BackColor = Color.Red;
                    }
                }

                Holtart.Be(Tábla.Columns.Count + 1);
                for (int oszlop = 1; oszlop <= Tábla.Columns.Count - 1; oszlop++)
                {
                    DateTime Hónapelső = MyF.Hónap_elsőnapja(Dátum.Value);
                    string szöveg = $@"SELECT * FROM kiadástábla WHERE [dátum]>=#{Hónapelső:M-d-yy}# AND [dátum]<=#{hónaputolsónapja:M-d-yy}#";
                    if (Délelőtt.Checked)
                        szöveg += " AND napszak ='de'";
                    if (Délután.Checked == true)
                        szöveg += " AND napszak='du'";
                    szöveg += " and telephely='" + AdatokTelep[oszlop - 1].Telephelynév + "'";
                    szöveg += " order by dátum,napszak, típus";

                    Kezelő_FőKiadási_adatok Kéz = new Kezelő_FőKiadási_adatok();
                    List<Adat_FőKiadási_adatok> Adatok = Kéz.Lista_adatok(hely, jelszó, szöveg);

                    foreach (Adat_FőKiadási_adatok rekord in Adatok)
                    {
                        int aktnap = rekord.Dátum.Day;
                        Tábla.Rows[aktnap - 1].Cells[oszlop].Value = "1";
                        Tábla.Rows[aktnap - 1].Cells[oszlop].Style.BackColor = Color.Green;
                    }
                    Holtart.Lép();
                }
                Tábla.Refresh();
                Tábla.Visible = true;

                Label6_eseménye();
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


        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.Visible == true & Tábla.Rows.Count <= 0) return;
                if (Tábla1.Visible == true & Tábla1.Rows.Count <= 0) return;
                if (Tábla2.Visible == true & Tábla2.Rows.Count <= 0) return;

                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Napi_adatok_" + Program.PostásNév + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + Dátum.Value.ToString("yyyyMMdd"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                if (Tábla.Visible) MyE.EXCELtábla(fájlexc, Tábla, false);
                if (Tábla1.Visible) MyE.EXCELtábla(fájlexc, Tábla1, false);
                if (Tábla2.Visible) MyE.EXCELtábla(fájlexc, Tábla2, false);
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


        #region Adatok rögzítése törlése

        private void Command1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Gyökér_telephely.Trim() == "") return;
                if (SzolgálatNév.Trim() == "") return;

                Napikiadásiadatokrögzítése();
                Napitípuscsererögzítése();
                Napiszemélyzethiányrögzítése();
                Rögzítgomb();

                MessageBox.Show("Az adatok rögzítve lettek!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                // töröljük a napi adatokat
                string hely = $@"{Application.StartupPath}\főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_kiadási_adatok.mdb";
                string jelszó = "pozsi";
                string szöveg;
                FőmérnökségListaFeltöltés();


                Adat_FőKiadási_adatok Elem = (from a in AdatokKiadási
                                              where a.Dátum.ToShortDateString() == Dátum.Value.ToShortDateString() &&
                                              a.Telephely == Vál_Telephely.Trim()
                                              select a).FirstOrDefault();
                if (Elem != null)
                {
                    szöveg = $"DELETE FROM kiadástábla WHERE [dátum]=#{Dátum.Value:M-d-yy}#";
                    szöveg += $" and telephely='{Vál_Telephely.Trim()}'";
                    MyA.ABtörlés(hely, jelszó, szöveg);
                }

                // töröljük a személyzethiányt
                hely = $@"{Application.StartupPath}\főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_személyzet_adatok.mdb";
                FőSzemélyzetListaFeltöltés();

                Adat_Személyzet_Adatok Elemszemélyzet = (from a in AdatokSzemélyzet
                                                         where a.Dátum.ToShortDateString() == Dátum.Value.ToShortDateString() &&
                                                         a.Telephely == Vál_Telephely.Trim()
                                                         select a).FirstOrDefault();
                if (Elemszemélyzet != null)
                {
                    szöveg = $"DELETE FROM személyzettábla WHERE [dátum]=#{Dátum.Value:M-d-yy}#";
                    szöveg += $" and telephely='{Vál_Telephely.Trim()}'";
                    MyA.ABtörlés(hely, jelszó, szöveg);
                }

                // töröljük a típuscseréket
                hely = $@"{Application.StartupPath}\főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_típuscsere_adatok.mdb";
                FőTípuscsereListaFeltöltés();

                Adat_Típuscsere_Adatok ElemTípuscsere = (from a in AdatokTípuscsere
                                                         where a.Dátum.ToShortDateString() == Dátum.Value.ToShortDateString() &&
                                                         a.Telephely == Vál_Telephely.Trim()
                                                         select a).FirstOrDefault();

                if (ElemTípuscsere != null)
                {
                    szöveg = $"DELETE FROM típuscseretábla WHERE [dátum]=#{Dátum.Value:M-d-yy}#";
                    szöveg += $" and telephely='{Vál_Telephely.Trim()}'";
                    MyA.ABtörlés(hely, jelszó, szöveg);
                }

                Rögzítgomb();
                MessageBox.Show("Az adatok törölve lettek!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);

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


        private void Napikiadásiadatokrögzítése()
        {
            try
            {
                string hely, jelszó, szöveg;
                hely = $@"{Application.StartupPath}\főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_kiadási_adatok.mdb";
                jelszó = "pozsi";
                FőmérnökségListaFeltöltés();

                List<string> SzövegGy = new List<string>();
                for (int sor = 0; sor < Tábla.Rows.Count; sor++)
                {
                    Kategórizál(sor);
                    // megnézzük, hogy van-e már ilyen adat rögzítve
                    Adat_FőKiadási_adatok Elem = (from a in AdatokKiadási
                                                  where a.Dátum.ToShortDateString() == Dátum.Value.ToShortDateString() &&
                                                  a.Napszak == Tábla.Rows[sor].Cells[0].Value.ToStrTrim() &&
                                                  a.Telephely == Vál_Telephely.Trim() &&
                                                  a.Telephelyitípus == Tábla.Rows[sor].Cells[1].Value.ToStrTrim()
                                                  select a).FirstOrDefault();

                    if (Elem != null)
                    {
                        // ha van ilyen akkor módosítjuk
                        szöveg = "UPDATE kiadástábla  SET ";
                        szöveg += $"Főkategória='{Főkategória.Trim()}', ";
                        szöveg += $"Típus='{Típus.Trim()}', ";
                        szöveg += $"Altípus='{Altípus.Trim()}', ";
                        szöveg += $"szolgálat='" + SzolgálatNév.Trim() + "', ";
                        szöveg += $"forgalomban=" + Tábla.Rows[sor].Cells[4].Value.ToStrTrim() + ", ";
                        szöveg += $"tartalék={int.Parse(Tábla.Rows[sor].Cells[5].Value.ToStrTrim()) - int.Parse(Tábla.Rows[sor].Cells[10].Value.ToStrTrim())}, ";
                        szöveg += $"kocsiszíni=" + Tábla.Rows[sor].Cells[6].Value.ToStrTrim() + ", ";
                        szöveg += $"félreállítás=" + Tábla.Rows[sor].Cells[7].Value.ToStrTrim() + ", ";
                        szöveg += $"főjavítás=" + Tábla.Rows[sor].Cells[8].Value.ToStrTrim() + ", ";
                        if (Munkanap.ToUpper().Trim() == "Munkanap".ToUpper())
                            szöveg += "munkanap=0, ";
                        else
                            szöveg += "munkanap=1, ";

                        szöveg += "kiadás=" + Tábla.Rows[sor].Cells[3].Value.ToStrTrim() + ", ";
                        szöveg += "személyzet=" + Tábla.Rows[sor].Cells[10].Value.ToStrTrim();
                        szöveg += " WHERE [dátum]=#" + Dátum.Value.ToString("M-d-yy") + "# ";
                        szöveg += " and napszak='" + Tábla.Rows[sor].Cells[0].Value.ToStrTrim() + "'";
                        szöveg += " and telephely='" + Vál_Telephely.ToStrTrim() + "'";
                        szöveg += " and telephelyitípus='" + Tábla.Rows[sor].Cells[1].Value.ToStrTrim() + "'";
                    }
                    else
                    {
                        // ha nincs akkor rögzítjük
                        szöveg = "INSERT INTO kiadástábla  (dátum, napszak, főkategória, típus, ";
                        szöveg += "altípus, telephely, szolgálat, telephelyitípus, ";
                        szöveg += "forgalomban, tartalék, kocsiszíni, félreállítás, ";
                        szöveg += "főjavítás, munkanap, kiadás, személyzet ) VALUES (";
                        szöveg += "'" + Dátum.Value.ToString("yyyy.MM.dd") + "', ";
                        szöveg += "'" + Tábla.Rows[sor].Cells[0].Value.ToStrTrim() + "', ";
                        szöveg += "'" + Főkategória.Trim() + "', ";
                        szöveg += "'" + Típus.Trim() + "', ";

                        szöveg += "'" + Altípus.Trim() + "', ";
                        szöveg += "'" + Vál_Telephely.Trim() + "', ";
                        szöveg += "'" + SzolgálatNév.Trim() + "', ";
                        szöveg += "'" + Tábla.Rows[sor].Cells[1].Value.ToStrTrim() + "', ";

                        szöveg += Tábla.Rows[sor].Cells[4].Value.ToStrTrim() + ", ";
                        szöveg += $"{int.Parse(Tábla.Rows[sor].Cells[5].Value.ToStrTrim()) - int.Parse(Tábla.Rows[sor].Cells[10].Value.ToStrTrim())}, ";
                        szöveg += Tábla.Rows[sor].Cells[6].Value.ToStrTrim() + ", ";
                        szöveg += Tábla.Rows[sor].Cells[7].Value.ToStrTrim() + ", ";

                        szöveg += Tábla.Rows[sor].Cells[8].Value.ToStrTrim() + ", ";
                        if (Munkanap.ToUpper().Trim() == "Munkanap".ToUpper())
                            szöveg += "0, ";
                        else
                            szöveg += "1, ";

                        szöveg += Tábla.Rows[sor].Cells[3].Value.ToStrTrim() + ", ";
                        szöveg += Tábla.Rows[sor].Cells[10].Value.ToStrTrim() + ") ";
                    }
                    SzövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
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


        private void Kategórizál(int sor)
        {
            try
            {

                Főkategória = "";
                Típus = "";
                Altípus = "";

                Adat_Kiegészítő_Típusrendezéstábla Elem = (from a in AdatokTipRend
                                                           where a.Telephely == LabelTelephely.Text.Trim() && a.Telephelyitípus == Tábla.Rows[sor].Cells[1].Value.ToStrTrim()
                                                           select a).FirstOrDefault();
                if (Elem != null)
                {
                    Főkategória = Elem.Főkategória;
                    Típus = Elem.Típus;
                    Altípus = Elem.AlTípus;
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


        private void Napiszemélyzethiányrögzítése()
        {
            try
            {
                string helytelep = $@"{Gyökér_telephely}\adatok\főkönyv\személyzet{Dátum.Value.Year}.mdb";
                string jelszótelep = "plédke";
                string szöveg;

                FőSzemélyzetListaFeltöltés();
                string hely = $@"{Application.StartupPath}\főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_személyzet_adatok.mdb";
                string jelszó = "pozsi";

                Adat_Személyzet_Adatok Elem = (from a in AdatokSzemélyzet
                                               where a.Napszak == (Délelőtt.Checked ? "de" : "du") &&
                                               a.Telephely == Vál_Telephely.ToStrTrim() &&
                                               a.Dátum.ToShortDateString() == Dátum.Value.ToShortDateString()
                                               select a).FirstOrDefault();

                // leellenőrizzük, hogy volt-e a napra már rögzítve, ha volt töröljük
                if (Elem != null)
                {
                    szöveg = $"DELETE FROM személyzettábla WHERE [dátum]=#{Dátum.Value:M-d-yy}#";

                    if (Délelőtt.Checked)
                        szöveg += " and napszak='de'";
                    else
                        szöveg += " and napszak='du'";

                    szöveg += $" and telephely='{Vál_Telephely.ToStrTrim()}'";
                    MyA.ABtörlés(hely, jelszó, szöveg);
                }

                szöveg = "SELECT * FROM tábla where [dátum]=#" + Dátum.Value.ToString("M-d-yy") + "#";

                if (Délelőtt.Checked)
                    szöveg += " and napszak='de'";
                else
                    szöveg += " and napszak='du'";

                Kezelő_Főkönyv_Személyzet KFK_Kéz = new Kezelő_Főkönyv_Személyzet();
                List<Adat_Főkönyv_Személyzet> Adatok = KFK_Kéz.Lista_adatok(helytelep, jelszótelep, szöveg);

                List<string> SzövegGy = new List<string>();
                foreach (Adat_Főkönyv_Személyzet rekord in Adatok)
                {
                    string újtípus = (from a in AdatokTipRend
                                      where a.Telephely == LabelTelephely.Text.Trim() && a.Telephelyitípus == rekord.Típus.Trim()
                                      select a.AlTípus).FirstOrDefault() ?? "?";

                    szöveg = "INSERT INTO személyzettábla (dátum, napszak, telephely, szolgálat, típus, viszonylat, forgalmiszám, tervindulás, azonosító ) VALUES (";
                    szöveg += "'" + rekord.Dátum.ToString("yyyy.MM.dd") + "', ";
                    szöveg += "'" + rekord.Napszak + "', ";
                    szöveg += "'" + Vál_Telephely.ToStrTrim() + "', ";
                    szöveg += "'" + SzolgálatNév.ToStrTrim() + "', ";
                    szöveg += "'" + újtípus.ToStrTrim() + "', ";
                    szöveg += "'" + rekord.Viszonylat + "', ";
                    szöveg += "'" + rekord.Forgalmiszám + "', ";
                    szöveg += "'" + rekord.Tervindulás.ToString("HH:mm") + "', ";
                    szöveg += "'" + rekord.Azonosító + "') ";

                    SzövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
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


        private void Napitípuscsererögzítése()
        {
            try
            {
                string helytelep = Gyökér_telephely + $@"\adatok\főkönyv\típuscsere{Dátum.Value.Year}.mdb";
                string jelszótelep = "plédke";

                string hely = $@"{Application.StartupPath}\főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_típuscsere_adatok.mdb";
                string jelszó = "pozsi";
                string szöveg = "SELECT * FROM típuscseretábla where [dátum]=#" + Dátum.Value.ToString("M-d-yy") + "#";
                if (Délelőtt.Checked)
                    szöveg += " and napszak='de'";
                else
                    szöveg += " and napszak='du'";
                szöveg = szöveg + " and telephely='" + Vál_Telephely.Trim() + "'";

                FőTípuscsereListaFeltöltés();
                Adat_Típuscsere_Adatok Elem = (from a in AdatokTípuscsere
                                               where a.Dátum.ToShortDateString() == Dátum.Value.ToShortDateString() &&
                                               a.Napszak == (Délelőtt.Checked ? "de" : "du") &&
                                               a.Telephely == Vál_Telephely.Trim()
                                               select a).FirstOrDefault();


                if (Elem != null)
                {
                    szöveg = "DELETE FROM típuscseretábla WHERE [dátum]=#" + Dátum.Value.ToString("M-d-yy") + "#";

                    if (Délelőtt.Checked)
                        szöveg += " and napszak='de'";
                    else
                        szöveg += " and napszak='du'";

                    szöveg += " and telephely='" + Vál_Telephely.Trim() + "'";
                    MyA.ABtörlés(hely, jelszó, szöveg);
                }


                szöveg = "SELECT * FROM típuscseretábla where [dátum]=#" + Dátum.Value.ToString("M-d-yy") + "#";

                if (Délelőtt.Checked)
                    szöveg += " and napszak='de'";
                else
                    szöveg += " and napszak='du'";

                Kezelő_Főkönyv_Típuscsere KFT_kéz = new Kezelő_Főkönyv_Típuscsere();
                List<Adat_FőKönyv_Típuscsere> Adatok = KFT_kéz.Lista_adatok(helytelep, jelszótelep, szöveg);

                List<string> SzövegGy = new List<string>();
                foreach (Adat_FőKönyv_Típuscsere rekord in Adatok)
                {

                    string típuskiadott = (from a in AdatokTipRend
                                           where a.Telephely == LabelTelephely.Text.Trim() && a.Telephelyitípus == rekord.Típuskiadott.ToStrTrim()
                                           select a.AlTípus).FirstOrDefault() ?? "?";

                    string típuselőírt = (from a in AdatokTipRend
                                          where a.Telephely == LabelTelephely.Text.Trim() && a.Telephelyitípus == rekord.Típuselőírt.ToStrTrim()
                                          select a.AlTípus).FirstOrDefault() ?? "?";

                    szöveg = "INSERT INTO típuscseretábla (dátum, napszak, telephely, szolgálat, típuselőírt, típuskiadott, viszonylat, forgalmiszám, tervindulás, azonosító  ) VALUES (";
                    szöveg += "'" + rekord.Dátum.ToString("yyyy.MM.dd") + "', ";
                    szöveg += "'" + rekord.Napszak.ToStrTrim() + "', ";
                    szöveg += "'" + Vál_Telephely.Trim() + "', ";
                    szöveg += "'" + SzolgálatNév.Trim() + "', ";
                    szöveg += "'" + típuselőírt.ToStrTrim() + "', ";
                    szöveg += "'" + típuskiadott.ToStrTrim() + "', ";
                    szöveg += "'" + rekord.Viszonylat.ToStrTrim() + "', ";
                    szöveg += "'" + rekord.Forgalmiszám.ToStrTrim() + "', ";
                    szöveg += "'" + rekord.Tervindulás.ToString("HH:mm") + "', ";
                    szöveg += "'" + rekord.Azonosító + "') ";

                    SzövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
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
        private void Listák_Töltése()
        {
            KiegTípusRendezés_feltöltés();
            KiegSzolgTelep_feltöltés();
            KiegTelepek_feltöltés();
        }

        private void KiegTípusRendezés_feltöltés()
        {
            try
            {
                AdatokTipRend.Clear();
                AdatokTipRend = KézTipRend.Lista_Adatok();
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

        private void KiegSzolgTelep_feltöltés()
        {
            try
            {
                AdatokSzolgTelep.Clear();
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő.mdb";
                string jelszó = "Mocó";

                string szöveg = "SELECT * FROM szolgálattelepeitábla";
                AdatokSzolgTelep = KézSzolgTelep.Lista_Adatok(hely, jelszó, szöveg);
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

        private void KiegTelepek_feltöltés()
        {
            try
            {
                AdatokTelep.Clear();
                AdatokTelep = KézTelep.Lista_adatok();
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

        private void FőmérnökségListaFeltöltés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_kiadási_adatok.mdb";
                string jelszó = "pozsi";
                string szöveg = "SELECT * FROM kiadástábla ";
                AdatokKiadási.Clear();
                AdatokKiadási = KézKiadás.Lista_adatok(hely, jelszó, szöveg);
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

        private void FőSzemélyzetListaFeltöltés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_személyzet_adatok.mdb";
                string jelszó = "pozsi";
                string szöveg = "SELECT * FROM személyzettábla ";

                AdatokSzemélyzet.Clear();
                AdatokSzemélyzet = KézSzeméyzet.Lista_adatok(hely, jelszó, szöveg);
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

        private void FőTípuscsereListaFeltöltés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_típuscsere_adatok.mdb";
                string jelszó = "pozsi";
                string szöveg = "SELECT * FROM típuscseretábla ";

                AdatokTípuscsere.Clear();
                AdatokTípuscsere = KézTípuscsere.Lista_adatok(hely, jelszó, szöveg);
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

        private void TelepKidásiListaFeltöltés(string hely)
        {
            try
            {
                string jelszó = "plédke";
                string szöveg = "SELECT * FROM tábla";
                AdatokKiadásTelep.Clear();
                AdatokKiadásTelep = KézTelepKiadás.Lista_adatok(hely, jelszó, szöveg);
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