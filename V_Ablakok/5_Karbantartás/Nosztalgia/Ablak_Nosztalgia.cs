using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos.Kezelők;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using Villamos.Villamos_Kezelők;
using static System.IO.File;
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok
{
    public partial class Ablak_Nosztalgia : Form
    {

        //!
        //felvenni barossi kocsit -- 1233 1820 2806 3720 5005 5884ű
        // Dócs Endrével beszélni, valós zser adatok lekéréséről
        //!

        #region Kezelők - Listák

        Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        Kezelő_Nosztalgia_Állomány KézÁllomány = new Kezelő_Nosztalgia_Állomány();
        Kezelő_Ciklus KézCiklus = new Kezelő_Ciklus();

        List<Adat_Jármű> AdatokJármű = new List<Adat_Jármű>();
        List<Adat_Nosztalgia_Állomány> AdatokÁllomány = new List<Adat_Nosztalgia_Állomány>();
        List<Adat_Ciklus> AdatokCiklus = new List<Adat_Ciklus>();

        private void ListaVillamos()
        {
            string hely = Application.StartupPath + @"\" + Cmbtelephely.Text + @"\adatok\villamos\villamos.mdb";
            string jelszó = "pozsgaii";
            string szöveg = $"SELECT * FROM állománytábla";
            AdatokJármű?.Clear();
            AdatokJármű = KézJármű.Lista_Adatok(hely, jelszó, szöveg);
        }
        private void ListaFutásNapNoszt()
        {
            string hely = Application.StartupPath + @"\Főmérnökség\Adatok\Nosztalgia\FutásnapNoszt.mdb";
            string jelszó = "kloczkal";
            string szöveg = $"SELECT * FROM Állomány";
            AdatokÁllomány?.Clear();
            AdatokÁllomány = KézÁllomány.Lista_Adat(hely, jelszó, szöveg);
        }
        private void ListaCiklus()
        {
            string hely = Application.StartupPath + @"\Főmérnökség\Adatok\Ciklus.mdb";
            string jelszó = "pocsaierzsi";
            string szöveg = $"SELECT * FROM Ciklusrendtábla";
            AdatokCiklus?.Clear();
            AdatokCiklus = KézCiklus.Lista_Adatok(hely, jelszó, szöveg);
        }
        private void ListaFeltöltés()
        {
            ListaVillamos();
            ListaFutásNapNoszt();
            ListaCiklus();
        }
        #endregion

        #region Ablak
        public Ablak_Nosztalgia()
        {
            InitializeComponent();
        }
        private void Ablak_Nosztalgia_Load(object sender, EventArgs e)
        {
            Telephelyekfeltöltése();

            Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;
            Fülek.SelectedIndex = 0;

            // létrehozzuk a  könyvtárat
            string hely = Application.StartupPath + @"\Főmérnökség\Adatok\Nosztalgia";
            if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

            hely = Application.StartupPath + @"\Főmérnökség\Adatok\Nosztalgia\VillamosNoszt.mdb";
            if (!Exists(hely)) Adatbázis_Létrehozás.NosztTábla(hely);

            hely = Application.StartupPath + @"\Főmérnökség\Adatok\Nosztalgia\FutásnapNoszt.mdb";
            if (!Exists(hely)) Adatbázis_Létrehozás.Futásnaptábla_Nosztalgia(hely);

            hely = Application.StartupPath + $@"\Főmérnökség\Adatok\Nosztalgia\Futás_{DateTime.Today.Year}.mdb";
            if (!Exists(hely)) Adatbázis_Létrehozás.NosztFutás(hely);

            Pályaszám_feltöltés();

            HibaVizsgálat();

            hely = Application.StartupPath + @"\Főmérnökség\Adatok\Nosztalgia\Kép";
            if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

            hely = Application.StartupPath + @"\Főmérnökség\Adatok\Nosztalgia\Pdf";
            if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

            Fülek.SelectedIndex = 0;
            Fülekkitöltése();
            Jogosultságkiosztás();
            Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;
        }
        #endregion

        #region Alap
        private void HibaVizsgálat()
        {
            try
            {

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
                Cmbtelephely.Items.Clear();
                Cmbtelephely.Items.AddRange(Listák.TelephelyLista_Jármű());
                if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér) Cmbtelephely.Text = Cmbtelephely.Items[0].ToStrTrim();
                else Cmbtelephely.Text = Program.PostásTelephely;
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
        private void Pályaszám_feltöltés()
        {
            try
            {
                Pályaszám.Items.Clear();

                string hely = Application.StartupPath + @"\Főmérnökség\Adatok\Nosztalgia\FutásnapNoszt.mdb";
                string jelszó = "kloczkal";
                string szöveg = $"SELECT * FROM Állomány";

                Pályaszám.BeginUpdate();
                Pályaszám.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "azonosító").ToArray());
                Pályaszám.EndUpdate();
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
            switch (Fülek.SelectedIndex)
            {
                case 0:
                    {
                        Kiirjaalapadatokat();
                        break;
                    }
                case 1: break;

                case 2: break;

                case 3: break;
            }
        }
        private void Jogosultságkiosztás()
        {
            //int melyikelem;

            //// ide kell az összes gombot tenni amit szabályozni akarunk false
            //alapadatRögzít.Enabled = false;
            //melyikelem = 125;
            //// módosítás 1 
            //if (MyF.Vanjoga(melyikelem, 1))
            //{
            //    alapadatRögzít.Enabled = true;
            //}
        }
        private void Fülek_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Határozza meg, hogy melyik lap van jelenleg kiválasztva
            var SelectedTab = Fülek.TabPages[e.Index];

            // Szerezze be a lap fejlécének területét
            var HeaderRect = Fülek.GetTabRect(e.Index);

            // Hozzon létreecsetet a szöveg megfestéséhez
            SolidBrush BlackTextBrush = new SolidBrush(Color.Black);

            // Állítsa be a szöveg igazítását
            StringFormat sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

            // Festse meg a szöveget a megfelelő félkövér és szín beállítással
            if ((e.State & DrawItemState.Selected) != 0)
            {
                Font BoldFont = new Font(Fülek.Font.Name, Fülek.Font.Size, FontStyle.Bold);
                // háttér szín beállítása
                e.Graphics.FillRectangle(new SolidBrush(Color.DarkGray), e.Bounds);
                var paddedBounds = e.Bounds;
                paddedBounds.Inflate(0, 0);
                e.Graphics.DrawString(SelectedTab.Text, BoldFont, BlackTextBrush, paddedBounds, sf);
            }
            else e.Graphics.DrawString(SelectedTab.Text, e.Font, BlackTextBrush, HeaderRect, sf);

            // Munka kész – dobja ki a keféket
            BlackTextBrush.Dispose();
        }
        private void Pályaszámkereső_Click(object sender, EventArgs e)
        {
            Frissít();
        }
        private void Frissít()
        {
            try
            {
                if (Pályaszám.Text.Trim() == "") return;

                switch (Fülek.SelectedIndex)
                {
                    case 0: Kiirjaalapadatokat(); break;
                    case 1: break;
                    case 4: break;
                    case 5: break;
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
        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Nosztalgia.html";
                MyE.Megnyitás(hely);
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
        #endregion

        #region Alapadatok lapfül
        private void Kiirjaalapadatokat()
        {
            if (Cmbtelephely.Text.Trim() == "") return;
            if (Pályaszám.Text.Trim() == "") return;

            ListaFeltöltés();

            // ürítjük a mezőket
            {
                Típus_text.Text = "";
                Státus_text.Text = "";
                Miótaáll_text.Text = "";
                Takarítás_text.Text = "";
                Főmérnökség_text.Text = "";
                Járműtípus_text.Text = "";
            }

            Adat_Jármű KiválKocsi = (from a in AdatokJármű
                                     where a.Üzem.Trim() == Cmbtelephely.Text.Trim()
                                     && a.Azonosító.Trim() == Pályaszám.Text.Trim()
                                     select a).FirstOrDefault();

            Adat_Nosztalgia_Állomány KiválKocsi1 = (from a in AdatokÁllomány
                                                    where a.Azonosító.Trim() == Pályaszám.Text.Trim()
                                                    select a).FirstOrDefault();

            if (KiválKocsi == null) MessageBox.Show("Az adatbázisban nem található a pályaszám!", "Figyelmeztetés!");
            else
            {


                Járműtípus_text.Text = KiválKocsi.Valóstípus2.Trim();
                Főmérnökség_text.Text = KiválKocsi.Valóstípus.Trim();
                switch (KiválKocsi.Státus)
                {
                    case 0: Státus_text.Text = "Nincs hibája"; break;
                    case 1: Státus_text.Text = "Szabad"; break;
                    case 2: Státus_text.Text = "Beállóba kért"; break;
                    case 3: Státus_text.Text = "Beállóba adott"; break;
                    case 4: Státus_text.Text = "Benn maradó"; break;
                }
                if (KiválKocsi.Miótaáll == null) Miótaáll_text.Text = "";
                else Miótaáll_text.Text = KiválKocsi.Miótaáll.ToShortDateString();
                TárH_text.Text = KiválKocsi.Üzem.Trim();
            }

            if (KiválKocsi1 == null) MessageBox.Show("Az adatbázisban nem található a pályaszám!", "Figyelmeztetés!");
            else
            {


                Típus_text.Text = KiválKocsi1.Ntípus.Trim();
                Gyártó_text.Text = KiválKocsi1.Gyártó.Trim();
                Év_text.Text = KiválKocsi1.Év.ToString();
                EszkSz_text.Text = KiválKocsi1.Eszközszám.Trim();
                LeltSz_text.Text = KiválKocsi1.Leltári_szám.Trim();
                ut_forg_text.Text = KiválKocsi1.Utolsóforgalminap.ToString().Trim();
                Fut_dátum.Value = KiválKocsi1.Vizsgálatdátuma_idő;
                if (KiválKocsi1.Vizsgálatfokozata.Contains("V")) Cmb_FutCiklusE.Text = "-";
                else Cmb_FutCiklusE.Text = KiválKocsi1.Vizsgálatfokozata.Trim();
                Fut_sorszám.Text = KiválKocsi1.Vizsgálatszáma_idő.Trim();
                Txt_V1_dátum.Value = KiválKocsi1.Vizsgálatdátuma_km;
                Cmb_KmCiklus_V1.Text = KiválKocsi1.Vizsgálatfokozata.Trim();
                Txt_V1_sorszám.Text = KiválKocsi1.Vizsgálatszáma_km.Trim();
                Txt_V1_Kmv.Text = KiválKocsi1.Km_v.ToString().Trim();
                Txt_V1_Kmu.Text = KiválKocsi1.Km_u.ToString().Trim();
                Txt_V2_dátum.Value = KiválKocsi1.Vizsgálatdátuma_km;
                Cmb_KmCiklus_V2.Text = KiválKocsi1.Vizsgálatfokozata.Trim();
                Txt_V2_sorszám.Text = KiválKocsi1.Vizsgálatszáma_km.Trim();
                Txt_V2_Kmv.Text = KiválKocsi1.Km_v.ToString().Trim();
                Txt_V2_Kmu.Text = KiválKocsi1.Km_u.ToString().Trim();
            }

            AdatokCiklus = (from a in AdatokCiklus
                            where a.Típus.Contains("Noszt")
                            orderby a.Sorszám ascending
                            select a).ToList();

            Cmb_FutCiklusE.Items.Clear();
            Cmb_KmCiklus_V1.Items.Clear();
            Cmb_KmCiklus_V2.Items.Clear();
            Cmb_FutCiklusE_Cnév.Items.Add("Noszt_idő");
            Cmb_KmCiklus_V1_Cnév.Items.Add("Noszt_km");
            Cmb_KmCiklus_V1_Cnév.Items.Add("Noszt_km+i");
            Cmb_KmCiklus_V2_Cnév.Items.Add("Noszt_km+i");
            Cmb_KmCiklus_V2_Cnév.Items.Add("Noszt_km");

            foreach (Adat_Ciklus rekord in AdatokCiklus)
            {
                if (rekord.Típus.Trim() == "Noszt_idő") Cmb_FutCiklusE.Items.Add(rekord.Vizsgálatfok);
                else if (rekord.Típus.Trim() == "Noszt_km") Cmb_KmCiklus_V1.Items.Add(rekord.Vizsgálatfok);
                else Cmb_KmCiklus_V2.Items.Add(rekord.Vizsgálatfok);
            }

            string hely = Application.StartupPath + @"\" + Cmbtelephely.Text + @"\adatok\villamos\villamos2.mdb"; //takarítás dátumok
            string jelszó = "pozsgaii";
            string szöveg = $"SELECT * FROM állománytábla";

            Kezelő_Jármű2 KJAdat_2 = new Kezelő_Jármű2();
            List<Adat_Jármű_2> JAdatok_2 = KJAdat_2.Lista_Adatok(hely, jelszó, szöveg);
            Adat_Jármű_2 TakDátum = (from a in JAdatok_2
                                     where a.Azonosító.Trim() == Pályaszám.Text.Trim()
                                     select a).FirstOrDefault();

            Takarítás_text.Text = TakDátum.Takarítás.ToStrTrim();
        }
        private void Pályaszám_SelectedIndexChanged(object sender, EventArgs e)
        {
            Kiirjaalapadatokat();

            if (Fülek.SelectedIndex == 3) PDF_azonísító_választó();

            if (Fülek.SelectedIndex == 2) Kép_azonísító_választó();
        }
        private void AlapadatRögzít_Click(object sender, EventArgs e)
        {
            try
            {
                string hibaszöveg = "";
                if (Év_text.Text.Trim() == string.Empty) Év_text.Text = "1900";
                if (!int.TryParse(Év_text.Text.Trim(), out int év)) hibaszöveg += "Az év mezőnek egész számnak kell lennie.\n";
                if (Pályaszám.Text.Trim() == string.Empty) hibaszöveg += "A pályaszám megadása kötelező.\n";
                if (Gyártó_text.Text.Trim().Length > 11) hibaszöveg += "A gyártó maximum 10 karakter lehet.\n";
                if (hibaszöveg.Trim() != string.Empty) throw new HibásBevittAdat(hibaszöveg);

                // leellenőrizzük, hogy létezik-e a kocsi
                string hely = Application.StartupPath + @"\Főmérnökség\Adatok\Nosztalgia\FutásnapNoszt.mdb";
                string jelszó = "kloczkal";
                string szöveg = "";

                ListaFutásNapNoszt();
                AdatokÁllomány = (from a in AdatokÁllomány
                                  where a.Azonosító.Trim() == Pályaszám.Text.Trim()
                                  select a).ToList();

                if (AdatokÁllomány.Count() != 0)
                {
                    // módosítás
                    szöveg = "UPDATE Állomány  SET ";
                    szöveg += "gyártó='" + Gyártó_text.Text.Trim() + "', ";
                    szöveg += "év=" + év + ", ";
                    szöveg += "Ntípus='" + Típus_text.Text.Trim() + "', ";
                    szöveg += "eszközszám='" + EszkSz_text.Text.Trim() + "', ";
                    szöveg += "leltári_szám='" + LeltSz_text.Text.Trim() + "' ";
                    szöveg += " WHERE azonosító='" + Pályaszám.Text.Trim() + "'";
                }
                else
                {
                    // új adat
                    szöveg = "INSERT INTO Állomány (azonosító, gyártó, év, Ntípus, eszközszám, leltári_szám, " +
                        "vizsgálatdátuma, vizsgálatfokozata, vizsgálatszáma, utolsóforgalminap, futásnap, km_v, km_u, utolsórögzítés, telephely" +
                        ") VALUES (";
                    szöveg += $"'{Pályaszám.Text.Trim()}', '{Gyártó_text.Text.Trim()}', {év}, '{Típus_text.Text.Trim()}', '{EszkSz_text.Text.Trim()}', '{LeltSz_text.Text.Trim()}',";
                    szöveg += "'1900.01.01', '', '', '1900.01.01', 0, 0, 0, '1900.01.01', '' )";
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);

                Típus_text.Text = "";
                Gyártó_text.Text = "";
                Év_text.Text = "";
                EszkSz_text.Text = "";
                LeltSz_text.Text = "";
                TárH_text.Text = "";
                Státus_text.Text = "";
                Miótaáll_text.Text = "";
                Takarítás_text.Text = "";
                Főmérnökség_text.Text = "";
                Járműtípus_text.Text = "";
                ut_forg_text.Text = "";
                Fut_nap_text.Text = "";

                Pályaszám_feltöltés();

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
        private void Futás_Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                string hibaszöveg = "";
                if (Pályaszám.Text.Trim() == string.Empty)
                {
                    hibaszöveg += "A pályaszám megadása kötelező.\n";
                    throw new HibásBevittAdat(hibaszöveg);
                }

                // leellenőrizzük, hogy létezik-e a kocsi
                string hely = Application.StartupPath + @"\Főmérnökség\Adatok\Nosztalgia\FutásnapNoszt.mdb";
                string jelszó = "kloczkal";
                string szöveg = "";

                ListaFutásNapNoszt();
                AdatokÁllomány = (from a in AdatokÁllomány
                                  where a.Azonosító.Trim() == Pályaszám.Text.Trim()
                                  select a).ToList();


                if (AdatokÁllomány.Count()!=0)
                {
                    // módosítás
                    szöveg = "UPDATE Állomány  SET ";
                    szöveg += $"vizsgálatdátuma=' {Fut_dátum.Value:yyyy.MM.dd}', ";
                    szöveg += $"vizsgálatfokozata= '{Cmb_FutCiklusE.Text.Trim()}', ";
                    szöveg += $"vizsgálatszáma= '{Fut_sorszám.Text.Trim()}',  ";
                    szöveg += $"utolsórögzítés= '{DateTime.Now:yyyy.MM.dd}'  ";
                    szöveg += " WHERE azonosító='" + Pályaszám.Text.Trim() + "'";
                }
                else
                {
                    // új adat
                    szöveg = "INSERT INTO Állomány (azonosító, gyártó, év, Ntípus, eszközszám, leltári_szám ) VALUES (";
                    szöveg += $"'{Pályaszám.Text.Trim()} ', '{Gyártó_text.Text.Trim()}', '{Év_text.Text.Trim()}', '{Típus_text.Text.Trim()}', '{EszkSz_text.Text.Trim()}', '{LeltSz_text.Text.Trim()}')";
                }
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
        private void Km_Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                string hibaszöveg = "";
                if (Pályaszám.Text.Trim() == string.Empty)
                {
                    hibaszöveg += "A pályaszám megadása kötelező.\n";
                    throw new HibásBevittAdat(hibaszöveg);
                }

                // leellenőrizzük, hogy létezik-e a kocsi
                string hely = Application.StartupPath + @"\Főmérnökség\Adatok\Nosztalgia\FutásnapNoszt.mdb";
                string jelszó = "kloczkal";
                string szöveg = "";

                ListaFutásNapNoszt();
                AdatokÁllomány = (from a in AdatokÁllomány
                                  where a.Azonosító.Trim() == Pályaszám.Text.Trim()
                                  select a).ToList();

                if (AdatokÁllomány.Count()!=0)
                {
                    // módosítás
                    szöveg = "UPDATE Állomány  SET ";
                    szöveg += $"vizsgálatdátuma=' {Txt_V1_dátum.Value:yyyy.MM.dd}', ";
                    szöveg += $"vizsgálatfokozata= '{Cmb_KmCiklus_V1.Text.Trim()}', ";
                    szöveg += $"vizsgálatszáma= '{Txt_V1_sorszám.Text.Trim()}',  ";
                    szöveg += $"km_v= '{Txt_V1_Kmv.Text.Trim()}',  ";
                    szöveg += $"km_u= '{Txt_V1_Kmu.Text.Trim()}',  ";
                    szöveg += $"utolsórögzítés= '{DateTime.Now:yyyy.MM.dd}'  ";
                    szöveg += " WHERE azonosító='" + Pályaszám.Text.Trim() + "'";
                }
                else
                {
                    // új adat
                    szöveg = "INSERT INTO Állomány (azonosító, gyártó, év, Ntípus, eszközszám, leltári_szám ) VALUES (";
                    szöveg += $"'{Pályaszám.Text.Trim()} ', '{Gyártó_text.Text.Trim()}', '{Év_text.Text.Trim()}', '{Típus_text.Text.Trim()}', '{EszkSz_text.Text.Trim()}', '{LeltSz_text.Text.Trim()}')";

                }
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
        #endregion

        #region Lekérdezések
        private void Napi_Adatok_rögzítése_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + $@"\Főmérnökség\Adatok\Nosztalgia\Futás_{Dátum.Value.Year}.mdb";
                string jelszó = "kloczkal";

                //le kell ellenőrizni, hogy van e olyan pályaszám!
                string azon = Tábla_lekérdezés.CurrentRow?.Cells[0].Value?.ToString();
                if (azon == null) { MessageBox.Show("Nincs kiválasztott kocsi", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                DateTime celldate = Tábla_lekérdezés.CurrentRow.Cells[1].Value.ToÉrt_DaTeTime();

                string szöveg = $"SELECT * FROM Futás WHERE azonosító='{Nap_azonosító.Text.Trim()}' AND dátum=#{celldate:yyyy.MM.dd}#";
                if (MyA.ABvanilyen(hely, jelszó, szöveg))
                {
                    szöveg = "UPDATE Futás SET ";
                    szöveg += $" dátum='{Nap_Dátum.Value:yyyy.MM.dd}', ";
                    if (Nap_törlés.Checked) szöveg += "státusz=true, ";
                    else szöveg += "státusz=false, ";
                    szöveg += $" mikor='{DateTime.Now}', ";
                    szöveg += $" ki='{Program.PostásNév.Trim()}', ";
                    szöveg += $" telephely='{Cmbtelephely.Text.Trim()}' ";
                    szöveg += $" WHERE azonosító = '{Nap_azonosító.Text.Trim()}' AND dátum=#{celldate:yyyy.MM.dd}#";
                }
                else
                {
                    szöveg = "INSERT INTO futás (azonosító, dátum, státusz, mikor, ki, telephely)  VALUES (";
                    szöveg += $"'{Nap_azonosító.Text.Trim()}', ";
                    szöveg += $"'{Nap_Dátum.Value:yyyy.MM.dd}', ";
                    if (Nap_törlés.Checked == true) szöveg += " true, ";
                    else szöveg += " false, ";
                    szöveg += $"'{DateTime.Now.ToString()}', ";
                    szöveg += $"'{Program.PostásNév.Trim()}', ";
                    szöveg += $"'{Cmbtelephely.Text.Trim()}') ";
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);
                MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Lekérdezés_lekérdezés_listázás();
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
        private void Lekérdezés_lekérdezés_listázás()
        {
            try
            {
                DataSet ds = AccessDbLoader.LoadFromFile(Application.StartupPath + $@"\Főmérnökség\Adatok\Nosztalgia\Futás_{Dátum.Value.Year}.mdb");
                Tábla_lekérdezés.DataSource = ds.Tables[0];
                Tábla_lekérdezés.Columns[0].HeaderText = "Pályaszám";
                Tábla_lekérdezés.Columns[0].Width = 120;
                Tábla_lekérdezés.Columns[1].HeaderText = "Futás dátuma";
                Tábla_lekérdezés.Columns[1].Width = 120;
                Tábla_lekérdezés.Columns[2].HeaderText = "Törölt";
                Tábla_lekérdezés.Columns[2].Width = 120;
                Tábla_lekérdezés.Columns[3].HeaderText = "Rögzítés";
                Tábla_lekérdezés.Columns[3].Width = 200;
                Tábla_lekérdezés.Columns[4].HeaderText = "Rögzítő";
                Tábla_lekérdezés.Columns[4].Width = 120;
                Tábla_lekérdezés.Columns[5].HeaderText = "Telephely";
                Tábla_lekérdezés.Columns[5].Width = 120;
                Tábla_lekérdezés.Refresh();
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
            Lekérdezés_lekérdezés_listázás();
            Futásnaptábla_Rögzítés.Enabled = true;
        }

        void ZSER_Beolvasás()
        {
            try
            {
                //// megnézzük, hogy létezik-e adott új helyen napi tábla
                string hely = Application.StartupPath + @"\Főmérnökség\Adatok\Nosztalgia\Futás_" + DateTime.Today.ToString("yyyy") + ".mdb";

                // megpróbáljuk megnyitni az excel táblát.
                openFileDialog1.InitialDirectory = "MyDocuments";
                openFileDialog1.Title = "SAP-s Adatok betöltése";
                openFileDialog1.FileName = "";
                openFileDialog1.Filter = "Excel |*.xlsx";
                string fájlexc;
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (openFileDialog1.ShowDialog() != DialogResult.Cancel) fájlexc = openFileDialog1.FileName;
                else return;

                // megnyitjuk a beolvasandó táblát
                MyE.ExcelMegnyitás(fájlexc);

                // megnézzük, hogy hány sorból áll a tábla
                int i = 1;
                int utolsó = 0;
                while (MyE.Beolvas("a" + i.ToString()) != "_") { utolsó = i; i += 1; }
                Holtart.Maximum = utolsó;
                Holtart.Visible = true;
                Holtart.Value = 1;
                string jelszó = "kloczkal";
                string szöveg;
                if (utolsó > 1)
                {
                    i = 2;
                    List<string> lista = new List<string>();
                    while (utolsó + 1 != i)
                    {
                        int KocsiSz = MyE.Beolvas("o" + i).ToÉrt_Int();
                        string[] kocsicell = { "u", "w", "y", "aa", "ac" };
                        szöveg = "INSERT INTO Futás (azonosító, dátum, státusz,mikor, ki, telephely )  VALUES (";

                        if (KocsiSz == 1) szöveg += $"'{MyE.Beolvas("s" + i.ToString()).Substring(1).Trim()} ', "; //kocsi          
                        else
                        {
                            //ezzel lehet a hiba
                            int cellid = 0;
                            while (KocsiSz > 1)
                            {
                                szöveg += "'" + MyE.Beolvas(kocsicell[cellid] + i.ToString()).Substring(1).Trim() + "', ";
                                KocsiSz--;
                                cellid++;
                            }
                        }

                        szöveg += "'" + MyE.BeolvasDátum("d" + i.ToString()).ToString("yyyy.MM.dd").Trim() + "', "; //indulás
                        if (MyE.Beolvas("n" + i.ToString()).Trim() == string.Empty) szöveg += "'0', "; //nem törölt
                        else szöveg += "'-1', "; // törölt
                        szöveg += "'" + DateTime.Now.ToString().Trim() + "',"; //mikor
                        szöveg += "'" + Program.PostásNév.Trim() + "' ,"; //ki
                        switch (MyE.Beolvas("a" + i))
                        {
                            case "VZA": szöveg += "'Angyalföld' )"; break;
                            case "VZB": szöveg += "'Baross' )"; break;
                            case "VKU": szöveg += "'Budafok' )"; break;
                            case "VSF": szöveg += "'Ferencváros' )"; break;
                            case "VF": szöveg += "'Fogaskerekű' )"; break;
                            case "VSH": szöveg += "'Hungária' )"; break;
                            case "VKK": szöveg += "'Kelenföld' )"; break;
                            case "VSS": szöveg += "'Száva' )"; break;
                            case "VKI": szöveg += "'Szépilona' )"; break;
                            case "VZZ": szöveg += "'Zugló' )"; break;
                        }
                        lista.Add(szöveg);
                        Holtart.Value++;
                        i++;
                        if (Holtart.Value >= Holtart.Maximum) Holtart.Value = 1;
                    }

                    MyA.ABMódosítás(hely, jelszó, lista);
                }
                // az excel tábla bezárása
                MyE.ExcelBezárás();
                Holtart.Visible = false;
                // kitöröljük a betöltött fájlt
                //if (File.Exists(fájlexc) == true)
                //    File.Delete(fájlexc);


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
        private void SAP_Beolv_Click(object sender, EventArgs e)
        {
            ZSER_Beolvasás();
            Pályaszám_feltöltés();

            //új metódus 
        }
        private void RögzítőbeAdatok()
        {
            Nap_azonosító.Text = Tábla_lekérdezés.CurrentRow.Cells[0].Value.ToString();
            Nap_Dátum.Text = Tábla_lekérdezés.CurrentRow.Cells[1].Value.ToString();
            if (Tábla_lekérdezés.CurrentRow.Cells[2].Value.ToÉrt_Bool()) Nap_törlés.Checked = true;
            else Nap_törlés.Checked = false;
            Nap_Telephely.Text = Tábla_lekérdezés.CurrentRow.Cells[5].Value.ToString();
        }
        private void Tábla_lekérdezés_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RögzítőbeAdatok();
        }
        private void Futásnaptábla_Rögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + $@"\Főmérnökség\Adatok\Nosztalgia\FutásnapNoszt.mdb";
                string jelszó = "kloczkal";
                DateTime alap = DateTime.Parse("1900.01.01");
                int nap = 0;

                for (int i = 0; i < Tábla_lekérdezés.RowCount; i++)
                {
                    string azon = Tábla_lekérdezés.Rows[i].Cells[0].Value?.ToString();
                    if (azon == null) { MessageBox.Show("Nincs kiválasztott kocsi", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                    DateTime forgNap = Tábla_lekérdezés.Rows[i].Cells[1].Value.ToÉrt_DaTeTime();
                    string szöveg = $"SELECT * FROM Állomány WHERE azonosító='{azon.Trim()}'";
                    if (MyA.ABvanilyen(hely, jelszó, szöveg))
                    {
                        DateTime forgnapElőző = alap;
                        if (i > 1) forgnapElőző = Tábla_lekérdezés.Rows[i - 1].Cells[1].Value.ToÉrt_DaTeTime();

                        szöveg = "UPDATE Állomány SET ";
                        szöveg += $"utolsórögzítés='{DateTime.Now}', ";
                        if (forgNap > forgnapElőző) szöveg += $"utolsóforgalminap='{forgNap}', ";
                        else if (forgNap < forgnapElőző) szöveg += $"utolsóforgalminap='{forgnapElőző}', ";
                        nap++;
                        szöveg += $"futásnap='{nap}', ";
                        szöveg += "telephely='" + Cmbtelephely.Text.Trim() + "'";
                        szöveg += $"WHERE azonosító ='{azon.Trim()}'";
                    }
                    else
                    {
                        nap = 1;
                        szöveg = "INSERT INTO Állomány (azonosító, utolsórögzítés, vizsgálatdátuma, utolsóforgalminap, vizsgálatfokozata, vizsgálatszáma, futásnap, telephely)  VALUES (";
                        szöveg += $"'{azon.Trim()}', ";
                        szöveg += "'" + DateTime.Now.ToString() + "', ";
                        szöveg += $" '{alap}', ";
                        szöveg += $"'{forgNap}', ";
                        szöveg += "'-', ";
                        szöveg += "'-', ";
                        szöveg += $"'{nap}', ";
                        szöveg += "'" + Cmbtelephely.Text.Trim() + "' )";
                    }
                    MyA.ABMódosítás(hely, jelszó, szöveg);
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

        #region KÉP

        private void Kép_Listázás_Click(object sender, EventArgs e)
        {
            Kép_azonísító_választó();
            Kép_lista_szűrés();
        }

        private void Kép_azonísító_választó()
        {
            try
            {
                if (Pályaszám.Text.Trim() == "")
                    throw new HibásBevittAdat("Nincs kijelölve egy Azonosító sem.");

                string hely = Application.StartupPath + @"\" + Cmbtelephely.Text.Trim() + @"\Adatok\Villamos\Villamos.mdb";
                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM Állománytábla WHERE azonosító='" + Pályaszám.Text.Trim() + "'";

                if (Adatbázis.ABvanilyen(hely, jelszó, szöveg))
                {
                    Kép_megnevezés.Text = Adatbázis.ABkiolvasásszöveg(hely, jelszó, szöveg, "típus");
                }

                Kép_lista_szűrés();
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

        private void Kép_lista_szűrés()
        {
            try
            {
                if (Pályaszám.Text.Trim() == "")
                    throw new HibásBevittAdat("Nincs kijelölve egy Azonosító sem.");
                Kép_listbox.Items.Clear();

                string helykép = Application.StartupPath + @"\Főmérnökség\Adatok\Nosztalgia\kép";
                var Directories = new System.IO.DirectoryInfo(helykép);

                string mialapján = Pályaszám.Text.Trim() + "*.jpg";
                // ha nem üres

                System.IO.FileInfo[] fileInfo = Directories.GetFiles(mialapján, System.IO.SearchOption.AllDirectories);
                foreach (var file in fileInfo)
                    Kép_listbox.Items.Add(file.Name);
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
        private void Kép_btn_Click(object sender, EventArgs e)
        {
            try
            {
                Kép_Feltöltendő.Text = "";

                openFileDialog1.Filter = "JPG Files |*.jpg";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Kép_Feltöltendő.Text = openFileDialog1.FileName;
                    Kép_megjelenítés();
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

        private void Kép_megjelenítés()
        {
            try
            {
                string helykép = Kép_Feltöltendő.Text.Trim();

                if (File.Exists(helykép) == false)
                    throw new HibásBevittAdat("Nincs kiválasztva egy kép sem.");

                Image Kép = Image.FromFile(helykép);
                // megnyitjuk a ablakban 
                PictureBox1.Image = new Bitmap(Kép);

                Kép.Dispose();


                PictureBox1.Visible = true;
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

        private void Kép_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Kép_Feltöltendő.Text = Application.StartupPath + @"\Főmérnökség\Adatok\Nosztalgia\kép\" + Kép_listbox.SelectedItems[0].ToString();
            Kép_megjelenítés();
        }

        private void Kép_rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Pályaszám.Text.Trim() == "")
                    throw new HibásBevittAdat("Nincs kijelölve egy Azonosítós sem.");
                if (Kép_Feltöltendő.Text.Trim() == "")
                    throw new HibásBevittAdat("Nincs kijelölve egy feltöltendő kép sem.");


                string helykép = Application.StartupPath + @"\Főmérnökség\Adatok\Nosztalgia\kép";
                if (Directory.Exists(helykép) == false)
                {
                    // Megnézzük, hogy létezik-e a könyvtár, ha nem létrehozzuk
                    System.IO.Directory.CreateDirectory(helykép);
                }

                // A tervezett fájlnévnek megfelelően szűrjük a könyvtár tartalmát
                Kép_szűrés.Items.Clear();
                var Directories = new System.IO.DirectoryInfo(helykép);

                string mialapján = Pályaszám.Text.Trim() + "_*.jpg";

                System.IO.FileInfo[] fileInfo = Directories.GetFiles(mialapján, System.IO.SearchOption.AllDirectories);

                foreach (var file in fileInfo)
                    Kép_szűrés.Items.Add(file.Name);

                int i;
                if (fileInfo.Length < 1)
                    i = 1;
                else
                {
                    string[] darab = Kép_szűrés.Items[Kép_szűrés.Items.Count - 1].ToString().Split('_');
                    i = int.Parse(darab[1].Replace(".jpg", "")) + 1;
                }

                // átmásoljuk a fájl és átnevezzük
                string újfájlnév = helykép + @"\" + Pályaszám.Text.Trim() + "_" + i.ToString() + ".jpg";

                File.Copy(Kép_Feltöltendő.Text.Trim(), újfájlnév);
                File.Delete(Kép_Feltöltendő.Text.Trim());
                Kép_lista_szűrés();
                Kép_Feltöltendő.Text = "";
                MessageBox.Show("A Kép feltöltése elkészült!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void KépTörlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Kép_listbox.SelectedItems.Count < 1)
                    throw new HibásBevittAdat("Nincs kijelölve egy kép sem.");
                if (Kép_listbox.SelectedItems[0].ToString().Trim() == "")
                    throw new HibásBevittAdat("Nincs kijelölve egy kép sem.");
                if (MessageBox.Show("Biztos, hogy a töröljük a fájlt?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    string helykép = Application.StartupPath + @"\Főmérnökség\Adatok\Nosztalgia\kép\" + Kép_listbox.SelectedItem.ToString();
                    Delete(helykép);
                    Kép_lista_szűrés();
                    Kép_Feltöltendő.Text = "";
                    PictureBox1.Visible = false;
                    MessageBox.Show("Az kép törlése megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Mentés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Kép_listbox.SelectedItems.Count < 1)
                    throw new HibásBevittAdat("Nincs kijelölve egy kép sem.");
                if (Kép_listbox.SelectedItems[0].ToString().Trim() == "")
                    throw new HibásBevittAdat("Nincs kijelölve egy kép sem.");

                string helykép = Application.StartupPath + @"\Főmérnökség\Adatok\Nosztalgia\kép";
                if (Directory.Exists(helykép) == false)
                    throw new HibásBevittAdat("A tárhely nem létezik.");

                string hova = "";
                if (FolderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    DirectoryInfo di = new DirectoryInfo(FolderBrowserDialog1.SelectedPath);
                    hova = FolderBrowserDialog1.SelectedPath;
                }
                if (hova.Trim() == "")
                    throw new HibásBevittAdat("Nincs hova menteni a kiválaszott képet.");


                for (int i = 0; i <= Kép_listbox.SelectedItems.Count - 1; i++)
                    File.Copy(helykép + @"\" + Kép_listbox.SelectedItems[0].ToString().Trim(), hova + @"\" + Kép_listbox.SelectedItems[i].ToString().Trim());

                MessageBox.Show("A Kép(ek) másolása megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        #region PDF lapfül
        private void PDF_lista_szűrés()
        {
            try
            {
                if (Pályaszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve egy szonosító sem.");

                Pdf_listbox.Items.Clear();

                string helypdf = Application.StartupPath + @"\Főmérnökség\Adatok\Nosztalgia\pdf";
                var Directories = new System.IO.DirectoryInfo(helypdf);
                string mialapján = Pályaszám.Text.Trim() + "*.pdf";
                // ha nem üres

                FileInfo[] fileInfo = Directories.GetFiles(mialapján, SearchOption.AllDirectories);
                foreach (var file in fileInfo) Pdf_listbox.Items.Add(file.Name);
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
        private void PDF_Frissít_Click(object sender, EventArgs e)
        {
            PDF_lista_szűrés();
        }
        private void BtnPDF_Click(object sender, EventArgs e)
        {
            try
            {
                Feltöltendő.Text = "";
                openFileDialog1.Filter = "PDF Files |*.pdf";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Byte[] bytes = File.ReadAllBytes(openFileDialog1.FileName);
                    MemoryStream stream = new MemoryStream(bytes);
                    PdfDocument pdfDocument = PdfDocument.Load(stream);
                    PDF_néző.Document = pdfDocument;
                    PDF_néző.Visible = true;
                    Feltöltendő.Text = openFileDialog1.FileName;
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
        private void PDF_rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Pályaszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs megadva az azonosító.");
                if (Feltöltendő.Text.Trim() == "") throw new HibásBevittAdat("Nincs feltöltendő fájl.");

                string helypdf = Application.StartupPath + @"\Főmérnökség\Adatok\Nosztalgia\pdf";
                if (Directory.Exists(helypdf) == false) Directory.CreateDirectory(helypdf);

                // A tervezett fájlnévnek megfelelően szűrjük a könyvtár tartalmát
                Szűrés.Items.Clear();
                DirectoryInfo Directories = new DirectoryInfo(helypdf);

                string mialapján = Pályaszám.Text.Trim() + "_*.pdf";

                FileInfo[] fileInfo = Directories.GetFiles(mialapján, SearchOption.AllDirectories);

                foreach (var file in fileInfo) Szűrés.Items.Add(file.Name);

                int i;
                if (fileInfo.Length < 1) i = 1;
                else
                {
                    string[] darab = Szűrés.Items[Szűrés.Items.Count - 1].ToString().Split('_');
                    i = int.Parse(darab[1].Replace(".pdf", "")) + 1;
                }

                //létrehozzuk az új fájlnevet és átmásoljuk a tárhelyre
                string újfájlnév = helypdf + @"\" + Pályaszám.Text.Trim() + "_" + i.ToString() + ".pdf";

                File.Copy(Feltöltendő.Text.Trim(), újfájlnév);
                //kitöröljük a feltöltendő fájlt
                File.Delete(Feltöltendő.Text.Trim());
                Feltöltendő.Text = "";
                PDF_lista_szűrés();

                MessageBox.Show("A PDF feltöltése elkészült!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void PDF_törlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Pdf_listbox.SelectedItems.Count < 1) throw new HibásBevittAdat("Nincs kijelölve egy elem sem.");
                if (Pdf_listbox.SelectedItems[0].ToString().Trim() == "") throw new HibásBevittAdat("Nincs kijelölve egy elem sem.");

                if (MessageBox.Show("Biztos, hogy a töröljük a fájlt?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    string helypdf = Application.StartupPath + @"\Főmérnökség\Adatok\Nosztalgia\pdf\" + Pdf_listbox.SelectedItem.ToString();
                    Delete(helypdf);
                    // igent választottuk
                    PDF_lista_szűrés();
                    Feltöltendő.Text = "";
                    PDF_néző.Visible = false;
                    MessageBox.Show("A PDF fájl törlése megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void PDF_azonísító_választó()
        {
            try
            {
                if (Pályaszám.Text.Trim() == "") return;

                string szöveg = "SELECT * FROM Állománytábla WHERE azonosító='" + Pályaszám.Text.Trim() + "'";
                string hely = Application.StartupPath + @"\" + Cmbtelephely.Text.Trim() + @"\Adatok\Villamos\Villamos.mdb";
                string jelszó = "pozsgaii";

                if (Adatbázis.ABvanilyen(hely, jelszó, szöveg)) PDF_megnevezés.Text = Adatbázis.ABkiolvasásszöveg(hely, jelszó, szöveg, "típus");
                PDF_lista_szűrés();
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
        private void Pdf_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string helypdf = Application.StartupPath + @"\Főmérnökség\Adatok\Nosztalgia\pdf\" + Pdf_listbox.SelectedItems[0].ToString();
                Byte[] bytes = System.IO.File.ReadAllBytes(helypdf);
                MemoryStream stream = new MemoryStream(bytes);
                PdfDocument pdfDocument = PdfDocument.Load(stream);
                PDF_néző.Document = pdfDocument;
                PDF_néző.Visible = true;
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

        private void button2_Click(object sender, EventArgs e)
        {
            //kmu és dátum adatokat rögzíti


            try
            {
                //// megnézzük, hogy létezik-e adott új helyen napi tábla
                string hely = Application.StartupPath + @"\Főmérnökség\Adatok\Nosztalgia\FutásnapNoszt.mdb";

                // megpróbáljuk megnyitni az excel táblát.
                openFileDialog1.InitialDirectory = "MyDocuments";
                openFileDialog1.Title = "SAP-s Adatok betöltése";
                openFileDialog1.FileName = "";
                openFileDialog1.Filter = "Excel |*.xlsx";
                string fájlexc;
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (openFileDialog1.ShowDialog() != DialogResult.Cancel) fájlexc = openFileDialog1.FileName;
                else return;

                // megnyitjuk a beolvasandó táblát
                MyE.ExcelMegnyitás(fájlexc);

                // megnézzük, hogy hány sorból áll a tábla
                int i = 1;
                int utolsó = 0;
                while (MyE.Beolvas("a" + i.ToString()) != "_") { utolsó = i; i += 1; }
                Holtart.Maximum = utolsó;
                Holtart.Visible = true;
                Holtart.Value = 1;
                string jelszó = "kloczkal";
                string szöveg;
                if (utolsó > 1)
                {
                    i = 2;
                    List<string> lista = new List<string>();
                    while (utolsó + 1 != i)
                    {
                        //UPDATE Állomány SET Év = '1990' where Azonosító = "1305" (UPDATE szintaktika)
                        szöveg = "UPDATE Állomány SET km_u='";
                        szöveg += MyE.Beolvas("D" + i) + "',";
                        szöveg += $" utolsórögzítés='{DateTime.Today.ToString("yyyy.MM.dd").Trim()}'";
                        szöveg += " WHERE Azonosító='";
                        szöveg += MyE.Beolvas("A" + i).Substring(1).Trim() + "'";

                        lista.Add(szöveg);
                        Holtart.Value++;
                        i++;
                        if (Holtart.Value >= Holtart.Maximum) Holtart.Value = 1;
                    }

                    MyA.ABMódosítás(hely, jelszó, lista);
                }
                // az excel tábla bezárása
                MyE.ExcelBezárás();
                Holtart.Visible = false;
                // kitöröljük a betöltött fájlt
                //if (File.Exists(fájlexc) == true)
                //    File.Delete(fájlexc);
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // ha nincs tábla tartalma
                if (Pályaszám.Text == string.Empty) throw new HibásBevittAdat("A pályaszám nincs kiválasztva.");

                ListaFutásNapNoszt();

                string jelszó = "pozsgaii";
                // Módosítjuk a jármű státuszát
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";
                // megnyitjuk a hibákat
                string helyhiba = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\villamos\hiba.mdb";
                // naplózás
                string helynapló = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\hibanapló";
                helynapló += @"\" + DateTime.Now.ToString("yyyyMM") + "hibanapló.mdb";
                if (Exists(helynapló) == false)
                    Adatbázis_Létrehozás.Hibatáblalap(helynapló);

                Holtart.Visible = true;
                Holtart.Maximum = 100;

                string szöveg;
                string szöveg0;
                string szöveg1;
                string szöveg2;
                int talált;
                int státus;
                string típusa;
                int hibáksorszáma;
                int hiba;
                int volt = 0;
                string ideig_psz = Pályaszám.Text;

                //Hiba -- Itt meg kéne vizsgálnia, hogy melyik a következő esedékes vizsgálat / Le kéne szűrnie, hogy a 3 közül melyik következzen be

                szöveg1 = "Noszt-";
                if (Cmb_FutCiklusE_Cnév.Text != string.Empty && Cmb_FutCiklusE.Text != string.Empty)
                {
                    szöveg1 += Cmb_FutCiklusE.Text;
                    szöveg1 += $"-{Fut_dátum.Value:yyyy.MM.dd}-";
                    volt = 1;
                }
                if (Cmb_KmCiklus_V1.Text != string.Empty && Cmb_KmCiklus_V1_Cnév.Text != string.Empty)
                {
                    szöveg1 += Cmb_KmCiklus_V1.Text;
                    szöveg1 += $"-{Txt_V1_dátum.Value:yyyy.MM.dd}-";
                    volt = 1;
                }
                if (Cmb_KmCiklus_V2.Text != string.Empty && Cmb_KmCiklus_V2_Cnév.Text != string.Empty)
                {
                    szöveg1 += Cmb_KmCiklus_V2.Text;
                    szöveg1 += $"-{Txt_V2_dátum.Value:yyyy.MM.dd}";
                    volt = 1;
                }
                if (volt == 1)
                {
                    // Megnézzük, hogy volt-e már rögzítve ilyen szöveg
                    talált = 0;
                    szöveg2 = "SELECT * FROM hibatábla where azonosító='" + ideig_psz.Trim() + "' AND (hibaleírása LIKE '%" + szöveg1.Trim() + "%' )";
                    if (MyA.ABvanilyen(helyhiba, jelszó, szöveg2))
                        talált = 1;
                    // ha már volt ilyen szöveg rögzítve a pályaszámhoz akkor nem rögzítjük mégegyszer
                    if (talált == 0)
                    {
                        // hibák számát emeljük és státus állítjuk ha kell
                        szöveg0 = "SELECT * FROM állománytábla where [azonosító]='" + ideig_psz.Trim() + "'";
                        Kezelő_Jármű KézJármű = new Kezelő_Jármű();
                        Adat_Jármű AdatokJármű = KézJármű.Egy_Adat(hely, jelszó, szöveg0);
                        if (AdatokJármű != null)
                        {
                            if (!int.TryParse(AdatokJármű?.Hibák.ToString(), out hibáksorszáma)) hibáksorszáma = 0;

                            hiba = hibáksorszáma++;
                            típusa = AdatokJármű.Típus ?? "";
                            if (!int.TryParse(AdatokJármű.Státus.ToString(), out státus)) státus = 0;


                            if (státus < 3)
                                státus = 3; // ha 3,4 státusa akkor nem kell módosítani.

                            // rögzítjük a villamos.mdb-be
                            szöveg = "UPDATE állománytábla SET ";
                            szöveg += " hibák=" + hiba.ToString() + ", ";
                            szöveg += " státus=" + státus.ToString();
                            szöveg += " WHERE  [azonosító]='" + ideig_psz.Trim() + "'";
                            MyA.ABMódosítás(hely, jelszó, szöveg);

                            // beírjuk a hibákat
                            szöveg = "INSERT INTO hibatábla (létrehozta, korlát, hibaleírása, idő, javítva, típus, azonosító, hibáksorszáma ) VALUES (";
                            szöveg += "'" + Program.PostásNév.Trim() + "', ";
                            szöveg += " 3, ";
                            szöveg += "'" + szöveg1.Trim() + "', ";
                            szöveg += "'" + DateTime.Now.ToString() + "', false, ";
                            szöveg += "'" + típusa.Trim() + "', ";
                            szöveg += "'" + ideig_psz.Trim() + "', " + hibáksorszáma.ToString() + ")";
                            MyA.ABMódosítás(helyhiba, jelszó, szöveg);
                            // naplózzuk a hibákat
                            MyA.ABMódosítás(helynapló, jelszó, szöveg);
                        }
                    }
                }
                volt = 0;

                Holtart.Visible = false;

                MessageBox.Show("Az adatok rögzítése befejeződött!", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
