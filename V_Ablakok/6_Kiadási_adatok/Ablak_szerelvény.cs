using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos.Ablakok
{
    public partial class Ablak_szerelvény : Form
    {
        Adat_Szerelvény szAdat = null;
        Adat_Szerelvény ESZAdat = null;
        Ablak_Kereső Új_Ablak_Kereső;

        string Melyik_azonosító = "";
        bool Osztás = false;

        //Új felépítés
        readonly Kezelő_Szerelvény KézSzerelvény = new Kezelő_Szerelvény();
        readonly Kezelő_Szerelvény_Napló KézSzerNapló = new Kezelő_Szerelvény_Napló();
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly Kezelő_Jármű2 KézJármű2 = new Kezelő_Jármű2();
        readonly Kezelő_Kiegészítő_Igen_Nem KézIgenNem = new Kezelő_Kiegészítő_Igen_Nem();
        readonly Kezelő_Utasítás KézUtasítás = new Kezelő_Utasítás();
        readonly Kezelő_Jármű_Állomány_Típus KézTípus = new Kezelő_Jármű_Állomány_Típus();


        List<Adat_Jármű_2> AdatokJ2 = new List<Adat_Jármű_2>();
        List<Adat_Szerelvény> Elő_Szer_Adatok = new List<Adat_Szerelvény>();
        List<Adat_Szerelvény> AdatokSzer = new List<Adat_Szerelvény>();
        List<Adat_Jármű> AdatokJár = new List<Adat_Jármű>();

        #region Alap
        public Ablak_szerelvény()
        {
            InitializeComponent();
            Start();
        }

        private void Ablak_szerelvény_Load(object sender, EventArgs e)
        {

        }

        private void Ablak_szerelvény_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kereső?.Close();
        }

        private void Ablak_szerelvény_Shown(object sender, EventArgs e)
        {
            try
            {
                Ellenőrzés();
                List<Adat_Szerelvény> Adatok = KézSzerelvény.Lista_Adatok(Cmbtelephely.Text.Trim(), true);
                if (Adatok != null && Adatok.Count > 0)
                {
                    HibásTábla.Visible = true;
                    Label3.Visible = true;
                }
                else
                {
                    HibásTábla.Visible = false;
                    Label3.Visible = false;
                }
                Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;
                Képernyő_frissítés_Tényleges();
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

        private void Start()
        {
            Telephelyekfeltöltése();
            Jogosultságkiosztás();
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
            if (Convert.ToBoolean(e.State & DrawItemState.Selected))
            {
                Font BoldFont = new Font(Fülek.Font.Name, Fülek.Font.Size, FontStyle.Bold);
                // háttér szín beállítása
                e.Graphics.FillRectangle(new SolidBrush(Color.DarkGray), e.Bounds);
                Rectangle paddedBounds = e.Bounds;
                paddedBounds.Inflate(0, 0);
                e.Graphics.DrawString(SelectedTab.Text, BoldFont, BlackTextBrush, paddedBounds, sf);
            }
            else
                e.Graphics.DrawString(SelectedTab.Text, e.Font, BlackTextBrush, HeaderRect, sf);
            // Munka kész – dobja ki a keféket
            BlackTextBrush.Dispose();
        }

        private void Fülek_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC
            if ((int)e.KeyCode == 27)
            {
                Új_Ablak_Kereső = null;
            }


            //Ctrl+f laponkénti kereső
            if (Fülek.SelectedIndex == 0 && (int)e.KeyCode == 70)
                Kereső_hívás("Tény");

            if (Fülek.SelectedIndex == 1 && (int)e.KeyCode == 70)
                Kereső_hívás("Előírt");
        }

        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\szerelvény.html";
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

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    Cmbtelephely.Items.Add(Elem);

                if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér)
                    Cmbtelephely.Text = Cmbtelephely.Items[0].ToString().Trim();
                else
                    Cmbtelephely.Text = Program.PostásTelephely;

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

        private void Jogosultságkiosztás()
        {
            int melyikelem;

            // ide kell az összes gombot tenni amit szabályozni akarunk false
            Egyszerelvényminusz.Enabled = false;
            Szerelvénytörlés.Enabled = false;
            Újszerelvény.Enabled = false;
            Hozzáad.Enabled = false;

            Előírt_Egyszerelvényminusz.Enabled = false;
            Előírt_szerelvénytörlés.Enabled = false;
            Előírt_Újszerelvény.Enabled = false;
            Előírt_hozzáad.Enabled = false;
            E2_panel.Visible = false;

            Btnrögzítés.Enabled = false;
            E2_Törlés.Enabled = false;



            melyikelem = 100;
            // módosítás 1 Szerelvény módosítás gombok

            if (MyF.Vanjoga(melyikelem, 1))
            {
                //itt kapcsolja vissza a gombot
                Egyszerelvényminusz.Enabled = true;
                Szerelvénytörlés.Enabled = true;
                Újszerelvény.Enabled = true;
                Hozzáad.Enabled = true;
            }

            if (MyF.Vanjoga(melyikelem, 2))
            {
                //itt kapcsolja vissza a gombot
                Előírt_Egyszerelvényminusz.Enabled = true;
                Előírt_szerelvénytörlés.Enabled = true;
                Előírt_Újszerelvény.Enabled = true;
                Előírt_hozzáad.Enabled = true;
                E2_panel.Visible = true;
            }

            if (MyF.Vanjoga(melyikelem, 3))
            {
                //itt kapcsolja vissza a gombot
                Btnrögzítés.Enabled = true;
                E2_Törlés.Enabled = true;
            }
        }

        private void Fülekkitöltése()
        {
            switch (Fülek.SelectedIndex)
            {
                case 0:
                    {
                        // Tényleges
                        this.AcceptButton = Hozzáad;
                        Képernyő_frissítés_Tényleges();
                        break;
                    }

                case 1:
                    {
                        // Előírás
                        this.AcceptButton = Előírt_hozzáad;
                        TípuCombo_Listáz();
                        AdatokJár = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                        Előírt_Szerelvénytábla_listázás();
                        Előírt_Felsőtábla();
                        E2PanelLátszik();
                        break;
                    }
                case 2:
                    {
                        // napló
                        DátumNapló.Value = DateTime.Today;
                        break;
                    }
                case 3:
                    {
                        // Utasítás
                        Tervezet_utasítás();
                        break;
                    }
            }
        }

        private void E2PanelLátszik()
        {
            try
            {
                List<Adat_Kiegészítő_Igen_Nem> Adatok = KézIgenNem.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adat_Kiegészítő_Igen_Nem Válasz = (from a in Adatok
                                                   where a.Id == 1
                                                   select a).FirstOrDefault();

                if (!Válasz.Válasz)
                {
                    E2_panel.Visible = true;
                    Osztás = true;
                }
                else
                {
                    E2_panel.Visible = false;
                    Osztás = false;
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

        private void Fülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }

        private void Cmbtelephely_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmbtelephely.SelectedIndex < 0 && Cmbtelephely.Text.Trim() == "") return;

            Cmbtelephely.Text = Cmbtelephely.Items[Cmbtelephely.SelectedIndex].ToString();
            Fülek.SelectedIndex = 0;
            Fülekkitöltése();
        }
        #endregion


        #region Tényleges
        private void Képernyő_frissítés_Tényleges()
        {
            ComboListáz();
            Kiírja_pályaszámokat();
            AdatokJár = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
            Szerelvénytábla_listázás();
            Felsőtábla();
            Hibás_csatolások();
            szAdat = new Adat_Szerelvény(0, 0, "0", "0", "0", "0", "0", "0");

        }

        private void Képernyő_frissítés_Tényleges_rész()
        {
            Kiírja_pályaszámokat();
            AdatokJár = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
            Szerelvénytábla_listázás();
            Hibás_csatolások();
        }

        private void ComboListáz()
        {
            try
            {
                Combo1.Items.Clear();
                List<Adat_Jármű_Állomány_Típus> Adatok = KézTípus.Lista_Adatok(Cmbtelephely.Text.Trim());

                foreach (Adat_Jármű_Állomány_Típus Elem in Adatok)
                    Combo1.Items.Add(Elem.Típus);

                Combo1.Refresh();
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

        private void Hozzáad_Click(object sender, EventArgs e)
        {
            try
            {
                // ha nincs kiválasztva pályaszám, vagy üres a mező
                if (Pályaszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve egy jármű sem.");
                if (szAdat.Szerelvényhossz >= 6) throw new HibásBevittAdat("Több járművet nem lehet a szerelvényhez adni!");

                // megnézzük, hogy létezik-e kocsi       
                AdatokJár = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adat_Jármű Szűrt = (from a in AdatokJár
                                    where a.Azonosító == Pályaszám.Text.Trim()
                                    select a).FirstOrDefault();

                if (Szűrt == null)
                {
                    Pályaszám.Focus();
                    throw new HibásBevittAdat("A telephelyen nincs ilyen jármű!");
                }
                //Leellenőrizzük, hogy nincs-e másik szerelvényben
                else if (Szűrt.Szerelvény)
                {
                    KözösKereső(Szerelvénylista, Pályaszám.Text.Trim());
                    throw new HibásBevittAdat("A jármű már egy másik szerelvényben van! Beépítéshez előbb ki kell építeni.");
                }

                AdatokSzer = KézSzerelvény.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adat_Szerelvény ideig = (from a in AdatokSzer
                                         where a.Kocsi1 == Pályaszám.Text.Trim() || a.Kocsi2 == Pályaszám.Text.Trim() || a.Kocsi3 == Pályaszám.Text.Trim() ||
                                               a.Kocsi4 == Pályaszám.Text.Trim() || a.Kocsi5 == Pályaszám.Text.Trim() || a.Kocsi6 == Pályaszám.Text.Trim()
                                         select a).FirstOrDefault();
                if (ideig != null)
                {
                    KözösKereső(Szerelvénylista, Pályaszám.Text.Trim());
                    throw new HibásBevittAdat("A jármű már egy másik szerelvényben van! Beépítéshez előbb ki kell építeni.");
                }

                //új szerelvényszám
                long Szerelvény_ID = szAdat.Szerelvény_ID;
                if (szAdat.Szerelvény_ID == 0)
                {
                    if (AdatokSzer.Count > 0) Szerelvény_ID = AdatokSzer.Max(a => a.Szerelvény_ID) + 1;    // megkeressük az utolsó számot
                }

                long Szerelvény_hossz = szAdat.Szerelvényhossz + 1;

                //Hozzáadjuk az első kocsinak, majd rendezzük
                string[] kocsik = new string[] { Pályaszám.Text.Trim(), szAdat.Kocsi1, szAdat.Kocsi2, szAdat.Kocsi3, szAdat.Kocsi4, szAdat.Kocsi5 };
                Kocsi_rendező(kocsik, "0");

                Adat_Szerelvény Adat = new Adat_Szerelvény(Szerelvény_ID, Szerelvény_hossz, kocsik[0], kocsik[1], kocsik[2], kocsik[3], kocsik[4], kocsik[5]);
                // Rögzítjük, vagy módosítjuk
                if (szAdat.Szerelvény_ID == 0)
                    KézSzerelvény.Rögzítés(Cmbtelephely.Text.Trim(), Adat);
                else
                    KézSzerelvény.Módosítás(Cmbtelephely.Text.Trim(), Adat);

                //Alapértéket beállítjuk és naplózzuk
                szAdat = new Adat_Szerelvény(Szerelvény_ID, Szerelvény_hossz, kocsik[0], kocsik[1], kocsik[2], kocsik[3], kocsik[4], kocsik[5]);
                KézSzerNapló.Rögzítés(Cmbtelephely.Text.Trim(), DateTime.Today, szAdat);


                // átállítjuk a jármű tulajdonságait
                Adat_Jármű ADAT = new Adat_Jármű(Pályaszám.Text.Trim(), true, Szerelvény_ID);
                KézJármű.Módosítás_Szerelvény(Cmbtelephely.Text.Trim(), ADAT);

                Képernyő_frissítés_Tényleges_rész();
                Szerelvénytáblasor_kiírása(szAdat);

                Pályaszám.Text = "";
                Pályaszám.Focus();

                this.AcceptButton = Hozzáad;
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

        private void Kocsi_rendező(string[] kocsik, string mire)
        {
            try
            {         //kicseréljük a 0 és a '' és a _ egy A betűre
                for (int i = 0; i < kocsik.Length; i++)
                {
                    if (kocsik[i] == "0" || kocsik[i] == "" || kocsik[i] == "_" || kocsik[i] == null)
                    {
                        kocsik[i] = "A";
                    }

                }
                // Sorbarendezzük
                for (int i = 0; i < kocsik.Length; i++)
                {
                    for (int j = i + 1; j < kocsik.Length; j++)
                    {
                        if (string.Compare(kocsik[i], kocsik[j]) > 0)
                            (kocsik[j], kocsik[i]) = (kocsik[i], kocsik[j]);    //Sorbarendezzük a kocsikat

                    }
                }
                //Visszacseréljük az A-t
                for (int i = 0; i < kocsik.Length; i++)
                {
                    if (kocsik[i] == "A")
                    {
                        kocsik[i] = mire;
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

        private void Szerelvénytörlés_Click(object sender, EventArgs e)
        {
            Törli_szerelvényt();
        }

        private void Törli_szerelvényt()
        {
            try
            {
                if (Szerelvénytáblasor.Rows[0].Cells[0].Value == null || Szerelvénytáblasor.Rows[0].Cells[0].Value.ToStrTrim() == "") throw new HibásBevittAdat("Nincs kijelölve egy szerelvény sem.");

                KézSzerelvény.Törlés(Cmbtelephely.Text.Trim(), szAdat.Szerelvény_ID);

                //Módosítjuk a villamos táblát
                string[] kocsik = new string[] { szAdat.Kocsi1, szAdat.Kocsi2, szAdat.Kocsi3, szAdat.Kocsi4, szAdat.Kocsi5, szAdat.Kocsi6 };

                List<Adat_Jármű> AdatokGy = new List<Adat_Jármű>();
                for (int i = 0; i < kocsik.Length; i++)
                {
                    if (kocsik[i] != null)
                    {
                        Adat_Jármű ADAT = new Adat_Jármű(kocsik[i], false, 0);
                        AdatokGy.Add(ADAT);
                    }
                }
                KézJármű.Módosítás_Szerelvény(Cmbtelephely.Text.Trim(), AdatokGy);

                KézSzerNapló.Rögzítés(Cmbtelephely.Text.Trim(), DateTime.Today, szAdat);

                szAdat = new Adat_Szerelvény(0, 0, "0", "0", "0", "0", "0", "0");

                Képernyő_frissítés_Tényleges();

                Pályaszám.Text = "";
                Pályaszám.Focus();
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

        private void Újszerelvény_Click(object sender, EventArgs e)
        {
            Képernyő_frissítés_Tényleges();

            Pályaszám.Text = "";
            Pályaszám.Focus();
        }

        private void Szerelvénytáblasor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                long szerelvényID;
                if (Szerelvénytáblasor.Rows[0].Cells[6].Value == null || Szerelvénytáblasor.Rows[0].Cells[6].Value.ToStrTrim() == "")
                    szerelvényID = 0;
                else
                    szerelvényID = Szerelvénytáblasor.Rows[0].Cells[6].Value.ToÉrt_Long();

                long szerelvényhossz;
                if (Szerelvénytáblasor.Rows[0].Cells[7].Value == null || Szerelvénytáblasor.Rows[0].Cells[7].Value.ToStrTrim() == "")
                    szerelvényhossz = 0;
                else
                    szerelvényhossz = Szerelvénytáblasor.Rows[0].Cells[7].Value.ToÉrt_Long();

                string[] kocsik = new string[6];
                if (!(Szerelvénytáblasor.Rows[0].Cells[0].Value == null || Szerelvénytáblasor.Rows[0].Cells[0].Value.ToStrTrim() == ""))
                    kocsik[0] = Szerelvénytáblasor.Rows[0].Cells[0].Value.ToStrTrim();
                if (!(Szerelvénytáblasor.Rows[0].Cells[1].Value == null || Szerelvénytáblasor.Rows[0].Cells[1].Value.ToStrTrim() == ""))
                    kocsik[1] = Szerelvénytáblasor.Rows[0].Cells[1].Value.ToStrTrim();
                if (!(Szerelvénytáblasor.Rows[0].Cells[2].Value == null || Szerelvénytáblasor.Rows[0].Cells[2].Value.ToStrTrim() == ""))
                    kocsik[2] = Szerelvénytáblasor.Rows[0].Cells[2].Value.ToStrTrim();
                if (!(Szerelvénytáblasor.Rows[0].Cells[3].Value == null || Szerelvénytáblasor.Rows[0].Cells[3].Value.ToStrTrim() == ""))
                    kocsik[3] = Szerelvénytáblasor.Rows[0].Cells[3].Value.ToStrTrim();
                if (!(Szerelvénytáblasor.Rows[0].Cells[4].Value == null || Szerelvénytáblasor.Rows[0].Cells[4].Value.ToStrTrim() == ""))
                    kocsik[4] = Szerelvénytáblasor.Rows[0].Cells[4].Value.ToStrTrim();
                if (!(Szerelvénytáblasor.Rows[0].Cells[5].Value == null || Szerelvénytáblasor.Rows[0].Cells[5].Value.ToStrTrim() == ""))
                    kocsik[5] = Szerelvénytáblasor.Rows[0].Cells[5].Value.ToStrTrim();

                szAdat = new Adat_Szerelvény(szerelvényID, szerelvényhossz, kocsik[0], kocsik[1], kocsik[2], kocsik[3], kocsik[4], kocsik[5]);

                if ((Szerelvénytáblasor.Rows[0].Cells[e.ColumnIndex].Value == null || Szerelvénytáblasor.Rows[0].Cells[e.ColumnIndex].Value.ToStrTrim() == ""))
                    Melyik_azonosító = "0";
                else
                    Melyik_azonosító = Szerelvénytáblasor.Rows[0].Cells[e.ColumnIndex].Value.ToStrTrim();
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

        private void Egyszerelvényminusz_Click(object sender, EventArgs e)
        {
            try
            {
                if (Szerelvénytáblasor.Rows[0].Cells[6].Value == null || Szerelvénytáblasor.Rows[0].Cells[6].Value.ToStrTrim() == "0") throw new HibásBevittAdat("Nincs kijelölve egy jármű sem.");

                //ha csak egy kocsi van akkor törli azt
                if (Szerelvénytáblasor.Rows[0].Cells[7].Value.ToString() == "1")
                {
                    Törli_szerelvényt();
                    return;
                }

                // kicseréljük 0-ra a kiválasztott kocsit
                string[] kocsik = new string[] { szAdat.Kocsi1, szAdat.Kocsi2, szAdat.Kocsi3, szAdat.Kocsi4, szAdat.Kocsi5, szAdat.Kocsi6 };
                for (int i = 0; i < kocsik.Length; i++)
                {
                    if (kocsik[i] == Melyik_azonosító.Trim())
                        kocsik[i] = "0";
                }

                Kocsi_rendező(kocsik, "0");
                long Szerelvény_hossz = szAdat.Szerelvényhossz - 1;

                //rögzítjük a módosítást
                Adat_Szerelvény ADAT = new Adat_Szerelvény(szAdat.Szerelvény_ID, Szerelvény_hossz, kocsik[0], kocsik[1], kocsik[2], kocsik[3], kocsik[4], kocsik[5]);
                KézSzerelvény.Módosítás(Cmbtelephely.Text.Trim(), ADAT);


                // átállítjuk a jármű tulajdonságait
                Adat_Jármű ADATJármű = new Adat_Jármű(Melyik_azonosító.Trim(), false, 0);
                KézJármű.Módosítás_Szerelvény(Cmbtelephely.Text.Trim(), ADATJármű);

                //Átállítjuk a osztályszintű értéket és naplózzuk a módosítást
                szAdat = ADAT;
                KézSzerNapló.Rögzítés(Cmbtelephely.Text.Trim(), DateTime.Today, szAdat);

                Szerelvénytábla_listázás();
                Szerelvénytáblasor_kiírása(szAdat);
                Kiírja_pályaszámokat();

                Pályaszám.Text = "";
                Pályaszám.Focus();

                Melyik_azonosító = "0";
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

        private void Combo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Kiírja_pályaszámokat();
        }

        private void Kiírja_pályaszámokat()
        {
            try
            {
                Pályaszám.Items.Clear();
                if (Combo1.SelectedIndex < 0 && Combo1.Text.Trim() == "") return;

                List<Adat_Jármű> Adatok = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());

                List<Adat_Jármű> Elemek = (from a in Adatok
                                           where a.Típus == Combo1.Text.Trim()
                                           && a.Szerelvény == false
                                           orderby a.Azonosító
                                           select a).ToList();

                if (Elemek != null)
                {
                    foreach (Adat_Jármű rekord in Elemek)
                        Pályaszám.Items.Add(rekord.Azonosító);
                    Pályaszám.Refresh();
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Szerelvénylista_gomb_Click(object sender, EventArgs e)
        {
            Képernyő_frissítés_Tényleges();
        }

        private void Szerelvénytábla_listázás()
        {
            try
            {
                AdatokSzer = KézSzerelvény.Lista_Adatok(Cmbtelephely.Text.Trim());

                Szerelvénylista.Rows.Clear();
                Szerelvénylista.Columns.Clear();
                Szerelvénylista.Refresh();
                Szerelvénylista.Visible = false;
                Szerelvénylista.ColumnCount = 9;

                // fejléc elkészítése
                Szerelvénylista.Columns[0].HeaderText = "Kocsi 1";
                Szerelvénylista.Columns[0].Width = 70;
                Szerelvénylista.Columns[1].HeaderText = "Kocsi 2";
                Szerelvénylista.Columns[1].Width = 70;
                Szerelvénylista.Columns[2].HeaderText = "Kocsi 3";
                Szerelvénylista.Columns[2].Width = 70;
                Szerelvénylista.Columns[3].HeaderText = "Kocsi 4";
                Szerelvénylista.Columns[3].Width = 70;
                Szerelvénylista.Columns[4].HeaderText = "Kocsi 5";
                Szerelvénylista.Columns[4].Width = 70;
                Szerelvénylista.Columns[5].HeaderText = "Kocsi 6";
                Szerelvénylista.Columns[5].Width = 70;
                Szerelvénylista.Columns[6].HeaderText = "Sorzám";
                Szerelvénylista.Columns[6].Width = 80;
                Szerelvénylista.Columns[7].HeaderText = "Hossz";
                Szerelvénylista.Columns[7].Width = 100;
                Szerelvénylista.Columns[8].HeaderText = "Szerelvény";
                Szerelvénylista.Columns[8].Width = 100;
                Szerelvénylista.Columns[8].Visible = false;

                foreach (Adat_Szerelvény adat in AdatokSzer)
                {
                    Szerelvénylista.RowCount++;
                    int i = Szerelvénylista.RowCount - 1;
                    Szerelvénylista.Rows[i].Cells[0].Value = adat.Kocsi1 == "0" ? "" : adat.Kocsi1;
                    Szerelvénylista.Rows[i].Cells[0].Style.BackColor = Milyenszínű(adat.Kocsi1);
                    Szerelvénylista.Rows[i].Cells[1].Value = adat.Kocsi2 == "0" ? "" : adat.Kocsi2;
                    Szerelvénylista.Rows[i].Cells[1].Style.BackColor = Milyenszínű(adat.Kocsi2);
                    Szerelvénylista.Rows[i].Cells[2].Value = adat.Kocsi3 == "0" ? "" : adat.Kocsi3;
                    Szerelvénylista.Rows[i].Cells[2].Style.BackColor = Milyenszínű(adat.Kocsi3);
                    Szerelvénylista.Rows[i].Cells[3].Value = adat.Kocsi4 == "0" ? "" : adat.Kocsi4;
                    Szerelvénylista.Rows[i].Cells[3].Style.BackColor = Milyenszínű(adat.Kocsi4);
                    Szerelvénylista.Rows[i].Cells[4].Value = adat.Kocsi5 == "0" ? "" : adat.Kocsi5;
                    Szerelvénylista.Rows[i].Cells[4].Style.BackColor = Milyenszínű(adat.Kocsi5);
                    Szerelvénylista.Rows[i].Cells[5].Value = adat.Kocsi6 == "0" ? "" : adat.Kocsi6;
                    Szerelvénylista.Rows[i].Cells[5].Style.BackColor = Milyenszínű(adat.Kocsi6);
                    Szerelvénylista.Rows[i].Cells[6].Value = adat.Szerelvény_ID.ToString();
                    Szerelvénylista.Rows[i].Cells[7].Value = adat.Szerelvényhossz.ToString();
                    string szerelvény = adat.Kocsi1 == "0" ? "" : adat.Kocsi1;
                    szerelvény += adat.Kocsi2 == "0" ? "" : adat.Kocsi2;
                    szerelvény += adat.Kocsi3 == "0" ? "" : adat.Kocsi3;
                    szerelvény += adat.Kocsi4 == "0" ? "" : adat.Kocsi4;
                    szerelvény += adat.Kocsi5 == "0" ? "" : adat.Kocsi5;
                    szerelvény += adat.Kocsi6 == "0" ? "" : adat.Kocsi6;
                    Szerelvénylista.Rows[i].Cells[8].Value = szerelvény;
                }
                Szerelvénylista.Visible = true;
                Szerelvénylista.Refresh();
                Szerelvénylista.ClearSelection();

                Hibás_csatolások();
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Hibás_csatolások()
        {
            try
            {
                Elő_Szer_Adatok = KézSzerelvény.Lista_Adatok(Cmbtelephely.Text.Trim(), true);
                AdatokSzer = KézSzerelvény.Lista_Adatok(Cmbtelephely.Text.Trim());

                HibásTábla.Rows.Clear();
                HibásTábla.Columns.Clear();
                HibásTábla.Refresh();
                HibásTábla.Visible = false;
                HibásTábla.ColumnCount = 6;

                // fejléc elkészítése
                HibásTábla.Columns[0].HeaderText = "Kocsi 1";
                HibásTábla.Columns[0].Width = 70;
                HibásTábla.Columns[1].HeaderText = "Kocsi 2";
                HibásTábla.Columns[1].Width = 70;
                HibásTábla.Columns[2].HeaderText = "Kocsi 3";
                HibásTábla.Columns[2].Width = 70;
                HibásTábla.Columns[3].HeaderText = "Kocsi 4";
                HibásTábla.Columns[3].Width = 70;
                HibásTábla.Columns[4].HeaderText = "Kocsi 5";
                HibásTábla.Columns[4].Width = 70;
                HibásTábla.Columns[5].HeaderText = "Kocsi 6";
                HibásTábla.Columns[5].Width = 70;

                foreach (Adat_Szerelvény Elem in AdatokSzer)
                {
                    //megkeressük, hogy a előírtban benne van-e
                    Adat_Szerelvény Ideig = (from a in Elő_Szer_Adatok
                                             where a.Kocsi1 == Elem.Kocsi1 || a.Kocsi1 == Elem.Kocsi2 || a.Kocsi1 == Elem.Kocsi3 ||
                                             a.Kocsi1 == Elem.Kocsi4 || a.Kocsi1 == Elem.Kocsi5 || a.Kocsi1 == Elem.Kocsi6
                                             select a).FirstOrDefault();
                    //ha nincs benne az előírtban akkor nem foglakozunk vele
                    if (Ideig != null)
                    {
                        string[] elemektény = { Elem.Kocsi1 != "0" ? Elem.Kocsi1 : "" ,
                                                Elem.Kocsi2 != "0" ? Elem.Kocsi2 : "" ,
                                                Elem.Kocsi3 != "0" ? Elem.Kocsi3 : "" ,
                                                Elem.Kocsi4 != "0" ? Elem.Kocsi4 : "" ,
                                                Elem.Kocsi5 != "0" ? Elem.Kocsi5 : "" ,
                                                Elem.Kocsi6 != "0" ? Elem.Kocsi6 : ""  };
                        string Tényszerelvény = string.Join("", elemektény);
                        string[] elemekelőírt = {Ideig.Kocsi1 != "_" ? Ideig.Kocsi1 : "",
                                                 Ideig.Kocsi2 != "_" ? Ideig.Kocsi2 : "",
                                                 Ideig.Kocsi3 != "_" ? Ideig.Kocsi3 : "",
                                                 Ideig.Kocsi4 != "_" ? Ideig.Kocsi4 : "",
                                                 Ideig.Kocsi5 != "_" ? Ideig.Kocsi5 : "",
                                                 Ideig.Kocsi6 != "_" ? Ideig.Kocsi6 : "" };
                        string Előszerelvény = string.Join("", elemekelőírt);
                        //Ha eltér akkor kiírjuk
                        if (Tényszerelvény != Előszerelvény)
                        {
                            HibásTábla.RowCount++;
                            int i = HibásTábla.RowCount - 1;

                            if (Ideig.Kocsi1 != "_")
                            {
                                HibásTábla.Rows[i].Cells[0].Value = Ideig.Kocsi1;
                                HibásTábla.Rows[i].Cells[0].Style.BackColor = Milyenszínű(Ideig.Kocsi1);
                            }
                            if (Ideig.Kocsi2 != "_")
                            {
                                HibásTábla.Rows[i].Cells[1].Value = Ideig.Kocsi2;
                                HibásTábla.Rows[i].Cells[1].Style.BackColor = Milyenszínű(Ideig.Kocsi2);
                            }
                            if (Ideig.Kocsi3 != "_")
                            {
                                HibásTábla.Rows[i].Cells[2].Value = Ideig.Kocsi3;
                                HibásTábla.Rows[i].Cells[2].Style.BackColor = Milyenszínű(Ideig.Kocsi3);
                            }
                            if (Ideig.Kocsi4 != "_")
                            {
                                HibásTábla.Rows[i].Cells[3].Value = Ideig.Kocsi4;
                                HibásTábla.Rows[i].Cells[3].Style.BackColor = Milyenszínű(Ideig.Kocsi4);
                            }
                            if (Ideig.Kocsi5 != "_")
                            {
                                HibásTábla.Rows[i].Cells[4].Value = Ideig.Kocsi5;
                                HibásTábla.Rows[i].Cells[4].Style.BackColor = Milyenszínű(Ideig.Kocsi5);
                            }
                            if (Ideig.Kocsi6 != "_")
                            {
                                HibásTábla.Rows[i].Cells[5].Value = Ideig.Kocsi6;
                                HibásTábla.Rows[i].Cells[5].Style.BackColor = Milyenszínű(Ideig.Kocsi6);
                            }
                        }
                    }
                }
                HibásTábla.Visible = true;
                HibásTábla.ClearSelection();
                HibásTábla.Refresh();
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Felsőtábla()
        {
            Szerelvénytáblasor.Rows.Clear();
            Szerelvénytáblasor.Columns.Clear();
            Szerelvénytáblasor.Refresh();
            Szerelvénytáblasor.Visible = false;
            Szerelvénytáblasor.ColumnCount = 8;
            Szerelvénytáblasor.RowCount = 1;
            Szerelvénytáblasor.Columns[0].HeaderText = "Kocsi 1";
            Szerelvénytáblasor.Columns[0].Width = 70;
            Szerelvénytáblasor.Columns[1].HeaderText = "Kocsi 2";
            Szerelvénytáblasor.Columns[1].Width = 70;
            Szerelvénytáblasor.Columns[2].HeaderText = "Kocsi 3";
            Szerelvénytáblasor.Columns[2].Width = 70;
            Szerelvénytáblasor.Columns[3].HeaderText = "Kocsi 4";
            Szerelvénytáblasor.Columns[3].Width = 70;
            Szerelvénytáblasor.Columns[4].HeaderText = "Kocsi 5";
            Szerelvénytáblasor.Columns[4].Width = 70;
            Szerelvénytáblasor.Columns[5].HeaderText = "Kocsi 6";
            Szerelvénytáblasor.Columns[5].Width = 70;
            Szerelvénytáblasor.Columns[6].HeaderText = "Szerelvény";
            Szerelvénytáblasor.Columns[6].Width = 60;
            Szerelvénytáblasor.Columns[6].Visible = false;
            Szerelvénytáblasor.Columns[7].HeaderText = "Szerelvény";
            Szerelvénytáblasor.Columns[7].Width = 60;
            Szerelvénytáblasor.Columns[7].Visible = false;
            Szerelvénytáblasor.Visible = true;
            Szerelvénytáblasor.Refresh();
            Szerelvénytáblasor.ClearSelection();
        }

        private void Szerelvénylista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    long szerelvényID;
                    if (Szerelvénylista.Rows[e.RowIndex].Cells[6].Value == null || Szerelvénylista.Rows[e.RowIndex].Cells[6].Value.ToStrTrim() == "")
                        szerelvényID = 0;
                    else
                        szerelvényID = Szerelvénylista.Rows[e.RowIndex].Cells[6].Value.ToÉrt_Long();

                    List<Adat_Szerelvény> Adatok = KézSzerelvény.Lista_Adatok(Cmbtelephely.Text.Trim());
                    szAdat = Adatok.FirstOrDefault(a => a.Szerelvény_ID == szerelvényID);

                    if (szAdat != null) Szerelvénytáblasor_kiírása(szAdat);
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

        private void Szerelvénytáblasor_kiírása(Adat_Szerelvény Adat)
        {
            try
            {
                Szerelvénytáblasor.Rows[0].Cells[0].Value = Adat.Kocsi1 == "0" ? "" : Adat.Kocsi1;
                Szerelvénytáblasor.Rows[0].Cells[1].Value = Adat.Kocsi2 == "0" ? "" : Adat.Kocsi2;
                Szerelvénytáblasor.Rows[0].Cells[2].Value = Adat.Kocsi3 == "0" ? "" : Adat.Kocsi3;
                Szerelvénytáblasor.Rows[0].Cells[3].Value = Adat.Kocsi4 == "0" ? "" : Adat.Kocsi4;
                Szerelvénytáblasor.Rows[0].Cells[4].Value = Adat.Kocsi5 == "0" ? "" : Adat.Kocsi5;
                Szerelvénytáblasor.Rows[0].Cells[5].Value = Adat.Kocsi6 == "0" ? "" : Adat.Kocsi6;

                Szerelvénytáblasor.Rows[0].Cells[6].Value = Adat.Szerelvény_ID.ToString();
                Szerelvénytáblasor.Rows[0].Cells[7].Value = Adat.Szerelvényhossz.ToString();
                Szerelvénytáblasor.ClearSelection();
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
                if (Szerelvénylista.Rows.Count <= 0) return;

                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Szerelvémyek_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddhhmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyE.DataGridViewToExcel(fájlexc, Szerelvénylista);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MyE.Megnyitás(fájlexc);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region Ellenőrzések
        private void Ellenőrzés()
        {
            Panel2.Visible = true;
            this.Refresh();
            this.Cursor = Cursors.WaitCursor; // homok óra kezdete

            //kitöröljük azokat a szerelvényeket, melynek valamelyik eleme elment a telephelyről
            Ellenőrző4(true);
            Ellenőrző4(false);
            Ellenőrző2();
            Ellenőrző();
            Ellenőrző1();
            Ellenőrző5();
            this.Cursor = Cursors.Default; // homokóra vége
            Panel2.Visible = false;
            this.Refresh();
        }

        private void Ellenőrző()
        {
            try
            {
                // leellenőrizzük, hogy a szerelvény táblában szereplő adatok egyeznek a villamos táblában lévővel
                AdatokSzer = KézSzerelvény.Lista_Adatok(Cmbtelephely.Text.Trim());
                AdatokJár = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());

                Holtart.Be(AdatokJár.Count + 1);

                // végig pörgetjük a kocsikat és közben ellenőrizzük, hogy a kocsihoz letárolt szerelvény szám létezik-e,
                // ha nem létezik, akkor a kocsit egy kocsiként fogjuk kezelni.

                List<Adat_Jármű> AdatokGy = new List<Adat_Jármű>();
                foreach (Adat_Jármű rekord in AdatokJár)
                {
                    if (rekord.Szerelvénykocsik != 0)
                    {
                        Adat_Szerelvény Elem = (from a in AdatokSzer
                                                where a.Szerelvény_ID == rekord.Szerelvénykocsik
                                                select a).FirstOrDefault();
                        // ha nincs akkor kitöröljük a villamosból
                        if (Elem == null)
                        {
                            Adat_Jármű ADAT = new Adat_Jármű(rekord.Azonosító.Trim(), false, 0);
                            AdatokGy.Add(ADAT);
                        }
                        else
                        {
                            //ha az id egyezik, de nincs benne a kocsi
                            if (!(Elem.Kocsi1 == rekord.Azonosító || Elem.Kocsi2 == rekord.Azonosító || Elem.Kocsi3 == rekord.Azonosító ||
                                 Elem.Kocsi4 == rekord.Azonosító || Elem.Kocsi5 == rekord.Azonosító || Elem.Kocsi6 == rekord.Azonosító))
                            {
                                Adat_Jármű ADAT = new Adat_Jármű(rekord.Azonosító.Trim(), false, 0);
                                AdatokGy.Add(ADAT);
                            }
                        }
                    }
                    Holtart.Lép();
                }
                if (AdatokGy != null && AdatokGy.Count > 0) KézJármű.Módosítás_Szerelvény(Cmbtelephely.Text.Trim(), AdatokGy);

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

        private void Ellenőrző1()
        {
            try
            {
                AdatokSzer = KézSzerelvény.Lista_Adatok(Cmbtelephely.Text.Trim());
                AdatokJár = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());

                // Végig  nézzük a szerelvény kocsijait és ha létezik a psz-os táblában, akkor beíjuk a szerelvényt
                Holtart.Be(AdatokSzer.Count + 1);

                foreach (Adat_Szerelvény rekordszer in AdatokSzer)
                {

                    Elem_Vizs(rekordszer.Kocsi1, rekordszer.Szerelvény_ID);
                    Elem_Vizs(rekordszer.Kocsi2, rekordszer.Szerelvény_ID);
                    Elem_Vizs(rekordszer.Kocsi3, rekordszer.Szerelvény_ID);
                    Elem_Vizs(rekordszer.Kocsi4, rekordszer.Szerelvény_ID);
                    Elem_Vizs(rekordszer.Kocsi5, rekordszer.Szerelvény_ID);
                    Elem_Vizs(rekordszer.Kocsi6, rekordszer.Szerelvény_ID);
                    // megnézzük a szerelvényeket számokhoz tartozó kocsikat, hogy a villamos táblában ugyanúgy szerepel-e
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

        private void Elem_Vizs(string kocsi, long szerelvényszám)
        {
            try
            {
                if (kocsi.Trim() != "0")
                {
                    Adat_Jármű Ideig = (from a in AdatokJár
                                        where a.Azonosító == kocsi && szerelvényszám == a.Szerelvénykocsik && a.Szerelvény
                                        select a).FirstOrDefault();
                    if (Ideig == null)
                    {
                        // ha nem egyezik meg akkor berögzítjük 
                        Adat_Jármű ADAT = new Adat_Jármű(kocsi.Trim(), true, szerelvényszám);
                        KézJármű.Módosítás_Szerelvény(Cmbtelephely.Text.Trim(), ADAT);

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

        private void Ellenőrző2()
        {
            try
            {
                //kitöröljük azokat a szerelvényeket, melyek hossza 0
                Elő_Szer_Adatok = KézSzerelvény.Lista_Adatok(Cmbtelephely.Text.Trim(), true);
                AdatokSzer = KézSzerelvény.Lista_Adatok(Cmbtelephely.Text.Trim());

                List<Adat_Szerelvény> ideig = (from a in AdatokSzer
                                               where a.Szerelvényhossz == 0
                                               select a).ToList();
                List<long> Sorszámok = new List<long>();
                foreach (Adat_Szerelvény Elem in ideig)
                    Sorszámok.Add(Elem.Szerelvény_ID);

                if (Sorszámok != null && Sorszámok.Count > 0)
                    KézSzerelvény.Törlés(Cmbtelephely.Text.Trim(), Sorszámok);


                ideig = (from a in Elő_Szer_Adatok
                         where a.Szerelvényhossz == 0
                         select a).ToList();
                Sorszámok.Clear();
                foreach (Adat_Szerelvény Elem in ideig)
                    Sorszámok.Add(Elem.Szerelvény_ID);

                if (Sorszámok != null && Sorszámok.Count > 0)
                    KézSzerelvény.Törlés(Cmbtelephely.Text.Trim(), Sorszámok, true);
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

        private void Ellenőrző4(bool előírt)
        {
            try
            {
                Elő_Szer_Adatok = KézSzerelvény.Lista_Adatok(Cmbtelephely.Text.Trim(), előírt);
                AdatokJár = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                // A szerelvényt töröljük, ha olyan eleme van ami nincs a telephelyen

                Holtart.Be(Elő_Szer_Adatok.Count + 1);
                List<long> Sorszámok = new List<long>();

                foreach (Adat_Szerelvény rekord in Elő_Szer_Adatok)
                {
                    //Csak egyszer törlünk ki egy szerelvényt
                    if (NincsItt(AdatokJár, rekord.Kocsi1)) Sorszámok.Add(rekord.Szerelvény_ID);
                    if (NincsItt(AdatokJár, rekord.Kocsi2)) Sorszámok.Add(rekord.Szerelvény_ID);
                    if (NincsItt(AdatokJár, rekord.Kocsi3)) Sorszámok.Add(rekord.Szerelvény_ID);
                    if (NincsItt(AdatokJár, rekord.Kocsi4)) Sorszámok.Add(rekord.Szerelvény_ID);
                    if (NincsItt(AdatokJár, rekord.Kocsi5)) Sorszámok.Add(rekord.Szerelvény_ID);
                    if (NincsItt(AdatokJár, rekord.Kocsi6)) Sorszámok.Add(rekord.Szerelvény_ID);
                    Holtart.Lép();
                }
                Holtart.Ki();
                if (Sorszámok != null && Sorszámok.Count > 0)
                {
                    Sorszámok.OrderBy(a => a).Distinct().ToList();
                    KézSzerelvény.Törlés(Cmbtelephely.Text.Trim(), Sorszámok, előírt);
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

        private bool NincsItt(List<Adat_Jármű> Adatok, string Azonosító)
        {
            bool Válasz = false;
            try
            {
                if (Azonosító == "_" || Azonosító == "0") return false;
                Adat_Jármű Ideig = (from a in Adatok
                                    where a.Azonosító == Azonosító
                                    select a).FirstOrDefault();
                if (Ideig == null) Válasz = true;
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

        private void Ellenőrző5()
        {
            try
            {
                //leellenörizzük, hogy a szerelvény hossza jó-e
                AdatokSzer = KézSzerelvény.Lista_Adatok(Cmbtelephely.Text.Trim());
                Holtart.Be(AdatokSzer.Count + 1);

                List<Adat_Szerelvény> AdatokGy = new List<Adat_Szerelvény>();
                foreach (Adat_Szerelvény rekord in AdatokSzer)
                {
                    long hossz = 0;
                    if (rekord.Kocsi1 != "0") hossz++;
                    if (rekord.Kocsi2 != "0") hossz++;
                    if (rekord.Kocsi3 != "0") hossz++;
                    if (rekord.Kocsi4 != "0") hossz++;
                    if (rekord.Kocsi5 != "0") hossz++;
                    if (rekord.Kocsi6 != "0") hossz++;

                    if (hossz != rekord.Szerelvényhossz)
                    {
                        Adat_Szerelvény ADAT = new Adat_Szerelvény(rekord.Szerelvény_ID, hossz);
                        AdatokGy.Add(ADAT);
                    }
                    Holtart.Lép();
                }
                if (AdatokGy.Count > 0) KézSzerelvény.MódosításHossz(Cmbtelephely.Text.Trim(), AdatokGy);

                //Előírt
                Elő_Szer_Adatok = KézSzerelvény.Lista_Adatok(Cmbtelephely.Text.Trim(), true);
                Holtart.Be(Elő_Szer_Adatok.Count + 1);

                AdatokGy.Clear();
                foreach (Adat_Szerelvény rekord in Elő_Szer_Adatok)
                {
                    long hossz = 0;
                    if (rekord.Kocsi1 != "_") hossz++;
                    if (rekord.Kocsi2 != "_") hossz++;
                    if (rekord.Kocsi3 != "_") hossz++;
                    if (rekord.Kocsi4 != "_") hossz++;
                    if (rekord.Kocsi5 != "_") hossz++;
                    if (rekord.Kocsi6 != "_") hossz++;

                    if (hossz != rekord.Szerelvényhossz)
                    {
                        Adat_Szerelvény ADAT = new Adat_Szerelvény(rekord.Szerelvény_ID, hossz);
                        AdatokGy.Add(ADAT);
                    }
                    Holtart.Lép();
                }
                if (AdatokGy.Count > 0) KézSzerelvény.MódosításHossz(Cmbtelephely.Text.Trim(), AdatokGy, true);
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


        #region Funkciók
        private Color Milyenszínű(string pályaszám)
        {
            Color szín = Color.White;
            long státus = (from a in AdatokJár
                           where a.Azonosító == pályaszám
                           select a.Státus).FirstOrDefault();

            switch (státus)
            {
                case 0:
                    {
                        szín = Color.White;
                        break;
                    }
                case 1:
                    {
                        szín = Color.Gray;
                        break;
                    }
                case 2:
                    {
                        szín = Color.Blue;
                        break;
                    }
                case 3:
                    {
                        szín = Color.Yellow;
                        break;
                    }
                case 4:
                    {
                        szín = Color.Red;
                        break;
                    }
                default:
                    szín = Color.White;
                    break;
            }
            return szín;
        }
        #endregion


        #region Kereső
        private void Új_Ablak_Kereső_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kereső = null;
        }

        private void Szövegkeresés()
        {
            if (Új_Ablak_Kereső.Keresendő == null) return;
            if (Új_Ablak_Kereső.Keresendő.Trim() == "") return;
            if (Szerelvénylista.RowCount < 1) return;
            KözösKereső(Szerelvénylista, Új_Ablak_Kereső.Keresendő.Trim());
        }

        private void KözösKereső(DataGridView Táblázat, string Keressük)
        {
            try
            {
                // megkeressük a szöveget a táblázatban
                for (int j = 0; j < 7; j++)
                {
                    for (int i = 0; i < Táblázat.RowCount - 1; i++)
                    {
                        if (Táblázat.Rows[i].Cells[j].Value != null)
                        {
                            if (Táblázat.Rows[i].Cells[j].Value.ToStrTrim() == Keressük)
                            {
                                Táblázat.Rows[i].Cells[j].Style.BackColor = Color.Orange;
                                Táblázat.Rows[i].Selected = true;
                                return;
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

        private void TényKeres_Click(object sender, EventArgs e)
        {
            Kereső_hívás("Tény");
        }

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
                if (honnan == "Tény")
                    Új_Ablak_Kereső.Ismétlődő_Változás += Szövegkeresés;
                else
                    Új_Ablak_Kereső.Ismétlődő_Változás += Előírt_Kereső_hívás;

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

        private void Előírt_Keresés_Click(object sender, EventArgs e)
        {
            Kereső_hívás("Előírt");
        }

        void Előírt_Kereső_hívás()
        {
            if (Új_Ablak_Kereső.Keresendő == null) return;
            if (Új_Ablak_Kereső.Keresendő.Trim() == "") return;
            if (Szerelvénylista.RowCount < 1) return;
            KözösKereső(Előírt_Szerelvénylista, Új_Ablak_Kereső.Keresendő.Trim());
        }
        #endregion


        #region Előírt
        private void TípuCombo_Listáz()
        {
            try
            {
                Előírt_Combo1.Items.Clear();
                List<Adat_Jármű_Állomány_Típus> Adatok = KézTípus.Lista_Adatok(Cmbtelephely.Text.Trim());

                foreach (Adat_Jármű_Állomány_Típus Elem in Adatok)
                    Előírt_Combo1.Items.Add(Elem.Típus);

                Előírt_Combo1.Refresh();
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

        private void Előírt_pályaszám_MouseEnter(object sender, EventArgs e)
        {
            AcceptButton = Előírt_hozzáad;
        }

        private void Előírt_pályaszám_TextUpdate(object sender, EventArgs e)
        {
            AcceptButton = Előírt_hozzáad;
        }

        private void Előírt_Szerelvénytábla_listázás()
        {
            try
            {
                Elő_Szer_Adatok = KézSzerelvény.Lista_Adatok(Cmbtelephely.Text.Trim(), true);
                AdatokJ2 = KézJármű2.Lista_Adatok(Cmbtelephely.Text.Trim());

                Előírt_Szerelvénylista.Rows.Clear();
                Előírt_Szerelvénylista.Columns.Clear();
                Előírt_Szerelvénylista.Refresh();
                Előírt_Szerelvénylista.Visible = false;
                Előírt_Szerelvénylista.ColumnCount = 9;

                // fejléc elkészítése
                Előírt_Szerelvénylista.Columns[0].HeaderText = "Kocsi 1";
                Előírt_Szerelvénylista.Columns[0].Width = 80;
                Előírt_Szerelvénylista.Columns[1].HeaderText = "Kocsi 2";
                Előírt_Szerelvénylista.Columns[1].Width = 80;
                Előírt_Szerelvénylista.Columns[2].HeaderText = "Kocsi 3";
                Előírt_Szerelvénylista.Columns[2].Width = 80;
                Előírt_Szerelvénylista.Columns[3].HeaderText = "Kocsi 4";
                Előírt_Szerelvénylista.Columns[3].Width = 80;
                Előírt_Szerelvénylista.Columns[4].HeaderText = "Kocsi 5";
                Előírt_Szerelvénylista.Columns[4].Width = 80;
                Előírt_Szerelvénylista.Columns[5].HeaderText = "Kocsi 6";
                Előírt_Szerelvénylista.Columns[5].Width = 80;
                Előírt_Szerelvénylista.Columns[6].HeaderText = "Sorzám";
                Előírt_Szerelvénylista.Columns[6].Width = 70;
                Előírt_Szerelvénylista.Columns[6].Visible = false;
                Előírt_Szerelvénylista.Columns[7].HeaderText = "Szerelvény";
                Előírt_Szerelvénylista.Columns[7].Width = 100;
                Előírt_Szerelvénylista.Columns[7].Visible = false;
                Előírt_Szerelvénylista.Columns[8].HeaderText = "E2 vizsgálati napja";
                Előírt_Szerelvénylista.Columns[8].Width = 150;


                foreach (Adat_Szerelvény adat in Elő_Szer_Adatok)
                {
                    Előírt_Szerelvénylista.RowCount++;
                    int i = Előírt_Szerelvénylista.RowCount - 1;
                    Előírt_Szerelvénylista.Rows[i].Cells[0].Value = adat.Kocsi1 == "_" ? "" : adat.Kocsi1;
                    Előírt_Szerelvénylista.Rows[i].Cells[0].Style.BackColor = Milyenszínű(adat.Kocsi1);
                    Előírt_Szerelvénylista.Rows[i].Cells[1].Value = adat.Kocsi2 == "_" ? "" : adat.Kocsi2;
                    Előírt_Szerelvénylista.Rows[i].Cells[1].Style.BackColor = Milyenszínű(adat.Kocsi2);
                    Előírt_Szerelvénylista.Rows[i].Cells[2].Value = adat.Kocsi3 == "_" ? "" : adat.Kocsi3;
                    Előírt_Szerelvénylista.Rows[i].Cells[2].Style.BackColor = Milyenszínű(adat.Kocsi3);
                    Előírt_Szerelvénylista.Rows[i].Cells[3].Value = adat.Kocsi4 == "_" ? "" : adat.Kocsi4;
                    Előírt_Szerelvénylista.Rows[i].Cells[3].Style.BackColor = Milyenszínű(adat.Kocsi4);
                    Előírt_Szerelvénylista.Rows[i].Cells[4].Value = adat.Kocsi5 == "_" ? "" : adat.Kocsi5;
                    Előírt_Szerelvénylista.Rows[i].Cells[4].Style.BackColor = Milyenszínű(adat.Kocsi5);
                    Előírt_Szerelvénylista.Rows[i].Cells[5].Value = adat.Kocsi6 == "_" ? "" : adat.Kocsi6;
                    Előírt_Szerelvénylista.Rows[i].Cells[5].Style.BackColor = Milyenszínű(adat.Kocsi6);
                    Előírt_Szerelvénylista.Rows[i].Cells[6].Value = adat.Szerelvény_ID.ToString();
                    Előírt_Szerelvénylista.Rows[i].Cells[7].Value = adat.Szerelvényhossz.ToString();
                    Előírt_Szerelvénylista.Rows[i].Cells[8].Value = "Nincs beállítva";
                    int napos = (from a in AdatokJ2
                                 where a.Azonosító == adat.Kocsi1
                                 select a.Haromnapos).FirstOrDefault();
                    switch (napos)
                    {
                        case 0:
                            {
                                Előírt_Szerelvénylista.Rows[i].Cells[8].Value = "Nincs beállítva";
                                break;
                            }
                        case 1:
                            {
                                Előírt_Szerelvénylista.Rows[i].Cells[8].Value = "Hétfő- Csütörtök";
                                break;
                            }
                        case 2:
                            {
                                Előírt_Szerelvénylista.Rows[i].Cells[8].Value = "Kedd- Péntek";
                                break;
                            }
                        case 3:
                            {
                                Előírt_Szerelvénylista.Rows[i].Cells[8].Value = "Szerda- Szombat";
                                break;
                            }
                        default:
                            {
                                Előírt_Szerelvénylista.Rows[i].Cells[8].Value = "Nincs beállítva";
                                break;
                            }
                    }
                }
                Előírt_Szerelvénylista.Visible = true;
                Előírt_Szerelvénylista.Refresh();
                Előírt_Szerelvénylista.ClearSelection();
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

        private void Előírt_pályaszám_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Előírt_pályaszám.Text.Trim() == "") return;

                AdatokJ2 = KézJármű2.Lista_Adatok(Cmbtelephely.Text.Trim());
                int napos = (from a in AdatokJ2
                             where a.Azonosító == Előírt_pályaszám.Text.Trim()
                             select a.Haromnapos).FirstOrDefault();

                switch (napos)
                {
                    case 1:
                        E2_1.Checked = true;
                        break;
                    case 2:
                        E2_2.Checked = true;
                        break;
                    case 3:
                        E2_3.Checked = true;
                        break;
                    default:
                        E2_0.Checked = true;
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

        private void Előírt_Felsőtábla()
        {
            // Újszerelvény.Visible = False
            Előírt_Szerelvénytáblasor.Rows.Clear();
            Előírt_Szerelvénytáblasor.Columns.Clear();
            Előírt_Szerelvénytáblasor.Refresh();
            Előírt_Szerelvénytáblasor.Visible = false;
            Előírt_Szerelvénytáblasor.ColumnCount = 8;
            Előírt_Szerelvénytáblasor.RowCount = 1;
            Előírt_Szerelvénytáblasor.Columns[0].HeaderText = "Kocsi 1";
            Előírt_Szerelvénytáblasor.Columns[0].Width = 60;
            Előírt_Szerelvénytáblasor.Columns[1].HeaderText = "Kocsi 2";
            Előírt_Szerelvénytáblasor.Columns[1].Width = 60;
            Előírt_Szerelvénytáblasor.Columns[2].HeaderText = "Kocsi 3";
            Előírt_Szerelvénytáblasor.Columns[2].Width = 60;
            Előírt_Szerelvénytáblasor.Columns[3].HeaderText = "Kocsi 4";
            Előírt_Szerelvénytáblasor.Columns[3].Width = 60;
            Előírt_Szerelvénytáblasor.Columns[4].HeaderText = "Kocsi 5";
            Előírt_Szerelvénytáblasor.Columns[4].Width = 60;
            Előírt_Szerelvénytáblasor.Columns[5].HeaderText = "Kocsi 6";
            Előírt_Szerelvénytáblasor.Columns[5].Width = 60;
            Előírt_Szerelvénytáblasor.Columns[6].HeaderText = "Szerelvény";
            Előírt_Szerelvénytáblasor.Columns[6].Width = 60;
            Előírt_Szerelvénytáblasor.Columns[6].Visible = false;
            Előírt_Szerelvénytáblasor.Columns[7].HeaderText = "Szerelvény";
            Előírt_Szerelvénytáblasor.Columns[7].Width = 60;
            Előírt_Szerelvénytáblasor.Columns[7].Visible = false;
            Előírt_Szerelvénytáblasor.Visible = true;
            Előírt_Szerelvénytáblasor.ClearSelection();
            Előírt_Szerelvénytáblasor.Refresh();

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Képernyő_frissítés_Előírt();
        }

        private void Előírt_Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Előírt_Szerelvénylista.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"ElőírtSzerelvémyek_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmm}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyE.DataGridViewToExcel(fájlexc, Előírt_Szerelvénylista);
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

        private void Előírt_hozzáad_Click(object sender, EventArgs e)
        {
            try
            {
                if (Előírt_pályaszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve egy jármű sem.");
                if (ESZAdat == null || ESZAdat.Szerelvényhossz >= 6) throw new HibásBevittAdat("Több járművet nem lehet a szerelvényhez adni!");

                // megnézzük, hogy létezik-e kocsi       
                AdatokJár = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                Elő_Szer_Adatok = KézSzerelvény.Lista_Adatok(Cmbtelephely.Text.Trim(), true);

                Adat_Jármű Szűrt = (from a in AdatokJár
                                    where a.Azonosító == Előírt_pályaszám.Text.Trim()
                                    select a).FirstOrDefault();

                if (Szűrt == null)
                {
                    Előírt_pályaszám.Focus();
                    throw new HibásBevittAdat("A telephelyen nincs ilyen jármű!");
                }
                Adat_Szerelvény Ideig = (from a in Elő_Szer_Adatok
                                         where a.Kocsi1 == Előírt_pályaszám.Text.Trim() || a.Kocsi2 == Előírt_pályaszám.Text.Trim() || a.Kocsi3 == Előírt_pályaszám.Text.Trim() ||
                                               a.Kocsi4 == Előírt_pályaszám.Text.Trim() || a.Kocsi5 == Előírt_pályaszám.Text.Trim() || a.Kocsi6 == Előírt_pályaszám.Text.Trim()
                                         select a).FirstOrDefault();
                //Leellenőrizzük, hogy nincs-e másik szerelvényben
                if (Ideig != null)
                {
                    KözösKereső(Előírt_Szerelvénylista, Előírt_pályaszám.Text.Trim());
                    throw new HibásBevittAdat("A jármű már egy másik szerelvényben van! Beépítéshez előbb ki kell építeni.");
                }

                //új szerelvényszám
                long Szerelvény_ID = ESZAdat.Szerelvény_ID;
                if (Szerelvény_ID == 0)
                    if (Elő_Szer_Adatok.Count == 0)
                        Szerelvény_ID = 1;
                    else
                        Szerelvény_ID = Elő_Szer_Adatok.Max(a => a.Szerelvény_ID) + 1;


                long Szerelvény_hossz = ESZAdat.Szerelvényhossz + 1;

                //Hozzáadjuk az első kocsinak, majd rendezzük
                string[] kocsik = new string[] { Előírt_pályaszám.Text.Trim(), ESZAdat.Kocsi1, ESZAdat.Kocsi2, ESZAdat.Kocsi3, ESZAdat.Kocsi4, ESZAdat.Kocsi5 };
                Kocsi_rendező(kocsik, "_");

                Adat_Szerelvény Adat = new Adat_Szerelvény(Szerelvény_ID, Szerelvény_hossz, kocsik[0], kocsik[1], kocsik[2], kocsik[3], kocsik[4], kocsik[5]);

                // Rögzítjük, vagy módosítjuk
                if (ESZAdat.Szerelvény_ID == 0)
                    KézSzerelvény.Rögzítés(Cmbtelephely.Text.Trim(), Adat, true);
                else
                    KézSzerelvény.Módosítás(Cmbtelephely.Text.Trim(), Adat, true);

                //Ha E2-t manuálisan állítjuk be
                if (Osztás) E2_rögzítés_Eljárás();

                //Alapértéket beállítjuk 
                ESZAdat = new Adat_Szerelvény(Szerelvény_ID, Szerelvény_hossz, kocsik[0], kocsik[1], kocsik[2], kocsik[3], kocsik[4], kocsik[5]);

                Képernyő_frissítés_Előírt_rész();
                E_Szerelvénytáblasor_kiírása(ESZAdat);

                Előírt_pályaszám.Text = "";
                Előírt_pályaszám.Focus();

                this.AcceptButton = Hozzáad;
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

        private void Képernyő_frissítés_Előírt_rész()
        {
            Előírt_pályaszámok_listázása();
            Előírt_Szerelvénytábla_listázás();
        }

        private void Képernyő_frissítés_Előírt()
        {
            TípuCombo_Listáz();
            Előírt_pályaszámok_listázása();
            AdatokJár = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
            Előírt_Szerelvénytábla_listázás();
            Előírt_Felsőtábla();

            Előírt_pályaszám.Text = "";
            Előírt_pályaszám.Focus();

            ESZAdat = new Adat_Szerelvény(0, 0, "0", "0", "0", "0", "0", "0");

        }

        private void Előírt_Combo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Előírt_pályaszámok_listázása();
        }

        private void Előírt_pályaszámok_listázása()
        {
            try
            {
                Előírt_pályaszám.Items.Clear();
                Elő_Szer_Adatok = KézSzerelvény.Lista_Adatok(Cmbtelephely.Text.Trim(), true);
                AdatokJár = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());

                List<Adat_Jármű> Típus = (from a in AdatokJár
                                          where a.Típus == Előírt_Combo1.Text.Trim()
                                          select a).ToList();

                foreach (Adat_Jármű rekord in Típus)
                {
                    long ID = (from a in Elő_Szer_Adatok
                               where a.Kocsi1 == rekord.Azonosító || a.Kocsi2 == rekord.Azonosító || a.Kocsi3 == rekord.Azonosító ||
                                     a.Kocsi4 == rekord.Azonosító || a.Kocsi5 == rekord.Azonosító || a.Kocsi6 == rekord.Azonosító
                               select a.Szerelvény_ID).FirstOrDefault();
                    if (ID == 0)
                        Előírt_pályaszám.Items.Add(rekord.Azonosító);
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

        private void Előírt_Egyszerelvénymnusz_Click(object sender, EventArgs e)
        {
            try
            {
                if (Melyik_azonosító == "0") throw new HibásBevittAdat("Nincs kijelölve egy jármű sem.");

                if (Előírt_Szerelvénytáblasor.Rows[0].Cells[7].Value.ToÉrt_Int() == 1)
                {
                    Előírt_Törli_szerelvényt();
                    return;
                }

                // kicseréljük _-ra a kiválasztott kocsit
                string[] kocsik = new string[] { ESZAdat.Kocsi1, ESZAdat.Kocsi2, ESZAdat.Kocsi3, ESZAdat.Kocsi4, ESZAdat.Kocsi5, ESZAdat.Kocsi6 };
                for (int i = 0; i < kocsik.Length; i++)
                {
                    if (kocsik[i] == Melyik_azonosító.Trim())
                        kocsik[i] = "_";
                }

                Kocsi_rendező(kocsik, "_");
                long Szerelvény_hossz = szAdat.Szerelvényhossz - 1;

                Adat_Szerelvény ADAT = new Adat_Szerelvény(ESZAdat.Szerelvény_ID, Szerelvény_hossz, kocsik[0], kocsik[1], kocsik[2], kocsik[3], kocsik[4], kocsik[5]);
                KézSzerelvény.Módosítás(Cmbtelephely.Text.Trim(), ADAT, true);

                E_Szerelvénytáblasor_kiírása(ADAT);
                Képernyő_frissítés_Előírt_rész();

                Előírt_pályaszám.Text = "";
                Előírt_pályaszám.Focus();
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

        private void Előírt_szerelvénytörlés_Click(object sender, EventArgs e)
        {
            Előírt_Törli_szerelvényt();
        }

        private void Előírt_Törli_szerelvényt()
        {
            try
            {
                if (Előírt_Szerelvénytáblasor.Rows[0].Cells[6].Value == null || Előírt_Szerelvénytáblasor.Rows[0].Cells[6].Value.ToStrTrim() == "")
                    throw new HibásBevittAdat("Nincs kijelölve egy szerelvény sem.");
                if (!long.TryParse(Előírt_Szerelvénytáblasor.Rows[0].Cells[6].Value.ToStrTrim(), out long Id)) throw new HibásBevittAdat("Nincs kijelölve egy szerelvény sem.");

                KézSzerelvény.Törlés(Cmbtelephely.Text.Trim(), Id, true);
                Képernyő_frissítés_Előírt();

                Előírt_pályaszám.Text = "";
                Előírt_pályaszám.Focus();
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

        private void Előírt_Újszerelvény_Click(object sender, EventArgs e)
        {
            try
            {
                Képernyő_frissítés_Előírt();
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Előírt_Szerelvénylista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    long szerelvényID;
                    if (Előírt_Szerelvénylista.Rows[e.RowIndex].Cells[6].Value == null || Előírt_Szerelvénylista.Rows[e.RowIndex].Cells[6].Value.ToStrTrim() == "")
                        szerelvényID = 0;
                    else
                        szerelvényID = long.Parse(Előírt_Szerelvénylista.Rows[e.RowIndex].Cells[6].Value.ToStrTrim());

                    switch (Előírt_Szerelvénylista.Rows[e.RowIndex].Cells[8].Value.ToStrTrim())
                    {
                        case "Nincs beállítva":
                            {
                                E2_0.Checked = true;
                                break;
                            }
                        case "Hétfő- Csütörtök":
                            {
                                E2_1.Checked = true;
                                break;
                            }
                        case "Kedd- Péntek":
                            {
                                E2_2.Checked = true;
                                break;
                            }
                        case "Szerda- Szombat":
                            {
                                E2_3.Checked = true;
                                break;
                            }
                    }
                    List<Adat_Szerelvény> AdatokSzerElő = KézSzerelvény.Lista_Adatok(Cmbtelephely.Text.Trim(), true);
                    ESZAdat = AdatokSzerElő.FirstOrDefault(a => a.Szerelvény_ID == szerelvényID);
                    if (ESZAdat != null) E_Szerelvénytáblasor_kiírása(ESZAdat);
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

        private void E_Szerelvénytáblasor_kiírása(Adat_Szerelvény Adat)
        {

            Előírt_Szerelvénytáblasor.Rows[0].Cells[0].Value = Adat.Kocsi1 == "_" ? "" : Adat.Kocsi1;
            Előírt_Szerelvénytáblasor.Rows[0].Cells[1].Value = Adat.Kocsi2 == "_" ? "" : Adat.Kocsi2;
            Előírt_Szerelvénytáblasor.Rows[0].Cells[2].Value = Adat.Kocsi3 == "_" ? "" : Adat.Kocsi3;
            Előírt_Szerelvénytáblasor.Rows[0].Cells[3].Value = Adat.Kocsi4 == "_" ? "" : Adat.Kocsi4;
            Előírt_Szerelvénytáblasor.Rows[0].Cells[4].Value = Adat.Kocsi5 == "_" ? "" : Adat.Kocsi5;
            Előírt_Szerelvénytáblasor.Rows[0].Cells[5].Value = Adat.Kocsi6 == "_" ? "" : Adat.Kocsi6;

            Előírt_Szerelvénytáblasor.Rows[0].Cells[6].Value = Adat.Szerelvény_ID;
            Előírt_Szerelvénytáblasor.Rows[0].Cells[7].Value = Adat.Szerelvényhossz;
        }

        private void Előírt_Szerelvénytáblasor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                long szerelvényID;
                if (Előírt_Szerelvénytáblasor.Rows[0].Cells[6].Value == null || Előírt_Szerelvénytáblasor.Rows[0].Cells[6].Value.ToStrTrim() == "")
                    szerelvényID = 0;
                else
                    szerelvényID = Előírt_Szerelvénytáblasor.Rows[0].Cells[6].Value.ToÉrt_Long();

                long szerelvényhossz;
                if (Előírt_Szerelvénytáblasor.Rows[0].Cells[7].Value == null || Előírt_Szerelvénytáblasor.Rows[0].Cells[7].Value.ToStrTrim() == "")
                    szerelvényhossz = 0;
                else
                    szerelvényhossz = Előírt_Szerelvénytáblasor.Rows[0].Cells[7].Value.ToÉrt_Long();
                string[] kocsik = new string[6];
                if (!(Előírt_Szerelvénytáblasor.Rows[0].Cells[0].Value == null || Előírt_Szerelvénytáblasor.Rows[0].Cells[0].Value.ToStrTrim() == ""))
                    kocsik[0] = Előírt_Szerelvénytáblasor.Rows[0].Cells[0].Value.ToStrTrim();
                if (!(Előírt_Szerelvénytáblasor.Rows[0].Cells[1].Value == null || Előírt_Szerelvénytáblasor.Rows[0].Cells[1].Value.ToStrTrim() == ""))
                    kocsik[1] = Előírt_Szerelvénytáblasor.Rows[0].Cells[1].Value.ToStrTrim();
                if (!(Előírt_Szerelvénytáblasor.Rows[0].Cells[2].Value == null || Előírt_Szerelvénytáblasor.Rows[0].Cells[2].Value.ToStrTrim() == ""))
                    kocsik[2] = Előírt_Szerelvénytáblasor.Rows[0].Cells[2].Value.ToStrTrim();
                if (!(Előírt_Szerelvénytáblasor.Rows[0].Cells[3].Value == null || Előírt_Szerelvénytáblasor.Rows[0].Cells[3].Value.ToStrTrim() == ""))
                    kocsik[3] = Előírt_Szerelvénytáblasor.Rows[0].Cells[3].Value.ToStrTrim();
                if (!(Előírt_Szerelvénytáblasor.Rows[0].Cells[4].Value == null || Előírt_Szerelvénytáblasor.Rows[0].Cells[4].Value.ToStrTrim() == ""))
                    kocsik[4] = Előírt_Szerelvénytáblasor.Rows[0].Cells[4].Value.ToStrTrim();
                if (!(Előírt_Szerelvénytáblasor.Rows[0].Cells[5].Value == null || Előírt_Szerelvénytáblasor.Rows[0].Cells[5].Value.ToStrTrim() == ""))
                    kocsik[5] = Előírt_Szerelvénytáblasor.Rows[0].Cells[5].Value.ToStrTrim();

                szAdat = new Adat_Szerelvény(szerelvényID, szerelvényhossz, kocsik[0], kocsik[1], kocsik[2], kocsik[3], kocsik[4], kocsik[5]);

                if (Előírt_Szerelvénytáblasor.Rows[0].Cells[e.ColumnIndex].Value.ToStrTrim() == "")
                    Melyik_azonosító = "0";
                else
                    Melyik_azonosító = Előírt_Szerelvénytáblasor.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToStrTrim();
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

        private void E2_rögzítés_Eljárás()
        {
            try
            {
                // rögzítjük E2 vizsgálatot
                AdatokJ2 = KézJármű2.Lista_Adatok(Cmbtelephely.Text.Trim());

                List<Adat_Jármű_2> Módosít = new List<Adat_Jármű_2>();
                List<Adat_Jármű_2> Rögzít = new List<Adat_Jármű_2>();
                for (int i = 0; i < 6; i++)
                {
                    string pályaszám = Előírt_Szerelvénytáblasor.Rows[0].Cells[i].Value.ToStrTrim();
                    if (pályaszám != "")
                    {
                        Adat_Jármű_2 Elem = (from a in AdatokJ2
                                             where a.Azonosító == pályaszám
                                             select a).FirstOrDefault();

                        int Érték;
                        if (E2_0.Checked)
                            Érték = 0;
                        else if (E2_1.Checked)
                            Érték = 1;
                        else if (E2_2.Checked)
                            Érték = 2;
                        else
                            Érték = 3;
                        Adat_Jármű_2 Adat = new Adat_Jármű_2(pályaszám, new DateTime(1900, 1, 1), Érték);
                        if (Elem != null)
                            Módosít.Add(Adat);
                        else
                            Rögzít.Add(Adat);
                    }
                }
                if (Módosít.Count > 0) KézJármű2.Módosítás(Cmbtelephely.Text.Trim(), Módosít);
                if (Rögzít.Count > 0) KézJármű2.Rögzítés(Cmbtelephely.Text.Trim(), Rögzít);
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

        private void E2_rögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                E2_rögzítés_Eljárás();
                Előírt_Szerelvénytábla_listázás();
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void E2_Törlés_Click(object sender, EventArgs e)
        {
            try
            {
                AdatokJ2 = KézJármű2.Lista_Adatok(Cmbtelephely.Text.Trim());
                Holtart.Be(AdatokJ2.Count + 1);
                List<Adat_Jármű_2> AdatokGy = new List<Adat_Jármű_2>();
                foreach (Adat_Jármű_2 rekord in AdatokJ2)
                {
                    Adat_Jármű_2 ADAT = new Adat_Jármű_2(rekord.Azonosító, new DateTime(1900, 1, 1), 0);
                    AdatokGy.Add(ADAT);
                    Holtart.Lép();
                }
                KézJármű2.Módosítás(Cmbtelephely.Text.Trim(), AdatokGy);
                Holtart.Ki();
                Előírt_Szerelvénytábla_listázás();
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


        #region Napló
        private void Tábla_napló_listázás()
        {
            try
            {
                List<Adat_Szerelvény_Napló> Adatok = KézSzerNapló.Lista_Adatok(Cmbtelephely.Text.Trim(), DátumNapló.Value);
                DataTable AdatTábla = new DataTable();

                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Kocsi 1");
                AdatTábla.Columns.Add("Kocsi 2");
                AdatTábla.Columns.Add("Kocsi 3");
                AdatTábla.Columns.Add("Kocsi 4");
                AdatTábla.Columns.Add("Kocsi 5");
                AdatTábla.Columns.Add("Kocsi 6");
                AdatTábla.Columns.Add("Sorzám");
                AdatTábla.Columns.Add("Szerelvény");
                AdatTábla.Columns.Add("Módosító");
                AdatTábla.Columns.Add("Mikor");

                AdatTábla.Clear();
                foreach (Adat_Szerelvény_Napló rekord in Adatok)
                {
                    DataRow Soradat = AdatTábla.NewRow();

                    Soradat["Kocsi 1"] = rekord.Kocsi1;
                    Soradat["Kocsi 2"] = rekord.Kocsi2;
                    Soradat["Kocsi 3"] = rekord.Kocsi3;
                    Soradat["Kocsi 4"] = rekord.Kocsi4;
                    Soradat["Kocsi 5"] = rekord.Kocsi5;
                    Soradat["Kocsi 6"] = rekord.Kocsi6;
                    Soradat["Sorzám"] = rekord.ID;
                    Soradat["Szerelvény"] = rekord.Szerelvényhossz;
                    Soradat["Módosító"] = rekord.Módosító;
                    Soradat["Mikor"] = rekord.Mikor;

                    AdatTábla.Rows.Add(Soradat);
                }

                Tábla_napló.DataSource = AdatTábla;

                Tábla_napló.Columns["Kocsi 1"].Width = 100;
                Tábla_napló.Columns["Kocsi 2"].Width = 100;
                Tábla_napló.Columns["Kocsi 3"].Width = 100;
                Tábla_napló.Columns["Kocsi 4"].Width = 100;
                Tábla_napló.Columns["Kocsi 5"].Width = 100;
                Tábla_napló.Columns["Kocsi 6"].Width = 100;
                Tábla_napló.Columns["Sorzám"].Width = 70;
                Tábla_napló.Columns["Szerelvény"].Width = 100;
                Tábla_napló.Columns["Módosító"].Width = 100;
                Tábla_napló.Columns["Mikor"].Width = 200;

                Tábla_napló.Visible = true;
                Tábla_napló.Refresh();
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

        private void Napló_Frissít_Click(object sender, EventArgs e)
        {
            Tábla_napló_listázás();
        }

        private void Napló_Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla_napló.Rows.Count <= 0) return;

                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Szerelvémyek_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddhhmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyE.DataGridViewToExcel(fájlexc, Tábla_napló);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MyE.Megnyitás(fájlexc);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void DátumNapló_ValueChanged(object sender, EventArgs e)
        {
            Tábla_napló_listázás();
        }
        #endregion


        #region Utasítás
        private void Utasítás_tervezet_Click(object sender, EventArgs e)
        {
            Tervezet_utasítás();
        }

        private void Tervezet_utasítás()
        {
            try
            {
                // ****************************************
                // **  Előírt szerelvény lista           **
                // ****************************************
                Txtírásimező.Text = "";
                Btnrögzítés.Visible = true;
                Txtírásimező.Text = "Előírt szerelvény lista 20  -től \r\n\r\n";

                Elő_Szer_Adatok = KézSzerelvény.Lista_Adatok(Cmbtelephely.Text.Trim(), true);
                foreach (Adat_Szerelvény adat in Elő_Szer_Adatok)
                {

                    Txtírásimező.Text += adat.Kocsi1 == "_" ? "" : adat.Kocsi1;
                    Txtírásimező.Text += adat.Kocsi2 == "_" ? "" : "-" + adat.Kocsi2;
                    Txtírásimező.Text += adat.Kocsi3 == "_" ? "" : "-" + adat.Kocsi3;
                    Txtírásimező.Text += adat.Kocsi4 == "_" ? "" : "-" + adat.Kocsi4;
                    Txtírásimező.Text += adat.Kocsi5 == "_" ? "" : "-" + adat.Kocsi5;
                    Txtírásimező.Text += adat.Kocsi6 == "_" ? "" : "-" + adat.Kocsi6;
                    Txtírásimező.Text += "\r\n";
                }
                Txtírásimező.Text += "\r\n";
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

        private void Btnrögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Txtírásimező.Text.Trim() == "") return;
                // megtisztítjuk a szöveget
                Txtírásimező.Text = MyF.Szöveg_Tisztítás(Txtírásimező.Text);
                //Utasírás rögzítés és olvasás rögzítés
                double Sorszám = KézUtasítás.Új_utasítás(Cmbtelephely.Text.Trim(), DateTime.Now.Year, Txtírásimező.Text.Trim());

                MessageBox.Show($"Az utasítás rögzítése {Sorszám} szám alatt megtörtént!", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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