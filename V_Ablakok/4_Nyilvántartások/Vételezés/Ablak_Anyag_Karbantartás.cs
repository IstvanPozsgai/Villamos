using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Villamos.V_Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

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
            //Ha az első karakter "R" akkor az új jogosultságkiosztást használjuk
            //ha nem akkor a régit használjuk
            if (Program.PostásJogkör.Substring(0, 1) == "R")
                GombLathatosagKezelo.Beallit(this, "Főmérnökség");
            else
            {
            }
            TáblaÍrás();
        }

        private void BtnSúgó_Click(object sender, System.EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\akkumulátor.html";
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
        #endregion

        #region Táblázat
        private void Frissíti_táblalistát_Click(object sender, EventArgs e)
        {
            TáblaÍrás();
        }

        private void TáblaÍrás()
        {
            Tábla.CleanFilterAndSort();
            Adatok = KézAnyag.Lista_Adatok();
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
            Tábla.Columns["Cikkszám"].Width = 130;
            Tábla.Columns["Megnevezés"].Width = 400;
            Tábla.Columns["Kereső fogalom"].Width = 400;
            Tábla.Columns["Sarzs"].Width = 80;
            Tábla.Columns["Ár"].Width = 80;

        }

        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Cikkszám.Text = Tábla.CurrentRow.Cells["Cikkszám"].Value.ToString();
            Megnevezés.Text = Tábla.CurrentRow.Cells["Megnevezés"].Value.ToString();
            KeresőFogalom.Text = Tábla.CurrentRow.Cells["Kereső fogalom"].Value.ToString();
            Sarzs.Text = Tábla.CurrentRow.Cells["Sarzs"].Value.ToString();
        }

        #endregion

        private void BtnRögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Cikkszám.Text.Trim() == "") throw new HibásBevittAdat("A cikkszám megadása kötelező!");
                Adat_Anyagok ADAT = new Adat_Anyagok(
                    Cikkszám.Text.Trim(),
                    MyF.Szöveg_Tisztítás(Megnevezés.Text.Trim(), 0, 255),
                    MyF.Szöveg_Tisztítás(KeresőFogalom.Text.Trim()),
                    MyF.Szöveg_Tisztítás(Sarzs.Text.Trim(), 0, 5),
                    0);
                KézAnyag.DöntésEgyedi(ADAT);
                TáblaÍrás();
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
