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
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos
{
    public partial class Ablak_Jármű
    {
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly Kezelő_Jármű2 KézJármű2 = new Kezelő_Jármű2();
        readonly Kezelő_Jármű_Napló KadatNapló = new Kezelő_Jármű_Napló();
        readonly Kezelő_jármű_hiba Kéz_JHadat = new Kezelő_jármű_hiba();
        readonly Kezelő_kiegészítő_telephely KézTelephely = new Kezelő_kiegészítő_telephely();
        readonly Kezelő_Jármű_Állomány_Típus KézÁllomány = new Kezelő_Jármű_Állomány_Típus();


        List<Adat_Jármű> Adatok_Állomány = new List<Adat_Jármű>();
        List<Adat_Jármű_Napló> Adatok_Napló = new List<Adat_Jármű_Napló>();
        List<string> Szűrés = new List<string>();

        DateTime ElőzőDátum = new DateTime(1900, 1, 1);


        #region Ablak
        public Ablak_Jármű()
        {
            InitializeComponent();
            Start();
        }

        private void Ablak_Átadás_átvétel_Load(object sender, EventArgs e)
        {
        }


        private void Ablak_Jármű_Shown(object sender, EventArgs e)
        {
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
                List<Adat_Jármű> AdatokFőm = (from a in Adatok_Állomány
                                              orderby a.Azonosító
                                              where a.Üzem == Program.PostásTelephely
                                              select a).ToList();

                List<Adat_Jármű> AdatokTelep = KézJármű.Lista_Adatok(Program.PostásTelephely.Trim());

                Holtart.Be();
                List<string> Azonosítók = new List<string>();
                List<string> Üzemek = new List<string>();
                foreach (Adat_Jármű item in AdatokFőm)
                {
                    if (!(AdatokTelep.Exists(x => x.Azonosító.Trim() == item.Azonosító.Trim())))
                    {
                        Üzemek.Add("Közös");
                        Azonosítók.Add(item.Azonosító.Trim());
                    }
                    Holtart.Lép();
                }
                KézJármű.Módosítás_Telephely("Főmérnökség", Üzemek, Azonosítók);
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
                Visible = false;
                Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;

                Telephelyeklistázasa();
                Adatok_Állomány = KézJármű.Lista_Adatok("Főmérnökség");
                Kocsilistaellenőrzés();
                Fülek.SelectedIndex = 1;
                Fülekkitöltése();

                Refresh();
                Visible = true;

            }
            catch (HibásBevittAdat ex)
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
            Cmbtelephely.Items.Clear();
            foreach (string Elem in Listák.TelephelyLista_Jármű())
                Cmbtelephely.Items.Add(Elem);

            if (Program.PostásTelephely == "Főmérnökség" || Program.PostásTelephely.Contains("törzs"))
                Cmbtelephely.Text = Cmbtelephely.Items[0].ToString().Trim();
            else
                Cmbtelephely.Text = Program.PostásTelephely;

            Cmbtelephely.Enabled = Program.Postás_Vezér;
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
            switch (Fülek.SelectedIndex)
            {
                case 0:
                    {
                        // betöltjük a kocsikat
                        Telephelyeklistázasa();
                        Listázközös();
                        ComboListáz();
                        LÉT_listáz();
                        break;
                    }

                case 1:
                    {
                        Típusfeltöltés();
                        Mód_üzembehelyezésdátuma.Value = new DateTime(1900, 1, 1);
                        Listáz_psz_();
                        Listázközös();
                        break;
                    }
                case 2:
                    {
                        // pdf lapfül
                        PDF_Listáz_psz_();
                        Kiegészítők_feltöltése();
                        break;
                    }

                case 3:
                    {
                        // napló listázása               
                        Mozg_Dátum.Value = DateTime.Today;
                        Adatok_Napló = KadatNapló.Lista_adatok(Mozg_Dátum.Value.Year);
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
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\Jármű.html";
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
            PDF_Listáz_psz_();
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
                                   new DateTime(1900, 1, 1));
                    KézJármű.Rögzítés("Főmérnökség", Adat);
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

                Adatok_Állomány = KézJármű.Lista_Adatok("Főmérnökség");

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
            Adatok_Állomány = KézJármű.Lista_Adatok("Főmérnökség");
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
                KézJármű.Törlés("Főmérnökség", TÖR_Text1.Text.Trim());

                Adat_Jármű_Napló ADATNapló = new Adat_Jármű_Napló(
                              TÖR_Text1.Text.Trim(),
                              "Új",
                              "Közös",
                              "Törölt",
                              false,
                              Program.PostásNév.Trim(),
                              DateTime.Now,
                              "Közös",
                              0);
                KadatNapló.Rögzítés(DateTime.Today.Year, ADATNapló);

                Adatok_Állomány = KézJármű.Lista_Adatok("Főmérnökség");
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
                Adatok_Állomány = KézJármű.Lista_Adatok("Főmérnökség");
                Adat_Jármű adat = (from a in Adatok_Állomány
                                   where a.Azonosító == Mód_pályaszám.Text.Trim()
                                   && a.Törölt == false
                                   select a).FirstOrDefault();
                if (adat != null)
                {
                    Mód_telephely.Text = adat.Üzem.Trim();
                    MÓD_típustext.Text = adat.Típus.Trim();
                    Mód_üzembehelyezésdátuma.Value = adat.Üzembehelyezés < new DateTime(1901, 1, 1) ? new DateTime(1900, 1, 1) : adat.Üzembehelyezés;
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

                Adat_Jármű Elem = (from a in Adatok_Állomány
                                   where a.Azonosító == Mód_pályaszám.Text.Trim()
                                   select a).FirstOrDefault();

                if (Elem != null)
                {
                    Adat_Jármű ADAT = new Adat_Jármű(
                            Mód_pályaszám.Text.Trim(),
                            MÓD_főmérnökségitípus.Text.Trim(),
                            MÓD_járműtípus.Text.Trim(),
                            Mód_üzembehelyezésdátuma.Value);
                    KézJármű.Módosítás_Típus("Főmérnökség", ADAT);
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
                    Adatok_Állomány.Clear();
                    Adatok_Állomány = KézJármű.Lista_Adatok(Mód_telephely.Text.Trim());

                    Elem = (from a in Adatok_Állomány
                            where a.Azonosító == Mód_pályaszám.Text.Trim()
                            select a).FirstOrDefault();

                    if (Elem != null)
                    {
                        Adat_Jármű ADAT = new Adat_Jármű(
                                 Mód_pályaszám.Text.Trim(),
                                 MÓD_főmérnökségitípus.Text.Trim(),
                                 MÓD_járműtípus.Text.Trim(),
                                 Mód_üzembehelyezésdátuma.Value);
                        KézJármű.Módosítás_Típus(Mód_telephely.Text.Trim(), ADAT);
                    }
                    MessageBox.Show("Az adatok a telephelyi adatokban is módosultak!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Adatok_Állomány = KézJármű.Lista_Adatok("Főmérnökség");
            }
            catch (HibásBevittAdat ex)
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
                if (OpenFileDialogPI.ShowDialogEllenőr(OpenFileDialog1) == DialogResult.OK)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;

                List<Adat_Jármű> AdatokBe = SAP_Adatokbeolvasása.ÜzembeHelyezés_beolvasó(fájlexc);

                Adatok_Állomány.Clear();
                Adatok_Állomány = KézJármű.Lista_Adatok("Főmérnökség");

                Holtart.Be();

                List<Adat_Jármű> AdatokGy = new List<Adat_Jármű>();
                foreach (Adat_Jármű Adat in AdatokBe)
                {
                    Adat_Jármű AdatJármű = (from a in Adatok_Állomány
                                            where a.Azonosító == Adat.Azonosító.Trim()
                                            select a).FirstOrDefault();

                    if (AdatJármű != null) AdatokGy.Add(Adat);
                    Holtart.Lép();
                }
                if (AdatokGy.Count > 0) KézJármű.Módosítás_ÜzemBe("Főmérnökség", AdatokGy);

                Holtart.Ki();
                MessageBox.Show("Az adat konvertálás befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Adatok_Állomány = KézJármű.Lista_Adatok("Főmérnökség");
            }
            catch (HibásBevittAdat ex)
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
                Adatok_Napló = KadatNapló.Lista_adatok(Mozg_Dátum.Value.Year);
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
                Tábla.CleanFilterAndSort();
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
                if (Tábla.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Telephelyek_közötti_Naplózások_{Program.PostásNév}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyX.DataGridViewToXML(fájlexc, Tábla);
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
        #endregion


        #region Átadás-átvétel fül
        private void Telephelyeklistázasa()
        {
            try
            {
                List<Adat_kiegészítő_telephely> Adatok = KézTelephely.Lista_Adatok();

                Lektelephely.Items.Clear();
                foreach (Adat_kiegészítő_telephely Elem in Adatok)
                    Lektelephely.Items.Add(Elem.Telephelynév);
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
                Adatok_Állomány = KézJármű.Lista_Adatok("Főmérnökség");
                List<Adat_Jármű> Elemek = (from a in Adatok_Állomány
                                           where a.Üzem.Trim() == "Közös" && !a.Törölt
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
                List<Adat_Jármű_Állomány_Típus> Adatok = KézÁllomány.Lista_Adatok(Cmbtelephely.Text.Trim());

                Telephelyi_típus.Items.Clear();
                foreach (Adat_Jármű_Állomány_Típus Elem in Adatok)
                    Telephelyi_típus.Items.Add(Elem.Típus);

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
                List<Adat_Jármű> Adatok = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adatok = (from a in Adatok
                          where a.Típus == Telephelyi_típus.Text.Trim()
                          && a.Törölt == false
                          orderby a.Azonosító
                          select a).ToList();

                Saját_járművek.Items.Clear();
                foreach (Adat_Jármű Elem in Adatok)
                    Saját_járművek.Items.Add(Elem.Azonosító);

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
                Adatok_Állomány = KézJármű.Lista_Adatok("Főmérnökség");
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
                    Adatok_Állomány = KézJármű.Lista_Adatok("Főmérnökség");
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
                string hova = Cmbtelephely.Text.Trim();
                string honnan = "Főmérnökség";

                ÁlllományLétrehozás(hova, Közös_járművek.SelectedItem.ToStrTrim());      // áthelyezzük a fogadó telephelyre
                ÁllományMódosítás(honnan, Közös_járművek.SelectedItem.ToStrTrim(), hova);
                ÁllományNaplózás(Közös_járművek.SelectedItem.ToStrTrim(), "Közös", hova);   //Naplózzuk
                TípusDB(true);   // Módosítjuk a típus darabszámát
                HibákMásolása(honnan, hova, Közös_járművek.SelectedItem.ToStrTrim(), Telephelyi_típus.Text.Trim());   // hibákat átmásoljuk az állományba              
                E2Másolása(honnan, hova, Közös_járművek.SelectedItem.ToStrTrim());  //E2 másolás
            }
            catch (HibásBevittAdat ex)
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
                Adatok_Állomány = KézJármű.Lista_Adatok("Főmérnökség");
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
                string hova = "Főmérnökség";
                string honnan = Cmbtelephely.Text.Trim();

                ÁllományMódosítás(hova, Saját_járművek.SelectedItem.ToStrTrim(), "Közös"); // berakjuk közös állományba             
                KézJármű.Törlés(honnan, Saját_járművek.SelectedItem.ToStrTrim());     // kitöröljük a telephelyről             
                ÁllományNaplózás(Saját_járművek.SelectedItem.ToStrTrim(), honnan, "Közös"); //Naplózzuk
                TípusDB(false);    // Módosítjuk a típus darabszámát
                HibákMásolása(honnan, hova, Saját_járművek.SelectedItem.ToStrTrim(), Telephelyi_típus.Text.Trim());  //Hibák másolás
                E2Másolása(honnan, hova, Saját_járművek.SelectedItem.ToStrTrim());  //E2 másolás
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ÁllományMódosítás(string Telephely, string azonosító, string Hova)
        {
            try
            {
                Adat_Jármű ADAT = new Adat_Jármű(azonosító, Telephelyi_típus.Text.Trim(), Hova);
                KézJármű.Módosítás_ÜzemÁtvétel(Telephely, ADAT);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ÁlllományLétrehozás(string Telephely, string azonosító)
        {
            try
            {
                Adat_Jármű adat = (from a in Adatok_Állomány
                                   where a.Azonosító == azonosító
                                   select a).FirstOrDefault();

                if (adat.Üzem == Cmbtelephely.Text.Trim())
                {
                    adat.Üzem = Cmbtelephely.Text.Trim();
                    adat.Típus = Telephelyi_típus.Text.Trim();
                    // ha van a telephelyen
                    KézJármű.Módosítás(Telephely, adat);
                }
                else
                {
                    adat.Üzem = Cmbtelephely.Text.Trim();
                    adat.Típus = Telephelyi_típus.Text.Trim();
                    // ha nincs a telephelyen
                    KézJármű.Rögzítés(Telephely, adat);
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

        private void TípusDB(bool be)
        {
            try
            {
                List<Adat_Jármű_Állomány_Típus> Adatok = KézÁllomány.Lista_Adatok(Cmbtelephely.Text.Trim()) ?? throw new HibásBevittAdat("Nincs telephelyi állomány.");
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
                    Adat_Jármű_Állomány_Típus ADAT = new Adat_Jármű_Állomány_Típus(0, állomány, Telephelyi_típus.Text.Trim());
                    KézÁllomány.Módosítás(Cmbtelephely.Text.Trim(), EgyTípus);
                }
                else
                {
                    Adat_Jármű_Állomány_Típus ADAT = new Adat_Jármű_Állomány_Típus(0, 1, Telephelyi_típus.Text.Trim());
                    KézÁllomány.Rögzítés(Cmbtelephely.Text.Trim(), EgyTípus);
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
                List<Adat_Jármű_hiba> JHadatok = Kéz_JHadat.Lista_Adatok(honnan);
                if (JHadatok == null) return;
                JHadatok = (from a in JHadatok
                            where a.Azonosító == azonosító
                            select a).ToList();

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
                Kéz_JHadat.Törlés(honnan, azonosító);
            }
            catch (HibásBevittAdat ex)
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
                // E2 napot átmásoljuk az állományba
                List<Adat_Jármű_2> Adatok = KézJármű2.Lista_Adatok(honnan);
                if (Adatok == null) return;
                Adat_Jármű_2 adat = Adatok.FirstOrDefault(a => a.Azonosító == azonosító);
                if (adat != null)
                {
                    KézJármű2.Módosítás(hova, adat);
                    // kitöröljük
                    KézJármű2.Törlés(honnan, azonosító);
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

        private void Keresés_metódus()
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
                    FileName = $"Járművek_Telephelyek_{Program.PostásNév}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyX.DataGridViewToXML(fájlexc, Tábla_telephely);
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
                if (OpenFileDialogPI.ShowDialogEllenőr(OpenFileDialog1) == DialogResult.OK)
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