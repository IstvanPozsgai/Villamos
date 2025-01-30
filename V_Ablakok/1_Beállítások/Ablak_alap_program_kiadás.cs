using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;
using MySz = Villamos.V_MindenEgyéb.Kezelő_Szín;

namespace Villamos
{

    public partial class Ablak_alap_program_kiadás
    {
        #region Kezelők
        readonly Kezelő_Kiegészítő_Sérülés KézSérülés = new Kezelő_Kiegészítő_Sérülés();
        readonly Kezelő_Kiegészítő_Típusrendezéstábla KézKiegTípus = new Kezelő_Kiegészítő_Típusrendezéstábla();
        readonly Kezelő_Kiegészítő_Idő_Tábla Kéz_Idő = new Kezelő_Kiegészítő_Idő_Tábla();
        readonly Kezelő_Kiegészítő_Idő_Kor KézIdőKor = new Kezelő_Kiegészítő_Idő_Kor();
        readonly Kezelő_Kiegészítő_főkönyvtábla Kézfőkönyvtábla = new Kezelő_Kiegészítő_főkönyvtábla();
        readonly Kezelő_Kiegészítő_Mentésihelyek KézMentésihelyek = new Kezelő_Kiegészítő_Mentésihelyek();
        readonly Kezelő_Jármű_Állomány_Típus Kéz_Állomány_Típus = new Kezelő_Jármű_Állomány_Típus();
        readonly Kezelő_Kiegészítő_Típuszínektábla KézTípuszínektábla = new Kezelő_Kiegészítő_Típuszínektábla();
        readonly Kezelő_Telep_Kiegészítő_Kidobó KézKidobó = new Kezelő_Telep_Kiegészítő_Kidobó();
        readonly Kezelő_Telep_Kiegészítő_SAP KézSap = new Kezelő_Telep_Kiegészítő_SAP();
        readonly Kezelő_T5C5_Göngyöl_DátumTábla KézDátumTábla = new Kezelő_T5C5_Göngyöl_DátumTábla();
        readonly Kezelő_Kiegészítő_Igen_Nem KézIgenNem = new Kezelő_Kiegészítő_Igen_Nem();
        readonly Kezelő_Telep_Kieg_Fortetípus KézKiegFortetíp = new Kezelő_Telep_Kieg_Fortetípus();
        readonly Kezelő_kiegészítő_Hibaterv KézKiegHiba = new Kezelő_kiegészítő_Hibaterv();
        readonly Kezelő_Telep_Kiegészítő_E3típus KézE3Típus = new Kezelő_Telep_Kiegészítő_E3típus();
        readonly Kezelő_Telep_Kiegészítő_Takarítástípus KézTakarítástípus = new Kezelő_Telep_Kiegészítő_Takarítástípus();
        readonly Kezelő_Kiegészítő_Szolgálat KézKiegSzolg = new Kezelő_Kiegészítő_Szolgálat();
        readonly Kezelő_Kiegészítő_Adatok_Terjesztés KézTerjesztés = new Kezelő_Kiegészítő_Adatok_Terjesztés();
        readonly Kezelő_Kiegészítő_Főkategóriatábla KézFőkategóriatábla = new Kezelő_Kiegészítő_Főkategóriatábla();
        readonly Kezelő_Kiegészítő_Reklám KézReklám = new Kezelő_Kiegészítő_Reklám();
        readonly Kezelő_Kiegészítő_Forte_Vonal KézForte_Vonal = new Kezelő_Kiegészítő_Forte_Vonal();
        readonly Kezelő_kiegészítő_telephely Kézkiegészítő_telephely = new Kezelő_kiegészítő_telephely();
        readonly Kezelő_Kiegészítő_Szolgálattelepei KézSzolgálattelepei = new Kezelő_Kiegészítő_Szolgálattelepei();
        readonly Kezelő_Kiegészítő_Típusaltípustábla KézTípusaltípustábla = new Kezelő_Kiegészítő_Típusaltípustábla();
        readonly Kezelő_Kiegészítő_Fortetípus KézKiegFortetípus = new Kezelő_Kiegészítő_Fortetípus();
        #endregion

        public Ablak_alap_program_kiadás()
        {

            InitializeComponent();
            BtnMmegnyitás = _BtnMmegnyitás;
            BtnMentésihely = _BtnMentésihely;
            _BtnMmegnyitás.Name = "BtnMmegnyitás";
            _BtnMentésihely.Name = "BtnMentésihely";
        }


