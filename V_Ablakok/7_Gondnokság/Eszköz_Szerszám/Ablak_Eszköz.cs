using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos.Villamos_Ablakok
{
    public partial class Ablak_Eszköz : Form
    {
        bool VanKönyv = false;
        string KönyvSzám = "";

        readonly Kezelő_Szerszám_Könyv KézKönyv = new Kezelő_Szerszám_Könyv();
        readonly Kezelő_Szerszám_Cikk KézSzerszámCikk = new Kezelő_Szerszám_Cikk();
        readonly Kezelő_Eszköz KézEszk = new Kezelő_Eszköz();
        readonly Kezelő_Szerszám_könvyvelés KézKönyvelés = new Kezelő_Szerszám_könvyvelés();

        List<Adat_Szerszám_Cikktörzs> AdatokCikk = new List<Adat_Szerszám_Cikktörzs>();

        #region Alap
        public Ablak_Eszköz()
        {
            InitializeComponent();
            Start();
        }

        private void Ablak_Eszköz_Load(object sender, EventArgs e)
        {
        }

        private void Start()
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

            Fülekkitöltése();
            Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;
        }

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Személy(true))
                    Cmbtelephely.Items.Add(Elem);

                if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér)
                    Cmbtelephely.Text = Cmbtelephely.Items[0].ToString().Trim();
                else
                    Cmbtelephely.Text = Program.PostásTelephely;

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

        private void Jogosultságkiosztás()
        {
            int melyikelem;

            // ide kell az összes gombot tenni amit szabályozni akarunk false
            SAP_adatok.Visible = false;
            Át_Tölt.Visible = false;

            melyikelem = 228;
            // módosítás 1 
            if (MyF.Vanjoga(melyikelem, 1))
            {
                SAP_adatok.Visible = true;
            }
            // módosítás 2
            if (MyF.Vanjoga(melyikelem, 2))
            {
                Át_Tölt.Visible = true;
            }
            // módosítás 3 
            if (MyF.Vanjoga(melyikelem, 3))
            {

            }
        }

        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\eszköz.html";
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
                        OsztályozCombo_Feltöltés();
                        break;
                    }
                case 1:
                    {
                        EllenCombo_Feltöltés();
                        break;
                    }

            }
        }

        private void OsztályozCombo_Feltöltés()
        {
            Szűr_Osztás.Items.Clear();
            Szűr_Osztás.Items.Add("");
            Szűr_Osztás.Items.Add("Nincs beállítva");
            Szűr_Osztás.Items.Add("Épület");
            Szűr_Osztás.Items.Add("Szerszám");
        }

        private void EllenCombo_Feltöltés()
        {
            Ellen_Besorolás.Items.Clear();
            Ellen_Besorolás.Items.Add("");
            Ellen_Besorolás.Items.Add("Nincs beállítva");
            Ellen_Besorolás.Items.Add("Épület");
            Ellen_Besorolás.Items.Add("Szerszám");

            Ellen_Szűrő.Items.Clear();
            Ellen_Szűrő.Items.Add("Nem vizsgál");
            Ellen_Szűrő.Items.Add("Csak épület");
            Ellen_Szűrő.Items.Add("Csak szerszám");
            Ellen_Szűrő.Items.Add("Mind kettő");
            Ellen_Szűrő.Items.Add("Egyik sem");

            Ellen_Szűrő.Text = "Nem vizsgál";

            Besorolás_Combo.Items.Clear();
            Besorolás_Combo.Items.Add("");
            Besorolás_Combo.Items.Add("Nincs beállítva");
            Besorolás_Combo.Items.Add("Épület");
            Besorolás_Combo.Items.Add("Szerszám");
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
            StringFormat sf = new StringFormat()
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


        #region Adatbeolvasás lapfül
        // JAVÍTANDÓ:
        private void SAP_adatok_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "SAP-s Adatok betöltése",
                    FileName = "",
                    Filter = "Excel 97-2003 (*.xls)|*.xls|Excel (*.xlsx)|*.xlsx"
                };
                string fájlexc;
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialogPI.ShowDialogEllenőr(OpenFileDialog1) == DialogResult.OK)
                    fájlexc = OpenFileDialog1.FileName;
                else
                {
                    SAP_adatok.Visible = true;
                    return;
                }

                SAP_Adatokbeolvasása.Eszköz_Beolvasó(fájlexc, Cmbtelephely.Text.Trim());
                MessageBox.Show("Az adat konvertálás befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void BtnExcelkimenet_Click(object sender, EventArgs e)
        {
            try
            {
                Excel_Kimenet(Tábla, "Eszköz_lista");
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Frissítés_Click(object sender, EventArgs e)
        {
            TáblaÍró();
        }

        private void TáblaÍró()
        {
            try
            {
                List<Adat_Eszköz> Adatok = KézEszk.Lista_Adatok(Cmbtelephely.Text.Trim());
                if (Adatok == null) return;

                if (Szűr_Hely.Text.Trim() != "")
                {
                    Adatok = (from a in Adatok
                              where a.Helyiség_megnevezés.Contains(Szűr_Hely.Text.Trim())
                              orderby a.Eszköz
                              select a).ToList();
                }
                if (Szűr_Megnevezés.Text.Trim() != "")
                {
                    Adatok = (from a in Adatok
                              where a.Megnevezés.Contains(Szűr_Megnevezés.Text.Trim())
                              orderby a.Eszköz
                              select a).ToList();
                }
                if (Szűr_Név.Text.Trim() != "")
                {
                    Adatok = (from a in Adatok
                              where a.Dolgozó_neve.Contains(Szűr_Név.Text.Trim())
                              orderby a.Eszköz
                              select a).ToList();
                }
                if (Szűr_Osztás.Text.Trim() != "")
                {
                    Adatok = (from a in Adatok
                              where a.Épület_Szerszám.Contains(Szűr_Osztás.Text.Trim())
                              orderby a.Eszköz
                              select a).ToList();
                }


                DataTable AdatTábla = new DataTable();

                AdatTábla.Columns.Add("Eszközszám");
                AdatTábla.Columns.Add("Megnevezés");
                AdatTábla.Columns.Add("Leltáriszám");
                AdatTábla.Columns.Add("Helyiség_megnevezés");
                AdatTábla.Columns.Add("HR szám");
                AdatTábla.Columns.Add("Dolgozó neve");
                AdatTábla.Columns.Add("Gyáriszám");
                AdatTábla.Columns.Add("Alszám");
                AdatTábla.Columns.Add("Megnevezés folytatása");
                AdatTábla.Columns.Add("Mennyiség");
                AdatTábla.Columns.Add("Telephely");
                AdatTábla.Columns.Add("Telephely megnevezése");
                AdatTábla.Columns.Add("Költséghely");
                AdatTábla.Columns.Add("Felelős Költséghely");
                AdatTábla.Columns.Add("Helyiség");
                AdatTábla.Columns.Add("Vonalkódozható");
                AdatTábla.Columns.Add("Pályaszám");
                AdatTábla.Columns.Add("Épületben van");
                AdatTábla.Columns.Add("Szerszámban van");
                AdatTábla.Columns.Add("Besorolás");

                AdatTábla.Clear();
                foreach (Adat_Eszköz rekord in Adatok)
                {
                    DataRow Soradat = AdatTábla.NewRow();
                    Soradat["Eszközszám"] = rekord.Eszköz;
                    Soradat["Megnevezés"] = rekord.Megnevezés;
                    Soradat["Leltáriszám"] = rekord.Leltárszám;
                    Soradat["Helyiség_megnevezés"] = rekord.Helyiség_megnevezés;
                    Soradat["HR szám"] = rekord.Szemügyi_törzsszám;
                    Soradat["Dolgozó neve"] = rekord.Dolgozó_neve;
                    Soradat["Gyáriszám"] = rekord.Gyártási_szám;
                    Soradat["Alszám"] = rekord.Alszám;
                    Soradat["Megnevezés folytatása"] = rekord.Megnevezés_folyt;
                    Soradat["Mennyiség"] = rekord.Mennyiség;
                    Soradat["Telephely"] = rekord.Telephely;
                    Soradat["Telephely megnevezése"] = rekord.Telephely_megnevezése;
                    Soradat["Költséghely"] = rekord.Költséghely;
                    Soradat["Felelős Költséghely"] = rekord.Felelős_költséghely;
                    Soradat["Helyiség"] = rekord.Helyiség;
                    Soradat["Vonalkódozható"] = rekord.Vonalkód;
                    Soradat["Pályaszám"] = rekord.Rendszám_pályaszám;
                    Soradat["Épületben van"] = rekord.Épület_van == true ? "Igen" : "Nem";
                    Soradat["Szerszámban van"] = rekord.Szerszám_van == true ? "Igen" : "Nem";
                    Soradat["Besorolás"] = rekord.Épület_Szerszám;

                    AdatTábla.Rows.Add(Soradat);
                }
                Tábla.CleanFilterAndSort();
                Tábla.DataSource = AdatTábla;

                Tábla.Columns["Eszközszám"].Width = 140;
                Tábla.Columns["Megnevezés"].Width = 400;
                Tábla.Columns["Leltáriszám"].Width = 120;
                Tábla.Columns["Helyiség_megnevezés"].Width = 180;
                Tábla.Columns["HR szám"].Width = 80;
                Tábla.Columns["Dolgozó neve"].Width = 160;
                Tábla.Columns["Gyáriszám"].Width = 160;
                Tábla.Columns["Alszám"].Width = 70;
                Tábla.Columns["Megnevezés folytatása"].Width = 300;
                Tábla.Columns["Mennyiség"].Width = 100;
                Tábla.Columns["Telephely"].Width = 100;
                Tábla.Columns["Telephely megnevezése"].Width = 250;
                Tábla.Columns["Költséghely"].Width = 100;
                Tábla.Columns["Felelős Költséghely"].Width = 100;
                Tábla.Columns["Helyiség"].Width = 100;
                Tábla.Columns["Vonalkódozható"].Width = 80;
                Tábla.Columns["Pályaszám"].Width = 100;
                Tábla.Columns["Épületben van"].Width = 100;
                Tábla.Columns["Szerszámban van"].Width = 100;
                Tábla.Columns["Besorolás"].Width = 100;

                Tábla.Visible = true;
                Tábla.Refresh();
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

        private void Cikklétrehozás(string Telephely, string Melyik, Adat_Eszköz EszkAdat)
        {
            // cikk adatok
            List<Adat_Szerszám_Cikktörzs> AAdat = KézSzerszámCikk.Lista_Adatok(Telephely, Melyik);
            string azon = $"E{EszkAdat.Eszköz.Trim()}";

            Adat_Szerszám_Cikktörzs Adat = new Adat_Szerszám_Cikktörzs(
                "E" + EszkAdat.Eszköz,
                EszkAdat.Megnevezés.Trim(),
                EszkAdat.Mennyiség.ToString(),
                EszkAdat.Helyiség.Trim(),
                EszkAdat.Leltárszám.Trim(),
                EszkAdat.Aktiválás_dátuma,
                0,
                EszkAdat.Költséghely,
                EszkAdat.Gyártási_szám);

            Adat_Szerszám_Cikktörzs vane = (from a in AAdat
                                            where a.Azonosító == azon
                                            select a).FirstOrDefault();

            if (vane != null)
                KézSzerszámCikk.Módosítás(Telephely, Melyik, Adat);
            else
                KézSzerszámCikk.Rögzítés(Telephely, Melyik, Adat);
        }

        private void Könyvlétrehozás(string Telephely, string Melyik, Adat_Eszköz EszkAdat)
        {
            VanKönyv = false;
            KönyvSzám = "";
            Adat_Szerszám_Könyvtörzs AdatHely = null;
            List<Adat_Szerszám_Könyvtörzs> Adatok = KézKönyv.Lista_Adatok(Telephely, Melyik);

            //Helyiség adatok
            if (EszkAdat.Szemügyi_törzsszám.Trim() != "")
            {
                // Akkor személyes használatra kiadott eszköz
                //Csak az új tételekkel foglakozunk

                string rekord = $"{EszkAdat.Dolgozó_neve.Trim()}={EszkAdat.Szemügyi_törzsszám.Trim()}";
                Adat_Szerszám_Könyvtörzs vane = Adatok.FirstOrDefault(a => a.Felelős1 == rekord);

                if (vane == null)
                {
                    AdatHely = new Adat_Szerszám_Könyvtörzs(
                        EszkAdat.Szemügyi_törzsszám.Trim(),
                        EszkAdat.Dolgozó_neve.Trim(),
                        EszkAdat.Dolgozó_neve.Trim() + "=" + EszkAdat.Szemügyi_törzsszám.Trim(),
                        "",
                        false);
                    VanKönyv = true;
                    KönyvSzám = EszkAdat.Szemügyi_törzsszám.Trim();
                }
                else
                {
                    VanKönyv = true;
                    KönyvSzám = vane.Szerszámkönyvszám;
                }
            }
            else
            {
                // Helyiség
                if (EszkAdat.Helyiség.Trim() != "")
                {
                    string azon = EszkAdat.Helyiség.Trim();
                    bool vane = Adatok.Any(a => a.Szerszámkönyvszám == azon);

                    if (!vane)
                    {
                        string ideig = EszkAdat.Helyiség_megnevezés.Trim() == "" ? "_" : EszkAdat.Helyiség_megnevezés.Trim();
                        AdatHely = new Adat_Szerszám_Könyvtörzs(
                            EszkAdat.Helyiség.Trim(),
                            ideig.Trim(),
                            "", "", false);
                        VanKönyv = true;
                        KönyvSzám = EszkAdat.Helyiség.Trim();
                    }
                    else
                    {
                        VanKönyv = true;
                        KönyvSzám = EszkAdat.Helyiség.Trim();
                    }
                }
            }
            //ha van könyvadat akkor rögzítjük
            if (AdatHely != null) KézKönyv.Rögzítés(Telephely, Melyik, AdatHely);
        }

        private void KönyvelésLétrehozása(string Telephely, string Melyik, Adat_Eszköz eszkAdat)
        {
            if (VanKönyv)
            {
                string eszkoz = $"E{eszkAdat.Eszköz.Trim()}";

                List<Adat_Szerszám_Könyvelés> Adatok = KézKönyvelés.Lista_Adatok(Telephely, Melyik);
                Adat_Szerszám_Könyvelés vane = (from a in Adatok
                                                where a.AzonosítóMás == eszkoz
                                                select a).FirstOrDefault();

                //Ha nincs a könyvelésben csak akkor rögzítjük
                if (vane == null)
                {
                    Adat_Szerszám_Cikktörzs Azonosító = new Adat_Szerszám_Cikktörzs(eszkoz, "");
                    Adat_Szerszám_Könyvtörzs Szerszámkönyvszám = new Adat_Szerszám_Könyvtörzs(KönyvSzám.Trim(), "");
                    Adat_Szerszám_Könyvelés Adat = new Adat_Szerszám_Könyvelés(Azonosító, Szerszámkönyvszám, eszkAdat.Mennyiség.ToÉrt_Int());
                    KézKönyvelés.Rögzítés(Telephely, Melyik, Adat);
                }
            }
        }
        #endregion


        #region Ellenőrzés lapfül
        private void Ellen_Excel_Click(object sender, EventArgs e)
        {
            try
            {
                Excel_Kimenet(Ellen_Tábla, "Ellenőrző_");
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

        private void Ellen_Frissít_Click(object sender, EventArgs e)
        {
            Ellen_TáblaÍró();
        }

        private void Ellen_TáblaÍró()
        {
            try
            {
                List<Adat_Eszköz> Adatok = KézEszk.Lista_Adatok(Cmbtelephely.Text.Trim());

                if (Adatok == null) return;

                if (Ellen_Besorolás.Text.Trim() != "")
                {
                    Adatok = (from a in Adatok
                              where a.Épület_Szerszám.Contains(Ellen_Besorolás.Text.Trim())
                              orderby a.Eszköz
                              select a).ToList();
                }

                if (Ellen_Szűrő.Text.Trim() == "Csak épület")
                {
                    Adatok = (from a in Adatok
                              where a.Épület_van == true && a.Szerszám_van == false
                              orderby a.Eszköz
                              select a).ToList();
                }
                else if (Ellen_Szűrő.Text.Trim() == "Csak szerszám")
                {
                    Adatok = (from a in Adatok
                              where a.Épület_van == false && a.Szerszám_van == true
                              orderby a.Eszköz
                              select a).ToList();
                }
                else if (Ellen_Szűrő.Text.Trim() == "Mind kettő")
                {
                    Adatok = (from a in Adatok
                              where a.Épület_van == true && a.Szerszám_van == true
                              orderby a.Eszköz
                              select a).ToList();
                }
                else if (Ellen_Szűrő.Text.Trim() == "Egyik sem")
                {
                    Adatok = (from a in Adatok
                              where a.Épület_van == false && a.Szerszám_van == false
                              orderby a.Eszköz
                              select a).ToList();
                }

                DataTable AdatTábla = new DataTable();

                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Eszközszám", typeof(string));
                AdatTábla.Columns.Add("Megnevezés");
                AdatTábla.Columns.Add("Besorolás");
                AdatTábla.Columns.Add("Épületben van");
                AdatTábla.Columns.Add("Szerszámban van");
                AdatTábla.Columns.Add("Költséghely");

                AdatTábla.Clear();

                foreach (Adat_Eszköz rekord in Adatok)
                {
                    DataRow Soradat = AdatTábla.NewRow();
                    Soradat["Eszközszám"] = rekord.Eszköz;
                    Soradat["Megnevezés"] = rekord.Megnevezés;
                    Soradat["Besorolás"] = rekord.Épület_Szerszám;
                    Soradat["Épületben van"] = rekord.Épület_van == true ? "Igen" : "Nem";
                    Soradat["Szerszámban van"] = rekord.Szerszám_van == true ? "Igen" : "Nem";
                    Soradat["Költséghely"] = rekord.Költséghely.Trim();

                    AdatTábla.Rows.Add(Soradat);
                }
                Ellen_Tábla.CleanFilterAndSort();
                Ellen_Tábla.DataSource = AdatTábla;

                Ellen_Tábla.Columns["Eszközszám"].Width = 140;
                Ellen_Tábla.Columns["Megnevezés"].Width = 400;
                Ellen_Tábla.Columns["Besorolás"].Width = 150;
                Ellen_Tábla.Columns["Épületben van"].Width = 150;
                Ellen_Tábla.Columns["Szerszámban van"].Width = 150;
                Ellen_Tábla.Columns["Költséghely"].Width = 150;

                Ellen_Tábla.Visible = true;
                Ellen_Tábla.Refresh();
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

        private void Ellenőriz()
        {
            try
            {
                if (Ellen_Tábla.Rows.Count < 1) throw new HibásBevittAdat("A táblázat nem tartalmaz ellenőrindő elemeket");

                if (!(Ellen_Besorolás.Text.Trim() == "Épület" || Ellen_Besorolás.Text.Trim() == "Szerszám"))
                    throw new HibásBevittAdat("Nincs kiválasztva adatbázis!");


                CikktörzsListaFeltöltés(Cmbtelephely.Text.Trim(), Ellen_Besorolás.Text.Trim());
                List<string> Adatok = (from a in AdatokCikk
                                       where a.Azonosító.Substring(0, 1) == "E"
                                       select a.Azonosító).ToList();

                Holtart.Be(100);

                //végigmegyünk a táblázaton és a kijelölt elemeket megvizsgáljuk
                List<Adat_Eszköz> AdatokGy = new List<Adat_Eszköz>();
                for (int j = 0; j < Ellen_Tábla.Rows.Count; j++)
                {

                    string Eszköz = Ellen_Tábla.Rows[j].Cells[0].Value.ToString().Trim();
                    string EEszköz = "E" + Eszköz;
                    bool Volt = Adatok.Contains(EEszköz);
                    if (Ellen_Besorolás.Text.Trim() == "Épület")
                    {
                        Adat_Eszköz ADAT = new Adat_Eszköz(
                        Eszköz,
                        Volt,
                        false);
                        AdatokGy.Add(ADAT);
                    }
                    else
                    {
                        Adat_Eszköz ADAT = new Adat_Eszköz(
                          Eszköz,
                          false,
                          Volt);
                        AdatokGy.Add(ADAT);
                    }
                    Holtart.Lép();
                }
                if (AdatokGy.Count > 0) KézEszk.MódosításDönt(Cmbtelephely.Text.Trim(), AdatokGy);
                Holtart.Ki();
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

        private void Ellen_Ellenőr_Click(object sender, EventArgs e)
        {
            try
            {
                Ellenőriz();
                MessageBox.Show("Az Ellenőrzés befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Besorol_Click(object sender, EventArgs e)
        {
            try
            {
                Holtart.Be();
                if (Ellen_Tábla.SelectedRows.Count < 1) throw new HibásBevittAdat("Nincs kijelölve egy eszköz sem.");
                if (Besorolás_Combo.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve egy besorolási hely sem.");

                List<Adat_Eszköz> Adatok = KézEszk.Lista_Adatok(Cmbtelephely.Text.Trim());

                List<Adat_Eszköz> AdatokGy = new List<Adat_Eszköz>();
                for (int j = 0; j < Ellen_Tábla.Rows.Count; j++)
                {
                    if (Ellen_Tábla.Rows[j].Selected == true)
                    {
                        string Eszköz = Ellen_Tábla.Rows[j].Cells[0].Value.ToString().Trim();

                        Adat_Eszköz vane = (from a in Adatok
                                            where a.Eszköz == Eszköz
                                            select a).FirstOrDefault();

                        if (vane != null)
                        {
                            Adat_Eszköz ADAT = new Adat_Eszköz(Eszköz, Besorolás_Combo.Text.Trim());
                            AdatokGy.Add(ADAT);
                        }
                    }
                    Holtart.Lép();
                }
                if (AdatokGy.Count > 0) KézEszk.MódosításBesorol(Cmbtelephely.Text.Trim(), AdatokGy);
                Holtart.Ki();
                Ellen_TáblaÍró();
                MessageBox.Show("A besorolások beállítása megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Át_Tölt_Click(object sender, EventArgs e)
        {
            try
            {
                Holtart.Be(100);
                List<Adat_Eszköz> EszkAdatok = KézEszk.Lista_Adatok(Cmbtelephely.Text.Trim());

                //végigmegyünk a táblázaton és a kijelölt elemeket megvizsgáljuk
                for (int j = 0; j < Ellen_Tábla.Rows.Count; j++)
                {
                    // ha ki van jelölve
                    if (Ellen_Tábla.Rows[j].Selected == true)
                    {
                        string Eszköz = Ellen_Tábla.Rows[j].Cells[0].Value.ToString().Trim();
                        //Betöltjük az egy eszközt és az adatai felhasználásával feltöltjük a épületbe, vagy a szerszámban
                        Adat_Eszköz EszkAdat = EszkAdatok.Where(a => a.Eszköz == Eszköz.Trim()).FirstOrDefault();
                        if (EszkAdat != null)
                        {
                            string Melyik_nyilvántartás = Ellen_Tábla.Rows[j].Cells[2].Value.ToString().Trim();

                            //Meghatározzuk, hogy hova kell menteni
                            string Melyik = "Szerszám";
                            if (Melyik_nyilvántartás == "Épület") Melyik = "Helység";

                            //ha egyiksem akkor nem rögzítünk
                            // cikk adatok
                            Cikklétrehozás(Cmbtelephely.Text.Trim(), Melyik, EszkAdat);
                            //könyv létrehozása
                            Könyvlétrehozás(Cmbtelephely.Text.Trim(), Melyik, EszkAdat);
                            //Könyvelés elkészítése
                            KönyvelésLétrehozása(Cmbtelephely.Text.Trim(), Melyik, EszkAdat);
                        }
                    }
                    Holtart.Lép();
                }
                Holtart.Ki();
                Ellenőriz();
                Ellen_TáblaÍró();
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
        #endregion


        #region Közös
        private void Excel_Kimenet(DataGridView Tábla, string fájlnévrész)
        {
            try
            {
                if (Tábla.Rows.Count <= 0) return;

                string fájlexc;
                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"{fájlnévrész}_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddhhmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyX.DataGridViewToXML(fájlexc, Tábla);
                MyF.Megnyitás(fájlexc);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

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


        #region ListákFeltöltése
        private void CikktörzsListaFeltöltés(string Melyik, string Telephely)
        {
            try
            {
                AdatokCikk.Clear();
                AdatokCikk = KézSzerszámCikk.Lista_Adatok(Melyik, Telephely);
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
