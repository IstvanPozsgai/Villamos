using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Villamos.Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Villamos_Ablakok
{
    public partial class Ablak_Épülettakarítás_kieg : Form
    {
        public string HelységKód { get; private set; }
        public string Cmbtelephely { get; private set; }

        public bool FőAl { get; private set; }

        Kezelő_Épület_Adattábla kéz = new Kezelő_Épület_Adattábla();
        List<Adat_Épület_Adattábla> Adatok;

        public Ablak_Épülettakarítás_kieg(string cmbtelephely, string helységKód, bool főal)
        {
            HelységKód = helységKód;
            Cmbtelephely = cmbtelephely;
            InitializeComponent();
            FőAl = főal;
        }


        private void Ablak_Épülettakarítás_kieg_Load(object sender, EventArgs e)
        {
            if (FőAl)
                Fő();
            else
                Al();
        }


        private void Fő()
        {
            label1.Text = $"{HelységKód.Trim()} kapcsolt helységei:";

            List2.Items.Clear();
            List2.Visible = true;
            string[] darabol = HelységKód.Split(' ');

            string hely = $@"{Application.StartupPath}\" + Cmbtelephely.Trim() + @"\Adatok\Épület\épülettörzs.mdb";
            string jelszó = "seprűéslapát";
            string szöveg = "SELECT * FROM Adattábla where státus=0 and kapcsolthelység='" + darabol[0].Trim() + "'";

            Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);

            foreach (Adat_Épület_Adattábla Elem in Adatok)
            {
                List2.Items.Add(Elem.Helységkód.Trim() + " - " + Elem.Megnevezés.Trim());
            }
        }


        void Al()
        {

            string hely = $@"{Application.StartupPath}\" + Cmbtelephely.Trim() + @"\Adatok\Épület\épülettörzs.mdb";
            string jelszó = "seprűéslapát";
            string szöveg = $"SELECT * FROM Adattábla";

            Kezelő_Épület_Takarítás_Adattábla Kéz = new Kezelő_Épület_Takarítás_Adattábla();
            List<Adat_Épület_Takarítás_Adattábla> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

            Adat_Épület_Takarítás_Adattábla Elem = (from a in Adatok
                                                    where a.Státus == false
                                                    && a.Helységkód == HelységKód.Trim()
                                                    select a).FirstOrDefault ();
            string Eredmény = "";
            if (Elem != null) Eredmény = Elem.Kapcsolthelység;
            label1.Text = $"A {HelységKód.Trim()} helység {Eredmény}-hez van kapcsolva.";
            List2.Visible = false;
        }


        private void Ablak_Épülettakarítás_kieg_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == 27)
            {
                this.Close();
            }
        }
    }

}
