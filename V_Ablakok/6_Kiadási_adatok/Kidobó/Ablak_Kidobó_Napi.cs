using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Villamos.Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Villamos_Ablakok
{
    public partial class Ablak_Kidobó_Napi : Form
    {

        public event Event_Kidobó Ismétlődő_Változás;
        public string Cmbtelephely { get; private set; }
        public string Alsópanel { get; private set; }
        public Adat_Kidobó Rekord { get; private set; }
        public DateTime Dátum { get; private set; }

        public Ablak_Kidobó_Napi(string cmbtelephely, Adat_Kidobó rekord, DateTime dátum, string alsópanel)
        {
            Cmbtelephely = cmbtelephely;
            Dátum = dátum;
            Rekord = rekord;
            Alsópanel = alsópanel;

            InitializeComponent();
            Adatak_kiírása();
        }
        private void Ablak_Kidobó_Napi_Load(object sender, EventArgs e)
        {

        }

        private void Adatak_kiírása()
        {
            KezdésiHely.Text = Rekord.Kezdéshely.Trim();
            VégzésiHely.Text = Rekord.Végzéshely.Trim();
            KezdésiIdő.Value = Rekord.Kezdés;
            VégzésiIdő.Value = Rekord.Végzés;
            Megjegyzés.Text = Rekord.Megjegyzés.Trim();
        }

        private void Plusz_Click(object sender, EventArgs e)
        {
            KezdésiHely.Text = Alsópanel;
        }


        private void Command3_Click(object sender, EventArgs e)
        {
            VégzésiHely.Text = Alsópanel;
        }


        private void Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\Főkönyv\Kidobó\{Dátum.Year}\{Dátum:yyyyMMdd}Forte.mdb";
                string jelszó = "lilaakác";
                string szöveg = $"SELECT * FROM kidobótábla";

                Kezelő_Kidobó KézKidobó = new Kezelő_Kidobó();
                List<Adat_Kidobó> Adatok = KézKidobó.Lista_Adat(hely,jelszó,szöveg );

                Adat_Kidobó Elem = (from a in Adatok
                                    where a.Szolgálatiszám== Rekord.Szolgálatiszám
                                    select a).FirstOrDefault ();


                if (Elem!=null)
                {
                    szöveg = "UPDATE kidobótábla  SET ";
                    if (KezdésiHely.Text.Trim() == "")
                        szöveg += "Kezdéshely='_', ";
                    else
                        szöveg += $"Kezdéshely='{KezdésiHely.Text.Trim()}', ";

                    if (VégzésiHely.Text.Trim() == "")
                        szöveg += "Végzéshely='_', ";
                    else
                        szöveg += $"Végzéshely='{VégzésiHely.Text.Trim()}', ";

                    if (Megjegyzés.Text.Trim() == "")
                        szöveg += "megjegyzés='_', ";
                    else
                        szöveg += $"megjegyzés='{Megjegyzés.Text.Trim()}', ";

                    szöveg += $" Kezdés='{KezdésiIdő.Value:HH:mm}', ";
                    szöveg += $" végzés='{VégzésiIdő.Value:HH:mm}' ";
                    szöveg += $" WHERE szolgálatiszám='{Rekord.Szolgálatiszám}'";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                }
                Ismétlődő_Változás?.Invoke();
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

        private void Ablak_Kidobó_Napi_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC
            if ((int)e.KeyCode == 27)
            {
                this.Close();
            }
        }
    }
}
