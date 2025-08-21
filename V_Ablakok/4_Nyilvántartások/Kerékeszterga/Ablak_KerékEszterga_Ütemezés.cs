using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Ablakok.Kerékeszterga;
using Villamos.Villamos_Adatszerkezet;
using MyColor = Villamos.V_MindenEgyéb.Kezelő_Szín;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;
using MyO = Microsoft.Office.Interop.Outlook;


namespace Villamos.Villamos_Ablakok
{
    public partial class Ablak_KerékEszterga_Ütemezés : Form
    {
        DateTime DátumÉsIdő = DateTime.Today;
        List<Adat_Jármű> Honos = null;
        readonly Kezelő_Dolgozó_Beosztás_Új Kezelő_Beoszt_Új = new Kezelő_Dolgozó_Beosztás_Új();
        readonly Kezelő_Kerék_Eszterga_Naptár KézNaptár = new Kezelő_Kerék_Eszterga_Naptár();
        readonly Kezelő_Kerék_Eszterga_Igény KézIgény = new Kezelő_Kerék_Eszterga_Igény();
        readonly Kezelő_Kerék_Eszterga_Terjesztés KézTerjeszt = new Kezelő_Kerék_Eszterga_Terjesztés();
        readonly Kezelő_Kerék_Eszterga_Automata KézAuto = new Kezelő_Kerék_Eszterga_Automata();
        readonly Kezelő_kiegészítő_telephely KézTelep = new Kezelő_kiegészítő_telephely();
        readonly Kezelő_Kiegészítő_Beosztáskódok KézB = new Kezelő_Kiegészítő_Beosztáskódok();
        readonly Kezelő_Kerék_Eszterga_Esztergályos KézEsztergályos = new Kezelő_Kerék_Eszterga_Esztergályos();

        List<Adat_Dolgozó_Beosztás_Új> Adatok_Beoszt_Új = new List<Adat_Dolgozó_Beosztás_Új>();
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();

        #region Alap
        public Ablak_KerékEszterga_Ütemezés()
        {
            InitializeComponent();
            Start();
        }

        private void Ablak_KerékEszterga_Ütemezés_Load(object sender, EventArgs e)
        { }

        private void Ablak_KerékEszterga_Ütemezés_ControlAdded(object sender, ControlEventArgs e)
        { }

        private void Ablak_KerékEszterga_Ütemezés_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Eszterga_Dolgozók?.Close();
            Új_Ablak_Eszterga_Választék?.Close();
            Új_Ablak_Eszterga_Segéd?.Close();
            Új_Ablak_Eszterga_Terjesztés?.Close();
            Új_Ablak_Eszterga_Beosztás?.Close();
        }

