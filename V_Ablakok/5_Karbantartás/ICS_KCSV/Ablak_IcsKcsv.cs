
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Ablakok.ICS_KCSV;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using static System.IO.File;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{

    public partial class Ablak_IcsKcsv
    {
        string _fájlexc;
        DataTable _Tábla = new DataTable();
        long utolsósor;
        long HavikmICS = 5000;
        int Hónapok = 24;

        readonly Kezelő_Ciklus KézCiklus = new Kezelő_Ciklus();
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly Kezelő_Jármű2ICS KézJármű2ICS = new Kezelő_Jármű2ICS();
        readonly Kezelő_T5C5_Kmadatok KézICSKmadatok = new Kezelő_T5C5_Kmadatok("ICS");
        readonly Kezelő_Kerék_Mérés KézMérés = new Kezelő_Kerék_Mérés();
        readonly Kezelő_Nap_Hiba KézHiba = new Kezelő_Nap_Hiba();
        readonly Kezelő_Vezénylés KézVezénylés = new Kezelő_Vezénylés();
        readonly Kezelő_Főkönyv_Zser_Km KézKorr = new Kezelő_Főkönyv_Zser_Km();
        readonly Kezelő_jármű_hiba KézJárműHiba = new Kezelő_jármű_hiba();
        readonly Kezelő_kiegészítő_telephely KézTelep = new Kezelő_kiegészítő_telephely();
        readonly Kezelő_ICS_előterv KézElőterv = new Kezelő_ICS_előterv();

        List<Adat_T5C5_Kmadatok> AdatokICSKmadatok = new List<Adat_T5C5_Kmadatok>();
        List<Adat_Ciklus> AdatokCiklus = new List<Adat_Ciklus>();
        List<Adat_Jármű> AdatokJármű = new List<Adat_Jármű>();
        List<Adat_Jármű> AdatokFőJármű = new List<Adat_Jármű>();
        List<Adat_Jármű_2ICS> AdatokJármű2ICS = new List<Adat_Jármű_2ICS>();
        List<Adat_Kerék_Mérés> AdatokMérés = new List<Adat_Kerék_Mérés>();
        List<Adat_Nap_Hiba> AdatokHiba = new List<Adat_Nap_Hiba>();
        List<Adat_Vezénylés> AdatokVezénylés = new List<Adat_Vezénylés>();
#pragma warning disable IDE0044
        List<Adat_Főkönyv_Zser_Km> AdatokZserKm = new List<Adat_Főkönyv_Zser_Km>();
#pragma warning restore IDE0044


        #region Alap
        public Ablak_IcsKcsv()
        {
            InitializeComponent();
            Start();
        }

        /// <summary>
        /// Ablak betöltésekor elinduló események
        /// </summary>
        private void Start()
        {
            Telephelyekfeltöltése();
            Pályaszám_feltöltés();
            AdatokCiklus = KézCiklus.Lista_Adatok();
            Fülek.SelectedIndex = 0;
            Fülekkitöltése();
            Jogosultságkiosztás();
            Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;
        }

        private void IcsKcsv_Load(object sender, EventArgs e)
        {
        }

        private void Ablak_IcsKcsv_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_ICS_KCSV_segéd?.Close();
        }

        /// <summary>
        /// Telephelyek feltöltése a comboboxba
        /// </summary>
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

        /// <summary>
        /// Jogosultságok kiosztása
        /// </summary>
        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;

                // ide kell az összes gombot tenni amit szabályozni akarunk false
                E_rögzít.Enabled = false;

                Utolsó_V_rögzítés.Enabled = false;
                Töröl.Enabled = false;
                SAP_adatok.Enabled = false;


                Btn_Vezénylésbeírás.Enabled = false;

                // csak főmérnökségi belépéssel törölhető
                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {
                    Töröl.Visible = true;
                }
                else
                {
                    Töröl.Visible = false;
                }

                melyikelem = 113;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    E_rögzít.Enabled = true;
                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    Utolsó_V_rögzítés.Enabled = true;
                    Töröl.Enabled = true;
                    SAP_adatok.Enabled = true;
                }
                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 3))
                {
                    Btn_Vezénylésbeírás.Enabled = true;
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

        /// <summary>
        /// Felhasználói leírást nyitja meg
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\Tulajdonság_ICS.html";
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

        /// <summary>
        /// Telephely kiválasztásakor a combobaxba a pályaszámok feltöltése
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmbtelephely_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
            Pályaszám_feltöltés();
        }

        /// <summary>
        /// Pályaszámok feltöltése a combobaxba
        /// </summary>
        private void Pályaszám_feltöltés()
        {
            try
            {
                Pályaszám.Items.Clear();
                if (!Program.Postás_Vezér && Cmbtelephely.Text.Trim() == "") return;

                AdatokJármű = KézJármű.Lista_Adatok("Főmérnökség");
                List<Adat_Jármű> Adatok = new List<Adat_Jármű>();

                if (Program.Postás_Vezér)
                {
                    // ha főmérnökség vagy vezér akkor minden telephelyről
                    // feltöltjük az összes pályaszámot a Comboba
                    Adatok = (from a in AdatokJármű
                              where a.Törölt == false && (a.Valóstípus == "ICS" || a.Valóstípus == "KCSV-7")
                              orderby a.Azonosító
                              select a).ToList();

                }
                else
                {
                    // ha nem főmérnökség akkor csak a kiválasztott telephelyről
                    // feltöltjük az összes pályaszámot a Comboba
                    Adatok = (from a in AdatokJármű
                              where a.Törölt == false && a.Üzem == Cmbtelephely.Text.Trim()
                              && (a.Valóstípus == "ICS" || a.Valóstípus == "KCSV-7")
                              orderby a.Azonosító
                              select a).ToList();
                }

                // feltöltjük az összes pályaszámot a Comboba
                foreach (Adat_Jármű rekord in Adatok)
                    Pályaszám.Items.Add(rekord.Azonosító);

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

        /// <summary>
        /// Pályaszám kereső gomb megnyomásakor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pályaszámkereső_Click(object sender, EventArgs e)
        {
            Frissít();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Frissít()
        {
            try
            {
                if (Pályaszám.Text.Trim() == "") return;

                switch (Fülek.SelectedIndex)
                {
                    case 0:
                        {
                            Kiirjaalapadatokat();
                            Hétnapjai_feltöltése();
                            break;
                        }
                    case 1:
                        {
                            break;
                        }

                    case 3:
                        {
                            Kiüríti_lapfül();
                            Kiirjaatörténelmet();
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
                            Ütemezettkocsik();
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

        /// <summary>
        /// Lapfül kiválasztásakor a megfelelő lapfül betöltése
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Fülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }

        /// <summary>
        /// Lapfül tartalmának feltöltése
        /// </summary>
        private void Fülekkitöltése()
        {
            try
            {
                switch (Fülek.SelectedIndex)
                {
                    case 0:
                        {
                            // alapadatok
                            // ürítjük a mezőket
                            Típus_text.Text = "";
                            Státus_text.Text = "";
                            Miótaáll_text.Text = "";
                            Főmérnökség_text.Text = "";
                            Járműtípus_text.Text = "";
                            Combo_E2.Text = "";
                            Combo_E3.Text = "";

                            Kiirjaalapadatokat();
                            Hétnapjai_feltöltése();
                            break;
                        }
                    case 1:
                        {
                            break;
                        }
                    case 2:
                        {
                            CiklusrendCombo_feltöltés();
                            Vizsgsorszámcombofeltölés();
                            Üzemek_listázása();
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
                    case 5:
                        {
                            Ütemezettkocsik();
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

        /// <summary>
        /// Pályaszám kiválasztásakor a combobaxba a pályaszámok feltöltése
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pályaszám_SelectedIndexChanged(object sender, EventArgs e)
        {
            Frissít();
            Kiüríti_lapfül();
        }

        /// <summary>
        /// Lapfül fejlécének megrajzolása
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Kiírja a pályaszámhoz tartozó adatoka a mezőkbe
        /// </summary>
        private void Kiirjaalapadatokat()
        {
            try
            {
                if (Cmbtelephely.Text.Trim() == "") return;
                if (Pályaszám.Text.Trim() == "") return;
                AdatokJármű = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                AdatokJármű2ICS = KézJármű2ICS.Lista_Adatok(Cmbtelephely.Text.Trim());
                Üríti();

                Adat_Jármű rekord = (from a in AdatokJármű
                                     where a.Azonosító == Pályaszám.Text.Trim()
                                     select a).FirstOrDefault();


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
                    if (rekord.Miótaáll == null || rekord.Miótaáll == new DateTime(1900, 1, 1))
                        Miótaáll_text.Text = "";
                    else
                        Miótaáll_text.Text = rekord.Miótaáll.ToString("yyyy.MM.dd");
                }

                Adat_Jármű_2ICS Elem2ICS = (from a in AdatokJármű2ICS
                                            where a.Azonosító == Pályaszám.Text.Trim()
                                            select a).FirstOrDefault();

                if (Elem2ICS != null)
                {
                    int E2_sorszám = Elem2ICS.E2;
                    if (E2_sorszám == 0)
                        Combo_E2.Text = "";
                    else
                        Combo_E2.Text = Combo_E2.Items[E2_sorszám - 1].ToString();


                    int E3_sorszám = Elem2ICS.E3;
                    if (E3_sorszám == 0)
                        Combo_E3.Text = "";
                    else
                        Combo_E3.Text = Combo_E3.Items[E3_sorszám - 1].ToString();
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

        /// <summary>
        /// Ürítjük a mezőket
        /// </summary>
        private void Üríti()
        {
            // ürítjük a mezőket
            Típus_text.Text = "";
            Státus_text.Text = "";
            Miótaáll_text.Text = "";

            Főmérnökség_text.Text = "";
            Járműtípus_text.Text = "";

            Combo_E2.Text = "";
            Combo_E3.Text = "";
        }

        /// <summary>
        /// Feltölti a ComboBoxot a hét napjaival
        /// </summary>
        private void Hétnapjai_feltöltése()
        {
            Combo_E2.Items.Clear();
            Combo_E2.Items.Add("Hétfő");
            Combo_E2.Items.Add("Kedd");
            Combo_E2.Items.Add("Szerda");
            Combo_E2.Items.Add("Csütörtök");
            Combo_E2.Items.Add("Péntek");
            Combo_E2.Items.Add("Szombat");
            Combo_E2.Items.Add("Vasárnap");

            Combo_E3.Items.Clear();
            Combo_E3.Items.Add("Hétfő");
            Combo_E3.Items.Add("Kedd");
            Combo_E3.Items.Add("Szerda");
            Combo_E3.Items.Add("Csütörtök");
            Combo_E3.Items.Add("Péntek");
            Combo_E3.Items.Add("Szombat");
            Combo_E3.Items.Add("Vasárnap");
        }

        /// <summary>
        /// Rögzíti a pályaszámhoz tartozó adatokat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void E_rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Pályaszám.Text.Trim() == "") return;

                // leellenőrizzük, hogy létezik-e a kocsi
                AdatokJármű = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adat_Jármű ElemJármű = (from a in AdatokJármű
                                        where a.Azonosító == Pályaszám.Text.Trim() &&
                                        a.Törölt == false
                                        select a).FirstOrDefault();
                if (ElemJármű == null)
                {
                    if (MessageBox.Show("Nincs ilyen jármű a telephelyen! Mégis rögzítjük?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                        return;
                }

                int e2 = 0;
                int e3 = 0;
                if (Combo_E3.Text.Trim() != "")
                {

                    for (int i = 0; i < 7; i++)
                    {
                        if (Combo_E3.Items[i].ToStrTrim() == Combo_E3.Text.Trim())
                        {
                            e3 = i + 1;
                            break;
                        }
                    }
                }
                if (Combo_E2.Text.Trim() != "")
                {
                    for (int i = 0; i < 7; i++)
                    {
                        if (Combo_E2.Items[i].ToStrTrim() == Combo_E2.Text.Trim())
                        {
                            e2 = i + 1;
                            break;
                        }
                    }
                }

                List<Adat_Jármű_2ICS> AdatokVizsgálat = KézJármű2ICS.Lista_Adatok(Cmbtelephely.Text.Trim());

                Adat_Jármű_2ICS ElemVizsgálat = (from a in AdatokVizsgálat
                                                 where a.Azonosító == Pályaszám.Text.Trim()
                                                 select a).FirstOrDefault();
                Adat_Jármű_2ICS ADAT = new Adat_Jármű_2ICS(
                          Pályaszám.Text.Trim(),
                          new DateTime(1900, 1, 1),
                          e2,
                          e3);

                if (ElemVizsgálat != null)
                    KézJármű2ICS.Módosítás(Cmbtelephely.Text.Trim(), ADAT);
                else
                    KézJármű2ICS.Rögzítés(Cmbtelephely.Text.Trim(), ADAT);

                Pályaszám_ellenőrzés();
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

        /// <summary>
        /// Ellenőrzi, hogy a pályaszámok léteznek-e a jármű telephelyi adatbázisban, ha nincs akkor törli
        /// </summary>
        private void Pályaszám_ellenőrzés()
        {
            try
            {
                List<Adat_Jármű_2ICS> ICS = KézJármű2ICS.Lista_Adatok(Cmbtelephely.Text.Trim());
                List<Adat_Jármű> Jármű = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());

                foreach (Adat_Jármű_2ICS rekord in ICS)
                {
                    Adat_Jármű Szűrt = Jármű.Where(e => e.Azonosító.Trim() == rekord.Azonosító.Trim()).FirstOrDefault();
                    if (Szűrt == null) KézJármű2ICS.Törlés(Cmbtelephely.Text.Trim(), rekord.Azonosító.Trim());
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

        /// <summary>
        /// Excel kimenetet készít a E2 E3 napok listájáról
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                // kimeneti fájl helye és neve
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "E2-E3 napok listájának készítése",
                    FileName = $"E2-E3_tábla_{DateTime.Now:yyyyMMddhhmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                List<Adat_Jármű_2ICS> Adatok = KézJármű2ICS.Lista_Adatok(Cmbtelephely.Text.Trim());

                int sor;
                // megnyitjuk az excelt
                MyE.ExcelLétrehozás();
                string munkalap = "7 napos";
                MyE.Munkalap_átnevezés("Munka1", munkalap);
                MyE.Új_munkalap("3 napos");
                MyE.Munkalap_aktív(munkalap);


                for (int i = 1; i <= 2; i++)
                {
                    if (i == 1)
                        munkalap = "7 napos";
                    else
                        munkalap = "3 napos";

                    MyE.Munkalap_aktív(munkalap);
                    MyE.Munkalap_betű("Calibri", 20);

                    MyE.Oszlopszélesség(munkalap, "a:g", 18);
                    MyE.Sormagasság("1:11", 40);
                    MyE.Rácsoz("a1:g11");
                    MyE.Vastagkeret("a1:g1");
                    MyE.Kiir("Hétfő", "a1");
                    MyE.Kiir("Kedd", "b1");
                    MyE.Kiir("Szerda", "c1");
                    MyE.Kiir("Csütörtök", "d1");
                    MyE.Kiir("Péntek", "e1");
                    MyE.Kiir("Szombat", "f1");
                    MyE.Kiir("Vasárnap", "g1");
                    // kiírjuk a kocsikat
                    for (int j = 1; j <= 7; j++)
                    {
                        sor = 1;
                        List<Adat_Jármű_2ICS> Szűrt;
                        if (i == 1)
                            Szűrt = Adatok.Where(a => a.E2 == j).ToList();
                        else
                            Szűrt = Adatok.Where(a => a.E3 == j).ToList();

                        foreach (Adat_Jármű_2ICS rekord in Szűrt)
                        {
                            sor += 1;
                            MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(j) + sor.ToString());
                        }

                    }
                }

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
        #endregion


        #region Lekérdezések
        /// <summary>
        /// Lekérdezés gomb megnyomásakor kiírja az összesített adatokat a táblázatba
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lekérdezés_lekérdezés_Click(object sender, EventArgs e)
        {
            try
            {
                Tábla_lekérdezés.Rows.Clear();
                Tábla_lekérdezés.Columns.Clear();
                Tábla_lekérdezés.Refresh();
                Tábla_lekérdezés.Visible = false;
                Tábla_lekérdezés.ColumnCount = 32;
                // fejléc elkészítése

                Tábla_lekérdezés.Columns[0].HeaderText = "Psz";
                Tábla_lekérdezés.Columns[0].Width = 60;
                Tábla_lekérdezés.Columns[1].HeaderText = "Vizsg. foka";
                Tábla_lekérdezés.Columns[1].Width = 80;
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
                Tábla_lekérdezés.Columns[16].HeaderText = "Kerék-K1";
                Tábla_lekérdezés.Columns[16].Width = 80;
                Tábla_lekérdezés.Columns[17].HeaderText = "Kerék-K2";
                Tábla_lekérdezés.Columns[17].Width = 80;
                Tábla_lekérdezés.Columns[18].HeaderText = "Kerék-K3";
                Tábla_lekérdezés.Columns[18].Width = 80;
                Tábla_lekérdezés.Columns[19].HeaderText = "Kerék-K4";
                Tábla_lekérdezés.Columns[19].Width = 80;
                Tábla_lekérdezés.Columns[20].HeaderText = "Kerék-K5";
                Tábla_lekérdezés.Columns[20].Width = 80;
                Tábla_lekérdezés.Columns[21].HeaderText = "Kerék-K6";
                Tábla_lekérdezés.Columns[21].Width = 80;
                Tábla_lekérdezés.Columns[22].HeaderText = "Kerék-K7";
                Tábla_lekérdezés.Columns[22].Width = 80;
                Tábla_lekérdezés.Columns[23].HeaderText = "Kerék-K8";
                Tábla_lekérdezés.Columns[23].Width = 80;
                Tábla_lekérdezés.Columns[24].HeaderText = "Kerék min";
                Tábla_lekérdezés.Columns[24].Width = 80;
                Tábla_lekérdezés.Columns[25].HeaderText = "Ssz.";
                Tábla_lekérdezés.Columns[25].Width = 80;
                Tábla_lekérdezés.Columns[26].HeaderText = "Végezte";
                Tábla_lekérdezés.Columns[26].Width = 120;
                Tábla_lekérdezés.Columns[27].HeaderText = "Következő V";
                Tábla_lekérdezés.Columns[27].Width = 120;
                Tábla_lekérdezés.Columns[28].HeaderText = "Következő V Ssz.";
                Tábla_lekérdezés.Columns[28].Width = 120;
                Tábla_lekérdezés.Columns[29].HeaderText = "Következő V2-V3";
                Tábla_lekérdezés.Columns[29].Width = 120;
                Tábla_lekérdezés.Columns[30].HeaderText = "Következő V2-V3 Ssz.";
                Tábla_lekérdezés.Columns[30].Width = 120;
                Tábla_lekérdezés.Columns[31].HeaderText = "Utolsó V2-V3 számláló";
                Tábla_lekérdezés.Columns[31].Width = 120;

                AdatokJármű = KézJármű.Lista_Adatok("Főmérnökség");
                List<Adat_Jármű> Adatok = (from a in AdatokJármű
                                           where a.Törölt == false && (a.Valóstípus == "ICS" || a.Valóstípus == "KCSV-7")
                                           orderby a.Azonosító
                                           select a).ToList();

                List<Adat_T5C5_Kmadatok> AdatokKm = KézICSKmadatok.Lista_Adatok();

                KerékadatokListaFeltöltés();

                Holtart.Be();

                foreach (Adat_Jármű Elem in Adatok)
                {
                    Tábla_lekérdezés.RowCount++;
                    int i = Tábla_lekérdezés.RowCount - 1;
                    //alapadatok kiírása
                    Tábla_lekérdezés.Rows[i].Cells[0].Value = Elem.Azonosító.Trim();
                    Tábla_lekérdezés.Rows[i].Cells[14].Value = Elem.Üzem.Trim();
                    Tábla_lekérdezés.Rows[i].Cells[15].Value = Elem.Típus.Trim();

                    Adat_T5C5_Kmadatok rekord = (from a in AdatokKm
                                                 where a.Azonosító == Elem.Azonosító
                                                 orderby a.Vizsgdátumk descending
                                                 select a).FirstOrDefault();
                    //Vizsgálati adatok kiírása
                    if (rekord != null)
                    {
                        // ki olvassuk az elsőt majd kilépünk
                        Tábla_lekérdezés.Rows[i].Cells[1].Value = rekord.Vizsgfok.Trim();
                        Tábla_lekérdezés.Rows[i].Cells[2].Value = rekord.Vizsgsorszám;
                        Tábla_lekérdezés.Rows[i].Cells[3].Value = rekord.Vizsgdátumk.ToString("yyyy.MM.dd");
                        Tábla_lekérdezés.Rows[i].Cells[4].Value = rekord.Vizsgdátumv.ToString("yyyy.MM.dd");
                        Tábla_lekérdezés.Rows[i].Cells[5].Value = rekord.Vizsgkm;
                        Tábla_lekérdezés.Rows[i].Cells[6].Value = rekord.KMUdátum.ToString("yyyy.MM.dd");
                        Tábla_lekérdezés.Rows[i].Cells[7].Value = rekord.KMUkm;
                        // ha J akkor nem kell különbséget képezni
                        if (rekord.Vizsgsorszám == 0)
                            Tábla_lekérdezés.Rows[i].Cells[8].Value = rekord.KMUkm;
                        else
                            Tábla_lekérdezés.Rows[i].Cells[8].Value = (rekord.KMUkm - rekord.Vizsgkm);

                        Tábla_lekérdezés.Rows[i].Cells[9].Value = rekord.Havikm;
                        Tábla_lekérdezés.Rows[i].Cells[10].Value = rekord.Jjavszám;
                        Tábla_lekérdezés.Rows[i].Cells[11].Value = rekord.Fudátum.ToString("yyyy.MM.dd");
                        Tábla_lekérdezés.Rows[i].Cells[12].Value = rekord.Ciklusrend.Trim();
                        Tábla_lekérdezés.Rows[i].Cells[13].Value = rekord.Teljeskm;

                        Tábla_lekérdezés.Rows[i].Cells[25].Value = rekord.ID;
                        if (rekord.V2végezte.Trim() != "_")
                            Tábla_lekérdezés.Rows[i].Cells[26].Value = rekord.V2végezte.Trim();
                        Tábla_lekérdezés.Rows[i].Cells[27].Value = rekord.KövV.Trim();
                        Tábla_lekérdezés.Rows[i].Cells[28].Value = rekord.KövV_sorszám;
                        Tábla_lekérdezés.Rows[i].Cells[29].Value = rekord.KövV2.Trim();
                        Tábla_lekérdezés.Rows[i].Cells[30].Value = rekord.KövV2_sorszám;
                        Tábla_lekérdezés.Rows[i].Cells[31].Value = rekord.V2V3Számláló;
                    }
                    //Kerék átmérők kiírása
                    if (AdatokMérés.Count > 0)
                    {
                        int kerékminimum = 1000;
                        for (int j = 0; j <= 7; j++)
                        {
                            string[] darabol = Tábla_lekérdezés.Columns[j + 16].HeaderText.Split('-');
                            Adat_Kerék_Mérés Méret = (from a in AdatokMérés
                                                      where a.Azonosító == Elem.Azonosító.Trim() &&
                                                      a.Pozíció == darabol[1].Trim()
                                                      orderby a.Mikor descending
                                                      select a).FirstOrDefault();
                            if (Méret != null)
                            {
                                Tábla_lekérdezés.Rows[i].Cells[16 + j].Value = Méret.Méret;
                                if (kerékminimum > Méret.Méret) kerékminimum = Méret.Méret;
                            }
                        }

                        if (kerékminimum != 1000) Tábla_lekérdezés.Rows[i].Cells[24].Value = kerékminimum;
                    }

                    Holtart.Lép();
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

        /// <summary>
        /// Készít egy excelt a lekérdezésből
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    FileName = $"ICS_KCSV_futásadatok_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
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

        /// <summary>
        /// Készít egy excelt a teljes adatbázisból
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Teljes_adatbázis_excel_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog
            {
                // kimeneti fájl helye és neve
                InitialDirectory = "MyDocuments",

                Title = "Adatbázis mentése Excel fájlba",
                FileName = $"ICS_KCSV_adatbázis_mentés_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                Filter = "Excel |*.xlsx"
            };
            // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
            if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                _fájlexc = SaveFileDialog1.FileName;
            else
                return;
            List<Adat_T5C5_Kmadatok> Adatok = KézICSKmadatok.Lista_Adatok();
            _Tábla = MyF.ToDataTable(Adatok);

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

        /// <summary>
        /// Szálon futó eljárás, ami elkészíti az excelt a háttérben
        /// </summary>
        /// <param name="callback"></param>
        private void SZál_ABadatbázis(Action callback)
        {
            Thread proc = new Thread(() =>
            {
                // elkészítjük a formanyomtatványt változókat nem lehet küldeni definiálni kell egy külső változót
                MyE.EXCELtábla(_Tábla, _fájlexc);
                this.Invoke(callback, new object[] { });
            });
            proc.Start();
        }

        /// <summary>
        /// Időzítő esemény, ami a Holtartot vezérli
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer1_Tick(object sender, EventArgs e)
        {
            Holtart.Lép();
        }

        /// <summary>
        /// Lekérdezés gomb megnyomásakor kiírja az összesített adatokat a táblázatba
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            Telephelyi_lekérdezés();
        }

        /// <summary>
        /// Lekérdezés gomb megnyomásakor kiírja az összesített adatokat a táblázatba
        /// </summary>
        private void Telephelyi_lekérdezés()
        {
            try
            {
                Holtart.Be();

                Tábla_lekérdezés.Rows.Clear();
                Tábla_lekérdezés.Columns.Clear();
                Tábla_lekérdezés.Refresh();
                Tábla_lekérdezés.Visible = false;
                Tábla_lekérdezés.ColumnCount = 14;
                // fejléc elkészítése
                Tábla_lekérdezés.Columns[0].HeaderText = "Psz";
                Tábla_lekérdezés.Columns[0].Width = 100;
                Tábla_lekérdezés.Columns[1].HeaderText = "KM J-óta";
                Tábla_lekérdezés.Columns[1].Width = 100;
                Tábla_lekérdezés.Columns[2].HeaderText = "Frissítés Dátum";
                Tábla_lekérdezés.Columns[2].Width = 120;
                Tábla_lekérdezés.Columns[3].HeaderText = "Vizsg. Dátum";
                Tábla_lekérdezés.Columns[3].Width = 120;
                Tábla_lekérdezés.Columns[4].HeaderText = "Vizsg KM állás";
                Tábla_lekérdezés.Columns[4].Width = 100;
                Tábla_lekérdezés.Columns[5].HeaderText = "Vizsg. foka";
                Tábla_lekérdezés.Columns[5].Width = 100;
                Tábla_lekérdezés.Columns[6].HeaderText = "Vizsg. Ssz";
                Tábla_lekérdezés.Columns[6].Width = 100;
                Tábla_lekérdezés.Columns[7].HeaderText = "Utolsó V2 km";
                Tábla_lekérdezés.Columns[7].Width = 100;
                Tábla_lekérdezés.Columns[8].HeaderText = "Utolsó V3 km";
                Tábla_lekérdezés.Columns[8].Width = 100;
                Tábla_lekérdezés.Columns[9].HeaderText = "V óta futott km";
                Tábla_lekérdezés.Columns[9].Width = 100;
                Tábla_lekérdezés.Columns[10].HeaderText = "V2 óta futott km";
                Tábla_lekérdezés.Columns[10].Width = 100;
                Tábla_lekérdezés.Columns[11].HeaderText = "V3 óta futott km";
                Tábla_lekérdezés.Columns[11].Width = 100;
                Tábla_lekérdezés.Columns[12].HeaderText = "Ciklusrend";
                Tábla_lekérdezés.Columns[12].Width = 100;
                Tábla_lekérdezés.Columns[13].HeaderText = "Követk. vizsg.";
                Tábla_lekérdezés.Columns[13].Width = 100;

                // kilistázzuk a adatbázis adatait
                AdatokJármű = KézJármű.Lista_Adatok("Főmérnökség");
                List<Adat_Jármű> AdatokJ = new List<Adat_Jármű>();
                if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér)
                    AdatokJ = (from a in AdatokJármű
                               where a.Törölt == false
                               && (a.Valóstípus == "ICS" || a.Valóstípus == "KCSV-7")
                               orderby a.Azonosító
                               select a).ToList();
                else
                    AdatokJ = (from a in AdatokJármű
                               where a.Törölt == false
                               && (a.Valóstípus == "ICS" || a.Valóstípus == "KCSV-7")
                               && a.Üzem == Cmbtelephely.Text.Trim()
                               orderby a.Azonosító
                               select a).ToList();

                List<Adat_T5C5_Kmadatok> AdatokICS = KézICSKmadatok.Lista_Adatok();
                foreach (Adat_Jármű rekord in AdatokJ)
                {
                    Tábla_lekérdezés.RowCount++;
                    int i = Tábla_lekérdezés.RowCount - 1;
                    Tábla_lekérdezés.Rows[i].Cells[0].Value = rekord.Azonosító.Trim();

                    Adat_T5C5_Kmadatok rekordICS = (from a in AdatokICS
                                                    where a.Azonosító == rekord.Azonosító &&
                                                    a.Törölt == false
                                                    orderby a.Vizsgdátumk descending
                                                    select a).FirstOrDefault();
                    if (rekordICS != null)
                    {
                        Tábla_lekérdezés.Rows[i].Cells[1].Value = rekordICS.KMUkm;
                        Tábla_lekérdezés.Rows[i].Cells[2].Value = rekordICS.KMUdátum.ToString("yyyy.MM.dd");
                        Tábla_lekérdezés.Rows[i].Cells[3].Value = rekordICS.Vizsgdátumv.ToString("yyyy.MM.dd");
                        Tábla_lekérdezés.Rows[i].Cells[4].Value = rekordICS.Vizsgkm;
                        Tábla_lekérdezés.Rows[i].Cells[5].Value = rekordICS.Vizsgfok.Trim();
                        Tábla_lekérdezés.Rows[i].Cells[6].Value = rekordICS.Vizsgsorszám;
                        if (rekordICS.Vizsgsorszám == 0)
                        {
                            // ha J akkor nem kell különbséget képezni
                            Tábla_lekérdezés.Rows[i].Cells[9].Value = rekordICS.KMUkm;
                        }
                        else
                        {
                            Tábla_lekérdezés.Rows[i].Cells[9].Value = (rekordICS.KMUkm - rekordICS.Vizsgkm);
                        }
                        Tábla_lekérdezés.Rows[i].Cells[12].Value = rekordICS.Ciklusrend.Trim();

                        // utolsó V2 vizsgálat kiírása
                        Adat_T5C5_Kmadatok rekordICSV2 = (from a in AdatokICS
                                                          where a.Azonosító == rekord.Azonosító &&
                                                          a.Törölt == false &&
                                                          a.Vizsgfok.Contains("V2")
                                                          orderby a.Vizsgdátumk descending
                                                          select a).FirstOrDefault();
                        if (rekordICSV2 != null)
                        {
                            Tábla_lekérdezés.Rows[i].Cells[7].Value = rekordICSV2.Vizsgkm;
                            Tábla_lekérdezés.Rows[i].Cells[10].Value = rekordICS.KMUkm - rekordICSV2.Vizsgkm;
                        }
                        // utolsó V3 vizsgálat kiírása
                        Adat_T5C5_Kmadatok rekordICSV3 = (from a in AdatokICS
                                                          where a.Azonosító == rekord.Azonosító &&
                                                          a.Törölt == false &&
                                                          a.Vizsgfok.Contains("V3")
                                                          orderby a.Vizsgdátumk descending
                                                          select a).FirstOrDefault();
                        if (rekordICSV3 != null)
                        {
                            Tábla_lekérdezés.Rows[i].Cells[8].Value = rekordICSV3.Vizsgkm;
                            Tábla_lekérdezés.Rows[i].Cells[11].Value = rekordICS.KMUkm - rekordICSV3.Vizsgkm;
                        }

                        Adat_Ciklus ElemCiklus = (from a in AdatokCiklus
                                                  where a.Típus == rekordICS.Ciklusrend.Trim() &&
                                                  a.Sorszám == rekordICS.Vizsgsorszám + 1
                                                  select a).FirstOrDefault();

                        if (ElemCiklus != null) Tábla_lekérdezés.Rows[i].Cells[13].Value = ElemCiklus.Vizsgálatfok;
                    }
                    Holtart.Lép();
                }

                Tábla_lekérdezés.Refresh();
                Tábla_lekérdezés.Visible = true;

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


        #region Utolsó vizsgálati adatok lapfül
        /// <summary>
        /// Új adat gomb megnyomásakor kiírja a táblázatba az utolsó vizsgálati adatokat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Új_adat_Click(object sender, EventArgs e)
        {
            try
            {
                // melyik az utolsó elem kiírjuk a táblázatot
                KorrekcióListaFeltöltés();

                int i = Kiirjaatörténelmet();
                if (Tábla1.Rows.Count == 0) return;
                int KöVsorszám = Tábla1.Rows[i].Cells[3].Value.ToString().ToÉrt_Int() + 1;
                string Köv_V_név = MyF.Szöveg_Tisztítás(Tábla1.Rows[i].Cells[2].Value.ToString(), 0, 2);
                double kmu_km = Tábla1.Rows[i].Cells[8].Value.ToString().ToÉrt_Double() + KM_korrekció(DateTime.Parse(Tábla1.Rows[i].Cells[7].Value.ToString()));
                double V2számláló = Tábla1.Rows[i].Cells[20].Value.ToString().ToÉrt_Double();
                string ciklusrend = Tábla1.Rows[i].Cells[13].Value.ToString();
                // beolvassuk a soron következő elemet
                Kiüríti_lapfül();

                // a ciklusrendet kiválasztjuk
                CiklusrendCombo.Text = ciklusrend;
                Vizsgsorszámcombofeltölés();

                VizsgKm.Text = kmu_km.ToString();
                KMUkm.Text = kmu_km.ToString();

                if (Köv_V_név.Trim() == "V2")
                    KövV2_számláló.Text = kmu_km.ToString();
                else
                    KövV2_számláló.Text = V2számláló.ToString();


                Vizsgsorszám.Text = KöVsorszám.ToString();

                Sorszám_választás(KöVsorszám);
            }
            catch (HibásBevittAdat ex)
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
        /// kiszámolja a SAP adathoz képest hány napot volt forgalomba a jármű és mennyit futott az alatt
        /// </summary>
        /// <param name="Dátum"></param>
        /// <returns></returns>
        int KM_korrekció(DateTime Dátum)
        {
            int válasz = 0;
            if (AdatokZserKm != null && AdatokZserKm.Count > 0)
            {
                List<Adat_Főkönyv_Zser_Km> AdatokPSZKm = (from a in AdatokZserKm
                                                          where a.Azonosító == Pályaszám.Text.Trim() &&
                                                          a.Dátum > Dátum
                                                          select a).ToList();
                if (AdatokPSZKm != null && AdatokPSZKm.Count > 0) válasz = AdatokPSZKm.Sum(a => a.Napikm);
            }
            return válasz;
        }

        /// <summary>
        /// Kiüríti a lapfülön lévő adatokat
        /// </summary>
        private void Kiüríti_lapfül()
        {
            Sorszám.Text = "";

            Vizsgsorszám.Text = 0.ToString();
            Vizsgfok.Text = "";
            Vizsgdátumk.Value = DateTime.Today;
            Vizsgdátumv.Value = DateTime.Today;
            VizsgKm.Text = 0.ToString();
            Üzemek.Text = "";

            KMUkm.Text = 0.ToString();

            KMUdátum.Value = DateTime.Today;

            HaviKm.Text = 0.ToString();
            KMUdátum.Value = DateTime.Today;

            KövV.Text = "";
            KövV_Sorszám.Text = "";
            KövV1km.Text = 0.ToString();
            KövV2.Text = "";
            KövV2_Sorszám.Text = "";
            KövV2_számláló.Text = 0.ToString();
            KövV2km.Text = 0.ToString();
        }

        /// <summary>
        /// Feltölti a vizsgasorszám combot a kiválasztott ciklusrend alapján
        /// </summary>
        private void Vizsgsorszámcombofeltölés()
        {
            try
            {
                Vizsgsorszám.Items.Clear();
                if (CiklusrendCombo.Text.Trim() == "") return;

                List<Adat_Ciklus> Adatok = (from a in AdatokCiklus
                                            where a.Típus == CiklusrendCombo.Text.Trim() &&
                                            a.Törölt == "0"
                                            orderby a.Sorszám
                                            select a).ToList();
                foreach (Adat_Ciklus elem in Adatok)
                    Vizsgsorszám.Items.Add(elem.Sorszám.ToString());
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

        /// <summary>
        /// Feltölti a ciklusrend combot a ciklus táblából
        /// </summary>
        private void CiklusrendCombo_feltöltés()
        {
            try
            {
                CiklusrendCombo.Items.Clear();
                List<Adat_Ciklus> Adatok = (from a in AdatokCiklus
                                            where a.Törölt == "0"
                                            orderby a.Típus
                                            select a).ToList();
                foreach (Adat_Ciklus Elem in Adatok)
                    CiklusrendCombo.Items.Add(Elem.Típus);
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

        /// <summary>
        /// Ciklusrend comb kiválasztásakor feltölti a vizsgasorszám combot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CiklusrendCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Vizsgsorszámcombofeltölés();
        }

        /// <summary>
        /// Kiválasztja a telephelyet és feltölti a telephely combot
        /// </summary>
        private void Üzemek_listázása()
        {
            try
            {

                Üzemek.Items.Clear();

                List<Adat_kiegészítő_telephely> Adatok = KézTelep.Lista_Adatok().OrderBy(a => a.Telephelykönyvtár).ToList();
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

        /// <summary>
        /// Vizsgasorszám comb kiválasztásakor feltölti a vizsgafokot és a következő vizsgálatot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Vizsgsorszám_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = Vizsgsorszám.SelectedIndex;
            Sorszám_választás(i);
        }

        /// <summary>
        /// Kiválasztja a sorszámot és kiírja a vizsgálatfokot és a következő vizsgálatot
        /// </summary>
        /// <param name="sorszám"></param>
        private void Sorszám_választás(int sorszám)
        {
            try
            {
                int i = sorszám;
                if (CiklusrendCombo.Text.Trim() == "") return;

                List<Adat_Ciklus> CiklusAdatok = (from a in AdatokCiklus
                                                  where a.Típus == CiklusrendCombo.Text.Trim()
                                                  && a.Törölt == "0"
                                                  orderby a.Sorszám
                                                  select a).ToList();

                string Vizsgálatfok = (from a in CiklusAdatok
                                       where a.Sorszám == i

                                       select a.Vizsgálatfok).FirstOrDefault();

                if (Vizsgálatfok != null)
                    Vizsgfok.Text = Vizsgálatfok;

                // következő vizsgálat sorszáma
                Vizsgálatfok = (from a in CiklusAdatok
                                where a.Sorszám == i + 1
                                select a.Vizsgálatfok).FirstOrDefault();
                if (Vizsgálatfok != null)
                    KövV.Text = Vizsgálatfok;

                KövV_Sorszám.Text = (i + 1).ToString();
                // követekező V2-V3
                KövV2.Text = "J";
                KövV2_Sorszám.Text = "0";
                for (int j = i + 1; j < CiklusAdatok.Count; j++)
                {
                    if (CiklusAdatok[j].Vizsgálatfok.Contains("V2"))
                    {
                        KövV2.Text = CiklusAdatok[j].Vizsgálatfok;
                        KövV2_Sorszám.Text = j.ToString();
                        break;
                    }
                    if (CiklusAdatok[j].Vizsgálatfok.Contains("V3"))
                    {
                        KövV2.Text = CiklusAdatok[j].Vizsgálatfok;
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

        /// <summary>
        /// Rögzíti az utolsó vizsgálati adatokat a táblába
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Utolsó_V_rögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                // leellenőrizzük, hogy minden adat ki van-e töltve
                if (VizsgKm.Text.Trim() == "") throw new HibásBevittAdat("Vizsgálat km számláló állása mező nem lehet üres.");
                if (!long.TryParse(VizsgKm.Text, out long Vizsg_Km)) throw new HibásBevittAdat("Vizsgálat km számláló állása mezőnek számnak kell lennie.");
                if (Vizsgfok.Text.Trim() == "") throw new HibásBevittAdat("Vizsgálat foka beviteli mező nem lehet üres.");
                if (Vizsgsorszám.Text.Trim() == "") throw new HibásBevittAdat("Vizsgálat sorszáma mező nem lehet üres.");
                if (!long.TryParse(Vizsgsorszám.Text, out long Vizsg_sorszám)) throw new HibásBevittAdat("Vizsgálat sorszáma mezőnek számnak kell lennie.");
                if (KMUkm.Text.Trim() == "") throw new HibásBevittAdat("Kmu km mező nem lehet üres.");
                if (!long.TryParse(KMUkm.Text, out long Kmu_km)) throw new HibásBevittAdat("Kmu km mezőnek számnak kell lennie.");
                if (HaviKm.Text.Trim() == "") throw new HibásBevittAdat("Havi km mező nem lehet üres.");
                if (!long.TryParse(HaviKm.Text, out long Havi_km)) throw new HibásBevittAdat("Havi km mezőnek számnak kell lennie.");
                if (Jjavszám.Text.Trim() == "") throw new HibásBevittAdat("Felújítás sorszáma mező nem lehet üres.");
                if (!long.TryParse(Jjavszám.Text, out long Jjav_szám)) throw new HibásBevittAdat("Felújítás sorszáma mezőnek számnak kell lennie.");
                if (TEljesKmText.Text.Trim() == "") throw new HibásBevittAdat("Üzembehelyezés óta futott km mező nem lehet üres.");
                if (!long.TryParse(TEljesKmText.Text, out long Teljes_kmText)) throw new HibásBevittAdat("Üzembehelyezés óta futott km mezőnek számnak kell lennie.");
                if (CiklusrendCombo.Text.Trim() == "") throw new HibásBevittAdat("Ütemezés típusa nem lehet üres.");
                if (KövV2_Sorszám.Text.Trim() == "") throw new HibásBevittAdat("Következő V2-V3 sorszám mező nem lehet üres.");
                if (!long.TryParse(KövV2_Sorszám.Text, out long Kövv2_sorszám)) throw new HibásBevittAdat("Következő V2-V3 sorszám mezőnek számnak kell lennie.");
                if (KövV_Sorszám.Text.Trim() == "") throw new HibásBevittAdat("Következő V sorszám mező nem lehet üres.");
                if (!long.TryParse(KövV_Sorszám.Text, out long kövv_sorszám)) throw new HibásBevittAdat("Következő V sorszám mezőnek számnak kell lennie.");
                if (KövV2km.Text.Trim() == "") throw new HibásBevittAdat("Következő V2-V2 km mező nem lehet üres.");
                if (!long.TryParse(KövV2km.Text, out long kövv2km)) throw new HibásBevittAdat("Következő V2-V3 km mezőnek számnak kell lennie.");
                if (!long.TryParse(KövV2_számláló.Text, out long V2V3Számláló)) throw new HibásBevittAdat("Következő V2-V3 számláló állás mezőnek számnak kell lennie.");
                if (!long.TryParse(Sorszám.Text, out long ID)) ID = 0;

                // megnézzük az adatbázist, ha nincs ilyen kocsi ICS benne akkor rögzít máskülönben az adatokat módosítja
                AdatokFőJármű = KézJármű.Lista_Adatok("Főmérnökség");
                Adat_Jármű ElemJármű = (from a in AdatokFőJármű
                                        where a.Törölt == false &&
                                        a.Azonosító == Pályaszám.Text.Trim() &&
                                        (a.Valóstípus == "ICS" || a.Valóstípus == "KCSV-7")
                                        select a).FirstOrDefault();

                if (ElemJármű != null)
                {
                    Adat_T5C5_Kmadatok ADAT = new Adat_T5C5_Kmadatok(
                                     ID,
                                     Pályaszám.Text.Trim(),
                                     Jjav_szám,
                                     Kmu_km,
                                     KMUdátum.Value,
                                     Vizsgfok.Text.Trim(),
                                     Vizsgdátumk.Value,
                                     Vizsgdátumv.Value,
                                     Vizsg_Km,
                                     Havi_km,
                                     Vizsg_sorszám,
                                     Utolsófelújításdátuma.Value,
                                     Teljes_kmText,
                                     CiklusrendCombo.Text.Trim(),
                                     Üzemek.Text.Trim(),
                                     Kövv2_sorszám,
                                     KövV2.Text.Trim(),
                                     kövv_sorszám,
                                     KövV.Text.Trim(),
                                     false,
                                     V2V3Számláló);

                    if (Sorszám.Text.Trim() == "")
                        KézICSKmadatok.Rögzítés(ADAT);
                    else
                        KézICSKmadatok.Módosítás(ADAT);
                    MessageBox.Show("Az adatok rögzítése megtörtént. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("A pályaszám nem ICS-KCSV! ", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Kiirjaatörténelmet();
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

        /// <summary>
        /// Törlés gomb megnyomásakor törli a kiválasztott adatokat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (long.TryParse(Sorszám.Text.Trim(), out long sorszám))
                {
                    if (MessageBox.Show("Valóban töröljük az adatsort?", "Biztonsági kérdés", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        KézICSKmadatok.Törlés(sorszám);
                        Kiirjaatörténelmet();
                        Fülek.SelectedIndex = 3;
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

        /// <summary>
        /// SAP adatok gomb megnyomásakor betölti az excel táblát és a benne lévő adatokat beírja a táblába
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                await Task.Run(() => SAP_Adatokbeolvasása.Km_beolvasó(_fájlexc, "ICS"));
                timer1.Enabled = false;
                Holtart.Ki();
                MessageBox.Show("Az adatok beolvasása megtörtént !", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        #endregion


        #region Vizsgálati adatok lapfül
        /// <summary>
        /// Kiírja a vizsgálati adatokat a táblázatba
        /// </summary>
        /// <returns></returns>
        int Kiirjaatörténelmet()
        {
            int válasz = 0;
            try
            {
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
                Tábla1.Columns[13].Width = 80;
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
                AdatokICSKmadatok = KézICSKmadatok.Lista_Adatok().OrderByDescending(a => a.ID).ToList();
                List<Adat_T5C5_Kmadatok> Adatok = (from a in AdatokICSKmadatok
                                                   where a.Törölt == false
                                                   && a.Azonosító == Pályaszám.Text.Trim()
                                                   orderby a.Vizsgdátumk
                                                   select a).ToList();
                foreach (Adat_T5C5_Kmadatok rekord in Adatok)
                {
                    Tábla1.RowCount++;
                    int i = Tábla1.RowCount - 1;
                    Tábla1.Rows[i].Cells[0].Value = rekord.ID;
                    Tábla1.Rows[i].Cells[1].Value = rekord.Azonosító.Trim();
                    Tábla1.Rows[i].Cells[2].Value = rekord.Vizsgfok.Trim();
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
                    Tábla1.Rows[i].Cells[13].Value = rekord.Ciklusrend.Trim();
                    Tábla1.Rows[i].Cells[14].Value = rekord.Teljeskm;
                    if (rekord.V2végezte.Trim() != "_")
                        Tábla1.Rows[i].Cells[15].Value = rekord.V2végezte.Trim();
                    Tábla1.Rows[i].Cells[16].Value = rekord.KövV.Trim();
                    Tábla1.Rows[i].Cells[17].Value = rekord.KövV_sorszám;
                    Tábla1.Rows[i].Cells[18].Value = rekord.KövV2.Trim();
                    Tábla1.Rows[i].Cells[19].Value = rekord.KövV2_sorszám;
                    Tábla1.Rows[i].Cells[20].Value = rekord.V2V3Számláló;
                }

                Tábla1.Visible = true;
                Tábla1.Refresh();

                válasz = Tábla1.RowCount - 1;
            }
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

        /// <summary>
        /// Kiválasztja a táblázatban lévő adatokat és kiírja a Utolsó vizsgálati adatok lapfülre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            Fülek.SelectedIndex = 2;
        }

        /// <summary>
        /// Frissíti a vizsgálati adatokat a táblázatban
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VizsAdat_Frissít_Click(object sender, EventArgs e)
        {
            Kiirjaatörténelmet();
        }

        /// <summary>
        /// Mentés Excel fájlba gomb megnyomásakor menti a táblázatot excel fájlba
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VizsAdat_Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla1.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
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


        #region Állomány tábla
        /// <summary>
        /// Állomány táblát készít a telephelyen lévő járművekről
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


        #region Vizsgálat_ütemező
        /// <summary>
        /// Frissíti a vizsgálati ütemezőt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ütem_frissít_Click(object sender, EventArgs e)
        {
            Ütemező_lekérdezés();
            Ütemezettkocsik();
        }

        /// <summary>
        /// Lekérdezi járművenként az aktuális állapotokat
        /// </summary>
        private void Ütemező_lekérdezés()
        {
            try
            {
                // kilistázzuk a adatbázis adatait
                List<Adat_Jármű> AdatokJ = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                AdatokJ = (from a in AdatokJ
                           where a.Törölt == false
                           && (a.Valóstípus.Trim() == "ICS" || a.Valóstípus == "KCSV-7")
                           orderby a.Azonosító
                           select a).ToList();

                AdatokICSKmadatok = KézICSKmadatok.Lista_Adatok();

                Főkönyv_Funkciók.Napiállók(Cmbtelephely.Text.Trim());
                AdatokHiba = KézHiba.Lista_Adatok(Cmbtelephely.Text.Trim());
                AdatokVezénylés = KézVezénylés.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum_ütem.Value);
                KorrekcióListaFeltöltés();

                Holtart.Be();

                Tábla_ütemező.Rows.Clear();
                Tábla_ütemező.Columns.Clear();
                Tábla_ütemező.Refresh();
                Tábla_ütemező.Visible = false;
                Tábla_ütemező.ColumnCount = 24;
                // fejléc elkészítése
                Tábla_ütemező.Columns[0].HeaderText = "Psz";
                Tábla_ütemező.Columns[0].Width = 80;
                Tábla_ütemező.Columns[0].Frozen = true;
                Tábla_ütemező.Columns[1].HeaderText = "KM J-óta";
                Tábla_ütemező.Columns[1].Width = 80;
                Tábla_ütemező.Columns[1].Visible = false;
                Tábla_ütemező.Columns[2].HeaderText = "Frissítés Dátum";
                Tábla_ütemező.Columns[2].Width = 100;
                Tábla_ütemező.Columns[3].HeaderText = "Vizsg. Dátum";
                Tábla_ütemező.Columns[3].Width = 100;
                Tábla_ütemező.Columns[4].HeaderText = "Vizsg KM állás";
                Tábla_ütemező.Columns[4].Width = 150;
                Tábla_ütemező.Columns[5].HeaderText = "Utolsó Vizsg. foka";
                Tábla_ütemező.Columns[5].Width = 150;
                Tábla_ütemező.Columns[6].HeaderText = "V óta futott km korrigált";
                Tábla_ütemező.Columns[6].Width = 80;
                Tábla_ütemező.Columns[7].HeaderText = "V2 óta futott km korrigált";
                Tábla_ütemező.Columns[7].Width = 80;
                Tábla_ütemező.Columns[8].HeaderText = "V3 óta futott km korrigált";
                Tábla_ütemező.Columns[8].Width = 80;
                Tábla_ütemező.Columns[9].HeaderText = "Ciklusrend";
                Tábla_ütemező.Columns[9].Width = 100;
                Tábla_ütemező.Columns[10].HeaderText = "Követk. vizsg.";
                Tábla_ütemező.Columns[10].Width = 80;
                Tábla_ütemező.Columns[11].HeaderText = "Jármű hibák";
                Tábla_ütemező.Columns[11].Width = 200;

                Tábla_ütemező.Columns[12].Visible = false;
                Tábla_ütemező.Columns[13].Visible = false;
                Tábla_ütemező.Columns[14].HeaderText = "Mit kér";
                Tábla_ütemező.Columns[14].Width = 100;
                Tábla_ütemező.Columns[14].Visible = false;
                Tábla_ütemező.Columns[15].HeaderText = "Rendelés szám";
                Tábla_ütemező.Columns[15].Width = 100;
                Tábla_ütemező.Columns[15].Visible = false;
                Tábla_ütemező.Columns[16].HeaderText = "Vizsgálat";
                Tábla_ütemező.Columns[16].Width = 100;
                Tábla_ütemező.Columns[16].Visible = false;
                Tábla_ütemező.Columns[17].HeaderText = "Takarítás";
                Tábla_ütemező.Columns[17].Width = 100;
                Tábla_ütemező.Columns[17].Visible = false;
                Tábla_ütemező.Columns[18].HeaderText = "Járműstátus";
                Tábla_ütemező.Columns[18].Width = 100;
                Tábla_ütemező.Columns[18].Visible = false;
                Tábla_ütemező.Columns[19].HeaderText = "Sorszám";
                Tábla_ütemező.Columns[19].Width = 80;
                Tábla_ütemező.Columns[19].Visible = false;

                Tábla_ütemező.Columns[20].HeaderText = "V óta futott km ";
                Tábla_ütemező.Columns[20].Width = 80;
                Tábla_ütemező.Columns[21].HeaderText = "V2 óta futott km ";
                Tábla_ütemező.Columns[21].Width = 80;
                Tábla_ütemező.Columns[22].HeaderText = "V3 óta futott km ";
                Tábla_ütemező.Columns[22].Width = 80;
                Tábla_ütemező.Columns[23].HeaderText = "km korr ";
                Tábla_ütemező.Columns[23].Width = 100;

                // kiírjuk a pályaszámokat


                int i;
                foreach (Adat_Jármű rekord in AdatokJ)
                {

                    Tábla_ütemező.RowCount++;
                    i = Tábla_ütemező.RowCount - 1;
                    Tábla_ütemező.Rows[i].Cells[0].Value = rekord.Azonosító.Trim();
                    Tábla_ütemező.Rows[i].Cells[18].Value = rekord.Státus;

                    Adat_T5C5_Kmadatok rekordICS = (from a in AdatokICSKmadatok
                                                    where a.Azonosító == rekord.Azonosító &&
                                                    a.Törölt == false
                                                    orderby a.Vizsgdátumk descending
                                                    select a).FirstOrDefault();

                    if (rekordICS != null)
                    {
                        Tábla_ütemező.Rows[i].Cells[14].Value = "0";
                        Tábla_ütemező.Rows[i].Cells[15].Value = "0";
                        Tábla_ütemező.Rows[i].Cells[16].Value = "0";
                        Tábla_ütemező.Rows[i].Cells[19].Value = "0";

                        Tábla_ütemező.Rows[i].Cells[1].Value = rekordICS.KMUkm;
                        Tábla_ütemező.Rows[i].Cells[2].Value = rekordICS.KMUdátum.ToString("yyyy.MM.dd");
                        Tábla_ütemező.Rows[i].Cells[3].Value = rekordICS.Vizsgdátumv.ToString("yyyy.MM.dd");
                        Tábla_ütemező.Rows[i].Cells[4].Value = rekordICS.Vizsgkm;
                        Tábla_ütemező.Rows[i].Cells[5].Value = rekordICS.Vizsgfok;

                        //km korrekció
                        int korrekció = 0;
                        List<Adat_Főkönyv_Zser_Km> AdatokPSZKm = (from a in AdatokZserKm
                                                                  where a.Azonosító == rekord.Azonosító &&
                                                                  a.Dátum > rekordICS.KMUdátum
                                                                  select a).ToList();
                        if (AdatokPSZKm != null && AdatokPSZKm.Count > 0) korrekció = AdatokPSZKm.Sum(a => a.Napikm);
                        Tábla_ütemező.Rows[i].Cells[23].Value = korrekció;

                        // ha J akkor nem kell különbséget képezni
                        if (rekordICS.Vizsgsorszám == 0)
                        {
                            Tábla_ütemező.Rows[i].Cells[20].Value = rekordICS.KMUkm;
                            Tábla_ütemező.Rows[i].Cells[6].Value = rekordICS.KMUkm + korrekció;
                        }
                        else
                        {
                            Tábla_ütemező.Rows[i].Cells[20].Value = (rekordICS.KMUkm - rekordICS.Vizsgkm);
                            Tábla_ütemező.Rows[i].Cells[6].Value = (rekordICS.KMUkm - rekordICS.Vizsgkm) + korrekció;
                        }

                        Tábla_ütemező.Rows[i].Cells[9].Value = rekordICS.Ciklusrend;
                        Tábla_ütemező.Rows[i].Cells[19].Value = rekordICS.Vizsgsorszám;

                        // utolsó V2 vizsgálat kiírása
                        Adat_T5C5_Kmadatok rekordICSV2 = (from a in AdatokICSKmadatok
                                                          where a.Azonosító == rekord.Azonosító &&
                                                          a.Törölt == false &&
                                                          a.Vizsgfok.Contains("V2")
                                                          orderby a.Vizsgdátumk descending
                                                          select a).FirstOrDefault();
                        if (rekordICSV2 != null)
                        {
                            Tábla_ütemező.Rows[i].Cells[21].Value = rekordICS.KMUkm - rekordICSV2.Vizsgkm;
                            Tábla_ütemező.Rows[i].Cells[7].Value = rekordICS.KMUkm - rekordICSV2.Vizsgkm + korrekció;
                        }

                        // utolsó V3 vizsgálat kiírása
                        Adat_T5C5_Kmadatok rekordICSV3 = (from a in AdatokICSKmadatok
                                                          where a.Azonosító == rekord.Azonosító &&
                                                          a.Törölt == false &&
                                                          a.Vizsgfok.Contains("V3")
                                                          orderby a.Vizsgdátumk descending
                                                          select a).FirstOrDefault();
                        if (rekordICSV3 != null)
                        {
                            Tábla_ütemező.Rows[i].Cells[22].Value = rekordICS.KMUkm - rekordICSV3.Vizsgkm;
                            Tábla_ütemező.Rows[i].Cells[8].Value = rekordICS.KMUkm - rekordICSV3.Vizsgkm + korrekció;
                        }


                        Adat_Ciklus ElemCiklus = (from a in AdatokCiklus
                                                  where a.Típus == rekordICS.Ciklusrend.Trim() &&
                                                  a.Sorszám == rekordICS.Vizsgsorszám + 1
                                                  select a).FirstOrDefault();

                        if (ElemCiklus != null)
                            Tábla_ütemező.Rows[i].Cells[10].Value = ElemCiklus.Vizsgálatfok;
                        else
                            Tábla_ütemező.Rows[i].Cells[10].Value = "";
                        Adat_Nap_Hiba ElemHiba = (from a in AdatokHiba
                                                  where a.Azonosító == rekord.Azonosító
                                                  select a).FirstOrDefault();
                        if (ElemHiba != null) Tábla_ütemező.Rows[i].Cells[11].Value = $"{ElemHiba.Üzemképtelen.Trim()}-{ElemHiba.Beálló.Trim()}-{ElemHiba.Üzemképeshiba.Trim()}";

                        Adat_Vezénylés ElemVezénylés = (from a in AdatokVezénylés
                                                        where a.Dátum >= Dátum_ütem.Value &&
                                                        a.Törlés == 0 &&
                                                        a.Azonosító == rekord.Azonosító
                                                        select a).FirstOrDefault();
                        if (ElemVezénylés != null)
                        {
                            if (ElemVezénylés.Vizsgálatraütemez == 1)
                                Tábla_ütemező.Rows[i].Cells[16].Value = "1";
                            else
                                Tábla_ütemező.Rows[i].Cells[16].Value = "0";

                            Tábla_ütemező.Rows[i].Cells[15].Value = ElemVezénylés.Rendelésiszám.Trim();
                            Tábla_ütemező.Rows[i].Cells[14].Value = ElemVezénylés.Státus;
                        }

                    }

                    Holtart.Lép();
                }
                Tábla_ütemező.Refresh();
                Tábla_ütemező.Sort(Tábla_ütemező.Columns[6], System.ComponentModel.ListSortDirection.Descending);
                Tábla_ütemező.Visible = true;
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

        /// <summary>
        /// Vezénylés adatokat frissíti a táblázatban
        /// </summary>
        private void Vezénylés_listázása()
        {
            try
            {
                AdatokVezénylés = KézVezénylés.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum_ütem.Value);

                foreach (DataGridViewRow Sor in Tábla_ütemező.Rows)
                {
                    Adat_Vezénylés ElemVezénylés = (from a in AdatokVezénylés
                                                    where a.Dátum >= Dátum_ütem.Value &&
                                                    a.Törlés == 0 &&
                                                    a.Azonosító == Sor.Cells[0].Value.ToStrTrim()
                                                    select a).FirstOrDefault();
                    if (ElemVezénylés != null)
                    {
                        if (ElemVezénylés.Vizsgálatraütemez == 1)
                            Sor.Cells[16].Value = "1";
                        else
                            Sor.Cells[16].Value = "0";
                        Sor.Cells[15].Value = ElemVezénylés.Rendelésiszám.Trim();
                        Sor.Cells[14].Value = ElemVezénylés.Státus;
                    }
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

        /// <summary>
        /// Kiválasztja a táblázatban lévő adatokat és rögzítési ablakot nyitja meg
        /// </summary>
        /// <param name="sor"></param>
        private void Táblázatba_kattint(int sor)
        {
            try
            {
                if (Tábla_ütemező.Rows[sor].Cells[0].Value == null) throw new HibásBevittAdat("Nincs kijelölve érvényes sor.");
                if (!int.TryParse(Tábla_ütemező.Rows[sor].Cells[14].Value.ToString(), out int állapot)) állapot = 0;
                bool ütemez = true;
                if (Tábla_ütemező.Rows[sor].Cells[16].Value.ToStrTrim() == "0") ütemez = false;
                if (!int.TryParse(Tábla_ütemező.Rows[sor].Cells[19].Value.ToString(), out int v_sorszám)) v_sorszám = 0;
                if (!int.TryParse(Tábla_ütemező.Rows[sor].Cells[6].Value.ToString(), out int v_km)) v_km = 0;

                Adat_ICS_Ütem Küld = new Adat_ICS_Ütem(
                    Tábla_ütemező.Rows[sor].Cells[0].Value.ToStrTrim(),
                    állapot,
                    ütemez,
                    Tábla_ütemező.Rows[sor].Cells[15].Value.ToStrTrim(),
                    v_sorszám,
                    Tábla_ütemező.Rows[sor].Cells[5].Value.ToStrTrim(),
                    v_km,
                    Tábla_ütemező.Rows[sor].Cells[10].Value.ToStrTrim(),
                    v_sorszám + 1
                    );

                if (Új_Ablak_ICS_KCSV_segéd != null) Új_Ablak_ICS_KCSV_segéd = null;

                Új_Ablak_ICS_KCSV_segéd = new Ablak_ICS_KCSV_segéd(Dátum_ütem.Value, Cmbtelephely.Text.Trim(), Küld);
                Új_Ablak_ICS_KCSV_segéd.FormClosed += Új_Ablak_ICS_KCSV_segéd_FormClosed;
                Új_Ablak_ICS_KCSV_segéd.Változás += Vezénylés_listázása;
                Új_Ablak_ICS_KCSV_segéd.Változás += Ütemezettkocsik;
                Új_Ablak_ICS_KCSV_segéd.Show();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        Ablak_ICS_KCSV_segéd Új_Ablak_ICS_KCSV_segéd;

        /// <summary>
        /// Kiválasztja a táblázatban lévő adatokat és rögzítési ablakot nyitja meg
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tábla_ütemező_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) throw new HibásBevittAdat("Nincs kijelölve érvényes sor.");
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

        /// <summary>
        /// Bezáráskor törli a segédablakot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Új_Ablak_ICS_KCSV_segéd_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_ICS_KCSV_segéd = null;
        }

        /// <summary>
        /// Ütemezett kocsik táblázatot készít a vizsgálatra ütemezett kocsikról
        /// </summary>
        private void Ütemezettkocsik()
        {
            try
            {
                Tábla_vezénylés.Rows.Clear();
                Tábla_vezénylés.Columns.Clear();
                Tábla_vezénylés.Refresh();
                Tábla_vezénylés.Visible = false;
                Tábla_vezénylés.ColumnCount = 4;

                // fejléc elkészítése
                Tábla_vezénylés.Columns[0].HeaderText = "Dátum";
                Tábla_vezénylés.Columns[0].Width = 100;
                Tábla_vezénylés.Columns[1].HeaderText = "Psz.";
                Tábla_vezénylés.Columns[1].Width = 60;
                Tábla_vezénylés.Columns[2].HeaderText = "Vizsgálat";
                Tábla_vezénylés.Columns[2].Width = 80;
                Tábla_vezénylés.Columns[3].HeaderText = "";
                Tábla_vezénylés.Columns[3].Width = 80;

                DateTime kezdet = MyF.Nap0000(Dátum_ütem.Value.AddDays(-5));
                DateTime vége = MyF.Nap2359(Dátum_ütem.Value.AddDays(5));
                List<Adat_Vezénylés> Adatok = KézVezénylés.Lista_Adatok(Cmbtelephely.Text.Trim(), kezdet);
                Adatok = (from a in Adatok
                          where a.Törlés == 0
                          && a.Dátum >= kezdet
                          && a.Dátum <= vége
                          orderby a.Dátum, a.Vizsgálat, a.Azonosító
                          select a).ToList();


                foreach (Adat_Vezénylés rekord in Adatok)
                {
                    if (rekord.Vizsgálatraütemez == 1)
                    {
                        Tábla_vezénylés.RowCount++;
                        int i = Tábla_vezénylés.RowCount - 1;
                        Tábla_vezénylés.Rows[i].Cells[0].Value = rekord.Dátum.ToString("yyyy.MM.dd");
                        Tábla_vezénylés.Rows[i].Cells[1].Value = rekord.Azonosító.Trim();
                        Tábla_vezénylés.Rows[i].Cells[2].Value = rekord.Vizsgálat.Trim();
                        if (rekord.Státus == 3)
                            Tábla_vezénylés.Rows[i].Cells[3].Value = "Beálló";
                        else
                            Tábla_vezénylés.Rows[i].Cells[3].Value = "Benn marad";

                    }
                }
                Tábla_vezénylés.Refresh();
                Tábla_vezénylés.Visible = true;
            }
            catch (HibásBevittAdat ex)
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
        /// Kiválasztja a táblázatban lévő adatokat és rögzítési ablakot nyitja meg
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tábla_vezénylés_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Tábla_vezénylés.Rows.Count < 1)
                    return;

                Dátum_ütem.Value = DateTime.Parse(Tábla_vezénylés.Rows[e.RowIndex].Cells[0].Value.ToString());
                Ütemező_lekérdezés();
                if (Tábla_ütemező.Rows.Count < 1)
                    return;
                // megkeressük a nagytáblába, majd kiíratjuk

                for (int i = 0; i < Tábla_ütemező.Rows.Count; i++)
                {
                    if (Tábla_vezénylés.Rows[e.RowIndex].Cells[1].Value.ToStrTrim() == Tábla_ütemező.Rows[i].Cells[0].Value.ToStrTrim())
                    {
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

        /// <summary>
        /// Színezés a táblázatban
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tábla_ütemező_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {

                if (Tábla_ütemező.RowCount < 1)
                    return;
                foreach (DataGridViewRow row in Tábla_ütemező.Rows)
                {
                    if (row.Cells[18].Value.ToString() == "4")
                    {
                        row.DefaultCellStyle.ForeColor = Color.White;
                        row.DefaultCellStyle.BackColor = Color.IndianRed;
                        row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f);
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

        /// <summary>
        /// Vezénylésbeírás gomb megnyomásakor a kiválasztott járművek státuszát módosítja és a hibák közé beírjuk a vizsgálatot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Vezénylésbeírás_Click(object sender, EventArgs e)
        {
            try
            {
                List<Adat_Vezénylés> AdatokVezénylés = KézVezénylés.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum_ütem.Value);
                AdatokVezénylés = (from a in AdatokVezénylés
                                   where a.Törlés == 0
                                   && a.Dátum.ToShortDateString() == Dátum_ütem.Value.ToShortDateString()
                                   orderby a.Azonosító
                                   select a).ToList();

                AdatokJármű = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());    // Módosítjuk a jármű státuszát
                List<Adat_Jármű_hiba> AdatokHiba = KézJárműHiba.Lista_Adatok(Cmbtelephely.Text.Trim());     // megnyitjuk a hibákat

                Holtart.Be();
                // ha van ütemezett kocsi
                foreach (Adat_Vezénylés rekordütemez in AdatokVezénylés)
                {
                    long újstátus = 0;
                    Holtart.Lép();
                    if (rekordütemez.Vizsgálatraütemez == 1)
                    {
                        // hiba leírása
                        string szöveg1 = $"{rekordütemez.Vizsgálat.Trim()}-{rekordütemez.Vizsgálatszám}-{rekordütemez.Dátum:yyyy.MM.dd} ";
                        string szöveg3 = $"{rekordütemez.Vizsgálat.Trim()}-{rekordütemez.Vizsgálatszám}";
                        if (rekordütemez.Státus == 4)
                            szöveg1 += $" Maradjon benn ";
                        else
                            szöveg1 += $" Beálló ";

                        // Megnézzük, hogy volt-e már rögzítve ilyen szöveg
                        Adat_Jármű_hiba ElemHiba = (from a in AdatokHiba
                                                    where a.Azonosító == rekordütemez.Azonosító &&
                                                    a.Hibaleírása.Contains(szöveg3)
                                                    select a).FirstOrDefault();
                        // ha már volt ilyen szöveg rögzítve a pályaszámhoz akkor nem rögzítjük mégegyszer
                        if (ElemHiba == null)
                        {
                            // hibák számát emeljük és státus állítjuk ha kell
                            Adat_Jármű ElemJármű = (from a in AdatokJármű
                                                    where a.Azonosító == rekordütemez.Azonosító
                                                    select a).FirstOrDefault();
                            long hibáksorszáma = 0;
                            string típusa = "";
                            long státus = 0;
                            if (ElemJármű != null)
                            {
                                hibáksorszáma = ElemJármű.Hibák;
                                típusa = ElemJármű.Típus;
                                státus = ElemJármű.Státus;
                            }
                            long hiba = hibáksorszáma + 1;

                            if (státus != 4) // ha 4 státusa akkor nem kell módosítani.
                            {
                                // ha a következő napra ütemez
                                if (DateTime.Today.AddDays(1).ToString("yyyy.MM.dd") == Dátum_ütem.Value.ToString("yyyy.MM.dd"))
                                {
                                    if (rekordütemez.Státus == 4)
                                        státus = 4;
                                    else
                                        státus = 3;
                                }
                                else if (státus < 4) státus = 3;
                                // ha ma van  
                                if (DateTime.Today.ToString("yyyy.MM.dd") == Dátum_ütem.Value.ToString("yyyy.MM.dd")) státus = 4;
                            }
                            else
                            {
                                újstátus = 1;
                            }

                            // rögzítjük a villamos.mdb-be
                            if (státus == 4 & újstátus == 0)
                            {
                                Adat_Jármű ADATJármű = new Adat_Jármű(
                                     rekordütemez.Azonosító.Trim(),
                                     hiba,
                                     státus,
                                     DateTime.Now);
                                KézJármű.Módosítás_Státus_Hiba_Dátum(Cmbtelephely.Text.Trim(), ADATJármű);
                            }
                            else
                            {
                                Adat_Jármű ADATJármű = new Adat_Jármű(
                                     rekordütemez.Azonosító.Trim(),
                                     hiba,
                                     státus);
                                KézJármű.Módosítás_Hiba_Státus(Cmbtelephely.Text.Trim(), ADATJármű);
                            }

                            // beírjuk a hibákat
                            Adat_Jármű_hiba ADAT = new Adat_Jármű_hiba(
                                     Program.PostásNév.Trim(),
                                     rekordütemez.Státus == 4 ? 4 : 3,
                                     szöveg1.Trim(),
                                     DateTime.Now,
                                     false,
                                     típusa.Trim(),
                                     rekordütemez.Azonosító.Trim(),
                                     hibáksorszáma);
                            KézJárműHiba.Rögzítés(Cmbtelephely.Text.Trim(), ADAT);
                        }
                    }
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

        /// <summary>
        /// Mentés Excel fájlba
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla_ütemező.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"ICS_KCSV_ütemzés_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Tábla_ütemező, false);
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

        /// <summary>
        /// Dátum változásakor frissíti a táblázatot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dátum_ütem_ValueChanged(object sender, EventArgs e)
        {
            Ütemezettkocsik();
        }
        #endregion


        #region előtervező
        /// <summary>
        /// Comboba feltölti a főmérnökség járművei közül az ICS és KCSV kocsikat
        /// </summary>
        private void Pszlista()
        {
            try
            {
                PszJelölő.Items.Clear();
                AdatokFőJármű = KézJármű.Lista_Adatok("Főmérnökség");
                List<Adat_Jármű> Adatok = (from a in AdatokFőJármű
                                           where a.Törölt == false
                                           && (a.Valóstípus == "ICS" || a.Valóstípus == "KCSV-7")
                                           orderby a.Azonosító
                                           select a).ToList();

                foreach (Adat_Jármű rekord in Adatok)
                    PszJelölő.Items.Add(rekord.Azonosító.Trim());
            }
            catch (HibásBevittAdat ex)
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
        /// Telephely comboba feltölti azon üzemeket ahoil van főmérnökség járművei közül az ICS és KCSV kocsikat
        /// </summary>
        private void Telephelylista()
        {
            try
            {
                Telephely.Items.Clear();
                AdatokFőJármű = KézJármű.Lista_Adatok("Főmérnökség");
                List<string> Adatok = (from a in AdatokFőJármű
                                       where a.Törölt == false
                                       && (a.Valóstípus == "ICS" || a.Valóstípus == "KCSV-7")
                                       orderby a.Azonosító
                                       select a.Üzem).Distinct().ToList();

                foreach (string rekord in Adatok)
                    Telephely.Items.Add(rekord.Trim());
            }
            catch (HibásBevittAdat ex)
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
        /// Havi km 0-ra állítva, akkor a kocsi km-el számol
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Option5_Click(object sender, EventArgs e)
        {
            // Kocsi havi km
            Text1.Text = "0";
            HavikmICS = 0;
        }

        /// <summary>
        /// Kiválasztott kocsik átlagát számolja ki
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Option6_Click(object sender, EventArgs e)
        {
            try
            {
                // 5000 állít be ha nincs kiválasztva üzem
                if (Telephely.Text.Trim() == "")
                {
                    Option8.Checked = true;
                    Text1.Text = "5000";
                    HavikmICS = 5000;
                    return;
                }

                PályaszámokJeltelenítése();
                Frissíti_a_pályaszámokat();
                AdatokICSKmadatok = KézICSKmadatok.Lista_Adatok().OrderByDescending(a => a.ID).ToList();
                // kilistázzuk a adatbázis adatait

                double típusátlag = 0;
                int darabszám = 0;
                Holtart.Be();


                for (int j = 0; j < PszJelölő.CheckedItems.Count; j++)
                {
                    Holtart.Lép();

                    Adat_T5C5_Kmadatok Elem = (from a in AdatokICSKmadatok
                                               where a.Azonosító == PszJelölő.CheckedItems[j].ToStrTrim()
                                               orderby a.Vizsgdátumk descending
                                               select a).FirstOrDefault();
                    if (Elem != null)
                    {
                        típusátlag += Elem.Havikm;
                        darabszám++;
                    }

                }
                Holtart.Ki();
                if (darabszám != 0) típusátlag /= darabszám;

                HavikmICS = (long)Math.Round(típusátlag);
                Text1.Text = HavikmICS.ToString();
            }
            catch (HibásBevittAdat ex)
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
        /// Típus átlagot számol
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Option7_Click(object sender, EventArgs e)
        {
            try
            {
                // típusátlag
                // kilistázzuk a adatbázis adatait
                AdatokICSKmadatok = KézICSKmadatok.Lista_Adatok().OrderByDescending(a => a.ID).ToList();
                double típusátlag = 0;
                int ii = 0;
                Holtart.Be();

                for (int j = 0; j < PszJelölő.Items.Count; j++)
                {
                    Holtart.Lép();
                    Adat_T5C5_Kmadatok Elem = (from a in AdatokICSKmadatok
                                               where a.Azonosító == PszJelölő.Items[j].ToStrTrim()
                                               orderby a.Vizsgdátumk descending
                                               select a).FirstOrDefault();
                    if (Elem != null)
                    {
                        típusátlag += Elem.Havikm;
                        ii++;
                    }

                }
                Holtart.Ki();
                if (ii != 0) típusátlag /= ii;
                HavikmICS = (long)Math.Round(típusátlag);
                Text1.Text = HavikmICS.ToString();
            }
            catch (HibásBevittAdat ex)
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
        /// Kijelöltek átlaga
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Option9_Click(object sender, EventArgs e)
        {
            try
            {
                // 'kijelöltek átlaga
                double típusátlag = 0;
                int ii = 0;
                Holtart.Be();

                for (int j = 0; j < PszJelölő.CheckedItems.Count; j++)
                {
                    Holtart.Lép();
                    if (PszJelölő.GetItemChecked(j))
                    {
                        Adat_T5C5_Kmadatok Elem = (from a in AdatokICSKmadatok
                                                   where a.Azonosító == PszJelölő.CheckedItems[j].ToStrTrim()
                                                   orderby a.Vizsgdátumk descending
                                                   select a).FirstOrDefault();
                        if (Elem != null)
                        {
                            típusátlag += Elem.Havikm;
                            ii++;
                        }
                    }
                }
                Holtart.Ki();
                if (ii != 0) típusátlag /= ii;
                HavikmICS = (long)Math.Round(típusátlag);
                Text1.Text = HavikmICS.ToString();
            }
            catch (HibásBevittAdat ex)
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
        /// Üzemhez tartozó pályaszámok kijelölése
        /// </summary>
        private void Frissíti_a_pályaszámokat()
        {
            try
            {
                if (Telephely.Text.Trim() == "") return;
                AdatokFőJármű = KézJármű.Lista_Adatok("Főmérnökség");
                List<Adat_Jármű> Adatok = (from a in AdatokFőJármű
                                           where a.Törölt == false
                                           && (a.Valóstípus == "ICS" || a.Valóstípus == "KCSV-7")
                                           orderby a.Azonosító
                                           select a).ToList();
                PszJelölő.Items.Clear();
                int i = 0;
                foreach (Adat_Jármű rekord in Adatok)
                {
                    PszJelölő.Items.Add(rekord.Azonosító);
                    if (Telephely.Text.Trim() == rekord.Üzem.Trim()) PszJelölő.SetItemChecked(i, true);
                    i++;
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

        /// <summary>
        /// Üzemekhez tartozó pályaszámok kijelölése
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Command2_Click(object sender, EventArgs e)
        {
            Frissíti_a_pályaszámokat();
        }

        /// <summary>
        /// Minden pályaszám kijelölése
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mindentkijelöl_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < PszJelölő.Items.Count; i++)
                PszJelölő.SetItemChecked(i, true);
        }

        /// <summary>
        /// Kijelölés törlése
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Kijelöléstörlése_Click(object sender, EventArgs e)
        {
            PályaszámokJeltelenítése();
        }

        /// <summary>
        /// KijelölésEK törlése
        /// </summary>
        private void PályaszámokJeltelenítése()
        {
            for (int i = 0; i < PszJelölő.Items.Count; i++)
                PszJelölő.SetItemChecked(i, false);
        }

        /// <summary>
        ///    Hónapok számának meghatározása
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Text2_Leave(object sender, EventArgs e)
        {
            if (!int.TryParse(Text2.Text, out int n)) Hónapok = 24;
            Hónapok = n;
        }

        /// <summary>
        /// Havi futás meghatározása ha nem megfelelő érték volt benne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Text1_Leave(object sender, EventArgs e)
        {
            if (Text1.Text.Trim() == "") return;
            if (!int.TryParse(Text1.Text, out int n))
            {
                Text1.Text = "";
                return;
            }
            HaviKm.Text = n.ToString();
            Option8.Checked = true;
        }

        /// <summary>
        /// Előterv készítése
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Command1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Hónapok == 0) return;
                if (PszJelölő.CheckedItems.Count < 1) throw new HibásBevittAdat("Nincs kijölve egy pályaszám sem.");
                AlHoltart.Be();
                FőHoltart.Be();
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

        /// <summary>
        /// Létrehozza a táblát, ha nem létezik és az utolsó vizsgálat adatait rögzítjük
        /// </summary>
        private void Alaptábla()
        {
            try
            {
                if (Check1.Checked) return;
                string hova = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Kmadatok.mdb";
                if (Exists(hova) && !Check1.Checked) Delete(hova);
                if (!Exists(hova)) Adatbázis_Létrehozás.ElőtervkmfutástáblaICS(hova);

                KerékadatokListaFeltöltés();
                AdatokFőJármű = KézJármű.Lista_Adatok("Főmérnökség");
                AdatokICSKmadatok = KézICSKmadatok.Lista_Adatok().OrderByDescending(a => a.ID).ToList();

                // kilistázzuk a adatbázis adatait
                AlHoltart.Be(PszJelölő.CheckedItems.Count + 1);
                int i = 1;

                List<Adat_ICS_előterv> AdatokGy = new List<Adat_ICS_előterv>();
                for (int j = 0; j < PszJelölő.CheckedItems.Count; j++)
                {
                    Adat_T5C5_Kmadatok rekord = (from a in AdatokICSKmadatok
                                                 where a.Azonosító == PszJelölő.CheckedItems[j].ToStrTrim()
                                                 orderby a.Vizsgdátumk descending
                                                 select a).FirstOrDefault();

                    if (rekord != null)
                    {
                        double kerékminimum;
                        double Kerék_K1 = 0;
                        double Kerék_K2 = 0;
                        double Kerék_K3 = 0;
                        double Kerék_K4 = 0;
                        double Kerék_K5 = 0;
                        double Kerék_K6 = 0;
                        double Kerék_K7 = 0;
                        double Kerék_K8 = 0;

                        kerékminimum = 1000;
                        // kerék méretek
                        if (AdatokMérés != null)
                        {
                            Kerék_K1 = Kerékméret(rekord.Azonosító.Trim(), "K1");
                            Kerék_K2 = Kerékméret(rekord.Azonosító.Trim(), "K2");
                            Kerék_K3 = Kerékméret(rekord.Azonosító.Trim(), "K3");
                            Kerék_K4 = Kerékméret(rekord.Azonosító.Trim(), "K4");
                            Kerék_K5 = Kerékméret(rekord.Azonosító.Trim(), "K5");
                            Kerék_K6 = Kerékméret(rekord.Azonosító.Trim(), "K6");
                            Kerék_K7 = Kerékméret(rekord.Azonosító.Trim(), "K7");
                            Kerék_K8 = Kerékméret(rekord.Azonosító.Trim(), "K8");
                        }

                        if (kerékminimum > Kerék_K1) kerékminimum = Kerék_K1;
                        if (kerékminimum > Kerék_K2) kerékminimum = Kerék_K2;
                        if (kerékminimum > Kerék_K3) kerékminimum = Kerék_K3;
                        if (kerékminimum > Kerék_K4) kerékminimum = Kerék_K4;
                        if (kerékminimum > Kerék_K5) kerékminimum = Kerék_K5;
                        if (kerékminimum > Kerék_K6) kerékminimum = Kerék_K6;
                        if (kerékminimum > Kerék_K7) kerékminimum = Kerék_K7;
                        if (kerékminimum > Kerék_K8) kerékminimum = Kerék_K8;

                        Adat_ICS_előterv ADAT = new Adat_ICS_előterv(
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
                                  rekord.V2V3Számláló,
                                  false,
                                  rekord.V2végezte.Trim(),
                                  0,
                                  Kerék_K1,
                                  Kerék_K2,
                                  Kerék_K3,
                                  Kerék_K4,
                                  Kerék_K5,
                                  Kerék_K6,
                                  Kerék_K7,
                                  Kerék_K8,
                                  kerékminimum);

                        AdatokGy.Add(ADAT);
                        i += 1;
                    }
                    AlHoltart.Lép();
                }
                KézElőterv.Rögzítés(hova, AdatokGy);
                AlHoltart.Ki();
            }
            catch (HibásBevittAdat ex)
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
        /// Kerék méretét adja vissza a kerék azonosítója és pozíciója alapján
        /// </summary>
        /// <param name="kazonosító"></param>
        /// <param name="kpozíció"></param>
        /// <returns></returns>
        private int Kerékméret(string kazonosító, string kpozíció)
        {
            int méret = 0;
            Adat_Kerék_Mérés Elem = (from a in AdatokMérés
                                     where a.Pozíció == kpozíció.Trim() &&
                                     a.Azonosító == kazonosító.Trim()
                                     select a).FirstOrDefault();
            if (Elem != null) méret = Elem.Méret;
            return méret;
        }

        /// <summary>
        /// Előtervet készít a megadott feltétleknek megfelelően
        /// </summary>
        private void Egyhónaprögzítése()
        {
            try
            {
                if (Hónapok == 0) return;
                if (HavikmICS == 0) return;


                string hova = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Kmadatok.mdb";
                if (!Exists(hova)) return;

                AdatokCiklus = KézCiklus.Lista_Adatok();


                long Alsó = 0, Felső = 0, Névleges = 0;
                long Havifutás, Mennyi = 0, sorszám = 0, különbözet;
                long id_sorszám = 0;

                double figyelő;


                FőHoltart.Be(PszJelölő.Items.Count + 3);
                AlHoltart.Be(Hónapok + 3);
                // beolvassuk a ID sorszámot, majd növeljük minden rögzítésnél
                List<Adat_ICS_előterv> AdatokAlap = KézElőterv.Lista_Adatok(hova).OrderByDescending(a => a.ID).ToList();
                Adat_ICS_előterv ElemKarb = (from a in AdatokAlap
                                             orderby a.ID descending
                                             select a).FirstOrDefault();
                if (ElemKarb != null) id_sorszám = ElemKarb.ID;

                List<Adat_ICS_előterv> AdatokGy = new List<Adat_ICS_előterv>();
                for (int j = 0; j < PszJelölő.CheckedItems.Count; j++)
                {
                    Adat_ICS_előterv rekordhova = (from a in AdatokAlap
                                                   where a.Azonosító == PszJelölő.CheckedItems[j].ToStrTrim()
                                                   orderby a.Vizsgdátumv descending
                                                   select a).FirstOrDefault();

                    if (rekordhova != null)
                    {
                        // beolvassuk a kocsi alapadatait, hogy tudjuk növelni.
                        string ideigazonosító = rekordhova.Azonosító.Trim();
                        long ideigjjavszám = rekordhova.Jjavszám;
                        long ideigKMUkm = rekordhova.KMUkm;
                        DateTime ideigKMUdátum = rekordhova.KMUdátum;
                        string ideigvizsgfok = rekordhova.Vizsgfok;
                        DateTime ideigvizsgdátumk = rekordhova.Vizsgdátumk;
                        DateTime ideigvizsgdátumv = rekordhova.Vizsgdátumv;
                        long ideigvizsgkm = rekordhova.Vizsgkm;
                        long ideighavikm = rekordhova.Havikm;
                        long ideigvizsgsorszám = rekordhova.Vizsgsorszám;
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
                        double ideigkerék_1 = rekordhova.Kerék_K1;
                        double ideigkerék_2 = rekordhova.Kerék_K2;
                        double ideigkerék_3 = rekordhova.Kerék_K3;
                        double ideigkerék_4 = rekordhova.Kerék_K4;
                        double ideigkerék_5 = rekordhova.Kerék_K5;
                        double ideigkerék_6 = rekordhova.Kerék_K6;
                        double ideigkerék_7 = rekordhova.Kerék_K7;
                        double ideigkerék_8 = rekordhova.Kerék_K8;
                        double ideigkerék_min = rekordhova.Kerék_min;
                        long ideigV2V3számláló = rekordhova.V2V3Számláló;


                        for (int i = 1; i < Hónapok; i++)
                        {
                            DateTime elődátum = DateTime.Now.AddMonths(i);

                            // megnézzük, hogy mi a ciklus határa
                            Adat_Ciklus ElemCiklus = (from a in AdatokCiklus
                                                      where a.Típus == ideigCiklusrend.Trim() &&
                                                      a.Sorszám == ideigvizsgsorszám
                                                      select a).FirstOrDefault();
                            if (ElemCiklus != null)
                            {
                                Alsó = ElemCiklus.Alsóérték;
                                Felső = ElemCiklus.Felsőérték;
                                Névleges = ElemCiklus.Névleges;
                                sorszám = ElemCiklus.Sorszám;
                            }
                            if (Option10.Checked) Mennyi = Alsó;
                            if (Option11.Checked) Mennyi = Névleges;
                            if (Option12.Checked) Mennyi = Felső;

                            // megnézzük a következő V-t

                            ElemCiklus = (from a in AdatokCiklus
                                          where a.Típus == ideigCiklusrend.Trim() &&
                                          a.Sorszám == (sorszám + 1)
                                          select a).FirstOrDefault();
                            string következőv;
                            if (ElemCiklus != null)
                                következőv = ElemCiklus.Vizsgálatfok;  // ha talált akkor
                            else
                                következőv = "J";  // ha nem talált



                            // az utolsó rögzített adatot megvizsgáljuk, hogy a havi km-et át lépjük -e fokozatot
                            if (HavikmICS == 0)
                                Havifutás = ideighavikm;
                            else
                                Havifutás = HavikmICS;
                            figyelő = ideigKMUkm - ideigvizsgkm + Havifutás;

                            if (Mennyi <= figyelő)
                            {

                                különbözet = ideigKMUkm - ideigvizsgkm + Havifutás - Mennyi;
                                // módosítjuk a határig tartó adatokat
                                ideigKMUkm = ideigKMUkm + Havifutás - különbözet;
                                ideigTeljeskm = ideigTeljeskm + Havifutás - különbözet;
                                id_sorszám += 1;
                                // ideigvizsgkm = ideigKMUkm + Havifutás - különbözet
                                ideigvizsgkm += Mennyi;
                                ideigTeljeskm += Havifutás;
                                ideigKMUdátum = elődátum;
                                ideigvizsgfok = következőv;
                                ideigvizsgdátumk = elődátum;
                                ideigvizsgdátumv = elődátum;
                                ideigtervsorszám += 1;
                                ideigkerék_1 -= double.Parse(Kerékcsökkenés.Text);
                                ideigkerék_2 -= double.Parse(Kerékcsökkenés.Text);
                                ideigkerék_3 -= double.Parse(Kerékcsökkenés.Text);
                                ideigkerék_4 -= double.Parse(Kerékcsökkenés.Text);
                                ideigkerék_5 -= double.Parse(Kerékcsökkenés.Text);
                                ideigkerék_6 -= double.Parse(Kerékcsökkenés.Text);
                                ideigkerék_7 -= double.Parse(Kerékcsökkenés.Text);
                                ideigkerék_8 -= double.Parse(Kerékcsökkenés.Text);

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

                                Adat_ICS_előterv ADAT = new Adat_ICS_előterv(
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
                                           ideigV2V3számláló,
                                           false,
                                           ideigHonostelephely,
                                           ideigtervsorszám,
                                           ideigkerék_1,
                                           ideigkerék_2,
                                           ideigkerék_3,
                                           ideigkerék_4,
                                           ideigkerék_5,
                                           ideigkerék_6,
                                           ideigkerék_7,
                                           ideigkerék_8,
                                           ideigkerék_min);
                                AdatokGy.Add(ADAT);

                            }
                            else
                            {
                                // módosítjuk az utolsó adatsort
                                if (ideigKMUkm == 0) // ha felújítva volt és nem lett lenullázva
                                    ideigvizsgkm = 0;

                                ideigKMUkm += Havifutás;
                                ideigTeljeskm += Havifutás;
                            }
                            AlHoltart.Lép();
                        }
                    }
                    FőHoltart.Lép();
                }
                if (AdatokGy.Count > 0) KézElőterv.Rögzítés(hova, AdatokGy);
            }
            catch (HibásBevittAdat ex)
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
        /// Excel fájl létrehozása és a munkalapok feltöltése
        /// </summary>
        private void Excel_előtervező()
        {
            try
            {
                string[] cím = new string[5];
                string[] Leírás = new string[5];

                // paraméter tábla feltöltése
                cím[0] = "Munkalapfül";
                Leírás[0] = "Leírás";
                cím[1] = "Adatok";
                Leírás[1] = "Előtervezett adatok";
                cím[2] = "Vizsgálatok";
                Leírás[2] = "Vizsgálati adatok havonta";
                cím[3] = "Éves_terv";
                Leírás[3] = "Vizsgálati adatok éves";
                cím[4] = "Éves_havi_terv";
                Leírás[4] = "Vizsgálati adatok éves/havi";

                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
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

                MyE.ExcelLétrehozás();
                string munkalap = "Munka1";
                MyE.Munkalap_átnevezés(munkalap, "Tartalom");
                // ****************************************************
                // elkészítjük a lapokat
                // ****************************************************

                for (int i = 1; i < 5; i++)
                    MyE.Új_munkalap(cím[i]);

                // ****************************************************
                // Elkészítjük a tartalom jegyzéket
                // ****************************************************
                munkalap = "Tartalom";
                MyE.Munkalap_aktív(munkalap);
                MyE.Kiir("Munkalapfül", "a1");
                MyE.Kiir("Leírás", "b1");
                for (int i = 1; i < 5; i++)
                {
                    MyE.Link_beillesztés(munkalap, "A" + (i + 1).ToString(), cím[i].Trim());
                    MyE.Kiir(Leírás[i].Trim(), "b" + (i + 1).ToString());
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

        /// <summary>
        /// Kimutatáshoz alapadatok kilistázása Excel táblába
        /// </summary>
        private void Adatoklistázása()
        {
            try
            {
                string munkalap = "Adatok";
                MyE.Munkalap_aktív(munkalap);
                MyE.Link_beillesztés(munkalap, "A1", "Tartalom");

                // megnyitjuk az adatbázist
                string hely = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Kmadatok.mdb";

                DataTable dataTable = MyF.ToDataTable(KézElőterv.Lista_Adatok(hely).OrderBy(a => a.ID).ToList());
                utolsósor = MyE.Munkalap(dataTable, 3, munkalap);

                // fejlécet kiírjuk
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
                MyE.Kiir("Kerék_1", "y3");
                MyE.Kiir("Kerék_2", "z3");
                MyE.Kiir("Kerék_3", "aa3");
                MyE.Kiir("Kerék_4", "ab3");
                MyE.Kiir("Kerék_5", "ac3");
                MyE.Kiir("Kerék_6", "ad3");
                MyE.Kiir("Kerék_7", "ae3");
                MyE.Kiir("Kerék_8", "af3");
                MyE.Kiir("Kerék_min", "ag3");
                MyE.Kiir("V2V3 számláló", "ah3");
                MyE.Kiir("Év", "ai3");
                MyE.Kiir("fokozat", "aj3");
                MyE.Kiir("Hónap", "ak3");

                MyE.Kiir("=YEAR(RC[-27])", "Ai4");
                MyE.Kiir("=LEFT(RC[-30],2)", "Aj4");
                MyE.Kiir("=MONTH(RC[-29])", "Ak4");

                MyE.Képlet_másol(munkalap, "AI4:AK4", "AI5:AK" + (utolsósor + 3));

                // megformázzuk
                MyE.Oszlopszélesség(munkalap, "A:Ak");
                MyE.Vastagkeret("a3:Ak3");
                MyE.Rácsoz("a3:AK" + (utolsósor + 3).ToString());
                MyE.Vastagkeret("a3:Ak" + (utolsósor + 3).ToString());
                MyE.Vastagkeret("a3:Ak3");
                // szűrő
                MyE.Szűrés(munkalap, "A3:AK" + (utolsósor + 3), 3);

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

        /// <summary>
        /// Beépített kimutatás készítése 
        /// </summary>
        private void Kimutatás()
        {
            try
            {
                string munkalap = "Vizsgálatok";
                MyE.Munkalap_aktív(munkalap);
                MyE.Link_beillesztés(munkalap, "A1", "Tartalom");

                string munkalap_adat = "Adatok";
                string balfelső = "A3";
                string jobbalsó = "AK" + (utolsósor + 3);
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

        /// <summary>
        ///    Beépített kimutatás készítése 
        /// </summary>
        private void Kimutatás1()
        {
            try
            {
                string munkalap = "Éves_terv";
                MyE.Munkalap_aktív(munkalap);
                MyE.Link_beillesztés(munkalap, "A1", "Tartalom");

                string munkalap_adat = "Adatok";
                string balfelső = "A3";
                string jobbalsó = "AK" + (utolsósor + 3);
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

        /// <summary>
        /// Beépített kimutatás készítése 
        /// </summary>
        private void Kimutatás2()
        {
            try
            {

                string munkalap = "Éves_havi_terv";
                MyE.Munkalap_aktív(munkalap);
                MyE.Link_beillesztés(munkalap, "A1", "Tartalom");

                string munkalap_adat = "Adatok";
                string balfelső = "A3";
                string jobbalsó = "AK" + (utolsósor + 3);
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

        /// <summary>
        /// Adatbázis kimentése Excel táblába
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Command3_Click(object sender, EventArgs e)
        {
            try
            {
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Vizsgálatok tény adatai",
                    FileName = $"ICS_KCSV_adatbázis_mentés_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddhhmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                Holtart.Be();

                List<Adat_T5C5_Kmadatok> Adatok = KézICSKmadatok.Lista_Adatok().OrderBy(a => a.Azonosító).ToList();
                DataTable dataTable = MyF.ToDataTable(Adatok);
                MyE.ExcelLétrehozás();
                string munkalap = "Adatok";
                MyE.Munkalap_betű("Arial", 12);
                MyE.Munkalap_átnevezés("Munka1", munkalap);
                utolsósor = MyE.Munkalap(dataTable, 1, munkalap) + 1;
                MyE.Új_munkalap("Kimutatás");
                Holtart.Lép();
                MyE.Munkalap_aktív(munkalap);
                MyE.Kiir("=YEAR(RC[-15])", "v2");
                MyE.Kiir("=MONTH(RC[-16])", "w2");
                MyE.Kiir("=LEFT(RC[-18],2)", "x2");
                MyE.Képlet_másol(munkalap, "V2:X2", "V3:X" + utolsósor);
                MyE.Kiir("Év", "v1");
                MyE.Kiir("hó", "w1");
                MyE.Kiir("Vizsgálat rövid", "x1");
                MyE.Oszlopszélesség(munkalap, "A:X");
                Holtart.Lép();
                MyE.Betű("D:D", "", "M/d/yyyy");
                MyE.Betű("F:F", "", "M/d/yyyy");
                MyE.Betű("G:G", "", "M/d/yyyy");
                MyE.Betű("K:K", "", "M/d/yyyy");

                // rácsozás
                MyE.Rácsoz("A1:X" + utolsósor);
                Holtart.Lép();
                //szűrést felteszük
                MyE.Szűrés("Adatok", "A", "X", 1);

                //Nyomtatási terület kijelülése
                MyE.NyomtatásiTerület_részletes("Adatok", "A1:X" + utolsósor, "$1:$1", "", true);
                Holtart.Lép();
                munkalap = "Kimutatás";
                MyE.Munkalap_aktív(munkalap);

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
                Holtart.Lép();
                MyE.Kimutatás_Fő(munkalap_adat, balfelső, jobbalsó, kimutatás_Munkalap, Kimutatás_cella, Kimutatás_név
                                , összesítNév, Összesít_módja, sorNév, oszlopNév, SzűrőNév);

                munkalap = "Adatok";
                MyE.Aktív_Cella(munkalap, "A1");

                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();

                MyE.Megnyitás(fájlexc);
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


        #region Listák
        /// <summary>
        /// Feltölti az utolsó két év kerékadatokat a listába
        /// </summary>
        private void KerékadatokListaFeltöltés()
        {
            try
            {
                AdatokMérés.Clear();
                AdatokMérés = KézMérés.Lista_Adatok(DateTime.Today.Year - 1);
                List<Adat_Kerék_Mérés> Ideig = KézMérés.Lista_Adatok(DateTime.Today.Year);
                AdatokMérés.AddRange(Ideig);
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

        /// <summary>
        /// A napi Zserkm adatok feltöltése listába
        /// </summary>
        private void KorrekcióListaFeltöltés()
        {
            try
            {
                AdatokZserKm.Clear();
                List<Adat_Főkönyv_Zser_Km> Előző = KézKorr.Lista_adatok(DateTime.Today.Year);
                AdatokZserKm.AddRange(Előző);

                Előző = KézKorr.Lista_adatok(DateTime.Today.Year - 1);
                AdatokZserKm.AddRange(Előző);
            }
            catch (HibásBevittAdat ex)
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