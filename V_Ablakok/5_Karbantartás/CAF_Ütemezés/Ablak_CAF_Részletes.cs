using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using Villamos.Kezelők;
using MyA = Adatbázis;
using MyCaf = Villamos.Villamos_Ablakok.CAF_Ütemezés.CAF_Közös_Eljárások;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok.CAF_Ütemezés
{
    public partial class Ablak_CAF_Részletes : Form
    {
        public event Event_Kidobó Változás;
        public CAF_Segéd_Adat Posta_Segéd { get; private set; }
        public DateTime Elő_Dátumig { get; private set; }

        string hely = Application.StartupPath + @"\Főmérnökség\adatok\CAF\CAF.mdb";
        string jelszó = "CzabalayL";

        Kezelő_CAF_alap AlapKéz = new Kezelő_CAF_alap();
        Adat_CAF_alap EgyCAF;
        Kezelő_Ciklus Kéz_Ciklus = new Kezelő_Ciklus();
        List<Adat_Ciklus> Ciklus_Idő = null;
        List<Adat_Ciklus> Ciklus_Km = null;

        public Ablak_CAF_Részletes(CAF_Segéd_Adat posta_Segéd, DateTime elő_Dátumig)
        {
            InitializeComponent();

            Posta_Segéd = posta_Segéd;
            Start();
            Elő_Dátumig = elő_Dátumig;
        }


        #region Alap
        private void Ablak_CAF_Részletes_Load(object sender, EventArgs e)
        {
            Jogosultságkiosztás();
        }


        private void Ablak_CAF_Részletes_FormClosed(object sender, FormClosedEventArgs e)
        {

        }


        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;
                // ide kell az összes gombot tenni amit szabályozni akarunk false
                Ütem_Rögzít.Enabled = false;

                // csak főmérnökségi belépéssel módosítható

                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {
                    Ütem_Rögzít.Visible = true;
                }
                else
                {
                    Ütem_Rögzít.Visible = false;
                }


                melyikelem = 118;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Ütem_Rögzít.Enabled = true;
                }
                // módosítás 2 
                if (MyF.Vanjoga(melyikelem, 2))
                {

                }
                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 3))
                {

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
        #endregion


        #region Induláskor fut le
        void Start()
        {
            string szöveg = $"SELECT * FROM alap WHERE azonosító='{Posta_Segéd.Azonosító.Trim()}'";
            EgyCAF = AlapKéz.Egy_Adat(hely, jelszó, szöveg);

            Státus_feltöltés();
            Ütem_Pályaszámokfeltöltése();
            Ütem_Ciklus_IDŐ_Sorszám_feltöltés();
            Ütem_Ciklus_KM_Sorszám_feltöltés();
            if (Posta_Segéd.Sorszám > 0)
            {
                AdatokKiírása();
                KiírJobbOldal();
            }
            else
            {
                AdatokKeresés();
                KiírJobbOldal();
            }
        }

        private void Státus_feltöltés()
        {
            Ütem_státus.Items.Clear();
            Ütem_státus.Items.Add("0- Tervezési");
            Ütem_státus.Items.Add("2- Ütemezett");
            Ütem_státus.Items.Add("4- Előjegyzett");
            Ütem_státus.Items.Add("6- Elvégzett");
            Ütem_státus.Items.Add("8- Tervezésisegéd");
            Ütem_státus.Items.Add("9- Törölt");

            Ütem_Köv_Státus.Items.Clear();
            Ütem_Köv_Státus.Items.Add("0- Tervezési");
            Ütem_Köv_Státus.Items.Add("2- Ütemezett");
            Ütem_Köv_Státus.Items.Add("4- Előjegyzett");
            Ütem_Köv_Státus.Items.Add("6- Elvégzett");
            Ütem_Köv_Státus.Items.Add("8- Tervezésisegéd");
            Ütem_Köv_Státus.Items.Add("9- Törölt");
        }


        private void Ütem_Pályaszámokfeltöltése()
        {
            string szöveg = "SELECT * FROM alap WHERE törölt=false ORDER BY azonosító";
            Ütem_pályaszám.Items.Clear();
            Ütem_pályaszám.BeginUpdate();
            Ütem_pályaszám.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "azonosító"));
            Ütem_pályaszám.EndUpdate();
            Ütem_pályaszám.Refresh();
        }


        private void Ütem_Ciklus_IDŐ_Sorszám_feltöltés()
        {
            try
            {
                Ütem_Ciklus_IDŐ.Text = EgyCAF.Ciklusnap;
                Ütem_vizsg_sorszám_idő.Items.Clear();

                string helycik = Application.StartupPath + @"\Főmérnökség\adatok\ciklus.mdb";
                string jelszócik = "pocsaierzsi";

                string szöveg = $"SELECT * FROM ciklusrendtábla WHERE  [törölt]='0' AND típus='{EgyCAF.Ciklusnap}' ORDER BY sorszám";

                Ütem_vizsg_sorszám_idő.BeginUpdate();
                Ütem_vizsg_sorszám_idő.Items.AddRange(MyF.ComboFeltöltés(helycik, jelszócik, szöveg, "sorszám"));
                Ütem_vizsg_sorszám_idő.EndUpdate();
                Ütem_vizsg_sorszám_idő.Refresh();

                Ciklus_Idő = Kéz_Ciklus.Lista_Adatok(helycik, jelszócik, szöveg);

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


        private void Ütem_Ciklus_KM_Sorszám_feltöltés()
        {
            try
            {
                Ütem_Ciklus_KM.Text = EgyCAF.Cikluskm;
                Ütem_vizsg_sorszám_km.Items.Clear();

                string helycik = Application.StartupPath + @"\Főmérnökség\adatok\ciklus.mdb";
                string jelszócik = "pocsaierzsi";

                string szöveg = $"SELECT * FROM ciklusrendtábla WHERE  [törölt]='0' AND típus='{EgyCAF.Cikluskm}' ORDER BY sorszám";

                Ütem_vizsg_sorszám_km.BeginUpdate();
                Ütem_vizsg_sorszám_km.Items.AddRange(MyF.ComboFeltöltés(helycik, jelszócik, szöveg, "sorszám"));
                Ütem_vizsg_sorszám_km.EndUpdate();
                Ütem_vizsg_sorszám_km.Refresh();
                Ciklus_Km = Kéz_Ciklus.Lista_Adatok(helycik, jelszócik, szöveg);
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


        #region Kiírások

        void AdatokKiírása()
        {
            try
            {
                string szöveg = $"SELECT * FROM adatok WHERE id={Posta_Segéd.Sorszám}";

                Kezelő_CAF_Adatok kéz = new Kezelő_CAF_Adatok();
                Adat_CAF_Adatok Adat = kéz.Egy_Adat(hely, jelszó, szöveg);
                KiírEgyAdatot(Adat);

                szöveg = $"SELECT * FROM adatok WHERE id<{Posta_Segéd.Sorszám} AND Azonosító='{Posta_Segéd.Azonosító.Trim()}' AND státus<9 order by id desc";
                Adat = kéz.Egy_Adat(hely, jelszó, szöveg);
                KiírElőzőAdatot(Adat);
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


        void KiírEgyAdatot(Adat_CAF_Adatok rekord)
        {
            try
            {
                if (rekord != null)
                {
                    Ütem_pályaszám.Text = rekord.Azonosító.Trim();
                    Ütem_köv_sorszám.Text = rekord.Id.ToString();
                    Ütem_Köv_Vizsgálat.Text = rekord.Vizsgálat;
                    Ütem_Köv_Dátum.Value = rekord.Dátum;
                    Ütem_Köv_Számláló.Text = rekord.Számláló.ToString();

                    for (int i = 0; i < Ütem_Köv_Státus.Items.Count; i++)
                    {
                        if (Ütem_Köv_Státus.Items[i].ToString().Contains(rekord.Státus.ToString()))
                        {
                            Ütem_Köv_Státus.Text = Ütem_Köv_Státus.Items[i].ToString();
                            break;
                        }
                    }

                    switch (rekord.IDŐvKM)
                    {
                        case 0:
                            {
                                Ütem_Köv_IDŐvKM.Text = "?";
                                break;
                            }
                        case 1:
                            {
                                Ütem_Köv_IDŐvKM.Text = "Idő";
                                break;
                            }
                        case 2:
                            {
                                Ütem_Köv_IDŐvKM.Text = "Km";
                                break;
                            }
                    }
                    Ütem_vizsg_sorszám_km.Text = rekord.KM_Sorszám.ToString();
                    Ütem_vizsg_sorszám_idő.Text = rekord.IDŐ_Sorszám.ToString();
                    Ütem_megjegyzés.Text = rekord.Megjegyzés.Trim();
                    Ütem_dátum_program.Value = rekord.Dátum_program;
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

        void KiírJobbOldal()
        {
            try
            {
                KM_ciklus_kiirás();
                int KM_felső = int.TryParse(Ütem_KM_felső.Text, out KM_felső) ? KM_felső : 0;
                Ütem_számláló_KM.Text = EgyCAF.Számláló.ToString();
                Ütem_Utolsó_futott.Text = (EgyCAF.KMUkm - EgyCAF.Számláló).ToString();
                Ütem_km_KMU.Text = EgyCAF.KMUkm.ToString();
                Ütem_havifutás.Text = EgyCAF.Havikm.ToString();
                Ütem_Napkm.Text = ((int)(EgyCAF.Havikm / 30)).ToString();
                Ütem_KM_futhatmég.Text = ((KM_felső - (EgyCAF.KMUkm - EgyCAF.Számláló)) / ((int)(EgyCAF.Havikm / 30))).ToString();
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "KiírJobbOldal", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ütem_mezők_ürítése()
        {
            Ütem_köv_sorszám.Text = "";
            Ütem_Köv_Vizsgálat.Text = "";
            Ütem_Köv_Dátum.Value = new DateTime(1900, 1, 1);
            Ütem_dátum_program.Value = new DateTime(1900, 1, 1);
            Ütem_Köv_Számláló.Text = "";
            Ütem_Köv_Státus.Text = "";
            Ütem_vizsg_sorszám_km.Text = "";
            Ütem_vizsg_sorszám_idő.Text = "";
            Ütem_Köv_IDŐvKM.Text = "";
        }


        void KiírElőzőAdatot(Adat_CAF_Adatok rekord)
        {
            try
            {
                if (rekord != null)
                {
                    Ütem_sorszám.Text = rekord.Id.ToString();
                    Ütem_vizsgálat.Text = rekord.Vizsgálat;
                    Ütem_dátum.Value = rekord.Dátum;
                    Ütem_számláló.Text = rekord.Számláló.ToString();
                    Ütem_státus.Text = rekord.Státus.ToString();

                    for (int i = 0; i < Ütem_státus.Items.Count; i++)
                    {
                        if (Ütem_státus.Items[i].ToString().Contains(rekord.Státus.ToString()))
                            Ütem_státus.Text = Ütem_státus.Items[i].ToString();
                    }

                    Ütem_Km_sorszám.Text = rekord.KM_Sorszám.ToString();
                    Ütem_idő_sorszám.Text = rekord.IDŐ_Sorszám.ToString();
                    switch (rekord.IDŐvKM)
                    {
                        case 0:
                            {
                                Ütem_IDŐvKM.Text = "?";
                                break;
                            }
                        case 1:
                            {
                                Ütem_IDŐvKM.Text = "Idő";
                                break;
                            }
                        case 2:
                            {
                                Ütem_IDŐvKM.Text = "Km";
                                break;
                            }

                    }
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


        private void Utolsó_ütemezett_kiírása_km()
        {
            try
            {
                string szöveg = $"SELECT * FROM adatok WHERE azonosító='{Ütem_pályaszám.Text.Trim()}' AND státus<9 AND IDŐvKM=2 ORDER BY dátum desc";

                Kezelő_CAF_Adatok kéz = new Kezelő_CAF_Adatok();
                Adat_CAF_Adatok rekord = kéz.Egy_Adat(hely, jelszó, szöveg);
                KiírEgyAdatot(rekord);
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


        void AdatokKeresés()
        {
            try
            {
                string szöveg = $"SELECT * FROM adatok WHERE Azonosító='{Posta_Segéd.Azonosító}' AND  Dátum=#{Posta_Segéd.Dátum.ToString("MM-dd-yyyy")}# AND Státus<8";

                Kezelő_CAF_Adatok kéz = new Kezelő_CAF_Adatok();
                Adat_CAF_Adatok Adat = kéz.Egy_Adat(hely, jelszó, szöveg);
                KiírEgyAdatot(Adat);
                KiírElőzőAdatot(Adat);
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







        private void Ütem_pályaszám_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void Ütem_pályaszám_SelectionChangeCommitted(object sender, EventArgs e)
        {

            if (Ütem_pályaszám.Text.Trim() != "")
            {
                Ütem_mezők_ürítése();

                string szöveg = $"SELECT * FROM adatok WHERE azonosító='{Ütem_pályaszám.Items[Ütem_pályaszám.SelectedIndex].ToString().Trim()}' AND státus<9 ORDER BY dátum desc";

                Kezelő_CAF_Adatok kéz = new Kezelő_CAF_Adatok();
                Adat_CAF_Adatok Adat = kéz.Egy_Adat(hely, jelszó, szöveg);
                KiírEgyAdatot(Adat);

                szöveg = $"SELECT * FROM adatok WHERE id<{Adat.Id} AND Azonosító='{Posta_Segéd.Azonosító.Trim()}' AND státus<9 order by id desc";
                Adat = kéz.Egy_Adat(hely, jelszó, szöveg);
                KiírElőzőAdatot(Adat);

                KiírJobbOldal();
            }
        }

        private void Alapról_átír()
        {
            try
            {
                if (Ütem_pályaszám.Text.Trim() == "")
                    return;


                EgyCAF = MyCaf.Villamos_tulajdonság(Ütem_pályaszám.Text.Trim());

                if (EgyCAF != null)
                {
                    Ütem_Ciklus_IDŐ.Text = EgyCAF.Ciklusnap;
                    Ütem_Ciklus_KM.Text = EgyCAF.Cikluskm;
                    Ütem_Utolsó_futott.Text = (EgyCAF.KMUkm - EgyCAF.Számláló).ToString();
                    Ütem_km_KMU.Text = EgyCAF.KMUkm.ToString();
                    Ütem_havifutás.Text = EgyCAF.Havikm.ToString();
                    Ütem_frissítés.Value = EgyCAF.KMUdátum;
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


        private void Ütem_vizsg_sorszám_idő_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Ütem_Ciklus_IDŐ.Text.Trim() == "")
                return;
            if (Ütem_vizsg_sorszám_idő.Text.Trim() == "")
                return;

            string helycik = Application.StartupPath + @"\Főmérnökség\adatok\ciklus.mdb";
            string jelszócik = "pocsaierzsi";
            string szöveg = "SELECT * FROM ciklusrendtábla WHERE  [törölt]='0' AND sorszám=" + Ütem_vizsg_sorszám_idő.Items[Ütem_vizsg_sorszám_idő.SelectedIndex].ToString().Trim() + " AND típus ='" + Ütem_Ciklus_IDŐ.Text.Trim() + "'";


            Adat_Ciklus Adat = Kéz_Ciklus.Egy_Adat(helycik, jelszócik, szöveg);

            if (Adat != null)
            {
                Ütem_vizsgálat_IDŐ.Text = Adat.Vizsgálatfok;
                Ütem_névleges_nap.Text = Adat.Névleges.ToString();
                Ütem_Köv_Vizsgálat.Text = Ütem_vizsgálat_IDŐ.Text;
            }
        }


        private void Ütem_vizsg_sorszám_km_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Ütem_Ciklus_KM.Text.Trim() == "")
                return;
            if (Ütem_vizsg_sorszám_km.Text.Trim() == "")
                return;

            string helycik = Application.StartupPath + @"\Főmérnökség\adatok\ciklus.mdb";
            string jelszócik = "pocsaierzsi";
            string szöveg = "SELECT * FROM ciklusrendtábla WHERE  [törölt]='0' AND sorszám=" + Ütem_vizsg_sorszám_km.Items[Ütem_vizsg_sorszám_km.SelectedIndex].ToString().Trim() + " AND típus ='" + Ütem_Ciklus_KM.Text.Trim() + "'";


            Adat_Ciklus Adat = Kéz_Ciklus.Egy_Adat(helycik, jelszócik, szöveg);

            if (Adat != null)
            {
                Ütem_vizsgálat_KM.Text = Adat.Vizsgálatfok;
                Ütem_KM_alsó.Text = Adat.Alsóérték.ToString();
                Ütem_KM_felső.Text = Adat.Felsőérték.ToString();
                Ütem_KM_névleges.Text = Adat.Névleges.ToString();
                Ütem_Köv_Vizsgálat.Text = Ütem_vizsgálat_KM.Text;
            }
        }

        void KM_ciklus_kiirás()
        {
            try
            {
                string helycik = Application.StartupPath + @"\Főmérnökség\adatok\ciklus.mdb";
                string jelszócik = "pocsaierzsi";
                if (Ütem_vizsg_sorszám_km.Text.Trim() == "") return;
                string szöveg = "SELECT * FROM ciklusrendtábla WHERE  [törölt]='0' AND sorszám=" + Ütem_vizsg_sorszám_km.Text.Trim() + " AND típus ='" + Ütem_Ciklus_KM.Text.Trim() + "'";


                Adat_Ciklus Adat = Kéz_Ciklus.Egy_Adat(helycik, jelszócik, szöveg);

                if (Adat != null)
                {
                    Ütem_vizsgálat_KM.Text = Adat.Vizsgálatfok;
                    Ütem_KM_alsó.Text = Adat.Alsóérték.ToString();
                    Ütem_KM_felső.Text = Adat.Felsőérték.ToString();
                    Ütem_KM_névleges.Text = Adat.Névleges.ToString();
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

        private void Következő_km()
        {
            if (Ütem_Ciklus_KM.Text.Trim() == "")
                return;
            if (Ütem_vizsg_sorszám_km.Text.Trim() == "")
                return;

            string helycik = Application.StartupPath + @"\Főmérnökség\adatok\ciklus.mdb";
            string jelszócik = "pocsaierzsi";
            string szöveg = "SELECT * FROM ciklusrendtábla WHERE  [törölt]='0' AND sorszám=" + Ütem_vizsg_sorszám_km.Text.Trim() + " AND típus ='" + Ütem_Ciklus_KM.Text.Trim() + "'";

            Adat_Ciklus Adat = Kéz_Ciklus.Egy_Adat(helycik, jelszócik, szöveg);

            if (Adat != null)
            {
                Ütem_vizsgálat_KM.Text = Adat.Vizsgálatfok;
                Ütem_KM_alsó.Text = Adat.Alsóérték.ToString();
                Ütem_KM_felső.Text = Adat.Felsőérték.ToString();
                Ütem_KM_névleges.Text = Adat.Névleges.ToString();
                Ütem_Köv_Vizsgálat.Text = Ütem_vizsgálat_KM.Text;
            }
        }



        #region KmUgrás


        private void KM_Click(object sender, EventArgs e)
        {
            try
            {
                if (Ütem_pályaszám.Text.Trim() == "")
                    throw new HibásBevittAdat("Nincs kitöltve a pályaszám mező.");
                //Jármű tulajdonsága
                EgyCAF = MyCaf.Villamos_tulajdonság(Ütem_pályaszám.Text.Trim());

                // utolsó ütemezett
                Adat_CAF_Adatok Előző = MyCaf.Utolsó_ütemezett(Ütem_pályaszám.Text.Trim(), "km");
                KiírElőzőAdatot(Előző);

                // következő km szerinti
                Adat_CAF_Adatok Adat = MyCaf.Következő_Km(Ciklus_Km, Előző, EgyCAF);
                KiírEgyAdatot(Adat);

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
        #endregion


        #region Ütemezés


        private void Ütem_átütemezés_Click(object sender, EventArgs e)
        {
            if (Ütem_Köv_IDŐvKM.Text.Trim() == "Idő")
                Idő_átütemezés();
            if (Ütem_Köv_IDŐvKM.Text.Trim() == "Km")
                Km_átütemezés();
        }


        private void Idő_átütemezés()
        {
            try
            {
                if (Ütem_pályaszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs kitöltve a pályaszám mező.");
                if (Ütem_Köv_Vizsgálat.Text.Trim() == "") throw new HibásBevittAdat("Nincs meghatározva, hogy milyen vizsgálat lesz a következő");
                if (Ütem_Köv_Státus.Text.Trim() == "") Ütem_Köv_Státus.Text = "0";
                if (Ütem_vizsg_sorszám_km.Text.Trim() == "") throw new HibásBevittAdat("Nincs kitöltve a km sorszám.");
                if (Ütem_vizsg_sorszám_idő.Text.Trim() == "") throw new HibásBevittAdat("Nincs kitöltve az idő sorszám.");
                if (Ütem_köv_sorszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve érvényes adat.");
                if (!int.TryParse(Ütem_köv_sorszám.Text, out int ÜtemKövSorszám)) throw new HibásBevittAdat("Nem érvényes a sorszám.");

                string szöveg = "SELECT * FROM adatok";
                Kezelő_CAF_Adatok kéz = new Kezelő_CAF_Adatok();
                List<Adat_CAF_Adatok> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);
                Adat_CAF_Adatok Adat = (from a in Adatok
                                        where a.Id == ÜtemKövSorszám
                                        select a).FirstOrDefault();

                // ha nem raktuk át másik napra akkor kilépünk
                if (Adat.Dátum_program == Ütem_Köv_Dátum.Value) throw new HibásBevittAdat("Nem történt meg az átütemezés");

                if (Adat != null)
                {
                    // töröltre állítjuk az aktuális sorszámot
                    szöveg = $"UPDATE adatok  SET státus=9 WHERE id={ÜtemKövSorszám}";
                    MyA.ABMódosítás(hely, jelszó, szöveg);

                    // az új sorszám
                    double sorszám = MyCaf.Köv_Sorszám(hely, jelszó);

                    // rögzítjük az új dátumra az adatot

                    // újat hoz létre
                    szöveg = "INSERT INTO adatok (id, azonosító, vizsgálat, Dátum, számláló, státus, km_sorszám, idő_sorszám, idővKM, Dátum_program) VALUES (";
                    szöveg += sorszám + ", "; // id 
                    szöveg += $"'" + Ütem_pályaszám.Text.Trim() + "', "; // azonosító
                    szöveg += $"'" + Ütem_Köv_Vizsgálat.Text.Trim() + "', "; // vizsgálat
                    szöveg += $" '" + Ütem_Köv_Dátum.Value.ToString("yyyy.MM.dd") + "', "; // Dátum
                    szöveg += Ütem_Köv_Számláló.Text + ", "; // számláló
                    szöveg += MyF.Szöveg_Tisztítás(Ütem_Köv_Státus.Text, 0, 1) + ", "; // státus 
                    szöveg += Ütem_vizsg_sorszám_km.Text + ", "; // km_sorszám
                    szöveg += Ütem_vizsg_sorszám_idő.Text + ", "; // idő_sorszám
                    if (Ütem_Köv_IDŐvKM.Text.Trim() == "Idő") // idővKM
                        szöveg += " 1, ";
                    else
                        szöveg += " 2, ";

                    szöveg += " '" + Adat.Dátum_program.ToString("yyyy.MM.dd") + "') "; // Dátum_program
                    MyA.ABMódosítás(hely, jelszó, szöveg);

                    // töröljük az új dátum utáni tervet
                    Adat_CAF_Adatok Töröl = (from a in Adatok
                                             where a.Azonosító == Ütem_pályaszám.Text.Trim()
                                             && a.Dátum > Ütem_Köv_Dátum.Value
                                             && a.Státus == 0
                                             select a).FirstOrDefault();
                    if (Töröl != null)
                    {
                        szöveg = $"DELETE  FROM adatok WHERE azonosító='{Ütem_pályaszám.Text.Trim()}' AND dátum>#";
                        szöveg += Ütem_Köv_Dátum.Value.ToString("MM-dd-yyyy") + "# AND státus=0";
                        MyA.ABtörlés(hely, jelszó, szöveg);
                    }
                    // ütemezzük újra a kocsikat

                    // idő szerit
                    MyCaf.IDŐ_Eltervező_EgyKocsi(Ütem_pályaszám.Text.Trim(), Elő_Dátumig);

                    // km szerint
                    MyCaf.KM_Eltervező_EgyKocsi(Ütem_pályaszám.Text.Trim(), Elő_Dátumig);
                }
                if (Változás != null) Változás();
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


        private void Km_átütemezés()
        {
            try
            {
                // töröltre állítjuk az aktuális sorszámot
                if (Ütem_pályaszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs kitöltve a pályaszám mező.");
                if (Ütem_Köv_Vizsgálat.Text.Trim() == "") throw new HibásBevittAdat("Nincs meghatározva, hogy milyen vizsgálat lesz a következő");
                if (Ütem_Köv_Státus.Text.Trim() == "") Ütem_Köv_Státus.Text = "0";
                if (Ütem_vizsg_sorszám_km.Text.Trim() == "") throw new HibásBevittAdat("Nincs kitöltve a km sorszám.");
                if (Ütem_vizsg_sorszám_idő.Text.Trim() == "") throw new HibásBevittAdat("Nincs kitöltve az idő sorszám.");
                if (Ütem_köv_sorszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve érvényes adat.");
                if (!int.TryParse(Ütem_köv_sorszám.Text, out int ÜtemKövSorszám)) throw new HibásBevittAdat("Nem érvényes a sorszám.");

                string szöveg = "SELECT * FROM adatok";
                Kezelő_CAF_Adatok kéz = new Kezelő_CAF_Adatok();
                List<Adat_CAF_Adatok> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);
                Adat_CAF_Adatok Adat = (from a in Adatok
                                        where a.Id == ÜtemKövSorszám
                                        select a).FirstOrDefault();

                if (Adat.Dátum_program != Ütem_Köv_Dátum.Value)
                {
                    if (Adat != null)
                    {
                        // töröltre állítjuk az aktuális sorszámot
                        szöveg = "UPDATE adatok  SET státus=9 WHERE id=" + Ütem_köv_sorszám.Text.Trim();
                        MyA.ABMódosítás(hely, jelszó, szöveg);

                        // rögzítjük az új dátumra az adatot

                        // ezen a napon ha van már idő alapú akkor töröljük
                        Adat_CAF_Adatok IdőTöröl = (from a in Adatok
                                                    where a.Dátum.ToShortDateString() == Ütem_Köv_Dátum.Value.ToShortDateString()
                                                    && a.Azonosító == Ütem_pályaszám.Text.Trim()
                                                    select a).FirstOrDefault();

                        if (IdőTöröl != null)
                        {
                            szöveg = $"DELETE FROM adatok WHERE [Dátum]=#{Ütem_Köv_Dátum.Value.ToString("MM-dd-yyyy")}# AND azonosító='{Ütem_pályaszám.Text.Trim()}'";
                            MyA.ABtörlés(hely, jelszó, szöveg);
                        }

                        // az új sorszám
                        double sorszám = MyCaf.Köv_Sorszám(hely, jelszó);
                        // az új sorszám
                        szöveg = "SELECT * FROM adatok ORDER BY id desc";
                        Ütem_köv_sorszám.Text = sorszám.ToString();
                        // újat hoz létre
                        szöveg = "INSERT INTO adatok (id, azonosító, vizsgálat, Dátum, számláló, státus, km_sorszám, idő_sorszám, idővKM) VALUES (";
                        szöveg += Ütem_köv_sorszám.Text + ", "; // id 
                        szöveg += "'" + Ütem_pályaszám.Text.Trim() + "', "; // azonosító
                        szöveg += "'" + Ütem_Köv_Vizsgálat.Text.Trim() + "', "; // vizsgálat
                        szöveg += " '" + Ütem_Köv_Dátum.Value.ToString("yyyy.MM.dd").Trim() + "', "; // Dátum
                        szöveg += Ütem_Köv_Számláló.Text + ", "; // számláló
                        szöveg += " 0, "; // státus 
                        szöveg += Ütem_vizsg_sorszám_km.Text + ", "; // km_sorszám
                        szöveg += "0, "; // idő_sorszám
                        if (Ütem_Köv_IDŐvKM.Text.Trim() == "Idő") // idővKM
                            szöveg += " 1) ";
                        else
                            szöveg += " 2) ";

                        MyA.ABMódosítás(hely, jelszó, szöveg);
                    }
                }
                // töröljük az új dátum utáni tervet
                Adat_CAF_Adatok Töröl = (from a in Adatok
                                         where a.Dátum.ToShortDateString() == Ütem_Köv_Dátum.Value.ToShortDateString()
                                         && a.Azonosító == Ütem_pályaszám.Text.Trim()
                                         && a.Státus == 0
                                         select a).FirstOrDefault();

                if (Töröl != null)

                {
                    szöveg = $"DELETE  FROM adatok WHERE azonosító='{Ütem_pályaszám.Text.Trim()}' AND dátum>#{Ütem_Köv_Dátum.Value:MM-dd-yyyy}# AND státus=0";
                    MyA.ABtörlés(hely, jelszó, szöveg);
                }

                // ütemezzük újra a kocsikat

                // idő szerit
                MyCaf.IDŐ_Eltervező_EgyKocsi(Ütem_pályaszám.Text.Trim(), Elő_Dátumig);

                // km szerint
                MyCaf.KM_Eltervező_EgyKocsi(Ütem_pályaszám.Text.Trim(), Elő_Dátumig);

                MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        #endregion


        #region Rögzítés


        private void Ütem_Rögzít_Click(object sender, EventArgs e)
        {
            Rögzíti_ütemet();
            MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void Rögzíti_ütemet()
        {
            try
            {
                if (Ütem_pályaszám.Text.Trim() == "") return;
                if (Ütem_Köv_Vizsgálat.Text.Trim() == "") return;
                if (Ütem_Köv_Státus.Text.Trim() == "") return;
                if (Ütem_vizsg_sorszám_km.Text.Trim() == "") return;
                if (Ütem_vizsg_sorszám_idő.Text.Trim() == "") return;
                if (Ütem_megjegyzés.Text.Trim() == "") Ütem_megjegyzés.Text = "_";
                if (Ütem_Köv_Számláló.Text.Trim() == "") Ütem_Köv_Számláló.Text = 0.ToString();
                if (!double.TryParse(Ütem_köv_sorszám.Text, out double ID)) ID = 0;

                string szöveg = "SELECT * FROM adatok";
                Kezelő_CAF_Adatok kéz = new Kezelő_CAF_Adatok();
                List<Adat_CAF_Adatok> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);
                
                // ha nincs kitöltve az id, megkeressük a következő számot
                if (Ütem_köv_sorszám.Text.Trim() == "" || Ütem_köv_sorszám.Text.Trim() == "0")
                {
                    ID = 1;
                    if (Adatok.Count > 0) ID = Adatok.Max(a => a.Id) + 1;
                    Ütem_köv_sorszám.Text = ID.ToString();
                }

                Adat_CAF_Adatok Elem = (from a in Adatok
                                        where a.Id ==ID
                                        select a ).FirstOrDefault ();
                if (Elem!=null)
                {
                    // Módosít
                    szöveg = "UPDATE adatok  SET ";
                    szöveg += "vizsgálat='" + Ütem_Köv_Vizsgálat.Text.Trim() + "', "; // vizsgálat
                    szöveg += "Dátum='" + Ütem_Köv_Dátum.Value.ToString("yyyy.MM.dd").Trim() + "', "; // Dátum
                    szöveg += "számláló=" + Ütem_Köv_Számláló.Text + ", "; // számláló
                    szöveg += "státus=" + MyF.Szöveg_Tisztítás(Ütem_Köv_Státus.Text, 0, 1) + ", "; // státus 
                    szöveg += "km_sorszám=" + Ütem_vizsg_sorszám_km.Text + ", "; // km_sorszám
                    szöveg += "idő_sorszám=" + Ütem_vizsg_sorszám_idő.Text + ", "; // idő_sorszám
                    szöveg += "megjegyzés='" + Ütem_megjegyzés.Text.Trim() + "', "; // megjegyzés
                    if (Ütem_Köv_IDŐvKM.Text.Trim() == "Idő") // idővKM
                        szöveg += "idővKM=1 ";
                    else
                        szöveg += "idővKM=2 ";

                    szöveg += " WHERE id=" + Ütem_köv_sorszám.Text.Trim();
                }
                else
                {
                    // újat hoz létre
                    szöveg = "INSERT INTO adatok (id, azonosító, vizsgálat, Dátum, számláló, státus, km_sorszám, idő_sorszám, idővKM, megjegyzés, Dátum_program) VALUES (";
                    szöveg += Ütem_köv_sorszám.Text + ", "; // id 
                    szöveg += "'" + Ütem_pályaszám.Text.Trim() + "', "; // azonosító
                    szöveg += "'" + Ütem_Köv_Vizsgálat.Text.Trim() + "', "; // vizsgálat
                    szöveg += " '" + Ütem_Köv_Dátum.Value.ToString("yyyy.MM.dd").Trim() + "', "; // Dátum
                    szöveg += Ütem_Köv_Számláló.Text + ", "; // számláló
                    szöveg += MyF.Szöveg_Tisztítás(Ütem_Köv_Státus.Text, 0, 1) + ", "; // státus 
                    szöveg += Ütem_vizsg_sorszám_km.Text + ", "; // km_sorszám
                    szöveg += Ütem_vizsg_sorszám_idő.Text + ", "; // idő_sorszám
                    if (Ütem_Köv_IDŐvKM.Text.Trim() == "Idő") // idővKM
                        szöveg += " 1, ";
                    else
                        szöveg += " 2, ";

                    szöveg += "'" + Ütem_megjegyzés.Text.Trim() + "', "; // megjegyzés
                    szöveg += " '" + Ütem_dátum_program.Value.ToString("yyyy.MM.dd").Trim() + "') ";
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);

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

        #endregion


        #region IdőUgrás


        private void IDŐ_Click(object sender, EventArgs e)
        {
            try
            {
                if (Ütem_pályaszám.Text.Trim() == "")
                    throw new HibásBevittAdat("Nincs kitöltve a pályaszám mező.");

                //Jármű tulajdonsága
                EgyCAF = MyCaf.Villamos_tulajdonság(Ütem_pályaszám.Text.Trim());
                // utolsó ütemezett
                Adat_CAF_Adatok Előző = MyCaf.Utolsó_ütemezett(Ütem_pályaszám.Text.Trim(), "");
                KiírElőzőAdatot(Előző);

                // következő idő szerinti
                Adat_CAF_Adatok Adat = MyCaf.Következő_Idő(Ciklus_Idő, Előző, EgyCAF);
                KiírEgyAdatot(Adat);

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

        private void Utolsó_ütemezett_kiírása()
        {
            try
            {
                if (Ütem_pályaszám.Text.Trim() == "")
                    throw new HibásBevittAdat("Nincs kiválasztva pályaszám.");
                Adat_CAF_Adatok Adat = MyCaf.Utolsó_ütemezett(Ütem_pályaszám.Text.Trim(), "");
                KiírEgyAdatot(Adat);
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

        private void Segéd_Töröl_Click(object sender, EventArgs e)
        {

        }
    }
}