        private void Start()
        {
            try
            {
                //Ha van 0-tól különböző akkor a régi jogosultságkiosztást használjuk
                //ha mind 0 akkor a GombLathatosagKezelo-t használjuk
                if (Program.PostásJogkör.Any(c => c != '0'))
                {
                    Telephelyekfeltöltése();
                    Jogosultságkiosztás();
                }
                else
                {
                    TelephelyekFeltöltéseÚj();
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
                }

                Dátum.Value = DateTime.Today;
                Fülekkitöltése();
                Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;
                Telephelyek_Szűrő_feltöltése();
                Automata_Jelentés();

            }
            catch (HibásBevittAdat ex)
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
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Baross_Eszterga.html";
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
                    Cmbtelephely.Text = Cmbtelephely.Items[0].ToStrTrim();
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

        private void TelephelyekFeltöltéseÚj()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Adat in GombLathatosagKezelo.Telephelyek(this.Name))
                    Cmbtelephely.Items.Add(Adat.Trim());
                //Alapkönyvtárat beállítjuk 
                Cmbtelephely.Text = Program.PostásTelephely;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Telephelyek_Szűrő_feltöltése()
        {
            try
            {
                List<Adat_kiegészítő_telephely> Adatok = KézTelep.Lista_Adatok().OrderBy(a => a.Telephelynév).ToList();
                Telephely.Items.Clear();
                Telephely.Items.Add("");
                foreach (Adat_kiegészítő_telephely Elem in Adatok)
                    Telephely.Items.Add(Elem.Telephelynév);

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

        private void Jogosultságkiosztás()
        {
            int melyikelem;

            // ide kell az összes gombot tenni amit szabályozni akarunk false
            Elkészült.Enabled = false;
            Visszaállítás.Enabled = false;
            Törölt.Enabled = false;
            BeosztásAdatok.Enabled = false;
            Esztergályosok.Enabled = false;
            Választék_Lista.Enabled = false;
            Terjesztési.Enabled = false;
            Heti_terv_küldés.Enabled = false;
            Heti_jelentés.Enabled = false;


            if (Program.PostásTelephely.Trim() == "Főmérnökség")
            {
                Elkészült.Visible = true;
                Visszaállítás.Visible = true;
                Terjesztési.Visible = true;

                BeosztásAdatok.Visible = true;
                Esztergályosok.Visible = true;
                Választék_Lista.Visible = true;
                Heti_terv_küldés.Visible = true;
                Heti_jelentés.Visible = true;
            }
            else
            {
                Elkészült.Visible = false;
                Visszaállítás.Visible = false;
                Terjesztési.Visible = false;

                BeosztásAdatok.Visible = false;
                Esztergályosok.Visible = false;
                Választék_Lista.Visible = false;
                Heti_terv_küldés.Visible = false;
                Heti_jelentés.Visible = false;
            }

            if (Program.PostásTelephely.Trim() == "Baross")
            {
                Elkészült.Visible = true;
                Visszaállítás.Visible = true;
                BeosztásAdatok.Visible = true;
                Esztergályosok.Visible = true;
                Terjesztési.Visible = true;
                Heti_terv_küldés.Visible = true;
                Heti_jelentés.Visible = true;
            }

            melyikelem = 165;
            // módosítás 1 
            if (MyF.Vanjoga(melyikelem, 1))
            {
                Elkészült.Enabled = true;
                Visszaállítás.Enabled = true;
            }
            // módosítás 2
            if (MyF.Vanjoga(melyikelem, 2))
            {
                Törölt.Enabled = true;
            }
            // módosítás 3 
            if (MyF.Vanjoga(melyikelem, 3))
            {
                BeosztásAdatok.Enabled = true;
                Esztergályosok.Enabled = true;
            }


            melyikelem = 166;
            // módosítás 1 
            if (MyF.Vanjoga(melyikelem, 1))
            {
                Választék_Lista.Enabled = true;
            }
            // módosítás 2
            if (MyF.Vanjoga(melyikelem, 2))
            {
                Terjesztési.Enabled = true;
                Heti_terv_küldés.Enabled = true;
                Heti_jelentés.Enabled = true;
            }
            // módosítás 3 
            if (MyF.Vanjoga(melyikelem, 3))
            {

            }
        }

        private void Fülekkitöltése()
        {

            switch (Fülek.SelectedIndex)
            {
                case 1:
                    {
                        Igény_Típus_Feltöltés();
                        Státus_Feltöltés();
                        break;
                    }
                case 0:
                    {

                        break;
                    }
                case 2:
                    {
                        // beállítások

                        break;
                    }

            }
        }

        private void Státus_Feltöltés()
        {
            Igény_Státus.Items.Clear();
            Igény_Státus.Items.Add("");
            Igény_Státus.Items.Add("0 - Igény");
            Igény_Státus.Items.Add("2 - Ütemezett");
            Igény_Státus.Items.Add("7 - Elkészült");
            Igény_Státus.Items.Add("9 - Törölt");
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
            StringFormat sf = new StringFormat()
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

        private void Fülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }
        #endregion


        #region Igény
        private void Igény_Típus_Feltöltés()
        {
            try
            {
                Igény_Típus.Items.Clear();
                Igény_Típus.Items.Add("");
                //Aktuális év és előző év adatait betesszük egy listába, melyet sorbarendezzünk típus 
                //szerint majd minden típusból kiveszünk egy elemet
                List<Adat_Kerék_Eszterga_Igény> Adatok = KézIgény.Lista_Adatok(DateTime.Today.Year);
                List<Adat_Kerék_Eszterga_Igény> AdatokE = KézIgény.Lista_Adatok(DateTime.Today.Year - 1);
                Adatok.AddRange(AdatokE);
                Adatok = Adatok.OrderBy(a => a.Típus).ToList();
                List<string> Típusok = Adatok.Select(a => a.Típus.Trim()).Distinct().ToList();
                //betesszük az igény comboboxba 
                foreach (string item in Típusok)
                    Igény_Típus.Items.Add(item);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Lista_Tábla_Click(object sender, EventArgs e)
        {
            Lista_Tábla_kiírás();
        }

        private void Lista_Tábla_kiírás()
        {
            try
            {
                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 9;

                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Prioritás";
                Tábla.Columns[0].Width = 80;
                Tábla.Columns[1].HeaderText = "Igénylés ideje";
                Tábla.Columns[1].Width = 180;
                Tábla.Columns[2].HeaderText = "Pályaszám";
                Tábla.Columns[2].Width = 150;
                Tábla.Columns[3].HeaderText = "Telephely";
                Tábla.Columns[3].Width = 100;
                Tábla.Columns[4].HeaderText = "Típus";
                Tábla.Columns[4].Width = 100;
                Tábla.Columns[5].HeaderText = "Státus";
                Tábla.Columns[5].Width = 100;
                Tábla.Columns[6].HeaderText = "Megjegyzés";
                Tábla.Columns[6].Width = 400;
                Tábla.Columns[7].HeaderText = "Ütemezés dátuma";
                Tábla.Columns[7].Width = 120;
                Tábla.Columns[8].HeaderText = "Norma idő";
                Tábla.Columns[8].Width = 120;

                for (int ii = -1; ii < 1; ii++)
                {

                    List<Adat_Kerék_Eszterga_Igény> Adatok = KézIgény.Lista_Adatok(DateTime.Today.AddYears(ii).Year);


                    if (Igény_Státus.Text.Trim() != "")
                    {
                        string[] darabol = Igény_Státus.Text.Trim().Split('-');
                        int státus = 0;
                        switch (darabol[0].Trim())
                        {
                            case "0":
                                státus = 0;
                                break;
                            case "2":
                                státus = 2;
                                break;
                            case "7":
                                státus = 7;
                                break;
                            case "9":
                                státus = 9;
                                break;
                        }
                        Adatok = (from a in Adatok
                                  where a.Státus == státus
                                  orderby a.Prioritás descending, a.Rögzítés_dátum
                                  select a).ToList();
                    }
                    else
                        Adatok = (from a in Adatok
                                  where a.Státus < 7
                                  orderby a.Prioritás descending, a.Rögzítés_dátum
                                  select a).ToList();


                    if (Telephely.Text.Trim() != "")
                        Adatok = (from a in Adatok
                                  where a.Telephely == Telephely.Text.Trim()
                                  orderby a.Prioritás descending, a.Rögzítés_dátum
                                  select a).ToList();



                    if (Igény_Típus.Text.Trim() != "")
                        Adatok = (from a in Adatok
                                  where a.Típus == Igény_Típus.Text.Trim()
                                  orderby a.Prioritás descending, a.Rögzítés_dátum
                                  select a).ToList();


                    foreach (Adat_Kerék_Eszterga_Igény rekord in Adatok)
                    {
                        Tábla.RowCount++;
                        int i = Tábla.RowCount - 1;
                        Tábla.Rows[i].Cells[0].Value = rekord.Prioritás;
                        Tábla.Rows[i].Cells[1].Value = rekord.Rögzítés_dátum.ToString();
                        Tábla.Rows[i].Cells[2].Value = rekord.Pályaszám.Trim();
                        Tábla.Rows[i].Cells[3].Value = rekord.Telephely.Trim();
                        Tábla.Rows[i].Cells[4].Value = rekord.Típus.Trim();
                        switch (rekord.Státus)
                        {
                            case 0:
                                Tábla.Rows[i].Cells[5].Value = "Igény";
                                break;
                            case 2:
                                Tábla.Rows[i].Cells[5].Value = "Ütemezett";
                                break;
                            case 7:
                                Tábla.Rows[i].Cells[5].Value = "Elkészült";
                                break;
                            case 9:
                                Tábla.Rows[i].Cells[5].Value = "Törölt";
                                break;
                        }
                        Tábla.Rows[i].Cells[6].Value = rekord.Megjegyzés.Trim();
                        Tábla.Rows[i].Cells[7].Value = rekord.Ütemezés_dátum.ToString("yyyy.MM.dd") == "1900.01.01" ? "" : rekord.Ütemezés_dátum.ToString("yyyy.MM.dd");
                        Tábla.Rows[i].Cells[8].Value = rekord.Norma;
                    }

                }
                Tábla.Refresh();
                Tábla.Visible = true;
            }
            catch (HibásBevittAdat ex)
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


        #region Munkaidő 
        private void Beosztás_Adatok()
        {
            try
            {
                HétAlapAdatai();
                Beosztások_Esztergához();
                Munkaidő_Töröl();
                Munkaidő_Átír_vez();
                Terv_lista_elj();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HétAlapAdatai()
        {
            try
            {
                Holtart.Be(20);

                DateTime Hételső = MyF.Hét_elsőnapja(Dátum.Value);
                DateTime Hétutolsó = MyF.Nap2359(MyF.Hét_Utolsónapja(Dátum.Value));

                List<Adat_Kerék_Eszterga_Naptár> Adatok = KézNaptár.Lista_Adatok(Hételső.Year);
                if (Hételső.Year != Hétutolsó.Year)
                {
                    List<Adat_Kerék_Eszterga_Naptár> AdatokK = KézNaptár.Lista_Adatok(Hétutolsó.Year);
                    Adatok.AddRange(AdatokK);
                    Adatok = Adatok.OrderBy(a => a.Idő).ToList();
                }

                Adatok = (from a in Adatok
                          where a.Idő >= Hételső && a.Idő <= Hétutolsó
                          orderby a.Idő
                          select a).ToList();
                if (Adatok.Count > 0) return;

                DateTime FutóIdő = new DateTime(Hételső.Year, Hételső.Month, Hételső.Day, 0, 0, 0);
                DateTime VégeIdő = FutóIdő.AddDays(7);

                List<Adat_Kerék_Eszterga_Naptár> AdatokGy = new List<Adat_Kerék_Eszterga_Naptár>();
                while (FutóIdő < VégeIdő)
                {
                    Adat_Kerék_Eszterga_Naptár ADAT = new Adat_Kerék_Eszterga_Naptár(FutóIdő);
                    AdatokGy.Add(ADAT);
                    FutóIdő = FutóIdő.AddMinutes(30);
                    Holtart.Lép();
                }
                if (AdatokGy.Count > 0) KézNaptár.Rögzítés(Hételső.Year, AdatokGy);

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

        private void BeosztásAdatok_Click(object sender, EventArgs e)
        {
            Beosztás_Adatok();
        }

        /// <summary>
        /// Beosztásból kiszűri azon dolgozókat akik esztergálnak és csak az Ő adatai jelennek meg
        /// Így 2-3 fő adatai külön kezelhetők
        /// </summary>
        private void Beosztások_Esztergához()
        {
            try
            {
                //Betöltjük a dolgozókat akik beosztását nézzük
                List<Adat_Kerék_Eszterga_Esztergályos> AdatokE = KézEsztergályos.Lista_Adatok();

                DateTime Hételső = MyF.Hét_elsőnapja(Dátum.Value);
                DateTime Hétutolsó = MyF.Hét_Utolsónapja(Dátum.Value);

                Holtart.Be(20);

                foreach (Adat_Kerék_Eszterga_Esztergályos rekord in AdatokE)
                {
                    Eszt_Beosztás_Törlés(rekord.Telephely.Trim(), Hételső, Hétutolsó);
                    Holtart.Lép();
                }


                // Új beosztás táblába áttöltjük, majd törölni kell az új beosztás esetén
                foreach (Adat_Kerék_Eszterga_Esztergályos rekord in AdatokE)
                {
                    //Eredeti csoport azon részé akik főállásból végzik
                    if (rekord.Státus == 1)
                        Eszt_Új_Beosztás(rekord.Telephely.Trim(), rekord.Dolgozószám.Trim(), Hételső, Hétutolsó);

                    //Telephelyen lévő, de besegítő
                    if (rekord.Státus == 2)

                        Eszt_Új_Beosztás_besegítő(rekord.Telephely.Trim(), rekord.Dolgozószám.Trim(), Hételső, Hétutolsó);

                    Holtart.Lép();
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

        private void Munkaidő_Töröl()
        {
            try
            {
                Holtart.Be(20);

                DateTime Hételső = MyF.Hét_elsőnapja(Dátum.Value);
                DateTime Hétutolsó = MyF.Hét_Utolsónapja(Dátum.Value);
                List<Adat_Kerék_Eszterga_Naptár> Adatok = KézNaptár.Lista_Adatok(Dátum.Value.Year);

                bool vane = Adatok.Any(n =>
                n.Idő >= Hételső.Date &&
                n.Idő <= Hétutolsó.Date.AddDays(1).AddTicks(-1) &&
                n.Munkaidő == true);

                if (vane)
                {
                    Adat_Kerék_Eszterga_Naptár ADAT = new Adat_Kerék_Eszterga_Naptár(false, Hételső, Hétutolsó);
                    KézNaptár.Módosítás_Munkaidő(Hételső.Year, ADAT);
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

        private void Munkaidő_Átír_vez()
        {
            try
            {
                DateTime Hételső = MyF.Hét_elsőnapja(Dátum.Value);
                DateTime Hétutolsó = MyF.Hét_Utolsónapja(Dátum.Value);
                if (Hételső.Month != Hétutolsó.Month) Munkaidő_Átír(Hétutolsó);

                Munkaidő_Átír(Hételső);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Munkaidő_Átír(DateTime NapTÁR)
        {
            try
            {
                Holtart.Be(20);
                //Beosztás adatok betöltése amelyek valós munkaidőt tartalmaznak
                List<Adat_Kiegészítő_Beosztáskódok> AdatB = KézB.Lista_Adatok(Cmbtelephely.Text.Trim());
                AdatB = (from a in AdatB
                         where a.Számoló == true
                         orderby a.Beosztáskód
                         select a).ToList();

                DateTime Hételső = MyF.Hét_elsőnapja(NapTÁR);
                DateTime Hétutolsó = MyF.Hét_Utolsónapja(NapTÁR);

                foreach (Adat_Kiegészítő_Beosztáskódok rekordBKód in AdatB)
                {
                    List<Adat_Dolgozó_Beosztás_Új> AdatBEO = Kezelő_Beoszt_Új.Lista_Adatok(Cmbtelephely.Text.Trim(), NapTÁR, true);
                    AdatBEO = (from a in AdatBEO
                               where a.Nap >= MyF.Nap0000(Hételső)
                               && a.Nap <= MyF.Nap2359(Hétutolsó)
                               && (a.Beosztáskód == rekordBKód.Beosztáskód.Trim() || a.Beosztáskód == "#")
                               orderby a.Nap
                               select a).ToList();

                    List<Adat_Kerék_Eszterga_Naptár> AdatokGy = new List<Adat_Kerék_Eszterga_Naptár>();
                    foreach (Adat_Dolgozó_Beosztás_Új rekord in AdatBEO)
                    {
                        if (rekord.Beosztáskód.Trim() != "#")
                        {
                            DateTime Munka_eleje = new DateTime(rekord.Nap.Year, rekord.Nap.Month, rekord.Nap.Day, rekordBKód.Munkaidőkezdet.Hour, rekordBKód.Munkaidőkezdet.Minute, 0);
                            DateTime Munka_vége;
                            if (rekord.Túlóraok.Contains("&T"))
                                Munka_vége = Munka_eleje.AddMinutes(rekord.Túlóra + rekord.Ledolgozott);//Túlóra
                            else
                                Munka_vége = Munka_eleje.AddMinutes(rekord.Ledolgozott);//Elvont pihenő
                            Adat_Kerék_Eszterga_Naptár ADAT = new Adat_Kerék_Eszterga_Naptár(true, Munka_eleje, Munka_vége);
                            AdatokGy.Add(ADAT);
                        }
                        else
                        {
                            if (!rekord.Megjegyzés.Contains("#"))
                            {
                                DateTime Munka_eleje = new DateTime(rekord.Nap.Year, rekord.Nap.Month, rekord.Nap.Day, rekord.Túlórakezd.Hour, rekord.Túlórakezd.Minute, 0);
                                DateTime Munka_vége = Munka_eleje.AddMinutes(rekord.Túlóra);

                                Adat_Kerék_Eszterga_Naptár ADAT = new Adat_Kerék_Eszterga_Naptár(true, Munka_eleje, Munka_vége);
                                AdatokGy.Add(ADAT);
                            }
                            else
                            {
                                string[] darabol = rekord.Megjegyzés.Trim().Split('-');
                                DateTime kezdet;
                                int munkaidő;
                                if (darabol.Length != 3)
                                {
                                    kezdet = new DateTime(rekord.Nap.Year, rekord.Nap.Month, rekord.Nap.Day, 6, 0, 0);
                                    munkaidő = 30;
                                }

                                if (!DateTime.TryParse(darabol[1], out kezdet))
                                    kezdet = new DateTime(rekord.Nap.Year, rekord.Nap.Month, rekord.Nap.Day, 6, 0, 0);
                                int óra = kezdet.Hour;
                                int perc = kezdet.Minute;
                                if (!int.TryParse(darabol[2], out munkaidő))
                                    munkaidő = 30;

                                DateTime Munka_eleje = new DateTime(rekord.Nap.Year, rekord.Nap.Month, rekord.Nap.Day, óra, perc, 0);
                                DateTime Munka_vége = Munka_eleje.AddMinutes(munkaidő);

                                Adat_Kerék_Eszterga_Naptár ADAT = new Adat_Kerék_Eszterga_Naptár(true, Munka_eleje, Munka_vége);
                                AdatokGy.Add(ADAT);
                            }
                        }
                        Holtart.Lép();
                    }
                    if (AdatokGy.Count > 0) KézNaptár.Módosítás_Munkaidő(Hételső.Year, AdatokGy);
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

        private void Eszt_Új_Beosztás(string Telephely, string dolgozószám, DateTime Dátumtól, DateTime Dátumig)
        {
            try
            {
                List<Adat_Dolgozó_Beosztás_Új> Adatok = Kezelő_Beoszt_Új.Lista_Adatok(Telephely.Trim(), Dátumtól);
                if (Adatok.Count > 0)
                {
                    //Az új beosztásból vesszük az adatokat
                    if (Dátumtól.Year != Dátumig.Year)
                    {
                        //évváltás
                        DateTime Éveleje = MyF.Hónap_elsőnapja(Dátumig);
                        Új_Beosztás_hónap(Telephely, dolgozószám, Éveleje, Dátumig);
                    }
                    else
                    {
                        if (Dátumtól.Month != Dátumig.Month)
                        {
                            //hónapváltás
                            DateTime Hónapeleje = MyF.Hónap_elsőnapja(Dátumig);
                            Új_Beosztás_hónap(Telephely, dolgozószám, Hónapeleje, Dátumig);
                        }
                    }
                    Új_Beosztás_hónap(Telephely, dolgozószám, Dátumtól, Dátumig);
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

        private void Eszt_Új_Beosztás_besegítő(string Telephely, string dolgozószám, DateTime Dátumtól, DateTime Dátumig)
        {
            try
            {
                //Az új beosztásból vesszük az adatokat
                if (Dátumtól.Year != Dátumig.Year)
                {
                    //évváltás
                    DateTime Éveleje = MyF.Hónap_elsőnapja(Dátumig);
                    Új_Beosztás_hónap_besegítő(Telephely, dolgozószám, Éveleje, Dátumig);
                }
                else
                {
                    if (Dátumtól.Month != Dátumig.Month)
                    {
                        //hónapváltás
                        DateTime Hónapeleje = MyF.Hónap_elsőnapja(Dátumig);
                        Új_Beosztás_hónap_besegítő(Telephely, dolgozószám, Hónapeleje, Dátumig);
                    }
                }
                Új_Beosztás_hónap_besegítő(Telephely, dolgozószám, Dátumtól, Dátumig);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ez a változat az új adatbázisból emeli át az adatokat.
        /// </summary>
        /// <param name="Telephely"></param>
        /// <param name="dolgozószám"></param>
        /// <param name="Dátumtól"></param>
        /// <param name="Dátumig"></param>
        private void Új_Beosztás_hónap_besegítő(string Telephely, string dolgozószám, DateTime Dátumtól, DateTime Dátumig)
        {
            try
            {
                Adatok_Beoszt_Új = Kezelő_Beoszt_Új.Lista_Adatok(Telephely.Trim(), Dátumtól);
                List<Adat_Dolgozó_Beosztás_Új> AdatokSzűrt = (from a in Adatok_Beoszt_Új
                                                              where a.Dolgozószám == dolgozószám.Trim()
                                                              select a).ToList();
                if (AdatokSzűrt.Count > 0)
                {
                    List<Adat_Dolgozó_Beosztás_Új> Adatok = new List<Adat_Dolgozó_Beosztás_Új>();
                    if (Dátumtól > Dátumig)
                        Adatok = (from a in Adatok_Beoszt_Új
                                  where a.Dolgozószám == dolgozószám.Trim()
                                && a.Nap >= Dátumtól
                                  orderby a.Nap
                                  select a).ToList();
                    else
                        Adatok = (from a in Adatok_Beoszt_Új
                                  where a.Dolgozószám == dolgozószám.Trim()
                                && a.Nap >= Dátumtól
                                && a.Nap <= Dátumig
                                  orderby a.Nap
                                  select a).ToList();

                    Kezelő_Beoszt_Új.Rögzítés(Telephely.Trim(), Dátumtól, Adatok, true);
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

        private void Új_Beosztás_hónap(string Telephely, string dolgozószám, DateTime Dátumtól, DateTime Dátumig)
        {
            try
            {
                Adatok_Beoszt_Új = Kezelő_Beoszt_Új.Lista_Adatok(Telephely.Trim(), Dátumtól);
                List<Adat_Dolgozó_Beosztás_Új> AdatokSzűrt = (from a in Adatok_Beoszt_Új
                                                              where a.Dolgozószám == dolgozószám.Trim()
                                                              select a).ToList();
                if (AdatokSzűrt.Count > 0)
                {
                    List<Adat_Dolgozó_Beosztás_Új> Adatok = new List<Adat_Dolgozó_Beosztás_Új>();
                    if (Dátumtól > Dátumig)
                        Adatok = (from a in Adatok_Beoszt_Új
                                  where a.Dolgozószám == dolgozószám.Trim()
                                  && a.Nap >= Dátumtól
                                  orderby a.Nap
                                  select a).ToList();
                    else
                        Adatok = (from a in Adatok_Beoszt_Új
                                  where a.Dolgozószám == dolgozószám.Trim()
                                  && a.Nap >= Dátumtól
                                  && a.Nap <= Dátumig
                                  orderby a.Nap
                                  select a).ToList();
                    Kezelő_Beoszt_Új.Rögzítés(Telephely.Trim(), Dátumtól, Adatok, true);
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

        private void Eszt_Beosztás_Törlés(string Telephely, DateTime Dátumtól, DateTime Dátumig)
        {
            try
            {
                //Beolvassuk az adatokat
                Adatok_Beoszt_Új = Kezelő_Beoszt_Új.Lista_Adatok(Telephely.Trim(), Dátumtól, true);
                //ha nem ugyanabban a hónapban van a két dátum és van adat a két idő között, akkor töröljük az eddigi adatokat.
                if (Adatok_Beoszt_Új.Any(a => a.Nap >= Dátumtól && a.Nap <= Dátumig)) Kezelő_Beoszt_Új.Törlés(Telephely.Trim(), Dátumtól, Dátumtól, Dátumig, true);
                if (Dátumtól.Month != Dátumig.Month)
                    if (Adatok_Beoszt_Új.Any(a => a.Nap >= Dátumtól && a.Nap <= Dátumig)) Kezelő_Beoszt_Új.Törlés(Telephely.Trim(), Dátumig, Dátumtól, Dátumig, true);
            }
            catch (HibásBevittAdat ex)
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


        #region Terv
        private void Terv_Lista_Click(object sender, EventArgs e)
        {
            Terv_lista_elj();
        }

        private void Terv_lista_elj()
        {
            try
            {
                Terv_Tábla.Rows.Clear();
                Terv_Tábla.Columns.Clear();
                Terv_Tábla.Refresh();
                Terv_Tábla.Visible = false;
                Terv_Tábla.ColumnCount = 8;
                Terv_Tábla.RowCount = 48;

                Terv_Tábla.Columns[0].HeaderText = "Óra";
                Terv_Tábla.Columns[0].Width = 80;
                Terv_Tábla.Columns[0].Frozen = true;
                Terv_Tábla.Columns[1].HeaderText = "Hétfő";
                Terv_Tábla.Columns[1].Width = 200;
                Terv_Tábla.Columns[2].HeaderText = "Kedd";
                Terv_Tábla.Columns[2].Width = 200;
                Terv_Tábla.Columns[3].HeaderText = "Szerda";
                Terv_Tábla.Columns[3].Width = 200;
                Terv_Tábla.Columns[4].HeaderText = "Csütörtök";
                Terv_Tábla.Columns[4].Width = 200;
                Terv_Tábla.Columns[5].HeaderText = "Péntek";
                Terv_Tábla.Columns[5].Width = 200;
                Terv_Tábla.Columns[6].HeaderText = "Szombat";
                Terv_Tábla.Columns[6].Width = 200;
                Terv_Tábla.Columns[7].HeaderText = "Vasárnap";
                Terv_Tábla.Columns[7].Width = 200;

                DateTime óra = new DateTime(1900, 1, 1, 0, 0, 0);
                for (int i = 0; i < 48; i++)
                {
                    Terv_Tábla.Rows[i].Cells[0].Value = óra.ToString("HH:mm");

                    óra = óra.AddMinutes(30);
                }


                DateTime Hételső = MyF.Hét_elsőnapja(Dátum.Value);
                DateTime IdeigDát = Hételső;
                DateTime Hétutolsó = MyF.Hét_Utolsónapja(Dátum.Value);
                List<Adat_Kerék_Eszterga_Naptár> Adatok = KézNaptár.Lista_Adatok(Hételső.Year);
                if (Hételső.Year != Hétutolsó.Year)
                {
                    List<Adat_Kerék_Eszterga_Naptár> AdatokIdeig = KézNaptár.Lista_Adatok(Hétutolsó.Year);
                    Adatok.AddRange(AdatokIdeig);
                }
                Adatok = (from a in Adatok
                          where a.Idő >= MyF.Nap0000(Hételső)
                          && a.Idő <= MyF.Nap2359(Hétutolsó)
                          orderby a.Idő
                          orderby a.Idő
                          select a).ToList();
                Szín_kódolás Szín;

                int k = 1;
                while (k < 8)
                {
                    Terv_Tábla.Columns[k].HeaderText = IdeigDát.ToString("MM.dd  dddd");
                    IdeigDát = IdeigDát.AddDays(1);
                    k++;
                }



                foreach (Adat_Kerék_Eszterga_Naptár rekord in Adatok)
                {
                    int oszlop = MyF.Hét_Melyiknapja(rekord.Idő);
                    int sor = (2 * rekord.Idő.Hour) + (rekord.Idő.Minute == 30 ? 1 : 0);

                    if (rekord.Munkaidő)
                        Terv_Tábla.Rows[sor].Cells[oszlop].Style.BackColor = Color.Green;
                    if (rekord.Foglalt)
                    {
                        Terv_Tábla.Rows[sor].Cells[oszlop].Value = rekord.Pályaszám.Trim();
                        Szín = MyColor.Szín_váltó(rekord.BetűSzín);
                        Terv_Tábla.Rows[sor].Cells[oszlop].Style.ForeColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);
                        Szín = MyColor.Szín_váltó(rekord.HáttérSzín);
                        Terv_Tábla.Rows[sor].Cells[oszlop].Style.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);
                        if (!rekord.Munkaidő)
                        {
                            // Ha időközben kikerült a munkaidő akkor tovább alakítjuk
                            Terv_Tábla.Rows[sor].Cells[oszlop].Style.Font = new Font("Microsoft Sans Serif", 6, FontStyle.Italic);
                        }
                    }
                }
                Terv_Tábla.FirstDisplayedScrollingRowIndex = 12;

                Terv_Tábla.Visible = true;
            }
            catch (HibásBevittAdat ex)
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


        #region Terv küldés
        private void Heti_terv_küldés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Terv_Tábla.Rows.Count < 1) throw new HibásBevittAdat("A terv táblának nincs érvényes adata.");
                Email();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Email()
        {
            try
            {
                //Levél érdemi része
                string Html_szöveg = "<html><body>";
                DateTime Hételső = MyF.Hét_elsőnapja(Dátum.Value);
                DateTime Hétutolsó = MyF.Hét_Utolsónapja(Dátum.Value);
                string Border = $"border-width:1px;border-style:solid;border-color:{MyColor.ColorToHex(Color.Black)}";

                Html_szöveg += $"<p><i>Barossi kerékeszterga {Hételső:yyyy.MM.dd}-{Hétutolsó:yyyy.MM.dd} közötti esztergálási terve</i></p></br>";
                Html_szöveg += $"</br>";
                Html_szöveg += $"<table cellpadding='5' cellspacing='0' style='{Border};font-size: 12pt'>";

                //Fejléc
                Html_szöveg += $"<tr><th style='background-color: #B8DBFD;{Border}'>Óra</th>";
                Html_szöveg += $"<th style='background-color:#B8DBFD;{Border}'>{Terv_Tábla.Columns[1].HeaderText}</th>";
                Html_szöveg += $"<th style='background-color:#B8DBFD;{Border}'>{Terv_Tábla.Columns[2].HeaderText}</th>";
                Html_szöveg += $"<th style='background-color:#B8DBFD;{Border}'>{Terv_Tábla.Columns[3].HeaderText}</th>";
                Html_szöveg += $"<th style='background-color:#B8DBFD;{Border}'>{Terv_Tábla.Columns[4].HeaderText}</th>";
                Html_szöveg += $"<th style='background-color:#B8DBFD;{Border}'>{Terv_Tábla.Columns[5].HeaderText}</th>";
                Html_szöveg += $"<th style='background-color:#B8DBFD;{Border}'>{Terv_Tábla.Columns[6].HeaderText}</th>";
                Html_szöveg += $"<th style='background-color:#B8DBFD;{Border}'>{Terv_Tábla.Columns[7].HeaderText}</th></tr>";

                for (int i = 0; i < Terv_Tábla.Rows.Count - 1; i++)
                {
                    string szöveg0 = Terv_Tábla.Rows[i].Cells[0].Value != null ? Pályaszám_ellenőrzés(Terv_Tábla.Rows[i].Cells[0].Value.ToString()) : "";
                    string szöveg1 = Terv_Tábla.Rows[i].Cells[1].Value != null ? Pályaszám_ellenőrzés(Terv_Tábla.Rows[i].Cells[1].Value.ToString()) : "";
                    string szöveg2 = Terv_Tábla.Rows[i].Cells[2].Value != null ? Pályaszám_ellenőrzés(Terv_Tábla.Rows[i].Cells[2].Value.ToString()) : "";
                    string szöveg3 = Terv_Tábla.Rows[i].Cells[3].Value != null ? Pályaszám_ellenőrzés(Terv_Tábla.Rows[i].Cells[3].Value.ToString()) : "";
                    string szöveg4 = Terv_Tábla.Rows[i].Cells[4].Value != null ? Pályaszám_ellenőrzés(Terv_Tábla.Rows[i].Cells[4].Value.ToString()) : "";
                    string szöveg5 = Terv_Tábla.Rows[i].Cells[5].Value != null ? Pályaszám_ellenőrzés(Terv_Tábla.Rows[i].Cells[5].Value.ToString()) : "";
                    string szöveg6 = Terv_Tábla.Rows[i].Cells[6].Value != null ? Pályaszám_ellenőrzés(Terv_Tábla.Rows[i].Cells[6].Value.ToString()) : "";
                    string szöveg7 = Terv_Tábla.Rows[i].Cells[7].Value != null ? Pályaszám_ellenőrzés(Terv_Tábla.Rows[i].Cells[7].Value.ToString()) : "";

                    Color szín1 = Terv_Tábla.Rows[i].Cells[0].Style.BackColor.Name == "0" ? Color.WhiteSmoke : Terv_Tábla.Rows[i].Cells[0].Style.BackColor;
                    Color szín2 = Terv_Tábla.Rows[i].Cells[1].Style.BackColor.Name == "0" ? Color.WhiteSmoke : Terv_Tábla.Rows[i].Cells[1].Style.BackColor;
                    Color szín3 = Terv_Tábla.Rows[i].Cells[2].Style.BackColor.Name == "0" ? Color.WhiteSmoke : Terv_Tábla.Rows[i].Cells[2].Style.BackColor;
                    Color szín4 = Terv_Tábla.Rows[i].Cells[3].Style.BackColor.Name == "0" ? Color.WhiteSmoke : Terv_Tábla.Rows[i].Cells[3].Style.BackColor;
                    Color szín5 = Terv_Tábla.Rows[i].Cells[4].Style.BackColor.Name == "0" ? Color.WhiteSmoke : Terv_Tábla.Rows[i].Cells[4].Style.BackColor;
                    Color szín6 = Terv_Tábla.Rows[i].Cells[5].Style.BackColor.Name == "0" ? Color.WhiteSmoke : Terv_Tábla.Rows[i].Cells[5].Style.BackColor;
                    Color szín7 = Terv_Tábla.Rows[i].Cells[6].Style.BackColor.Name == "0" ? Color.WhiteSmoke : Terv_Tábla.Rows[i].Cells[6].Style.BackColor;
                    Color szín8 = Terv_Tábla.Rows[i].Cells[7].Style.BackColor.Name == "0" ? Color.WhiteSmoke : Terv_Tábla.Rows[i].Cells[7].Style.BackColor;

                    Html_szöveg += $"<tr>" +
                                   $"<td style='background-color:{MyColor.ColorToHex(szín1)};{Border}'>{szöveg0}</td>" +
                                   $"<td style='background-color:{MyColor.ColorToHex(szín2)};{Border}'>{szöveg1}</td>" +
                                   $"<td style='background-color:{MyColor.ColorToHex(szín3)};{Border}'>{szöveg2}</td>" +
                                   $"<td style='background-color:{MyColor.ColorToHex(szín4)};{Border}'>{szöveg3}</td>" +
                                   $"<td style='background-color:{MyColor.ColorToHex(szín5)};{Border}'>{szöveg4}</td>" +
                                   $"<td style='background-color:{MyColor.ColorToHex(szín6)};{Border}'>{szöveg5}</td>" +
                                   $"<td style='background-color:{MyColor.ColorToHex(szín7)};{Border}'>{szöveg6}</td>" +
                                   $"<td style='background-color:{MyColor.ColorToHex(szín8)};{Border}'>{szöveg7}</td></tr>";
                }

                Html_szöveg += "</table>";
                Html_szöveg += "<p>Ez az e-mail a Villamos program által készített automatikus üzenet .</p>";
                Html_szöveg += "</body></html>";



                MyO._Application _app = new MyO.Application();
                MyO.MailItem mail = (MyO.MailItem)_app.CreateItem(MyO.OlItemType.olMailItem);

                // címzettek
                List<Adat_Kerék_Eszterga_Terjesztés> Adatok = KézTerjeszt.Lista_Adatok();
                Adatok = (from a in Adatok
                          where a.Változat == 1
                          || a.Változat == 3
                          orderby a.Név
                          select a).ToList();

                string címzettek = "";
                foreach (Adat_Kerék_Eszterga_Terjesztés rekord in Adatok)
                    címzettek += rekord.Email + "; ";

                mail.To = címzettek;

                // üzenet tárgya
                mail.Subject = $"Kerékeszterga {MyF.Hét_Sorszáma(Dátum.Value)}.-ik heti tervezett ütemterve ";
                // üzent szövege
                mail.HTMLBody = Html_szöveg;
                mail.Importance = MyO.OlImportance.olImportanceNormal;

                ((MyO._MailItem)mail).Send();

                MessageBox.Show("Üzenet el lett küldve", "Üzenet küldés sikeres", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        string Pályaszám_ellenőrzés(string vezényelt)
        {
            if (Honos == null) Honos_feltöltés();
            string válasz = vezényelt.Trim();
            string[] darabol = vezényelt.Split('=');
            string pályaszám;

            if (darabol.Length > 1)
            {
                if (darabol[0].Contains('-'))
                {
                    string[] tovább = darabol[0].Split('-');
                    pályaszám = tovább[0].Trim();
                }
                else
                    pályaszám = darabol[0].Trim();

                string telephely = "";
                Adat_Jármű EgyJármű = Honos.Where(Elem => Elem.Azonosító.Trim() == pályaszám.Trim()).FirstOrDefault();
                if (EgyJármű != null) telephely = EgyJármű.Üzem.Trim();

                if (telephely.Trim() != "" && darabol[1].Trim() != telephely.Trim())
                    válasz += "<br>Honos:" + telephely.Trim();
            }
            return válasz;
        }

        private void Honos_feltöltés()
        {
            try
            {
                Honos = KézJármű.Lista_Adatok("Főmérnökség");
                Honos = (from a in Honos
                         where a.Törölt == false
                         orderby a.Azonosító
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


        #region Lejelentés
        private void Automata_Jelentés()
        {
            try
            {
                List<Adat_Kerék_Eszterga_Automata> Lista = KézAuto.Lista_Adatok();
                if (Lista == null) return;

                DateTime Utolsó = Lista.Max(a => a.UtolsóÜzenet);
                if (Utolsó >= MyF.Hét_elsőnapja(DateTime.Today)) return;   //ha a héten már küldött  valaki üzenetet
                if (!Lista.Any(a => a.FelhasználóiNév.Trim() == Program.PostásNév.Trim())) return;  //ha nincs benne a listában a személy akkor nem küld a nevében

                while (Utolsó < MyF.Hét_elsőnapja(DateTime.Today))
                {
                    Dátum.Value = Utolsó;
                    Heti_jelentés_eljárás();
                    Utolsó = Utolsó.AddDays(7);
                }
                Adat_Kerék_Eszterga_Automata ADAT = new Adat_Kerék_Eszterga_Automata(Program.PostásNév.Trim(), DateTime.Today);
                KézAuto.Módosítás(ADAT);
                Dátum.Value = DateTime.Today;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Heti_jelentés_eljárás()
        {
            try
            {
                Beosztás_Adatok();
                string fájlexc;

                fájlexc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + $@"\Eszterga_Lejelentés_{MyF.Hét_Sorszáma(Dátum.Value)}_heti_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}.xlsx";

                Holtart.Be();
                Kerékeszterga_Excel KerExc = new Kerékeszterga_Excel(fájlexc, Dátum.Value);
                Holtart.Lép();
                KerExc.Excel_alaptábla();
                Holtart.Lép();
                Email_jelentés(fájlexc);
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

        private void Heti_jelentés_Click(object sender, EventArgs e)
        {
            try
            {
                Heti_jelentés_eljárás();
                KézAuto.Módosítás(new Adat_Kerék_Eszterga_Automata(Program.PostásNév.Trim(), DateTime.Today));
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Email_jelentés(string fájlexc)
        {
            try
            {
                string Border = $"border-width:1px;border-style:solid;border-color:{MyColor.ColorToHex(Color.Black)}";

                //Levél érdemi része
                string Html_szöveg = "<html><body>";
                Html_szöveg += $"<b>Kerékeszterga {MyF.Hét_Sorszáma(Dátum.Value)}.-ik heti elvégzett tevékenységeinek összefoglaló jelentését mellékelten megküldöm.</b><br><br>";
                Html_szöveg += "<p>Ez az e-mail a Villamos program által készített automatikus üzenet .</p>";
                Html_szöveg += "</body></html>";

                MyO._Application _app = new MyO.Application();
                MyO.MailItem mail = (MyO.MailItem)_app.CreateItem(MyO.OlItemType.olMailItem);

                // címzettek
                List<Adat_Kerék_Eszterga_Terjesztés> Adatok = KézTerjeszt.Lista_Adatok();
                Adatok = (from a in Adatok
                          where a.Változat == 2
                          || a.Változat == 3
                          orderby a.Név
                          select a).ToList();

                string címzettek = "";
                foreach (Adat_Kerék_Eszterga_Terjesztés rekord in Adatok)
                    címzettek += rekord.Email + "; ";

                mail.To = címzettek;

                // üzenet tárgya
                mail.Subject = $"Kerékeszterga {MyF.Hét_Sorszáma(Dátum.Value)}.-ik heti elvégzett tevékenységeinek összefoglaló jelentése";
                // üzent szövege
                mail.HTMLBody = Html_szöveg;
                mail.Importance = MyO.OlImportance.olImportanceNormal;
                //csatolmány
                mail.Attachments.Add(fájlexc);
                ((MyO._MailItem)mail).Send();

                MessageBox.Show("Üzenet el lett küldve", "Üzenet küldés sikeres", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
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


        #region Beállítások
        Ablak_Eszterga_Dolgozók Új_Ablak_Eszterga_Dolgozók;
        private void Esztergályosok_Click(object sender, EventArgs e)
        {
            Új_Ablak_Eszterga_Dolgozók?.Close();

            Új_Ablak_Eszterga_Dolgozók = new Ablak_Eszterga_Dolgozók(Cmbtelephely.Text.Trim());
            Új_Ablak_Eszterga_Dolgozók.FormClosed += Ablak_Eszterga_Dolgozók_Closed;
            Új_Ablak_Eszterga_Dolgozók.StartPosition = FormStartPosition.CenterScreen;
            Új_Ablak_Eszterga_Dolgozók.Show();
        }

        private void Ablak_Eszterga_Dolgozók_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Eszterga_Dolgozók = null;
        }

        Ablak_Eszterga_Választék Új_Ablak_Eszterga_Választék;
        private void Választék_Lista_Click(object sender, EventArgs e)
        {
            Új_Ablak_Eszterga_Választék?.Close();

            Új_Ablak_Eszterga_Választék = new Ablak_Eszterga_Választék();
            Új_Ablak_Eszterga_Választék.FormClosed += Ablak_Eszterga_Választék_Closed;
            Új_Ablak_Eszterga_Választék.StartPosition = FormStartPosition.CenterScreen;
            Új_Ablak_Eszterga_Választék.Show();
        }

        private void Ablak_Eszterga_Választék_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Eszterga_Választék = null;
        }
        #endregion


        #region Rögzítés
        Ablak_Eszterga_Segéd Új_Ablak_Eszterga_Segéd;
        private void Terv_Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0) return;


            DateTime idő = DateTime.Parse(Terv_Tábla.Rows[e.RowIndex].Cells[0].Value.ToString());
            DateTime Hételső = MyF.Hét_elsőnapja(Dátum.Value);
            DateTime dátum = Hételső.AddDays(e.ColumnIndex - 1);
            DátumÉsIdő = new DateTime(dátum.Year, dátum.Month, dátum.Day, idő.Hour, idő.Minute, 0);

            if (Új_Ablak_Eszterga_Segéd != null)
            {
                Új_Ablak_Eszterga_Segéd = new Ablak_Eszterga_Segéd(DátumÉsIdő, 0);
                Új_Ablak_Eszterga_Segéd.FormClosed += Ablak_Eszterga_Segéd_Closed;
                Új_Ablak_Eszterga_Segéd.Változás += Terv_lista_elj;
                Új_Ablak_Eszterga_Segéd.StartPosition = FormStartPosition.CenterScreen;
                Új_Ablak_Eszterga_Segéd.Show();
            }
        }

        private void Ablak_Eszterga_Segéd_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Eszterga_Segéd = null;
        }

        private void Ablak_nyitás(DateTime DátumÉsIdő, int Mód)
        {
            Új_Ablak_Eszterga_Segéd?.Close();

            Új_Ablak_Eszterga_Segéd = new Ablak_Eszterga_Segéd(DátumÉsIdő, Mód);
            Új_Ablak_Eszterga_Segéd.FormClosed += Ablak_Eszterga_Segéd_Closed;
            Új_Ablak_Eszterga_Segéd.Változás += Terv_lista_elj;
            Új_Ablak_Eszterga_Segéd.StartPosition = FormStartPosition.CenterScreen;
            Új_Ablak_Eszterga_Segéd.Show();
        }

        private void Rögzítés_Click(object sender, EventArgs e)
        {
            Ablak_nyitás(DátumÉsIdő, 0);

        }

        private void Sor_Beszúrása_Click(object sender, EventArgs e)
        {
            Ablak_nyitás(DátumÉsIdő, 1);
        }

        private void Sor_törlése_Click(object sender, EventArgs e)
        {
            Ablak_nyitás(DátumÉsIdő, 2);
        }

        private void Munkaközi_Click(object sender, EventArgs e)
        {
            Ablak_nyitás(DátumÉsIdő, 3);
        }
        #endregion


        #region Igények
        private void Elkészült_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.SelectedRows.Count < 1) throw new HibásBevittAdat("Nincs kijelölve módosítandó sor.");
                foreach (DataGridViewRow SOR in Tábla.SelectedRows)
                {
                    //Csak az ütemezett kocsikkal foglalkozunk
                    if ("Ütemezett" == SOR.Cells[5].Value.ToStrTrim())
                    {
                        KézIgény.Módosítás_Státus(DateTime.Parse(SOR.Cells[1].Value.ToString()).Year,
                                                  SOR.Cells[2].Value.ToString(),
                                                  2,
                                                  7);
                    }
                    else
                        MessageBox.Show("Csak Ütemezett feladatokat lehet készre jelenteni.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                Lista_Tábla_kiírás();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Visszaállítás_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.SelectedRows.Count < 1) throw new HibásBevittAdat("Nincs kijelölve módosítandó sor.");
                foreach (DataGridViewRow SOR in Tábla.SelectedRows)
                {
                    //Csak az Elkészült kocsikkal foglalkozunk
                    if ("Elkészült" == SOR.Cells[5].Value.ToStrTrim())
                    {
                        KézIgény.Módosítás_Státus(DateTime.Parse(SOR.Cells[1].Value.ToString()).Year,
                                           SOR.Cells[2].Value.ToString(),
                                           7,
                                           2);
                    }
                    else
                        MessageBox.Show("Csak Elkészült feladatokat lehet visszaállítani ütemezettre.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Lista_Tábla_kiírás();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Törölt_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.SelectedRows.Count < 1) throw new HibásBevittAdat("Nincs kijelölve módosítandó sor.");
                foreach (DataGridViewRow SOR in Tábla.SelectedRows)
                {
                    //Csak a saját telephely igényeit lehet törölni
                    if (Cmbtelephely.Text.Trim() == SOR.Cells[3].Value.ToStrTrim())
                    {
                        //Csak az igény státusú kocsikkal foglalkozunk
                        if ("Igény" == SOR.Cells[5].Value.ToStrTrim())
                        {
                            KézIgény.Módosítás_Státus(DateTime.Parse(SOR.Cells[1].Value.ToString()).Year,
                                           SOR.Cells[2].Value.ToString(),
                                           0,
                                           9);
                        }
                        else
                            MessageBox.Show("Csak Igény státusú feladatokat lehet törölni.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Csak a saját telephely igény státusú feladatait lehet törölni.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Lista_Tábla_kiírás();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Excel_készítés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Eszterga_Igény_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyE.DataGridViewToExcel(fájlexc, Tábla);
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


        #region Terjesztési lista
        Ablak_Eszterga_Terjesztés Új_Ablak_Eszterga_Terjesztés;
        private void Terjesztési_Click(object sender, EventArgs e)
        {
            Új_Ablak_Eszterga_Terjesztés?.Close();

            Új_Ablak_Eszterga_Terjesztés = new Ablak_Eszterga_Terjesztés();
            Új_Ablak_Eszterga_Terjesztés.FormClosed += Ablak_Eszterga_Terjesztés_Closed;
            Új_Ablak_Eszterga_Terjesztés.StartPosition = FormStartPosition.CenterScreen;
            Új_Ablak_Eszterga_Terjesztés.Show();
        }

        private void Ablak_Eszterga_Terjesztés_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Eszterga_Terjesztés = null;
        }
        #endregion


        #region beosztásAblak
        Ablak_Eszterga_Beosztás Új_Ablak_Eszterga_Beosztás;
        private void MiniBeosztás_Click(object sender, EventArgs e)
        {
            Új_Ablak_Eszterga_Beosztás?.Close();

            Új_Ablak_Eszterga_Beosztás = new Ablak_Eszterga_Beosztás(Dátum.Value, Application.StartupPath);
            Új_Ablak_Eszterga_Beosztás.FormClosed += Új_Ablak_Eszterga_Beosztás_Closed;
            Új_Ablak_Eszterga_Beosztás.StartPosition = FormStartPosition.CenterScreen;
            Új_Ablak_Eszterga_Beosztás.Show();
        }

        private void Új_Ablak_Eszterga_Beosztás_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Eszterga_Beosztás = null;
        }
        #endregion
    }
}
