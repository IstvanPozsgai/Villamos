using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Villamos.Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok
{
    public delegate void Event_Kidobó();
    public partial class Ablak_Kidobó_változat : Form
    {
        public event Event_Kidobó Változat_Változás;
        readonly Kezelő_Kidobó_Változat KézKidobSeg = new Kezelő_Kidobó_Változat();

        public string Cmbtelephely { get; private set; }



        public Ablak_Kidobó_változat(string cmbtelephely)
        {
            InitializeComponent();
            Cmbtelephely = cmbtelephely;
            Start();
        }

        private void Start()
        {
            Változatlista1();
        }

        private void Ablak_Kidobó_változat_Load(object sender, EventArgs e)
        {
        }

        #region Változat nevek karbantartása

        private void Változatlista1()
        {
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\Főkönyv\Kidobó\kidobósegéd.mdb";
            string jelszó = "erzsébet";

            string szöveg = "SELECT * FROM Változattábla  order by id";

            Változatalaplista.Items.Clear();

            Változatalaplista.BeginUpdate();
            Változatalaplista.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "Változatnév"));
            Változatalaplista.EndUpdate();
            Változatalaplista.Refresh();
        }


        private void ÚjváltozatRögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Újváltozat.Text.Trim() == "")                     return;

                string Elem = MyF.Szöveg_Tisztítás(Újváltozat.Text, 0, 50);
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\Főkönyv\Kidobó\kidobósegéd.mdb";
                string jelszó = "erzsébet";
                string szöveg = "SELECT * FROM Változattábla  order by id desc";

            
                List<Adat_Kidobó_Változat> AdatokKidobSeg = KézKidobSeg.Lista_Adat(hely, jelszó, szöveg);

                long utolsó = 1;
                if (AdatokKidobSeg.Count > 0) utolsó = AdatokKidobSeg.Max(a => a.Id)+ 1;  

                Adat_Kidobó_Változat AdatKidobSeg = (from a in AdatokKidobSeg
                                                     where a.Változatnév == Elem
                                                     orderby a.Id
                                                     select a).FirstOrDefault();

                if (AdatKidobSeg == null)
                {
                    szöveg = "INSERT INTO Változattábla (id, változatnév) VALUES (";
                    szöveg += $"{utolsó}, '{Elem}') ";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                }

                Újváltozat.Text = "";
                Változatlista1();
                if (Változat_Változás != null) Változat_Változás();
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

        private void VáltozatTörlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Újváltozat.Text.Trim() == "")
                    return;

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\Főkönyv\Kidobó\kidobósegéd.mdb";
                string jelszó = "erzsébet";

                string szöveg = "SELECT * FROM Változattábla ";
                List<Adat_Kidobó_Változat> AdatokKidobSeg = KézKidobSeg.Lista_Adat(hely, jelszó, szöveg);

                Adat_Kidobó_Változat AdatKidobSeg = (from a in AdatokKidobSeg
                                                     where a.Változatnév == Újváltozat.Text.Trim()
                                                     orderby a.Id
                                                     select a).FirstOrDefault();

                if (AdatKidobSeg != null)
                {
                    szöveg = $"DELETE FROM Változattábla WHERE Változatnév='{Újváltozat.Text.Trim()}'";
                    MyA.ABtörlés(hely, jelszó, szöveg);
                }
                Újváltozat.Text = "";
                Változatlista1();

                if (Változat_Változás != null) Változat_Változás();
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
        private void Változatalaplista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Változatalaplista.SelectedIndex < 0) return;
            Újváltozat.Text = Változatalaplista.Items[Változatalaplista.SelectedIndex].ToString();
        }


        #endregion

        private void Ablak_Kidobó_változat_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC
            if ((int)e.KeyCode == 27)
            {
                this.Close();
            }
        }
    }
}
