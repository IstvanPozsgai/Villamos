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
using static System.IO.File;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

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
        readonly Kezelő_Sérülés_Ideig KézKieg = new Kezelő_Sérülés_Ideig();
        readonly Kezelő_Excel_Beolvasás KézBeolvas = new Kezelő_Excel_Beolvasás();
        readonly Kezelő_Dolgozó_Alap KézDolgAlap = new Kezelő_Dolgozó_Alap();
        readonly Kezelő_Szerelvény KézSzerelvény = new Kezelő_Szerelvény();

        List<Adat_Kiegészítő_SérülésSzöveg> AdatokSérülésSzöveg = new List<Adat_Kiegészítő_SérülésSzöveg>();
        List<Adat_Telep_Kiegészítő_SérülésCaf> AdatokSérülésCaf = new List<Adat_Telep_Kiegészítő_SérülésCaf>();
        List<Adat_Sérülés_Jelentés> AdatokSérülésJelentés = new List<Adat_Sérülés_Jelentés>();
        List<Adat_Sérülés_Költség> AdatokSérülésKöltség = new List<Adat_Sérülés_Költség>();
        Adat_Jármű AdatJármű;
        Adat_Kiegészítő_Sérülés AdatKiegSérülés;
        List<Adat_Kiegészítő_Sérülés> AdatokKiegSérülés = new List<Adat_Kiegészítő_Sérülés>();

        int KivalasztottSorszam = -1;
        int Doksikdb, Képdb;
        int Cafsorszám;
        int FénySorszám2;
        string FényPályaszám2;
#pragma warning disable IDE0044 // Add readonly modifier
        List<string> Telephely_Költ = new List<string>();
        List<string> Telephely_Jel = new List<string>();
