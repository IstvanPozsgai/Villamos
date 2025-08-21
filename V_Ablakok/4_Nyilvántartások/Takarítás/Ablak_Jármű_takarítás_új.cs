using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_Ablakok._4_Nyilvántartások.Takarítás;
using Villamos.V_Ablakok._7_Gondnokság.Épület_takarítás;
using Villamos.Villamos_Ablakok._4_Nyilvántartások.Jármű_Takarítás;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;
using MyEn = Villamos.V_MindenEgyéb.Enumok;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    public delegate void Esemény_Delegált();
    public partial class Ablak_Jármű_takarítás_új
    {
#pragma warning disable IDE0044 // Add readonly modifier
        DataTable AdatTábla_Utolsó = new DataTable();
        DataTable GépiTábla = new DataTable();
#pragma warning restore IDE0044 // Add readonly modifier

        #region Kezelők-Adatok
        readonly Kezelő_Jármű_Takarítás_Vezénylés KézVezény = new Kezelő_Jármű_Takarítás_Vezénylés();
        readonly Kezelő_Jármű_Takarítás KézTak = new Kezelő_Jármű_Takarítás();
        readonly Kezelő_Vezénylés KézVez = new Kezelő_Vezénylés();
        readonly Kezelő_Jármű_Takarítás_Teljesítés KézTakarításTelj = new Kezelő_Jármű_Takarítás_Teljesítés();
        readonly Kezelő_Jármű_Takarítás_J1 KézJ1 = new Kezelő_Jármű_Takarítás_J1();
        readonly Kezelő_Szerelvény KézSzer = new Kezelő_Szerelvény();
        readonly Kezelő_Nap_Hiba KézHiba = new Kezelő_Nap_Hiba();
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly Kezelő_Jármű_Takarítás_Ütemező KézÜtem = new Kezelő_Jármű_Takarítás_Ütemező();
        readonly Kezelő_Jármű_Vendég KézIdegen = new Kezelő_Jármű_Vendég();
        readonly Kezelő_jármű_hiba KézJárműHiba = new Kezelő_jármű_hiba();
        readonly Kezelő_Jármű_Hiba_Napló KézJárműHibaNapló = new Kezelő_Jármű_Hiba_Napló();
        readonly Kezelő_Jármű_Takarítás_Mátrix KézMátrix = new Kezelő_Jármű_Takarítás_Mátrix();
        readonly Kezelő_Jármű_Takarítás_Létszám KézLétsz = new Kezelő_Jármű_Takarítás_Létszám();
        readonly Kezelő_Jármű_Takarítás_Kötbér KézKötbér = new Kezelő_Jármű_Takarítás_Kötbér();
        readonly Kezelő_Jármű_Takarítás_Ár KézÁr = new Kezelő_Jármű_Takarítás_Ár();
        readonly Kezelő_Kiegészítő_Jelenlétiív KézJelen = new Kezelő_Kiegészítő_Jelenlétiív();
        readonly Kezelő_Jármű_Takarítás_Napló KézTakNapló = new Kezelő_Jármű_Takarítás_Napló();
        readonly Kezelő_Jármű_Állomány_Típus KézJárműTípus = new Kezelő_Jármű_Állomány_Típus();
        readonly Kezelő_kiegészítő_telephely Kéztelephely = new Kezelő_kiegészítő_telephely();

        List<Adat_Jármű_Takarítás_Takarítások> AdatokTak = new List<Adat_Jármű_Takarítás_Takarítások>();
        public List<Adat_Jármű_Takarítás_Vezénylés> AdatokVezény = new List<Adat_Jármű_Takarítás_Vezénylés>();
        List<Adat_Vezénylés> AdatokVez = new List<Adat_Vezénylés>();
        List<Adat_Jármű_Takarítás_Teljesítés> AdatokTelj = new List<Adat_Jármű_Takarítás_Teljesítés>();
        List<Adat_Jármű_Takarítás_J1> AdatokJ1 = new List<Adat_Jármű_Takarítás_J1>();
        List<Adat_Szerelvény> AdatokSzer = new List<Adat_Szerelvény>();
        List<Adat_Szerelvény> AdatokSzerelvényElő = new List<Adat_Szerelvény>();
        List<Adat_Nap_Hiba> AdatokHiba = new List<Adat_Nap_Hiba>();
        List<Adat_Jármű> AdatokJármű = new List<Adat_Jármű>();
        List<Adat_Jármű_Takarítás_Ütemező> AdatokÜtem = new List<Adat_Jármű_Takarítás_Ütemező>();
        List<Adat_Jármű_Vendég> AdatokIdegen = new List<Adat_Jármű_Vendég>();
        List<Adat_Jármű_Takarítás_Árak> AdatokÁrak = new List<Adat_Jármű_Takarítás_Árak>();
        List<Adat_Jármű_hiba> AdatokJárműHiba = new List<Adat_Jármű_hiba>();
        List<Adat_Jármű_Takarítás_Mátrix> AdatokMátrix = new List<Adat_Jármű_Takarítás_Mátrix>();
        List<Adat_Jármű_Takarítás_Kötbér> AdatokKötbér = new List<Adat_Jármű_Takarítás_Kötbér>();
        List<Adat_Kiegészítő_Jelenlétiív> AdatokJelen = new List<Adat_Kiegészítő_Jelenlétiív>();

        private int SorIdx = 0;
        public string Telephely_ = "";
        public DateTime Dátum_ = new DateTime(1900, 1, 1);
        public string fájlexcel_ = "";
        #endregion


        #region Form Betöltés, Alap műveletek
        public Ablak_Jármű_takarítás_új()
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

                Visible = false;
                Fülekkitöltése();

                Lapfülek.DrawMode = TabDrawMode.OwnerDrawFixed;
                Lapfülek.SelectedIndex = 0;
                JDátum.Value = DateTime.Today.AddDays(-1);
                Dátum.Value = DateTime.Today;
                Gepi_datum.Value = DateTime.Today;
                Ütem_kezdődátum.Value = DateTime.Today;
                Utolsó_dátum.Value = DateTime.Today;
                ListaDátum.Value = DateTime.Today;

                Background_Process();
                this.KeyPreview = true;

                Refresh();
                Visible = true;

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ablak_Jármű_takarítás_új_Load(object sender, EventArgs e)
        {

        }

        private void Ablak_Jármű_takarítás_új_Shown(object sender, EventArgs e)
        {

        }

        public void Background_Process()
        {
            AdatokSzer = KézSzer.Lista_Adatok(Cmbtelephely.Text.Trim());
            AdatokSzerelvényElő = KézSzer.Lista_Adatok(Cmbtelephely.Text.Trim(), true);
            AdatokHiba = KézHiba.Lista_Adatok(Cmbtelephely.Text.Trim());
            AdatokJármű = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
            AdatokÜtem = KézÜtem.Lista_Adat();
            AdatokVez = KézVez.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value);
            AdatokVezény = KézVezény.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);
            IdegenLista();
        }

        private void Cmbtelephely_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }

        private void Jogosultságkiosztás()
        {
            int melyikelem;
            // ide kell az összes gombot tenni amit szabályozni akarunk false
            Btn_Vezénylésbeírás.Enabled = false;

            Utolsó_módosít.Enabled = false;
            Ütem_Rögzítés.Enabled = false;

            J1Mentés.Enabled = false;
            LétszámMentés.Enabled = false;
            Opció_mentés.Enabled = false;
            Opció_Töröl.Enabled = false;
            JK_Mentés.Enabled = false;

            melyikelem = 181;
            // módosítás 1 
            if (MyF.Vanjoga(melyikelem, 1))
            {
                Utolsó_módosít.Enabled = true;
                Ütem_Rögzítés.Enabled = true;
            }
            // módosítás 2 
            if (MyF.Vanjoga(melyikelem, 2))
            {
                Btn_Vezénylésbeírás.Enabled = true;
            }
            // módosítás 3 
            if (MyF.Vanjoga(melyikelem, 3))
            {
                J1Mentés.Enabled = true;
                LétszámMentés.Enabled = true;
                Opció_mentés.Enabled = true;
                Opció_Töröl.Enabled = true;
                JK_Mentés.Enabled = true;
            }
        }

        private void Fülekkitöltése()
        {
            switch (Lapfülek.SelectedIndex)
            {
                case 0:
                    {
                        Utolsó_pályaszám_feltöltés();
                        Utolsó_takfajta_feltöltés();
                        Utolsó_Telephelyek_feltöltése();

                        Ütem_Telephelyek_feltöltése();
                        Ütem_takfajta_feltöltés();
                        Ütem_pályaszám_feltöltés();
                        Ütem_lépték_feltöltése();
                        break;
                    }
                case 1:
                    {
                        string[] adat = { "J2", "J3", "J4", "J5", "J6" };
                        for (int i = 0; i < adat.Length; i++)
                            Ütemezett_kocsik_részlet(adat[i]);

                        break;
                    }

                case 2:
                    {
                        JK_kat_feltöltés();
                        Típusfeltöltés();
                        Opció_lista_feltöltés();
                        break;
                    }
                case 3:
                    {
                        Lek_kat_feltöltés();
                        break;
                    }
                case 4:
                    {
                        GépiTípusCmbFeltölt();
                        Gepi_palyaszam_feltoltes();
                        Telephelyek();
                        break;
                    }
            }
        }
        #endregion


        #region Alap
        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\jármű_takarítás.html";
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

        private void LapFülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    Cmbtelephely.Items.Add(Elem);

                if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér)
                {
                    Cmbtelephely.Text = Cmbtelephely.Items[0].ToStrTrim();
                    Cmbtelephely.Enabled = Program.Postás_Vezér;
                }
                else
                {
                    Cmbtelephely.Text = Program.PostásTelephely;
                    Cmbtelephely.Enabled = Program.Postás_Vezér;
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


        private void LapFülek_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Határozza meg, hogy melyik lap van jelenleg kiválasztva
            TabPage SelectedTab = Lapfülek.TabPages[e.Index];

            // Szerezze be a lap fejlécének területét
            Rectangle HeaderRect = Lapfülek.GetTabRect(e.Index);

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
                Font BoldFont = new Font(Lapfülek.Font.Name, Lapfülek.Font.Size, FontStyle.Bold);
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

        private void Gombok()
        {
            // színek alaphelyzetbe
            J1Mentés.BackColor = Color.Silver;
            LétszámMentés.BackColor = Color.Silver;
            JK_Mentés.BackColor = Color.Silver;
            Opció_mentés.BackColor = Color.Silver;

            GroupBox1.BackColor = Color.Blue;
            GroupBox2.BackColor = Color.Blue;
            GroupBox4.BackColor = Color.Blue;
            GroupBox3.BackColor = Color.Blue;
        }

        private void Ablak_Jármű_takarítás_új_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kereső?.Close();
            AblakTakSegéd2?.Close();
            AblakTakSegéd1?.Close();
        }
        #endregion


        #region Segédablakok

        bool SegédAblakIgaz = false;

        private Jármű_Takarítás_Ütemezés_Segéd2 AblakTakSegéd2;
        private Jármű_Takarítás_Ütemezés_Segéd1 AblakTakSegéd1;
        Ablak_Kereső Új_Ablak_Kereső;

        private void Melyik_Ablak()
        {
            AblakTakSegéd1?.Close();
            AblakTakSegéd2?.Close();
            if (SegédAblakIgaz)
                AblakTakSegéd1_Megnyit();
            else
                AblakTakSegéd2_Megnyit();
        }

        private void AblakTakSegéd1_Megnyit()
        {
            AblakTakSegéd1 = new Jármű_Takarítás_Ütemezés_Segéd1(this);
            AblakTakSegéd1.FormClosed += AblakTakSegéd1_Closed;
            AblakTakSegéd1.Show();
            AblakTakSegéd1.Esemény += AblakVáltás;
        }

        private void AblakTakSegéd1_Closed(object sender, FormClosedEventArgs e)
        {
            AblakTakSegéd1 = null;
        }

        private void AblakTakSegéd2_Megnyit()
        {
            AblakTakSegéd2 = new Jármű_Takarítás_Ütemezés_Segéd2(this);
            AblakTakSegéd2.FormClosed += AblakTakSegéd2_Closed;
            AblakTakSegéd2.Show();
            AblakTakSegéd2.Esemény += AblakVáltás;
        }

        private void AblakTakSegéd2_Closed(object sender, FormClosedEventArgs e)
        {
            AblakTakSegéd2 = null;
        }

        private void AblakVáltás()
        {
            if (SegédAblakIgaz)
            {
                AblakTakSegéd1?.Close();
                AblakTakSegéd2_Megnyit();
                SegédAblakIgaz = false;
                AblakTakSegéd2?.Kiírja_Kocsi_Másik(SorIdx);
            }
            else
            {
                AblakTakSegéd2?.Close();
                AblakTakSegéd1_Megnyit();
                SegédAblakIgaz = true;
                AblakTakSegéd1?.Kiírja_Kocsi(SorIdx);
            }
        }

        private void SegédAblak_bezárás()
        {
            AblakTakSegéd1?.Close();
            AblakTakSegéd2?.Close();
        }
        #endregion


        #region Ütemezés
        private void Alap_Lista_Click(object sender, EventArgs e)
        {
            try
            {
                Ütemezés_tábla_lista();
                Ütemezettkocsik();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Ütemezés_tábla_lista()
        {
            try
            {
                Holtart.Be();
                #region Fejléc
                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 31;

                Tábla.Columns[0].HeaderText = "Psz";
                Tábla.Columns[0].Width = 70;
                Tábla.Columns[1].HeaderText = "Típus";
                Tábla.Columns[1].Width = 70;
                Tábla.Columns[2].HeaderText = "Hiba";
                Tábla.Columns[2].Width = 300;
                Tábla.Columns[3].HeaderText = "J2 dátum";
                Tábla.Columns[3].Width = 100;
                Tábla.Columns[4].HeaderText = "J2 nap";
                Tábla.Columns[4].Width = 50;
                Tábla.Columns[5].HeaderText = "J2 ütem";
                Tábla.Columns[5].Width = 50;
                Tábla.Columns[6].HeaderText = "J3 dátum";
                Tábla.Columns[6].Width = 100;
                Tábla.Columns[7].HeaderText = "J3 nap";
                Tábla.Columns[7].Width = 50;
                Tábla.Columns[8].HeaderText = "J3 ütem";
                Tábla.Columns[8].Width = 50;
                Tábla.Columns[9].HeaderText = "J4 dátum";
                Tábla.Columns[9].Width = 100;
                Tábla.Columns[10].HeaderText = "J4 nap";
                Tábla.Columns[10].Width = 50;
                Tábla.Columns[11].HeaderText = "J4 ütem";
                Tábla.Columns[11].Width = 50;

                Tábla.Columns[12].HeaderText = "J5 dátum";
                Tábla.Columns[12].Width = 100;
                Tábla.Columns[13].HeaderText = "J5 nap";
                Tábla.Columns[13].Width = 50;
                Tábla.Columns[14].HeaderText = "J5 ütem";
                Tábla.Columns[14].Width = 50;

                Tábla.Columns[15].HeaderText = "J6 dátum";
                Tábla.Columns[15].Width = 100;
                Tábla.Columns[16].HeaderText = "J6 nap";
                Tábla.Columns[16].Width = 50;
                Tábla.Columns[17].HeaderText = "J6 ütem";
                Tábla.Columns[17].Width = 50;

                Tábla.Columns[18].HeaderText = "";
                Tábla.Columns[18].Width = 5;
                Tábla.Columns[19].HeaderText = "Előírt szerelvény szám";
                Tábla.Columns[19].Width = 150;
                Tábla.Columns[20].HeaderText = "Előírt szerelvény";
                Tábla.Columns[20].Width = 150;

                Tábla.Columns[21].HeaderText = "Tényleges szerelvény szám";
                Tábla.Columns[21].Width = 150;
                Tábla.Columns[22].HeaderText = "Tényleges szerelvény";
                Tábla.Columns[22].Width = 150;

                Tábla.Columns[23].HeaderText = "Státus";
                Tábla.Columns[23].Width = 150;

                Tábla.Columns[24].HeaderText = "J2";
                Tábla.Columns[24].Width = 50;
                Tábla.Columns[25].HeaderText = "J3";
                Tábla.Columns[25].Width = 50;
                Tábla.Columns[26].HeaderText = "J4";
                Tábla.Columns[26].Width = 50;
                Tábla.Columns[27].HeaderText = "J5";
                Tábla.Columns[27].Width = 50;
                Tábla.Columns[28].HeaderText = "J6";
                Tábla.Columns[28].Width = 50;

                Tábla.Columns[29].HeaderText = "T5C5 előterv";
                Tábla.Columns[29].Width = 50;
                Tábla.Columns[30].HeaderText = "Telephely";
                Tábla.Columns[30].Width = 100;

                #endregion

                AdatokTak = KézTak.Lista_Adatok();
                List<string> Pályaszámok = (from a in AdatokTak
                                            where a.Telephely == Cmbtelephely.Text.Trim()
                                            && a.Státus == 0
                                            orderby a.Azonosító, a.Takarítási_fajta, a.Dátum ascending
                                            select a.Azonosító).Distinct().ToList();
                List<Adat_Jármű_Takarítás_Takarítások> AdatokTakÖ = AdatokTak;

                foreach (string pályaszám in Pályaszámok)
                {
                    Tábla.RowCount++;
                    int j = Tábla.Rows.Count - 1;
                    Tábla.Rows[j].Cells[0].Value = pályaszám;
                    TimeSpan delta;

                    DateTime Ideig_dátum = MikorVolt(AdatokTakÖ, pályaszám, "J2");
                    Tábla.Rows[j].Cells[3].Value = Ideig_dátum.ToString("yyyy.MM.dd");
                    delta = DateTime.Now - Ideig_dátum;
                    Tábla.Rows[j].Cells[4].Value = (int)delta.TotalDays;

                    Ideig_dátum = MikorVolt(AdatokTakÖ, pályaszám, "J3");
                    Tábla.Rows[j].Cells[6].Value = Ideig_dátum.ToString("yyyy.MM.dd");
                    delta = DateTime.Now - Ideig_dátum;
                    Tábla.Rows[j].Cells[7].Value = (int)delta.TotalDays;

                    Ideig_dátum = MikorVolt(AdatokTakÖ, pályaszám, "J4");
                    Tábla.Rows[j].Cells[9].Value = Ideig_dátum.ToString("yyyy.MM.dd");
                    delta = DateTime.Now - Ideig_dátum;
                    Tábla.Rows[j].Cells[10].Value = (int)delta.TotalDays;

                    Ideig_dátum = MikorVolt(AdatokTakÖ, pályaszám, "J5");
                    Tábla.Rows[j].Cells[12].Value = Ideig_dátum.ToString("yyyy.MM.dd");
                    delta = DateTime.Now - Ideig_dátum;
                    Tábla.Rows[j].Cells[13].Value = (int)delta.TotalDays;

                    Ideig_dátum = MikorVolt(AdatokTakÖ, pályaszám, "J6");
                    Tábla.Rows[j].Cells[15].Value = Ideig_dátum.ToString("yyyy.MM.dd");
                    delta = DateTime.Now - Ideig_dátum;
                    Tábla.Rows[j].Cells[16].Value = (int)delta.TotalDays;

                    Szerelvények_listázása(pályaszám, j);
                    Előírt_szerelvény_listázása(pályaszám, j);
                    Hiba_listázása(pályaszám, j);
                    Típus_listázása(pályaszám, j);
                    Vezénylés_listázása(pályaszám, j);

                    ELőterv_listázása(pályaszám, j, "J2");
                    ELőterv_listázása(pályaszám, j, "J3");
                    ELőterv_listázása(pályaszám, j, "J4");
                    ELőterv_listázása(pályaszám, j, "J5");
                    ELőterv_listázása(pályaszám, j, "J6");

                    Vezénylés_listázása_napi(pályaszám, j, "J2");
                    Vezénylés_listázása_napi(pályaszám, j, "J3");
                    Vezénylés_listázása_napi(pályaszám, j, "J4");
                    Vezénylés_listázása_napi(pályaszám, j, "J5");
                    Vezénylés_listázása_napi(pályaszám, j, "J6");

                    T5C5_ütemezés(pályaszám, j);
                    Idegenben_Telephely_kiírása(pályaszám, j);


                    if (Tábla.Rows[j].Cells[19].Value == null) Tábla.Rows[j].Cells[19].Value = 0;
                    if (Tábla.Rows[j].Cells[21].Value == null) Tábla.Rows[j].Cells[21].Value = 0;
                    Holtart.Lép();
                }

                CellaSzínezés();
                Tábla.Visible = true;
                Tábla.Refresh();
                Tábla.ClearSelection();
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

        private DateTime MikorVolt(List<Adat_Jármű_Takarítás_Takarítások> AdatokTak, string pályaszám, string Takarítási_fajta)
        {
            DateTime Válasz = DateTime.Parse("1900.01.01");
            try
            {
                List<Adat_Jármű_Takarítás_Takarítások> rekord = (from a in AdatokTak
                                                                 where a.Azonosító == pályaszám
                                                                 && a.Státus == 0
                                                                 && a.Takarítási_fajta == Takarítási_fajta
                                                                 orderby a.Dátum
                                                                 select a).ToList();
                if (rekord != null && rekord.Count > 0) Válasz = rekord.Max(a => a.Dátum);
            }
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

        private void Szerelvények_listázása(string azonosító, int sor)
        {
            try
            {
                Adat_Szerelvény rekordszer = (from a in AdatokSzer
                                              where a.Kocsi1 == azonosító || a.Kocsi2 == azonosító || a.Kocsi3 == azonosító ||
                                               a.Kocsi4 == azonosító || a.Kocsi5 == azonosító || a.Kocsi6 == azonosító
                                              orderby a.Szerelvény_ID
                                              select a).FirstOrDefault();
                if (rekordszer != null)
                {
                    string ideigl = "";
                    // ha egyforma akkor kiírjuk
                    if (rekordszer.Kocsi1.Trim() != "_" && rekordszer.Kocsi1.Trim() != "0") ideigl += rekordszer.Kocsi1.Trim();
                    if (rekordszer.Kocsi2.Trim() != "_" && rekordszer.Kocsi2.Trim() != "0") ideigl += "-" + rekordszer.Kocsi2.Trim();
                    if (rekordszer.Kocsi3.Trim() != "_" && rekordszer.Kocsi3.Trim() != "0") ideigl += "-" + rekordszer.Kocsi3.Trim();
                    if (rekordszer.Kocsi4.Trim() != "_" && rekordszer.Kocsi4.Trim() != "0") ideigl += "-" + rekordszer.Kocsi4.Trim();
                    if (rekordszer.Kocsi5.Trim() != "_" && rekordszer.Kocsi5.Trim() != "0") ideigl += "-" + rekordszer.Kocsi5.Trim();
                    if (rekordszer.Kocsi6.Trim() != "_" && rekordszer.Kocsi6.Trim() != "0") ideigl += "-" + rekordszer.Kocsi6.Trim();
                    Tábla.Rows[sor].Cells[21].Value = rekordszer.Szerelvény_ID;
                    Tábla.Rows[sor].Cells[22].Value = ideigl;
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

        private void Előírt_szerelvény_listázása(string azonosító, int sor)
        {
            try
            {

                foreach (Adat_Szerelvény rekord in AdatokSzerelvényElő)
                {
                    Adat_Szerelvény Elem = (from a in AdatokSzerelvényElő
                                            where a.Kocsi1 == azonosító || a.Kocsi2 == azonosító || a.Kocsi3 == azonosító ||
                                                  a.Kocsi4 == azonosító || a.Kocsi5 == azonosító || a.Kocsi6 == azonosító
                                            select a).FirstOrDefault();

                    if (Elem != null)
                    {
                        string ideig = Elem.Kocsi1.Trim();
                        ideig += Elem.Kocsi2.Trim() == "_" ? "" : "-" + Elem.Kocsi2.Trim();
                        ideig += Elem.Kocsi3.Trim() == "_" ? "" : "-" + Elem.Kocsi3.Trim();
                        ideig += Elem.Kocsi4.Trim() == "_" ? "" : "-" + Elem.Kocsi4.Trim();
                        ideig += Elem.Kocsi5.Trim() == "_" ? "" : "-" + Elem.Kocsi5.Trim();
                        ideig += Elem.Kocsi6.Trim() == "_" ? "" : "-" + Elem.Kocsi6.Trim();

                        Tábla.Rows[sor].Cells[19].Value = Elem.Szerelvény_ID;
                        Tábla.Rows[sor].Cells[20].Value = ideig;
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

        private void Hiba_listázása(string azonosító, int sor)
        {
            try
            {
                if (AdatokHiba == null) return;
                Adat_Nap_Hiba rekordszer = (from a in AdatokHiba
                                            where a.Azonosító == azonosító
                                            orderby a.Azonosító
                                            select a).FirstOrDefault();
                if (rekordszer != null)
                {
                    Tábla.Rows[sor].Cells[2].Value = rekordszer.Üzemképtelen + "-" + rekordszer.Beálló + "-" + rekordszer.Üzemképeshiba;
                    Tábla.Rows[sor].Cells[23].Value = rekordszer.Státus;

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

        private void Típus_listázása(string azonosító, int sor)
        {
            try
            {
                Adat_Jármű rekord = (from a in AdatokJármű
                                     where a.Azonosító == azonosító
                                     select a).FirstOrDefault();

                if (rekord != null)
                {
                    Tábla.Rows[sor].Cells[1].Value = rekord.Valóstípus;
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

        private void Vezénylés_listázása(string pályaszám, int sor)
        {
            try
            {
                Adat_Jármű_Takarítás_Vezénylés EgyVezénylés = (from a in AdatokVezény
                                                               where a.Azonosító == pályaszám && a.Dátum == Dátum.Value.AddDays(-1) && a.Státus != 9
                                                               orderby a.Azonosító
                                                               select a).FirstOrDefault();
                if (EgyVezénylés != null)
                {
                    switch (EgyVezénylés.Takarítási_fajta)
                    {
                        case "J2":
                            {
                                Tábla.Rows[sor].Cells[24].Value = "1";
                                break;
                            }
                        case "J3":
                            {
                                Tábla.Rows[sor].Cells[25].Value = "1";
                                break;
                            }
                        case "J4":
                            {
                                Tábla.Rows[sor].Cells[26].Value = "1";
                                break;
                            }
                        case "J5":
                            {
                                Tábla.Rows[sor].Cells[27].Value = "1";
                                break;
                            }
                        case "J6":
                            {
                                Tábla.Rows[sor].Cells[28].Value = "1";
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

        private void ELőterv_listázása(string pályaszám, int sor, string fajta)
        {
            try
            {
                int különbség = 0;
                TimeSpan delta;

                Adat_Jármű_Takarítás_Ütemező Ütemezés = (from a in AdatokÜtem
                                                         where a.Azonosító == pályaszám && a.Státus != 9 && a.Takarítási_fajta == fajta
                                                         orderby a.Azonosító, a.Takarítási_fajta
                                                         select a).FirstOrDefault();
                if (Ütemezés != null)
                {
                    switch (Ütemezés.Mérték.ToUpper().Trim())
                    {
                        case "NAP":
                            {
                                delta = Dátum.Value - Ütemezés.Dátum;
                                különbség = delta.TotalDays.ToÉrt_Int();
                                break;
                            }
                        case "HÓNAP":
                            {
                                delta = Dátum.Value - Ütemezés.Dátum;
                                különbség = delta.TotalDays.ToÉrt_Int();
                                break;
                            }
                    }
                    if (különbség % Ütemezés.Növekmény == 0)
                    {
                        // ha egyforma akkor kiírjuk
                        switch (Ütemezés.Takarítási_fajta.Trim())
                        {
                            case "J2":
                                {
                                    Tábla.Rows[sor].Cells[5].Value = "Terv";
                                    break;
                                }
                            case "J3":
                                {
                                    Tábla.Rows[sor].Cells[8].Value = "Terv";
                                    break;
                                }
                            case "J4":
                                {
                                    Tábla.Rows[sor].Cells[11].Value = "Terv";
                                    break;
                                }
                            case "J5":
                                {
                                    Tábla.Rows[sor].Cells[14].Value = "Terv";
                                    break;
                                }
                            case "J6":
                                {
                                    Tábla.Rows[sor].Cells[17].Value = "Terv";
                                    break;
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

        private void Vezénylés_listázása_napi(string pályaszám, int sor, string fajta)
        {
            try
            {
                Adat_Jármű_Takarítás_Vezénylés EgyVezénylés = (from a in AdatokVezény
                                                               where a.Azonosító == pályaszám
                                                               && a.Dátum.ToShortDateString() == Dátum.Value.ToShortDateString()
                                                               && a.Státus != 9
                                                               && a.Takarítási_fajta == fajta
                                                               orderby a.Azonosító
                                                               select a).FirstOrDefault();
                if (EgyVezénylés != null)
                {
                    switch (EgyVezénylés.Takarítási_fajta)
                    {
                        case "J2":
                            {
                                Tábla.Rows[sor].Cells[5].Value = "Igen";
                                break;
                            }
                        case "J3":
                            {
                                Tábla.Rows[sor].Cells[8].Value = "Igen";
                                break;
                            }
                        case "J4":
                            {
                                Tábla.Rows[sor].Cells[11].Value = "Igen";
                                break;
                            }
                        case "J5":
                            {
                                Tábla.Rows[sor].Cells[14].Value = "Igen";
                                break;
                            }
                        case "J6":
                            {
                                Tábla.Rows[sor].Cells[17].Value = "Igen";
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

        private void T5C5_ütemezés(string pályaszám, int sor)
        {
            try
            {
                Adat_Vezénylés Vezénylés = (from a in AdatokVez
                                            where a.Azonosító == pályaszám
                                            && a.Dátum >= DateTime.Today.AddDays(-1)
                                            && a.Törlés == 0
                                            orderby a.Azonosító
                                            select a).FirstOrDefault();

                if (Vezénylés != null)
                {
                    if (Vezénylés.Vizsgálatraütemez == 1)
                    {
                        // előző napi
                        if (Vezénylés.Dátum == Dátum.Value.AddDays(-1))
                        {
                            Tábla.Rows[sor].Cells[29].Value = Vezénylés.Vizsgálat.Trim() + "-" + Vezénylés.Dátum.ToString("MM.dd");
                        }
                        // aznapi
                        else if (Vezénylés.Dátum == Dátum.Value)
                        {
                            Tábla.Rows[sor].Cells[29].Value = Vezénylés.Vizsgálat.Trim() + "-" + Vezénylés.Dátum.ToString("MM.dd");
                        }
                        else
                        {
                            Tábla.Rows[sor].Cells[29].Value = Vezénylés.Vizsgálat.Trim() + "-" + Vezénylés.Dátum.ToString("MM.dd");
                        }
                    }
                }
                else
                {
                    Tábla.Rows[sor].Cells[29].Value = "-";
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

        private void CellaSzínezés()
        {
            try
            {
                for (int i = 0; i < Tábla.Rows.Count; i++)
                {
                    if (Tábla.Rows[i].Cells[2].Value != null)
                    {
                        switch (Tábla.Rows[i].Cells[23].Value.ToÉrt_Int())
                        {
                            case 4:
                                {
                                    // ha áll a kocsi
                                    Tábla.Rows[i].Cells[2].Style.BackColor = Color.Red;
                                    Tábla.Rows[i].Cells[2].Style.ForeColor = Color.White;
                                    Tábla.Rows[i].Cells[2].Style.Font = new Font("Arial Narrow", 11f, FontStyle.Italic);
                                    break;
                                }
                            case 3:
                                {
                                    // beálló
                                    Tábla.Rows[i].Cells[2].Style.BackColor = Color.Yellow;
                                    Tábla.Rows[i].Cells[2].Style.ForeColor = Color.Black;
                                    Tábla.Rows[i].Cells[2].Style.Font = new Font("Arial Narrow", 11f, FontStyle.Italic);
                                    break;
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

        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                SorIdx = e.RowIndex;
                Melyik_Ablak();
                Táblázatba_kattint(e.RowIndex);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Táblázatba_kattint(int sor)
        {
            try
            {
                if (sor < 0) return;
                // Bármelyik elemre kattintva kiírja a kocsi adatai;

                AblakTakSegéd1?.Kiírja_Kocsi(sor);
                AblakTakSegéd2?.Kiírja_Kocsi_Másik(sor);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ütemezés_lista_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Ütemezés_lista.SelectedIndex == -1) return;
                if (Ütemezés_lista.Items[Ütemezés_lista.SelectedIndex].ToStrTrim() == "") return;
                if (Ütemezés_lista.Items[Ütemezés_lista.SelectedIndex].ToString().Contains("J2")) return;
                if (Ütemezés_lista.Items[Ütemezés_lista.SelectedIndex].ToString().Contains("J3")) return;
                if (Ütemezés_lista.Items[Ütemezés_lista.SelectedIndex].ToString().Contains("J4")) return;
                if (Ütemezés_lista.Items[Ütemezés_lista.SelectedIndex].ToString().Contains("J5")) return;
                if (Ütemezés_lista.Items[Ütemezés_lista.SelectedIndex].ToString().Contains("J6")) return;

                for (int i = 0; i < Tábla.Rows.Count; i++)
                {

                    if (Ütemezés_lista.Items[Ütemezés_lista.SelectedIndex].ToString().Substring(0, 4) == Tábla.Rows[i].Cells[0].Value.ToStrTrim())
                    {
                        SorIdx = i;
                        Melyik_Ablak();
                        Táblázatba_kattint(i);
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

        public void Ütemezett_kocsik_részlet(string fajta)
        {
            try
            {
                long szerelvény = 0;
                string szöveg1 = "";

                List<Adat_Jármű_Takarítás_Vezénylés> EgyVezénylés = (from a in AdatokVezény
                                                                     where a.Státus != 9 && a.Dátum.ToShortDateString() == Dátum.Value.ToShortDateString() && a.Takarítási_fajta == fajta
                                                                     orderby a.Szerelvényszám, a.Azonosító ascending
                                                                     select a).ToList();


                foreach (Adat_Jármű_Takarítás_Vezénylés rekord in EgyVezénylés)
                {
                    if (szerelvény == 0) szerelvény = rekord.Szerelvényszám;

                    if (szerelvény == rekord.Szerelvényszám)
                    {
                        szöveg1 += rekord.Azonosító.Trim() + "-";
                    }
                    else
                    {
                        if (szöveg1.Trim() != "")
                        {
                            Ütemezés_lista.Items.Add(szöveg1);
                            szöveg1 = "";
                        }
                        szöveg1 = rekord.Azonosító.Trim() + "-";
                        szerelvény = rekord.Szerelvényszám;
                    }
                }
                if (szöveg1.ToStrTrim() != "")
                {
                    Ütemezés_lista.Items.Add(szöveg1);
                    szöveg1 = "";
                }

                Ütemezés_lista.Items.Add("");
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Ütemezettkocsik()
        {
            try
            {
                Ütemezés_lista.Items.Clear();
                if (Tábla.Columns.Count != 0) Tábla.Sort(Tábla.Columns[0], System.ComponentModel.ListSortDirection.Ascending);

                AdatokVezény = KézVezény.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);

                if (Tábla.RowCount != 0)
                {
                    foreach (string adat in Enum.GetNames(typeof(MyEn.Takfajtaadat)))
                    {
                        Ütemezés_lista.Items.Add(adat);
                        Ütemezett_kocsik_részlet(adat);
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

        private void Idegenben_Telephely_kiírása(string PályaSzám, int sor)
        {
            try
            {
                //    Idegen 
                if (AdatokIdegen != null)
                    foreach (Adat_Jármű_Vendég rekord in AdatokIdegen)
                    {
                        Adat_Jármű_Vendég EgyIdegen = (from a in AdatokIdegen
                                                       where a.Azonosító == PályaSzám
                                                       select a).FirstOrDefault();

                        Tábla.Rows[sor].Cells[30].Value = EgyIdegen?.KiadóTelephely;
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

        public void Ütem_tábla_Rögzítés(string SPSz, string SJtakfajta)
        {
            if (Tábla.Rows.Count < 1) return;

            for (int sor = 0; sor < Tábla.Rows.Count; sor++)
            {
                if (Tábla.Rows[sor].Cells[0].Value.ToStrTrim() == SPSz.Trim())
                {
                    switch (SJtakfajta)
                    {
                        case "J2":
                            {
                                Tábla.Rows[sor].Cells[5].Value = "Igen";
                                break;
                            }
                        case "J3":
                            {
                                Tábla.Rows[sor].Cells[8].Value = "Igen";
                                break;
                            }
                        case "J4":
                            {
                                Tábla.Rows[sor].Cells[11].Value = "Igen";
                                break;
                            }
                        case "J5":
                            {
                                Tábla.Rows[sor].Cells[14].Value = "Igen";
                                break;
                            }
                        case "J6":
                            {
                                Tábla.Rows[sor].Cells[17].Value = "Igen";
                                break;
                            }
                    }
                    break;
                }
            }
        }

        public void Ütem_Tábla_törlés(string SPSz, string SJtakfajta)
        {
            if (Tábla.Rows.Count < 1) return;

            for (int sor = 0; sor < Tábla.Rows.Count; sor++)
            {
                if (Tábla.Rows[sor].Cells[0].Value.ToStrTrim() == SPSz.Trim())
                {
                    switch (SJtakfajta)
                    {
                        case "J2":
                            {
                                Tábla.Rows[sor].Cells[5].Value = "";
                                break;
                            }
                        case "J3":
                            {
                                Tábla.Rows[sor].Cells[8].Value = "";
                                break;
                            }
                        case "J4":
                            {
                                Tábla.Rows[sor].Cells[11].Value = "";
                                break;
                            }
                        case "J5":
                            {
                                Tábla.Rows[sor].Cells[14].Value = "";
                                break;
                            }
                        case "J6":
                            {
                                Tábla.Rows[sor].Cells[17].Value = "";
                                break;
                            }
                    }
                    break;
                }
            }
        }
        #endregion


        #region Egyéb gombok
        private void BtnExcelkimenet_Click(object sender, EventArgs e)
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
                    FileName = $"T5C5_Nap_futás_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddhhmmss}",
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

        private void Dátum_ValueChanged(object sender, EventArgs e)
        {
            Ütemezettkocsik();
        }

        private void Btn_Vezénylésbeírás_Click(object sender, EventArgs e)
        {
            try
            {
                // ha nincs tábla tartalma
                if (Tábla.Rows.Count < 1) throw new HibásBevittAdat("A táblázat nincs megjelenítve.");
                Holtart.Be(Tábla.Rows.Count + 1);
                string szöveg1;
                int talált;
                string típusa;
                int hiba;
                string ideig_psz;
                int volt = 0;

                AdatokJárműHiba = KézJárműHiba.Lista_Adatok(Cmbtelephely.Text.Trim());
                AdatokJármű = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());

                for (int i = 0; i < Tábla.Rows.Count; i++)
                {
                    ideig_psz = Tábla.Rows[i].Cells[0].Value.ToStrTrim();

                    szöveg1 = "Mosó";
                    if (Tábla.Rows[i].Cells[5].Value != null && Tábla.Rows[i].Cells[5].Value.ToStrTrim() == "Igen")
                    {
                        szöveg1 += "-J_2";
                        volt = 1;
                    }
                    if (Tábla.Rows[i].Cells[8].Value != null && Tábla.Rows[i].Cells[8].Value.ToStrTrim() == "Igen")
                    {
                        szöveg1 += "-J_3";
                        volt = 1;
                    }
                    if (Tábla.Rows[i].Cells[11].Value != null && Tábla.Rows[i].Cells[11].Value.ToStrTrim() == "Igen")
                    {
                        szöveg1 += "-J_4";
                        volt = 1;
                    }
                    if (Tábla.Rows[i].Cells[14].Value != null && Tábla.Rows[i].Cells[14].Value.ToStrTrim() == "Igen")
                    {
                        szöveg1 += "-J_5";
                        volt = 1;
                    }
                    if (Tábla.Rows[i].Cells[17].Value != null && Tábla.Rows[i].Cells[17].Value.ToStrTrim() == "Igen")
                    {
                        szöveg1 += "-J_6";
                        volt = 1;
                    }
                    szöveg1 += "-" + Dátum.Value.ToString("yyyy.MM.dd");
                    if (volt == 1)
                    {
                        // Megnézzük, hogy volt-e már rögzítve ilyen szöveg
                        talált = 0;
                        Adat_Jármű_hiba AdatJárműHiba = (from a in AdatokJárműHiba
                                                         where a.Azonosító == ideig_psz
                                                         && a.Hibaleírása.Contains(szöveg1.Trim())
                                                         select a).FirstOrDefault();

                        if (AdatJárműHiba != null) talált = 1;
                        // ha már volt ilyen szöveg rögzítve a pályaszámhoz akkor nem rögzítjük mégegyszer
                        if (talált == 0)
                        {
                            // hibák számát emeljük és státus állítjuk ha kell
                            Adat_Jármű AdatJármű = (from a in AdatokJármű
                                                    where a.Azonosító == ideig_psz
                                                    select a).FirstOrDefault();

                            if (AdatJármű != null)
                            {
                                if (!int.TryParse(AdatJármű?.Hibák.ToString(), out int hibáksorszáma)) hibáksorszáma = 0;

                                hiba = hibáksorszáma++;
                                típusa = AdatJármű.Típus ?? "";
                                if (!int.TryParse(AdatJármű.Státus.ToString(), out int státus)) státus = 0;


                                if (státus < 3)
                                    státus = 3; // ha 3,4 státusa akkor nem kell módosítani.

                                // rögzítjük a villamos.mdb-be
                                Adat_Jármű ADATJármű = new Adat_Jármű(ideig_psz.Trim(), hiba, státus);
                                KézJármű.Módosítás_Hiba_Státus(Cmbtelephely.Text.Trim(), ADATJármű);

                                // beírjuk a hibákat
                                Adat_Jármű_hiba ADATHiba = new Adat_Jármű_hiba(
                                                        Program.PostásNév.Trim(),
                                                        3,
                                                        szöveg1.Trim(),
                                                        DateTime.Now,
                                                        false,
                                                        típusa.Trim(),
                                                        ideig_psz.Trim(),
                                                        hibáksorszáma);
                                KézJárműHiba.Rögzítés(Cmbtelephely.Text.Trim(), ADATHiba);

                                // naplózzuk a hibákat
                                KézJárműHibaNapló.Rögzítés(Cmbtelephely.Text.Trim(), DateTime.Today, ADATHiba);
                            }
                        }
                    }
                    volt = 0;
                }

                Holtart.Ki();

                MessageBox.Show("Az adatok rögzítése befejeződött!", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
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


        #region Keresés
        void Keresés_metódus()
        {
            try
            {
                if (Új_Ablak_Kereső == null)
                {
                    Új_Ablak_Kereső = new Ablak_Kereső();
                    Új_Ablak_Kereső.FormClosed += Új_Ablak_Kereső_Closed;
                    Új_Ablak_Kereső.Top = 50;
                    Új_Ablak_Kereső.Left = 50;
                    Új_Ablak_Kereső.Show();
                    Új_Ablak_Kereső.Ismétlődő_Változás += Szövegkeresés;
                }
                else
                {
                    Új_Ablak_Kereső.Activate();
                    Új_Ablak_Kereső.WindowState = FormWindowState.Normal;
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

        private void Szövegkeresés()
        {
            // megkeressük a szöveget a táblázatban
            if (Új_Ablak_Kereső.Keresendő == null) return;
            if (Új_Ablak_Kereső.Keresendő.Trim() == "") return;
            if (Tábla.Rows.Count < 0) return;

            for (int i = 0; i < Tábla.Rows.Count; i++)
            {
                if (Tábla.Rows[i].Cells[0].Value.ToStrTrim() == Új_Ablak_Kereső.Keresendő.Trim())
                {
                    Tábla.Rows[i].Cells[0].Style.BackColor = Color.Orange;
                    Tábla.FirstDisplayedScrollingRowIndex = i;
                    Tábla.CurrentCell = Tábla.Rows[i].Cells[0];
                    return;
                }
            }
        }

        private void ESC_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC
            if ((int)e.KeyCode == 27)
            {
                Új_Ablak_Kereső?.Close();
                SegédAblak_bezárás();
            }

            //Ctrl+F
            if (e.Control && e.KeyCode == Keys.F)
            {
                Keresés_metódus();
            }
        }

        private void Kereső_hívó_Click(object sender, EventArgs e)
        {
            Keresés_metódus();
        }
        #endregion


        #region Jármű takarítások
        private void JK_kat_feltöltés()
        {
            JK_Kategória.Items.Clear();
            foreach (string adat in Enum.GetNames(typeof(MyEn.Takfajtaadat)))
                JK_Kategória.Items.Add(adat);
        }

        private void JK_törlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (JK_Azonosító.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve egy elem sem.");
                if (JK_List.SelectedItems.Count < 1) throw new HibásBevittAdat("Nincs kijelölve egy elem sem.");

                int napszak = 2;

                for (int sorszám = 0; sorszám < JK_List.SelectedItems.Count; sorszám++)
                {
                    JK_Azonosító.Text = JK_List.SelectedItems[sorszám].ToString();

                    if (Jnappal.Checked) napszak = 1; else napszak = 2;
                    AdatokTelj = KézTakarításTelj.Lista_Adatok(Cmbtelephely.Text.Trim(), JDátum.Value.Year);

                    Adat_Jármű_Takarítás_Teljesítés AdatTelj = (from a in AdatokTelj
                                                                where a.Dátum == JDátum.Value
                                                                && a.Napszak == napszak
                                                                && a.Azonosító == JK_Azonosító.Text.Trim()
                                                                && a.Takarítási_fajta == JK_Kategória.Text.Trim()
                                                                select a).FirstOrDefault();

                    if (AdatTelj != null)
                    {
                        Adat_Jármű_Takarítás_Teljesítés ADAT = new Adat_Jármű_Takarítás_Teljesítés(
                                                            AdatTelj.Azonosító,
                                                            AdatTelj.Takarítási_fajta,
                                                            AdatTelj.Dátum,
                                                            0, 3, 0, false,
                                                            napszak,
                                                            AdatTelj.Mérték,
                                                            AdatTelj.Típus);
                        KézTakarításTelj.Módosítás(Cmbtelephely.Text.Trim(), JDátum.Value.Year, ADAT);
                        Átrögzít(AdatTelj.Takarítási_fajta, 1);
                        MessageBox.Show("Az adatok rögzítése megtörtént !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                JK_Azonosító.Text = "";
                JK_Azonosító.Focus();
                JK_Törölt.Checked = true;
                Takarítottkocsik();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void JK_Kategória_Click(object sender, EventArgs e)
        {
            Gombok();
            AcceptButton = JK_Mentés;
            JK_Mentés.BackColor = Color.LimeGreen;
            GroupBox4.BackColor = Color.SaddleBrown;
        }

        private void JK_Mentés_Click(object sender, EventArgs e)
        {
            try
            {
                if (JK_Kategória.Text.ToStrTrim() == "") return;
                if (JK_Azonosító.Text.ToStrTrim() == "") return;

                List<Adat_Jármű> AdatokFőJármű = KézJármű.Lista_Adatok("Főmérnökség");
                Adat_Jármű AdatJármű = (from a in AdatokFőJármű
                                        where a.Azonosító == JK_Azonosító.Text.Trim()
                                        select a).FirstOrDefault();

                string típus;
                if (AdatJármű != null) típus = AdatJármű.Típus.Trim().Replace("\0", "");
                else
                    throw new HibásBevittAdat($"Nincs:{JK_Azonosító.Text.Trim()} ilyen pályaszám a nyilvántartásban !");

                if (típus.Trim() == "_" || típus.Trim() == "") throw new HibásBevittAdat($"Nincs a {JK_Azonosító.Text.Trim()} pályaszámú járműnek a nyilvántartásban a típus beállítva !");

                int napszak = 2;
                if (Jnappal.Checked) napszak = 1;

                AdatokTelj = KézTakarításTelj.Lista_Adatok(Cmbtelephely.Text.Trim(), JDátum.Value.Year);
                Adat_Jármű_Takarítás_Teljesítés AdatTelj = (from a in AdatokTelj
                                                            where a.Dátum == JDátum.Value
                                                            && a.Napszak == napszak
                                                            && a.Azonosító == JK_Azonosító.Text.Trim()
                                                            && a.Takarítási_fajta == JK_Kategória.Text.Trim()
                                                            select a).FirstOrDefault();
                int Megfelelt1 = 0;
                int Státus = 0;
                int Megfelelt2 = 0;
                bool Pótdátum = false;
                if (JK_Törölt.Checked)
                {
                    Megfelelt1 = 0;
                    Státus = 3;
                    Megfelelt2 = 0;
                    Pótdátum = false;
                }
                else if (JK_Megfelel1.Checked)
                {
                    Megfelelt1 = 1;
                    Státus = 1;
                    Megfelelt2 = 0;
                    Pótdátum = false;
                }
                else if (JK_Nem1.Checked && !JK_pót.Checked)
                {
                    Megfelelt1 = 2;
                    Státus = 2;
                    Megfelelt2 = 0;
                    Pótdátum = false;
                }
                else if (JK_Nem1.Checked && JK_pót.Checked && JK_megfelel2.Checked)
                {
                    Megfelelt1 = 2;
                    Státus = 1;
                    Megfelelt2 = 1;
                    Pótdátum = true;
                }
                else if (JK_Nem1.Checked && JK_pót.Checked && JK_Nem2.Checked)
                {
                    Megfelelt1 = 2;
                    Státus = 2;
                    Megfelelt2 = 2;
                    Pótdátum = true;
                }
                Adat_Jármű_Takarítás_Teljesítés ADAT = new Adat_Jármű_Takarítás_Teljesítés(
                                                JK_Azonosító.Text.Trim(),
                                                JK_Kategória.Text.Trim(),
                                                JDátum.Value,
                                                Megfelelt1,
                                                Státus,
                                                Megfelelt2,
                                                Pótdátum,
                                                napszak,
                                                0,
                                                típus.Trim());

                if (AdatTelj != null)
                    KézTakarításTelj.Módosítás(Cmbtelephely.Text.Trim(), JDátum.Value.Year, ADAT);
                else
                    KézTakarításTelj.Rögzítés(Cmbtelephely.Text.Trim(), JDátum.Value.Year, ADAT);



                if (JK_Megfelel1.Checked || JK_megfelel2.Checked) Takarításátrögzítés();
                MessageBox.Show("Az adatok rögzítése megtörtént !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                JK_Azonosító.Text = "";
                JK_Azonosító.Focus();

                Takarítottkocsik();

                AcceptButton = JK_Mentés;
                JK_Mentés.BackColor = Color.LimeGreen;
                GroupBox4.BackColor = Color.SaddleBrown;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Takarításátrögzítés()
        {
            try
            {
                if (JK_Azonosító.Text.Trim() == "") return;
                AdatokJármű = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adat_Jármű AdatJármű = (from a in AdatokJármű
                                        where a.Azonosító == JK_Azonosító.Text.Trim()
                                        select a).FirstOrDefault();

                if (AdatJármű == null) MessageBox.Show("A telephelyen nincs ilyen jármű!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                Átrögzít(JK_Kategória.Text.Trim());
                AdatokMátrix = KézMátrix.Lista_Adat();
                List<Adat_Jármű_Takarítás_Mátrix> AdatokMátrix1 = (from a in AdatokMátrix
                                                                   where a.Igazság == true
                                                                   && a.Fajta == JK_Kategória.Text.Trim()
                                                                   select a).ToList();

                if (AdatokMátrix1.Count != 0)
                {
                    foreach (Adat_Jármű_Takarítás_Mátrix Madat in AdatokMátrix1)
                    {
                        Átrögzít(Madat.Fajtamásik.Trim());
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

        private void Átrögzít(string Tak_fajta, int Státus = 0)
        {
            try
            {
                // beírjuk a kocskat a gyűjtő táblába is és naplózzuk.
                AdatokTak = KézTak.Lista_Adatok();
                Adat_Jármű_Takarítás_Takarítások AdatTak = (from a in AdatokTak
                                                            where a.Azonosító == JK_Azonosító.Text.Trim()
                                                            && a.Takarítási_fajta == Tak_fajta.Trim()
                                                            && a.Telephely == Cmbtelephely.Text.Trim()
                                                            select a).FirstOrDefault();
                Adat_Jármű_Takarítás_Takarítások ADAT = new Adat_Jármű_Takarítás_Takarítások(
                                                JK_Azonosító.Text.Trim(),
                                                JDátum.Value,
                                                Tak_fajta.Trim(),
                                                Cmbtelephely.Text.Trim(),
                                                Státus);

                if (AdatTak != null)
                    KézTak.Módosítás_Dátum(ADAT);
                else
                    KézTak.Rögzítés(ADAT);

                // naplózás
                Adat_Jármű_Takarítás_Napló ADATN = new Adat_Jármű_Takarítás_Napló(
                                            ADAT.Azonosító,
                                            ADAT.Dátum,
                                            ADAT.Takarítási_fajta,
                                            ADAT.Telephely,
                                            DateTime.Now,
                                            Program.PostásNév,
                                            Státus);
                KézTakNapló.Rögzítés(DateTime.Today.Year, ADATN);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Takarítottkocsik()
        {
            try
            {
                if (JK_Kategória.Text.ToStrTrim() == "") return;
                List<Adat_Jármű_Takarítás_Teljesítés> Adatok = KézTakarításTelj.Lista_Adatok(Cmbtelephely.Text.Trim(), JDátum.Value.Year);
                Adatok = Adatok.Where(a => a.Dátum.ToShortDateString() == JDátum.Value.ToShortDateString()).ToList();

                int napszak = 2;
                if (Jnappal.Checked) napszak = 1;

                int státus = 0;
                if (JK_Megfelel1.Checked)
                    státus = 1;
                else if (JK_Nem1.Checked)
                    státus = 2;
                else
                    státus = 3;

                Adatok = (from a in Adatok
                          where a.Napszak == napszak
                          && a.Státus == státus
                          && a.Takarítási_fajta == JK_Kategória.Text.Trim()
                          select a).ToList();

                JK_List.Items.Clear();
                foreach (Adat_Jármű_Takarítás_Teljesítés Elem in Adatok)
                    JK_List.Items.Add(Elem.Azonosító);
                JK_List.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void J_takarított(string psz)
        {
            try
            {
                AdatokTelj = KézTakarításTelj.Lista_Adatok(Cmbtelephely.Text.Trim(), JDátum.Value.Year);
                foreach (Adat_Jármű_Takarítás_Teljesítés rekord in AdatokTelj)
                {
                    int napsz = 2;
                    if (Jnappal.Checked) napsz = 1;

                    Adat_Jármű_Takarítás_Teljesítés Teljesítés = (from a in AdatokTelj
                                                                  where a.Azonosító == psz
                                                                  && a.Dátum == JDátum.Value
                                                                  && a.Napszak == napsz
                                                                  && a.Takarítási_fajta == JK_Kategória.Text.Trim()
                                                                  orderby a.Azonosító
                                                                  select a).FirstOrDefault();

                    if (Teljesítés == null) return;

                    JK_pót.Checked = Teljesítés.Pótdátum.ToÉrt_Bool();
                    JK_Megfelel1.Checked = Teljesítés.Megfelelt1 == 1;
                    JK_Nem1.Checked = Teljesítés.Megfelelt1 == 2;
                    JK_megfelel2.Checked = Teljesítés.Megfelelt2 == 1;
                    if (Teljesítés.Státus == 3) JK_Törölt.Checked = true;
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

        private void JK_List_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (JK_List.SelectedItem == null) return;

            JK_Azonosító.Text = JK_List.SelectedItem.ToString();
            J_takarított(JK_Azonosító.Text.Trim());
        }

        private void J2_Megfelel_Click(object sender, EventArgs e)
        {
            Gombok();
            AcceptButton = JK_Mentés;
            JK_Mentés.BackColor = Color.LimeGreen;
            GroupBox4.BackColor = Color.SaddleBrown;
            JK_pót.Checked = false;
            Takarítottkocsik();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Takarítottkocsik();
        }

        private void JK_Kategória_SelectedIndexChanged(object sender, EventArgs e)
        {
            Takarítottkocsik();
        }

        private void JK_Nem1_Click(object sender, EventArgs e)
        {
            Gombok();
            AcceptButton = JK_Mentés;
            JK_Mentés.BackColor = Color.LimeGreen;
            GroupBox4.BackColor = Color.SaddleBrown;
            JK_Nem2.Checked = true;
            Takarítottkocsik();
        }
        #endregion


        #region J1 rögzítés
        private void J1Mentés_Click(object sender, EventArgs e)
        {
            try
            {
                if (J1Megfelelő.Text.ToStrTrim() == "") J1Megfelelő.Text = 0.ToString();
                if (J1NemMegfelelő.Text.ToStrTrim() == "") J1NemMegfelelő.Text = 0.ToString();
                if (J1Típus.Text.ToStrTrim() == "") return;
                if (!int.TryParse(J1Megfelelő.Text, out int J1Meg)) return;
                if (!int.TryParse(J1NemMegfelelő.Text, out int J1Nem)) return;

                int napszak = 2;
                if (Jnappal.Checked) napszak = 1;

                AdatokJ1 = KézJ1.Lista_Adat(Cmbtelephely.Text.Trim(), JDátum.Value.Year);
                Adat_Jármű_Takarítás_J1 AdatJ1 = (from a in AdatokJ1
                                                  where a.Dátum == JDátum.Value
                                                  && a.Napszak == napszak
                                                  && a.Típus == J1Típus.Text.Trim()
                                                  select a).FirstOrDefault();

                Adat_Jármű_Takarítás_J1 ADAT = new Adat_Jármű_Takarítás_J1(
                                       JDátum.Value,
                                       J1Meg,
                                       J1Nem, napszak,
                                       J1Típus.Text.Trim());

                if (AdatJ1 != null)
                    KézJ1.Módosítás(Cmbtelephely.Text.Trim(), JDátum.Value.Year, ADAT);
                else
                    KézJ1.Rögzítés(Cmbtelephely.Text.Trim(), JDátum.Value.Year, ADAT);

                J1Megfelelő.Text = "";
                J1Megfelelő.Focus();
                J1NemMegfelelő.Text = "";
                MessageBox.Show("Az adatok rögzítése megtörtént !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);

                AcceptButton = J1Mentés;
                J1Mentés.BackColor = Color.LimeGreen;
                GroupBox1.BackColor = Color.SaddleBrown;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void J1Típus_Click(object sender, EventArgs e)
        {
            Gombok();
            AcceptButton = J1Mentés;
            J1Mentés.BackColor = Color.LimeGreen;
            GroupBox1.BackColor = Color.SaddleBrown;
        }

        private void Típusfeltöltés()
        {
            try
            {
                List<Adat_Jármű_Állomány_Típus> Adatok = KézJárműTípus.Lista_Adatok(Cmbtelephely.Text.Trim());
                List<string> Típusok = Adatok.OrderBy(a => a.Id).Select(a => a.Típus).Distinct().ToList();

                J1Típus.Items.Clear();
                J1Típus.Items.Add("");
                foreach (string Elem in Típusok)
                    J1Típus.Items.Add(Elem);

                J1Típus.Refresh();
            }
            catch (HibásBevittAdat ex)
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


        #region Létszám
        private void Lét_Előírt_Click(object sender, EventArgs e)
        {
            Gombok();
            AcceptButton = LétszámMentés;
            LétszámMentés.BackColor = Color.LimeGreen;
            GroupBox2.BackColor = Color.SaddleBrown;
        }

        private void LétszámMentés_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Lét_Előírt.Text, out int Előírt)) Lét_Előírt.Text = 0.ToString();
                if (!int.TryParse(Lét_Megjelent.Text, out int Megjelent)) Lét_Megjelent.Text = 0.ToString();
                if (!int.TryParse(Lét_Viselt.Text, out int Ruhátlan)) Lét_Viselt.Text = 0.ToString();

                int napszak = 2;
                if (Jnappal.Checked) napszak = 1;

                List<Adat_Jármű_Takarítás_Létszám> AdatokLétsz = KézLétsz.Lista_Adat(Cmbtelephely.Text.Trim(), JDátum.Value.Year);

                Adat_Jármű_Takarítás_Létszám AdatLétszám = (from a in AdatokLétsz
                                                            where a.Dátum == JDátum.Value
                                                            && a.Napszak == napszak
                                                            select a).FirstOrDefault();

                Adat_Jármű_Takarítás_Létszám ADAT = new Adat_Jármű_Takarítás_Létszám(
                                                JDátum.Value,
                                                Előírt,
                                                Megjelent,
                                                napszak,
                                                Ruhátlan);
                if (AdatLétszám != null)
                    KézLétsz.Módosítás(Cmbtelephely.Text.Trim(), JDátum.Value.Year, ADAT);
                else
                    KézLétsz.Rögzítés(Cmbtelephely.Text.Trim(), JDátum.Value.Year, ADAT);

                Lét_Előírt.Text = "";
                Lét_Megjelent.Text = "";
                Lét_Viselt.Text = "";

                AcceptButton = LétszámMentés;
                LétszámMentés.BackColor = Color.LimeGreen;
                GroupBox2.BackColor = Color.SaddleBrown;
                MessageBox.Show("Az adatok rögzítése megtörtént !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
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


        #region OPciók
        private void Opció_lista_Click(object sender, EventArgs e)
        {
            Gombok();
            AcceptButton = Opció_mentés;
            Opció_mentés.BackColor = Color.LimeGreen;
            GroupBox3.BackColor = Color.SaddleBrown;
        }

        private void Opció_lista_SelectedIndexChanged(object sender, EventArgs e)
        {
            OpciósKocsik();
        }

        private void Opció_Töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (Opció_lista.Text.Trim() == "") return;
                if (Opció_psz.Text.Trim() == "") return;
                int napszak = 2;
                if (Jnappal.Checked) napszak = 1;

                AdatokTelj = KézTakarításTelj.Lista_Adatok(Cmbtelephely.Text.Trim(), JDátum.Value.Year);
                Adat_Jármű_Takarítás_Teljesítés AdatTakTelj = (from a in AdatokTelj
                                                               where a.Dátum == JDátum.Value
                                                               && a.Napszak == napszak
                                                               && a.Státus == 1
                                                               && a.Takarítási_fajta == Opció_lista.Text.Trim()
                                                               && a.Azonosító == Opció_psz.Text.Trim()
                                                               select a).FirstOrDefault();

                if (AdatTakTelj != null)
                {
                    Adat_Jármű_Takarítás_Teljesítés ADAT = new Adat_Jármű_Takarítás_Teljesítés(
                                       Opció_psz.Text.Trim(),
                                       Opció_lista.Text.Trim(),
                                       JDátum.Value,
                                       0, 3, 0, false,
                                       napszak,
                                       AdatTakTelj.Mérték,
                                       AdatTakTelj.Típus);
                    KézTakarításTelj.Módosítás(Cmbtelephely.Text.Trim(), JDátum.Value.Year, ADAT);
                    MessageBox.Show("Az adatok törlése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                Opció_psz.Text = "";
                Opció_terület.Text = "";
                OpciósKocsik();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpciósKocsik()
        {
            try
            {
                if (Opció_lista.Text.ToStrTrim() == "") return;

                AdatokTelj = KézTakarításTelj.Lista_Adatok(Cmbtelephely.Text.Trim(), JDátum.Value.Year);

                Opció_tábla.Rows.Clear();
                Opció_tábla.Columns.Clear();
                Opció_tábla.Refresh();
                Opció_tábla.Visible = false;
                Opció_tábla.ColumnCount = 2;

                Opció_tábla.Columns[0].HeaderText = "Pályaszám";
                Opció_tábla.Columns[0].Width = 120;
                Opció_tábla.Columns[1].HeaderText = "[m2]";
                Opció_tábla.Columns[1].Width = 110;


                int i = 0;
                int napsz = 1;
                if (!Jnappal.Checked) napsz = 2;

                List<Adat_Jármű_Takarítás_Teljesítés> Teljesítés = (from a in AdatokTelj
                                                                    where a.Dátum.ToShortDateString() == JDátum.Value.ToShortDateString()
                                                                    && a.Napszak == napsz
                                                                    && a.Státus == 1
                                                                    && a.Takarítási_fajta == Opció_lista.Text.Trim()
                                                                    select a).ToList();

                foreach (Adat_Jármű_Takarítás_Teljesítés rekord in Teljesítés)
                {
                    Opció_tábla.RowCount++;
                    i = Opció_tábla.RowCount - 1;
                    Opció_tábla.Rows[i].Cells[0].Value = rekord.Azonosító.Trim();
                    Opció_tábla.Rows[i].Cells[1].Value = rekord.Mérték.ToString();
                }

                Opció_tábla.Visible = true;
                Opció_tábla.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            OpciósKocsik();
        }

        private void Opció_mentés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Opció_lista.Text.ToStrTrim() == "") return;
                if (Opció_psz.Text.ToStrTrim() == "") return;
                if (Opció_terület.Text.ToStrTrim() == "") return;
                if (double.TryParse(Opció_terület.Text, out double Mérték) == false) return;

                string típus;
                List<Adat_Jármű> AdatokFőJármű = KézJármű.Lista_Adatok("Főmérnökség");
                Adat_Jármű AdatJármű = (from a in AdatokFőJármű
                                        where a.Azonosító == Opció_psz.Text.Trim()
                                        select a).FirstOrDefault();

                if (AdatJármű != null)
                    típus = AdatJármű.Típus;
                else
                {
                    MessageBox.Show("Nincs ilyen pályaszám a nyilvántartásban !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Rögzítjük a teljesítés táblába
                int napszak = 2;
                if (Jnappal.Checked) napszak = 1;

                AdatokTelj = KézTakarításTelj.Lista_Adatok(Cmbtelephely.Text.Trim(), JDátum.Value.Year);
                Adat_Jármű_Takarítás_Teljesítés AdatTeljes = (from a in AdatokTelj
                                                              where a.Dátum == JDátum.Value
                                                              && a.Napszak == napszak
                                                              && a.Azonosító == Opció_psz.Text.Trim()
                                                              && a.Státus == 1
                                                              && a.Takarítási_fajta == Opció_lista.Text.Trim()
                                                              select a).FirstOrDefault();

                Adat_Jármű_Takarítás_Teljesítés ADAT = new Adat_Jármű_Takarítás_Teljesítés(
                                                       Opció_psz.Text.Trim(),
                                                       Opció_lista.Text.Trim(),
                                                       JDátum.Value,
                                                       0, 1, 0, false,
                                                       napszak,
                                                       Mérték,
                                                       típus.Trim());
                if (AdatTeljes != null)
                    KézTakarításTelj.Módosítás(Cmbtelephely.Text.Trim(), JDátum.Value.Year, ADAT);
                else
                    KézTakarításTelj.Rögzítés(Cmbtelephely.Text.Trim(), JDátum.Value.Year, ADAT);

                if (JK_Megfelel1.Checked == true | JK_megfelel2.Checked == true) Takarításátrögzítés();
                MessageBox.Show("Az adatok rögzítése megtörtént !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Opció_psz.Text = "";
                Opció_terület.Text = "";
                Opció_psz.Focus();

                OpciósKocsik();

                AcceptButton = Opció_mentés;
                Opció_mentés.BackColor = Color.LimeGreen;
                GroupBox3.BackColor = Color.SaddleBrown;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Opció_lista_feltöltés()
        {
            Opció_lista.Items.Clear();
            Opció_lista.Items.Add("Graffiti");
            Opció_lista.Items.Add("Eseti");
            Opció_lista.Items.Add("Fertőtlenítés");
        }

        private void Opció_tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Opció_psz.Text = Opció_tábla.Rows[e.RowIndex].Cells[0].Value.ToString();
            Opció_terület.Text = Opció_tábla.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void Jnappal_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Takarítottkocsik();
                OpciósKocsik();
            }
            catch (HibásBevittAdat ex)
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


        #region Alapadatok Takarítások
        private void Előzmény_lista_takarítás()
        {
            try
            {
                AdatokTak.Clear();
                List<Adat_Jármű_Takarítás_Takarítások> AdatokTakÖ = KézTak.Lista_Adatok();

                if (Utolsó_pályaszám.Text.Trim() == "" && Utolsó_telephely.Text.Trim() == "")
                    AdatokTakÖ = (from a in AdatokTakÖ
                                  orderby a.Azonosító, a.Takarítási_fajta, a.Dátum descending
                                  select a).ToList();
                else if (Utolsó_pályaszám.Text.Trim() != "" && Utolsó_telephely.Text.Trim() == "")
                    AdatokTakÖ = (from a in AdatokTakÖ
                                  where a.Azonosító == Utolsó_pályaszám.Text.Trim()
                                  orderby a.Takarítási_fajta ascending, a.Dátum descending
                                  select a).ToList();
                else if (Utolsó_pályaszám.Text.Trim() == "" && Utolsó_telephely.Text.Trim() != "")
                    AdatokTakÖ = (from a in AdatokTakÖ
                                  where a.Telephely == Utolsó_telephely.Text.Trim()
                                  orderby a.Takarítási_fajta ascending, a.Dátum descending
                                  select a).ToList();
                else
                    AdatokTakÖ = (from a in AdatokTakÖ
                                  where a.Telephely == Utolsó_telephely.Text.Trim()
                                  && a.Azonosító == Utolsó_pályaszám.Text.Trim()
                                  orderby a.Takarítási_fajta ascending, a.Dátum descending
                                  select a).ToList();

                AdatokTak = AdatokTakÖ;
                Tábla_utolsó.Visible = false;
                Tábla_utolsó.CleanFilterAndSort();
                Tábla_utolsó_Fejléc();
                Tábla_utolsó_Tartalom();
                Tábla_utolsó.DataSource = AdatTábla_Utolsó;
                Tábla_utolsó_OszlopSzél();
                Tábla_utolsó.Visible = true;
                Tábla_utolsó.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Tábla_utolsó_OszlopSzél()
        {
            Tábla_utolsó.Columns["Azonosító"].Width = 100;
            Tábla_utolsó.Columns["Dátum"].Width = 100;
            Tábla_utolsó.Columns["Takarítási fajta"].Width = 100;
            Tábla_utolsó.Columns["Telephely"].Width = 150;
            Tábla_utolsó.Columns["Státus"].Width = 100;
        }

        private void Tábla_utolsó_Tartalom()
        {
            AdatTábla_Utolsó.Clear();
            foreach (Adat_Jármű_Takarítás_Takarítások rekord in AdatokTak)
            {
                DataRow Soradat = AdatTábla_Utolsó.NewRow();
                Soradat["Azonosító"] = rekord.Azonosító.Trim();
                Soradat["Dátum"] = rekord.Dátum.ToString("yyyy.MM.dd");
                Soradat["Takarítási fajta"] = rekord.Takarítási_fajta.Trim();
                Soradat["Telephely"] = rekord.Telephely.Trim();
                Soradat["Státus"] = rekord.Státus;

                AdatTábla_Utolsó.Rows.Add(Soradat);
            }
        }

        private void Tábla_utolsó_Fejléc()
        {
            AdatTábla_Utolsó.Columns.Clear();
            AdatTábla_Utolsó.Columns.Add("Azonosító");
            AdatTábla_Utolsó.Columns.Add("Dátum");
            AdatTábla_Utolsó.Columns.Add("Takarítási fajta");
            AdatTábla_Utolsó.Columns.Add("Telephely");
            AdatTábla_Utolsó.Columns.Add("Státus");
        }

        private void Utolsó_frissít_Click(object sender, EventArgs e)
        {
            Előzmény_lista_takarítás();
            TáblaUtolsóCella();
        }

        private void Utolsó_Telephelyek_feltöltése()
        {
            Utolsó_telephely.Items.Clear();
            if (Cmbtelephely.Enabled == false)
            {
                // ha csak egy telephely
                Utolsó_telephely.Items.Add(Cmbtelephely.Text.Trim());
            }
            else
            {

                for (int i = 0; i < Cmbtelephely.Items.Count - 1; i++)
                    Utolsó_telephely.Items.Add(Cmbtelephely.Items[i].ToStrTrim());
            }
        }

        private void Utolsó_pályaszám_feltöltés()
        {
            List<Adat_Jármű_Takarítás_Takarítások> AdatokÖ = KézTak.Lista_Adatok().OrderBy(a => a.Azonosító).ToList();
            if (Cmbtelephely.Text.Trim() != "Főmérnökség" || Program.Postás_Vezér)
                AdatokÖ = AdatokÖ.Where(a => a.Telephely == Cmbtelephely.Text.Trim() && a.Státus == 0).ToList();

            List<string> Adatok = AdatokÖ.Select(a => a.Azonosító).Distinct().ToList();
            Utolsó_pályaszám.Items.Clear();
            foreach (string Elem in Adatok)
                Utolsó_pályaszám.Items.Add(Elem);
            Utolsó_pályaszám.Refresh();
        }

        private void Utolsó_takfajta_feltöltés()
        {
            Utolsó_takarítási_fajta.Items.Clear();
            Utolsó_takarítási_fajta.Items.Add("");
            foreach (MyEn.Takfajtaadat elem in Enum.GetValues(typeof(MyEn.Takfajtaadat)))
                Utolsó_takarítási_fajta.Items.Add($"{elem.ToString().Replace('_', ' ')}");
        }

        private void Tábla_utolsó_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                int i = e.RowIndex;

                Utolsó_pályaszám.Text = Tábla_utolsó.Rows[i].Cells[0].Value.ToStrTrim();
                Utolsó_dátum.Value = Tábla_utolsó.Rows[i].Cells[1].Value.ToÉrt_DaTeTime();
                Utolsó_takarítási_fajta.Text = Tábla_utolsó.Rows[i].Cells[2].Value.ToStrTrim();
                Utolsó_telephely.Text = Tábla_utolsó.Rows[i].Cells[3].Value.ToStrTrim();
                Utolsó_státus.Checked = Tábla_utolsó.Rows[i].Cells[4].Value.ToÉrt_Bool();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Utolsó_történet_Click(object sender, EventArgs e)
        {
            try
            {
                List<Adat_Jármű_Takarítás_Napló> AdatokNapl = KézTakNapló.Lista_Adatok(Utolsó_dátum.Value.Year - 1);
                List<Adat_Jármű_Takarítás_Napló> Ideig = KézTakNapló.Lista_Adatok(Utolsó_dátum.Value.Year);
                AdatokNapl.AddRange(Ideig);
                AdatokNapl = AdatokNapl.OrderByDescending(a => a.Dátum).ToList();

                List<Adat_Jármű_Takarítás_Napló> AdatNap;

                if (Utolsó_pályaszám.Text.Trim() == "")
                {
                    AdatNap = (from a in AdatokNapl
                               orderby a.Azonosító, a.Takarítási_fajta, a.Dátum
                               select a).ToList();
                }
                else
                {
                    AdatNap = (from a in AdatokNapl
                               where a.Azonosító == Utolsó_pályaszám.Text.Trim()
                               orderby a.Takarítási_fajta, a.Dátum
                               select a).ToList();
                }
                if (Utolsó_telephely.Text.Trim() != "")
                    AdatNap = (from a in AdatNap
                               where a.Telephely == Utolsó_telephely.Text.Trim()
                               orderby a.Takarítási_fajta, a.Dátum
                               select a).ToList();
                if (Utolsó_takarítási_fajta.Text.Trim() != "")
                    AdatNap = (from a in AdatNap
                               where a.Takarítási_fajta == Utolsó_takarítási_fajta.Text.Trim()
                               orderby a.Takarítási_fajta, a.Dátum
                               select a).ToList();
                if (AdatNap == null || AdatNap.Count == 0) throw new HibásBevittAdat("Nincs listázandó adat."); ;
                Tábla_utolsó.Refresh();
                Tábla_utolsó.Visible = false;
                AdatTábla_Utolsó.Clear();

                // fejléc elkészítése
                AdatTábla_Utolsó.Rows.Clear();
                AdatTábla_Utolsó.Columns.Clear();
                AdatTábla_Utolsó.Columns.Add("Azonosító");
                AdatTábla_Utolsó.Columns.Add("Dátum");
                AdatTábla_Utolsó.Columns.Add("Takarítási fajta");
                AdatTábla_Utolsó.Columns.Add("Telephely");
                AdatTábla_Utolsó.Columns.Add("Státus");
                AdatTábla_Utolsó.Columns.Add("Mikor");
                AdatTábla_Utolsó.Columns.Add("Módosító");


                foreach (Adat_Jármű_Takarítás_Napló rekord in AdatNap)
                {
                    DataRow Soradat = AdatTábla_Utolsó.NewRow();
                    Soradat["Azonosító"] = rekord.Azonosító.Trim();
                    Soradat["Dátum"] = rekord.Dátum.ToString("yyyy.MM.dd");
                    Soradat["Takarítási fajta"] = rekord.Takarítási_fajta.Trim();
                    Soradat["Telephely"] = rekord.Telephely.Trim();
                    Soradat["Státus"] = rekord.Státus;
                    Soradat["Mikor"] = rekord.Mikor;
                    Soradat["Módosító"] = rekord.Módosító.ToStrTrim();

                    AdatTábla_Utolsó.Rows.Add(Soradat);
                }
                Tábla_utolsó.CleanFilterAndSort();
                Tábla_utolsó.DataSource = AdatTábla_Utolsó;

                Tábla_utolsó.Columns["Azonosító"].Width = 100;
                Tábla_utolsó.Columns["Dátum"].Width = 100;
                Tábla_utolsó.Columns["Takarítási fajta"].Width = 100;
                Tábla_utolsó.Columns["Telephely"].Width = 150;
                Tábla_utolsó.Columns["Státus"].Width = 100;
                Tábla_utolsó.Columns["Mikor"].Width = 200;
                Tábla_utolsó.Columns["Módosító"].Width = 100;


                Tábla_utolsó.Visible = true;
                Tábla_utolsó.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Utolsó_módosít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utolsó_pályaszám.Text.ToStrTrim() == "") return;
                if (Utolsó_takarítási_fajta.Text.ToStrTrim() == "") return;
                if (Utolsó_telephely.Text.ToStrTrim() == "") return;
                AdatokTak = KézTak.Lista_Adatok();
                Adat_Jármű_Takarítás_Takarítások AdatTak = (from a in AdatokTak
                                                            where a.Azonosító == Utolsó_pályaszám.Text.Trim()
                                                            && a.Takarítási_fajta == Utolsó_takarítási_fajta.Text.Trim()
                                                            && a.Telephely == Utolsó_telephely.Text.Trim()
                                                            select a).FirstOrDefault();
                int státus = 0;
                if (Utolsó_státus.Checked) státus = 1;

                Adat_Jármű_Takarítás_Takarítások ADAT = new Adat_Jármű_Takarítás_Takarítások(
                                                Utolsó_pályaszám.Text.ToStrTrim(),
                                                Utolsó_dátum.Value,
                                                Utolsó_takarítási_fajta.Text.ToStrTrim(),
                                                Utolsó_telephely.Text.ToStrTrim(),
                                                státus);

                if (AdatTak != null)
                    KézTak.Módosítás_Dátum(ADAT);
                else
                    KézTak.Rögzítés(ADAT);

                Előzmény_lista_takarítás();
                TáblaUtolsóCella();
                MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TáblaUtolsóCella()
        {
            try
            {
                if (Tábla_utolsó.RowCount > 0)
                {
                    foreach (DataGridViewRow row in Tábla_utolsó.Rows)
                    {
                        if (row.Cells[4].Value.ToÉrt_Int() == 1)
                        {
                            row.DefaultCellStyle.ForeColor = Color.White;
                            row.DefaultCellStyle.BackColor = Color.IndianRed;
                            row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f, FontStyle.Strikeout);
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

        private void Excel_Takarítás_Click(object sender, EventArgs e)
        {
            if (Tábla_utolsó.Rows.Count <= 0) return;
            string fájlexc;

            // kimeneti fájl helye és neve
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog
            {
                InitialDirectory = "MyDocuments",
                Title = "Listázott tartalom mentése Excel fájlba",
                FileName = $"Takarítás_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddhhmmss}",
                Filter = "Excel |*.xlsx"
            };
            // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
            if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                fájlexc = SaveFileDialog1.FileName;
            }
            else
            {
                return;
            }

            MyE.DataGridViewToExcel(fájlexc, Tábla_utolsó);
            MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

            MyE.Megnyitás(fájlexc);
        }

        private void PályaszámTakarításai_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utolsó_pályaszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva pályaszám");
                List<Adat_Jármű_Takarítás_Takarítások> Adatok = KézTak.Lista_Adatok();
                Adatok = (from a in Adatok
                          where a.Azonosító == Utolsó_pályaszám.Text.Trim()
                          && a.Státus == 0
                          orderby a.Takarítási_fajta, a.Dátum descending
                          select a).ToList();

                Tábla_utolsó.Visible = false;
                Tábla_utolsó_Fejléc();
                Tábla_Tartalom(Adatok);
                Tábla_utolsó.CleanFilterAndSort();
                Tábla_utolsó.DataSource = AdatTábla_Utolsó;
                Tábla_utolsó_OszlopSzél();
                Tábla_utolsó.Visible = true;
                Tábla_utolsó.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Tábla_Tartalom(List<Adat_Jármű_Takarítás_Takarítások> Adatok)
        {
            AdatTábla_Utolsó.Clear();
            foreach (MyEn.TakfajtaadatÖ elem in Enum.GetValues(typeof(MyEn.TakfajtaadatÖ)))
            {
                foreach (Adat_Jármű_Takarítás_Takarítások rekord in Adatok)
                {
                    if (rekord.Takarítási_fajta == elem.ToStrTrim())
                    {
                        DataRow Soradat = AdatTábla_Utolsó.NewRow();
                        Soradat["Azonosító"] = rekord.Azonosító.Trim();
                        Soradat["Dátum"] = rekord.Dátum.ToString("yyyy.MM.dd");
                        Soradat["Takarítási fajta"] = rekord.Takarítási_fajta.Trim();
                        Soradat["Telephely"] = rekord.Telephely.Trim();
                        Soradat["Státus"] = rekord.Státus;
                        AdatTábla_Utolsó.Rows.Add(Soradat);
                        break;
                    }
                }
            }
        }
        #endregion


        #region Ütemezés alapadatok
        private void Ütem_Telephelyek_feltöltése()
        {
            Ütem_telephely.Items.Clear();
            if (Cmbtelephely.Enabled == false)
            {
                // ha csak egy telephely
                Ütem_telephely.Items.Add(Cmbtelephely.Text.Trim());
            }
            else
            {
                for (int i = 0; i < Cmbtelephely.Items.Count; i++)
                    Ütem_telephely.Items.Add(Cmbtelephely.Items[i].ToStrTrim());
            }
        }

        private void Ütem_takfajta_feltöltés()
        {
            Ütem_takarítási_fajta.Items.Clear();
            Ütem_takarítási_fajta.Items.Add("");
            foreach (MyEn.Takfajtaadat elem in Enum.GetValues(typeof(MyEn.Takfajtaadat)))
                Ütem_takarítási_fajta.Items.Add($"{elem.ToString().Replace('_', ' ')}");
        }

        private void Ütem_pályaszám_feltöltés()
        {
            List<Adat_Jármű_Takarítás_Ütemező> Adatok = KézÜtem.Lista_Adat();
            if (Cmbtelephely.Text.Trim() != "Főmérnökség" || Program.Postás_Vezér)
                Adatok = Adatok.Where(a => a.Telephely == Cmbtelephely.Text.Trim()).OrderBy(a => a.Azonosító).ToList();
            List<string> Pályaszámok = Adatok.Select(a => a.Azonosító).Distinct().ToList();
            Ütem_azonosító.Items.Clear();
            foreach (string Elem in Pályaszámok)
                Ütem_azonosító.Items.Add(Elem);

            Ütem_azonosító.Refresh();
        }

        private void Ütem_lépték_feltöltése()
        {
            Ütem_mérték.Items.Clear();
            Ütem_mérték.Items.Add("Nap");
            Ütem_mérték.Items.Add("Hónap");
        }

        private void Ütem_frissít_Click(object sender, EventArgs e)
        {
            Listázza_Ütemtervet();
            Ütem_mezők_ürítése();
        }

        private void Listázza_Ütemtervet()
        {
            try
            {
                List<Adat_Jármű_Takarítás_Ütemező> AdatokÜtem = KézÜtem.Lista_Adat();

                Ütem_Tábla.Rows.Clear();
                Ütem_Tábla.Columns.Clear();
                Ütem_Tábla.Refresh();
                Ütem_Tábla.Visible = false;
                Ütem_Tábla.ColumnCount = 7;

                // fejléc elkészítése
                Ütem_Tábla.Columns[0].HeaderText = "Pályaszám";
                Ütem_Tábla.Columns[0].Width = 100;
                Ütem_Tábla.Columns[1].HeaderText = "Kezdő Dátum";
                Ütem_Tábla.Columns[1].Width = 100;
                Ütem_Tábla.Columns[2].HeaderText = "Ütem nagysága";
                Ütem_Tábla.Columns[2].Width = 100;
                Ütem_Tábla.Columns[3].HeaderText = "Ütem lépték";
                Ütem_Tábla.Columns[3].Width = 100;
                Ütem_Tábla.Columns[4].HeaderText = "Takarítási fajta";
                Ütem_Tábla.Columns[4].Width = 100;
                Ütem_Tábla.Columns[5].HeaderText = "Telephely";
                Ütem_Tábla.Columns[5].Width = 100;
                Ütem_Tábla.Columns[6].HeaderText = "Státus";
                Ütem_Tábla.Columns[6].Width = 100;

                int i;

                List<Adat_Jármű_Takarítás_Ütemező> ÜtemAdatok = (from a in AdatokÜtem
                                                                 orderby a.Azonosító
                                                                 select a).ToList();

                if (Ütem_azonosító.Text.Trim() != "")
                {
                    List<Adat_Jármű_Takarítás_Ütemező> ÜtemAdatSzűrt = (from a in ÜtemAdatok
                                                                        where a.Azonosító == Ütem_azonosító.Text.Trim()
                                                                        select a).ToList();
                    foreach (Adat_Jármű_Takarítás_Ütemező rekord in ÜtemAdatSzűrt)
                    {
                        Ütem_Tábla.RowCount++;
                        i = Ütem_Tábla.RowCount - 1;
                        Ütem_Tábla.Rows[i].Cells[0].Value = rekord.Azonosító.Trim();
                        Ütem_Tábla.Rows[i].Cells[1].Value = rekord.Dátum.ToString("yyyy.MM.dd");
                        Ütem_Tábla.Rows[i].Cells[2].Value = rekord.Növekmény;
                        Ütem_Tábla.Rows[i].Cells[3].Value = rekord.Mérték.Trim();
                        Ütem_Tábla.Rows[i].Cells[4].Value = rekord.Takarítási_fajta.Trim();
                        Ütem_Tábla.Rows[i].Cells[5].Value = rekord.Telephely.Trim();
                        Ütem_Tábla.Rows[i].Cells[6].Value = rekord.Státus;
                    }
                }
                else
                {
                    foreach (Adat_Jármű_Takarítás_Ütemező rekord in ÜtemAdatok)
                    {
                        Ütem_Tábla.RowCount++;
                        i = Ütem_Tábla.RowCount - 1;
                        Ütem_Tábla.Rows[i].Cells[0].Value = rekord.Azonosító.Trim();
                        Ütem_Tábla.Rows[i].Cells[1].Value = rekord.Dátum.ToString("yyyy.MM.dd");
                        Ütem_Tábla.Rows[i].Cells[2].Value = rekord.Növekmény;
                        Ütem_Tábla.Rows[i].Cells[3].Value = rekord.Mérték.Trim();
                        Ütem_Tábla.Rows[i].Cells[4].Value = rekord.Takarítási_fajta.Trim();
                        Ütem_Tábla.Rows[i].Cells[5].Value = rekord.Telephely.Trim();
                        Ütem_Tábla.Rows[i].Cells[6].Value = rekord.Státus;
                    }


                }

                Ütem_Tábla.Visible = true;
                Ütem_Tábla.Refresh();

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ütem_Tábla_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (Ütem_Tábla.RowCount > 0)
                {

                    foreach (DataGridViewRow row in Ütem_Tábla.Rows)
                    {
                        if (row.Cells[6].Value.ToÉrt_Int() == 9)
                        {
                            row.DefaultCellStyle.ForeColor = Color.White;
                            row.DefaultCellStyle.BackColor = Color.IndianRed;
                            row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f, FontStyle.Strikeout);
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

        private void Ütem_Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                int i = e.RowIndex;

                Ütem_azonosító.Text = Ütem_Tábla.Rows[i].Cells[0].Value.ToStrTrim();
                Ütem_kezdődátum.Value = Ütem_Tábla.Rows[i].Cells[1].Value.ToÉrt_DaTeTime();
                Ütem_növekmény.Text = Ütem_Tábla.Rows[i].Cells[2].Value.ToStrTrim();
                Ütem_mérték.Text = Ütem_Tábla.Rows[i].Cells[3].Value.ToStrTrim();
                Ütem_takarítási_fajta.Text = Ütem_Tábla.Rows[i].Cells[4].Value.ToStrTrim();
                Ütem_telephely.Text = Ütem_Tábla.Rows[i].Cells[5].Value.ToStrTrim();
                Ütem_státus.Checked = Ütem_Tábla.Rows[i].Cells[6].Value.ToÉrt_Bool();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ütem_mezők_ürítése()
        {

            Ütem_azonosító.Text = "";
            Ütem_kezdődátum.Value = DateTime.Parse("1900.01.01");
            Ütem_növekmény.Text = "";
            Ütem_mérték.Text = "";
            Ütem_takarítási_fajta.Text = "";
            Ütem_telephely.Text = "";
            Ütem_státus.Checked = false;
        }

        private void Ütem_Rögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Ütem_azonosító.Text.ToStrTrim() == "") return;
                if (Ütem_takarítási_fajta.Text.ToStrTrim() == "") return;
                if (Ütem_telephely.Text.ToStrTrim() == "") return;
                if (Ütem_mérték.Text.ToStrTrim() == "") return;
                if (Ütem_növekmény.Text.ToStrTrim() == "") return;
                if (!int.TryParse(Ütem_növekmény.Text, out int Növekmény)) return;

                AdatokÜtem = KézÜtem.Lista_Adat();
                Adat_Jármű_Takarítás_Ütemező AdatÜtem = (from a in AdatokÜtem
                                                         where a.Azonosító == Ütem_azonosító.Text.Trim()
                                                         && a.Takarítási_fajta == Ütem_takarítási_fajta.Text.Trim()
                                                         && a.Telephely == Ütem_telephely.Text.Trim()
                                                         select a).FirstOrDefault();
                int státus = 0;
                if (Ütem_státus.Checked) státus = 9;

                Adat_Jármű_Takarítás_Ütemező ADAT = new Adat_Jármű_Takarítás_Ütemező(
                                            Ütem_azonosító.Text.ToStrTrim(),
                                            Ütem_kezdődátum.Value,
                                            Növekmény,
                                            Ütem_mérték.Text.ToStrTrim(),
                                            Ütem_takarítási_fajta.Text.ToStrTrim(),
                                            Ütem_telephely.Text.ToStrTrim(),
                                            státus);


                if (AdatÜtem != null)
                    KézÜtem.Módosítás(ADAT);
                else
                    KézÜtem.Rögzítés(ADAT);

                Listázza_Ütemtervet();
                MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Excell_Ütem_tábla_Click(object sender, EventArgs e)
        {
            try
            {
                if (Ütem_Tábla.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Takarítás{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddhhmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                {
                    fájlexc = SaveFileDialog1.FileName;
                }
                else
                {
                    return;
                }

                MyE.DataGridViewToExcel(fájlexc, Ütem_Tábla);

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

        private void JDátum_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Takarítottkocsik();
                OpciósKocsik();
            }
            catch (HibásBevittAdat ex)
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


        #region Havi lekérdezések lapfül

        private void Havi_Lekérdezés_Click(object sender, EventArgs e)
        {
            try
            {
                if (RadioButton3.Checked) J1Listázás();
                if (RadioButton4.Checked && Lekérdezés_Kategória.Text.Trim() == "Graffiti")
                {
                    GraffitiListázás();
                }
                else if (RadioButton4.Checked && Lekérdezés_Kategória.Text.Trim() == "Eseti")
                {
                    GraffitiListázás();
                }
                else if (RadioButton4.Checked && Lekérdezés_Kategória.Text.Trim() == "Fertőtlenítés")
                {
                    GraffitiListázás();
                }
                else if (RadioButton4.Checked && Lekérdezés_Kategória.Text.ToStrTrim() != "" && Lekérdezés_Kategória.Text.ToStrTrim() != "Graffiti")
                {
                    MindenListázás();
                }
                if (RadioButton6.Checked)
                    LétszámListázás();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void J1Listázás()
        {
            try
            {

                int hónapnap = DateTime.DaysInMonth(ListaDátum.Value.Year, ListaDátum.Value.Month);
                DateTime hónaputolsónapja = new DateTime(ListaDátum.Value.Year, ListaDátum.Value.Month, hónapnap);

                AdatokJ1 = KézJ1.Lista_Adat(Cmbtelephely.Text.Trim(), ListaDátum.Value.Year);
                // fejléc

                ListaTábla.Rows.Clear();
                ListaTábla.Columns.Clear();
                ListaTábla.Refresh();
                ListaTábla.Visible = false;
                ListaTábla.ColumnCount = 1;

                // fejléc elkészítése
                ListaTábla.Columns[0].HeaderText = "Dátum";
                ListaTábla.Columns[0].Width = 120;

                // kiirjuk a napokat

                ListaTábla.RowCount = hónapnap + 1;

                for (int ki = 1; ki <= hónapnap; ki++)
                    ListaTábla.Rows[ki].Cells[0].Value = new DateTime(ListaDátum.Value.Year, ListaDátum.Value.Month, ki).ToString("yyyy.MM.dd");
                // hány típus van?
                int volt = 0;


                int i = 0;
                int napszak = 1;
                if (RadioButton1.Checked != true)
                    napszak = 2;

                DateTime Hónaputolsónapja = MyF.Hónap_utolsónapja(ListaDátum.Value);
                DateTime Hónapelsőnapja = MyF.Hónap_elsőnapja(ListaDátum.Value);

                string típus = "";


                List<Adat_Jármű_Takarítás_J1> J1Szűrt = (from a in AdatokJ1
                                                         where a.Dátum >= Hónapelsőnapja
                                                         && a.Dátum <= Hónaputolsónapja
                                                         && a.Napszak == napszak
                                                         orderby a.Típus, a.Dátum
                                                         select a).ToList();

                foreach (Adat_Jármű_Takarítás_J1 rekord in J1Szűrt)
                {

                    if (típus.Trim() != rekord.Típus.ToStrTrim())
                    {
                        // ha új típus, akkor elkészítjük az új oszlopokat
                        volt += 1;
                        típus = rekord.Típus.ToStrTrim();

                        ListaTábla.ColumnCount += 2;

                        // fejléc elkészítése
                        ListaTábla.Columns[ListaTábla.ColumnCount - 2].HeaderText = típus;
                        ListaTábla.Columns[ListaTábla.ColumnCount - 2].Width = 130;
                        ListaTábla.Columns[ListaTábla.ColumnCount - 1].Width = 130;
                        ListaTábla.Rows[i].Cells[ListaTábla.ColumnCount - 2].Value = "Megfelelő";
                        ListaTábla.Rows[i].Cells[ListaTábla.ColumnCount - 1].Value = "Nem Megfelelő";

                    }
                    // feltöljük az adatokat
                    ListaTábla.Rows[rekord.Dátum.Day].Cells[ListaTábla.ColumnCount - 2].Value = rekord.J1megfelelő;
                    ListaTábla.Rows[rekord.Dátum.Day].Cells[ListaTábla.ColumnCount - 1].Value = rekord.J1nemmegfelelő;
                }
                ListaTábla.Visible = true;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MindenListázás()
        {
            try
            {
                AdatokTelj?.Clear();
                AdatokTelj = KézTakarításTelj.Lista_Adatok(Cmbtelephely.Text.Trim(), ListaDátum.Value.Year);

                if (AdatokTelj?.Count <= 0)
                {
                    MessageBox.Show("Nincsen ilyen adat", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // fejléc
                ListaTábla.Rows.Clear();
                ListaTábla.Columns.Clear();
                ListaTábla.Refresh();
                ListaTábla.Visible = false;
                ListaTábla.ColumnCount = 1;
                ListaTábla.Columns[0].HeaderText = "Dátum";
                ListaTábla.Columns[0].Width = 120;

                // kiirjuk a napokat
                ListaTábla.RowCount = MyF.Hónap_hossza(ListaDátum.Value) + 1;
                for (int ki = 1; ki <= MyF.Hónap_hossza(ListaDátum.Value); ki++)
                    ListaTábla.Rows[ki].Cells[0].Value = new DateTime(ListaDátum.Value.Year, ListaDátum.Value.Month, ki).ToString("yyyy.MM.dd");

                int i = 0;
                string típus = "";
                int napszak = 2;
                if (RadioButton1.Checked) napszak = 1;


                DateTime HónapElsőN = MyF.Hónap_elsőnapja(ListaDátum.Value);
                DateTime HónapUtolsóN = MyF.Hónap_utolsónapja(ListaDátum.Value);

                List<Adat_Jármű_Takarítás_Teljesítés> SzűrtTelj = (from a in AdatokTelj
                                                                   where a.Dátum >= HónapElsőN
                                                                   && a.Dátum <= HónapUtolsóN
                                                                   && a.Napszak == napszak
                                                                   && a.Takarítási_fajta == Lekérdezés_Kategória.Text.ToStrTrim()
                                                                   orderby a.Típus, a.Dátum
                                                                   select a).ToList();
                if (SzűrtTelj.Count == 0)
                {
                    MessageBox.Show("Nincsen ilyen adat", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ListaTábla.Visible = true;
                    return;
                }
                int megf = 0;
                int Nmegf = 0;
                DateTime Melyiknapra = SzűrtTelj[0].Dátum;
                foreach (Adat_Jármű_Takarítás_Teljesítés rekord in SzűrtTelj)
                {
                    if (típus.Trim() != rekord.Típus.ToStrTrim())
                    {
                        // ha új típus, akkor elkészítjük az új oszlopokat
                        megf = 0;
                        Nmegf = 0;
                        típus = rekord.Típus.ToStrTrim();
                        ListaTábla.ColumnCount += 2;

                        // fejléc elkészítése
                        ListaTábla.Columns[ListaTábla.ColumnCount - 2].HeaderText = típus;
                        ListaTábla.Columns[ListaTábla.ColumnCount - 2].Width = 130;
                        ListaTábla.Columns[ListaTábla.ColumnCount - 1].Width = 130;
                        ListaTábla.Rows[i].Cells[ListaTábla.ColumnCount - 2].Value = "Megfelelő";
                        ListaTábla.Rows[i].Cells[ListaTábla.ColumnCount - 1].Value = "Nem Megfelelő";
                    }

                    if (Melyiknapra != rekord.Dátum)
                    {
                        Melyiknapra = rekord.Dátum;
                        megf = 0;
                        Nmegf = 0;
                    }



                    // feltöljük az adatokat
                    switch (rekord.Státus)
                    {
                        case 1:
                            {
                                megf++;
                                ListaTábla.Rows[rekord.Dátum.Day].Cells[ListaTábla.ColumnCount - 2].Value = megf;
                                break;
                            }
                        case 2:
                            {
                                Nmegf++;
                                ListaTábla.Rows[rekord.Dátum.Day].Cells[ListaTábla.ColumnCount - 1].Value = Nmegf;
                                break;
                            }

                    }
                }
                ListaTábla.Refresh();
                ListaTábla.Visible = true;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GraffitiListázás()
        {
            try
            {
                AdatokTelj?.Clear();
                AdatokTelj = KézTakarításTelj.Lista_Adatok(Cmbtelephely.Text.Trim(), ListaDátum.Value.Year);

                if (AdatokTelj?.Count <= 0)
                {
                    MessageBox.Show("Nincsen ilyen adat", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // fejléc
                ListaTábla.Rows.Clear();
                ListaTábla.Columns.Clear();
                ListaTábla.Refresh();
                ListaTábla.Visible = false;
                ListaTábla.ColumnCount = 1;
                ListaTábla.Columns[0].HeaderText = "Dátum";
                ListaTábla.Columns[0].Width = 120;

                // kiirjuk a napokat
                ListaTábla.RowCount = MyF.Hónap_hossza(ListaDátum.Value) + 1;
                for (int ki = 1; ki <= MyF.Hónap_hossza(ListaDátum.Value); ki++)
                    ListaTábla.Rows[ki].Cells[0].Value = new DateTime(ListaDátum.Value.Year, ListaDátum.Value.Month, ki).ToString("yyyy.MM.dd");

                int i = 0;
                string típus = "";
                int napszak = 2;
                if (RadioButton1.Checked) napszak = 1;


                DateTime HónapElsőN = MyF.Hónap_elsőnapja(ListaDátum.Value);
                DateTime HónapUtolsóN = MyF.Hónap_utolsónapja(ListaDátum.Value);

                List<Adat_Jármű_Takarítás_Teljesítés> SzűrtTelj = (from a in AdatokTelj
                                                                   where a.Dátum >= HónapElsőN
                                                                   && a.Dátum <= HónapUtolsóN
                                                                   && a.Napszak == napszak
                                                                   && a.Takarítási_fajta == Lekérdezés_Kategória.Text.ToStrTrim()
                                                                   && a.Státus != 3
                                                                   orderby a.Típus, a.Dátum
                                                                   select a).ToList();
                if (SzűrtTelj.Count == 0)
                {
                    MessageBox.Show("Nincsen ilyen adat", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ListaTábla.Visible = true;
                    return;
                }
                double mérték = 0;
                DateTime Melyiknapra = SzűrtTelj[0].Dátum;
                foreach (Adat_Jármű_Takarítás_Teljesítés rekord in SzűrtTelj)
                {
                    if (típus.Trim() != rekord.Típus.ToStrTrim())
                    {
                        // ha új típus, akkor elkészítjük az új oszlopokat
                        típus = rekord.Típus.ToStrTrim();
                        ListaTábla.ColumnCount += 1;

                        // fejléc elkészítése
                        ListaTábla.Columns[ListaTábla.ColumnCount - 1].HeaderText = típus;
                        ListaTábla.Columns[ListaTábla.ColumnCount - 1].Width = 130;
                        ListaTábla.Rows[i].Cells[ListaTábla.ColumnCount - 1].Value = "Felület";
                    }

                    if (Melyiknapra != rekord.Dátum) { Melyiknapra = rekord.Dátum; mérték = rekord.Mérték; }
                    else mérték += rekord.Mérték;

                    // feltöljük az adatokat
                    ListaTábla.Rows[rekord.Dátum.Day].Cells[ListaTábla.ColumnCount - 1].Value = mérték;


                }
                ListaTábla.Refresh();
                ListaTábla.Visible = true;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LétszámListázás()
        {
            try
            {
                int hónapnap = DateTime.DaysInMonth(ListaDátum.Value.Year, ListaDátum.Value.Month);
                DateTime hónaputolsónapja = new DateTime(ListaDátum.Value.Year, ListaDátum.Value.Month, hónapnap);

                int napszak = 1;
                if (!RadioButton1.Checked) napszak = 2;

                List<Adat_Jármű_Takarítás_Létszám> AdatLétsz = KézLétsz.Lista_Adat(Cmbtelephely.Text.Trim(), ListaDátum.Value.Year).OrderBy(a => a.Dátum).ToList();

                ListaTábla.Rows.Clear();
                ListaTábla.Columns.Clear();
                ListaTábla.Refresh();
                ListaTábla.Visible = false;
                ListaTábla.ColumnCount = 4;

                // fejléc elkészítése
                ListaTábla.Columns[0].HeaderText = "Dátum";
                ListaTábla.Columns[0].Width = 110;
                ListaTábla.Columns[1].HeaderText = "Előírt létszám";
                ListaTábla.Columns[1].Width = 200;
                ListaTábla.Columns[2].HeaderText = "Megjelent létszám";
                ListaTábla.Columns[2].Width = 200;
                ListaTábla.Columns[3].HeaderText = "Előírt ruházatot nem viselt:";
                ListaTábla.Columns[3].Width = 300;
                // kiirjuk a napokat

                ListaTábla.RowCount = hónapnap;

                for (int i = 1; i <= hónapnap; i++)
                    ListaTábla.Rows[i - 1].Cells[0].Value = new DateTime(ListaDátum.Value.Year, ListaDátum.Value.Month, i).ToString("yyyy.MM.dd");

                DateTime listadátum = new DateTime(ListaDátum.Value.Year, ListaDátum.Value.Month, 1);
                List<Adat_Jármű_Takarítás_Létszám> Létszámadat = (from a in AdatLétsz
                                                                  where a.Dátum >= listadátum
                                                                  && a.Dátum.ToÉrt_DaTeTime() <= hónaputolsónapja.ToÉrt_DaTeTime()
                                                                  && a.Napszak == napszak
                                                                  select a).ToList();

                foreach (Adat_Jármű_Takarítás_Létszám rekord in Létszámadat)
                {


                    ListaTábla.Rows[rekord.Dátum.Day - 1].Cells[1].Value = rekord.Előírt;
                    ListaTábla.Rows[rekord.Dátum.Day - 1].Cells[2].Value = rekord.Megjelent;
                    ListaTábla.Rows[rekord.Dátum.Day - 1].Cells[3].Value = rekord.Ruhátlan;
                }

                ListaTábla.Visible = true;
                ListaTábla.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Lek_excel_Click(object sender, EventArgs e)
        {
            try
            {
                // kimeneti fájl helye és neve
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Jármű Takarítási teljesítési igazolás készítés",
                    FileName = $"Jármű Takarítási teljesítési igazolás_{ListaDátum.Value:yyyyMM}_{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                {
                    fájlexc = SaveFileDialog1.FileName;
                }
                else
                {
                    return;
                }
                DateTime Eleje = DateTime.Now;
                // megnyitjuk az excelt
                MyE.ExcelLétrehozás();

                // '*********************************************
                // '*******munka lapok elkészítése   ************
                // '*********************************************

                //Meglévőt átnevezzük 
                MyE.Munkalap_átnevezés("Munka1", "Összesítő_eredmény");

                // az új munkalapoknak elkészítjük a listát
                List<string> Munkalapnév = new List<string>
                {
                    "Összesítő_minden",
                    "Létszám",
                    "J1"
                };

                for (int i = 0; i <= Lekérdezés_Kategória.Items.Count - 1; i++)
                {
                    Munkalapnév.Add(Lekérdezés_Kategória.Items[i].ToString());
                }

                //Elkészítjük a munkalapokat
                for (int i = 0; i < Munkalapnév.Count; i++)
                {
                    MyE.Új_munkalap(Munkalapnév[i].ToString());
                }


                Holtart.Lép();
                // Elkészítjük a munkalapokat
                Létszám_excel();
                J1_excel();

                for (int ii = 0; ii <= Lekérdezés_Kategória.Items.Count - 1; ii++)
                {
                    Lekérdezés_Kategória.Text = Lekérdezés_Kategória.Items[ii].ToString();
                    if (Lekérdezés_Kategória.Items[ii].ToString().Substring(0, 1) == "J")
                        J2_excel(Lekérdezés_Kategória.Items[ii].ToStrTrim());
                }

                Opció_excel("Graffiti");
                Opció_excel("Eseti");
                Opció_excel("Fertőtlenítés");

                Összesítő_Minden();
                Összesítő_eredmény();


                MyE.Munkalap_aktív("Összesítő_eredmény");
                MyE.Aktív_Cella("Összesítő_eredmény", "A1");


                // az excel tábla bezárása
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();
                DateTime Vége = DateTime.Now;
                MyE.Megnyitás(fájlexc);
                Holtart.Ki();
                MessageBox.Show($"A feladat {Vége - Eleje} idő alatt végrehajtásra került.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Létszám_excel()
        {
            try
            {
                // *********************************************
                // ********* Létszám tábla *********************
                // *********************************************
                // fejléc elkészítése
                MyE.Munkalap_aktív("Létszám");

                MyE.Kiir("Dátum", "a1");
                MyE.Egyesít("Létszám", "b1:e1");
                MyE.Kiir("Nappal", "b1");
                MyE.Kiir("Előírt létszám", "b2");
                MyE.Kiir("Megjelent létszám", "c2");
                MyE.Kiir("Nem teljesített létszám", "d2");
                MyE.Kiir("Előírt ruházatot nem viselt:", "e2");
                MyE.Egyesít("Létszám", "f1:i1");
                MyE.Kiir("Éjszaka", "f1");
                MyE.Kiir("Előírt létszám", "f2");
                MyE.Kiir("Megjelent létszám", "g2");
                MyE.Kiir("Nem teljesített létszám", "h2");
                MyE.Kiir("Előírt ruházatot nem viselt:", "i2");
                MyE.Oszlopszélesség("Létszám", "a:a", 10);
                MyE.Oszlopszélesség("Létszám", "b:i", 15);
                MyE.Sortörésseltöbbsorba("2:2");

                int hónapnap = DateTime.DaysInMonth(ListaDátum.Value.Year, ListaDátum.Value.Month);
                DateTime hónaputolsónapja = new DateTime(ListaDátum.Value.Year, ListaDátum.Value.Month, hónapnap);

                DateTime ideig;
                for (int j = 0; j < hónapnap; j++)
                {
                    ideig = new DateTime(ListaDátum.Value.Year, ListaDátum.Value.Month, j + 1);
                    MyE.Kiir(ideig.ToString("yyyy.MM.dd"), "a" + (j + 3));
                }

                MyE.Kiir("Összesen", "a" + (hónapnap + 3));
                // feltöljük a táblázatot

                ideig = new DateTime(ListaDátum.Value.Year, ListaDátum.Value.Month, 1);
                List<Adat_Jármű_Takarítás_Létszám> AdatLétsz = KézLétsz.Lista_Adat(Cmbtelephely.Text.Trim(), ListaDátum.Value.Year);

                AdatLétsz = (from a in AdatLétsz
                             where a.Dátum >= ideig
                             && a.Dátum <= hónaputolsónapja
                             && a.Napszak == 1
                             orderby a.Dátum
                             select a).ToList();

                int i;
                Holtart.Be(hónapnap + 1);

                foreach (Adat_Jármű_Takarítás_Létszám rekord in AdatLétsz)
                {
                    i = rekord.Dátum.Day;
                    MyE.Kiir(rekord.Előírt.ToStrTrim(), "b" + (i + 2));
                    MyE.Kiir(rekord.Megjelent.ToStrTrim(), "c" + (i + 2));
                    MyE.Kiir("=IF(RC[-2]-RC[-1]>0,RC[-2]-RC[-1],0)", "d" + (i + 2));
                    MyE.Kiir(rekord.Ruhátlan.ToStrTrim(), "e" + (i + 2));
                    Holtart.Lép();
                }

                MyE.Kiir("=SUM(R[-" + hónapnap.ToString() + "]C:R[-1]C)", "d" + (hónapnap + 3));
                MyE.Kiir("=SUM(R[-" + hónapnap.ToString() + "]C:R[-1]C)", "e" + (hónapnap + 3));

                AdatLétsz.Clear();
                AdatLétsz = KézLétsz.Lista_Adat(Cmbtelephely.Text.Trim(), ListaDátum.Value.Year);

                AdatLétsz = (from a in AdatLétsz
                             where a.Dátum >= ideig
                             && a.Dátum <= hónaputolsónapja
                             && a.Napszak == 2
                             orderby a.Dátum
                             select a).ToList();


                foreach (Adat_Jármű_Takarítás_Létszám rekord in AdatLétsz)
                {
                    i = rekord.Dátum.Day;
                    MyE.Kiir(rekord.Előírt.ToStrTrim(), "f" + (i + 2));
                    MyE.Kiir(rekord.Megjelent.ToStrTrim(), "g" + (i + 2));
                    MyE.Kiir("=IF(RC[-2]-RC[-1]>0,RC[-2]-RC[-1],0)", "h" + (i + 2));
                    MyE.Kiir(rekord.Ruhátlan.ToStrTrim(), "i" + (i + 2));
                    Holtart.Lép();
                }

                MyE.Kiir("=SUM(R[-" + hónapnap + "]C:R[-1]C)", "h" + (hónapnap + 3));
                MyE.Kiir("=SUM(R[-" + hónapnap + "]C:R[-1]C)", "i" + (hónapnap + 3));
                MyE.Rácsoz("a1:i" + (hónapnap + 3));
                MyE.Vastagkeret("a1:i2");
                MyE.Vastagkeret("a" + (hónapnap + 3) + ":i" + (hónapnap + 3));

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void J1_excel()
        {
            try
            {
                MyE.Munkalap_aktív("J1");

                // beolvassuk a kötbér értékét
                AdatokKötbér = KézKötbér.Lista_Adat();
                Adat_Jármű_Takarítás_Kötbér AdatKötbér = (from a in AdatokKötbér
                                                          where a.Takarítási_fajta == "J1"
                                                          select a).FirstOrDefault();

                double NemMegfelel = AdatKötbér.NemMegfelel.ToÉrt_Double();
                double Póthatáridő = AdatKötbér.Póthatáridő.ToÉrt_Double();

                // Dátum kiírása
                MyE.Kiir("Dátum", "a3");

                int hónapnap = DateTime.DaysInMonth(ListaDátum.Value.Year, ListaDátum.Value.Month);
                DateTime hónaputolsónapja = new DateTime(ListaDátum.Value.Year, ListaDátum.Value.Month, hónapnap);

                DateTime ideig;
                for (int j = 1; j <= hónapnap; j++)
                {
                    ideig = new DateTime(ListaDátum.Value.Year, ListaDátum.Value.Month, j);
                    MyE.Kiir(ideig.ToString("yyyy.MM.dd"), "a" + (j + 3));
                }

                MyE.Kiir("Típus", "a1");
                MyE.Rácsoz("a1:a" + (hónapnap + 4));

                ideig = new DateTime(ListaDátum.Value.Year, ListaDátum.Value.Month, 1);

                int oszlop = 2;
                string típus = "";
                int volt = 0;
                int i = 0;
                int nap = 0;
                Holtart.Be(hónapnap + 2);


                AdatokJ1 = KézJ1.Lista_Adat(Cmbtelephely.Text.Trim(), ListaDátum.Value.Year);
                AdatokJ1 = (from a in AdatokJ1
                            where a.Dátum >= ideig
                            && a.Dátum <= hónaputolsónapja
                            orderby a.Típus, a.Dátum, a.Napszak
                            select a).ToList();

                foreach (Adat_Jármű_Takarítás_J1 rekord in AdatokJ1)
                {
                    i = rekord.Dátum.Day;
                    if (típus != rekord.Típus.Trim())
                    {
                        if (!string.IsNullOrEmpty(típus))
                            oszlop += 12;
                        volt += 1;
                        típus = rekord.Típus.Trim();
                        // fejléc elkészítése
                        MyE.Egyesít("J1", MyE.Oszlopnév(oszlop) + "1:" + MyE.Oszlopnév(oszlop + 11) + "1");
                        MyE.Kiir(típus, MyE.Oszlopnév(oszlop) + "1");

                        MyE.Egyesít("J1", MyE.Oszlopnév(oszlop) + "2:" + MyE.Oszlopnév(oszlop + 3) + "2");
                        MyE.Kiir("Nappal", MyE.Oszlopnév(oszlop) + "2");
                        MyE.Egyesít("J1", MyE.Oszlopnév(oszlop + 4) + "2:" + MyE.Oszlopnév(oszlop + 7) + "2");
                        MyE.Kiir("Éjszaka", MyE.Oszlopnév(oszlop + 4) + "2");
                        MyE.Egyesít("J1", MyE.Oszlopnév(oszlop + 8) + "2:" + MyE.Oszlopnév(oszlop + 11) + "2");
                        MyE.Kiir("Összesen", MyE.Oszlopnév(oszlop + 8) + "2");

                        MyE.Kiir("Előírt", MyE.Oszlopnév(oszlop) + "3");
                        MyE.Kiir("Megfelelő", MyE.Oszlopnév(oszlop + 1) + "3");
                        MyE.Kiir("Nem Megfelelő", MyE.Oszlopnév(oszlop + 2) + "3");
                        MyE.Kiir("Kötbér", MyE.Oszlopnév(oszlop + 3) + "3");

                        MyE.Kiir("Előírt", MyE.Oszlopnév(oszlop + 4) + "3");
                        MyE.Kiir("Megfelelő", MyE.Oszlopnév(oszlop + 5) + "3");
                        MyE.Kiir("Nem Megfelelő", MyE.Oszlopnév(oszlop + 6) + "3");
                        MyE.Kiir("Kötbér", MyE.Oszlopnév(oszlop + 7) + "3");

                        MyE.Kiir("Előírt", MyE.Oszlopnév(oszlop + 8) + "3");
                        MyE.Kiir("Megfelelő", MyE.Oszlopnév(oszlop + 9) + "3");
                        MyE.Kiir("Nem Megfelelő", MyE.Oszlopnév(oszlop + 10) + "3");
                        MyE.Kiir("Kötbér", MyE.Oszlopnév(oszlop + 11) + "3");

                        MyE.Oszlopszélesség("J1", MyE.Oszlopnév(oszlop) + ":" + MyE.Oszlopnév(oszlop + 11), 10);
                        MyE.Sortörésseltöbbsorba("3:3");

                        MyE.Rácsoz(MyE.Oszlopnév(oszlop) + "1:" + MyE.Oszlopnév(oszlop + 11) + "3");
                        MyE.Rácsoz(MyE.Oszlopnév(oszlop) + "4:" + MyE.Oszlopnév(oszlop + 11) + (hónapnap + 3));
                        MyE.Rácsoz(MyE.Oszlopnév(oszlop) + (hónapnap + 4) + ":" + MyE.Oszlopnév(oszlop + 11) + (hónapnap + 4));
                        for (int alma = 0; alma <= 11; alma++)
                            MyE.Kiir("=SUM(R[-" + hónapnap + "]C:R[-1]C)", MyE.Oszlopnév(oszlop + alma) + (hónapnap + 4));
                        MyE.Vastagkeret(MyE.Oszlopnév(oszlop) + "2:" + MyE.Oszlopnév(oszlop + 3) + (hónapnap + 4));
                        MyE.Vastagkeret(MyE.Oszlopnév(oszlop + 4) + "2:" + MyE.Oszlopnév(oszlop + 7) + (hónapnap + 4));
                        MyE.Vastagkeret(MyE.Oszlopnév(oszlop + 8) + "2:" + MyE.Oszlopnév(oszlop + 11) + (hónapnap + 4));
                    }
                    // kiíjuk az értéket
                    nap = rekord.Dátum.Day;
                    double eredmény = rekord.J1nemmegfelelő * (NemMegfelel + Póthatáridő);
                    if (rekord.Napszak == 1)
                    {
                        // ha nappal
                        MyE.Kiir("=RC[1]+RC[2]", MyE.Oszlopnév(oszlop) + (nap + 3));
                        MyE.Kiir(rekord.J1megfelelő.ToString(), MyE.Oszlopnév(oszlop + 1) + (nap + 3));
                        MyE.Kiir(rekord.J1nemmegfelelő.ToString(), MyE.Oszlopnév(oszlop + 2) + (nap + 3));
                        MyE.Kiir(eredmény.ToString(), MyE.Oszlopnév(oszlop + 3) + (nap + 3));
                    }
                    else
                    {
                        // ha éjszaka
                        MyE.Kiir("=RC[1]+RC[2]", MyE.Oszlopnév(oszlop + 4) + (nap + 3));
                        MyE.Kiir(rekord.J1megfelelő.ToString(), MyE.Oszlopnév(oszlop + 5) + (nap + 3));
                        MyE.Kiir(rekord.J1nemmegfelelő.ToString(), MyE.Oszlopnév(oszlop + 6) + (nap + 3));
                        MyE.Kiir(eredmény.ToString(), MyE.Oszlopnév(oszlop + 7) + (nap + 3));
                    }

                    // összesítjük
                    MyE.Kiir("=RC[-8]+RC[-4]", MyE.Oszlopnév(oszlop + 8) + (nap + 3));
                    MyE.Kiir("=RC[-8]+RC[-4]", MyE.Oszlopnév(oszlop + 9) + (nap + 3));
                    MyE.Kiir("=RC[-8]+RC[-4]", MyE.Oszlopnév(oszlop + 10) + (nap + 3));
                    MyE.Kiir("=RC[-8]+RC[-4]", MyE.Oszlopnév(oszlop + 11) + (nap + 3));
                    Holtart.Lép();
                }

                // ha több csoport volt
                if (volt > 1)
                {
                    oszlop += 12;
                    // fejléc elkészítése
                    MyE.Egyesít("J1", MyE.Oszlopnév(oszlop) + "1:" + MyE.Oszlopnév(oszlop + 3) + "1");
                    MyE.Kiir("Összesen", MyE.Oszlopnév(oszlop) + "1");

                    MyE.Egyesít("J1", MyE.Oszlopnév(oszlop) + "2:" + MyE.Oszlopnév(oszlop + 3) + "2");
                    MyE.Kiir("Összesen", MyE.Oszlopnév(oszlop) + "2");

                    MyE.Kiir("Előírt", MyE.Oszlopnév(oszlop) + "3");
                    MyE.Kiir("Megfelelő", MyE.Oszlopnév(oszlop + 1) + "3");
                    MyE.Kiir("Nem Megfelelő", MyE.Oszlopnév(oszlop + 2) + "3");
                    MyE.Kiir("Kötbér", MyE.Oszlopnév(oszlop + 3) + "3");

                    MyE.Oszlopszélesség("J1", MyE.Oszlopnév(oszlop) + ":" + MyE.Oszlopnév(oszlop + 11), 10);
                    MyE.Sortörésseltöbbsorba("3:3");

                    MyE.Rácsoz(MyE.Oszlopnév(oszlop) + "1:" + MyE.Oszlopnév(oszlop + 3) + "3");
                    MyE.Rácsoz(MyE.Oszlopnév(oszlop) + "4:" + MyE.Oszlopnév(oszlop + 3) + (hónapnap + 3).ToString());
                    MyE.Rácsoz(MyE.Oszlopnév(oszlop) + (hónapnap + 4).ToString() + ":" + MyE.Oszlopnév(oszlop + 3) + (hónapnap + 4).ToString());
                    string képlet = "=RC[-4]";
                    int oszlopsegéd = 16;
                    while (oszlop > oszlopsegéd)
                    {
                        képlet += "+RC[-" + oszlopsegéd + "]";
                        oszlopsegéd += 12;
                    }
                    // összesítések
                    for (int napi = 4; napi <= hónapnap + 4; napi++)
                    {
                        for (int alma = 0; alma <= 3; alma++)
                            MyE.Kiir(képlet, MyE.Oszlopnév(oszlop + alma) + napi);
                    }
                }
                MyE.Oszlopszélesség("J1", "a:a", 10);

                MyE.Kiir("Összesen", "a" + (hónapnap + 4).ToString());
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void J2_excel(string munkalap)
        {
            try
            {
                MyE.Munkalap_aktív(munkalap);

                AdatokKötbér = KézKötbér.Lista_Adat();
                Adat_Jármű_Takarítás_Kötbér AdatKötbér = (from a in AdatokKötbér
                                                          where a.Takarítási_fajta == munkalap
                                                          select a).FirstOrDefault();

                double NemMegfelel = AdatKötbér.NemMegfelel.ToÉrt_Double();
                double Póthatáridő = AdatKötbér.Póthatáridő.ToÉrt_Double();

                // Dátum kiírása
                MyE.Kiir("Dátum", "a3");
                int i;
                int hónapnap = DateTime.DaysInMonth(ListaDátum.Value.Year, ListaDátum.Value.Month);
                DateTime hónaputolsónapja = new DateTime(ListaDátum.Value.Year, ListaDátum.Value.Month, hónapnap);

                DateTime ideig;
                for (int j = 1; j <= hónapnap; j++)
                {
                    ideig = new DateTime(ListaDátum.Value.Year, ListaDátum.Value.Month, j);
                    MyE.Kiir(ideig.ToString("yyyy.MM.dd"), "a" + (j + 3));
                }

                MyE.Kiir("Típus", "a1");
                MyE.Rácsoz("a1:a" + (hónapnap + 4));

                // Típusszámnak megfelelően elkészítjük a fejlécet
                ideig = new DateTime(ListaDátum.Value.Year, ListaDátum.Value.Month, 1);

                int oszlop = 2;
                string típus = "";
                int volt = 0;
                double mennyi;
                Holtart.Be(hónapnap + 1);
                AdatokTelj = KézTakarításTelj.Lista_Adatok(Cmbtelephely.Text.Trim(), ListaDátum.Value.Year);

                AdatokTelj = (from a in AdatokTelj
                              where a.Dátum >= ideig
                              && a.Dátum <= hónaputolsónapja
                              && a.Takarítási_fajta == munkalap
                              orderby a.Típus, a.Dátum, a.Napszak
                              select a).ToList();


                foreach (Adat_Jármű_Takarítás_Teljesítés rekord in AdatokTelj)
                {
                    i = rekord.Dátum.Day;

                    if (típus != rekord.Típus.Trim())
                    {
                        if (!string.IsNullOrEmpty(típus))
                            oszlop += 12;
                        volt += 1;
                        típus = rekord.Típus.Trim();
                        // fejléc elkészítése

                        MyE.Egyesít(munkalap, MyE.Oszlopnév(oszlop) + "1:" + MyE.Oszlopnév(oszlop + 11) + "1");
                        MyE.Kiir(típus, MyE.Oszlopnév(oszlop) + "1");

                        MyE.Egyesít(munkalap, MyE.Oszlopnév(oszlop) + "2:" + MyE.Oszlopnév(oszlop + 3) + "2");
                        MyE.Kiir("Nappal", MyE.Oszlopnév(oszlop) + "2");
                        MyE.Egyesít(munkalap, MyE.Oszlopnév(oszlop + 4) + "2:" + MyE.Oszlopnév(oszlop + 7) + "2");
                        MyE.Kiir("Éjszaka", MyE.Oszlopnév(oszlop + 4) + "2");
                        MyE.Egyesít(munkalap, MyE.Oszlopnév(oszlop + 8) + "2:" + MyE.Oszlopnév(oszlop + 11) + "2");
                        MyE.Kiir("Összesen", MyE.Oszlopnév(oszlop + 8) + "2");

                        MyE.Kiir("Előírt", MyE.Oszlopnév(oszlop) + "3");
                        MyE.Kiir("Megfelelő", MyE.Oszlopnév(oszlop + 1) + "3");
                        MyE.Kiir("Nem Megfelelő", MyE.Oszlopnév(oszlop + 2) + "3");
                        MyE.Kiir("Kötbér", MyE.Oszlopnév(oszlop + 3) + "3");

                        MyE.Kiir("Előírt", MyE.Oszlopnév(oszlop + 4) + "3");
                        MyE.Kiir("Megfelelő", MyE.Oszlopnév(oszlop + 5) + "3");
                        MyE.Kiir("Nem Megfelelő", MyE.Oszlopnév(oszlop + 6) + "3");
                        MyE.Kiir("Kötbér", MyE.Oszlopnév(oszlop + 7) + "3");

                        MyE.Kiir("Előírt", MyE.Oszlopnév(oszlop + 8) + "3");
                        MyE.Kiir("Megfelelő", MyE.Oszlopnév(oszlop + 9) + "3");
                        MyE.Kiir("Nem Megfelelő", MyE.Oszlopnév(oszlop + 10) + "3");
                        MyE.Kiir("Kötbér", MyE.Oszlopnév(oszlop + 11) + "3");

                        MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(oszlop) + ":" + MyE.Oszlopnév(oszlop + 11), 10);
                        MyE.Sortörésseltöbbsorba("3:3");

                        MyE.Rácsoz(MyE.Oszlopnév(oszlop) + "1:" + MyE.Oszlopnév(oszlop + 11) + "3");
                        MyE.Rácsoz(MyE.Oszlopnév(oszlop) + "4:" + MyE.Oszlopnév(oszlop + 11) + (hónapnap + 3));
                        MyE.Rácsoz(MyE.Oszlopnév(oszlop) + (hónapnap + 4) + ":" + MyE.Oszlopnév(oszlop + 11) + (hónapnap + 4));
                        for (int alma = 0; alma <= 11; alma++)
                            MyE.Kiir("=SUM(R[-" + hónapnap + "]C:R[-1]C)", MyE.Oszlopnév(oszlop + alma) + (hónapnap + 4));
                        for (int alma = 0, loopTo1 = hónapnap; alma <= loopTo1; alma++)
                        {
                            MyE.Kiir("=RC[-8]+RC[-4]", MyE.Oszlopnév(oszlop + 8) + (alma + 4));
                            MyE.Kiir("=RC[-8]+RC[-4]", MyE.Oszlopnév(oszlop + 9) + (alma + 4));
                            MyE.Kiir("=RC[-8]+RC[-4]", MyE.Oszlopnév(oszlop + 10) + (alma + 4));
                            MyE.Kiir("=RC[-8]+RC[-4]", MyE.Oszlopnév(oszlop + 11) + (alma + 4));
                        }

                        MyE.Vastagkeret(MyE.Oszlopnév(oszlop) + "2:" + MyE.Oszlopnév(oszlop + 3) + (hónapnap + 4));
                        MyE.Vastagkeret(MyE.Oszlopnév(oszlop + 4) + "2:" + MyE.Oszlopnév(oszlop + 7) + (hónapnap + 4));
                        MyE.Vastagkeret(MyE.Oszlopnév(oszlop + 8) + "2:" + MyE.Oszlopnév(oszlop + 11) + (hónapnap + 4));
                    }

                    int nap = rekord.Dátum.Day;
                    // kiíjuk az értéket
                    if (rekord.Napszak == 1)

                    {
                        // ha nappal
                        // első alkalommal kiírjuk a 0

                        if (MyE.Beolvas(MyE.Oszlopnév(oszlop + 1) + (nap + 3)) == "_" && MyE.Beolvas(MyE.Oszlopnév(oszlop + 2) + (nap + 3)) == "_")
                        {
                            MyE.Kiir("0", MyE.Oszlopnév(oszlop + 1) + (nap + 3));
                            MyE.Kiir("0", MyE.Oszlopnév(oszlop + 2) + (nap + 3));
                            MyE.Kiir("0", MyE.Oszlopnév(oszlop + 3) + (nap + 3));
                            MyE.Kiir("=RC[1]+RC[2]", MyE.Oszlopnév(oszlop) + (nap + 3));
                        }


                        switch (rekord.Státus)
                        {
                            case 1:
                                {
                                    // ha megfelelő
                                    if (MyE.Beolvas(MyE.Oszlopnév(oszlop + 1) + (nap + 3)) == "")
                                        mennyi = 0;
                                    else
                                        mennyi = double.Parse(MyE.Beolvas(MyE.Oszlopnév(oszlop + 1) + (nap + 3)));

                                    MyE.Kiir((mennyi + 1).ToString(), MyE.Oszlopnév(oszlop + 1) + (nap + 3));
                                    break;
                                }
                            case 2:
                                {
                                    // ha nem megfelelő
                                    if (MyE.Beolvas(MyE.Oszlopnév(oszlop + 2) + (nap + 3)).Trim() == "")
                                        mennyi = 0;
                                    else
                                        mennyi = double.Parse(MyE.Beolvas(MyE.Oszlopnév(oszlop + 2) + (nap + 3)));

                                    MyE.Kiir((mennyi + 1).ToString(), MyE.Oszlopnév(oszlop + 2) + (nap + 3));

                                    // kötbér
                                    if (MyE.Beolvas(MyE.Oszlopnév(oszlop + 3) + (nap + 3)) == "")
                                        mennyi = 0;
                                    else
                                        mennyi = double.Parse(MyE.Beolvas(MyE.Oszlopnév(oszlop + 3) + (nap + 3)));

                                    // hozzáadjuk a nem megfelelőséget
                                    mennyi += NemMegfelel;
                                    if (Póthatáridő != 0)
                                    {
                                        // megvizsgáljuk, hogy kell-e
                                        if (rekord.Pótdátum == true)
                                            mennyi += Póthatáridő;
                                    }
                                    MyE.Kiir(mennyi.ToString(), MyE.Oszlopnév(oszlop + 3) + (nap + 3));
                                    break;
                                }

                            case 3:
                                {
                                    // törölt
                                    break;
                                }

                        }
                    }

                    else
                    {
                        // ha éjszaka

                        if (MyE.Beolvas(MyE.Oszlopnév(oszlop + 5) + (nap + 3)) == "_" && MyE.Beolvas(MyE.Oszlopnév(oszlop + 6) + (nap + 3)) == "_")
                        {
                            MyE.Kiir("0", MyE.Oszlopnév(oszlop + 5) + (nap + 3));
                            MyE.Kiir("0", MyE.Oszlopnév(oszlop + 6) + (nap + 3));
                            MyE.Kiir("0", MyE.Oszlopnév(oszlop + 7) + (nap + 3));
                            MyE.Kiir("=RC[1]+RC[2]", MyE.Oszlopnév(oszlop + 4) + (nap + 3));
                        }


                        switch (rekord.Státus)
                        {
                            case 1:
                                {

                                    // ha megfelelő
                                    if (!double.TryParse(MyE.Beolvas(MyE.Oszlopnév(oszlop + 5) + (nap + 3)), out mennyi))
                                        mennyi = 0d;

                                    MyE.Kiir((mennyi + 1).ToString(), MyE.Oszlopnév(oszlop + 5) + (nap + 3));
                                    break;
                                }
                            case 2:
                                {
                                    // ha nem megfelelő
                                    if (!double.TryParse(MyE.Beolvas(MyE.Oszlopnév(oszlop + 6) + (nap + 3)), out mennyi))
                                        mennyi = 0;
                                    MyE.Kiir((mennyi + 1).ToString(), MyE.Oszlopnév(oszlop + 6) + (nap + 3));

                                    // kötbér
                                    if (!double.TryParse(MyE.Beolvas(MyE.Oszlopnév(oszlop + 7) + (nap + 3)), out mennyi))
                                        mennyi = 0;

                                    // hozzáadjuk a nem megfelelőséget
                                    mennyi += NemMegfelel;
                                    if (Póthatáridő != 0)
                                    {
                                        // megvizsgáljuk, hogy kell-e
                                        if (rekord.Pótdátum == true)
                                            mennyi += Póthatáridő;
                                    }
                                    MyE.Kiir(mennyi.ToString(), MyE.Oszlopnév(oszlop + 7) + (nap + 3));
                                    break;
                                }

                            case 3:
                                { // törölt
                                    break;
                                }
                        }
                    }

                    Holtart.Lép();
                }

                // ha több csoport volt
                if (volt > 1)
                {
                    oszlop += 12;
                    // fejléc elkészítése
                    MyE.Egyesít(munkalap, MyE.Oszlopnév(oszlop) + "1:" + MyE.Oszlopnév(oszlop + 3) + "1");
                    MyE.Kiir("Összesen", MyE.Oszlopnév(oszlop) + "1");

                    MyE.Egyesít(munkalap, MyE.Oszlopnév(oszlop) + "2:" + MyE.Oszlopnév(oszlop + 3) + "2");
                    MyE.Kiir("Összesen", MyE.Oszlopnév(oszlop) + "2");


                    MyE.Kiir("Előírt", MyE.Oszlopnév(oszlop) + "3");
                    MyE.Kiir("Megfelelő", MyE.Oszlopnév(oszlop + 1) + "3");
                    MyE.Kiir("Nem Megfelelő", MyE.Oszlopnév(oszlop + 2) + "3");
                    MyE.Kiir("Kötbér", MyE.Oszlopnév(oszlop + 3) + "3");

                    MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(oszlop) + ":" + MyE.Oszlopnév(oszlop + 11), 10);

                    MyE.Sortörésseltöbbsorba("3:3");

                    MyE.Rácsoz(MyE.Oszlopnév(oszlop) + "1:" + MyE.Oszlopnév(oszlop + 3) + "3");
                    MyE.Rácsoz(MyE.Oszlopnév(oszlop) + "4:" + MyE.Oszlopnév(oszlop + 3) + (hónapnap + 3));
                    MyE.Rácsoz(MyE.Oszlopnév(oszlop) + (hónapnap + 4) + ":" + MyE.Oszlopnév(oszlop + 3) + (hónapnap + 4));
                    string képlet = "=RC[-4]";
                    int oszlopsegéd = 16;
                    while (oszlop > oszlopsegéd)
                    {
                        képlet += "+RC[-" + oszlopsegéd + "]";
                        oszlopsegéd += 12;
                    }
                    // összesítések
                    for (int nap = 4; nap <= hónapnap + 4; nap++)
                    {
                        for (int alma = 0; alma <= 3; alma++)
                            MyE.Kiir(képlet, MyE.Oszlopnév(oszlop + alma) + nap);
                    }
                }

                MyE.Oszlopszélesség(munkalap, "a:a", 10);

                MyE.Sortörésseltöbbsorba("2:2");

                MyE.Kiir("Összesen", "a" + (hónapnap + 4));

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Összesítő_eredmény()
        {
            try
            {
                string munkalap = "Összesítő_eredmény";
                MyE.Munkalap_aktív("Összesítő_eredmény");


                MyE.Egyesít(munkalap, "a1:d1");

                // jelenléti ív
                AdatokJelen = KézJelen.Lista_Adatok(Cmbtelephely.Text.Trim());

                AdatokJelen = (from a in AdatokJelen
                               orderby a.Id
                               select a).ToList();

                Adat_Kiegészítő_Jelenlétiív AdatJelen = (from a in AdatokJelen
                                                         select a).FirstOrDefault();

                if (AdatJelen != null)
                {
                    MyE.Kiir(AdatJelen.Szervezet.Trim(), "a1");
                }
                MyE.Oszlopszélesség(munkalap, "a:d", 25);
                MyE.Egyesít(munkalap, "a4:d4");

                string szöveg = ListaDátum.Value.ToString("yyyy. MMMM") + ". havi ";
                szöveg += "Takarítás összesítőlap";
                MyE.Kiir(szöveg, "A4");
                MyE.Betű("A4", 18);
                MyE.Betű("A4", false, true, true);

                MyE.Kiir("Takarítás fajtája", "A6");
                MyE.Kiir("J1 nappal", "A7");
                MyE.Kiir("J1 éjszaka", "A8");
                MyE.Kiir("M.e.", "b6");
                MyE.Kiir("db", "b7");
                MyE.Kiir("db", "b8");
                int sor = 8;
                int eleje = 8;
                int vége = 0;
                for (int ii = 0; ii <= Lekérdezés_Kategória.Items.Count - 1; ii++)
                {
                    sor += 1;
                    Lekérdezés_Kategória.Text = Lekérdezés_Kategória.Items[ii].ToString();
                    MyE.Kiir(Lekérdezés_Kategória.Text.Trim() + " nappal", "a" + sor);
                    if (Lekérdezés_Kategória.Text.Trim() == "Graffiti" | Lekérdezés_Kategória.Text.Trim() == "Eseti")
                    {
                        MyE.Kiir("nm", "b" + sor.ToString());
                    }
                    else
                    {
                        MyE.Kiir("db", "b" + sor.ToString());
                    }
                    MyE.Kiir("0", "c" + sor.ToString());

                    sor += 1;
                    Lekérdezés_Kategória.Text = Lekérdezés_Kategória.Items[ii].ToString();
                    MyE.Kiir(Lekérdezés_Kategória.Text.Trim() + " éjszaka", "a" + sor);
                    if (Lekérdezés_Kategória.Text.Trim() == "Graffiti" | Lekérdezés_Kategória.Text.Trim() == "Eseti" | Lekérdezés_Kategória.Text.Trim() == "Fertőtlenítés")
                    {
                        MyE.Kiir("nm", "b" + sor.ToString());
                    }
                    else
                    {
                        MyE.Kiir("db", "b" + sor.ToString());
                    }
                    MyE.Kiir("0", "c" + sor.ToString());
                }
                vége = sor;
                int melyiksor = 0;

                int hónapnap = DateTime.DaysInMonth(ListaDátum.Value.Year, ListaDátum.Value.Month);
                DateTime hónaputolsónapja = new DateTime(ListaDátum.Value.Year, ListaDátum.Value.Month, hónapnap);



                string típus = "";
                int oszlop = 2;
                double mennyi = 0d;
                int volt = 0;

                for (int i = 0; i <= típusnév.Count - 1; i++)
                {
                    if (string.IsNullOrEmpty(típusnév[i]))
                        break;
                    // -----------J1----------------
                    DateTime ideig = new DateTime(ListaDátum.Value.Year, ListaDátum.Value.Month, 1);

                    AdatokJ1.Clear();
                    AdatokJ1 = KézJ1.Lista_Adat(Cmbtelephely.Text.Trim(), ListaDátum.Value.Year);

                    AdatokJ1 = (from a in AdatokJ1
                                where a.Dátum >= ideig
                                && a.Dátum <= hónaputolsónapja
                                && a.Típus == típusnév[i]
                                orderby a.Típus, a.Dátum, a.Napszak
                                select a).ToList();

                    foreach (Adat_Jármű_Takarítás_J1 rekord in AdatokJ1)
                    {
                        if (típus.Trim() != típusnév[i].ToStrTrim())
                        {
                            oszlop += 1;
                            típus = típusnév[i].ToStrTrim();
                            for (int j = 7; j <= 16; j++)
                                MyE.Kiir(0.ToString(), MyE.Oszlopnév(oszlop) + j.ToString());
                        }
                        if (típus.Trim() == rekord.Típus.Trim())
                        {
                            if (rekord.Napszak == 1)
                            {
                                mennyi += rekord.J1megfelelő;
                            }
                            else
                            {
                                volt += rekord.J1megfelelő;
                            }
                            MyE.Kiir(típusnév[i].ToStrTrim(), MyE.Oszlopnév(oszlop) + "6");
                            MyE.Kiir(mennyi.ToString(), MyE.Oszlopnév(oszlop) + "7");
                            MyE.Kiir(volt.ToString(), MyE.Oszlopnév(oszlop) + "8");
                        }
                    }
                    mennyi = 0d;
                    volt = 0;

                    // -----------Minden más----------------
                    for (int ij = 0; ij <= Lekérdezés_Kategória.Items.Count - 1; ij++)
                    {

                        if (Lekérdezés_Kategória.Items[ij].ToString().Substring(0, 1) == "J")
                        {
                            // J takarítások lekérdezése
                            Lekérdezés_Kategória.Text = Lekérdezés_Kategória.Items[ij].ToStrTrim();
                            for (int k = eleje; k <= vége; k++)
                            {
                                if (MyE.Beolvas("a" + k).Contains(Lekérdezés_Kategória.Text.Trim()))
                                {
                                    melyiksor = k;
                                    break;
                                }

                            }
                            ideig = new DateTime(ListaDátum.Value.Year, ListaDátum.Value.Month, 1);
                            AdatokTelj.Clear();
                            AdatokTelj = KézTakarításTelj.Lista_Adatok(Cmbtelephely.Text.Trim(), ListaDátum.Value.Year);
                            AdatokTelj = (from a in AdatokTelj
                                          where a.Dátum >= ideig
                                          && a.Dátum <= hónaputolsónapja
                                          && a.Típus == típusnév[i]
                                          && a.Takarítási_fajta == Lekérdezés_Kategória.Text
                                          && a.Státus == 1
                                          orderby a.Típus, a.Dátum, a.Napszak
                                          select a).ToList();

                            foreach (Adat_Jármű_Takarítás_Teljesítés rekord in AdatokTelj)
                            {
                                if (típus.Trim() != típusnév[i].ToStrTrim())
                                {
                                    oszlop += 1;
                                    típus = típusnév[i].ToStrTrim();
                                    for (int j = 7; j <= 16; j++)
                                        MyE.Kiir("0", MyE.Oszlopnév(oszlop) + j);
                                }
                                if (típus.Trim() == rekord.Típus.Trim())
                                {
                                    if (rekord.Napszak == 1 && rekord.Státus == 1)
                                    {
                                        mennyi += 1d;
                                    }
                                    if (rekord.Napszak == 2 && rekord.Státus == 1)
                                    {
                                        volt += 1;
                                    }
                                    MyE.Kiir(típusnév[i].ToStrTrim(), MyE.Oszlopnév(oszlop) + "6");
                                    MyE.Kiir(mennyi.ToString(), MyE.Oszlopnév(oszlop) + melyiksor.ToString());
                                    MyE.Kiir(volt.ToString(), MyE.Oszlopnév(oszlop) + (melyiksor + 1).ToString());
                                }
                            }
                            mennyi = 0;
                            volt = 0;
                        }
                        else
                        {
                            // Opció 
                            // J takarítások lekérdezése
                            Lekérdezés_Kategória.Text = Lekérdezés_Kategória.Items[ij].ToStrTrim();
                            for (int k = eleje; k <= vége; k++)
                            {
                                if (MyE.Beolvas("a" + k).Contains(Lekérdezés_Kategória.Text.Trim()))
                                {
                                    melyiksor = k;
                                    break;
                                }

                            }
                            double nappal = 0d;
                            double éjszaka = 0d;

                            ideig = new DateTime(ListaDátum.Value.Year, ListaDátum.Value.Month, 1);


                            AdatokTelj.Clear();
                            AdatokTelj = KézTakarításTelj.Lista_Adatok(Cmbtelephely.Text.Trim(), ListaDátum.Value.Year);
                            AdatokTelj = (from a in AdatokTelj
                                          where a.Dátum >= ideig
                                          && a.Dátum <= hónaputolsónapja
                                          && a.Típus == típusnév[i]
                                          && a.Takarítási_fajta == Lekérdezés_Kategória.Text
                                          && a.Státus == 1
                                          orderby a.Típus, a.Dátum, a.Napszak
                                          select a).ToList();

                            foreach (Adat_Jármű_Takarítás_Teljesítés rekord in AdatokTelj)
                            {
                                if (típus.Trim() != típusnév[i].ToStrTrim())
                                {
                                    oszlop += 1;
                                    típus = típusnév[i].ToStrTrim();
                                    for (int j = 7; j <= 16; j++)
                                        MyE.Kiir("0", MyE.Oszlopnév(oszlop) + i);
                                }
                                if (típus.Trim() == rekord.Típus.ToStrTrim())
                                {
                                    if (rekord.Napszak == 1)
                                    {
                                        nappal += rekord.Mérték;
                                    }
                                    if (rekord.Napszak == 2)
                                    {
                                        éjszaka += rekord.Mérték;
                                    }
                                    MyE.Kiir(típusnév[i].ToStrTrim(), MyE.Oszlopnév(oszlop) + "6");
                                    MyE.Kiir(nappal.ToString().Replace(",", "."), MyE.Oszlopnév(oszlop) + melyiksor);
                                    MyE.Kiir(éjszaka.ToString().Replace(",", "."), MyE.Oszlopnév(oszlop) + (melyiksor + 1));
                                }
                            }
                            nappal = 0d;
                            éjszaka = 0d;
                        }
                    }
                }

                MyE.Rácsoz("A6:" + MyE.Oszlopnév(oszlop) + vége.ToString());
                MyE.Kiir("Budapest, " + DateTime.Today.ToString("yyyy.MM.dd"), "a" + (vége + 3));
                MyE.Kiir("Vállalkozó", "a" + (vége + 8));
                MyE.Igazít_vízszintes("a" + (vége + 8), "közép");
                MyE.Pontvonal("a" + (vége + 8));

                MyE.Kiir("BKV Zrt.", "D" + (vége + 8));
                MyE.Igazít_vízszintes("D" + (vége + 8), "közép");
                MyE.Pontvonal("D" + (vége + 8));

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

        private void Összesítő_Minden()
        {
            try
            {
                int hónapnap = DateTime.DaysInMonth(ListaDátum.Value.Year, ListaDátum.Value.Month);
                DateTime hónaputolsónapja = new DateTime(ListaDátum.Value.Year, ListaDátum.Value.Month, hónapnap);

                string munkalap = "Összesítő_minden";

                MyE.Munkalap_aktív("Összesítő_minden");
                MyE.Egyesít(munkalap, "a1:g1");
                MyE.Sortörésseltöbbsorba_egyesített("A1:G1");

                // kiírjuk a kocsiszín azonosítóját
                // jelenléti ív

                AdatokJelen = KézJelen.Lista_Adatok(Cmbtelephely.Text.Trim());

                AdatokJelen = (from a in AdatokJelen
                               orderby a.Id
                               select a).ToList();

                Adat_Kiegészítő_Jelenlétiív AdatJelen = (from a in AdatokJelen
                                                         select a).FirstOrDefault();



                if (AdatJelen != null) MyE.Kiir(AdatJelen.Szervezet.Trim(), "a1");

                MyE.Oszlopszélesség(munkalap, "a:a", 15);
                MyE.Oszlopszélesség(munkalap, "b:b", 8);
                MyE.Egyesít(munkalap, "a4:g4");
                string szöveg = ListaDátum.Value.ToString("yyyy. MMMM") + ". havi ";
                szöveg += "Takarítás összesítőlap";
                MyE.Kiir(szöveg, "A4");
                MyE.Betű("A4", 18);
                MyE.Betű("A4", false, true, true);

                // kiírjuk a takarítási fajtákat egymás alá tábla bal oldala
                MyE.Kiir("Takarítás fajtája", "A7");
                MyE.Kiir("Napszak", "B7");

                int sor;
                sor = 8;
                MyE.Kiir("J1", "A" + sor);
                MyE.Kiir("Nappal", "B" + sor);
                sor += 1;
                MyE.Kiir("J1", "A" + sor);
                MyE.Kiir("Éjszaka", "B" + sor);

                for (int i = 0; i <= Lekérdezés_Kategória.Items.Count - 1; i++)
                {
                    sor += 1;
                    MyE.Kiir(Lekérdezés_Kategória.Items[i].ToString(), "A" + sor);
                    MyE.Kiir("Nappal", "B" + sor);
                    sor += 1;
                    MyE.Kiir(Lekérdezés_Kategória.Items[i].ToString(), "A" + sor);
                    MyE.Kiir("Éjszaka", "B" + sor);
                }
                sor += 1;
                MyE.Kiir("Összesen", "A" + sor);
                MyE.Rácsoz("a7:b" + sor);
                MyE.Vastagkeret("a7:b" + sor);

                // Típusok száma
                int típus = 0;
                int újtípus = 0;
                int oszlop = 2;

                // Munkalap nevek feltöltéséhez megnézzük a J1 oldalon a villamos típusokat.
                MyE.Munkalap_aktív("J1");
                típusnév.Clear();

                while (MyE.Beolvas(MyE.Oszlopnév(oszlop) + "1").Trim() != "_")
                {
                    if (MyE.Beolvas(MyE.Oszlopnév(oszlop) + "1").Trim() != "Összesen")
                    {
                        típus += 1;
                        típusnév.Add(MyE.Beolvas(MyE.Oszlopnév(oszlop) + "1").Trim());
                    }
                    oszlop += 12;
                }

                // megnézzük, hogy a többi lapon van-e másik típus ami eddig nem volt

                for (int i = 0; i < Lekérdezés_Kategória.Items.Count - 1; i++)
                {
                    MyE.Munkalap_aktív(Lekérdezés_Kategória.Items[i].ToString());

                    oszlop = 2;
                    bool voltilyen = false;
                    while (MyE.Beolvas(MyE.Oszlopnév(oszlop) + "1").Trim() != "_")
                    {
                        if (MyE.Beolvas(MyE.Oszlopnév(oszlop) + "1").Trim() != "Összesen")
                        {
                            // meg kell vizsgálni mindegyik lapot a típusok neveit
                            if (típusnév.Contains(MyE.Beolvas(MyE.Oszlopnév(oszlop) + "1").Trim()))
                                voltilyen = true;
                            // ha nem volt még akkor rögzítjük
                            if (voltilyen == false)
                            {
                                típus += 1;
                                típusnév.Add(MyE.Beolvas(MyE.Oszlopnév(oszlop) + "1"));
                            }

                        }
                        oszlop += 12;
                        voltilyen = false;
                    }
                    if (újtípus > típus)
                        típus = újtípus;
                }

                // típusonként elkészítjük a fejlécet

                oszlop = 3;
                string takfajta;
                string napszak;
                int melyikoszlop;
                double megfelelő;
                double kötbér;

                AdatokÁrak = KézÁr.Lista_Adatok();
                Adat_Jármű_Takarítás_Árak AdatÁrak;
                int napszakInt;
                munkalap = "Összesítő_minden";
                for (int i = 0; i < típusnév.Count; i++)
                {

                    MyE.Munkalap_aktív(munkalap);

                    // összesítések Fizetendő
                    MyE.Kiir("=SUM(R[-" + (sor - 8) + "]C:R[-1]C)", MyE.Oszlopnév(oszlop + 2) + sor);
                    MyE.Betű(MyE.Oszlopnév(oszlop + 2) + sor, "", "#,##0 $");
                    //Kötbér összesen
                    MyE.Kiir("=SUM(R[-" + (sor - 8).ToString() + "]C:R[-1]C)", MyE.Oszlopnév(oszlop + 4) + sor);
                    MyE.Betű(MyE.Oszlopnév(oszlop + 4) + sor, "", "#,##0 $");

                    MyE.Egyesít(munkalap, MyE.Oszlopnév(oszlop) + "6:" + MyE.Oszlopnév(oszlop + 4) + "6");
                    MyE.Kiir(típusnév[i], MyE.Oszlopnév(oszlop) + "6");
                    MyE.Kiir("Megfelelő db", MyE.Oszlopnév(oszlop) + "7");

                    MyE.Kiir("Egység ár", MyE.Oszlopnév(oszlop + 1) + "7");
                    MyE.Kiir("Fizetendő összeg", MyE.Oszlopnév(oszlop + 2) + "7");
                    MyE.Kiir("Kötbér mennyiség", MyE.Oszlopnév(oszlop + 3) + "7");
                    MyE.Kiir("Kötbér összeg", MyE.Oszlopnév(oszlop + 4) + "7");

                    MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(oszlop) + ":" + MyE.Oszlopnév(oszlop + 4), 13);
                    MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(oszlop + 2) + ":" + MyE.Oszlopnév(oszlop + 3), 17);
                    MyE.Rácsoz(MyE.Oszlopnév(oszlop) + "6:" + MyE.Oszlopnév(oszlop + 4) + sor);
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlop) + "6:" + MyE.Oszlopnév(oszlop + 4) + sor);
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlop) + sor + ":" + MyE.Oszlopnév(oszlop + 4) + sor);

                    // az összesítő sorokat a 60 sorba másoljuk értékként
                    for (int sorb = 8; sorb <= sor - 1; sorb++)
                    {
                        MyE.Munkalap_aktív("Összesítő_minden");
                        takfajta = MyE.Beolvas("a" + sorb).Trim();
                        // átmegyünk a lapra
                        MyE.Munkalap_aktív(takfajta);
                        int utolsóoszlop_ = MyE.Utolsóoszlop(takfajta);
                        string honnan = "A" + (hónapnap + 4) + ":" + MyE.Oszlopnév(utolsóoszlop_) + (hónapnap + 4);
                        string hova = "A60:" + MyE.Oszlopnév(utolsóoszlop_) + "60";
                        MyE.Értékmásol(takfajta, honnan, hova);
                    }

                    // soronként kiírjuk a
                    for (int sorb = 8; sorb <= sor - 1; sorb++)
                    {
                        Holtart.Value = sorb;
                        MyE.Munkalap_aktív("Összesítő_minden");

                        melyikoszlop = 0;
                        takfajta = MyE.Beolvas("a" + sorb);
                        napszak = MyE.Beolvas("b" + sorb);
                        if (takfajta.Substring(0, 1) == "J")
                        {
                            // átmegyünk a lapra
                            MyE.Munkalap_aktív(takfajta);
                            // megkeressük az utolsó oszlopot 
                            int utolsóoszlop = MyE.Utolsóoszlop(takfajta);
                            string típus_név = "_";
                            for (int oszlopkereső = 2; oszlopkereső <= utolsóoszlop; oszlopkereső++)
                            {
                                if (MyE.Beolvas(MyE.Oszlopnév(oszlopkereső) + "1").Trim() != "_")
                                    típus_név = MyE.Beolvas(MyE.Oszlopnév(oszlopkereső) + "1").Trim();

                                if ((típus_név.Trim() == típusnév[i].Trim()) && (MyE.Beolvas(MyE.Oszlopnév(oszlopkereső) + "2").Trim()) == napszak.Trim())
                                {
                                    melyikoszlop = oszlopkereső;
                                    break;
                                }
                            }

                            if (melyikoszlop != 0)
                            {

                                megfelelő = double.Parse(MyE.Beolvas(MyE.Oszlopnév(melyikoszlop + 1) + "60"));
                                kötbér = double.Parse(MyE.Beolvas(MyE.Oszlopnév(melyikoszlop + 3) + "60"));

                                MyE.Munkalap_aktív("Összesítő_minden");
                                // kiírjuk a hivatkozást
                                MyE.Kiir(megfelelő.ToString(), MyE.Oszlopnév(oszlop) + sorb);
                                MyE.Kiir(kötbér.ToString(), MyE.Oszlopnév(oszlop + 3) + sorb);
                                // szorzások
                                MyE.Kiir("=RC[-2]*RC[-1]", MyE.Oszlopnév(oszlop + 2) + sorb);
                                MyE.Betű(MyE.Oszlopnév(oszlop + 2) + sorb, "", "#,##0 $");

                                MyE.Kiir("=RC[-1]*RC[-3]", MyE.Oszlopnév(oszlop + 4) + sorb);
                                MyE.Betű(MyE.Oszlopnév(oszlop + 4) + sorb, "", "#,##0 $");

                                if (napszak == "Nappal") napszakInt = 1; else napszakInt = 2;

                                AdatÁrak = (from a in AdatokÁrak
                                            where a.JárműTípus == típusnév[i].Trim()
                                            && a.Takarítási_fajta == takfajta.Trim()
                                            && a.Napszak == napszakInt
                                            && a.Érv_vég >= ListaDátum.Value
                                            && a.Érv_kezdet <= ListaDátum.Value
                                            select a).FirstOrDefault();
                                if (AdatÁrak != null)
                                {
                                    MyE.Kiir(AdatÁrak.Ár.ToString(), MyE.Oszlopnév(oszlop + 1) + sorb);
                                    MyE.Betű(MyE.Oszlopnév(oszlop + 1) + sorb, "", "#,##0 $");
                                }
                            }
                        }
                        else
                        {
                            // átmegyünk a lapra
                            MyE.Munkalap_aktív(takfajta);

                            // megkeressük az utolsó oszlopot 
                            int utolsóoszlop = MyE.Utolsóoszlop(takfajta);
                            string típus_név = "_";

                            for (int oszlopkereső = 2; oszlopkereső <= utolsóoszlop; oszlopkereső++)
                            {
                                if (MyE.Beolvas(MyE.Oszlopnév(oszlopkereső) + "1").Trim() != "_")
                                    típus_név = MyE.Beolvas(MyE.Oszlopnév(oszlopkereső) + "1").Trim();
                                if (típus_név == típusnév[i].Trim() && MyE.Beolvas(MyE.Oszlopnév(oszlopkereső) + "2").Trim() == napszak.Trim())
                                {
                                    melyikoszlop = oszlopkereső;
                                    break;
                                }
                            }

                            if (melyikoszlop != 0)
                            {
                                megfelelő = double.Parse(MyE.Beolvas(MyE.Oszlopnév(melyikoszlop) + "60"));
                                kötbér = 0;

                                MyE.Munkalap_aktív("Összesítő_minden");

                                // kiírjuk a hivatkozást
                                MyE.Kiir(megfelelő.ToString().Replace(",", "."), MyE.Oszlopnév(oszlop) + sorb);
                                MyE.Kiir(kötbér.ToString().Replace(",", "."), MyE.Oszlopnév(oszlop + 3) + sorb);
                                // szorzások
                                MyE.Kiir("=RC[-2]*RC[-1]", MyE.Oszlopnév(oszlop + 2) + sorb);
                                MyE.Betű(MyE.Oszlopnév(oszlop + 2) + sorb, "", "#,##0 $");


                                MyE.Kiir("=RC[-1]*RC[-3]", MyE.Oszlopnév(oszlop + 4) + sorb);
                                MyE.Betű(MyE.Oszlopnév(oszlop + 4) + sorb, "", "#,##0 $");

                                if (napszak == "Nappal") napszakInt = 1; else napszakInt = 2;

                                AdatÁrak = (from a in AdatokÁrak
                                            where a.JárműTípus == típusnév[i].Trim()
                                            && a.Takarítási_fajta == takfajta.Trim()
                                            && a.Napszak == napszakInt
                                            && a.Érv_vég >= ListaDátum.Value
                                            && a.Érv_kezdet <= ListaDátum.Value
                                            select a).FirstOrDefault();
                                if (AdatÁrak != null)
                                {
                                    MyE.Kiir(AdatÁrak.Ár.ToString(), MyE.Oszlopnév(oszlop + 1) + sorb.ToString());
                                    MyE.Betű(MyE.Oszlopnév(oszlop + 1) + sorb.ToString(), "", "#,##0 $");
                                }
                            }

                        }
                    }
                    // előkészítjük a következő típust
                    oszlop += 6;
                }

                // visszaállítjuk a lapokat
                for (int sorb = 8; sorb <= sor - 1; sorb++)
                {
                    MyE.Munkalap_aktív("Összesítő_minden");
                    takfajta = MyE.Beolvas("a" + sorb.ToString()).Trim();
                    // átmegyünk a lapra
                    MyE.Munkalap_aktív(takfajta);
                    MyE.Aktív_Cella(takfajta, "A1");
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

        private void Lek_kat_feltöltés()
        {
            Lekérdezés_Kategória.Items.Clear();
            Lekérdezés_Kategória.Items.Add("J2");
            Lekérdezés_Kategória.Items.Add("J3");
            Lekérdezés_Kategória.Items.Add("J4");
            Lekérdezés_Kategória.Items.Add("J5");
            Lekérdezés_Kategória.Items.Add("J6");
            Lekérdezés_Kategória.Items.Add("Graffiti");
            Lekérdezés_Kategória.Items.Add("Eseti");
            Lekérdezés_Kategória.Items.Add("Fertőtlenítés");
        }

        private void Lekérdezés_Kategória_Click(object sender, EventArgs e)
        {
            RadioButton4.Checked = true;
        }

        private void Opció_excel(string munkalap)
        {
            try
            {
                MyE.Munkalap_aktív(munkalap);

                AdatokKötbér = KézKötbér.Lista_Adat();
                Adat_Jármű_Takarítás_Kötbér AdatKötbér = (from a in AdatokKötbér
                                                          where a.Takarítási_fajta == munkalap
                                                          select a).FirstOrDefault();

                double NemMegfelel = 0;
                double Póthatáridő = 0;
                if (AdatKötbér != null)
                {
                    NemMegfelel = AdatKötbér.NemMegfelel.ToÉrt_Double();
                    Póthatáridő = AdatKötbér.Póthatáridő.ToÉrt_Double();
                }

                // Dátum kiírása
                MyE.Kiir("Dátum", "a3");

                int hónapnap = DateTime.DaysInMonth(ListaDátum.Value.Year, ListaDátum.Value.Month);
                DateTime hónaputolsónapja = new DateTime(ListaDátum.Value.Year, ListaDátum.Value.Month, hónapnap);

                DateTime ideig;
                for (int j = 1; j <= hónapnap; j++)
                {
                    ideig = new DateTime(ListaDátum.Value.Year, ListaDátum.Value.Month, j);
                    MyE.Kiir(ideig.ToString("yyyy.MM.dd"), "a" + (j + 3));
                }

                MyE.Kiir("Típus", "a1");
                MyE.Rácsoz("a1:a" + (hónapnap + 4).ToString());

                // Típusszámnak megfelelően elkészítjük a fejlécet

                ideig = new DateTime(ListaDátum.Value.Year, ListaDátum.Value.Month, 1);

                int oszlop = 2;
                string típus = "";
                int volt = 0;
                double mennyi;
                Holtart.Be(hónapnap + 1);

                int i = 0;

                AdatokTelj = KézTakarításTelj.Lista_Adatok(Cmbtelephely.Text.Trim(), ListaDátum.Value.Year);

                AdatokTelj = (from a in AdatokTelj
                              where a.Dátum >= ideig
                              && a.Dátum <= hónaputolsónapja
                              && a.Takarítási_fajta == munkalap
                              && a.Státus < 3
                              orderby a.Típus, a.Dátum, a.Napszak
                              select a).ToList();

                foreach (Adat_Jármű_Takarítás_Teljesítés rekord in AdatokTelj)
                {
                    i = rekord.Dátum.Day;

                    if (típus != rekord.Típus.Trim())
                    {
                        if (!string.IsNullOrEmpty(típus))
                            oszlop += 3;
                        volt += 1;
                        típus = rekord.Típus.Trim();
                        // fejléc elkészítése
                        MyE.Egyesít(munkalap, MyE.Oszlopnév(oszlop) + "1:" + MyE.Oszlopnév(oszlop + 2) + "1");
                        MyE.Kiir(típus, MyE.Oszlopnév(oszlop) + "1");

                        MyE.Kiir("Nappal", MyE.Oszlopnév(oszlop) + "2");
                        MyE.Kiir("Éjszaka", MyE.Oszlopnév(oszlop + 1) + "2");
                        MyE.Kiir("Összesen", MyE.Oszlopnév(oszlop + 2) + "2");

                        MyE.Kiir("Felület", MyE.Oszlopnév(oszlop) + "3");
                        MyE.Kiir("Felület", MyE.Oszlopnév(oszlop + 1) + "3");
                        MyE.Kiir("Felület", MyE.Oszlopnév(oszlop + 2) + "3");

                        MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(oszlop) + ":" + MyE.Oszlopnév(oszlop + 2), 10);
                        MyE.Sortörésseltöbbsorba("3:3");

                        // Lenti összesítés 
                        MyE.Rácsoz(MyE.Oszlopnév(oszlop) + "1:" + MyE.Oszlopnév(oszlop + 2) + "3");
                        MyE.Rácsoz(MyE.Oszlopnév(oszlop) + "4:" + MyE.Oszlopnév(oszlop + 2) + (hónapnap + 4));
                        MyE.Kiir("=SUM(R[-" + hónapnap + "]C:R[-1]C)", MyE.Oszlopnév(oszlop) + (hónapnap + 4));
                        MyE.Kiir("=SUM(R[-" + hónapnap + "]C:R[-1]C)", MyE.Oszlopnév(oszlop + 1) + (hónapnap + 4));
                        MyE.Kiir("=SUM(R[-" + hónapnap + "]C:R[-1]C)", MyE.Oszlopnév(oszlop + 2) + (hónapnap + 4));

                        for (int alma = 0; alma <= hónapnap - 1; alma++)
                        {
                            // kinullázzuk
                            MyE.Kiir("0", MyE.Oszlopnév(oszlop) + (alma + 4));
                            MyE.Kiir("0", MyE.Oszlopnév(oszlop + 1) + (alma + 4));
                            MyE.Kiir("=SUM(RC[-2]:RC[-1])", MyE.Oszlopnév(oszlop + 2) + (alma + 4));
                        }

                        MyE.Vastagkeret(MyE.Oszlopnév(oszlop) + "2:" + MyE.Oszlopnév(oszlop + 2) + (hónapnap + 4));
                        MyE.Vastagkeret(MyE.Oszlopnév(oszlop) + (hónapnap + 4) + ":" + MyE.Oszlopnév(oszlop + 2) + (hónapnap + 4));

                    }

                    int nap = rekord.Dátum.Day;
                    int korrektor = 1;
                    // kiíjuk az értéket
                    if (rekord.Napszak == 1) korrektor = 0;         //ha nappal

                    mennyi = double.Parse(MyE.Beolvas(MyE.Oszlopnév(oszlop + korrektor) + (nap + 3)));
                    mennyi += rekord.Mérték;
                    MyE.Kiir(mennyi.ToString().Replace(",", "."), MyE.Oszlopnév(oszlop + korrektor) + (nap + 3));

                    Holtart.Lép();
                }

                MyE.Oszlopszélesség(munkalap, "a:a", 10);
                MyE.Sortörésseltöbbsorba("2:2");
                MyE.Kiir("Összesen", "a" + (hónapnap + 4).ToString());
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (ListaTábla.Rows.Count <= 0)
                    return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Takarítás_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                {
                    fájlexc = SaveFileDialog1.FileName;
                }
                else
                {
                    return;
                }

                MyE.DataGridViewToExcel(fájlexc, ListaTábla);
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

        private void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                ListaTábla.Rows.Clear();
                ListaTábla.Columns.Clear();
                ListaTábla.Refresh();
                ListaTábla.Visible = false;
                ListaTábla.ColumnCount = 6;

                // fejléc elkészítése
                ListaTábla.Columns[0].HeaderText = "Pályaszám";
                ListaTábla.Columns[0].Width = 100;
                ListaTábla.Columns[1].HeaderText = "J2";
                ListaTábla.Columns[1].Width = 100;
                ListaTábla.Columns[2].HeaderText = "J3";
                ListaTábla.Columns[2].Width = 100;
                ListaTábla.Columns[3].HeaderText = "J4";
                ListaTábla.Columns[3].Width = 100;
                ListaTábla.Columns[4].HeaderText = "J5";
                ListaTábla.Columns[4].Width = 100;
                ListaTábla.Columns[5].HeaderText = "J6";
                ListaTábla.Columns[5].Width = 100;


                int sor = 0;
                string előzőpsz = "";
                DateTime előződátum = DateTime.Parse("1900.01.01");
                string előzőtaktípus = "";
                int volt = 0;


                List<Adat_Jármű_Takarítás_Napló> AdatokNapló = KézTakNapló.Lista_Adatok(ListaDátum.Value.Year);

                AdatokNapló = (from a in AdatokNapló
                               where a.Telephely == Cmbtelephely.Text.Trim()
                               && a.Státus == 0
                               orderby a.Azonosító ascending, a.Takarítási_fajta ascending, a.Dátum descending
                               select a).ToList();

                foreach (Adat_Jármű_Takarítás_Napló rekord in AdatokNapló)
                {


                    if (előzőpsz.Trim() != rekord.Azonosító.Trim())
                    {
                        // ha új pályaszám van akkor új sorba írjuk
                        ListaTábla.RowCount++;
                        sor = ListaTábla.RowCount - 1;
                        ListaTábla.Rows[sor].Cells[0].Value = rekord.Azonosító;
                        előzőpsz = rekord.Azonosító.Trim();
                        előződátum = rekord.Dátum;
                        előzőtaktípus = rekord.Takarítási_fajta.Trim();
                        volt = 0;

                    }
                    else if (előzőtaktípus.Trim() == rekord.Takarítási_fajta.Trim())
                    {
                        if (volt == 0) // ha már kiírtunk akkor nem vizsgálunk tovább abban a típusban
                        {
                            // ha egyforma akkor tovább boncolunk hiba
                            if (előződátum.ToString("yyyy.MM.dd") == rekord.Dátum.ToString("yyyy.MM.dd"))
                            {
                            }
                            // ha megegyezik az előző dátummal akkor nem foglalkozunk vele
                            else
                            {
                                // ha különböző akkor kiírjuk a különbözetet
                                int napszám;
                                TimeSpan delta = rekord.Dátum - előződátum;
                                napszám = delta.TotalDays.ToÉrt_Int();
                                switch (előzőtaktípus.Trim())
                                {
                                    case "J2":
                                        {
                                            ListaTábla.Rows[sor].Cells[1].Value = napszám;
                                            break;
                                        }
                                    case "J3":
                                        {
                                            ListaTábla.Rows[sor].Cells[2].Value = napszám;
                                            break;
                                        }
                                    case "J4":
                                        {
                                            ListaTábla.Rows[sor].Cells[3].Value = napszám;
                                            break;
                                        }
                                    case "J5":
                                        {
                                            ListaTábla.Rows[sor].Cells[4].Value = napszám;
                                            break;
                                        }
                                    case "J6":
                                        {
                                            ListaTábla.Rows[sor].Cells[5].Value = napszám;
                                            break;
                                        }
                                }
                                volt = 1;
                            }
                        }
                    }
                    else
                    {
                        // ha új takarítási fajta akkor nullázzuk az adatokat
                        előződátum = rekord.Dátum;
                        előzőtaktípus = rekord.Takarítási_fajta.Trim();
                        volt = 0;
                    }
                }
                ListaTábla.Visible = true;
                ListaTábla.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_vezénylésexcel_Click(object sender, EventArgs e)
        {
            try
            {

                if (Ütemezés_lista.Items.Count == 0) return;

                string fájlexc;
                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Takarítás-{Program.PostásNév.Trim()}-{Dátum.Value:yyyyMMdd}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                {
                    fájlexc = SaveFileDialog1.FileName;
                }
                else
                {
                    return;
                }

                Holtart.Lép();

                MyE.ExcelLétrehozás();

                int sor;
                MyE.Munkalap_betű("Times New Roman CE", 24);

                // oszlop szélességek beállítása
                MyE.Oszlopszélesség("Munka1", "A:A", 3);
                MyE.Oszlopszélesség("Munka1", "C:C", 3);
                MyE.Oszlopszélesség("Munka1", "B:B", 90);

                MyE.Háttérszín("A1:C1", Color.Yellow);


                // Két széle színez
                for (int i = 2; i <= 8; i++)
                {
                    MyE.Háttérszín("A" + i, Color.Yellow);
                    MyE.Háttérszín("C" + i, Color.Yellow);
                }
                Holtart.Lép();

                MyE.Kép_beillesztés("Munka1", "A1", $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\képek\Villamos.png");

                sor = 8;
                MyE.Kiir("Takarítási terv", "b" + sor);
                MyE.Igazít_vízszintes("b" + sor, "közép");

                sor += 1;
                // kiírjuk a dátumot
                MyE.Sormagasság(sor + ":" + sor, 45);


                // két széle sárga
                MyE.Háttérszín("A" + sor, Color.Yellow);
                MyE.Háttérszín("C" + sor, Color.Yellow);
                MyE.Kiir(Dátum.Value.ToString("yyyy.MM.dd"), "b" + sor);
                MyE.Igazít_vízszintes("b" + sor, "közép");
                MyE.Betű("b" + sor, 36);

                Holtart.Be(Ütemezés_lista.Items.Count);

                for (int elemek = 0; elemek < Ütemezés_lista.Items.Count - 1; elemek++)
                {
                    Holtart.Value = elemek;
                    if (Ütemezés_lista.Items[elemek].ToStrTrim() == "")
                    {
                        // üres sornál sort emel
                        sor += 1;
                        MyE.Háttérszín("A" + sor, Color.Yellow);
                        MyE.Háttérszín("C" + sor, Color.Yellow);
                    }
                    else if (Ütemezés_lista.Items[elemek].ToStrTrim().Substring(0, 1) == "J")
                    {
                        // kategória kiírása
                        sor += 1;
                        // ha új takarítási fajta akkor kiírjuk a takarítási fajtát
                        MyE.Sormagasság(sor + ":" + sor, 45);
                        // két széle sárga
                        MyE.Háttérszín("A" + sor, Color.Yellow);
                        MyE.Háttérszín("C" + sor, Color.Yellow);
                        MyE.Kiir(Ütemezés_lista.Items[elemek].ToStrTrim(), "b" + sor);
                        MyE.Igazít_vízszintes("b" + sor, "közép");
                        MyE.Betű("b" + sor, 36);
                    }

                    else
                    {
                        // ütemezett kocsik
                        sor += 1;
                        string kiírandó = Ütemezés_lista.Items[elemek].ToStrTrim();
                        if (kiírandó.Substring(kiírandó.Length - 1, 1) == "-")
                        {
                            MyE.Kiir(kiírandó.Substring(0, kiírandó.Length - 1), "b" + sor.ToString());
                        }
                        else
                        {
                            MyE.Kiir(kiírandó, "b" + sor.ToString());
                        }
                        MyE.Igazít_vízszintes("b" + sor, "közép");
                        MyE.Háttérszín("A" + sor, Color.Yellow);
                        MyE.Háttérszín("C" + sor, Color.Yellow);
                    }
                }

                // vége sárga
                sor += 1;
                MyE.Háttérszín("A" + sor + ":C" + sor, Color.Yellow);
                // nyomtatási beállítások
                MyE.NyomtatásiTerület_részletes("Munka1", "A1:C" + sor, "", "", true);
                MyE.Aktív_Cella("Munka1", "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();

                Holtart.Ki();
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

        private async void TIG_Készítés_Click(object sender, EventArgs e)
        {
            try
            {
                Holtart.Be();
                DateTime Eleje = DateTime.Now;
                Telephely_ = Cmbtelephely.Text.Trim();
                Dátum_ = ListaDátum.Value;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Jármű Takarítási teljesítési igazolás készítés",
                    FileName = $"Jármű_TIG_{ListaDátum.Value.Year}_év_{ListaDátum.Value:MMMM}_hó_{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexcel_ = SaveFileDialog1.FileName;
                else
                    return;

                timer1.Enabled = true;
                Takarítás_teljesítés_Igazolás Fájl = new Takarítás_teljesítés_Igazolás(Dátum_, false, Telephely_);
                await Task.Run(() => Fájl.ExcelJárműTábla(fájlexcel_));

                timer1.Enabled = false;
                Holtart.Ki();
                DateTime Vége = DateTime.Now;
                MessageBox.Show($"A feladat {Vége - Eleje} idő alatt végrehajtásra került.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (HibásBevittAdat ex)
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


        #region Gépi Mosás Lapfül
        private void Gepi_palyaszam_feltoltes()
        {
            Gepi_pályaszám.Items.Clear();
            List<Adat_Jármű> AdatokÖ = KézJármű.Lista_Adatok("Főmérnökség");
            AdatokÖ = (from a in AdatokÖ
                       where a.Törölt == false
                       orderby a.Azonosító
                       select a).ToList();

            if (Cmbtelephely.Text.Trim() != "Főmérnökség" || Program.Postás_Vezér)
                AdatokÖ = AdatokÖ.Where(a => a.Üzem == Cmbtelephely.Text.Trim()).ToList();

            foreach (Adat_Jármű elem in AdatokÖ)
                Gepi_pályaszám.Items.Add(elem.Azonosító);
            Gepi_pályaszám.Refresh();
        }

        private void Telephelyek()
        {
            List<Adat_kiegészítő_telephely> Adatok = Kéztelephely.Lista_Adatok();
            Tel_TB.Items.Clear();
            foreach (Adat_kiegészítő_telephely Elem in Adatok)
                Tel_TB.Items.Add(Elem.Telephelynév);
            Tel_TB.Refresh();
        }

        private void Gepi_lista(object sender, EventArgs e)
        {
            if (Rögzítések.Checked)
                Gepi_lista_Napló();
            else
                Gepi_lista_Kocsik();

            TöröltekKiemelése();
        }

        private void TöröltekKiemelése()
        {

            try
            {
                if (Gépi_Tábla.RowCount > 0)
                {
                    foreach (DataGridViewRow row in Gépi_Tábla.Rows)
                    {
                        if (row.Cells[5].Value.ToÉrt_Int() == 1)
                        {
                            row.DefaultCellStyle.ForeColor = Color.White;
                            row.DefaultCellStyle.BackColor = Color.IndianRed;
                            row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f, FontStyle.Strikeout);
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

        private void Gepi_lista_Napló()
        {
            try
            {
                Gépi_Tábla.Visible = false;
                Gépi_Tábla.CleanFilterAndSort();
                GépiNaplóTáblaFejléc();
                GépiNaplóTáblaTartalom();
                Gépi_Tábla.DataSource = GépiTábla;
                GépiNaplóTáblaSzélesség();
                Gépi_Tábla.Visible = true;
                Gépi_Tábla.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GépiNaplóTáblaTartalom()
        {
            try
            {

                List<Adat_Jármű_Takarítás_Napló> AdatokNapló = KézTakNapló.Lista_Adatok(Gepi_datum.Value.Year).Where(a => a.Takarítási_fajta == "Gépi").ToList();
                List<Adat_Jármű_Takarítás_Napló> ideig = KézTakNapló.Lista_Adatok(Gepi_datum.Value.Year - 1).Where(a => a.Takarítási_fajta == "Gépi").ToList();
                AdatokNapló.AddRange(ideig);

                if (Pály_TB.Text.Trim() != "") AdatokNapló = AdatokNapló.Where(a => a.Azonosító == Pály_TB.Text.Trim()).ToList();

                if (Tel_TB.Text.Trim() != "") AdatokNapló = AdatokNapló.Where(a => a.Telephely == Tel_TB.Text.Trim()).ToList();

                AdatokNapló = (from a in AdatokNapló
                               orderby a.Azonosító, a.Takarítási_fajta, a.Dátum descending
                               select a).ToList();
                List<Adat_Jármű> AdatokJármű = KézJármű.Lista_Adatok("Főmérnökség");

                GépiTábla.Clear();
                foreach (Adat_Jármű_Takarítás_Napló rekord in AdatokNapló)
                {
                    DataRow Soradat = GépiTábla.NewRow();
                    Soradat["Azonosító"] = rekord.Azonosító.Trim();

                    string Típus = (from a in AdatokJármű
                                    where a.Azonosító == rekord.Azonosító
                                    select a.Típus).FirstOrDefault();
                    if (Típus == null || Típus.Trim() == "")
                        Soradat["Típus"] = "";
                    else
                        Soradat["Típus"] = Típus;
                    Soradat["Dátum"] = rekord.Dátum.ToShortDateString();
                    Soradat["Takarítási fajta"] = rekord.Takarítási_fajta;
                    Soradat["Telephely"] = rekord.Telephely;
                    Soradat["Státus"] = rekord.Státus;
                    Soradat["Rögzítő"] = rekord.Módosító;
                    Soradat["Mikor"] = rekord.Mikor;
                    GépiTábla.Rows.Add(Soradat);
                }
                if (CmbGépiTíp.Text.Trim() != "")
                {
                    EnumerableRowCollection<DataRow> filteredRows = GépiTábla.AsEnumerable()
                        .Where(a => a.Field<string>("Típus") == CmbGépiTíp.Text.Trim());
                    GépiTábla = filteredRows.CopyToDataTable();
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

        private void GépiNaplóTáblaFejléc()
        {
            try
            {
                GépiTábla.Columns.Clear();
                GépiTábla.Columns.Add("Azonosító");
                GépiTábla.Columns.Add("Típus");
                GépiTábla.Columns.Add("Dátum");
                GépiTábla.Columns.Add("Takarítási fajta");
                GépiTábla.Columns.Add("Telephely");
                GépiTábla.Columns.Add("Státus");
                GépiTábla.Columns.Add("Rögzítő");
                GépiTábla.Columns.Add("Mikor");

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GépiNaplóTáblaSzélesség()
        {
            Gépi_Tábla.Columns["Azonosító"].Width = 100;
            Gépi_Tábla.Columns["Típus"].Width = 100;
            Gépi_Tábla.Columns["Dátum"].Width = 100;
            Gépi_Tábla.Columns["Takarítási fajta"].Width = 100;
            Gépi_Tábla.Columns["Telephely"].Width = 150;
            Gépi_Tábla.Columns["Státus"].Width = 100;
            Gépi_Tábla.Columns["Rögzítő"].Width = 130;
            Gépi_Tábla.Columns["Mikor"].Width = 180;
        }

        private void Gepi_lista_Kocsik()
        {
            try
            {
                Gépi_Tábla.Visible = false;
                Gépi_Tábla.CleanFilterAndSort();
                GépiTáblaFejléc();
                GépiTáblaTartalom();
                Gépi_Tábla.DataSource = GépiTábla;
                GépiTáblaOszlopSzélesség();
                Gépi_Tábla.Visible = true;
                Gépi_Tábla.Refresh();


            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GépiTáblaTartalom()
        {
            try
            {
                AdatokTak = KézTak.Lista_Adatok();

                List<Adat_Jármű_Takarítás_Takarítások> Adatok = (from a in AdatokTak
                                                                 where a.Takarítási_fajta == "Gépi"
                                                                 orderby a.Azonosító, a.Takarítási_fajta, a.Dátum
                                                                 select a).ToList();
                if (Pály_TB.Text.Trim() != "") Adatok = (from a in Adatok
                                                         where a.Azonosító == Pály_TB.Text.Trim()
                                                         select a).ToList();
                if (Tel_TB.Text.Trim() != "") Adatok = (from a in Adatok
                                                        where a.Telephely == Tel_TB.Text.Trim()
                                                        select a).ToList();
                List<Adat_Jármű> AdatokJármű = KézJármű.Lista_Adatok("Főmérnökség");


                GépiTábla.Clear();
                foreach (Adat_Jármű_Takarítás_Takarítások rekord in Adatok)
                {
                    DataRow Soradat = GépiTábla.NewRow();
                    Soradat["Azonosító"] = rekord.Azonosító.Trim();
                    string Típus = (from a in AdatokJármű
                                    where a.Azonosító == rekord.Azonosító
                                    select a.Típus).FirstOrDefault() ?? "";
                    if (Típus.Trim() == "")
                        Soradat["Típus"] = "";
                    else
                        Soradat["Típus"] = Típus;
                    Soradat["Dátum"] = rekord.Dátum.ToShortDateString();
                    Soradat["Takarítási fajta"] = rekord.Takarítási_fajta.ToStrTrim();
                    Soradat["Telephely"] = rekord.Telephely;
                    Soradat["Státus"] = rekord.Státus;
                    Soradat["Eltelt napok"] = (DateTime.Today - rekord.Dátum).Days;
                    GépiTábla.Rows.Add(Soradat);
                }
                if (CmbGépiTíp.Text.Trim() != "")
                {
                    EnumerableRowCollection<DataRow> filteredRows = GépiTábla.AsEnumerable().Where(a => a.Field<string>("Típus") == CmbGépiTíp.Text.Trim());

                    List<DataRow> AdatokS = filteredRows.ToList();
                    if (AdatokS != null && AdatokS.Count > 0)
                        GépiTábla = filteredRows.CopyToDataTable();
                    else
                        throw new HibásBevittAdat($"{CmbGépiTíp.Text.Trim()} típusra nem lehet szűrni.");
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

        private void GépiTáblaFejléc()
        {
            try
            {
                GépiTábla.Columns.Clear();
                GépiTábla.Columns.Add("Azonosító");
                GépiTábla.Columns.Add("Típus");
                GépiTábla.Columns.Add("Dátum");
                GépiTábla.Columns.Add("Takarítási fajta");
                GépiTábla.Columns.Add("Telephely");
                GépiTábla.Columns.Add("Státus");
                GépiTábla.Columns.Add("Eltelt napok", System.Type.GetType("System.Int32"));
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GépiTáblaOszlopSzélesség()
        {
            Gépi_Tábla.Columns["Azonosító"].Width = 100;
            Gépi_Tábla.Columns["Típus"].Width = 100;
            Gépi_Tábla.Columns["Dátum"].Width = 100;
            Gépi_Tábla.Columns["Takarítási fajta"].Width = 100;
            Gépi_Tábla.Columns["Telephely"].Width = 150;
            Gépi_Tábla.Columns["Státus"].Width = 100;
            Gépi_Tábla.Columns["Eltelt napok"].Width = 130;
        }

        private void GépiTípusCmbFeltölt()
        {
            List<Adat_Jármű> AdatokJármű = KézJármű.Lista_Adatok("Főmérnökség");
            List<string> Típusok = (from a in AdatokJármű
                                    orderby a.Típus
                                    select a.Típus).Distinct().ToList();

            CmbGépiTíp.Items.Clear();

            foreach (string rekord in Típusok) if (rekord != "") CmbGépiTíp.Items.Add(rekord);

        }

        private void Gepi_excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Gépi_Tábla.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Takarítás_Gépi_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddhhmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyE.DataGridViewToExcel(fájlexc, Gépi_Tábla);
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

        private void Gepi_rogzit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Gepi_pályaszám.Text.Trim() == "") throw new HibásBevittAdat("Pályaszámot meg kell adni.");
                AdatokTak = KézTak.Lista_Adatok();
                int státus = 0;
                if (Gepi_torolt.Checked) státus = 1;

                Adat_Jármű_Takarítás_Takarítások AdatTakarítások = (from a in AdatokTak
                                                                    where a.Azonosító == Gepi_pályaszám.Text.Trim()
                                                                    && a.Takarítási_fajta == "Gépi"
                                                                    && a.Telephely == Cmbtelephely.Text.Trim()
                                                                    && a.Státus == 0
                                                                    select a).FirstOrDefault();

                Adat_Jármű_Takarítás_Takarítások ADAT = new Adat_Jármű_Takarítás_Takarítások(
                                                     Gepi_pályaszám.Text.Trim(),
                                                     Gepi_datum.Value,
                                                     "Gépi",
                                                     Cmbtelephely.Text.Trim(),
                                                     státus);
                if (AdatTakarítások == null)
                {
                    //Rögzítjük az új pályaszámot és naplózzuk
                    KézTak.Rögzítés(ADAT);
                    MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Ha van csak a dátumot módosítjuk
                    if (státus == 0)
                    {
                        if (Gepi_datum.Value.ToShortDateString() != AdatTakarítások.Dátum.ToShortDateString())
                        {
                            KézTak.Módosítás(ADAT);
                            MessageBox.Show("Az adatok módosítása befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("Erre a napra már volt rögzítve.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        //töröljük az adott napi takarítást és megkeressük az előzőt
                        //Kitöröljük a Naplóból
                        Adat_Jármű_Takarítás_Napló ADATNAP = new Adat_Jármű_Takarítás_Napló(
                                    ADAT.Azonosító,
                                    ADAT.Dátum,
                                    ADAT.Takarítási_fajta,
                                    ADAT.Telephely,
                                    DateTime.Now,
                                    Program.PostásNév,
                                    ADAT.Státus);
                        KézTakNapló.Rögzítés(DateTime.Now.Year, ADATNAP);

                        List<Adat_Jármű_Takarítás_Napló> AdatokNapló = KézTakNapló.Lista_Adatok(Gepi_datum.Value.Year).Where(a => a.Takarítási_fajta == "Gépi").ToList();
                        List<Adat_Jármű_Takarítás_Napló> ideig = KézTakNapló.Lista_Adatok(Gepi_datum.Value.Year - 1).Where(a => a.Takarítási_fajta == "Gépi").ToList();
                        AdatokNapló.AddRange(ideig);
                        Adat_Jármű_Takarítás_Napló Előző = (from a in AdatokNapló
                                                            where a.Azonosító == Gepi_pályaszám.Text.Trim()
                                                            && a.Takarítási_fajta == "Gépi"
                                                            && a.Telephely == Cmbtelephely.Text.Trim()
                                                            && a.Státus == 0
                                                            orderby a.Dátum descending
                                                            select a).FirstOrDefault();
                        if (Előző != null)
                        {
                            // ha töröljük, akkor vissza kell állítani az előző dátumot
                            ADAT = new Adat_Jármű_Takarítás_Takarítások(
                                Gepi_pályaszám.Text.Trim(),
                                Előző.Dátum,
                                "Gépi",
                                Cmbtelephely.Text.Trim(),
                                0);
                            KézTak.Módosítás_Dátum(ADAT);
                            MessageBox.Show("Az adatok módosítása megtörtént.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        #region BMR
        Ablak_BMR Új_Ablak_BMR;
        private void BMR_Click(object sender, EventArgs e)
        {
            Új_Ablak_BMR?.Close();
            Új_Ablak_BMR = new Ablak_BMR(ListaDátum.Value, true, Cmbtelephely.Text.Trim());
            Új_Ablak_BMR.FormClosed += Új_Ablak_BMR_FormClosed;
            Új_Ablak_BMR.Show();
        }

        private void Új_Ablak_BMR_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_BMR = null;
        }

        #endregion


        #region Lista

        readonly List<string> típusnév = new List<string>();

        public void IdegenLista()
        {
            try
            {
                AdatokJ1.Clear();
                AdatokIdegen = KézIdegen.Lista_Adatok();
                AdatokIdegen = (from a in AdatokIdegen
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
    }
}