using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    public partial class Ablak_T5C5_Tulajdonság
    {
        string _fájlexc;
        DataTable _AdatTábla = new DataTable();
        long utolsósor;
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly Kezelő_Jármű2 KézVizsgálat = new Kezelő_Jármű2();
        readonly Kezelő_T5C5_Kmadatok KézKmAdatok = new Kezelő_T5C5_Kmadatok("T5C5");
        readonly Kezelő_Kerék_Mérés KézMérés = new Kezelő_Kerék_Mérés();
        readonly Kezelő_Ciklus KézCiklus = new Kezelő_Ciklus();
        readonly Kezelő_T5C5_Göngyöl KézGöngyöl = new Kezelő_T5C5_Göngyöl();
        readonly Kezelő_Szerelvény Kézszer = new Kezelő_Szerelvény();
        readonly Kezelő_kiegészítő_telephely KézKieg = new Kezelő_kiegészítő_telephely();
        readonly Kezelő_T5C5_Kmadatok_Napló KézT5C5Napló = new Kezelő_T5C5_Kmadatok_Napló();
        readonly Kezelő_T5C5_Előterv KézElőterv = new Kezelő_T5C5_Előterv();

        List<Adat_T5C5_Kmadatok> AdatokKmAdatok = new List<Adat_T5C5_Kmadatok>();
        List<Adat_Jármű> AdatokJármű = new List<Adat_Jármű>();
        List<Adat_Kerék_Mérés> AdatokMérés = new List<Adat_Kerék_Mérés>();
        List<Adat_Ciklus> AdatokCiklus = new List<Adat_Ciklus>();

        int Hónapok = 24;
        long Havikm = 5000;

        #region Alap
        public Ablak_T5C5_Tulajdonság()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            AdatokCiklus = KézCiklus.Lista_Adatok();
            Telephelyekfeltöltése();
            Pályaszám_feltöltés();
            Fülek.SelectedIndex = 0;
            Fülekkitöltése();
            Jogosultságkiosztás();
            Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;
        }

        private void Tulajdonság_T5C5_Load(object sender, EventArgs e)
        {
        }

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    Cmbtelephely.Items.Add(Elem);
                if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér)
                { Cmbtelephely.Text = Cmbtelephely.Items[0].ToStrTrim(); }
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
                Rögzítnap.Enabled = false;

                Utolsó_V_rögzítés.Enabled = false;
                Töröl.Enabled = false;

                // csak főmérnökségi belépéssel törölhető
                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {
                    Töröl.Visible = true;
                    Új_adat.Visible = true;
                }
                else
                {
                    Töröl.Visible = false;
                    Új_adat.Visible = false;
                }
                melyikelem = 106;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Rögzítnap.Enabled = true;
                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                {

                }
                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 3))
                {

                }
                melyikelem = 107;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Utolsó_V_rögzítés.Enabled = true;
                    Töröl.Enabled = true;
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

        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Tulajdonság_T5C5.html";
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
            Pályaszám_feltöltés();
        }

        private void Pályaszám_feltöltés()
        {
            try
            {
                Pályaszám.Items.Clear();
                if ((Cmbtelephely.Text) == "") return;
                AdatokJármű = KézJármű.Lista_Adatok("Főmérnökség");
                List<Adat_Jármű> Adatok = new List<Adat_Jármű>();

                // ha nem telephelyeól kérdezzük le akkor minden kocsit kiír
                if (Program.PostásTelephely == "Főmérnökség")
                {
                    Adatok = (from a in AdatokJármű
                              where a.Törölt == false
                              && a.Valóstípus.Contains("T5C5")
                              orderby a.Azonosító
                              select a).ToList();
                }
                else if (Program.Postás_Vezér)
                {
                    // Szakszolgálat is 
                    Adatok = (from a in AdatokJármű
                              where a.Törölt == false
                              && a.Valóstípus.Contains("T5C5")
                              orderby a.Azonosító
                              select a).ToList();
                }
                else
                {
                    Adatok = (from a in AdatokJármű
                              where a.Törölt == false
                              && a.Üzem == Cmbtelephely.Text.Trim()
                              && a.Valóstípus.Contains("T5C5")
                              orderby a.Azonosító
                              select a).ToList();
                }
                foreach (Adat_Jármű elem in Adatok)
                    Pályaszám.Items.Add(elem.Azonosító.Trim());

                Pályaszám.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pályaszámkereső_Click(object sender, EventArgs e)
        {
            Frissít();
        }

        private void Frissít()
        {
            if (Pályaszám.Text.Trim() == "")
                return;

            switch (Fülek.SelectedIndex)
            {
                case 0:
                    {
                        Kiirjaalapadatokat();
                        break;
                    }
                case 1:
                    {
                        Kiír_Futásadatok();
                        break;
                    }
                case 4:
                    {
                        Kiüríti_lapfül();
                        Kiirjaatörténelmet();
                        break;
                    }
                case 5:
                    {
                        Kiüríti_lapfül();
                        Kiirjaatörténelmet();
                        break;
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
                        // alapadatok
                        Alap_Adat_Ürítés();
                        Kiirjaalapadatokat();
                        break;
                    }
                case 1:
                    {
                        // Futás adatok
                        Kiír_Futásadatok();
                        Combofeltöltés();
                        break;
                    }

                case 3:
                    {
                        CiklusrendCombo_feltöltés();
                        Vizsgsorszámcombofeltölés();
                        Üzemek_listázása();
                        break;
                    }
                case 4:
                    {
                        Kiirjaatörténelmet();
                        break;
                    }
                case 5:
                    {

                        Pszlista();
                        Telephelylista();
                        break;
                    }
            }
        }

        private void Pályaszám_SelectedIndexChanged(object sender, EventArgs e)
        {
            Frissít();
            Kiüríti_lapfül();
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


        #region alapadatok lapfül
        private void Kiirjaalapadatokat()
        {
            try
            {
                if (Cmbtelephely.Text.Trim() == "") return;
                if (Pályaszám.Text.Trim() == "") return;
                Alap_Adat_Ürítés();
                // ürítjük a mezőket

                Alap_Adatok();
                E2_Vizsgálat();
                Előírt_Szerelvény_kiir();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Alap_Adat_Ürítés()
        {
            Típus_text.Text = "";
            Státus_text.Text = "";
            Miótaáll_text.Text = "";
            Szerelvény_text.Text = "";
            Elő_Szerelvény_text.Text = "";
            Vizsgálati_text.Text = "";
            Főmérnökség_text.Text = "";
            Járműtípus_text.Text = "";
        }

        private void E2_Vizsgálat()
        {
            try
            {
                List<Adat_Jármű_2> AdatokVizsgálat = KézVizsgálat.Lista_Adatok(Cmbtelephely.Text.Trim());

                Adat_Jármű_2 ElemVizsgálat = (from a in AdatokVizsgálat
                                              where a.Azonosító == Pályaszám.Text.Trim()
                                              select a).FirstOrDefault();
                if (ElemVizsgálat != null)
                {
                    switch (ElemVizsgálat.Haromnapos)
                    {
                        case 1:
                            {
                                Vizsgálati_text.Text = "Hétfő- Csütörtök";
                                break;
                            }
                        case 2:
                            {
                                Vizsgálati_text.Text = "Kedd- Péntek";
                                break;
                            }
                        case 3:
                            {
                                Vizsgálati_text.Text = "Szerda- Szombat";
                                break;
                            }

                        default:
                            {
                                Vizsgálati_text.Text = "Nincs beállítva";
                                break;
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

        private void Alap_Adatok()
        {
            List<Adat_Jármű> Adatok = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
            Adat_Jármű rekord = Adatok.FirstOrDefault(a => a.Azonosító == Pályaszám.Text.Trim());
            if (rekord != null)
            {
                Típus_text.Text = rekord.Típus.Trim();
                Járműtípus_text.Text = rekord.Valóstípus2.Trim();
                Főmérnökség_text.Text = rekord.Valóstípus.Trim();
                switch (rekord.Státus)
                {
                    case 0:
                        {
                            Státus_text.Text = "Nincs hibája";
                            break;
                        }
                    case 1:
                        {
                            Státus_text.Text = "Szabad";
                            break;
                        }
                    case 2:
                        {
                            Státus_text.Text = "Beállóba kért";
                            break;
                        }
                    case 3:
                        {
                            Státus_text.Text = "Beállóba adott";
                            break;
                        }
                    case 4:
                        {
                            Státus_text.Text = "Benn maradó";
                            break;
                        }
                }
                if (rekord.Miótaáll == null || rekord.Miótaáll.ToString("yyyy.MM.dd") == "1900.01.01")
                    Miótaáll_text.Text = "";
                else
                    Miótaáll_text.Text = rekord.Miótaáll.ToString();
            }

            Szerelvény_Kiírás();
        }

        private void Szerelvény_Kiírás()
        {
            try
            {
                List<Adat_Szerelvény> Adatok = Kézszer.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adat_Szerelvény Szerel = (from a in Adatok
                                          where a.Kocsi1 == Pályaszám.Text.Trim()
                                          || a.Kocsi2 == Pályaszám.Text.Trim()
                                          || a.Kocsi3 == Pályaszám.Text.Trim()
                                          || a.Kocsi4 == Pályaszám.Text.Trim()
                                          || a.Kocsi5 == Pályaszám.Text.Trim()
                                          || a.Kocsi6 == Pályaszám.Text.Trim()
                                          select a).FirstOrDefault();
                if (Szerel != null)
                {
                    Szerelvény_text.Text = Szerel.Kocsi1.Trim();
                    Szerelvény_text.Text += Szerel.Kocsi2.Trim() != "0" ? "-" + Szerel.Kocsi2.Trim() : "";
                    Szerelvény_text.Text += Szerel.Kocsi3.Trim() != "0" ? "-" + Szerel.Kocsi3.Trim() : "";
                    Szerelvény_text.Text += Szerel.Kocsi4.Trim() != "0" ? "-" + Szerel.Kocsi4.Trim() : "";
                    Szerelvény_text.Text += Szerel.Kocsi5.Trim() != "0" ? "-" + Szerel.Kocsi5.Trim() : "";
                    Szerelvény_text.Text += Szerel.Kocsi6.Trim() != "0" ? "-" + Szerel.Kocsi6.Trim() : "";
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

        private bool Üzembehelyzés(string azonosító)
        {
            bool válasz = false;
            string hely = Application.StartupPath + @"\Főmérnökség\Jegyzőkönyvek\";
            DirectoryInfo di = new DirectoryInfo(hely);
            var aryFi = di.GetFiles($"*{azonosító}*.pdf");
            if (aryFi.Length > 0)
                válasz = true;
            return válasz;
        }

        private void Előírt_Szerelvény_kiir()
        {
            List<Adat_Szerelvény> Adatok = Kézszer.Lista_Adatok(Cmbtelephely.Text.Trim(), true);
            Adat_Szerelvény Szerel = (from a in Adatok
                                      where a.Kocsi1 == Pályaszám.Text.Trim()
                                      || a.Kocsi2 == Pályaszám.Text.Trim()
                                      || a.Kocsi3 == Pályaszám.Text.Trim()
                                      || a.Kocsi4 == Pályaszám.Text.Trim()
                                      || a.Kocsi5 == Pályaszám.Text.Trim()
                                      || a.Kocsi6 == Pályaszám.Text.Trim()
                                      select a).FirstOrDefault();
            if (Szerel != null)
            {
                Elő_Szerelvény_text.Text = Szerel.Kocsi1.Trim();
                Elő_Szerelvény_text.Text += Szerel.Kocsi2.Trim() != "_" ? "-" + Szerel.Kocsi2.Trim() : "";
                Elő_Szerelvény_text.Text += Szerel.Kocsi3.Trim() != "_" ? "-" + Szerel.Kocsi3.Trim() : "";
                Elő_Szerelvény_text.Text += Szerel.Kocsi4.Trim() != "_" ? "-" + Szerel.Kocsi4.Trim() : "";
                Elő_Szerelvény_text.Text += Szerel.Kocsi5.Trim() != "_" ? "-" + Szerel.Kocsi5.Trim() : "";
                Elő_Szerelvény_text.Text += Szerel.Kocsi6.Trim() != "_" ? "-" + Szerel.Kocsi6.Trim() : "";
            }
        }
        #endregion


        #region Futásadatok lapfül

        private void Kiír_Futásadatok()
        {
            try
            {
                List<Adat_T5C5_Göngyöl> Adatok = KézGöngyöl.Lista_Adatok("Főmérnökség", DateTime.Today);
                Adat_T5C5_Göngyöl Rekord = (from a in Adatok
                                            where a.Azonosító == Pályaszám.Text.Trim()
                                            select a).FirstOrDefault();
                if (Rekord != null)
                {
                    Utolsóvizsgálatdátuma.Value = Rekord.Vizsgálatdátuma;
                    Utolsóvizsgálatfokozata.Text = Rekord.Vizsgálatfokozata.Trim();
                    Utolsóvizsgálatszáma.Text = Rekord.Vizsgálatszáma.ToString();
                    Futásnap.Text = Rekord.Futásnap.ToString();
                    Utolsóforgalminap.Value = Rekord.Utolsóforgalminap;
                }
                else
                {
                    Utolsóvizsgálatdátuma.Value = new DateTime(1900, 1, 1);
                    Utolsóvizsgálatfokozata.Text = "";
                    Utolsóvizsgálatszáma.Text = "";
                    Futásnap.Text = "";
                    Utolsóforgalminap.Value = new DateTime(1900, 1, 1);
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

        private void Rögzítnap_Click(object sender, EventArgs e)
        {
            try
            {
                // leellenőrizzük, hogy minden adat ki van-e töltve
                if (Pályaszám.Text.Trim() == "") throw new HibásBevittAdat("A pályaszámot meg kell adni.");
                if (Utolsóvizsgálatfokozata.Text.Trim() == "") throw new HibásBevittAdat("Az utolsó vizsgálat fokozata nem lehet üres.");
                if (Utolsóvizsgálatszáma.Text.Trim() == "") throw new HibásBevittAdat("Az utolsó vizsgálat száma nem lehet üres.");
                if (!int.TryParse(Utolsóvizsgálatszáma.Text, out int utolsóvizsgálatszám)) throw new HibásBevittAdat("Az utolsó vizsgálat száma egész számnak kell lennie.");
                if (utolsóvizsgálatszám < 0) throw new HibásBevittAdat("Az utolsó vizsgálat száma nem lehet negatív szám.");
                if (Futásnap.Text.Trim() == "") throw new HibásBevittAdat("A futásnap nem lehet üres.");
                if (!int.TryParse(Futásnap.Text, out int futás_nap)) throw new HibásBevittAdat("A futásnapnak egész számnak kell lennie.");

                // megnézzük az adatbázist, ha nincs ilyen kocsi benne akkor rógzít máskülönben az adatokat módosítja
                // Leellenőrizzük, hogy van-e ilyen kocsi
                List<Adat_T5C5_Göngyöl> AdatokT5C5Állomány = KézGöngyöl.Lista_Adatok("Főmérnökség", DateTime.Today);

                Adat_T5C5_Göngyöl Elem = (from a in AdatokT5C5Állomány
                                          where a.Azonosító == Pályaszám.Text.Trim()
                                          select a).FirstOrDefault();
                Adat_T5C5_Göngyöl ADAT = new Adat_T5C5_Göngyöl(
                                  Pályaszám.Text.Trim(),
                                  Utolsóforgalminap.Value,
                                  Utolsóvizsgálatdátuma.Value,
                                  Utolsóforgalminap.Value,
                                  Utolsóvizsgálatfokozata.Text.Trim(),
                                  utolsóvizsgálatszám,
                                  futás_nap,
                                  Cmbtelephely.Text.Trim());
                if (Elem == null)
                    KézGöngyöl.Rögzítés("Főmérnökség", DateTime.Today, ADAT);
                else
                    KézGöngyöl.Módosítás("Főmérnökség", DateTime.Today, ADAT);

                MessageBox.Show("Az adatok rögzítése megtörtént. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Combofeltöltés()
        {
            // combo feltöltése adatokkal
            Utolsóvizsgálatfokozata.Items.Clear();
            Utolsóvizsgálatfokozata.Items.Add("E3");
            Utolsóvizsgálatfokozata.Items.Add("V1");
            Utolsóvizsgálatfokozata.Items.Add("V2");
            Utolsóvizsgálatfokozata.Items.Add("V3");
            Utolsóvizsgálatfokozata.Items.Add("J1");
            Utolsóvizsgálatfokozata.Items.Add("J2");
            Utolsóvizsgálatfokozata.Refresh();
        }
        #endregion


        #region Lekérdezések
        private void Lekérdezés_lekérdezés_Click(object sender, EventArgs e)
        {
            try
            {
                Tábla_lekérdezés.Rows.Clear();
                Tábla_lekérdezés.Columns.Clear();
                Tábla_lekérdezés.Refresh();
                Tábla_lekérdezés.Visible = false;
                Tábla_lekérdezés.ColumnCount = 28;
                // fejléc elkészítése

                Tábla_lekérdezés.Columns[0].HeaderText = "Psz";
                Tábla_lekérdezés.Columns[0].Width = 60;
                Tábla_lekérdezés.Columns[1].HeaderText = "Vizsg. foka";
                Tábla_lekérdezés.Columns[1].Width = 60;
                Tábla_lekérdezés.Columns[2].HeaderText = "Vizsg. Ssz.";
                Tábla_lekérdezés.Columns[2].Width = 60;
                Tábla_lekérdezés.Columns[3].HeaderText = "Vizsg. Kezdete";
                Tábla_lekérdezés.Columns[3].Width = 110;
                Tábla_lekérdezés.Columns[4].HeaderText = "Vizsg. Vége";
                Tábla_lekérdezés.Columns[4].Width = 110;
                Tábla_lekérdezés.Columns[5].HeaderText = "Vizsg KM állás";
                Tábla_lekérdezés.Columns[5].Width = 80;
                Tábla_lekérdezés.Columns[6].HeaderText = "Frissítés Dátum";
                Tábla_lekérdezés.Columns[6].Width = 110;
                Tábla_lekérdezés.Columns[7].HeaderText = "KM J-óta";
                Tábla_lekérdezés.Columns[7].Width = 80;
                Tábla_lekérdezés.Columns[8].HeaderText = "V után futott";
                Tábla_lekérdezés.Columns[8].Width = 80;
                Tábla_lekérdezés.Columns[9].HeaderText = "Havi km";
                Tábla_lekérdezés.Columns[9].Width = 80;
                Tábla_lekérdezés.Columns[10].HeaderText = "Felújítás szám";
                Tábla_lekérdezés.Columns[10].Width = 80;
                Tábla_lekérdezés.Columns[11].HeaderText = "Felújítás Dátum";
                Tábla_lekérdezés.Columns[11].Width = 110;
                Tábla_lekérdezés.Columns[12].HeaderText = "Ciklusrend típus";
                Tábla_lekérdezés.Columns[12].Width = 80;
                Tábla_lekérdezés.Columns[13].HeaderText = "Üzembehelyezés km";
                Tábla_lekérdezés.Columns[13].Width = 80;
                Tábla_lekérdezés.Columns[14].HeaderText = "Telephely";
                Tábla_lekérdezés.Columns[14].Width = 80;
                Tábla_lekérdezés.Columns[15].HeaderText = "Típus";
                Tábla_lekérdezés.Columns[15].Width = 80;
                Tábla_lekérdezés.Columns[16].HeaderText = "Kerék K11";
                Tábla_lekérdezés.Columns[16].Width = 80;
                Tábla_lekérdezés.Columns[17].HeaderText = "Kerék K12";
                Tábla_lekérdezés.Columns[17].Width = 80;
                Tábla_lekérdezés.Columns[18].HeaderText = "Kerék K21";
                Tábla_lekérdezés.Columns[18].Width = 80;
                Tábla_lekérdezés.Columns[19].HeaderText = "Kerék K22";
                Tábla_lekérdezés.Columns[19].Width = 80;
                Tábla_lekérdezés.Columns[20].HeaderText = "Kerék min";
                Tábla_lekérdezés.Columns[20].Width = 80;

                Tábla_lekérdezés.Columns[21].HeaderText = "Ssz.";
                Tábla_lekérdezés.Columns[21].Width = 80;
                Tábla_lekérdezés.Columns[22].HeaderText = "Végezte";
                Tábla_lekérdezés.Columns[22].Width = 120;
                Tábla_lekérdezés.Columns[23].HeaderText = "Következő V";
                Tábla_lekérdezés.Columns[23].Width = 120;
                Tábla_lekérdezés.Columns[24].HeaderText = "Következő V Ssz.";
                Tábla_lekérdezés.Columns[24].Width = 120;
                Tábla_lekérdezés.Columns[25].HeaderText = "Következő V2-V3";
                Tábla_lekérdezés.Columns[25].Width = 120;
                Tábla_lekérdezés.Columns[26].HeaderText = "Következő V2-V3 Ssz.";
                Tábla_lekérdezés.Columns[26].Width = 120;
                Tábla_lekérdezés.Columns[27].HeaderText = "Utolsó V2-V3 számláló";
                Tábla_lekérdezés.Columns[27].Width = 120;

                List<Adat_T5C5_Kmadatok> Adatok = KézKmAdatok.Lista_Adatok();
                Adatok = (from a in Adatok
                          where a.Törölt == false
                          orderby a.Vizsgdátumk descending
                          group a by a.Azonosító into g
                          select g.First()).OrderBy(a => a.Azonosító).ToList();
                Holtart.Be();

                List<Adat_Kerék_Mérés> Mérés_Adatok = KézMérés.Lista_Adatok(DateTime.Today.Year - 1);
                List<Adat_Kerék_Mérés> Mérés_AdatokE = KézMérés.Lista_Adatok(DateTime.Today.Year);
                if (Mérés_AdatokE != null) Mérés_Adatok.AddRange(Mérés_AdatokE);

                int i = 0;
                foreach (Adat_T5C5_Kmadatok rekord in Adatok)
                {
                    Holtart.Lép();

                    Adat_Jármű TípusAdat = (from a in AdatokJármű
                                            where a.Azonosító == rekord.Azonosító
                                            select a).FirstOrDefault();
                    if (TípusAdat != null)
                    {
                        Tábla_lekérdezés.RowCount++;
                        i = Tábla_lekérdezés.RowCount - 1;

                        Tábla_lekérdezés.Rows[i].Cells[0].Value = rekord.Azonosító;
                        Tábla_lekérdezés.Rows[i].Cells[1].Value = rekord.Vizsgfok.Trim();
                        Tábla_lekérdezés.Rows[i].Cells[2].Value = rekord.Vizsgsorszám;
                        Tábla_lekérdezés.Rows[i].Cells[3].Value = rekord.Vizsgdátumk.ToString("yyyy.MM.dd");
                        Tábla_lekérdezés.Rows[i].Cells[4].Value = rekord.Vizsgdátumv.ToString("yyyy.MM.dd");
                        Tábla_lekérdezés.Rows[i].Cells[5].Value = rekord.Vizsgkm;
                        Tábla_lekérdezés.Rows[i].Cells[6].Value = rekord.KMUdátum.ToString("yyyy.MM.dd");
                        Tábla_lekérdezés.Rows[i].Cells[7].Value = rekord.KMUkm;
                        if (rekord.Vizsgsorszám == 0)
                            Tábla_lekérdezés.Rows[i].Cells[8].Value = rekord.KMUkm; // ha J akkor nem kell különbséget képezni
                        else
                            Tábla_lekérdezés.Rows[i].Cells[8].Value = (rekord.KMUkm - rekord.Vizsgkm);

                        Tábla_lekérdezés.Rows[i].Cells[9].Value = rekord.Havikm;
                        Tábla_lekérdezés.Rows[i].Cells[10].Value = rekord.Jjavszám;
                        Tábla_lekérdezés.Rows[i].Cells[11].Value = rekord.Fudátum.ToString("yyyy.MM.dd");
                        Tábla_lekérdezés.Rows[i].Cells[12].Value = rekord.Ciklusrend.Trim();
                        Tábla_lekérdezés.Rows[i].Cells[13].Value = rekord.Teljeskm;

                        Tábla_lekérdezés.Rows[i].Cells[14].Value = TípusAdat.Üzem.Trim();
                        Tábla_lekérdezés.Rows[i].Cells[15].Value = TípusAdat.Típus.Trim();
                        // Kerékadatok
                        List<Adat_Kerék_Mérés> Mérés = (from a in Mérés_Adatok
                                                        where a.Azonosító == rekord.Azonosító
                                                        orderby a.Mikor descending
                                                        select a).ToList();
                        int kerékminimum = 1000;
                        if (Mérés != null && Mérés.Count >= 4)
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                Tábla_lekérdezés.Rows[i].Cells[16 + j].Value = Mérés[j].Méret;
                                if (kerékminimum > Mérés[j].Méret) kerékminimum = Mérés[j].Méret;
                            }
                        }
                        Tábla_lekérdezés.Rows[i].Cells[20].Value = kerékminimum;

                        Tábla_lekérdezés.Rows[i].Cells[21].Value = rekord.ID;
                        if (rekord.V2végezte != "_") Tábla_lekérdezés.Rows[i].Cells[22].Value = rekord.V2végezte.Trim();
                        Tábla_lekérdezés.Rows[i].Cells[23].Value = rekord.KövV.Trim();
                        Tábla_lekérdezés.Rows[i].Cells[24].Value = rekord.KövV_sorszám;
                        Tábla_lekérdezés.Rows[i].Cells[25].Value = rekord.KövV2.Trim();
                        Tábla_lekérdezés.Rows[i].Cells[26].Value = rekord.KövV2_sorszám;
                        Tábla_lekérdezés.Rows[i].Cells[27].Value = rekord.V2V3Számláló;
                    }
                }
                Tábla_lekérdezés.Visible = true;
                Holtart.Ki();
                Tábla_lekérdezés.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Excellekérdezés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla_lekérdezés.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"T5C5_futásadatok_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;
                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Tábla_lekérdezés, false);
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

        private void Teljes_adatbázis_excel_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog
            {
                // kimeneti fájl helye és neve
                InitialDirectory = "MyDocuments",

                Title = "Adatbázis mentése Excel fájlba",
                FileName = $"T5C5_adatbázis_mentés_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                Filter = "Excel |*.xlsx"
            };
            // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
            if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                _fájlexc = SaveFileDialog1.FileName;
            else
                return;
            _AdatTábla.Clear();
            //    _AdatTábla = AdattáblaFeltöltés();
            List<Adat_T5C5_Kmadatok> Adatok = KézKmAdatok.Lista_Adatok();
            _AdatTábla = MyF.ToDataTable(Adatok);
            Holtart.Be();
            timer1.Enabled = true;
            SZál_ABadatbázis(() =>
             { //leállítjuk a számlálót és kikapcsoljuk a holtartot.
                 timer1.Enabled = false;
                 Holtart.Ki();
                 MessageBox.Show("Az Excel tábla elkészült !", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 MyE.Megnyitás(_fájlexc);
             });
        }

        private void SZál_ABadatbázis(Action callback)
        {
            Thread proc = new Thread(() =>
            {
                // elkészítjük a formanyomtatványt változókat nem lehet küldeni definiálni kell egy külső változót
                MyE.EXCELtábla(_AdatTábla, _fájlexc);

                this.Invoke(callback, new object[] { });
            });
            proc.Start();
        }

        private void Tábla_lekérdezés_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Kiüríti_lapfül();
                if (e.RowIndex < 0) return;

                Sorszám.Text = Tábla_lekérdezés.Rows[e.RowIndex].Cells[21].Value.ToString();

                Vizsgsorszám.Text = Tábla_lekérdezés.Rows[e.RowIndex].Cells[2].Value.ToString();
                Vizsgfok.Text = Tábla_lekérdezés.Rows[e.RowIndex].Cells[1].Value.ToString();
                Vizsgdátumk.Value = DateTime.Parse(Tábla_lekérdezés.Rows[e.RowIndex].Cells[3].Value.ToString());
                Vizsgdátumv.Value = DateTime.Parse(Tábla_lekérdezés.Rows[e.RowIndex].Cells[4].Value.ToString());
                VizsgKm.Text = Tábla_lekérdezés.Rows[e.RowIndex].Cells[5].Value.ToString();
                Üzemek.Text = Tábla_lekérdezés.Rows[e.RowIndex].Cells[22].Value.ToString();

                KMUkm.Text = Tábla_lekérdezés.Rows[e.RowIndex].Cells[7].Value.ToString();
                Jjavszám.Text = Tábla_lekérdezés.Rows[e.RowIndex].Cells[10].Value.ToString();
                Utolsófelújításdátuma.Value = DateTime.Parse(Tábla_lekérdezés.Rows[e.RowIndex].Cells[11].Value.ToString());


                TEljesKmText.Text = Tábla_lekérdezés.Rows[e.RowIndex].Cells[13].Value.ToString();
                CiklusrendCombo.Text = Tábla_lekérdezés.Rows[e.RowIndex].Cells[12].Value.ToString();

                HaviKm.Text = Tábla_lekérdezés.Rows[e.RowIndex].Cells[9].Value.ToString();
                KMUdátum.Value = DateTime.Parse(Tábla_lekérdezés.Rows[e.RowIndex].Cells[6].Value.ToString());

                KövV.Text = Tábla_lekérdezés.Rows[e.RowIndex].Cells[23].Value.ToString();
                KövV_Sorszám.Text = Tábla_lekérdezés.Rows[e.RowIndex].Cells[24].Value.ToString();
                KövV2.Text = Tábla_lekérdezés.Rows[e.RowIndex].Cells[25].Value.ToString();
                KövV2_Sorszám.Text = Tábla_lekérdezés.Rows[e.RowIndex].Cells[26].Value.ToString();
                KövV2_számláló.Text = Tábla_lekérdezés.Rows[e.RowIndex].Cells[27].Value.ToString();

                KövV1km.Text = (int.Parse(KMUkm.Text) - int.Parse(VizsgKm.Text)).ToString();
                KövV2km.Text = (int.Parse(KMUkm.Text) - int.Parse(KövV2_számláló.Text)).ToString();


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


        #region Utolsó vizsgálati adatok lapfül
        private void Új_adat_Click(object sender, EventArgs e)
        {
            Kiüríti_lapfül();
        }

        private void Kiüríti_lapfül()
        {
            Sorszám.Text = "";

            Vizsgsorszám.Text = "0";
            Vizsgfok.Text = "";
            Vizsgdátumk.Value = DateTime.Today;
            Vizsgdátumv.Value = DateTime.Today;
            VizsgKm.Text = "0";
            Üzemek.Text = "";

            KMUkm.Text = "0";
            KMUdátum.Value = DateTime.Today;

            HaviKm.Text = "0";
            KMUdátum.Value = DateTime.Today;

            KövV.Text = "";
            KövV_Sorszám.Text = "";
            KövV1km.Text = "0";
            KövV2.Text = "";
            KövV2_Sorszám.Text = "";
            KövV2_számláló.Text = "0";
            KövV2km.Text = "0";
        }

        private void Vizsgsorszámcombofeltölés()
        {
            try
            {
                Vizsgsorszám.Items.Clear();

                if (CiklusrendCombo.Text.Trim() == "") return;
                List<Adat_Ciklus> Adatok = KézCiklus.Lista_Adatok();
                Adatok = (from a in Adatok
                          where a.Típus.Trim() == CiklusrendCombo.Text.Trim()
                          orderby a.Sorszám
                          select a).ToList();

                foreach (Adat_Ciklus Elem in Adatok)
                    Vizsgsorszám.Items.Add(Elem.Sorszám);
                Vizsgsorszám.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CiklusrendCombo_feltöltés()
        {
            try
            {
                CiklusrendCombo.Items.Clear();
                List<string> CiklusTípus = (from a in AdatokCiklus
                                            orderby a.Típus
                                            select a.Típus).Distinct().ToList();
                foreach (string Elem in CiklusTípus)
                    CiklusrendCombo.Items.Add(Elem);

                CiklusrendCombo.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CiklusrendCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Vizsgsorszámcombofeltölés();
        }

        private void Üzemek_listázása()
        {
            try
            {
                Üzemek.Items.Clear();
                List<Adat_kiegészítő_telephely> Adatok = KézKieg.Lista_Adatok();
                foreach (Adat_kiegészítő_telephely Elem in Adatok)
                    Üzemek.Items.Add(Elem.Telephelykönyvtár);
                Üzemek.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Vizsgsorszám_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int i = Vizsgsorszám.SelectedIndex;
                if (CiklusrendCombo.Text.Trim() == "") return;

                List<Adat_Ciklus> CiklusAdat = KézCiklus.Lista_Adatok();
                CiklusAdat = CiklusAdat.Where(a => a.Típus.Trim() == CiklusrendCombo.Text.Trim()).OrderBy(a => a.Sorszám).ToList();
                string Vizsgálatfok = (from a in CiklusAdat
                                       where a.Sorszám == i
                                       select a.Vizsgálatfok).FirstOrDefault();

                if (Vizsgálatfok != null)
                    Vizsgfok.Text = Vizsgálatfok;

                // következő vizsgálat sorszáma
                Vizsgálatfok = (from a in CiklusAdat
                                where a.Sorszám == i + 1
                                select a.Vizsgálatfok).FirstOrDefault();
                if (Vizsgálatfok != null)
                    KövV.Text = Vizsgálatfok;

                KövV_Sorszám.Text = (i + 1).ToString();
                // követekező V2-V3
                KövV2.Text = "J";
                KövV2_Sorszám.Text = "0";
                for (int j = i + 1; j < CiklusAdat.Count; j++)
                {
                    if (CiklusAdat[j].Vizsgálatfok.Contains("V2"))
                    {
                        KövV2.Text = CiklusAdat[j].Vizsgálatfok;
                        KövV2_Sorszám.Text = j.ToString();
                        break;
                    }
                    if (CiklusAdat[j].Vizsgálatfok.Contains("V3"))
                    {
                        KövV2.Text = CiklusAdat[j].Vizsgálatfok;
                        KövV2_Sorszám.Text = j.ToString();
                        break;
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

        private void Utolsó_V_rögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                // leellenőrizzük, hogy minden adat ki van-e töltve
                if (!int.TryParse(VizsgKm.Text, out int vizsgkm)) throw new HibásBevittAdat("Vizsgálat km számláló állása mező nem lehet üres és egész számnak kell lennie.");
                if (Vizsgfok.Text.Trim() == "") throw new HibásBevittAdat("Vizsgálat foka mezőt ki kell tölteni");
                if (!int.TryParse(Vizsgsorszám.Text, out int vizsgsorszám)) throw new HibásBevittAdat("Vizsgálat sorszáma mező nem lehet üres és egész számnak kell lennie.");
                if (!int.TryParse(KMUkm.Text, out int kmukm)) throw new HibásBevittAdat("Utolsó felújítás óta futott km mező nem lehet üres és egész számnak kell lennie.");
                if (!int.TryParse(HaviKm.Text, out int havikm)) throw new HibásBevittAdat("Havi futásteljesítmény mező nem lehet üres és egész számnak kell lennie.");
                if (!int.TryParse(Jjavszám.Text, out int jjavszám)) throw new HibásBevittAdat("Felújítás sorszáma mező nem lehet üres és egész számnak kell lennie.");
                if (!int.TryParse(TEljesKmText.Text, out int teljesKmText)) throw new HibásBevittAdat("Üzembehelyezés óta futott km mező nem lehet üres és egész számnak kell lennie.");
                if (CiklusrendCombo.Text.Trim() == "") throw new HibásBevittAdat("Ütemezés típusa mezőt ki kell tölteni");
                if (!int.TryParse(KövV2_Sorszám.Text, out int kövV2_Sorszám)) throw new HibásBevittAdat("Következő V2-V3 sorszám mező nem lehet üres és egész számnak kell lennie.");
                if (!int.TryParse(KövV_Sorszám.Text, out int kövV_Sorszám)) throw new HibásBevittAdat("Következő V mező nem lehet üres és egész számnak kell lennie.");
                if (!int.TryParse(KövV2km.Text, out int kövV2km)) throw new HibásBevittAdat("V2-V3-tól futott km mező nem lehet üres és egész számnak kell lennie.");
                if (!long.TryParse(KövV2_számláló.Text, out long kövV2_számláló)) throw new HibásBevittAdat("V2-V3 számláló állás mező nem lehet üres és egész számnak kell lennie.");

                // megnézzük az adatbázist, ha nincs ilyen kocsi T5C5 benne akkor rögzít máskülönben az adatokat módosítja
                List<Adat_Jármű> AdatokJármű = KézJármű.Lista_Adatok("Főmérnökség");

                Adat_Jármű ElemJármű = (from a in AdatokJármű
                                        where a.Azonosító == Pályaszám.Text.Trim()
                                        && (a.Valóstípus.Contains("T5C5") || a.Típus.Contains("T5C5"))
                                        select a).FirstOrDefault();

                if (ElemJármű != null)
                {
                    AdatokKmAdatok = KézKmAdatok.Lista_Adatok().OrderByDescending(a => a.ID).ToList();
                    if (!long.TryParse(Sorszám.Text, out long sorszám))
                    {
                        sorszám = 1;
                        if (AdatokKmAdatok.Count > 0) sorszám = AdatokKmAdatok.Max(a => a.ID) + 1;
                    }

                    Adat_T5C5_Kmadatok ADAT = new Adat_T5C5_Kmadatok(
                        sorszám,
                        MyF.Szöveg_Tisztítás(Pályaszám.Text.Trim()),
                        jjavszám,
                        kmukm,
                        KMUdátum.Value,
                        MyF.Szöveg_Tisztítás(Vizsgfok.Text.Trim()),
                        Vizsgdátumk.Value,
                        Vizsgdátumv.Value,
                        vizsgkm,
                        havikm,
                        vizsgsorszám,
                        Utolsófelújításdátuma.Value,
                        teljesKmText,
                        MyF.Szöveg_Tisztítás(CiklusrendCombo.Text.Trim()),
                        MyF.Szöveg_Tisztítás(Üzemek.Text.Trim()),
                        kövV2_Sorszám,
                        MyF.Szöveg_Tisztítás(KövV2.Text.Trim()),
                        kövV_Sorszám,
                        MyF.Szöveg_Tisztítás(KövV.Text.Trim()),
                        false,
                        kövV2_számláló);


                    if (Sorszám.Text == "")
                        KézKmAdatok.Rögzítés(ADAT);                          // Új adat
                    else
                        KézKmAdatok.Módosítás(ADAT);  // módosítjuk az adatokat
                    MessageBox.Show("Az adatok rögzítése megtörtént. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("A pályaszám nem T5C5! ", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Kiirjaatörténelmet();
                Fülek.SelectedIndex = 4;

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (long.TryParse(Sorszám.Text.Trim(), out long sorSzám))
                {
                    if (MessageBox.Show("Valóban töröljük az adatsort?", "Biztonsági kérdés", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        KézKmAdatok.Törlés(sorSzám);
                        Kiirjaatörténelmet();
                        Fülek.SelectedIndex = 4;
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


        #region Vizsgálati adatok lapfül
        private void Kiirjaatörténelmet()
        {
            try
            {
                AdatokKmAdatok = KézKmAdatok.Lista_Adatok().Where(a => a.Törölt == false).ToList();

                Tábla1.Rows.Clear();
                Tábla1.Columns.Clear();
                Tábla1.Refresh();
                Tábla1.Visible = false;
                Tábla1.ColumnCount = 21;

                // fejléc elkészítése
                Tábla1.Columns[0].HeaderText = "Ssz.";
                Tábla1.Columns[0].Width = 80;
                Tábla1.Columns[1].HeaderText = "Psz";
                Tábla1.Columns[1].Width = 80;
                Tábla1.Columns[2].HeaderText = "Vizsg. foka";
                Tábla1.Columns[2].Width = 80;
                Tábla1.Columns[3].HeaderText = "Vizsg. Ssz.";
                Tábla1.Columns[3].Width = 80;
                Tábla1.Columns[4].HeaderText = "Vizsg. Kezdete";
                Tábla1.Columns[4].Width = 110;
                Tábla1.Columns[5].HeaderText = "Vizsg. Vége";
                Tábla1.Columns[5].Width = 110;
                Tábla1.Columns[6].HeaderText = "Vizsg KM állás";
                Tábla1.Columns[6].Width = 80;
                Tábla1.Columns[7].HeaderText = "Frissítés Dátum";
                Tábla1.Columns[7].Width = 110;
                Tábla1.Columns[8].HeaderText = "KM J-óta";
                Tábla1.Columns[8].Width = 80;
                Tábla1.Columns[9].HeaderText = "V után futott";
                Tábla1.Columns[9].Width = 80;
                Tábla1.Columns[10].HeaderText = "Havi km";
                Tábla1.Columns[10].Width = 80;
                Tábla1.Columns[11].HeaderText = "Felújítás szám";
                Tábla1.Columns[11].Width = 80;
                Tábla1.Columns[12].HeaderText = "Felújítás Dátum";
                Tábla1.Columns[12].Width = 110;
                Tábla1.Columns[13].HeaderText = "Ciklusrend típus";
                Tábla1.Columns[13].Width = 120;
                Tábla1.Columns[14].HeaderText = "Üzembehelyezés km";
                Tábla1.Columns[14].Width = 80;
                Tábla1.Columns[15].HeaderText = "Végezte";
                Tábla1.Columns[15].Width = 120;
                Tábla1.Columns[16].HeaderText = "Következő V";
                Tábla1.Columns[16].Width = 120;
                Tábla1.Columns[17].HeaderText = "Következő V Ssz.";
                Tábla1.Columns[17].Width = 120;
                Tábla1.Columns[18].HeaderText = "Következő V2-V3";
                Tábla1.Columns[18].Width = 120;
                Tábla1.Columns[19].HeaderText = "Következő V2-V3 Ssz.";
                Tábla1.Columns[19].Width = 120;
                Tábla1.Columns[20].HeaderText = "Utolsó V2-V3 számláló";
                Tábla1.Columns[20].Width = 120;

                List<Adat_T5C5_Kmadatok> Adatok = (from a in AdatokKmAdatok
                                                   where a.Azonosító == Pályaszám.Text.Trim()
                                                   orderby a.Vizsgdátumk
                                                   select a).ToList();

                foreach (Adat_T5C5_Kmadatok rekord in Adatok)
                {


                    Tábla1.RowCount++;
                    int i = Tábla1.RowCount - 1;
                    Tábla1.Rows[i].Cells[0].Value = rekord.ID;
                    Tábla1.Rows[i].Cells[1].Value = rekord.Azonosító;
                    Tábla1.Rows[i].Cells[2].Value = rekord.Vizsgfok;
                    Tábla1.Rows[i].Cells[3].Value = rekord.Vizsgsorszám;
                    Tábla1.Rows[i].Cells[4].Value = rekord.Vizsgdátumk.ToString("yyyy.MM.dd");
                    Tábla1.Rows[i].Cells[5].Value = rekord.Vizsgdátumv.ToString("yyyy.MM.dd");
                    Tábla1.Rows[i].Cells[6].Value = rekord.Vizsgkm;
                    Tábla1.Rows[i].Cells[7].Value = rekord.KMUdátum.ToString("yyyy.MM.dd");
                    Tábla1.Rows[i].Cells[8].Value = rekord.KMUkm;

                    if (rekord.Vizsgsorszám == 0)
                    {
                        // ha J akkor nem kell különbséget képezni
                        Tábla1.Rows[i].Cells[9].Value = rekord.KMUkm;
                    }
                    else
                    {
                        Tábla1.Rows[i].Cells[9].Value = (rekord.KMUkm - rekord.Vizsgkm);
                    }
                    Tábla1.Rows[i].Cells[10].Value = rekord.Havikm;
                    Tábla1.Rows[i].Cells[11].Value = rekord.Jjavszám;
                    Tábla1.Rows[i].Cells[12].Value = rekord.Fudátum.ToString("yyyy.MM.dd");
                    Tábla1.Rows[i].Cells[13].Value = rekord.Ciklusrend;
                    Tábla1.Rows[i].Cells[14].Value = rekord.Teljeskm;
                    if (rekord.V2végezte.Trim() != "_")
                        Tábla1.Rows[i].Cells[15].Value = rekord.V2végezte.Trim();
                    Tábla1.Rows[i].Cells[16].Value = rekord.KövV;
                    Tábla1.Rows[i].Cells[17].Value = rekord.KövV_sorszám;
                    Tábla1.Rows[i].Cells[18].Value = rekord.KövV2;
                    Tábla1.Rows[i].Cells[19].Value = rekord.KövV2_sorszám;
                    Tábla1.Rows[i].Cells[20].Value = rekord.V2V3Számláló;
                }

                Tábla1.Visible = true;
                Tábla1.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Tábla1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Kiüríti_lapfül();
            if (e.RowIndex < 0) return;

            Sorszám.Text = Tábla1.Rows[e.RowIndex].Cells[0].Value.ToString();

            Vizsgsorszám.Text = Tábla1.Rows[e.RowIndex].Cells[3].Value.ToString();
            Vizsgfok.Text = Tábla1.Rows[e.RowIndex].Cells[2].Value.ToString();
            Vizsgdátumk.Value = DateTime.Parse(Tábla1.Rows[e.RowIndex].Cells[4].Value.ToString());
            Vizsgdátumv.Value = DateTime.Parse(Tábla1.Rows[e.RowIndex].Cells[5].Value.ToString());
            VizsgKm.Text = Tábla1.Rows[e.RowIndex].Cells[6].Value.ToString();
            Üzemek.Text = Tábla1.Rows[e.RowIndex].Cells[15].Value.ToString();

            KMUkm.Text = Tábla1.Rows[e.RowIndex].Cells[8].Value.ToString();
            Jjavszám.Text = Tábla1.Rows[e.RowIndex].Cells[11].Value.ToString();
            Utolsófelújításdátuma.Value = DateTime.Parse(Tábla1.Rows[e.RowIndex].Cells[12].Value.ToString());


            TEljesKmText.Text = Tábla1.Rows[e.RowIndex].Cells[14].Value.ToString();
            CiklusrendCombo.Text = Tábla1.Rows[e.RowIndex].Cells[13].Value.ToString();

            HaviKm.Text = Tábla1.Rows[e.RowIndex].Cells[10].Value.ToString();
            KMUdátum.Value = DateTime.Parse(Tábla1.Rows[e.RowIndex].Cells[7].Value.ToString());

            KövV.Text = Tábla1.Rows[e.RowIndex].Cells[16].Value.ToString();
            KövV_Sorszám.Text = Tábla1.Rows[e.RowIndex].Cells[17].Value.ToString();
            KövV2.Text = Tábla1.Rows[e.RowIndex].Cells[18].Value.ToString();
            KövV2_Sorszám.Text = Tábla1.Rows[e.RowIndex].Cells[19].Value.ToString();
            KövV2_számláló.Text = Tábla1.Rows[e.RowIndex].Cells[20].Value.ToString();

            KövV1km.Text = (int.Parse(KMUkm.Text) - int.Parse(VizsgKm.Text)).ToString();
            KövV2km.Text = (int.Parse(KMUkm.Text) - int.Parse(KövV2_számláló.Text)).ToString();


            Fülek.SelectedIndex = 3;
        }
        #endregion


        #region Állomány tábla
        private void Excel_gomb_Click(object sender, EventArgs e)
        {
            try
            {
                List<Adat_Jármű> Adatok = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                List<string> Elemek = new List<string> { "Azonosító", "Típus" };
                if (Adatok.Count <= 0) return;
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    // kimeneti fájl helye és neve
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Állománytábla_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;
                DataTable TáblaAdat = MyF.ToDataTable(Adatok);
                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(TáblaAdat, fájlexc, Elemek);
                Tábla_lekérdezés.Rows.Clear();
                Tábla_lekérdezés.Columns.Clear();

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


        #region SAP betöltés
        private async void SAP_adatok_Click(object sender, EventArgs e)
        {
            try
            {
                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "SAP-s Adatok betöltése",
                    FileName = "",
                    Filter = "Excel (*.xlsx)|*.xlsx|Excel 97-2003 (*.xls)|*.xls"
                };

                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    _fájlexc = OpenFileDialog1.FileName;
                else
                    return;


                timer1.Enabled = true;
                Holtart.Be();
                await Task.Run(() => SAP_Adatokbeolvasása.Km_beolvasó(_fájlexc, "T5C5"));
                timer1.Enabled = false;
                Holtart.Ki();
                MessageBox.Show("Az adatok beolvasása megtörtént !", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Holtart.Lép();
        }
        #endregion


        #region előtervező

        private void Pszlista()
        {
            try
            {
                PszJelölő.Items.Clear();
                List<Adat_Jármű> Adatok = KézJármű.Lista_Adatok("Főmérnökség");
                Adatok = (from a in Adatok
                          where a.Törölt == false
                          && a.Valóstípus.Contains("T5C5")
                          orderby a.Azonosító
                          select a).ToList();
                foreach (Adat_Jármű Elem in Adatok)
                    PszJelölő.Items.Add(Elem.Azonosító);

                PszJelölő.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Telephelylista()
        {
            try
            {
                Telephely.Items.Clear();
                List<Adat_Jármű> Adatok = KézJármű.Lista_Adatok("Főmérnökség");
                List<string> Üzemek = (from a in Adatok
                                       where a.Törölt == false
                                       && a.Valóstípus.Contains("T5C5")
                                       orderby a.Üzem
                                       select a.Üzem).Distinct().ToList();
                foreach (string Elem in Üzemek)
                    Telephely.Items.Add(Elem);
                Telephely.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Option5_Click(object sender, EventArgs e)
        {
            // Kocsi havi km
            Havikm = 0;
        }

        private void Option6_Click(object sender, EventArgs e)
        {
            // telephely átlag
            int i;
            if (Telephely.Text.Trim() == "")
            {
                Option8.Checked = true;
                Text1.Text = "5000";
                return;
            }
            for (i = 0; i < PszJelölő.Items.Count; i++)
                PszJelölő.SetItemChecked(i, false);

            Frissíti_a_pályaszámokat();
            // kilistázzuk a adatbázis adatait
            AdatokKmAdatok = KézKmAdatok.Lista_Adatok().Where(a => a.Törölt == false).ToList();

            double típusátlag = 0d;
            i = 0;
            FőHoltart.Be(PszJelölő.Items.Count + 1);

            for (int j = 0; j < PszJelölő.Items.Count; j++)
            {
                FőHoltart.Lép();
                if (PszJelölő.GetItemChecked(j))
                {
                    Adat_T5C5_Kmadatok Elem = (from a in AdatokKmAdatok
                                               where a.Azonosító == PszJelölő.Items[j].ToStrTrim()
                                               orderby a.Vizsgdátumk descending
                                               select a).FirstOrDefault();
                    if (Elem != null)
                    {
                        típusátlag += Elem.Havikm;
                        i += 1;
                    }
                }
            }
            FőHoltart.Ki();
            if (i != 0) típusátlag /= i;
            Havikm = ((long)Math.Round(típusátlag));
            Text1.Text = Havikm.ToString();
        }

        private void Option7_Click(object sender, EventArgs e)
        {
            // típusátlag

            AdatokKmAdatok = KézKmAdatok.Lista_Adatok().Where(a => a.Törölt == false).ToList();

            double típusátlag = 0;
            int i = 0;
            FőHoltart.Be(PszJelölő.Items.Count + 1);

            for (int j = 0; j < PszJelölő.Items.Count; j++)
            {
                FőHoltart.Lép();
                Adat_T5C5_Kmadatok Elem = (from a in AdatokKmAdatok
                                           where a.Azonosító == PszJelölő.Items[j].ToStrTrim()
                                           orderby a.Vizsgdátumk descending
                                           select a).FirstOrDefault();
                if (Elem != null)
                {
                    típusátlag += Elem.Havikm;
                    i += 1;
                }
            }
            FőHoltart.Ki();
            if (i != 0) típusátlag /= i;
            Havikm = (long)Math.Round(típusátlag);
            Text1.Text = Havikm.ToString();
        }

        private void Option9_Click(object sender, EventArgs e)
        {
            // 'kijelöltek átlaga
            AdatokKmAdatok = KézKmAdatok.Lista_Adatok().Where(a => a.Törölt == false).ToList();
            double típusátlag = 0d;
            int i = 0;
            FőHoltart.Be(PszJelölő.Items.Count + 1);

            for (int j = 0; j < PszJelölő.Items.Count; j++)
            {
                FőHoltart.Lép();
                if (PszJelölő.GetItemChecked(j))
                {
                    Adat_T5C5_Kmadatok Elem = (from a in AdatokKmAdatok
                                               where a.Azonosító == PszJelölő.Items[j].ToStrTrim()
                                               orderby a.Vizsgdátumk descending
                                               select a).FirstOrDefault();
                    if (Elem != null)
                    {
                        típusátlag += Elem.Havikm;
                        i += 1;
                    }
                }
            }
            FőHoltart.Ki();
            if (i != 0)
                típusátlag /= i;
            Havikm = (long)Math.Round(típusátlag);
            Text1.Text = Havikm.ToString();
        }

        private void Option8_Click(object sender, EventArgs e)
        {

        }

        private void Frissíti_a_pályaszámokat()
        {
            try
            {
                if (Telephely.Text.Trim() == "") return;

                List<Adat_Jármű> Adatok = KézJármű.Lista_Adatok("Főmérnökség");
                Adatok = (from a in Adatok
                          where a.Törölt == false
                          && a.Valóstípus.Contains("T5C5")
                          && a.Üzem == Telephely.Text.Trim()
                          orderby a.Azonosító
                          select a).ToList();

                int i = 0;
                foreach (Adat_Jármű rekord in Adatok)
                {
                    while (PszJelölő.Items[i].ToStrTrim() != rekord.Azonosító.ToStrTrim())
                    {
                        i += 1;
                        if (PszJelölő.Items.Count - 1 <= i)
                            break;
                    }
                    if (PszJelölő.Items[i].ToStrTrim() == rekord.Azonosító.ToStrTrim())
                    {
                        PszJelölő.SetItemChecked(i, true);
                    }
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

        private void Command2_Click(object sender, EventArgs e)
        {
            Frissíti_a_pályaszámokat();
        }

        private void Mindentkijelöl_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < PszJelölő.Items.Count; i++)
                PszJelölő.SetItemChecked(i, true);
        }

        private void Kijelöléstörlése_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < PszJelölő.Items.Count; i++)
                PszJelölő.SetItemChecked(i, false);
        }

        private void Text2_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(Text2.Text, out int result))
            {
                Text2.Text = result.ToString();
                Hónapok = result;
            }
            else
            {
                Text2.Text = "24";
                Hónapok = 24;
            }
        }

        private void Text1_Leave(object sender, EventArgs e)
        {

            if (!int.TryParse(Text1.Text, out int result))
                Text1.Text = "";

            HaviKm.Text = result.ToString();
            Option8.Checked = true;
        }

        private void Command1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Text2.Text, out int result)) throw new HibásBevittAdat("Hónapok száma nem lehet üres és egész számnak kell lennie.");
                if (PszJelölő.CheckedItems.Count < 1) return;

                AlHoltart.Be();
                FőHoltart.Be(10);

                Alaptábla();
                FőHoltart.Lép();
                Egyhónaprögzítése();
                Excel_előtervező();
                AlHoltart.Ki();
                FőHoltart.Ki();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Alaptábla()
        {
            try
            {
                string hova = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Kmadatok.mdb";
                if (File.Exists(hova) && !Check1.Checked) File.Delete(hova);

                double kerékminimum;
                double Kerék_K11;
                double Kerék_K12;
                double Kerék_K21;
                double Kerék_K22;


                AdatokKmAdatok = KézKmAdatok.Lista_Adatok().Where(a => a.Törölt == false).ToList();
                AdatokJármű = KézJármű.Lista_Adatok("Főmérnökség");
                KerékadatokListaFeltöltés();

                // kilistázzuk a adatbázis adatait
                AlHoltart.Be(PszJelölő.Items.Count + 1);
                AlHoltart.BackColor = Color.Yellow;
                int i = 1;
                List<Adat_T5C5_Előterv> AdatokGy = new List<Adat_T5C5_Előterv>();
                for (int j = 0; j < PszJelölő.Items.Count; j++)
                {
                    if (PszJelölő.GetItemChecked(j))
                    {
                        Adat_T5C5_Kmadatok rekord = (from a in AdatokKmAdatok
                                                     where a.Azonosító == PszJelölő.Items[j].ToStrTrim()
                                                     orderby a.Vizsgdátumk descending
                                                     select a).FirstOrDefault();

                        Adat_Jármű JárműElem = (from a in AdatokJármű
                                                where a.Azonosító == PszJelölő.Items[j].ToStrTrim()
                                                select a).FirstOrDefault();

                        if (rekord != null)
                        {
                            Kerék_K11 = 0d;
                            Kerék_K12 = 0d;
                            Kerék_K21 = 0d;
                            Kerék_K22 = 0d;
                            kerékminimum = 1000d;
                            // kerék méretek
                            if (AdatokMérés != null)
                            {
                                Adat_Kerék_Mérés Elem = (from a in AdatokMérés
                                                         where a.Azonosító == rekord.Azonosító
                                                         && a.Pozíció == "K11"
                                                         orderby a.Mikor descending
                                                         select a).FirstOrDefault();
                                if (Elem != null) Kerék_K11 = Elem.Méret;

                                Elem = (from a in AdatokMérés
                                        where a.Azonosító == rekord.Azonosító
                                        && a.Pozíció == "K12"
                                        orderby a.Mikor descending
                                        select a).FirstOrDefault();
                                if (Elem != null) Kerék_K12 = Elem.Méret;

                                Elem = (from a in AdatokMérés
                                        where a.Azonosító == rekord.Azonosító
                                        && a.Pozíció == "K21"
                                        orderby a.Mikor descending
                                        select a).FirstOrDefault();
                                if (Elem != null) Kerék_K21 = Elem.Méret;

                                Elem = (from a in AdatokMérés
                                        where a.Azonosító == rekord.Azonosító
                                        && a.Pozíció == "K22"
                                        orderby a.Mikor descending
                                        select a).FirstOrDefault();
                                if (Elem != null) Kerék_K22 = Elem.Méret;
                            }

                            if (kerékminimum > Kerék_K11) kerékminimum = Kerék_K11;
                            if (kerékminimum > Kerék_K12) kerékminimum = Kerék_K12;
                            if (kerékminimum > Kerék_K21) kerékminimum = Kerék_K21;
                            if (kerékminimum > Kerék_K22) kerékminimum = Kerék_K22;
                            Adat_T5C5_Előterv ADAT = new Adat_T5C5_Előterv(
                                              i,
                                              rekord.Azonosító.ToStrTrim(),
                                              rekord.Jjavszám,
                                              rekord.KMUkm,
                                              rekord.KMUdátum,
                                              rekord.Vizsgfok.Trim(),
                                              rekord.Vizsgdátumk,
                                              rekord.Vizsgdátumv,
                                              rekord.Vizsgkm,
                                              rekord.Havikm,
                                              rekord.Vizsgsorszám,
                                              rekord.Fudátum,
                                              rekord.Teljeskm,
                                              rekord.Ciklusrend.Trim(),
                                              rekord.V2végezte.Trim(),
                                              rekord.KövV2_sorszám,
                                              rekord.KövV2.ToStrTrim(),
                                              rekord.KövV_sorszám,
                                              rekord.KövV.Trim(),
                                              false,
                                              JárműElem.Üzem,
                                              0,
                                              Kerék_K11,
                                              Kerék_K12,
                                              Kerék_K21,
                                              Kerék_K22,
                                              kerékminimum,
                                              rekord.V2V3Számláló);
                            AdatokGy.Add(ADAT);
                            i += 1;
                        }

                        AlHoltart.Lép();
                    }
                }
                KézElőterv.Rögzítés(hova, AdatokGy);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Egyhónaprögzítése()
        {
            try
            {
                string hova = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Kmadatok.mdb";
                if (!File.Exists(hova)) return;

                FőHoltart.Be();
                AlHoltart.Be(Hónapok + 3);
                AlHoltart.BackColor = Color.Green;

                // beolvassuk a ID sorszámot, majd növeljük minden rögzítésnél
                List<Adat_T5C5_Előterv> TervAdatok = KézElőterv.Lista_Adatok(hova).OrderByDescending(a => a.ID).ToList();
                long id_sorszám = 1;
                if (TervAdatok.Count > 0) id_sorszám = TervAdatok.Max(a => a.ID);

                TervAdatok = TervAdatok.OrderByDescending(a => a.Vizsgdátumv).ToList();

                List<Adat_Ciklus> CiklusAdat = KézCiklus.Lista_Adatok();

                List<Adat_T5C5_Előterv> AdatokGy = new List<Adat_T5C5_Előterv>();
                for (int j = 0; j < PszJelölő.CheckedItems.Count; j++)
                {
                    Adat_T5C5_Előterv rekordhova = (from a in TervAdatok
                                                    where a.Azonosító == PszJelölő.CheckedItems[j].ToStrTrim()
                                                    orderby a.Vizsgdátumv descending
                                                    select a).FirstOrDefault();

                    if (rekordhova != null)
                    {
                        long ideigvizsgsorszám = rekordhova.Vizsgsorszám;
                        long ideighavikm = rekordhova.Havikm;
                        long ideigKMUkm = rekordhova.KMUkm;
                        long ideigvizsgkm = rekordhova.Vizsgkm;
                        long figyelő = 0;
                        long különbözet = 0;
                        string ideigazonosító = rekordhova.Azonosító.Trim();
                        long ideigjjavszám = rekordhova.Jjavszám;
                        DateTime ideigKMUdátum = rekordhova.KMUdátum;
                        string ideigvizsgfok = rekordhova.Vizsgfok;
                        DateTime ideigvizsgdátumk = rekordhova.Vizsgdátumk;
                        DateTime ideigvizsgdátumv = rekordhova.Vizsgdátumv;
                        DateTime ideigfudátum = rekordhova.Fudátum;
                        long ideigTeljeskm = rekordhova.Teljeskm;
                        string ideigCiklusrend = rekordhova.Ciklusrend;
                        string ideigV2végezte = "Előterv";
                        long ideigkövV2_sorszám = rekordhova.KövV2_sorszám;
                        string ideigkövV2 = rekordhova.KövV2;
                        long ideigkövV_sorszám = rekordhova.KövV_sorszám;
                        string ideigKövV = rekordhova.KövV;
                        bool ideigtörölt = rekordhova.Törölt;
                        string ideigHonostelephely = rekordhova.Honostelephely;
                        long ideigtervsorszám = rekordhova.Tervsorszám;
                        double ideigkerék_11 = rekordhova.Kerék_K11;
                        double ideigkerék_12 = rekordhova.Kerék_K12;
                        double ideigkerék_21 = rekordhova.Kerék_K21;
                        double ideigkerék_22 = rekordhova.Kerék_K22;
                        double ideigkerék_min = rekordhova.Kerék_min;
                        long ideigV2V3számláló = rekordhova.V2V3Számláló;

                        for (int i = 1; i < Hónapok; i++)
                        {
                            DateTime elődátum = DateTime.Today.AddMonths(i);
                            Adat_Ciklus CiklusElem = (from a in AdatokCiklus
                                                      where a.Típus == rekordhova.Ciklusrend
                                                      && a.Sorszám == ideigvizsgsorszám
                                                      select a).FirstOrDefault();
                            // megnézzük, hogy mi a ciklus határa
                            long Alsó = 0;
                            long Felső = 0;
                            long Névleges = 0;
                            long sorszám = 0;
                            long Mennyi = 0;
                            if (CiklusElem != null)
                            {
                                Alsó = CiklusElem.Alsóérték;
                                Felső = CiklusElem.Felsőérték;
                                Névleges = CiklusElem.Névleges;
                                sorszám = CiklusElem.Sorszám;
                            }
                            if (Option10.Checked) Mennyi = Alsó;
                            if (Option11.Checked) Mennyi = Névleges;
                            if (Option12.Checked) Mennyi = Felső;

                            // megnézzük a következő V-t
                            CiklusElem = (from a in AdatokCiklus
                                          where a.Típus == rekordhova.Ciklusrend
                                          && a.Sorszám == sorszám + 1
                                          select a).FirstOrDefault();

                            string következőv = "";
                            if (CiklusElem != null)
                                következőv = CiklusElem.Vizsgálatfok;       // ha talált akkor
                            else
                                következőv = "J";   // ha nem talált

                            // az utolsó rögzített adatot megvizsgáljuk, hogy a havi km-et át lépjük -e fokozatot
                            figyelő = ideigKMUkm - ideigvizsgkm + Havikm;

                            if (Mennyi <= figyelő)
                            {
                                különbözet = ideigKMUkm - ideigvizsgkm + Havikm - Mennyi;
                                // módosítjuk a határig tartó adatokat
                                ideigKMUkm = ideigKMUkm + Havikm - különbözet;
                                ideigTeljeskm = ideigTeljeskm + Havikm - különbözet;
                                id_sorszám += 1;
                                ideigvizsgkm += Mennyi;
                                ideigTeljeskm += Havikm;
                                ideigKMUdátum = elődátum;
                                ideigvizsgfok = következőv;
                                ideigvizsgdátumk = elődátum;
                                ideigvizsgdátumv = elődátum;
                                ideigtervsorszám += 1;
                                ideigkerék_11 -= double.Parse(Kerékcsökkenés.Text);
                                ideigkerék_12 -= double.Parse(Kerékcsökkenés.Text);
                                ideigkerék_21 -= double.Parse(Kerékcsökkenés.Text);
                                ideigkerék_22 -= double.Parse(Kerékcsökkenés.Text);
                                ideigkerék_min -= double.Parse(Kerékcsökkenés.Text);
                                // rögzítjük és egy ciklussal feljebb emeljük
                                if (következőv == "J")
                                {
                                    ideigvizsgsorszám = 0;
                                    ideigKMUkm = 0;
                                    ideigfudátum = elődátum;
                                    ideigjjavszám += 1;
                                    ideigvizsgkm = 0;
                                }
                                else
                                {
                                    ideigvizsgsorszám += 1;
                                }
                                Adat_T5C5_Előterv ADAT = new Adat_T5C5_Előterv(
                                       id_sorszám,
                                       ideigazonosító,
                                       ideigjjavszám,
                                       ideigKMUkm,
                                       ideigKMUdátum,
                                       ideigvizsgfok,
                                       ideigvizsgdátumk,
                                       ideigvizsgdátumv,
                                       ideigvizsgkm,
                                       ideighavikm,
                                       ideigvizsgsorszám,
                                       ideigfudátum,
                                       ideigTeljeskm,
                                       ideigCiklusrend,
                                       ideigV2végezte,
                                       ideigkövV2_sorszám,
                                       ideigkövV2,
                                       ideigkövV_sorszám,
                                       ideigKövV,
                                       false,
                                       ideigHonostelephely,
                                       ideigtervsorszám,
                                       ideigkerék_11,
                                       ideigkerék_12,
                                       ideigkerék_21,
                                       ideigkerék_22,
                                       ideigkerék_min,
                                       ideigV2V3számláló);
                                AdatokGy.Add(ADAT);
                            }
                            else
                            {
                                // módosítjuk az utolsó adatsort

                                if (ideigKMUkm == 0) // ha felújítva volt és nem lett lenullázva
                                {
                                    ideigvizsgkm = 0;
                                }
                                ideigKMUkm += Havikm;
                                ideigTeljeskm += Havikm;
                            }
                            AlHoltart.Lép();
                        }
                    }
                    FőHoltart.Lép();
                }
                KézElőterv.Rögzítés(hova, AdatokGy);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Excel_előtervező()
        {
            try
            {
                string[] cím = new string[5];
                string[] Leírás = new string[5];

                // paraméter tábla feltöltése
                cím[1] = "Adatok";
                Leírás[1] = "Előtervezett adatok";
                cím[2] = "Vizsgálatok";
                Leírás[2] = "Vizsgálati adatok havonta";
                cím[3] = "Éves_terv";
                Leírás[3] = "Vizsgálati adatok éves";
                cím[4] = "Éves_havi_terv";
                Leírás[4] = "Vizsgálati adatok éves/havi";

                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    // kimeneti fájl helye és neve
                    InitialDirectory = "MyDocuments",

                    Title = "Vizsgálat előtervező",
                    FileName = "V_javítások_előtervezése_" + DateTime.Now.ToString("yyyyMMddhhmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;


                // megnyitjuk
                MyE.ExcelLétrehozás();

                // ****************************************************
                // elkészítjük a lapokat
                // ****************************************************
                string munkalap = "Munka1";
                MyE.Munkalap_átnevezés(munkalap, "Tartalom");
                munkalap = "Tartalom";

                for (int i = 1; i <= 4; i++)
                    MyE.Új_munkalap(cím[i].Trim());


                // ****************************************************
                // Elkészítjük a tartalom jegyzéket
                // ****************************************************
                MyE.Munkalap_aktív(munkalap);

                MyE.Kiir("Munkalapfül", "a1");
                MyE.Kiir("Leírás", "b1");
                for (int i = 1; i <= 4; i++)
                {
                    MyE.Kiir(cím[i], "A" + (i + 1).ToString());
                    MyE.Link_beillesztés(munkalap, "B" + (i + 1).ToString(), cím[i].Trim());
                    MyE.Kiir(Leírás[i], "B" + (i + 1).ToString());
                }
                MyE.Oszlopszélesség(munkalap, "A:B");

                // ****************************************************
                // Elkészítjük a munkalapokat
                // ****************************************************
                FőHoltart.Be(4);
                Adatoklistázása();
                FőHoltart.Lép();
                Kimutatás();
                FőHoltart.Lép();
                Kimutatás1();
                FőHoltart.Lép();
                Kimutatás2();

                MyE.Munkalap_aktív(munkalap);
                MyE.Aktív_Cella(munkalap, "A1");

                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();
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

        private void Adatoklistázása()
        {
            try
            {
                string munkalap = "Adatok";
                MyE.Munkalap_aktív(munkalap);
                MyE.Link_beillesztés(munkalap, "A1", "Tartalom");
                MyE.Munkalap_aktív(munkalap);

                // megnyitjuk az adatbázist
                string hely = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Kmadatok.mdb";
                DataTable dataTable = MyF.ToDataTable(KézElőterv.Lista_Adatok(hely));
                utolsósor = MyE.Munkalap(dataTable, 3, munkalap);

                //// fejlécet kiírjuk
                MyE.Kiir("ID", "a3");
                MyE.Kiir("Pályaszám", "b3");
                MyE.Kiir("Jjavszám", "c3");
                MyE.Kiir("KMUkm", "d3");
                MyE.Kiir("KMUdátum", "e3");
                MyE.Kiir("vizsgfok", "f3");
                MyE.Kiir("vizsgdátumkezdő", "g3");
                MyE.Kiir("vizsgdátumvég", "h3");
                MyE.Kiir("vizsgkmszámláló", "i3");
                MyE.Kiir("havikm", "j3");
                MyE.Kiir("vizsgsorszám", "k3");
                MyE.Kiir("Jdátum", "l3");
                MyE.Kiir("Teljeskm", "m3");
                MyE.Kiir("Ciklusrend", "n3");
                MyE.Kiir("V2végezte", "o3");
                MyE.Kiir("Köv V2 sorszám", "p3");
                MyE.Kiir("Köv V2", "q3");
                MyE.Kiir("Köv V sorszám", "r3");
                MyE.Kiir("köv V", "s3");
                MyE.Kiir("Törölt", "t3");
                MyE.Kiir("Módosító", "u3");
                MyE.Kiir("Módosítás dátuma", "v3");
                MyE.Kiir("Honostelephely", "w3");
                MyE.Kiir("tervsorszám", "x3");
                MyE.Kiir("Kerék_11", "y3");
                MyE.Kiir("Kerék_12", "z3");
                MyE.Kiir("Kerék_21", "aa3");
                MyE.Kiir("Kerék_22", "ab3");
                MyE.Kiir("Kerék_min", "ac3");
                MyE.Kiir("V2V3 számláló", "ad3");
                MyE.Kiir("Év", "ae3");
                MyE.Kiir("fokozat", "af3");
                MyE.Kiir("Hónap", "ag3");

                MyE.Kiir("=YEAR(RC[-23])", "AE4");
                MyE.Kiir("=LEFT(RC[-26],2)", "AF4");
                MyE.Kiir("=MONTH(RC[-25])", "AG4");

                MyE.Képlet_másol(munkalap, "AE4:AG4", "AE5:AG" + (utolsósor + 3));


                // megformázzuk
                MyE.Oszlopszélesség(munkalap, "A:AG");

                MyE.Vastagkeret("a3:AG3");
                MyE.Rácsoz("a3:AG" + (utolsósor + 3));
                MyE.Vastagkeret("a3:AG" + (utolsósor + 3));
                MyE.Vastagkeret("a3:AG3");
                // szűrő
                MyE.Szűrés(munkalap, "A3:AG" + (utolsósor + 3), 3);

                // ablaktábla rögzítése

                MyE.Tábla_Rögzítés("3:3", 3);


                // kiírjuk a tábla méretét
                MyE.Munkalap_aktív("Vizsgálatok");
                MyE.Kiir((utolsósor + 2).ToString(), "aa1");
                MyE.Munkalap_aktív("Éves_terv");
                MyE.Kiir((utolsósor + 2).ToString(), "aa1");
                MyE.Munkalap_aktív("Éves_havi_terv");
                MyE.Kiir((utolsósor + 2).ToString(), "aa1");
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Kimutatás()
        {
            try
            {
                string munkalap = "Vizsgálatok";
                MyE.Munkalap_aktív(munkalap);
                MyE.Link_beillesztés(munkalap, "A1", "Tartalom");

                string munkalap_adat = "Adatok";
                string balfelső = "A3";
                string jobbalsó = "AG" + utolsósor;
                string kimutatás_Munkalap = munkalap;
                string Kimutatás_cella = "A6";
                string Kimutatás_név = "Kimutatás";

                List<string> összesítNév = new List<string>();
                List<string> Összesít_módja = new List<string>();
                List<string> sorNév = new List<string>();
                List<string> oszlopNév = new List<string>();
                List<string> SzűrőNév = new List<string>();

                összesítNév.Add("Pályaszám");

                Összesít_módja.Add("xlCount");

                sorNév.Add("vizsgdátumkezdő");


                SzűrőNév.Add("Honostelephely");
                SzűrőNév.Add("tervsorszám");

                oszlopNév.Add("vizsgfok");

                MyE.Kimutatás_Fő(munkalap_adat, balfelső, jobbalsó, kimutatás_Munkalap, Kimutatás_cella, Kimutatás_név
                                , összesítNév, Összesít_módja, sorNév, oszlopNév, SzűrőNév);
                MyE.Aktív_Cella(munkalap, "A1");
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Kimutatás1()
        {
            try
            {
                string munkalap = "Éves_terv";
                MyE.Munkalap_aktív(munkalap);
                MyE.Link_beillesztés(munkalap, "A1", "Tartalom");

                string munkalap_adat = "Adatok";
                string balfelső = "A3";
                string jobbalsó = "AG" + utolsósor;
                string kimutatás_Munkalap = munkalap;
                string Kimutatás_cella = "A6";
                string Kimutatás_név = "Kimutatás1";

                List<string> összesítNév = new List<string>();
                List<string> Összesít_módja = new List<string>();
                List<string> sorNév = new List<string>();
                List<string> oszlopNév = new List<string>();
                List<string> SzűrőNév = new List<string>();

                összesítNév.Add("Pályaszám");

                Összesít_módja.Add("xlCount");

                sorNév.Add("Év");


                SzűrőNév.Add("Honostelephely");
                SzűrőNév.Add("tervsorszám");

                oszlopNév.Add("Fokozat");

                MyE.Kimutatás_Fő(munkalap_adat, balfelső, jobbalsó, kimutatás_Munkalap, Kimutatás_cella, Kimutatás_név
                                , összesítNév, Összesít_módja, sorNév, oszlopNév, SzűrőNév);
                MyE.Aktív_Cella(munkalap, "A1");
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Kimutatás2()
        {
            try
            {
                string munkalap = "Éves_havi_terv";
                MyE.Munkalap_aktív(munkalap);
                MyE.Link_beillesztés(munkalap, "A1", "Tartalom");

                string munkalap_adat = "Adatok";
                string balfelső = "A3";
                string jobbalsó = "AG" + utolsósor;
                string kimutatás_Munkalap = munkalap;
                string Kimutatás_cella = "A6";
                string Kimutatás_név = "Kimutatás2";

                List<string> összesítNév = new List<string>();
                List<string> Összesít_módja = new List<string>();
                List<string> sorNév = new List<string>();
                List<string> oszlopNév = new List<string>();
                List<string> SzűrőNév = new List<string>();

                összesítNév.Add("ID");

                Összesít_módja.Add("xlCount");

                sorNév.Add("Pályaszám");

                oszlopNév.Add("Hónap");

                SzűrőNév.Add("Honostelephely");
                SzűrőNév.Add("Év");
                SzűrőNév.Add("Fokozat");



                MyE.Kimutatás_Fő(munkalap_adat, balfelső, jobbalsó, kimutatás_Munkalap, Kimutatás_cella, Kimutatás_név
                                , összesítNév, Összesít_módja, sorNév, oszlopNév, SzűrőNév);
                MyE.Aktív_Cella(munkalap, "A1");
            }
            catch (HibásBevittAdat ex)
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


        #region Másik Kimenet
        private void Kimutatás_más_Click(object sender, EventArgs e)
        {
            // kimeneti fájl helye és neve
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog
            {
                InitialDirectory = "MyDocuments",

                Title = "Vizsgálatok tény adatai",
                FileName = $"T5C5_adatbázis_mentés_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddhhmmss}",
                Filter = "Excel |*.xlsx"
            };
            // bekérjük a fájl nevét és helyét ha mégse, akkor kilép

            if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                _fájlexc = SaveFileDialog1.FileName;
            else
                return;

            Holtart.Be();
            timer1.Enabled = true;
            SZál_Kimutatás(() =>
            { //leállítjuk a számlálót és kikapcsoljuk a holtartot.
                timer1.Enabled = false;
                Holtart.Ki();
                MessageBox.Show("A nyomtatvány elkészült !", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
        }

        private void SZál_Kimutatás(Action callback)
        {
            Thread proc = new Thread(() =>
            {
                // elkészítjük a formanyomtatványt változókat nem lehet küldeni definiálni kell egy külső változót.
                SZál_Kimutatás_Eljárás();

                this.Invoke(callback, new object[] { });
            });
            proc.Start();
        }

        private void SZál_Kimutatás_Eljárás()
        {
            try
            {

                MyE.ExcelLétrehozás();
                string munkalap = "Adatok";
                MyE.Munkalap_átnevezés("Munka1", munkalap);
                DataTable dataTable = MyF.ToDataTable(KézKmAdatok.Lista_Adatok().OrderBy(a => a.Azonosító).ToList());
                utolsósor = MyE.Munkalap(dataTable, 1, munkalap) + 1;


                MyE.Betű("D:D", "", "M/d/yyyy");
                MyE.Betű("F:F", "", "M/d/yyyy");
                MyE.Betű("G:G", "", "M/d/yyyy");
                MyE.Betű("K:K", "", "M/d/yyyy");

                // kiírjuk az évet, hónapot és a 2 betűs vizsgálatot
                MyE.Kiir("=YEAR(RC[-15])", "v2");
                MyE.Kiir("=MONTH(RC[-16])", "w2");
                MyE.Kiir("=LEFT(RC[-19],2)", "x2");

                MyE.Képlet_másol(munkalap, "V2:X2", "V3:X" + utolsósor);


                MyE.Kiir("Év", "v1");
                MyE.Kiir("hó", "w1");
                MyE.Kiir("Vizsgálat rövid", "x1");


                MyE.Oszlopszélesség(munkalap, "A:X");

                // rácsozás
                MyE.Rácsoz("A1:X" + utolsósor);

                //szűrést felteszük
                MyE.Szűrés("Adatok", "A", "X", 1);

                //Nyomtatási terület kijelülése
                MyE.NyomtatásiTerület_részletes("Adatok", "A1:X" + utolsósor, "$1:$1", "", true);

                MyE.Új_munkalap("Kimutatás");

                Kimutatás3();

                MyE.Munkalap_aktív("Adatok");
                MyE.Aktív_Cella(munkalap, "A1");

                MyE.ExcelMentés(_fájlexc);
                MyE.ExcelBezárás();

                MyE.Megnyitás(_fájlexc);
                FőHoltart.Ki();

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Kimutatás3()
        {
            try
            {
                string munkalap = "Kimutatás";

                string munkalap_adat = "Adatok";
                string balfelső = "A1";
                string jobbalsó = "X" + utolsósor;
                string kimutatás_Munkalap = munkalap;
                string Kimutatás_cella = "A6";
                string Kimutatás_név = "Kimutatás1";

                List<string> összesítNév = new List<string>();
                List<string> Összesít_módja = new List<string>();
                List<string> sorNév = new List<string>();
                List<string> oszlopNév = new List<string>();
                List<string> SzűrőNév = new List<string>();

                összesítNév.Add("azonosító");
                Összesít_módja.Add("xlCount");

                sorNév.Add("Vizsgálat rövid");

                oszlopNév.Add("V2végezte");

                SzűrőNév.Add("Év");
                SzűrőNév.Add("hó");

                MyE.Kimutatás_Fő(munkalap_adat, balfelső, jobbalsó, kimutatás_Munkalap, Kimutatás_cella, Kimutatás_név
                                , összesítNév, Összesít_módja, sorNév, oszlopNév, SzűrőNév);
                MyE.Aktív_Cella(munkalap, "A1");

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VizsgAdat_Frissít_Click(object sender, EventArgs e)
        {
            Kiirjaatörténelmet();
        }

        private void VizsgAdat_Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla1.Rows.Count <= 0)
                    return;
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    // kimeneti fájl helye és neve
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"{Pályaszám.Text.Trim()}_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Tábla1, false);

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


        #region ListákFeltöltése
        private void KerékadatokListaFeltöltés()
        {
            try
            {
                AdatokMérés.Clear();
                AdatokMérés = KézMérés.Lista_Adatok(DateTime.Today.AddYears(-1).Year);
                List<Adat_Kerék_Mérés> AdatokMérés1 = KézMérés.Lista_Adatok(DateTime.Today.Year);
                AdatokMérés.AddRange(AdatokMérés1);
                AdatokMérés = (from a in AdatokMérés
                               orderby a.Kerékberendezés ascending, a.Mikor descending
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
        }
        #endregion
    }
}