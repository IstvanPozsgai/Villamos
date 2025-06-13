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

    public partial class Ablak_Fogaskerekű_Tulajdonságok
    {
        long utolsósor;
        string _fájlexc;
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly Kezelő_Ciklus KézCiklus = new Kezelő_Ciklus();
        readonly Kezelő_jármű_hiba KézHiba = new Kezelő_jármű_hiba();
        readonly Kezelő_T5C5_Kmadatok KézKMAdatok = new Kezelő_T5C5_Kmadatok("SGP");
        readonly Kezelő_kiegészítő_telephely KézKieg = new Kezelő_kiegészítő_telephely();
        readonly Kezelő_T5C5_Előterv KézElőterv = new Kezelő_T5C5_Előterv();
        readonly Kezelő_Kerék_Mérés KézMérés = new Kezelő_Kerék_Mérés();

        List<Adat_T5C5_Kmadatok> AdatokKm = new List<Adat_T5C5_Kmadatok>();
        List<Adat_Jármű> AdatokJármű = new List<Adat_Jármű>();
        List<Adat_Kerék_Mérés> AdatokMérés = new List<Adat_Kerék_Mérés>();
        DataTable AdatTábla = new DataTable();

        int Hónapok = 24;
        int Havifutás = 1500;

        #region Alap
        public Ablak_Fogaskerekű_Tulajdonságok()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            Telephelyekfeltöltése();
            Pályaszám_feltöltés();
            Fülek.SelectedIndex = 0;
            Fülekkitöltése();
            Jogosultságkiosztás();
            Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;
            Ciklusrendcombofeltöltés();
            Üzemek_listázása();
        }

        private void Tulajdonságok_Fogaskerekű_Load(object sender, EventArgs e)
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
            int melyikelem;

            // ide kell az összes gombot tenni amit szabályozni akarunk false

            Utolsó_V_rögzítés.Enabled = false;
            Töröl.Enabled = false;
            // csak főmérnökségi belépéssel törölhető
            if ((Program.PostásTelephely) == "Főmérnökség")
            {
                Töröl.Visible = true;
            }
            else
            {
                Töröl.Visible = false;
            }
            melyikelem = 109;
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

        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\Tulajdonság_Fogaskerekű.html";
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
                if (Cmbtelephely.Text.Trim() == "") return;
                List<Adat_Jármű> Adatok = KézJármű.Lista_Adatok("Főmérnökség");
                Adatok = (from a in Adatok
                          where a.Valóstípus.Contains("SGP")
                          && a.Törölt == false
                          orderby a.Azonosító
                          select a).ToList();

                foreach (Adat_Jármű Elem in Adatok)
                    Pályaszám.Items.Add(Elem.Azonosító);

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
            try
            {
                if (Pályaszám.Text.Trim() == "") return;

                switch (Fülek.SelectedIndex)
                {
                    case 1:
                        {
                            Vizsgfokcombofeltölés();
                            break;
                        }
                    case 2:
                        {
                            Kiirjaatörténelmet();
                            break;
                        }
                    case 3:
                        {

                            Pszlista();
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

        private void Fülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }

        private void Fülekkitöltése()
        {
            switch (Fülek.SelectedIndex)
            {
                case 1:
                    {
                        break;
                    }

                case 2:
                    {
                        Kiirjaatörténelmet();
                        break;
                    }

                case 3:
                    {

                        Pszlista();
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


        #region Egyéb
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

        private void Ciklusrendcombofeltöltés()
        {
            try
            {
                CiklusrendCombo.Items.Clear();
                List<Adat_Ciklus> AdatokCiklus = KézCiklus.Lista_Adatok();
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

        private void Pályaszám_SelectedIndexChanged(object sender, EventArgs e)
        {
            Frissít();
        }
        #endregion


        #region Lekérdezés lapfül
        /// <summary>
        /// Lekérdezés gomb megnyomásakor a kiválasztott telephelyhez tartozó járművek adatait kilistázzuk
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
                Tábla_lekérdezés.ColumnCount = 18;

                // fejléc elkészítése
                Tábla_lekérdezés.Columns[0].HeaderText = "Psz";
                Tábla_lekérdezés.Columns[0].Width = 70;
                Tábla_lekérdezés.Columns[1].HeaderText = "Típus";
                Tábla_lekérdezés.Columns[1].Width = 70;
                Tábla_lekérdezés.Columns[2].HeaderText = "Vizsg. foka";
                Tábla_lekérdezés.Columns[2].Width = 100;
                Tábla_lekérdezés.Columns[3].HeaderText = "Vizsg. Ssz.";
                Tábla_lekérdezés.Columns[3].Width = 100;
                Tábla_lekérdezés.Columns[4].HeaderText = "Vizsg. Kezdete";
                Tábla_lekérdezés.Columns[4].Width = 110;
                Tábla_lekérdezés.Columns[5].HeaderText = "Vizsg. Vége";
                Tábla_lekérdezés.Columns[5].Width = 110;
                Tábla_lekérdezés.Columns[6].HeaderText = "Vizsg KM állás";
                Tábla_lekérdezés.Columns[6].Width = 100;
                Tábla_lekérdezés.Columns[7].HeaderText = "Frissítés Dátum";
                Tábla_lekérdezés.Columns[7].Width = 110;
                Tábla_lekérdezés.Columns[8].HeaderText = "KM J-óta";
                Tábla_lekérdezés.Columns[8].Width = 100;
                Tábla_lekérdezés.Columns[9].HeaderText = "V után futott";
                Tábla_lekérdezés.Columns[9].Width = 100;
                Tábla_lekérdezés.Columns[10].HeaderText = "Havi km";
                Tábla_lekérdezés.Columns[10].Width = 100;
                Tábla_lekérdezés.Columns[11].HeaderText = "Felújítás szám";
                Tábla_lekérdezés.Columns[11].Width = 100;
                Tábla_lekérdezés.Columns[12].HeaderText = "Felújítás Dátum";
                Tábla_lekérdezés.Columns[12].Width = 110;
                Tábla_lekérdezés.Columns[13].HeaderText = "Ciklusrend típus";
                Tábla_lekérdezés.Columns[13].Width = 100;
                Tábla_lekérdezés.Columns[14].HeaderText = "Üzembehelyezés km";
                Tábla_lekérdezés.Columns[14].Width = 100;
                Tábla_lekérdezés.Columns[15].HeaderText = "Jármű státusz";
                Tábla_lekérdezés.Columns[15].Width = 100;
                Tábla_lekérdezés.Columns[16].HeaderText = "Hiba leírása";
                Tábla_lekérdezés.Columns[16].Width = 100;
                Tábla_lekérdezés.Columns[17].HeaderText = "Járműtípus";
                Tábla_lekérdezés.Columns[17].Width = 100;
                // kilistázzuk a adatbázis adatait
                List<Adat_Jármű> Adatok = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                List<Adat_Jármű_hiba> AdatokHiba = KézHiba.Lista_Adatok(Cmbtelephely.Text.Trim());

                List<Adat_T5C5_Kmadatok> AdatokKm = KézKMAdatok.Lista_Adatok().Where(a => a.Törölt == false).OrderByDescending(a => a.Vizsgdátumk).ToList();

                int i;
                foreach (Adat_Jármű rekordhonnan in Adatok)
                {
                    Adat_T5C5_Kmadatok rekord = (from a in AdatokKm
                                                 where a.Azonosító == rekordhonnan.Azonosító.Trim()
                                                 select a).FirstOrDefault();

                    if (rekord != null)
                    {
                        Tábla_lekérdezés.RowCount++;
                        i = Tábla_lekérdezés.RowCount - 1;
                        Tábla_lekérdezés.Rows[i].Cells[0].Value = rekord.Azonosító.Trim();
                        Tábla_lekérdezés.Rows[i].Cells[1].Value = rekordhonnan.Típus.Trim();
                        Tábla_lekérdezés.Rows[i].Cells[2].Value = rekord.Vizsgfok.Trim();
                        Tábla_lekérdezés.Rows[i].Cells[3].Value = rekord.Vizsgsorszám;
                        Tábla_lekérdezés.Rows[i].Cells[4].Value = rekord.Vizsgdátumk.ToString("yyyy.MM.dd");
                        Tábla_lekérdezés.Rows[i].Cells[5].Value = rekord.Vizsgdátumv.ToString("yyyy.MM.dd");
                        Tábla_lekérdezés.Rows[i].Cells[6].Value = rekord.Vizsgkm;
                        Tábla_lekérdezés.Rows[i].Cells[7].Value = rekord.KMUdátum.ToString("yyyy.MM.dd");
                        Tábla_lekérdezés.Rows[i].Cells[8].Value = rekord.KMUkm;
                        if (rekord.Vizsgsorszám == 0)
                            Tábla_lekérdezés.Rows[i].Cells[9].Value = rekord.KMUkm;
                        else
                            Tábla_lekérdezés.Rows[i].Cells[9].Value = rekord.KMUkm - rekord.Vizsgkm;

                        Tábla_lekérdezés.Rows[i].Cells[10].Value = rekord.Havikm;
                        Tábla_lekérdezés.Rows[i].Cells[11].Value = rekord.Jjavszám;
                        Tábla_lekérdezés.Rows[i].Cells[12].Value = rekord.Fudátum.ToString("yyyy.MM.dd");
                        Tábla_lekérdezés.Rows[i].Cells[13].Value = rekord.Ciklusrend;
                        Tábla_lekérdezés.Rows[i].Cells[14].Value = rekord.Teljeskm;
                        switch (rekordhonnan.Státus)
                        {
                            case 0:
                                {
                                    Tábla_lekérdezés.Rows[i].Cells[15].Value = "Üzemképes";
                                    break;
                                }
                            case 1:
                                {
                                    Tábla_lekérdezés.Rows[i].Cells[15].Value = "Szabad";
                                    break;
                                }
                            case 2:
                                {
                                    Tábla_lekérdezés.Rows[i].Cells[15].Value = "Beálló";
                                    break;
                                }
                            case 3:
                                {
                                    Tábla_lekérdezés.Rows[i].Cells[15].Value = "Beállóba adott";
                                    break;
                                }
                            case 4:
                                {
                                    Tábla_lekérdezés.Rows[i].Cells[15].Value = "Üzemképtelen";

                                    Adat_Jármű_hiba AdatHiba = (from a in AdatokHiba
                                                                where a.Korlát == 4
                                                                && a.Azonosító == rekordhonnan.Azonosító.Trim()
                                                                select a).FirstOrDefault();
                                    Tábla_lekérdezés.Rows[i].Cells[16].Value = AdatHiba.Hibaleírása;
                                    break;
                                }
                        }

                        Tábla_lekérdezés.Rows[i].Cells[17].Value = rekordhonnan.Típus.Trim();
                    }
                }
                Tábla_lekérdezés.Refresh();
                Tábla_lekérdezés.Visible = true;

            }
            catch (HibásBevittAdat ex)
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
        /// Excel fájlba mentés a lekérdezett a táblázatban kilistázott adatokból
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Excellekérdezés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla_lekérdezés.Rows.Count <= 0)
                    return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"SGP_futásadatok_{Program.PostásTelephely}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = MyF.Szöveg_Tisztítás(fájlexc, 0, fájlexc.Length - 5);
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
        /// Az adatbázis teljes tartalmának Excel fájlba mentése.
        /// a Listában tárolt adatokat datatable tesszük
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Teljes_adatbázis_excel_Click(object sender, EventArgs e)
        {
            try
            {

                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    // kimeneti fájl helye és neve
                    InitialDirectory = "MyDocuments",

                    Title = "Adatbázis mentése Excel fájlba",
                    FileName = $"SGP_adatbázis_mentés_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    _fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                AdatokKm = KézKMAdatok.Lista_Adatok();
                AdatTábla = MyF.ToDataTable(AdatokKm);
                Holtart.Be();
                timer1.Enabled = true;
                SZál_ABadatbázis(() =>
                { //leállítjuk a számlálót és kikapcsoljuk a holtartot.
                    timer1.Enabled = false;
                    Holtart.Be();
                    MessageBox.Show("Az Excel tábla elkészült !", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MyE.Megnyitás(_fájlexc);
                    Holtart.Ki();
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

        /// <summary>
        /// Szálban futó eljárás, ami elkészíti az Excel fájlt
        /// </summary>
        /// <param name="callback"></param>
        private void SZál_ABadatbázis(Action callback)
        {
            Thread proc = new Thread(() =>
            {
                // elkészítjük a formanyomtatványt változókat nem lehet küldeni definiálni kell egy külső változót
                MyE.EXCELtábla(AdatTábla, _fájlexc);

                this.Invoke(callback, new object[] { });
            });
            proc.Start();
        }

        /// <summary>
        /// Számláló eljárás, ami a holtartóban fut
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer1_Tick(object sender, EventArgs e)
        {
            Holtart.Lép();
        }
        #endregion


        #region Vizsgálati adatok lapfül
        ///// <summary>
        ///// A kiválasztott pályaszámhoz tartozó vizsgafokokat és a vizsgák időpontját kiírja a táblázatba
        ///// a táblázatba kattintást követően töltődik ki a rögzítési lap
        ///// </summary>
        private void Kiirjaatörténelmet()
        {
            try
            {
                AdatokKm = KézKMAdatok.Lista_Adatok().Where(a => a.Törölt == false).ToList();

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

                List<Adat_T5C5_Kmadatok> Adatok = (from a in AdatokKm
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

        ///// <summary>
        ///// A táblázat cellájára kattintva a kiválasztott adatokat kiírja a rögzítési lapra és
        ///// arra a lapra fog ugrani
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        private void Tábla1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Kiüríti_lapfül();
            if (e.RowIndex < 0) return;

            Sorszám.Text = Tábla1.Rows[e.RowIndex].Cells[0].Value.ToString();

            Vizsg_sorszám_combo.Text = Tábla1.Rows[e.RowIndex].Cells[3].Value.ToString();
            Vizsgfok_új.Text = Tábla1.Rows[e.RowIndex].Cells[2].Value.ToString();
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


            Fülek.SelectedIndex = 1;
        }
        #endregion

        /// <summary>
        /// Frissíti a táblázat adatait
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VizsA_Frisss_Click(object sender, EventArgs e)
        {
            Kiirjaatörténelmet();
        }

        /// <summary>
        /// Excel fájlba mentés a táblázatban kilistázott adatokból
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VizsA_Excel_Click(object sender, EventArgs e)
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
                    FileName = $"{Pályaszám.Text.Trim()}_{Program.PostásTelephely.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = MyF.Szöveg_Tisztítás(fájlexc, 0, fájlexc.Length - 5);
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


        #region Rögzítés lap
        /// <summary>
        /// Ürítjük a mezőket
        /// </summary>
        private void Kiüríti_lapfül()
        {
            Sorszám.Text = "";

            Sorszám.Text = "0";
            Vizsgfok_új.Text = "";
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

        /// <summary>
        /// A vizsgafok combot feltöltjük a kiválasztott ciklus rend alapján
        /// </summary>
        private void Vizsgfokcombofeltölés()
        {
            try
            {
                string ideig = Vizsg_sorszám_combo.Text;
                List<Adat_Ciklus> Adatok = KézCiklus.Lista_Adatok();
                Adatok = (from a in Adatok
                          where a.Típus == CiklusrendCombo.Text.Trim()
                          && a.Törölt == "0"
                          orderby a.Sorszám
                          select a).ToList();
                Vizsg_sorszám_combo.Items.Clear();
                foreach (Adat_Ciklus Elem in Adatok)
                    Vizsg_sorszám_combo.Items.Add(Elem.Sorszám);
                Vizsg_sorszám_combo.Refresh();
                Vizsg_sorszám_combo.Text = ideig;
            }
            catch (HibásBevittAdat ex)
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
        /// Kiírja a kiválasztott rekord adatait a mezőkbe
        /// </summary>
        /// <param name="sorszám">long</param>
        private void KiírjaAdatot(long sorszám)
        {
            try
            {
                Kiüríti_lapfül();
                List<Adat_T5C5_Kmadatok> Adatok = KézKMAdatok.Lista_Adatok();
                Adat_T5C5_Kmadatok rekord = (from a in Adatok
                                             where a.ID == sorszám
                                             select a).FirstOrDefault();
                if (rekord != null)
                {
                    Vizsgfok_új.Text = rekord.Vizsgfok.Trim();
                    Vizsg_sorszám_combo.Text = rekord.Vizsgsorszám.ToString();
                    Vizsgdátumk.Value = rekord.Vizsgdátumk;
                    Vizsgdátumv.Value = rekord.Vizsgdátumv;
                    VizsgKm.Text = rekord.Vizsgkm.ToString();
                    KMUdátum.Value = rekord.KMUdátum;
                    KMUkm.Text = rekord.KMUkm.ToString();
                    HaviKm.Text = rekord.Havikm.ToString();
                    Jjavszám.Text = rekord.Jjavszám.ToString();
                    Utolsófelújításdátuma.Value = rekord.Fudátum;
                    TEljesKmText.Text = rekord.Teljeskm.ToString();
                    CiklusrendCombo.Text = rekord.Ciklusrend.ToString();
                    Sorszám.Text = rekord.ID.ToString();
                    if (rekord.V2végezte.Trim() != "")
                        Üzemek.Text = rekord.V2végezte.Trim();
                    else
                        Üzemek.Text = "";
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
        /// A kiválasztott ciklus rend alapján feltöltjük a vizsgafok combot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CiklusrendCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Vizsgfokcombofeltölés();
        }

        /// <summary>
        /// Ürítjük a mezőket
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Új_adat_Click(object sender, EventArgs e)
        {
            Kiüríti_lapfül();
        }

        /// <summary>
        /// Következő vizsgálat sorszámát kiírja a mezőbe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Következő_V_Click(object sender, EventArgs e)
        {
            if (Vizsg_sorszám_combo.Text.Trim() == "") return;
            Sorszám.Text = "";
            Vizsg_sorszám_combo.Text = (int.Parse(Vizsg_sorszám_combo.Text) + 1).ToString();
            Vizsgdátumk.Value = DateTime.Today;
            Vizsgdátumv.Value = DateTime.Today;
            VizsgKm.Text = KMUkm.Text;
        }

        /// <summary>
        /// A kiválasztott vizsgálat sorszám alapján kiírja a vizsgálat fokát
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Vizsg_sorszám_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int i = Vizsg_sorszám_combo.SelectedIndex;
                if (CiklusrendCombo.Text.Trim() == "") return;

                List<Adat_Ciklus> CiklusAdat = KézCiklus.Lista_Adatok();
                CiklusAdat = CiklusAdat.Where(a => a.Típus.Trim() == CiklusrendCombo.Text.Trim()).OrderBy(a => a.Sorszám).ToList();
                string Vizsgálatfok = (from a in CiklusAdat
                                       where a.Sorszám == i
                                       select a.Vizsgálatfok).FirstOrDefault();

                if (Vizsgálatfok != null)
                    Vizsgfok_új.Text = Vizsgálatfok;

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

        /// <summary>
        /// Rögzíti/Módosítja a kiválasztott vizsgálat adatait
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Utolsó_V_rögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                // leellenőrizzük, hogy minden adat ki van-e töltve

                if (VizsgKm.Text.Trim() == "") throw new HibásBevittAdat("Vizsgálat km számláló állása mező nem lehet üres");
                if (!long.TryParse(VizsgKm.Text, out long vizsgKm)) throw new HibásBevittAdat("Vizsgálat km számláló állása mezőnek egész számnak kell lennie.");
                if (Vizsgfok_új.Text.Trim() == "") throw new HibásBevittAdat("Vizsgálat fok mező nem lehet üres");
                if (Vizsg_sorszám_combo.Text.Trim() == "") throw new HibásBevittAdat("Vizsgálat sorszáma mező nem lehet üres.");
                if (!long.TryParse(Vizsg_sorszám_combo.Text, out long sorszám)) throw new HibásBevittAdat("Vizsgálat sorszáma mezőnek egész számnak kell lennie.");
                if (KMUkm.Text.Trim() == "") KMUkm.Text = "0";
                if (!long.TryParse(KMUkm.Text, out long kmukm)) throw new HibásBevittAdat("Utolsó felújítás óta futott km mezőnek egész számnak kell lennie.");
                if (HaviKm.Text.Trim() == "") HaviKm.Text = 0.ToString();
                if (!long.TryParse(HaviKm.Text, out long haviKm)) throw new HibásBevittAdat("Havi futásteljesítmény mezőnek egész számnak kell lennie.");
                if (Jjavszám.Text.Trim() == "") Jjavszám.Text = "0";
                if (!int.TryParse(Jjavszám.Text, out int jjavszám)) throw new HibásBevittAdat("Felújítás sorszáma mezőnek egész számnak kell lennie.");
                if (TEljesKmText.Text.Trim() == "") TEljesKmText.Text = 0.ToString();
                if (!long.TryParse(TEljesKmText.Text, out long tEljesKmText)) throw new HibásBevittAdat("Üzembehelyezés óta futott mezőnek egész számnak kell lennie.");
                if (CiklusrendCombo.Text.Trim() == "") throw new HibásBevittAdat("Ütemezés típusa mező nem lehet üres.");
                if (!long.TryParse(Sorszám.Text.Trim(), out long sorszámId)) sorszámId = 0;
                if (!int.TryParse(KövV2_Sorszám.Text, out int kövV2_Sorszám)) throw new HibásBevittAdat("Következő V2-V3 sorszám mező nem lehet üres és egész számnak kell lennie.");
                if (!int.TryParse(KövV_Sorszám.Text, out int kövV_Sorszám)) throw new HibásBevittAdat("Következő V mező nem lehet üres és egész számnak kell lennie.");
                if (!int.TryParse(KövV2km.Text, out int kövV2km)) throw new HibásBevittAdat("V2-V3-tól futott km mező nem lehet üres és egész számnak kell lennie.");
                if (!long.TryParse(KövV2_számláló.Text, out long kövV2_számláló)) throw new HibásBevittAdat("V2-V3 számláló állás mező nem lehet üres és egész számnak kell lennie.");


                Adat_T5C5_Kmadatok ADAT = new Adat_T5C5_Kmadatok(
                                sorszámId,
                                Pályaszám.Text.Trim(),
                                jjavszám,
                                kmukm,
                                KMUdátum.Value,
                                Vizsgfok_új.Text,
                                Vizsgdátumk.Value,
                                Vizsgdátumv.Value,
                                vizsgKm,
                                haviKm,
                                sorszám,
                                Utolsófelújításdátuma.Value,
                                tEljesKmText,
                                MyF.Szöveg_Tisztítás(CiklusrendCombo.Text.Trim()),
                                MyF.Szöveg_Tisztítás(Üzemek.Text.Trim()),
                                kövV2_Sorszám,
                                MyF.Szöveg_Tisztítás(KövV2.Text.Trim()),
                                kövV_Sorszám,
                                MyF.Szöveg_Tisztítás(KövV.Text.Trim()),
                                false,
                                kövV2_számláló);

                if (Sorszám.Text.Trim() == "")
                    KézKMAdatok.Rögzítés(ADAT);
                else
                    KézKMAdatok.Módosítás(ADAT);

                MessageBox.Show("Az adatok rögzítése megtörtént. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Kiirjaatörténelmet();
                Fülek.SelectedIndex = 2;
            }
            catch (HibásBevittAdat ex)
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
        /// SAP-s adatokat Excel tábla segítségével betölti
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
                Holtart.Visible = true;

                // Wrap the void method in a Task.Run to make it awaitable
                await Task.Run(() => SAP_Adatokbeolvasása.Km_beolvasó(_fájlexc, "SGP"));

                //leállítjuk a számlálót és kikapcsoljuk a holtartot.
                timer1.Enabled = false;
                Holtart.Visible = false;
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

        /// <summary>
        /// Törli a kiválasztott rekordot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (long.TryParse(Sorszám.Text.Trim(), out long IDsorszám))
                {
                    if (MessageBox.Show("Valóban töröljük az adatsort?", "Biztonsági kérdés", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        KézKMAdatok.Törlés(IDsorszám);
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


        #region Kimutatások
        /// <summary>
        /// Kiválasztott pályaszámok listáját tölti fel a pszjelölőbe
        /// </summary>
        private void Pszlista()
        {
            try
            {
                PszJelölő.Items.Clear();
                List<Adat_Jármű> Adatok = KézJármű.Lista_Adatok("Főmérnökség");
                Adatok = (from a in Adatok
                          where a.Törölt == false
                          && a.Valóstípus.Contains("SGP")
                          orderby a.Azonosító
                          select a).ToList();

                foreach (Adat_Jármű elem in Adatok)
                    PszJelölő.Items.Add(elem.Azonosító);

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

        /// <summary>
        /// Havi km 0-ra állítása
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Option5_Click(object sender, EventArgs e)
        {
            // Kocsi havi km
            Text1.Text = "0";
        }

        /// <summary>
        /// A típus átlagát számolja ki a táblázat adatai alapján
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Option7_Click(object sender, EventArgs e)
        {
            try
            { // típusátlag
              // kilistázzuk a adatbázis adatait

                double típusátlag = 0d;
                int i = 0;
                FőHoltart.Maximum = PszJelölő.Items.Count + 1;
                FőHoltart.Visible = true;

                AdatokKm = KézKMAdatok.Lista_Adatok();

                for (int j = 0; j < PszJelölő.Items.Count; j++)
                {
                    FőHoltart.Value = j + 1;


                    Adat_T5C5_Kmadatok AdatFogas = (from a in AdatokKm
                                                    where a.Azonosító == PszJelölő.Items[j].ToStrTrim()
                                                    orderby a.Vizsgdátumk descending
                                                    select a).FirstOrDefault();

                    if (AdatFogas != null)
                    {
                        típusátlag += AdatFogas.Havikm; ;
                        i += 1;
                    }
                }
                FőHoltart.Visible = false;
                if (i != 0) típusátlag /= i;
                Text1.Text = ((long)Math.Round(típusátlag)).ToString();
            }
            catch (HibásBevittAdat ex)
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
        /// Kiválasztott pályaszámok átlagát számolja ki a táblázat adatai alapján
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Option9_Click(object sender, EventArgs e)
        {
            try
            {
                // 'kijelöltek átlaga

                double típusátlag = 0d;
                int i = 0;
                FőHoltart.Maximum = PszJelölő.Items.Count + 1;
                FőHoltart.Visible = true;

                AdatokKm = KézKMAdatok.Lista_Adatok();

                for (int j = 0; j < PszJelölő.CheckedItems.Count; j++)
                {
                    FőHoltart.Value = j + 1;
                    Adat_T5C5_Kmadatok AdatFogas = (from a in AdatokKm
                                                    where a.Azonosító == PszJelölő.CheckedItems[j].ToStrTrim()
                                                    orderby a.Vizsgdátumk descending
                                                    select a).FirstOrDefault();

                    if (AdatFogas != null)
                    {
                        típusátlag += AdatFogas.Havikm;
                        i += 1;
                    }
                }
                FőHoltart.Visible = false;
                if (i != 0)
                    típusátlag /= i;

                Text1.Text = ((long)Math.Round(típusátlag)).ToString();
            }
            catch (HibásBevittAdat ex)
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
        /// Ha a mezőben nem egész szám van, akkor visszaírja a 24 számot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Text2_Leave(object sender, EventArgs e)
        {
            if (!int.TryParse(Text2.Text, out int eredmény))
            {
                Text2.Text = "24";
                Hónapok = 24;
            }
            else
            {
                Text2.Text = eredmény.ToString();
                Hónapok = eredmény;
            }
        }

        /// <summary>
        /// Ha a mezőben nem egész szám van, akkor visszaírja a 0 számot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Text1_Leave(object sender, EventArgs e)
        {

            if (!int.TryParse(Text1.Text, out int szám))
            {
                Text1.Text = "";
                Havifutás = 1500;
            }
            else
            {
                Text1.Text = szám.ToString();
                Havifutás = szám;
            }
            Option8.Checked = true;
        }

        /// <summary>
        /// Kijelölést törli a pályaszám listából
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Kijelöléstörlése_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < PszJelölő.Items.Count; i++)
                PszJelölő.SetItemChecked(i, false);
        }

        /// <summary>
        /// Kijelölést minden pályaszámra beállítja
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mindentkijelöl_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < PszJelölő.Items.Count; i++)
                PszJelölő.SetItemChecked(i, true);
        }

        /// <summary>
        /// Adatbázis adatait írja ki excelbe és előre beállított kimutatást is készít hozzá.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Command3_Click(object sender, EventArgs e)
        {
            try
            {
                List<Adat_T5C5_Kmadatok> Adatok = KézKMAdatok.Lista_Adatok().OrderBy(a => a.Azonosító).ToList();
                if (Adatok.Count < 1) return;
                DataTable Tábla = MyF.ToDataTable(Adatok);
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Vizsgálatok tény adatai",
                    FileName = $"Fogas_adatbázis_mentés_{Program.PostásTelephely.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                Holtart.Be();
                MyE.ExcelLétrehozás();


                string munkalap = "Adatok";
                MyE.Munkalap_átnevezés("Munka1", munkalap);

                utolsósor = MyE.Munkalap(Tábla, 1, munkalap);

                Holtart.Lép();


                MyE.Kiir("Év", "v1");
                MyE.Kiir("hó", "w1");
                MyE.Kiir("Vizsgálat rövid", "x1");

                // kiírjuk az évet, hónapot és a 2 betűs vizsgálatot
                MyE.Kiir("=YEAR(RC[-15])", "v2");
                MyE.Kiir("=MONTH(RC[-16])", "w2");
                MyE.Kiir("=LEFT(RC[-18],2)", "x2");
                Holtart.Lép();

                MyE.Képlet_másol(munkalap, "V2:X2", "V3:X" + (utolsósor + 1));
                MyE.Rácsoz("A1:X" + (utolsósor + 1));

                MyE.Oszlopszélesség(munkalap, "A:X");
                Holtart.Lép();

                MyE.Aktív_Cella(munkalap, "A1");
                MyE.NyomtatásiTerület_részletes(munkalap, "A1:X" + (utolsósor + 1), "$1:$1", "", true);
                Holtart.Lép();

                MyE.Új_munkalap("Kimutatás");

                Kimutatás3();
                Holtart.Lép();
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();

                MyE.Megnyitás(fájlexc);
                Holtart.Ki();

                MessageBox.Show("Az Excel tábla elkészült !", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
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
        /// Előtervet készít a lap beállításainak megfelelően
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Command1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Text2.Text, out int Hónap)) return;
                if (PszJelölő.CheckedItems.Count == 0) return;
                AlHoltart.Visible = true;
                FőHoltart.Visible = true;
                FőHoltart.Maximum = 10;
                FőHoltart.Value = 1;
                Alaptábla();
                FőHoltart.Value = 2;
                Egyhónaprögzítése();
                Excel_előtervező();

            }
            catch (HibásBevittAdat ex)
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


                AdatokKm = KézKMAdatok.Lista_Adatok().Where(a => a.Törölt == false).ToList();
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
                        Adat_T5C5_Kmadatok rekord = (from a in AdatokKm
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
                            Adat_Ciklus CiklusElem = (from a in CiklusAdat
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
                            CiklusElem = (from a in CiklusAdat
                                          where a.Típus == rekordhova.Ciklusrend
                                          && a.Sorszám == sorszám + 1
                                          select a).FirstOrDefault();

                            string következőv = "";
                            if (CiklusElem != null)
                                következőv = CiklusElem.Vizsgálatfok;       // ha talált akkor
                            else
                                következőv = "J";   // ha nem talált

                            // az utolsó rögzített adatot megvizsgáljuk, hogy a havi km-et át lépjük -e fokozatot
                            figyelő = ideigKMUkm - ideigvizsgkm + Havifutás;

                            if (Mennyi <= figyelő)
                            {
                                különbözet = ideigKMUkm - ideigvizsgkm + Havifutás - Mennyi;
                                // módosítjuk a határig tartó adatokat
                                ideigKMUkm = ideigKMUkm + Havifutás - különbözet;
                                ideigTeljeskm = ideigTeljeskm + Havifutás - különbözet;
                                id_sorszám += 1;
                                ideigvizsgkm += Mennyi;
                                ideigTeljeskm += Havifutás;
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
                                ideigKMUkm += Havifutás;
                                ideigTeljeskm += Havifutás;
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

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Vizsgálat előtervező",
                    FileName = "V_javítások_előtervezése_Fogas_" + DateTime.Now.ToString("yyyyMMddhhmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                // megnyitjuk
                MyE.ExcelLétrehozás();
                string munkalap = "Tartalom";

                // ****************************************************
                // elkészítjük a lapokat
                // ****************************************************
                MyE.Munkalap_átnevezés("Munka1", munkalap);

                for (int i = 1; i < 5; i++)
                    MyE.Új_munkalap(cím[i]);

                // ****************************************************
                // Elkészítjük a tartalom jegyzéket
                // ****************************************************
                MyE.Munkalap_aktív(munkalap);
                MyE.Kiir("Munkalapfül", "a1");
                MyE.Kiir("Leírás", "b1");

                for (int i = 1; i < 5; i++)
                {
                    MyE.Kiir(cím[i], "A" + (i + 1).ToString());
                    MyE.Link_beillesztés(munkalap, "B" + (i + 1).ToString(), cím[i].Trim());
                    MyE.Kiir(Leírás[i], "B" + (i + 1).ToString());
                }
                MyE.Oszlopszélesség(munkalap, "A:B");

                // ****************************************************
                // Elkészítjük a munkalapokat
                // ****************************************************
                FőHoltart.Maximum = 4;
                FőHoltart.Value = 1;
                Adatoklistázása();
                FőHoltart.Value = 2;
                Kimutatás();
                FőHoltart.Value = 3;
                Kimutatás1();
                FőHoltart.Value = 4;
                Kimutatás2();

                MyE.Munkalap_aktív(munkalap);
                MyE.Aktív_Cella(munkalap, "A1");

                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();
                MyE.Megnyitás(fájlexc);
                AlHoltart.Visible = false;
                FőHoltart.Visible = false;
                MessageBox.Show("A nyomtatvány elkészült !", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
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
                string jelszó = "pocsaierzsi";
                string szöveg = "SELECT * FROM KMtábla  order by azonosító,vizsgdátumv ";

                utolsósor = MyE.Tábla_Író(hely, jelszó, szöveg, 3, munkalap);


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

                MyE.Munkalap_aktív(munkalap);
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

        private void Kimutatás3()
        {
            try
            {
                string munkalap = "Kimutatás";
                MyE.Munkalap_aktív(munkalap);


                string munkalap_adat = "Adatok";
                string balfelső = "A1";
                string jobbalsó = "X" + (utolsósor + 1);
                string kimutatás_Munkalap = munkalap;
                string Kimutatás_cella = "A6";
                string Kimutatás_név = "Kimutatás";

                List<string> összesítNév = new List<string>();
                List<string> Összesít_módja = new List<string>();
                List<string> sorNév = new List<string>();
                List<string> oszlopNév = new List<string>();
                List<string> SzűrőNév = new List<string>();

                összesítNév.Add("azonosító");

                Összesít_módja.Add("xlCount");

                sorNév.Add("Vizsgálat rövid");


                SzűrőNév.Add("Év");
                SzűrőNév.Add("hó");

                oszlopNév.Add("V2végezte");

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