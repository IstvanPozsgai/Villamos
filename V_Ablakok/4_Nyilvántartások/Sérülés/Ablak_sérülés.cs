using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Ablakok._4_Nyilvántartások.Sérülés;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using static System.IO.File;
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    public partial class Ablak_sérülés
    {
        #region Osztályszintű elemek
        readonly Kezelő_Telep_Kiegészítő_SérülésCaf KézSérülésCaf = new Kezelő_Telep_Kiegészítő_SérülésCaf();
        readonly Kezelő_Sérülés_Jelentés KézSérülésJelentés = new Kezelő_Sérülés_Jelentés();
        readonly Kezelő_Sérülés_Költség KézSérülésKöltség = new Kezelő_Sérülés_Költség();
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly Kezelő_Kiegészítő_Sérülés KézKiegSérülés = new Kezelő_Kiegészítő_Sérülés();
        readonly Kezelő_Sérülés_Tarifa KézTarifa = new Kezelő_Sérülés_Tarifa();
        readonly Kezelő_Kiegészítő_SérülésSzöveg KézSérülésSzöveg = new Kezelő_Kiegészítő_SérülésSzöveg();
        readonly Kezelő_Sérülés_Anyag KézSérülésAnyag = new Kezelő_Sérülés_Anyag();
        readonly Kezelő_Sérülés_Művelet KézSérülésMűvelet = new Kezelő_Sérülés_Művelet();
        readonly Kezelő_Sérülés_Visszajelentés KézSérülésVisszajelentés = new Kezelő_Sérülés_Visszajelentés();

        List<Adat_Kiegészítő_SérülésSzöveg> AdatokSérülésSzöveg = new List<Adat_Kiegészítő_SérülésSzöveg>();
        List<Adat_Telep_Kiegészítő_SérülésCaf> AdatokSérülésCaf = new List<Adat_Telep_Kiegészítő_SérülésCaf>();
        List<Adat_Sérülés_Jelentés> AdatokSérülésJelentés = new List<Adat_Sérülés_Jelentés>();
        List<Adat_Sérülés_Költség> AdatokSérülésKöltség = new List<Adat_Sérülés_Költség>();
        Adat_Jármű AdatJármű;
        Adat_Kiegészítő_Sérülés AdatKiegSérülés;
        List<Adat_Kiegészítő_Sérülés> AdatokKiegSérülés = new List<Adat_Kiegészítő_Sérülés>();

        readonly string Sérülésjelszó = "tükör";
        readonly string Jelentésszöveg = "SELECT * FROM jelentés";
        readonly string Költségszöveg = "SELECT * FROM költség";
        int KivalasztottSorszam = -1;
        int Doksikdb, Képdb;
        int Cafsorszám;
        int FénySorszám2;
        string FényPályaszám2;
#pragma warning disable IDE0044 // Add readonly modifier
        List<string> Telephely_Költ = new List<string>();
        List<string> Telephely_Jel = new List<string>();
#pragma warning restore IDE0044 // Add readonly modifier
        #endregion


        #region Ablak Töltése
        public Ablak_sérülés()
        {
            InitializeComponent();
        }

        private void Ablak_sérülés_Load(object sender, EventArgs e)
        {
            Lapfülek.Visible = false;
            Cursor = Cursors.WaitCursor;
            Telephelyekfeltöltése();
            Dátum.Value = DateTime.Today;
        }


        private void Ablak_sérülés_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Sérülés_PDF?.Close();
            Új_Ablak_Sérülés_Kép?.Close();
        }


        private void Ablak_sérülés_Shown(object sender, EventArgs e)
        {
            try
            {
                // létrehozzuk az adott évi táblázatot illetve könyvtárat
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{DateTime.Today:yyyy}";
                if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

                hely += $@"\sérülés{DateTime.Today:yyyy}.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.Tükörtáblák(hely);

                Telephely1.Text = Cmbtelephely.Text.Trim();
                LekDátumig.Value = DateTime.Today;
                LekDátumtól.Value = new DateTime(DateTime.Today.Year, 1, 1);

                KöltDátumig.Value = DateTime.Today;
                KöltDátumtól.Value = new DateTime(DateTime.Today.Year, 1, 1);

                Dátum_tarifa.Value = DateTime.Today;

                Lek_telephelyfeltöltés();

                Tarifa_kiírása();
                Állandókiiró();
                DigitálisKiíró();
                Cafkiiró();

                Üresrögzítő();
                Jogosultságkiosztás();
                AdatokSérülés_Feltöltés();
                AdatokKöltség_Feltöltés();
                AdatokKöltségNullás_Feltöltés();
                Lapfülek.DrawMode = TabDrawMode.OwnerDrawFixed;
                Lapfülek.SelectedIndex = 1;
                Lapfülek.Visible = true;
                Refresh();
                Cursor = Cursors.Default;
                Kitöltendő_mezők();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region alap

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                Cmbtelephely.Items.AddRange(Listák.TelephelyLista_Jármű());
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
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Btn_Súgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\sérülésnyilvántartás.html";
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


        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;

                // ide kell az összes gombot tenni amit szabályozni akarunk false

                Rögzítjelentés.Enabled = false;
                Visszaállít.Enabled = false;
                Újat.Enabled = false;
                Btn_Kép_Hozzáad.Enabled = false;
                Btn_PDF_Hozzáad.Enabled = false;
                Elkészült.Enabled = false;
                SAPBeolvasó.Enabled = false;
                Btn_SAP_Betöltés_Excelbe.Enabled = false;
                Btn_SAP_Feltöltés_Excelből.Enabled = false;
                Btn_ÁllandóÉrt_Felépít_Rögzít.Enabled = false;
                Btn_ÁllandóÉrt_Tarifa_Rögzít.Enabled = false;
                KépLementés.Enabled = false;
                KépTörlés.Enabled = false;
                PdfTörlés.Enabled = false;
                CAFRögzít.Enabled = false;
                CafTöröl.Enabled = false;

                // csak főmérnökségi belépéssel törölhető
                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {
                    KépTörlés.Visible = true;
                    PdfTörlés.Visible = true;
                }
                else
                {
                    KépTörlés.Visible = false;
                    PdfTörlés.Visible = false;
                }

                melyikelem = 92;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Újat.Enabled = true;
                    Rögzítjelentés.Enabled = true;
                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    Btn_Kép_Hozzáad.Enabled = true;
                    Btn_PDF_Hozzáad.Enabled = true;
                }
                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 3))
                {
                    Btn_ÁllandóÉrt_Tarifa_Rögzít.Enabled = true;
                    Btn_ÁllandóÉrt_Felépít_Rögzít.Enabled = true;
                }
                if (Program.Postás_Vezér)
                {
                    melyikelem = 93;
                    // módosítás 1 
                    if (MyF.Vanjoga(melyikelem, 1))
                        Visszaállít.Enabled = true;
                    // módosítás 2
                    if (MyF.Vanjoga(melyikelem, 2))
                        Elkészült.Enabled = true;
                    // módosítás 3 
                    if (MyF.Vanjoga(melyikelem, 3))
                        SAPBeolvasó.Enabled = true;

                    melyikelem = 94;
                    // módosítás 1 
                    if (MyF.Vanjoga(melyikelem, 1))
                    {
                        Btn_SAP_Betöltés_Excelbe.Enabled = true;
                        Btn_SAP_Feltöltés_Excelből.Enabled = true;
                    }
                    // módosítás 2
                    if (MyF.Vanjoga(melyikelem, 2))
                    {
                        KépTörlés.Visible = true;
                        KépTörlés.Enabled = true;
                    }
                    // módosítás 3 
                    if (MyF.Vanjoga(melyikelem, 3))
                    {
                        PdfTörlés.Enabled = true;
                        PdfTörlés.Visible = true;
                    }
                }
                // ez már megint telephelyi
                melyikelem = 95;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                    KépLementés.Enabled = true;

                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    CAFRögzít.Enabled = true;
                    CafTöröl.Enabled = true;
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
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LAPFülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }


        private void Fülekkitöltése()
        {

            switch (Lapfülek.SelectedIndex)
            {
                case 0:
                    break;

                case 1:
                    break;

                case 3:
                    {
                        // SAP adatok
                        Tábla1.Visible = false;
                        break;
                    }

                case 4:
                    {
                        // állandó értékek
                        Tarifa_kiírása();
                        Állandókiiró();
                        DigitálisKiíró();
                        break;
                    }

                case 5:
                    break;

                case 6:
                    break;

                case 7:
                    {
                        // CAF lapfül
                        Cafkiiró();
                        break;
                    }
            }
        }


        private void Lapfülek_DrawItem(object sender, DrawItemEventArgs e)
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
        #endregion


        #region CAF lapfül
        private void CaFListaFeltöltés()
        {
            try
            {

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd";
                if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

                hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\sérüléscaf.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.CAFtáblakészít(hely);
                AdatokSérülésCaf.Clear();
                string jelszó = "kismalac";
                string szöveg = "SELECT * FROM tábla ORDER BY id";
                AdatokSérülésCaf = KézSérülésCaf.Lista_Adatok(hely, jelszó, szöveg);

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cafkiiró()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd";
                if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

                hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\sérüléscaf.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.CAFtáblakészít(hely);

                string jelszó = "kismalac";
                string szöveg = "SELECT * FROM tábla ORDER BY id";

                DataTable AdatTábla = new DataTable();

                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("S.sz");
                AdatTábla.Columns.Add("Cég");
                AdatTábla.Columns.Add("Név");
                AdatTábla.Columns.Add("Beosztás");

                AdatokSérülésCaf = KézSérülésCaf.Lista_Adatok(hely, jelszó, szöveg);

                AdatTábla.Clear();
                foreach (Adat_Telep_Kiegészítő_SérülésCaf rekord in AdatokSérülésCaf)
                {
                    DataRow Soradat = AdatTábla.NewRow();

                    Soradat["S.sz"] = rekord.Id;
                    Soradat["Cég"] = rekord.Cég;
                    Soradat["Név"] = rekord.Név;
                    Soradat["Beosztás"] = rekord.Beosztás;

                    AdatTábla.Rows.Add(Soradat);

                }
                CafTábla.DataSource = AdatTábla;

                CafTábla.Columns["S.sz"].Width = 100;
                CafTábla.Columns["Cég"].Width = 300;
                CafTábla.Columns["Név"].Width = 300;
                CafTábla.Columns["Beosztás"].Width = 300;

                CafTábla.Visible = true;
                CafTábla.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CAF_ürítő()
        {
            Cafsorszám = -1;
            Cégtext.Text = "";
            Névtext.Text = "";
            BeosztásText.Text = "";
            Cafkiiró();
        }

        private void Btn_CAF_Új_Click(object sender, EventArgs e)
        {
            CAF_ürítő();
        }

        private void CAFRögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Cégtext.Text.Trim() == "") throw new HibásBevittAdat("Töltse ki a Cég mezőt!");
                if (Névtext.Text.Trim() == "") throw new HibásBevittAdat("Töltse ki a Név mezőt!");
                if (BeosztásText.Text.Trim() == "") throw new HibásBevittAdat("Töltse ki a Beosztás mezőt!");

                CaFListaFeltöltés();
                int Rekordszám = 1;
                if (AdatokSérülésCaf.Count > 0) Rekordszám = AdatokSérülésCaf.Max(a => a.Id) + 1;

                string szöveg = "";
                if (Cafsorszám == -1)
                {

                    szöveg = "INSERT INTO tábla (id, cég, név, beosztás) VALUES (";
                    szöveg += $"{Rekordszám}, ";
                    szöveg += $"'{Cégtext.Text.Trim()}', ";
                    szöveg += $"'{Névtext.Text.Trim()}', ";
                    szöveg += $"'{BeosztásText.Text.Trim()}')";
                    string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\sérüléscaf.mdb";
                    string jelszó = "kismalac";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                    MessageBox.Show("A rögzítés megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Adat_Telep_Kiegészítő_SérülésCaf EgyElem = (from a in AdatokSérülésCaf
                                                                where a.Id == Cafsorszám
                                                                select a).FirstOrDefault();
                    if (EgyElem != null)
                    {
                        szöveg = "UPDATE tábla SET ";
                        szöveg += $"cég='{Cégtext.Text.Trim()}', ";
                        szöveg += $"név='{Névtext.Text.Trim()}', ";
                        szöveg += $"beosztás='{BeosztásText.Text.Trim()}' ";
                        szöveg += $" WHERE [id] ={Cafsorszám}";
                        string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\sérüléscaf.mdb";
                        string jelszó = "kismalac";
                        MyA.ABMódosítás(hely, jelszó, szöveg);
                        MessageBox.Show("A módosítás megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                Cafkiiró();
                CAF_ürítő();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CafTöröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (Cafsorszám == -1) throw new HibásBevittAdat("Nincs kiválasztva sorszám!");
                CaFListaFeltöltés();

                Adat_Telep_Kiegészítő_SérülésCaf Elem = (from a in AdatokSérülésCaf
                                                         where a.Id == Cafsorszám
                                                         select a).FirstOrDefault();

                if (Elem != null)
                {
                    string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\sérüléscaf.mdb";
                    string jelszó = "kismalac";
                    string szöveg = $"DELETE FROM tábla WHERE id={Cafsorszám}";
                    MyA.ABtörlés(hely, jelszó, szöveg);
                    Újraszámolás(hely, jelszó);

                    MessageBox.Show("A törlés megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Cafkiiró();
                CAF_ürítő();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void Újraszámolás(string hely, string jelszó)
        {
            try
            {
                string szöveg = "SELECT * FROM tábla ORDER BY id";
                AdatokSérülésCaf = KézSérülésCaf.Lista_Adatok(hely, jelszó, szöveg);

                List<string> szövegGy = new List<string>();
                for (int index = 0; index < AdatokSérülésCaf.Count; index++)
                {
                    int újId = index + 1;
                    szöveg = $"UPDATE tábla SET id={újId} WHERE id={AdatokSérülésCaf[index].Id}";
                    szövegGy.Add(szöveg);
                    AdatokSérülésCaf[index].Id = újId;
                }
                MyA.ABMódosítás(hely, jelszó, szövegGy);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CafTábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) throw new HibásBevittAdat("Nincs sora a táblázatnak!");
                Cafsorszám = CafTábla.Rows[e.RowIndex].Cells[0].Value.ToÉrt_Int();
                Cégtext.Text = CafTábla.Rows[e.RowIndex].Cells[1].Value.ToStrTrim();
                Névtext.Text = CafTábla.Rows[e.RowIndex].Cells[2].Value.ToStrTrim();
                BeosztásText.Text = CafTábla.Rows[e.RowIndex].Cells[3].Value.ToStrTrim();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion



        #region Állanó értékek
        private void Tarifa_kiírása()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\Segéd\sérülés{Dátum_tarifa.Value:yyyy}.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.Tükörtáblák(hely);

                // kitölti az állandó értékeket

                string szöveg = "SELECT * FROM tarifa WHERE id=1";
                Kezelő_Sérülés_Tarifa Kéz = new Kezelő_Sérülés_Tarifa();
                Adat_Sérülés_Tarifa Adat = Kéz.Egy_Adat(hely, Sérülésjelszó, szöveg);
                ÉvestarifaD60.Text = "0";
                ÉvestarifaD03.Text = "0";
                if (Adat != null)
                {
                    ÉvestarifaD60.Text = Adat.D60tarifa.ToStrTrim();
                    ÉvestarifaD03.Text = Adat.D03tarifa.ToStrTrim();
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ÁllandóListaFeltöltés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd";
                if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

                hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\sérülés.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.Sérüléstábla(hely);

                string jelszó = "kismalac";
                string szöveg = "SELECT * FROM tábla";

                AdatokSérülésSzöveg = KézSérülésSzöveg.Lista_Adatok(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Állandókiiró()
        {
            try
            {
                ÁllandóListaFeltöltés();
                Adat_Kiegészítő_SérülésSzöveg Rekord = (from a in AdatokSérülésSzöveg
                                                        where a.Id == 1
                                                        select a).FirstOrDefault();

                if (Rekord != null)
                {
                    Iktatószám.Text = Rekord.Szöveg1;
                    Kiállította.Text = Rekord.Szöveg2;
                    Telefonszám.Text = Rekord.Szöveg3;
                    Eszköz.Text = Rekord.Szöveg4;
                    Text1.Text = Rekord.Szöveg5;
                    Text2.Text = Rekord.Szöveg6;
                    Text3.Text = Rekord.Szöveg7;
                    Text4.Text = Rekord.Szöveg8;
                    Text5.Text = Rekord.Szöveg9;
                    Text6.Text = Rekord.Szöveg10;
                    Text7.Text = Rekord.Szöveg11;
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_ÁllandóÉrt_Tarifa_Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (ÉvestarifaD60.Text.Trim() == "") throw new HibásBevittAdat("Töltse ki a ÉvestarifaD60 mezőt!");
                if (!int.TryParse(ÉvestarifaD60.Text, out int ÉvesD60)) throw new HibásBevittAdat("Az Éves D60 tarifa egész számnak kell lennie!");
                if (ÉvestarifaD03.Text.Trim() == "") throw new HibásBevittAdat("Töltse ki a ÉvestarifaD03 mezőt!");
                if (!int.TryParse(ÉvestarifaD03.Text, out int ÉvesD03)) throw new HibásBevittAdat("Az Éves D03 tarifa egész számnak kell lennie!");

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\Segéd\sérülés{Dátum_tarifa.Value.Year}.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.Tükörtáblák(hely);

                string szöveg = "SELECT * FROM tarifa";
                List<Adat_Sérülés_Tarifa> Adatok = KézTarifa.Lista_Adatok(hely, Sérülésjelszó, szöveg);
                Adat_Sérülés_Tarifa Elem = (from a in Adatok
                                            where a.Id == 1
                                            select a).FirstOrDefault();


                if (Elem != null)
                {
                    // módosítás
                    szöveg = "UPDATE tarifa SET ";
                    szöveg += $"d60tarifa={ÉvesD60}, ";
                    szöveg += $"d03tarifa={ÉvesD03}";
                    szöveg += " WHERE [id] =1";
                    MyA.ABMódosítás(hely, Sérülésjelszó, szöveg);

                    MessageBox.Show("A módosítás megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // új adat
                    szöveg = "INSERT INTO tarifa  (id, d60tarifa, d03tarifa ) VALUES (";
                    szöveg += "1, ";
                    szöveg += $"{ÉvesD60}, ";
                    szöveg += $"{ÉvesD03}) ";
                    MyA.ABMódosítás(hely, Sérülésjelszó, szöveg);

                    MessageBox.Show("A rögzítés megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Dátum_tarifa_ValueChanged(object sender, EventArgs e)
        {
            Tarifa_kiírása();
        }

        private void Btn_ÁllandóÉrt_Felépít_Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Iktatószám.Text.Trim() == "") Iktatószám.Text = "_";
                if (Kiállította.Text.Trim() == "") Kiállította.Text = "_";
                if (Telefonszám.Text.Trim() == "") Telefonszám.Text = "_";
                if (Eszköz.Text.Trim() == "") Eszköz.Text = "_";
                if (Text1.Text.Trim() == "") Text1.Text = "_";
                if (Text2.Text.Trim() == "") Text2.Text = "_";
                if (Text3.Text.Trim() == "") Text3.Text = "_";
                if (Text4.Text.Trim() == "") Text4.Text = "_";
                if (Text5.Text.Trim() == "") Text5.Text = "_";
                if (Text6.Text.Trim() == "") Text6.Text = "_";
                if (Text7.Text.Trim() == "") Text7.Text = "_";

                ÁllandóListaFeltöltés();

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\sérülés.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.Sérüléstábla(hely);

                string jelszó = "kismalac";
                string szöveg = "SELECT * FROM tábla ";

                Adat_Kiegészítő_SérülésSzöveg Elem = (from a in AdatokSérülésSzöveg
                                                      where a.Id == 1
                                                      select a).FirstOrDefault();

                if (Elem != null)
                {
                    // módosítás
                    szöveg = "UPDATE tábla SET ";
                    szöveg += $"szöveg1='{Iktatószám.Text.Trim()}', ";
                    szöveg += $"szöveg2='{Kiállította.Text.Trim()}', ";
                    szöveg += $"szöveg3='{Telefonszám.Text.Trim()}', ";
                    szöveg += $"szöveg4='{Eszköz.Text.Trim()}', ";
                    szöveg += $"szöveg5='{Text1.Text.Trim()}', ";
                    szöveg += $"szöveg6='{Text2.Text.Trim()}', ";
                    szöveg += $"szöveg7='{Text3.Text.Trim()}', ";
                    szöveg += $"szöveg8='{Text4.Text.Trim()}', ";
                    szöveg += $"szöveg9='{Text5.Text.Trim()}', ";
                    szöveg += $"szöveg10='{Text6.Text.Trim()}', ";
                    szöveg += $"szöveg11='{Text7.Text.Trim()}' ";
                    szöveg += " WHERE [id] =1";
                }
                else
                {
                    // új
                    szöveg = "INSERT INTO tábla (id, szöveg1, szöveg2, szöveg3, szöveg4, szöveg5, szöveg6, szöveg7, szöveg8, szöveg9, szöveg10, szöveg11) VALUES (";
                    szöveg += "1, ";
                    szöveg += $"'{Iktatószám.Text.Trim()}', ";
                    szöveg += $"'{Kiállította.Text.Trim()}', ";
                    szöveg += $"'{Telefonszám.Text.Trim()}', ";
                    szöveg += $"'{Eszköz.Text.Trim()}', ";
                    szöveg += $"'{Text1.Text.Trim()}', ";
                    szöveg += $"'{Text2.Text.Trim()}', ";
                    szöveg += $"'{Text3.Text.Trim()}', ";
                    szöveg += $"'{Text4.Text.Trim()}', ";
                    szöveg += $"'{Text5.Text.Trim()}', ";
                    szöveg += $"'{Text6.Text.Trim()}', ";
                    szöveg += $"'{Text7.Text.Trim()}')";
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);

                Állandókiiró();

                MessageBox.Show("Az adatok rögzítése/ módosítása megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DigitálisKiíró()
        {
            try
            {
                ÁllandóListaFeltöltés();

                Adat_Kiegészítő_SérülésSzöveg Elem = (from a in AdatokSérülésSzöveg
                                                      where a.Id == 2
                                                      select a).FirstOrDefault();

                if (Elem != null)
                {
                    TxtBxDigitalisAlairo1.Text = Elem.Szöveg1;
                    TxtBxDigitalisAlairo2.Text = Elem.Szöveg2;
                    TxtBxBeosztas1.Text = Elem.Szöveg3;
                    TxtBxBeosztas2.Text = Elem.Szöveg4;
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Digitális_Aláírók_Click(object sender, EventArgs e)
        {
            if (TxtBxDigitalisAlairo1.Text.Trim() == "") throw new HibásBevittAdat("Töltse ki az 1. Aláíró mezőt.");
            if (TxtBxDigitalisAlairo1.Text.Trim() == "") throw new HibásBevittAdat("Töltse ki a 2. Aláíró mezőt.");

            if (TxtBxBeosztas1.Text.Trim() == "") throw new HibásBevittAdat("Töltse ki az 1. Beosztás mezőt.");
            if (TxtBxBeosztas2.Text.Trim() == "") throw new HibásBevittAdat("Töltse ki a 2. Beosztás mezőt.");

            ÁllandóListaFeltöltés();

            Adat_Kiegészítő_SérülésSzöveg Elem = (from a in AdatokSérülésSzöveg
                                                  where a.Id == 2
                                                  select a).FirstOrDefault();
            string szöveg;
            if (Elem != null)
            {
                // módosítás
                szöveg = "UPDATE tábla SET ";
                szöveg += $"szöveg1='{TxtBxDigitalisAlairo1.Text.Trim()}', ";
                szöveg += $"szöveg2='{TxtBxDigitalisAlairo2.Text.Trim()}', ";
                szöveg += $"szöveg3='{TxtBxBeosztas1.Text.Trim()}', ";
                szöveg += $"szöveg4='{TxtBxBeosztas2.Text.Trim()}'";
                szöveg += " WHERE [id] = 2";
            }
            else
            {
                // új adat
                szöveg = "INSERT INTO tábla (id, szöveg1, szöveg2, szöveg3, szöveg4, szöveg5, szöveg6, szöveg7, szöveg8, szöveg9, szöveg10, szöveg11) VALUES (";
                szöveg += $"2, ";
                szöveg += $"'{TxtBxDigitalisAlairo1.Text.Trim()}', ";
                szöveg += $"'{TxtBxDigitalisAlairo2.Text.Trim()}', ";
                szöveg += $"'{TxtBxBeosztas1.Text.Trim()}', ";
                szöveg += $"'{TxtBxBeosztas2.Text.Trim()}', ";
                szöveg += $"'-', ";
                szöveg += $"'-', ";
                szöveg += $"'-', ";
                szöveg += $"'-', ";
                szöveg += $"'-', ";
                szöveg += $"'-', ";
                szöveg += $"'-')";
            }
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\Segéd\sérülés.mdb";
            string jelszó = "kismalac";
            MyA.ABMódosítás(hely, jelszó, szöveg);
            MessageBox.Show("A rögzítés/módosítás megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DigitálisKiíró();
        }
        #endregion


        #region Sérülések Lekérdezése lapfül
        private void Lek_telephelyfeltöltés()
        {
            try
            {
                List<Adat_Kiegészítő_Sérülés> AdatokKiegSérülésÖ = KézKiegSérülés.Lista_Adatok();
                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                    AdatokKiegSérülés = (from a in AdatokKiegSérülésÖ
                                         where a.Vezér1 == false
                                         select a).ToList();
                else
                    AdatokKiegSérülés = (from a in AdatokKiegSérülésÖ
                                         where a.Csoport1 == Program.Postás_csoport
                                         && a.Vezér1 == false
                                         select a).ToList();

                LekTelephely.Items.Clear();
                LekTelephely.Items.Add("<Összes>");

                KöltTelephely.Items.Clear();
                KöltTelephely.Items.Add("<Összes>");

                LekTelephely.BeginUpdate();
                foreach (Adat_Kiegészítő_Sérülés rekord in AdatokKiegSérülés)
                {
                    LekTelephely.Items.Add(rekord.Név);
                    Telephely_Költ.Add(rekord.Név);
                }
                LekTelephely.EndUpdate();
                LekTelephely.Refresh();

                KöltTelephely.BeginUpdate();
                foreach (Adat_Kiegészítő_Sérülés rekord in AdatokKiegSérülés)
                {
                    KöltTelephely.Items.Add(rekord.Név);
                    Telephely_Jel.Add(rekord.Név);
                }
                KöltTelephely.EndUpdate();
                KöltTelephely.Refresh();

                LekTelephely.Text = "<Összes>";
                KöltTelephely.Text = "<Összes>";
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LekLekérdezés_Click(object sender, EventArgs e)
        {
            try
            {
                if (LekDátumig.Value < LekDátumtól.Value) throw new HibásBevittAdat("A kezdő dátumnak nagyobbnak kell lennie a befejező dátumnál.");
                Holtart.Be();

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{LekDátumtól.Value:yyyy}\sérülés{LekDátumtól.Value:yyyy}.mdb";
                string szöveg = "SELECT * FROM költség ORDER BY Rendelés";
                AdatokSérülésKöltség = KézSérülésKöltség.Lista_Adatok(hely, Sérülésjelszó, szöveg);

                AdatokSérülés_Feltöltés();
                DataTable AdatTábla = new DataTable();

                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("S.sz");
                AdatTábla.Columns.Add("Telephely");
                AdatTábla.Columns.Add("Dátum");
                AdatTábla.Columns.Add("Idő");
                AdatTábla.Columns.Add("Helyszín");
                AdatTábla.Columns.Add("Psz");
                AdatTábla.Columns.Add("Járművezető");
                AdatTábla.Columns.Add("Rendelésszám");
                AdatTábla.Columns.Add("M.Státus");
                AdatTábla.Columns.Add("K.Státus");
                AdatTábla.Columns.Add("R.Státus");

                AdatTábla.Clear();
                //int i;
                foreach (Adat_Sérülés_Jelentés rekord in AdatokSérülésJelentés)
                {
                    DataRow Soradat = AdatTábla.NewRow();

                    Soradat["S.sz"] = rekord.Sorszám;
                    Soradat["Telephely"] = rekord.Telephely;
                    Soradat["Dátum"] = rekord.Dátum.ToString("yyyy.MM.dd");
                    Soradat["Idő"] = rekord.Dátum.ToString("HH:mm:ss");
                    Soradat["Helyszín"] = rekord.Balesethelyszín;
                    Soradat["Psz"] = rekord.Rendszám;
                    Soradat["Járművezető"] = rekord.Járművezető;
                    Soradat["Rendelésszám"] = rekord.Rendelésszám;



                    switch (rekord.Státus)
                    {
                        case 1:
                            Soradat["M.Státus"] = "Nyitott";
                            break;
                        case 2:
                            Soradat["M.Státus"] = "Elkészült";
                            break;
                        case 9:
                            Soradat["M.Státus"] = "Törölt";
                            break;
                    }
                    switch (rekord.Státus1)
                    {
                        case 1:
                            Soradat["K.Státus"] = "Nyitott";
                            break;
                        case 2:
                            Soradat["K.Státus"] = "Elkészült";
                            break;
                        case 9:
                            Soradat["K.Státus"] = "Törölt";
                            break;
                    }
                    if (rekord.Rendelésszám.ToStrTrim() == "0")
                        Soradat["R.Státus"] = "-";
                    else
                        Soradat["R.Státus"] = "Nincs SAP";

                    Adat_Sérülés_Költség folyt = (from a in AdatokSérülésKöltség
                                                  where a.Rendelés == rekord.Rendelésszám
                                                  select a).FirstOrDefault();
                    if (folyt != null)
                    {
                        if (folyt.Státus.ToStrTrim() == "1")
                            Soradat["R.Státus"] = "MLZR";
                        else
                            Soradat["R.Státus"] = "Nyitott";
                    }
                    AdatTábla.Rows.Add(Soradat);
                    Holtart.Lép();
                }
                Tábla.DataSource = AdatTábla;

                Tábla.Columns["S.sz"].Width = 50;
                Tábla.Columns["Telephely"].Width = 100;
                Tábla.Columns["Dátum"].Width = 100;
                Tábla.Columns["Idő"].Width = 100;
                Tábla.Columns["Helyszín"].Width = 250;
                Tábla.Columns["Psz"].Width = 100;
                Tábla.Columns["Járművezető"].Width = 200;
                Tábla.Columns["Rendelésszám"].Width = 120;
                Tábla.Columns["M.Státus"].Width = 100;
                Tábla.Columns["K.Státus"].Width = 100;
                Tábla.Columns["R.Státus"].Width = 100;

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
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdatokSérülés_Feltöltés()
        {
            try
            {
                AdatokSérülésJelentés.Clear();

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{LekDátumtól.Value.Year}\sérülés{LekDátumtól.Value.Year}.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.Tükörtáblák(hely);

                List<Adat_Sérülés_Jelentés> Adatok = KézSérülésJelentés.Lista_Adatok(hely, Sérülésjelszó, Jelentésszöveg);
                List<Adat_Sérülés_Jelentés> Ideig = DátumSzűr(Adatok, LekDátumtól.Value, LekDátumig.Value);
                Ideig = RendszámSzűr(Ideig, Lekrendszám.Text.Trim());
                Ideig = TelephelySzűr(Ideig, Telephely_Jel, LekTelephely.Text.Trim());
                Ideig = StátusSzűrJel(Ideig);

                AdatokSérülésJelentés.Clear();
                AdatokSérülésJelentés.AddRange(Ideig);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdatokJelentés_Feltöltés()
        {
            try
            {
                AdatokSérülésJelentés.Clear();

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{KöltDátumtól.Value.Year}\sérülés{KöltDátumtól.Value.Year}.mdb";

                if (!Exists(hely)) Adatbázis_Létrehozás.Tükörtáblák(hely);
                // Teljes lista
                List<Adat_Sérülés_Jelentés> Adatok = KézSérülésJelentés.Lista_Adatok(hely, Sérülésjelszó, Jelentésszöveg);
                List<Adat_Sérülés_Jelentés> Ideig = DátumSzűr(Adatok, KöltDátumtól.Value, KöltDátumig.Value);
                Ideig = RendszámSzűr(Ideig, KöltRendszám.Text.Trim());
                Ideig = TelephelySzűr(Ideig, Telephely_Költ, KöltTelephely.Text.Trim());
                Ideig = StátusSzűrKölt(Ideig);
                AdatokSérülésJelentés.Clear();
                AdatokSérülésJelentés.AddRange(Ideig);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdatokKöltség_Feltöltés()
        {
            try
            {
                AdatokSérülésKöltség.Clear();

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{KöltDátumtól.Value.Year}\sérülés{KöltDátumtól.Value.Year}.mdb";

                if (!Exists(hely)) Adatbázis_Létrehozás.Tükörtáblák(hely);

                AdatokSérülésKöltség = KézSérülésKöltség.Lista_Adatok(hely, Sérülésjelszó, Költségszöveg);


            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LekExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.Rows.Count <= 0) throw new HibásBevittAdat("Nincs sora a táblázatnak!");
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Sérülés_Telephely_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;
                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);

                MyE.EXCELtábla(fájlexc, Tábla, false);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MyE.Megnyitás($"{fájlexc}.xlsx");
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                KivalasztottSorszam = Tábla.Rows[e.RowIndex].Cells[0].Value.ToÉrt_Int();
                Kiír(LekDátumtól.Value);
                Lapfülek.SelectedIndex = 0;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region PDF lapfül
        private void FilePDF_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (FilePDF.SelectedIndex == -1 || FilePDF.SelectedItem == null) return;

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value:yyyy}\PDF\{FilePDF.SelectedItems[0]}";
                if (!Exists(hely)) throw new HibásBevittAdat("Nem létezik a pdf fájl!");

                Kezelő_Pdf.PdfMegnyitás(PDF_néző, hely);

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void PdfTörlés_Click(object sender, EventArgs e)
        {
            try
            {
                string honnan = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value:yyyy}\PDF\";

                if (FilePDF.SelectedItems.Count == 0) throw new HibásBevittAdat("Nincs kijelölt dokumentum!");
                if (MessageBox.Show("A kijelölt Dokumentunok biztos törölni akarja ?!", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    for (int i = 0; i < FilePDF.SelectedItems.Count; i++)
                        Delete(honnan + FilePDF.SelectedItems[i].ToStrTrim());
                    MessageBox.Show("A Dokumentunok törlése megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Pdflistázása();
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region fénykép lapfül
        private void FileBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (FileBox.SelectedIndex == -1) return;
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value:yyyy}\képek\{FileBox.SelectedItems[0]}";
                if (!Exists(hely)) throw new HibásBevittAdat("Nem létezik a fénykép!");
                KépKeret.Image?.Dispose();

                using (Image Kép = Image.FromFile(hely))
                {
                    KépKeret.Image = new Bitmap(Kép);
                    toolTip1.SetToolTip(KépKeret, hely);
                }
                KépKeret.Visible = true;

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void KépLementés_Click(object sender, EventArgs e)
        {
            try
            {
                string honnan = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value:yyyy}\képek\";
                string hova = "";
                FolderBrowserDialog FolderBrowserDialog1 = new FolderBrowserDialog();
                if (FolderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    hova = FolderBrowserDialog1.SelectedPath;

                if (hova.ToStrTrim() == "") throw new HibásBevittAdat("Nincs elérési út megadva!");
                if (FileBox.SelectedItems.Count == 0) throw new HibásBevittAdat("Nincs kiválasztva kép!");

                for (int i = 0; i < FileBox.SelectedItems.Count; i++)
                    Copy(honnan + FileBox.SelectedItems[i].ToStrTrim(), $@"{hova}\{FileBox.SelectedItems[i].ToStrTrim()}");

                MessageBox.Show("A Képek másolása megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void KépTörlés_Click(object sender, EventArgs e)
        {
            try
            {
                string honnan = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value:yyyy}\képek\";
                KépKeret.Visible = false;
                if (FileBox.SelectedItems.Count == 0)
                    throw new HibásBevittAdat("Nincs kiválasztva kép!");

                if (MessageBox.Show("A kijelölt képeket biztos törölni akarja ?!", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    for (int i = 0; i < FileBox.SelectedItems.Count; i++)
                        Delete(honnan + FileBox.SelectedItems[i].ToStrTrim());
                    MessageBox.Show("A Képek törlése megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Képeklistázása();
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        #endregion


        #region Sérülés képek 

        Ablak_Sérülés_Kép Új_Ablak_Sérülés_Kép;
        private void Btn_Kép_Hozzáad_Click(object sender, EventArgs e)
        {
            try
            {
                if (Sorszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs érvényes sorszám, így nem lehet kép fájlt feltölteni.");
                Új_Ablak_Sérülés_Kép?.Close();

                List<string> képek = new List<string>();
                foreach (string item in FileBox.Items)
                    képek.Add(item.ToStrTrim());

                Új_Ablak_Sérülés_Kép = new Ablak_Sérülés_Kép(Dátum.Value, Képdb, FénySorszám2, FényPályaszám2, képek)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };
                Új_Ablak_Sérülés_Kép.FormClosed += Új_Ablak_Sérülés_Kép_Closed;
                Új_Ablak_Sérülés_Kép.Show();
                Új_Ablak_Sérülés_Kép.Változás += Képeklistázása;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Új_Ablak_Sérülés_Kép_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Sérülés_Kép = null;
        }
        #endregion


        #region Sérülés pdfek
        Ablak_PDF_Feltöltés Új_Ablak_Sérülés_PDF;
        private void Btn_PDF_Hozzáad_Click(object sender, EventArgs e)
        {
            try
            {
                if (Sorszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs érvényes sorszám, így nem lehet pdf fájlt feltölteni.");
                Új_Ablak_Sérülés_PDF?.Close();

                List<string> PDFek = new List<string>();
                foreach (string item in FilePDF.Items)
                    PDFek.Add(item.ToStrTrim());

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value.Year}\PDF\";

                Új_Ablak_Sérülés_PDF = new Ablak_PDF_Feltöltés(hely, Dátum.Value, Doksikdb, FénySorszám2, FényPályaszám2, PDFek, "Sérülés", false)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };
                Új_Ablak_Sérülés_PDF.FormClosed += Új_Ablak_Sérülés_PDF_Closed;
                Új_Ablak_Sérülés_PDF.Show();
                Új_Ablak_Sérülés_PDF.Változás += Pdflistázása;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Új_Ablak_Sérülés_PDF_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Sérülés_PDF = null;
        }
        #endregion


        #region SAP Feltöltés
        private void SAPfül()
        {
            try
            {
                //nullás adatoknál nem csinálunk semmit
                SapSorszám.Text = "";
                SapTelephely.Text = "";
                SapPályaszám.Text = "";
                SapDátum.Value = new DateTime(1900, 1, 1);
                SapRendelés.Text = "";
                NyomtatványKitöltés.Visible = false;
                Chck_Egyszerüsített.Visible = false;
                ChckBxDigitális.Visible = false;

                if (KivalasztottSorszam == -1) return;

                int rowIndex = Tábla2.Rows.Cast<DataGridViewRow>()
                                           .Where(row => row.Cells[0].Value.ToÉrt_Int() == KivalasztottSorszam)
                                           .Select(row => row.Index)
                                           .FirstOrDefault();

                if (rowIndex < 0 || rowIndex >= Tábla2.Rows.Count)
                    return;

                SapSorszám.Text = Tábla2.Rows[rowIndex].Cells[0].Value.ToStrTrim();
                SapTelephely.Text = Tábla2.Rows[rowIndex].Cells[1].Value.ToStrTrim();
                SapPályaszám.Text = Tábla2.Rows[rowIndex].Cells[5].Value.ToStrTrim();
                if (!DateTime.TryParse(Tábla2.Rows[rowIndex].Cells[2].Value.ToStrTrim(), out DateTime Nap)) Nap = new DateTime(1900, 1, 1);
                SapDátum.Value = Nap;
                SapRendelés.Text = Tábla2.Rows[rowIndex].Cells[7].Value.ToStrTrim();

                if (Tábla2.Rows[rowIndex].Cells[8].Value.ToStrTrim() == "Elkészült" && Tábla2.Rows[rowIndex].Cells[10].Value.ToStrTrim() == "MLZR")
                {
                    NyomtatványKitöltés.Visible = true;
                    Chck_Egyszerüsített.Visible = true;
                    ChckBxDigitális.Visible = true;
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RendelésAdatokAnyag_Click(object sender, EventArgs e)
        {
            Rendelésadatokanyag_listázás();
        }

        private void Rendelésadatokanyag_listázás()
        {
            try
            {
                if (SapRendelés.Text.Trim() == "" || SapRendelés.Text.Trim() == "0") throw new HibásBevittAdat("A Rendelés mező üres!");

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{KöltDátumtól.Value:yyyy}\sérülés{KöltDátumtól.Value:yyyy}.mdb";

                if (!Exists(hely)) Adatbázis_Létrehozás.Tükörtáblák(hely);

                string szöveg = "SELECT * FROM Anyag WHERE ";
                if (SapRendelés.Text.ToStrTrim() != "") szöveg += $" rendelés LIKE '%{SapRendelés.Text.ToStrTrim()}%' ORDER BY cikkszám";

                double összesen = default;


                DataTable AdatTábla = new DataTable();

                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Cikkszám");
                AdatTábla.Columns.Add("Megnevezés");
                AdatTábla.Columns.Add("Mennyiség");
                AdatTábla.Columns.Add("ME");
                AdatTábla.Columns.Add("Mozgásnem");
                AdatTábla.Columns.Add("Ár");
                AdatTábla.Columns.Add("Állapot");

                AdatTábla.Clear();

                List<Adat_Sérülés_Anyag> AdatokSérülésAnyag = KézSérülésAnyag.Lista_Adatok(hely, Sérülésjelszó, szöveg);
                DataRow Soradat;
                foreach (Adat_Sérülés_Anyag rekord in AdatokSérülésAnyag)
                {
                    Soradat = AdatTábla.NewRow();

                    Soradat["Cikkszám"] = rekord.Cikkszám;
                    Soradat["Megnevezés"] = rekord.Anyagnév;
                    Soradat["ME"] = rekord.Me;
                    Soradat["Mozgásnem"] = rekord.Mozgásnem;
                    Soradat["Állapot"] = rekord.Állapot;

                    if (rekord.Mozgásnem.ToStrTrim() == "261")
                    {
                        Soradat["Mennyiség"] = rekord.Mennyiség;
                        Soradat["Ár"] = rekord.Ár;
                        összesen += rekord.Ár;
                    }
                    else
                    {
                        Soradat["Mennyiség"] = -1 * rekord.Mennyiség;
                        Soradat["Ár"] = -1 * rekord.Ár;
                        összesen -= rekord.Ár;
                    }

                    AdatTábla.Rows.Add(Soradat);
                }
                Soradat = AdatTábla.NewRow();
                Soradat["Megnevezés"] = "Összesen";
                Soradat["Ár"] = összesen;
                AdatTábla.Rows.Add(Soradat);

                Tábla1.DataSource = AdatTábla;

                Tábla1.Columns["Cikkszám"].Width = 200;
                Tábla1.Columns["Megnevezés"].Width = 500;
                Tábla1.Columns["Mennyiség"].Width = 100;
                Tábla1.Columns["ME"].Width = 100;
                Tábla1.Columns["Mozgásnem"].Width = 100;
                Tábla1.Columns["Ár"].Width = 100;
                Tábla1.Columns["Állapot"].Width = 100;

                Tábla1.Visible = true;

                Tábla1.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RendelésAdatokIdő_Click(object sender, EventArgs e)
        {
            Rendelésadatokmunkaidő_listázása();
        }

        private void Rendelésadatokmunkaidő_listázása()
        {
            try
            {
                Dátum_tarifa.Value = KöltDátumtól.Value;
                Tarifa_kiírása();

                if (SapRendelés.Text.Trim() == "" || SapRendelés.Text.Trim() == "0") throw new HibásBevittAdat("A Rendelés mező üres!");

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{KöltDátumtól.Value:yyyy}\sérülés{KöltDátumtól.Value:yyyy}.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.Tükörtáblák(hely);

                string szöveg = "SELECT * FROM művelet WHERE ";
                if (SapRendelés.Text.ToStrTrim() != "") szöveg += $" rendelés={SapRendelés.Text.ToStrTrim()}";

                DataTable AdatTábla = new DataTable();

                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Művelet leírása");
                AdatTábla.Columns.Add("Felhasznált idő");
                AdatTábla.Columns.Add("Teljesítmény fajta");
                AdatTábla.Columns.Add("Visszaszám");
                AdatTábla.Columns.Add("Költség");

                Kezelő_Sérülés_Művelet Kéz = new Kezelő_Sérülés_Művelet();
                Adat_Sérülés_Művelet Adat = Kéz.Egy_Adat(hely, Sérülésjelszó, szöveg);

                szöveg = "SELECT * FROM visszajelentés";
                Kezelő_Sérülés_Visszajelentés KézVissza = new Kezelő_Sérülés_Visszajelentés();
                List<Adat_Sérülés_Visszajelentés> AdatokVissza = KézVissza.Lista_Adatok(hely, Sérülésjelszó, szöveg);
                if (!double.TryParse(ÉvestarifaD60.Text, out double TarifaD60)) TarifaD60 = 0;
                if (!double.TryParse(ÉvestarifaD03.Text, out double TarifaD03)) TarifaD03 = 0;

                double összesen = 0;

                AdatTábla.Clear();
                DataRow Soradat;
                if (Adat != null)
                {
                    Soradat = AdatTábla.NewRow();

                    Soradat["Művelet leírása"] = Adat.Műveletszöveg;


                    List<Adat_Sérülés_Visszajelentés> IdeigVissza = (from a in AdatokVissza
                                                                     where a.Visszaszám == Adat.Visszaszám
                                                                     select a).ToList();

                    double ideigperc = 0;
                    double ideigÓra = 0;
                    foreach (Adat_Sérülés_Visszajelentés rekord in IdeigVissza)
                    {
                        double perc, óra;
                        if (rekord.Storno.ToStrTrim() == "I")
                            perc = -1 * (double)rekord.Munkaidő;
                        else
                            perc = (double)rekord.Munkaidő;

                        ideigperc += perc;

                        if (rekord.Teljesítményfajta.ToStrTrim() == "D60")
                            óra = perc * TarifaD60 / 60d;
                        else
                            óra = perc * TarifaD03 / 60d;
                        ideigÓra += óra;
                    }

                    Soradat["Felhasznált idő"] = ideigperc;
                    Soradat["Teljesítmény fajta"] = Adat.Teljesítményfajta;
                    Soradat["Visszaszám"] = Adat.Visszaszám;
                    Soradat["Költség"] = ideigÓra;

                    AdatTábla.Rows.Add(Soradat);
                    összesen += ideigÓra;
                }
                Soradat = AdatTábla.NewRow();
                Soradat["Művelet leírása"] = "Összesen";
                Soradat["Költség"] = összesen;
                AdatTábla.Rows.Add(Soradat);

                Tábla1.DataSource = AdatTábla;
                Tábla1.Columns["Művelet leírása"].Width = 350;
                Tábla1.Columns["Felhasznált idő"].Width = 200;
                Tábla1.Columns["Teljesítmény fajta"].Width = 200;
                Tábla1.Columns["Visszaszám"].Width = 200;
                Tábla1.Columns["Költség"].Width = 200;


                Tábla1.Refresh();
                Tábla1.Visible = true;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RendelésAdatokSzolgáltatás_Click(object sender, EventArgs e)
        {
            try
            {

                if (SapRendelés.Text.Trim() == "" || SapRendelés.Text.Trim() == "0") throw new HibásBevittAdat("A Rendelés mező üres!");
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{KöltDátumtól.Value:yyyy}\sérülés{KöltDátumtól.Value:yyyy}.mdb";

                if (!Exists(hely)) Adatbázis_Létrehozás.Tükörtáblák(hely);

                string szöveg = $"SELECT * FROM Költség WHERE rendelés={SapRendelés.Text.ToStrTrim()}";


                AdatokSérülésKöltség = KézSérülésKöltség.Lista_Adatok(hely, Sérülésjelszó, szöveg);

                DataTable AdatTábla = new DataTable();

                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Rendelés szám");
                AdatTábla.Columns.Add("Anyagköltség");
                AdatTábla.Columns.Add("Munka idő költség");
                AdatTábla.Columns.Add("Gépköltség");
                AdatTábla.Columns.Add("Szolgáltatás");

                AdatTábla.Clear();
                foreach (Adat_Sérülés_Költség rekord in AdatokSérülésKöltség)
                {
                    DataRow Soradat = AdatTábla.NewRow();

                    Soradat["Rendelés szám"] = rekord.Rendelés;
                    Soradat["Anyagköltség"] = rekord.Anyagköltség;
                    Soradat["Munka idő költség"] = "0";
                    Soradat["Gépköltség"] = rekord.Gépköltség;
                    Soradat["Szolgáltatás"] = rekord.Szolgáltatás;

                    AdatTábla.Rows.Add(Soradat);

                }
                Tábla1.DataSource = AdatTábla;

                Tábla1.Columns["Rendelés szám"].Width = 200;
                Tábla1.Columns["Anyagköltség"].Width = 500;
                Tábla1.Columns["Munka idő költség"].Width = 100;
                Tábla1.Columns["Gépköltség"].Width = 100;
                Tábla1.Columns["Szolgáltatás"].Width = 100;

                Tábla1.Visible = true;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SAPBeolvasó_Click(object sender, EventArgs e)
        {
            try
            {
                if (SapDátum.Value == DateTime.Parse("1900.01.01")) throw new HibásBevittAdat("Nem megfelelő az évszám!");
                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "SAP-s Adatok betöltése",
                    FileName = "",
                    Filter = "Excel |*.xlsx"
                };
                string fájlexc;
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;

                // megnyitjuk a beolvasandó táblát
                MyE.ExcelMegnyitás(fájlexc);

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{SapDátum.Value:yyyy}\sérülés{SapDátum.Value:yyyy}.mdb";

                // ellenőrizzük, hogy léteznek a táblák
                if (!Exists(hely)) Adatbázis_Létrehozás.Tükörtáblák(hely);

                string szöveg = $"SELECT * FROM Anyag ";
                List<Adat_Sérülés_Anyag> AnyagAdatok = KézSérülésAnyag.Lista_Adatok(hely, Sérülésjelszó, szöveg);
                szöveg = $"SELECT * FROM Költség";
                List<Adat_Sérülés_Költség> KölstégAdatok = KézSérülésKöltség.Lista_Adatok(hely, Sérülésjelszó, szöveg);
                szöveg = $"SELECT * FROM Művelet";
                List<Adat_Sérülés_Művelet> MűveletAdatok = KézSérülésMűvelet.Lista_Adatok(hely, Sérülésjelszó, szöveg);
                szöveg = $"SELECT * FROM Visszajelentés";
                List<Adat_Sérülés_Visszajelentés> VisszajelentésAdatok = KézSérülésVisszajelentés.Lista_Adatok(hely, Sérülésjelszó, szöveg);
                // rendelés szám adatokat átnézzük ha van már ilyen adat az adtok között először töröljük
                int i = 1;
                int hossz, eleje, vége;
                string szó, rendelés, ideig;
                int szószám, utolsóeleje, rendelésstátus;
                double anyagköltség, munkaköltség, gépköltség, szolgáltatás;


                Holtart.Be();
                int utolsó_sor = MyE.Utolsósor("Munka1");
                while (MyE.Beolvas($"A{i}").Trim() != "_")
                {
                    Holtart.Lép();
                    szöveg = MyE.Beolvas($"A{i}");

                    if (Adat_módosítás($"A{i}", 4) == "PM22")
                    {
                        hossz = szöveg.Length;
                        eleje = 0;
                        vége = 0;
                        szó = "";
                        szószám = 1;
                        for (int betűs = 5; betűs < hossz; betűs++)
                        {
                            if (szószám == 2)
                                break;
                            if (szöveg.Substring(betűs - 1, 1).Trim() != "" & eleje == 0)
                                eleje = betűs;
                            if (szöveg.Substring(betűs - 1, 1).Trim() == "" & eleje != 0)
                                vége = betűs;
                            if (hossz == betűs)
                                vége = betűs;
                            if (vége != 0 & eleje != 0)
                            {
                                szó = szöveg.Substring(eleje - 1, vége - eleje).Trim();
                                utolsóeleje = eleje;
                                eleje = 0;
                                vége = 0;
                            }

                            if (double.TryParse(szó.ToStrTrim(), out double RendeléS))
                            {
                                // itt kell nyitogatni a táblákat és törölni az előző adatokat
                                List<string> GySzöveg = new List<string>();

                                Adat_Sérülés_Költség KöltségElem = (from a in KölstégAdatok
                                                                    where a.Rendelés == RendeléS
                                                                    select a).FirstOrDefault();
                                if (KöltségElem != null)
                                {
                                    szöveg = $"DELETE FROM költség WHERE rendelés={RendeléS}";
                                    GySzöveg.Add(szöveg);
                                }

                                Adat_Sérülés_Művelet MűveletElem = (from a in MűveletAdatok
                                                                    where a.Rendelés == RendeléS
                                                                    select a).FirstOrDefault();
                                if (MűveletElem != null)
                                {
                                    szöveg = $"DELETE FROM Művelet WHERE rendelés={RendeléS}";
                                    GySzöveg.Add(szöveg);
                                }

                                Adat_Sérülés_Visszajelentés VisszajelentésElem = (from a in VisszajelentésAdatok
                                                                                  where a.Rendelés == RendeléS
                                                                                  select a).FirstOrDefault();
                                if (VisszajelentésElem != null)
                                {
                                    szöveg = $"DELETE FROM Visszajelentés WHERE rendelés={RendeléS}";
                                    GySzöveg.Add(szöveg);
                                }

                                Adat_Sérülés_Anyag AnyagElem = (from a in AnyagAdatok
                                                                where a.Rendelés == RendeléS
                                                                select a).FirstOrDefault();
                                if (AnyagElem != null)
                                {
                                    szöveg = $"DELETE FROM Anyag WHERE rendelés={RendeléS}";
                                    GySzöveg.Add(szöveg);
                                }
                                szószám += 1;
                                if (GySzöveg.Count > 0) MyA.ABtörlés(hely, Sérülésjelszó, GySzöveg);
                            }
                        }
                    }
                    i += 1;
                }

                #region  Költség sorok
                i = 1;
                anyagköltség = 0;
                munkaköltség = 0;
                gépköltség = 0;
                szolgáltatás = 0;
                rendelésstátus = 0;

                List<string> szövegGy = new List<string>();
                while (MyE.Beolvas($"A{i}").Trim() != "_")
                {

                    Holtart.Lép();
                    szöveg = MyE.Beolvas($"A{i}");
                    if (Adat_módosítás($"A{i}", 4) == "PM22")
                    {
                        if (szöveg.Contains("MLZR") || szöveg.Contains("LEZR"))
                            rendelésstátus = 1;
                        else
                            rendelésstátus = 0;
                    }
                    if (szöveg.Substring(0, 2) == "HU")
                    {
                        rendelés = Adat_módosítás(2, 10, szöveg).ToStrTrim().Replace(".", "");
                        ideig = Adat_módosítás(11, 22, szöveg).Replace(".", "").Replace(" ", "");
                        switch (szöveg.Substring(szöveg.Length - 4, 4))
                        {
                            case "513)":
                                {
                                    if (!double.TryParse(ideig, out anyagköltség)) anyagköltség = 0;
                                    break;
                                }
                            case "571)":
                                {
                                    if (!double.TryParse(ideig, out munkaköltség)) munkaköltség = 0;
                                    break;
                                }
                            case "566)":
                                {
                                    if (!double.TryParse(ideig, out gépköltség)) gépköltség = 0;
                                    break;
                                }
                            case "515)":
                                {
                                    if (!double.TryParse(ideig, out szolgáltatás)) szolgáltatás = 0;
                                    break;
                                }
                        }
                        szöveg = "INSERT INTO ideig (rendelés, anyagköltség, munkaköltség, gépköltség, szolgáltatás, státus ) VALUES (";
                        szöveg += $"{rendelés}, ";
                        szöveg += $"{anyagköltség}, ";
                        szöveg += $"{munkaköltség}, ";
                        szöveg += $"{gépköltség}, ";
                        szöveg += $"{szolgáltatás}, ";
                        szöveg += $"{rendelésstátus}) ";
                        szövegGy.Add(szöveg);

                        anyagköltség = 0;
                        munkaköltség = 0;
                        gépköltség = 0;
                        szolgáltatás = 0;
                        rendelésstátus = 0;
                    }
                    i += 1;
                }
                MyA.ABMódosítás(hely, Sérülésjelszó, szövegGy);

                // költség adatokat rendezzük

                anyagköltség = 0;
                szolgáltatás = 0;
                gépköltség = 0;
                munkaköltség = 0;
                rendelésstátus = 0;
                double rendelés_szám = 0;

                szöveg = "SELECT * FROM ideig ORDER BY rendelés";

                Kezelő_Sérülés_Ideig Kéz = new Kezelő_Sérülés_Ideig();
                List<Adat_Sérülés_Ideig> Adatok = Kéz.Lista_Adatok(hely, Sérülésjelszó, szöveg);
                szövegGy.Clear();
                foreach (Adat_Sérülés_Ideig rekord in Adatok)
                {
                    ideig = rekord.Rendelés.ToStrTrim();
                    if (rendelés_szám != 0 & rendelés_szám.ToStrTrim() != ideig)
                    {
                        szöveg = "INSERT INTO költség (rendelés, anyagköltség, munkaköltség, gépköltség, szolgáltatás, státus ) VALUES (";
                        szöveg += $"{rendelés_szám}, ";
                        szöveg += $"{anyagköltség}, ";
                        szöveg += $"{munkaköltség}, ";
                        szöveg += $"{gépköltség}, ";
                        szöveg += $"{szolgáltatás}, ";
                        szöveg += $"{rendelésstátus}) ";
                        szövegGy.Add(szöveg);


                        anyagköltség = 0;
                        szolgáltatás = 0;
                        gépköltség = 0;
                        munkaköltség = 0;
                        rendelés_szám = 0;
                        rendelésstátus = 0;
                    }

                    rendelésstátus = int.Parse(rekord.Státus.ToStrTrim());
                    rendelés_szám = rekord.Rendelés;
                    if (anyagköltség == 0) anyagköltség = rekord.Anyagköltség;
                    if (szolgáltatás == 0) szolgáltatás = rekord.Szolgáltatás;
                    if (gépköltség == 0) gépköltség = rekord.Gépköltség;
                    if (munkaköltség == 0) munkaköltség = rekord.Munkaköltség;

                }

                szöveg = "INSERT INTO költség (rendelés, anyagköltség, munkaköltség, gépköltség, szolgáltatás, státus ) VALUES (";
                szöveg += $"{rendelés_szám}, ";
                szöveg += $"{anyagköltség}, ";
                szöveg += $"{munkaköltség}, ";
                szöveg += $"{gépköltség}, ";
                szöveg += $"{szolgáltatás}, ";
                szöveg += $"{rendelésstátus}) ";
                szövegGy.Add(szöveg);
                MyA.ABMódosítás(hely, Sérülésjelszó, szövegGy);

                // ki kell törölni az ideig tartalmát
                // *************************************
                // 
                szöveg = "DELETE FROM ideig";
                MyA.ABtörlés(hely, Sérülésjelszó, szöveg);
                #endregion

                #region Művelet sorok

                string Teljesítményfajta;
                string Visszaszám;
                string Műveletszöveg;

                i = 1;
                szövegGy.Clear();
                while (MyE.Beolvas($"A{i}").Trim() != "_")
                {
                    Holtart.Lép();
                    szöveg = MyE.Beolvas($"A{i}").Trim();
                    if (Adat_módosítás($"A{i}", 4) == "MJV1")
                    {
                        hossz = szöveg.Length;
                        eleje = 0;
                        vége = 0;
                        szó = "";
                        szószám = 1;
                        Teljesítményfajta = "A";
                        Visszaszám = "A";
                        Műveletszöveg = "A";
                        rendelés_szám = 0;
                        utolsóeleje = 0;

                        for (int betűs = 5; betűs < hossz; betűs++)
                        {
                            if (szöveg.Substring(betűs - 1, 1).Trim() != "" & eleje == 0)
                                eleje = betűs;
                            if (szöveg.Substring(betűs - 1, 1).Trim() == "" & eleje != 0)
                                vége = betűs;
                            if (hossz == betűs)
                                vége = betűs;
                            if (vége != 0 & eleje != 0)
                            {
                                szó = szöveg.Substring(eleje - 1, vége - eleje + 1).Trim();
                                utolsóeleje = eleje;
                                eleje = 0;
                                vége = 0;
                            }
                            if (szó.Trim() != "")
                            {
                                switch (szószám)
                                {
                                    case 1:
                                        {
                                            if (szó.Substring(0, 1) == "D")
                                                Teljesítményfajta = szó.Trim();
                                            else
                                            {
                                                Teljesítményfajta = "DDD";
                                                szószám -= 1;
                                            }

                                            break;
                                        }
                                    case 2:
                                        {
                                            if (!double.TryParse(szó, out rendelés_szám)) rendelés_szám = 0;
                                            break;
                                        }
                                    case 3:
                                        {
                                            Visszaszám = szó;
                                            break;
                                        }
                                    case 4:
                                        break;
                                }
                                szó = "";
                                if (szószám == 5)
                                {
                                    Műveletszöveg = szöveg.Substring(utolsóeleje - 1, szöveg.Length - utolsóeleje + 1).Trim();
                                    break;
                                }
                                szószám++;
                            }
                        }
                        szöveg = "INSERT INTO művelet (rendelés, Teljesítményfajta, Visszaszám, Műveletszöveg ) VALUES (";
                        szöveg += $"{rendelés_szám}, ";
                        szöveg += $"'{Teljesítményfajta.Trim()} ', ";
                        szöveg += $"'{Visszaszám.Trim()}', ";
                        szöveg += $"'{Műveletszöveg.Trim()} ') ";
                        szövegGy.Add(szöveg);

                    }
                    i += 1;
                }
                MyA.ABMódosítás(hely, Sérülésjelszó, szövegGy);
                #endregion

                #region visszajelentés sorok
                i = 1;
                double munkaidő = 0;
                string storno = "";
                rendelés = "";

                szövegGy.Clear();
                while (MyE.Beolvas($"A{i}").Trim() != "_")
                {
                    Holtart.Lép();
                    szöveg = MyE.Beolvas($"A{i}").Trim();
                    if (szöveg.Substring(0, 3) == "D03" | szöveg.Substring(0, 3) == "D60")
                    {
                        Visszaszám = Adat_módosítás(3, 12, szöveg);

                        if (!double.TryParse(Adat_módosítás(25, 18, szöveg).Replace(".", ""), out munkaidő)) munkaidő = 0;

                        rendelés = Adat_módosítás(42, 9, szöveg);
                        if (szöveg.Substring(szöveg.Length - 1, 1).Trim() == "X")
                            storno = "I";
                        else
                            storno = "N";
                        Teljesítményfajta = szöveg.Substring(0, 3);


                        szöveg = "INSERT INTO visszajelentés (Visszaszám, munkaidő, storno, rendelés,  Teljesítményfajta ) VALUES (";
                        szöveg += $"'{Visszaszám.Trim()}', ";
                        szöveg += $"{munkaidő}, ";
                        szöveg += $"'{storno.Trim()}', ";
                        szöveg += $"{rendelés}, ";
                        szöveg += $"'{Teljesítményfajta.Trim()}') ";
                        szövegGy.Add(szöveg);

                    }
                    i++;
                }
                MyA.ABMódosítás(hely, Sérülésjelszó, szövegGy);
                #endregion

                #region  Anyag sorok

                i = 1;
                string cikkszám = "";
                double mennyiség = 0d;
                double ár = 0d;
                string állapot = "";
                string Mennyiségegység = "";
                string mozgásnem = "";
                string anyagnév = "";
                rendelés_szám = 0;
                utolsóeleje = 0;

                szövegGy.Clear();
                while (MyE.Beolvas($"A{i}") != "_")
                {
                    Holtart.Lép();
                    szöveg = MyE.Beolvas($"A{i}").Trim();
                    if (szöveg.Substring(0, 3) == "BKV" & szöveg.Substring(0, 7) != "BKV BKV")
                    {
                        hossz = szöveg.Length;
                        eleje = 0;
                        vége = 0;
                        szó = "";
                        szószám = 4;

                        cikkszám = Adat_módosítás(3, 20, szöveg).Trim();
                        ideig = Adat_módosítás(22, 17, szöveg).Trim().Replace(".", "");
                        if (!double.TryParse(ideig, out mennyiség)) mennyiség = 0;


                        ideig = Adat_módosítás(39, 17, szöveg).Trim().Replace(".", "");
                        if (!double.TryParse(ideig, out ár)) ár = 0;


                        for (int betűs = 56; betűs < hossz; betűs++)
                        {
                            if (szöveg.Substring(betűs - 1, 1).Trim() != "" && eleje == 0) eleje = betűs;
                            if (szöveg.Substring(betűs - 1, 1).Trim() == "" && eleje != 0) vége = betűs;
                            if (hossz == betűs) vége = betűs;
                            if (vége != 0 && eleje != 0)
                            {
                                szó = szöveg.Substring(eleje - 1, vége - eleje + 1).Trim();
                                utolsóeleje = vége;
                                eleje = 0;
                                vége = 0;
                            }
                            if (szó.Trim() != "")
                            {
                                switch (szószám)
                                {
                                    case 4:
                                        {
                                            állapot = szó.Trim();
                                            break;
                                        }
                                    case 5:
                                        break;
                                    // dátum amit nem használunk fel
                                    case 6:
                                        break;
                                    // ezt sem használjuk
                                    case 7:
                                        {
                                            if (!double.TryParse(szó, out rendelés_szám)) rendelés_szám = 0;
                                            break;
                                        }
                                    case 8:
                                        {
                                            Mennyiségegység = szó.Trim();
                                            break;
                                        }
                                    case 9:
                                        {
                                            mozgásnem = szó.Trim();
                                            break;
                                        }
                                }
                                szó = "";
                                szószám++;
                                if (szószám == 10)
                                {
                                    anyagnév = szöveg.Substring(utolsóeleje - 1, szöveg.Length - utolsóeleje + 1).Trim();
                                    break;
                                }

                            }
                        }

                        szöveg = "INSERT INTO Anyag (cikkszám, anyagnév, mennyiség, me, ár, állapot, rendelés, mozgásnem ) VALUES (";
                        szöveg += $"'{cikkszám.Trim()}', ";
                        szöveg += $"'{anyagnév.Trim()} ', ";
                        szöveg += $"{mennyiség.ToStrTrim().Replace(",", ".")}, ";  // a tizedes vessző miatt ponttal rögzítem
                        szöveg += $"'{Mennyiségegység.Trim()} ', ";
                        szöveg += $"{ár}, ";
                        szöveg += $"'{állapot.Trim()}', ";
                        szöveg += $"{rendelés_szám}, ";
                        szöveg += $"'{mozgásnem.Trim()}') ";
                        szövegGy.Add(szöveg);
                    }

                    i += 1;
                }
                MyA.ABMódosítás(hely, Sérülésjelszó, szövegGy);
                #endregion
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();

                Holtart.Ki();
                // kitöröljük a betöltött fájlt
                Delete(fájlexc);
                MessageBox.Show("Az adatok feltöltése megtörtént!", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_SAP_Betöltés_Excelbe_Click(object sender, EventArgs e)
        {
            try
            {
                string fájlexc;
                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Sérülés_beviteli_alaptábla készítés",
                    FileName = $"Sérülés_beviteli_alaptábla{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                Holtart.Be();
                MyE.ExcelLétrehozás();


                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\beolvasás.mdb";
                string jelszó = "sajátmagam";
                string szöveg = "SELECT * FROM tábla WHERE [csoport]='SérülésAny' AND [törölt]='0' ORDER BY oszlop";

                int i = 1;
                Kezelő_Alap_Beolvasás Kéz = new Kezelő_Alap_Beolvasás();
                List<Adat_Alap_Beolvasás> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);
                foreach (Adat_Alap_Beolvasás rekord in Adatok)
                {
                    MyE.Kiir(rekord.Fejléc.ToStrTrim(), MyE.Oszlopnév(i) + "1");
                    i += 1;
                }

                MyE.Oszlopszélesség("Munka1", "a:a", 20);
                MyE.Oszlopszélesség("Munka1", "b:b", 50);
                MyE.Oszlopszélesség("Munka1", "c:h", 11);
                // minden szöveg
                MyE.Betű("A:H", "", "@");
                MyE.Betű("C:C", "", "0.00");
                MyE.Betű("E:E", "", "0.00");

                MyE.Aktív_Cella("Munka1", "A1");
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
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_SAP_Feltöltés_Excelből_Click(object sender, EventArgs e)
        {
            try
            {
                if (SapDátum.Value == new DateTime(1900, 1, 1)) throw new HibásBevittAdat("Nem megfelelő az évszám!");

                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "SAP-s Adatok betöltése",
                    FileName = "",
                    Filter = "Excel |*.xlsx"
                };
                string fájlexc;
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;

                // megnyitjuk a beolvasandó táblát
                MyE.ExcelMegnyitás(fájlexc);

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{SapDátum.Value:yyyy}\sérülés{SapDátum.Value:yyyy}.mdb";
                // ellenőrizzük, hogy léteznek a táblák
                if (!Exists(hely)) Adatbázis_Létrehozás.Tükörtáblák(hely);

                // megnézzük, hogy hány sorból áll a tábla
                int utolsó = MyE.Utolsósor("Munka1");

                Holtart.Lép();
                // kitöröljük azokat az anyagokat amiket a rendelésekre amelyek korábban rögzítettünk
                string szöveg = "SELECT * FROM Anyag ";
                List<Adat_Sérülés_Anyag> AnyagAdatok = KézSérülésAnyag.Lista_Adatok(hely, Sérülésjelszó, szöveg);

                List<string> szövegGy = new List<string>();
                for (int i = 2; i < utolsó; i++)
                {
                    if (double.TryParse(MyE.Beolvas($"g{i}").Trim(), out double rendelés))
                    {
                        Adat_Sérülés_Anyag EgyAnyag = (from a in AnyagAdatok
                                                       where a.Rendelés == rendelés
                                                       select a).FirstOrDefault();

                        if (EgyAnyag != null)
                        {
                            szöveg = $"DELETE * FROM Anyag WHERE rendelés={rendelés}";
                            szövegGy.Add(szöveg);
                        }
                    }
                }
                if (szövegGy.Count != 0) MyA.ABtörlés(hely, Sérülésjelszó, szövegGy);
                // beolvassuk az adatokat
                szöveg = "SELECT * FROM anyag ";
                string cikkszám = "";
                string mennyiségstr = "";
                string árstr = "";
                string állapot = "";
                string Mennyiségegység = "";
                string mozgásnem = "";
                string anyagnév = "";
                string rendelésstr;

                szövegGy.Clear();
                for (int i = 2; i < utolsó; i++)
                {
                    Holtart.Lép();
                    cikkszám = Adat_módosítás($"a{i}", 20);
                    anyagnév = Adat_módosítás($"b{i}", 50);
                    string ideig = MyE.Beolvas($"c{i}").Trim();
                    if (ideig != "")
                        mennyiségstr = ideig;
                    else
                        mennyiségstr = "0";
                    Mennyiségegység = Adat_módosítás($"d{i}", 10);

                    ideig = MyE.Beolvas($"e{i}").Trim();
                    if (ideig != "")
                        árstr = ideig;
                    else
                        árstr = "0";
                    állapot = Adat_módosítás($"f{i}", 3);

                    ideig = MyE.Beolvas($"g{i}").Trim();
                    if (ideig != "")
                        rendelésstr = ideig;
                    else
                        rendelésstr = "0";
                    mozgásnem = Adat_módosítás($"h{i}", 5);

                    szöveg = "INSERT INTO Anyag (cikkszám, anyagnév, mennyiség, me, ár, állapot, rendelés, mozgásnem ) VALUES (";
                    szöveg += $"'{cikkszám}', "; //cikkszám
                    szöveg += $"'{anyagnév}', "; //anyagnév
                    szöveg += $"{mennyiségstr.Replace(',', '.')}, ";  // a tizedes vessző miatt ponttal rögzítem  mennyiség
                    szöveg += $"'{Mennyiségegység}', "; //me
                    szöveg += $"{árstr.Replace(',', '.')}, "; //ár
                    szöveg += $"'{állapot}', ";  //állapot
                    szöveg += $"{rendelésstr.Replace(',', '.')}, ";  //rendelés
                    szöveg += $"'{mozgásnem}') ";// mozgásnem
                    szövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(hely, Sérülésjelszó, szöveg);

                MyE.Aktív_Cella("Munka1", "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();

                Holtart.Ki();
                // kitöröljük a betöltött fájlt
                Delete(fájlexc);
                MessageBox.Show("Az adatok feltöltése megtörtént!", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string Adat_módosítás(string beolvasott, int hossz)
        {
            string ideig = MyE.Beolvas(beolvasott).Trim();
            if (ideig.Length > hossz)
                ideig = ideig.Substring(0, hossz);
            return ideig;
        }

        private string Adat_módosítás(int első, int hossz, string szöveg)
        {
            string ideig = szöveg;
            if (szöveg.Length > (hossz + első))
                ideig = ideig.Substring(első, hossz);  //ha rövidebb a hossznál akkor vágunk
            else
                ideig = ideig.Substring(első, szöveg.Length - első);  // ha hosszabb a akkor csak annyit vágunk amennnyit lehet
            return ideig.Trim();
        }
        #endregion


        #region Költség lapfül
        private void ExcelKöltség_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla2.Rows.Count <= 0) throw new HibásBevittAdat("Nincs kijelölve sor!");
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Sérülés_költség_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Tábla2, true);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MyE.Megnyitás($"{fájlexc}.xlsx");
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Elkészült_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{KöltDátumtól.Value:yyyy}\sérülés{KöltDátumtól.Value:yyyy}.mdb";
                string szöveg = "SELECT * FROM jelentés";

                Kezelő_Sérülés_Jelentés Kéz = new Kezelő_Sérülés_Jelentés();
                List<Adat_Sérülés_Jelentés> Adatok = Kéz.Lista_Adatok(hely, Sérülésjelszó, szöveg);
                List<string> SzövegGY = new List<string>();

                for (int i = 0; i < Tábla2.Rows.Count; i++)
                {
                    if (Tábla2.Rows[i].Selected)
                    {
                        Adat_Sérülés_Jelentés Elem = (from a in Adatok
                                                      where a.Sorszám == Tábla2.Rows[i].Cells[0].Value.ToÉrt_Int()
                                                      select a).FirstOrDefault();

                        if (Elem != null)
                        {
                            szöveg = "UPDATE jelentés  SET ";
                            szöveg += " státus1=2 ";
                            szöveg += $" WHERE [sorszám]={Tábla2.Rows[i].Cells[0].Value.ToStrTrim()}";
                            SzövegGY.Add(szöveg);
                        }
                    }
                }
                if (SzövegGY.Count > 0) MyA.ABMódosítás(hely, Sérülésjelszó, SzövegGY);
                Elkészült.Visible = false;
                Költlekérdezés_Kiiró();
                KivalasztottSorszam = -1;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void KöltLekérdezés_Click(object sender, EventArgs e)
        {
            try
            {
                if (KöltDátumig.Value < KöltDátumtól.Value) throw new HibásBevittAdat("A kezdő dátumnak nagyobbnak kell lennie a befejező dátumnál.");
                Költlekérdezés_Kiiró();
                SapDátum.Value = KöltDátumtól.Value;
                Holtart.Ki();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Költlekérdezés_Kiiró()
        {
            try
            {
                DataTable AdatTábla = new DataTable();

                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("S.sz.");
                AdatTábla.Columns.Add("Telephely");
                AdatTábla.Columns.Add("Dátum");
                AdatTábla.Columns.Add("Idő");
                AdatTábla.Columns.Add("Helyszín");
                AdatTábla.Columns.Add("Psz.");
                AdatTábla.Columns.Add("Járművezető");
                AdatTábla.Columns.Add("Rendelésszám");
                AdatTábla.Columns.Add("M.Státus");
                AdatTábla.Columns.Add("K.Státus");
                AdatTábla.Columns.Add("R.Státus");
                AdatTábla.Columns.Add("Külső ár");

                AdatokKöltség_Feltöltés();
                AdatokJelentés_Feltöltés();
                AdatTábla.Clear();
                foreach (Adat_Sérülés_Jelentés rekord in AdatokSérülésJelentés)
                {
                    DataRow Soradat = AdatTábla.NewRow();

                    Soradat["S.sz."] = rekord.Sorszám;
                    Soradat["Telephely"] = rekord.Telephely.ToStrTrim();
                    Soradat["Dátum"] = rekord.Dátum.ToString("yyyy.MM.dd");
                    Soradat["Idő"] = rekord.Dátum.ToString("HH:mm:ss");
                    Soradat["Helyszín"] = rekord.Balesethelyszín.ToStrTrim();
                    Soradat["Psz."] = rekord.Rendszám.ToStrTrim();
                    Soradat["Járművezető"] = rekord.Járművezető.ToStrTrim();
                    Soradat["Rendelésszám"] = rekord.Rendelésszám.ToStrTrim();

                    switch (rekord.Státus)
                    {
                        case 1:
                            Soradat["M.Státus"] = "Nyitott";
                            break;
                        case 2:
                            Soradat["M.Státus"] = "Elkészült";
                            break;
                    }
                    switch (rekord.Státus1)
                    {
                        case 1:
                            Soradat["K.Státus"] = "Nyitott";
                            break;
                        case 2:
                            Soradat["K.Státus"] = "Elkészült";
                            break;
                    }

                    if (rekord.Rendelésszám == 0)
                        Soradat["R.Státus"] = "-";
                    else
                    {
                        Soradat["R.Státus"] = "Nincs SAP";

                        Adat_Sérülés_Költség folyt = (from a in AdatokSérülésKöltség
                                                      where a.Rendelés == rekord.Rendelésszám
                                                      select a).FirstOrDefault();
                        if (folyt != null)
                        {
                            if (folyt.Státus.ToStrTrim() == "1")
                                Soradat["R.Státus"] = "MLZR";
                            else
                                Soradat["R.Státus"] = "Nyitott";
                            Soradat["Külső ár"] = folyt.Szolgáltatás;
                        }
                    }
                    AdatTábla.Rows.Add(Soradat);

                    Holtart.Lép();
                }
                Tábla2.DataSource = AdatTábla;

                Tábla2.Columns["S.sz."].Width = 50;
                Tábla2.Columns["Telephely"].Width = 100;
                Tábla2.Columns["Dátum"].Width = 100;
                Tábla2.Columns["Idő"].Width = 100;
                Tábla2.Columns["Helyszín"].Width = 200;
                Tábla2.Columns["Psz."].Width = 50;
                Tábla2.Columns["Járművezető"].Width = 200;
                Tábla2.Columns["Rendelésszám"].Width = 120;
                Tábla2.Columns["M.Státus"].Width = 90;
                Tábla2.Columns["K.Státus"].Width = 90;
                Tábla2.Columns["R.Státus"].Width = 90;
                Tábla2.Columns["Külső ár"].Width = 90;

                Tábla2.Visible = true;
                Tábla2.Refresh();
                Holtart.Ki();
                SAPfül();

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Tábla2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Tábla2.Rows.Count < 1) return;
                if (e.RowIndex < 0) return;

                // Lekérjük a sorszámot a kijelölt sorból
                KivalasztottSorszam = Tábla2.Rows[e.RowIndex].Cells[0].Value.ToÉrt_Int();
                Kiír(KöltDátumtól.Value);
                if (Tábla2.Columns[1].HeaderText == "Telephely")
                {
                    SAPfül();

                    if (Tábla2.Rows[e.RowIndex].Cells[9].Value.ToStrTrim() == "Elkészült")
                        Elkészült.Visible = false;
                    else
                        Elkészült.Visible = true;
                    // a telephelyhez tartozó tarifát kiírja
                    Cmbtelephely.Text = Tábla2.Rows[e.RowIndex].Cells[1].Value.ToStrTrim();
                    Tarifa_kiírása();
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Nullás_Click(object sender, EventArgs e)
        {
            Nullás_tábla();
        }

        private void AdatokKöltségNullás_Feltöltés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{KöltDátumtól.Value.Year}\sérülés{KöltDátumtól.Value.Year}.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.Tükörtáblák(hely);
                List<Adat_Sérülés_Jelentés> Adatok = KézSérülésJelentés.Lista_Adatok(hely, Sérülésjelszó, Jelentésszöveg);
                List<Adat_Sérülés_Jelentés> Ideig = DátumSzűr(Adatok, KöltDátumtól.Value, KöltDátumig.Value);
                Ideig = RendszámSzűr(Ideig, KöltRendszám.Text.Trim());
                Ideig = TelephelySzűr(Ideig, Telephely_Költ, KöltTelephely.Text.Trim());
                Ideig = StátusSzűrKölt(Ideig);
                Ideig = NullásSzűrő(Ideig);

                AdatokSérülésJelentés.Clear();
                AdatokSérülésJelentés.AddRange(Ideig);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Nullás_tábla()
        {
            try
            {
                DataTable AdatTábla = new DataTable();

                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("S.sz.       ");
                AdatTábla.Columns.Add("Dátum       ");
                AdatTábla.Columns.Add("pályaszám   ");
                AdatTábla.Columns.Add("viszonylat  ");
                AdatTábla.Columns.Add("telep       ");
                AdatTábla.Columns.Add("rövid szöveg");
                AdatTábla.Columns.Add("Járművezet  ");

                AdatokKöltségNullás_Feltöltés();
                AdatTábla.Clear();
                foreach (Adat_Sérülés_Jelentés rekord in AdatokSérülésJelentés)
                {
                    DataRow Soradat = AdatTábla.NewRow();

                    Soradat["S.sz.       "] = rekord.Sorszám;
                    Soradat["Dátum       "] = rekord.Dátum.ToString("yyyy.MM.dd");
                    Soradat["pályaszám   "] = rekord.Rendszám;
                    Soradat["viszonylat  "] = rekord.Viszonylat;
                    Soradat["telep       "] = rekord.Telephely;
                    Soradat["rövid szöveg"] = rekord.Mivelütközött.ToStrTrim() != "_" ? $"Ütközött {rekord.Mivelütközött}" : $"{rekord.Esemény} {rekord.Balesethelyszín.Trim()}";
                    Soradat["Járművezet  "] = rekord.Járművezető;

                    AdatTábla.Rows.Add(Soradat);

                    Holtart.Lép();
                }
                Tábla2.DataSource = AdatTábla;

                Tábla2.Columns["S.sz.       "].Width = 50;
                Tábla2.Columns["Dátum       "].Width = 100;
                Tábla2.Columns["pályaszám   "].Width = 100;
                Tábla2.Columns["viszonylat  "].Width = 100;
                Tábla2.Columns["telep       "].Width = 200;
                Tábla2.Columns["rövid szöveg"].Width = 400;
                Tábla2.Columns["Járművezet  "].Width = 200;

                Tábla2.Refresh();
                Tábla2.Visible = true;
                Holtart.Ki();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExcelNullás_Click(object sender, EventArgs e)
        {
            try
            {
                Nullás_tábla();
                // ha üres a tábla akkor kilép
                if (Tábla2.Rows.Count < 1) throw new HibásBevittAdat("Nincs kijelölve sor!");

                string fájlexc;
                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Nullás sérülés jelentés készítés",
                    FileName = $"Nullás_Sérülések_{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Tábla2, true);

                MyE.ExcelMegnyitás(fájlexc);

                int utolsóoszlop = MyE.Utolsóoszlop("Munka1");
                int utolsósor = MyE.Utolsósor("Munka1");
                // oszlopszélesség
                MyE.Munkalap_betű("Arial", 10);
                MyE.Oszlopszélesség("Munka1", "A:D", 10);
                MyE.Oszlopszélesség("Munka1", "e:e", 9);
                MyE.Oszlopszélesség("Munka1", "f:f", 33);
                MyE.Oszlopszélesség("Munka1", "g:g", 20);
                MyE.Oszlopszélesség("Munka1", "h:h", 15);
                MyE.Sormagasság("1:1", 25);
                MyE.Sormagasság($"2:{utolsósor}", 18);
                MyE.Betű("1:1", false, false, true);

                // egész rácsoz és vastagkeret
                MyE.Rácsoz("B1:" + MyE.Oszlopnév(utolsóoszlop) + utolsósor.ToStrTrim());
                MyE.Vastagkeret($"B1:G{utolsósor}");

                // nyomtatási terület
                MyE.NyomtatásiTerület_részletes("Munka1", "b1:" + MyE.Oszlopnév(utolsóoszlop) + utolsósor.ToStrTrim(), "", "", false);
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
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CsoportkijelölMind_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla2.Rows.Count < 1) throw new HibásBevittAdat("Nincs sor a táblázatban!");

                for (int i = 0; i < Tábla2.Rows.Count; i++)
                    Tábla2.Rows[i].Selected = true;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CsoportVissza_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla2.Rows.Count < 1)
                    throw new HibásBevittAdat("Nincs sor a táblázatban!");
                Tábla2.ClearSelection();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NyomtatványKitöltés_Click(object sender, EventArgs e)
        {
            try
            {
                if (SapSorszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva sorszám!");
                string fájlexc;
                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Költéskimutatás készítés"
                };
                string szöveg = $"{SapSorszám.Text.Trim()}_{SapTelephely.Text.Trim()}_{SapPályaszám.Text.Trim()}_" +
                                    $"{SapRendelés.Text.Trim()}_{SapDátum.Value:yyyyMMdd}_{DateTime.Now:yyyyMMddHHmmss}";
                SaveFileDialog1.FileName = szöveg;
                SaveFileDialog1.Filter = "Excel |*.xlsx";
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                Holtart.Be();
                MyE.ExcelLétrehozás();

                int szolgsor, sor, elsősor, anyagsor;
                string szöveg2;
                double anyagegység;
                DataGridViewRow sorIndex = Tábla2.Rows.Cast<DataGridViewRow>()
                         .FirstOrDefault(row => row.Cells[0].Value.ToÉrt_Int() == KivalasztottSorszam)
                         ?? throw new HibásBevittAdat("A kiválasztott sorszám nem található a táblázatban!");

                int Sor = sorIndex.Index;

                // megformázzuk
                MyE.Munkalap_betű("Arial", 14);

                // oszlopszélesség

                MyE.Oszlopszélesség("Munka1", "a:a", 50);
                MyE.Oszlopszélesség("Munka1", "b:b", 34);
                MyE.Oszlopszélesség("Munka1", "c:c", 16);
                MyE.Oszlopszélesség("Munka1", "d:d", 17);
                MyE.Oszlopszélesség("Munka1", "e:e", 22);
                MyE.Oszlopszélesség("Munka1", "f:f", 28);
                MyE.Oszlopszélesség("Munka1", "g:g", 24);

                Holtart.Lép();
                // 1 SOR
                szolgsor = 0;
                sor = 1;
                Rendelésadatokmunkaidő_listázása();
                if (Tábla1.Rows.Count == 1)
                {
                    MyE.Kiir("34/VU/2020. 2.sz. melléklet", $"g{sor}");
                    MyE.Betű($"g{sor}", false, false, true);
                }
                else
                {
                    if (Tábla1.Rows[Tábla1.Rows.Count - 2].Cells[2].Value.ToStrTrim() != "D03")
                    {
                        MyE.Kiir("34/VU/2020. 1.sz. melléklet", $"g{sor}");
                        MyE.Betű($"g{sor}", false, false, true);
                    }
                    else
                    {
                        MyE.Kiir("34/VU/2020. 2.sz. melléklet", $"g{sor}");
                        MyE.Betű($"g{sor}", false, false, true);
                    }
                }
                sor++;
                MyE.Sormagasság($"{sor}:{sor + 2}", 30);
                MyE.Egyesít("Munka1", $"a{sor}:g{sor}");
                MyE.Betű($"a{sor}", 22);
                MyE.Betű($"a{sor}", false, false, true);
                MyE.Kiir("KÖLTSÉGKIMUTATÁS", $"a{sor}");

                // 2 SOR
                sor++;
                MyE.Egyesít("Munka1", $"a{sor}:g{sor}");
                MyE.Betű($"a{sor}", 20);
                MyE.Betű($"a{sor}", false, false, true);
                if (Tábla1.Rows.Count == 1)
                {
                    MyE.Kiir("KÁRESEMÉNY SAJÁT KIVITELEZÉSBEN ÉS/VAGY SZOLGÁLTATÁS ", $"a{sor}");
                    sor++;
                    MyE.Kiir("IGÉNYBEVÉTELÉVEL ELVÉGZETT HELYREÁLLÍTÁSÁRÓL", $"a{sor}");
                    MyE.Egyesít("Munka1", $"a{sor}:g{sor}");
                    MyE.Betű($"a{sor}", 20);
                    MyE.Betű($"a{sor}", false, false, true);
                    sor++;
                    MyE.Egyesít("Munka1", $"a{sor}" + $":f{sor}");
                    MyE.Betű($"a{sor}", 20);
                    MyE.Kiir("Munkavállalói közvetlen kártérítés", $"a{sor}");
                    MyE.Egyesít("Munka1", $"a{sor}:g{sor}");
                    MyE.Betű($"a{sor}", 12);
                    MyE.Betű($"a{sor}", false, false, true);
                }
                else
                {
                    if (Tábla1.Rows[Tábla1.Rows.Count - 2].Cells[2].Value == null || Tábla1.Rows[Tábla1.Rows.Count - 2].Cells[2].Value.ToStrTrim() != "D03")
                        MyE.Kiir("KÁRESEMÉNY SAJÁT KIVITELEZÉSBEN ELVÉGZETT HELYREÁLLÍTÁSÁRÓL", $"a{sor}");
                    else
                    {
                        MyE.Kiir("KÁRESEMÉNY SAJÁT KIVITELEZÉSBEN ÉS/VAGY SZOLGÁLTATÁS ", $"a{sor}");
                        sor++;
                        MyE.Kiir("IGÉNYBEVÉTELÉVEL ELVÉGZETT HELYREÁLLÍTÁSÁRÓL", $"a{sor}");
                        MyE.Egyesít("Munka1", $"a{sor}:g{sor}");
                        MyE.Betű($"a{sor}", 20);
                        MyE.Betű($"a{sor}", false, false, true);
                        sor++;
                        MyE.Egyesít("Munka1", $"a{sor}" + $":f{sor}");
                        MyE.Betű($"a{sor}", 20);
                        MyE.Kiir("Munkavállalói közvetlen kártérítés", $"a{sor}");
                        MyE.Egyesít("Munka1", $"a{sor}:g{sor}");
                        MyE.Betű($"a{sor}", 12);
                        MyE.Betű($"a{sor}", false, false, true);
                    }
                }

                // 4 sor
                sor++;
                MyE.Sormagasság($"{sor}:{sor + 5}", 25);
                MyE.Egyesít("Munka1", $"d{sor}" + $":e{sor}");
                MyE.Egyesít("Munka1", $"f{sor}" + $":g{sor}");
                MyE.Betű($"a{sor}", 14);
                MyE.Kiir("Iktatószám:", $"d{sor}");
                MyE.Kiir(Iktatószám.Text.Trim(), $"f{sor}");
                MyE.Igazít_vízszintes($"d{sor}", "bal");
                MyE.Igazít_vízszintes($"f{sor}", "jobb");
                MyE.Keret($"d{sor}" + $":g{sor}", false, false, true, false);

                // 5 sor
                sor++;
                MyE.Egyesít("Munka1", $"d{sor}" + $":e{sor}");
                MyE.Egyesít("Munka1", $"f{sor}" + $":g{sor}");
                MyE.Betű($"a{sor}", 14);
                MyE.Kiir("Bizonylatot kiállította:", $"d{sor}");
                MyE.Kiir(Kiállította.Text.Trim(), $"f{sor}");
                MyE.Igazít_vízszintes($"d{sor}", "bal");
                MyE.Igazít_vízszintes($"f{sor}", "jobb");
                MyE.Keret($"d{sor}" + $":g{sor}", false, false, true, false);

                // 6 sor
                sor++;
                MyE.Egyesít("Munka1", $"d{sor}" + $":e{sor}");
                MyE.Egyesít("Munka1", $"f{sor}" + $":g{sor}");
                MyE.Betű($"a{sor}", 14);
                MyE.Kiir("Telefonszám:", $"d{sor}");
                MyE.Kiir(Telefonszám.Text.Trim(), $"f{sor}");
                MyE.Igazít_vízszintes($"d{sor}", "bal");
                MyE.Igazít_vízszintes($"f{sor}", "jobb");
                MyE.Keret($"d{sor}" + $":g{sor}", false, false, true, false);

                // 7 sor
                sor++;
                MyE.Egyesít("Munka1", $"d{sor}" + $":e{sor}");
                MyE.Egyesít("Munka1", $"f{sor}" + $":g{sor}");
                MyE.Betű($"a{sor}", 14);
                if (ChckBxDigitális.Checked)
                {
                    MyE.Kiir("Kiállítás dátuma:", $"d{sor}");
                    MyE.Kiir("időbélyegző szerinti időpontban", $"f{sor}");
                }
                else
                {
                    MyE.Kiir("Kiállítás dátuma:", $"d{sor}");
                    MyE.Kiir($"{DateTime.Today:yyyy.MM.dd}", $"f{sor}");
                }
                MyE.Igazít_vízszintes($"d{sor}", "bal");
                MyE.Igazít_vízszintes($"f{sor}", "jobb");
                MyE.Keret($"d{sor}" + $":g{sor}", false, false, true, false);

                // 8 sor
                sor++;
                MyE.Egyesít("Munka1", $"d{sor}" + $":e{sor}");
                MyE.Egyesít("Munka1", $"f{sor}" + $":g{sor}");
                MyE.Betű($"a{sor}", 14);
                MyE.Kiir("Mellékletek száma:", $"d{sor}");
                MyE.Kiir("-", $"f{sor}");
                MyE.Igazít_vízszintes($"d{sor}", "bal");
                MyE.Igazít_vízszintes($"f{sor}", "jobb");
                MyE.Keret($"d{sor}" + $":g{sor}", false, false, true, false);

                // 9 sor
                sor++;
                MyE.Sormagasság($"{sor}:{sor + 6}", 40);
                MyE.Betű($"a{sor}", 18);
                MyE.Betű($"a{sor}", false, false, true);
                MyE.Kiir("Káresemény azonosító adatai:", $"a{sor}");
                MyE.Igazít_vízszintes($"a{sor}", "bal");
                MyE.Betű($"a{sor}", true, false, true);

                // 10 sor
                sor++;
                MyE.Kiir("Helyreállított eszköz / eszközök:", $"a{sor}");
                MyE.Igazít_vízszintes($"a{sor}", "bal");
                MyE.Betű($"a{sor}", false, false, true);
                MyE.Egyesít("Munka1", $"b{sor}" + $":g{sor}");
                MyE.Kiir(Eszköz.Text.Trim(), $"b{sor}" + $":g{sor}");
                MyE.Igazít_vízszintes($"b{sor}" + $":g{sor}", "bal");
                MyE.Keret($"a{sor}" + $":g{sor}", false, false, true, false);

                // 11 sor
                sor++;
                MyE.Kiir("Helyreállított eszköz / eszközök azonosítója:", $"a{sor}");
                MyE.Igazít_vízszintes($"a{sor}", "bal");
                MyE.Betű($"a{sor}", false, false, true);
                MyE.Sortörésseltöbbsorba($"a{sor}");
                MyE.Egyesít("Munka1", $"b{sor}" + $":g{sor}");
                MyE.Kiir(Pályaszám.Text.Trim(), $"b{sor}" + $":g{sor}");
                MyE.Igazít_vízszintes($"b{sor}" + $":g{sor}", "bal");
                MyE.Keret($"a{sor}" + $":g{sor}", false, false, true, false);

                // 12 sor
                sor++;
                MyE.Kiir("Káresemény helyszíne:", $"a{sor}");
                MyE.Igazít_vízszintes($"a{sor}", "bal");
                MyE.Betű($"a{sor}", false, false, true);
                MyE.Egyesít("Munka1", $"b{sor}" + $":g{sor}");
                MyE.Kiir(Helyszín.Text, $"b{sor}" + $":g{sor}");
                MyE.Igazít_vízszintes($"b{sor}" + $":g{sor}", "bal");
                MyE.Keret($"a{sor}" + $":g{sor}", false, false, true, false);

                // 13 sor
                sor++;
                MyE.Kiir("Káresemény ideje:", $"a{sor}");
                MyE.Igazít_vízszintes($"a{sor}", "bal");
                MyE.Betű($"a{sor}", false, false, true);
                MyE.Egyesít("Munka1", $"b{sor}" + $":g{sor}");
                MyE.Kiir($"{Dátum.Value:yyyy.MM.dd} {Idő.Value:hh:mm}", $"b{sor}" + $":g{sor}");
                MyE.Igazít_vízszintes($"b{sor}" + $":g{sor}", "bal");
                MyE.Keret($"a{sor}" + $":g{sor}", false, false, true, false);

                // 14 sor
                sor++;
                MyE.Sormagasság($"{sor}:{sor}", 65);
                MyE.Kiir("Helyreállítást végző szolgálat:", $"a{sor}");
                MyE.Igazít_vízszintes($"a{sor}", "bal");
                MyE.Betű($"a{sor}", false, false, true);
                MyE.Egyesít("Munka1", $"b{sor}" + $":g{sor}");
                szöveg2 = $"{Költséghely.Text.Trim()}, ";
                szöveg2 += $"{Text1.Text.Trim()}, ";
                szöveg2 += $"{Text2.Text.Trim()}, ";
                szöveg2 += $"{Text3.Text.Trim()}, ";
                szöveg2 += $"{Text4.Text.Trim()}, ";
                szöveg2 += $"{Telephely.Text.Trim()} ";
                szöveg2 += Text5.Text.Trim();
                MyE.Kiir(szöveg2, $"b{sor}:g{sor}");
                MyE.Igazít_vízszintes($"b{sor}:g{sor}", "bal");
                MyE.Sortörésseltöbbsorba_egyesített($"b{sor}:g{sor}");
                MyE.Keret($"a{sor}:g{sor}", false, false, true, false);

                // 15 sor
                sor++;
                MyE.Kiir("Helyreállítás munkaszáma SAP-ban:", $"a{sor}");
                MyE.Igazít_vízszintes($"a{sor}", "bal");
                MyE.Betű($"a{sor}", false, false, true);
                MyE.Egyesít("Munka1", $"b{sor}:g{sor}");
                MyE.Kiir(Rendelésszám.Text, $"b{sor}:g{sor}");
                MyE.Igazít_vízszintes($"b{sor}:g{sor}", "bal");
                MyE.Keret($"a{sor}:g{sor}", false, false, true, false);

                // 16 sor
                sor++;
                MyE.Sormagasság($"{sor}:{sor + 3}", 25);

                // 17 sor
                sor++;
                MyE.Kiir("Kárhelyreállítás költségeinek kimutatása:", $"a{sor}");
                MyE.Betű($"a{sor}", 18);
                MyE.Igazít_vízszintes($"a{sor}", "bal");
                MyE.Betű($"a{sor}", true, false, true);
                sor++;

                // 19 sor
                Holtart.Lép();
                sor++;
                MyE.Kiir("Anyagfelhasználás", $"a{sor}");
                MyE.Betű($"a{sor}", 16);
                MyE.Igazít_vízszintes($"a{sor}", "bal");
                MyE.Betű($"a{sor}", false, false, true);

                // 20 sor
                sor++;
                MyE.Sormagasság($"{sor}:{sor}", 72);
                MyE.Kiir("Felhasznált anyag megnevezése", $"a{sor}");
                MyE.Kiir("Felhasznált anyag       állapota         SAP-ban (SARZS)", $"b{sor}");
                MyE.Kiir("Felhasznált anyag cikkszáma SAP-ban", $"c{sor}");
                MyE.Kiir("Felhasznált mennyiség", $"d{sor}");
                MyE.Kiir("Felhasználás mennyiségi egysége", $"e{sor}");
                MyE.Kiir("Egységár (Forint / mennyiségi egység)", $"f{sor}");
                MyE.Kiir("Költsége (Forint)", $"g{sor}");
                MyE.Betű($"a{sor}:g{sor}", false, false, true);
                MyE.Sortörésseltöbbsorba($"a{sor}:g{sor}");
                MyE.Igazít_vízszintes($"a{sor}:g{sor}", "közép");
                MyE.Rácsoz($"a{sor}:g{sor}");

                // ***********************************************
                // Magyarázó sor
                sor++;
                MyE.Sormagasság($"{sor}:{sor}", 72);
                MyE.Kiir("Anyag megnevezése SAP-ban", $"a{sor}");
                MyE.Kiir($"01 – gyári\n02 – javított\n03 – selejt/javításra vár", $"b{sor}");
                MyE.Kiir("Cikkszám SAP-ban", $"c{sor}");
                MyE.Kiir("Javításhoz vételezett mennyiség", $"d{sor}");
                MyE.Kiir("Vételezés mennyiségi egysége", $"e{sor}");
                MyE.Kiir("Anyag SAP átlagára a vételezéskor", $"f{sor}");
                MyE.Kiir("Anyag-felhasználás költsége", $"g{sor}");
                MyE.Betű($"a{sor}:g{sor}", false, true, false);
                MyE.Sortörésseltöbbsorba($"a{sor}:g{sor}");
                MyE.Igazít_vízszintes($"a{sor}:g{sor}", "közép");
                MyE.Rácsoz($"a{sor}:g{sor}");

                // ***********************************************
                // Anyag részletesen
                elsősor = sor;
                Rendelésadatokanyag_listázás();

                if (Tábla1.Columns[0].HeaderText.Trim() == "Cikkszám")
                {
                    for (int i = 0; i < Tábla1.Rows.Count - 1; i++)
                    {
                        sor++;
                        MyE.Igazít_vízszintes($"b{sor}:d{sor}", "közép");
                        MyE.Igazít_vízszintes($"e{sor}:f{sor}", "jobb");
                        MyE.Kiir(Tábla1.Rows[i].Cells[1].Value.ToStrTrim(), $"a{sor}");
                        MyE.Betű($"B{sor}", "", "0#");
                        MyE.Kiir(Tábla1.Rows[i].Cells[6].Value.ToStrTrim(), $"b{sor}");
                        MyE.Kiir(Tábla1.Rows[i].Cells[0].Value.ToStrTrim(), $"c{sor}");
                        MyE.Kiir(Tábla1.Rows[i].Cells[2].Value.ToStrTrim(), $"d{sor}");
                        MyE.Kiir(Tábla1.Rows[i].Cells[3].Value.ToStrTrim(), $"e{sor}");
                        double anyagköltség = 0;
                        if (Tábla1.Rows[i].Cells[2].Value.ToStrTrim() != "")
                        {

                            if (double.TryParse(Tábla1.Rows[i].Cells[2].Value.ToStrTrim(), out double result) == true && result != 0)
                            {
                                if (!double.TryParse(Tábla1.Rows[i].Cells[5].Value.ToStrTrim(), out anyagköltség)) anyagköltség = 0;
                                if (!double.TryParse(Tábla1.Rows[i].Cells[2].Value.ToStrTrim(), out double anyagdarab)) anyagdarab = 1;
                                anyagegység = Math.Round(anyagköltség / anyagdarab, 2);
                                MyE.Kiir($"{anyagegység}", $"f{sor}");
                                MyE.Betű($"f{sor}", "Comma", $@"_-* #,###_-;-* #,###_-;_-* ""-""_-;_-@_-");

                            }
                        }
                        MyE.Kiir(anyagköltség.ToString(), $"g{sor}");
                        MyE.Betű($"g{sor}", "Comma", "#,###");
                    }
                    MyE.Rácsoz($"a{elsősor}:g{sor}");
                    MyE.Vastagkeret($"a{elsősor}:g{sor}");

                }
                Holtart.Lép();

                // 22 sor
                sor++;
                MyE.Egyesít("Munka1", $"a{sor}:g{sor}");
                MyE.Sormagasság($"{sor}:{sor}", 57);
                MyE.Kiir("Cikkszámok szerint fel kell sorolni a káresemény helyreállítása során felhasznált anyagokat. A helyreállítás során visszanyert," +
                    " cikkszámmal rendelkező hulladékokat negatív felhasználási mennyiségként kell feltüntetni, csökkentve az anyagfelhasználás költségét.",
                    $"a{sor}:g{sor}");
                MyE.Igazít_vízszintes($"a{sor}:g{sor}", "közép");
                MyE.Betű($"a{sor}", false, true, true);
                MyE.Vastagkeret($"a{sor}");
                MyE.Sortörésseltöbbsorba_egyesített($"a{sor}");

                // 23 sor
                sor++;
                anyagsor = sor;
                MyE.Sormagasság($"{sor}:{sor + 2}", 20);
                MyE.Egyesít("Munka1", $"a{sor}:f{sor}");
                MyE.Kiir("Helyreállításhoz felhasznált anyagok költsége összesen:", $"a{sor}");
                MyE.Kiir("=sum(R[" + (elsősor - sor).ToStrTrim() + "]C:R[-2]C", $"g{sor}");
                MyE.Betű($"g{sor}", "Comma", "#,###");
                MyE.Igazít_vízszintes($"a{sor}", "bal");
                MyE.Betű($"a{sor}", 16);
                MyE.Rácsoz($"a{sor}:g{sor}");
                MyE.Betű($"a{sor}", false, false, true);

                if (!Chck_Egyszerüsített.Checked)
                {
                    // 25 sor
                    sor++;
                    sor++;
                    MyE.Kiir("Közvetlen gépköltség", $"a{sor}");
                    MyE.Betű($"a{sor}", 16);
                    MyE.Igazít_vízszintes($"a{sor}", "bal");
                    MyE.Betű($"a{sor}", false, false, true);
                    // 26 sor
                    sor++;
                    MyE.Sormagasság($"{sor}:{sor}", 54);
                    MyE.Kiir("Elvégzett fő munkafolyamat megnevezése", $"a{sor}");
                    MyE.Kiir("Végrehajtás időtartama (órában)", $"e{sor}");
                    MyE.Kiir("Óradíj (Forint/óra)", $"f{sor}");
                    MyE.Kiir("Munkadíj (Forint)", $"g{sor}");
                    MyE.Betű($"a{sor}:g{sor}", false, false, true);
                    MyE.Sortörésseltöbbsorba($"a{sor}:g{sor}");
                    MyE.Igazít_vízszintes($"a{sor}:g{sor}", "közép");
                    MyE.Egyesít("Munka1", $"a{sor}:d{sor}");
                    // 27 sor
                    sor++;
                    MyE.Egyesít("Munka1", $"a{sor}:d{sor}");
                    MyE.Kiir("-", $"a{sor}");
                    MyE.Kiir("-", $"d{sor}");
                    MyE.Kiir("-", $"e{sor}");
                    MyE.Kiir("-", $"f{sor}");
                    MyE.Kiir("-", $"g{sor}");
                    MyE.Rácsoz($"a{sor}:g{sor}");
                    // 28 sor
                    sor++;
                    MyE.Egyesít("Munka1", $"a{sor}:g{sor}");
                    MyE.Sormagasság($"{sor}:{sor}", 50);
                    MyE.Kiir("A kár helyreállítása érdekében felhasznált gépi teljesítmény munkafolyamatonként fel kell sorolni. A PM modulban ennek megfelelően kell a munkaidő nyilvántartásokat vezetni.", $"a{sor}");
                    MyE.Betű($"a{sor}", false, true, true);
                    MyE.Vastagkeret($"a{sor}");
                    MyE.Sortörésseltöbbsorba($"a{sor}");
                    MyE.Egyesít("Munka1", $"a{sor}:g{sor}");
                    // 29 sor
                    sor++;
                    MyE.Sormagasság($"{sor}:{sor + 2}", 20);
                    MyE.Egyesít("Munka1", $"a{sor}:f{sor}");
                    MyE.Kiir("Helyreállítás közvetlen gépköltsége összesen:", $"a{sor}");
                    MyE.Kiir("-", $"g{sor}");
                    MyE.Betű($"g{sor}", "Comma", "#,###");
                    MyE.Igazít_vízszintes($"a{sor}", "bal");
                    MyE.Betű($"a{sor}", 16);
                    MyE.Betű($"a{sor}", false, false, true);
                    MyE.Rácsoz($"a{sor}:g{sor}");
                }
                if (Tábla2.Rows[Sor].Cells[11].Value.ToStrTrim() != "0")
                {
                    // 31 sor
                    sor += 2;

                    MyE.Kiir("Igénybe vett szolgáltatások", $"a{sor}");
                    MyE.Betű($"a{sor}", 16);
                    MyE.Egyesít("Munka1", $"a{sor}:e{sor}");
                    MyE.Igazít_vízszintes($"a{sor}", "bal");
                    MyE.Betű($"a{sor}", false, false, true);
                    // 32 sor
                    sor++;
                    MyE.Sormagasság($"{sor}:{sor}", 108);
                    MyE.Kiir("Igénybe vett szolgáltatások megnevezése", $"a{sor}");
                    MyE.Kiir("Mellékelt számla (vagy SAP bizonylat megnevezése és) sorszáma", $"f{sor}");
                    MyE.Kiir("Számla nettó értéke (SAP-ban kimutatható költség) (Forint)", $"g{sor}");
                    MyE.Betű($"a{sor}:g{sor}", false, false, true);
                    MyE.Rácsoz($"a{sor}:g{sor}");
                    MyE.Sortörésseltöbbsorba($"a{sor}:g{sor}");
                    MyE.Igazít_vízszintes($"a{sor}:g{sor}", "közép");
                    // 33 sor
                    sor++;
                    MyE.Egyesít("Munka1", $"a{sor}:e{sor}");
                    MyE.Kiir("-", $"a{sor}");
                    MyE.Kiir("-", $"e{sor}");
                    MyE.Kiir(Tábla2.Rows[Sor].Cells[11].Value.ToStrTrim(), $"g{sor}");
                    MyE.Betű($"g{sor}", "Comma", "#,###");
                    MyE.Rácsoz($"a{sor}:g{sor}");
                    MyE.Vastagkeret($"a{sor}:g{sor}");
                    // 34 sor
                    sor++;
                    MyE.Egyesít("Munka1", $"a{sor}:g{sor}");
                    MyE.Sormagasság($"{sor}:{sor}", 50);
                    MyE.Kiir("A kár helyreállításához igénybe vett külső szolgáltatásokat számlánként fel kell sorolni. A hivatkozott számlák másolatát a költségkimutatáshoz csatolni kell.", $"a{sor}");
                    MyE.Betű($"a{sor}", false, true, true);
                    MyE.Sortörésseltöbbsorba_egyesített($"a{sor}");
                    MyE.Vastagkeret($"a{sor}");
                    MyE.Egyesít("Munka1", $"a{sor}:g{sor}");
                    MyE.Igazít_vízszintes($"a{sor}", "közép");
                    // 35 sor
                    sor++;
                    szolgsor = sor;
                    MyE.Sormagasság($"{sor}:{sor + 2}", 20);
                    MyE.Egyesít("Munka1", $"a{sor}:f{sor}");
                    MyE.Kiir("Helyreállításhoz igénybe vett szolgáltatások összesen:", $"a{sor}");
                    MyE.Kiir(Tábla2.Rows[Sor].Cells[11].Value.ToStrTrim(), $"g{sor}");
                    MyE.Igazít_vízszintes($"a{sor}", "bal");
                    MyE.Betű($"a{sor}", 16);
                    MyE.Betű($"a{sor}", false, false, true);
                    MyE.Rácsoz($"a{sor}:g{sor}");
                }

                // 37 sor
                Holtart.Lép();
                sor += 2;
                MyE.Kiir("Munkadíj", $"a{sor}");
                MyE.Betű($"a{sor}", 16);
                MyE.Igazít_vízszintes($"a{sor}", "bal");
                MyE.Betű($"a{sor}", false, false, true);

                // 38 sor
                sor++;
                MyE.Sormagasság($"{sor}:{sor}", 54);

                MyE.Egyesít("Munka1", $"a{sor}:d{sor}");
                MyE.Kiir("Elvégzett fő munkafolyamat megnevezése", $"a{sor}:d{sor}");
                MyE.Kiir("Végrehajtás időtartama (órában)", $"e{sor}");
                MyE.Kiir("Tarifa (Forint/óra)", $"f{sor}");
                MyE.Kiir("Munkadíj (Forint)", $"g{sor}");
                MyE.Betű($"a{sor}:g{sor}", false, false, true);
                MyE.Sortörésseltöbbsorba($"a{sor}:g{sor}");
                MyE.Rácsoz($"a{sor}:g{sor}");
                MyE.Igazít_vízszintes($"a{sor}:g{sor}", "közép");
                MyE.Egyesít("Munka1", $"a{sor}:d{sor}");
                // ************************************
                // idő

                Rendelésadatokmunkaidő_listázása();
                elsősor = sor + 1;

                if (Tábla1.Columns[0].HeaderText.Trim() == "Művelet leírása")
                {
                    for (int i = 0; i < Tábla1.Rows.Count - 1; i++)
                    {
                        sor++;
                        MyE.Egyesít("Munka1", $"a{sor}:d{sor}");
                        MyE.Kiir(Tábla1.Rows[i].Cells[0].Value.ToStrTrim(), $"a{sor}");
                        MyE.Kiir($"={Tábla1.Rows[i].Cells[1].Value}/60", $"e{sor}");
                        MyE.Betű($"g{sor}", "Comma", "#,###");
                        MyE.Igazít_vízszintes($"e{sor}", "jobb");
                        if (Tábla1.Rows[i].Cells[2].Value.ToStrTrim() == "D60")
                            MyE.Kiir(ÉvestarifaD60.Text, $"f{sor}");
                        else
                            MyE.Kiir(ÉvestarifaD03.Text, $"f{sor}");

                        MyE.Kiir("=RC[-2]*RC[-1]", $"g{sor}");
                    }
                    MyE.Rácsoz($"a{elsősor}:g{sor}");
                    MyE.Vastagkeret($"a{elsősor}:g{sor}");
                }

                Holtart.Lép();
                // 40 sor
                sor++;
                MyE.Egyesít("Munka1", $"a{sor}:g{sor}");
                MyE.Sormagasság($"{sor}:{sor}", 50);
                MyE.Kiir("A kár helyreállítása érdekében elvégzett tevékenységeket fő munkafolyamatonként (például: x elemek cseréje, fényezés javítása) fel kell sorolni. A PM modulban ennek megfelelően kell a munkaidő nyilvántartásokat vezetni.", $"a{sor}");
                MyE.Betű($"a{sor}", false, true, true);
                MyE.Sortörésseltöbbsorba_egyesített($"a{sor}");
                MyE.Vastagkeret($"a{sor}");

                // 41 sor
                sor++;
                MyE.Sormagasság($"{sor}:{sor + 3}", 20);
                MyE.Egyesít("Munka1", $"a{sor}:f{sor}");
                MyE.Kiir("Helyreállítás munkadíja összesen:", $"a{sor}");
                MyE.Kiir("=sum(R[" + (elsősor - sor).ToStrTrim() + "]C:R[-2]C", $"g{sor}");
                MyE.Betű($"g{sor}", "Comma", "#,###");
                MyE.Igazít_vízszintes($"a{sor}", "bal");
                MyE.Betű($"a{sor}", 16);
                MyE.Betű($"a{sor}", false, false, true);
                MyE.Sortörésseltöbbsorba_egyesített($"a{sor}");
                MyE.Rácsoz($"a{sor}:g{sor}");

                // 45 sor
                sor += 2;
                MyE.Sormagasság($"{sor}:{sor}", 45);
                MyE.Egyesít("Munka1", $"a{sor}:f{sor}");
                MyE.Kiir("Helyreállítás nettó költsége összesen (Forint):", $"a{sor}");
                if (szolgsor == 0)
                    MyE.Kiir("=SUM(R[-2]C,R[" + (anyagsor - sor).ToStrTrim() + "]C)", $"g{sor}");
                else
                    MyE.Kiir("=SUM(R[-2]C,R[" + (anyagsor - sor).ToStrTrim() + "]C, R[" + (szolgsor - sor).ToStrTrim() + "]C)", $"g{sor}");

                MyE.Betű($"g{sor}", "Comma", "#,###");
                MyE.Igazít_vízszintes($"a{sor}", "bal");
                MyE.Betű($"a{sor}", 20);
                MyE.Betű($"a{sor}", false, false, true);
                MyE.Rácsoz($"a{sor}:g{sor}");
                if (!ChckBxDigitális.Checked)
                {
                    // 46 sor
                    sor++;
                    MyE.Sormagasság($"{sor}:{sor + 4}", 20);
                    // 47 sor
                    sor++;
                    MyE.Kiir("kiállítás dátuma:", $"a{sor}");
                    // 48 sor
                    sor++;
                    MyE.Kiir($"Budapest,{DateTime.Today:yyyy.MM.dd}", $"a{sor}");
                    MyE.Egyesít("Munka1", $"d{sor}:f{sor}");
                    MyE.Keret($"d{sor}:g{sor}", false, false, true, false);
                    // 49 sor
                    sor++;
                    MyE.Egyesít("Munka1", $"d{sor}:g{sor}");
                    MyE.Kiir(Text6.Text.Trim(), $"d{sor}");
                    // 50 sor
                    sor++;
                    MyE.Egyesít("Munka1", $"d{sor}:g{sor}");
                    MyE.Kiir(Text7.Text.Trim(), $"d{sor}");
                }
                else
                {
                    sor += 2;
                    MyE.Kiir("Kelt, az elektronikus aláírás időbélyegzője szerinti időpontban", $"a{sor}");
                    MyE.Betű($"a{sor}", 12);
                    MyE.Betű($"a{sor}", false, true, false);


                    sor += 3;
                    MyE.Sormagasság($"a{sor}", 80);
                    MyE.Keret($"b{sor}", false, false, true, false);
                    MyE.Keret($"f{sor}", false, false, true, false);

                    sor++;
                    MyE.Kiir(TxtBxDigitalisAlairo1.Text.Trim(), $"b{sor}");
                    MyE.Igazít_vízszintes($"b{sor}", "közép");
                    MyE.Kiir(TxtBxDigitalisAlairo2.Text.Trim(), $"f{sor}");
                    MyE.Igazít_vízszintes($"f{sor}", "közép");

                    sor++;
                    MyE.Kiir(TxtBxBeosztas1.Text.Trim(), $"b{sor}");
                    MyE.Igazít_vízszintes($"b{sor}", "közép");
                    MyE.Kiir(TxtBxBeosztas2.Text.Trim(), $"f{sor}");
                    MyE.Igazít_vízszintes($"f{sor}", "közép");

                }
                sor++;
                MyE.Kiir("Budapesti Közlekedési Zártkörűen Működő Részvénytársaság", $"c{sor}");
                MyE.Sormagasság($"a{sor}", 60);
                MyE.Egyesít("Munka1", $"b{sor}:f{sor}");
                MyE.Aktív_Cella("Munka1", "A1");

                // nyomtatási beállítások
                string helycsop = $@"{Application.StartupPath}\Főmérnökség\adatok\BKV.jpg";

                string jobbfejléc = "&\"Arial,Félkövér\"&20&EBudapesti Közlekedési Zártkörűen Működő Részvénytársaság&12" + '\n' + "&\"Arial,Normál\"&16&E 1980 Budapest Akácfa u. 15.  Telefon: 461-6500";
                MyE.NyomtatásiTerület_részletes("Munka1", $"A1:G{sor}", "", "", "&G", "", jobbfejléc, helycsop);

                MyE.ExcelMentés(fájlexc);

                MyE.ExcelBezárás();
                MyE.Megnyitás(fájlexc);

                Költlekérdezés_Kiiró();
                Holtart.Ki();

                MessageBox.Show("A nyomtatvány elkészült.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region Adatok rögzítése lapfül
        private void Üresrögzítő()
        {
            KmóraÁllás.Text = "";
            KépKeret.Visible = false;
            Üzembehelyezés.Text = "";
            Opt_Nyitott.Checked = true;
            Idegenhiba.Checked = true;
            Hosszú.Checked = true;
            Sorszám.Text = "";
            Pályaszám.Text = "";
            Típus.Text = "";
            Viszonylat.Text = "";
            Dátum.Value = DateTime.Today;
            Idő.Value = new DateTime(1900, 1, 1, 12, 0, 0);
            Szerelvény.Text = "";
            Forgalmiakadály.Text = "";
            Járművezető.Text = "";
            Műszakihiba.Checked = false;
            Anyagikár.Checked = false;
            Rendelésszám.Text = "0";
            Biztosító.Text = "";
            Helyszín.Text = "";
            Ütközött.Text = "";
            Telephely.Text = "";
            Személyi.Checked = false;
            AnyagikárÁr.Text = "0";
            Leírás.Text = "";
            Esemény.Text = "";
            Leírás1.Text = "";
            Költséghely.Text = "";
            Fényképek.Text = "0";
            Doksik.Text = "0";
        }


        public void Kiír(DateTime Melyikév)
        {
            try
            {
                if (KivalasztottSorszam == -1) return;

                Alap_mezők();
                Üresrögzítő();

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Melyikév:yyyy}\sérülés{Melyikév:yyyy}.mdb";
                if (!Exists(hely)) throw new HibásBevittAdat("A beállított dátumra nincs adatbázis létrehozva!");

                string szöveg = $"SELECT * FROM jelentés WHERE [sorszám]={KivalasztottSorszam}";

                Adat_Sérülés_Jelentés rekord = KézSérülésJelentés.Egy_Adat(hely, Sérülésjelszó, szöveg);

                Sorszám.Text = rekord.Sorszám.ToStrTrim();
                Telephely.Text = rekord.Telephely.ToStrTrim();
                Helyszín.Text = rekord.Balesethelyszín.ToStrTrim();
                Viszonylat.Text = rekord.Viszonylat.ToStrTrim();
                Pályaszám.Text = rekord.Rendszám.ToStrTrim();
                Járművezető.Text = rekord.Járművezető.ToStrTrim();
                Rendelésszám.Text = rekord.Rendelésszám.ToStrTrim();
                Típus.Text = rekord.Típus.ToStrTrim();
                Szerelvény.Text = rekord.Szerelvény.ToStrTrim();
                Forgalmiakadály.Text = rekord.Forgalmiakadály.ToStrTrim();
                Ütközött.Text = rekord.Mivelütközött.ToStrTrim();
                AnyagikárÁr.Text = rekord.Anyagikárft.ToStrTrim();
                Leírás.Text = rekord.Leírás.ToStrTrim();
                Leírás1.Text = rekord.Leírás1.ToStrTrim();
                Esemény.Text = rekord.Esemény.ToStrTrim();
                Biztosító.Text = rekord.Biztosító.ToStrTrim();

                if (rekord.Státus.ToStrTrim() == "1")
                {
                    Opt_Nyitott.Checked = true;
                    Rögzítjelentés.Visible = true;
                    Visszaállít.Visible = false;
                }
                if (rekord.Státus.ToStrTrim() == "2")
                {
                    Opt_Elkészült.Checked = true;
                    Rögzítjelentés.Visible = false;
                    Visszaállít.Visible = true;
                }
                if (rekord.Státus.ToStrTrim() == "9")
                {
                    Opt_Törölt.Checked = true;
                    Rögzítjelentés.Visible = false;
                    Visszaállít.Visible = true;
                }

                if (rekord.Kimenetel == 1)
                    Sajáthiba.Checked = true;
                if (rekord.Kimenetel == 2)
                    Idegenhiba.Checked = true;
                if (rekord.Kimenetel == 3)
                    Személyhiba.Checked = true;
                if (rekord.Kimenetel == 4)
                    Egyébhiba.Checked = true;

                Műszakihiba.Checked = rekord.Műszaki;
                Anyagikár.Checked = rekord.Anyagikár;
                Személyi.Checked = rekord.Személyisérülés;
                Dátum.Value = rekord.Dátum;
                Idő.Value = rekord.Dátum;

                if (rekord.Biztosítóidő == 1) Gyors.Checked = true;
                if (rekord.Biztosítóidő == 2) Hosszú.Checked = true;
                KmóraÁllás.Text = rekord.Kmóraállás.ToStrTrim();


                // költséghely
                List<Adat_Kiegészítő_Sérülés> AdatokSérülés = KézKiegSérülés.Lista_Adatok();
                AdatKiegSérülés = (from a in AdatokSérülés
                                   where a.Név == Telephely.Text.Trim()
                                   select a).FirstOrDefault();
                if (AdatKiegSérülés != null) Költséghely.Text = AdatKiegSérülés.Költséghely;

                // üzembehelyezés
                hely = $@"{Application.StartupPath}\Főmérnökség\adatok\villamos.mdb";
                string jelszó = "pozsgaii";
                szöveg = $"SELECT * FROM állománytábla WHERE [azonosító]='{Pályaszám.Text.Trim()}'";

                AdatJármű = KézJármű.Egy_Adat_fő(hely, jelszó, szöveg);
                if (AdatJármű != null)
                    Üzembehelyezés.Text = AdatJármű.Üzembehelyezés.ToString("yyyy.MM.dd");

                // fényképszámolás
                Képeklistázása();
                //pdfszámolás
                Pdflistázása();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void Képeklistázása()
        {
            try
            {
                FileBox.Items.Clear();
                Képdb = 0;
                // létrehozzuk a fénykép könyvtárat, ha még nincs
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value:yyyy}";
                if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);
                hely += @"\Képek";
                if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);
                if (!int.TryParse(Sorszám.Text, out FénySorszám2)) FénySorszám2 = 0;
                FénySorszám.Text = FénySorszám2.ToString();
                FényPályaszám2 = Pályaszám.Text;
                FényPályaszám.Text = FényPályaszám2;
                FényDátum.Value = Dátum.Value;
                FényIdő.Value = Idő.Value;

                DirectoryInfo di = new DirectoryInfo(hely);
                FileInfo[] aryFi = di.GetFiles("*.jpg");
                string szöveg = $"{Dátum.Value:yyyy}_{Sorszám.Text.Trim()}_{Pályaszám.Text.Trim()}";
                List<string> KépNevek = new List<string>();

                foreach (FileInfo fi in aryFi)
                    if (fi.Name.Contains(szöveg))
                        KépNevek.Add(fi.Name);
                KépNevek.Sort((x, y) =>
                {
                    int xSorszam = int.Parse(x.Split('_').Last().Replace(".jpg", ""));
                    int ySorszam = int.Parse(y.Split('_').Last().Replace(".jpg", ""));
                    return xSorszam.CompareTo(ySorszam);
                });

                foreach (string kepnev in KépNevek)
                    FileBox.Items.Add(kepnev);

                Képdb = FileBox.Items.Count;
                Fényképek.Text = Képdb.ToString();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void Pdflistázása()
        {
            try
            {
                FilePDF.Items.Clear();
                Doksikdb = 0;
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value:yyyy}";
                if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);
                hely += @"\PDF";
                if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

                PdfSorszám.Text = Sorszám.Text;
                PdfPályaszám.Text = Pályaszám.Text;
                PdfDátum.Value = Dátum.Value;
                PdfIdő.Value = Idő.Value;

                DirectoryInfo di = new DirectoryInfo(hely);
                FileInfo[] aryFi = di.GetFiles("*.pdf");
                string szöveg = $"{Dátum.Value:yyyy}_{Sorszám.Text.Trim()}_{Pályaszám.Text.Trim()}";
                List<string> FájlNevek = new List<string>();

                foreach (FileInfo fi in aryFi)
                    if (fi.Name.Contains(szöveg))
                        FájlNevek.Add(fi.Name);
                FájlNevek.Sort((x, y) =>
                {
                    int xSorszam = int.Parse(x.Split('_').Last().Replace(".pdf", ""));
                    int ySorszam = int.Parse(y.Split('_').Last().Replace(".pdf", ""));
                    return xSorszam.CompareTo(ySorszam);
                });
                foreach (string fajlnev in FájlNevek)
                    FilePDF.Items.Add(fajlnev);

                Doksikdb = FilePDF.Items.Count;
                Doksik.Text = Doksikdb.ToString();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void FékvizsgálatiExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Sorszám.Text.ToStrTrim() == "")
                    throw new HibásBevittAdat("Nincs kiválasztva sorszám!");
                string fájlexc;
                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Fékvizsgálati jelentés készítés",
                    FileName = $"Fékvizsgálati_{Dátum.Value:yyyyMMdd}_{Pályaszám.Text.ToStrTrim()}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                Holtart.Be();
                MyE.ExcelLétrehozás();

                // excel kitöltése

                // betű beállítása
                MyE.Munkalap_betű("Arial", 11);

                MyE.Oszlopszélesség("Munka1", "a:b", 13);
                MyE.Oszlopszélesség("Munka1", "c:g", 12);
                MyE.Oszlopszélesség("Munka1", "d:d", 14);
                // egyesítések
                for (int i = 1; i <= 5; i++)
                    MyE.Egyesít("Munka1", $"a{i}:d{i}");

                MyE.Egyesít("Munka1", "e2:g2");
                MyE.Egyesít("Munka1", "f7:g7");
                MyE.Egyesít("Munka1", "d9:f9");
                MyE.Egyesít("Munka1", "a11:b11");
                MyE.Egyesít("Munka1", "a13:b13");
                MyE.Egyesít("Munka1", "a15:b15");
                MyE.Egyesít("Munka1", "a19:g20");
                for (int i = 22; i <= 24; i++)
                    MyE.Egyesít("Munka1", $"a{i}:b{i}");
                MyE.Egyesít("Munka1", "a26:g27");
                MyE.Egyesít("Munka1", "a29:g29");
                for (int i = 30; i <= 33; i++)
                {
                    MyE.Egyesít("Munka1", $"a{i}:b{i}");
                    MyE.Egyesít("Munka1", $"c{i}:g{i}");
                }
                MyE.Egyesít("Munka1", "a34:b37");
                MyE.Egyesít("Munka1", "c34:g37");
                MyE.Egyesít("Munka1", "a38:b38");
                MyE.Egyesít("Munka1", "c38:g38");
                MyE.Egyesít("Munka1", "a39:b43");
                MyE.Egyesít("Munka1", "c39:g43");
                MyE.Egyesít("Munka1", "a45:g46");
                for (int i = 51; i <= 53; i++)
                    MyE.Egyesít("Munka1", $"e{i}:g{i}");
                Holtart.Lép();
                // fix kiírások
                MyE.Kiir("Fékvizsgálati jelentés", "e2");
                MyE.Betű("e2", false, false, true);
                MyE.Kiir("pályaszámú", "b7");
                MyE.Kiir("típusú villamos", "d7");
                MyE.Kiir("számú viszonylaton", "f7");
                MyE.Kiir(" -n", "b9");
                MyE.Kiir(" -kor történt eseményre vonatkozólag.", "d9");
                MyE.Kiir("Szerelvény pályaszámai:", "a11");
                MyE.Kiir("Üzembehelyezés dátuma", "e11");
                MyE.Kiir("Forgalmi akadály ideje:", "a13");
                MyE.Kiir("perc", "d13");
                MyE.Kiir("Járművezető neve:", "a15");
                MyE.Kiir("Járművezető nem hivatkozott műszaki hibára.", "a17");
                MyE.Kiir("Ha a járművezető nem hivatkozott műszaki hibára, akkor a jármű fékszerkezetét és működését az üzem területén átvizsgáltam és megállapítottam, hogy ", "a19");
                MyE.Sortörésseltöbbsorba_egyesített("A19:G20");
                Holtart.Lép();

                MyE.Kiir("Elektrodinamikus fék:", "a22");
                MyE.Kiir("Rögzítőfék :", "a23");
                MyE.Kiir("Sínfék :", "a24");
                MyE.Kiir("üzemképes", "c22");
                MyE.Kiir("üzemképes", "c23");
                MyE.Kiir("üzemképes", "c24");
                MyE.Kiir("Ha a járművezető műszaki hibára hivatkozott, akkor a járművet a Zavarelhárító Szolgálat szállíthatja az érintett üzembe. Gondoskodni kell a jármű esemény utáni állapotának megőrzéséről!", "a26");
                MyE.Sortörésseltöbbsorba_egyesített("A26:G27");

                MyE.Kiir("Az esemény leírása", "a29");
                MyE.Betű("a29", false, false, true);
                MyE.Kiir("Baleset helyszíne:", "a30");
                MyE.Kiir("Mivel ütközött:", "a31");
                MyE.Kiir("Személyi sérülés:", "a32");
                MyE.Kiir("Becsült anyagi kár:", "a33");
                MyE.Kiir("Jármű sérülésének leírása:", "a34");
                MyE.Sortörésseltöbbsorba_egyesített("A34:B37");
                MyE.Sortörésseltöbbsorba_egyesített("C34:G37");

                MyE.Kiir("Egyéb esemény:", "a38");
                MyE.Kiir("Egyéb esemény rövid leírása:", "a39");
                MyE.Sortörésseltöbbsorba_egyesített("A39:A43");


                MyE.Kiir("A fékvizsgálati jelentés a járművezető által kiállított 'Járművezetői jelentés közlekedési balesetről, eseményről' lap alapján készült.", "a45");
                MyE.Sortörésseltöbbsorba_egyesített("A45:B46");
                MyE.Sortörésseltöbbsorba_egyesített("C45:G46");

                MyE.Kiir("Budapest,", "a48");
                MyE.Kiir(DateTime.Today.ToStrTrim(), "b48");
                MyE.Kiir("aláírás", "e51");
                Holtart.Lép();
                // Változó adatok
                MyE.Kiir(Pályaszám.Text.ToStrTrim(), "a7");
                MyE.Betű("a7", false, false, true);
                MyE.Kiir(Típus.Text.ToStrTrim(), "c7");
                MyE.Betű("c7", false, false, true);
                MyE.Kiir(Viszonylat.Text.ToStrTrim(), "e7");
                MyE.Betű("e7", false, false, true);
                MyE.Kiir(Dátum.Value.ToString("yyyy.MM.dd"), "A9");
                MyE.Betű("a9", false, false, true);
                MyE.Kiir(Idő.Value.ToString("HH:mm"), "c9");
                MyE.Betű("c9", false, false, true);
                MyE.Kiir(Szerelvény.Text.ToStrTrim(), "c11");
                MyE.Kiir(Üzembehelyezés.Text.ToStrTrim(), "g11");
                MyE.Kiir(Forgalmiakadály.Text.ToStrTrim(), "c13");
                MyE.Kiir(Járművezető.Text.ToStrTrim(), "c15");
                MyE.Kiir(Helyszín.Text.ToStrTrim(), "c30");
                MyE.Kiir(Ütközött.Text.Trim(), "c31");
                if (!Személyi.Checked)
                    MyE.Kiir("Nem volt", "c32");
                else
                    MyE.Kiir("Volt", "c32");

                MyE.Kiir(AnyagikárÁr.Text.ToStrTrim(), "c33");
                MyE.Kiir(Leírás.Text.ToStrTrim(), "c34");
                MyE.Kiir(Esemény.Text.Trim(), "c38");
                MyE.Kiir(Leírás1.Text.ToStrTrim(), "c39");
                Holtart.Lép();

                MyE.Sortörésseltöbbsorba_egyesített("C39:G43");

                MyE.Rácsoz("a30:g43");
                MyE.Vastagkeret("a30:g43");
                MyE.Aláírásvonal("e51:g51");
                Holtart.Lép();
                // kiirjuk a készítő nevét és beosztását/
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Dolgozók.mdb";
                string jelszó = "forgalmiutasítás";
                string szöveg = $"SELECT * FROM dolgozóadatok WHERE [Bejelentkezésinév]='{Program.PostásNév.Trim()}'";
                Kezelő_Dolgozó_Alap Kéz = new Kezelő_Dolgozó_Alap();
                Adat_Dolgozó_Alap Adat = Kéz.Egy_Adat(hely, jelszó, szöveg);

                if (Adat != null)
                {
                    MyE.Kiir(Adat.DolgozóNév.Trim(), "e52");
                    MyE.Kiir(Adat.Főkönyvtitulus.Trim(), "e53");
                }

                Holtart.Lép();
                // kiírjuk a szervezetet
                MyE.Kiir(Text1.Text.Trim(), "a1");
                MyE.Igazít_vízszintes("a1", "bal");
                MyE.Kiir(Text2.Text.Trim(), "a2");
                MyE.Igazít_vízszintes("a2", "bal");
                MyE.Kiir(Text3.Text.Trim(), "a3");
                MyE.Igazít_vízszintes("a3", "bal");
                MyE.Kiir(Text4.Text.Trim(), "a4");
                MyE.Igazít_vízszintes("a4", "bal");
                MyE.Kiir($"{Telephely.Text.ToStrTrim()} {Text5.Text.Trim()}", "a5");
                MyE.Igazít_vízszintes("a5", "bal");

                // nyomtatási terület
                MyE.NyomtatásiTerület_részletes("Munka1", "a1:g53", "", "", true);
                Holtart.Ki();

                // bezárjuk az Excel-t
                MyE.Aktív_Cella("Munka1", "A1");

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
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void CAFExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Sorszám.Text.ToStrTrim() == "")
                    throw new HibásBevittAdat("Nincs kiválasztva sorszám!");
                Holtart.Be();
                Cafkiiró();

                string fájlexc;
                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "CAF jegyzőkönyv készítés",
                    FileName = $"CAF_{Dátum.Value:yyyyMMdd}_{Pályaszám.Text.ToStrTrim()}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                Holtart.Lép();
                MyE.ExcelLétrehozás();

                int sor;
                // formázáshoz

                // betű beállítása
                MyE.Munkalap_betű("calibri", 11);

                MyE.Oszlopszélesség("Munka1", "a:c", 26);
                sor = 1;
                MyE.Egyesít("Munka1", $"a{sor}:c{sor}");
                MyE.Betű("a1", 18);

                MyE.Kiir($"{Pályaszám.Text} pályaszám - sérülés utáni járműszemle", $"a{sor}");
                MyE.Háttérszín($"a{sor}", 13619151d);
                MyE.Igazít_vízszintes($"a{sor}", "közép");
                MyE.Vastagkeret($"a{sor}:c{sor}");
                sor++;

                MyE.Egyesít("Munka1", $"a{sor}:c{sor}");
                MyE.Kiir($"Járműszemle dátuma időpontja: {DateTime.Today}", $"a{sor}");
                MyE.Vastagkeret($"a{sor}:c{sor}");
                MyE.Igazít_vízszintes($"a{sor}", "bal");
                sor++;
                MyE.Egyesít("Munka1", $"a{sor}:c{sor}");
                MyE.Kiir("Jelen vannak", $"a{sor}");
                MyE.Vastagkeret($"a{sor}:c{sor}");

                MyE.Háttérszín($"a{sor}", 13619151d);

                sor++;
                MyE.Kiir("Szervezet", $"a{sor}");
                MyE.Kiir("Név", $"b{sor}");
                MyE.Kiir("Beosztás", $"c{sor}");
                MyE.Rácsoz($"a{sor}:c{sor}");
                MyE.Vastagkeret($"a{sor}:c{sor}");
                if (CafTábla.Rows.Count > 0)
                {
                    for (int i = 0; i < CafTábla.Rows.Count; i++)
                    {
                        sor++;
                        MyE.Kiir(CafTábla.Rows[i].Cells[1].Value.ToStrTrim(), $"a{sor}");
                        MyE.Kiir(CafTábla.Rows[i].Cells[3].Value.ToStrTrim(), $"c{sor}");
                        MyE.Kiir(CafTábla.Rows[i].Cells[2].Value.ToStrTrim(), $"b{sor}");
                    }
                    MyE.Rácsoz($"a{sor - CafTábla.Rows.Count - 1}:c{sor}");
                    MyE.Vastagkeret($"a{(sor - CafTábla.Rows.Count - 1)}:c{sor}");
                }
                sor++;
                MyE.Egyesít("Munka1", $"a{sor}:c{sor}");
                MyE.Kiir("Baleset / Rongálás bekövetkezésének időpontja.", $"a{sor}:c{sor}");
                MyE.Háttérszín($"a{sor}:c{sor}", 13619151d);

                sor++;
                MyE.Kiir("Dátum / idő", $"a{sor}");
                MyE.Kiir($"{Dátum.Value:yyyy.MM.dd}", $"b{sor}");
                MyE.Kiir($"{Idő.Value:HH:mm}", $"c{sor}");
                MyE.Rácsoz($"a{sor - 1}:c{sor}");
                MyE.Vastagkeret($"a{sor - 1}:c{sor}");
                sor++;
                MyE.Egyesít("Munka1", $"a{sor}:c{sor}");
                MyE.Kiir("Baleset / Rongálás bekövetkezésének időpontjában a jármű Km állása.", $"a{sor}");
                MyE.Vastagkeret($"a{sor - 1}:c{sor}");

                sor++;
                MyE.Kiir("KM állás", $"a{sor}");
                MyE.Kiir(KmóraÁllás.Text, $"b{sor}");
                MyE.Kiir("km", $"c{sor}");
                MyE.Rácsoz($"a{sor - 1}:c{sor}");
                MyE.Vastagkeret($"a{sor - 1}:c{sor}");
                sor++;
                MyE.Egyesít("Munka1", $"a{sor}:c{sor}");
                MyE.Kiir("Baleset/vagy rongálásal érintett kocsirészek (pl C1, S, stb)", $"a{sor}");
                MyE.Háttérszín($"a{sor}", 13619151d);
                sor++;
                MyE.Egyesít("Munka1", $"a{sor}:c{sor}");
                MyE.Kiir(Leírás.Text, $"a{sor}");
                MyE.Rácsoz($"a{sor - 1}:c{sor}");
                MyE.Vastagkeret($"a{sor - 1}:c{sor}");
                sor++;
                MyE.Egyesít("Munka1", $"a{sor}:c{sor}");
                MyE.Kiir("Biztosítói hibaszemle történt e? (megfelelő aláhúzandó)", $"a{sor}");
                MyE.Háttérszín($"a{sor}", 13619151d);
                sor++;
                MyE.Kiir("Igen", $"a{sor}");
                MyE.Kiir("Nem", $"b{sor}");
                if (Biztosító.Text.ToStrTrim() == "_")
                    MyE.Betű($"b{sor}", true, false, false);
                else
                    MyE.Betű($"a{sor}", true, false, false);

                MyE.Rácsoz($"a{sor - 1}:c{sor}");
                MyE.Vastagkeret($"a{sor - 1}:c{sor}");
                sor++;
                MyE.Egyesít("Munka1", $"a{sor}:c{sor}");
                MyE.Kiir("Baleset / Rongálás leírása (pontosan mely elemek sérültek, leírás szövegesen)." +
                    " Mindenképpen szükséges fotókat készíteni", $"a{sor}:c{sor}");
                MyE.Háttérszín($"a{sor}:c{sor}", 13619151d);
                MyE.Sormagasság($"{sor}:{sor}", 32);
                MyE.Sortörésseltöbbsorba_egyesített($"a{sor}:c{sor}");
                MyE.Vastagkeret($"a{sor - 6}:c{sor}");
                sor++;
                MyE.Egyesít("Munka1", $"a{sor}:c{sor + 5}");
                MyE.Kiir(Leírás.Text, $"b{sor}");
                MyE.Sortörésseltöbbsorba_egyesített($"a{sor}:c{sor + 6}");
                MyE.Vastagkeret($"a{sor}:c{sor + 6}");
                Holtart.Lép();
                sor += 6;
                MyE.Egyesít("Munka1", $"a{sor}:c{sor}");
                MyE.Kiir("A baleset / rongálás okozója", $"a{sor}");
                MyE.Háttérszín($"a{sor}", 13619151d);
                sor++;
                MyE.Egyesít("Munka1", $"a{sor}:c{sor + 1}");
                string szöveg = "";
                if (Sajáthiba.Checked)
                    szöveg = "Saját jármű";
                if (Idegenhiba.Checked)
                    szöveg = "Idegen jármű";
                MyE.Kiir(szöveg, $"a{sor}");
                sor++;
                MyE.Rácsoz($"a{sor - 2}:c{sor}");
                MyE.Vastagkeret($"a{sor - 2}:c{sor}");
                sor++;
                MyE.Egyesít("Munka1", $"a{sor}:c{sor}");
                MyE.Kiir("Baleset / Rongálás javításához felhasznált alkatrészek", $"a{sor}");
                MyE.Háttérszín($"a{sor}", 13619151d);
                sor++;
                MyE.Kiir("Megnevezés", $"a{sor}");
                MyE.Kiir("Cikkszám", $"b{sor}");
                MyE.Kiir("Darabszám", $"c{sor}");
                MyE.Rácsoz($"a{sor - 1}:c{sor}");
                MyE.Vastagkeret($"a{sor - 1}:c{sor}");
                sor += 5;
                MyE.Rácsoz($"a{sor - 4}:c{sor}");
                MyE.Vastagkeret($"a{sor - 4}:c{sor}");
                sor++;
                MyE.Egyesít("Munka1", $"a{sor}:c{sor + 1}");
                MyE.Kiir("Javítási módszer meghatározása, ha van rá pontos technológia annak számát kell beírni, ha nincs szövegesen kell leírni a javítást.", $"a{sor}:c{sor + 1}");
                MyE.Háttérszín($"a{sor}", 13619151d);
                MyE.Sortörésseltöbbsorba_egyesített($"a{sor}");
                sor += 2;
                MyE.Egyesít("Munka1", $"a{sor}:c{sor}");
                MyE.Kiir("CAJ-00-001 Korrózióvédelem c. technológiai utasítás", $"a{sor}");
                MyE.Igazít_vízszintes($"a{sor}", "bal");
                sor++;
                MyE.Egyesít("Munka1", $"a{sor}:c{sor}");
                MyE.Kiir("CAJ-00-002 Meghúzási nyomatékok c. technológiai utasítás", $"a{sor}");
                MyE.Igazít_vízszintes($"a{sor}", "bal");
                sor++;
                MyE.Egyesít("Munka1", $"a{sor}:c{sor}");
                MyE.Rácsoz($"a{sor - 4}:c{sor}");
                MyE.Vastagkeret($"a{sor - 4}:c{sor}");
                Holtart.Lép();
                sor++;
                MyE.Egyesít("Munka1", $"a{sor}:c{sor}");
                MyE.Háttérszín($"a{sor}", 13619151d);
                MyE.Kiir("A fenti javítást ki végzi el? (megfelelő aláhúzandó)", $"a{sor}");
                sor++;
                MyE.Vastagkeret($"a{sor - 3}:c{sor}");
                sor++;
                MyE.Egyesít("Munka1", $"a{sor}:c{sor}");
                MyE.Háttérszín($"a{sor}", 13619151d);
                MyE.Kiir("A fenti javítási technológiát elfogadják, javítás megkezdhető:", $"a{sor}");
                MyE.Vastagkeret($"a{sor}:c{sor}");
                sor++;
                MyE.Kiir("Szervezet", $"a{sor}");
                MyE.Kiir("Név", $"b{sor}");
                MyE.Kiir("Aláírás", $"c{sor}");
                MyE.Rácsoz($"a{sor}:c{sor}");
                MyE.Vastagkeret($"a{sor}:c{sor}");
                if (CafTábla.Rows.Count > 0)
                {
                    for (int i = 0; i < CafTábla.Rows.Count; i++)
                    {
                        sor++;
                        MyE.Kiir(CafTábla.Rows[i].Cells[1].Value.ToStrTrim(), $"a{sor}");
                        MyE.Kiir(CafTábla.Rows[i].Cells[2].Value.ToStrTrim(), $"b{sor}");
                        MyE.Sormagasság($"{sor - CafTábla.Rows.Count + 2}:{sor}", 32);
                    }
                    MyE.Rácsoz($"a{sor - CafTábla.Rows.Count + 1}:c{sor}");
                    MyE.Vastagkeret($"a{sor - CafTábla.Rows.Count}:c{sor}");

                }
                Holtart.Lép();
                sor++;
                MyE.Egyesít("Munka1", $"a{sor}:c{sor}");
                MyE.Betű($"a{sor}", 18);
                MyE.Kiir("Javítás utáni visszaellenőrzés", $"a{sor}");
                MyE.Háttérszín($"a{sor}", 13619151d);
                MyE.Vastagkeret($"a{sor}:c{sor}");
                sor++;
                MyE.Egyesít("Munka1", $"a{sor}:c{sor}");
                MyE.Vastagkeret($"a{sor}:c{sor}");
                sor++;
                MyE.Egyesít("Munka1", $"a{sor}:c{sor}");
                MyE.Kiir("Dátum:", $"a{sor}");
                MyE.Vastagkeret($"a{sor}:c{sor}");
                MyE.Igazít_vízszintes($"a{sor}", "bal");
                sor++;
                MyE.Egyesít("Munka1", $"a{sor}:c{sor}");
                MyE.Kiir("A Javítást felek átnézték, az alábbi észrevételeket teszik:", $"a{sor}");
                MyE.Vastagkeret($"a{sor}:c{sor}");
                MyE.Háttérszín($"a{sor}", 13619151d);
                sor++;
                MyE.Egyesít("Munka1", $"a{sor}:c{sor + 3}");
                MyE.Vastagkeret($"a{sor}:c{sor + 3}");
                sor += 4;
                MyE.Egyesít("Munka1", $"a{sor}:c{sor}");
                MyE.Kiir("Javításnál BKV készletből felhasznált erőforrások (idő, anyag):", $"a{sor}");
                MyE.Háttérszín($"a{sor}", 13619151d);
                sor++;
                MyE.Kiir("Megnevezés", $"a{sor}");
                MyE.Kiir("Cikkszám/rendelési szám", $"b{sor}");
                MyE.Kiir("Darabszám/Idő", $"c{sor}");
                sor += 2;
                MyE.Kiir("Munkaidő", $"a{sor}");
                MyE.Rácsoz($"a{sor - 3}:c{sor}");
                MyE.Vastagkeret($"a{sor - 3}:c{sor}");
                sor++;
                MyE.Egyesít("Munka1", $"a{sor}:c{sor}");
                MyE.Kiir("Megjegyzések", $"a{sor}");
                MyE.Háttérszín($"a{sor}", 13619151d);
                sor++;
                MyE.Egyesít("Munka1", $"a{sor}:c{sor}");
                MyE.Kiir("A felek a javítás eredményét együttesen átnézték, BKV nyilatkozik hogy az előírt technológiának megfelelően végezte el a javítást." +
                    " A javítás kivitelezését a fenti megjegyzések figyelembevételével elfogadják. A CAF a szállítási szerződés szerinti garanciát a járműre " +
                    "és a CAF által szállított alkatrészekre fenntartja.", $"a{sor}");
                MyE.Sormagasság($"{sor}:{sor}", 60);
                MyE.Sortörésseltöbbsorba_egyesített($"a{sor}:c{sor}");
                MyE.Rácsoz($"a{sor - 1}:c{sor}");
                MyE.Vastagkeret($"a{sor - 1}:c{sor}");
                sor++;
                MyE.Kiir("Szervezet", $"a{sor}");
                MyE.Kiir("Név", $"b{sor}");
                MyE.Kiir("Aláírás", $"c{sor}");
                MyE.Rácsoz($"a{sor}:c{sor}");
                MyE.Vastagkeret($"a{sor}:c{sor}");
                Holtart.Lép();
                if (CafTábla.Rows.Count > 0)
                {
                    for (int i = 0; i < CafTábla.Rows.Count; i++)
                    {
                        sor++;
                        MyE.Kiir(CafTábla.Rows[i].Cells[1].Value.ToStrTrim(), $"a{sor}");
                        MyE.Kiir(CafTábla.Rows[i].Cells[2].Value.ToStrTrim(), $"b{sor}");
                        MyE.Sormagasság($"{sor - CafTábla.Rows.Count + 2}:{sor}", 32);
                    }
                    MyE.Rácsoz($"a{sor - CafTábla.Rows.Count + 1}:c{sor}");
                    MyE.Vastagkeret($"a{sor - CafTábla.Rows.Count}:c{sor}");

                }
                // nyomtatási terület
                MyE.NyomtatásiTerület_részletes("Munka1", $"a1:c{sor}", "", "", true);
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
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Újat_Click(object sender, EventArgs e)
        {
            try
            {
                Üresrögzítő();
                Sorszám.Text = "";

                Rögzítjelentés.Visible = true;
                Kitöltendő_mezők();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Visszaállít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Sorszám.Text.ToStrTrim() == "") throw new HibásBevittAdat("Nincs kiválasztva sorszám!");

                // létrehozzuk az adott évi táblázatot illetve könyvtárat
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{DateTime.Today:yyyy}";
                if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

                hely += $@"\sérülés{DateTime.Today:yyyy}.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.Tükörtáblák(hely);

                // aktuális beállított dátum mezőbe mentjük
                hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value:yyyy}\sérülés{Dátum.Value:yyyy}.mdb";
                if (!Exists(hely)) throw new HibásBevittAdat("A beállított dátumra nincs adatbázis létrehozva!");
                if (!int.TryParse(Sorszám.Text, out int sorszám)) throw new HibásBevittAdat("Nincs ilyen sorszám");

                string szöveg = $"SELECT * FROM jelentés";
                List<Adat_Sérülés_Jelentés> Adatok = KézSérülésJelentés.Lista_Adatok(hely, Sérülésjelszó, Jelentésszöveg);

                Adat_Sérülés_Jelentés Elem = (from a in Adatok
                                              where a.Sorszám == sorszám
                                              select a).FirstOrDefault();


                if (Elem != null)
                {
                    szöveg = "UPDATE jelentés  SET ";
                    szöveg += "státus=1, státus1=1 ";
                    szöveg += $" WHERE [sorszám]={Sorszám.Text.Trim()}";
                    MyA.ABMódosítás(hely, Sérülésjelszó, szöveg);
                    MessageBox.Show("Az adatok rögzítése/ módosítása megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Kiír(Dátum.Value);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Rögzítjelentés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Pályaszám.Text.Trim() == "") throw new HibásBevittAdat("A pályaszám mező nem lehet üres.");
                if (Típus.Text.Trim() == "") throw new HibásBevittAdat("A Típus mező nem lehet üres.");
                if (Viszonylat.Text.Trim() == "") throw new HibásBevittAdat("A viszonylatot meg kell adni.");
                if (Szerelvény.Text.Trim() == "") Szerelvény.Text = "_";
                if (KmóraÁllás.Text.Trim() == "") KmóraÁllás.Text = "_";
                if (!int.TryParse(Forgalmiakadály.Text, out int fresult)) throw new HibásBevittAdat("A forgalmi akadálynak egész számnak kell lennie!");
                if (Járművezető.Text.Trim() == "") throw new HibásBevittAdat("A járművezető nevét meg kell adni!");
                if (Rendelésszám.Text.Trim() != "" && !int.TryParse(Rendelésszám.Text.Trim(), out int result)) throw new HibásBevittAdat("A rendelési szám mezőnek számnak kell lennie.");
                if (Helyszín.Text.Trim() == "") throw new HibásBevittAdat("A helyszínt meg kell adni!");
                if (Ütközött.Text.Trim() == "" && Esemény.Text.Trim() != "") throw new HibásBevittAdat("Ha esemény nem üres, akkor az ütközött mező nem lehet üres!");
                else if (Ütközött.Text.Trim() == "") throw new HibásBevittAdat("Az ütközött mező nem lehet üres!");

                if (Esemény.Text.Trim() == "" && Ütközött.Text.Trim() != "") Esemény.Text = "_";
                else if (Esemény.Text.Trim() == "") throw new HibásBevittAdat("Az esemény mező nem lehet üres.");

                if (!int.TryParse(AnyagikárÁr.Text.Trim(), out int anyagikára)) throw new HibásBevittAdat("Az anyagi kár nem lehet üres és egész számnak kell lennie.");
                if (Leírás.Text.ToStrTrim() == "" && Leírás1.Text.ToStrTrim() != "") Leírás.Text = "_";
                else if (Leírás.Text.ToStrTrim() == "") throw new HibásBevittAdat("Leírás mező nem lehet üres.");

                if (Leírás1.Text.ToStrTrim() == "" && Leírás.Text.ToStrTrim() != "") Leírás1.Text = "_";
                else if (Leírás1.Text.ToStrTrim() == "") throw new HibásBevittAdat("A leírás mező nem lehet üres.");

                if (Biztosító.Text.ToStrTrim() == "") Biztosító.Text = "_";

                Leírás1.Text = MyF.Szöveg_Tisztítás(Leírás1.Text, 0, Leírás1.Text.Length, true);
                Leírás.Text = MyF.Szöveg_Tisztítás(Leírás.Text, 0, Leírás.Text.Length, true);
                Esemény.Text = MyF.Szöveg_Tisztítás(Esemény.Text, 0, Esemény.Text.Length, true);
                Helyszín.Text = MyF.Szöveg_Tisztítás(Helyszín.Text, 0, Helyszín.Text.Length, true);
                Ütközött.Text = MyF.Szöveg_Tisztítás(Ütközött.Text, 0, Ütközött.Text.Length, true);

                // létrehozzuk az adot évi táblázatot illetve könyvtárat
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value.Year}";
                if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

                hely += $@"\sérülés{Dátum.Value:yyyy}.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.Tükörtáblák(hely);

                if (!Exists(hely)) throw new HibásBevittAdat("A beállított dátumra nincs adatbázis létrehozva!");

                string szöveg = "SELECT * FROM jelentés ";
                AdatokSérülésJelentés = KézSérülésJelentés.Lista_Adatok(hely, Sérülésjelszó, szöveg);

                int Rekordszám = 1;
                if (AdatokSérülésJelentés.Count > 0) Rekordszám = AdatokSérülésJelentés.Max(a => a.Sorszám) + 1;
                int új = 1;
                if (Sorszám.Text.ToStrTrim() == "")    // ha üres volt a sorszám mező akkor megkeressük az utolsót és emeljük a sorszámot
                {
                    if (AdatokSérülésJelentés == null)
                        // ha az első
                        Sorszám.Text = új.ToStrTrim();
                    else
                        // ha volt már akkor megkeressük az utolsót
                        Sorszám.Text = Rekordszám.ToStrTrim();
                }
                else
                    új = 0;

                if (új == 0)
                {
                    // Módosítás
                    szöveg = "UPDATE jelentés  SET ";
                    szöveg += $"Telephely='{Telephely.Text.Trim()}', ";
                    szöveg += $"Dátum='{Dátum.Value:yyyy.MM.dd} {Idő.Value:HH:mm:ss}', ";
                    szöveg += $"Balesethelyszín='{Helyszín.Text.Trim()}', ";
                    szöveg += $"Viszonylat='{Viszonylat.Text.Trim()}', ";
                    szöveg += $"Rendszám='{Pályaszám.Text.Trim()}', ";
                    szöveg += $"járművezető='{Járművezető.Text.Trim()}', ";
                    szöveg += $"Rendelésszám={Rendelésszám.Text.Trim()}, ";

                    if (Opt_Nyitott.Checked) szöveg += "státus=1, ";
                    if (Opt_Elkészült.Checked) szöveg += "státus=2, ";
                    if (Opt_Törölt.Checked) szöveg += "státus=9, ";
                    if (Sajáthiba.Checked) szöveg += "kimenetel=1, ";
                    if (Idegenhiba.Checked) szöveg += "kimenetel=2, ";
                    if (Személyhiba.Checked) szöveg += "kimenetel=3, ";
                    if (Egyébhiba.Checked) szöveg += "kimenetel=4, ";

                    szöveg += "Státus1=1, ";
                    szöveg += "iktatószám='_', ";
                    szöveg += $"Típus='{Típus.Text.Trim()}', ";
                    szöveg += $"Szerelvény='{Szerelvény.Text.Trim()}',";
                    szöveg += $"forgalmiakadály={Forgalmiakadály.Text.Trim()}, ";

                    if (Műszakihiba.Checked)
                        szöveg += "műszaki=true, ";
                    else
                        szöveg += "műszaki=false, ";

                    if (Anyagikár.Checked)
                        szöveg += "anyagikár=true, ";
                    else
                        szöveg += "anyagikár=false, ";

                    szöveg += $"biztosító='{Biztosító.Text.Trim()}', ";

                    if (Személyi.Checked)
                        szöveg += "személyisérülés=true, ";
                    else
                        szöveg += "személyisérülés=false, ";

                    szöveg += "személyisérülés1=false, ";

                    if (Gyors.Checked)
                        szöveg += "biztosítóidő=1, ";
                    else
                        szöveg += "biztosítóidő=2, ";

                    szöveg += $"mivelütközött='{Ütközött.Text.Trim()}', ";
                    szöveg += $"anyagikárft={anyagikára}, ";
                    szöveg += $"Leírás='{Leírás.Text.Trim()}', ";
                    szöveg += $"Leírás1='{Leírás1.Text.Trim()}', ";
                    szöveg += "Balesethelyszín1='_', ";
                    szöveg += $"esemény='{Esemény.Text.Trim()}', ";
                    szöveg += $"anyagikárft1=0, ";
                    szöveg += $"kmóraállás='{KmóraÁllás.Text.Trim()}' ";
                    szöveg += $" WHERE [sorszám]={Sorszám.Text}";
                }
                else
                {
                    // rögzítés
                    szöveg = "INSERT INTO jelentés  (sorszám, Telephely, Dátum, Balesethelyszín, ";
                    szöveg += "Viszonylat, Rendszám, járművezető,  Rendelésszám, ";
                    szöveg += "státus, kimenetel, Státus1, iktatószám, ";
                    szöveg += "Típus, Szerelvény, forgalmiakadály, műszaki, ";
                    szöveg += "anyagikár, biztosító, személyisérülés, személyisérülés1, ";
                    szöveg += "biztosítóidő, mivelütközött, anyagikárft, Leírás,";
                    szöveg += "Leírás1, Balesethelyszín1, esemény, anyagikárft1, ";
                    szöveg += "kmóraállás ) VALUES (";
                    szöveg += $"{Sorszám.Text}, ";
                    szöveg += $"'{Telephely.Text.Trim()}', ";
                    szöveg += $"'{Dátum.Value:yyyy.MM.dd} {Idő.Value:HH:mm:ss}', ";
                    szöveg += $"'{Helyszín.Text.Trim()}', ";
                    szöveg += $"'{Viszonylat.Text.Trim()}', ";
                    szöveg += $"'{Pályaszám.Text.Trim()}', ";
                    szöveg += $"'{Járművezető.Text.Trim()}', ";
                    szöveg += $"{Rendelésszám.Text.Trim()}, ";

                    if (Opt_Nyitott.Checked) szöveg += "1, ";
                    if (Opt_Elkészült.Checked) szöveg += "2, ";
                    if (Opt_Törölt.Checked) szöveg += "9, ";
                    if (Sajáthiba.Checked) szöveg += "1, ";
                    if (Idegenhiba.Checked) szöveg += "2, ";
                    if (Személyhiba.Checked) szöveg += "3, ";
                    if (Egyébhiba.Checked) szöveg += "4, ";

                    szöveg += "1, ";
                    szöveg += "'_', ";
                    szöveg += $"'{Típus.Text.Trim()}', ";
                    szöveg += $"'{Szerelvény.Text.Trim()} ',";
                    szöveg += $"{Forgalmiakadály.Text.Trim()}, ";

                    if (Műszakihiba.Checked)
                        szöveg += " true, ";
                    else
                        szöveg += " false, ";

                    if (Anyagikár.Checked)
                        szöveg += " true, ";
                    else
                        szöveg += " false, ";

                    szöveg += $"'{Biztosító.Text.Trim()}', ";

                    if (Személyi.Checked)
                        szöveg += " true, ";
                    else
                        szöveg += " false, ";

                    szöveg += " false, ";

                    if (Gyors.Checked)
                        szöveg += "1, ";
                    if (Hosszú.Checked)
                        szöveg += "2, ";

                    szöveg += $"'{Ütközött.Text.Trim()}', ";
                    szöveg += $"{anyagikára}, ";
                    szöveg += $"'{Leírás.Text.Trim()}', ";
                    szöveg += $"'{Leírás1.Text.Trim()}', ";
                    szöveg += "'_', ";
                    szöveg += $"'{Esemény.Text.Trim()}', ";
                    szöveg += "0, ";
                    szöveg += $"'{KmóraÁllás.Text.Trim()}') ";
                }
                MyA.ABMódosítás(hely, Sérülésjelszó, szöveg);

                MessageBox.Show("Az adatok rögzítése/ módosítása megtörtént!", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                KivalasztottSorszam = -1;
                Kiír(Dátum.Value);
                Pdflistázása();
                Képeklistázása();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TextBox_LostFocus(object sender, EventArgs e)
        {
            try
            {
                ((TextBox)sender).BackColor = Color.White;

                if (Pályaszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs kitöltve a pályaszám!");
                // ha már egyszer beállítottuk az alapadatokat, akkor nem módosítjuk
                if (Telephely.Text.Trim() != "") return;

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\villamos.mdb";
                string jelszó = "pozsgaii";
                string szöveg = $"SELECT * FROM állománytábla WHERE [azonosító]='{Pályaszám.Text.Trim()}'";
                AdatJármű = KézJármű.Egy_Adat_fő(hely, jelszó, szöveg);


                if (AdatJármű == null)
                {
                    MessageBox.Show("Nincs ilyen jármű!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Pályaszám.BackColor = Color.Aquamarine;
                    return;
                }
                else
                {
                    if (AdatJármű.Üzem == Telephely1.Text.Trim() || Program.Postás_Vezér == true)
                    {
                        Típus.Text = AdatJármű.Valóstípus;
                        Típus.BackColor = Color.White;
                        Telephely.Text = AdatJármű.Üzem;
                        Telephely.BackColor = Color.White;
                        Üzembehelyezés.Text = AdatJármű.Üzembehelyezés.ToString("yyyy.MM.dd");
                        Üzembehelyezés.BackColor = Color.White;
                        Szerelvény.BackColor = Color.White;
                    }
                    else
                    {
                        // nincs joga
                        Pályaszám.Text = "0000";
                        Pályaszám.BackColor = Color.Aquamarine;
                        throw new HibásBevittAdat("A jármű másik telephelyen van így nincs joga a rögzítéshez!");
                    }
                }

                // megnézzük, hogy szerelvényben fut-e
                hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";

                AdatJármű = KézJármű.Egy_Adat(hely, jelszó, szöveg);

                if (AdatJármű != null)
                {
                    double szerelvénykocsik = AdatJármű.Szerelvénykocsik;
                    if (szerelvénykocsik != 0d)
                    {
                        hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\villamos\szerelvény.mdb";
                        szöveg = $"SELECT * FROM szerelvénytábla WHERE [id]={szerelvénykocsik}";

                        Kezelő_Szerelvény Kéz2 = new Kezelő_Szerelvény();
                        Adat_Szerelvény Adat2 = Kéz2.Egy_Adat(hely, jelszó, szöveg);
                        Szerelvény.Text = "";
                        if (Adat2 != null)
                        {
                            if (Adat2.Kocsi1 != "0") Szerelvény.Text += $"{Adat2.Kocsi1}-";
                            if (Adat2.Kocsi2 != "0") Szerelvény.Text += $"{Adat2.Kocsi2}-";
                            if (Adat2.Kocsi3 != "0") Szerelvény.Text += $"{Adat2.Kocsi3}-";
                            if (Adat2.Kocsi4 != "0") Szerelvény.Text += $"{Adat2.Kocsi4}-";
                            if (Adat2.Kocsi5 != "0") Szerelvény.Text += $"{Adat2.Kocsi5}-";
                            if (Adat2.Kocsi6 != "0") Szerelvény.Text += Adat2.Kocsi6;
                        }
                    }
                }
                Viszonylat.Select();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Kitöltendő_mezők()
        {
            Forgalmiakadály.BackColor = Color.Aquamarine;
            Rendelésszám.BackColor = Color.Aquamarine;
            AnyagikárÁr.BackColor = Color.Aquamarine;
            AnyagikárÁr.BackColor = Color.Aquamarine;
            Pályaszám.BackColor = Color.Aquamarine;
            Típus.BackColor = Color.Aquamarine;
            Viszonylat.BackColor = Color.Aquamarine;
            Szerelvény.BackColor = Color.Aquamarine;
            Forgalmiakadály.BackColor = Color.Aquamarine;
            Járművezető.BackColor = Color.Aquamarine;
            Műszakihiba.BackColor = Color.Aquamarine;
            Anyagikár.BackColor = Color.Aquamarine;
            Rendelésszám.BackColor = Color.Aquamarine;
            Biztosító.BackColor = Color.Aquamarine;
            Helyszín.BackColor = Color.Aquamarine;
            Ütközött.BackColor = Color.Aquamarine;
            Személyi.BackColor = Color.Aquamarine;
            AnyagikárÁr.BackColor = Color.Aquamarine;
            Leírás.BackColor = Color.Aquamarine;
            Esemény.BackColor = Color.Aquamarine;
            Esemény.BackColor = Color.Aquamarine;
            Leírás1.BackColor = Color.Aquamarine;
        }

        private void Alap_mezők()
        {
            Forgalmiakadály.BackColor = Color.White;
            Rendelésszám.BackColor = Color.White;
            AnyagikárÁr.BackColor = Color.White;
            AnyagikárÁr.BackColor = Color.White;
            Pályaszám.BackColor = Color.White;
            Típus.BackColor = Color.White;
            Viszonylat.BackColor = Color.White;
            Szerelvény.BackColor = Color.White;
            Forgalmiakadály.BackColor = Color.White;
            Járművezető.BackColor = Color.White;
            Műszakihiba.BackColor = Color.White;
            Anyagikár.BackColor = Color.White;
            Rendelésszám.BackColor = Color.White;
            Biztosító.BackColor = Color.White;
            Helyszín.BackColor = Color.White;
            Ütközött.BackColor = Color.White;
            Személyi.BackColor = Color.White;
            AnyagikárÁr.BackColor = Color.White;
            Leírás.BackColor = Color.White;
            Esemény.BackColor = Color.White;
            Leírás1.BackColor = Color.White;
        }

        private void Cmbtelephely_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tarifa_kiírása();
            Állandókiiró();
        }
        #endregion


        #region Szűrők
        private List<Adat_Sérülés_Jelentés> StátusSzűrKölt(List<Adat_Sérülés_Jelentés> Adatok)
        {
            List<Adat_Sérülés_Jelentés> Válasz = new List<Adat_Sérülés_Jelentés>();
            if (KöltMind.Checked)
                Válasz.AddRange(from a in Adatok
                                select a);
            else
            {
                if (KöltNyitott.Checked)
                    Válasz.AddRange(from a in Adatok
                                    where a.Státus1 == 1
                                    select a);
                if (KöltKész.Checked)
                    Válasz.AddRange(from a in Adatok
                                    where a.Státus1 == 2
                                    select a);
            }
            return Válasz;

        }

        private List<Adat_Sérülés_Jelentés> TelephelySzűr(List<Adat_Sérülés_Jelentés> Adatok, List<string> Telephelyek, string Telephely)
        {
            List<Adat_Sérülés_Jelentés> Válasz = new List<Adat_Sérülés_Jelentés>();
            if (Telephely == "<Összes>")
                Válasz.AddRange(from a in Adatok
                                where Telephelyek.Contains(a.Telephely)
                                select a);
            else
                Válasz.AddRange(from a in Adatok
                                where a.Telephely == Telephely
                                select a);
            return Válasz;
        }

        private List<Adat_Sérülés_Jelentés> RendszámSzűr(List<Adat_Sérülés_Jelentés> Adatok, string rendszám)
        {
            List<Adat_Sérülés_Jelentés> Válasz = new List<Adat_Sérülés_Jelentés>();
            if (rendszám != "")
                Válasz.AddRange(from a in Adatok
                                where a.Rendszám == rendszám
                                select a);

            else
                Válasz.AddRange(Adatok);

            return Válasz;
        }

        private List<Adat_Sérülés_Jelentés> DátumSzűr(List<Adat_Sérülés_Jelentés> Adatok, DateTime Dátumtól, DateTime Dátumig)
        {
            List<Adat_Sérülés_Jelentés> Válasz = (from a in Adatok
                                                  where a.Dátum >= Dátumtól && a.Dátum <= Dátumig.AddDays(1)
                                                  orderby a.Sorszám
                                                  select a).ToList();
            return Válasz;
        }

        private List<Adat_Sérülés_Jelentés> NullásSzűrő(List<Adat_Sérülés_Jelentés> Adatok)
        {
            List<Adat_Sérülés_Jelentés> Válasz = new List<Adat_Sérülés_Jelentés>();

            Válasz.AddRange(from a in Adatok
                            where a.Rendelésszám == 0
                            select a);

            return Válasz;
        }

        private List<Adat_Sérülés_Jelentés> StátusSzűrJel(List<Adat_Sérülés_Jelentés> Adatok)
        {
            List<Adat_Sérülés_Jelentés> Válasz = new List<Adat_Sérülés_Jelentés>();
            if (LekMind.Checked)
                Válasz.AddRange(from a in Adatok
                                select a);
            else
            {
                if (LekNyitott.Checked)
                    Válasz.AddRange(from a in Adatok
                                    where a.Státus == 1
                                    select a);
                if (LekKész.Checked)
                    Válasz.AddRange(from a in Adatok
                                    where a.Státus == 2
                                    select a);
                if (LekTörölt.Checked)
                    Válasz.AddRange(from a in Adatok
                                    where a.Státus == 9
                                    select a);
            }
            return Válasz;
        }
        #endregion
    }
}