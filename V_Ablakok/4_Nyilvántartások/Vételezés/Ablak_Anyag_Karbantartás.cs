using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Villamos.V_Kezelők;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.V_Ablakok._4_Nyilvántartások.Vételezés
{
    public partial class Ablak_Anyag_Karbantartás : Form
    {
        readonly Kezelő_AnyagTörzs KézAnyag = new Kezelő_AnyagTörzs();
        List<Adat_Anyagok> Adatok = new List<Adat_Anyagok>();

        DataTable AdatTábla = new DataTable();
        public Ablak_Anyag_Karbantartás()
        {
            InitializeComponent();
            Start();
        }


        #region Alap 
        private void Ablak_Anyag_Karbantartás_Load(object sender, EventArgs e)
        { }

        private void Start()
        {
            Adatok = KézAnyag.Lista_Adatok();
            TáblaÍrás();
        }

        private void BtnSúgó_Click(object sender, System.EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\akkumulátor.html";
                Module_Excel.Megnyitás(hely);
            }
            catch (HibásBevittAdat ex)
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

        #region Táblázat
        private void Frissíti_táblalistát_Click(object sender, EventArgs e)
        {
            TáblaÍrás();
        }

        private void TáblaÍrás()
        {
            Fejléc();
            ABFeltöltése();
            Tábla.DataSource = AdatTábla;
            OszlopSzélesség();
            Tábla.Refresh();
            Tábla.Visible = true;
            Tábla.ClearSelection();
        }

        private void ABFeltöltése()
        {
            try
            {
                AdatTábla.Clear();
                foreach (Adat_Anyagok rekord in Adatok)
                {
                    DataRow Soradat = AdatTábla.NewRow();
                    Soradat["Cikkszám"] = rekord.Cikkszám;
                    Soradat["Megnevezés"] = rekord.Megnevezés;
                    Soradat["Kereső fogalom"] = rekord.KeresőFogalom;
                    Soradat["Sarzs"] = rekord.Sarzs;
                    Soradat["Ár"] = rekord.Ár;
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

        private void Fejléc()
        {
            try
            {
                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Cikkszám");
                AdatTábla.Columns.Add("Megnevezés");
                AdatTábla.Columns.Add("Kereső fogalom");
                AdatTábla.Columns.Add("Sarzs");
                AdatTábla.Columns.Add("Ár");
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OszlopSzélesség()
        {
            Tábla.Columns["Cikkszám"].Width = 80;
            Tábla.Columns["Megnevezés"].Width = 80;
            Tábla.Columns["Kereső fogalom"].Width = 430;
            Tábla.Columns["Sarzs"].Width = 430;
            Tábla.Columns["Ár"].Width = 430;

        }
        #endregion


    }
}
