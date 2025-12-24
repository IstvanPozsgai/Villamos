using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Adatszerkezet;

namespace Villamos.Villamos_Ablakok
{
    public partial class Ablak_Épülettakarítás_kieg : Form
    {
        public string HelységKód { get; private set; }
        public string Cmbtelephely { get; private set; }

        public bool FőAl { get; private set; }

        readonly Kezelő_Épület_Adattábla Kéz = new Kezelő_Épület_Adattábla();

        List<Adat_Épület_Adattábla> Adatok;

        public Ablak_Épülettakarítás_kieg(string cmbtelephely, string helységKód, bool főal)
        {
            HelységKód = helységKód;
            Cmbtelephely = cmbtelephely;
            InitializeComponent();
            FőAl = főal;
        }

        public Ablak_Épülettakarítás_kieg()
        {
            InitializeComponent();
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
            Adatok = Kéz.Lista_Adatok(Cmbtelephely.Trim());
            Adatok = Adatok.Where(a => a.Státus == false && a.Kapcsolthelység.Trim() == darabol[0].Trim()).ToList();

            foreach (Adat_Épület_Adattábla Elem in Adatok)
            {
                List2.Items.Add(Elem.Helységkód.Trim() + " - " + Elem.Megnevezés.Trim());
            }
        }


        private void Al()
        {
            List<Adat_Épület_Adattábla> Adatok = Kéz.Lista_Adatok(Cmbtelephely.Trim());

            Adat_Épület_Adattábla Elem = (from a in Adatok
                                          where a.Státus == false
                                          && a.Helységkód == HelységKód.Trim()
                                          select a).FirstOrDefault();
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
