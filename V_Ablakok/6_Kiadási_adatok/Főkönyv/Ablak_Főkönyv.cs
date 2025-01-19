using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos.Kezelők;
using Villamos.Villamos_Ablakok._6_Kiadási_adatok.Főkönyv;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using Villamos.Villamos_Nyomtatványok;
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    public partial class Ablak_Főkönyv
    {
        string Telephely_;
        string fájlnév_;
        DateTime Dátum_;
        string napszak_;
        string szövegd_;
        string Papírméret_;
        string PapírElrendezés_;
        int ZSER_tábla_sor = -1;

        public string HelyNap = "";
        public string HelyZser = "";

        readonly Kezelő_Szerelvény KézSzerelvény = new Kezelő_Szerelvény();
        readonly Kezelő_Nap_Hiba KézNapiHiba = new Kezelő_Nap_Hiba();
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly Kezelő_Főkönyv_Zser_Km KézFőZserKm = new Kezelő_Főkönyv_Zser_Km();
        readonly Kezelő_Kiegészítő_Igen_Nem KézKiegIgenNem = new Kezelő_Kiegészítő_Igen_Nem();
        readonly Kezelő_Főkönyv_Nap KézFőkönyvNap = new Kezelő_Főkönyv_Nap();
        readonly Kezelő_Reklám KézReklám = new Kezelő_Reklám();
        readonly Kezelő_Főkönyv_ZSER KézFőkönyvZSER = new Kezelő_Főkönyv_ZSER();
        readonly Kezelő_Kiegészítő_Takarítás KézTakarításTípus = new Kezelő_Kiegészítő_Takarítás();
        readonly Kezelő_Jármű_Vendég KézFőJárműVendég = new Kezelő_Jármű_Vendég();


        public List<Adat_Reklám> AdatokReklám = new List<Adat_Reklám>();
        public List<Adat_Jármű> AdatokJármű = new List<Adat_Jármű>();
        public List<Adat_Jármű> AdatokFőJármű = new List<Adat_Jármű>();
        public List<Adat_Nap_Hiba> AdatokNapiHiba = new List<Adat_Nap_Hiba>();
        public List<Adat_Szerelvény> AdatokSzerelvény = new List<Adat_Szerelvény>();
        public List<Adat_Kiegészítő_Igen_Nem> AdatokIgenNem = new List<Adat_Kiegészítő_Igen_Nem>();
        public List<Adat_Főkönyv_Nap> AdatokFőkönyvNap = new List<Adat_Főkönyv_Nap>();
        public List<Adat_Főkönyv_ZSER> AdatokFőkönyvZSER = new List<Adat_Főkönyv_ZSER>();
        public List<string> AdatokTakarításTípus = new List<string>();
        public List<Adat_Jármű_Vendég> AdatokFőVendég = new List<Adat_Jármű_Vendég>();

        //Ablak_Kereső Keres;
        public Ablak_Főkönyv()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            Telephelyekfeltöltése();
            Jogosultságkiosztás();
            Papír();
        }

        void Papír()
        {
            Papírméret.Items.Add("--");
            Papírméret.Items.Add("A4");
            Papírméret.Items.Add("A3");
            Papírméret.Text = "--";

            PapírElrendezés.Items.Add("--");
            PapírElrendezés.Items.Add("Álló");
            PapírElrendezés.Items.Add("Fekvő");
            PapírElrendezés.Text = "--";
        }

        private void Ablak_Főkönyv_Load(object sender, EventArgs e)
        {

        }

        private void Ablak_Főkönyv_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kereső?.Close();
            Új_Ablak_Főkönyv_Napi?.Close();
            Új_Ablak_Főkönyv_Zser_Másol?.Close();
            Új_Ablak_Főkönyv_Napi_Adatok?.Close();
        }


        private void Ablak_Főkönyv_Shown(object sender, EventArgs e)
        {
            Dátum.Value = DateTime.Today;
            Idődátum.Value = DateTime.Today;
            Időidő.Value = DateTime.Now;

            Részletes_ürítés();

            // megnézzük, hogy létezik-e az éves tábla fájl
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\kiadás" + Dátum.Value.ToString("yyyy") + ".mdb";
            if (!System.IO.File.Exists(hely)) Adatbázis_Létrehozás.Kiadásiösszesítőtábla(hely);

            // megnézzük, hogy létezik-e az éves tábla fájl
            hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\személyzet" + Dátum.Value.ToString("yyyy") + ".mdb";
            if (!System.IO.File.Exists(hely)) Adatbázis_Létrehozás.Személyzetösszesítőtábla(hely);

            // megnézzük, hogy létezik-e az éves tábla fájl
            hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\típuscsere" + Dátum.Value.ToString("yyyy") + ".mdb";
            if (!System.IO.File.Exists(hely)) Adatbázis_Létrehozás.Tipuscsereösszesítőtábla(hely);

            // megnézzük, hogy létezik-e az üzemben már a fájl
            hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos2.mdb";
            if (!System.IO.File.Exists(hely)) Adatbázis_Létrehozás.Villamostábla(hely);

            // főkönyvi adatok új struktúrája
            // új adatok helye
            hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\" + DateTime.Now.ToString("yyyy");
            if (!System.IO.Directory.Exists(hely)) System.IO.Directory.CreateDirectory(hely);

            hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\Főkönyv\" + DateTime.Now.ToString("yyyy") + @"\Zser";
            if (!System.IO.Directory.Exists(hely)) System.IO.Directory.CreateDirectory(hely);
            hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\Főkönyv\" + DateTime.Now.ToString("yyyy") + @"\Nap";
            if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

            if (DateTime.Now.Hour > 10)
                Délutáni.Checked = true;
            else
                Délelőtt.Checked = true;

            Eredménytábla();
            Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;
            Óráig.Value = new DateTime(Dátum.Value.Year, Dátum.Value.Month, Dátum.Value.Day, 11, 0, 0);
            Fájlnévbeállítás(Dátum.Value);
            JárműListaFeltöltés();
            Jogosultságkiosztás();
            Gombok();
        }

        #region Alap
        private void Fájlnévbeállítás(DateTime dátum)
        {
            HelyNap = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\{dátum.Year}\nap\{dátum:yyyyMMdd}";
            if (Délelőtt.Checked)
                HelyNap += "denap.mdb";
            else
                HelyNap += "dunap.mdb";

            HelyZser = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\{dátum.Year}\zser\zser{dátum:yyyyMMdd}";

            if (Délelőtt.Checked)
                HelyZser += "de.mdb";
            else
                HelyZser += "du.mdb";
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Holtart.Lép();
        }

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                Cmbtelephely.Items.AddRange(Listák.TelephelyLista_Jármű());
                if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér)
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

        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\Főkönyv.html";
            MyE.Megnyitás(hely);
        }

        private void Jogosultságkiosztás()
        {
            int melyikelem;


            // ide kell az összes gombot tenni amit szabályozni akarunk false
            Program_adatok.Visible = false;
            Zserbeolvasás.Visible = false;
            ZSERellenőrzés.Visible = false;

            Button5.Visible = false;
            Beállólista.Visible = false;
            Meghagyás.Visible = false;
            Haromnapos.Visible = false;

            Főkönyv.Visible = false;
            ZSERellenőrzés.Visible = false;
            Zserbeolvasás.Visible = false;
            Program_adatok.Visible = false;

            R_törlés.Visible = false;
            R_rögzít.Visible = false;
            Járműpanel_OK.Visible = false;

            ZSER_módosítás.Visible = false;
            ZSER_másol.Visible = false;

            melyikelem = 96;
            // módosítás 1 
            if (MyF.Vanjoga(melyikelem, 1))

            {
                ZSER_módosítás.Visible = true;
                ZSER_másol.Visible = true;

            }
            // módosítás 2 
            if (MyF.Vanjoga(melyikelem, 2))

            {
                R_törlés.Visible = true;
                R_rögzít.Visible = true;
            }
            // módosítás 3
            if (MyF.Vanjoga(melyikelem, 3))

            {
                Járműpanel_OK.Visible = true;
            }

            melyikelem = 97;
            // módosítás 1 
            if (MyF.Vanjoga(melyikelem, 1))

            {

                // visszamenő rögzítés a gombok menüben
            }
            // módosítás 2 
            if (MyF.Vanjoga(melyikelem, 2))

            {

                Button5.Visible = true;
                Beállólista.Visible = true;
                Meghagyás.Visible = true;
                Haromnapos.Visible = true;
            }
            // módosítás 3
            if (MyF.Vanjoga(melyikelem, 3))

            {
                Főkönyv.Visible = true;
            }

            melyikelem = 98;
            // módosítás 1 
            if (MyF.Vanjoga(melyikelem, 1))

            {
                Program_adatok.Visible = true;

            }
            // módosítás 2 
            if (MyF.Vanjoga(melyikelem, 2))

            {
                Zserbeolvasás.Visible = true;

            }
            // módosítás 3
            if (MyF.Vanjoga(melyikelem, 3))

            {
                ZSERellenőrzés.Visible = true;
            }
        }

        private void Ablak_Jármű_állapotok_KeyDown(object sender, KeyEventArgs e)
        {
            //ctrl+F
            if (e.Control && e.KeyCode == Keys.F)
            {
                switch (Fülek.SelectedIndex)
                {
                    case 0:
                        {
                            break;
                        }

                    case 1:
                        {
                            Kereső_hívás("zser");
                            break;
                        }

                    case 2:
                        {
                            Kereső_hívás("zseridő");
                            break;
                        }

                    case 3:
                        {
                            Kereső_hívás("napi");
                            break;
                        }

                    case 4:
                        {
                            break;
                        }
                    case 5:
                        {
                            break;
                        }
                }
            }
        }

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
                        break;
                    }

                case 1:
                    {
                        break;
                    }

                case 2:
                    {
                        break;
                    }

                case 3:
                    {
                        break;
                    }

                case 4:
                    {
                        Napszak_Conmbofeltöltés();
                        break;
                    }
                case 5:
                    {

                        Reklámot_üzen();
                        break;
                    }
            }
        }

        private void Fülek_DrawItem(object sender, DrawItemEventArgs e)
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

        #endregion


        #region Gombok kezelése
        private void Gombok()
        {
            // ha nincs fájl akkor a gombok nem aktívak
            if (System.IO.File.Exists(HelyNap))
            {
                Beállólista.Enabled = true;
                Főkönyv.Enabled = true;
                Button5.Enabled = true;
                Jegykezelő.Enabled = true;
            }
            else
            {
                Beállólista.Enabled = false;
                Főkönyv.Enabled = false;
                Button5.Enabled = false;
                Jegykezelő.Enabled = false;
            }

            Program_adatok.Enabled = true;
            ZSERellenőrzés.Enabled = true;
            Zserbeolvasás.Enabled = true;

            // ha 10 óra után  van és délélelőtt van kijelölve és a mai napot akarjuk módosítani, akkor
            if (DateTime.Now.Hour >= 10 && Délelőtt.Checked && DateTime.Today.ToString("yyyy.MM.dd") == Dátum.Value.ToString("yyyy.MM.dd"))
            {
                Program_adatok.Enabled = false;
                ZSERellenőrzés.Enabled = false;
                Zserbeolvasás.Enabled = false;

            }

            // ha 10 óra előtt  van és délután van kijelölve és a mai napot akarjuk módosítani, akkor
            if (DateTime.Now.Hour < 10 && Délutáni.Checked == true && DateTime.Today.ToString("yyyy.MM.dd") == Dátum.Value.ToString("yyyy.MM.dd"))
            {
                Program_adatok.Enabled = false;
                ZSERellenőrzés.Enabled = false;
                Zserbeolvasás.Enabled = false;
            }

            // ha van joga visszamenőleg rögzíteni, akkor visszaadjuk
            int melyikelem = 97;
            // módosítás 1 

            if (MyF.Vanjoga(melyikelem, 1))
            {
                // visszamenő rögzítés a gombok menüben
                Program_adatok.Enabled = true;
                ZSERellenőrzés.Enabled = true;
                Zserbeolvasás.Enabled = true;
            }
        }

        private void Délelőtt_Click(object sender, EventArgs e)
        {
            Fájlnévbeállítás(Dátum.Value);
            Eredménytábla();
            Gombok();
        }

        private void Délutáni_Click(object sender, EventArgs e)
        {
            Fájlnévbeállítás(Dátum.Value);
            Eredménytábla();
            Gombok();
        }

        private void Dátum_ValueChanged(object sender, EventArgs e)
        {
            Fájlnévbeállítás(Dátum.Value);
            Eredménytábla();
            Gombok();
            Óráig.Value = new DateTime(Dátum.Value.Year, Dátum.Value.Month, Dátum.Value.Day, 11, 0, 0);
        }

        private void Eredménytábla()
        {
            Tábla.Rows.Clear();
            Tábla.Columns.Clear();
            Tábla.Refresh();
            Tábla.Visible = false;
            Tábla.ColumnCount = 9;

            // fejléc elkészítése
            Tábla.Columns[0].HeaderText = "Visz.";
            Tábla.Columns[0].Width = 60;
            Tábla.Columns[1].HeaderText = "F.sz.";
            Tábla.Columns[1].Width = 60;
            Tábla.Columns[2].HeaderText = "Kocsi 1";
            Tábla.Columns[2].Width = 70;
            Tábla.Columns[3].HeaderText = "Kocsi 2";
            Tábla.Columns[3].Width = 70;
            Tábla.Columns[4].HeaderText = "Kocsi 3";
            Tábla.Columns[4].Width = 70;
            Tábla.Columns[5].HeaderText = "Kocsi 4";
            Tábla.Columns[5].Width = 70;
            Tábla.Columns[6].HeaderText = "Kocsi 5";
            Tábla.Columns[6].Width = 70;
            Tábla.Columns[7].HeaderText = "Kocsi 6";
            Tábla.Columns[7].Width = 70;
            Tábla.Columns[8].HeaderText = "Hiba";
            Tábla.Columns[8].Width = 700;

            // megnézzük, hogy létezik-e adott napi tábla
            if (!System.IO.File.Exists(HelyZser)) return;

            FőkönyZserListaFeltöltés(HelyZser);
            int i;
            foreach (Adat_Főkönyv_ZSER rekord in AdatokFőkönyvZSER)
            {
                if (rekord.Napszak.Trim() == "DE" || rekord.Napszak.Trim() == "DU")
                {
                    if (rekord.Ellenőrző.Trim().Contains("1"))
                    {
                        if (rekord.Napszak.Trim() != "*")
                        {
                            Tábla.RowCount += 1;
                            i = Tábla.RowCount - 1;

                            Tábla.Rows[i].Cells[0].Value = rekord.Viszonylat.Trim();

                            Tábla.Rows[i].Cells[1].Value = rekord.Forgalmiszám.ToString();

                            if (rekord.Kocsi1.Trim() != "_")
                                Tábla.Rows[i].Cells[2].Value = rekord.Kocsi1.Trim();
                            if (rekord.Ellenőrző.Substring(1, 1) == "1")
                            {
                                Tábla.Rows[i].Cells[2].Style.BackColor = Color.IndianRed;
                                Tábla.Rows[i].Cells[8].Value += rekord.Kocsi1.Trim() + "- Nincs ilyen kocsi a telephelyen, ";
                            }

                            if (rekord.Kocsi2.Trim() != "_")
                                Tábla.Rows[i].Cells[3].Value = rekord.Kocsi2.Trim();
                            if (rekord.Ellenőrző.Substring(2, 1) == "1")
                            {
                                Tábla.Rows[i].Cells[3].Style.BackColor = Color.IndianRed;
                                Tábla.Rows[i].Cells[8].Value += rekord.Kocsi2.Trim() + "- Nincs ilyen kocsi a telephelyen, ";
                            }

                            if (rekord.Kocsi3.Trim() != "_")
                                Tábla.Rows[i].Cells[4].Value = rekord.Kocsi3.Trim();
                            if (rekord.Ellenőrző.Substring(3, 1) == "1")
                            {
                                Tábla.Rows[i].Cells[4].Style.BackColor = Color.IndianRed;
                                Tábla.Rows[i].Cells[8].Value += rekord.Kocsi3.Trim() + "- Nincs ilyen kocsi a telephelyen, ";
                            }

                            if (rekord.Kocsi4.Trim() != "_")
                                Tábla.Rows[i].Cells[5].Value = rekord.Kocsi4.Trim();
                            if (rekord.Ellenőrző.Substring(4, 1) == "1")
                            {
                                Tábla.Rows[i].Cells[5].Style.BackColor = Color.IndianRed;
                                Tábla.Rows[i].Cells[8].Value += rekord.Kocsi4.Trim() + "- Nincs ilyen kocsi a telephelyen, ";
                            }

                            if (rekord.Kocsi5.Trim() != "_")
                                Tábla.Rows[i].Cells[6].Value = rekord.Kocsi5.Trim();
                            if (rekord.Ellenőrző.Substring(5, 1) == "1")
                            {
                                Tábla.Rows[i].Cells[6].Style.BackColor = Color.IndianRed;
                                Tábla.Rows[i].Cells[8].Value += rekord.Kocsi5.Trim() + "- Nincs ilyen kocsi a telephelyen, ";
                            }

                            if (rekord.Kocsi6.Trim() != "_")
                                Tábla.Rows[i].Cells[7].Value = rekord.Kocsi6.Trim();
                            if (rekord.Ellenőrző.Substring(6, 1) == "1")
                            {
                                Tábla.Rows[i].Cells[7].Style.BackColor = Color.IndianRed;
                                Tábla.Rows[i].Cells[8].Value += rekord.Kocsi6.Trim() + "- Nincs ilyen kocsi a telephelyen, ";
                            }

                            if (rekord.Ellenőrző.Substring(7, 1) == "1")
                            {
                                Tábla.Rows[i].Cells[1].Style.BackColor = Color.IndianRed;
                                Tábla.Rows[i].Cells[8].Value += "Szerelvény összeállítási hiba, ";
                            }
                        }
                    }
                }
            }
            if (Tábla.Rows.Count > 1)
            {
                Tábla.Visible = true;
                Label1.Visible = true;
            }
            else
            {
                Tábla.Visible = false;
                Label1.Visible = false;
            }
            Tábla.Visible = true;
            Tábla.Refresh();
            Tábla.ClearSelection();
            Rosszkiadása();
        }

        private void Rosszkiadása()
        {
            if (!System.IO.File.Exists(HelyNap)) return;
            FőkönyvNapListaFeltöltés(HelyNap);

            // egy üres sor
            Tábla.RowCount += 1;
            int i;
            foreach (Adat_Főkönyv_Nap rekord in AdatokFőkönyvNap)
            {

                if (rekord.Napszak.Trim() == "DE" || rekord.Napszak.Trim() == "DU")
                {
                    if (rekord.Státus == 4 && rekord.Viszonylat.Trim() != "-")
                    {
                        Tábla.RowCount += 1;
                        i = Tábla.RowCount - 1;
                        Tábla.Rows[i].Cells[0].Value = rekord.Viszonylat.Trim();
                        Tábla.Rows[i].Cells[1].Value = rekord.Forgalmiszám.Trim();
                        Tábla.Rows[i].Cells[2].Value = rekord.Azonosító.Trim();
                        Tábla.Rows[i].Cells[8].Value = "Üzemképtelen kocsi forgalomban => " + rekord.Hibaleírása.Trim();
                    }
                }
            }
            if (Tábla.Rows.Count > 1)
            {
                Tábla.Visible = true;
                Label1.Visible = true;
            }
            else
            {
                Tábla.Visible = false;
                Label1.Visible = false;
            }
            Tábla.Visible = true;
            Tábla.Refresh();
            Tábla.ClearSelection();
        }
        #endregion


        #region Háromnapos
        private void Haromnapos_Click(object sender, EventArgs e)
        {
            Papírméret.Text = "A4";
            PapírElrendezés.Text = "Fekvő";
            Papírméret_ = "A4";
            PapírElrendezés_ = "Fekvő";
            KiegIgeNemListaFeltöltés();

            Adat_Kiegészítő_Igen_Nem Elem = (from a in AdatokIgenNem
                                             where a.Id == 1
                                             select a).FirstOrDefault();

            // elosztja a járműveket ha hétfő van
            if (Elem.Válasz)
            {
                //újra osztjuk ha ez van beállítva
                if (DateTime.Today.ToString("dddd") == "hétfő") Osztás();
            }

            string fájlexc;

            // kimeneti fájl helye és neve
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog
            {
                InitialDirectory = "MyDocuments",
                Title = "E2 vizsgálati lap",
                FileName = "Háromnapos_nyomtatvány_" + Program.PostásNév.Trim() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                Filter = "Excel |*.xlsx"
            };
            // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
            if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                fájlexc = SaveFileDialog1.FileName;
            else
                return;


            Holtart.Be(100);
            timer1.Enabled = true;
            fájlnév_ = fájlexc.Trim();
            Telephely_ = Cmbtelephely.Text.Trim();

            SZál_háromnapos(() =>
            { //leállítjuk a számlálót és kikapcsoljuk a holtartot.
                timer1.Enabled = false;
                Holtart.Ki();
                MessageBox.Show("A nyomtatvány elkészült !", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
        }

        private void SZál_háromnapos(Action callback)
        {
            Thread proc = new Thread(() =>
            {
                Főkönyv_Funkciók.Napiállók(Telephely_);
                Főkönyv_Háromnapos nyomtatvány = new Főkönyv_Háromnapos();
                // elkészítjük a formanyomtatványt változókat nem lehet küldeni definiálni kell egy külső változót.
                nyomtatvány.Három_Nyomtatvány(fájlnév_, Telephely_, Papírméret_, PapírElrendezés_);

                this.Invoke(callback, new object[] { });
            });
            proc.Start();
        }

        private void Osztás()
        {
            Holtart.Be(100);
            // Minden létező kocsinak a napját 99-re állítjuk
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos2.mdb";
            string jelszó = "pozsgaii";


            //Minden kocsinál beállítjuk, hogy 99.
            string szöveg = "UPDATE Állománytábla SET Állománytábla.haromnapos = 99";
            MyA.ABMódosítás(hely, jelszó, szöveg);

            szöveg = "SELECT * FROM állománytábla ORDER BY azonosító";
            Kezelő_Jármű2 KJ2_kéz = new Kezelő_Jármű2();
            List<Adat_Jármű_2> Adatok2 = KJ2_kéz.Lista_Adatok(hely, jelszó, szöveg);


            // leellenőrizzük, hogy minden kocsi szerepel
            JárműListaFeltöltés();
            // Amik elmentek törlődnek
            foreach (Adat_Jármű_2 rekord in Adatok2)
            {
                Adat_Jármű Elem = (from a in AdatokJármű
                                   where a.Azonosító == rekord.Azonosító
                                   select a).FirstOrDefault();

                if (Elem == null)
                {
                    szöveg = $"DELETE FROM állománytábla Where azonosító='{rekord.Azonosító}'";
                    MyA.ABtörlés(hely, jelszó, szöveg);

                }
            }

            //Amik jöttek beíródnak
            foreach (Adat_Jármű rekord in AdatokJármű)
            {
                Adat_Jármű_2 Elem = (from a in Adatok2
                                     where a.Azonosító == rekord.Azonosító
                                     select a).FirstOrDefault();
                if (Elem == null)
                {
                    szöveg = "INSERT INTO állománytábla (azonosító, haromnapos, takarítás) VALUES (";
                    szöveg += $"'{rekord.Azonosító.Trim()}', 99, '1900.01.01.')";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                }
            }


            // pályaszám emelkedőbe osztja el a járműveket

            // kiírjuk a T5c5-t 
            string helyvill = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";
            szöveg = "SELECT * FROM állománytábla where valóstípus Like '%T5C5%'";
            AdatokJármű = KézJármű.Lista_Adatok(helyvill, jelszó, szöveg);

            int darabszám = AdatokJármű.Count;


            // elosztjuk darabra
            int vége1 = darabszám / 3;
            int vége2 = vége1 + (darabszám / 3);

            for (int ii = 0; ii < AdatokJármű.Count; ii++)
            {
                if (ii < vége1)
                {
                    szöveg = "UPDATE állománytábla SET haromnapos=1 WHERE azonosító='" + AdatokJármű[ii].Azonosító.Trim() + "'";
                }
                else if (ii < vége2)
                {
                    szöveg = "UPDATE állománytábla SET haromnapos=2 WHERE azonosító='" + AdatokJármű[ii].Azonosító.Trim() + "'";
                }
                else
                {
                    szöveg = "UPDATE állománytábla SET haromnapos=3 WHERE azonosító='" + AdatokJármű[ii].Azonosító.Trim() + "'";
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);
                Holtart.Lép();
            }
            Holtart.Ki();
        }
        #endregion

        #region Program adatok fordítása
        private void Program_adatok_Click(object sender, EventArgs e)
        {
            try
            {
                Holtart.Be();
                Főkönyv_Funkciók.SUBnapihibagöngyölés(Cmbtelephely.Text.Trim());
                Főkönyv_Funkciók.SUBNapielkészültek(Dátum.Value, Cmbtelephely.Text.Trim());
                Főkönyv_Funkciók.Napiállók(Cmbtelephely.Text.Trim());
                DateTime tegnap = DateTime.Today.AddDays(-1);

                // ha éjfél után készítik el a délutáni főkönyvet, akkor az előző napit kell ütemezni
                // ha aktuális nap készítik és délután akkor is lefutattja vizsgálat ütemezést
                if (tegnap.ToString("yyyy.MM.dd") == Dátum.Value.ToString("yyyy.MM.dd") || Dátum.Value.ToString("yyyy.MM.dd") == DateTime.Today.ToString("yyyy.MM.dd"))
                {
                    if (!Délelőtt.Checked)
                    {
                        TW6000ütemezés();
                        ICSÜtemezés();
                        T5C5Ütemezés();
                        CAFÜtemezés();
                    }
                }

                if (!System.IO.File.Exists(HelyNap))
                {
                    Adatbázis_Létrehozás.Főkönyvtáblaalap(HelyNap);
                }
                else if (MessageBox.Show("Már van az adott napra feltöltve adat ! Módosítjuk az adatokat ?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    File.Delete(HelyNap);
                    Adatbázis_Létrehozás.Főkönyvtáblaalap(HelyNap);
                }
                else
                {
                    NapiTábla_kiírás(0);
                    Holtart.Ki();
                    return;
                }

                // rögzítjük a módosítót
                string jelszó = "lilaakác";
                string szöveg = $"INSERT INTO segédtábla (id, Bejelentkezésinév) VALUES (1, '{Program.PostásNév.Trim()}' )";
                MyA.ABMódosítás(HelyNap, jelszó, szöveg);

                // beolvassuk a villamos adatokat

                Holtart.Be();
                JárműListaFeltöltés();
                NapiHibalistaFeltöltés();
                SzerelvényListaFeltöltés();
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Jármű rekord in AdatokJármű)
                {

                    szöveg = "INSERT INTO Adattábla  (Státus, hibaleírása, típus, azonosító, szerelvény, ";
                    szöveg += "viszonylat, forgalmiszám, kocsikszáma, tervindulás, tényindulás, ";
                    szöveg += "tervérkezés, tényérkezés, miótaáll, napszak, megjegyzés ) VALUES (";
                    szöveg += $"{rekord.Státus}, '{Hiba_Leírás(rekord.Azonosító.Trim())}', '{rekord.Típus.Trim()}', '{rekord.Azonosító.Trim()}', {rekord.Szerelvénykocsik}, ";
                    szöveg += $"'-', '-', {KocsikSzáma(rekord.Szerelvénykocsik)}, '1900.01.01. 00:00:00', '1900.01.01. 00:00:00', ";
                    szöveg += "'1900.01.01. 00:00:00', '1900.01.01. 00:00:00', ";
                    if (rekord.Miótaáll.ToString() != "")
                    {
                        szöveg += $"'{rekord.Miótaáll}', ";
                    }
                    else
                    {
                        szöveg += "'1900.01.01. 00:00:00', ";
                    }
                    szöveg += " '-', '*')";
                    SzövegGy.Add(szöveg);
                    Holtart.Lép();
                }
                MyA.ABMódosítás(HelyNap, jelszó, SzövegGy);

                NapiTábla_kiírás(0);
                Holtart.Ki();
                MessageBox.Show("Az adat konvertálás befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private string Hiba_Leírás(string azonosító)
        {
            string válasz = "_";
            try
            {
                Adat_Nap_Hiba Elem = (from a in AdatokNapiHiba
                                      where a.Azonosító == azonosító
                                      select a).FirstOrDefault();
                if (Elem != null)
                {
                    if (Elem.Üzemképtelen.Trim() != "_") válasz = Elem.Üzemképtelen.Trim();

                    if (Elem.Beálló.Trim() != "_")
                    {
                        if (válasz.Trim() == "_")
                            válasz = Elem.Beálló.Trim();
                        else
                            válasz += "-" + Elem.Beálló.Trim();
                    }
                    if (Elem.Üzemképeshiba.Trim() != "_")
                    {
                        if (válasz.Trim() == "_")
                            válasz = Elem.Üzemképeshiba.Trim();
                        else
                            válasz += "-" + Elem.Üzemképeshiba.Trim();
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
            return válasz;
        }

        private long KocsikSzáma(long szerelvényszám)
        {
            long válasz = 0;
            try
            {
                if (AdatokSzerelvény == null) return válasz;
                if (AdatokSzerelvény.Count == 0) return válasz;

                Adat_Szerelvény Elem = (from a in AdatokSzerelvény
                                        where a.Szerelvény_ID == szerelvényszám
                                        select a).FirstOrDefault();
                if (Elem != null) válasz = Elem.Szerelvényhossz;
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
            return válasz;
        }
        #endregion

        #region ZSER
        private void Zserbeolvasás_Click(object sender, EventArgs e)
        {
            try
            {
                ZSER_Beolvasás();
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

        void ZSER_Beolvasás()
        {
            string fájlexc = "";
            try
            {
                // Idő korrekciók
                Kezelő_Kiegészítő_Idő_Kor KézKor = new Kezelő_Kiegészítő_Idő_Kor();
                List<Adat_Kiegészítő_Idő_Kor> AdatokKor = KézKor.Lista_Adatok();

                Adat_Kiegészítő_Idő_Kor Elem = (from a in AdatokKor
                                                where a.Id == 1
                                                select a).FirstOrDefault();

                long kiadási_korr = 0;
                long érkezési_korr = 0;
                if (Elem != null)
                {
                    kiadási_korr = Elem.Kiadási;
                    érkezési_korr = Elem.Érkezési;
                }

                // megnézzük, hogy létezik-e adott napi tábla
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\" + Dátum.Value.ToString("yyyy") + @"\ZSER\zser" + Dátum.Value.ToString("yyyyMMdd");
                if (Délelőtt.Checked)
                    hely += "de.mdb";
                else
                    hely += "du.mdb";

                if (File.Exists(hely))
                {
                    // ha létezik akkor töröljük
                    if (MessageBox.Show("Már van az adott napra feltöltve adat ! Módosítjuk az adatokat ?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        File.Delete(hely);
                    else
                        return;
                }

                Adatbázis_Létrehozás.Zseltáblaalap(hely);

                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "SAP-s Adatok betöltése",
                    FileName = "",
                    Filter = "Excel |*.xlsx"
                };


                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;

                DateTime Eleje = DateTime.Now;
                Főkönyv_Funkciók.ZSER_Betöltés(hely, fájlexc, Dátum.Value, Cmbtelephely.Text.Trim(), kiadási_korr, érkezési_korr);
                DateTime Vége = DateTime.Now;

                // megnézzük, hogy előző éjszaka volt -e tábla, ha volt akkor hozzá fűzzük a napi adatokhoz.
                hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\" + Dátum.Value.AddDays(-1).ToString("yyyy") + @"\ZSER\zser" + Dátum.Value.AddDays(-1).ToString("yyyyMMdd") + "éj.mdb";
                if (System.IO.File.Exists(hely)) Előzőnapuéjszakaijárat();
                MessageBox.Show($"Az adat konvertálás befejeződött!\n Idő:{Vége - Eleje}", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private void Előzőnapuéjszakaijárat()
        {
            // hozzátesszük az előző éjszakai járatokat az aktuális naphoz.
            string helyéj = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\" + Dátum.Value.AddDays(-1).ToString("yyyy")
                + @"\ZSER\zser" + Dátum.Value.AddDays(-1).ToString("yyyyMMdd") + "éj.mdb";
            // Az adott napi adatok megnyitása
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\" + Dátum.Value.ToString("yyyy") + @"\ZSER\zser" + Dátum.Value.ToString("yyyyMMdd");
            if (Délelőtt.Checked)
                hely += "de.mdb";
            else
                hely += "du.mdb";

            string jelszó = "lilaakác";

            string szöveg = "SELECT * FROM zseltábla ";


            List<Adat_Főkönyv_ZSER> Adatok = KézFőkönyvZSER.Lista_adatok(helyéj, jelszó, szöveg);

            foreach (Adat_Főkönyv_ZSER rekord in Adatok)
            {

                szöveg = " INSERT INTO Zseltábla (";
                szöveg += " viszonylat, forgalmiszám, tervindulás, tényindulás, tervérkezés, ";
                szöveg += " tényérkezés, napszak, szerelvénytípus, kocsikszáma, megjegyzés, ";
                szöveg += " kocsi1, kocsi2, kocsi3, kocsi4, kocsi5, kocsi6, ";
                szöveg += " ellenőrző, Státus) VALUES ( ";
                szöveg += "'" + rekord.Viszonylat.Trim() + "', ";
                szöveg += "'" + rekord.Forgalmiszám.Trim() + "', ";
                szöveg += "'" + rekord.Tervindulás.ToString() + "', ";
                szöveg += "'" + rekord.Tényindulás.ToString() + "', ";
                szöveg += "'" + rekord.Tervérkezés.ToString() + "', ";
                szöveg += "'" + rekord.Tényérkezés.ToString() + "', ";
                szöveg += "'*', ";
                szöveg += "'" + rekord.Szerelvénytípus.Trim() + "', ";
                szöveg += rekord.Kocsikszáma.ToString() + ", ";
                szöveg += "'" + rekord.Megjegyzés.Trim() + "', ";
                szöveg += "'" + rekord.Kocsi1.Trim() + "', ";
                szöveg += "'" + rekord.Kocsi2.Trim() + "', ";
                szöveg += "'" + rekord.Kocsi3.Trim() + "', ";
                szöveg += "'" + rekord.Kocsi4.Trim() + "', ";
                szöveg += "'" + rekord.Kocsi5.Trim() + "', ";
                szöveg += "'" + rekord.Kocsi6.Trim() + "', ";
                szöveg += "'" + rekord.Ellenőrző.Trim() + "', ";
                szöveg += "'" + rekord.Státus.Trim() + "') ";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
        }

        #endregion


        #region ZSER összevetés
        private void ZSERellenőrzés_Click(object sender, EventArgs e)
        {
            try
            {
                // megnézzük, hogy létezik-e adott napi tábla

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\{Dátum.Value.Year}\nap\{Dátum.Value:yyyyMMdd}";
                if (Délelőtt.Checked)
                    hely += "denap.mdb";
                else
                    hely += "dunap.mdb";


                if (!System.IO.File.Exists(hely)) throw new HibásBevittAdat("Hiányzonak a napi adatok!");

                string jelszó = "lilaakác";
                // lenullázzuk az előző adatokat

                Holtart.Be(100);
                string szöveg = "SELECT * FROM Adattábla ";

                List<Adat_Főkönyv_Nap> Adatok = KézFőkönyvNap.Lista_adatok(hely, jelszó, szöveg);

                List<string> szövegGy = new List<string>();
                foreach (Adat_Főkönyv_Nap rekord in Adatok)
                {
                    szöveg = "UPDATE Adattábla SET viszonylat='-', forgalmiszám='-',  ";
                    szöveg += "tervindulás='1900.01.01 00:00:00', ";
                    szöveg += "tényindulás='1900.01.01 00:00:00', ";
                    szöveg += "tervérkezés='1900.01.01 00:00:00', ";
                    szöveg += "tényérkezés='1900.01.01 00:00:00', ";
                    szöveg += "napszak='_', ";
                    szöveg += "megjegyzés='_' ";
                    szöveg += $" WHERE azonosító='{rekord.Azonosító.Trim()}'";

                    szövegGy.Add(szöveg);
                    Holtart.Lép();
                }
                MyA.ABMódosítás(hely, jelszó, szövegGy);


                // leellnőrizzük a zser adatokat hogy megvannak-e
                string helyzser = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\{Dátum.Value.Year}\ZSER\zser{Dátum.Value:yyyyMMdd}";
                if (Délelőtt.Checked)
                    helyzser += "de.mdb";
                else
                    helyzser += "du.mdb";


                if (!File.Exists(helyzser))
                {
                    MessageBox.Show("Hiányzonak a napi ZSER adatok!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // ha nincs zser, mert vágány zár van .
                    if (Délelőtt.Checked)
                    {
                        Holtart.Lép();
                        Főkönyv_Funkciók.Napiadatokmentése("de", Dátum.Value, Cmbtelephely.Text);
                        Holtart.Lép();
                        Főkönyv_Funkciók.Napitipuscsere("de", Dátum.Value, Cmbtelephely.Text);
                        Holtart.Lép();
                        Főkönyv_Funkciók.Napiszemélyzet("de", Dátum.Value, Cmbtelephely.Text);
                        Holtart.Lép();
                        Főkönyv_Funkciók.Napitöbblet("de", Dátum.Value, Cmbtelephely.Text);
                    }
                    else
                    {
                        Holtart.Lép();
                        Főkönyv_Funkciók.Napiadatokmentése("du", Dátum.Value, Cmbtelephely.Text);
                        Holtart.Lép();
                        Főkönyv_Funkciók.Napitipuscsere("du", Dátum.Value, Cmbtelephely.Text);
                        Holtart.Lép();
                        Főkönyv_Funkciók.Napiszemélyzet("du", Dátum.Value, Cmbtelephely.Text);
                        Holtart.Lép();
                        Főkönyv_Funkciók.Napitöbblet("du", Dátum.Value, Cmbtelephely.Text);
                    }
                    return;
                }
                Holtart.Lép();
                Osztályozúj();
                Holtart.Lép();
                Zser_ellenőrzés();
                Holtart.Lép();
                ZSER_szerelvény_ellenőrzés();
                Holtart.Lép();
                Áttölti_adatokat_ZSERből();

                if (Délelőtt.Checked)
                {
                    Holtart.Lép();
                    Főkönyv_Funkciók.Napiadatokmentése("de", Dátum.Value, Cmbtelephely.Text);
                    Holtart.Lép();
                    Főkönyv_Funkciók.Napitipuscsere("de", Dátum.Value, Cmbtelephely.Text);
                    Holtart.Lép();
                    Főkönyv_Funkciók.Napiszemélyzet("de", Dátum.Value, Cmbtelephely.Text);
                    Holtart.Lép();
                    Főkönyv_Funkciók.Napitöbblet("de", Dátum.Value, Cmbtelephely.Text);
                }
                else
                {
                    Holtart.Lép();
                    Főkönyv_Funkciók.Napiadatokmentése("du", Dátum.Value, Cmbtelephely.Text);
                    Holtart.Lép();
                    Főkönyv_Funkciók.Napitipuscsere("du", Dátum.Value, Cmbtelephely.Text);
                    Holtart.Lép();
                    Főkönyv_Funkciók.Napiszemélyzet("du", Dátum.Value, Cmbtelephely.Text);
                    Holtart.Lép();
                    Főkönyv_Funkciók.Napitöbblet("du", Dátum.Value, Cmbtelephely.Text);
                }
                Holtart.Ki();
                MessageBox.Show("Az adat konvertálás befejeződött!", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Holtart.Lép();
                Eredménytábla();

                Holtart.Lép();
                Gombok();
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

        private void Osztályozúj()
        {

            int éjszakavolt = 0;
            DateTime reggeldát = new DateTime(1900, 1, 1, 0, 0, 0);
            DateTime délutándát = new DateTime(1900, 1, 1, 0, 0, 0);
            DateTime estedát = new DateTime(1900, 1, 1, 0, 0, 0);
            string napszak;
            DateTime ideigdátum;

            // megnézzük, hogy az adott nap munkanap, vagy hétvége
            int munkanap = 1;
            string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value.Year}\{Dátum.Value.Year}_fortekiadási_adatok.mdb";
            string jelszó = "gémkapocs";
            string szöveg = "SELECT * FROM fortekiadástábla";
            if (File.Exists(hely))
            {
                Kezelő_Forte_Kiadási_Adatok KézForte = new Kezelő_Forte_Kiadási_Adatok();
                List<Adat_Forte_Kiadási_Adatok> AdatokForte = KézForte.Lista_adatok(hely, jelszó, szöveg);

                Adat_Forte_Kiadási_Adatok ElemForte = (from a in AdatokForte
                                                       where a.Dátum.ToShortDateString() == Dátum.Value.ToShortDateString()
                                                       select a).FirstOrDefault();
                // ha létezik a fájl akkor megnyitjuk
                if (ElemForte != null)
                {
                    // ha talált akkor a legelső adat alapján osztályoz
                    // ha hétvége akkor módosítjuk a sorszámot 2-re, ha munkanap akkor marad 1.
                    if (ElemForte.Munkanap == 1) munkanap = 2;
                }
            }
            // betöltjük az időket
            Kezelő_Kiegészítő_Idő_Tábla Kéz = new Kezelő_Kiegészítő_Idő_Tábla();
            List<Adat_Kiegészítő_Idő_Tábla> AdatokIdő = Kéz.Lista_Adatok();
            Adat_Kiegészítő_Idő_Tábla EgyAdat = (from a in AdatokIdő
                                                 where a.Sorszám == munkanap
                                                 select a).FirstOrDefault();
            if (EgyAdat != null)
            {
                ideigdátum = EgyAdat.Reggel;
                reggeldát = new DateTime(Dátum.Value.Year, Dátum.Value.Month, Dátum.Value.Day, ideigdátum.Hour, ideigdátum.Minute, 0);

                ideigdátum = EgyAdat.Délután;
                délutándát = new DateTime(Dátum.Value.Year, Dátum.Value.Month, Dátum.Value.Day, ideigdátum.Hour, ideigdátum.Minute, 0);

                ideigdátum = EgyAdat.Este;
                estedát = new DateTime(Dátum.Value.Year, Dátum.Value.Month, Dátum.Value.Day, ideigdátum.Hour, ideigdátum.Minute, 0);
            }
            // megnézzük, hogy létezik-e adott napi tábla
            hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\" + Dátum.Value.ToString("yyyy") + @"\ZSER\zser" + Dátum.Value.ToString("yyyyMMdd");
            if (Délelőtt.Checked)
                hely += "de.mdb";
            else
                hely += "du.mdb";

            jelszó = "lilaakác";

            szöveg = "SELECT * FROM zseltábla order by viszonylat,forgalmiszám, tervindulás";


            List<Adat_Főkönyv_ZSER> Adatok = KézFőkönyvZSER.Lista_adatok(hely, jelszó, szöveg);


            foreach (Adat_Főkönyv_ZSER rekord in Adatok)
            {
                napszak = "*";

                if (rekord.Tényindulás <= délutándát && rekord.Tényérkezés >= délutándát && Délelőtt.Checked == false)
                    napszak = "DU";

                if (rekord.Tényindulás <= reggeldát && rekord.Tényérkezés >= reggeldát && Délelőtt.Checked)
                    napszak = "DE";

                if (rekord.Tényindulás >= estedát && napszak.Trim() == "*")
                {
                    napszak = "X";
                    éjszakavolt = 1;
                }

                szöveg = "UPDATE zseltábla SET napszak='" + napszak.Trim() + "'";
                szöveg += " WHERE viszonylat='" + rekord.Viszonylat.Trim() + "' AND ";
                szöveg += " forgalmiszám='" + rekord.Forgalmiszám.Trim() + "' AND ";
                szöveg += " tervindulás=#" + rekord.Tervindulás.ToString("M-d-yyyy HH:mm:s") + "# ";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }


            // azokat a vonalakat amiket nem kell figyelembe venni kicsillagozzuk
            Tiltott_vonalak.Items.Clear();

            string helykieg = $@"{Application.StartupPath}\főmérnökség\adatok\Kiegészítő.mdb";
            string jelszókieg = "Mocó";
            szöveg = "SELECT * FROM fortevonal ";

            Tiltott_vonalak.BeginUpdate();
            Tiltott_vonalak.Items.AddRange(MyF.ComboFeltöltés(helykieg, jelszókieg, szöveg, "fortevonal"));
            Tiltott_vonalak.EndUpdate();
            Tiltott_vonalak.Refresh();

            if (Tiltott_vonalak.Items.Count > 0)
            {
                szöveg = "SELECT * FROM zseltábla ORDER BY viszonylat";
                Adatok = KézFőkönyvZSER.Lista_adatok(hely, jelszó, szöveg);
                foreach (Adat_Főkönyv_ZSER rekord in Adatok)
                {
                    for (int i = 0; i < Tiltott_vonalak.Items.Count; i++)
                    {
                        if (rekord.Viszonylat.Trim() == Tiltott_vonalak.Items[i].ToString())
                        {
                            szöveg = "UPDATE zseltábla SET napszak='*' ";
                            szöveg += " WHERE viszonylat='" + rekord.Viszonylat.Trim() + "' AND ";
                            szöveg += " forgalmiszám='" + rekord.Forgalmiszám.Trim() + "' AND ";
                            szöveg += " tervindulás=#" + rekord.Tervindulás.ToString("yyyy-MM-dd HH:mm:s") + "# ";
                            MyA.ABMódosítás(hely, jelszó, szöveg);
                        }
                    }
                }
            }
            if (éjszakavolt == 1)
                Éjszakaijárat();
        }


        private void Éjszakaijárat()
        {
            // osztályozásnál a napi éjszakait X-el jelöltük.
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\" + Dátum.Value.ToString("yyyy") + @"\ZSER\zser" + Dátum.Value.ToString("yyyyMMdd") + "éj.mdb";
            // ha van akkor töröljük
            if (System.IO.File.Exists(hely))
                File.Delete(hely);
            Adatbázis_Létrehozás.Zseltáblaalap(hely);
            // leellnőrizzük a zser adatokat hogy megvannak-e
            string helyzser = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\" + Dátum.Value.ToString("yyyy") + @"\ZSER\zser" + Dátum.Value.ToString("yyyyMMdd");
            if (Délelőtt.Checked)
                helyzser += "de.mdb";
            else
                helyzser += "du.mdb";

            string jelszó = "lilaakác";
            string szöveg = "SELECT * FROM zseltábla where napszak='X'";


            List<Adat_Főkönyv_ZSER> Adatok = KézFőkönyvZSER.Lista_adatok(helyzser, jelszó, szöveg);

            foreach (Adat_Főkönyv_ZSER rekord in Adatok)
            {

                szöveg = " INSERT INTO Zseltábla (";
                szöveg += " viszonylat, forgalmiszám, tervindulás, tényindulás, tervérkezés, ";
                szöveg += " tényérkezés, napszak, szerelvénytípus, kocsikszáma, megjegyzés, ";
                szöveg += " kocsi1, kocsi2, kocsi3, kocsi4, kocsi5, kocsi6, ";
                szöveg += " ellenőrző, Státus) VALUES ( ";
                szöveg += "'" + rekord.Viszonylat.Trim() + "', ";
                szöveg += "'" + rekord.Forgalmiszám.Trim() + "', ";
                szöveg += "'" + rekord.Tervindulás.ToString() + "', ";
                szöveg += "'" + rekord.Tényindulás.ToString() + "', ";
                szöveg += "'" + rekord.Tervérkezés.ToString() + "', ";
                szöveg += "'" + rekord.Tényérkezés.ToString() + "', ";
                szöveg += "'É', ";
                szöveg += "'" + rekord.Szerelvénytípus.Trim() + "', ";
                szöveg += rekord.Kocsikszáma.ToString() + ", ";
                szöveg += "'" + rekord.Megjegyzés.Trim() + "', ";
                szöveg += "'" + rekord.Kocsi1.Trim() + "', ";
                szöveg += "'" + rekord.Kocsi2.Trim() + "', ";
                szöveg += "'" + rekord.Kocsi3.Trim() + "', ";
                szöveg += "'" + rekord.Kocsi4.Trim() + "', ";
                szöveg += "'" + rekord.Kocsi5.Trim() + "', ";
                szöveg += "'" + rekord.Kocsi6.Trim() + "', ";
                szöveg += "'" + rekord.Ellenőrző.Trim() + "', ";
                szöveg += "'" + rekord.Státus.Trim() + "') ";

                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
        }


        private void Zser_ellenőrzés()
        {
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\" + Dátum.Value.ToString("yyyy") + @"\nap\" + Dátum.Value.ToString("yyyyMMdd");
            if (Délelőtt.Checked)
                hely += "denap.mdb";
            else
                hely += "dunap.mdb";


            string helyzser = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\" + Dátum.Value.ToString("yyyy") + @"\ZSER\zser" + Dátum.Value.ToString("yyyyMMdd");
            if (Délelőtt.Checked)
                helyzser += "de.mdb";
            else
                helyzser += "du.mdb";

            string jelszó = "lilaakác";

            Holtart.Be(100);
            // visszaállítjuk az összes ellenrőzőt alaphelyzetbe
            string szöveg = "SELECT * FROM Zseltábla ";


            List<Adat_Főkönyv_ZSER> Adatok = KézFőkönyvZSER.Lista_adatok(helyzser, jelszó, szöveg);

            foreach (Adat_Főkönyv_ZSER rekord in Adatok)
            {
                szöveg = "UPDATE zseltábla SET ellenőrző='_' ";
                szöveg += " WHERE viszonylat='" + rekord.Viszonylat.Trim() + "' AND ";
                szöveg += " forgalmiszám='" + rekord.Forgalmiszám.Trim() + "' AND ";
                szöveg += " tervindulás=#" + rekord.Tervindulás.ToString("yyyy-MM-dd HH:mm:s") + "# ";

                MyA.ABMódosítás(helyzser, jelszó, szöveg);
                Holtart.Lép();
            }


            // ellenőrizzük, hogy a pályaszámok a telephez tartoznak
            // *******************************
            // feltöltjük a pályaszám listába
            // *******************************
            szöveg = "SELECT * FROM Adattábla ORDER BY azonosító ";
            Pályaszámok.Items.Clear();
            Pályaszámok.BeginUpdate();
            Pályaszámok.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "azonosító"));
            Pályaszámok.EndUpdate();
            Pályaszámok.Refresh();


            szöveg = "SELECT * FROM Zseltábla ORDER BY viszonylat, forgalmiszám, tervindulás";
            Adatok = KézFőkönyvZSER.Lista_adatok(helyzser, jelszó, szöveg);


            string eredmény;
            Holtart.Lép();

            foreach (Adat_Főkönyv_ZSER rekord in Adatok)
            {
                eredmény = "_";
                if (rekord.Napszak.Trim() == "DE" || rekord.Napszak.Trim() == "DU")
                {
                    eredmény += Pályaszám_vizsgálat(rekord.Kocsi1.Trim()) +
                                Pályaszám_vizsgálat(rekord.Kocsi2.Trim()) +
                                Pályaszám_vizsgálat(rekord.Kocsi3.Trim()) +
                                Pályaszám_vizsgálat(rekord.Kocsi4.Trim()) +
                                Pályaszám_vizsgálat(rekord.Kocsi5.Trim()) +
                                Pályaszám_vizsgálat(rekord.Kocsi6.Trim());

                    // módosítjuk az adatokat
                    szöveg = "UPDATE zseltábla SET ellenőrző='" + eredmény.Trim() + "' ";
                    szöveg += " WHERE viszonylat='" + rekord.Viszonylat.Trim() + "' AND ";
                    szöveg += " forgalmiszám='" + rekord.Forgalmiszám.Trim() + "' AND ";
                    szöveg += " tervindulás=#" + rekord.Tervindulás.ToString("yyyy-MM-dd HH:mm:s") + "# ";
                    MyA.ABMódosítás(helyzser, jelszó, szöveg);
                }
                Holtart.Lép();
            }
        }

        /// <summary>
        /// Azt vizsgláljuk, hogy a kapott pályaszám a telephely járműve-e.
        /// Ha igaz, vagy ha nincs értelmezhető pályaszám, akkor 1 tér vissza 
        /// Ha nem akkor 0
        /// </summary>
        /// <param name="pályaszám"></param>
        /// <returns></returns>

        string Pályaszám_vizsgálat(string pályaszám)
        {
            string válasz;
            int voltdarab = 0;

            if (pályaszám.Trim() != "_")
            {
                for (int i = 0; i < Pályaszámok.Items.Count; i++)
                {
                    if (pályaszám.Trim() == Pályaszámok.Items[i].ToString().Trim())
                    {
                        voltdarab = 1;
                        break;
                    }
                }
            }
            else
            {
                // ha üres a mező akkor
                voltdarab = 1;
            }
            if (voltdarab == 1)
                válasz = "0";
            else
                válasz = "1";

            return válasz;
        }

        private void ZSER_szerelvény_ellenőrzés()
        {

            FőkönyvNapListaFeltöltés(HelyNap);
            FőkönyZserListaFeltöltés(HelyZser);

            string jelszó = "lilaakác";

            Holtart.Be(100);
            // leellenőizzük, hogy azonos szerelvényben futnak
            List<string> SzövegGy = new List<string>();
            foreach (Adat_Főkönyv_ZSER rekord in AdatokFőkönyvZSER)
            {
                if (rekord.Napszak.Trim() == "DE" | rekord.Napszak.Trim() == "DU")
                {
                    //lenullázzuk a 6 kocsis szerelvényt
                    long[] szerelvényszám = { 0, 0, 0, 0, 0, 0 };

                    // megnézzük, hogy a kocsiknak mi a szerelvényszáma
                    if (rekord.Kocsi1.Trim() != "_") szerelvényszám[0] = MiASzerelvényszám(rekord.Kocsi1);
                    if (rekord.Kocsi2.Trim() != "_") szerelvényszám[1] = MiASzerelvényszám(rekord.Kocsi2);
                    if (rekord.Kocsi3.Trim() != "_") szerelvényszám[2] = MiASzerelvényszám(rekord.Kocsi3);
                    if (rekord.Kocsi4.Trim() != "_") szerelvényszám[3] = MiASzerelvényszám(rekord.Kocsi4);
                    if (rekord.Kocsi5.Trim() != "_") szerelvényszám[4] = MiASzerelvényszám(rekord.Kocsi5);
                    if (rekord.Kocsi6.Trim() != "_") szerelvényszám[5] = MiASzerelvényszám(rekord.Kocsi6);


                    bool hiba = false;
                    // összehasonlítjuk a többi kocsiéval ha eltér akkor hibás
                    long első = szerelvényszám[0];
                    for (int i = 1; i < szerelvényszám.Length; i++)
                    {
                        if (szerelvényszám[i] != első && szerelvényszám[i] != 0) hiba = true;
                    }

                    string ideig = rekord.Ellenőrző;
                    if (hiba)
                        ideig += "1";
                    else
                        ideig += "0";

                    string szöveg = $"UPDATE zseltábla SET ellenőrző='{ideig.Trim()}' ";
                    szöveg += $" WHERE viszonylat='{rekord.Viszonylat.Trim()}' AND ";
                    szöveg += $" forgalmiszám='{rekord.Forgalmiszám.Trim()}' AND ";
                    szöveg += $" tervindulás=#{rekord.Tervindulás:yyyy - MM - dd HH: mm:s}# ";
                    SzövegGy.Add(szöveg);
                }
                Holtart.Lép();
            }
            if (SzövegGy.Count > 0) MyA.ABMódosítás(HelyZser, jelszó, SzövegGy);
        }

        private long MiASzerelvényszám(string Kocsi)
        {
            long Válasz = 0;
            try
            {
                Adat_Főkönyv_Nap Elem = (from a in AdatokFőkönyvNap
                                         where a.Azonosító == Kocsi
                                         select a).FirstOrDefault();
                if (Elem != null) Válasz = Elem.Szerelvény;
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
            return Válasz;
        }

        private void Áttölti_adatokat_ZSERből()
        {
            string helyzser = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\" + Dátum.Value.ToString("yyyy") + @"\ZSER\zser" + Dátum.Value.ToString("yyyyMMdd");
            if (Délelőtt.Checked)
                helyzser += "de.mdb";
            else
                helyzser += "du.mdb";

            string jelszó = "lilaakác";

            Holtart.Be(100);

            // visszaállítjuk az összes ellenrőzőt alaphelyzetbe
            string szöveg = "SELECT * FROM Zseltábla ORDER BY tervindulás";
            List<Adat_Főkönyv_ZSER> Adatok = KézFőkönyvZSER.Lista_adatok(helyzser, jelszó, szöveg);

            foreach (Adat_Főkönyv_ZSER rekordzser in Adatok)
            {
                if (rekordzser.Kocsi1.Trim() != "_") Napi_vizsgál_rögzítés(HelyNap, jelszó, rekordzser, rekordzser.Kocsi1);
                if (rekordzser.Kocsi2.Trim() != "_") Napi_vizsgál_rögzítés(HelyNap, jelszó, rekordzser, rekordzser.Kocsi2);
                if (rekordzser.Kocsi3.Trim() != "_") Napi_vizsgál_rögzítés(HelyNap, jelszó, rekordzser, rekordzser.Kocsi3);
                if (rekordzser.Kocsi4.Trim() != "_") Napi_vizsgál_rögzítés(HelyNap, jelszó, rekordzser, rekordzser.Kocsi4);
                if (rekordzser.Kocsi5.Trim() != "_") Napi_vizsgál_rögzítés(HelyNap, jelszó, rekordzser, rekordzser.Kocsi5);
                if (rekordzser.Kocsi6.Trim() != "_") Napi_vizsgál_rögzítés(HelyNap, jelszó, rekordzser, rekordzser.Kocsi6);
                Holtart.Lép();
            }

            // azon kocsikat is átírja amelyek voltak forgalomban az nap
            szöveg = "SELECT * FROM Adattábla WHERE tervindulás<#01-01-2000# ORDER BY Azonosító";

            List<Adat_Főkönyv_Nap> Adat = KézFőkönyvNap.Lista_adatok(HelyNap, jelszó, szöveg);

            List<string> SzövegGy = new List<string>();
            foreach (Adat_Főkönyv_Nap rekord in Adat)
            {
                // megkeressük a zser adatokban
                Adat_Főkönyv_ZSER ElemZSer = (from a in Adatok
                                              where a.Kocsi1 == rekord.Azonosító.Trim() ||
                                              a.Kocsi2 == rekord.Azonosító.Trim() ||
                                              a.Kocsi3 == rekord.Azonosító.Trim() ||
                                              a.Kocsi4 == rekord.Azonosító.Trim() ||
                                              a.Kocsi5 == rekord.Azonosító.Trim() ||
                                              a.Kocsi6 == rekord.Azonosító.Trim()
                                              orderby a.Tervérkezés
                                              select a).FirstOrDefault();

                if (ElemZSer != null)
                {
                    // rögzítjük az adatokat
                    szöveg = "UPDATE Adattábla SET ";
                    szöveg += $"viszonylat='{ElemZSer.Viszonylat.Trim()}', ";
                    szöveg += $"forgalmiszám='{ElemZSer.Forgalmiszám.Trim()}', ";
                    szöveg += $"tervindulás='{ElemZSer.Tervindulás}', ";
                    szöveg += $"tényindulás='{ElemZSer.Tényindulás}', ";
                    szöveg += $"tervérkezés='{ElemZSer.Tervérkezés}', ";
                    szöveg += $"tényérkezés='{ElemZSer.Tényérkezés}', ";
                    szöveg += $"kocsikszáma={ElemZSer.Kocsikszáma} ";
                    szöveg += $" WHERE azonosító='{rekord.Azonosító.Trim()}'";
                    SzövegGy.Add(szöveg);
                }
                Holtart.Lép();
            }
            if (SzövegGy.Count > 0) MyA.ABMódosítás(HelyNap, jelszó, SzövegGy);
        }

        void Rögzít_elemet_napi(string hely, string jelszó, Adat_Főkönyv_Nap rekordzser, string azonosító)
        {
            string szöveg = "UPDATE Adattábla SET ";
            szöveg += "viszonylat='" + rekordzser.Viszonylat.Trim() + "', ";
            szöveg += "forgalmiszám='" + rekordzser.Forgalmiszám.Trim() + "', ";
            szöveg += "kocsikszáma=" + rekordzser.Kocsikszáma.ToString() + ", ";
            szöveg += "tervindulás='" + rekordzser.Tervindulás.ToString() + "', ";
            szöveg += "tényindulás='" + rekordzser.Tényindulás.ToString() + "', ";
            szöveg += "tervérkezés='" + rekordzser.Tervérkezés.ToString() + "', ";
            szöveg += "tényérkezés='" + rekordzser.Tényérkezés.ToString() + "', ";
            szöveg += "napszak='" + rekordzser.Napszak.Trim() + "', ";
            szöveg += "megjegyzés='" + rekordzser.Megjegyzés.Trim() + "' ";
            szöveg += " WHERE azonosító='" + azonosító.Trim() + "'";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        void Napi_vizsgál_rögzítés(string hely, string jelszó, Adat_Főkönyv_ZSER rekordzser, string azonosító)
        {
            FőkönyvNapListaFeltöltés(hely);
            Adat_Főkönyv_Nap Elem = (from a in AdatokFőkönyvNap
                                     where a.Azonosító == azonosító.Trim()
                                     select a).FirstOrDefault();

            if (Elem != null)
            {
                // ha van
                // rögzítjük a pályaszámhoz a zser adatokat
                if (Délelőtt.Checked &&
                        (rekordzser.Napszak.Trim() == "E" || rekordzser.Napszak.Trim() == "B" || rekordzser.Napszak.Trim() == "DE" ||
                        rekordzser.Napszak.Trim() == "ECR" || rekordzser.Napszak.Trim() == "ER" || rekordzser.Napszak.Trim() == "BCR" ||
                        rekordzser.Napszak.Trim() == "BCD"))
                {
                    Adat_Főkönyv_Nap Elemek = new Adat_Főkönyv_Nap(rekordzser.Viszonylat.Trim(), rekordzser.Forgalmiszám.Trim(),
                        rekordzser.Kocsikszáma, rekordzser.Tervindulás, rekordzser.Tényindulás, rekordzser.Tervérkezés, rekordzser.Tényérkezés,
                        rekordzser.Napszak.Trim(), rekordzser.Megjegyzés.Trim());
                    Rögzít_elemet_napi(hely, jelszó, Elemek, azonosító.Trim());
                }

                if (Délutáni.Checked == true &&
                        (rekordzser.Napszak.Trim() == "E" || rekordzser.Napszak.Trim() == "D" || rekordzser.Napszak.Trim() == "DU" ||
                        rekordzser.Napszak.Trim() == "ECD" || rekordzser.Napszak.Trim() == "ECR" || rekordzser.Napszak.Trim() == "DCD"))
                {
                    Adat_Főkönyv_Nap Elemek = new Adat_Főkönyv_Nap(rekordzser.Viszonylat.Trim(), rekordzser.Forgalmiszám.Trim(),
                        rekordzser.Kocsikszáma, rekordzser.Tervindulás, rekordzser.Tényindulás, rekordzser.Tervérkezés, rekordzser.Tényérkezés,
                        rekordzser.Napszak.Trim(), rekordzser.Megjegyzés.Trim());
                    Rögzít_elemet_napi(hely, jelszó, Elemek, azonosító.Trim());
                }
            }
        }
        #endregion


        #region ZSER adatok listázása
        private void Szerelvénylista_gomb_Click(object sender, EventArgs e)
        { Szerelvény_Lista_eljárás(); }

        private void Szerelvény_Lista_eljárás()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\" + Dátum.Value.ToString("yyyy") + @"\zser\zser" + Dátum.Value.ToString("yyyyMMdd");

                if (Délelőtt.Checked)
                    hely += "de.mdb";
                else
                    hely += "du.mdb";

                if (!File.Exists(hely)) return;


                string jelszó = "lilaakác";
                string szöveg = "SELECT * FROM zseltábla Order By viszonylat,forgalmiszám,tervindulás";

                List<Adat_Főkönyv_ZSER> Adatok = KézFőkönyvZSER.Lista_adatok(hely, jelszó, szöveg);

                DataTable AdatTábla = new DataTable();

                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Visz.");
                AdatTábla.Columns.Add("Forg.sz.");
                AdatTábla.Columns.Add("Napszak");
                AdatTábla.Columns.Add("Tervindulás");
                AdatTábla.Columns.Add("Tényindulás");
                AdatTábla.Columns.Add("Tervérkezés");
                AdatTábla.Columns.Add("Tényérkezés");
                AdatTábla.Columns.Add("F.típus");
                AdatTábla.Columns.Add("Kocsisz.");
                AdatTábla.Columns.Add("Megjegyzés");
                AdatTábla.Columns.Add("Kocsi1");
                AdatTábla.Columns.Add("Kocsi2");
                AdatTábla.Columns.Add("Kocsi3");
                AdatTábla.Columns.Add("Kocsi4");
                AdatTábla.Columns.Add("Kocsi5");
                AdatTábla.Columns.Add("Kocsi6");
                AdatTábla.Columns.Add("Státus");
                AdatTábla.Columns.Add("Ellenőrző");

                AdatTábla.Clear();
                foreach (Adat_Főkönyv_ZSER rekord in Adatok)
                {
                    DataRow Soradat = AdatTábla.NewRow();
                    Soradat["Visz."] = rekord.Viszonylat;
                    Soradat["Forg.sz."] = rekord.Forgalmiszám;
                    Soradat["Napszak"] = rekord.Napszak;
                    Soradat["Tervindulás"] = rekord.Tervindulás;
                    Soradat["Tényindulás"] = rekord.Tényindulás;
                    Soradat["Tervérkezés"] = rekord.Tervérkezés;
                    Soradat["Tényérkezés"] = rekord.Tényérkezés;
                    Soradat["F.típus"] = rekord.Szerelvénytípus;
                    Soradat["Kocsisz."] = rekord.Kocsikszáma;
                    Soradat["Megjegyzés"] = rekord.Megjegyzés;
                    Soradat["Kocsi1"] = rekord.Kocsi1;
                    Soradat["Kocsi2"] = rekord.Kocsi2;
                    Soradat["Kocsi3"] = rekord.Kocsi3;
                    Soradat["Kocsi4"] = rekord.Kocsi4;
                    Soradat["Kocsi5"] = rekord.Kocsi5;
                    Soradat["Kocsi6"] = rekord.Kocsi6;
                    Soradat["Státus"] = rekord.Státus;
                    Soradat["Ellenőrző"] = rekord.Ellenőrző;

                    AdatTábla.Rows.Add(Soradat);
                }

                ZSER_tábla.DataSource = AdatTábla;

                ZSER_tábla.Columns["Visz."].Width = 70;
                ZSER_tábla.Columns["Forg.sz."].Width = 60;
                ZSER_tábla.Columns["Napszak"].Width = 60;
                ZSER_tábla.Columns["Tervindulás"].Width = 160;
                ZSER_tábla.Columns["Tényindulás"].Width = 160;
                ZSER_tábla.Columns["Tervérkezés"].Width = 160;
                ZSER_tábla.Columns["Tényérkezés"].Width = 160;
                ZSER_tábla.Columns["F.típus"].Width = 70;
                ZSER_tábla.Columns["Kocsisz."].Width = 60;
                ZSER_tábla.Columns["Megjegyzés"].Width = 100;
                ZSER_tábla.Columns["Kocsi1"].Width = 60;
                ZSER_tábla.Columns["Kocsi2"].Width = 60;
                ZSER_tábla.Columns["Kocsi3"].Width = 60;
                ZSER_tábla.Columns["Kocsi4"].Width = 60;
                ZSER_tábla.Columns["Kocsi5"].Width = 60;
                ZSER_tábla.Columns["Kocsi6"].Width = 60;
                ZSER_tábla.Columns["Státus"].Width = 120;
                ZSER_tábla.Columns["Ellenőrző"].Width = 120;

                ZSER_tábla.Visible = true;
                ZSER_tábla.Refresh();
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


        private void Kereső_hívó_Click(object sender, EventArgs e)
        {
            Kereső_hívás("zser");
        }



        private void BtnExcelkimenet_Click(object sender, EventArgs e)
        {
            try
            {
                if (ZSER_tábla.Rows.Count <= 0)
                    return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "ZSER_" + Program.PostásNév + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                Module_Excel.EXCELtábla(fájlexc, ZSER_tábla, false);
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



        #region Zser Időponti listázás


        private void Kereső_hívó_idő_Click(object sender, EventArgs e)
        {
            Kereső_hívás("zseridő");
        }

        private void Szövegkeresés_ZSER_Idő()
        {
            if (Új_Ablak_Kereső.Keresendő == null) return;
            if (Új_Ablak_Kereső.Keresendő.Trim() == "") return;
            if (ZSER_tábla_idő.RowCount < 1) return;
            KözösKereső(ZSER_tábla_idő, Új_Ablak_Kereső.Keresendő.Trim());
        }


        private void ZSER_időponti_lista_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\" + Dátum.Value.ToString("yyyy") + @"\zser\zser" + Idődátum.Value.ToString("yyyyMMdd");

                if (Délelőtt.Checked)
                    hely += "de.mdb";
                else
                    hely += "du.mdb";

                if (!System.IO.File.Exists(hely))
                    return;
                string jelszó = "lilaakác";
                string szöveg = "SELECT * FROM zseltábla where (tényindulás<=#" + Idődátum.Value.ToString("MM-dd-yyyy") + " " + Időidő.Value.ToString("HH:mm:ss") + "#)";
                szöveg += " and (tényérkezés>#" + Idődátum.Value.ToString("MM-dd-yyyy") + " " + Időidő.Value.ToString("HH:mm:ss") + "#)";
                szöveg += " order by szerelvénytípus,kocsi1";


                List<Adat_Főkönyv_ZSER> Adatok = KézFőkönyvZSER.Lista_adatok(hely, jelszó, szöveg);

                DataTable AdatTábla = new DataTable();

                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Visz.");
                AdatTábla.Columns.Add("Forg.sz.");
                AdatTábla.Columns.Add("Napszak");
                AdatTábla.Columns.Add("Tervindulás");
                AdatTábla.Columns.Add("Tényindulás");
                AdatTábla.Columns.Add("Tervérkezés");
                AdatTábla.Columns.Add("Tényérkezés");
                AdatTábla.Columns.Add("F.típus");
                AdatTábla.Columns.Add("Kocsisz.");
                AdatTábla.Columns.Add("Megjegyzés");
                AdatTábla.Columns.Add("Kocsi1");
                AdatTábla.Columns.Add("Kocsi2");
                AdatTábla.Columns.Add("Kocsi3");
                AdatTábla.Columns.Add("Kocsi4");
                AdatTábla.Columns.Add("Kocsi5");
                AdatTábla.Columns.Add("Kocsi6");
                AdatTábla.Columns.Add("Státus");
                AdatTábla.Columns.Add("Ellenőrző");

                AdatTábla.Clear();
                foreach (Adat_Főkönyv_ZSER rekord in Adatok)
                {
                    DataRow Soradat = AdatTábla.NewRow();
                    Soradat["Visz."] = rekord.Viszonylat;
                    Soradat["Forg.sz."] = rekord.Forgalmiszám;
                    Soradat["Napszak"] = rekord.Napszak;
                    Soradat["Tervindulás"] = rekord.Tervindulás;
                    Soradat["Tényindulás"] = rekord.Tényindulás;
                    Soradat["Tervérkezés"] = rekord.Tervérkezés;
                    Soradat["Tényérkezés"] = rekord.Tényérkezés;
                    Soradat["F.típus"] = rekord.Szerelvénytípus;
                    Soradat["Kocsisz."] = rekord.Kocsikszáma;
                    Soradat["Megjegyzés"] = rekord.Megjegyzés;
                    Soradat["Kocsi1"] = rekord.Kocsi1;
                    Soradat["Kocsi2"] = rekord.Kocsi2;
                    Soradat["Kocsi3"] = rekord.Kocsi3;
                    Soradat["Kocsi4"] = rekord.Kocsi4;
                    Soradat["Kocsi5"] = rekord.Kocsi5;
                    Soradat["Kocsi6"] = rekord.Kocsi6;
                    Soradat["Státus"] = rekord.Státus;
                    Soradat["Ellenőrző"] = rekord.Ellenőrző;

                    AdatTábla.Rows.Add(Soradat);
                }
                ZSER_tábla_idő.DataSource = AdatTábla;

                ZSER_tábla_idő.Columns["Visz."].Width = 70;
                ZSER_tábla_idő.Columns["Forg.sz."].Width = 60;
                ZSER_tábla_idő.Columns["Napszak"].Width = 60;
                ZSER_tábla_idő.Columns["Tervindulás"].Width = 140;
                ZSER_tábla_idő.Columns["Tényindulás"].Width = 140;
                ZSER_tábla_idő.Columns["Tervérkezés"].Width = 140;
                ZSER_tábla_idő.Columns["Tényérkezés"].Width = 140;
                ZSER_tábla_idő.Columns["F.típus"].Width = 70;
                ZSER_tábla_idő.Columns["Kocsisz."].Width = 60;
                ZSER_tábla_idő.Columns["Megjegyzés"].Width = 100;
                ZSER_tábla_idő.Columns["Kocsi1"].Width = 60;
                ZSER_tábla_idő.Columns["Kocsi2"].Width = 60;
                ZSER_tábla_idő.Columns["Kocsi3"].Width = 60;
                ZSER_tábla_idő.Columns["Kocsi4"].Width = 60;
                ZSER_tábla_idő.Columns["Kocsi5"].Width = 60;
                ZSER_tábla_idő.Columns["Kocsi6"].Width = 60;
                ZSER_tábla_idő.Columns["Státus"].Width = 120;
                ZSER_tábla_idő.Columns["Ellenőrző"].Width = 120;

                ZSER_tábla_idő.Visible = true;
                ZSER_tábla_idő.Refresh();
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


        private void Idő_frissítés_Click(object sender, EventArgs e)
        {
            Idődátum.Value = DateTime.Today;
            Időidő.Value = DateTime.Now;
        }

        #endregion



        #region Napi adatok listázása lapfül

        private void Napi_adatok_listázása_Click(object sender, EventArgs e)
        {
            NapiTábla_kiírás(0);
        }

        private void Beálló_Kocsik_Hibái_Click(object sender, EventArgs e)
        {
            NapiTábla_kiírás(1);
            ExcelNapi();
        }

        private void NapiTábla_kiírás(int változat)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\{Dátum.Value.Year}\nap\{Dátum.Value:yyyyMMdd}";

                if (Délelőtt.Checked)
                    hely += "denap.mdb";
                else
                    hely += "dunap.mdb";

                if (!File.Exists(hely)) return;

                List<Adat_Főkönyv_Nap> AdatokÖ = KézFőkönyvNap.Lista_adatok(hely);

                List<Adat_Főkönyv_Nap> Adatok;
                if (változat == 0)
                    Adatok = AdatokÖ;
                else
                    Adatok = (from a in AdatokÖ
                              where a.Tervérkezés < Óráig.Value
                              && a.Hibaleírása != "_"
                              select a).ToList();

                DataTable AdatTábla = new DataTable();

                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Státus");
                AdatTábla.Columns.Add("hibaleírása");
                AdatTábla.Columns.Add("típus");
                AdatTábla.Columns.Add("azonosító");
                AdatTábla.Columns.Add("szerelvény");
                AdatTábla.Columns.Add("viszonylat");
                AdatTábla.Columns.Add("forgalmiszám");
                AdatTábla.Columns.Add("kocsikszáma");
                AdatTábla.Columns.Add("tervindulás");
                AdatTábla.Columns.Add("tényindulás");
                AdatTábla.Columns.Add("tervérkezés");
                AdatTábla.Columns.Add("tényérkezés");
                AdatTábla.Columns.Add("miótaáll");
                AdatTábla.Columns.Add("napszak");
                AdatTábla.Columns.Add("megjegyzés");

                AdatTábla.Clear();
                foreach (Adat_Főkönyv_Nap rekord in Adatok)
                {
                    DataRow Soradat = AdatTábla.NewRow();

                    Soradat["Státus"] = rekord.Státus;
                    Soradat["hibaleírása"] = rekord.Hibaleírása;
                    Soradat["típus"] = rekord.Típus;
                    Soradat["azonosító"] = rekord.Azonosító;
                    Soradat["szerelvény"] = rekord.Szerelvény;
                    Soradat["viszonylat"] = rekord.Viszonylat;
                    Soradat["forgalmiszám"] = rekord.Forgalmiszám;
                    Soradat["kocsikszáma"] = rekord.Kocsikszáma;
                    Soradat["tervindulás"] = rekord.Tervindulás;
                    Soradat["tényindulás"] = rekord.Tényindulás;
                    Soradat["tervérkezés"] = rekord.Tervérkezés;
                    Soradat["tényérkezés"] = rekord.Tényérkezés;
                    Soradat["miótaáll"] = rekord.Miótaáll;
                    Soradat["napszak"] = rekord.Napszak;
                    Soradat["megjegyzés"] = rekord.Megjegyzés;

                    AdatTábla.Rows.Add(Soradat);
                }
                NapiTábla.DataSource = AdatTábla;

                NapiTábla.Columns["Státus"].Width = 150;
                NapiTábla.Columns["hibaleírása"].Width = 150;
                NapiTábla.Columns["típus"].Width = 150;
                NapiTábla.Columns["azonosító"].Width = 150;
                NapiTábla.Columns["szerelvény"].Width = 150;
                NapiTábla.Columns["viszonylat"].Width = 150;
                NapiTábla.Columns["forgalmiszám"].Width = 150;
                NapiTábla.Columns["kocsikszáma"].Width = 150;
                NapiTábla.Columns["tervindulás"].Width = 150;
                NapiTábla.Columns["tényindulás"].Width = 150;
                NapiTábla.Columns["tervérkezés"].Width = 150;
                NapiTábla.Columns["tényérkezés"].Width = 150;
                NapiTábla.Columns["miótaáll"].Width = 150;
                NapiTábla.Columns["napszak"].Width = 150;
                NapiTábla.Columns["megjegyzés"].Width = 150;

                NapiTábla.Visible = true;
                NapiTábla.Refresh();
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


        private void NapiTábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Részletes_ürítés();
            if (e.RowIndex < 0)
                return;

            R_Státus.Text = NapiTábla.Rows[e.RowIndex].Cells[0].Value.ToString();
            R_hibaleírása.Text = NapiTábla.Rows[e.RowIndex].Cells[1].Value.ToString();
            R_típus.Text = NapiTábla.Rows[e.RowIndex].Cells[2].Value.ToString();
            R_azonosító.Text = NapiTábla.Rows[e.RowIndex].Cells[3].Value.ToString();
            R_szerelvény.Text = NapiTábla.Rows[e.RowIndex].Cells[4].Value.ToString();
            R_viszonylat.Text = NapiTábla.Rows[e.RowIndex].Cells[5].Value.ToString();
            R_forgalmiszám.Text = NapiTábla.Rows[e.RowIndex].Cells[6].Value.ToString();
            R_kocsikszáma.Text = NapiTábla.Rows[e.RowIndex].Cells[7].Value.ToString();
            R_tervindulás.Value = DateTime.Parse(NapiTábla.Rows[e.RowIndex].Cells[8].Value.ToString());
            R_tényindulás.Value = DateTime.Parse(NapiTábla.Rows[e.RowIndex].Cells[9].Value.ToString());
            R_tervérkezés.Value = DateTime.Parse(NapiTábla.Rows[e.RowIndex].Cells[10].Value.ToString());
            R_tényérkezés.Value = DateTime.Parse(NapiTábla.Rows[e.RowIndex].Cells[11].Value.ToString());
            R_miótaáll.Value = DateTime.Parse(NapiTábla.Rows[e.RowIndex].Cells[12].Value.ToString());
            R_napszak.Text = NapiTábla.Rows[e.RowIndex].Cells[13].Value.ToString();
            R_megjegyzés.Text = NapiTábla.Rows[e.RowIndex].Cells[14].Value.ToString();

            // átmegyünk a módosítási lapra
            Fülek.SelectedIndex = 4;
        }


        private void N_keres_Click(object sender, EventArgs e)
        {
            Kereső_hívás("napi");
        }

        private void Szövegkeresés_Napi()
        {
            if (Új_Ablak_Kereső.Keresendő == null) return;
            if (Új_Ablak_Kereső.Keresendő.Trim() == "") return;
            if (NapiTábla.RowCount < 1) return;
            KözösKereső(NapiTábla, Új_Ablak_Kereső.Keresendő.Trim());
        }

        private void ExcelNapi()
        {
            try
            {
                if (NapiTábla.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Napi_részletes_" + Program.PostásNév + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                Module_Excel.EXCELtábla(fájlexc, NapiTábla, false);
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

        private void Button3_Click(object sender, EventArgs e)
        {
            ExcelNapi();
        }


        private void Járműpanel_OK_Click(object sender, EventArgs e)
        {
            try
            {
                if (Járműpanel_Text.Text.Trim() == "") return;
                JárműFőListaFeltöltés();

                Adat_Jármű Elem = (from a in AdatokFőJármű
                                   where a.Azonosító == Járműpanel_Text.Text.Trim()
                                   select a).FirstOrDefault() ?? throw new HibásBevittAdat("A Főmérnökségi adatokban nincs ilyen kocsi.");


                // megnézzük, hogy a napi adatok léteznek-e

                if (!File.Exists(HelyNap)) return;
                FőkönyvNapListaFeltöltés(HelyNap);

                Adat_Főkönyv_Nap ElemNap = (from a in AdatokFőkönyvNap
                                            where a.Azonosító == Járműpanel_Text.Text.Trim()
                                            select a).FirstOrDefault();


                if (ElemNap != null) throw new HibásBevittAdat("Az adott napi adatokban létezik már a kocsi.");

                string jelszó = "lilaakác";
                // rögzítjük a pályaszámot üres adatokkal.

                string szöveg = "INSERT INTO Adattábla  (Státus, hibaleírása, típus, azonosító, szerelvény, ";
                szöveg += "viszonylat, forgalmiszám, kocsikszáma, tervindulás, tényindulás, ";
                szöveg += "tervérkezés, tényérkezés, miótaáll, napszak, megjegyzés ) VALUES (";
                szöveg += $"4, '-', '-', '{Járműpanel_Text.Text.Trim()}', 0, ";
                szöveg += "'-', '-', 0, '1900.01.01. 00:00:00', '1900.01.01. 00:00:00', ";
                szöveg += "'1900.01.01. 00:00:00', '1900.01.01. 00:00:00', ";
                szöveg += "'1900.01.01. 00:00:00', ";
                szöveg += "  '*','-')";
                MyA.ABMódosítás(HelyNap, jelszó, szöveg);

                MessageBox.Show("Az adatok rögzítése megtörtént.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Járműpanel_panel.Visible = false;
                NapiTábla_kiírás(0);
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


        private void Járműpanel_bezár_Click(object sender, EventArgs e)
        {
            Járműpanel_panel.Visible = false;
        }


        private void Jármű_panel_be_Click(object sender, EventArgs e)
        {
            Járműpanel_Text.Text = "";
            Járműpanel_Text.Focus();
            AcceptButton = Járműpanel_OK;
            Járműpanel_panel.Visible = true;
        }
        #endregion


        #region részletes adatok lapfül

        private void Napszak_Conmbofeltöltés()
        {
            R_napszak.Items.Clear();
            R_napszak.Items.Add("DE");
            R_napszak.Items.Add("DU");
            R_napszak.Items.Add("*");
        }


        private void Cmbtelephely_SelectedIndexChanged(object sender, EventArgs e)
        {
            R_Típus_feltöltés();
            JárműListaFeltöltés();
        }


        private void R_Típus_feltöltés()
        {

            R_típus.Items.Clear();

            string hely = $@"{Application.StartupPath}\" + Cmbtelephely.Text + @"\adatok\villamos\Jármű.mdb";
            string jelszó = "pozsgaii";
            string szöveg = "SELECT * FROM típustábla order by id";

            R_típus.BeginUpdate();
            R_típus.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "típus"));
            R_típus.EndUpdate();
            R_típus.Refresh();

        }


        private void Részletes_ürítés()
        {

            R_Státus.Text = "";
            R_hibaleírása.Text = "";
            R_típus.Text = "";
            R_azonosító.Text = "";
            R_szerelvény.Text = "";
            R_viszonylat.Text = "";
            R_forgalmiszám.Text = "";
            R_kocsikszáma.Text = "";
            R_tervindulás.Value = DateTime.Parse("1900.01.01. 00:00:00");
            R_tényindulás.Value = DateTime.Parse("1900.01.01. 00:00:00");
            R_tervérkezés.Value = DateTime.Parse("1900.01.01. 00:00:00");
            R_tényérkezés.Value = DateTime.Parse("1900.01.01. 00:00:00");
            R_miótaáll.Value = DateTime.Parse("1900.01.01");
            R_napszak.Text = "";
            R_megjegyzés.Text = "";

        }

        private void R_frissít_Click(object sender, EventArgs e)
        { R_frissít_eljárás(); }


        private void R_frissít_eljárás()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\" + Dátum.Value.ToString("yyyy") + @"\nap\" + Dátum.Value.ToString("yyyyMMdd");

                if (Délelőtt.Checked)
                    hely += "denap.mdb";
                else
                    hely += "dunap.mdb";

                if (!File.Exists(hely))
                    return;

                string jelszó = "lilaakác";
                string szöveg = "SELECT * FROM Adattábla WHERE azonosító='" + R_azonosító.Text.Trim() + "'";


                Adat_Főkönyv_Nap rekord = KézFőkönyvNap.Egy_Adat(hely, jelszó, szöveg);

                if (rekord != null)
                {
                    R_Státus.Text = rekord.Státus.ToString();
                    R_hibaleírása.Text = rekord.Hibaleírása.Trim();
                    R_típus.Text = rekord.Típus.Trim();
                    R_szerelvény.Text = rekord.Szerelvény.ToString();
                    R_viszonylat.Text = rekord.Viszonylat.ToString();
                    R_forgalmiszám.Text = rekord.Forgalmiszám.ToString();
                    R_kocsikszáma.Text = rekord.Kocsikszáma.ToString();
                    R_tervindulás.Value = rekord.Tervindulás;
                    R_tényindulás.Value = rekord.Tényindulás;
                    R_tervérkezés.Value = rekord.Tervérkezés;
                    R_tényérkezés.Value = rekord.Tényérkezés;
                    R_miótaáll.Value = rekord.Miótaáll;
                    R_napszak.Text = rekord.Napszak.Trim();
                    R_megjegyzés.Text = rekord.Megjegyzés.Trim();
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


        private void R_rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (R_típus.Text.Trim() == "") throw new HibásBevittAdat("A járműtípust meg kell adni.");
                if (R_hibaleírása.Text.Trim() == "") R_hibaleírása.Text = "-";
                if (R_Státus.Text.Trim() == "") R_Státus.Text = "0";
                if (int.TryParse(R_szerelvény.Text, out int result) == false) R_szerelvény.Text = "0";
                if (R_szerelvény.Text.Trim() == "") R_szerelvény.Text = "0";
                if (R_viszonylat.Text.Trim() == "") R_viszonylat.Text = "-";
                if (R_forgalmiszám.Text.Trim() == "") R_forgalmiszám.Text = "-";
                if (int.TryParse(R_kocsikszáma.Text, out int result1) == false) R_kocsikszáma.Text = "0";
                if (R_kocsikszáma.Text.Trim() == "") R_kocsikszáma.Text = "0";
                if (!System.IO.File.Exists(HelyNap)) return;

                FőkönyvNapListaFeltöltés(HelyNap);
                string jelszó = "lilaakác";

                Adat_Főkönyv_Nap Elem = (from a in AdatokFőkönyvNap
                                         where a.Azonosító == R_azonosító.Text.Trim()
                                         select a).FirstOrDefault();

                if (Elem != null)
                {
                    string szöveg = "UPDATE Adattábla SET ";
                    szöveg += " Státus=" + R_Státus.Text.Trim() + ", ";
                    szöveg += " hibaleírása='" + R_hibaleírása.Text.Trim() + "', ";
                    szöveg += " típus='" + R_típus.Text.Trim() + "', ";
                    szöveg += " szerelvény=" + R_szerelvény.Text.Trim() + ", ";
                    szöveg += " viszonylat='" + R_viszonylat.Text.Trim() + "', ";
                    szöveg += " forgalmiszám='" + R_forgalmiszám.Text.Trim() + "', ";
                    szöveg += " kocsikszáma=" + R_kocsikszáma.Text.Trim() + ", ";
                    szöveg += " tervindulás='" + R_tervindulás.Value + "', ";
                    szöveg += " tényindulás='" + R_tényindulás.Value + "', ";
                    szöveg += " tervérkezés='" + R_tervérkezés.Value + "', ";
                    szöveg += " tényérkezés='" + R_tényérkezés.Value + "', ";
                    szöveg += " miótaáll='" + R_miótaáll.Value + "', ";
                    szöveg += " napszak='" + R_napszak.Text.Trim() + "', ";
                    szöveg += " megjegyzés='" + R_megjegyzés.Text.Trim() + "'";
                    szöveg += " WHERE azonosító='" + R_azonosító.Text.Trim() + "'";
                    MyA.ABMódosítás(HelyNap, jelszó, szöveg);
                }
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Rögzítés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                NapiTábla_kiírás(0);
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


        private void R_törlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Biztos, hogy törüljük a járművet?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
                if (!System.IO.File.Exists(HelyNap)) return;

                FőkönyvNapListaFeltöltés(HelyNap);
                string jelszó = "lilaakác";

                Adat_Főkönyv_Nap Elem = (from a in AdatokFőkönyvNap
                                         where a.Azonosító == R_azonosító.Text.Trim()
                                         select a).FirstOrDefault();

                if (Elem != null)
                {
                    string szöveg = $"DELETE FROM adattábla where azonosító='{R_azonosító.Text.Trim()}'";
                    MyA.ABtörlés(HelyNap, jelszó, szöveg);
                }

                MessageBox.Show("A kocsi törlésre került az adott napi és napszaki adatokból", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                NapiTábla_kiírás(0);
                Kocsikiirása_gombok();
                // átmegyünk a táblázati lapra
                Fülek.SelectedIndex = 3;
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


        #region Részletes Gombok Lapfül
        private void Button4_Click(object sender, EventArgs e)
        {
            Kocsikiirása_gombok();
        }

        private void Kocsikiirása_gombok()
        {
            try
            {
                GombokPanel.Controls.Clear();
                int darab = 0;

                // Adatok betöltése
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\{Dátum.Value.Year}\nap\{Dátum.Value:yyyyMMdd}";
                if (Délelőtt.Checked)
                    hely += "denap.mdb";
                else
                    hely += "dunap.mdb";

                if (!System.IO.File.Exists(hely)) return;
                FőkönyvNapListaFeltöltés(hely);

                int i = 1;
                int j = 1;
                int k = 1;
                if (AdatokFőkönyvNap != null && AdatokFőkönyvNap.Count > 0)
                {
                    foreach (Adat_Főkönyv_Nap rekord in AdatokFőkönyvNap)
                    {
                        Button Telephelygomb = new Button
                        {
                            Location = new Point(10 + 80 * (k - 1), 10 + 60 * (j - 1)),
                            Size = new Size(70, 50),
                            Name = "Kocsi_" + (darab + 1),
                            Text = rekord.Azonosító.Trim() + "\n" + MyF.Szöveg_Tisztítás(rekord.Típus, 0, 5)
                        };

                        if (rekord.Viszonylat.Trim() != "-")
                        {
                            // forgalomban volt zöld
                            Telephelygomb.BackColor = Color.LimeGreen;
                        }
                        // ha rossz volt
                        else if (rekord.Státus == 4)
                        {
                            // ha rossz piros
                            Telephelygomb.BackColor = Color.Red;
                            if (rekord.Hibaleírása.Contains("§"))
                            {
                                // telepenkívül kék
                                Telephelygomb.BackColor = Color.Blue;
                            }
                            if (rekord.Hibaleírása.Contains("#"))
                            {
                                // főjavítás sárga
                                Telephelygomb.BackColor = Color.Yellow;
                            }
                            if (rekord.Hibaleírása.Contains("&"))
                            {
                                // Félreállítás narancssárga
                                Telephelygomb.BackColor = Color.DarkOrange;
                            }
                        }
                        else
                        {
                            // tartalék szürke
                            Telephelygomb.BackColor = Color.Silver;
                        }

                        Telephelygomb.Visible = true;
                        ToolTip1.SetToolTip(Telephelygomb, rekord.Státus.ToString());

                        // AddHandler Telephelygomb.Click, AddressOf Telephelyre_Click
                        Telephelygomb.MouseDown += Telephelyre_MouseDown;

                        GombokPanel.Controls.Add(Telephelygomb);

                        k += 1;
                        if (k == 16)
                        {
                            k = 1;
                            j += 1;
                        }
                        i += 1;
                        darab += 1;
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

        private void Telephelyre_MouseDown(object sender, MouseEventArgs e)
        {
            Részletes_ürítés();
            // megkeressük a szöveget a táblázatban
            NapiTábla_kiírás(0);
            if (NapiTábla.Rows.Count < 0) return;
            string[] gombfelirat = sender.ToString().Split('\n');
            string melyikpsz = gombfelirat[0].Trim().Substring(gombfelirat[0].Length - 4, 4);
            R_azonosító.Text = melyikpsz.Trim();
            R_frissít_eljárás();
            // átmegyünk a módosítási lapra
            Fülek.SelectedIndex = 4;
        }
        #endregion


        #region főkönyv készül

        private void SZál_Főkönyv(Action callback)
        {
            Thread proc = new Thread(() =>
            {
                Főkönyv_Főkönyv nyomtatvány = new Főkönyv_Főkönyv();

                // elkészítjük a formanyomtatványt változókat nem lehet küldeni definiálni kell egy külső változót.
                nyomtatvány.Főkönyv_Alap(Telephely_, szövegd_, napszak_, Dátum_, fájlnév_);

                this.Invoke(callback, new object[] { });
            });
            proc.Start();
        }

        private void Főkönyv_Click(object sender, EventArgs e)
        {

            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\{Dátum.Value.Year}\nap\{Dátum.Value:yyyyMMdd}";
            if (Délelőtt.Checked)
                hely += "denap.mdb";
            else
                hely += "dunap.mdb";

            if (!System.IO.File.Exists(hely)) return;

            string szövegd = Dátum.Value.ToString("yyyy.MM.dd");
            string fájlexc = Dátum.Value.ToString("yyyy.MM.dd") + "-Főkönyv-";
            if (Délelőtt.Checked)
            {
                fájlexc += "de-";
                szövegd += " De.";
            }
            else
            {
                fájlexc += "du-";
                szövegd += " Du.";
            }
            fájlexc += Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss");

            // ha gyűjtő hely akkor odamentjük
            string ideig = "MyDocuments";
            hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő1.mdb";
            Kezelő_Kiegészítő_Mentésihelyek KézMentés = new Kezelő_Kiegészítő_Mentésihelyek();
            List<Adat_Kiegészítő_Mentésihelyek> AdatokMentés = KézMentés.Lista_Adatok(hely);
            Adat_Kiegészítő_Mentésihelyek AdatMentés = (from a in AdatokMentés
                                                        where a.Sorszám == 1
                                                        select a).FirstOrDefault();
            if (AdatMentés != null)
            {
                //ha van és nem NINCS
                if (AdatMentés.Elérésiút.Trim().ToUpper() != "NINCS") ideig = AdatMentés.Elérésiút;
            }
            else
            {
                Adat_Kiegészítő_Mentésihelyek ADAT = new Adat_Kiegészítő_Mentésihelyek(1, "Főkönyv készítés", "NINCS");
                KézMentés.Rögzítés(hely, ADAT);
            }
            Főkönyv.Visible = false;

            // kimeneti fájl helye és neve
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog
            {
                InitialDirectory = ideig,
                Title = "Főkönyv készítés",
                FileName = fájlexc,
                Filter = "Excel |*.xlsx"
            };
            // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
            if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                fájlexc = SaveFileDialog1.FileName;
            else
            {
                Főkönyv.Visible = true;
                return;
            }

            Holtart.Be();
            timer1.Enabled = true;
            fájlnév_ = fájlexc.Trim();
            Telephely_ = Cmbtelephely.Text.Trim();
            szövegd_ = szövegd;
            napszak_ = Délelőtt.Checked ? "de" : "du";
            Dátum_ = Dátum.Value;

            SZál_Főkönyv(() =>
            { //leállítjuk a számlálót és kikapcsoljuk a holtartot.
                timer1.Enabled = false;
                Holtart.Ki();
                MessageBox.Show("A nyomtatvány elkészült !", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });

            // járműreklámot kiírjuk ha a feltétleknek megfelel
            if (Dátum.Value == DateTime.Today)
            {
                if (Reklám_Check.Checked)
                {
                    Reklám_eltérés();

                    Vezénylésbeírás_eljárás();
                }
            }
            Főkönyv.Visible = true;
        }
        #endregion


        #region meghagyás

        private void Meghagyás_Click(object sender, EventArgs e)
        {

            string fájlexc;
            // kimeneti fájl helye és neve
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog
            {
                InitialDirectory = "MyDocuments",
                Title = "Meghagyás készítés",
                FileName = $"Meghagyás_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                Filter = "Excel |*.xlsx"
            };
            // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
            if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                fájlexc = SaveFileDialog1.FileName;
            else
                return;


            Holtart.Be();
            timer1.Enabled = true;
            fájlnév_ = fájlexc.Trim();
            Telephely_ = Cmbtelephely.Text.Trim();
            Dátum_ = Dátum.Value;
            PapírElrendezés_ = PapírElrendezés.Text.Trim();
            Papírméret_ = Papírméret.Text.Trim();


            SZál_Meghagyás(() =>
            { //leállítjuk a számlálót és kikapcsoljuk a holtartot.
                timer1.Enabled = false;
                Holtart.Ki();
                MessageBox.Show("A nyomtatvány elkészült !", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
        }


        private void SZál_Meghagyás(Action value)
        {
            Thread proc = new Thread(() =>
             {
                 Főkönyv_Meghagyás nyomtatvány = new Főkönyv_Meghagyás();

                 // elkészítjük a formanyomtatványt változókat nem lehet küldeni definiálni kell egy külső változót.
                 nyomtatvány.Főkönyv_Meghagyáskészítés(fájlnév_, Telephely_, Dátum_, Papírméret_, PapírElrendezés_);

                 this.Invoke(value, new object[] { });
             });
            proc.Start();
        }
        #endregion


        #region Beálló lista

        private void Beállólista_Click(object sender, EventArgs e)
        {
            try
            {
                string fájlexc;
                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Beálló kocsi lista készítés",
                    FileName = "Beálló_" + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                Holtart.Be(100);
                timer1.Enabled = true;

                fájlnév_ = fájlexc.Trim();
                Telephely_ = Cmbtelephely.Text.Trim();
                Dátum_ = Dátum.Value;
                napszak_ = Délelőtt.Checked ? "de" : "du";
                PapírElrendezés_ = PapírElrendezés.Text.Trim();
                Papírméret_ = Papírméret.Text.Trim();

                SZál_Beálló(() =>
                { //leállítjuk a számlálót és kikapcsoljuk a holtartot.
                    timer1.Enabled = false;
                    Holtart.Ki();
                    MessageBox.Show("A nyomtatvány elkészült !", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
                });

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

        private void SZál_Beálló(Action callback)
        {
            Thread proc = new Thread(() =>
            {
                Főkönyv_Beálló nyomtatvány = new Főkönyv_Beálló();


                // elkészítjük a formanyomtatványt változókat nem lehet küldeni definiálni kell egy külső változót.
                nyomtatvány.Beálló_kocsik(fájlnév_, Telephely_, Dátum_, napszak_, Papírméret_, PapírElrendezés_);

                this.Invoke(callback, new object[] { });
            });
            proc.Start();
        }

        #endregion


        #region TW6000 ütemezés
        private void TW6000ütemezés()
        {
            string szöveg1;
            string helyütemez = $@"{Application.StartupPath}\főmérnökség\adatok\villamos4TW.mdb";
            string jelszóütemez = "czapmiklós";

            // Módosítjuk a jármű státuszát
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";
            string jelszó = "pozsgaii";
            JárműListaFeltöltés();

            // megnyitjuk a hibákat
            string helyhiba = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\hiba.mdb";
            string jelszóhiba = "pozsgaii";
            string szöveg = "SELECT * FROM hibatábla ";
            Kezelő_jármű_hiba KézHiba = new Kezelő_jármű_hiba();
            List<Adat_Jármű_hiba> AdatokHiba = KézHiba.Lista_adatok(helyhiba, jelszóhiba, szöveg);

            // naplózás
            string helynapló = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\hibanapló\{DateTime.Now:yyyyMM}hibanapló.mdb";
            if (!File.Exists(helynapló)) Adatbázis_Létrehozás.Hibatáblalap(helynapló);
            string jelszónapló = "pozsgaii";

            // Másik napózás
            string helynapló2 = $@"{Application.StartupPath}\főmérnökség\napló\naplóTW6000Ütem_{DateTime.Today.Year}.mdb";
            string jelszónapló2 = "czapmiklós";
            if (!File.Exists(helynapló2)) Adatbázis_Létrehozás.TW6000ütemnapló(helynapló2);

            szöveg = "SELECT * FROM ütemezés where ";
            szöveg += $" vütemezés=#{Dátum.Value.AddDays(1):MM-dd-yyyy}#";
            szöveg += " and státus=2";
            szöveg += " order by azonosító";

            Kezelő_TW6000_Ütemezés kéz = new Kezelő_TW6000_Ütemezés();
            List<Adat_TW6000_Ütemezés> Adatok = kéz.Lista_Adatok(helyütemez, jelszóütemez, szöveg);

            Holtart.Be(100);



            foreach (Adat_TW6000_Ütemezés rekordütemez in Adatok)
            {
                Holtart.Lép();

                // megnézzük, hogy a telephelyen van-e a kocsi
                Adat_Jármű ElemJármű = (from a in AdatokJármű
                                        where a.Azonosító == rekordütemez.Azonosító
                                        select a).FirstOrDefault();
                if (ElemJármű != null)
                {
                    // ha telephelyen van a kocsi
                    // hiba leírása
                    szöveg1 = rekordütemez.Vizsgfoka.Trim() + "-" + rekordütemez.Vsorszám + "-" + rekordütemez.Vütemezés.ToString("yyyy.MM.dd");

                    // Megnézzük, hogy volt-e már rögzítve ilyen szöveg
                    bool talált = false;
                    Adat_Jármű_hiba ElemHiba = (from a in AdatokHiba
                                                where a.Azonosító == rekordütemez.Azonosító && a.Hibaleírása.Contains(szöveg1.Trim())
                                                select a).FirstOrDefault();
                    if (ElemHiba != null) talált = true;

                    // ha már volt ilyen szöveg rögzítve a pályaszámhoz akkor nem rögzítjük mégegyszer
                    if (!talált)
                    {
                        // hibák számát emeljük és státus állítjuk ha kell

                        long hibáksorszáma = 0;
                        string típusa = "";
                        long státus = 0;

                        if (ElemJármű != null)
                        {
                            hibáksorszáma = ElemJármű.Hibák;
                            típusa = ElemJármű.Típus;
                            státus = ElemJármű.Státus;
                        }
                        // hibák számát emeljük és státus állítjuk ha kell

                        // rögzítjük a villamos.mdb-be
                        szöveg = "UPDATE állománytábla SET ";
                        szöveg += $" hibák={hibáksorszáma + 1}, ";
                        if (státus < 4)
                            szöveg += " státus=3 ";
                        else
                            szöveg += " státus=4 ";

                        szöveg += $" WHERE  [azonosító]='{rekordütemez.Azonosító.Trim()}'";
                        MyA.ABMódosítás(hely, jelszó, szöveg);


                        // beírjuk a hibákat
                        szöveg = "INSERT INTO hibatábla (létrehozta, korlát, hibaleírása, idő, javítva, típus, azonosító, hibáksorszáma ) VALUES (";
                        szöveg += "'" + Program.PostásNév.Trim() + "', 3, ";
                        szöveg += "'" + szöveg1.Trim() + "', ";
                        szöveg += "'" + DateTime.Now.ToString() + "', false, ";
                        szöveg += "'" + típusa.Trim() + "', ";
                        szöveg += "'" + rekordütemez.Azonosító.Trim() + "', " + (hibáksorszáma + 1).ToString() + ")";
                        MyA.ABMódosítás(helyhiba, jelszóhiba, szöveg);
                        // naplózzuk a hibákat
                        MyA.ABMódosítás(helynapló, jelszónapló, szöveg);


                        // módosítjuk az ütemezett adatokat is
                        szöveg = "UPDATE ütemezés SET ";
                        szöveg += " megjegyzés='Előjegyezve: " + Program.PostásTelephely.Trim() + "', státus=4 ";
                        szöveg += " WHERE  vütemezés=#" + Dátum.Value.AddDays(1).ToString("MM-dd-yyyy") + "#  And státus=2 ";
                        szöveg += " AND azonosító='" + rekordütemez.Azonosító.Trim() + "'";
                        MyA.ABMódosítás(helyütemez, jelszóütemez, szöveg);

                        // naplózzuk a TW6000-be is
                        szöveg = "INSERT INTO ütemezésnapló (azonosító, ciklusrend, vizsgfoka, vsorszám, megjegyzés, vesedékesség, vütemezés, vvégezte, ";
                        szöveg += "  velkészülés, státus, elkészült, rögzítő, rögzítésideje) VALUES (";

                        szöveg += "'" + rekordütemez.Azonosító.Trim() + "', ";
                        szöveg += "'" + rekordütemez.Ciklusrend.Trim() + "', ";
                        szöveg += "'" + rekordütemez.Vizsgfoka.Trim() + "', ";
                        szöveg += rekordütemez.Vsorszám.ToString() + ", ";
                        szöveg += "'" + rekordütemez.Megjegyzés.Trim() + "', ";
                        szöveg += "'" + rekordütemez.Vesedékesség.ToString() + "', ";
                        szöveg += "'" + rekordütemez.Vütemezés.ToString() + "', ";
                        szöveg += "'" + rekordütemez.Vvégezte.Trim() + "', ";
                        szöveg += "'" + rekordütemez.Velkészülés.ToString() + "', ";
                        szöveg += rekordütemez.Státus.ToString() + ", ";
                        szöveg += rekordütemez.Elkészült + ", ";
                        szöveg += "'" + Program.PostásNév.Trim() + "', ";
                        szöveg += "'" + DateTime.Now.ToString() + "')";
                        MyA.ABMódosítás(helynapló2, jelszónapló2, szöveg);
                    }
                }
            }
            Holtart.Ki();
        }
        #endregion


        #region ICS ütemezés

        private void ICSÜtemezés()
        {

            DateTime Dátum_ütem = Dátum.Value.AddDays(1);
            string helyütemez = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\főkönyv\futás\{Dátum_ütem.Year}\vezénylés{Dátum_ütem.Year}.mdb";
            if (!File.Exists(helyütemez)) return;
            string jelszóütemez = "tápijános";

            string szöveg = $"SELECT * FROM vezényléstábla where [törlés]=0 and [dátum]=#{Dátum_ütem:M-d-yy}# AND típus='ICS' order by  azonosító";

            // Módosítjuk a jármű státuszát
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";
            string jelszó = "pozsgaii";
            JárműListaFeltöltés();

            // megnyitjuk a hibákat
            string helyhiba = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\hiba.mdb";
            string jelszóhiba = "pozsgaii";
            szöveg = "SELECT * FROM hibatábla ";
            Kezelő_jármű_hiba KézHiba = new Kezelő_jármű_hiba();
            List<Adat_Jármű_hiba> AdatokHiba = KézHiba.Lista_adatok(helyhiba, jelszóhiba, szöveg);


            // naplózás
            string helynapló = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\hibanapló\{DateTime.Now:yyyyMM}hibanapló.mdb";
            if (!File.Exists(helynapló)) Adatbázis_Létrehozás.Hibatáblalap(helynapló);

            Holtart.Be();
            Kezelő_Vezénylés kéz = new Kezelő_Vezénylés();
            szöveg = $"SELECT * FROM vezényléstábla where [törlés]=0 and [dátum]=#{Dátum_ütem:M-d-yy}# AND típus='ICS' order by  azonosító";
            List<Adat_Vezénylés> Adatok = kéz.Lista_Adatok(helyütemez, jelszóütemez, szöveg);

            DateTime mikor;
            // ha van ütemezett kocsi
            foreach (Adat_Vezénylés rekordütemez in Adatok)
            {
                Holtart.Lép();
                if (rekordütemez.Takarításraütemez == 1 || rekordütemez.Vizsgálatraütemez == 1)
                {
                    // hiba leírása
                    string szöveg1 = rekordütemez.Vizsgálat.Trim() + "-" + rekordütemez.Vizsgálatszám;
                    string szöveg3 = szöveg1;

                    if (rekordütemez.Státus == 4)
                        szöveg1 += "-" + rekordütemez.Dátum.ToString("yyyy.MM.dd.") + " Maradjon benn ";
                    else
                        szöveg1 += "-" + rekordütemez.Dátum.ToString("yyyy.MM.dd.") + " Beálló ";

                    if (rekordütemez.Takarításraütemez == 1)
                        szöveg1 += "+Mosó ";

                    // Megnézzük, hogy volt-e már rögzítve ilyen szöveg
                    bool talált = false;

                    Adat_Jármű_hiba ElemHiba = (from a in AdatokHiba
                                                where a.Azonosító == rekordütemez.Azonosító && a.Hibaleírása.Contains(szöveg3.Trim())
                                                select a).FirstOrDefault();
                    if (ElemHiba != null) talált = true;

                    ElemHiba = (from a in AdatokHiba
                                where a.Azonosító == rekordütemez.Azonosító && a.Hibaleírása.Contains(szöveg1.Trim())
                                select a).FirstOrDefault();
                    if (ElemHiba != null) talált = true;

                    int szín = 0;
                    // ha már volt ilyen szöveg rögzítve a pályaszámhoz akkor nem rögzítjük mégegyszer
                    if (!talált)
                    {
                        // hibák számát emeljük és státus állítjuk ha kell
                        Adat_Jármű ElemJármű = (from a in AdatokJármű
                                                where a.Azonosító == rekordütemez.Azonosító.Trim()
                                                select a).FirstOrDefault();
                        long hibáksorszáma = 0;
                        string típusa = "";
                        long státus = 0;
                        long újstátus = 0;
                        if (ElemJármű != null)
                        {
                            hibáksorszáma = ElemJármű.Hibák;
                            típusa = ElemJármű.Típus;
                            státus = ElemJármű.Státus;
                        }

                        szín = 1;
                        long hiba = hibáksorszáma + 1;
                        if (státus != 4) // ha 4 státusa akkor nem kell módosítani.
                        {
                            // ha a következő napra ütemez
                            if (DateTime.Today.AddDays(1).ToString("yyyy.MM.dd") == Dátum_ütem.ToString("yyyy.MM.dd"))
                            {
                                if (rekordütemez.Státus == 4)
                                {
                                    státus = 4;
                                    mikor = DateTime.Now;
                                }
                                else
                                {
                                    státus = 3;
                                }
                            }
                            else if (státus < 4)
                                státus = 3;
                        }
                        else
                        {
                            újstátus = 1;
                        }

                        // rögzítjük a villamos.mdb-be
                        szöveg = "UPDATE állománytábla SET ";
                        szöveg += " hibák=" + hiba.ToString() + ", ";
                        // csak akkor módosítkjuk a dátumot, ha nem áll
                        if (státus == 4 && újstátus == 0)
                            szöveg += " miótaáll='" + DateTime.Now.ToString() + "', ";

                        szöveg += " státus=" + státus.ToString();
                        szöveg += " WHERE  [azonosító]='" + rekordütemez.Azonosító.Trim() + "'";
                        MyA.ABMódosítás(hely, jelszó, szöveg);


                        // beírjuk a hibákat

                        if (szín == 1)
                        {
                            szöveg = "INSERT INTO hibatábla (létrehozta, korlát, hibaleírása, idő, javítva, típus, azonosító, hibáksorszáma ) VALUES (";
                            szöveg += $"'{Program.PostásNév.Trim()}', ";
                            // ha a következő napra ütemez
                            if (DateTime.Today.AddDays(1).ToString("yyyy.MM.dd") == Dátum_ütem.ToString("yyyy.MM.dd"))
                            {
                                if (rekordütemez.Státus == 4)
                                {
                                    szöveg += " 4, ";
                                }
                                else
                                {
                                    szöveg += " 3, ";
                                }
                            }
                            else
                            {
                                szöveg += " 3, ";
                            }
                            szöveg += $"'{szöveg1.Trim()}', ";
                            szöveg += $"'{DateTime.Now}', false, ";
                            szöveg += $"'{típusa.Trim()}', ";
                            szöveg += $"'{rekordütemez.Azonosító.Trim()}', {hibáksorszáma})";
                            MyA.ABMódosítás(helyhiba, jelszó, szöveg);
                            // naplózzuk a hibákat
                            MyA.ABMódosítás(helynapló, jelszó, szöveg);
                        }
                    }
                }
            }
            Holtart.Ki();
        }
        #endregion


        #region T5C5 ütemezés
        private void T5C5Ütemezés()
        {
            DateTime Dátum_ütem = Dátum.Value.AddDays(1);
            string helyütemez = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\főkönyv\futás\{Dátum_ütem.Year}\vezénylés{Dátum_ütem.Year}.mdb";
            if (!File.Exists(helyütemez)) return;
            string jelszóütemez = "tápijános";

            string szöveg = "SELECT * FROM vezényléstábla where [törlés]=0 and [dátum]=#" + Dátum_ütem.ToString("M-d-yy") + "# AND típus Like  '%T5C5%' order by  azonosító";

            // Módosítjuk a jármű státuszát
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";
            string jelszó = "pozsgaii";
            JárműListaFeltöltés();

            // megnyitjuk a hibákat
            string helyhiba = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\hiba.mdb";
            string jelszóhiba = "pozsgaii";
            szöveg = "SELECT * FROM hibatábla ";
            Kezelő_jármű_hiba KézHiba = new Kezelő_jármű_hiba();
            List<Adat_Jármű_hiba> AdatokHiba = KézHiba.Lista_adatok(helyhiba, jelszóhiba, szöveg);

            // naplózás
            string helynapló = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\hibanapló\{DateTime.Now:yyyyMM}hibanapló.mdb";
            if (!File.Exists(helynapló)) Adatbázis_Létrehozás.Hibatáblalap(helynapló);

            Holtart.Be(100);

            Kezelő_Vezénylés kéz = new Kezelő_Vezénylés();
            szöveg = $"SELECT * FROM vezényléstábla where [törlés]=0 and [dátum]=#{Dátum_ütem:M-d-yy}# AND típus Like  '%T5C5%' order by  azonosító";
            List<Adat_Vezénylés> Adatok = kéz.Lista_Adatok(helyütemez, jelszóütemez, szöveg);


            DateTime mikor;

            // ha van ütemezett kocsi
            foreach (Adat_Vezénylés rekordütemez in Adatok)
            {
                Holtart.Lép();

                if (rekordütemez.Takarításraütemez == 1 || rekordütemez.Vizsgálatraütemez == 1)
                {
                    // hiba leírása
                    string szöveg1 = "";
                    string szöveg3 = "KARÓRARUGÓ";
                    if (rekordütemez.Vizsgálatraütemez == 1)
                    {
                        if (rekordütemez.Vizsgálat.Contains("V1"))
                        {
                            string helyT5C5 = $@"{Application.StartupPath}\Főmérnökség\Adatok\T5C5\Villamos4T5C5.mdb";
                            string jelszóT5C5 = "pocsaierzsi";
                            szöveg = $"Select * FROM KMtábla WHERE törölt=false  AND azonosító='{rekordütemez.Azonosító.Trim()}' ORDER BY  vizsgdátumk desc ";
                            Kezelő_T5C5_Kmadatok KézT5C5 = new Kezelő_T5C5_Kmadatok();
                            Adat_T5C5_Kmadatok AdatokT5C5 = KézT5C5.Egy_Adat(helyT5C5, jelszóT5C5, szöveg);
                            string KövetkezőV = AdatokT5C5.KövV;
                            long kövVSorszám = AdatokT5C5.KövV_sorszám;
                            szöveg1 += KövetkezőV.Trim() + "-" + kövVSorszám.ToString();
                            szöveg3 = szöveg1;
                        }
                        else
                        {
                            szöveg1 += rekordütemez.Vizsgálat.Trim() + " ";
                        }

                    }

                    if (rekordütemez.Státus == 4)
                    {
                        szöveg1 += "-" + rekordütemez.Dátum.ToString("yyyy.MM.dd.") + " Maradjon benn ";
                    }
                    else
                    {
                        szöveg1 += "-" + rekordütemez.Dátum.ToString("yyyy.MM.dd.") + " Beálló ";
                    }
                    if (rekordütemez.Takarításraütemez == 1)
                    {
                        szöveg1 += "+Mosó ";
                    }
                    // Megnézzük, hogy volt-e már rögzítve ilyen szöveg
                    bool talált = false;

                    Adat_Jármű_hiba ElemHiba = (from a in AdatokHiba
                                                where a.Azonosító == rekordütemez.Azonosító && a.Hibaleírása.Contains(szöveg3.Trim())
                                                select a).FirstOrDefault();
                    if (ElemHiba != null) talált = true;

                    ElemHiba = (from a in AdatokHiba
                                where a.Azonosító == rekordütemez.Azonosító && a.Hibaleírása.Contains(szöveg1.Trim())
                                select a).FirstOrDefault();
                    if (ElemHiba != null) talált = true;

                    int szín = 0;
                    // ha már volt ilyen szöveg rögzítve a pályaszámhoz akkor nem rögzítjük mégegyszer
                    if (!talált)
                    {
                        // hibák számát emeljük és státus állítjuk ha kell
                        Adat_Jármű ElemJármű = (from a in AdatokJármű
                                                where a.Azonosító == rekordütemez.Azonosító.Trim()
                                                select a).FirstOrDefault();
                        long hibáksorszáma = 0;
                        string típusa = "";
                        long státus = 0;
                        long újstátus = 0;
                        if (ElemJármű != null)
                        {
                            hibáksorszáma = ElemJármű.Hibák;
                            típusa = ElemJármű.Típus;
                            státus = ElemJármű.Státus;
                        }

                        szín = 1;
                        long hiba = hibáksorszáma + 1;

                        if (státus != 4) // ha 4 státusa akkor nem kell módosítani.
                        {
                            // ha a következő napra ütemez
                            if (DateTime.Today.ToString("yyyy.MM.dd") == Dátum_ütem.ToString("yyyy.MM.dd"))
                            {
                                if (rekordütemez.Státus == 4)
                                {
                                    státus = 4;
                                    mikor = DateTime.Now;
                                }
                                else
                                {
                                    státus = 3;
                                }
                            }
                            else if (státus < 4)
                                státus = 3;
                        }
                        else
                        {
                            újstátus = 1;
                        }

                        // rögzítjük a villamos.mdb-be
                        szöveg = "UPDATE állománytábla SET ";
                        szöveg += " hibák=" + hiba.ToString() + ", ";
                        // csak akkor módosítkjuk a dátumot, ha nem áll
                        if (státus == 4 && újstátus == 0)
                            szöveg += " miótaáll='" + DateTime.Now.ToString() + "', ";
                        szöveg += " státus=" + státus.ToString();
                        szöveg += " WHERE  [azonosító]='" + rekordütemez.Azonosító.Trim() + "'";
                        MyA.ABMódosítás(hely, jelszó, szöveg);


                        // beírjuk a hibákat

                        if (szín == 1)
                        {
                            szöveg = "INSERT INTO hibatábla (létrehozta, korlát, hibaleírása, idő, javítva, típus, azonosító, hibáksorszáma ) VALUES (";
                            szöveg += "'" + Program.PostásNév.Trim() + "', ";
                            // ha a következő napra ütemez
                            if (DateTime.Today.AddDays(1) == Dátum_ütem)
                            {
                                if (rekordütemez.Státus == 4)
                                {
                                    szöveg += " 4, ";
                                }
                                else
                                {
                                    szöveg += " 3, ";
                                }
                            }
                            else
                            {
                                szöveg += " 3, ";
                            }
                            szöveg += "'" + szöveg1.Trim() + "', ";
                            szöveg += "'" + DateTime.Now.ToString() + "', false, ";
                            szöveg += "'" + típusa.Trim() + "', ";
                            szöveg += "'" + rekordütemez.Azonosító.Trim() + "', " + hibáksorszáma.ToString() + ")";
                            MyA.ABMódosítás(helyhiba, jelszó, szöveg);
                            // naplózzuk a hibákat
                            MyA.ABMódosítás(helynapló, jelszó, szöveg);
                        }
                    }
                }
            }
            Holtart.Ki();
        }
        #endregion


        #region CAF ütemezés
        private void CAFÜtemezés()
        {
            DateTime Dátum_ütem = Dátum.Value.AddDays(1);

            string helyütem = $@"{Application.StartupPath}\Főmérnökség\adatok\CAF\CAF.mdb";
            if (!File.Exists(helyütem)) return;
            string jelszóütem = "CzabalayL";

            string szöveg = "SELECT * FROM adatok where STÁTUS=2 and [dátum]=#" + Dátum_ütem.ToString("M-d-yy") + "# order by  azonosító";

            // Módosítjuk a jármű státuszát
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";
            string jelszó = "pozsgaii";
            JárműListaFeltöltés();

            // megnyitjuk a hibákat
            string helyhiba = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\hiba.mdb";
            string jelszóhiba = "pozsgaii";
            szöveg = "SELECT * FROM hibatábla ";
            Kezelő_jármű_hiba KézHiba = new Kezelő_jármű_hiba();
            List<Adat_Jármű_hiba> AdatokHiba = KézHiba.Lista_adatok(helyhiba, jelszóhiba, szöveg);

            // naplózás
            string helynapló = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\hibanapló\{DateTime.Now:yyyyMM}hibanapló.mdb";
            if (!File.Exists(helynapló)) Adatbázis_Létrehozás.Hibatáblalap(helynapló);

            Holtart.Be(100);
            Kezelő_CAF_Adatok kéz = new Kezelő_CAF_Adatok();
            szöveg = $"SELECT * FROM adatok where STÁTUS=2 and [dátum]=#{Dátum_ütem:M-d-yy}# order by  azonosító";
            List<Adat_CAF_Adatok> Adatok = kéz.Lista_Adatok(helyütem, jelszóütem, szöveg);

            foreach (Adat_CAF_Adatok rekordütemez in Adatok)
            {
                // ha van ütemezett kocsi
                Holtart.Lép();

                // megnézzük, hogy a telephelyen van-e a kocsi
                Adat_Jármű ElemJármű = (from a in AdatokJármű
                                        where a.Azonosító == rekordütemez.Azonosító
                                        select a).FirstOrDefault();
                if (ElemJármű != null)
                {
                    // ha telephelyen van a kocsi
                    // hiba leírása
                    string szöveg1 = rekordütemez.Vizsgálat.Trim() + "-" + rekordütemez.Id + "-" + rekordütemez.Dátum.ToString("yyyy.MM.dd");

                    // Megnézzük, hogy volt-e már rögzítve ilyen szöveg
                    bool talált = false;
                    Adat_Jármű_hiba ElemHiba = (from a in AdatokHiba
                                                where a.Azonosító == rekordütemez.Azonosító && a.Hibaleírása.Contains(szöveg1.Trim())
                                                select a).FirstOrDefault();
                    if (ElemHiba != null) talált = true;


                    // ha már volt ilyen szöveg rögzítve a pályaszámhoz akkor nem rögzítjük mégegyszer
                    if (!talált)
                    {
                        // hibák számát emeljük és státus állítjuk ha kell

                        long hibáksorszáma = 0;
                        string típusa = "";
                        long státus = 0;

                        if (ElemJármű != null)
                        {
                            hibáksorszáma = ElemJármű.Hibák;
                            típusa = ElemJármű.Típus;
                            státus = ElemJármű.Státus;
                        }

                        // rögzítjük a villamos.mdb-be
                        szöveg = "UPDATE állománytábla SET ";
                        szöveg += $" hibák={hibáksorszáma + 1}, ";
                        if (státus < 4)
                            szöveg += " státus=3 ";
                        else
                            szöveg += " státus=4 ";
                        szöveg += $" WHERE  [azonosító]='{rekordütemez.Azonosító.Trim()}'";
                        MyA.ABMódosítás(hely, jelszó, szöveg);


                        // beírjuk a hibákat
                        szöveg = "INSERT INTO hibatábla (létrehozta, korlát, hibaleírása, idő, javítva, típus, azonosító, hibáksorszáma ) VALUES (";
                        szöveg += "'" + Program.PostásNév.Trim() + "', 3, ";
                        szöveg += "'" + szöveg1.Trim() + "', ";
                        szöveg += "'" + DateTime.Now.ToString() + "', false, ";
                        szöveg += "'" + típusa.Trim() + "', ";
                        szöveg += "'" + rekordütemez.Azonosító.Trim() + "', " + (hibáksorszáma + 1).ToString() + ")";
                        MyA.ABMódosítás(helyhiba, jelszó, szöveg);
                        // naplózzuk a hibákat
                        MyA.ABMódosítás(helynapló, jelszó, szöveg);

                        // módosítjuk az ütemezett adatokat is
                        szöveg = "UPDATE adatok  SET Státus=4  WHERE id=" + rekordütemez.Id;
                        MyA.ABMódosítás(helyütem, jelszóütem, szöveg);
                    }
                }
            }
            Holtart.Ki();
        }
        #endregion



        #region Jegykezelő      
        private void Jegykezelő_Click(object sender, EventArgs e)
        {
            try
            {
                string fájlexc;

                if (!File.Exists(HelyNap)) throw new HibásBevittAdat("Hiányoznak a főkönyvi adatok!");

                FőkönyvNapListaFeltöltés(HelyNap);
                KiegTakarításListaFeltöltés();
                JárműListaFeltöltés();
                FőVendégListaFeltöltés();

                fájlexc = $"Jegykezelő_ellenőrzés_nyomtatvány_{Dátum.Value:yyyyMMdd}_{DateTime.Now:yyyyMMddHHmmss}";
                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Jegykezelő ellenőrzés nyomtatvány készítés",
                    FileName = fájlexc,
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                Holtart.Be(100);
                timer1.Enabled = true;
                fájlnév_ = fájlexc.Trim();
                Telephely_ = Cmbtelephely.Text.Trim();
                Dátum_ = Dátum.Value;
                DateTime kezdet = DateTime.Now;

                SZál_Jegykezelő(() =>
                { //leállítjuk a számlálót és kikapcsoljuk a holtartot.
                    timer1.Enabled = false;
                    Holtart.Ki();
                    DateTime Vége = DateTime.Now;
                    MessageBox.Show($"A nyomtatvány elkészült ! Elkészítési idő:{Vége - kezdet}", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
                });
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

        private void SZál_Jegykezelő(Action callback)
        {
            Thread proc = new Thread(() =>
            {
                Főkönyv_Jegykezelő nyomtatvány = new Főkönyv_Jegykezelő();
                // elkészítjük a formanyomtatványt változókat nem lehet küldeni definiálni kell egy külső változót.
                nyomtatvány.Jegykezelő(fájlnév_, Telephely_, AdatokJármű, AdatokFőkönyvNap, Dátum_, AdatokTakarításTípus, AdatokFőVendég);
                this.Invoke(callback, new object[] { });
            });
            proc.Start();
        }

        #endregion


        #region Takarítás

        private void SZál_takarítás(Action callback)
        {
            Thread proc = new Thread(() =>
            {
                Főkönyv_Takarítás nyomtatvány = new Főkönyv_Takarítás();
                nyomtatvány.Takarítás_Excel(fájlnév_, Telephely_, Dátum_, napszak_, AdatokTakarításTípus, AdatokJármű, AdatokFőkönyvNap, AdatokFőVendég, AdatokFőkönyvZSER);
                this.Invoke(callback, new object[] { });
            });
            proc.Start();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            string fájlexc;
            if (!File.Exists(HelyNap)) throw new HibásBevittAdat("Hiányoznak a főkönyvi adatok!");

            if (Délelőtt.Checked)
                fájlexc = $"Takarítás_nyomtatvány_nappal_{Dátum.Value:yyyyMMdd}_{DateTime.Now:yyyyMMddHHmmss}";
            else
                fájlexc = $"Takarítás_nyomtatvány_esti_{Dátum.Value:yyyyMMdd}_{DateTime.Now:yyyyMMddHHmmss}";

            KiegTakarításListaFeltöltés();
            JárműListaFeltöltés();
            FőVendégListaFeltöltés();
            FőkönyvNapListaFeltöltés(HelyNap);
            FőkönyZserListaFeltöltés(HelyZser);


            // kimeneti fájl helye és neve
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog
            {
                InitialDirectory = "MyDocuments",
                Title = "Takarítási nyomtatvány készítés",
                FileName = fájlexc,
                Filter = "Excel |*.xlsx"
            };
            // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
            if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                fájlexc = SaveFileDialog1.FileName;
            else
                return;

            Holtart.Be(100);
            timer1.Enabled = true;

            fájlnév_ = fájlexc.Trim();
            Telephely_ = Cmbtelephely.Text.Trim();

            Dátum_ = Dátum.Value;
            napszak_ = Délelőtt.Checked ? "de" : "du";
            DateTime kezdet = DateTime.Now;

            SZál_takarítás(() =>
                { //leállítjuk a számlálót és kikapcsoljuk a holtartot.
                    timer1.Enabled = false;
                    Holtart.Ki();
                    DateTime Vége = DateTime.Now;
                    MessageBox.Show($"A nyomtatvány elkészült ! Elkészítési idő:{Vége - kezdet}", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
                });
        }

        #endregion


        #region NAPI adatok másolás
        Ablak_Főkönyv_Napi_Adatok Új_Ablak_Főkönyv_Napi_Adatok;
        private void Button1_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Főkönyv_Napi_Adatok != null) Új_Ablak_Főkönyv_Napi_Adatok = null;
            Új_Ablak_Főkönyv_Napi_Adatok = new Ablak_Főkönyv_Napi_Adatok(Cmbtelephely.Text.Trim());
            Új_Ablak_Főkönyv_Napi_Adatok.FormClosed += Új_Ablak_Főkönyv_Napi_Adatok_Closed;
            Új_Ablak_Főkönyv_Napi_Adatok.Show();
        }

        private void Új_Ablak_Főkönyv_Napi_Adatok_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Főkönyv_Napi_Adatok = null;
        }






        #endregion


        #region ZSER adatok másolása

        Ablak_Főkönyv_Zser_Másol Új_Ablak_Főkönyv_Zser_Másol;
        private void ZSER_másol_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Főkönyv_Zser_Másol != null) Új_Ablak_Főkönyv_Zser_Másol = null;
            Új_Ablak_Főkönyv_Zser_Másol = new Ablak_Főkönyv_Zser_Másol(Cmbtelephely.Text.Trim());
            Új_Ablak_Főkönyv_Zser_Másol.FormClosed += Új_Ablak_Főkönyv_Zser_Másol_Closed;
            Új_Ablak_Főkönyv_Zser_Másol.Show();
        }

        private void Új_Ablak_Főkönyv_Zser_Másol_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Főkönyv_Zser_Másol = null;
        }

        #endregion


        #region ZSER adatok módosítása

        Ablak_Főkönyv_Napi Új_Ablak_Főkönyv_Napi;
        private void ZSER_módosítás_Click(object sender, EventArgs e)
        {
            try
            {
                if (ZSER_tábla_sor < 0) return;

                if (Új_Ablak_Főkönyv_Napi != null) Új_Ablak_Főkönyv_Napi = null;

                Adat_Főkönyv_ZSER Adat = new Adat_Főkönyv_ZSER(
                                ZSER_tábla.Rows[ZSER_tábla_sor].Cells[0].Value.ToStrTrim(), //viszonylat
                                ZSER_tábla.Rows[ZSER_tábla_sor].Cells[1].Value.ToString(),  //forgalmiszám
                                ZSER_tábla.Rows[ZSER_tábla_sor].Cells[3].Value.ToÉrt_DaTeTime(),   //tervidnulás
                                ZSER_tábla.Rows[ZSER_tábla_sor].Cells[4].Value.ToÉrt_DaTeTime(), //tényindulás
                                ZSER_tábla.Rows[ZSER_tábla_sor].Cells[5].Value.ToÉrt_DaTeTime(),      //tervérkezés
                                ZSER_tábla.Rows[ZSER_tábla_sor].Cells[6].Value.ToÉrt_DaTeTime(), //tényérkezés
                                ZSER_tábla.Rows[ZSER_tábla_sor].Cells[2].Value.ToStrTrim(), //napszak
                                ZSER_tábla.Rows[ZSER_tábla_sor].Cells[7].Value.ToStrTrim(),  //  szerelvénytípus
                                ZSER_tábla.Rows[ZSER_tábla_sor].Cells[8].Value.ToÉrt_Long(), //kocsikszáma
                                ZSER_tábla.Rows[ZSER_tábla_sor].Cells[9].Value.ToStrTrim(),   //megjegyzés
                                ZSER_tábla.Rows[ZSER_tábla_sor].Cells[10].Value.ToStrTrim(),  //kocsi1
                                ZSER_tábla.Rows[ZSER_tábla_sor].Cells[11].Value.ToStrTrim(),  //kocsi2
                                ZSER_tábla.Rows[ZSER_tábla_sor].Cells[12].Value.ToStrTrim(),  //kocsi3
                                ZSER_tábla.Rows[ZSER_tábla_sor].Cells[13].Value.ToStrTrim(),  //kocsi4
                                ZSER_tábla.Rows[ZSER_tábla_sor].Cells[14].Value.ToStrTrim(),  //kocsi5
                                ZSER_tábla.Rows[ZSER_tábla_sor].Cells[15].Value.ToStrTrim(),  //kocsi6
                                ZSER_tábla.Rows[ZSER_tábla_sor].Cells[17].Value.ToStrTrim(),   //ellenőrző
                                ZSER_tábla.Rows[ZSER_tábla_sor].Cells[16].Value.ToStrTrim() //státus
                                );
                Új_Ablak_Főkönyv_Napi = new Ablak_Főkönyv_Napi(Cmbtelephely.Text, Délelőtt.Checked, Dátum.Value, Adat);
                Új_Ablak_Főkönyv_Napi.FormClosed += Új_Ablak_Főkönyv_Napi_Closed;
                Új_Ablak_Főkönyv_Napi.Változás += Szerelvény_Lista_eljárás;
                Új_Ablak_Főkönyv_Napi.Show();

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

        private void Új_Ablak_Főkönyv_Napi_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Főkönyv_Napi = null;
        }

        private void ZSER_tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            ZSER_tábla_sor = e.RowIndex;
        }
        #endregion


        #region Járműreklám lapfül
        private void Reklámot_üzen()
        {
            KiegIgeNemListaFeltöltés();
            Adat_Kiegészítő_Igen_Nem Elem = (from a in AdatokIgenNem
                                             where a.Id == 2
                                             select a).FirstOrDefault();
            if (Elem != null)
            {
                Reklám_Check.Checked = Elem.Válasz;
            }
            else
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő1.mdb";
                string jelszó = "Mocó";
                string szöveg = "INSERT INTO igen_nem  (id, válasz, megjegyzés) VALUES (2, false, 'Reklámos kocsik megfelelő vonalon történő közlekedése')";
                MyA.ABMódosítás(hely, jelszó, szöveg);
                Reklám_Check.Checked = false;
            }
        }

        private void REklám_frissít_Click(object sender, EventArgs e)
        {
            Reklám_eltérés();
        }

        private void Reklám_eltérés()
        {
            try
            {
                bool Volt = false;

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\{Dátum.Value.Year}\nap\{Dátum.Value:yyyyMMdd}";
                if (Délelőtt.Checked)
                    hely += "denap.mdb";
                else
                    hely += "dunap.mdb";

                if (!File.Exists(hely)) throw new HibásBevittAdat("Hiányzonak az adatok a reklámok kiírásához!");

                RichtextBox1.Text = $"{Dátum.Value:yyyy.MM.dd}-n a következő járműveknek az alábbi vonalakon kellett volna futnia reklám miatt:\r\n\r\n";

                FőkönyvNapListaFeltöltés(hely);
                ReklámListaFeltöltés();

                Holtart.Be(100);
                foreach (Adat_Főkönyv_Nap rekord in AdatokFőkönyvNap)
                {
                    Adat_Reklám Elem = (from a in AdatokReklám
                                        where a.Kezdődátum <= Dátum.Value &&
                                        a.Befejeződátum >= Dátum.Value &&
                                        a.Azonosító == rekord.Azonosító
                                        select a).FirstOrDefault(); ;

                    string viszonylatkiadott = rekord.Viszonylat.Trim();
                    if (viszonylatkiadott.Trim() != "-")
                    {
                        if (Elem != null)
                        {
                            string viszonylatelőírt = Elem.Viszonylat.Trim();
                            if (viszonylatelőírt != "*")
                            {
                                // ha nem tartalmazza a reklám viszonylatot akkor kiírja
                                if (!viszonylatelőírt.Contains(viszonylatkiadott))
                                {
                                    string szöveg = $"{rekord.Azonosító.Trim()}-nek a {viszonylatelőírt.Trim()}-on kellett volna közlekednie, helyette a {viszonylatkiadott.Trim()}-ra lett kiadva.\r\n";
                                    RichtextBox1.Text += szöveg;
                                    Volt = true;
                                }
                            }
                        }
                    }
                    Holtart.Lép();
                }


                Holtart.Ki();
                if (!Volt)
                {
                    RichtextBox1.Text = $"{Dátum.Value:yyyy.MM.dd}-n minden olyan jármű amin van reklám a megfelelő vonalra lett kiadva.";
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

        private void Vezénylésbeírás_Click(object sender, EventArgs e)
        {
            Vezénylésbeírás_eljárás();
        }

        private void Vezénylésbeírás_eljárás()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\üzenetek\{DateTime.Today.Year}utasítás.mdb";
                Kezelő_Utasítás KézUtasítás = new Kezelő_Utasítás();

                string txtsorszám = KézUtasítás.Új_utasítás(hely, RichtextBox1.Text.Trim()).ToStrTrim();
                MessageBox.Show($"Az üzenet rögzítése {txtsorszám} szám alatt megtörtént!", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        #region Napi km adatok listája
        private void Km_frissít_Click(object sender, EventArgs e)
        {
            NAPI_km_kiírás();
        }

        private void NAPI_km_kiírás()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{KM_dátum_kezd.Value.Year}\Napi_km_Zser_{KM_dátum_kezd.Value.Year}.mdb";
                if (!System.IO.File.Exists(hely)) return;

                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM tábla";
                List<Adat_Főkönyv_Zser_Km> AdatokFőZserKm = KézFőZserKm.Lista_adatok(hely, jelszó, szöveg);

                List<Adat_Főkönyv_Zser_Km> AdatokFőZserKmSzűrt;
                if (KM_pályaszám.Text.Trim() != "")
                    AdatokFőZserKmSzűrt = (from a in AdatokFőZserKm
                                           where a.Telephely == Cmbtelephely.Text.Trim() &&
                                           a.Dátum >= KM_dátum_kezd.Value &&
                                           a.Dátum <= KM_dátum_végez.Value &&
                                           a.Azonosító == KM_pályaszám.Text.Trim()
                                           orderby a.Dátum, a.Azonosító
                                           select a).ToList();
                else
                    AdatokFőZserKmSzűrt = (from a in AdatokFőZserKm
                                           where a.Telephely == Cmbtelephely.Text.Trim() &&
                                           a.Dátum >= KM_dátum_kezd.Value &&
                                           a.Dátum <= KM_dátum_végez.Value
                                           orderby a.Dátum, a.Azonosító
                                           select a).ToList();


                double napi_km = 0d;
                int napi_nap = 0;
                string előzőpsz = "";
                DateTime előzőnap = new DateTime(1900, 1, 1);

                Km_tábla.Rows.Clear();
                Km_tábla.Columns.Clear();
                Km_tábla.Refresh();
                Km_tábla.Visible = false;
                Km_tábla.ColumnCount = 4;

                // fejléc elkészítése
                Km_tábla.Columns[0].HeaderText = "Pályaszám";
                Km_tábla.Columns[0].Width = 100;
                Km_tábla.Columns[1].HeaderText = "Dátum";
                Km_tábla.Columns[1].Width = 150;
                Km_tábla.Columns[2].HeaderText = "Napi km";
                Km_tábla.Columns[2].Width = 100;
                Km_tábla.Columns[3].HeaderText = "Telephely";
                Km_tábla.Columns[3].Width = 150;


                int i;
                foreach (Adat_Főkönyv_Zser_Km rekord in AdatokFőZserKmSzűrt)
                {
                    Km_tábla.RowCount++;
                    i = Km_tábla.RowCount - 1;
                    Km_tábla.Rows[i].Cells[0].Value = rekord.Azonosító.Trim();
                    Km_tábla.Rows[i].Cells[1].Value = rekord.Dátum.ToString("yyyy.MM.dd");
                    Km_tábla.Rows[i].Cells[2].Value = rekord.Napikm;
                    Km_tábla.Rows[i].Cells[3].Value = rekord.Telephely.Trim();

                    napi_km += rekord.Napikm;

                    if (előzőpsz.Trim() == rekord.Azonosító.Trim() && előzőnap.ToString("yyyy.MM.dd") == rekord.Dátum.ToString("yyyy.MM.dd"))
                    {
                    }
                    else
                    {
                        napi_nap += 1;
                        előzőpsz = rekord.Azonosító.Trim();
                        előzőnap = rekord.Dátum;
                    }
                }

                Km_tábla.RowCount++;
                i = Km_tábla.RowCount - 1;
                Km_tábla.Rows[i].Cells[2].Value = napi_km;
                Km_tábla.Rows[i].Cells[0].Value = "Összesen:";

                Km_tábla.RowCount++;
                i = Km_tábla.RowCount - 1;
                Km_tábla.Rows[i].Cells[0].Value = "Átlag:";
                if (napi_nap != 0)
                    Km_tábla.Rows[i].Cells[2].Value = napi_km / napi_nap;

                Km_tábla.Visible = true;
                Km_tábla.Refresh();
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

        private void Napi_excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Km_tábla.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Napi_futás_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMdd}-{Dátum.Value:yyyyMMddHHmm}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Km_tábla, false);
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


        #region Közös Kereső

        Ablak_Kereső Új_Ablak_Kereső;
        void Kereső_hívás(string honnan)
        {
            try
            {
                Új_Ablak_Kereső?.Close();

                Új_Ablak_Kereső = new Ablak_Kereső();
                Új_Ablak_Kereső.FormClosed += Új_Ablak_Kereső_Closed;
                Új_Ablak_Kereső.Top = 50;
                Új_Ablak_Kereső.Left = 50;
                Új_Ablak_Kereső.Show();
                switch (honnan)
                {
                    case "zser":
                        Új_Ablak_Kereső.Ismétlődő_Változás += Szövegkeresés_ZSER;
                        break;
                    case "zseridő":
                        Új_Ablak_Kereső.Ismétlődő_Változás += Szövegkeresés_ZSER_Idő;
                        break;
                    case "napi":
                        Új_Ablak_Kereső.Ismétlődő_Változás += Szövegkeresés_Napi;
                        break;
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

        private void Szövegkeresés_ZSER()
        {
            if (Új_Ablak_Kereső.Keresendő == null) return;
            if (Új_Ablak_Kereső.Keresendő.Trim() == "") return;
            if (ZSER_tábla.RowCount < 1) return;
            KözösKereső(ZSER_tábla, Új_Ablak_Kereső.Keresendő.Trim());
        }

        private void KözösKereső(DataGridView Táblázat, string Keressük)
        {
            try
            {
                // megkeressük a szöveget a táblázatban
                for (int j = 0; j < Táblázat.ColumnCount; j++)
                {
                    for (int i = 0; i < Táblázat.RowCount; i++)
                    {
                        if (Táblázat.Rows[i].Cells[j].Value != null)
                        {
                            if (Táblázat.Rows[i].Cells[j].Value.ToStrTrim() == Keressük)
                            {
                                Táblázat.Rows[i].Cells[j].Style.BackColor = Color.Orange;

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


        private void Új_Ablak_Kereső_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kereső = null;
        }

        #endregion


        #region Listák feltöltése
        private void SzerelvényListaFeltöltés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\villamos\szerelvény.mdb";
                if (!System.IO.File.Exists(hely)) return;
                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM szerelvénytábla  ORDER BY id";
                AdatokSzerelvény.Clear();
                AdatokSzerelvény = KézSzerelvény.Lista_Adatok(hely, jelszó, szöveg);
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

        private void NapiHibalistaFeltöltés()
        {
            try
            {
                AdatokNapiHiba.Clear();
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\Új_napihiba.mdb";
                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM hiba ORDER BY azonosító";
                AdatokNapiHiba = KézNapiHiba.Lista_adatok(hely, jelszó, szöveg);
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

        private void JárműListaFeltöltés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";
                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM állománytábla ORDER BY azonosító";
                AdatokJármű.Clear();
                AdatokJármű = KézJármű.Lista_Adatok(hely, jelszó, szöveg);
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

        private void JárműFőListaFeltöltés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\villamos.mdb";
                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM állománytábla ORDER BY azonosító";
                AdatokFőJármű.Clear();
                AdatokFőJármű = KézJármű.Lista_Adatok(hely, jelszó, szöveg);
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

        private void KiegIgeNemListaFeltöltés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő1.mdb";
                AdatokIgenNem.Clear();
                AdatokIgenNem = KézKiegIgenNem.Lista_Adatok(hely);
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

        private void FőkönyvNapListaFeltöltés(string hely)
        {
            try
            {
                string jelszó = "lilaakác";
                string szöveg = "SELECT * FROM adattábla  order by azonosító";
                AdatokFőkönyvNap.Clear();
                AdatokFőkönyvNap = KézFőkönyvNap.Lista_adatok(hely, jelszó, szöveg);
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

        private void ReklámListaFeltöltés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\villamos5.mdb";
                string jelszó = "morecs";
                AdatokReklám.Clear();
                string szöveg = $"SELECT * FROM reklámtábla";
                AdatokReklám = KézReklám.Lista_Adatok(hely, jelszó, szöveg);
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

        private void FőkönyZserListaFeltöltés(string hely)
        {
            try
            {
                string jelszó = "lilaakác";
                string szöveg = "SELECT * FROM zseltábla  ORDER BY  viszonylat, forgalmiszám";
                AdatokFőkönyvZSER.Clear();
                AdatokFőkönyvZSER = KézFőkönyvZSER.Lista_adatok(hely, jelszó, szöveg);
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

        private void KiegTakarításListaFeltöltés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő.mdb";
                string jelszó = "Mocó";
                string szöveg = "Select * FROM takarítástípus order by típus";
                AdatokTakarításTípus = KézTakarításTípus.Lista_Adatok(hely, jelszó, szöveg);
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

        private void FőVendégListaFeltöltés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\villamos.mdb";
                string jelszó = "pozsgaii";
                string szöveg = "Select * FROM vendégtábla";
                AdatokFőVendég.Clear();
                AdatokFőVendég = KézFőJárműVendég.Lista_adatok(hely, jelszó, szöveg);
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