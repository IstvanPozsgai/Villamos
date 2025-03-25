using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    public partial class Ablak_Jármű
    {
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly Kezelő_Jármű2 Kadat2 = new Kezelő_Jármű2();
        readonly Kezelő_Jármű_Napló KadatNapló = new Kezelő_Jármű_Napló();
        readonly Kezelő_jármű_hiba Kéz_JHadat = new Kezelő_jármű_hiba();
        readonly Kezelő_Alap_Beolvasás KAAdat = new Kezelő_Alap_Beolvasás();


        List<Adat_Jármű> Adatok_Állomány = new List<Adat_Jármű>();
        List<Adat_Jármű_Napló> Adatok_Napló = new List<Adat_Jármű_Napló>();
        List<string> Szűrés = new List<string>();

        DateTime ElőzőDátum = new DateTime(1900, 1, 1);

        public Ablak_Jármű()
        {
            InitializeComponent();
        }

        #region Ablak
        private void Ablak_Átadás_átvétel_Load(object sender, EventArgs e)
        {
        }


        private void Ablak_Jármű_Shown(object sender, EventArgs e)
        {
            Visible = false;
            Cursor = Cursors.WaitCursor;

            Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;
            Refresh();
            Visible = true;
            Cursor = Cursors.Default;
            // telephely
            // megnézzük, hogy van-e hiba tábla
            string hely = $@"{Application.StartupPath}\{Program.PostásTelephely.Trim()}\Adatok\villamos\hiba.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Hibatáblalap(hely);

            // megnézzük, hogy van-e villamos... tábla
            hely = $@"{Application.StartupPath}\{Program.PostásTelephely.Trim()}\Adatok\villamos\villamos.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.KocsikTípusa(hely);

            hely = $@"{Application.StartupPath}\{Program.PostásTelephely.Trim()}\Adatok\villamos\villamos2.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Villamostábla(hely);

            hely = $@"{Application.StartupPath}\{Program.PostásTelephely.Trim()}\Adatok\villamos\villamos3.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Villamostábla3(hely);

            // Közös lista
            // megnézzük, hogy van-e közös hiba tábla
            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\hiba.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Hibatáblalap(hely);

            // megnézzük, hogy van-e villamos... tábla
            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\villamos.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.KocsikTípusa(hely);

            hely = Application.StartupPath + @"\Főmérnökség\adatok\villamos2.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Villamostábla(hely);

            hely = Application.StartupPath + @"\Főmérnökség\adatok\villamos3.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Villamostábla3(hely);

            Telephelyekfeltöltése();

            Jogosultságkiosztás();
            Főmérnökségi_Állomány_Lista();
            Kocsilistaellenőrzés();

            Fülek.SelectedIndex = 0;
            Fülekkitöltése();
        }


        private void Ablak_Jármű_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC
            if ((int)e.KeyCode == 27)
            {
                Új_Ablak_Kereső?.Close();

            }

            if (e.Control && e.KeyCode == Keys.F)
            {
                Keresés_metódus();
            }
        }
        #endregion


        #region Ellenőrzések
        private void Kocsilistaellenőrzés()
        {
            try
            {
                if (Program.PostásTelephely.Trim() == "Főmérnökség") return;
                //   leellenőrizzük, hogy a közös adatok szerint a telephelyen lévő kocsik valóban a telephelyen vannak, ha nincs a telephelyen, akkor a közös adatokban Közösre állítjuk
                // Gazdátlan kocsikat berakjuk a közösbe
                string hely = $@"{Application.StartupPath}\{Program.PostásTelephely.Trim()}\adatok\villamos\villamos.mdb";
                string jelszó = "pozsgaii";
                string helyközös = $@"{Application.StartupPath}\Főmérnökség\adatok\villamos.mdb";

                List<Adat_Jármű> AdatokFőm = (from a in Adatok_Állomány
                                              orderby a.Azonosító
                                              where a.Üzem == Program.PostásTelephely
                                              select a).ToList();

                string szöveg = "SELECT * FROM Állománytábla ORDER BY azonosító";
                List<Adat_Jármű> AdatokTelep = KézJármű.Lista_Adatok(hely, jelszó, szöveg);

                Holtart.Be();

                List<string> SzövegGy = new List<string>();
                foreach (Adat_Jármű item in AdatokFőm)
                {
                    if (!(AdatokTelep.Exists(x => x.Azonosító.Trim() == item.Azonosító.Trim())))
                    {
                        szöveg = $"UPDATE Állománytábla SET üzem='Közös' WHERE azonosító='{item.Azonosító.Trim()}'";
                        SzövegGy.Add(szöveg);
                    }
                    Holtart.Lép();
                }
                MyA.ABMódosítás(helyközös, jelszó, SzövegGy);
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
        #endregion


        #region Alap
        private void Telephelyekfeltöltése()
        {
            Cmbtelephely.Items.Clear();
            foreach (string Elem in Listák.TelephelyLista_Jármű())
                Cmbtelephely.Items.Add(Elem);

            if (Program.PostásTelephely == "Főmérnökség" || Program.PostásTelephely.Contains("törzs"))
                Cmbtelephely.Text = Cmbtelephely.Items[0].ToString().Trim();
            else
                Cmbtelephely.Text = Program.PostásTelephely;

            Cmbtelephely.Enabled = Program.Postás_Vezér;
        }


        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;

                // ide kell az összes gombot tenni amit szabályozni akarunk false
                LÉT_hozzáad.Visible = false;
                TÖR_töröl.Visible = false;
                MÓD_rögzít.Visible = false;
                MÓD_SAP_adatok.Visible = false;

                Állvesz.Visible = false;
                Állkirak.Visible = false;

                PDF_rögzít.Visible = false;

                // csak főmérnökségi belépéssel van módosítás
                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {
                    LÉT_hozzáad.Enabled = true;
                    TÖR_töröl.Enabled = true;
                    MÓD_rögzít.Enabled = true;
                    MÓD_SAP_adatok.Enabled = true;
                }
                else
                {
                    LÉT_hozzáad.Enabled = false;
                    TÖR_töröl.Enabled = false;
                    MÓD_rögzít.Enabled = false;
                    MÓD_SAP_adatok.Enabled = false;
                }

                melyikelem = 90;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))

                {
                    LÉT_hozzáad.Visible = true;
                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))

                {
                    TÖR_töröl.Visible = true;
                }
                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 3))

                {
                    MÓD_rögzít.Visible = true;
                    MÓD_SAP_adatok.Visible = true;
                }

                melyikelem = 91;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))

                {
                    Állvesz.Visible = true;
                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))

                {
                    Állkirak.Visible = true;
                }
                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 3))

                {
                    PDF_rögzít.Visible = true;
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
            string hely;
            switch (Fülek.SelectedIndex)
            {
                case 0:
                    {
                        // betöltjük a kocsikat
                        Telephelyeklistázasa();
                        Listázközös();
                        ComboListáz();
                        break;
                    }

                case 1:
                    {
                        // jármű létrehozás, törlés, módosítás
                        hely = $@"{Application.StartupPath}\Főmérnökség\adatok\villamos.mdb";

                        if (File.Exists(hely))
                            LÉT_listáz();
                        else
                            Adatbázis_Létrehozás.KocsikTípusa(hely);

                        // megnézzük, hogy létezik-e naplófájl
                        hely = $@"{Application.StartupPath}\Főmérnökség\napló\napló{DateTime.Today.Year}.mdb";
                        if (!File.Exists(hely)) Adatbázis_Létrehozás.Kocsitípusanapló(hely);
                        // feltöltjük a típusokat
                        Típusfeltöltés();
                        Mód_üzembehelyezésdátuma.Value = new DateTime(1900, 1, 1);
                        Listáz_psz_();
                        break;
                    }
                case 2:
                    {
                        // pdf lapfül
                        // leellenőrizzük, hogy létezik-e a könyvtár
                        hely = Application.StartupPath + @"\Főmérnökség\Jegyzőkönyvek";
                        if (!File.Exists(hely)) Directory.CreateDirectory(hely);
                        PDF_Listáz_psz_();
                        Kiegészítők_feltöltése();
                        break;
                    }

                case 3:
                    {
                        // napló listázása               
                        Mozg_Dátum.Value = DateTime.Today;
                        hely = $@"{Application.StartupPath}\Főmérnökség\napló\napló{DateTime.Today.Year}.mdb";
                        if (!File.Exists(hely)) Adatbázis_Létrehozás.Kocsitípusanapló(hely);
                        Napló_Feltöltés();
                        break;
                    }


                case 4:
                    {
                        // melyik telephelyen van
                        Típusfeltöltés_melyik();
                        break;
                    }
            }
        }


        private void Btn_súgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Jármű.html";
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


        public void Fülek_DrawItem(object sender, DrawItemEventArgs e)
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
            if (Convert.ToBoolean(e.State & DrawItemState.Selected))
            {
                Font BoldFont = new Font(Fülek.Font.Name, Fülek.Font.Size, FontStyle.Bold);
                // háttér szín beállítása
                e.Graphics.FillRectangle(new SolidBrush(Color.DarkGray), e.Bounds);
                Rectangle paddedBounds = e.Bounds;
                paddedBounds.Inflate(0, 0);
                e.Graphics.DrawString(SelectedTab.Text, BoldFont, BlackTextBrush, paddedBounds, sf);
            }
            else
                e.Graphics.DrawString(SelectedTab.Text, e.Font, BlackTextBrush, HeaderRect, sf);
            // Munka kész – dobja ki a keféket
            BlackTextBrush.Dispose();
        }


        private void Cmbtelephely_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboListáz();
        }
        #endregion


        #region Jármű létrehozás
        private void Típusfeltöltés()
        {
            List<string> Valóstípus2 = (from a in Adatok_Állomány
                                        orderby a.Valóstípus2
                                        select a.Valóstípus2).ToList().Distinct().ToList();
            List<string> Valóstípus = (from a in Adatok_Állomány
                                       orderby a.Valóstípus
                                       select a.Valóstípus).ToList().Distinct().ToList();

            LÉT_járműtípus.Items.Clear();
            MÓD_járműtípus.Items.Clear();
            LÉT_főmérnökségitípus.Items.Clear();
            MÓD_főmérnökségitípus.Items.Clear();

            foreach (string Elem in Valóstípus2)
            {
                LÉT_járműtípus.Items.Add(Elem);
                MÓD_járműtípus.Items.Add(Elem);
            }

            foreach (string Elem in Valóstípus)
            {
                LÉT_főmérnökségitípus.Items.Add(Elem);
                MÓD_főmérnökségitípus.Items.Add(Elem);
            }

            LÉT_járműtípus.Refresh();
            MÓD_járműtípus.Refresh();
            LÉT_főmérnökségitípus.Refresh();
            MÓD_főmérnökségitípus.Refresh();
        }


        private void LÉT_hozzáad_Click(object sender, EventArgs e)
        {
            try
            {
                if (LÉT_Pályaszám.Text.Trim() == "") throw new HibásBevittAdat("A pályaszámot meg kell adni!");
                if (LÉT_főmérnökségitípus.Text.Trim() == "") throw new HibásBevittAdat("A Főmérnökségi típus meg kell adni!");
                if (LÉT_járműtípus.Text.Trim() == "") throw new HibásBevittAdat("A jármű típus meg kell adni!");

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\villamos.mdb";
                string jelszó = "pozsgaii";

                Adat_Jármű Egy = (from a in Adatok_Állomány
                                  where a.Azonosító == LÉT_Pályaszám.Text.Trim()
                                  select a).FirstOrDefault();
                if (Egy == null)
                {
                    // ha nincs, akkor rögzítjük
                    Adat_Jármű Adat = new Adat_Jármű(
                        LÉT_Pályaszám.Text.Trim(), 0, 0, "Nincs", "Közös", false, 0, false, 0, new DateTime(1900, 1, 1),
                        LÉT_főmérnökségitípus.Text.Trim(),
                        LÉT_járműtípus.Text.Trim(),
                        new DateTime(1900, 1, 1)
                        );
                    KézJármű.Rögzítés(hely, jelszó, Adat);
                }
                else
                {
                    // ha van akkor
                    throw new HibásBevittAdat("Van már ilyen pályaszámű jármű !");
                }

                // naplózás
                Adat_Jármű_Napló AdatNapló = new Adat_Jármű_Napló(
                                                 LÉT_Pályaszám.Text.Trim(),
                                                 "Új", "Új", "Közös", false,
                                                 Program.PostásNév.Trim(), DateTime.Now, "Közös", 0
                                                 );
                KadatNapló.Rögzítés(DateTime.Today.Year, AdatNapló);

                Főmérnökségi_Állomány_Lista();

                LÉT_Pályaszám.Text = "";
                LÉT_főmérnökségitípus.Text = "";
                LÉT_járműtípus.Text = "";
                LÉT_listáz();
                Listáz_psz_();
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
        #endregion


        #region Jármű Törlés
        private void LÉT_listáz()
        {
            List<Adat_Jármű> Adatok;
            if (TÖR_töröltek.Checked)
                Adatok = (from a in Adatok_Állomány
                          where a.Üzem == "Közös"
                          && a.Törölt
                          orderby a.Azonosító
                          select a).ToList();
            else
                Adatok = (from a in Adatok_Állomány
                          where a.Üzem == "Közös"
                          && !a.Törölt
                          orderby a.Azonosító
                          select a).ToList();


            TÖR_List1.Items.Clear();
            foreach (Adat_Jármű Elem in Adatok)
                TÖR_List1.Items.Add(Elem.Azonosító);
            TÖR_List1.Refresh();
        }


        private void TÖR_töröltek_CheckedChanged(object sender, EventArgs e)
        {
            LÉT_listáz();
        }


        private void TÖR_List1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TÖR_List1.SelectedItem == null) return;
            if (TÖR_List1.Items.Count < 0) return;
            TÖR_Text1.Text = TÖR_List1.SelectedItem.ToString().Trim();
        }


        private void TÖR_töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (TÖR_Text1.Text.Trim() == "") return;

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\villamos.mdb";
                string jelszó = "pozsgaii";
                string szöveg;
                Adat_Jármű Elem = (from a in Adatok_Állomány
                                   where a.Azonosító == TÖR_Text1.Text.Trim()
                                   select a).FirstOrDefault();


                if (Elem != null)
                {
                    if (Elem.Törölt)
                        szöveg = $"UPDATE Állománytábla SET törölt=false WHERE [azonosító]='{TÖR_Text1.Text.Trim()}'";
                    else
                        szöveg = $"UPDATE Állománytábla SET törölt=true WHERE [azonosító]='{TÖR_Text1.Text.Trim()}'";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                }
                else
                    return;

                // naplózás
                hely = $@"{Application.StartupPath}\Főmérnökség\napló\napló{DateTime.Today.Year}.mdb";
                if (!File.Exists(hely)) Adatbázis_Létrehozás.Kocsitípusanapló(hely);


                szöveg = "INSERT INTO Állománytáblanapló (azonosító, típus, honnan, hova, törölt, Módosító, mikor, céltelep, üzenet) VALUES (";
                szöveg += $"'{TÖR_Text1.Text.Trim()}', 'Új', 'Közös', 'Törölt', ";
                if (Elem.Törölt)
                    szöveg += "false,";
                else
                    szöveg += "true,";
                szöveg += $" '{Program.PostásNév}', '{DateTime.Now}', 'Közös', 0)";
                MyA.ABMódosítás(hely, jelszó, szöveg);

                Főmérnökségi_Állomány_Lista();
                LÉT_listáz();
                Listáz_psz_();
                TÖR_Text1.Text = "";
            }
            catch (HibásBevittAdat ex)
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


        #region Típus módosítás
        private void Mód_pályaszám_SelectedIndexChanged(object sender, EventArgs e)
        {
            Alapadatokkiírása();
        }


        private void MÓD_pályaszámkereső_Click(object sender, EventArgs e)
        {
            Típusfeltöltés();
            Alapadatokkiírása();
        }


        private void Alapadat_ürítése()
        {
            Mód_telephely.Text = "";
            MÓD_típustext.Text = "";
            Mód_üzembehelyezésdátuma.Value = new DateTime(1900, 1, 1);
            MÓD_főmérnökségitípus.Text = "";
            MÓD_járműtípus.Text = "";
        }


        private void Alapadatokkiírása()
        {
            try
            {
                if (Mód_pályaszám.Text.Trim() == "") return;
                Alapadat_ürítése();

                Adat_Jármű adat = (from a in Adatok_Állomány
                                   where a.Azonosító == Mód_pályaszám.Text.Trim()
                                   && a.Törölt == false
                                   select a).FirstOrDefault();
                if (adat != null)
                {
                    Mód_telephely.Text = adat.Üzem.Trim();
                    MÓD_típustext.Text = adat.Típus.Trim();
                    Mód_üzembehelyezésdátuma.Value = adat.Üzembehelyezés < new DateTime(1900, 1, 1) ? new DateTime(1900, 1, 1) : adat.Üzembehelyezés;
                    MÓD_főmérnökségitípus.Text = adat.Valóstípus.Trim();
                    MÓD_járműtípus.Text = adat.Valóstípus2.Trim();
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


        private void Listáz_psz_()
        {
            Mód_pályaszám.Items.Clear();
            List<Adat_Jármű> Elemek = (from a in Adatok_Állomány
                                       where !a.Törölt
                                       select a).ToList();

            foreach (Adat_Jármű Elem in Elemek)
                Mód_pályaszám.Items.Add(Elem.Azonosító);
        }


        private void MÓD_rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (MÓD_járműtípus.Text.Trim() == "") throw new HibásBevittAdat("Nincs megadva a járműtípus");
                if (MÓD_főmérnökségitípus.Text.Trim() == "") throw new HibásBevittAdat("Nincs megadva a főmérnökségi típus.");
                if (Mód_pályaszám.Text.Trim() == "") throw new HibásBevittAdat("A pályaszám mezőben nincs érték.");

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\villamos.mdb";
                string jelszó = "pozsgaii";
                string szöveg;

                Adat_Jármű Elem = (from a in Adatok_Állomány
                                   where a.Azonosító == Mód_pályaszám.Text.Trim()
                                   select a).FirstOrDefault();

                if (Elem != null)
                {
                    szöveg = "UPDATE állománytábla SET ";
                    szöveg += "valóstípus='" + MÓD_főmérnökségitípus.Text.Trim() + "', ";
                    szöveg += "valóstípus2='" + MÓD_járműtípus.Text.Trim() + "', ";
                    szöveg += "üzembehelyezés='" + Mód_üzembehelyezésdátuma.Value.ToString("yyyy.MM.dd") + "' ";
                    szöveg += "where [azonosító] ='" + Mód_pályaszám.Text.Trim() + "'";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                }
                else
                {
                    MessageBox.Show("Nincs ilyen pályaszámú villamos!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                MessageBox.Show("Az adatok rögzítése megtörtént !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (!(Mód_telephely.Text.Trim() == "" || Mód_telephely.Text.Trim() == "Közös"))
                {
                    // telephelyi adatokban is módosít
                    hely = $@"{Application.StartupPath}\{Mód_telephely.Text.Trim()}\adatok\villamos\villamos.mdb";
                    szöveg = "SELECT * FROM állománytábla ";

                    Adatok_Állomány.Clear();
                    Adatok_Állomány = KézJármű.Lista_Adatok(hely, jelszó, szöveg);

                    Elem = (from a in Adatok_Állomány
                            where a.Azonosító == Mód_pályaszám.Text.Trim()
                            select a).FirstOrDefault();

                    if (Elem != null)
                    {
                        szöveg = "UPDATE állománytábla SET ";
                        szöveg += "valóstípus='" + MÓD_főmérnökségitípus.Text.Trim() + "', ";
                        szöveg += "valóstípus2='" + MÓD_járműtípus.Text.Trim() + "' ";
                        szöveg += "where [azonosító] ='" + Mód_pályaszám.Text.Trim() + "'";
                        MyA.ABMódosítás(hely, jelszó, szöveg);
                    }
                    MessageBox.Show("Az adatok a telephelyi adatokban is módosultak!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Főmérnökségi_Állomány_Lista();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void MÓD_SAP_adatok_Click(object sender, EventArgs e)
        {
            try
            {

                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Üzembehelyezési adatok feltöltése",
                    FileName = "",
                    Filter = "Excel |*.xlsx"
                };
                string fájlexc;
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;

                // megnyitjuk a beolvasandó táblát
                MyE.ExcelMegnyitás(fájlexc);

                // ***********************************
                // ***** Ellenőrzés       ************
                // ***********************************
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\beolvasás.mdb";
                string jelszó = "sajátmagam";
                string szöveg = "SELECT * FROM tábla where [csoport]='Üzembehely' and [törölt]='0' AND kell=1 ORDER BY oszlop";

                List<Adat_Alap_Beolvasás> Adatok = KAAdat.Lista_Adatok(hely, jelszó, szöveg);

                string előírtfejléc = "";

                foreach (Adat_Alap_Beolvasás item in Adatok)
                {
                    előírtfejléc += item.Fejléc.Trim();
                }


                string kapottfejléc = "";
                // beolvassuk a fejlécet ha eltér a megadotttól, akkor kiírja és bezárja
                for (int i = 1; i <= 2; i++) // a max jelöli a helyes oszlopokat
                {
                    kapottfejléc += MyE.Beolvas(MyE.Oszlopnév(i) + "1").Trim();
                }

                if (előírtfejléc.Trim() != kapottfejléc.Trim())
                {
                    MyE.ExcelBezárás();
                    throw new HibásBevittAdat("Nem megfelelő a betölteni kívánt adatok formátuma");
                }

                // ***********************************
                // ***** Ellenőrzés    vége **********
                // ***********************************
                // megnézzük, hogy hány sorból áll a tábla
                int utolsó = MyE.Utolsósor("Sheet1");

                hely = $@"{Application.StartupPath}\Főmérnökség\adatok\villamos.mdb";
                jelszó = "pozsgaii";
                szöveg = $"SELECT * FROM állománytábla ";
                Adatok_Állomány.Clear();
                Adatok_Állomány = KézJármű.Lista_Adatok(hely, jelszó, szöveg);

                Holtart.Be(utolsó + 2);
                // Első adattól végig pörgetjüka beolvasást



                List<string> SzövegGy = new List<string>();
                for (int i = 2; i < utolsó; i++)
                {
                    string pályaszám = MyE.Beolvas("a" + i.ToString()).Substring(1, 4);
                    DateTime Dátum = DateTime.Parse(MyE.Beolvas("B" + i.ToString()));

                    Adat_Jármű AdatJármű = (from a in Adatok_Állomány
                                            where a.Azonosító == pályaszám.Trim()
                                            select a).FirstOrDefault();

                    if (AdatJármű != null)
                    {
                        szöveg = "UPDATE állománytábla SET ";
                        szöveg += $" üzembehelyezés='{Dátum:yyyy.MM.dd}' ";
                        szöveg += $"where [azonosító] ='{pályaszám.Trim()}'";
                        SzövegGy.Add(szöveg);
                    }
                    Holtart.Lép();
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);

                Holtart.Ki();
                MyE.ExcelBezárás();

                // kitöröljük a betöltött fájlt
                File.Delete(fájlexc);
                MessageBox.Show("Az adat konvertálás befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Főmérnökségi_Állomány_Lista();
            }
            catch (HibásBevittAdat ex)
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


        #region napló listázás

        private void Mozg_Dátum_ValueChanged(object sender, EventArgs e)
        {
            if (ElőzőDátum.Year != Mozg_Dátum.Value.Year)
            {
                Napló_Feltöltés();
                ElőzőDátum = Mozg_Dátum.Value;
            }
        }


        private void Táblalistázás(string melyik)
        {
            try
            {
                if (Adatok_Napló == null) throw new HibásBevittAdat("Nincs naplófájl erre az időre vonatkozóan.");

                DateTime KövHónapelsőnapja = MyF.Hónap_utolsónapja(Mozg_Dátum.Value).AddDays(1);
                DateTime hónapelsőnapja = MyF.Hónap_elsőnapja(Mozg_Dátum.Value);
                DateTime Következőnap = Mozg_Dátum.Value.AddDays(1);
                List<Adat_Jármű_Napló> Adatok;
                if (melyik == "napi")
                {
                    Adatok = (from a in Adatok_Napló
                              where a.Mikor >= Mozg_Dátum.Value
                              && a.Mikor < Következőnap
                              select a).ToList();
                }
                else
                {
                    Adatok = (from a in Adatok_Napló
                              where a.Mikor >= hónapelsőnapja
                              && a.Mikor < KövHónapelsőnapja
                              select a).ToList();
                }

                DataTable AdatTábla = new DataTable();

                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Dátum");
                AdatTábla.Columns.Add("Azonosító");
                AdatTábla.Columns.Add("Típus");
                AdatTábla.Columns.Add("Honnan");
                AdatTábla.Columns.Add("Hova");
                AdatTábla.Columns.Add("Törölt");
                AdatTábla.Columns.Add("Módosító");
                AdatTábla.Columns.Add("Cél telep");
                AdatTábla.Columns.Add("Üzenet volt");


                AdatTábla.Clear();
                foreach (Adat_Jármű_Napló item in Adatok)
                {
                    DataRow Soradat = AdatTábla.NewRow();

                    Soradat["Dátum"] = item.Mikor.ToString();
                    Soradat["Azonosító"] = item.Azonosító.Trim();
                    Soradat["Típus"] = item.Típus.Trim();
                    Soradat["Honnan"] = item.Hova.Trim();
                    Soradat["Hova"] = item.Honnan.Trim();
                    Soradat["Törölt"] = item.Törölt == true ? "Törölve" : "Aktív";
                    Soradat["Módosító"] = item.Módosító.Trim();
                    Soradat["Cél telep"] = item.Céltelep.Trim();
                    Soradat["Üzenet volt"] = item.Üzenet == 1 ? "Volt" : "Nem volt";

                    AdatTábla.Rows.Add(Soradat);

                }
                Tábla.DataSource = AdatTábla;

                Tábla.Columns["Dátum"].Width = 170;
                Tábla.Columns["Azonosító"].Width = 100;
                Tábla.Columns["Típus"].Width = 100;
                Tábla.Columns["Honnan"].Width = 150;
                Tábla.Columns["Hova"].Width = 150;
                Tábla.Columns["Törölt"].Width = 100;
                Tábla.Columns["Módosító"].Width = 100;
                Tábla.Columns["Cél telep"].Width = 100;
                Tábla.Columns["Üzenet volt"].Width = 100;

                Tábla.Refresh();
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


        private void Mozg_lista_Click(object sender, EventArgs e)
        {
            Táblalistázás("napi");

        }


        private void Mozg_havilista_Click(object sender, EventArgs e)
        {
            Táblalistázás("havi");

        }


        private void Mozg_Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.Rows.Count <= 0)
                    return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Telephelyek_közötti_Naplózások_" + Program.PostásNév + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Tábla, false);
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
        #endregion


        #region Átadás-átvétel fül
        private void Telephelyeklistázasa()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő.mdb";
                string jelszó = "Mocó";
                string szöveg = "SELECT * FROM telephelytábla  order by sorszám";
                Lektelephely.Items.Clear();
                Lektelephely.BeginUpdate();
                Lektelephely.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "Telephelynév"));
                Lektelephely.EndUpdate();
                Lektelephely.Refresh();

                Lektelephely.Items.Add("Értékesítés");
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Listázközös()
        {
            try
            {
                List<Adat_Jármű> Elemek = (from a in Adatok_Állomány
                                           where a.Üzem == "Közös" && !a.Törölt
                                           orderby a.Azonosító
                                           select a).ToList();

                Közös_járművek.Items.Clear();

                foreach (Adat_Jármű Elem in Elemek)
                    Közös_járművek.Items.Add(Elem.Azonosító);

                Közös_járművek.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ComboListáz()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\Jármű.mdb";
                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM típustábla  order by id";

                Telephelyi_típus.Items.Clear();
                Telephelyi_típus.BeginUpdate();
                Telephelyi_típus.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "típus"));
                Telephelyi_típus.EndUpdate();
                Telephelyi_típus.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Command6_Click(object sender, EventArgs e)
        {
            Listázközös();
        }


        private void Combo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Listáztípus();
        }


        private void Listáztípus()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";
                string jelszó = "pozsgaii";
                string szöveg = $"SELECT * FROM Állománytábla where [típus]='{Telephelyi_típus.Text.Trim()}'  and [törölt]=false  ORDER BY azonosító";

                Saját_járművek.Items.Clear();
                Saját_járművek.BeginUpdate();
                Saját_járművek.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "azonosító"));
                Saját_járművek.EndUpdate();
                Saját_járművek.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Állvesz_Click(object sender, EventArgs e)
        {
            try
            {
                if (Telephelyi_típus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva, hogy melyik típusba rakjuk be a járművet.");
                if (Közös_járművek.SelectedItem == null || Közös_járművek.SelectedItem.ToString().Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva jármű.");

                bool volt = false;
                Adat_Jármű Elem = (from a in Adatok_Állomány
                                   where a.Azonosító == Közös_járművek.SelectedItem.ToStrTrim() && !a.Törölt
                                   orderby a.Azonosító
                                   select a).FirstOrDefault();
                if (Elem != null)
                    if (Elem.Üzem.ToUpper() == "KÖZÖS") volt = true;

                // ha még közösben van akkor átrakja
                if (volt)
                {
                    Subállveszvillamos();
                    Főmérnökségi_Állomány_Lista();
                    MessageBox.Show("Az áthelyezés megtörtént.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    throw new HibásBevittAdat($"A {Közös_járművek.SelectedItem.ToString().Trim()} jármű már a(z) {Elem.Üzem}-ben van.");
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
            finally
            {
                Listáztípus();
                Listázközös();
            }
        }


        private void Subállveszvillamos()
        {
            try
            {
                // megnézzük, hogy létezik-e az üzemben már a fájl, ha nem akkor létrehozzuk
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";
                if (!File.Exists(hely)) Adatbázis_Létrehozás.KocsikTípusa(hely);

                string helyközös = $@"{Application.StartupPath}\Főmérnökség\adatok\villamos.mdb";

                // áthelyezzük a fogadó telephelyre
                ÁlllományLétrehozás(hely, Közös_járművek.SelectedItem.ToStrTrim());
                ÁllományMódosítás(helyközös, Közös_járművek.SelectedItem.ToStrTrim(), Cmbtelephely.Text.Trim());

                //Naplózzuk
                ÁllományNaplózás(Közös_járművek.SelectedItem.ToStrTrim(), "Közös", Cmbtelephely.Text.Trim());

                // Módosítjuk a típus darabszámát
                TípusDB(true);

                // hibákat átmásoljuk az állományba
                string hova = Cmbtelephely.Text.Trim();
                string honnan = "Főmérnökség";
                HibákMásolása(honnan, hova, Közös_járművek.SelectedItem.ToStrTrim(), Telephelyi_típus.Text.Trim());

                //E2 másolás
                hova = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos2.mdb";
                honnan = Application.StartupPath + @"\Főmérnökség\adatok\villamos2.mdb";
                E2Másolása(honnan, hova, Közös_járművek.SelectedItem.ToStrTrim());
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Állkirak_Click(object sender, EventArgs e)
        {
            try
            {
                if (Telephelyi_típus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva, hogy melyik típusból rakjuk ki a járművet.");
                if (Saját_járművek.SelectedItem == null || Saját_járművek.SelectedItem.ToString().Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva, a kirakandó jármű.");
                if (Lektelephely.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva a cél telephely.");

                if (Lektelephely.Text.Trim() == Program.PostásTelephely.Trim())
                {
                    if (MessageBox.Show("Biztos, hogy a saját telephely a cél telephely?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    {
                        return;
                    }
                }

                Subállkirakvillamos();
                Főmérnökségi_Állomány_Lista();
                Listáztípus();
                Listázközös();
                MessageBox.Show("Az áthelyezés megtörtént.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Subállkirakvillamos()
        {
            try
            {
                // megnézzük, hogy létezik-e az üzemben már a fájl, ha nem akkor létrehozzuk
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";
                if (!File.Exists(hely)) Adatbázis_Létrehozás.KocsikTípusa(hely);

                // berakjuk közös állományba
                string helyközös = $@"{Application.StartupPath}\Főmérnökség\adatok\villamos.mdb";
                ÁllományMódosítás(helyközös, Saját_járművek.SelectedItem.ToStrTrim(), "Közös");

                // kitöröljük a telephelyről        
                ÁlllományTörlés(hely, Saját_járművek.SelectedItem.ToStrTrim());

                //Naplózzuk
                ÁllományNaplózás(Saját_járművek.SelectedItem.ToStrTrim(), Cmbtelephely.Text.Trim(), "Közös");

                // Módosítjuk a típus darabszámát
                TípusDB(false);

                //Hibák másolás
                string honnan = Cmbtelephely.Text.Trim();
                string hova = "Főmérnökség";
                HibákMásolása(honnan, hova, Saját_járművek.SelectedItem.ToStrTrim(), Telephelyi_típus.Text.Trim());

                //E2 másolás
                hova = Application.StartupPath + @"\Főmérnökség\adatok\villamos2.mdb";
                honnan = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos2.mdb";
                E2Másolása(honnan, hova, Saját_járművek.SelectedItem.ToStrTrim());
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ÁllományMódosítás(string hely, string azonosító, string Hova)
        {
            try
            {
                string jelszó = "pozsgaii";
                string szöveg = "UPDATE Állománytábla SET ";
                szöveg += $" üzem='{Hova}', típus='{Telephelyi_típus.Text.Trim()}' WHERE azonosító='{azonosító}'";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ÁlllományTörlés(string hely, string azonosító)
        {
            try
            {
                string jelszó = "pozsgaii";
                string szöveg = $"DELETE FROM Állománytábla WHERE [azonosító]='{azonosító}'";
                MyA.ABtörlés(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ÁlllományLétrehozás(string hely, string azonosító)
        {
            try
            {
                string jelszó = "pozsgaii";
                Adat_Jármű adat = (from a in Adatok_Állomány
                                   where a.Azonosító == azonosító
                                   select a).FirstOrDefault();

                if (adat.Üzem == Cmbtelephely.Text.Trim())
                {
                    adat.Üzem = Cmbtelephely.Text.Trim();
                    adat.Típus = Telephelyi_típus.Text.Trim();
                    // ha van a telephelyen
                    KézJármű.Módosítás(hely, jelszó, adat);
                }
                else
                {
                    adat.Üzem = Cmbtelephely.Text.Trim();
                    adat.Típus = Telephelyi_típus.Text.Trim();
                    // ha nincs a telephelyen
                    KézJármű.Áthelyezés_új(hely, jelszó, adat);
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

        private void ÁllományNaplózás(string azonosító, string Honnan, string Hova)
        {
            try
            {
                Adat_Jármű adat = (from a in Adatok_Állomány
                                   where a.Azonosító == azonosító
                                   select a).FirstOrDefault();

                int üzenet = 0;
                if (Honnan == "Közös") üzenet = 1;         // ha közösből vesszük be akkor nem kell üzenetet írni
                Adat_Jármű_Napló adatnapló = new Adat_Jármű_Napló(
                                        adat.Azonosító.Trim(),
                                        adat.Típus.Trim(),
                                        Honnan,
                                        Hova,
                                        adat.Törölt,
                                        Program.PostásNév.Trim(),
                                        DateTime.Now,
                                        Lektelephely.Text.Trim(),
                                        üzenet
                                        );
                KadatNapló.Rögzítés(DateTime.Now.Year, adatnapló);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="be">Igen akkor hozzáad, false esetén levon</param>
        private void TípusDB(bool be)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\Jármű.mdb";
                string jelszó = "pozsgaii";

                Kezelő_Jármű_Állomány_Típus kéz = new Kezelő_Jármű_Állomány_Típus();
                List<Adat_Jármű_Állomány_Típus> Adatok = kéz.Lista_Adatok(Cmbtelephely.Text.Trim());

                Adat_Jármű_Állomány_Típus EgyTípus = (from a in Adatok
                                                      where a.Típus == Telephelyi_típus.Text.Trim()
                                                      select a).FirstOrDefault();

                if (EgyTípus != null)
                {
                    long állomány = EgyTípus.Állomány;
                    if (be)
                        állomány++;
                    else
                        állomány--;
                    if (állomány < 0) állomány = 0;

                    string szöveg = "UPDATE típustábla SET ";
                    szöveg += $" állomány={állomány} ";
                    szöveg += $" WHERE [típus] ='{Telephelyi_típus.Text.Trim()}'";
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


        private void HibákMásolása(string honnan, string hova, string azonosító, string típus)
        {
            try
            {
                // hibákat átmásoljuk az állományba
                string szöveg = $"SELECT * FROM hibatábla where [azonosító]='{azonosító}'";
                string jelszó = "pozsgaii";
                List<Adat_Jármű_hiba> JHadatok = Kéz_JHadat.Lista_adatok(honnan, jelszó, szöveg);

                foreach (Adat_Jármű_hiba item in JHadatok)
                {
                    Adat_Jármű_hiba Küld = new Adat_Jármű_hiba(
                                           item.Létrehozta,
                                           item.Korlát,
                                           item.Hibaleírása,
                                           item.Idő,
                                           item.Javítva,
                                           típus,
                                           item.Azonosító,
                                           item.Hibáksorszáma
                                           );
                    Kéz_JHadat.Rögzítés(hova, Küld);
                }

                //kitöröljük pályaszámhoz tartozó az összes hibát

                szöveg = $"DELETE FROM hibatábla WHERE [azonosító]='{azonosító}'";
                MyA.ABtörlés(honnan, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void E2Másolása(string honnan, string hova, string azonosító)
        {
            try
            {
                // hibákat átmásoljuk az állományba
                string jelszó = "pozsgaii";
                string szöveg = $"SELECT * FROM állománytábla where [azonosító]='{azonosító}'";

                Adat_Jármű_2 adat = Kadat2.Egy_Adat(honnan, jelszó, szöveg);
                if (adat != null)
                {
                    Kadat2.Módosít(hova, jelszó, adat);
                    // kitöröljük
                    szöveg = $"DELETE FROM állománytábla WHERE [azonosító]='{azonosító}'";
                    MyA.ABtörlés(honnan, jelszó, szöveg);
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


        #region Keresés
        Ablak_Kereső Új_Ablak_Kereső;

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
            try
            {
                // megkeressük a szöveget a táblázatban
                if (Új_Ablak_Kereső.Keresendő == null) return;
                if (Új_Ablak_Kereső.Keresendő.Trim() == "") return;


                for (int i = 0; i < Tábla_telephely.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < Tábla_telephely.Columns.Count - 1; j++)
                    {
                        if (Tábla_telephely.Rows[i].Cells[j].Value != null)
                        {
                            if (Tábla_telephely.Rows[i].Cells[j].Value.ToString().Trim() == Új_Ablak_Kereső.Keresendő.Trim())
                            {
                                Tábla_telephely.Rows[i].Cells[j].Style.BackColor = Color.Orange;
                                Tábla_telephely.FirstDisplayedScrollingRowIndex = i;
                                return;
                            }
                        }
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
        #endregion


        #region Melyik telepen van
        private void Keresés_Click(object sender, EventArgs e)
        {
            Keresés_metódus();
        }


        private void Típusfeltöltés_melyik()
        {
            try
            {
                List<string> Valóstípus2 = (from a in Adatok_Állomány
                                            orderby a.Valóstípus2
                                            select a.Valóstípus2).ToList().Distinct().ToList();

                Típuslista_melyik.Items.Clear();
                foreach (string Elem in Valóstípus2)
                    Típuslista_melyik.Items.Add(Elem);
                Típuslista_melyik.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void CsoportkijelölMind_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= Típuslista_melyik.Items.Count - 1; i++)
                Típuslista_melyik.SetItemChecked(i, true);
        }


        private void CsoportVissza_Click(object sender, EventArgs e)
        {

            for (int i = 0; i <= Típuslista_melyik.Items.Count - 1; i++)
                Típuslista_melyik.SetItemChecked(i, false);
        }


        private void Telephely_Frissít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Típuslista_melyik.CheckedItems.Count <= 0) throw new HibásBevittAdat("Nincs kijelölve egy típus sem.");

                List<Adat_Jármű> Adatok = new List<Adat_Jármű>();

                for (int i = 0; i < Típuslista_melyik.CheckedItems.Count; i++)
                {
                    List<Adat_Jármű> ideig = (from a in Adatok_Állomány
                                              where a.Valóstípus2 == Típuslista_melyik.CheckedItems[i].ToStrTrim()
                                              orderby a.Üzem, a.Azonosító
                                              select a).ToList();
                    Adatok.AddRange(ideig);
                }

                string előző = "";
                string előzőüzem = "";
                int darab = 0;
                int oszlop;
                int sor;

                Tábla_telephely.Rows.Clear();
                Tábla_telephely.Columns.Clear();
                Tábla_telephely.Refresh();
                Tábla_telephely.Visible = false;
                Tábla_telephely.ColumnCount = 13;

                // fejléc elkészítése
                Tábla_telephely.Columns[0].HeaderText = "Típus";
                Tábla_telephely.Columns[0].Width = 100;
                Tábla_telephely.Columns[1].HeaderText = "Telephely";
                Tábla_telephely.Columns[1].Width = 100;
                Tábla_telephely.Columns[2].HeaderText = "";
                Tábla_telephely.Columns[2].Width = 60;
                Tábla_telephely.Columns[3].HeaderText = "";
                Tábla_telephely.Columns[3].Width = 60;
                Tábla_telephely.Columns[4].HeaderText = "";
                Tábla_telephely.Columns[4].Width = 60;
                Tábla_telephely.Columns[5].HeaderText = "";
                Tábla_telephely.Columns[5].Width = 60;
                Tábla_telephely.Columns[6].HeaderText = "";
                Tábla_telephely.Columns[6].Width = 60;
                Tábla_telephely.Columns[7].HeaderText = "";
                Tábla_telephely.Columns[7].Width = 60;
                Tábla_telephely.Columns[8].HeaderText = "";
                Tábla_telephely.Columns[8].Width = 60;
                Tábla_telephely.Columns[9].HeaderText = "";
                Tábla_telephely.Columns[9].Width = 60;
                Tábla_telephely.Columns[10].HeaderText = "";
                Tábla_telephely.Columns[10].Width = 60;
                Tábla_telephely.Columns[11].HeaderText = "";
                Tábla_telephely.Columns[11].Width = 60;
                Tábla_telephely.Columns[12].HeaderText = "Darabszám";
                Tábla_telephely.Columns[12].Width = 100;

                oszlop = 2;
                sor = 0;

                foreach (Adat_Jármű adat in Adatok)
                {
                    if (előző.Trim() == "" && !adat.Törölt)
                    {
                        előző = adat.Valóstípus2.Trim();
                        előzőüzem = adat.Üzem.Trim();
                        Tábla_telephely.RowCount++;
                        sor = Tábla_telephely.RowCount - 1;
                    }
                    // ha új típust ír ki
                    if (előző.Trim() != adat.Valóstípus2.Trim() && !adat.Törölt)
                    {
                        Tábla_telephely.Rows[sor].Cells[12].Value = darab;

                        darab = 0;
                        előző = adat.Valóstípus2.Trim();
                        előzőüzem = adat.Üzem.Trim();
                        Tábla_telephely.RowCount += 2;
                        sor = Tábla_telephely.RowCount - 1;
                        oszlop = 2;
                    }
                    // ha másik üzemben van akkor új sor
                    if (előzőüzem.Trim() != adat.Üzem.Trim() && !adat.Törölt)
                    {
                        Tábla_telephely.Rows[sor].Cells[12].Value = darab;
                        darab = 0;
                        Tábla_telephely.RowCount += 2;
                        sor = Tábla_telephely.RowCount - 1;
                        oszlop = 2;
                        előzőüzem = adat.Üzem.Trim();
                    }
                    if (!adat.Törölt)
                    {
                        Tábla_telephely.Rows[sor].Cells[0].Value = adat.Valóstípus2.Trim();
                        Tábla_telephely.Rows[sor].Cells[1].Value = adat.Üzem.Trim();
                        Tábla_telephely.Rows[sor].Cells[oszlop].Value = adat.Azonosító.Trim();
                        oszlop += 1;
                        darab += 1;
                    }
                    if (oszlop == 12)
                    {
                        oszlop = 2;
                        Tábla_telephely.RowCount++;
                        sor = Tábla_telephely.RowCount - 1;
                    }

                }

                Tábla_telephely.Rows[sor].Cells[12].Value = darab;
                Tábla_telephely.Visible = true;
                Tábla_telephely.Refresh();

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Excel_Melyik_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla_telephely.Rows.Count <= 0)
                    return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Járművek_Telephelyek_" + Program.PostásNév + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Tábla_telephely, false);
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
        #endregion


        #region PDF lapfül
        private void PDF_Listáz_psz_()
        {
            PDF_pályaszám.Items.Clear();
            List<Adat_Jármű> Adatok = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim()).Where(a => a.Törölt == false).ToList();

            PDF_pályaszám.Items.Clear();
            foreach (Adat_Jármű Elem in Adatok)
                PDF_pályaszám.Items.Add(Elem.Azonosító);

            PDF_pályaszám.Refresh();
        }

        private void PDF_lista_szűrés()
        {
            try
            {
                if (PDF_pályaszám.Text.Trim() == "") return;
                Pdf_listbox.Items.Clear();

                string hely = $@"{Application.StartupPath}\Főmérnökség\Jegyzőkönyvek".KönyvSzerk();
                string mialapján = $"{PDF_pályaszám.Text.Trim()}*.pdf";

                DirectoryInfo Directories = new DirectoryInfo(hely);
                FileInfo[] fileInfo = Directories.GetFiles(mialapján, SearchOption.AllDirectories);
                foreach (FileInfo file in fileInfo)
                    Pdf_listbox.Items.Add(file.Name);
            }
            catch (HibásBevittAdat ex)
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

        private void Kiegészítők_feltöltése()
        {
            Kiegészítő.Items.Clear();
            string hely = $@"{Application.StartupPath}\Főmérnökség\Jegyzőkönyvek".KönyvSzerk();

            DirectoryInfo Directories = new DirectoryInfo(hely);
            string mialapján = "*.pdf";
            // ha nem üres

            FileInfo[] fileInfo = Directories.GetFiles(mialapján, SearchOption.AllDirectories);
            foreach (FileInfo file in fileInfo)
            {
                string[] szövegek;
                szövegek = file.Name.Split('_');
                if (Kiegészítő.Items.IndexOf(szövegek[1].ToString().Trim()) < 0)
                {
                    Kiegészítő.Items.Add(szövegek[1].ToString().Trim());
                }
            }
        }

        private void BtnPDF_Click(object sender, EventArgs e)
        {
            try
            {
                Feltöltendő.Text = "";
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    Filter = "PDF Files |*.pdf"
                };
                if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Kezelő_Pdf.PdfMegnyitás(PDF_néző, OpenFileDialog1.FileName);
                    Feltöltendő.Text = OpenFileDialog1.FileName;
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
                if (PDF_pályaszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs megadva az azonosító.");
                if (Feltöltendő.Text.Trim() == "") throw new HibásBevittAdat("Nincs feltöltendő fájl.");
                if (Kiegészítő.Text.Trim() == "") throw new HibásBevittAdat("Nincs meghatározva a kiegészítő kategória.");

                string helypdf = $@"{Application.StartupPath}\Főmérnökség\Jegyzőkönyvek".KönyvSzerk();

                // A tervezett fájlnévnek megfelelően szűrjük a könyvtár tartalmát
                Szűrés.Clear();
                DirectoryInfo Directories = new System.IO.DirectoryInfo(helypdf);

                string mialapján = $"{PDF_pályaszám.Text.Trim()}_{Kiegészítő.Text.Trim()}*.pdf";
                FileInfo[] fileInfo = Directories.GetFiles(mialapján, SearchOption.AllDirectories);

                foreach (FileInfo file in fileInfo)
                    Szűrés.Add(file.Name);

                int max = 1;
                if (fileInfo.Length >= 1)
                {
                    foreach (string Elem in Szűrés)
                    {
                        string[] darab = Elem.Split('_');
                        int i = int.Parse(darab[2].Replace(".pdf", "")) + 1;
                        if (max < i) max = i;
                    }
                }

                //létrehozzuk az új fájlnevet és átmásoljuk a tárhelyre

                string újfájlnév = $@"{helypdf}\{PDF_pályaszám.Text.Trim()}_{Kiegészítő.Text.Trim()}_{max}.pdf";

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

        private void Filelistbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Pdf_listbox.SelectedItems.Count < 1) return;
                string helypdf = $@"{Application.StartupPath}\Főmérnökség\Jegyzőkönyvek\{Pdf_listbox.SelectedItems[0]}";
                Kezelő_Pdf.PdfMegnyitás(PDF_néző, helypdf);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pdf_csere_Click(object sender, EventArgs e)
        {
            try
            {
                if (Pdf_listbox.SelectedItems.Count != 2) throw new HibásBevittAdat("Két elemet lehet csak egyszerre megcserélni.");
                if (PDF_pályaszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve a pályaszám.");

                string helypdf = $@"{Application.StartupPath}\Főmérnökség\Jegyzőkönyvek".KönyvSzerk();
                string Ideig = $@"{helypdf}\{PDF_pályaszám.Text.Trim()}_Ideig_0.pdf";
                if (File.Exists(Ideig)) File.Delete(Ideig);
                string psz1 = $@"{helypdf}\{Pdf_listbox.SelectedItems[0]}";
                string psz2 = $@"{helypdf}\{Pdf_listbox.SelectedItems[1]}";

                File.Copy(psz1, Ideig);
                File.Delete(psz1);
                File.Copy(psz2, psz1);
                File.Delete(psz2);
                File.Copy(Ideig, psz2);
                File.Delete(Ideig);
                MessageBox.Show("A PDF-k cseréje megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
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


        #region Listák
        private void Főmérnökségi_Állomány_Lista()
        {
            try
            {
                Adatok_Állomány.Clear();
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\villamos.mdb";
                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM állománytábla";

                Adatok_Állomány = KézJármű.Lista_Adatok(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Napló_Feltöltés()
        {
            try
            {

                Adatok_Napló.Clear();
                string hely = $@"{Application.StartupPath}\Főmérnökség\napló\napló{Mozg_Dátum.Value.Year}.mdb";
                if (!File.Exists(hely)) return;
                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM állománytáblanapló";
                Adatok_Napló = KadatNapló.Lista_adatok(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
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