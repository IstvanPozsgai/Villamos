using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;

namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.TTP
{
    public partial class Ablak_TTP_Alapadat : Form
    {
        public List<Adat_Jármű> AdatokJármű { get; set; }
        public event Event_Kidobó TTP_Változás;

        readonly Kezelő_TTP_Alapadat KézAlap = new Kezelő_TTP_Alapadat();

        #region Alap
        public Ablak_TTP_Alapadat(List<Adat_Jármű> adatokJármű)
        {
            AdatokJármű = adatokJármű;
            InitializeComponent();
            Start();
        }

        private void Ablak_TTP_Alapadat_Load(object sender, EventArgs e)
        { }

        private void Start()
        {
            CmbPályaszámFeltölt();
        }
        #endregion


        /// <summary>
        /// Combo feltöltése a pályaszámokkal.
        /// </summary>
        public void CmbPályaszámFeltölt()
        {
            foreach (Adat_Jármű rekord in AdatokJármű)
                CmbPályaszám.Items.Add(rekord.Azonosító);
        }

        /// <summary>
        /// Rögzíti a TTP alapadatokat a pályaszámhoz tartozóan.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (CmbPályaszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs kitöltve a Pályaszám mező.");
                if (!CmbPályaszám.Items.Contains(CmbPályaszám.Text.Trim())) throw new HibásBevittAdat("Nem létezik a pályaszám.");

                Adat_TTP_Alapadat Adat = (from a in KézAlap.Lista_Adatok()
                                          where a.Azonosító == CmbPályaszám.Text.Trim()
                                          select a).FirstOrDefault();

                Adat_TTP_Alapadat ADAT = new Adat_TTP_Alapadat(
                                 CmbPályaszám.Text.Trim(),
                                 DátumGyártás.Value,
                                 ChbTTP.Checked,
                                 TxtbxMegjegyz.Text.Trim());

                if (Adat == null)
                    KézAlap.Rögzítés(ADAT);
                else
                    KézAlap.Módosítás(ADAT);
                TTP_Változás?.Invoke();

                MessageBox.Show("Az adatok rögzítése megtörtént.", "Rögzítve.", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// Pályaszámhoz tartozó adatok keresése és kitöltése a mezőkbe.
        /// </summary>
        private void PályaszámKereső()
        {
            Adat_TTP_Alapadat Adat = (from a in KézAlap.Lista_Adatok()
                                      where a.Azonosító == CmbPályaszám.Text.Trim()
                                      select a).FirstOrDefault();

            if (Adat != null)
            {
                TxtbxMegjegyz.Text = Adat.Megjegyzés;
                DátumGyártás.Value = Adat.Gyártási_Év;
                ChbTTP.Checked = Adat.TTP;
            }
            else
            {
                TxtbxMegjegyz.Text = "";
                DátumGyártás.Value = new DateTime(1900, 1, 1);
                ChbTTP.Checked = false;
            }

        }

        private void CmbPályaszám_TextUpdate(object sender, EventArgs e)
        {
            PályaszámKereső();
        }

        private void CmbPályaszám_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CmbPályaszám.Text = CmbPályaszám.Items[CmbPályaszám.SelectedIndex].ToString();
            PályaszámKereső();
        }
    }
}
