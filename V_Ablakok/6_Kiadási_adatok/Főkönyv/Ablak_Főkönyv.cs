using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_Ablakok._6_Kiadási_adatok.Főkönyv;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Ablakok._6_Kiadási_adatok.Főkönyv;
using Villamos.Villamos_Adatszerkezet;
using Villamos.Villamos_Nyomtatványok;
using MyE = Villamos.Module_Excel;
using MyEn = Villamos.V_MindenEgyéb.Enumok;
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
        readonly Kezelő_Utasítás KézUtasítás = new Kezelő_Utasítás();
        readonly Kezelő_Kiegészítő_Idő_Kor KézKor = new Kezelő_Kiegészítő_Idő_Kor();
        readonly Kezelő_CAF_Adatok KézCAF = new Kezelő_CAF_Adatok();
        readonly Kezelő_Főkönyv_SegédTábla KézFőkönyvSegéd = new Kezelő_Főkönyv_SegédTábla();
        readonly Kezelő_Jármű_Állomány_Típus KézJárműTípus = new Kezelő_Jármű_Állomány_Típus();
        readonly Kezelő_Jármű2 KézJármű2 = new Kezelő_Jármű2();
        readonly Kezelő_jármű_hiba KézHiba = new Kezelő_jármű_hiba();
        readonly Kezelő_Vezénylés KézICSVezénylés = new Kezelő_Vezénylés();
        readonly Kezelő_TW6000_Ütemezés KézTW6000 = new Kezelő_TW6000_Ütemezés();
        readonly Kezelő_TW600_ÜtemNapló KézTwNapló = new Kezelő_TW600_ÜtemNapló();
        readonly Kezelő_Forte_Kiadási_Adatok KézForte = new Kezelő_Forte_Kiadási_Adatok();
        readonly Kezelő_Kiegészítő_Idő_Tábla KézKiegIdő = new Kezelő_Kiegészítő_Idő_Tábla();
        readonly Kezelő_Kiegészítő_Forte_Vonal KézForte_Vonal = new Kezelő_Kiegészítő_Forte_Vonal();
        readonly Kezelő_Üzenet KézÜzenet = new Kezelő_Üzenet();


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
            Reklámot_üzen();
        }

        private void Papír()
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
            if (DateTime.Now.Hour > 10)
                Délutáni.Checked = true;
            else
                Délelőtt.Checked = true;

            Eredménytábla();
            Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;
            Óráig.Value = new DateTime(Dátum.Value.Year, Dátum.Value.Month, Dátum.Value.Day, 11, 0, 0);
            AdatokJármű = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
            Jogosultságkiosztás();
            Gombok();
        }

        #region Alap
        private void Timer1_Tick(object sender, EventArgs e)
        {
            Holtart.Lép();
        }

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    Cmbtelephely.Items.Add(Elem);
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
            try
            {
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\Főkönyv.html";
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
                        Status_ComboFeltöltés();
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
            AdatokFőkönyvNap = KézFőkönyvNap.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
            if (AdatokFőkönyvNap == null || AdatokFőkönyvNap.Count == 0)
            {
                Beállólista.Enabled = false;
                Főkönyv.Enabled = false;
                Button5.Enabled = false;
                Jegykezelő.Enabled = false;
            }
            else
            {
                Beállólista.Enabled = true;
                Főkönyv.Enabled = true;
                Button5.Enabled = true;
                Jegykezelő.Enabled = true;
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
            Eredménytábla();
            Gombok();
        }

        private void Délutáni_Click(object sender, EventArgs e)
        {
            Eredménytábla();
            Gombok();
        }

        private void Dátum_ValueChanged(object sender, EventArgs e)
        {
            Eredménytábla();
            Gombok();
            Óráig.Value = new DateTime(Dátum.Value.Year, Dátum.Value.Month, Dátum.Value.Day, 11, 0, 0);
        }

        private void Eredménytábla()
        {
            try
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
                AdatokFőkönyvZSER = KézFőkönyvZSER.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
                if (AdatokFőkönyvZSER == null || AdatokFőkönyvZSER.Count == 0) return;

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

        private void Rosszkiadása()
        {
            AdatokFőkönyvNap = KézFőkönyvNap.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
            if (AdatokFőkönyvNap == null || AdatokFőkönyvNap.Count == 0) return;

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
            AdatokIgenNem = KézKiegIgenNem.Lista_Adatok(Cmbtelephely.Text.Trim());

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
                FileName = $"Háromnapos_nyomtatvány_{Program.PostásNév.Trim()}_{DateTime.Now:yyyyMMddHHmmss}",
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
            Holtart.Be();
            // Minden létező kocsinak a napját 99-re állítjuk
            KézJármű2.Módosítás99(Cmbtelephely.Text.Trim());

            List<Adat_Jármű_2> Adatok2 = KézJármű2.Lista_Adatok(Cmbtelephely.Text.Trim());

            // leellenőrizzük, hogy minden kocsi szerepel
            AdatokJármű = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
            // Amik elmentek törlődnek
            foreach (Adat_Jármű_2 rekord in Adatok2)
            {
                Adat_Jármű Elem = (from a in AdatokJármű
                                   where a.Azonosító == rekord.Azonosító
                                   select a).FirstOrDefault();

                if (Elem == null) KézJármű2.Törlés(Cmbtelephely.Text.Trim(), rekord.Azonosító);
            }

            //Amik jöttek beíródnak
            foreach (Adat_Jármű rekord in AdatokJármű)
            {
                Adat_Jármű_2 Elem = (from a in Adatok2
                                     where a.Azonosító == rekord.Azonosító
                                     select a).FirstOrDefault();
                if (Elem == null)
                {
                    Adat_Jármű_2 ADAT = new Adat_Jármű_2(
                                rekord.Azonosító.Trim(),
                                 new DateTime(1900, 1, 1),
                                99);
                    KézJármű2.Rögzítés(Cmbtelephely.Text.Trim(), ADAT);
                }
            }


            // pályaszám emelkedőbe osztja el a járműveket

            // kiírjuk a T5c5-t 
            AdatokJármű = AdatokJármű.Where(a => a.Valóstípus.Contains("T5C5")).ToList();
            int darabszám = AdatokJármű.Count;

            // elosztjuk darabra
            int vége1 = darabszám / 3;
            int vége2 = vége1 + (darabszám / 3);
            List<Adat_Jármű_2> AdatokGy = new List<Adat_Jármű_2>();
            for (int ii = 0; ii < AdatokJármű.Count; ii++)
            {
                if (ii < vége1)
                {
                    Adat_Jármű_2 ADAT = new Adat_Jármű_2(AdatokJármű[ii].Azonosító.Trim(), 1);
                    AdatokGy.Add(ADAT);
                }
                else if (ii < vége2)
                {
                    Adat_Jármű_2 ADAT = new Adat_Jármű_2(AdatokJármű[ii].Azonosító.Trim(), 2);
                    AdatokGy.Add(ADAT);
                }
                else
                {
                    Adat_Jármű_2 ADAT = new Adat_Jármű_2(AdatokJármű[ii].Azonosító.Trim(), 3);
                    AdatokGy.Add(ADAT);
                }
                Holtart.Lép();
            }
            KézJármű2.Módosítás(Cmbtelephely.Text.Trim(), AdatokGy);
            Holtart.Ki();
        }
        #endregion


        #region Program adatok fordítása
        private void Program_adatok_Click(object sender, EventArgs e)
        {
            try
            {
                Holtart.Be();
                Főkönyv_Funkciók.FőadatEllenőrzése(Cmbtelephely.Text.Trim());
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
                        Vezénylés.T5C5(Cmbtelephely.Text.Trim(), Dátum.Value.AddDays(1));
                        CAFÜtemezés();
                    }
                }

                AdatokFőkönyvNap = KézFőkönyvNap.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
                if (AdatokFőkönyvNap == null || AdatokFőkönyvNap.Count == 0)
                {

                }
                else if (MessageBox.Show("Már van az adott napra feltöltve adat ! Módosítjuk az adatokat ?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    KézFőkönyvNap.Törlés(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
                }
                else
                {
                    NapiTábla_kiírás(0);
                    Holtart.Ki();
                    return;
                }

                // rögzítjük a módosítót
                KézFőkönyvSegéd.Törlés(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
                Adat_Főkönyv_SegédTábla AdatSegéd = new Adat_Főkönyv_SegédTábla(1, Program.PostásNév.Trim());
                KézFőkönyvSegéd.Rögzítés(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du", AdatSegéd);

                NapiAdatokRögzítése();

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

        private void NapiAdatokRögzítése()
        {
            try
            {
                // beolvassuk a villamos adatokat
                Holtart.Be();
                AdatokJármű = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                AdatokNapiHiba = KézNapiHiba.Lista_Adatok(Cmbtelephely.Text.Trim());
                AdatokSzerelvény = KézSzerelvény.Lista_Adatok(Cmbtelephely.Text.Trim());

                List<Adat_Főkönyv_Nap> AdatokGy = new List<Adat_Főkönyv_Nap>();
                foreach (Adat_Jármű rekord in AdatokJármű)
                {
                    Adat_Főkönyv_Nap ADATNAP = new Adat_Főkönyv_Nap(
                                     rekord.Státus,
                                     Hiba_Leírás(rekord.Azonosító.Trim()),
                                     rekord.Típus,
                                     rekord.Azonosító,
                                     rekord.Szerelvénykocsik,
                                     "-", "-",
                                     KocsikSzáma(rekord.Szerelvénykocsik),
                                     new DateTime(1900, 1, 1, 0, 0, 0),
                                     new DateTime(1900, 1, 1, 0, 0, 0),
                                     new DateTime(1900, 1, 1, 0, 0, 0),
                                     new DateTime(1900, 1, 1, 0, 0, 0),
                                     rekord.Miótaáll.ToString() != "" ? rekord.Miótaáll : new DateTime(1900, 1, 1, 0, 0, 0),
                                     "-", "*");
                    AdatokGy.Add(ADATNAP);
                    Holtart.Lép();
                }
                KézFőkönyvNap.Rögzítés(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du", AdatokGy);
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
                List<Adat_Főkönyv_Nap> Adatok = KézFőkönyvNap.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
                //Ha volt megállítani való jármű akkor a napi adatokat frissítjük
                if (Főkönyv_Határérték.T5C5_Túllépés(Adatok, Cmbtelephely.Text.Trim(), "T5C5"))
                {
                    KézFőkönyvNap.Törlés(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
                    NapiAdatokRögzítése();
                    MessageBox.Show("Zser adatok feldolgozása során járművek túlfutottak, ezért megállításra kerül(tek)!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (Főkönyv_Határérték.T5C5_Túllépés(Adatok, Cmbtelephely.Text.Trim(), "SGP"))
                {
                    KézFőkönyvNap.Törlés(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
                    NapiAdatokRögzítése();
                    MessageBox.Show("Zser adatok feldolgozása során járművek túlfutottak, ezért megállításra kerül(tek)!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void ZSER_Beolvasás()
        {
            string fájlexc = "";
            try
            {
                // Idő korrekciók
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

                List<Adat_Főkönyv_ZSER> Adatok = KézFőkönyvZSER.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
                // megnézzük, hogy létezik-e adott napi tábla
                if (Adatok != null && Adatok.Count != 0)
                {
                    // ha létezik akkor töröljük
                    if (MessageBox.Show("Már van az adott napra feltöltve adat ! Módosítjuk az adatokat ?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        KézFőkönyvZSER.Törlés(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
                    else
                        return;
                }
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
                Főkönyv_Funkciók.ZSER_Betöltés(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du", fájlexc, kiadási_korr, érkezési_korr);
                DateTime Vége = DateTime.Now;

                // megnézzük, hogy előző éjszaka volt -e tábla, ha volt akkor hozzá fűzzük a napi adatokhoz.
                List<Adat_Főkönyv_ZSER> AdatokÉ = KézFőkönyvZSER.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.AddDays(-1), "éj");
                if (AdatokÉ != null && AdatokÉ.Count != 0) Előzőnapuéjszakaijárat();
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
            try
            {
                // hozzátesszük az előző éjszakai járatokat az aktuális naphoz.
                List<Adat_Főkönyv_ZSER> AdatokÉ = KézFőkönyvZSER.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.AddDays(-1), "éj");
                List<Adat_Főkönyv_ZSER> AdatokGY = new List<Adat_Főkönyv_ZSER>();
                foreach (Adat_Főkönyv_ZSER rekord in AdatokÉ)
                {
                    Adat_Főkönyv_ZSER ADAT = new Adat_Főkönyv_ZSER(
                                   rekord.Viszonylat.Trim(),
                                   rekord.Forgalmiszám.Trim(),
                                   rekord.Tervindulás,
                                   rekord.Tényindulás,
                                   rekord.Tervérkezés,
                                   rekord.Tényérkezés,
                                   "*",
                                   rekord.Szerelvénytípus.Trim(),
                                   rekord.Kocsikszáma,
                                   rekord.Megjegyzés.Trim(),
                                   rekord.Kocsi1.Trim(),
                                   rekord.Kocsi2.Trim(),
                                   rekord.Kocsi3.Trim(),
                                   rekord.Kocsi4.Trim(),
                                   rekord.Kocsi5.Trim(),
                                   rekord.Kocsi6.Trim(),
                                   rekord.Ellenőrző.Trim(),
                                   rekord.Státus.Trim());
                    AdatokGY.Add(ADAT);
                }
                KézFőkönyvZSER.Rögzítés(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du", AdatokGY);
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


        #region ZSER összevetés
        private void ZSERellenőrzés_Click(object sender, EventArgs e)
        {
            try
            {
                // megnézzük, hogy létezik-e adott napi tábla
                List<Adat_Főkönyv_Nap> Adatok = KézFőkönyvNap.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
                if (Adatok == null || Adatok.Count == 0) throw new HibásBevittAdat("Hiányzonak a napi adatok!");

                // lenullázzuk az előző adatokat
                Holtart.Be();
                List<string> AdatokGy = new List<string>();
                foreach (Adat_Főkönyv_Nap rekord in Adatok)
                {
                    AdatokGy.Add(rekord.Azonosító);
                    Holtart.Lép();
                }
                KézFőkönyvNap.Módosítás(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du", AdatokGy);

                // leellnőrizzük a zser adatokat hogy megvannak-e
                List<Adat_Főkönyv_ZSER> AdatokZser = KézFőkönyvZSER.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");

                if (AdatokZser == null || AdatokZser.Count == 0)
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
                // járműreklámot kiírjuk ha a feltétleknek megfelel
                if (Dátum.Value == DateTime.Today && Reklám_Check.Checked) Reklám_eltérés();
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
            try
            {
                int éjszakavolt = 0;
                DateTime reggeldát = new DateTime(1900, 1, 1, 0, 0, 0);
                DateTime délutándát = new DateTime(1900, 1, 1, 0, 0, 0);
                DateTime estedát = new DateTime(1900, 1, 1, 0, 0, 0);
                string napszak;
                DateTime ideigdátum;

                // megnézzük, hogy az adott nap munkanap, vagy hétvége
                int munkanap = 1;

                List<Adat_Forte_Kiadási_Adatok> AdatokForte = KézForte.Lista_Adatok(Dátum.Value.Year);
                if (AdatokForte != null && AdatokForte.Count > 0)
                {


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
                List<Adat_Kiegészítő_Idő_Tábla> AdatokIdő = KézKiegIdő.Lista_Adatok();
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
                List<Adat_Főkönyv_ZSER> Adatok = KézFőkönyvZSER.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");

                List<Adat_Főkönyv_ZSER> AdatokGy = new List<Adat_Főkönyv_ZSER>();
                foreach (Adat_Főkönyv_ZSER rekord in Adatok)
                {
                    napszak = "*";

                    if (rekord.Tényindulás <= délutándát && rekord.Tényérkezés >= délutándát && !Délelőtt.Checked) napszak = "DU";
                    if (rekord.Tényindulás <= reggeldát && rekord.Tényérkezés >= reggeldát && Délelőtt.Checked) napszak = "DE";
                    if (rekord.Tényindulás >= estedát && napszak.Trim() == "*")
                    {
                        napszak = "X";
                        éjszakavolt = 1;
                    }
                    Adat_Főkönyv_ZSER ADAT = new Adat_Főkönyv_ZSER(napszak.Trim(), rekord.Viszonylat.Trim(), rekord.Forgalmiszám.Trim(), rekord.Tervindulás);
                    AdatokGy.Add(ADAT);
                }

                // azokat a vonalakat amiket nem kell figyelembe venni kicsillagozzuk
                List<string> Tiltott_vonalak = KézForte_Vonal.Lista_Adatok().Select(a => a.ForteVonal).ToList();
                napszak = "*";
                if (Tiltott_vonalak.Count > 0)
                {
                    foreach (Adat_Főkönyv_ZSER rekord in Adatok)
                    {
                        if (Tiltott_vonalak.Contains(rekord.Viszonylat.Trim()))
                        { Adat_Főkönyv_ZSER ADAT = new Adat_Főkönyv_ZSER(napszak.Trim(), rekord.Viszonylat.Trim(), rekord.Forgalmiszám.Trim(), rekord.Tervindulás); }
                    }
                }

                if (AdatokGy.Count > 0) KézFőkönyvZSER.Módosítás_Napszak(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du", AdatokGy);
                if (éjszakavolt == 1) Éjszakaijárat();
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

        private void Éjszakaijárat()
        {
            try
            {
                // osztályozásnál a napi éjszakait X-el jelöltük.

                List<Adat_Főkönyv_ZSER> AdatokÉj = KézFőkönyvZSER.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, "éj");
                if (AdatokÉj.Count > 0) KézFőkönyvZSER.Törlés(Cmbtelephely.Text.Trim(), Dátum.Value, "éj");       //ha van éjszaki akkor először töröljük az adatokat


                // leellnőrizzük a zser adatokat hogy megvannak-e
                List<Adat_Főkönyv_ZSER> Adatok = KézFőkönyvZSER.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
                Adatok = Adatok.Where(a => a.Napszak.Trim() == "X").ToList();

                List<Adat_Főkönyv_ZSER> AdatokGy = new List<Adat_Főkönyv_ZSER>();
                foreach (Adat_Főkönyv_ZSER rekord in Adatok)
                {
                    Adat_Főkönyv_ZSER AdatÉj = new Adat_Főkönyv_ZSER(
                                rekord.Viszonylat,
                                rekord.Forgalmiszám,
                                rekord.Tervindulás,
                                rekord.Tényindulás,
                                rekord.Tervérkezés,
                                rekord.Tényérkezés,
                                "É",
                                rekord.Szerelvénytípus,
                                rekord.Kocsikszáma,
                                rekord.Megjegyzés,
                                rekord.Kocsi1,
                                rekord.Kocsi2,
                                rekord.Kocsi3,
                                rekord.Kocsi4,
                                rekord.Kocsi5,
                                rekord.Kocsi6,
                                rekord.Ellenőrző,
                                rekord.Státus);

                    AdatokGy.Add(AdatÉj);
                }
                if (AdatokGy.Count > 0) KézFőkönyvZSER.Rögzítés(Cmbtelephely.Text.Trim(), Dátum.Value, "éj", AdatokGy);
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

        private void Zser_ellenőrzés()
        {
            try
            {
                Holtart.Be();
                // visszaállítjuk az összes ellenrőzőt alaphelyzetbe
                List<Adat_Főkönyv_ZSER> AdatokZSER = KézFőkönyvZSER.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");

                List<Adat_Főkönyv_ZSER> AdatokGy = new List<Adat_Főkönyv_ZSER>();
                foreach (Adat_Főkönyv_ZSER rekord in AdatokZSER)
                {
                    Adat_Főkönyv_ZSER AdatEll = new Adat_Főkönyv_ZSER(
                                     rekord.Viszonylat.Trim(),
                                     rekord.Forgalmiszám.Trim(),
                                     rekord.Tervindulás,
                                     "_");
                    AdatokGy.Add(AdatEll);
                    Holtart.Lép();
                }
                if (AdatokGy.Count > 0) KézFőkönyvZSER.Módosítás_Ellenőr(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du", AdatokGy);


                // ellenőrizzük, hogy a pályaszámok a telephez tartoznak
                // *******************************
                // feltöltjük a pályaszám listába
                // *******************************

                List<Adat_Főkönyv_Nap> AdatokNap = KézFőkönyvNap.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
                List<string> Pályaszámok = (from a in AdatokNap
                                            orderby a.Azonosító
                                            select a.Azonosító).ToList();

                AdatokZSER = KézFőkönyvZSER.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");

                Holtart.Lép();
                AdatokGy.Clear();
                foreach (Adat_Főkönyv_ZSER rekord in AdatokZSER)
                {
                    string eredmény = "_";
                    if (rekord.Napszak.Trim() == "DE" || rekord.Napszak.Trim() == "DU")
                    {
                        eredmény += Pályaszám_vizsgálat(Pályaszámok, rekord.Kocsi1.Trim()) +
                                    Pályaszám_vizsgálat(Pályaszámok, rekord.Kocsi2.Trim()) +
                                    Pályaszám_vizsgálat(Pályaszámok, rekord.Kocsi3.Trim()) +
                                    Pályaszám_vizsgálat(Pályaszámok, rekord.Kocsi4.Trim()) +
                                    Pályaszám_vizsgálat(Pályaszámok, rekord.Kocsi5.Trim()) +
                                    Pályaszám_vizsgálat(Pályaszámok, rekord.Kocsi6.Trim());

                        // módosítjuk az adatokat
                        Adat_Főkönyv_ZSER AdatEll = new Adat_Főkönyv_ZSER(
                                  rekord.Viszonylat.Trim(),
                                  rekord.Forgalmiszám.Trim(),
                                  rekord.Tervindulás,
                                  eredmény);
                        AdatokGy.Add(AdatEll);
                    }
                    Holtart.Lép();
                }
                if (AdatokGy.Count > 0) KézFőkönyvZSER.Módosítás_Ellenőr(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du", AdatokGy);
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

        string Pályaszám_vizsgálat(List<string> Pályaszámok, string pályaszám)
        {
            string válasz = "0";
            if (pályaszám.Trim() == "_") return válasz;         //ha nincs beosztva kocsi
            if (Pályaszámok.Contains(pályaszám)) return válasz; //Ha a telephelyen van a kocsi akkor jó
            válasz = "1";                                       // Nincs a telephelyen a kocsi ilyenkor nem jó
            return válasz;
        }

        private void ZSER_szerelvény_ellenőrzés()
        {

            AdatokFőkönyvNap = KézFőkönyvNap.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
            if (AdatokFőkönyvNap == null || AdatokFőkönyvNap.Count == 0) return;
            AdatokFőkönyvZSER = KézFőkönyvZSER.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
            if (AdatokFőkönyvZSER == null || AdatokFőkönyvZSER.Count == 0) return;

            Holtart.Be(100);
            // leellenőizzük, hogy azonos szerelvényben futnak
            List<Adat_Főkönyv_ZSER> AdatokGy = new List<Adat_Főkönyv_ZSER>();
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

                    Adat_Főkönyv_ZSER ADAT = new Adat_Főkönyv_ZSER(
                        rekord.Viszonylat.Trim(),
                        rekord.Forgalmiszám.Trim(),
                        rekord.Tervindulás,
                        ideig.Trim());
                    AdatokGy.Add(ADAT);

                }
                Holtart.Lép();
            }

            if (AdatokGy.Count > 0) KézFőkönyvZSER.Módosítás_Ellenőr(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du", AdatokGy);
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
            Holtart.Be();
            // visszaállítjuk az összes ellenrőzőt alaphelyzetbe
            List<Adat_Főkönyv_ZSER> Adatok = KézFőkönyvZSER.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du").OrderBy(a => a.Tervindulás).ToList();

            foreach (Adat_Főkönyv_ZSER rekordzser in Adatok)
            {
                if (rekordzser.Kocsi1.Trim() != "_") Napi_vizsgál_rögzítés(rekordzser, rekordzser.Kocsi1);
                if (rekordzser.Kocsi2.Trim() != "_") Napi_vizsgál_rögzítés(rekordzser, rekordzser.Kocsi2);
                if (rekordzser.Kocsi3.Trim() != "_") Napi_vizsgál_rögzítés(rekordzser, rekordzser.Kocsi3);
                if (rekordzser.Kocsi4.Trim() != "_") Napi_vizsgál_rögzítés(rekordzser, rekordzser.Kocsi4);
                if (rekordzser.Kocsi5.Trim() != "_") Napi_vizsgál_rögzítés(rekordzser, rekordzser.Kocsi5);
                if (rekordzser.Kocsi6.Trim() != "_") Napi_vizsgál_rögzítés(rekordzser, rekordzser.Kocsi6);
                Holtart.Lép();
            }

            // azon kocsikat is átírja amelyek voltak forgalomban az nap
            List<Adat_Főkönyv_Nap> AdatokNapi = KézFőkönyvNap.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
            AdatokNapi = (from a in AdatokNapi
                          where a.Tervindulás < new DateTime(2000, 1, 1)
                          orderby a.Azonosító
                          select a).ToList();

            List<Adat_Főkönyv_Nap> AdatokGy = new List<Adat_Főkönyv_Nap>();
            foreach (Adat_Főkönyv_Nap rekord in AdatokNapi)
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
                    Adat_Főkönyv_Nap ADAT = new Adat_Főkönyv_Nap(
                       ElemZSer.Viszonylat.Trim(),
                       ElemZSer.Forgalmiszám.Trim(),
                       ElemZSer.Kocsikszáma,
                       ElemZSer.Tervindulás,
                       ElemZSer.Tényindulás,
                       ElemZSer.Tervérkezés,
                       ElemZSer.Tényérkezés,
                       rekord.Azonosító);
                    // rögzítjük az adatokat

                    AdatokGy.Add(ADAT);
                }
                Holtart.Lép();
            }
            if (AdatokGy.Count > 0) KézFőkönyvNap.Módosítás_Áttölt(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du", AdatokGy);
        }

        private void Rögzít_elemet_napi(Adat_Főkönyv_Nap rekordzser, string Azonosító)
        {
            Adat_Főkönyv_Nap ADAT = new Adat_Főkönyv_Nap(
                rekordzser.Viszonylat.Trim(),
                rekordzser.Forgalmiszám.Trim(),
                rekordzser.Kocsikszáma,
                rekordzser.Tervindulás,
                rekordzser.Tényindulás,
                rekordzser.Tervérkezés,
                rekordzser.Tényérkezés,
                rekordzser.Napszak.Trim(),
                rekordzser.Megjegyzés.Trim(),
                Azonosító);
            KézFőkönyvNap.Módosítás_Napi(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du", ADAT);
        }

        private void Napi_vizsgál_rögzítés(Adat_Főkönyv_ZSER rekordzser, string Azonosító)
        {
            AdatokFőkönyvNap = KézFőkönyvNap.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
            if (AdatokFőkönyvNap == null || AdatokFőkönyvNap.Count == 0) return;
            Adat_Főkönyv_Nap Elem = (from a in AdatokFőkönyvNap
                                     where a.Azonosító == Azonosító.Trim()
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
                    Rögzít_elemet_napi(Elemek, Azonosító.Trim());
                }

                if (Délutáni.Checked == true &&
                        (rekordzser.Napszak.Trim() == "E" || rekordzser.Napszak.Trim() == "D" || rekordzser.Napszak.Trim() == "DU" ||
                        rekordzser.Napszak.Trim() == "ECD" || rekordzser.Napszak.Trim() == "ECR" || rekordzser.Napszak.Trim() == "DCD"))
                {
                    Adat_Főkönyv_Nap Elemek = new Adat_Főkönyv_Nap(rekordzser.Viszonylat.Trim(), rekordzser.Forgalmiszám.Trim(),
                        rekordzser.Kocsikszáma, rekordzser.Tervindulás, rekordzser.Tényindulás, rekordzser.Tervérkezés, rekordzser.Tényérkezés,
                        rekordzser.Napszak.Trim(), rekordzser.Megjegyzés.Trim());
                    Rögzít_elemet_napi(Elemek, Azonosító.Trim());
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
                List<Adat_Főkönyv_ZSER> Adatok = KézFőkönyvZSER.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
                if (Adatok == null || Adatok.Count == 0) return;

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
                ZSER_tábla.CleanFilterAndSort();
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
                if (ZSER_tábla.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"ZSER_{Program.PostásNév}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyE.DataGridViewToExcel(fájlexc, ZSER_tábla);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                List<Adat_Főkönyv_ZSER> Adatok = KézFőkönyvZSER.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
                if (Adatok == null || Adatok.Count == 0) return;

                DateTime Mikor = new DateTime(Idődátum.Value.Year, Idődátum.Value.Month, Idődátum.Value.Day, Időidő.Value.Hour, Időidő.Value.Minute, Időidő.Value.Second);

                Adatok = (from a in Adatok
                          where a.Tényindulás <= Mikor
                          && a.Tényérkezés > Mikor
                          orderby a.Szerelvénytípus, a.Kocsi1
                          select a).ToList();

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

                ZSER_tábla_idő.CleanFilterAndSort();
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
                NapiTábla.Visible = false;
                List<Adat_Főkönyv_Nap> AdatokÖ = KézFőkönyvNap.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
                if (AdatokÖ.Count < 1) return;

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
                NapiTábla.CleanFilterAndSort();
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
            if (e.RowIndex < 0) return;
            R_azonosító.Text = NapiTábla.Rows[e.RowIndex].Cells[3].Value.ToString();
            // átmegyünk a módosítási lapra
            Fülek.SelectedIndex = 4;
            R_frissít_eljárás();
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
                    FileName = $"Napi_részletes_{Program.PostásNév}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                Module_Excel.DataGridViewToExcel(fájlexc, NapiTábla);
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
                AdatokFőJármű = KézJármű.Lista_Adatok("Főmérnökség");

                Adat_Jármű Elem = (from a in AdatokFőJármű
                                   where a.Azonosító == Járműpanel_Text.Text.Trim()
                                   select a).FirstOrDefault() ?? throw new HibásBevittAdat("A Főmérnökségi adatokban nincs ilyen kocsi.");


                // megnézzük, hogy a napi adatok léteznek-e
                AdatokFőkönyvNap = KézFőkönyvNap.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
                if (AdatokFőkönyvNap == null || AdatokFőkönyvNap.Count == 0) return;

                Adat_Főkönyv_Nap ElemNap = (from a in AdatokFőkönyvNap
                                            where a.Azonosító == Járműpanel_Text.Text.Trim()
                                            select a).FirstOrDefault();


                if (ElemNap != null) throw new HibásBevittAdat("Az adott napi adatokban létezik már a kocsi.");
                // rögzítjük a pályaszámot üres adatokkal.
                Adat_Főkönyv_Nap ADAT = new Adat_Főkönyv_Nap(
                         4,
                         "-",
                         "-",
                         Járműpanel_Text.Text.Trim(),
                         0,
                         "-",
                         "-",
                         0,
                         new DateTime(1900, 1, 1, 0, 0, 0),
                         new DateTime(1900, 1, 1, 0, 0, 0),
                         new DateTime(1900, 1, 1, 0, 0, 0),
                         new DateTime(1900, 1, 1, 0, 0, 0),
                         new DateTime(1900, 1, 1),
                         "*",
                         "-");
                KézFőkönyvNap.Rögzítés(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du", ADAT);
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

        private void Status_ComboFeltöltés()
        {
            R_Státus.Items.Clear();
            foreach (MyEn.Jármű_Státus elem in Enum.GetValues(typeof(MyEn.Jármű_Státus)))
                R_Státus.Items.Add($"{(int)elem} - {elem.ToString().Replace('_', ' ')}");
        }

        private void Cmbtelephely_SelectedIndexChanged(object sender, EventArgs e)
        {
            R_Típus_feltöltés();
            AdatokJármű = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
        }

        private void R_Típus_feltöltés()
        {
            try
            {
                R_típus.Items.Clear();

                List<Adat_Jármű_Állomány_Típus> Adatok = KézJárműTípus.Lista_Adatok(Cmbtelephely.Text.Trim());
                foreach (Adat_Jármű_Állomány_Típus elem in Adatok)
                    R_típus.Items.Add(elem.Típus);

                R_típus.Refresh();
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
            R_tervindulás.Value = new DateTime(1900, 1, 1, 0, 0, 0);
            R_tényindulás.Value = new DateTime(1900, 1, 1, 0, 0, 0);
            R_tervérkezés.Value = new DateTime(1900, 1, 1, 0, 0, 0);
            R_tényérkezés.Value = new DateTime(1900, 1, 1, 0, 0, 0);
            R_miótaáll.Value = new DateTime(1900, 1, 1);
            R_napszak.Text = "";
            R_megjegyzés.Text = "";
        }

        private void R_frissít_Click(object sender, EventArgs e)
        {
            R_frissít_eljárás();
        }

        private void R_frissít_eljárás()
        {
            try
            {
                AdatokFőkönyvNap = KézFőkönyvNap.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");

                Adat_Főkönyv_Nap rekord = AdatokFőkönyvNap.Where(a => a.Azonosító == R_azonosító.Text.Trim()).FirstOrDefault();

                if (rekord != null)
                {
                    R_Státus.Text = $"{rekord.Státus} - {(MyEn.Jármű_Státus)rekord.Státus}";
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
                if (!long.TryParse(MyF.Szöveg_Tisztítás(R_Státus.Text.Trim(), 0, 1), out long Státus)) Státus = 0;
                if (!long.TryParse(R_szerelvény.Text, out long Szerelvény)) R_szerelvény.Text = Szerelvény.ToString();
                if (R_szerelvény.Text.Trim() == "") R_szerelvény.Text = "0";
                if (R_viszonylat.Text.Trim() == "") R_viszonylat.Text = "-";
                if (R_forgalmiszám.Text.Trim() == "") R_forgalmiszám.Text = "-";
                if (!long.TryParse(R_kocsikszáma.Text, out long Kocsikszáma)) R_kocsikszáma.Text = Kocsikszáma.ToString();
                if (R_kocsikszáma.Text.Trim() == "") R_kocsikszáma.Text = "0";
                if (AdatokFőkönyvNap == null || AdatokFőkönyvNap.Count == 0) return;

                AdatokFőkönyvNap = KézFőkönyvNap.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
                Adat_Főkönyv_Nap Elem = (from a in AdatokFőkönyvNap
                                         where a.Azonosító == R_azonosító.Text.Trim()
                                         select a).FirstOrDefault();
                if (Elem != null)
                {
                    Adat_Főkönyv_Nap ADAT = new Adat_Főkönyv_Nap(
                                      Státus,
                                      MyF.Szöveg_Tisztítás(R_hibaleírása.Text.Trim()),
                                      R_típus.Text.Trim(),
                                      R_azonosító.Text.Trim(),
                                      Szerelvény,
                                      R_viszonylat.Text.Trim(),
                                      R_forgalmiszám.Text.Trim(),
                                      Kocsikszáma,
                                      R_tervindulás.Value,
                                      R_tényindulás.Value,
                                      R_tervérkezés.Value,
                                      R_tényérkezés.Value,
                                      R_miótaáll.Value,
                                      R_napszak.Text.Trim(),
                                      R_megjegyzés.Text.Trim());
                    KézFőkönyvNap.Módosítás(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du", ADAT);
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


                AdatokFőkönyvNap = KézFőkönyvNap.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
                if (AdatokFőkönyvNap == null || AdatokFőkönyvNap.Count == 0) return;

                Adat_Főkönyv_Nap Elem = (from a in AdatokFőkönyvNap
                                         where a.Azonosító == R_azonosító.Text.Trim()
                                         select a).FirstOrDefault();

                if (Elem != null)
                {
                    KézFőkönyvNap.Törlés(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du", R_azonosító.Text.Trim());
                    MessageBox.Show("A kocsi törlésre került az adott napi és napszaki adatokból", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
                AdatokFőkönyvNap = KézFőkönyvNap.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
                if (AdatokFőkönyvNap == null || AdatokFőkönyvNap.Count == 0) return;

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
            List<Adat_Főkönyv_Nap> AdatokÖ = KézFőkönyvNap.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
            if (AdatokÖ.Count < 1) return;

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
            Kezelő_Kiegészítő_Mentésihelyek KézMentés = new Kezelő_Kiegészítő_Mentésihelyek();
            List<Adat_Kiegészítő_Mentésihelyek> AdatokMentés = KézMentés.Lista_Adatok(Cmbtelephely.Text.Trim());
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
                KézMentés.Rögzítés(Cmbtelephely.Text.Trim(), ADAT);
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
                    FileName = $"Beálló_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
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
            try
            {
                DateTime Dátum_ütem = Dátum.Value.AddDays(1);
                AdatokJármű = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                List<Adat_Jármű_hiba> AdatokHiba = KézHiba.Lista_Adatok(Cmbtelephely.Text.Trim());

                List<Adat_TW6000_Ütemezés> Adatok = KézTW6000.Lista_Adatok();
                Adatok = (from a in Adatok
                          where a.Vütemezés == Dátum_ütem
                          && a.Státus == 2
                          orderby a.Azonosító
                          select a).ToList();
                Holtart.Be();

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
                        string szöveg1 = rekordütemez.Vizsgfoka.Trim() + "-" + rekordütemez.Vsorszám + "-" + rekordütemez.Vütemezés.ToString("yyyy.MM.dd");

                        // Megnézzük, hogy volt-e már rögzítve ilyen szöveg
                        Adat_Jármű_hiba ElemHiba = (from a in AdatokHiba
                                                    where a.Azonosító == rekordütemez.Azonosító && a.Hibaleírása.Contains(szöveg1.Trim())
                                                    select a).FirstOrDefault();
                        // ha már volt ilyen szöveg rögzítve a pályaszámhoz akkor nem rögzítjük mégegyszer
                        if (ElemHiba == null)
                        {
                            // hibák számát emeljük és státus állítjuk ha kell

                            long hibáksorszáma = 0;
                            string típusa = "";
                            long státus = 0;
                            bool újstátus = false;

                            if (ElemJármű != null)
                            {
                                hibáksorszáma = ElemJármű.Hibák;
                                típusa = ElemJármű.Típus;
                                státus = ElemJármű.Státus;
                            }
                            // hibák számát emeljük és státus állítjuk ha kell
                            if (státus != 4) // ha 4 státusa akkor nem kell módosítani.
                            {
                                újstátus = true;
                                // ha a következő napra ütemez
                                if (DateTime.Today.AddDays(1).ToString("yyyy.MM.dd") == Dátum_ütem.ToString("yyyy.MM.dd"))
                                {
                                    if (rekordütemez.Státus == 4)
                                        státus = 4;
                                    else
                                        státus = 3;
                                }
                                else if (státus < 4)
                                    státus = 3;
                            }

                            // rögzítjük a villamos.mdb-be
                            if (újstátus && státus == 4)
                            {
                                Adat_Jármű AdatJármű = new Adat_Jármű(
                                       rekordütemez.Azonosító.Trim(),
                                       hibáksorszáma + 1,
                                       státus,
                                       DateTime.Now);
                                KézJármű.Módosítás_Státus_Hiba_Dátum(Cmbtelephely.Text.Trim(), AdatJármű);
                            }
                            else
                            {
                                Adat_Jármű AdatJármű = new Adat_Jármű(
                                       rekordütemez.Azonosító.Trim(),
                                       hibáksorszáma + 1,
                                       státus);
                                KézJármű.Módosítás_Hiba_Státus(Cmbtelephely.Text.Trim(), AdatJármű);
                            }

                            // beírjuk a hibákat
                            Adat_Jármű_hiba AdatHiba = new Adat_Jármű_hiba(
                                        Program.PostásNév.Trim(),
                                        státus,
                                        MyF.Szöveg_Tisztítás(szöveg1),
                                        DateTime.Now,
                                        false,
                                        típusa,
                                        rekordütemez.Azonosító,
                                        0);
                            KézHiba.Rögzítés(Cmbtelephely.Text.Trim(), AdatHiba);


                            // módosítjuk az ütemezett adatokat is

                            Adat_TW6000_Ütemezés AdatTW = new Adat_TW6000_Ütemezés(
                                       rekordütemez.Azonosító.Trim(),
                                       $"Előjegyezve: {Program.PostásTelephely.Trim()}",
                                       4,
                                       Dátum.Value.AddDays(1));
                            KézTW6000.Módosítás_ütem(AdatTW, 2);

                            // naplózzuk a TW6000-be is
                            Adat_TW6000_ÜtemNapló ADATNApló = new Adat_TW6000_ÜtemNapló(
                                       rekordütemez.Azonosító,
                                       rekordütemez.Ciklusrend,
                                       rekordütemez.Elkészült,
                                       rekordütemez.Megjegyzés,
                                       DateTime.Now,
                                       Program.PostásNév,
                                       rekordütemez.Státus,
                                       rekordütemez.Velkészülés,
                                       rekordütemez.Vesedékesség,
                                       rekordütemez.Vizsgfoka,
                                       rekordütemez.Vsorszám,
                                       rekordütemez.Vütemezés,
                                       rekordütemez.Vvégezte);
                            KézTwNapló.Rögzítés(DateTime.Now.Year, ADATNApló);
                        }
                    }
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
        #endregion


        #region ICS ütemezés

        private void ICSÜtemezés()
        {

            DateTime Dátum_ütem = Dátum.Value.AddDays(1);

            AdatokJármű = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());

            List<Adat_Jármű_hiba> AdatokHiba = KézHiba.Lista_Adatok(Cmbtelephely.Text.Trim());

            List<Adat_Vezénylés> Adatok = KézICSVezénylés.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum_ütem);
            Adatok = (from a in Adatok
                      where a.Törlés == 0
                      && a.Dátum == Dátum_ütem
                      && a.Típus == "ICS"
                      orderby a.Azonosító
                      select a).ToList();

            Holtart.Be();
            // ha van ütemezett kocsi
            foreach (Adat_Vezénylés rekordütemez in Adatok)
            {
                Holtart.Lép();
                if (rekordütemez.Vizsgálatraütemez == 1)
                {
                    // hiba leírása
                    string szöveg1 = rekordütemez.Vizsgálat.Trim() + "-" + rekordütemez.Vizsgálatszám;
                    string szöveg3 = szöveg1;

                    if (rekordütemez.Státus == 4)
                        szöveg1 += "-" + rekordütemez.Dátum.ToString("yyyy.MM.dd.") + " Maradjon benn ";
                    else
                        szöveg1 += "-" + rekordütemez.Dátum.ToString("yyyy.MM.dd.") + " Beálló ";


                    // Megnézzük, hogy volt-e már rögzítve ilyen szöveg

                    Adat_Jármű_hiba ElemHiba = (from a in AdatokHiba
                                                where a.Azonosító == rekordütemez.Azonosító && a.Hibaleírása.Contains(szöveg3.Trim())
                                                select a).FirstOrDefault();

                    // ha már volt ilyen szöveg rögzítve a pályaszámhoz akkor nem rögzítjük mégegyszer
                    if (ElemHiba == null)
                    {
                        // hibák számát emeljük és státus állítjuk ha kell
                        Adat_Jármű ElemJármű = (from a in AdatokJármű
                                                where a.Azonosító == rekordütemez.Azonosító.Trim()
                                                select a).FirstOrDefault();
                        long hibáksorszáma = 0;
                        string típusa = "";
                        long státus = 0;
                        bool újstátus = false;
                        if (ElemJármű != null)
                        {
                            hibáksorszáma = ElemJármű.Hibák;
                            típusa = ElemJármű.Típus;
                            státus = ElemJármű.Státus;
                        }


                        if (státus != 4) // ha 4 státusa akkor nem kell módosítani.
                        {
                            újstátus = true;
                            // ha a következő napra ütemez
                            if (DateTime.Today.AddDays(1).ToString("yyyy.MM.dd") == Dátum_ütem.ToString("yyyy.MM.dd"))
                            {
                                if (rekordütemez.Státus == 4)
                                    státus = 4;
                                else
                                    státus = 3;

                            }
                            else if (státus < 4)
                                státus = 3;
                        }

                        // rögzítjük a villamos.mdb-be
                        if (újstátus && státus == 4)
                        {
                            Adat_Jármű AdatJármű = new Adat_Jármű(
                                   rekordütemez.Azonosító.Trim(),
                                   hibáksorszáma + 1,
                                   státus,
                                   DateTime.Now);
                            KézJármű.Módosítás_Státus_Hiba_Dátum(Cmbtelephely.Text.Trim(), AdatJármű);
                        }
                        else
                        {
                            Adat_Jármű AdatJármű = new Adat_Jármű(
                                   rekordütemez.Azonosító.Trim(),
                                   hibáksorszáma + 1,
                                   státus);
                            KézJármű.Módosítás_Hiba_Státus(Cmbtelephely.Text.Trim(), AdatJármű);
                        }


                        // beírjuk a hibákat
                        Adat_Jármű_hiba AdatHiba = new Adat_Jármű_hiba(
                                    Program.PostásNév.Trim(),
                                    státus,
                                    MyF.Szöveg_Tisztítás(szöveg1),
                                    DateTime.Now,
                                    false,
                                    típusa,
                                    rekordütemez.Azonosító,
                                    0);
                        KézHiba.Rögzítés(Cmbtelephely.Text.Trim(), AdatHiba);
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
            AdatokJármű = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
            List<Adat_Jármű_hiba> AdatokHiba = KézHiba.Lista_Adatok(Cmbtelephely.Text.Trim());

            Holtart.Be();


            List<Adat_CAF_Adatok> Adatok = KézCAF.Lista_Adatok();
            Adatok = (from a in Adatok
                      where a.Státus == 2
                      && a.Dátum.ToShortDateString() == Dátum_ütem.ToShortDateString()
                      orderby a.Azonosító
                      select a).ToList();

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
                    Adat_Jármű_hiba ElemHiba = (from a in AdatokHiba
                                                where a.Azonosító == rekordütemez.Azonosító
                                                && a.Hibaleírása.Contains(szöveg1.Trim())
                                                select a).FirstOrDefault();

                    // ha már volt ilyen szöveg rögzítve a pályaszámhoz akkor nem rögzítjük mégegyszer
                    if (ElemHiba == null)
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
                        if (státus < 4)
                        {
                            státus = 3;
                            Adat_Jármű AdatJármű = new Adat_Jármű(
                                  rekordütemez.Azonosító.Trim(),
                                  hibáksorszáma + 1,
                                  státus);
                            KézJármű.Módosítás_Hiba_Státus(Cmbtelephely.Text.Trim(), AdatJármű);
                        }
                        else
                        {
                            státus = 4;
                            Adat_Jármű AdatJármű = new Adat_Jármű(
                                 rekordütemez.Azonosító.Trim(),
                                 hibáksorszáma + 1,
                                 státus,
                                 DateTime.Now);
                            KézJármű.Módosítás_Státus_Hiba_Dátum(Cmbtelephely.Text.Trim(), AdatJármű);
                        }

                        // beírjuk a hibákat
                        Adat_Jármű_hiba AdatHiba = new Adat_Jármű_hiba(
                                      Program.PostásNév.Trim(),
                                      státus,
                                      MyF.Szöveg_Tisztítás(szöveg1),
                                      DateTime.Now,
                                      false,
                                      típusa,
                                      rekordütemez.Azonosító,
                                      0);
                        KézHiba.Rögzítés(Cmbtelephely.Text.Trim(), AdatHiba);

                        // módosítjuk az ütemezett adatokat is
                        KézCAF.Módosítás_Státus(rekordütemez.Id, 4);
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

                AdatokFőkönyvNap = KézFőkönyvNap.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
                if (AdatokFőkönyvNap == null || AdatokFőkönyvNap.Count == 0) throw new HibásBevittAdat("Hiányoznak a főkönyvi adatok!");
                AdatokTakarításTípus = KézTakarításTípus.Lista_Adatok(Cmbtelephely.Text.Trim());
                AdatokJármű = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                AdatokFőVendég = KézFőJárműVendég.Lista_Adatok();

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
            AdatokFőkönyvNap = KézFőkönyvNap.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
            if (AdatokFőkönyvNap == null || AdatokFőkönyvNap.Count == 0) throw new HibásBevittAdat("Hiányoznak a főkönyvi adatok!");

            if (Délelőtt.Checked)
                fájlexc = $"Takarítás_nyomtatvány_nappal_{Dátum.Value:yyyyMMdd}_{DateTime.Now:yyyyMMddHHmmss}";
            else
                fájlexc = $"Takarítás_nyomtatvány_esti_{Dátum.Value:yyyyMMdd}_{DateTime.Now:yyyyMMddHHmmss}";

            AdatokTakarításTípus = KézTakarításTípus.Lista_Adatok(Cmbtelephely.Text.Trim());
            AdatokJármű = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
            AdatokFőVendég = KézFőJárműVendég.Lista_Adatok();

            AdatokFőkönyvZSER = KézFőkönyvZSER.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
            if (AdatokFőkönyvZSER == null || AdatokFőkönyvZSER.Count == 0) return;


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
            AdatokIgenNem = KézKiegIgenNem.Lista_Adatok(Cmbtelephely.Text.Trim());
            Adat_Kiegészítő_Igen_Nem Elem = (from a in AdatokIgenNem
                                             where a.Id == 2
                                             select a).FirstOrDefault();
            if (Elem != null)
            {
                Reklám_Check.Checked = Elem.Válasz;
            }
            else
            {
                Reklám_Check.Checked = false;
                Adat_Kiegészítő_Igen_Nem ADAT = new Adat_Kiegészítő_Igen_Nem(2,
                                                                             false,
                                                                             "Reklámos kocsik megfelelő vonalon történő közlekedése");
                KézKiegIgenNem.Módosítás(Cmbtelephely.Text.Trim(), ADAT);
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
                AdatokFőkönyvNap = KézFőkönyvNap.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, Délelőtt.Checked ? "de" : "du");
                if (AdatokFőkönyvNap == null || AdatokFőkönyvNap.Count == 0) throw new HibásBevittAdat("Hiányzonak az adatok a reklámok kiírásához!");

                RichtextBox1.Text = $"{Dátum.Value:yyyy.MM.dd}-n a következő járműveknek az alábbi vonalakon kellett volna futnia reklám miatt:\r\n\r\n";
                AdatokReklám = KézReklám.Lista_Adatok();

                Holtart.Be();
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
                else
                {
                    Adat_Üzenet ADAT = new Adat_Üzenet(
                        0,
                        RichtextBox1.Text.Trim(),
                        "Program",
                        DateTime.Now,
                        0);
                    KézÜzenet.Rögzítés(Cmbtelephely.Text.Trim(), DateTime.Now.Year, ADAT);
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
                string txtsorszám = KézUtasítás.Új_utasítás(Cmbtelephely.Text.Trim(), DateTime.Now.Year, RichtextBox1.Text.Trim()).ToStrTrim();
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
                List<Adat_Főkönyv_Zser_Km> AdatokFőZserKm = KézFőZserKm.Lista_adatok(KM_dátum_kezd.Value.Year);

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

                MyE.DataGridViewToExcel(fájlexc, Km_tábla);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        #region Közös Kereső
        Ablak_Kereső Új_Ablak_Kereső;
        private void Kereső_hívás(string honnan)
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
    }
}