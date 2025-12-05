using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_Adatszerkezet;
using Villamos.Villamos_Ablakok;
using Villamos.Villamos_Ablakok.Beosztás;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;


namespace Villamos
{
    public partial class Ablak_Beosztás
    {
        int ScrollX = 0;
        int ScrollY = 0;
        int Elsősor = 0;
        int hónap_hossz = 0;
        bool Chk_CTRL = false;

        int TáblaSor;
        int TáblaOszlop;
        string Előzőtartalom = "";
        string BeosztáskódVálasztott = "";
        int Ledolgozott_idő = 0;
        DateTime Hónap_első;
        string munkalap = "";
        readonly Beállítás_Betű BeBetű = new Beállítás_Betű { };
        readonly Beállítás_Betű BeBetűV = new Beállítás_Betű { Vastag = true };

        readonly Kezelő_Dolgozó_Alap KézDolg = new Kezelő_Dolgozó_Alap();
        readonly Kezelő_Kiegészítő_Csoportbeosztás KézCsop = new Kezelő_Kiegészítő_Csoportbeosztás();
        readonly Kezelő_Kiegészítő_Beosztáskódok KÉZBeoKód = new Kezelő_Kiegészítő_Beosztáskódok();
        readonly Kezelő_Dolgozó_Beosztás_Új KézBeo = new Kezelő_Dolgozó_Beosztás_Új();
        readonly Kezelő_Váltós_Naptár KézVáltNaptár = new Kezelő_Váltós_Naptár();
        readonly Kezelő_Kiegészítő_Munkaidő KézMunkaIdő = new Kezelő_Kiegészítő_Munkaidő();
        readonly Kezelő_Kiegészítő_Váltóstábla KézVáltósTábla = new Kezelő_Kiegészítő_Váltóstábla();
        readonly Kezelő_Kiegészítő_Beosztásciklus KézBeoCiklus = new Kezelő_Kiegészítő_Beosztásciklus();
        readonly Kezelő_Szatube_Aft KézAft = new Kezelő_Szatube_Aft();
        readonly Kezelő_Szatube_Beteg KézBeteg = new Kezelő_Szatube_Beteg();
        readonly Kezelő_Szatube_Csúsztatás KézCsúsztatás = new Kezelő_Szatube_Csúsztatás();
        readonly Kezelő_Szatube_Szabadság KézSzabadság = new Kezelő_Szatube_Szabadság();
        readonly Kezelő_Szatube_Túlóra KézTúlóra = new Kezelő_Szatube_Túlóra();

        List<Adat_Kiegészítő_Beosztáskódok> AdatokBeoKód = new List<Adat_Kiegészítő_Beosztáskódok>();
        List<Adat_Dolgozó_Alap> AdatokDolg = new List<Adat_Dolgozó_Alap>();


        #region Alap
        public Ablak_Beosztás()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            try
            {
                //Ha az első karakter "R" akkor az új jogosultságkiosztást használjuk
                //ha nem akkor a régit használjuk
                if (Program.PostásJogkör.Substring(0, 1) == "R")
                {
                    TelephelyekFeltöltéseÚj();
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
                }
                else
                {
                    Telephelyekfeltöltése();
                    Jogosultságkiosztás();
                }

                Telephely_Beállítás();
                hónap_hossz = MyF.Hónap_hossza(Dátum.Value);
                Hónap_első = MyF.Hónap_elsőnapja(Dátum.Value);
                this.Text = $"A {Dátum.Value.Year} év {Dátum.Value.Month} havi beosztása";
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

        private void Ablak_Beosztás_Load(object sender, EventArgs e)
        { }

        private void Ablak_Beosztás_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Beosztás_kieg?.Close();
        }

