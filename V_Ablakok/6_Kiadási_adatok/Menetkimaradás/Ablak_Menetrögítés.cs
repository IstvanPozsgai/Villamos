
using System;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos
{
    public partial class Ablak_Menetrögítés
    {
        public Adat_Menetkimaradás Adat { get; private set; }
        public Ablak_Menetrögítés(Adat_Menetkimaradás adat)
        {
            InitializeComponent();
            Adat = adat;
            Kiir();
        }

        private void Ablak_Menetrögítés_Load(object sender, System.EventArgs e)
        {
        }

        /// <summary>
        /// A kapott adatot kiírjuk részletesen a mezőkbe.
        /// </summary>
        private void Kiir()
        {

            try
            {
                if (Adat != null)
                {
                    txtsorszám.Text = Adat.Id.ToString();
                    txteseményjele.Text = Adat.Eseményjele;
                    txtviszonylat.Text = Adat.Viszonylat;
                    txttípus.Text = Adat.Típus;
                    txtpályaszám.Text = Adat.Azonosító;
                    txtjvbeírás.Text = Adat.Jvbeírás;
                    txthibajavítás.Text = Adat.Javítás;
                    Dátum.Value = Adat.Bekövetkezés;
                    idő.Value = Adat.Bekövetkezés;
                    txtmenet.Text = Adat.Kimaradtmenet.ToString();
                    if (Adat.Törölt)
                        chktörlés.Checked = true;
                    else
                        chktörlés.Checked = false;
                    txtjelentés.Text = Adat.Jelentés;
                    txttétel.Text = Adat.Tétel.ToString();
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
    }
}