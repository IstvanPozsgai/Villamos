using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;

namespace Villamos.V_Ablakok._1_Bejelentkezés
{
    public partial class Ablak_Ideig : Form
    {
        readonly Kezelő_Kiegészítő_Sérülés KézSérülés = new Kezelő_Kiegészítő_Sérülés();
        readonly Kezelő_Belépés_Jogosultságtábla KézJogOld = new Kezelő_Belépés_Jogosultságtábla();
        readonly SQL_Kezelő_Belépés_Users KézUsers = new SQL_Kezelő_Belépés_Users();

        List<Adat_Bejelentkezés_Users> ÚjFelhasználók = new List<Adat_Bejelentkezés_Users>();
        public Ablak_Ideig()
        {
            InitializeComponent();
            Start();
        }

        private void Ablak_Ideig_Load(object sender, EventArgs e)
        {

        }

        private void Start()
        {
            Telephelyekfeltöltése();
            Újfelhasználóklistája();
        }


        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                Cmbtelephely.Items.Add("");
                List<Adat_Kiegészítő_Sérülés> Adatok = KézSérülés.Lista_Adatok();
                foreach (Adat_Kiegészítő_Sérülés rekord in Adatok)
                    Cmbtelephely.Items.Add(rekord.Név);
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


        private void Cmbtelephely_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Cmbtelephely.Text = Cmbtelephely.Items[Cmbtelephely.SelectedIndex].ToStrTrim();
            Neveklistája();
        }

        private void Neveklistája()
        {
            try
            {
                if (Cmbtelephely.Text.Trim() == "") return;
                List<Adat_Belépés_Jogosultságtábla> AdatokLista = KézJogOld.Lista_Adatok(Cmbtelephely.Text.Trim());


                if (AdatokLista != null)
                {
                    CmbNevekOld.Items.Clear();
                    CmbNevekOld.BeginUpdate();
                    foreach (Adat_Belépés_Jogosultságtábla Elem in AdatokLista)
                        CmbNevekOld.Items.Add(Elem.Név);

                    CmbNevekOld.EndUpdate();
                    CmbNevekOld.Refresh();
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

        private void CmbNevekOld_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CmbNevekOld.Text = CmbNevekOld.Items[CmbNevekOld.SelectedIndex].ToStrTrim();
            if (CmbNevekOld.Text.Trim() == "") return;
            // Megkeressük a dolgozót és kiíjuk a jogosultságait
            List<Adat_Belépés_Jogosultságtábla> Adatok = KézJogOld.Lista_Adatok(Cmbtelephely.Text.Trim());
            Adat_Belépés_Jogosultságtábla rekord = (from a in Adatok
                                                    where a.Név == CmbNevekOld.Text.Trim()
                                                    select a).FirstOrDefault();
            TxtJogkör.Text = rekord.Jogkörúj1;
        }


        private void Újfelhasználóklistája()
        {
            ÚjFelhasználók = KézUsers.Lista_Adatok().OrderBy(a => a.UserName).ToList();

            if (ÚjFelhasználók != null)
            {
                CmbFelhasználóNew.Items.Clear();
                CmbFelhasználóNew.BeginUpdate();
                foreach (Adat_Bejelentkezés_Users Elem in ÚjFelhasználók)
                    CmbFelhasználóNew.Items.Add($"{Elem.UserName}-{Elem.UserId}");

                CmbFelhasználóNew.EndUpdate();
                CmbFelhasználóNew.Refresh();
            }
        }

        private void CmbFelhasználóNew_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CmbFelhasználóNew.Text = CmbFelhasználóNew.Items[CmbFelhasználóNew.SelectedIndex].ToStrTrim();
            if (CmbFelhasználóNew.Text.Trim() == "") return;
            string[] darabol = CmbFelhasználóNew.Text.Trim().Split('-');
            FelhasználóId.Value = darabol[1].ToÉrt_Int();
        }
    }
}