        #region alap
        private void AblakBeállításkiadás_Load(object sender, EventArgs e)
        {
            try
            {
                Telephelyekfeltöltése();
                if (Cmbtelephely.Items.Count > 0)
                {// Ha telephelyek feltöltése sikeres csak akkor fejezi be az ablak kitöltését
                    Jogosultságkiosztás();
                    Típusfeltöltés();
                    Főkönyvialáírások();
                    Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;
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

        private void Típusfeltöltés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\Jármű.mdb";

                List<Adat_Jármű_Állomány_Típus> Adatok = Kéz_Állomány_Típus.Lista_adatok(hely);

                CmbTípus.Items.Clear();
                CmbForteTípus.Items.Clear();
                CmbE3típus.Items.Clear();
                CmbTakTípus.Items.Clear();
                LstTípuslét.Items.Clear();

                foreach (Adat_Jármű_Állomány_Típus rekord in Adatok)
                {
                    CmbTípus.Items.Add(rekord.Típus);
                    CmbForteTípus.Items.Add(rekord.Típus);
                    CmbE3típus.Items.Add(rekord.Típus);
                    CmbTakTípus.Items.Add(rekord.Típus);
                    LstTípuslét.Items.Add(rekord.Típus);
                }
                CmbTípus.Refresh();
                CmbForteTípus.Refresh();
                CmbE3típus.Refresh();
                CmbTakTípus.Refresh();
                LstTípuslét.Refresh();
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

        private void Telephelyekfeltöltése()
        {
            try
            {
                Kezelő_Kiegészítő_Sérülés KézSérülés = new Kezelő_Kiegészítő_Sérülés();
                Cmbtelephely.Items.Clear();
                List<Adat_Kiegészítő_Sérülés> Adatok = KézSérülés.Lista_Adatok();
                foreach (Adat_Kiegészítő_Sérülés rekord in Adatok)
                    Cmbtelephely.Items.Add(rekord.Név);
                Cmbtelephely.Refresh();

                if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér)
                { Cmbtelephely.Text = Cmbtelephely.Items[0].ToStrTrim(); }
                else
                { Cmbtelephely.Text = Program.PostásTelephely; }

                Cmbtelephely.Enabled = Program.Postás_Vezér;

                if (Cmbtelephely.Items.Contains(Program.PostásTelephely.Trim())) Program.Postás_telephely = true;
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

        private void Cmbtelephely_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Fülekkitöltése();
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
            try
            {
                int melyikelem;
                // ha Főmérnökség jelölve akkor a gombok látszódnak
                if (Program.PostásTelephely == "Főmérnökség")
                {
                    BtnIszolgálat.Visible = true;
                    BtnIIszolgálat.Visible = true;
                    BtnIIIszolgálat.Visible = true;

                    BtnMreggel.Visible = true;
                    BtnMdélután.Visible = true;
                    BtnMeste.Visible = true;
                    BtnHreggel.Visible = true;
                    BtnHdélután.Visible = true;
                    BtnHeste.Visible = true;

                    Korrekció_kiadási_rögzít.Visible = true;


                    BtnForteVonaltöröl.Visible = true;
                    BtnForteVonalOk.Visible = true;
                    BtnReklámméretTöröl.Visible = true;
                    BtnReklámméretOk.Visible = true;
                    BtnFőkategóriaOK.Visible = true;
                    BtnFőkategóriaTöröl.Visible = true;
                    BtnFőkategóriaFel.Visible = true;
                    BtnSzolgálatOK.Visible = true;
                    BtnSzolgálatTöröl.Visible = true;
                    BtnSzolgálatFel.Visible = true;

                    BtnTelephelyekOK.Visible = true;
                    BtnTelephelyekTöröl.Visible = true;
                    BtnTelephelyekFel.Visible = true;

                    BtnSzolgtelepOK.Visible = true;
                    BtnSzolgtelepTöröl.Visible = true;
                    BtnSzolgtelepFel.Visible = true;

                    BtnTípusAltípusOK.Visible = true;
                    BtnTípusAltípusTöröl.Visible = true;
                    BtnTípusAltípusFel.Visible = true;

                    BtnFőforteOK.Visible = true;
                    BtnFőforteTöröl.Visible = true;

                    BtnKapcsolatOK.Visible = true;
                    BtnKapcsolatTöröl.Visible = true;
                    BtnKapcsolatFel.Visible = true;

                    BtnForte.Visible = false;
                    BtnSAPmunkahely.Visible = false;
                    Rögzít_göngyölés.Visible = false;
                    E2_vizsgálat.Visible = false;

                    Telep_Rögzít.Visible = true;
                    Telep_Töröl.Visible = true;
                }
                else
                {
                    BtnIszolgálat.Visible = false;
                    BtnIIszolgálat.Visible = false;
                    BtnIIIszolgálat.Visible = false;

                    BtnMreggel.Visible = false;
                    BtnMdélután.Visible = false;
                    BtnMeste.Visible = false;
                    BtnHreggel.Visible = false;
                    BtnHdélután.Visible = false;
                    BtnHeste.Visible = false;

                    Korrekció_kiadási_rögzít.Visible = false;

                    BtnForteVonaltöröl.Visible = false;
                    BtnForteVonalOk.Visible = false;
                    BtnReklámméretTöröl.Visible = false;
                    BtnReklámméretOk.Visible = false;
                    BtnFőkategóriaOK.Visible = false;
                    BtnFőkategóriaTöröl.Visible = false;
                    BtnFőkategóriaFel.Visible = false;
                    BtnSzolgálatOK.Visible = false;
                    BtnSzolgálatTöröl.Visible = false;
                    BtnSzolgálatFel.Visible = false;

                    BtnTelephelyekOK.Visible = false;
                    BtnTelephelyekTöröl.Visible = false;
                    BtnTelephelyekFel.Visible = false;

                    BtnSzolgtelepOK.Visible = false;
                    BtnSzolgtelepTöröl.Visible = false;
                    BtnSzolgtelepFel.Visible = false;

                    BtnTípusAltípusOK.Visible = false;
                    BtnTípusAltípusTöröl.Visible = false;
                    BtnTípusAltípusFel.Visible = false;

                    BtnFőforteOK.Visible = false;
                    BtnFőforteTöröl.Visible = false;

                    BtnKapcsolatOK.Visible = false;
                    BtnKapcsolatTöröl.Visible = false;
                    BtnKapcsolatFel.Visible = false;

                    BtnForte.Visible = true;
                    BtnSAPmunkahely.Visible = true;
                    Rögzít_göngyölés.Visible = true;
                    E2_vizsgálat.Visible = true;

                    Telep_Rögzít.Visible = false;
                    Telep_Töröl.Visible = false;
                }
                // ha telephely jelölve akkor a gombok látszódnak
                if (Program.Postás_telephely)
                {
                    Button2.Visible = true;
                    Button3.Visible = true;

                    BtnMentésihely.Visible = true;
                    Btnfőkönyv.Visible = true;

                    BtnHibatervTöröl.Visible = true;
                    BtnHibatervÚj.Visible = true;
                    BtnHibatervFel.Visible = true;
                    BtnHibatervRögzít.Visible = true;

                    BtnForteTípusTörlés.Visible = true;
                    BtnForteTípusRögzítés.Visible = true;

                    BtnForte.Visible = true;
                    BtnSAPmunkahely.Visible = true;

                    BtnE3TípusOK.Visible = true;
                    BtnE3TípusTöröl.Visible = true;
                    BtnTAKtípusOK.Visible = true;
                    BtnTAKtípusTöröl.Visible = true;

                    BtnTíputlétFel.Visible = true;
                    BtnTíputléttöröl.Visible = true;
                    BtnTíputlétOK.Visible = true;
                }

                else
                {
                    Button2.Visible = false;
                    Button3.Visible = false;

                    BtnMentésihely.Visible = false;
                    Btnfőkönyv.Visible = false;

                    BtnHibatervTöröl.Visible = false;
                    BtnHibatervÚj.Visible = false;
                    BtnHibatervFel.Visible = false;
                    BtnHibatervRögzít.Visible = false;

                    BtnForteTípusTörlés.Visible = false;
                    BtnForteTípusRögzítés.Visible = false;

                    BtnForte.Visible = false;
                    BtnSAPmunkahely.Visible = false;

                    BtnE3TípusOK.Visible = false;
                    BtnE3TípusTöröl.Visible = false;
                    BtnTAKtípusOK.Visible = false;
                    BtnTAKtípusTöröl.Visible = false;

                    BtnTíputlétFel.Visible = false;
                    BtnTíputléttöröl.Visible = false;
                    BtnTíputlétOK.Visible = false;
                }

                if (Program.Postás_Vezér)
                {
                    switch (Program.Postás_csoport)
                    {
                        case 1:
                            {
                                BtnIszolgálat.Visible = true;
                                break;
                            }
                        case 2:
                            {
                                BtnIIszolgálat.Visible = true;
                                break;
                            }
                        case 3:
                            {
                                BtnIIIszolgálat.Visible = true;
                                break;
                            }
                    }

                }
                // ide kell az összes gombot tenni amit szabályozni akarunk

                BtnIszolgálat.Enabled = false;
                BtnIIszolgálat.Enabled = false;
                BtnIIIszolgálat.Enabled = false;

                Button2.Enabled = false;
                Button3.Enabled = false;

                BtnMentésihely.Enabled = false;
                Btnfőkönyv.Enabled = false;

                BtnHibatervTöröl.Enabled = false;
                BtnHibatervÚj.Enabled = false;
                BtnHibatervFel.Enabled = false;
                BtnHibatervRögzít.Enabled = false;

                BtnForteTípusTörlés.Enabled = false;
                BtnForteTípusRögzítés.Enabled = false;

                BtnForte.Enabled = false;
                BtnSAPmunkahely.Enabled = false;

                BtnE3TípusOK.Enabled = false;
                BtnE3TípusTöröl.Enabled = false;
                BtnTAKtípusOK.Enabled = false;
                BtnTAKtípusTöröl.Enabled = false;

                BtnMreggel.Enabled = false;
                BtnMdélután.Enabled = false;
                BtnMeste.Enabled = false;
                BtnHreggel.Enabled = false;
                BtnHdélután.Enabled = false;
                BtnHeste.Enabled = false;

                Korrekció_kiadási_rögzít.Enabled = false;

                BtnForteVonaltöröl.Enabled = false;
                BtnForteVonalOk.Enabled = false;
                BtnReklámméretTöröl.Enabled = false;
                BtnReklámméretOk.Enabled = false;
                BtnFőkategóriaOK.Enabled = false;
                BtnFőkategóriaTöröl.Enabled = false;
                BtnFőkategóriaFel.Enabled = false;
                BtnSzolgálatOK.Enabled = false;
                BtnSzolgálatTöröl.Enabled = false;
                BtnSzolgálatFel.Enabled = false;

                BtnTíputlétFel.Enabled = false;
                BtnTíputléttöröl.Enabled = false;
                BtnTíputlétOK.Enabled = false;

                BtnTelephelyekOK.Enabled = false;
                BtnTelephelyekTöröl.Enabled = false;
                BtnTelephelyekFel.Enabled = false;

                BtnSzolgtelepOK.Enabled = false;
                BtnSzolgtelepTöröl.Enabled = false;
                BtnSzolgtelepFel.Enabled = false;

                BtnTípusAltípusOK.Enabled = false;
                BtnTípusAltípusTöröl.Enabled = false;
                BtnTípusAltípusFel.Enabled = false;

                BtnFőforteOK.Enabled = false;
                BtnFőforteTöröl.Enabled = false;

                BtnKapcsolatOK.Enabled = false;
                BtnKapcsolatTöröl.Enabled = false;
                BtnKapcsolatFel.Enabled = false;

                Telep_Rögzít.Enabled = false;
                Telep_Töröl.Enabled = false;

                // telephelyi módosítások

                melyikelem = 2;
                // módosítás 1

                if (MyF.Vanjoga(melyikelem, 1))
                {
                    // telephelyi módosítások
                    Button2.Enabled = true;
                    Button3.Enabled = true;

                    BtnMentésihely.Enabled = true;
                    Btnfőkönyv.Enabled = true;

                    BtnHibatervTöröl.Enabled = true;
                    BtnHibatervÚj.Enabled = true;
                    BtnHibatervFel.Enabled = true;
                    BtnHibatervRögzít.Enabled = true;

                    BtnForteTípusTörlés.Enabled = true;
                    BtnForteTípusRögzítés.Enabled = true;

                    BtnForte.Enabled = true;
                    BtnSAPmunkahely.Enabled = true;


                    BtnE3TípusOK.Enabled = true;
                    BtnE3TípusTöröl.Enabled = true;
                    BtnTAKtípusOK.Enabled = true;
                    BtnTAKtípusTöröl.Enabled = true;

                    BtnTíputlétFel.Enabled = true;
                    BtnTíputléttöröl.Enabled = true;
                    BtnTíputlétOK.Enabled = true;
                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))

                {


                }
                // módosítás 3
                if (MyF.Vanjoga(melyikelem, 3))

                {

                }
                // Szakszolgálati gombok módosítások
                melyikelem = 3;
                // módosítás 1
                if (MyF.Vanjoga(melyikelem, 1))

                {
                    BtnIszolgálat.Enabled = true;

                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))

                {
                    BtnIIszolgálat.Enabled = true;


                }
                // módosítás 3
                if (MyF.Vanjoga(melyikelem, 3))

                {
                    BtnIIIszolgálat.Enabled = true;
                }
                // Főmérnökségi módosítások
                melyikelem = 4;
                // módosítás 1
                if (MyF.Vanjoga(melyikelem, 1))

                {

                    BtnIszolgálat.Enabled = true;
                    BtnMreggel.Enabled = true;
                    BtnMdélután.Enabled = true;
                    BtnMeste.Enabled = true;
                    BtnHreggel.Enabled = true;
                    BtnHdélután.Enabled = true;
                    BtnHeste.Enabled = true;

                    Korrekció_kiadási_rögzít.Enabled = true;

                    BtnForteVonaltöröl.Enabled = true;
                    BtnForteVonalOk.Enabled = true;
                    BtnReklámméretTöröl.Enabled = true;
                    BtnReklámméretOk.Enabled = true;
                    BtnFőkategóriaOK.Enabled = true;
                    BtnFőkategóriaTöröl.Enabled = true;
                    BtnFőkategóriaFel.Enabled = true;
                    BtnSzolgálatOK.Enabled = true;
                    BtnSzolgálatTöröl.Enabled = true;
                    BtnSzolgálatFel.Enabled = true;

                    BtnTelephelyekOK.Enabled = true;
                    BtnTelephelyekTöröl.Enabled = true;
                    BtnTelephelyekFel.Enabled = true;

                    BtnSzolgtelepOK.Enabled = true;
                    BtnSzolgtelepTöröl.Enabled = true;
                    BtnSzolgtelepFel.Enabled = true;

                    BtnTípusAltípusOK.Enabled = true;
                    BtnTípusAltípusTöröl.Enabled = true;
                    BtnTípusAltípusFel.Enabled = true;

                    BtnFőforteOK.Enabled = true;
                    BtnFőforteTöröl.Enabled = true;

                    BtnKapcsolatOK.Enabled = true;
                    BtnKapcsolatTöröl.Enabled = true;
                    BtnKapcsolatFel.Enabled = true;

                    Telep_Rögzít.Enabled = true;
                    Telep_Töröl.Enabled = true;
                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))

                {
                    BtnIIszolgálat.Enabled = true;


                }
                // módosítás 3
                if (MyF.Vanjoga(melyikelem, 3))

                {
                    BtnIIIszolgálat.Enabled = true;
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

        private void Button13_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\kiadási.html";
                Module_Excel.Megnyitás(hely);
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

        private void Fülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Fülekkitöltése();
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

        private void Fülekkitöltése()
        {
            try
            {
                switch (Fülek.SelectedIndex)
                {
                    case 0:
                        {
                            Főkönyvialáírások();
                            break;
                        }
                    case 1:
                        {
                            Mentésihelyekkiírása();
                            break;
                        }
                    case 2:
                        {
                            Típusfeltöltés();
                            Színtáblafeltöltés();
                            break;
                        }
                    case 3:
                        {
                            Sap_Forte_T5C5();
                            break;
                        }
                    case 4:
                        {
                            TáblaForteTípusfeltöltés();
                            break;
                        }
                    case 5:
                        {
                            TáblaHibatervlistázás();
                            break;
                        }
                    case 6:
                        {
                            ListE3Típusfeltöltés();
                            ListTAKTípusfeltöltés();
                            break;
                        }
                    case 7:
                        {
                            Szolgálatokmenetkimaradása();
                            break;
                        }
                    case 8:
                        {
                            Kiadásiidőkkiírása();
                            break;
                        }
                    case 9:
                        {
                            LstForteVonalListázás();
                            Lstreklámméretlistázás();
                            LstFőkategórialistázás();
                            Táblaszolgálatlistázás();
                            LstReklámcheck();
                            break;
                        }
                    case 10:
                        {
                            Szolgálattelephelylista();
                            TáblaTelephelyeklistázás();
                            SZakSzolgálat_feltöltés();
                            SZakSzolgálat_Telep_feltöltés();

                            break;
                        }
                    case 11:
                        {
                            Típusaltípuslista();
                            TípusAltípusKategóriabetöltés();

                            Fortekódlista();
                            Főfortetelephelylista();
                            break;
                        }
                    case 12:
                        {
                            TáblaKapcsolatAltípus();
                            TáblaKapcsolatkapcsoltadat();
                            KapcsolatTelephelylista();
                            break;
                        }

                    case 13:
                        {
                            Telep_Tábla_kiirás();
                            break;
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

        private void Fülek_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                // Határozza meg, hogy melyik lap van jelenleg kiválasztva
                TabPage SelectedTab = Fülek.TabPages[e.Index];

                // Szerezze be a lap fejlécének területét
                Rectangle HeaderRect = Fülek.GetTabRect(e.Index);

                // Hozzon létreecsetet a szöveg megfestéséhez
                SolidBrush BlackTextBrush = new SolidBrush(Color.Black);

                // Állítsa be a szöveg igazítását
                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                // Festse meg a szöveget a megfelelő félkövér és szín beállítással
                if ((e.State & DrawItemState.Selected) != 0)
                {
                    Font BoldFont = new Font(Fülek.Font.Name, Fülek.Font.Size, FontStyle.Bold);
                    // háttér szín beállítása
                    e.Graphics.FillRectangle(new SolidBrush(Color.DarkGray), e.Bounds);
                    Rectangle paddedBounds = e.Bounds;
                    paddedBounds.Inflate(0, 0);
                    e.Graphics.DrawString(SelectedTab.Text, BoldFont, BlackTextBrush, paddedBounds, sf);
                }
                else
                {
                    e.Graphics.DrawString(SelectedTab.Text, e.Font, BlackTextBrush, HeaderRect, sf);
                }
                // Munka kész – dobja ki a keféket
                BlackTextBrush.Dispose();

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


        #region Aláírások
        private void Btnfőkönyv_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő.mdb";

                Adat_Kiegészítő_főkönyvtábla ADAT = new Adat_Kiegészítő_főkönyvtábla(2, txtnév2.Text.Trim(), txtbeosztás2.Text.Trim());

                Kézfőkönyvtábla.Módosítás(hely, ADAT);

                ADAT = new Adat_Kiegészítő_főkönyvtábla(3, txtnév3.Text.Trim(), txtbeosztás3.Text.Trim());
                Kézfőkönyvtábla.Módosítás(hely, ADAT);


                Főkönyvialáírások();
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Főkönyvialáírások()
        {
            try
            {
                // főkönyvi aláírások
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő.mdb";

                List<Adat_Kiegészítő_főkönyvtábla> Adatok = Kézfőkönyvtábla.Lista_Adatok(hely);

                Adat_Kiegészítő_főkönyvtábla Adat = (from a in Adatok
                                                     where a.Id == 2
                                                     select a).FirstOrDefault();
                if (Adat != null)
                {
                    txtnév2.Text = Adat.Név;
                    txtbeosztás2.Text = Adat.Beosztás;
                }

                Adat = (from a in Adatok
                        where a.Id == 3
                        select a).FirstOrDefault();

                if (Adat != null)
                {
                    txtnév3.Text = Adat.Név;
                    txtbeosztás3.Text = Adat.Beosztás;
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


        #region Főkönyv Mentési hely
        private void BtnMentésihely_Click(object sender, EventArgs e)
        {
            try
            {
                // mentési helyek rögzítése
                if (txtMelérési.Text.Trim() == "") txtMelérési.Text = "Nincs";
                if (!int.TryParse(txtMsorszám.Text.Trim(), out int sorszám)) throw new HibásBevittAdat("A sorszám mezőbe egész számot kell írni.");

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő1.mdb";

                List<Adat_Kiegészítő_Mentésihelyek> Adatok = KézMentésihelyek.Lista_Adatok(hely);

                Adat_Kiegészítő_Mentésihelyek Adat = (from a in Adatok
                                                      where a.Sorszám == sorszám
                                                      select a).FirstOrDefault();

                Adat_Kiegészítő_Mentésihelyek ADAT = new Adat_Kiegészítő_Mentésihelyek(sorszám,
                                                                       txtMalprogram.Text.Trim(),
                                                                       txtMelérési.Text.Trim());

                if (Adat != null)
                    KézMentésihelyek.Módosítás(hely, ADAT);
                else
                    KézMentésihelyek.Rögzítés(hely, ADAT);


                Mentésihelyekkiírása();
                MessageBox.Show("Az adatok módosítása megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Mentésihelyekkiírása()
        {
            try
            {
                // mentési helyek kiirása
                Táblamentés.Visible = false;
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő1.mdb";

                List<Adat_Kiegészítő_Mentésihelyek> AdatokKiegMent = KézMentésihelyek.Lista_Adatok(hely);

                Táblamentés.ColumnCount = 3;
                Táblamentés.RowCount = 0;
                // fejléc elkészítése
                Táblamentés.Columns[0].HeaderText = "Sorszám";
                Táblamentés.Columns[0].Width = 80;
                Táblamentés.Columns[1].HeaderText = "Alprogram";
                Táblamentés.Columns[1].Width = 240;
                Táblamentés.Columns[2].HeaderText = "Elérési út";
                Táblamentés.Columns[2].Width = 600;

                foreach (Adat_Kiegészítő_Mentésihelyek rekord in AdatokKiegMent)
                {
                    Táblamentés.RowCount++;
                    int i = Táblamentés.RowCount - 1;
                    Táblamentés.Rows[i].Cells[0].Value = rekord.Sorszám;
                    Táblamentés.Rows[i].Cells[1].Value = rekord.Alprogram;
                    Táblamentés.Rows[i].Cells[2].Value = rekord.Elérésiút;
                }
                Táblamentés.Visible = true;
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

        private void Főkönyv_mentés_Törlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtMsorszám.Text.Trim(), out int sorszám)) throw new HibásBevittAdat("A sorszámnak számnak kell lennie.");

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő1.mdb";

                List<Adat_Kiegészítő_Mentésihelyek> Adatok = KézMentésihelyek.Lista_Adatok(hely);

                Adat_Kiegészítő_Mentésihelyek Adat = (from a in Adatok
                                                      where a.Sorszám == sorszám
                                                      select a).FirstOrDefault();
                Adat_Kiegészítő_Mentésihelyek ADAT = new Adat_Kiegészítő_Mentésihelyek(sorszám, "", "");
                if (Adat != null)
                {
                    KézMentésihelyek.Törlés(hely, ADAT);
                    MessageBox.Show("Az adatok törlése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Mentésihelyekkiírása();

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

        private void Táblamentés_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Táblamentés.RowCount == 0) throw new HibásBevittAdat("A Típus mező nem lehet üres.");
                if (e.RowIndex >= 0)
                {
                    txtMsorszám.Text = Táblamentés.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtMelérési.Text = Táblamentés.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txtMalprogram.Text = Táblamentés.Rows[e.RowIndex].Cells[1].Value.ToString();
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

        private void BtnMmegnyitás_Click(object sender, EventArgs e)
        {
            try
            {
                // megpróbáljuk megnyitni az excel táblát.
                FolderBrowserDialog FolderBrowserDialog1 = new FolderBrowserDialog();
                FolderBrowserDialog1.ShowDialog();
                txtMelérési.Text = FolderBrowserDialog1.SelectedPath;
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


        #region telephelyi típusok
        private void LstTíputlét_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (LstTípuslét.Items.Count < 1) return;
                txtTípuslét.Text = LstTípuslét.SelectedItem.ToStrTrim();
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

        private void BtnTíputlétOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTípuslét.Text.Trim() == "") throw new HibásBevittAdat("A Típust meg kell adni nem lehet üres mező.");
                // Leellenőrizzük, hogy van-e már ilyen típus
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\Jármű.mdb";



                List<Adat_Jármű_Állomány_Típus> Adatok = Kéz_Állomány_Típus.Lista_adatok(hely);

                Adat_Jármű_Állomány_Típus rekord = (from a in Adatok
                                                    where a.Típus == txtTípuslét.Text.Trim()
                                                    select a).FirstOrDefault();
                if (rekord == null)
                {
                    // ha nincs még ilyen típus akkor létrehozzuk
                    // melyik az utolsó elem
                    long i = 1;
                    if (Adatok.Count > 0) i = Adatok.Max(a => a.Id) + 1;
                    Adat_Jármű_Állomány_Típus ADAT = new Adat_Jármű_Állomány_Típus(i,
                                                                                   0,
                                                                                   txtTípuslét.Text.Trim());
                    Kéz_Állomány_Típus.Rögzítés(hely, ADAT);
                    MessageBox.Show("Az adatok módosítása megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Típusfeltöltés();

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

        private void BtnTíputléttöröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTípuslét.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva elem amit törölni kellene.");
                // Leellenőrizzük, hogy van-e már ilyen típus
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\Jármű.mdb";

                List<Adat_Jármű_Állomány_Típus> Adatok = Kéz_Állomány_Típus.Lista_adatok(hely);
                Adat_Jármű_Állomány_Típus rekord = (from a in Adatok
                                                    where a.Típus == txtTípuslét.Text.Trim()
                                                    select a).FirstOrDefault();
                Adat_Jármű_Állomány_Típus ADAT = new Adat_Jármű_Állomány_Típus(0, 0, txtTípuslét.Text.Trim());

                if (rekord != null)
                {
                    // ha van alatta kocsi akkor nem engedjük törölni a típust
                    if (rekord.Állomány != 0)
                    {
                        MessageBox.Show("Amíg van a típushoz rendelve jármű addig nem lehet törölni.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        Kéz_Állomány_Típus.Törlés(hely, ADAT);
                        Típusfeltöltés();
                        MessageBox.Show("Az adatok törlése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void BtnTíputlétFel_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTípuslét.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva elem amit előrébb kell tenni.");
                if (LstTípuslét.Items[0].ToStrTrim() == txtTípuslét.Text.Trim()) throw new HibásBevittAdat("Az első elemet nem lehet előrébb helyezni.");

                string előzőnév = "";
                for (int i = 0; i < LstTípuslét.Items.Count; i++)
                {
                    if (LstTípuslét.Items[i].ToStrTrim() == txtTípuslét.Text.Trim()) break;
                    előzőnév = LstTípuslét.Items[i].ToStrTrim();
                }

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\Jármű.mdb";

                List<Adat_Jármű_Állomány_Típus> Adatok = Kéz_Állomány_Típus.Lista_adatok(hely);

                Adat_Jármű_Állomány_Típus Előző = (from a in Adatok
                                                   where a.Típus == előzőnév
                                                   select a).FirstOrDefault();
                Adat_Jármű_Állomány_Típus Aktuális = (from a in Adatok
                                                      where a.Típus == txtTípuslét.Text.Trim()
                                                      select a).FirstOrDefault();
                if (Előző != null && Aktuális != null && előzőnév != txtTípuslét.Text.Trim())
                {
                    Adat_Jármű_Állomány_Típus ADAT = new Adat_Jármű_Állomány_Típus(Aktuális.Id,
                                                                                   Előző.Állomány,
                                                                                   Előző.Típus);
                    Kéz_Állomány_Típus.Módosítás(hely, ADAT);
                    ADAT = new Adat_Jármű_Állomány_Típus(Előző.Id, Aktuális.Állomány, Aktuális.Típus);
                    Kéz_Állomány_Típus.Módosítás(hely, ADAT);
                    Típusfeltöltés();
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


        #region telephely színezés
        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                // ha nem mégsemmel tér vissza a színválasztásból
                ColorDialog ColorDialog1 = new ColorDialog();
                if (ColorDialog1.ShowDialog() != DialogResult.Cancel)
                {
                    double piros = ColorDialog1.Color.R;
                    double zöld = ColorDialog1.Color.G;
                    double kék = ColorDialog1.Color.B;

                    txtszín.Text = (piros + zöld * 256d + kék * 65536d).ToString();

                    Button1.BackColor = ColorDialog1.Color;
                    Label18.BackColor = ColorDialog1.Color;
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

        private void Táblaszín_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Táblaszín.RowCount < 1) return;

                if (e.RowIndex >= 0)
                {
                    CmbTípus.Text = Táblaszín.Rows[e.RowIndex].Cells[0].Value.ToStrTrim();
                    txtszín.Text = Táblaszín.Rows[e.RowIndex].Cells[1].Value.ToStrTrim();
                    Button1.BackColor = Táblaszín.Rows[e.RowIndex].Cells[0].Style.BackColor;
                    Label18.BackColor = Táblaszín.Rows[e.RowIndex].Cells[0].Style.BackColor;
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

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (CmbTípus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva Típus így nem lehet rögzíteni.");
                if (!long.TryParse(txtszín.Text, out long szín)) szín = 0;

                // rögzítjük a színezést
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő1.mdb";

                List<Adat_Kiegészítő_Típuszínektábla> Adatok = KézTípuszínektábla.Lista_Adatok(hely);

                Adat_Kiegészítő_Típuszínektábla rekord = (from a in Adatok
                                                          where a.Típus == CmbTípus.Text.Trim()
                                                          select a).FirstOrDefault();

                Adat_Kiegészítő_Típuszínektábla ADAT = new Adat_Kiegészítő_Típuszínektábla(CmbTípus.Text.Trim(),
                                                                                           szín);
                if (rekord != null)
                    KézTípuszínektábla.Módosítás(hely, ADAT);
                else
                    KézTípuszínektábla.Rögzítés(hely, ADAT);


                Színtáblafeltöltés();
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (CmbTípus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva Típus a törléshez.");
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő1.mdb";

                List<Adat_Kiegészítő_Típuszínektábla> Adatok = KézTípuszínektábla.Lista_Adatok(hely);

                Adat_Kiegészítő_Típuszínektábla rekord = (from a in Adatok
                                                          where a.Típus == CmbTípus.Text.Trim()
                                                          select a).FirstOrDefault();
                Adat_Kiegészítő_Típuszínektábla ADAT = new Adat_Kiegészítő_Típuszínektábla(CmbTípus.Text.Trim(), 0);
                if (rekord != null)
                {
                    KézTípuszínektábla.Törlés(hely, ADAT);
                    Színtáblafeltöltés();
                    MessageBox.Show("Az adatok törlése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Színtáblafeltöltés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő1.mdb";

                List<Adat_Kiegészítő_Típuszínektábla> AdatokKiegTípSzín = KézTípuszínektábla.Lista_Adatok(hely);

                Táblaszín.Visible = false;
                Táblaszín.ColumnCount = 3;
                Táblaszín.RowCount = 0;
                // fejléc elkészítése
                Táblaszín.Columns[0].HeaderText = "típus";
                Táblaszín.Columns[0].Width = 80;
                Táblaszín.Columns[1].HeaderText = "Színszám";
                Táblaszín.Columns[1].Width = 150;
                Táblaszín.Columns[2].HeaderText = "Szín Hexa";
                Táblaszín.Columns[2].Width = 150;

                foreach (Adat_Kiegészítő_Típuszínektábla rekord in AdatokKiegTípSzín)
                {
                    Táblaszín.RowCount++;
                    int i = Táblaszín.RowCount - 1;
                    Táblaszín.Rows[i].Cells[0].Value = rekord.Típus;
                    Táblaszín.Rows[i].Cells[1].Value = rekord.Színszám;
                    Táblaszín.Rows[i].Cells[2].Value = rekord.Színszám.ToString("X");  // Hexa színszám
                    Szín_kódolás szín = MySz.Szín_váltó(rekord.Színszám);

                    Táblaszín.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(szín.Piros, szín.Zöld, szín.Kék);
                    Táblaszín.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(szín.Piros, szín.Zöld, szín.Kék);
                    Táblaszín.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(szín.Piros, szín.Zöld, szín.Kék);
                }
                Táblaszín.Visible = true;
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


        #region SAP-FORTE-T5C5
        private void Sap_Forte_T5C5()
        {
            try
            {
                SAPKiírás();
                ForteKiírás();
                Göngyölés_kiírása();
                E2_kiírása();
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

        #region T5C5 göngyölés
        private void Göngyölés_kiírása()
        {
            try
            {
                List<Adat_T5C5_Göngyöl_DátumTábla> Adatok = KézDátumTábla.Lista_Adatok();

                Adat_T5C5_Göngyöl_DátumTábla rekord = (from a in Adatok
                                                       where a.Telephely == Cmbtelephely.Text.Trim()
                                                       select a).FirstOrDefault();
                if (rekord != null)
                    Dátum_göngyölt.Value = rekord.Utolsórögzítés;
                else
                    Dátum_göngyölt.Value = new DateTime(1900, 1, 1);
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

        private void Rögzít_göngyölés_Click(object sender, EventArgs e)
        {
            try
            {
                List<Adat_T5C5_Göngyöl_DátumTábla> Adatok = KézDátumTábla.Lista_Adatok();

                Adat_T5C5_Göngyöl_DátumTábla rekord = (from a in Adatok
                                                       where a.Telephely == Cmbtelephely.Text.Trim()
                                                       select a).FirstOrDefault();

                Adat_T5C5_Göngyöl_DátumTábla ADAT = new Adat_T5C5_Göngyöl_DátumTábla(Cmbtelephely.Text.Trim(),
                                                                                     Dátum_göngyölt.Value,
                                                                                     false);

                if (rekord != null)
                    KézDátumTábla.Módosítás(ADAT);
                else
                    KézDátumTábla.Rögzítés(ADAT);

                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        #region Forte
        private void ForteKiírás()
        {
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő.mdb";
            List<Adat_Telep_Kiegészítő_Kidobó> Adatok = KézKidobó.Lista_Adatok(hely);

            Adat_Telep_Kiegészítő_Kidobó rekord = (from a in Adatok
                                                   where a.Id == 1
                                                   select a).FirstOrDefault();
            if (rekord != null) txtForte.Text = rekord.Telephely;
        }

        private void BtnForte_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtForte.Text.Trim() == "") txtForte.Text = "_";
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő.mdb";
                List<Adat_Telep_Kiegészítő_Kidobó> Adatok = KézKidobó.Lista_Adatok(hely);

                Adat_Telep_Kiegészítő_Kidobó rekord = (from a in Adatok
                                                       where a.Id == 1
                                                       select a).FirstOrDefault();

                Adat_Telep_Kiegészítő_Kidobó ADAT = new Adat_Telep_Kiegészítő_Kidobó(1,
                                                                                     txtForte.Text.Trim());

                if (rekord != null)
                    KézKidobó.Módosítás(hely, ADAT);
                else
                    KézKidobó.Rögzítés(hely, ADAT);

                Sap_Forte_T5C5();
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        #region SAP
        private void SAPKiírás()
        {
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő.mdb";

            List<Adat_Telep_Kiegészítő_SAP> AdatokSAP = KézSap.Lista_Adatok(hely);
            Adat_Telep_Kiegészítő_SAP RekordSAP = (from a in AdatokSAP
                                                   where a.Id == 1
                                                   select a).FirstOrDefault();
            if (RekordSAP != null) txtSAPmunkahely.Text = RekordSAP.Felelősmunkahely;
        }

        private void BtnSAPmunkahely_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő.mdb";

                List<Adat_Telep_Kiegészítő_SAP> AdatokSAP = KézSap.Lista_Adatok(hely);
                Adat_Telep_Kiegészítő_SAP RekordSAP = (from a in AdatokSAP
                                                       where a.Id == 1
                                                       select a).FirstOrDefault();

                Adat_Telep_Kiegészítő_SAP ADAT = new Adat_Telep_Kiegészítő_SAP(1,
                                                                               txtSAPmunkahely.Text.Trim());

                if (RekordSAP != null)
                    KézSap.Módosítás(hely, ADAT);
                else
                    KézSap.Rögzítés(hely, ADAT);

                Sap_Forte_T5C5();
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        #region T5C5 E2
        private void E2_kiírása()
        {
            try
            {

                E2_Kiírás.Checked = false;
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő1.mdb";
                List<Adat_Kiegészítő_Igen_Nem> Adatok = KézIgenNem.Lista_Adatok(hely);
                Adat_Kiegészítő_Igen_Nem Érték = (from a in Adatok
                                                  where a.Id == 1
                                                  select a).FirstOrDefault();
                if (Érték != null)
                    E2_Kiírás.Checked = Érték.Válasz;
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

        private void E2_vizsgálat_Click(object sender, EventArgs e)
        {
            try
            {
                Adat_Kiegészítő_Igen_Nem ADAT = new Adat_Kiegészítő_Igen_Nem(1,
                                                                             E2_Kiírás.Checked,
                                                                             "");

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő1.mdb";
                KézIgenNem.Módosítás(hely, ADAT);
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        #endregion


        #region Forte Típus 
        private void TáblaForteTípusfeltöltés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő.mdb";

                List<Adat_Telep_Kieg_Fortetípus> AdatokKiegFortetíp = KézKiegFortetíp.Lista_Adatok(hely);

                TáblaFroteTípus.Visible = false;
                TáblaFroteTípus.ColumnCount = 2;
                TáblaFroteTípus.RowCount = 0;
                // fejléc elkészítése
                TáblaFroteTípus.Columns[0].HeaderText = "Típus";
                TáblaFroteTípus.Columns[0].Width = 80;
                TáblaFroteTípus.Columns[1].HeaderText = "Forte Típus";
                TáblaFroteTípus.Columns[1].Width = 80;

                foreach (Adat_Telep_Kieg_Fortetípus rekord in AdatokKiegFortetíp)
                {
                    TáblaFroteTípus.RowCount++;
                    int i = TáblaFroteTípus.RowCount - 1;
                    TáblaFroteTípus.Rows[i].Cells[0].Value = rekord.Típus;
                    TáblaFroteTípus.Rows[i].Cells[1].Value = rekord.Ftípus;
                }
                TáblaFroteTípus.Visible = true;
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

        private void TáblaFroteTípus_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (TáblaFroteTípus.RowCount == 0) throw new HibásBevittAdat("Nincs kiválasztva érvényes sor.");
                if (e.RowIndex > 0)
                {
                    txtForteTípus.Text = TáblaFroteTípus.Rows[e.RowIndex].Cells[1].Value.ToStrTrim();
                    CmbForteTípus.Text = TáblaFroteTípus.Rows[e.RowIndex].Cells[0].Value.ToStrTrim();
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

        private void BtnForteTípusRögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                if (CmbForteTípus.Text.Trim() == "") throw new HibásBevittAdat("A Típus mező nem lehet üres.");
                if (txtForteTípus.Text.Trim() == "") throw new HibásBevittAdat("A Forte típus mező nem lehet üres.");
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő.mdb";

                List<Adat_Telep_Kieg_Fortetípus> AdatokKiegFortetíp = KézKiegFortetíp.Lista_Adatok(hely);

                Adat_Telep_Kieg_Fortetípus rekord = (from a in AdatokKiegFortetíp
                                                     where a.Típus == CmbForteTípus.Text.Trim() && a.Ftípus == txtForteTípus.Text.Trim()
                                                     select a).FirstOrDefault();

                Adat_Telep_Kieg_Fortetípus ADAT = new Adat_Telep_Kieg_Fortetípus(CmbForteTípus.Text.Trim(),
                                                                                 txtForteTípus.Text.Trim());

                if (rekord == null)
                {
                    KézKiegFortetíp.Rögzítés(hely, ADAT);
                    TáblaForteTípusfeltöltés();
                    MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void BtnForteTípusTörlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (CmbForteTípus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva érvényes elem a törléshez.");
                if (txtForteTípus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva érvényes elem a törléshez.");
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő.mdb";

                List<Adat_Telep_Kieg_Fortetípus> AdatokKiegFortetíp = KézKiegFortetíp.Lista_Adatok(hely);

                Adat_Telep_Kieg_Fortetípus rekord = (from a in AdatokKiegFortetíp
                                                     where a.Típus == CmbForteTípus.Text.Trim() && a.Ftípus == txtForteTípus.Text.Trim()
                                                     select a).FirstOrDefault();

                Adat_Telep_Kieg_Fortetípus ADAT = new Adat_Telep_Kieg_Fortetípus(CmbForteTípus.Text.Trim(), txtForteTípus.Text.Trim());

                if (rekord != null)
                {
                    KézKiegFortetíp.Törlés(hely, ADAT);
                    TáblaForteTípusfeltöltés();
                    MessageBox.Show("Az adatok törlése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        #region Hibaterv
        private void TáblaHibatervlistázás()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő.mdb";
                List<Adat_Kiegészítő_Hibaterv> AdatokKiegHiba = KézKiegHiba.Lista_Adatok(hely);

                TáblaHibaterv.Visible = false;
                TáblaHibaterv.ColumnCount = 3;
                TáblaHibaterv.RowCount = 0;
                // fejléc elkészítése
                TáblaHibaterv.Columns[0].HeaderText = "ID";
                TáblaHibaterv.Columns[0].Width = 30;
                TáblaHibaterv.Columns[1].HeaderText = "Hibaterv szöveg";
                TáblaHibaterv.Columns[1].Width = 250;
                TáblaHibaterv.Columns[2].HeaderText = "Hibaterv Főkönyvben";
                TáblaHibaterv.Columns[2].Width = 180;

                foreach (Adat_Kiegészítő_Hibaterv rekord in AdatokKiegHiba)
                {
                    TáblaHibaterv.RowCount++;
                    int i = TáblaHibaterv.RowCount - 1;
                    TáblaHibaterv.Rows[i].Cells[0].Value = rekord.Id;
                    TáblaHibaterv.Rows[i].Cells[1].Value = rekord.Szöveg;
                    if (rekord.Főkönyv)
                        TáblaHibaterv.Rows[i].Cells[2].Value = "Igen";
                    else
                        TáblaHibaterv.Rows[i].Cells[2].Value = "Nem";
                }
                TáblaHibaterv.Visible = true;
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

        private void TáblaHibaterv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (TáblaHibaterv.RowCount < 1) return;
                if (e.RowIndex >= 0)
                {
                    Txthibaterv.Text = TáblaHibaterv.Rows[e.RowIndex].Cells[1].Value.ToStrTrim();
                    if (TáblaHibaterv.Rows[e.RowIndex].Cells[2].Value.ToStrTrim() == "Igen")
                        Check1.Checked = true;
                    else
                        Check1.Checked = false;

                    txthibatervID.Text = TáblaHibaterv.Rows[e.RowIndex].Cells[0].Value.ToStrTrim();
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

        private void BtnHibatervRögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Txthibaterv.Text.Trim() == "") throw new HibásBevittAdat("A hibaterv szövegét ki kell tölteni.");
                if (!int.TryParse(txthibatervID.Text, out int Id)) throw new HibásBevittAdat("Az Id mező ki kell tölteni egész számmal.");

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő.mdb";
                List<Adat_Kiegészítő_Hibaterv> AdatokKiegHiba = KézKiegHiba.Lista_Adatok(hely);

                Adat_Kiegészítő_Hibaterv rekord = (from a in AdatokKiegHiba
                                                   where a.Id == Id
                                                   select a).FirstOrDefault();

                Adat_Kiegészítő_Hibaterv ADAT = new Adat_Kiegészítő_Hibaterv(Id,
                                                                             Txthibaterv.Text.Trim(),
                                                                             Check1.Checked);

                if (rekord != null)
                    KézKiegHiba.Mósosítás(hely, ADAT);
                else
                    KézKiegHiba.Rögzítés(hely, ADAT);

                TáblaHibatervlistázás();
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void BtnHibatervFel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txthibatervID.Text, out int ID)) throw new HibásBevittAdat("Nincs kiválasztva érvényes elem.");
                if (ID == 1) throw new HibásBevittAdat("Az első elemet nem lehet feljebb vinni.");

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő.mdb";

                List<Adat_Kiegészítő_Hibaterv> AdatokKiegHiba = KézKiegHiba.Lista_Adatok(hely);

                Adat_Kiegészítő_Hibaterv rekord = (from a in AdatokKiegHiba
                                                   where a.Id == ID
                                                   select a).FirstOrDefault();
                string előző = "";
                int előzőID = 0;
                for (int i = 0; i < TáblaHibaterv.Rows.Count; i++)
                {
                    if (TáblaHibaterv.Rows[i].Cells[1].Value.ToStrTrim() == Txthibaterv.Text.Trim()) break;
                    előző = TáblaHibaterv.Rows[i].Cells[1].Value.ToStrTrim();
                    előzőID = TáblaHibaterv.Rows[i].Cells[0].Value.ToÉrt_Int();
                }

                Adat_Kiegészítő_Hibaterv rekordElőző = (from a in AdatokKiegHiba
                                                        where a.Id == előzőID
                                                        select a).FirstOrDefault();

                if (rekord != null && rekordElőző != null && előzőID != ID)
                {
                    Adat_Kiegészítő_Hibaterv ADAT = new Adat_Kiegészítő_Hibaterv(rekord.Id,
                                                                                 rekordElőző.Szöveg,
                                                                                 rekordElőző.Főkönyv);
                    KézKiegHiba.Mósosítás(hely, ADAT);
                    ADAT = new Adat_Kiegészítő_Hibaterv(rekordElőző.Id,
                                                        rekord.Szöveg,
                                                        rekord.Főkönyv);
                    KézKiegHiba.Mósosítás(hely, ADAT);
                    TáblaHibatervlistázás();
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

        private void BtnHibatervTöröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txthibatervID.Text, out int Id)) throw new HibásBevittAdat("Az Id mező ki kell tölteni egész számmal.");

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő.mdb";
                List<Adat_Kiegészítő_Hibaterv> AdatokKiegHiba = KézKiegHiba.Lista_Adatok(hely);

                Adat_Kiegészítő_Hibaterv rekord = (from a in AdatokKiegHiba
                                                   where a.Id == Id
                                                   select a).FirstOrDefault();
                Adat_Kiegészítő_Hibaterv ADAT = new Adat_Kiegészítő_Hibaterv(Id, "", false);
                if (rekord != null)
                {
                    KézKiegHiba.Törlés(hely, ADAT);
                    TáblaHibatervlistázás();
                    MessageBox.Show("Az adatok törlése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void BtnHibatervÚj_Click(object sender, EventArgs e)
        {
            try
            {
                Txthibaterv.Text = "";
                Check1.Checked = false;
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő.mdb";
                List<Adat_Kiegészítő_Hibaterv> AdatokKiegHiba = KézKiegHiba.Lista_Adatok(hely);

                long i = 1;
                if (AdatokKiegHiba.Count > 0) i = AdatokKiegHiba.Max(a => a.Id) + 1;
                txthibatervID.Text = i.ToString();
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


        #region típus szűk E3
        private void ListE3Típusfeltöltés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő.mdb";

                List<Adat_Telep_Kiegészítő_E3típus> Adatok = KézE3Típus.Lista_Adatok(hely);
                ListE3Típus.Items.Clear();
                foreach (Adat_Telep_Kiegészítő_E3típus Elem in Adatok)
                    ListE3Típus.Items.Add(Elem.Típus);
                ListE3Típus.Refresh();
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

        private void ListE3Típus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ListE3Típus.SelectedIndex < 0) return;
                CmbE3típus.Text = ListE3Típus.Items[ListE3Típus.SelectedIndex].ToStrTrim();
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

        private void BtnE3TípusTöröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (CmbE3típus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva elem a törléshez.");
                if (!ListE3Típus.Items.Contains(CmbE3típus.Text.Trim())) throw new HibásBevittAdat("Nincs ilyen a listában.");

                Adat_Telep_Kiegészítő_E3típus ADAT = new Adat_Telep_Kiegészítő_E3típus(CmbE3típus.Text.Trim());

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő.mdb";

                KézE3Típus.Törlés(hely, ADAT);
                ListE3Típusfeltöltés();
                MessageBox.Show("Az adatok törlése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void BtnE3TípusOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (CmbE3típus.Text.Trim() == "") throw new HibásBevittAdat("Típus mező nem lehet üres.");
                if (ListE3Típus.Items.Contains(CmbE3típus.Text.Trim())) throw new HibásBevittAdat("Van már ilyen a listában.");

                Adat_Telep_Kiegészítő_E3típus ADAT = new Adat_Telep_Kiegészítő_E3típus(CmbE3típus.Text.Trim());
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő.mdb";
                KézE3Típus.Rögzítés(hely, ADAT);
                ListE3Típusfeltöltés();
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        #region típus szűk tak     
        private void ListTAKTípusfeltöltés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő.mdb";
                List<Adat_Telep_Kiegészítő_Takarítástípus> Adatok = KézTakarítástípus.Lista_Adatok(hely);
                ListTAK.Items.Clear();
                foreach (Adat_Telep_Kiegészítő_Takarítástípus Elem in Adatok)
                    ListTAK.Items.Add(Elem.Típus);

                ListTAK.Refresh();
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

        private void ListTAK_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ListTAK.SelectedIndex < 0) return;
                CmbTakTípus.Text = ListTAK.Items[ListTAK.SelectedIndex].ToStrTrim();
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

            if (ListTAK.Items.Count < 0)
                return;
            CmbTakTípus.Text = ListTAK.SelectedItem.ToStrTrim();
        }

        private void BtnTAKtípusTöröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (CmbTakTípus.Text.Trim() == "") throw new HibásBevittAdat("Típust ki kell választani.");
                if (!ListTAK.Items.Contains(CmbTakTípus.Text.Trim())) throw new HibásBevittAdat("Nincs ilyen a listában.");

                Adat_Telep_Kiegészítő_Takarítástípus ADAT = new Adat_Telep_Kiegészítő_Takarítástípus(CmbTakTípus.Text.Trim());

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő.mdb";

                KézTakarítástípus.Törlés(hely, ADAT);
                ListTAKTípusfeltöltés();
                MessageBox.Show("Az adatok törlése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void BtnTAKtípusOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (CmbTakTípus.Text.Trim() == "") throw new HibásBevittAdat("A típus mező nem lehet üres.");
                if (ListTAK.Items.Contains(CmbTakTípus.Text.Trim())) throw new HibásBevittAdat("Van már ilyen a listában.");

                Adat_Telep_Kiegészítő_Takarítástípus ADAT = new Adat_Telep_Kiegészítő_Takarítástípus(CmbTakTípus.Text.Trim());

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő.mdb";

                KézTakarítástípus.Rögzítés(hely, ADAT);
                ListTAKTípusfeltöltés();
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        #region Menetkimaradás
        private void Szolgálatokmenetkimaradása()
        {
            try
            {
                List<Adat_Kiegészítő_Adatok_Terjesztés> Adatok = KézTerjesztés.Lista_Adatok();

                foreach (Adat_Kiegészítő_Adatok_Terjesztés rekord in Adatok)
                {
                    switch (rekord.Id)
                    {
                        case 1:
                            txtIelérési.Text = rekord.Szöveg;
                            txtIterjesztési.Text = rekord.Email;
                            break;
                        case 2:
                            txtIIelérési.Text = rekord.Szöveg;
                            txtIIterjesztési.Text = rekord.Email;
                            break;
                        case 3:
                            txtIIIelérési.Text = rekord.Szöveg;
                            txtIIIterjesztési.Text = rekord.Email;
                            break;
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

        private void BtnIszolgálat_Click(object sender, EventArgs e)
        {
            try
            {
                Adat_Kiegészítő_Adatok_Terjesztés ADAT = new Adat_Kiegészítő_Adatok_Terjesztés(1,
                                                                                               txtIelérési.Text.Trim(),
                                                                                               txtIterjesztési.Text.Trim());

                KézTerjesztés.Módosítás(ADAT);
                // kiírjuk a módosított értékeket
                Szolgálatokmenetkimaradása();
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void BtnIIszolgálat_Click(object sender, EventArgs e)
        {
            try
            {

                Adat_Kiegészítő_Adatok_Terjesztés ADAT = new Adat_Kiegészítő_Adatok_Terjesztés(2,
                                                                                               txtIIelérési.Text.Trim(),
                                                                                               txtIIterjesztési.Text.Trim());
                KézTerjesztés.Módosítás(ADAT);
                // kiírjuk a módosított értékeket
                Szolgálatokmenetkimaradása();
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void BtnIIIszolgálat_Click(object sender, EventArgs e)
        {
            try
            {
                Adat_Kiegészítő_Adatok_Terjesztés ADAT = new Adat_Kiegészítő_Adatok_Terjesztés(3,
                                                                                               txtIIIelérési.Text.Trim(),
                                                                                               txtIIIterjesztési.Text.Trim());
                KézTerjesztés.Módosítás(ADAT);
                // kiírjuk a módosított értékeket
                Szolgálatokmenetkimaradása();
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        #region kiadási idők lapfül
        private List<Adat_Kiegészítő_Idő_Tábla> KiadásiIdőLista()
        {
            List<Adat_Kiegészítő_Idő_Tábla> Válasz = new List<Adat_Kiegészítő_Idő_Tábla>();
            try
            {
                Válasz.Clear();
                Válasz = Kéz_Idő.Lista_Adatok();
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
            return Válasz;
        }

        private void Kiadásiidőkkiírása()
        {
            try
            {
                foreach (Adat_Kiegészítő_Idő_Tábla rekord in KiadásiIdőLista())
                {
                    switch (rekord.Sorszám)
                    {
                        case 1:
                            mreggel.Value = rekord.Reggel;
                            mdélután.Value = rekord.Délután;
                            meste.Value = rekord.Este;
                            break;

                        case 2:
                            hreggel.Value = rekord.Reggel;
                            hdélután.Value = rekord.Délután;
                            heste.Value = rekord.Este;
                            break;
                    }
                }
                Idő_Korr_kiírás();
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

        private void BtnMreggel_Click(object sender, EventArgs e)
        {
            try
            {
                Adat_Kiegészítő_Idő_Tábla Elem = (from a in KiadásiIdőLista()
                                                  where a.Sorszám == 1
                                                  select a).FirstOrDefault();
                Adat_Kiegészítő_Idő_Tábla ADAT = new Adat_Kiegészítő_Idő_Tábla(1,
                                                                               mreggel.Value,
                                                                               Elem.Este,
                                                                               Elem.Délután);
                Kéz_Idő.Módosítás(ADAT);
                Kiadásiidőkkiírása();
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

        private void BtnMdélután_Click(object sender, EventArgs e)
        {
            try
            {
                Adat_Kiegészítő_Idő_Tábla Elem = (from a in KiadásiIdőLista()
                                                  where a.Sorszám == 1
                                                  select a).FirstOrDefault();
                Adat_Kiegészítő_Idő_Tábla ADAT = new Adat_Kiegészítő_Idő_Tábla(1,
                                                                               Elem.Reggel,
                                                                               Elem.Este,
                                                                               mdélután.Value);
                Kéz_Idő.Módosítás(ADAT);
                Kiadásiidőkkiírása();
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

        private void BtnMeste_Click(object sender, EventArgs e)
        {
            try
            {
                Adat_Kiegészítő_Idő_Tábla Elem = (from a in KiadásiIdőLista()
                                                  where a.Sorszám == 1
                                                  select a).FirstOrDefault();
                Adat_Kiegészítő_Idő_Tábla ADAT = new Adat_Kiegészítő_Idő_Tábla(1,
                                                                               Elem.Reggel,
                                                                               meste.Value,
                                                                               Elem.Délután);
                Kéz_Idő.Módosítás(ADAT);
                Kiadásiidőkkiírása();
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

        private void BtnHreggel_Click(object sender, EventArgs e)
        {
            try
            {
                Adat_Kiegészítő_Idő_Tábla Elem = (from a in KiadásiIdőLista()
                                                  where a.Sorszám == 2
                                                  select a).FirstOrDefault();
                Adat_Kiegészítő_Idő_Tábla ADAT = new Adat_Kiegészítő_Idő_Tábla(2,
                                                                               hreggel.Value,
                                                                               Elem.Este,
                                                                               Elem.Délután);
                Kéz_Idő.Módosítás(ADAT);
                Kiadásiidőkkiírása();
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

        private void BtnHdélután_Click(object sender, EventArgs e)
        {
            try
            {
                Adat_Kiegészítő_Idő_Tábla Elem = (from a in KiadásiIdőLista()
                                                  where a.Sorszám == 2
                                                  select a).FirstOrDefault();
                Adat_Kiegészítő_Idő_Tábla ADAT = new Adat_Kiegészítő_Idő_Tábla(2,
                                                                               Elem.Reggel,
                                                                               Elem.Este,
                                                                               hdélután.Value);
                Kéz_Idő.Módosítás(ADAT);
                Kiadásiidőkkiírása();
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

        private void BtnHeste_Click(object sender, EventArgs e)
        {
            try
            {
                Adat_Kiegészítő_Idő_Tábla Elem = (from a in KiadásiIdőLista()
                                                  where a.Sorszám == 2
                                                  select a).FirstOrDefault();
                Adat_Kiegészítő_Idő_Tábla ADAT = new Adat_Kiegészítő_Idő_Tábla(2,
                                                                               Elem.Reggel,
                                                                               heste.Value,
                                                                               Elem.Délután);
                Kéz_Idő.Módosítás(ADAT);
                Kiadásiidőkkiírása();
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


        #region Idő korrekció
        private void Idő_Korr_kiírás()
        {
            try
            {
                Idő_korr_kiadási.Text = "0";
                Idő_korr_Érkezési.Text = "0";
                List<Adat_Kiegészítő_Idő_Kor> Adatok = KézIdőKor.Lista_Adatok();
                Adat_Kiegészítő_Idő_Kor rekord = (from a in Adatok
                                                  where a.Id == 1
                                                  select a).FirstOrDefault();
                if (rekord != null)
                {
                    Idő_korr_kiadási.Text = rekord.Kiadási.ToStrTrim();
                    Idő_korr_Érkezési.Text = rekord.Érkezési.ToStrTrim();
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

        private void Korrekció_kiadási_rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Idő_korr_Érkezési.Text.Trim(), out int Érkezés)) throw new HibásBevittAdat("Kiadási idő korrekció mezőnek egész számnak kell lenni.");
                if (!int.TryParse(Idő_korr_kiadási.Text.Trim(), out int Kiadás)) throw new HibásBevittAdat("Érkezési idő korrekció mezőnek egész számnak kell lenni.");

                List<Adat_Kiegészítő_Idő_Kor> Adatok = KézIdőKor.Lista_Adatok();
                Adat_Kiegészítő_Idő_Kor rekord = (from a in Adatok
                                                  where a.Id == 1
                                                  select a).FirstOrDefault();
                Adat_Kiegészítő_Idő_Kor ADAT = new Adat_Kiegészítő_Idő_Kor(1,
                                                                           Kiadás,
                                                                           Érkezés);
                if (rekord != null)
                    KézIdőKor.Módosítás(ADAT);
                else
                    KézIdőKor.Rögzítés(ADAT);

                MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Idő_Korr_kiírás();
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


        #region Szolgálat
        private void Táblaszolgálatlistázás()
        {
            try
            {
                List<Adat_Kiegészítő_Szolgálat> AdatokKiegSzolg = KézKiegSzolg.Lista_Adatok();

                Táblaszolgálat.Visible = false;
                Táblaszolgálat.ColumnCount = 2;
                Táblaszolgálat.RowCount = 0;
                // fejléc elkészítése
                Táblaszolgálat.Columns[0].HeaderText = "Sorszám";
                Táblaszolgálat.Columns[0].Width = 80;
                Táblaszolgálat.Columns[1].HeaderText = "Szolgálat";
                Táblaszolgálat.Columns[1].Width = 150;

                foreach (Adat_Kiegészítő_Szolgálat rekord in AdatokKiegSzolg)
                {
                    Táblaszolgálat.RowCount++;
                    int i = Táblaszolgálat.RowCount - 1;
                    Táblaszolgálat.Rows[i].Cells[0].Value = rekord.Sorszám;
                    Táblaszolgálat.Rows[i].Cells[1].Value = rekord.Szolgálatnév;
                }
                Táblaszolgálat.Visible = true;
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

        private void Táblaszolgálat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Táblaszolgálat.RowCount == 0) return;
                if (e.RowIndex >= 0) txtSzolgálat.Text = Táblaszolgálat.Rows[e.RowIndex].Cells[1].Value.ToStrTrim();
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

        private void BtnSzolgálatOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSzolgálat.Text.Trim() == "") throw new HibásBevittAdat("Szolgálatnevet meg kell adni.");

                List<Adat_Kiegészítő_Szolgálat> Adatok = KézKiegSzolg.Lista_Adatok();
                int i = 1;
                if (Adatok.Count > 0) i = Adatok.Max(a => a.Sorszám) + 1;

                Adat_Kiegészítő_Szolgálat Elem = (from a in Adatok
                                                  where a.Szolgálatnév == txtSzolgálat.Text.Trim()
                                                  select a).FirstOrDefault();

                Adat_Kiegészítő_Szolgálat ADAT = new Adat_Kiegészítő_Szolgálat(i,
                                                                               txtSzolgálat.Text.Trim());
                if (Elem == null)
                {
                    KézKiegSzolg.Rögzítés(ADAT);
                    Táblaszolgálatlistázás();
                    MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void BtnSzolgálatTöröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSzolgálat.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva törlendő adat.");
                List<Adat_Kiegészítő_Szolgálat> Adatok = KézKiegSzolg.Lista_Adatok();

                Adat_Kiegészítő_Szolgálat Elem = (from a in Adatok
                                                  where a.Szolgálatnév == txtSzolgálat.Text.Trim()
                                                  select a).FirstOrDefault();

                Adat_Kiegészítő_Szolgálat ADAT = new Adat_Kiegészítő_Szolgálat(0, txtSzolgálat.Text.Trim());
                if (Elem != null)
                {
                    KézKiegSzolg.Törlés(ADAT);
                    Táblaszolgálatlistázás();
                    MessageBox.Show("Az adatok törlése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void BtnSzolgálatFel_Click(object sender, EventArgs e)
        {
            try
            {
                // megcseréljük a sort és az előző sort
                if (txtSzolgálat.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva érvényes elem.");
                List<Adat_Kiegészítő_Szolgálat> Adatok = KézKiegSzolg.Lista_Adatok();

                Adat_Kiegészítő_Szolgálat Elem = (from a in Adatok
                                                  where a.Szolgálatnév == txtSzolgálat.Text.Trim()
                                                  select a).FirstOrDefault();

                string előző = "";
                for (int i = 0; i < Táblaszolgálat.Rows.Count; i++)
                {
                    if (Táblaszolgálat.Rows[i].Cells[1].Value.ToStrTrim() == txtSzolgálat.Text.Trim()) break;
                    előző = Táblaszolgálat.Rows[i].Cells[1].Value.ToStrTrim();
                }

                Adat_Kiegészítő_Szolgálat ElőzőElem = (from a in Adatok
                                                       where a.Szolgálatnév == előző
                                                       select a).FirstOrDefault();

                if (Elem != null && ElőzőElem != null && előző != txtSzolgálat.Text.Trim())
                {
                    Adat_Kiegészítő_Szolgálat ADAT = new Adat_Kiegészítő_Szolgálat(ElőzőElem.Sorszám,
                                                                                   Elem.Szolgálatnév);
                    KézKiegSzolg.Módosítás(ADAT);
                    ADAT = new Adat_Kiegészítő_Szolgálat(Elem.Sorszám,
                                                         ElőzőElem.Szolgálatnév);
                    KézKiegSzolg.Módosítás(ADAT);

                    Táblaszolgálatlistázás();
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


        #region Főkategória
        private void LstFőkategórialistázás()
        {
            try
            {
                List<Adat_Kiegészítő_Főkategóriatábla> Adatok = KézFőkategóriatábla.Lista_Adatok();

                TáblaFőkategória.Visible = false;
                TáblaFőkategória.ColumnCount = 2;
                TáblaFőkategória.RowCount = 0;
                // fejléc elkészítése
                TáblaFőkategória.Columns[0].HeaderText = "Sorszám";
                TáblaFőkategória.Columns[0].Width = 80;
                TáblaFőkategória.Columns[1].HeaderText = "Főkategória";
                TáblaFőkategória.Columns[1].Width = 150;

                foreach (Adat_Kiegészítő_Főkategóriatábla rekord in Adatok)
                {
                    TáblaFőkategória.RowCount++;
                    int i = TáblaFőkategória.RowCount - 1;
                    TáblaFőkategória.Rows[i].Cells[0].Value = rekord.Sorszám;
                    TáblaFőkategória.Rows[i].Cells[1].Value = rekord.Főkategória;
                }
                TáblaFőkategória.Visible = true;
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

        private void BtnFőkategóriaOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFőkategória.Text.Trim() == "") throw new HibásBevittAdat("Főkategória nem lehet üres.");

                List<Adat_Kiegészítő_Főkategóriatábla> Adatok = KézFőkategóriatábla.Lista_Adatok();

                long i = 1;
                if (Adatok.Count > 0) i = Adatok.Max(a => a.Sorszám) + 1;

                Adat_Kiegészítő_Főkategóriatábla Elem = (from a in Adatok
                                                         where a.Főkategória == txtFőkategória.Text.Trim()
                                                         select a).FirstOrDefault();

                Adat_Kiegészítő_Főkategóriatábla ADAT = new Adat_Kiegészítő_Főkategóriatábla(i,
                                                                                             txtFőkategória.Text.Trim());
                if (Elem == null)
                {
                    KézFőkategóriatábla.Rögzítés(ADAT);
                    LstFőkategórialistázás();
                    MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void BtnFőkategóriaTöröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFőkategória.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva törlendő adat.");

                List<Adat_Kiegészítő_Főkategóriatábla> Adatok = KézFőkategóriatábla.Lista_Adatok();

                Adat_Kiegészítő_Főkategóriatábla Elem = (from a in Adatok
                                                         where a.Főkategória == txtFőkategória.Text.Trim()
                                                         select a).FirstOrDefault();

                Adat_Kiegészítő_Főkategóriatábla ADAT = new Adat_Kiegészítő_Főkategóriatábla(0, txtFőkategória.Text.Trim());
                if (Elem != null)
                {
                    KézFőkategóriatábla.Törlés(ADAT);
                    LstFőkategórialistázás();
                    MessageBox.Show("Az adatok törlése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void BtnFőkategóriaFel_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFőkategória.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva érvényes elem.");

                List<Adat_Kiegészítő_Főkategóriatábla> Adatok = KézFőkategóriatábla.Lista_Adatok();

                Adat_Kiegészítő_Főkategóriatábla Elem = (from a in Adatok
                                                         where a.Főkategória == txtFőkategória.Text.Trim()
                                                         select a).FirstOrDefault();
                string előző = "";
                for (int i = 0; i < TáblaFőkategória.Rows.Count; i++)
                {
                    if (TáblaFőkategória.Rows[i].Cells[1].Value.ToStrTrim() == txtFőkategória.Text.Trim()) break;
                    előző = TáblaFőkategória.Rows[i].Cells[1].Value.ToStrTrim();
                }

                Adat_Kiegészítő_Főkategóriatábla ElőzőElem = (from a in Adatok
                                                              where a.Főkategória == előző
                                                              select a).FirstOrDefault();

                if (Elem != null && ElőzőElem != null && előző != txtFőkategória.Text.Trim())
                {
                    Adat_Kiegészítő_Főkategóriatábla ADAT = new Adat_Kiegészítő_Főkategóriatábla(ElőzőElem.Sorszám,
                                                                                                 Elem.Főkategória);
                    KézFőkategóriatábla.Módosítás(ADAT);
                    ADAT = new Adat_Kiegészítő_Főkategóriatábla(Elem.Sorszám,
                                                                ElőzőElem.Főkategória);
                    KézFőkategóriatábla.Módosítás(ADAT);
                    LstFőkategórialistázás();
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

        private void TáblaFőkategória_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (TáblaFőkategória.RowCount == 0) return;
                if (e.RowIndex >= 0) txtFőkategória.Text = TáblaFőkategória.Rows[e.RowIndex].Cells[1].Value.ToStrTrim();
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


        #region Reklám lapfül
        private void LstReklámméret_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (LstReklámméret.SelectedIndex < 0) return;
                txtReklámméret.Text = LstReklámméret.SelectedItem.ToStrTrim();
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

        private void Lstreklámméretlistázás()
        {
            try
            {
                List<Adat_Kiegészítő_Reklám> Adatok = KézReklám.Lista_Adatok();

                LstReklámméret.Items.Clear();

                foreach (Adat_Kiegészítő_Reklám rekord in Adatok)
                    LstReklámméret.Items.Add(rekord.Méret);

                LstReklámméret.Refresh();
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

        private void BtnReklámméretOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtReklámméret.Text.Trim() == "") throw new HibásBevittAdat("Méret mezőnek tartalmaznia kell valamit.");
                List<Adat_Kiegészítő_Reklám> Adatok = KézReklám.Lista_Adatok();

                Adat_Kiegészítő_Reklám Elem = (from a in Adatok
                                               where a.Méret == txtReklámméret.Text.Trim()
                                               select a).FirstOrDefault();

                Adat_Kiegészítő_Reklám ADAT = new Adat_Kiegészítő_Reklám(txtReklámméret.Text.Trim());
                if (Elem == null)
                {
                    KézReklám.Rögzítés(ADAT);
                    Lstreklámméretlistázás();
                    MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void BtnReklámméretTöröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtReklámméret.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva érvényes elem.");
                List<Adat_Kiegészítő_Reklám> Adatok = KézReklám.Lista_Adatok();

                Adat_Kiegészítő_Reklám Elem = (from a in Adatok
                                               where a.Méret == txtReklámméret.Text.Trim()
                                               select a).FirstOrDefault();
                Adat_Kiegészítő_Reklám ADAT = new Adat_Kiegészítő_Reklám(txtReklámméret.Text.Trim());
                if (Elem != null)
                {
                    KézReklám.Törlés(ADAT);
                    Lstreklámméretlistázás();
                    MessageBox.Show("Az adatok törlése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Reklám_elétérés_Click(object sender, EventArgs e)
        {
            try
            {
                Adat_Kiegészítő_Igen_Nem ADAT = new Adat_Kiegészítő_Igen_Nem(2,
                                                                             Reklám_Check.Checked,
                                                                             "");
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő1.mdb";
                KézIgenNem.Módosítás(hely, ADAT);
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void LstReklámcheck()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő1.mdb";

                List<Adat_Kiegészítő_Igen_Nem> Adatok = KézIgenNem.Lista_Adatok(hely);
                Adat_Kiegészítő_Igen_Nem rekord = (from a in Adatok
                                                   where a.Id == 2
                                                   select a).FirstOrDefault();

                Adat_Kiegészítő_Igen_Nem ADAT = new Adat_Kiegészítő_Igen_Nem(2,
                                                                             false,
                                                                             "Reklámos kocsik megfelelő vonalon történő közlekedése");
                if (rekord != null)
                    Reklám_Check.Checked = rekord.Válasz;
                else
                {
                    KézIgenNem.Rögzítés(hely, ADAT);
                    Reklám_Check.Checked = false;
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


        #region FORTE Vonalak amiket nem veszünk figyelembe
        private void LstForteVonalListázás()
        {
            try
            {
                List<Adat_Kiegészítő_Forte_Vonal> Adatok = KézForte_Vonal.Lista_Adatok();
                LstForteVonal.Items.Clear();

                foreach (Adat_Kiegészítő_Forte_Vonal rekord in Adatok)
                    LstForteVonal.Items.Add(rekord.ForteVonal);

                LstForteVonal.Refresh();
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

        private void LstForteVonal_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (LstForteVonal.SelectedIndex < 0) return;
                txtForteVonal.Text = LstForteVonal.SelectedItem.ToStrTrim();
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

        private void BtnForteVonaltöröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtForteVonal.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva érvényes elem.");

                List<Adat_Kiegészítő_Forte_Vonal> Adatok = KézForte_Vonal.Lista_Adatok();

                Adat_Kiegészítő_Forte_Vonal Elem = (from a in Adatok
                                                    where a.ForteVonal == txtForteVonal.Text.Trim()
                                                    select a).FirstOrDefault();

                Adat_Kiegészítő_Forte_Vonal ADAT = new Adat_Kiegészítő_Forte_Vonal(txtForteVonal.Text.Trim());
                if (Elem != null)
                {
                    KézForte_Vonal.Törlés(ADAT);
                    LstForteVonalListázás();
                    MessageBox.Show("Az adatok törlése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void BtnForteVonalOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtForteVonal.Text.Trim() == "") throw new HibásBevittAdat("A Forte vonal nincs kitöltve.");
                List<Adat_Kiegészítő_Forte_Vonal> Adatok = KézForte_Vonal.Lista_Adatok();

                Adat_Kiegészítő_Forte_Vonal Elem = (from a in Adatok
                                                    where a.ForteVonal == txtForteVonal.Text.Trim()
                                                    select a).FirstOrDefault();
                Adat_Kiegészítő_Forte_Vonal ADAT = new Adat_Kiegészítő_Forte_Vonal(txtForteVonal.Text.Trim());
                if (Elem == null)
                {
                    KézForte_Vonal.Rögzítés(ADAT);
                    LstForteVonalListázás();
                    MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        #region TElephelyek
        private void TáblaTelephelyek_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (TáblaTelephelyek.RowCount == 0) return;
                if (e.RowIndex >= 0)
                {
                    txtTelephelyekID.Text = TáblaTelephelyek.Rows[e.RowIndex].Cells[0].Value.ToStrTrim();
                    txtTelephelyekNév.Text = TáblaTelephelyek.Rows[e.RowIndex].Cells[1].Value.ToStrTrim();
                    txtTelephelyekKönyvtár.Text = TáblaTelephelyek.Rows[e.RowIndex].Cells[2].Value.ToStrTrim();
                    txtTelephelyekForte.Text = TáblaTelephelyek.Rows[e.RowIndex].Cells[3].Value.ToStrTrim();
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

        private void BtnTelephelyekOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtTelephelyekID.Text.Trim(), out int Sorszám)) throw new HibásBevittAdat("A sorszám mező nem lehet üres és egész számnak kell lennie.");
                if (txtTelephelyekNév.Text.Trim() == "") throw new HibásBevittAdat("Telephely név mező nem lehet üres.");
                if (txtTelephelyekKönyvtár.Text.Trim() == "") throw new HibásBevittAdat("Telephely könyvtára mező nem lehet üres.");
                if (txtTelephelyekForte.Text.Trim() == "") throw new HibásBevittAdat("Forte kód mező nem lehet üres.");

                List<Adat_kiegészítő_telephely> Adatok = Kézkiegészítő_telephely.Lista_adatok();

                Adat_kiegészítő_telephely Elem = (from a in Adatok
                                                  where a.Telephelynév == txtTelephelyekNév.Text.Trim() && a.Sorszám == Sorszám
                                                  select a).FirstOrDefault();

                Adat_kiegészítő_telephely ADAT = new Adat_kiegészítő_telephely(Sorszám,
                                                                               txtTelephelyekNév.Text.Trim(),
                                                                               txtTelephelyekKönyvtár.Text.Trim(),
                                                                               txtTelephelyekForte.Text.Trim());

                if (Elem != null)
                {
                    Kézkiegészítő_telephely.Módosítás(ADAT);
                }
                else
                {
                    // új adat
                    long sorszáma = 1;
                    if (Adatok.Count > 0) sorszáma = Adatok.Max(a => a.Sorszám) + 1;
                    Kézkiegészítő_telephely.Rögzítés(ADAT);
                }

                TáblaTelephelyeklistázás();
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void BtnTelephelyekFel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!long.TryParse(txtTelephelyekID.Text.Trim(), out long sor1)) throw new HibásBevittAdat("Nincs kiválasztva érvényes elem.");

                List<Adat_kiegészítő_telephely> Adatok = Kézkiegészítő_telephely.Lista_adatok();

                Adat_kiegészítő_telephely Elem = (from a in Adatok
                                                  where a.Telephelynév == txtTelephelyekNév.Text.Trim()
                                                  select a).FirstOrDefault();

                long sor2 = 0;
                string előző = "";
                for (int i = 0; i < TáblaTelephelyek.Rows.Count; i++)
                {
                    if (TáblaTelephelyek.Rows[i].Cells[0].Value.ToÉrt_Long() == sor1) break;
                    sor2 = long.Parse(TáblaTelephelyek.Rows[i].Cells[0].Value.ToStrTrim());
                    előző = TáblaTelephelyek.Rows[i].Cells[1].Value.ToStrTrim();
                }

                Adat_kiegészítő_telephely ElőzőElem = (from a in Adatok
                                                       where a.Telephelynév == előző
                                                       select a).FirstOrDefault();

                if (ElőzőElem != null && Elem != null && előző != txtTelephelyekNév.Text.Trim())
                {
                    Kézkiegészítő_telephely.Csere(sor1, sor2);
                    TáblaTelephelyeklistázás();
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

        private void BtnTelephelyekTöröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtTelephelyekID.Text.Trim(), out int Sorszám)) throw new HibásBevittAdat("Nincs kiválasztva érvényes elem.");
                if (txtTelephelyekNév.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva érvényes elem.");

                List<Adat_kiegészítő_telephely> Adatok = Kézkiegészítő_telephely.Lista_adatok();

                Adat_kiegészítő_telephely Elem = (from a in Adatok
                                                  where a.Telephelynév == txtTelephelyekNév.Text.Trim() && a.Sorszám == Sorszám
                                                  select a).FirstOrDefault();
                Adat_kiegészítő_telephely ADAT = new Adat_kiegészítő_telephely(Sorszám,
                                                                               txtTelephelyekNév.Text.Trim(),
                                                                               txtTelephelyekKönyvtár.Text,
                                                                               txtTelephelyekForte.Text);
                if (Elem != null)
                {
                    Kézkiegészítő_telephely.Törlés(ADAT);
                    txtTelephelyekID.Text = "";
                    txtTelephelyekNév.Text = "";
                    txtTelephelyekKönyvtár.Text = "";
                    txtTelephelyekForte.Text = "";
                    TáblaTelephelyeklistázás();
                    MessageBox.Show("Az adatok törlése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void TáblaTelephelyeklistázás()
        {
            try
            {
                List<Adat_kiegészítő_telephely> Adatok = Kézkiegészítő_telephely.Lista_adatok();

                TáblaTelephelyek.Visible = false;
                TáblaTelephelyek.ColumnCount = 4;
                TáblaTelephelyek.RowCount = 0;
                // ' fejléc elkészítése
                TáblaTelephelyek.Columns[0].HeaderText = "Sorszám";
                TáblaTelephelyek.Columns[0].Width = 80;
                TáblaTelephelyek.Columns[1].HeaderText = "Telephely";
                TáblaTelephelyek.Columns[1].Width = 150;
                TáblaTelephelyek.Columns[2].HeaderText = "Telephely könyvtár";
                TáblaTelephelyek.Columns[2].Width = 180;
                TáblaTelephelyek.Columns[3].HeaderText = "Forte kód";
                TáblaTelephelyek.Columns[3].Width = 120;

                foreach (Adat_kiegészítő_telephely rekord in Adatok)
                {
                    TáblaTelephelyek.RowCount++;
                    int i = TáblaTelephelyek.RowCount - 1;
                    TáblaTelephelyek.Rows[i].Cells[0].Value = rekord.Sorszám;
                    TáblaTelephelyek.Rows[i].Cells[1].Value = rekord.Telephelynév;
                    TáblaTelephelyek.Rows[i].Cells[2].Value = rekord.Telephelykönyvtár;
                    TáblaTelephelyek.Rows[i].Cells[3].Value = rekord.Fortekód;
                }
                TáblaTelephelyek.Visible = true;
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


        #region Szakszolgálat telep
        private void TáblaSzolgtelep_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (TáblaSzolgtelep.RowCount == 0) return;
                if (e.RowIndex >= 0)
                {
                    txtSzolgtelepID.Text = TáblaSzolgtelep.Rows[e.RowIndex].Cells[0].Value.ToStrTrim();
                    CmbSzolgtelepSZOL.Text = TáblaSzolgtelep.Rows[e.RowIndex].Cells[1].Value.ToStrTrim();
                    CmbSzolgtelepTELEP.Text = TáblaSzolgtelep.Rows[e.RowIndex].Cells[2].Value.ToStrTrim();
                    txtSzolgtelepFelelősmunkahely.Text = TáblaSzolgtelep.Rows[e.RowIndex].Cells[3].Value.ToStrTrim();
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

        private void Szolgálattelephelylista()
        {
            try
            {
                CmbSzolgtelepSZOL.Items.Clear();
                CmbSzolgtelepTELEP.Items.Clear();
                List<Adat_Kiegészítő_Szolgálattelepei> Adatok = KézSzolgálattelepei.Lista_Adatok();

                TáblaSzolgtelep.Visible = false;
                TáblaSzolgtelep.ColumnCount = 4;
                TáblaSzolgtelep.RowCount = 0;
                // ' fejléc elkészítése
                TáblaSzolgtelep.Columns[0].HeaderText = "Sorszám";
                TáblaSzolgtelep.Columns[0].Width = 80;
                TáblaSzolgtelep.Columns[1].HeaderText = "Szolgálat";
                TáblaSzolgtelep.Columns[1].Width = 150;
                TáblaSzolgtelep.Columns[2].HeaderText = "Telephely";
                TáblaSzolgtelep.Columns[2].Width = 150;
                TáblaSzolgtelep.Columns[3].HeaderText = "Felelősmunkahely";
                TáblaSzolgtelep.Columns[3].Width = 150;

                foreach (Adat_Kiegészítő_Szolgálattelepei rekord in Adatok)
                {
                    TáblaSzolgtelep.RowCount++;
                    int i = TáblaSzolgtelep.RowCount - 1;
                    TáblaSzolgtelep.Rows[i].Cells[0].Value = rekord.Sorszám;
                    TáblaSzolgtelep.Rows[i].Cells[1].Value = rekord.Szolgálatnév;
                    TáblaSzolgtelep.Rows[i].Cells[2].Value = rekord.Telephelynév;
                    TáblaSzolgtelep.Rows[i].Cells[3].Value = rekord.Felelősmunkahely;
                }
                TáblaSzolgtelep.Visible = true;
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

        private void SZakSzolgálat_feltöltés()
        {
            try
            {
                List<Adat_Kiegészítő_Szolgálat> AdatokKiegSzolg = KézKiegSzolg.Lista_Adatok();

                CmbSzolgtelepSZOL.Items.Clear();
                foreach (Adat_Kiegészítő_Szolgálat rekord in AdatokKiegSzolg)
                    CmbSzolgtelepSZOL.Items.Add(rekord.Szolgálatnév);

                CmbSzolgtelepSZOL.Refresh();
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

        private void SZakSzolgálat_Telep_feltöltés()
        {
            try
            {
                List<Adat_kiegészítő_telephely> Adatok = Kézkiegészítő_telephely.Lista_adatok();

                CmbSzolgtelepTELEP.Items.Clear();

                foreach (Adat_kiegészítő_telephely rekord in Adatok)
                    CmbSzolgtelepTELEP.Items.Add(rekord.Telephelynév);

                CmbSzolgtelepTELEP.Refresh();
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

        private void BtnSzolgtelepOK_Click(object sender, EventArgs e)
        {
            try
            {
                // szolgálat- telephely összerendezés rögzítés
                if (CmbSzolgtelepTELEP.Text.Trim() == "") throw new HibásBevittAdat("A telephely nevét nem lehet üresen hagyni.");
                if (CmbSzolgtelepSZOL.Text.Trim() == "") throw new HibásBevittAdat("Szakszolgálat nevét meg kell adni.");
                if (txtSzolgtelepFelelősmunkahely.Text.Trim() == "") throw new HibásBevittAdat("Felelős munkahelyet meg kell adni.");
                if (!int.TryParse(txtSzolgtelepID.Text.Trim(), out int Sorszám)) throw new HibásBevittAdat("A sorszám mezőnek egész számot kell tartalmaznia.");

                List<Adat_Kiegészítő_Szolgálattelepei> Adatok = KézSzolgálattelepei.Lista_Adatok();

                Adat_Kiegészítő_Szolgálattelepei Elem = (from a in Adatok
                                                         where a.Telephelynév == CmbSzolgtelepTELEP.Text.Trim()
                                                         select a).FirstOrDefault();

                Adat_Kiegészítő_Szolgálattelepei ADAT = new Adat_Kiegészítő_Szolgálattelepei(Sorszám,
                                                                                             CmbSzolgtelepTELEP.Text.Trim(),
                                                                                             CmbSzolgtelepSZOL.Text.Trim(),
                                                                                             txtSzolgtelepFelelősmunkahely.Text.Trim());
                if (Elem != null)
                    KézSzolgálattelepei.Módosítás(ADAT);
                else
                    KézSzolgálattelepei.Rögzítés(ADAT);

                Szolgálattelephelylista();
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void BtnSzolgtelepFel_Click(object sender, EventArgs e)
        {
            try
            {
                if (CmbSzolgtelepTELEP.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva érvényes elem.");
                List<Adat_Kiegészítő_Szolgálattelepei> Adatok = KézSzolgálattelepei.Lista_Adatok();

                Adat_Kiegészítő_Szolgálattelepei Elem = (from a in Adatok
                                                         where a.Telephelynév == CmbSzolgtelepTELEP.Text.Trim()
                                                         select a).FirstOrDefault();
                string előző = "";
                for (int i = 0; i < TáblaSzolgtelep.Rows.Count; i++)
                {
                    if (TáblaSzolgtelep.Rows[i].Cells[2].Value.ToStrTrim() == CmbSzolgtelepTELEP.Text.Trim()) break;
                    előző = TáblaSzolgtelep.Rows[i].Cells[2].Value.ToStrTrim();
                }

                Adat_Kiegészítő_Szolgálattelepei ElőzőElem = (from a in Adatok
                                                              where a.Telephelynév == előző
                                                              select a).FirstOrDefault();

                if (ElőzőElem != null && Elem != null && előző != CmbSzolgtelepTELEP.Text.Trim())
                {
                    Adat_Kiegészítő_Szolgálattelepei ADAT = new Adat_Kiegészítő_Szolgálattelepei(Elem.Sorszám,
                                                                                                 ElőzőElem.Telephelynév,
                                                                                                 ElőzőElem.Szolgálatnév,
                                                                                                 ElőzőElem.Felelősmunkahely);
                    KézSzolgálattelepei.Módosítás(ADAT);
                    ADAT = new Adat_Kiegészítő_Szolgálattelepei(ElőzőElem.Sorszám,
                                                                Elem.Telephelynév,
                                                                Elem.Szolgálatnév,
                                                                Elem.Felelősmunkahely);
                    KézSzolgálattelepei.Módosítás(ADAT);
                    Szolgálattelephelylista();
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

        private void BtnSzolgtelepTöröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtSzolgtelepID.Text.Trim(), out int Sorszám)) throw new HibásBevittAdat("Nincs kiválasztva érvényes elem.");
                if (CmbSzolgtelepTELEP.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva érvényes elem.");
                List<Adat_Kiegészítő_Szolgálattelepei> Adatok = KézSzolgálattelepei.Lista_Adatok();

                Adat_Kiegészítő_Szolgálattelepei Elem = (from a in Adatok
                                                         where a.Telephelynév == CmbSzolgtelepTELEP.Text.Trim()
                                                         select a).FirstOrDefault();

                Adat_Kiegészítő_Szolgálattelepei ADAT = new Adat_Kiegészítő_Szolgálattelepei(Sorszám,
                                                                                             CmbSzolgtelepTELEP.Text.Trim(),
                                                                                             "",
                                                                                             "");
                if (Elem != null)
                {
                    KézSzolgálattelepei.Törlés(ADAT);
                    Szolgálattelephelylista();
                    MessageBox.Show("Az adatok törlése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        #region  Típus Altípus 
        private void Típusaltípuslista()
        {
            try
            {
                List<Adat_Kiegészítő_Típusaltípustábla> Adatok = KézTípusaltípustábla.Lista_Adatok();

                TáblaTípusAltípus.Visible = false;
                TáblaTípusAltípus.ColumnCount = 4;
                TáblaTípusAltípus.RowCount = 0;
                // ' fejléc elkészítése
                TáblaTípusAltípus.Columns[0].HeaderText = "Sorszám";
                TáblaTípusAltípus.Columns[0].Width = 80;
                TáblaTípusAltípus.Columns[1].HeaderText = "Főkategória";
                TáblaTípusAltípus.Columns[1].Width = 140;
                TáblaTípusAltípus.Columns[2].HeaderText = "Típus";
                TáblaTípusAltípus.Columns[2].Width = 140;
                TáblaTípusAltípus.Columns[3].HeaderText = "Al-Típus";
                TáblaTípusAltípus.Columns[3].Width = 140;

                foreach (Adat_Kiegészítő_Típusaltípustábla rekord in Adatok)
                {
                    TáblaTípusAltípus.RowCount++;
                    int i = TáblaTípusAltípus.RowCount - 1;
                    TáblaTípusAltípus.Rows[i].Cells[0].Value = rekord.Sorszám;
                    TáblaTípusAltípus.Rows[i].Cells[1].Value = rekord.Főkategória;
                    TáblaTípusAltípus.Rows[i].Cells[2].Value = rekord.Típus;
                    TáblaTípusAltípus.Rows[i].Cells[3].Value = rekord.AlTípus;
                }
                TáblaTípusAltípus.Visible = true;
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

        private void TáblaTípusAltípus_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (TáblaTípusAltípus.RowCount == 0) return;
                if (e.RowIndex >= 0)
                {
                    TxtTípusAltípusID.Text = TáblaTípusAltípus.Rows[e.RowIndex].Cells[0].Value.ToStrTrim();
                    CmbTípusAltípusfőkategória.Text = TáblaTípusAltípus.Rows[e.RowIndex].Cells[1].Value.ToStrTrim();
                    TxtTípusAltípusTípus.Text = TáblaTípusAltípus.Rows[e.RowIndex].Cells[2].Value.ToStrTrim();
                    TxtTípusAltípusAltípus.Text = TáblaTípusAltípus.Rows[e.RowIndex].Cells[3].Value.ToStrTrim();
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

        private void TípusAltípusKategóriabetöltés()
        {
            try
            {
                List<Adat_Kiegészítő_Főkategóriatábla> Adatok = KézFőkategóriatábla.Lista_Adatok();

                CmbTípusAltípusfőkategória.Items.Clear();

                foreach (Adat_Kiegészítő_Főkategóriatábla rekord in Adatok)
                    CmbTípusAltípusfőkategória.Items.Add(rekord.Főkategória);

                CmbTípusAltípusfőkategória.Refresh();
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

        private void BtnTípusAltípusOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (CmbTípusAltípusfőkategória.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva érvényes elem.");
                if (TxtTípusAltípusTípus.Text.Trim() == "") throw new HibásBevittAdat("Típus mező nem lehet üres.");
                if (TxtTípusAltípusAltípus.Text.Trim() == "") throw new HibásBevittAdat("Al-típus mező nem lehet üres.");

                List<Adat_Kiegészítő_Típusaltípustábla> Adatok = KézTípusaltípustábla.Lista_Adatok();

                long i = 1;
                if (Adatok.Count > 0) i = Adatok.Max(a => a.Sorszám) + 1;

                Adat_Kiegészítő_Típusaltípustábla Elem = (from a in Adatok
                                                          where a.Főkategória == CmbTípusAltípusfőkategória.Text.Trim()
                                                             && a.Típus == TxtTípusAltípusTípus.Text.Trim()
                                                             && a.AlTípus == TxtTípusAltípusAltípus.Text.Trim()
                                                          select a).FirstOrDefault();
                Adat_Kiegészítő_Típusaltípustábla ADAT = new Adat_Kiegészítő_Típusaltípustábla(i,
                                                                                               CmbTípusAltípusfőkategória.Text.Trim(),
                                                                                               TxtTípusAltípusTípus.Text.Trim(),
                                                                                               TxtTípusAltípusAltípus.Text.Trim());
                if (Elem == null)
                {
                    KézTípusaltípustábla.Rögzítés(ADAT);
                    Típusaltípuslista();
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

        private void BtnTípusAltípusTöröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (CmbTípusAltípusfőkategória.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva érvényes elem.");
                if (TxtTípusAltípusTípus.Text.Trim() == "") throw new HibásBevittAdat("Típus mező nem lehet üres.");
                if (TxtTípusAltípusAltípus.Text.Trim() == "") throw new HibásBevittAdat("Al-típus mező nem lehet üres.");

                List<Adat_Kiegészítő_Típusaltípustábla> Adatok = KézTípusaltípustábla.Lista_Adatok();

                Adat_Kiegészítő_Típusaltípustábla Elem = (from a in Adatok
                                                          where a.Főkategória == CmbTípusAltípusfőkategória.Text.Trim()
                                                             && a.Típus == TxtTípusAltípusTípus.Text.Trim()
                                                             && a.AlTípus == TxtTípusAltípusAltípus.Text.Trim()
                                                          select a).FirstOrDefault();
                Adat_Kiegészítő_Típusaltípustábla ADAT = new Adat_Kiegészítő_Típusaltípustábla(Elem.Sorszám,
                                                                                                "",
                                                                                                "",
                                                                                                "");
                if (Elem != null)
                {
                    KézTípusaltípustábla.Törlés(ADAT);
                    Típusaltípuslista();
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

        private void BtnTípusAltípusFel_Click(object sender, EventArgs e)
        {
            try
            {
                if (CmbTípusAltípusfőkategória.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva érvényes elem.");
                if (TxtTípusAltípusTípus.Text.Trim() == "") throw new HibásBevittAdat("Típus mező nem lehet üres.");
                if (TxtTípusAltípusAltípus.Text.Trim() == "") throw new HibásBevittAdat("Al-típus mező nem lehet üres.");

                List<Adat_Kiegészítő_Típusaltípustábla> Adatok = KézTípusaltípustábla.Lista_Adatok();

                Adat_Kiegészítő_Típusaltípustábla Elem = (from a in Adatok
                                                          where a.Főkategória == CmbTípusAltípusfőkategória.Text.Trim()
                                                             && a.Típus == TxtTípusAltípusTípus.Text.Trim()
                                                             && a.AlTípus == TxtTípusAltípusAltípus.Text.Trim()
                                                          select a).FirstOrDefault();
                string előző = "";
                for (int i = 0; i < TáblaTípusAltípus.Rows.Count; i++)
                {
                    if (TáblaTípusAltípus.Rows[i].Cells[3].Value.ToStrTrim() == TxtTípusAltípusAltípus.Text.Trim()) break;
                    előző = TáblaTípusAltípus.Rows[i].Cells[3].Value.ToStrTrim();
                }

                Adat_Kiegészítő_Típusaltípustábla ElőzőElem = (from a in Adatok
                                                               where a.AlTípus == előző
                                                               select a).FirstOrDefault();
                if (ElőzőElem != null && Elem != null && előző != TxtTípusAltípusTípus.Text.Trim())
                {
                    // módosítjuk a sorszámot
                    Adat_Kiegészítő_Típusaltípustábla ADAT = new Adat_Kiegészítő_Típusaltípustábla(ElőzőElem.Sorszám,
                                                                                                 Elem.Főkategória,
                                                                                                 Elem.Típus,
                                                                                                 Elem.AlTípus);
                    KézTípusaltípustábla.Módosítás(ADAT);
                    ADAT = new Adat_Kiegészítő_Típusaltípustábla(Elem.Sorszám,
                                                                 ElőzőElem.Főkategória,
                                                                 ElőzőElem.Típus,
                                                                 ElőzőElem.AlTípus);
                    KézTípusaltípustábla.Módosítás(ADAT);
                    Típusaltípuslista();
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


        #region  FőForte Típusok
        private void Fortekódlista()
        {
            try
            {
                List<Adat_Kiegészítő_Fortetípus> Adatok = KézKiegFortetípus.Lista_Adatok();

                TáblaFőforte.Visible = false;
                TáblaFőforte.ColumnCount = 4;
                TáblaFőforte.RowCount = 0;
                // ' fejléc elkészítése
                TáblaFőforte.Columns[0].HeaderText = "Sorszám";
                TáblaFőforte.Columns[0].Width = 80;
                TáblaFőforte.Columns[1].HeaderText = "Forte Típus";
                TáblaFőforte.Columns[1].Width = 140;
                TáblaFőforte.Columns[2].HeaderText = "Telephely";
                TáblaFőforte.Columns[2].Width = 140;
                TáblaFőforte.Columns[3].HeaderText = "Telephelyi Típus";
                TáblaFőforte.Columns[3].Width = 140;

                foreach (Adat_Kiegészítő_Fortetípus rekord in Adatok)
                {
                    TáblaFőforte.RowCount++;
                    int i = TáblaFőforte.RowCount - 1;
                    TáblaFőforte.Rows[i].Cells[0].Value = rekord.Sorszám;
                    TáblaFőforte.Rows[i].Cells[1].Value = rekord.Ftípus;
                    TáblaFőforte.Rows[i].Cells[2].Value = rekord.Telephely;
                    TáblaFőforte.Rows[i].Cells[3].Value = rekord.Telephelyitípus;
                }
                TáblaFőforte.Visible = true;
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

        private void TáblaFőforte_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (TáblaFőforte.RowCount == 0) return;
                if (e.RowIndex >= 0)
                {
                    TxtFőforteID.Text = TáblaFőforte.Rows[e.RowIndex].Cells[0].Value.ToStrTrim();
                    TxtFőforteForteTípus.Text = TáblaFőforte.Rows[e.RowIndex].Cells[1].Value.ToStrTrim();
                    CmbFőforteTelephely.Text = TáblaFőforte.Rows[e.RowIndex].Cells[2].Value.ToStrTrim();
                    CmbFőforteTelephelyTípus.Text = TáblaFőforte.Rows[e.RowIndex].Cells[3].Value.ToStrTrim();
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

        private void Főfortetelephelylista()
        {
            try
            {
                List<Adat_kiegészítő_telephely> Adatok = Kézkiegészítő_telephely.Lista_adatok();

                CmbFőforteTelephely.Items.Clear();

                foreach (Adat_kiegészítő_telephely rekord in Adatok)
                    CmbFőforteTelephely.Items.Add(rekord.Telephelynév);

                CmbFőforteTelephely.Refresh();
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

        private void CmbFőforteTelephely_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (CmbFőforteTelephely.Text.Trim() == "") throw new HibásBevittAdat("A Forte típus mező nem lehet üres.");

                string hely = $@"{Application.StartupPath}\{CmbFőforteTelephely.Text.Trim()}\adatok\villamos\Jármű.mdb";

                CmbFőforteTelephelyTípus.Items.Clear();

                List<Adat_Jármű_Állomány_Típus> Adatok = Kéz_Állomány_Típus.Lista_adatok(hely);
                foreach (Adat_Jármű_Állomány_Típus rekord in Adatok)
                    CmbFőforteTelephelyTípus.Items.Add(rekord.Típus);

                CmbFőforteTelephelyTípus.Refresh();
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

        private void BtnFőforteOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtFőforteForteTípus.Text.Trim() == "") throw new HibásBevittAdat("Típus mező nem lehet üres.");
                if (CmbFőforteTelephely.Text.Trim() == "") throw new HibásBevittAdat("Telephely nevét meg kell adni.");
                if (CmbFőforteTelephelyTípus.Text.Trim() == "") throw new HibásBevittAdat("Telephely típusa mezőt meg kell adni");

                List<Adat_Kiegészítő_Fortetípus> Adatok = KézKiegFortetípus.Lista_Adatok();

                long i = 1;
                if (Adatok.Count > 0) i = Adatok.Max(a => a.Sorszám) + 1;
                Adat_Kiegészítő_Fortetípus Elem = (from a in Adatok
                                                   where a.Ftípus == TxtFőforteForteTípus.Text.Trim()
                                                   && a.Telephely == CmbFőforteTelephely.Text.Trim()
                                                   && a.Telephelyitípus == CmbFőforteTelephelyTípus.Text.Trim()
                                                   select a).FirstOrDefault();


                Adat_Kiegészítő_Fortetípus ADAT = new Adat_Kiegészítő_Fortetípus(i,
                                                                    TxtFőforteForteTípus.Text.Trim(),
                                                                    CmbFőforteTelephely.Text.Trim(),
                                                                    CmbFőforteTelephelyTípus.Text.Trim());
                if (Elem == null)
                {
                    KézKiegFortetípus.Rögzítés(ADAT);
                    Fortekódlista();
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

        private void BtnFőforteTöröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtFőforteID.Text.Trim() == "" || (!int.TryParse(TxtFőforteID.Text.Trim(), out int result))) throw new HibásBevittAdat("Nincs kiválasztva érvényes elem.");

                List<Adat_Kiegészítő_Fortetípus> Adatok = KézKiegFortetípus.Lista_Adatok();

                Adat_Kiegészítő_Fortetípus Elem = (from a in Adatok
                                                   where a.Ftípus == TxtFőforteForteTípus.Text.Trim()
                                                   && a.Telephely == CmbFőforteTelephely.Text.Trim()
                                                   && a.Telephelyitípus == CmbFőforteTelephelyTípus.Text.Trim()
                                                   select a).FirstOrDefault();

                Adat_Kiegészítő_Fortetípus ADAT = new Adat_Kiegészítő_Fortetípus(Elem.Sorszám,
                                                                                  "", "", "");
                if (Elem != null)
                {
                    KézKiegFortetípus.Törlés(ADAT);
                    Fortekódlista();
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


        #region  Összekapcsolás
        private void TáblaKapcsolatAltípus()
        {
            try
            {
                List<Adat_Kiegészítő_Típusaltípustábla> Adatok = KézTípusaltípustábla.Lista_Adatok();

                TáblaKapcsolatKategória.Visible = false;
                TáblaKapcsolatKategória.ColumnCount = 4;
                TáblaKapcsolatKategória.RowCount = 0;
                // ' fejléc elkészítése
                TáblaKapcsolatKategória.Columns[0].HeaderText = "Sorszám";
                TáblaKapcsolatKategória.Columns[0].Width = 80;
                TáblaKapcsolatKategória.Columns[1].HeaderText = "Főkategória";
                TáblaKapcsolatKategória.Columns[1].Width = 140;
                TáblaKapcsolatKategória.Columns[2].HeaderText = "Típus";
                TáblaKapcsolatKategória.Columns[2].Width = 140;
                TáblaKapcsolatKategória.Columns[3].HeaderText = "Al-Típus";
                TáblaKapcsolatKategória.Columns[3].Width = 140;


                foreach (Adat_Kiegészítő_Típusaltípustábla rekord in Adatok)
                {
                    TáblaKapcsolatKategória.RowCount++;
                    int i = TáblaKapcsolatKategória.RowCount - 1;
                    TáblaKapcsolatKategória.Rows[i].Cells[0].Value = rekord.Sorszám;
                    TáblaKapcsolatKategória.Rows[i].Cells[1].Value = rekord.Főkategória;
                    TáblaKapcsolatKategória.Rows[i].Cells[2].Value = rekord.Típus;
                    TáblaKapcsolatKategória.Rows[i].Cells[3].Value = rekord.AlTípus;
                }
                TáblaKapcsolatKategória.Visible = true;
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

        private void TáblaKapcsolatkapcsoltadat()
        {
            try
            {
                List<Adat_Kiegészítő_Típusrendezéstábla> AdatokKiegTípus = KézKiegTípus.Lista_Adatok();

                DataTable AdatTábla = new DataTable();

                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Sorszám");
                AdatTábla.Columns.Add("Főkategória");
                AdatTábla.Columns.Add("Típus");
                AdatTábla.Columns.Add("Al-Típus");
                AdatTábla.Columns.Add("Telephely");
                AdatTábla.Columns.Add("Telephelyi Típus");

                AdatTábla.Clear();
                foreach (Adat_Kiegészítő_Típusrendezéstábla rekord in AdatokKiegTípus)
                {
                    DataRow Soradat = AdatTábla.NewRow();

                    Soradat["Sorszám"] = rekord.Sorszám;
                    Soradat["Főkategória"] = rekord.Főkategória;
                    Soradat["Típus"] = rekord.Típus;
                    Soradat["Al-Típus"] = rekord.AlTípus;
                    Soradat["Telephely"] = rekord.Telephely;
                    Soradat["Telephelyi Típus"] = rekord.Telephelyitípus;

                    AdatTábla.Rows.Add(Soradat);
                }
                TáblaKapcsolatKapcsolt.DataSource = AdatTábla;

                TáblaKapcsolatKapcsolt.Columns["Sorszám"].Width = 80;
                TáblaKapcsolatKapcsolt.Columns["Főkategória"].Width = 140;
                TáblaKapcsolatKapcsolt.Columns["Típus"].Width = 140;
                TáblaKapcsolatKapcsolt.Columns["Al-Típus"].Width = 140;
                TáblaKapcsolatKapcsolt.Columns["Telephely"].Width = 140;
                TáblaKapcsolatKapcsolt.Columns["Telephelyi Típus"].Width = 140;

                TáblaKapcsolatKapcsolt.Visible = true;
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

        private void KapcsolatTelephelylista()
        {
            try
            {
                List<Adat_kiegészítő_telephely> Adatok = Kézkiegészítő_telephely.Lista_adatok();

                CmbKapcsolat.Items.Clear();
                foreach (Adat_kiegészítő_telephely rekord in Adatok)
                    CmbKapcsolat.Items.Add(rekord.Telephelynév);

                CmbKapcsolat.Refresh();
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

        private void CmbKapcsolat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LstKapcsolat.Items.Clear();
                if (CmbKapcsolat.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva típus.");

                string hely = $@"{Application.StartupPath}\" + CmbKapcsolat.Text + @"\adatok\villamos\Jármű.mdb";

                List<Adat_Jármű_Állomány_Típus> AdatokJármÁll = Kéz_Állomány_Típus.Lista_adatok(hely);

                List<Adat_Kiegészítő_Típusrendezéstábla> AdatokKiegTípus = KézKiegTípus.Lista_Adatok();


                foreach (Adat_Jármű_Állomány_Típus rekord in AdatokJármÁll)
                {
                    Adat_Kiegészítő_Típusrendezéstábla Elem = (from a in AdatokKiegTípus
                                                               where a.Telephely == CmbKapcsolat.Text.Trim() && a.Telephelyitípus == rekord.Típus.ToStrTrim()
                                                               select a).FirstOrDefault();
                    if (Elem == null)
                        LstKapcsolat.Items.Add(rekord.Típus);
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

        private void BtnKapcsolatTöröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (TáblaKapcsolatKapcsolt.SelectedRows.Count != 1) throw new HibásBevittAdat("Nincs kiválasztva érvényes elem.");
                int sor = TáblaKapcsolatKapcsolt.SelectedRows[0].Index;
                int sorszám = TáblaKapcsolatKapcsolt.Rows[sor].Cells[0].Value.ToÉrt_Int();

                List<Adat_Kiegészítő_Típusrendezéstábla> AdatokKiegTípus = KézKiegTípus.Lista_Adatok();
                Adat_Kiegészítő_Típusrendezéstábla Elem = (from a in AdatokKiegTípus
                                                           where a.Sorszám == sorszám
                                                           select a).FirstOrDefault();

                Adat_Kiegészítő_Típusrendezéstábla ADAT = new Adat_Kiegészítő_Típusrendezéstábla(sorszám, "", "", "", "", "");
                if (Elem != null)
                {
                    KézKiegTípus.Törlés(ADAT);
                    TáblaKapcsolatkapcsoltadat();
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

        private void BtnKapcsolatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (TáblaKapcsolatKapcsolt.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Telephelyi_Típusok_" + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, TáblaKapcsolatKapcsolt, true);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyE.Megnyitás(fájlexc + ".xlsx");
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

        private void BtnKapcsolatOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (TáblaKapcsolatKategória.SelectedRows.Count != 1) throw new HibásBevittAdat("Nincs elem kiválasztva kategória táblázatban.");
                if (LstKapcsolat.SelectedItems.Count != 1) throw new HibásBevittAdat("Nincs elem kiválasztva Listában.");
                int sor = TáblaKapcsolatKategória.SelectedRows[0].Index;
                int sorszám = TáblaKapcsolatKategória.Rows[sor].Cells[0].Value.ToÉrt_Int();


                List<Adat_Kiegészítő_Típusrendezéstábla> AdatokKiegTípus = KézKiegTípus.Lista_Adatok();
                long i = 1;
                if (AdatokKiegTípus.Count > 0) i = AdatokKiegTípus.Max(a => a.Sorszám) + 1;

                string TelephelyTípus = LstKapcsolat.SelectedItems[0].ToStrTrim();

                Adat_Kiegészítő_Típusrendezéstábla ADAT = new Adat_Kiegészítő_Típusrendezéstábla(i,
                                                                                                 TáblaKapcsolatKategória.Rows[sor].Cells[1].Value.ToStrTrim(),
                                                                                                 TáblaKapcsolatKategória.Rows[sor].Cells[2].Value.ToStrTrim(),
                                                                                                 TáblaKapcsolatKategória.Rows[sor].Cells[3].Value.ToStrTrim(),
                                                                                                 CmbKapcsolat.Text.Trim(),
                                                                                                 TelephelyTípus);
                KézKiegTípus.Rögzítés(ADAT);

                LstKapcsolat.Items.Clear();
                TáblaKapcsolatkapcsoltadat();
                CmbKapcsolat.Text = "";
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

        private void BtnKapcsolatFel_Click(object sender, EventArgs e)
        {
            try
            {
                if (TáblaKapcsolatKapcsolt.SelectedRows.Count != 1) throw new HibásBevittAdat("Nincs kiválasztva érvényes elem.");
                int sor = TáblaKapcsolatKapcsolt.SelectedRows[0].Index;
                int sorszám = TáblaKapcsolatKapcsolt.Rows[sor].Cells[0].Value.ToÉrt_Int();

                List<Adat_Kiegészítő_Típusrendezéstábla> AdatokKiegTípus = KézKiegTípus.Lista_Adatok();

                Adat_Kiegészítő_Típusrendezéstábla Elem = (from a in AdatokKiegTípus
                                                           where a.Sorszám == sorszám
                                                           select a).FirstOrDefault();
                int előző = 0;
                for (int i = 0; i < TáblaKapcsolatKapcsolt.Rows.Count; i++)
                {
                    if (TáblaKapcsolatKapcsolt.Rows[i].Cells[0].Value.ToÉrt_Int() == sorszám) break;
                    előző = TáblaKapcsolatKapcsolt.Rows[i].Cells[0].Value.ToÉrt_Int();
                }

                Adat_Kiegészítő_Típusrendezéstábla ElőzőElem = (from a in AdatokKiegTípus
                                                                where a.Sorszám == előző
                                                                select a).FirstOrDefault();
                if (ElőzőElem != null && Elem != null && előző != sorszám)
                {
                    Adat_Kiegészítő_Típusrendezéstábla ADAT = new Adat_Kiegészítő_Típusrendezéstábla(ElőzőElem.Sorszám,
                                                                                                  Elem.Főkategória,
                                                                                                  Elem.Típus,
                                                                                                  Elem.AlTípus,
                                                                                                  Elem.Telephely,
                                                                                                  Elem.Telephelyitípus);
                    KézKiegTípus.Módosítás(ADAT);
                    ADAT = new Adat_Kiegészítő_Típusrendezéstábla(Elem.Sorszám,
                                                                  ElőzőElem.Főkategória,
                                                                  ElőzőElem.Típus,
                                                                  ElőzőElem.AlTípus,
                                                                  ElőzőElem.Telephely,
                                                                  ElőzőElem.Telephelyitípus);
                    KézKiegTípus.Módosítás(ADAT);
                    TáblaKapcsolatkapcsoltadat();
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


        #region Telephely Könyvtár
        private void Telep_Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Telep_Könyvtár.Text.Trim() == "") throw new HibásBevittAdat("A könyvtár mező nem lehet üres.");
                if (Telep_Költséghely.Text.Trim() == "") throw new HibásBevittAdat("A Költséghely mező nem lehet üres.");
                if (!int.TryParse(Telep_Csoport1.Text, out int Csoport_1)) throw new HibásBevittAdat("A csoport 1 mező nem lehet üres és számnak kell lennie.");
                if (!int.TryParse(Telep_Csoport2.Text, out int Csoport_2)) throw new HibásBevittAdat("A csoport 2 mező nem lehet üres és számnak kell lennie.");
                if (!int.TryParse(Telep_Sorrend1.Text, out int Sorrend_1)) throw new HibásBevittAdat("A Sorrend 1 mező nem lehet üres és számnak kell lennie.");
                if (!int.TryParse(Telep_Sorrend2.Text, out int Sorrend_2)) throw new HibásBevittAdat("A Sorrend 2 mező nem lehet üres és számnak kell lennie.");
                if (!int.TryParse(Telep_sorszám.Text, out int sorszám)) throw new HibásBevittAdat("A Sorszám mező nem lehet üres és számnak kell lennie.");

                Telep_Könyvtár.Text = MyF.Szöveg_Tisztítás(Telep_Könyvtár.Text);
                Telep_Költséghely.Text = MyF.Szöveg_Tisztítás(Telep_Költséghely.Text);

                List<Adat_Kiegészítő_Sérülés> Adatok = KézSérülés.Lista_Adatok();

                Adat_Kiegészítő_Sérülés Elem = (from a in Adatok
                                                where a.ID == sorszám
                                                select a).FirstOrDefault();

                Adat_Kiegészítő_Sérülés ADAT = new Adat_Kiegészítő_Sérülés(sorszám,
                                                                           Telep_Könyvtár.Text.Trim(),
                                                                           Vezér1.Checked,
                                                                           Csoport_1,
                                                                           Csoport_2,
                                                                           Vezér2.Checked,
                                                                           Sorrend_1,
                                                                           Sorrend_2,
                                                                           Telep_Költséghely.Text.Trim());
                if (Elem == null)
                    KézSérülés.Rögzítés(ADAT);
                else
                    KézSérülés.Módosítás(ADAT);

                Telep_Tábla_kiirás();
                Könytár_tisztít();
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

        private void Telep_Töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Telep_sorszám.Text.Trim(), out int sorszám)) throw new HibásBevittAdat("Nincs kiválasztva érvényes adat törlésre.");

                Adat_Kiegészítő_Sérülés ADAT = new Adat_Kiegészítő_Sérülés(sorszám, "", false, 0, 0, false, 0, 0, "");
                KézSérülés.Törlés(ADAT);

                Telep_Tábla_kiirás();
                Könytár_tisztít();
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

        private void Telep_Új_Click(object sender, EventArgs e)
        {
            Könytár_tisztít();
        }

        private void Könytár_tisztít()
        {
            try
            {
                Telep_sorszám.Text = "0";
                Telep_Csoport1.Text = "0";
                Telep_Csoport2.Text = "0";
                Telep_Könyvtár.Text = "";
                Vezér1.Checked = false;
                Vezér2.Checked = false;
                Telep_Sorrend2.Text = "0";
                Telep_Sorrend1.Text = "0";
                Telep_Költséghely.Text = "";
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

        private void Telep_Frissít_Click(object sender, EventArgs e)
        {
            Telep_Tábla_kiirás();
        }

        private void Telep_Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                Telep_sorszám.Text = Telep_Tábla.Rows[e.RowIndex].Cells[0].Value.ToString();
                if (!int.TryParse(Telep_sorszám.Text, out int sorszám)) return;
                Kiírja_Sorszámot(sorszám);
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

        private void Kiírja_Sorszámot(int sorszám)
        {
            try
            {
                List<Adat_Kiegészítő_Sérülés> Adatok = KézSérülés.Lista_Adatok();
                Adat_Kiegészítő_Sérülés Rekord = (from a in Adatok
                                                  where a.ID == sorszám
                                                  select a).FirstOrDefault();

                // Táblázat sorának kijelölése
                if (Rekord != null)
                {
                    Telep_sorszám.Text = Rekord.ID.ToString();
                    Telep_Könyvtár.Text = Rekord.Név.Trim();
                    Telep_Csoport1.Text = Rekord.Csoport1.ToString();
                    if (Rekord.Vezér1)
                        Vezér1.Checked = true;
                    else
                        Vezér1.Checked = false;
                    Telep_Sorrend1.Text = Rekord.Sorrend1.ToString();
                    Telep_Csoport2.Text = Rekord.Csoport2.ToString();
                    if (Rekord.Vezér2)
                        Vezér2.Checked = true;
                    else
                        Vezér2.Checked = false;
                    Telep_Sorrend2.Text = Rekord.Sorrend2.ToString();
                    Telep_Költséghely.Text = Rekord.Költséghely.Trim();
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

        private void Telep_Tábla_kiirás()
        {
            try
            {
                Telep_Tábla.Rows.Clear();
                Telep_Tábla.Columns.Clear();
                Telep_Tábla.Refresh();
                Telep_Tábla.Visible = false;
                Telep_Tábla.ColumnCount = 9;

                // fejléc elkészítése
                Telep_Tábla.Columns[0].HeaderText = "Id";
                Telep_Tábla.Columns[0].Width = 80;
                Telep_Tábla.Columns[1].HeaderText = "Könyvtár";
                Telep_Tábla.Columns[1].Width = 200;
                Telep_Tábla.Columns[2].HeaderText = "csoport1";
                Telep_Tábla.Columns[2].Width = 120;
                Telep_Tábla.Columns[3].HeaderText = "Vezér1";
                Telep_Tábla.Columns[3].Width = 120;
                Telep_Tábla.Columns[4].HeaderText = "Sorrend1";
                Telep_Tábla.Columns[4].Width = 120;
                Telep_Tábla.Columns[5].HeaderText = "csoport2";
                Telep_Tábla.Columns[5].Width = 120;
                Telep_Tábla.Columns[6].HeaderText = "Vezér2";
                Telep_Tábla.Columns[6].Width = 120;
                Telep_Tábla.Columns[7].HeaderText = "Sorrend2";
                Telep_Tábla.Columns[7].Width = 120;
                Telep_Tábla.Columns[8].HeaderText = "Költséghely";
                Telep_Tábla.Columns[8].Width = 120;

                List<Adat_Kiegészítő_Sérülés> Adatok = KézSérülés.Lista_Adatok();

                foreach (Adat_Kiegészítő_Sérülés rekord in Adatok)
                {
                    Telep_Tábla.RowCount++;
                    int i = Telep_Tábla.RowCount - 1;
                    Telep_Tábla.Rows[i].Cells[0].Value = rekord.ID;
                    Telep_Tábla.Rows[i].Cells[1].Value = rekord.Név;
                    Telep_Tábla.Rows[i].Cells[2].Value = rekord.Csoport1;
                    if (rekord.Vezér1)
                        Telep_Tábla.Rows[i].Cells[3].Value = "IGAZ";
                    else
                        Telep_Tábla.Rows[i].Cells[3].Value = "HAMIS";

                    Telep_Tábla.Rows[i].Cells[4].Value = rekord.Sorrend1;
                    Telep_Tábla.Rows[i].Cells[5].Value = rekord.Csoport2;
                    if (rekord.Vezér2)
                        Telep_Tábla.Rows[i].Cells[6].Value = "IGAZ";
                    else
                        Telep_Tábla.Rows[i].Cells[6].Value = "HAMIS";

                    Telep_Tábla.Rows[i].Cells[7].Value = rekord.Sorrend2;
                    Telep_Tábla.Rows[i].Cells[8].Value = rekord.Költséghely.Trim();
                }
                Telep_Tábla.Visible = true;
                Telep_Tábla.Refresh();
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