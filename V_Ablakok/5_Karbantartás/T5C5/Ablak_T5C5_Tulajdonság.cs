using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_Ablakok._5_Karbantartás.Karbantartás_Közös;
using Villamos.V_MindenEgyéb;
using DataTable = System.Data.DataTable;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos
{
    public partial class Ablak_T5C5_Tulajdonság
    {
        string _fájlexc;
        DataTable _AdatTábla = new DataTable();
        long utolsósor;
        long JelöltSor = -1;
        long TáblaUtolsóSor = -1;
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
        string munkalap = "";

        readonly Beállítás_Betű BeBetű = new Beállítás_Betű();
        readonly Beállítás_Betű BeBetűF = new Beállítás_Betű { Méret = 12, Név = "Arial", Szín = Color.Black };
        readonly Beállítás_Betű BeBetűD = new Beállítás_Betű { Méret = 12, Név = "Arial", Formátum = "yyyy.MM.dd" };

        #region Alap
        public Ablak_T5C5_Tulajdonság()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            try
            {
                //Ha van 0-tól különböző akkor a régi jogosultságkiosztást használjuk
                //ha mind 0 akkor a GombLathatosagKezelo-t használjuk
                if (Program.PostásJogkör.Substring(0, 1) != "R")
                {
                    Telephelyekfeltöltése();
                    Jogosultságkiosztás();
                }
                else
                {
                    TelephelyekFeltöltéseÚj();
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
                }
                AdatokCiklus = KézCiklus.Lista_Adatok();
                Pályaszám_feltöltés();
                Fülek.SelectedIndex = 0;
                Fülekkitöltése();
                Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Tulajdonság_T5C5_Load(object sender, EventArgs e)
        {
        }

        private void Ablak_T5C5_Tulajdonság_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Karbantartás_Rögzítés?.Close();
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


        private void Jogosultságkiosztás()
        {
            try
            {
                // ide kell az összes gombot tenni amit szabályozni akarunk false
                Rögzítnap.Enabled = false;
                Módosítás.Enabled = false;

                int melyikelem = 106;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Rögzítnap.Enabled = true;
                    Módosítás.Enabled = true;
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
                case 3:
                    {

                        Kiirjaatörténelmet();
                        break;
                    }
                case 4:
                    {

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
                        Kiirjaatörténelmet();
                        break;
                    }
                case 4:
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
                DateTime Kezdet = DateTime.Now;
                MyX.DataGridViewToXML(fájlexc, Tábla_lekérdezés);
                MessageBox.Show($"Elkészült az Excel tábla: {fájlexc}\n idő alatt:{DateTime.Now - Kezdet}", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private async void Teljes_adatbázis_excel_Click(object sender, EventArgs e)
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
            // JAVÍTANDÓ: Itt lehet próbálkozni Excel kimenet gyorsításával
            List<Adat_T5C5_Kmadatok> Adatok = KézKmAdatok.Lista_Adatok();
            _AdatTábla = MyF.ToDataTable(Adatok);
            Holtart.Be();
            timer1.Enabled = true;
            DateTime Kezdet = DateTime.Now;
            await Task.Run(() => MyX.DataTableToXML(_fájlexc, _AdatTábla));

            timer1.Enabled = false;
            Holtart.Ki();
            MessageBox.Show($"Az Excel tábla elkészült !\n Futási idő{DateTime.Now - Kezdet}", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MyF.Megnyitás(_fájlexc);

        }


        private void Tábla_lekérdezés_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
            }
            catch (HibásBevittAdat ex)
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
                    TáblaUtolsóSor = rekord.ID;
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
                Tábla1.ClearSelection();
            }
            catch (HibásBevittAdat ex)
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

            if (e.RowIndex < 0) return;

            if (!long.TryParse(Tábla1.Rows[e.RowIndex].Cells[0].Value.ToString(), out JelöltSor)) JelöltSor = -1;
        }

        Karbantartás_Rögzítés Új_Karbantartás_Rögzítés;
        private void RögzítésAblak()
        {
            Adat_T5C5_Kmadatok adat = (from a in AdatokKmAdatok
                                       where a.ID == JelöltSor
                                       select a).FirstOrDefault();
            if (adat == null) return;
            bool Utolsó = JelöltSor == TáblaUtolsóSor;

            Új_Karbantartás_Rögzítés?.Close();

            Új_Karbantartás_Rögzítés = new Karbantartás_Rögzítés("T5C5", adat, Utolsó);
            Új_Karbantartás_Rögzítés.FormClosed += Karbantartás_Rögzítés_FormClosed;
            Új_Karbantartás_Rögzítés.Változás += Kiirjaatörténelmet;
            Új_Karbantartás_Rögzítés.Show();

        }

        private void Karbantartás_Rögzítés_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Karbantartás_Rögzítés = null;
        }

        private void Módosítás_Click(object sender, EventArgs e)
        {
            try
            {
                if (JelöltSor == -1) return;
                if (TáblaUtolsóSor == -1) return;
                RögzítésAblak();
                JelöltSor = -1;

            }
            catch (HibásBevittAdat ex)
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


        #region Állomány tábla
        private void Excel_gomb_Click(object sender, EventArgs e)
        {
            try
            {
                List<Adat_Jármű> Adatok = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
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

                var Elemek = Adatok
                    .OrderBy(j => j.Valóstípus)        // Elsődleges rendezés: Valóstípus szerint
                    .ThenBy(j => j.Azonosító)          // Másodlagos rendezés: Azonosító szerint
                    .Select(j => new
                    {
                        j.Azonosító,
                        j.Valóstípus
                    })
                    .ToList();
                DataTable TáblaAdat = MyF.ToDataTable(Elemek);

                MyX.DataTableToXML(fájlexc, TáblaAdat);

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
                if (OpenFileDialogPI.ShowDialogEllenőr(OpenFileDialog1) == DialogResult.OK)
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
            Holtart.Be(PszJelölő.Items.Count + 1);

            for (int j = 0; j < PszJelölő.Items.Count; j++)
            {
                Holtart.Lép();
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
            Holtart.Ki();
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
            Holtart.Be(PszJelölő.Items.Count + 1);

            for (int j = 0; j < PszJelölő.Items.Count; j++)
            {
                Holtart.Lép();
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
            Holtart.Ki();
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
            Holtart.Be(PszJelölő.Items.Count + 1);

            for (int j = 0; j < PszJelölő.Items.Count; j++)
            {
                Holtart.Lép();
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
            Holtart.Ki();
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

            if (!int.TryParse(Text1.Text, result: out int result))
                Text1.Text = "";
            Option8.Checked = true;
        }

        private void Command1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Text2.Text, out int result)) throw new HibásBevittAdat("Hónapok száma nem lehet üres és egész számnak kell lennie.");
                if (PszJelölő.CheckedItems.Count < 1) return;

                Holtart.Be();
                Holtart.Be(10);

                Alaptábla();
                Holtart.Lép();
                Egyhónaprögzítése();
                Excel_előtervező();
                Holtart.Ki();
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
                Holtart.Be(PszJelölő.Items.Count + 1);
                Holtart.BackColor = Color.Yellow;
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
                    }
                    Holtart.Lép();
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

                Holtart.Be(PszJelölő.CheckedItems.Count + 2);
                Holtart.Be(Hónapok + 3);
                Holtart.BackColor = Color.Green;

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
                            Holtart.Lép();
                        }
                    }
                    Holtart.Lép();
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

                cím[0] = "Tartalom";
                Leírás[0] = "Tartalom jegyzék";
                cím[1] = "Vizsgálatok";
                Leírás[1] = "Vizsgálati adatok havonta";
                cím[2] = "Éves_terv";
                Leírás[2] = "Vizsgálati adatok éves";
                cím[3] = "Éves_havi_terv";
                Leírás[3] = "Vizsgálati adatok éves/havi";
                cím[4] = "Adatok";
                Leírás[4] = "Előtervezett adatok";

                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    // kimeneti fájl helye és neve
                    InitialDirectory = "MyDocuments",

                    Title = "Vizsgálat előtervező",
                    FileName = $"V_javítások_előtervezése_{DateTime.Now:yyyyMMddhhmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                // ****************************************************
                // elkészítjük a lapokat
                // ****************************************************
                string munkalap;
                MyX.ExcelLétrehozás(cím[0].Trim());
                for (int i = 1; i < cím.Length; i++)
                    MyX.Munkalap_Új(cím[i].Trim());

                Holtart.Be(5);

                // megnyitjuk az adatbázist   
                //Részletes adatokat a Adatok lapon készítjük el 
                string hely = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Kmadatok.mdb";
                DataTable dataTable = MyF.ToDataTable(KézElőterv.Lista_Adatok(hely));
                utolsósor = dataTable.Rows.Count;

                munkalap = "Adatok";
                MyX.Munkalap_betű(munkalap, BeBetű);
                MyX.Munkalap_Adattábla(munkalap, dataTable);

                munkalap = "Tartalom";
                // ****************************************************
                // Elkészítjük a tartalom jegyzéket
                // ****************************************************
                MyX.Munkalap_aktív(munkalap);

                MyX.Kiir("Munkalapfül", "a1");
                MyX.Kiir("Leírás", "b1");
                for (int i = 1; i <= 4; i++)
                {
                    MyX.Kiir(cím[i], $"A{i + 1}");
                    MyX.Kiir(Leírás[i], $"B{i + 1}");
                    MyX.Link_beillesztés(munkalap, $"B{i + 1}", cím[i].Trim());

                }
                MyX.Oszlopszélesség(munkalap, "A:B");

                // ****************************************************
                // Elkészítjük a munkalapokat
                // ****************************************************
                Holtart.Lép();
                AdatokFormázása(utolsósor);
                Holtart.Lép();
                Kimutatás();
                Holtart.Lép();
                Kimutatás1();
                Holtart.Lép();
                Kimutatás2();
                munkalap = "Tartalom";
                MyX.Munkalap_aktív(munkalap);

                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();
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

        private void AdatokFormázása(long utolsósor)
        {
            try
            {
                munkalap = "Adatok";
                MyX.Munkalap_aktív(munkalap);

                MyX.SorBeszúrás(munkalap, 1, 2);  //beszúrunk két sort előre
                MyX.Háttérszín(munkalap, $"A3:AG3", Color.White); //Visszaállítjuk a háttér színt
                MyX.Betű(munkalap, $"A3:AG3", BeBetűF);

                MyX.Link_beillesztés(munkalap, "A1", "Tartalom");

                //// fejlécet kiírjuk
                MyX.Kiir("ID", "a3");
                MyX.Kiir("Pályaszám", "b3");
                MyX.Kiir("Jjavszám", "c3");
                MyX.Kiir("KMUkm", "d3");
                MyX.Kiir("KMUdátum", "e3");
                MyX.Kiir("vizsgfok", "f3");
                MyX.Kiir("vizsgdátumkezdő", "g3");
                MyX.Kiir("vizsgdátumvég", "h3");
                MyX.Kiir("vizsgkmszámláló", "i3");
                MyX.Kiir("havikm", "j3");
                MyX.Kiir("vizsgsorszám", "k3");
                MyX.Kiir("Jdátum", "l3");
                MyX.Kiir("Teljeskm", "m3");
                MyX.Kiir("Ciklusrend", "n3");
                MyX.Kiir("V2végezte", "o3");
                MyX.Kiir("Köv V2 sorszám", "p3");
                MyX.Kiir("Köv V2", "q3");
                MyX.Kiir("Köv V sorszám", "r3");
                MyX.Kiir("köv V", "s3");
                MyX.Kiir("Törölt", "t3");
                MyX.Kiir("Honostelephely", "u3");
                MyX.Kiir("tervsorszám", "v3");
                MyX.Kiir("Kerék_11", "w3");
                MyX.Kiir("Kerék_12", "x3");
                MyX.Kiir("Kerék_21", "y3");
                MyX.Kiir("Kerék_22", "z3");
                MyX.Kiir("Kerék_min", "aa3");
                MyX.Kiir("V2V3 számláló", "ab3");
                MyX.Kiir("Év", "ac3");
                MyX.Kiir("fokozat", "ad3");
                MyX.Kiir("Hónap", "ae3");

                MyX.Kiir("#KÉPLET#=YEAR(RC[-22])", "AC4");
                MyX.Kiir("#KÉPLET#=LEFT(RC[-24],2)", "AD4");
                MyX.Kiir("#KÉPLET#=MONTH(RC[-24])", "AE4");

                MyX.Képlet_másol(munkalap, "AC4:AE4", "AC5:AE" + (utolsósor + 3));

                //// megformázzuk
                MyX.Oszlopszélesség(munkalap, "A:AE");

                MyX.Rácsoz(munkalap, "a3:AE3");
                MyX.Rácsoz(munkalap, "a4:AE" + (utolsósor + 3));
                // szűrő
                MyX.Szűrés(munkalap, "A", "AE", (int)(utolsósor + 3), 3);

                // ablaktábla rögzítése
                MyX.Tábla_Rögzítés(munkalap, 3);


                // kiírjuk a tábla méretét
                MyX.Munkalap_aktív("Vizsgálatok");
                MyX.Kiir((utolsósor + 2).ToString(), "aa1");
                MyX.Munkalap_aktív("Éves_terv");
                MyX.Kiir((utolsósor + 2).ToString(), "aa1");
                MyX.Munkalap_aktív("Éves_havi_terv");
                MyX.Kiir((utolsósor + 2).ToString(), "aa1");
            }
            catch (HibásBevittAdat ex)
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
                MyX.Munkalap_aktív(munkalap);
                MyX.Link_beillesztés(munkalap, "A1", "Tartalom");

                string munkalap_adat = "Adatok";
                string balfelső = "A3";
                string jobbalsó = "AE" + utolsósor;
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

                Beállítás_Kimutatás Bekimutat = new Beállítás_Kimutatás
                {
                    Munkalapnév = munkalap_adat,
                    Balfelső = balfelső,
                    Jobbalsó = jobbalsó,
                    Kimutatás_Munkalapnév = kimutatás_Munkalap,
                    Kimutatás_cella = Kimutatás_cella,
                    Kimutatás_név = Kimutatás_név,
                    ÖsszesítNév = összesítNév,
                    Összesítés_módja = Összesít_módja,
                    SorNév = sorNév,
                    OszlopNév = oszlopNév,
                    SzűrőNév = SzűrőNév
                };
                MyX.Kimutatás_Fő(Bekimutat);
            }
            catch (HibásBevittAdat ex)
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
                MyX.Munkalap_aktív(munkalap);
                MyX.Link_beillesztés(munkalap, "A1", "Tartalom");

                string munkalap_adat = "Adatok";
                string balfelső = "A3";
                string jobbalsó = "AE" + utolsósor;
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

                Beállítás_Kimutatás Bekimutat = new Beállítás_Kimutatás
                {
                    Munkalapnév = munkalap_adat,
                    Balfelső = balfelső,
                    Jobbalsó = jobbalsó,
                    Kimutatás_Munkalapnév = kimutatás_Munkalap,
                    Kimutatás_cella = Kimutatás_cella,
                    Kimutatás_név = Kimutatás_név,
                    ÖsszesítNév = összesítNév,
                    Összesítés_módja = Összesít_módja,
                    SorNév = sorNév,
                    OszlopNév = oszlopNév,
                    SzűrőNév = SzűrőNév
                };
                MyX.Kimutatás_Fő(Bekimutat);
            }
            catch (HibásBevittAdat ex)
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
                MyX.Munkalap_aktív(munkalap);
                MyX.Link_beillesztés(munkalap, "A1", "Tartalom");

                string munkalap_adat = "Adatok";
                string balfelső = "A3";
                string jobbalsó = "AE" + utolsósor;
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

                Beállítás_Kimutatás Bekimutat = new Beállítás_Kimutatás
                {
                    Munkalapnév = munkalap_adat,
                    Balfelső = balfelső,
                    Jobbalsó = jobbalsó,
                    Kimutatás_Munkalapnév = kimutatás_Munkalap,
                    Kimutatás_cella = Kimutatás_cella,
                    Kimutatás_név = Kimutatás_név,
                    ÖsszesítNév = összesítNév,
                    Összesítés_módja = Összesít_módja,
                    SorNév = sorNév,
                    OszlopNév = oszlopNév,
                    SzűrőNév = SzűrőNév
                };
                MyX.Kimutatás_Fő(Bekimutat);
            }
            catch (HibásBevittAdat ex)
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
        private async void Kimutatás_más_Click(object sender, EventArgs e)
        {
            // kimeneti fájl helye és neve
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog
            {
                InitialDirectory = "MyDocuments",

                Title = "Vizsgálatok tény adatai",
                FileName = $"T5C5_AB_{Program.PostásNév.Trim()}_{DateTime.Now:yyyyMMddhhmmss}",
                Filter = "Excel |*.xlsx"
            };
            // bekérjük a fájl nevét és helyét ha mégse, akkor kilép

            if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                _fájlexc = SaveFileDialog1.FileName;
            else
                return;

            Holtart.Be();
            timer1.Enabled = true;

            await Task.Run(() => SZál_Kimutatás_Eljárás());
            //leállítjuk a számlálót és kikapcsoljuk a holtartot.
            timer1.Enabled = false;
            Holtart.Ki();
            MessageBox.Show("A nyomtatvány elkészült !", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SZál_Kimutatás_Eljárás()
        {
            try
            {
                string munkalap = "Adatok";
                MyX.ExcelLétrehozás(munkalap);
                MyX.Munkalap_betű(munkalap, BeBetű);

                DataTable dataTable = MyF.ToDataTable(KézKmAdatok.Lista_Adatok().OrderBy(a => a.Azonosító).ToList());
                utolsósor = MyX.Munkalap(dataTable, 1, munkalap) + 1;


                MyX.Betű(munkalap, $"E1:E{utolsósor}", BeBetűD);
                MyX.Betű(munkalap, $"G1:G{utolsósor}", BeBetűD);
                MyX.Betű(munkalap, $"H1:H{utolsósor}", BeBetűD);
                MyX.Betű(munkalap, $"L1:L{utolsósor}", BeBetűD);

                // kiírjuk az évet, hónapot és a 2 betűs vizsgálatot
                MyX.Kiir("#KÉPLET#=YEAR(RC[-15])", "v2");
                MyX.Kiir("#KÉPLET#=MONTH(RC[-16])", "w2");
                MyX.Kiir("#KÉPLET#=LEFT(RC[-18],2)", "x2");

                MyX.Képlet_másol(munkalap, "V2:X2", "V3:X" + utolsósor);


                MyX.Kiir("Év", "v1");
                MyX.Kiir("hó", "w1");
                MyX.Kiir("Vizsgálat rövid", "x1");


                MyX.Oszlopszélesség(munkalap, "A:X");

                // rácsozás
                MyX.Rácsoz(munkalap, "A1:X" + utolsósor);
                MyX.Háttérszín(munkalap, "A1:X1", Color.Yellow);

                //szűrést felteszük
                MyX.Szűrés("Adatok", "A", "X", 1);

                //Nyomtatási terület kijelülése
                Beállítás_Nyomtatás BeNyom = new Beállítás_Nyomtatás
                {
                    NyomtatásiTerület = "A1:X" + utolsósor,
                    IsmétlődőSorok = "$1:$1",
                    IsmétlődőOszlopok = "",
                    Álló = true,

                    LapSzéles = 1,
                    LapMagas = 1,
                    Papírméret = "A4",
                    BalMargó = 15,
                    JobbMargó = 15,
                    FelsőMargó = 20,
                    AlsóMargó = 20,
                    FejlécMéret = 13,
                    LáblécMéret = 13,

                    // Fejléc/Lábléc (ha a Program.PostásNév nem elérhető itt, írd át fix szövegre)
                    FejlécKözép = Program.PostásNév.Trim(),
                    FejlécJobb = DateTime.Now.ToString("yyyy.MM.dd HH:mm"),
                    LáblécKözép = "&P/&N",

                    FüggKözép = false,
                    VízKözép = false
                };

                // Meghívjuk a függvényt az objektummal
                MyX.NyomtatásiTerület_részletes("Adatok", BeNyom);

                Kimutatás3();

                MyX.Munkalap_aktív("Adatok");

                MyX.ExcelMentés(_fájlexc);
                MyX.ExcelBezárás();

                MyF.Megnyitás(_fájlexc);
            }
            catch (HibásBevittAdat ex)
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

                Beállítás_Kimutatás BeKimutat = new Beállítás_Kimutatás
                {
                    Munkalapnév = munkalap_adat,
                    Balfelső = balfelső,
                    Jobbalsó = jobbalsó,
                    Kimutatás_Munkalapnév = kimutatás_Munkalap,
                    Kimutatás_cella = Kimutatás_cella,
                    Kimutatás_név = Kimutatás_név,
                    ÖsszesítNév = összesítNév,
                    Összesítés_módja = Összesít_módja,
                    SorNév = sorNév,
                    OszlopNév = oszlopNév,
                    SzűrőNév = SzűrőNév
                };

                MyX.Kimutatás_Fő(BeKimutat);

            }
            catch (HibásBevittAdat ex)
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

                MyX.DataGridViewToXML(fájlexc, Tábla1);

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

        private void Cmbtelephely_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                Cmbtelephely.Text = Cmbtelephely.Items[Cmbtelephely.SelectedIndex].ToStrTrim();
                if (Cmbtelephely.Text.Trim() == "") return;
                if (Program.PostásJogkör.Any(c => c != '0'))
                {

                }
                else
                {
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
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
    }
}