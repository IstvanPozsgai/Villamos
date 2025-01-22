using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos.Kezelők;
using Villamos.Villamos_Ablakok.Beosztás;
using Villamos.Villamos_Adatszerkezet;
using Villamos.Villamos_Kezelők;
using static System.IO.File;
using MyA = Adatbázis;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok
{
    public partial class Ablak_Beosztás_kieg : Form
    {
        public string Cmbtelephely { get; private set; }
        public DateTime Dátum { get; private set; }
        public string BeosztáskódVálasztott { get; private set; }
        public string BeosztáskódElőző { get; private set; }
        public string Hrazonosító { get; private set; }

        public string DolgozóNév { get; private set; }

        public int Ledolgozott { get; private set; }
        public int Fülszám { get; private set; }

        public event Event_Kidobó Változás;

        readonly Kezelő_Dolgozó_Beosztás_Új Kéz = new Kezelő_Dolgozó_Beosztás_Új();
        Adat_Dolgozó_Beosztás_Új Adat = null;



        readonly Beosztás_Rögzítés BR = new Beosztás_Rögzítés();

        public Ablak_Beosztás_kieg(string cmbtelephely, DateTime dátum, string beosztáskódVálasztott, string beosztáskódElőző, string hrazonosító, string dolgozónév, int ledolgozott, int fülszám)
        {
            InitializeComponent();

            Cmbtelephely = cmbtelephely;
            Dátum = dátum;
            BeosztáskódVálasztott = beosztáskódVálasztott;
            Hrazonosító = hrazonosító;
            DolgozóNév = dolgozónév;
            Ledolgozott = ledolgozott;
            Fülszám = fülszám;
            BeosztáskódElőző = beosztáskódElőző;
            Start();
        }

        private void Ablak_Beosztás_kieg_Load(object sender, EventArgs e)
        {
            this.Text = $"Dolgozó neve: {DolgozóNév}  Dátum: {Dátum:yyyy.MM.dd}";
        }


        void Start()
        {
            Jogosultságkiosztás();

            Kiürít_Füleket();

            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\Beosztás\{Dátum.Year}\Ebeosztás{Dátum:yyyyMM}.mdb";

            string jelszó = "kiskakas";
            if (!Exists(hely))
                return;

            string szöveg = $"SELECT * FROM beosztás WHERE Dolgozószám='{Hrazonosító.Trim()}' AND nap=#{Dátum:MM-dd-yyyy}#";
            Adat = Kéz.Egy_Adat(hely, jelszó, szöveg);

            Kiegészítő_doboz();
            Fülek.SelectedIndex = Fülszám;
            Fülekkitöltése();
        }


        #region Alap

        private void Jogosultságkiosztás()
        {

            int melyikelem;
            //idő engedély
            TúlóraRögzítés.Visible = false;
            Túlóratörlés.Visible = false;

            CsúsztatásTörlés.Visible = false;
            CsúsztatásRögzítés.Visible = false;

            SzabadságRögzítés.Visible = false;

            MegjegyzésRögzítés.Visible = false;

            AftTörlés.Visible = false;
            AftRögzítés.Visible = false;

            // ide kell az összes gombot tenni amit szabályozni akarunk false
            TúlóraRögzítés.Enabled = false;
            Túlóratörlés.Enabled = false;

            CsúsztatásTörlés.Enabled = false;
            CsúsztatásRögzítés.Enabled = false;

            SzabadságRögzítés.Enabled = false;

            MegjegyzésRögzítés.Enabled = false;

            AftTörlés.Enabled = false;
            AftRögzítés.Enabled = false;

            melyikelem = 22;
            int Választék;
            if (Dátum == DateTime.Today)
            {
                Választék = 0;
            }
            else if (Dátum > DateTime.Today)
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
                        if (MyF.Vanjoga(melyikelem, 1))
                            Gombok_látszik();
                        break;
                    }
                case 0:
                    {
                        if (MyF.Vanjoga(melyikelem, 2))
                            Gombok_látszik();
                        break;
                    }
                case 1:
                    {
                        if (MyF.Vanjoga(melyikelem, 3))
                            Gombok_látszik();
                        break;
                    }
            }

            melyikelem = 23;
            // módosítás 1 Túlóra rögzítés
            if (MyF.Vanjoga(melyikelem, 1))
            {
                TúlóraRögzítés.Enabled = true;
            }
            // módosítás 2 túlóra törlés
            if (MyF.Vanjoga(melyikelem, 2))
            {
                Túlóratörlés.Enabled = true;
            }
            // módosítás 3
            if (MyF.Vanjoga(melyikelem, 3))
            {
            }

            melyikelem = 24;
            // módosítás 1 Csúsztatás rögzítés
            if (MyF.Vanjoga(melyikelem, 1))
            {
                CsúsztatásRögzítés.Enabled = true;
            }
            // módosítás 2 Csúsztatás törlés
            if (MyF.Vanjoga(melyikelem, 2))
            {
                CsúsztatásTörlés.Enabled = true;
            }
            // módosítás 3
            if (MyF.Vanjoga(melyikelem, 3))
            {
            }

            melyikelem = 25;
            // módosítás 1 Szabadság rögzítés
            if (MyF.Vanjoga(melyikelem, 1))
            {

                SzabadságRögzítés.Enabled = true;
            }
            // módosítás 2 Megjegyzés rögzítás
            if (MyF.Vanjoga(melyikelem, 2))
            {
                MegjegyzésRögzítés.Enabled = true;
            }
            // módosítás 3
            if (MyF.Vanjoga(melyikelem, 3))
            {
            }

            melyikelem = 26;
            // módosítás 1 AFT rögzítés
            if (MyF.Vanjoga(melyikelem, 1))
            {

                AftRögzítés.Enabled = true;
            }
            // módosítás 2 AFT törlés
            if (MyF.Vanjoga(melyikelem, 2))
            {
                AftTörlés.Enabled = true;
            }
            // módosítás 3
            if (MyF.Vanjoga(melyikelem, 3))
            {
            }

            melyikelem = 27;
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

            melyikelem = 28;
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


        void Gombok_látszik()
        {
            TúlóraRögzítés.Visible = true;
            Túlóratörlés.Visible = true;

            CsúsztatásTörlés.Visible = true;
            CsúsztatásRögzítés.Visible = true;

            SzabadságRögzítés.Visible = true;

            MegjegyzésRögzítés.Visible = true;

            AftTörlés.Visible = true;
            AftRögzítés.Visible = true;
        }


        private void Fülek_DrawItem(object sender, DrawItemEventArgs e)
        {
            {
                // Határozza meg, hogy melyik lap van jelenleg kiválasztva
                TabPage SelectedTab = Fülek.TabPages[e.Index];

                // Szerezze be a lap fejlécének területét
                Rectangle HeaderRect = Fülek.GetTabRect(e.Index);

                // Hozzon létreecsetet a szöveg megfestéséhez
                SolidBrush BlackTextBrush = new SolidBrush(Color.Black);

                // Állítsa be a szöveg igazítását
                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                // Festse meg a szöveget a megfelelő félkövér és szín beállítással
                if ((e.State & DrawItemState.Selected) != 0)
                {
                    Font BoldFont = new Font(Fülek.Font.Name, Fülek.Font.Size, FontStyle.Bold);
                    // háttér szín beállítása
                    e.Graphics.FillRectangle(new SolidBrush(Color.DarkGray), e.Bounds);
                    Rectangle paddedBounds = e.Bounds;
                    paddedBounds.Inflate(0, 0);
                    e.Graphics.DrawString(SelectedTab.Text, BoldFont, BlackTextBrush, paddedBounds, sf);
                }
                else
                {
                    e.Graphics.DrawString(SelectedTab.Text, e.Font, BlackTextBrush, HeaderRect, sf);
                }
                // Munka kész – dobja ki a keféket
                BlackTextBrush.Dispose();
            }
        }


        private void Kiegészítő_doboz()
        {
            try
            {
                //Ha üres volt akkor a választott szerint különbez az előző
                string Kód = BeosztáskódVálasztott.Trim();
                if (BeosztáskódElőző.Trim() != "")
                    Kód = BeosztáskódElőző.Trim();


                // ha üres akkor csak megjegyzés és csúsztatás
                if (Kód.Trim() == "")
                {
                    TúlóraRögzítés.Visible = false;
                    KitöltésMintával.Visible = false;
                    Túlóratörlés.Visible = false;
                    SzabadságRögzítés.Visible = false;

                }
                // ha beteg vagy afin van akkor
                if (Kód.Trim().Length > 0 && (MyF.Szöveg_Tisztítás(Kód, 0, 1) == "B" || MyF.Szöveg_Tisztítás(Kód, 0, 1) == "A"))
                {
                    SzabadságRögzítés.Visible = false;
                    CsúsztatásRögzítés.Visible = false;
                    CsúsztatásTörlés.Visible = false;
                    TúlóraRögzítés.Visible = false;
                    Túlóratörlés.Visible = false;
                    KitöltésMintával.Visible = false;
                }
                // szabadság akkor nem enged módosítani
                if (Kód.Trim().Length > 0 && MyF.Szöveg_Tisztítás(Kód, 0, 1) == "S")
                {
                    SzabadságRögzítés.Visible = true;
                    TúlóraRögzítés.Visible = false;
                    Túlóratörlés.Visible = false;
                    KitöltésMintával.Visible = false;
                    AftRögzítés.Visible = false;
                    AftTörlés.Visible = false;
                }
                else
                {
                    SzabadságRögzítés.Visible = false;
                    SzabadságOka.Text = "";
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



        #region Fülek kitöltése

        private void Fülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }


        private void Fülekkitöltése()
        {
            switch (Fülek.SelectedIndex)
            {
                case 0:
                    {
                        Túlóra_kiírása();
                        break;
                    }
                case 1:
                    {
                        Csúsztatás_kiírása();
                        break;
                    }
                case 2:
                    {
                        Szabadságokokfeltöltése();
                        Szabadság_kiírása();
                        break;
                    }
                case 3:
                    {
                        Megjegyzés_kiírása();
                        break;
                    }
                case 4:
                    {
                        AFT_kiírása();
                        break;
                    }
            }
        }


        private void Kiürít_Füleket()
        {

            Túlóra.Text = "";
            Túlórakezd.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);
            Túlóravég.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);
            TúlóraOk.Text = "";

            Csúszóra.Text = "";
            Csúszórakezd.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);
            CsúszóraVég.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);
            CsúszOk.Text = "";

            SzabadságOka.Text = "";

            Kértnap.Checked = false;
            Megjegyzés.Text = "";

            AFTok.Text = "";
            AFTóra.Text = "";
        }
        #endregion



        #region Túlóra lapfül
        private void Túlóra_kiírása()
        {
            try
            {
                if (Adat != null)
                {
                    Túlóra.Text = Adat.Túlóra.ToString();
                    Túlórakezd.Value = Adat.Túlórakezd;
                    Túlóravég.Value = Adat.Túlóravég;
                    TúlóraOk.Text = Adat.Túlóraok;
                    ÉvesTúlóra.Text = (BR.Évestúlóra_Keret_Figyelés(Cmbtelephely, Dátum, Hrazonosító) / 60).ToString();

                    if (BeosztáskódVálasztott.Trim() != "")
                    {
                        string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\Segéd\kiegészítő.mdb";
                        if (!Exists(hely))
                            return;
                        string jelszó = "Mocó";
                        string szöveg = $"SELECT * FROM beosztáskódok  WHERE beosztáskód='{BeosztáskódVálasztott.Trim()}'";

                        Kezelő_Kiegészítő_Beosztáskódok KézBeo = new Kezelő_Kiegészítő_Beosztáskódok();
                        Adat_Kiegészítő_Beosztáskódok Rekord = KézBeo.Egy_Adat(hely, jelszó, szöveg);
                        if (Rekord != null)
                        {
                            Túlórakezd.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, Rekord.Munkaidőkezdet.Hour, Rekord.Munkaidőkezdet.Minute, Rekord.Munkaidőkezdet.Second);
                            Túlóravég.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, Rekord.Munkaidővége.Hour, Rekord.Munkaidővége.Minute, Rekord.Munkaidővége.Second);
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


        private void Túlóratörlés_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime ideigDátum = new DateTime(1900, 1, 1, 0, 0, 0);
                BR.Rögzít_Túlóra(Cmbtelephely, Dátum, BeosztáskódVálasztott, Hrazonosító, Ledolgozott, ideigDátum, ideigDátum, 0, "", DolgozóNév);
                Változás?.Invoke();
                this.Close();
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


        private void KitöltésMintával_Click(object sender, EventArgs e)
        {
            KitöltésMintával_sub();
        }


        private void KitöltésMintával_sub()
        {
            try
            {
                Túlóra.Text = "0";
                // megkeressük a beosztáskódhoz tertozó sablont
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Kiegészítő1.mdb";
                string jelszó = "Mocó";
                string szöveg = $"SELECT * FROM beosegéd";

                Kezelő_Kiegészítő_Beosegéd KézBeo = new Kezelő_Kiegészítő_Beosegéd();
                List<Adat_Kiegészítő_Beosegéd> Adatok = KézBeo.Lista_Adatok(hely, jelszó, szöveg);

                Adat_Kiegészítő_Beosegéd Rekord = (from a in Adatok
                                                   where a.Beosztáskód == BeosztáskódVálasztott && a.Telephely == Cmbtelephely.Trim()
                                                   select a).FirstOrDefault();
                if (Rekord != null)
                {
                    // ha volt akkor kitölti a telephelyi sajátosságoknak megfelelően
                    Túlóra.Text = Rekord.Túlóra.ToString();
                    Túlórakezd.Value = Rekord.Kezdőidő;
                    Túlóravég.Value = Rekord.Végeidő;
                    TúlóraOk.Text = Rekord.Túlóraoka.Trim();
                }
                else
                {
                    // ha nem volt akkor az általánost tölti be
                    Rekord = (from a in Adatok
                              where a.Beosztáskód == BeosztáskódVálasztott && a.Telephely == "_"
                              select a).FirstOrDefault();

                    if (Rekord != null)
                    {
                        Túlóra.Text = Rekord.Túlóra.ToString();
                        Túlórakezd.Value = Rekord.Kezdőidő;
                        Túlóravég.Value = Rekord.Végeidő;
                        TúlóraOk.Text = Rekord.Túlóraoka.Trim();
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


        private void TúlóraRögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Túlóravég.Value == Túlórakezd.Value) throw new HibásBevittAdat("A Túlóra kezdetének és a végének különbözni kell!");
                if (TúlóraOk.Text.Trim() == "") throw new HibásBevittAdat("A Túlóra okát ki kell tölteni!");
                TúlóraOk.Text = MyF.Szöveg_Tisztítás(TúlóraOk.Text, 0, 240);
                if (!int.TryParse(Túlóra.Text, out int túlóra)) throw new HibásBevittAdat("A Túlóra órájának egész számnak kell lennie és nullánál nagyobbnak számnak kell lennie!");

                // a szövegnek tartalmaznia kell & jelet
                bool túlóravolt = false;
                string ideig = MyF.Szöveg_Tisztítás(TúlóraOk.Text, 0, 3);
                if (ideig == "&eb" || ideig == "&EB") túlóravolt = true;
                if (ideig == "&ep" || ideig == "&EP") túlóravolt = true;
                if (ideig == "&v" || ideig == "&V") túlóravolt = true;
                ideig = MyF.Szöveg_Tisztítás(TúlóraOk.Text, 0, 2);
                if (ideig == "&t" || ideig == "&T") túlóravolt = true;
                if (!túlóravolt) throw new HibásBevittAdat("A Túlóra okának &T, &EB, &EP, &V- vel kell kezdődnie!");

                string hely;
                int parancs = BR.Túlóra_Keret_Ellenőrzés(Cmbtelephely, Dátum, Hrazonosító);
                switch (parancs)
                {
                    case 1:
                        {
                            DirectoryInfo Directories = new DirectoryInfo($@"{Application.StartupPath}\Főmérnökség\adatok\dokumentumok");
                            string mialapján = $"{Hrazonosító.Trim()}_tul_{Dátum.Year}*.pdf";
                            FileInfo[] fileInfo = Directories.GetFiles(mialapján, SearchOption.TopDirectoryOnly);

                            if (fileInfo.Count() < 1)
                                throw new HibásBevittAdat("A Túlórát dolgozói nyilatkozat hiányában nem lehet rögzíteni!");
                            break;
                        }
                    case 5:
                        {
                            hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\Dolgozók.mdb";
                            string jelszó = "forgalmiutasítás";
                            string szöveg = $" select * from  dolgozóadatok where dolgozószám='{Hrazonosító.Trim()}'";

                            Kezelő_Dolgozó_Alap KéZalap = new Kezelő_Dolgozó_Alap();
                            Adat_Dolgozó_Alap Dolgozó = KéZalap.Egy_Adat(hely, jelszó, szöveg);

                            if (Dolgozó != null)
                            {
                                if (Dolgozó.Túlóraeng)
                                {
                                    szöveg = $"UPDATE dolgozóadatok SET túlóraeng=false WHERE dolgozószám='{Hrazonosító.Trim()}'";
                                    MyA.ABMódosítás(hely, jelszó, szöveg);
                                }
                                else
                                    throw new HibásBevittAdat("A Túlóra engedélyezése nem történt meg, így ennek hiányában nem lehet rögzíteni!");
                            }

                            break;
                        }
                    case 9:
                        {
                            throw new HibásBevittAdat("A dolgozó elérte a maximális túlóra keretét, így nem rögzíthető több neki!");
                        }

                }
                BR.Rögzít_Túlóra(Cmbtelephely, Dátum, BeosztáskódVálasztott, Hrazonosító, Ledolgozott, Túlórakezd.Value, Túlóravég.Value, túlóra, TúlóraOk.Text.Trim(), DolgozóNév);
                Változás?.Invoke();
                this.Close();
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



        private void Túlóra_Leave(object sender, EventArgs e)
        {
            try
            {

                if (Túlóra.Text.Trim() == "")
                    return;
                if (!int.TryParse(Túlóra.Text, out int túlóra))
                    return;

                if (túlóra == 695)
                    Túlóravég.Value = Túlórakezd.Value.AddMinutes(720);
                else
                    Túlóravég.Value = Túlórakezd.Value.AddMinutes(túlóra);

                TúlóraOk.Text = "&" + TúlóraOk.Text.Trim();

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


        private void Túlórakezd_Leave(object sender, EventArgs e)
        {

            try
            {
                if (Túlóra.Text.Trim() == "")
                    return;
                if (!int.TryParse(Túlóra.Text, out int túlóra))
                    return;
                Túlóravég.Value = Túlórakezd.Value.AddMinutes(túlóra);
                TúlóraOk.Text = "&";

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



        #region Csúsztatás lapfül
        private void Csúsztatás_kiírása()
        {
            try
            {
                if (Adat != null)
                {
                    Csúszóra.Text = Adat.Csúszóra.ToString();
                    Csúszórakezd.Value = Adat.CSúszórakezd;
                    CsúszóraVég.Value = Adat.Csúszóravég;
                    CsúszOk.Text = Adat.Csúszok.Trim();
                }
                else
                {
                    DateTime ideig = new DateTime(1900, 1, 1, 0, 0, 0);
                    Csúszóra.Text = "";
                    Csúszórakezd.Value = ideig;
                    CsúszóraVég.Value = ideig;
                    CsúszOk.Text = "";

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


        private void CsúsztatásTörlés_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime Dátumpót = new DateTime(1900, 1, 1, 0, 0, 0);
                BR.Rögzít_Csúsztatás(Cmbtelephely, Dátum, BeosztáskódVálasztott, Hrazonosító, Ledolgozott, Dátumpót, Dátumpót, 0, "", DolgozóNév);
                BR.Ellenőrzés_Csúsztatás(Cmbtelephely, Dátum, Hrazonosító);
                Változás?.Invoke();
                this.Close();
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


        private void CsúsztatásRögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                if (CsúszóraVég.Value == Csúszórakezd.Value)
                    throw new HibásBevittAdat("A Csúsztatás kezdetének és a végének különbözni kell!");
                if (CsúszOk.Text.Trim() == "")
                    throw new HibásBevittAdat("A Csúsztatás okát ki kell tölteni!");
                if (!int.TryParse(Csúszóra.Text, out int csúszóra))
                    throw new HibásBevittAdat("A Csúsztatás óráját ki kell tölteni és egész számnak kell lennie!");

                BR.Rögzít_Csúsztatás(Cmbtelephely, Dátum, BeosztáskódVálasztott, Hrazonosító, Ledolgozott, Csúszórakezd.Value, CsúszóraVég.Value, csúszóra, CsúszOk.Text.Trim(), DolgozóNév);
                Változás?.Invoke();
                this.Close();

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

        private void Csúszóra_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Csúszóra.Text, out int csúszóra)) return;
                if (csúszóra < 0) csúszóra = -1 * csúszóra;
                CsúszóraVég.Value = Csúszórakezd.Value.AddHours(csúszóra);

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

        private void Csúszórakezd_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Csúszóra.Text, out int csúszóra)) return;
                if (csúszóra < 0) csúszóra = -1 * csúszóra;
                CsúszóraVég.Value = Csúszórakezd.Value.AddHours(csúszóra);

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



        #region Szabadság lapfül
        private void Szabadságokokfeltöltése()
        {
            try
            {
                SzabadságOka.Items.Clear();
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\Segéd\kiegészítő.mdb";
                string jelszó = "Mocó";
                string szöveg = "SELECT * FROM szabadságok WHERE megnevezés like '%kivétel%' ";

                SzabadságOka.BeginUpdate();
                SzabadságOka.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "megnevezés"));
                SzabadságOka.EndUpdate();
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


        private void Szabadság_kiírása()
        {
            try
            {
                if (Adat != null)
                    SzabadságOka.Text = Adat.Szabiok.Trim();
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


        private void SzabadságRögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                if (SzabadságOka.Text.Trim() == "")
                    throw new HibásBevittAdat("Az Szabadság okát ki kell tölteni!");


                BR.Rögzít_Szabadság(Cmbtelephely, Dátum, BeosztáskódVálasztott, Hrazonosító, Ledolgozott, SzabadságOka.Text.Trim(), DolgozóNév.Trim());

                Változás?.Invoke();
                this.Close();
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



        #region Megjegyzés lapfül
        private void Megjegyzés_kiírása()
        {
            try
            {
                if (Adat != null)
                {
                    Megjegyzés.Text = Adat.Megjegyzés.Trim();
                    Kértnap.Checked = Adat.Kért;
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


        private void MegjegyzésRögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                BR.Rögzít_Megjegyzés(Cmbtelephely, Dátum, Hrazonosító, Megjegyzés.Text.Trim(), Kértnap.Checked, DolgozóNév.Trim());
                Változás?.Invoke();
                this.Close();
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



        #region AFT lapfül
        private void AFT_kiírása()
        {
            try
            {
                if (Adat != null)
                {
                    AFTóra.Text = Adat.AFTóra.ToString();
                    AFTok.Text = Adat.AFTok;
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


        private void AftTörlés_Click(object sender, EventArgs e)
        {
            try
            {
                BR.Rögzít_AFT(Cmbtelephely, Dátum, BeosztáskódVálasztott, Hrazonosító, Ledolgozott, "", 0, "");
                Változás?.Invoke();
                this.Close();
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


        private void AftRögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                if (AFTok.Text.Trim() == "")
                    throw new HibásBevittAdat("Az AFT okát ki kell tölteni!");
                if (!int.TryParse(AFTóra.Text, out int AftÓra))
                    throw new HibásBevittAdat("Az AFT percének számnak kell lennie!");

                BR.Rögzít_AFT(Cmbtelephely, Dátum, BeosztáskódVálasztott, Hrazonosító, Ledolgozott, AFTok.Text.Trim(), AftÓra, DolgozóNév);
                Változás?.Invoke();
                this.Close();
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
