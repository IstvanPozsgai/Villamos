using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using static System.IO.File;
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{

    public partial class Ablak_Fogaskerekű_Tulajdonságok
    {
        string _fájlexc, _hely, _jelszó, _szöveg;
        int utolsósor;
        readonly Kezelő_Fogas_km KézKmadatok = new Kezelő_Fogas_km();
        readonly Kezelő_Ciklus KézCiklus = new Kezelő_Ciklus();
        readonly Kezelő_jármű_hiba KézHiba = new Kezelő_jármű_hiba();

        List<Adat_Fogas_Km> AdatokFogas = new List<Adat_Fogas_Km>();
        List<Adat_Ciklus> AdatokCiklus = new List<Adat_Ciklus>();

        public Ablak_Fogaskerekű_Tulajdonságok()
        {
            InitializeComponent();
        }


        private void Tulajdonságok_Fogaskerekű_Load(object sender, EventArgs e)
        {
            Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;

            Telephelyekfeltöltése();
            Pályaszám_feltöltés();

            string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\villamos4Fogas.mdb";
            if (!Exists(hely))
                Adatbázis_Létrehozás.Kmfutástábla(hely);


            Fülek.SelectedIndex = 0;
            Fülekkitöltése();

            Jogosultságkiosztás();

            Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;

            Ciklusrendcombofeltöltés();
            Üzemek_listázása();
        }


        #region Alap
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
            string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\Tulajdonság_Fogaskerekű.html";
            MyE.Megnyitás(hely);
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
                if (Cmbtelephely.Text.Trim() == "")
                    return;
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\villamos.mdb";
                string jelszó = "pozsgaii";
                string szöveg = "";
                // ha nem telephelyeól kérdezzük le akkor minden kocsit kiír
                bool volt = false;

                for (int i = 0; i < Cmbtelephely.Items.Count; i++)
                {
                    if (Cmbtelephely.Items[i].ToString().Trim() == Program.PostásTelephely.Trim())
                        volt = true;
                }

                if (volt == true)
                {
                    szöveg = "Select * FROM Állománytábla WHERE Üzem='" + Cmbtelephely.Text.Trim() + "' AND ";
                    szöveg += " törölt=0 AND valóstípus Like  '%SGP%' ORDER BY azonosító";
                }
                else
                {
                    szöveg = "Select * FROM Állománytábla WHERE  törölt=0 AND valóstípus Like  '%SGP%' ORDER BY azonosító";
                }
                // feltöltjük az összes pályaszámot a Comboba

                Pályaszám.BeginUpdate();
                Pályaszám.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "azonosító"));
                Pályaszám.EndUpdate();
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
                if (Pályaszám.Text.Trim() == "")
                    return;

                switch (Fülek.SelectedIndex)
                {
                    case 1:
                        {

                            KiirjaafüleketKM();
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


        #region egyéb
        private void Excel_gomb_Click(object sender, EventArgs e)
        {
            try
            {
                Táblázatlistázás();

                if (Tábla_lekérdezés.Rows.Count <= 0)
                    return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Állománytábla_" + Program.PostásTelephely.Trim() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = MyF.Szöveg_Tisztítás(fájlexc, 0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Tábla_lekérdezés, false);
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


        private void Táblázatlistázás()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";
                if (!Exists(hely))
                    return;
                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM állománytábla  order by típus, azonosító";

                Tábla_lekérdezés.Rows.Clear();
                Tábla_lekérdezés.Columns.Clear();
                Tábla_lekérdezés.Refresh();
                Tábla_lekérdezés.Visible = false;
                Tábla_lekérdezés.ColumnCount = 2;

                // fejléc elkészítése 
                Tábla_lekérdezés.Columns[0].HeaderText = "Pályaszám";
                Tábla_lekérdezés.Columns[0].Width = 120;
                Tábla_lekérdezés.Columns[1].HeaderText = "Típus";
                Tábla_lekérdezés.Columns[1].Width = 150;

                Kezelő_Jármű Kéz = new Kezelő_Jármű();
                List<Adat_Jármű> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                int i;
                foreach (Adat_Jármű rekord in Adatok)
                {
                    Tábla_lekérdezés.RowCount++;
                    i = Tábla_lekérdezés.RowCount - 1;

                    Tábla_lekérdezés.Rows[i].Cells[0].Value = rekord.Azonosító.Trim();
                    Tábla_lekérdezés.Rows[i].Cells[1].Value = rekord.Típus.Trim();
                }

                Tábla_lekérdezés.Visible = true;
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


        private void Ciklusrendcombofeltöltés()
        {
            CiklusrendCombo.Items.Clear();
            string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\ciklus.mdb";
            string jelszó = "pocsaierzsi";

            string szöveg = "SELECT DISTINCT típus FROM ciklusrendtábla WHERE  [törölt]='0' ORDER BY típus";

            CiklusrendCombo.BeginUpdate();
            CiklusrendCombo.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "típus"));
            CiklusrendCombo.EndUpdate();
            CiklusrendCombo.Refresh();
        }


        private void Üzemek_listázása()
        {
            Üzemek.Items.Clear();
            string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő.mdb";
            string jelszó = "Mocó";
            string szöveg = "SELECT * FROM telephelytábla order by sorszám";

            Üzemek.BeginUpdate();
            Üzemek.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "telephelykönyvtár"));
            Üzemek.EndUpdate();
            Üzemek.Refresh();
        }


        private void Pályaszám_SelectedIndexChanged(object sender, EventArgs e)
        {
            Frissít();
        }
        #endregion


        #region Lekérdezés lapfül
        private void Lekérdezés_lekérdezés_Click(object sender, EventArgs e)
        {
            try
            {
                // kilistázzuk a adatbázis adatait
                string honnan = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";
                string jelszóhonnan = "pozsgaii";
                string jelszó = "pocsaierzsi";
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\villamos4Fogas.mdb";

                List<Adat_Jármű_hiba> AdatokHiba = KézHiba.Lista_Adatok(Cmbtelephely.Text.Trim());

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

                string szöveg = "SELECT * FROM Állománytábla where [törölt]= false order by azonosító ";

                Kezelő_Jármű Kéz = new Kezelő_Jármű();
                List<Adat_Jármű> Adatok = Kéz.Lista_Adatok(honnan, jelszóhonnan, szöveg);

                Kezelő_T5C5_Kmadatok KézÁ = new Kezelő_T5C5_Kmadatok();
                Adat_T5C5_Kmadatok rekord;

                int i;
                foreach (Adat_Jármű rekordhonnan in Adatok)
                {
                    szöveg = $"SELECT * FROM KMtábla where [azonosító]='{rekordhonnan.Azonosító.Trim()}' AND törölt=false  order by vizsgdátumk desc";
                    rekord = KézÁ.Egy_Adat(hely, jelszó, szöveg);

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
                    FileName = "SGP_futásadatok_" + Program.PostásTelephely + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
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


        private void Teljes_adatbázis_excel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    // kimeneti fájl helye és neve
                    InitialDirectory = "MyDocuments",

                    Title = "Adatbázis mentése Excel fájlba",
                    FileName = "SGP_adatbázis_mentés_" + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    _fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                _hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\villamos4Fogas.mdb";
                _jelszó = "pocsaierzsi";
                _szöveg = "SELECT * FROM kmtábla ORDER BY azonosító,vizsgdátumk";
                Holtart_Be();
                timer1.Enabled = true;
                SZál_ABadatbázis(() =>
                { //leállítjuk a számlálót és kikapcsoljuk a holtartot.
                    timer1.Enabled = false;
                    Holtart.Visible = false;
                    MessageBox.Show("Az Excel tábla elkészült !", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MyE.Megnyitás(_fájlexc);
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


        private void SZál_ABadatbázis(Action callback)
        {
            Thread proc = new Thread(() =>
            {
                // elkészítjük a formanyomtatványt változókat nem lehet küldeni definiálni kell egy külső változót
                MyE.EXCELtábla(_hely, _jelszó, _szöveg, _fájlexc);

                this.Invoke(callback, new object[] { });
            });
            proc.Start();
        }


        private void Timer1_Tick(object sender, EventArgs e)
        {
            Holtart.Value++;
            if (Holtart.Maximum <= Holtart.Value) Holtart.Value = 1;
        }

        #endregion


        #region Táblázat

        private void Kiirjaatörténelmet()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\főmérnökség\adatok\villamos4Fogas.mdb";
                if (!Exists(hely))
                    return;
                string jelszó = "pocsaierzsi";
                string szöveg = $"SELECT * FROM KMtábla where [azonosító]='{Pályaszám.Text.Trim()}' AND törölt=false order by vizsgdátumk ";

                Tábla1.Rows.Clear();
                Tábla1.Columns.Clear();
                Tábla1.Refresh();
                Tábla1.Visible = false;
                Tábla1.ColumnCount = 16;

                // fejléc elkészítése
                Tábla1.Columns[0].HeaderText = "Psz";
                Tábla1.Columns[0].Width = 80;
                Tábla1.Columns[1].HeaderText = "Vizsg. foka";
                Tábla1.Columns[1].Width = 100;
                Tábla1.Columns[2].HeaderText = "Vizsg. Ssz.";
                Tábla1.Columns[2].Width = 100;
                Tábla1.Columns[3].HeaderText = "Vizsg. Kezdete";
                Tábla1.Columns[3].Width = 110;
                Tábla1.Columns[4].HeaderText = "Vizsg. Vége";
                Tábla1.Columns[4].Width = 110;
                Tábla1.Columns[5].HeaderText = "Vizsg KM állás";
                Tábla1.Columns[5].Width = 120;
                Tábla1.Columns[6].HeaderText = "Frissítés Dátum";
                Tábla1.Columns[6].Width = 110;
                Tábla1.Columns[7].HeaderText = "KM J-óta";
                Tábla1.Columns[7].Width = 100;
                Tábla1.Columns[8].HeaderText = "V után futott";
                Tábla1.Columns[8].Width = 100;
                Tábla1.Columns[9].HeaderText = "Havi km";
                Tábla1.Columns[9].Width = 100;
                Tábla1.Columns[10].HeaderText = "Felújítás szám";
                Tábla1.Columns[10].Width = 110;
                Tábla1.Columns[11].HeaderText = "Felújítás Dátum";
                Tábla1.Columns[11].Width = 110;
                Tábla1.Columns[12].HeaderText = "Ciklusrend típus";
                Tábla1.Columns[12].Width = 110;
                Tábla1.Columns[13].HeaderText = "Üzembehelyezés km";
                Tábla1.Columns[13].Width = 100;
                Tábla1.Columns[14].HeaderText = "Végezte";
                Tábla1.Columns[14].Width = 100;
                Tábla1.Columns[15].HeaderText = "ID";
                Tábla1.Columns[15].Width = 100;

                Kezelő_T5C5_Kmadatok Kéz = new Kezelő_T5C5_Kmadatok();
                List<Adat_T5C5_Kmadatok> Adatok = Kéz.Lista_Adat(hely, jelszó, szöveg);

                foreach (Adat_T5C5_Kmadatok rekord in Adatok)
                {

                    Tábla1.RowCount++;
                    int i = Tábla1.RowCount - 1;
                    Tábla1.Rows[i].Cells[0].Value = rekord.Azonosító.Trim();
                    Tábla1.Rows[i].Cells[1].Value = rekord.Vizsgfok.Trim();
                    Tábla1.Rows[i].Cells[2].Value = rekord.Vizsgsorszám;
                    Tábla1.Rows[i].Cells[3].Value = rekord.Vizsgdátumk.ToString("yyyy.MM.dd");
                    Tábla1.Rows[i].Cells[4].Value = rekord.Vizsgdátumv.ToString("yyyy.MM.dd");
                    Tábla1.Rows[i].Cells[5].Value = rekord.Vizsgkm;
                    Tábla1.Rows[i].Cells[6].Value = rekord.KMUdátum.ToString("yyyy.MM.dd");
                    Tábla1.Rows[i].Cells[7].Value = rekord.KMUkm;
                    if (rekord.Vizsgsorszám == 0)
                        Tábla1.Rows[i].Cells[8].Value = rekord.KMUkm;
                    else
                        Tábla1.Rows[i].Cells[8].Value = rekord.KMUkm - rekord.Vizsgkm;

                    Tábla1.Rows[i].Cells[9].Value = rekord.Havikm;
                    Tábla1.Rows[i].Cells[10].Value = rekord.Jjavszám;
                    Tábla1.Rows[i].Cells[11].Value = rekord.Fudátum.ToString("yyyy.MM.dd");
                    Tábla1.Rows[i].Cells[12].Value = rekord.Ciklusrend.Trim();
                    Tábla1.Rows[i].Cells[13].Value = rekord.Teljeskm;
                    Tábla1.Rows[i].Cells[14].Value = rekord.V2végezte;
                    Tábla1.Rows[i].Cells[15].Value = rekord.ID;
                }

                Tábla1.Refresh();
                Tábla1.Visible = true;

            }
            catch (HibásBevittAdat ex)
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
            Mezők_ürítése();

            {
                // kiirjuk a másik fülre a kiválasztott adatokat.

                Vizsgfok_új.Text = Tábla1.Rows[e.RowIndex].Cells[1].Value.ToString();
                Vizsg_sorszám_combo.Text = Tábla1.Rows[e.RowIndex].Cells[2].Value.ToString();
                Vizsgdátumk.Value = DateTime.Parse(Tábla1.Rows[e.RowIndex].Cells[3].Value.ToString());
                Vizsgdátumv.Value = DateTime.Parse(Tábla1.Rows[e.RowIndex].Cells[4].Value.ToString());
                VizsgKm.Text = Tábla1.Rows[e.RowIndex].Cells[5].Value.ToString();
                KMUdátum.Value = DateTime.Parse(Tábla1.Rows[e.RowIndex].Cells[6].Value.ToString());
                KMUkm.Text = Tábla1.Rows[e.RowIndex].Cells[7].Value.ToString();
                HaviKm.Text = Tábla1.Rows[e.RowIndex].Cells[9].Value.ToString();
                Jjavszám.Text = Tábla1.Rows[e.RowIndex].Cells[10].Value.ToString();
                Utolsófelújításdátuma.Value = DateTime.Parse(Tábla1.Rows[e.RowIndex].Cells[11].Value.ToString());
                TEljesKmText.Text = Tábla1.Rows[e.RowIndex].Cells[13].Value.ToString();
                CiklusrendCombo.Text = Tábla1.Rows[e.RowIndex].Cells[12].Value.ToString();
                Üzemek.Text = Tábla1.Rows[e.RowIndex].Cells[14].Value.ToString();
                Sorszám.Text = Tábla1.Rows[e.RowIndex].Cells[15].Value.ToString();
            }
            Vizsgfokcombofeltölés();
            Fülek.SelectedIndex = 1;
        }


        private void Mezők_ürítése()
        {
            Vizsgfok_új.Text = "";
            Vizsg_sorszám_combo.Text = "";
            Vizsgdátumk.Value = DateTime.Today;
            Vizsgdátumv.Value = DateTime.Today;
            VizsgKm.Text = "";
            KMUdátum.Value = DateTime.Today;
            KMUkm.Text = "";
            HaviKm.Text = "";
            Jjavszám.Text = "";
            Utolsófelújításdátuma.Value = DateTime.Today;
            TEljesKmText.Text = "";
            CiklusrendCombo.Text = "";
            Üzemek.Text = "";
            Sorszám.Text = "";
        }


        private void Vizsgfokcombofeltölés()
        {
            string ideig = Vizsg_sorszám_combo.Text;
            string jelszó = "pocsaierzsi";
            string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\ciklus.mdb";
            string szöveg = $"SELECT * FROM ciklusrendtábla where [típus]='{CiklusrendCombo.Text.Trim()}' AND [törölt]='0'  order by sorszám";
            Vizsg_sorszám_combo.Items.Clear();
            Vizsg_sorszám_combo.BeginUpdate();
            Vizsg_sorszám_combo.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "sorszám"));
            Vizsg_sorszám_combo.EndUpdate();
            Vizsg_sorszám_combo.Refresh();
            Vizsg_sorszám_combo.Text = ideig;
        }


        #endregion


        #region Rögzítés lap


        private void KiirjaafüleketKM()
        {
            try
            {
                Mezők_ürítése();

                string hely = $@"{Application.StartupPath}\főmérnökség\adatok\villamos4Fogas.mdb";
                if (!Exists(hely))
                    return;
                string jelszó = "pocsaierzsi";

                if (Pályaszám.Text.Trim() == "")
                    return;

                string szöveg = $"SELECT * FROM KMtábla where [azonosító]='{Pályaszám.Text.Trim()}' AND Törölt=false order by vizsgdátumk desc";

                Kezelő_T5C5_Kmadatok Kéz = new Kezelő_T5C5_Kmadatok();
                Adat_T5C5_Kmadatok rekord = Kéz.Egy_Adat(hely, jelszó, szöveg);

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


        private void CiklusrendCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Vizsgfokcombofeltölés();
        }


        private void Új_adat_Click(object sender, EventArgs e)
        {
            Mezők_ürítése();
        }


        private void Következő_V_Click(object sender, EventArgs e)
        {
            if (Vizsg_sorszám_combo.Text.Trim() == "") return;
            Sorszám.Text = "";
            Vizsg_sorszám_combo.Text = (int.Parse(Vizsg_sorszám_combo.Text) + 1).ToString();
            Vizsgdátumk.Value = DateTime.Today;
            Vizsgdátumv.Value = DateTime.Today;
            VizsgKm.Text = KMUkm.Text;
        }

        private void Vizsg_sorszám_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Vizsgfok_új.Text = "";
                if (Vizsg_sorszám_combo.Text.Trim() == "") return;
                if (CiklusrendCombo.Text.Trim() == "") return;
                CiklusListázás();

                long.TryParse(Vizsg_sorszám_combo.Text.Trim(), out long vSorsz);

                Adat_Ciklus AdatCiklus = (from a in AdatokCiklus
                                          where a.Típus == CiklusrendCombo.Text.Trim()
                                          && a.Sorszám == vSorsz
                                          && a.Törölt == "0"
                                          select a).FirstOrDefault();

                if (AdatCiklus != null)
                    Vizsgfok_új.Text = AdatCiklus.Vizsgálatfok.Trim();
            }
            catch (HibásBevittAdat ex)
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

                if (VizsgKm.Text.Trim() == "") throw new HibásBevittAdat("Vizsgálat km számláló állása mező nem lehet üres");
                if (!int.TryParse(VizsgKm.Text, out int vizsgKm)) throw new HibásBevittAdat("Vizsgálat km számláló állása mezőnek egész számnak kell lennie.");
                if (Vizsgfok_új.Text.Trim() == "") throw new HibásBevittAdat("Vizsgálat fok mező nem lehet üres");
                if (Vizsg_sorszám_combo.Text.Trim() == "") throw new HibásBevittAdat("Vizsgálat sorszáma mező nem lehet üres.");
                if (!int.TryParse(Vizsg_sorszám_combo.Text, out int sorszám)) throw new HibásBevittAdat("Vizsgálat sorszáma mezőnek egész számnak kell lennie.");
                if (KMUkm.Text.Trim() == "") KMUkm.Text = "0";
                if (!int.TryParse(KMUkm.Text, out int kmukm)) throw new HibásBevittAdat("Utolsó felújítás óta futott km mezőnek egész számnak kell lennie.");
                if (HaviKm.Text.Trim() == "") HaviKm.Text = 0.ToString();
                if (!int.TryParse(HaviKm.Text, out int haviKm)) throw new HibásBevittAdat("Havi futásteljesítmény mezőnek egész számnak kell lennie.");
                if (Jjavszám.Text.Trim() == "") Jjavszám.Text = "0";
                if (!int.TryParse(Jjavszám.Text, out int jjavszám)) throw new HibásBevittAdat("Felújítás sorszáma mezőnek egész számnak kell lennie.");
                if (TEljesKmText.Text.Trim() == "") TEljesKmText.Text = 0.ToString();
                if (!int.TryParse(TEljesKmText.Text, out int tEljesKmText)) throw new HibásBevittAdat("Üzembehelyezés óta futott mezőnek egész számnak kell lennie.");
                if (CiklusrendCombo.Text.Trim() == "") throw new HibásBevittAdat("Ütemezés típusa mező nem lehet üres.");

                string hely = $@"{Application.StartupPath}\főmérnökség\adatok\villamos4Fogas.mdb";
                if (!Exists(hely)) return;
                string jelszó = "pocsaierzsi";



                string szöveg = "";
                int i = 0;
                if (Sorszám.Text.Trim() == "")
                {
                    szöveg = "SELECT * FROM kmtábla order by id desc ";

                    //Új

                    FogasListázás();

                    Adat_Fogas_Km AdatFogas = (from a in AdatokFogas
                                               orderby a.ID descending
                                               select a).FirstOrDefault();

                    i = int.Parse(AdatFogas.ID.ToString()) + 1;
                    // Új adat
                    szöveg = "INSERT INTO kmtábla  (ID, azonosító, jjavszám, KMUkm, KMUdátum, ";
                    szöveg += " vizsgfok,  vizsgdátumk, vizsgdátumv,";
                    szöveg += " vizsgkm, havikm, vizsgsorszám, fudátum, ";
                    szöveg += " Teljeskm, Ciklusrend, V2végezte, KövV2_Sorszám, KövV2, ";
                    szöveg += " KövV_Sorszám, KövV, V2V3Számláló, törölt) VALUES (";
                    szöveg += i.ToString() + ", '" + Pályaszám.Text.Trim() + "', " + Jjavszám.Text.Trim() + ", " + KMUkm.Text.Trim() + ", '" + KMUdátum.Value.ToString("yyyy.MM.dd") + "', ";
                    szöveg += "'" + Vizsgfok_új.Text.Trim() + "', '" + Vizsgdátumk.Value.ToString("yyyy.MM.dd") + "', '" + Vizsgdátumv.Value.ToString("yyyy.MM.dd") + "', ";
                    szöveg += VizsgKm.Text.Trim() + ", " + HaviKm.Text.Trim() + ", " + Vizsg_sorszám_combo.Text.Trim() + ", '" + Utolsófelújításdátuma.Value.ToString("yyyy.MM.dd") + "', ";
                    szöveg += TEljesKmText.Text.Trim() + ", '" + CiklusrendCombo.Text.Trim() + "', '" + Üzemek.Text.Trim() + "', 0, '_', 0, '_', 0, false )";
                }

                else
                {
                    // módosítjuk az adatokat
                    szöveg = " UPDATE kmtábla SET ";
                    szöveg += " Jjavszám=" + Jjavszám.Text.Trim() + ", ";
                    szöveg += " KMUkm=" + KMUkm.Text.Trim() + ", ";
                    szöveg += " KMUdátum='" + KMUdátum.Value.ToString("yyyy.MM.dd") + "', ";
                    szöveg += " Vizsgfok='" + Vizsgfok_új.Text.Trim() + "', ";
                    szöveg += " Vizsgdátumk='" + Vizsgdátumk.Value.ToString("yyyy.MM.dd") + "', ";
                    szöveg += " Vizsgdátumv='" + Vizsgdátumv.Value.ToString("yyyy.MM.dd") + "', ";
                    szöveg += " VizsgKm=" + VizsgKm.Text.Trim() + ", ";
                    szöveg += " HaviKm=" + HaviKm.Text.Trim() + ", ";
                    szöveg += " VizsgSorszám=" + Vizsg_sorszám_combo.Text.Trim() + ", ";
                    szöveg += " fudátum='" + Utolsófelújításdátuma.Value.ToString("yyyy.MM.dd") + "', ";
                    szöveg += " Teljeskm=" + TEljesKmText.Text.Trim() + ", ";
                    szöveg += " Ciklusrend='" + CiklusrendCombo.Text.Trim() + "', ";
                    szöveg += " V2végezte='" + Üzemek.Text.Trim() + "', ";
                    szöveg += " törölt=false ";
                    szöveg += " WHERE id=" + Sorszám.Text.Trim();
                }

                MyA.ABMódosítás(hely, jelszó, szöveg);

                // naplózás
                hely = $@"{Application.StartupPath}\Főmérnökség\Napló\2021Kmnapló{DateTime.Today.Year}.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.KmfutástáblaNapló(hely);

                Kezelő_T5C5_Kmadatok_Napló KézT5C5Napló = new Kezelő_T5C5_Kmadatok_Napló();
                szöveg = "SELECT * FROM kmtáblaNapló";
                List<Adat_T5C5_Kmadatok_Napló> AdatokNapló = KézT5C5Napló.Lista_Adat(hely, jelszó, szöveg);
                long id = 1;
                if (AdatokNapló.Count > 0) id = AdatokNapló.Max(a => a.ID) + 1;

                szöveg = "INSERT INTO kmtáblaNapló  (ID, azonosító, jjavszám, KMUkm, KMUdátum, ";
                szöveg += " vizsgfok,  vizsgdátumk, vizsgdátumv,";
                szöveg += " vizsgkm, havikm, vizsgsorszám, fudátum, ";
                szöveg += " Teljeskm, Ciklusrend, V2végezte, KövV2_Sorszám, KövV2, ";
                szöveg += " KövV_Sorszám, KövV, V2V3Számláló, törölt, Módosító, Mikor) VALUES (";
                szöveg += $"{id}, '" + Pályaszám.Text.Trim() + "', " + Jjavszám.Text.Trim() + ", " + KMUkm.Text.Trim() + ", '" + KMUdátum.Value.ToString("yyyy.MM.dd") + "', ";
                szöveg += "'" + Vizsgfok_új.Text.Trim() + "', '" + Vizsgdátumk.Value.ToString("yyyy.MM.dd") + "', '" + Vizsgdátumv.Value.ToString("yyyy.MM.dd") + "', ";
                szöveg += VizsgKm.Text.Trim() + ", " + HaviKm.Text.Trim() + ", " + Vizsg_sorszám_combo.Text.Trim() + ", '" + Utolsófelújításdátuma.Value.ToString("yyyy.MM.dd") + "', ";
                szöveg += TEljesKmText.Text.Trim() + ", '" + CiklusrendCombo.Text.Trim() + "', '" + Üzemek.Text.Trim() + "', 0, '_', ";
                szöveg += "0, '_', 0, false, '" + Program.PostásTelephely + "', '" + DateTime.Now.ToString() + "')";
                MyA.ABMódosítás(hely, jelszó, szöveg);

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


        private void SAP_adatok_Click(object sender, EventArgs e)
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
                _hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\villamos4Fogas.mdb";

                timer1.Enabled = true;
                Holtart.Visible = true;
                SZál_KM_Beolvasás(() =>
                { //leállítjuk a számlálót és kikapcsoljuk a holtartot.
                    timer1.Enabled = false;
                    Holtart.Visible = false;
                    MessageBox.Show("Az adatok beolvasása megtörtént !", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void SZál_KM_Beolvasás(Action callback)
        {
            Thread proc = new Thread(() =>
            {
                //beolvassuk az adatokat
                SAP_Adatokbeolvasása_km.Km_beolvasó(_fájlexc, _hely);
                this.Invoke(callback, new object[] { });
            });
            proc.Start();
        }



        private void Töröl_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\villamos4Fogas.mdb";
                string jelszó = "pocsaierzsi";
                string szöveg;
                if (Sorszám.Text.Trim() != "")
                {
                    if (MessageBox.Show("Valóban töröljük az adatsort?", "Biztonsági kérdés", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        szöveg = " UPDATE kmtábla SET ";
                        szöveg += " törölt=true  ";
                        szöveg += " WHERE id=" + Sorszám.Text.Trim();

                        MyA.ABMódosítás(hely, jelszó, szöveg);

                        // naplózás
                        hely = $@"{Application.StartupPath}\Főmérnökség\Napló\" + "2021Kmnapló" + DateTime.Today.ToString("yyyy") + ".mdb";
                        if (!Exists(hely)) Adatbázis_Létrehozás.KmfutástáblaNapló(hely);

                        Kezelő_T5C5_Kmadatok_Napló KézT5C5Napló = new Kezelő_T5C5_Kmadatok_Napló();
                        szöveg = "SELECT * FROM kmtáblaNapló";
                        List<Adat_T5C5_Kmadatok_Napló> AdatokNapló = KézT5C5Napló.Lista_Adat(hely, jelszó, szöveg);
                        long id = 1;
                        if (AdatokNapló.Count > 0) id = AdatokNapló.Max(a => a.ID) + 1;

                        szöveg = "INSERT INTO kmtáblaNapló  (ID, azonosító, jjavszám, KMUkm, KMUdátum, ";
                        szöveg += " vizsgfok,  vizsgdátumk, vizsgdátumv,";
                        szöveg += " vizsgkm, havikm, vizsgsorszám, fudátum, ";
                        szöveg += " Teljeskm, Ciklusrend, V2végezte, KövV2_Sorszám, KövV2, ";
                        szöveg += " KövV_Sorszám, KövV, V2V3Számláló, törölt, Módosító, Mikor) VALUES (";
                        szöveg += $"{id}, '" + Pályaszám.Text.Trim() + "', " + Jjavszám.Text.Trim() + ", " + KMUkm.Text.Trim() + ", '" + KMUdátum.Value.ToString("yyyy.MM.dd") + "', ";
                        szöveg += "'" + Vizsgfok_új.Text.Trim() + "', '" + Vizsgdátumk.Value.ToString("yyyy.MM.dd") + "', '" + Vizsgdátumv.Value.ToString("yyyy.MM.dd") + "', ";
                        szöveg += VizsgKm.Text.Trim() + ", " + HaviKm.Text.Trim() + ", " + Vizsg_sorszám_combo.Text.Trim() + ", '" + Utolsófelújításdátuma.Value.ToString("yyyy.MM.dd") + "', ";
                        szöveg += TEljesKmText.Text.Trim() + ", '" + CiklusrendCombo.Text.Trim() + "', '" + Üzemek.Text.Trim() + "', 0, '_', ";
                        szöveg += "0, '_',  0, False, '" + Program.PostásTelephely.Trim() + "', '" + DateTime.Now.ToString() + "')";
                        MyA.ABMódosítás(hely, jelszó, szöveg);

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


        private void Pszlista()
        {
            PszJelölő.Items.Clear();
            string hely = $@"{Application.StartupPath}\főmérnökség\adatok\villamos.mdb";
            string jelszó = "pozsgaii";
            string szöveg = "SELECT * FROM Állománytábla where [törölt]= false AND valóstípus Like  '%SGP%'  ORDER BY azonosító ";

            PszJelölő.Items.Clear();
            PszJelölő.BeginUpdate();
            PszJelölő.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "azonosító"));
            PszJelölő.EndUpdate();
            PszJelölő.Refresh();
        }


        private void Option5_Click(object sender, EventArgs e)
        {
            // Kocsi havi km
            Text1.Text = "0";
        }
        private void Option7_Click(object sender, EventArgs e)
        {
            // típusátlag
            // kilistázzuk a adatbázis adatait

            double típusátlag = 0d;
            int i = 0;
            FőHoltart.Maximum = PszJelölő.Items.Count + 1;
            FőHoltart.Visible = true;

            FogasListázás();

            for (int j = 0; j < PszJelölő.Items.Count; j++)
            {
                FőHoltart.Value = j + 1;


                Adat_Fogas_Km AdatFogas = (from a in AdatokFogas
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

        private void Option9_Click(object sender, EventArgs e)
        {
            // 'kijelöltek átlaga

            double típusátlag = 0d;
            int i = 0;
            FőHoltart.Maximum = PszJelölő.Items.Count + 1;
            FőHoltart.Visible = true;

            FogasListázás();

            for (int j = 0; j < PszJelölő.Items.Count; j++)
            {
                FőHoltart.Value = j + 1;
                if (PszJelölő.GetItemChecked(j))
                {

                    Adat_Fogas_Km AdatFogas = (from a in AdatokFogas
                                               where a.Azonosító == PszJelölő.Items[j].ToStrTrim()
                                               orderby a.Vizsgdátumk descending
                                               select a).FirstOrDefault();

                    if (AdatFogas != null)
                    {
                        típusátlag += AdatFogas.Havikm;
                        i += 1;
                    }
                }
            }
            FőHoltart.Visible = false;
            if (i != 0)
                típusátlag /= i;

            Text1.Text = ((long)Math.Round(típusátlag)).ToString();
        }



        private void Text2_Leave(object sender, EventArgs e)
        {
            if (!int.TryParse(Text2.Text, out int eredmény))
                Text2.Text = "24";
            else
                Text2.Text = eredmény.ToString();
        }


        private void Text1_Leave(object sender, EventArgs e)
        {

            if (!int.TryParse(Text1.Text, out int szám))
                Text1.Text = "";
            else
                Text1.Text = szám.ToString();

            HaviKm.Text = Text1.Text;
            Option8.Checked = true;
        }


        private void Kijelöléstörlése_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < PszJelölő.Items.Count; i++)
                PszJelölő.SetItemChecked(i, false);
        }


        private void Mindentkijelöl_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < PszJelölő.Items.Count; i++)
                PszJelölő.SetItemChecked(i, true);
        }


        private void Command3_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\villamos4Fogas.mdb";
                string jelszó = "pocsaierzsi";
                string szöveg = "SELECT * FROM KMtábla order by azonosító";
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Vizsgálatok tény adatai",
                    FileName = "Fogas_adatbázis_mentés_" + Program.PostásTelephely.Trim() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;


                Holtart_Be();

                MyE.ExcelLétrehozás();


                string munkalap = "Adatok";
                MyE.Munkalap_átnevezés("Munka1", munkalap);

                utolsósor = MyE.Tábla_Író(hely, jelszó, szöveg, 1, munkalap);

                Holtart_Lép();


                MyE.Kiir("Év", "v1");
                MyE.Kiir("hó", "w1");
                MyE.Kiir("Vizsgálat rövid", "x1");

                // kiírjuk az évet, hónapot és a 2 betűs vizsgálatot
                MyE.Kiir("=YEAR(RC[-15])", "v2");
                MyE.Kiir("=MONTH(RC[-16])", "w2");
                MyE.Kiir("=LEFT(RC[-18],2)", "x2");
                Holtart_Lép();

                MyE.Képlet_másol(munkalap, "V2:X2", "V3:X" + (utolsósor + 1));
                MyE.Rácsoz("A1:X" + (utolsósor + 1));

                MyE.Oszlopszélesség(munkalap, "A:X");
                Holtart_Lép();

                MyE.Aktív_Cella(munkalap, "A1");
                MyE.NyomtatásiTerület_részletes(munkalap, "A1:X" + (utolsósor + 1), "$1:$1", "", true);
                Holtart_Lép();

                MyE.Új_munkalap("Kimutatás");

                Kimutatás3();
                Holtart_Lép();
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();

                MyE.Megnyitás(fájlexc);
                Holtart_Ki();

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


        private void Command1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Text2.Text, out int Hónap))
                    return;
                int volt = 0;
                for (int j = 0; j < PszJelölő.Items.Count; j++)
                {
                    if (PszJelölő.GetItemChecked(j) == true)
                    {
                        volt = 1;
                        break;
                    }
                }
                if (volt == 0)
                {
                    return;
                }

                AlHoltart.Visible = true;
                FőHoltart.Visible = true;
                FőHoltart.Maximum = 10;
                FőHoltart.Value = 1;
                Alaptábla();
                FőHoltart.Value = 2;
                Egyhónaprögzítése();
                Excel_előtervező();
                AlHoltart.Visible = false;
                FőHoltart.Visible = false;
            }
            catch (HibásBevittAdat ex)
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
                if (Check1.Checked) return;
                string hova = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Kmadatok.mdb";

                if (Exists(hova) && !Check1.Checked) Delete(hova);
                if (!Exists(hova)) Adatbázis_Létrehozás.Előtervkmfutástábla(hova);

                string helykerék = $@"{Application.StartupPath}\Főmérnökség\adatok\" + DateTime.Today.Year + @"\telepikerék.mdb";
                string helykerékelőző = $@"{Application.StartupPath}\Főmérnökség\adatok\" + DateTime.Today.AddYears(-1).Year + @"\telepikerék.mdb";
                string jelszókerék = "szabólászló";
                double kerékminimum;
                double Kerék_K11;
                double Kerék_K12;
                double Kerék_K21;
                double Kerék_K22;
                string szövegkerék = "SELECT * FROM keréktábla";

                //Új

                Kezelő_Kerék_Mérés KézKerékM = new Kezelő_Kerék_Mérés();
                List<Adat_Kerék_Mérés> AdatokKerékM = KézKerékM.Lista_Adatok(helykerék, jelszókerék, szövegkerék);
                List<Adat_Kerék_Mérés> AdatokKerékM_Előző = KézKerékM.Lista_Adatok(helykerékelőző, jelszókerék, szövegkerék);
                AdatokKerékM.AddRange(AdatokKerékM_Előző);

                // kilistázzuk a adatbázis adatait
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Villamos4Fogas.mdb";
                string jelszó = "pocsaierzsi";
                string honnan = $@"{Application.StartupPath}\Főmérnökség\adatok\villamos.mdb";
                string jelszóhonnan = "pozsgaii";
                string szöveg0 = "SELECT * FROM Állománytábla";


                // adatbázis
                Kezelő_T5C5_Kmadatok Kéz = new Kezelő_T5C5_Kmadatok();
                Adat_T5C5_Kmadatok rekord;

                Kezelő_Jármű KézJármű = new Kezelő_Jármű();

                List<Adat_Jármű> AdatokJármű = KézJármű.Lista_Adatok(honnan, jelszóhonnan, szöveg0);


                string szöveg;
                AlHoltart.Maximum = PszJelölő.Items.Count + 1;
                int i = 1;

                List<string> SzövegGy = new List<string>();
                for (int j = 0, loopTo = PszJelölő.Items.Count - 1; j <= loopTo; j++)
                {
                    if (PszJelölő.GetItemChecked(j) == true)
                    {

                        Adat_Jármű AdatJármű = (from a in AdatokJármű
                                                where a.Törölt == false
                                                && a.Azonosító == PszJelölő.Items[j].ToStrTrim()
                                                select a).FirstOrDefault();

                        szöveg = "SELECT * FROM KMtábla where [azonosító]='" + PszJelölő.Items[j].ToString().Trim() + "'";
                        szöveg += " order by vizsgdátumk desc ";

                        rekord = Kéz.Egy_Adat(hely, jelszó, szöveg);

                        if (rekord != null)
                        {
                            // Új adat
                            szöveg = "INSERT INTO kmtábla  (ID, azonosító, jjavszám, KMUkm, KMUdátum, ";
                            szöveg += " vizsgfok,  vizsgdátumk, vizsgdátumv,";
                            szöveg += " vizsgkm, havikm, vizsgsorszám, fudátum, ";
                            szöveg += " Teljeskm, Ciklusrend, V2végezte, KövV2_Sorszám, KövV2, ";
                            szöveg += " KövV_Sorszám, KövV, V2V3Számláló, törölt, Honostelephely, tervsorszám, Kerék_K11, Kerék_K12, Kerék_K21, Kerék_K22, Kerék_min)";
                            szöveg += " VALUES (";
                            szöveg += i.ToString() + ", ";                                               // id
                            szöveg += "'" + rekord.Azonosító + "', ";                            // azonosító
                            szöveg += rekord.Jjavszám + ", ";                                   // jjavszám
                            szöveg += rekord.KMUkm + ", ";                                     // KMUkm
                            szöveg += "'" + rekord.KMUdátum.ToString("yyyy.MM.dd") + "', ";                 // KMUdátum
                            szöveg += "'" + rekord.Vizsgfok + "', ";                            // vizsgfok
                            szöveg += "'" + rekord.Vizsgdátumk.ToString("yyyy.MM.dd") + "', ";             // vizsgdátumk
                            szöveg += "'" + rekord.Vizsgdátumv.ToString("yyyy.MM.dd") + "', ";              // vizsgdátumv
                            szöveg += rekord.Vizsgkm + ", ";                                     // vizsgkm
                            szöveg += rekord.Havikm + ", ";                                     // havikm
                            szöveg += rekord.Vizsgsorszám + ", ";                              // vizsgsorszám
                            szöveg += "'" + rekord.Fudátum.ToString("yyyy.MM.dd") + "', ";    // fudátum
                            szöveg += rekord.Teljeskm + ", ";                               // Teljeskm
                            szöveg += "'" + rekord.Ciklusrend.Trim() + "', ";                          // Ciklusrend
                            szöveg += "'" + rekord.V2végezte.Trim() + "', ";                                    // V2végezte
                            szöveg += " 0, ";                             // KövV2_Sorszám
                            szöveg += " '_', ";                                     // KövV2
                            szöveg += " 0, ";                               // KövV_Sorszám
                            szöveg += " '_', ";                                      // KövV
                            szöveg += "0, ";                                // V2V3Számláló
                            szöveg += " false, ";                                                   // törölt
                            szöveg += "'" + AdatJármű.Üzem.Trim() + "', "; // Honostelephely
                            szöveg += "0, ";    // tervsorszám

                            Kerék_K11 = 0d;
                            Kerék_K12 = 0d;
                            Kerék_K21 = 0d;
                            Kerék_K22 = 0d;
                            kerékminimum = 1000d;
                            //  kerék méretek
                            if (AdatokKerékM != null)
                            {
                                Adat_Kerék_Mérés AdatKerékMérés = (from a in AdatokKerékM
                                                                   where a.Azonosító == rekord.Azonosító.Trim()
                                                                   && a.Pozíció == "K11"
                                                                   orderby a.Mikor descending
                                                                   select a).FirstOrDefault();

                                if (AdatKerékMérés != null) Kerék_K11 = AdatKerékMérés.Méret;

                                AdatKerékMérés = (from a in AdatokKerékM
                                                  where a.Azonosító == rekord.Azonosító.Trim()
                                                  && a.Pozíció == "K12"
                                                  orderby a.Mikor descending
                                                  select a).FirstOrDefault();
                                if (AdatKerékMérés != null) Kerék_K12 = AdatKerékMérés.Méret;

                                AdatKerékMérés = (from a in AdatokKerékM
                                                  where a.Azonosító == rekord.Azonosító.Trim()
                                                  && a.Pozíció == "K21"
                                                  orderby a.Mikor descending
                                                  select a).FirstOrDefault();
                                if (AdatKerékMérés != null) Kerék_K21 = AdatKerékMérés.Méret;

                                AdatKerékMérés = (from a in AdatokKerékM
                                                  where a.Azonosító == rekord.Azonosító.Trim()
                                                  && a.Pozíció == "K22"
                                                  orderby a.Mikor descending
                                                  select a).FirstOrDefault();
                                if (AdatKerékMérés != null) Kerék_K22 = AdatKerékMérés.Méret;

                                if (kerékminimum > Kerék_K11) kerékminimum = Kerék_K11;
                                if (kerékminimum > Kerék_K12) kerékminimum = Kerék_K12;
                                if (kerékminimum > Kerék_K21) kerékminimum = Kerék_K21;
                                if (kerékminimum > Kerék_K22) kerékminimum = Kerék_K22;

                                szöveg += Kerék_K11.ToString() + ", ";  // Kerék_K11
                                szöveg += Kerék_K12.ToString() + ", "; // Kerék_K12
                                szöveg += Kerék_K21.ToString() + ", "; // Kerék_K21
                                szöveg += Kerék_K22.ToString() + ", "; // Kerék_K22
                                szöveg += kerékminimum.ToString() + " )";  // Kerék_min
                                SzövegGy.Add(szöveg);
                                i += 1;
                            }
                            AlHoltart.Value = j + 1;
                        }
                    }
                    MyA.ABMódosítás(hova, jelszó, SzövegGy);
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


        private void Egyhónaprögzítése()
        {
            try
            {
                if (Text2.Text.Trim() == "") return;
                if (!int.TryParse(Text2.Text, out int hónap)) return;


                string hova = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Kmadatok.mdb";
                if (!Exists(hova)) return;

                string jelszó = "pocsaierzsi";
                var Alsó = default(double);
                var Felső = default(double);
                var Névleges = default(double);
                double Havifutás;
                var Mennyi = default(double);
                var sorszám = default(double);
                double különbözet;

                //string Szöveg1;
                string következőv;

                //Új

                CiklusListázás();

                string ideigazonosító;
                double ideigjjavszám;
                double ideigKMUkm;
                DateTime ideigKMUdátum;
                string ideigvizsgfok;
                DateTime ideigvizsgdátumk;
                DateTime ideigvizsgdátumv;
                double ideigvizsgkm;
                double ideighavikm;
                double ideigvizsgsorszám;
                DateTime ideigfudátum;
                double ideigTeljeskm;
                string ideigCiklusrend;
                string ideigV2végezte;
                string ideigHonostelephely;
                double ideigtervsorszám;
                double ideigkövV2_sorszám;
                string ideigkövV2;
                double ideigkövV_sorszám;
                string ideigKövV;
                bool ideigtörölt;
                double ideigkerék_11;
                double ideigkerék_12;
                double ideigkerék_21;
                double ideigkerék_22;
                double ideigkerék_min;
                double ideigV2V3számláló;
                double id_sorszám;
                DateTime elődátum;
                double figyelő;

                FőHoltart.Maximum = PszJelölő.Items.Count + 3;
                AlHoltart.Maximum = int.Parse(Text2.Text) + 3;
                // beolvassuk a ID sorszámot, majd növeljük minden rögzítésnél

                Kezelő_T5C5_Előterv Kéz = new Kezelő_T5C5_Előterv();
                Adat_T5C5_Előterv rekordhova;

                string szöveg = "SELECT * FROM KMtábla ";
                List<Adat_T5C5_Előterv> Adatok_T5C5 = Kéz.Lista_Adat(hova, jelszó, szöveg);
                rekordhova = (from a in Adatok_T5C5
                              orderby a.ID descending
                              select a).FirstOrDefault();

                id_sorszám = rekordhova.ID;

                for (int j = 0; j < PszJelölő.Items.Count; j++)
                {
                    if (PszJelölő.GetItemChecked(j))
                    {

                        szöveg = $"SELECT * FROM KMtábla where [azonosító]='{PszJelölő.Items[j].ToStrTrim()}' order by vizsgdátumv desc";

                        rekordhova = Kéz.Egy_Adat(hova, jelszó, szöveg);

                        if (rekordhova != null)
                        {

                            // beolvassuk a kocsi alapadatait, hogy tudjuk növelni.
                            ideigazonosító = rekordhova.Azonosító.Trim();
                            ideigjjavszám = rekordhova.Jjavszám;
                            ideigKMUkm = rekordhova.KMUkm;
                            ideigKMUdátum = rekordhova.KMUdátum;
                            ideigvizsgfok = rekordhova.Vizsgfok;
                            ideigvizsgdátumk = rekordhova.Vizsgdátumk;
                            ideigvizsgdátumv = rekordhova.Vizsgdátumv;
                            ideigvizsgkm = rekordhova.Vizsgkm;
                            ideighavikm = rekordhova.Havikm;
                            ideigvizsgsorszám = rekordhova.Vizsgsorszám;
                            ideigfudátum = rekordhova.Fudátum;
                            ideigTeljeskm = rekordhova.Teljeskm;
                            ideigCiklusrend = rekordhova.Ciklusrend;
                            ideigV2végezte = "Előterv";
                            ideigkövV2_sorszám = rekordhova.KövV2_sorszám;
                            ideigkövV2 = rekordhova.KövV2;
                            ideigkövV_sorszám = rekordhova.KövV_sorszám;
                            ideigKövV = rekordhova.KövV;
                            ideigtörölt = rekordhova.Törölt;
                            ideigHonostelephely = rekordhova.Honostelephely;
                            ideigtervsorszám = rekordhova.Tervsorszám;
                            ideigkerék_11 = rekordhova.Kerék_K11;
                            ideigkerék_12 = rekordhova.Kerék_K12;
                            ideigkerék_21 = rekordhova.Kerék_K21;
                            ideigkerék_22 = rekordhova.Kerék_K22;
                            ideigkerék_min = rekordhova.Kerék_min;
                            ideigV2V3számláló = rekordhova.V2V3Számláló;


                            for (int i = 1; i < hónap; i++)
                            {
                                elődátum = DateTime.Today.AddMonths(i);

                                // megnézzük, hogy mi a ciklus határa
                                Adat_Ciklus AdatCiklus = (from a in AdatokCiklus
                                                          where a.Típus == ideigCiklusrend.Trim()
                                                          && a.Sorszám == ideigvizsgsorszám
                                                          select a).FirstOrDefault();

                                if (AdatCiklus != null)
                                {
                                    Alsó = AdatCiklus.Alsóérték;
                                    Felső = AdatCiklus.Felsőérték;
                                    Névleges = AdatCiklus.Névleges;
                                    sorszám = AdatCiklus.Sorszám;
                                }
                                if (Option10.Checked) Mennyi = Alsó;
                                if (Option11.Checked) Mennyi = Névleges;
                                if (Option12.Checked) Mennyi = Felső;

                                // megnézzük a következő V-t
                                AdatCiklus = (from a in AdatokCiklus
                                              where a.Típus == ideigCiklusrend.Trim()
                                              && a.Sorszám == sorszám + 1
                                              select a).FirstOrDefault();

                                if (AdatCiklus != null)
                                    következőv = AdatCiklus.Vizsgálatfok;
                                else
                                    következőv = "J";



                                // az utolsó rögzített adatot megvizsgáljuk, hogy a havi km-et át lépjük -e fokozatot
                                if (!int.TryParse(Text1.Text, out int havilabel))
                                    Havifutás = ideighavikm;
                                else
                                    Havifutás = havilabel;
                                figyelő = ideigKMUkm - ideigvizsgkm + Havifutás;

                                if (Mennyi <= figyelő)
                                {

                                    különbözet = ideigKMUkm - ideigvizsgkm + Havifutás - Mennyi;
                                    // módosítjuk a határig tartó adatokat
                                    ideigKMUkm = ideigKMUkm + Havifutás - különbözet;
                                    ideigTeljeskm = ideigTeljeskm + Havifutás - különbözet;
                                    id_sorszám += 1d;
                                    //ideigvizsgkm = ideigKMUkm + Havifutás - különbözet
                                    ideigvizsgkm += Mennyi;
                                    ideigTeljeskm += Havifutás;
                                    ideigKMUdátum = elődátum;
                                    ideigvizsgfok = következőv;
                                    ideigvizsgdátumk = elődátum;
                                    ideigvizsgdátumv = elődátum;
                                    ideigtervsorszám += 1d;
                                    double kerékcsökkenés = double.TryParse(Kerékcsökkenés.Text, out kerékcsökkenés) ? kerékcsökkenés : 0;
                                    ideigkerék_11 -= kerékcsökkenés;
                                    ideigkerék_12 -= kerékcsökkenés;
                                    ideigkerék_21 -= kerékcsökkenés;
                                    ideigkerék_22 -= kerékcsökkenés;
                                    ideigkerék_min -= kerékcsökkenés;
                                    // rögzítjük és egy ciklussal feljebb emeljük
                                    if (következőv == "J")
                                    {
                                        ideigvizsgsorszám = 0d;
                                        ideigKMUkm = 0d;
                                        ideigfudátum = elődátum;
                                        ideigjjavszám += 1d;
                                        ideigvizsgkm = 0d;
                                    }
                                    else
                                    {
                                        ideigvizsgsorszám += 1;
                                    }
                                    szöveg = "INSERT INTO kmtábla  (ID, azonosító, jjavszám, KMUkm, KMUdátum, ";
                                    szöveg += " vizsgfok,  vizsgdátumk, vizsgdátumv,";
                                    szöveg += " vizsgkm, havikm, vizsgsorszám, fudátum, ";
                                    szöveg += " Teljeskm, Ciklusrend, V2végezte, KövV2_Sorszám, KövV2, ";
                                    szöveg += " KövV_Sorszám, KövV, V2V3Számláló, törölt, Honostelephely, tervsorszám, Kerék_K11, Kerék_K12, Kerék_K21, Kerék_K22, Kerék_min)";
                                    szöveg += " VALUES (";
                                    szöveg += id_sorszám.ToString() + ", ";                                               // id
                                    szöveg += "'" + ideigazonosító.Trim() + "', ";                            // azonosító
                                    szöveg += ideigjjavszám.ToString() + ", ";                                   // jjavszám
                                    szöveg += ideigKMUkm.ToString() + ", ";                                     // KMUkm
                                    szöveg += "'" + ideigKMUdátum.ToString("yyyy.MM.dd") + "', ";                 // KMUdátum
                                    szöveg += "'" + ideigvizsgfok.Trim() + "', ";                            // vizsgfok
                                    szöveg += "'" + ideigvizsgdátumk.ToString("yyyy.MM.dd") + "', ";             // vizsgdátumk
                                    szöveg += "'" + ideigvizsgdátumv.ToString("yyyy.MM.dd") + "', ";              // vizsgdátumv
                                    szöveg += ideigvizsgkm.ToString() + ", ";                                     // vizsgkm
                                    szöveg += ideighavikm.ToString() + ", ";                                     // havikm
                                    szöveg += ideigvizsgsorszám.ToString() + ", ";                              // vizsgsorszám
                                    szöveg += "'" + ideigfudátum.ToString("yyyy.MM.dd") + "', ";    // fudátum
                                    szöveg += ideigTeljeskm.ToString() + ", ";                               // Teljeskm
                                    szöveg += "'" + ideigCiklusrend.Trim() + "', ";                          // Ciklusrend
                                    szöveg += "'" + ideigV2végezte.Trim() + "', ";                                    // V2végezte
                                    szöveg += ideigkövV2_sorszám.ToString() + ", ";                             // KövV2_Sorszám
                                    szöveg += "'" + ideigkövV2.Trim() + "', ";                                     // KövV2
                                    szöveg += ideigkövV_sorszám.ToString() + ", ";                               // KövV_Sorszám
                                    szöveg += "'" + ideigKövV.Trim() + "', ";                                      // KövV
                                    szöveg += ideigV2V3számláló.ToString().Trim() + ", ";                                // V2V3Számláló
                                    szöveg += " false, ";                                                   // törölt
                                    szöveg += "'" + ideigHonostelephely.Trim() + "', "; // Honostelephely
                                    szöveg += ideigtervsorszám.ToString() + ", ";    // tervsorszám
                                    szöveg += ideigkerék_11.ToString().Replace(",", ".") + ", ";
                                    szöveg += ideigkerék_12.ToString().Replace(",", ".") + ", ";
                                    szöveg += ideigkerék_21.ToString().Replace(",", ".") + ", ";
                                    szöveg += ideigkerék_22.ToString().Replace(",", ".") + ", ";
                                    szöveg += ideigkerék_min.ToString().Replace(",", ".") + ") ";

                                    MyA.ABMódosítás(hova, jelszó, szöveg);
                                }
                                else
                                {
                                    // módosítjuk az utolsó adatsort

                                    if (ideigKMUkm == 0d) // ha felújítva volt és nem lett lenullázva
                                    {
                                        ideigvizsgkm = 0d;
                                    }
                                    ideigKMUkm += Havifutás;
                                    ideigTeljeskm += Havifutás;

                                }
                                AlHoltart.Value = i + 1;
                            }
                        }
                    }
                    FőHoltart.Value = j + 1;
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

        private void VizsA_Frisss_Click(object sender, EventArgs e)
        {
            Kiirjaatörténelmet();
        }

        private void VizsA_Excel_Click(object sender, EventArgs e)
        {
            try
            {

                if (Tábla1.Rows.Count <= 0)
                    return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"{Pályaszám.Text.Trim()}_" + Program.PostásTelephely.Trim() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
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

        #endregion

        #region Holtartok

        void Holtart_Be()
        {
            Holtart.Maximum = 20;
            Holtart.Visible = true;
            Holtart.Value = 1;

        }


        void Holtart_Lép()
        {
            Holtart.Value++;
            if (Holtart.Value >= Holtart.Maximum) Holtart.Value = 1;
        }

        void Holtart_Ki()
        {
            Holtart.Visible = false;
        }

        #endregion

        private void FogasListázás()
        {
            try
            {
                AdatokFogas.Clear();
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Villamos4Fogas.mdb";
                string jelszó = "pocsaierzsi";
                string szöveg = "SELECT * FROM KMtábla";
                AdatokFogas = KézKmadatok.Lista_Adatok(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void CiklusListázás()
        {
            try
            {
                AdatokCiklus.Clear();
                string jelszó = "pocsaierzsi";
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\ciklus.mdb";
                string szöveg = $"SELECT * FROM ciklusrendtábla";
                KézCiklus.Lista_Adatok(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
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