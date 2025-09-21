using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.V_Ablakok._0_Bejelentkezés
{
    public partial class Ablak_Szoftver_Kulcs : Form
    {

        readonly Kezelő_Kulcs_Fekete KézKulcs = new Kezelő_Kulcs_Fekete();
        readonly Kezelő_Users KézUsers = new Kezelő_Users();
        readonly Kezelő_Behajtás_Dolgozótábla KézDolgozó = new Kezelő_Behajtás_Dolgozótábla();

        List<Adat_Behajtás_Dolgozótábla> AdatokDolgozó = new List<Adat_Behajtás_Dolgozótábla>();
        List<Adat_Users> AdatokUsers = new List<Adat_Users>();

        int FelhasználóFőId = -1;

        public Ablak_Szoftver_Kulcs()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            Engedélyekfeltöltése();
            AdatokUsers = KézUsers.Lista_Adatok();
            AdatokUsers = (from a in AdatokUsers
                           where a.Törölt == false
                           orderby a.UserName
                           select a).ToList();
            AdatokDolgozó = KézDolgozó.Lista_Adatok().Where(a => a.Státus == true).OrderBy(a => a.Dolgozónév).ToList();
            FelhasználóFeltöltés();
            SzervezetFeltöltés();
            //Csak globaladmin tud módosítani
            Alap_Rögzít.Visible = Program.Postás_Felhasználó.GlobalAdmin;
        }

        private void Ablak_Szoftver_Kulcs_Load(object sender, EventArgs e)
        { }

        /// <summary>
        /// Feltöltjük a felhasználókat a comboxba.
        /// </summary>
        private void FelhasználóFeltöltés()
        {
            try
            {
                TextNév.Items.Clear();
                TextNév.Items.Add("");
                foreach (Adat_Users item in AdatokUsers)
                {
                    TextNév.Items.Add(item.UserName);
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

        private void Engedélyekfeltöltése()
        {
            CMBMireSzemélyes.Items.Clear();
            CMBMireSzemélyes.Items.Add("A - Személyes adatok");
            CMBMireSzemélyes.Items.Add("B - Bér adatok");
            CMBMireSzemélyes.Items.Add("C - Túlóra engedélyezés");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Cmbtelephely.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva érvényes telephely");
                if (CMBMireSzemélyes.CheckedItems.Count <= 0) throw new HibásBevittAdat("Nincs kiválasztva jogosultsági profil");
                if (TextNév.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva dolgozó.");

                List<Adat_Kulcs> AdatokKulcs = KézKulcs.Lista_Adatok();


                for (int i = 0; i < CMBMireSzemélyes.Items.Count; i++)
                {
                    bool volt = false;
                    if (CMBMireSzemélyes.GetItemChecked(i)) // ha be van jelölve
                    {
                        // soronként rögzítjük
                        string adat1 = TextNév.Text.Trim().ToUpper();
                        string adat2 = Cmbtelephely.Text.Trim().ToUpper();
                        string adat3 = CMBMireSzemélyes.Items[i].ToString().Trim().Substring(0, 1);
                        volt = KézKulcs.ABKULCSvan(adat1, adat2, adat3);
                        if (!volt)
                        {
                            // ha nincs ilyen adat akkor nem rögzítjük újra
                            Adat_Kulcs Adat = new Adat_Kulcs(MyF.MÁSKódol(adat1),
                                                             MyF.MÁSKódol(adat2),
                                                             MyF.MÁSKódol(adat3));
                            KézKulcs.Rögzít(Adat);
                        }
                    }
                }

                MessageBox.Show("Az adatok rögzítése megtörtént. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void TextNév_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                TextNév.Text = TextNév.Items[TextNév.SelectedIndex].ToString();
                Adat_Users Felhasználó = AdatokUsers.FirstOrDefault(a => a.UserName == TextNév.Text);
                if (Felhasználó == null)
                {
                    DolgozóNév.Text = $"<< - >>";
                    FelhasználóFőId = -1;
                }
                else
                {
                    FelhasználóFőId = Felhasználó.UserId;
                    Adat_Behajtás_Dolgozótábla dolgozó = AdatokDolgozó.FirstOrDefault(a => a.Dolgozószám == Felhasználó.Dolgozószám);
                    if (dolgozó != null)
                        DolgozóNév.Text = $"<<{dolgozó.Dolgozószám} - {dolgozó.Dolgozónév}>>";
                    else
                        DolgozóNév.Text = $"<<{Felhasználó.Dolgozószám} - >>";
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

        private void SzervezetFeltöltés()
        {
            try
            {
                Kezelő_Kiegészítő_Könyvtár kezSzervezet = new Kezelő_Kiegészítő_Könyvtár();
                List<Adat_Kiegészítő_Könyvtár> adatokSzervezet = kezSzervezet.Lista_Adatok().OrderBy(a => a.Név).ToList();

                Cmbtelephely.Items.Clear();
                for (int i = 0; i < adatokSzervezet.Count; i++)
                {
                    Cmbtelephely.Items.Add(adatokSzervezet[i].Név);
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
