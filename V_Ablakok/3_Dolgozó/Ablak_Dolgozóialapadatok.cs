using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;


namespace Villamos
{
    public partial class Ablak_Dolgozóialapadatok
    {
        #region Kezelők és Listák
        readonly Kezelő_Oktatás_Napló KézOktatásNapló = new Kezelő_Oktatás_Napló();
        readonly Kezelő_Kiegészítő_Jogtípus KézKiegTípus = new Kezelő_Kiegészítő_Jogtípus();
        readonly Kezelő_Dolgozó_Alap KézDolgozó = new Kezelő_Dolgozó_Alap();
        readonly Kezelő_Kiegészítő_Munkakör KézMunkakör = new Kezelő_Kiegészítő_Munkakör();
        readonly Kezelő_Munkakör Kéz_Munkakör = new Kezelő_Munkakör();
        readonly Kezelő_JogosítványTípus Kéz_JogosítványTípus = new Kezelő_JogosítványTípus();
        readonly Kezelő_JogosítványVonal Kéz_JogosítványVonal = new Kezelő_JogosítványVonal();
        readonly Kezelő_Kiegészítő_Feorszámok Kéz = new Kezelő_Kiegészítő_Feorszámok();
        readonly Kezelő_Kulcs KézKulcs = new Kezelő_Kulcs();
        readonly Kezelő_Kulcs_Kettő KézKulcsKettő = new Kezelő_Kulcs_Kettő();
        readonly Kezelő_Dolgozó_Személyes KézSzemélyes = new Kezelő_Dolgozó_Személyes();
        readonly Kezelő_Szatube_Túlóra KézTúlóra = new Kezelő_Szatube_Túlóra();
        readonly Kezelő_Kiegészítő_Turnusok KézTurnusok = new Kezelő_Kiegészítő_Turnusok();
        readonly Kezelő_Belépés_Bejelentkezés KézBejelentkezés = new Kezelő_Belépés_Bejelentkezés();
        readonly Kezelő_Kiegészítő_Csoportbeosztás KézCsoportbeo = new Kezelő_Kiegészítő_Csoportbeosztás();
        readonly Kezelő_Kiegészítő_JogVonal KézKiegVonal = new Kezelő_Kiegészítő_JogVonal();
        readonly Kezelő_OktatásTábla KézOktatás = new Kezelő_OktatásTábla();

        List<Adat_Dolgozó_Alap> DolgozóAdatok = new List<Adat_Dolgozó_Alap>();
        Adat_Dolgozó_Alap EgyDolgozó = null;
        List<Adat_JogosítványTípus> Adatok_JogostivanyTipus = new List<Adat_JogosítványTípus>();
        List<Adat_JogosítványVonal> Adatok_JogosítványVonal = new List<Adat_JogosítványVonal>();
        List<Adat_Kiegészítő_Munkakör> AdatokMunkakör = new List<Adat_Kiegészítő_Munkakör>();
        List<Adat_Kiegészítő_Feorszámok> AdatokFeor = new List<Adat_Kiegészítő_Feorszámok>();
        List<Adat_Kulcs> AdatokKulcs = new List<Adat_Kulcs>();
        #endregion


        #region Alap
        public Ablak_Dolgozóialapadatok()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            try
            {
                //Ha az első karakter "R" akkor az új jogosultságkiosztást használjuk
                //ha nem akkor a régit használjuk
                if (Program.PostásJogkör.Substring(0, 1) == "R")
                {
                    TelephelyekFeltöltéseÚj();
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
                }
                else
                {
                    Telephelyekfeltöltése();
                    Jogosultságkiosztás();
                }

                DolgozóAdatok = KézDolgozó.Lista_Adatok(Cmbtelephely.Text.Trim());
                Névfeltöltés();
                Fülek.SelectedIndex = 0;
                Fülekkitöltése();
                Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;
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

        private void AblakDolgozóialapadatok_Load(object sender, EventArgs e)
        { }

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Személy(true))
                    Cmbtelephely.Items.Add(Elem);
                if (Program.PostásTelephely == "Főmérnökség")
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

        private void TelephelyekFeltöltéseÚj()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Adat in GombLathatosagKezelo.Telephelyek(this.Name))
                    Cmbtelephely.Items.Add(Adat.Trim());
                //Alapkönyvtárat beállítjuk 
                if (Cmbtelephely.Items.Cast<string>().Contains(Program.PostásTelephely))
                    Cmbtelephely.Text = Program.PostásTelephely;
                else
                    Cmbtelephely.Text = Cmbtelephely.Items[0].ToStrTrim();
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
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\Dolgozó.html";
                MyF.Megnyitás(hely);
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

                // ide kell az összes gombot tenni amit szabályozni akarunk false
                Button4.Enabled = false;

                Jogosítványmódosít.Enabled = false;
                Típusrögzítés.Enabled = false;
                Típustörlés.Enabled = false;
                Jogterületrögzítés.Enabled = false;
                Jogterülettörlés.Enabled = false;

                Munkakörmódosít.Enabled = false;
                BtnPDFsave.Enabled = false;
                Munkakör_Töröl.Enabled = false;

                Bérrögzítés.Enabled = false;

                Button2.Enabled = false;

                //Csak vezérből rögzíthető
                Button2.Visible = Program.Postás_Vezér;
                Bérrögzítés.Visible = Program.Postás_Vezér;

