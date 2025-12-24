using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Adatszerkezet;
using static System.IO.File;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos
{
    public partial class Ablak_DolgozóiLekérdezések
    {
        #region Kezelők
        readonly Kezelő_Kulcs KézKulcs = new Kezelő_Kulcs();
        readonly Kezelő_Kulcs_Kettő KézKulcs2 = new Kezelő_Kulcs_Kettő();
        readonly Kezelő_Létszám_Elrendezés_Változatok Kéz_Változatok = new Kezelő_Létszám_Elrendezés_Változatok();
        readonly Kezelő_Dolgozó_Személyes KézSzemélyes = new Kezelő_Dolgozó_Személyes();
        readonly Kezelő_Kiegészítő_Csoportbeosztás KézCsop = new Kezelő_Kiegészítő_Csoportbeosztás();
        readonly Kezelő_Dolgozó_Alap KézDolg = new Kezelő_Dolgozó_Alap();
        readonly Kezelő_JogosítványTípus KézJog = new Kezelő_JogosítványTípus();
        readonly Kezelő_JogosítványVonal KézVonal = new Kezelő_JogosítványVonal();
        readonly Kezelő_Kiegészítő_Feorszámok KézFeor = new Kezelő_Kiegészítő_Feorszámok();
        readonly Kezelő_Kiegészítő_Munkakör KézKMunkakör = new Kezelő_Kiegészítő_Munkakör();
        readonly Kezelő_Munkakör KézMunkakör = new Kezelő_Munkakör();
        #endregion

        List<Adat_Kulcs> Adatok_Kulcs = new List<Adat_Kulcs>();

        readonly Beállítás_Betű Bebetű = new Beállítás_Betű();
        readonly Beállítás_Betű BebetűV = new Beállítás_Betű { Vastag = true };

        #region Alap
        public Ablak_DolgozóiLekérdezések()
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

                Fülek.SelectedIndex = 0;
                Fülekkitöltése();
                Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;

                Dátumig.Value = new DateTime(DateTime.Today.Year, 12, 31);
                Dátumtól.Value = new DateTime(DateTime.Today.Year, 1, 1);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void AblakDolgozóiLekérdezések_Load(object sender, EventArgs e)
        {

        }

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
                foreach (string Elem in Listák.TelephelyLista_Személy(true))
                    Cmbtelephely.Items.Add(Elem);
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
            Váltmódosítás.Visible = false;
            Újváltozat.Visible = false;

            melyikelem = 64;
            if (MyF.Vanjoga(melyikelem, 1))
            {
                Váltmódosítás.Visible = true;
                Újváltozat.Visible = true;
            }
            if (MyF.Vanjoga(melyikelem, 2))
            {

            }
            if (MyF.Vanjoga(melyikelem, 3))
            {

            }
        }

        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Dolgozólekérdezések.html";
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
                Alholtart.Be();
                Főholtart.Be(Cmbtelephely.Items.Count + 1);

                string helykulcs = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Villamos\Kulcs.mdb";
                bool kulcsfájlvan = false;
                List<Adat_Kulcs> AdatokKulcs = null;
                if (File.Exists(helykulcs))
                {
                    kulcsfájlvan = true;
                    AdatokKulcs = KézKulcs.Lista_Adatok();
                }

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Szakszolgálati lekérdezés",
                    FileName = $"Felépítés_Üzemenként_{DateTime.Now:yyyyMMddhhmmss}",
                    Filter = "Excel |*.xlsx"
                };
                string fájlexc;
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                // létrehozzuk az excel táblát
                string munkalap = "Összesítő";
                MyX.ExcelLétrehozás(munkalap);
                MyX.Munkalap_betű(munkalap, Bebetű);

                // ****************************************************
                // elkészítjük a lapokat
                // ****************************************************
                for (int i = 0; i < Cmbtelephely.CheckedItems.Count; i++)
                    MyX.Munkalap_Új(Cmbtelephely.CheckedItems[i].ToString());

                int öoszlop = 2;
                List<Adat_Dolgozó_Személyes> AdatokSzemélyes = KézSzemélyes.Lista_Adatok();

                for (int ii = 0; ii < Cmbtelephely.CheckedItems.Count; ii++)
                {
                    bool személyeseng = false;
                    bool béreng = false;
                    string Telephely = Cmbtelephely.CheckedItems[ii].ToString();
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
                    MyX.Munkalap_aktív(munkalap);
                    MyX.Munkalap_betű(munkalap, Bebetű);

                    // elkészítjük a fejlécet
                    MyX.Kiir("Sorszám", "a1");
                    MyX.Kiir("Név", "b1");
                    MyX.Kiir("Munkakör", "c1");
                    MyX.Kiir("HR törzsszám", "d1");
                    MyX.Kiir("Születési idő", "e1");
                    MyX.Kiir("Belépési idő", "f1");
                    MyX.Kiir("Bér", "g1");
                    MyX.Kiir("Csoport", "h1");
                    MyX.Kiir("Passzív", "i1");
                    MyX.Kiir("Alkalmazott/fizikai", "j1");
                    MyX.Kiir("Ide vezényelt", "k1");
                    MyX.Kiir("Elvezényelve", "l1");
                    MyX.Kiir("Részmunkaidős", "m1");

                    // lenullázzuk
                    int fizikai = 0;
                    int alkalmazott = 0;
                    int Vezényelt = 0;
                    int Vezényelve = 0;
                    int Részmunkaidős = 0;
                    int passzív = 0;
                    // leellenőrizzük, hogy minden munkahely ki van-e töltve.
                    Munkahelyellenőrzés(Cmbtelephely.CheckedItems[ii].ToStrTrim());

                    List<Adat_Kiegészítő_Csoportbeosztás> AdatokCsop = KézCsop.Lista_Adatok(Cmbtelephely.CheckedItems[ii].ToStrTrim());
                    List<Adat_Dolgozó_Alap> AdatokDolg = KézDolg.Lista_Adatok(Cmbtelephely.CheckedItems[ii].ToStrTrim());
                    AdatokDolg = AdatokDolg.Where(a => a.Kilépésiidő == new DateTime(1900, 1, 1)).ToList();

                    int i = 2;
                    if (AdatokCsop.Count > 0 && AdatokDolg.Count > 0)
                    {
                        Alholtart.Be(AdatokCsop.Count + 1);

                        foreach (Adat_Kiegészítő_Csoportbeosztás Csoport in AdatokCsop)
                        {
                            Alholtart.Lép();
                            List<Adat_Dolgozó_Alap> CsoportTagok = AdatokDolg.Where(Elem => Elem.Csoport.Trim() == Csoport.Csoportbeosztás.Trim()).ToList();

                            foreach (Adat_Dolgozó_Alap rekord in CsoportTagok)
                            {
                                MyX.Kiir((i - 1).ToString(), $"a{i}");
                                MyX.Kiir(rekord.DolgozóNév.Trim(), $"b{i}");
                                MyX.Kiir(rekord.Munkakör.Trim(), $"c{i}");
                                MyX.Kiir(rekord.Dolgozószám.Trim(), $"d{i}");

                                if (személyeseng)
                                {
                                    Adat_Dolgozó_Személyes Elem = (from a in AdatokSzemélyes
                                                                   where a.Dolgozószám == rekord.Dolgozószám
                                                                   select a).FirstOrDefault();
                                    if (Elem != null) MyX.Kiir(Elem.Születésiidő.ToString("yyyy.MM.dd"), $"e{i}");
                                }

                                MyX.Kiir(rekord.Belépésiidő.ToString("yyyy.MM.dd"), $"f{i}");

                                if (béreng)
                                {
                                    string ideig = MyF.Rövidkód(rekord.Dolgozószám);

                                    Adatok_Kulcs = KézKulcs2.Lista_Adatok();

                                    Adat_Kulcs vane = Adatok_Kulcs.FirstOrDefault(a => a.Adat1.Contains(ideig));

                                    if (vane != null)
                                    {
                                        ideig = vane.Adat2;

                                        if (ideig != "_" && ideig != null)
                                        {
                                            string bére = MyF.Dekódolja(ideig);
                                            MyX.Kiir(bére, $"g{i}");
                                        }
                                    }
                                }
                                MyX.Kiir(rekord.Csoport, $"h{i}");
                                if (rekord.Passzív)
                                {
                                    MyX.Kiir("passzív", $"i{i}");
                                    passzív++;
                                }
                                if (rekord.Alkalmazott)
                                {
                                    MyX.Kiir("Alkalmazott", $"j{i}");
                                    alkalmazott++;
                                }
                                else
                                {
                                    MyX.Kiir("Fizikai", $"j{i}");
                                    fizikai++;
                                }
                                if (rekord.Vezényelt)
                                {
                                    MyX.Kiir("vezényelt", $"k{i}");
                                    Vezényelt++;
                                }
                                if (rekord.Vezényelve)
                                {
                                    MyX.Kiir("vezényelve", $"l{i}");
                                    Vezényelve++;
                                }
                                if (rekord.Részmunkaidős)
                                {
                                    MyX.Kiir("részmunkaidős", $"m{i}");
                                    Részmunkaidős++;
                                }
                                i += 1;
                            }
                        }

                        //Nincs csoportban
                        List<Adat_Dolgozó_Alap> NincsTagok = AdatokDolg.Where(Elem => Elem.Csoport.Trim() == "Nincs").ToList();

                        foreach (Adat_Dolgozó_Alap rekord in NincsTagok)
                        {
                            MyX.Kiir($"#SZÁME#{i - 1}", $"a{i}");
                            MyX.Kiir(rekord.DolgozóNév.Trim(), $"b{i}");
                            MyX.Kiir(rekord.Munkakör.Trim(), $"c{i}");
                            MyX.Kiir(rekord.Dolgozószám.Trim(), $"d{i}");

                            if (személyeseng)
                            {
                                Adat_Dolgozó_Személyes Elem = (from a in AdatokSzemélyes
                                                               where a.Dolgozószám == rekord.Dolgozószám
                                                               select a).FirstOrDefault();
                                if (Elem != null)
                                    MyX.Kiir(Elem.Születésiidő.ToString("yyyy.MM.dd"), $"e{i}");
                            }

                            MyX.Kiir(rekord.Belépésiidő.ToString("yyyy.MM.dd"), $"f{i}");

                            if (béreng)
                            {
                                string ideig = MyF.Rövidkód(rekord.Dolgozószám);

                                Adatok_Kulcs = KézKulcs2.Lista_Adatok();
                                Adat_Kulcs vane = Adatok_Kulcs.FirstOrDefault(a => a.Adat1.Contains(ideig));
                                ideig = vane.Adat2;

                                if (ideig != "_")
                                {
                                    MyX.Kiir(MyF.Dekódolja(ideig), $"g{i}");
                                }
                            }
                            MyX.Kiir(rekord.Csoport, $"h{i}");
                            if (rekord.Passzív)
                            {
                                MyX.Kiir("passzív", $"i{i}");
                                passzív++;
                            }
                            if (rekord.Alkalmazott)
                            {
                                MyX.Kiir("Alkalmazott", $"j{i}");
                                alkalmazott++;
                            }
                            else
                            {
                                MyX.Kiir("Fizikai", $"j{i}");
                                fizikai++;
                            }
                            if (rekord.Vezényelt)
                            {
                                MyX.Kiir("vezényelt", $"k{i}");
                                Vezényelt++;
                            }
                            if (rekord.Vezényelve)
                            {
                                MyX.Kiir("vezényelve", $"l{i}");
                                Vezényelve++;
                            }
                            if (rekord.Részmunkaidős)
                            {
                                MyX.Kiir("részmunkaidős", $"m{i}");
                                Részmunkaidős++;
                            }
                            i += 1;
                        }
                    }
                    MyX.Oszlopszélesség(munkalap, "A:M");
                    MyX.Szűrés(munkalap, "A", "M", i);
                    MyX.Rácsoz(munkalap, $"A1:m{i}");

                    i += 1;
                    MyX.Kiir("Szellemi", $"b{i}");
                    MyX.Kiir($"#SZÁME#{alkalmazott}", $"c{i}");

                    MyX.Kiir("Fizikai", $"b{i + 1}");
                    MyX.Kiir($"#SZÁME#{fizikai}", $"c{i + 1}");

                    MyX.Kiir("Összesen", $"b{i + 2}");
                    MyX.Kiir($"#SZÁME#{(fizikai + alkalmazott)}", $"c{i + 2}");

                    MyX.Kiir("Vezényelve", $"b{i + 3}");
                    MyX.Kiir($"#SZÁME#{Vezényelve}", $"c{i + 3}");

                    MyX.Kiir("vezényelt", $"b{i + 4}");
                    MyX.Kiir($"#SZÁME#{Vezényelt}", $"c{i + 4}");

                    MyX.Kiir("részmunkaidős", $"b{i + 5}");
                    MyX.Kiir($"#SZÁME#{Részmunkaidős}", $"c{i + 5}");

                    MyX.Kiir("Passzív", $"b{i + 6}");
                    MyX.Kiir($"#SZÁME#{passzív}", $"c{i + 6}");

                    MyX.Rácsoz(munkalap, $"b{i}:c{i + 6}");
                    MyX.Tábla_Rögzítés(munkalap, 1);

                    // összesítő lapra kiírjuk telephelyenként
                    munkalap = "Összesítő";
                    MyX.Munkalap_aktív(munkalap);
                    MyX.Kiir(Telephely, MyF.Oszlopnév(öoszlop) + "1");
                    MyX.Kiir($"#SZÁME#{alkalmazott}", MyF.Oszlopnév(öoszlop) + "2");
                    MyX.Kiir($"#SZÁME#{fizikai}", MyF.Oszlopnév(öoszlop) + "3");
                    MyX.Kiir($"#SZÁME#{fizikai + alkalmazott}", MyF.Oszlopnév(öoszlop) + "4");
                    MyX.Betű(munkalap, MyF.Oszlopnév(öoszlop) + "4", BebetűV);
                    MyX.Kiir($"#SZÁME#{Vezényelve}", MyF.Oszlopnév(öoszlop) + "5");
                    MyX.Kiir($"#SZÁME#{Vezényelt}", MyF.Oszlopnév(öoszlop) + "6");
                    MyX.Kiir($"#SZÁME#{Részmunkaidős}", MyF.Oszlopnév(öoszlop) + "7");
                    MyX.Kiir($"#SZÁME#{passzív}", MyF.Oszlopnév(öoszlop) + "8");

                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(öoszlop) + "1:" + MyF.Oszlopnév(öoszlop) + "8");
                    MyX.Vastagkeret(munkalap, MyF.Oszlopnév(öoszlop) + "1:" + MyF.Oszlopnév(öoszlop) + "1");
                    MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(öoszlop) + ":" + MyF.Oszlopnév(öoszlop));
                    öoszlop += 1;
                }


                munkalap = "Összesítő";
                MyX.Vastagkeret(munkalap, "a1:a1");
                MyX.Rácsoz(munkalap, "a1:a8");
                MyX.Kiir("Szellemi", "a2");
                MyX.Kiir("Fizikai", "a3");
                MyX.Kiir("Összesen", "a4");
                MyX.Betű(munkalap, "a4", BebetűV);
                MyX.Kiir("Ide vezényelve", "a5");
                MyX.Kiir("Elvezényelt", "a6");
                MyX.Kiir("részmunkaidős", "a7");
                MyX.Kiir("Passzív", "a8");
                MyX.Oszlopszélesség(munkalap, "A:A");

                // összesítő oszlop
                MyX.Kiir("Összesen:", MyF.Oszlopnév(öoszlop) + "1");
                for (int i = 2; i < 9; i++)
                    MyX.Kiir("#KÉPLET#=SUM(RC[-" + (öoszlop - 2).ToString() + "]:RC[-1])", MyF.Oszlopnév(öoszlop) + i);


                MyX.Rácsoz(munkalap, MyF.Oszlopnév(öoszlop) + "1:" + MyF.Oszlopnév(öoszlop) + "8");
                MyX.Vastagkeret(munkalap, MyF.Oszlopnév(öoszlop) + "1:" + MyF.Oszlopnév(öoszlop) + "1");
                MyX.Oszlopszélesség("Összesítő", MyF.Oszlopnév(öoszlop) + ":" + MyF.Oszlopnév(öoszlop));
                MyX.Betű(munkalap, MyF.Oszlopnév(öoszlop) + "4", BebetűV);
                MyX.Munkalap_aktív("Összesítő");
                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();

                MessageBox.Show("A fájl elkészült.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyF.Megnyitás(fájlexc);

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

                Főholtart.Be(Cmbtelephely.CheckedItems.Count + 1);
                Alholtart.Be();

                // kimeneti fájl helye és neve
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Felépítés  lekérdezés",
                    FileName = $"Felépítés_{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                // létrehozzuk az excel táblát
                string munkalap = "Összesítő";
                MyX.ExcelLétrehozás(munkalap);
                MyX.Munkalap_betű(munkalap, Bebetű);

                // ****************************************************
                // elkészítjük a lapokat
                // ****************************************************
                for (int i = 0; i < Cmbtelephely.CheckedItems.Count; i++)
                    MyX.Munkalap_Új(Cmbtelephely.CheckedItems[i].ToStrTrim());

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
                    MyX.Munkalap_aktív(munkalap);

                    List<Adat_Kiegészítő_Csoportbeosztás> Csoport = KézCsop.Lista_Adatok(telep);
                    sor = 1;
                    oszlop = -3;

                    Alholtart.Be(Csoport.Count + 2);

                    foreach (Adat_Kiegészítő_Csoportbeosztás rekordvált in Csoport)
                    {
                        Alholtart.Lép();
                        oszlop += 4;

                        List<Adat_Dolgozó_Alap> AdatDolg = KézDolg.Lista_Adatok(telep);
                        AdatDolg = AdatDolg.Where(a => a.Csoport == rekordvált.Csoportbeosztás.Trim() && a.Kilépésiidő == new DateTime(1900, 1, 1)).ToList();
                        // elkészítjük a fejlécet
                        MyX.Egyesít(munkalap, MyF.Oszlopnév(oszlop) + sor + ":" + MyF.Oszlopnév(oszlop + 3) + sor);
                        MyX.Kiir(rekordvált.Csoportbeosztás.Trim(), MyF.Oszlopnév(oszlop) + sor);

                        sor += 1;
                        MyX.Kiir("Ssz.", MyF.Oszlopnév(oszlop) + sor);
                        MyX.Kiir("Név", MyF.Oszlopnév(oszlop + 1) + sor);
                        MyX.Kiir("Feor", MyF.Oszlopnév(oszlop + 2) + sor);
                        MyX.Kiir("Munkakör", MyF.Oszlopnév(oszlop + 3) + sor);


                        foreach (Adat_Dolgozó_Alap rekord in AdatDolg)
                        {
                            sor += 1;
                            MyX.Kiir((sor - 2).ToString(), MyF.Oszlopnév(oszlop) + sor);
                            MyX.Kiir(rekord.DolgozóNév.Trim(), MyF.Oszlopnév(oszlop + 1) + sor);
                            MyX.Kiir(rekord.Feorsz.Trim(), MyF.Oszlopnév(oszlop + 2) + sor);
                            MyX.Kiir(rekord.Munkakör.Trim(), MyF.Oszlopnév(oszlop + 3) + sor);

                            if (rekord.Feorsz.Length > 1)
                            {
                                if (int.TryParse(rekord.Feorsz.Substring(0, 1), out int Szám))
                                {
                                    if (Szám < 5)
                                        MyX.Háttérszín(munkalap, MyF.Oszlopnév(oszlop) + sor + ":" + MyF.Oszlopnév(oszlop + 3) + sor, Color.MediumSlateBlue);

                                    feorössz[Szám]++;
                                }
                            }

                            // passzív színez
                            if (rekord.Passzív)
                                MyX.Háttérszín(munkalap, MyF.Oszlopnév(oszlop) + sor + ":" + MyF.Oszlopnév(oszlop + 3) + sor, Color.SlateGray);
                        }

                        if (utolsósor < sor)
                            utolsósor = sor;
                        if (sor > 1)
                        {
                            MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlop) + "1:" + MyF.Oszlopnév(oszlop + 3) + sor);
                            MyX.Vastagkeret(munkalap, MyF.Oszlopnév(oszlop) + "1:" + MyF.Oszlopnév(oszlop + 3) + "1");
                        }
                        MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlop) + "1:" + MyF.Oszlopnév(oszlop + 3) + "2");
                        MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(oszlop) + ":" + MyF.Oszlopnév(oszlop + 3));
                        sor = 1;
                    }

                    // kiírjuk az öszesítő táblákat
                    sor = utolsósor + 2;

                    MyX.Kiir("Létszám összetétel", "b" + sor);
                    MyX.Egyesít(munkalap, "a" + sor + ":a" + sor);
                    sor += 2;
                    MyX.Kiir("Feor", "b" + sor);
                    MyX.Egyesít(munkalap, "a" + sor + ":a" + sor);
                    darab = 0;
                    for (int j = 1; j < 10; j++)
                    {
                        sor += 1;
                        MyX.Kiir("F." + j, "b" + sor);
                        MyX.Kiir(feorössz[j].ToString(), "c" + sor);
                        darab += feorössz[j];
                    }
                    sor += 1;
                    MyX.Kiir("Összesen:", "b" + sor);
                    MyX.Kiir(darab.ToString(), "c" + sor);
                    MyX.Rácsoz(munkalap, "b" + (sor - 10).ToString() + ":" + "c" + sor);
                }

                Főholtart.Ki();
                Alholtart.Ki();
                MyX.Munkalap_aktív(munkalap);

                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();

                MessageBox.Show("A fájl elkészült.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyF.Megnyitás(fájlexc);

            }
            catch (HibásBevittAdat ex)
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

                string munkalap = "Munka1";
                MyX.ExcelLétrehozás(munkalap);

                MyX.Munkalap_betű(munkalap, Bebetű);
                Holtart.Be();

                List<Adat_Létszám_Elrendezés_Változatok> AdatokLétÖ = Kéz_Változatok.Lista_Adatok(Cmbtelephely.Text.Trim());
                List<Adat_Létszám_Elrendezés_Változatok> AdatokLét = (from a in AdatokLétÖ
                                                                      where a.Változatnév == Változatoklist.Text.Trim()
                                                                      select a).ToList();

                List<Adat_Dolgozó_Alap> AdatokDolg = KézDolg.Lista_Adatok(Cmbtelephely.Text.Trim());

                // passzív
                int i = 1;
                MyX.Kiir("Passzív", $"A{i}");
                MyX.Háttérszín(munkalap, $"A{i}", Color.LightCoral);
                List<Adat_Dolgozó_Alap> Rész = (from a in AdatokDolg
                                                where a.Kilépésiidő == new DateTime(1900, 1, 1)
                                                && a.Passzív == true
                                                select a).ToList();
                if (Rész.Count > 0)
                    MyX.Kiir($"#SZÁME#{Rész.Count}", $"b{i}");
                else
                    MyX.Kiir($"#SZÁME#{0}", $"b{i}");

                i += 1;
                // vezényelt
                MyX.Kiir("El Vezényelt", $"A{i}");
                MyX.Háttérszín(munkalap, $"A{i}", Color.Cyan);
                Rész.Clear();
                Rész = (from a in AdatokDolg
                        where a.Vezényelt == true && a.Kilépésiidő == new DateTime(1900, 1, 1)
                        select a).ToList();
                if (Rész != null)
                    MyX.Kiir($"#SZÁME#{Rész.Count}", $"b{i}");
                else
                    MyX.Kiir($"#SZÁME#{0}", $"b{i}");


                i += 1;
                // vezényelve
                MyX.Kiir("Ide Vezényelve", $"A{i}");
                MyX.Háttérszín(munkalap, $"A{i}", Color.DodgerBlue);
                Rész.Clear();
                Rész = (from a in AdatokDolg
                        where a.Vezényelve == true && a.Kilépésiidő == new DateTime(1900, 1, 1)
                        select a).ToList();
                if (Rész != null)
                    MyX.Kiir($"#SZÁME#{Rész.Count}", $"b{i}");
                else
                    MyX.Kiir($"#SZÁME#{0}", $"b{i}");


                i += 1;
                // részmunkaidős
                MyX.Kiir("Részmunkaidős", $"A{i}");
                MyX.Háttérszín(munkalap, $"A{i}", Color.Lime);
                Rész.Clear();
                Rész = (from a in AdatokDolg
                        where a.Részmunkaidős == true && a.Kilépésiidő == new DateTime(1900, 1, 1)
                        select a).ToList();
                if (Rész != null)
                    MyX.Kiir($"#SZÁME#{Rész.Count}", $"b{i}");
                else
                    MyX.Kiir($"#SZÁME#{0}", $"b{i}");


                i += 1;
                // Nincs csoportban
                MyX.Kiir("Nincs csoportban", $"A{i}");
                Rész.Clear();
                Rész = (from a in AdatokDolg
                        where a.Csoport == "" && a.Kilépésiidő == new DateTime(1900, 1, 1)
                        select a).ToList();
                if (Rész != null)
                    MyX.Kiir($"#SZÁME#{Rész.Count}", $"b{i}");
                else
                    MyX.Kiir($"#SZÁME#{0}", $"b{i}");


                // csoportok létszáma
                i += 2;
                int darabö = 0;

                int k = 0;
                while (k != Csoportlista.Items.Count)
                {
                    MyX.Kiir(Csoportlista.Items[k].ToString(), $"A{i}");
                    Rész.Clear();
                    Rész = (from a in AdatokDolg
                            where a.Csoport == Csoportlista.Items[k].ToStrTrim()
                            && a.Passzív == false
                            && a.Vezényelve == false
                            && a.Részmunkaidős == false
                            && a.Kilépésiidő == new DateTime(1900, 1, 1)
                            select a).ToList();
                    if (Rész != null)
                        MyX.Kiir($"#SZÁME#{Rész.Count}", $"b{i}");
                    else
                        MyX.Kiir($"#SZÁME#{0}", $"b{i}");

                    i += 1;
                    k += 1;
                    darabö += Rész.Count;
                }
                MyX.Kiir("Összesen:", $"A{i}");
                MyX.Betű(munkalap, $"A{i}", BebetűV);
                MyX.Kiir($"#SZÁME#{darabö}", $"b{i}");
                MyX.Betű(munkalap, "b" + i, BebetűV);

                // kiirja a passzívokat
                i += 2;
                MyX.Kiir("Passzív", $"A{i}");
                i += 1;

                List<Adat_Dolgozó_Alap> Adatok = KézDolg.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adatok = (from a in AdatokDolg
                          where a.Kilépésiidő == new DateTime(1900, 1, 1)
                          && a.Passzív == true
                          select a).ToList();
                foreach (Adat_Dolgozó_Alap rekord in Adatok)
                {
                    MyX.Kiir(rekord.DolgozóNév.Trim(), $"A{i}");
                    i += 1;
                }

                // kirja a nincs csoportbant
                i += 2;
                MyX.Kiir("Nincs csoportban", $"A{i}");
                i += 1;

                AdatokDolg = KézDolg.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adatok = (from a in AdatokDolg
                          where a.Csoport == ""
                          && a.Kilépésiidő == new DateTime(1900, 1, 1)
                          select a).ToList();
                foreach (Adat_Dolgozó_Alap rekord in Adatok)
                {
                    MyX.Kiir(rekord.DolgozóNév.Trim(), $"A{i}");
                    i += 1;
                }

                int utolsósor = 0;

                int utolsóoszlop = 1;
                int sor;
                // csoportonkénti kiírás
                foreach (Adat_Létszám_Elrendezés_Változatok Ábrázol in AdatokLét)
                {
                    Adatok = (from a in AdatokDolg
                              where a.Csoport == Ábrázol.Csoportnév.Trim()
                              && a.Kilépésiidő == new DateTime(1900, 1, 1)
                              select a).ToList();
                    sor = Ábrázol.Sor;
                    //csoportnév
                    MyX.Kiir(Ábrázol.Csoportnév.Trim(), Ábrázol.Oszlop + sor);
                    MyX.Betű(munkalap, Ábrázol.Oszlop + sor, BebetűV);
                    MyX.Háttérszín(munkalap, Ábrázol.Oszlop + sor, Color.Silver);

                    //Csoport tagjai
                    sor++;
                    foreach (Adat_Dolgozó_Alap rekord in Adatok)
                    {
                        MyX.Kiir(rekord.DolgozóNév.Trim(), Ábrázol.Oszlop + sor);
                        if (rekord.Vezényelve) MyX.Háttérszín(munkalap, Ábrázol.Oszlop + sor, Color.DodgerBlue);
                        if (rekord.Vezényelt) MyX.Háttérszín(munkalap, Ábrázol.Oszlop + sor, Color.Cyan);
                        if (rekord.Részmunkaidős) MyX.Háttérszín(munkalap, Ábrázol.Oszlop + sor, Color.Lime);
                        if (rekord.Passzív) MyX.Háttérszín(munkalap, Ábrázol.Oszlop + sor, Color.LightCoral);
                        if (rekord.Csopvez) MyX.Betű(munkalap, Ábrázol.Oszlop + sor, BebetűV);
                        sor += 1;
                        Holtart.Lép();
                    }
                    if (utolsósor < sor) utolsósor = sor;
                    if (Ábrázol.Oszlop.Length == 1)
                    {
                        int oszlop = (int)(char.Parse(Ábrázol.Oszlop.Substring(0, 1).ToUpper())) - 64;
                        if (utolsóoszlop < oszlop)
                            utolsóoszlop = oszlop;
                    }
                    MyX.Oszlopszélesség(munkalap, Ábrázol.Oszlop + ":" + Ábrázol.Oszlop, Ábrázol.Szélesség);
                    Holtart.Lép();
                }


                MyX.Oszlopszélesség(munkalap, "A:A", 25);

                MyX.Rácsoz(munkalap, "A1:" + MyF.Oszlopnév(utolsóoszlop) + utolsósor);
                Beállítás_Nyomtatás BeNyom = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"A1:{MyF.Oszlopnév(utolsóoszlop)}{utolsósor}",
                    Álló = false,
                    LapMagas = 1,
                    LapSzéles = 1
                };
                MyX.NyomtatásiTerület_részletes(munkalap, BeNyom);
                MyX.Aktív_Cella(munkalap, "A1");
                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();

                Holtart.Ki();

                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MyF.Megnyitás(fájlexc);
            }
            catch (HibásBevittAdat ex)
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
                if (Változatoklist.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve egy változat név sem.");

                // Megnézzük, hogy van-e ilyen tábla
                List<Adat_Létszám_Elrendezés_Változatok> Adatok = Kéz_Változatok.Lista_Adatok(Cmbtelephely.Text.Trim());

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
                    Kéz_Változatok.Rögzítés(Cmbtelephely.Text.Trim(), ADAT);
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
                if (Változatoklist.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve törlendő elem.");
                List<Adat_Létszám_Elrendezés_Változatok> Adatok = Kéz_Változatok.Lista_Adatok(Cmbtelephely.Text.Trim());

                if (int.TryParse(Sorszám.Text.Trim(), out int sorszám))
                {
                    Adat_Létszám_Elrendezés_Változatok vane2 = Adatok.FirstOrDefault(a => a.Id == sorszám);

                    if (vane2 != null)
                    {
                        Adat_Létszám_Elrendezés_Változatok ADAT = new Adat_Létszám_Elrendezés_Változatok(sorszám, "", "", "", 0, 15);
                        Kéz_Változatok.Törlés(Cmbtelephely.Text.Trim(), ADAT);
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

                List<Adat_Létszám_Elrendezés_Változatok> Adatok = Kéz_Változatok.Lista_Adatok(Cmbtelephely.Text.Trim());

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
                    Kéz_Változatok.Módosítás(Cmbtelephely.Text.Trim(), ADAT);
                else
                    Kéz_Változatok.Rögzítés(Cmbtelephely.Text.Trim(), ADAT);
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
                List<Adat_Létszám_Elrendezés_Változatok> Adatok = Kéz_Változatok.Lista_Adatok(Cmbtelephely.Text.Trim());
                if (Változatoklist.Text.Trim() != "")
                    Adatok = (from a in Adatok
                              where a.Változatnév == Változatoklist.Text.Trim()
                              select a).ToList();
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
                List<Adat_Kiegészítő_Csoportbeosztás> Adatok = KézCsop.Lista_Adatok(Cmbtelephely.Text.Trim());
                foreach (Adat_Kiegészítő_Csoportbeosztás elem in Adatok)
                    Csoportlista.Items.Add(elem.Csoportbeosztás);
            }
            catch (HibásBevittAdat ex)
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

                List<Adat_Létszám_Elrendezés_Változatok> AdatokÖ = Kéz_Változatok.Lista_Adatok(Cmbtelephely.Text.Trim());
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

        private void Egy_Adatot(int sor)
        {
            try
            {
                List<Adat_Létszám_Elrendezés_Változatok> AdatokÖ = Kéz_Változatok.Lista_Adatok(Cmbtelephely.Text.Trim());
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
                if (Változatoklist.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiejölve törlendő változat.");
                if (MessageBox.Show($"Biztos, hogy az a {Változatoklist.Text.Trim()} változatot töröljük?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Adat_Létszám_Elrendezés_Változatok ADAT = new Adat_Létszám_Elrendezés_Változatok(0, Változatoklist.Text.Trim(), "", "", 0, 15);
                    Kéz_Változatok.Törlés(Cmbtelephely.Text.Trim(), ADAT);

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
                if (Dátumtól.Value > Dátumig.Value) throw new HibásBevittAdat("A kezdeti dátum nagyobb a befejező dátumnál. ");

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
                        List<Adat_Dolgozó_Alap> AdatokDolg = KézDolg.Lista_Adatok(TáblaDolgozónévsor.Rows[j].Cells[2].Value.ToStrTrim());

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
                if (JogTábla.Rows.Count <= 0) return;
                string fájlexc;
                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Jogosítvány_Adatok_listája_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyX.DataGridViewToXML(fájlexc, JogTábla);

                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MyF.Megnyitás(fájlexc);
            }
            catch (HibásBevittAdat ex)
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
                    if (Cmbtelephely.GetItemChecked(j))
                    {
                        List<Adat_Dolgozó_Alap> Adatok = KézDolg.Lista_Adatok(Cmbtelephely.Items[j].ToStrTrim());
                        Adatok = (from a in Adatok
                                  where a.Jogosítványszám != ""
                                  && a.Kilépésiidő == new DateTime(1900, 1, 1)
                                  select a).ToList();
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
                if (Munkakörtábla.Rows.Count <= 0) return;
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

                MyX.DataGridViewToXML(fájlexc, Munkakörtábla);

                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MyF.Megnyitás(fájlexc);
            }
            catch (HibásBevittAdat ex)
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
                if (RadioButton1.Checked) Feorszámok();
                if (RadioButton2.Checked) Részmunkakör();
                if (RadioButton3.Checked) Kiegmunkakör();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Feorszámok()
        {
            try
            {
                PDFMunkakör.Items.Clear();
                List<Adat_Kiegészítő_Feorszámok> Adatok = KézFeor.Lista_Adatok();
                Adatok = (from a in Adatok
                          where a.Státus == 0
                          orderby a.Feormegnevezés
                          select a).ToList();
                foreach (Adat_Kiegészítő_Feorszámok elem in Adatok)
                    PDFMunkakör.Items.Add(elem.Feormegnevezés);

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

        private void Részmunkakör()
        {
            try
            {
                PDFMunkakör.Items.Clear();
                List<Adat_Kiegészítő_Munkakör> Adatok = KézKMunkakör.Lista_Adatok();
                Adatok = (from a in Adatok
                          where a.Kategória == "Részmunkakör"
                          && a.Státus == false
                          orderby a.Megnevezés
                          select a).ToList();

                foreach (Adat_Kiegészítő_Munkakör item in Adatok)
                    PDFMunkakör.Items.Add(item.Megnevezés);

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

        private void Kiegmunkakör()
        {
            try
            {
                PDFMunkakör.Items.Clear();
                List<Adat_Kiegészítő_Munkakör> Adatok = KézKMunkakör.Lista_Adatok();
                Adatok = (from a in Adatok
                          where a.Kategória == "Kiegészítő"
                          && a.Státus == false
                          orderby a.Megnevezés
                          select a).ToList();

                foreach (Adat_Kiegészítő_Munkakör item in Adatok)
                    PDFMunkakör.Items.Add(item.Megnevezés);

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
                if (PDFMunkakör.Text.Trim() == "") return;

                List<Adat_Munkakör> AdatokÖ = KézMunkakör.Lista_Adatok();
                List<Adat_Dolgozó_Alap> AdatokDolg = new List<Adat_Dolgozó_Alap>();
                for (int i = 0; i < Cmbtelephely.CheckedItems.Count; i++)
                {
                    List<Adat_Dolgozó_Alap> ideig = KézDolg.Lista_Adatok(Cmbtelephely.CheckedItems[i].ToString());
                    AdatokDolg.AddRange(ideig);
                }


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

                List<Adat_Munkakör> Adatok = (from a in AdatokÖ
                                              where a.Megnevezés == PDFMunkakör.Text.Trim()
                                              && a.Státus == 0
                                              orderby a.ID
                                              select a).ToList();

                foreach (Adat_Munkakör rekord in Adatok)
                {
                    Adat_Dolgozó_Alap AdatNév = (from a in AdatokDolg
                                                 where a.Dolgozószám.Trim() == rekord.HRazonosító.Trim()
                                                 select a).FirstOrDefault();
                    if (AdatNév != null)
                    {
                        Munkakörtábla.RowCount++;
                        int i = Munkakörtábla.RowCount - 1;

                        string Név = AdatNév.DolgozóNév;

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
                Munkakörtábla_Színezés();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Munkakörtábla_Színezés()
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
                if (Munkakörtábla.SelectedRows.Count >0)
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
            if (e.RowIndex < 1) return;
            Munkakörtábla.Rows[e.RowIndex].Selected = true;
        }
        #endregion
    }
}