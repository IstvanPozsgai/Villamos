using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.V_Adatszerkezet;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos.V_Ablakok._4_Nyilvántartások.Nóta
{
    public partial class Ablak_Fődarab : Form
    {
        DataTable AdatTábla = new DataTable();

        readonly Kezelő_Nóta KézNóta = new Kezelő_Nóta();


        #region Alap
        public Ablak_Fődarab()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            Telephelyekfeltöltése();
            Jogosultságkiosztás();
        }

        private void Ablak_Fődarab_Load(object sender, EventArgs e)
        {

        }

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    Cmbtelephely.Items.Add(Elem);
                if (Program.PostásTelephely == "Főmérnökség" || Program.PostásTelephely.Contains("törzs"))
                { Cmbtelephely.Text = Cmbtelephely.Items[0].ToString().Trim(); }
                else
                { Cmbtelephely.Text = Program.PostásTelephely; }

                Cmbtelephely.Enabled = Program.Postás_Vezér;
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

        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\Nóta.html";
                MyE.Megnyitás(hely);
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

        private void Jogosultságkiosztás()
        {
            int melyikelem;

            // ide kell az összes gombot tenni amit szabályozni akarunk false


            melyikelem = 99;

            // módosítás 1 
            if (MyF.Vanjoga(melyikelem, 1))
            {

            }
            // módosítás 2 
            if (MyF.Vanjoga(melyikelem, 2))
            { }

            // módosítás 3
            if (MyF.Vanjoga(melyikelem, 3))
            { }

        }

        #endregion


        #region Táblázat
        private void Frissíti_táblalistát_Click(object sender, EventArgs e)
        {
            ABFejléc();
            ABFeltöltése();
            Táblalista.DataSource = AdatTábla;
            OszlopSzélesség();
            Táblalista.Refresh();
            Táblalista.Visible = true;
            Táblalista.ClearSelection();
        }


        private void OszlopSzélesség()
        {
            Táblalista.Columns["Id"].Width = 50;
            Táblalista.Columns["Berendezés"].Width = 100;
            Táblalista.Columns["Készlet Sarzs"].Width = 80;
            Táblalista.Columns["Raktár"].Width = 80;
            Táblalista.Columns["Telephely"].Width = 120;
            Táblalista.Columns["Gyártási Szám"].Width = 80;
            Táblalista.Columns["Forgóváz"].Width = 80;
            Táblalista.Columns["Beépíthető"].Width = 100;
            Táblalista.Columns["Műszaki Megjegyzés"].Width = 250;
            Táblalista.Columns["Osztási Megjegyzés"].Width = 250;
            Táblalista.Columns["Dátum"].Width = 120;
            Táblalista.Columns["Státus"].Width = 80;
        }

        private void ABFeltöltése()
        {
            try
            {
                List<Adat_Nóta> Adatok = KézNóta.Lista_Adat();
                AdatTábla.Clear();
                foreach (Adat_Nóta rekord in Adatok)
                {
                    DataRow Soradat = AdatTábla.NewRow();
                    Soradat["Id"] = rekord.Id;
                    Soradat["Berendezés"] = rekord.Berendezés;
                    Soradat["Készlet Sarzs"] = rekord.Készlet_Sarzs;
                    Soradat["Raktár"] = rekord.Raktár;
                    Soradat["Telephely"] = rekord.Telephely;
                    Soradat["Forgóváz"] = rekord.Forgóváz;
                    Soradat["Gyártási Szám"] = "";
                    Soradat["Beépíthető"] = rekord.Beépíthető ? "Igen" : "Nem";
                    Soradat["Műszaki Megjegyzés"] = rekord.MűszakiM;
                    Soradat["Osztási Megjegyzés"] = rekord.OsztásiM;
                    Soradat["Dátum"] = rekord.Dátum.ToShortDateString();
                    Soradat["Státus"] = rekord.Státus;


                    AdatTábla.Rows.Add(Soradat);
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

        private void ABFejléc()
        {
            try
            {
                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Id");
                AdatTábla.Columns.Add("Berendezés");
                AdatTábla.Columns.Add("Készlet Sarzs");
                AdatTábla.Columns.Add("Raktár");
                AdatTábla.Columns.Add("Telephely");
                AdatTábla.Columns.Add("Gyártási Szám");
                AdatTábla.Columns.Add("Forgóváz");
                AdatTábla.Columns.Add("Beépíthető");
                AdatTábla.Columns.Add("Műszaki Megjegyzés");
                AdatTábla.Columns.Add("Osztási Megjegyzés");
                AdatTábla.Columns.Add("Dátum");
                AdatTábla.Columns.Add("Státus");

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
    }
}
