using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.V_Adatszerkezet;
using Villamos.Villamos_Adatszerkezet;
using static Villamos.V_MindenEgyéb.Enumok;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos.V_Ablakok._4_Nyilvántartások.Nóta
{
    public partial class Ablak_Fődarab : Form
    {
        DataTable AdatTábla = new DataTable();

        readonly Kezelő_Nóta KézNóta = new Kezelő_Nóta();
        readonly Kezelő_Kerék_Tábla KézKerék = new Kezelő_Kerék_Tábla();
        readonly Kezelő_Kerék_Mérés KézMérés = new Kezelő_Kerék_Mérés();

        #region Alap
        public Ablak_Fődarab()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            Jogosultságkiosztás();
        }

        private void Ablak_Fődarab_Load(object sender, EventArgs e)
        {

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
                List<Adat_Kerék_Tábla> AdatokKerék = KézKerék.Lista_Adatok();

                List<Adat_Kerék_Mérés> AdatokMérés = KézMérés.Lista_Adatok(DateTime.Today.Year - 1);
                List<Adat_Kerék_Mérés> Ideig = KézMérés.Lista_Adatok(DateTime.Today.Year);
                AdatokMérés.AddRange(Ideig);
                AdatokMérés = AdatokMérés.OrderBy(a => a.Mikor).ToList();
                AdatTábla.Clear();

                foreach (Adat_Nóta rekord in Adatok)
                {
                    Adat_Kerék_Tábla EgyKerék = AdatokKerék.FirstOrDefault(x => x.Kerékberendezés == rekord.Berendezés);
                    string gyáriszám = "";
                    if (EgyKerék != null) gyáriszám = EgyKerék.Kerékgyártásiszám;

                    Adat_Kerék_Mérés Mérés = (from a in AdatokMérés
                                              where a.Kerékberendezés == rekord.Berendezés
                                              orderby a.Mikor ascending
                                              select a).LastOrDefault();
                    int átmérő = 0;
                    string állapot = "";
                    if (Mérés != null)
                    {
                        átmérő = Mérés.Méret;
                        állapot = $"{Mérés.Állapot}-{Enum.GetName(typeof(Kerék_Állapot), Mérés.Állapot.ToÉrt_Int()).Replace('_', ' ')}";
                    }


                    DataRow Soradat = AdatTábla.NewRow();
                    Soradat["Id"] = rekord.Id;
                    Soradat["Berendezés"] = rekord.Berendezés;
                    Soradat["Készlet Sarzs"] = rekord.Készlet_Sarzs;
                    Soradat["Raktár"] = rekord.Raktár;
                    Soradat["Telephely"] = rekord.Telephely;
                    Soradat["Forgóváz"] = rekord.Forgóváz;
                    Soradat["Gyártási Szám"] = gyáriszám;
                    Soradat["Beépíthető"] = rekord.Beépíthető ? "Igen" : "Nem";
                    Soradat["Műszaki Megjegyzés"] = rekord.MűszakiM;
                    Soradat["Osztási Megjegyzés"] = rekord.OsztásiM;
                    Soradat["Dátum"] = rekord.Dátum.ToShortDateString();
                    Soradat["Státus"] = rekord.Státus;
                    Soradat["Átmérő"] = átmérő;
                    Soradat["Állapot"] = állapot;
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
                AdatTábla.Columns.Add("Átmérő");
                AdatTábla.Columns.Add("Állapot");
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

        string MilyenÁllapot(string Állapot)
        {
            string MilyenÁllapot = ""; try
            {
                switch (Állapot.Trim().Substring(0, 1))
                {
                    case "1":
                        MilyenÁllapot = "1 Frissen esztergált";
                        break;
                    case "2":
                        MilyenÁllapot = "2 Üzemszerűen kopott forgalomban";
                        break;
                    case "3":
                        MilyenÁllapot = "3 Forgalomképes esztergálandó";
                        break;
                    case "4":
                        MilyenÁllapot = "4 Forgalomképtelen azonnali esztergálást igényel";
                        break;
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
            return MilyenÁllapot;
        }
        #endregion
    }
}
