using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Ablakok._4_Nyilvántartások.Sérülés;
using MyE = Villamos.Module_Excel;
using MyEn = Villamos.V_MindenEgyéb.Enumok;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.TTP
{
    public partial class Ablak_TTP_Történet : Form
    {
        readonly Kezelő_TTP_Tábla KézTábla = new Kezelő_TTP_Tábla();

        public event Event_Kidobó Változás;

        public string Azonosító { get; set; }
        List<Adat_TTP_Tábla> AdatokTeljes { get; set; }
        public List<Adat_Jármű> AdatokJármű { get; set; }
        public string Művelet { get; set; }
        public DateTime ÜtemezésDátum { get; set; }

        readonly Kezelő_jármű_hiba KézHiba = new Kezelő_jármű_hiba();

        public Ablak_TTP_Történet(string azonosító, List<Adat_Jármű> adatokJármű, List<Adat_TTP_Tábla> adatokTeljes, string művelet, DateTime ütemezésDátum)
        {
            InitializeComponent();
            Azonosító = azonosító;
            AdatokJármű = adatokJármű;
            AdatokTeljes = adatokTeljes;
            Művelet = művelet;
            ÜtemezésDátum = ütemezésDátum;
        }

        private void Ablak_TTP_Történet_Load(object sender, EventArgs e)
        {
            BevitelÜres();
            Pályaszámok_feltöltése();
            StátusokFeltöltése();
            Jogosultságkiosztás();
            AdatokTeljes = KézTábla.Lista_Adatok();
            CmbAzonosító.Text = Azonosító;
            TáblaListázás();
            MindenInAktív();
            ListázandóElem(Azonosító, ÜtemezésDátum);
            BeállítjaAzAktív();
        }

        private void Ablak_TTP_Történet_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_PDF_Feltöltés?.Close();
        }



        #region Alap

        private void StátusokFeltöltése()
        {
            try
            {
                foreach (string adat in Enum.GetNames(typeof(MyEn.TTP_Státus)))
                    CmbStátus.Items.Add(adat);
            }
            catch (HibásBevittAdat ex)
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


        #region Form vezérlők

        private void Pályaszámok_feltöltése()
        {
            try
            {
                foreach (Adat_Jármű rekord in AdatokJármű)
                    CmbAzonosító.Items.Add(rekord.Azonosító);

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void CmbAzonosító_SelectedIndexChanged(object sender, EventArgs e)
        {
            Azonosító = CmbAzonosító.Text.Trim();
            BevitelÜres();
            TáblaListázás();
        }
        #endregion


        #region Jogosultság beállításhoz

        private void MindenInAktív()
        {
            DtLejárat.Enabled = false;
            DtÜtemezés.Enabled = false;
            TxtEgyütt.Enabled = false;
            DtTTPDátum.Enabled = false;
            ChkTTPJavítás.Enabled = false;
            TxtRendelés.Enabled = false;
            DtJavBefDát.Enabled = false;
            CmbStátus.Enabled = false;
            TxtMegjegyzés.Enabled = false;
        }

        private void MindenAktív()
        {
            DtLejárat.Enabled = true;
            DtÜtemezés.Enabled = true;
            TxtEgyütt.Enabled = true;
            DtTTPDátum.Enabled = true;
            ChkTTPJavítás.Enabled = true;
            TxtRendelés.Enabled = true;
            DtJavBefDát.Enabled = true;
            CmbStátus.Enabled = true;
            TxtMegjegyzés.Enabled = true;
        }

        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;

                // ide kell az összes gombot tenni amit szabályozni akarunk false
                Btn_TTP_Rögz.Visible = false;
                BtnPDFFel.Visible = false;

                // csak főmérnökségi belépéssel törölhető
                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {

                }
                else
                {

                }

                melyikelem = 131;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Btn_TTP_Rögz.Visible = true;
                    BtnPDFFel.Visible = true;
                }

                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                { }
                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 3))
                { }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BeállítjaAzAktív()
        {
            switch (Művelet)
            {
                case "KészJó":
                    DtTTPDátum.Enabled = true;
                    CmbStátus.Text = Enum.GetName(typeof(MyEn.TTP_Státus), 8);
                    RendeklésLátszik(false);
                    return;
                case "KészJav":
                    DtTTPDátum.Enabled = true;
                    ChkTTPJavítás.Enabled = true;
                    ChkTTPJavítás.Checked = true;
                    CmbStátus.Text = Enum.GetName(typeof(MyEn.TTP_Státus), 5);
                    RendeklésLátszik(false);
                    return;
                case "JavKész":
                    TxtRendelés.Enabled = true;
                    DtJavBefDát.Enabled = true;
                    CmbStátus.Text = Enum.GetName(typeof(MyEn.TTP_Státus), 8);
                    return;
                case "Összes":
                    if (Program.PostásTelephely.Trim() == "Főmérnökség") MindenAktív();
                    return;
            }
        }

        private void RendeklésLátszik(bool érték)
        {
            label5.Visible = érték;
            TxtRendelés.Visible = érték;
            label6.Visible = érték;
            DtJavBefDát.Visible = érték;
        }

        #endregion


        #region Tábla Műveletek
        private void TáblaListázás()
        {
            try
            {
                List<Adat_TTP_Tábla> Ideig = (from a in AdatokTeljes
                                              where a.Azonosító == Azonosító
                                              orderby a.TTP_Dátum
                                              select a).ToList();
                if (Ideig != null)
                    Tábla.DataSource = AdatTábla_TTP_TáblaFeltölt(Ideig);
                else
                    Tábla.DataSource = null;

                Oszlopszélesség();
                Tábla.Visible = true;
                Tábla.ClearSelection();
                Tábla.CleanFilterAndSort();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Oszlopszélesség()
        {
            Tábla.Columns["Pályaszám"].Width = 110;
            Tábla.Columns["Lejárat dátum"].Width = 110;
            Tábla.Columns["Ütemezés dátum"].Width = 110;
            Tábla.Columns["TTP dátum"].Width = 110;
            Tábla.Columns["TTP Javítás"].Width = 110;
            Tábla.Columns["Rendelés"].Width = 110;
            Tábla.Columns["Javítás befejező dátum"].Width = 110;
            Tábla.Columns["Szerelvény"].Width = 110;
            Tábla.Columns["Státus"].Width = 110;
            Tábla.Columns["Megjegyzés"].Width = 110;
        }

        private void BtnFrissít_Click(object sender, EventArgs e)
        {
            Azonosító = CmbAzonosító.Text.Trim();
            BevitelÜres();
            TáblaListázás();
            PDFNéz.Visible = false;
        }

        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Művelet != "Összes") return;
            if (e.RowIndex < 0) return;
            if (CmbAzonosító.Text.Trim() == "") return;

            DateTime üdátum = Tábla.Rows[e.RowIndex].Cells["Ütemezés dátum"].Value.ToÉrt_DaTeTime();
            ListázandóElem(CmbAzonosító.Text.Trim(), üdátum);
            PDFNéz.Visible = MyF.VanPDF(Azonosító, üdátum);
        }

        private void BevitelÜres()
        {
            DtLejárat.Value = new DateTime(1900, 1, 1);
            DtÜtemezés.Value = new DateTime(1900, 1, 1);
            TxtEgyütt.Text = "";
            DtTTPDátum.Value = new DateTime(1900, 1, 1);
            ChkTTPJavítás.Checked = false;
            TxtRendelés.Text = "";
            DtJavBefDát.Value = new DateTime(1900, 1, 1);
            CmbStátus.Text = "";
            TxtMegjegyzés.Text = "";
        }

        private void ListázandóElem(string pályaszám, DateTime üDátum)
        {
            BevitelÜres();
            Adat_TTP_Tábla rekord = (from a in AdatokTeljes
                                     where a.Azonosító == pályaszám && a.Ütemezés_Dátum == üDátum
                                     select a).FirstOrDefault();
            if (rekord != null)
            {
                DtLejárat.Value = rekord.Lejárat_Dátum;
                DtÜtemezés.Value = rekord.Ütemezés_Dátum;
                TxtEgyütt.Text = rekord.Együtt;
                DtTTPDátum.Value = rekord.TTP_Dátum;
                ChkTTPJavítás.Checked = rekord.TTP_Javítás;
                TxtRendelés.Text = rekord.Rendelés;
                DtJavBefDát.Value = rekord.JavBefDát;
                CmbStátus.Text = Enum.GetName(typeof(MyEn.TTP_Státus), rekord.Státus);
                TxtMegjegyzés.Text = rekord.Megjegyzés;

                //Ha most rögzítjük a tényt akkor felkínáljuk az ütemezés dátumát
                if (Művelet == "KészJó" || Művelet == "KészJav") DtTTPDátum.Value = rekord.Ütemezés_Dátum;
                if (Művelet == "JavKész") DtJavBefDát.Value = DateTime.Today;
            }
        }
        #endregion


        #region Adatok módosítása rögzítése
        private void Btn_TTP_Rögz_Click(object sender, EventArgs e)
        {
            try
            {
                if (DtÜtemezés.Value == new DateTime(1900, 1, 1)) return;
                Adat_TTP_Tábla Elem = new Adat_TTP_Tábla(
                                CmbAzonosító.Text.Trim(),
                                DtLejárat.Value,
                                DtÜtemezés.Value,
                                DtTTPDátum.Value,
                                ChkTTPJavítás.Checked,
                                TxtRendelés.Text.Trim(),
                                DtJavBefDát.Value,
                                TxtEgyütt.Text.Trim(),
                                ((MyEn.TTP_Státus)Enum.Parse(typeof(MyEn.TTP_Státus), CmbStátus.Text.Trim())).GetHashCode(),
                                TxtMegjegyzés.Text.Trim()
                                );
                KézTábla.TTP_AdatTábla_Vizsgál(Elem, AdatokTeljes);
                if (Művelet == "KészJav") SzabadHiba();
                AdatokTeljes = KézTábla.Lista_Adatok();
                TáblaListázás();
                Változás?.Invoke();
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void SzabadHiba()
        {
            try
            {
                Adat_Jármű Egyed = (from a in AdatokJármű
                                    where a.Azonosító == CmbAzonosító.Text.Trim()
                                    select a).FirstOrDefault();
                if (Egyed != null)
                {
                    Adat_Jármű_hiba Elem = new Adat_Jármű_hiba(
                                         Program.PostásNév.Trim(),
                                         1,
                                         "TTP javítási feladadatok",
                                         DateTime.Now,
                                         false,
                                         Egyed.Valóstípus,
                                         CmbAzonosító.Text.Trim(),
                                         1);
                    KézHiba.Rögzítés(Egyed.Üzem.Trim(), Elem);
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


        #region PDF Feltöltés

        Ablak_PDF_Feltöltés Új_Ablak_PDF_Feltöltés;

        private void BtnPDFFel_Click(object sender, EventArgs e)
        {
            try
            {
                Új_Ablak_PDF_Feltöltés?.Close();

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\TTP\PDF\";
                Új_Ablak_PDF_Feltöltés = new Ablak_PDF_Feltöltés(hely, DtTTPDátum.Value, 0, 0, CmbAzonosító.Text.Trim(), null, "TTP", false)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };
                Új_Ablak_PDF_Feltöltés.FormClosed += Új_Ablak_Sérülés_PDF_Closed;
                Új_Ablak_PDF_Feltöltés.Show();
                //  Új_Ablak_PDF_Feltöltés.Változás += Pdflistázása;
            }
            catch (HibásBevittAdat ex)
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
            Új_Ablak_PDF_Feltöltés = null;
        }


        private void PDFNéz_Click(object sender, EventArgs e)
        {
            try
            {
                Új_Ablak_PDF_Feltöltés?.Close();

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\TTP\PDF\";
                Új_Ablak_PDF_Feltöltés = new Ablak_PDF_Feltöltés(hely, DtTTPDátum.Value, 0, 0, CmbAzonosító.Text.Trim(), null, "TTP", true)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };
                Új_Ablak_PDF_Feltöltés.FormClosed += Új_Ablak_Sérülés_PDF_Closed;
                Új_Ablak_PDF_Feltöltés.Show();
                //  Új_Ablak_PDF_Feltöltés.Változás += Pdflistázása;
            }
            catch (HibásBevittAdat ex)
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

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "TTP_Történeti_" + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMddhhmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;
                BtnExcel.Visible = false;
                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);

                DataTable ValamiTábla;
                ValamiTábla = AdatTábla_TTP_TáblaFeltölt(AdatokTeljes);
                MyE.EXCELtábla(ValamiTábla, fájlexc);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MyE.Megnyitás(fájlexc + ".xlsx");
                BtnExcel.Visible = true;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                BtnExcel.Visible = true;
            }
        }

        DataTable AdatTábla_TTP_TáblaFeltölt(List<Adat_TTP_Tábla> Adatok)
        {
            DataTable AdatTábla = new DataTable();
            try
            {
                //Tábla mezőnevek
                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Pályaszám");
                AdatTábla.Columns.Add("Lejárat dátum", typeof(DateTime));
                AdatTábla.Columns.Add("Ütemezés dátum", typeof(DateTime));
                AdatTábla.Columns.Add("TTP dátum", typeof(DateTime));
                AdatTábla.Columns.Add("TTP Javítás");
                AdatTábla.Columns.Add("Rendelés");
                AdatTábla.Columns.Add("Javítás befejező dátum", typeof(DateTime));
                AdatTábla.Columns.Add("Szerelvény");
                AdatTábla.Columns.Add("Státus");
                AdatTábla.Columns.Add("Megjegyzés");


                foreach (Adat_TTP_Tábla rekord in Adatok)
                {
                    DataRow Soradat = AdatTábla.NewRow();
                    Soradat["Pályaszám"] = rekord.Azonosító;
                    Soradat["Lejárat dátum"] = rekord.Lejárat_Dátum;
                    Soradat["Ütemezés dátum"] = rekord.Ütemezés_Dátum;
                    Soradat["TTP dátum"] = rekord.TTP_Dátum;
                    Soradat["TTP Javítás"] = rekord.TTP_Javítás == true ? "Igen" : "Nem";
                    Soradat["Rendelés"] = rekord.Rendelés;
                    if (rekord.TTP_Javítás) Soradat["Javítás befejező dátum"] = rekord.JavBefDát;

                    Soradat["Szerelvény"] = rekord.Együtt;
                    Soradat["Státus"] = Enum.GetName(typeof(MyEn.TTP_Státus), rekord.Státus);
                    Soradat["Megjegyzés"] = rekord.Megjegyzés;
                    AdatTábla.Rows.Add(Soradat);
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "TTP_VezénylésFeltölt", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return AdatTábla;
        }

    }
}