        private void Ablak_Beosztás_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == 17)
                Chk_CTRL = true;
        }

        private void Ablak_Beosztás_KeyUp(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == 17)
                Chk_CTRL = false;
        }

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Személy(true))
                    Cmbtelephely.Items.Add(Elem);
                if (Program.PostásTelephely == "Főmérnökség")
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

        private void TelephelyekFeltöltéseÚj()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Adat in GombLathatosagKezelo.Telephelyek(this.Name))
                    Cmbtelephely.Items.Add(Adat.Trim());
                //Alapkönyvtárat beállítjuk 
                if (Cmbtelephely.Items.Cast<string>().Contains(Program.PostásTelephely))
                    Cmbtelephely.Text = Program.PostásTelephely;
                else
                    Cmbtelephely.Text = Cmbtelephely.Items[0].ToStrTrim();
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


        private void Telephely_Beállítás()
        {
            Cursor = Cursors.WaitCursor; // homok óra kezdete
            AdatokDolg = KézDolg.Lista_Adatok(Cmbtelephely.Text.Trim());
            Csoportfeltöltés();
            Névfeltöltés();
            Dátum.Value = DateTime.Today;

            Visszacsukcsoport();
            Visszacsukjadolgozó();

            AdatokBeoKód = KÉZBeoKód.Lista_Adatok(Cmbtelephely.Text.Trim());
            Nyolcórásfeltölt();
            Mindenfeltölt();
            Tizenkétórásfeltölt();

            ScrollX = 0;
            ScrollY = 0;
            Elsősor = 0;

            Dolgozóneve.Text = "";
            Hrazonosító.Text = "";
            NapKiválaszt.Text = "";

            Cursor = Cursors.Default; // homokóra vége
        }

        private void Cmbtelephely_SelectedIndexChanged(object sender, EventArgs e)
        {
            Telephely_Beállítás();
        }

        private void Kilépettjel_CheckedChanged(object sender, EventArgs e)
        {
            Névfeltöltés();
        }

        private void Jogosultságkiosztás()
        {

            int melyikelem;

            // ide kell az összes gombot tenni amit szabályozni akarunk false

            Excel_gomb.Visible = false;
            Váltós.Visible = false;
            Előzmény.Visible = false;
            Adatok_egyeztetése.Visible = false;
            Gomb_nappalos.Visible = false;

            Múlt.Visible = false;
            Jelen.Visible = false;
            Jövő.Visible = false;

            melyikelem = 22;
            // módosítás 1  visszamenőleges beosztás rögzítés
            if (MyF.Vanjoga(melyikelem, 1))
            {
                Múlt.Visible = true;
            }
            // módosítás 2 Adott napi beosztás rögzítés
            if (MyF.Vanjoga(melyikelem, 2))
            {
                Jelen.Visible = true;
            }
            // módosítás 3 előre menő adatrögzítés
            if (MyF.Vanjoga(melyikelem, 3))
            {
                Jövő.Visible = true;
            }

            melyikelem = 23;
            // módosítás 1 Túlóra rögzítés
            if (MyF.Vanjoga(melyikelem, 1))
            {
            }
            // módosítás 2 túlóra törlés
            if (MyF.Vanjoga(melyikelem, 2))
            {

            }
            // módosítás 3
            if (MyF.Vanjoga(melyikelem, 3))
            {
            }

            melyikelem = 24;
            // módosítás 1 Csúsztatás rögzítés
            if (MyF.Vanjoga(melyikelem, 1))
            {

            }
            // módosítás 2 Csúsztatás törlés
            if (MyF.Vanjoga(melyikelem, 2))
            {

            }
            // módosítás 3
            if (MyF.Vanjoga(melyikelem, 3))
            {
            }

            melyikelem = 25;
            // módosítás 1 Szabadság rögzítés
            if (MyF.Vanjoga(melyikelem, 1))
            {


            }
            // módosítás 2 Megjegyzés rögzítás
            if (MyF.Vanjoga(melyikelem, 2))
            {

            }
            // módosítás 3
            if (MyF.Vanjoga(melyikelem, 3))
            {
            }

            melyikelem = 26;
            // módosítás 1 AFT rögzítés
            if (MyF.Vanjoga(melyikelem, 1))
            {


            }
            // módosítás 2 AFT törlés
            if (MyF.Vanjoga(melyikelem, 2))
            {
            }
            // módosítás 3
            if (MyF.Vanjoga(melyikelem, 3))
            {
            }

            melyikelem = 27;
            // módosítás 1 
            if (MyF.Vanjoga(melyikelem, 1))
            {

                Excel_gomb.Visible = true;
            }
            // módosítás 2
            if (MyF.Vanjoga(melyikelem, 2))
            {
                Váltós.Visible = true;
            }
            // módosítás 3
            if (MyF.Vanjoga(melyikelem, 3))
            {
                Előzmény.Visible = true;
            }

            melyikelem = 28;
            // módosítás 1 
            if (MyF.Vanjoga(melyikelem, 1))
            {
                Adatok_egyeztetése.Visible = true;

            }
            // módosítás 2
            if (MyF.Vanjoga(melyikelem, 2))
            {
                Gomb_nappalos.Visible = true;
            }
            // módosítás 3
            if (MyF.Vanjoga(melyikelem, 3))
            {

            }

        }

        private void Cmbtelephely_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                Cmbtelephely.Text = Cmbtelephely.Items[Cmbtelephely.SelectedIndex].ToStrTrim();
                if (Cmbtelephely.Text.Trim() == "") return;
                //Ha az első karakter "R" akkor az új jogosultságkiosztást használjuk
                //ha nem akkor a régit használjuk
                if (Program.PostásJogkör.Substring(0, 1) == "R")
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
                else
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
        #endregion


        #region Dolgozónév választás
        private void Névfeltöltés()
        {
            try
            {
                Dolgozónév.Items.Clear();
                Dolgozónév.BeginUpdate();

                List<Adat_Dolgozó_Alap> Adatok;
                if (Kilépettjel.Checked)
                    Adatok = AdatokDolg.OrderBy(y => y.DolgozóNév).ToList();
                else
                    Adatok = (from a in AdatokDolg
                              where a.Kilépésiidő == new DateTime(1900, 1, 1)
                              orderby a.DolgozóNév ascending
                              select a).ToList();

                foreach (Adat_Dolgozó_Alap rekord in Adatok)
                    Dolgozónév.Items.Add(rekord.DolgozóNév + " = " + rekord.Dolgozószám.Trim());

                Dolgozónév.EndUpdate();
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

        private void Nyitdolgozó_Click(object sender, EventArgs e)
        {
            Dolgozónév.Height = 500;
            CsukDolgozó.Visible = true;
            NyitDolgozó.Visible = false;
        }

        private void Csukdolgozó_Click(object sender, EventArgs e)
        {
            Visszacsukjadolgozó();
        }

        private void Visszacsukjadolgozó()
        {
            Dolgozónév.Height = 25;
            CsukDolgozó.Visible = false;
            NyitDolgozó.Visible = true;
            Lenyíló_Off();
        }

        private void Dolgozókijelölmind_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < Dolgozónév.Items.Count; i++)
                    Dolgozónév.SetItemChecked(i, true);
                Visszacsukjadolgozó();
                Táblaíró();
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

        private void Dolgozóvissza_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < Dolgozónév.Items.Count; i++)
                    Dolgozónév.SetItemChecked(i, false);
                Visszacsukjadolgozó();
                Táblaíró();
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

        private void DolgozóFrissít_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // homok óra kezdete
                Visszacsukjadolgozó();
                Táblaíró();

                Cursor = Cursors.Default; // homokóra vége
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


        #region Csoport választás
        private void Csoportfeltöltés()
        {
            try
            {
                Csoport.Items.Clear();
                List<Adat_Kiegészítő_Csoportbeosztás> Adatok = KézCsop.Lista_Adatok(Cmbtelephely.Text.Trim());
                foreach (Adat_Kiegészítő_Csoportbeosztás Elem in Adatok)
                    Csoport.Items.Add(Elem.Csoportbeosztás);
                Csoport.Refresh();
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

        private void NyitCsoport_Click(object sender, EventArgs e)
        {
            Csoport.Height = 300;
            CsukCsoport.Visible = true;
            NyitCsoport.Visible = false;
        }

        private void CsukCsoport_Click(object sender, EventArgs e)
        {
            Visszacsukcsoport();
        }

        private void Visszacsukcsoport()
        {
            Csoport.Height = 25;
            CsukCsoport.Visible = false;
            NyitCsoport.Visible = true;
            Lenyíló_Off();
        }

        private void Csoportkijelölmind_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < Csoport.Items.Count; i++)
                    Csoport.SetItemChecked(i, true);
                Visszacsukcsoport();
                Csoport_listáz();
                Táblaíró();
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

        private void Csoportvissza_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < Csoport.Items.Count; i++)
                    Csoport.SetItemChecked(i, false);
                Visszacsukcsoport();
                Csoport_listáz();
                Táblaíró();
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

        private void CsoportFrissít_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // homok óra kezdete
                Visszacsukcsoport();
                Csoport_listáz();
                Táblaíró();
                Lenyíló_Off();

                Cursor = Cursors.Default; // homokóra vége
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

        private void Lenyíló_Off()
        {
            Nyolcórás.Visible = false;
            Tizenkétórás.Visible = false;
            Minden.Visible = false;
            Dolgozóneve.Text = "";
            Hrazonosító.Text = "";
            NapKiválaszt.Text = "";
            Ledolgozottidő.Text = "";
        }

        private void Csoport_listáz()
        {
            try
            {
                // minden kijelölést töröl
                for (int i = 0; i < Dolgozónév.Items.Count; i++)
                    Dolgozónév.SetItemChecked(i, false);


                for (int j = 0; j < Csoport.CheckedItems.Count; j++)
                {
                    List<Adat_Dolgozó_Alap> Adatok = (from a in AdatokDolg
                                                      where a.Kilépésiidő == new DateTime(1900, 1, 1) && a.Csoport == Csoport.CheckedItems[j].ToStrTrim()
                                                      orderby a.DolgozóNév ascending
                                                      select a).ToList();

                    for (int i = 0; i < Dolgozónév.Items.Count; i++)
                    {
                        string[] darabol = Dolgozónév.Items[i].ToString().Split('=');
                        string Elem = (from a in Adatok
                                       where a.Dolgozószám.Trim() == darabol[1].Trim()
                                       select a.Dolgozószám).FirstOrDefault();
                        if (Elem != null)
                            Dolgozónév.SetItemChecked(i, true);
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


        #region kiírás
        private void Táblaíró()
        {

            Holtart.Be();
            Naptár_kiírás();
            if (Váltósbeosztás.Checked) Váltós_BEO_Kiírás();
            Dolgozók_kiírás_tábla();
            Szabi_kiírás();
            Munkarend_kiírás();
            Naptár_Színezése();
            Beosztás_kiírása();

            Tábla.Visible = true;
            Holtart.Ki();
        }

        private void Naptár_Színezése()
        {
            try
            {
                Holtart.BackColor = Color.Green;
                // ********************************************
                // kiszinezzük a szabad és munkaszüneti napokat
                // ********************************************
                Hónap_első = MyF.Hónap_elsőnapja(Dátum.Value);
                List<Adat_Váltós_Naptár> Adatok = KézVáltNaptár.Lista_Adatok(Dátum.Value.Year, "");
                Adatok = (from a in Adatok
                          where a.Dátum >= MyF.Hónap_elsőnapja(Dátum.Value) &&
                                a.Dátum <= MyF.Hónap_utolsónapja(Dátum.Value)
                          orderby a.Dátum ascending
                          select a).ToList();


                if (Adatok == null || Adatok.Count < 1)
                {
                    // akkor a naptári jelölés
                    for (int i = 3; i <= hónap_hossz + 2; i++)
                    {
                        if (Tábla.Rows[0].Cells[i].Value != null)
                        {
                            if (Tábla.Rows[0].Cells[i].Value.ToString().Trim() == "Szo")
                            {
                                for (int j = 0; j < Tábla.Rows.Count; j++)
                                    Tábla.Rows[j].Cells[i].Style.BackColor = Color.FromArgb(186, 176, 165);
                            }
                            if (Tábla.Rows[0].Cells[i].Value.ToString().Trim() == "V")
                            {
                                for (int j = 1; j < Tábla.Rows.Count; j++)
                                    Tábla.Rows[j].Cells[i].Style.BackColor = Color.FromArgb(228, 189, 141);
                            }
                        }
                    }
                }
                else
                {
                    // munkaügyi naptár
                    for (int i = 3; i <= hónap_hossz + 2; i++)
                    {
                        string nap = (from a in Adatok
                                      where a.Dátum == Hónap_első.AddDays(i - 3)
                                      select a.Nap).FirstOrDefault();
                        if (nap != null)
                        {
                            switch (nap)
                            {
                                case "P":
                                    {

                                        for (int j = 0; j < Tábla.RowCount; j++)
                                            Tábla.Rows[j].Cells[i].Style.BackColor = Color.FromArgb(186, 176, 165);
                                        break;
                                    }
                                case "V":
                                    {

                                        for (int j = 0; j < Tábla.RowCount; j++)
                                            Tábla.Rows[j].Cells[i].Style.BackColor = Color.FromArgb(228, 189, 141);
                                        break;
                                    }
                                case "Ü":
                                    {

                                        for (int j = 0; j < Tábla.RowCount; j++)
                                            Tábla.Rows[j].Cells[i].Style.BackColor = Color.FromArgb(244, 95, 95);
                                        break;
                                    }
                            }
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

        private void Naptár_kiírás()
        {
            try
            {
                Hónap_első = MyF.Hónap_elsőnapja(Dátum.Value);
                Holtart.BackColor = Color.MediumSeaGreen;
                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;

                Tábla.ColumnCount = hónap_hossz + 5;
                Tábla.RowCount = 1;
                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Név";
                Tábla.Columns[0].Width = 200;
                Tábla.Columns[1].HeaderText = "Perc";
                Tábla.Columns[1].Width = 50;
                Tábla.Columns[2].HeaderText = "Szab";
                Tábla.Columns[2].Width = 50;
                Tábla.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable; // letiltjuk az oszlopnak megfelelő rendezést.
                Tábla.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                Tábla.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                Tábla.Rows[0].Cells[0].Style.BackColor = Color.MediumSeaGreen;
                Tábla.Rows[0].Cells[1].Style.BackColor = Color.MediumSeaGreen;
                Tábla.Rows[0].Cells[2].Style.BackColor = Color.MediumSeaGreen;


                for (int i = 0; i < hónap_hossz; i++)
                {
                    Tábla.Columns[i + 3].HeaderText = (i + 1).ToString();
                    Tábla.Columns[i + 3].Width = 39;
                    Tábla.Columns[i + 3].SortMode = DataGridViewColumnSortMode.NotSortable;
                    DateTime adottnap = Hónap_első.AddDays(i);
                    Tábla.Rows[0].Cells[i + 3].Value = adottnap.ToString("ddd");
                    Tábla.Rows[0].Cells[i + 3].Style.BackColor = Color.MediumSeaGreen;
                    Holtart.Lép();
                }

                Tábla.Rows[0].Cells[Tábla.ColumnCount - 1].Style.BackColor = Color.MediumSeaGreen;
                Tábla.Columns[Tábla.ColumnCount - 1].SortMode = DataGridViewColumnSortMode.NotSortable;
                Tábla.Rows[0].Cells[Tábla.ColumnCount - 2].Style.BackColor = Color.MediumSeaGreen;
                Tábla.Columns[Tábla.ColumnCount - 2].SortMode = DataGridViewColumnSortMode.NotSortable;
                // beállítjuk a háttérszínre a betűszínt, hogy ne látszódjon
                Tábla.Rows[0].Cells[Tábla.ColumnCount - 1].Style.ForeColor = Color.MediumSeaGreen;
                Tábla.Rows[0].Cells[Tábla.ColumnCount - 2].Style.ForeColor = Color.MediumSeaGreen;
                Tábla.Rows[0].Cells[0].Style.ForeColor = Color.MediumSeaGreen;
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

        private void Váltós_BEO_Kiírás()
        {
            try
            {
                // ******************************
                // kiirjuk a váltós munkarendeket
                // ******************************
                Holtart.BackColor = Color.Orange;
                List<Adat_Kiegészítő_Váltóstábla> Adatok = KézVáltósTábla.Lista_Adatok();

                foreach (Adat_Kiegészítő_Váltóstábla rekordváltó in Adatok)
                {
                    // ha nincs kijelölve váltós akkor nem írja ki
                    for (int j = 0; j < Csoport.CheckedItems.Count; j++)
                    {
                        Holtart.Lép();
                        if (Csoport.CheckedItems[j].ToString().Trim().Contains(rekordváltó.Megnevezés.Trim()))
                        {

                            // ha volt váltócsoport
                            Tábla.RowCount++;
                            Tábla.Rows[Tábla.RowCount - 1].Cells[0].Value = rekordváltó.Megnevezés.Trim();
                            // hol tart a ciklusban

                            Tábla.Rows[Tábla.RowCount - 1].Frozen = true;

                            Tábla.Rows[Tábla.RowCount - 1].Cells[0].Style.BackColor = Color.MediumSeaGreen;
                            Tábla.Rows[Tábla.RowCount - 1].Cells[1].Style.BackColor = Color.MediumSeaGreen;
                            Tábla.Rows[Tábla.RowCount - 1].Cells[2].Style.BackColor = Color.MediumSeaGreen;

                            List<Adat_Kiegészítő_Beosztásciklus> AdatokBeo = KézBeoCiklus.Lista_Adatok("beosztásciklus");

                            //Végig megyünk a hónapnapjain és kiírjuk a beosztáskódot
                            for (int i = 0; i < hónap_hossz; i++)
                            {
                                DateTime Aktuális = MyF.Hónap_elsőnapja(Dátum.Value).AddDays(i);
                                int hanyadik = MyF.Váltónap(rekordváltó.Kezdődátum, Aktuális, rekordváltó.Ciklusnap);
                                string elem = (from a in AdatokBeo
                                               where a.Id == hanyadik
                                               select a.Beosztáskód).FirstOrDefault();
                                if (elem != null)
                                    Tábla.Rows[Tábla.RowCount - 1].Cells[i + 3].Value = elem.Trim();
                            }
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

        private void Dolgozók_kiírás_tábla()
        {
            try
            {
                Holtart.BackColor = Color.Yellow;
                // ******************************
                // kijelölt nevek beírása a listába
                // ******************************
                for (int i = 0; i < Dolgozónév.CheckedItems.Count; i++)
                {
                    Holtart.Lép();

                    Tábla.RowCount++;
                    string[] darabol = Dolgozónév.CheckedItems[i].ToString().Split('=');

                    Tábla.Rows[Tábla.RowCount - 1].Cells[0].Value = darabol[0].Trim();
                    Tábla.Rows[Tábla.RowCount - 1].Cells[1].Value = "0";
                    Tábla.Rows[Tábla.RowCount - 1].Cells[0].Style.BackColor = Color.MediumSeaGreen;
                    Tábla.Rows[Tábla.RowCount - 1].Cells[1].Style.BackColor = Color.MediumSeaGreen;


                    Tábla.Rows[Tábla.RowCount - 1].Cells[Tábla.ColumnCount - 1].Value = darabol[1].Trim();
                    Tábla.Rows[Tábla.RowCount - 1].Cells[Tábla.ColumnCount - 2].Value = "";
                }
                Tábla.Columns[Tábla.ColumnCount - 1].Width = 100;
                Tábla.Columns[Tábla.ColumnCount - 2].Width = 50;

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

        private void Szabi_kiírás()
        {
            try
            {
                List<Adat_Szatube_Szabadság> AdatokSzab = KézSzabadság.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);
                if (AdatokSzab == null || AdatokSzab.Count < 1) return;

                for (int i = 0; i < Tábla.Rows.Count; i++)
                {
                    if (Tábla.Rows[i].Cells[Tábla.ColumnCount - 1].Value != null)
                    {
                        List<Adat_Szatube_Szabadság> Adatok = (from a in AdatokSzab
                                                               where a.Státus != 3 && a.Törzsszám == Tábla.Rows[i].Cells[Tábla.ColumnCount - 1].Value.ToStrTrim()
                                                               select a).ToList();
                        int eredmény = 0;
                        foreach (Adat_Szatube_Szabadság rekord in Adatok)
                        {
                            if (rekord.Szabiok.ToUpper().Contains("KIVÉTEL"))
                                eredmény -= rekord.Kivettnap;
                            else
                                eredmény += rekord.Kivettnap;
                        }
                        Tábla.Rows[i].Cells[2].Value = eredmény;

                        if (eredmény < 0)
                            Tábla.Rows[i].Cells[2].Style.BackColor = Color.Red;
                        else
                            Tábla.Rows[i].Cells[2].Style.BackColor = Color.MediumSeaGreen;
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

        private void Munkarend_kiírás()
        {
            try
            {
                // ********************************************
                // Munkarend kiírása 
                // ********************************************
                Holtart.BackColor = Color.Red;
                for (int i = 0; i < Tábla.Rows.Count; i++)
                {
                    Holtart.Lép();
                    if (Tábla.Rows[i].Cells[Tábla.Columns.Count - 1].Value != null)
                    {
                        bool munkarend = (from a in AdatokDolg
                                          where a.Dolgozószám.Trim() == Tábla.Rows[i].Cells[Tábla.Columns.Count - 1].Value.ToString().Trim()
                                          select a.Munkarend).FirstOrDefault();
                        if (munkarend)
                            Tábla.Rows[i].Cells[Tábla.Columns.Count - 2].Value = 8;
                        else
                            Tábla.Rows[i].Cells[Tábla.Columns.Count - 2].Value = 12;
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

        private void Beosztás_kiírása()
        {
            try
            {
                List<Adat_Kiegészítő_Beosztáskódok> AdatokBEO = KÉZBeoKód.Lista_Adatok(Cmbtelephely.Text.Trim());
                AdatokBEO = AdatokBEO.Where(x => x.Éjszakás).ToList();

                Holtart.BackColor = Color.Blue;

                List<Adat_Dolgozó_Beosztás_Új> AdatokBeoszt = KézBeo.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value);
                if (AdatokBeoszt != null)
                {
                    for (int i = 0; i < Tábla.Rows.Count; i++)
                    {
                        int Kötelező_óraszám = 0;
                        Holtart.Lép();

                        if (Tábla.Rows[i].Cells[Tábla.ColumnCount - 1].Value != null)
                        {
                            List<Adat_Dolgozó_Beosztás_Új> Adatok = (from a in AdatokBeoszt
                                                                     where a.Dolgozószám == Tábla.Rows[i].Cells[Tábla.ColumnCount - 1].Value.ToStrTrim() &&
                                                                     a.Nap >= MyF.Hónap_elsőnapja(Dátum.Value) &&
                                                                     a.Nap <= MyF.Hónap_utolsónapja(Dátum.Value)
                                                                     orderby a.Nap
                                                                     select a).ToList();
                            foreach (Adat_Dolgozó_Beosztás_Új rekord in Adatok)
                            {
                                int oszlop = rekord.Nap.Day + 2;
                                Tábla.Rows[i].Cells[oszlop].Value = rekord.Beosztáskód.Trim();

                                // ha éjszakás
                                string beoKód = (from a in AdatokBEO
                                                 where a.Beosztáskód.Trim() == rekord.Beosztáskód.Trim()
                                                 select a.Beosztáskód).FirstOrDefault();

                                if (beoKód != null)
                                    Tábla.Rows[i].Cells[oszlop].Style.BackColor = Color.FromArgb(146, 165, 214);



                                // ha Szabadságon kap
                                if (rekord.Beosztáskód.ToUpper().Contains("SZ"))
                                    Tábla.Rows[i].Cells[oszlop].Style.BackColor = Color.FromArgb(221, 251, 0);

                                // ha beteg
                                if (rekord.Beosztáskód.ToUpper().Contains("B"))
                                    Tábla.Rows[i].Cells[oszlop].Style.BackColor = Color.FromArgb(130, 145, 145);
                                // ha Átlag
                                if (rekord.Beosztáskód.ToUpper().Contains("A"))
                                    Tábla.Rows[i].Cells[oszlop].Style.BackColor = Color.FromArgb(104, 243, 112);

                                // ha fizetett ünnep
                                if (rekord.Beosztáskód.ToUpper().Contains("F"))
                                    Tábla.Rows[i].Cells[oszlop].Style.BackColor = Color.FromArgb(255, 0, 0);

                                // ha van csúsztatás
                                if (rekord.Csúszóra != 0)
                                {
                                    Tábla.Rows[i].Cells[oszlop].Style.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Bold | FontStyle.Underline);
                                    Tábla.Rows[i].Cells[oszlop].Style.ForeColor = Color.Yellow;
                                    Tábla.Rows[i].Cells[oszlop].Style.BackColor = Color.FromArgb(225, 50, 255);
                                }

                                // kért nap
                                if (rekord.Kért)
                                    Tábla.Rows[i].Cells[oszlop].Style.BackColor = Color.Orange;

                                // ha van megjegyzés
                                if (rekord.Megjegyzés != null)
                                {
                                    if (rekord.Megjegyzés.Trim() != "")
                                    {
                                        Tábla.Rows[i].Cells[oszlop].Style.BackColor = Color.Orange;
                                        Tábla.Rows[i].Cells[oszlop].Style.ForeColor = Color.FromArgb(255, 0, 0);
                                        Tábla.Rows[i].Cells[oszlop].Style.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Regular);
                                    }
                                }

                                // ha van AFT
                                if (rekord.AFTóra != 0)
                                {
                                    Tábla.Rows[i].Cells[oszlop].Style.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Bold | FontStyle.Italic);
                                    Tábla.Rows[i].Cells[oszlop].Style.BackColor = Color.FromArgb(104, 243, 112);
                                    Tábla.Rows[i].Cells[oszlop].Style.ForeColor = Color.FromArgb(55, 55, 255);
                                }

                                // ha van túlóra
                                if (rekord.Túlóra != 0)
                                {
                                    Tábla.Rows[i].Cells[oszlop].Style.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Bold | FontStyle.Underline);
                                    Tábla.Rows[i].Cells[oszlop].Style.ForeColor = Color.Red;
                                    Tábla.Rows[i].Cells[oszlop].Style.BackColor = Color.FromArgb(30, 225, 255);
                                }

                                if (rekord.Beosztáskód.ToUpper().Contains("NEP") || rekord.Beosztáskód.ToUpper().Contains("ÉEP") || rekord.Beosztáskód.ToUpper().Contains("NE") || rekord.Beosztáskód.ToUpper().Contains("ÉE"))
                                {
                                    if (rekord.Túlóra == 0)
                                    {
                                        Tábla.Rows[i].Cells[oszlop].Style.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Bold | FontStyle.Underline);
                                        Tábla.Rows[i].Cells[oszlop].Style.BackColor = Color.Red;
                                        Tábla.Rows[i].Cells[oszlop].Style.ForeColor = Color.White;
                                    }
                                    else
                                    {
                                        Tábla.Rows[i].Cells[oszlop].Style.BackColor = Color.FromArgb(30, 225, 255);
                                    }
                                }

                                // kötelező óraszám
                                if (!rekord.Beosztáskód.ToUpper().Contains("FÜ"))
                                    Kötelező_óraszám += rekord.Ledolgozott;
                            }


                            Tábla.Rows[i].Cells[1].Value = Kötelező_óraszám;
                        }


                        // oszlopok rögzítése
                        Tábla.Columns[0].Frozen = true;
                        Tábla.Columns[1].Frozen = true;
                        Tábla.Columns[2].Frozen = true;
                        // sor rögzítése
                        if (Váltósbeosztás.Checked)
                        {
                            Tábla.Rows[0].Frozen = true;
                            if (Tábla.RowCount > 1)
                                Tábla.Rows[1].Frozen = true;
                        }
                        else
                        {
                            Tábla.Rows[0].Frozen = true;
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

        private void Tábla_Sorted(object sender, EventArgs e)
        {
            //nem engedjük a sorbarendezést
            for (int i = 0; i < Tábla.ColumnCount; i++)
                Tábla.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void Tábla_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == 0)
                ScrollX = e.NewValue;
            else
                ScrollY = e.NewValue;
        }

        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Dolgozóneve.Text = "";
                Hrazonosító.Text = "";
                NapKiválaszt.Text = "";
                Lenyíló_Off();

                int j = 0; bool volt = false;
                do
                {
                    if (Tábla.Rows[j].Cells[Tábla.ColumnCount - 1].Value != null)
                    {
                        Elsősor = j;
                        volt = true;
                    }
                    if (Tábla.Rows.Count <= j)
                        volt = true;

                    j++;

                } while (volt == false);

                // érvényes sorokat engedünk kiválasztani
                if (e.RowIndex < Elsősor)
                {
                    return;
                }

                // érvényes oszlopokat engedünk kiválasztani
                if (e.ColumnIndex <= 2 || e.ColumnIndex >= Tábla.ColumnCount - 2)
                {
                    return;
                }
                // Kijelöljök a sort
                if (Chk_CTRL)
                {
                    // egész sor színezése ha törölt
                    for (int i = 0; i < Tábla.ColumnCount; i++)
                        Tábla.Rows[e.RowIndex].Cells[i].Style.BackColor = Color.MediumSeaGreen;
                    return;
                }

                Dolgozóneve.Text = Tábla.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                Hrazonosító.Text = Tábla.Rows[e.RowIndex].Cells[Tábla.ColumnCount - 1].Value.ToString().Trim();
                NapKiválaszt.Text = new DateTime(Dátum.Value.Year, Dátum.Value.Month, int.Parse(Tábla.Columns[e.ColumnIndex].HeaderText)).ToString("yyyy.MM.dd");


                TáblaSor = e.RowIndex;
                TáblaOszlop = e.ColumnIndex;
                Előzőtartalom = Tábla.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null ? Tábla.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Trim() : "";

                if (Width > 300 + 39 * (e.ColumnIndex - 3) - ScrollX + 230)
                {
                    Nyolcórás.Left = 300 + 39 * (e.ColumnIndex - 3) - ScrollX;
                    Tizenkétórás.Left = 300 + 39 * (e.ColumnIndex - 3) - ScrollX;
                    Minden.Left = 300 + 39 * (e.ColumnIndex - 3) - ScrollX;
                }
                else
                {
                    Nyolcórás.Left = 300 + 39 * (e.ColumnIndex - 3) - ScrollX - 181;
                    Tizenkétórás.Left = 300 + 39 * (e.ColumnIndex - 3) - ScrollX - 181;
                    Minden.Left = 300 + 39 * (e.ColumnIndex - 3) - ScrollX - 181;
                }
                Nyolcórás.Top = 160 + 22 * e.RowIndex - ScrollY * 22;
                Tizenkétórás.Top = 160 + 22 * e.RowIndex - ScrollY * 22;
                Minden.Top = 160 + 22 * e.RowIndex - ScrollY * 22;

                if (Tábla.Rows[e.RowIndex].Cells[Tábla.ColumnCount - 2].Value.ToString().Trim() == "8")
                    Nyolcórás.Visible = true;
                else
                    Tizenkétórás.Visible = true;
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
            try
            {
                Hónap_első = MyF.Hónap_elsőnapja(Dátum.Value);
                hónap_hossz = MyF.Hónap_hossza(Dátum.Value);
                this.Text = $"A {Dátum.Value.Year} év {Dátum.Value.Month} havi beosztása";

                if (Dolgozónév.CheckedItems.Count != 0) Táblaíró();
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

        private void Tábla_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // ESC
                if ((int)e.KeyCode == 27)
                {
                    Nyolcórás.Visible = false;
                    Tizenkétórás.Visible = false;
                    Minden.Visible = false;
                    Dolgozóneve.Text = "";
                    Hrazonosító.Text = "";
                    NapKiválaszt.Text = "";
                }

                // F5
                if ((int)e.KeyCode == 116)
                {
                    Tizenkétórás.Visible = false;
                    Nyolcórás.Visible = false;
                    Minden.Visible = true;
                }
                // F6
                if ((int)e.KeyCode == 117)
                {
                    Tizenkétórás.Visible = false;
                    Nyolcórás.Visible = true;
                    Minden.Visible = false;
                }
                // F7
                if ((int)e.KeyCode == 118)
                {
                    Tizenkétórás.Visible = true;
                    Nyolcórás.Visible = false;
                    Minden.Visible = false;
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


        #region Tizenkétórás
        private void Tizenkétórásfeltölt()
        {
            try
            {

                Tizenkétórás.Items.Clear();
                Tizenkétórás.BeginUpdate();
                Tizenkétórás.Items.Add("");
                List<Adat_Kiegészítő_Beosztáskódok> Adatok = (from a in AdatokBeoKód
                                                              where a.Munkarend == 12
                                                              orderby a.Sorszám
                                                              select a).ToList();

                foreach (Adat_Kiegészítő_Beosztáskódok rekord in Adatok)
                    Tizenkétórás.Items.Add(rekord.Beosztáskód + " = " + rekord.Munkaidőkezdet.ToString("HH:mm") + " - " + rekord.Munkaidővége.ToString("HH:mm") + " = " + rekord.Munkaidő);

                Tizenkétórás.EndUpdate();
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

        private void Tizenkétórás_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tizenkétórás_rögzítés();
            Tizenkétórás.Visible = false;
        }

        private void Tizenkétórás_rögzítés()
        {
            try
            {
                BeosztáskódVálasztott = "";
                Ledolgozott_idő = 0;
                if (Tizenkétórás.Text.Trim() != "")
                {
                    string[] darabol = Tizenkétórás.Text.Split('=');
                    BeosztáskódVálasztott = darabol[0].Trim();
                    Ledolgozott_idő = int.Parse(darabol[2]);
                }
                Tábla.Rows[TáblaSor].Cells[TáblaOszlop].Value = BeosztáskódVálasztott;
                Rögzítés();
                Tizenkétórás.Visible = false;
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

        private void Tizenkétórás_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // ESC
                if ((int)e.KeyCode == 27)
                {
                    Nyolcórás.Visible = false;
                    Tizenkétórás.Visible = false;
                    Minden.Visible = false;
                }
                // F1
                if ((int)e.KeyCode == 112)
                {
                    Kiegészítő_doboz();
                }

                // F5
                if ((int)e.KeyCode == 116)
                {
                    Tizenkétórás.Visible = false;
                    Nyolcórás.Visible = false;
                    Minden.Visible = true;
                }
                // F6
                if ((int)e.KeyCode == 117)
                {
                    Tizenkétórás.Visible = false;
                    Nyolcórás.Visible = true;
                    Minden.Visible = false;
                }
                // F3
                if ((int)e.KeyCode == 118)
                {
                    Tizenkétórás.Visible = true;
                    Nyolcórás.Visible = false;
                    Minden.Visible = false;
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

        private void Tizenkétórás_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    Tizenkétórás_rögzítés();
                }
                if (e.Button == MouseButtons.Middle)
                {
                    Kiegészítő_doboz();
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


        #region Minden
        private void Mindenfeltölt()
        {
            try
            {
                Minden.Items.Clear();
                Minden.BeginUpdate();
                Minden.Items.Add("");
                List<Adat_Kiegészítő_Beosztáskódok> Adatok = (from a in AdatokBeoKód
                                                              orderby a.Sorszám
                                                              select a).ToList();

                foreach (Adat_Kiegészítő_Beosztáskódok rekord in Adatok)
                    Minden.Items.Add(rekord.Beosztáskód + " = " + rekord.Munkaidőkezdet.ToString("HH:mm") + " - " + rekord.Munkaidővége.ToString("HH:mm") + " = " + rekord.Munkaidő);

                Minden.EndUpdate();
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

        private void Minden_SelectedIndexChanged(object sender, EventArgs e)
        {
            Minden_rögzítés();
            Minden.Visible = false;
        }

        private void Minden_rögzítés()
        {
            try
            {
                BeosztáskódVálasztott = "";
                Ledolgozott_idő = 0;
                if (Minden.Text.Trim() != "")
                {
                    string[] darabol = Minden.Text.Split('=');
                    BeosztáskódVálasztott = darabol[0].Trim();
                    Ledolgozott_idő = int.Parse(darabol[2]);
                }
                Tábla.Rows[TáblaSor].Cells[TáblaOszlop].Value = BeosztáskódVálasztott;
                Rögzítés();
                Minden.Visible = false;
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

        private void Minden_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    Minden_rögzítés();
                }
                if (e.Button == MouseButtons.Middle)
                {
                    Kiegészítő_doboz();
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

        private void Minden_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // ESC
                if ((int)e.KeyCode == 27)
                {
                    Nyolcórás.Visible = false;
                    Tizenkétórás.Visible = false;
                    Minden.Visible = false;
                }
                // F1
                if ((int)e.KeyCode == 112)
                {
                    Kiegészítő_doboz();
                }
                // F5
                if ((int)e.KeyCode == 116)
                {
                    Tizenkétórás.Visible = false;
                    Nyolcórás.Visible = false;
                    Minden.Visible = true;
                }
                // F6
                if ((int)e.KeyCode == 117)
                {
                    Tizenkétórás.Visible = false;
                    Nyolcórás.Visible = true;
                    Minden.Visible = false;
                }
                // F3
                if ((int)e.KeyCode == 118)
                {
                    Tizenkétórás.Visible = true;
                    Nyolcórás.Visible = false;
                    Minden.Visible = false;
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


        #region Nyolcórás
        private void Nyolcórásfeltölt()
        {
            try
            {

                Nyolcórás.Items.Clear();
                Nyolcórás.BeginUpdate();
                Nyolcórás.Items.Add("");
                List<Adat_Kiegészítő_Beosztáskódok> Adatok = (from a in AdatokBeoKód
                                                              where a.Munkarend == 8
                                                              orderby a.Sorszám
                                                              select a).ToList();

                foreach (Adat_Kiegészítő_Beosztáskódok rekord in Adatok)
                    Nyolcórás.Items.Add(rekord.Beosztáskód + " = " + rekord.Munkaidőkezdet.ToString("HH:mm") + " - " + rekord.Munkaidővége.ToString("HH:mm") + " = " + rekord.Munkaidő);

                Nyolcórás.EndUpdate();
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

        private void Nyolcórás_SelectedIndexChanged(object sender, EventArgs e)
        {
            Nyolcórás_rögzítés();
        }

        private void Nyolcórás_rögzítés()
        {
            try
            {
                BeosztáskódVálasztott = "";
                Ledolgozott_idő = 0;
                if (Nyolcórás.Text.Trim() != "")
                {
                    string[] darabol = Nyolcórás.Text.Split('=');
                    BeosztáskódVálasztott = darabol[0].Trim();
                    Ledolgozott_idő = int.Parse(darabol[2]);
                }
                Tábla.Rows[TáblaSor].Cells[TáblaOszlop].Value = BeosztáskódVálasztott;
                Rögzítés();
                Nyolcórás.Visible = false;
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

        private void Nyolcórás_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    Nyolcórás_rögzítés();
                }
                if (e.Button == MouseButtons.Middle)
                {

                    Kiegészítő_doboz();
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

        private void Nyolcórás_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // ESC
                if ((int)e.KeyCode == 27)
                {
                    Nyolcórás.Visible = false;
                    Tizenkétórás.Visible = false;
                    Minden.Visible = false;
                }
                // F1
                if ((int)e.KeyCode == 112)
                {
                    Kiegészítő_doboz();
                }
                // F5
                if ((int)e.KeyCode == 116)
                {
                    Tizenkétórás.Visible = false;
                    Nyolcórás.Visible = false;
                    Minden.Visible = true;
                }
                // F6
                if ((int)e.KeyCode == 117)
                {
                    Tizenkétórás.Visible = false;
                    Nyolcórás.Visible = true;
                    Minden.Visible = false;
                }
                // F7
                if ((int)e.KeyCode == 118)
                {
                    Tizenkétórás.Visible = true;
                    Nyolcórás.Visible = false;
                    Minden.Visible = false;
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


        #region Rögzítés
        private void Rögzítés()
        {
            try
            {
                //Ha különböző az előzőtől, akkor rögzít
                if (BeosztáskódVálasztott.Trim() != Előzőtartalom.Trim())
                {
                    // Jogosultságok ellenőrzése
                    int Választék;

                    if (!(NapKiválaszt.Text.Trim() == "" || NapKiválaszt.Text.Trim() == "_"))
                    {
                        if (DateTime.Parse(NapKiválaszt.Text) == DateTime.Today)
                        {
                            Választék = 0;
                        }
                        else if (DateTime.Parse(NapKiválaszt.Text) > DateTime.Today)
                        {
                            Választék = 1;
                        }
                        else
                        {
                            Választék = -1;
                        }

                        switch (Választék)
                        {
                            case -1:
                                {
                                    if (Múlt.Visible == false) throw new HibásBevittAdat("Nincs jogosultsága az elmúlt napok beosztásának megváltoztatására!");
                                    break;
                                }
                            case 0:
                                {
                                    // módosítás 2 dolgozó oktatás elrendelésének törlése átütemezése
                                    if (Jelen.Visible == false) throw new HibásBevittAdat("Nincs jogosultsága a beosztás megváltoztatására!");
                                    break;
                                }
                            case 1:
                                {
                                    // módosítás 3 adminisztráció mentés, jelenléti ív készítés, e-mail küldés
                                    if (Jövő.Visible == false) throw new HibásBevittAdat("Nincs jogosultsága a hónap hátralévő napjainak beosztásának megváltoztatására!");
                                    break;
                                }
                        }
                    }
                    else
                    {
                        return;
                    }
                    Cursor = Cursors.WaitCursor; // homok óra kezdete
                    Beosztás_Rögzítés BR = new Beosztás_Rögzítés();
                    BR.Rögzít_BEO(Cmbtelephely.Text.Trim(), DateTime.Parse(NapKiválaszt.Text), BeosztáskódVálasztott, Előzőtartalom, Hrazonosító.Text.Trim(), Ledolgozott_idő, Dolgozóneve.Text.Trim());

                    //AFt adatainak módosítására visszaküldjük
                    if (BeosztáskódVálasztott.Length > 0 && BeosztáskódVálasztott.Substring(0, 1) == "A")
                    {
                        //       Táblaíró();
                        Kiegészítő_doboz();
                    }

                    Cursor = Cursors.Default; // homokóra vége
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


        #region Kiegészítő doboz
        private void Kiegészítő_Doboz_Click(object sender, EventArgs e)
        {
            Tizenkétórás.Visible = false;
            Minden.Visible = false;
            Nyolcórás.Visible = false;
            Kiegészítő_doboz();
        }

        Ablak_Beosztás_kieg Új_Ablak_Beosztás_kieg;
        private void Kiegészítő_doboz()
        {
            try
            {
                Új_Ablak_Beosztás_kieg?.Close();
                int lapfülszám = 3;

                if (BeosztáskódVálasztott.Length > 0 && BeosztáskódVálasztott.Substring(0, 1) == "A") lapfülszám = 4;
                if (Előzőtartalom.Length > 0 && Előzőtartalom.Substring(0, 1) == "A") lapfülszám = 4;
                if (!DateTime.TryParse(NapKiválaszt.Text, out DateTime Dátumérték)) throw new HibásBevittAdat("Nincs kiválasztva módodításhoz/rögzítéshez dátum.");

                Új_Ablak_Beosztás_kieg = new Ablak_Beosztás_kieg(Cmbtelephely.Text.Trim(), Dátumérték, BeosztáskódVálasztott, Előzőtartalom, Hrazonosító.Text.Trim(), Dolgozóneve.Text.Trim(), Ledolgozott_idő, lapfülszám, Múlt.Visible, Jelen.Visible, Jövő.Visible);
                Új_Ablak_Beosztás_kieg.FormClosed += Új_Ablak_Beosztás_kieg_FormClosed;
                Új_Ablak_Beosztás_kieg.Show();
                Új_Ablak_Beosztás_kieg.Változás += Táblaíró;
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

        private void Új_Ablak_Beosztás_kieg_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Beosztás_kieg = null;
        }
        #endregion


        #region gombok
        private void Adatok_egyeztetése_Click(object sender, EventArgs e)
        {
            Adat_egyeztetés_eseménye();
            MessageBox.Show("Az adatok ellenőrzése megtörént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Adat_egyeztetés_eseménye()
        {
            try
            {
                Holtart.BackColor = Color.Red;
                Holtart.Be(Tábla.Rows.Count + 1);

                Beosztás_Rögzítés BR = new Beosztás_Rögzítés();
                foreach (DataGridViewRow sor in Tábla.Rows)
                {
                    if (sor.Cells[Tábla.ColumnCount - 1].Value != null)
                    {
                        string Hr_Azonosító = sor.Cells[Tábla.ColumnCount - 1].Value.ToString().Trim();
                        BR.Ellenőrzés_Csúsztatás(Cmbtelephely.Text.Trim(), Dátum.Value, Hr_Azonosító);
                        BR.Ellenőrzés_Aft(Cmbtelephely.Text.Trim(), Dátum.Value, Hr_Azonosító);
                        BR.Ellenőrzés_Túlóra(Cmbtelephely.Text.Trim(), Dátum.Value, Hr_Azonosító);
                        BR.Ellenőrzés_Beteg(Cmbtelephely.Text.Trim(), Dátum.Value, Hr_Azonosító);
                        BR.Ellenőrzés_Szabadság(Cmbtelephely.Text.Trim(), Dátum.Value, Hr_Azonosító);
                        Holtart.Lép();
                    }
                }
                Holtart.Ki();
                Holtart.BackColor = Color.Lime;
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
                if (Dolgozónév.CheckedItems.Count < 1) throw new HibásBevittAdat("Nincs kijelölve egy dolgozó sem, így nem készül Excel tábla.");

                string fájlexc;
                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Beosztás_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                Adat_egyeztetés_eseménye();

                Holtart.Be(hónap_hossz + 1);
                Holtart.BackColor = Color.BlueViolet;
                Cursor = Cursors.WaitCursor; // homok óra kezdete
                munkalap = "Beosztás";
                MyX.ExcelLétrehozás(munkalap);
                MyX.Munkalap_betű(munkalap, BeBetű);

                MyX.Kiir(Cmbtelephely.Text.Trim() + $" telephely {Dátum.Value:yyyy.MMMM} havi beosztása", "A1");
                MyX.Betű(munkalap, "A1", BeBetűV);
                MyX.Munkalap_aktív(munkalap);

                //Fejléc
                MyX.Oszlopszélesség(munkalap, "A:A", 10);
                MyX.Oszlopszélesség(munkalap, "B:B", 30);
                MyX.Oszlopszélesség(munkalap, "C:D", 7);
                MyX.Oszlopszélesség(munkalap, "E:AP", 5);

                int sor = 3;
                MyX.Kiir("HR Azon.", $"A{sor}");
                MyX.Kiir("Név", $"B{sor}");
                MyX.Kiir("Perc", $"C{sor}");
                MyX.Kiir("Szab", $"D{sor}");

                int oszlop = 5;

                for (int i = 0; i < hónap_hossz; i++)
                {
                    DateTime adottnap = Hónap_első.AddDays(i);
                    MyX.Kiir(Hónap_első.AddDays(i).ToString("dd"), MyF.Oszlopnév(oszlop + i) + sor.ToString());
                    Holtart.Lép();
                }

                MyX.Betű(munkalap, "3:4", BeBetűV);
                MyX.Háttérszín(munkalap, $"A3:{MyF.Oszlopnév(hónap_hossz + oszlop - 1)}4", Color.DarkGray);
                MyX.Rácsoz(munkalap, $"A3:{MyF.Oszlopnév(hónap_hossz + oszlop - 1)}4");
                MyX.Háttérszín(munkalap, "A4", Color.MediumSeaGreen);

                //Táblázat adatainak másolása
                bool volt = false;
                int jj = 0;
                do
                {
                    if (Tábla.Rows[jj].Cells[Tábla.ColumnCount - 1].Value != null)
                    {
                        Elsősor = jj;
                        volt = true;
                    }
                    if (Tábla.Rows.Count <= jj)
                        volt = true;
                    jj++;
                } while (volt == false);

                sor = 4;
                oszlop = 2;
                Color Háttér;
                for (int i = 0; i < Tábla.Rows.Count; i++)
                {
                    for (int j = 0; j < Tábla.Columns.Count; j++)
                    {
                        Háttér = Tábla.Rows[i].Cells[j].Style.BackColor;
                        if (Háttér.Name == "0")
                            Háttér = Color.White;

                        if (j < Tábla.Columns.Count - 2)
                            MyX.Háttérszín(munkalap, MyF.Oszlopnév(oszlop + j) + (sor + i).ToString(), Háttér);

                        if (Tábla.Rows[i].Cells[j].Value != null)
                        {
                            if (j < Tábla.Columns.Count - 2)
                            {
                                MyX.Kiir(Tábla.Rows[i].Cells[j].Value.ToString(), MyF.Oszlopnév(oszlop + j) + (sor + i).ToString());
                            }


                            if (j == Tábla.Columns.Count - 1)
                            {
                                MyX.Kiir(Tábla.Rows[i].Cells[j].Value.ToString(), "A" + (sor + i).ToString());
                                MyX.Háttérszín(munkalap, "A" + (sor + i).ToString(), Color.MediumSeaGreen);
                            }

                        }
                    }
                    MyX.Sormagasság(munkalap, $"{(sor + i)}:{(sor + i)}", 20);
                }
                MyX.Rácsoz(munkalap, $"A5:{MyF.Oszlopnév(hónap_hossz + oszlop + 2)}{sor + Tábla.Rows.Count - 1}");

                Beállítás_Nyomtatás BeNYom = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"a1:{MyF.Oszlopnév(hónap_hossz + oszlop + 2)}{sor + Tábla.Rows.Count - 1}",
                    BalMargó = 10,
                    JobbMargó = 10,
                    FelsőMargó = 15,
                    AlsóMargó = 15,
                    FejlécMéret = 13,
                    LáblécMéret = 13,
                    LapSzéles = 1,
                    IsmétlődőSorok = "$1:$4",
                    Álló = false,
                    VízKözép = true
                };
                MyX.NyomtatásiTerület_részletes(munkalap, BeNYom);

                munkalap = "Részletes";
                MyX.Munkalap_Új(munkalap);
                MyX.Munkalap_betű(munkalap, BeBetű);
                MyX.Munkalap_aktív(munkalap);

                sor = 1;
                // fejlév
                MyX.Kiir("HR azonosító", "a1");
                MyX.Kiir("Dolgozónév", "b1");
                MyX.Kiir("Dátum", "c1");
                MyX.Kiir("Idő", "d1");
                MyX.Kiir("Kategória", "e1");
                MyX.Kiir("Szöveg", "f1");

                MyX.Oszlopszélesség(munkalap, "A:A", 15);
                MyX.Oszlopszélesség(munkalap, "B:B", 30);
                MyX.Oszlopszélesség(munkalap, "C:C", 12);
                MyX.Oszlopszélesség(munkalap, "D:D", 10);
                MyX.Oszlopszélesség(munkalap, "E:E", 20);
                MyX.Oszlopszélesség(munkalap, "F:F", 80);

                List<Adat_Dolgozó_Beosztás_Új> AdatokBEO = KézBeo.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value);
                sor = 1;
                for (int ki = 0; ki < Dolgozónév.CheckedItems.Count; ki++)
                {
                    string[] darabol = Dolgozónév.CheckedItems[ki].ToString().Split('=');

                    List<Adat_Dolgozó_Beosztás_Új> Adatok = (from a in AdatokBEO
                                                             where a.Dolgozószám == darabol[1].Trim() &&
                                                                   a.Nap >= MyF.Hónap_elsőnapja(Dátum.Value) &&
                                                                   a.Nap <= MyF.Hónap_utolsónapja(Dátum.Value)
                                                             orderby a.Nap ascending
                                                             select a).ToList();

                    if (Adatok != null)
                    {
                        foreach (Adat_Dolgozó_Beosztás_Új rekord in Adatok)
                        {
                            Adat_Dolgozó_Alap Dolgozó = (from a in AdatokDolg
                                                         where a.Dolgozószám == rekord.Dolgozószám
                                                         select a).FirstOrDefault();

                            if (rekord.Túlóra != 0)
                            {
                                sor += 1;
                                MyX.Kiir(rekord.Dolgozószám.Trim(), $"A{sor}");
                                MyX.Kiir(Dolgozó.DolgozóNév.Trim(), $"B{sor}");
                                MyX.Kiir(rekord.Nap.ToString("yyyy.MM.dd"), $"C{sor}");
                                MyX.Kiir(rekord.Túlóra.ToString(), $"D{sor}");
                                MyX.Kiir("túlóra", $"E{sor}");
                                string ideig = rekord.Túlórakezd.ToString("HH:mm") + " - " + rekord.Túlóravég.ToString("HH:mm") + " - " + rekord.Túlóraok.Trim();
                                MyX.Kiir(ideig, $"F{sor}");
                            }

                            if (rekord.Csúszóra != 0)
                            {
                                sor += 1;
                                MyX.Kiir(rekord.Dolgozószám.Trim(), $"A{sor}");
                                MyX.Kiir(Dolgozó.DolgozóNév.Trim(), $"B{sor}");
                                MyX.Kiir(rekord.Nap.ToString("yyyy.MM.dd"), $"C{sor}");
                                MyX.Kiir(rekord.Csúszóra.ToString(), $"D{sor}");
                                MyX.Kiir("Csúsztatás", $"E{sor}");
                                MyX.Kiir(rekord.Csúszok.Trim(), $"F{sor}");
                            }

                            if (rekord.AFTóra != 0)
                            {
                                sor += 1;
                                MyX.Kiir(rekord.Dolgozószám.Trim(), $"A{sor}");
                                MyX.Kiir(Dolgozó.DolgozóNév.Trim(), $"B{sor}");
                                MyX.Kiir(rekord.Nap.ToString("yyyy.MM.dd"), $"C{sor}");
                                MyX.Kiir(rekord.AFTóra.ToString(), $"D{sor}");
                                MyX.Kiir("Átlaggal fizetett", $"E{sor}");
                                MyX.Kiir(rekord.AFTok.Trim(), $"F{sor}");
                            }

                            if (rekord.Megjegyzés.Trim() != "")
                            {
                                sor += 1;
                                MyX.Kiir(rekord.Dolgozószám.Trim(), $"A{sor}");
                                MyX.Kiir(Dolgozó.DolgozóNév.Trim(), $"B{sor}");
                                MyX.Kiir(rekord.Nap.ToString("yyyy.MM.dd"), $"C{sor}");
                                MyX.Kiir("-", $"D{sor}");
                                MyX.Kiir("Információ", $"E{sor}");
                                MyX.Kiir(rekord.Megjegyzés.Trim(), $"F{sor}");
                            }

                            if (rekord.Szabiok.Trim() != "" && rekord.Szabiok.Trim() != "Normál kivétel")
                            {
                                sor += 1;
                                MyX.Kiir(rekord.Dolgozószám.Trim(), $"A{sor}");
                                MyX.Kiir(Dolgozó.DolgozóNév.Trim(), $"B{sor}");
                                MyX.Kiir(rekord.Nap.ToString("yyyy.MM.dd"), $"C{sor}");
                                MyX.Kiir(" - Szabadság - ", $"E{sor}");
                                MyX.Kiir(rekord.Szabiok.Trim(), $"F{sor}");
                            }
                            Holtart.Lép();
                        }
                    }
                }
                MyX.Rácsoz(munkalap, $"A1:F{sor}");

                Beállítás_Nyomtatás BeNYom1 = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"A1:F{sor}",
                    BalMargó = 10,
                    JobbMargó = 10,
                    FelsőMargó = 15,
                    AlsóMargó = 15,
                    FejlécMéret = 13,
                    LáblécMéret = 13,
                    LapSzéles = 1,
                    Álló = false,
                    VízKözép = true
                };
                MyX.NyomtatásiTerület_részletes(munkalap, BeNYom1);

                // az excel tábla bezárása
                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();

                Cursor = Cursors.Default; // homokóra vége
                Holtart.Ki();
                MessageBox.Show("Az Excel táblázat elkészült.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Súgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\beosztás.html";
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

        private void Váltós_Click(object sender, EventArgs e)
        {
            Kitölti_váltósnak();
        }


        private void Kitölti_váltósnak()
        {
            try
            {
                if (MessageBox.Show("Biztos, hogy feltölti a váltós beosztást a táblázatban szereplő dolgozóknál?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Cancel)
                    return;

                Holtart.Be(Tábla.Rows.Count + 1);

                List<Adat_Váltós_Naptár> AdatokVált = KézVáltNaptár.Lista_Adatok(Dátum.Value.Year, "");
                if (AdatokVált == null || AdatokVált.Count < 1) throw new HibásBevittAdat($"{Dátum.Value.Year} évben még nincs beállítva a munkaidő naptár.");

                List<Adat_Kiegészítő_Munkaidő> AdatokMunkaIdő = KézMunkaIdő.Lista_Adatok();

                for (int i = 0; i < Tábla.Rows.Count; i++)
                {
                    Holtart.Lép();

                    if (Tábla.Rows[i].Cells[Tábla.Columns.Count - 1].Value != null)
                    {
                        string sztörzsszám = Tábla.Rows[i].Cells[Tábla.Columns.Count - 1].Value.ToString().Trim();
                        string szmunkarend = Tábla.Rows[i].Cells[Tábla.Columns.Count - 2].Value.ToString().Trim();

                        // melyik csoportba dolgozik
                        string szcsoport = (from a in AdatokDolg
                                            where a.Dolgozószám.Trim() == sztörzsszám.Trim()
                                            select a.Csoportkód).FirstOrDefault();


                        // napi munkaidő
                        Adat_Kiegészítő_Munkaidő AdatMunkaIdő = (from a in AdatokMunkaIdő
                                                                 where a.Munkarendelnevezés == szmunkarend
                                                                 select a).FirstOrDefault();

                        Ledolgozott_idő = AdatMunkaIdő.Munkaidő.ToÉrt_Int();

                        // ha nem váltós akkor kihagy
                        if (szcsoport != null && szcsoport.Trim() != "_")
                            if (szcsoport != null && szcsoport.Trim() != "")
                            {

                                if (!szcsoport.Contains("É"))
                                    AdatokVált = KézVáltNaptár.Lista_Adatok(Dátum.Value.Year, szcsoport.Substring(szcsoport.Length - 1, 1));
                                else
                                    AdatokVált = KézVáltNaptár.Lista_Adatok(Dátum.Value.Year, (int.Parse(szcsoport.Substring(szcsoport.Length - 1, 1)) + 4).ToString());

                                AdatokVált = (from a in AdatokVált
                                              where a.Dátum >= MyF.Hónap_elsőnapja(Dátum.Value) &&
                                                    a.Dátum <= MyF.Hónap_utolsónapja(Dátum.Value)
                                              orderby a.Dátum ascending
                                              select a).ToList();

                                if (AdatokVált != null)
                                {
                                    for (int j = 3; j < Tábla.Columns.Count - 2; j++)
                                    {
                                        Holtart.Lép();
                                        DateTime IdeigNap = MyF.Hónap_elsőnapja(Dátum.Value).AddDays(j - 3);
                                        string sznap = (from a in AdatokVált
                                                        where a.Dátum == IdeigNap
                                                        select a.Nap).FirstOrDefault();

                                        BeosztáskódVálasztott = sznap;
                                        if (sznap != null)
                                        {
                                            if (sznap != "_")
                                            {
                                                // megkeressük a beosztáskódhoz tartozó adatokat
                                                switch (sznap)
                                                {
                                                    case "E":
                                                        {
                                                            if (!szcsoport.Contains("É"))
                                                                BeosztáskódVálasztott = "7"; // ha váltós
                                                            else
                                                                BeosztáskódVálasztott = "8";// ha állandó éjszakás
                                                            break;
                                                        }
                                                    case "Z":
                                                        {
                                                            if (!szcsoport.Contains("É"))
                                                                BeosztáskódVálasztott = "7"; // ha váltós
                                                            else
                                                                BeosztáskódVálasztott = "8";// ha állandó éjszakás
                                                            break;
                                                        }
                                                    case "P":
                                                        {
                                                            BeosztáskódVálasztott = "";
                                                            break;
                                                        }
                                                }
                                                NapKiválaszt.Text = IdeigNap.ToString("yyyy.MM.dd");
                                                Előzőtartalom = "";
                                                Hrazonosító.Text = sztörzsszám;
                                                Ledolgozottidő.Text = Ledolgozott_idő.ToString();
                                                Rögzítés();
                                            }
                                        }
                                    }
                                }
                            }
                    }
                }
                NapKiválaszt.Text = "";
                Előzőtartalom = "";
                Hrazonosító.Text = "";
                Holtart.Ki();
                Táblaíró();
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

        private void Előzmény_Click(object sender, EventArgs e)
        {
            Beosztás_Rögzítés BR = new Beosztás_Rögzítés();
            BR.Naplózás(Cmbtelephely.Text.Trim(), "Csoportos törlés");
            ELőzmények_törlése();
        }

        private void ELőzmények_törlése()
        {
            try
            {
                // kitörli az összes dolgozó beosztását
                if (MessageBox.Show("Biztos, hogy törli az összes havi adatot a táblázatban szereplő dolgozóknál?",
                    "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Cancel)
                    return;

                Holtart.Be(Tábla.Rows.Count + 1);
                List<string> Hr_Azonosítók = new List<string>();

                foreach (DataGridViewRow sor in Tábla.Rows)
                {
                    if (sor.Cells[Tábla.ColumnCount - 1].Value != null)
                    {
                        string Hr_Azonosító = sor.Cells[Tábla.ColumnCount - 1].Value.ToString().Trim();
                        Hr_Azonosítók.Add(Hr_Azonosító);
                        Holtart.Lép();
                    }
                }
                if (Hr_Azonosítók.Count > 1)
                {
                    KézBeo.Törlés(Cmbtelephely.Text.Trim(),
                                   Dátum.Value,
                                   MyF.Hónap_elsőnapja(Dátum.Value),
                                   MyF.Hónap_utolsónapja(Dátum.Value),
                                   Hr_Azonosítók,
                                   false);

                    KézAft.StátusÁllítás(Cmbtelephely.Text.Trim(), Dátum.Value.Year, MyF.Hónap_elsőnapja(Dátum.Value),
                                   MyF.Hónap_utolsónapja(Dátum.Value), Hr_Azonosítók, 3);
                    KézBeteg.Módosítás(Cmbtelephely.Text.Trim(), Dátum.Value.Year, MyF.Hónap_elsőnapja(Dátum.Value),
                                   MyF.Hónap_utolsónapja(Dátum.Value), Hr_Azonosítók, 3);
                    KézCsúsztatás.Módosítás(Cmbtelephely.Text.Trim(), Dátum.Value.Year, MyF.Hónap_elsőnapja(Dátum.Value),
                                   MyF.Hónap_utolsónapja(Dátum.Value), Hr_Azonosítók, 3);
                    KézSzabadság.Módosítás(Cmbtelephely.Text.Trim(), Dátum.Value.Year, MyF.Hónap_elsőnapja(Dátum.Value),
                                   MyF.Hónap_utolsónapja(Dátum.Value), Hr_Azonosítók, 3);
                    KézTúlóra.Módosítás(Cmbtelephely.Text.Trim(), Dátum.Value.Year, MyF.Hónap_elsőnapja(Dátum.Value),
                                   MyF.Hónap_utolsónapja(Dátum.Value), Hr_Azonosítók, 3);
                }
                Holtart.Ki();
                Táblaíró();
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

        private void Gomb_nappalos_Click(object sender, EventArgs e)
        {
            Nappalos_beosztás();
        }

        private void Nappalos_beosztás()
        {
            try
            {
                if (MessageBox.Show("Csak ÜRES beosztás esetén használható!\nBiztos, hogy feltölti a nappalos beosztást a táblázatban szereplő dolgozóknál?" +
                    " \n Minden meglévő adatot törölni fogsz vele.", "Figyelmeztetés", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                    return;

                Holtart.Be(Tábla.Rows.Count + 1);

                //Betöltjük a nappalos beosztást
                List<Adat_Váltós_Naptár> Adatok = KézVáltNaptár.Lista_Adatok(Dátum.Value.Year, "");
                if (Adatok == null || Adatok.Count < 1) throw new HibásBevittAdat($"{Dátum.Value.Year} évben még nincs beállítva a munkaidő naptár.");
                Adatok = (from a in Adatok
                          where a.Dátum >= MyF.Hónap_elsőnapja(Dátum.Value) &&
                                a.Dátum <= MyF.Hónap_utolsónapja(Dátum.Value)
                          orderby a.Dátum ascending
                          select a).ToList();

                Beosztás_Rögzítés BR = new Beosztás_Rögzítés();
                string Hr_Azonosító;

                foreach (DataGridViewRow sor in Tábla.Rows)
                {
                    if (sor.Cells[Tábla.Columns.Count - 1].Value != null)
                    {
                        Hr_Azonosító = sor.Cells[Tábla.ColumnCount - 1].Value.ToString().Trim();
                        foreach (Adat_Váltós_Naptár rekord in Adatok)
                        {
                            BeosztáskódVálasztott = rekord.Nap.Trim();
                            Ledolgozott_idő = 480;
                            if (BeosztáskódVálasztott.Trim() == "1")
                                BR.Rögzít_BEO(Cmbtelephely.Text.Trim(), rekord.Dátum, BeosztáskódVálasztott, "", Hr_Azonosító.Trim(), Ledolgozott_idő, Dolgozóneve.Text.Trim());
                            Holtart.Lép();
                        }
                    }
                }

                Holtart.Ki();

                Táblaíró();
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