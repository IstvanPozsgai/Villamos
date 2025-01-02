using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.TTP
{
    public partial class Ablak_TTP_Alapadat : Form
    {
        public List<Adat_Jármű> AdatokJármű { get; set; }
        public event Event_Kidobó TTP_Változás;

        public Ablak_TTP_Alapadat(List<Adat_Jármű> adatokJármű)
        {
            AdatokJármű = adatokJármű;
            InitializeComponent();
        }

        public void CmbPályaszámFeltölt()
        {
            foreach (Adat_Jármű rekord in AdatokJármű)
                CmbPályaszám.Items.Add(rekord.Azonosító);
        }

        private void Ablak_TTP_Alapadat_Load(object sender, EventArgs e)
        {
            CmbPályaszámFeltölt();
        }

        private void BtnRögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (CmbPályaszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs kitöltve a Pályaszám mező.");
                if (!CmbPályaszám.Items.Contains(CmbPályaszám.Text.Trim())) throw new HibásBevittAdat("Nem létezik a pályaszám.");

                Adat_TTP_Alapadat Adat = (from a in MyF.TTP_AlapadatFeltölt()
                                          where a.Azonosító == CmbPályaszám.Text.Trim()
                                          select a).FirstOrDefault();
                string szöveg;
                if (Adat != null)
                {
                    szöveg = $"UPDATE TTP_Alapadat SET Gyártási_Év='{DátumGyártás.Value}', ";
                    szöveg += $"TTP={ChbTTP.Checked}, ";
                    szöveg += $"Megjegyzés='{TxtbxMegjegyz.Text.Trim()}' ";
                    szöveg += $"WHERE Azonosító='{CmbPályaszám.Text.Trim()}'";
                }
                else
                {
                    szöveg = $"INSERT INTO TTP_Alapadat (Azonosító, Gyártási_Év, TTP, Megjegyzés)";
                    szöveg += $"VALUES (";
                    szöveg += $"'{CmbPályaszám.Text.Trim()}',";
                    szöveg += $"'{DátumGyártás.Value}',";
                    szöveg += $"{ChbTTP.Checked},";
                    szöveg += $"'{TxtbxMegjegyz.Text.Trim()}')";
                }

                string hely = $@"{Application.StartupPath}/Főmérnökség/adatok/TTP/TTP_Adatbázis.mdb";
                string jelszó = "rudolfg";

                MyA.ABMódosítás(hely, jelszó, szöveg);
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

        private void CmbPályaszám_SelectedIndexChanged(object sender, EventArgs e)
        {
            PályaszámKereső();
        }


        private void PályaszámKereső()
        {
            Adat_TTP_Alapadat Adat = (from a in MyF.TTP_AlapadatFeltölt()
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
    }
}
