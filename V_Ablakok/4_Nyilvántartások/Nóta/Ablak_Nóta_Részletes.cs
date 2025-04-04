using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.V_Adatszerkezet;
using Villamos.Villamos_Adatszerkezet;
using static Villamos.V_MindenEgyéb.Enumok;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok
{
    public partial class Ablak_Nóta_Részletes : Form
    {
        public event Event_Kidobó Változás;

        public int Sorszám { get; private set; }

        #region Kezelők
        readonly Kezelő_Nóta KézNóta = new Kezelő_Nóta();
        readonly Kezelő_Kerék_Tábla KézKerék = new Kezelő_Kerék_Tábla();
        readonly Kezelő_Kerék_Mérés KézMérés = new Kezelő_Kerék_Mérés();
        readonly Kezelő_Kiegészítő_Sérülés KézSérülés = new Kezelő_Kiegészítő_Sérülés();
        #endregion

        public Ablak_Nóta_Részletes(int sorszám)
        {
            InitializeComponent();
            Sorszám = sorszám;
            Jogosultságkiosztás();
        }

        private void Jogosultságkiosztás()
        {

            // ide kell az összes gombot tenni amit szabályozni akarunk false
            int melyikelem = 210;

            if (Program.Postás_Vezér || Program.PostásTelephely == "Főmérnökség")
            {
                Osztási.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
                Státus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            }
            else
            {
                Osztási.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
                Státus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            }

            // módosítás 2 
            Rögzít.Visible = MyF.Vanjoga(melyikelem, 2);
            KerékRögzít.Visible = MyF.Vanjoga(melyikelem, 2);
        }

        private void Ablak_Nóta_Részletes_Load(object sender, EventArgs e)
        {
            TelephelyFeltöltés();
            StátusFeltöltés();
            Kerékállapotfeltöltés();
            BeépíthetőFeltöltés();
            AdatokKiírása();
        }

        private void BeépíthetőFeltöltés()
        {
            Beépíthető.Items.Add("");
            Beépíthető.Items.Add("Igen");
            Beépíthető.Items.Add("Nem");
        }

        private void Kerékállapotfeltöltés()
        {
            try
            {
                foreach (Kerék_Állapot elem in Enum.GetValues(typeof(Kerék_Állapot)))
                    Állapot.Items.Add($"{(int)elem} - {elem.ToString().Replace('_', ' ')}");

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

        private void StátusFeltöltés()
        {
            try
            {
                foreach (Nóta_Státus elem in Enum.GetValues(typeof(Nóta_Státus)))
                    Státus.Items.Add($"{(int)elem} - {elem.ToString().Replace('_', ' ')}");

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

        private void TelephelyFeltöltés()
        {
            try
            {
                Telephely.Items.Clear();
                List<Adat_Kiegészítő_Sérülés> Adatok = KézSérülés.Lista_Adatok().Where(a => a.Vezér1 == false).OrderBy(a => a.Név).ToList();
                foreach (Adat_Kiegészítő_Sérülés Elem in Adatok)
                    Telephely.Items.Add(Elem.Név);
                Telephely.Items.Add("VJSZ");
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

        private void AdatokKiírása()
        {
            try
            {
                List<Adat_Nóta> Adatok = KézNóta.Lista_Adat(false);
                List<Adat_Kerék_Tábla> AdatokKerék = KézKerék.Lista_Adatok();

                List<Adat_Kerék_Mérés> AdatokMérés = KézMérés.Lista_Adatok(DateTime.Today.Year - 1);
                List<Adat_Kerék_Mérés> Ideig = KézMérés.Lista_Adatok(DateTime.Today.Year);
                AdatokMérés.AddRange(Ideig);
                AdatokMérés = AdatokMérés.OrderBy(a => a.Mikor).ToList();

                Adat_Nóta rekord = Adatok.FirstOrDefault(x => x.Id == Sorszám);
                if (rekord != null)
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

                    Id.Text = rekord.Id.ToString();
                    Berendezés.Text = rekord.Berendezés;
                    KészletSarzs.Text = rekord.Készlet_Sarzs;
                    Raktár.Text = rekord.Raktár;
                    Telephely.Text = rekord.Telephely;
                    Forgóváz.Text = rekord.Forgóváz;
                    GyártásiSzám.Text = gyáriszám;
                    Beépíthető.Text = rekord.Beépíthető ? "Igen" : "Nem";
                    Műszaki.Text = rekord.MűszakiM;
                    Osztási.Text = rekord.OsztásiM;
                    Dátum.Value = rekord.Dátum;
                    Státus.Text = $"{rekord.Státus} - {((Nóta_Státus)rekord.Státus).ToStrTrim().Replace('_', ' ')}";
                    Átmérő.Text = átmérő.ToStrTrim();
                    Állapot.Text = állapot;

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

        private void KerékRögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Átmérő.Text, out int Méret)) Méret = 0;

                Adat_Kerék_Mérés ADAT = new Adat_Kerék_Mérés(
                    "Kiépített",
                    "-",
                    Berendezés.Text.Trim(),
                    GyártásiSzám.Text.Trim(),
                    Állapot.Text.Trim().Substring(0, 1),
                    Méret,
                    Program.PostásNév,
                    DateTime.Now,
                    "Nóta adatok",
                    0);
                KézMérés.Rögzítés(DateTime.Today.Year, ADAT);
                Változás?.Invoke();
                MessageBox.Show("Az adatok rögzítése megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Id.Text, out int Sorszám)) Sorszám = 0;
                if (Sorszám == 0) return;
                Adat_Nóta ADAT = new Adat_Nóta(
                                Sorszám,
                                Berendezés.Text.Trim(),
                                KészletSarzs.Text.Trim(),
                                Raktár.Text.Trim(),
                                Telephely.Text.Trim(),
                                Forgóváz.Text.Trim(),
                                Beépíthető.Text.Trim() == "Igen",
                                Műszaki.Text.Trim(),
                                Osztási.Text.Trim(),
                                Dátum.Value,
                                Státus.Text.Trim().Substring(0, 1).ToÉrt_Int());

                KézNóta.Módosítás(ADAT);
                Változás?.Invoke();
                MessageBox.Show("Az adatok rögzítése megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Beépíthető_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Beépíthető.Text.Trim() == "Igen")
                Státus.Text = "7 - Felhasználható";
            else
                Státus.Text = "1 - Feldolgozandó";
        }
    }
}
