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
using Villamos.V_Ablakok._5_Karbantartás.Fogaskereku;
using Villamos.V_Ablakok._5_Karbantartás.Karbantartás_Közös;
using Villamos.V_MindenEgyéb;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos
{

    public partial class Ablak_Fogaskerekű_Tulajdonságok
    {

        long JelöltSor = -1;
        long TáblaUtolsóSor = -1;
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
            try
            {
                //Ha az első karakter "R" akkor az új jogosultságkiosztást használjuk
                //ha nem akkor a régit használjuk
                if (Program.PostásJogkör.Substring(0, 1) == "R")
                {
                    TelephelyekFeltöltéseÚj();
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
                }
                else
                {
                    Telephelyekfeltöltése();
                    Jogosultságkiosztás();
                }
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

        private void Tulajdonságok_Fogaskerekű_Load(object sender, EventArgs e)
        {
        }

        private void Ablak_Fogaskerekű_Tulajdonságok_FormClosed(object sender, FormClosedEventArgs e)
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
            int melyikelem;

            // ide kell az összes gombot tenni amit szabályozni akarunk false
            // csak Főmérnökségi belépéssel törölhető
            if ((Program.PostásTelephely) == "Főmérnökség")
            {
            }
            else
            {
            }
            melyikelem = 109;
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

        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\Tulajdonság_Fogaskerekű.html";
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
                    case 0:
                        {
                            break;
                        }
                    case 1:
                        {
                            Kiirjaatörténelmet();
                            break;
                        }
                    case 2:
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
                case 0:
                    {
                        break;
                    }

                case 1:
                    {
                        Kiirjaatörténelmet();
                        break;
                    }

                case 2:
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
                if (Tábla_lekérdezés.Rows.Count <= 0) return;
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

                MyX.DataGridViewToXML(fájlexc, Tábla_lekérdezés);
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

        /// <summary>
        /// Az adatbázis teljes tartalmának Excel fájlba mentése.
        /// a Listában tárolt adatokat datatable tesszük
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Teljes_adatbázis_excel_Click(object sender, EventArgs e)
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

                await Task.Run(() => MyX.DataTableToXML(_fájlexc, AdatTábla));

                //leállítjuk a számlálót és kikapcsoljuk a holtartot.
                timer1.Enabled = false;
                Holtart.Be();
                MessageBox.Show("Az Excel tábla elkészült !", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyF.Megnyitás(_fájlexc);
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
            }
            catch (HibásBevittAdat ex)
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
            if (e.RowIndex < 0) return;
            if (!long.TryParse(Tábla1.Rows[e.RowIndex].Cells[0].Value.ToString(), out JelöltSor)) JelöltSor = -1;
        }


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

        Karbantartás_Rögzítés Új_Karbantartás_Rögzítés;
        private void RögzítésAblak()
        {
            Adat_T5C5_Kmadatok adat = (from a in AdatokKm
                                       where a.ID == JelöltSor
                                       select a).FirstOrDefault();
            if (adat == null) return;
            bool Utolsó = JelöltSor == TáblaUtolsóSor;

            Új_Karbantartás_Rögzítés?.Close();

            Új_Karbantartás_Rögzítés = new Karbantartás_Rögzítés("SGP", adat, Utolsó);
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

                Fogaskereku_KimutatasExcel FogasKimutat = new Fogaskereku_KimutatasExcel();
                FogasKimutat.KimutatastKeszit(fájlexc, Tábla);

                MyF.Megnyitás(fájlexc);
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
<<<<<<<< HEAD:V_Ablakok/5_Karbantartás/Fogaskereku/Ablak_Fogaskerekű_Tulajdonságok.cs
               
========

>>>>>>>> master:V_Ablakok/5_Karbantartás/Fogaskerekű/Ablak_Fogaskerekű_Tulajdonságok.cs

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

                Fogaskereku_Javitasok_Elotervezese_Excel EloExcel = new Fogaskereku_Javitasok_Elotervezese_Excel();
                EloExcel.ExceltGeneral(fájlexc);

<<<<<<<< HEAD:V_Ablakok/5_Karbantartás/Fogaskereku/Ablak_Fogaskerekű_Tulajdonságok.cs
                MyE.Megnyitás(fájlexc);
========
                MyF.Megnyitás(fájlexc);
>>>>>>>> master:V_Ablakok/5_Karbantartás/Fogaskerekű/Ablak_Fogaskerekű_Tulajdonságok.cs
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
<<<<<<<< HEAD:V_Ablakok/5_Karbantartás/Fogaskereku/Ablak_Fogaskerekű_Tulajdonságok.cs
       

========


>>>>>>>> master:V_Ablakok/5_Karbantartás/Fogaskerekű/Ablak_Fogaskerekű_Tulajdonságok.cs
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
                await Task.Run(() => SAP_Adatokbeolvasása.Km_beolvasó(_fájlexc, "SGP"));
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

        private void Cmbtelephely_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                Cmbtelephely.Text = Cmbtelephely.Items[Cmbtelephely.SelectedIndex].ToStrTrim();
                if (Cmbtelephely.Text.Trim() == "") return;
                //Ha az első karakter "R" akkor az új jogosultságkiosztást használjuk
                //ha nem akkor a régit használjuk
                if (Program.PostásJogkör.Substring(0, 1) == "R")
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
                else
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
    }
}