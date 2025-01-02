using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Ablakok._4_Nyilvántartások.Kerékeszterga;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using Funkció = Villamos.Villamos_Ablakok._4_Nyilvántartások.Kerékeszterga.Eszterga_Funkció;
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok._5_Karbantartás.Eszterga_Karbantartás
{
    public delegate void Event_Kidobó();

    //Eloretervezesnel csak a datummal lehet uzemoraval nem mukodik valtozot kell lertrehozni es azzal szamolni
    //Excel nem tokeletes
    //Megjegyzes hianyos
    public partial class Ablak_Eszterga_Karbantartás : Form
    {
        #region osztalyszintű elemek
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Eszterga_Karbantartás.mdb";
        readonly string jelszó = "bozaim";
        readonly DateTime MaiDatum = DateTime.Today;
        DateTime TervDatum;
        int Üzemóra = 0;
 
        private List<Adat_Eszterga_Műveletek> AdatokMűvelet;
        private List<Adat_Eszterga_Üzemóra> AdatokÜzemóra;
        private List<DataGridViewRow> PirosSor = new List<DataGridViewRow>();
        private List<DataGridViewRow> SargaSor = new List<DataGridViewRow>();
        private List<DataGridViewRow> ZoldSor = new List<DataGridViewRow>();
        #endregion

        #region Alap
        public Ablak_Eszterga_Karbantartás()
        {
            InitializeComponent();
        }
        private void Ablak_Eszterga_Karbantartás_Load(object sender, EventArgs e)
        {
            string hely = $@"{Application.StartupPath}/Főmérnökség/adatok/Kerékeszterga";
            if (Directory.Exists(hely))
                Directory.CreateDirectory(hely);

            hely += "/Eszterga_Karbantartás.mdb";

            if (!File.Exists(hely))
                Adatbázis_Létrehozás.Eszterga_Karbantartás(hely);

            AdatokÜzemóra = Funkció.Eszterga_ÜzemóraFeltölt();

            Adat_Eszterga_Üzemóra rekord = (from a in AdatokÜzemóra
                                            where a.Dátum.Date == MaiDatum && a.Státus != true
                                            select a).FirstOrDefault();

            if (rekord != null)
            {
                MessageBox.Show($"A mai napon már rögzítettek üzemóra adatot. Az utolsó rögzített üzemóra: {rekord.Üzemóra}.",
                                "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Üzemóra = rekord.Üzemóra;
            }
            else
            {
                using (Ablak_Eszterga_Karbantartás_Segéd SegedAblak = new Ablak_Eszterga_Karbantartás_Segéd())
                {
                    if (SegedAblak.ShowDialog() == DialogResult.OK)
                        Üzemóra = SegedAblak.Üzemóra;
                    else
                    {
                        this.Close();
                        return;
                    }
                }
            }
            Jogosultságkiosztás();
            TáblaListázás();
            AdatokMűvelet = Funkció.Eszterga_KarbantartasFeltölt();
            Tábla.ClearSelection();
        }
        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;
                // csak főmérnökségi belépéssel törölhető
                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {
                }
                else
                {

                }

                melyikelem = 130;
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
        private void Ablak_Eszterga_Karbantartás_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_ablak_EsztergaMódosít?.Close();
        }
        #endregion

        #region Ablakok
        Ablak_Eszterga_Karbantartás_Módosít Új_ablak_EsztergaMódosít;
        private void Btn_Módosítás_Click(object sender, EventArgs e)
        {
            if (Új_ablak_EsztergaMódosít == null)
            {
                Új_ablak_EsztergaMódosít = new Ablak_Eszterga_Karbantartás_Módosít();
                Új_ablak_EsztergaMódosít.FormClosed += Új_ablak_EsztergaMódosít_Closed;
                Új_ablak_EsztergaMódosít.Show();
                Új_ablak_EsztergaMódosít.Eszterga_Változás += TáblaListázás;
            }
            else
            {
                Új_ablak_EsztergaMódosít.Activate();
                Új_ablak_EsztergaMódosít.WindowState = FormWindowState.Maximized;
            }
        }
        private void Új_ablak_EsztergaMódosít_Closed(object sender, FormClosedEventArgs e)
        {
            Új_ablak_EsztergaMódosít = null;
        }
        #endregion

        #region Egyseg
        public enum EsztergaEgyseg
        {
            Dátum = 1,
            Üzemóra = 2,
            Bekövetkezés = 3
        }
        #endregion

        #region Metodusok
        private void TáblaListázás()
        {
            Tábla.Rows.Clear();
            Tábla.Columns.Clear();
            Tábla.Refresh();
            Tábla.Visible = false;
            Tábla.ColumnCount = 8;

            // Fejléc elkészítése
            Tábla.Columns[0].HeaderText = "Sorszám";
            Tábla.Columns[0].Width = 90;

            Tábla.Columns[0].HeaderText = "Művelet";
            Tábla.Columns[0].Width = 530;

            Tábla.Columns[0].HeaderText = "Egység";
            Tábla.Columns[0].Width = 80;

            Tábla.Columns[0].HeaderText = "Nap";
            Tábla.Columns[0].Width = 60;

            Tábla.Columns[0].HeaderText = "Óra";
            Tábla.Columns[0].Width = 60;

            Tábla.Columns[0].HeaderText = "Státus";
            Tábla.Columns[0].Width = 80;

            Tábla.Columns[0].HeaderText = "Utolsó Dátum";
            Tábla.Columns[0].Width = 110;

            Tábla.Columns[0].HeaderText = "Utolsó Üzemóra";
            Tábla.Columns[0].Width = 140;


            AdatokMűvelet = Funkció.Eszterga_KarbantartasFeltölt();
            AdatokÜzemóra = Funkció.Eszterga_ÜzemóraFeltölt();
            TervDatum = DtmPckrElőTerv.Value.Date;
            PirosSor.Clear();
            SargaSor.Clear();
            ZoldSor.Clear();

            foreach (Adat_Eszterga_Műveletek rekord in AdatokMűvelet)
            {
                if (rekord.Státus != true)
                {
                    DataGridViewRow sor = new DataGridViewRow();
                    sor.CreateCells(Tábla);

                    sor.Cells[0].Value = rekord.ID;
                    sor.Cells[1].Value = rekord.Művelet;
                    sor.Cells[2].Value = rekord.Egység;
                    sor.Cells[3].Value = rekord.Mennyi_Dátum;
                    sor.Cells[4].Value = rekord.Mennyi_Óra;
                    sor.Cells[5].Value = rekord.Státus ? "Törölt" : "Aktív";
                    sor.Cells[6].Value = rekord.Utolsó_Dátum.ToShortDateString();
                    sor.Cells[7].Value = rekord.Utolsó_Üzemóra_Állás;

                    Color HatterSzin = Kiszínezés(rekord, TervDatum);
                    sor.DefaultCellStyle.BackColor = HatterSzin;

                    if (HatterSzin == Color.IndianRed)
                        PirosSor.Add(sor);
                    else if (HatterSzin == Color.Yellow)
                        SargaSor.Add(sor);
                    else
                        ZoldSor.Add(sor);
                }
            }
            Tábla.Rows.AddRange(PirosSor.ToArray());
            Tábla.Rows.AddRange(SargaSor.ToArray());
            Tábla.Rows.AddRange(ZoldSor.ToArray());

            Tábla.Visible = true;
            Tábla.ClearSelection();
        }
        private Color Kiszínezés(Adat_Eszterga_Műveletek rekord, DateTime TervDatum)
        {
            int Egyseg = rekord.Egység;
            DateTime UtolsoDatum = rekord.Utolsó_Dátum;
            long UtolsoUzemora = rekord.Utolsó_Üzemóra_Állás;
            long TervUzemora = 0;
            int ElteltNapok = 0;
            long ElteltOrak = 0;
            long AktualisUzemora = 0;
            if (AdatokÜzemóra.Count > 0) AktualisUzemora= AdatokÜzemóra.Max(a => a.Üzemóra);
            if (Egyseg == (int)EsztergaEgyseg.Dátum)
            {
                ElteltNapok = (int)(TervDatum - UtolsoDatum).TotalDays;

                if (ElteltNapok >= rekord.Mennyi_Dátum)
                    return Color.IndianRed;
                else if (ElteltNapok > rekord.Mennyi_Dátum - 10 && rekord.Mennyi_Dátum > 1)
                    return Color.Yellow;
                else
                    return Color.LawnGreen;
            }
            else if (Egyseg == (int)EsztergaEgyseg.Üzemóra)
            {
                ElteltOrak = AktualisUzemora - UtolsoUzemora;
                TervUzemora = ElteltNapok * rekord.Mennyi_Óra;
                if (ElteltOrak >= rekord.Mennyi_Óra && TervUzemora > AktualisUzemora)
                    return Color.IndianRed;
                else if (ElteltOrak >= rekord.Mennyi_Óra - 10)
                    return Color.Yellow;
                else
                    return Color.LawnGreen;
            }
            else if (Egyseg == (int)EsztergaEgyseg.Bekövetkezés)
            {
                bool Datum = (TervDatum - UtolsoDatum).TotalDays >= rekord.Mennyi_Dátum;
                bool Uzemora = AktualisUzemora - UtolsoUzemora >= rekord.Mennyi_Óra;

                if (Datum && Uzemora)
                    return Color.IndianRed;
                else if ((Datum || Uzemora) && rekord.Mennyi_Dátum > 1)
                    return Color.Yellow;
                else
                    return Color.LawnGreen;
            }
            return Color.LawnGreen;
        }
        #endregion

        #region Gombok
        private void Btn_Súgó_Click(object sender, EventArgs e)
        {
            try
            {
                //string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\";
                //MyE.Megnyitás(hely);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Btn_Frissít_Click(object sender, EventArgs e)
        {
            TáblaListázás();
        }
        private void Btn_Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow Sor in Tábla.SelectedRows)
                    {
                        Color HatterSzin = Sor.DefaultCellStyle.BackColor;
                        TervDatum = DtmPckrElőTerv.Value.Date;

                        if (MaiDatum != TervDatum)
                        {
                            MessageBox.Show("A mai dátum nem egyezik a kiválasztott tervdátummal.");
                            return;
                        }
                        if (HatterSzin == Color.LawnGreen)
                        {
                            MessageBox.Show("Ez a sor nem módosítható, mert már a művelet elkészült vagy nem kell még végrehajtani.");
                            return;
                        }

                        int Id = Sor.Cells[0].Value.ToÉrt_Int();
                        long AktivUzemora = 0;
                        if (AdatokÜzemóra.Count > 0) AktivUzemora = AdatokÜzemóra.Max(a => a.Üzemóra);

                        string szöveg = $"UPDATE Műveletek SET ";
                        szöveg += $"Utolsó_Dátum=#{MaiDatum:yyyy-MM-dd}#, ";
                        szöveg += $"Utolsó_Üzemóra_Állás={AktivUzemora} ";
                        szöveg += $"WHERE ID = {Id}";

                        MyA.ABMódosítás(hely, jelszó, szöveg);
                    }
                    TáblaListázás();

                    MessageBox.Show("Az adatok rögzítése megtörtént.", "Rögzítve.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Válasszon ki egy vagy több sort a táblázatból.");
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DtmPckrElőTerv_ValueChanged(object sender, EventArgs e)
        {
            TáblaListázás();
        }
        private void Btn_Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.Rows.Count <= 0) throw new HibásBevittAdat("Nincs sora a táblázatnak!");
                string fájlexc;
                int SorId = 2;
                int FejlecId = 1;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Eszterga Karbantartás Mentése",
                    FileName = $"Eszterga_Karbantartás_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;
                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);

                MyE.ExcelLétrehozás();
                MyE.Aktív_Cella("Munka1", "A1");

                string[] Fejlecek = { "Sorszám", "Művelet", "Egység", "Nap", "Óra", "Státus", "Utolsó Dátum", "Utolsó Üzemóra" };
                
                foreach (string fejlec in Fejlecek)
                {
                    MyE.Kiir(fejlec, $"{MyE.Oszlopnév(FejlecId)}1");
                    FejlecId++;
                }

                foreach (DataGridViewRow Sor in Tábla.Rows)
                {
                    if (Sor.IsNewRow) continue;

                    int OszlopId = 1;
                    foreach (DataGridViewCell cell in Sor.Cells)
                    {
                        string CellaErtek = cell.Value?.ToString();
                        MyE.Kiir(CellaErtek, MyE.Oszlopnév(OszlopId) + SorId);
                        OszlopId++;
                    }

                    Color SorSzin = Sor.DefaultCellStyle.BackColor;
                    if (SorSzin != Color.Empty)
                    {
                        string CellaHossz = $"{MyE.Oszlopnév(1)}{SorId}:{MyE.Oszlopnév(Sor.Cells.Count)}{SorId}";
                        MyE.Háttérszín(CellaHossz, SorSzin);
                    }
                    SorId++;
                }
                string szovegracsoz = $"{MyE.Oszlopnév(1)}1:{MyE.Oszlopnév(Tábla.Columns.Count)}{Tábla.Rows.Count + 1}";
                MyE.Rácsoz(szovegracsoz);
                MyE.ExcelMentés(fájlexc);
                MyE.Megnyitás($"{fájlexc}.xlsx");

                MessageBox.Show($"Az Excel fájl sikeresen létrejött: {fájlexc}", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}