                melyikelem = 66;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Button4.Enabled = true;
                }
                // módosítás 2 
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    Jogosítványmódosít.Enabled = true;
                    Típusrögzítés.Enabled = true;
                    Típustörlés.Enabled = true;
                    Jogterületrögzítés.Enabled = true;
                    Jogterülettörlés.Enabled = true;
                }
                // módosítás 3
                if (MyF.Vanjoga(melyikelem, 3))
                {
                    Munkakörmódosít.Enabled = true;
                    BtnPDFsave.Enabled = true;
                    Munkakör_Töröl.Enabled = true;
                }


                melyikelem = 67;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Bérrögzítés.Enabled = true;
                }
                // módosítás 2 
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    Button2.Enabled = true;
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

        private void Fülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }

        private void Fülekkitöltése()
        {
            switch (Fülek.SelectedIndex)
            {
                case 0:
                    {
                        // csoport adatok
                        Csoportfeltöltés();
                        VáltóCsoportfeltöltés();
                        Felhasználónévfeltöltés();
                        Csoportürítés();
                        Csoportadatokkiírása();
                        break;
                    }
                case 1:
                    {
                        // jogosítvány és típus
                        Típusfeltöltés();
                        Kiürítijogosítvány();
                        Kiürítijogosítványtípus();
                        Jogtípusfeltöltése();
                        Jogosítványtípustábla();
                        break;
                    }
                case 2:
                    {
                        // jogosítvány és vonal
                        Vonalfeltöltés();
                        Kiürítijogosítványvonal();
                        Jogosítványvonaltábla();
                        break;
                    }

                case 3:
                    {
                        // munnkakör
                        CsoportCombofeltöltés();
                        Munkakörfeltöltés();

                        Üritimunkakört();

                        Munkakör_kiírás();
                        Munkakörlistázás();
                        break;
                    }
                case 4:
                    {
                        // Oktatások
                        Tárgyfeltöltés();
                        break;
                    }
                case 5:
                    {
                        break;
                    }
                // pdf megjelenítő      
                case 6:
                    {
                        // személyes adatok
                        Panel4.Visible = false;
                        Láthatszemélyes();
                        Ürítiaszemélyest();
                        Kiírja_személyes();
                        break;
                    }

                case 7:
                    {
                        // Bér adatok
                        Panel5.Visible = false;
                        LáthatBér();
                        Kiirja_bért();
                        break;
                    }
                case 8:
                    {
                        // túlóra engedély
                        Panel6.Visible = false;
                        Láthattúlóra();
                        Túlóra_kiírása();
                        Túlóraellenőrzés();
                        break;
                    }
            }
        }

        private void Fülek_DrawItem(object sender, DrawItemEventArgs e)
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
        #endregion


        #region Névválasztás
        private void Névfeltöltés()
        {
            try
            {
                DolgozóAdatok = KézDolgozó.Lista_Adatok(Cmbtelephely.Text.Trim());
                ChkDolgozónév.Items.Clear();
                List<Adat_Dolgozó_Alap> Adatok;

                if (Kilépettjel.Checked)
                    Adatok = DolgozóAdatok;
                else
                    Adatok = (from a in DolgozóAdatok
                              where a.Kilépésiidő == new DateTime(1900, 1, 1)
                              select a).ToList();

                foreach (Adat_Dolgozó_Alap rekord in Adatok)
                    ChkDolgozónév.Items.Add(rekord.DolgozóNév.Trim() + " = " + rekord.Dolgozószám.Trim());

                ChkDolgozónév.Refresh();
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

        private void ChkDolgozónév_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((ChkDolgozónév.Text) == "") return;
                ÚjraKiír();
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
        #endregion


        #region Csoport adatok
        private void Csoportfeltöltés()
        {
            try
            {
                Csoport.BeginUpdate();
                Csoport.Items.Clear();
                List<Adat_Kiegészítő_Csoportbeosztás> Adatok = KézCsoportbeo.Lista_Adatok(Cmbtelephely.Text.Trim()).OrderBy(a => a.Csoportbeosztás).ToList();

                foreach (Adat_Kiegészítő_Csoportbeosztás rekord in Adatok)
                    Csoport.Items.Add(rekord.Csoportbeosztás.Trim());

                Csoport.EndUpdate();
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

        private void VáltóCsoportfeltöltés()
        {
            try
            {
                Váltóscsoport.BeginUpdate();
                Váltóscsoport.Items.Clear();
                Váltóscsoport.Items.Add("");
                List<Adat_Kiegészítő_Turnusok> Adatok = KézTurnusok.Lista_Adatok();

                foreach (Adat_Kiegészítő_Turnusok rekord in Adatok)
                    Váltóscsoport.Items.Add(rekord.Csoport.Trim());

                Váltóscsoport.EndUpdate();
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

        private void Felhasználónévfeltöltés()
        {
            try
            {
                Felhasználóinév.BeginUpdate();
                Felhasználóinév.Items.Clear();
                List<Adat_Belépés_Bejelentkezés> Adatok = KézBejelentkezés.Lista_Adatok(Cmbtelephely.Text.Trim());

                foreach (Adat_Belépés_Bejelentkezés rekord in Adatok)
                    Felhasználóinév.Items.Add(rekord.Név.Trim());

                Felhasználóinév.EndUpdate();
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

        private void Csoportürítés()
        {
            Csopvez.Checked = false;
            Csoport.Text = "";
            Váltóscsoport.Text = "";
            Passzív.Checked = false;
            Vezényelt.Checked = false;
            Vezényelve.Checked = false;
            Hovavez.Text = "";
            Honnanvez.Text = "";
            Részmunkaidős.Checked = false;
            Nyugdíjas.Checked = false;
            Állományonkívül.Checked = false;
            Szünidős.Checked = false;
            Eltérőmunkarend.Checked = false;
            Részmunkaidőperc.Text = "0";
            Felhasználóinév.Text = "";
            Főkönyvititulus.Text = "";
            Belépésiidő.Value = new DateTime(1900, 1, 1);
            Kilépésiidő.Value = new DateTime(1900, 1, 1);
        }

        private void Csoportadatokkiírása()
        {
            try
            {
                if ((Dolgozószám.Text) == "") return;

                // Csoport
                Csoport.Text = EgyDolgozó.Csoport;
                Főkönyvititulus.Text = EgyDolgozó.Főkönyvtitulus;
                Csopvez.Checked = EgyDolgozó.Csopvez;

                if (EgyDolgozó.Munkarend)
                    Óra8.Checked = true;
                else
                    Óra12.Checked = true;
                Passzív.Checked = EgyDolgozó.Passzív;

                if (EgyDolgozó.Vezényelt)
                {
                    Vezényelt.Checked = true;
                    Hovavez.Text = EgyDolgozó.Lakcím;
                }
                else
                    Vezényelt.Checked = false;

                if (EgyDolgozó.Vezényelve)
                {
                    Vezényelve.Checked = true;
                    Honnanvez.Text = EgyDolgozó.Lakcím;
                }
                else
                    Vezényelve.Checked = false;

                Részmunkaidős.Checked = EgyDolgozó.Részmunkaidős;

                if ((EgyDolgozó.Bejelentkezésinév) == "")
                    Felhasználóinév.Text = "";
                else
                    Felhasználóinév.Text = EgyDolgozó.Bejelentkezésinév.Trim();

                if (EgyDolgozó.Alkalmazott)
                    Alkalmazott.Checked = true;
                else
                    Fizikai.Checked = true;

                Belépésiidő.Value = EgyDolgozó.Belépésiidő;
                Kilépésiidő.Value = EgyDolgozó.Kilépésiidő;

                if ((EgyDolgozó.TAj) != "")
                {
                    if (EgyDolgozó.TAj.Substring(0, 1) == "N")
                        Nyugdíjas.Checked = true;
                    else
                        Nyugdíjas.Checked = false;
                    if (EgyDolgozó.TAj.Substring(1, 1) == "Á")
                        Állományonkívül.Checked = true;
                    else
                        Állományonkívül.Checked = false;
                    if (EgyDolgozó.TAj.Substring(2, 1) == "S")
                        Szünidős.Checked = true;
                    else
                        Szünidős.Checked = false;
                    if (EgyDolgozó.TAj.Substring(3, 1) == "E")
                        Eltérőmunkarend.Checked = true;
                    else
                        Eltérőmunkarend.Checked = false;
                }
                if (EgyDolgozó.Csoportkód.Trim() == "_")
                    Váltóscsoport.Text = "";
                else
                    Váltóscsoport.Text = EgyDolgozó.Csoportkód;

                Részmunkaidőperc.Text = EgyDolgozó.Részmunkaidőperc.ToString();
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

        private void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Dolgozószám.Text) == "") return;
                string ideig;
                if (Nyugdíjas.Checked)
                    ideig = "N";
                else
                    ideig = "0";
                if (Állományonkívül.Checked)
                    ideig += "Á";
                else
                    ideig += "0";
                if (Szünidős.Checked)
                    ideig += "S";
                else
                    ideig += "0";
                if (Eltérőmunkarend.Checked)
                    ideig += "E";
                else
                    ideig += "0";
                if (!int.TryParse(Részmunkaidőperc.Text.Trim(), out int részmunkaidőperc)) részmunkaidőperc = 0;
                Részmunkaidőperc.Text = részmunkaidőperc.ToString();

                Adat_Dolgozó_Alap ADAT = new Adat_Dolgozó_Alap(Dolgozószám.Text.Trim(),
                                                             Csoport.Text.Trim(),
                                                             Főkönyvititulus.Text.Trim(),
                                                             Felhasználóinév.Text.Trim(),
                                                             Csopvez.Checked,
                                                             Óra8.Checked,
                                                             Passzív.Checked,
                                                             Részmunkaidős.Checked,
                                                             Alkalmazott.Checked,
                                                             ideig,
                                                             Váltóscsoport.Text.Trim(),
                                                             részmunkaidőperc);
                KézDolgozó.Módosít_Alap(Cmbtelephely.Text.Trim(), ADAT);
                DolgozóAdatok = KézDolgozó.Lista_Adatok(Cmbtelephely.Text.Trim());
                ÚjraKiír();
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
        #endregion


        #region Jogosítvány és típus
        private void Típusfeltöltés()
        {
            try
            {
                Jogtípus.BeginUpdate();
                Jogtípus.Items.Clear();

                List<Adat_Kiegészítő_Jogtípus> Adatok = KézKiegTípus.Lista_Adatok().OrderBy(a => a.Típus).ToList();

                foreach (Adat_Kiegészítő_Jogtípus rekord in Adatok)
                    Jogtípus.Items.Add(rekord.Típus.Trim());

                Jogtípus.EndUpdate();
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

        private void Kiürítijogosítvány()
        {
            Jogosítványszám.Text = "";
            Jogtanusítvány.Text = "";
            Jogosítványidő.Value = new DateTime(1900, 1, 1);
            Jogorvosi.Value = new DateTime(1900, 1, 1);
        }

        private void Kiürítijogosítványtípus()
        {
            Jogtípus.Text = "";
            Jogtípusmegszerzés.Value = new DateTime(1900, 1, 1);
            Jogtípusérvényes.Value = new DateTime(1900, 1, 1);
        }

        private void Jogtípusfeltöltése()
        {
            try
            {
                if ((Dolgozószám.Text) == "") return;

                Jogosítványszám.Text = EgyDolgozó.Jogosítványszám.Trim();
                Jogtanusítvány.Text = EgyDolgozó.Jogtanúsítvány.Trim();
                Jogosítványidő.Value = EgyDolgozó.Jogosítványérvényesség;
                Jogorvosi.Value = EgyDolgozó.Jogorvosi;
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

        private void Jogosítványtípustábla()
        {
            try
            {
                List<Adat_JogosítványTípus> AdatokÖ = Kéz_JogosítványTípus.Lista_Adatok();
                List<Adat_JogosítványTípus> Adatok = (from a in AdatokÖ
                                                      where a.Törzsszám == Dolgozószám.Text.Trim()
                                                      && a.Státus == false
                                                      select a).ToList();

                if (Adatok == null && Adatok.Count < 1) return;

                DataTable AdatTábla = new DataTable();

                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Sorszám");
                AdatTábla.Columns.Add("Megszerzés");
                AdatTábla.Columns.Add("Típus");
                AdatTábla.Columns.Add("Érvényesség");

                AdatTábla.Clear();
                foreach (Adat_JogosítványTípus rekord in Adatok)
                {
                    DataRow Soradat = AdatTábla.NewRow();

                    Soradat["Sorszám"] = rekord.Sorszám;
                    Soradat["Megszerzés"] = rekord.Jogtípusmegszerzés.ToShortDateString();
                    Soradat["Típus"] = rekord.Jogtípus;
                    Soradat["Érvényesség"] = rekord.Jogtípusérvényes.ToShortDateString();

                    AdatTábla.Rows.Add(Soradat);
                }
                Tábla.CleanFilterAndSort();
                Tábla.DataSource = AdatTábla;

                Tábla.Columns["Sorszám"].Width = 100;
                Tábla.Columns["Megszerzés"].Width = 120;
                Tábla.Columns["Típus"].Width = 500;
                Tábla.Columns["Érvényesség"].Width = 120;

                Tábla.Refresh();
                Tábla.ClearSelection();
                Tábla.Visible = true;
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

        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    Jogtípusmegszerzés.Value = DateTime.Parse(Tábla.Rows[e.RowIndex].Cells[1].Value.ToString());
                    Jogtípusérvényes.Value = DateTime.Parse(Tábla.Rows[e.RowIndex].Cells[3].Value.ToString());
                    Jogtípus.Text = Tábla.Rows[e.RowIndex].Cells[2].Value.ToString();
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

        private void Jogosítványmódosít_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Dolgozószám.Text) == "") throw new HibásBevittAdat("Nincs megadva a dolgozószám.");
                if (Jogosítványszám.Text.Trim() != "")
                    if (Jogtanusítvány.Text.Trim() == "") throw new HibásBevittAdat("A tanusítvány számát meg kell adni.");

                Adat_Dolgozó_Alap ADAT = new Adat_Dolgozó_Alap(Dolgozószám.Text.Trim(),
                                                               Jogosítványszám.Text.Trim(),
                                                               Jogtanusítvány.Text.Trim(),
                                                               Jogosítványidő.Value,
                                                               Jogorvosi.Value);
                KézDolgozó.Módosít_Jog(Cmbtelephely.Text.Trim(), ADAT);
                DolgozóAdatok = KézDolgozó.Lista_Adatok(Cmbtelephely.Text.Trim());
                ÚjraKiír();
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

        private void Típusrögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Jogtípus.Text) == "") throw new HibásBevittAdat("A jogosítvány típust meg kell adni.");
                if ((Dolgozószám.Text) == "") throw new HibásBevittAdat("A dolgozót ki kell választani.");

                List<Adat_JogosítványTípus> Adatok = Kéz_JogosítványTípus.Lista_Adatok();

                Adat_JogosítványTípus vane = (from a in Adatok
                                              where a.Törzsszám == Dolgozószám.Text.Trim()
                                              && a.Jogtípus == Jogtípus.Text.Trim()
                                              && a.Státus == false
                                              select a).FirstOrDefault();

                if (vane != null)
                {
                    // módosít
                    Adat_JogosítványTípus ADAT = new Adat_JogosítványTípus(vane.Sorszám,
                                                                           Dolgozószám.Text.Trim(),
                                                                           Jogtípus.Text.Trim(),
                                                                           Jogtípusérvényes.Value,
                                                                           Jogtípusmegszerzés.Value,
                                                                           false);
                    Kéz_JogosítványTípus.Módosítás(ADAT);
                }
                else
                {
                    // új adat
                    Adat_JogosítványTípus ADAT = new Adat_JogosítványTípus(0,
                                                                  Dolgozószám.Text.Trim(),
                                                                  Jogtípus.Text.Trim(),
                                                                  Jogtípusérvényes.Value,
                                                                  Jogtípusmegszerzés.Value,
                                                                  false);
                    Kéz_JogosítványTípus.Rögzítés(ADAT);
                }
                MessageBox.Show("Az adatok rögzítése megtörtént. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Jogosítványtípustábla();
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

        private void Típustörlés_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Jogtípus.Text) == "") throw new HibásBevittAdat("A jogosítvány típust meg kell adni.");
                if ((Dolgozószám.Text) == "") throw new HibásBevittAdat("A dolgozót ki kell választani.");

                Adatok_JogostivanyTipus = Kéz_JogosítványTípus.Lista_Adatok();

                Adat_JogosítványTípus vane = (from a in Adatok_JogostivanyTipus
                                              where a.Törzsszám == Dolgozószám.Text.Trim()
                                              && a.Jogtípus == Jogtípus.Text.Trim()
                                              && a.Státus == false
                                              select a).FirstOrDefault();

                if (vane != null)
                {
                    Adat_JogosítványTípus ADAT = new Adat_JogosítványTípus(vane.Sorszám,
                                                       Dolgozószám.Text.Trim(),
                                                       Jogtípus.Text.Trim(),
                                                       Jogtípusérvényes.Value,
                                                       Jogtípusmegszerzés.Value,
                                                       false);
                    Kéz_JogosítványTípus.Törlés(ADAT);
                }
                MessageBox.Show("Az adatok rögzítése megtörtént. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Jogosítványtípustábla();
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


        #region Jogosítvány és vonal
        private void Vonalfeltöltés()
        {
            try
            {
                Vonalszám.BeginUpdate();
                Vonalszám.Items.Clear();
                Vonalmegnevezés.BeginUpdate();
                Vonalmegnevezés.Items.Clear();

                List<Adat_Kiegészítő_Jogvonal> Adatok = KézKiegVonal.Lista_Adatok().OrderBy(a => a.Szám).ToList();

                foreach (Adat_Kiegészítő_Jogvonal rekord in Adatok)
                {
                    Vonalszám.Items.Add(rekord.Szám.Trim());
                    Vonalmegnevezés.Items.Add(rekord.Megnevezés.Trim());
                }

                Vonalszám.EndUpdate();
                Vonalmegnevezés.EndUpdate();
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

        private void Kiürítijogosítványvonal()
        {
            Vonalszám.Text = "";
            Vonalmegnevezés.Text = "";
            Jogvonalmegszerzés.Value = new DateTime(1900, 1, 1);
            Jogvonalérv.Value = new DateTime(1900, 1, 1);
        }

        private void Jogosítványvonaltábla()
        {
            try
            {
                List<Adat_JogosítványVonal> AdatokÖ = Kéz_JogosítványVonal.Lista_Adatok();
                List<Adat_JogosítványVonal> Adatok = (from a in AdatokÖ
                                                      where a.Törzsszám == Dolgozószám.Text.Trim()
                                                      && a.Státus == false
                                                      select a).ToList();
                if (Adatok == null && Adatok.Count < 1) return;
                DataTable AdatTábla = new DataTable();

                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Sorszám");
                AdatTábla.Columns.Add("Megszerzés");
                AdatTábla.Columns.Add("Területszám");
                AdatTábla.Columns.Add("Megnevezés");
                AdatTábla.Columns.Add("Érvényesség");

                AdatTábla.Clear();
                foreach (Adat_JogosítványVonal rekord in Adatok)
                {
                    DataRow Soradat = AdatTábla.NewRow();

                    Soradat["Sorszám"] = rekord.Sorszám;
                    Soradat["Megszerzés"] = rekord.Jogvonalmegszerzés.ToShortDateString();
                    Soradat["Területszám"] = rekord.Vonalszám;
                    Soradat["Megnevezés"] = rekord.Vonalmegnevezés;
                    Soradat["Érvényesség"] = rekord.Jogvonalérv.ToShortDateString();

                    AdatTábla.Rows.Add(Soradat);
                }
                Tábla1.CleanFilterAndSort();
                Tábla1.DataSource = AdatTábla;

                Tábla1.Columns["Sorszám"].Width = 100;
                Tábla1.Columns["Megszerzés"].Width = 120;
                Tábla1.Columns["Területszám"].Width = 120;
                Tábla1.Columns["Megnevezés"].Width = 750;
                Tábla1.Columns["Érvényesség"].Width = 120;

                Tábla1.Refresh();
                Tábla1.Visible = true;
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

        private void Tábla1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Jogvonalmegszerzés.Value = DateTime.Parse(Tábla1.Rows[e.RowIndex].Cells[1].Value.ToString());
                Vonalszám.Text = Tábla1.Rows[e.RowIndex].Cells[2].Value.ToString();
                Vonalmegnevezés.Text = Tábla1.Rows[e.RowIndex].Cells[3].Value.ToString();
                Jogvonalérv.Value = DateTime.Parse(Tábla1.Rows[e.RowIndex].Cells[4].Value.ToString());
            }
        }

        private void Jogterületrögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Dolgozószám.Text) == "") throw new HibásBevittAdat("A dolgozót ki kell választani.");
                if (Vonalszám.Text.Trim() == "") throw new HibásBevittAdat("A vonalszámot meg kell adni.");
                if (Vonalmegnevezés.Text.Trim() == "") throw new HibásBevittAdat("A vonal megnevezését meg kell adni.");
                Adatok_JogosítványVonal = Kéz_JogosítványVonal.Lista_Adatok();
                Adat_JogosítványVonal vane = (from a in Adatok_JogosítványVonal
                                              where a.Törzsszám == Dolgozószám.Text.Trim() &&
                                              a.Vonalszám == Vonalszám.Text.Trim() &&
                                              a.Státus == false
                                              select a).FirstOrDefault();

                if (vane != null)
                {
                    Adat_JogosítványVonal ADAT = new Adat_JogosítványVonal(vane.Sorszám,
                                                                           Dolgozószám.Text.Trim(),
                                                                           Jogvonalérv.Value,
                                                                           Jogvonalmegszerzés.Value,
                                                                           Vonalmegnevezés.Text.Trim(),
                                                                           Vonalszám.Text.Trim(),
                                                                           false);
                    Kéz_JogosítványVonal.Módosítás(ADAT);
                }
                else
                {
                    Adat_JogosítványVonal ADAT = new Adat_JogosítványVonal(0,
                                                                           Dolgozószám.Text.Trim(),
                                                                           Jogvonalérv.Value,
                                                                           Jogvonalmegszerzés.Value,
                                                                           Vonalmegnevezés.Text.Trim(),
                                                                           Vonalszám.Text.Trim(),
                                                                           false);
                    Kéz_JogosítványVonal.Rögzítés(ADAT);
                }
                MessageBox.Show("Az adatok rögzítése megtörtént. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Jogosítványvonaltábla();
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

        private void Jogterülettörlés_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Dolgozószám.Text) == "") throw new HibásBevittAdat("A dolgozót ki kell választani.");
                if (Vonalszám.Text.Trim() == "") throw new HibásBevittAdat("A vonalszámot meg kell adni.");
                Adatok_JogosítványVonal = Kéz_JogosítványVonal.Lista_Adatok();
                Adat_JogosítványVonal vane = (from a in Adatok_JogosítványVonal
                                              where a.Törzsszám == Dolgozószám.Text.Trim() &&
                                              a.Vonalszám == Vonalszám.Text.Trim() &&
                                              a.Státus == false
                                              select a).FirstOrDefault();

                if (vane != null)
                {
                    Adat_JogosítványVonal ADAT = new Adat_JogosítványVonal(vane.Sorszám,
                                                             Dolgozószám.Text.Trim(),
                                                             Jogvonalérv.Value,
                                                             Jogvonalmegszerzés.Value,
                                                             Vonalmegnevezés.Text.Trim(),
                                                             Vonalszám.Text.Trim(),
                                                             false);
                    Kéz_JogosítványVonal.Törlés(ADAT);

                    Vonalszám.Text = "";
                    Vonalmegnevezés.Text = "";
                    MessageBox.Show("Az adatok törlése megtörtént. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Jogosítványvonaltábla();
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

        private void Vonalszám_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {

                Adat_Kiegészítő_Jogvonal rekord = KézKiegVonal.Lista_Adatok().Where(a => a.Szám == Vonalszám.Text.Trim()).FirstOrDefault();

                if (rekord != null) Vonalmegnevezés.Text = rekord.Megnevezés.Trim();
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

        private void Vonalmegnevezés_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {

                Adat_Kiegészítő_Jogvonal rekord = KézKiegVonal.Lista_Adatok().Where(a => a.Megnevezés == Vonalmegnevezés.Text.Trim()).FirstOrDefault();
                if (rekord != null) Vonalszám.Text = rekord.Szám.Trim();
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


        #region Oktatások
        private void Tárgyfeltöltés()
        {
            try
            {
                Cmboktatásrögz.Items.Clear();
                Cmboktatásrögz.Items.Add("");
                List<Adat_OktatásTábla> Adatok = KézOktatás.Lista_Adatok().Where(a => a.Telephely == Cmbtelephely.Text.Trim()).OrderBy(a => a.Listázásisorrend).ToList();
                foreach (Adat_OktatásTábla rekord in Adatok)
                    Cmboktatásrögz.Items.Add(rekord.IDoktatás.ToString() + "=" + rekord.Téma.Trim());

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

        private void Kilépettjel_Click(object sender, EventArgs e)
        {
            Névfeltöltés();
        }

        private void Cmbtelephely_SelectedIndexChanged(object sender, EventArgs e)
        {
            Névfeltöltés();
        }

        private void Btnfrissít_Click(object sender, EventArgs e)
        {
            OktatásListanapló();
        }

        private void OktatásListanapló()
        {
            try
            {
                if (Dolgozószám.Text.Trim() == "") return;
                TáblaOktatás.Rows.Clear();
                TáblaOktatás.Columns.Clear();

                TáblaOktatás.Visible = false;
                TáblaOktatás.ColumnCount = 14;
                TáblaOktatás.RowCount = 0;
                // ' fejléc elkészítése
                TáblaOktatás.Columns[0].HeaderText = "Sorszám";
                TáblaOktatás.Columns[0].Width = 80;
                TáblaOktatás.Columns[1].HeaderText = "HR azonosító";
                TáblaOktatás.Columns[1].Width = 115;
                TáblaOktatás.Columns[2].HeaderText = "Név";
                TáblaOktatás.Columns[2].Width = 300;
                TáblaOktatás.Columns[3].HeaderText = "IDoktatás";
                TáblaOktatás.Columns[3].Width = 80;
                TáblaOktatás.Columns[4].HeaderText = "Oktatás Témája";
                TáblaOktatás.Columns[4].Width = 300;
                TáblaOktatás.Columns[5].HeaderText = "Oktatás dátuma";
                TáblaOktatás.Columns[5].Width = 110;
                TáblaOktatás.Columns[6].HeaderText = "Telephely";
                TáblaOktatás.Columns[6].Width = 120;
                TáblaOktatás.Columns[7].HeaderText = "Oktató";
                TáblaOktatás.Columns[7].Width = 150;
                TáblaOktatás.Columns[8].HeaderText = "PDF név";
                TáblaOktatás.Columns[8].Width = 300;
                TáblaOktatás.Columns[9].HeaderText = "Számonkérés";
                TáblaOktatás.Columns[9].Width = 100;
                TáblaOktatás.Columns[10].HeaderText = "Rögzítő";
                TáblaOktatás.Columns[10].Width = 100;
                TáblaOktatás.Columns[11].HeaderText = "Rögzítés ideje";
                TáblaOktatás.Columns[11].Width = 170;
                TáblaOktatás.Columns[12].HeaderText = "Státus";
                TáblaOktatás.Columns[12].Width = 100;
                TáblaOktatás.Columns[13].HeaderText = "Megjegyzés/ Tárolási hely";
                TáblaOktatás.Columns[13].Width = 200;

                // évenként ellenőrizzük az oktatásokat, egész addig amíg az aktuális évhez nem érünk
                int év = DateTime.Now.Year;
                int kezdőév = 2020;
                int futóév;

                string[] darabol = ChkDolgozónév.Text.Trim().Split('=');
                List<Adat_Oktatás_Napló> Adatok = new List<Adat_Oktatás_Napló>();
                for (futóév = év; futóév >= kezdőév; futóév--)
                {
                    List<Adat_Oktatás_Napló> AdatokÖ = KézOktatásNapló.Lista_Adatok(Cmbtelephely.Text.Trim(), futóév);
                    AdatokÖ = (from a in AdatokÖ
                               where a.Telephely == Cmbtelephely.Text.Trim()
                               && a.HRazonosító == Dolgozószám.Text.Trim()
                               orderby a.Oktatásdátuma descending
                               select a).ToList();
                    if (Cmboktatásrögz.Text != "")
                    {
                        string[] darab = Cmboktatásrögz.Text.Trim().Split('=');
                        AdatokÖ = (from a in AdatokÖ
                                   where a.IDoktatás == long.Parse(darab[0])
                                   select a).ToList();
                    }
                    Adatok.AddRange(AdatokÖ);
                }
                Adatok.OrderByDescending(a => a.Oktatásdátuma).ToList();

                List<Adat_OktatásTábla> Adatok2 = KézOktatás.Lista_Adatok();
                foreach (Adat_Oktatás_Napló rekord in Adatok)
                {
                    TáblaOktatás.RowCount++;
                    int i = TáblaOktatás.RowCount - 1;
                    TáblaOktatás.Rows[i].Cells[0].Value = rekord.ID;
                    TáblaOktatás.Rows[i].Cells[1].Value = rekord.HRazonosító.Trim();

                    TáblaOktatás.Rows[i].Cells[2].Value = darabol[0];
                    TáblaOktatás.Rows[i].Cells[3].Value = rekord.IDoktatás;
                    Adat_OktatásTábla rekordlinq = (from a in Adatok2
                                                    where a.IDoktatás == rekord.IDoktatás
                                                    select a).FirstOrDefault();
                    TáblaOktatás.Rows[i].Cells[4].Value = rekordlinq?.Téma.Trim();


                    TáblaOktatás.Rows[i].Cells[5].Value = rekord.Oktatásdátuma.ToString("yyyy.MM.dd");
                    TáblaOktatás.Rows[i].Cells[6].Value = rekord.Telephely.Trim();
                    TáblaOktatás.Rows[i].Cells[7].Value = rekord.Kioktatta.Trim();
                    TáblaOktatás.Rows[i].Cells[8].Value = rekord.PDFFájlneve.Trim();
                    switch (rekord.Számonkérés)
                    {
                        case 0:
                            {
                                TáblaOktatás.Rows[i].Cells[9].Value = "nem volt";
                                break;
                            }
                        case 1:
                            {
                                TáblaOktatás.Rows[i].Cells[9].Value = "megfelelt";
                                break;
                            }
                        case 2:
                            {
                                TáblaOktatás.Rows[i].Cells[9].Value = "nem felelt meg";
                                break;
                            }
                    }
                    TáblaOktatás.Rows[i].Cells[10].Value = rekord.Rögzítő.Trim();
                    TáblaOktatás.Rows[i].Cells[11].Value = rekord.Rögzítésdátuma.ToString("yyyy.MM.dd");
                    switch (rekord.Státus)
                    {
                        case 0:
                            {
                                TáblaOktatás.Rows[i].Cells[12].Value = "Érvényes";
                                break;
                            }
                        case 1:
                            {
                                TáblaOktatás.Rows[i].Cells[12].Value = "Törölt";
                                break;
                            }
                    }
                    TáblaOktatás.Rows[i].Cells[13].Value = rekord.Megjegyzés.Trim();
                }
                TáblaOktatás.Visible = true;
                TáblaOktatás.Refresh();

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

        private void TáblaOktatás_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                // egész sor színezése ha törölt
                foreach (DataGridViewRow row in TáblaOktatás.Rows)
                {
                    if (row.Cells[12].Value.ToString().Trim() == "Törölt")
                    {
                        row.DefaultCellStyle.ForeColor = Color.White;
                        row.DefaultCellStyle.BackColor = Color.IndianRed;
                        row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f, FontStyle.Strikeout);
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

        private void TáblaOktatás_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (TáblaOktatás.SelectedRows.Count != 0)
                {
                    string hely = $@"{Application.StartupPath}\Főmérnökség\Oktatás\{Cmbtelephely.Text.Trim()}\{TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[8].Value}";
                    if (!File.Exists(hely)) return;
                    PDF_Nyitás(hely);
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

        private void TáblaOktatás_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                TáblaOktatás.ClearSelection();
                TáblaOktatás.Rows[e.RowIndex].Selected = true;
            }
        }
        #endregion


        #region Munkakör
        private void Munkakörfeltöltés()
        {
            try
            {
                AdatokFeor = Kéz.Lista_Adatok().Where(a => a.Státus == 0).OrderBy(a => a.Feormegnevezés).ToList();
                Munkakör.BeginUpdate();
                Munkakör.Items.Clear();
                foreach (Adat_Kiegészítő_Feorszámok rekord in AdatokFeor)
                    Munkakör.Items.Add(rekord.Feormegnevezés.Trim());

                Munkakör.EndUpdate();
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

        private void Üritimunkakört()
        {
            Munkakör.Text = "";
            Feorszám.Text = "";
        }

        private void Munkakörmódosít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Feorszám.Text.Trim() == "") throw new HibásBevittAdat("A Feor szám mező nem lehet üres mező.");
                if (Munkakör.Text.Trim() == "") throw new HibásBevittAdat("A munkakört meg kell adni.");
                if (Dolgozószám.Text.Trim() == "") throw new HibásBevittAdat("Dolgozót ki kell választani.");

                Adat_Dolgozó_Alap ADAT = new Adat_Dolgozó_Alap(Dolgozószám.Text.Trim(),
                                                             Feorszám.Text.Trim(),
                                                             Munkakör.Text.Trim());
                KézDolgozó.Módosít_Munka(Cmbtelephely.Text.Trim(), ADAT);
                DolgozóAdatok = KézDolgozó.Lista_Adatok(Cmbtelephely.Text.Trim());
                ÚjraKiír();
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

        private void Munkakör_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Adat_Kiegészítő_Feorszámok rekord = (from a in AdatokFeor
                                                     where a.Feormegnevezés == Munkakör.Text.Trim()
                                                     select a).FirstOrDefault();
                if (rekord != null) Feorszám.Text = rekord.Feorszám.Trim();
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

        private void Munkakör_kiírás()
        {
            try
            {
                if (Dolgozószám.Text.Trim() == "") return;

                if (EgyDolgozó != null)
                {
                    Feorszám.Text = EgyDolgozó.Feorsz.Trim();
                    Munkakör.Text = EgyDolgozó.Munkakör.Trim();
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region Munkaköri adatok
        private void CsoportCombofeltöltés()
        {
            MunkaCsoport.Items.Clear();
            MunkaCsoport.Items.Add("Munkakör");
            AdatokMunkakör = KézMunkakör.Lista_Adatok();
            List<string> Adatok = AdatokMunkakör.Select(a => a.Kategória).Distinct().ToList();
            if (Adatok != null)
            {
                foreach (string rekord in Adatok)
                    MunkaCsoport.Items.Add(rekord.Trim());
            }
        }

        private void PDFMunkakörfeltöltés()
        {
            try
            {
                AdatokFeor = Kéz.Lista_Adatok().Where(a => a.Státus == 0).OrderBy(a => a.Feormegnevezés).ToList();
                PDFMunkakör.BeginUpdate();
                PDFMunkakör.Items.Clear();
                foreach (Adat_Kiegészítő_Feorszámok rekord in AdatokFeor)
                    PDFMunkakör.Items.Add(rekord.Feormegnevezés.Trim());

                PDFMunkakör.EndUpdate();
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

        private void MunkakCsoport_SelectedIndexChanged(object sender, EventArgs e)
        {
            Csoportválasztó();
        }

        private void Csoportválasztó()
        {
            try
            {
                if (MunkaCsoport.Text.Trim() == "") return;
                PDFMunkakör.Items.Clear();
                if (MunkaCsoport.Text.Trim() == "Munkakör")
                    PDFMunkakörfeltöltés();
                else
                    MindenMásFeltöltés();
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

        private void MindenMásFeltöltés()
        {
            try
            {
                PDFMunkakör.BeginUpdate();
                PDFMunkakör.Items.Clear();
                AdatokMunkakör = KézMunkakör.Lista_Adatok();

                List<Adat_Kiegészítő_Munkakör> Adatok = (from a in AdatokMunkakör
                                                         where a.Kategória == MunkaCsoport.Text.Trim()
                                                         select a).ToList();

                if (Adatok != null)
                {
                    foreach (Adat_Kiegészítő_Munkakör rekord in Adatok)
                        PDFMunkakör.Items.Add(rekord.Megnevezés.Trim());
                }
                PDFMunkakör.EndUpdate();
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

        private void Munkakör_Megnyit_Click(object sender, EventArgs e)
        {
            try
            {
                TxtPDFfájl.Text = "";
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    Filter = "PDF Files |*.pdf"
                };
                if (OpenFileDialogPI.ShowDialogEllenőr(OpenFileDialog1) == DialogResult.OK)
                {
                    PDF_Nyitás(OpenFileDialog1.FileName);
                    TxtPDFfájl.Text = OpenFileDialog1.FileName;

                    Fülek.SelectedIndex = 5;
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

        private void BtnPDFsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtPDFfájl.Text == "") throw new HibásBevittAdat("Nincs feltöltendő fájl.");
                if (Dolgozószám.Text == "") throw new HibásBevittAdat("Nincs kiválasztva egy dolgozó sem.");
                if (PDFMunkakör.Text == "") throw new HibásBevittAdat("Nincs meghatározva a munkakör.");
                if (MunkaCsoport.Text.Trim() == "") throw new HibásBevittAdat("Nincs meghatározva a csoport.");

                long sorszám;
                string fájlnév = "";
                if (MunkaCsoport.Text.Trim() == "Munkakör")
                {
                    Adat_Kiegészítő_Feorszámok rekord = AdatokFeor.FirstOrDefault(a => a.Feormegnevezés.Trim() == PDFMunkakör.Text.Trim());
                    sorszám = rekord != null ? rekord.Sorszám : 0;
                    fájlnév = $@"{Dolgozószám.Text.Trim()}_MUN_{Cmbtelephely.Text.Trim()}_{sorszám}_{DateTime.Now:yyyyMMdd}.pdf";
                }
                else
                {
                    Adat_Kiegészítő_Munkakör rekord = AdatokMunkakör.FirstOrDefault(a => a.Megnevezés.Trim() == PDFMunkakör.Text.Trim());
                    sorszám = rekord != null ? rekord.Id : 0;
                    fájlnév = $@"{Dolgozószám.Text.Trim()}_{MunkaCsoport.Text.Substring(0, 4)}_{Cmbtelephely.Text.Trim()}_{sorszám}_{DateTime.Now:yyyyMMdd}.pdf";
                }

                string hely = $@"{Application.StartupPath}\Főmérnökség\Munkakör".KönyvSzerk();
                hely = $@"{Application.StartupPath}\Főmérnökség\Munkakör\{Cmbtelephely.Text.Trim()}".KönyvSzerk();
                hely += $@"\{fájlnév}";

                // PDF fájl-t feltöljük.
                if (File.Exists(hely))
                {
                    if (MessageBox.Show("Ezen a néven már létezik fájl, felülírjuk?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                        return;
                    else
                        File.Delete(hely);
                }
                // ha nem létezik akkor odamásoljuk
                File.Copy(TxtPDFfájl.Text, hely);

                Adat_Munkakör ADAT = new Adat_Munkakör(0,
                                                       PDFMunkakör.Text.Trim(),
                                                       fájlnév,
                                                       0,
                                                       Cmbtelephely.Text.Trim(),
                                                       Dolgozószám.Text.Trim(),
                                                       DateTime.Now,
                                                       Program.PostásNév.Trim());
                Kéz_Munkakör.Rögzítés(ADAT);
                MessageBox.Show("Az adatok rögzítése megtörtént. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                TxtPDFfájl.Text = "";
                PDFMunkakör.Text = "";
                MunkaCsoport.Text = "";
                Munkakörlistázás();
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

        private void Munkakör_Töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (Munkakörtábla.SelectedRows.Count == 0) return;

                Adat_Munkakör ADAT = new Adat_Munkakör(long.Parse(Munkakörtábla.Rows[Munkakörtábla.SelectedRows[0].Index].Cells[0].Value.ToString()),
                                                       PDFMunkakör.Text.Trim(),
                                                       "",
                                                       1,
                                                       Cmbtelephely.Text.Trim(),
                                                       Dolgozószám.Text.Trim(),
                                                       DateTime.Now,
                                                       Program.PostásNév.Trim());
                Kéz_Munkakör.Törlés(ADAT);

                MessageBox.Show("Az adatok törlése megtörtént. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Munkakörlistázás();
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

        private void Munkakörlistázás()
        {
            try
            {
                if (Dolgozószám.Text == "") return;

                Munkakörtábla.Rows.Clear();
                Munkakörtábla.Columns.Clear();
                // .Refresh()
                Munkakörtábla.Visible = false;
                Munkakörtábla.ColumnCount = 8;
                Munkakörtábla.RowCount = 0;
                // ' fejléc elkészítése
                Munkakörtábla.Columns[0].HeaderText = "Sorszám";
                Munkakörtábla.Columns[0].Width = 80;
                Munkakörtábla.Columns[1].HeaderText = "HR azonosító";
                Munkakörtábla.Columns[1].Width = 115;
                Munkakörtábla.Columns[2].HeaderText = "Tevékenység";
                Munkakörtábla.Columns[2].Width = 300;
                Munkakörtábla.Columns[3].HeaderText = "Telephely";
                Munkakörtábla.Columns[3].Width = 120;
                Munkakörtábla.Columns[4].HeaderText = "PDF név";
                Munkakörtábla.Columns[4].Width = 400;
                Munkakörtábla.Columns[5].HeaderText = "Rögzítő";
                Munkakörtábla.Columns[5].Width = 100;
                Munkakörtábla.Columns[6].HeaderText = "Rögzítés ideje";
                Munkakörtábla.Columns[6].Width = 170;
                Munkakörtábla.Columns[7].HeaderText = "Státus";
                Munkakörtábla.Columns[7].Width = 100;

                List<Adat_Munkakör> AdatokÖ = Kéz_Munkakör.Lista_Adatok();
                List<Adat_Munkakör> Adatok = (from a in AdatokÖ
                                              where a.Telephely == Cmbtelephely.Text.Trim()
                                              && a.HRazonosító == Dolgozószám.Text.Trim()
                                              orderby a.ID
                                              select a).ToList();

                foreach (Adat_Munkakör rekord in Adatok)
                {
                    Munkakörtábla.RowCount++;
                    int i = Munkakörtábla.RowCount - 1;
                    Munkakörtábla.Rows[i].Cells[0].Value = rekord.ID;
                    Munkakörtábla.Rows[i].Cells[1].Value = rekord.HRazonosító.Trim();
                    Munkakörtábla.Rows[i].Cells[2].Value = rekord.Megnevezés.Trim();
                    Munkakörtábla.Rows[i].Cells[3].Value = rekord.Telephely.Trim();
                    Munkakörtábla.Rows[i].Cells[4].Value = rekord.PDFfájlnév.Trim();
                    Munkakörtábla.Rows[i].Cells[5].Value = rekord.Rögzítő.Trim();
                    Munkakörtábla.Rows[i].Cells[6].Value = rekord.Dátum.ToString("yyyy.MM.dd");
                    switch (rekord.Státus)
                    {
                        case 0:
                            {
                                Munkakörtábla.Rows[i].Cells[7].Value = "Érvényes";
                                break;
                            }
                        case 1:
                            {
                                Munkakörtábla.Rows[i].Cells[7].Value = "Törölt";
                                break;
                            }
                    }
                }

                Munkakörtábla.Visible = true;
                Munkakörtábla.Refresh();
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

        private void Munkakörtábla_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                // egész sor színezése ha törölt
                foreach (DataGridViewRow row in Munkakörtábla.Rows)
                {
                    if (row.Cells[7].Value.ToString().Trim() == "Törölt")
                    {
                        row.DefaultCellStyle.ForeColor = Color.White;
                        row.DefaultCellStyle.BackColor = Color.IndianRed;
                        row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f, FontStyle.Strikeout);
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

        private void Munkakörtábla_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (Munkakörtábla.SelectedRows.Count != 0)
                {
                    string hely = $@"{Application.StartupPath}\Főmérnökség\Munkakör\{Cmbtelephely.Text.Trim()}\{Munkakörtábla.Rows[Munkakörtábla.SelectedRows[0].Index].Cells[4].Value}";
                    PDF_Nyitás(hely);
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

        private void Munkakörtábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string hely = $@"{Application.StartupPath}\Főmérnökség\Munkakör\{Cmbtelephely.Text.Trim()}\{Munkakörtábla.Rows[e.RowIndex].Cells[4].Value}";
            PDF_Nyitás(hely);
        }

        private void PDF_Nyitás(string hely)
        {
            try
            {
                if (!File.Exists(hely)) return;
                Kezelő_Pdf.PdfMegnyitás(PDF_néző, hely);
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


        #region Szeméyes adatok
        private void Ürítiaszemélyest()
        {
            Leánykori.Text = "";
            Anyja.Text = "";
            Születésiidő.Value = new DateTime(1900, 1, 1);
            Születésihely.Text = "";
            Lakcím.Text = "";
            Ideiglenescím.Text = "";
            Telefonszám1.Text = "";
            Telefonszám2.Text = "";
            Telefonszám3.Text = "";
        }

        private void Láthatszemélyes()
        {
            try
            {
                string adat1 = Program.PostásNév.Trim().ToUpper();
                string adat2 = Program.PostásTelephely.Trim().ToUpper();
                string adat3 = "A";

                Panel4.Visible = KézKulcs.ABKULCSvan(adat1, adat2, adat3);
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

        private void Kiírja_személyes()
        {
            try
            {
                if (Dolgozószám.Text.Trim() == "") return;

                List<Adat_Dolgozó_Személyes> Adatok = KézSzemélyes.Lista_Adatok();
                Adat_Dolgozó_Személyes rekord = (from a in Adatok
                                                 where a.Dolgozószám == Dolgozószám.Text.Trim()
                                                 select a).FirstOrDefault();

                if (rekord != null)
                {
                    Leánykori.Text = rekord.Leánykori.Trim();
                    Anyja.Text = rekord.Anyja.Trim();
                    Születésiidő.Value = rekord.Születésiidő;
                    Születésihely.Text = rekord.Születésihely.Trim();
                    Lakcím.Text = rekord.Lakcím.Trim();
                    Ideiglenescím.Text = rekord.Ideiglenescím.Trim();
                    Telefonszám1.Text = rekord.Telefonszám1.Trim();
                    Telefonszám2.Text = rekord.Telefonszám2.Trim();
                    Telefonszám3.Text = rekord.Telefonszám3.Trim();
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

        private void Személyesmódosítás_Click(object sender, EventArgs e)
        {
            try
            {
                if (Dolgozószám.Text.Trim() == "") return;

                List<Adat_Dolgozó_Személyes> Adatok = KézSzemélyes.Lista_Adatok();

                Adat_Dolgozó_Személyes vane = (from a in Adatok
                                               where a.Dolgozószám == Dolgozószám.Text.Trim()
                                               select a).FirstOrDefault();

                Adat_Dolgozó_Személyes ADAT = new Adat_Dolgozó_Személyes(Anyja.Text.Trim(),
                                                                         Dolgozószám.Text.Trim(),
                                                                         Ideiglenescím.Text.Trim(),
                                                                         Lakcím.Text.Trim(),
                                                                         Leánykori.Text.Trim(),
                                                                         Születésihely.Text.Trim(),
                                                                         Születésiidő.Value,
                                                                         Telefonszám1.Text.Trim(),
                                                                         Telefonszám2.Text.Trim(),
                                                                         Telefonszám3.Text.Trim());

                if (vane == null)
                    KézSzemélyes.Rögzítés(ADAT);
                else
                    KézSzemélyes.Módosítás(ADAT);

                Ürítiaszemélyest();
                Kiírja_személyes();
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
        #endregion


        #region Bér adatok
        private void LáthatBér()
        {
            try
            {
                string adat1 = Program.PostásNév.Trim().ToUpper();
                string adat2 = Program.PostásTelephely.Trim().ToUpper();
                string adat3 = "B";
                Panel5.Visible = KézKulcs.ABKULCSvan(adat1, adat2, adat3);
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

        private void Kiirja_bért()
        {
            try
            {
                Órabér.Text = "";
                if (Dolgozószám.Text.Trim() == "") return;

                List<Adat_Kulcs> Adatok = KézKulcsKettő.Lista_Adatok();
                Adat_Kulcs vane = Adatok.FirstOrDefault(a => a.Adat1.Contains(MyF.Rövidkód(Dolgozószám.Text)));

                if (vane != null) Órabér.Text = MyF.Dekódolja(vane.Adat2);

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

        private void Bérrögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Dolgozószám.Text == "") return;
                if (Órabér.Text == "") return;
                if (!int.TryParse(Órabér.Text, out int órabér)) return;

                AdatokKulcs = KézKulcsKettő.Lista_Adatok();

                Adat_Kulcs vane = AdatokKulcs.FirstOrDefault(a => a.Adat1.Contains(MyF.Rövidkód(Dolgozószám.Text)));
                Adat_Kulcs ADAT = new Adat_Kulcs(MyF.Rövidkód(Dolgozószám.Text), MyF.Kódol(Órabér.Text));
                if (vane != null)
                    KézKulcsKettő.Módosít(ADAT);
                else
                    KézKulcsKettő.Rögzít(ADAT);

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
        #endregion


        #region Túlóra adatok
        private void Láthattúlóra()
        {
            try
            {
                string adat1 = Program.PostásNév.Trim().ToUpper();
                string adat2 = Program.PostásTelephely.Trim().ToUpper();
                string adat3 = "C";

                Panel6.Visible = KézKulcs.ABKULCSvan(adat1, adat2, adat3);
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

        private void Túlóra_kiírása()
        {
            try
            {
                if (Dolgozószám.Text.Trim() == "") return;

                CheckBox1.Checked = EgyDolgozó.Túlóraeng;
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
                if (Dolgozószám.Text.Trim() == "") return;
                KézDolgozó.Módosít_Túl(Cmbtelephely.Text.Trim(), Dolgozószám.Text.Trim(), CheckBox1.Checked);
                DolgozóAdatok = KézDolgozó.Lista_Adatok(Cmbtelephely.Text.Trim());
                ÚjraKiír();
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

        private void Túlóraellenőrzés()
        {
            try
            {
                // *****************************************
                // leellenőrizzük, hogy lehet-e még túlóráznia
                // *****************************************
                List<Adat_Szatube_Túlóra> Adatok = KézTúlóra.Lista_Adatok(Cmbtelephely.Text.Trim(), DateTime.Today.Year);

                int ÉvesTúlóra = (from a in Adatok
                                  where a.Törzsszám == Dolgozószám.Text.Trim() && a.Státus != 3
                                  select a.Kivettnap).Sum();


                Túlórakiró.Text = $"Tárgy évi túlóra mennyiség: {ÉvesTúlóra / 60} óra.";
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


        #region Névdarabolás

        private void ÚjraKiír()
        {
            string[] Darabol = ChkDolgozónév.Text.Split('=');

            if (Darabol.Length == 2)
            {
                EgyDolgozó = (from a in DolgozóAdatok
                              where a.Dolgozószám == Darabol[1].Trim()
                              select a).FirstOrDefault();
                Dolgozószám.Text = Darabol[1].Trim();
            }
        }
        #endregion

        private void Cmbtelephely_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                Cmbtelephely.Text = Cmbtelephely.Items[Cmbtelephely.SelectedIndex].ToStrTrim();
                if (Cmbtelephely.Text.Trim() == "") return;
                //Ha az első karakter "R" akkor az új jogosultságkiosztást használjuk
                //ha nem akkor a régit használjuk
                if (Program.PostásJogkör.Substring(0, 1) == "R")
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
                else
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
    }
}