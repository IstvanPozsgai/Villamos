using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using static System.IO.File;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{

    public partial class AblakFelhasználó
    {
        string Másolnadó = "VENDÉG";

        readonly Kezelő_Kulcs_Fekete KézKulcs = new Kezelő_Kulcs_Fekete();
        readonly Kezelő_Belépés_Jogosultságtábla KézJog = new Kezelő_Belépés_Jogosultságtábla();
        readonly Kezelő_Belépés_Bejelentkezés KézBej = new Kezelő_Belépés_Bejelentkezés();
        readonly Kezelő_Belépés_WinTábla KézWin = new Kezelő_Belépés_WinTábla();

        List<Adat_Belépés_Jogosultságtábla> AdatokLista = new List<Adat_Belépés_Jogosultságtábla>();

        public AblakFelhasználó()
        {
            InitializeComponent();
        }

        private void AblakFelhasználó_Load(object sender, EventArgs e)
        {
            try
            {
                Telephelyekfeltöltése();
                Neveklistája();
                Fülek.TabIndex = 0;
                TextNév.Focus();
                Jogosultságkiosztás();
                Táblaíró();

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

        private void AblakFelhasználó_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kereső?.Close();
        }

        #region Alap
        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Személy(true))
                    Cmbtelephely.Items.Add(Elem);

                int i;
                i = Cmbtelephely.FindString(Program.PostásTelephely);
                Cmbtelephely.Text = Cmbtelephely.Items[i].ToStrTrim();
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

        private void BtnSugó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\felhasználó.html";
                MyE.Megnyitás(hely);
            }
            catch (HibásBevittAdat ex)
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
            // kikapcsoljuk a gombokat
            BtnÚjdolgozó.Visible = false;
            BtnDolgozótörlés.Visible = false;
            BtnÚjjelszó.Visible = false;
            BtnJogosultság.Visible = false;
            BtnVendég.Visible = false;
            Btnalapjogosultság.Visible = false;

            int melyikelem = 1;
            // módosítás 1
            if (MyF.Vanjoga(melyikelem, 1))
            {
                BtnÚjdolgozó.Visible = true;
                BtnDolgozótörlés.Visible = true;
                BtnÚjjelszó.Visible = true;
                BtnJogosultság.Visible = true;
                BtnVendég.Visible = true;
                Btnalapjogosultság.Visible = true;
            }
            // módosítás 2
            if (MyF.Vanjoga(melyikelem, 2))
            {
            }

            // módosítás 3
            if (MyF.Vanjoga(melyikelem, 3))
            {
            }
        }

        private void Lapfülek_DrawItem(object sender, DrawItemEventArgs e)
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

        private void Cmbtelephely_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Neveklistája();
            }
            catch (HibásBevittAdat ex)
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



        #region Felhasználó lapfül
        private void BtnÚjdolgozó_Click(object sender, EventArgs e)
        {
            try
            {
                string Dolgozójogköre = "";
                // rögzítjük az új dolgozót
                if (TextNév.Text.Trim() == "") throw new HibásBevittAdat("A felhasználói név nem lehet üres karakter.");
                if (!Regex.IsMatch(TextNév.Text.Trim(), "^[A-Za-záÁéÉíÍűŰúÚőŐöÖüÜ]+$")) throw new HibásBevittAdat("Nem engedélyezett karakterek a felhasználónévben!");

                string voltmár = (from a in AdatokLista
                                  where a.Név == TextNév.Text.Trim()
                                  select a.Név).FirstOrDefault();

                if (voltmár != null)
                {
                    TextNév.Focus();
                    throw new HibásBevittAdat("Van már ilyen nevű felhasználó, nem lehet létrehozni mégegyszer!");
                }


                if (TextNév.Text.Trim().Length < 3)
                {
                    TextNév.Focus();
                    throw new HibásBevittAdat("A felhasználó névnek 3 karakternél hosszabbnak kell lennie !");
                }

                if (TextNév.Text.Trim().Length > 15)
                {
                    TextNév.Focus();
                    throw new HibásBevittAdat("A felhasználó névnek 15 karakternél rövidebbnek kell lennie !");
                }

                Dolgozójogköre = "";
                // elkészítjük a 0-ás karakterláncot
                for (int i = 1; i <= 255; i++)
                    Dolgozójogköre += "0";

                Adat_Belépés_Bejelentkezés ADAT = new Adat_Belépés_Bejelentkezés(0,
                                                                 TextNév.Text.Trim().ToUpper(),
                                                                 "INIT",
                                                                 Dolgozójogköre);

                // létrehozzuk az első beállítás értékeit.

                KézBej.Rögzítés(Cmbtelephely.Text.Trim(), ADAT);

                // létrehozzuk az első beállítás értékeit.
                Adat_Belépés_Jogosultságtábla ADAT1 = new Adat_Belépés_Jogosultságtábla(TextNév.Text.Trim().ToUpper(),
                                                                                        Dolgozójogköre,
                                                                                        Dolgozójogköre);

                //   Kezelő_Belépés_Jogosultságtábla
                KézJog.Rögzítés(Cmbtelephely.Text.Trim(), ADAT1);

                Neveklistája();
                TextNév.Text = "";
                TextNév.Focus();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDolgozótörlés_Click(object sender, EventArgs e)
        {
            try
            {
                // töröljük a nevet a listából
                if (TextNév.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva törölni kívánt felhasználó.");

                Adat_Belépés_Bejelentkezés ADAT = new Adat_Belépés_Bejelentkezés(0,
                                                                                 TextNév.Text.Trim(),
                                                                                 "",
                                                                                 "");

                Adat_Belépés_Jogosultságtábla ADAT1 = new Adat_Belépés_Jogosultságtábla(TextNév.Text.Trim(), "", "");


                // rákérdezünk, hogy valóban töröljük-e

                if (MessageBox.Show("Biztos, hogy töröljük, " + TextNév.Text.Trim() + " felhasználót?", "Biztonsági kérdés", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // igent választottuk
                    KézBej.Törlés(Cmbtelephely.Text.Trim(), ADAT);
                    KézJog.Törlés(Cmbtelephely.Text.Trim(), ADAT1);
                    MessageBox.Show("Az adat törlése megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Neveklistája();
                TextNév.Text = "";
                TextNév.Focus();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnÚjjelszó_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextNév.Text.Trim() == "")
                    throw new HibásBevittAdat("Nincs kiválasztva felhasználó.");

                if (MessageBox.Show($"Biztos, hogy új jelszót adunk, {TextNév.Text.Trim()} felhasználónak?", "Felhasználó jelszó módosítás", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    // igent választottuk
                    Adat_Belépés_Bejelentkezés ADAT = new Adat_Belépés_Bejelentkezés(0,
                                                                                     TextNév.Text.Trim(),
                                                                                     "INIT",
                                                                                     "_");
                    KézBej.Módosítás(Cmbtelephely.Text.Trim(), ADAT);
                    MessageBox.Show("A jelszó 'INIT'-re változott.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                ListákFeltöltése();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Listtételek_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Listtételek.SelectedItem == null) return;
                TextNév.Text = Listtételek.SelectedItem.ToString();
                lblnév.Text = TextNév.Text;
                Újprogrambanvanadata();
                Táblaíró();
                Win_Kiír();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnVendég_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextNév.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva módosítandó dolgozó.");
                if (MessageBox.Show($"Biztos, hogy a \n {Másolnadó} \n felhasználó jogosultságait akarjuk másolni?", "Biztonsági kérdés", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    throw new HibásBevittAdat("Nem történt semmilyen módosítás.");

                // kiolvassuk a vendég jogosultságát
                string jogosultság = (from a in AdatokLista
                                      where a.Név.ToUpper() == Másolnadó
                                      select a.Jogkörúj1).FirstOrDefault();

                string jogosulságegyéni = (from a in AdatokLista
                                           where a.Név.ToUpper() == TextNév.Text.Trim()
                                           select a.Jogkörúj1).FirstOrDefault();

                Adat_Belépés_Jogosultságtábla ADAT = new Adat_Belépés_Jogosultságtábla(TextNév.Text.Trim(),
                                                                       jogosultság,
                                                                       "");


                // Ha nincs véletlenül semmi joga
                if (jogosulságegyéni == null)
                {
                    //Kezelő_Belépés_Jogosultságtábla
                    KézJog.Módosítás(Cmbtelephely.Text.Trim(), ADAT);
                    throw new HibásBevittAdat(TextNév.Text.Trim().ToUpper() + " jogosultsági körét megváltoztatta!");
                }


                if (jogosultság != null)
                {
                    string újjog = "";

                    for (int i = 0; i < jogosultság.Length; i++)
                    {
                        if (jogosulságegyéni.Substring(i, 1) == "0")
                        {
                            // ha nincs jogosultsága hozzá, akkor a vendégét másoljuk
                            újjog += jogosultság.Substring(i, 1);
                        }
                        else
                        {
                            // sajátját másoljuk
                            újjog += jogosulságegyéni.Substring(i, 1);
                        }
                    }
                    // módosítjuk a jogosultságot
                    ADAT = new Adat_Belépés_Jogosultságtábla(TextNév.Text.Trim(),
                                                             újjog,
                                                             "");
                    KézJog.Módosítás(Cmbtelephely.Text.Trim(), ADAT);
                    MessageBox.Show(TextNév.Text.Trim().ToUpper() + " jogosultsági körét megváltoztatta!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                Táblaíró();
                ListákFeltöltése();
                Másolnadó = "VENDÉG";
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btnalapjogosultság_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextNév.Text.Trim() == "")
                    throw new HibásBevittAdat("Nincs kiválasztva módosítandó dolgozó.");

                string jogosultság = "";
                // lenullázzuk a jogosultságot
                for (int i = 1; i <= 255; i++)
                    jogosultság += "0";

                Adat_Belépés_Jogosultságtábla ADAT = new Adat_Belépés_Jogosultságtábla(TextNév.Text.Trim(),
                                                                                       jogosultság,
                                                                                       "");
                KézJog.Módosítás(Cmbtelephely.Text.Trim(), ADAT);
                MessageBox.Show(TextNév.Text.Trim().ToUpper() + " jogosultsági körét megváltoztatta!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // kiírjuk az új jogosultságokat
                Táblaíró();
                ListákFeltöltése();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ListákFeltöltése()
        {

            AdatokLista = KézJog.Lista_Adatok(Cmbtelephely.Text.Trim());
        }

        private void Neveklistája()
        {
            try
            {
                AdatokLista.Clear();
                ListákFeltöltése();

                if (AdatokLista != null)
                {
                    Listtételek.Items.Clear();
                    Listtételek.BeginUpdate();
                    foreach (Adat_Belépés_Jogosultságtábla Elem in AdatokLista)
                        Listtételek.Items.Add(Elem.Név);

                    Listtételek.EndUpdate();
                    Listtételek.Refresh();
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

        private void Felhasználómásolása_Click(object sender, EventArgs e)
        {
            try
            {
                if (Listtételek.Items.Contains(TextNév.Text.Trim().ToUpper()))
                    Másolnadó = TextNév.Text.Trim();
                else
                    Másolnadó = "VENDÉG";
                ToolTip1.SetToolTip(BtnVendég, $"{Másolnadó} jogosultságainak másolása");
            }
            catch (HibásBevittAdat ex)
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


        #region Jogosultság lapfül
        private void Táblaíró()
        {
            try
            {
                // feltöltjük a menük nevét

                string[] menünév = new string[255];
                string[] menüleírás = new string[255];

                // beállítjuk a kezdőértéket
                for (int i = 0; i <= 250; i++)
                {
                    menünév[i] = "";
                    menüleírás[i] = "";
                }
                menünév[1] = "Felhasználók beállítása";
                menüleírás[1] = "Program adatok/ Felhasználók 1- módosítások, 2- nincs, 3-nincs";
                menünév[2] = "Program adatok Kiadási adatok telephelyi";
                menüleírás[2] = "Program adatok/ Program adatok kiadási adatok 1- módosítások, 2- nincs, 3-nincs";
                menünév[3] = "Program adatok Kiadási adatok Szakszolgálati";
                menüleírás[3] = " 1- I szakszolgálat módosítási, 2- II szakszolgálat módosítási, 3-III szakszolgálat módosítási";
                menünév[4] = "Program adatok Kiadási adatok Főmérnökség";
                menüleírás[4] = "1-módosítások, 2- nincs, 3-nincs";
                menünév[5] = "Program adatok Személy";
                menüleírás[5] = " 1- Oktatási gombok, 2- Feor számok rögzítése, 3- Jogosítvány típusok";
                menünév[6] = "Program adatok Személy";
                menüleírás[6] = " 1- Jogosítvány vonalak, 2-Szervezeti lap , 3- Dokumentum lap";
                menünév[7] = "Ciklusrend";
                menüleírás[7] = " 1- Rögzítés, 2-nincs , 3- nincs";
                menünév[8] = "Program adatok Személy";
                menüleírás[8] = " 1- Védő, 2- Gondnok , 3- Eszköz lap";
                // 9
                // 10
                menünév[11] = "Váltós munkarend és Túlóra";
                menüleírás[11] = " 1- beosztáskód, 2-túlóra keret , 3- éves összesítő";
                menünév[12] = "Váltós munkarend és Túlóra";
                menüleírás[12] = " 1- csoport turnusok, 2-Munkaidő naptár , 3- Váltós naptár";
                menünév[13] = "Váltós munkarend és Túlóra";
                menüleírás[13] = " 1- elvont napok, 2-váltós munkarend , 3- csopveznevek";
                menünév[14] = "Váltós munkarend és Túlóra";
                menüleírás[14] = " 1-  éjszakás munkarend, 2- , 3- ";
                menünév[15] = "Program adatok Egyéb";
                menüleírás[15] = " 1- SAP-FORTE Rögzítési gombok, 2- Osztály rögzítési gomb, 3-Jármű takarítás rögzítés";
                menünév[16] = "Technológiai adatok";
                menüleírás[16] = " 1- Adatok szerkesztésén rögzítési gombok, 2- Beállítási lapon rögzítési gombok, 3-Jármű altípusok beállítása";
                //17
                // 18
                // 19
                menünév[20] = "Menetkimaradás kezelés";
                menüleírás[20] = " 1- SAP Adatok feltöltése, 2- Adatok kézi módosítása, 3-Főmérnökségi megjelenítés";
                menünév[21] = "Menetkimaradás kezelés";
                menüleírás[21] = "0- E-mail küldés 1- I szakszolgálat , 2- II szakszolgálat, 3-III szakszolgálat";
                menünév[22] = "Beosztás";
                menüleírás[22] = " 1- Múlt módosítás , 2- Jelen módosítás, 3- jövő módosítás";
                menünév[23] = "Beosztás-Túlóra módosítás";
                menüleírás[23] = " 1- rögzítés , 2- törlés, 3- ";
                menünév[24] = "Beosztás-Csúsztatás módosítás";
                menüleírás[24] = " 1- rögzítés , 2- törlés, 3- ";
                menünév[25] = "Beosztás-Szabadság/megjegyzés módosítás";
                menüleírás[25] = " 1- Szabadság , 2- Megjegyzés, 3- ";
                menünév[26] = "Beosztás-AFT módosítás";
                menüleírás[26] = " 1- rögzítés , 2- törlés, 3- ";
                menünév[27] = "Beosztás-gombok";
                menüleírás[27] = " 1- Excel készítés , 2- Éves Beosztás, 3- Törli a tábla beosztását. ";
                menünév[28] = "Beosztás-gombok";
                menüleírás[28] = " 1- Adatok egyesztetése 2- Nappalos beosztás rögzítés , 3- ";
                // 29
                // 30
                // 31
                // 32
                // 33
                // 34
                // 35
                // 36
                // 37
                // 38
                // 39
                // 40
                // 41
                // 42
                // 43
                // 44
                // 45
                // 46
                // 47
                // 48
                // 49
                // 50
                // 51
                // 52
                // 53
                // 54
                // 55
                // 56
                // 57
                // 58
                // 59
                menünév[60] = "Jelenléti ív készítés";
                menüleírás[60] = " 1- nincs , 2- nincs, 3- nincs";
                menünév[61] = "Szabadság-Túlóra-Csúsztatás-Beteg-AFT";
                menüleírás[61] = " 1- Szabadság nyomtatás, leadott , 2- Alap és kiegészító szabadság, 3- Szabadságnyilatkozat";
                menünév[62] = "Szabadság-Túlóra-Csúsztatás-Beteg-AFT";
                menüleírás[62] = " 1- Túlóra leadás gomb , 2- Túlóra nyomtatás, 3- Nincs";
                menünév[63] = "Szabadság-Túlóra-Csúsztatás-Beteg-AFT";
                menüleírás[63] = " 1- Szabad telephely választás , 2- Nincs, 3- Nincs";
                menünév[64] = "Oktatások adminisztrációja";
                menüleírás[64] = " 1- Elrendelés rögzítés , 2- elrendelés törlés, átütemezés, 3-Adminisztráció mentés, jelenléti ív, e-mail küldés";
                menünév[65] = "Oktatások adminisztrációja";
                menüleírás[65] = " 1-Oktatás tény rögzítés , 2- Oktatás tény törlése , 3- nincs";
                menünév[66] = "Dolgozói adatok";
                menüleírás[66] = " 1-Csoport adatok rögzítése , 2- Jogosítvány adatok, 3- Munkakör módosítás";
                menünév[67] = "Dolgozói adatok";
                menüleírás[67] = " 1-Bér adatok rögzítése , 2- Túlóra engedély, 3- PDF feltöltés a dokumentumok alá";
                menünév[69] = "Dolgozó felvétel-átvétel-vezénylés";
                menüleírás[69] = " 1- Dolgozó ki/belépés, 2- Telephelyre ki/bevétel, 3-Vezénylés készítés/törlés";
                // 70
                // 71
                // 72
                // 73
                // 74
                menünév[75] = "Dolgozó Létszámgazdálkodás";
                menüleírás[75] = " 1- Részleges rögzítési gombok, 2- Mindent rögzítés teljes, 3-Státus létrehozás/törlés";
                menünév[76] = "Dolgozó Létszámgazdálkodás";
                menüleírás[76] = " 1- Bér frissítés, 2- , 3-";
                menünév[77] = "Dolgozói Lekérdezések";
                menüleírás[77] = " 1- , 2- , 3-";
                menünév[78] = "Munkaidőkeret és túlóra";
                menüleírás[78] = " 1- Túlóra feladás, 2- , 3-";
                menünév[79] = "Beosztás Napló";
                menüleírás[79] = " 1- , 2- , 3-";
                menünév[80] = "Munkalap Admin felület - Munkafolyamat";
                menüleírás[80] = " 1- rögzítés- törlés- visszaállítás  , 2- csoportos rendelés szám csere , 3- Adatbázis tisztítás";
                menünév[81] = "Munkalap Admin felület -Munkarend";
                menüleírás[81] = " 1- rögzítés- törlés- visszaállítás  , 2-, 3- ";
                menünév[82] = "Munkalap Admin felület -Munkarend";
                menüleírás[82] = " 1- rögzítés-   , 2-  , 3- ";
                // 83
                // 84
                menünév[85] = "Munkalap készítés";
                menüleírás[85] = " 1- rögzítés-   , 2-  , 3- ";
                menünév[86] = "Munkalap elszámolás";
                menüleírás[86] = " 1- rögzítés, 2- rendelésszám módosítás, 3- Napi adatok törlése";
                menünév[87] = "Munkalap elszámolás Napi munkaidő adatok lapfül";
                menüleírás[87] = " 1-Rögzítés , 2- Törlés, 3- Rendezés";
                menünév[88] = "Munkalap elszámolás Napi munkaidő lista lapfül";
                menüleírás[88] = " 1-Rögzítés , 2- Törlés, 3- Rendezés";
                menünév[89] = "Jármű Reklám";
                menüleírás[89] = " 1-Rögzítés  , 2-Törlés  , 3- Ragasztási tilalom ";
                menünév[90] = "Jármű adatok";
                menüleírás[90] = " 1- Új jármű Rögzítés , 2- Jármű törlés , 3- Jármű módosítás ";
                menünév[91] = "Jármű átadás-átvétel";
                menüleírás[91] = " 1- Állományból kirak , 2- Állományba vesz , 3- PDF feltöltés ";
                menünév[92] = "Sérülés nyilvántartás Menüállító";
                menüleírás[92] = " 1- Létrehozás/módosítás, 2- Fénykép/PDF feltöltés , 3-Állandó értékek/ Tarifa rögzítés ";
                menünév[93] = "Sérülés nyilvántartás";
                menüleírás[93] = " 1- Jelentés visszaállítás, 2- Kölstégkim. elkészült, 3-SAP adatok feltöltése ";
                menünév[94] = "Sérülés nyilvántartás";
                menüleírás[94] = " 1- Külső költség adatok, 2-Fénykép törlés , 3- PDF törlés";
                menünév[95] = "Sérülés nyilvántartás Főmérnökségi bejelentekzéssel";
                menüleírás[95] = " 1- Képek lementés, 2- CAF adatok rögzítése, 3- nincs";
                menünév[96] = "Főkönyv Adattábla módosítások";
                menüleírás[96] = " 1- ZSER módosítás , 2-NAPI adat módosítás, törlés , 3- jármű létrehozása, adatokmásolás";
                menünév[97] = "Főkönyv ";
                menüleírás[97] = " 1- Visszamenő főkönyv , 2-Lekérdezés gombok , 3-Főkönyv készítés";
                menünév[98] = "Főkönyv ";
                menüleírás[98] = " 1- Program adatok fordítása , 2- Zser beolvasás, 3- Zser összevetés";
                menünév[99] = "Jármű karbantartási adatok";
                menüleírás[99] = " 1- Rögzítés , 2- , 3- ";
                menünév[100] = "Szerelvényképzés";
                menüleírás[100] = " 1-Tényleges módosítási gombok , 2- Előírás módosítás, 3- Utasítás lapfül rögzítés";
                menünév[101] = "T5C5 Futásnap rögzítés";
                menüleírás[101] = " 1-módosítási gombok , 2-, 3- ";
                menünév[102] = "T5C5 Futásnap ütemezés";
                menüleírás[102] = " 1-Rögzítési gombok, 2-Törlési gombok, 3-Karbantartási adatokba írás";
                menünév[103] = "T5C5 V javítás ütemezés-  Vizsgálat Ütemező lapfül";
                menüleírás[103] = " 1-rögzítés, 2- Törlés, 3- Ütemezés";
                menünév[104] = "T5C5 V javítás ütemezés- Vonalak lapfül";
                menüleírás[104] = " 1- rögzítés, 2-Ciklus eltolás mentés, e-mail, 3- nincs";
                menünév[105] = "T5C5 V javítás ütemezés- Előírás utasítás lapfül";
                menüleírás[105] = " 1- rögzítés, 2-nincs, 3- nincs";
                menünév[106] = "T5C5 adatok módosítása";
                menüleírás[106] = " 1- Futás adatok lapfül rögzítés, 2- nincs, 3- nincs";
                menünév[107] = "T5C5 adatok módosítása";
                menüleírás[107] = " 1- Utolsó vizsgálati adatok lapfül rögzítés/törlés, 2-nincs, 3- nincs";
                menünév[108] = "Kiadási és Javítási adatok";
                menüleírás[108] = " 1- Napi adatok frissítése, 2- nincs, 3- nincs";
                menünév[109] = "&Fogaskerekű adatok és ütemezés";
                menüleírás[109] = " 1- Utolsó vizsgálati adatok lapfül rögzítés/törlés, 2-nincs, 3- nincs";
                menünév[110] = "TW6000 ütemezés és adatok";
                menüleírás[110] = " 1- járműadatok lapfül rögzítés , 2-Ütemezés lapfül Előzetes terv készítés, 3- Ütemezés lapfül Vizsgálat ütemezés ";
                menünév[111] = "TW6000 ütemezés és adatok";
                menüleírás[111] = " 1-Ütemezés lapfül törlés , 2- ütemezés lapfül telephelyi sorrend , 3- Ütemezés lapfül vizsgálatok színezése";
                menünév[112] = "TW6000 ütemezés és adatok";
                menüleírás[112] = " 1-Ütemezés részletes Esedékesség , 2-Ütemezés részletes Rögzítés, 3-";
                menünév[113] = "ICS-KCSV adatok módosítása";
                menüleírás[113] = " 1-Alapadatok lapfül rögzítés , 2-Utolsó vizsgálati adatok lapfül rögzítés/törlés, 3- Vizsgálat ütemező";
                //114
                menünév[115] = "CAF karbantartás";
                menüleírás[115] = " 1- Alap adatok rögzítése módosítása , 2- , 3- ZSER adatok göngyölés";
                menünév[116] = "CAF karbantartás";
                menüleírás[116] = " 1- Előtervet készít , 2- Előtervet töröl, 3- Előtervet véglegesít ";
                menünév[117] = "CAF karbantartás";
                menüleírás[117] = " 1- Előjegyez a hibák közé , 2- Segédtáblás módosítások, 3- ";
                menünév[118] = "CAF karbantartás";
                menüleírás[118] = " 1-Ütemezés módosítás , 2-, 3- ";
                menünév[119] = "CAF karbantartás";
                menüleírás[119] = " 1-Archíválás, 2- , 3- Színválasztás ";

                menünév[125] = "Nosztalgia";
                menüleírás[125] = " 1-, 2- , 3-  ";

                menünév[130] = "TTP";
                menüleírás[130] = " 1- Alap adatok rögzítése, 2- Munkanap beállítás, 3-Jármű alapadatok beállítása";
                menünév[131] = "TTP";
                menüleírás[131] = " 1- Jármű történte módosítás és PDF feltöltés, 2- , 3-  ";

                menünév[160] = "Kerékeszterga Karbantartás";
                menüleírás[160] = " 1- Adatok listázása/excel kimenet, 2- Alap adatok rögzítése , 3- Adatok módosítása/újak létrehozása ";

                menünév[165] = "Baross Kerékeszterga I";
                menüleírás[165] = " 1- Elkészülés és visszaállítás gombok , 2- Törölt gomb, 3- Beosztás és Esztergályosok gombok";
                menünév[166] = "Baross Kerékeszterga II";
                menüleírás[166] = " 1- Alapvető beállítások gomb , 2- Terjesztési lista és E-mail küldés, 3- Nincs";

                menünév[168] = "Baross Kerékeszterga Mérések";
                menüleírás[168] = " 1- Beolvassa a csv fájlokat , 2- , 3- ";


                menünév[170] = "Karbantartási Munkalapok";
                menüleírás[170] = " 1- Csoportosítás, 2- , 3-Digitális munkalap";

                menünév[177] = "T5C5 Fűtés ellenőrzés";
                menüleírás[177] = " 1- Rögzítés, 2-, 3- ";
                menünév[178] = "Kidobó";
                menüleírás[178] = " 1- Forte beolvasás, 2-, 3- ";
                menünév[179] = "Állomány tábla";
                menüleírás[179] = " 1-Telephely módosítás  , 2- nincs kiosztás, 3- nincs";
                menünév[180] = "MEO kerékmérések";
                menüleírás[180] = " 1-Rögzítés  , 2- Jogosultság kiosztás, 3-";
                menünév[181] = "Jármű takarítás";
                menüleírás[181] = " 1- Alapadatok  , 2- Ütemezés, 3- Elkészült takarítás rögzítés";

                menünév[183] = "Főmérnökségi adatok";
                menüleírás[183] = " 1- , 2-, 3-";
                menünév[184] = "Telephelyi adatok összesítése";
                menüleírás[184] = " 1-Adatok módosítás, rögzítése , 2-, 3-";
                menünév[185] = "Kiadási Forte adatok feltöltése Főmérnökség";
                menüleírás[185] = " 1-Adatok módosítás, rögzítése , 2-, 3-";
                menünév[186] = "Kerékméretek nyilvántartása";
                menüleírás[186] = " 1- Adatok Exportálása SAP-ba , 2- nincs , 3- Főmérnökségi rögzítés";
                menünév[187] = "Kerékméretek nyilvántartása";
                menüleírás[187] = " 1- új adat rögzítés , 2- nyomtatvány készítés , 3- nincs";
                menünév[188] = "Digitális Főkönyv";
                menüleírás[188] = " 1- nincs , 2- nincs, 3- nincs";
                menünév[189] = "SAP Osztály adatok";
                menüleírás[189] = " 1- SAP Adatok frissítése , 2- Telephelyek frissítése, 3- ";
                menünév[189] = "SAP Osztály adatok";
                menüleírás[189] = " 1- SAP Adatok frissítése , 2- Telephelyek frissítése, 3- ";
                menünév[190] = "Akkumulátor nyilvántartás";
                menüleírás[190] = " 1- Akku rögzítés , 2- Mérés rögzítés, 3- Telephely választó aktív";
                menünév[191] = "Akkumulátor nyilvántartás";
                menüleírás[191] = " 1- Akku státus módosítás , 2-Jármű ellenőrzése, hogy a telephelyen van-e , 3- ";

                menünév[200] = "Üzenetek olvasása";
                menüleírás[200] = " 1- Rögzítés , 2- Főmérnökségi telephely választó aktív, 3- Szakszolgálati telephely választó aktív";
                menünév[202] = "Utasítások olvasása";
                menüleírás[202] = " 1- olvasási visszaigazolás , 2- Főmérnökségi telephely választó aktív, 3- Szakszolgálati telephely választó aktív";
                menünév[203] = "Utasítások olvasása";
                menüleírás[203] = " 1- Utasítás rögzítés , 2- Utasítás visszavonás, 3- Nincs";


                menünév[210] = "Fődarab Nóta";
                menüleírás[210] = " 1-SAP adatok frissítése, 2-Módosítások, 3- ";

                menünév[220] = "Rezsi raktár";
                menüleírás[220] = " 1-Alapadatok rögzítése, 2-Tárolási hely rögzítése, 3- Képfeltöltés";
                menünév[221] = "Rezsi raktár";
                menüleírás[221] = " 1-Beraktározás , 2-Anyagkiadás , 3- ";

                menünév[228] = "Eszköz nyilvántartás";
                menüleírás[228] = " 1- SAP adatok betöltése, 2- Besorolás megváltoztatása , 3-  ";
                menünév[229] = "Épület tartozék nyilvántartás";
                menüleírás[229] = " 1- Épület létrehozás, 2- Épületkönyv létrehozás, 3-  Könyvelés";
                menünév[230] = "Szerszám nyilvántartás";
                menüleírás[230] = " 1- Szerszám létrehozás, 2- Szerszámkönyv létrehozás, 3-  Könyvelés";

                menünév[234] = "Épülettakarítás havi rögzítéssek";
                menüleírás[234] = " 1- Tervlap rögzítés, 2- Tény rögzítés, 3-  Naptár rögzítés";
                menünév[235] = "Épülettakarítás alap";
                menüleírás[235] = " 1- Osztály módosítás, 2- Helység módosítás, 3-  Részletes módosítás";
                // 236 tartalék


                menünév[237] = "Védőeszköz nyilvántartás";
                menüleírás[237] = " 1- Védőeszköz létrehozás, 2- Védőkönyv létrehozás, 3-  Könyvelés";
                // 238 tartalék
                menünév[240] = "Behajtási Adminisztrátori lapfül";
                menüleírás[240] = " 1- Kérelem okainak módosítása , 2- Értesítési e-mail, 3-  Alapadatok módosítása";
                menünév[241] = "Behajtási Adminisztrátori lapfül";
                menüleírás[241] = " 1- dolgozók frissítése , 2- , 3- ";
                menünév[242] = "Behajtási Gondnok/ Szakszolgálat lapfül";
                menüleírás[242] = " 1-  Gondnoki eng., 2- Szakszolgálati eng., 3- ";
                menünév[243] = "Behajtási Kérelem lapfül";
                menüleírás[243] = " 1-  Új szám generálás, 2- PDF feltöltés, 3- Rögzítés ";
                menünév[244] = "Behajtási Lista lapfül";
                menüleírás[244] = " 1- e-mail értesítés küldés, 2- engedély nyomtatás , 3- átvételi elismervény nyomtatás ";
                menünév[245] = "Behajtási lista lapfül";
                menüleírás[245] = " 1- Átvételre küldés , 2- Készre jelentés, 3- Törlés  ";
                menünév[247] = "Külsős Behajtási lista lapfül";
                menüleírás[247] = " 1-Autó rögzítés , 2- Dolgozó rögzítés , 3-   ";
                menünév[248] = "Külsős Behajtási lista lapfül";
                menüleírás[248] = " 1-Telephely rögzítés, 2- Cégadatok rögzítése, 3-Engedélyezésre továbbít";
                menünév[249] = "Külsős Behajtási lista lapfül";
                menüleírás[249] = " 1- Engedélyezés, 2- Elutasítás , 3- Visszavonás";

                Tábla.Rows.Clear();
                Tábla.ColumnCount = 7;
                Tábla.RowCount = 250;
                Tábla.Columns[0].HeaderText = "Srsz";
                Tábla.Columns[0].Width = 45;
                Tábla.Columns[1].HeaderText = "Menünév";
                Tábla.Columns[1].Width = 400;
                Tábla.Columns[2].HeaderText = "Elérési út";
                Tábla.Columns[2].Width = 600;
                Tábla.Columns[3].HeaderText = "Megjel.";
                Tábla.Columns[3].Width = 70;
                Tábla.Columns[4].HeaderText = "Mód. 1";
                Tábla.Columns[4].Width = 70;
                Tábla.Columns[5].HeaderText = "Mód. 2";
                Tábla.Columns[5].Width = 70;
                Tábla.Columns[6].HeaderText = "Mód. 3";
                Tábla.Columns[6].Width = 70;
                // kiírjuk az üres táblázatot
                for (int i = 1; i <= 250; i++)
                {
                    Tábla.Rows[i - 1].Cells[0].Value = i;
                    Tábla.Rows[i - 1].Cells[1].Value = menünév[i].Trim();
                    Tábla.Rows[i - 1].Cells[2].Value = menüleírás[i].Trim();
                }


                if (TextNév.Text.Trim() == "")
                    return;

                // Megkeressük a dolgozót és kiíjuk a jogosultságait
                List<Adat_Belépés_Jogosultságtábla> Adatok = KézJog.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adat_Belépés_Jogosultságtábla rekord = (from a in Adatok
                                                        where a.Név == TextNév.Text.Trim()
                                                        select a).FirstOrDefault();
                if (rekord != null)
                {
                    for (int i = 0; i < Tábla.RowCount; i++)
                    {
                        switch (rekord.Jogkörúj1.Substring(i, 1))
                        {
                            case "0":
                                {
                                    Tábla.Rows[i].Cells[3].Value = false;
                                    Tábla.Rows[i].Cells[4].Value = false;
                                    Tábla.Rows[i].Cells[5].Value = false;
                                    Tábla.Rows[i].Cells[6].Value = false;
                                    break;
                                }
                            case "1":
                                {
                                    Tábla.Rows[i].Cells[3].Value = true;
                                    Tábla.Rows[i].Cells[4].Value = false;
                                    Tábla.Rows[i].Cells[5].Value = false;
                                    Tábla.Rows[i].Cells[6].Value = false;
                                    break;
                                }
                            case "3":
                                {
                                    Tábla.Rows[i].Cells[3].Value = true;
                                    Tábla.Rows[i].Cells[4].Value = true;
                                    Tábla.Rows[i].Cells[5].Value = false;
                                    Tábla.Rows[i].Cells[6].Value = false;
                                    break;
                                }
                            case "5":
                                {
                                    Tábla.Rows[i].Cells[3].Value = true;
                                    Tábla.Rows[i].Cells[4].Value = false;
                                    Tábla.Rows[i].Cells[5].Value = true;
                                    Tábla.Rows[i].Cells[6].Value = false;
                                    break;
                                }
                            case "7":
                                {
                                    Tábla.Rows[i].Cells[3].Value = true;
                                    Tábla.Rows[i].Cells[4].Value = true;
                                    Tábla.Rows[i].Cells[5].Value = true;
                                    Tábla.Rows[i].Cells[6].Value = false;
                                    break;
                                }
                            case "9":
                                {
                                    Tábla.Rows[i].Cells[3].Value = true;
                                    Tábla.Rows[i].Cells[4].Value = false;
                                    Tábla.Rows[i].Cells[5].Value = false;
                                    Tábla.Rows[i].Cells[6].Value = true;
                                    break;
                                }
                            case "b":
                                {
                                    Tábla.Rows[i].Cells[3].Value = true;
                                    Tábla.Rows[i].Cells[4].Value = true;
                                    Tábla.Rows[i].Cells[5].Value = false;
                                    Tábla.Rows[i].Cells[6].Value = true;
                                    break;
                                }
                            case "d":
                                {
                                    Tábla.Rows[i].Cells[3].Value = true;
                                    Tábla.Rows[i].Cells[4].Value = false;
                                    Tábla.Rows[i].Cells[5].Value = true;
                                    Tábla.Rows[i].Cells[6].Value = true;
                                    break;
                                }
                            case "f":
                                {
                                    Tábla.Rows[i].Cells[3].Value = true;
                                    Tábla.Rows[i].Cells[4].Value = true;
                                    Tábla.Rows[i].Cells[5].Value = true;
                                    Tábla.Rows[i].Cells[6].Value = true;
                                    break;
                                }
                        }

                    }
                }
                Tábla.ClearSelection();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnJogosultság_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextNév.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva felhasználó");
                //mielőtt rögzítjük sorba rendezzük az első oszlop szerint
                Tábla.Sort(Tábla.Columns[0], System.ComponentModel.ListSortDirection.Ascending);

                string betű;
                int a, b, c, d, érték;
                string jogosultság = "";
                for (int i = 0; i < Tábla.RowCount; i++)
                {
                    betű = "0";
                    if (bool.Parse(Tábla.Rows[i].Cells[3].Value.ToString()))
                        a = 1;
                    else
                        a = 0; // megjelenítés
                    if (bool.Parse(Tábla.Rows[i].Cells[4].Value.ToString()))
                        b = 1;
                    else
                        b = 0; // módosítás 1
                    if (bool.Parse(Tábla.Rows[i].Cells[5].Value.ToString()))
                        c = 1;
                    else
                        c = 0; // módosítás 2
                    if (bool.Parse(Tábla.Rows[i].Cells[6].Value.ToString()))
                        d = 1;
                    else
                        d = 0; // módosítás 3
                               // ha nincs megjelenítés akkor minden érték 0!
                    if (a == 0)
                    {
                        b = 0;
                        c = 0;
                        d = 0;
                    }
                    érték = a * 1 + b * 2 + c * 4 + d * 8;
                    if (érték > 9)
                    {
                        switch (érték)
                        {
                            case 10:
                                {
                                    betű = "a";
                                    break;
                                }
                            case 11:
                                {
                                    betű = "b";
                                    break;
                                }
                            case 12:
                                {
                                    betű = "c";
                                    break;
                                }
                            case 13:
                                {
                                    betű = "d";
                                    break;
                                }
                            case 14:
                                {
                                    betű = "e";
                                    break;
                                }
                            case 15:
                                {
                                    betű = "f";
                                    break;
                                }
                        }
                    }
                    else
                    {
                        betű = érték.ToString();
                    }
                    jogosultság += betű;
                }

                Adat_Belépés_Jogosultságtábla ADAT = new Adat_Belépés_Jogosultságtábla(TextNév.Text.Trim(),
                                                                                       jogosultság,
                                                                                       "");
                List<Adat_Belépés_Jogosultságtábla> Adatok = KézJog.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adat_Belépés_Jogosultságtábla rekord = (from aa in Adatok
                                                        where aa.Név == TextNév.Text.Trim()
                                                        select aa).FirstOrDefault();

                if (rekord != null)
                {
                    KézJog.Módosítás(Cmbtelephely.Text.Trim(), ADAT);
                    MessageBox.Show(TextNév.Text.Trim().ToUpper() + " jogosultsági körét megváltoztatta!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Újprogrambanvanadata()
        {
            try
            {
                if (TextNév.Text.Trim() == "") return;

                string Elem = (from a in AdatokLista
                               where a.Név == TextNév.Text.Trim()
                               select a.Név).FirstOrDefault();

                if (Elem == null)
                {
                    string Dolgozójogköre = "";
                    // elkészítjük a 0-ás karakterláncot
                    for (int i = 1; i <= 255; i++)
                        Dolgozójogköre += "0";

                    Adat_Belépés_Jogosultságtábla ADAT = new Adat_Belépés_Jogosultságtábla(TextNév.Text.Trim().ToUpper(),
                                                                        "Dolgozójogköre",
                                                                        "Dolgozójogköre");
                    KézJog.Rögzítés(Cmbtelephely.Text.Trim(), ADAT);
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

        #region Hardver kulcs készítéshez
        private void AblakFelhasználó_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {
                    // Csepi sorrend tartandó
                    if ((int)e.KeyCode == 17)
                    {
                        Chk_CTRL.Checked = true;
                    }
                    else if ((int)e.KeyCode == 16 & Chk_CTRL.Checked == true)
                    {
                        Chk_Shift.Checked = true;
                    }
                    else if ((int)e.KeyCode == 35 & Chk_Shift.Checked == true)
                    {
                        Chk_Enter.Checked = true;
                    }
                    else if ((int)e.KeyCode == 33 & Chk_Enter.Checked == true)
                    {
                        Chk_PageUp.Checked = true;
                    }
                    else if ((int)e.KeyCode == 45 & Chk_PageUp.Checked == true)
                    {
                        Chk_Insert.Checked = true;
                    }
                    else
                    {
                        Kiürítgomb();
                    }
                    if (Chk_Insert.Checked == true)
                    {
                        Panel_titok.Visible = true;
                        Engedélyekfeltöltése();

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

        private void Kiürítgomb()
        {
            Chk_CTRL.Checked = false;
            Chk_Shift.Checked = false;
            Chk_Enter.Checked = false;
            Chk_PageUp.Checked = false;
            Chk_Insert.Checked = false;
        }

        private void Btn_Bezár_Click(object sender, EventArgs e)
        {
            Panel_titok.Visible = false;
            Kiürítgomb();

        }

        private void Engedélyekfeltöltése()
        {
            CMBMireSzemélyes.Items.Clear();
            CMBMireSzemélyes.Items.Add("A - Személyes adatok");
            CMBMireSzemélyes.Items.Add("B - Bér adatok");
            CMBMireSzemélyes.Items.Add("C - Túlóra engedélyezés");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Cmbtelephely.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva érvényes telephely");
                if (CMBMireSzemélyes.CheckedItems.Count <= 0) throw new HibásBevittAdat("Nincs kiválasztva jogosultsági profil");
                if (TextNév.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva dolgozó.");

                string helyi = $@"{Application.StartupPath}\Főmérnökség\Adatok\Villamos9.mdb";
                if (!Exists(helyi)) Adatbázis_Létrehozás.Felhasználó_Extra(helyi);

                List<Adat_Kulcs> AdatokKulcs = KézKulcs.Lista_Adatok();


                for (int i = 0; i < CMBMireSzemélyes.Items.Count; i++)
                {
                    bool volt = false;
                    if (CMBMireSzemélyes.GetItemChecked(i)) // ha be van jelölve
                    {
                        // soronként rögzítjük
                        string adat1 = TextNév.Text.Trim();
                        string adat2 = Cmbtelephely.Text.Trim();
                        string adat3 = CMBMireSzemélyes.Items[i].ToString().Trim().Substring(0, 1);
                        volt = KézKulcs.ABKULCSvan(adat1, adat2, adat3);
                        if (!volt)
                        {
                            // ha nincs ilyen adat akkor nem rögzítjük újra
                            Adat_Kulcs Adat = new Adat_Kulcs(MyF.MÁSKódol(TextNév.Text.Trim()),
                                                             MyF.MÁSKódol(Cmbtelephely.Text.Trim()),
                                                             MyF.MÁSKódol(CMBMireSzemélyes.Items[i].ToString().Trim().Substring(0, 1)));
                            KézKulcs.Rögzít(Adat);
                        }
                    }
                }

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

        private void Win_Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextNév.Text.Trim() == "") throw new HibásBevittAdat("Nincs kitöltve a Felhasználónév mező.");
                if (WinUser.Text.Trim() == "") WinUser.Text = "_";



                Adat_Belépés_WinTábla ADAT = new Adat_Belépés_WinTábla(TextNév.Text.Trim(),
                                                                       Cmbtelephely.Text.Trim(),
                                                                       WinUser.Text.Trim());
                List<Adat_Belépés_WinTábla> Adatok = KézWin.Lista_Adatok();
                if (Adatok != null)
                {
                    Adat_Belépés_WinTábla Elem = (from a in Adatok
                                                  where a.Név.ToUpper() == TextNév.Text.Trim().ToUpper()
                                                  select a).FirstOrDefault();

                    //Ha van ilyen dolgozó, akkor módosítjuk a belépését
                    if (Elem != null)
                        KézWin.Módosítás(ADAT);
                    else
                        KézWin.Rögzítés(ADAT);

                    MessageBox.Show(TextNév.Text.Trim().ToUpper() + " Windows usernev kapcsdolatát megváltoztatta!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Win_Kiír()
        {
            try
            {

                List<Adat_Belépés_WinTábla> Adatok = KézWin.Lista_Adatok();
                if (Adatok != null)
                {
                    Adat_Belépés_WinTábla Elem = (from a in Adatok
                                                  where a.Név.ToUpper() == TextNév.Text.Trim().ToUpper()
                                                  select a).FirstOrDefault();

                    //Ha van ilyen dolgozó, akkor módosítjuk a belépését
                    if (Elem != null)
                        WinUser.Text = Elem.WinUser;
                    else
                        WinUser.Text = "";

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


        #region Keresés
        Ablak_Kereső Új_Ablak_Kereső;
        private void Kereső_Click(object sender, EventArgs e)
        {
            Keresés_metódus();
        }

        void Keresés_metódus()
        {
            try
            {
                if (Új_Ablak_Kereső == null)
                {
                    Új_Ablak_Kereső = new Ablak_Kereső();
                    Új_Ablak_Kereső.FormClosed += Új_Ablak_Kereső_Closed;
                    Új_Ablak_Kereső.Top = 50;
                    Új_Ablak_Kereső.Left = 50;
                    Új_Ablak_Kereső.Show();
                    Új_Ablak_Kereső.Ismétlődő_Változás += Szövegkeresés;
                }
                else
                {
                    Új_Ablak_Kereső.Activate();
                    Új_Ablak_Kereső.WindowState = FormWindowState.Normal;
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

        private void Új_Ablak_Kereső_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kereső = null;
        }

        private void Szövegkeresés()
        {
            // megkeressük a szöveget a táblázatban
            if (Új_Ablak_Kereső.Keresendő == null) return;
            if (Új_Ablak_Kereső.Keresendő.Trim() == "") return;

            Táblaíró();
            if (Tábla.Rows.Count < 0) return;
            for (int i = 1; i < Tábla.RowCount; i++)
            {
                for (int j = 1; j < 3; j++)
                {
                    if (Tábla.Rows[i].Cells[j].Value.ToString().ToUpper().Contains(Új_Ablak_Kereső.Keresendő.Trim().ToUpper()))
                    {
                        Tábla.Rows[i].Cells[j].Style.BackColor = Color.Orange;
                        Tábla.FirstDisplayedScrollingRowIndex = i;
                        Tábla.CurrentCell = Tábla.Rows[i].Cells[1];
                    }
                }
            }
        }
        #endregion
    }
}