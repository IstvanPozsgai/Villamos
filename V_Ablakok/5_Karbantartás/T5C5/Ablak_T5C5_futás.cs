using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    public partial class Ablak_T5C5_futás
    {
        readonly Kezelő_T5C5_Futás Kéz_Futás = new Kezelő_T5C5_Futás();
        readonly Kezelő_T5C5_Futás1 Kéz_Futás1 = new Kezelő_T5C5_Futás1();
        readonly Kezelő_Jármű Kéz_Jármű = new Kezelő_Jármű();
        readonly Kezelő_jármű_hiba KézHiba = new Kezelő_jármű_hiba();
        readonly Kezelő_Főkönyv_ZSER KézZser = new Kezelő_Főkönyv_ZSER();
        readonly Kezelő_Főkönyv_Nap KézFőkönyv = new Kezelő_Főkönyv_Nap();
        readonly Kezelő_T5C5_Havi_Nap KézHavi = new Kezelő_T5C5_Havi_Nap();
        readonly Kezelő_T5C5_Göngyöl_DátumTábla KézGöngyölDátum = new Kezelő_T5C5_Göngyöl_DátumTábla();
        readonly Kezelő_T5C5_Göngyöl Kéz_Göngyöl = new Kezelő_T5C5_Göngyöl();

        bool CTRL_le = false;
        private string FájlExcel_;
        private int Gombok_száma = 0;
        private int Utolsó_Gomb = 0;
        string GombNév = "";
        string Gombfelirat = "";

        List<string> Pályaszám = new List<string>();
        List<Adat_T5C5_Göngyöl_DátumTábla> AdatokGöngyöl = new List<Adat_T5C5_Göngyöl_DátumTábla>();
        List<Adat_T5C5_Futás> AdatokFutás = new List<Adat_T5C5_Futás>();

        #region Alap
        public Ablak_T5C5_futás()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            Telephelyekfeltöltése();
            Jogosultságkiosztás();
            Pályaszám = Pályaszám_feltöltés();
            Dátum.MaxDate = DateTime.Today;
            Dátum.Value = DateTime.Today;
        }

        private void Ablak_T5C5_futás_Load(object sender, EventArgs e)
        {
            Combo_feltöltés();
            Táblaellenőrzés();
            Gombok_vezérlése();
        }

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    Cmbtelephely.Items.Add(Elem);
                if (Program.PostásTelephely == "Főmérnökség" || Program.PostásTelephely.Contains("törzs"))
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

        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;

                // ide kell az összes gombot tenni amit szabályozni akarunk false
                Napadatai.Enabled = false;
                Zserbeolvasás.Enabled = false;
                Zseradategyeztetés.Enabled = false;
                Rögzít.Enabled = false;



                melyikelem = 101;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Napadatai.Enabled = true;
                    Zserbeolvasás.Enabled = true;
                    Zseradategyeztetés.Enabled = true;
                    Rögzít.Enabled = true;
                }

                // módosítás 2 
                if (MyF.Vanjoga(melyikelem, 2))
                { }

                // módosítás 3
                if (MyF.Vanjoga(melyikelem, 3))
                { }
            }
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
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\T5C5_futás.html";
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

        private void Cmbtelephely_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Pályaszám.Clear();
                Pályaszám = Pályaszám_feltöltés();
                if (Gombok_száma != 0)
                {
                    // ha nem nulla akkor előbb a gombokat le kell szedni
                    Panel3.Controls.Clear();
                    Gombok_száma = 0;
                }
                Bevitelilap.Visible = false;
                Gombok_vezérlése();
            }
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

        private void Ablak_T5C5_futás_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC
            if ((int)e.KeyCode == 27)
            {
                Bevitelilap.Visible = false;
            }
            // F1 utolsó gombeltávolítás visszavonása
            if ((int)e.KeyCode == 112)
            {
                if (Utolsó_Gomb != 0)
                {
                    Panel3.Controls[Utolsó_Gomb].Visible = true;
                    Utolsó_Gomb = 0;
                }
            }
            if (e.Control)
                CTRL_le = true;
        }
        #endregion


        #region Segédablak
        private void Combo_feltöltés()
        {
            // combo feltöltése adatokkal
            Kategória.Items.Clear();
            Kategória.Items.Add("Forgalomban");
            Kategória.Items.Add("Hibás");
            Kategória.Items.Add("E3");
            Kategória.Items.Add("V1");
            Kategória.Items.Add("V2");
            Kategória.Items.Add("V3");
            Kategória.Items.Add("J1");
            Kategória.Items.Add("-");
            Kategória.Refresh();
        }
        #endregion


        #region háttér folyamatok
        private void Táblaellenőrzés()
        {
            try
            {
                //előző hónap ha van akkor kiolvassa a az utolsó rögzített napot, ha nincs akkor az előző hónap utolsó napja
                DateTime Előzőhónap = Dátum.Value.AddMonths(-1);
                if (Előzőhónap.Month == Dátum.Value.Month) return;

                List<Adat_T5C5_Havi_Nap> Fut_Adatok = KézHavi.Lista_Adatok(Előzőhónap);

                //megnézzük, hogy mi az utolsó rögzített adat
                Holtart.Be(Pályaszám.Count + 1);


                List<Adat_T5C5_Havi_Nap> AdatokGy = new List<Adat_T5C5_Havi_Nap>();
                foreach (string elem in Pályaszám)
                {
                    Adat_T5C5_Havi_Nap Rekord = (from a in Fut_Adatok
                                                 where a.Azonosító == elem.Trim()
                                                 select a).FirstOrDefault();
                    if (Rekord != null)
                    {
                        Adat_T5C5_Havi_Nap ADAT = new Adat_T5C5_Havi_Nap(elem,
                                                                         ".", ".", ".", ".", ".", ".", ".", ".", ".", ".",
                                                                         ".", ".", ".", ".", ".", ".", ".", ".", ".", ".",
                                                                         ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".",
                                                                         Rekord.Futásnap,
                                                                         Cmbtelephely.Text.Trim());
                        AdatokGy.Add(ADAT);
                    }
                    Holtart.Lép();

                }
                KézHavi.Rögzítés(Dátum.Value, AdatokGy);
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

        private void Dátum_ValueChanged(object sender, EventArgs e)
        {
            try
            {

                if (Gombok_száma != 0)
                {
                    // ha nem nulla akkor előbb a gombokat le kell szedni
                    Panel3.Controls.Clear();
                    Gombok_száma = 0;
                }
                Bevitelilap.Visible = false;
                Gombok_vezérlése();
            }
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

        private void Gombok_vezérlése()
        {
            try
            {
                List<Adat_T5C5_Futás> Adatok = Kéz_Futás.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value);
                if (Adatok == null || Adatok.Count == 0)
                {
                    Lista.Visible = false;
                    Napkinyitása.Visible = false;
                    Zseradategyeztetés.Visible = false;
                    Zserbeolvasás.Visible = false;
                    Naplezárása.Visible = false;
                    Göngyölés.Visible = false;
                    Vissza.Visible = false;
                    Napadatai.Visible = true;
                }
                else
                {
                    Lista.Visible = true;
                    Napkinyitása.Visible = false;
                    Zseradategyeztetés.Visible = true;
                    Zserbeolvasás.Visible = true;
                    Naplezárása.Visible = true;
                    Napadatai.Visible = true;
                    // ha létezik,akkor megnézzük, hogy le van-e zárva
                    List<Adat_T5C5_Futás1> ListaSt = Kéz_Futás1.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value).ToList();
                    Adat_T5C5_Futás1 Elem = (from a in ListaSt select a).FirstOrDefault();
                    if (Elem.Státus == 1)
                    {
                        // minden gomb inaktív kivéve visszanyit
                        Napadatai.Visible = false;
                        Zseradategyeztetés.Visible = false;
                        Zserbeolvasás.Visible = false;
                        Naplezárása.Visible = false;
                        Napkinyitása.Visible = true;
                        Göngyölés.Visible = true;
                        Vissza.Visible = true;
                    }
                    else
                    {
                        Napadatai.Visible = true;
                        Zseradategyeztetés.Visible = true;
                        Zserbeolvasás.Visible = true;
                        Naplezárása.Visible = true;
                        Napkinyitása.Visible = false;
                        Göngyölés.Visible = false;
                        Vissza.Visible = false;
                    }
                    // gönygyölés csak akkor aktív ha a soron következő nap
                    AdatokGöngyöl = KézGöngyölDátum.Lista_Adatok("Főmérnökség", DateTime.Today);

                    Adat_T5C5_Göngyöl_DátumTábla EgyTelep = (from a in AdatokGöngyöl
                                                             where a.Telephely == Cmbtelephely.Text.Trim()
                                                             select a).FirstOrDefault();

                    if (EgyTelep != null)
                    {
                        // 'ha van
                        if (EgyTelep.Utolsórögzítés.AddDays(1) == Dátum.Value)
                        {
                            Göngyölés.Enabled = true;
                            Napkinyitása.Enabled = true;
                            Göngyölés.BackColor = Color.Green;
                            Napkinyitása.BackColor = Color.Green;
                        }
                        else
                        {
                            Göngyölés.Enabled = false;
                            Napkinyitása.Enabled = false;
                            Göngyölés.BackColor = Color.Red;
                            Napkinyitása.BackColor = Color.Red;
                        }

                        if (EgyTelep.Utolsórögzítés == Dátum.Value)
                        {
                            Vissza.Enabled = true;
                            Vissza.BackColor = Color.Green;
                        }
                        else
                        {
                            Vissza.Enabled = false;
                            Vissza.BackColor = Color.Red;
                        }

                    }
                    else
                    {
                        Göngyölés.Enabled = false;
                        Göngyölés.BackColor = Color.Red;
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


        #region Adat olvasó Gombok
        private void Lista_Click(object sender, EventArgs e)
        {
            Kocsikiirása();
        }

        private void Kocsikiirása()
        {
            try
            {
                Panel3.Controls.Clear();
                // ha nem nulla akkor előbb a gombokat le kell szedni
                if (Gombok_száma != 0) Gombok_száma = 0;
                int darab = 0;

                List<Adat_T5C5_Futás> Adatok = Kéz_Futás.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value);
                if (Adatok == null || Adatok.Count == 0) Napadatai_eseménye();

                int i = 1;
                int j = 1;
                int k = 1;


                AdatokFutás = Kéz_Futás.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value);

                if (AdatokFutás.Count > 0)
                {
                    foreach (Adat_T5C5_Futás rekord in AdatokFutás)
                    {
                        Button Telephelygomb = new Button()
                        {
                            Location = new Point(10 + 80 * (k - 1), 10 + 40 * (j - 1)),
                            Size = new Size(70, 30),
                            Name = "Kocsi_" + (darab + 1),
                            Text = rekord.Azonosító.Trim()
                        };


                        switch (rekord.Futásstátus.Trim())
                        {
                            case "Forgalomban":
                                {
                                    // zöld
                                    Telephelygomb.BackColor = Color.LimeGreen;
                                    break;
                                }
                            case "-":
                                {
                                    // szürke
                                    Telephelygomb.BackColor = Color.Silver;
                                    break;
                                }
                            case "_":
                                {
                                    // szürke
                                    Telephelygomb.BackColor = Color.Silver;
                                    break;
                                }
                            case "E3":
                                {
                                    // kék
                                    Telephelygomb.BackColor = Color.Blue;
                                    Telephelygomb.ForeColor = Color.White;
                                    break;
                                }
                            case "V1":
                                {
                                    // sárga
                                    Telephelygomb.BackColor = Color.Yellow;
                                    break;
                                }
                            case "V2":
                                {
                                    // narancssárga
                                    Telephelygomb.BackColor = Color.DarkOrange;
                                    break;
                                }
                            case "V3":
                                {
                                    // narancssárga
                                    Telephelygomb.BackColor = Color.DarkOrange;
                                    break;
                                }
                            case "J1":
                                {
                                    // narancssárga
                                    Telephelygomb.BackColor = Color.DarkOrange;
                                    break;
                                }
                            case "J2":
                                {
                                    // narancssárga
                                    Telephelygomb.BackColor = Color.DarkOrange;
                                    break;
                                }
                            case "Hibás":
                                {
                                    // piros
                                    Telephelygomb.BackColor = Color.Red;
                                    Telephelygomb.ForeColor = Color.White;
                                    break;
                                }
                        }
                        Telephelygomb.Visible = true;
                        ToolTip1.SetToolTip(Telephelygomb, rekord.Futásstátus.Trim());

                        Telephelygomb.MouseDown += Telephelyre_MouseDown;

                        Panel3.Controls.Add(Telephelygomb);
                        Gombok_száma = i;

                        k += 1;
                        if (k == 11)
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

        private void Napadatai_Click(object sender, EventArgs e)
        {
            Napadatai_eseménye();
            MessageBox.Show("Az adatok rögzítésre kerültek!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Napadatai_eseménye()
        {
            try
            {
                Konvertálás();
                Adatáttöltés();
                Gombok_vezérlése();
                Napkinyitása.Visible = false;
            }
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

        private void Konvertálás()
        {
            try
            {
                List<Adat_T5C5_Futás> AdatokFutás = Kéz_Futás.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value);
                // ha létezik akkor töröljük
                if (AdatokFutás != null && AdatokFutás.Count != 0)
                {
                    if (MessageBox.Show("Már van az adott napra feltöltve adat ! Módosítjuk az adatokat ?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        Kéz_Futás.Törlés(Cmbtelephely.Text.Trim(), Dátum.Value);
                        Kéz_Futás1.Törlés(Cmbtelephely.Text.Trim(), Dátum.Value);
                    }
                    else
                        return;
                }

                // Kitöltjük az aktuális kocsilistával
                List<Adat_Jármű> Adatok = Kéz_Jármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adatok = (from a in Adatok
                          where a.Üzem == Cmbtelephely.Text.Trim()
                          && a.Törölt == false
                          && a.Típus.Contains("T5C5")
                          orderby a.Azonosító
                          select a).ToList();

                List<Adat_Jármű_hiba> HibaAdatok = KézHiba.Lista_Adatok(Cmbtelephely.Text.Trim());

                Holtart.Be(Adatok.Count + 1);

                List<Adat_T5C5_Futás> AdatokGy = new List<Adat_T5C5_Futás>();
                foreach (Adat_Jármű rekord in Adatok)
                {
                    string futásstátus = "-";
                    List<Adat_Jármű_hiba> PályaszámRész = (from a in HibaAdatok
                                                           where a.Azonosító == rekord.Azonosító
                                                           && a.Korlát == 4
                                                           select a).ToList();
                    string hibaszöveg = "";
                    foreach (Adat_Jármű_hiba rekordhiba in PályaszámRész)
                        hibaszöveg += rekordhiba.Hibaleírása.Trim();
                    hibaszöveg = hibaszöveg.ToUpper();

                    if (hibaszöveg.Contains("E3")) futásstátus = "E3";
                    if (hibaszöveg.Contains("V1")) futásstátus = "V1";
                    if (hibaszöveg.Contains("V2")) futásstátus = "V2";
                    if (hibaszöveg.Contains("V3")) futásstátus = "V3";
                    if (hibaszöveg.Contains("J2")) futásstátus = "J2";
                    if (hibaszöveg.Contains("J1")) futásstátus = "J2";
                    if (futásstátus == "-" && hibaszöveg.Trim() != "") futásstátus = "Hibás";

                    Adat_T5C5_Futás ADAT = new Adat_T5C5_Futás(
                                   rekord.Azonosító.Trim(),
                                   Dátum.Value,
                                   futásstátus,
                                   rekord.Státus);
                    AdatokGy.Add(ADAT);
                    Holtart.Lép();
                }
                Kéz_Futás.Rögzítés(Cmbtelephely.Text.Trim(), Dátum.Value, AdatokGy);

                Adat_T5C5_Futás1 ADAT1 = new Adat_T5C5_Futás1(0);
                Kéz_Futás1.Rögzítés(Cmbtelephely.Text.Trim(), Dátum.Value, ADAT1);

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

        private void Adatáttöltés()
        {
            try
            {
                // megnézzük, hogy készült-e az adott nap főkönyv ha készült akkor abból vesszük  át
                //ha van délután akkor abból olvassa be
                List<Adat_Főkönyv_Nap> Adatok = KézFőkönyv.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, "du");
                if (Adatok == null || Adatok.Count == 0) Adatok = KézFőkönyv.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, "de"); //délelőttit is megnézzük
                if (Adatok == null || Adatok.Count == 0) return; //Nincs adat,akkor nem fárasztjuk magunkat.

                Kocsikiirása();
                Panel3.Refresh();

                Holtart.Be(Panel3.Controls.Count + 1);

                List<Adat_T5C5_Futás> AdatokGy = new List<Adat_T5C5_Futás>();
                for (int i = 0; i < Panel3.Controls.Count; i++)
                {
                    Holtart.Lép();
                    Adat_Főkönyv_Nap rekord = (from a in Adatok
                                               where a.Azonosító == Panel3.Controls[i].Text.Trim()
                                               select a).FirstOrDefault();

                    string Futásstátus = "-";
                    long jármű_státus = 0;
                    if (rekord != null)
                    {
                        jármű_státus = rekord.Státus;
                        if (rekord.Státus == 4) Futásstátus = "Hibás";
                        if (rekord.Viszonylat.Trim() != "-") Futásstátus = "Forgalomban";
                        if (rekord.Hibaleírása.ToUpper().Contains("E3")) Futásstátus = "E3";
                        if (rekord.Hibaleírása.ToUpper().Contains("V1")) Futásstátus = "V1";
                        if (rekord.Hibaleírása.ToUpper().Contains("V2")) Futásstátus = "V2";
                        if (rekord.Hibaleírása.ToUpper().Contains("V3")) Futásstátus = "V3";
                        if (rekord.Hibaleírása.ToUpper().Contains("#J")) Futásstátus = "J";
                    }
                    Adat_T5C5_Futás ADAT = new Adat_T5C5_Futás(Panel3.Controls[i].Text.Trim(), Dátum.Value, Futásstátus, jármű_státus);
                    AdatokGy.Add(ADAT);
                    Panel3.Controls[i].Visible = false;
                    Panel3.Refresh();
                }
                Kéz_Futás.Módosítás(Cmbtelephely.Text.Trim(), Dátum.Value, AdatokGy);
                Holtart.Ki();
                MessageBox.Show("Az adat konvertálás befejeződött!", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Kocsikiirása();
            }
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


        #region Adatrögzítés
        private void Telephelyre_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Rögzít.Visible = !Napkinyitása.Visible;
                Bevitelilap.Visible = false;
                GombNév = (sender as Button).Name;
                Gombfelirat = (sender as Button).Text;
                string[] név = GombNév.Split('_');
                Utolsó_Gomb = int.Parse(név[1]) - 1;

                // jobb egér gomb
                if (e.Button == MouseButtons.Right) Panel3.Controls[Utolsó_Gomb].Visible = false;

                // ha bal egérgomb
                if (e.Button == MouseButtons.Left)
                {
                    Bevitelilap.Left = Panel3.Controls[Utolsó_Gomb].Left + Panel3.Left;
                    Bevitelilap.Top = Panel3.Controls[Utolsó_Gomb].Top + Panel3.Top;
                    Label4.Text = Gombfelirat;

                    Adat_T5C5_Futás EgyElem = (from a in AdatokFutás
                                               where a.Azonosító == Gombfelirat
                                               select a).FirstOrDefault();

                    if (EgyElem != null) Kategória.Text = EgyElem.Futásstátus;

                    Bevitelilap.Refresh();
                    Bevitelilap.Visible = true;
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

        private void Rögzít_Click(object sender, EventArgs e)
        {
            if (Kategória.Text.Trim() == "") return;
            if (Label4.Text.Trim() == "") return;

            Adat_T5C5_Futás ADAT = new Adat_T5C5_Futás(Label4.Text.Trim(), Dátum.Value, Kategória.Text.Trim(), 0);
            Kéz_Futás.Módosítás(Cmbtelephely.Text.Trim(), Dátum.Value, ADAT);
            Gombok_Színezése(Utolsó_Gomb, Kategória.Text.Trim());

            Bevitelilap.Visible = false;
        }
        #endregion


        #region Göngyölés gönygyölés
        private void Göngyölés_Click(object sender, EventArgs e)
        {
            try
            {
                AdatokGöngyöl = KézGöngyölDátum.Lista_Adatok("Főmérnökség", DateTime.Today);
                if (AdatokGöngyöl == null) return;
                Adat_T5C5_Göngyöl_DátumTábla Elem = (from a in AdatokGöngyöl
                                                     where a.Zárol == true
                                                     select a).FirstOrDefault();

                if (Elem != null) throw new HibásBevittAdat($"Az adatok göngyölése nem lehetséges, mert {Elem.Telephely} dolgozza fel az adatokat.");
                KézGöngyölDátum.Zárolás("Főmérnökség", DateTime.Today, Cmbtelephely.Text.Trim(), true);


                // a pályaszámokat ellenőrizzük
                Pályaszám_ellenőrzés();

                // A GÖNGYÖLÉS ELŐTTI ÁLLAPOTOT rögzíjük egy napi táblába
                List<Adat_T5C5_Göngyöl> AdatokFő = Kéz_Göngyöl.Lista_Adatok("Főmérnökség", DateTime.Today);
                Kéz_Göngyöl.Rögzítés(Cmbtelephely.Text.Trim(), Dátum.Value.AddDays(-1), AdatokFő);

                Göngyöl();
                MessageBox.Show("Az adatok gönygyölése megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                KézGöngyölDátum.Zárolás("Főmérnökség", DateTime.Today, Cmbtelephely.Text.Trim(), false);
                MessageBox.Show($"A(z) {Cmbtelephely.Text.Trim()} telephelyi zárolás feloldásra került!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Pályaszám_ellenőrzés()
        {
            try
            {
                List<Adat_T5C5_Göngyöl> Adatok = Kéz_Göngyöl.Lista_Adatok("Főmérnökség", DateTime.Today);

                string előző = "";
                string előzőtelep = "";

                List<string> Töröl = new List<string>();
                List<Adat_T5C5_Göngyöl> RögzítAdatok = new List<Adat_T5C5_Göngyöl>();
                foreach (Adat_T5C5_Göngyöl rekord in Adatok)
                {
                    if (előző.Trim() == rekord.Azonosító.Trim() && előzőtelep.Trim() == rekord.Telephely.Trim())
                    {
                        // ha egyforma akkor töröljük
                        Adat_T5C5_Göngyöl ADATRögz = new Adat_T5C5_Göngyöl(
                                           rekord.Azonosító.Trim(),
                                           rekord.Utolsórögzítés,
                                           rekord.Vizsgálatdátuma,
                                           rekord.Utolsóforgalminap,
                                           rekord.Vizsgálatfokozata,
                                           rekord.Vizsgálatszáma,
                                           rekord.Futásnap,
                                           rekord.Telephely);
                        RögzítAdatok.Add(ADATRögz);
                        Töröl.Add(rekord.Azonosító.Trim());
                    }
                    else
                    {
                        előző = rekord.Azonosító.Trim();
                        előzőtelep = rekord.Telephely.Trim();
                    }
                }

                if (Töröl.Count > 0) Kéz_Göngyöl.Törlés("Főmérnökség", DateTime.Today, Töröl);
                if (RögzítAdatok.Count > 0) Kéz_Göngyöl.Rögzítés("Főmérnökség", DateTime.Today, RögzítAdatok);

            }
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

        private void Göngyöl()
        {
            try
            {

                List<Adat_T5C5_Futás> AdatokNapi = Kéz_Futás.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value);     // a napi adatokat pörgetjük végig
                List<Adat_T5C5_Havi_Nap> AdatokHavi = KézHavi.Lista_Adatok(Dátum.Value);      //a havi adatokat betöltése
                List<Adat_T5C5_Göngyöl> AdatokBázis = Kéz_Göngyöl.Lista_Adatok("Főmérnökség", DateTime.Today);      //bázis adatokat betölti
                List<Adat_T5C5_Göngyöl_DátumTábla> AdatokDátum = KézGöngyölDátum.Lista_Adatok("Főmérnökség", DateTime.Today);  //Telephelyi göngyölés

                Holtart.Be(AdatokNapi.Count);
                List<Adat_T5C5_Havi_Nap> AdatokGyHavi = new List<Adat_T5C5_Havi_Nap>();
                List<Adat_T5C5_Havi_Nap> AdatokGyHaviÚ = new List<Adat_T5C5_Havi_Nap>();
                List<Adat_T5C5_Göngyöl> AdatokGyBázis = new List<Adat_T5C5_Göngyöl>();
                List<Adat_T5C5_Göngyöl> AdatokGyBázisÚ = new List<Adat_T5C5_Göngyöl>();
                foreach (Adat_T5C5_Futás rekord in AdatokNapi)
                {
                    string napikód = "0";
                    int futásnap = -1;
                    DateTime vizsgálatdátuma = new DateTime(1900, 1, 1);
                    DateTime utolsóforgalminap = new DateTime(1900, 1, 1);
                    string vizsgálatfokozata = "_";
                    int vizsgálatszáma = -1;

                    Adat_T5C5_Göngyöl AdatBázis = (from a in AdatokBázis
                                                   where a.Azonosító == rekord.Azonosító
                                                   select a).FirstOrDefault();
                    if (AdatBázis != null)
                    {
                        vizsgálatszáma = AdatBázis.Vizsgálatszáma;
                        futásnap = AdatBázis.Futásnap;
                        vizsgálatdátuma = AdatBázis.Vizsgálatdátuma;
                        vizsgálatfokozata = AdatBázis.Vizsgálatfokozata;
                        utolsóforgalminap = AdatBázis.Utolsóforgalminap;
                    }
                    napikód = "_";
                    switch (rekord.Futásstátus.Trim())
                    {
                        case "Forgalomban":
                            {
                                napikód = "F";
                                futásnap++;
                                utolsóforgalminap = Dátum.Value;
                                break;
                            }
                        case "Hibás":
                            {
                                napikód = "R";
                                break;
                            }

                        case "V1":
                            {
                                napikód = "V1";
                                futásnap = 0;
                                vizsgálatdátuma = Dátum.Value;
                                utolsóforgalminap = Dátum.Value;
                                vizsgálatfokozata = "V1";
                                vizsgálatszáma = 0;
                                break;
                            }
                        case "V2":
                            {
                                napikód = "V2";
                                futásnap = 0;
                                vizsgálatdátuma = Dátum.Value;
                                utolsóforgalminap = Dátum.Value;
                                vizsgálatfokozata = "V2";
                                vizsgálatszáma = 0;
                                break;
                            }
                        case "V3":
                            {
                                napikód = "V3";
                                futásnap = 0;
                                vizsgálatdátuma = Dátum.Value;
                                utolsóforgalminap = Dátum.Value;
                                vizsgálatfokozata = "V3";
                                vizsgálatszáma = 0;
                                break;
                            }
                        case "J1":
                            {
                                napikód = "J1";
                                futásnap = 0;
                                vizsgálatdátuma = Dátum.Value;
                                utolsóforgalminap = Dátum.Value;
                                vizsgálatfokozata = "J1";
                                vizsgálatszáma = 0;
                                break;
                            }
                        case "J2":
                            {
                                napikód = "J2";
                                futásnap = 0;
                                vizsgálatdátuma = Dátum.Value;
                                utolsóforgalminap = Dátum.Value;
                                vizsgálatfokozata = "J2";
                                vizsgálatszáma = 0;
                                break;
                            }
                        case "#J":
                            {
                                napikód = "J1";
                                futásnap = 0;
                                vizsgálatdátuma = Dátum.Value;
                                utolsóforgalminap = Dátum.Value;
                                vizsgálatfokozata = "J1";
                                vizsgálatszáma = 0;
                                break;
                            }
                        case "J":
                            {
                                napikód = "J1";
                                futásnap = 0;
                                vizsgálatdátuma = Dátum.Value;
                                utolsóforgalminap = Dátum.Value;
                                vizsgálatfokozata = "J1";
                                vizsgálatszáma = 0;
                                break;
                            }
                        case "E3":
                            {
                                napikód = "E3";
                                futásnap = 0;
                                vizsgálatdátuma = Dátum.Value;
                                utolsóforgalminap = Dátum.Value;
                                vizsgálatfokozata = "E3";

                                vizsgálatszáma += 1;
                                break;
                            }
                        case "-":
                            {
                                napikód = "-";
                                break;
                            }

                    }
                    // Havi tábla
                    Adat_T5C5_Havi_Nap AdatHavi = (from a in AdatokHavi
                                                   where a.Azonosító == rekord.Azonosító
                                                   select a).FirstOrDefault();

                    if (AdatHavi == null)
                    {
                        //Új azonosító
                        Adat_T5C5_Havi_Nap HaviADATÚ = new Adat_T5C5_Havi_Nap(rekord.Azonosító,
                                                            ".", ".", ".", ".", ".", ".", ".", ".", ".", ".",
                                                            ".", ".", ".", ".", ".", ".", ".", ".", ".", ".",
                                                            ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".",
                                                            0,
                                                            "_");
                        AdatokGyHaviÚ.Add(HaviADATÚ);
                    }

                    Adat_T5C5_Havi_Nap HaviADAT = new Adat_T5C5_Havi_Nap(
                                       rekord.Azonosító,
                                       napikód,
                                       futásnap,
                                       Cmbtelephely.Text.Trim());
                    AdatokGyHavi.Add(HaviADAT);


                    // Villamos3 tábla
                    if (AdatBázis == null)
                    {
                        //Új azonosító
                        Adat_T5C5_Göngyöl FőAdatÚ = new Adat_T5C5_Göngyöl(
                                            rekord.Azonosító,
                                            new DateTime(1900, 1, 1),
                                            new DateTime(1900, 1, 1),
                                            new DateTime(1900, 1, 1),
                                            ".",
                                            0,
                                            0,
                                            ".");
                        AdatokGyBázisÚ.Add(FőAdatÚ);
                    }
                    Adat_T5C5_Göngyöl FőAdat = new Adat_T5C5_Göngyöl(
                                      rekord.Azonosító.Trim(),
                                      Dátum.Value,
                                      vizsgálatdátuma,
                                      utolsóforgalminap,
                                      vizsgálatfokozata,
                                      vizsgálatszáma,
                                      futásnap,
                                      Cmbtelephely.Text.Trim());
                    AdatokGyBázis.Add(FőAdat);
                    Holtart.Lép();
                }
                if (AdatokGyBázisÚ.Count > 0) Kéz_Göngyöl.Rögzítés("Főmérnökség", DateTime.Today, AdatokGyBázisÚ);
                if (AdatokGyBázis.Count > 0) Kéz_Göngyöl.Módosítás("Főmérnökség", DateTime.Today, AdatokGyBázis);
                if (AdatokGyHaviÚ.Count > 0) KézHavi.Rögzítés(Dátum.Value, AdatokGyHaviÚ);
                if (AdatokGyHavi.Count > 0) KézHavi.Módosítás(Dátum.Value, AdatokGyHavi);


                // átállítjuk a dátumot
                Adat_T5C5_Göngyöl_DátumTábla Telepdátum = (from a in AdatokDátum
                                                           where a.Telephely == Cmbtelephely.Text.Trim()
                                                           select a).FirstOrDefault();
                Adat_T5C5_Göngyöl_DátumTábla ADATGDátum = new Adat_T5C5_Göngyöl_DátumTábla(Cmbtelephely.Text.Trim(), Dátum.Value, false);
                if (Telepdátum == null)
                    KézGöngyölDátum.Rögzítés("Főmérnökség", Dátum.Value, ADATGDátum);
                else
                    KézGöngyölDátum.Módosítás("Főmérnökség", Dátum.Value, ADATGDátum);
                Gombok_vezérlése();

                Holtart.Ki();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                Vissza_esemény();
                HibaNapló.Log(ex.Message, "A göngyölés közben hiba keletkezett, ezért a félbemaradt göngyölés visszagöngyöltem.\n Próbálja ismételten göngyölni.", ex.StackTrace, ex.Source, ex.HResult);

                MessageBox.Show("A göngyölés közben hiba keletkezett, ezért a félbemaradt göngyölés visszagöngyöltem.\n Próbálja ismételten göngyölni.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region Vissza gönygyölés
        private void Vissza_Click(object sender, EventArgs e)
        {
            try
            {
                AdatokGöngyöl = KézGöngyölDátum.Lista_Adatok("Főmérnökség", DateTime.Today);
                if (AdatokGöngyöl == null) return;
                Adat_T5C5_Göngyöl_DátumTábla Elem = (from a in AdatokGöngyöl
                                                     where a.Zárol == true
                                                     select a).FirstOrDefault();

                if (Elem != null) throw new HibásBevittAdat($"Az adatok göngyölése nem lehetséges, mert {Elem.Telephely} dolgozza fel az adatokat.");

                KézGöngyölDátum.Zárolás("Főmérnökség", DateTime.Today, Cmbtelephely.Text.Trim(), true);
                Vissza_esemény();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                KézGöngyölDátum.Zárolás("Főmérnökség", DateTime.Today, Cmbtelephely.Text.Trim(), false);
                MessageBox.Show($"A(z) {Cmbtelephely.Text.Trim()} telephelyi zárolás feloldásra került!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Vissza_esemény()
        {
            try
            {
                // a havi táblába kitöröljük az adatokat és visszaírjuk a futásnapot
                string helyhonnan = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\futás\{Dátum.Value.Year}";
                helyhonnan += $@"\Villamos3-{Dátum.Value.AddDays(-1):yyyyMMdd}.mdb";

                List<Adat_T5C5_Göngyöl> Áll_Adatok = Kéz_Göngyöl.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.AddDays(-1));
                Áll_Adatok = (from a in Áll_Adatok
                              where a.Telephely == Cmbtelephely.Text.Trim()
                              orderby a.Azonosító
                              select a).ToList();

                Holtart.Be();

                List<Adat_T5C5_Havi_Nap> AdatokGyHavi = new List<Adat_T5C5_Havi_Nap>();
                List<Adat_T5C5_Göngyöl> AdatokGyBázis = new List<Adat_T5C5_Göngyöl>();

                foreach (Adat_T5C5_Göngyöl rekord in Áll_Adatok)
                {
                    Holtart.Lép();
                    // előző napi futás adatokat írjuk vissza a havi táblában
                    Adat_T5C5_Havi_Nap ADATHavi = new Adat_T5C5_Havi_Nap(
                                       rekord.Azonosító,
                                       "/",
                                       rekord.Futásnap,
                                       Cmbtelephely.Text.Trim());
                    AdatokGyHavi.Add(ADATHavi);

                    // módosítjuk a villamos3 adatait
                    Adat_T5C5_Göngyöl ADATBázis = new Adat_T5C5_Göngyöl(
                                      rekord.Azonosító,
                                      rekord.Utolsórögzítés,
                                      rekord.Vizsgálatdátuma,
                                      rekord.Utolsóforgalminap,
                                      rekord.Vizsgálatfokozata,
                                      rekord.Vizsgálatszáma,
                                      rekord.Futásnap,
                                      rekord.Telephely);
                    AdatokGyBázis.Add(ADATBázis);
                }
                if (AdatokGyBázis.Count > 0) Kéz_Göngyöl.Módosítás("Főmérnökség", DateTime.Today, AdatokGyBázis);
                if (AdatokGyHavi.Count > 0) KézHavi.Módosítás(Dátum.Value, AdatokGyHavi);

                Holtart.Ki();

                // visszaállítjuk az utolsó napot Villamos3-naplófáljból
                List<Adat_T5C5_Göngyöl_DátumTábla> ElőzőNapi = KézGöngyölDátum.Lista_Adatok("Főmérnökség", DateTime.Today);

                Adat_T5C5_Göngyöl_DátumTábla rögzítés = (from a in ElőzőNapi
                                                         where a.Telephely == Cmbtelephely.Text.Trim()
                                                         select a).FirstOrDefault();
                if (rögzítés != null)
                {
                    Adat_T5C5_Göngyöl_DátumTábla ADAT = new Adat_T5C5_Göngyöl_DátumTábla(
                                                 Cmbtelephely.Text.Trim(),
                                                 rögzítés.Utolsórögzítés.AddDays(-1),
                                                 false);
                    KézGöngyölDátum.Módosítás("Főmérnökség", DateTime.Today, ADAT);

                    // kitöröljük a Villamos3-naplófáljból fájlt miutón frissítettük a villamos3-t
                    if (File.Exists(helyhonnan)) File.Delete(helyhonnan);
                }

                Gombok_vezérlése();
                MessageBox.Show("Az adatok visszagöngyölésre kerültek!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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


        #region Nap nyitás-zárás
        private void Naplezárása_Click(object sender, EventArgs e)
        {
            Naplezárása_esemény();
        }

        private void Naplezárása_esemény()
        {
            try
            {
                List<Adat_T5C5_Futás1> ListaSt = Kéz_Futás1.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value).ToList();
                Adat_T5C5_Futás1 Elem = (from a in ListaSt select a).FirstOrDefault();

                Adat_T5C5_Futás1 ADAT = new Adat_T5C5_Futás1(1);
                if (Elem != null)
                    Kéz_Futás1.Módosítás(Cmbtelephely.Text.Trim(), Dátum.Value, ADAT);
                else
                    Kéz_Futás1.Rögzítés(Cmbtelephely.Text.Trim(), Dátum.Value, ADAT);
                Gombok_vezérlése();
                Bevitelilap.Visible = false;
            }
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

        private void Napkinyitása_Click(object sender, EventArgs e)
        {
            Napkinyitása_esemény();
        }

        private void Napkinyitása_esemény()
        {
            try
            {
                List<Adat_T5C5_Futás1> ListaSt = Kéz_Futás1.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value).ToList();
                Adat_T5C5_Futás1 Elem = (from a in ListaSt select a).FirstOrDefault();

                Adat_T5C5_Futás1 ADAT = new Adat_T5C5_Futás1(0);
                if (Elem != null)
                    Kéz_Futás1.Módosítás(Cmbtelephely.Text.Trim(), Dátum.Value, ADAT);
                else
                    Kéz_Futás1.Rögzítés(Cmbtelephely.Text.Trim(), Dátum.Value, ADAT);

                Gombok_vezérlése();
                Bevitelilap.Visible = false;
            }
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


        #region ZSER
        private void Zserbeolvasás_Click(object sender, EventArgs e)
        {
            try
            {
                // ha létezik akkor töröljük az adatokat
                List<Adat_Főkönyv_ZSER> Adatok = KézZser.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, "");
                if (Adatok != null && Adatok.Count == 0)
                {
                    if (MessageBox.Show("Már van az adott napra feltöltve adat ! Módosítjuk az adatokat ?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        KézZser.Törlés(Cmbtelephely.Text.Trim(), Dátum.Value, "");
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
                string fájlexc;
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;
                DateTime Kezdet = DateTime.Now;
                Holtart.Be();
                Főkönyv_Funkciók.ZSER_Betöltés(Cmbtelephely.Text.Trim(), Dátum.Value, "", fájlexc);
                Holtart.Ki();
                DateTime Vég = DateTime.Now;
                MessageBox.Show($"Az adat konvertálás befejeződött!\n Eltelt idő{Vég - Kezdet}", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

        private void Zseradategyeztetés_Click(object sender, EventArgs e)
        {
            try
            {
                // megnézzük, hogy létezik-e adott napi tábla
                List<Adat_T5C5_Futás> AdatokFutás = Kéz_Futás.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value);
                if (AdatokFutás == null || AdatokFutás.Count == 0) throw new HibásBevittAdat("Hiányzonak a napi adatok!");

                // leellnőrizzük a zser adatokat hogy megvannak-e
                List<Adat_Főkönyv_ZSER> AdatokZser = KézZser.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, "");
                if (AdatokZser == null || AdatokZser.Count == 0)
                {
                    AdatokZser = KézZser.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, "du");
                    if (AdatokZser == null || AdatokZser.Count == 0)
                    {
                        AdatokZser = KézZser.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value, "de");
                        if (AdatokZser == null || AdatokZser.Count == 0) throw new HibásBevittAdat("Hiányzonak a napi ZSER adatok!");
                    }
                }

                Holtart.Be();

                List<string> Azonosítók = new List<string>();
                foreach (Adat_T5C5_Futás rekord in AdatokFutás)
                {
                    Holtart.Lép();

                    Adat_Főkönyv_ZSER Kiadás = (from a in AdatokZser
                                                where a.Kocsi1 == rekord.Azonosító.Trim() || a.Kocsi2 == rekord.Azonosító.Trim() ||
                                                      a.Kocsi3 == rekord.Azonosító.Trim() || a.Kocsi4 == rekord.Azonosító.Trim() ||
                                                      a.Kocsi5 == rekord.Azonosító.Trim() || a.Kocsi6 == rekord.Azonosító.Trim()
                                                select a).FirstOrDefault();
                    if (Kiadás != null)
                        if (!(rekord.Futásstátus.Contains("E") || rekord.Futásstátus.Contains("V") || rekord.Futásstátus.Contains("J")))
                            Azonosítók.Add(rekord.Azonosító.Trim());


                }
                if (Azonosítók.Count > 0) Kéz_Futás.Módosítás(Cmbtelephely.Text.Trim(), Dátum.Value, Azonosítók);

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
        #endregion


        #region Funkciók
        private void Gombok_Színezése(int Gombszám, string Kategória)
        {
            // frissítjük a színezést
            switch (Kategória.Trim())
            {
                case "Forgalomban":
                    {
                        // zöld
                        Panel3.Controls[Gombszám].BackColor = Color.LimeGreen;
                        ToolTip1.SetToolTip(Panel3.Controls[Gombszám], "Hibás");
                        break;
                    }
                case "-":
                    {
                        // szürke
                        Panel3.Controls[Gombszám].BackColor = Color.Silver;
                        ToolTip1.SetToolTip(Panel3.Controls[Gombszám], "-");
                        break;
                    }
                case "_":
                    {
                        // szürke
                        Panel3.Controls[Gombszám].BackColor = Color.Silver;
                        ToolTip1.SetToolTip(Panel3.Controls[Gombszám], "_");
                        break;
                    }
                case "E3":
                    {
                        // kék
                        Panel3.Controls[Gombszám].BackColor = Color.Blue;
                        Panel3.Controls[Gombszám].ForeColor = Color.White;
                        ToolTip1.SetToolTip(Panel3.Controls[Gombszám], "E3");
                        break;
                    }
                case "V1":
                    {
                        // sárga
                        Panel3.Controls[Gombszám].BackColor = Color.Yellow;
                        ToolTip1.SetToolTip(Panel3.Controls[Gombszám], "V1");
                        break;
                    }
                case "V2":
                    {
                        // narancssárga
                        Panel3.Controls[Gombszám].BackColor = Color.DarkOrange;
                        ToolTip1.SetToolTip(Panel3.Controls[Gombszám], "V2");
                        break;
                    }
                case "V3":
                    {
                        // narancssárga
                        Panel3.Controls[Gombszám].BackColor = Color.DarkOrange;
                        ToolTip1.SetToolTip(Panel3.Controls[Gombszám], "V3");
                        break;
                    }
                case "J1":
                    {
                        // narancssárga
                        Panel3.Controls[Gombszám].BackColor = Color.DarkOrange;
                        ToolTip1.SetToolTip(Panel3.Controls[Gombszám], "J1");
                        break;
                    }
                case "J2":
                    {
                        // narancssárga
                        Panel3.Controls[Gombszám].BackColor = Color.DarkOrange;
                        ToolTip1.SetToolTip(Panel3.Controls[Gombszám], "J2");
                        break;
                    }
                case "Hibás":
                    {
                        // piros
                        Panel3.Controls[Gombszám].BackColor = Color.Red;
                        Panel3.Controls[Gombszám].ForeColor = Color.White;
                        ToolTip1.SetToolTip(Panel3.Controls[Gombszám], "Hibás");
                        break;
                    }
            }

        }

        private void SAP_adatok_Click(object sender, EventArgs e)
        {
            try
            {
                SAP_adatok.Visible = false;
                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "SAP-s Adatok betöltése",
                    FileName = "",
                    Filter = "Excel (*.xlsx)|*.xlsx|Excel 97-2003 (*.xls)|*.xls"
                };
                string fájlexc;
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = OpenFileDialog1.FileName;
                else
                {
                    SAP_adatok.Visible = true;
                    return;
                }

                Holtart.Be();

                timer1.Enabled = true;
                FájlExcel_ = fájlexc;

                SZál_KM_Beolvasás(() =>
                {
                    //leállítjuk a számlálót és kikapcsoljuk a holtartot.
                    timer1.Enabled = false;
                    Holtart.Ki();
                });

                SAP_adatok.Visible = true;
            }
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

        private void SZál_KM_Beolvasás(Action callback)
        {
            Thread proc = new Thread(() =>
            {
                //beolvassuk az adatokat
                SAP_Adatokbeolvasása_km.Km_beolvasó(FájlExcel_);
                MessageBox.Show("Az adat konvertálás befejeződött!", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Invoke(callback, new object[] { });
            });
            proc.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Holtart.Lép();
        }

        private void Label13_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (CTRL_le)
                {
                    KézGöngyölDátum.ZárolásFelold("Főmérnökség", DateTime.Today);
                    MessageBox.Show($"Minden telephelyi zárolás feloldásra került!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private List<string> Pályaszám_feltöltés()
        {
            List<string> Válasz = new List<string>();
            try
            {
                if (Cmbtelephely.Text.Trim() == "") return Válasz;
                List<Adat_Jármű> Adatok = Kéz_Jármű.Lista_Adatok("Főmérnökség");

                Válasz = (from a in Adatok
                          where a.Üzem == Cmbtelephely.Text.Trim()
                          && a.Törölt == false
                          && a.Valóstípus.Contains("T5C5")
                          orderby a.Azonosító
                          select a.Azonosító).ToList();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Pályaszám_feltöltés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Válasz;
        }
        #endregion
    }
}