#pragma warning restore IDE0044 // Add readonly modifier

        readonly Beállítás_Betű BeBetű = new Beállítás_Betű();
        readonly Beállítás_Betű BeBetűV = new Beállítás_Betű { Vastag = true };
        readonly Beállítás_Betű BeBetűD = new Beállítás_Betű { Dőlt = true };
        readonly Beállítás_Betű BeBetű10 = new Beállítás_Betű { Méret = 10 };
        readonly Beállítás_Betű BeBetű11 = new Beállítás_Betű { Méret = 11 };
        readonly Beállítás_Betű BeBetű11V = new Beállítás_Betű { Méret = 11, Vastag = true };
        readonly Beállítás_Betű BeBetű14 = new Beállítás_Betű { Méret = 14 };
        readonly Beállítás_Betű BeBetű16 = new Beállítás_Betű { Méret = 16 };
        readonly Beállítás_Betű BeBetű16V = new Beállítás_Betű { Méret = 16, Vastag = true };
        readonly Beállítás_Betű BeBetű14V = new Beállítás_Betű { Méret = 14, Vastag = true };
        readonly Beállítás_Betű BeBetű14VE = new Beállítás_Betű { Méret = 14, Vastag = true, Formátum = "#,### Ft" };
        readonly Beállítás_Betű BeBetű14E = new Beállítás_Betű { Méret = 14, Formátum = "#,### Ft" };
        readonly Beállítás_Betű BeBetű14VD = new Beállítás_Betű { Méret = 14, Vastag = true, Dőlt = true };
        readonly Beállítás_Betű BeBetű18V = new Beállítás_Betű { Méret = 18, Vastag = true };
        readonly Beállítás_Betű BeBetű20V = new Beállítás_Betű { Méret = 20, Vastag = true };
        readonly Beállítás_Betű BeBetű22V = new Beállítás_Betű { Méret = 22, Vastag = true };
        readonly Beállítás_Betű BeBetű20 = new Beállítás_Betű { Méret = 20 };
        readonly Beállítás_Betű BeBetűCal = new Beállítás_Betű { Név = "Calibri", Méret = 11 };
        readonly Beállítás_Betű BeBetűCal18 = new Beállítás_Betű { Név = "Calibri", Méret = 18 };
        readonly Beállítás_Betű BeBetűCalA = new Beállítás_Betű { Név = "Calibri", Méret = 11, Vastag = true };
        #endregion


        #region alap
        public Ablak_sérülés()
        {
            InitializeComponent();
            Start();
        }

        private void Ablak_sérülés_Load(object sender, EventArgs e)
        {

        }

        private void Ablak_sérülés_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Sérülés_PDF?.Close();
            Új_Ablak_Sérülés_Kép?.Close();
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
                Lapfülek.Visible = false;
                Cursor = Cursors.WaitCursor;
                Dátum.Value = DateTime.Today;

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

                AdatokSérülés_Feltöltés();
                AdatokSérülésKöltség = KézSérülésKöltség.Lista_Adatok(KöltDátumtól.Value.Year);
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
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                Telephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                {
                    Cmbtelephely.Items.Add(Elem);
                    Telephely.Items.Add(Elem);
                }


                if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér)
                    Cmbtelephely.Text = Cmbtelephely.Items[0].ToStrTrim();
                else
                    Cmbtelephely.Text = Program.PostásTelephely;

                Cmbtelephely.Enabled = Program.Postás_Vezér;
                Telephely.Enabled = Program.Postás_Vezér;
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

        private void Btn_Súgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\sérülésnyilvántartás.html";
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
        private void Cafkiiró()
        {
            try
            {
                AdatokSérülésCaf = KézSérülésCaf.Lista_Adatok(Cmbtelephely.Text.Trim());
                DataTable AdatTábla = new DataTable();

                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("S.sz");
                AdatTábla.Columns.Add("Cég");
                AdatTábla.Columns.Add("Név");
                AdatTábla.Columns.Add("Beosztás");

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

                AdatokSérülésCaf = KézSérülésCaf.Lista_Adatok(Cmbtelephely.Text.Trim());
                int Rekordszám = 1;
                if (AdatokSérülésCaf.Count > 0) Rekordszám = AdatokSérülésCaf.Max(a => a.Id) + 1;

                if (Cafsorszám == -1)
                {
                    Adat_Telep_Kiegészítő_SérülésCaf ADAT = new Adat_Telep_Kiegészítő_SérülésCaf(
                         Rekordszám,
                         Cégtext.Text.Trim(),
                         Névtext.Text.Trim(),
                         BeosztásText.Text.Trim());
                    KézSérülésCaf.Rögzítés(Cmbtelephely.Text.Trim(), ADAT);
                    MessageBox.Show("A rögzítés megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Adat_Telep_Kiegészítő_SérülésCaf EgyElem = (from a in AdatokSérülésCaf
                                                                where a.Id == Cafsorszám
                                                                select a).FirstOrDefault();
                    if (EgyElem != null)
                    {
                        Adat_Telep_Kiegészítő_SérülésCaf ADAT = new Adat_Telep_Kiegészítő_SérülésCaf(
                            Cafsorszám,
                            Cégtext.Text.Trim(),
                            Névtext.Text.Trim(),
                            BeosztásText.Text.Trim());
                        KézSérülésCaf.Módosítás(Cmbtelephely.Text.Trim(), ADAT);
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
                AdatokSérülésCaf = KézSérülésCaf.Lista_Adatok(Cmbtelephely.Text.Trim());

                Adat_Telep_Kiegészítő_SérülésCaf Elem = (from a in AdatokSérülésCaf
                                                         where a.Id == Cafsorszám
                                                         select a).FirstOrDefault();

                if (Elem != null)
                {
                    KézSérülésCaf.Törlés(Cmbtelephely.Text.Trim(), Cafsorszám);
                    KézSérülésCaf.Újraszámolás(Cmbtelephely.Text.Trim());

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
                List<Adat_Sérülés_Tarifa> Adatok = KézTarifa.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum_tarifa.Value.Year);
                Adat_Sérülés_Tarifa Adat = Adatok.Where(a => a.Id == 1).FirstOrDefault();
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

        private void Állandókiiró()
        {
            try
            {
                AdatokSérülésSzöveg = KézSérülésSzöveg.Lista_Adatok(Cmbtelephely.Text.Trim());
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

                List<Adat_Sérülés_Tarifa> Adatok = KézTarifa.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum_tarifa.Value.Year);
                Adat_Sérülés_Tarifa Elem = (from a in Adatok
                                            where a.Id == 1
                                            select a).FirstOrDefault();


                if (Elem != null)
                {
                    Adat_Sérülés_Tarifa ADAT = new Adat_Sérülés_Tarifa(
                        1,
                        ÉvesD60,
                        ÉvesD03);
                    KézTarifa.Módosítás(Cmbtelephely.Text.Trim(), Dátum_tarifa.Value.Year, ADAT);
                    // módosítás
                    MessageBox.Show("A módosítás megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // új adat
                    Adat_Sérülés_Tarifa ADAT = new Adat_Sérülés_Tarifa(
                            1,
                            ÉvesD60,
                            ÉvesD03);
                    KézTarifa.Rögzítés(Cmbtelephely.Text.Trim(), Dátum_tarifa.Value.Year, ADAT);
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

                AdatokSérülésSzöveg = KézSérülésSzöveg.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adat_Kiegészítő_SérülésSzöveg Elem = (from a in AdatokSérülésSzöveg
                                                      where a.Id == 1
                                                      select a).FirstOrDefault();

                Adat_Kiegészítő_SérülésSzöveg ADAT = new Adat_Kiegészítő_SérülésSzöveg(
                     1,
                     Iktatószám.Text.Trim(),
                     Kiállította.Text.Trim(),
                     Telefonszám.Text.Trim(),
                     Eszköz.Text.Trim(),
                     Text1.Text.Trim(),
                     Text2.Text.Trim(),
                     Text3.Text.Trim(),
                     Text4.Text.Trim(),
                     Text5.Text.Trim(),
                     Text6.Text.Trim(),
                     Text7.Text.Trim());

                if (Elem != null)
                {
                    // módosítás
                    KézSérülésSzöveg.Módosítás(Cmbtelephely.Text.Trim(), ADAT);
                }
                else
                {
                    // új
                    KézSérülésSzöveg.Rögzítés(Cmbtelephely.Text.Trim(), ADAT);
                }
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
                AdatokSérülésSzöveg = KézSérülésSzöveg.Lista_Adatok(Cmbtelephely.Text.Trim());

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

            AdatokSérülésSzöveg = KézSérülésSzöveg.Lista_Adatok(Cmbtelephely.Text.Trim());

            Adat_Kiegészítő_SérülésSzöveg Elem = (from a in AdatokSérülésSzöveg
                                                  where a.Id == 2
                                                  select a).FirstOrDefault();

            Adat_Kiegészítő_SérülésSzöveg ADAT = new Adat_Kiegészítő_SérülésSzöveg(
                2,
                TxtBxDigitalisAlairo1.Text.Trim(),
                TxtBxDigitalisAlairo2.Text.Trim(),
                TxtBxBeosztas1.Text.Trim(),
                TxtBxBeosztas2.Text.Trim(),
                "_",
                "_",
                "_",
                "_",
                "_",
                "_",
                "_");

            if (Elem != null)
            {
                // módosítás
                KézSérülésSzöveg.Módosítás(Cmbtelephely.Text.Trim(), ADAT);
            }
            else
            {
                // új adat
                KézSérülésSzöveg.Rögzítés(Cmbtelephely.Text.Trim(), ADAT);
            }
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
                AdatokSérülésKöltség = KézSérülésKöltség.Lista_Adatok(LekDátumtól.Value.Year);

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
                List<Adat_Sérülés_Jelentés> Adatok = KézSérülésJelentés.Lista_Adatok(LekDátumtól.Value.Year);
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
                // Teljes lista
                List<Adat_Sérülés_Jelentés> Adatok = KézSérülésJelentés.Lista_Adatok(KöltDátumtól.Value.Year);
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

                MyX.DataGridViewToXML(fájlexc, Tábla);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MyF.Megnyitás(fájlexc);
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
                List<Adat_Sérülés_Anyag> AdatokSérülésAnyag = KézSérülésAnyag.Lista_Adatok(KöltDátumtól.Value.Year);
                AdatokSérülésAnyag = (from a in AdatokSérülésAnyag
                                      where a.Rendelés == SapRendelés.Text.ToÉrt_Int()
                                      orderby a.Cikkszám
                                      select a).ToList();

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

                DataTable AdatTábla = new DataTable();

                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Művelet leírása");
                AdatTábla.Columns.Add("Felhasznált idő");
                AdatTábla.Columns.Add("Teljesítmény fajta");
                AdatTábla.Columns.Add("Visszaszám");
                AdatTábla.Columns.Add("Költség");

                List<Adat_Sérülés_Művelet> AdatokMűvelet = KézSérülésMűvelet.Lista_Adatok(KöltDátumtól.Value.Year);
                Adat_Sérülés_Művelet Adat = AdatokMűvelet.Where(a => a.Rendelés == SapRendelés.Text.ToÉrt_Int()).FirstOrDefault();

                List<Adat_Sérülés_Visszajelentés> AdatokVissza = KézSérülésVisszajelentés.Lista_Adatok(KöltDátumtól.Value.Year);
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
                AdatokSérülésKöltség = KézSérülésKöltség.Lista_Adatok(KöltDátumtól.Value.Year);
                AdatokSérülésKöltség = AdatokSérülésKöltség.Where(a => a.Rendelés == SapRendelés.Text.ToÉrt_Int()).ToList();

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
                if (OpenFileDialogPI.ShowDialogEllenőr(OpenFileDialog1) == DialogResult.OK)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;

                // megnyitjuk a beolvasandó táblát
                string munkalap = "Munka1";
                MyX.ExcelMegnyitás(fájlexc);

                List<Adat_Sérülés_Anyag> AnyagAdatok = KézSérülésAnyag.Lista_Adatok(SapDátum.Value.Year);
                List<Adat_Sérülés_Költség> KölstégAdatok = KézSérülésKöltség.Lista_Adatok(SapDátum.Value.Year);
                List<Adat_Sérülés_Művelet> MűveletAdatok = KézSérülésMűvelet.Lista_Adatok(SapDátum.Value.Year);
                List<Adat_Sérülés_Visszajelentés> VisszajelentésAdatok = KézSérülésVisszajelentés.Lista_Adatok(SapDátum.Value.Year);
                // rendelés szám adatokat átnézzük ha van már ilyen adat az adtok között először töröljük
                int i = 1;
                int hossz, eleje, vége;
                string szó, ideig;
                int rendelés;
                int szószám, utolsóeleje, rendelésstátus;
                int anyagköltség, munkaköltség, gépköltség, szolgáltatás;
                string szöveg;

                Holtart.Be();
                int utolsó_sor = MyX.Utolsósor(munkalap);
                List<double> Költrend = new List<double>();
                List<double> Művrend = new List<double>();
                List<double> Visszrend = new List<double>();
                List<double> Anyagrend = new List<double>();
                while (MyX.Beolvas(munkalap, $"A{i}").Trim() != "_")
                {
                    Holtart.Lép();
                    szöveg = MyX.Beolvas(munkalap, $"A{i}");

                    if (Adat_módosítás(munkalap, $"A{i}", 4) == "PM22")
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

                            if (int.TryParse(szó.ToStrTrim(), out rendelés))
                            {
                                // itt kell nyitogatni a táblákat és törölni az előző adatokat
                                Adat_Sérülés_Költség KöltségElem = (from a in KölstégAdatok
                                                                    where a.Rendelés == rendelés
                                                                    select a).FirstOrDefault();
                                if (KöltségElem != null) Költrend.Add(rendelés);

                                Adat_Sérülés_Művelet MűveletElem = (from a in MűveletAdatok
                                                                    where a.Rendelés == rendelés
                                                                    select a).FirstOrDefault();
                                if (MűveletElem != null) Művrend.Add(rendelés);

                                Adat_Sérülés_Visszajelentés VisszajelentésElem = (from a in VisszajelentésAdatok
                                                                                  where a.Rendelés == rendelés
                                                                                  select a).FirstOrDefault();
                                if (VisszajelentésElem != null) Visszrend.Add(rendelés);


                                Adat_Sérülés_Anyag AnyagElem = (from a in AnyagAdatok
                                                                where a.Rendelés == rendelés
                                                                select a).FirstOrDefault();
                                if (AnyagElem != null) Anyagrend.Add(rendelés);
                                szószám += 1;
                            }
                        }
                    }
                    i += 1;
                }

                if (Költrend.Count > 0) KézSérülésKöltség.Törlés(SapDátum.Value.Year, Költrend);
                if (Művrend.Count > 0) KézSérülésMűvelet.Törlés(SapDátum.Value.Year, Művrend);
                if (Visszrend.Count > 0) KézSérülésVisszajelentés.Törlés(SapDátum.Value.Year, Visszrend);
                if (Anyagrend.Count > 0) KézSérülésAnyag.Törlés(SapDátum.Value.Year, Anyagrend);


                #region  Költség sorok
                i = 1;
                anyagköltség = 0;
                munkaköltség = 0;
                gépköltség = 0;
                szolgáltatás = 0;
                rendelésstátus = 0;

                List<Adat_Sérülés_Ideig> AdatokIdeig = new List<Adat_Sérülés_Ideig>();
                while (MyX.Beolvas(munkalap, $"A{i}").Trim() != "_")
                {

                    Holtart.Lép();
                    szöveg = MyX.Beolvas(munkalap, $"A{i}");
                    if (Adat_módosítás(munkalap, $"A{i}", 4) == "PM22")
                    {
                        if (szöveg.Contains("MLZR") || szöveg.Contains("LEZR"))
                            rendelésstátus = 1;
                        else
                            rendelésstátus = 0;
                    }
                    if (szöveg.Substring(0, 2) == "HU")
                    {
                        rendelés = Adat_módosítás(2, 10, szöveg).ToStrTrim().Replace(".", "").ToÉrt_Int();
                        ideig = Adat_módosítás(11, 22, szöveg).Replace(".", "").Replace(" ", "");
                        switch (szöveg.Substring(szöveg.Length - 4, 4))
                        {
                            case "513)":
                                {
                                    if (!int.TryParse(ideig, out anyagköltség)) anyagköltség = 0;
                                    break;
                                }
                            case "571)":
                                {
                                    if (!int.TryParse(ideig, out munkaköltség)) munkaköltség = 0;
                                    break;
                                }
                            case "566)":
                                {
                                    if (!int.TryParse(ideig, out gépköltség)) gépköltség = 0;
                                    break;
                                }
                            case "515)":
                                {
                                    if (!int.TryParse(ideig, out szolgáltatás)) szolgáltatás = 0;
                                    break;
                                }
                        }
                        Adat_Sérülés_Ideig ADATI = new Adat_Sérülés_Ideig(
                            rendelés,
                            anyagköltség,
                            munkaköltség,
                            gépköltség,
                            szolgáltatás,
                            rendelésstátus);
                        AdatokIdeig.Add(ADATI);

                        anyagköltség = 0;
                        munkaköltség = 0;
                        gépköltség = 0;
                        szolgáltatás = 0;
                        rendelésstátus = 0;
                        rendelés = 0;
                    }
                    i += 1;
                }
                if (AdatokIdeig.Count > 0) KézKieg.Rögzítés(SapDátum.Value.Year, AdatokIdeig);

                // költség adatokat rendezzük

                anyagköltség = 0;
                szolgáltatás = 0;
                gépköltség = 0;
                munkaköltség = 0;
                rendelésstátus = 0;
                rendelés = 0;

                List<Adat_Sérülés_Ideig> Adatok = KézKieg.Lista_Adatok(SapDátum.Value.Year);
                List<Adat_Sérülés_Költség> AdatokKöltség = new List<Adat_Sérülés_Költség>();
                foreach (Adat_Sérülés_Ideig rekord in Adatok)
                {
                    ideig = rekord.Rendelés.ToStrTrim();
                    if (rendelés != 0 & rendelés.ToStrTrim() != ideig)
                    {
                        Adat_Sérülés_Költség ADATK = new Adat_Sérülés_Költség(
                            rendelés,
                            anyagköltség,
                            munkaköltség,
                            gépköltség,
                            szolgáltatás,
                            rendelésstátus);
                        AdatokKöltség.Add(ADATK);
                        anyagköltség = 0;
                        szolgáltatás = 0;
                        gépköltség = 0;
                        munkaköltség = 0;
                        rendelésstátus = 0;
                        rendelés = 0;
                    }

                    rendelésstátus = int.Parse(rekord.Státus.ToStrTrim());
                    rendelés = rekord.Rendelés;
                    if (anyagköltség == 0) anyagköltség = rekord.Anyagköltség;
                    if (szolgáltatás == 0) szolgáltatás = rekord.Szolgáltatás;
                    if (gépköltség == 0) gépköltség = rekord.Gépköltség;
                    if (munkaköltség == 0) munkaköltség = rekord.Munkaköltség;

                }
                Adat_Sérülés_Költség ADAT = new Adat_Sérülés_Költség(
                      rendelés,
                      anyagköltség,
                      munkaköltség,
                      gépköltség,
                      szolgáltatás,
                      rendelésstátus);
                AdatokKöltség.Add(ADAT);
                if (AdatokKöltség.Count > 0) KézSérülésKöltség.Rögzítés(SapDátum.Value.Year, AdatokKöltség);


                // ki kell törölni az ideig tartalmát
                // *************************************
                KézKieg.Törlés(SapDátum.Value.Year);
                #endregion


                #region Művelet sorok

                string Teljesítményfajta;
                string Visszaszám;
                string Műveletszöveg;

                i = 1;
                List<Adat_Sérülés_Művelet> AdatokMűv = new List<Adat_Sérülés_Művelet>();
                while (MyX.Beolvas(munkalap, $"A{i}").Trim() != "_")
                {
                    Holtart.Lép();
                    szöveg = MyX.Beolvas(munkalap, $"A{i}").Trim();
                    if (Adat_módosítás(munkalap, $"A{i}", 4) == "MJV1")
                    {
                        hossz = szöveg.Length;
                        eleje = 0;
                        vége = 0;
                        szó = "";
                        szószám = 1;
                        Teljesítményfajta = "A";
                        Visszaszám = "A";
                        Műveletszöveg = "A";
                        rendelés = 0;
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
                                            if (!int.TryParse(szó, out rendelés)) rendelés = 0;
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
                        Adat_Sérülés_Művelet ADATM = new Adat_Sérülés_Művelet(
                             Teljesítményfajta.Trim(),
                             rendelés,
                             Visszaszám.Trim(),
                             Műveletszöveg.Trim()
                            );
                        AdatokMűv.Add(ADATM);
                    }
                    i += 1;
                }
                if (AdatokMűv.Count > 0) KézSérülésMűvelet.Rögzítés(SapDátum.Value.Year, AdatokMűv);
                #endregion


                #region visszajelentés sorok
                i = 1;
                int munkaidő = 0;
                string storno = "";
                rendelés = 0;

                List<Adat_Sérülés_Visszajelentés> AdatokVissz = new List<Adat_Sérülés_Visszajelentés>();
                while (MyX.Beolvas(munkalap, $"A{i}").Trim() != "_")
                {
                    Holtart.Lép();
                    szöveg = MyX.Beolvas(munkalap, $"A{i}").Trim();
                    if (szöveg.Substring(0, 3) == "D03" | szöveg.Substring(0, 3) == "D60")
                    {
                        Visszaszám = Adat_módosítás(3, 12, szöveg);

                        if (!int.TryParse(Adat_módosítás(25, 18, szöveg).Replace(".", ""), out munkaidő)) munkaidő = 0;

                        rendelés = Adat_módosítás(42, 9, szöveg).ToÉrt_Int();
                        if (szöveg.Substring(szöveg.Length - 1, 1).Trim() == "X")
                            storno = "I";
                        else
                            storno = "N";
                        Teljesítményfajta = szöveg.Substring(0, 3);

                        Adat_Sérülés_Visszajelentés ADATVissz = new Adat_Sérülés_Visszajelentés(
                            Visszaszám.Trim(),
                            munkaidő,
                            storno.Trim(),
                            rendelés,
                            Teljesítményfajta.Trim());
                        AdatokVissz.Add(ADATVissz);
                    }
                    i++;
                }
                if (AdatokVissz.Count > 0) KézSérülésVisszajelentés.Rögzítés(SapDátum.Value.Year, AdatokVissz);
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
                rendelés = 0;
                utolsóeleje = 0;

                List<Adat_Sérülés_Anyag> AdatokAnyag = new List<Adat_Sérülés_Anyag>();
                while (MyX.Beolvas(munkalap, $"A{i}") != "_")
                {
                    Holtart.Lép();
                    szöveg = MyX.Beolvas(munkalap, $"A{i}").Trim();
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
                                            if (!int.TryParse(szó, out rendelés)) rendelés = 0;
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
                        Adat_Sérülés_Anyag ADATanyag = new Adat_Sérülés_Anyag(
                             cikkszám.Trim(),
                             anyagnév.Trim(),
                             mennyiség,
                             Mennyiségegység.Trim(),
                             ár,
                             állapot.Trim(),
                             rendelés,
                             mozgásnem.Trim()
                            );
                        AdatokAnyag.Add(ADATanyag);
                    }
                    i += 1;
                }
                if (AdatokAnyag.Count > 0) KézSérülésAnyag.Rögzítés(SapDátum.Value.Year, AdatokAnyag);
                #endregion

                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();

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
                string munkalap = "Munka1";
                MyX.ExcelLétrehozás(munkalap);

                List<Adat_Excel_Beolvasás> Adatok = KézBeolvas.Lista_Adatok();
                Adatok = (from a in Adatok
                          where a.Csoport == "SérülésAny"
                          && a.Státusz == false
                          orderby a.Oszlop
                          select a).ToList();

                int i = 1;


                foreach (Adat_Excel_Beolvasás rekord in Adatok)
                {
                    MyX.Kiir(rekord.Fejléc.ToStrTrim(), MyF.Oszlopnév(i) + "1");
                    i += 1;
                }

                MyX.Oszlopszélesség(munkalap, "a:a", 20);
                MyX.Oszlopszélesség(munkalap, "b:b", 50);
                MyX.Oszlopszélesség(munkalap, "c:h", 11);
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
                if (OpenFileDialogPI.ShowDialogEllenőr(OpenFileDialog1) == DialogResult.OK)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;

                SAP_Adatokbeolvasása.Sérülés_beolvasó(fájlexc);

                // megnyitjuk a beolvasandó táblát
                MyX.ExcelMegnyitás(fájlexc);
                string munkalap = "Munka1";

                // megnézzük, hogy hány sorból áll a tábla
                int utolsó = MyX.Utolsósor(munkalap);

                Holtart.Lép();
                List<Adat_Sérülés_Anyag> AnyagAdatok = KézSérülésAnyag.Lista_Adatok(SapDátum.Value.Year);

                List<double> Anyagrend = new List<double>();
                for (int i = 2; i < utolsó; i++)
                {
                    if (double.TryParse(MyX.Beolvas(munkalap, $"g{i}").Trim(), out double rendelés))
                    {
                        Adat_Sérülés_Anyag EgyAnyag = (from a in AnyagAdatok
                                                       where a.Rendelés == rendelés
                                                       select a).FirstOrDefault();

                        if (EgyAnyag != null) Anyagrend.Add(rendelés);
                    }
                }
                if (Anyagrend.Count > 0) KézSérülésAnyag.Törlés(SapDátum.Value.Year, Anyagrend);

                string cikkszám = "";
                string mennyiségstr = "";
                string árstr = "";
                string állapot = "";
                string Mennyiségegység = "";
                string mozgásnem = "";
                string anyagnév = "";
                string rendelésstr;

                List<Adat_Sérülés_Anyag> AdatokAnyag = new List<Adat_Sérülés_Anyag>();
                for (int i = 2; i < utolsó; i++)
                {
                    Holtart.Lép();
                    cikkszám = Adat_módosítás(munkalap, $"a{i}", 20);
                    anyagnév = Adat_módosítás(munkalap, $"b{i}", 50);
                    string ideig = MyX.Beolvas(munkalap, $"c{i}").Trim();
                    if (ideig != "")
                        mennyiségstr = ideig;
                    else
                        mennyiségstr = "0";
                    Mennyiségegység = Adat_módosítás(munkalap, $"d{i}", 10);

                    ideig = MyX.Beolvas(munkalap, $"e{i}").Trim();
                    if (ideig != "")
                        árstr = ideig;
                    else
                        árstr = "0";
                    állapot = Adat_módosítás(munkalap, $"f{i}", 3);

                    ideig = MyX.Beolvas(munkalap, $"g{i}").Trim();
                    if (ideig != "")
                        rendelésstr = ideig;
                    else
                        rendelésstr = "0";
                    mozgásnem = Adat_módosítás(munkalap, $"h{i}", 5);
                    Adat_Sérülés_Anyag ADATanyag = new Adat_Sérülés_Anyag(
                          cikkszám.Trim(),
                          anyagnév.Trim(),
                          mennyiségstr.ToÉrt_Double(),
                          Mennyiségegység.Trim(),
                          árstr.ToÉrt_Double(),
                          állapot.Trim(),
                          rendelésstr.ToÉrt_Double(),
                          mozgásnem.Trim());
                    AdatokAnyag.Add(ADATanyag);
                }
                if (AdatokAnyag.Count > 0) KézSérülésAnyag.Rögzítés(SapDátum.Value.Year, AdatokAnyag);

                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();

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

        private string Adat_módosítás(string munkalap, string beolvasott, int hossz)
        {
            string ideig = MyX.Beolvas(munkalap, beolvasott).Trim();
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

                MyX.DataGridViewToXML(fájlexc, Tábla2);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MyF.Megnyitás(fájlexc);
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
                List<Adat_Sérülés_Jelentés> Adatok = KézSérülésJelentés.Lista_Adatok(KöltDátumtól.Value.Year);
                List<Adat_Sérülés_Jelentés> AdatokGY = new List<Adat_Sérülés_Jelentés>();

                for (int i = 0; i < Tábla2.Rows.Count; i++)
                {
                    if (Tábla2.Rows[i].Selected)
                    {
                        Adat_Sérülés_Jelentés Elem = (from a in Adatok
                                                      where a.Sorszám == Tábla2.Rows[i].Cells[0].Value.ToÉrt_Int()
                                                      select a).FirstOrDefault();

                        if (Elem != null)
                        {
                            Adat_Sérülés_Jelentés ADAT = new Adat_Sérülés_Jelentés(
                                Tábla2.Rows[i].Cells[0].Value.ToStrTrim().ToÉrt_Int(),
                                2);
                            AdatokGY.Add(ADAT);
                        }
                    }
                }
                if (AdatokGY.Count > 0) KézSérülésJelentés.Státus1Elk(KöltDátumtól.Value.Year, AdatokGY);
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

                AdatokSérülésKöltség = KézSérülésKöltség.Lista_Adatok(KöltDátumtól.Value.Year);
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
                List<Adat_Sérülés_Jelentés> Adatok = KézSérülésJelentés.Lista_Adatok(KöltDátumtól.Value.Year);
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
                AdatTábla.Columns.Add("Járművezető ");

                AdatokKöltségNullás_Feltöltés();
                AdatTábla.Clear();
                foreach (Adat_Sérülés_Jelentés rekord in AdatokSérülésJelentés)
                {
                    DataRow Soradat = AdatTábla.NewRow();

                    Soradat["S.sz.       "] = rekord.Sorszám;
                    Soradat["Dátum       "] = rekord.Dátum.ToString("yyyy.MM.dd");
                    Soradat["Pályaszám   "] = rekord.Rendszám;
                    Soradat["Viszonylat  "] = rekord.Viszonylat;
                    Soradat["Telep       "] = rekord.Telephely;
                    Soradat["Rövid szöveg"] = rekord.Mivelütközött.ToStrTrim() != "_" ? $"Ütközött {rekord.Mivelütközött}" : $"{rekord.Esemény} {rekord.Balesethelyszín.Trim()}";
                    Soradat["Járművezető "] = rekord.Járművezető;

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
                Tábla2.Columns["Járművezető "].Width = 200;

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
                string munkalap = "Munka1";
                MyX.DataGridViewToXML(fájlexc, Tábla2);

                MyX.ExcelMegnyitás(fájlexc);

                int utolsóoszlop = MyX.Utolsóoszlop(munkalap);
                int utolsósor = MyX.Utolsósor(munkalap);
                // oszlopszélesség
                MyX.Munkalap_betű(munkalap, BeBetű10);
                MyX.Oszlopszélesség(munkalap, "A:D", 10);
                MyX.Oszlopszélesség(munkalap, "e:e", 9);
                MyX.Oszlopszélesség(munkalap, "f:f", 33);
                MyX.Oszlopszélesség(munkalap, "g:g", 20);
                MyX.Oszlopszélesség(munkalap, "h:h", 15);
                MyX.Sormagasság(munkalap, "1:1", 25);
                MyX.Sormagasság(munkalap, $"2:{utolsósor}", 18);
                MyX.Betű(munkalap, "1:1", BeBetűV);

                MyX.Igazít_vízszintes(munkalap, "A:G", "közép");

                // egész rácsoz és vastagkeret
                MyX.Rácsoz(munkalap, "B1:" + MyF.Oszlopnév(utolsóoszlop) + utolsósor.ToStrTrim());
                MyX.Vastagkeret(munkalap, $"B1:G{utolsósor}");

                // nyomtatási terület
                Beállítás_Nyomtatás BeNyom = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"B1:{MyF.Oszlopnév(utolsóoszlop)}{utolsósor}",
                    LapSzéles = 1,
                    FejlécKözép = Program.PostásNév,
                    FejlécJobb = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"),
                    LáblécKözép = "&P/&N"
                };
                MyX.NyomtatásiTerület_részletes(munkalap, BeNyom);
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
                string munkalap = "Munka1";
                MyX.ExcelLétrehozás(munkalap);

                int szolgsor, sor, elsősor, anyagsor;
                string szöveg2;
                double anyagegység;
                DataGridViewRow sorIndex = Tábla2.Rows.Cast<DataGridViewRow>()
                         .FirstOrDefault(row => row.Cells[0].Value.ToÉrt_Int() == KivalasztottSorszam)
                         ?? throw new HibásBevittAdat("A kiválasztott sorszám nem található a táblázatban!");

                int Sor = sorIndex.Index;

                // megformázzuk
                MyX.Munkalap_betű(munkalap, BeBetű14);

                // oszlopszélesség
                MyX.Oszlopszélesség(munkalap, "a:a", 50);
                MyX.Oszlopszélesség(munkalap, "b:b", 34);
                MyX.Oszlopszélesség(munkalap, "c:c", 16);
                MyX.Oszlopszélesség(munkalap, "d:d", 17);
                MyX.Oszlopszélesség(munkalap, "e:e", 22);
                MyX.Oszlopszélesség(munkalap, "f:f", 28);
                MyX.Oszlopszélesség(munkalap, "g:g", 24);

                Holtart.Lép();
                // 1 SOR
                szolgsor = 0;
                sor = 1;
                Rendelésadatokmunkaidő_listázása();
                if (Tábla1.Rows.Count == 1)
                {
                    MyX.Kiir("34/VU/2020. 2.sz. melléklet", $"g{sor}");
                    MyX.Betű(munkalap, $"g{sor}", BeBetű14V);
                }
                else
                {
                    if (Tábla1.Rows[Tábla1.Rows.Count - 2].Cells[2].Value.ToStrTrim() != "D03")
                    {
                        MyX.Kiir("34/VU/2020. 1.sz. melléklet", $"g{sor}");
                        MyX.Betű(munkalap, $"g{sor}", BeBetű14V);
                    }
                    else
                    {
                        MyX.Kiir("34/VU/2020. 2.sz. melléklet", $"g{sor}");
                        MyX.Betű(munkalap, $"g{sor}", BeBetű14V);
                    }
                }
                sor++;
                MyX.Sormagasság(munkalap, $"{sor}:{sor + 2}", 30);
                MyX.Egyesít(munkalap, $"a{sor}:g{sor}");
                MyX.Betű(munkalap, $"a{sor}", BeBetű22V);
                MyX.Kiir("KÖLTSÉGKIMUTATÁS", $"a{sor}");

                // 2 SOR
                sor++;
                MyX.Egyesít(munkalap, $"a{sor}:g{sor}");
                MyX.Betű(munkalap, $"a{sor}", BeBetű20V);
                if (Tábla1.Rows.Count == 1)
                {
                    MyX.Kiir("KÁRESEMÉNY SAJÁT KIVITELEZÉSBEN ÉS/VAGY SZOLGÁLTATÁS ", $"a{sor}");
                    sor++;
                    MyX.Kiir("IGÉNYBEVÉTELÉVEL ELVÉGZETT HELYREÁLLÍTÁSÁRÓL", $"a{sor}");
                    MyX.Egyesít(munkalap, $"a{sor}:g{sor}");
                    MyX.Betű(munkalap, $"a{sor}", BeBetű20V);
                    sor++;
                    MyX.Egyesít(munkalap, $"a{sor}" + $":f{sor}");
                    MyX.Betű(munkalap, $"a{sor}", BeBetű20);
                    MyX.Kiir("Munkavállalói közvetlen kártérítés", $"a{sor}");
                    MyX.Egyesít(munkalap, $"a{sor}:g{sor}");
                    MyX.Betű(munkalap, $"a{sor}", BeBetűV);
                }
                else
                {
                    if (Tábla1.Rows[Tábla1.Rows.Count - 2].Cells[2].Value == null || Tábla1.Rows[Tábla1.Rows.Count - 2].Cells[2].Value.ToStrTrim() != "D03")
                        MyX.Kiir("KÁRESEMÉNY SAJÁT KIVITELEZÉSBEN ELVÉGZETT HELYREÁLLÍTÁSÁRÓL", $"a{sor}");
                    else
                    {
                        MyX.Kiir("KÁRESEMÉNY SAJÁT KIVITELEZÉSBEN ÉS/VAGY SZOLGÁLTATÁS ", $"a{sor}");
                        sor++;
                        MyX.Kiir("IGÉNYBEVÉTELÉVEL ELVÉGZETT HELYREÁLLÍTÁSÁRÓL", $"a{sor}");
                        MyX.Egyesít(munkalap, $"a{sor}:g{sor}");
                        MyX.Betű(munkalap, $"a{sor}", BeBetű20V);
                        sor++;
                        MyX.Egyesít(munkalap, $"a{sor}" + $":f{sor}");
                        MyX.Betű(munkalap, $"a{sor}", BeBetű20);
                        MyX.Kiir("Munkavállalói közvetlen kártérítés", $"a{sor}");
                        MyX.Egyesít(munkalap, $"a{sor}:g{sor}");
                        MyX.Betű(munkalap, $"a{sor}", BeBetűV);
                    }
                }

                // 4 sor
                sor++;
                MyX.Sormagasság(munkalap, $"{sor}:{sor + 5}", 25);
                MyX.Egyesít(munkalap, $"d{sor}" + $":e{sor}");
                MyX.Egyesít(munkalap, $"f{sor}" + $":g{sor}");
                MyX.Betű(munkalap, $"a{sor}", BeBetű14);
                MyX.Kiir("Iktatószám:", $"d{sor}");
                MyX.Kiir(Iktatószám.Text.Trim(), $"f{sor}");
                MyX.Igazít_vízszintes(munkalap, $"d{sor}", "bal");
                MyX.Igazít_vízszintes(munkalap, $"f{sor}", "jobb");
                MyX.VékonyAlsóVonal(munkalap, $"d{sor}" + $":g{sor}");

                // 5 sor
                sor++;
                MyX.Egyesít(munkalap, $"d{sor}" + $":e{sor}");
                MyX.Egyesít(munkalap, $"f{sor}" + $":g{sor}");
                MyX.Betű(munkalap, $"a{sor}", BeBetű14);
                MyX.Kiir("Bizonylatot kiállította:", $"d{sor}");
                MyX.Kiir(Kiállította.Text.Trim(), $"f{sor}");
                MyX.Igazít_vízszintes(munkalap, $"d{sor}", "bal");
                MyX.Igazít_vízszintes(munkalap, $"f{sor}", "jobb");
                MyX.VékonyAlsóVonal(munkalap, $"d{sor}" + $":g{sor}");

                // 6 sor
                sor++;
                MyX.Egyesít(munkalap, $"d{sor}" + $":e{sor}");
                MyX.Egyesít(munkalap, $"f{sor}" + $":g{sor}");
                MyX.Betű(munkalap, $"a{sor}", BeBetű14);
                MyX.Kiir("Telefonszám:", $"d{sor}");
                MyX.Kiir(Telefonszám.Text.Trim(), $"f{sor}");
                MyX.Igazít_vízszintes(munkalap, $"d{sor}", "bal");
                MyX.Igazít_vízszintes(munkalap, $"f{sor}", "jobb");
                MyX.VékonyAlsóVonal(munkalap, $"d{sor}" + $":g{sor}");

                // 7 sor
                sor++;
                MyX.Egyesít(munkalap, $"d{sor}" + $":e{sor}");
                MyX.Egyesít(munkalap, $"f{sor}" + $":g{sor}");
                MyX.Betű(munkalap, $"a{sor}", BeBetű14);
                if (ChckBxDigitális.Checked)
                {
                    MyX.Kiir("Kiállítás dátuma:", $"d{sor}");
                    MyX.Kiir("időbélyegző szerinti időpontban", $"f{sor}");
                }
                else
                {
                    MyX.Kiir("Kiállítás dátuma:", $"d{sor}");
                    MyX.Kiir($"{DateTime.Today:yyyy.MM.dd}", $"f{sor}");
                }
                MyX.Igazít_vízszintes(munkalap, $"d{sor}", "bal");
                MyX.Igazít_vízszintes(munkalap, $"f{sor}", "jobb");
                MyX.VékonyAlsóVonal(munkalap, $"d{sor}" + $":g{sor}");

                // 8 sor
                sor++;
                MyX.Egyesít(munkalap, $"d{sor}" + $":e{sor}");
                MyX.Egyesít(munkalap, $"f{sor}" + $":g{sor}");
                MyX.Betű(munkalap, $"a{sor}", BeBetű14);
                MyX.Kiir("Mellékletek száma:", $"d{sor}");
                MyX.Kiir("-", $"f{sor}");
                MyX.Igazít_vízszintes(munkalap, $"d{sor}", "bal");
                MyX.Igazít_vízszintes(munkalap, $"f{sor}", "jobb");
                MyX.VékonyAlsóVonal(munkalap, $"d{sor}" + $":g{sor}");

                // 9 sor
                sor++;
                MyX.Sormagasság(munkalap, $"{sor}:{sor + 6}", 40);
                MyX.Betű(munkalap, $"a{sor}", BeBetű18V);
                MyX.Kiir("Káresemény azonosító adatai:", $"a{sor}");
                MyX.Igazít_vízszintes(munkalap, $"a{sor}", "bal");
                MyX.Betű(munkalap, $"a{sor}", BeBetű14V);

                // 10 sor
                sor++;
                MyX.Kiir("Helyreállított eszköz / eszközök:", $"a{sor}");
                MyX.Igazít_vízszintes(munkalap, $"a{sor}", "bal");
                MyX.Betű(munkalap, $"a{sor}", BeBetű14V);
                MyX.Egyesít(munkalap, $"b{sor}" + $":g{sor}");
                MyX.Kiir(Eszköz.Text.Trim(), $"b{sor}" + $":g{sor}");
                MyX.Igazít_vízszintes(munkalap, $"b{sor}" + $":g{sor}", "bal");
                MyX.VékonyAlsóVonal(munkalap, $"a{sor}" + $":g{sor}");

                // 11 sor
                sor++;
                MyX.Kiir("Helyreállított eszköz / eszközök azonosítója:", $"a{sor}");
                MyX.Igazít_vízszintes(munkalap, $"a{sor}", "bal");
                MyX.Betű(munkalap, $"a{sor}", BeBetű14V);
                MyX.Sortörésseltöbbsorba(munkalap, $"a{sor}");
                MyX.Egyesít(munkalap, $"b{sor}" + $":g{sor}");
                MyX.Kiir(Pályaszám.Text.Trim(), $"b{sor}" + $":g{sor}");
                MyX.Igazít_vízszintes(munkalap, $"b{sor}" + $":g{sor}", "bal");
                MyX.VékonyAlsóVonal(munkalap, $"a{sor}" + $":g{sor}");

                // 12 sor
                sor++;
                MyX.Kiir("Káresemény helyszíne:", $"a{sor}");
                MyX.Igazít_vízszintes(munkalap, $"a{sor}", "bal");
                MyX.Betű(munkalap, $"a{sor}", BeBetű14V);
                MyX.Egyesít(munkalap, $"b{sor}" + $":g{sor}");
                MyX.Kiir(Helyszín.Text, $"b{sor}" + $":g{sor}");
                MyX.Igazít_vízszintes(munkalap, $"b{sor}" + $":g{sor}", "bal");
                MyX.VékonyAlsóVonal(munkalap, $"a{sor}" + $":g{sor}");

                // 13 sor
                sor++;
                MyX.Kiir("Káresemény ideje:", $"a{sor}");
                MyX.Igazít_vízszintes(munkalap, $"a{sor}", "bal");
                MyX.Betű(munkalap, $"a{sor}", BeBetű14V);
                MyX.Egyesít(munkalap, $"b{sor}" + $":g{sor}");
                MyX.Kiir($"{Dátum.Value:yyyy.MM.dd} {Idő.Value:hh:mm}", $"b{sor}" + $":g{sor}");
                MyX.Igazít_vízszintes(munkalap, $"b{sor}" + $":g{sor}", "bal");
                MyX.VékonyAlsóVonal(munkalap, $"a{sor}" + $":g{sor}");

                // 14 sor
                sor++;
                MyX.Sormagasság(munkalap, $"{sor}:{sor}", 65);
                MyX.Kiir("Helyreállítást végző szolgálat:", $"a{sor}");
                MyX.Igazít_vízszintes(munkalap, $"a{sor}", "bal");
                MyX.Betű(munkalap, $"a{sor}", BeBetű14V);
                MyX.Egyesít(munkalap, $"b{sor}" + $":g{sor}");
                szöveg2 = $"{Költséghely.Text.Trim()}, ";
                szöveg2 += $"{Text1.Text.Trim()}, ";
                szöveg2 += $"{Text2.Text.Trim()}, ";
                szöveg2 += $"{Text3.Text.Trim()}, ";
                szöveg2 += $"{Text4.Text.Trim()}, ";
                szöveg2 += $"{Telephely.Text.Trim()} ";
                szöveg2 += Text5.Text.Trim();
                MyX.Kiir(szöveg2, $"b{sor}:g{sor}");
                MyX.Igazít_vízszintes(munkalap, $"b{sor}:g{sor}", "bal");
                MyX.Sortörésseltöbbsorba(munkalap, $"b{sor}:g{sor}", true);
                MyX.VékonyAlsóVonal(munkalap, $"a{sor}:g{sor}");

                // 15 sor
                sor++;
                MyX.Kiir("Helyreállítás munkaszáma SAP-ban:", $"a{sor}");
                MyX.Igazít_vízszintes(munkalap, $"a{sor}", "bal");
                MyX.Betű(munkalap, $"a{sor}", BeBetű14V);
                MyX.Egyesít(munkalap, $"b{sor}:g{sor}");
                MyX.Kiir(Rendelésszám.Text, $"b{sor}:g{sor}");
                MyX.Igazít_vízszintes(munkalap, $"b{sor}:g{sor}", "bal");
                MyX.VékonyAlsóVonal(munkalap, $"a{sor}:g{sor}");

                // 16 sor
                sor++;
                MyX.Sormagasság(munkalap, $"{sor}:{sor + 3}", 25);

                // 17 sor
                sor++;
                MyX.Kiir("Kárhelyreállítás költségeinek kimutatása:", $"a{sor}");
                MyX.Betű(munkalap, $"a{sor}", BeBetű18V);
                MyX.Igazít_vízszintes(munkalap, $"a{sor}", "bal");
                sor++;

                // 19 sor
                Holtart.Lép();
                sor++;
                MyX.Kiir("Anyagfelhasználás", $"a{sor}");
                MyX.Betű(munkalap, $"a{sor}", BeBetű16);
                MyX.Igazít_vízszintes(munkalap, $"a{sor}", "bal");
                MyX.Betű(munkalap, $"a{sor}", BeBetű14V);

                // 20 sor
                sor++;
                MyX.Sormagasság(munkalap, $"{sor}:{sor}", 72);
                MyX.Kiir("Felhasznált anyag megnevezése", $"a{sor}");
                MyX.Kiir("Felhasznált anyag       állapota         SAP-ban (SARZS)", $"b{sor}");
                MyX.Kiir("Felhasznált anyag cikkszáma SAP-ban", $"c{sor}");
                MyX.Kiir("Felhasznált mennyiség", $"d{sor}");
                MyX.Kiir("Felhasználás mennyiségi egysége", $"e{sor}");
                MyX.Kiir("Egységár (Forint / mennyiségi egység)", $"f{sor}");
                MyX.Kiir("Költsége (Forint)", $"g{sor}");
                MyX.Betű(munkalap, $"a{sor}:g{sor}", BeBetű14V);
                MyX.Sortörésseltöbbsorba(munkalap, $"a{sor}:g{sor}");
                MyX.Igazít_vízszintes(munkalap, $"a{sor}:g{sor}", "közép");
                MyX.Rácsoz(munkalap, $"a{sor}:g{sor}");

                // ***********************************************
                // Magyarázó sor
                sor++;
                MyX.Sormagasság(munkalap, $"{sor}:{sor}", 72);
                MyX.Kiir("Anyag megnevezése SAP-ban", $"a{sor}");
                MyX.Kiir($"01 – gyári\n02 – javított\n03 – selejt/javításra vár", $"b{sor}");
                MyX.Kiir("Cikkszám SAP-ban", $"c{sor}");
                MyX.Kiir("Javításhoz vételezett mennyiség", $"d{sor}");
                MyX.Kiir("Vételezés mennyiségi egysége", $"e{sor}");
                MyX.Kiir("Anyag SAP átlagára a vételezéskor", $"f{sor}");
                MyX.Kiir("Anyag-felhasználás költsége", $"g{sor}");
                MyX.Betű(munkalap, $"a{sor}:g{sor}", BeBetű14V);
                MyX.Sortörésseltöbbsorba(munkalap, $"a{sor}:g{sor}");
                MyX.Igazít_vízszintes(munkalap, $"a{sor}:g{sor}", "közép");
                MyX.Rácsoz(munkalap, $"a{sor}:g{sor}");

                // ***********************************************
                // Anyag részletesen
                elsősor = sor;
                Rendelésadatokanyag_listázás();

                if (Tábla1.Columns[0].HeaderText.Trim() == "Cikkszám")
                {
                    for (int i = 0; i < Tábla1.Rows.Count - 1; i++)
                    {
                        sor++;
                        MyX.Igazít_vízszintes(munkalap, $"b{sor}:d{sor}", "közép");
                        MyX.Igazít_vízszintes(munkalap, $"e{sor}:f{sor}", "jobb");
                        MyX.Kiir(Tábla1.Rows[i].Cells[1].Value.ToStrTrim(), $"a{sor}");
                        MyX.Kiir(Tábla1.Rows[i].Cells[6].Value.ToStrTrim(), $"b{sor}");
                        MyX.Kiir(Tábla1.Rows[i].Cells[0].Value.ToStrTrim().TrimStart('0'), $"c{sor}");
                        MyX.Kiir("#SZÁMD#" + Tábla1.Rows[i].Cells[2].Value.ToStrTrim(), $"d{sor}");
                        MyX.Kiir(Tábla1.Rows[i].Cells[3].Value.ToStrTrim(), $"e{sor}");
                        double anyagköltség = 0;
                        if (Tábla1.Rows[i].Cells[2].Value.ToStrTrim() != "")
                        {

                            if (double.TryParse(Tábla1.Rows[i].Cells[2].Value.ToStrTrim(), out double result) == true && result != 0)
                            {
                                if (!double.TryParse(Tábla1.Rows[i].Cells[5].Value.ToStrTrim(), out anyagköltség)) anyagköltség = 0;
                                if (!double.TryParse(Tábla1.Rows[i].Cells[2].Value.ToStrTrim(), out double anyagdarab)) anyagdarab = 1;
                                anyagegység = Math.Round(anyagköltség / anyagdarab, 2);
                                MyX.Kiir($"#SZÁMD#{anyagegység}", $"f{sor}");
                                MyX.Betű(munkalap, $"f{sor}", BeBetű14E);

                            }
                        }
                        MyX.Kiir($"#SZÁMD#{anyagköltség}", $"g{sor}");
                        MyX.Betű(munkalap, $"g{sor}", BeBetű14E);
                    }
                    MyX.Rácsoz(munkalap, $"a{elsősor}:g{sor}");
                }
                Holtart.Lép();

                // 22 sor
                sor++;
                MyX.Egyesít(munkalap, $"a{sor}:g{sor}");
                MyX.Sormagasság(munkalap, $"{sor}:{sor}", 57);
                MyX.Kiir("Cikkszámok szerint fel kell sorolni a káresemény helyreállítása során felhasznált anyagokat. A helyreállítás során visszanyert," +
                    " cikkszámmal rendelkező hulladékokat negatív felhasználási mennyiségként kell feltüntetni, csökkentve az anyagfelhasználás költségét.",
                    $"a{sor}:g{sor}");
                MyX.Igazít_vízszintes(munkalap, $"a{sor}:g{sor}", "közép");
                MyX.Betű(munkalap, $"a{sor}", BeBetű14VD);
                MyX.Vastagkeret(munkalap, $"a{sor}:g{sor}");
                MyX.Sortörésseltöbbsorba(munkalap, $"a{sor}", true);

                // 23 sor
                sor++;
                anyagsor = sor;
                MyX.Sormagasság(munkalap, $"{sor}:{sor + 2}", 20);
                MyX.Egyesít(munkalap, $"a{sor}:f{sor}");
                MyX.Kiir("Helyreállításhoz felhasznált anyagok költsége összesen:", $"a{sor}");
                if (elsősor <= sor)
                    MyX.Kiir($"#KÉPLET#=SUM(R[{elsősor - sor}]C:R[-2]C)", $"g{sor}");
                else
                    MyX.Kiir($"#SZÁME#0", $"g{sor}");
                MyX.Betű(munkalap, $"g{sor}", BeBetű14E);
                MyX.Igazít_vízszintes(munkalap, $"a{sor}", "bal");
                MyX.Betű(munkalap, $"a{sor}", BeBetű16);
                MyX.Rácsoz(munkalap, $"a{sor}:g{sor}");
                MyX.Betű(munkalap, $"a{sor}", BeBetű14V);

                if (!Chck_Egyszerüsített.Checked)
                {
                    // 25 sor
                    sor++;
                    sor++;
                    MyX.Kiir("Közvetlen gépköltség", $"a{sor}");
                    MyX.Betű(munkalap, $"a{sor}", BeBetű16V);
                    MyX.Igazít_vízszintes(munkalap, $"a{sor}", "bal");
                    // 26 sor
                    sor++;
                    MyX.Sormagasság(munkalap, $"{sor}:{sor}", 54);
                    MyX.Kiir("Elvégzett fő munkafolyamat megnevezése", $"a{sor}");
                    MyX.Kiir("Végrehajtás időtartama (órában)", $"e{sor}");
                    MyX.Kiir("Óradíj (Forint/óra)", $"f{sor}");
                    MyX.Kiir("Munkadíj (Forint)", $"g{sor}");
                    MyX.Betű(munkalap, $"a{sor}:g{sor}", BeBetű14V);
                    MyX.Sortörésseltöbbsorba(munkalap, $"a{sor}:g{sor}");
                    MyX.Igazít_vízszintes(munkalap, $"a{sor}:g{sor}", "közép");
                    MyX.Egyesít(munkalap, $"a{sor}:d{sor}");
                    // 27 sor
                    sor++;
                    MyX.Egyesít(munkalap, $"a{sor}:d{sor}");
                    MyX.Kiir("-", $"a{sor}");
                    MyX.Kiir("-", $"d{sor}");
                    MyX.Kiir("-", $"e{sor}");
                    MyX.Kiir("-", $"f{sor}");
                    MyX.Kiir("-", $"g{sor}");
                    MyX.Rácsoz(munkalap, $"a{sor}:g{sor}");
                    // 28 sor
                    sor++;
                    MyX.Egyesít(munkalap, $"a{sor}:g{sor}");
                    MyX.Sormagasság(munkalap, $"{sor}:{sor}", 50);
                    MyX.Kiir("A kár helyreállítása érdekében felhasznált gépi teljesítmény munkafolyamatonként fel kell sorolni. A PM modulban ennek megfelelően kell a munkaidő nyilvántartásokat vezetni.", $"a{sor}");
                    MyX.Betű(munkalap, $"a{sor}", BeBetű14V);
                    MyX.Vastagkeret(munkalap, $"a{sor}:g{sor}");
                    MyX.Sortörésseltöbbsorba(munkalap, $"a{sor}");
                    MyX.Egyesít(munkalap, $"a{sor}:g{sor}");
                    // 29 sor
                    sor++;
                    MyX.Sormagasság(munkalap, $"{sor}:{sor + 2}", 20);
                    MyX.Egyesít(munkalap, $"a{sor}:f{sor}");
                    MyX.Kiir("Helyreállítás közvetlen gépköltsége összesen:", $"a{sor}");
                    MyX.Kiir("-", $"g{sor}");
                    MyX.Betű(munkalap, $"g{sor}", BeBetű14E);
                    MyX.Igazít_vízszintes(munkalap, $"a{sor}", "bal");
                    MyX.Betű(munkalap, $"a{sor}", BeBetű16V);
                    MyX.Rácsoz(munkalap, $"a{sor}:g{sor}");
                }
                if (Tábla2.Rows[Sor].Cells[11].Value.ToStrTrim() != "0")
                {
                    // 31 sor
                    sor += 2;

                    MyX.Kiir("Igénybe vett szolgáltatások", $"a{sor}");
                    MyX.Betű(munkalap, $"a{sor}", BeBetű16V);
                    MyX.Egyesít(munkalap, $"a{sor}:e{sor}");
                    MyX.Igazít_vízszintes(munkalap, $"a{sor}", "bal");

                    // 32 sor
                    sor++;
                    MyX.Sormagasság(munkalap, $"{sor}:{sor}", 108);
                    MyX.Kiir("Igénybe vett szolgáltatások megnevezése", $"a{sor}");
                    MyX.Kiir("Mellékelt számla (vagy SAP bizonylat megnevezése és) sorszáma", $"f{sor}");
                    MyX.Kiir("Számla nettó értéke (SAP-ban kimutatható költség) (Forint)", $"g{sor}");
                    MyX.Betű(munkalap, $"a{sor}:g{sor}", BeBetű14V);
                    MyX.Rácsoz(munkalap, $"a{sor}:g{sor}");
                    MyX.Sortörésseltöbbsorba(munkalap, $"a{sor}:g{sor}");
                    MyX.Igazít_vízszintes(munkalap, $"a{sor}:g{sor}", "közép");
                    // 33 sor
                    sor++;
                    MyX.Egyesít(munkalap, $"a{sor}:e{sor}");
                    MyX.Kiir("-", $"a{sor}");
                    MyX.Kiir("-", $"e{sor}");
                    MyX.Kiir($"#SZÁMD#{Tábla2.Rows[Sor].Cells[11].Value.ToStrTrim()}", $"g{sor}");
                    MyX.Betű(munkalap, $"g{sor}", BeBetű14E);
                    MyX.Rácsoz(munkalap, $"a{sor}:g{sor}");
                    MyX.Vastagkeret(munkalap, $"a{sor}:g{sor}");
                    // 34 sor
                    sor++;
                    MyX.Egyesít(munkalap, $"a{sor}:g{sor}");
                    MyX.Sormagasság(munkalap, $"{sor}:{sor}", 50);
                    MyX.Kiir("A kár helyreállításához igénybe vett külső szolgáltatásokat számlánként fel kell sorolni. A hivatkozott számlák másolatát a költségkimutatáshoz csatolni kell.", $"a{sor}");
                    MyX.Betű(munkalap, $"a{sor}", BeBetű14V);
                    MyX.Sortörésseltöbbsorba(munkalap, $"a{sor}", true);
                    MyX.Vastagkeret(munkalap, $"a{sor}");
                    MyX.Egyesít(munkalap, $"a{sor}:g{sor}");
                    MyX.Igazít_vízszintes(munkalap, $"a{sor}", "közép");
                    // 35 sor
                    sor++;
                    szolgsor = sor;
                    MyX.Sormagasság(munkalap, $"{sor}:{sor + 2}", 20);
                    MyX.Egyesít(munkalap, $"a{sor}:f{sor}");
                    MyX.Kiir("Helyreállításhoz igénybe vett szolgáltatások összesen:", $"a{sor}");
                    MyX.Kiir($"#SZÁMD#{Tábla2.Rows[Sor].Cells[11].Value.ToStrTrim()}", $"g{sor}");
                    MyX.Igazít_vízszintes(munkalap, $"a{sor}", "bal");
                    MyX.Betű(munkalap, $"a{sor}", BeBetű16V);
                    MyX.Rácsoz(munkalap, $"a{sor}:g{sor}");
                }

                // 37 sor
                Holtart.Lép();
                sor += 2;
                MyX.Kiir("Munkadíj", $"a{sor}");
                MyX.Betű(munkalap, $"a{sor}", BeBetű16V);
                MyX.Igazít_vízszintes(munkalap, $"a{sor}", "bal");


                // 38 sor
                sor++;
                MyX.Sormagasság(munkalap, $"{sor}:{sor}", 54);

                MyX.Egyesít(munkalap, $"a{sor}:d{sor}");
                MyX.Kiir("Elvégzett fő munkafolyamat megnevezése", $"a{sor}:d{sor}");
                MyX.Kiir("Végrehajtás időtartama (órában)", $"e{sor}");
                MyX.Kiir("Tarifa (Forint/óra)", $"f{sor}");
                MyX.Kiir("Munkadíj (Forint)", $"g{sor}");
                MyX.Betű(munkalap, $"a{sor}:g{sor}", BeBetű14V);
                MyX.Sortörésseltöbbsorba(munkalap, $"a{sor}:g{sor}");
                MyX.Rácsoz(munkalap, $"a{sor}:g{sor}");
                MyX.Igazít_vízszintes(munkalap, $"a{sor}:g{sor}", "közép");
                MyX.Egyesít(munkalap, $"a{sor}:d{sor}");
                // ************************************
                // idő

                Rendelésadatokmunkaidő_listázása();
                elsősor = sor + 1;

                if (Tábla1.Columns[0].HeaderText.Trim() == "Művelet leírása")
                {
                    for (int i = 0; i < Tábla1.Rows.Count - 1; i++)
                    {
                        sor++;
                        MyX.Egyesít(munkalap, $"a{sor}:d{sor}");
                        MyX.Kiir(Tábla1.Rows[i].Cells[0].Value.ToStrTrim(), $"a{sor}");
                        double óra = Tábla1.Rows[i].Cells[1].Value.ToÉrt_Double() / 60;
                        MyX.Kiir($"#SZÁMD#{óra}", $"e{sor}");
                        MyX.Betű(munkalap, $"g{sor}", BeBetű14E);
                        MyX.Igazít_vízszintes(munkalap, $"e{sor}", "jobb");
                        if (Tábla1.Rows[i].Cells[2].Value.ToStrTrim() == "D60")
                            MyX.Kiir("#SZÁME#" + ÉvestarifaD60.Text, $"f{sor}");
                        else
                            MyX.Kiir("#SZÁME#" + ÉvestarifaD03.Text, $"f{sor}");

                        MyX.Kiir("#KÉPLET#=RC[-2]*RC[-1]", $"g{sor}");
                    }
                    if (elsősor <= sor)
                    {
                        MyX.Rácsoz(munkalap, $"a{elsősor}:g{sor}");
                    }
                }

                Holtart.Lép();
                // 40 sor
                sor++;
                MyX.Egyesít(munkalap, $"a{sor}:g{sor}");
                MyX.Sormagasság(munkalap, $"{sor}:{sor}", 50);
                MyX.Kiir("A kár helyreállítása érdekében elvégzett tevékenységeket fő munkafolyamatonként (például: x elemek cseréje, fényezés javítása) fel kell sorolni. A PM modulban ennek megfelelően kell a munkaidő nyilvántartásokat vezetni.", $"a{sor}");
                MyX.Betű(munkalap, $"a{sor}", BeBetű14V);
                MyX.Sortörésseltöbbsorba(munkalap, $"a{sor}", true);
                MyX.Vastagkeret(munkalap, $"a{sor}:g{sor}");

                // 41 sor
                sor++;
                MyX.Sormagasság(munkalap, $"{sor}:{sor + 3}", 20);
                MyX.Egyesít(munkalap, $"a{sor}:f{sor}");
                MyX.Kiir("Helyreállítás munkadíja összesen:", $"a{sor}");
                if (elsősor <= sor)
                    MyX.Kiir("#KÉPLET#=sum(R[" + (elsősor - sor).ToStrTrim() + "]C:R[-2]C)", $"g{sor}");
                else
                    MyX.Kiir($"#SZÁME#0", $"g{sor}");

                MyX.Betű(munkalap, $"g{sor}", BeBetű14E);
                MyX.Igazít_vízszintes(munkalap, $"a{sor}", "bal");
                MyX.Betű(munkalap, $"a{sor}", BeBetű16V);
                MyX.Sortörésseltöbbsorba(munkalap, $"a{sor}", true);
                MyX.Rácsoz(munkalap, $"a{sor}:g{sor}");

                // 45 sor
                sor += 2;
                MyX.Sormagasság(munkalap, $"{sor}:{sor}", 45);
                MyX.Egyesít(munkalap, $"a{sor}:f{sor}");
                MyX.Kiir("Helyreállítás nettó költsége összesen (Forint):", $"a{sor}");
                if (szolgsor == 0)
                    MyX.Kiir("#KÉPLET#=SUM(R[-2]C,R[" + (anyagsor - sor).ToStrTrim() + "]C)", $"g{sor}");
                else
                    MyX.Kiir("#KÉPLET#=SUM(R[-2]C,R[" + (anyagsor - sor).ToStrTrim() + "]C, R[" + (szolgsor - sor).ToStrTrim() + "]C)", $"g{sor}");

                MyX.Betű(munkalap, $"g{sor}", BeBetű14VE);
                MyX.Igazít_vízszintes(munkalap, $"a{sor}", "bal");
                MyX.Betű(munkalap, $"a{sor}", BeBetű20V);
                MyX.Rácsoz(munkalap, $"a{sor}:g{sor}");
                if (!ChckBxDigitális.Checked)
                {
                    // 46 sor
                    sor++;
                    MyX.Sormagasság(munkalap, $"{sor}:{sor + 4}", 20);
                    // 47 sor
                    sor++;
                    MyX.Kiir("kiállítás dátuma:", $"a{sor}");
                    // 48 sor
                    sor++;
                    MyX.Kiir($"Budapest,{DateTime.Today:yyyy.MM.dd}", $"a{sor}");
                    MyX.Egyesít(munkalap, $"d{sor}:f{sor}");
                    MyX.VékonyAlsóVonal(munkalap, $"d{sor}:g{sor}");
                    // 49 sor
                    sor++;
                    MyX.Egyesít(munkalap, $"d{sor}:g{sor}");
                    MyX.Kiir(Text6.Text.Trim(), $"d{sor}");
                    // 50 sor
                    sor++;
                    MyX.Egyesít(munkalap, $"d{sor}:g{sor}");
                    MyX.Kiir(Text7.Text.Trim(), $"d{sor}");
                }
                else
                {
                    sor += 2;
                    MyX.Kiir("Kelt, az elektronikus aláírás időbélyegzője szerinti időpontban", $"a{sor}");
                    MyX.Betű(munkalap, $"a{sor}", BeBetűD);

                    sor += 3;
                    MyX.Sormagasság(munkalap, $"a{sor}", 80);
                    MyX.VékonyAlsóVonal(munkalap, $"b{sor}");
                    MyX.VékonyAlsóVonal(munkalap, $"f{sor}");

                    sor++;
                    MyX.Kiir(TxtBxDigitalisAlairo1.Text.Trim(), $"b{sor}");
                    MyX.Igazít_vízszintes(munkalap, $"b{sor}", "közép");
                    MyX.Kiir(TxtBxDigitalisAlairo2.Text.Trim(), $"f{sor}");
                    MyX.Igazít_vízszintes(munkalap, $"f{sor}", "közép");

                    sor++;
                    MyX.Kiir(TxtBxBeosztas1.Text.Trim(), $"b{sor}");
                    MyX.Igazít_vízszintes(munkalap, $"b{sor}", "közép");
                    MyX.Kiir(TxtBxBeosztas2.Text.Trim(), $"f{sor}");
                    MyX.Igazít_vízszintes(munkalap, $"f{sor}", "közép");

                }
                sor++;
                MyX.Kiir("Budapesti Közlekedési Zártkörűen Működő Részvénytársaság", $"c{sor}");
                MyX.Sormagasság(munkalap, $"a{sor}", 60);
                MyX.Egyesít(munkalap, $"b{sor}:f{sor}");

                // nyomtatási beállítások
                string helycsop = $@"{Application.StartupPath}\Főmérnökség\adatok\BKV.jpg";
                string jobbfejléc = "&\"Arial,Félkövér\"&20&EBudapesti Közlekedési Zártkörűen Működő Részvénytársaság&12" + '\n' + "&\"Arial,Normál\"&16&E 1980 Budapest Akácfa u. 15.  Telefon: 461-6500";
                Beállítás_Nyomtatás BeNYom = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"A1:G{sor}",
                    FejlécJobb = jobbfejléc,
                    Képútvonal = helycsop,
                    LáblécKözép = "&G"
                };
                MyX.NyomtatásiTerület_részletes(munkalap, BeNYom);
                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();
                MyF.Megnyitás(fájlexc);
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
                List<Adat_Sérülés_Jelentés> Adatok = KézSérülésJelentés.Lista_Adatok(Melyikév.Year) ?? throw new HibásBevittAdat("A beállított dátumra nincs adatbázis létrehozva!");
                Adat_Sérülés_Jelentés rekord = Adatok.Where(a => a.Sorszám == KivalasztottSorszam).FirstOrDefault();

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
                List<Adat_Jármű> JárműAdatok = KézJármű.Lista_Adatok("Főmérnökség");
                AdatJármű = JárműAdatok.Where(a => a.Azonosító == Pályaszám.Text.Trim()).FirstOrDefault();
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
                string munkalap = "Munka1";
                MyX.ExcelLétrehozás(munkalap);

                // excel kitöltése

                // betű beállítása
                MyX.Munkalap_betű(munkalap, BeBetű11);

                MyX.Oszlopszélesség(munkalap, "a:b", 13);
                MyX.Oszlopszélesség(munkalap, "c:g", 12);
                MyX.Oszlopszélesség(munkalap, "d:d", 14);
                // egyesítések
                for (int i = 1; i <= 5; i++)
                    MyX.Egyesít(munkalap, $"a{i}:d{i}");

                MyX.Egyesít(munkalap, "e2:g2");
                MyX.Egyesít(munkalap, "f7:g7");
                MyX.Egyesít(munkalap, "d9:f9");
                MyX.Egyesít(munkalap, "a11:b11");
                MyX.Egyesít(munkalap, "a13:b13");
                MyX.Egyesít(munkalap, "a15:b15");
                MyX.Egyesít(munkalap, "a19:g20");
                for (int i = 22; i <= 24; i++)
                    MyX.Egyesít(munkalap, $"a{i}:b{i}");
                MyX.Egyesít(munkalap, "a26:g27");
                MyX.Egyesít(munkalap, "a29:g29");
                for (int i = 30; i <= 33; i++)
                {
                    MyX.Egyesít(munkalap, $"a{i}:b{i}");
                    MyX.Egyesít(munkalap, $"c{i}:g{i}");
                }
                MyX.Egyesít(munkalap, "a34:b37");
                MyX.Egyesít(munkalap, "c34:g37");
                MyX.Egyesít(munkalap, "a38:b38");
                MyX.Egyesít(munkalap, "c38:g38");
                MyX.Egyesít(munkalap, "a39:b43");
                MyX.Egyesít(munkalap, "c39:g43");
                MyX.Egyesít(munkalap, "a45:g46");
                for (int i = 51; i <= 53; i++)
                    MyX.Egyesít(munkalap, $"e{i}:g{i}");
                Holtart.Lép();
                // fix kiírások
                MyX.Kiir("Fékvizsgálati jelentés", "e2");
                MyX.Betű(munkalap, "e2", BeBetű11V);
                MyX.Kiir("pályaszámú", "b7");
                MyX.Kiir("típusú villamos", "d7");
                MyX.Kiir("számú viszonylaton", "f7");
                MyX.Kiir(" -n", "b9");
                MyX.Kiir(" -kor történt eseményre vonatkozólag.", "d9");
                MyX.Kiir("Szerelvény pályaszámai:", "a11");
                MyX.Kiir("Üzembehelyezés dátuma", "e11");
                MyX.Kiir("Forgalmi akadály ideje:", "a13");
                MyX.Kiir("perc", "d13");
                MyX.Kiir("Járművezető neve:", "a15");
                MyX.Kiir("Járművezető nem hivatkozott műszaki hibára.", "a17");
                MyX.Kiir("Ha a járművezető nem hivatkozott műszaki hibára, akkor a jármű fékszerkezetét és működését az üzem területén átvizsgáltam és megállapítottam, hogy ", "a19");
                MyX.Sortörésseltöbbsorba(munkalap, "A19:G20", true);
                Holtart.Lép();

                MyX.Kiir("Elektrodinamikus fék:", "a22");
                MyX.Kiir("Rögzítőfék :", "a23");
                MyX.Kiir("Sínfék :", "a24");
                MyX.Kiir("üzemképes", "c22");
                MyX.Kiir("üzemképes", "c23");
                MyX.Kiir("üzemképes", "c24");
                MyX.Kiir("Ha a járművezető műszaki hibára hivatkozott, akkor a járművet a Zavarelhárító Szolgálat szállíthatja az érintett üzembe. Gondoskodni kell a jármű esemény utáni állapotának megőrzéséről!", "a26");
                MyX.Sortörésseltöbbsorba(munkalap, "A26:G27", true);

                MyX.Kiir("Az esemény leírása", "a29");
                MyX.Betű(munkalap, "a29", BeBetű11V);
                MyX.Kiir("Baleset helyszíne:", "a30");
                MyX.Kiir("Mivel ütközött:", "a31");
                MyX.Kiir("Személyi sérülés:", "a32");
                MyX.Kiir("Becsült anyagi kár:", "a33");
                MyX.Kiir("Jármű sérülésének leírása:", "a34");
                MyX.Sortörésseltöbbsorba(munkalap, "A34:B37", true);
                MyX.Sortörésseltöbbsorba(munkalap, "C34:G37", true);

                MyX.Kiir("Egyéb esemény:", "a38");
                MyX.Kiir("Egyéb esemény rövid leírása:", "a39");
                MyX.Sortörésseltöbbsorba(munkalap, "A39:B43", true);


                MyX.Kiir("A fékvizsgálati jelentés a járművezető által kiállított 'Járművezetői jelentés közlekedési balesetről, eseményről' lap alapján készült.", "a45");
                MyX.Sortörésseltöbbsorba(munkalap, "A45:G46", true);

                MyX.Kiir("Budapest,", "a48");
                MyX.Kiir(DateTime.Today.ToStrTrim(), "b48");
                MyX.Kiir("aláírás", "e51");
                Holtart.Lép();
                // Változó adatok
                MyX.Kiir(Pályaszám.Text.ToStrTrim(), "a7");
                MyX.Betű(munkalap, "a7", BeBetű11V);
                MyX.Kiir(Típus.Text.ToStrTrim(), "c7");
                MyX.Betű(munkalap, "c7", BeBetű11V);
                MyX.Kiir(Viszonylat.Text.ToStrTrim(), "e7");
                MyX.Betű(munkalap, "e7", BeBetű11V);
                MyX.Kiir(Dátum.Value.ToString("yyyy.MM.dd"), "A9");
                MyX.Betű(munkalap, "a9", BeBetű11V);
                MyX.Kiir(Idő.Value.ToString("HH:mm"), "c9");
                MyX.Betű(munkalap, "c9", BeBetű11V);
                MyX.Kiir(Szerelvény.Text.ToStrTrim(), "c11");
                MyX.Kiir(Üzembehelyezés.Text.ToStrTrim(), "g11");
                MyX.Kiir(Forgalmiakadály.Text.ToStrTrim(), "c13");
                MyX.Kiir(Járművezető.Text.ToStrTrim(), "c15");
                MyX.Kiir(Helyszín.Text.ToStrTrim(), "c30");
                MyX.Kiir(Ütközött.Text.Trim(), "c31");
                if (!Személyi.Checked)
                    MyX.Kiir("Nem volt", "c32");
                else
                    MyX.Kiir("Volt", "c32");

                MyX.Kiir(AnyagikárÁr.Text.ToStrTrim(), "c33");
                MyX.Kiir(Leírás.Text.ToStrTrim(), "c34");
                MyX.Kiir(Esemény.Text.Trim(), "c38");
                MyX.Kiir(Leírás1.Text.ToStrTrim(), "c39");
                Holtart.Lép();

                MyX.Sortörésseltöbbsorba(munkalap, "C39:G43", true);

                MyX.Rácsoz(munkalap, "a30:g43");
                MyX.Aláírásvonal(munkalap, "e51:g51");
                Holtart.Lép();
                // kiirjuk a készítő nevét és beosztását/
                List<Adat_Dolgozó_Alap> AdatokDolg = KézDolgAlap.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adat_Dolgozó_Alap Adat = AdatokDolg.Where(a => a.Bejelentkezésinév == Program.PostásNév.Trim()).FirstOrDefault();

                if (Adat != null)
                {
                    MyX.Kiir(Adat.DolgozóNév.Trim(), "e52");
                    MyX.Kiir(Adat.Főkönyvtitulus.Trim(), "e53");
                }

                Holtart.Lép();
                // kiírjuk a szervezetet
                MyX.Kiir(Text1.Text.Trim(), "a1");
                MyX.Igazít_vízszintes(munkalap, "a1", "bal");
                MyX.Kiir(Text2.Text.Trim(), "a2");
                MyX.Igazít_vízszintes(munkalap, "a2", "bal");
                MyX.Kiir(Text3.Text.Trim(), "a3");
                MyX.Igazít_vízszintes(munkalap, "a3", "bal");
                MyX.Kiir(Text4.Text.Trim(), "a4");
                MyX.Igazít_vízszintes(munkalap, "a4", "bal");
                MyX.Kiir($"{Telephely.Text.ToStrTrim()} {Text5.Text.Trim()}", "a5");
                MyX.Igazít_vízszintes(munkalap, "a5", "bal");

                // nyomtatási terület
                Beállítás_Nyomtatás BeNyom = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = "a1:g53",
                    LapMagas = 1,
                    LapSzéles = 1,
                    FejlécKözép = Program.PostásNév,
                    FejlécJobb = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"),
                    LáblécKözép = "&P/&N"
                };
                MyX.NyomtatásiTerület_részletes(munkalap, BeNyom);
                Holtart.Ki();
                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();

                MyF.Megnyitás(fájlexc);

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
                if (Sorszám.Text.ToStrTrim() == "") throw new HibásBevittAdat("Nincs kiválasztva sorszám!");
                Holtart.Be();
                Cafkiiró();

                string fájlexc;
                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "CAF jegyzőkönyv készítés",
                    FileName = $"CAF_{Dátum.Value:yyyyMMdd}_{Pályaszám.Text.ToStrTrim()}_{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                Holtart.Lép();
                string munkalap = "Munka1";
                MyX.ExcelLétrehozás(munkalap);

                int sor;
                // formázáshoz

                // betű beállítása
                MyX.Munkalap_betű(munkalap, BeBetűCal);

                MyX.Oszlopszélesség(munkalap, "a:c", 26);
                sor = 1;
                MyX.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyX.Betű(munkalap, "a1", BeBetűCal18);

                MyX.Kiir($"{Pályaszám.Text} pályaszám - sérülés utáni járműszemle", $"a{sor}");
                MyX.Háttérszín(munkalap, $"a{sor}", Color.Silver);
                MyX.Igazít_vízszintes(munkalap, $"a{sor}", "közép");
                MyX.Rácsoz(munkalap, $"a{sor}:c{sor}");
                sor++;

                MyX.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyX.Kiir($"Járműszemle dátuma időpontja: {DateTime.Today}", $"a{sor}");
                MyX.Rácsoz(munkalap, $"a{sor}:c{sor}");
                MyX.Igazít_vízszintes(munkalap, $"a{sor}", "bal");
                sor++;
                MyX.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyX.Kiir("Jelen vannak", $"a{sor}");
                MyX.Rácsoz(munkalap, $"a{sor}:c{sor}");

                MyX.Háttérszín(munkalap, $"a{sor}", Color.Silver);

                sor++;
                MyX.Kiir("Szervezet", $"a{sor}");
                MyX.Kiir("Név", $"b{sor}");
                MyX.Kiir("Beosztás", $"c{sor}");
                MyX.Rácsoz(munkalap, $"a{sor}:c{sor}");

                if (CafTábla.Rows.Count > 0)
                {
                    for (int i = 0; i < CafTábla.Rows.Count; i++)
                    {
                        sor++;
                        MyX.Kiir(CafTábla.Rows[i].Cells[1].Value.ToStrTrim(), $"a{sor}");
                        MyX.Kiir(CafTábla.Rows[i].Cells[3].Value.ToStrTrim(), $"c{sor}");
                        MyX.Kiir(CafTábla.Rows[i].Cells[2].Value.ToStrTrim(), $"b{sor}");
                    }
                    MyX.Rácsoz(munkalap, $"a{sor - CafTábla.Rows.Count - 1}:c{sor}");
                }
                sor++;
                MyX.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyX.Vastagkeret(munkalap, $"a{sor}:c{sor}");
                MyX.Kiir("Baleset / Rongálás bekövetkezésének időpontja.", $"a{sor}:c{sor}");
                MyX.Háttérszín(munkalap, $"a{sor}:c{sor}", Color.Silver);

                sor++;
                MyX.Kiir("Dátum / idő", $"a{sor}");
                MyX.Kiir($"{Dátum.Value:yyyy.MM.dd}", $"b{sor}");
                MyX.Kiir($"{Idő.Value:HH:mm}", $"c{sor}");
                MyX.Rácsoz(munkalap, $"a{sor - 1}:c{sor}");
                sor++;
                MyX.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyX.Kiir("Baleset / Rongálás bekövetkezésének időpontjában a jármű Km állása.", $"a{sor}");
                MyX.Háttérszín(munkalap, $"a{sor - 1}:c{sor}", Color.Silver);

                sor++;
                MyX.Kiir("KM állás", $"a{sor}");
                MyX.Kiir(KmóraÁllás.Text, $"b{sor}");
                MyX.Kiir("km", $"c{sor}");
                MyX.Rácsoz(munkalap, $"a{sor - 1}:c{sor}");

                sor++;
                MyX.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyX.Kiir("Baleset/vagy rongálásal érintett kocsirészek (pl C1, S, stb)", $"a{sor}");
                MyX.Rácsoz(munkalap, $"a{sor}");
                sor++;
                MyX.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyX.Kiir(Leírás.Text, $"a{sor}");
                MyX.Rácsoz(munkalap, $"a{sor - 1}:c{sor}");

                sor++;
                MyX.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyX.Kiir("Biztosítói hibaszemle történt e? (megfelelő aláhúzandó)", $"a{sor}");
                MyX.Háttérszín(munkalap, $"a{sor}", Color.Silver);
                sor++;
                MyX.Kiir("Igen", $"a{sor}");
                MyX.Kiir("Nem", $"b{sor}");
                if (Biztosító.Text.ToStrTrim() == "_")
                    MyX.Betű(munkalap, $"b{sor}", BeBetűCalA);
                else
                    MyX.Betű(munkalap, $"a{sor}", BeBetűCalA);

                MyX.Rácsoz(munkalap, $"a{sor - 1}:c{sor}");

                sor++;
                MyX.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyX.Kiir("Baleset / Rongálás leírása (pontosan mely elemek sérültek, leírás szövegesen)." +
                    " Mindenképpen szükséges fotókat készíteni", $"a{sor}:c{sor}");
                MyX.Háttérszín(munkalap, $"a{sor}:c{sor}", Color.Silver);
                MyX.Sormagasság(munkalap, $"{sor}:{sor}", 32);
                MyX.Sortörésseltöbbsorba(munkalap, $"a{sor}:c{sor}", true);
                MyX.Rácsoz(munkalap, $"a{sor - 6}:c{sor}");
                sor++;
                MyX.Egyesít(munkalap, $"a{sor}:c{sor + 5}");
                MyX.Kiir(Leírás.Text, $"b{sor}");
                MyX.Sortörésseltöbbsorba(munkalap, $"a{sor}:c{sor + 6}", true);
                MyX.Rácsoz(munkalap, $"a{sor}:c{sor + 6}");
                Holtart.Lép();
                sor += 6;
                MyX.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyX.Kiir("A baleset / rongálás okozója", $"a{sor}");
                MyX.Háttérszín(munkalap, $"a{sor}", Color.Silver);
                sor++;
                MyX.Egyesít(munkalap, $"a{sor}:c{sor + 1}");
                string szöveg = "";
                if (Sajáthiba.Checked)
                    szöveg = "Saját jármű";
                if (Idegenhiba.Checked)
                    szöveg = "Idegen jármű";
                MyX.Kiir(szöveg, $"a{sor}");
                sor++;
                MyX.Rácsoz(munkalap, $"a{sor - 2}:c{sor}");

                sor++;
                MyX.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyX.Kiir("Baleset / Rongálás javításához felhasznált alkatrészek", $"a{sor}");
                MyX.Háttérszín(munkalap, $"a{sor}", Color.Silver);
                sor++;
                MyX.Kiir("Megnevezés", $"a{sor}");
                MyX.Kiir("Cikkszám", $"b{sor}");
                MyX.Kiir("Darabszám", $"c{sor}");
                MyX.Rácsoz(munkalap, $"a{sor - 1}:c{sor}");

                sor += 5;
                MyX.Rácsoz(munkalap, $"a{sor - 4}:c{sor}");

                sor++;
                MyX.Egyesít(munkalap, $"a{sor}:c{sor + 1}");
                MyX.Kiir("Javítási módszer meghatározása, ha van rá pontos technológia annak számát kell beírni, ha nincs szövegesen kell leírni a javítást.", $"a{sor}:c{sor + 1}");
                MyX.Háttérszín(munkalap, $"a{sor}", Color.Silver);
                MyX.Sortörésseltöbbsorba(munkalap, $"a{sor}", true);
                sor += 2;
                MyX.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyX.Kiir("CAJ-00-001 Korrózióvédelem c. technológiai utasítás", $"a{sor}");
                MyX.Igazít_vízszintes(munkalap, $"a{sor}", "bal");
                sor++;
                MyX.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyX.Kiir("CAJ-00-002 Meghúzási nyomatékok c. technológiai utasítás", $"a{sor}");
                MyX.Igazít_vízszintes(munkalap, $"a{sor}", "bal");
                sor++;
                MyX.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyX.Rácsoz(munkalap, $"a{sor - 4}:c{sor}");

                Holtart.Lép();
                sor++;
                MyX.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyX.Háttérszín(munkalap, $"a{sor}", Color.Silver);
                MyX.Kiir("A fenti javítást ki végzi el? (megfelelő aláhúzandó)", $"a{sor}");
                sor++;
                MyX.Vastagkeret(munkalap, $"a{sor - 3}:c{sor}");
                sor++;
                MyX.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyX.Háttérszín(munkalap, $"a{sor}", Color.Silver);
                MyX.Kiir("A fenti javítási technológiát elfogadják, javítás megkezdhető:", $"a{sor}");
                MyX.Rácsoz(munkalap, $"a{sor}:c{sor}");
                sor++;
                MyX.Kiir("Szervezet", $"a{sor}");
                MyX.Kiir("Név", $"b{sor}");
                MyX.Kiir("Aláírás", $"c{sor}");
                MyX.Rácsoz(munkalap, $"a{sor}:c{sor}");

                if (CafTábla.Rows.Count > 0)
                {
                    for (int i = 0; i < CafTábla.Rows.Count; i++)
                    {
                        sor++;
                        MyX.Kiir(CafTábla.Rows[i].Cells[1].Value.ToStrTrim(), $"a{sor}");
                        MyX.Kiir(CafTábla.Rows[i].Cells[2].Value.ToStrTrim(), $"b{sor}");
                        MyX.Sormagasság(munkalap, $"{sor - CafTábla.Rows.Count + 2}:{sor}", 32);
                    }
                    MyX.Rácsoz(munkalap, $"a{sor - CafTábla.Rows.Count + 1}:c{sor}");


                }
                Holtart.Lép();
                sor++;
                MyX.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyX.Betű(munkalap, $"a{sor}", BeBetűCal18);
                MyX.Kiir("Javítás utáni visszaellenőrzés", $"a{sor}");
                MyX.Háttérszín(munkalap, $"a{sor}", Color.Silver);
                MyX.Rácsoz(munkalap, $"a{sor}:c{sor}");
                sor++;
                MyX.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyX.Rácsoz(munkalap, $"a{sor}:c{sor}");
                sor++;
                MyX.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyX.Kiir("Dátum:", $"a{sor}");
                MyX.Rácsoz(munkalap, $"a{sor}:c{sor}");
                MyX.Igazít_vízszintes(munkalap, $"a{sor}", "bal");
                sor++;
                MyX.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyX.Kiir("A Javítást felek átnézték, az alábbi észrevételeket teszik:", $"a{sor}");
                MyX.Rácsoz(munkalap, $"a{sor}:c{sor}");
                MyX.Háttérszín(munkalap, $"a{sor}", Color.Silver);
                sor++;
                MyX.Egyesít(munkalap, $"a{sor}:c{sor + 3}");
                MyX.Rácsoz(munkalap, $"a{sor}:c{sor + 3}");
                sor += 4;
                MyX.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyX.Kiir("Javításnál BKV készletből felhasznált erőforrások (idő, anyag):", $"a{sor}");
                MyX.Háttérszín(munkalap, $"a{sor}", Color.Silver);
                sor++;
                MyX.Kiir("Megnevezés", $"a{sor}");
                MyX.Kiir("Cikkszám/rendelési szám", $"b{sor}");
                MyX.Kiir("Darabszám/Idő", $"c{sor}");
                sor += 2;
                MyX.Kiir("Munkaidő", $"a{sor}");
                MyX.Rácsoz(munkalap, $"a{sor - 3}:c{sor}");

                sor++;
                MyX.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyX.Kiir("Megjegyzések", $"a{sor}");
                MyX.Háttérszín(munkalap, $"a{sor}", Color.Silver);
                sor++;
                MyX.Egyesít(munkalap, $"a{sor}:c{sor}");
                MyX.Kiir("A felek a javítás eredményét együttesen átnézték, BKV nyilatkozik hogy az előírt technológiának megfelelően végezte el a javítást." +
                    " A javítás kivitelezését a fenti megjegyzések figyelembevételével elfogadják. A CAF a szállítási szerződés szerinti garanciát a járműre " +
                    "és a CAF által szállított alkatrészekre fenntartja.", $"a{sor}");
                MyX.Sormagasság(munkalap, $"{sor}:{sor}", 60);
                MyX.Sortörésseltöbbsorba(munkalap, $"a{sor}:c{sor}", true);
                MyX.Rácsoz(munkalap, $"a{sor - 1}:c{sor}");

                sor++;
                MyX.Kiir("Szervezet", $"a{sor}");
                MyX.Kiir("Név", $"b{sor}");
                MyX.Kiir("Aláírás", $"c{sor}");
                MyX.Rácsoz(munkalap, $"a{sor}:c{sor}");

                Holtart.Lép();
                if (CafTábla.Rows.Count > 0)
                {
                    for (int i = 0; i < CafTábla.Rows.Count; i++)
                    {
                        sor++;
                        MyX.Kiir(CafTábla.Rows[i].Cells[1].Value.ToStrTrim(), $"a{sor}");
                        MyX.Kiir(CafTábla.Rows[i].Cells[2].Value.ToStrTrim(), $"b{sor}");
                        MyX.Sormagasság(munkalap, $"{sor - CafTábla.Rows.Count + 2}:{sor}", 32);
                    }
                    MyX.Rácsoz(munkalap, $"a{sor - CafTábla.Rows.Count + 1}:c{sor}");


                }
                // nyomtatási terület
                Beállítás_Nyomtatás BeNyom = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"a1:c{sor}",
                    LapSzéles = 1,
                    FejlécKözép = Program.PostásNév,
                    FejlécJobb = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"),
                    LáblécKözép = "&P/&N"
                };
                MyX.NyomtatásiTerület_részletes(munkalap, BeNyom);

                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();

                Holtart.Ki();
                MyF.Megnyitás(fájlexc);
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
                if (!int.TryParse(Sorszám.Text, out int sorszám)) throw new HibásBevittAdat("Nincs ilyen sorszám");

                List<Adat_Sérülés_Jelentés> Adatok = KézSérülésJelentés.Lista_Adatok(Dátum.Value.Year) ?? throw new HibásBevittAdat("A beállított dátumra nincs adatbázis létrehozva!");
                Adat_Sérülés_Jelentés Elem = (from a in Adatok
                                              where a.Sorszám == sorszám
                                              select a).FirstOrDefault();

                if (Elem != null)
                {
                    KézSérülésJelentés.VisszaÁllít(Dátum.Value.Year, sorszám);
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
                if (!int.TryParse(Rendelésszám.Text.Trim(), out int rendelésszám))
                {
                    rendelésszám = 0;
                    Rendelésszám.Text = "0";
                }
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

                AdatokSérülésJelentés = KézSérülésJelentés.Lista_Adatok(Dátum.Value.Year);

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
                DateTime DátumIdő = new DateTime(Dátum.Value.Year, Dátum.Value.Month, Dátum.Value.Day, Idő.Value.Hour, Idő.Value.Minute, Idő.Value.Second);
                int kimenetel = 1;
                if (Idegenhiba.Checked) kimenetel = 2;
                if (Személyhiba.Checked) kimenetel = 3;
                if (Egyébhiba.Checked) kimenetel = 4;
                int státus = 1;
                if (Opt_Elkészült.Checked) státus = 2;
                if (Opt_Törölt.Checked) státus = 9;

                Adat_Sérülés_Jelentés ADAT = new Adat_Sérülés_Jelentés(
                    Sorszám.Text.ToÉrt_Int(),
                    Telephely.Text.Trim(),
                    DátumIdő,
                    Helyszín.Text.Trim(),
                    Viszonylat.Text.Trim(),
                    Pályaszám.Text.Trim(),
                    Járművezető.Text.Trim(),
                    Rendelésszám.Text.ToÉrt_Int(),
                    kimenetel,
                    státus,
                    "_",
                    Típus.Text.Trim(),
                    Szerelvény.Text.Trim(),
                    Forgalmiakadály.Text.ToÉrt_Int(),
                    Műszakihiba.Checked,
                    Anyagikár.Checked,
                    Biztosító.Text.Trim(),
                    Személyi.Checked,
                    false,
                    Gyors.Checked ? 1 : 2,
                    Ütközött.Text.Trim(),
                    anyagikára,
                    Leírás.Text.Trim(),
                    Leírás1.Text.Trim(),
                    "_",
                    Esemény.Text.Trim(),
                    0,
                    1,
                    KmóraÁllás.Text.Trim());

                if (új == 0)
                {
                    // Módosítás
                    KézSérülésJelentés.Módosítás(Dátum.Value.Year, ADAT);
                }
                else
                {
                    // rögzítés
                    KézSérülésJelentés.Rögzítés(Dátum.Value.Year, ADAT);
                }
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

                List<Adat_Jármű> AdatokJármű = KézJármű.Lista_Adatok("Főmérnökség");
                AdatJármű = AdatokJármű.Where(a => a.Azonosító == Pályaszám.Text.Trim()).FirstOrDefault();


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
                AdatokJármű = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                AdatJármű = AdatokJármű.Where(a => a.Azonosító == Pályaszám.Text.Trim()).FirstOrDefault();

                if (AdatJármű != null)
                {
                    double szerelvénykocsik = AdatJármű.Szerelvénykocsik;
                    if (szerelvénykocsik != 0d)
                    {
                        List<Adat_Szerelvény> AdatokSzerelvény = KézSzerelvény.Lista_Adatok(Cmbtelephely.Text.Trim());
                        Adat_Szerelvény Adat2 = AdatokSzerelvény.Where(a => a.Szerelvény_ID == szerelvénykocsik).FirstOrDefault();
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