using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using static System.IO.File;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    public partial class Ablak_DolgozóiLekérdezések
    {
        readonly Kezelő_Kulcs KézKulcs = new Kezelő_Kulcs();
        readonly Kezelő_Kulcs_Kettő KézKulcs2 = new Kezelő_Kulcs_Kettő();
        readonly Kezelő_Létszám_Elrendezés_Változatok Kéz_Változatok = new Kezelő_Létszám_Elrendezés_Változatok();
        readonly Kezelő_Dolgozó_Személyes KézSzemélyes = new Kezelő_Dolgozó_Személyes();
        List<Adat_Kulcs> Adatok_Kulcs = new List<Adat_Kulcs>();

        public Ablak_DolgozóiLekérdezések()
        {
            InitializeComponent();
        }


        private void AblakDolgozóiLekérdezések_Load(object sender, EventArgs e)
        {
            Telephelyekfeltöltése();
            Jogosultságkiosztás();
            Fülek.SelectedIndex = 0;
            Fülekkitöltése();
            Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;

            Dátumig.Value = new DateTime(DateTime.Today.Year, 12, 31);
            Dátumtól.Value = new DateTime(DateTime.Today.Year, 1, 1);

            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Segéd\megjelenfeláll.mdb";
            if (!Exists(hely))
                Adatbázis_Létrehozás.Létszám_Elrendezés_Változatok(hely);
        }


        #region Alap
        private void Fülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
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


        private void Fülekkitöltése()
        {
            switch (Fülek.SelectedIndex)
            {
                case 0:
                    {
                        break;
                    }


                case 1:
                    {
                        Változatokbetöltése();
                        Csoportfeltöltés2();
                        break;
                    }
                case 2:
                    {
                        // jogosítvány adatok feltöltése

                        KiírjaDolgozókat();
                        break;
                    }


                case 3:
                    {
                        // kiegészítő munkakör
                        PDFMunkakörfeltöltés();
                        break;
                    }
            }
        }


        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                Cmbtelephely.Items.AddRange(Listák.TelephelyLista_Személy(true));
                if (Program.PostásTelephely == "Főmérnökség")
                    Cmbtelephely.Text = Cmbtelephely.Items[0].ToStrTrim();
                else
                    Cmbtelephely.Text = Program.PostásTelephely;

                for (int i = 0; i < Cmbtelephely.Items.Count; i++)
                    Cmbtelephely.SetItemChecked(i, true);

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

            melyikelem = 64;
            // módosítás 1 Dolgozók oktatásainak elrendelése
            if (MyF.Vanjoga(melyikelem, 1))
            {

            }
            // módosítás 2 dolgozó oktatás elrendelésének törlése átütemezése
            if (MyF.Vanjoga(melyikelem, 2))
            {

            }
            // módosítás 3 adminisztráció mentés, jelenléti ív készítés, e-mail küldés
            if (MyF.Vanjoga(melyikelem, 3))
            {

            }
        }


        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Dolgozólekérdezések.html";
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


        private void BtnFel_Click(object sender, EventArgs e)
        {
            Cmbtelephely.Height = 45;
            BtnLe.Visible = true;
            BtnFel.Visible = false;
        }


        private void BtnLe_Click(object sender, EventArgs e)
        {
            Cmbtelephely.Height = 400;
            BtnLe.Visible = false;
            BtnFel.Visible = true;
        }


        private void BtnTelepMind_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Cmbtelephely.Items.Count; i++)
                Cmbtelephely.SetItemChecked(i, true);
        }


        private void BtnTelepÜres_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Cmbtelephely.Items.Count; i++)
                Cmbtelephely.SetItemChecked(i, false);
        }

        #endregion



        #region Létszám adatok lekérdezése

        private void BtnExcelkimenet_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\kiegészítő2.mdb";

                Alholtart.Be();
                Főholtart.Be(Cmbtelephely.Items.Count + 1);

                string helykulcs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Villamos\Kulcs.mdb";
                string szöveg;
                bool kulcsfájlvan = false;
                List<Adat_Kulcs> AdatokKulcs = null;
                if (File.Exists(helykulcs))
                {
                    kulcsfájlvan = true;
                    AdatokKulcs = KézKulcs.Lista_Adatok();
                }


                string helypénz = Application.StartupPath + @"\Főmérnökség\adatok\Villamos10.mdb";


                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Szakszolgálati lekérdezés",
                    FileName = "Felépítés_Üzemenként_" + DateTime.Now.ToString("yyyyMMddhhmmss"),
                    Filter = "Excel |*.xlsx"
                };
                string fájlexc;
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;
                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);


                // létrehozzuk az excel táblát
                MyE.ExcelLétrehozás();
                MyE.Munkalap_betű("Arial", 12);


                // ****************************************************
                // elkészítjük a lapokat
                // ****************************************************
                string munkalap = "Összesítő";
                MyE.Munkalap_átnevezés("Munka1", munkalap);


                for (int i = 0; i < Cmbtelephely.CheckedItems.Count; i++)
                    MyE.Új_munkalap(Cmbtelephely.CheckedItems[i].ToString());

                int öoszlop = 2;


                string helyvált;
                string jelszóvált = "Mocó";
                string jelszó = "forgalmiutasítás";


                List<Adat_Dolgozó_Személyes> AdatokSzemélyes = KézSzemélyes.Lista_Adatok();


                int fizikai = 0;
                int alkalmazott = 0;
                int Vezényelt = 0;
                int Vezényelve = 0;
                int Részmunkaidős = 0;
                int passzív = 0;
                bool személyeseng;
                bool béreng;
                string ideig;

                string Telephely;


                for (int ii = 0; ii < Cmbtelephely.CheckedItems.Count; ii++)
                {
                    személyeseng = false;
                    béreng = false;
                    Telephely = Cmbtelephely.CheckedItems[ii].ToString();
                    if (kulcsfájlvan)
                    {

                        string adat1 = Program.PostásNév.Trim().ToUpper();
                        string adat2 = Program.PostásTelephely.Trim().ToUpper();
                        string adat3 = "A";
                        személyeseng = KézKulcs.ABKULCSvan(adat1, adat2, adat3);

                        adat3 = "B";
                        béreng = KézKulcs.ABKULCSvan(adat1, adat2, adat3);
                    }
                    Főholtart.Lép();
                    munkalap = Cmbtelephely.CheckedItems[ii].ToString().Trim();
                    MyE.Munkalap_aktív(munkalap);

                    // elkészítjük a fejlécet
                    MyE.Kiir("Sorszám", "a1");
                    MyE.Kiir("Név", "b1");
                    MyE.Kiir("Munkakör", "c1");
                    MyE.Kiir("HR törzsszám", "d1");
                    MyE.Kiir("Születési idő", "e1");
                    MyE.Kiir("Belépési idő", "f1");
                    MyE.Kiir("Bér", "g1");
                    MyE.Kiir("Csoport", "h1");
                    MyE.Kiir("Passzív", "i1");
                    MyE.Kiir("Alkalmazott/fizikai", "j1");
                    MyE.Kiir("Ide vezényelt", "k1");
                    MyE.Kiir("Elvezényelve", "l1");
                    MyE.Kiir("Részmunkaidős", "m1");

                    // lenullázzuk
                    fizikai = 0;
                    alkalmazott = 0;
                    Vezényelt = 0;
                    Vezényelve = 0;
                    Részmunkaidős = 0;
                    passzív = 0;

                    hely = $@"{Application.StartupPath}\{Cmbtelephely.CheckedItems[ii].ToStrTrim()}\Adatok\Dolgozók.mdb";
                    // leellenőrizzük, hogy minden munkahely ki van-e töltve.
                    Munkahelyellenőrzés(Cmbtelephely.CheckedItems[ii].ToStrTrim());

                    helyvált = $@"{Application.StartupPath}\{Cmbtelephely.CheckedItems[ii].ToString().Trim()}\adatok\segéd\kiegészítő.mdb";
                    szöveg = "SELECT * FROM csoportbeosztás order by sorszám";

                    Kezelő_Kiegészítő_Csoportbeosztás KézCsop = new Kezelő_Kiegészítő_Csoportbeosztás();
                    List<Adat_Kiegészítő_Csoportbeosztás> AdatokCsop = KézCsop.Lista_Adatok(helyvált, jelszóvált, szöveg);

                    int i = 2;
                    if (Exists(helyvált) && Exists(hely))
                    {
                        szöveg = "SELECT * FROM Dolgozóadatok WHERE Kilépésiidő=#01-01-1900#  order by DolgozóNév ";
                        Kezelő_Dolgozó_Alap KézDolg = new Kezelő_Dolgozó_Alap();
                        List<Adat_Dolgozó_Alap> AdatokDolg = KézDolg.Lista_Adatok(hely, jelszó, szöveg);

                        Alholtart.Be(AdatokCsop.Count + 1);

                        foreach (Adat_Kiegészítő_Csoportbeosztás Csoport in AdatokCsop)
                        {
                            Alholtart.Lép();

                            List<Adat_Dolgozó_Alap> CsoportTagok = AdatokDolg.Where(Elem => Elem.Csoport.Trim() == Csoport.Csoportbeosztás.Trim()).ToList();


                            foreach (Adat_Dolgozó_Alap rekord in CsoportTagok)
                            {
                                MyE.Kiir((i - 1).ToString(), "a" + i);
                                MyE.Kiir(rekord.DolgozóNév.Trim(), "b" + i);
                                MyE.Kiir(rekord.Munkakör.Trim(), "c" + i);
                                MyE.Kiir(rekord.Dolgozószám.Trim(), "d" + i);

                                if (személyeseng)
                                {
                                    Adat_Dolgozó_Személyes Elem = (from a in AdatokSzemélyes
                                                                   where a.Dolgozószám == rekord.Dolgozószám
                                                                   select a).FirstOrDefault();
                                    if (Elem != null)
                                        MyE.Kiir(Elem.Születésiidő.ToString("yyyy.MM.dd"), "e" + i);

                                }

                                MyE.Kiir(rekord.Belépésiidő.ToString("yyyy.MM.dd"), "f" + i);

                                if (béreng)
                                {
                                    ideig = MyF.Rövidkód(rekord.Dolgozószám);

                                    Adatok_Kulcs = KézKulcs2.Lista_Adatok();

                                    Adat_Kulcs vane = Adatok_Kulcs.FirstOrDefault(a => a.Adat1.Contains(ideig));

                                    if (vane != null)
                                    {
                                        ideig = vane.Adat2;

                                        if (ideig != "_" && ideig != null)
                                        {
                                            string bére = MyF.Dekódolja(ideig);
                                            MyE.Kiir(bére, "g" + i);
                                        }
                                    }
                                }
                                MyE.Kiir(rekord.Csoport, "h" + i);
                                if (rekord.Passzív)
                                {
                                    MyE.Kiir("passzív", "i" + i);
                                    passzív++;
                                }
                                if (rekord.Alkalmazott)
                                {
                                    MyE.Kiir("Alkalmazott", "j" + i);
                                    alkalmazott++;
                                }
                                else
                                {
                                    MyE.Kiir("Fizikai", "j" + i);
                                    fizikai++;
                                }
                                if (rekord.Vezényelt)
                                {
                                    MyE.Kiir("vezényelt", "k" + i);
                                    Vezényelt++;
                                }
                                if (rekord.Vezényelve)
                                {
                                    MyE.Kiir("vezényelve", "l" + i);
                                    Vezényelve++;
                                }
                                if (rekord.Részmunkaidős)
                                {
                                    MyE.Kiir("részmunkaidős", "m" + i);
                                    Részmunkaidős++;
                                }
                                i += 1;
                            }
                        }

                        //Nincs csoportban
                        List<Adat_Dolgozó_Alap> NincsTagok = AdatokDolg.Where(Elem => Elem.Csoport.Trim() == "Nincs").ToList();

                        foreach (Adat_Dolgozó_Alap rekord in NincsTagok)
                        {
                            MyE.Kiir((i - 1).ToString(), "a" + i);
                            MyE.Kiir(rekord.DolgozóNév.Trim(), "b" + i);
                            MyE.Kiir(rekord.Munkakör.Trim(), "c" + i);
                            MyE.Kiir(rekord.Dolgozószám.Trim(), "d" + i);

                            if (személyeseng)
                            {
                                Adat_Dolgozó_Személyes Elem = (from a in AdatokSzemélyes
                                                               where a.Dolgozószám == rekord.Dolgozószám
                                                               select a).FirstOrDefault();
                                if (Elem != null)
                                    MyE.Kiir(Elem.Születésiidő.ToString("yyyy.MM.dd"), "e" + i);
                            }

                            MyE.Kiir(rekord.Belépésiidő.ToString("yyyy.MM.dd"), "f" + i);

                            if (béreng)
                            {
                                ideig = MyF.Rövidkód(rekord.Dolgozószám);

                                Adatok_Kulcs = KézKulcs2.Lista_Adatok();
                                Adat_Kulcs vane = Adatok_Kulcs.FirstOrDefault(a => a.Adat1.Contains(ideig));
                                ideig = vane.Adat2;

                                if (ideig != "_")
                                {
                                    MyE.Kiir(MyF.Dekódolja(ideig), "g" + i);
                                }
                            }
                            MyE.Kiir(rekord.Csoport, "h" + i);
                            if (rekord.Passzív)
                            {
                                MyE.Kiir("passzív", "i" + i);
                                passzív++;
                            }
                            if (rekord.Alkalmazott)
                            {
                                MyE.Kiir("Alkalmazott", "j" + i);
                                alkalmazott++;
                            }
                            else
                            {
                                MyE.Kiir("Fizikai", "j" + i);
                                fizikai++;
                            }
                            if (rekord.Vezényelt)
                            {
                                MyE.Kiir("vezényelt", "k" + i);
                                Vezényelt++;
                            }
                            if (rekord.Vezényelve)
                            {
                                MyE.Kiir("vezényelve", "l" + i);
                                Vezényelve++;
                            }
                            if (rekord.Részmunkaidős)
                            {
                                MyE.Kiir("részmunkaidős", "m" + i);
                                Részmunkaidős++;
                            }
                            i += 1;
                        }
                    }
                    MyE.Oszlopszélesség(munkalap, "A:M");
                    MyE.Szűrés(munkalap, "A:M", 1);

                    MyE.Rácsoz("A1:m" + i);
                    MyE.Vastagkeret("A1:m" + i);


                    i += 1;
                    MyE.Kiir("Szellemi", "b" + i);
                    MyE.Kiir(alkalmazott.ToString(), "c" + i);

                    MyE.Kiir("Fizikai", "b" + (i + 1).ToString());
                    MyE.Kiir(fizikai.ToString(), "c" + (i + 1).ToString());

                    MyE.Kiir("Összesen", "b" + (i + 2).ToString());
                    MyE.Kiir((fizikai + alkalmazott).ToString(), "c" + (i + 2).ToString());

                    MyE.Kiir("Vezényelve", "b" + (i + 3).ToString());
                    MyE.Kiir(Vezényelve.ToString(), "c" + (i + 3).ToString());

                    MyE.Kiir("vezényelt", "b" + (i + 4).ToString());
                    MyE.Kiir(Vezényelt.ToString(), "c" + (i + 4).ToString());

                    MyE.Kiir("részmunkaidős", "b" + (i + 5).ToString());
                    MyE.Kiir(Részmunkaidős.ToString(), "c" + (i + 5).ToString());

                    MyE.Kiir("Passzív", "b" + (i + 6).ToString());
                    MyE.Kiir(passzív.ToString(), "c" + (i + 6).ToString());

                    MyE.Rácsoz("b" + i + ":c" + (i + 6));
                    MyE.Vastagkeret("b" + i + ":c" + (i + 6).ToString());
                    MyE.Tábla_Rögzítés("b1:c" + (i + 6), 1);


                    // összesítő lapra kiírjuk telephelyenként
                    MyE.Munkalap_aktív("Összesítő");
                    MyE.Kiir(Telephely, MyE.Oszlopnév(öoszlop) + "1");
                    MyE.Kiir(alkalmazott.ToString(), MyE.Oszlopnév(öoszlop) + "2");
                    MyE.Kiir(fizikai.ToString(), MyE.Oszlopnév(öoszlop) + "3");
                    MyE.Kiir((fizikai + alkalmazott).ToString(), MyE.Oszlopnév(öoszlop) + "4");
                    MyE.Betű(MyE.Oszlopnév(öoszlop) + "4", false, false, true);
                    MyE.Kiir(Vezényelve.ToString(), MyE.Oszlopnév(öoszlop) + "5");
                    MyE.Kiir(Vezényelt.ToString(), MyE.Oszlopnév(öoszlop) + "6");
                    MyE.Kiir(Részmunkaidős.ToString(), MyE.Oszlopnév(öoszlop) + "7");
                    MyE.Kiir(passzív.ToString(), MyE.Oszlopnév(öoszlop) + "8");

                    MyE.Rácsoz(MyE.Oszlopnév(öoszlop) + "1:" + MyE.Oszlopnév(öoszlop) + "8");
                    MyE.Vastagkeret(MyE.Oszlopnév(öoszlop) + "1:" + MyE.Oszlopnév(öoszlop) + "8");
                    MyE.Oszlopszélesség("Összesítő", MyE.Oszlopnév(öoszlop) + ":" + MyE.Oszlopnév(öoszlop));

                    öoszlop += 1;
                }



                MyE.Rácsoz("a1:a8");
                MyE.Vastagkeret("a1:a8");
                MyE.Kiir("Szellemi", "a2");
                MyE.Kiir("Fizikai", "a3");
                MyE.Kiir("Összesen", "a4");
                MyE.Betű("a4", false, false, true);
                MyE.Kiir("Ide vezényelve", "a5");
                MyE.Kiir("Elvezényelt", "a6");
                MyE.Kiir("részmunkaidős", "a7");
                MyE.Kiir("Passzív", "a8");
                MyE.Oszlopszélesség("Összesítő", "A:A");

                // összesítő oszlop
                MyE.Kiir("Összesen:", MyE.Oszlopnév(öoszlop) + "1");
                for (int i = 2; i < 9; i++)
                    MyE.Kiir("=SUM(RC[-" + (öoszlop - 2).ToString() + "]:RC[-1])", MyE.Oszlopnév(öoszlop) + i);

                MyE.Betű(MyE.Oszlopnév(öoszlop) + "4");
                MyE.Rácsoz(MyE.Oszlopnév(öoszlop) + "1:" + MyE.Oszlopnév(öoszlop) + "8");
                MyE.Oszlopszélesség("Összesítő", MyE.Oszlopnév(öoszlop) + ":" + MyE.Oszlopnév(öoszlop));


                MyE.Munkalap_aktív("Összesítő");
                MyE.Aktív_Cella("Összesítő", "A1");
                MyE.ExcelMentés(fájlexc + ".xlsx");
                MyE.ExcelBezárás();

                MessageBox.Show("A fájl elkészült.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyE.Megnyitás(fájlexc + ".xlsx");

                Főholtart.Ki();
                Alholtart.Ki();

            }
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



        private void Munkahelyellenőrzés(string Telephely)
        {
            try
            {
                Kezelő_Dolgozó_Alap KézDolg = new Kezelő_Dolgozó_Alap();
                List<Adat_Dolgozó_Alap> AdatokDolgÖ = KézDolg.Lista_Adatok(Telephely);
                List<Adat_Dolgozó_Alap> AdatokDolg = (from a in AdatokDolgÖ
                                                      where a.Kilépésiidő.ToShortDateString() == "1900.01.01"
                                                      orderby a.DolgozóNév
                                                      select a).ToList();

                foreach (Adat_Dolgozó_Alap rekord in AdatokDolg)
                {
                    if (rekord.Csoport == null || rekord.Csoport.Trim() == "")
                    {
                        Adat_Dolgozó_Alap ADAT = new Adat_Dolgozó_Alap(rekord.Dolgozószám.Trim(),
                                                                       "Nincs",
                                                                       new DateTime(1900, 1, 1));
                        KézDolg.Módosít_Csoport(Telephely, ADAT);
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


        private void Command3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Cmbtelephely.CheckedItems.Count < 1) throw new HibásBevittAdat("Nincs kiválasztva egy telephely sem.");


                Kezelő_Kiegészítő_Csoportbeosztás KézCsop = new Kezelő_Kiegészítő_Csoportbeosztás();
                Kezelő_Dolgozó_Alap KézDolg = new Kezelő_Dolgozó_Alap();

                Főholtart.Be(Cmbtelephely.CheckedItems.Count + 1);
                Alholtart.Be();

                // kimeneti fájl helye és neve
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Felépítés  lekérdezés",
                    FileName = "Felépítés_" + DateTime.Now.ToString("yyyyMMddhhmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);

                // létrehozzuk az excel táblát
                MyE.ExcelLétrehozás();
                string munkalap = "Összesítő";
                MyE.Munkalap_betű("Arial", 12);

                // ****************************************************
                // elkészítjük a lapokat
                // ****************************************************
                MyE.Munkalap_átnevezés("Munka1", munkalap);

                for (int i = 0; i < Cmbtelephely.CheckedItems.Count; i++)
                    MyE.Új_munkalap(Cmbtelephely.CheckedItems[i].ToStrTrim());

                int[] feorössz = new int[10];
                int sor;
                int oszlop;
                var utolsósor = default(int);
                int darab;

                // elkészítjük az egyes telephelyeket
                for (int i = 0; i < Cmbtelephely.CheckedItems.Count; i++)
                {
                    string telep = Cmbtelephely.CheckedItems[i].ToStrTrim();
                    utolsósor = 0;
                    for (int j = 1; j < 10; j++)
                        feorössz[j] = 0;
                    Főholtart.Lép();
                    munkalap = telep;
                    MyE.Munkalap_aktív(munkalap);

                    string helyvált = $@"{Application.StartupPath}\{telep}\adatok\segéd\kiegészítő.mdb";
                    string jelszóvált = "Mocó";
                    string szöveg = "select * from csoportbeosztás order by sorszám";
                    List<Adat_Kiegészítő_Csoportbeosztás> Csoport = KézCsop.Lista_Adatok(helyvált, jelszóvált, szöveg);

                    string hely = $@"{Application.StartupPath}\{telep}\Adatok\Dolgozók.mdb";
                    string jelszó = "forgalmiutasítás";

                    sor = 1;
                    oszlop = -3;

                    Alholtart.Be(Csoport.Count + 2);

                    foreach (Adat_Kiegészítő_Csoportbeosztás rekordvált in Csoport)
                    {
                        Alholtart.Lép();
                        oszlop += 4;

                        szöveg = $"SELECT * FROM Dolgozóadatok where csoport='{rekordvált.Csoportbeosztás.Trim()}' AND kilépésiidő=#01-01-1900# order by DolgozóNév asc";
                        List<Adat_Dolgozó_Alap> AdatDolg = KézDolg.Lista_Adatok(hely, jelszó, szöveg);

                        // elkészítjük a fejlécet
                        MyE.Egyesít(munkalap, MyE.Oszlopnév(oszlop) + sor + ":" + MyE.Oszlopnév(oszlop + 3) + sor);
                        MyE.Kiir(rekordvált.Csoportbeosztás.Trim(), MyE.Oszlopnév(oszlop) + sor);

                        sor += 1;
                        MyE.Kiir("Ssz.", MyE.Oszlopnév(oszlop) + sor);
                        MyE.Kiir("Név", MyE.Oszlopnév(oszlop + 1) + sor);
                        MyE.Kiir("Feor", MyE.Oszlopnév(oszlop + 2) + sor);
                        MyE.Kiir("Munkakör", MyE.Oszlopnév(oszlop + 3) + sor);


                        foreach (Adat_Dolgozó_Alap rekord in AdatDolg)
                        {
                            sor += 1;
                            MyE.Kiir((sor - 2).ToString(), MyE.Oszlopnév(oszlop) + sor);
                            MyE.Kiir(rekord.DolgozóNév.Trim(), MyE.Oszlopnév(oszlop + 1) + sor);
                            MyE.Kiir(rekord.Feorsz.Trim(), MyE.Oszlopnév(oszlop + 2) + sor);
                            MyE.Kiir(rekord.Munkakör.Trim(), MyE.Oszlopnév(oszlop + 3) + sor);

                            if (rekord.Feorsz.Length > 1)
                            {
                                if (int.TryParse(rekord.Feorsz.Substring(0, 1), out int Szám))
                                {
                                    if (Szám < 5)
                                        MyE.Háttérszín(MyE.Oszlopnév(oszlop) + sor + ":" + MyE.Oszlopnév(oszlop + 3) + sor, 10053375);

                                    feorössz[Szám]++;
                                }
                            }

                            // passzív színez
                            if (rekord.Passzív)
                                MyE.Háttérszín(MyE.Oszlopnév(oszlop) + sor + ":" + MyE.Oszlopnév(oszlop + 3) + sor, 9868950);
                        }

                        if (utolsósor < sor)
                            utolsósor = sor;
                        if (sor > 1)
                        {
                            MyE.Rácsoz(MyE.Oszlopnév(oszlop) + "1:" + MyE.Oszlopnév(oszlop + 3) + sor);
                            MyE.Vastagkeret(MyE.Oszlopnév(oszlop) + "1:" + MyE.Oszlopnév(oszlop + 3) + sor);
                        }
                        MyE.Vastagkeret(MyE.Oszlopnév(oszlop) + "1:" + MyE.Oszlopnév(oszlop + 3) + "2");
                        MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(oszlop) + ":" + MyE.Oszlopnév(oszlop + 3));
                        sor = 1;
                    }

                    // kiírjuk az öszesítő táblákat
                    sor = utolsósor + 2;

                    MyE.Kiir("Létszám összetétel", "b" + sor);
                    MyE.Egyesít(munkalap, "a" + sor + ":a" + sor);
                    sor += 2;
                    MyE.Kiir("Feor", "b" + sor);
                    MyE.Egyesít(munkalap, "a" + sor + ":a" + sor);
                    darab = 0;
                    for (int j = 1; j < 10; j++)
                    {
                        sor += 1;
                        MyE.Kiir("F." + j, "b" + sor);
                        MyE.Kiir(feorössz[j].ToString(), "c" + sor);
                        darab += feorössz[j];
                    }
                    sor += 1;
                    MyE.Kiir("Összesen:", "b" + sor);
                    MyE.Kiir(darab.ToString(), "c" + sor);
                    MyE.Rácsoz("b" + (sor - 10).ToString() + ":" + "c" + sor);
                    MyE.Vastagkeret("b" + (sor - 10).ToString() + ":" + "c" + sor);
                }

                Főholtart.Ki();
                Alholtart.Ki();
                MyE.Munkalap_aktív(munkalap);
                MyE.Aktív_Cella(munkalap, "A1");
                MyE.ExcelMentés(fájlexc + ".xlsx");
                MyE.ExcelBezárás();

                MessageBox.Show("A fájl elkészült.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        #endregion



        #region Saját elrendezés készítés
        private void Excelbe_Click(object sender, EventArgs e)
        {
            try
            {
                if (Változatoklist.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva érvényes változat.");

                // kimeneti fájl helye és neve
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Felépítés  lekérdezés",
                    FileName = $"Felépítés_{Változatoklist.Text.Trim()}_" + DateTime.Now.ToString("yyyyMMddhhmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                string munkalap = "Munka1";
                MyE.ExcelLétrehozás();

                MyE.Munkalap_betű("Arial", 12);
                Holtart.Be();

                string helyvált = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Segéd\megjelenfeláll.mdb";

                Kezelő_Létszám_Elrendezés_Változatok KézLÉT = new Kezelő_Létszám_Elrendezés_Változatok();
                List<Adat_Létszám_Elrendezés_Változatok> AdatokLétÖ = KézLÉT.Lista_Adatok(helyvált);
                List<Adat_Létszám_Elrendezés_Változatok> AdatokLét = (from a in AdatokLétÖ
                                                                      where a.Változatnév == Változatoklist.Text.Trim()
                                                                      select a).ToList();

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Dolgozók.mdb";
                string jelszó = "forgalmiutasítás";
                string szöveg = "SELECT * FROM Dolgozóadatok";
                Kezelő_Dolgozó_Alap KézDolg = new Kezelő_Dolgozó_Alap();
                List<Adat_Dolgozó_Alap> AdatokDolg = KézDolg.Lista_Adatok(hely, jelszó, szöveg);

                // passzív
                int i = 1;
                MyE.Kiir("Passzív", "A" + i);
                MyE.Háttérszín("A" + i, 15773696);
                szöveg = "SELECT COUNT(*) FROM Dolgozóadatok WHERE passzív=true AND kilépésiidő=#1900-01-01#";
                List<Adat_Dolgozó_Alap> Rész = (from a in AdatokDolg
                                                where a.Passzív == true && a.Kilépésiidő == new DateTime(1900, 1, 1)
                                                select a).ToList();
                if (Rész != null)
                    MyE.Kiir(Rész.Count.ToString(), "b" + i.ToString());
                else
                    MyE.Kiir("0", "b" + i.ToString());

                i += 1;
                // vezényelt
                MyE.Kiir("El Vezényelt", "A" + i);
                MyE.Háttérszín("A" + i, 65535);
                Rész.Clear();
                Rész = (from a in AdatokDolg
                        where a.Vezényelt == true && a.Kilépésiidő == new DateTime(1900, 1, 1)
                        select a).ToList();
                if (Rész != null)
                    MyE.Kiir(Rész.Count.ToString(), "b" + i.ToString());
                else
                    MyE.Kiir("0", "b" + i.ToString());


                i += 1;
                // vezényelve
                MyE.Kiir("Ide Vezényelve", "A" + i.ToString());
                MyE.Háttérszín("A" + i, 33023);
                Rész.Clear();
                Rész = (from a in AdatokDolg
                        where a.Vezényelve == true && a.Kilépésiidő == new DateTime(1900, 1, 1)
                        select a).ToList();
                if (Rész != null)
                    MyE.Kiir(Rész.Count.ToString(), "b" + i.ToString());
                else
                    MyE.Kiir("0", "b" + i.ToString());


                i += 1;
                // részmunkaidős
                MyE.Kiir("Részmunkaidős", "A" + i.ToString());
                MyE.Háttérszín("A" + i, 65280);
                Rész.Clear();
                Rész = (from a in AdatokDolg
                        where a.Részmunkaidős == true && a.Kilépésiidő == new DateTime(1900, 1, 1)
                        select a).ToList();
                if (Rész != null)
                    MyE.Kiir(Rész.Count.ToString(), "b" + i.ToString());
                else
                    MyE.Kiir("0", "b" + i.ToString());


                i += 1;
                // Nincs csoportban
                MyE.Kiir("Nincs csoportban", "A" + i.ToString());
                Rész.Clear();
                Rész = (from a in AdatokDolg
                        where a.Csoport == "" && a.Kilépésiidő == new DateTime(1900, 1, 1)
                        select a).ToList();
                if (Rész != null)
                    MyE.Kiir(Rész.Count.ToString(), "b" + i.ToString());
                else
                    MyE.Kiir("0", "b" + i.ToString());


                // csoportok létszáma
                i += 2;
                int darabö = 0;

                int k = 0;
                while (k != Csoportlista.Items.Count)
                {
                    MyE.Kiir(Csoportlista.Items[k].ToString(), "A" + i.ToString());
                    Rész.Clear();
                    Rész = (from a in AdatokDolg
                            where a.Csoport == Csoportlista.Items[k].ToStrTrim()
                            && a.Passzív == false
                            && a.Vezényelve == false
                            && a.Részmunkaidős == false
                            && a.Kilépésiidő == new DateTime(1900, 1, 1)
                            select a).ToList();
                    if (Rész != null)
                        MyE.Kiir(Rész.Count.ToString(), "b" + i.ToString());
                    else
                        MyE.Kiir("0", "b" + i.ToString());

                    i += 1;
                    k += 1;
                    darabö += Rész.Count;
                }
                MyE.Kiir("Összesen:", "A" + i.ToString());
                MyE.Betű("A" + i, false, false, true);
                MyE.Kiir(darabö.ToString(), "b" + i.ToString());
                MyE.Betű("b" + i, false, false, true);

                // kiirja a passzívokat
                i += 2;
                MyE.Kiir("Passzív", "A" + i.ToString());
                i += 1;

                szöveg = "SELECT * FROM Dolgozóadatok WHERE passzív=true AND kilépésiidő=#1900-01-01#";

                Kezelő_Dolgozó_Alap Kéz = new Kezelő_Dolgozó_Alap();
                List<Adat_Dolgozó_Alap> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                foreach (Adat_Dolgozó_Alap rekord in Adatok)
                {
                    MyE.Kiir(rekord.DolgozóNév.Trim(), "A" + i);
                    i += 1;
                }

                // kirja a nincs csoportbant
                i += 2;
                MyE.Kiir("Nincs csoportban", "A" + i.ToString());
                i += 1;

                szöveg = "SELECT * FROM Dolgozóadatok WHERE csoport='' AND kilépésiidő=#1900-01-01#";
                Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);
                foreach (Adat_Dolgozó_Alap rekord in Adatok)
                {
                    MyE.Kiir(rekord.DolgozóNév.Trim(), "A" + i);
                    i += 1;
                }

                int utolsósor = 0;

                int utolsóoszlop = 1;
                int sor;
                // csoportonkénti kiírás
                foreach (Adat_Létszám_Elrendezés_Változatok Ábrázol in AdatokLét)
                {
                    szöveg = $"SELECT * FROM Dolgozóadatok WHERE csoport='{Ábrázol.Csoportnév.Trim()}' AND kilépésiidő=#1900-01-01#";
                    Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);
                    sor = Ábrázol.Sor;
                    //csoportnév
                    MyE.Kiir(Ábrázol.Csoportnév.Trim(), Ábrázol.Oszlop + sor);
                    MyE.Betű(Ábrázol.Oszlop + sor, false, false, true);
                    MyE.Háttérszín(Ábrázol.Oszlop + sor, 13092807);

                    //Csoport tagjai
                    sor++;
                    foreach (Adat_Dolgozó_Alap rekord in Adatok)
                    {
                        MyE.Kiir(rekord.DolgozóNév.Trim(), Ábrázol.Oszlop + sor);
                        if (rekord.Vezényelve)
                            MyE.Háttérszín(Ábrázol.Oszlop + sor, 33023);
                        if (rekord.Vezényelt)
                            MyE.Háttérszín(Ábrázol.Oszlop + sor, 65535);
                        if (rekord.Részmunkaidős)
                            MyE.Háttérszín(Ábrázol.Oszlop + sor, 65280);
                        if (rekord.Passzív)
                            MyE.Háttérszín(Ábrázol.Oszlop + sor, 15773696);
                        if (rekord.Csopvez)
                            MyE.Betű(Ábrázol.Oszlop + sor, false, false, true);
                        sor += 1;
                        Holtart.Lép();
                    }
                    if (utolsósor < sor)
                        utolsósor = sor;
                    if (Ábrázol.Oszlop.Length == 1)
                    {
                        int oszlop = (int)(char.Parse(Ábrázol.Oszlop.Substring(0, 1).ToUpper())) - 64;
                        if (utolsóoszlop < oszlop)
                            utolsóoszlop = oszlop;
                    }
                    MyE.Oszlopszélesség(munkalap, Ábrázol.Oszlop + ":" + Ábrázol.Oszlop, Ábrázol.Szélesség);
                    Holtart.Lép();
                }


                MyE.Oszlopszélesség(munkalap, "A:A", 25);

                MyE.Rácsoz("A1:" + MyE.Oszlopnév(utolsóoszlop) + utolsósor);
                MyE.Vastagkeret("A1:" + MyE.Oszlopnév(utolsóoszlop) + utolsósor);
                MyE.NyomtatásiTerület_részletes(munkalap, "A1:" + MyE.Oszlopnév(utolsóoszlop) + utolsósor, "", "", false);
                MyE.Aktív_Cella(munkalap, "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();

                Holtart.Ki();

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

        private void Váltmódosítás_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Segéd\megjelenfeláll.mdb";
                // létrehozzuk a adattáblát
                if (!Exists(hely)) Adatbázis_Létrehozás.Létszám_Elrendezés_Változatok(hely);
                if (Változatoklist.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve egy változat név sem.");

                // Megnézzük, hogy van-e ilyen tábla
                List<Adat_Létszám_Elrendezés_Változatok> Adatok = Kéz_Változatok.Lista_Adatok(hely);

                Adat_Létszám_Elrendezés_Változatok vane = (from a in Adatok
                                                           where a.Változatnév == Változatoklist.Text.Trim()
                                                           select a).FirstOrDefault();

                if (vane == null)
                {
                    Adat_Létszám_Elrendezés_Változatok ADAT = new Adat_Létszám_Elrendezés_Változatok(0,
                                                                                                     Változatoklist.Text.Trim(),
                                                                                                     "_",
                                                                                                     "A",
                                                                                                     1,
                                                                                                     25);
                    Kéz_Változatok.Rögzítés(hely, ADAT);
                }
                Változatokbetöltése();
            }
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

        private void Változattörlés_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Segéd\megjelenfeláll.mdb";
                if (!Exists(hely)) return;

                if (Változatoklist.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve törlendő elem.");
                List<Adat_Létszám_Elrendezés_Változatok> Adatok = Kéz_Változatok.Lista_Adatok(hely);

                if (int.TryParse(Sorszám.Text.Trim(), out int sorszám))
                {
                    Adat_Létszám_Elrendezés_Változatok vane2 = Adatok.FirstOrDefault(a => a.Id == sorszám);

                    if (vane2 != null)
                    {
                        Adat_Létszám_Elrendezés_Változatok ADAT = new Adat_Létszám_Elrendezés_Változatok(sorszám, "", "", "", 0, 15);
                        Kéz_Változatok.Törlés(hely, ADAT);
                    }
                }

                Csoportlista.Text = "";
                Oszlopa.Text = "";
                Sora.Text = "";
                Szélessége.Text = "";
                Sorszám.Text = "";
                Táblaíró();
                Változatokbetöltése();
            }
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

        private void Újváltozat_Click(object sender, EventArgs e)
        {
            try
            {

                if (Csoportlista.Text.Trim() == "") throw new HibásBevittAdat("Nincs kitöltve a csoportok mező.");
                if (Oszlopa.Text.Trim() == "" || Oszlopa.Text.Trim().Length > 2) throw new HibásBevittAdat("Nincs kitöltve a Oszlop mező a mező maximum két betű lehet.");
                if (!int.TryParse(Sora.Text.Trim(), out int sor)) throw new HibásBevittAdat("Nincs kitöltve a Sor mező a mezőnek pozitív egész számnak kell lennie.");
                if (!int.TryParse(Szélessége.Text.Trim(), out int szélesség)) throw new HibásBevittAdat("Nincs kitöltve a Szélesság mező a mezőnek pozitív egész számnak kell lennie.");


                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Segéd\megjelenfeláll.mdb";

                List<Adat_Létszám_Elrendezés_Változatok> Adatok = Kéz_Változatok.Lista_Adatok(hely);

                if (!int.TryParse(Sorszám.Text.Trim(), out int sorszám))
                {
                    sorszám = 1;
                    if (Adatok.Count > 0) sorszám = Adatok.Max(a => a.Id) + 1;
                }

                Adat_Létszám_Elrendezés_Változatok vane = Adatok.FirstOrDefault(a => a.Id == sorszám);

                Adat_Létszám_Elrendezés_Változatok ADAT = new Adat_Létszám_Elrendezés_Változatok(sorszám,
                                                                                                 Változatoklist.Text.Trim(),
                                                                                                 Csoportlista.Text.Trim(),
                                                                                                 Oszlopa.Text.Trim(),
                                                                                                 sor,
                                                                                                 szélesség);

                if (vane != null)
                    Kéz_Változatok.Módosítás(hely, ADAT);
                else
                    Kéz_Változatok.Rögzítés(hely, ADAT);
                Csoportlista.Text = "";
                Oszlopa.Text = "";
                Sora.Text = "";
                Szélessége.Text = "";
                Sorszám.Text = "";
                Táblaíró();
                Változatokbetöltése();
            }
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



        private void Változatoklist_SelectedIndexChanged(object sender, EventArgs e)
        {
            Táblaíró();
        }


        private void Tábla_Frissít_Click(object sender, EventArgs e)
        {
            Táblaíró();
        }


        private void Táblaíró()
        {
            try
            {

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Segéd\megjelenfeláll.mdb";
                if (!Exists(hely)) return;
                string szöveg;
                if (Változatoklist.Text.Trim() == "")
                    szöveg = "SELECT * FROM Alaplista order by változatnév, id";
                else
                    szöveg = $"SELECT * FROM Alaplista WHERE  változatnév='{Változatoklist.Text.Trim()}' order by  id";

                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.ColumnCount = 6;
                Tábla.RowCount = 0;
                Tábla.Visible = false;
                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Sorszám";
                Tábla.Columns[0].Width = 100;
                Tábla.Columns[1].HeaderText = "csoportnév";
                Tábla.Columns[1].Width = 300;
                Tábla.Columns[2].HeaderText = "oszlop";
                Tábla.Columns[2].Width = 100;
                Tábla.Columns[3].HeaderText = "sor";
                Tábla.Columns[3].Width = 100;
                Tábla.Columns[4].HeaderText = "szélesség";
                Tábla.Columns[4].Width = 100;
                Tábla.Columns[5].HeaderText = "Változat";
                Tábla.Columns[5].Width = 100;

                Kezelő_Létszám_Elrendezés_Változatok Kéz = new Kezelő_Létszám_Elrendezés_Változatok();
                List<Adat_Létszám_Elrendezés_Változatok> Adatok = Kéz.Lista_Adatok(hely);

                foreach (Adat_Létszám_Elrendezés_Változatok rekord in Adatok)
                {

                    Tábla.RowCount++;
                    int i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = rekord.Id;
                    Tábla.Rows[i].Cells[1].Value = rekord.Csoportnév.Trim();
                    Tábla.Rows[i].Cells[2].Value = rekord.Oszlop.Trim();
                    Tábla.Rows[i].Cells[3].Value = rekord.Sor;
                    Tábla.Rows[i].Cells[4].Value = rekord.Szélesség;
                    Tábla.Rows[i].Cells[5].Value = rekord.Változatnév.Trim();
                }

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


        private void Csoportfeltöltés2()
        {
            try
            {
                Csoportlista.Items.Clear();
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Segéd\kiegészítő.mdb";
                string jelszó = "Mocó";
                string szöveg = "SELECT * FROM csoportbeosztás order by Sorszám";

                Csoportlista.BeginUpdate();
                Csoportlista.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "csoportbeosztás"));
                Csoportlista.EndUpdate();
            }
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


        private void Változatokbetöltése()
        {
            try
            {
                Változatoklist.Text = "";
                Változatoklist.Items.Clear();
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Segéd\megjelenfeláll.mdb";
                if (!Exists(hely)) return;

                List<Adat_Létszám_Elrendezés_Változatok> AdatokÖ = Kéz_Változatok.Lista_Adatok(hely);
                List<string> Adatok = AdatokÖ.Select(a => a.Változatnév).Distinct().ToList();

                foreach (string rekord in Adatok)
                    Változatoklist.Items.Add(rekord.Trim());

            }
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
                if (e.RowIndex < 0) return;
                int sor = int.Parse(Tábla.Rows[e.RowIndex].Cells[0].Value.ToString());
                Egy_Adatot(sor);


            }
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


        void Egy_Adatot(int sor)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Segéd\megjelenfeláll.mdb";

                List<Adat_Létszám_Elrendezés_Változatok> AdatokÖ = Kéz_Változatok.Lista_Adatok(hely);
                Adat_Létszám_Elrendezés_Változatok Adat = (from a in AdatokÖ
                                                           where a.Id == sor
                                                           select a).FirstOrDefault();

                Sorszám.Text = Adat.Id.ToString();
                Csoportlista.Text = Adat.Csoportnév.Trim();
                Oszlopa.Text = Adat.Oszlop.Trim();
                Sora.Text = Adat.Sor.ToString();
                Szélessége.Text = Adat.Szélesség.ToString();
                Változatoklist.Text = Adat.Változatnév;
            }
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


        private void Vált_Törlés_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Segéd\megjelenfeláll.mdb";
                if (Változatoklist.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiejölve törlendő változat.");
                if (MessageBox.Show($"Biztos, hogy az a {Változatoklist.Text.Trim()} változatot töröljük?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Adat_Létszám_Elrendezés_Változatok ADAT = new Adat_Létszám_Elrendezés_Változatok(0, Változatoklist.Text.Trim(), "", "", 0, 15);
                    Kéz_Változatok.Törlés(hely, ADAT);

                    Változatokbetöltése();
                    Táblaíró();
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


        private void Új_adat_Click(object sender, EventArgs e)
        {
            Sorszám.Text = "";
        }
        #endregion


        #region Jogosítvány lekérdezés
        private void BtnTáblafrissítés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Dátumtól.Value > Dátumig.Value)
                    throw new HibásBevittAdat("A kezdeti dátum nagyobb a befejező dátumnál. ");

                TáblázatListázás();
            }
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


        private void TáblázatListázás()
        {
            try
            {
                string helyjog = Application.StartupPath + @"\Főmérnökség\adatok\Főmérnökség1.mdb";
                JogTábla.Rows.Clear();
                JogTábla.Columns.Clear();
                JogTábla.Refresh();
                JogTábla.ColumnCount = 5;
                JogTábla.RowCount = 0;
                JogTábla.Visible = false;
                // fejléc elkészítése
                JogTábla.Columns[0].HeaderText = "Dolgozó neve:";
                JogTábla.Columns[0].Width = 300;
                JogTábla.Columns[1].HeaderText = "HR azonosító";
                JogTábla.Columns[1].Width = 100;
                JogTábla.Columns[2].HeaderText = "Telephely";
                JogTábla.Columns[2].Width = 100;
                JogTábla.Columns[3].HeaderText = "Mi jár le";
                JogTábla.Columns[3].Width = 400;
                JogTábla.Columns[4].HeaderText = "Lejárat dátuma";
                JogTábla.Columns[4].Width = 150;
                DateTime eredmény;

                for (int j = 0; j < TáblaDolgozónévsor.Rows.Count; j++)
                {
                    if (TáblaDolgozónévsor.Rows[j].Selected)
                    {
                        eredmény = new DateTime(1900, 1, 1);
                        // ha ki van választva a dolgozó akkor lekérdezzük a jogosítványát
                        string hely = $@"{Application.StartupPath}\{TáblaDolgozónévsor.Rows[j].Cells[2].Value.ToString().Trim()}\Adatok\Dolgozók.mdb";
                        string jelszó = "forgalmiutasítás";
                        string szöveg = "SELECT * FROM Dolgozóadatok";

                        Kezelő_Dolgozó_Alap KézDolgozó = new Kezelő_Dolgozó_Alap();
                        List<Adat_Dolgozó_Alap> AdatokDolg = KézDolgozó.Lista_Adatok(hely, jelszó, szöveg);

                        Adat_Dolgozó_Alap Elem = (from a in AdatokDolg
                                                  where a.Dolgozószám == TáblaDolgozónévsor.Rows[j].Cells[1].Value.ToStrTrim()
                                                  && a.Jogosítványérvényesség <= Dátumig.Value
                                                  && a.Jogosítványérvényesség >= Dátumtól.Value
                                                  select a).FirstOrDefault();
                        if (Elem != null) eredmény = Elem.Jogosítványérvényesség;

                        if (eredmény != new DateTime(1900, 1, 1))
                        {
                            JogTábla.RowCount++;
                            int i = JogTábla.RowCount - 1;
                            JogTábla.Rows[i].Cells[0].Value = TáblaDolgozónévsor.Rows[j].Cells[0].Value;
                            JogTábla.Rows[i].Cells[1].Value = TáblaDolgozónévsor.Rows[j].Cells[1].Value;
                            JogTábla.Rows[i].Cells[2].Value = TáblaDolgozónévsor.Rows[j].Cells[2].Value;
                            JogTábla.Rows[i].Cells[3].Value = "Jogosítvány";
                            JogTábla.Rows[i].Cells[4].Value = eredmény.ToString("yyyy.MM.dd");
                            eredmény = new DateTime(1900, 1, 1);
                        }

                        // Lekérdezzük a típust
                        Kezelő_JogosítványTípus KézJog = new Kezelő_JogosítványTípus();
                        List<Adat_JogosítványTípus> AdatokJogÖ = KézJog.Lista_Adatok();

                        List<Adat_JogosítványTípus> AdatokJog = (from a in AdatokJogÖ
                                                                 where a.Törzsszám == TáblaDolgozónévsor.Rows[j].Cells[1].Value.ToString().Trim()
                                                                 && a.Státus == false
                                                                 && a.Jogtípusérvényes <= Dátumig.Value
                                                                 && a.Jogtípusérvényes >= Dátumtól.Value
                                                                 select a).ToList();

                        foreach (Adat_JogosítványTípus rekord in AdatokJog)
                        {
                            JogTábla.RowCount++;
                            int i = JogTábla.RowCount - 1;
                            JogTábla.Rows[i].Cells[0].Value = TáblaDolgozónévsor.Rows[j].Cells[0].Value;
                            JogTábla.Rows[i].Cells[1].Value = TáblaDolgozónévsor.Rows[j].Cells[1].Value;
                            JogTábla.Rows[i].Cells[2].Value = TáblaDolgozónévsor.Rows[j].Cells[2].Value;
                            JogTábla.Rows[i].Cells[3].Value = rekord.Jogtípus.Trim();
                            JogTábla.Rows[i].Cells[4].Value = rekord.Jogtípusérvényes.ToString("yyyy.MM.dd");
                        }

                        Kezelő_JogosítványVonal KézVonal = new Kezelő_JogosítványVonal();
                        List<Adat_JogosítványVonal> AdatokvonalÖ = KézVonal.Lista_Adatok();

                        List<Adat_JogosítványVonal> Adatokvonal = (from a in AdatokvonalÖ
                                                                   where a.Törzsszám == TáblaDolgozónévsor.Rows[j].Cells[1].Value.ToString().Trim()
                                                                   && a.Státus == false
                                                                   && a.Jogvonalérv <= Dátumig.Value
                                                                   && a.Jogvonalérv >= Dátumtól.Value
                                                                   select a).ToList();

                        foreach (Adat_JogosítványVonal rekord in Adatokvonal)
                        {
                            JogTábla.RowCount++;
                            int i = JogTábla.RowCount - 1;
                            JogTábla.Rows[i].Cells[0].Value = TáblaDolgozónévsor.Rows[j].Cells[0].Value;
                            JogTábla.Rows[i].Cells[1].Value = TáblaDolgozónévsor.Rows[j].Cells[1].Value;
                            JogTábla.Rows[i].Cells[2].Value = TáblaDolgozónévsor.Rows[j].Cells[2].Value;
                            JogTábla.Rows[i].Cells[3].Value = rekord.Vonalszám.Trim() + " = " + rekord.Vonalmegnevezés.Trim();
                            JogTábla.Rows[i].Cells[4].Value = rekord.Jogvonalérv.ToString("yyyy.MM.dd");
                        }
                    }
                }
                JogTábla.Visible = true;
            }
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


        private void BtndolgozóLE_Click(object sender, EventArgs e)
        {
            TáblaDolgozónévsor.Height = 400;
            BtndolgozóLE.Visible = false;
            BtndolgozóFEL.Visible = true;
        }


        private void BtndolgozóFEL_Click(object sender, EventArgs e)
        {
            TáblaDolgozónévsor.Height = 155;
            BtndolgozóLE.Visible = true;
            BtndolgozóFEL.Visible = false;
        }


        private void BtnKijelölésátjelöl_Click(object sender, EventArgs e)
        {
            KiírjaDolgozókat();
        }


        private void BtnDolgozóMind_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < TáblaDolgozónévsor.Rows.Count; i++)
                TáblaDolgozónévsor.Rows[i].Selected = true;
        }


        private void BtnDolgozóÜres_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < TáblaDolgozónévsor.Rows.Count; i++)
                TáblaDolgozónévsor.Rows[i].Selected = false;
        }


        private void Jog_Excel_Click(object sender, EventArgs e)
        {
            try
            {

                if (JogTábla.Rows.Count <= 0)
                    return;
                string fájlexc;
                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Jogosítvány_Adatok_listája_" + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                Module_Excel.EXCELtábla(fájlexc, JogTábla, true);

                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Module_Excel.Megnyitás(fájlexc + ".xlsx");
            }
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


        private void KiírjaDolgozókat()
        {
            try
            {
                TáblaDolgozónévsor.Rows.Clear();
                TáblaDolgozónévsor.Columns.Clear();
                TáblaDolgozónévsor.Refresh();
                TáblaDolgozónévsor.ColumnCount = 3;
                TáblaDolgozónévsor.RowCount = 0;
                TáblaDolgozónévsor.Visible = false;
                // fejléc elkészítése
                TáblaDolgozónévsor.Columns[0].HeaderText = "Dolgozó neve:";
                TáblaDolgozónévsor.Columns[0].Width = 300;
                TáblaDolgozónévsor.Columns[1].HeaderText = "HR azonosító";
                TáblaDolgozónévsor.Columns[1].Width = 100;
                TáblaDolgozónévsor.Columns[2].HeaderText = "Telephely";
                TáblaDolgozónévsor.Columns[2].Width = 100;

                for (int j = 0; j < Cmbtelephely.Items.Count; j++)
                {
                    if (Cmbtelephely.GetItemChecked(j) == true)
                    {
                        string hely = $@"{Application.StartupPath}\{Cmbtelephely.Items[j]}\Adatok\Dolgozók.mdb";
                        string szöveg = "SELECT * FROM Dolgozóadatok WHERE Jogosítványszám<>'' AND kilépésiidő=#01-01-1900#";
                        string jelszó = "forgalmiutasítás";

                        Kezelő_Dolgozó_Alap Kéz = new Kezelő_Dolgozó_Alap();
                        List<Adat_Dolgozó_Alap> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                        foreach (Adat_Dolgozó_Alap rekord in Adatok)
                        {
                            TáblaDolgozónévsor.RowCount++;
                            int i = TáblaDolgozónévsor.RowCount - 1;
                            TáblaDolgozónévsor.Rows[i].Cells[0].Value = rekord.DolgozóNév.Trim();
                            TáblaDolgozónévsor.Rows[i].Cells[1].Value = rekord.Dolgozószám.Trim();
                            TáblaDolgozónévsor.Rows[i].Cells[2].Value = Cmbtelephely.Items[j].ToString();
                        }
                    }
                }
                TáblaDolgozónévsor.Visible = true;
            }
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


        private void TáblaDolgozónévsor_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            TáblaDolgozónévsor.Rows[e.RowIndex].Selected = true;
        }

        #endregion



        #region Munkakör és kiegészítő

        private void Munkakör_excel_Click(object sender, EventArgs e)
        {
            try
            {

                if (Munkakörtábla.Rows.Count <= 0)
                    return;
                string fájlexc;
                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Munkaköri_Adatok_listája_" + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                Module_Excel.EXCELtábla(fájlexc, Munkakörtábla, true);

                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Module_Excel.Megnyitás(fájlexc + ".xlsx");
            }
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


        private void PDFMunkakörfeltöltés()
        {
            try
            {

                if (RadioButton1.Checked)
                    Feorszámok();

                if (RadioButton2.Checked)
                    Részmunkakör();
                if (RadioButton3.Checked)
                    Kiegmunkakör();
            }
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


        void Feorszámok()
        {
            try
            {
                PDFMunkakör.Items.Clear();

                string hely = $@"{Application.StartupPath}\" + @"Főmérnökség\adatok\kiegészítő2.mdb";
                string jelszó = "Mocó";
                string szöveg = "SELECT * FROM feorszámok Where státus=0 ORDER BY feormegnevezés ";

                PDFMunkakör.BeginUpdate();
                PDFMunkakör.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "feormegnevezés"));
                PDFMunkakör.EndUpdate();
                PDFMunkakör.Refresh();


            }
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


        void Részmunkakör()
        {
            try
            {
                PDFMunkakör.Items.Clear();

                string hely = $@"{Application.StartupPath}\" + @"Főmérnökség\adatok\kiegészítő2.mdb";
                string jelszó = "Mocó";
                string szöveg = "SELECT * FROM részmunkakör Where státus=0 ORDER BY megnevezés ";

                PDFMunkakör.BeginUpdate();
                PDFMunkakör.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "megnevezés"));
                PDFMunkakör.EndUpdate();
                PDFMunkakör.Refresh();
            }
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


        void Kiegmunkakör()
        {
            try
            {
                PDFMunkakör.Items.Clear();

                string hely = $@"{Application.StartupPath}\" + @"Főmérnökség\adatok\kiegészítő2.mdb";
                string jelszó = "Mocó";
                string szöveg = "SELECT * FROM Kiegmunkakör Where státus=0 ORDER BY megnevezés ";

                PDFMunkakör.BeginUpdate();
                PDFMunkakör.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "megnevezés"));
                PDFMunkakör.EndUpdate();
                PDFMunkakör.Refresh();
            }
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
            Munkakörlistázás();
        }


        private void Munkakörlistázás()
        {
            try
            {
                Munkakörtábla.Rows.Clear();
                Munkakörtábla.Columns.Clear();
                Munkakörtábla.Visible = false;
                Munkakörtábla.ColumnCount = 9;
                Munkakörtábla.RowCount = 0;
                // ' fejléc elkészítése
                Munkakörtábla.Columns[0].HeaderText = "Sorszám";
                Munkakörtábla.Columns[0].Width = 80;
                Munkakörtábla.Columns[1].HeaderText = "HR azonosító";
                Munkakörtábla.Columns[1].Width = 115;
                Munkakörtábla.Columns[2].HeaderText = "Dolgozónév";
                Munkakörtábla.Columns[2].Width = 200;
                Munkakörtábla.Columns[3].HeaderText = "Tevékenység";
                Munkakörtábla.Columns[3].Width = 300;
                Munkakörtábla.Columns[4].HeaderText = "Telephely";
                Munkakörtábla.Columns[4].Width = 120;
                Munkakörtábla.Columns[5].HeaderText = "PDF név";
                Munkakörtábla.Columns[5].Width = 300;
                Munkakörtábla.Columns[6].HeaderText = "Rögzítő";
                Munkakörtábla.Columns[6].Width = 100;
                Munkakörtábla.Columns[7].HeaderText = "Rögzítés ideje";
                Munkakörtábla.Columns[7].Width = 170;
                Munkakörtábla.Columns[8].HeaderText = "Státus";
                Munkakörtábla.Columns[8].Width = 100;

                if (PDFMunkakör.Text.Trim() == "") return;
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Főmérnökség_munkakör.mdb";

                string szöveg;

                string helydolg = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Dolgozók.mdb";
                string jelszódolg = "forgalmiutasítás";
                if (Exists(hely))
                {

                    Kezelő_Munkakör Kéz = new Kezelő_Munkakör();
                    List<Adat_Munkakör> AdatokÖ = Kéz.Lista_Adatok();

                    List<Adat_Munkakör> Adatok = (from a in AdatokÖ
                                                  where a.Telephely == Cmbtelephely.Text.Trim()
                                                  && a.Megnevezés == PDFMunkakör.Text.Trim()
                                                  && a.Státus == 0
                                                  orderby a.ID
                                                  select a).ToList();

                    Kezelő_Dolgozó_Alap Kézdolg = new Kezelő_Dolgozó_Alap();
                    szöveg = "SELECT * FROM Dolgozóadatok";
                    List<Adat_Dolgozó_Alap> AdatokDolg = Kézdolg.Lista_Adatok(helydolg, jelszódolg, szöveg);

                    foreach (Adat_Munkakör rekord in Adatok)
                    {
                        Munkakörtábla.RowCount++;
                        int i = Munkakörtábla.RowCount - 1;

                        string Név = (from a in AdatokDolg
                                      where a.Dolgozószám.Trim() == rekord.HRazonosító.Trim()
                                      select a.DolgozóNév).FirstOrDefault().Trim();

                        Munkakörtábla.Rows[i].Cells[0].Value = rekord.ID;
                        Munkakörtábla.Rows[i].Cells[1].Value = rekord.HRazonosító.Trim();
                        Munkakörtábla.Rows[i].Cells[2].Value = Név;
                        Munkakörtábla.Rows[i].Cells[3].Value = rekord.Megnevezés.Trim();
                        Munkakörtábla.Rows[i].Cells[4].Value = rekord.Telephely.Trim();
                        Munkakörtábla.Rows[i].Cells[5].Value = rekord.PDFfájlnév.Trim();
                        Munkakörtábla.Rows[i].Cells[6].Value = rekord.Rögzítő.Trim();
                        Munkakörtábla.Rows[i].Cells[7].Value = rekord.Dátum.ToString("yyyy.MM.dd");
                        switch (rekord.Státus)
                        {
                            case 0:
                                {
                                    Munkakörtábla.Rows[i].Cells[8].Value = "Érvényes";
                                    break;
                                }
                            case 1:
                                {
                                    Munkakörtábla.Rows[i].Cells[8].Value = "Törölt";
                                    break;
                                }
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

            // egész sor színezése ha törölt
            foreach (DataGridViewRow row in Munkakörtábla.Rows)
            {
                if (row.Cells[8].Value.ToString().Trim() == "Törölt")
                {
                    row.DefaultCellStyle.ForeColor = Color.White;
                    row.DefaultCellStyle.BackColor = Color.IndianRed;
                    row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f, FontStyle.Strikeout);
                }
            }
        }


        private void Munkakörtábla_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (Munkakörtábla.SelectedRows.Count != 0)
                {
                    string hely = $@"{Application.StartupPath}\Főmérnökség\Munkakör\{Cmbtelephely.Text.Trim()}\" + Munkakörtábla.Rows[Munkakörtábla.SelectedRows[0].Index].Cells[5].Value.ToString();
                    if (!Exists(hely)) return;

                    Kezelő_Pdf.PdfMegnyitás(PDF_néző, hely);

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


        private void RadioButton1_Click(object sender, EventArgs e)
        {
            PDFMunkakör.Text = "";
            PDFMunkakörfeltöltés();
        }


        private void RadioButton3_Click(object sender, EventArgs e)
        {
            PDFMunkakör.Text = "";
            PDFMunkakörfeltöltés();
        }


        private void RadioButton2_Click(object sender, EventArgs e)
        {
            PDFMunkakör.Text = "";
            PDFMunkakörfeltöltés();
        }


        private void Munkakörtábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Munkakörtábla.Rows[e.RowIndex].Selected = true;
        }

        #endregion
    }
}