using System;
using System.Windows.Forms;

namespace Villamos.Villamos_Ablakok
{
    public partial class Ablak_munkalap_dekádoló_oszlopot_készít : Form
    {
        public string Választott;

        public event Event_Kidobó Változás;
        public Ablak_munkalap_dekádoló_oszlopot_készít()
        {
            InitializeComponent();
        }

        private void Ablak_munkalap_dekádoló_oszlopot_készít_Load(object sender, EventArgs e)
        {
            AcceptButton = Command8;
        }

        private void Command8_Click(object sender, EventArgs e)
        {
            try
            {

                if (Text5.Text.Trim() == "")
                    throw new HibásBevittAdat("A rendelési szám megadása kötelező.");
                if (Text2.Text.Trim() == "")
                    throw new HibásBevittAdat("A műveletszám megadása kötelező.");
                if (Text4.Text.Trim() == "")
                    throw new HibásBevittAdat("A pályaszzám vagy típus megadása kötelező.");
                if (Text3.Text.Trim() == "")
                    throw new HibásBevittAdat("A munkaleírás megadása kötelező.");

                string szöveg = "";
                szöveg += Text5.Text.Trim() + "\r\n";
                szöveg += Text2.Text.Trim() + "\r\n";
                szöveg += Text4.Text.Trim() + "\r\n";
                szöveg += Text3.Text.Trim() + "\r\n";
                Választott = szöveg;

                if (Változás != null) Változás();
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

        private void Ablak_munkalap_dekádoló_oszlopot_készít_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC
            if ((int)e.KeyCode == 27)
            {
                this.Close();
            }
        }

 
    }
}
