using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Villamos.V_Kezelők;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.V_Ablakok._4_Nyilvántartások.Vételezés
{
    public partial class Ablak_Anyag_Karbantartás : Form
    {
        readonly Kezelő_AnyagTörzs KézAnyag = new Kezelő_AnyagTörzs();
        List<Adat_Anyagok> Adatok = new List<Adat_Anyagok>();

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


        #endregion


    }
}
