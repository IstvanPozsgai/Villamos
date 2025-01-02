using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using Villamos.Villamos_Kezelők;
using MyA = Adatbázis;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok.CAF_Ütemezés
{
    public partial class Ablak_CAF_Alapadat : Form
    {
        public string Azonosító { get; private set; }

        readonly Kezelő_Ciklus KezCiklus = new Kezelő_Ciklus();
        readonly Kezelő_Főkönyv_Zser_Km KézZser = new Kezelő_Főkönyv_Zser_Km();

        List<Adat_Főkönyv_Zser_Km> AdatokZser = new List<Adat_Főkönyv_Zser_Km>();
        List<Adat_Ciklus> AdatokCiklus = new List<Adat_Ciklus>();

        public Ablak_CAF_Alapadat(string azonosító)
        {
            InitializeComponent();
            Azonosító = azonosító;
            Start();

        }


        void Start()
        {
            Jogosultságkiosztás();
            Pályaszámokfeltöltése();
            Vizsgsorszámcombofeltölés();
            Üzemek_listázása();
            CiklusrendCombok_feltöltés();
            CiklusrendCombok_feltöltés();
            if (Azonosító.Trim() != "")
            {
                Alap_pályaszám.Text = Azonosító;
                Alapadatokat_kiír();
            }
        }


        private void Ablak_CAF_Alapadat_Load(object sender, EventArgs e)
        {

        }


        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;
                // ide kell az összes gombot tenni amit szabályozni akarunk false
                Alap_rögzít.Enabled = false;

                Kalkulál.Enabled = false;


                // csak főmérnökségi belépéssel módosítható

                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                    Alap_rögzít.Visible = true;
                else
                    Alap_rögzít.Visible = false;


                melyikelem = 115;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Alap_rögzít.Enabled = true;
                }
                // módosítás 2 
                if (MyF.Vanjoga(melyikelem, 2))
                {

                }
                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 3))
                {
                    Kalkulál.Enabled = true;
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


        private void Pályaszámokfeltöltése()
        {
            try
            {
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\CAF\CAF.mdb";
                string jelszó = "CzabalayL";
                string szöveg = "SELECT * FROM alap ORDER BY azonosító";
                Alap_pályaszám.Items.Clear();
                Alap_pályaszám.BeginUpdate();
                Alap_pályaszám.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "azonosító"));
                Alap_pályaszám.EndUpdate();
                Alap_pályaszám.Refresh();

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


        private void Vizsgsorszámcombofeltölés()
        {
            try
            {
                Alap_ciklus_idő.Items.Clear();
                Alap_ciklus_km.Items.Clear();

                string hely = Application.StartupPath + @"\Főmérnökség\adatok\ciklus.mdb";
                string jelszó = "pocsaierzsi";

                string szöveg = "SELECT DISTINCT típus FROM ciklusrendtábla WHERE  [törölt]='0' ORDER BY típus";

                Alap_ciklus_idő.BeginUpdate();
                Alap_ciklus_idő.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "típus"));
                Alap_ciklus_idő.EndUpdate();
                Alap_ciklus_idő.Refresh();

                Alap_ciklus_km.BeginUpdate();
                Alap_ciklus_km.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "típus"));
                Alap_ciklus_km.EndUpdate();
                Alap_ciklus_km.Refresh();

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


        private void Üzemek_listázása()
        {
            try
            {
                ALAP_Üzemek_km.Items.Clear();
                ALAP_Üzemek_nap.Items.Clear();


                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő.mdb";
                string jelszó = "Mocó";
                string szöveg = "SELECT * FROM telephelytábla order by sorszám";

                ALAP_Üzemek_km.BeginUpdate();
                ALAP_Üzemek_km.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "telephelykönyvtár"));
                ALAP_Üzemek_km.EndUpdate();
                ALAP_Üzemek_km.Refresh();

                ALAP_Üzemek_nap.BeginUpdate();
                ALAP_Üzemek_nap.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "telephelykönyvtár"));
                ALAP_Üzemek_nap.EndUpdate();
                ALAP_Üzemek_nap.Refresh();

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


        private void Kalkulál_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\CAF\CAF.mdb";
                string jelszó = "CzabalayL";
                string szöveg = "SELECT * FROM alap WHERE törölt=false ORDER BY azonosító";

                Kezelő_CAF_alap Kéz = new Kezelő_CAF_alap();
                List<Adat_CAF_alap> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                NapiZSerListaFeltöltés();
                if (Adatok != null)
                {
                    Holtart.Be();
                    List<string> SzövegGy = new List<string>();
                    foreach (Adat_CAF_alap rekord in Adatok)
                    {
                        long havikm = 0;
                        double kmukm = 0;

                        List<Adat_Főkönyv_Zser_Km> vane = (from a in AdatokZser
                                                           where a.Azonosító.Trim() == rekord.Azonosító.Trim()
                                                           orderby a.Dátum descending
                                                           select a).Take(30).ToList();
                        if (vane != null) havikm = vane.Sum(t => t.Napikm);

                        vane = (from t in AdatokZser
                                where t.Azonosító.Trim() == rekord.Azonosító.Trim()
                                && t.Dátum > rekord.Vizsgdátum_km
                                select t).ToList();
                        if (vane != null) kmukm = vane.Sum(t => t.Napikm);

                        // módosítjuk az adatokat
                        szöveg = "UPDATE alap  SET ";
                        szöveg += $" kmukm={kmukm}, ";
                        szöveg += $" havikm={havikm}, ";
                        szöveg += $" KMUdátum=#{DateTime.Today:M-d-yy}# ";
                        szöveg += $" WHERE azonosító='{rekord.Azonosító.Trim()}'";
                        SzövegGy.Add(szöveg);

                        Holtart.Lép();
                    }
                    MyA.ABMódosítás(hely, jelszó, SzövegGy);
                }

                Holtart.Ki();
                MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private void Lekérdezés_lekérdezés_Click(object sender, EventArgs e)
        {
            Alapadatokat_kiír();
        }


        private void Alap_pályaszám_SelectedIndexChanged(object sender, EventArgs e)
        {
            Alapadatokat_kiír();
        }


        private void Alapadatokat_kiír()
        {
            try
            {
                if (Alap_pályaszám.Text.Trim() == "")
                    return;
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\CAF\CAF.mdb";
                string jelszó = "CzabalayL";
                string szöveg = "SELECT * FROM alap WHERE azonosító='" + Alap_pályaszám.Text.Trim() + "'";

                Kezelő_CAF_alap Kéz = new Kezelő_CAF_alap();
                Adat_CAF_alap Adat = Kéz.Egy_Adat(hely, jelszó, szöveg);

                if (Adat != null)
                {

                    Alap_ciklus_idő.Text = Adat.Ciklusnap;
                    Ciklus_IDŐ_Sorszám_feltöltés();
                    Alap_vizsg_idő.Text = Adat.Utolsó_Nap;
                    Alap_vizsg_sorszám_idő.Text = Adat.Utolsó_Nap_sorszám.ToString();
                    ALAP_Üzemek_nap.Text = Adat.Végezte_nap;
                    Alap_dátum_idő.Value = Adat.Vizsgdátum_nap;

                    Alap_ciklus_km.Text = Adat.Cikluskm;
                    Ciklus_KM_Sorszám_feltöltés();
                    Alap_vizsg_km.Text = Adat.Utolsó_Km;
                    Alap_vizsg_sorszám_km.Text = Adat.Utolsó_Km_sorszám.ToString();
                    ALAP_Üzemek_km.Text = Adat.Végezte_km;
                    Alap_dátum_km.Value = Adat.Vizsgdátum_km;
                    Alap_KM_számláló.Text = Adat.Számláló.ToString();

                    Alap_Havi_km.Text = Adat.Havikm.ToString();
                    Alap_KMU.Text = Adat.KMUkm.ToString();
                    Alap_Össz_km.Text = Adat.Teljeskm.ToString();
                    Alap_Dátum_frissítés.Value = Adat.KMUdátum;
                    Alap_Típus.Text = Adat.Típus;
                    Alap_felújítás.Value = Adat.Fudátum;

                    Utolsó_vizsgóta.Text = (Adat.KMUkm - Adat.Számláló).ToString();

                    if (Adat.Törölt == false)
                        Alap_Státus.Checked = false;
                    else
                        Alap_Státus.Checked = true;

                    if (Adat.Garancia == false)
                        Alap_Garancia.Checked = false;
                    else
                        Alap_Garancia.Checked = true;
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


        private void Ciklus_IDŐ_Sorszám_feltöltés()
        {
            try
            {
                Alap_vizsg_sorszám_idő.Items.Clear();
                if (Alap_ciklus_idő.Text.Trim() == "")
                    return;

                string hely = Application.StartupPath + @"\Főmérnökség\adatok\ciklus.mdb";
                string jelszó = "pocsaierzsi";

                string szöveg = "SELECT * FROM ciklusrendtábla WHERE  [törölt]='0' AND típus='" + Alap_ciklus_idő.Text.Trim() + "' ORDER BY sorszám";

                Alap_vizsg_sorszám_idő.BeginUpdate();
                Alap_vizsg_sorszám_idő.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "sorszám"));
                Alap_vizsg_sorszám_idő.EndUpdate();
                Alap_vizsg_sorszám_idő.Refresh();

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


        private void E_rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Alap_pályaszám.Text.Trim() == "") throw new HibásBevittAdat("Pályaszám mező nincs kitöltve.");
                if (Alap_ciklus_idő.Text.Trim() == "") Alap_ciklus_idő.Text = "_";
                if (Alap_vizsg_idő.Text.Trim() == "") Alap_vizsg_idő.Text = "_";
                if (ALAP_Üzemek_nap.Text.Trim() == "") ALAP_Üzemek_nap.Text = "_";
                if (Alap_ciklus_km.Text.Trim() == "") Alap_ciklus_km.Text = "_";
                if (Alap_vizsg_km.Text.Trim() == "") Alap_vizsg_km.Text = "_";
                if (ALAP_Üzemek_km.Text.Trim() == "") ALAP_Üzemek_km.Text = "_";
                if (Alap_KM_számláló.Text.Trim() == "") Alap_KM_számláló.Text = "0";
                if (Alap_Havi_km.Text.Trim() == "") Alap_Havi_km.Text = "0";
                if (Alap_KMU.Text.Trim() == "") Alap_KMU.Text = "0";
                if (Alap_Össz_km.Text.Trim() == "") Alap_Össz_km.Text = "0";
                if (Alap_vizsg_sorszám_idő.Text.Trim() == "") Alap_vizsg_sorszám_idő.Text = "0";
                if (Alap_vizsg_sorszám_km.Text.Trim() == "") Alap_vizsg_sorszám_km.Text = "0";
                if (!int.TryParse(Alap_KM_számláló.Text, out int alap_KM_számláló)) alap_KM_számláló = 0;
                if (!int.TryParse(Alap_Havi_km.Text, out int alap_Havi_km)) alap_Havi_km = 0;
                if (!int.TryParse(Alap_KMU.Text, out int alap_KMU)) alap_KMU = 0;
                if (!int.TryParse(Alap_Össz_km.Text, out int alap_Össz_km)) alap_Össz_km = 0;
                if (!int.TryParse(Alap_vizsg_sorszám_idő.Text, out int alap_vizsg_sorszám_idő)) alap_vizsg_sorszám_idő = 0;
                if (!int.TryParse(Alap_vizsg_sorszám_km.Text, out int alap_vizsg_sorszám_km)) alap_vizsg_sorszám_km = 0;

                string hely = Application.StartupPath + @"\Főmérnökség\adatok\CAF\CAF.mdb";
                string jelszó = "CzabalayL";
                string szöveg = $"SELECT * FROM alap";
                Kezelő_CAF_alap KézCAFAlap = new Kezelő_CAF_alap();
                List<Adat_CAF_alap> AdatokCAFAlap = KézCAFAlap.Lista_Adatok(hely, jelszó, szöveg);
                bool vane = AdatokCAFAlap.Any(a => a.Azonosító.Trim() == Alap_pályaszám.Text.Trim());

                if (vane)
                {
                    // módosít
                    szöveg = "UPDATE alap  SET ";
                    szöveg += $"Ciklusnap='{Alap_ciklus_idő.Text.Trim()}', "; // Ciklusnap
                    szöveg += $"Utolsó_Nap='{Alap_vizsg_idő.Text.Trim()}', "; // Utolsó_Nap
                    szöveg += $"Utolsó_Nap_sorszám={alap_vizsg_sorszám_idő}, "; // Utolsó_Nap_sorszám
                    szöveg += $"Végezte_nap='{ALAP_Üzemek_nap.Text.Trim()}', "; // Végezte_nap
                    szöveg += $"Vizsgdátum_nap='{Alap_dátum_idő.Value:MM-dd-yyyy}', "; // Vizsgdátum_nap

                    szöveg += $"Cikluskm='{Alap_ciklus_km.Text.Trim()}', "; // Cikluskm
                    szöveg += $"Utolsó_Km='{Alap_vizsg_km.Text.Trim()}', ";  // Utolsó_Km
                    szöveg += $"Utolsó_Km_sorszám={alap_vizsg_sorszám_km}, "; // Utolsó_Km_sorszám
                    szöveg += $"Végezte_km='{ALAP_Üzemek_km.Text.Trim()}', "; // Végezte_km
                    szöveg += $"Vizsgdátum_km='{Alap_dátum_km.Value:MM-dd-yyyy}', "; // Vizsgdátum_km
                    szöveg += $"számláló={alap_KM_számláló}, "; // számláló,

                    szöveg += $"havikm={alap_Havi_km}, "; // havikm,
                    szöveg += $"KMUkm={alap_KMU}, ";  // KMUkm
                    szöveg += $"KMUdátum='{Alap_Dátum_frissítés.Value:MM-dd-yyyy}', "; // KMUdátum,
                    szöveg += $"fudátum='{Alap_felújítás.Value:MM-dd-yyyy}', ";  // fudátum
                    szöveg += $"Teljeskm={alap_Össz_km}, "; // Teljeskm
                    if (Alap_Garancia.Checked) // Garancia
                        szöveg += "Garancia=true, ";
                    else
                        szöveg += "Garancia=false, ";

                    if (Alap_Státus.Checked) // törölt
                        szöveg += "törölt=true, ";
                    else
                        szöveg += "törölt=false, ";

                    szöveg += $"típus='{Alap_Típus.Text.Trim()} '"; // típus

                    szöveg += $" WHERE azonosító='{Alap_pályaszám.Text.Trim()}'";
                }
                else
                {
                    // új jármű
                    szöveg = "INSERT INTO alap (azonosító, Ciklusnap, Utolsó_Nap, Utolsó_Nap_sorszám, Végezte_nap, Vizsgdátum_nap, Cikluskm, Utolsó_Km,";
                    szöveg += " Utolsó_Km_sorszám, Végezte_km, Vizsgdátum_km, számláló, havikm, KMUkm, KMUdátum, fudátum, Teljeskm, Garancia, törölt, típus ) VALUES (";
                    szöveg += "'" + Alap_pályaszám.Text.Trim() + "', "; // azonosító
                    szöveg += "'" + Alap_ciklus_idő.Text.Trim() + "', "; // Ciklusnap
                    szöveg += "'" + Alap_vizsg_idő.Text.Trim() + "', "; // Utolsó_Nap
                    szöveg += alap_vizsg_sorszám_idő + ", "; // Utolsó_Nap_sorszám
                    szöveg += "'" + ALAP_Üzemek_nap.Text.Trim() + "', "; // Végezte_nap
                    szöveg += "'" + Alap_dátum_idő.Value.ToString("MM-dd-yyyy") + "', "; // Vizsgdátum_nap

                    szöveg += "'" + Alap_ciklus_km.Text.Trim() + "', "; // Cikluskm
                    szöveg += "'" + Alap_vizsg_km.Text.Trim() + "', ";  // Utolsó_Km
                    szöveg += alap_vizsg_sorszám_km + ", "; // Utolsó_Km_sorszám
                    szöveg += "'" + ALAP_Üzemek_km.Text.Trim() + "', "; // Végezte_km
                    szöveg += "'" + Alap_dátum_km.Value.ToString("MM-dd-yyyy") + "', "; // Vizsgdátum_km
                    szöveg += alap_KM_számláló + ", "; // számláló,

                    szöveg += alap_Havi_km + ", "; // havikm,
                    szöveg += alap_KMU + ", ";  // KMUkm
                    szöveg += "'" + Alap_Dátum_frissítés.Value.ToString("MM-dd-yyyy") + "', "; // KMUdátum,
                    szöveg += "'" + Alap_felújítás.Value.ToString("MM-dd-yyyy") + "', ";  // fudátum
                    szöveg += alap_Össz_km + ", "; // Teljeskm
                    if (Alap_Garancia.Checked == true) // Garancia
                        szöveg += " true, ";
                    else
                        szöveg += " false, ";

                    if (Alap_Státus.Checked == true) // törölt
                        szöveg += " true, ";
                    else
                        szöveg += " false, ";

                    szöveg += "'" + Alap_Típus.Text.Trim() + "') ";
                } // típus
                MyA.ABMódosítás(hely, jelszó, szöveg);
                MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);

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


        private void Alap_ciklus_idő_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ciklus_IDŐ_Sorszám_feltöltés();
        }


        private void Alap_ciklus_km_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ciklus_KM_Sorszám_feltöltés();
        }


        private void Ciklus_KM_Sorszám_feltöltés()
        {
            try
            {
                Alap_vizsg_sorszám_km.Items.Clear();
                if (Alap_ciklus_km.Text.Trim() == "")
                    return;

                string hely = Application.StartupPath + @"\Főmérnökség\adatok\ciklus.mdb";
                string jelszó = "pocsaierzsi";

                string szöveg = $"SELECT * FROM ciklusrendtábla WHERE  [törölt]='0' AND típus='{Alap_ciklus_km.Text.Trim()}' ORDER BY sorszám";

                Alap_vizsg_sorszám_km.BeginUpdate();
                Alap_vizsg_sorszám_km.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "sorszám"));
                Alap_vizsg_sorszám_km.EndUpdate();
                Alap_vizsg_sorszám_km.Refresh();

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


        private void CiklusrendCombok_feltöltés()
        {
            try
            {
                Alap_ciklus_idő.Items.Clear();
                Alap_ciklus_km.Items.Clear();

                string hely = Application.StartupPath + @"\Főmérnökség\adatok\ciklus.mdb";
                string jelszó = "pocsaierzsi";

                string szöveg = "SELECT DISTINCT típus FROM ciklusrendtábla WHERE  [törölt]='0' ORDER BY típus";

                Alap_ciklus_idő.BeginUpdate();
                Alap_ciklus_idő.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "típus"));
                Alap_ciklus_idő.EndUpdate();
                Alap_ciklus_idő.Refresh();

                Alap_ciklus_km.BeginUpdate();
                Alap_ciklus_km.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "típus"));
                Alap_ciklus_km.EndUpdate();
                Alap_ciklus_km.Refresh();

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


        private void Alap_vizsg_sorszám_idő_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Alap_ciklus_idő.Text.Trim() == "") return;
            if (Alap_vizsg_sorszám_idő.Text.Trim() == "") return;

            string hely = Application.StartupPath + @"\Főmérnökség\adatok\ciklus.mdb";
            string jelszó = "pocsaierzsi";
            string szöveg = "SELECT * FROM ciklusrendtábla";
            List<Adat_Ciklus> AdatokCiklus = KezCiklus.Lista_Adatok(hely, jelszó, szöveg);
            if (!long.TryParse(Alap_vizsg_sorszám_idő.Text.Trim(), out long SorSzám)) SorSzám = 0;
            Adat_Ciklus vane = (from a in AdatokCiklus
                                where a.Törölt == "0" &&
                                a.Sorszám == SorSzám &&
                                a.Típus.Trim() == Alap_ciklus_idő.Text.Trim()
                                select a).FirstOrDefault();

            if (vane != null) Alap_vizsg_idő.Text = vane.Vizsgálatfok;
        }

        private void Alap_vizsg_sorszám_km_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Alap_ciklus_km.Text.Trim() == "") return;
            if (Alap_vizsg_sorszám_km.Text.Trim() == "") return;

            string hely = Application.StartupPath + @"\Főmérnökség\adatok\ciklus.mdb";
            string jelszó = "pocsaierzsi";
            string szöveg = "SELECT * FROM ciklusrendtábla";
            List<Adat_Ciklus> AdatokCiklus = KezCiklus.Lista_Adatok(hely, jelszó, szöveg);

            if (!long.TryParse(Alap_vizsg_sorszám_idő.Text.Trim(), out long SorSzám)) SorSzám = 0;
            Adat_Ciklus vane = (from a in AdatokCiklus
                                where a.Törölt == "0" &&
                                a.Sorszám == SorSzám &&
                                a.Típus.Trim() == Alap_ciklus_km.Text.Trim()
                                select a).FirstOrDefault();
            if (vane != null) Alap_vizsg_km.Text = vane.Vizsgálatfok;
        }

        private void Ablak_CAF_Alapadat_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC
            if ((int)e.KeyCode == 27)
            {
                this.Close();
            }
        }


        private void NapiZSerListaFeltöltés()

        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{DateTime.Today.Year - 1}\Napi_km_Zser_{DateTime.Today.Year - 1}.mdb";
                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM Tábla";
                List<Adat_Főkönyv_Zser_Km> Ideig = new List<Adat_Főkönyv_Zser_Km>();
                AdatokZser.Clear();
                if (File.Exists(hely)) AdatokZser = KézZser.Lista_adatok(hely, jelszó, szöveg);
                hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{DateTime.Today.Year}\Napi_km_Zser_{DateTime.Today.Year}.mdb";
                if (File.Exists(hely)) Ideig = KézZser.Lista_adatok(hely, jelszó, szöveg);
                if (Ideig.Any()) AdatokZser.AddRange(Ideig);
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
