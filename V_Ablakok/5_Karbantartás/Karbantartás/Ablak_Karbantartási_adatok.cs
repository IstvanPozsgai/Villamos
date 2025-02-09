using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;
using MyEn = Villamos.V_MindenEgyéb.Enumok;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    public partial class Ablak_Karbantartási_adatok
    {
        readonly Kezelő_jármű_hiba KézHiba = new Kezelő_jármű_hiba();
        readonly Kezelő_Jármű KéZJármű = new Kezelő_Jármű();
        readonly Kezelő_Szerelvény KézSzerelvény = new Kezelő_Szerelvény();
        readonly Kezelő_Jármű_Állomány_Típus KézTípus = new Kezelő_Jármű_Állomány_Típus();
        readonly Kezelő_Ciklus KézCiklus = new Kezelő_Ciklus();
        readonly Kezelő_Jármű_Xnapos KézXnapos = new Kezelő_Jármű_Xnapos();

        List<Adat_Szerelvény> AdatokSzer = new List<Adat_Szerelvény>();
        List<Adat_Jármű> AdatokJármű = new List<Adat_Jármű>();
        List<Adat_Jármű> AdatokFőJármű = new List<Adat_Jármű>();
        List<Adat_Jármű_hiba> AdatokHiba = new List<Adat_Jármű_hiba>();
        List<Adat_Jármű_Állomány_Típus> AdatokTípus = new List<Adat_Jármű_Állomány_Típus>();
        List<Adat_Ciklus> AdatokCiklus = new List<Adat_Ciklus>();
#pragma warning disable IDE0044
        List<Adat_Jármű_hiba> AdatokNapló = new List<Adat_Jármű_hiba>();
        List<Adat_Karbantartási> AdatokKarbantartási = new List<Adat_Karbantartási>();
        List<Adat_Jármű_Xnapos> AdatokXnapos = new List<Adat_Jármű_Xnapos>();

        DataTable AdatTábla = new DataTable();

        string Egyed_Típus = "";
        long Egyed_Státus = 0;
        long Hiba_státus = 0;
        int darab = 0;
        int GombokSzáma = 0;
        int Utolsóhiba = 0;

        string CiklusrendCombo = "";
        string Vizsgfoka_Jármű = "";
        int Vsorszám_Jármű = 0;
        long KövV2_Sorszám = 0;
        DateTime Vütemezés_Jármű_Dátum;
        long VizsgKm_Jármű = 0;
        string KövV2 = "";
        long KövV_Sorszám = 0;
        long KövV2_számláló = 0;
        string KövV = "";

        string szűrő = "";
        string sorba = "";
        public Ablak_Karbantartási_adatok()
        {
            InitializeComponent();
            Start();
        }

        void Start()
        {
            Telephelyekfeltöltése();
            Jogosultságkiosztás();
            ABFejléc();
            ListákFeltöltése();

            Dátumig.Value = DateTime.Today;
            Dátumtól.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            Fülek.SelectedIndex = 0;
            Fülekkitöltése();
            Pályaszámok_feltöltése();
            Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;


        }

        private void ListákFeltöltése()
        {
            HibaListázás();
            JárművekLista();
            Szer_Feltöltés();
            AdatokTípus = KézTípus.Lista_Adatok(Cmbtelephely.Text.Trim());
            Ciklus_Feltölése();
            JárművekFŐLista();
            Napi_Feltölése();
        }

        private void Ablak_Jármű_állapotok_Load(object sender, EventArgs e)
        {

        }


        #region Alap
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

        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Karbantartás.html";
            MyE.Megnyitás(hely);
        }


        private void Jogosultságkiosztás()
        {
            int melyikelem;

            // ide kell az összes gombot tenni amit szabályozni akarunk false

            Rögzít_Módosít.Enabled = false;
            Rögzít_Módosít.Visible = false;
            melyikelem = 99;

            // módosítás 1 
            if (MyF.Vanjoga(melyikelem, 1))
            {
                Rögzít_Módosít.Enabled = true;
                Rögzít_Módosít.Visible = true;
            }
            // módosítás 2 
            if (MyF.Vanjoga(melyikelem, 2))
            { }

            // módosítás 3
            if (MyF.Vanjoga(melyikelem, 3))
            { }

        }

        private void Fülekkitöltése()
        {
            switch (Fülek.SelectedIndex)
            {
                case 0:
                    {
                        Táblalista_kiírás();
                        break;
                    }
                case 1:
                    {
                        Bevitelimezők_alap();
                        break;
                    }
                case 4:
                    {
                        // gombok
                        Gombok_feltöltés();
                        break;
                    }
            }
        }

        private void Fülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }

        private void Ablak_Jármű_állapotok_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC
            if ((int)e.KeyCode == 27)
            {
                Bevitelimezők_alap();
                Hibaterv_combo.Visible = false;
            }
        }

        private void Cmbtelephely_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Táblalista_kiírás();
            Pályaszámok_feltöltése();
            ListákFeltöltése();
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



        #region Lista lap
        private void Frissíti_táblalistát_Click(object sender, EventArgs e)
        {
            szűrő = "";
            sorba = "";

            if (szűrő != "" || sorba != "")
                Táblalista.LoadFilterAndSort(szűrő, sorba);

            Táblalista.TriggerSortStringChanged();
            Táblalista.TriggerFilterStringChanged();
            Táblalista_kiírás();

            for (int i = 0; i < Táblalista.Columns.Count; i++)
            {
                Táblalista.SetFilterEnabled(Táblalista.Columns[i], true);
                Táblalista.SetSortEnabled(Táblalista.Columns[i], true);
                Táblalista.SetFilterCustomEnabled(Táblalista.Columns[i], true);
            }
        }

        private void Táblalista_kiírás()
        {
            ListákFeltöltése();
            AdatokKarbantartási.Clear();
            AdatokRendezése();
            ABFeltöltése();

            Táblalista.DataSource = AdatTábla;
            OszlopSzélesség();
            Táblalista.Refresh();
            Táblalista.Visible = true;
            Táblalista.ClearSelection();
        }

        private void ABFejléc()
        {
            try
            {
                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Típus");
                AdatTábla.Columns.Add("Psz");
                AdatTábla.Columns.Add("Bennmaradó hibák");
                AdatTábla.Columns.Add("Beállóba hibák");
                AdatTábla.Columns.Add("Szabad hibák");
                AdatTábla.Columns.Add("Szerelvény");
                AdatTábla.Columns.Add("Mióta áll");

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ABFeltöltése()
        {
            try
            {
                AdatTábla.Clear();
                foreach (Adat_Karbantartási rekord in AdatokKarbantartási)
                {
                    DataRow Soradat = AdatTábla.NewRow();
                    Soradat["Típus"] = rekord.Típus;
                    Soradat["Psz"] = rekord.Azonosító;
                    Soradat["Bennmaradó hibák"] = rekord.Álló;
                    Soradat["Beállóba hibák"] = rekord.Beálló;
                    Soradat["Szabad hibák"] = rekord.Szabad;
                    Soradat["Szerelvény"] = rekord.Szerelvény;
                    Soradat["Mióta áll"] = rekord.Miótaáll == new DateTime(2000, 1, 1) ? "" : rekord.Miótaáll.ToShortDateString();
                    AdatTábla.Rows.Add(Soradat);
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

        private void AdatokRendezése()
        {
            try
            {

                string Pályaszám = "";
                string Típus = "";
                string álló = "";
                string beálló = "";
                string szabad = "";
                foreach (Adat_Jármű_hiba rekord in AdatokHiba)
                {
                    if (Pályaszám.Trim() == "") Pályaszám = rekord.Azonosító;
                    if (Pályaszám.Trim() != "" && rekord.Azonosító != Pályaszám.Trim())
                    {
                        AdatokListábaRakása(Pályaszám, Típus, álló, beálló, szabad);
                        álló = "";
                        beálló = "";
                        szabad = "";
                        Pályaszám = rekord.Azonosító;
                    }

                    switch (rekord.Korlát)
                    {
                        case 4:
                            {
                                álló += rekord.Hibaleírása.Trim() + "-";
                                break;
                            }
                        case 3:
                            {
                                beálló += rekord.Hibaleírása.Trim() + "-";
                                break;
                            }
                        case 1:
                            {
                                szabad += rekord.Hibaleírása.Trim() + "-";
                                break;
                            }
                    }
                    Adat_Jármű Elem = (from a in AdatokFőJármű
                                       where a.Azonosító == Pályaszám
                                       select a).FirstOrDefault();
                    if (Elem != null) Típus = Elem.Valóstípus; else Típus = "nincs";
                }
                if (szabad.Trim() != "" || álló.Trim() != "" || beálló.Trim() != "") AdatokListábaRakása(Pályaszám, Típus, álló, beálló, szabad);

                // Sorba rendezés
                AdatokKarbantartási = (from a in AdatokKarbantartási
                                       orderby a.Típus, a.Azonosító
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

        private void AdatokListábaRakása(string Azonosító, string Típus, string álló, string beálló, string szabad)
        {
            DateTime miótaáll = new DateTime(2000, 1, 1);
            Adat_Jármű Elem = (from a in AdatokJármű
                               where a.Azonosító == Azonosító
                               select a).FirstOrDefault();
            if (Elem != null)
                if (Elem.Miótaáll > new DateTime(2000, 1, 1))
                    miótaáll = Elem.Miótaáll;

            string szerelvény = SzerelvényÖÁ(Azonosító);

            Adat_Karbantartási Adat = new Adat_Karbantartási(
                                Azonosító,
                                álló,
                                beálló,
                                szabad,
                                Típus,
                                miótaáll,
                                szerelvény);
            AdatokKarbantartási.Add(Adat);
        }

        private string SzerelvényÖÁ(string Azonosító)
        {
            string válasz = "";
            Adat_Szerelvény rekordszer = (from a in AdatokSzer
                                          where a.Kocsi1 == Azonosító || a.Kocsi2 == Azonosító || a.Kocsi3 == Azonosító ||
                                                a.Kocsi4 == Azonosító || a.Kocsi5 == Azonosító || a.Kocsi6 == Azonosító
                                          select a).FirstOrDefault();
            if (rekordszer != null)
            {
                válasz = rekordszer.Kocsi1;
                if (rekordszer.Kocsi2 != "0") válasz += "-" + rekordszer.Kocsi2;
                if (rekordszer.Kocsi3 != "0") válasz += "-" + rekordszer.Kocsi3;
                if (rekordszer.Kocsi4 != "0") válasz += "-" + rekordszer.Kocsi4;
                if (rekordszer.Kocsi5 != "0") válasz += "-" + rekordszer.Kocsi5;
                if (rekordszer.Kocsi6 != "0") válasz += "-" + rekordszer.Kocsi6;
            }
            return válasz;
        }

        private void OszlopSzélesség()
        {
            Táblalista.Columns["Típus"].Width = 80;
            Táblalista.Columns["Psz"].Width = 80;
            Táblalista.Columns["Bennmaradó hibák"].Width = 430;
            Táblalista.Columns["Beállóba hibák"].Width = 430;
            Táblalista.Columns["Szabad hibák"].Width = 430;
            Táblalista.Columns["Szerelvény"].Width = 150;
            Táblalista.Columns["Mióta áll"].Width = 110;
        }

        private void Excel_gomb_Click(object sender, EventArgs e)
        {
            try
            {
                if (Táblalista.Rows.Count <= 0)
                    return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Típus_lista_" + Program.PostásNév + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Táblalista, false);
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

        private void Táblalista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Pályaszám.Text = Táblalista.Rows[e.RowIndex].Cells[1].Value.ToString();
            Fülek.SelectedIndex = 1;
            Hibák_kiírása();
        }

        private void Táblalista_SortStringChanged(object sender, Zuby.ADGV.AdvancedDataGridView.SortEventArgs e)
        {
            sorba = Táblalista.SortString;
        }

        private void Táblalista_FilterStringChanged(object sender, Zuby.ADGV.AdvancedDataGridView.FilterEventArgs e)
        {
            szűrő = Táblalista.FilterString;
        }

        #endregion



        #region Hibalap
        private void Pályaszámok_feltöltése()
        {
            try
            {
                Pályaszám.Items.Clear();
                Napló_pályaszám.Items.Clear();

                if (Cmbtelephely.Text.Trim() == "") return;
                if (AdatokJármű == null) return;
                List<Adat_Jármű> Adatok = (from a in AdatokJármű
                                           where a.Üzem == Cmbtelephely.Text.Trim() && a.Törölt == false
                                           orderby a.Azonosító
                                           select a).ToList();
                if (Adatok == null) return;

                Pályaszám.BeginUpdate();
                Napló_pályaszám.BeginUpdate();
                foreach (Adat_Jármű rekord in Adatok)
                {
                    Pályaszám.Items.Add(rekord.Azonosító);
                    Napló_pályaszám.Items.Add(rekord.Azonosító);
                }
                Pályaszám.EndUpdate();
                Pályaszám.Refresh();

                Napló_pályaszám.EndUpdate();
                Napló_pályaszám.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Hibák_kiírása()
        {
            try
            {
                Bevitelimezők_alap();
                HibaListázás();
                JárművekLista();

                long státushiba = 0;
                bool figyel = false;
                darab = 0;

                #region Tábla eleje
                Tábla_Hibalista.Rows.Clear();
                Tábla_Hibalista.Columns.Clear();
                Tábla_Hibalista.Refresh();
                Tábla_Hibalista.Visible = false;
                Tábla_Hibalista.ColumnCount = 6;
                #endregion

                #region Fejléc
                // fejléc elkészítése
                Tábla_Hibalista.Columns[0].HeaderText = "Sorszám";
                Tábla_Hibalista.Columns[0].Width = 80;  // 15-el kell osztani
                Tábla_Hibalista.Columns[1].HeaderText = "Rögzítette";
                Tábla_Hibalista.Columns[1].Width = 100;
                Tábla_Hibalista.Columns[2].HeaderText = "hibaleírása";
                Tábla_Hibalista.Columns[2].Width = 600;
                Tábla_Hibalista.Columns[3].HeaderText = "Hiba Státus";
                Tábla_Hibalista.Columns[3].Width = 120;
                Tábla_Hibalista.Columns[4].HeaderText = "Rögzítés ideje";
                Tábla_Hibalista.Columns[4].Width = 160;
                Tábla_Hibalista.Columns[5].HeaderText = "Típus";
                Tábla_Hibalista.Columns[5].Width = 80;
                #endregion

                List<Adat_Jármű_hiba> Adatok = (from a in AdatokHiba
                                                where a.Azonosító == Pályaszám.Text.Trim()
                                                orderby a.Hibáksorszáma
                                                select a).ToList();

                foreach (Adat_Jármű_hiba rekord in Adatok)
                {
                    Tábla_Hibalista.RowCount++;
                    int i = Tábla_Hibalista.RowCount - 1;
                    if (rekord.Hibáksorszáma != i + 1) figyel = true;
                    Tábla_Hibalista.Rows[i].Cells[0].Value = rekord.Hibáksorszáma;
                    Tábla_Hibalista.Rows[i].Cells[1].Value = rekord.Létrehozta.Trim();
                    Tábla_Hibalista.Rows[i].Cells[2].Value = rekord.Hibaleírása.Trim();
                    if (rekord.Korlát > státushiba) státushiba = rekord.Korlát;
                    Tábla_Hibalista.Rows[i].Cells[3].Value = Enum.GetName(typeof(MyEn.Jármű_Státus), rekord.Korlát);
                    if (rekord.Korlát == 4) darab++;
                    Tábla_Hibalista.Rows[i].Cells[4].Value = rekord.Idő;
                    Tábla_Hibalista.Rows[i].Cells[5].Value = rekord.Típus.Trim();
                }
                Tábla_Hibalista.Refresh();
                Tábla_Hibalista.ClearSelection();
                Tábla_Hibalista.Visible = true;

                Utolsóhiba = Adatok.Count;
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";
                string szöveg;
                string jelszó = "pozsgaii";
                // a kiírt darabszámhoz igazítjuk mindig a villamos adatait.
                if (Utolsóhiba != JárműSorszám())
                {
                    szöveg = $"UPDATE állománytábla SET hibák={Utolsóhiba} WHERE  azonosító='{Pályaszám.Text.Trim()}'";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                    JárművekLista();
                }

                Hiba_státus = státushiba;
                Egyed_Státus = JárműStátusz();

                if (státushiba != Egyed_Státus)
                {
                    szöveg = $"UPDATE állománytábla SET státus={státushiba} WHERE  azonosító='{Pályaszám.Text.Trim()}'";
                    if (státushiba == 4)
                    {
                        // ha megállt módosítjuk a miótaállt is
                        szöveg = $"UPDATE állománytábla SET státus={státushiba}, miótaáll='{DateTime.Now}' WHERE  azonosító='{Pályaszám.Text.Trim()}'";
                    }
                    if (Egyed_Státus == 4)
                    {
                        // ha elindult
                        szöveg = $"UPDATE állománytábla SET státus={státushiba}, miótaáll='1900.01.01' WHERE  azonosító='{Pályaszám.Text.Trim()}'";
                    }
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                    JárművekLista();
                }

                if (figyel) Újrasorszámoz();
                Sorszám.Text = (Utolsóhiba + 1).ToString();
                Tábla_Hibalista_Színezés();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private long JárműStátusz()
        {
            Adat_Jármű Elem = (from a in AdatokJármű
                               where a.Azonosító == Pályaszám.Text.Trim()
                               select a).FirstOrDefault();
            if (Elem != null) return Elem.Státus;
            return 0;
        }

        private string JárműTípus()
        {
            Adat_Jármű Elem = (from a in AdatokJármű
                               where a.Azonosító == Pályaszám.Text.Trim()
                               select a).FirstOrDefault();
            if (Elem != null) return Elem.Valóstípus;
            return "Nincs";
        }

        private long JárműSorszám()
        {
            Adat_Jármű Elem = (from a in AdatokJármű
                               where a.Azonosító == Pályaszám.Text.Trim()
                               select a).FirstOrDefault();
            if (Elem != null) return Elem.Hibáksorszáma;
            return 1;
        }

        private void Újrasorszámoz()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\Hiba.mdb";
                string jelszó = "pozsgaii";
                string szöveg;

                List<Adat_Jármű_hiba> Adatok = (from a in AdatokHiba
                                                where a.Azonosító == Pályaszám.Text.Trim()
                                                orderby a.Hibáksorszáma
                                                select a).ToList();

                List<string> szövegGy = new List<string>();
                for (int i = 0; i < Adatok.Count; i++)
                {
                    szöveg = $"UPDATE Hibatábla SET hibáksorszáma={i + 1} WHERE azonosító='{Pályaszám.Text.Trim()}'";
                    szöveg += $" And  hibaleírása='{Adatok[i].Hibaleírása}' AND idő=#{Adatok[i].Idő:MM-dd-yyyy HH:mm:ss}#";
                    szövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, szövegGy);
                HibaListázás();
                Hibák_kiírása();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pályaszám_TextUpdate(object sender, EventArgs e)
        {
            if (Pályaszám.Text.Trim() == "") return;
            Hibák_kiírása();
        }

        private void Lekérdez_Click(object sender, EventArgs e)
        {
            if (Pályaszám.Text.Trim() == "") return;
            Hibák_kiírása();
        }

        private void Pályaszám_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Pályaszám.Text.Trim() == "") return;
            Pályaszám.Text = Pályaszám.Items[Pályaszám.SelectedIndex].ToString();
            Hibák_kiírása();
        }

        private void Tábla_Hibalista_Színezés()
        {
            try
            {
                for (int i = 0; i < Tábla_Hibalista.Rows.Count; i++)
                {
                    if (Tábla_Hibalista.Rows[i].Cells[3].Value.ToStrTrim() == "Üzemképtelen")
                    {
                        Tábla_Hibalista.Rows[i].Cells[3].Style.BackColor = Color.Red;
                        Tábla_Hibalista.Rows[i].Cells[3].Style.ForeColor = Color.White;
                        Tábla_Hibalista.Rows[i].Cells[3].Style.Font = new Font("Arial Narrow", 11f, FontStyle.Italic);

                        Tábla_Hibalista.Rows[i].Cells[0].Style.BackColor = Color.Red;
                        Tábla_Hibalista.Rows[i].Cells[0].Style.ForeColor = Color.White;
                        Tábla_Hibalista.Rows[i].Cells[0].Style.Font = new Font("Arial Narrow", 11f, FontStyle.Italic);
                    }
                    if (Tábla_Hibalista.Rows[i].Cells[3].Value.ToStrTrim() == "Beálló")
                    {
                        Tábla_Hibalista.Rows[i].Cells[3].Style.BackColor = Color.Yellow;
                        Tábla_Hibalista.Rows[i].Cells[3].Style.ForeColor = Color.Black;
                        Tábla_Hibalista.Rows[i].Cells[3].Style.Font = new Font("Arial Narrow", 11f, FontStyle.Italic);

                        Tábla_Hibalista.Rows[i].Cells[0].Style.BackColor = Color.Yellow;
                        Tábla_Hibalista.Rows[i].Cells[0].Style.ForeColor = Color.Black;
                        Tábla_Hibalista.Rows[i].Cells[0].Style.Font = new Font("Arial Narrow", 11f, FontStyle.Italic);
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

        private void Tábla_Hibalista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Sorszám.Text = Tábla_Hibalista.Rows[e.RowIndex].Cells[0].Value.ToString();
            Hibaszöveg.Text = Tábla_Hibalista.Rows[e.RowIndex].Cells[2].Value.ToString();
            Hibaterv_combo.Visible = false;
            Javítva.Visible = true;
            switch (Tábla_Hibalista.Rows[e.RowIndex].Cells[3].Value.ToString().Trim())
            {
                case "Bennmaradó":
                    {
                        Jel4.Checked = true;
                        Hiba_státus = 4;
                        break;
                    }
                case "Beálló":
                    {
                        Jel3.Checked = true;
                        Hiba_státus = 3;
                        break;
                    }
                case "Szabad":
                    {
                        Jel1.Checked = true;
                        Hiba_státus = 1;
                        break;
                    }
            }
        }

        private void Bevitelimezők_alap()
        {
            Sorszám.Text = "1";
            Jel4.Checked = true;
            Hibaszöveg.Text = "";
            Hibaterv_combo.Text = "";
            Hibaterv_combo.Visible = false;
            Javítva.Checked = false;
            Javítva.Visible = false;
        }

        private void Hibaterv_command4_Click(object sender, EventArgs e)
        {
            Bevitelimezők_alap();
            Hibaterv_feltöltés();
            Hibaterv_combo.Visible = true;
        }

        private void Hibaterv_feltöltés()
        {
            Hibaterv_combo.Items.Clear();
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Segéd\kiegészítő.mdb";
            string jelszó = "Mocó";

            string szöveg = "SELECT * FROM Hibaterv order by id";

            Hibaterv_combo.BeginUpdate();
            Hibaterv_combo.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "szöveg"));
            Hibaterv_combo.EndUpdate();
            Hibaterv_combo.Refresh();
        }

        private void Hibaterv_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hibaszöveg.Text = Hibaterv_combo.Text;
            Hibaterv_combo.Visible = false;
        }

        private void Egysorfel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Pályaszám.FindStringExact(Pályaszám.Text.Trim()) < 0) throw new HibásBevittAdat("A telephelyen nincs ilyen pályaszámú jármű!");
                if (!int.TryParse(Sorszám.Text, out int sorszám)) throw new HibásBevittAdat("Nincs kijelölve sor.");
                if (sorszám <= 1) throw new HibásBevittAdat("Az első elemet nem lehet előrébb tenni.");
                if (Utolsóhiba < sorszám) return;

                Adat_Jármű_hiba Előző = (from a in AdatokHiba
                                         where a.Hibáksorszáma == sorszám - 1 && a.Azonosító == Pályaszám.Text.Trim()
                                         select a).FirstOrDefault();
                Adat_Jármű_hiba Következő = (from a in AdatokHiba
                                             where a.Hibáksorszáma == sorszám && a.Azonosító == Pályaszám.Text.Trim()
                                             select a).FirstOrDefault();

                if (Előző == null || Következő == null) return;         //Ha valamelyik nincs akkor kilép

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\hiba.mdb";
                string jelszó = "pozsgaii";

                string szöveg = "UPDATE hibatábla  SET ";
                szöveg += $"hibáksorszáma={Következő.Hibáksorszáma} ";
                szöveg += $" WHERE létrehozta='{Előző.Létrehozta}' AND hibaleírása='{Előző.Hibaleírása}' AND azonosító='{Előző.Azonosító}'";
                MyA.ABMódosítás(hely, jelszó, szöveg);

                szöveg = "UPDATE hibatábla  SET ";
                szöveg += $"hibáksorszáma={Előző.Hibáksorszáma} ";
                szöveg += $" WHERE létrehozta='{Következő.Létrehozta}' AND hibaleírása='{Következő.Hibaleírása}' AND azonosító='{Következő.Azonosító}'";
                MyA.ABMódosítás(hely, jelszó, szöveg);

                HibaListázás();
                Hibák_kiírása();
                Bevitelimezők_alap();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Rögzít_Módosít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Hibaszöveg.Text.Trim() == "") throw new HibásBevittAdat("Hiba szövege nem lehet üres karakter lánc.");
                if (Pályaszám.FindStringExact(Pályaszám.Text.Trim()) < 0) throw new HibásBevittAdat("A telephelyen nincs ilyen pályaszámú jármű!");


                // méretre vágjuk a szöveget
                Hibaszöveg.Text = MyF.Szöveg_Tisztítás(Hibaszöveg.Text, 0, 84, true);

                Egyed_Típus = JárműTípus();
                Egyed_Státus = JárműStátusz();

                // ha látszódik a Javítva akkor módosítás
                if (Javítva.Visible)
                {
                    if (!Javítva.Checked)
                        Hiba_státus = HibaMódosítás();   // módosítás
                    else
                        Hiba_státus = HibaTörlés();  // törlés
                }
                else
                {
                    // leellenőrizzük, hogy van már ilyen beírású hiba már
                    Adat_Jármű_hiba Elem = (from a in AdatokHiba
                                            where a.Hibaleírása == Hibaszöveg.Text.Trim()
                                            && a.Azonosító == Pályaszám.Text.Trim()
                                            select a).FirstOrDefault();
                    if (Elem != null) throw new HibásBevittAdat("Ezzel a szövegezéssel már van egy másik hiba!");
                    // új hiba rögzítése
                    Hiba_státus = HibaRögzítés();
                }

                Hiba_Rögzítés_Napló();
                HibaListázás();


                // Napi tábla azok kerülnek bele amelyek megállnak
                // Akkor áll meg ha nem állt a jármű és a hiba státusa megállít 
                if (Egyed_Státus < 4 && Hiba_státus == 4) Napi_Rögzítés();

                // ha állt a jármű és a hiba státusa megálít, akkor a szöveget kiegészíti
                if (Egyed_Státus == 4 && Hiba_státus == 4) Napi_Módosítás();

                // ha a villamos áll, hiba státusa nem 4 és nincs másik álló hiba
                if (Egyed_Státus == 4 && Hiba_státus < 4 && darab < 2) Napi_Törlés();


                if (Egyed_Típus == "TW6000" && Javítva.Checked) KészTW6000();


                if (Egyed_Típus.Contains("T5C5") && Javítva.Checked)
                {
                    if (Hibaszöveg.Text.Contains("-")) Terv_lezárás_T5C5();
                }

                if ((Egyed_Típus.Contains("ICS") || Egyed_Típus.Contains("KCSV")) && Javítva.Checked)
                {
                    if (Hibaszöveg.Text.Contains("-")) Terv_lezárás_ICS();
                }

                if (Egyed_Típus.Contains("CAF") && Javítva.Checked) KészCAF();


                Hibák_kiírása();
                Bevitelimezők_alap();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void KészCAF()
        {
            try
            {
                // szétbontjuk a szöveget
                if (Hibaszöveg.Text.Trim().Contains("-"))
                {
                    string[] tömb = Hibaszöveg.Text.Trim().Split('-');
                    if (tömb.Length > 2)
                    {
                        Vizsgfoka_Jármű = tömb[0];
                        Vsorszám_Jármű = tömb[1].ToÉrt_Int();
                        Vütemezés_Jármű_Dátum = tömb[2].ToÉrt_DaTeTime();
                        if (Vizsgfoka_Jármű != "Mosó") CAFelkészülés();
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

        private void KészTW6000()
        {
            try
            {
                // szétbontjuk a szöveget
                if (Hibaszöveg.Text.Trim().Contains("-"))
                {
                    string[] tömb = Hibaszöveg.Text.Trim().Split('-');

                    if (tömb.Length > 2)
                    {
                        Vizsgfoka_Jármű = tömb[0].ToStrTrim();
                        Vsorszám_Jármű = tömb[1].ToÉrt_Int();
                        Vütemezés_Jármű_Dátum = tömb[2].ToÉrt_DaTeTime();
                        if (!Vizsgfoka_Jármű.Contains("Mosó")) TW6000elkészülés();
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

        private void Napi_Törlés()
        {
            try
            {        // kitöröljük a napos listából
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\hibanapló\napi.mdb";
                string jelszó = "plédke";

                // leellenőrizzük, hogy biztos nincs
                Adat_Jármű_Xnapos Elem = (from a in AdatokXnapos
                                          where a.Azonosító == Pályaszám.Text.Trim()
                                          select a).FirstOrDefault();

                if (Elem != null)
                {
                    DateTime kezdődátum = Elem.Kezdődátum;
                    string hibaleírása = Elem.Hibaleírása;

                    //Kitöröljük a táblából
                    string szöveg = $"DELETE FROM xnapostábla WHERE [azonosító]='{Pályaszám.Text.Trim()}' ";
                    MyA.ABtörlés(hely, jelszó, szöveg);
                    Napi_Feltölése();

                    // beírjuk a gyűjtőbe ha kell
                    hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\hibanapló\Elkészült{DateTime.Today.Year}.mdb";
                    if (!File.Exists(hely)) Adatbázis_Létrehozás.Javításiátfutástábla(hely);

                    szöveg = "INSERT INTO xnapostábla (kezdődátum, végdátum,  azonosító,  hibaleírása) VALUES (";
                    szöveg += $"'{kezdődátum}', ";
                    szöveg += $"'{DateTime.Now}', ";
                    szöveg += $"'{Pályaszám.Text.Trim()}', ";
                    szöveg += $"'{hibaleírása}')";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
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

        private void Napi_Módosítás()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\hibanapló\napi.mdb";
                string jelszó = "plédke";
                // leellenőrizzük, hogy biztos nincs
                Adat_Jármű_Xnapos Elem = (from a in AdatokXnapos
                                          where a.Azonosító == Pályaszám.Text.Trim()
                                          select a).FirstOrDefault();
                // ha már volt akkor beolvassuk a szöveget és ha nincs benne az új hiba szövege akkor kiegészíti azzal
                if (Elem != null)
                {
                    if (Elem.Hibaleírása.Contains(Hibaszöveg.Text))
                    {
                        string szöveg = $"UPDATE xnapostábla SET hibaleírása='{Elem.Hibaleírása}-{Hibaszöveg.Text.Trim()}' ";
                        szöveg += $" WHERE [azonosító]='{Pályaszám.Text.Trim()}' ";
                        MyA.ABMódosítás(hely, jelszó, szöveg);
                        Napi_Feltölése();
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

        private void Napi_Rögzítés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\hibanapló\napi.mdb";
                string jelszó = "plédke";
                // leellenőrizzük, hogy biztos nincs
                Adat_Jármű_Xnapos Elem = (from a in AdatokXnapos
                                          where a.Azonosító == Pályaszám.Text.Trim()
                                          select a).FirstOrDefault();

                if (Elem == null)
                {
                    string szöveg = "INSERT INTO xnapostábla (kezdődátum, végdátum,  azonosító,  hibaleírása) VALUES (";
                    szöveg += $"'{DateTime.Now}', '1900.01.01', ";
                    szöveg += $"'{Pályaszám.Text.Trim()}', ";
                    szöveg += $"'{Hibaszöveg.Text.Trim()}')";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                    Napi_Feltölése();
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

        private long HibaRögzítés()
        {
            try
            {
                if (Jel1.Checked)
                    Hiba_státus = 1;
                else if (Jel3.Checked)
                    Hiba_státus = 3;
                else
                    Hiba_státus = 4;

                Adat_Jármű_hiba Elem = new Adat_Jármű_hiba(
                                 Program.PostásNév.Trim(),
                                 Hiba_státus,
                                 Hibaszöveg.Text.Trim(),
                                 DateTime.Now,
                                 false,
                                 Egyed_Típus,
                                 Pályaszám.Text.Trim(),
                                 1);

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\hiba.mdb";
                if (!File.Exists(hely)) Hiba_státus = 0;
                string jelszó = "pozsgaii";
                KézHiba.Rögzítés(hely, jelszó, Elem);

                HibaListázás();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Hiba_státus;
        }


        private long HibaMódosítás()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\hiba.mdb";
                string jelszó = "pozsgaii";

                if (Jel1.Checked)
                {
                    Hiba_státus = 1;
                }
                else if (Jel3.Checked)
                {
                    Hiba_státus = 3;
                }
                else
                {
                    Hiba_státus = 4;
                }

                Adat_Jármű_hiba Elem = new Adat_Jármű_hiba(
                     Program.PostásNév.Trim(),
                     Hiba_státus,
                     Hibaszöveg.Text.Trim(),
                     DateTime.Now,
                     false,
                     Egyed_Típus,
                     Pályaszám.Text.Trim(),
                     long.Parse(Sorszám.Text));


                KézHiba.Módosítás(hely, jelszó, Elem);
                HibaListázás();

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Hiba_státus;
        }

        private long HibaTörlés()
        {
            try
            {
                Adat_Jármű_hiba Elem = new Adat_Jármű_hiba(
                                                          Program.PostásNév.Trim(),
                                                          Hiba_státus,
                                                          Hibaszöveg.Text.Trim(),
                                                          DateTime.Now,
                                                          false,
                                                          Egyed_Típus,
                                                          Pályaszám.Text.Trim(),
                                                          long.Parse(Sorszám.Text));
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\hiba.mdb";
                string jelszó = "pozsgaii";
                KézHiba.Törlés(hely, jelszó, Elem);
                HibaListázás();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Hiba_státus = 0;
        }

        private void Terv_lezárás_ICS()
        {
            try
            {
                string[] tömb = Hibaszöveg.Text.Trim().Split('-');
                if (tömb.Length >= 2)
                {
                    Vizsgfoka_Jármű = tömb[0];
                    Vsorszám_Jármű = tömb[1].ToÉrt_Int();
                    if (Vsorszám_Jármű > 0)
                    {
                        Vütemezés_Jármű_Dátum = MyF.Szöveg_Tisztítás(tömb[2], 0, 11).ToÉrt_DaTeTime();

                        string hely = Application.StartupPath + @"\Főmérnökség\Adatok\ICSKCSV\Villamos4ICS.mdb";
                        string jelszó = "pocsaierzsi";
                        string szöveg = $"Select * FROM KMtábla where törölt=false AND [azonosító]='{Pályaszám.Text.Trim()}' AND ";
                        szöveg += $"kövv='{Vizsgfoka_Jármű}' AND ";
                        szöveg += $"kövv_sorszám={Vsorszám_Jármű}";
                        szöveg += $" ORDER BY ID desc";
                        Kezelő_T5C5_Kmadatok KéZKm = new Kezelő_T5C5_Kmadatok();
                        Adat_T5C5_Kmadatok EgyAdat = KéZKm.Egy_Adat(hely, jelszó, szöveg);

                        if (EgyAdat != null)
                        {
                            CiklusrendCombo = EgyAdat.Ciklusrend;
                            VizsgKm_Jármű = EgyAdat.KMUkm;

                            // ha V2/V3 volt akkor változik, ha nem akkor marad 
                            if (Vizsgfoka_Jármű.Contains("V2") || Vizsgfoka_Jármű.Contains("V3"))
                                KövV2_számláló = VizsgKm_Jármű;        // V2/V3 volt
                            else
                                KövV2_számláló = EgyAdat.V2V3Számláló;  // minden egyéb

                            // feltöltjük a vizsgálatokat
                            List<Adat_Ciklus> SzűrtCiklus = (from a in AdatokCiklus
                                                             where a.Típus == CiklusrendCombo
                                                             orderby a.Sorszám
                                                             select a).ToList();
                            KövetkezőVizsgálat(SzűrtCiklus);
                            KövetkezőV2V3vizsgálat(SzűrtCiklus);
                            ICSelkészülés();
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

        private void KövetkezőV2V3vizsgálat(List<Adat_Ciklus> szűrtCiklus)
        {
            try
            {
                KövV2 = "J";
                KövV2_Sorszám = 0;
                foreach (Adat_Ciklus Elem in szűrtCiklus)
                {
                    if (Elem.Sorszám >= Vsorszám_Jármű + 1)
                    {
                        if (Elem.Vizsgálatfok.Contains("V3") || Elem.Vizsgálatfok.Contains("V2"))
                        {
                            KövV2_Sorszám = Elem.Sorszám;
                            KövV2 = Elem.Vizsgálatfok;
                            return;
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

        private void KövetkezőVizsgálat(List<Adat_Ciklus> szűrtCiklus)
        {
            try
            {
                KövV_Sorszám = 0;
                KövV = "J";
                foreach (Adat_Ciklus Elem in szűrtCiklus)
                {
                    if (Elem.Sorszám == Vsorszám_Jármű + 1)
                    {
                        KövV_Sorszám = Elem.Sorszám;
                        KövV = Elem.Vizsgálatfok;
                        return;
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

        private void Terv_lezárás_T5C5()
        {
            try
            {
                string[] tömb = Hibaszöveg.Text.Trim().Split('-');
                if (tömb.Length >= 2)
                {
                    Vsorszám_Jármű = tömb[1].ToÉrt_Int();
                    Vizsgfoka_Jármű = tömb[0];
                    if (Vsorszám_Jármű > 0)
                    {
                        Vütemezés_Jármű_Dátum = MyF.Szöveg_Tisztítás(tömb[2], 0, 10).ToÉrt_DaTeTime();
                        // ellenőrizzük, hogy szám-e
                        // rögzítendő adatokat ellenőrizzük, hogy tényleg azt akarjuk

                        string hely = Application.StartupPath + @"\Főmérnökség\Adatok\T5C5\Villamos4T5C5.mdb";
                        string jelszó = "pocsaierzsi";
                        string szöveg = $"Select * FROM KMtábla where törölt=false AND [azonosító]='{Pályaszám.Text.Trim()}' AND ";
                        szöveg += $"kövv='{Vizsgfoka_Jármű}' AND ";
                        szöveg += $"kövv_sorszám={Vsorszám_Jármű}";
                        szöveg += $" ORDER BY ID desc";
                        //A következő vizsgálatot nézzük, ami soron következne, ha van ilyen legalább egy akkor rögzítjük,
                        //így biztosítja a program, hogy a ciklusterv szerint legyen végezve.
                        Kezelő_T5C5_Kmadatok KéZKm = new Kezelő_T5C5_Kmadatok();
                        Adat_T5C5_Kmadatok EgyAdat = KéZKm.Egy_Adat(hely, jelszó, szöveg);
                        if (EgyAdat != null)
                        {
                            // kiírjuk az örökítendő adatokat
                            CiklusrendCombo = EgyAdat.Ciklusrend;
                            VizsgKm_Jármű = EgyAdat.KMUkm;

                            // ha V2/V3 volt akkor változik, ha nem akkor marad 
                            if (Vizsgfoka_Jármű.Contains("V2") || Vizsgfoka_Jármű.Contains("V3"))
                                KövV2_számláló = VizsgKm_Jármű;       // V2/V3 volt
                            else
                                KövV2_számláló = EgyAdat.V2V3Számláló;      // minden egyéb

                            List<Adat_Ciklus> SzűrtCiklus = (from a in AdatokCiklus
                                                             where a.Típus == CiklusrendCombo
                                                             orderby a.Sorszám
                                                             select a).ToList();
                            KövetkezőVizsgálat(SzűrtCiklus);
                            KövetkezőV2V3vizsgálat(SzűrtCiklus);
                            T5C5elkészülés();
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

        private void Hiba_Rögzítés_Napló()
        {
            try
            {
                // Naplózás
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\hibanapló\{DateTime.Today:yyyyMM}hibanapló.mdb";
                if (!File.Exists(hely)) Adatbázis_Létrehozás.Hibatáblalap(hely);
                string jelszó = "pozsgaii";
                string szöveg = "INSERT INTO Hibatábla (létrehozta,Korlát,hibaleírása,javítva,típus,azonosító,hibáksorszáma, idő) VALUES (";
                szöveg += $"'{Program.PostásNév}', ";
                if (Jel1.Checked)
                    szöveg += "1, ";
                else if (Jel3.Checked)
                    szöveg += "3, ";
                else
                    szöveg += "4, ";

                szöveg += $"'{Hibaszöveg.Text.Trim()}',  ";
                if (!Javítva.Checked)
                    szöveg += "false, ";
                else
                    szöveg += "true, ";

                szöveg += $"'{Egyed_Típus}', ";
                szöveg += $"'{Pályaszám.Text.Trim()}', ";
                szöveg += Sorszám.Text + ", ";
                szöveg += $"'{DateTime.Now}' )";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Új_hiba_command1_Click(object sender, EventArgs e)
        {
            Bevitelimezők_alap();
            Sorszám.Text = Utolsóhiba.ToString();
        }

        private void Járműlista_excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla_Hibalista.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Karbantartás_" + Program.PostásNév + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Tábla_Hibalista, false);
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



        #region Ellenőrzések

        private void CAFelkészülés()
        {
            try
            {
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\CAF\CAF.mdb";
                string jelszó = "CzabalayL";
                string szöveg = $"SELECT * FROM adatok";

                Kezelő_CAF_Adatok KézCAFAdatok = new Kezelő_CAF_Adatok();
                List<Adat_CAF_Adatok> Adatok = KézCAFAdatok.Lista_Adatok(hely, jelszó, szöveg);

                szöveg = $"SELECT * FROM Alap";
                Kezelő_CAF_alap KézCALAlap = new Kezelő_CAF_alap();
                List<Adat_CAF_alap> AdatokAlap = KézCALAlap.Lista_Adatok(hely, jelszó, szöveg);

                Adat_CAF_Adatok Elem = (from a in Adatok
                                        where a.Azonosító == Pályaszám.Text.Trim() && a.Id == Vsorszám_Jármű
                                        select a).FirstOrDefault();

                if (Elem != null)
                {
                    szöveg = "UPDATE adatok  SET Státus=6 ";
                    szöveg += $" WHERE azonosító='{Pályaszám.Text.Trim()}' AND id={Vsorszám_Jármű}";
                    MyA.ABMódosítás(hely, jelszó, szöveg);

                    // alaptáblában is módosítunk
                    if (Elem.IDŐvKM == 1)
                    {
                        szöveg = "UPDATE alap  SET ";
                        szöveg += $" Utolsó_nap='{Elem.Vizsgálat}', "; // vizsgálatot kapott
                        szöveg += $" Utolsó_Nap_sorszám={Elem.IDŐ_Sorszám}, ";  // vizsgálat sorszáma
                        szöveg += $" Vizsgdátum_nap='{Elem.Dátum:yyyy.MM.dd}', ";
                        szöveg += $" Végezte_nap='{Cmbtelephely.Text.Trim()}' ";
                        szöveg += $" WHERE azonosító='{Pályaszám.Text.Trim()}'";
                    }
                    else
                    {
                        Adat_CAF_alap rekord = (from a in AdatokAlap
                                                where a.Azonosító == Pályaszám.Text.Trim()
                                                select a).FirstOrDefault();
                        DateTime vizsgdátum = new DateTime(1900, 1, 1);
                        long számlálo_old = 0;
                        long számláló = 0;

                        szöveg = "UPDATE alap  SET ";
                        szöveg += $" Utolsó_km='{Elem.Vizsgálat}', "; // vizsgálatot kapott
                        szöveg += $" Utolsó_km_sorszám={Elem.KM_Sorszám}, ";  // vizsgálat sorszáma
                        szöveg += $" Vizsgdátum_km='{Elem.Dátum:yyyy.MM.dd}', ";
                        szöveg += $" Végezte_km='{Cmbtelephely.Text.Trim()}', ";

                        // számláló állás meghatározása
                        if (rekord != null)
                        {
                            vizsgdátum = rekord.Vizsgdátum_km;
                            számlálo_old = rekord.Számláló;
                            számláló = számlálo_old + MyF.Futás_km(Pályaszám.Text.Trim(), vizsgdátum);
                            szöveg += $" számláló={számláló}";
                        }

                        szöveg += $" WHERE azonosító='{Pályaszám.Text.Trim()}'";
                    }
                    MyA.ABMódosítás(hely, jelszó, szöveg);
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

        private void TW6000elkészülés()
        {
            try
            {
                string hely = Application.StartupPath + @"\főmérnökség\adatok\Villamos4TW.mdb";
                string helynapló = $@"{Application.StartupPath}\főmérnökség\napló\naplóTW6000_{DateTime.Today.Year}.mdb";
                if (!File.Exists(helynapló)) Adatbázis_Létrehozás.TW6000táblanapló(helynapló);
                string helynapló2 = $@"{Application.StartupPath}\főmérnökség\napló\naplóTW6000Ütem_{DateTime.Today.Year}.mdb";
                if (!File.Exists(helynapló2)) Adatbázis_Létrehozás.TW6000ütemnapló(helynapló2);

                string szöveg1;
                string ciklusrend;
                DateTime vesedékesség;
                bool kötöttstart;
                bool megállítás;
                DateTime start;

                string jelszó = "czapmiklós";
                string szöveg = $"SELECT * FROM ütemezés";

                Kezelő_TW6000_Ütemezés KétÜtemezés = new Kezelő_TW6000_Ütemezés();
                List<Adat_TW6000_Ütemezés> AdatokÜtemezés = KétÜtemezés.Lista_Adatok(hely, jelszó, szöveg);

                Adat_TW6000_Ütemezés ÜtemElem = (from a in AdatokÜtemezés
                                                 where a.Azonosító == Pályaszám.Text.Trim()
                                                 && a.Vizsgfoka == Vizsgfoka_Jármű
                                                 && a.Vsorszám == Vsorszám_Jármű
                                                 && a.Vütemezés.ToShortDateString() == Vütemezés_Jármű_Dátum.ToShortDateString()
                                                 && a.Státus == 4
                                                 select a).FirstOrDefault();

                if (ÜtemElem != null)
                {
                    ciklusrend = ÜtemElem.Ciklusrend;
                    vesedékesség = ÜtemElem.Vesedékesség;

                    // átírjuk a táblázatban az adatokat
                    szöveg1 = "UPDATE Ütemezés SET ";
                    szöveg1 += $" megjegyzés='Végrehajtva: " + Program.PostásTelephely.Trim() + "', ";
                    szöveg1 += $" státus= 6, ";
                    szöveg1 += $" vvégezte='" + Program.PostásNév.Trim() + "', ";
                    szöveg1 += $" velkészülés='" + DateTime.Now.ToString("yyyy.MM.dd") + "', ";
                    szöveg1 += $" elkészült= true ";
                    szöveg1 += $" WHERE  azonosító='{Pályaszám.Text.Trim()}' AND vizsgfoka='{Vizsgfoka_Jármű}' ";
                    szöveg1 += $" AND vsorszám={Vsorszám_Jármű} AND vütemezés=#{Vütemezés_Jármű_Dátum:MM-dd-yyyy}# AND státus=4";
                    MyA.ABMódosítás(hely, jelszó, szöveg1);

                    // naplózzuk a TW6000-be is
                    szöveg1 = "INSERT INTO ütemezésnapló (azonosító, Ciklusrend, Elkészült, Megjegyzés, rögzítésideje, rögzítő, státus, velkészülés, vesedékesség,";
                    szöveg1 += $"  vizsgfoka, vsorszám, vütemezés, Vvégezte  ) VALUES (";
                    szöveg1 += $"'" + Pályaszám.Text.Trim() + "', ";
                    szöveg1 += $"'" + ciklusrend.Trim() + "', 1, 'Végrehajtva: " + Program.PostásTelephely.Trim() + "', '" + DateTime.Now.ToString() + "', ";
                    szöveg1 += $"'" + Program.PostásNév.Trim() + "', 6, '" + DateTime.Now.ToString("yyyy.MM.dd.") + "', '" + vesedékesség.ToString() + "', '" + Vizsgfoka_Jármű + "', ";
                    szöveg1 += $"{Vsorszám_Jármű}, '{Vütemezés_Jármű_Dátum:MM-dd-yyyy}', '" + Program.PostásTelephely.Trim() + "')";
                    MyA.ABMódosítás(helynapló2, jelszó, szöveg1);
                }

                szöveg = $"SELECT * FROM Alap";
                Kezelő_TW6000_Alap KézTWAlap = new Kezelő_TW6000_Alap();
                List<Adat_TW6000_Alap> AdatokTWAlap = KézTWAlap.Lista_Adatok(hely, jelszó, szöveg);

                Adat_TW6000_Alap ElemAlap = (from a in AdatokTWAlap
                                             where a.Azonosító == Pályaszám.Text.Trim()
                                             select a).FirstOrDefault();


                if (ElemAlap != null)
                {
                    kötöttstart = ElemAlap.Kötöttstart;
                    megállítás = ElemAlap.Megállítás;
                    ciklusrend = ElemAlap.Ciklusrend;
                    start = ElemAlap.Start;
                    szöveg1 = "Update ALAP  SET ";
                    szöveg1 += $" vizsgdátum='" + DateTime.Now.ToString("yyyy.MM.dd.") + "', ";
                    szöveg1 += $" vizsgsorszám={Vsorszám_Jármű}, ";
                    szöveg1 += $" Vizsgnév='" + Vizsgfoka_Jármű + "' ";
                    szöveg1 += $" WHERE  azonosító='" + Pályaszám.Text.Trim() + "' ";
                    MyA.ABMódosítás(hely, jelszó, szöveg1);
                    // naplózás
                    szöveg1 = "INSERT INTO alapnapló (Azonosító, ciklusrend, kötöttstart, megállítás, Oka, rögzítésiidő, rögzítő, start, vizsgdátum, vizsgnév, vizsgsorszám ) VALUES (";
                    szöveg1 += $"'" + Pályaszám.Text.Trim() + "', ";
                    szöveg1 += $"'" + ciklusrend.Trim() + "', ";
                    szöveg1 += kötöttstart.ToString() + "," + megállítás.ToString() + ", ";
                    szöveg1 += $"'Végrehajtva: " + Program.PostásTelephely.Trim() + "', ";
                    szöveg1 += $"'" + DateTime.Now.ToString() + "', ";
                    szöveg1 += $"'" + Program.PostásNév.Trim() + "', ";
                    szöveg1 += $"'" + start.ToString("yyyy.MM.dd.") + "', ";
                    szöveg1 += $"'" + DateTime.Now.ToString("yyyy.MM.dd.") + "', ";
                    szöveg1 += $"'" + Vizsgfoka_Jármű + "', ";
                    szöveg1 += $"{Vsorszám_Jármű} )";
                    MyA.ABMódosítás(helynapló, jelszó, szöveg1);
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

        private void T5C5elkészülés()
        {
            try
            {
                Adat_Jármű Elem = (from a in AdatokFőJármű
                                   where a.Azonosító == Pályaszám.Text.Trim()
                                   && a.Valóstípus.Contains("T5C5")
                                   && !a.Törölt
                                   select a).FirstOrDefault();

                if (Elem != null)
                {
                    string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\T5C5\Villamos4T5C5.mdb";
                    string jelszó = "pocsaierzsi";
                    string szöveg = "SELECT * FROM kmtábla";

                    Kezelő_T5C5_Kmadatok KéZT5C5 = new Kezelő_T5C5_Kmadatok();
                    List<Adat_T5C5_Kmadatok> AdatokT5C5 = KéZT5C5.Lista_Adat(hely, jelszó, szöveg);

                    long i = 1;
                    if (AdatokT5C5.Count > 0) i = AdatokT5C5.Max(a => a.ID) + 1;
                    // Új adat
                    szöveg = "INSERT INTO kmtábla  (ID, azonosító, jjavszám, KMUkm, KMUdátum, ";
                    szöveg += " vizsgfok,  vizsgdátumk, vizsgdátumv,";
                    szöveg += " vizsgkm, havikm, vizsgsorszám, fudátum, ";
                    szöveg += " Teljeskm, Ciklusrend, V2végezte, KövV2_Sorszám, KövV2, ";
                    szöveg += " KövV_Sorszám, KövV, V2V3Számláló, törölt) VALUES (";
                    szöveg += $"{i}, '{Pályaszám.Text.Trim()}', 0, {VizsgKm_Jármű}, '{DateTime.Today:yyyy.MM.dd}', ";
                    szöveg += $"'{Vizsgfoka_Jármű}', '{Vütemezés_Jármű_Dátum:yyyy.MM.dd}', '{DateTime.Today:yyyy.MM.dd}', ";
                    szöveg += $"{VizsgKm_Jármű}, 0, {Vsorszám_Jármű}, '1900.01.01', ";
                    szöveg += $" 0, '{CiklusrendCombo}', '{Program.PostásTelephely.Trim()}', {KövV2_Sorszám}, '{KövV2}', ";
                    szöveg += $"{KövV_Sorszám}, '{KövV.Trim()}', {KövV2_számláló}, false)";
                    MyA.ABMódosítás(hely, jelszó, szöveg);

                    // naplózás
                    hely = $@"{Application.StartupPath}\Főmérnökség\Napló\2021Kmnapló{DateTime.Today.Year}.mdb";
                    if (!File.Exists(hely)) Adatbázis_Létrehozás.KmfutástáblaNapló(hely);

                    szöveg = "INSERT INTO kmtáblaNapló  (ID, azonosító, jjavszám, KMUkm, KMUdátum, ";
                    szöveg += " vizsgfok,  vizsgdátumk, vizsgdátumv,";
                    szöveg += " vizsgkm, havikm, vizsgsorszám, fudátum, ";
                    szöveg += " Teljeskm, Ciklusrend, V2végezte, KövV2_Sorszám, KövV2, ";
                    szöveg += " KövV_Sorszám, KövV, V2V3Számláló, törölt, Módosító, Mikor) VALUES (";
                    szöveg += $"{i}, '{Pályaszám.Text.Trim()}', 0, {VizsgKm_Jármű}, '" + DateTime.Today.ToString("yyyy.MM.dd") + "', ";
                    szöveg += $"'{Vizsgfoka_Jármű}', '{Vütemezés_Jármű_Dátum:yyyy.MM.dd}', '" + DateTime.Today.ToString("yyyy.MM.dd") + "', ";
                    szöveg += $"{VizsgKm_Jármű}, 0, {Vsorszám_Jármű}, '1900.01.01', ";
                    szöveg += $" 0, '{CiklusrendCombo}', '{Program.PostásTelephely.Trim()}', {KövV2_Sorszám}, '{KövV2}', ";
                    szöveg += $"{KövV_Sorszám}, '{KövV.Trim()}', {KövV2_számláló}, false, '{Program.PostásNév.Trim()}', '{DateTime.Now}')";
                    MyA.ABMódosítás(hely, jelszó, szöveg);

                    MessageBox.Show("Az adatok rögzítése megtörtént. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    throw new HibásBevittAdat("A pályaszám nem T5C5!");

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ICSelkészülés()
        {
            try
            {
                Adat_Jármű Elem = (from a in AdatokFőJármű
                                   where a.Azonosító == Pályaszám.Text.Trim()
                                   && (a.Valóstípus.Contains("ICS") || a.Valóstípus.Contains("KCSV"))
                                   && !a.Törölt
                                   select a).FirstOrDefault();
                if (Elem != null)
                {
                    string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\ICSKCSV\Villamos4ICS.mdb";
                    string jelszó = "pocsaierzsi";
                    string szöveg = "SELECT * FROM kmtábla";

                    Kezelő_T5C5_Kmadatok KéZKm = new Kezelő_T5C5_Kmadatok();
                    List<Adat_T5C5_Kmadatok> Adatok = KéZKm.Lista_Adat(hely, jelszó, szöveg);

                    long i = 1;
                    if (Adatok.Count > 0) i = Adatok.Max(a => a.ID) + 1;

                    // Új adat
                    szöveg = "INSERT INTO kmtábla  (ID, azonosító, jjavszám, KMUkm, KMUdátum, ";
                    szöveg += " vizsgfok,  vizsgdátumk, vizsgdátumv,";
                    szöveg += " vizsgkm, havikm, vizsgsorszám, fudátum, ";
                    szöveg += " Teljeskm, Ciklusrend, V2végezte, KövV2_Sorszám, KövV2, ";
                    szöveg += " KövV_Sorszám, KövV, V2V3Számláló, törölt) VALUES (";
                    szöveg += $"{i}, '{Pályaszám.Text.Trim()}', 0, {VizsgKm_Jármű}, '{DateTime.Today:yyyy.MM.dd}', ";
                    szöveg += $"'{Vizsgfoka_Jármű}', '{Vütemezés_Jármű_Dátum:yyyy.MM.dd}', '{DateTime.Today:yyyy.MM.dd}', ";
                    szöveg += $"{VizsgKm_Jármű}, 0, {Vsorszám_Jármű}, '1900.01.01', ";
                    szöveg += $" 0, '{CiklusrendCombo}', '{Program.PostásTelephely.Trim()}', {KövV2_Sorszám}, '{KövV2}', ";
                    szöveg += $"{KövV_Sorszám}, '{KövV.Trim()}', {KövV2_számláló}, false)";
                    MyA.ABMódosítás(hely, jelszó, szöveg);

                    // naplózás
                    hely = $@"{Application.StartupPath}\Főmérnökség\Napló\2021Kmnapló{DateTime.Today.Year}.mdb";
                    if (!File.Exists(hely)) Adatbázis_Létrehozás.KmfutástáblaNapló(hely);

                    szöveg = "INSERT INTO kmtáblaNapló  (ID, azonosító, jjavszám, KMUkm, KMUdátum, ";
                    szöveg += " vizsgfok,  vizsgdátumk, vizsgdátumv,";
                    szöveg += " vizsgkm, havikm, vizsgsorszám, fudátum, ";
                    szöveg += " Teljeskm, Ciklusrend, V2végezte, KövV2_Sorszám, KövV2, ";
                    szöveg += " KövV_Sorszám, KövV, V2V3Számláló, törölt, Módosító, Mikor) VALUES (";
                    szöveg += $"{i}, '{Pályaszám.Text.Trim()}', 0, {VizsgKm_Jármű}, '{DateTime.Today:yyyy.MM.dd}', ";
                    szöveg += $"'{Vizsgfoka_Jármű}', '{Vütemezés_Jármű_Dátum:yyyy.MM.dd}', '{DateTime.Today:yyyy.MM.dd}', ";
                    szöveg += $"{VizsgKm_Jármű}, 0, {Vsorszám_Jármű}, '1900.01.01', ";
                    szöveg += $" 0, '{CiklusrendCombo}', '{Program.PostásTelephely.Trim()}', {KövV2_Sorszám}, '{KövV2}', ";
                    szöveg += $"{KövV_Sorszám}, '{KövV.Trim()}', {KövV2_számláló}, false, '{Program.PostásNév.Trim()}', '{DateTime.Now}')";
                    MyA.ABMódosítás(hely, jelszó, szöveg);

                    MessageBox.Show("Az adatok rögzítése megtörtént. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    throw new HibásBevittAdat("A pályaszám nem ICS vagy KCSV!");

            }
            catch (HibásBevittAdat ex)
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



        #region Naplózás
        private void Napló_excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Napló_tábla.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Karbantartási_Napló_" + Program.PostásNév + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Napló_tábla, false);
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


        private void Szűrés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Dátumtól.Value >= Dátumig.Value) Dátumig.Value = Dátumtól.Value.AddDays(+1);

                DateTime ideigdátum = Dátumtól.Value;

                Napló_Tábla_Fejléc();

                Naplók_feltöltése();
                DateTime Időtől = new DateTime(Dátumtól.Value.Year, Dátumtól.Value.Month, Dátumtól.Value.Day, 0, 0, 0);
                DateTime Időig = new DateTime(Dátumig.Value.Year, Dátumig.Value.Month, Dátumig.Value.Day, 23, 59, 59);

                if (AdatokNapló == null) return;
                List<Adat_Jármű_hiba> Adatok;
                if (Napló_pályaszám.Text.Trim() != "")
                {
                    Adatok = (from a in AdatokNapló
                              where a.Azonosító == Napló_pályaszám.Text.Trim() &&
                              (a.Idő >= Időtől && a.Idő <= Időig)
                              orderby a.Idő
                              select a).ToList();
                }
                else
                {
                    Adatok = (from a in AdatokNapló
                              where (a.Idő >= Időtől && a.Idő <= Időig)
                              orderby a.Idő
                              select a).ToList();

                }

                foreach (Adat_Jármű_hiba rekord in Adatok)
                {
                    Napló_tábla.RowCount++;
                    int i = Napló_tábla.RowCount - 1;
                    Napló_tábla.Rows[i].Cells[0].Value = i + 1;
                    Napló_tábla.Rows[i].Cells[1].Value = rekord.Azonosító;
                    Napló_tábla.Rows[i].Cells[2].Value = rekord.Idő;
                    Napló_tábla.Rows[i].Cells[3].Value = rekord.Hibaleírása;
                    Napló_tábla.Rows[i].Cells[4].Value = Enum.GetName(typeof(MyEn.Jármű_Státus), rekord.Korlát);
                    if (rekord.Javítva)
                        Napló_tábla.Rows[i].Cells[5].Value = "Igen";
                    else
                        Napló_tábla.Rows[i].Cells[5].Value = "Nem";

                    Napló_tábla.Rows[i].Cells[6].Value = rekord.Létrehozta;
                }

                Napló_tábla.Visible = true;
                Napló_tábla.Refresh();
                Napló_tábla.ClearSelection();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Napló_Tábla_Fejléc()
        {
            Napló_tábla.Rows.Clear();
            Napló_tábla.Columns.Clear();
            Napló_tábla.Refresh();
            Napló_tábla.Visible = false;
            Napló_tábla.ColumnCount = 7;
            // fejléc elkészítése
            Napló_tábla.Columns[0].HeaderText = "Sorszám";
            Napló_tábla.Columns[0].Width = 80;
            Napló_tábla.Columns[1].HeaderText = "Pályaszám";
            Napló_tábla.Columns[1].Width = 90;
            Napló_tábla.Columns[2].HeaderText = "Dátum";
            Napló_tábla.Columns[2].Width = 160;
            Napló_tábla.Columns[3].HeaderText = "Hiba szöveg";
            Napló_tábla.Columns[3].Width = 600;
            Napló_tábla.Columns[4].HeaderText = "Hiba Státus";
            Napló_tábla.Columns[4].Width = 130;
            Napló_tábla.Columns[5].HeaderText = "Javítva";
            Napló_tábla.Columns[5].Width = 90;
            Napló_tábla.Columns[6].HeaderText = "Módosító";
            Napló_tábla.Columns[6].Width = 120;
        }
        #endregion



        #region darabszámok
        private void Frissíti_darabszámokat_Click(object sender, EventArgs e)
        {
            try
            {
                ListákFeltöltése();

                Tábla_darabszámok.Rows.Clear();
                Tábla_darabszámok.Columns.Clear();
                Tábla_darabszámok.Refresh();
                Tábla_darabszámok.Visible = false;
                Tábla_darabszámok.ColumnCount = 8;

                // fejléc elkészítése
                Tábla_darabszámok.Columns[0].HeaderText = "Típus";
                Tábla_darabszámok.Columns[0].Width = 150;
                Tábla_darabszámok.Columns[1].HeaderText = "Állományi darab";
                Tábla_darabszámok.Columns[1].Width = 100;
                Tábla_darabszámok.Columns[2].HeaderText = "Üzemképes";
                Tábla_darabszámok.Columns[2].Width = 100;
                Tábla_darabszámok.Columns[3].HeaderText = "Kocsiszíni";
                Tábla_darabszámok.Columns[3].Width = 100;
                Tábla_darabszámok.Columns[4].HeaderText = "Kocsiszínen kívül";
                Tábla_darabszámok.Columns[4].Width = 100;
                Tábla_darabszámok.Columns[5].HeaderText = "Félreállítás";
                Tábla_darabszámok.Columns[5].Width = 100;
                Tábla_darabszámok.Columns[6].HeaderText = "Főműhely";
                Tábla_darabszámok.Columns[6].Width = 100;
                Tábla_darabszámok.Columns[7].HeaderText = "Javítás Összesen";
                Tábla_darabszámok.Columns[7].Width = 100;

                List<Adat_Jármű_Állomány_Típus> Adatok = AdatokTípus.OrderBy(x => x.Id).ToList();

                foreach (Adat_Jármű_Állomány_Típus rekord in Adatok)
                {
                    Tábla_darabszámok.RowCount++;
                    int i = Tábla_darabszámok.RowCount - 1;
                    Tábla_darabszámok.Rows[i].Cells[0].Value = rekord.Típus;


                    int Üzemképes = (from a in AdatokJármű
                                     where a.Típus == rekord.Típus && a.Státus < 4
                                     select a).ToList().Count;

                    Tábla_darabszámok.Rows[i].Cells[2].Value = Üzemképes;
                    int Összesen = (from a in AdatokHiba
                                    where a.Típus == rekord.Típus && a.Korlát == 4
                                    select a).ToList().GroupBy(a => a.Azonosító).ToList().Count;
                    Tábla_darabszámok.Rows[i].Cells[7].Value = Összesen;
                    int főműhelyi = (from a in AdatokHiba
                                     where a.Típus == rekord.Típus && a.Korlát == 4 && a.Hibaleírása.Contains("#")
                                     select a).ToList().GroupBy(a => a.Azonosító).ToList().Count;
                    Tábla_darabszámok.Rows[i].Cells[6].Value = főműhelyi;
                    int félreállítás = (from a in AdatokHiba
                                        where a.Típus == rekord.Típus && a.Korlát == 4 && a.Hibaleírása.Contains("&")
                                        select a).ToList().GroupBy(a => a.Azonosító).ToList().Count;
                    Tábla_darabszámok.Rows[i].Cells[5].Value = félreállítás;
                    int telepenkívül = (from a in AdatokHiba
                                        where a.Típus == rekord.Típus && a.Korlát == 4 && a.Hibaleírása.Contains("§")
                                        select a).ToList().GroupBy(a => a.Azonosító).ToList().Count;
                    Tábla_darabszámok.Rows[i].Cells[4].Value = telepenkívül;
                    Tábla_darabszámok.Rows[i].Cells[3].Value = Összesen - (főműhelyi + félreállítás + telepenkívül);
                    Tábla_darabszámok.Rows[i].Cells[1].Value = Üzemképes + Összesen;
                }

                Tábla_darabszámok.Visible = true;
                Tábla_darabszámok.Refresh();
                Tábla_darabszámok.ClearSelection();
            }
            catch (HibásBevittAdat ex)
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


        #region Gombok
        private void Gombok_feltöltés()
        {
            try
            {
                Gombok_típus.Items.Clear();

                List<string> Típusok = AdatokJármű.Select(a => a.Valóstípus).Distinct().ToList();
                foreach (string rekord in Típusok)
                    Gombok_típus.Items.Add(rekord);
                Gombok_típus.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Gombok_frissít_Click(object sender, EventArgs e)
        {
            ListákFeltöltése();
            Kocsikiirása_gombok();
        }

        private void Kocsikiirása_gombok()
        {
            try
            {
                if (GombokSzáma != 0)
                {
                    // ha nem nulla akkor előbb a gombokat le kell szedni
                    GombokSzáma = 0;
                    GombPanel.Controls.Clear();
                }

                List<Adat_Jármű> Adatok = (from a in AdatokJármű
                                           where a.Típus == Gombok_típus.Text.Trim()
                                           select a).ToList();

                int i = 1;
                int j = 1;
                int darab = 0;
                int k = 1;
                if (Adatok != null)
                {

                    foreach (Adat_Jármű rekord in Adatok)
                    {
                        Button Telephelygomb = new Button
                        {
                            Location = new Point(10 + 80 * (k - 1), 10 + (j - 1) * 60),
                            Size = new Size(70, 50),
                            Name = "Kocsi_" + (darab + 1),
                            Text = rekord.Azonosító.Trim() + "\n" + rekord.Típus.Trim()
                        };

                        switch (rekord.Státus)
                        {
                            case 0:
                                {
                                    Telephelygomb.BackColor = Color.Silver;
                                    break;
                                }
                            case 1:
                                {
                                    Telephelygomb.BackColor = Color.Green;
                                    break;
                                }
                            case 2:
                                {
                                    Telephelygomb.BackColor = Color.Yellow;
                                    break;
                                }
                            case 3:
                                {
                                    Telephelygomb.BackColor = Color.Yellow;
                                    break;
                                }
                            case 4:
                                {
                                    Telephelygomb.BackColor = Color.Red;
                                    break;
                                }
                        }


                        Telephelygomb.Visible = true;
                        ToolTip1.SetToolTip(Telephelygomb, rekord.Azonosító.Trim());

                        Telephelygomb.MouseDown += Telephelyre_MouseDown;

                        GombPanel.Controls.Add(Telephelygomb);
                        GombokSzáma = i;

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
            string[] tömb = (sender as Button).Text.Split('\n');
            Pályaszám.Text = tömb[0];
            Fülek.SelectedIndex = 1;
        }
        #endregion


        #region Listázások
        private void HibaListázás()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\hiba.mdb";
                if (!File.Exists(hely)) Adatbázis_Létrehozás.Hibatáblalap(hely);

                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM Hibatábla ORDER BY azonosító, Korlát, hibáksorszáma";

                AdatokHiba.Clear();
                AdatokHiba = KézHiba.Lista_adatok(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void JárművekLista()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\villamos\villamos.mdb";
                if (!File.Exists(hely)) return;
                string szöveg = "Select * FROM állománytábla ORDER BY azonosító";
                string jelszó = "pozsgaii";

                AdatokJármű.Clear();
                AdatokJármű = KéZJármű.Lista_Adatok(hely, jelszó, szöveg);

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void JárművekFŐLista()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\villamos.mdb";
                if (!File.Exists(hely)) return;
                string szöveg = "Select * FROM állománytábla ORDER BY azonosító";
                string jelszó = "pozsgaii";

                AdatokFőJármű.Clear();
                AdatokFőJármű = KéZJármű.Lista_Adatok(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Szer_Feltöltés()
        {
            try
            {
                AdatokSzer.Clear();
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\villamos\szerelvény.mdb";
                if (!File.Exists(hely)) return;
                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM szerelvénytábla order by [kocsi1]";
                AdatokSzer = KézSzerelvény.Lista_Adatok(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void Naplók_feltöltése()
        {
            try
            {
                AdatokNapló.Clear();
                DateTime ideigdátum = Dátumtól.Value;
                while (Dátumig.Value > ideigdátum)
                {
                    string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\hibanapló\{ideigdátum:yyyyMM}hibanapló.mdb";
                    string jelszó = "pozsgaii";
                    string szöveg = "SELECT * FROM hibatábla ";
                    if (File.Exists(hely))
                    {
                        List<Adat_Jármű_hiba> Ideig = KézHiba.Lista_adatok(hely, jelszó, szöveg);
                        if (Ideig != null) AdatokNapló.AddRange(Ideig);
                    }
                    ideigdátum = ideigdátum.AddMonths(1);
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

        private void Ciklus_Feltölése()
        {
            try
            {
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\ciklus.mdb";
                string jelszó = "pocsaierzsi";
                string szöveg = $"SELECT * FROM ciklusrendtábla WHERE [törölt]=false ORDER BY sorszám";
                AdatokCiklus.Clear();
                AdatokCiklus = KézCiklus.Lista_Adatok(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void Napi_Feltölése()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\hibanapló\napi.mdb";
                if (!File.Exists(hely)) Adatbázis_Létrehozás.Javításiátfutástábla(hely);
                string jelszó = "plédke";
                string szöveg = $"SELECT * FROM xnapostábla";
                AdatokXnapos.Clear();
                AdatokXnapos = KézXnapos.Lista_Adatok(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
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