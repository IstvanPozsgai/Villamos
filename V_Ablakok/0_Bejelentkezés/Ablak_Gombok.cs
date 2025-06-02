using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using MyE = Villamos.Module_Excel;

namespace Villamos.Ablakok
{
    public partial class Ablak_Gombok : Form
    {
        readonly Kezelők_Gombok Kéz = new Kezelők_Gombok();
        DataTable AdatTáblaALap = new DataTable();
        List<Adat_Gombok> Adatok = new List<Adat_Gombok>();

        public Ablak_Gombok()
        {
            InitializeComponent();
            Start();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Start()
        {
            GombokFeltöltése();
            FormFeltöltése();
            Adatok = Kéz.Lista_Adatok();
            Alap_tábla_író();
            //   GombLathatosagKezelo.Beallit(this);
        }

        private void Ablak_Anyagok_Load(object sender, System.EventArgs e)
        {

        }


        /// <summary>
        /// Előkészítjük a beviteli mezőket az új adat fogadására
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Új_adat_Click(object sender, System.EventArgs e)
        {
            TxtId.Text = "";
            Ablaknév.Text = "";
            GombNév.Text = "";
            GombFelirat.Text = "";
            Láthatóság.Checked = false;
            Törölt.Checked = false;
        }

        /// <summary>
        /// A kiválasztott ablak gombjainak listája
        /// </summary>
        private void GombokFeltöltése()
        {
            try
            {
                if (Ablaknév.Text.Trim() == "") return;
                GombNév.Items.Clear();
                List<Button> gombok = AblakokGombok.FormbanlévőGombok(Ablaknév.Text.Trim());
                if (gombok == null) return;
                GombNév.Items.Add("");
                foreach (Button item in gombok)
                    GombNév.Items.Add(item.Name);

                GombNév.Text = "";
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
        /// Feltöltjük a formok listáját a comboboxba
        /// </summary>
        private void FormFeltöltése()
        {
            try
            {
                Ablaknév.Items.Add("");
                List<Type> Adatok = AblakokGombok.FormokListázásaType().OrderBy(a => a.Name).ToList();
                foreach (Type item in Adatok)
                {
                    Ablaknév.Items.Add(item.Name);
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
        /// Rögzítjük az új adatokat a táblába
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Alap_Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(GombNév.Text.Trim())) throw new HibásBevittAdat("Kérem adja meg a Menü nevét!");

                Adat_Gombok adat = new Adat_Gombok(
                        TxtId.Text.ToÉrt_Int(),
                        Ablaknév.Text.ToStrTrim(),
                        GombNév.Text.ToStrTrim(),
                        GombFelirat.Text.ToStrTrim(),
                        Láthatóság.Checked,
                        Törölt.Checked);

                Kéz.Döntés(adat);
                Adatok = Kéz.Lista_Adatok();
                Alap_tábla_író();
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
        /// Frissíti a táblázat adatait
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFrissít_Click(object sender, EventArgs e)
        {
            Alap_tábla_író();
        }

        /// <summary>
        /// A táblázat adatait írja ki a DataGridView-ba
        /// </summary>
        private void Alap_tábla_író()
        {
            try
            {
                Tábla.Visible = false;
                Tábla.CleanFilterAndSort();
                AlapTáblaFejléc();
                AlapTáblaTartalom();
                Tábla.DataSource = AdatTáblaALap;
                AlapTáblaOszlopSzélesség();
                Tábla.Visible = true;
                Tábla.Refresh();
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
        /// Az adatok kiírása a táblázatba
        /// </summary>
        private void AlapTáblaTartalom()
        {
            AdatTáblaALap.Clear();
            Adatok = (from a in Adatok
                      orderby a.FromName, a.GombName
                      select a).ToList();
            foreach (Adat_Gombok rekord in Adatok)
            {
                DataRow Soradat = AdatTáblaALap.NewRow();

                Soradat["Gomb Id"] = rekord.GombokId;
                Soradat["Form Név"] = rekord.FromName;
                Soradat["Gomb Leírás"] = rekord.GombFelirat;
                Soradat["Gomb Név"] = rekord.GombName;
                Soradat["Látható"] = rekord.Látható ? "Igen" : "Nem";
                Soradat["Törölt"] = rekord.Törölt ? "Igen" : "Nem";
                AdatTáblaALap.Rows.Add(Soradat);
            }
        }

        /// <summary>
        /// Fejléc beállítása a táblázatnak
        /// </summary>
        private void AlapTáblaFejléc()
        {
            try
            {
                AdatTáblaALap.Columns.Clear();
                AdatTáblaALap.Columns.Add("Gomb Id");
                AdatTáblaALap.Columns.Add("Gomb Leírás");
                AdatTáblaALap.Columns.Add("Gomb Név");
                AdatTáblaALap.Columns.Add("Form Név");
                AdatTáblaALap.Columns.Add("Látható");
                AdatTáblaALap.Columns.Add("Törölt");
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
        /// Oldalszélesség beállítása a táblázatnak
        /// </summary>
        private void AlapTáblaOszlopSzélesség()
        {
            Tábla.Columns["Gomb Id"].Width = 130;
            Tábla.Columns["Form Név"].Width = 400;
            Tábla.Columns["Gomb Leírás"].Width = 400;
            Tábla.Columns["Gomb Név"].Width = 450;
            Tábla.Columns["Látható"].Width = 100;
            Tábla.Columns["Törölt"].Width = 100;

        }

        /// <summary>
        /// Táblázatot kimenti Excelbe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Menük_és_Ablakok_{Program.PostásNév}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Tábla, true);
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
        /// Kiírja a kiválasztott sor adatait a beviteli mezőkbe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int ID = Tábla.Rows[e.RowIndex].Cells[0].Value.ToÉrt_Int();
            Adatokkiírása(ID);
        }

        /// <summary>
        /// Kiválasztott adat listázása a beviteli mezőbe
        /// </summary>
        /// <param name="ID"></param>
        private void Adatokkiírása(int ID)
        {
            try
            {
                Adat_Gombok adat = (from a in Adatok
                                    where a.GombokId == ID
                                    select a).FirstOrDefault();
                if (adat == null) return;
                TxtId.Text = adat.GombokId.ToString();
                GombFelirat.Text = adat.GombFelirat;
                GombNév.Text = adat.GombName;
                Ablaknév.Text = adat.FromName;
                Láthatóság.Checked = adat.Látható;
                Törölt.Checked = adat.Törölt;
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

        private void Ablaknév_SelectionChangeCommitted(object sender, EventArgs e)
        {
            TxtId.Text = "";
            Ablaknév.Text = Ablaknév.Items[Ablaknév.SelectedIndex].ToString();
            GombokFeltöltése();
        }
    }
}
