using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.V_Adatszerkezet;
using Villamos.Villamos_Adatszerkezet;
using static Villamos.V_MindenEgyéb.Enumok;

namespace Villamos.V_Ablakok._4_Nyilvántartások.Nóta
{
    public partial class Ablak_Nóta_Részletes : Form
    {

        #region Kezelők
        readonly Kezelő_Nóta KézNóta = new Kezelő_Nóta();
        readonly Kezelő_Kerék_Tábla KézKerék = new Kezelő_Kerék_Tábla();
        readonly Kezelő_Kerék_Mérés KézMérés = new Kezelő_Kerék_Mérés();
        #endregion



        public int Sorszám { get; private set; }
        public Ablak_Nóta_Részletes(int sorszám)
        {
            InitializeComponent();
            Sorszám = sorszám;
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
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    Telephely.Items.Add(Elem);
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
                List<Adat_Nóta> Adatok = KézNóta.Lista_Adat();
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


    }
}
