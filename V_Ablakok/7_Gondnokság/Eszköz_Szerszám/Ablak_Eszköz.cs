using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok
{
    public partial class Ablak_Eszköz : Form
    {
        bool VanKönyv = false;
        string KönyvSzám = "";

        readonly Kezelő_Szerszám_Könyv KézKönyv = new Kezelő_Szerszám_Könyv();
        readonly Kezelő_Szerszám_Cikk KézSzerszámCikk = new Kezelő_Szerszám_Cikk();
        readonly Kezelő_Eszköz KézEszk = new Kezelő_Eszköz();

        List<Adat_Szerszám_Cikktörzs> AdatokCikk = new List<Adat_Szerszám_Cikktörzs>();


        public Ablak_Eszköz()
        {
            InitializeComponent();
            Start();
        }

        private void Ablak_Eszköz_Load(object sender, EventArgs e)
        {
            // létrehozzuk a  könyvtárat
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Eszköz";
            if (!Directory.Exists(hely))
                Directory.CreateDirectory(hely);

            hely += $@"\Eszköz.mdb";
            if (!File.Exists(hely))
                Adatbázis_Létrehozás.Eszköztábla(hely);


        }

        void Start()
        {
            Telephelyekfeltöltése();
            Jogosultságkiosztás();
            Fülekkitöltése();
            Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;
        }


        #region Alap


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
            string hely = Application.StartupPath + @"\Súgó\VillamosLapok\eszköz.html";
            Module_Excel.Megnyitás(hely);
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
            var SelectedTab = Fülek.TabPages[e.Index];

            // Szerezze be a lap fejlécének területét
            var HeaderRect = Fülek.GetTabRect(e.Index);

            // Hozzon létreecsetet a szöveg megfestéséhez
            var BlackTextBrush = new SolidBrush(Color.Black);

            // Állítsa be a szöveg igazítását
            var sf = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };


            // Festse meg a szöveget a megfelelő félkövér és szín beállítással
            if ((e.State & DrawItemState.Selected) != 0)
            {
                var BoldFont = new Font(Fülek.Font.Name, Fülek.Font.Size, FontStyle.Bold);
                // háttér szín beállítása
                e.Graphics.FillRectangle(new SolidBrush(Color.DarkGray), e.Bounds);
                var paddedBounds = e.Bounds;
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
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = OpenFileDialog1.FileName;
                else
                {
                    SAP_adatok.Visible = true;
                    return;
                }

                MyE.ExcelMegnyitás(fájlexc);
                string munkalap = "Format";
                //  string munkalap = "RawData";

                MyE.Munkalap_aktív(munkalap);
                // ***********************************
                // ***** Ellenőrzés eleje ************
                // ***********************************
                // beolvassuk a fejlécet ha eltér a megadotttól, akkor kiírja és bezárja
                string fejlécbeolvas = "";
                for (int i = 1; i <= 29; i++)
                    fejlécbeolvas += MyE.Beolvas(MyE.Oszlopnév(i) + "1").Trim();

                if (MyF.Betöltéshelyes("Eszköz", fejlécbeolvas))
                {
                    MyE.ExcelBezárás();
                    throw new HibásBevittAdat("Nem megfelelő a betölteni kívánt adatok formátuma ! ");
                }
                // ***********************************
                // ***** Ellenőrzés vége  ************
                // ***********************************
                Holtart.Be(100);

                //Beolvasás
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Eszköz\Eszköz.mdb";
                string jelszó = "TóthKatalin";

                string szöveg = "SELECT * FROM Adatok ORDER BY eszköz";

                List<string> AdatokEszk = KézEszk.Lista_EszközNév(hely, jelszó, szöveg);


                string Eszköz, Alszám, Megnevezés, Megnevezés_folyt, Gyártási_szám, Leltárszám;
                DateTime Leltár_dátuma;
                double Mennyiség;
                string Bázis_menny_egység;
                DateTime Aktiválás_dátuma;
                string Telephely, Telephely_megnevezése, Helyiség, Helyiség_megnevezés, Gyár, Leltári_költséghely, Vonalkód;
                DateTime Leltár_forduló_nap;
                string Szemügyi_törzsszám, Dolgozó_neve;
                DateTime Deaktiválás_dátuma;
                string Eszközosztály, Üzletág, Cím, Költséghely, Felelős_költséghely, Régi_leltárszám;
                bool Vonalkódozható;
                string Rendszám_pályaszám;


                // Első adattól végig pörgetjük a beolvasást addig amíg nem lesz üres
                int sor = 2;

                List<string> SzövegGy = new List<string>();
                while (MyE.Beolvas($"A{sor}") != "_")
                {
                    //Elemek amiket mindenképpen beolvasunk
                    Eszköz = MyE.Beolvas($"A{sor}").Trim().Replace(",", "");
                    Megnevezés = MyE.Beolvas($"C{sor}").Trim().Replace(",", "").Replace("'", "");
                    double ideigdouble;
                    ideigdouble = double.TryParse(MyE.Beolvas($"H{sor}").Trim(), out ideigdouble) ? ideigdouble : 0;
                    Mennyiség = ideigdouble;

                    if (Mennyiség != 0)
                    {
                        if (AdatokEszk.Contains(Eszköz))
                        {
                            //ha van ilyen akkor módosítjuk?
                            Helyiség = MyE.Beolvas($"M{sor}").Trim().Replace(",", "");
                            Helyiség_megnevezés = MyE.Beolvas($"N{sor}").Trim().Replace(",", "");
                            Szemügyi_törzsszám = MyE.Beolvas($"S{sor}").Trim().Replace(",", "");
                            Dolgozó_neve = MyE.Beolvas($"T{sor}").Trim().Replace(",", "");
                            Költséghely = MyE.Beolvas($"Y{sor}".Trim()).Replace(",", "");
                            Felelős_költséghely = MyE.Beolvas($"Z{sor}".Trim()).Replace(",", "");
                            Leltárszám = MyE.Beolvas($"F{sor}").Trim().Replace(",", "");
                            if (Leltárszám.Trim() == "") Leltárszám = "_";
                            szöveg = "UPDATE Adatok SET ";
                            szöveg += $" Helyiség='{Helyiség.Trim()}', ";
                            szöveg += $" Helyiség_megnevezés='{Helyiség_megnevezés.Trim()}', ";
                            szöveg += $" Szemügyi_törzsszám='{Szemügyi_törzsszám.Trim()}', ";
                            szöveg += $" Dolgozó_neve='{Dolgozó_neve.Trim()}', ";
                            szöveg += $" Költséghely='{Költséghely.Trim()}', ";
                            szöveg += $" Leltárszám='{Leltárszám.Trim()}', ";
                            szöveg += $" Felelős_költséghely='{Felelős_költséghely.Trim()}' ";
                            szöveg += $"WHERE Eszköz='{Eszköz.Trim()}'";
                            SzövegGy.Add(szöveg);
                        }
                        else
                        {
                            //ha nincs akkor létrehozzuk
                            szöveg = "INSERT INTO Adatok (" +
                                "Eszköz, Alszám, Megnevezés, Megnevezés_folyt, Gyártási_szám," +
                                "Leltárszám, Leltár_dátuma, Mennyiség, Bázis_menny_egység, Aktiválás_dátuma," +
                                " Telephely, Telephely_megnevezése, Helyiség, Helyiség_megnevezés,  Gyár," +
                                " Leltári_költséghely, Vonalkód, Leltár_forduló_nap, Szemügyi_törzsszám, Dolgozó_neve," +
                                " Deaktiválás_dátuma, Eszközosztály, Üzletág, Cím, Költséghely, " +
                                "Felelős_költséghely, Régi_leltárszám, Vonalkódozható, Rendszám_pályaszám," +
                                " Épület_Szerszám, Épület_van, Szerszám_van, státus) VALUES ( ";
                            DateTime ideigdátum;

                            Alszám = MyE.Beolvas($"B{sor}").Trim().Replace(",", "");

                            Megnevezés_folyt = MyE.Beolvas($"D{sor}").Trim().Replace(",", "").Replace("'", "");
                            Gyártási_szám = MyE.Beolvas($"E{sor}").Trim().Replace(",", "");
                            Leltárszám = MyE.Beolvas($"F{sor}").Trim().Replace(",", "");
                            if (Leltárszám.Trim() == "") Leltárszám = "_";
                            ideigdátum = DateTime.TryParse(MyE.Beolvas($"G{sor}").Trim(), out ideigdátum) ? ideigdátum : new DateTime(1900, 1, 1);
                            Leltár_dátuma = ideigdátum;
                            Bázis_menny_egység = MyE.Beolvas($"I{sor}").Trim().Replace(",", "");
                            ideigdátum = DateTime.TryParse(MyE.Beolvas($"J{sor}").Trim(), out ideigdátum) ? ideigdátum : new DateTime(1900, 1, 1);
                            Aktiválás_dátuma = ideigdátum;
                            Telephely = MyE.Beolvas($"K{sor}").Trim().Replace(",", "");
                            Telephely_megnevezése = MyE.Beolvas($"L{sor}").Trim().Replace(",", "");
                            Helyiség = MyE.Beolvas($"M{sor}").Trim().Replace(",", "");
                            Helyiség_megnevezés = MyE.Beolvas($"N{sor}").Trim().Replace(",", "");
                            Gyár = MyE.Beolvas($"O{sor}").Trim().Replace(",", "");
                            Leltári_költséghely = MyE.Beolvas($"P{sor}").Trim().Replace(",", "");
                            Vonalkód = MyE.Beolvas($"Q{sor}").Trim().Replace(",", "");
                            ideigdátum = DateTime.TryParse(MyE.Beolvas($"R{sor}").Trim(), out ideigdátum) ? ideigdátum : new DateTime(1900, 1, 1);
                            Leltár_forduló_nap = ideigdátum;
                            Szemügyi_törzsszám = MyE.Beolvas($"S{sor}").Trim().Replace(",", "");
                            Dolgozó_neve = MyE.Beolvas($"T{sor}").Trim().Replace(",", "");
                            ideigdátum = DateTime.TryParse(MyE.Beolvas($"U{sor}").Trim(), out ideigdátum) ? ideigdátum : new DateTime(1900, 1, 1);
                            Deaktiválás_dátuma = ideigdátum;
                            Eszközosztály = MyE.Beolvas($"V{sor}".Trim()).Replace(",", "");
                            Üzletág = MyE.Beolvas($"W{sor}".Trim()).Replace(",", "");
                            Cím = MyE.Beolvas($"X{sor}".Trim()).Replace(",", "");
                            Költséghely = MyE.Beolvas($"Y{sor}".Trim()).Replace(",", "");
                            Felelős_költséghely = MyE.Beolvas($"Z{sor}".Trim()).Replace(",", "");
                            Régi_leltárszám = MyE.Beolvas($"AA{sor}".Trim()).Replace(",", "");
                            Vonalkódozható = MyE.Beolvas($"AB{sor}").Trim() != "NEM";
                            Rendszám_pályaszám = MyF.Szöveg_Tisztítás(MyE.Beolvas($"AC{sor}").Trim().Replace(",", ""), 0, 10);

                            szöveg += $"'{Eszköz}', '{Alszám}', '{Megnevezés}', '{Megnevezés_folyt}', '{Gyártási_szám}', ";
                            szöveg += $"'{Leltárszám}', '{Leltár_dátuma}', {Mennyiség.ToString().Replace(',', '.')}, '{Bázis_menny_egység}', '{Aktiválás_dátuma}', ";
                            szöveg += $"'{Telephely}', '{Telephely_megnevezése}', '{Helyiség}', '{Helyiség_megnevezés}', '{Gyár}', ";
                            szöveg += $"'{Leltári_költséghely}', '{Vonalkód}', '{Leltár_forduló_nap}', '{Szemügyi_törzsszám}', '{Dolgozó_neve}', ";
                            szöveg += $"'{Deaktiválás_dátuma}', '{Eszközosztály}', '{Üzletág}', '{Cím}', '{Költséghely}', ";
                            szöveg += $"'{Felelős_költséghely}', '{Régi_leltárszám}', {Vonalkódozható}, '{Rendszám_pályaszám}', ";
                            szöveg += "'Nincs beállítva', false, false, false)";

                            SzövegGy.Add(szöveg);

                            AdatokEszk.Add(Eszköz);
                        }
                    }
                    sor++;
                    Holtart.Lép();
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);

                Holtart.Ki();
                MyE.ExcelBezárás();

                File.Delete(fájlexc);
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
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Eszköz\Eszköz.mdb";
                string jelszó = "TóthKatalin";

                if (System.IO.File.Exists(hely) == false)
                    return;
                string szöveg = "SELECT * FROM Adatok ";
                if (Szűr_Hely.Text.Trim() != "" || Szűr_Megnevezés.Text.Trim() != "" || Szűr_Név.Text.Trim() != "" || Szűr_Osztás.Text.Trim() != "")
                {
                    szöveg += " WHERE ";
                    bool volt = false;
                    if (Szűr_Hely.Text.Trim() != "")
                    {
                        szöveg += $" Helyiség_megnevezés LIKE '%{Szűr_Hely.Text.Trim()}%'";
                        volt = true;
                    }
                    if (Szűr_Megnevezés.Text.Trim() != "")
                    {
                        if (volt) szöveg += " AND ";
                        szöveg += $" Megnevezés LIKE '%{Szűr_Megnevezés.Text.Trim()}%'";
                        volt = true;

                    }
                    if (Szűr_Név.Text.Trim() != "")
                    {
                        if (volt) szöveg += " AND ";
                        szöveg += $" Dolgozó_neve  LIKE '%{Szűr_Név.Text.Trim()}%'";
                        volt = true;
                    }
                    if (Szűr_Osztás.Text.Trim() != "")
                    {
                        if (volt) szöveg += " AND ";
                        szöveg += $" Épület_Szerszám  LIKE '%{Szűr_Osztás.Text.Trim()}%'";
                        volt = true;
                    }

                }

                szöveg += " ORDER BY eszköz";


                List<Adat_Eszköz> Adatok = KézEszk.Lista_Adatok(hely, jelszó, szöveg);

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


        void Cikklétrehozás(string hely, Adat_Eszköz EszkAdat)
        {
            // cikk adatok
            string jelszóúj = "csavarhúzó";

            string szöveg = $"SELECT * FROM Cikktörzs";
            Kezelő_Szerszám_Cikk KézSzerszámCikk = new Kezelő_Szerszám_Cikk();
            List<Adat_Szerszám_Cikktörzs> AAdat = KézSzerszámCikk.Lista_Adatok(hely, jelszóúj, szöveg);
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
                KézSzerszámCikk.Módosítás(hely, jelszóúj, Adat);
            else
                KézSzerszámCikk.Rögzítés(hely, jelszóúj, Adat);
        }



        void Könyvlétrehozás(string hely, Adat_Eszköz EszkAdat)
        {
            VanKönyv = false;
            KönyvSzám = "";
            Adat_Szerszám_Könyvtörzs AdatHely = null;
            string jelszóúj = "csavarhúzó";
            string szöveg;


            szöveg = "SELECT * FROM könyvtörzs";
            List<Adat_Szerszám_Könyvtörzs> Adatok = KézKönyv.Lista_Adatok(hely, jelszóúj, szöveg);

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
            if (AdatHely != null) KézKönyv.Rögzítés(hely, jelszóúj, AdatHely);
        }


        void KönyvelésLétrehozása(string hely, Adat_Eszköz eszkAdat)
        {
            string jelszóúj = "csavarhúzó";
            Kezelő_Szerszám_könvyvelés KézKönyvelés = new Kezelő_Szerszám_könvyvelés();

            if (VanKönyv)
            {
                string szöveg = $"SELECT * FROM könyvelés";
                string eszkoz = $"E{eszkAdat.Eszköz.Trim()}";

                List<Adat_Szerszám_Könyvelés> Adatok = KézKönyvelés.Lista_Adatok(hely, jelszóúj, szöveg);
                Adat_Szerszám_Könyvelés vane = (from a in Adatok
                                                where a.AzonosítóMás == eszkoz
                                                select a).FirstOrDefault();

                //Ha nincs a könyvelésben csak akkor rögzítjük
                if (vane == null)
                {
                    Adat_Szerszám_Cikktörzs Azonosító = new Adat_Szerszám_Cikktörzs(eszkoz, "");
                    Adat_Szerszám_Könyvtörzs Szerszámkönyvszám = new Adat_Szerszám_Könyvtörzs(KönyvSzám.Trim(), "");
                    Adat_Szerszám_Könyvelés Adat = new Adat_Szerszám_Könyvelés(Azonosító, Szerszámkönyvszám, eszkAdat.Mennyiség.ToÉrt_Int());
                    KézKönyvelés.Rögzítés(hely, jelszóúj, Adat);
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
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Eszköz\Eszköz.mdb";
                string jelszó = "TóthKatalin";

                if (!System.IO.File.Exists(hely)) return;

                string szöveg = "SELECT * FROM Adatok ";
                if (Ellen_Besorolás.Text.Trim() != "" || Ellen_Szűrő.Text.Trim() != "Nem vizsgál")
                {
                    szöveg += " WHERE ";
                    bool volt = false;
                    if (Ellen_Besorolás.Text.Trim() != "")
                    {
                        if (volt) szöveg += " AND ";
                        szöveg += $" Épület_Szerszám  LIKE '%{Ellen_Besorolás.Text.Trim()}%'";
                        volt = true;
                    }
                    if (Ellen_Szűrő.Text.Trim() != "")
                    {

                        switch (Ellen_Szűrő.Text.Trim())
                        {
                            case "Nem vizsgál":
                                break;
                            case "Csak épület":
                                if (volt) szöveg += " AND ";
                                szöveg += " Épület_van=true AND Szerszám_van=false ";
                                break;
                            case "Csak szerszám":
                                if (volt) szöveg += " AND ";
                                szöveg += " Épület_van=false AND Szerszám_van=true ";
                                break;
                            case "Mind kettő":
                                if (volt) szöveg += " AND ";
                                szöveg += " Épület_van=true AND Szerszám_van=true ";
                                break;
                            case "Egyik sem":
                                if (volt) szöveg += " AND ";
                                szöveg += " Épület_van=false AND Szerszám_van=false ";
                                break;
                        }
                    }
                }

                szöveg += " ORDER BY eszköz";


                List<Adat_Eszköz> Adatok = KézEszk.Lista_Adatok(hely, jelszó, szöveg);

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
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Eszköz\Eszköz.mdb";
                string jelszó = "TóthKatalin";

                if (Ellen_Tábla.Rows.Count < 1) throw new HibásBevittAdat("A táblázat nem tartalmaz ellenőrindő elemeket");
                string helySzersz = "";
                switch (Ellen_Besorolás.Text.Trim())
                {
                    case "Épület":
                        helySzersz = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Helység\Adatok\Szerszám.mdb";
                        break;
                    case "Szerszám":
                        helySzersz = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Szerszám\Adatok\Szerszám.mdb";
                        break;
                    default:
                        throw new HibásBevittAdat("Nincs kiválasztva adatbázis!");
                }

                CikktörzsListaFeltöltés(helySzersz);
                List<string> Adatok = (from a in AdatokCikk
                                       where a.Azonosító.Substring(0, 1) == "E"
                                       select a.Azonosító).ToList();

                Holtart.Be(100);

                //végigmegyünk a táblázaton és a kijelölt elemeket megvizsgáljuk

                List<string> SzövegGy = new List<string>();
                for (int j = 0; j < Ellen_Tábla.Rows.Count; j++)
                {
                    string szöveg;
                    string Eszköz = Ellen_Tábla.Rows[j].Cells[0].Value.ToString().Trim();
                    string EEszköz = "E" + Eszköz;
                    bool Volt = Adatok.Contains(EEszköz);
                    switch (Ellen_Besorolás.Text.Trim())
                    {
                        case "Épület":
                            szöveg = $"UPDATE Adatok SET Épület_van={Volt} WHERE eszköz='{Eszköz}' ";
                            SzövegGy.Add(szöveg);
                            break;
                        case "Szerszám":
                            szöveg = $"UPDATE Adatok SET Szerszám_van={Volt} WHERE eszköz='{Eszköz}' ";
                            SzövegGy.Add(szöveg);
                            break;
                    }
                    Holtart.Lép();
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
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

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Eszköz\Eszköz.mdb";
                string jelszó = "TóthKatalin";
                string szöveg = $"SELECT * FROM Adatok";

                List<Adat_Eszköz> Adatok = KézEszk.Lista_Adatok(hely, jelszó, szöveg);


                List<string> SzövegGy = new List<string>();
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
                            szöveg = $"UPDATE Adatok SET Épület_Szerszám='{Besorolás_Combo.Text.Trim()}' WHERE eszköz='{Eszköz}' ";
                            SzövegGy.Add(szöveg);
                        }
                    }
                    Holtart.Lép();
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
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
                //ha nincs kijelölve elem akkor kilép
                //if (Ellen_Tábla.SelectedRows.Count < 1)
                //    throw new HibásBevittAdat("Nincs kijelölve egy eszköz sem.");


                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Eszköz\Eszköz.mdb";
                string jelszó = "TóthKatalin";
                string szöveg;


                //végigmegyünk a táblázaton és a kijelölt elemeket megvizsgáljuk
                for (int j = 0; j < Ellen_Tábla.Rows.Count; j++)
                {
                    // ha ki van jelölve
                    if (Ellen_Tábla.Rows[j].Selected == true)
                    {
                        string Eszköz = Ellen_Tábla.Rows[j].Cells[0].Value.ToString().Trim();
                        szöveg = $"SELECT * FROM Adatok WHERE eszköz='{Eszköz.Trim()}'";
                        //Betöltjük az egy eszközt és az adatai felhasználásával feltöltjük a épületbe, vagy a szerszámban
                        Adat_Eszköz EszkAdat = KézEszk.Egy_Adat(hely, jelszó, szöveg);
                        if (EszkAdat != null)
                        {
                            string Melyik_nyilvántartás = Ellen_Tábla.Rows[j].Cells[2].Value.ToString().Trim();
                            string helyúj = "";
                            //Meghatározzuk, hogy hova kell menteni

                            if (Melyik_nyilvántartás == "Épület")
                            {
                                helyúj = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Helység\Adatok\Szerszám.mdb";
                            }
                            else if (Melyik_nyilvántartás == "Szerszám")
                            {
                                helyúj = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Szerszám\Adatok\Szerszám.mdb";
                            }
                            //ha egyiksem akkor nem rögzítünk
                            if (helyúj.Trim() != "")
                            {
                                if (!File.Exists(helyúj))
                                    Adatbázis_Létrehozás.Szerszám_nyilvántartás(helyúj);

                                // cikk adatok
                                Cikklétrehozás(helyúj, EszkAdat);
                                //könyv létrehozása
                                Könyvlétrehozás(helyúj, EszkAdat);
                                //Könyvelés elkészítése
                                KönyvelésLétrehozása(helyúj, EszkAdat);
                            }
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
        void Excel_Kimenet(DataGridView Tábla, string fájlnévrész)
        {
            if (Tábla.Rows.Count <= 0)
                return;

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

            fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
            MyE.EXCELtábla(fájlexc, Tábla, true);
            MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Module_Excel.Megnyitás(fájlexc + ".xlsx");
        }



        #endregion

        #region ListákFeltöltése
        private void CikktörzsListaFeltöltés(string hely)
        {
            try
            {
                AdatokCikk.Clear();
                string szöveg = "SELECT * FROM cikktörzs ORDER BY azonosító";
                string jelszó = "csavarhúzó";
                AdatokCikk = KézSzerszámCikk.Lista_Adatok(hely, jelszó, szöveg);
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
