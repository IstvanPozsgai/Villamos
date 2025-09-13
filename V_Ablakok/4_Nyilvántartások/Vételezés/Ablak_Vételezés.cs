using System;
using System.Windows.Forms;
using Villamos.V_Kezelők;
using Villamos.V_MindenEgyéb;

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
        { }

        private void Ablak_Vételezés_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Anyag_Karbantartás?.Close();
        }

        private void BtnSúgó_Click(object sender, EventArgs e)
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
            try
            {
                string fájlexc = "";
                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "SAP-s Adatok betöltése",
                    FileName = "",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                {
                    fájlexc = OpenFileDialog1.FileName.ToLower();
                    string[] darabol = fájlexc.Split('.');
                    if (darabol.Length < 2) throw new HibásBevittAdat("Nem megfelelő a betölteni kívánt fájl formátuma!");
                    if (!darabol[darabol.Length - 1].Contains("xls")) throw new HibásBevittAdat("Nem megfelelő a betölteni kívánt fájl kiterjesztés formátuma!");
                }
                else
                    return;

                SAP_Adatokbeolvasása.Raktár_beolvasó(fájlexc);



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



        #region Anyagkarbantartás 
        Ablak_Anyag_Karbantartás Új_Ablak_Anyag_Karbantartás;
        private void AnyagMódosítás_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Anyag_Karbantartás == null)
            {
                Új_Ablak_Anyag_Karbantartás = new Ablak_Anyag_Karbantartás();
                Új_Ablak_Anyag_Karbantartás.FormClosed += Új_Ablak_Anyag_Karbantartás_FormClosed;
                Új_Ablak_Anyag_Karbantartás.Show();
            }
            else
            {
                Új_Ablak_Anyag_Karbantartás.Activate();
                Új_Ablak_Anyag_Karbantartás.WindowState = FormWindowState.Maximized;
            }
        }

        private void Új_Ablak_Anyag_Karbantartás_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Anyag_Karbantartás = null;
        }
        #endregion

    }
}
