using System;
using System.Windows.Forms;
using Villamos.V_Kezelők;

namespace Villamos.V_Ablakok._4_Nyilvántartások.Vételezés
{
    public partial class Ablak_Vételezés : Form
    {
        readonly Kezelő_AnyagTörzs KézAnyag = new Kezelő_AnyagTörzs();

        public Ablak_Vételezés()
        {
            InitializeComponent();
            Start();
        }

        #region Alap

        private void Start()
        {

        }

        private void Ablak_Vételezés_Load(object sender, EventArgs e)
        {

        }

        private void Súgó_Click(object sender, EventArgs e)
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
        /// <summary>
        /// Betöltjük a raktárkészletet és módosítjuk a cikkszámokat és árakat SAP szerint
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSAP_Click(object sender, EventArgs e)
        {

        }
    }
}
